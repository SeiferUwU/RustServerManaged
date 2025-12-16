using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class PuzzleReset : IDisposable, Pool.IPooled, IProto<PuzzleReset>, IProto
{
	[NonSerialized]
	public bool playerBlocksReset;

	[NonSerialized]
	public float playerDetectionRadius;

	[NonSerialized]
	public Vector3 playerDetectionOrigin;

	[NonSerialized]
	public float timeBetweenResets;

	[NonSerialized]
	public bool scaleWithServerPopulation;

	[NonSerialized]
	public bool checkSleepingAIZForPlayers;

	[NonSerialized]
	public bool ignoreAboveGroundPlayers;

	[NonSerialized]
	public bool broadcastResetMessage;

	[NonSerialized]
	public string resetPhrase;

	[NonSerialized]
	public bool radiationReset;

	[NonSerialized]
	public bool pauseUntilLooted;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(PuzzleReset instance)
	{
		if (instance.ShouldPool)
		{
			instance.playerBlocksReset = false;
			instance.playerDetectionRadius = 0f;
			instance.playerDetectionOrigin = default(Vector3);
			instance.timeBetweenResets = 0f;
			instance.scaleWithServerPopulation = false;
			instance.checkSleepingAIZForPlayers = false;
			instance.ignoreAboveGroundPlayers = false;
			instance.broadcastResetMessage = false;
			instance.resetPhrase = string.Empty;
			instance.radiationReset = false;
			instance.pauseUntilLooted = false;
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
			throw new Exception("Trying to dispose PuzzleReset with ShouldPool set to false!");
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

	public void CopyTo(PuzzleReset instance)
	{
		instance.playerBlocksReset = playerBlocksReset;
		instance.playerDetectionRadius = playerDetectionRadius;
		instance.playerDetectionOrigin = playerDetectionOrigin;
		instance.timeBetweenResets = timeBetweenResets;
		instance.scaleWithServerPopulation = scaleWithServerPopulation;
		instance.checkSleepingAIZForPlayers = checkSleepingAIZForPlayers;
		instance.ignoreAboveGroundPlayers = ignoreAboveGroundPlayers;
		instance.broadcastResetMessage = broadcastResetMessage;
		instance.resetPhrase = resetPhrase;
		instance.radiationReset = radiationReset;
		instance.pauseUntilLooted = pauseUntilLooted;
	}

	public PuzzleReset Copy()
	{
		PuzzleReset puzzleReset = Pool.Get<PuzzleReset>();
		CopyTo(puzzleReset);
		return puzzleReset;
	}

	public static PuzzleReset Deserialize(BufferStream stream)
	{
		PuzzleReset puzzleReset = Pool.Get<PuzzleReset>();
		Deserialize(stream, puzzleReset, isDelta: false);
		return puzzleReset;
	}

	public static PuzzleReset DeserializeLengthDelimited(BufferStream stream)
	{
		PuzzleReset puzzleReset = Pool.Get<PuzzleReset>();
		DeserializeLengthDelimited(stream, puzzleReset, isDelta: false);
		return puzzleReset;
	}

	public static PuzzleReset DeserializeLength(BufferStream stream, int length)
	{
		PuzzleReset puzzleReset = Pool.Get<PuzzleReset>();
		DeserializeLength(stream, length, puzzleReset, isDelta: false);
		return puzzleReset;
	}

	public static PuzzleReset Deserialize(byte[] buffer)
	{
		PuzzleReset puzzleReset = Pool.Get<PuzzleReset>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, puzzleReset, isDelta: false);
		return puzzleReset;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, PuzzleReset previous)
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

	public static PuzzleReset Deserialize(BufferStream stream, PuzzleReset instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.playerBlocksReset = ProtocolParser.ReadBool(stream);
				continue;
			case 21:
				instance.playerDetectionRadius = ProtocolParser.ReadSingle(stream);
				continue;
			case 26:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.playerDetectionOrigin, isDelta);
				continue;
			case 37:
				instance.timeBetweenResets = ProtocolParser.ReadSingle(stream);
				continue;
			case 40:
				instance.scaleWithServerPopulation = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.checkSleepingAIZForPlayers = ProtocolParser.ReadBool(stream);
				continue;
			case 56:
				instance.ignoreAboveGroundPlayers = ProtocolParser.ReadBool(stream);
				continue;
			case 64:
				instance.broadcastResetMessage = ProtocolParser.ReadBool(stream);
				continue;
			case 74:
				instance.resetPhrase = ProtocolParser.ReadString(stream);
				continue;
			case 80:
				instance.radiationReset = ProtocolParser.ReadBool(stream);
				continue;
			case 88:
				instance.pauseUntilLooted = ProtocolParser.ReadBool(stream);
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

	public static PuzzleReset DeserializeLengthDelimited(BufferStream stream, PuzzleReset instance, bool isDelta)
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
				instance.playerBlocksReset = ProtocolParser.ReadBool(stream);
				continue;
			case 21:
				instance.playerDetectionRadius = ProtocolParser.ReadSingle(stream);
				continue;
			case 26:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.playerDetectionOrigin, isDelta);
				continue;
			case 37:
				instance.timeBetweenResets = ProtocolParser.ReadSingle(stream);
				continue;
			case 40:
				instance.scaleWithServerPopulation = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.checkSleepingAIZForPlayers = ProtocolParser.ReadBool(stream);
				continue;
			case 56:
				instance.ignoreAboveGroundPlayers = ProtocolParser.ReadBool(stream);
				continue;
			case 64:
				instance.broadcastResetMessage = ProtocolParser.ReadBool(stream);
				continue;
			case 74:
				instance.resetPhrase = ProtocolParser.ReadString(stream);
				continue;
			case 80:
				instance.radiationReset = ProtocolParser.ReadBool(stream);
				continue;
			case 88:
				instance.pauseUntilLooted = ProtocolParser.ReadBool(stream);
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

	public static PuzzleReset DeserializeLength(BufferStream stream, int length, PuzzleReset instance, bool isDelta)
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
				instance.playerBlocksReset = ProtocolParser.ReadBool(stream);
				continue;
			case 21:
				instance.playerDetectionRadius = ProtocolParser.ReadSingle(stream);
				continue;
			case 26:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.playerDetectionOrigin, isDelta);
				continue;
			case 37:
				instance.timeBetweenResets = ProtocolParser.ReadSingle(stream);
				continue;
			case 40:
				instance.scaleWithServerPopulation = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.checkSleepingAIZForPlayers = ProtocolParser.ReadBool(stream);
				continue;
			case 56:
				instance.ignoreAboveGroundPlayers = ProtocolParser.ReadBool(stream);
				continue;
			case 64:
				instance.broadcastResetMessage = ProtocolParser.ReadBool(stream);
				continue;
			case 74:
				instance.resetPhrase = ProtocolParser.ReadString(stream);
				continue;
			case 80:
				instance.radiationReset = ProtocolParser.ReadBool(stream);
				continue;
			case 88:
				instance.pauseUntilLooted = ProtocolParser.ReadBool(stream);
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

	public static void SerializeDelta(BufferStream stream, PuzzleReset instance, PuzzleReset previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteBool(stream, instance.playerBlocksReset);
		if (instance.playerDetectionRadius != previous.playerDetectionRadius)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.playerDetectionRadius);
		}
		if (instance.playerDetectionOrigin != previous.playerDetectionOrigin)
		{
			stream.WriteByte(26);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.playerDetectionOrigin, previous.playerDetectionOrigin);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field playerDetectionOrigin (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.timeBetweenResets != previous.timeBetweenResets)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.timeBetweenResets);
		}
		stream.WriteByte(40);
		ProtocolParser.WriteBool(stream, instance.scaleWithServerPopulation);
		stream.WriteByte(48);
		ProtocolParser.WriteBool(stream, instance.checkSleepingAIZForPlayers);
		stream.WriteByte(56);
		ProtocolParser.WriteBool(stream, instance.ignoreAboveGroundPlayers);
		stream.WriteByte(64);
		ProtocolParser.WriteBool(stream, instance.broadcastResetMessage);
		if (instance.resetPhrase != null && instance.resetPhrase != previous.resetPhrase)
		{
			stream.WriteByte(74);
			ProtocolParser.WriteString(stream, instance.resetPhrase);
		}
		stream.WriteByte(80);
		ProtocolParser.WriteBool(stream, instance.radiationReset);
		stream.WriteByte(88);
		ProtocolParser.WriteBool(stream, instance.pauseUntilLooted);
	}

	public static void Serialize(BufferStream stream, PuzzleReset instance)
	{
		if (instance.playerBlocksReset)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.playerBlocksReset);
		}
		if (instance.playerDetectionRadius != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.playerDetectionRadius);
		}
		if (instance.playerDetectionOrigin != default(Vector3))
		{
			stream.WriteByte(26);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.Serialize(stream, instance.playerDetectionOrigin);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field playerDetectionOrigin (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.timeBetweenResets != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.timeBetweenResets);
		}
		if (instance.scaleWithServerPopulation)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteBool(stream, instance.scaleWithServerPopulation);
		}
		if (instance.checkSleepingAIZForPlayers)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteBool(stream, instance.checkSleepingAIZForPlayers);
		}
		if (instance.ignoreAboveGroundPlayers)
		{
			stream.WriteByte(56);
			ProtocolParser.WriteBool(stream, instance.ignoreAboveGroundPlayers);
		}
		if (instance.broadcastResetMessage)
		{
			stream.WriteByte(64);
			ProtocolParser.WriteBool(stream, instance.broadcastResetMessage);
		}
		if (instance.resetPhrase != null)
		{
			stream.WriteByte(74);
			ProtocolParser.WriteString(stream, instance.resetPhrase);
		}
		if (instance.radiationReset)
		{
			stream.WriteByte(80);
			ProtocolParser.WriteBool(stream, instance.radiationReset);
		}
		if (instance.pauseUntilLooted)
		{
			stream.WriteByte(88);
			ProtocolParser.WriteBool(stream, instance.pauseUntilLooted);
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
