using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class HarborCrane : IDisposable, Pool.IPooled, IProto<HarborCrane>, IProto
{
	public class QueuedMove : IDisposable, Pool.IPooled, IProto<QueuedMove>, IProto
	{
		[NonSerialized]
		public NetworkableId targetEntity;

		[NonSerialized]
		public Vector3 targetWorldPosition;

		[NonSerialized]
		public Vector4 targetWorldRotation;

		[NonSerialized]
		public bool hasTarget;

		public bool ShouldPool = true;

		private bool _disposed;

		public static void ResetToPool(QueuedMove instance)
		{
			if (instance.ShouldPool)
			{
				instance.targetEntity = default(NetworkableId);
				instance.targetWorldPosition = default(Vector3);
				instance.targetWorldRotation = default(Vector4);
				instance.hasTarget = false;
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
				throw new Exception("Trying to dispose QueuedMove with ShouldPool set to false!");
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

		public void CopyTo(QueuedMove instance)
		{
			instance.targetEntity = targetEntity;
			instance.targetWorldPosition = targetWorldPosition;
			instance.targetWorldRotation = targetWorldRotation;
			instance.hasTarget = hasTarget;
		}

		public QueuedMove Copy()
		{
			QueuedMove queuedMove = Pool.Get<QueuedMove>();
			CopyTo(queuedMove);
			return queuedMove;
		}

		public static QueuedMove Deserialize(BufferStream stream)
		{
			QueuedMove queuedMove = Pool.Get<QueuedMove>();
			Deserialize(stream, queuedMove, isDelta: false);
			return queuedMove;
		}

		public static QueuedMove DeserializeLengthDelimited(BufferStream stream)
		{
			QueuedMove queuedMove = Pool.Get<QueuedMove>();
			DeserializeLengthDelimited(stream, queuedMove, isDelta: false);
			return queuedMove;
		}

		public static QueuedMove DeserializeLength(BufferStream stream, int length)
		{
			QueuedMove queuedMove = Pool.Get<QueuedMove>();
			DeserializeLength(stream, length, queuedMove, isDelta: false);
			return queuedMove;
		}

		public static QueuedMove Deserialize(byte[] buffer)
		{
			QueuedMove queuedMove = Pool.Get<QueuedMove>();
			using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
			Deserialize(stream, queuedMove, isDelta: false);
			return queuedMove;
		}

		public void FromProto(BufferStream stream, bool isDelta = false)
		{
			Deserialize(stream, this, isDelta);
		}

		public virtual void WriteToStream(BufferStream stream)
		{
			Serialize(stream, this);
		}

		public virtual void WriteToStreamDelta(BufferStream stream, QueuedMove previous)
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

		public static QueuedMove Deserialize(BufferStream stream, QueuedMove instance, bool isDelta)
		{
			while (true)
			{
				int num = stream.ReadByte();
				switch (num)
				{
				case 8:
					instance.targetEntity = new NetworkableId(ProtocolParser.ReadUInt64(stream));
					continue;
				case 18:
					Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.targetWorldPosition, isDelta);
					continue;
				case 26:
					Vector4Serialized.DeserializeLengthDelimited(stream, ref instance.targetWorldRotation, isDelta);
					continue;
				case 32:
					instance.hasTarget = ProtocolParser.ReadBool(stream);
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

		public static QueuedMove DeserializeLengthDelimited(BufferStream stream, QueuedMove instance, bool isDelta)
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
					instance.targetEntity = new NetworkableId(ProtocolParser.ReadUInt64(stream));
					continue;
				case 18:
					Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.targetWorldPosition, isDelta);
					continue;
				case 26:
					Vector4Serialized.DeserializeLengthDelimited(stream, ref instance.targetWorldRotation, isDelta);
					continue;
				case 32:
					instance.hasTarget = ProtocolParser.ReadBool(stream);
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

		public static QueuedMove DeserializeLength(BufferStream stream, int length, QueuedMove instance, bool isDelta)
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
					instance.targetEntity = new NetworkableId(ProtocolParser.ReadUInt64(stream));
					continue;
				case 18:
					Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.targetWorldPosition, isDelta);
					continue;
				case 26:
					Vector4Serialized.DeserializeLengthDelimited(stream, ref instance.targetWorldRotation, isDelta);
					continue;
				case 32:
					instance.hasTarget = ProtocolParser.ReadBool(stream);
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

		public static void SerializeDelta(BufferStream stream, QueuedMove instance, QueuedMove previous)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.targetEntity.Value);
			if (instance.targetWorldPosition != previous.targetWorldPosition)
			{
				stream.WriteByte(18);
				BufferStream.RangeHandle range = stream.GetRange(1);
				int position = stream.Position;
				Vector3Serialized.SerializeDelta(stream, instance.targetWorldPosition, previous.targetWorldPosition);
				int num = stream.Position - position;
				if (num > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field targetWorldPosition (UnityEngine.Vector3)");
				}
				Span<byte> span = range.GetSpan();
				ProtocolParser.WriteUInt32((uint)num, span, 0);
			}
			if (instance.targetWorldRotation != previous.targetWorldRotation)
			{
				stream.WriteByte(26);
				BufferStream.RangeHandle range2 = stream.GetRange(1);
				int position2 = stream.Position;
				Vector4Serialized.SerializeDelta(stream, instance.targetWorldRotation, previous.targetWorldRotation);
				int num2 = stream.Position - position2;
				if (num2 > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field targetWorldRotation (UnityEngine.Vector4)");
				}
				Span<byte> span2 = range2.GetSpan();
				ProtocolParser.WriteUInt32((uint)num2, span2, 0);
			}
			stream.WriteByte(32);
			ProtocolParser.WriteBool(stream, instance.hasTarget);
		}

		public static void Serialize(BufferStream stream, QueuedMove instance)
		{
			if (instance.targetEntity != default(NetworkableId))
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.targetEntity.Value);
			}
			if (instance.targetWorldPosition != default(Vector3))
			{
				stream.WriteByte(18);
				BufferStream.RangeHandle range = stream.GetRange(1);
				int position = stream.Position;
				Vector3Serialized.Serialize(stream, instance.targetWorldPosition);
				int num = stream.Position - position;
				if (num > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field targetWorldPosition (UnityEngine.Vector3)");
				}
				Span<byte> span = range.GetSpan();
				ProtocolParser.WriteUInt32((uint)num, span, 0);
			}
			if (instance.targetWorldRotation != default(Vector4))
			{
				stream.WriteByte(26);
				BufferStream.RangeHandle range2 = stream.GetRange(1);
				int position2 = stream.Position;
				Vector4Serialized.Serialize(stream, instance.targetWorldRotation);
				int num2 = stream.Position - position2;
				if (num2 > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field targetWorldRotation (UnityEngine.Vector4)");
				}
				Span<byte> span2 = range2.GetSpan();
				ProtocolParser.WriteUInt32((uint)num2, span2, 0);
			}
			if (instance.hasTarget)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.hasTarget);
			}
		}

		public void ToProto(BufferStream stream)
		{
			Serialize(stream, this);
		}

		public void InspectUids(UidInspector<ulong> action)
		{
			action(UidType.NetworkableId, ref targetEntity.Value);
		}
	}

	[NonSerialized]
	public float time;

	[NonSerialized]
	public float yaw;

	[NonSerialized]
	public float depth;

	[NonSerialized]
	public float height;

	[NonSerialized]
	public QueuedMove currentMove;

	[NonSerialized]
	public int currentPickupState;

	[NonSerialized]
	public Vector3 startForward;

	[NonSerialized]
	public float maxMoveHeight;

	[NonSerialized]
	public NetworkableId toParent;

	[NonSerialized]
	public List<QueuedMove> queuedMoves;

	[NonSerialized]
	public float moveDelay;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(HarborCrane instance)
	{
		if (!instance.ShouldPool)
		{
			return;
		}
		instance.time = 0f;
		instance.yaw = 0f;
		instance.depth = 0f;
		instance.height = 0f;
		if (instance.currentMove != null)
		{
			instance.currentMove.ResetToPool();
			instance.currentMove = null;
		}
		instance.currentPickupState = 0;
		instance.startForward = default(Vector3);
		instance.maxMoveHeight = 0f;
		instance.toParent = default(NetworkableId);
		if (instance.queuedMoves != null)
		{
			for (int i = 0; i < instance.queuedMoves.Count; i++)
			{
				if (instance.queuedMoves[i] != null)
				{
					instance.queuedMoves[i].ResetToPool();
					instance.queuedMoves[i] = null;
				}
			}
			List<QueuedMove> obj = instance.queuedMoves;
			Pool.Free(ref obj, freeElements: false);
			instance.queuedMoves = obj;
		}
		instance.moveDelay = 0f;
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
			throw new Exception("Trying to dispose HarborCrane with ShouldPool set to false!");
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

	public void CopyTo(HarborCrane instance)
	{
		instance.time = time;
		instance.yaw = yaw;
		instance.depth = depth;
		instance.height = height;
		if (currentMove != null)
		{
			if (instance.currentMove == null)
			{
				instance.currentMove = currentMove.Copy();
			}
			else
			{
				currentMove.CopyTo(instance.currentMove);
			}
		}
		else
		{
			instance.currentMove = null;
		}
		instance.currentPickupState = currentPickupState;
		instance.startForward = startForward;
		instance.maxMoveHeight = maxMoveHeight;
		instance.toParent = toParent;
		if (queuedMoves != null)
		{
			instance.queuedMoves = Pool.Get<List<QueuedMove>>();
			for (int i = 0; i < queuedMoves.Count; i++)
			{
				QueuedMove item = queuedMoves[i].Copy();
				instance.queuedMoves.Add(item);
			}
		}
		else
		{
			instance.queuedMoves = null;
		}
		instance.moveDelay = moveDelay;
	}

	public HarborCrane Copy()
	{
		HarborCrane harborCrane = Pool.Get<HarborCrane>();
		CopyTo(harborCrane);
		return harborCrane;
	}

	public static HarborCrane Deserialize(BufferStream stream)
	{
		HarborCrane harborCrane = Pool.Get<HarborCrane>();
		Deserialize(stream, harborCrane, isDelta: false);
		return harborCrane;
	}

	public static HarborCrane DeserializeLengthDelimited(BufferStream stream)
	{
		HarborCrane harborCrane = Pool.Get<HarborCrane>();
		DeserializeLengthDelimited(stream, harborCrane, isDelta: false);
		return harborCrane;
	}

	public static HarborCrane DeserializeLength(BufferStream stream, int length)
	{
		HarborCrane harborCrane = Pool.Get<HarborCrane>();
		DeserializeLength(stream, length, harborCrane, isDelta: false);
		return harborCrane;
	}

	public static HarborCrane Deserialize(byte[] buffer)
	{
		HarborCrane harborCrane = Pool.Get<HarborCrane>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, harborCrane, isDelta: false);
		return harborCrane;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, HarborCrane previous)
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

	public static HarborCrane Deserialize(BufferStream stream, HarborCrane instance, bool isDelta)
	{
		if (!isDelta && instance.queuedMoves == null)
		{
			instance.queuedMoves = Pool.Get<List<QueuedMove>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.time = ProtocolParser.ReadSingle(stream);
				break;
			case 21:
				instance.yaw = ProtocolParser.ReadSingle(stream);
				break;
			case 29:
				instance.depth = ProtocolParser.ReadSingle(stream);
				break;
			case 37:
				instance.height = ProtocolParser.ReadSingle(stream);
				break;
			case 42:
				if (instance.currentMove == null)
				{
					instance.currentMove = QueuedMove.DeserializeLengthDelimited(stream);
				}
				else
				{
					QueuedMove.DeserializeLengthDelimited(stream, instance.currentMove, isDelta);
				}
				break;
			case 48:
				instance.currentPickupState = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 58:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.startForward, isDelta);
				break;
			case 69:
				instance.maxMoveHeight = ProtocolParser.ReadSingle(stream);
				break;
			case 72:
				instance.toParent = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 90:
				instance.queuedMoves.Add(QueuedMove.DeserializeLengthDelimited(stream));
				break;
			case 101:
				instance.moveDelay = ProtocolParser.ReadSingle(stream);
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

	public static HarborCrane DeserializeLengthDelimited(BufferStream stream, HarborCrane instance, bool isDelta)
	{
		if (!isDelta && instance.queuedMoves == null)
		{
			instance.queuedMoves = Pool.Get<List<QueuedMove>>();
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
				break;
			case 21:
				instance.yaw = ProtocolParser.ReadSingle(stream);
				break;
			case 29:
				instance.depth = ProtocolParser.ReadSingle(stream);
				break;
			case 37:
				instance.height = ProtocolParser.ReadSingle(stream);
				break;
			case 42:
				if (instance.currentMove == null)
				{
					instance.currentMove = QueuedMove.DeserializeLengthDelimited(stream);
				}
				else
				{
					QueuedMove.DeserializeLengthDelimited(stream, instance.currentMove, isDelta);
				}
				break;
			case 48:
				instance.currentPickupState = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 58:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.startForward, isDelta);
				break;
			case 69:
				instance.maxMoveHeight = ProtocolParser.ReadSingle(stream);
				break;
			case 72:
				instance.toParent = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 90:
				instance.queuedMoves.Add(QueuedMove.DeserializeLengthDelimited(stream));
				break;
			case 101:
				instance.moveDelay = ProtocolParser.ReadSingle(stream);
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

	public static HarborCrane DeserializeLength(BufferStream stream, int length, HarborCrane instance, bool isDelta)
	{
		if (!isDelta && instance.queuedMoves == null)
		{
			instance.queuedMoves = Pool.Get<List<QueuedMove>>();
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
				break;
			case 21:
				instance.yaw = ProtocolParser.ReadSingle(stream);
				break;
			case 29:
				instance.depth = ProtocolParser.ReadSingle(stream);
				break;
			case 37:
				instance.height = ProtocolParser.ReadSingle(stream);
				break;
			case 42:
				if (instance.currentMove == null)
				{
					instance.currentMove = QueuedMove.DeserializeLengthDelimited(stream);
				}
				else
				{
					QueuedMove.DeserializeLengthDelimited(stream, instance.currentMove, isDelta);
				}
				break;
			case 48:
				instance.currentPickupState = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 58:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.startForward, isDelta);
				break;
			case 69:
				instance.maxMoveHeight = ProtocolParser.ReadSingle(stream);
				break;
			case 72:
				instance.toParent = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 90:
				instance.queuedMoves.Add(QueuedMove.DeserializeLengthDelimited(stream));
				break;
			case 101:
				instance.moveDelay = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, HarborCrane instance, HarborCrane previous)
	{
		if (instance.time != previous.time)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.time);
		}
		if (instance.yaw != previous.yaw)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.yaw);
		}
		if (instance.depth != previous.depth)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.depth);
		}
		if (instance.height != previous.height)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.height);
		}
		if (instance.currentMove != null)
		{
			stream.WriteByte(42);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			QueuedMove.SerializeDelta(stream, instance.currentMove, previous.currentMove);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field currentMove (ProtoBuf.HarborCrane.QueuedMove)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.currentPickupState != previous.currentPickupState)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.currentPickupState);
		}
		if (instance.startForward != previous.startForward)
		{
			stream.WriteByte(58);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.startForward, previous.startForward);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field startForward (UnityEngine.Vector3)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
		if (instance.maxMoveHeight != previous.maxMoveHeight)
		{
			stream.WriteByte(69);
			ProtocolParser.WriteSingle(stream, instance.maxMoveHeight);
		}
		stream.WriteByte(72);
		ProtocolParser.WriteUInt64(stream, instance.toParent.Value);
		if (instance.queuedMoves != null)
		{
			for (int i = 0; i < instance.queuedMoves.Count; i++)
			{
				QueuedMove queuedMove = instance.queuedMoves[i];
				stream.WriteByte(90);
				BufferStream.RangeHandle range3 = stream.GetRange(1);
				int position3 = stream.Position;
				QueuedMove.SerializeDelta(stream, queuedMove, queuedMove);
				int num3 = stream.Position - position3;
				if (num3 > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field queuedMoves (ProtoBuf.HarborCrane.QueuedMove)");
				}
				Span<byte> span3 = range3.GetSpan();
				ProtocolParser.WriteUInt32((uint)num3, span3, 0);
			}
		}
		if (instance.moveDelay != previous.moveDelay)
		{
			stream.WriteByte(101);
			ProtocolParser.WriteSingle(stream, instance.moveDelay);
		}
	}

	public static void Serialize(BufferStream stream, HarborCrane instance)
	{
		if (instance.time != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.time);
		}
		if (instance.yaw != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.yaw);
		}
		if (instance.depth != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.depth);
		}
		if (instance.height != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.height);
		}
		if (instance.currentMove != null)
		{
			stream.WriteByte(42);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			QueuedMove.Serialize(stream, instance.currentMove);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field currentMove (ProtoBuf.HarborCrane.QueuedMove)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.currentPickupState != 0)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.currentPickupState);
		}
		if (instance.startForward != default(Vector3))
		{
			stream.WriteByte(58);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			Vector3Serialized.Serialize(stream, instance.startForward);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field startForward (UnityEngine.Vector3)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
		if (instance.maxMoveHeight != 0f)
		{
			stream.WriteByte(69);
			ProtocolParser.WriteSingle(stream, instance.maxMoveHeight);
		}
		if (instance.toParent != default(NetworkableId))
		{
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, instance.toParent.Value);
		}
		if (instance.queuedMoves != null)
		{
			for (int i = 0; i < instance.queuedMoves.Count; i++)
			{
				QueuedMove instance2 = instance.queuedMoves[i];
				stream.WriteByte(90);
				BufferStream.RangeHandle range3 = stream.GetRange(1);
				int position3 = stream.Position;
				QueuedMove.Serialize(stream, instance2);
				int num3 = stream.Position - position3;
				if (num3 > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field queuedMoves (ProtoBuf.HarborCrane.QueuedMove)");
				}
				Span<byte> span3 = range3.GetSpan();
				ProtocolParser.WriteUInt32((uint)num3, span3, 0);
			}
		}
		if (instance.moveDelay != 0f)
		{
			stream.WriteByte(101);
			ProtocolParser.WriteSingle(stream, instance.moveDelay);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		currentMove?.InspectUids(action);
		action(UidType.NetworkableId, ref toParent.Value);
		if (queuedMoves != null)
		{
			for (int i = 0; i < queuedMoves.Count; i++)
			{
				queuedMoves[i]?.InspectUids(action);
			}
		}
	}
}
