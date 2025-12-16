using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class WallpaperTool : IDisposable, Pool.IPooled, IProto<WallpaperTool>, IProto
{
	[NonSerialized]
	public ulong wallSkinID;

	[NonSerialized]
	public ulong flooringSkinID;

	[NonSerialized]
	public ulong ceilingSkinID;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(WallpaperTool instance)
	{
		if (instance.ShouldPool)
		{
			instance.wallSkinID = 0uL;
			instance.flooringSkinID = 0uL;
			instance.ceilingSkinID = 0uL;
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
			throw new Exception("Trying to dispose WallpaperTool with ShouldPool set to false!");
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

	public void CopyTo(WallpaperTool instance)
	{
		instance.wallSkinID = wallSkinID;
		instance.flooringSkinID = flooringSkinID;
		instance.ceilingSkinID = ceilingSkinID;
	}

	public WallpaperTool Copy()
	{
		WallpaperTool wallpaperTool = Pool.Get<WallpaperTool>();
		CopyTo(wallpaperTool);
		return wallpaperTool;
	}

	public static WallpaperTool Deserialize(BufferStream stream)
	{
		WallpaperTool wallpaperTool = Pool.Get<WallpaperTool>();
		Deserialize(stream, wallpaperTool, isDelta: false);
		return wallpaperTool;
	}

	public static WallpaperTool DeserializeLengthDelimited(BufferStream stream)
	{
		WallpaperTool wallpaperTool = Pool.Get<WallpaperTool>();
		DeserializeLengthDelimited(stream, wallpaperTool, isDelta: false);
		return wallpaperTool;
	}

	public static WallpaperTool DeserializeLength(BufferStream stream, int length)
	{
		WallpaperTool wallpaperTool = Pool.Get<WallpaperTool>();
		DeserializeLength(stream, length, wallpaperTool, isDelta: false);
		return wallpaperTool;
	}

	public static WallpaperTool Deserialize(byte[] buffer)
	{
		WallpaperTool wallpaperTool = Pool.Get<WallpaperTool>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, wallpaperTool, isDelta: false);
		return wallpaperTool;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, WallpaperTool previous)
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

	public static WallpaperTool Deserialize(BufferStream stream, WallpaperTool instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.wallSkinID = ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.flooringSkinID = ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.ceilingSkinID = ProtocolParser.ReadUInt64(stream);
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

	public static WallpaperTool DeserializeLengthDelimited(BufferStream stream, WallpaperTool instance, bool isDelta)
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
				instance.wallSkinID = ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.flooringSkinID = ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.ceilingSkinID = ProtocolParser.ReadUInt64(stream);
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

	public static WallpaperTool DeserializeLength(BufferStream stream, int length, WallpaperTool instance, bool isDelta)
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
				instance.wallSkinID = ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.flooringSkinID = ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.ceilingSkinID = ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, WallpaperTool instance, WallpaperTool previous)
	{
		if (instance.wallSkinID != previous.wallSkinID)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.wallSkinID);
		}
		if (instance.flooringSkinID != previous.flooringSkinID)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.flooringSkinID);
		}
		if (instance.ceilingSkinID != previous.ceilingSkinID)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.ceilingSkinID);
		}
	}

	public static void Serialize(BufferStream stream, WallpaperTool instance)
	{
		if (instance.wallSkinID != 0L)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.wallSkinID);
		}
		if (instance.flooringSkinID != 0L)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.flooringSkinID);
		}
		if (instance.ceilingSkinID != 0L)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.ceilingSkinID);
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
