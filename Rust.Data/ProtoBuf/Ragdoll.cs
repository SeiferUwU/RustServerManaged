using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Ragdoll : IDisposable, Pool.IPooled, IProto<Ragdoll>, IProto
{
	[NonSerialized]
	public float time;

	[NonSerialized]
	public List<int> positions;

	[NonSerialized]
	public List<int> rotations;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Ragdoll instance)
	{
		if (instance.ShouldPool)
		{
			instance.time = 0f;
			if (instance.positions != null)
			{
				List<int> obj = instance.positions;
				Pool.FreeUnmanaged(ref obj);
				instance.positions = obj;
			}
			if (instance.rotations != null)
			{
				List<int> obj2 = instance.rotations;
				Pool.FreeUnmanaged(ref obj2);
				instance.rotations = obj2;
			}
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
			throw new Exception("Trying to dispose Ragdoll with ShouldPool set to false!");
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

	public void CopyTo(Ragdoll instance)
	{
		instance.time = time;
		if (positions != null)
		{
			instance.positions = Pool.Get<List<int>>();
			for (int i = 0; i < positions.Count; i++)
			{
				int item = positions[i];
				instance.positions.Add(item);
			}
		}
		else
		{
			instance.positions = null;
		}
		if (rotations != null)
		{
			instance.rotations = Pool.Get<List<int>>();
			for (int j = 0; j < rotations.Count; j++)
			{
				int item2 = rotations[j];
				instance.rotations.Add(item2);
			}
		}
		else
		{
			instance.rotations = null;
		}
	}

	public Ragdoll Copy()
	{
		Ragdoll ragdoll = Pool.Get<Ragdoll>();
		CopyTo(ragdoll);
		return ragdoll;
	}

	public static Ragdoll Deserialize(BufferStream stream)
	{
		Ragdoll ragdoll = Pool.Get<Ragdoll>();
		Deserialize(stream, ragdoll, isDelta: false);
		return ragdoll;
	}

	public static Ragdoll DeserializeLengthDelimited(BufferStream stream)
	{
		Ragdoll ragdoll = Pool.Get<Ragdoll>();
		DeserializeLengthDelimited(stream, ragdoll, isDelta: false);
		return ragdoll;
	}

	public static Ragdoll DeserializeLength(BufferStream stream, int length)
	{
		Ragdoll ragdoll = Pool.Get<Ragdoll>();
		DeserializeLength(stream, length, ragdoll, isDelta: false);
		return ragdoll;
	}

	public static Ragdoll Deserialize(byte[] buffer)
	{
		Ragdoll ragdoll = Pool.Get<Ragdoll>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, ragdoll, isDelta: false);
		return ragdoll;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Ragdoll previous)
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

	public static Ragdoll Deserialize(BufferStream stream, Ragdoll instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.positions == null)
			{
				instance.positions = Pool.Get<List<int>>();
			}
			if (instance.rotations == null)
			{
				instance.rotations = Pool.Get<List<int>>();
			}
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.time = ProtocolParser.ReadSingle(stream);
				continue;
			case 16:
				instance.positions.Add((int)ProtocolParser.ReadUInt64(stream));
				continue;
			case 24:
				instance.rotations.Add((int)ProtocolParser.ReadUInt64(stream));
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

	public static Ragdoll DeserializeLengthDelimited(BufferStream stream, Ragdoll instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.positions == null)
			{
				instance.positions = Pool.Get<List<int>>();
			}
			if (instance.rotations == null)
			{
				instance.rotations = Pool.Get<List<int>>();
			}
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
				instance.time = ProtocolParser.ReadSingle(stream);
				continue;
			case 16:
				instance.positions.Add((int)ProtocolParser.ReadUInt64(stream));
				continue;
			case 24:
				instance.rotations.Add((int)ProtocolParser.ReadUInt64(stream));
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

	public static Ragdoll DeserializeLength(BufferStream stream, int length, Ragdoll instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.positions == null)
			{
				instance.positions = Pool.Get<List<int>>();
			}
			if (instance.rotations == null)
			{
				instance.rotations = Pool.Get<List<int>>();
			}
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
				instance.time = ProtocolParser.ReadSingle(stream);
				continue;
			case 16:
				instance.positions.Add((int)ProtocolParser.ReadUInt64(stream));
				continue;
			case 24:
				instance.rotations.Add((int)ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, Ragdoll instance, Ragdoll previous)
	{
		if (instance.time != previous.time)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.time);
		}
		if (instance.positions != null)
		{
			for (int i = 0; i < instance.positions.Count; i++)
			{
				int num = instance.positions[i];
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)num);
			}
		}
		if (instance.rotations != null)
		{
			for (int j = 0; j < instance.rotations.Count; j++)
			{
				int num2 = instance.rotations[j];
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)num2);
			}
		}
	}

	public static void Serialize(BufferStream stream, Ragdoll instance)
	{
		if (instance.time != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.time);
		}
		if (instance.positions != null)
		{
			for (int i = 0; i < instance.positions.Count; i++)
			{
				int num = instance.positions[i];
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)num);
			}
		}
		if (instance.rotations != null)
		{
			for (int j = 0; j < instance.rotations.Count; j++)
			{
				int num2 = instance.rotations[j];
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)num2);
			}
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
