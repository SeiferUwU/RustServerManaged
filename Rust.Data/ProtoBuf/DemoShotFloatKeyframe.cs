using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public struct DemoShotFloatKeyframe : IProto<DemoShotFloatKeyframe>, IProto
{
	[NonSerialized]
	public float keyframeTime;

	[NonSerialized]
	public float keyFrameValue;

	public static void ResetToPool(DemoShotFloatKeyframe instance)
	{
		instance.keyframeTime = 0f;
		instance.keyFrameValue = 0f;
	}

	public void CopyTo(DemoShotFloatKeyframe instance)
	{
		instance.keyframeTime = keyframeTime;
		instance.keyFrameValue = keyFrameValue;
	}

	public DemoShotFloatKeyframe Copy()
	{
		DemoShotFloatKeyframe demoShotFloatKeyframe = default(DemoShotFloatKeyframe);
		CopyTo(demoShotFloatKeyframe);
		return demoShotFloatKeyframe;
	}

	public static DemoShotFloatKeyframe Deserialize(BufferStream stream)
	{
		DemoShotFloatKeyframe instance = default(DemoShotFloatKeyframe);
		Deserialize(stream, ref instance, isDelta: false);
		return instance;
	}

	public static DemoShotFloatKeyframe DeserializeLengthDelimited(BufferStream stream)
	{
		DemoShotFloatKeyframe instance = default(DemoShotFloatKeyframe);
		DeserializeLengthDelimited(stream, ref instance, isDelta: false);
		return instance;
	}

	public static DemoShotFloatKeyframe DeserializeLength(BufferStream stream, int length)
	{
		DemoShotFloatKeyframe instance = default(DemoShotFloatKeyframe);
		DeserializeLength(stream, length, ref instance, isDelta: false);
		return instance;
	}

	public static DemoShotFloatKeyframe Deserialize(byte[] buffer)
	{
		DemoShotFloatKeyframe instance = default(DemoShotFloatKeyframe);
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, ref instance, isDelta: false);
		return instance;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, ref this, isDelta);
	}

	public void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void WriteToStreamDelta(BufferStream stream, DemoShotFloatKeyframe previous)
	{
		SerializeDelta(stream, this, previous);
	}

	public void ReadFromStream(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, ref this, isDelta);
	}

	public void ReadFromStream(BufferStream stream, int size, bool isDelta = false)
	{
		DeserializeLength(stream, size, ref this, isDelta);
	}

	public static DemoShotFloatKeyframe Deserialize(BufferStream stream, ref DemoShotFloatKeyframe instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.keyframeTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.keyFrameValue = ProtocolParser.ReadSingle(stream);
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

	public static DemoShotFloatKeyframe DeserializeLengthDelimited(BufferStream stream, ref DemoShotFloatKeyframe instance, bool isDelta)
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
			case 13:
				instance.keyframeTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.keyFrameValue = ProtocolParser.ReadSingle(stream);
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

	public static DemoShotFloatKeyframe DeserializeLength(BufferStream stream, int length, ref DemoShotFloatKeyframe instance, bool isDelta)
	{
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 13:
				instance.keyframeTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.keyFrameValue = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, DemoShotFloatKeyframe instance, DemoShotFloatKeyframe previous)
	{
		if (instance.keyframeTime != previous.keyframeTime)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.keyframeTime);
		}
		if (instance.keyFrameValue != previous.keyFrameValue)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.keyFrameValue);
		}
	}

	public static void Serialize(BufferStream stream, DemoShotFloatKeyframe instance)
	{
		if (instance.keyframeTime != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.keyframeTime);
		}
		if (instance.keyFrameValue != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.keyFrameValue);
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
