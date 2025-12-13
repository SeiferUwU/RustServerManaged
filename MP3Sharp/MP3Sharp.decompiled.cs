using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Text;
using MP3Sharp.Decoding;
using MP3Sharp.Decoding.Decoders;
using MP3Sharp.Decoding.Decoders.LayerI;
using MP3Sharp.Decoding.Decoders.LayerII;
using MP3Sharp.Decoding.Decoders.LayerIII;
using MP3Sharp.Support;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("MP3Sharp")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("MP3Sharp")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: Guid("2c379a78-8a96-48a5-81e5-ea0ae696aa5b")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: TargetFramework(".NETFramework,Version=v4.0,Profile=Client", FrameworkDisplayName = ".NET Framework 4 Client Profile")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace MP3Sharp
{
	public class Buffer16BitStereo : ABuffer
	{
		internal bool DoubleMonoToStereo;

		private const int OUTPUT_CHANNELS = 2;

		private readonly byte[] _Buffer = new byte[4608];

		private readonly int[] _BufferChannelOffsets = new int[2];

		private int _End;

		private int _Offset;

		internal int BytesLeft => _End - _Offset;

		internal Buffer16BitStereo()
		{
			ClearBuffer();
		}

		internal int Read(byte[] bufferOut, int offset, int count)
		{
			if (bufferOut == null)
			{
				throw new ArgumentNullException("bufferOut");
			}
			if (count + offset > bufferOut.Length)
			{
				throw new ArgumentException("The sum of offset and count is larger than the buffer length");
			}
			int bytesLeft = BytesLeft;
			int num;
			if (count > bytesLeft)
			{
				num = bytesLeft;
			}
			else
			{
				int num2 = count % 4;
				num = count - num2;
			}
			Array.Copy(_Buffer, _Offset, bufferOut, offset, num);
			_Offset += num;
			return num;
		}

		protected override void Append(int channel, short sampleValue)
		{
			_Buffer[_BufferChannelOffsets[channel]] = (byte)(sampleValue & 0xFF);
			_Buffer[_BufferChannelOffsets[channel] + 1] = (byte)(sampleValue >> 8);
			_BufferChannelOffsets[channel] += 4;
		}

		internal override void AppendSamples(int channel, float[] samples)
		{
			if (samples == null)
			{
				throw new ArgumentNullException("samples");
			}
			if (samples.Length < 32)
			{
				throw new ArgumentException("samples must have 32 values");
			}
			if (_BufferChannelOffsets == null || channel >= _BufferChannelOffsets.Length)
			{
				throw new Exception("Song is closing...");
			}
			int num = _BufferChannelOffsets[channel];
			for (int i = 0; i < 32; i++)
			{
				float num2 = samples[i];
				if (num2 > 32767f)
				{
					num2 = 32767f;
				}
				else if (num2 < -32767f)
				{
					num2 = -32767f;
				}
				int num3 = (int)num2;
				_Buffer[num] = (byte)(num3 & 0xFF);
				_Buffer[num + 1] = (byte)(num3 >> 8);
				if (DoubleMonoToStereo)
				{
					_Buffer[num + 2] = (byte)(num3 & 0xFF);
					_Buffer[num + 3] = (byte)(num3 >> 8);
				}
				num += 4;
			}
			_BufferChannelOffsets[channel] = num;
		}

		internal sealed override void ClearBuffer()
		{
			_Offset = 0;
			_End = 0;
			for (int i = 0; i < 2; i++)
			{
				_BufferChannelOffsets[i] = i * 2;
			}
		}

		internal override void SetStopFlag()
		{
		}

		internal override void WriteBuffer(int val)
		{
			_Offset = 0;
			_End = _BufferChannelOffsets[0];
		}

		internal override void Close()
		{
		}
	}
	[Serializable]
	public class MP3SharpException : Exception
	{
		internal MP3SharpException()
		{
		}

		internal MP3SharpException(string message)
			: base(message)
		{
		}

		internal MP3SharpException(string message, Exception inner)
			: base(message, inner)
		{
		}

		protected MP3SharpException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		internal void PrintStackTrace()
		{
			SupportClass.WriteStackTrace(this, Console.Error);
		}

		internal void PrintStackTrace(StreamWriter ps)
		{
			if (base.InnerException == null)
			{
				SupportClass.WriteStackTrace(this, ps);
			}
			else
			{
				SupportClass.WriteStackTrace(base.InnerException, Console.Error);
			}
		}
	}
	public class MP3Stream : Stream
	{
		private readonly Bitstream _BitStream;

		private readonly MP3Sharp.Decoding.Decoder _Decoder = new MP3Sharp.Decoding.Decoder(MP3Sharp.Decoding.Decoder.DefaultParams);

		private readonly Buffer16BitStereo _Buffer;

		private readonly Stream _SourceStream;

		private const int BACK_STREAM_BYTE_COUNT_REP = 0;

		private short _ChannelCountRep = -1;

		private readonly SoundFormat FormatRep;

		private int _FrequencyRep = -1;

		public bool IsEOF { get; protected set; }

		internal int ChunkSize => 0;

		public override bool CanRead => _SourceStream.CanRead;

		public override bool CanSeek => _SourceStream.CanSeek;

		public override bool CanWrite => _SourceStream.CanWrite;

		public override long Length => _SourceStream.Length;

		public override long Position
		{
			get
			{
				return _SourceStream.Position;
			}
			set
			{
				if (value < 0)
				{
					value = 0L;
				}
				if (value > _SourceStream.Length)
				{
					value = _SourceStream.Length;
				}
				_SourceStream.Position = value;
				IsEOF = false;
				IsEOF |= !ReadFrame();
			}
		}

		public int Frequency => _FrequencyRep;

		internal short ChannelCount => _ChannelCountRep;

		internal SoundFormat Format => FormatRep;

		public MP3Stream(string fileName)
			: this(new FileStream(fileName, FileMode.Open, FileAccess.Read))
		{
		}

		public MP3Stream(string fileName, int chunkSize)
			: this(new FileStream(fileName, FileMode.Open, FileAccess.Read), chunkSize)
		{
		}

		public MP3Stream(Stream sourceStream)
			: this(sourceStream, 4096)
		{
		}

		public MP3Stream(Stream sourceStream, int chunkSize)
		{
			IsEOF = false;
			_SourceStream = sourceStream;
			_BitStream = new Bitstream(new PushbackStream(_SourceStream, chunkSize));
			_Buffer = new Buffer16BitStereo();
			_Decoder.OutputBuffer = _Buffer;
			IsEOF |= !ReadFrame();
			switch (_ChannelCountRep)
			{
			case 1:
				FormatRep = SoundFormat.Pcm16BitMono;
				break;
			case 2:
				FormatRep = SoundFormat.Pcm16BitStereo;
				break;
			default:
				throw new MP3SharpException($"Unhandled channel count rep: {_ChannelCountRep} (allowed values are 1-mono and 2-stereo).");
			}
			if (FormatRep == SoundFormat.Pcm16BitMono)
			{
				_Buffer.DoubleMonoToStereo = true;
			}
		}

		public override void Flush()
		{
			_SourceStream.Flush();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return _SourceStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			throw new InvalidOperationException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException();
		}

		internal int DecodeFrames(int frameCount)
		{
			int num = 0;
			bool flag = true;
			while (num < frameCount && flag)
			{
				flag = ReadFrame();
				if (flag)
				{
					num++;
				}
			}
			return num;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (IsEOF)
			{
				return 0;
			}
			int num = 0;
			do
			{
				if (_Buffer.BytesLeft <= 0 && !ReadFrame())
				{
					IsEOF = true;
					_BitStream.CloseFrame();
					break;
				}
				num += _Buffer.Read(buffer, offset + num, count - num);
			}
			while (num < count);
			return num;
		}

		public override void Close()
		{
			_BitStream.Close();
		}

		private bool ReadFrame()
		{
			Header header = _BitStream.ReadFrame();
			if (header == null)
			{
				return false;
			}
			try
			{
				if (header.Mode() == 3)
				{
					_ChannelCountRep = 1;
				}
				else
				{
					_ChannelCountRep = 2;
				}
				_FrequencyRep = header.Frequency();
				if (_Decoder.DecodeFrame(header, _BitStream) != _Buffer)
				{
					throw new ApplicationException("Output buffers are different.");
				}
			}
			finally
			{
				_BitStream.CloseFrame();
			}
			return true;
		}
	}
	public enum SoundFormat
	{
		Pcm16BitMono,
		Pcm16BitStereo
	}
}
namespace MP3Sharp.Support
{
	public class SupportClass
	{
		internal static int URShift(int number, int bits)
		{
			if (number >= 0)
			{
				return number >> bits;
			}
			return (number >> bits) + (2 << ~bits);
		}

		internal static int URShift(int number, long bits)
		{
			return URShift(number, (int)bits);
		}

		internal static long URShift(long number, int bits)
		{
			if (number >= 0)
			{
				return number >> bits;
			}
			return (number >> bits) + (2L << ~bits);
		}

		internal static long URShift(long number, long bits)
		{
			return URShift(number, (int)bits);
		}

		internal static void WriteStackTrace(Exception throwable, TextWriter stream)
		{
			stream.Write(throwable.StackTrace);
			stream.Flush();
		}

		internal static long Identity(long literal)
		{
			return literal;
		}

		internal static ulong Identity(ulong literal)
		{
			return literal;
		}

		internal static float Identity(float literal)
		{
			return literal;
		}

		internal static double Identity(double literal)
		{
			return literal;
		}

		internal static int ReadInput(Stream sourceStream, ref sbyte[] target, int start, int count)
		{
			byte[] array = new byte[target.Length];
			int num = sourceStream.Read(array, start, count);
			for (int i = start; i < start + num; i++)
			{
				target[i] = (sbyte)array[i];
			}
			return num;
		}

		internal static byte[] ToByteArray(sbyte[] sbyteArray)
		{
			byte[] array = new byte[sbyteArray.Length];
			for (int i = 0; i < sbyteArray.Length; i++)
			{
				array[i] = (byte)sbyteArray[i];
			}
			return array;
		}

		internal static byte[] ToByteArray(string sourceString)
		{
			byte[] array = new byte[sourceString.Length];
			for (int i = 0; i < sourceString.Length; i++)
			{
				array[i] = (byte)sourceString[i];
			}
			return array;
		}

		internal static void GetSBytesFromString(string sourceString, int sourceStart, int sourceEnd, ref sbyte[] destinationArray, int destinationStart)
		{
			int num = sourceStart;
			int num2 = destinationStart;
			while (num < sourceEnd)
			{
				destinationArray[num2] = (sbyte)sourceString[num];
				num++;
				num2++;
			}
		}
	}
}
namespace MP3Sharp.IO
{
	public class RandomAccessFileStream
	{
		internal static FileStream CreateRandomAccessFile(string fileName, string mode)
		{
			if (string.Compare(mode, "rw", StringComparison.Ordinal) == 0)
			{
				return new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			}
			if (string.Compare(mode, "r", StringComparison.Ordinal) == 0)
			{
				return new FileStream(fileName, FileMode.Open, FileAccess.Read);
			}
			throw new ArgumentException();
		}
	}
	public class RiffFile
	{
		public class RiffChunkHeader
		{
			internal int CkId;

			internal int CkSize;

			private RiffFile _EnclosingInstance;

			internal RiffFile EnclosingInstance => _EnclosingInstance;

			internal RiffChunkHeader(RiffFile enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}

			private void InitBlock(RiffFile enclosingInstance)
			{
				_EnclosingInstance = enclosingInstance;
			}
		}

		protected const int DDC_SUCCESS = 0;

		protected const int DDC_FAILURE = 1;

		protected const int DDC_OUT_OF_MEMORY = 2;

		protected const int DDC_FILE_ERROR = 3;

		protected const int DDC_INVALID_CALL = 4;

		protected const int DDC_USER_ABORT = 5;

		protected const int DDC_INVALID_FILE = 6;

		protected const int RF_UNKNOWN = 0;

		protected const int RF_WRITE = 1;

		protected const int RF_READ = 2;

		private readonly RiffChunkHeader _RiffHeader;

		protected int Fmode;

		private Stream _File;

		internal RiffFile()
		{
			_File = null;
			Fmode = 0;
			_RiffHeader = new RiffChunkHeader(this);
			_RiffHeader.CkId = FourCC("RIFF");
			_RiffHeader.CkSize = 0;
		}

		internal int CurrentFileMode()
		{
			return Fmode;
		}

		internal virtual int Open(string filename, int newMode)
		{
			int num = 0;
			if (Fmode != 0)
			{
				num = Close();
			}
			if (num == 0)
			{
				switch (newMode)
				{
				case 1:
					try
					{
						_File = RandomAccessFileStream.CreateRandomAccessFile(filename, "rw");
						try
						{
							sbyte[] array = new sbyte[8]
							{
								(sbyte)(SupportClass.URShift(_RiffHeader.CkId, 24) & 0xFF),
								(sbyte)(SupportClass.URShift(_RiffHeader.CkId, 16) & 0xFF),
								(sbyte)(SupportClass.URShift(_RiffHeader.CkId, 8) & 0xFF),
								(sbyte)(_RiffHeader.CkId & 0xFF),
								0,
								0,
								0,
								0
							};
							sbyte b = (sbyte)(SupportClass.URShift(_RiffHeader.CkSize, 24) & 0xFF);
							sbyte b2 = (sbyte)(SupportClass.URShift(_RiffHeader.CkSize, 16) & 0xFF);
							sbyte b3 = (sbyte)(SupportClass.URShift(_RiffHeader.CkSize, 8) & 0xFF);
							sbyte b4 = (sbyte)(_RiffHeader.CkSize & 0xFF);
							array[4] = b4;
							array[5] = b3;
							array[6] = b2;
							array[7] = b;
							_File.Write(SupportClass.ToByteArray(array), 0, 8);
							Fmode = 1;
						}
						catch
						{
							_File.Close();
							Fmode = 0;
						}
					}
					catch
					{
						Fmode = 0;
						num = 3;
					}
					break;
				case 2:
					try
					{
						_File = RandomAccessFileStream.CreateRandomAccessFile(filename, "r");
						try
						{
							sbyte[] target = new sbyte[8];
							SupportClass.ReadInput(_File, ref target, 0, 8);
							Fmode = 2;
							_RiffHeader.CkId = ((target[0] << 24) & (int)SupportClass.Identity(4278190080L)) | ((target[1] << 16) & 0xFF0000) | ((target[2] << 8) & 0xFF00) | (target[3] & 0xFF);
							_RiffHeader.CkSize = ((target[4] << 24) & (int)SupportClass.Identity(4278190080L)) | ((target[5] << 16) & 0xFF0000) | ((target[6] << 8) & 0xFF00) | (target[7] & 0xFF);
						}
						catch
						{
							_File.Close();
							Fmode = 0;
						}
					}
					catch
					{
						Fmode = 0;
						num = 3;
					}
					break;
				default:
					num = 4;
					break;
				}
			}
			return num;
		}

		internal virtual int Open(Stream stream, int newMode)
		{
			int num = 0;
			if (Fmode != 0)
			{
				num = Close();
			}
			if (num == 0)
			{
				switch (newMode)
				{
				case 1:
					try
					{
						_File = stream;
						try
						{
							sbyte[] array = new sbyte[8]
							{
								(sbyte)(SupportClass.URShift(_RiffHeader.CkId, 24) & 0xFF),
								(sbyte)(SupportClass.URShift(_RiffHeader.CkId, 16) & 0xFF),
								(sbyte)(SupportClass.URShift(_RiffHeader.CkId, 8) & 0xFF),
								(sbyte)(_RiffHeader.CkId & 0xFF),
								0,
								0,
								0,
								0
							};
							sbyte b = (sbyte)(SupportClass.URShift(_RiffHeader.CkSize, 24) & 0xFF);
							sbyte b2 = (sbyte)(SupportClass.URShift(_RiffHeader.CkSize, 16) & 0xFF);
							sbyte b3 = (sbyte)(SupportClass.URShift(_RiffHeader.CkSize, 8) & 0xFF);
							sbyte b4 = (sbyte)(_RiffHeader.CkSize & 0xFF);
							array[4] = b4;
							array[5] = b3;
							array[6] = b2;
							array[7] = b;
							_File.Write(SupportClass.ToByteArray(array), 0, 8);
							Fmode = 1;
						}
						catch
						{
							_File.Close();
							Fmode = 0;
						}
					}
					catch
					{
						Fmode = 0;
						num = 3;
					}
					break;
				case 2:
					try
					{
						_File = stream;
						try
						{
							sbyte[] target = new sbyte[8];
							SupportClass.ReadInput(_File, ref target, 0, 8);
							Fmode = 2;
							_RiffHeader.CkId = ((target[0] << 24) & (int)SupportClass.Identity(4278190080L)) | ((target[1] << 16) & 0xFF0000) | ((target[2] << 8) & 0xFF00) | (target[3] & 0xFF);
							_RiffHeader.CkSize = ((target[4] << 24) & (int)SupportClass.Identity(4278190080L)) | ((target[5] << 16) & 0xFF0000) | ((target[6] << 8) & 0xFF00) | (target[7] & 0xFF);
						}
						catch
						{
							_File.Close();
							Fmode = 0;
						}
					}
					catch
					{
						Fmode = 0;
						num = 3;
					}
					break;
				default:
					num = 4;
					break;
				}
			}
			return num;
		}

		internal virtual int Write(sbyte[] data, int numBytes)
		{
			if (Fmode != 1)
			{
				return 4;
			}
			try
			{
				_File.Write(SupportClass.ToByteArray(data), 0, numBytes);
				Fmode = 1;
			}
			catch
			{
				return 3;
			}
			_RiffHeader.CkSize += numBytes;
			return 0;
		}

		internal virtual int Write(short[] data, int numBytes)
		{
			sbyte[] array = new sbyte[numBytes];
			int num = 0;
			for (int i = 0; i < numBytes; i += 2)
			{
				array[i] = (sbyte)(data[num] & 0xFF);
				array[i + 1] = (sbyte)(SupportClass.URShift(data[num++], 8) & 0xFF);
			}
			if (Fmode != 1)
			{
				return 4;
			}
			try
			{
				_File.Write(SupportClass.ToByteArray(array), 0, numBytes);
				Fmode = 1;
			}
			catch
			{
				return 3;
			}
			_RiffHeader.CkSize += numBytes;
			return 0;
		}

		internal virtual int Write(RiffChunkHeader riffHeader, int numBytes)
		{
			sbyte[] array = new sbyte[8]
			{
				(sbyte)(SupportClass.URShift(riffHeader.CkId, 24) & 0xFF),
				(sbyte)(SupportClass.URShift(riffHeader.CkId, 16) & 0xFF),
				(sbyte)(SupportClass.URShift(riffHeader.CkId, 8) & 0xFF),
				(sbyte)(riffHeader.CkId & 0xFF),
				0,
				0,
				0,
				0
			};
			sbyte b = (sbyte)(SupportClass.URShift(riffHeader.CkSize, 24) & 0xFF);
			sbyte b2 = (sbyte)(SupportClass.URShift(riffHeader.CkSize, 16) & 0xFF);
			sbyte b3 = (sbyte)(SupportClass.URShift(riffHeader.CkSize, 8) & 0xFF);
			sbyte b4 = (sbyte)(riffHeader.CkSize & 0xFF);
			array[4] = b4;
			array[5] = b3;
			array[6] = b2;
			array[7] = b;
			if (Fmode != 1)
			{
				return 4;
			}
			try
			{
				_File.Write(SupportClass.ToByteArray(array), 0, numBytes);
				Fmode = 1;
			}
			catch
			{
				return 3;
			}
			_RiffHeader.CkSize += numBytes;
			return 0;
		}

		internal virtual int Write(short data, int numBytes)
		{
			short value = data;
			if (Fmode != 1)
			{
				return 4;
			}
			try
			{
				new BinaryWriter(_File).Write(value);
				Fmode = 1;
			}
			catch
			{
				return 3;
			}
			_RiffHeader.CkSize += numBytes;
			return 0;
		}

		internal virtual int Write(int data, int numBytes)
		{
			if (Fmode != 1)
			{
				return 4;
			}
			try
			{
				new BinaryWriter(_File).Write(data);
				Fmode = 1;
			}
			catch
			{
				return 3;
			}
			_RiffHeader.CkSize += numBytes;
			return 0;
		}

		internal virtual int Read(sbyte[] data, int numBytes)
		{
			int result = 0;
			try
			{
				SupportClass.ReadInput(_File, ref data, 0, numBytes);
			}
			catch
			{
				result = 3;
			}
			return result;
		}

		internal virtual int Expect(string data, int numBytes)
		{
			int num = 0;
			try
			{
				while (numBytes-- != 0)
				{
					if ((sbyte)_File.ReadByte() != data[num++])
					{
						return 3;
					}
				}
			}
			catch
			{
				return 3;
			}
			return 0;
		}

		internal virtual int Close()
		{
			int result = 0;
			switch (Fmode)
			{
			case 1:
				try
				{
					_File.Seek(0L, SeekOrigin.Begin);
					try
					{
						sbyte[] array = new sbyte[8];
						array[0] = (sbyte)(SupportClass.URShift(_RiffHeader.CkId, 24) & 0xFF);
						array[1] = (sbyte)(SupportClass.URShift(_RiffHeader.CkId, 16) & 0xFF);
						array[2] = (sbyte)(SupportClass.URShift(_RiffHeader.CkId, 8) & 0xFF);
						array[3] = (sbyte)(_RiffHeader.CkId & 0xFF);
						array[7] = (sbyte)(SupportClass.URShift(_RiffHeader.CkSize, 24) & 0xFF);
						array[6] = (sbyte)(SupportClass.URShift(_RiffHeader.CkSize, 16) & 0xFF);
						array[5] = (sbyte)(SupportClass.URShift(_RiffHeader.CkSize, 8) & 0xFF);
						array[4] = (sbyte)(_RiffHeader.CkSize & 0xFF);
						_File.Write(SupportClass.ToByteArray(array), 0, 8);
						_File.Close();
					}
					catch
					{
						result = 3;
					}
				}
				catch
				{
					result = 3;
				}
				break;
			case 2:
				try
				{
					_File.Close();
				}
				catch
				{
					result = 3;
				}
				break;
			}
			_File = null;
			Fmode = 0;
			return result;
		}

		internal virtual long CurrentFilePosition()
		{
			try
			{
				return _File.Position;
			}
			catch
			{
				return -1L;
			}
		}

		internal virtual int Backpatch(long fileOffset, RiffChunkHeader data, int numBytes)
		{
			if (_File == null)
			{
				return 4;
			}
			try
			{
				_File.Seek(fileOffset, SeekOrigin.Begin);
			}
			catch
			{
				return 3;
			}
			return Write(data, numBytes);
		}

		internal virtual int Backpatch(long fileOffset, sbyte[] data, int numBytes)
		{
			if (_File == null)
			{
				return 4;
			}
			try
			{
				_File.Seek(fileOffset, SeekOrigin.Begin);
			}
			catch
			{
				return 3;
			}
			return Write(data, numBytes);
		}

		protected virtual int Seek(long offset)
		{
			try
			{
				_File.Seek(offset, SeekOrigin.Begin);
				return 0;
			}
			catch
			{
				return 3;
			}
		}

		internal static int FourCC(string chunkName)
		{
			sbyte[] destinationArray = new sbyte[4] { 32, 32, 32, 32 };
			SupportClass.GetSBytesFromString(chunkName, 0, 4, ref destinationArray, 0);
			return ((destinationArray[0] << 24) & (int)SupportClass.Identity(4278190080L)) | ((destinationArray[1] << 16) & 0xFF0000) | ((destinationArray[2] << 8) & 0xFF00) | (destinationArray[3] & 0xFF);
		}
	}
	public class WaveFile : RiffFile
	{
		internal sealed class WaveFormatChunkData
		{
			private WaveFile _EnclosingInstance;

			internal int NumAvgBytesPerSec;

			internal short NumBitsPerSample;

			internal short NumBlockAlign;

			internal short NumChannels;

			internal int NumSamplesPerSec;

			internal short FormatTag;

			internal WaveFile EnclosingInstance => _EnclosingInstance;

			internal WaveFormatChunkData(WaveFile enclosingInstance)
			{
				InitBlock(enclosingInstance);
				FormatTag = 1;
				Config(44100, 16, 1);
			}

			private void InitBlock(WaveFile enclosingInstance)
			{
				_EnclosingInstance = enclosingInstance;
			}

			internal void Config(int newSamplingRate, short newBitsPerSample, short newNumChannels)
			{
				NumSamplesPerSec = newSamplingRate;
				NumChannels = newNumChannels;
				NumBitsPerSample = newBitsPerSample;
				NumAvgBytesPerSec = NumChannels * NumSamplesPerSec * NumBitsPerSample / 8;
				NumBlockAlign = (short)(NumChannels * NumBitsPerSample / 8);
			}
		}

		public class WaveFormatChunk
		{
			internal WaveFormatChunkData Data;

			private WaveFile _EnclosingInstance;

			internal RiffChunkHeader Header;

			internal WaveFile EnclosingInstance => _EnclosingInstance;

			internal WaveFormatChunk(WaveFile enclosingInstance)
			{
				InitBlock(enclosingInstance);
				Header = new RiffChunkHeader(enclosingInstance);
				Data = new WaveFormatChunkData(enclosingInstance);
				Header.CkId = RiffFile.FourCC("fmt ");
				Header.CkSize = 16;
			}

			private void InitBlock(WaveFile enclosingInstance)
			{
				_EnclosingInstance = enclosingInstance;
			}

			internal virtual int VerifyValidity()
			{
				if (Header.CkId != RiffFile.FourCC("fmt ") || (Data.NumChannels != 1 && Data.NumChannels != 2) || Data.NumAvgBytesPerSec != Data.NumChannels * Data.NumSamplesPerSec * Data.NumBitsPerSample / 8 || Data.NumBlockAlign != Data.NumChannels * Data.NumBitsPerSample / 8)
				{
					return 0;
				}
				return 1;
			}
		}

		public class WaveFileSample
		{
			internal short[] Chan;

			private WaveFile _EnclosingInstance;

			internal WaveFile EnclosingInstance => _EnclosingInstance;

			internal WaveFileSample(WaveFile enclosingInstance)
			{
				InitBlock(enclosingInstance);
				Chan = new short[2];
			}

			private void InitBlock(WaveFile enclosingInstance)
			{
				_EnclosingInstance = enclosingInstance;
			}
		}

		internal const int MAX_WAVE_CHANNELS = 2;

		private readonly int _NumSamples;

		private readonly RiffChunkHeader _PcmData;

		private readonly WaveFormatChunk _WaveFormat;

		private bool _JustWriteLengthBytes;

		private long _PcmDataOffset;

		internal WaveFile()
		{
			_PcmData = new RiffChunkHeader(this);
			_WaveFormat = new WaveFormatChunk(this);
			_PcmData.CkId = RiffFile.FourCC("data");
			_PcmData.CkSize = 0;
			_NumSamples = 0;
		}

		internal virtual int OpenForWrite(string filename, Stream stream, int samplingRate, short bitsPerSample, short numChannels)
		{
			if ((bitsPerSample != 8 && bitsPerSample != 16) || numChannels < 1 || numChannels > 2)
			{
				return 4;
			}
			_WaveFormat.Data.Config(samplingRate, bitsPerSample, numChannels);
			int num = 0;
			if (stream != null)
			{
				Open(stream, 1);
			}
			else
			{
				Open(filename, 1);
			}
			if (num == 0)
			{
				sbyte[] data = new sbyte[4]
				{
					(sbyte)SupportClass.Identity(87L),
					(sbyte)SupportClass.Identity(65L),
					(sbyte)SupportClass.Identity(86L),
					(sbyte)SupportClass.Identity(69L)
				};
				num = Write(data, 4);
				if (num == 0)
				{
					num = Write(_WaveFormat.Header, 8);
					num = Write(_WaveFormat.Data.FormatTag, 2);
					num = Write(_WaveFormat.Data.NumChannels, 2);
					num = Write(_WaveFormat.Data.NumSamplesPerSec, 4);
					num = Write(_WaveFormat.Data.NumAvgBytesPerSec, 4);
					num = Write(_WaveFormat.Data.NumBlockAlign, 2);
					num = Write(_WaveFormat.Data.NumBitsPerSample, 2);
					if (num == 0)
					{
						_PcmDataOffset = CurrentFilePosition();
						num = Write(_PcmData, 8);
					}
				}
			}
			return num;
		}

		internal virtual int WriteData(short[] data, int numData)
		{
			int num = numData * 2;
			_PcmData.CkSize += num;
			return Write(data, num);
		}

		internal override int Close()
		{
			int num = 0;
			if (Fmode == 1)
			{
				num = Backpatch(_PcmDataOffset, _PcmData, 8);
			}
			if (!_JustWriteLengthBytes && num == 0)
			{
				num = base.Close();
			}
			return num;
		}

		internal int Close(bool justWriteLengthBytes)
		{
			_JustWriteLengthBytes = justWriteLengthBytes;
			int result = Close();
			_JustWriteLengthBytes = false;
			return result;
		}

		internal virtual int SamplingRate()
		{
			return _WaveFormat.Data.NumSamplesPerSec;
		}

		internal virtual short BitsPerSample()
		{
			return _WaveFormat.Data.NumBitsPerSample;
		}

		internal virtual short NumChannels()
		{
			return _WaveFormat.Data.NumChannels;
		}

		internal virtual int NumSamples()
		{
			return _NumSamples;
		}

		internal virtual int OpenForWrite(string filename, WaveFile otherWave)
		{
			return OpenForWrite(filename, null, otherWave.SamplingRate(), otherWave.BitsPerSample(), otherWave.NumChannels());
		}
	}
	public class WaveFileBuffer : ABuffer
	{
		private readonly short[] _Buffer;

		private readonly short[] _Bufferp;

		private readonly int _Channels;

		private readonly WaveFile _OutWave;

		internal WaveFileBuffer(int numberOfChannels, int freq, string fileName)
		{
			if (fileName == null)
			{
				throw new NullReferenceException("FileName");
			}
			_Buffer = new short[2304];
			_Bufferp = new short[2];
			_Channels = numberOfChannels;
			for (int i = 0; i < numberOfChannels; i++)
			{
				_Bufferp[i] = (short)i;
			}
			_OutWave = new WaveFile();
			_OutWave.OpenForWrite(fileName, null, freq, 16, (short)_Channels);
		}

		internal WaveFileBuffer(int numberOfChannels, int freq, Stream stream)
		{
			_Buffer = new short[2304];
			_Bufferp = new short[2];
			_Channels = numberOfChannels;
			for (int i = 0; i < numberOfChannels; i++)
			{
				_Bufferp[i] = (short)i;
			}
			_OutWave = new WaveFile();
			_OutWave.OpenForWrite(null, stream, freq, 16, (short)_Channels);
		}

		protected override void Append(int channel, short valueRenamed)
		{
			_Buffer[_Bufferp[channel]] = valueRenamed;
			_Bufferp[channel] = (short)(_Bufferp[channel] + _Channels);
		}

		internal override void WriteBuffer(int val)
		{
			_OutWave.WriteData(_Buffer, _Bufferp[0]);
			for (int i = 0; i < _Channels; i++)
			{
				_Bufferp[i] = (short)i;
			}
		}

		internal void Close(bool justWriteLengthBytes)
		{
			_OutWave.Close(justWriteLengthBytes);
		}

		internal override void Close()
		{
			_OutWave.Close();
		}

		internal override void ClearBuffer()
		{
		}

		internal override void SetStopFlag()
		{
		}
	}
}
namespace MP3Sharp.Decoding
{
	public abstract class ABuffer
	{
		internal const int OBUFFERSIZE = 2304;

		internal const int MAXCHANNELS = 2;

		protected abstract void Append(int channel, short sampleValue);

		internal virtual void AppendSamples(int channel, float[] samples)
		{
			for (int i = 0; i < 32; i++)
			{
				Append(channel, Clip(samples[i]));
			}
		}

		private static short Clip(float sample)
		{
			if (!(sample > 32767f))
			{
				if (!(sample < -32768f))
				{
					return (short)sample;
				}
				return short.MinValue;
			}
			return short.MaxValue;
		}

		internal abstract void WriteBuffer(int val);

		internal abstract void Close();

		internal abstract void ClearBuffer();

		internal abstract void SetStopFlag();
	}
	internal sealed class BitReserve
	{
		private const int BUFSIZE = 32768;

		private const int BUFSIZE_MASK = 32767;

		private int[] _Buffer;

		private int _Offset;

		private int _Totbit;

		private int _BufByteIdx;

		internal BitReserve()
		{
			InitBlock();
			_Offset = 0;
			_Totbit = 0;
			_BufByteIdx = 0;
		}

		private void InitBlock()
		{
			_Buffer = new int[32768];
		}

		internal int HssTell()
		{
			return _Totbit;
		}

		internal int ReadBits(int n)
		{
			_Totbit += n;
			int num = 0;
			int num2 = _BufByteIdx;
			if (num2 + n < 32768)
			{
				while (n-- > 0)
				{
					num <<= 1;
					num |= ((_Buffer[num2++] != 0) ? 1 : 0);
				}
			}
			else
			{
				while (n-- > 0)
				{
					num <<= 1;
					num |= ((_Buffer[num2] != 0) ? 1 : 0);
					num2 = (num2 + 1) & 0x7FFF;
				}
			}
			_BufByteIdx = num2;
			return num;
		}

		internal int ReadOneBit()
		{
			_Totbit++;
			int result = _Buffer[_BufByteIdx];
			_BufByteIdx = (_BufByteIdx + 1) & 0x7FFF;
			return result;
		}

		internal void PutBuffer(int val)
		{
			int offset = _Offset;
			_Buffer[offset++] = val & 0x80;
			_Buffer[offset++] = val & 0x40;
			_Buffer[offset++] = val & 0x20;
			_Buffer[offset++] = val & 0x10;
			_Buffer[offset++] = val & 8;
			_Buffer[offset++] = val & 4;
			_Buffer[offset++] = val & 2;
			_Buffer[offset++] = val & 1;
			if (offset == 32768)
			{
				_Offset = 0;
			}
			else
			{
				_Offset = offset;
			}
		}

		internal void RewindStreamBits(int bitCount)
		{
			_Totbit -= bitCount;
			_BufByteIdx -= bitCount;
			if (_BufByteIdx < 0)
			{
				_BufByteIdx += 32768;
			}
		}

		internal void RewindStreamBytes(int byteCount)
		{
			int num = byteCount << 3;
			_Totbit -= num;
			_BufByteIdx -= num;
			if (_BufByteIdx < 0)
			{
				_BufByteIdx += 32768;
			}
		}
	}
	public sealed class Bitstream
	{
		private const int BUFFER_INT_SIZE = 433;

		internal const sbyte INITIAL_SYNC = 0;

		internal const sbyte STRICT_SYNC = 1;

		private readonly int[] _Bitmask = new int[18]
		{
			0, 1, 3, 7, 15, 31, 63, 127, 255, 511,
			1023, 2047, 4095, 8191, 16383, 32767, 65535, 131071
		};

		private readonly PushbackStream _SourceStream;

		private int _BitIndex;

		private Crc16[] _CRC;

		private sbyte[] _FrameBytes;

		private int[] _FrameBuffer;

		private int _FrameSize;

		private Header _Header;

		private bool _SingleChMode;

		private sbyte[] _SyncBuffer;

		private int _SyncWord;

		private int _WordPointer;

		internal Bitstream(PushbackStream stream)
		{
			InitBlock();
			_SourceStream = stream ?? throw new NullReferenceException("in stream is null");
			CloseFrame();
		}

		private void InitBlock()
		{
			_CRC = new Crc16[1];
			_SyncBuffer = new sbyte[4];
			_FrameBytes = new sbyte[1732];
			_FrameBuffer = new int[433];
			_Header = new Header();
		}

		internal void Close()
		{
			try
			{
				_SourceStream.Close();
			}
			catch (IOException throwable)
			{
				throw NewBitstreamException(258, throwable);
			}
		}

		internal Header ReadFrame()
		{
			Header result = null;
			try
			{
				result = ReadNextFrame();
			}
			catch (BitstreamException ex)
			{
				if (ex.ErrorCode != 260)
				{
					throw NewBitstreamException(ex.ErrorCode, ex);
				}
			}
			return result;
		}

		private Header ReadNextFrame()
		{
			if (_FrameSize == -1)
			{
				_Header.read_header(this, _CRC);
			}
			return _Header;
		}

		internal void UnreadFrame()
		{
			if (_WordPointer == -1 && _BitIndex == -1 && _FrameSize > 0)
			{
				try
				{
					_SourceStream.UnRead(_FrameSize);
				}
				catch
				{
					throw NewBitstreamException(258);
				}
			}
		}

		internal void CloseFrame()
		{
			_FrameSize = -1;
			_WordPointer = -1;
			_BitIndex = -1;
		}

		internal bool IsSyncCurrentPosition(int syncmode)
		{
			int num = ReadBytes(_SyncBuffer, 0, 4);
			int headerstring = ((_SyncBuffer[0] << 24) & (int)SupportClass.Identity(4278190080L)) | ((_SyncBuffer[1] << 16) & 0xFF0000) | ((_SyncBuffer[2] << 8) & 0xFF00) | (_SyncBuffer[3] & 0xFF);
			try
			{
				_SourceStream.UnRead(num);
			}
			catch (Exception inner)
			{
				throw new MP3SharpException("Could not restore file after reading frame header.", inner);
			}
			bool result = false;
			switch (num)
			{
			case 0:
				result = true;
				break;
			case 4:
				result = IsSyncMark(headerstring, syncmode, _SyncWord);
				break;
			}
			return result;
		}

		internal int ReadBits(int n)
		{
			return GetBitsFromBuffer(n);
		}

		internal int ReadCheckedBits(int n)
		{
			return GetBitsFromBuffer(n);
		}

		internal BitstreamException NewBitstreamException(int errorcode)
		{
			return new BitstreamException(errorcode, null);
		}

		internal BitstreamException NewBitstreamException(int errorcode, Exception throwable)
		{
			return new BitstreamException(errorcode, throwable);
		}

		internal int SyncHeader(sbyte syncmode)
		{
			if (ReadBytes(_SyncBuffer, 0, 3) != 3)
			{
				throw NewBitstreamException(260, null);
			}
			int num = ((_SyncBuffer[0] << 16) & 0xFF0000) | ((_SyncBuffer[1] << 8) & 0xFF00) | (_SyncBuffer[2] & 0xFF);
			do
			{
				num <<= 8;
				if (ReadBytes(_SyncBuffer, 3, 1) != 1)
				{
					throw NewBitstreamException(260, null);
				}
				num |= _SyncBuffer[3] & 0xFF;
			}
			while (!IsSyncMark(num, syncmode, _SyncWord));
			return num;
		}

		internal bool IsSyncMark(int headerstring, int syncmode, int word)
		{
			bool flag = ((syncmode != 0) ? ((headerstring & 0xFFE00000u) == 4292870144u && (headerstring & 0xC0) == 192 == _SingleChMode) : ((headerstring & 0xFFE00000u) == 4292870144u));
			if (flag)
			{
				flag = (SupportClass.URShift(headerstring, 10) & 3) != 3;
			}
			if (flag)
			{
				flag = (SupportClass.URShift(headerstring, 17) & 3) != 0;
			}
			if (flag)
			{
				flag = (SupportClass.URShift(headerstring, 19) & 3) != 1;
				if (!flag)
				{
					Console.WriteLine("INVALID VERSION DETECTED");
				}
			}
			return flag;
		}

		internal void Read_frame_data(int bytesize)
		{
			ReadFully(_FrameBytes, 0, bytesize);
			_FrameSize = bytesize;
			_WordPointer = -1;
			_BitIndex = -1;
		}

		internal void ParseFrame()
		{
			int num = 0;
			sbyte[] frameBytes = _FrameBytes;
			int frameSize = _FrameSize;
			for (int i = 0; i < frameSize; i += 4)
			{
				sbyte b = frameBytes[i];
				sbyte b2 = 0;
				sbyte b3 = 0;
				sbyte b4 = 0;
				if (i + 1 < frameSize)
				{
					b2 = frameBytes[i + 1];
				}
				if (i + 2 < frameSize)
				{
					b3 = frameBytes[i + 2];
				}
				if (i + 3 < frameSize)
				{
					b4 = frameBytes[i + 3];
				}
				_FrameBuffer[num++] = ((b << 24) & (int)SupportClass.Identity(4278190080L)) | ((b2 << 16) & 0xFF0000) | ((b3 << 8) & 0xFF00) | (b4 & 0xFF);
			}
			_WordPointer = 0;
			_BitIndex = 0;
		}

		internal int GetBitsFromBuffer(int countBits)
		{
			int num = _BitIndex + countBits;
			if (_WordPointer < 0)
			{
				_WordPointer = 0;
			}
			if (num <= 32)
			{
				int result = SupportClass.URShift(_FrameBuffer[_WordPointer], 32 - num) & _Bitmask[countBits];
				if ((_BitIndex += countBits) == 32)
				{
					_BitIndex = 0;
					_WordPointer++;
				}
				return result;
			}
			int num2 = _FrameBuffer[_WordPointer] & 0xFFFF;
			_WordPointer++;
			int number = _FrameBuffer[_WordPointer] & (int)SupportClass.Identity(4294901760L);
			int result2 = SupportClass.URShift(((num2 << 16) & (int)SupportClass.Identity(4294901760L)) | (SupportClass.URShift(number, 16) & 0xFFFF), 48 - num) & _Bitmask[countBits];
			_BitIndex = num - 32;
			return result2;
		}

		internal void SetSyncWord(int syncword0)
		{
			_SyncWord = syncword0 & -193;
			_SingleChMode = (syncword0 & 0xC0) == 192;
		}

		private void ReadFully(sbyte[] b, int offs, int len)
		{
			try
			{
				while (len > 0)
				{
					int num = _SourceStream.Read(b, offs, len);
					if (num == -1 || num == 0)
					{
						while (len-- > 0)
						{
							b[offs++] = 0;
						}
						break;
					}
					offs += num;
					len -= num;
				}
			}
			catch (IOException throwable)
			{
				throw NewBitstreamException(258, throwable);
			}
		}

		private int ReadBytes(sbyte[] b, int offs, int len)
		{
			int num = 0;
			try
			{
				while (len > 0)
				{
					int num2 = _SourceStream.Read(b, offs, len);
					if (num2 != -1 && num2 != 0)
					{
						num += num2;
						offs += num2;
						len -= num2;
						continue;
					}
					break;
				}
			}
			catch (IOException throwable)
			{
				throw NewBitstreamException(258, throwable);
			}
			return num;
		}
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct BitstreamErrors
	{
		internal const int UNKNOWN_ERROR = 256;

		internal const int UNKNOWN_SAMPLE_RATE = 257;

		internal const int STREA_ERROR = 258;

		internal const int UNEXPECTED_EOF = 259;

		internal const int STREA_EOF = 260;

		internal const int BITSTREA_LAST = 511;

		internal const int BITSTREAM_ERROR = 256;

		internal const int DECODER_ERROR = 512;
	}
	[Serializable]
	public class BitstreamException : MP3SharpException
	{
		private int _ErrorCode;

		internal virtual int ErrorCode => _ErrorCode;

		internal BitstreamException(string message, Exception inner)
			: base(message, inner)
		{
			InitBlock();
		}

		internal BitstreamException(int errorcode, Exception inner)
			: this(GetErrorString(errorcode), inner)
		{
			InitBlock();
			_ErrorCode = errorcode;
		}

		protected BitstreamException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			_ErrorCode = info.GetInt32("ErrorCode");
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ErrorCode", _ErrorCode);
			base.GetObjectData(info, context);
		}

		private void InitBlock()
		{
			_ErrorCode = 256;
		}

		internal static string GetErrorString(int errorcode)
		{
			return "Bitstream errorcode " + Convert.ToString(errorcode, 16);
		}
	}
	[Serializable]
	internal class CircularByteBuffer
	{
		private byte[] _DataArray;

		private int _Index;

		private int _Length;

		private int _NumValid;

		internal int BufferSize
		{
			get
			{
				return _Length;
			}
			set
			{
				byte[] array = new byte[value];
				int num = ((_Length > value) ? value : _Length);
				for (int i = 0; i < num; i++)
				{
					array[i] = InternalGet(i - _Length + 1);
				}
				_DataArray = array;
				_Index = num - 1;
				_Length = value;
			}
		}

		internal byte this[int index]
		{
			get
			{
				return InternalGet(-1 - index);
			}
			set
			{
				InternalSet(-1 - index, value);
			}
		}

		internal int NumValid
		{
			get
			{
				return _NumValid;
			}
			set
			{
				if (value > _NumValid)
				{
					throw new Exception("Can't set NumValid to " + value + " which is greater than the current numValid value of " + _NumValid);
				}
				_NumValid = value;
			}
		}

		internal CircularByteBuffer(int size)
		{
			_DataArray = new byte[size];
			_Length = size;
		}

		internal CircularByteBuffer(CircularByteBuffer cdb)
		{
			lock (cdb)
			{
				_Length = cdb._Length;
				_NumValid = cdb._NumValid;
				_Index = cdb._Index;
				_DataArray = new byte[_Length];
				for (int i = 0; i < _Length; i++)
				{
					_DataArray[i] = cdb._DataArray[i];
				}
			}
		}

		internal CircularByteBuffer Copy()
		{
			return new CircularByteBuffer(this);
		}

		internal void Reset()
		{
			_Index = 0;
			_NumValid = 0;
		}

		internal byte Push(byte newValue)
		{
			lock (this)
			{
				byte result = InternalGet(_Length);
				_DataArray[_Index] = newValue;
				_NumValid++;
				if (_NumValid > _Length)
				{
					_NumValid = _Length;
				}
				_Index++;
				_Index %= _Length;
				return result;
			}
		}

		internal byte Pop()
		{
			lock (this)
			{
				if (_NumValid == 0)
				{
					throw new Exception("Can't pop off an empty CircularByteBuffer");
				}
				_NumValid--;
				return this[_NumValid];
			}
		}

		internal byte Peek()
		{
			lock (this)
			{
				return InternalGet(_Length);
			}
		}

		private byte InternalGet(int offset)
		{
			int i;
			for (i = _Index + offset; i >= _Length; i -= _Length)
			{
			}
			for (; i < 0; i += _Length)
			{
			}
			return _DataArray[i];
		}

		private void InternalSet(int offset, byte valueToSet)
		{
			int i;
			for (i = _Index + offset; i > _Length; i -= _Length)
			{
			}
			for (; i < 0; i += _Length)
			{
			}
			_DataArray[i] = valueToSet;
		}

		internal byte[] GetRange(int str, int stp)
		{
			byte[] array = new byte[str - stp + 1];
			int num = str;
			int num2 = 0;
			while (num >= stp)
			{
				array[num2] = this[num];
				num--;
				num2++;
			}
			return array;
		}

		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < _DataArray.Length; i++)
			{
				text = text + _DataArray[i] + " ";
			}
			return text + "\n index = " + _Index + " numValid = " + NumValid;
		}
	}
	public sealed class Crc16
	{
		private static readonly short Polynomial;

		private short _CRC;

		static Crc16()
		{
			Polynomial = (short)SupportClass.Identity(32773L);
		}

		internal Crc16()
		{
			_CRC = (short)SupportClass.Identity(65535L);
		}

		internal void AddBits(int bitstring, int length)
		{
			int num = 1 << length - 1;
			do
			{
				if (((_CRC & 0x8000) == 0) ^ ((bitstring & num) == 0))
				{
					_CRC <<= 1;
					_CRC ^= Polynomial;
				}
				else
				{
					_CRC <<= 1;
				}
			}
			while ((num = SupportClass.URShift(num, 1)) != 0);
		}

		internal short Checksum()
		{
			short cRC = _CRC;
			_CRC = (short)SupportClass.Identity(65535L);
			return cRC;
		}
	}
	public class Decoder
	{
		public class Params : ICloneable
		{
			private OutputChannels _OutputChannels;

			private readonly Equalizer _Equalizer;

			internal virtual OutputChannels OutputChannels
			{
				get
				{
					return _OutputChannels;
				}
				set
				{
					_OutputChannels = value ?? throw new NullReferenceException("out");
				}
			}

			internal virtual Equalizer InitialEqualizerSettings => _Equalizer;

			public object Clone()
			{
				try
				{
					return MemberwiseClone();
				}
				catch (Exception ex)
				{
					throw new ApplicationException(this?.ToString() + ": " + ex);
				}
			}
		}

		private static readonly Params DecoderDefaultParams = new Params();

		private Equalizer _Equalizer;

		private SynthesisFilter _LeftChannelFilter;

		private SynthesisFilter _RightChannelFilter;

		private bool _IsInitialized;

		private LayerIDecoder _L1Decoder;

		private LayerIIDecoder _L2Decoder;

		private LayerIIIDecoder _L3Decoder;

		private ABuffer _Output;

		private int _OutputChannels;

		private int _OutputFrequency;

		internal static Params DefaultParams => (Params)DecoderDefaultParams.Clone();

		internal virtual Equalizer Equalizer
		{
			set
			{
				if (value == null)
				{
					value = Equalizer.PassThruEq;
				}
				_Equalizer.FromEqualizer = value;
				float[] bandFactors = _Equalizer.BandFactors;
				if (_LeftChannelFilter != null)
				{
					_LeftChannelFilter.Eq = bandFactors;
				}
				if (_RightChannelFilter != null)
				{
					_RightChannelFilter.Eq = bandFactors;
				}
			}
		}

		internal virtual ABuffer OutputBuffer
		{
			set
			{
				_Output = value;
			}
		}

		internal virtual int OutputFrequency => _OutputFrequency;

		internal virtual int OutputChannels => _OutputChannels;

		internal virtual int OutputBlockSize => 2304;

		internal Decoder()
			: this(null)
		{
			InitBlock();
		}

		internal Decoder(Params params0)
		{
			InitBlock();
			if (params0 == null)
			{
				params0 = DecoderDefaultParams;
			}
			Equalizer initialEqualizerSettings = params0.InitialEqualizerSettings;
			if (initialEqualizerSettings != null)
			{
				_Equalizer.FromEqualizer = initialEqualizerSettings;
			}
		}

		private void InitBlock()
		{
			_Equalizer = new Equalizer();
		}

		internal virtual ABuffer DecodeFrame(Header header, Bitstream stream)
		{
			if (!_IsInitialized)
			{
				Initialize(header);
			}
			int layer = header.Layer();
			_Output.ClearBuffer();
			RetrieveDecoder(header, stream, layer).DecodeFrame();
			_Output.WriteBuffer(1);
			return _Output;
		}

		protected virtual DecoderException NewDecoderException(int errorcode)
		{
			return new DecoderException(errorcode, null);
		}

		protected virtual DecoderException NewDecoderException(int errorcode, Exception throwable)
		{
			return new DecoderException(errorcode, throwable);
		}

		protected virtual IFrameDecoder RetrieveDecoder(Header header, Bitstream stream, int layer)
		{
			IFrameDecoder frameDecoder = null;
			switch (layer)
			{
			case 3:
				if (_L3Decoder == null)
				{
					_L3Decoder = new LayerIIIDecoder(stream, header, _LeftChannelFilter, _RightChannelFilter, _Output, 0);
				}
				frameDecoder = _L3Decoder;
				break;
			case 2:
				if (_L2Decoder == null)
				{
					_L2Decoder = new LayerIIDecoder();
					_L2Decoder.Create(stream, header, _LeftChannelFilter, _RightChannelFilter, _Output, 0);
				}
				frameDecoder = _L2Decoder;
				break;
			case 1:
				if (_L1Decoder == null)
				{
					_L1Decoder = new LayerIDecoder();
					_L1Decoder.Create(stream, header, _LeftChannelFilter, _RightChannelFilter, _Output, 0);
				}
				frameDecoder = _L1Decoder;
				break;
			}
			if (frameDecoder == null)
			{
				throw NewDecoderException(513, null);
			}
			return frameDecoder;
		}

		private void Initialize(Header header)
		{
			int num = ((header.Mode() == 3) ? 1 : 2);
			if (_Output == null)
			{
				_Output = new SampleBuffer(header.Frequency(), num);
			}
			float[] bandFactors = _Equalizer.BandFactors;
			_LeftChannelFilter = new SynthesisFilter(0, 32700f, bandFactors);
			if (num == 2)
			{
				_RightChannelFilter = new SynthesisFilter(1, 32700f, bandFactors);
			}
			_OutputChannels = num;
			_OutputFrequency = header.Frequency();
			_IsInitialized = true;
		}
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct DecoderErrors
	{
		internal const int UNKNOWN_ERROR = 512;

		internal const int UNSUPPORTED_LAYER = 513;
	}
	[Serializable]
	public class DecoderException : MP3SharpException
	{
		private int _ErrorCode;

		internal virtual int ErrorCode => _ErrorCode;

		internal DecoderException(string message, Exception inner)
			: base(message, inner)
		{
			InitBlock();
		}

		internal DecoderException(int errorcode, Exception inner)
			: this(GetErrorString(errorcode), inner)
		{
			InitBlock();
			_ErrorCode = errorcode;
		}

		protected DecoderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			_ErrorCode = info.GetInt32("ErrorCode");
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ErrorCode", _ErrorCode);
			base.GetObjectData(info, context);
		}

		private void InitBlock()
		{
			_ErrorCode = 512;
		}

		internal static string GetErrorString(int errorcode)
		{
			return "Decoder errorcode " + Convert.ToString(errorcode, 16);
		}
	}
	public class Equalizer
	{
		internal abstract class EQFunction
		{
			internal virtual float GetBand(int band)
			{
				return 0f;
			}
		}

		private const int BANDS = 32;

		internal const float BAND_NOT_PRESENT = float.NegativeInfinity;

		internal static readonly Equalizer PassThruEq = new Equalizer();

		private float[] _Settings;

		internal float[] FromFloatArray
		{
			set
			{
				Reset();
				int num = ((value.Length > 32) ? 32 : value.Length);
				for (int i = 0; i < num; i++)
				{
					_Settings[i] = Limit(value[i]);
				}
			}
		}

		internal virtual Equalizer FromEqualizer
		{
			set
			{
				if (value != this)
				{
					FromFloatArray = value._Settings;
				}
			}
		}

		internal EQFunction FromEQFunction
		{
			set
			{
				Reset();
				int num = 32;
				for (int i = 0; i < num; i++)
				{
					_Settings[i] = Limit(value.GetBand(i));
				}
			}
		}

		internal virtual int BandCount => _Settings.Length;

		internal virtual float[] BandFactors
		{
			get
			{
				float[] array = new float[32];
				for (int i = 0; i < 32; i++)
				{
					array[i] = GetBandFactor(_Settings[i]);
				}
				return array;
			}
		}

		internal Equalizer()
		{
			InitBlock();
		}

		internal Equalizer(float[] settings)
		{
			InitBlock();
			FromFloatArray = settings;
		}

		internal Equalizer(EQFunction eq)
		{
			InitBlock();
			FromEQFunction = eq;
		}

		private void InitBlock()
		{
			_Settings = new float[32];
		}

		internal void Reset()
		{
			for (int i = 0; i < 32; i++)
			{
				_Settings[i] = 0f;
			}
		}

		internal float SetBand(int band, float neweq)
		{
			float result = 0f;
			if (band >= 0 && band < 32)
			{
				result = _Settings[band];
				_Settings[band] = Limit(neweq);
			}
			return result;
		}

		internal float GetBand(int band)
		{
			float result = 0f;
			if (band >= 0 && band < 32)
			{
				result = _Settings[band];
			}
			return result;
		}

		private float Limit(float eq)
		{
			if (eq == float.NegativeInfinity)
			{
				return eq;
			}
			if (eq > 1f)
			{
				return 1f;
			}
			if (eq < -1f)
			{
				return -1f;
			}
			return eq;
		}

		internal float GetBandFactor(float eq)
		{
			if (eq == float.NegativeInfinity)
			{
				return 0f;
			}
			return (float)Math.Pow(2.0, eq);
		}
	}
	public class Header
	{
		internal const int MPEG2_LSF = 0;

		internal const int MPEG25_LSF = 2;

		internal const int MPEG1 = 1;

		internal const int STEREO = 0;

		internal const int JOINT_STEREO = 1;

		internal const int DUAL_CHANNEL = 2;

		internal const int SINGLE_CHANNEL = 3;

		internal const int FOURTYFOUR_POINT_ONE = 0;

		internal const int FOURTYEIGHT = 1;

		internal const int THIRTYTWO = 2;

		internal static readonly int[][] Frequencies = new int[3][]
		{
			new int[4] { 22050, 24000, 16000, 1 },
			new int[4] { 44100, 48000, 32000, 1 },
			new int[4] { 11025, 12000, 8000, 1 }
		};

		internal static readonly int[][][] Bitrates = new int[3][][]
		{
			new int[3][]
			{
				new int[16]
				{
					0, 32000, 48000, 56000, 64000, 80000, 96000, 112000, 128000, 144000,
					160000, 176000, 192000, 224000, 256000, 0
				},
				new int[16]
				{
					0, 8000, 16000, 24000, 32000, 40000, 48000, 56000, 64000, 80000,
					96000, 112000, 128000, 144000, 160000, 0
				},
				new int[16]
				{
					0, 8000, 16000, 24000, 32000, 40000, 48000, 56000, 64000, 80000,
					96000, 112000, 128000, 144000, 160000, 0
				}
			},
			new int[3][]
			{
				new int[16]
				{
					0, 32000, 64000, 96000, 128000, 160000, 192000, 224000, 256000, 288000,
					320000, 352000, 384000, 416000, 448000, 0
				},
				new int[16]
				{
					0, 32000, 48000, 56000, 64000, 80000, 96000, 112000, 128000, 160000,
					192000, 224000, 256000, 320000, 384000, 0
				},
				new int[16]
				{
					0, 32000, 40000, 48000, 56000, 64000, 80000, 96000, 112000, 128000,
					160000, 192000, 224000, 256000, 320000, 0
				}
			},
			new int[3][]
			{
				new int[16]
				{
					0, 32000, 48000, 56000, 64000, 80000, 96000, 112000, 128000, 144000,
					160000, 176000, 192000, 224000, 256000, 0
				},
				new int[16]
				{
					0, 8000, 16000, 24000, 32000, 40000, 48000, 56000, 64000, 80000,
					96000, 112000, 128000, 144000, 160000, 0
				},
				new int[16]
				{
					0, 8000, 16000, 24000, 32000, 40000, 48000, 56000, 64000, 80000,
					96000, 112000, 128000, 144000, 160000, 0
				}
			}
		};

		internal static readonly string[][][] BitrateStr = new string[3][][]
		{
			new string[3][]
			{
				new string[16]
				{
					"free format", "32 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s", "96 kbit/s", "112 kbit/s", "128 kbit/s", "144 kbit/s",
					"160 kbit/s", "176 kbit/s", "192 kbit/s", "224 kbit/s", "256 kbit/s", "forbidden"
				},
				new string[16]
				{
					"free format", "8 kbit/s", "16 kbit/s", "24 kbit/s", "32 kbit/s", "40 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s",
					"96 kbit/s", "112 kbit/s", "128 kbit/s", "144 kbit/s", "160 kbit/s", "forbidden"
				},
				new string[16]
				{
					"free format", "8 kbit/s", "16 kbit/s", "24 kbit/s", "32 kbit/s", "40 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s",
					"96 kbit/s", "112 kbit/s", "128 kbit/s", "144 kbit/s", "160 kbit/s", "forbidden"
				}
			},
			new string[3][]
			{
				new string[16]
				{
					"free format", "32 kbit/s", "64 kbit/s", "96 kbit/s", "128 kbit/s", "160 kbit/s", "192 kbit/s", "224 kbit/s", "256 kbit/s", "288 kbit/s",
					"320 kbit/s", "352 kbit/s", "384 kbit/s", "416 kbit/s", "448 kbit/s", "forbidden"
				},
				new string[16]
				{
					"free format", "32 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s", "96 kbit/s", "112 kbit/s", "128 kbit/s", "160 kbit/s",
					"192 kbit/s", "224 kbit/s", "256 kbit/s", "320 kbit/s", "384 kbit/s", "forbidden"
				},
				new string[16]
				{
					"free format", "32 kbit/s", "40 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s", "96 kbit/s", "112 kbit/s", "128 kbit/s",
					"160 kbit/s", "192 kbit/s", "224 kbit/s", "256 kbit/s", "320 kbit/s", "forbidden"
				}
			},
			new string[3][]
			{
				new string[16]
				{
					"free format", "32 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s", "96 kbit/s", "112 kbit/s", "128 kbit/s", "144 kbit/s",
					"160 kbit/s", "176 kbit/s", "192 kbit/s", "224 kbit/s", "256 kbit/s", "forbidden"
				},
				new string[16]
				{
					"free format", "8 kbit/s", "16 kbit/s", "24 kbit/s", "32 kbit/s", "40 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s",
					"96 kbit/s", "112 kbit/s", "128 kbit/s", "144 kbit/s", "160 kbit/s", "forbidden"
				},
				new string[16]
				{
					"free format", "8 kbit/s", "16 kbit/s", "24 kbit/s", "32 kbit/s", "40 kbit/s", "48 kbit/s", "56 kbit/s", "64 kbit/s", "80 kbit/s",
					"96 kbit/s", "112 kbit/s", "128 kbit/s", "144 kbit/s", "160 kbit/s", "forbidden"
				}
			}
		};

		internal short Checksum;

		internal int NSlots;

		private Crc16 _CRC;

		internal int Framesize;

		private bool _Copyright;

		private bool _Original;

		private int _Headerstring = -1;

		private int _Layer;

		private int _ProtectionBit;

		private int _BitrateIndex;

		private int _PaddingBit;

		private int _ModeExtension;

		private int _Mode;

		private int _NumberOfSubbands;

		private int _IntensityStereoBound;

		private int _SampleFrequency;

		private sbyte _Syncmode;

		private int _Version;

		internal virtual int SyncHeader => _Headerstring;

		internal Header()
		{
			InitBlock();
		}

		private void InitBlock()
		{
			_Syncmode = 0;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			stringBuilder.Append("Layer ");
			stringBuilder.Append(LayerAsString());
			stringBuilder.Append(" frame ");
			stringBuilder.Append(ModeAsString());
			stringBuilder.Append(' ');
			stringBuilder.Append(VersionAsString());
			if (!IsProtection())
			{
				stringBuilder.Append(" no");
			}
			stringBuilder.Append(" checksums");
			stringBuilder.Append(' ');
			stringBuilder.Append(SampleFrequencyAsString());
			stringBuilder.Append(',');
			stringBuilder.Append(' ');
			stringBuilder.Append(BitrateAsString());
			return stringBuilder.ToString();
		}

		internal void read_header(Bitstream stream, Crc16[] crcp)
		{
			bool flag = false;
			int num;
			do
			{
				num = (_Headerstring = stream.SyncHeader(_Syncmode));
				if (_Syncmode == 0)
				{
					_Version = SupportClass.URShift(num, 19) & 1;
					if ((SupportClass.URShift(num, 20) & 1) == 0)
					{
						if (_Version != 0)
						{
							throw stream.NewBitstreamException(256);
						}
						_Version = 2;
					}
					if ((_SampleFrequency = SupportClass.URShift(num, 10) & 3) == 3)
					{
						throw stream.NewBitstreamException(256);
					}
				}
				_Layer = (4 - SupportClass.URShift(num, 17)) & 3;
				_ProtectionBit = SupportClass.URShift(num, 16) & 1;
				_BitrateIndex = SupportClass.URShift(num, 12) & 0xF;
				_PaddingBit = SupportClass.URShift(num, 9) & 1;
				_Mode = SupportClass.URShift(num, 6) & 3;
				_ModeExtension = SupportClass.URShift(num, 4) & 3;
				if (_Mode == 1)
				{
					_IntensityStereoBound = (_ModeExtension << 2) + 4;
				}
				else
				{
					_IntensityStereoBound = 0;
				}
				_Copyright |= (SupportClass.URShift(num, 3) & 1) == 1;
				_Original |= (SupportClass.URShift(num, 2) & 1) == 1;
				if (_Layer == 1)
				{
					_NumberOfSubbands = 32;
				}
				else
				{
					int num2 = _BitrateIndex;
					if (_Mode != 3)
					{
						num2 = ((num2 == 4) ? 1 : (num2 - 4));
					}
					if (num2 == 1 || num2 == 2)
					{
						if (_SampleFrequency == 2)
						{
							_NumberOfSubbands = 12;
						}
						else
						{
							_NumberOfSubbands = 8;
						}
					}
					else if (_SampleFrequency == 1 || (num2 >= 3 && num2 <= 5))
					{
						_NumberOfSubbands = 27;
					}
					else
					{
						_NumberOfSubbands = 30;
					}
				}
				if (_IntensityStereoBound > _NumberOfSubbands)
				{
					_IntensityStereoBound = _NumberOfSubbands;
				}
				CalculateFrameSize();
				stream.Read_frame_data(Framesize);
				if (stream.IsSyncCurrentPosition(_Syncmode))
				{
					if (_Syncmode == 0)
					{
						_Syncmode = 1;
						stream.SetSyncWord(num & -521024);
					}
					flag = true;
				}
				else
				{
					stream.UnreadFrame();
				}
			}
			while (!flag);
			stream.ParseFrame();
			if (_ProtectionBit == 0)
			{
				Checksum = (short)stream.GetBitsFromBuffer(16);
				if (_CRC == null)
				{
					_CRC = new Crc16();
				}
				_CRC.AddBits(num, 16);
				crcp[0] = _CRC;
			}
			else
			{
				crcp[0] = null;
			}
			_ = _SampleFrequency;
		}

		internal int Version()
		{
			return _Version;
		}

		internal int Layer()
		{
			return _Layer;
		}

		internal int bitrate_index()
		{
			return _BitrateIndex;
		}

		internal int sample_frequency()
		{
			return _SampleFrequency;
		}

		internal int Frequency()
		{
			return Frequencies[_Version][_SampleFrequency];
		}

		internal int Mode()
		{
			return _Mode;
		}

		internal bool IsProtection()
		{
			if (_ProtectionBit == 0)
			{
				return true;
			}
			return false;
		}

		internal bool IsCopyright()
		{
			return _Copyright;
		}

		internal bool IsOriginal()
		{
			return _Original;
		}

		internal bool IsChecksumOK()
		{
			return Checksum == _CRC.Checksum();
		}

		internal bool IsPadding()
		{
			if (_PaddingBit == 0)
			{
				return false;
			}
			return true;
		}

		internal int Slots()
		{
			return NSlots;
		}

		internal int mode_extension()
		{
			return _ModeExtension;
		}

		internal int CalculateFrameSize()
		{
			if (_Layer == 1)
			{
				Framesize = 12 * Bitrates[_Version][0][_BitrateIndex] / Frequencies[_Version][_SampleFrequency];
				if (_PaddingBit != 0)
				{
					Framesize++;
				}
				Framesize <<= 2;
				NSlots = 0;
			}
			else
			{
				Framesize = 144 * Bitrates[_Version][_Layer - 1][_BitrateIndex] / Frequencies[_Version][_SampleFrequency];
				if (_Version == 0 || _Version == 2)
				{
					Framesize >>= 1;
				}
				if (_PaddingBit != 0)
				{
					Framesize++;
				}
				if (_Layer == 3)
				{
					if (_Version == 1)
					{
						NSlots = Framesize - ((_Mode == 3) ? 17 : 32) - ((_ProtectionBit == 0) ? 2 : 0) - 4;
					}
					else
					{
						NSlots = Framesize - ((_Mode == 3) ? 9 : 17) - ((_ProtectionBit == 0) ? 2 : 0) - 4;
					}
				}
				else
				{
					NSlots = 0;
				}
			}
			Framesize -= 4;
			return Framesize;
		}

		internal int MaxNumberOfFrame(int streamsize)
		{
			if (Framesize + 4 - _PaddingBit == 0)
			{
				return 0;
			}
			return streamsize / (Framesize + 4 - _PaddingBit);
		}

		internal int min_number_of_frames(int streamsize)
		{
			if (Framesize + 5 - _PaddingBit == 0)
			{
				return 0;
			}
			return streamsize / (Framesize + 5 - _PaddingBit);
		}

		internal float MsPerFrame()
		{
			return (new float[3][]
			{
				new float[3] { 8.707483f, 8f, 12f },
				new float[3] { 26.12245f, 24f, 36f },
				new float[3] { 26.12245f, 24f, 36f }
			})[_Layer - 1][_SampleFrequency];
		}

		internal float TotalMS(int streamsize)
		{
			return (float)MaxNumberOfFrame(streamsize) * MsPerFrame();
		}

		internal string LayerAsString()
		{
			return _Layer switch
			{
				1 => "I", 
				2 => "II", 
				3 => "III", 
				_ => null, 
			};
		}

		internal string BitrateAsString()
		{
			return BitrateStr[_Version][_Layer - 1][_BitrateIndex];
		}

		internal string SampleFrequencyAsString()
		{
			switch (_SampleFrequency)
			{
			case 2:
				if (_Version == 1)
				{
					return "32 kHz";
				}
				if (_Version == 0)
				{
					return "16 kHz";
				}
				return "8 kHz";
			case 0:
				if (_Version == 1)
				{
					return "44.1 kHz";
				}
				if (_Version == 0)
				{
					return "22.05 kHz";
				}
				return "11.025 kHz";
			case 1:
				if (_Version == 1)
				{
					return "48 kHz";
				}
				if (_Version == 0)
				{
					return "24 kHz";
				}
				return "12 kHz";
			default:
				return null;
			}
		}

		internal string ModeAsString()
		{
			return _Mode switch
			{
				0 => "Stereo", 
				1 => "Joint stereo", 
				2 => "Dual channel", 
				3 => "Single channel", 
				_ => null, 
			};
		}

		internal string VersionAsString()
		{
			return _Version switch
			{
				1 => "MPEG-1", 
				0 => "MPEG-2 LSF", 
				2 => "MPEG-2.5 LSF", 
				_ => null, 
			};
		}

		internal int NumberSubbands()
		{
			return _NumberOfSubbands;
		}

		internal int IntensityStereoBound()
		{
			return _IntensityStereoBound;
		}
	}
	internal sealed class Huffman
	{
		private const int MXOFF = 250;

		private const int HTN = 34;

		private static readonly int[][] ValTab0;

		private static readonly int[][] ValTab1;

		private static readonly int[][] ValTab2;

		private static readonly int[][] ValTab3;

		private static readonly int[][] ValTab4;

		private static readonly int[][] ValTab5;

		private static readonly int[][] ValTab6;

		private static readonly int[][] ValTab7;

		private static readonly int[][] ValTab8;

		private static readonly int[][] ValTab9;

		private static readonly int[][] ValTab10;

		private static readonly int[][] ValTab11;

		private static readonly int[][] ValTab12;

		private static readonly int[][] ValTab13;

		private static readonly int[][] ValTab14;

		private static readonly int[][] ValTab15;

		private static readonly int[][] ValTab16;

		private static readonly int[][] ValTab24;

		private static readonly int[][] ValTab32;

		private static readonly int[][] ValTab33;

		internal static Huffman[] HuffmanTable;

		private readonly int _Linbits;

		private readonly char _Tablename0;

		private readonly char _Tablename1;

		private readonly int _Treelen;

		private readonly int[][] _Val;

		private readonly int _Xlen;

		private readonly int _Ylen;

		private readonly int[] _Hlen;

		private readonly int _Linmax;

		private readonly int _RefRenamed;

		private readonly int[] _Table;

		private readonly char _Tablename2;

		static Huffman()
		{
			ValTab0 = new int[1][] { new int[2] };
			ValTab1 = new int[7][]
			{
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 2, 1 },
				new int[2] { 0, 16 },
				new int[2] { 2, 1 },
				new int[2] { 0, 1 },
				new int[2] { 0, 17 }
			};
			ValTab2 = new int[17][]
			{
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 16 },
				new int[2] { 0, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 17 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 32 },
				new int[2] { 0, 33 },
				new int[2] { 2, 1 },
				new int[2] { 0, 18 },
				new int[2] { 2, 1 },
				new int[2] { 0, 2 },
				new int[2] { 0, 34 }
			};
			ValTab3 = new int[17][]
			{
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 0, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 17 },
				new int[2] { 2, 1 },
				new int[2] { 0, 16 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 32 },
				new int[2] { 0, 33 },
				new int[2] { 2, 1 },
				new int[2] { 0, 18 },
				new int[2] { 2, 1 },
				new int[2] { 0, 2 },
				new int[2] { 0, 34 }
			};
			ValTab4 = new int[1][] { new int[2] };
			ValTab5 = new int[31][]
			{
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 16 },
				new int[2] { 0, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 17 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 32 },
				new int[2] { 0, 2 },
				new int[2] { 2, 1 },
				new int[2] { 0, 33 },
				new int[2] { 0, 18 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 34 },
				new int[2] { 0, 48 },
				new int[2] { 2, 1 },
				new int[2] { 0, 3 },
				new int[2] { 0, 19 },
				new int[2] { 2, 1 },
				new int[2] { 0, 49 },
				new int[2] { 2, 1 },
				new int[2] { 0, 50 },
				new int[2] { 2, 1 },
				new int[2] { 0, 35 },
				new int[2] { 0, 51 }
			};
			ValTab6 = new int[31][]
			{
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 0, 16 },
				new int[2] { 0, 17 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 32 },
				new int[2] { 0, 33 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 18 },
				new int[2] { 2, 1 },
				new int[2] { 0, 2 },
				new int[2] { 0, 34 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 49 },
				new int[2] { 0, 19 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 48 },
				new int[2] { 0, 50 },
				new int[2] { 2, 1 },
				new int[2] { 0, 35 },
				new int[2] { 2, 1 },
				new int[2] { 0, 3 },
				new int[2] { 0, 51 }
			};
			ValTab7 = new int[71][]
			{
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 16 },
				new int[2] { 0, 1 },
				new int[2] { 8, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 17 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 32 },
				new int[2] { 0, 2 },
				new int[2] { 0, 33 },
				new int[2] { 18, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 18 },
				new int[2] { 2, 1 },
				new int[2] { 0, 34 },
				new int[2] { 0, 48 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 49 },
				new int[2] { 0, 19 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 3 },
				new int[2] { 0, 50 },
				new int[2] { 2, 1 },
				new int[2] { 0, 35 },
				new int[2] { 0, 4 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 64 },
				new int[2] { 0, 65 },
				new int[2] { 2, 1 },
				new int[2] { 0, 20 },
				new int[2] { 2, 1 },
				new int[2] { 0, 66 },
				new int[2] { 0, 36 },
				new int[2] { 12, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 51 },
				new int[2] { 0, 67 },
				new int[2] { 0, 80 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 52 },
				new int[2] { 0, 5 },
				new int[2] { 0, 81 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 21 },
				new int[2] { 2, 1 },
				new int[2] { 0, 82 },
				new int[2] { 0, 37 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 68 },
				new int[2] { 0, 53 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 83 },
				new int[2] { 0, 84 },
				new int[2] { 2, 1 },
				new int[2] { 0, 69 },
				new int[2] { 0, 85 }
			};
			ValTab8 = new int[71][]
			{
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 2, 1 },
				new int[2] { 0, 16 },
				new int[2] { 0, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 17 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 33 },
				new int[2] { 0, 18 },
				new int[2] { 14, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 32 },
				new int[2] { 0, 2 },
				new int[2] { 2, 1 },
				new int[2] { 0, 34 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 48 },
				new int[2] { 0, 3 },
				new int[2] { 2, 1 },
				new int[2] { 0, 49 },
				new int[2] { 0, 19 },
				new int[2] { 14, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 50 },
				new int[2] { 0, 35 },
				new int[2] { 2, 1 },
				new int[2] { 0, 64 },
				new int[2] { 0, 4 },
				new int[2] { 2, 1 },
				new int[2] { 0, 65 },
				new int[2] { 2, 1 },
				new int[2] { 0, 20 },
				new int[2] { 0, 66 },
				new int[2] { 12, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 36 },
				new int[2] { 2, 1 },
				new int[2] { 0, 51 },
				new int[2] { 0, 80 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 67 },
				new int[2] { 0, 52 },
				new int[2] { 0, 81 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 21 },
				new int[2] { 2, 1 },
				new int[2] { 0, 5 },
				new int[2] { 0, 82 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 37 },
				new int[2] { 2, 1 },
				new int[2] { 0, 68 },
				new int[2] { 0, 53 },
				new int[2] { 2, 1 },
				new int[2] { 0, 83 },
				new int[2] { 2, 1 },
				new int[2] { 0, 69 },
				new int[2] { 2, 1 },
				new int[2] { 0, 84 },
				new int[2] { 0, 85 }
			};
			ValTab9 = new int[71][]
			{
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 0, 16 },
				new int[2] { 2, 1 },
				new int[2] { 0, 1 },
				new int[2] { 0, 17 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 32 },
				new int[2] { 0, 33 },
				new int[2] { 2, 1 },
				new int[2] { 0, 18 },
				new int[2] { 2, 1 },
				new int[2] { 0, 2 },
				new int[2] { 0, 34 },
				new int[2] { 12, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 48 },
				new int[2] { 0, 3 },
				new int[2] { 0, 49 },
				new int[2] { 2, 1 },
				new int[2] { 0, 19 },
				new int[2] { 2, 1 },
				new int[2] { 0, 50 },
				new int[2] { 0, 35 },
				new int[2] { 12, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 65 },
				new int[2] { 0, 20 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 64 },
				new int[2] { 0, 51 },
				new int[2] { 2, 1 },
				new int[2] { 0, 66 },
				new int[2] { 0, 36 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 4 },
				new int[2] { 0, 80 },
				new int[2] { 0, 67 },
				new int[2] { 2, 1 },
				new int[2] { 0, 52 },
				new int[2] { 0, 81 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 21 },
				new int[2] { 0, 82 },
				new int[2] { 2, 1 },
				new int[2] { 0, 37 },
				new int[2] { 0, 68 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 5 },
				new int[2] { 0, 84 },
				new int[2] { 0, 83 },
				new int[2] { 2, 1 },
				new int[2] { 0, 53 },
				new int[2] { 2, 1 },
				new int[2] { 0, 69 },
				new int[2] { 0, 85 }
			};
			ValTab10 = new int[127][]
			{
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 16 },
				new int[2] { 0, 1 },
				new int[2] { 10, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 17 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 32 },
				new int[2] { 0, 2 },
				new int[2] { 2, 1 },
				new int[2] { 0, 33 },
				new int[2] { 0, 18 },
				new int[2] { 28, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 34 },
				new int[2] { 0, 48 },
				new int[2] { 2, 1 },
				new int[2] { 0, 49 },
				new int[2] { 0, 19 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 3 },
				new int[2] { 0, 50 },
				new int[2] { 2, 1 },
				new int[2] { 0, 35 },
				new int[2] { 0, 64 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 65 },
				new int[2] { 0, 20 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 4 },
				new int[2] { 0, 51 },
				new int[2] { 2, 1 },
				new int[2] { 0, 66 },
				new int[2] { 0, 36 },
				new int[2] { 28, 1 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 80 },
				new int[2] { 0, 5 },
				new int[2] { 0, 96 },
				new int[2] { 2, 1 },
				new int[2] { 0, 97 },
				new int[2] { 0, 22 },
				new int[2] { 12, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 67 },
				new int[2] { 0, 52 },
				new int[2] { 0, 81 },
				new int[2] { 2, 1 },
				new int[2] { 0, 21 },
				new int[2] { 2, 1 },
				new int[2] { 0, 82 },
				new int[2] { 0, 37 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 38 },
				new int[2] { 0, 54 },
				new int[2] { 0, 113 },
				new int[2] { 20, 1 },
				new int[2] { 8, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 23 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 68 },
				new int[2] { 0, 83 },
				new int[2] { 0, 6 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 53 },
				new int[2] { 0, 69 },
				new int[2] { 0, 98 },
				new int[2] { 2, 1 },
				new int[2] { 0, 112 },
				new int[2] { 2, 1 },
				new int[2] { 0, 7 },
				new int[2] { 0, 100 },
				new int[2] { 14, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 114 },
				new int[2] { 0, 39 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 99 },
				new int[2] { 2, 1 },
				new int[2] { 0, 84 },
				new int[2] { 0, 85 },
				new int[2] { 2, 1 },
				new int[2] { 0, 70 },
				new int[2] { 0, 115 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 55 },
				new int[2] { 0, 101 },
				new int[2] { 2, 1 },
				new int[2] { 0, 86 },
				new int[2] { 0, 116 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 71 },
				new int[2] { 2, 1 },
				new int[2] { 0, 102 },
				new int[2] { 0, 117 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 87 },
				new int[2] { 0, 118 },
				new int[2] { 2, 1 },
				new int[2] { 0, 103 },
				new int[2] { 0, 119 }
			};
			ValTab11 = new int[127][]
			{
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 2, 1 },
				new int[2] { 0, 16 },
				new int[2] { 0, 1 },
				new int[2] { 8, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 17 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 32 },
				new int[2] { 0, 2 },
				new int[2] { 0, 18 },
				new int[2] { 24, 1 },
				new int[2] { 8, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 33 },
				new int[2] { 2, 1 },
				new int[2] { 0, 34 },
				new int[2] { 2, 1 },
				new int[2] { 0, 48 },
				new int[2] { 0, 3 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 49 },
				new int[2] { 0, 19 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 50 },
				new int[2] { 0, 35 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 64 },
				new int[2] { 0, 4 },
				new int[2] { 2, 1 },
				new int[2] { 0, 65 },
				new int[2] { 0, 20 },
				new int[2] { 30, 1 },
				new int[2] { 16, 1 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 66 },
				new int[2] { 0, 36 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 51 },
				new int[2] { 0, 67 },
				new int[2] { 0, 80 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 52 },
				new int[2] { 0, 81 },
				new int[2] { 0, 97 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 22 },
				new int[2] { 2, 1 },
				new int[2] { 0, 6 },
				new int[2] { 0, 38 },
				new int[2] { 2, 1 },
				new int[2] { 0, 98 },
				new int[2] { 2, 1 },
				new int[2] { 0, 21 },
				new int[2] { 2, 1 },
				new int[2] { 0, 5 },
				new int[2] { 0, 82 },
				new int[2] { 16, 1 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 37 },
				new int[2] { 0, 68 },
				new int[2] { 0, 96 },
				new int[2] { 2, 1 },
				new int[2] { 0, 99 },
				new int[2] { 0, 54 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 112 },
				new int[2] { 0, 23 },
				new int[2] { 0, 113 },
				new int[2] { 16, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 7 },
				new int[2] { 0, 100 },
				new int[2] { 0, 114 },
				new int[2] { 2, 1 },
				new int[2] { 0, 39 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 83 },
				new int[2] { 0, 53 },
				new int[2] { 2, 1 },
				new int[2] { 0, 84 },
				new int[2] { 0, 69 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 70 },
				new int[2] { 0, 115 },
				new int[2] { 2, 1 },
				new int[2] { 0, 55 },
				new int[2] { 2, 1 },
				new int[2] { 0, 101 },
				new int[2] { 0, 86 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 85 },
				new int[2] { 0, 87 },
				new int[2] { 0, 116 },
				new int[2] { 2, 1 },
				new int[2] { 0, 71 },
				new int[2] { 0, 102 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 117 },
				new int[2] { 0, 118 },
				new int[2] { 2, 1 },
				new int[2] { 0, 103 },
				new int[2] { 0, 119 }
			};
			ValTab12 = new int[127][]
			{
				new int[2] { 12, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 16 },
				new int[2] { 0, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 17 },
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 2, 1 },
				new int[2] { 0, 32 },
				new int[2] { 0, 2 },
				new int[2] { 16, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 33 },
				new int[2] { 0, 18 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 34 },
				new int[2] { 0, 49 },
				new int[2] { 2, 1 },
				new int[2] { 0, 19 },
				new int[2] { 2, 1 },
				new int[2] { 0, 48 },
				new int[2] { 2, 1 },
				new int[2] { 0, 3 },
				new int[2] { 0, 64 },
				new int[2] { 26, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 50 },
				new int[2] { 0, 35 },
				new int[2] { 2, 1 },
				new int[2] { 0, 65 },
				new int[2] { 0, 51 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 20 },
				new int[2] { 0, 66 },
				new int[2] { 2, 1 },
				new int[2] { 0, 36 },
				new int[2] { 2, 1 },
				new int[2] { 0, 4 },
				new int[2] { 0, 80 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 67 },
				new int[2] { 0, 52 },
				new int[2] { 2, 1 },
				new int[2] { 0, 81 },
				new int[2] { 0, 21 },
				new int[2] { 28, 1 },
				new int[2] { 14, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 82 },
				new int[2] { 0, 37 },
				new int[2] { 2, 1 },
				new int[2] { 0, 83 },
				new int[2] { 0, 53 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 96 },
				new int[2] { 0, 22 },
				new int[2] { 0, 97 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 98 },
				new int[2] { 0, 38 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 5 },
				new int[2] { 0, 6 },
				new int[2] { 0, 68 },
				new int[2] { 2, 1 },
				new int[2] { 0, 84 },
				new int[2] { 0, 69 },
				new int[2] { 18, 1 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 99 },
				new int[2] { 0, 54 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 112 },
				new int[2] { 0, 7 },
				new int[2] { 0, 113 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 23 },
				new int[2] { 0, 100 },
				new int[2] { 2, 1 },
				new int[2] { 0, 70 },
				new int[2] { 0, 114 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 39 },
				new int[2] { 2, 1 },
				new int[2] { 0, 85 },
				new int[2] { 0, 115 },
				new int[2] { 2, 1 },
				new int[2] { 0, 55 },
				new int[2] { 0, 86 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 101 },
				new int[2] { 0, 116 },
				new int[2] { 2, 1 },
				new int[2] { 0, 71 },
				new int[2] { 0, 102 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 117 },
				new int[2] { 0, 87 },
				new int[2] { 2, 1 },
				new int[2] { 0, 118 },
				new int[2] { 2, 1 },
				new int[2] { 0, 103 },
				new int[2] { 0, 119 }
			};
			ValTab13 = new int[511][]
			{
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 16 },
				new int[2] { 2, 1 },
				new int[2] { 0, 1 },
				new int[2] { 0, 17 },
				new int[2] { 28, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 32 },
				new int[2] { 0, 2 },
				new int[2] { 2, 1 },
				new int[2] { 0, 33 },
				new int[2] { 0, 18 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 34 },
				new int[2] { 0, 48 },
				new int[2] { 2, 1 },
				new int[2] { 0, 3 },
				new int[2] { 0, 49 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 19 },
				new int[2] { 2, 1 },
				new int[2] { 0, 50 },
				new int[2] { 0, 35 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 64 },
				new int[2] { 0, 4 },
				new int[2] { 0, 65 },
				new int[2] { 70, 1 },
				new int[2] { 28, 1 },
				new int[2] { 14, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 20 },
				new int[2] { 2, 1 },
				new int[2] { 0, 51 },
				new int[2] { 0, 66 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 36 },
				new int[2] { 0, 80 },
				new int[2] { 2, 1 },
				new int[2] { 0, 67 },
				new int[2] { 0, 52 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 81 },
				new int[2] { 0, 21 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 5 },
				new int[2] { 0, 82 },
				new int[2] { 2, 1 },
				new int[2] { 0, 37 },
				new int[2] { 2, 1 },
				new int[2] { 0, 68 },
				new int[2] { 0, 83 },
				new int[2] { 14, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 96 },
				new int[2] { 0, 6 },
				new int[2] { 2, 1 },
				new int[2] { 0, 97 },
				new int[2] { 0, 22 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 128 },
				new int[2] { 0, 8 },
				new int[2] { 0, 129 },
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 53 },
				new int[2] { 0, 98 },
				new int[2] { 2, 1 },
				new int[2] { 0, 38 },
				new int[2] { 0, 84 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 69 },
				new int[2] { 0, 99 },
				new int[2] { 2, 1 },
				new int[2] { 0, 54 },
				new int[2] { 0, 112 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 7 },
				new int[2] { 0, 85 },
				new int[2] { 0, 113 },
				new int[2] { 2, 1 },
				new int[2] { 0, 23 },
				new int[2] { 2, 1 },
				new int[2] { 0, 39 },
				new int[2] { 0, 55 },
				new int[2] { 72, 1 },
				new int[2] { 24, 1 },
				new int[2] { 12, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 24 },
				new int[2] { 0, 130 },
				new int[2] { 2, 1 },
				new int[2] { 0, 40 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 100 },
				new int[2] { 0, 70 },
				new int[2] { 0, 114 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 132 },
				new int[2] { 0, 72 },
				new int[2] { 2, 1 },
				new int[2] { 0, 144 },
				new int[2] { 0, 9 },
				new int[2] { 2, 1 },
				new int[2] { 0, 145 },
				new int[2] { 0, 25 },
				new int[2] { 24, 1 },
				new int[2] { 14, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 115 },
				new int[2] { 0, 101 },
				new int[2] { 2, 1 },
				new int[2] { 0, 86 },
				new int[2] { 0, 116 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 71 },
				new int[2] { 0, 102 },
				new int[2] { 0, 131 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 56 },
				new int[2] { 2, 1 },
				new int[2] { 0, 117 },
				new int[2] { 0, 87 },
				new int[2] { 2, 1 },
				new int[2] { 0, 146 },
				new int[2] { 0, 41 },
				new int[2] { 14, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 103 },
				new int[2] { 0, 133 },
				new int[2] { 2, 1 },
				new int[2] { 0, 88 },
				new int[2] { 0, 57 },
				new int[2] { 2, 1 },
				new int[2] { 0, 147 },
				new int[2] { 2, 1 },
				new int[2] { 0, 73 },
				new int[2] { 0, 134 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 160 },
				new int[2] { 2, 1 },
				new int[2] { 0, 104 },
				new int[2] { 0, 10 },
				new int[2] { 2, 1 },
				new int[2] { 0, 161 },
				new int[2] { 0, 26 },
				new int[2] { 68, 1 },
				new int[2] { 24, 1 },
				new int[2] { 12, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 162 },
				new int[2] { 0, 42 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 149 },
				new int[2] { 0, 89 },
				new int[2] { 2, 1 },
				new int[2] { 0, 163 },
				new int[2] { 0, 58 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 74 },
				new int[2] { 0, 150 },
				new int[2] { 2, 1 },
				new int[2] { 0, 176 },
				new int[2] { 0, 11 },
				new int[2] { 2, 1 },
				new int[2] { 0, 177 },
				new int[2] { 0, 27 },
				new int[2] { 20, 1 },
				new int[2] { 8, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 178 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 118 },
				new int[2] { 0, 119 },
				new int[2] { 0, 148 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 135 },
				new int[2] { 0, 120 },
				new int[2] { 0, 164 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 105 },
				new int[2] { 0, 165 },
				new int[2] { 0, 43 },
				new int[2] { 12, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 90 },
				new int[2] { 0, 136 },
				new int[2] { 0, 179 },
				new int[2] { 2, 1 },
				new int[2] { 0, 59 },
				new int[2] { 2, 1 },
				new int[2] { 0, 121 },
				new int[2] { 0, 166 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 106 },
				new int[2] { 0, 180 },
				new int[2] { 0, 192 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 12 },
				new int[2] { 0, 152 },
				new int[2] { 0, 193 },
				new int[2] { 60, 1 },
				new int[2] { 22, 1 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 28 },
				new int[2] { 2, 1 },
				new int[2] { 0, 137 },
				new int[2] { 0, 181 },
				new int[2] { 2, 1 },
				new int[2] { 0, 91 },
				new int[2] { 0, 194 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 44 },
				new int[2] { 0, 60 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 182 },
				new int[2] { 0, 107 },
				new int[2] { 2, 1 },
				new int[2] { 0, 196 },
				new int[2] { 0, 76 },
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 168 },
				new int[2] { 0, 138 },
				new int[2] { 2, 1 },
				new int[2] { 0, 208 },
				new int[2] { 0, 13 },
				new int[2] { 2, 1 },
				new int[2] { 0, 209 },
				new int[2] { 2, 1 },
				new int[2] { 0, 75 },
				new int[2] { 2, 1 },
				new int[2] { 0, 151 },
				new int[2] { 0, 167 },
				new int[2] { 12, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 195 },
				new int[2] { 2, 1 },
				new int[2] { 0, 122 },
				new int[2] { 0, 153 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 197 },
				new int[2] { 0, 92 },
				new int[2] { 0, 183 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 29 },
				new int[2] { 0, 210 },
				new int[2] { 2, 1 },
				new int[2] { 0, 45 },
				new int[2] { 2, 1 },
				new int[2] { 0, 123 },
				new int[2] { 0, 211 },
				new int[2] { 52, 1 },
				new int[2] { 28, 1 },
				new int[2] { 12, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 61 },
				new int[2] { 0, 198 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 108 },
				new int[2] { 0, 169 },
				new int[2] { 2, 1 },
				new int[2] { 0, 154 },
				new int[2] { 0, 212 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 184 },
				new int[2] { 0, 139 },
				new int[2] { 2, 1 },
				new int[2] { 0, 77 },
				new int[2] { 0, 199 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 124 },
				new int[2] { 0, 213 },
				new int[2] { 2, 1 },
				new int[2] { 0, 93 },
				new int[2] { 0, 224 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 225 },
				new int[2] { 0, 30 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 14 },
				new int[2] { 0, 46 },
				new int[2] { 0, 226 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 227 },
				new int[2] { 0, 109 },
				new int[2] { 2, 1 },
				new int[2] { 0, 140 },
				new int[2] { 0, 228 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 229 },
				new int[2] { 0, 186 },
				new int[2] { 0, 240 },
				new int[2] { 38, 1 },
				new int[2] { 16, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 241 },
				new int[2] { 0, 31 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 170 },
				new int[2] { 0, 155 },
				new int[2] { 0, 185 },
				new int[2] { 2, 1 },
				new int[2] { 0, 62 },
				new int[2] { 2, 1 },
				new int[2] { 0, 214 },
				new int[2] { 0, 200 },
				new int[2] { 12, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 78 },
				new int[2] { 2, 1 },
				new int[2] { 0, 215 },
				new int[2] { 0, 125 },
				new int[2] { 2, 1 },
				new int[2] { 0, 171 },
				new int[2] { 2, 1 },
				new int[2] { 0, 94 },
				new int[2] { 0, 201 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 15 },
				new int[2] { 2, 1 },
				new int[2] { 0, 156 },
				new int[2] { 0, 110 },
				new int[2] { 2, 1 },
				new int[2] { 0, 242 },
				new int[2] { 0, 47 },
				new int[2] { 32, 1 },
				new int[2] { 16, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 216 },
				new int[2] { 0, 141 },
				new int[2] { 0, 63 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 243 },
				new int[2] { 2, 1 },
				new int[2] { 0, 230 },
				new int[2] { 0, 202 },
				new int[2] { 2, 1 },
				new int[2] { 0, 244 },
				new int[2] { 0, 79 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 187 },
				new int[2] { 0, 172 },
				new int[2] { 2, 1 },
				new int[2] { 0, 231 },
				new int[2] { 0, 245 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 217 },
				new int[2] { 0, 157 },
				new int[2] { 2, 1 },
				new int[2] { 0, 95 },
				new int[2] { 0, 232 },
				new int[2] { 30, 1 },
				new int[2] { 12, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 111 },
				new int[2] { 2, 1 },
				new int[2] { 0, 246 },
				new int[2] { 0, 203 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 188 },
				new int[2] { 0, 173 },
				new int[2] { 0, 218 },
				new int[2] { 8, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 247 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 126 },
				new int[2] { 0, 127 },
				new int[2] { 0, 142 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 158 },
				new int[2] { 0, 174 },
				new int[2] { 0, 204 },
				new int[2] { 2, 1 },
				new int[2] { 0, 248 },
				new int[2] { 0, 143 },
				new int[2] { 18, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 219 },
				new int[2] { 0, 189 },
				new int[2] { 2, 1 },
				new int[2] { 0, 234 },
				new int[2] { 0, 249 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 159 },
				new int[2] { 0, 235 },
				new int[2] { 2, 1 },
				new int[2] { 0, 190 },
				new int[2] { 2, 1 },
				new int[2] { 0, 205 },
				new int[2] { 0, 250 },
				new int[2] { 14, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 221 },
				new int[2] { 0, 236 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 233 },
				new int[2] { 0, 175 },
				new int[2] { 0, 220 },
				new int[2] { 2, 1 },
				new int[2] { 0, 206 },
				new int[2] { 0, 251 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 191 },
				new int[2] { 0, 222 },
				new int[2] { 2, 1 },
				new int[2] { 0, 207 },
				new int[2] { 0, 238 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 223 },
				new int[2] { 0, 239 },
				new int[2] { 2, 1 },
				new int[2] { 0, 255 },
				new int[2] { 2, 1 },
				new int[2] { 0, 237 },
				new int[2] { 2, 1 },
				new int[2] { 0, 253 },
				new int[2] { 2, 1 },
				new int[2] { 0, 252 },
				new int[2] { 0, 254 }
			};
			ValTab14 = new int[1][] { new int[2] };
			ValTab15 = new int[511][]
			{
				new int[2] { 16, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 2, 1 },
				new int[2] { 0, 16 },
				new int[2] { 0, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 17 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 32 },
				new int[2] { 0, 2 },
				new int[2] { 2, 1 },
				new int[2] { 0, 33 },
				new int[2] { 0, 18 },
				new int[2] { 50, 1 },
				new int[2] { 16, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 34 },
				new int[2] { 2, 1 },
				new int[2] { 0, 48 },
				new int[2] { 0, 49 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 19 },
				new int[2] { 2, 1 },
				new int[2] { 0, 3 },
				new int[2] { 0, 64 },
				new int[2] { 2, 1 },
				new int[2] { 0, 50 },
				new int[2] { 0, 35 },
				new int[2] { 14, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 4 },
				new int[2] { 0, 20 },
				new int[2] { 0, 65 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 51 },
				new int[2] { 0, 66 },
				new int[2] { 2, 1 },
				new int[2] { 0, 36 },
				new int[2] { 0, 67 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 52 },
				new int[2] { 2, 1 },
				new int[2] { 0, 80 },
				new int[2] { 0, 5 },
				new int[2] { 2, 1 },
				new int[2] { 0, 81 },
				new int[2] { 0, 21 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 82 },
				new int[2] { 0, 37 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 68 },
				new int[2] { 0, 83 },
				new int[2] { 0, 97 },
				new int[2] { 90, 1 },
				new int[2] { 36, 1 },
				new int[2] { 18, 1 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 53 },
				new int[2] { 2, 1 },
				new int[2] { 0, 96 },
				new int[2] { 0, 6 },
				new int[2] { 2, 1 },
				new int[2] { 0, 22 },
				new int[2] { 0, 98 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 38 },
				new int[2] { 0, 84 },
				new int[2] { 2, 1 },
				new int[2] { 0, 69 },
				new int[2] { 0, 99 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 54 },
				new int[2] { 2, 1 },
				new int[2] { 0, 112 },
				new int[2] { 0, 7 },
				new int[2] { 2, 1 },
				new int[2] { 0, 113 },
				new int[2] { 0, 85 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 23 },
				new int[2] { 0, 100 },
				new int[2] { 2, 1 },
				new int[2] { 0, 114 },
				new int[2] { 0, 39 },
				new int[2] { 24, 1 },
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 70 },
				new int[2] { 0, 115 },
				new int[2] { 2, 1 },
				new int[2] { 0, 55 },
				new int[2] { 0, 101 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 86 },
				new int[2] { 0, 128 },
				new int[2] { 2, 1 },
				new int[2] { 0, 8 },
				new int[2] { 0, 116 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 129 },
				new int[2] { 0, 24 },
				new int[2] { 2, 1 },
				new int[2] { 0, 130 },
				new int[2] { 0, 40 },
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 71 },
				new int[2] { 0, 102 },
				new int[2] { 2, 1 },
				new int[2] { 0, 131 },
				new int[2] { 0, 56 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 117 },
				new int[2] { 0, 87 },
				new int[2] { 2, 1 },
				new int[2] { 0, 132 },
				new int[2] { 0, 72 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 144 },
				new int[2] { 0, 25 },
				new int[2] { 0, 145 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 146 },
				new int[2] { 0, 118 },
				new int[2] { 2, 1 },
				new int[2] { 0, 103 },
				new int[2] { 0, 41 },
				new int[2] { 92, 1 },
				new int[2] { 36, 1 },
				new int[2] { 18, 1 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 133 },
				new int[2] { 0, 88 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 9 },
				new int[2] { 0, 119 },
				new int[2] { 0, 147 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 57 },
				new int[2] { 0, 148 },
				new int[2] { 2, 1 },
				new int[2] { 0, 73 },
				new int[2] { 0, 134 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 104 },
				new int[2] { 2, 1 },
				new int[2] { 0, 160 },
				new int[2] { 0, 10 },
				new int[2] { 2, 1 },
				new int[2] { 0, 161 },
				new int[2] { 0, 26 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 162 },
				new int[2] { 0, 42 },
				new int[2] { 2, 1 },
				new int[2] { 0, 149 },
				new int[2] { 0, 89 },
				new int[2] { 26, 1 },
				new int[2] { 14, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 163 },
				new int[2] { 2, 1 },
				new int[2] { 0, 58 },
				new int[2] { 0, 135 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 120 },
				new int[2] { 0, 164 },
				new int[2] { 2, 1 },
				new int[2] { 0, 74 },
				new int[2] { 0, 150 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 105 },
				new int[2] { 0, 176 },
				new int[2] { 0, 177 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 27 },
				new int[2] { 0, 165 },
				new int[2] { 0, 178 },
				new int[2] { 14, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 90 },
				new int[2] { 0, 43 },
				new int[2] { 2, 1 },
				new int[2] { 0, 136 },
				new int[2] { 0, 151 },
				new int[2] { 2, 1 },
				new int[2] { 0, 179 },
				new int[2] { 2, 1 },
				new int[2] { 0, 121 },
				new int[2] { 0, 59 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 106 },
				new int[2] { 0, 180 },
				new int[2] { 2, 1 },
				new int[2] { 0, 75 },
				new int[2] { 0, 193 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 152 },
				new int[2] { 0, 137 },
				new int[2] { 2, 1 },
				new int[2] { 0, 28 },
				new int[2] { 0, 181 },
				new int[2] { 80, 1 },
				new int[2] { 34, 1 },
				new int[2] { 16, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 91 },
				new int[2] { 0, 44 },
				new int[2] { 0, 194 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 11 },
				new int[2] { 0, 192 },
				new int[2] { 0, 166 },
				new int[2] { 2, 1 },
				new int[2] { 0, 167 },
				new int[2] { 0, 122 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 195 },
				new int[2] { 0, 60 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 12 },
				new int[2] { 0, 153 },
				new int[2] { 0, 182 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 107 },
				new int[2] { 0, 196 },
				new int[2] { 2, 1 },
				new int[2] { 0, 76 },
				new int[2] { 0, 168 },
				new int[2] { 20, 1 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 138 },
				new int[2] { 0, 197 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 208 },
				new int[2] { 0, 92 },
				new int[2] { 0, 209 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 183 },
				new int[2] { 0, 123 },
				new int[2] { 2, 1 },
				new int[2] { 0, 29 },
				new int[2] { 2, 1 },
				new int[2] { 0, 13 },
				new int[2] { 0, 45 },
				new int[2] { 12, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 210 },
				new int[2] { 0, 211 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 61 },
				new int[2] { 0, 198 },
				new int[2] { 2, 1 },
				new int[2] { 0, 108 },
				new int[2] { 0, 169 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 154 },
				new int[2] { 0, 184 },
				new int[2] { 0, 212 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 139 },
				new int[2] { 0, 77 },
				new int[2] { 2, 1 },
				new int[2] { 0, 199 },
				new int[2] { 0, 124 },
				new int[2] { 68, 1 },
				new int[2] { 34, 1 },
				new int[2] { 18, 1 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 213 },
				new int[2] { 0, 93 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 224 },
				new int[2] { 0, 14 },
				new int[2] { 0, 225 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 30 },
				new int[2] { 0, 226 },
				new int[2] { 2, 1 },
				new int[2] { 0, 170 },
				new int[2] { 0, 46 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 185 },
				new int[2] { 0, 155 },
				new int[2] { 2, 1 },
				new int[2] { 0, 227 },
				new int[2] { 0, 214 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 109 },
				new int[2] { 0, 62 },
				new int[2] { 2, 1 },
				new int[2] { 0, 200 },
				new int[2] { 0, 140 },
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 228 },
				new int[2] { 0, 78 },
				new int[2] { 2, 1 },
				new int[2] { 0, 215 },
				new int[2] { 0, 125 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 229 },
				new int[2] { 0, 186 },
				new int[2] { 2, 1 },
				new int[2] { 0, 171 },
				new int[2] { 0, 94 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 201 },
				new int[2] { 0, 156 },
				new int[2] { 2, 1 },
				new int[2] { 0, 241 },
				new int[2] { 0, 31 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 240 },
				new int[2] { 0, 110 },
				new int[2] { 0, 242 },
				new int[2] { 2, 1 },
				new int[2] { 0, 47 },
				new int[2] { 0, 230 },
				new int[2] { 38, 1 },
				new int[2] { 18, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 216 },
				new int[2] { 0, 243 },
				new int[2] { 2, 1 },
				new int[2] { 0, 63 },
				new int[2] { 0, 244 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 79 },
				new int[2] { 2, 1 },
				new int[2] { 0, 141 },
				new int[2] { 0, 217 },
				new int[2] { 2, 1 },
				new int[2] { 0, 187 },
				new int[2] { 0, 202 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 172 },
				new int[2] { 0, 231 },
				new int[2] { 2, 1 },
				new int[2] { 0, 126 },
				new int[2] { 0, 245 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 157 },
				new int[2] { 0, 95 },
				new int[2] { 2, 1 },
				new int[2] { 0, 232 },
				new int[2] { 0, 142 },
				new int[2] { 2, 1 },
				new int[2] { 0, 246 },
				new int[2] { 0, 203 },
				new int[2] { 34, 1 },
				new int[2] { 18, 1 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 15 },
				new int[2] { 0, 174 },
				new int[2] { 0, 111 },
				new int[2] { 2, 1 },
				new int[2] { 0, 188 },
				new int[2] { 0, 218 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 173 },
				new int[2] { 0, 247 },
				new int[2] { 2, 1 },
				new int[2] { 0, 127 },
				new int[2] { 0, 233 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 158 },
				new int[2] { 0, 204 },
				new int[2] { 2, 1 },
				new int[2] { 0, 248 },
				new int[2] { 0, 143 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 219 },
				new int[2] { 0, 189 },
				new int[2] { 2, 1 },
				new int[2] { 0, 234 },
				new int[2] { 0, 249 },
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 159 },
				new int[2] { 0, 220 },
				new int[2] { 2, 1 },
				new int[2] { 0, 205 },
				new int[2] { 0, 235 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 190 },
				new int[2] { 0, 250 },
				new int[2] { 2, 1 },
				new int[2] { 0, 175 },
				new int[2] { 0, 221 },
				new int[2] { 14, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 236 },
				new int[2] { 0, 206 },
				new int[2] { 0, 251 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 191 },
				new int[2] { 0, 237 },
				new int[2] { 2, 1 },
				new int[2] { 0, 222 },
				new int[2] { 0, 252 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 207 },
				new int[2] { 0, 253 },
				new int[2] { 0, 238 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 223 },
				new int[2] { 0, 254 },
				new int[2] { 2, 1 },
				new int[2] { 0, 239 },
				new int[2] { 0, 255 }
			};
			ValTab16 = new int[511][]
			{
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 16 },
				new int[2] { 2, 1 },
				new int[2] { 0, 1 },
				new int[2] { 0, 17 },
				new int[2] { 42, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 32 },
				new int[2] { 0, 2 },
				new int[2] { 2, 1 },
				new int[2] { 0, 33 },
				new int[2] { 0, 18 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 34 },
				new int[2] { 2, 1 },
				new int[2] { 0, 48 },
				new int[2] { 0, 3 },
				new int[2] { 2, 1 },
				new int[2] { 0, 49 },
				new int[2] { 0, 19 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 50 },
				new int[2] { 0, 35 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 64 },
				new int[2] { 0, 4 },
				new int[2] { 0, 65 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 20 },
				new int[2] { 2, 1 },
				new int[2] { 0, 51 },
				new int[2] { 0, 66 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 36 },
				new int[2] { 0, 80 },
				new int[2] { 2, 1 },
				new int[2] { 0, 67 },
				new int[2] { 0, 52 },
				new int[2] { 138, 1 },
				new int[2] { 40, 1 },
				new int[2] { 16, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 5 },
				new int[2] { 0, 21 },
				new int[2] { 0, 81 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 82 },
				new int[2] { 0, 37 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 68 },
				new int[2] { 0, 53 },
				new int[2] { 0, 83 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 96 },
				new int[2] { 0, 6 },
				new int[2] { 0, 97 },
				new int[2] { 2, 1 },
				new int[2] { 0, 22 },
				new int[2] { 0, 98 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 38 },
				new int[2] { 0, 84 },
				new int[2] { 2, 1 },
				new int[2] { 0, 69 },
				new int[2] { 0, 99 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 54 },
				new int[2] { 0, 112 },
				new int[2] { 0, 113 },
				new int[2] { 40, 1 },
				new int[2] { 18, 1 },
				new int[2] { 8, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 23 },
				new int[2] { 2, 1 },
				new int[2] { 0, 7 },
				new int[2] { 2, 1 },
				new int[2] { 0, 85 },
				new int[2] { 0, 100 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 114 },
				new int[2] { 0, 39 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 70 },
				new int[2] { 0, 101 },
				new int[2] { 0, 115 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 55 },
				new int[2] { 2, 1 },
				new int[2] { 0, 86 },
				new int[2] { 0, 8 },
				new int[2] { 2, 1 },
				new int[2] { 0, 128 },
				new int[2] { 0, 129 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 24 },
				new int[2] { 2, 1 },
				new int[2] { 0, 116 },
				new int[2] { 0, 71 },
				new int[2] { 2, 1 },
				new int[2] { 0, 130 },
				new int[2] { 2, 1 },
				new int[2] { 0, 40 },
				new int[2] { 0, 102 },
				new int[2] { 24, 1 },
				new int[2] { 14, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 131 },
				new int[2] { 0, 56 },
				new int[2] { 2, 1 },
				new int[2] { 0, 117 },
				new int[2] { 0, 132 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 72 },
				new int[2] { 0, 144 },
				new int[2] { 0, 145 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 25 },
				new int[2] { 2, 1 },
				new int[2] { 0, 9 },
				new int[2] { 0, 118 },
				new int[2] { 2, 1 },
				new int[2] { 0, 146 },
				new int[2] { 0, 41 },
				new int[2] { 14, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 133 },
				new int[2] { 0, 88 },
				new int[2] { 2, 1 },
				new int[2] { 0, 147 },
				new int[2] { 0, 57 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 160 },
				new int[2] { 0, 10 },
				new int[2] { 0, 26 },
				new int[2] { 8, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 162 },
				new int[2] { 2, 1 },
				new int[2] { 0, 103 },
				new int[2] { 2, 1 },
				new int[2] { 0, 87 },
				new int[2] { 0, 73 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 148 },
				new int[2] { 2, 1 },
				new int[2] { 0, 119 },
				new int[2] { 0, 134 },
				new int[2] { 2, 1 },
				new int[2] { 0, 161 },
				new int[2] { 2, 1 },
				new int[2] { 0, 104 },
				new int[2] { 0, 149 },
				new int[2] { 220, 1 },
				new int[2] { 126, 1 },
				new int[2] { 50, 1 },
				new int[2] { 26, 1 },
				new int[2] { 12, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 42 },
				new int[2] { 2, 1 },
				new int[2] { 0, 89 },
				new int[2] { 0, 58 },
				new int[2] { 2, 1 },
				new int[2] { 0, 163 },
				new int[2] { 2, 1 },
				new int[2] { 0, 135 },
				new int[2] { 0, 120 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 164 },
				new int[2] { 0, 74 },
				new int[2] { 2, 1 },
				new int[2] { 0, 150 },
				new int[2] { 0, 105 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 176 },
				new int[2] { 0, 11 },
				new int[2] { 0, 177 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 27 },
				new int[2] { 0, 178 },
				new int[2] { 2, 1 },
				new int[2] { 0, 43 },
				new int[2] { 2, 1 },
				new int[2] { 0, 165 },
				new int[2] { 0, 90 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 179 },
				new int[2] { 2, 1 },
				new int[2] { 0, 166 },
				new int[2] { 0, 106 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 180 },
				new int[2] { 0, 75 },
				new int[2] { 2, 1 },
				new int[2] { 0, 12 },
				new int[2] { 0, 193 },
				new int[2] { 30, 1 },
				new int[2] { 14, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 181 },
				new int[2] { 0, 194 },
				new int[2] { 0, 44 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 167 },
				new int[2] { 0, 195 },
				new int[2] { 2, 1 },
				new int[2] { 0, 107 },
				new int[2] { 0, 196 },
				new int[2] { 8, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 29 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 136 },
				new int[2] { 0, 151 },
				new int[2] { 0, 59 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 209 },
				new int[2] { 0, 210 },
				new int[2] { 2, 1 },
				new int[2] { 0, 45 },
				new int[2] { 0, 211 },
				new int[2] { 18, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 30 },
				new int[2] { 0, 46 },
				new int[2] { 0, 226 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 121 },
				new int[2] { 0, 152 },
				new int[2] { 0, 192 },
				new int[2] { 2, 1 },
				new int[2] { 0, 28 },
				new int[2] { 2, 1 },
				new int[2] { 0, 137 },
				new int[2] { 0, 91 },
				new int[2] { 14, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 60 },
				new int[2] { 2, 1 },
				new int[2] { 0, 122 },
				new int[2] { 0, 182 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 76 },
				new int[2] { 0, 153 },
				new int[2] { 2, 1 },
				new int[2] { 0, 168 },
				new int[2] { 0, 138 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 13 },
				new int[2] { 2, 1 },
				new int[2] { 0, 197 },
				new int[2] { 0, 92 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 61 },
				new int[2] { 0, 198 },
				new int[2] { 2, 1 },
				new int[2] { 0, 108 },
				new int[2] { 0, 154 },
				new int[2] { 88, 1 },
				new int[2] { 86, 1 },
				new int[2] { 36, 1 },
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 139 },
				new int[2] { 0, 77 },
				new int[2] { 2, 1 },
				new int[2] { 0, 199 },
				new int[2] { 0, 124 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 213 },
				new int[2] { 0, 93 },
				new int[2] { 2, 1 },
				new int[2] { 0, 224 },
				new int[2] { 0, 14 },
				new int[2] { 8, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 227 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 208 },
				new int[2] { 0, 183 },
				new int[2] { 0, 123 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 169 },
				new int[2] { 0, 184 },
				new int[2] { 0, 212 },
				new int[2] { 2, 1 },
				new int[2] { 0, 225 },
				new int[2] { 2, 1 },
				new int[2] { 0, 170 },
				new int[2] { 0, 185 },
				new int[2] { 24, 1 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 155 },
				new int[2] { 0, 214 },
				new int[2] { 0, 109 },
				new int[2] { 2, 1 },
				new int[2] { 0, 62 },
				new int[2] { 0, 200 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 140 },
				new int[2] { 0, 228 },
				new int[2] { 0, 78 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 215 },
				new int[2] { 0, 229 },
				new int[2] { 2, 1 },
				new int[2] { 0, 186 },
				new int[2] { 0, 171 },
				new int[2] { 12, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 156 },
				new int[2] { 0, 230 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 110 },
				new int[2] { 0, 216 },
				new int[2] { 2, 1 },
				new int[2] { 0, 141 },
				new int[2] { 0, 187 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 231 },
				new int[2] { 0, 157 },
				new int[2] { 2, 1 },
				new int[2] { 0, 232 },
				new int[2] { 0, 142 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 203 },
				new int[2] { 0, 188 },
				new int[2] { 0, 158 },
				new int[2] { 0, 241 },
				new int[2] { 2, 1 },
				new int[2] { 0, 31 },
				new int[2] { 2, 1 },
				new int[2] { 0, 15 },
				new int[2] { 0, 47 },
				new int[2] { 66, 1 },
				new int[2] { 56, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 242 },
				new int[2] { 52, 1 },
				new int[2] { 50, 1 },
				new int[2] { 20, 1 },
				new int[2] { 8, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 189 },
				new int[2] { 2, 1 },
				new int[2] { 0, 94 },
				new int[2] { 2, 1 },
				new int[2] { 0, 125 },
				new int[2] { 0, 201 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 202 },
				new int[2] { 2, 1 },
				new int[2] { 0, 172 },
				new int[2] { 0, 126 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 218 },
				new int[2] { 0, 173 },
				new int[2] { 0, 204 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 174 },
				new int[2] { 2, 1 },
				new int[2] { 0, 219 },
				new int[2] { 0, 220 },
				new int[2] { 2, 1 },
				new int[2] { 0, 205 },
				new int[2] { 0, 190 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 235 },
				new int[2] { 0, 237 },
				new int[2] { 0, 238 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 217 },
				new int[2] { 0, 234 },
				new int[2] { 0, 233 },
				new int[2] { 2, 1 },
				new int[2] { 0, 222 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 221 },
				new int[2] { 0, 236 },
				new int[2] { 0, 206 },
				new int[2] { 0, 63 },
				new int[2] { 0, 240 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 243 },
				new int[2] { 0, 244 },
				new int[2] { 2, 1 },
				new int[2] { 0, 79 },
				new int[2] { 2, 1 },
				new int[2] { 0, 245 },
				new int[2] { 0, 95 },
				new int[2] { 10, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 255 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 246 },
				new int[2] { 0, 111 },
				new int[2] { 2, 1 },
				new int[2] { 0, 247 },
				new int[2] { 0, 127 },
				new int[2] { 12, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 143 },
				new int[2] { 2, 1 },
				new int[2] { 0, 248 },
				new int[2] { 0, 249 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 159 },
				new int[2] { 0, 250 },
				new int[2] { 0, 175 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 251 },
				new int[2] { 0, 191 },
				new int[2] { 2, 1 },
				new int[2] { 0, 252 },
				new int[2] { 0, 207 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 253 },
				new int[2] { 0, 223 },
				new int[2] { 2, 1 },
				new int[2] { 0, 254 },
				new int[2] { 0, 239 }
			};
			ValTab24 = new int[512][]
			{
				new int[2] { 60, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 0, 16 },
				new int[2] { 2, 1 },
				new int[2] { 0, 1 },
				new int[2] { 0, 17 },
				new int[2] { 14, 1 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 32 },
				new int[2] { 0, 2 },
				new int[2] { 0, 33 },
				new int[2] { 2, 1 },
				new int[2] { 0, 18 },
				new int[2] { 2, 1 },
				new int[2] { 0, 34 },
				new int[2] { 2, 1 },
				new int[2] { 0, 48 },
				new int[2] { 0, 3 },
				new int[2] { 14, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 49 },
				new int[2] { 0, 19 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 50 },
				new int[2] { 0, 35 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 64 },
				new int[2] { 0, 4 },
				new int[2] { 0, 65 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 20 },
				new int[2] { 0, 51 },
				new int[2] { 2, 1 },
				new int[2] { 0, 66 },
				new int[2] { 0, 36 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 67 },
				new int[2] { 0, 52 },
				new int[2] { 0, 81 },
				new int[2] { 6, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 80 },
				new int[2] { 0, 5 },
				new int[2] { 0, 21 },
				new int[2] { 2, 1 },
				new int[2] { 0, 82 },
				new int[2] { 0, 37 },
				new int[2] { 250, 1 },
				new int[2] { 98, 1 },
				new int[2] { 34, 1 },
				new int[2] { 18, 1 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 68 },
				new int[2] { 0, 83 },
				new int[2] { 2, 1 },
				new int[2] { 0, 53 },
				new int[2] { 2, 1 },
				new int[2] { 0, 96 },
				new int[2] { 0, 6 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 97 },
				new int[2] { 0, 22 },
				new int[2] { 2, 1 },
				new int[2] { 0, 98 },
				new int[2] { 0, 38 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 84 },
				new int[2] { 0, 69 },
				new int[2] { 2, 1 },
				new int[2] { 0, 99 },
				new int[2] { 0, 54 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 113 },
				new int[2] { 0, 85 },
				new int[2] { 2, 1 },
				new int[2] { 0, 100 },
				new int[2] { 0, 70 },
				new int[2] { 32, 1 },
				new int[2] { 14, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 114 },
				new int[2] { 2, 1 },
				new int[2] { 0, 39 },
				new int[2] { 0, 55 },
				new int[2] { 2, 1 },
				new int[2] { 0, 115 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 112 },
				new int[2] { 0, 7 },
				new int[2] { 0, 23 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 101 },
				new int[2] { 0, 86 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 128 },
				new int[2] { 0, 8 },
				new int[2] { 0, 129 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 116 },
				new int[2] { 0, 71 },
				new int[2] { 2, 1 },
				new int[2] { 0, 24 },
				new int[2] { 0, 130 },
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 40 },
				new int[2] { 0, 102 },
				new int[2] { 2, 1 },
				new int[2] { 0, 131 },
				new int[2] { 0, 56 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 117 },
				new int[2] { 0, 87 },
				new int[2] { 2, 1 },
				new int[2] { 0, 132 },
				new int[2] { 0, 72 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 145 },
				new int[2] { 0, 25 },
				new int[2] { 2, 1 },
				new int[2] { 0, 146 },
				new int[2] { 0, 118 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 103 },
				new int[2] { 0, 41 },
				new int[2] { 2, 1 },
				new int[2] { 0, 133 },
				new int[2] { 0, 88 },
				new int[2] { 92, 1 },
				new int[2] { 34, 1 },
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 147 },
				new int[2] { 0, 57 },
				new int[2] { 2, 1 },
				new int[2] { 0, 148 },
				new int[2] { 0, 73 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 119 },
				new int[2] { 0, 134 },
				new int[2] { 2, 1 },
				new int[2] { 0, 104 },
				new int[2] { 0, 161 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 162 },
				new int[2] { 0, 42 },
				new int[2] { 2, 1 },
				new int[2] { 0, 149 },
				new int[2] { 0, 89 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 163 },
				new int[2] { 0, 58 },
				new int[2] { 2, 1 },
				new int[2] { 0, 135 },
				new int[2] { 2, 1 },
				new int[2] { 0, 120 },
				new int[2] { 0, 74 },
				new int[2] { 22, 1 },
				new int[2] { 12, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 164 },
				new int[2] { 0, 150 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 105 },
				new int[2] { 0, 177 },
				new int[2] { 2, 1 },
				new int[2] { 0, 27 },
				new int[2] { 0, 165 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 178 },
				new int[2] { 2, 1 },
				new int[2] { 0, 90 },
				new int[2] { 0, 43 },
				new int[2] { 2, 1 },
				new int[2] { 0, 136 },
				new int[2] { 0, 179 },
				new int[2] { 16, 1 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 144 },
				new int[2] { 2, 1 },
				new int[2] { 0, 9 },
				new int[2] { 0, 160 },
				new int[2] { 2, 1 },
				new int[2] { 0, 151 },
				new int[2] { 0, 121 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 166 },
				new int[2] { 0, 106 },
				new int[2] { 0, 180 },
				new int[2] { 12, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 26 },
				new int[2] { 2, 1 },
				new int[2] { 0, 10 },
				new int[2] { 0, 176 },
				new int[2] { 2, 1 },
				new int[2] { 0, 59 },
				new int[2] { 2, 1 },
				new int[2] { 0, 11 },
				new int[2] { 0, 192 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 75 },
				new int[2] { 0, 193 },
				new int[2] { 2, 1 },
				new int[2] { 0, 152 },
				new int[2] { 0, 137 },
				new int[2] { 67, 1 },
				new int[2] { 34, 1 },
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 28 },
				new int[2] { 0, 181 },
				new int[2] { 2, 1 },
				new int[2] { 0, 91 },
				new int[2] { 0, 194 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 44 },
				new int[2] { 0, 167 },
				new int[2] { 2, 1 },
				new int[2] { 0, 122 },
				new int[2] { 0, 195 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 60 },
				new int[2] { 2, 1 },
				new int[2] { 0, 12 },
				new int[2] { 0, 208 },
				new int[2] { 2, 1 },
				new int[2] { 0, 182 },
				new int[2] { 0, 107 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 196 },
				new int[2] { 0, 76 },
				new int[2] { 2, 1 },
				new int[2] { 0, 153 },
				new int[2] { 0, 168 },
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 138 },
				new int[2] { 0, 197 },
				new int[2] { 2, 1 },
				new int[2] { 0, 92 },
				new int[2] { 0, 209 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 183 },
				new int[2] { 0, 123 },
				new int[2] { 2, 1 },
				new int[2] { 0, 29 },
				new int[2] { 0, 210 },
				new int[2] { 9, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 45 },
				new int[2] { 0, 211 },
				new int[2] { 2, 1 },
				new int[2] { 0, 61 },
				new int[2] { 0, 198 },
				new int[2] { 85, 250 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 108 },
				new int[2] { 0, 169 },
				new int[2] { 2, 1 },
				new int[2] { 0, 154 },
				new int[2] { 0, 212 },
				new int[2] { 32, 1 },
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 184 },
				new int[2] { 0, 139 },
				new int[2] { 2, 1 },
				new int[2] { 0, 77 },
				new int[2] { 0, 199 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 124 },
				new int[2] { 0, 213 },
				new int[2] { 2, 1 },
				new int[2] { 0, 93 },
				new int[2] { 0, 225 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 30 },
				new int[2] { 0, 226 },
				new int[2] { 2, 1 },
				new int[2] { 0, 170 },
				new int[2] { 0, 185 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 155 },
				new int[2] { 0, 227 },
				new int[2] { 2, 1 },
				new int[2] { 0, 214 },
				new int[2] { 0, 109 },
				new int[2] { 20, 1 },
				new int[2] { 10, 1 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 62 },
				new int[2] { 2, 1 },
				new int[2] { 0, 46 },
				new int[2] { 0, 78 },
				new int[2] { 2, 1 },
				new int[2] { 0, 200 },
				new int[2] { 0, 140 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 228 },
				new int[2] { 0, 215 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 125 },
				new int[2] { 0, 171 },
				new int[2] { 0, 229 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 186 },
				new int[2] { 0, 94 },
				new int[2] { 2, 1 },
				new int[2] { 0, 201 },
				new int[2] { 2, 1 },
				new int[2] { 0, 156 },
				new int[2] { 0, 110 },
				new int[2] { 8, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 230 },
				new int[2] { 2, 1 },
				new int[2] { 0, 13 },
				new int[2] { 2, 1 },
				new int[2] { 0, 224 },
				new int[2] { 0, 14 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 216 },
				new int[2] { 0, 141 },
				new int[2] { 2, 1 },
				new int[2] { 0, 187 },
				new int[2] { 0, 202 },
				new int[2] { 74, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 255 },
				new int[2] { 64, 1 },
				new int[2] { 58, 1 },
				new int[2] { 32, 1 },
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 172 },
				new int[2] { 0, 231 },
				new int[2] { 2, 1 },
				new int[2] { 0, 126 },
				new int[2] { 0, 217 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 157 },
				new int[2] { 0, 232 },
				new int[2] { 2, 1 },
				new int[2] { 0, 142 },
				new int[2] { 0, 203 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 188 },
				new int[2] { 0, 218 },
				new int[2] { 2, 1 },
				new int[2] { 0, 173 },
				new int[2] { 0, 233 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 158 },
				new int[2] { 0, 204 },
				new int[2] { 2, 1 },
				new int[2] { 0, 219 },
				new int[2] { 0, 189 },
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 234 },
				new int[2] { 0, 174 },
				new int[2] { 2, 1 },
				new int[2] { 0, 220 },
				new int[2] { 0, 205 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 235 },
				new int[2] { 0, 190 },
				new int[2] { 2, 1 },
				new int[2] { 0, 221 },
				new int[2] { 0, 236 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 206 },
				new int[2] { 0, 237 },
				new int[2] { 2, 1 },
				new int[2] { 0, 222 },
				new int[2] { 0, 238 },
				new int[2] { 0, 15 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 240 },
				new int[2] { 0, 31 },
				new int[2] { 0, 241 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 242 },
				new int[2] { 0, 47 },
				new int[2] { 2, 1 },
				new int[2] { 0, 243 },
				new int[2] { 0, 63 },
				new int[2] { 18, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 244 },
				new int[2] { 0, 79 },
				new int[2] { 2, 1 },
				new int[2] { 0, 245 },
				new int[2] { 0, 95 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 246 },
				new int[2] { 0, 111 },
				new int[2] { 2, 1 },
				new int[2] { 0, 247 },
				new int[2] { 2, 1 },
				new int[2] { 0, 127 },
				new int[2] { 0, 143 },
				new int[2] { 10, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 248 },
				new int[2] { 0, 249 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 159 },
				new int[2] { 0, 175 },
				new int[2] { 0, 250 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 251 },
				new int[2] { 0, 191 },
				new int[2] { 2, 1 },
				new int[2] { 0, 252 },
				new int[2] { 0, 207 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 253 },
				new int[2] { 0, 223 },
				new int[2] { 2, 1 },
				new int[2] { 0, 254 },
				new int[2] { 0, 239 }
			};
			ValTab32 = new int[31][]
			{
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 8 },
				new int[2] { 0, 4 },
				new int[2] { 2, 1 },
				new int[2] { 0, 1 },
				new int[2] { 0, 2 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 12 },
				new int[2] { 0, 10 },
				new int[2] { 2, 1 },
				new int[2] { 0, 3 },
				new int[2] { 0, 6 },
				new int[2] { 6, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 9 },
				new int[2] { 2, 1 },
				new int[2] { 0, 5 },
				new int[2] { 0, 7 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 14 },
				new int[2] { 0, 13 },
				new int[2] { 2, 1 },
				new int[2] { 0, 15 },
				new int[2] { 0, 11 }
			};
			ValTab33 = new int[31][]
			{
				new int[2] { 16, 1 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2],
				new int[2] { 0, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 2 },
				new int[2] { 0, 3 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 4 },
				new int[2] { 0, 5 },
				new int[2] { 2, 1 },
				new int[2] { 0, 6 },
				new int[2] { 0, 7 },
				new int[2] { 8, 1 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 8 },
				new int[2] { 0, 9 },
				new int[2] { 2, 1 },
				new int[2] { 0, 10 },
				new int[2] { 0, 11 },
				new int[2] { 4, 1 },
				new int[2] { 2, 1 },
				new int[2] { 0, 12 },
				new int[2] { 0, 13 },
				new int[2] { 2, 1 },
				new int[2] { 0, 14 },
				new int[2] { 0, 15 }
			};
		}

		private Huffman(string s, int xlen, int ylen, int linbits, int linmax, int @ref, int[] table, int[] hlen, int[][] val, int treelen)
		{
			_Tablename0 = s[0];
			_Tablename1 = s[1];
			_Tablename2 = s[2];
			_Xlen = xlen;
			_Ylen = ylen;
			_Linbits = linbits;
			_Linmax = linmax;
			_RefRenamed = @ref;
			_Table = table;
			_Hlen = hlen;
			_Val = val;
			_Treelen = treelen;
		}

		internal static int Decode(Huffman h, int[] x, int[] y, int[] v, int[] w, BitReserve br)
		{
			int i = 0;
			int result = 1;
			int num = int.MinValue;
			if (h._Val == null)
			{
				return 2;
			}
			if (h._Treelen == 0)
			{
				x[0] = (y[0] = 0);
				return 0;
			}
			do
			{
				if (h._Val[i][0] == 0)
				{
					x[0] = SupportClass.URShift(h._Val[i][1], 4);
					y[0] = h._Val[i][1] & 0xF;
					result = 0;
					break;
				}
				if (br.ReadOneBit() != 0)
				{
					for (; h._Val[i][1] >= 250; i += h._Val[i][1])
					{
					}
					i += h._Val[i][1];
				}
				else
				{
					for (; h._Val[i][0] >= 250; i += h._Val[i][0])
					{
					}
					i += h._Val[i][0];
				}
				num = SupportClass.URShift(num, 1);
			}
			while (num != 0 || i < 0);
			if (h._Tablename0 == '3' && (h._Tablename1 == '2' || h._Tablename1 == '3'))
			{
				v[0] = (y[0] >> 3) & 1;
				w[0] = (y[0] >> 2) & 1;
				x[0] = (y[0] >> 1) & 1;
				y[0] &= 1;
				if (v[0] != 0 && br.ReadOneBit() != 0)
				{
					v[0] = -v[0];
				}
				if (w[0] != 0 && br.ReadOneBit() != 0)
				{
					w[0] = -w[0];
				}
				if (x[0] != 0 && br.ReadOneBit() != 0)
				{
					x[0] = -x[0];
				}
				if (y[0] != 0 && br.ReadOneBit() != 0)
				{
					y[0] = -y[0];
				}
			}
			else
			{
				if (h._Linbits != 0 && h._Xlen - 1 == x[0])
				{
					x[0] += br.ReadBits(h._Linbits);
				}
				if (x[0] != 0 && br.ReadOneBit() != 0)
				{
					x[0] = -x[0];
				}
				if (h._Linbits != 0 && h._Ylen - 1 == y[0])
				{
					y[0] += br.ReadBits(h._Linbits);
				}
				if (y[0] != 0 && br.ReadOneBit() != 0)
				{
					y[0] = -y[0];
				}
			}
			return result;
		}

		internal static void Initialize()
		{
			if (HuffmanTable == null)
			{
				HuffmanTable = new Huffman[34];
				HuffmanTable[0] = new Huffman("0  ", 0, 0, 0, 0, -1, null, null, ValTab0, 0);
				HuffmanTable[1] = new Huffman("1  ", 2, 2, 0, 0, -1, null, null, ValTab1, 7);
				HuffmanTable[2] = new Huffman("2  ", 3, 3, 0, 0, -1, null, null, ValTab2, 17);
				HuffmanTable[3] = new Huffman("3  ", 3, 3, 0, 0, -1, null, null, ValTab3, 17);
				HuffmanTable[4] = new Huffman("4  ", 0, 0, 0, 0, -1, null, null, ValTab4, 0);
				HuffmanTable[5] = new Huffman("5  ", 4, 4, 0, 0, -1, null, null, ValTab5, 31);
				HuffmanTable[6] = new Huffman("6  ", 4, 4, 0, 0, -1, null, null, ValTab6, 31);
				HuffmanTable[7] = new Huffman("7  ", 6, 6, 0, 0, -1, null, null, ValTab7, 71);
				HuffmanTable[8] = new Huffman("8  ", 6, 6, 0, 0, -1, null, null, ValTab8, 71);
				HuffmanTable[9] = new Huffman("9  ", 6, 6, 0, 0, -1, null, null, ValTab9, 71);
				HuffmanTable[10] = new Huffman("10 ", 8, 8, 0, 0, -1, null, null, ValTab10, 127);
				HuffmanTable[11] = new Huffman("11 ", 8, 8, 0, 0, -1, null, null, ValTab11, 127);
				HuffmanTable[12] = new Huffman("12 ", 8, 8, 0, 0, -1, null, null, ValTab12, 127);
				HuffmanTable[13] = new Huffman("13 ", 16, 16, 0, 0, -1, null, null, ValTab13, 511);
				HuffmanTable[14] = new Huffman("14 ", 0, 0, 0, 0, -1, null, null, ValTab14, 0);
				HuffmanTable[15] = new Huffman("15 ", 16, 16, 0, 0, -1, null, null, ValTab15, 511);
				HuffmanTable[16] = new Huffman("16 ", 16, 16, 1, 1, -1, null, null, ValTab16, 511);
				HuffmanTable[17] = new Huffman("17 ", 16, 16, 2, 3, 16, null, null, ValTab16, 511);
				HuffmanTable[18] = new Huffman("18 ", 16, 16, 3, 7, 16, null, null, ValTab16, 511);
				HuffmanTable[19] = new Huffman("19 ", 16, 16, 4, 15, 16, null, null, ValTab16, 511);
				HuffmanTable[20] = new Huffman("20 ", 16, 16, 6, 63, 16, null, null, ValTab16, 511);
				HuffmanTable[21] = new Huffman("21 ", 16, 16, 8, 255, 16, null, null, ValTab16, 511);
				HuffmanTable[22] = new Huffman("22 ", 16, 16, 10, 1023, 16, null, null, ValTab16, 511);
				HuffmanTable[23] = new Huffman("23 ", 16, 16, 13, 8191, 16, null, null, ValTab16, 511);
				HuffmanTable[24] = new Huffman("24 ", 16, 16, 4, 15, -1, null, null, ValTab24, 512);
				HuffmanTable[25] = new Huffman("25 ", 16, 16, 5, 31, 24, null, null, ValTab24, 512);
				HuffmanTable[26] = new Huffman("26 ", 16, 16, 6, 63, 24, null, null, ValTab24, 512);
				HuffmanTable[27] = new Huffman("27 ", 16, 16, 7, 127, 24, null, null, ValTab24, 512);
				HuffmanTable[28] = new Huffman("28 ", 16, 16, 8, 255, 24, null, null, ValTab24, 512);
				HuffmanTable[29] = new Huffman("29 ", 16, 16, 9, 511, 24, null, null, ValTab24, 512);
				HuffmanTable[30] = new Huffman("30 ", 16, 16, 11, 2047, 24, null, null, ValTab24, 512);
				HuffmanTable[31] = new Huffman("31 ", 16, 16, 13, 8191, 24, null, null, ValTab24, 512);
				HuffmanTable[32] = new Huffman("32 ", 1, 16, 0, 0, -1, null, null, ValTab32, 31);
				HuffmanTable[33] = new Huffman("33 ", 1, 16, 0, 0, -1, null, null, ValTab33, 31);
			}
		}
	}
	public class OutputChannels
	{
		internal const int BOTH_CHANNELS = 0;

		internal const int LEFT_CHANNEL = 1;

		internal const int RIGHT_CHANNEL = 2;

		internal const int DOWNMIX_CHANNELS = 3;

		internal static readonly OutputChannels Left = new OutputChannels(1);

		internal static readonly OutputChannels Right = new OutputChannels(2);

		internal static readonly OutputChannels Both = new OutputChannels(0);

		internal static readonly OutputChannels DownMix = new OutputChannels(3);

		private readonly int _OutputChannels;

		internal virtual int ChannelsOutputCode => _OutputChannels;

		internal virtual int ChannelCount
		{
			get
			{
				if (_OutputChannels != 0)
				{
					return 1;
				}
				return 2;
			}
		}

		private OutputChannels(int channels)
		{
			_OutputChannels = channels;
			if (channels < 0 || channels > 3)
			{
				throw new ArgumentException("channels");
			}
		}

		internal static OutputChannels FromInt(int code)
		{
			return code switch
			{
				1 => Left, 
				2 => Right, 
				0 => Both, 
				3 => DownMix, 
				_ => throw new ArgumentException("Invalid channel code: " + code), 
			};
		}

		public override bool Equals(object obj)
		{
			bool result = false;
			if (obj is OutputChannels outputChannels)
			{
				result = outputChannels._OutputChannels == _OutputChannels;
			}
			return result;
		}

		public override int GetHashCode()
		{
			return _OutputChannels;
		}
	}
	internal enum OutputChannelsEnum
	{
		BothChannels,
		LeftChannel,
		RightChannel,
		DownmixChannels
	}
	public class PushbackStream
	{
		private readonly int _BackBufferSize;

		private readonly CircularByteBuffer _CircularByteBuffer;

		private readonly Stream _Stream;

		private readonly byte[] _TemporaryBuffer;

		private int _NumForwardBytesInBuffer;

		internal PushbackStream(Stream s, int backBufferSize)
		{
			_Stream = s;
			_BackBufferSize = backBufferSize;
			_TemporaryBuffer = new byte[_BackBufferSize];
			_CircularByteBuffer = new CircularByteBuffer(_BackBufferSize);
		}

		internal int Read(sbyte[] toRead, int offset, int length)
		{
			int num = 0;
			bool flag = true;
			while (num < length && flag)
			{
				if (_NumForwardBytesInBuffer > 0)
				{
					_NumForwardBytesInBuffer--;
					toRead[offset + num] = (sbyte)_CircularByteBuffer[_NumForwardBytesInBuffer];
					num++;
					continue;
				}
				int num2 = length - num;
				int num3 = _Stream.Read(_TemporaryBuffer, 0, num2);
				flag = num3 >= num2;
				for (int i = 0; i < num3; i++)
				{
					_CircularByteBuffer.Push(_TemporaryBuffer[i]);
					toRead[offset + num + i] = (sbyte)_TemporaryBuffer[i];
				}
				num += num3;
			}
			return num;
		}

		internal void UnRead(int length)
		{
			_NumForwardBytesInBuffer += length;
			if (_NumForwardBytesInBuffer > _BackBufferSize)
			{
				throw new Exception("The backstream cannot unread the requested number of bytes.");
			}
		}

		internal void Close()
		{
			_Stream.Close();
		}
	}
	public class SampleBuffer : ABuffer
	{
		private readonly short[] _Buffer;

		private readonly int[] _Bufferp;

		private readonly int _Channels;

		private readonly int _Frequency;

		internal virtual int ChannelCount => _Channels;

		internal virtual int SampleFrequency => _Frequency;

		internal virtual short[] Buffer => _Buffer;

		internal virtual int BufferLength => _Bufferp[0];

		internal SampleBuffer(int sampleFrequency, int numberOfChannels)
		{
			_Buffer = new short[2304];
			_Bufferp = new int[2];
			_Channels = numberOfChannels;
			_Frequency = sampleFrequency;
			for (int i = 0; i < numberOfChannels; i++)
			{
				_Bufferp[i] = (short)i;
			}
		}

		protected override void Append(int channel, short valueRenamed)
		{
			_Buffer[_Bufferp[channel]] = valueRenamed;
			_Bufferp[channel] += _Channels;
		}

		internal override void AppendSamples(int channel, float[] samples)
		{
			int num = _Bufferp[channel];
			int num2 = 0;
			while (num2 < 32)
			{
				float num3 = samples[num2++];
				num3 = ((num3 > 32767f) ? 32767f : ((num3 < -32767f) ? (-32767f) : num3));
				short num4 = (short)num3;
				_Buffer[num] = num4;
				num += _Channels;
			}
			_Bufferp[channel] = num;
		}

		internal override void WriteBuffer(int val)
		{
		}

		internal override void Close()
		{
		}

		internal override void ClearBuffer()
		{
			for (int i = 0; i < _Channels; i++)
			{
				_Bufferp[i] = (short)i;
			}
		}

		internal override void SetStopFlag()
		{
		}
	}
	public class SynthesisFilter
	{
		private const double MY_PI = Math.PI;

		private static readonly float Cos164 = (float)(1.0 / (2.0 * Math.Cos(Math.PI / 64.0)));

		private static readonly float Cos364 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 3.0 / 64.0)));

		private static readonly float Cos564 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 5.0 / 64.0)));

		private static readonly float Cos764 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 7.0 / 64.0)));

		private static readonly float Cos964 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 9.0 / 64.0)));

		private static readonly float Cos1164 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 11.0 / 64.0)));

		private static readonly float Cos1364 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 13.0 / 64.0)));

		private static readonly float Cos1564 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 15.0 / 64.0)));

		private static readonly float Cos1764 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 17.0 / 64.0)));

		private static readonly float Cos1964 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 19.0 / 64.0)));

		private static readonly float Cos2164 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 21.0 / 64.0)));

		private static readonly float Cos2364 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 23.0 / 64.0)));

		private static readonly float Cos2564 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 25.0 / 64.0)));

		private static readonly float Cos2764 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 27.0 / 64.0)));

		private static readonly float Cos2964 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 29.0 / 64.0)));

		private static readonly float Cos3164 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 31.0 / 64.0)));

		private static readonly float Cos132 = (float)(1.0 / (2.0 * Math.Cos(Math.PI / 32.0)));

		private static readonly float Cos332 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 3.0 / 32.0)));

		private static readonly float Cos532 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 5.0 / 32.0)));

		private static readonly float Cos732 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 7.0 / 32.0)));

		private static readonly float Cos932 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 9.0 / 32.0)));

		private static readonly float Cos1132 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 11.0 / 32.0)));

		private static readonly float Cos1332 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 13.0 / 32.0)));

		private static readonly float Cos1532 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 15.0 / 32.0)));

		private static readonly float Cos116 = (float)(1.0 / (2.0 * Math.Cos(Math.PI / 16.0)));

		private static readonly float Cos316 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 3.0 / 16.0)));

		private static readonly float Cos516 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 5.0 / 16.0)));

		private static readonly float Cos716 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 7.0 / 16.0)));

		private static readonly float Cos18 = (float)(1.0 / (2.0 * Math.Cos(Math.PI / 8.0)));

		private static readonly float Cos38 = (float)(1.0 / (2.0 * Math.Cos(Math.PI * 3.0 / 8.0)));

		private static readonly float Cos14 = (float)(1.0 / (2.0 * Math.Cos(Math.PI / 4.0)));

		private static float[] _d;

		private static float[][] _d16;

		private static readonly float[] DData = new float[512]
		{
			0f, -0.000442505f, 0.003250122f, -0.007003784f, 0.031082153f, -0.07862854f, 0.10031128f, -0.57203674f, 1.144989f, 0.57203674f,
			0.10031128f, 0.07862854f, 0.031082153f, 0.007003784f, 0.003250122f, 0.000442505f, -1.5259E-05f, -0.000473022f, 0.003326416f, -0.007919312f,
			0.030517578f, -0.08418274f, 0.090927124f, -0.6002197f, 1.1442871f, 0.54382324f, 0.1088562f, 0.07305908f, 0.03147888f, 0.006118774f,
			0.003173828f, 0.000396729f, -1.5259E-05f, -0.000534058f, 0.003387451f, -0.008865356f, 0.029785156f, -0.08970642f, 0.08068848f, -0.6282959f,
			1.1422119f, 0.51560974f, 0.11657715f, 0.06752014f, 0.03173828f, 0.0052948f, 0.003082275f, 0.000366211f, -1.5259E-05f, -0.000579834f,
			0.003433228f, -0.009841919f, 0.028884888f, -0.09516907f, 0.06959534f, -0.6562195f, 1.1387634f, 0.48747253f, 0.12347412f, 0.06199646f,
			0.031845093f, 0.004486084f, 0.002990723f, 0.000320435f, -1.5259E-05f, -0.00062561f, 0.003463745f, -0.010848999f, 0.027801514f, -0.10054016f,
			0.057617188f, -0.6839142f, 1.1339264f, 0.45947266f, 0.12957764f, 0.056533813f, 0.031814575f, 0.003723145f, 0.00289917f, 0.000289917f,
			-1.5259E-05f, -0.000686646f, 0.003479004f, -0.011886597f, 0.026535034f, -0.1058197f, 0.044784546f, -0.71131897f, 1.1277466f, 0.43165588f,
			0.1348877f, 0.051132202f, 0.031661987f, 0.003005981f, 0.002792358f, 0.000259399f, -1.5259E-05f, -0.000747681f, 0.003479004f, -0.012939453f,
			0.02508545f, -0.110946655f, 0.031082153f, -0.7383728f, 1.120224f, 0.40408325f, 0.13945007f, 0.045837402f, 0.03138733f, 0.002334595f,
			0.002685547f, 0.000244141f, -3.0518E-05f, -0.000808716f, 0.003463745f, -0.014022827f, 0.023422241f, -0.11592102f, 0.01651001f, -0.7650299f,
			1.1113739f, 0.37680054f, 0.14326477f, 0.040634155f, 0.03100586f, 0.001693726f, 0.002578735f, 0.000213623f, -3.0518E-05f, -0.00088501f,
			0.003417969f, -0.01512146f, 0.021575928f, -0.12069702f, 0.001068115f, -0.791214f, 1.1012115f, 0.34986877f, 0.1463623f, 0.03555298f,
			0.030532837f, 0.001098633f, 0.002456665f, 0.000198364f, -3.0518E-05f, -0.000961304f, 0.003372192f, -0.016235352f, 0.01953125f, -0.1252594f,
			-0.015228271f, -0.816864f, 1.0897827f, 0.32331848f, 0.1487732f, 0.03060913f, 0.029937744f, 0.000549316f, 0.002349854f, 0.000167847f,
			-3.0518E-05f, -0.001037598f, 0.00328064f, -0.017349243f, 0.01725769f, -0.12956238f, -0.03237915f, -0.84194946f, 1.0771179f, 0.2972107f,
			0.15049744f, 0.025817871f, 0.029281616f, 3.0518E-05f, 0.002243042f, 0.000152588f, -4.5776E-05f, -0.001113892f, 0.003173828f, -0.018463135f,
			0.014801025f, -0.1335907f, -0.050354004f, -0.8663635f, 1.0632172f, 0.2715912f, 0.15159607f, 0.0211792f, 0.028533936f, -0.000442505f,
			0.002120972f, 0.000137329f, -4.5776E-05f, -0.001205444f, 0.003051758f, -0.019577026f, 0.012115479f, -0.13729858f, -0.06916809f, -0.89009094f,
			1.0481567f, 0.24650574f, 0.15206909f, 0.016708374f, 0.02772522f, -0.000869751f, 0.00201416f, 0.00012207f, -6.1035E-05f, -0.001296997f,
			0.002883911f, -0.020690918f, 0.009231567f, -0.14067078f, -0.088775635f, -0.9130554f, 1.0319366f, 0.22198486f, 0.15196228f, 0.012420654f,
			0.02684021f, -0.001266479f, 0.001907349f, 0.000106812f, -6.1035E-05f, -0.00138855f, 0.002700806f, -0.02178955f, 0.006134033f, -0.14367676f,
			-0.10916138f, -0.9351959f, 1.0146179f, 0.19805908f, 0.15130615f, 0.00831604f, 0.025909424f, -0.001617432f, 0.001785278f, 0.000106812f,
			-7.6294E-05f, -0.001480103f, 0.002487183f, -0.022857666f, 0.002822876f, -0.1462555f, -0.13031006f, -0.95648193f, 0.99624634f, 0.17478943f,
			0.15011597f, 0.004394531f, 0.024932861f, -0.001937866f, 0.001693726f, 9.1553E-05f, -7.6294E-05f, -0.001586914f, 0.002227783f, -0.023910522f,
			-0.000686646f, -0.14842224f, -0.15220642f, -0.9768524f, 0.9768524f, 0.15220642f, 0.14842224f, 0.000686646f, 0.023910522f, -0.002227783f,
			0.001586914f, 7.6294E-05f, -9.1553E-05f, -0.001693726f, 0.001937866f, -0.024932861f, -0.004394531f, -0.15011597f, -0.17478943f, -0.99624634f,
			0.95648193f, 0.13031006f, 0.1462555f, -0.002822876f, 0.022857666f, -0.002487183f, 0.001480103f, 7.6294E-05f, -0.000106812f, -0.001785278f,
			0.001617432f, -0.025909424f, -0.00831604f, -0.15130615f, -0.19805908f, -1.0146179f, 0.9351959f, 0.10916138f, 0.14367676f, -0.006134033f,
			0.02178955f, -0.002700806f, 0.00138855f, 6.1035E-05f, -0.000106812f, -0.001907349f, 0.001266479f, -0.02684021f, -0.012420654f, -0.15196228f,
			-0.22198486f, -1.0319366f, 0.9130554f, 0.088775635f, 0.14067078f, -0.009231567f, 0.020690918f, -0.002883911f, 0.001296997f, 6.1035E-05f,
			-0.00012207f, -0.00201416f, 0.000869751f, -0.02772522f, -0.016708374f, -0.15206909f, -0.24650574f, -1.0481567f, 0.89009094f, 0.06916809f,
			0.13729858f, -0.012115479f, 0.019577026f, -0.003051758f, 0.001205444f, 4.5776E-05f, -0.000137329f, -0.002120972f, 0.000442505f, -0.028533936f,
			-0.0211792f, -0.15159607f, -0.2715912f, -1.0632172f, 0.8663635f, 0.050354004f, 0.1335907f, -0.014801025f, 0.018463135f, -0.003173828f,
			0.001113892f, 4.5776E-05f, -0.000152588f, -0.002243042f, -3.0518E-05f, -0.029281616f, -0.025817871f, -0.15049744f, -0.2972107f, -1.0771179f,
			0.84194946f, 0.03237915f, 0.12956238f, -0.01725769f, 0.017349243f, -0.00328064f, 0.001037598f, 3.0518E-05f, -0.000167847f, -0.002349854f,
			-0.000549316f, -0.029937744f, -0.03060913f, -0.1487732f, -0.32331848f, -1.0897827f, 0.816864f, 0.015228271f, 0.1252594f, -0.01953125f,
			0.016235352f, -0.003372192f, 0.000961304f, 3.0518E-05f, -0.000198364f, -0.002456665f, -0.001098633f, -0.030532837f, -0.03555298f, -0.1463623f,
			-0.34986877f, -1.1012115f, 0.791214f, -0.001068115f, 0.12069702f, -0.021575928f, 0.01512146f, -0.003417969f, 0.00088501f, 3.0518E-05f,
			-0.000213623f, -0.002578735f, -0.001693726f, -0.03100586f, -0.040634155f, -0.14326477f, -0.37680054f, -1.1113739f, 0.7650299f, -0.01651001f,
			0.11592102f, -0.023422241f, 0.014022827f, -0.003463745f, 0.000808716f, 3.0518E-05f, -0.000244141f, -0.002685547f, -0.002334595f, -0.03138733f,
			-0.045837402f, -0.13945007f, -0.40408325f, -1.120224f, 0.7383728f, -0.031082153f, 0.110946655f, -0.02508545f, 0.012939453f, -0.003479004f,
			0.000747681f, 1.5259E-05f, -0.000259399f, -0.002792358f, -0.003005981f, -0.031661987f, -0.051132202f, -0.1348877f, -0.43165588f, -1.1277466f,
			0.71131897f, -0.044784546f, 0.1058197f, -0.026535034f, 0.011886597f, -0.003479004f, 0.000686646f, 1.5259E-05f, -0.000289917f, -0.00289917f,
			-0.003723145f, -0.031814575f, -0.056533813f, -0.12957764f, -0.45947266f, -1.1339264f, 0.6839142f, -0.057617188f, 0.10054016f, -0.027801514f,
			0.010848999f, -0.003463745f, 0.00062561f, 1.5259E-05f, -0.000320435f, -0.002990723f, -0.004486084f, -0.031845093f, -0.06199646f, -0.12347412f,
			-0.48747253f, -1.1387634f, 0.6562195f, -0.06959534f, 0.09516907f, -0.028884888f, 0.009841919f, -0.003433228f, 0.000579834f, 1.5259E-05f,
			-0.000366211f, -0.003082275f, -0.0052948f, -0.03173828f, -0.06752014f, -0.11657715f, -0.51560974f, -1.1422119f, 0.6282959f, -0.08068848f,
			0.08970642f, -0.029785156f, 0.008865356f, -0.003387451f, 0.000534058f, 1.5259E-05f, -0.000396729f, -0.003173828f, -0.006118774f, -0.03147888f,
			-0.07305908f, -0.1088562f, -0.54382324f, -1.1442871f, 0.6002197f, -0.090927124f, 0.08418274f, -0.030517578f, 0.007919312f, -0.003326416f,
			0.000473022f, 1.5259E-05f
		};

		private readonly int _Channel;

		private readonly float[] _Samples;

		private readonly float _Scalefactor;

		private readonly float[] _V1;

		private readonly float[] _V2;

		private float[] _TmpOut;

		private float[] _ActualV;

		private int _ActualWritePos;

		private float[] _Eq;

		internal float[] Eq
		{
			set
			{
				_Eq = value;
				if (_Eq == null)
				{
					_Eq = new float[32];
					for (int i = 0; i < 32; i++)
					{
						_Eq[i] = 1f;
					}
				}
				if (_Eq.Length < 32)
				{
					throw new ArgumentException("eq0");
				}
			}
		}

		internal SynthesisFilter(int channelnumber, float factor, float[] eq0)
		{
			InitBlock();
			if (_d == null)
			{
				_d = DData;
				_d16 = SplitArray(_d, 16);
			}
			_V1 = new float[512];
			_V2 = new float[512];
			_Samples = new float[32];
			_Channel = channelnumber;
			_Scalefactor = factor;
			Eq = _Eq;
			Reset();
		}

		private void InitBlock()
		{
			_TmpOut = new float[32];
		}

		internal void Reset()
		{
			for (int i = 0; i < 512; i++)
			{
				_V1[i] = (_V2[i] = 0f);
			}
			for (int j = 0; j < 32; j++)
			{
				_Samples[j] = 0f;
			}
			_ActualV = _V1;
			_ActualWritePos = 15;
		}

		internal void AddSample(float sample, int subbandnumber)
		{
			_Samples[subbandnumber] = _Eq[subbandnumber] * sample;
		}

		internal void AddSamples(float[] s)
		{
			for (int num = 31; num >= 0; num--)
			{
				_Samples[num] = s[num] * _Eq[num];
			}
		}

		private void ComputeNewValues()
		{
			float[] samples = _Samples;
			float num = samples[0];
			float num2 = samples[1];
			float num3 = samples[2];
			float num4 = samples[3];
			float num5 = samples[4];
			float num6 = samples[5];
			float num7 = samples[6];
			float num8 = samples[7];
			float num9 = samples[8];
			float num10 = samples[9];
			float num11 = samples[10];
			float num12 = samples[11];
			float num13 = samples[12];
			float num14 = samples[13];
			float num15 = samples[14];
			float num16 = samples[15];
			float num17 = samples[16];
			float num18 = samples[17];
			float num19 = samples[18];
			float num20 = samples[19];
			float num21 = samples[20];
			float num22 = samples[21];
			float num23 = samples[22];
			float num24 = samples[23];
			float num25 = samples[24];
			float num26 = samples[25];
			float num27 = samples[26];
			float num28 = samples[27];
			float num29 = samples[28];
			float num30 = samples[29];
			float num31 = samples[30];
			float num32 = samples[31];
			float num33 = num + num32;
			float num34 = num2 + num31;
			float num35 = num3 + num30;
			float num36 = num4 + num29;
			float num37 = num5 + num28;
			float num38 = num6 + num27;
			float num39 = num7 + num26;
			float num40 = num8 + num25;
			float num41 = num9 + num24;
			float num42 = num10 + num23;
			float num43 = num11 + num22;
			float num44 = num12 + num21;
			float num45 = num13 + num20;
			float num46 = num14 + num19;
			float num47 = num15 + num18;
			float num48 = num16 + num17;
			float num49 = num33 + num48;
			float num50 = num34 + num47;
			float num51 = num35 + num46;
			float num52 = num36 + num45;
			float num53 = num37 + num44;
			float num54 = num38 + num43;
			float num55 = num39 + num42;
			float num56 = num40 + num41;
			float num57 = (num33 - num48) * Cos132;
			float num58 = (num34 - num47) * Cos332;
			float num59 = (num35 - num46) * Cos532;
			float num60 = (num36 - num45) * Cos732;
			float num61 = (num37 - num44) * Cos932;
			float num62 = (num38 - num43) * Cos1132;
			float num63 = (num39 - num42) * Cos1332;
			float num64 = (num40 - num41) * Cos1532;
			num33 = num49 + num56;
			num34 = num50 + num55;
			num35 = num51 + num54;
			num36 = num52 + num53;
			num37 = (num49 - num56) * Cos116;
			num38 = (num50 - num55) * Cos316;
			num39 = (num51 - num54) * Cos516;
			num40 = (num52 - num53) * Cos716;
			num41 = num57 + num64;
			num42 = num58 + num63;
			num43 = num59 + num62;
			num44 = num60 + num61;
			num45 = (num57 - num64) * Cos116;
			num46 = (num58 - num63) * Cos316;
			num47 = (num59 - num62) * Cos516;
			num48 = (num60 - num61) * Cos716;
			float num65 = num33 + num36;
			num50 = num34 + num35;
			num51 = (num33 - num36) * Cos18;
			num52 = (num34 - num35) * Cos38;
			num53 = num37 + num40;
			num54 = num38 + num39;
			num55 = (num37 - num40) * Cos18;
			num56 = (num38 - num39) * Cos38;
			num57 = num41 + num44;
			num58 = num42 + num43;
			num59 = (num41 - num44) * Cos18;
			num60 = (num42 - num43) * Cos38;
			num61 = num45 + num48;
			num62 = num46 + num47;
			num63 = (num45 - num48) * Cos18;
			num64 = (num46 - num47) * Cos38;
			num33 = num65 + num50;
			num34 = (num65 - num50) * Cos14;
			num35 = num51 + num52;
			num36 = (num51 - num52) * Cos14;
			num37 = num53 + num54;
			num38 = (num53 - num54) * Cos14;
			num39 = num55 + num56;
			num40 = (num55 - num56) * Cos14;
			num41 = num57 + num58;
			num42 = (num57 - num58) * Cos14;
			num43 = num59 + num60;
			num44 = (num59 - num60) * Cos14;
			num45 = num61 + num62;
			num46 = (num61 - num62) * Cos14;
			num47 = num63 + num64;
			num48 = (num63 - num64) * Cos14;
			float num67;
			float num68;
			float num66 = 0f - (num67 = (num68 = num40) + num38) - num39;
			float num69 = 0f - num39 - num40 - num37;
			float num71;
			float num72;
			float num70 = (num71 = (num72 = num48) + num44) + num46;
			float num74;
			float num73 = 0f - (num74 = num48 + num46 + num42) - num47;
			float num75 = 0f - num47 - num48 - num43 - num44;
			float num76 = num75 - num46;
			float num77 = 0f - num47 - num48 - num45 - num41;
			float num78 = num75 - num45;
			float num79 = 0f - num33;
			float num80 = num34;
			float num82;
			float num81 = 0f - (num82 = num36) - num35;
			num33 = (num - num32) * Cos164;
			num34 = (num2 - num31) * Cos364;
			num35 = (num3 - num30) * Cos564;
			num36 = (num4 - num29) * Cos764;
			num37 = (num5 - num28) * Cos964;
			num38 = (num6 - num27) * Cos1164;
			num39 = (num7 - num26) * Cos1364;
			num40 = (num8 - num25) * Cos1564;
			num41 = (num9 - num24) * Cos1764;
			num42 = (num10 - num23) * Cos1964;
			num43 = (num11 - num22) * Cos2164;
			num44 = (num12 - num21) * Cos2364;
			num45 = (num13 - num20) * Cos2564;
			num46 = (num14 - num19) * Cos2764;
			num47 = (num15 - num18) * Cos2964;
			num48 = (num16 - num17) * Cos3164;
			float num83 = num33 + num48;
			num50 = num34 + num47;
			num51 = num35 + num46;
			num52 = num36 + num45;
			num53 = num37 + num44;
			num54 = num38 + num43;
			num55 = num39 + num42;
			num56 = num40 + num41;
			num57 = (num33 - num48) * Cos132;
			num58 = (num34 - num47) * Cos332;
			num59 = (num35 - num46) * Cos532;
			num60 = (num36 - num45) * Cos732;
			num61 = (num37 - num44) * Cos932;
			num62 = (num38 - num43) * Cos1132;
			num63 = (num39 - num42) * Cos1332;
			num64 = (num40 - num41) * Cos1532;
			num33 = num83 + num56;
			num34 = num50 + num55;
			num35 = num51 + num54;
			num36 = num52 + num53;
			num37 = (num83 - num56) * Cos116;
			num38 = (num50 - num55) * Cos316;
			num39 = (num51 - num54) * Cos516;
			num40 = (num52 - num53) * Cos716;
			num41 = num57 + num64;
			num42 = num58 + num63;
			num43 = num59 + num62;
			num44 = num60 + num61;
			num45 = (num57 - num64) * Cos116;
			num46 = (num58 - num63) * Cos316;
			num47 = (num59 - num62) * Cos516;
			num48 = (num60 - num61) * Cos716;
			float num84 = num33 + num36;
			num50 = num34 + num35;
			num51 = (num33 - num36) * Cos18;
			num52 = (num34 - num35) * Cos38;
			num53 = num37 + num40;
			num54 = num38 + num39;
			num55 = (num37 - num40) * Cos18;
			num56 = (num38 - num39) * Cos38;
			num57 = num41 + num44;
			num58 = num42 + num43;
			num59 = (num41 - num44) * Cos18;
			num60 = (num42 - num43) * Cos38;
			num61 = num45 + num48;
			num62 = num46 + num47;
			num63 = (num45 - num48) * Cos18;
			num64 = (num46 - num47) * Cos38;
			num33 = num84 + num50;
			num34 = (num84 - num50) * Cos14;
			num35 = num51 + num52;
			num36 = (num51 - num52) * Cos14;
			num37 = num53 + num54;
			num38 = (num53 - num54) * Cos14;
			num39 = num55 + num56;
			num40 = (num55 - num56) * Cos14;
			num41 = num57 + num58;
			num42 = (num57 - num58) * Cos14;
			num43 = num59 + num60;
			num44 = (num59 - num60) * Cos14;
			num45 = num61 + num62;
			num46 = (num61 - num62) * Cos14;
			num47 = num63 + num64;
			num48 = (num63 - num64) * Cos14;
			float num86;
			float num87;
			float num88;
			float num85 = (num86 = (num87 = (num88 = num48) + num40) + num44) + num38 + num46;
			float num90;
			float num89 = (num90 = num48 + num44 + num36) + num46;
			float num91 = num46 + num48 + num42;
			float num93;
			float num92 = 0f - (num93 = num91 + num34) - num47;
			float num95;
			float num94 = 0f - (num95 = num91 + num38 + num40) - num39 - num47;
			float num96 = 0f - num43 - num44 - num47 - num48;
			float num97 = num96 - num46 - num35 - num36;
			float num98 = num96 - num46 - num38 - num39 - num40;
			float num99 = num96 - num45 - num35 - num36;
			float num101;
			float num100 = num96 - num45 - (num101 = num37 + num39 + num40);
			float num102 = 0f - num41 - num45 - num47 - num48;
			float num103 = num102 - num33;
			float num104 = num102 - num101;
			float[] actualV = _ActualV;
			int actualWritePos = _ActualWritePos;
			actualV[actualWritePos] = num80;
			actualV[16 + actualWritePos] = num93;
			actualV[32 + actualWritePos] = num74;
			actualV[48 + actualWritePos] = num95;
			actualV[64 + actualWritePos] = num67;
			actualV[80 + actualWritePos] = num85;
			actualV[96 + actualWritePos] = num70;
			actualV[112 + actualWritePos] = num89;
			actualV[128 + actualWritePos] = num82;
			actualV[144 + actualWritePos] = num90;
			actualV[160 + actualWritePos] = num71;
			actualV[176 + actualWritePos] = num86;
			actualV[192 + actualWritePos] = num68;
			actualV[208 + actualWritePos] = num87;
			actualV[224 + actualWritePos] = num72;
			actualV[240 + actualWritePos] = num88;
			actualV[256 + actualWritePos] = 0f;
			actualV[272 + actualWritePos] = 0f - num88;
			actualV[288 + actualWritePos] = 0f - num72;
			actualV[304 + actualWritePos] = 0f - num87;
			actualV[320 + actualWritePos] = 0f - num68;
			actualV[336 + actualWritePos] = 0f - num86;
			actualV[352 + actualWritePos] = 0f - num71;
			actualV[368 + actualWritePos] = 0f - num90;
			actualV[384 + actualWritePos] = 0f - num82;
			actualV[400 + actualWritePos] = 0f - num89;
			actualV[416 + actualWritePos] = 0f - num70;
			actualV[432 + actualWritePos] = 0f - num85;
			actualV[448 + actualWritePos] = 0f - num67;
			actualV[464 + actualWritePos] = 0f - num95;
			actualV[480 + actualWritePos] = 0f - num74;
			actualV[496 + actualWritePos] = 0f - num93;
			float[] obj = ((_ActualV == _V1) ? _V2 : _V1);
			obj[actualWritePos] = 0f - num80;
			obj[16 + actualWritePos] = num92;
			obj[32 + actualWritePos] = num73;
			obj[48 + actualWritePos] = num94;
			obj[64 + actualWritePos] = num66;
			obj[80 + actualWritePos] = num98;
			obj[96 + actualWritePos] = num76;
			obj[112 + actualWritePos] = num97;
			obj[128 + actualWritePos] = num81;
			obj[144 + actualWritePos] = num99;
			obj[160 + actualWritePos] = num78;
			obj[176 + actualWritePos] = num100;
			obj[192 + actualWritePos] = num69;
			obj[208 + actualWritePos] = num104;
			obj[224 + actualWritePos] = num77;
			obj[240 + actualWritePos] = num103;
			obj[256 + actualWritePos] = num79;
			obj[272 + actualWritePos] = num103;
			obj[288 + actualWritePos] = num77;
			obj[304 + actualWritePos] = num104;
			obj[320 + actualWritePos] = num69;
			obj[336 + actualWritePos] = num100;
			obj[352 + actualWritePos] = num78;
			obj[368 + actualWritePos] = num99;
			obj[384 + actualWritePos] = num81;
			obj[400 + actualWritePos] = num97;
			obj[416 + actualWritePos] = num76;
			obj[432 + actualWritePos] = num98;
			obj[448 + actualWritePos] = num66;
			obj[464 + actualWritePos] = num94;
			obj[480 + actualWritePos] = num73;
			obj[496 + actualWritePos] = num92;
		}

		private void compute_pc_samples0(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[num] * array[0] + actualV[15 + num] * array[1] + actualV[14 + num] * array[2] + actualV[13 + num] * array[3] + actualV[12 + num] * array[4] + actualV[11 + num] * array[5] + actualV[10 + num] * array[6] + actualV[9 + num] * array[7] + actualV[8 + num] * array[8] + actualV[7 + num] * array[9] + actualV[6 + num] * array[10] + actualV[5 + num] * array[11] + actualV[4 + num] * array[12] + actualV[3 + num] * array[13] + actualV[2 + num] * array[14] + actualV[1 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples1(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[1 + num] * array[0] + actualV[num] * array[1] + actualV[15 + num] * array[2] + actualV[14 + num] * array[3] + actualV[13 + num] * array[4] + actualV[12 + num] * array[5] + actualV[11 + num] * array[6] + actualV[10 + num] * array[7] + actualV[9 + num] * array[8] + actualV[8 + num] * array[9] + actualV[7 + num] * array[10] + actualV[6 + num] * array[11] + actualV[5 + num] * array[12] + actualV[4 + num] * array[13] + actualV[3 + num] * array[14] + actualV[2 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples2(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[2 + num] * array[0] + actualV[1 + num] * array[1] + actualV[num] * array[2] + actualV[15 + num] * array[3] + actualV[14 + num] * array[4] + actualV[13 + num] * array[5] + actualV[12 + num] * array[6] + actualV[11 + num] * array[7] + actualV[10 + num] * array[8] + actualV[9 + num] * array[9] + actualV[8 + num] * array[10] + actualV[7 + num] * array[11] + actualV[6 + num] * array[12] + actualV[5 + num] * array[13] + actualV[4 + num] * array[14] + actualV[3 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples3(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[3 + num] * array[0] + actualV[2 + num] * array[1] + actualV[1 + num] * array[2] + actualV[num] * array[3] + actualV[15 + num] * array[4] + actualV[14 + num] * array[5] + actualV[13 + num] * array[6] + actualV[12 + num] * array[7] + actualV[11 + num] * array[8] + actualV[10 + num] * array[9] + actualV[9 + num] * array[10] + actualV[8 + num] * array[11] + actualV[7 + num] * array[12] + actualV[6 + num] * array[13] + actualV[5 + num] * array[14] + actualV[4 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples4(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[4 + num] * array[0] + actualV[3 + num] * array[1] + actualV[2 + num] * array[2] + actualV[1 + num] * array[3] + actualV[num] * array[4] + actualV[15 + num] * array[5] + actualV[14 + num] * array[6] + actualV[13 + num] * array[7] + actualV[12 + num] * array[8] + actualV[11 + num] * array[9] + actualV[10 + num] * array[10] + actualV[9 + num] * array[11] + actualV[8 + num] * array[12] + actualV[7 + num] * array[13] + actualV[6 + num] * array[14] + actualV[5 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples5(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[5 + num] * array[0] + actualV[4 + num] * array[1] + actualV[3 + num] * array[2] + actualV[2 + num] * array[3] + actualV[1 + num] * array[4] + actualV[num] * array[5] + actualV[15 + num] * array[6] + actualV[14 + num] * array[7] + actualV[13 + num] * array[8] + actualV[12 + num] * array[9] + actualV[11 + num] * array[10] + actualV[10 + num] * array[11] + actualV[9 + num] * array[12] + actualV[8 + num] * array[13] + actualV[7 + num] * array[14] + actualV[6 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples6(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[6 + num] * array[0] + actualV[5 + num] * array[1] + actualV[4 + num] * array[2] + actualV[3 + num] * array[3] + actualV[2 + num] * array[4] + actualV[1 + num] * array[5] + actualV[num] * array[6] + actualV[15 + num] * array[7] + actualV[14 + num] * array[8] + actualV[13 + num] * array[9] + actualV[12 + num] * array[10] + actualV[11 + num] * array[11] + actualV[10 + num] * array[12] + actualV[9 + num] * array[13] + actualV[8 + num] * array[14] + actualV[7 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples7(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[7 + num] * array[0] + actualV[6 + num] * array[1] + actualV[5 + num] * array[2] + actualV[4 + num] * array[3] + actualV[3 + num] * array[4] + actualV[2 + num] * array[5] + actualV[1 + num] * array[6] + actualV[num] * array[7] + actualV[15 + num] * array[8] + actualV[14 + num] * array[9] + actualV[13 + num] * array[10] + actualV[12 + num] * array[11] + actualV[11 + num] * array[12] + actualV[10 + num] * array[13] + actualV[9 + num] * array[14] + actualV[8 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples8(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[8 + num] * array[0] + actualV[7 + num] * array[1] + actualV[6 + num] * array[2] + actualV[5 + num] * array[3] + actualV[4 + num] * array[4] + actualV[3 + num] * array[5] + actualV[2 + num] * array[6] + actualV[1 + num] * array[7] + actualV[num] * array[8] + actualV[15 + num] * array[9] + actualV[14 + num] * array[10] + actualV[13 + num] * array[11] + actualV[12 + num] * array[12] + actualV[11 + num] * array[13] + actualV[10 + num] * array[14] + actualV[9 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples9(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[9 + num] * array[0] + actualV[8 + num] * array[1] + actualV[7 + num] * array[2] + actualV[6 + num] * array[3] + actualV[5 + num] * array[4] + actualV[4 + num] * array[5] + actualV[3 + num] * array[6] + actualV[2 + num] * array[7] + actualV[1 + num] * array[8] + actualV[num] * array[9] + actualV[15 + num] * array[10] + actualV[14 + num] * array[11] + actualV[13 + num] * array[12] + actualV[12 + num] * array[13] + actualV[11 + num] * array[14] + actualV[10 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples10(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[10 + num] * array[0] + actualV[9 + num] * array[1] + actualV[8 + num] * array[2] + actualV[7 + num] * array[3] + actualV[6 + num] * array[4] + actualV[5 + num] * array[5] + actualV[4 + num] * array[6] + actualV[3 + num] * array[7] + actualV[2 + num] * array[8] + actualV[1 + num] * array[9] + actualV[num] * array[10] + actualV[15 + num] * array[11] + actualV[14 + num] * array[12] + actualV[13 + num] * array[13] + actualV[12 + num] * array[14] + actualV[11 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples11(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[11 + num] * array[0] + actualV[10 + num] * array[1] + actualV[9 + num] * array[2] + actualV[8 + num] * array[3] + actualV[7 + num] * array[4] + actualV[6 + num] * array[5] + actualV[5 + num] * array[6] + actualV[4 + num] * array[7] + actualV[3 + num] * array[8] + actualV[2 + num] * array[9] + actualV[1 + num] * array[10] + actualV[num] * array[11] + actualV[15 + num] * array[12] + actualV[14 + num] * array[13] + actualV[13 + num] * array[14] + actualV[12 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples12(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[12 + num] * array[0] + actualV[11 + num] * array[1] + actualV[10 + num] * array[2] + actualV[9 + num] * array[3] + actualV[8 + num] * array[4] + actualV[7 + num] * array[5] + actualV[6 + num] * array[6] + actualV[5 + num] * array[7] + actualV[4 + num] * array[8] + actualV[3 + num] * array[9] + actualV[2 + num] * array[10] + actualV[1 + num] * array[11] + actualV[num] * array[12] + actualV[15 + num] * array[13] + actualV[14 + num] * array[14] + actualV[13 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples13(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[13 + num] * array[0] + actualV[12 + num] * array[1] + actualV[11 + num] * array[2] + actualV[10 + num] * array[3] + actualV[9 + num] * array[4] + actualV[8 + num] * array[5] + actualV[7 + num] * array[6] + actualV[6 + num] * array[7] + actualV[5 + num] * array[8] + actualV[4 + num] * array[9] + actualV[3 + num] * array[10] + actualV[2 + num] * array[11] + actualV[1 + num] * array[12] + actualV[num] * array[13] + actualV[15 + num] * array[14] + actualV[14 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples14(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[14 + num] * array[0] + actualV[13 + num] * array[1] + actualV[12 + num] * array[2] + actualV[11 + num] * array[3] + actualV[10 + num] * array[4] + actualV[9 + num] * array[5] + actualV[8 + num] * array[6] + actualV[7 + num] * array[7] + actualV[6 + num] * array[8] + actualV[5 + num] * array[9] + actualV[4 + num] * array[10] + actualV[3 + num] * array[11] + actualV[2 + num] * array[12] + actualV[1 + num] * array[13] + actualV[num] * array[14] + actualV[15 + num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void Compute_pc_samples15(ABuffer buffer)
		{
			float[] actualV = _ActualV;
			float[] tmpOut = _TmpOut;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				float[] array = _d16[i];
				float num2 = (actualV[15 + num] * array[0] + actualV[14 + num] * array[1] + actualV[13 + num] * array[2] + actualV[12 + num] * array[3] + actualV[11 + num] * array[4] + actualV[10 + num] * array[5] + actualV[9 + num] * array[6] + actualV[8 + num] * array[7] + actualV[7 + num] * array[8] + actualV[6 + num] * array[9] + actualV[5 + num] * array[10] + actualV[4 + num] * array[11] + actualV[3 + num] * array[12] + actualV[2 + num] * array[13] + actualV[1 + num] * array[14] + actualV[num] * array[15]) * _Scalefactor;
				tmpOut[i] = num2;
				num += 16;
			}
		}

		private void compute_pc_samples(ABuffer buffer)
		{
			switch (_ActualWritePos)
			{
			case 0:
				compute_pc_samples0(buffer);
				break;
			case 1:
				compute_pc_samples1(buffer);
				break;
			case 2:
				compute_pc_samples2(buffer);
				break;
			case 3:
				compute_pc_samples3(buffer);
				break;
			case 4:
				compute_pc_samples4(buffer);
				break;
			case 5:
				compute_pc_samples5(buffer);
				break;
			case 6:
				compute_pc_samples6(buffer);
				break;
			case 7:
				compute_pc_samples7(buffer);
				break;
			case 8:
				compute_pc_samples8(buffer);
				break;
			case 9:
				compute_pc_samples9(buffer);
				break;
			case 10:
				compute_pc_samples10(buffer);
				break;
			case 11:
				compute_pc_samples11(buffer);
				break;
			case 12:
				compute_pc_samples12(buffer);
				break;
			case 13:
				compute_pc_samples13(buffer);
				break;
			case 14:
				compute_pc_samples14(buffer);
				break;
			case 15:
				Compute_pc_samples15(buffer);
				break;
			}
			buffer?.AppendSamples(_Channel, _TmpOut);
		}

		internal void calculate_pc_samples(ABuffer buffer)
		{
			ComputeNewValues();
			compute_pc_samples(buffer);
			_ActualWritePos = (_ActualWritePos + 1) & 0xF;
			_ActualV = ((_ActualV == _V1) ? _V2 : _V1);
			for (int i = 0; i < 32; i++)
			{
				_Samples[i] = 0f;
			}
		}

		private static float[][] SplitArray(float[] array, int blockSize)
		{
			int num = array.Length / blockSize;
			float[][] array2 = new float[num][];
			for (int i = 0; i < num; i++)
			{
				array2[i] = SubArray(array, i * blockSize, blockSize);
			}
			return array2;
		}

		private static float[] SubArray(float[] array, int offs, int len)
		{
			if (offs + len > array.Length)
			{
				len = array.Length - offs;
			}
			if (len < 0)
			{
				len = 0;
			}
			float[] array2 = new float[len];
			for (int i = 0; i < len; i++)
			{
				array2[i] = array[offs + i];
			}
			return array2;
		}
	}
}
namespace MP3Sharp.Decoding.Decoders
{
	public abstract class ASubband
	{
		internal static readonly float[] ScaleFactors = new float[64]
		{
			2f,
			1.587401f,
			1.2599211f,
			1f,
			0.7937005f,
			0.62996054f,
			0.5f,
			0.39685026f,
			0.31498027f,
			0.25f,
			0.19842513f,
			0.15749013f,
			0.125f,
			0.099212565f,
			0.07874507f,
			0.0625f,
			0.049606282f,
			0.039372534f,
			1f / 32f,
			0.024803141f,
			0.019686267f,
			1f / 64f,
			0.012401571f,
			0.009843133f,
			1f / 128f,
			0.0062007853f,
			0.0049215667f,
			0.00390625f,
			0.0031003926f,
			0.0024607833f,
			0.001953125f,
			0.0015501963f,
			0.0012303917f,
			0.0009765625f,
			0.00077509816f,
			0.00061519584f,
			0.00048828125f,
			0.00038754908f,
			0.00030759792f,
			0.00024414062f,
			0.00019377454f,
			0.00015379896f,
			0.00012207031f,
			9.688727E-05f,
			7.689948E-05f,
			6.1035156E-05f,
			4.8443635E-05f,
			3.844974E-05f,
			3.0517578E-05f,
			2.4221818E-05f,
			1.922487E-05f,
			1.5258789E-05f,
			1.2110909E-05f,
			9.612435E-06f,
			7.6293945E-06f,
			6.0554544E-06f,
			4.8062175E-06f,
			3.8146973E-06f,
			3.0277272E-06f,
			2.4031087E-06f,
			1.9073486E-06f,
			1.5138636E-06f,
			1.2015544E-06f,
			0f
		};

		internal abstract void ReadAllocation(Bitstream stream, Header header, Crc16 crc);

		internal abstract void ReadScaleFactor(Bitstream stream, Header header);

		internal abstract bool ReadSampleData(Bitstream stream);

		internal abstract bool PutNextSample(int channels, SynthesisFilter filter1, SynthesisFilter filter2);
	}
	public interface IFrameDecoder
	{
		void DecodeFrame();
	}
	public class LayerIDecoder : IFrameDecoder
	{
		protected ABuffer Buffer;

		protected readonly Crc16 CRC;

		protected SynthesisFilter Filter1;

		protected SynthesisFilter Filter2;

		protected Header Header;

		protected int Mode;

		protected int NuSubbands;

		protected Bitstream Stream;

		protected ASubband[] Subbands;

		protected int WhichChannels;

		internal LayerIDecoder()
		{
			CRC = new Crc16();
		}

		public virtual void DecodeFrame()
		{
			NuSubbands = Header.NumberSubbands();
			Subbands = new ASubband[32];
			Mode = Header.Mode();
			CreateSubbands();
			ReadAllocation();
			ReadScaleFactorSelection();
			if (CRC != null || Header.IsChecksumOK())
			{
				ReadScaleFactors();
				ReadSampleData();
			}
		}

		internal virtual void Create(Bitstream stream0, Header header0, SynthesisFilter filtera, SynthesisFilter filterb, ABuffer buffer0, int whichCh0)
		{
			Stream = stream0;
			Header = header0;
			Filter1 = filtera;
			Filter2 = filterb;
			Buffer = buffer0;
			WhichChannels = whichCh0;
		}

		protected virtual void CreateSubbands()
		{
			if (Mode == 3)
			{
				for (int i = 0; i < NuSubbands; i++)
				{
					Subbands[i] = new SubbandLayer1(i);
				}
			}
			else if (Mode == 1)
			{
				int i;
				for (i = 0; i < Header.IntensityStereoBound(); i++)
				{
					Subbands[i] = new SubbandLayer1Stereo(i);
				}
				for (; i < NuSubbands; i++)
				{
					Subbands[i] = new SubbandLayer1IntensityStereo(i);
				}
			}
			else
			{
				for (int i = 0; i < NuSubbands; i++)
				{
					Subbands[i] = new SubbandLayer1Stereo(i);
				}
			}
		}

		protected virtual void ReadAllocation()
		{
			for (int i = 0; i < NuSubbands; i++)
			{
				Subbands[i].ReadAllocation(Stream, Header, CRC);
			}
		}

		protected virtual void ReadScaleFactorSelection()
		{
		}

		protected virtual void ReadScaleFactors()
		{
			for (int i = 0; i < NuSubbands; i++)
			{
				Subbands[i].ReadScaleFactor(Stream, Header);
			}
		}

		protected virtual void ReadSampleData()
		{
			bool flag = false;
			bool flag2 = false;
			int num = Header.Mode();
			do
			{
				for (int i = 0; i < NuSubbands; i++)
				{
					flag = Subbands[i].ReadSampleData(Stream);
				}
				do
				{
					for (int i = 0; i < NuSubbands; i++)
					{
						flag2 = Subbands[i].PutNextSample(WhichChannels, Filter1, Filter2);
					}
					Filter1.calculate_pc_samples(Buffer);
					if (WhichChannels == 0 && num != 3)
					{
						Filter2.calculate_pc_samples(Buffer);
					}
				}
				while (!flag2);
			}
			while (!flag);
		}
	}
	public class LayerIIDecoder : LayerIDecoder
	{
		protected override void CreateSubbands()
		{
			switch (Mode)
			{
			case 3:
			{
				for (int i = 0; i < NuSubbands; i++)
				{
					Subbands[i] = new SubbandLayer2(i);
				}
				break;
			}
			case 1:
			{
				int i;
				for (i = 0; i < Header.IntensityStereoBound(); i++)
				{
					Subbands[i] = new SubbandLayer2Stereo(i);
				}
				for (; i < NuSubbands; i++)
				{
					Subbands[i] = new SubbandLayer2IntensityStereo(i);
				}
				break;
			}
			default:
			{
				for (int i = 0; i < NuSubbands; i++)
				{
					Subbands[i] = new SubbandLayer2Stereo(i);
				}
				break;
			}
			}
		}

		protected override void ReadScaleFactorSelection()
		{
			for (int i = 0; i < NuSubbands; i++)
			{
				((SubbandLayer2)Subbands[i]).ReadScaleFactorSelection(Stream, CRC);
			}
		}
	}
	internal sealed class LayerIIIDecoder : IFrameDecoder
	{
		private const int SSLIMIT = 18;

		private const int SBLIMIT = 32;

		private static readonly int[][] Slen;

		internal static readonly int[] Pretab;

		internal static readonly float[] TwoToNegativeHalfPow;

		internal static readonly float[] PowerTable;

		internal static readonly float[][] Io;

		internal static readonly float[] Tan12;

		private static int[][] _reorderTable;

		private static readonly float[] Cs;

		private static readonly float[] Ca;

		internal static readonly float[][] Win;

		internal static readonly int[][][] NrOfSfbBlock;

		private readonly ABuffer _Buffer;

		private readonly int _Channels;

		private readonly SynthesisFilter _Filter1;

		private readonly SynthesisFilter _Filter2;

		private readonly int _FirstChannel;

		private readonly Header _Header;

		private readonly int[] _Is1D;

		private readonly float[][] _K;

		private readonly int _LastChannel;

		private readonly float[][][] _Lr;

		private readonly int _MaxGr;

		private readonly int[] _Nonzero;

		private readonly float[] _Out1D;

		private readonly float[][] _Prevblck;

		private readonly float[][][] _Ro;

		private readonly ScaleFactorData[] _Scalefac;

		private readonly SBI[] _SfBandIndex;

		private readonly int _Sfreq;

		private readonly Layer3SideInfo _SideInfo;

		private readonly Bitstream _Stream;

		private readonly int _WhichChannels;

		private BitReserve _BitReserve;

		private int _CheckSumHuff;

		private int _FrameStart;

		internal int[] IsPos;

		internal float[] IsRatio;

		private int[] _NewSlen;

		private int _Part2Start;

		internal float[] Rawout;

		private float[] _Samples1;

		private float[] _Samples2;

		internal int[] ScalefacBuffer;

		internal ScaleFactorTable Sftable;

		internal float[] TsOutCopy;

		internal int[] V = new int[1];

		internal int[] W = new int[1];

		internal int[] X = new int[1];

		internal int[] Y = new int[1];

		static LayerIIIDecoder()
		{
			Slen = new int[2][]
			{
				new int[16]
				{
					0, 0, 0, 0, 3, 1, 1, 1, 2, 2,
					2, 3, 3, 3, 4, 4
				},
				new int[16]
				{
					0, 1, 2, 3, 0, 1, 2, 3, 1, 2,
					3, 1, 2, 3, 2, 3
				}
			};
			Pretab = new int[22]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 1, 1, 1, 1, 2, 2, 3, 3, 3,
				2, 0
			};
			TwoToNegativeHalfPow = new float[64]
			{
				1f,
				0.70710677f,
				0.5f,
				0.35355338f,
				0.25f,
				0.17677669f,
				0.125f,
				0.088388346f,
				0.0625f,
				0.044194173f,
				1f / 32f,
				0.022097087f,
				1f / 64f,
				0.011048543f,
				1f / 128f,
				0.0055242716f,
				0.00390625f,
				0.0027621358f,
				0.001953125f,
				0.0013810679f,
				0.0009765625f,
				0.00069053395f,
				0.00048828125f,
				0.00034526698f,
				0.00024414062f,
				0.00017263349f,
				0.00012207031f,
				8.6316744E-05f,
				6.1035156E-05f,
				4.3158372E-05f,
				3.0517578E-05f,
				2.1579186E-05f,
				1.5258789E-05f,
				1.0789593E-05f,
				7.6293945E-06f,
				5.3947965E-06f,
				3.8146973E-06f,
				2.6973983E-06f,
				1.9073486E-06f,
				1.3486991E-06f,
				9.536743E-07f,
				6.7434956E-07f,
				4.7683716E-07f,
				3.3717478E-07f,
				2.3841858E-07f,
				1.6858739E-07f,
				1.1920929E-07f,
				8.4293696E-08f,
				5.9604645E-08f,
				4.2146848E-08f,
				2.9802322E-08f,
				2.1073424E-08f,
				1.4901161E-08f,
				1.0536712E-08f,
				7.450581E-09f,
				5.268356E-09f,
				3.7252903E-09f,
				2.634178E-09f,
				1.8626451E-09f,
				1.317089E-09f,
				9.313226E-10f,
				6.585445E-10f,
				4.656613E-10f,
				3.2927225E-10f
			};
			Io = new float[2][]
			{
				new float[32]
				{
					1f,
					0.8408964f,
					0.70710677f,
					0.59460354f,
					0.5f,
					0.4204482f,
					0.35355338f,
					0.29730177f,
					0.25f,
					0.2102241f,
					0.17677669f,
					0.14865088f,
					0.125f,
					0.10511205f,
					0.088388346f,
					0.07432544f,
					0.0625f,
					0.052556027f,
					0.044194173f,
					0.03716272f,
					1f / 32f,
					0.026278013f,
					0.022097087f,
					0.01858136f,
					1f / 64f,
					0.013139007f,
					0.011048543f,
					0.00929068f,
					1f / 128f,
					0.0065695033f,
					0.0055242716f,
					0.00464534f
				},
				new float[32]
				{
					1f,
					0.70710677f,
					0.5f,
					0.35355338f,
					0.25f,
					0.17677669f,
					0.125f,
					0.088388346f,
					0.0625f,
					0.044194173f,
					1f / 32f,
					0.022097087f,
					1f / 64f,
					0.011048543f,
					1f / 128f,
					0.0055242716f,
					0.00390625f,
					0.0027621358f,
					0.001953125f,
					0.0013810679f,
					0.0009765625f,
					0.00069053395f,
					0.00048828125f,
					0.00034526698f,
					0.00024414062f,
					0.00017263349f,
					0.00012207031f,
					8.6316744E-05f,
					6.1035156E-05f,
					4.3158372E-05f,
					3.0517578E-05f,
					2.1579186E-05f
				}
			};
			Tan12 = new float[16]
			{
				0f, 0.2679492f, 0.57735026f, 1f, 1.7320508f, 3.732051f, 1E+11f, -3.732051f, -1.7320508f, -1f,
				-0.57735026f, -0.2679492f, 0f, 0.2679492f, 0.57735026f, 1f
			};
			Cs = new float[8] { 0.8574929f, 0.881742f, 0.94962865f, 0.9833146f, 0.9955178f, 0.9991606f, 0.9998992f, 0.99999315f };
			Ca = new float[8] { -0.51449573f, -0.47173196f, -0.31337744f, -0.1819132f, -0.09457419f, -0.040965583f, -0.014198569f, -0.0036999746f };
			Win = new float[4][]
			{
				new float[36]
				{
					-0.016141215f, -0.05360318f, -0.100707136f, -0.16280818f, -0.5f, -0.38388735f, -0.6206114f, -1.1659756f, -3.8720753f, -4.225629f,
					-1.519529f, -0.97416484f, -0.73744076f, -1.2071068f, -0.5163616f, -0.45426053f, -0.40715656f, -0.3696946f, -0.3387627f, -0.31242222f,
					-0.28939587f, -0.26880082f, -0.5f, -0.23251417f, -0.21596715f, -0.20004979f, -0.18449493f, -0.16905846f, -0.15350361f, -0.13758625f,
					-0.12103922f, -0.20710678f, -0.084752575f, -0.06415752f, -0.041131172f, -0.014790705f
				},
				new float[36]
				{
					-0.016141215f, -0.05360318f, -0.100707136f, -0.16280818f, -0.5f, -0.38388735f, -0.6206114f, -1.1659756f, -3.8720753f, -4.225629f,
					-1.519529f, -0.97416484f, -0.73744076f, -1.2071068f, -0.5163616f, -0.45426053f, -0.40715656f, -0.3696946f, -0.33908543f, -0.3151181f,
					-0.29642227f, -0.28184548f, -0.5411961f, -0.2621323f, -0.25387916f, -0.2329629f, -0.19852729f, -0.15233535f, -0.0964964f, -0.03342383f,
					0f, 0f, 0f, 0f, 0f, 0f
				},
				new float[36]
				{
					-0.0483008f, -0.15715657f, -0.28325045f, -0.42953748f, -1.2071068f, -0.8242648f, -1.1451749f, -1.769529f, -4.5470223f, -3.489053f,
					-0.7329629f, -0.15076515f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
					0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
					0f, 0f, 0f, 0f, 0f, 0f
				},
				new float[36]
				{
					0f, 0f, 0f, 0f, 0f, 0f, -0.15076514f, -0.7329629f, -3.489053f, -4.5470223f,
					-1.769529f, -1.1451749f, -0.8313774f, -1.306563f, -0.54142016f, -0.46528974f, -0.4106699f, -0.3700468f, -0.3387627f, -0.31242222f,
					-0.28939587f, -0.26880082f, -0.5f, -0.23251417f, -0.21596715f, -0.20004979f, -0.18449493f, -0.16905846f, -0.15350361f, -0.13758625f,
					-0.12103922f, -0.20710678f, -0.084752575f, -0.06415752f, -0.041131172f, -0.014790705f
				}
			};
			NrOfSfbBlock = new int[6][][]
			{
				new int[3][]
				{
					new int[4] { 6, 5, 5, 5 },
					new int[4] { 9, 9, 9, 9 },
					new int[4] { 6, 9, 9, 9 }
				},
				new int[3][]
				{
					new int[4] { 6, 5, 7, 3 },
					new int[4] { 9, 9, 12, 6 },
					new int[4] { 6, 9, 12, 6 }
				},
				new int[3][]
				{
					new int[4] { 11, 10, 0, 0 },
					new int[4] { 18, 18, 0, 0 },
					new int[4] { 15, 18, 0, 0 }
				},
				new int[3][]
				{
					new int[4] { 7, 7, 7, 0 },
					new int[4] { 12, 12, 12, 0 },
					new int[4] { 6, 15, 12, 0 }
				},
				new int[3][]
				{
					new int[4] { 6, 6, 6, 3 },
					new int[4] { 12, 9, 9, 6 },
					new int[4] { 6, 12, 9, 6 }
				},
				new int[3][]
				{
					new int[4] { 8, 8, 5, 0 },
					new int[4] { 15, 12, 9, 0 },
					new int[4] { 6, 18, 9, 0 }
				}
			};
			PowerTable = CreatePowerTable();
		}

		internal LayerIIIDecoder(Bitstream stream, Header header, SynthesisFilter filtera, SynthesisFilter filterb, ABuffer buffer, int whichCh)
		{
			Huffman.Initialize();
			InitBlock();
			_Is1D = new int[580];
			_Ro = new float[2][][];
			for (int i = 0; i < 2; i++)
			{
				_Ro[i] = new float[32][];
				for (int j = 0; j < 32; j++)
				{
					_Ro[i][j] = new float[18];
				}
			}
			_Lr = new float[2][][];
			for (int k = 0; k < 2; k++)
			{
				_Lr[k] = new float[32][];
				for (int l = 0; l < 32; l++)
				{
					_Lr[k][l] = new float[18];
				}
			}
			_Out1D = new float[576];
			_Prevblck = new float[2][];
			for (int m = 0; m < 2; m++)
			{
				_Prevblck[m] = new float[576];
			}
			_K = new float[2][];
			for (int n = 0; n < 2; n++)
			{
				_K[n] = new float[576];
			}
			_Nonzero = new int[2];
			_Scalefac = new ScaleFactorData[2]
			{
				new ScaleFactorData(),
				new ScaleFactorData()
			};
			_SfBandIndex = new SBI[9];
			int[] thel = new int[23]
			{
				0, 6, 12, 18, 24, 30, 36, 44, 54, 66,
				80, 96, 116, 140, 168, 200, 238, 284, 336, 396,
				464, 522, 576
			};
			int[] thes = new int[14]
			{
				0, 4, 8, 12, 18, 24, 32, 42, 56, 74,
				100, 132, 174, 192
			};
			int[] thel2 = new int[23]
			{
				0, 6, 12, 18, 24, 30, 36, 44, 54, 66,
				80, 96, 114, 136, 162, 194, 232, 278, 330, 394,
				464, 540, 576
			};
			int[] thes2 = new int[14]
			{
				0, 4, 8, 12, 18, 26, 36, 48, 62, 80,
				104, 136, 180, 192
			};
			int[] thel3 = new int[23]
			{
				0, 6, 12, 18, 24, 30, 36, 44, 54, 66,
				80, 96, 116, 140, 168, 200, 238, 284, 336, 396,
				464, 522, 576
			};
			int[] thes3 = new int[14]
			{
				0, 4, 8, 12, 18, 26, 36, 48, 62, 80,
				104, 134, 174, 192
			};
			int[] thel4 = new int[23]
			{
				0, 4, 8, 12, 16, 20, 24, 30, 36, 44,
				52, 62, 74, 90, 110, 134, 162, 196, 238, 288,
				342, 418, 576
			};
			int[] thes4 = new int[14]
			{
				0, 4, 8, 12, 16, 22, 30, 40, 52, 66,
				84, 106, 136, 192
			};
			int[] thel5 = new int[23]
			{
				0, 4, 8, 12, 16, 20, 24, 30, 36, 42,
				50, 60, 72, 88, 106, 128, 156, 190, 230, 276,
				330, 384, 576
			};
			int[] thes5 = new int[14]
			{
				0, 4, 8, 12, 16, 22, 28, 38, 50, 64,
				80, 100, 126, 192
			};
			int[] thel6 = new int[23]
			{
				0, 4, 8, 12, 16, 20, 24, 30, 36, 44,
				54, 66, 82, 102, 126, 156, 194, 240, 296, 364,
				448, 550, 576
			};
			int[] thes6 = new int[14]
			{
				0, 4, 8, 12, 16, 22, 30, 42, 58, 78,
				104, 138, 180, 192
			};
			int[] thel7 = new int[23]
			{
				0, 6, 12, 18, 24, 30, 36, 44, 54, 66,
				80, 96, 116, 140, 168, 200, 238, 284, 336, 396,
				464, 522, 576
			};
			int[] thes7 = new int[14]
			{
				0, 4, 8, 12, 18, 26, 36, 48, 62, 80,
				104, 134, 174, 192
			};
			int[] thel8 = new int[23]
			{
				0, 6, 12, 18, 24, 30, 36, 44, 54, 66,
				80, 96, 116, 140, 168, 200, 238, 284, 336, 396,
				464, 522, 576
			};
			int[] thes8 = new int[14]
			{
				0, 4, 8, 12, 18, 26, 36, 48, 62, 80,
				104, 134, 174, 192
			};
			int[] thel9 = new int[23]
			{
				0, 12, 24, 36, 48, 60, 72, 88, 108, 132,
				160, 192, 232, 280, 336, 400, 476, 566, 568, 570,
				572, 574, 576
			};
			int[] thes9 = new int[14]
			{
				0, 8, 16, 24, 36, 52, 72, 96, 124, 160,
				162, 164, 166, 192
			};
			_SfBandIndex[0] = new SBI(thel, thes);
			_SfBandIndex[1] = new SBI(thel2, thes2);
			_SfBandIndex[2] = new SBI(thel3, thes3);
			_SfBandIndex[3] = new SBI(thel4, thes4);
			_SfBandIndex[4] = new SBI(thel5, thes5);
			_SfBandIndex[5] = new SBI(thel6, thes6);
			_SfBandIndex[6] = new SBI(thel7, thes7);
			_SfBandIndex[7] = new SBI(thel8, thes8);
			_SfBandIndex[8] = new SBI(thel9, thes9);
			if (_reorderTable == null)
			{
				_reorderTable = new int[9][];
				for (int num = 0; num < 9; num++)
				{
					_reorderTable[num] = Reorder(_SfBandIndex[num].S);
				}
			}
			int[] thel10 = new int[5] { 0, 6, 11, 16, 21 };
			int[] thes10 = new int[3] { 0, 6, 12 };
			Sftable = new ScaleFactorTable(this, thel10, thes10);
			ScalefacBuffer = new int[54];
			_Stream = stream;
			_Header = header;
			_Filter1 = filtera;
			_Filter2 = filterb;
			_Buffer = buffer;
			_WhichChannels = whichCh;
			_FrameStart = 0;
			_Channels = ((_Header.Mode() == 3) ? 1 : 2);
			_MaxGr = ((_Header.Version() != 1) ? 1 : 2);
			_Sfreq = _Header.sample_frequency() + ((_Header.Version() == 1) ? 3 : ((_Header.Version() == 2) ? 6 : 0));
			if (_Channels == 2)
			{
				switch (_WhichChannels)
				{
				case 1:
				case 3:
					_FirstChannel = (_LastChannel = 0);
					break;
				case 2:
					_FirstChannel = (_LastChannel = 1);
					break;
				default:
					_FirstChannel = 0;
					_LastChannel = 1;
					break;
				}
			}
			else
			{
				_FirstChannel = (_LastChannel = 0);
			}
			for (int num2 = 0; num2 < 2; num2++)
			{
				for (int num3 = 0; num3 < 576; num3++)
				{
					_Prevblck[num2][num3] = 0f;
				}
			}
			_Nonzero[0] = (_Nonzero[1] = 576);
			_BitReserve = new BitReserve();
			_SideInfo = new Layer3SideInfo();
		}

		public void DecodeFrame()
		{
			Decode();
		}

		private void InitBlock()
		{
			Rawout = new float[36];
			TsOutCopy = new float[18];
			IsRatio = new float[576];
			IsPos = new int[576];
			_NewSlen = new int[4];
			_Samples2 = new float[32];
			_Samples1 = new float[32];
		}

		internal void SeekNotify()
		{
			_FrameStart = 0;
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 576; j++)
				{
					_Prevblck[i][j] = 0f;
				}
			}
			_BitReserve = new BitReserve();
		}

		internal void Decode()
		{
			int num = _Header.Slots();
			ReadSideInfo();
			for (int i = 0; i < num; i++)
			{
				_BitReserve.PutBuffer(_Stream.GetBitsFromBuffer(8));
			}
			int num2 = SupportClass.URShift(_BitReserve.HssTell(), 3);
			int num3;
			if ((num3 = _BitReserve.HssTell() & 7) != 0)
			{
				_BitReserve.ReadBits(8 - num3);
				num2++;
			}
			int num4 = _FrameStart - num2 - _SideInfo.MainDataBegin;
			_FrameStart += num;
			if (num4 < 0)
			{
				return;
			}
			if (num2 > 4096)
			{
				_FrameStart -= 4096;
				_BitReserve.RewindStreamBytes(4096);
			}
			while (num4 > 0)
			{
				_BitReserve.ReadBits(8);
				num4--;
			}
			for (int j = 0; j < _MaxGr; j++)
			{
				for (int k = 0; k < _Channels; k++)
				{
					_Part2Start = _BitReserve.HssTell();
					if (_Header.Version() == 1)
					{
						ReadScaleFactors(k, j);
					}
					else
					{
						GLSFScaleFactors(k, j);
					}
					HuffmanDecode(k, j);
					DequantizeSample(_Ro[k], k, j);
				}
				Stereo(j);
				if (_WhichChannels == 3 && _Channels > 1)
				{
					DoDownMix();
				}
				for (int k = _FirstChannel; k <= _LastChannel; k++)
				{
					Reorder(_Lr[k], k, j);
					Antialias(k, j);
					Hybrid(k, j);
					for (int l = 18; l < 576; l += 36)
					{
						for (int m = 1; m < 18; m += 2)
						{
							_Out1D[l + m] = 0f - _Out1D[l + m];
						}
					}
					if (k == 0 || _WhichChannels == 2)
					{
						for (int m = 0; m < 18; m++)
						{
							int num5 = 0;
							for (int l = 0; l < 576; l += 18)
							{
								_Samples1[num5] = _Out1D[l + m];
								num5++;
							}
							_Filter1.AddSamples(_Samples1);
							_Filter1.calculate_pc_samples(_Buffer);
						}
						continue;
					}
					for (int m = 0; m < 18; m++)
					{
						int num5 = 0;
						for (int l = 0; l < 576; l += 18)
						{
							_Samples2[num5] = _Out1D[l + m];
							num5++;
						}
						_Filter2.AddSamples(_Samples2);
						_Filter2.calculate_pc_samples(_Buffer);
					}
				}
			}
			_Buffer.WriteBuffer(1);
		}

		private bool ReadSideInfo()
		{
			if (_Header.Version() == 1)
			{
				_SideInfo.MainDataBegin = _Stream.GetBitsFromBuffer(9);
				if (_Channels == 1)
				{
					_SideInfo.PrivateBits = _Stream.GetBitsFromBuffer(5);
				}
				else
				{
					_SideInfo.PrivateBits = _Stream.GetBitsFromBuffer(3);
				}
				for (int i = 0; i < _Channels; i++)
				{
					_SideInfo.Channels[i].ScaleFactorBits[0] = _Stream.GetBitsFromBuffer(1);
					_SideInfo.Channels[i].ScaleFactorBits[1] = _Stream.GetBitsFromBuffer(1);
					_SideInfo.Channels[i].ScaleFactorBits[2] = _Stream.GetBitsFromBuffer(1);
					_SideInfo.Channels[i].ScaleFactorBits[3] = _Stream.GetBitsFromBuffer(1);
				}
				for (int j = 0; j < 2; j++)
				{
					for (int i = 0; i < _Channels; i++)
					{
						_SideInfo.Channels[i].Granules[j].Part23Length = _Stream.GetBitsFromBuffer(12);
						_SideInfo.Channels[i].Granules[j].BigValues = _Stream.GetBitsFromBuffer(9);
						_SideInfo.Channels[i].Granules[j].GlobalGain = _Stream.GetBitsFromBuffer(8);
						_SideInfo.Channels[i].Granules[j].ScaleFacCompress = _Stream.GetBitsFromBuffer(4);
						_SideInfo.Channels[i].Granules[j].WindowSwitchingFlag = _Stream.GetBitsFromBuffer(1);
						if (_SideInfo.Channels[i].Granules[j].WindowSwitchingFlag != 0)
						{
							_SideInfo.Channels[i].Granules[j].BlockType = _Stream.GetBitsFromBuffer(2);
							_SideInfo.Channels[i].Granules[j].MixedBlockFlag = _Stream.GetBitsFromBuffer(1);
							_SideInfo.Channels[i].Granules[j].TableSelect[0] = _Stream.GetBitsFromBuffer(5);
							_SideInfo.Channels[i].Granules[j].TableSelect[1] = _Stream.GetBitsFromBuffer(5);
							_SideInfo.Channels[i].Granules[j].SubblockGain[0] = _Stream.GetBitsFromBuffer(3);
							_SideInfo.Channels[i].Granules[j].SubblockGain[1] = _Stream.GetBitsFromBuffer(3);
							_SideInfo.Channels[i].Granules[j].SubblockGain[2] = _Stream.GetBitsFromBuffer(3);
							if (_SideInfo.Channels[i].Granules[j].BlockType == 0)
							{
								return false;
							}
							if (_SideInfo.Channels[i].Granules[j].BlockType == 2 && _SideInfo.Channels[i].Granules[j].MixedBlockFlag == 0)
							{
								_SideInfo.Channels[i].Granules[j].Region0Count = 8;
							}
							else
							{
								_SideInfo.Channels[i].Granules[j].Region0Count = 7;
							}
							_SideInfo.Channels[i].Granules[j].Region1Count = 20 - _SideInfo.Channels[i].Granules[j].Region0Count;
						}
						else
						{
							_SideInfo.Channels[i].Granules[j].TableSelect[0] = _Stream.GetBitsFromBuffer(5);
							_SideInfo.Channels[i].Granules[j].TableSelect[1] = _Stream.GetBitsFromBuffer(5);
							_SideInfo.Channels[i].Granules[j].TableSelect[2] = _Stream.GetBitsFromBuffer(5);
							_SideInfo.Channels[i].Granules[j].Region0Count = _Stream.GetBitsFromBuffer(4);
							_SideInfo.Channels[i].Granules[j].Region1Count = _Stream.GetBitsFromBuffer(3);
							_SideInfo.Channels[i].Granules[j].BlockType = 0;
						}
						_SideInfo.Channels[i].Granules[j].Preflag = _Stream.GetBitsFromBuffer(1);
						_SideInfo.Channels[i].Granules[j].ScaleFacScale = _Stream.GetBitsFromBuffer(1);
						_SideInfo.Channels[i].Granules[j].Count1TableSelect = _Stream.GetBitsFromBuffer(1);
					}
				}
			}
			else
			{
				_SideInfo.MainDataBegin = _Stream.GetBitsFromBuffer(8);
				if (_Channels == 1)
				{
					_SideInfo.PrivateBits = _Stream.GetBitsFromBuffer(1);
				}
				else
				{
					_SideInfo.PrivateBits = _Stream.GetBitsFromBuffer(2);
				}
				for (int i = 0; i < _Channels; i++)
				{
					_SideInfo.Channels[i].Granules[0].Part23Length = _Stream.GetBitsFromBuffer(12);
					_SideInfo.Channels[i].Granules[0].BigValues = _Stream.GetBitsFromBuffer(9);
					_SideInfo.Channels[i].Granules[0].GlobalGain = _Stream.GetBitsFromBuffer(8);
					_SideInfo.Channels[i].Granules[0].ScaleFacCompress = _Stream.GetBitsFromBuffer(9);
					_SideInfo.Channels[i].Granules[0].WindowSwitchingFlag = _Stream.GetBitsFromBuffer(1);
					if (_SideInfo.Channels[i].Granules[0].WindowSwitchingFlag != 0)
					{
						_SideInfo.Channels[i].Granules[0].BlockType = _Stream.GetBitsFromBuffer(2);
						_SideInfo.Channels[i].Granules[0].MixedBlockFlag = _Stream.GetBitsFromBuffer(1);
						_SideInfo.Channels[i].Granules[0].TableSelect[0] = _Stream.GetBitsFromBuffer(5);
						_SideInfo.Channels[i].Granules[0].TableSelect[1] = _Stream.GetBitsFromBuffer(5);
						_SideInfo.Channels[i].Granules[0].SubblockGain[0] = _Stream.GetBitsFromBuffer(3);
						_SideInfo.Channels[i].Granules[0].SubblockGain[1] = _Stream.GetBitsFromBuffer(3);
						_SideInfo.Channels[i].Granules[0].SubblockGain[2] = _Stream.GetBitsFromBuffer(3);
						if (_SideInfo.Channels[i].Granules[0].BlockType == 0)
						{
							return false;
						}
						if (_SideInfo.Channels[i].Granules[0].BlockType == 2 && _SideInfo.Channels[i].Granules[0].MixedBlockFlag == 0)
						{
							_SideInfo.Channels[i].Granules[0].Region0Count = 8;
						}
						else
						{
							_SideInfo.Channels[i].Granules[0].Region0Count = 7;
							_SideInfo.Channels[i].Granules[0].Region1Count = 20 - _SideInfo.Channels[i].Granules[0].Region0Count;
						}
					}
					else
					{
						_SideInfo.Channels[i].Granules[0].TableSelect[0] = _Stream.GetBitsFromBuffer(5);
						_SideInfo.Channels[i].Granules[0].TableSelect[1] = _Stream.GetBitsFromBuffer(5);
						_SideInfo.Channels[i].Granules[0].TableSelect[2] = _Stream.GetBitsFromBuffer(5);
						_SideInfo.Channels[i].Granules[0].Region0Count = _Stream.GetBitsFromBuffer(4);
						_SideInfo.Channels[i].Granules[0].Region1Count = _Stream.GetBitsFromBuffer(3);
						_SideInfo.Channels[i].Granules[0].BlockType = 0;
					}
					_SideInfo.Channels[i].Granules[0].ScaleFacScale = _Stream.GetBitsFromBuffer(1);
					_SideInfo.Channels[i].Granules[0].Count1TableSelect = _Stream.GetBitsFromBuffer(1);
				}
			}
			return true;
		}

		private void ReadScaleFactors(int ch, int gr)
		{
			GranuleInfo granuleInfo = _SideInfo.Channels[ch].Granules[gr];
			int scaleFacCompress = granuleInfo.ScaleFacCompress;
			int n = Slen[0][scaleFacCompress];
			int n2 = Slen[1][scaleFacCompress];
			if (granuleInfo.WindowSwitchingFlag != 0 && granuleInfo.BlockType == 2)
			{
				if (granuleInfo.MixedBlockFlag != 0)
				{
					int i;
					for (i = 0; i < 8; i++)
					{
						_Scalefac[ch].L[i] = _BitReserve.ReadBits(Slen[0][granuleInfo.ScaleFacCompress]);
					}
					for (i = 3; i < 6; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							_Scalefac[ch].S[j][i] = _BitReserve.ReadBits(Slen[0][granuleInfo.ScaleFacCompress]);
						}
					}
					for (i = 6; i < 12; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							_Scalefac[ch].S[j][i] = _BitReserve.ReadBits(Slen[1][granuleInfo.ScaleFacCompress]);
						}
					}
					i = 12;
					for (int j = 0; j < 3; j++)
					{
						_Scalefac[ch].S[j][i] = 0;
					}
					return;
				}
				_Scalefac[ch].S[0][0] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[1][0] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[2][0] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[0][1] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[1][1] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[2][1] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[0][2] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[1][2] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[2][2] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[0][3] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[1][3] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[2][3] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[0][4] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[1][4] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[2][4] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[0][5] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[1][5] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[2][5] = _BitReserve.ReadBits(n);
				_Scalefac[ch].S[0][6] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[1][6] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[2][6] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[0][7] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[1][7] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[2][7] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[0][8] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[1][8] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[2][8] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[0][9] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[1][9] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[2][9] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[0][10] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[1][10] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[2][10] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[0][11] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[1][11] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[2][11] = _BitReserve.ReadBits(n2);
				_Scalefac[ch].S[0][12] = 0;
				_Scalefac[ch].S[1][12] = 0;
				_Scalefac[ch].S[2][12] = 0;
			}
			else
			{
				if (_SideInfo.Channels[ch].ScaleFactorBits[0] == 0 || gr == 0)
				{
					_Scalefac[ch].L[0] = _BitReserve.ReadBits(n);
					_Scalefac[ch].L[1] = _BitReserve.ReadBits(n);
					_Scalefac[ch].L[2] = _BitReserve.ReadBits(n);
					_Scalefac[ch].L[3] = _BitReserve.ReadBits(n);
					_Scalefac[ch].L[4] = _BitReserve.ReadBits(n);
					_Scalefac[ch].L[5] = _BitReserve.ReadBits(n);
				}
				if (_SideInfo.Channels[ch].ScaleFactorBits[1] == 0 || gr == 0)
				{
					_Scalefac[ch].L[6] = _BitReserve.ReadBits(n);
					_Scalefac[ch].L[7] = _BitReserve.ReadBits(n);
					_Scalefac[ch].L[8] = _BitReserve.ReadBits(n);
					_Scalefac[ch].L[9] = _BitReserve.ReadBits(n);
					_Scalefac[ch].L[10] = _BitReserve.ReadBits(n);
				}
				if (_SideInfo.Channels[ch].ScaleFactorBits[2] == 0 || gr == 0)
				{
					_Scalefac[ch].L[11] = _BitReserve.ReadBits(n2);
					_Scalefac[ch].L[12] = _BitReserve.ReadBits(n2);
					_Scalefac[ch].L[13] = _BitReserve.ReadBits(n2);
					_Scalefac[ch].L[14] = _BitReserve.ReadBits(n2);
					_Scalefac[ch].L[15] = _BitReserve.ReadBits(n2);
				}
				if (_SideInfo.Channels[ch].ScaleFactorBits[3] == 0 || gr == 0)
				{
					_Scalefac[ch].L[16] = _BitReserve.ReadBits(n2);
					_Scalefac[ch].L[17] = _BitReserve.ReadBits(n2);
					_Scalefac[ch].L[18] = _BitReserve.ReadBits(n2);
					_Scalefac[ch].L[19] = _BitReserve.ReadBits(n2);
					_Scalefac[ch].L[20] = _BitReserve.ReadBits(n2);
				}
				_Scalefac[ch].L[21] = 0;
				_Scalefac[ch].L[22] = 0;
			}
		}

		private void GetLSFScaleData(int ch, int gr)
		{
			int num = _Header.mode_extension();
			int num2 = 0;
			GranuleInfo granuleInfo = _SideInfo.Channels[ch].Granules[gr];
			int scaleFacCompress = granuleInfo.ScaleFacCompress;
			int num3 = ((granuleInfo.BlockType == 2) ? ((granuleInfo.MixedBlockFlag == 0) ? 1 : ((granuleInfo.MixedBlockFlag == 1) ? 2 : 0)) : 0);
			if ((num != 1 && num != 3) || ch != 1)
			{
				if (scaleFacCompress < 400)
				{
					_NewSlen[0] = SupportClass.URShift(scaleFacCompress, 4) / 5;
					_NewSlen[1] = SupportClass.URShift(scaleFacCompress, 4) % 5;
					_NewSlen[2] = SupportClass.URShift(scaleFacCompress & 0xF, 2);
					_NewSlen[3] = scaleFacCompress & 3;
					_SideInfo.Channels[ch].Granules[gr].Preflag = 0;
					num2 = 0;
				}
				else if (scaleFacCompress < 500)
				{
					_NewSlen[0] = SupportClass.URShift(scaleFacCompress - 400, 2) / 5;
					_NewSlen[1] = SupportClass.URShift(scaleFacCompress - 400, 2) % 5;
					_NewSlen[2] = (scaleFacCompress - 400) & 3;
					_NewSlen[3] = 0;
					_SideInfo.Channels[ch].Granules[gr].Preflag = 0;
					num2 = 1;
				}
				else if (scaleFacCompress < 512)
				{
					_NewSlen[0] = (scaleFacCompress - 500) / 3;
					_NewSlen[1] = (scaleFacCompress - 500) % 3;
					_NewSlen[2] = 0;
					_NewSlen[3] = 0;
					_SideInfo.Channels[ch].Granules[gr].Preflag = 1;
					num2 = 2;
				}
			}
			if ((num == 1 || num == 3) && ch == 1)
			{
				int num4 = SupportClass.URShift(scaleFacCompress, 1);
				if (num4 < 180)
				{
					_NewSlen[0] = num4 / 36;
					_NewSlen[1] = num4 % 36 / 6;
					_NewSlen[2] = num4 % 36 % 6;
					_NewSlen[3] = 0;
					_SideInfo.Channels[ch].Granules[gr].Preflag = 0;
					num2 = 3;
				}
				else if (num4 < 244)
				{
					_NewSlen[0] = SupportClass.URShift((num4 - 180) & 0x3F, 4);
					_NewSlen[1] = SupportClass.URShift((num4 - 180) & 0xF, 2);
					_NewSlen[2] = (num4 - 180) & 3;
					_NewSlen[3] = 0;
					_SideInfo.Channels[ch].Granules[gr].Preflag = 0;
					num2 = 4;
				}
				else if (num4 < 255)
				{
					_NewSlen[0] = (num4 - 244) / 3;
					_NewSlen[1] = (num4 - 244) % 3;
					_NewSlen[2] = 0;
					_NewSlen[3] = 0;
					_SideInfo.Channels[ch].Granules[gr].Preflag = 0;
					num2 = 5;
				}
			}
			for (int i = 0; i < 45; i++)
			{
				ScalefacBuffer[i] = 0;
			}
			int num5 = 0;
			for (int j = 0; j < 4; j++)
			{
				for (int k = 0; k < NrOfSfbBlock[num2][num3][j]; k++)
				{
					ScalefacBuffer[num5] = ((_NewSlen[j] != 0) ? _BitReserve.ReadBits(_NewSlen[j]) : 0);
					num5++;
				}
			}
		}

		private void GLSFScaleFactors(int ch, int gr)
		{
			int num = 0;
			GranuleInfo granuleInfo = _SideInfo.Channels[ch].Granules[gr];
			GetLSFScaleData(ch, gr);
			if (granuleInfo.WindowSwitchingFlag != 0 && granuleInfo.BlockType == 2)
			{
				if (granuleInfo.MixedBlockFlag != 0)
				{
					for (int i = 0; i < 8; i++)
					{
						_Scalefac[ch].L[i] = ScalefacBuffer[num];
						num++;
					}
					for (int i = 3; i < 12; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							_Scalefac[ch].S[j][i] = ScalefacBuffer[num];
							num++;
						}
					}
					for (int j = 0; j < 3; j++)
					{
						_Scalefac[ch].S[j][12] = 0;
					}
					return;
				}
				for (int i = 0; i < 12; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						_Scalefac[ch].S[j][i] = ScalefacBuffer[num];
						num++;
					}
				}
				for (int j = 0; j < 3; j++)
				{
					_Scalefac[ch].S[j][12] = 0;
				}
			}
			else
			{
				for (int i = 0; i < 21; i++)
				{
					_Scalefac[ch].L[i] = ScalefacBuffer[num];
					num++;
				}
				_Scalefac[ch].L[21] = 0;
				_Scalefac[ch].L[22] = 0;
			}
		}

		private void HuffmanDecode(int ch, int gr)
		{
			X[0] = 0;
			Y[0] = 0;
			V[0] = 0;
			W[0] = 0;
			int num = _Part2Start + _SideInfo.Channels[ch].Granules[gr].Part23Length;
			int num2;
			int num3;
			if (_SideInfo.Channels[ch].Granules[gr].WindowSwitchingFlag != 0 && _SideInfo.Channels[ch].Granules[gr].BlockType == 2)
			{
				num2 = ((_Sfreq == 8) ? 72 : 36);
				num3 = 576;
			}
			else
			{
				int num4 = _SideInfo.Channels[ch].Granules[gr].Region0Count + 1;
				int num5 = num4 + _SideInfo.Channels[ch].Granules[gr].Region1Count + 1;
				if (num5 > _SfBandIndex[_Sfreq].L.Length - 1)
				{
					num5 = _SfBandIndex[_Sfreq].L.Length - 1;
				}
				num2 = _SfBandIndex[_Sfreq].L[num4];
				num3 = _SfBandIndex[_Sfreq].L[num5];
			}
			int i = 0;
			Huffman h;
			for (int j = 0; j < _SideInfo.Channels[ch].Granules[gr].BigValues << 1; j += 2)
			{
				h = ((j >= num2) ? ((j >= num3) ? Huffman.HuffmanTable[_SideInfo.Channels[ch].Granules[gr].TableSelect[2]] : Huffman.HuffmanTable[_SideInfo.Channels[ch].Granules[gr].TableSelect[1]]) : Huffman.HuffmanTable[_SideInfo.Channels[ch].Granules[gr].TableSelect[0]]);
				Huffman.Decode(h, X, Y, V, W, _BitReserve);
				_Is1D[i++] = X[0];
				_Is1D[i++] = Y[0];
				_CheckSumHuff = _CheckSumHuff + X[0] + Y[0];
			}
			h = Huffman.HuffmanTable[_SideInfo.Channels[ch].Granules[gr].Count1TableSelect + 32];
			int num6 = _BitReserve.HssTell();
			while (num6 < num && i < 576)
			{
				Huffman.Decode(h, X, Y, V, W, _BitReserve);
				_Is1D[i++] = V[0];
				_Is1D[i++] = W[0];
				_Is1D[i++] = X[0];
				_Is1D[i++] = Y[0];
				_CheckSumHuff = _CheckSumHuff + V[0] + W[0] + X[0] + Y[0];
				num6 = _BitReserve.HssTell();
			}
			if (num6 > num)
			{
				_BitReserve.RewindStreamBits(num6 - num);
				i -= 4;
			}
			num6 = _BitReserve.HssTell();
			if (num6 < num)
			{
				_BitReserve.ReadBits(num - num6);
			}
			if (i < 576)
			{
				_Nonzero[ch] = i;
			}
			else
			{
				_Nonzero[ch] = 576;
			}
			if (i < 0)
			{
				i = 0;
			}
			for (; i < 576; i++)
			{
				_Is1D[i] = 0;
			}
		}

		private void GetKStereoValues(int isPos, int ioType, int i)
		{
			if (isPos == 0)
			{
				_K[0][i] = 1f;
				_K[1][i] = 1f;
			}
			else if ((isPos & 1) != 0)
			{
				_K[0][i] = Io[ioType][SupportClass.URShift(isPos + 1, 1)];
				_K[1][i] = 1f;
			}
			else
			{
				_K[0][i] = 1f;
				_K[1][i] = Io[ioType][SupportClass.URShift(isPos, 1)];
			}
		}

		private void DequantizeSample(float[][] xr, int ch, int gr)
		{
			GranuleInfo granuleInfo = _SideInfo.Channels[ch].Granules[gr];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5;
			if (granuleInfo.WindowSwitchingFlag != 0 && granuleInfo.BlockType == 2)
			{
				if (granuleInfo.MixedBlockFlag != 0)
				{
					num5 = _SfBandIndex[_Sfreq].L[1];
				}
				else
				{
					num3 = _SfBandIndex[_Sfreq].S[1];
					num5 = (num3 << 2) - num3;
					num2 = 0;
				}
			}
			else
			{
				num5 = _SfBandIndex[_Sfreq].L[1];
			}
			float num6 = (float)Math.Pow(2.0, 0.25 * ((double)granuleInfo.GlobalGain - 210.0));
			for (int i = 0; i < _Nonzero[ch]; i++)
			{
				int num7 = i % 18;
				int num8 = (i - num7) / 18;
				if (_Is1D[i] == 0)
				{
					xr[num8][num7] = 0f;
					continue;
				}
				int num9 = _Is1D[i];
				if (num9 < PowerTable.Length)
				{
					if (_Is1D[i] > 0)
					{
						xr[num8][num7] = num6 * PowerTable[num9];
					}
					else if (-num9 < PowerTable.Length)
					{
						xr[num8][num7] = (0f - num6) * PowerTable[-num9];
					}
					else
					{
						xr[num8][num7] = (0f - num6) * (float)Math.Pow(-num9, 1.3333333333333333);
					}
				}
				else if (_Is1D[i] > 0)
				{
					xr[num8][num7] = num6 * (float)Math.Pow(num9, 1.3333333333333333);
				}
				else
				{
					xr[num8][num7] = (0f - num6) * (float)Math.Pow(-num9, 1.3333333333333333);
				}
			}
			for (int i = 0; i < _Nonzero[ch]; i++)
			{
				int num10 = i % 18;
				int num11 = (i - num10) / 18;
				if (num4 == num5)
				{
					if (granuleInfo.WindowSwitchingFlag != 0 && granuleInfo.BlockType == 2)
					{
						if (granuleInfo.MixedBlockFlag != 0)
						{
							if (num4 == _SfBandIndex[_Sfreq].L[8])
							{
								num5 = _SfBandIndex[_Sfreq].S[4];
								num5 = (num5 << 2) - num5;
								num = 3;
								num3 = _SfBandIndex[_Sfreq].S[4] - _SfBandIndex[_Sfreq].S[3];
								num2 = _SfBandIndex[_Sfreq].S[3];
								num2 = (num2 << 2) - num2;
							}
							else if (num4 < _SfBandIndex[_Sfreq].L[8])
							{
								num5 = _SfBandIndex[_Sfreq].L[++num + 1];
							}
							else
							{
								num5 = _SfBandIndex[_Sfreq].S[++num + 1];
								num5 = (num5 << 2) - num5;
								num2 = _SfBandIndex[_Sfreq].S[num];
								num3 = _SfBandIndex[_Sfreq].S[num + 1] - num2;
								num2 = (num2 << 2) - num2;
							}
						}
						else
						{
							num5 = _SfBandIndex[_Sfreq].S[++num + 1];
							num5 = (num5 << 2) - num5;
							num2 = _SfBandIndex[_Sfreq].S[num];
							num3 = _SfBandIndex[_Sfreq].S[num + 1] - num2;
							num2 = (num2 << 2) - num2;
						}
					}
					else
					{
						num5 = _SfBandIndex[_Sfreq].L[++num + 1];
					}
				}
				if (granuleInfo.WindowSwitchingFlag != 0 && ((granuleInfo.BlockType == 2 && granuleInfo.MixedBlockFlag == 0) || (granuleInfo.BlockType == 2 && granuleInfo.MixedBlockFlag != 0 && i >= 36)))
				{
					int num12 = (num4 - num2) / num3;
					int num13 = _Scalefac[ch].S[num12][num] << granuleInfo.ScaleFacScale;
					num13 += granuleInfo.SubblockGain[num12] << 2;
					xr[num11][num10] *= TwoToNegativeHalfPow[num13];
				}
				else
				{
					int num14 = _Scalefac[ch].L[num];
					if (granuleInfo.Preflag != 0)
					{
						num14 += Pretab[num];
					}
					num14 <<= granuleInfo.ScaleFacScale;
					xr[num11][num10] *= TwoToNegativeHalfPow[num14];
				}
				num4++;
			}
			for (int i = _Nonzero[ch]; i < 576; i++)
			{
				int num15 = i % 18;
				int num16 = (i - num15) / 18;
				if (num15 < 0)
				{
					num15 = 0;
				}
				if (num16 < 0)
				{
					num16 = 0;
				}
				xr[num16][num15] = 0f;
			}
		}

		private void Reorder(float[][] xr, int ch, int gr)
		{
			GranuleInfo granuleInfo = _SideInfo.Channels[ch].Granules[gr];
			if (granuleInfo.WindowSwitchingFlag != 0 && granuleInfo.BlockType == 2)
			{
				for (int i = 0; i < 576; i++)
				{
					_Out1D[i] = 0f;
				}
				if (granuleInfo.MixedBlockFlag != 0)
				{
					for (int i = 0; i < 36; i++)
					{
						int num = i % 18;
						int num2 = (i - num) / 18;
						_Out1D[i] = xr[num2][num];
					}
					int num3 = 3;
					int num4 = _SfBandIndex[_Sfreq].S[3];
					int num5 = _SfBandIndex[_Sfreq].S[4] - num4;
					while (num3 < 13)
					{
						int num6 = (num4 << 2) - num4;
						int num7 = 0;
						int num8 = 0;
						while (num7 < num5)
						{
							int num9 = num6 + num7;
							int num10 = num6 + num8;
							int num11 = num9 % 18;
							int num12 = (num9 - num11) / 18;
							_Out1D[num10] = xr[num12][num11];
							num9 += num5;
							num10++;
							num11 = num9 % 18;
							num12 = (num9 - num11) / 18;
							_Out1D[num10] = xr[num12][num11];
							num9 += num5;
							num10++;
							num11 = num9 % 18;
							num12 = (num9 - num11) / 18;
							_Out1D[num10] = xr[num12][num11];
							num7++;
							num8 += 3;
						}
						num3++;
						num4 = _SfBandIndex[_Sfreq].S[num3];
						num5 = _SfBandIndex[_Sfreq].S[num3 + 1] - num4;
					}
				}
				else
				{
					for (int i = 0; i < 576; i++)
					{
						int num13 = _reorderTable[_Sfreq][i];
						int num14 = num13 % 18;
						int num15 = (num13 - num14) / 18;
						_Out1D[i] = xr[num15][num14];
					}
				}
			}
			else
			{
				for (int i = 0; i < 576; i++)
				{
					int num16 = i % 18;
					int num17 = (i - num16) / 18;
					_Out1D[i] = xr[num17][num16];
				}
			}
		}

		private void Stereo(int gr)
		{
			if (_Channels == 1)
			{
				for (int i = 0; i < 32; i++)
				{
					for (int j = 0; j < 18; j += 3)
					{
						_Lr[0][i][j] = _Ro[0][i][j];
						_Lr[0][i][j + 1] = _Ro[0][i][j + 1];
						_Lr[0][i][j + 2] = _Ro[0][i][j + 2];
					}
				}
				return;
			}
			GranuleInfo granuleInfo = _SideInfo.Channels[0].Granules[gr];
			int num = _Header.mode_extension();
			bool flag = _Header.Mode() == 1 && (num & 2) != 0;
			bool flag2 = _Header.Mode() == 1 && (num & 1) != 0;
			bool flag3 = _Header.Version() == 0 || _Header.Version() == 2;
			int ioType = granuleInfo.ScaleFacCompress & 1;
			int k;
			for (k = 0; k < 576; k++)
			{
				IsPos[k] = 7;
				IsRatio[k] = 0f;
			}
			if (flag2)
			{
				if (granuleInfo.WindowSwitchingFlag != 0 && granuleInfo.BlockType == 2)
				{
					if (granuleInfo.MixedBlockFlag != 0)
					{
						int num2 = 0;
						for (int l = 0; l < 3; l++)
						{
							int num3 = 2;
							int num4;
							for (num4 = 12; num4 >= 3; num4--)
							{
								k = _SfBandIndex[_Sfreq].S[num4];
								int num5 = _SfBandIndex[_Sfreq].S[num4 + 1] - k;
								k = (k << 2) - k + (l + 1) * num5 - 1;
								while (num5 > 0)
								{
									if (_Ro[1][k / 18][k % 18] != 0f)
									{
										num3 = num4;
										num4 = -10;
										num5 = -10;
									}
									num5--;
									k--;
								}
							}
							num4 = num3 + 1;
							if (num4 > num2)
							{
								num2 = num4;
							}
							int num6;
							int i;
							for (; num4 < 12; num4++)
							{
								num6 = _SfBandIndex[_Sfreq].S[num4];
								i = _SfBandIndex[_Sfreq].S[num4 + 1] - num6;
								k = (num6 << 2) - num6 + l * i;
								while (i > 0)
								{
									IsPos[k] = _Scalefac[1].S[l][num4];
									if (IsPos[k] != 7)
									{
										if (flag3)
										{
											GetKStereoValues(IsPos[k], ioType, k);
										}
										else
										{
											IsRatio[k] = Tan12[IsPos[k]];
										}
									}
									k++;
									i--;
								}
							}
							num4 = _SfBandIndex[_Sfreq].S[10];
							i = _SfBandIndex[_Sfreq].S[11] - num4;
							num4 = (num4 << 2) - num4 + l * i;
							num6 = _SfBandIndex[_Sfreq].S[11];
							i = _SfBandIndex[_Sfreq].S[12] - num6;
							k = (num6 << 2) - num6 + l * i;
							while (i > 0)
							{
								IsPos[k] = IsPos[num4];
								if (flag3)
								{
									_K[0][k] = _K[0][num4];
									_K[1][k] = _K[1][num4];
								}
								else
								{
									IsRatio[k] = IsRatio[num4];
								}
								k++;
								i--;
							}
						}
						if (num2 <= 3)
						{
							k = 2;
							int j = 17;
							int i = -1;
							while (k >= 0)
							{
								if (_Ro[1][k][j] != 0f)
								{
									i = (k << 4) + (k << 1) + j;
									k = -1;
									continue;
								}
								j--;
								if (j < 0)
								{
									k--;
									j = 17;
								}
							}
							for (k = 0; _SfBandIndex[_Sfreq].L[k] <= i; k++)
							{
							}
							int num4 = k;
							k = _SfBandIndex[_Sfreq].L[k];
							for (; num4 < 8; num4++)
							{
								for (i = _SfBandIndex[_Sfreq].L[num4 + 1] - _SfBandIndex[_Sfreq].L[num4]; i > 0; i--)
								{
									IsPos[k] = _Scalefac[1].L[num4];
									if (IsPos[k] != 7)
									{
										if (flag3)
										{
											GetKStereoValues(IsPos[k], ioType, k);
										}
										else
										{
											IsRatio[k] = Tan12[IsPos[k]];
										}
									}
									k++;
								}
							}
						}
					}
					else
					{
						for (int m = 0; m < 3; m++)
						{
							int num7 = -1;
							int num6;
							int num4;
							for (num4 = 12; num4 >= 0; num4--)
							{
								num6 = _SfBandIndex[_Sfreq].S[num4];
								int num5 = _SfBandIndex[_Sfreq].S[num4 + 1] - num6;
								k = (num6 << 2) - num6 + (m + 1) * num5 - 1;
								while (num5 > 0)
								{
									if (_Ro[1][k / 18][k % 18] != 0f)
									{
										num7 = num4;
										num4 = -10;
										num5 = -10;
									}
									num5--;
									k--;
								}
							}
							int i;
							for (num4 = num7 + 1; num4 < 12; num4++)
							{
								num6 = _SfBandIndex[_Sfreq].S[num4];
								i = _SfBandIndex[_Sfreq].S[num4 + 1] - num6;
								k = (num6 << 2) - num6 + m * i;
								while (i > 0)
								{
									IsPos[k] = _Scalefac[1].S[m][num4];
									if (IsPos[k] != 7)
									{
										if (flag3)
										{
											GetKStereoValues(IsPos[k], ioType, k);
										}
										else
										{
											IsRatio[k] = Tan12[IsPos[k]];
										}
									}
									k++;
									i--;
								}
							}
							num6 = _SfBandIndex[_Sfreq].S[10];
							int num8 = _SfBandIndex[_Sfreq].S[11];
							i = num8 - num6;
							num4 = (num6 << 2) - num6 + m * i;
							i = _SfBandIndex[_Sfreq].S[12] - num8;
							k = (num8 << 2) - num8 + m * i;
							while (i > 0)
							{
								IsPos[k] = IsPos[num4];
								if (flag3)
								{
									_K[0][k] = _K[0][num4];
									_K[1][k] = _K[1][num4];
								}
								else
								{
									IsRatio[k] = IsRatio[num4];
								}
								k++;
								i--;
							}
						}
					}
				}
				else
				{
					k = 31;
					int j = 17;
					int i = 0;
					while (k >= 0)
					{
						if (_Ro[1][k][j] != 0f)
						{
							i = (k << 4) + (k << 1) + j;
							k = -1;
							continue;
						}
						j--;
						if (j < 0)
						{
							k--;
							j = 17;
						}
					}
					for (k = 0; _SfBandIndex[_Sfreq].L[k] <= i; k++)
					{
					}
					int num4 = k;
					k = _SfBandIndex[_Sfreq].L[k];
					for (; num4 < 21; num4++)
					{
						for (i = _SfBandIndex[_Sfreq].L[num4 + 1] - _SfBandIndex[_Sfreq].L[num4]; i > 0; i--)
						{
							IsPos[k] = _Scalefac[1].L[num4];
							if (IsPos[k] != 7)
							{
								if (flag3)
								{
									GetKStereoValues(IsPos[k], ioType, k);
								}
								else
								{
									IsRatio[k] = Tan12[IsPos[k]];
								}
							}
							k++;
						}
					}
					num4 = _SfBandIndex[_Sfreq].L[20];
					i = 576 - _SfBandIndex[_Sfreq].L[21];
					while (i > 0 && k < 576)
					{
						IsPos[k] = IsPos[num4];
						if (flag3)
						{
							_K[0][k] = _K[0][num4];
							_K[1][k] = _K[1][num4];
						}
						else
						{
							IsRatio[k] = IsRatio[num4];
						}
						k++;
						i--;
					}
				}
			}
			k = 0;
			for (int i = 0; i < 32; i++)
			{
				for (int j = 0; j < 18; j++)
				{
					if (IsPos[k] == 7)
					{
						if (flag)
						{
							_Lr[0][i][j] = (_Ro[0][i][j] + _Ro[1][i][j]) * 0.70710677f;
							_Lr[1][i][j] = (_Ro[0][i][j] - _Ro[1][i][j]) * 0.70710677f;
						}
						else
						{
							_Lr[0][i][j] = _Ro[0][i][j];
							_Lr[1][i][j] = _Ro[1][i][j];
						}
					}
					else if (flag2)
					{
						if (flag3)
						{
							_Lr[0][i][j] = _Ro[0][i][j] * _K[0][k];
							_Lr[1][i][j] = _Ro[0][i][j] * _K[1][k];
						}
						else
						{
							_Lr[1][i][j] = _Ro[0][i][j] / (1f + IsRatio[k]);
							_Lr[0][i][j] = _Lr[1][i][j] * IsRatio[k];
						}
					}
					k++;
				}
			}
		}

		private void Antialias(int ch, int gr)
		{
			GranuleInfo granuleInfo = _SideInfo.Channels[ch].Granules[gr];
			if (granuleInfo.WindowSwitchingFlag != 0 && granuleInfo.BlockType == 2 && granuleInfo.MixedBlockFlag == 0)
			{
				return;
			}
			int num = ((granuleInfo.WindowSwitchingFlag == 0 || granuleInfo.MixedBlockFlag == 0 || granuleInfo.BlockType != 2) ? 558 : 18);
			for (int i = 0; i < num; i += 18)
			{
				for (int j = 0; j < 8; j++)
				{
					int num2 = i + 17 - j;
					int num3 = i + 18 + j;
					float num4 = _Out1D[num2];
					float num5 = _Out1D[num3];
					_Out1D[num2] = num4 * Cs[j] - num5 * Ca[j];
					_Out1D[num3] = num5 * Cs[j] + num4 * Ca[j];
				}
			}
		}

		private void Hybrid(int ch, int gr)
		{
			GranuleInfo granuleInfo = _SideInfo.Channels[ch].Granules[gr];
			for (int i = 0; i < 576; i += 18)
			{
				int blockType = ((granuleInfo.WindowSwitchingFlag == 0 || granuleInfo.MixedBlockFlag == 0 || i >= 36) ? granuleInfo.BlockType : 0);
				float[] out1D = _Out1D;
				for (int j = 0; j < 18; j++)
				{
					TsOutCopy[j] = out1D[j + i];
				}
				InverseMdct(TsOutCopy, Rawout, blockType);
				for (int k = 0; k < 18; k++)
				{
					out1D[k + i] = TsOutCopy[k];
				}
				float[][] prevblck = _Prevblck;
				out1D[i] = Rawout[0] + prevblck[ch][i];
				prevblck[ch][i] = Rawout[18];
				out1D[1 + i] = Rawout[1] + prevblck[ch][i + 1];
				prevblck[ch][i + 1] = Rawout[19];
				out1D[2 + i] = Rawout[2] + prevblck[ch][i + 2];
				prevblck[ch][i + 2] = Rawout[20];
				out1D[3 + i] = Rawout[3] + prevblck[ch][i + 3];
				prevblck[ch][i + 3] = Rawout[21];
				out1D[4 + i] = Rawout[4] + prevblck[ch][i + 4];
				prevblck[ch][i + 4] = Rawout[22];
				out1D[5 + i] = Rawout[5] + prevblck[ch][i + 5];
				prevblck[ch][i + 5] = Rawout[23];
				out1D[6 + i] = Rawout[6] + prevblck[ch][i + 6];
				prevblck[ch][i + 6] = Rawout[24];
				out1D[7 + i] = Rawout[7] + prevblck[ch][i + 7];
				prevblck[ch][i + 7] = Rawout[25];
				out1D[8 + i] = Rawout[8] + prevblck[ch][i + 8];
				prevblck[ch][i + 8] = Rawout[26];
				out1D[9 + i] = Rawout[9] + prevblck[ch][i + 9];
				prevblck[ch][i + 9] = Rawout[27];
				out1D[10 + i] = Rawout[10] + prevblck[ch][i + 10];
				prevblck[ch][i + 10] = Rawout[28];
				out1D[11 + i] = Rawout[11] + prevblck[ch][i + 11];
				prevblck[ch][i + 11] = Rawout[29];
				out1D[12 + i] = Rawout[12] + prevblck[ch][i + 12];
				prevblck[ch][i + 12] = Rawout[30];
				out1D[13 + i] = Rawout[13] + prevblck[ch][i + 13];
				prevblck[ch][i + 13] = Rawout[31];
				out1D[14 + i] = Rawout[14] + prevblck[ch][i + 14];
				prevblck[ch][i + 14] = Rawout[32];
				out1D[15 + i] = Rawout[15] + prevblck[ch][i + 15];
				prevblck[ch][i + 15] = Rawout[33];
				out1D[16 + i] = Rawout[16] + prevblck[ch][i + 16];
				prevblck[ch][i + 16] = Rawout[34];
				out1D[17 + i] = Rawout[17] + prevblck[ch][i + 17];
				prevblck[ch][i + 17] = Rawout[35];
			}
		}

		private void DoDownMix()
		{
			for (int i = 0; i < 18; i++)
			{
				for (int j = 0; j < 18; j += 3)
				{
					_Lr[0][i][j] = (_Lr[0][i][j] + _Lr[1][i][j]) * 0.5f;
					_Lr[0][i][j + 1] = (_Lr[0][i][j + 1] + _Lr[1][i][j + 1]) * 0.5f;
					_Lr[0][i][j + 2] = (_Lr[0][i][j + 2] + _Lr[1][i][j + 2]) * 0.5f;
				}
			}
		}

		internal void InverseMdct(float[] inValues, float[] outValues, int blockType)
		{
			if (blockType == 2)
			{
				outValues[0] = 0f;
				outValues[1] = 0f;
				outValues[2] = 0f;
				outValues[3] = 0f;
				outValues[4] = 0f;
				outValues[5] = 0f;
				outValues[6] = 0f;
				outValues[7] = 0f;
				outValues[8] = 0f;
				outValues[9] = 0f;
				outValues[10] = 0f;
				outValues[11] = 0f;
				outValues[12] = 0f;
				outValues[13] = 0f;
				outValues[14] = 0f;
				outValues[15] = 0f;
				outValues[16] = 0f;
				outValues[17] = 0f;
				outValues[18] = 0f;
				outValues[19] = 0f;
				outValues[20] = 0f;
				outValues[21] = 0f;
				outValues[22] = 0f;
				outValues[23] = 0f;
				outValues[24] = 0f;
				outValues[25] = 0f;
				outValues[26] = 0f;
				outValues[27] = 0f;
				outValues[28] = 0f;
				outValues[29] = 0f;
				outValues[30] = 0f;
				outValues[31] = 0f;
				outValues[32] = 0f;
				outValues[33] = 0f;
				outValues[34] = 0f;
				outValues[35] = 0f;
				int num = 0;
				for (int i = 0; i < 3; i++)
				{
					inValues[15 + i] += inValues[12 + i];
					inValues[12 + i] += inValues[9 + i];
					inValues[9 + i] += inValues[6 + i];
					inValues[6 + i] += inValues[3 + i];
					inValues[3 + i] += inValues[i];
					inValues[15 + i] += inValues[9 + i];
					inValues[9 + i] += inValues[3 + i];
					float num2 = inValues[12 + i] * 0.5f;
					float num3 = inValues[6 + i] * 0.8660254f;
					float num4 = inValues[i] + num2;
					float num5 = inValues[i] - inValues[12 + i];
					float num6 = num4 + num3;
					float num7 = num4 - num3;
					num2 = inValues[15 + i] * 0.5f;
					num3 = inValues[9 + i] * 0.8660254f;
					float num8 = inValues[3 + i] + num2;
					float num9 = inValues[3 + i] - inValues[15 + i];
					float num10 = num8 + num3;
					float num11 = num8 - num3;
					num11 *= 1.9318516f;
					num9 *= 0.70710677f;
					num10 *= 0.5176381f;
					float num12 = num6;
					num6 += num10;
					num10 = num12 - num10;
					float num13 = num5;
					num5 += num9;
					num9 = num13 - num9;
					float num14 = num7;
					num7 += num11;
					num11 = num14 - num11;
					num6 *= 0.5043145f;
					num5 *= 0.5411961f;
					num7 *= 0.6302362f;
					num11 *= 0.8213398f;
					num9 *= 1.306563f;
					num10 *= 3.830649f;
					float num15 = (0f - num6) * 0.7933533f;
					float num16 = (0f - num6) * 0.6087614f;
					float num17 = (0f - num5) * 0.9238795f;
					float num18 = (0f - num5) * 0.38268343f;
					float num19 = (0f - num7) * 0.9914449f;
					float num20 = (0f - num7) * 0.13052619f;
					num6 = num11;
					num5 = num9 * 0.38268343f;
					num7 = num10 * 0.6087614f;
					num11 = (0f - num10) * 0.7933533f;
					num9 = (0f - num9) * 0.9238795f;
					num10 = (0f - num6) * 0.9914449f;
					num6 *= 0.13052619f;
					outValues[num + 6] += num6;
					outValues[num + 7] += num5;
					outValues[num + 8] += num7;
					outValues[num + 9] += num11;
					outValues[num + 10] += num9;
					outValues[num + 11] += num10;
					outValues[num + 12] += num19;
					outValues[num + 13] += num17;
					outValues[num + 14] += num15;
					outValues[num + 15] += num16;
					outValues[num + 16] += num18;
					outValues[num + 17] += num20;
					num += 6;
				}
			}
			else
			{
				inValues[17] += inValues[16];
				inValues[16] += inValues[15];
				inValues[15] += inValues[14];
				inValues[14] += inValues[13];
				inValues[13] += inValues[12];
				inValues[12] += inValues[11];
				inValues[11] += inValues[10];
				inValues[10] += inValues[9];
				inValues[9] += inValues[8];
				inValues[8] += inValues[7];
				inValues[7] += inValues[6];
				inValues[6] += inValues[5];
				inValues[5] += inValues[4];
				inValues[4] += inValues[3];
				inValues[3] += inValues[2];
				inValues[2] += inValues[1];
				inValues[1] += inValues[0];
				inValues[17] += inValues[15];
				inValues[15] += inValues[13];
				inValues[13] += inValues[11];
				inValues[11] += inValues[9];
				inValues[9] += inValues[7];
				inValues[7] += inValues[5];
				inValues[5] += inValues[3];
				inValues[3] += inValues[1];
				float num21 = inValues[0] + inValues[0];
				float num22 = num21 + inValues[12];
				float num23 = num22 + inValues[4] * 1.8793852f + inValues[8] * 1.5320889f + inValues[16] * 0.34729636f;
				float num24 = num21 + inValues[4] - inValues[8] - inValues[12] - inValues[12] - inValues[16];
				float num25 = num22 - inValues[4] * 0.34729636f - inValues[8] * 1.8793852f + inValues[16] * 1.5320889f;
				float num26 = num22 - inValues[4] * 1.5320889f + inValues[8] * 0.34729636f - inValues[16] * 1.8793852f;
				float num27 = inValues[0] - inValues[4] + inValues[8] - inValues[12] + inValues[16];
				float num28 = inValues[6] * 1.7320508f;
				float num29 = inValues[2] * 1.9696155f + num28 + inValues[10] * 1.2855753f + inValues[14] * 0.6840403f;
				float num30 = (inValues[2] - inValues[10] - inValues[14]) * 1.7320508f;
				float num31 = inValues[2] * 1.2855753f - num28 - inValues[10] * 0.6840403f + inValues[14] * 1.9696155f;
				float num32 = inValues[2] * 0.6840403f - num28 + inValues[10] * 1.9696155f - inValues[14] * 1.2855753f;
				float num33 = inValues[1] + inValues[1];
				float num34 = num33 + inValues[13];
				float num35 = num34 + inValues[5] * 1.8793852f + inValues[9] * 1.5320889f + inValues[17] * 0.34729636f;
				float num36 = num33 + inValues[5] - inValues[9] - inValues[13] - inValues[13] - inValues[17];
				float num37 = num34 - inValues[5] * 0.34729636f - inValues[9] * 1.8793852f + inValues[17] * 1.5320889f;
				float num38 = num34 - inValues[5] * 1.5320889f + inValues[9] * 0.34729636f - inValues[17] * 1.8793852f;
				float num39 = (inValues[1] - inValues[5] + inValues[9] - inValues[13] + inValues[17]) * 0.70710677f;
				float num40 = inValues[7] * 1.7320508f;
				float num41 = inValues[3] * 1.9696155f + num40 + inValues[11] * 1.2855753f + inValues[15] * 0.6840403f;
				float num42 = (inValues[3] - inValues[11] - inValues[15]) * 1.7320508f;
				float num43 = inValues[3] * 1.2855753f - num40 - inValues[11] * 0.6840403f + inValues[15] * 1.9696155f;
				float num44 = inValues[3] * 0.6840403f - num40 + inValues[11] * 1.9696155f - inValues[15] * 1.2855753f;
				float num45 = num23 + num29;
				float num46 = (num35 + num41) * 0.5019099f;
				float num6 = num45 + num46;
				float num47 = num45 - num46;
				float num48 = num24 + num30;
				num46 = (num36 + num42) * 0.5176381f;
				float num5 = num48 + num46;
				float num49 = num48 - num46;
				float num50 = num25 + num31;
				num46 = (num37 + num43) * 0.55168897f;
				float num7 = num50 + num46;
				float num51 = num50 - num46;
				float num52 = num26 + num32;
				num46 = (num38 + num44) * 0.61038727f;
				float num11 = num52 + num46;
				float num53 = num52 - num46;
				float num9 = num27 + num39;
				float num54 = num27 - num39;
				float num55 = num26 - num32;
				num46 = (num38 - num44) * 0.8717234f;
				float num10 = num55 + num46;
				float num56 = num55 - num46;
				float num57 = num25 - num31;
				num46 = (num37 - num43) * 1.1831008f;
				float num19 = num57 + num46;
				float num20 = num57 - num46;
				float num58 = num24 - num30;
				num46 = (num36 - num42) * 1.9318516f;
				float num17 = num58 + num46;
				float num18 = num58 - num46;
				float num59 = num23 - num29;
				num46 = (num35 - num41) * 5.7368565f;
				float num15 = num59 + num46;
				float num16 = num59 - num46;
				float[] array = Win[blockType];
				outValues[0] = (0f - num16) * array[0];
				outValues[1] = (0f - num18) * array[1];
				outValues[2] = (0f - num20) * array[2];
				outValues[3] = (0f - num56) * array[3];
				outValues[4] = (0f - num54) * array[4];
				outValues[5] = (0f - num53) * array[5];
				outValues[6] = (0f - num51) * array[6];
				outValues[7] = (0f - num49) * array[7];
				outValues[8] = (0f - num47) * array[8];
				outValues[9] = num47 * array[9];
				outValues[10] = num49 * array[10];
				outValues[11] = num51 * array[11];
				outValues[12] = num53 * array[12];
				outValues[13] = num54 * array[13];
				outValues[14] = num56 * array[14];
				outValues[15] = num20 * array[15];
				outValues[16] = num18 * array[16];
				outValues[17] = num16 * array[17];
				outValues[18] = num15 * array[18];
				outValues[19] = num17 * array[19];
				outValues[20] = num19 * array[20];
				outValues[21] = num10 * array[21];
				outValues[22] = num9 * array[22];
				outValues[23] = num11 * array[23];
				outValues[24] = num7 * array[24];
				outValues[25] = num5 * array[25];
				outValues[26] = num6 * array[26];
				outValues[27] = num6 * array[27];
				outValues[28] = num5 * array[28];
				outValues[29] = num7 * array[29];
				outValues[30] = num11 * array[30];
				outValues[31] = num9 * array[31];
				outValues[32] = num10 * array[32];
				outValues[33] = num19 * array[33];
				outValues[34] = num17 * array[34];
				outValues[35] = num15 * array[35];
			}
		}

		private static float[] CreatePowerTable()
		{
			float[] array = new float[8192];
			double y = 1.3333333333333333;
			for (int i = 0; i < 8192; i++)
			{
				array[i] = (float)Math.Pow(i, y);
			}
			return array;
		}

		internal static int[] Reorder(int[] scalefacBand)
		{
			int num = 0;
			int[] array = new int[576];
			for (int i = 0; i < 13; i++)
			{
				int num2 = scalefacBand[i];
				int num3 = scalefacBand[i + 1];
				for (int j = 0; j < 3; j++)
				{
					for (int k = num2; k < num3; k++)
					{
						array[3 * k + j] = num++;
					}
				}
			}
			return array;
		}
	}
}
namespace MP3Sharp.Decoding.Decoders.LayerI
{
	public class SubbandLayer1 : ASubband
	{
		internal static readonly float[] TableFactor = new float[15]
		{
			0f,
			2f / 3f,
			0.2857143f,
			2f / 15f,
			0.06451613f,
			2f / 63f,
			0.015748031f,
			0.007843138f,
			0.0039138943f,
			0.0019550342f,
			0.0009770396f,
			0.0004884005f,
			0.00024417043f,
			0.00012207776f,
			6.103702E-05f
		};

		internal static readonly float[] TableOffset = new float[15]
		{
			0f,
			-2f / 3f,
			-0.8571429f,
			-0.9333334f,
			-0.9677419f,
			-0.98412704f,
			-0.992126f,
			-0.9960785f,
			-0.99804306f,
			-0.9990225f,
			-0.9995115f,
			-0.99975586f,
			-0.9998779f,
			-0.99993896f,
			-0.9999695f
		};

		protected int Allocation;

		protected float Factor;

		protected float Offset;

		protected float Sample;

		protected int Samplelength;

		protected int Samplenumber;

		protected float Scalefactor;

		protected readonly int Subbandnumber;

		internal SubbandLayer1(int subbandnumber)
		{
			Subbandnumber = subbandnumber;
			Samplenumber = 0;
		}

		internal override void ReadAllocation(Bitstream stream, Header header, Crc16 crc)
		{
			int num = (Allocation = stream.GetBitsFromBuffer(4));
			_ = 15;
			crc?.AddBits(Allocation, 4);
			if (Allocation != 0)
			{
				Samplelength = Allocation + 1;
				Factor = TableFactor[Allocation];
				Offset = TableOffset[Allocation];
			}
		}

		internal override void ReadScaleFactor(Bitstream stream, Header header)
		{
			if (Allocation != 0)
			{
				Scalefactor = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
			}
		}

		internal override bool ReadSampleData(Bitstream stream)
		{
			if (Allocation != 0)
			{
				Sample = stream.GetBitsFromBuffer(Samplelength);
			}
			if (++Samplenumber == 12)
			{
				Samplenumber = 0;
				return true;
			}
			return false;
		}

		internal override bool PutNextSample(int channels, SynthesisFilter filter1, SynthesisFilter filter2)
		{
			if (Allocation != 0 && channels != 2)
			{
				float sample = (Sample * Factor + Offset) * Scalefactor;
				filter1.AddSample(sample, Subbandnumber);
			}
			return true;
		}
	}
	public class SubbandLayer1IntensityStereo : SubbandLayer1
	{
		protected float Channel2Scalefactor;

		internal SubbandLayer1IntensityStereo(int subbandnumber)
			: base(subbandnumber)
		{
		}

		internal override void ReadScaleFactor(Bitstream stream, Header header)
		{
			if (Allocation != 0)
			{
				Scalefactor = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
				Channel2Scalefactor = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
			}
		}

		internal override bool PutNextSample(int channels, SynthesisFilter filter1, SynthesisFilter filter2)
		{
			if (Allocation != 0)
			{
				Sample = Sample * Factor + Offset;
				switch (channels)
				{
				case 0:
				{
					float sample3 = Sample * Scalefactor;
					float sample4 = Sample * Channel2Scalefactor;
					filter1.AddSample(sample3, Subbandnumber);
					filter2.AddSample(sample4, Subbandnumber);
					break;
				}
				case 1:
				{
					float sample2 = Sample * Scalefactor;
					filter1.AddSample(sample2, Subbandnumber);
					break;
				}
				default:
				{
					float sample = Sample * Channel2Scalefactor;
					filter1.AddSample(sample, Subbandnumber);
					break;
				}
				}
			}
			return true;
		}
	}
	public class SubbandLayer1Stereo : SubbandLayer1
	{
		protected int Channel2Allocation;

		protected float Channel2Factor;

		protected float Channel2Offset;

		protected float Channel2Sample;

		protected int Channel2Samplelength;

		protected float Channel2Scalefactor;

		internal SubbandLayer1Stereo(int subbandnumber)
			: base(subbandnumber)
		{
		}

		internal override void ReadAllocation(Bitstream stream, Header header, Crc16 crc)
		{
			Allocation = stream.GetBitsFromBuffer(4);
			if (Allocation > 14)
			{
				return;
			}
			Channel2Allocation = stream.GetBitsFromBuffer(4);
			if (crc != null)
			{
				crc.AddBits(Allocation, 4);
				crc.AddBits(Channel2Allocation, 4);
			}
			if (Allocation != 0)
			{
				Samplelength = Allocation + 1;
				if (Allocation >= 0)
				{
					if (Allocation < SubbandLayer1.TableFactor.Length)
					{
						Factor = SubbandLayer1.TableFactor[Allocation];
					}
					if (Allocation < SubbandLayer1.TableOffset.Length)
					{
						Offset = SubbandLayer1.TableOffset[Allocation];
					}
				}
			}
			if (Channel2Allocation == 0)
			{
				return;
			}
			Channel2Samplelength = Channel2Allocation + 1;
			if (Channel2Allocation >= 0)
			{
				if (Channel2Allocation < SubbandLayer1.TableFactor.Length)
				{
					Channel2Factor = SubbandLayer1.TableFactor[Channel2Allocation];
				}
				if (Channel2Allocation < SubbandLayer1.TableOffset.Length)
				{
					Channel2Offset = SubbandLayer1.TableOffset[Channel2Allocation];
				}
			}
		}

		internal override void ReadScaleFactor(Bitstream stream, Header header)
		{
			if (Allocation != 0)
			{
				Scalefactor = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
			}
			if (Channel2Allocation != 0)
			{
				Channel2Scalefactor = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
			}
		}

		internal override bool ReadSampleData(Bitstream stream)
		{
			bool result = base.ReadSampleData(stream);
			if (Channel2Allocation != 0)
			{
				Channel2Sample = stream.GetBitsFromBuffer(Channel2Samplelength);
			}
			return result;
		}

		internal override bool PutNextSample(int channels, SynthesisFilter filter1, SynthesisFilter filter2)
		{
			base.PutNextSample(channels, filter1, filter2);
			if (Channel2Allocation != 0 && channels != 1)
			{
				float sample = (Channel2Sample * Channel2Factor + Channel2Offset) * Channel2Scalefactor;
				if (channels == 0)
				{
					filter2.AddSample(sample, Subbandnumber);
				}
				else
				{
					filter1.AddSample(sample, Subbandnumber);
				}
			}
			return true;
		}
	}
}
namespace MP3Sharp.Decoding.Decoders.LayerII
{
	public class SubbandLayer2 : ASubband
	{
		internal static readonly float[] Grouping5Bits = new float[81]
		{
			-2f / 3f,
			-2f / 3f,
			-2f / 3f,
			0f,
			-2f / 3f,
			-2f / 3f,
			2f / 3f,
			-2f / 3f,
			-2f / 3f,
			-2f / 3f,
			0f,
			-2f / 3f,
			0f,
			0f,
			-2f / 3f,
			2f / 3f,
			0f,
			-2f / 3f,
			-2f / 3f,
			2f / 3f,
			-2f / 3f,
			0f,
			2f / 3f,
			-2f / 3f,
			2f / 3f,
			2f / 3f,
			-2f / 3f,
			-2f / 3f,
			-2f / 3f,
			0f,
			0f,
			-2f / 3f,
			0f,
			2f / 3f,
			-2f / 3f,
			0f,
			-2f / 3f,
			0f,
			0f,
			0f,
			0f,
			0f,
			2f / 3f,
			0f,
			0f,
			-2f / 3f,
			2f / 3f,
			0f,
			0f,
			2f / 3f,
			0f,
			2f / 3f,
			2f / 3f,
			0f,
			-2f / 3f,
			-2f / 3f,
			2f / 3f,
			0f,
			-2f / 3f,
			2f / 3f,
			2f / 3f,
			-2f / 3f,
			2f / 3f,
			-2f / 3f,
			0f,
			2f / 3f,
			0f,
			0f,
			2f / 3f,
			2f / 3f,
			0f,
			2f / 3f,
			-2f / 3f,
			2f / 3f,
			2f / 3f,
			0f,
			2f / 3f,
			2f / 3f,
			2f / 3f,
			2f / 3f,
			2f / 3f
		};

		internal static readonly float[] Grouping7Bits = new float[375]
		{
			-0.8f, -0.8f, -0.8f, -0.4f, -0.8f, -0.8f, 0f, -0.8f, -0.8f, 0.4f,
			-0.8f, -0.8f, 0.8f, -0.8f, -0.8f, -0.8f, -0.4f, -0.8f, -0.4f, -0.4f,
			-0.8f, 0f, -0.4f, -0.8f, 0.4f, -0.4f, -0.8f, 0.8f, -0.4f, -0.8f,
			-0.8f, 0f, -0.8f, -0.4f, 0f, -0.8f, 0f, 0f, -0.8f, 0.4f,
			0f, -0.8f, 0.8f, 0f, -0.8f, -0.8f, 0.4f, -0.8f, -0.4f, 0.4f,
			-0.8f, 0f, 0.4f, -0.8f, 0.4f, 0.4f, -0.8f, 0.8f, 0.4f, -0.8f,
			-0.8f, 0.8f, -0.8f, -0.4f, 0.8f, -0.8f, 0f, 0.8f, -0.8f, 0.4f,
			0.8f, -0.8f, 0.8f, 0.8f, -0.8f, -0.8f, -0.8f, -0.4f, -0.4f, -0.8f,
			-0.4f, 0f, -0.8f, -0.4f, 0.4f, -0.8f, -0.4f, 0.8f, -0.8f, -0.4f,
			-0.8f, -0.4f, -0.4f, -0.4f, -0.4f, -0.4f, 0f, -0.4f, -0.4f, 0.4f,
			-0.4f, -0.4f, 0.8f, -0.4f, -0.4f, -0.8f, 0f, -0.4f, -0.4f, 0f,
			-0.4f, 0f, 0f, -0.4f, 0.4f, 0f, -0.4f, 0.8f, 0f, -0.4f,
			-0.8f, 0.4f, -0.4f, -0.4f, 0.4f, -0.4f, 0f, 0.4f, -0.4f, 0.4f,
			0.4f, -0.4f, 0.8f, 0.4f, -0.4f, -0.8f, 0.8f, -0.4f, -0.4f, 0.8f,
			-0.4f, 0f, 0.8f, -0.4f, 0.4f, 0.8f, -0.4f, 0.8f, 0.8f, -0.4f,
			-0.8f, -0.8f, 0f, -0.4f, -0.8f, 0f, 0f, -0.8f, 0f, 0.4f,
			-0.8f, 0f, 0.8f, -0.8f, 0f, -0.8f, -0.4f, 0f, -0.4f, -0.4f,
			0f, 0f, -0.4f, 0f, 0.4f, -0.4f, 0f, 0.8f, -0.4f, 0f,
			-0.8f, 0f, 0f, -0.4f, 0f, 0f, 0f, 0f, 0f, 0.4f,
			0f, 0f, 0.8f, 0f, 0f, -0.8f, 0.4f, 0f, -0.4f, 0.4f,
			0f, 0f, 0.4f, 0f, 0.4f, 0.4f, 0f, 0.8f, 0.4f, 0f,
			-0.8f, 0.8f, 0f, -0.4f, 0.8f, 0f, 0f, 0.8f, 0f, 0.4f,
			0.8f, 0f, 0.8f, 0.8f, 0f, -0.8f, -0.8f, 0.4f, -0.4f, -0.8f,
			0.4f, 0f, -0.8f, 0.4f, 0.4f, -0.8f, 0.4f, 0.8f, -0.8f, 0.4f,
			-0.8f, -0.4f, 0.4f, -0.4f, -0.4f, 0.4f, 0f, -0.4f, 0.4f, 0.4f,
			-0.4f, 0.4f, 0.8f, -0.4f, 0.4f, -0.8f, 0f, 0.4f, -0.4f, 0f,
			0.4f, 0f, 0f, 0.4f, 0.4f, 0f, 0.4f, 0.8f, 0f, 0.4f,
			-0.8f, 0.4f, 0.4f, -0.4f, 0.4f, 0.4f, 0f, 0.4f, 0.4f, 0.4f,
			0.4f, 0.4f, 0.8f, 0.4f, 0.4f, -0.8f, 0.8f, 0.4f, -0.4f, 0.8f,
			0.4f, 0f, 0.8f, 0.4f, 0.4f, 0.8f, 0.4f, 0.8f, 0.8f, 0.4f,
			-0.8f, -0.8f, 0.8f, -0.4f, -0.8f, 0.8f, 0f, -0.8f, 0.8f, 0.4f,
			-0.8f, 0.8f, 0.8f, -0.8f, 0.8f, -0.8f, -0.4f, 0.8f, -0.4f, -0.4f,
			0.8f, 0f, -0.4f, 0.8f, 0.4f, -0.4f, 0.8f, 0.8f, -0.4f, 0.8f,
			-0.8f, 0f, 0.8f, -0.4f, 0f, 0.8f, 0f, 0f, 0.8f, 0.4f,
			0f, 0.8f, 0.8f, 0f, 0.8f, -0.8f, 0.4f, 0.8f, -0.4f, 0.4f,
			0.8f, 0f, 0.4f, 0.8f, 0.4f, 0.4f, 0.8f, 0.8f, 0.4f, 0.8f,
			-0.8f, 0.8f, 0.8f, -0.4f, 0.8f, 0.8f, 0f, 0.8f, 0.8f, 0.4f,
			0.8f, 0.8f, 0.8f, 0.8f, 0.8f
		};

		internal static readonly float[] Grouping10Bits = new float[2187]
		{
			-8f / 9f,
			-8f / 9f,
			-8f / 9f,
			-2f / 3f,
			-8f / 9f,
			-8f / 9f,
			-4f / 9f,
			-8f / 9f,
			-8f / 9f,
			-2f / 9f,
			-8f / 9f,
			-8f / 9f,
			0f,
			-8f / 9f,
			-8f / 9f,
			2f / 9f,
			-8f / 9f,
			-8f / 9f,
			4f / 9f,
			-8f / 9f,
			-8f / 9f,
			2f / 3f,
			-8f / 9f,
			-8f / 9f,
			8f / 9f,
			-8f / 9f,
			-8f / 9f,
			-8f / 9f,
			-2f / 3f,
			-8f / 9f,
			-2f / 3f,
			-2f / 3f,
			-8f / 9f,
			-4f / 9f,
			-2f / 3f,
			-8f / 9f,
			-2f / 9f,
			-2f / 3f,
			-8f / 9f,
			0f,
			-2f / 3f,
			-8f / 9f,
			2f / 9f,
			-2f / 3f,
			-8f / 9f,
			4f / 9f,
			-2f / 3f,
			-8f / 9f,
			2f / 3f,
			-2f / 3f,
			-8f / 9f,
			8f / 9f,
			-2f / 3f,
			-8f / 9f,
			-8f / 9f,
			-4f / 9f,
			-8f / 9f,
			-2f / 3f,
			-4f / 9f,
			-8f / 9f,
			-4f / 9f,
			-4f / 9f,
			-8f / 9f,
			-2f / 9f,
			-4f / 9f,
			-8f / 9f,
			0f,
			-4f / 9f,
			-8f / 9f,
			2f / 9f,
			-4f / 9f,
			-8f / 9f,
			4f / 9f,
			-4f / 9f,
			-8f / 9f,
			2f / 3f,
			-4f / 9f,
			-8f / 9f,
			8f / 9f,
			-4f / 9f,
			-8f / 9f,
			-8f / 9f,
			-2f / 9f,
			-8f / 9f,
			-2f / 3f,
			-2f / 9f,
			-8f / 9f,
			-4f / 9f,
			-2f / 9f,
			-8f / 9f,
			-2f / 9f,
			-2f / 9f,
			-8f / 9f,
			0f,
			-2f / 9f,
			-8f / 9f,
			2f / 9f,
			-2f / 9f,
			-8f / 9f,
			4f / 9f,
			-2f / 9f,
			-8f / 9f,
			2f / 3f,
			-2f / 9f,
			-8f / 9f,
			8f / 9f,
			-2f / 9f,
			-8f / 9f,
			-8f / 9f,
			0f,
			-8f / 9f,
			-2f / 3f,
			0f,
			-8f / 9f,
			-4f / 9f,
			0f,
			-8f / 9f,
			-2f / 9f,
			0f,
			-8f / 9f,
			0f,
			0f,
			-8f / 9f,
			2f / 9f,
			0f,
			-8f / 9f,
			4f / 9f,
			0f,
			-8f / 9f,
			2f / 3f,
			0f,
			-8f / 9f,
			8f / 9f,
			0f,
			-8f / 9f,
			-8f / 9f,
			2f / 9f,
			-8f / 9f,
			-2f / 3f,
			2f / 9f,
			-8f / 9f,
			-4f / 9f,
			2f / 9f,
			-8f / 9f,
			-2f / 9f,
			2f / 9f,
			-8f / 9f,
			0f,
			2f / 9f,
			-8f / 9f,
			2f / 9f,
			2f / 9f,
			-8f / 9f,
			4f / 9f,
			2f / 9f,
			-8f / 9f,
			2f / 3f,
			2f / 9f,
			-8f / 9f,
			8f / 9f,
			2f / 9f,
			-8f / 9f,
			-8f / 9f,
			4f / 9f,
			-8f / 9f,
			-2f / 3f,
			4f / 9f,
			-8f / 9f,
			-4f / 9f,
			4f / 9f,
			-8f / 9f,
			-2f / 9f,
			4f / 9f,
			-8f / 9f,
			0f,
			4f / 9f,
			-8f / 9f,
			2f / 9f,
			4f / 9f,
			-8f / 9f,
			4f / 9f,
			4f / 9f,
			-8f / 9f,
			2f / 3f,
			4f / 9f,
			-8f / 9f,
			8f / 9f,
			4f / 9f,
			-8f / 9f,
			-8f / 9f,
			2f / 3f,
			-8f / 9f,
			-2f / 3f,
			2f / 3f,
			-8f / 9f,
			-4f / 9f,
			2f / 3f,
			-8f / 9f,
			-2f / 9f,
			2f / 3f,
			-8f / 9f,
			0f,
			2f / 3f,
			-8f / 9f,
			2f / 9f,
			2f / 3f,
			-8f / 9f,
			4f / 9f,
			2f / 3f,
			-8f / 9f,
			2f / 3f,
			2f / 3f,
			-8f / 9f,
			8f / 9f,
			2f / 3f,
			-8f / 9f,
			-8f / 9f,
			8f / 9f,
			-8f / 9f,
			-2f / 3f,
			8f / 9f,
			-8f / 9f,
			-4f / 9f,
			8f / 9f,
			-8f / 9f,
			-2f / 9f,
			8f / 9f,
			-8f / 9f,
			0f,
			8f / 9f,
			-8f / 9f,
			2f / 9f,
			8f / 9f,
			-8f / 9f,
			4f / 9f,
			8f / 9f,
			-8f / 9f,
			2f / 3f,
			8f / 9f,
			-8f / 9f,
			8f / 9f,
			8f / 9f,
			-8f / 9f,
			-8f / 9f,
			-8f / 9f,
			-2f / 3f,
			-2f / 3f,
			-8f / 9f,
			-2f / 3f,
			-4f / 9f,
			-8f / 9f,
			-2f / 3f,
			-2f / 9f,
			-8f / 9f,
			-2f / 3f,
			0f,
			-8f / 9f,
			-2f / 3f,
			2f / 9f,
			-8f / 9f,
			-2f / 3f,
			4f / 9f,
			-8f / 9f,
			-2f / 3f,
			2f / 3f,
			-8f / 9f,
			-2f / 3f,
			8f / 9f,
			-8f / 9f,
			-2f / 3f,
			-8f / 9f,
			-2f / 3f,
			-2f / 3f,
			-2f / 3f,
			-2f / 3f,
			-2f / 3f,
			-4f / 9f,
			-2f / 3f,
			-2f / 3f,
			-2f / 9f,
			-2f / 3f,
			-2f / 3f,
			0f,
			-2f / 3f,
			-2f / 3f,
			2f / 9f,
			-2f / 3f,
			-2f / 3f,
			4f / 9f,
			-2f / 3f,
			-2f / 3f,
			2f / 3f,
			-2f / 3f,
			-2f / 3f,
			8f / 9f,
			-2f / 3f,
			-2f / 3f,
			-8f / 9f,
			-4f / 9f,
			-2f / 3f,
			-2f / 3f,
			-4f / 9f,
			-2f / 3f,
			-4f / 9f,
			-4f / 9f,
			-2f / 3f,
			-2f / 9f,
			-4f / 9f,
			-2f / 3f,
			0f,
			-4f / 9f,
			-2f / 3f,
			2f / 9f,
			-4f / 9f,
			-2f / 3f,
			4f / 9f,
			-4f / 9f,
			-2f / 3f,
			2f / 3f,
			-4f / 9f,
			-2f / 3f,
			8f / 9f,
			-4f / 9f,
			-2f / 3f,
			-8f / 9f,
			-2f / 9f,
			-2f / 3f,
			-2f / 3f,
			-2f / 9f,
			-2f / 3f,
			-4f / 9f,
			-2f / 9f,
			-2f / 3f,
			-2f / 9f,
			-2f / 9f,
			-2f / 3f,
			0f,
			-2f / 9f,
			-2f / 3f,
			2f / 9f,
			-2f / 9f,
			-2f / 3f,
			4f / 9f,
			-2f / 9f,
			-2f / 3f,
			2f / 3f,
			-2f / 9f,
			-2f / 3f,
			8f / 9f,
			-2f / 9f,
			-2f / 3f,
			-8f / 9f,
			0f,
			-2f / 3f,
			-2f / 3f,
			0f,
			-2f / 3f,
			-4f / 9f,
			0f,
			-2f / 3f,
			-2f / 9f,
			0f,
			-2f / 3f,
			0f,
			0f,
			-2f / 3f,
			2f / 9f,
			0f,
			-2f / 3f,
			4f / 9f,
			0f,
			-2f / 3f,
			2f / 3f,
			0f,
			-2f / 3f,
			8f / 9f,
			0f,
			-2f / 3f,
			-8f / 9f,
			2f / 9f,
			-2f / 3f,
			-2f / 3f,
			2f / 9f,
			-2f / 3f,
			-4f / 9f,
			2f / 9f,
			-2f / 3f,
			-2f / 9f,
			2f / 9f,
			-2f / 3f,
			0f,
			2f / 9f,
			-2f / 3f,
			2f / 9f,
			2f / 9f,
			-2f / 3f,
			4f / 9f,
			2f / 9f,
			-2f / 3f,
			2f / 3f,
			2f / 9f,
			-2f / 3f,
			8f / 9f,
			2f / 9f,
			-2f / 3f,
			-8f / 9f,
			4f / 9f,
			-2f / 3f,
			-2f / 3f,
			4f / 9f,
			-2f / 3f,
			-4f / 9f,
			4f / 9f,
			-2f / 3f,
			-2f / 9f,
			4f / 9f,
			-2f / 3f,
			0f,
			4f / 9f,
			-2f / 3f,
			2f / 9f,
			4f / 9f,
			-2f / 3f,
			4f / 9f,
			4f / 9f,
			-2f / 3f,
			2f / 3f,
			4f / 9f,
			-2f / 3f,
			8f / 9f,
			4f / 9f,
			-2f / 3f,
			-8f / 9f,
			2f / 3f,
			-2f / 3f,
			-2f / 3f,
			2f / 3f,
			-2f / 3f,
			-4f / 9f,
			2f / 3f,
			-2f / 3f,
			-2f / 9f,
			2f / 3f,
			-2f / 3f,
			0f,
			2f / 3f,
			-2f / 3f,
			2f / 9f,
			2f / 3f,
			-2f / 3f,
			4f / 9f,
			2f / 3f,
			-2f / 3f,
			2f / 3f,
			2f / 3f,
			-2f / 3f,
			8f / 9f,
			2f / 3f,
			-2f / 3f,
			-8f / 9f,
			8f / 9f,
			-2f / 3f,
			-2f / 3f,
			8f / 9f,
			-2f / 3f,
			-4f / 9f,
			8f / 9f,
			-2f / 3f,
			-2f / 9f,
			8f / 9f,
			-2f / 3f,
			0f,
			8f / 9f,
			-2f / 3f,
			2f / 9f,
			8f / 9f,
			-2f / 3f,
			4f / 9f,
			8f / 9f,
			-2f / 3f,
			2f / 3f,
			8f / 9f,
			-2f / 3f,
			8f / 9f,
			8f / 9f,
			-2f / 3f,
			-8f / 9f,
			-8f / 9f,
			-4f / 9f,
			-2f / 3f,
			-8f / 9f,
			-4f / 9f,
			-4f / 9f,
			-8f / 9f,
			-4f / 9f,
			-2f / 9f,
			-8f / 9f,
			-4f / 9f,
			0f,
			-8f / 9f,
			-4f / 9f,
			2f / 9f,
			-8f / 9f,
			-4f / 9f,
			4f / 9f,
			-8f / 9f,
			-4f / 9f,
			2f / 3f,
			-8f / 9f,
			-4f / 9f,
			8f / 9f,
			-8f / 9f,
			-4f / 9f,
			-8f / 9f,
			-2f / 3f,
			-4f / 9f,
			-2f / 3f,
			-2f / 3f,
			-4f / 9f,
			-4f / 9f,
			-2f / 3f,
			-4f / 9f,
			-2f / 9f,
			-2f / 3f,
			-4f / 9f,
			0f,
			-2f / 3f,
			-4f / 9f,
			2f / 9f,
			-2f / 3f,
			-4f / 9f,
			4f / 9f,
			-2f / 3f,
			-4f / 9f,
			2f / 3f,
			-2f / 3f,
			-4f / 9f,
			8f / 9f,
			-2f / 3f,
			-4f / 9f,
			-8f / 9f,
			-4f / 9f,
			-4f / 9f,
			-2f / 3f,
			-4f / 9f,
			-4f / 9f,
			-4f / 9f,
			-4f / 9f,
			-4f / 9f,
			-2f / 9f,
			-4f / 9f,
			-4f / 9f,
			0f,
			-4f / 9f,
			-4f / 9f,
			2f / 9f,
			-4f / 9f,
			-4f / 9f,
			4f / 9f,
			-4f / 9f,
			-4f / 9f,
			2f / 3f,
			-4f / 9f,
			-4f / 9f,
			8f / 9f,
			-4f / 9f,
			-4f / 9f,
			-8f / 9f,
			-2f / 9f,
			-4f / 9f,
			-2f / 3f,
			-2f / 9f,
			-4f / 9f,
			-4f / 9f,
			-2f / 9f,
			-4f / 9f,
			-2f / 9f,
			-2f / 9f,
			-4f / 9f,
			0f,
			-2f / 9f,
			-4f / 9f,
			2f / 9f,
			-2f / 9f,
			-4f / 9f,
			4f / 9f,
			-2f / 9f,
			-4f / 9f,
			2f / 3f,
			-2f / 9f,
			-4f / 9f,
			8f / 9f,
			-2f / 9f,
			-4f / 9f,
			-8f / 9f,
			0f,
			-4f / 9f,
			-2f / 3f,
			0f,
			-4f / 9f,
			-4f / 9f,
			0f,
			-4f / 9f,
			-2f / 9f,
			0f,
			-4f / 9f,
			0f,
			0f,
			-4f / 9f,
			2f / 9f,
			0f,
			-4f / 9f,
			4f / 9f,
			0f,
			-4f / 9f,
			2f / 3f,
			0f,
			-4f / 9f,
			8f / 9f,
			0f,
			-4f / 9f,
			-8f / 9f,
			2f / 9f,
			-4f / 9f,
			-2f / 3f,
			2f / 9f,
			-4f / 9f,
			-4f / 9f,
			2f / 9f,
			-4f / 9f,
			-2f / 9f,
			2f / 9f,
			-4f / 9f,
			0f,
			2f / 9f,
			-4f / 9f,
			2f / 9f,
			2f / 9f,
			-4f / 9f,
			4f / 9f,
			2f / 9f,
			-4f / 9f,
			2f / 3f,
			2f / 9f,
			-4f / 9f,
			8f / 9f,
			2f / 9f,
			-4f / 9f,
			-8f / 9f,
			4f / 9f,
			-4f / 9f,
			-2f / 3f,
			4f / 9f,
			-4f / 9f,
			-4f / 9f,
			4f / 9f,
			-4f / 9f,
			-2f / 9f,
			4f / 9f,
			-4f / 9f,
			0f,
			4f / 9f,
			-4f / 9f,
			2f / 9f,
			4f / 9f,
			-4f / 9f,
			4f / 9f,
			4f / 9f,
			-4f / 9f,
			2f / 3f,
			4f / 9f,
			-4f / 9f,
			8f / 9f,
			4f / 9f,
			-4f / 9f,
			-8f / 9f,
			2f / 3f,
			-4f / 9f,
			-2f / 3f,
			2f / 3f,
			-4f / 9f,
			-4f / 9f,
			2f / 3f,
			-4f / 9f,
			-2f / 9f,
			2f / 3f,
			-4f / 9f,
			0f,
			2f / 3f,
			-4f / 9f,
			2f / 9f,
			2f / 3f,
			-4f / 9f,
			4f / 9f,
			2f / 3f,
			-4f / 9f,
			2f / 3f,
			2f / 3f,
			-4f / 9f,
			8f / 9f,
			2f / 3f,
			-4f / 9f,
			-8f / 9f,
			8f / 9f,
			-4f / 9f,
			-2f / 3f,
			8f / 9f,
			-4f / 9f,
			-4f / 9f,
			8f / 9f,
			-4f / 9f,
			-2f / 9f,
			8f / 9f,
			-4f / 9f,
			0f,
			8f / 9f,
			-4f / 9f,
			2f / 9f,
			8f / 9f,
			-4f / 9f,
			4f / 9f,
			8f / 9f,
			-4f / 9f,
			2f / 3f,
			8f / 9f,
			-4f / 9f,
			8f / 9f,
			8f / 9f,
			-4f / 9f,
			-8f / 9f,
			-8f / 9f,
			-2f / 9f,
			-2f / 3f,
			-8f / 9f,
			-2f / 9f,
			-4f / 9f,
			-8f / 9f,
			-2f / 9f,
			-2f / 9f,
			-8f / 9f,
			-2f / 9f,
			0f,
			-8f / 9f,
			-2f / 9f,
			2f / 9f,
			-8f / 9f,
			-2f / 9f,
			4f / 9f,
			-8f / 9f,
			-2f / 9f,
			2f / 3f,
			-8f / 9f,
			-2f / 9f,
			8f / 9f,
			-8f / 9f,
			-2f / 9f,
			-8f / 9f,
			-2f / 3f,
			-2f / 9f,
			-2f / 3f,
			-2f / 3f,
			-2f / 9f,
			-4f / 9f,
			-2f / 3f,
			-2f / 9f,
			-2f / 9f,
			-2f / 3f,
			-2f / 9f,
			0f,
			-2f / 3f,
			-2f / 9f,
			2f / 9f,
			-2f / 3f,
			-2f / 9f,
			4f / 9f,
			-2f / 3f,
			-2f / 9f,
			2f / 3f,
			-2f / 3f,
			-2f / 9f,
			8f / 9f,
			-2f / 3f,
			-2f / 9f,
			-8f / 9f,
			-4f / 9f,
			-2f / 9f,
			-2f / 3f,
			-4f / 9f,
			-2f / 9f,
			-4f / 9f,
			-4f / 9f,
			-2f / 9f,
			-2f / 9f,
			-4f / 9f,
			-2f / 9f,
			0f,
			-4f / 9f,
			-2f / 9f,
			2f / 9f,
			-4f / 9f,
			-2f / 9f,
			4f / 9f,
			-4f / 9f,
			-2f / 9f,
			2f / 3f,
			-4f / 9f,
			-2f / 9f,
			8f / 9f,
			-4f / 9f,
			-2f / 9f,
			-8f / 9f,
			-2f / 9f,
			-2f / 9f,
			-2f / 3f,
			-2f / 9f,
			-2f / 9f,
			-4f / 9f,
			-2f / 9f,
			-2f / 9f,
			-2f / 9f,
			-2f / 9f,
			-2f / 9f,
			0f,
			-2f / 9f,
			-2f / 9f,
			2f / 9f,
			-2f / 9f,
			-2f / 9f,
			4f / 9f,
			-2f / 9f,
			-2f / 9f,
			2f / 3f,
			-2f / 9f,
			-2f / 9f,
			8f / 9f,
			-2f / 9f,
			-2f / 9f,
			-8f / 9f,
			0f,
			-2f / 9f,
			-2f / 3f,
			0f,
			-2f / 9f,
			-4f / 9f,
			0f,
			-2f / 9f,
			-2f / 9f,
			0f,
			-2f / 9f,
			0f,
			0f,
			-2f / 9f,
			2f / 9f,
			0f,
			-2f / 9f,
			4f / 9f,
			0f,
			-2f / 9f,
			2f / 3f,
			0f,
			-2f / 9f,
			8f / 9f,
			0f,
			-2f / 9f,
			-8f / 9f,
			2f / 9f,
			-2f / 9f,
			-2f / 3f,
			2f / 9f,
			-2f / 9f,
			-4f / 9f,
			2f / 9f,
			-2f / 9f,
			-2f / 9f,
			2f / 9f,
			-2f / 9f,
			0f,
			2f / 9f,
			-2f / 9f,
			2f / 9f,
			2f / 9f,
			-2f / 9f,
			4f / 9f,
			2f / 9f,
			-2f / 9f,
			2f / 3f,
			2f / 9f,
			-2f / 9f,
			8f / 9f,
			2f / 9f,
			-2f / 9f,
			-8f / 9f,
			4f / 9f,
			-2f / 9f,
			-2f / 3f,
			4f / 9f,
			-2f / 9f,
			-4f / 9f,
			4f / 9f,
			-2f / 9f,
			-2f / 9f,
			4f / 9f,
			-2f / 9f,
			0f,
			4f / 9f,
			-2f / 9f,
			2f / 9f,
			4f / 9f,
			-2f / 9f,
			4f / 9f,
			4f / 9f,
			-2f / 9f,
			2f / 3f,
			4f / 9f,
			-2f / 9f,
			8f / 9f,
			4f / 9f,
			-2f / 9f,
			-8f / 9f,
			2f / 3f,
			-2f / 9f,
			-2f / 3f,
			2f / 3f,
			-2f / 9f,
			-4f / 9f,
			2f / 3f,
			-2f / 9f,
			-2f / 9f,
			2f / 3f,
			-2f / 9f,
			0f,
			2f / 3f,
			-2f / 9f,
			2f / 9f,
			2f / 3f,
			-2f / 9f,
			4f / 9f,
			2f / 3f,
			-2f / 9f,
			2f / 3f,
			2f / 3f,
			-2f / 9f,
			8f / 9f,
			2f / 3f,
			-2f / 9f,
			-8f / 9f,
			8f / 9f,
			-2f / 9f,
			-2f / 3f,
			8f / 9f,
			-2f / 9f,
			-4f / 9f,
			8f / 9f,
			-2f / 9f,
			-2f / 9f,
			8f / 9f,
			-2f / 9f,
			0f,
			8f / 9f,
			-2f / 9f,
			2f / 9f,
			8f / 9f,
			-2f / 9f,
			4f / 9f,
			8f / 9f,
			-2f / 9f,
			2f / 3f,
			8f / 9f,
			-2f / 9f,
			8f / 9f,
			8f / 9f,
			-2f / 9f,
			-8f / 9f,
			-8f / 9f,
			0f,
			-2f / 3f,
			-8f / 9f,
			0f,
			-4f / 9f,
			-8f / 9f,
			0f,
			-2f / 9f,
			-8f / 9f,
			0f,
			0f,
			-8f / 9f,
			0f,
			2f / 9f,
			-8f / 9f,
			0f,
			4f / 9f,
			-8f / 9f,
			0f,
			2f / 3f,
			-8f / 9f,
			0f,
			8f / 9f,
			-8f / 9f,
			0f,
			-8f / 9f,
			-2f / 3f,
			0f,
			-2f / 3f,
			-2f / 3f,
			0f,
			-4f / 9f,
			-2f / 3f,
			0f,
			-2f / 9f,
			-2f / 3f,
			0f,
			0f,
			-2f / 3f,
			0f,
			2f / 9f,
			-2f / 3f,
			0f,
			4f / 9f,
			-2f / 3f,
			0f,
			2f / 3f,
			-2f / 3f,
			0f,
			8f / 9f,
			-2f / 3f,
			0f,
			-8f / 9f,
			-4f / 9f,
			0f,
			-2f / 3f,
			-4f / 9f,
			0f,
			-4f / 9f,
			-4f / 9f,
			0f,
			-2f / 9f,
			-4f / 9f,
			0f,
			0f,
			-4f / 9f,
			0f,
			2f / 9f,
			-4f / 9f,
			0f,
			4f / 9f,
			-4f / 9f,
			0f,
			2f / 3f,
			-4f / 9f,
			0f,
			8f / 9f,
			-4f / 9f,
			0f,
			-8f / 9f,
			-2f / 9f,
			0f,
			-2f / 3f,
			-2f / 9f,
			0f,
			-4f / 9f,
			-2f / 9f,
			0f,
			-2f / 9f,
			-2f / 9f,
			0f,
			0f,
			-2f / 9f,
			0f,
			2f / 9f,
			-2f / 9f,
			0f,
			4f / 9f,
			-2f / 9f,
			0f,
			2f / 3f,
			-2f / 9f,
			0f,
			8f / 9f,
			-2f / 9f,
			0f,
			-8f / 9f,
			0f,
			0f,
			-2f / 3f,
			0f,
			0f,
			-4f / 9f,
			0f,
			0f,
			-2f / 9f,
			0f,
			0f,
			0f,
			0f,
			0f,
			2f / 9f,
			0f,
			0f,
			4f / 9f,
			0f,
			0f,
			2f / 3f,
			0f,
			0f,
			8f / 9f,
			0f,
			0f,
			-8f / 9f,
			2f / 9f,
			0f,
			-2f / 3f,
			2f / 9f,
			0f,
			-4f / 9f,
			2f / 9f,
			0f,
			-2f / 9f,
			2f / 9f,
			0f,
			0f,
			2f / 9f,
			0f,
			2f / 9f,
			2f / 9f,
			0f,
			4f / 9f,
			2f / 9f,
			0f,
			2f / 3f,
			2f / 9f,
			0f,
			8f / 9f,
			2f / 9f,
			0f,
			-8f / 9f,
			4f / 9f,
			0f,
			-2f / 3f,
			4f / 9f,
			0f,
			-4f / 9f,
			4f / 9f,
			0f,
			-2f / 9f,
			4f / 9f,
			0f,
			0f,
			4f / 9f,
			0f,
			2f / 9f,
			4f / 9f,
			0f,
			4f / 9f,
			4f / 9f,
			0f,
			2f / 3f,
			4f / 9f,
			0f,
			8f / 9f,
			4f / 9f,
			0f,
			-8f / 9f,
			2f / 3f,
			0f,
			-2f / 3f,
			2f / 3f,
			0f,
			-4f / 9f,
			2f / 3f,
			0f,
			-2f / 9f,
			2f / 3f,
			0f,
			0f,
			2f / 3f,
			0f,
			2f / 9f,
			2f / 3f,
			0f,
			4f / 9f,
			2f / 3f,
			0f,
			2f / 3f,
			2f / 3f,
			0f,
			8f / 9f,
			2f / 3f,
			0f,
			-8f / 9f,
			8f / 9f,
			0f,
			-2f / 3f,
			8f / 9f,
			0f,
			-4f / 9f,
			8f / 9f,
			0f,
			-2f / 9f,
			8f / 9f,
			0f,
			0f,
			8f / 9f,
			0f,
			2f / 9f,
			8f / 9f,
			0f,
			4f / 9f,
			8f / 9f,
			0f,
			2f / 3f,
			8f / 9f,
			0f,
			8f / 9f,
			8f / 9f,
			0f,
			-8f / 9f,
			-8f / 9f,
			2f / 9f,
			-2f / 3f,
			-8f / 9f,
			2f / 9f,
			-4f / 9f,
			-8f / 9f,
			2f / 9f,
			-2f / 9f,
			-8f / 9f,
			2f / 9f,
			0f,
			-8f / 9f,
			2f / 9f,
			2f / 9f,
			-8f / 9f,
			2f / 9f,
			4f / 9f,
			-8f / 9f,
			2f / 9f,
			2f / 3f,
			-8f / 9f,
			2f / 9f,
			8f / 9f,
			-8f / 9f,
			2f / 9f,
			-8f / 9f,
			-2f / 3f,
			2f / 9f,
			-2f / 3f,
			-2f / 3f,
			2f / 9f,
			-4f / 9f,
			-2f / 3f,
			2f / 9f,
			-2f / 9f,
			-2f / 3f,
			2f / 9f,
			0f,
			-2f / 3f,
			2f / 9f,
			2f / 9f,
			-2f / 3f,
			2f / 9f,
			4f / 9f,
			-2f / 3f,
			2f / 9f,
			2f / 3f,
			-2f / 3f,
			2f / 9f,
			8f / 9f,
			-2f / 3f,
			2f / 9f,
			-8f / 9f,
			-4f / 9f,
			2f / 9f,
			-2f / 3f,
			-4f / 9f,
			2f / 9f,
			-4f / 9f,
			-4f / 9f,
			2f / 9f,
			-2f / 9f,
			-4f / 9f,
			2f / 9f,
			0f,
			-4f / 9f,
			2f / 9f,
			2f / 9f,
			-4f / 9f,
			2f / 9f,
			4f / 9f,
			-4f / 9f,
			2f / 9f,
			2f / 3f,
			-4f / 9f,
			2f / 9f,
			8f / 9f,
			-4f / 9f,
			2f / 9f,
			-8f / 9f,
			-2f / 9f,
			2f / 9f,
			-2f / 3f,
			-2f / 9f,
			2f / 9f,
			-4f / 9f,
			-2f / 9f,
			2f / 9f,
			-2f / 9f,
			-2f / 9f,
			2f / 9f,
			0f,
			-2f / 9f,
			2f / 9f,
			2f / 9f,
			-2f / 9f,
			2f / 9f,
			4f / 9f,
			-2f / 9f,
			2f / 9f,
			2f / 3f,
			-2f / 9f,
			2f / 9f,
			8f / 9f,
			-2f / 9f,
			2f / 9f,
			-8f / 9f,
			0f,
			2f / 9f,
			-2f / 3f,
			0f,
			2f / 9f,
			-4f / 9f,
			0f,
			2f / 9f,
			-2f / 9f,
			0f,
			2f / 9f,
			0f,
			0f,
			2f / 9f,
			2f / 9f,
			0f,
			2f / 9f,
			4f / 9f,
			0f,
			2f / 9f,
			2f / 3f,
			0f,
			2f / 9f,
			8f / 9f,
			0f,
			2f / 9f,
			-8f / 9f,
			2f / 9f,
			2f / 9f,
			-2f / 3f,
			2f / 9f,
			2f / 9f,
			-4f / 9f,
			2f / 9f,
			2f / 9f,
			-2f / 9f,
			2f / 9f,
			2f / 9f,
			0f,
			2f / 9f,
			2f / 9f,
			2f / 9f,
			2f / 9f,
			2f / 9f,
			4f / 9f,
			2f / 9f,
			2f / 9f,
			2f / 3f,
			2f / 9f,
			2f / 9f,
			8f / 9f,
			2f / 9f,
			2f / 9f,
			-8f / 9f,
			4f / 9f,
			2f / 9f,
			-2f / 3f,
			4f / 9f,
			2f / 9f,
			-4f / 9f,
			4f / 9f,
			2f / 9f,
			-2f / 9f,
			4f / 9f,
			2f / 9f,
			0f,
			4f / 9f,
			2f / 9f,
			2f / 9f,
			4f / 9f,
			2f / 9f,
			4f / 9f,
			4f / 9f,
			2f / 9f,
			2f / 3f,
			4f / 9f,
			2f / 9f,
			8f / 9f,
			4f / 9f,
			2f / 9f,
			-8f / 9f,
			2f / 3f,
			2f / 9f,
			-2f / 3f,
			2f / 3f,
			2f / 9f,
			-4f / 9f,
			2f / 3f,
			2f / 9f,
			-2f / 9f,
			2f / 3f,
			2f / 9f,
			0f,
			2f / 3f,
			2f / 9f,
			2f / 9f,
			2f / 3f,
			2f / 9f,
			4f / 9f,
			2f / 3f,
			2f / 9f,
			2f / 3f,
			2f / 3f,
			2f / 9f,
			8f / 9f,
			2f / 3f,
			2f / 9f,
			-8f / 9f,
			8f / 9f,
			2f / 9f,
			-2f / 3f,
			8f / 9f,
			2f / 9f,
			-4f / 9f,
			8f / 9f,
			2f / 9f,
			-2f / 9f,
			8f / 9f,
			2f / 9f,
			0f,
			8f / 9f,
			2f / 9f,
			2f / 9f,
			8f / 9f,
			2f / 9f,
			4f / 9f,
			8f / 9f,
			2f / 9f,
			2f / 3f,
			8f / 9f,
			2f / 9f,
			8f / 9f,
			8f / 9f,
			2f / 9f,
			-8f / 9f,
			-8f / 9f,
			4f / 9f,
			-2f / 3f,
			-8f / 9f,
			4f / 9f,
			-4f / 9f,
			-8f / 9f,
			4f / 9f,
			-2f / 9f,
			-8f / 9f,
			4f / 9f,
			0f,
			-8f / 9f,
			4f / 9f,
			2f / 9f,
			-8f / 9f,
			4f / 9f,
			4f / 9f,
			-8f / 9f,
			4f / 9f,
			2f / 3f,
			-8f / 9f,
			4f / 9f,
			8f / 9f,
			-8f / 9f,
			4f / 9f,
			-8f / 9f,
			-2f / 3f,
			4f / 9f,
			-2f / 3f,
			-2f / 3f,
			4f / 9f,
			-4f / 9f,
			-2f / 3f,
			4f / 9f,
			-2f / 9f,
			-2f / 3f,
			4f / 9f,
			0f,
			-2f / 3f,
			4f / 9f,
			2f / 9f,
			-2f / 3f,
			4f / 9f,
			4f / 9f,
			-2f / 3f,
			4f / 9f,
			2f / 3f,
			-2f / 3f,
			4f / 9f,
			8f / 9f,
			-2f / 3f,
			4f / 9f,
			-8f / 9f,
			-4f / 9f,
			4f / 9f,
			-2f / 3f,
			-4f / 9f,
			4f / 9f,
			-4f / 9f,
			-4f / 9f,
			4f / 9f,
			-2f / 9f,
			-4f / 9f,
			4f / 9f,
			0f,
			-4f / 9f,
			4f / 9f,
			2f / 9f,
			-4f / 9f,
			4f / 9f,
			4f / 9f,
			-4f / 9f,
			4f / 9f,
			2f / 3f,
			-4f / 9f,
			4f / 9f,
			8f / 9f,
			-4f / 9f,
			4f / 9f,
			-8f / 9f,
			-2f / 9f,
			4f / 9f,
			-2f / 3f,
			-2f / 9f,
			4f / 9f,
			-4f / 9f,
			-2f / 9f,
			4f / 9f,
			-2f / 9f,
			-2f / 9f,
			4f / 9f,
			0f,
			-2f / 9f,
			4f / 9f,
			2f / 9f,
			-2f / 9f,
			4f / 9f,
			4f / 9f,
			-2f / 9f,
			4f / 9f,
			2f / 3f,
			-2f / 9f,
			4f / 9f,
			8f / 9f,
			-2f / 9f,
			4f / 9f,
			-8f / 9f,
			0f,
			4f / 9f,
			-2f / 3f,
			0f,
			4f / 9f,
			-4f / 9f,
			0f,
			4f / 9f,
			-2f / 9f,
			0f,
			4f / 9f,
			0f,
			0f,
			4f / 9f,
			2f / 9f,
			0f,
			4f / 9f,
			4f / 9f,
			0f,
			4f / 9f,
			2f / 3f,
			0f,
			4f / 9f,
			8f / 9f,
			0f,
			4f / 9f,
			-8f / 9f,
			2f / 9f,
			4f / 9f,
			-2f / 3f,
			2f / 9f,
			4f / 9f,
			-4f / 9f,
			2f / 9f,
			4f / 9f,
			-2f / 9f,
			2f / 9f,
			4f / 9f,
			0f,
			2f / 9f,
			4f / 9f,
			2f / 9f,
			2f / 9f,
			4f / 9f,
			4f / 9f,
			2f / 9f,
			4f / 9f,
			2f / 3f,
			2f / 9f,
			4f / 9f,
			8f / 9f,
			2f / 9f,
			4f / 9f,
			-8f / 9f,
			4f / 9f,
			4f / 9f,
			-2f / 3f,
			4f / 9f,
			4f / 9f,
			-4f / 9f,
			4f / 9f,
			4f / 9f,
			-2f / 9f,
			4f / 9f,
			4f / 9f,
			0f,
			4f / 9f,
			4f / 9f,
			2f / 9f,
			4f / 9f,
			4f / 9f,
			4f / 9f,
			4f / 9f,
			4f / 9f,
			2f / 3f,
			4f / 9f,
			4f / 9f,
			8f / 9f,
			4f / 9f,
			4f / 9f,
			-8f / 9f,
			2f / 3f,
			4f / 9f,
			-2f / 3f,
			2f / 3f,
			4f / 9f,
			-4f / 9f,
			2f / 3f,
			4f / 9f,
			-2f / 9f,
			2f / 3f,
			4f / 9f,
			0f,
			2f / 3f,
			4f / 9f,
			2f / 9f,
			2f / 3f,
			4f / 9f,
			4f / 9f,
			2f / 3f,
			4f / 9f,
			2f / 3f,
			2f / 3f,
			4f / 9f,
			8f / 9f,
			2f / 3f,
			4f / 9f,
			-8f / 9f,
			8f / 9f,
			4f / 9f,
			-2f / 3f,
			8f / 9f,
			4f / 9f,
			-4f / 9f,
			8f / 9f,
			4f / 9f,
			-2f / 9f,
			8f / 9f,
			4f / 9f,
			0f,
			8f / 9f,
			4f / 9f,
			2f / 9f,
			8f / 9f,
			4f / 9f,
			4f / 9f,
			8f / 9f,
			4f / 9f,
			2f / 3f,
			8f / 9f,
			4f / 9f,
			8f / 9f,
			8f / 9f,
			4f / 9f,
			-8f / 9f,
			-8f / 9f,
			2f / 3f,
			-2f / 3f,
			-8f / 9f,
			2f / 3f,
			-4f / 9f,
			-8f / 9f,
			2f / 3f,
			-2f / 9f,
			-8f / 9f,
			2f / 3f,
			0f,
			-8f / 9f,
			2f / 3f,
			2f / 9f,
			-8f / 9f,
			2f / 3f,
			4f / 9f,
			-8f / 9f,
			2f / 3f,
			2f / 3f,
			-8f / 9f,
			2f / 3f,
			8f / 9f,
			-8f / 9f,
			2f / 3f,
			-8f / 9f,
			-2f / 3f,
			2f / 3f,
			-2f / 3f,
			-2f / 3f,
			2f / 3f,
			-4f / 9f,
			-2f / 3f,
			2f / 3f,
			-2f / 9f,
			-2f / 3f,
			2f / 3f,
			0f,
			-2f / 3f,
			2f / 3f,
			2f / 9f,
			-2f / 3f,
			2f / 3f,
			4f / 9f,
			-2f / 3f,
			2f / 3f,
			2f / 3f,
			-2f / 3f,
			2f / 3f,
			8f / 9f,
			-2f / 3f,
			2f / 3f,
			-8f / 9f,
			-4f / 9f,
			2f / 3f,
			-2f / 3f,
			-4f / 9f,
			2f / 3f,
			-4f / 9f,
			-4f / 9f,
			2f / 3f,
			-2f / 9f,
			-4f / 9f,
			2f / 3f,
			0f,
			-4f / 9f,
			2f / 3f,
			2f / 9f,
			-4f / 9f,
			2f / 3f,
			4f / 9f,
			-4f / 9f,
			2f / 3f,
			2f / 3f,
			-4f / 9f,
			2f / 3f,
			8f / 9f,
			-4f / 9f,
			2f / 3f,
			-8f / 9f,
			-2f / 9f,
			2f / 3f,
			-2f / 3f,
			-2f / 9f,
			2f / 3f,
			-4f / 9f,
			-2f / 9f,
			2f / 3f,
			-2f / 9f,
			-2f / 9f,
			2f / 3f,
			0f,
			-2f / 9f,
			2f / 3f,
			2f / 9f,
			-2f / 9f,
			2f / 3f,
			4f / 9f,
			-2f / 9f,
			2f / 3f,
			2f / 3f,
			-2f / 9f,
			2f / 3f,
			8f / 9f,
			-2f / 9f,
			2f / 3f,
			-8f / 9f,
			0f,
			2f / 3f,
			-2f / 3f,
			0f,
			2f / 3f,
			-4f / 9f,
			0f,
			2f / 3f,
			-2f / 9f,
			0f,
			2f / 3f,
			0f,
			0f,
			2f / 3f,
			2f / 9f,
			0f,
			2f / 3f,
			4f / 9f,
			0f,
			2f / 3f,
			2f / 3f,
			0f,
			2f / 3f,
			8f / 9f,
			0f,
			2f / 3f,
			-8f / 9f,
			2f / 9f,
			2f / 3f,
			-2f / 3f,
			2f / 9f,
			2f / 3f,
			-4f / 9f,
			2f / 9f,
			2f / 3f,
			-2f / 9f,
			2f / 9f,
			2f / 3f,
			0f,
			2f / 9f,
			2f / 3f,
			2f / 9f,
			2f / 9f,
			2f / 3f,
			4f / 9f,
			2f / 9f,
			2f / 3f,
			2f / 3f,
			2f / 9f,
			2f / 3f,
			8f / 9f,
			2f / 9f,
			2f / 3f,
			-8f / 9f,
			4f / 9f,
			2f / 3f,
			-2f / 3f,
			4f / 9f,
			2f / 3f,
			-4f / 9f,
			4f / 9f,
			2f / 3f,
			-2f / 9f,
			4f / 9f,
			2f / 3f,
			0f,
			4f / 9f,
			2f / 3f,
			2f / 9f,
			4f / 9f,
			2f / 3f,
			4f / 9f,
			4f / 9f,
			2f / 3f,
			2f / 3f,
			4f / 9f,
			2f / 3f,
			8f / 9f,
			4f / 9f,
			2f / 3f,
			-8f / 9f,
			2f / 3f,
			2f / 3f,
			-2f / 3f,
			2f / 3f,
			2f / 3f,
			-4f / 9f,
			2f / 3f,
			2f / 3f,
			-2f / 9f,
			2f / 3f,
			2f / 3f,
			0f,
			2f / 3f,
			2f / 3f,
			2f / 9f,
			2f / 3f,
			2f / 3f,
			4f / 9f,
			2f / 3f,
			2f / 3f,
			2f / 3f,
			2f / 3f,
			2f / 3f,
			8f / 9f,
			2f / 3f,
			2f / 3f,
			-8f / 9f,
			8f / 9f,
			2f / 3f,
			-2f / 3f,
			8f / 9f,
			2f / 3f,
			-4f / 9f,
			8f / 9f,
			2f / 3f,
			-2f / 9f,
			8f / 9f,
			2f / 3f,
			0f,
			8f / 9f,
			2f / 3f,
			2f / 9f,
			8f / 9f,
			2f / 3f,
			4f / 9f,
			8f / 9f,
			2f / 3f,
			2f / 3f,
			8f / 9f,
			2f / 3f,
			8f / 9f,
			8f / 9f,
			2f / 3f,
			-8f / 9f,
			-8f / 9f,
			8f / 9f,
			-2f / 3f,
			-8f / 9f,
			8f / 9f,
			-4f / 9f,
			-8f / 9f,
			8f / 9f,
			-2f / 9f,
			-8f / 9f,
			8f / 9f,
			0f,
			-8f / 9f,
			8f / 9f,
			2f / 9f,
			-8f / 9f,
			8f / 9f,
			4f / 9f,
			-8f / 9f,
			8f / 9f,
			2f / 3f,
			-8f / 9f,
			8f / 9f,
			8f / 9f,
			-8f / 9f,
			8f / 9f,
			-8f / 9f,
			-2f / 3f,
			8f / 9f,
			-2f / 3f,
			-2f / 3f,
			8f / 9f,
			-4f / 9f,
			-2f / 3f,
			8f / 9f,
			-2f / 9f,
			-2f / 3f,
			8f / 9f,
			0f,
			-2f / 3f,
			8f / 9f,
			2f / 9f,
			-2f / 3f,
			8f / 9f,
			4f / 9f,
			-2f / 3f,
			8f / 9f,
			2f / 3f,
			-2f / 3f,
			8f / 9f,
			8f / 9f,
			-2f / 3f,
			8f / 9f,
			-8f / 9f,
			-4f / 9f,
			8f / 9f,
			-2f / 3f,
			-4f / 9f,
			8f / 9f,
			-4f / 9f,
			-4f / 9f,
			8f / 9f,
			-2f / 9f,
			-4f / 9f,
			8f / 9f,
			0f,
			-4f / 9f,
			8f / 9f,
			2f / 9f,
			-4f / 9f,
			8f / 9f,
			4f / 9f,
			-4f / 9f,
			8f / 9f,
			2f / 3f,
			-4f / 9f,
			8f / 9f,
			8f / 9f,
			-4f / 9f,
			8f / 9f,
			-8f / 9f,
			-2f / 9f,
			8f / 9f,
			-2f / 3f,
			-2f / 9f,
			8f / 9f,
			-4f / 9f,
			-2f / 9f,
			8f / 9f,
			-2f / 9f,
			-2f / 9f,
			8f / 9f,
			0f,
			-2f / 9f,
			8f / 9f,
			2f / 9f,
			-2f / 9f,
			8f / 9f,
			4f / 9f,
			-2f / 9f,
			8f / 9f,
			2f / 3f,
			-2f / 9f,
			8f / 9f,
			8f / 9f,
			-2f / 9f,
			8f / 9f,
			-8f / 9f,
			0f,
			8f / 9f,
			-2f / 3f,
			0f,
			8f / 9f,
			-4f / 9f,
			0f,
			8f / 9f,
			-2f / 9f,
			0f,
			8f / 9f,
			0f,
			0f,
			8f / 9f,
			2f / 9f,
			0f,
			8f / 9f,
			4f / 9f,
			0f,
			8f / 9f,
			2f / 3f,
			0f,
			8f / 9f,
			8f / 9f,
			0f,
			8f / 9f,
			-8f / 9f,
			2f / 9f,
			8f / 9f,
			-2f / 3f,
			2f / 9f,
			8f / 9f,
			-4f / 9f,
			2f / 9f,
			8f / 9f,
			-2f / 9f,
			2f / 9f,
			8f / 9f,
			0f,
			2f / 9f,
			8f / 9f,
			2f / 9f,
			2f / 9f,
			8f / 9f,
			4f / 9f,
			2f / 9f,
			8f / 9f,
			2f / 3f,
			2f / 9f,
			8f / 9f,
			8f / 9f,
			2f / 9f,
			8f / 9f,
			-8f / 9f,
			4f / 9f,
			8f / 9f,
			-2f / 3f,
			4f / 9f,
			8f / 9f,
			-4f / 9f,
			4f / 9f,
			8f / 9f,
			-2f / 9f,
			4f / 9f,
			8f / 9f,
			0f,
			4f / 9f,
			8f / 9f,
			2f / 9f,
			4f / 9f,
			8f / 9f,
			4f / 9f,
			4f / 9f,
			8f / 9f,
			2f / 3f,
			4f / 9f,
			8f / 9f,
			8f / 9f,
			4f / 9f,
			8f / 9f,
			-8f / 9f,
			2f / 3f,
			8f / 9f,
			-2f / 3f,
			2f / 3f,
			8f / 9f,
			-4f / 9f,
			2f / 3f,
			8f / 9f,
			-2f / 9f,
			2f / 3f,
			8f / 9f,
			0f,
			2f / 3f,
			8f / 9f,
			2f / 9f,
			2f / 3f,
			8f / 9f,
			4f / 9f,
			2f / 3f,
			8f / 9f,
			2f / 3f,
			2f / 3f,
			8f / 9f,
			8f / 9f,
			2f / 3f,
			8f / 9f,
			-8f / 9f,
			8f / 9f,
			8f / 9f,
			-2f / 3f,
			8f / 9f,
			8f / 9f,
			-4f / 9f,
			8f / 9f,
			8f / 9f,
			-2f / 9f,
			8f / 9f,
			8f / 9f,
			0f,
			8f / 9f,
			8f / 9f,
			2f / 9f,
			8f / 9f,
			8f / 9f,
			4f / 9f,
			8f / 9f,
			8f / 9f,
			2f / 3f,
			8f / 9f,
			8f / 9f,
			8f / 9f,
			8f / 9f,
			8f / 9f
		};

		internal static readonly int[] TableAb1Codelength = new int[16]
		{
			0, 5, 3, 4, 5, 6, 7, 8, 9, 10,
			11, 12, 13, 14, 15, 16
		};

		internal static readonly float[][] TableAb1Groupingtables = new float[16][]
		{
			null, Grouping5Bits, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null
		};

		internal static readonly float[] TableAb1Factor = new float[16]
		{
			0f,
			0.5f,
			0.25f,
			0.125f,
			0.0625f,
			1f / 32f,
			1f / 64f,
			1f / 128f,
			0.00390625f,
			0.001953125f,
			0.0009765625f,
			0.00048828125f,
			0.00024414062f,
			0.00012207031f,
			6.1035156E-05f,
			3.0517578E-05f
		};

		internal static readonly float[] TableAb1C = new float[16]
		{
			0f, 1.3333334f, 1.1428572f, 1.0666667f, 1.032258f, 1.0158731f, 1.007874f, 1.0039216f, 1.0019569f, 1.0009775f,
			1.0004885f, 1.0002443f, 1.0001221f, 1.000061f, 1.0000305f, 1.0000153f
		};

		internal static readonly float[] TableAb1D = new float[16]
		{
			0f,
			0.5f,
			0.25f,
			0.125f,
			0.0625f,
			1f / 32f,
			1f / 64f,
			1f / 128f,
			0.00390625f,
			0.001953125f,
			0.0009765625f,
			0.00048828125f,
			0.00024414062f,
			0.00012207031f,
			6.103516E-05f,
			3.051758E-05f
		};

		internal static readonly float[][] TableAb234Groupingtables = new float[16][]
		{
			null, Grouping5Bits, Grouping7Bits, null, Grouping10Bits, null, null, null, null, null,
			null, null, null, null, null, null
		};

		internal static readonly int[] TableAb2Codelength = new int[16]
		{
			0, 5, 7, 3, 10, 4, 5, 6, 7, 8,
			9, 10, 11, 12, 13, 16
		};

		internal static readonly float[] TableAb2Factor = new float[16]
		{
			0f,
			0.5f,
			0.25f,
			0.25f,
			0.125f,
			0.125f,
			0.0625f,
			1f / 32f,
			1f / 64f,
			1f / 128f,
			0.00390625f,
			0.001953125f,
			0.0009765625f,
			0.00048828125f,
			0.00024414062f,
			3.0517578E-05f
		};

		internal static readonly float[] TableAb2C = new float[16]
		{
			0f, 1.3333334f, 1.6f, 1.1428572f, 1.7777778f, 1.0666667f, 1.032258f, 1.0158731f, 1.007874f, 1.0039216f,
			1.0019569f, 1.0009775f, 1.0004885f, 1.0002443f, 1.0001221f, 1.0000153f
		};

		internal static readonly float[] TableAb2D = new float[16]
		{
			0f,
			0.5f,
			0.5f,
			0.25f,
			0.5f,
			0.125f,
			0.0625f,
			1f / 32f,
			1f / 64f,
			1f / 128f,
			0.00390625f,
			0.001953125f,
			0.0009765625f,
			0.00048828125f,
			0.00024414062f,
			3.051758E-05f
		};

		internal static readonly int[] TableAb3Codelength = new int[8] { 0, 5, 7, 3, 10, 4, 5, 16 };

		internal static readonly float[] TableAb3Factor = new float[8] { 0f, 0.5f, 0.25f, 0.25f, 0.125f, 0.125f, 0.0625f, 3.0517578E-05f };

		internal static readonly float[] TableAb3C = new float[8] { 0f, 1.3333334f, 1.6f, 1.1428572f, 1.7777778f, 1.0666667f, 1.032258f, 1.0000153f };

		internal static readonly float[] TableAb3D = new float[8] { 0f, 0.5f, 0.5f, 0.25f, 0.5f, 0.125f, 0.0625f, 3.051758E-05f };

		internal static readonly int[] TableAb4Codelength = new int[4] { 0, 5, 7, 16 };

		internal static readonly float[] TableAb4Factor = new float[4] { 0f, 0.5f, 0.25f, 3.0517578E-05f };

		internal static readonly float[] TableAb4C = new float[4] { 0f, 1.3333334f, 1.6f, 1.0000153f };

		internal static readonly float[] TableAb4D = new float[4] { 0f, 0.5f, 0.5f, 3.051758E-05f };

		internal static readonly int[] TableCdCodelength = new int[16]
		{
			0, 5, 7, 10, 4, 5, 6, 7, 8, 9,
			10, 11, 12, 13, 14, 15
		};

		internal static readonly float[][] TableCdGroupingtables = new float[16][]
		{
			null, Grouping5Bits, Grouping7Bits, Grouping10Bits, null, null, null, null, null, null,
			null, null, null, null, null, null
		};

		internal static readonly float[] TableCdFactor = new float[16]
		{
			0f,
			0.5f,
			0.25f,
			0.125f,
			0.125f,
			0.0625f,
			1f / 32f,
			1f / 64f,
			1f / 128f,
			0.00390625f,
			0.001953125f,
			0.0009765625f,
			0.00048828125f,
			0.00024414062f,
			0.00012207031f,
			6.1035156E-05f
		};

		internal static readonly float[] TableCdC = new float[16]
		{
			0f, 1.3333334f, 1.6f, 1.7777778f, 1.0666667f, 1.032258f, 1.0158731f, 1.007874f, 1.0039216f, 1.0019569f,
			1.0009775f, 1.0004885f, 1.0002443f, 1.0001221f, 1.000061f, 1.0000305f
		};

		internal static readonly float[] TableCdD = new float[16]
		{
			0f,
			0.5f,
			0.5f,
			0.5f,
			0.125f,
			0.0625f,
			1f / 32f,
			1f / 64f,
			1f / 128f,
			0.00390625f,
			0.001953125f,
			0.0009765625f,
			0.00048828125f,
			0.00024414062f,
			0.00012207031f,
			6.103516E-05f
		};

		protected int Allocation;

		protected readonly float[] CFactor = new float[1];

		protected readonly int[] Codelength = new int[1];

		protected float[] D = new float[1];

		protected readonly float[] Factor = new float[1];

		protected float[][] Groupingtable;

		protected int Groupnumber;

		protected int Samplenumber;

		protected float[] Samples;

		protected float Scalefactor1;

		protected float Scalefactor2;

		protected float Scalefactor3;

		protected int Scfsi;

		protected readonly int Subbandnumber;

		internal SubbandLayer2(int subbandnumber)
		{
			Subbandnumber = subbandnumber;
			Groupnumber = (Samplenumber = 0);
		}

		private void InitBlock()
		{
			Samples = new float[3];
			Groupingtable = new float[2][];
		}

		protected virtual int GetAllocationLength(Header header)
		{
			if (header.Version() == 1)
			{
				int num = header.bitrate_index();
				if (header.Mode() != 3)
				{
					num = ((num == 4) ? 1 : (num - 4));
				}
				if (num == 1 || num == 2)
				{
					if (Subbandnumber <= 1)
					{
						return 4;
					}
					return 3;
				}
				if (Subbandnumber <= 10)
				{
					return 4;
				}
				if (Subbandnumber <= 22)
				{
					return 3;
				}
				return 2;
			}
			if (Subbandnumber <= 3)
			{
				return 4;
			}
			if (Subbandnumber <= 10)
			{
				return 3;
			}
			return 2;
		}

		protected virtual void PrepareForSampleRead(Header header, int allocation, int channel, float[] factor, int[] codelength, float[] c, float[] d)
		{
			int num = header.bitrate_index();
			if (header.Mode() != 3)
			{
				num = ((num == 4) ? 1 : (num - 4));
			}
			if (num == 1 || num == 2)
			{
				Groupingtable[channel] = TableCdGroupingtables[allocation];
				factor[0] = TableCdFactor[allocation];
				codelength[0] = TableCdCodelength[allocation];
				c[0] = TableCdC[allocation];
				d[0] = TableCdD[allocation];
				return;
			}
			if (Subbandnumber <= 2)
			{
				Groupingtable[channel] = TableAb1Groupingtables[allocation];
				factor[0] = TableAb1Factor[allocation];
				codelength[0] = TableAb1Codelength[allocation];
				c[0] = TableAb1C[allocation];
				d[0] = TableAb1D[allocation];
				return;
			}
			Groupingtable[channel] = TableAb234Groupingtables[allocation];
			if (Subbandnumber <= 10)
			{
				factor[0] = TableAb2Factor[allocation];
				codelength[0] = TableAb2Codelength[allocation];
				c[0] = TableAb2C[allocation];
				d[0] = TableAb2D[allocation];
			}
			else if (Subbandnumber <= 22)
			{
				factor[0] = TableAb3Factor[allocation];
				codelength[0] = TableAb3Codelength[allocation];
				c[0] = TableAb3C[allocation];
				d[0] = TableAb3D[allocation];
			}
			else
			{
				factor[0] = TableAb4Factor[allocation];
				codelength[0] = TableAb4Codelength[allocation];
				c[0] = TableAb4C[allocation];
				d[0] = TableAb4D[allocation];
			}
		}

		internal override void ReadAllocation(Bitstream stream, Header header, Crc16 crc)
		{
			int allocationLength = GetAllocationLength(header);
			Allocation = stream.GetBitsFromBuffer(allocationLength);
			crc?.AddBits(Allocation, allocationLength);
		}

		internal virtual void ReadScaleFactorSelection(Bitstream stream, Crc16 crc)
		{
			if (Allocation != 0)
			{
				Scfsi = stream.GetBitsFromBuffer(2);
				crc?.AddBits(Scfsi, 2);
			}
		}

		internal override void ReadScaleFactor(Bitstream stream, Header header)
		{
			if (Allocation != 0)
			{
				switch (Scfsi)
				{
				case 0:
					Scalefactor1 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					Scalefactor2 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					break;
				case 1:
					Scalefactor1 = (Scalefactor2 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]);
					Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					break;
				case 2:
					Scalefactor1 = (Scalefactor2 = (Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]));
					break;
				case 3:
					Scalefactor1 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					Scalefactor2 = (Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]);
					break;
				}
				PrepareForSampleRead(header, Allocation, 0, Factor, Codelength, CFactor, D);
			}
		}

		internal override bool ReadSampleData(Bitstream stream)
		{
			if (Allocation != 0)
			{
				if (Groupingtable[0] != null)
				{
					int bitsFromBuffer = stream.GetBitsFromBuffer(Codelength[0]);
					int num = bitsFromBuffer + (bitsFromBuffer << 1);
					float[] samples = Samples;
					float[] array = Groupingtable[0];
					int num2 = 0;
					int num3 = num;
					if (num3 > array.Length - 3)
					{
						num3 = array.Length - 3;
					}
					samples[num2] = array[num3];
					num3++;
					num2++;
					samples[num2] = array[num3];
					num3++;
					num2++;
					samples[num2] = array[num3];
				}
				else
				{
					Samples[0] = (float)((double)((float)stream.GetBitsFromBuffer(Codelength[0]) * Factor[0]) - 1.0);
					Samples[1] = (float)((double)((float)stream.GetBitsFromBuffer(Codelength[0]) * Factor[0]) - 1.0);
					Samples[2] = (float)((double)((float)stream.GetBitsFromBuffer(Codelength[0]) * Factor[0]) - 1.0);
				}
			}
			Samplenumber = 0;
			if (++Groupnumber == 12)
			{
				return true;
			}
			return false;
		}

		internal override bool PutNextSample(int channels, SynthesisFilter filter1, SynthesisFilter filter2)
		{
			if (Allocation != 0 && channels != 2)
			{
				float num = Samples[Samplenumber];
				if (Groupingtable[0] == null)
				{
					num = (num + D[0]) * CFactor[0];
				}
				num = ((Groupnumber <= 4) ? (num * Scalefactor1) : ((Groupnumber > 8) ? (num * Scalefactor3) : (num * Scalefactor2)));
				filter1.AddSample(num, Subbandnumber);
			}
			if (++Samplenumber == 3)
			{
				return true;
			}
			return false;
		}
	}
	public class SubbandLayer2IntensityStereo : SubbandLayer2
	{
		protected float Channel2Scalefactor1;

		protected float Channel2Scalefactor2;

		protected float Channel2Scalefactor3;

		protected int Channel2Scfsi;

		internal SubbandLayer2IntensityStereo(int subbandnumber)
			: base(subbandnumber)
		{
		}

		internal override void ReadScaleFactorSelection(Bitstream stream, Crc16 crc)
		{
			if (Allocation != 0)
			{
				Scfsi = stream.GetBitsFromBuffer(2);
				Channel2Scfsi = stream.GetBitsFromBuffer(2);
				if (crc != null)
				{
					crc.AddBits(Scfsi, 2);
					crc.AddBits(Channel2Scfsi, 2);
				}
			}
		}

		internal override void ReadScaleFactor(Bitstream stream, Header header)
		{
			if (Allocation != 0)
			{
				base.ReadScaleFactor(stream, header);
				switch (Channel2Scfsi)
				{
				case 0:
					Channel2Scalefactor1 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					Channel2Scalefactor2 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					break;
				case 1:
					Channel2Scalefactor1 = (Channel2Scalefactor2 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]);
					Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					break;
				case 2:
					Channel2Scalefactor1 = (Channel2Scalefactor2 = (Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]));
					break;
				case 3:
					Channel2Scalefactor1 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					Channel2Scalefactor2 = (Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]);
					break;
				}
			}
		}

		internal override bool PutNextSample(int channels, SynthesisFilter filter1, SynthesisFilter filter2)
		{
			if (Allocation != 0)
			{
				float num = Samples[Samplenumber];
				if (Groupingtable[0] == null)
				{
					num = (num + D[0]) * CFactor[0];
				}
				switch (channels)
				{
				case 0:
				{
					float num2 = num;
					if (Groupnumber <= 4)
					{
						num *= Scalefactor1;
						num2 *= Channel2Scalefactor1;
					}
					else if (Groupnumber <= 8)
					{
						num *= Scalefactor2;
						num2 *= Channel2Scalefactor2;
					}
					else
					{
						num *= Scalefactor3;
						num2 *= Channel2Scalefactor3;
					}
					filter1.AddSample(num, Subbandnumber);
					filter2.AddSample(num2, Subbandnumber);
					break;
				}
				case 1:
					num = ((Groupnumber <= 4) ? (num * Scalefactor1) : ((Groupnumber > 8) ? (num * Scalefactor3) : (num * Scalefactor2)));
					filter1.AddSample(num, Subbandnumber);
					break;
				default:
					num = ((Groupnumber <= 4) ? (num * Channel2Scalefactor1) : ((Groupnumber > 8) ? (num * Channel2Scalefactor3) : (num * Channel2Scalefactor2)));
					filter1.AddSample(num, Subbandnumber);
					break;
				}
			}
			if (++Samplenumber == 3)
			{
				return true;
			}
			return false;
		}
	}
	public class SubbandLayer2Stereo : SubbandLayer2
	{
		protected int Channel2Allocation;

		protected readonly float[] Channel2C = new float[1];

		protected readonly int[] Channel2Codelength = new int[1];

		protected readonly float[] Channel2D = new float[1];

		protected readonly float[] Channel2Factor = new float[1];

		protected readonly float[] Channel2Samples;

		protected float Channel2Scalefactor1;

		protected float Channel2Scalefactor2;

		protected float Channel2Scalefactor3;

		protected int Channel2Scfsi;

		internal SubbandLayer2Stereo(int subbandnumber)
			: base(subbandnumber)
		{
			Channel2Samples = new float[3];
		}

		internal override void ReadAllocation(Bitstream stream, Header header, Crc16 crc)
		{
			int allocationLength = GetAllocationLength(header);
			Allocation = stream.GetBitsFromBuffer(allocationLength);
			Channel2Allocation = stream.GetBitsFromBuffer(allocationLength);
			if (crc != null)
			{
				crc.AddBits(Allocation, allocationLength);
				crc.AddBits(Channel2Allocation, allocationLength);
			}
		}

		internal override void ReadScaleFactorSelection(Bitstream stream, Crc16 crc)
		{
			if (Allocation != 0)
			{
				Scfsi = stream.GetBitsFromBuffer(2);
				crc?.AddBits(Scfsi, 2);
			}
			if (Channel2Allocation != 0)
			{
				Channel2Scfsi = stream.GetBitsFromBuffer(2);
				crc?.AddBits(Channel2Scfsi, 2);
			}
		}

		internal override void ReadScaleFactor(Bitstream stream, Header header)
		{
			base.ReadScaleFactor(stream, header);
			if (Channel2Allocation != 0)
			{
				switch (Channel2Scfsi)
				{
				case 0:
					Channel2Scalefactor1 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					Channel2Scalefactor2 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					break;
				case 1:
					Channel2Scalefactor1 = (Channel2Scalefactor2 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]);
					Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					break;
				case 2:
					Channel2Scalefactor1 = (Channel2Scalefactor2 = (Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]));
					break;
				case 3:
					Channel2Scalefactor1 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)];
					Channel2Scalefactor2 = (Channel2Scalefactor3 = ASubband.ScaleFactors[stream.GetBitsFromBuffer(6)]);
					break;
				}
				PrepareForSampleRead(header, Channel2Allocation, 1, Channel2Factor, Channel2Codelength, Channel2C, Channel2D);
			}
		}

		internal override bool ReadSampleData(Bitstream stream)
		{
			bool result = base.ReadSampleData(stream);
			if (Channel2Allocation != 0)
			{
				if (Groupingtable[1] != null)
				{
					int bitsFromBuffer = stream.GetBitsFromBuffer(Channel2Codelength[0]);
					bitsFromBuffer += bitsFromBuffer << 1;
					float[] channel2Samples = Channel2Samples;
					float[] array = Groupingtable[1];
					int num = 0;
					int num2 = bitsFromBuffer;
					channel2Samples[num] = array[num2];
					num2++;
					num++;
					channel2Samples[num] = array[num2];
					num2++;
					num++;
					channel2Samples[num] = array[num2];
					return result;
				}
				Channel2Samples[0] = (float)((double)((float)stream.GetBitsFromBuffer(Channel2Codelength[0]) * Channel2Factor[0]) - 1.0);
				Channel2Samples[1] = (float)((double)((float)stream.GetBitsFromBuffer(Channel2Codelength[0]) * Channel2Factor[0]) - 1.0);
				Channel2Samples[2] = (float)((double)((float)stream.GetBitsFromBuffer(Channel2Codelength[0]) * Channel2Factor[0]) - 1.0);
			}
			return result;
		}

		internal override bool PutNextSample(int channels, SynthesisFilter filter1, SynthesisFilter filter2)
		{
			bool result = base.PutNextSample(channels, filter1, filter2);
			if (Channel2Allocation != 0 && channels != 1)
			{
				float num = Channel2Samples[Samplenumber - 1];
				if (Groupingtable[1] == null)
				{
					num = (num + Channel2D[0]) * Channel2C[0];
				}
				num = ((Groupnumber <= 4) ? (num * Channel2Scalefactor1) : ((Groupnumber > 8) ? (num * Channel2Scalefactor3) : (num * Channel2Scalefactor2)));
				if (channels == 0)
				{
					filter2.AddSample(num, Subbandnumber);
					return result;
				}
				filter1.AddSample(num, Subbandnumber);
			}
			return result;
		}
	}
}
namespace MP3Sharp.Decoding.Decoders.LayerIII
{
	public class ChannelData
	{
		internal GranuleInfo[] Granules;

		internal int[] ScaleFactorBits;

		internal ChannelData()
		{
			ScaleFactorBits = new int[4];
			Granules = new GranuleInfo[2];
			Granules[0] = new GranuleInfo();
			Granules[1] = new GranuleInfo();
		}
	}
	public class GranuleInfo
	{
		internal int BigValues;

		internal int BlockType;

		internal int Count1TableSelect;

		internal int GlobalGain;

		internal int MixedBlockFlag;

		internal int Part23Length;

		internal int Preflag;

		internal int Region0Count;

		internal int Region1Count;

		internal int ScaleFacCompress;

		internal int ScaleFacScale;

		internal int[] SubblockGain;

		internal int[] TableSelect;

		internal int WindowSwitchingFlag;

		internal GranuleInfo()
		{
			TableSelect = new int[3];
			SubblockGain = new int[3];
		}
	}
	public class Layer3SideInfo
	{
		internal ChannelData[] Channels;

		internal int MainDataBegin;

		internal int PrivateBits;

		internal Layer3SideInfo()
		{
			Channels = new ChannelData[2];
			Channels[0] = new ChannelData();
			Channels[1] = new ChannelData();
		}
	}
	public class SBI
	{
		internal int[] L;

		internal int[] S;

		internal SBI()
		{
			L = new int[23];
			S = new int[14];
		}

		internal SBI(int[] thel, int[] thes)
		{
			L = thel;
			S = thes;
		}
	}
	public class ScaleFactorData
	{
		internal int[] L;

		internal int[][] S;

		internal ScaleFactorData()
		{
			L = new int[23];
			S = new int[3][];
			for (int i = 0; i < 3; i++)
			{
				S[i] = new int[13];
			}
		}
	}
	public class ScaleFactorTable
	{
		internal int[] L;

		internal int[] S;

		private LayerIIIDecoder _EnclosingInstance;

		internal LayerIIIDecoder EnclosingInstance => _EnclosingInstance;

		internal ScaleFactorTable(LayerIIIDecoder enclosingInstance)
		{
			InitBlock(enclosingInstance);
			L = new int[5];
			S = new int[3];
		}

		internal ScaleFactorTable(LayerIIIDecoder enclosingInstance, int[] thel, int[] thes)
		{
			InitBlock(enclosingInstance);
			L = thel;
			S = thes;
		}

		private void InitBlock(LayerIIIDecoder enclosingInstance)
		{
			_EnclosingInstance = enclosingInstance;
		}
	}
}
