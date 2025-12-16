using System.IO;
using SilentOrbit.ProtocolBuffers;

namespace UnityEngine;

public class Vector3Serialized
{
	public static void ResetToPool(Vector3 instance)
	{
		instance.x = 0f;
		instance.y = 0f;
		instance.z = 0f;
	}

	public static Vector3 Deserialize(BufferStream stream, ref Vector3 instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.x = 0f;
			instance.y = 0f;
			instance.z = 0f;
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.x = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.y = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.z = ProtocolParser.ReadSingle(stream);
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

	public static Vector3 DeserializeLengthDelimited(BufferStream stream, ref Vector3 instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.x = 0f;
			instance.y = 0f;
			instance.z = 0f;
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
			case 13:
				instance.x = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.y = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.z = ProtocolParser.ReadSingle(stream);
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

	public static Vector3 DeserializeLength(BufferStream stream, int length, ref Vector3 instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.x = 0f;
			instance.y = 0f;
			instance.z = 0f;
		}
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 13:
				instance.x = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.y = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.z = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, Vector3 instance, Vector3 previous)
	{
		if (instance.x != previous.x)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.x);
		}
		if (instance.y != previous.y)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.y);
		}
		if (instance.z != previous.z)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.z);
		}
	}

	public static void Serialize(BufferStream stream, Vector3 instance)
	{
		if (instance.x != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.x);
		}
		if (instance.y != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.y);
		}
		if (instance.z != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.z);
		}
	}

	public void InspectUids(UidInspector<ulong> action)
	{
	}
}
