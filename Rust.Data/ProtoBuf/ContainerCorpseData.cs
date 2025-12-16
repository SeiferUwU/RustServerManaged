using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class ContainerCorpseData : IDisposable, Pool.IPooled, IProto<ContainerCorpseData>, IProto
{
	[NonSerialized]
	public ulong lockOwnerId;

	[NonSerialized]
	public CodeLock codeLock;

	[NonSerialized]
	public KeyLock keyLock;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(ContainerCorpseData instance)
	{
		if (instance.ShouldPool)
		{
			instance.lockOwnerId = 0uL;
			if (instance.codeLock != null)
			{
				instance.codeLock.ResetToPool();
				instance.codeLock = null;
			}
			if (instance.keyLock != null)
			{
				instance.keyLock.ResetToPool();
				instance.keyLock = null;
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
			throw new Exception("Trying to dispose ContainerCorpseData with ShouldPool set to false!");
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

	public void CopyTo(ContainerCorpseData instance)
	{
		instance.lockOwnerId = lockOwnerId;
		if (codeLock != null)
		{
			if (instance.codeLock == null)
			{
				instance.codeLock = codeLock.Copy();
			}
			else
			{
				codeLock.CopyTo(instance.codeLock);
			}
		}
		else
		{
			instance.codeLock = null;
		}
		if (keyLock != null)
		{
			if (instance.keyLock == null)
			{
				instance.keyLock = keyLock.Copy();
			}
			else
			{
				keyLock.CopyTo(instance.keyLock);
			}
		}
		else
		{
			instance.keyLock = null;
		}
	}

	public ContainerCorpseData Copy()
	{
		ContainerCorpseData containerCorpseData = Pool.Get<ContainerCorpseData>();
		CopyTo(containerCorpseData);
		return containerCorpseData;
	}

	public static ContainerCorpseData Deserialize(BufferStream stream)
	{
		ContainerCorpseData containerCorpseData = Pool.Get<ContainerCorpseData>();
		Deserialize(stream, containerCorpseData, isDelta: false);
		return containerCorpseData;
	}

	public static ContainerCorpseData DeserializeLengthDelimited(BufferStream stream)
	{
		ContainerCorpseData containerCorpseData = Pool.Get<ContainerCorpseData>();
		DeserializeLengthDelimited(stream, containerCorpseData, isDelta: false);
		return containerCorpseData;
	}

	public static ContainerCorpseData DeserializeLength(BufferStream stream, int length)
	{
		ContainerCorpseData containerCorpseData = Pool.Get<ContainerCorpseData>();
		DeserializeLength(stream, length, containerCorpseData, isDelta: false);
		return containerCorpseData;
	}

	public static ContainerCorpseData Deserialize(byte[] buffer)
	{
		ContainerCorpseData containerCorpseData = Pool.Get<ContainerCorpseData>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, containerCorpseData, isDelta: false);
		return containerCorpseData;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, ContainerCorpseData previous)
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

	public static ContainerCorpseData Deserialize(BufferStream stream, ContainerCorpseData instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.lockOwnerId = ProtocolParser.ReadUInt64(stream);
				break;
			case 18:
				if (instance.codeLock == null)
				{
					instance.codeLock = CodeLock.DeserializeLengthDelimited(stream);
				}
				else
				{
					CodeLock.DeserializeLengthDelimited(stream, instance.codeLock, isDelta);
				}
				break;
			case 26:
				if (instance.keyLock == null)
				{
					instance.keyLock = KeyLock.DeserializeLengthDelimited(stream);
				}
				else
				{
					KeyLock.DeserializeLengthDelimited(stream, instance.keyLock, isDelta);
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

	public static ContainerCorpseData DeserializeLengthDelimited(BufferStream stream, ContainerCorpseData instance, bool isDelta)
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
				instance.lockOwnerId = ProtocolParser.ReadUInt64(stream);
				break;
			case 18:
				if (instance.codeLock == null)
				{
					instance.codeLock = CodeLock.DeserializeLengthDelimited(stream);
				}
				else
				{
					CodeLock.DeserializeLengthDelimited(stream, instance.codeLock, isDelta);
				}
				break;
			case 26:
				if (instance.keyLock == null)
				{
					instance.keyLock = KeyLock.DeserializeLengthDelimited(stream);
				}
				else
				{
					KeyLock.DeserializeLengthDelimited(stream, instance.keyLock, isDelta);
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

	public static ContainerCorpseData DeserializeLength(BufferStream stream, int length, ContainerCorpseData instance, bool isDelta)
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
				instance.lockOwnerId = ProtocolParser.ReadUInt64(stream);
				break;
			case 18:
				if (instance.codeLock == null)
				{
					instance.codeLock = CodeLock.DeserializeLengthDelimited(stream);
				}
				else
				{
					CodeLock.DeserializeLengthDelimited(stream, instance.codeLock, isDelta);
				}
				break;
			case 26:
				if (instance.keyLock == null)
				{
					instance.keyLock = KeyLock.DeserializeLengthDelimited(stream);
				}
				else
				{
					KeyLock.DeserializeLengthDelimited(stream, instance.keyLock, isDelta);
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

	public static void SerializeDelta(BufferStream stream, ContainerCorpseData instance, ContainerCorpseData previous)
	{
		if (instance.lockOwnerId != previous.lockOwnerId)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.lockOwnerId);
		}
		if (instance.codeLock != null)
		{
			stream.WriteByte(18);
			BufferStream.RangeHandle range = stream.GetRange(5);
			int position = stream.Position;
			CodeLock.SerializeDelta(stream, instance.codeLock, previous.codeLock);
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
		if (instance.keyLock != null)
		{
			stream.WriteByte(26);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			KeyLock.SerializeDelta(stream, instance.keyLock, previous.keyLock);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field keyLock (ProtoBuf.KeyLock)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
	}

	public static void Serialize(BufferStream stream, ContainerCorpseData instance)
	{
		if (instance.lockOwnerId != 0L)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.lockOwnerId);
		}
		if (instance.codeLock != null)
		{
			stream.WriteByte(18);
			BufferStream.RangeHandle range = stream.GetRange(5);
			int position = stream.Position;
			CodeLock.Serialize(stream, instance.codeLock);
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
		if (instance.keyLock != null)
		{
			stream.WriteByte(26);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			KeyLock.Serialize(stream, instance.keyLock);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field keyLock (ProtoBuf.KeyLock)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		codeLock?.InspectUids(action);
		keyLock?.InspectUids(action);
	}
}
