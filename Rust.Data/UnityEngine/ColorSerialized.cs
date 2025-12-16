using System.IO;
using SilentOrbit.ProtocolBuffers;

namespace UnityEngine;

public class ColorSerialized
{
	public static void ResetToPool(Color instance)
	{
		instance.r = 0f;
		instance.g = 0f;
		instance.b = 0f;
		instance.a = 0f;
	}

	public static Color Deserialize(BufferStream stream, ref Color instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.r = 0f;
			instance.g = 0f;
			instance.b = 0f;
			instance.a = 0f;
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.r = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.g = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.b = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.a = ProtocolParser.ReadSingle(stream);
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

	public static Color DeserializeLengthDelimited(BufferStream stream, ref Color instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.r = 0f;
			instance.g = 0f;
			instance.b = 0f;
			instance.a = 0f;
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
				instance.r = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.g = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.b = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.a = ProtocolParser.ReadSingle(stream);
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

	public static Color DeserializeLength(BufferStream stream, int length, ref Color instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.r = 0f;
			instance.g = 0f;
			instance.b = 0f;
			instance.a = 0f;
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
				instance.r = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.g = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.b = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.a = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, Color instance, Color previous)
	{
		if (instance.r != previous.r)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.r);
		}
		if (instance.g != previous.g)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.g);
		}
		if (instance.b != previous.b)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.b);
		}
		if (instance.a != previous.a)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.a);
		}
	}

	public static void Serialize(BufferStream stream, Color instance)
	{
		if (instance.r != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.r);
		}
		if (instance.g != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.g);
		}
		if (instance.b != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.b);
		}
		if (instance.a != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.a);
		}
	}

	public void InspectUids(UidInspector<ulong> action)
	{
	}
}
