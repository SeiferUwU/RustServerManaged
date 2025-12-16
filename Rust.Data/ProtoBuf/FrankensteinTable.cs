using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class FrankensteinTable : IDisposable, Pool.IPooled, IProto<FrankensteinTable>, IProto
{
	[NonSerialized]
	public List<int> itemIds;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(FrankensteinTable instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.itemIds != null)
			{
				List<int> obj = instance.itemIds;
				Pool.FreeUnmanaged(ref obj);
				instance.itemIds = obj;
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
			throw new Exception("Trying to dispose FrankensteinTable with ShouldPool set to false!");
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

	public void CopyTo(FrankensteinTable instance)
	{
		if (itemIds != null)
		{
			instance.itemIds = Pool.Get<List<int>>();
			for (int i = 0; i < itemIds.Count; i++)
			{
				int item = itemIds[i];
				instance.itemIds.Add(item);
			}
		}
		else
		{
			instance.itemIds = null;
		}
	}

	public FrankensteinTable Copy()
	{
		FrankensteinTable frankensteinTable = Pool.Get<FrankensteinTable>();
		CopyTo(frankensteinTable);
		return frankensteinTable;
	}

	public static FrankensteinTable Deserialize(BufferStream stream)
	{
		FrankensteinTable frankensteinTable = Pool.Get<FrankensteinTable>();
		Deserialize(stream, frankensteinTable, isDelta: false);
		return frankensteinTable;
	}

	public static FrankensteinTable DeserializeLengthDelimited(BufferStream stream)
	{
		FrankensteinTable frankensteinTable = Pool.Get<FrankensteinTable>();
		DeserializeLengthDelimited(stream, frankensteinTable, isDelta: false);
		return frankensteinTable;
	}

	public static FrankensteinTable DeserializeLength(BufferStream stream, int length)
	{
		FrankensteinTable frankensteinTable = Pool.Get<FrankensteinTable>();
		DeserializeLength(stream, length, frankensteinTable, isDelta: false);
		return frankensteinTable;
	}

	public static FrankensteinTable Deserialize(byte[] buffer)
	{
		FrankensteinTable frankensteinTable = Pool.Get<FrankensteinTable>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, frankensteinTable, isDelta: false);
		return frankensteinTable;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, FrankensteinTable previous)
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

	public static FrankensteinTable Deserialize(BufferStream stream, FrankensteinTable instance, bool isDelta)
	{
		if (!isDelta && instance.itemIds == null)
		{
			instance.itemIds = Pool.Get<List<int>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.itemIds.Add((int)ProtocolParser.ReadUInt64(stream));
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

	public static FrankensteinTable DeserializeLengthDelimited(BufferStream stream, FrankensteinTable instance, bool isDelta)
	{
		if (!isDelta && instance.itemIds == null)
		{
			instance.itemIds = Pool.Get<List<int>>();
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
				instance.itemIds.Add((int)ProtocolParser.ReadUInt64(stream));
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

	public static FrankensteinTable DeserializeLength(BufferStream stream, int length, FrankensteinTable instance, bool isDelta)
	{
		if (!isDelta && instance.itemIds == null)
		{
			instance.itemIds = Pool.Get<List<int>>();
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
				instance.itemIds.Add((int)ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, FrankensteinTable instance, FrankensteinTable previous)
	{
		if (instance.itemIds != null)
		{
			for (int i = 0; i < instance.itemIds.Count; i++)
			{
				int num = instance.itemIds[i];
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)num);
			}
		}
	}

	public static void Serialize(BufferStream stream, FrankensteinTable instance)
	{
		if (instance.itemIds != null)
		{
			for (int i = 0; i < instance.itemIds.Count; i++)
			{
				int num = instance.itemIds[i];
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
