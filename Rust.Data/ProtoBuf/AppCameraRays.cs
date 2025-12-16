using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class AppCameraRays : IDisposable, Pool.IPooled, IProto<AppCameraRays>, IProto
{
	public enum EntityType
	{
		Tree = 1,
		Player
	}

	public class Entity : IDisposable, Pool.IPooled, IProto<Entity>, IProto
	{
		[NonSerialized]
		public NetworkableId entityId;

		[NonSerialized]
		public EntityType type;

		[NonSerialized]
		public Vector3 position;

		[NonSerialized]
		public Vector3 rotation;

		[NonSerialized]
		public Vector3 size;

		[NonSerialized]
		public string name;

		public bool ShouldPool = true;

		private bool _disposed;

		public static void ResetToPool(Entity instance)
		{
			if (instance.ShouldPool)
			{
				instance.entityId = default(NetworkableId);
				instance.type = (EntityType)0;
				instance.position = default(Vector3);
				instance.rotation = default(Vector3);
				instance.size = default(Vector3);
				instance.name = string.Empty;
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
				throw new Exception("Trying to dispose Entity with ShouldPool set to false!");
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

		public void CopyTo(Entity instance)
		{
			instance.entityId = entityId;
			instance.type = type;
			instance.position = position;
			instance.rotation = rotation;
			instance.size = size;
			instance.name = name;
		}

		public Entity Copy()
		{
			Entity entity = Pool.Get<Entity>();
			CopyTo(entity);
			return entity;
		}

		public static Entity Deserialize(BufferStream stream)
		{
			Entity entity = Pool.Get<Entity>();
			Deserialize(stream, entity, isDelta: false);
			return entity;
		}

		public static Entity DeserializeLengthDelimited(BufferStream stream)
		{
			Entity entity = Pool.Get<Entity>();
			DeserializeLengthDelimited(stream, entity, isDelta: false);
			return entity;
		}

		public static Entity DeserializeLength(BufferStream stream, int length)
		{
			Entity entity = Pool.Get<Entity>();
			DeserializeLength(stream, length, entity, isDelta: false);
			return entity;
		}

		public static Entity Deserialize(byte[] buffer)
		{
			Entity entity = Pool.Get<Entity>();
			using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
			Deserialize(stream, entity, isDelta: false);
			return entity;
		}

		public void FromProto(BufferStream stream, bool isDelta = false)
		{
			Deserialize(stream, this, isDelta);
		}

		public virtual void WriteToStream(BufferStream stream)
		{
			Serialize(stream, this);
		}

		public virtual void WriteToStreamDelta(BufferStream stream, Entity previous)
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

		public static Entity Deserialize(BufferStream stream, Entity instance, bool isDelta)
		{
			while (true)
			{
				int num = stream.ReadByte();
				switch (num)
				{
				case 8:
					instance.entityId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
					continue;
				case 16:
					instance.type = (EntityType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.position, isDelta);
					continue;
				case 34:
					Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.rotation, isDelta);
					continue;
				case 42:
					Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.size, isDelta);
					continue;
				case 50:
					instance.name = ProtocolParser.ReadString(stream);
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

		public static Entity DeserializeLengthDelimited(BufferStream stream, Entity instance, bool isDelta)
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
					instance.entityId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
					continue;
				case 16:
					instance.type = (EntityType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.position, isDelta);
					continue;
				case 34:
					Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.rotation, isDelta);
					continue;
				case 42:
					Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.size, isDelta);
					continue;
				case 50:
					instance.name = ProtocolParser.ReadString(stream);
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

		public static Entity DeserializeLength(BufferStream stream, int length, Entity instance, bool isDelta)
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
					instance.entityId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
					continue;
				case 16:
					instance.type = (EntityType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.position, isDelta);
					continue;
				case 34:
					Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.rotation, isDelta);
					continue;
				case 42:
					Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.size, isDelta);
					continue;
				case 50:
					instance.name = ProtocolParser.ReadString(stream);
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

		public static void SerializeDelta(BufferStream stream, Entity instance, Entity previous)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.entityId.Value);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.type);
			if (instance.position != previous.position)
			{
				stream.WriteByte(26);
				BufferStream.RangeHandle range = stream.GetRange(1);
				int num = stream.Position;
				Vector3Serialized.SerializeDelta(stream, instance.position, previous.position);
				int num2 = stream.Position - num;
				if (num2 > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field position (UnityEngine.Vector3)");
				}
				Span<byte> span = range.GetSpan();
				ProtocolParser.WriteUInt32((uint)num2, span, 0);
			}
			if (instance.rotation != previous.rotation)
			{
				stream.WriteByte(34);
				BufferStream.RangeHandle range2 = stream.GetRange(1);
				int num3 = stream.Position;
				Vector3Serialized.SerializeDelta(stream, instance.rotation, previous.rotation);
				int num4 = stream.Position - num3;
				if (num4 > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field rotation (UnityEngine.Vector3)");
				}
				Span<byte> span2 = range2.GetSpan();
				ProtocolParser.WriteUInt32((uint)num4, span2, 0);
			}
			if (instance.size != previous.size)
			{
				stream.WriteByte(42);
				BufferStream.RangeHandle range3 = stream.GetRange(1);
				int num5 = stream.Position;
				Vector3Serialized.SerializeDelta(stream, instance.size, previous.size);
				int num6 = stream.Position - num5;
				if (num6 > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field size (UnityEngine.Vector3)");
				}
				Span<byte> span3 = range3.GetSpan();
				ProtocolParser.WriteUInt32((uint)num6, span3, 0);
			}
			if (instance.name != null && instance.name != previous.name)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteString(stream, instance.name);
			}
		}

		public static void Serialize(BufferStream stream, Entity instance)
		{
			if (instance.entityId != default(NetworkableId))
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.entityId.Value);
			}
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.type);
			if (instance.position != default(Vector3))
			{
				stream.WriteByte(26);
				BufferStream.RangeHandle range = stream.GetRange(1);
				int num = stream.Position;
				Vector3Serialized.Serialize(stream, instance.position);
				int num2 = stream.Position - num;
				if (num2 > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field position (UnityEngine.Vector3)");
				}
				Span<byte> span = range.GetSpan();
				ProtocolParser.WriteUInt32((uint)num2, span, 0);
			}
			if (instance.rotation != default(Vector3))
			{
				stream.WriteByte(34);
				BufferStream.RangeHandle range2 = stream.GetRange(1);
				int num3 = stream.Position;
				Vector3Serialized.Serialize(stream, instance.rotation);
				int num4 = stream.Position - num3;
				if (num4 > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field rotation (UnityEngine.Vector3)");
				}
				Span<byte> span2 = range2.GetSpan();
				ProtocolParser.WriteUInt32((uint)num4, span2, 0);
			}
			if (instance.size != default(Vector3))
			{
				stream.WriteByte(42);
				BufferStream.RangeHandle range3 = stream.GetRange(1);
				int num5 = stream.Position;
				Vector3Serialized.Serialize(stream, instance.size);
				int num6 = stream.Position - num5;
				if (num6 > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field size (UnityEngine.Vector3)");
				}
				Span<byte> span3 = range3.GetSpan();
				ProtocolParser.WriteUInt32((uint)num6, span3, 0);
			}
			if (instance.name != null)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteString(stream, instance.name);
			}
		}

		public void ToProto(BufferStream stream)
		{
			Serialize(stream, this);
		}

		public void InspectUids(UidInspector<ulong> action)
		{
			action(UidType.NetworkableId, ref entityId.Value);
		}
	}

	[NonSerialized]
	public float verticalFov;

	[NonSerialized]
	public int sampleOffset;

	[NonSerialized]
	public ArraySegment<byte> rayData;

	[NonSerialized]
	public float distance;

	[NonSerialized]
	public List<Entity> entities;

	[NonSerialized]
	public float timeOfDay;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(AppCameraRays instance)
	{
		if (!instance.ShouldPool)
		{
			return;
		}
		instance.verticalFov = 0f;
		instance.sampleOffset = 0;
		if (instance.rayData.Array != null)
		{
			BufferStream.Shared.ArrayPool.Return(instance.rayData.Array);
		}
		instance.rayData = default(ArraySegment<byte>);
		instance.distance = 0f;
		if (instance.entities != null)
		{
			for (int i = 0; i < instance.entities.Count; i++)
			{
				if (instance.entities[i] != null)
				{
					instance.entities[i].ResetToPool();
					instance.entities[i] = null;
				}
			}
			List<Entity> obj = instance.entities;
			Pool.Free(ref obj, freeElements: false);
			instance.entities = obj;
		}
		instance.timeOfDay = 0f;
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
			throw new Exception("Trying to dispose AppCameraRays with ShouldPool set to false!");
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

	public void CopyTo(AppCameraRays instance)
	{
		instance.verticalFov = verticalFov;
		instance.sampleOffset = sampleOffset;
		if (rayData.Array == null)
		{
			instance.rayData = default(ArraySegment<byte>);
		}
		else
		{
			byte[] array = BufferStream.Shared.ArrayPool.Rent(rayData.Count);
			Array.Copy(rayData.Array, 0, array, 0, rayData.Count);
			instance.rayData = new ArraySegment<byte>(array, 0, rayData.Count);
		}
		instance.distance = distance;
		if (entities != null)
		{
			instance.entities = Pool.Get<List<Entity>>();
			for (int i = 0; i < entities.Count; i++)
			{
				Entity item = entities[i].Copy();
				instance.entities.Add(item);
			}
		}
		else
		{
			instance.entities = null;
		}
		instance.timeOfDay = timeOfDay;
	}

	public AppCameraRays Copy()
	{
		AppCameraRays appCameraRays = Pool.Get<AppCameraRays>();
		CopyTo(appCameraRays);
		return appCameraRays;
	}

	public static AppCameraRays Deserialize(BufferStream stream)
	{
		AppCameraRays appCameraRays = Pool.Get<AppCameraRays>();
		Deserialize(stream, appCameraRays, isDelta: false);
		return appCameraRays;
	}

	public static AppCameraRays DeserializeLengthDelimited(BufferStream stream)
	{
		AppCameraRays appCameraRays = Pool.Get<AppCameraRays>();
		DeserializeLengthDelimited(stream, appCameraRays, isDelta: false);
		return appCameraRays;
	}

	public static AppCameraRays DeserializeLength(BufferStream stream, int length)
	{
		AppCameraRays appCameraRays = Pool.Get<AppCameraRays>();
		DeserializeLength(stream, length, appCameraRays, isDelta: false);
		return appCameraRays;
	}

	public static AppCameraRays Deserialize(byte[] buffer)
	{
		AppCameraRays appCameraRays = Pool.Get<AppCameraRays>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, appCameraRays, isDelta: false);
		return appCameraRays;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, AppCameraRays previous)
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

	public static AppCameraRays Deserialize(BufferStream stream, AppCameraRays instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.verticalFov = 0f;
			instance.sampleOffset = 0;
			instance.distance = 0f;
			if (instance.entities == null)
			{
				instance.entities = Pool.Get<List<Entity>>();
			}
			instance.timeOfDay = 1f;
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.verticalFov = ProtocolParser.ReadSingle(stream);
				continue;
			case 16:
				instance.sampleOffset = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 26:
				instance.rayData = ProtocolParser.ReadPooledBytes(stream);
				continue;
			case 37:
				instance.distance = ProtocolParser.ReadSingle(stream);
				continue;
			case 42:
				instance.entities.Add(Entity.DeserializeLengthDelimited(stream));
				continue;
			case 53:
				instance.timeOfDay = ProtocolParser.ReadSingle(stream);
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

	public static AppCameraRays DeserializeLengthDelimited(BufferStream stream, AppCameraRays instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.verticalFov = 0f;
			instance.sampleOffset = 0;
			instance.distance = 0f;
			if (instance.entities == null)
			{
				instance.entities = Pool.Get<List<Entity>>();
			}
			instance.timeOfDay = 1f;
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
				instance.verticalFov = ProtocolParser.ReadSingle(stream);
				continue;
			case 16:
				instance.sampleOffset = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 26:
				instance.rayData = ProtocolParser.ReadPooledBytes(stream);
				continue;
			case 37:
				instance.distance = ProtocolParser.ReadSingle(stream);
				continue;
			case 42:
				instance.entities.Add(Entity.DeserializeLengthDelimited(stream));
				continue;
			case 53:
				instance.timeOfDay = ProtocolParser.ReadSingle(stream);
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

	public static AppCameraRays DeserializeLength(BufferStream stream, int length, AppCameraRays instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.verticalFov = 0f;
			instance.sampleOffset = 0;
			instance.distance = 0f;
			if (instance.entities == null)
			{
				instance.entities = Pool.Get<List<Entity>>();
			}
			instance.timeOfDay = 1f;
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
				instance.verticalFov = ProtocolParser.ReadSingle(stream);
				continue;
			case 16:
				instance.sampleOffset = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 26:
				instance.rayData = ProtocolParser.ReadPooledBytes(stream);
				continue;
			case 37:
				instance.distance = ProtocolParser.ReadSingle(stream);
				continue;
			case 42:
				instance.entities.Add(Entity.DeserializeLengthDelimited(stream));
				continue;
			case 53:
				instance.timeOfDay = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, AppCameraRays instance, AppCameraRays previous)
	{
		if (instance.verticalFov != previous.verticalFov)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.verticalFov);
		}
		if (instance.sampleOffset != previous.sampleOffset)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.sampleOffset);
		}
		if (instance.rayData.Array == null)
		{
			throw new ArgumentNullException("rayData", "Required by proto specification.");
		}
		stream.WriteByte(26);
		ProtocolParser.WritePooledBytes(stream, instance.rayData);
		if (instance.distance != previous.distance)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.distance);
		}
		if (instance.entities != null)
		{
			for (int i = 0; i < instance.entities.Count; i++)
			{
				Entity entity = instance.entities[i];
				stream.WriteByte(42);
				BufferStream.RangeHandle range = stream.GetRange(5);
				int position = stream.Position;
				Entity.SerializeDelta(stream, entity, entity);
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
		if (instance.timeOfDay != previous.timeOfDay)
		{
			stream.WriteByte(53);
			ProtocolParser.WriteSingle(stream, instance.timeOfDay);
		}
	}

	public static void Serialize(BufferStream stream, AppCameraRays instance)
	{
		if (instance.verticalFov != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.verticalFov);
		}
		if (instance.sampleOffset != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.sampleOffset);
		}
		if (instance.rayData.Array == null)
		{
			throw new ArgumentNullException("rayData", "Required by proto specification.");
		}
		stream.WriteByte(26);
		ProtocolParser.WritePooledBytes(stream, instance.rayData);
		if (instance.distance != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.distance);
		}
		if (instance.entities != null)
		{
			for (int i = 0; i < instance.entities.Count; i++)
			{
				Entity instance2 = instance.entities[i];
				stream.WriteByte(42);
				BufferStream.RangeHandle range = stream.GetRange(5);
				int position = stream.Position;
				Entity.Serialize(stream, instance2);
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
		if (instance.timeOfDay != 1f)
		{
			stream.WriteByte(53);
			ProtocolParser.WriteSingle(stream, instance.timeOfDay);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		if (entities != null)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i]?.InspectUids(action);
			}
		}
	}
}
