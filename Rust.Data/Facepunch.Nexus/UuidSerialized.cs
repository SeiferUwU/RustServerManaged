using System.IO;
using SilentOrbit.ProtocolBuffers;

namespace Facepunch.Nexus;

public class UuidSerialized
{
	public static void ResetToPool(Uuid instance)
	{
		instance.NodeId = 0;
		instance.Sequence = 0;
		instance.Timestamp = 0uL;
	}

	public static Uuid Deserialize(BufferStream stream, ref Uuid instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.NodeId = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.Sequence = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.Timestamp = ProtocolParser.ReadUInt64(stream);
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

	public static Uuid DeserializeLengthDelimited(BufferStream stream, ref Uuid instance, bool isDelta)
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
				instance.NodeId = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.Sequence = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.Timestamp = ProtocolParser.ReadUInt64(stream);
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

	public static Uuid DeserializeLength(BufferStream stream, int length, ref Uuid instance, bool isDelta)
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
				instance.NodeId = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.Sequence = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.Timestamp = ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, Uuid instance, Uuid previous)
	{
		if (instance.NodeId != previous.NodeId)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.NodeId);
		}
		if (instance.Sequence != previous.Sequence)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Sequence);
		}
		if (instance.Timestamp != previous.Timestamp)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.Timestamp);
		}
	}

	public static void Serialize(BufferStream stream, Uuid instance)
	{
		if (instance.NodeId != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.NodeId);
		}
		if (instance.Sequence != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Sequence);
		}
		if (instance.Timestamp != 0L)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.Timestamp);
		}
	}

	public void InspectUids(UidInspector<ulong> action)
	{
	}
}
