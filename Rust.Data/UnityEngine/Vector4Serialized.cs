using System.IO;
using SilentOrbit.ProtocolBuffers;

namespace UnityEngine;

public class Vector4Serialized
{
	public static void ResetToPool(Vector4 instance)
	{
		instance.x = 0f;
		instance.y = 0f;
		instance.z = 0f;
		instance.w = 0f;
	}

	public static Vector4 Deserialize(BufferStream stream, ref Vector4 instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.x = 0f;
			instance.y = 0f;
			instance.z = 0f;
			instance.w = 0f;
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
			case 37:
				instance.w = ProtocolParser.ReadSingle(stream);
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

	public static Vector4 DeserializeLengthDelimited(BufferStream stream, ref Vector4 instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.x = 0f;
			instance.y = 0f;
			instance.z = 0f;
			instance.w = 0f;
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
			case 37:
				instance.w = ProtocolParser.ReadSingle(stream);
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

	public static Vector4 DeserializeLength(BufferStream stream, int length, ref Vector4 instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.x = 0f;
			instance.y = 0f;
			instance.z = 0f;
			instance.w = 0f;
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
			case 37:
				instance.w = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, Vector4 instance, Vector4 previous)
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
		if (instance.w != previous.w)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.w);
		}
	}

	public static void Serialize(BufferStream stream, Vector4 instance)
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
		if (instance.w != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.w);
		}
	}

	public void InspectUids(UidInspector<ulong> action)
	{
	}
}
