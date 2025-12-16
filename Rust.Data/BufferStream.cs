using System;
using System.Runtime.CompilerServices;
using Facepunch;
using UnityEngine;

public sealed class BufferStream : IDisposable, Pool.IPooled
{
	public static class Shared
	{
		public static int StartingCapacity = 64;

		public static int MaximumCapacity = 536870912;

		public static int MaximumPooledSize = 67108864;

		public static readonly ArrayPool<byte> ArrayPool = new ArrayPool<byte>(MaximumPooledSize);
	}

	public readonly ref struct RangeHandle
	{
		private readonly BufferStream _stream;

		private readonly int _offset;

		private readonly int _length;

		public RangeHandle(BufferStream stream, int offset, int length)
		{
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			_stream = stream ?? throw new ArgumentNullException("stream");
			_offset = offset;
			_length = length;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<byte> GetSpan()
		{
			return new Span<byte>(_stream._buffer, _offset, _length);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ArraySegment<byte> GetSegment()
		{
			return new ArraySegment<byte>(_stream._buffer, _offset, _length);
		}
	}

	private bool _isBufferOwned;

	private byte[] _buffer;

	private int _length;

	private int _position;

	public int Length
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return _length;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (_position > value)
			{
				throw new InvalidOperationException("Cannot shrink buffer below current position!");
			}
			int num = value - _length;
			if (num > 0)
			{
				EnsureCapacity(num);
			}
			_length = value;
		}
	}

	public int Position
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return _position;
		}
		set
		{
			if (value < 0 || value > _length)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			_position = value;
		}
	}

	public BufferStream Initialize()
	{
		_isBufferOwned = true;
		_buffer = null;
		_length = 0;
		_position = 0;
		return this;
	}

	public BufferStream Initialize(Span<byte> buffer)
	{
		_isBufferOwned = true;
		_buffer = null;
		_length = buffer.Length;
		_position = 0;
		EnsureCapacity(buffer.Length);
		buffer.CopyTo(_buffer);
		return this;
	}

	public BufferStream Initialize(byte[] buffer, int length = -1)
	{
		if (buffer == null)
		{
			throw new ArgumentNullException("buffer");
		}
		if (length > buffer.Length)
		{
			throw new ArgumentOutOfRangeException("length");
		}
		_isBufferOwned = false;
		_buffer = buffer;
		_length = ((length < 0) ? buffer.Length : length);
		_position = 0;
		return this;
	}

	public void Dispose()
	{
		if (_isBufferOwned && _buffer != null)
		{
			ReturnBuffer(_buffer);
		}
		_buffer = null;
		BufferStream obj = this;
		Pool.Free(ref obj);
	}

	void Pool.IPooled.EnterPool()
	{
		if (_isBufferOwned && _buffer != null)
		{
			ReturnBuffer(_buffer);
		}
		_buffer = null;
	}

	void Pool.IPooled.LeavePool()
	{
	}

	public void Clear()
	{
		_length = 0;
		_position = 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int ReadByte()
	{
		if (_position >= _length)
		{
			return -1;
		}
		return _buffer[_position++];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void WriteByte(byte b)
	{
		EnsureCapacity(1);
		_buffer[_position++] = b;
		_length = Math.Max(_length, _position);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T Read<T>() where T : unmanaged
	{
		int num = Unsafe.SizeOf<T>();
		if (_length - _position < num)
		{
			ThrowReadOutOfBounds();
		}
		ref T reference = ref Unsafe.As<byte, T>(ref _buffer[_position]);
		_position += num;
		return reference;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T Peek<T>() where T : unmanaged
	{
		int num = Unsafe.SizeOf<T>();
		if (_length - _position < num)
		{
			ThrowReadOutOfBounds();
		}
		return Unsafe.As<byte, T>(ref _buffer[_position]);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private void ThrowReadOutOfBounds()
	{
		throw new InvalidOperationException("Attempted to read past the end of the BufferStream");
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Write<T>(T value) where T : unmanaged
	{
		int num = Unsafe.SizeOf<T>();
		EnsureCapacity(num);
		Unsafe.As<byte, T>(ref _buffer[_position]) = value;
		_position += num;
		_length = Math.Max(_length, _position);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public RangeHandle GetRange(int count)
	{
		EnsureCapacity(count);
		RangeHandle result = new RangeHandle(this, _position, count);
		_position += count;
		_length = Math.Max(_length, _position);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Skip(int count)
	{
		_position += count;
	}

	public ArraySegment<byte> GetBuffer()
	{
		if (_length == 0)
		{
			return new ArraySegment<byte>(Array.Empty<byte>(), 0, 0);
		}
		return new ArraySegment<byte>(_buffer, 0, _length);
	}

	private void EnsureCapacity(int spaceRequired)
	{
		if (spaceRequired < 0)
		{
			throw new ArgumentOutOfRangeException("spaceRequired");
		}
		if (_buffer == null)
		{
			if (!_isBufferOwned)
			{
				throw new InvalidOperationException("Cannot allocate for BufferStream that doesn't own the buffer (did you forget to call Initialize?)");
			}
			int num = ((spaceRequired <= Shared.StartingCapacity) ? Shared.StartingCapacity : spaceRequired);
			int num2 = Mathf.NextPowerOfTwo(num);
			if (num2 > Shared.MaximumCapacity)
			{
				throw new Exception($"Preventing BufferStream buffer from growing too large (requiredLength={num})");
			}
			_buffer = RentBuffer(num2);
		}
		else if (_buffer.Length - _position < spaceRequired)
		{
			int num3 = _position + spaceRequired;
			int num4 = Mathf.NextPowerOfTwo(Math.Max(num3, _buffer.Length));
			if (!_isBufferOwned)
			{
				throw new InvalidOperationException($"Cannot grow buffer for BufferStream that doesn't own the buffer (requiredLength={num3})");
			}
			if (num4 > Shared.MaximumCapacity)
			{
				throw new Exception($"Preventing BufferStream buffer from growing too large (requiredLength={num3})");
			}
			byte[] array = RentBuffer(num4);
			Buffer.BlockCopy(_buffer, 0, array, 0, _length);
			ReturnBuffer(_buffer);
			_buffer = array;
		}
	}

	private static byte[] RentBuffer(int minSize)
	{
		if (minSize > Shared.MaximumPooledSize)
		{
			return new byte[minSize];
		}
		return Shared.ArrayPool.Rent(minSize);
	}

	private static void ReturnBuffer(byte[] buffer)
	{
		if (buffer != null && buffer.Length <= Shared.MaximumPooledSize)
		{
			Shared.ArrayPool.Return(buffer);
		}
	}
}
