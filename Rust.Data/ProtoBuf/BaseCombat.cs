using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class BaseCombat : IDisposable, Pool.IPooled, IProto<BaseCombat>, IProto
{
	[NonSerialized]
	public int state;

	[NonSerialized]
	public float health;

	[NonSerialized]
	public float maxHealth;

	[NonSerialized]
	public float maxHealthOverride;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(BaseCombat instance)
	{
		if (instance.ShouldPool)
		{
			instance.state = 0;
			instance.health = 0f;
			instance.maxHealth = 0f;
			instance.maxHealthOverride = 0f;
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
			throw new Exception("Trying to dispose BaseCombat with ShouldPool set to false!");
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

	public void CopyTo(BaseCombat instance)
	{
		instance.state = state;
		instance.health = health;
		instance.maxHealth = maxHealth;
		instance.maxHealthOverride = maxHealthOverride;
	}

	public BaseCombat Copy()
	{
		BaseCombat baseCombat = Pool.Get<BaseCombat>();
		CopyTo(baseCombat);
		return baseCombat;
	}

	public static BaseCombat Deserialize(BufferStream stream)
	{
		BaseCombat baseCombat = Pool.Get<BaseCombat>();
		Deserialize(stream, baseCombat, isDelta: false);
		return baseCombat;
	}

	public static BaseCombat DeserializeLengthDelimited(BufferStream stream)
	{
		BaseCombat baseCombat = Pool.Get<BaseCombat>();
		DeserializeLengthDelimited(stream, baseCombat, isDelta: false);
		return baseCombat;
	}

	public static BaseCombat DeserializeLength(BufferStream stream, int length)
	{
		BaseCombat baseCombat = Pool.Get<BaseCombat>();
		DeserializeLength(stream, length, baseCombat, isDelta: false);
		return baseCombat;
	}

	public static BaseCombat Deserialize(byte[] buffer)
	{
		BaseCombat baseCombat = Pool.Get<BaseCombat>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, baseCombat, isDelta: false);
		return baseCombat;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, BaseCombat previous)
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

	public static BaseCombat Deserialize(BufferStream stream, BaseCombat instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.state = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 21:
				instance.health = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.maxHealth = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.maxHealthOverride = ProtocolParser.ReadSingle(stream);
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

	public static BaseCombat DeserializeLengthDelimited(BufferStream stream, BaseCombat instance, bool isDelta)
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
				instance.state = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 21:
				instance.health = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.maxHealth = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.maxHealthOverride = ProtocolParser.ReadSingle(stream);
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

	public static BaseCombat DeserializeLength(BufferStream stream, int length, BaseCombat instance, bool isDelta)
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
				instance.state = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 21:
				instance.health = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.maxHealth = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.maxHealthOverride = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, BaseCombat instance, BaseCombat previous)
	{
		if (instance.state != previous.state)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.state);
		}
		if (instance.health != previous.health)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.health);
		}
		if (instance.maxHealth != previous.maxHealth)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.maxHealth);
		}
		if (instance.maxHealthOverride != previous.maxHealthOverride)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.maxHealthOverride);
		}
	}

	public static void Serialize(BufferStream stream, BaseCombat instance)
	{
		if (instance.state != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.state);
		}
		if (instance.health != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.health);
		}
		if (instance.maxHealth != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.maxHealth);
		}
		if (instance.maxHealthOverride != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.maxHealthOverride);
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
