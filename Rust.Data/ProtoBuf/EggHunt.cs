using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class EggHunt : IDisposable, Pool.IPooled, IProto<EggHunt>, IProto
{
	public class EggHunter : IDisposable, Pool.IPooled, IProto<EggHunter>, IProto
	{
		[NonSerialized]
		public string displayName;

		[NonSerialized]
		public int numEggs;

		[NonSerialized]
		public ulong playerID;

		public bool ShouldPool = true;

		private bool _disposed;

		public static void ResetToPool(EggHunter instance)
		{
			if (instance.ShouldPool)
			{
				instance.displayName = string.Empty;
				instance.numEggs = 0;
				instance.playerID = 0uL;
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
				throw new Exception("Trying to dispose EggHunter with ShouldPool set to false!");
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

		public void CopyTo(EggHunter instance)
		{
			instance.displayName = displayName;
			instance.numEggs = numEggs;
			instance.playerID = playerID;
		}

		public EggHunter Copy()
		{
			EggHunter eggHunter = Pool.Get<EggHunter>();
			CopyTo(eggHunter);
			return eggHunter;
		}

		public static EggHunter Deserialize(BufferStream stream)
		{
			EggHunter eggHunter = Pool.Get<EggHunter>();
			Deserialize(stream, eggHunter, isDelta: false);
			return eggHunter;
		}

		public static EggHunter DeserializeLengthDelimited(BufferStream stream)
		{
			EggHunter eggHunter = Pool.Get<EggHunter>();
			DeserializeLengthDelimited(stream, eggHunter, isDelta: false);
			return eggHunter;
		}

		public static EggHunter DeserializeLength(BufferStream stream, int length)
		{
			EggHunter eggHunter = Pool.Get<EggHunter>();
			DeserializeLength(stream, length, eggHunter, isDelta: false);
			return eggHunter;
		}

		public static EggHunter Deserialize(byte[] buffer)
		{
			EggHunter eggHunter = Pool.Get<EggHunter>();
			using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
			Deserialize(stream, eggHunter, isDelta: false);
			return eggHunter;
		}

		public void FromProto(BufferStream stream, bool isDelta = false)
		{
			Deserialize(stream, this, isDelta);
		}

		public virtual void WriteToStream(BufferStream stream)
		{
			Serialize(stream, this);
		}

		public virtual void WriteToStreamDelta(BufferStream stream, EggHunter previous)
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

		public static EggHunter Deserialize(BufferStream stream, EggHunter instance, bool isDelta)
		{
			while (true)
			{
				int num = stream.ReadByte();
				switch (num)
				{
				case 10:
					instance.displayName = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.numEggs = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.playerID = ProtocolParser.ReadUInt64(stream);
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

		public static EggHunter DeserializeLengthDelimited(BufferStream stream, EggHunter instance, bool isDelta)
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
				case 10:
					instance.displayName = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.numEggs = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.playerID = ProtocolParser.ReadUInt64(stream);
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

		public static EggHunter DeserializeLength(BufferStream stream, int length, EggHunter instance, bool isDelta)
		{
			long num = stream.Position + length;
			while (stream.Position < num)
			{
				int num2 = stream.ReadByte();
				switch (num2)
				{
				case -1:
					throw new EndOfStreamException();
				case 10:
					instance.displayName = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.numEggs = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.playerID = ProtocolParser.ReadUInt64(stream);
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

		public static void SerializeDelta(BufferStream stream, EggHunter instance, EggHunter previous)
		{
			if (instance.displayName != null && instance.displayName != previous.displayName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteString(stream, instance.displayName);
			}
			if (instance.numEggs != previous.numEggs)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.numEggs);
			}
			if (instance.playerID != previous.playerID)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.playerID);
			}
		}

		public static void Serialize(BufferStream stream, EggHunter instance)
		{
			if (instance.displayName != null)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteString(stream, instance.displayName);
			}
			if (instance.numEggs != 0)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.numEggs);
			}
			if (instance.playerID != 0L)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.playerID);
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

	[NonSerialized]
	public List<EggHunter> hunters;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(EggHunt instance)
	{
		if (!instance.ShouldPool)
		{
			return;
		}
		if (instance.hunters != null)
		{
			for (int i = 0; i < instance.hunters.Count; i++)
			{
				if (instance.hunters[i] != null)
				{
					instance.hunters[i].ResetToPool();
					instance.hunters[i] = null;
				}
			}
			List<EggHunter> obj = instance.hunters;
			Pool.Free(ref obj, freeElements: false);
			instance.hunters = obj;
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
			throw new Exception("Trying to dispose EggHunt with ShouldPool set to false!");
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

	public void CopyTo(EggHunt instance)
	{
		if (hunters != null)
		{
			instance.hunters = Pool.Get<List<EggHunter>>();
			for (int i = 0; i < hunters.Count; i++)
			{
				EggHunter item = hunters[i].Copy();
				instance.hunters.Add(item);
			}
		}
		else
		{
			instance.hunters = null;
		}
	}

	public EggHunt Copy()
	{
		EggHunt eggHunt = Pool.Get<EggHunt>();
		CopyTo(eggHunt);
		return eggHunt;
	}

	public static EggHunt Deserialize(BufferStream stream)
	{
		EggHunt eggHunt = Pool.Get<EggHunt>();
		Deserialize(stream, eggHunt, isDelta: false);
		return eggHunt;
	}

	public static EggHunt DeserializeLengthDelimited(BufferStream stream)
	{
		EggHunt eggHunt = Pool.Get<EggHunt>();
		DeserializeLengthDelimited(stream, eggHunt, isDelta: false);
		return eggHunt;
	}

	public static EggHunt DeserializeLength(BufferStream stream, int length)
	{
		EggHunt eggHunt = Pool.Get<EggHunt>();
		DeserializeLength(stream, length, eggHunt, isDelta: false);
		return eggHunt;
	}

	public static EggHunt Deserialize(byte[] buffer)
	{
		EggHunt eggHunt = Pool.Get<EggHunt>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, eggHunt, isDelta: false);
		return eggHunt;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, EggHunt previous)
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

	public static EggHunt Deserialize(BufferStream stream, EggHunt instance, bool isDelta)
	{
		if (!isDelta && instance.hunters == null)
		{
			instance.hunters = Pool.Get<List<EggHunter>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				instance.hunters.Add(EggHunter.DeserializeLengthDelimited(stream));
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

	public static EggHunt DeserializeLengthDelimited(BufferStream stream, EggHunt instance, bool isDelta)
	{
		if (!isDelta && instance.hunters == null)
		{
			instance.hunters = Pool.Get<List<EggHunter>>();
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
				instance.hunters.Add(EggHunter.DeserializeLengthDelimited(stream));
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

	public static EggHunt DeserializeLength(BufferStream stream, int length, EggHunt instance, bool isDelta)
	{
		if (!isDelta && instance.hunters == null)
		{
			instance.hunters = Pool.Get<List<EggHunter>>();
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
				instance.hunters.Add(EggHunter.DeserializeLengthDelimited(stream));
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

	public static void SerializeDelta(BufferStream stream, EggHunt instance, EggHunt previous)
	{
		if (instance.hunters == null)
		{
			return;
		}
		for (int i = 0; i < instance.hunters.Count; i++)
		{
			EggHunter eggHunter = instance.hunters[i];
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(5);
			int position = stream.Position;
			EggHunter.SerializeDelta(stream, eggHunter, eggHunter);
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

	public static void Serialize(BufferStream stream, EggHunt instance)
	{
		if (instance.hunters == null)
		{
			return;
		}
		for (int i = 0; i < instance.hunters.Count; i++)
		{
			EggHunter instance2 = instance.hunters[i];
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(5);
			int position = stream.Position;
			EggHunter.Serialize(stream, instance2);
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

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		if (hunters != null)
		{
			for (int i = 0; i < hunters.Count; i++)
			{
				hunters[i]?.InspectUids(action);
			}
		}
	}
}
