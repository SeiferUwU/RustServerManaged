using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class MissionInstanceData : IDisposable, Pool.IPooled, IProto<MissionInstanceData>, IProto
{
	[NonSerialized]
	public NetworkableId providerID;

	[NonSerialized]
	public float startTime;

	[NonSerialized]
	public float endTime;

	[NonSerialized]
	public Vector3 missionLocation;

	[NonSerialized]
	public List<ObjectiveStatus> objectiveStatuses;

	[NonSerialized]
	public List<MissionPoint> missionPoints;

	[NonSerialized]
	public List<MissionEntity> missionEntities;

	[NonSerialized]
	public int playerInputRequired;

	[NonSerialized]
	public long startTimeUtcSeconds;

	[NonSerialized]
	public long endTimeUtcSeconds;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(MissionInstanceData instance)
	{
		if (!instance.ShouldPool)
		{
			return;
		}
		instance.providerID = default(NetworkableId);
		instance.startTime = 0f;
		instance.endTime = 0f;
		instance.missionLocation = default(Vector3);
		if (instance.objectiveStatuses != null)
		{
			for (int i = 0; i < instance.objectiveStatuses.Count; i++)
			{
				if (instance.objectiveStatuses[i] != null)
				{
					instance.objectiveStatuses[i].ResetToPool();
					instance.objectiveStatuses[i] = null;
				}
			}
			List<ObjectiveStatus> obj = instance.objectiveStatuses;
			Pool.Free(ref obj, freeElements: false);
			instance.objectiveStatuses = obj;
		}
		if (instance.missionPoints != null)
		{
			for (int j = 0; j < instance.missionPoints.Count; j++)
			{
				if (instance.missionPoints[j] != null)
				{
					instance.missionPoints[j].ResetToPool();
					instance.missionPoints[j] = null;
				}
			}
			List<MissionPoint> obj2 = instance.missionPoints;
			Pool.Free(ref obj2, freeElements: false);
			instance.missionPoints = obj2;
		}
		if (instance.missionEntities != null)
		{
			for (int k = 0; k < instance.missionEntities.Count; k++)
			{
				if (instance.missionEntities[k] != null)
				{
					instance.missionEntities[k].ResetToPool();
					instance.missionEntities[k] = null;
				}
			}
			List<MissionEntity> obj3 = instance.missionEntities;
			Pool.Free(ref obj3, freeElements: false);
			instance.missionEntities = obj3;
		}
		instance.playerInputRequired = 0;
		instance.startTimeUtcSeconds = 0L;
		instance.endTimeUtcSeconds = 0L;
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
			throw new Exception("Trying to dispose MissionInstanceData with ShouldPool set to false!");
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

	public void CopyTo(MissionInstanceData instance)
	{
		instance.providerID = providerID;
		instance.startTime = startTime;
		instance.endTime = endTime;
		instance.missionLocation = missionLocation;
		if (objectiveStatuses != null)
		{
			instance.objectiveStatuses = Pool.Get<List<ObjectiveStatus>>();
			for (int i = 0; i < objectiveStatuses.Count; i++)
			{
				ObjectiveStatus item = objectiveStatuses[i].Copy();
				instance.objectiveStatuses.Add(item);
			}
		}
		else
		{
			instance.objectiveStatuses = null;
		}
		if (missionPoints != null)
		{
			instance.missionPoints = Pool.Get<List<MissionPoint>>();
			for (int j = 0; j < missionPoints.Count; j++)
			{
				MissionPoint item2 = missionPoints[j].Copy();
				instance.missionPoints.Add(item2);
			}
		}
		else
		{
			instance.missionPoints = null;
		}
		if (missionEntities != null)
		{
			instance.missionEntities = Pool.Get<List<MissionEntity>>();
			for (int k = 0; k < missionEntities.Count; k++)
			{
				MissionEntity item3 = missionEntities[k].Copy();
				instance.missionEntities.Add(item3);
			}
		}
		else
		{
			instance.missionEntities = null;
		}
		instance.playerInputRequired = playerInputRequired;
		instance.startTimeUtcSeconds = startTimeUtcSeconds;
		instance.endTimeUtcSeconds = endTimeUtcSeconds;
	}

	public MissionInstanceData Copy()
	{
		MissionInstanceData missionInstanceData = Pool.Get<MissionInstanceData>();
		CopyTo(missionInstanceData);
		return missionInstanceData;
	}

	public static MissionInstanceData Deserialize(BufferStream stream)
	{
		MissionInstanceData missionInstanceData = Pool.Get<MissionInstanceData>();
		Deserialize(stream, missionInstanceData, isDelta: false);
		return missionInstanceData;
	}

	public static MissionInstanceData DeserializeLengthDelimited(BufferStream stream)
	{
		MissionInstanceData missionInstanceData = Pool.Get<MissionInstanceData>();
		DeserializeLengthDelimited(stream, missionInstanceData, isDelta: false);
		return missionInstanceData;
	}

	public static MissionInstanceData DeserializeLength(BufferStream stream, int length)
	{
		MissionInstanceData missionInstanceData = Pool.Get<MissionInstanceData>();
		DeserializeLength(stream, length, missionInstanceData, isDelta: false);
		return missionInstanceData;
	}

	public static MissionInstanceData Deserialize(byte[] buffer)
	{
		MissionInstanceData missionInstanceData = Pool.Get<MissionInstanceData>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, missionInstanceData, isDelta: false);
		return missionInstanceData;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, MissionInstanceData previous)
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

	public static MissionInstanceData Deserialize(BufferStream stream, MissionInstanceData instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.objectiveStatuses == null)
			{
				instance.objectiveStatuses = Pool.Get<List<ObjectiveStatus>>();
			}
			if (instance.missionPoints == null)
			{
				instance.missionPoints = Pool.Get<List<MissionPoint>>();
			}
			if (instance.missionEntities == null)
			{
				instance.missionEntities = Pool.Get<List<MissionEntity>>();
			}
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.providerID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.startTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.endTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 34:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.missionLocation, isDelta);
				continue;
			case 42:
				instance.objectiveStatuses.Add(ObjectiveStatus.DeserializeLengthDelimited(stream));
				continue;
			case 50:
				instance.missionPoints.Add(MissionPoint.DeserializeLengthDelimited(stream));
				continue;
			case 58:
				instance.missionEntities.Add(MissionEntity.DeserializeLengthDelimited(stream));
				continue;
			case 72:
				instance.playerInputRequired = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 80:
				instance.startTimeUtcSeconds = (long)ProtocolParser.ReadUInt64(stream);
				continue;
			case 88:
				instance.endTimeUtcSeconds = (long)ProtocolParser.ReadUInt64(stream);
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

	public static MissionInstanceData DeserializeLengthDelimited(BufferStream stream, MissionInstanceData instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.objectiveStatuses == null)
			{
				instance.objectiveStatuses = Pool.Get<List<ObjectiveStatus>>();
			}
			if (instance.missionPoints == null)
			{
				instance.missionPoints = Pool.Get<List<MissionPoint>>();
			}
			if (instance.missionEntities == null)
			{
				instance.missionEntities = Pool.Get<List<MissionEntity>>();
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
			case 8:
				instance.providerID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.startTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.endTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 34:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.missionLocation, isDelta);
				continue;
			case 42:
				instance.objectiveStatuses.Add(ObjectiveStatus.DeserializeLengthDelimited(stream));
				continue;
			case 50:
				instance.missionPoints.Add(MissionPoint.DeserializeLengthDelimited(stream));
				continue;
			case 58:
				instance.missionEntities.Add(MissionEntity.DeserializeLengthDelimited(stream));
				continue;
			case 72:
				instance.playerInputRequired = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 80:
				instance.startTimeUtcSeconds = (long)ProtocolParser.ReadUInt64(stream);
				continue;
			case 88:
				instance.endTimeUtcSeconds = (long)ProtocolParser.ReadUInt64(stream);
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

	public static MissionInstanceData DeserializeLength(BufferStream stream, int length, MissionInstanceData instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.objectiveStatuses == null)
			{
				instance.objectiveStatuses = Pool.Get<List<ObjectiveStatus>>();
			}
			if (instance.missionPoints == null)
			{
				instance.missionPoints = Pool.Get<List<MissionPoint>>();
			}
			if (instance.missionEntities == null)
			{
				instance.missionEntities = Pool.Get<List<MissionEntity>>();
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
			case 8:
				instance.providerID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.startTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.endTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 34:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.missionLocation, isDelta);
				continue;
			case 42:
				instance.objectiveStatuses.Add(ObjectiveStatus.DeserializeLengthDelimited(stream));
				continue;
			case 50:
				instance.missionPoints.Add(MissionPoint.DeserializeLengthDelimited(stream));
				continue;
			case 58:
				instance.missionEntities.Add(MissionEntity.DeserializeLengthDelimited(stream));
				continue;
			case 72:
				instance.playerInputRequired = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 80:
				instance.startTimeUtcSeconds = (long)ProtocolParser.ReadUInt64(stream);
				continue;
			case 88:
				instance.endTimeUtcSeconds = (long)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, MissionInstanceData instance, MissionInstanceData previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.providerID.Value);
		if (instance.startTime != previous.startTime)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.startTime);
		}
		if (instance.endTime != previous.endTime)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.endTime);
		}
		if (instance.missionLocation != previous.missionLocation)
		{
			stream.WriteByte(34);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.missionLocation, previous.missionLocation);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field missionLocation (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.objectiveStatuses != null)
		{
			for (int i = 0; i < instance.objectiveStatuses.Count; i++)
			{
				ObjectiveStatus objectiveStatus = instance.objectiveStatuses[i];
				stream.WriteByte(42);
				BufferStream.RangeHandle range2 = stream.GetRange(1);
				int position2 = stream.Position;
				ObjectiveStatus.SerializeDelta(stream, objectiveStatus, objectiveStatus);
				int num2 = stream.Position - position2;
				if (num2 > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field objectiveStatuses (ProtoBuf.ObjectiveStatus)");
				}
				Span<byte> span2 = range2.GetSpan();
				ProtocolParser.WriteUInt32((uint)num2, span2, 0);
			}
		}
		if (instance.missionPoints != null)
		{
			for (int j = 0; j < instance.missionPoints.Count; j++)
			{
				MissionPoint missionPoint = instance.missionPoints[j];
				stream.WriteByte(50);
				BufferStream.RangeHandle range3 = stream.GetRange(5);
				int position3 = stream.Position;
				MissionPoint.SerializeDelta(stream, missionPoint, missionPoint);
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
		if (instance.missionEntities != null)
		{
			for (int k = 0; k < instance.missionEntities.Count; k++)
			{
				MissionEntity missionEntity = instance.missionEntities[k];
				stream.WriteByte(58);
				BufferStream.RangeHandle range4 = stream.GetRange(5);
				int position4 = stream.Position;
				MissionEntity.SerializeDelta(stream, missionEntity, missionEntity);
				int val2 = stream.Position - position4;
				Span<byte> span4 = range4.GetSpan();
				int num4 = ProtocolParser.WriteUInt32((uint)val2, span4, 0);
				if (num4 < 5)
				{
					span4[num4 - 1] |= 128;
					while (num4 < 4)
					{
						span4[num4++] = 128;
					}
					span4[4] = 0;
				}
			}
		}
		if (instance.playerInputRequired != previous.playerInputRequired)
		{
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.playerInputRequired);
		}
		stream.WriteByte(80);
		ProtocolParser.WriteUInt64(stream, (ulong)instance.startTimeUtcSeconds);
		stream.WriteByte(88);
		ProtocolParser.WriteUInt64(stream, (ulong)instance.endTimeUtcSeconds);
	}

	public static void Serialize(BufferStream stream, MissionInstanceData instance)
	{
		if (instance.providerID != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.providerID.Value);
		}
		if (instance.startTime != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.startTime);
		}
		if (instance.endTime != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.endTime);
		}
		if (instance.missionLocation != default(Vector3))
		{
			stream.WriteByte(34);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.Serialize(stream, instance.missionLocation);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field missionLocation (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.objectiveStatuses != null)
		{
			for (int i = 0; i < instance.objectiveStatuses.Count; i++)
			{
				ObjectiveStatus instance2 = instance.objectiveStatuses[i];
				stream.WriteByte(42);
				BufferStream.RangeHandle range2 = stream.GetRange(1);
				int position2 = stream.Position;
				ObjectiveStatus.Serialize(stream, instance2);
				int num2 = stream.Position - position2;
				if (num2 > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field objectiveStatuses (ProtoBuf.ObjectiveStatus)");
				}
				Span<byte> span2 = range2.GetSpan();
				ProtocolParser.WriteUInt32((uint)num2, span2, 0);
			}
		}
		if (instance.missionPoints != null)
		{
			for (int j = 0; j < instance.missionPoints.Count; j++)
			{
				MissionPoint instance3 = instance.missionPoints[j];
				stream.WriteByte(50);
				BufferStream.RangeHandle range3 = stream.GetRange(5);
				int position3 = stream.Position;
				MissionPoint.Serialize(stream, instance3);
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
		if (instance.missionEntities != null)
		{
			for (int k = 0; k < instance.missionEntities.Count; k++)
			{
				MissionEntity instance4 = instance.missionEntities[k];
				stream.WriteByte(58);
				BufferStream.RangeHandle range4 = stream.GetRange(5);
				int position4 = stream.Position;
				MissionEntity.Serialize(stream, instance4);
				int val2 = stream.Position - position4;
				Span<byte> span4 = range4.GetSpan();
				int num4 = ProtocolParser.WriteUInt32((uint)val2, span4, 0);
				if (num4 < 5)
				{
					span4[num4 - 1] |= 128;
					while (num4 < 4)
					{
						span4[num4++] = 128;
					}
					span4[4] = 0;
				}
			}
		}
		if (instance.playerInputRequired != 0)
		{
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.playerInputRequired);
		}
		if (instance.startTimeUtcSeconds != 0L)
		{
			stream.WriteByte(80);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.startTimeUtcSeconds);
		}
		if (instance.endTimeUtcSeconds != 0L)
		{
			stream.WriteByte(88);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.endTimeUtcSeconds);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref providerID.Value);
		if (objectiveStatuses != null)
		{
			for (int i = 0; i < objectiveStatuses.Count; i++)
			{
				objectiveStatuses[i]?.InspectUids(action);
			}
		}
		if (missionPoints != null)
		{
			for (int j = 0; j < missionPoints.Count; j++)
			{
				missionPoints[j]?.InspectUids(action);
			}
		}
		if (missionEntities != null)
		{
			for (int k = 0; k < missionEntities.Count; k++)
			{
				missionEntities[k]?.InspectUids(action);
			}
		}
	}
}
