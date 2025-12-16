using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class AppClanChanged : IDisposable, Pool.IPooled, IProto<AppClanChanged>, IProto
{
	[NonSerialized]
	public ClanInfo clanInfo;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(AppClanChanged instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.clanInfo != null)
			{
				instance.clanInfo.ResetToPool();
				instance.clanInfo = null;
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
			throw new Exception("Trying to dispose AppClanChanged with ShouldPool set to false!");
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

	public void CopyTo(AppClanChanged instance)
	{
		if (clanInfo != null)
		{
			if (instance.clanInfo == null)
			{
				instance.clanInfo = clanInfo.Copy();
			}
			else
			{
				clanInfo.CopyTo(instance.clanInfo);
			}
		}
		else
		{
			instance.clanInfo = null;
		}
	}

	public AppClanChanged Copy()
	{
		AppClanChanged appClanChanged = Pool.Get<AppClanChanged>();
		CopyTo(appClanChanged);
		return appClanChanged;
	}

	public static AppClanChanged Deserialize(BufferStream stream)
	{
		AppClanChanged appClanChanged = Pool.Get<AppClanChanged>();
		Deserialize(stream, appClanChanged, isDelta: false);
		return appClanChanged;
	}

	public static AppClanChanged DeserializeLengthDelimited(BufferStream stream)
	{
		AppClanChanged appClanChanged = Pool.Get<AppClanChanged>();
		DeserializeLengthDelimited(stream, appClanChanged, isDelta: false);
		return appClanChanged;
	}

	public static AppClanChanged DeserializeLength(BufferStream stream, int length)
	{
		AppClanChanged appClanChanged = Pool.Get<AppClanChanged>();
		DeserializeLength(stream, length, appClanChanged, isDelta: false);
		return appClanChanged;
	}

	public static AppClanChanged Deserialize(byte[] buffer)
	{
		AppClanChanged appClanChanged = Pool.Get<AppClanChanged>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, appClanChanged, isDelta: false);
		return appClanChanged;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, AppClanChanged previous)
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

	public static AppClanChanged Deserialize(BufferStream stream, AppClanChanged instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				if (instance.clanInfo == null)
				{
					instance.clanInfo = ClanInfo.DeserializeLengthDelimited(stream);
				}
				else
				{
					ClanInfo.DeserializeLengthDelimited(stream, instance.clanInfo, isDelta);
				}
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

	public static AppClanChanged DeserializeLengthDelimited(BufferStream stream, AppClanChanged instance, bool isDelta)
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
				if (instance.clanInfo == null)
				{
					instance.clanInfo = ClanInfo.DeserializeLengthDelimited(stream);
				}
				else
				{
					ClanInfo.DeserializeLengthDelimited(stream, instance.clanInfo, isDelta);
				}
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

	public static AppClanChanged DeserializeLength(BufferStream stream, int length, AppClanChanged instance, bool isDelta)
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
				if (instance.clanInfo == null)
				{
					instance.clanInfo = ClanInfo.DeserializeLengthDelimited(stream);
				}
				else
				{
					ClanInfo.DeserializeLengthDelimited(stream, instance.clanInfo, isDelta);
				}
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

	public static void SerializeDelta(BufferStream stream, AppClanChanged instance, AppClanChanged previous)
	{
		if (instance.clanInfo == null)
		{
			return;
		}
		stream.WriteByte(10);
		BufferStream.RangeHandle range = stream.GetRange(5);
		int position = stream.Position;
		ClanInfo.SerializeDelta(stream, instance.clanInfo, previous.clanInfo);
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

	public static void Serialize(BufferStream stream, AppClanChanged instance)
	{
		if (instance.clanInfo == null)
		{
			return;
		}
		stream.WriteByte(10);
		BufferStream.RangeHandle range = stream.GetRange(5);
		int position = stream.Position;
		ClanInfo.Serialize(stream, instance.clanInfo);
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

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		clanInfo?.InspectUids(action);
	}
}
