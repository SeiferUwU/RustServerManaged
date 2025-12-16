using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class WaypointRace : IDisposable, Pool.IPooled, IProto<WaypointRace>, IProto
{
	[NonSerialized]
	public List<Vector3> positions;

	[NonSerialized]
	public NetworkableId racingVehicle;

	[NonSerialized]
	public int currentWaypoint;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(WaypointRace instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.positions != null)
			{
				List<Vector3> obj = instance.positions;
				Pool.FreeUnmanaged(ref obj);
				instance.positions = obj;
			}
			instance.racingVehicle = default(NetworkableId);
			instance.currentWaypoint = 0;
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
			throw new Exception("Trying to dispose WaypointRace with ShouldPool set to false!");
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

	public void CopyTo(WaypointRace instance)
	{
		if (positions != null)
		{
			instance.positions = Pool.Get<List<Vector3>>();
			for (int i = 0; i < positions.Count; i++)
			{
				Vector3 item = positions[i];
				instance.positions.Add(item);
			}
		}
		else
		{
			instance.positions = null;
		}
		instance.racingVehicle = racingVehicle;
		instance.currentWaypoint = currentWaypoint;
	}

	public WaypointRace Copy()
	{
		WaypointRace waypointRace = Pool.Get<WaypointRace>();
		CopyTo(waypointRace);
		return waypointRace;
	}

	public static WaypointRace Deserialize(BufferStream stream)
	{
		WaypointRace waypointRace = Pool.Get<WaypointRace>();
		Deserialize(stream, waypointRace, isDelta: false);
		return waypointRace;
	}

	public static WaypointRace DeserializeLengthDelimited(BufferStream stream)
	{
		WaypointRace waypointRace = Pool.Get<WaypointRace>();
		DeserializeLengthDelimited(stream, waypointRace, isDelta: false);
		return waypointRace;
	}

	public static WaypointRace DeserializeLength(BufferStream stream, int length)
	{
		WaypointRace waypointRace = Pool.Get<WaypointRace>();
		DeserializeLength(stream, length, waypointRace, isDelta: false);
		return waypointRace;
	}

	public static WaypointRace Deserialize(byte[] buffer)
	{
		WaypointRace waypointRace = Pool.Get<WaypointRace>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, waypointRace, isDelta: false);
		return waypointRace;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, WaypointRace previous)
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

	public static WaypointRace Deserialize(BufferStream stream, WaypointRace instance, bool isDelta)
	{
		if (!isDelta && instance.positions == null)
		{
			instance.positions = Pool.Get<List<Vector3>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
			{
				Vector3 instance2 = default(Vector3);
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance2, isDelta);
				instance.positions.Add(instance2);
				break;
			}
			case 16:
				instance.racingVehicle = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 24:
				instance.currentWaypoint = (int)ProtocolParser.ReadUInt64(stream);
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

	public static WaypointRace DeserializeLengthDelimited(BufferStream stream, WaypointRace instance, bool isDelta)
	{
		if (!isDelta && instance.positions == null)
		{
			instance.positions = Pool.Get<List<Vector3>>();
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
			{
				Vector3 instance2 = default(Vector3);
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance2, isDelta);
				instance.positions.Add(instance2);
				break;
			}
			case 16:
				instance.racingVehicle = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 24:
				instance.currentWaypoint = (int)ProtocolParser.ReadUInt64(stream);
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

	public static WaypointRace DeserializeLength(BufferStream stream, int length, WaypointRace instance, bool isDelta)
	{
		if (!isDelta && instance.positions == null)
		{
			instance.positions = Pool.Get<List<Vector3>>();
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
			{
				Vector3 instance2 = default(Vector3);
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance2, isDelta);
				instance.positions.Add(instance2);
				break;
			}
			case 16:
				instance.racingVehicle = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 24:
				instance.currentWaypoint = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, WaypointRace instance, WaypointRace previous)
	{
		if (instance.positions != null)
		{
			for (int i = 0; i < instance.positions.Count; i++)
			{
				Vector3 vector = instance.positions[i];
				stream.WriteByte(10);
				BufferStream.RangeHandle range = stream.GetRange(1);
				int position = stream.Position;
				Vector3Serialized.SerializeDelta(stream, vector, vector);
				int num = stream.Position - position;
				if (num > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field positions (UnityEngine.Vector3)");
				}
				Span<byte> span = range.GetSpan();
				ProtocolParser.WriteUInt32((uint)num, span, 0);
			}
		}
		stream.WriteByte(16);
		ProtocolParser.WriteUInt64(stream, instance.racingVehicle.Value);
		if (instance.currentWaypoint != previous.currentWaypoint)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.currentWaypoint);
		}
	}

	public static void Serialize(BufferStream stream, WaypointRace instance)
	{
		if (instance.positions != null)
		{
			for (int i = 0; i < instance.positions.Count; i++)
			{
				Vector3 instance2 = instance.positions[i];
				stream.WriteByte(10);
				BufferStream.RangeHandle range = stream.GetRange(1);
				int position = stream.Position;
				Vector3Serialized.Serialize(stream, instance2);
				int num = stream.Position - position;
				if (num > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field positions (UnityEngine.Vector3)");
				}
				Span<byte> span = range.GetSpan();
				ProtocolParser.WriteUInt32((uint)num, span, 0);
			}
		}
		if (instance.racingVehicle != default(NetworkableId))
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.racingVehicle.Value);
		}
		if (instance.currentWaypoint != 0)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.currentWaypoint);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref racingVehicle.Value);
	}
}
