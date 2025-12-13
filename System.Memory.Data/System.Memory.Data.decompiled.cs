using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = "")]
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("© Microsoft Corporation. All rights reserved.")]
[assembly: AssemblyDescription("Contains the BinaryData type, which is useful for converting between strings, streams, JSON, and bytes.")]
[assembly: AssemblyFileVersion("1.0.221.20802")]
[assembly: AssemblyInformationalVersion("1.0.2+7e3cf643977591e9041f4c628fd4d28237398e0b")]
[assembly: AssemblyProduct("Azure .NET SDK")]
[assembly: AssemblyTitle("System.Memory.Data")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/Azure/azure-sdk-for-net")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: AssemblyVersion("1.0.2.0")]
namespace Microsoft.CodeAnalysis
{
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	internal sealed class EmbeddedAttribute : Attribute
	{
	}
}
namespace System.Runtime.CompilerServices
{
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
	internal sealed class NullableAttribute : Attribute
	{
		public readonly byte[] NullableFlags;

		public NullableAttribute(byte P_0)
		{
			NullableFlags = new byte[1] { P_0 };
		}

		public NullableAttribute(byte[] P_0)
		{
			NullableFlags = P_0;
		}
	}
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	internal sealed class NullableContextAttribute : Attribute
	{
		public readonly byte Flag;

		public NullableContextAttribute(byte P_0)
		{
			Flag = P_0;
		}
	}
}
namespace System
{
	public class BinaryData
	{
		private const int CopyToBufferSize = 81920;

		private readonly ReadOnlyMemory<byte> _bytes;

		public BinaryData(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			_bytes = data;
		}

		public BinaryData(object? jsonSerializable, JsonSerializerOptions? options = null, Type? type = null)
		{
			_bytes = JsonSerializer.SerializeToUtf8Bytes(jsonSerializable, type ?? jsonSerializable?.GetType() ?? typeof(object), options);
		}

		public BinaryData(ReadOnlyMemory<byte> data)
		{
			_bytes = data;
		}

		public BinaryData(string data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			_bytes = Encoding.UTF8.GetBytes(data);
		}

		public static BinaryData FromBytes(ReadOnlyMemory<byte> data)
		{
			return new BinaryData(data);
		}

		public static BinaryData FromBytes(byte[] data)
		{
			return new BinaryData(data);
		}

		public static BinaryData FromString(string data)
		{
			return new BinaryData(data);
		}

		public static BinaryData FromStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return FromStreamAsync(stream, async: false).GetAwaiter().GetResult();
		}

		public static Task<BinaryData> FromStreamAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return FromStreamAsync(stream, async: true, cancellationToken);
		}

		private static async Task<BinaryData> FromStreamAsync(Stream stream, bool async, CancellationToken cancellationToken = default(CancellationToken))
		{
			int capacity = 0;
			if (stream.CanSeek)
			{
				long num = stream.Length - stream.Position;
				if (num > int.MaxValue)
				{
					throw new ArgumentOutOfRangeException("stream", "Stream length must be less than Int32.MaxValue");
				}
				capacity = (int)num;
			}
			using MemoryStream memoryStream = (stream.CanSeek ? new MemoryStream(capacity) : new MemoryStream());
			if (async)
			{
				await stream.CopyToAsync(memoryStream, 81920, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				stream.CopyTo(memoryStream);
			}
			return new BinaryData(MemoryExtensions.AsMemory(memoryStream.GetBuffer(), 0, (int)memoryStream.Position));
		}

		public static BinaryData FromObjectAsJson<T>(T jsonSerializable, JsonSerializerOptions? options = null)
		{
			return new BinaryData(JsonSerializer.SerializeToUtf8Bytes(jsonSerializable, typeof(T), options));
		}

		public override string ToString()
		{
			if (MemoryMarshal.TryGetArray(_bytes, out var segment))
			{
				return Encoding.UTF8.GetString(segment.Array, segment.Offset, segment.Count);
			}
			return Encoding.UTF8.GetString(_bytes.ToArray());
		}

		public Stream ToStream()
		{
			return new System.IO.ReadOnlyMemoryStream(_bytes);
		}

		public ReadOnlyMemory<byte> ToMemory()
		{
			return _bytes;
		}

		public byte[] ToArray()
		{
			return _bytes.ToArray();
		}

		public T ToObjectFromJson<T>(JsonSerializerOptions? options = null)
		{
			return (T)JsonSerializer.Deserialize(_bytes.Span, typeof(T), options);
		}

		public static implicit operator ReadOnlyMemory<byte>(BinaryData? data)
		{
			return data?._bytes ?? default(ReadOnlyMemory<byte>);
		}

		public static implicit operator ReadOnlySpan<byte>(BinaryData? data)
		{
			return data?._bytes.Span ?? default(ReadOnlySpan<byte>);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? obj)
		{
			return this == obj;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
namespace System.IO
{
	internal sealed class ReadOnlyMemoryStream : Stream
	{
		private ReadOnlyMemory<byte> _content;

		private bool _isOpen;

		private int _position;

		public override bool CanRead => _isOpen;

		public override bool CanSeek => _isOpen;

		public override bool CanWrite => false;

		public override long Length
		{
			get
			{
				ValidateNotClosed();
				return _content.Length;
			}
		}

		public override long Position
		{
			get
			{
				ValidateNotClosed();
				return _position;
			}
			set
			{
				ValidateNotClosed();
				if (value < 0 || value > int.MaxValue)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_position = (int)value;
			}
		}

		public ReadOnlyMemoryStream(ReadOnlyMemory<byte> content)
		{
			_content = content;
			_isOpen = true;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			ValidateNotClosed();
			long num = origin switch
			{
				SeekOrigin.End => _content.Length + offset, 
				SeekOrigin.Current => _position + offset, 
				SeekOrigin.Begin => offset, 
				_ => throw new ArgumentOutOfRangeException("origin"), 
			};
			if (num > int.MaxValue)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (num < 0)
			{
				throw new IOException("An attempt was made to move the position before the beginning of the stream.");
			}
			_position = (int)num;
			return _position;
		}

		public override int ReadByte()
		{
			ValidateNotClosed();
			ReadOnlySpan<byte> span = _content.Span;
			if (_position >= span.Length)
			{
				return -1;
			}
			return span[_position++];
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			ValidateNotClosed();
			ValidateReadArrayArguments(buffer, offset, count);
			return ReadBuffer(new Span<byte>(buffer, offset, count));
		}

		private int ReadBuffer(Span<byte> buffer)
		{
			int num = _content.Length - _position;
			if (num <= 0 || buffer.Length == 0)
			{
				return 0;
			}
			ReadOnlySpan<byte> readOnlySpan;
			if (num <= buffer.Length)
			{
				readOnlySpan = _content.Span;
				readOnlySpan = readOnlySpan.Slice(_position);
				readOnlySpan.CopyTo(buffer);
				_position = _content.Length;
				return num;
			}
			readOnlySpan = _content.Span;
			readOnlySpan = readOnlySpan.Slice(_position, buffer.Length);
			readOnlySpan.CopyTo(buffer);
			_position += buffer.Length;
			return buffer.Length;
		}

		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			ValidateNotClosed();
			ValidateReadArrayArguments(buffer, offset, count);
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.FromResult(ReadBuffer(new Span<byte>(buffer, offset, count)));
			}
			return Task.FromCanceled<int>(cancellationToken);
		}

		public override void Flush()
		{
		}

		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		private static void ValidateReadArrayArguments(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || buffer.Length - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		private void ValidateNotClosed()
		{
			if (!_isOpen)
			{
				throw new ObjectDisposedException(null, "Cannot access a closed Stream");
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					_isOpen = false;
					_content = default(ReadOnlyMemory<byte>);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
	}
}
