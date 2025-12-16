using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Photo : IDisposable, Pool.IPooled, IProto<Photo>, IProto
{
	[NonSerialized]
	public ulong photographerSteamId;

	[NonSerialized]
	public uint imageCrc;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Photo instance)
	{
		if (instance.ShouldPool)
		{
			instance.photographerSteamId = 0uL;
			instance.imageCrc = 0u;
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
			throw new Exception("Trying to dispose Photo with ShouldPool set to false!");
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

	public void CopyTo(Photo instance)
	{
		instance.photographerSteamId = photographerSteamId;
		instance.imageCrc = imageCrc;
	}

	public Photo Copy()
	{
		Photo photo = Pool.Get<Photo>();
		CopyTo(photo);
		return photo;
	}

	public static Photo Deserialize(BufferStream stream)
	{
		Photo photo = Pool.Get<Photo>();
		Deserialize(stream, photo, isDelta: false);
		return photo;
	}

	public static Photo DeserializeLengthDelimited(BufferStream stream)
	{
		Photo photo = Pool.Get<Photo>();
		DeserializeLengthDelimited(stream, photo, isDelta: false);
		return photo;
	}

	public static Photo DeserializeLength(BufferStream stream, int length)
	{
		Photo photo = Pool.Get<Photo>();
		DeserializeLength(stream, length, photo, isDelta: false);
		return photo;
	}

	public static Photo Deserialize(byte[] buffer)
	{
		Photo photo = Pool.Get<Photo>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, photo, isDelta: false);
		return photo;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Photo previous)
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

	public static Photo Deserialize(BufferStream stream, Photo instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.photographerSteamId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.imageCrc = ProtocolParser.ReadUInt32(stream);
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

	public static Photo DeserializeLengthDelimited(BufferStream stream, Photo instance, bool isDelta)
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
				instance.photographerSteamId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.imageCrc = ProtocolParser.ReadUInt32(stream);
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

	public static Photo DeserializeLength(BufferStream stream, int length, Photo instance, bool isDelta)
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
				instance.photographerSteamId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.imageCrc = ProtocolParser.ReadUInt32(stream);
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

	public static void SerializeDelta(BufferStream stream, Photo instance, Photo previous)
	{
		if (instance.photographerSteamId != previous.photographerSteamId)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.photographerSteamId);
		}
		if (instance.imageCrc != previous.imageCrc)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.imageCrc);
		}
	}

	public static void Serialize(BufferStream stream, Photo instance)
	{
		if (instance.photographerSteamId != 0L)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.photographerSteamId);
		}
		if (instance.imageCrc != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.imageCrc);
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
