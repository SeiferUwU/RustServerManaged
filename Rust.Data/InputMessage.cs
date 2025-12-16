using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

public class InputMessage : IDisposable, Pool.IPooled, IProto<InputMessage>, IProto
{
	[NonSerialized]
	public int buttons;

	[NonSerialized]
	public Vector3 aimAngles;

	[NonSerialized]
	public Vector3 mouseDelta;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(InputMessage instance)
	{
		if (instance.ShouldPool)
		{
			instance.buttons = 0;
			instance.aimAngles = default(Vector3);
			instance.mouseDelta = default(Vector3);
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
			throw new Exception("Trying to dispose InputMessage with ShouldPool set to false!");
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

	public void CopyTo(InputMessage instance)
	{
		instance.buttons = buttons;
		instance.aimAngles = aimAngles;
		instance.mouseDelta = mouseDelta;
	}

	public InputMessage Copy()
	{
		InputMessage inputMessage = Pool.Get<InputMessage>();
		CopyTo(inputMessage);
		return inputMessage;
	}

	public static InputMessage Deserialize(BufferStream stream)
	{
		InputMessage inputMessage = Pool.Get<InputMessage>();
		Deserialize(stream, inputMessage, isDelta: false);
		return inputMessage;
	}

	public static InputMessage DeserializeLengthDelimited(BufferStream stream)
	{
		InputMessage inputMessage = Pool.Get<InputMessage>();
		DeserializeLengthDelimited(stream, inputMessage, isDelta: false);
		return inputMessage;
	}

	public static InputMessage DeserializeLength(BufferStream stream, int length)
	{
		InputMessage inputMessage = Pool.Get<InputMessage>();
		DeserializeLength(stream, length, inputMessage, isDelta: false);
		return inputMessage;
	}

	public static InputMessage Deserialize(byte[] buffer)
	{
		InputMessage inputMessage = Pool.Get<InputMessage>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, inputMessage, isDelta: false);
		return inputMessage;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, InputMessage previous)
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

	public static InputMessage Deserialize(BufferStream stream, InputMessage instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.buttons = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.aimAngles, isDelta);
				continue;
			case 26:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.mouseDelta, isDelta);
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

	public static InputMessage DeserializeLengthDelimited(BufferStream stream, InputMessage instance, bool isDelta)
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
				instance.buttons = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.aimAngles, isDelta);
				continue;
			case 26:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.mouseDelta, isDelta);
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

	public static InputMessage DeserializeLength(BufferStream stream, int length, InputMessage instance, bool isDelta)
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
				instance.buttons = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.aimAngles, isDelta);
				continue;
			case 26:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.mouseDelta, isDelta);
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

	public static void SerializeDelta(BufferStream stream, InputMessage instance, InputMessage previous)
	{
		if (instance.buttons != previous.buttons)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.buttons);
		}
		if (instance.aimAngles != previous.aimAngles)
		{
			stream.WriteByte(18);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.aimAngles, previous.aimAngles);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field aimAngles (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.mouseDelta != previous.mouseDelta)
		{
			stream.WriteByte(26);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.mouseDelta, previous.mouseDelta);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field mouseDelta (UnityEngine.Vector3)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
	}

	public static void Serialize(BufferStream stream, InputMessage instance)
	{
		if (instance.buttons != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.buttons);
		}
		if (instance.aimAngles != default(Vector3))
		{
			stream.WriteByte(18);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.Serialize(stream, instance.aimAngles);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field aimAngles (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.mouseDelta != default(Vector3))
		{
			stream.WriteByte(26);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			Vector3Serialized.Serialize(stream, instance.mouseDelta);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field mouseDelta (UnityEngine.Vector3)");
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
	}
}
