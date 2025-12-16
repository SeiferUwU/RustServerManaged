using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class PartyData : IDisposable, Pool.IPooled, IProto<PartyData>, IProto
{
	[NonSerialized]
	public ulong lobbyId;

	[NonSerialized]
	public List<PartyMemberData> members;

	[NonSerialized]
	public string joinKey;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(PartyData instance)
	{
		if (!instance.ShouldPool)
		{
			return;
		}
		instance.lobbyId = 0uL;
		if (instance.members != null)
		{
			for (int i = 0; i < instance.members.Count; i++)
			{
				if (instance.members[i] != null)
				{
					instance.members[i].ResetToPool();
					instance.members[i] = null;
				}
			}
			List<PartyMemberData> obj = instance.members;
			Pool.Free(ref obj, freeElements: false);
			instance.members = obj;
		}
		instance.joinKey = string.Empty;
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
			throw new Exception("Trying to dispose PartyData with ShouldPool set to false!");
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

	public void CopyTo(PartyData instance)
	{
		instance.lobbyId = lobbyId;
		if (members != null)
		{
			instance.members = Pool.Get<List<PartyMemberData>>();
			for (int i = 0; i < members.Count; i++)
			{
				PartyMemberData item = members[i].Copy();
				instance.members.Add(item);
			}
		}
		else
		{
			instance.members = null;
		}
		instance.joinKey = joinKey;
	}

	public PartyData Copy()
	{
		PartyData partyData = Pool.Get<PartyData>();
		CopyTo(partyData);
		return partyData;
	}

	public static PartyData Deserialize(BufferStream stream)
	{
		PartyData partyData = Pool.Get<PartyData>();
		Deserialize(stream, partyData, isDelta: false);
		return partyData;
	}

	public static PartyData DeserializeLengthDelimited(BufferStream stream)
	{
		PartyData partyData = Pool.Get<PartyData>();
		DeserializeLengthDelimited(stream, partyData, isDelta: false);
		return partyData;
	}

	public static PartyData DeserializeLength(BufferStream stream, int length)
	{
		PartyData partyData = Pool.Get<PartyData>();
		DeserializeLength(stream, length, partyData, isDelta: false);
		return partyData;
	}

	public static PartyData Deserialize(byte[] buffer)
	{
		PartyData partyData = Pool.Get<PartyData>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, partyData, isDelta: false);
		return partyData;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, PartyData previous)
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

	public static PartyData Deserialize(BufferStream stream, PartyData instance, bool isDelta)
	{
		if (!isDelta && instance.members == null)
		{
			instance.members = Pool.Get<List<PartyMemberData>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.lobbyId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 18:
				instance.members.Add(PartyMemberData.DeserializeLengthDelimited(stream));
				continue;
			case 26:
				instance.joinKey = ProtocolParser.ReadString(stream);
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

	public static PartyData DeserializeLengthDelimited(BufferStream stream, PartyData instance, bool isDelta)
	{
		if (!isDelta && instance.members == null)
		{
			instance.members = Pool.Get<List<PartyMemberData>>();
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
				instance.lobbyId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 18:
				instance.members.Add(PartyMemberData.DeserializeLengthDelimited(stream));
				continue;
			case 26:
				instance.joinKey = ProtocolParser.ReadString(stream);
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

	public static PartyData DeserializeLength(BufferStream stream, int length, PartyData instance, bool isDelta)
	{
		if (!isDelta && instance.members == null)
		{
			instance.members = Pool.Get<List<PartyMemberData>>();
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
				instance.lobbyId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 18:
				instance.members.Add(PartyMemberData.DeserializeLengthDelimited(stream));
				continue;
			case 26:
				instance.joinKey = ProtocolParser.ReadString(stream);
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

	public static void SerializeDelta(BufferStream stream, PartyData instance, PartyData previous)
	{
		if (instance.lobbyId != previous.lobbyId)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.lobbyId);
		}
		if (instance.members != null)
		{
			for (int i = 0; i < instance.members.Count; i++)
			{
				PartyMemberData partyMemberData = instance.members[i];
				stream.WriteByte(18);
				BufferStream.RangeHandle range = stream.GetRange(5);
				int position = stream.Position;
				PartyMemberData.SerializeDelta(stream, partyMemberData, partyMemberData);
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
		if (instance.joinKey != previous.joinKey)
		{
			if (instance.joinKey == null)
			{
				throw new ArgumentNullException("joinKey", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteString(stream, instance.joinKey);
		}
	}

	public static void Serialize(BufferStream stream, PartyData instance)
	{
		if (instance.lobbyId != 0L)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.lobbyId);
		}
		if (instance.members != null)
		{
			for (int i = 0; i < instance.members.Count; i++)
			{
				PartyMemberData instance2 = instance.members[i];
				stream.WriteByte(18);
				BufferStream.RangeHandle range = stream.GetRange(5);
				int position = stream.Position;
				PartyMemberData.Serialize(stream, instance2);
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
		if (instance.joinKey == null)
		{
			throw new ArgumentNullException("joinKey", "Required by proto specification.");
		}
		stream.WriteByte(26);
		ProtocolParser.WriteString(stream, instance.joinKey);
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		if (members != null)
		{
			for (int i = 0; i < members.Count; i++)
			{
				members[i]?.InspectUids(action);
			}
		}
	}
}
