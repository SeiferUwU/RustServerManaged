using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class InstrumentRecordingNote : IDisposable, Pool.IPooled, IProto<InstrumentRecordingNote>, IProto
{
	[NonSerialized]
	public float startTime;

	[NonSerialized]
	public float duration;

	[NonSerialized]
	public int note;

	[NonSerialized]
	public int octave;

	[NonSerialized]
	public float velocity;

	[NonSerialized]
	public int noteType;

	[NonSerialized]
	public bool shouldPlay;

	[NonSerialized]
	public bool hasPlayed;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(InstrumentRecordingNote instance)
	{
		if (instance.ShouldPool)
		{
			instance.startTime = 0f;
			instance.duration = 0f;
			instance.note = 0;
			instance.octave = 0;
			instance.velocity = 0f;
			instance.noteType = 0;
			instance.shouldPlay = false;
			instance.hasPlayed = false;
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
			throw new Exception("Trying to dispose InstrumentRecordingNote with ShouldPool set to false!");
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

	public void CopyTo(InstrumentRecordingNote instance)
	{
		instance.startTime = startTime;
		instance.duration = duration;
		instance.note = note;
		instance.octave = octave;
		instance.velocity = velocity;
		instance.noteType = noteType;
		instance.shouldPlay = shouldPlay;
		instance.hasPlayed = hasPlayed;
	}

	public InstrumentRecordingNote Copy()
	{
		InstrumentRecordingNote instrumentRecordingNote = Pool.Get<InstrumentRecordingNote>();
		CopyTo(instrumentRecordingNote);
		return instrumentRecordingNote;
	}

	public static InstrumentRecordingNote Deserialize(BufferStream stream)
	{
		InstrumentRecordingNote instrumentRecordingNote = Pool.Get<InstrumentRecordingNote>();
		Deserialize(stream, instrumentRecordingNote, isDelta: false);
		return instrumentRecordingNote;
	}

	public static InstrumentRecordingNote DeserializeLengthDelimited(BufferStream stream)
	{
		InstrumentRecordingNote instrumentRecordingNote = Pool.Get<InstrumentRecordingNote>();
		DeserializeLengthDelimited(stream, instrumentRecordingNote, isDelta: false);
		return instrumentRecordingNote;
	}

	public static InstrumentRecordingNote DeserializeLength(BufferStream stream, int length)
	{
		InstrumentRecordingNote instrumentRecordingNote = Pool.Get<InstrumentRecordingNote>();
		DeserializeLength(stream, length, instrumentRecordingNote, isDelta: false);
		return instrumentRecordingNote;
	}

	public static InstrumentRecordingNote Deserialize(byte[] buffer)
	{
		InstrumentRecordingNote instrumentRecordingNote = Pool.Get<InstrumentRecordingNote>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, instrumentRecordingNote, isDelta: false);
		return instrumentRecordingNote;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, InstrumentRecordingNote previous)
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

	public static InstrumentRecordingNote Deserialize(BufferStream stream, InstrumentRecordingNote instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.startTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.duration = ProtocolParser.ReadSingle(stream);
				continue;
			case 24:
				instance.note = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.octave = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 45:
				instance.velocity = ProtocolParser.ReadSingle(stream);
				continue;
			case 48:
				instance.noteType = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 56:
				instance.shouldPlay = ProtocolParser.ReadBool(stream);
				continue;
			case 64:
				instance.hasPlayed = ProtocolParser.ReadBool(stream);
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

	public static InstrumentRecordingNote DeserializeLengthDelimited(BufferStream stream, InstrumentRecordingNote instance, bool isDelta)
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
				instance.startTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.duration = ProtocolParser.ReadSingle(stream);
				continue;
			case 24:
				instance.note = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.octave = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 45:
				instance.velocity = ProtocolParser.ReadSingle(stream);
				continue;
			case 48:
				instance.noteType = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 56:
				instance.shouldPlay = ProtocolParser.ReadBool(stream);
				continue;
			case 64:
				instance.hasPlayed = ProtocolParser.ReadBool(stream);
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

	public static InstrumentRecordingNote DeserializeLength(BufferStream stream, int length, InstrumentRecordingNote instance, bool isDelta)
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
				instance.startTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.duration = ProtocolParser.ReadSingle(stream);
				continue;
			case 24:
				instance.note = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.octave = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 45:
				instance.velocity = ProtocolParser.ReadSingle(stream);
				continue;
			case 48:
				instance.noteType = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 56:
				instance.shouldPlay = ProtocolParser.ReadBool(stream);
				continue;
			case 64:
				instance.hasPlayed = ProtocolParser.ReadBool(stream);
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

	public static void SerializeDelta(BufferStream stream, InstrumentRecordingNote instance, InstrumentRecordingNote previous)
	{
		if (instance.startTime != previous.startTime)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.startTime);
		}
		if (instance.duration != previous.duration)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.duration);
		}
		if (instance.note != previous.note)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.note);
		}
		if (instance.octave != previous.octave)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.octave);
		}
		if (instance.velocity != previous.velocity)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.velocity);
		}
		if (instance.noteType != previous.noteType)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.noteType);
		}
		stream.WriteByte(56);
		ProtocolParser.WriteBool(stream, instance.shouldPlay);
		stream.WriteByte(64);
		ProtocolParser.WriteBool(stream, instance.hasPlayed);
	}

	public static void Serialize(BufferStream stream, InstrumentRecordingNote instance)
	{
		if (instance.startTime != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.startTime);
		}
		if (instance.duration != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.duration);
		}
		if (instance.note != 0)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.note);
		}
		if (instance.octave != 0)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.octave);
		}
		if (instance.velocity != 0f)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.velocity);
		}
		if (instance.noteType != 0)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.noteType);
		}
		if (instance.shouldPlay)
		{
			stream.WriteByte(56);
			ProtocolParser.WriteBool(stream, instance.shouldPlay);
		}
		if (instance.hasPlayed)
		{
			stream.WriteByte(64);
			ProtocolParser.WriteBool(stream, instance.hasPlayed);
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
