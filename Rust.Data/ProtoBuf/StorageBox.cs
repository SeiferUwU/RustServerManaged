using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class StorageBox : IDisposable, Pool.IPooled, IProto<StorageBox>, IProto
{
	[NonSerialized]
	public ItemContainer contents;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(StorageBox instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.contents != null)
			{
				instance.contents.ResetToPool();
				instance.contents = null;
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
			throw new Exception("Trying to dispose StorageBox with ShouldPool set to false!");
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

	public void CopyTo(StorageBox instance)
	{
		if (contents != null)
		{
			if (instance.contents == null)
			{
				instance.contents = contents.Copy();
			}
			else
			{
				contents.CopyTo(instance.contents);
			}
		}
		else
		{
			instance.contents = null;
		}
	}

	public StorageBox Copy()
	{
		StorageBox storageBox = Pool.Get<StorageBox>();
		CopyTo(storageBox);
		return storageBox;
	}

	public static StorageBox Deserialize(BufferStream stream)
	{
		StorageBox storageBox = Pool.Get<StorageBox>();
		Deserialize(stream, storageBox, isDelta: false);
		return storageBox;
	}

	public static StorageBox DeserializeLengthDelimited(BufferStream stream)
	{
		StorageBox storageBox = Pool.Get<StorageBox>();
		DeserializeLengthDelimited(stream, storageBox, isDelta: false);
		return storageBox;
	}

	public static StorageBox DeserializeLength(BufferStream stream, int length)
	{
		StorageBox storageBox = Pool.Get<StorageBox>();
		DeserializeLength(stream, length, storageBox, isDelta: false);
		return storageBox;
	}

	public static StorageBox Deserialize(byte[] buffer)
	{
		StorageBox storageBox = Pool.Get<StorageBox>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, storageBox, isDelta: false);
		return storageBox;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, StorageBox previous)
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

	public static StorageBox Deserialize(BufferStream stream, StorageBox instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				if (instance.contents == null)
				{
					instance.contents = ItemContainer.DeserializeLengthDelimited(stream);
				}
				else
				{
					ItemContainer.DeserializeLengthDelimited(stream, instance.contents, isDelta);
				}
				break;
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

	public static StorageBox DeserializeLengthDelimited(BufferStream stream, StorageBox instance, bool isDelta)
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
				if (instance.contents == null)
				{
					instance.contents = ItemContainer.DeserializeLengthDelimited(stream);
				}
				else
				{
					ItemContainer.DeserializeLengthDelimited(stream, instance.contents, isDelta);
				}
				break;
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

	public static StorageBox DeserializeLength(BufferStream stream, int length, StorageBox instance, bool isDelta)
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
				if (instance.contents == null)
				{
					instance.contents = ItemContainer.DeserializeLengthDelimited(stream);
				}
				else
				{
					ItemContainer.DeserializeLengthDelimited(stream, instance.contents, isDelta);
				}
				break;
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

	public static void SerializeDelta(BufferStream stream, StorageBox instance, StorageBox previous)
	{
		if (instance.contents == null)
		{
			return;
		}
		stream.WriteByte(10);
		BufferStream.RangeHandle range = stream.GetRange(5);
		int position = stream.Position;
		ItemContainer.SerializeDelta(stream, instance.contents, previous.contents);
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

	public static void Serialize(BufferStream stream, StorageBox instance)
	{
		if (instance.contents == null)
		{
			return;
		}
		stream.WriteByte(10);
		BufferStream.RangeHandle range = stream.GetRange(5);
		int position = stream.Position;
		ItemContainer.Serialize(stream, instance.contents);
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

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		contents?.InspectUids(action);
	}
}
