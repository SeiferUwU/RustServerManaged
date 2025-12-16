using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class ConstructableEntity : IDisposable, Pool.IPooled, IProto<ConstructableEntity>, IProto
{
	[NonSerialized]
	public List<int> addedResources;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(ConstructableEntity instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.addedResources != null)
			{
				List<int> obj = instance.addedResources;
				Pool.FreeUnmanaged(ref obj);
				instance.addedResources = obj;
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
			throw new Exception("Trying to dispose ConstructableEntity with ShouldPool set to false!");
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

	public void CopyTo(ConstructableEntity instance)
	{
		if (addedResources != null)
		{
			instance.addedResources = Pool.Get<List<int>>();
			for (int i = 0; i < addedResources.Count; i++)
			{
				int item = addedResources[i];
				instance.addedResources.Add(item);
			}
		}
		else
		{
			instance.addedResources = null;
		}
	}

	public ConstructableEntity Copy()
	{
		ConstructableEntity constructableEntity = Pool.Get<ConstructableEntity>();
		CopyTo(constructableEntity);
		return constructableEntity;
	}

	public static ConstructableEntity Deserialize(BufferStream stream)
	{
		ConstructableEntity constructableEntity = Pool.Get<ConstructableEntity>();
		Deserialize(stream, constructableEntity, isDelta: false);
		return constructableEntity;
	}

	public static ConstructableEntity DeserializeLengthDelimited(BufferStream stream)
	{
		ConstructableEntity constructableEntity = Pool.Get<ConstructableEntity>();
		DeserializeLengthDelimited(stream, constructableEntity, isDelta: false);
		return constructableEntity;
	}

	public static ConstructableEntity DeserializeLength(BufferStream stream, int length)
	{
		ConstructableEntity constructableEntity = Pool.Get<ConstructableEntity>();
		DeserializeLength(stream, length, constructableEntity, isDelta: false);
		return constructableEntity;
	}

	public static ConstructableEntity Deserialize(byte[] buffer)
	{
		ConstructableEntity constructableEntity = Pool.Get<ConstructableEntity>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, constructableEntity, isDelta: false);
		return constructableEntity;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, ConstructableEntity previous)
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

	public static ConstructableEntity Deserialize(BufferStream stream, ConstructableEntity instance, bool isDelta)
	{
		if (!isDelta && instance.addedResources == null)
		{
			instance.addedResources = Pool.Get<List<int>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.addedResources.Add((int)ProtocolParser.ReadUInt64(stream));
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

	public static ConstructableEntity DeserializeLengthDelimited(BufferStream stream, ConstructableEntity instance, bool isDelta)
	{
		if (!isDelta && instance.addedResources == null)
		{
			instance.addedResources = Pool.Get<List<int>>();
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
			case 8:
				instance.addedResources.Add((int)ProtocolParser.ReadUInt64(stream));
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

	public static ConstructableEntity DeserializeLength(BufferStream stream, int length, ConstructableEntity instance, bool isDelta)
	{
		if (!isDelta && instance.addedResources == null)
		{
			instance.addedResources = Pool.Get<List<int>>();
		}
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 8:
				instance.addedResources.Add((int)ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, ConstructableEntity instance, ConstructableEntity previous)
	{
		if (instance.addedResources != null)
		{
			for (int i = 0; i < instance.addedResources.Count; i++)
			{
				int num = instance.addedResources[i];
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)num);
			}
		}
	}

	public static void Serialize(BufferStream stream, ConstructableEntity instance)
	{
		if (instance.addedResources != null)
		{
			for (int i = 0; i < instance.addedResources.Count; i++)
			{
				int num = instance.addedResources[i];
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)num);
			}
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
