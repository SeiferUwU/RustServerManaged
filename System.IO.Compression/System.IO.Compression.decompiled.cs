using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using Unity;

[assembly: CLSCompliant(true)]
[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("System.IO.Compression.dll")]
[assembly: AssemblyDefaultAlias("System.IO.Compression.dll")]
[assembly: AssemblyDescription("System.IO.Compression.dll")]
[assembly: AssemblyCompany("Mono development team")]
[assembly: AssemblyProduct("Mono Common Language Infrastructure")]
[assembly: AssemblyCopyright("(c) Various Mono authors")]
[assembly: AssemblyInformationalVersion("4.6.57.0")]
[assembly: AssemblyFileVersion("4.6.57.0")]
[assembly: AssemblyDelaySign(true)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("4.0.0.0")]
[assembly: TypeForwardedTo(typeof(CompressionLevel))]
[assembly: TypeForwardedTo(typeof(CompressionMode))]
[assembly: TypeForwardedTo(typeof(DeflateStream))]
[assembly: TypeForwardedTo(typeof(GZipStream))]
[module: UnverifiableCode]
internal static class Interop
{
	internal static class Brotli
	{
		[DllImport("__Internal")]
		internal static extern SafeBrotliDecoderHandle BrotliDecoderCreateInstance(IntPtr allocFunc, IntPtr freeFunc, IntPtr opaque);

		[DllImport("__Internal")]
		internal unsafe static extern int BrotliDecoderDecompressStream(SafeBrotliDecoderHandle state, ref IntPtr availableIn, byte** nextIn, ref IntPtr availableOut, byte** nextOut, out IntPtr totalOut);

		[DllImport("__Internal")]
		internal unsafe static extern bool BrotliDecoderDecompress(IntPtr availableInput, byte* inBytes, ref IntPtr availableOutput, byte* outBytes);

		[DllImport("__Internal")]
		internal static extern void BrotliDecoderDestroyInstance(IntPtr state);

		[DllImport("__Internal")]
		internal static extern bool BrotliDecoderIsFinished(SafeBrotliDecoderHandle state);

		[DllImport("__Internal")]
		internal static extern SafeBrotliEncoderHandle BrotliEncoderCreateInstance(IntPtr allocFunc, IntPtr freeFunc, IntPtr opaque);

		[DllImport("__Internal")]
		internal static extern bool BrotliEncoderSetParameter(SafeBrotliEncoderHandle state, BrotliEncoderParameter parameter, uint value);

		[DllImport("__Internal")]
		internal unsafe static extern bool BrotliEncoderCompressStream(SafeBrotliEncoderHandle state, BrotliEncoderOperation op, ref IntPtr availableIn, byte** nextIn, ref IntPtr availableOut, byte** nextOut, out IntPtr totalOut);

		[DllImport("__Internal")]
		internal static extern bool BrotliEncoderHasMoreOutput(SafeBrotliEncoderHandle state);

		[DllImport("__Internal")]
		internal static extern void BrotliEncoderDestroyInstance(IntPtr state);

		[DllImport("__Internal")]
		internal unsafe static extern bool BrotliEncoderCompress(int quality, int window, int v, IntPtr availableInput, byte* inBytes, ref IntPtr availableOutput, byte* outBytes);
	}

	internal static class Libraries
	{
		internal const string CompressionNative = "__Internal";
	}
}
internal static class Consts
{
	public const string MonoCorlibVersion = "1A5E0066-58DC-428A-B21C-0AD6CDAE2789";

	public const string MonoVersion = "6.13.0.0";

	public const string MonoCompany = "Mono development team";

	public const string MonoProduct = "Mono Common Language Infrastructure";

	public const string MonoCopyright = "(c) Various Mono authors";

	public const string FxVersion = "4.0.0.0";

	public const string FxFileVersion = "4.6.57.0";

	public const string EnvironmentVersion = "4.0.30319.42000";

	public const string VsVersion = "0.0.0.0";

	public const string VsFileVersion = "11.0.0.0";

	private const string PublicKeyToken = "b77a5c561934e089";

	public const string AssemblyI18N = "I18N, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMicrosoft_JScript = "Microsoft.JScript, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VisualStudio = "Microsoft.VisualStudio, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VisualStudio_Web = "Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VSDesigner = "Microsoft.VSDesigner, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMono_Http = "Mono.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Posix = "Mono.Posix, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Security = "Mono.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Messaging_RabbitMQ = "Mono.Messaging.RabbitMQ, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyCorlib = "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem = "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Data = "System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Design = "System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_DirectoryServices = "System.DirectoryServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Drawing = "System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Drawing_Design = "System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Messaging = "System.Messaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Security = "System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_ServiceProcess = "System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Web = "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Windows_Forms = "System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_2_0 = "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystemCore_3_5 = "System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Core = "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string WindowsBase_3_0 = "WindowsBase, Version=3.0.0.0, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyWindowsBase = "WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationCore_3_5 = "PresentationCore, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationCore_4_0 = "PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationFramework_3_5 = "PresentationFramework, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblySystemServiceModel_3_0 = "System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
}
internal static class SR
{
	public const string ArgumentOutOfRange_Enum = "Enum value was out of legal range.";

	public const string ArgumentOutOfRange_NeedPosNum = "Positive number required.";

	public const string CannotReadFromDeflateStream = "Reading from the compression stream is not supported.";

	public const string CannotWriteToDeflateStream = "Writing to the compression stream is not supported.";

	public const string GenericInvalidData = "Found invalid data while decoding.";

	public const string InvalidArgumentOffsetCount = "Offset plus count is larger than the length of target array.";

	public const string InvalidBeginCall = "Only one asynchronous reader or writer is allowed time at one time.";

	public const string InvalidBlockLength = "Block length does not match with its complement.";

	public const string InvalidHuffmanData = "Failed to construct a huffman tree using the length array. The stream might be corrupted.";

	public const string NotSupported = "This operation is not supported.";

	public const string NotSupported_UnreadableStream = "Stream does not support reading.";

	public const string NotSupported_UnwritableStream = "Stream does not support writing.";

	public const string ObjectDisposed_StreamClosed = "Can not access a closed Stream.";

	public const string UnknownBlockType = "Unknown block type. Stream might be corrupted.";

	public const string UnknownState = "Decoder is in some unknown state. This might be caused by corrupted data.";

	public const string ZLibErrorDLLLoadError = "The underlying compression routine could not be loaded correctly.";

	public const string ZLibErrorInconsistentStream = "The stream state of the underlying compression routine is inconsistent.";

	public const string ZLibErrorIncorrectInitParameters = "The underlying compression routine received incorrect initialization parameters.";

	public const string ZLibErrorNotEnoughMemory = "The underlying compression routine could not reserve sufficient memory.";

	public const string ZLibErrorVersionMismatch = "The version of the underlying compression routine does not match expected version.";

	public const string ZLibErrorUnexpected = "The underlying compression routine returned an unexpected error code.";

	public const string ArgumentNeedNonNegative = "The argument must be non-negative.";

	public const string CannotBeEmpty = "String cannot be empty.";

	public const string CDCorrupt = "Central Directory corrupt.";

	public const string CentralDirectoryInvalid = "Central Directory is invalid.";

	public const string CreateInReadMode = "Cannot create entries on an archive opened in read mode.";

	public const string CreateModeCapabilities = "Cannot use create mode on a non-writable stream.";

	public const string CreateModeCreateEntryWhileOpen = "Entries cannot be created while previously created entries are still open.";

	public const string CreateModeWriteOnceAndOneEntryAtATime = "Entries in create mode may only be written to once, and only one entry may be held open at a time.";

	public const string DateTimeOutOfRange = "The DateTimeOffset specified cannot be converted into a Zip file timestamp.";

	public const string DeletedEntry = "Cannot modify deleted entry.";

	public const string DeleteOnlyInUpdate = "Delete can only be used when the archive is in Update mode.";

	public const string DeleteOpenEntry = "Cannot delete an entry currently open for writing.";

	public const string EntriesInCreateMode = "Cannot access entries in Create mode.";

	public const string EntryNameEncodingNotSupported = "The specified entry name encoding is not supported.";

	public const string EntryNamesTooLong = "Entry names cannot require more than 2^16 bits.";

	public const string EntryTooLarge = "Entries larger than 4GB are not supported in Update mode.";

	public const string EOCDNotFound = "End of Central Directory record could not be found.";

	public const string FieldTooBigCompressedSize = "Compressed Size cannot be held in an Int64.";

	public const string FieldTooBigLocalHeaderOffset = "Local Header Offset cannot be held in an Int64.";

	public const string FieldTooBigNumEntries = "Number of Entries cannot be held in an Int64.";

	public const string FieldTooBigOffsetToCD = "Offset to Central Directory cannot be held in an Int64.";

	public const string FieldTooBigOffsetToZip64EOCD = "Offset to Zip64 End Of Central Directory record cannot be held in an Int64.";

	public const string FieldTooBigStartDiskNumber = "Start Disk Number cannot be held in an Int64.";

	public const string FieldTooBigUncompressedSize = "Uncompressed Size cannot be held in an Int64.";

	public const string FrozenAfterWrite = "Cannot modify entry in Create mode after entry has been opened for writing.";

	public const string HiddenStreamName = "A stream from ZipArchiveEntry has been disposed.";

	public const string LengthAfterWrite = "Length properties are unavailable once an entry has been opened for writing.";

	public const string LocalFileHeaderCorrupt = "A local file header is corrupt.";

	public const string NumEntriesWrong = "Number of entries expected in End Of Central Directory does not correspond to number of entries in Central Directory.";

	public const string OffsetLengthInvalid = "The offset and length parameters are not valid for the array that was given.";

	public const string ReadingNotSupported = "This stream from ZipArchiveEntry does not support reading.";

	public const string ReadModeCapabilities = "Cannot use read mode on a non-readable stream.";

	public const string ReadOnlyArchive = "Cannot modify read-only archive.";

	public const string SeekingNotSupported = "This stream from ZipArchiveEntry does not support seeking.";

	public const string SetLengthRequiresSeekingAndWriting = "SetLength requires a stream that supports seeking and writing.";

	public const string SplitSpanned = "Split or spanned archives are not supported.";

	public const string UnexpectedEndOfStream = "Zip file corrupt: unexpected end of stream reached.";

	public const string UnsupportedCompression = "The archive entry was compressed using an unsupported compression method.";

	public const string UnsupportedCompressionMethod = "The archive entry was compressed using {0} and is not supported.";

	public const string UpdateModeCapabilities = "Update mode requires a stream with read, write, and seek capabilities.";

	public const string UpdateModeOneStream = "Entries cannot be opened multiple times in Update mode.";

	public const string WritingNotSupported = "This stream from ZipArchiveEntry does not support writing.";

	public const string Zip64EOCDNotWhereExpected = "Zip 64 End of Central Directory Record not where indicated.";

	public const string Argument_InvalidPathChars = "Illegal characters in path '{0}'.";

	public const string Stream_FalseCanRead = "Stream does not support reading.";

	public const string Stream_FalseCanWrite = "Stream does not support writing.";

	public const string BrotliEncoder_Create = "Failed to create BrotliEncoder instance";

	public const string BrotliEncoder_Disposed = "Can not access a closed Encoder.";

	public const string BrotliEncoder_Quality = "Provided BrotliEncoder Quality of {0} is not between the minimum value of {1} and the maximum value of {2}";

	public const string BrotliEncoder_Window = "Provided BrotliEncoder Window of {0} is not between the minimum value of {1} and the maximum value of {2}";

	public const string BrotliEncoder_InvalidSetParameter = "The BrotliEncoder {0} can not be changed at current encoder state.";

	public const string BrotliDecoder_Create = "Failed to create BrotliDecoder instance";

	public const string BrotliDecoder_Error = "Decoder threw unexpected error: {0}";

	public const string BrotliDecoder_Disposed = "Can not access a closed Decoder.";

	public const string BrotliStream_Compress_UnsupportedOperation = "Can not perform Read operations on a BrotliStream constructed with CompressionMode.Compress.";

	public const string BrotliStream_Compress_InvalidData = "Encoder ran into invalid data.";

	public const string BrotliStream_Decompress_UnsupportedOperation = "Can not perform Write operations on a BrotliStream constructed with CompressionMode.Decompress.";

	public const string BrotliStream_Decompress_InvalidData = "Decoder ran into invalid data.";

	public const string BrotliStream_Decompress_InvalidStream = "BrotliStream.BaseStream returned more bytes than requested in Read.";

	internal static string GetString(string name, params object[] args)
	{
		return GetString(CultureInfo.InvariantCulture, name, args);
	}

	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	internal static string GetString(string name)
	{
		return name;
	}

	internal static string GetString(CultureInfo culture, string name)
	{
		return name;
	}

	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	internal static string Format(CultureInfo ci, string resourceFormat, object p1, object p2)
	{
		return string.Format(ci, resourceFormat, p1, p2);
	}

	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}

	internal static string GetResourceString(string str)
	{
		return str;
	}
}
namespace System.Runtime.CompilerServices
{
	internal class FriendAccessAllowedAttribute : Attribute
	{
	}
}
namespace System.IO.Compression
{
	public sealed class BrotliStream : Stream
	{
		private const int DefaultInternalBufferSize = 65520;

		private Stream _stream;

		private readonly byte[] _buffer;

		private readonly bool _leaveOpen;

		private readonly CompressionMode _mode;

		private int _activeAsyncOperation;

		private BrotliDecoder _decoder;

		private int _bufferOffset;

		private int _bufferCount;

		private BrotliEncoder _encoder;

		public Stream BaseStream => _stream;

		public override bool CanRead
		{
			get
			{
				if (_mode == CompressionMode.Decompress && _stream != null)
				{
					return _stream.CanRead;
				}
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				if (_mode == CompressionMode.Compress && _stream != null)
				{
					return _stream.CanWrite;
				}
				return false;
			}
		}

		public override bool CanSeek => false;

		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		private bool AsyncOperationIsActive => _activeAsyncOperation != 0;

		public BrotliStream(Stream stream, CompressionMode mode)
			: this(stream, mode, leaveOpen: false)
		{
		}

		public BrotliStream(Stream stream, CompressionMode mode, bool leaveOpen)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			switch (mode)
			{
			case CompressionMode.Compress:
				if (!stream.CanWrite)
				{
					throw new ArgumentException("Stream does not support writing.", "stream");
				}
				break;
			case CompressionMode.Decompress:
				if (!stream.CanRead)
				{
					throw new ArgumentException("Stream does not support reading.", "stream");
				}
				break;
			default:
				throw new ArgumentException("Enum value was out of legal range.", "mode");
			}
			_mode = mode;
			_stream = stream;
			_leaveOpen = leaveOpen;
			_buffer = new byte[65520];
		}

		private void EnsureNotDisposed()
		{
			if (_stream == null)
			{
				throw new ObjectDisposedException("stream", "Can not access a closed Stream.");
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && _stream != null)
				{
					if (_mode == CompressionMode.Compress)
					{
						WriteCore(ReadOnlySpan<byte>.Empty, isFinalBlock: true);
					}
					if (!_leaveOpen)
					{
						_stream.Dispose();
					}
				}
			}
			finally
			{
				_stream = null;
				_encoder.Dispose();
				_decoder.Dispose();
				base.Dispose(disposing);
			}
		}

		private static void ValidateParameters(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Positive number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Positive number required.");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("Offset plus count is larger than the length of target array.");
			}
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		private void EnsureNoActiveAsyncOperation()
		{
			if (AsyncOperationIsActive)
			{
				ThrowInvalidBeginCall();
			}
		}

		private void AsyncOperationStarting()
		{
			if (Interlocked.CompareExchange(ref _activeAsyncOperation, 1, 0) != 0)
			{
				ThrowInvalidBeginCall();
			}
		}

		private void AsyncOperationCompleting()
		{
			Interlocked.CompareExchange(ref _activeAsyncOperation, 0, 1);
		}

		private static void ThrowInvalidBeginCall()
		{
			throw new InvalidOperationException("Only one asynchronous reader or writer is allowed time at one time.");
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			ValidateParameters(buffer, offset, count);
			return Read(new Span<byte>(buffer, offset, count));
		}

		public override int Read(Span<byte> buffer)
		{
			if (_mode != CompressionMode.Decompress)
			{
				throw new InvalidOperationException("Can not perform Read operations on a BrotliStream constructed with CompressionMode.Compress.");
			}
			EnsureNotDisposed();
			int num = 0;
			OperationStatus operationStatus = OperationStatus.DestinationTooSmall;
			while (buffer.Length > 0 && operationStatus != OperationStatus.Done)
			{
				if (operationStatus == OperationStatus.NeedMoreData)
				{
					if (_bufferCount > 0 && _bufferOffset != 0)
					{
						_buffer.AsSpan(_bufferOffset, _bufferCount).CopyTo(_buffer);
					}
					_bufferOffset = 0;
					int num2 = 0;
					while (_bufferCount < _buffer.Length && (num2 = _stream.Read(_buffer, _bufferCount, _buffer.Length - _bufferCount)) > 0)
					{
						_bufferCount += num2;
						if (_bufferCount > _buffer.Length)
						{
							throw new InvalidDataException("BrotliStream.BaseStream returned more bytes than requested in Read.");
						}
					}
					if (_bufferCount <= 0)
					{
						break;
					}
				}
				operationStatus = _decoder.Decompress(_buffer.AsSpan(_bufferOffset, _bufferCount), buffer, out var bytesConsumed, out var bytesWritten);
				if (operationStatus == OperationStatus.InvalidData)
				{
					throw new InvalidOperationException("Decoder ran into invalid data.");
				}
				if (bytesConsumed > 0)
				{
					_bufferOffset += bytesConsumed;
					_bufferCount -= bytesConsumed;
				}
				if (bytesWritten > 0)
				{
					num += bytesWritten;
					buffer = buffer.Slice(bytesWritten);
				}
			}
			return num;
		}

		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return System.Threading.Tasks.TaskToApm.Begin(ReadAsync(buffer, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		public override int EndRead(IAsyncResult asyncResult)
		{
			return System.Threading.Tasks.TaskToApm.End<int>(asyncResult);
		}

		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			ValidateParameters(buffer, offset, count);
			return ReadAsync(new Memory<byte>(buffer, offset, count), cancellationToken).AsTask();
		}

		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (_mode != CompressionMode.Decompress)
			{
				throw new InvalidOperationException("Can not perform Read operations on a BrotliStream constructed with CompressionMode.Compress.");
			}
			EnsureNoActiveAsyncOperation();
			EnsureNotDisposed();
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
			}
			return FinishReadAsyncMemory(buffer, cancellationToken);
		}

		private async ValueTask<int> FinishReadAsyncMemory(Memory<byte> buffer, CancellationToken cancellationToken)
		{
			AsyncOperationStarting();
			try
			{
				int totalWritten = 0;
				_ = Memory<byte>.Empty;
				OperationStatus operationStatus = OperationStatus.DestinationTooSmall;
				while (buffer.Length > 0 && operationStatus != OperationStatus.Done)
				{
					if (operationStatus == OperationStatus.NeedMoreData)
					{
						if (_bufferCount > 0 && _bufferOffset != 0)
						{
							_buffer.AsSpan(_bufferOffset, _bufferCount).CopyTo(_buffer);
						}
						_bufferOffset = 0;
						int num = 0;
						while (true)
						{
							bool flag = _bufferCount < _buffer.Length;
							if (flag)
							{
								flag = (num = await _stream.ReadAsync(new Memory<byte>(_buffer, _bufferCount, _buffer.Length - _bufferCount)).ConfigureAwait(continueOnCapturedContext: false)) > 0;
							}
							if (!flag)
							{
								break;
							}
							_bufferCount += num;
							if (_bufferCount > _buffer.Length)
							{
								throw new InvalidDataException("BrotliStream.BaseStream returned more bytes than requested in Read.");
							}
						}
						if (_bufferCount <= 0)
						{
							break;
						}
					}
					cancellationToken.ThrowIfCancellationRequested();
					operationStatus = _decoder.Decompress(_buffer.AsSpan(_bufferOffset, _bufferCount), buffer.Span, out var bytesConsumed, out var bytesWritten);
					if (operationStatus == OperationStatus.InvalidData)
					{
						throw new InvalidOperationException("Decoder ran into invalid data.");
					}
					if (bytesConsumed > 0)
					{
						_bufferOffset += bytesConsumed;
						_bufferCount -= bytesConsumed;
					}
					if (bytesWritten > 0)
					{
						totalWritten += bytesWritten;
						buffer = buffer.Slice(bytesWritten);
					}
				}
				return totalWritten;
			}
			finally
			{
				AsyncOperationCompleting();
			}
		}

		public BrotliStream(Stream stream, CompressionLevel compressionLevel)
			: this(stream, compressionLevel, leaveOpen: false)
		{
		}

		public BrotliStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen)
			: this(stream, CompressionMode.Compress, leaveOpen)
		{
			_encoder.SetQuality(BrotliUtils.GetQualityFromCompressionLevel(compressionLevel));
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			ValidateParameters(buffer, offset, count);
			WriteCore(new ReadOnlySpan<byte>(buffer, offset, count));
		}

		public override void Write(ReadOnlySpan<byte> buffer)
		{
			WriteCore(buffer);
		}

		internal void WriteCore(ReadOnlySpan<byte> buffer, bool isFinalBlock = false)
		{
			if (_mode != CompressionMode.Compress)
			{
				throw new InvalidOperationException("Can not perform Write operations on a BrotliStream constructed with CompressionMode.Decompress.");
			}
			EnsureNotDisposed();
			OperationStatus operationStatus = OperationStatus.DestinationTooSmall;
			Span<byte> destination = new Span<byte>(_buffer);
			while (operationStatus == OperationStatus.DestinationTooSmall)
			{
				int bytesConsumed = 0;
				int bytesWritten = 0;
				operationStatus = _encoder.Compress(buffer, destination, out bytesConsumed, out bytesWritten, isFinalBlock);
				if (operationStatus == OperationStatus.InvalidData)
				{
					throw new InvalidOperationException("Encoder ran into invalid data.");
				}
				if (bytesWritten > 0)
				{
					_stream.Write(destination.Slice(0, bytesWritten));
				}
				if (bytesConsumed > 0)
				{
					buffer = buffer.Slice(bytesConsumed);
				}
			}
		}

		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return System.Threading.Tasks.TaskToApm.Begin(WriteAsync(buffer, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		public override void EndWrite(IAsyncResult asyncResult)
		{
			System.Threading.Tasks.TaskToApm.End(asyncResult);
		}

		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			ValidateParameters(buffer, offset, count);
			return WriteAsync(new ReadOnlyMemory<byte>(buffer, offset, count), cancellationToken).AsTask();
		}

		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (_mode != CompressionMode.Compress)
			{
				throw new InvalidOperationException("Can not perform Write operations on a BrotliStream constructed with CompressionMode.Decompress.");
			}
			EnsureNoActiveAsyncOperation();
			EnsureNotDisposed();
			return new ValueTask(cancellationToken.IsCancellationRequested ? Task.FromCanceled<int>(cancellationToken) : WriteAsyncMemoryCore(buffer, cancellationToken));
		}

		private async Task WriteAsyncMemoryCore(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken)
		{
			AsyncOperationStarting();
			try
			{
				OperationStatus lastResult = OperationStatus.DestinationTooSmall;
				while (lastResult == OperationStatus.DestinationTooSmall)
				{
					Memory<byte> destination = new Memory<byte>(_buffer);
					int bytesConsumed = 0;
					int bytesWritten = 0;
					lastResult = _encoder.Compress(buffer, destination, out bytesConsumed, out bytesWritten, isFinalBlock: false);
					if (lastResult == OperationStatus.InvalidData)
					{
						throw new InvalidOperationException("Encoder ran into invalid data.");
					}
					if (bytesConsumed > 0)
					{
						buffer = buffer.Slice(bytesConsumed);
					}
					if (bytesWritten > 0)
					{
						await _stream.WriteAsync(new ReadOnlyMemory<byte>(_buffer, 0, bytesWritten), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					}
				}
			}
			finally
			{
				AsyncOperationCompleting();
			}
		}

		public override void Flush()
		{
			EnsureNotDisposed();
			if (_mode != CompressionMode.Compress || _encoder._state == null || _encoder._state.IsClosed)
			{
				return;
			}
			OperationStatus operationStatus = OperationStatus.DestinationTooSmall;
			Span<byte> destination = new Span<byte>(_buffer);
			while (operationStatus == OperationStatus.DestinationTooSmall)
			{
				int bytesWritten = 0;
				operationStatus = _encoder.Flush(destination, out bytesWritten);
				if (operationStatus == OperationStatus.InvalidData)
				{
					throw new InvalidDataException("Encoder ran into invalid data.");
				}
				if (bytesWritten > 0)
				{
					_stream.Write(destination.Slice(0, bytesWritten));
				}
			}
		}

		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			EnsureNoActiveAsyncOperation();
			EnsureNotDisposed();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (_mode == CompressionMode.Compress)
			{
				return FlushAsyncCore(cancellationToken);
			}
			return Task.CompletedTask;
		}

		private async Task FlushAsyncCore(CancellationToken cancellationToken)
		{
			AsyncOperationStarting();
			try
			{
				if (_encoder._state == null || _encoder._state.IsClosed)
				{
					return;
				}
				OperationStatus lastResult = OperationStatus.DestinationTooSmall;
				while (lastResult == OperationStatus.DestinationTooSmall)
				{
					Memory<byte> destination = new Memory<byte>(_buffer);
					int bytesWritten = 0;
					lastResult = _encoder.Flush(destination, out bytesWritten);
					if (lastResult == OperationStatus.InvalidData)
					{
						throw new InvalidDataException("Encoder ran into invalid data.");
					}
					if (bytesWritten > 0)
					{
						await _stream.WriteAsync(destination.Slice(0, bytesWritten), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					}
				}
			}
			finally
			{
				AsyncOperationCompleting();
			}
		}
	}
	internal static class BrotliUtils
	{
		public const int WindowBits_Min = 10;

		public const int WindowBits_Default = 22;

		public const int WindowBits_Max = 24;

		public const int Quality_Min = 0;

		public const int Quality_Default = 11;

		public const int Quality_Max = 11;

		public const int MaxInputSize = 2147483132;

		internal static int GetQualityFromCompressionLevel(CompressionLevel level)
		{
			return level switch
			{
				CompressionLevel.Optimal => 11, 
				CompressionLevel.NoCompression => 0, 
				CompressionLevel.Fastest => 1, 
				_ => (int)level, 
			};
		}
	}
	public struct BrotliDecoder : IDisposable
	{
		private SafeBrotliDecoderHandle _state;

		private bool _disposed;

		internal void InitializeDecoder()
		{
			_state = global::Interop.Brotli.BrotliDecoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (_state.IsInvalid)
			{
				throw new IOException("Failed to create BrotliDecoder instance");
			}
		}

		internal void EnsureInitialized()
		{
			EnsureNotDisposed();
			if (_state == null)
			{
				InitializeDecoder();
			}
		}

		public void Dispose()
		{
			_disposed = true;
			_state?.Dispose();
		}

		private void EnsureNotDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("BrotliDecoder", "Can not access a closed Decoder.");
			}
		}

		public unsafe OperationStatus Decompress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesConsumed, out int bytesWritten)
		{
			EnsureInitialized();
			bytesConsumed = 0;
			bytesWritten = 0;
			if (global::Interop.Brotli.BrotliDecoderIsFinished(_state))
			{
				return OperationStatus.Done;
			}
			IntPtr availableOut = (IntPtr)destination.Length;
			IntPtr availableIn = (IntPtr)source.Length;
			while ((int)availableOut > 0)
			{
				fixed (byte* reference = &MemoryMarshal.GetReference(source))
				{
					fixed (byte* reference2 = &MemoryMarshal.GetReference(destination))
					{
						byte* ptr = reference;
						byte* ptr2 = reference2;
						IntPtr totalOut;
						int num = global::Interop.Brotli.BrotliDecoderDecompressStream(_state, ref availableIn, &ptr, ref availableOut, &ptr2, out totalOut);
						if (num == 0)
						{
							return OperationStatus.InvalidData;
						}
						bytesConsumed += source.Length - (int)availableIn;
						bytesWritten += destination.Length - (int)availableOut;
						switch (num)
						{
						case 1:
							return OperationStatus.Done;
						case 3:
							return OperationStatus.DestinationTooSmall;
						}
						source = source.Slice(source.Length - (int)availableIn);
						destination = destination.Slice(destination.Length - (int)availableOut);
						if (num == 2 && source.Length == 0)
						{
							return OperationStatus.NeedMoreData;
						}
					}
				}
			}
			return OperationStatus.DestinationTooSmall;
		}

		public unsafe static bool TryDecompress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
		{
			fixed (byte* reference = &MemoryMarshal.GetReference(source))
			{
				fixed (byte* reference2 = &MemoryMarshal.GetReference(destination))
				{
					IntPtr availableOutput = (IntPtr)destination.Length;
					bool result = global::Interop.Brotli.BrotliDecoderDecompress((IntPtr)source.Length, reference, ref availableOutput, reference2);
					bytesWritten = (int)availableOutput;
					return result;
				}
			}
		}
	}
	public struct BrotliEncoder : IDisposable
	{
		internal SafeBrotliEncoderHandle _state;

		private bool _disposed;

		public BrotliEncoder(int quality, int window)
		{
			_disposed = false;
			_state = global::Interop.Brotli.BrotliEncoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (_state.IsInvalid)
			{
				throw new IOException("Failed to create BrotliEncoder instance");
			}
			SetQuality(quality);
			SetWindow(window);
		}

		internal void InitializeEncoder()
		{
			EnsureNotDisposed();
			_state = global::Interop.Brotli.BrotliEncoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (_state.IsInvalid)
			{
				throw new IOException("Failed to create BrotliEncoder instance");
			}
		}

		internal void EnsureInitialized()
		{
			EnsureNotDisposed();
			if (_state == null)
			{
				InitializeEncoder();
			}
		}

		public void Dispose()
		{
			_disposed = true;
			_state?.Dispose();
		}

		private void EnsureNotDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("BrotliEncoder", "Can not access a closed Encoder.");
			}
		}

		internal void SetQuality(int quality)
		{
			EnsureNotDisposed();
			if (_state == null || _state.IsInvalid || _state.IsClosed)
			{
				InitializeEncoder();
			}
			if (quality < 0 || quality > 11)
			{
				throw new ArgumentOutOfRangeException("quality", global::SR.Format("Provided BrotliEncoder Quality of {0} is not between the minimum value of {1} and the maximum value of {2}", quality, 0, 11));
			}
			if (!global::Interop.Brotli.BrotliEncoderSetParameter(_state, BrotliEncoderParameter.Quality, (uint)quality))
			{
				throw new InvalidOperationException(global::SR.Format("The BrotliEncoder {0} can not be changed at current encoder state.", "Quality"));
			}
		}

		internal void SetWindow(int window)
		{
			EnsureNotDisposed();
			if (_state == null || _state.IsInvalid || _state.IsClosed)
			{
				InitializeEncoder();
			}
			if (window < 10 || window > 24)
			{
				throw new ArgumentOutOfRangeException("window", global::SR.Format("Provided BrotliEncoder Window of {0} is not between the minimum value of {1} and the maximum value of {2}", window, 10, 24));
			}
			if (!global::Interop.Brotli.BrotliEncoderSetParameter(_state, BrotliEncoderParameter.LGWin, (uint)window))
			{
				throw new InvalidOperationException(global::SR.Format("The BrotliEncoder {0} can not be changed at current encoder state.", "Window"));
			}
		}

		public static int GetMaxCompressedLength(int length)
		{
			if (length < 0 || length > 2147483132)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (length == 0)
			{
				return 1;
			}
			int num = length >> 24;
			int num2 = (((length & 0xFFFFFF) > 1048576) ? 4 : 3);
			int num3 = 2 + 4 * num + num2 + 1;
			return length + num3;
		}

		internal OperationStatus Flush(Memory<byte> destination, out int bytesWritten)
		{
			return Flush(destination.Span, out bytesWritten);
		}

		public OperationStatus Flush(Span<byte> destination, out int bytesWritten)
		{
			int bytesConsumed;
			return Compress(ReadOnlySpan<byte>.Empty, destination, out bytesConsumed, out bytesWritten, BrotliEncoderOperation.Flush);
		}

		internal OperationStatus Compress(ReadOnlyMemory<byte> source, Memory<byte> destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock)
		{
			return Compress(source.Span, destination.Span, out bytesConsumed, out bytesWritten, isFinalBlock);
		}

		public OperationStatus Compress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock)
		{
			return Compress(source, destination, out bytesConsumed, out bytesWritten, isFinalBlock ? BrotliEncoderOperation.Finish : BrotliEncoderOperation.Process);
		}

		internal unsafe OperationStatus Compress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesConsumed, out int bytesWritten, BrotliEncoderOperation operation)
		{
			EnsureInitialized();
			bytesWritten = 0;
			bytesConsumed = 0;
			IntPtr availableOut = (IntPtr)destination.Length;
			IntPtr availableIn = (IntPtr)source.Length;
			while ((int)availableOut > 0)
			{
				fixed (byte* reference = &MemoryMarshal.GetReference(source))
				{
					fixed (byte* reference2 = &MemoryMarshal.GetReference(destination))
					{
						byte* ptr = reference;
						byte* ptr2 = reference2;
						if (!global::Interop.Brotli.BrotliEncoderCompressStream(_state, operation, ref availableIn, &ptr, ref availableOut, &ptr2, out var _))
						{
							return OperationStatus.InvalidData;
						}
						bytesConsumed += source.Length - (int)availableIn;
						bytesWritten += destination.Length - (int)availableOut;
						if ((int)availableOut == destination.Length && !global::Interop.Brotli.BrotliEncoderHasMoreOutput(_state) && (int)availableIn == 0)
						{
							return OperationStatus.Done;
						}
						source = source.Slice(source.Length - (int)availableIn);
						destination = destination.Slice(destination.Length - (int)availableOut);
					}
				}
			}
			return OperationStatus.DestinationTooSmall;
		}

		public static bool TryCompress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
		{
			return TryCompress(source, destination, out bytesWritten, 11, 22);
		}

		public unsafe static bool TryCompress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten, int quality, int window)
		{
			if (quality < 0 || quality > 11)
			{
				throw new ArgumentOutOfRangeException("quality", global::SR.Format("Provided BrotliEncoder Quality of {0} is not between the minimum value of {1} and the maximum value of {2}", quality, 0, 11));
			}
			if (window < 10 || window > 24)
			{
				throw new ArgumentOutOfRangeException("window", global::SR.Format("Provided BrotliEncoder Window of {0} is not between the minimum value of {1} and the maximum value of {2}", window, 10, 24));
			}
			fixed (byte* reference = &MemoryMarshal.GetReference(source))
			{
				fixed (byte* reference2 = &MemoryMarshal.GetReference(destination))
				{
					IntPtr availableOutput = (IntPtr)destination.Length;
					bool result = global::Interop.Brotli.BrotliEncoderCompress(quality, window, 0, (IntPtr)source.Length, reference, ref availableOutput, reference2);
					bytesWritten = (int)availableOutput;
					return result;
				}
			}
		}
	}
	internal enum BrotliEncoderOperation
	{
		Process,
		Flush,
		Finish,
		EmitMetadata
	}
	internal enum BrotliEncoderParameter
	{
		Mode,
		Quality,
		LGWin,
		LGBlock,
		LCModeling,
		SizeHint
	}
	internal enum BlockType
	{
		Uncompressed,
		Static,
		Dynamic
	}
	internal sealed class CopyEncoder
	{
		private const int PaddingSize = 5;

		private const int MaxUncompressedBlockSize = 65536;

		public void GetBlock(DeflateInput input, OutputBuffer output, bool isFinal)
		{
			int num = 0;
			if (input != null)
			{
				num = Math.Min(input.Count, output.FreeBytes - 5 - output.BitsInBuffer);
				if (num > 65531)
				{
					num = 65531;
				}
			}
			if (isFinal)
			{
				output.WriteBits(3, 1u);
			}
			else
			{
				output.WriteBits(3, 0u);
			}
			output.FlushBits();
			WriteLenNLen((ushort)num, output);
			if (input != null && num > 0)
			{
				output.WriteBytes(input.Buffer, input.StartIndex, num);
				input.ConsumeBytes(num);
			}
		}

		private void WriteLenNLen(ushort len, OutputBuffer output)
		{
			output.WriteUInt16(len);
			ushort value = (ushort)(~len);
			output.WriteUInt16(value);
		}
	}
	internal sealed class DeflateInput
	{
		internal readonly struct InputState
		{
			internal readonly int _count;

			internal readonly int _startIndex;

			internal InputState(int count, int startIndex)
			{
				_count = count;
				_startIndex = startIndex;
			}
		}

		internal byte[] Buffer { get; set; }

		internal int Count { get; set; }

		internal int StartIndex { get; set; }

		internal void ConsumeBytes(int n)
		{
			StartIndex += n;
			Count -= n;
		}

		internal InputState DumpState()
		{
			return new InputState(Count, StartIndex);
		}

		internal void RestoreState(InputState state)
		{
			Count = state._count;
			StartIndex = state._startIndex;
		}
	}
	internal sealed class DeflateManagedStream : Stream
	{
		internal const int DefaultBufferSize = 8192;

		private Stream _stream;

		private CompressionMode _mode;

		private bool _leaveOpen;

		private InflaterManaged _inflater;

		private DeflaterManaged _deflater;

		private byte[] _buffer;

		private int _asyncOperations;

		private IFileFormatWriter _formatWriter;

		private bool _wroteHeader;

		private bool _wroteBytes;

		public override bool CanRead
		{
			get
			{
				if (_stream == null)
				{
					return false;
				}
				if (_mode == CompressionMode.Decompress)
				{
					return _stream.CanRead;
				}
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				if (_stream == null)
				{
					return false;
				}
				if (_mode == CompressionMode.Compress)
				{
					return _stream.CanWrite;
				}
				return false;
			}
		}

		public override bool CanSeek => false;

		public override long Length
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		public override long Position
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
			set
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		internal DeflateManagedStream(Stream stream, ZipArchiveEntry.CompressionMethodValues method)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream does not support reading.", "stream");
			}
			InitializeInflater(stream, leaveOpen: false, null, method);
		}

		internal void InitializeInflater(Stream stream, bool leaveOpen, IFileFormatReader reader = null, ZipArchiveEntry.CompressionMethodValues method = ZipArchiveEntry.CompressionMethodValues.Deflate)
		{
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream does not support reading.", "stream");
			}
			_inflater = new InflaterManaged(reader, method == ZipArchiveEntry.CompressionMethodValues.Deflate64);
			_stream = stream;
			_mode = CompressionMode.Decompress;
			_leaveOpen = leaveOpen;
			_buffer = new byte[8192];
		}

		internal void SetFileFormatWriter(IFileFormatWriter writer)
		{
			if (writer != null)
			{
				_formatWriter = writer;
			}
		}

		public override void Flush()
		{
			EnsureNotDisposed();
		}

		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			EnsureNotDisposed();
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}
			return Task.FromCanceled(cancellationToken);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		public override int Read(byte[] array, int offset, int count)
		{
			EnsureDecompressionMode();
			ValidateParameters(array, offset, count);
			EnsureNotDisposed();
			int num = offset;
			int num2 = count;
			while (true)
			{
				int num3 = _inflater.Inflate(array, num, num2);
				num += num3;
				num2 -= num3;
				if (num2 == 0 || _inflater.Finished())
				{
					break;
				}
				int num4 = _stream.Read(_buffer, 0, _buffer.Length);
				if (num4 <= 0)
				{
					break;
				}
				if (num4 > _buffer.Length)
				{
					throw new InvalidDataException("Found invalid data while decoding.");
				}
				_inflater.SetInput(_buffer, 0, num4);
			}
			return count - num2;
		}

		private void ValidateParameters(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("Offset plus count is larger than the length of target array.");
			}
		}

		private void EnsureNotDisposed()
		{
			if (_stream == null)
			{
				ThrowStreamClosedException();
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowStreamClosedException()
		{
			throw new ObjectDisposedException(null, "Can not access a closed Stream.");
		}

		private void EnsureDecompressionMode()
		{
			if (_mode != CompressionMode.Decompress)
			{
				ThrowCannotReadFromDeflateManagedStreamException();
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowCannotReadFromDeflateManagedStreamException()
		{
			throw new InvalidOperationException("Reading from the compression stream is not supported.");
		}

		private void EnsureCompressionMode()
		{
			if (_mode != CompressionMode.Compress)
			{
				ThrowCannotWriteToDeflateManagedStreamException();
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowCannotWriteToDeflateManagedStreamException()
		{
			throw new InvalidOperationException("Writing to the compression stream is not supported.");
		}

		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return System.Threading.Tasks.TaskToApm.Begin(ReadAsync(buffer, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		public override int EndRead(IAsyncResult asyncResult)
		{
			return System.Threading.Tasks.TaskToApm.End<int>(asyncResult);
		}

		public override Task<int> ReadAsync(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			EnsureDecompressionMode();
			if (_asyncOperations != 0)
			{
				throw new InvalidOperationException("Only one asynchronous reader or writer is allowed time at one time.");
			}
			ValidateParameters(array, offset, count);
			EnsureNotDisposed();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			Interlocked.Increment(ref _asyncOperations);
			Task<int> task = null;
			try
			{
				int num = _inflater.Inflate(array, offset, count);
				if (num != 0)
				{
					return Task.FromResult(num);
				}
				if (_inflater.Finished())
				{
					return Task.FromResult(0);
				}
				task = _stream.ReadAsync(_buffer, 0, _buffer.Length, cancellationToken);
				if (task == null)
				{
					throw new InvalidOperationException("Stream does not support reading.");
				}
				return ReadAsyncCore(task, array, offset, count, cancellationToken);
			}
			finally
			{
				if (task == null)
				{
					Interlocked.Decrement(ref _asyncOperations);
				}
			}
		}

		private async Task<int> ReadAsyncCore(Task<int> readTask, byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			try
			{
				int num;
				while (true)
				{
					num = await readTask.ConfigureAwait(continueOnCapturedContext: false);
					EnsureNotDisposed();
					if (num <= 0)
					{
						return 0;
					}
					if (num > _buffer.Length)
					{
						throw new InvalidDataException("Found invalid data while decoding.");
					}
					cancellationToken.ThrowIfCancellationRequested();
					_inflater.SetInput(_buffer, 0, num);
					num = _inflater.Inflate(array, offset, count);
					if (num != 0 || _inflater.Finished())
					{
						break;
					}
					readTask = _stream.ReadAsync(_buffer, 0, _buffer.Length, cancellationToken);
					if (readTask == null)
					{
						throw new InvalidOperationException("Stream does not support reading.");
					}
				}
				return num;
			}
			finally
			{
				Interlocked.Decrement(ref _asyncOperations);
			}
		}

		public override void Write(byte[] array, int offset, int count)
		{
			EnsureCompressionMode();
			ValidateParameters(array, offset, count);
			EnsureNotDisposed();
			DoMaintenance(array, offset, count);
			WriteDeflaterOutput();
			_deflater.SetInput(array, offset, count);
			WriteDeflaterOutput();
		}

		private void WriteDeflaterOutput()
		{
			while (!_deflater.NeedsInput())
			{
				int deflateOutput = _deflater.GetDeflateOutput(_buffer);
				if (deflateOutput > 0)
				{
					_stream.Write(_buffer, 0, deflateOutput);
				}
			}
		}

		private void DoMaintenance(byte[] array, int offset, int count)
		{
			if (count <= 0)
			{
				return;
			}
			_wroteBytes = true;
			if (_formatWriter != null)
			{
				if (!_wroteHeader)
				{
					byte[] header = _formatWriter.GetHeader();
					_stream.Write(header, 0, header.Length);
					_wroteHeader = true;
				}
				_formatWriter.UpdateWithBytesRead(array, offset, count);
			}
		}

		private void PurgeBuffers(bool disposing)
		{
			if (!disposing || _stream == null)
			{
				return;
			}
			Flush();
			if (_mode != CompressionMode.Compress)
			{
				return;
			}
			if (_wroteBytes)
			{
				WriteDeflaterOutput();
				bool flag;
				do
				{
					flag = _deflater.Finish(_buffer, out var bytesRead);
					if (bytesRead > 0)
					{
						_stream.Write(_buffer, 0, bytesRead);
					}
				}
				while (!flag);
			}
			else
			{
				int bytesRead2;
				while (!_deflater.Finish(_buffer, out bytesRead2))
				{
				}
			}
			if (_formatWriter != null && _wroteHeader)
			{
				byte[] footer = _formatWriter.GetFooter();
				_stream.Write(footer, 0, footer.Length);
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				PurgeBuffers(disposing);
			}
			finally
			{
				try
				{
					if (disposing && !_leaveOpen && _stream != null)
					{
						_stream.Dispose();
					}
				}
				finally
				{
					_stream = null;
					try
					{
						_deflater?.Dispose();
						_inflater?.Dispose();
					}
					finally
					{
						_deflater = null;
						_inflater = null;
						base.Dispose(disposing);
					}
				}
			}
		}

		public override Task WriteAsync(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			EnsureCompressionMode();
			if (_asyncOperations != 0)
			{
				throw new InvalidOperationException("Only one asynchronous reader or writer is allowed time at one time.");
			}
			ValidateParameters(array, offset, count);
			EnsureNotDisposed();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			return WriteAsyncCore(array, offset, count, cancellationToken);
		}

		private async Task WriteAsyncCore(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			Interlocked.Increment(ref _asyncOperations);
			try
			{
				await base.WriteAsync(array, offset, count, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			finally
			{
				Interlocked.Decrement(ref _asyncOperations);
			}
		}

		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return System.Threading.Tasks.TaskToApm.Begin(WriteAsync(buffer, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		public override void EndWrite(IAsyncResult asyncResult)
		{
			System.Threading.Tasks.TaskToApm.End(asyncResult);
		}
	}
	internal sealed class DeflaterManaged : IDisposable
	{
		private enum DeflaterState
		{
			NotStarted,
			SlowDownForIncompressible1,
			SlowDownForIncompressible2,
			StartingSmallData,
			CompressThenCheck,
			CheckingForIncompressible,
			HandlingSmallData
		}

		private const int MinBlockSize = 256;

		private const int MaxHeaderFooterGoo = 120;

		private const int CleanCopySize = 8072;

		private const double BadCompressionThreshold = 1.0;

		private readonly FastEncoder _deflateEncoder;

		private readonly CopyEncoder _copyEncoder;

		private readonly DeflateInput _input;

		private readonly OutputBuffer _output;

		private DeflaterState _processingState;

		private DeflateInput _inputFromHistory;

		internal DeflaterManaged()
		{
			_deflateEncoder = new FastEncoder();
			_copyEncoder = new CopyEncoder();
			_input = new DeflateInput();
			_output = new OutputBuffer();
			_processingState = DeflaterState.NotStarted;
		}

		internal bool NeedsInput()
		{
			if (_input.Count == 0)
			{
				return _deflateEncoder.BytesInHistory == 0;
			}
			return false;
		}

		internal void SetInput(byte[] inputBuffer, int startIndex, int count)
		{
			_input.Buffer = inputBuffer;
			_input.Count = count;
			_input.StartIndex = startIndex;
			if (count > 0 && count < 256)
			{
				switch (_processingState)
				{
				case DeflaterState.NotStarted:
				case DeflaterState.CheckingForIncompressible:
					_processingState = DeflaterState.StartingSmallData;
					break;
				case DeflaterState.CompressThenCheck:
					_processingState = DeflaterState.HandlingSmallData;
					break;
				}
			}
		}

		internal int GetDeflateOutput(byte[] outputBuffer)
		{
			_output.UpdateBuffer(outputBuffer);
			switch (_processingState)
			{
			case DeflaterState.NotStarted:
			{
				DeflateInput.InputState state3 = _input.DumpState();
				OutputBuffer.BufferState state4 = _output.DumpState();
				_deflateEncoder.GetBlockHeader(_output);
				_deflateEncoder.GetCompressedData(_input, _output);
				if (!UseCompressed(_deflateEncoder.LastCompressionRatio))
				{
					_input.RestoreState(state3);
					_output.RestoreState(state4);
					_copyEncoder.GetBlock(_input, _output, isFinal: false);
					FlushInputWindows();
					_processingState = DeflaterState.CheckingForIncompressible;
				}
				else
				{
					_processingState = DeflaterState.CompressThenCheck;
				}
				break;
			}
			case DeflaterState.CompressThenCheck:
				_deflateEncoder.GetCompressedData(_input, _output);
				if (!UseCompressed(_deflateEncoder.LastCompressionRatio))
				{
					_processingState = DeflaterState.SlowDownForIncompressible1;
					_inputFromHistory = _deflateEncoder.UnprocessedInput;
				}
				break;
			case DeflaterState.SlowDownForIncompressible1:
				_deflateEncoder.GetBlockFooter(_output);
				_processingState = DeflaterState.SlowDownForIncompressible2;
				goto case DeflaterState.SlowDownForIncompressible2;
			case DeflaterState.SlowDownForIncompressible2:
				if (_inputFromHistory.Count > 0)
				{
					_copyEncoder.GetBlock(_inputFromHistory, _output, isFinal: false);
				}
				if (_inputFromHistory.Count == 0)
				{
					_deflateEncoder.FlushInput();
					_processingState = DeflaterState.CheckingForIncompressible;
				}
				break;
			case DeflaterState.CheckingForIncompressible:
			{
				DeflateInput.InputState state = _input.DumpState();
				OutputBuffer.BufferState state2 = _output.DumpState();
				_deflateEncoder.GetBlock(_input, _output, 8072);
				if (!UseCompressed(_deflateEncoder.LastCompressionRatio))
				{
					_input.RestoreState(state);
					_output.RestoreState(state2);
					_copyEncoder.GetBlock(_input, _output, isFinal: false);
					FlushInputWindows();
				}
				break;
			}
			case DeflaterState.StartingSmallData:
				_deflateEncoder.GetBlockHeader(_output);
				_processingState = DeflaterState.HandlingSmallData;
				goto case DeflaterState.HandlingSmallData;
			case DeflaterState.HandlingSmallData:
				_deflateEncoder.GetCompressedData(_input, _output);
				break;
			}
			return _output.BytesWritten;
		}

		internal bool Finish(byte[] outputBuffer, out int bytesRead)
		{
			if (_processingState == DeflaterState.NotStarted)
			{
				bytesRead = 0;
				return true;
			}
			_output.UpdateBuffer(outputBuffer);
			if (_processingState == DeflaterState.CompressThenCheck || _processingState == DeflaterState.HandlingSmallData || _processingState == DeflaterState.SlowDownForIncompressible1)
			{
				_deflateEncoder.GetBlockFooter(_output);
			}
			WriteFinal();
			bytesRead = _output.BytesWritten;
			return true;
		}

		private bool UseCompressed(double ratio)
		{
			return ratio <= 1.0;
		}

		private void FlushInputWindows()
		{
			_deflateEncoder.FlushInput();
		}

		private void WriteFinal()
		{
			_copyEncoder.GetBlock(null, _output, isFinal: true);
		}

		public void Dispose()
		{
		}
	}
	internal sealed class FastEncoder
	{
		private readonly FastEncoderWindow _inputWindow;

		private readonly Match _currentMatch;

		private double _lastCompressionRatio;

		internal int BytesInHistory => _inputWindow.BytesAvailable;

		internal DeflateInput UnprocessedInput => _inputWindow.UnprocessedInput;

		internal double LastCompressionRatio => _lastCompressionRatio;

		public FastEncoder()
		{
			_inputWindow = new FastEncoderWindow();
			_currentMatch = new Match();
		}

		internal void FlushInput()
		{
			_inputWindow.FlushWindow();
		}

		internal void GetBlock(DeflateInput input, OutputBuffer output, int maxBytesToCopy)
		{
			WriteDeflatePreamble(output);
			GetCompressedOutput(input, output, maxBytesToCopy);
			WriteEndOfBlock(output);
		}

		internal void GetCompressedData(DeflateInput input, OutputBuffer output)
		{
			GetCompressedOutput(input, output, -1);
		}

		internal void GetBlockHeader(OutputBuffer output)
		{
			WriteDeflatePreamble(output);
		}

		internal void GetBlockFooter(OutputBuffer output)
		{
			WriteEndOfBlock(output);
		}

		private void GetCompressedOutput(DeflateInput input, OutputBuffer output, int maxBytesToCopy)
		{
			int bytesWritten = output.BytesWritten;
			int num = 0;
			int num2 = BytesInHistory + input.Count;
			do
			{
				int num3 = ((input.Count < _inputWindow.FreeWindowSpace) ? input.Count : _inputWindow.FreeWindowSpace);
				if (maxBytesToCopy >= 1)
				{
					num3 = Math.Min(num3, maxBytesToCopy - num);
				}
				if (num3 > 0)
				{
					_inputWindow.CopyBytes(input.Buffer, input.StartIndex, num3);
					input.ConsumeBytes(num3);
					num += num3;
				}
				GetCompressedOutput(output);
			}
			while (SafeToWriteTo(output) && InputAvailable(input) && (maxBytesToCopy < 1 || num < maxBytesToCopy));
			int num4 = output.BytesWritten - bytesWritten;
			int num5 = BytesInHistory + input.Count;
			int num6 = num2 - num5;
			if (num4 != 0)
			{
				_lastCompressionRatio = (double)num4 / (double)num6;
			}
		}

		private void GetCompressedOutput(OutputBuffer output)
		{
			while (_inputWindow.BytesAvailable > 0 && SafeToWriteTo(output))
			{
				_inputWindow.GetNextSymbolOrMatch(_currentMatch);
				if (_currentMatch.State == MatchState.HasSymbol)
				{
					WriteChar(_currentMatch.Symbol, output);
					continue;
				}
				if (_currentMatch.State == MatchState.HasMatch)
				{
					WriteMatch(_currentMatch.Length, _currentMatch.Position, output);
					continue;
				}
				WriteChar(_currentMatch.Symbol, output);
				WriteMatch(_currentMatch.Length, _currentMatch.Position, output);
			}
		}

		private bool InputAvailable(DeflateInput input)
		{
			if (input.Count <= 0)
			{
				return BytesInHistory > 0;
			}
			return true;
		}

		private bool SafeToWriteTo(OutputBuffer output)
		{
			return output.FreeBytes > 16;
		}

		private void WriteEndOfBlock(OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[256];
			int n = (int)(num & 0x1F);
			output.WriteBits(n, num >> 5);
		}

		internal static void WriteMatch(int matchLen, int matchPos, OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[254 + matchLen];
			int num2 = (int)(num & 0x1F);
			if (num2 <= 16)
			{
				output.WriteBits(num2, num >> 5);
			}
			else
			{
				output.WriteBits(16, (num >> 5) & 0xFFFF);
				output.WriteBits(num2 - 16, num >> 21);
			}
			num = FastEncoderStatics.FastEncoderDistanceCodeInfo[FastEncoderStatics.GetSlot(matchPos)];
			output.WriteBits((int)(num & 0xF), num >> 8);
			int num3 = (int)((num >> 4) & 0xF);
			if (num3 != 0)
			{
				output.WriteBits(num3, (uint)matchPos & FastEncoderStatics.BitMask[num3]);
			}
		}

		internal static void WriteChar(byte b, OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[b];
			output.WriteBits((int)(num & 0x1F), num >> 5);
		}

		internal static void WriteDeflatePreamble(OutputBuffer output)
		{
			output.WriteBytes(FastEncoderStatics.FastEncoderTreeStructureData, 0, FastEncoderStatics.FastEncoderTreeStructureData.Length);
			output.WriteBits(9, 34u);
		}
	}
	internal static class FastEncoderStatics
	{
		internal static readonly byte[] FastEncoderTreeStructureData = new byte[98]
		{
			236, 189, 7, 96, 28, 73, 150, 37, 38, 47,
			109, 202, 123, 127, 74, 245, 74, 215, 224, 116,
			161, 8, 128, 96, 19, 36, 216, 144, 64, 16,
			236, 193, 136, 205, 230, 146, 236, 29, 105, 71,
			35, 41, 171, 42, 129, 202, 101, 86, 101, 93,
			102, 22, 64, 204, 237, 157, 188, 247, 222, 123,
			239, 189, 247, 222, 123, 239, 189, 247, 186, 59,
			157, 78, 39, 247, 223, 255, 63, 92, 102, 100,
			1, 108, 246, 206, 74, 218, 201, 158, 33, 128,
			170, 200, 31, 63, 126, 124, 31, 63
		};

		internal static readonly byte[] BFinalFastEncoderTreeStructureData = new byte[98]
		{
			237, 189, 7, 96, 28, 73, 150, 37, 38, 47,
			109, 202, 123, 127, 74, 245, 74, 215, 224, 116,
			161, 8, 128, 96, 19, 36, 216, 144, 64, 16,
			236, 193, 136, 205, 230, 146, 236, 29, 105, 71,
			35, 41, 171, 42, 129, 202, 101, 86, 101, 93,
			102, 22, 64, 204, 237, 157, 188, 247, 222, 123,
			239, 189, 247, 222, 123, 239, 189, 247, 186, 59,
			157, 78, 39, 247, 223, 255, 63, 92, 102, 100,
			1, 108, 246, 206, 74, 218, 201, 158, 33, 128,
			170, 200, 31, 63, 126, 124, 31, 63
		};

		internal static readonly uint[] FastEncoderLiteralCodeInfo = new uint[513]
		{
			55278u, 317422u, 186350u, 448494u, 120814u, 382958u, 251886u, 514030u, 14318u, 51180u,
			294u, 276462u, 145390u, 407534u, 79854u, 341998u, 210926u, 473070u, 47086u, 309230u,
			178158u, 440302u, 112622u, 374766u, 243694u, 505838u, 30702u, 292846u, 161774u, 423918u,
			6125u, 96238u, 1318u, 358382u, 9194u, 116716u, 227310u, 489454u, 137197u, 25578u,
			2920u, 3817u, 23531u, 5098u, 1127u, 7016u, 3175u, 12009u, 1896u, 5992u,
			3944u, 7913u, 8040u, 16105u, 21482u, 489u, 232u, 8681u, 4585u, 4328u,
			12777u, 13290u, 2280u, 63470u, 325614u, 6376u, 2537u, 1256u, 10729u, 5352u,
			6633u, 29674u, 56299u, 3304u, 15339u, 194542u, 14825u, 3050u, 1513u, 19434u,
			9705u, 10220u, 5609u, 13801u, 3561u, 11242u, 75756u, 48107u, 456686u, 129006u,
			42988u, 31723u, 391150u, 64491u, 260078u, 522222u, 4078u, 806u, 615u, 2663u,
			1639u, 1830u, 7400u, 744u, 3687u, 166u, 108524u, 11753u, 1190u, 359u,
			2407u, 678u, 1383u, 71661u, 1702u, 422u, 1446u, 3431u, 4840u, 2792u,
			7657u, 6888u, 2027u, 202733u, 26604u, 38893u, 169965u, 266222u, 135150u, 397294u,
			69614u, 331758u, 200686u, 462830u, 36846u, 298990u, 167918u, 430062u, 102382u, 364526u,
			233454u, 495598u, 20462u, 282606u, 151534u, 413678u, 85998u, 348142u, 217070u, 479214u,
			53230u, 315374u, 184302u, 446446u, 118766u, 380910u, 249838u, 511982u, 12270u, 274414u,
			143342u, 405486u, 77806u, 339950u, 208878u, 471022u, 45038u, 307182u, 176110u, 438254u,
			110574u, 372718u, 241646u, 503790u, 28654u, 290798u, 159726u, 421870u, 94190u, 356334u,
			225262u, 487406u, 61422u, 323566u, 192494u, 454638u, 126958u, 389102u, 258030u, 520174u,
			8174u, 270318u, 139246u, 401390u, 73710u, 335854u, 204782u, 466926u, 40942u, 303086u,
			172014u, 434158u, 106478u, 368622u, 237550u, 499694u, 24558u, 286702u, 155630u, 417774u,
			90094u, 352238u, 221166u, 483310u, 57326u, 319470u, 188398u, 450542u, 122862u, 385006u,
			253934u, 516078u, 16366u, 278510u, 147438u, 409582u, 81902u, 344046u, 212974u, 475118u,
			49134u, 311278u, 180206u, 442350u, 114670u, 376814u, 245742u, 507886u, 32750u, 294894u,
			163822u, 425966u, 98286u, 104429u, 235501u, 22509u, 360430u, 153581u, 229358u, 88045u,
			491502u, 219117u, 65518u, 327662u, 196590u, 458734u, 131054u, 132u, 3u, 388u,
			68u, 324u, 197u, 709u, 453u, 966u, 1990u, 38u, 1062u, 935u,
			2983u, 1959u, 4007u, 551u, 1575u, 2599u, 3623u, 104u, 2152u, 4200u,
			6248u, 873u, 4969u, 9065u, 13161u, 1770u, 9962u, 18154u, 26346u, 5867u,
			14059u, 22251u, 30443u, 38635u, 46827u, 55019u, 63211u, 15852u, 32236u, 48620u,
			65004u, 81388u, 97772u, 114156u, 130540u, 27629u, 60397u, 93165u, 125933u, 158701u,
			191469u, 224237u, 257005u, 1004u, 17388u, 33772u, 50156u, 66540u, 82924u, 99308u,
			115692u, 7150u, 39918u, 72686u, 105454u, 138222u, 170990u, 203758u, 236526u, 269294u,
			302062u, 334830u, 367598u, 400366u, 433134u, 465902u, 498670u, 92144u, 223216u, 354288u,
			485360u, 616432u, 747504u, 878576u, 1009648u, 1140720u, 1271792u, 1402864u, 1533936u, 1665008u,
			1796080u, 1927152u, 2058224u, 34799u, 100335u, 165871u, 231407u, 296943u, 362479u, 428015u,
			493551u, 559087u, 624623u, 690159u, 755695u, 821231u, 886767u, 952303u, 1017839u, 59376u,
			190448u, 321520u, 452592u, 583664u, 714736u, 845808u, 976880u, 1107952u, 1239024u, 1370096u,
			1501168u, 1632240u, 1763312u, 1894384u, 2025456u, 393203u, 917491u, 1441779u, 1966067u, 2490355u,
			3014643u, 3538931u, 4063219u, 4587507u, 5111795u, 5636083u, 6160371u, 6684659u, 7208947u, 7733235u,
			8257523u, 8781811u, 9306099u, 9830387u, 10354675u, 10878963u, 11403251u, 11927539u, 12451827u, 12976115u,
			13500403u, 14024691u, 14548979u, 15073267u, 15597555u, 16121843u, 16646131u, 262131u, 786419u, 1310707u,
			1834995u, 2359283u, 2883571u, 3407859u, 3932147u, 4456435u, 4980723u, 5505011u, 6029299u, 6553587u,
			7077875u, 7602163u, 8126451u, 8650739u, 9175027u, 9699315u, 10223603u, 10747891u, 11272179u, 11796467u,
			12320755u, 12845043u, 13369331u, 13893619u, 14417907u, 14942195u, 15466483u, 15990771u, 16515059u, 524275u,
			1048563u, 1572851u, 2097139u, 2621427u, 3145715u, 3670003u, 4194291u, 4718579u, 5242867u, 5767155u,
			6291443u, 6815731u, 7340019u, 7864307u, 8388595u, 8912883u, 9437171u, 9961459u, 10485747u, 11010035u,
			11534323u, 12058611u, 12582899u, 13107187u, 13631475u, 14155763u, 14680051u, 15204339u, 15728627u, 16252915u,
			16777203u, 124913u, 255985u, 387057u, 518129u, 649201u, 780273u, 911345u, 1042417u, 1173489u,
			1304561u, 1435633u, 1566705u, 1697777u, 1828849u, 1959921u, 2090993u, 2222065u, 2353137u, 2484209u,
			2615281u, 2746353u, 2877425u, 3008497u, 3139569u, 3270641u, 3401713u, 3532785u, 3663857u, 3794929u,
			3926001u, 4057073u, 18411u
		};

		internal static readonly uint[] FastEncoderDistanceCodeInfo = new uint[32]
		{
			3846u, 130826u, 261899u, 524043u, 65305u, 16152u, 48936u, 32552u, 7991u, 24375u,
			3397u, 12102u, 84u, 7509u, 2148u, 869u, 1140u, 4981u, 3204u, 644u,
			2708u, 1684u, 3748u, 420u, 2484u, 2997u, 1476u, 7109u, 2005u, 6101u,
			0u, 256u
		};

		internal static readonly uint[] BitMask = new uint[16]
		{
			0u, 1u, 3u, 7u, 15u, 31u, 63u, 127u, 255u, 511u,
			1023u, 2047u, 4095u, 8191u, 16383u, 32767u
		};

		internal static readonly byte[] ExtraLengthBits = new byte[29]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 2, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 5, 5, 5, 5, 0
		};

		internal static readonly byte[] ExtraDistanceBits = new byte[32]
		{
			0, 0, 0, 0, 1, 1, 2, 2, 3, 3,
			4, 4, 5, 5, 6, 6, 7, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 13, 13,
			0, 0
		};

		internal const int NumChars = 256;

		internal const int NumLengthBaseCodes = 29;

		internal const int NumDistBaseCodes = 30;

		internal const uint FastEncoderPostTreeBitBuf = 34u;

		internal const int FastEncoderPostTreeBitCount = 9;

		internal const uint NoCompressionHeader = 0u;

		internal const int NoCompressionHeaderBitCount = 3;

		internal const uint BFinalNoCompressionHeader = 1u;

		internal const int BFinalNoCompressionHeaderBitCount = 3;

		internal const int MaxCodeLen = 16;

		private static readonly byte[] s_distLookup = CreateDistanceLookup();

		private static byte[] CreateDistanceLookup()
		{
			byte[] array = new byte[512];
			int num = 0;
			int i;
			for (i = 0; i < 16; i++)
			{
				for (int j = 0; j < 1 << (int)ExtraDistanceBits[i]; j++)
				{
					array[num++] = (byte)i;
				}
			}
			num >>= 7;
			for (; i < 30; i++)
			{
				for (int k = 0; k < 1 << ExtraDistanceBits[i] - 7; k++)
				{
					array[256 + num++] = (byte)i;
				}
			}
			return array;
		}

		internal static int GetSlot(int pos)
		{
			return s_distLookup[(pos < 256) ? pos : (256 + (pos >> 7))];
		}

		public static uint BitReverse(uint code, int length)
		{
			uint num = 0u;
			do
			{
				num |= code & 1;
				num <<= 1;
				code >>= 1;
			}
			while (--length > 0);
			return num >> 1;
		}
	}
	internal sealed class FastEncoderWindow
	{
		private byte[] _window;

		private int _bufPos;

		private int _bufEnd;

		private const int FastEncoderHashShift = 4;

		private const int FastEncoderHashtableSize = 2048;

		private const int FastEncoderHashMask = 2047;

		private const int FastEncoderWindowSize = 8192;

		private const int FastEncoderWindowMask = 8191;

		private const int FastEncoderMatch3DistThreshold = 16384;

		internal const int MaxMatch = 258;

		internal const int MinMatch = 3;

		private const int SearchDepth = 32;

		private const int GoodLength = 4;

		private const int NiceLength = 32;

		private const int LazyMatchThreshold = 6;

		private ushort[] _prev;

		private ushort[] _lookup;

		public int BytesAvailable => _bufEnd - _bufPos;

		public DeflateInput UnprocessedInput => new DeflateInput
		{
			Buffer = _window,
			StartIndex = _bufPos,
			Count = _bufEnd - _bufPos
		};

		public int FreeWindowSpace => 16384 - _bufEnd;

		public FastEncoderWindow()
		{
			ResetWindow();
		}

		public void FlushWindow()
		{
			ResetWindow();
		}

		private void ResetWindow()
		{
			_window = new byte[16646];
			_prev = new ushort[8450];
			_lookup = new ushort[2048];
			_bufPos = 8192;
			_bufEnd = _bufPos;
		}

		public void CopyBytes(byte[] inputBuffer, int startIndex, int count)
		{
			Array.Copy(inputBuffer, startIndex, _window, _bufEnd, count);
			_bufEnd += count;
		}

		public void MoveWindows()
		{
			Array.Copy(_window, _bufPos - 8192, _window, 0, 8192);
			for (int i = 0; i < 2048; i++)
			{
				int num = _lookup[i] - 8192;
				if (num <= 0)
				{
					_lookup[i] = 0;
				}
				else
				{
					_lookup[i] = (ushort)num;
				}
			}
			for (int i = 0; i < 8192; i++)
			{
				long num2 = (long)_prev[i] - 8192L;
				if (num2 <= 0)
				{
					_prev[i] = 0;
				}
				else
				{
					_prev[i] = (ushort)num2;
				}
			}
			_bufPos = 8192;
			_bufEnd = _bufPos;
		}

		private uint HashValue(uint hash, byte b)
		{
			return (hash << 4) ^ b;
		}

		private uint InsertString(ref uint hash)
		{
			hash = HashValue(hash, _window[_bufPos + 2]);
			uint num = _lookup[hash & 0x7FF];
			_lookup[hash & 0x7FF] = (ushort)_bufPos;
			_prev[_bufPos & 0x1FFF] = (ushort)num;
			return num;
		}

		private void InsertStrings(ref uint hash, int matchLen)
		{
			if (_bufEnd - _bufPos <= matchLen)
			{
				_bufPos += matchLen - 1;
				return;
			}
			while (--matchLen > 0)
			{
				InsertString(ref hash);
				_bufPos++;
			}
		}

		internal bool GetNextSymbolOrMatch(Match match)
		{
			uint hash = HashValue(0u, _window[_bufPos]);
			hash = HashValue(hash, _window[_bufPos + 1]);
			int matchPos = 0;
			int num;
			if (_bufEnd - _bufPos <= 3)
			{
				num = 0;
			}
			else
			{
				int num2 = (int)InsertString(ref hash);
				if (num2 != 0)
				{
					num = FindMatch(num2, out matchPos, 32, 32);
					if (_bufPos + num > _bufEnd)
					{
						num = _bufEnd - _bufPos;
					}
				}
				else
				{
					num = 0;
				}
			}
			if (num < 3)
			{
				match.State = MatchState.HasSymbol;
				match.Symbol = _window[_bufPos];
				_bufPos++;
			}
			else
			{
				_bufPos++;
				if (num <= 6)
				{
					int matchPos2 = 0;
					int num3 = (int)InsertString(ref hash);
					int num4;
					if (num3 != 0)
					{
						num4 = FindMatch(num3, out matchPos2, (num < 4) ? 32 : 8, 32);
						if (_bufPos + num4 > _bufEnd)
						{
							num4 = _bufEnd - _bufPos;
						}
					}
					else
					{
						num4 = 0;
					}
					if (num4 > num)
					{
						match.State = MatchState.HasSymbolAndMatch;
						match.Symbol = _window[_bufPos - 1];
						match.Position = matchPos2;
						match.Length = num4;
						_bufPos++;
						num = num4;
						InsertStrings(ref hash, num);
					}
					else
					{
						match.State = MatchState.HasMatch;
						match.Position = matchPos;
						match.Length = num;
						num--;
						_bufPos++;
						InsertStrings(ref hash, num);
					}
				}
				else
				{
					match.State = MatchState.HasMatch;
					match.Position = matchPos;
					match.Length = num;
					InsertStrings(ref hash, num);
				}
			}
			if (_bufPos == 16384)
			{
				MoveWindows();
			}
			return true;
		}

		private int FindMatch(int search, out int matchPos, int searchDepth, int niceLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = _bufPos - 8192;
			byte b = _window[_bufPos];
			while (search > num3)
			{
				if (_window[search + num] == b)
				{
					int i;
					for (i = 0; i < 258 && _window[_bufPos + i] == _window[search + i]; i++)
					{
					}
					if (i > num)
					{
						num = i;
						num2 = search;
						if (i > 32)
						{
							break;
						}
						b = _window[_bufPos + i];
					}
				}
				if (--searchDepth == 0)
				{
					break;
				}
				search = _prev[search & 0x1FFF];
			}
			matchPos = _bufPos - num2 - 1;
			if (num == 3 && matchPos >= 16384)
			{
				return 0;
			}
			return num;
		}

		[Conditional("DEBUG")]
		private void DebugAssertVerifyHashes()
		{
		}

		[Conditional("DEBUG")]
		private void DebugAssertRecalculatedHashesAreEqual(int position1, int position2, string message = "")
		{
		}
	}
	internal interface IFileFormatWriter
	{
		byte[] GetHeader();

		void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy);

		byte[] GetFooter();
	}
	internal interface IFileFormatReader
	{
		bool ReadHeader(InputBuffer input);

		bool ReadFooter(InputBuffer input);

		void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy);

		void Validate();
	}
	internal sealed class HuffmanTree
	{
		internal const int MaxLiteralTreeElements = 288;

		internal const int MaxDistTreeElements = 32;

		internal const int EndOfBlockCode = 256;

		internal const int NumberOfCodeLengthTreeElements = 19;

		private readonly int _tableBits;

		private readonly short[] _table;

		private readonly short[] _left;

		private readonly short[] _right;

		private readonly byte[] _codeLengthArray;

		private readonly int _tableMask;

		public static HuffmanTree StaticLiteralLengthTree { get; } = new HuffmanTree(GetStaticLiteralTreeLength());

		public static HuffmanTree StaticDistanceTree { get; } = new HuffmanTree(GetStaticDistanceTreeLength());

		public HuffmanTree(byte[] codeLengths)
		{
			_codeLengthArray = codeLengths;
			if (_codeLengthArray.Length == 288)
			{
				_tableBits = 9;
			}
			else
			{
				_tableBits = 7;
			}
			_tableMask = (1 << _tableBits) - 1;
			_table = new short[1 << _tableBits];
			_left = new short[2 * _codeLengthArray.Length];
			_right = new short[2 * _codeLengthArray.Length];
			CreateTable();
		}

		private static byte[] GetStaticLiteralTreeLength()
		{
			byte[] array = new byte[288];
			for (int i = 0; i <= 143; i++)
			{
				array[i] = 8;
			}
			for (int j = 144; j <= 255; j++)
			{
				array[j] = 9;
			}
			for (int k = 256; k <= 279; k++)
			{
				array[k] = 7;
			}
			for (int l = 280; l <= 287; l++)
			{
				array[l] = 8;
			}
			return array;
		}

		private static byte[] GetStaticDistanceTreeLength()
		{
			byte[] array = new byte[32];
			for (int i = 0; i < 32; i++)
			{
				array[i] = 5;
			}
			return array;
		}

		private uint[] CalculateHuffmanCode()
		{
			uint[] array = new uint[17];
			byte[] codeLengthArray = _codeLengthArray;
			foreach (int num in codeLengthArray)
			{
				array[num]++;
			}
			array[0] = 0u;
			uint[] array2 = new uint[17];
			uint num2 = 0u;
			for (int j = 1; j <= 16; j++)
			{
				num2 = (array2[j] = num2 + array[j - 1] << 1);
			}
			uint[] array3 = new uint[288];
			for (int k = 0; k < _codeLengthArray.Length; k++)
			{
				int num3 = _codeLengthArray[k];
				if (num3 > 0)
				{
					array3[k] = FastEncoderStatics.BitReverse(array2[num3], num3);
					array2[num3]++;
				}
			}
			return array3;
		}

		private void CreateTable()
		{
			uint[] array = CalculateHuffmanCode();
			short num = (short)_codeLengthArray.Length;
			for (int i = 0; i < _codeLengthArray.Length; i++)
			{
				int num2 = _codeLengthArray[i];
				if (num2 <= 0)
				{
					continue;
				}
				int num3 = (int)array[i];
				if (num2 <= _tableBits)
				{
					int num4 = 1 << num2;
					if (num3 >= num4)
					{
						throw new InvalidDataException("Failed to construct a huffman tree using the length array. The stream might be corrupted.");
					}
					int num5 = 1 << _tableBits - num2;
					for (int j = 0; j < num5; j++)
					{
						_table[num3] = (short)i;
						num3 += num4;
					}
					continue;
				}
				int num6 = num2 - _tableBits;
				int num7 = 1 << _tableBits;
				int num8 = num3 & ((1 << _tableBits) - 1);
				short[] array2 = _table;
				do
				{
					short num9 = array2[num8];
					if (num9 == 0)
					{
						array2[num8] = (short)(-num);
						num9 = (short)(-num);
						num++;
					}
					if (num9 > 0)
					{
						throw new InvalidDataException("Failed to construct a huffman tree using the length array. The stream might be corrupted.");
					}
					array2 = (((num3 & num7) != 0) ? _right : _left);
					num8 = -num9;
					num7 <<= 1;
					num6--;
				}
				while (num6 != 0);
				array2[num8] = (short)i;
			}
		}

		public int GetNextSymbol(InputBuffer input)
		{
			uint num = input.TryLoad16Bits();
			if (input.AvailableBits == 0)
			{
				return -1;
			}
			int num2 = _table[num & _tableMask];
			if (num2 < 0)
			{
				uint num3 = (uint)(1 << _tableBits);
				do
				{
					num2 = -num2;
					num2 = (((num & num3) != 0) ? _right[num2] : _left[num2]);
					num3 <<= 1;
				}
				while (num2 < 0);
			}
			int num4 = _codeLengthArray[num2];
			if (num4 <= 0)
			{
				throw new InvalidDataException("Failed to construct a huffman tree using the length array. The stream might be corrupted.");
			}
			if (num4 > input.AvailableBits)
			{
				return -1;
			}
			input.SkipBits(num4);
			return num2;
		}
	}
	internal sealed class InflaterManaged
	{
		private static readonly byte[] s_extraLengthBits = new byte[29]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 2, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 5, 5, 5, 5, 16
		};

		private static readonly int[] s_lengthBase = new int[29]
		{
			3, 4, 5, 6, 7, 8, 9, 10, 11, 13,
			15, 17, 19, 23, 27, 31, 35, 43, 51, 59,
			67, 83, 99, 115, 131, 163, 195, 227, 3
		};

		private static readonly int[] s_distanceBasePosition = new int[32]
		{
			1, 2, 3, 4, 5, 7, 9, 13, 17, 25,
			33, 49, 65, 97, 129, 193, 257, 385, 513, 769,
			1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577,
			32769, 49153
		};

		private static readonly byte[] s_codeOrder = new byte[19]
		{
			16, 17, 18, 0, 8, 7, 9, 6, 10, 5,
			11, 4, 12, 3, 13, 2, 14, 1, 15
		};

		private static readonly byte[] s_staticDistanceTreeTable = new byte[32]
		{
			0, 16, 8, 24, 4, 20, 12, 28, 2, 18,
			10, 26, 6, 22, 14, 30, 1, 17, 9, 25,
			5, 21, 13, 29, 3, 19, 11, 27, 7, 23,
			15, 31
		};

		private readonly OutputWindow _output;

		private readonly InputBuffer _input;

		private HuffmanTree _literalLengthTree;

		private HuffmanTree _distanceTree;

		private InflaterState _state;

		private bool _hasFormatReader;

		private int _bfinal;

		private BlockType _blockType;

		private readonly byte[] _blockLengthBuffer = new byte[4];

		private int _blockLength;

		private int _length;

		private int _distanceCode;

		private int _extraBits;

		private int _loopCounter;

		private int _literalLengthCodeCount;

		private int _distanceCodeCount;

		private int _codeLengthCodeCount;

		private int _codeArraySize;

		private int _lengthCode;

		private readonly byte[] _codeList;

		private readonly byte[] _codeLengthTreeCodeLength;

		private readonly bool _deflate64;

		private HuffmanTree _codeLengthTree;

		private IFileFormatReader _formatReader;

		public int AvailableOutput => _output.AvailableBytes;

		internal InflaterManaged(IFileFormatReader reader, bool deflate64)
		{
			_output = new OutputWindow();
			_input = new InputBuffer();
			_codeList = new byte[320];
			_codeLengthTreeCodeLength = new byte[19];
			_deflate64 = deflate64;
			if (reader != null)
			{
				_formatReader = reader;
				_hasFormatReader = true;
			}
			Reset();
		}

		private void Reset()
		{
			_state = ((!_hasFormatReader) ? InflaterState.ReadingBFinal : InflaterState.ReadingHeader);
		}

		public void SetInput(byte[] inputBytes, int offset, int length)
		{
			_input.SetInput(inputBytes, offset, length);
		}

		public bool Finished()
		{
			if (_state != InflaterState.Done)
			{
				return _state == InflaterState.VerifyingFooter;
			}
			return true;
		}

		public int Inflate(byte[] bytes, int offset, int length)
		{
			int num = 0;
			do
			{
				int num2 = _output.CopyTo(bytes, offset, length);
				if (num2 > 0)
				{
					if (_hasFormatReader)
					{
						_formatReader.UpdateWithBytesRead(bytes, offset, num2);
					}
					offset += num2;
					num += num2;
					length -= num2;
				}
			}
			while (length != 0 && !Finished() && Decode());
			if (_state == InflaterState.VerifyingFooter && _output.AvailableBytes == 0)
			{
				_formatReader.Validate();
			}
			return num;
		}

		private bool Decode()
		{
			bool end_of_block = false;
			bool flag = false;
			if (Finished())
			{
				return true;
			}
			if (_hasFormatReader)
			{
				if (_state == InflaterState.ReadingHeader)
				{
					if (!_formatReader.ReadHeader(_input))
					{
						return false;
					}
					_state = InflaterState.ReadingBFinal;
				}
				else if (_state == InflaterState.StartReadingFooter || _state == InflaterState.ReadingFooter)
				{
					if (!_formatReader.ReadFooter(_input))
					{
						return false;
					}
					_state = InflaterState.VerifyingFooter;
					return true;
				}
			}
			if (_state == InflaterState.ReadingBFinal)
			{
				if (!_input.EnsureBitsAvailable(1))
				{
					return false;
				}
				_bfinal = _input.GetBits(1);
				_state = InflaterState.ReadingBType;
			}
			if (_state == InflaterState.ReadingBType)
			{
				if (!_input.EnsureBitsAvailable(2))
				{
					_state = InflaterState.ReadingBType;
					return false;
				}
				_blockType = (BlockType)_input.GetBits(2);
				if (_blockType == BlockType.Dynamic)
				{
					_state = InflaterState.ReadingNumLitCodes;
				}
				else if (_blockType == BlockType.Static)
				{
					_literalLengthTree = HuffmanTree.StaticLiteralLengthTree;
					_distanceTree = HuffmanTree.StaticDistanceTree;
					_state = InflaterState.DecodeTop;
				}
				else
				{
					if (_blockType != BlockType.Uncompressed)
					{
						throw new InvalidDataException("Unknown block type. Stream might be corrupted.");
					}
					_state = InflaterState.UncompressedAligning;
				}
			}
			if (_blockType == BlockType.Dynamic)
			{
				flag = ((_state >= InflaterState.DecodeTop) ? DecodeBlock(out end_of_block) : DecodeDynamicBlockHeader());
			}
			else if (_blockType == BlockType.Static)
			{
				flag = DecodeBlock(out end_of_block);
			}
			else
			{
				if (_blockType != BlockType.Uncompressed)
				{
					throw new InvalidDataException("Unknown block type. Stream might be corrupted.");
				}
				flag = DecodeUncompressedBlock(out end_of_block);
			}
			if (end_of_block && _bfinal != 0)
			{
				if (_hasFormatReader)
				{
					_state = InflaterState.StartReadingFooter;
				}
				else
				{
					_state = InflaterState.Done;
				}
			}
			return flag;
		}

		private bool DecodeUncompressedBlock(out bool end_of_block)
		{
			end_of_block = false;
			while (true)
			{
				switch (_state)
				{
				case InflaterState.UncompressedAligning:
					_input.SkipToByteBoundary();
					_state = InflaterState.UncompressedByte1;
					goto case InflaterState.UncompressedByte1;
				case InflaterState.UncompressedByte1:
				case InflaterState.UncompressedByte2:
				case InflaterState.UncompressedByte3:
				case InflaterState.UncompressedByte4:
				{
					int bits = _input.GetBits(8);
					if (bits < 0)
					{
						return false;
					}
					_blockLengthBuffer[(int)(_state - 16)] = (byte)bits;
					if (_state == InflaterState.UncompressedByte4)
					{
						_blockLength = _blockLengthBuffer[0] + _blockLengthBuffer[1] * 256;
						int num2 = _blockLengthBuffer[2] + _blockLengthBuffer[3] * 256;
						if ((ushort)_blockLength != (ushort)(~num2))
						{
							throw new InvalidDataException("Block length does not match with its complement.");
						}
					}
					break;
				}
				case InflaterState.DecodingUncompressed:
				{
					int num = _output.CopyFrom(_input, _blockLength);
					_blockLength -= num;
					if (_blockLength == 0)
					{
						_state = InflaterState.ReadingBFinal;
						end_of_block = true;
						return true;
					}
					if (_output.FreeBytes == 0)
					{
						return true;
					}
					return false;
				}
				default:
					throw new InvalidDataException("Decoder is in some unknown state. This might be caused by corrupted data.");
				}
				_state++;
			}
		}

		private bool DecodeBlock(out bool end_of_block_code_seen)
		{
			end_of_block_code_seen = false;
			int num = _output.FreeBytes;
			while (num > 65536)
			{
				switch (_state)
				{
				case InflaterState.DecodeTop:
				{
					int nextSymbol = _literalLengthTree.GetNextSymbol(_input);
					if (nextSymbol < 0)
					{
						return false;
					}
					if (nextSymbol < 256)
					{
						_output.Write((byte)nextSymbol);
						num--;
						break;
					}
					if (nextSymbol == 256)
					{
						end_of_block_code_seen = true;
						_state = InflaterState.ReadingBFinal;
						return true;
					}
					nextSymbol -= 257;
					if (nextSymbol < 8)
					{
						nextSymbol += 3;
						_extraBits = 0;
					}
					else if (!_deflate64 && nextSymbol == 28)
					{
						nextSymbol = 258;
						_extraBits = 0;
					}
					else
					{
						if (nextSymbol < 0 || nextSymbol >= s_extraLengthBits.Length)
						{
							throw new InvalidDataException("Found invalid data while decoding.");
						}
						_extraBits = s_extraLengthBits[nextSymbol];
					}
					_length = nextSymbol;
					goto case InflaterState.HaveInitialLength;
				}
				case InflaterState.HaveInitialLength:
					if (_extraBits > 0)
					{
						_state = InflaterState.HaveInitialLength;
						int bits2 = _input.GetBits(_extraBits);
						if (bits2 < 0)
						{
							return false;
						}
						if (_length < 0 || _length >= s_lengthBase.Length)
						{
							throw new InvalidDataException("Found invalid data while decoding.");
						}
						_length = s_lengthBase[_length] + bits2;
					}
					_state = InflaterState.HaveFullLength;
					goto case InflaterState.HaveFullLength;
				case InflaterState.HaveFullLength:
					if (_blockType == BlockType.Dynamic)
					{
						_distanceCode = _distanceTree.GetNextSymbol(_input);
					}
					else
					{
						_distanceCode = _input.GetBits(5);
						if (_distanceCode >= 0)
						{
							_distanceCode = s_staticDistanceTreeTable[_distanceCode];
						}
					}
					if (_distanceCode < 0)
					{
						return false;
					}
					_state = InflaterState.HaveDistCode;
					goto case InflaterState.HaveDistCode;
				case InflaterState.HaveDistCode:
				{
					int distance;
					if (_distanceCode > 3)
					{
						_extraBits = _distanceCode - 2 >> 1;
						int bits = _input.GetBits(_extraBits);
						if (bits < 0)
						{
							return false;
						}
						distance = s_distanceBasePosition[_distanceCode] + bits;
					}
					else
					{
						distance = _distanceCode + 1;
					}
					_output.WriteLengthDistance(_length, distance);
					num -= _length;
					_state = InflaterState.DecodeTop;
					break;
				}
				default:
					throw new InvalidDataException("Decoder is in some unknown state. This might be caused by corrupted data.");
				}
			}
			return true;
		}

		private bool DecodeDynamicBlockHeader()
		{
			switch (_state)
			{
			case InflaterState.ReadingNumLitCodes:
				_literalLengthCodeCount = _input.GetBits(5);
				if (_literalLengthCodeCount < 0)
				{
					return false;
				}
				_literalLengthCodeCount += 257;
				_state = InflaterState.ReadingNumDistCodes;
				goto case InflaterState.ReadingNumDistCodes;
			case InflaterState.ReadingNumDistCodes:
				_distanceCodeCount = _input.GetBits(5);
				if (_distanceCodeCount < 0)
				{
					return false;
				}
				_distanceCodeCount++;
				_state = InflaterState.ReadingNumCodeLengthCodes;
				goto case InflaterState.ReadingNumCodeLengthCodes;
			case InflaterState.ReadingNumCodeLengthCodes:
				_codeLengthCodeCount = _input.GetBits(4);
				if (_codeLengthCodeCount < 0)
				{
					return false;
				}
				_codeLengthCodeCount += 4;
				_loopCounter = 0;
				_state = InflaterState.ReadingCodeLengthCodes;
				goto case InflaterState.ReadingCodeLengthCodes;
			case InflaterState.ReadingCodeLengthCodes:
			{
				while (_loopCounter < _codeLengthCodeCount)
				{
					int bits = _input.GetBits(3);
					if (bits < 0)
					{
						return false;
					}
					_codeLengthTreeCodeLength[s_codeOrder[_loopCounter]] = (byte)bits;
					_loopCounter++;
				}
				for (int l = _codeLengthCodeCount; l < s_codeOrder.Length; l++)
				{
					_codeLengthTreeCodeLength[s_codeOrder[l]] = 0;
				}
				_codeLengthTree = new HuffmanTree(_codeLengthTreeCodeLength);
				_codeArraySize = _literalLengthCodeCount + _distanceCodeCount;
				_loopCounter = 0;
				_state = InflaterState.ReadingTreeCodesBefore;
				goto case InflaterState.ReadingTreeCodesBefore;
			}
			case InflaterState.ReadingTreeCodesBefore:
			case InflaterState.ReadingTreeCodesAfter:
			{
				while (_loopCounter < _codeArraySize)
				{
					if (_state == InflaterState.ReadingTreeCodesBefore && (_lengthCode = _codeLengthTree.GetNextSymbol(_input)) < 0)
					{
						return false;
					}
					if (_lengthCode <= 15)
					{
						_codeList[_loopCounter++] = (byte)_lengthCode;
					}
					else if (_lengthCode == 16)
					{
						if (!_input.EnsureBitsAvailable(2))
						{
							_state = InflaterState.ReadingTreeCodesAfter;
							return false;
						}
						if (_loopCounter == 0)
						{
							throw new InvalidDataException();
						}
						byte b = _codeList[_loopCounter - 1];
						int num = _input.GetBits(2) + 3;
						if (_loopCounter + num > _codeArraySize)
						{
							throw new InvalidDataException();
						}
						for (int i = 0; i < num; i++)
						{
							_codeList[_loopCounter++] = b;
						}
					}
					else if (_lengthCode == 17)
					{
						if (!_input.EnsureBitsAvailable(3))
						{
							_state = InflaterState.ReadingTreeCodesAfter;
							return false;
						}
						int num = _input.GetBits(3) + 3;
						if (_loopCounter + num > _codeArraySize)
						{
							throw new InvalidDataException();
						}
						for (int j = 0; j < num; j++)
						{
							_codeList[_loopCounter++] = 0;
						}
					}
					else
					{
						if (!_input.EnsureBitsAvailable(7))
						{
							_state = InflaterState.ReadingTreeCodesAfter;
							return false;
						}
						int num = _input.GetBits(7) + 11;
						if (_loopCounter + num > _codeArraySize)
						{
							throw new InvalidDataException();
						}
						for (int k = 0; k < num; k++)
						{
							_codeList[_loopCounter++] = 0;
						}
					}
					_state = InflaterState.ReadingTreeCodesBefore;
				}
				byte[] array = new byte[288];
				byte[] array2 = new byte[32];
				Array.Copy(_codeList, 0, array, 0, _literalLengthCodeCount);
				Array.Copy(_codeList, _literalLengthCodeCount, array2, 0, _distanceCodeCount);
				if (array[256] == 0)
				{
					throw new InvalidDataException();
				}
				_literalLengthTree = new HuffmanTree(array);
				_distanceTree = new HuffmanTree(array2);
				_state = InflaterState.DecodeTop;
				return true;
			}
			default:
				throw new InvalidDataException("Decoder is in some unknown state. This might be caused by corrupted data.");
			}
		}

		public void Dispose()
		{
		}
	}
	internal enum InflaterState
	{
		ReadingHeader = 0,
		ReadingBFinal = 2,
		ReadingBType = 3,
		ReadingNumLitCodes = 4,
		ReadingNumDistCodes = 5,
		ReadingNumCodeLengthCodes = 6,
		ReadingCodeLengthCodes = 7,
		ReadingTreeCodesBefore = 8,
		ReadingTreeCodesAfter = 9,
		DecodeTop = 10,
		HaveInitialLength = 11,
		HaveFullLength = 12,
		HaveDistCode = 13,
		UncompressedAligning = 15,
		UncompressedByte1 = 16,
		UncompressedByte2 = 17,
		UncompressedByte3 = 18,
		UncompressedByte4 = 19,
		DecodingUncompressed = 20,
		StartReadingFooter = 21,
		ReadingFooter = 22,
		VerifyingFooter = 23,
		Done = 24
	}
	internal sealed class InputBuffer
	{
		private byte[] _buffer;

		private int _start;

		private int _end;

		private uint _bitBuffer;

		private int _bitsInBuffer;

		public int AvailableBits => _bitsInBuffer;

		public int AvailableBytes => _end - _start + _bitsInBuffer / 8;

		public bool EnsureBitsAvailable(int count)
		{
			if (_bitsInBuffer < count)
			{
				if (NeedsInput())
				{
					return false;
				}
				_bitBuffer |= (uint)(_buffer[_start++] << _bitsInBuffer);
				_bitsInBuffer += 8;
				if (_bitsInBuffer < count)
				{
					if (NeedsInput())
					{
						return false;
					}
					_bitBuffer |= (uint)(_buffer[_start++] << _bitsInBuffer);
					_bitsInBuffer += 8;
				}
			}
			return true;
		}

		public uint TryLoad16Bits()
		{
			if (_bitsInBuffer < 8)
			{
				if (_start < _end)
				{
					_bitBuffer |= (uint)(_buffer[_start++] << _bitsInBuffer);
					_bitsInBuffer += 8;
				}
				if (_start < _end)
				{
					_bitBuffer |= (uint)(_buffer[_start++] << _bitsInBuffer);
					_bitsInBuffer += 8;
				}
			}
			else if (_bitsInBuffer < 16 && _start < _end)
			{
				_bitBuffer |= (uint)(_buffer[_start++] << _bitsInBuffer);
				_bitsInBuffer += 8;
			}
			return _bitBuffer;
		}

		private uint GetBitMask(int count)
		{
			return (uint)((1 << count) - 1);
		}

		public int GetBits(int count)
		{
			if (!EnsureBitsAvailable(count))
			{
				return -1;
			}
			uint result = _bitBuffer & GetBitMask(count);
			_bitBuffer >>= count;
			_bitsInBuffer -= count;
			return (int)result;
		}

		public int CopyTo(byte[] output, int offset, int length)
		{
			int num = 0;
			while (_bitsInBuffer > 0 && length > 0)
			{
				output[offset++] = (byte)_bitBuffer;
				_bitBuffer >>= 8;
				_bitsInBuffer -= 8;
				length--;
				num++;
			}
			if (length == 0)
			{
				return num;
			}
			int num2 = _end - _start;
			if (length > num2)
			{
				length = num2;
			}
			Array.Copy(_buffer, _start, output, offset, length);
			_start += length;
			return num + length;
		}

		public bool NeedsInput()
		{
			return _start == _end;
		}

		public void SetInput(byte[] buffer, int offset, int length)
		{
			_buffer = buffer;
			_start = offset;
			_end = offset + length;
		}

		public void SkipBits(int n)
		{
			_bitBuffer >>= n;
			_bitsInBuffer -= n;
		}

		public void SkipToByteBoundary()
		{
			_bitBuffer >>= _bitsInBuffer % 8;
			_bitsInBuffer -= _bitsInBuffer % 8;
		}
	}
	internal sealed class Match
	{
		internal MatchState State { get; set; }

		internal int Position { get; set; }

		internal int Length { get; set; }

		internal byte Symbol { get; set; }
	}
	internal enum MatchState
	{
		HasSymbol = 1,
		HasMatch,
		HasSymbolAndMatch
	}
	internal sealed class OutputBuffer
	{
		internal readonly struct BufferState
		{
			internal readonly int _pos;

			internal readonly uint _bitBuf;

			internal readonly int _bitCount;

			internal BufferState(int pos, uint bitBuf, int bitCount)
			{
				_pos = pos;
				_bitBuf = bitBuf;
				_bitCount = bitCount;
			}
		}

		private byte[] _byteBuffer;

		private int _pos;

		private uint _bitBuf;

		private int _bitCount;

		internal int BytesWritten => _pos;

		internal int FreeBytes => _byteBuffer.Length - _pos;

		internal int BitsInBuffer => _bitCount / 8 + 1;

		internal void UpdateBuffer(byte[] output)
		{
			_byteBuffer = output;
			_pos = 0;
		}

		internal void WriteUInt16(ushort value)
		{
			_byteBuffer[_pos++] = (byte)value;
			_byteBuffer[_pos++] = (byte)(value >> 8);
		}

		internal void WriteBits(int n, uint bits)
		{
			_bitBuf |= bits << _bitCount;
			_bitCount += n;
			if (_bitCount >= 16)
			{
				_byteBuffer[_pos++] = (byte)_bitBuf;
				_byteBuffer[_pos++] = (byte)(_bitBuf >> 8);
				_bitCount -= 16;
				_bitBuf >>= 16;
			}
		}

		internal void FlushBits()
		{
			while (_bitCount >= 8)
			{
				_byteBuffer[_pos++] = (byte)_bitBuf;
				_bitCount -= 8;
				_bitBuf >>= 8;
			}
			if (_bitCount > 0)
			{
				_byteBuffer[_pos++] = (byte)_bitBuf;
				_bitBuf = 0u;
				_bitCount = 0;
			}
		}

		internal void WriteBytes(byte[] byteArray, int offset, int count)
		{
			if (_bitCount == 0)
			{
				Array.Copy(byteArray, offset, _byteBuffer, _pos, count);
				_pos += count;
			}
			else
			{
				WriteBytesUnaligned(byteArray, offset, count);
			}
		}

		private void WriteBytesUnaligned(byte[] byteArray, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				byte b = byteArray[offset + i];
				WriteByteUnaligned(b);
			}
		}

		private void WriteByteUnaligned(byte b)
		{
			WriteBits(8, b);
		}

		internal BufferState DumpState()
		{
			return new BufferState(_pos, _bitBuf, _bitCount);
		}

		internal void RestoreState(BufferState state)
		{
			_pos = state._pos;
			_bitBuf = state._bitBuf;
			_bitCount = state._bitCount;
		}
	}
	internal sealed class OutputWindow
	{
		private const int WindowSize = 262144;

		private const int WindowMask = 262143;

		private readonly byte[] _window = new byte[262144];

		private int _end;

		private int _bytesUsed;

		public int FreeBytes => 262144 - _bytesUsed;

		public int AvailableBytes => _bytesUsed;

		public void Write(byte b)
		{
			_window[_end++] = b;
			_end &= 262143;
			_bytesUsed++;
		}

		public void WriteLengthDistance(int length, int distance)
		{
			_bytesUsed += length;
			int num = (_end - distance) & 0x3FFFF;
			int num2 = 262144 - length;
			if (num <= num2 && _end < num2)
			{
				if (length <= distance)
				{
					Array.Copy(_window, num, _window, _end, length);
					_end += length;
				}
				else
				{
					while (length-- > 0)
					{
						_window[_end++] = _window[num++];
					}
				}
			}
			else
			{
				while (length-- > 0)
				{
					_window[_end++] = _window[num++];
					_end &= 262143;
					num &= 0x3FFFF;
				}
			}
		}

		public int CopyFrom(InputBuffer input, int length)
		{
			length = Math.Min(Math.Min(length, 262144 - _bytesUsed), input.AvailableBytes);
			int num = 262144 - _end;
			int num2;
			if (length > num)
			{
				num2 = input.CopyTo(_window, _end, num);
				if (num2 == num)
				{
					num2 += input.CopyTo(_window, 0, length - num);
				}
			}
			else
			{
				num2 = input.CopyTo(_window, _end, length);
			}
			_end = (_end + num2) & 0x3FFFF;
			_bytesUsed += num2;
			return num2;
		}

		public int CopyTo(byte[] output, int offset, int length)
		{
			int num;
			if (length > _bytesUsed)
			{
				num = _end;
				length = _bytesUsed;
			}
			else
			{
				num = (_end - _bytesUsed + length) & 0x3FFFF;
			}
			int num2 = length;
			int num3 = length - num;
			if (num3 > 0)
			{
				Array.Copy(_window, 262144 - num3, output, offset, num3);
				offset += num3;
				length = num;
			}
			Array.Copy(_window, num - length, output, offset, length);
			_bytesUsed -= num2;
			return num2;
		}
	}
	internal sealed class PositionPreservingWriteOnlyStreamWrapper : Stream
	{
		private readonly Stream _stream;

		private long _position;

		public override bool CanRead => false;

		public override bool CanSeek => false;

		public override bool CanWrite => true;

		public override long Position
		{
			get
			{
				return _position;
			}
			set
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		public override bool CanTimeout => _stream.CanTimeout;

		public override int ReadTimeout
		{
			get
			{
				return _stream.ReadTimeout;
			}
			set
			{
				_stream.ReadTimeout = value;
			}
		}

		public override int WriteTimeout
		{
			get
			{
				return _stream.WriteTimeout;
			}
			set
			{
				_stream.WriteTimeout = value;
			}
		}

		public override long Length
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		public PositionPreservingWriteOnlyStreamWrapper(Stream stream)
		{
			_stream = stream;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			_position += count;
			_stream.Write(buffer, offset, count);
		}

		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			_position += count;
			return _stream.BeginWrite(buffer, offset, count, callback, state);
		}

		public override void EndWrite(IAsyncResult asyncResult)
		{
			_stream.EndWrite(asyncResult);
		}

		public override void WriteByte(byte value)
		{
			_position++;
			_stream.WriteByte(value);
		}

		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			_position += count;
			return _stream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		public override void Flush()
		{
			_stream.Flush();
		}

		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return _stream.FlushAsync(cancellationToken);
		}

		public override void Close()
		{
			_stream.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_stream.Dispose();
			}
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("This operation is not supported.");
		}
	}
	/// <summary>Represents a package of compressed files in the zip archive format.</summary>
	public class ZipArchive : IDisposable
	{
		private Stream _archiveStream;

		private ZipArchiveEntry _archiveStreamOwner;

		private BinaryReader _archiveReader;

		private ZipArchiveMode _mode;

		private List<ZipArchiveEntry> _entries;

		private ReadOnlyCollection<ZipArchiveEntry> _entriesCollection;

		private Dictionary<string, ZipArchiveEntry> _entriesDictionary;

		private bool _readEntries;

		private bool _leaveOpen;

		private long _centralDirectoryStart;

		private bool _isDisposed;

		private uint _numberOfThisDisk;

		private long _expectedNumberOfEntries;

		private Stream _backingStream;

		private byte[] _archiveComment;

		private Encoding _entryNameEncoding;

		/// <summary>Gets the collection of entries that are currently in the zip archive.</summary>
		/// <returns>The collection of entries that are currently in the zip archive.</returns>
		/// <exception cref="T:System.NotSupportedException">The zip archive does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The zip archive is corrupt, and its entries cannot be retrieved.</exception>
		public ReadOnlyCollection<ZipArchiveEntry> Entries
		{
			get
			{
				if (_mode == ZipArchiveMode.Create)
				{
					throw new NotSupportedException("Cannot access entries in Create mode.");
				}
				ThrowIfDisposed();
				EnsureCentralDirectoryRead();
				return _entriesCollection;
			}
		}

		/// <summary>Gets a value that describes the type of action the zip archive can perform on entries.</summary>
		/// <returns>One of the enumeration values that describes the type of action (read, create, or update) the zip archive can perform on entries.</returns>
		public ZipArchiveMode Mode => _mode;

		internal BinaryReader ArchiveReader => _archiveReader;

		internal Stream ArchiveStream => _archiveStream;

		internal uint NumberOfThisDisk => _numberOfThisDisk;

		internal Encoding EntryNameEncoding
		{
			get
			{
				return _entryNameEncoding;
			}
			private set
			{
				if (value != null && (value.Equals(Encoding.BigEndianUnicode) || value.Equals(Encoding.Unicode)))
				{
					throw new ArgumentException("The specified entry name encoding is not supported.", "EntryNameEncoding");
				}
				_entryNameEncoding = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class from the specified stream.</summary>
		/// <param name="stream">The stream that contains the archive to be read.</param>
		/// <exception cref="T:System.ArgumentException">The stream is already closed or does not support reading.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The contents of the stream are not in the zip archive format.</exception>
		public ZipArchive(Stream stream)
			: this(stream, ZipArchiveMode.Read, leaveOpen: false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class from the specified stream and with the specified mode.</summary>
		/// <param name="stream">The input or output stream.</param>
		/// <param name="mode">One of the enumeration values that indicates whether the zip archive is used to read, create, or update entries.</param>
		/// <exception cref="T:System.ArgumentException">The stream is already closed, or the capabilities of the stream do not match the mode.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="mode" /> is an invalid value.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The contents of the stream could not be interpreted as a zip archive.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is missing from the archive or is corrupt and cannot be read.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is too large to fit into memory.</exception>
		public ZipArchive(Stream stream, ZipArchiveMode mode)
			: this(stream, mode, leaveOpen: false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class on the specified stream for the specified mode, and optionally leaves the stream open.</summary>
		/// <param name="stream">The input or output stream.</param>
		/// <param name="mode">One of the enumeration values that indicates whether the zip archive is used to read, create, or update entries.</param>
		/// <param name="leaveOpen">
		///       <see langword="true" /> to leave the stream open after the <see cref="T:System.IO.Compression.ZipArchive" /> object is disposed; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">The stream is already closed, or the capabilities of the stream do not match the mode.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="mode" /> is an invalid value.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The contents of the stream could not be interpreted as a zip archive.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is missing from the archive or is corrupt and cannot be read.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is too large to fit into memory.</exception>
		public ZipArchive(Stream stream, ZipArchiveMode mode, bool leaveOpen)
			: this(stream, mode, leaveOpen, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class on the specified stream for the specified mode, uses the specified encoding for entry names, and optionally leaves the stream open.</summary>
		/// <param name="stream">The input or output stream.</param>
		/// <param name="mode">One of the enumeration values that indicates whether the zip archive is used to read, create, or update entries.</param>
		/// <param name="leaveOpen">
		///       <see langword="true" /> to leave the stream open after the <see cref="T:System.IO.Compression.ZipArchive" /> object is disposed; otherwise, <see langword="false" />.</param>
		/// <param name="entryNameEncoding">The encoding to use when reading or writing entry names in this archive. Specify a value for this parameter only when an encoding is required for interoperability with zip archive tools and libraries that do not support UTF-8 encoding for entry names.</param>
		/// <exception cref="T:System.ArgumentException">The stream is already closed, or the capabilities of the stream do not match the mode.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="mode" /> is an invalid value.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The contents of the stream could not be interpreted as a zip archive.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is missing from the archive or is corrupt and cannot be read.-or-
		///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is too large to fit into memory.</exception>
		public ZipArchive(Stream stream, ZipArchiveMode mode, bool leaveOpen, Encoding entryNameEncoding)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			EntryNameEncoding = entryNameEncoding;
			Init(stream, mode, leaveOpen);
		}

		/// <summary>Creates an empty entry that has the specified path and entry name in the zip archive.</summary>
		/// <param name="entryName">A path, relative to the root of the archive, that specifies the name of the entry to be created.</param>
		/// <returns>An empty entry in the zip archive.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="entryName" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="entryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The zip archive does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
		public ZipArchiveEntry CreateEntry(string entryName)
		{
			return DoCreateEntry(entryName, null);
		}

		/// <summary>Creates an empty entry that has the specified entry name and compression level in the zip archive.</summary>
		/// <param name="entryName">A path, relative to the root of the archive, that specifies the name of the entry to be created.</param>
		/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression effectiveness when creating the entry.</param>
		/// <returns>An empty entry in the zip archive.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="entryName" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="entryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The zip archive does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
		public ZipArchiveEntry CreateEntry(string entryName, CompressionLevel compressionLevel)
		{
			return DoCreateEntry(entryName, compressionLevel);
		}

		/// <summary>Called by the <see cref="M:System.IO.Compression.ZipArchive.Dispose" /> and <see cref="M:System.Object.Finalize" /> methods to release the unmanaged resources used by the current instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class, and optionally finishes writing the archive and releases the managed resources.</summary>
		/// <param name="disposing">
		///       <see langword="true" /> to finish writing the archive and release unmanaged and managed resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing || _isDisposed)
			{
				return;
			}
			try
			{
				ZipArchiveMode mode = _mode;
				if (mode != ZipArchiveMode.Read)
				{
					_ = mode - 1;
					_ = 1;
					WriteFile();
				}
			}
			finally
			{
				CloseStreams();
				_isDisposed = true;
			}
		}

		/// <summary>Releases the resources used by the current instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class.</summary>
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Retrieves a wrapper for the specified entry in the zip archive.</summary>
		/// <param name="entryName">A path, relative to the root of the archive, that identifies the entry to retrieve.</param>
		/// <returns>A wrapper for the specified entry in the archive; <see langword="null" /> if the entry does not exist in the archive.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="entryName" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="entryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The zip archive does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The zip archive is corrupt, and its entries cannot be retrieved.</exception>
		public ZipArchiveEntry GetEntry(string entryName)
		{
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			if (_mode == ZipArchiveMode.Create)
			{
				throw new NotSupportedException("Cannot access entries in Create mode.");
			}
			EnsureCentralDirectoryRead();
			_entriesDictionary.TryGetValue(entryName, out var value);
			return value;
		}

		private ZipArchiveEntry DoCreateEntry(string entryName, CompressionLevel? compressionLevel)
		{
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			if (string.IsNullOrEmpty(entryName))
			{
				throw new ArgumentException("String cannot be empty.", "entryName");
			}
			if (_mode == ZipArchiveMode.Read)
			{
				throw new NotSupportedException("Cannot create entries on an archive opened in read mode.");
			}
			ThrowIfDisposed();
			ZipArchiveEntry zipArchiveEntry = (compressionLevel.HasValue ? new ZipArchiveEntry(this, entryName, compressionLevel.Value) : new ZipArchiveEntry(this, entryName));
			AddEntry(zipArchiveEntry);
			return zipArchiveEntry;
		}

		internal void AcquireArchiveStream(ZipArchiveEntry entry)
		{
			if (_archiveStreamOwner != null)
			{
				if (_archiveStreamOwner.EverOpenedForWrite)
				{
					throw new IOException("Entries cannot be created while previously created entries are still open.");
				}
				_archiveStreamOwner.WriteAndFinishLocalEntry();
			}
			_archiveStreamOwner = entry;
		}

		private void AddEntry(ZipArchiveEntry entry)
		{
			_entries.Add(entry);
			string fullName = entry.FullName;
			if (!_entriesDictionary.ContainsKey(fullName))
			{
				_entriesDictionary.Add(fullName, entry);
			}
		}

		[Conditional("DEBUG")]
		internal void DebugAssertIsStillArchiveStreamOwner(ZipArchiveEntry entry)
		{
		}

		internal void ReleaseArchiveStream(ZipArchiveEntry entry)
		{
			_archiveStreamOwner = null;
		}

		internal void RemoveEntry(ZipArchiveEntry entry)
		{
			_entries.Remove(entry);
			_entriesDictionary.Remove(entry.FullName);
		}

		internal void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
		}

		private void CloseStreams()
		{
			if (!_leaveOpen)
			{
				_archiveStream.Dispose();
				_backingStream?.Dispose();
				_archiveReader?.Dispose();
			}
			else if (_backingStream != null)
			{
				_archiveStream.Dispose();
			}
		}

		private void EnsureCentralDirectoryRead()
		{
			if (!_readEntries)
			{
				ReadCentralDirectory();
				_readEntries = true;
			}
		}

		private void Init(Stream stream, ZipArchiveMode mode, bool leaveOpen)
		{
			Stream stream2 = null;
			try
			{
				_backingStream = null;
				switch (mode)
				{
				case ZipArchiveMode.Create:
					if (!stream.CanWrite)
					{
						throw new ArgumentException("Cannot use create mode on a non-writable stream.");
					}
					break;
				case ZipArchiveMode.Read:
					if (!stream.CanRead)
					{
						throw new ArgumentException("Cannot use read mode on a non-readable stream.");
					}
					if (!stream.CanSeek)
					{
						_backingStream = stream;
						stream2 = (stream = new MemoryStream());
						_backingStream.CopyTo(stream);
						stream.Seek(0L, SeekOrigin.Begin);
					}
					break;
				case ZipArchiveMode.Update:
					if (!stream.CanRead || !stream.CanWrite || !stream.CanSeek)
					{
						throw new ArgumentException("Update mode requires a stream with read, write, and seek capabilities.");
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("mode");
				}
				_mode = mode;
				if (mode == ZipArchiveMode.Create && !stream.CanSeek)
				{
					_archiveStream = new PositionPreservingWriteOnlyStreamWrapper(stream);
				}
				else
				{
					_archiveStream = stream;
				}
				_archiveStreamOwner = null;
				if (mode == ZipArchiveMode.Create)
				{
					_archiveReader = null;
				}
				else
				{
					_archiveReader = new BinaryReader(_archiveStream);
				}
				_entries = new List<ZipArchiveEntry>();
				_entriesCollection = new ReadOnlyCollection<ZipArchiveEntry>(_entries);
				_entriesDictionary = new Dictionary<string, ZipArchiveEntry>();
				_readEntries = false;
				_leaveOpen = leaveOpen;
				_centralDirectoryStart = 0L;
				_isDisposed = false;
				_numberOfThisDisk = 0u;
				_archiveComment = null;
				switch (mode)
				{
				case ZipArchiveMode.Create:
					_readEntries = true;
					return;
				case ZipArchiveMode.Read:
					ReadEndOfCentralDirectory();
					return;
				}
				if (_archiveStream.Length == 0L)
				{
					_readEntries = true;
					return;
				}
				ReadEndOfCentralDirectory();
				EnsureCentralDirectoryRead();
				foreach (ZipArchiveEntry entry in _entries)
				{
					entry.ThrowIfNotOpenable(needToUncompress: false, needToLoadIntoMemory: true);
				}
			}
			catch
			{
				stream2?.Dispose();
				throw;
			}
		}

		private void ReadCentralDirectory()
		{
			try
			{
				_archiveStream.Seek(_centralDirectoryStart, SeekOrigin.Begin);
				long num = 0L;
				bool saveExtraFieldsAndComments = Mode == ZipArchiveMode.Update;
				ZipCentralDirectoryFileHeader header;
				while (ZipCentralDirectoryFileHeader.TryReadBlock(_archiveReader, saveExtraFieldsAndComments, out header))
				{
					AddEntry(new ZipArchiveEntry(this, header));
					num++;
				}
				if (num != _expectedNumberOfEntries)
				{
					throw new InvalidDataException("Number of entries expected in End Of Central Directory does not correspond to number of entries in Central Directory.");
				}
			}
			catch (EndOfStreamException p)
			{
				throw new InvalidDataException(global::SR.Format("Central Directory is invalid.", p));
			}
		}

		private void ReadEndOfCentralDirectory()
		{
			try
			{
				_archiveStream.Seek(-18L, SeekOrigin.End);
				if (!ZipHelper.SeekBackwardsToSignature(_archiveStream, 101010256u))
				{
					throw new InvalidDataException("End of Central Directory record could not be found.");
				}
				long position = _archiveStream.Position;
				ZipEndOfCentralDirectoryBlock.TryReadBlock(_archiveReader, out var eocdBlock);
				if (eocdBlock.NumberOfThisDisk != eocdBlock.NumberOfTheDiskWithTheStartOfTheCentralDirectory)
				{
					throw new InvalidDataException("Split or spanned archives are not supported.");
				}
				_numberOfThisDisk = eocdBlock.NumberOfThisDisk;
				_centralDirectoryStart = eocdBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber;
				if (eocdBlock.NumberOfEntriesInTheCentralDirectory != eocdBlock.NumberOfEntriesInTheCentralDirectoryOnThisDisk)
				{
					throw new InvalidDataException("Split or spanned archives are not supported.");
				}
				_expectedNumberOfEntries = eocdBlock.NumberOfEntriesInTheCentralDirectory;
				if (_mode == ZipArchiveMode.Update)
				{
					_archiveComment = eocdBlock.ArchiveComment;
				}
				if (eocdBlock.NumberOfThisDisk == ushort.MaxValue || eocdBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber == uint.MaxValue || eocdBlock.NumberOfEntriesInTheCentralDirectory == ushort.MaxValue)
				{
					_archiveStream.Seek(position - 16, SeekOrigin.Begin);
					if (ZipHelper.SeekBackwardsToSignature(_archiveStream, 117853008u))
					{
						Zip64EndOfCentralDirectoryLocator.TryReadBlock(_archiveReader, out var zip64EOCDLocator);
						if (zip64EOCDLocator.OffsetOfZip64EOCD > long.MaxValue)
						{
							throw new InvalidDataException("Offset to Zip64 End Of Central Directory record cannot be held in an Int64.");
						}
						long offsetOfZip64EOCD = (long)zip64EOCDLocator.OffsetOfZip64EOCD;
						_archiveStream.Seek(offsetOfZip64EOCD, SeekOrigin.Begin);
						if (!Zip64EndOfCentralDirectoryRecord.TryReadBlock(_archiveReader, out var zip64EOCDRecord))
						{
							throw new InvalidDataException("Zip 64 End of Central Directory Record not where indicated.");
						}
						_numberOfThisDisk = zip64EOCDRecord.NumberOfThisDisk;
						if (zip64EOCDRecord.NumberOfEntriesTotal > long.MaxValue)
						{
							throw new InvalidDataException("Number of Entries cannot be held in an Int64.");
						}
						if (zip64EOCDRecord.OffsetOfCentralDirectory > long.MaxValue)
						{
							throw new InvalidDataException("Offset to Central Directory cannot be held in an Int64.");
						}
						if (zip64EOCDRecord.NumberOfEntriesTotal != zip64EOCDRecord.NumberOfEntriesOnThisDisk)
						{
							throw new InvalidDataException("Split or spanned archives are not supported.");
						}
						_expectedNumberOfEntries = (long)zip64EOCDRecord.NumberOfEntriesTotal;
						_centralDirectoryStart = (long)zip64EOCDRecord.OffsetOfCentralDirectory;
					}
				}
				if (_centralDirectoryStart > _archiveStream.Length)
				{
					throw new InvalidDataException("Offset to Central Directory cannot be held in an Int64.");
				}
			}
			catch (EndOfStreamException innerException)
			{
				throw new InvalidDataException("Central Directory corrupt.", innerException);
			}
			catch (IOException innerException2)
			{
				throw new InvalidDataException("Central Directory corrupt.", innerException2);
			}
		}

		private void WriteFile()
		{
			if (_mode == ZipArchiveMode.Update)
			{
				List<ZipArchiveEntry> list = new List<ZipArchiveEntry>();
				foreach (ZipArchiveEntry entry in _entries)
				{
					if (!entry.LoadLocalHeaderExtraFieldAndCompressedBytesIfNeeded())
					{
						list.Add(entry);
					}
				}
				foreach (ZipArchiveEntry item in list)
				{
					item.Delete();
				}
				_archiveStream.Seek(0L, SeekOrigin.Begin);
				_archiveStream.SetLength(0L);
			}
			foreach (ZipArchiveEntry entry2 in _entries)
			{
				entry2.WriteAndFinishLocalEntry();
			}
			long position = _archiveStream.Position;
			foreach (ZipArchiveEntry entry3 in _entries)
			{
				entry3.WriteCentralDirectoryFileHeader();
			}
			long sizeOfCentralDirectory = _archiveStream.Position - position;
			WriteArchiveEpilogue(position, sizeOfCentralDirectory);
		}

		private void WriteArchiveEpilogue(long startOfCentralDirectory, long sizeOfCentralDirectory)
		{
			if (startOfCentralDirectory >= uint.MaxValue || sizeOfCentralDirectory >= uint.MaxValue || _entries.Count >= 65535)
			{
				long position = _archiveStream.Position;
				Zip64EndOfCentralDirectoryRecord.WriteBlock(_archiveStream, _entries.Count, startOfCentralDirectory, sizeOfCentralDirectory);
				Zip64EndOfCentralDirectoryLocator.WriteBlock(_archiveStream, position);
			}
			ZipEndOfCentralDirectoryBlock.WriteBlock(_archiveStream, _entries.Count, startOfCentralDirectory, sizeOfCentralDirectory, _archiveComment);
		}
	}
	/// <summary>Represents a compressed file within a zip archive.</summary>
	public class ZipArchiveEntry
	{
		private sealed class DirectToArchiveWriterStream : Stream
		{
			private long _position;

			private CheckSumAndSizeWriteStream _crcSizeStream;

			private bool _everWritten;

			private bool _isDisposed;

			private ZipArchiveEntry _entry;

			private bool _usedZip64inLH;

			private bool _canWrite;

			public override long Length
			{
				get
				{
					ThrowIfDisposed();
					throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
				}
			}

			public override long Position
			{
				get
				{
					ThrowIfDisposed();
					return _position;
				}
				set
				{
					ThrowIfDisposed();
					throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
				}
			}

			public override bool CanRead => false;

			public override bool CanSeek => false;

			public override bool CanWrite => _canWrite;

			public DirectToArchiveWriterStream(CheckSumAndSizeWriteStream crcSizeStream, ZipArchiveEntry entry)
			{
				_position = 0L;
				_crcSizeStream = crcSizeStream;
				_everWritten = false;
				_isDisposed = false;
				_entry = entry;
				_usedZip64inLH = false;
				_canWrite = true;
			}

			private void ThrowIfDisposed()
			{
				if (_isDisposed)
				{
					throw new ObjectDisposedException(GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
				}
			}

			public override int Read(byte[] buffer, int offset, int count)
			{
				ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
			}

			public override long Seek(long offset, SeekOrigin origin)
			{
				ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}

			public override void SetLength(long value)
			{
				ThrowIfDisposed();
				throw new NotSupportedException("SetLength requires a stream that supports seeking and writing.");
			}

			public override void Write(byte[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (offset < 0)
				{
					throw new ArgumentOutOfRangeException("offset", "The argument must be non-negative.");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count", "The argument must be non-negative.");
				}
				if (buffer.Length - offset < count)
				{
					throw new ArgumentException("The offset and length parameters are not valid for the array that was given.");
				}
				ThrowIfDisposed();
				if (count != 0)
				{
					if (!_everWritten)
					{
						_everWritten = true;
						_usedZip64inLH = _entry.WriteLocalFileHeader(isEmptyFile: false);
					}
					_crcSizeStream.Write(buffer, offset, count);
					_position += count;
				}
			}

			public override void Flush()
			{
				ThrowIfDisposed();
				_crcSizeStream.Flush();
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing && !_isDisposed)
				{
					_crcSizeStream.Dispose();
					if (!_everWritten)
					{
						_entry.WriteLocalFileHeader(isEmptyFile: true);
					}
					else if (_entry._archive.ArchiveStream.CanSeek)
					{
						_entry.WriteCrcAndSizesInLocalHeader(_usedZip64inLH);
					}
					else
					{
						_entry.WriteDataDescriptor();
					}
					_canWrite = false;
					_isDisposed = true;
				}
				base.Dispose(disposing);
			}
		}

		[Flags]
		private enum BitFlagValues : ushort
		{
			DataDescriptor = 8,
			UnicodeFileName = 0x800
		}

		internal enum CompressionMethodValues : ushort
		{
			Stored = 0,
			Deflate = 8,
			Deflate64 = 9,
			BZip2 = 12,
			LZMA = 14
		}

		private const ushort DefaultVersionToExtract = 10;

		private const int MaxSingleBufferSize = 2147483591;

		private ZipArchive _archive;

		private readonly bool _originallyInArchive;

		private readonly int _diskNumberStart;

		private readonly ZipVersionMadeByPlatform _versionMadeByPlatform;

		private ZipVersionNeededValues _versionMadeBySpecification;

		private ZipVersionNeededValues _versionToExtract;

		private BitFlagValues _generalPurposeBitFlag;

		private CompressionMethodValues _storedCompressionMethod;

		private DateTimeOffset _lastModified;

		private long _compressedSize;

		private long _uncompressedSize;

		private long _offsetOfLocalHeader;

		private long? _storedOffsetOfCompressedData;

		private uint _crc32;

		private byte[][] _compressedBytes;

		private MemoryStream _storedUncompressedData;

		private bool _currentlyOpenForWrite;

		private bool _everOpenedForWrite;

		private Stream _outstandingWriteStream;

		private uint _externalFileAttr;

		private string _storedEntryName;

		private byte[] _storedEntryNameBytes;

		private List<ZipGenericExtraField> _cdUnknownExtraFields;

		private List<ZipGenericExtraField> _lhUnknownExtraFields;

		private byte[] _fileComment;

		private CompressionLevel? _compressionLevel;

		private static readonly bool s_allowLargeZipArchiveEntriesInUpdateMode = IntPtr.Size > 4;

		internal static readonly ZipVersionMadeByPlatform CurrentZipPlatform = ((Path.PathSeparator == '/') ? ZipVersionMadeByPlatform.Unix : ZipVersionMadeByPlatform.Windows);

		/// <summary>Gets the zip archive that the entry belongs to.</summary>
		/// <returns>The zip archive that the entry belongs to, or <see langword="null" /> if the entry has been deleted.</returns>
		public ZipArchive Archive => _archive;

		[CLSCompliant(false)]
		public uint Crc32 => _crc32;

		/// <summary>Gets the compressed size of the entry in the zip archive.</summary>
		/// <returns>The compressed size of the entry in the zip archive.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the property is not available because the entry has been modified.</exception>
		public long CompressedLength
		{
			get
			{
				if (_everOpenedForWrite)
				{
					throw new InvalidOperationException("Length properties are unavailable once an entry has been opened for writing.");
				}
				return _compressedSize;
			}
		}

		/// <summary>
		/// 		  OS and Application specific file attributes.
		/// </summary>
		/// <returns>The external attributes written by the application when this entry was written. It is both host OS and application dependent.</returns>
		public int ExternalAttributes
		{
			get
			{
				return (int)_externalFileAttr;
			}
			set
			{
				ThrowIfInvalidArchive();
				_externalFileAttr = (uint)value;
			}
		}

		/// <summary>Gets the relative path of the entry in the zip archive.</summary>
		/// <returns>The relative path of the entry in the zip archive.</returns>
		public string FullName
		{
			get
			{
				return _storedEntryName;
			}
			private set
			{
				if (value == null)
				{
					throw new ArgumentNullException("FullName");
				}
				_storedEntryNameBytes = EncodeEntryName(value, out var isUTF);
				_storedEntryName = value;
				if (isUTF)
				{
					_generalPurposeBitFlag |= BitFlagValues.UnicodeFileName;
				}
				else
				{
					_generalPurposeBitFlag &= ~BitFlagValues.UnicodeFileName;
				}
				if (ParseFileName(value, _versionMadeByPlatform) == "")
				{
					VersionToExtractAtLeast(ZipVersionNeededValues.ExplicitDirectory);
				}
			}
		}

		/// <summary>Gets or sets the last time the entry in the zip archive was changed.</summary>
		/// <returns>The last time the entry in the zip archive was changed.</returns>
		/// <exception cref="T:System.NotSupportedException">The attempt to set this property failed, because the zip archive for the entry is in <see cref="F:System.IO.Compression.ZipArchiveMode.Read" /> mode.</exception>
		/// <exception cref="T:System.IO.IOException">The archive mode is set to <see cref="F:System.IO.Compression.ZipArchiveMode.Create" />.- or -The archive mode is set to <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and the entry has been opened.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An attempt was made to set this property to a value that is either earlier than 1980 January 1 0:00:00 (midnight) or later than 2107 December 31 23:59:58 (one second before midnight).</exception>
		public DateTimeOffset LastWriteTime
		{
			get
			{
				return _lastModified;
			}
			set
			{
				ThrowIfInvalidArchive();
				if (_archive.Mode == ZipArchiveMode.Read)
				{
					throw new NotSupportedException("Cannot modify read-only archive.");
				}
				if (_archive.Mode == ZipArchiveMode.Create && _everOpenedForWrite)
				{
					throw new IOException("Cannot modify entry in Create mode after entry has been opened for writing.");
				}
				if (value.DateTime.Year < 1980 || value.DateTime.Year > 2107)
				{
					throw new ArgumentOutOfRangeException("value", "The DateTimeOffset specified cannot be converted into a Zip file timestamp.");
				}
				_lastModified = value;
			}
		}

		/// <summary>Gets the uncompressed size of the entry in the zip archive.</summary>
		/// <returns>The uncompressed size of the entry in the zip archive.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the property is not available because the entry has been modified.</exception>
		public long Length
		{
			get
			{
				if (_everOpenedForWrite)
				{
					throw new InvalidOperationException("Length properties are unavailable once an entry has been opened for writing.");
				}
				return _uncompressedSize;
			}
		}

		/// <summary>Gets the file name of the entry in the zip archive.</summary>
		/// <returns>The file name of the entry in the zip archive.</returns>
		public string Name => ParseFileName(FullName, _versionMadeByPlatform);

		internal bool EverOpenedForWrite => _everOpenedForWrite;

		private long OffsetOfCompressedData
		{
			get
			{
				if (!_storedOffsetOfCompressedData.HasValue)
				{
					_archive.ArchiveStream.Seek(_offsetOfLocalHeader, SeekOrigin.Begin);
					if (!ZipLocalFileHeader.TrySkipBlock(_archive.ArchiveReader))
					{
						throw new InvalidDataException("A local file header is corrupt.");
					}
					_storedOffsetOfCompressedData = _archive.ArchiveStream.Position;
				}
				return _storedOffsetOfCompressedData.Value;
			}
		}

		private MemoryStream UncompressedData
		{
			get
			{
				if (_storedUncompressedData == null)
				{
					_storedUncompressedData = new MemoryStream((int)_uncompressedSize);
					if (_originallyInArchive)
					{
						using Stream stream = OpenInReadMode(checkOpenable: false);
						try
						{
							stream.CopyTo(_storedUncompressedData);
						}
						catch (InvalidDataException)
						{
							_storedUncompressedData.Dispose();
							_storedUncompressedData = null;
							_currentlyOpenForWrite = false;
							_everOpenedForWrite = false;
							throw;
						}
					}
					CompressionMethod = CompressionMethodValues.Deflate;
				}
				return _storedUncompressedData;
			}
		}

		private CompressionMethodValues CompressionMethod
		{
			get
			{
				return _storedCompressionMethod;
			}
			set
			{
				switch (value)
				{
				case CompressionMethodValues.Deflate:
					VersionToExtractAtLeast(ZipVersionNeededValues.ExplicitDirectory);
					break;
				case CompressionMethodValues.Deflate64:
					VersionToExtractAtLeast(ZipVersionNeededValues.Deflate64);
					break;
				}
				_storedCompressionMethod = value;
			}
		}

		internal ZipArchiveEntry(ZipArchive archive, ZipCentralDirectoryFileHeader cd)
		{
			_archive = archive;
			_originallyInArchive = true;
			_diskNumberStart = cd.DiskNumberStart;
			_versionMadeByPlatform = (ZipVersionMadeByPlatform)cd.VersionMadeByCompatibility;
			_versionMadeBySpecification = (ZipVersionNeededValues)cd.VersionMadeBySpecification;
			_versionToExtract = (ZipVersionNeededValues)cd.VersionNeededToExtract;
			_generalPurposeBitFlag = (BitFlagValues)cd.GeneralPurposeBitFlag;
			CompressionMethod = (CompressionMethodValues)cd.CompressionMethod;
			_lastModified = new DateTimeOffset(ZipHelper.DosTimeToDateTime(cd.LastModified));
			_compressedSize = cd.CompressedSize;
			_uncompressedSize = cd.UncompressedSize;
			_externalFileAttr = cd.ExternalFileAttributes;
			_offsetOfLocalHeader = cd.RelativeOffsetOfLocalHeader;
			_storedOffsetOfCompressedData = null;
			_crc32 = cd.Crc32;
			_compressedBytes = null;
			_storedUncompressedData = null;
			_currentlyOpenForWrite = false;
			_everOpenedForWrite = false;
			_outstandingWriteStream = null;
			FullName = DecodeEntryName(cd.Filename);
			_lhUnknownExtraFields = null;
			_cdUnknownExtraFields = cd.ExtraFields;
			_fileComment = cd.FileComment;
			_compressionLevel = null;
		}

		internal ZipArchiveEntry(ZipArchive archive, string entryName, CompressionLevel compressionLevel)
			: this(archive, entryName)
		{
			_compressionLevel = compressionLevel;
		}

		internal ZipArchiveEntry(ZipArchive archive, string entryName)
		{
			_archive = archive;
			_originallyInArchive = false;
			_diskNumberStart = 0;
			_versionMadeByPlatform = CurrentZipPlatform;
			_versionMadeBySpecification = ZipVersionNeededValues.Default;
			_versionToExtract = ZipVersionNeededValues.Default;
			_generalPurposeBitFlag = (BitFlagValues)0;
			CompressionMethod = CompressionMethodValues.Deflate;
			_lastModified = DateTimeOffset.Now;
			_compressedSize = 0L;
			_uncompressedSize = 0L;
			_externalFileAttr = 0u;
			_offsetOfLocalHeader = 0L;
			_storedOffsetOfCompressedData = null;
			_crc32 = 0u;
			_compressedBytes = null;
			_storedUncompressedData = null;
			_currentlyOpenForWrite = false;
			_everOpenedForWrite = false;
			_outstandingWriteStream = null;
			FullName = entryName;
			_cdUnknownExtraFields = null;
			_lhUnknownExtraFields = null;
			_fileComment = null;
			_compressionLevel = null;
			if (_storedEntryNameBytes.Length > 65535)
			{
				throw new ArgumentException("Entry names cannot require more than 2^16 bits.");
			}
			if (_archive.Mode == ZipArchiveMode.Create)
			{
				_archive.AcquireArchiveStream(this);
			}
		}

		/// <summary>Deletes the entry from the zip archive.</summary>
		/// <exception cref="T:System.IO.IOException">The entry is already open for reading or writing.</exception>
		/// <exception cref="T:System.NotSupportedException">The zip archive for this entry was opened in a mode other than <see cref="F:System.IO.Compression.ZipArchiveMode.Update" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive for this entry has been disposed.</exception>
		public void Delete()
		{
			if (_archive != null)
			{
				if (_currentlyOpenForWrite)
				{
					throw new IOException("Cannot delete an entry currently open for writing.");
				}
				if (_archive.Mode != ZipArchiveMode.Update)
				{
					throw new NotSupportedException("Delete can only be used when the archive is in Update mode.");
				}
				_archive.ThrowIfDisposed();
				_archive.RemoveEntry(this);
				_archive = null;
				UnloadStreams();
			}
		}

		/// <summary>Opens the entry from the zip archive.</summary>
		/// <returns>The stream that represents the contents of the entry.</returns>
		/// <exception cref="T:System.IO.IOException">The entry is already currently open for writing.-or-The entry has been deleted from the archive.-or-The archive for this entry was opened with the <see cref="F:System.IO.Compression.ZipArchiveMode.Create" /> mode, and this entry has already been written to. </exception>
		/// <exception cref="T:System.IO.InvalidDataException">The entry is either missing from the archive or is corrupt and cannot be read. -or-The entry has been compressed by using a compression method that is not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive for this entry has been disposed.</exception>
		public Stream Open()
		{
			ThrowIfInvalidArchive();
			return _archive.Mode switch
			{
				ZipArchiveMode.Read => OpenInReadMode(checkOpenable: true), 
				ZipArchiveMode.Create => OpenInWriteMode(), 
				_ => OpenInUpdateMode(), 
			};
		}

		/// <summary>Retrieves the relative path of the entry in the zip archive.</summary>
		/// <returns>The relative path of the entry, which is the value stored in the <see cref="P:System.IO.Compression.ZipArchiveEntry.FullName" /> property.</returns>
		public override string ToString()
		{
			return FullName;
		}

		private string DecodeEntryName(byte[] entryNameBytes)
		{
			Encoding encoding = (((_generalPurposeBitFlag & BitFlagValues.UnicodeFileName) != 0) ? Encoding.UTF8 : ((_archive == null) ? Encoding.UTF8 : (_archive.EntryNameEncoding ?? Encoding.UTF8)));
			return encoding.GetString(entryNameBytes);
		}

		private byte[] EncodeEntryName(string entryName, out bool isUTF8)
		{
			Encoding encoding = ((_archive == null || _archive.EntryNameEncoding == null) ? (ZipHelper.RequiresUnicode(entryName) ? Encoding.UTF8 : Encoding.ASCII) : _archive.EntryNameEncoding);
			isUTF8 = encoding.Equals(Encoding.UTF8);
			return encoding.GetBytes(entryName);
		}

		internal void WriteAndFinishLocalEntry()
		{
			CloseStreams();
			WriteLocalFileHeaderAndDataIfNeeded();
			UnloadStreams();
		}

		internal void WriteCentralDirectoryFileHeader()
		{
			BinaryWriter binaryWriter = new BinaryWriter(_archive.ArchiveStream);
			Zip64ExtraField zip64ExtraField = default(Zip64ExtraField);
			bool flag = false;
			uint value;
			uint value2;
			if (SizesTooLarge())
			{
				flag = true;
				value = uint.MaxValue;
				value2 = uint.MaxValue;
				zip64ExtraField.CompressedSize = _compressedSize;
				zip64ExtraField.UncompressedSize = _uncompressedSize;
			}
			else
			{
				value = (uint)_compressedSize;
				value2 = (uint)_uncompressedSize;
			}
			uint value3;
			if (_offsetOfLocalHeader > uint.MaxValue)
			{
				flag = true;
				value3 = uint.MaxValue;
				zip64ExtraField.LocalHeaderOffset = _offsetOfLocalHeader;
			}
			else
			{
				value3 = (uint)_offsetOfLocalHeader;
			}
			if (flag)
			{
				VersionToExtractAtLeast(ZipVersionNeededValues.Zip64);
			}
			int num = (flag ? zip64ExtraField.TotalSize : 0) + ((_cdUnknownExtraFields != null) ? ZipGenericExtraField.TotalSize(_cdUnknownExtraFields) : 0);
			ushort value4;
			if (num > 65535)
			{
				value4 = (ushort)(flag ? zip64ExtraField.TotalSize : 0);
				_cdUnknownExtraFields = null;
			}
			else
			{
				value4 = (ushort)num;
			}
			binaryWriter.Write(33639248u);
			binaryWriter.Write((byte)_versionMadeBySpecification);
			binaryWriter.Write((byte)CurrentZipPlatform);
			binaryWriter.Write((ushort)_versionToExtract);
			binaryWriter.Write((ushort)_generalPurposeBitFlag);
			binaryWriter.Write((ushort)CompressionMethod);
			binaryWriter.Write(ZipHelper.DateTimeToDosTime(_lastModified.DateTime));
			binaryWriter.Write(_crc32);
			binaryWriter.Write(value);
			binaryWriter.Write(value2);
			binaryWriter.Write((ushort)_storedEntryNameBytes.Length);
			binaryWriter.Write(value4);
			binaryWriter.Write((ushort)((_fileComment != null) ? ((ushort)_fileComment.Length) : 0));
			binaryWriter.Write((ushort)0);
			binaryWriter.Write((ushort)0);
			binaryWriter.Write(_externalFileAttr);
			binaryWriter.Write(value3);
			binaryWriter.Write(_storedEntryNameBytes);
			if (flag)
			{
				zip64ExtraField.WriteBlock(_archive.ArchiveStream);
			}
			if (_cdUnknownExtraFields != null)
			{
				ZipGenericExtraField.WriteAllBlocks(_cdUnknownExtraFields, _archive.ArchiveStream);
			}
			if (_fileComment != null)
			{
				binaryWriter.Write(_fileComment);
			}
		}

		internal bool LoadLocalHeaderExtraFieldAndCompressedBytesIfNeeded()
		{
			if (_originallyInArchive)
			{
				_archive.ArchiveStream.Seek(_offsetOfLocalHeader, SeekOrigin.Begin);
				_lhUnknownExtraFields = ZipLocalFileHeader.GetExtraFields(_archive.ArchiveReader);
			}
			if (!_everOpenedForWrite && _originallyInArchive)
			{
				_compressedBytes = new byte[_compressedSize / 2147483591 + 1][];
				for (int i = 0; i < _compressedBytes.Length - 1; i++)
				{
					_compressedBytes[i] = new byte[2147483591];
				}
				_compressedBytes[_compressedBytes.Length - 1] = new byte[_compressedSize % 2147483591];
				_archive.ArchiveStream.Seek(OffsetOfCompressedData, SeekOrigin.Begin);
				for (int j = 0; j < _compressedBytes.Length - 1; j++)
				{
					ZipHelper.ReadBytes(_archive.ArchiveStream, _compressedBytes[j], 2147483591);
				}
				ZipHelper.ReadBytes(_archive.ArchiveStream, _compressedBytes[_compressedBytes.Length - 1], (int)(_compressedSize % 2147483591));
			}
			return true;
		}

		internal void ThrowIfNotOpenable(bool needToUncompress, bool needToLoadIntoMemory)
		{
			if (!IsOpenable(needToUncompress, needToLoadIntoMemory, out var message))
			{
				throw new InvalidDataException(message);
			}
		}

		private CheckSumAndSizeWriteStream GetDataCompressor(Stream backingStream, bool leaveBackingStreamOpen, EventHandler onClose)
		{
			DeflateStream baseStream = (_compressionLevel.HasValue ? new DeflateStream(backingStream, _compressionLevel.Value, leaveBackingStreamOpen) : new DeflateStream(backingStream, CompressionMode.Compress, leaveBackingStreamOpen));
			bool flag = true;
			bool leaveOpenOnClose = leaveBackingStreamOpen && !flag;
			return new CheckSumAndSizeWriteStream(baseStream, backingStream, leaveOpenOnClose, this, onClose, delegate(long initialPosition, long currentPosition, uint checkSum, Stream backing, ZipArchiveEntry thisRef, EventHandler closeHandler)
			{
				thisRef._crc32 = checkSum;
				thisRef._uncompressedSize = currentPosition;
				thisRef._compressedSize = backing.Position - initialPosition;
				closeHandler?.Invoke(thisRef, EventArgs.Empty);
			});
		}

		private Stream GetDataDecompressor(Stream compressedStreamToRead)
		{
			Stream stream = null;
			return CompressionMethod switch
			{
				CompressionMethodValues.Deflate => new DeflateStream(compressedStreamToRead, CompressionMode.Decompress), 
				CompressionMethodValues.Deflate64 => new DeflateManagedStream(compressedStreamToRead, CompressionMethodValues.Deflate64), 
				_ => compressedStreamToRead, 
			};
		}

		private Stream OpenInReadMode(bool checkOpenable)
		{
			if (checkOpenable)
			{
				ThrowIfNotOpenable(needToUncompress: true, needToLoadIntoMemory: false);
			}
			Stream compressedStreamToRead = new SubReadStream(_archive.ArchiveStream, OffsetOfCompressedData, _compressedSize);
			return GetDataDecompressor(compressedStreamToRead);
		}

		private Stream OpenInWriteMode()
		{
			if (_everOpenedForWrite)
			{
				throw new IOException("Entries in create mode may only be written to once, and only one entry may be held open at a time.");
			}
			_everOpenedForWrite = true;
			CheckSumAndSizeWriteStream dataCompressor = GetDataCompressor(_archive.ArchiveStream, leaveBackingStreamOpen: true, delegate(object o, EventArgs e)
			{
				ZipArchiveEntry zipArchiveEntry = (ZipArchiveEntry)o;
				zipArchiveEntry._archive.ReleaseArchiveStream(zipArchiveEntry);
				zipArchiveEntry._outstandingWriteStream = null;
			});
			_outstandingWriteStream = new DirectToArchiveWriterStream(dataCompressor, this);
			return new WrappedStream(_outstandingWriteStream, closeBaseStream: true);
		}

		private Stream OpenInUpdateMode()
		{
			if (_currentlyOpenForWrite)
			{
				throw new IOException("Entries cannot be opened multiple times in Update mode.");
			}
			ThrowIfNotOpenable(needToUncompress: true, needToLoadIntoMemory: true);
			_everOpenedForWrite = true;
			_currentlyOpenForWrite = true;
			UncompressedData.Seek(0L, SeekOrigin.Begin);
			return new WrappedStream(UncompressedData, this, delegate(ZipArchiveEntry thisRef)
			{
				thisRef._currentlyOpenForWrite = false;
			});
		}

		private bool IsOpenable(bool needToUncompress, bool needToLoadIntoMemory, out string message)
		{
			message = null;
			if (_originallyInArchive)
			{
				if (needToUncompress && CompressionMethod != CompressionMethodValues.Stored && CompressionMethod != CompressionMethodValues.Deflate && CompressionMethod != CompressionMethodValues.Deflate64)
				{
					CompressionMethodValues compressionMethod = CompressionMethod;
					if (compressionMethod == CompressionMethodValues.BZip2 || compressionMethod == CompressionMethodValues.LZMA)
					{
						message = global::SR.Format("The archive entry was compressed using {0} and is not supported.", CompressionMethod.ToString());
					}
					else
					{
						message = "The archive entry was compressed using an unsupported compression method.";
					}
					return false;
				}
				if (_diskNumberStart != _archive.NumberOfThisDisk)
				{
					message = "Split or spanned archives are not supported.";
					return false;
				}
				if (_offsetOfLocalHeader > _archive.ArchiveStream.Length)
				{
					message = "A local file header is corrupt.";
					return false;
				}
				_archive.ArchiveStream.Seek(_offsetOfLocalHeader, SeekOrigin.Begin);
				if (!ZipLocalFileHeader.TrySkipBlock(_archive.ArchiveReader))
				{
					message = "A local file header is corrupt.";
					return false;
				}
				if (OffsetOfCompressedData + _compressedSize > _archive.ArchiveStream.Length)
				{
					message = "A local file header is corrupt.";
					return false;
				}
				if (needToLoadIntoMemory && _compressedSize > int.MaxValue && !s_allowLargeZipArchiveEntriesInUpdateMode)
				{
					message = "Entries larger than 4GB are not supported in Update mode.";
					return false;
				}
			}
			return true;
		}

		private bool SizesTooLarge()
		{
			if (_compressedSize <= uint.MaxValue)
			{
				return _uncompressedSize > uint.MaxValue;
			}
			return true;
		}

		private bool WriteLocalFileHeader(bool isEmptyFile)
		{
			BinaryWriter binaryWriter = new BinaryWriter(_archive.ArchiveStream);
			Zip64ExtraField zip64ExtraField = default(Zip64ExtraField);
			bool flag = false;
			uint value;
			uint value2;
			if (isEmptyFile)
			{
				CompressionMethod = CompressionMethodValues.Stored;
				value = 0u;
				value2 = 0u;
			}
			else if (_archive.Mode == ZipArchiveMode.Create && !_archive.ArchiveStream.CanSeek && !isEmptyFile)
			{
				_generalPurposeBitFlag |= BitFlagValues.DataDescriptor;
				flag = false;
				value = 0u;
				value2 = 0u;
			}
			else if (SizesTooLarge())
			{
				flag = true;
				value = uint.MaxValue;
				value2 = uint.MaxValue;
				zip64ExtraField.CompressedSize = _compressedSize;
				zip64ExtraField.UncompressedSize = _uncompressedSize;
				VersionToExtractAtLeast(ZipVersionNeededValues.Zip64);
			}
			else
			{
				flag = false;
				value = (uint)_compressedSize;
				value2 = (uint)_uncompressedSize;
			}
			_offsetOfLocalHeader = binaryWriter.BaseStream.Position;
			int num = (flag ? zip64ExtraField.TotalSize : 0) + ((_lhUnknownExtraFields != null) ? ZipGenericExtraField.TotalSize(_lhUnknownExtraFields) : 0);
			ushort value3;
			if (num > 65535)
			{
				value3 = (ushort)(flag ? zip64ExtraField.TotalSize : 0);
				_lhUnknownExtraFields = null;
			}
			else
			{
				value3 = (ushort)num;
			}
			binaryWriter.Write(67324752u);
			binaryWriter.Write((ushort)_versionToExtract);
			binaryWriter.Write((ushort)_generalPurposeBitFlag);
			binaryWriter.Write((ushort)CompressionMethod);
			binaryWriter.Write(ZipHelper.DateTimeToDosTime(_lastModified.DateTime));
			binaryWriter.Write(_crc32);
			binaryWriter.Write(value);
			binaryWriter.Write(value2);
			binaryWriter.Write((ushort)_storedEntryNameBytes.Length);
			binaryWriter.Write(value3);
			binaryWriter.Write(_storedEntryNameBytes);
			if (flag)
			{
				zip64ExtraField.WriteBlock(_archive.ArchiveStream);
			}
			if (_lhUnknownExtraFields != null)
			{
				ZipGenericExtraField.WriteAllBlocks(_lhUnknownExtraFields, _archive.ArchiveStream);
			}
			return flag;
		}

		private void WriteLocalFileHeaderAndDataIfNeeded()
		{
			if (_storedUncompressedData != null || _compressedBytes != null)
			{
				if (_storedUncompressedData != null)
				{
					_uncompressedSize = _storedUncompressedData.Length;
					using Stream destination = new DirectToArchiveWriterStream(GetDataCompressor(_archive.ArchiveStream, leaveBackingStreamOpen: true, null), this);
					_storedUncompressedData.Seek(0L, SeekOrigin.Begin);
					_storedUncompressedData.CopyTo(destination);
					_storedUncompressedData.Dispose();
					_storedUncompressedData = null;
					return;
				}
				if (_uncompressedSize == 0L)
				{
					CompressionMethod = CompressionMethodValues.Stored;
				}
				WriteLocalFileHeader(isEmptyFile: false);
				byte[][] compressedBytes = _compressedBytes;
				foreach (byte[] array in compressedBytes)
				{
					_archive.ArchiveStream.Write(array, 0, array.Length);
				}
			}
			else if (_archive.Mode == ZipArchiveMode.Update || !_everOpenedForWrite)
			{
				_everOpenedForWrite = true;
				WriteLocalFileHeader(isEmptyFile: true);
			}
		}

		private void WriteCrcAndSizesInLocalHeader(bool zip64HeaderUsed)
		{
			long position = _archive.ArchiveStream.Position;
			BinaryWriter binaryWriter = new BinaryWriter(_archive.ArchiveStream);
			bool num = SizesTooLarge();
			bool flag = num && !zip64HeaderUsed;
			uint value = (uint)(num ? uint.MaxValue : _compressedSize);
			uint value2 = (uint)(num ? uint.MaxValue : _uncompressedSize);
			if (flag)
			{
				_generalPurposeBitFlag |= BitFlagValues.DataDescriptor;
				_archive.ArchiveStream.Seek(_offsetOfLocalHeader + 6, SeekOrigin.Begin);
				binaryWriter.Write((ushort)_generalPurposeBitFlag);
			}
			_archive.ArchiveStream.Seek(_offsetOfLocalHeader + 14, SeekOrigin.Begin);
			if (!flag)
			{
				binaryWriter.Write(_crc32);
				binaryWriter.Write(value);
				binaryWriter.Write(value2);
			}
			else
			{
				binaryWriter.Write(0u);
				binaryWriter.Write(0u);
				binaryWriter.Write(0u);
			}
			if (zip64HeaderUsed)
			{
				_archive.ArchiveStream.Seek(_offsetOfLocalHeader + 30 + _storedEntryNameBytes.Length + 4, SeekOrigin.Begin);
				binaryWriter.Write(_uncompressedSize);
				binaryWriter.Write(_compressedSize);
				_archive.ArchiveStream.Seek(position, SeekOrigin.Begin);
			}
			_archive.ArchiveStream.Seek(position, SeekOrigin.Begin);
			if (flag)
			{
				binaryWriter.Write(_crc32);
				binaryWriter.Write(_compressedSize);
				binaryWriter.Write(_uncompressedSize);
			}
		}

		private void WriteDataDescriptor()
		{
			BinaryWriter binaryWriter = new BinaryWriter(_archive.ArchiveStream);
			binaryWriter.Write(134695760u);
			binaryWriter.Write(_crc32);
			if (SizesTooLarge())
			{
				binaryWriter.Write(_compressedSize);
				binaryWriter.Write(_uncompressedSize);
			}
			else
			{
				binaryWriter.Write((uint)_compressedSize);
				binaryWriter.Write((uint)_uncompressedSize);
			}
		}

		private void UnloadStreams()
		{
			if (_storedUncompressedData != null)
			{
				_storedUncompressedData.Dispose();
			}
			_compressedBytes = null;
			_outstandingWriteStream = null;
		}

		private void CloseStreams()
		{
			if (_outstandingWriteStream != null)
			{
				_outstandingWriteStream.Dispose();
			}
		}

		private void VersionToExtractAtLeast(ZipVersionNeededValues value)
		{
			if ((int)_versionToExtract < (int)value)
			{
				_versionToExtract = value;
			}
			if ((int)_versionMadeBySpecification < (int)value)
			{
				_versionMadeBySpecification = value;
			}
		}

		private void ThrowIfInvalidArchive()
		{
			if (_archive == null)
			{
				throw new InvalidOperationException("Cannot modify deleted entry.");
			}
			_archive.ThrowIfDisposed();
		}

		private static string GetFileName_Windows(string path)
		{
			int num = path.Length;
			while (--num >= 0)
			{
				char c = path[num];
				if (c == '\\' || c == '/' || c == ':')
				{
					return path.Substring(num + 1);
				}
			}
			return path;
		}

		private static string GetFileName_Unix(string path)
		{
			int num = path.Length;
			while (--num >= 0)
			{
				if (path[num] == '/')
				{
					return path.Substring(num + 1);
				}
			}
			return path;
		}

		internal static string ParseFileName(string path, ZipVersionMadeByPlatform madeByPlatform)
		{
			return madeByPlatform switch
			{
				ZipVersionMadeByPlatform.Windows => GetFileName_Windows(path), 
				ZipVersionMadeByPlatform.Unix => GetFileName_Unix(path), 
				_ => ParseFileName(path, CurrentZipPlatform), 
			};
		}

		internal ZipArchiveEntry()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Specifies values for interacting with zip archive entries.</summary>
	public enum ZipArchiveMode
	{
		/// <summary>Only reading archive entries is permitted.</summary>
		Read,
		/// <summary>Only creating new archive entries is permitted.</summary>
		Create,
		/// <summary>Both read and write operations are permitted for archive entries.</summary>
		Update
	}
	internal struct ZipGenericExtraField
	{
		private const int SizeOfHeader = 4;

		private ushort _tag;

		private ushort _size;

		private byte[] _data;

		public ushort Tag => _tag;

		public ushort Size => _size;

		public byte[] Data => _data;

		public void WriteBlock(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(Tag);
			binaryWriter.Write(Size);
			binaryWriter.Write(Data);
		}

		public static bool TryReadBlock(BinaryReader reader, long endExtraField, out ZipGenericExtraField field)
		{
			field = default(ZipGenericExtraField);
			if (endExtraField - reader.BaseStream.Position < 4)
			{
				return false;
			}
			field._tag = reader.ReadUInt16();
			field._size = reader.ReadUInt16();
			if (endExtraField - reader.BaseStream.Position < field._size)
			{
				return false;
			}
			field._data = reader.ReadBytes(field._size);
			return true;
		}

		public static List<ZipGenericExtraField> ParseExtraField(Stream extraFieldData)
		{
			List<ZipGenericExtraField> list = new List<ZipGenericExtraField>();
			using BinaryReader reader = new BinaryReader(extraFieldData);
			ZipGenericExtraField field;
			while (TryReadBlock(reader, extraFieldData.Length, out field))
			{
				list.Add(field);
			}
			return list;
		}

		public static int TotalSize(List<ZipGenericExtraField> fields)
		{
			int num = 0;
			foreach (ZipGenericExtraField field in fields)
			{
				num += field.Size + 4;
			}
			return num;
		}

		public static void WriteAllBlocks(List<ZipGenericExtraField> fields, Stream stream)
		{
			foreach (ZipGenericExtraField field in fields)
			{
				field.WriteBlock(stream);
			}
		}
	}
	internal struct Zip64ExtraField
	{
		public const int OffsetToFirstField = 4;

		private const ushort TagConstant = 1;

		private ushort _size;

		private long? _uncompressedSize;

		private long? _compressedSize;

		private long? _localHeaderOffset;

		private int? _startDiskNumber;

		public ushort TotalSize => (ushort)(_size + 4);

		public long? UncompressedSize
		{
			get
			{
				return _uncompressedSize;
			}
			set
			{
				_uncompressedSize = value;
				UpdateSize();
			}
		}

		public long? CompressedSize
		{
			get
			{
				return _compressedSize;
			}
			set
			{
				_compressedSize = value;
				UpdateSize();
			}
		}

		public long? LocalHeaderOffset
		{
			get
			{
				return _localHeaderOffset;
			}
			set
			{
				_localHeaderOffset = value;
				UpdateSize();
			}
		}

		public int? StartDiskNumber => _startDiskNumber;

		private void UpdateSize()
		{
			_size = 0;
			if (_uncompressedSize.HasValue)
			{
				_size += 8;
			}
			if (_compressedSize.HasValue)
			{
				_size += 8;
			}
			if (_localHeaderOffset.HasValue)
			{
				_size += 8;
			}
			if (_startDiskNumber.HasValue)
			{
				_size += 4;
			}
		}

		public static Zip64ExtraField GetJustZip64Block(Stream extraFieldStream, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber)
		{
			using (BinaryReader reader = new BinaryReader(extraFieldStream))
			{
				ZipGenericExtraField field;
				while (ZipGenericExtraField.TryReadBlock(reader, extraFieldStream.Length, out field))
				{
					if (TryGetZip64BlockFromGenericExtraField(field, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber, out var zip64Block))
					{
						return zip64Block;
					}
				}
			}
			return new Zip64ExtraField
			{
				_compressedSize = null,
				_uncompressedSize = null,
				_localHeaderOffset = null,
				_startDiskNumber = null
			};
		}

		private static bool TryGetZip64BlockFromGenericExtraField(ZipGenericExtraField extraField, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber, out Zip64ExtraField zip64Block)
		{
			zip64Block = default(Zip64ExtraField);
			zip64Block._compressedSize = null;
			zip64Block._uncompressedSize = null;
			zip64Block._localHeaderOffset = null;
			zip64Block._startDiskNumber = null;
			if (extraField.Tag != 1)
			{
				return false;
			}
			MemoryStream memoryStream = null;
			try
			{
				memoryStream = new MemoryStream(extraField.Data);
				using BinaryReader binaryReader = new BinaryReader(memoryStream);
				memoryStream = null;
				zip64Block._size = extraField.Size;
				ushort num = 0;
				if (readUncompressedSize)
				{
					num += 8;
				}
				if (readCompressedSize)
				{
					num += 8;
				}
				if (readLocalHeaderOffset)
				{
					num += 8;
				}
				if (readStartDiskNumber)
				{
					num += 4;
				}
				if (num != zip64Block._size)
				{
					return false;
				}
				if (readUncompressedSize)
				{
					zip64Block._uncompressedSize = binaryReader.ReadInt64();
				}
				if (readCompressedSize)
				{
					zip64Block._compressedSize = binaryReader.ReadInt64();
				}
				if (readLocalHeaderOffset)
				{
					zip64Block._localHeaderOffset = binaryReader.ReadInt64();
				}
				if (readStartDiskNumber)
				{
					zip64Block._startDiskNumber = binaryReader.ReadInt32();
				}
				if (zip64Block._uncompressedSize < 0)
				{
					throw new InvalidDataException("Uncompressed Size cannot be held in an Int64.");
				}
				if (zip64Block._compressedSize < 0)
				{
					throw new InvalidDataException("Compressed Size cannot be held in an Int64.");
				}
				if (zip64Block._localHeaderOffset < 0)
				{
					throw new InvalidDataException("Local Header Offset cannot be held in an Int64.");
				}
				if (zip64Block._startDiskNumber < 0)
				{
					throw new InvalidDataException("Start Disk Number cannot be held in an Int64.");
				}
				return true;
			}
			finally
			{
				memoryStream?.Dispose();
			}
		}

		public static Zip64ExtraField GetAndRemoveZip64Block(List<ZipGenericExtraField> extraFields, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber)
		{
			Zip64ExtraField zip64Block = new Zip64ExtraField
			{
				_compressedSize = null,
				_uncompressedSize = null,
				_localHeaderOffset = null,
				_startDiskNumber = null
			};
			List<ZipGenericExtraField> list = new List<ZipGenericExtraField>();
			bool flag = false;
			foreach (ZipGenericExtraField extraField in extraFields)
			{
				if (extraField.Tag == 1)
				{
					list.Add(extraField);
					if (!flag && TryGetZip64BlockFromGenericExtraField(extraField, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber, out zip64Block))
					{
						flag = true;
					}
				}
			}
			foreach (ZipGenericExtraField item in list)
			{
				extraFields.Remove(item);
			}
			return zip64Block;
		}

		public static void RemoveZip64Blocks(List<ZipGenericExtraField> extraFields)
		{
			List<ZipGenericExtraField> list = new List<ZipGenericExtraField>();
			foreach (ZipGenericExtraField extraField in extraFields)
			{
				if (extraField.Tag == 1)
				{
					list.Add(extraField);
				}
			}
			foreach (ZipGenericExtraField item in list)
			{
				extraFields.Remove(item);
			}
		}

		public void WriteBlock(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write((ushort)1);
			binaryWriter.Write(_size);
			if (_uncompressedSize.HasValue)
			{
				binaryWriter.Write(_uncompressedSize.Value);
			}
			if (_compressedSize.HasValue)
			{
				binaryWriter.Write(_compressedSize.Value);
			}
			if (_localHeaderOffset.HasValue)
			{
				binaryWriter.Write(_localHeaderOffset.Value);
			}
			if (_startDiskNumber.HasValue)
			{
				binaryWriter.Write(_startDiskNumber.Value);
			}
		}
	}
	internal struct Zip64EndOfCentralDirectoryLocator
	{
		public const uint SignatureConstant = 117853008u;

		public const int SizeOfBlockWithoutSignature = 16;

		public uint NumberOfDiskWithZip64EOCD;

		public ulong OffsetOfZip64EOCD;

		public uint TotalNumberOfDisks;

		public static bool TryReadBlock(BinaryReader reader, out Zip64EndOfCentralDirectoryLocator zip64EOCDLocator)
		{
			zip64EOCDLocator = default(Zip64EndOfCentralDirectoryLocator);
			if (reader.ReadUInt32() != 117853008)
			{
				return false;
			}
			zip64EOCDLocator.NumberOfDiskWithZip64EOCD = reader.ReadUInt32();
			zip64EOCDLocator.OffsetOfZip64EOCD = reader.ReadUInt64();
			zip64EOCDLocator.TotalNumberOfDisks = reader.ReadUInt32();
			return true;
		}

		public static void WriteBlock(Stream stream, long zip64EOCDRecordStart)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(117853008u);
			binaryWriter.Write(0u);
			binaryWriter.Write(zip64EOCDRecordStart);
			binaryWriter.Write(1u);
		}
	}
	internal struct Zip64EndOfCentralDirectoryRecord
	{
		private const uint SignatureConstant = 101075792u;

		private const ulong NormalSize = 44uL;

		public ulong SizeOfThisRecord;

		public ushort VersionMadeBy;

		public ushort VersionNeededToExtract;

		public uint NumberOfThisDisk;

		public uint NumberOfDiskWithStartOfCD;

		public ulong NumberOfEntriesOnThisDisk;

		public ulong NumberOfEntriesTotal;

		public ulong SizeOfCentralDirectory;

		public ulong OffsetOfCentralDirectory;

		public static bool TryReadBlock(BinaryReader reader, out Zip64EndOfCentralDirectoryRecord zip64EOCDRecord)
		{
			zip64EOCDRecord = default(Zip64EndOfCentralDirectoryRecord);
			if (reader.ReadUInt32() != 101075792)
			{
				return false;
			}
			zip64EOCDRecord.SizeOfThisRecord = reader.ReadUInt64();
			zip64EOCDRecord.VersionMadeBy = reader.ReadUInt16();
			zip64EOCDRecord.VersionNeededToExtract = reader.ReadUInt16();
			zip64EOCDRecord.NumberOfThisDisk = reader.ReadUInt32();
			zip64EOCDRecord.NumberOfDiskWithStartOfCD = reader.ReadUInt32();
			zip64EOCDRecord.NumberOfEntriesOnThisDisk = reader.ReadUInt64();
			zip64EOCDRecord.NumberOfEntriesTotal = reader.ReadUInt64();
			zip64EOCDRecord.SizeOfCentralDirectory = reader.ReadUInt64();
			zip64EOCDRecord.OffsetOfCentralDirectory = reader.ReadUInt64();
			return true;
		}

		public static void WriteBlock(Stream stream, long numberOfEntries, long startOfCentralDirectory, long sizeOfCentralDirectory)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(101075792u);
			binaryWriter.Write(44uL);
			binaryWriter.Write((ushort)45);
			binaryWriter.Write((ushort)45);
			binaryWriter.Write(0u);
			binaryWriter.Write(0u);
			binaryWriter.Write(numberOfEntries);
			binaryWriter.Write(numberOfEntries);
			binaryWriter.Write(sizeOfCentralDirectory);
			binaryWriter.Write(startOfCentralDirectory);
		}
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal readonly struct ZipLocalFileHeader
	{
		public const uint DataDescriptorSignature = 134695760u;

		public const uint SignatureConstant = 67324752u;

		public const int OffsetToCrcFromHeaderStart = 14;

		public const int OffsetToBitFlagFromHeaderStart = 6;

		public const int SizeOfLocalHeader = 30;

		public static List<ZipGenericExtraField> GetExtraFields(BinaryReader reader)
		{
			reader.BaseStream.Seek(26L, SeekOrigin.Current);
			ushort num = reader.ReadUInt16();
			ushort num2 = reader.ReadUInt16();
			reader.BaseStream.Seek(num, SeekOrigin.Current);
			List<ZipGenericExtraField> list;
			using (Stream extraFieldData = new SubReadStream(reader.BaseStream, reader.BaseStream.Position, num2))
			{
				list = ZipGenericExtraField.ParseExtraField(extraFieldData);
			}
			Zip64ExtraField.RemoveZip64Blocks(list);
			return list;
		}

		public static bool TrySkipBlock(BinaryReader reader)
		{
			if (reader.ReadUInt32() != 67324752)
			{
				return false;
			}
			if (reader.BaseStream.Length < reader.BaseStream.Position + 22)
			{
				return false;
			}
			reader.BaseStream.Seek(22L, SeekOrigin.Current);
			ushort num = reader.ReadUInt16();
			ushort num2 = reader.ReadUInt16();
			if (reader.BaseStream.Length < reader.BaseStream.Position + num + num2)
			{
				return false;
			}
			reader.BaseStream.Seek(num + num2, SeekOrigin.Current);
			return true;
		}
	}
	internal struct ZipCentralDirectoryFileHeader
	{
		public const uint SignatureConstant = 33639248u;

		public byte VersionMadeByCompatibility;

		public byte VersionMadeBySpecification;

		public ushort VersionNeededToExtract;

		public ushort GeneralPurposeBitFlag;

		public ushort CompressionMethod;

		public uint LastModified;

		public uint Crc32;

		public long CompressedSize;

		public long UncompressedSize;

		public ushort FilenameLength;

		public ushort ExtraFieldLength;

		public ushort FileCommentLength;

		public int DiskNumberStart;

		public ushort InternalFileAttributes;

		public uint ExternalFileAttributes;

		public long RelativeOffsetOfLocalHeader;

		public byte[] Filename;

		public byte[] FileComment;

		public List<ZipGenericExtraField> ExtraFields;

		public static bool TryReadBlock(BinaryReader reader, bool saveExtraFieldsAndComments, out ZipCentralDirectoryFileHeader header)
		{
			header = default(ZipCentralDirectoryFileHeader);
			if (reader.ReadUInt32() != 33639248)
			{
				return false;
			}
			header.VersionMadeBySpecification = reader.ReadByte();
			header.VersionMadeByCompatibility = reader.ReadByte();
			header.VersionNeededToExtract = reader.ReadUInt16();
			header.GeneralPurposeBitFlag = reader.ReadUInt16();
			header.CompressionMethod = reader.ReadUInt16();
			header.LastModified = reader.ReadUInt32();
			header.Crc32 = reader.ReadUInt32();
			uint num = reader.ReadUInt32();
			uint num2 = reader.ReadUInt32();
			header.FilenameLength = reader.ReadUInt16();
			header.ExtraFieldLength = reader.ReadUInt16();
			header.FileCommentLength = reader.ReadUInt16();
			ushort num3 = reader.ReadUInt16();
			header.InternalFileAttributes = reader.ReadUInt16();
			header.ExternalFileAttributes = reader.ReadUInt32();
			uint num4 = reader.ReadUInt32();
			header.Filename = reader.ReadBytes(header.FilenameLength);
			bool readUncompressedSize = num2 == uint.MaxValue;
			bool readCompressedSize = num == uint.MaxValue;
			bool readLocalHeaderOffset = num4 == uint.MaxValue;
			bool readStartDiskNumber = num3 == ushort.MaxValue;
			long position = reader.BaseStream.Position + header.ExtraFieldLength;
			Zip64ExtraField zip64ExtraField;
			using (Stream stream = new SubReadStream(reader.BaseStream, reader.BaseStream.Position, header.ExtraFieldLength))
			{
				if (saveExtraFieldsAndComments)
				{
					header.ExtraFields = ZipGenericExtraField.ParseExtraField(stream);
					zip64ExtraField = Zip64ExtraField.GetAndRemoveZip64Block(header.ExtraFields, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber);
				}
				else
				{
					header.ExtraFields = null;
					zip64ExtraField = Zip64ExtraField.GetJustZip64Block(stream, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber);
				}
			}
			reader.BaseStream.AdvanceToPosition(position);
			if (saveExtraFieldsAndComments)
			{
				header.FileComment = reader.ReadBytes(header.FileCommentLength);
			}
			else
			{
				reader.BaseStream.Position += header.FileCommentLength;
				header.FileComment = null;
			}
			header.UncompressedSize = ((!zip64ExtraField.UncompressedSize.HasValue) ? num2 : zip64ExtraField.UncompressedSize.Value);
			header.CompressedSize = ((!zip64ExtraField.CompressedSize.HasValue) ? num : zip64ExtraField.CompressedSize.Value);
			header.RelativeOffsetOfLocalHeader = ((!zip64ExtraField.LocalHeaderOffset.HasValue) ? num4 : zip64ExtraField.LocalHeaderOffset.Value);
			header.DiskNumberStart = ((!zip64ExtraField.StartDiskNumber.HasValue) ? num3 : zip64ExtraField.StartDiskNumber.Value);
			return true;
		}
	}
	internal struct ZipEndOfCentralDirectoryBlock
	{
		public const uint SignatureConstant = 101010256u;

		public const int SizeOfBlockWithoutSignature = 18;

		public uint Signature;

		public ushort NumberOfThisDisk;

		public ushort NumberOfTheDiskWithTheStartOfTheCentralDirectory;

		public ushort NumberOfEntriesInTheCentralDirectoryOnThisDisk;

		public ushort NumberOfEntriesInTheCentralDirectory;

		public uint SizeOfCentralDirectory;

		public uint OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber;

		public byte[] ArchiveComment;

		public static void WriteBlock(Stream stream, long numberOfEntries, long startOfCentralDirectory, long sizeOfCentralDirectory, byte[] archiveComment)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			ushort value = ((numberOfEntries > 65535) ? ushort.MaxValue : ((ushort)numberOfEntries));
			uint value2 = (uint)((startOfCentralDirectory > uint.MaxValue) ? uint.MaxValue : startOfCentralDirectory);
			uint value3 = (uint)((sizeOfCentralDirectory > uint.MaxValue) ? uint.MaxValue : sizeOfCentralDirectory);
			binaryWriter.Write(101010256u);
			binaryWriter.Write((ushort)0);
			binaryWriter.Write((ushort)0);
			binaryWriter.Write(value);
			binaryWriter.Write(value);
			binaryWriter.Write(value3);
			binaryWriter.Write(value2);
			binaryWriter.Write((ushort)((archiveComment != null) ? ((ushort)archiveComment.Length) : 0));
			if (archiveComment != null)
			{
				binaryWriter.Write(archiveComment);
			}
		}

		public static bool TryReadBlock(BinaryReader reader, out ZipEndOfCentralDirectoryBlock eocdBlock)
		{
			eocdBlock = default(ZipEndOfCentralDirectoryBlock);
			if (reader.ReadUInt32() != 101010256)
			{
				return false;
			}
			eocdBlock.Signature = 101010256u;
			eocdBlock.NumberOfThisDisk = reader.ReadUInt16();
			eocdBlock.NumberOfTheDiskWithTheStartOfTheCentralDirectory = reader.ReadUInt16();
			eocdBlock.NumberOfEntriesInTheCentralDirectoryOnThisDisk = reader.ReadUInt16();
			eocdBlock.NumberOfEntriesInTheCentralDirectory = reader.ReadUInt16();
			eocdBlock.SizeOfCentralDirectory = reader.ReadUInt32();
			eocdBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber = reader.ReadUInt32();
			ushort count = reader.ReadUInt16();
			eocdBlock.ArchiveComment = reader.ReadBytes(count);
			return true;
		}
	}
	internal sealed class WrappedStream : Stream
	{
		private readonly Stream _baseStream;

		private readonly bool _closeBaseStream;

		private readonly Action<ZipArchiveEntry> _onClosed;

		private readonly ZipArchiveEntry _zipArchiveEntry;

		private bool _isDisposed;

		public override long Length
		{
			get
			{
				ThrowIfDisposed();
				return _baseStream.Length;
			}
		}

		public override long Position
		{
			get
			{
				ThrowIfDisposed();
				return _baseStream.Position;
			}
			set
			{
				ThrowIfDisposed();
				ThrowIfCantSeek();
				_baseStream.Position = value;
			}
		}

		public override bool CanRead
		{
			get
			{
				if (!_isDisposed)
				{
					return _baseStream.CanRead;
				}
				return false;
			}
		}

		public override bool CanSeek
		{
			get
			{
				if (!_isDisposed)
				{
					return _baseStream.CanSeek;
				}
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				if (!_isDisposed)
				{
					return _baseStream.CanWrite;
				}
				return false;
			}
		}

		internal WrappedStream(Stream baseStream, bool closeBaseStream)
			: this(baseStream, closeBaseStream, null, null)
		{
		}

		private WrappedStream(Stream baseStream, bool closeBaseStream, ZipArchiveEntry entry, Action<ZipArchiveEntry> onClosed)
		{
			_baseStream = baseStream;
			_closeBaseStream = closeBaseStream;
			_onClosed = onClosed;
			_zipArchiveEntry = entry;
			_isDisposed = false;
		}

		internal WrappedStream(Stream baseStream, ZipArchiveEntry entry, Action<ZipArchiveEntry> onClosed)
			: this(baseStream, closeBaseStream: false, entry, onClosed)
		{
		}

		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw new ObjectDisposedException(GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
			}
		}

		private void ThrowIfCantRead()
		{
			if (!CanRead)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
			}
		}

		private void ThrowIfCantWrite()
		{
			if (!CanWrite)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support writing.");
			}
		}

		private void ThrowIfCantSeek()
		{
			if (!CanSeek)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			ThrowIfDisposed();
			ThrowIfCantRead();
			return _baseStream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			ThrowIfDisposed();
			ThrowIfCantSeek();
			return _baseStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			ThrowIfDisposed();
			ThrowIfCantSeek();
			ThrowIfCantWrite();
			_baseStream.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			ThrowIfDisposed();
			ThrowIfCantWrite();
			_baseStream.Write(buffer, offset, count);
		}

		public override void Flush()
		{
			ThrowIfDisposed();
			ThrowIfCantWrite();
			_baseStream.Flush();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !_isDisposed)
			{
				_onClosed?.Invoke(_zipArchiveEntry);
				if (_closeBaseStream)
				{
					_baseStream.Dispose();
				}
				_isDisposed = true;
			}
			base.Dispose(disposing);
		}
	}
	internal sealed class SubReadStream : Stream
	{
		private readonly long _startInSuperStream;

		private long _positionInSuperStream;

		private readonly long _endInSuperStream;

		private readonly Stream _superStream;

		private bool _canRead;

		private bool _isDisposed;

		public override long Length
		{
			get
			{
				ThrowIfDisposed();
				return _endInSuperStream - _startInSuperStream;
			}
		}

		public override long Position
		{
			get
			{
				ThrowIfDisposed();
				return _positionInSuperStream - _startInSuperStream;
			}
			set
			{
				ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		public override bool CanRead
		{
			get
			{
				if (_superStream.CanRead)
				{
					return _canRead;
				}
				return false;
			}
		}

		public override bool CanSeek => false;

		public override bool CanWrite => false;

		public SubReadStream(Stream superStream, long startPosition, long maxLength)
		{
			_startInSuperStream = startPosition;
			_positionInSuperStream = startPosition;
			_endInSuperStream = startPosition + maxLength;
			_superStream = superStream;
			_canRead = true;
			_isDisposed = false;
		}

		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw new ObjectDisposedException(GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
			}
		}

		private void ThrowIfCantRead()
		{
			if (!CanRead)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			ThrowIfDisposed();
			ThrowIfCantRead();
			if (_superStream.Position != _positionInSuperStream)
			{
				_superStream.Seek(_positionInSuperStream, SeekOrigin.Begin);
			}
			if (_positionInSuperStream + count > _endInSuperStream)
			{
				count = (int)(_endInSuperStream - _positionInSuperStream);
			}
			int num = _superStream.Read(buffer, offset, count);
			_positionInSuperStream += num;
			return num;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
		}

		public override void SetLength(long value)
		{
			ThrowIfDisposed();
			throw new NotSupportedException("SetLength requires a stream that supports seeking and writing.");
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support writing.");
		}

		public override void Flush()
		{
			ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support writing.");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !_isDisposed)
			{
				_canRead = false;
				_isDisposed = true;
			}
			base.Dispose(disposing);
		}
	}
	internal sealed class CheckSumAndSizeWriteStream : Stream
	{
		private readonly Stream _baseStream;

		private readonly Stream _baseBaseStream;

		private long _position;

		private uint _checksum;

		private readonly bool _leaveOpenOnClose;

		private bool _canWrite;

		private bool _isDisposed;

		private bool _everWritten;

		private long _initialPosition;

		private readonly ZipArchiveEntry _zipArchiveEntry;

		private readonly EventHandler _onClose;

		private readonly Action<long, long, uint, Stream, ZipArchiveEntry, EventHandler> _saveCrcAndSizes;

		public override long Length
		{
			get
			{
				ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		public override long Position
		{
			get
			{
				ThrowIfDisposed();
				return _position;
			}
			set
			{
				ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		public override bool CanRead => false;

		public override bool CanSeek => false;

		public override bool CanWrite => _canWrite;

		public CheckSumAndSizeWriteStream(Stream baseStream, Stream baseBaseStream, bool leaveOpenOnClose, ZipArchiveEntry entry, EventHandler onClose, Action<long, long, uint, Stream, ZipArchiveEntry, EventHandler> saveCrcAndSizes)
		{
			_baseStream = baseStream;
			_baseBaseStream = baseBaseStream;
			_position = 0L;
			_checksum = 0u;
			_leaveOpenOnClose = leaveOpenOnClose;
			_canWrite = true;
			_isDisposed = false;
			_initialPosition = 0L;
			_zipArchiveEntry = entry;
			_onClose = onClose;
			_saveCrcAndSizes = saveCrcAndSizes;
		}

		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw new ObjectDisposedException(GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
		}

		public override void SetLength(long value)
		{
			ThrowIfDisposed();
			throw new NotSupportedException("SetLength requires a stream that supports seeking and writing.");
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "The argument must be non-negative.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "The argument must be non-negative.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("The offset and length parameters are not valid for the array that was given.");
			}
			ThrowIfDisposed();
			if (count != 0)
			{
				if (!_everWritten)
				{
					_initialPosition = _baseBaseStream.Position;
					_everWritten = true;
				}
				_checksum = Crc32Helper.UpdateCrc32(_checksum, buffer, offset, count);
				_baseStream.Write(buffer, offset, count);
				_position += count;
			}
		}

		public override void Flush()
		{
			ThrowIfDisposed();
			_baseStream.Flush();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !_isDisposed)
			{
				if (!_everWritten)
				{
					_initialPosition = _baseBaseStream.Position;
				}
				if (!_leaveOpenOnClose)
				{
					_baseStream.Dispose();
				}
				_saveCrcAndSizes?.Invoke(_initialPosition, Position, _checksum, _baseBaseStream, _zipArchiveEntry, _onClose);
				_isDisposed = true;
			}
			base.Dispose(disposing);
		}
	}
	internal static class ZipHelper
	{
		internal const uint Mask32Bit = uint.MaxValue;

		internal const ushort Mask16Bit = ushort.MaxValue;

		private const int BackwardsSeekingBufferSize = 32;

		internal const int ValidZipDate_YearMin = 1980;

		internal const int ValidZipDate_YearMax = 2107;

		private static readonly DateTime s_invalidDateIndicator = new DateTime(1980, 1, 1, 0, 0, 0);

		internal static bool RequiresUnicode(string test)
		{
			foreach (char c in test)
			{
				if (c > '~' || c < ' ')
				{
					return true;
				}
			}
			return false;
		}

		internal static void ReadBytes(Stream stream, byte[] buffer, int bytesToRead)
		{
			int num = bytesToRead;
			int num2 = 0;
			while (num > 0)
			{
				int num3 = stream.Read(buffer, num2, num);
				if (num3 == 0)
				{
					throw new IOException("Zip file corrupt: unexpected end of stream reached.");
				}
				num2 += num3;
				num -= num3;
			}
		}

		internal static DateTime DosTimeToDateTime(uint dateTime)
		{
			int year = (int)(1980 + (dateTime >> 25));
			int month = (int)((dateTime >> 21) & 0xF);
			int day = (int)((dateTime >> 16) & 0x1F);
			int hour = (int)((dateTime >> 11) & 0x1F);
			int minute = (int)((dateTime >> 5) & 0x3F);
			int second = (int)((dateTime & 0x1F) * 2);
			try
			{
				return new DateTime(year, month, day, hour, minute, second, 0);
			}
			catch (ArgumentOutOfRangeException)
			{
				return s_invalidDateIndicator;
			}
			catch (ArgumentException)
			{
				return s_invalidDateIndicator;
			}
		}

		internal static uint DateTimeToDosTime(DateTime dateTime)
		{
			return (uint)((((((((dateTime.Year - 1980) & 0x7F) << 4) + dateTime.Month << 5) + dateTime.Day << 5) + dateTime.Hour << 6) + dateTime.Minute << 5) + dateTime.Second / 2);
		}

		internal static bool SeekBackwardsToSignature(Stream stream, uint signatureToFind)
		{
			int bufferPointer = 0;
			uint num = 0u;
			byte[] array = new byte[32];
			bool flag = false;
			bool flag2 = false;
			while (!flag2 && !flag)
			{
				flag = SeekBackwardsAndRead(stream, array, out bufferPointer);
				while (bufferPointer >= 0 && !flag2)
				{
					num = (num << 8) | array[bufferPointer];
					if (num == signatureToFind)
					{
						flag2 = true;
					}
					else
					{
						bufferPointer--;
					}
				}
			}
			if (!flag2)
			{
				return false;
			}
			stream.Seek(bufferPointer, SeekOrigin.Current);
			return true;
		}

		internal static void AdvanceToPosition(this Stream stream, long position)
		{
			long num = position - stream.Position;
			while (num != 0L)
			{
				int count = (int)((num > 64) ? 64 : num);
				int num2 = stream.Read(new byte[64], 0, count);
				if (num2 == 0)
				{
					throw new IOException("Zip file corrupt: unexpected end of stream reached.");
				}
				num -= num2;
			}
		}

		private static bool SeekBackwardsAndRead(Stream stream, byte[] buffer, out int bufferPointer)
		{
			if (stream.Position >= buffer.Length)
			{
				stream.Seek(-buffer.Length, SeekOrigin.Current);
				ReadBytes(stream, buffer, buffer.Length);
				stream.Seek(-buffer.Length, SeekOrigin.Current);
				bufferPointer = buffer.Length - 1;
				return false;
			}
			int num = (int)stream.Position;
			stream.Seek(0L, SeekOrigin.Begin);
			ReadBytes(stream, buffer, num);
			stream.Seek(0L, SeekOrigin.Begin);
			bufferPointer = num - 1;
			return true;
		}
	}
	internal enum ZipVersionNeededValues : ushort
	{
		Default = 10,
		ExplicitDirectory = 20,
		Deflate = 20,
		Deflate64 = 21,
		Zip64 = 45
	}
	internal enum ZipVersionMadeByPlatform : byte
	{
		Windows = 0,
		Unix = 3
	}
	internal static class Crc32Helper
	{
		private static readonly uint[] s_crcTable_0 = new uint[256]
		{
			0u, 1996959894u, 3993919788u, 2567524794u, 124634137u, 1886057615u, 3915621685u, 2657392035u, 249268274u, 2044508324u,
			3772115230u, 2547177864u, 162941995u, 2125561021u, 3887607047u, 2428444049u, 498536548u, 1789927666u, 4089016648u, 2227061214u,
			450548861u, 1843258603u, 4107580753u, 2211677639u, 325883990u, 1684777152u, 4251122042u, 2321926636u, 335633487u, 1661365465u,
			4195302755u, 2366115317u, 997073096u, 1281953886u, 3579855332u, 2724688242u, 1006888145u, 1258607687u, 3524101629u, 2768942443u,
			901097722u, 1119000684u, 3686517206u, 2898065728u, 853044451u, 1172266101u, 3705015759u, 2882616665u, 651767980u, 1373503546u,
			3369554304u, 3218104598u, 565507253u, 1454621731u, 3485111705u, 3099436303u, 671266974u, 1594198024u, 3322730930u, 2970347812u,
			795835527u, 1483230225u, 3244367275u, 3060149565u, 1994146192u, 31158534u, 2563907772u, 4023717930u, 1907459465u, 112637215u,
			2680153253u, 3904427059u, 2013776290u, 251722036u, 2517215374u, 3775830040u, 2137656763u, 141376813u, 2439277719u, 3865271297u,
			1802195444u, 476864866u, 2238001368u, 4066508878u, 1812370925u, 453092731u, 2181625025u, 4111451223u, 1706088902u, 314042704u,
			2344532202u, 4240017532u, 1658658271u, 366619977u, 2362670323u, 4224994405u, 1303535960u, 984961486u, 2747007092u, 3569037538u,
			1256170817u, 1037604311u, 2765210733u, 3554079995u, 1131014506u, 879679996u, 2909243462u, 3663771856u, 1141124467u, 855842277u,
			2852801631u, 3708648649u, 1342533948u, 654459306u, 3188396048u, 3373015174u, 1466479909u, 544179635u, 3110523913u, 3462522015u,
			1591671054u, 702138776u, 2966460450u, 3352799412u, 1504918807u, 783551873u, 3082640443u, 3233442989u, 3988292384u, 2596254646u,
			62317068u, 1957810842u, 3939845945u, 2647816111u, 81470997u, 1943803523u, 3814918930u, 2489596804u, 225274430u, 2053790376u,
			3826175755u, 2466906013u, 167816743u, 2097651377u, 4027552580u, 2265490386u, 503444072u, 1762050814u, 4150417245u, 2154129355u,
			426522225u, 1852507879u, 4275313526u, 2312317920u, 282753626u, 1742555852u, 4189708143u, 2394877945u, 397917763u, 1622183637u,
			3604390888u, 2714866558u, 953729732u, 1340076626u, 3518719985u, 2797360999u, 1068828381u, 1219638859u, 3624741850u, 2936675148u,
			906185462u, 1090812512u, 3747672003u, 2825379669u, 829329135u, 1181335161u, 3412177804u, 3160834842u, 628085408u, 1382605366u,
			3423369109u, 3138078467u, 570562233u, 1426400815u, 3317316542u, 2998733608u, 733239954u, 1555261956u, 3268935591u, 3050360625u,
			752459403u, 1541320221u, 2607071920u, 3965973030u, 1969922972u, 40735498u, 2617837225u, 3943577151u, 1913087877u, 83908371u,
			2512341634u, 3803740692u, 2075208622u, 213261112u, 2463272603u, 3855990285u, 2094854071u, 198958881u, 2262029012u, 4057260610u,
			1759359992u, 534414190u, 2176718541u, 4139329115u, 1873836001u, 414664567u, 2282248934u, 4279200368u, 1711684554u, 285281116u,
			2405801727u, 4167216745u, 1634467795u, 376229701u, 2685067896u, 3608007406u, 1308918612u, 956543938u, 2808555105u, 3495958263u,
			1231636301u, 1047427035u, 2932959818u, 3654703836u, 1088359270u, 936918000u, 2847714899u, 3736837829u, 1202900863u, 817233897u,
			3183342108u, 3401237130u, 1404277552u, 615818150u, 3134207493u, 3453421203u, 1423857449u, 601450431u, 3009837614u, 3294710456u,
			1567103746u, 711928724u, 3020668471u, 3272380065u, 1510334235u, 755167117u
		};

		private static readonly uint[] s_crcTable_1 = new uint[256]
		{
			0u, 421212481u, 842424962u, 724390851u, 1684849924u, 2105013317u, 1448781702u, 1329698503u, 3369699848u, 3519200073u,
			4210026634u, 3824474571u, 2897563404u, 3048111693u, 2659397006u, 2274893007u, 1254232657u, 1406739216u, 2029285587u, 1643069842u,
			783210325u, 934667796u, 479770071u, 92505238u, 2182846553u, 2600511768u, 2955803355u, 2838940570u, 3866582365u, 4285295644u,
			3561045983u, 3445231262u, 2508465314u, 2359236067u, 2813478432u, 3198777185u, 4058571174u, 3908292839u, 3286139684u, 3670389349u,
			1566420650u, 1145479147u, 1869335592u, 1987116393u, 959540142u, 539646703u, 185010476u, 303839341u, 3745920755u, 3327985586u,
			3983561841u, 4100678960u, 3140154359u, 2721170102u, 2300350837u, 2416418868u, 396344571u, 243568058u, 631889529u, 1018359608u,
			1945336319u, 1793607870u, 1103436669u, 1490954812u, 4034481925u, 3915546180u, 3259968903u, 3679722694u, 2484439553u, 2366552896u,
			2787371139u, 3208174018u, 950060301u, 565965900u, 177645455u, 328046286u, 1556873225u, 1171730760u, 1861902987u, 2011255754u,
			3132841300u, 2745199637u, 2290958294u, 2442530455u, 3738671184u, 3352078609u, 3974232786u, 4126854035u, 1919080284u, 1803150877u,
			1079293406u, 1498383519u, 370020952u, 253043481u, 607678682u, 1025720731u, 1711106983u, 2095471334u, 1472923941u, 1322268772u,
			26324643u, 411738082u, 866634785u, 717028704u, 2904875439u, 3024081134u, 2668790573u, 2248782444u, 3376948395u, 3495106026u,
			4219356713u, 3798300520u, 792689142u, 908347575u, 487136116u, 68299317u, 1263779058u, 1380486579u, 2036719216u, 1618931505u,
			3890672638u, 4278043327u, 3587215740u, 3435896893u, 2206873338u, 2593195963u, 2981909624u, 2829542713u, 998479947u, 580430090u,
			162921161u, 279890824u, 1609522511u, 1190423566u, 1842954189u, 1958874764u, 4082766403u, 3930137346u, 3245109441u, 3631694208u,
			2536953671u, 2385372678u, 2768287173u, 3155920004u, 1900120602u, 1750776667u, 1131931800u, 1517083097u, 355290910u, 204897887u,
			656092572u, 1040194781u, 3113746450u, 2692952403u, 2343461520u, 2461357009u, 3723805974u, 3304059991u, 4022511508u, 4141455061u,
			2919742697u, 3072101800u, 2620513899u, 2234183466u, 3396041197u, 3547351212u, 4166851439u, 3779471918u, 1725839073u, 2143618976u,
			1424512099u, 1307796770u, 45282277u, 464110244u, 813994343u, 698327078u, 3838160568u, 4259225593u, 3606301754u, 3488152955u,
			2158586812u, 2578602749u, 2996767038u, 2877569151u, 740041904u, 889656817u, 506086962u, 120682355u, 1215357364u, 1366020341u,
			2051441462u, 1667084919u, 3422213966u, 3538019855u, 4190942668u, 3772220557u, 2945847882u, 3062702859u, 2644537544u, 2226864521u,
			52649286u, 439905287u, 823476164u, 672009861u, 1733269570u, 2119477507u, 1434057408u, 1281543041u, 2167981343u, 2552493150u,
			3004082077u, 2853541596u, 3847487515u, 4233048410u, 3613549209u, 3464057816u, 1239502615u, 1358593622u, 2077699477u, 1657543892u,
			764250643u, 882293586u, 532408465u, 111204816u, 1585378284u, 1197851309u, 1816695150u, 1968414767u, 974272232u, 587794345u,
			136598634u, 289367339u, 2527558116u, 2411481253u, 2760973158u, 3179948583u, 4073438432u, 3956313505u, 3237863010u, 3655790371u,
			347922877u, 229101820u, 646611775u, 1066513022u, 1892689081u, 1774917112u, 1122387515u, 1543337850u, 3697634229u, 3313392372u,
			3998419255u, 4148705398u, 3087642289u, 2702352368u, 2319436851u, 2468674930u
		};

		private static readonly uint[] s_crcTable_2 = new uint[256]
		{
			0u, 29518391u, 59036782u, 38190681u, 118073564u, 114017003u, 76381362u, 89069189u, 236147128u, 265370511u,
			228034006u, 206958561u, 152762724u, 148411219u, 178138378u, 190596925u, 472294256u, 501532999u, 530741022u, 509615401u,
			456068012u, 451764635u, 413917122u, 426358261u, 305525448u, 334993663u, 296822438u, 275991697u, 356276756u, 352202787u,
			381193850u, 393929805u, 944588512u, 965684439u, 1003065998u, 973863097u, 1061482044u, 1049003019u, 1019230802u, 1023561829u,
			912136024u, 933002607u, 903529270u, 874031361u, 827834244u, 815125939u, 852716522u, 856752605u, 611050896u, 631869351u,
			669987326u, 640506825u, 593644876u, 580921211u, 551983394u, 556069653u, 712553512u, 733666847u, 704405574u, 675154545u,
			762387700u, 749958851u, 787859610u, 792175277u, 1889177024u, 1901651959u, 1931368878u, 1927033753u, 2006131996u, 1985040171u,
			1947726194u, 1976933189u, 2122964088u, 2135668303u, 2098006038u, 2093965857u, 2038461604u, 2017599123u, 2047123658u, 2076625661u,
			1824272048u, 1836991623u, 1866005214u, 1861914857u, 1807058540u, 1786244187u, 1748062722u, 1777547317u, 1655668488u, 1668093247u,
			1630251878u, 1625932113u, 1705433044u, 1684323811u, 1713505210u, 1742760333u, 1222101792u, 1226154263u, 1263738702u, 1251046777u,
			1339974652u, 1310460363u, 1281013650u, 1301863845u, 1187289752u, 1191637167u, 1161842422u, 1149379777u, 1103966788u, 1074747507u,
			1112139306u, 1133218845u, 1425107024u, 1429406311u, 1467333694u, 1454888457u, 1408811148u, 1379576507u, 1350309090u, 1371438805u,
			1524775400u, 1528845279u, 1499917702u, 1487177649u, 1575719220u, 1546255107u, 1584350554u, 1605185389u, 3778354048u, 3774312887u,
			3803303918u, 3816007129u, 3862737756u, 3892238699u, 3854067506u, 3833203973u, 4012263992u, 4007927823u, 3970080342u, 3982554209u,
			3895452388u, 3924658387u, 3953866378u, 3932773565u, 4245928176u, 4241609415u, 4271336606u, 4283762345u, 4196012076u, 4225268251u,
			4187931714u, 4166823541u, 4076923208u, 4072833919u, 4035198246u, 4047918865u, 4094247316u, 4123732899u, 4153251322u, 4132437965u,
			3648544096u, 3636082519u, 3673983246u, 3678331705u, 3732010428u, 3753090955u, 3723829714u, 3694611429u, 3614117080u, 3601426159u,
			3572488374u, 3576541825u, 3496125444u, 3516976691u, 3555094634u, 3525581405u, 3311336976u, 3298595879u, 3336186494u, 3340255305u,
			3260503756u, 3281337595u, 3251864226u, 3222399125u, 3410866088u, 3398419871u, 3368647622u, 3372945905u, 3427010420u, 3448139075u,
			3485520666u, 3456284973u, 2444203584u, 2423127159u, 2452308526u, 2481530905u, 2527477404u, 2539934891u, 2502093554u, 2497740997u,
			2679949304u, 2659102159u, 2620920726u, 2650438049u, 2562027300u, 2574714131u, 2603727690u, 2599670141u, 2374579504u, 2353749767u,
			2383274334u, 2412743529u, 2323684844u, 2336421851u, 2298759554u, 2294686645u, 2207933576u, 2186809023u, 2149495014u, 2178734801u,
			2224278612u, 2236720739u, 2266437690u, 2262135309u, 2850214048u, 2820717207u, 2858812622u, 2879680249u, 2934667388u, 2938704459u,
			2909776914u, 2897069605u, 2817622296u, 2788420399u, 2759153014u, 2780249921u, 2700618180u, 2704950259u, 2742877610u, 2730399645u,
			3049550800u, 3020298727u, 3057690558u, 3078802825u, 2999835404u, 3004150075u, 2974355298u, 2961925461u, 3151438440u, 3121956959u,
			3092510214u, 3113327665u, 3168701108u, 3172786307u, 3210370778u, 3197646061u
		};

		private static readonly uint[] s_crcTable_3 = new uint[256]
		{
			0u, 3099354981u, 2852767883u, 313896942u, 2405603159u, 937357362u, 627793884u, 2648127673u, 3316918511u, 2097696650u,
			1874714724u, 3607201537u, 1255587768u, 4067088605u, 3772741427u, 1482887254u, 1343838111u, 3903140090u, 4195393300u, 1118632049u,
			3749429448u, 1741137837u, 1970407491u, 3452858150u, 2511175536u, 756094997u, 1067759611u, 2266550430u, 449832999u, 2725482306u,
			2965774508u, 142231497u, 2687676222u, 412010587u, 171665333u, 2995192016u, 793786473u, 2548850444u, 2237264098u, 1038456711u,
			1703315409u, 3711623348u, 3482275674u, 1999841343u, 3940814982u, 1381529571u, 1089329165u, 4166106984u, 4029413537u, 1217896388u,
			1512189994u, 3802027855u, 2135519222u, 3354724499u, 3577784189u, 1845280792u, 899665998u, 2367928107u, 2677414085u, 657096608u,
			3137160985u, 37822588u, 284462994u, 2823350519u, 2601801789u, 598228824u, 824021174u, 2309093331u, 343330666u, 2898962447u,
			3195996129u, 113467524u, 1587572946u, 3860600759u, 4104763481u, 1276501820u, 3519211397u, 1769898208u, 2076913422u, 3279374443u,
			3406630818u, 1941006535u, 1627703081u, 3652755532u, 1148164341u, 4241751952u, 3999682686u, 1457141531u, 247015245u, 3053797416u,
			2763059142u, 470583459u, 2178658330u, 963106687u, 735213713u, 2473467892u, 992409347u, 2207944806u, 2435792776u, 697522413u,
			3024379988u, 217581361u, 508405983u, 2800865210u, 4271038444u, 1177467017u, 1419450215u, 3962007554u, 1911572667u, 3377213406u,
			3690561584u, 1665525589u, 1799331996u, 3548628985u, 3241568279u, 2039091058u, 3831314379u, 1558270126u, 1314193216u, 4142438437u,
			2928380019u, 372764438u, 75645176u, 3158189981u, 568925988u, 2572515393u, 2346768303u, 861712586u, 3982079547u, 1441124702u,
			1196457648u, 4293663189u, 1648042348u, 3666298377u, 3358779879u, 1888390786u, 686661332u, 2421291441u, 2196002399u, 978858298u,
			2811169155u, 523464422u, 226935048u, 3040519789u, 3175145892u, 100435649u, 390670639u, 2952089162u, 841119475u, 2325614998u,
			2553003640u, 546822429u, 2029308235u, 3225988654u, 3539796416u, 1782671013u, 4153826844u, 1328167289u, 1570739863u, 3844338162u,
			1298864389u, 4124540512u, 3882013070u, 1608431339u, 3255406162u, 2058742071u, 1744848601u, 3501990332u, 2296328682u, 811816591u,
			584513889u, 2590678532u, 129869501u, 3204563416u, 2914283062u, 352848211u, 494030490u, 2781751807u, 3078325777u, 264757620u,
			2450577869u, 715964072u, 941166918u, 2158327331u, 3636881013u, 1618608400u, 1926213374u, 3396585883u, 1470427426u, 4011365959u,
			4255988137u, 1158766284u, 1984818694u, 3471935843u, 3695453837u, 1693991400u, 4180638033u, 1100160564u, 1395044826u, 3952793279u,
			3019491049u, 189112716u, 435162722u, 2706139399u, 1016811966u, 2217162459u, 2526189877u, 774831696u, 643086745u, 2666061564u,
			2354934034u, 887166583u, 2838900430u, 294275499u, 54519365u, 3145957664u, 3823145334u, 1532818963u, 1240029693u, 4048895640u,
			1820460577u, 3560857924u, 3331051178u, 2117577167u, 3598663992u, 1858283101u, 2088143283u, 3301633750u, 1495127663u, 3785470218u,
			4078182116u, 1269332353u, 332098007u, 2876706482u, 3116540252u, 25085497u, 2628386432u, 605395429u, 916469259u, 2384220526u,
			2254837415u, 1054503362u, 745528876u, 2496903497u, 151290352u, 2981684885u, 2735556987u, 464596510u, 1137851976u, 4218313005u,
			3923506883u, 1365741990u, 3434129695u, 1946996346u, 1723425172u, 3724871409u
		};

		private static readonly uint[] s_crcTable_4 = new uint[256]
		{
			0u, 1029712304u, 2059424608u, 1201699536u, 4118849216u, 3370159984u, 2403399072u, 2988497936u, 812665793u, 219177585u,
			1253054625u, 2010132753u, 3320900865u, 4170237105u, 3207642721u, 2186319825u, 1625331586u, 1568718386u, 438355170u, 658566482u,
			2506109250u, 2818578674u, 4020265506u, 3535817618u, 1351670851u, 1844508147u, 709922595u, 389064339u, 2769320579u, 2557498163u,
			3754961379u, 3803185235u, 3250663172u, 4238411444u, 3137436772u, 2254525908u, 876710340u, 153198708u, 1317132964u, 1944187668u,
			4054934725u, 3436268917u, 2339452837u, 3054575125u, 70369797u, 961670069u, 2129760613u, 1133623509u, 2703341702u, 2621542710u,
			3689016294u, 3867263574u, 1419845190u, 1774270454u, 778128678u, 318858390u, 2438067015u, 2888948471u, 3952189479u, 3606153623u,
			1691440519u, 1504803895u, 504432359u, 594620247u, 1492342857u, 1704161785u, 573770537u, 525542041u, 2910060169u, 2417219385u,
			3618876905u, 3939730521u, 1753420680u, 1440954936u, 306397416u, 790849880u, 2634265928u, 2690882808u, 3888375336u, 3668168600u,
			940822475u, 91481723u, 1121164459u, 2142483739u, 3448989963u, 4042473659u, 3075684971u, 2318603227u, 140739594u, 889433530u,
			1923340138u, 1338244826u, 4259521226u, 3229813626u, 2267247018u, 3124975642u, 2570221389u, 2756861693u, 3824297005u, 3734113693u,
			1823658381u, 1372780605u, 376603373u, 722643805u, 2839690380u, 2485261628u, 3548540908u, 4007806556u, 1556257356u, 1638052860u,
			637716780u, 459464860u, 4191346895u, 3300051327u, 2199040943u, 3195181599u, 206718479u, 825388991u, 1989285231u, 1274166495u,
			3382881038u, 4106388158u, 3009607790u, 2382549470u, 1008864718u, 21111934u, 1189240494u, 2072147742u, 2984685714u, 2357631266u,
			3408323570u, 4131834434u, 1147541074u, 2030452706u, 1051084082u, 63335554u, 2174155603u, 3170292451u, 4216760371u, 3325460867u,
			1947622803u, 1232499747u, 248909555u, 867575619u, 3506841360u, 3966111392u, 2881909872u, 2527485376u, 612794832u, 434546784u,
			1581699760u, 1663499008u, 3782634705u, 3692447073u, 2612412337u, 2799048193u, 351717905u, 697754529u, 1849071985u, 1398190273u,
			1881644950u, 1296545318u, 182963446u, 931652934u, 2242328918u, 3100053734u, 4284967478u, 3255255942u, 1079497815u, 2100821479u,
			983009079u, 133672583u, 3050795671u, 2293717799u, 3474399735u, 4067887175u, 281479188u, 765927844u, 1778867060u, 1466397380u,
			3846680276u, 3626469220u, 2676489652u, 2733102084u, 548881365u, 500656741u, 1517752501u, 1729575173u, 3577210133u, 3898068133u,
			2952246901u, 2459410373u, 3910527195u, 3564487019u, 2480257979u, 2931134987u, 479546907u, 569730987u, 1716854139u, 1530213579u,
			3647316762u, 3825568426u, 2745561210u, 2663766474u, 753206746u, 293940330u, 1445287610u, 1799716618u, 2314567513u, 3029685993u,
			4080348217u, 3461678473u, 2088098201u, 1091956777u, 112560889u, 1003856713u, 3112514712u, 2229607720u, 3276105720u, 4263857736u,
			1275433560u, 1902492648u, 918929720u, 195422344u, 685033439u, 364179055u, 1377080511u, 1869921551u, 3713294623u, 3761522863u,
			2811507327u, 2599689167u, 413436958u, 633644462u, 1650777982u, 1594160846u, 3978570462u, 3494118254u, 2548332990u, 2860797966u,
			1211387997u, 1968470509u, 854852413u, 261368461u, 3182753437u, 2161434413u, 3346310653u, 4195650637u, 2017729436u, 1160000044u,
			42223868u, 1071931724u, 2378480988u, 2963576044u, 4144295484u, 3395602316u
		};

		private static readonly uint[] s_crcTable_5 = new uint[256]
		{
			0u, 3411858341u, 1304994059u, 2257875630u, 2609988118u, 1355649459u, 3596215069u, 486879416u, 3964895853u, 655315400u,
			2711298918u, 1791488195u, 2009251963u, 3164476382u, 973758832u, 4048990933u, 64357019u, 3364540734u, 1310630800u, 2235723829u,
			2554806413u, 1394316072u, 3582976390u, 517157411u, 4018503926u, 618222419u, 2722963965u, 1762783832u, 1947517664u, 3209171269u,
			970744811u, 4068520014u, 128714038u, 3438335635u, 1248109629u, 2167961496u, 2621261600u, 1466012805u, 3522553387u, 447296910u,
			3959392091u, 547575038u, 2788632144u, 1835791861u, 1886307661u, 3140622056u, 1034314822u, 4143626211u, 75106221u, 3475428360u,
			1236444838u, 2196665603u, 2682996155u, 1421317662u, 3525567664u, 427767573u, 3895035328u, 594892389u, 2782995659u, 1857943406u,
			1941489622u, 3101955187u, 1047553757u, 4113347960u, 257428076u, 3288652233u, 1116777319u, 2311878850u, 2496219258u, 1603640287u,
			3640781169u, 308099796u, 3809183745u, 676813732u, 2932025610u, 1704983215u, 2023410199u, 3016104370u, 894593820u, 4262377657u,
			210634999u, 3352484690u, 1095150076u, 2316991065u, 2535410401u, 1547934020u, 3671583722u, 294336591u, 3772615322u, 729897279u,
			2903845777u, 1716123700u, 2068629644u, 2953845545u, 914647431u, 4258839074u, 150212442u, 3282623743u, 1161604689u, 2388688372u,
			2472889676u, 1480171241u, 3735940167u, 368132066u, 3836185911u, 805002898u, 2842635324u, 1647574937u, 2134298401u, 3026852996u,
			855535146u, 4188192143u, 186781121u, 3229539940u, 1189784778u, 2377547631u, 2427670487u, 1542429810u, 3715886812u, 371670393u,
			3882979244u, 741170185u, 2864262823u, 1642462466u, 2095107514u, 3082559007u, 824732849u, 4201955092u, 514856152u, 3589064573u,
			1400419795u, 2552522358u, 2233554638u, 1316849003u, 3370776517u, 62202976u, 4075001525u, 968836368u, 3207280574u, 1954014235u,
			1769133219u, 2720925446u, 616199592u, 4024870413u, 493229635u, 3594175974u, 1353627464u, 2616354029u, 2264355925u, 1303087088u,
			3409966430u, 6498043u, 4046820398u, 979978123u, 3170710821u, 2007099008u, 1789187640u, 2717386141u, 661419827u, 3962610838u,
			421269998u, 3527459403u, 1423225061u, 2676515648u, 2190300152u, 1238466653u, 3477467891u, 68755798u, 4115633027u, 1041448998u,
			3095868040u, 1943789869u, 1860096405u, 2776760880u, 588673182u, 3897205563u, 449450869u, 3516317904u, 1459794558u, 2623431131u,
			2170245475u, 1242006214u, 3432247400u, 131015629u, 4137259288u, 1036337853u, 3142660115u, 1879958454u, 1829294862u, 2790523051u,
			549483013u, 3952910752u, 300424884u, 3669282065u, 1545650111u, 2541513754u, 2323209378u, 1092980487u, 3350330793u, 216870412u,
			4256931033u, 921128828u, 2960342482u, 2066738807u, 1714085583u, 2910195050u, 736264132u, 3770592353u, 306060335u, 3647131530u,
			1610005796u, 2494197377u, 2309971513u, 1123257756u, 3295149874u, 255536279u, 4268596802u, 892423655u, 3013951305u, 2029645036u,
			1711070292u, 2929725425u, 674528607u, 3815288570u, 373562242u, 3709388839u, 1535949449u, 2429577516u, 2379569556u, 1183418929u,
			3223189663u, 188820282u, 4195850735u, 827017802u, 3084859620u, 2089020225u, 1636228089u, 2866415708u, 743340786u, 3876759895u,
			361896217u, 3738094268u, 1482340370u, 2466671543u, 2382584591u, 1163888810u, 3284924932u, 144124321u, 4190215028u, 849168593u,
			3020503679u, 2136336858u, 1649465698u, 2836138695u, 798521449u, 3838094284u
		};

		private static readonly uint[] s_crcTable_6 = new uint[256]
		{
			0u, 2792819636u, 2543784233u, 837294749u, 4098827283u, 1379413927u, 1674589498u, 3316072078u, 871321191u, 2509784531u,
			2758827854u, 34034938u, 3349178996u, 1641505216u, 1346337629u, 4131942633u, 1742642382u, 3249117050u, 4030828007u, 1446413907u,
			2475800797u, 904311657u, 68069876u, 2725880384u, 1412551337u, 4064729373u, 3283010432u, 1708771380u, 2692675258u, 101317902u,
			937551763u, 2442587175u, 3485284764u, 1774858792u, 1478633653u, 4266992385u, 1005723023u, 2642744891u, 2892827814u, 169477906u,
			4233263099u, 1512406095u, 1808623314u, 3451546982u, 136139752u, 2926205020u, 2676114113u, 972376437u, 2825102674u, 236236518u,
			1073525883u, 2576072655u, 1546420545u, 4200303349u, 3417542760u, 1841601500u, 2609703733u, 1039917185u, 202635804u, 2858742184u,
			1875103526u, 3384067218u, 4166835727u, 1579931067u, 1141601657u, 3799809741u, 3549717584u, 1977839588u, 2957267306u, 372464350u,
			668680259u, 2175552503u, 2011446046u, 3516084394u, 3766168119u, 1175200131u, 2209029901u, 635180217u, 338955812u, 2990736784u,
			601221559u, 2242044419u, 3024812190u, 306049834u, 3617246628u, 1911408144u, 1074125965u, 3866285881u, 272279504u, 3058543716u,
			2275784441u, 567459149u, 3832906691u, 1107462263u, 1944752874u, 3583875422u, 2343980261u, 767641425u, 472473036u, 3126744696u,
			2147051766u, 3649987394u, 3899029983u, 1309766251u, 3092841090u, 506333494u, 801510315u, 2310084639u, 1276520081u, 3932237093u,
			3683203000u, 2113813516u, 3966292011u, 1243601823u, 2079834370u, 3716205238u, 405271608u, 3192979340u, 2411259153u, 701492901u,
			3750207052u, 2045810168u, 1209569125u, 4000285905u, 734575199u, 2378150379u, 3159862134u, 438345922u, 2283203314u, 778166598u,
			529136603u, 3120492655u, 2086260449u, 3660498261u, 3955679176u, 1303499900u, 3153699989u, 495890209u, 744928700u, 2316418568u,
			1337360518u, 3921775410u, 3626602927u, 2120129051u, 4022892092u, 1237286280u, 2018993941u, 3726666913u, 461853231u, 3186645403u,
			2350400262u, 711936178u, 3693557851u, 2052076527u, 1270360434u, 3989775046u, 677911624u, 2384402428u, 3220639073u, 427820757u,
			1202443118u, 3789347034u, 3493118535u, 1984154099u, 3018127229u, 362020041u, 612099668u, 2181885408u, 1950653705u, 3526596285u,
			3822816288u, 1168934804u, 2148251930u, 645706414u, 395618355u, 2984485767u, 544559008u, 2248295444u, 3085590153u, 295523645u,
			3560598451u, 1917673479u, 1134918298u, 3855773998u, 328860103u, 3052210803u, 2214924526u, 577903450u, 3889505748u, 1101147744u,
			1883911421u, 3594338121u, 3424493451u, 1785369663u, 1535282850u, 4260726038u, 944946072u, 2653270060u, 2949491377u, 163225861u,
			4294103532u, 1501944408u, 1752023237u, 3457862513u, 196998655u, 2915761739u, 2619532502u, 978710370u, 2881684293u, 229902577u,
			1012666988u, 2586515928u, 1603020630u, 4193987810u, 3356702335u, 1852063179u, 2553040162u, 1046169238u, 263412747u, 2848217023u,
			1818454321u, 3390333573u, 4227627032u, 1569420204u, 60859927u, 2782375331u, 2487203646u, 843627658u, 4159668740u, 1368951216u,
			1617990445u, 3322386585u, 810543216u, 2520310724u, 2815490393u, 27783917u, 3288386659u, 1652017111u, 1402985802u, 4125677310u,
			1685994201u, 3255382381u, 4091620336u, 1435902020u, 2419138250u, 910562686u, 128847843u, 2715354199u, 1469150398u, 4058414858u,
			3222168983u, 1719234083u, 2749255853u, 94984985u, 876691844u, 2453031472u
		};

		private static readonly uint[] s_crcTable_7 = new uint[256]
		{
			0u, 3433693342u, 1109723005u, 2391738339u, 2219446010u, 1222643300u, 3329165703u, 180685081u, 3555007413u, 525277995u,
			2445286600u, 1567235158u, 1471092047u, 2600801745u, 361370162u, 3642757804u, 2092642603u, 2953916853u, 1050555990u, 4063508168u,
			4176560081u, 878395215u, 3134470316u, 1987983410u, 2942184094u, 1676945920u, 3984272867u, 567356797u, 722740324u, 3887998202u,
			1764827929u, 2778407815u, 4185285206u, 903635656u, 3142804779u, 2012833205u, 2101111980u, 2979425330u, 1058630609u, 4088621903u,
			714308067u, 3862526333u, 1756790430u, 2753330688u, 2933487385u, 1651734407u, 3975966820u, 542535930u, 2244825981u, 1231508451u,
			3353891840u, 188896414u, 25648519u, 3442302233u, 1134713594u, 2399689316u, 1445480648u, 2592229462u, 336416693u, 3634843435u,
			3529655858u, 516441772u, 2420588879u, 1559052753u, 698204909u, 3845636723u, 1807271312u, 2803025166u, 2916600855u, 1635634313u,
			4025666410u, 593021940u, 4202223960u, 919787974u, 3093159461u, 1962401467u, 2117261218u, 2996361020u, 1008193759u, 4038971457u,
			1428616134u, 2576151384u, 386135227u, 3685348389u, 3513580860u, 499580322u, 2471098945u, 1608776415u, 2260985971u, 1248454893u,
			3303468814u, 139259792u, 42591881u, 3458459159u, 1085071860u, 2349261162u, 3505103035u, 474062885u, 2463016902u, 1583654744u,
			1419882049u, 2550902495u, 377792828u, 3660491170u, 51297038u, 3483679632u, 1093385331u, 2374089965u, 2269427188u, 1273935210u,
			3311514249u, 164344343u, 2890961296u, 1627033870u, 4000683757u, 585078387u, 672833386u, 3836780532u, 1782552599u, 2794821769u,
			2142603813u, 3005188795u, 1032883544u, 4047146438u, 4227826911u, 928351297u, 3118105506u, 1970307900u, 1396409818u, 2677114180u,
			287212199u, 3719594553u, 3614542624u, 467372990u, 2505346141u, 1509854403u, 2162073199u, 1282711281u, 3271268626u, 240228748u,
			76845205u, 3359543307u, 1186043880u, 2317064054u, 796964081u, 3811226735u, 1839575948u, 2702160658u, 2882189835u, 1734392469u,
			3924802934u, 625327592u, 4234522436u, 818917338u, 3191908409u, 1927981223u, 2016387518u, 3028656416u, 973776579u, 4137723485u,
			2857232268u, 1726474002u, 3899187441u, 616751215u, 772270454u, 3803048424u, 1814228491u, 2693328533u, 2041117753u, 3036871847u,
			999160644u, 4146592730u, 4259508931u, 826864221u, 3217552830u, 1936586016u, 3606501031u, 442291769u, 2496909786u, 1484378436u,
			1388107869u, 2652297411u, 278519584u, 3694387134u, 85183762u, 3384397196u, 1194773103u, 2342308593u, 2170143720u, 1307820918u,
			3279733909u, 265733131u, 2057717559u, 3054258089u, 948125770u, 4096344276u, 4276898253u, 843467091u, 3167309488u, 1885556270u,
			2839764098u, 1709792284u, 3949353983u, 667704161u, 755585656u, 3785577190u, 1865176325u, 2743489947u, 102594076u, 3401021058u,
			1144549729u, 2291298815u, 2186770662u, 1325234296u, 3228729243u, 215514885u, 3589828009u, 424832311u, 2547870420u, 1534552650u,
			1370645331u, 2635621325u, 328688686u, 3745342640u, 2211456353u, 1333405183u, 3254067740u, 224338562u, 127544219u, 3408931589u,
			1170156774u, 2299866232u, 1345666772u, 2627681866u, 303053225u, 3736746295u, 3565105198u, 416624816u, 2522494803u, 1525692365u,
			4285207626u, 868291796u, 3176010551u, 1910772649u, 2065767088u, 3079346734u, 956571085u, 4121828691u, 747507711u, 3760459617u,
			1856702594u, 2717976604u, 2831417605u, 1684930971u, 3940615800u, 642451174u
		};

		public static uint UpdateCrc32(uint crc32, byte[] buffer, int offset, int length)
		{
			return ManagedCrc32(crc32, buffer, offset, length);
		}

		private static uint ManagedCrc32(uint crc32, byte[] buffer, int offset, int length)
		{
			uint num = 0u;
			crc32 ^= 0xFFFFFFFFu;
			int num2 = length / 8 * 8;
			int num3 = length - num2;
			for (int i = 0; i < num2 / 8; i++)
			{
				crc32 ^= (uint)(buffer[offset] | (buffer[offset + 1] << 8) | (buffer[offset + 2] << 16) | (buffer[offset + 3] << 24));
				offset += 4;
				uint num4 = s_crcTable_7[crc32 & 0xFF] ^ s_crcTable_6[(crc32 >> 8) & 0xFF];
				uint num5 = crc32 >> 16;
				crc32 = num4 ^ s_crcTable_5[num5 & 0xFF] ^ s_crcTable_4[(num5 >> 8) & 0xFF];
				num = (uint)(buffer[offset] | (buffer[offset + 1] << 8) | (buffer[offset + 2] << 16) | (buffer[offset + 3] << 24));
				offset += 4;
				num4 = s_crcTable_3[num & 0xFF] ^ s_crcTable_2[(num >> 8) & 0xFF];
				num5 = num >> 16;
				crc32 ^= num4 ^ s_crcTable_1[num5 & 0xFF] ^ s_crcTable_0[(num5 >> 8) & 0xFF];
			}
			for (int j = 0; j < num3; j++)
			{
				crc32 = s_crcTable_0[(crc32 ^ buffer[offset++]) & 0xFF] ^ (crc32 >> 8);
			}
			crc32 ^= 0xFFFFFFFFu;
			return crc32;
		}
	}
}
namespace System.Threading.Tasks
{
	internal static class TaskToApm
	{
		private sealed class TaskWrapperAsyncResult : IAsyncResult
		{
			internal readonly Task Task;

			private readonly object _state;

			private readonly bool _completedSynchronously;

			object IAsyncResult.AsyncState => _state;

			bool IAsyncResult.CompletedSynchronously => _completedSynchronously;

			bool IAsyncResult.IsCompleted => Task.IsCompleted;

			WaitHandle IAsyncResult.AsyncWaitHandle => ((IAsyncResult)Task).AsyncWaitHandle;

			internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
			{
				Task = task;
				_state = state;
				_completedSynchronously = completedSynchronously;
			}
		}

		public static IAsyncResult Begin(Task task, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			if (task.IsCompleted)
			{
				asyncResult = new TaskWrapperAsyncResult(task, state, completedSynchronously: true);
				callback?.Invoke(asyncResult);
			}
			else
			{
				IAsyncResult asyncResult3;
				if (task.AsyncState != state)
				{
					IAsyncResult asyncResult2 = new TaskWrapperAsyncResult(task, state, completedSynchronously: false);
					asyncResult3 = asyncResult2;
				}
				else
				{
					IAsyncResult asyncResult2 = task;
					asyncResult3 = asyncResult2;
				}
				asyncResult = asyncResult3;
				if (callback != null)
				{
					InvokeCallbackWhenTaskCompletes(task, callback, asyncResult);
				}
			}
			return asyncResult;
		}

		public static void End(IAsyncResult asyncResult)
		{
			Task task = ((!(asyncResult is TaskWrapperAsyncResult taskWrapperAsyncResult)) ? (asyncResult as Task) : taskWrapperAsyncResult.Task);
			if (task == null)
			{
				throw new ArgumentNullException();
			}
			task.GetAwaiter().GetResult();
		}

		public static TResult End<TResult>(IAsyncResult asyncResult)
		{
			Task<TResult> task = ((!(asyncResult is TaskWrapperAsyncResult taskWrapperAsyncResult)) ? (asyncResult as Task<TResult>) : (taskWrapperAsyncResult.Task as Task<TResult>));
			if (task == null)
			{
				throw new ArgumentNullException();
			}
			return task.GetAwaiter().GetResult();
		}

		private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
		{
			antecedent.ConfigureAwait(continueOnCapturedContext: false).GetAwaiter().OnCompleted(delegate
			{
				callback(asyncResult);
			});
		}
	}
}
namespace Microsoft.Win32.SafeHandles
{
	internal sealed class SafeBrotliEncoderHandle : SafeHandle
	{
		public override bool IsInvalid => handle == IntPtr.Zero;

		public SafeBrotliEncoderHandle()
			: base(IntPtr.Zero, ownsHandle: true)
		{
		}

		protected override bool ReleaseHandle()
		{
			global::Interop.Brotli.BrotliEncoderDestroyInstance(handle);
			return true;
		}
	}
	internal sealed class SafeBrotliDecoderHandle : SafeHandle
	{
		public override bool IsInvalid => handle == IntPtr.Zero;

		public SafeBrotliDecoderHandle()
			: base(IntPtr.Zero, ownsHandle: true)
		{
		}

		protected override bool ReleaseHandle()
		{
			global::Interop.Brotli.BrotliDecoderDestroyInstance(handle);
			return true;
		}
	}
}
namespace Unity
{
	internal sealed class ThrowStub : ObjectDisposedException
	{
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
