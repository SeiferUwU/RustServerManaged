using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class AppClanInfo : IDisposable, Pool.IPooled, IProto<AppClanInfo>, IProto
{
	[NonSerialized]
	public ClanInfo clanInfo;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(AppClanInfo instance)
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
			throw new Exception("Trying to dispose AppClanInfo with ShouldPool set to false!");
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

	public void CopyTo(AppClanInfo instance)
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

	public AppClanInfo Copy()
	{
		AppClanInfo appClanInfo = Pool.Get<AppClanInfo>();
		CopyTo(appClanInfo);
		return appClanInfo;
	}

	public static AppClanInfo Deserialize(BufferStream stream)
	{
		AppClanInfo appClanInfo = Pool.Get<AppClanInfo>();
		Deserialize(stream, appClanInfo, isDelta: false);
		return appClanInfo;
	}

	public static AppClanInfo DeserializeLengthDelimited(BufferStream stream)
	{
		AppClanInfo appClanInfo = Pool.Get<AppClanInfo>();
		DeserializeLengthDelimited(stream, appClanInfo, isDelta: false);
		return appClanInfo;
	}

	public static AppClanInfo DeserializeLength(BufferStream stream, int length)
	{
		AppClanInfo appClanInfo = Pool.Get<AppClanInfo>();
		DeserializeLength(stream, length, appClanInfo, isDelta: false);
		return appClanInfo;
	}

	public static AppClanInfo Deserialize(byte[] buffer)
	{
		AppClanInfo appClanInfo = Pool.Get<AppClanInfo>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, appClanInfo, isDelta: false);
		return appClanInfo;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, AppClanInfo previous)
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

	public static AppClanInfo Deserialize(BufferStream stream, AppClanInfo instance, bool isDelta)
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

	public static AppClanInfo DeserializeLengthDelimited(BufferStream stream, AppClanInfo instance, bool isDelta)
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

	public static AppClanInfo DeserializeLength(BufferStream stream, int length, AppClanInfo instance, bool isDelta)
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

	public static void SerializeDelta(BufferStream stream, AppClanInfo instance, AppClanInfo previous)
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

	public static void Serialize(BufferStream stream, AppClanInfo instance)
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
