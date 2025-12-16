using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class MissionMapMarker : IDisposable, Pool.IPooled, IProto<MissionMapMarker>, IProto
{
	[NonSerialized]
	public List<uint> missionIds;

	[NonSerialized]
	public string providerToken;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(MissionMapMarker instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.missionIds != null)
			{
				List<uint> obj = instance.missionIds;
				Pool.FreeUnmanaged(ref obj);
				instance.missionIds = obj;
			}
			instance.providerToken = string.Empty;
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
			throw new Exception("Trying to dispose MissionMapMarker with ShouldPool set to false!");
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

	public void CopyTo(MissionMapMarker instance)
	{
		if (missionIds != null)
		{
			instance.missionIds = Pool.Get<List<uint>>();
			for (int i = 0; i < missionIds.Count; i++)
			{
				uint item = missionIds[i];
				instance.missionIds.Add(item);
			}
		}
		else
		{
			instance.missionIds = null;
		}
		instance.providerToken = providerToken;
	}

	public MissionMapMarker Copy()
	{
		MissionMapMarker missionMapMarker = Pool.Get<MissionMapMarker>();
		CopyTo(missionMapMarker);
		return missionMapMarker;
	}

	public static MissionMapMarker Deserialize(BufferStream stream)
	{
		MissionMapMarker missionMapMarker = Pool.Get<MissionMapMarker>();
		Deserialize(stream, missionMapMarker, isDelta: false);
		return missionMapMarker;
	}

	public static MissionMapMarker DeserializeLengthDelimited(BufferStream stream)
	{
		MissionMapMarker missionMapMarker = Pool.Get<MissionMapMarker>();
		DeserializeLengthDelimited(stream, missionMapMarker, isDelta: false);
		return missionMapMarker;
	}

	public static MissionMapMarker DeserializeLength(BufferStream stream, int length)
	{
		MissionMapMarker missionMapMarker = Pool.Get<MissionMapMarker>();
		DeserializeLength(stream, length, missionMapMarker, isDelta: false);
		return missionMapMarker;
	}

	public static MissionMapMarker Deserialize(byte[] buffer)
	{
		MissionMapMarker missionMapMarker = Pool.Get<MissionMapMarker>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, missionMapMarker, isDelta: false);
		return missionMapMarker;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, MissionMapMarker previous)
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

	public static MissionMapMarker Deserialize(BufferStream stream, MissionMapMarker instance, bool isDelta)
	{
		if (!isDelta && instance.missionIds == null)
		{
			instance.missionIds = Pool.Get<List<uint>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.missionIds.Add(ProtocolParser.ReadUInt32(stream));
				continue;
			case 18:
				instance.providerToken = ProtocolParser.ReadString(stream);
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

	public static MissionMapMarker DeserializeLengthDelimited(BufferStream stream, MissionMapMarker instance, bool isDelta)
	{
		if (!isDelta && instance.missionIds == null)
		{
			instance.missionIds = Pool.Get<List<uint>>();
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
				instance.missionIds.Add(ProtocolParser.ReadUInt32(stream));
				continue;
			case 18:
				instance.providerToken = ProtocolParser.ReadString(stream);
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

	public static MissionMapMarker DeserializeLength(BufferStream stream, int length, MissionMapMarker instance, bool isDelta)
	{
		if (!isDelta && instance.missionIds == null)
		{
			instance.missionIds = Pool.Get<List<uint>>();
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
				instance.missionIds.Add(ProtocolParser.ReadUInt32(stream));
				continue;
			case 18:
				instance.providerToken = ProtocolParser.ReadString(stream);
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

	public static void SerializeDelta(BufferStream stream, MissionMapMarker instance, MissionMapMarker previous)
	{
		if (instance.missionIds != null)
		{
			for (int i = 0; i < instance.missionIds.Count; i++)
			{
				uint val = instance.missionIds[i];
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, val);
			}
		}
		if (instance.providerToken != null && instance.providerToken != previous.providerToken)
		{
			stream.WriteByte(18);
			ProtocolParser.WriteString(stream, instance.providerToken);
		}
	}

	public static void Serialize(BufferStream stream, MissionMapMarker instance)
	{
		if (instance.missionIds != null)
		{
			for (int i = 0; i < instance.missionIds.Count; i++)
			{
				uint val = instance.missionIds[i];
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, val);
			}
		}
		if (instance.providerToken != null)
		{
			stream.WriteByte(18);
			ProtocolParser.WriteString(stream, instance.providerToken);
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
