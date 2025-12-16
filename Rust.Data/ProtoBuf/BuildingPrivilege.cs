using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class BuildingPrivilege : IDisposable, Pool.IPooled, IProto<BuildingPrivilege>, IProto
{
	[NonSerialized]
	public List<PlayerNameID> users;

	[NonSerialized]
	public float upkeepPeriodMinutes;

	[NonSerialized]
	public float costFraction;

	[NonSerialized]
	public float protectedMinutes;

	[NonSerialized]
	public bool clientAuthed;

	[NonSerialized]
	public bool clientAnyAuthed;

	[NonSerialized]
	public float doorCostFraction;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(BuildingPrivilege instance)
	{
		if (!instance.ShouldPool)
		{
			return;
		}
		if (instance.users != null)
		{
			for (int i = 0; i < instance.users.Count; i++)
			{
				if (instance.users[i] != null)
				{
					instance.users[i].ResetToPool();
					instance.users[i] = null;
				}
			}
			List<PlayerNameID> obj = instance.users;
			Pool.Free(ref obj, freeElements: false);
			instance.users = obj;
		}
		instance.upkeepPeriodMinutes = 0f;
		instance.costFraction = 0f;
		instance.protectedMinutes = 0f;
		instance.clientAuthed = false;
		instance.clientAnyAuthed = false;
		instance.doorCostFraction = 0f;
		Pool.Free(ref instance);
	}

	public void ResetToPool()
	{
		ResetToPool(this);
	}

	public virtual void Dispose()
	{
		if (!ShouldPool)
		{
			throw new Exception("Trying to dispose BuildingPrivilege with ShouldPool set to false!");
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

	public void CopyTo(BuildingPrivilege instance)
	{
		if (users != null)
		{
			instance.users = Pool.Get<List<PlayerNameID>>();
			for (int i = 0; i < users.Count; i++)
			{
				PlayerNameID item = users[i].Copy();
				instance.users.Add(item);
			}
		}
		else
		{
			instance.users = null;
		}
		instance.upkeepPeriodMinutes = upkeepPeriodMinutes;
		instance.costFraction = costFraction;
		instance.protectedMinutes = protectedMinutes;
		instance.clientAuthed = clientAuthed;
		instance.clientAnyAuthed = clientAnyAuthed;
		instance.doorCostFraction = doorCostFraction;
	}

	public BuildingPrivilege Copy()
	{
		BuildingPrivilege buildingPrivilege = Pool.Get<BuildingPrivilege>();
		CopyTo(buildingPrivilege);
		return buildingPrivilege;
	}

	public static BuildingPrivilege Deserialize(BufferStream stream)
	{
		BuildingPrivilege buildingPrivilege = Pool.Get<BuildingPrivilege>();
		Deserialize(stream, buildingPrivilege, isDelta: false);
		return buildingPrivilege;
	}

	public static BuildingPrivilege DeserializeLengthDelimited(BufferStream stream)
	{
		BuildingPrivilege buildingPrivilege = Pool.Get<BuildingPrivilege>();
		DeserializeLengthDelimited(stream, buildingPrivilege, isDelta: false);
		return buildingPrivilege;
	}

	public static BuildingPrivilege DeserializeLength(BufferStream stream, int length)
	{
		BuildingPrivilege buildingPrivilege = Pool.Get<BuildingPrivilege>();
		DeserializeLength(stream, length, buildingPrivilege, isDelta: false);
		return buildingPrivilege;
	}

	public static BuildingPrivilege Deserialize(byte[] buffer)
	{
		BuildingPrivilege buildingPrivilege = Pool.Get<BuildingPrivilege>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, buildingPrivilege, isDelta: false);
		return buildingPrivilege;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, BuildingPrivilege previous)
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

	public static BuildingPrivilege Deserialize(BufferStream stream, BuildingPrivilege instance, bool isDelta)
	{
		if (!isDelta && instance.users == null)
		{
			instance.users = Pool.Get<List<PlayerNameID>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				instance.users.Add(PlayerNameID.DeserializeLengthDelimited(stream));
				continue;
			case 21:
				instance.upkeepPeriodMinutes = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.costFraction = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.protectedMinutes = ProtocolParser.ReadSingle(stream);
				continue;
			case 40:
				instance.clientAuthed = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.clientAnyAuthed = ProtocolParser.ReadBool(stream);
				continue;
			case 61:
				instance.doorCostFraction = ProtocolParser.ReadSingle(stream);
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

	public static BuildingPrivilege DeserializeLengthDelimited(BufferStream stream, BuildingPrivilege instance, bool isDelta)
	{
		if (!isDelta && instance.users == null)
		{
			instance.users = Pool.Get<List<PlayerNameID>>();
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
			case 10:
				instance.users.Add(PlayerNameID.DeserializeLengthDelimited(stream));
				continue;
			case 21:
				instance.upkeepPeriodMinutes = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.costFraction = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.protectedMinutes = ProtocolParser.ReadSingle(stream);
				continue;
			case 40:
				instance.clientAuthed = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.clientAnyAuthed = ProtocolParser.ReadBool(stream);
				continue;
			case 61:
				instance.doorCostFraction = ProtocolParser.ReadSingle(stream);
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

	public static BuildingPrivilege DeserializeLength(BufferStream stream, int length, BuildingPrivilege instance, bool isDelta)
	{
		if (!isDelta && instance.users == null)
		{
			instance.users = Pool.Get<List<PlayerNameID>>();
		}
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 10:
				instance.users.Add(PlayerNameID.DeserializeLengthDelimited(stream));
				continue;
			case 21:
				instance.upkeepPeriodMinutes = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.costFraction = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.protectedMinutes = ProtocolParser.ReadSingle(stream);
				continue;
			case 40:
				instance.clientAuthed = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.clientAnyAuthed = ProtocolParser.ReadBool(stream);
				continue;
			case 61:
				instance.doorCostFraction = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, BuildingPrivilege instance, BuildingPrivilege previous)
	{
		if (instance.users != null)
		{
			for (int i = 0; i < instance.users.Count; i++)
			{
				PlayerNameID playerNameID = instance.users[i];
				stream.WriteByte(10);
				BufferStream.RangeHandle range = stream.GetRange(5);
				int position = stream.Position;
				PlayerNameID.SerializeDelta(stream, playerNameID, playerNameID);
				int val = stream.Position - position;
				Span<byte> span = range.GetSpan();
				int num = ProtocolParser.WriteUInt32((uint)val, span, 0);
				if (num < 5)
				{
					span[num - 1] |= 128;
					while (num < 4)
					{
						span[num++] = 128;
					}
					span[4] = 0;
				}
			}
		}
		if (instance.upkeepPeriodMinutes != previous.upkeepPeriodMinutes)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.upkeepPeriodMinutes);
		}
		if (instance.costFraction != previous.costFraction)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.costFraction);
		}
		if (instance.protectedMinutes != previous.protectedMinutes)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.protectedMinutes);
		}
		stream.WriteByte(40);
		ProtocolParser.WriteBool(stream, instance.clientAuthed);
		stream.WriteByte(48);
		ProtocolParser.WriteBool(stream, instance.clientAnyAuthed);
		if (instance.doorCostFraction != previous.doorCostFraction)
		{
			stream.WriteByte(61);
			ProtocolParser.WriteSingle(stream, instance.doorCostFraction);
		}
	}

	public static void Serialize(BufferStream stream, BuildingPrivilege instance)
	{
		if (instance.users != null)
		{
			for (int i = 0; i < instance.users.Count; i++)
			{
				PlayerNameID instance2 = instance.users[i];
				stream.WriteByte(10);
				BufferStream.RangeHandle range = stream.GetRange(5);
				int position = stream.Position;
				PlayerNameID.Serialize(stream, instance2);
				int val = stream.Position - position;
				Span<byte> span = range.GetSpan();
				int num = ProtocolParser.WriteUInt32((uint)val, span, 0);
				if (num < 5)
				{
					span[num - 1] |= 128;
					while (num < 4)
					{
						span[num++] = 128;
					}
					span[4] = 0;
				}
			}
		}
		if (instance.upkeepPeriodMinutes != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.upkeepPeriodMinutes);
		}
		if (instance.costFraction != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.costFraction);
		}
		if (instance.protectedMinutes != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.protectedMinutes);
		}
		if (instance.clientAuthed)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteBool(stream, instance.clientAuthed);
		}
		if (instance.clientAnyAuthed)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteBool(stream, instance.clientAnyAuthed);
		}
		if (instance.doorCostFraction != 0f)
		{
			stream.WriteByte(61);
			ProtocolParser.WriteSingle(stream, instance.doorCostFraction);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		if (users != null)
		{
			for (int i = 0; i < users.Count; i++)
			{
				users[i]?.InspectUids(action);
			}
		}
	}
}
