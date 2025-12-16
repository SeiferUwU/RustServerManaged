using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class PlayerProjectileAttack : IDisposable, Pool.IPooled, IProto<PlayerProjectileAttack>, IProto
{
	[NonSerialized]
	public PlayerAttack playerAttack;

	[NonSerialized]
	public Vector3 hitVelocity;

	[NonSerialized]
	public float hitDistance;

	[NonSerialized]
	public float travelTime;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(PlayerProjectileAttack instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.playerAttack != null)
			{
				instance.playerAttack.ResetToPool();
				instance.playerAttack = null;
			}
			instance.hitVelocity = default(Vector3);
			instance.hitDistance = 0f;
			instance.travelTime = 0f;
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
			throw new Exception("Trying to dispose PlayerProjectileAttack with ShouldPool set to false!");
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

	public void CopyTo(PlayerProjectileAttack instance)
	{
		if (playerAttack != null)
		{
			if (instance.playerAttack == null)
			{
				instance.playerAttack = playerAttack.Copy();
			}
			else
			{
				playerAttack.CopyTo(instance.playerAttack);
			}
		}
		else
		{
			instance.playerAttack = null;
		}
		instance.hitVelocity = hitVelocity;
		instance.hitDistance = hitDistance;
		instance.travelTime = travelTime;
	}

	public PlayerProjectileAttack Copy()
	{
		PlayerProjectileAttack playerProjectileAttack = Pool.Get<PlayerProjectileAttack>();
		CopyTo(playerProjectileAttack);
		return playerProjectileAttack;
	}

	public static PlayerProjectileAttack Deserialize(BufferStream stream)
	{
		PlayerProjectileAttack playerProjectileAttack = Pool.Get<PlayerProjectileAttack>();
		Deserialize(stream, playerProjectileAttack, isDelta: false);
		return playerProjectileAttack;
	}

	public static PlayerProjectileAttack DeserializeLengthDelimited(BufferStream stream)
	{
		PlayerProjectileAttack playerProjectileAttack = Pool.Get<PlayerProjectileAttack>();
		DeserializeLengthDelimited(stream, playerProjectileAttack, isDelta: false);
		return playerProjectileAttack;
	}

	public static PlayerProjectileAttack DeserializeLength(BufferStream stream, int length)
	{
		PlayerProjectileAttack playerProjectileAttack = Pool.Get<PlayerProjectileAttack>();
		DeserializeLength(stream, length, playerProjectileAttack, isDelta: false);
		return playerProjectileAttack;
	}

	public static PlayerProjectileAttack Deserialize(byte[] buffer)
	{
		PlayerProjectileAttack playerProjectileAttack = Pool.Get<PlayerProjectileAttack>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, playerProjectileAttack, isDelta: false);
		return playerProjectileAttack;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, PlayerProjectileAttack previous)
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

	public static PlayerProjectileAttack Deserialize(BufferStream stream, PlayerProjectileAttack instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				if (instance.playerAttack == null)
				{
					instance.playerAttack = PlayerAttack.DeserializeLengthDelimited(stream);
				}
				else
				{
					PlayerAttack.DeserializeLengthDelimited(stream, instance.playerAttack, isDelta);
				}
				break;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.hitVelocity, isDelta);
				break;
			case 29:
				instance.hitDistance = ProtocolParser.ReadSingle(stream);
				break;
			case 37:
				instance.travelTime = ProtocolParser.ReadSingle(stream);
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

	public static PlayerProjectileAttack DeserializeLengthDelimited(BufferStream stream, PlayerProjectileAttack instance, bool isDelta)
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
			case 10:
				if (instance.playerAttack == null)
				{
					instance.playerAttack = PlayerAttack.DeserializeLengthDelimited(stream);
				}
				else
				{
					PlayerAttack.DeserializeLengthDelimited(stream, instance.playerAttack, isDelta);
				}
				break;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.hitVelocity, isDelta);
				break;
			case 29:
				instance.hitDistance = ProtocolParser.ReadSingle(stream);
				break;
			case 37:
				instance.travelTime = ProtocolParser.ReadSingle(stream);
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

	public static PlayerProjectileAttack DeserializeLength(BufferStream stream, int length, PlayerProjectileAttack instance, bool isDelta)
	{
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 10:
				if (instance.playerAttack == null)
				{
					instance.playerAttack = PlayerAttack.DeserializeLengthDelimited(stream);
				}
				else
				{
					PlayerAttack.DeserializeLengthDelimited(stream, instance.playerAttack, isDelta);
				}
				break;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.hitVelocity, isDelta);
				break;
			case 29:
				instance.hitDistance = ProtocolParser.ReadSingle(stream);
				break;
			case 37:
				instance.travelTime = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, PlayerProjectileAttack instance, PlayerProjectileAttack previous)
	{
		if (instance.playerAttack == null)
		{
			throw new ArgumentNullException("playerAttack", "Required by proto specification.");
		}
		stream.WriteByte(10);
		BufferStream.RangeHandle range = stream.GetRange(2);
		int position = stream.Position;
		PlayerAttack.SerializeDelta(stream, instance.playerAttack, previous.playerAttack);
		int num = stream.Position - position;
		if (num > 16383)
		{
			throw new InvalidOperationException("Not enough space was reserved for the length prefix of field playerAttack (ProtoBuf.PlayerAttack)");
		}
		Span<byte> span = range.GetSpan();
		if (ProtocolParser.WriteUInt32((uint)num, span, 0) < 2)
		{
			span[0] |= 128;
			span[1] = 0;
		}
		if (instance.hitVelocity != previous.hitVelocity)
		{
			stream.WriteByte(18);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.hitVelocity, previous.hitVelocity);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field hitVelocity (UnityEngine.Vector3)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
		if (instance.hitDistance != previous.hitDistance)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.hitDistance);
		}
		if (instance.travelTime != previous.travelTime)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.travelTime);
		}
	}

	public static void Serialize(BufferStream stream, PlayerProjectileAttack instance)
	{
		if (instance.playerAttack == null)
		{
			throw new ArgumentNullException("playerAttack", "Required by proto specification.");
		}
		stream.WriteByte(10);
		BufferStream.RangeHandle range = stream.GetRange(2);
		int position = stream.Position;
		PlayerAttack.Serialize(stream, instance.playerAttack);
		int num = stream.Position - position;
		if (num > 16383)
		{
			throw new InvalidOperationException("Not enough space was reserved for the length prefix of field playerAttack (ProtoBuf.PlayerAttack)");
		}
		Span<byte> span = range.GetSpan();
		if (ProtocolParser.WriteUInt32((uint)num, span, 0) < 2)
		{
			span[0] |= 128;
			span[1] = 0;
		}
		if (instance.hitVelocity != default(Vector3))
		{
			stream.WriteByte(18);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			Vector3Serialized.Serialize(stream, instance.hitVelocity);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field hitVelocity (UnityEngine.Vector3)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
		if (instance.hitDistance != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.hitDistance);
		}
		if (instance.travelTime != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.travelTime);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		playerAttack?.InspectUids(action);
	}
}
