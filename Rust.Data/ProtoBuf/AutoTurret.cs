using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class AutoTurret : IDisposable, Pool.IPooled, IProto<AutoTurret>, IProto
{
	[NonSerialized]
	public Vector3 aimPos;

	[NonSerialized]
	public Vector3 aimDir;

	[NonSerialized]
	public uint targetID;

	[NonSerialized]
	public List<PlayerNameID> users;

	[NonSerialized]
	public int powerOrder;

	[NonSerialized]
	public List<NetworkableId> interferenceList;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(AutoTurret instance)
	{
		if (!instance.ShouldPool)
		{
			return;
		}
		instance.aimPos = default(Vector3);
		instance.aimDir = default(Vector3);
		instance.targetID = 0u;
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
		instance.powerOrder = 0;
		if (instance.interferenceList != null)
		{
			List<NetworkableId> obj2 = instance.interferenceList;
			Pool.FreeUnmanaged(ref obj2);
			instance.interferenceList = obj2;
		}
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
			throw new Exception("Trying to dispose AutoTurret with ShouldPool set to false!");
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

	public void CopyTo(AutoTurret instance)
	{
		instance.aimPos = aimPos;
		instance.aimDir = aimDir;
		instance.targetID = targetID;
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
		instance.powerOrder = powerOrder;
		if (interferenceList != null)
		{
			instance.interferenceList = Pool.Get<List<NetworkableId>>();
			for (int j = 0; j < interferenceList.Count; j++)
			{
				NetworkableId item2 = interferenceList[j];
				instance.interferenceList.Add(item2);
			}
		}
		else
		{
			instance.interferenceList = null;
		}
	}

	public AutoTurret Copy()
	{
		AutoTurret autoTurret = Pool.Get<AutoTurret>();
		CopyTo(autoTurret);
		return autoTurret;
	}

	public static AutoTurret Deserialize(BufferStream stream)
	{
		AutoTurret autoTurret = Pool.Get<AutoTurret>();
		Deserialize(stream, autoTurret, isDelta: false);
		return autoTurret;
	}

	public static AutoTurret DeserializeLengthDelimited(BufferStream stream)
	{
		AutoTurret autoTurret = Pool.Get<AutoTurret>();
		DeserializeLengthDelimited(stream, autoTurret, isDelta: false);
		return autoTurret;
	}

	public static AutoTurret DeserializeLength(BufferStream stream, int length)
	{
		AutoTurret autoTurret = Pool.Get<AutoTurret>();
		DeserializeLength(stream, length, autoTurret, isDelta: false);
		return autoTurret;
	}

	public static AutoTurret Deserialize(byte[] buffer)
	{
		AutoTurret autoTurret = Pool.Get<AutoTurret>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, autoTurret, isDelta: false);
		return autoTurret;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, AutoTurret previous)
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

	public static AutoTurret Deserialize(BufferStream stream, AutoTurret instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.users == null)
			{
				instance.users = Pool.Get<List<PlayerNameID>>();
			}
			if (instance.interferenceList == null)
			{
				instance.interferenceList = Pool.Get<List<NetworkableId>>();
			}
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.aimPos, isDelta);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.aimDir, isDelta);
				continue;
			case 24:
				instance.targetID = ProtocolParser.ReadUInt32(stream);
				continue;
			case 34:
				instance.users.Add(PlayerNameID.DeserializeLengthDelimited(stream));
				continue;
			case 40:
				instance.powerOrder = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 48:
				instance.interferenceList.Add(new NetworkableId(ProtocolParser.ReadUInt64(stream)));
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

	public static AutoTurret DeserializeLengthDelimited(BufferStream stream, AutoTurret instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.users == null)
			{
				instance.users = Pool.Get<List<PlayerNameID>>();
			}
			if (instance.interferenceList == null)
			{
				instance.interferenceList = Pool.Get<List<NetworkableId>>();
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
			case 10:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.aimPos, isDelta);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.aimDir, isDelta);
				continue;
			case 24:
				instance.targetID = ProtocolParser.ReadUInt32(stream);
				continue;
			case 34:
				instance.users.Add(PlayerNameID.DeserializeLengthDelimited(stream));
				continue;
			case 40:
				instance.powerOrder = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 48:
				instance.interferenceList.Add(new NetworkableId(ProtocolParser.ReadUInt64(stream)));
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

	public static AutoTurret DeserializeLength(BufferStream stream, int length, AutoTurret instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.users == null)
			{
				instance.users = Pool.Get<List<PlayerNameID>>();
			}
			if (instance.interferenceList == null)
			{
				instance.interferenceList = Pool.Get<List<NetworkableId>>();
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
			case 10:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.aimPos, isDelta);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.aimDir, isDelta);
				continue;
			case 24:
				instance.targetID = ProtocolParser.ReadUInt32(stream);
				continue;
			case 34:
				instance.users.Add(PlayerNameID.DeserializeLengthDelimited(stream));
				continue;
			case 40:
				instance.powerOrder = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 48:
				instance.interferenceList.Add(new NetworkableId(ProtocolParser.ReadUInt64(stream)));
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

	public static void SerializeDelta(BufferStream stream, AutoTurret instance, AutoTurret previous)
	{
		if (instance.aimPos != previous.aimPos)
		{
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.aimPos, previous.aimPos);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field aimPos (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.aimDir != previous.aimDir)
		{
			stream.WriteByte(18);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.aimDir, previous.aimDir);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field aimDir (UnityEngine.Vector3)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
		if (instance.targetID != previous.targetID)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.targetID);
		}
		if (instance.users != null)
		{
			for (int i = 0; i < instance.users.Count; i++)
			{
				PlayerNameID playerNameID = instance.users[i];
				stream.WriteByte(34);
				BufferStream.RangeHandle range3 = stream.GetRange(5);
				int position3 = stream.Position;
				PlayerNameID.SerializeDelta(stream, playerNameID, playerNameID);
				int val = stream.Position - position3;
				Span<byte> span3 = range3.GetSpan();
				int num3 = ProtocolParser.WriteUInt32((uint)val, span3, 0);
				if (num3 < 5)
				{
					span3[num3 - 1] |= 128;
					while (num3 < 4)
					{
						span3[num3++] = 128;
					}
					span3[4] = 0;
				}
			}
		}
		if (instance.powerOrder != previous.powerOrder)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.powerOrder);
		}
		if (instance.interferenceList != null)
		{
			for (int j = 0; j < instance.interferenceList.Count; j++)
			{
				NetworkableId networkableId = instance.interferenceList[j];
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, networkableId.Value);
			}
		}
	}

	public static void Serialize(BufferStream stream, AutoTurret instance)
	{
		if (instance.aimPos != default(Vector3))
		{
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.Serialize(stream, instance.aimPos);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field aimPos (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.aimDir != default(Vector3))
		{
			stream.WriteByte(18);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			Vector3Serialized.Serialize(stream, instance.aimDir);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field aimDir (UnityEngine.Vector3)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
		if (instance.targetID != 0)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.targetID);
		}
		if (instance.users != null)
		{
			for (int i = 0; i < instance.users.Count; i++)
			{
				PlayerNameID instance2 = instance.users[i];
				stream.WriteByte(34);
				BufferStream.RangeHandle range3 = stream.GetRange(5);
				int position3 = stream.Position;
				PlayerNameID.Serialize(stream, instance2);
				int val = stream.Position - position3;
				Span<byte> span3 = range3.GetSpan();
				int num3 = ProtocolParser.WriteUInt32((uint)val, span3, 0);
				if (num3 < 5)
				{
					span3[num3 - 1] |= 128;
					while (num3 < 4)
					{
						span3[num3++] = 128;
					}
					span3[4] = 0;
				}
			}
		}
		if (instance.powerOrder != 0)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.powerOrder);
		}
		if (instance.interferenceList != null)
		{
			for (int j = 0; j < instance.interferenceList.Count; j++)
			{
				NetworkableId networkableId = instance.interferenceList[j];
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, networkableId.Value);
			}
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
		for (int j = 0; j < interferenceList.Count; j++)
		{
			NetworkableId value = interferenceList[j];
			action(UidType.NetworkableId, ref value.Value);
			interferenceList[j] = value;
		}
	}
}
