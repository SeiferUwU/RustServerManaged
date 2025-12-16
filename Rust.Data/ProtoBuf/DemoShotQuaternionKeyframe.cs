using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public struct DemoShotQuaternionKeyframe : IProto<DemoShotQuaternionKeyframe>, IProto
{
	[NonSerialized]
	public float keyframeTime;

	[NonSerialized]
	public float keyFrameValueX;

	[NonSerialized]
	public float keyFrameValueY;

	[NonSerialized]
	public float keyFrameValueZ;

	[NonSerialized]
	public float keyFrameValueW;

	public static void ResetToPool(DemoShotQuaternionKeyframe instance)
	{
		instance.keyframeTime = 0f;
		instance.keyFrameValueX = 0f;
		instance.keyFrameValueY = 0f;
		instance.keyFrameValueZ = 0f;
		instance.keyFrameValueW = 0f;
	}

	public void CopyTo(DemoShotQuaternionKeyframe instance)
	{
		instance.keyframeTime = keyframeTime;
		instance.keyFrameValueX = keyFrameValueX;
		instance.keyFrameValueY = keyFrameValueY;
		instance.keyFrameValueZ = keyFrameValueZ;
		instance.keyFrameValueW = keyFrameValueW;
	}

	public DemoShotQuaternionKeyframe Copy()
	{
		DemoShotQuaternionKeyframe demoShotQuaternionKeyframe = default(DemoShotQuaternionKeyframe);
		CopyTo(demoShotQuaternionKeyframe);
		return demoShotQuaternionKeyframe;
	}

	public static DemoShotQuaternionKeyframe Deserialize(BufferStream stream)
	{
		DemoShotQuaternionKeyframe instance = default(DemoShotQuaternionKeyframe);
		Deserialize(stream, ref instance, isDelta: false);
		return instance;
	}

	public static DemoShotQuaternionKeyframe DeserializeLengthDelimited(BufferStream stream)
	{
		DemoShotQuaternionKeyframe instance = default(DemoShotQuaternionKeyframe);
		DeserializeLengthDelimited(stream, ref instance, isDelta: false);
		return instance;
	}

	public static DemoShotQuaternionKeyframe DeserializeLength(BufferStream stream, int length)
	{
		DemoShotQuaternionKeyframe instance = default(DemoShotQuaternionKeyframe);
		DeserializeLength(stream, length, ref instance, isDelta: false);
		return instance;
	}

	public static DemoShotQuaternionKeyframe Deserialize(byte[] buffer)
	{
		DemoShotQuaternionKeyframe instance = default(DemoShotQuaternionKeyframe);
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

	public void WriteToStreamDelta(BufferStream stream, DemoShotQuaternionKeyframe previous)
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

	public static DemoShotQuaternionKeyframe Deserialize(BufferStream stream, ref DemoShotQuaternionKeyframe instance, bool isDelta)
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
				instance.keyFrameValueX = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.keyFrameValueY = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.keyFrameValueZ = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.keyFrameValueW = ProtocolParser.ReadSingle(stream);
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

	public static DemoShotQuaternionKeyframe DeserializeLengthDelimited(BufferStream stream, ref DemoShotQuaternionKeyframe instance, bool isDelta)
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
				instance.keyFrameValueX = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.keyFrameValueY = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.keyFrameValueZ = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.keyFrameValueW = ProtocolParser.ReadSingle(stream);
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

	public static DemoShotQuaternionKeyframe DeserializeLength(BufferStream stream, int length, ref DemoShotQuaternionKeyframe instance, bool isDelta)
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
				instance.keyFrameValueX = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.keyFrameValueY = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.keyFrameValueZ = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.keyFrameValueW = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, DemoShotQuaternionKeyframe instance, DemoShotQuaternionKeyframe previous)
	{
		if (instance.keyframeTime != previous.keyframeTime)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.keyframeTime);
		}
		if (instance.keyFrameValueX != previous.keyFrameValueX)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.keyFrameValueX);
		}
		if (instance.keyFrameValueY != previous.keyFrameValueY)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.keyFrameValueY);
		}
		if (instance.keyFrameValueZ != previous.keyFrameValueZ)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.keyFrameValueZ);
		}
		if (instance.keyFrameValueW != previous.keyFrameValueW)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.keyFrameValueW);
		}
	}

	public static void Serialize(BufferStream stream, DemoShotQuaternionKeyframe instance)
	{
		if (instance.keyframeTime != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.keyframeTime);
		}
		if (instance.keyFrameValueX != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.keyFrameValueX);
		}
		if (instance.keyFrameValueY != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.keyFrameValueY);
		}
		if (instance.keyFrameValueZ != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.keyFrameValueZ);
		}
		if (instance.keyFrameValueW != 0f)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.keyFrameValueW);
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
