using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class RadioList : IDisposable, Pool.IPooled, IProto<RadioList>, IProto
{
	[NonSerialized]
	public List<string> stationUrls;

	[NonSerialized]
	public List<string> stationNames;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(RadioList instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.stationUrls != null)
			{
				List<string> obj = instance.stationUrls;
				Pool.FreeUnmanaged(ref obj);
				instance.stationUrls = obj;
			}
			if (instance.stationNames != null)
			{
				List<string> obj2 = instance.stationNames;
				Pool.FreeUnmanaged(ref obj2);
				instance.stationNames = obj2;
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
			throw new Exception("Trying to dispose RadioList with ShouldPool set to false!");
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

	public void CopyTo(RadioList instance)
	{
		if (stationUrls != null)
		{
			instance.stationUrls = Pool.Get<List<string>>();
			for (int i = 0; i < stationUrls.Count; i++)
			{
				string item = stationUrls[i];
				instance.stationUrls.Add(item);
			}
		}
		else
		{
			instance.stationUrls = null;
		}
		if (stationNames != null)
		{
			instance.stationNames = Pool.Get<List<string>>();
			for (int j = 0; j < stationNames.Count; j++)
			{
				string item2 = stationNames[j];
				instance.stationNames.Add(item2);
			}
		}
		else
		{
			instance.stationNames = null;
		}
	}

	public RadioList Copy()
	{
		RadioList radioList = Pool.Get<RadioList>();
		CopyTo(radioList);
		return radioList;
	}

	public static RadioList Deserialize(BufferStream stream)
	{
		RadioList radioList = Pool.Get<RadioList>();
		Deserialize(stream, radioList, isDelta: false);
		return radioList;
	}

	public static RadioList DeserializeLengthDelimited(BufferStream stream)
	{
		RadioList radioList = Pool.Get<RadioList>();
		DeserializeLengthDelimited(stream, radioList, isDelta: false);
		return radioList;
	}

	public static RadioList DeserializeLength(BufferStream stream, int length)
	{
		RadioList radioList = Pool.Get<RadioList>();
		DeserializeLength(stream, length, radioList, isDelta: false);
		return radioList;
	}

	public static RadioList Deserialize(byte[] buffer)
	{
		RadioList radioList = Pool.Get<RadioList>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, radioList, isDelta: false);
		return radioList;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, RadioList previous)
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

	public static RadioList Deserialize(BufferStream stream, RadioList instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.stationUrls == null)
			{
				instance.stationUrls = Pool.Get<List<string>>();
			}
			if (instance.stationNames == null)
			{
				instance.stationNames = Pool.Get<List<string>>();
			}
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				instance.stationUrls.Add(ProtocolParser.ReadString(stream));
				continue;
			case 18:
				instance.stationNames.Add(ProtocolParser.ReadString(stream));
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

	public static RadioList DeserializeLengthDelimited(BufferStream stream, RadioList instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.stationUrls == null)
			{
				instance.stationUrls = Pool.Get<List<string>>();
			}
			if (instance.stationNames == null)
			{
				instance.stationNames = Pool.Get<List<string>>();
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
				instance.stationUrls.Add(ProtocolParser.ReadString(stream));
				continue;
			case 18:
				instance.stationNames.Add(ProtocolParser.ReadString(stream));
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

	public static RadioList DeserializeLength(BufferStream stream, int length, RadioList instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.stationUrls == null)
			{
				instance.stationUrls = Pool.Get<List<string>>();
			}
			if (instance.stationNames == null)
			{
				instance.stationNames = Pool.Get<List<string>>();
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
				instance.stationUrls.Add(ProtocolParser.ReadString(stream));
				continue;
			case 18:
				instance.stationNames.Add(ProtocolParser.ReadString(stream));
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

	public static void SerializeDelta(BufferStream stream, RadioList instance, RadioList previous)
	{
		if (instance.stationUrls != null)
		{
			for (int i = 0; i < instance.stationUrls.Count; i++)
			{
				string val = instance.stationUrls[i];
				stream.WriteByte(10);
				ProtocolParser.WriteString(stream, val);
			}
		}
		if (instance.stationNames != null)
		{
			for (int j = 0; j < instance.stationNames.Count; j++)
			{
				string val2 = instance.stationNames[j];
				stream.WriteByte(18);
				ProtocolParser.WriteString(stream, val2);
			}
		}
	}

	public static void Serialize(BufferStream stream, RadioList instance)
	{
		if (instance.stationUrls != null)
		{
			for (int i = 0; i < instance.stationUrls.Count; i++)
			{
				string val = instance.stationUrls[i];
				stream.WriteByte(10);
				ProtocolParser.WriteString(stream, val);
			}
		}
		if (instance.stationNames != null)
		{
			for (int j = 0; j < instance.stationNames.Count; j++)
			{
				string val2 = instance.stationNames[j];
				stream.WriteByte(18);
				ProtocolParser.WriteString(stream, val2);
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
