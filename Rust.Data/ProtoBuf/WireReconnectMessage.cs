using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class WireReconnectMessage : IDisposable, Pool.IPooled, IProto<WireReconnectMessage>, IProto
{
	[NonSerialized]
	public NetworkableId entityId;

	[NonSerialized]
	public int slotIndex;

	[NonSerialized]
	public bool isInput;

	[NonSerialized]
	public List<Vector3> linePoints;

	[NonSerialized]
	public int wireColor;

	[NonSerialized]
	public List<WireLineAnchorInfo> lineAnchors;

	[NonSerialized]
	public List<float> slackLevels;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(WireReconnectMessage instance)
	{
		if (!instance.ShouldPool)
		{
			return;
		}
		instance.entityId = default(NetworkableId);
		instance.slotIndex = 0;
		instance.isInput = false;
		if (instance.linePoints != null)
		{
			List<Vector3> obj = instance.linePoints;
			Pool.FreeUnmanaged(ref obj);
			instance.linePoints = obj;
		}
		instance.wireColor = 0;
		if (instance.lineAnchors != null)
		{
			for (int i = 0; i < instance.lineAnchors.Count; i++)
			{
				if (instance.lineAnchors[i] != null)
				{
					instance.lineAnchors[i].ResetToPool();
					instance.lineAnchors[i] = null;
				}
			}
			List<WireLineAnchorInfo> obj2 = instance.lineAnchors;
			Pool.Free(ref obj2, freeElements: false);
			instance.lineAnchors = obj2;
		}
		if (instance.slackLevels != null)
		{
			List<float> obj3 = instance.slackLevels;
			Pool.FreeUnmanaged(ref obj3);
			instance.slackLevels = obj3;
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
			throw new Exception("Trying to dispose WireReconnectMessage with ShouldPool set to false!");
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

	public void CopyTo(WireReconnectMessage instance)
	{
		instance.entityId = entityId;
		instance.slotIndex = slotIndex;
		instance.isInput = isInput;
		if (linePoints != null)
		{
			instance.linePoints = Pool.Get<List<Vector3>>();
			for (int i = 0; i < linePoints.Count; i++)
			{
				Vector3 item = linePoints[i];
				instance.linePoints.Add(item);
			}
		}
		else
		{
			instance.linePoints = null;
		}
		instance.wireColor = wireColor;
		if (lineAnchors != null)
		{
			instance.lineAnchors = Pool.Get<List<WireLineAnchorInfo>>();
			for (int j = 0; j < lineAnchors.Count; j++)
			{
				WireLineAnchorInfo item2 = lineAnchors[j].Copy();
				instance.lineAnchors.Add(item2);
			}
		}
		else
		{
			instance.lineAnchors = null;
		}
		if (slackLevels != null)
		{
			instance.slackLevels = Pool.Get<List<float>>();
			for (int k = 0; k < slackLevels.Count; k++)
			{
				float item3 = slackLevels[k];
				instance.slackLevels.Add(item3);
			}
		}
		else
		{
			instance.slackLevels = null;
		}
	}

	public WireReconnectMessage Copy()
	{
		WireReconnectMessage wireReconnectMessage = Pool.Get<WireReconnectMessage>();
		CopyTo(wireReconnectMessage);
		return wireReconnectMessage;
	}

	public static WireReconnectMessage Deserialize(BufferStream stream)
	{
		WireReconnectMessage wireReconnectMessage = Pool.Get<WireReconnectMessage>();
		Deserialize(stream, wireReconnectMessage, isDelta: false);
		return wireReconnectMessage;
	}

	public static WireReconnectMessage DeserializeLengthDelimited(BufferStream stream)
	{
		WireReconnectMessage wireReconnectMessage = Pool.Get<WireReconnectMessage>();
		DeserializeLengthDelimited(stream, wireReconnectMessage, isDelta: false);
		return wireReconnectMessage;
	}

	public static WireReconnectMessage DeserializeLength(BufferStream stream, int length)
	{
		WireReconnectMessage wireReconnectMessage = Pool.Get<WireReconnectMessage>();
		DeserializeLength(stream, length, wireReconnectMessage, isDelta: false);
		return wireReconnectMessage;
	}

	public static WireReconnectMessage Deserialize(byte[] buffer)
	{
		WireReconnectMessage wireReconnectMessage = Pool.Get<WireReconnectMessage>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, wireReconnectMessage, isDelta: false);
		return wireReconnectMessage;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, WireReconnectMessage previous)
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

	public static WireReconnectMessage Deserialize(BufferStream stream, WireReconnectMessage instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.linePoints == null)
			{
				instance.linePoints = Pool.Get<List<Vector3>>();
			}
			if (instance.lineAnchors == null)
			{
				instance.lineAnchors = Pool.Get<List<WireLineAnchorInfo>>();
			}
			if (instance.slackLevels == null)
			{
				instance.slackLevels = Pool.Get<List<float>>();
			}
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.entityId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 16:
				instance.slotIndex = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 24:
				instance.isInput = ProtocolParser.ReadBool(stream);
				break;
			case 34:
			{
				Vector3 instance2 = default(Vector3);
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance2, isDelta);
				instance.linePoints.Add(instance2);
				break;
			}
			case 40:
				instance.wireColor = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 50:
				instance.lineAnchors.Add(WireLineAnchorInfo.DeserializeLengthDelimited(stream));
				break;
			case 61:
				instance.slackLevels.Add(ProtocolParser.ReadSingle(stream));
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

	public static WireReconnectMessage DeserializeLengthDelimited(BufferStream stream, WireReconnectMessage instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.linePoints == null)
			{
				instance.linePoints = Pool.Get<List<Vector3>>();
			}
			if (instance.lineAnchors == null)
			{
				instance.lineAnchors = Pool.Get<List<WireLineAnchorInfo>>();
			}
			if (instance.slackLevels == null)
			{
				instance.slackLevels = Pool.Get<List<float>>();
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
				instance.entityId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 16:
				instance.slotIndex = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 24:
				instance.isInput = ProtocolParser.ReadBool(stream);
				break;
			case 34:
			{
				Vector3 instance2 = default(Vector3);
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance2, isDelta);
				instance.linePoints.Add(instance2);
				break;
			}
			case 40:
				instance.wireColor = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 50:
				instance.lineAnchors.Add(WireLineAnchorInfo.DeserializeLengthDelimited(stream));
				break;
			case 61:
				instance.slackLevels.Add(ProtocolParser.ReadSingle(stream));
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

	public static WireReconnectMessage DeserializeLength(BufferStream stream, int length, WireReconnectMessage instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.linePoints == null)
			{
				instance.linePoints = Pool.Get<List<Vector3>>();
			}
			if (instance.lineAnchors == null)
			{
				instance.lineAnchors = Pool.Get<List<WireLineAnchorInfo>>();
			}
			if (instance.slackLevels == null)
			{
				instance.slackLevels = Pool.Get<List<float>>();
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
				instance.entityId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 16:
				instance.slotIndex = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 24:
				instance.isInput = ProtocolParser.ReadBool(stream);
				break;
			case 34:
			{
				Vector3 instance2 = default(Vector3);
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance2, isDelta);
				instance.linePoints.Add(instance2);
				break;
			}
			case 40:
				instance.wireColor = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 50:
				instance.lineAnchors.Add(WireLineAnchorInfo.DeserializeLengthDelimited(stream));
				break;
			case 61:
				instance.slackLevels.Add(ProtocolParser.ReadSingle(stream));
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

	public static void SerializeDelta(BufferStream stream, WireReconnectMessage instance, WireReconnectMessage previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.entityId.Value);
		if (instance.slotIndex != previous.slotIndex)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.slotIndex);
		}
		stream.WriteByte(24);
		ProtocolParser.WriteBool(stream, instance.isInput);
		if (instance.linePoints != null)
		{
			for (int i = 0; i < instance.linePoints.Count; i++)
			{
				Vector3 vector = instance.linePoints[i];
				stream.WriteByte(34);
				BufferStream.RangeHandle range = stream.GetRange(1);
				int position = stream.Position;
				Vector3Serialized.SerializeDelta(stream, vector, vector);
				int num = stream.Position - position;
				if (num > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field linePoints (UnityEngine.Vector3)");
				}
				Span<byte> span = range.GetSpan();
				ProtocolParser.WriteUInt32((uint)num, span, 0);
			}
		}
		if (instance.wireColor != previous.wireColor)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.wireColor);
		}
		if (instance.lineAnchors != null)
		{
			for (int j = 0; j < instance.lineAnchors.Count; j++)
			{
				WireLineAnchorInfo wireLineAnchorInfo = instance.lineAnchors[j];
				stream.WriteByte(50);
				BufferStream.RangeHandle range2 = stream.GetRange(5);
				int position2 = stream.Position;
				WireLineAnchorInfo.SerializeDelta(stream, wireLineAnchorInfo, wireLineAnchorInfo);
				int val = stream.Position - position2;
				Span<byte> span2 = range2.GetSpan();
				int num2 = ProtocolParser.WriteUInt32((uint)val, span2, 0);
				if (num2 < 5)
				{
					span2[num2 - 1] |= 128;
					while (num2 < 4)
					{
						span2[num2++] = 128;
					}
					span2[4] = 0;
				}
			}
		}
		if (instance.slackLevels != null)
		{
			for (int k = 0; k < instance.slackLevels.Count; k++)
			{
				float f = instance.slackLevels[k];
				stream.WriteByte(61);
				ProtocolParser.WriteSingle(stream, f);
			}
		}
	}

	public static void Serialize(BufferStream stream, WireReconnectMessage instance)
	{
		if (instance.entityId != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.entityId.Value);
		}
		if (instance.slotIndex != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.slotIndex);
		}
		if (instance.isInput)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteBool(stream, instance.isInput);
		}
		if (instance.linePoints != null)
		{
			for (int i = 0; i < instance.linePoints.Count; i++)
			{
				Vector3 instance2 = instance.linePoints[i];
				stream.WriteByte(34);
				BufferStream.RangeHandle range = stream.GetRange(1);
				int position = stream.Position;
				Vector3Serialized.Serialize(stream, instance2);
				int num = stream.Position - position;
				if (num > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field linePoints (UnityEngine.Vector3)");
				}
				Span<byte> span = range.GetSpan();
				ProtocolParser.WriteUInt32((uint)num, span, 0);
			}
		}
		if (instance.wireColor != 0)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.wireColor);
		}
		if (instance.lineAnchors != null)
		{
			for (int j = 0; j < instance.lineAnchors.Count; j++)
			{
				WireLineAnchorInfo instance3 = instance.lineAnchors[j];
				stream.WriteByte(50);
				BufferStream.RangeHandle range2 = stream.GetRange(5);
				int position2 = stream.Position;
				WireLineAnchorInfo.Serialize(stream, instance3);
				int val = stream.Position - position2;
				Span<byte> span2 = range2.GetSpan();
				int num2 = ProtocolParser.WriteUInt32((uint)val, span2, 0);
				if (num2 < 5)
				{
					span2[num2 - 1] |= 128;
					while (num2 < 4)
					{
						span2[num2++] = 128;
					}
					span2[4] = 0;
				}
			}
		}
		if (instance.slackLevels != null)
		{
			for (int k = 0; k < instance.slackLevels.Count; k++)
			{
				float f = instance.slackLevels[k];
				stream.WriteByte(61);
				ProtocolParser.WriteSingle(stream, f);
			}
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref entityId.Value);
		if (lineAnchors != null)
		{
			for (int i = 0; i < lineAnchors.Count; i++)
			{
				lineAnchors[i]?.InspectUids(action);
			}
		}
	}
}
