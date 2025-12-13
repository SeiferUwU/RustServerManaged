#define TRACE
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DnsClient.Internal;
using DnsClient.Linux;
using DnsClient.Protocol;
using DnsClient.Protocol.Options;
using DnsClient.Windows;
using DnsClient.Windows.IpHlpApi;
using Microsoft.Win32;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: CLSCompliant(true)]
[assembly: InternalsVisibleTo("DnsClient.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100676f9d5ff1e268c55fda5578e9f09f27b5fdfadc2b96eec28616532974ffdab2551ac7082ef0037690e3f859328da8425afc284333a808f01b5bbef674a615723b1085b6404b293e10dc8132d5636b692edab794ada3f53711175f0520d3d84e217fc9269de230ee8ca90415f919514776435bff5cb94cad1652a90ead386fc1")]
[assembly: InternalsVisibleTo("Benchmarks, PublicKey=0024000004800000940000000602000000240000525341310004000001000100676f9d5ff1e268c55fda5578e9f09f27b5fdfadc2b96eec28616532974ffdab2551ac7082ef0037690e3f859328da8425afc284333a808f01b5bbef674a615723b1085b6404b293e10dc8132d5636b692edab794ada3f53711175f0520d3d84e217fc9269de230ee8ca90415f919514776435bff5cb94cad1652a90ead386fc1")]
[assembly: TargetFramework(".NETStandard,Version=v2.1", FrameworkDisplayName = "")]
[assembly: AssemblyCompany("MichaCo")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("Copyright (c) 2021 Michael Conrad")]
[assembly: AssemblyDescription("DnsClient.NET is a simple yet very powerful and high performance open source library for the .NET Framework to do DNS lookups")]
[assembly: AssemblyFileVersion("1.7.0.0")]
[assembly: AssemblyInformationalVersion("1.7.0")]
[assembly: AssemblyProduct("DnsClient.NET")]
[assembly: AssemblyTitle("DnsClient")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/MichaCo/DnsClient.NET")]
[assembly: AssemblyVersion("1.7.0.0")]
internal static class Interop
{
	internal static class Libraries
	{
		internal const string IpHlpApi = "iphlpapi.dll";
	}

	internal static class IpHlpApi
	{
		internal struct FIXED_INFO
		{
			public const int MAX_HOSTNAME_LEN = 128;

			public const int MAX_DOMAIN_NAME_LEN = 128;

			public const int MAX_SCOPE_ID_LEN = 256;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
			public string hostName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
			public string domainName;

			public IntPtr currentDnsServer;

			public IP_ADDR_STRING DnsServerList;

			public uint nodeType;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string scopeId;

			public bool enableRouting;

			public bool enableProxy;

			public bool enableDns;
		}

		internal struct IP_ADDR_STRING
		{
			public IntPtr Next;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string IpAddress;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
			public string IpMask;

			public uint Context;
		}

		internal const uint ERROR_SUCCESS = 0u;

		internal const uint ERROR_INVALID_FUNCTION = 1u;

		internal const uint ERROR_NO_SUCH_DEVICE = 2u;

		internal const uint ERROR_INVALID_DATA = 13u;

		internal const uint ERROR_INVALID_PARAMETER = 87u;

		internal const uint ERROR_BUFFER_OVERFLOW = 111u;

		internal const uint ERROR_INSUFFICIENT_BUFFER = 122u;

		internal const uint ERROR_NO_DATA = 232u;

		internal const uint ERROR_IO_PENDING = 997u;

		internal const uint ERROR_NOT_FOUND = 1168u;

		[DllImport("iphlpapi.dll")]
		internal static extern uint GetAdaptersAddresses(AddressFamily family, uint flags, IntPtr pReserved, IntPtr adapterAddresses, ref uint outBufLen);

		[DllImport("iphlpapi.dll", ExactSpelling = true)]
		internal static extern uint GetNetworkParams(IntPtr pFixedInfo, ref uint pOutBufLen);
	}
}
namespace System.Threading.Tasks
{
	internal static class TaskExtensions
	{
		public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
			using (cancellationToken.Register(delegate(object s)
			{
				((TaskCompletionSource<bool>)s).TrySetResult(result: true);
			}, taskCompletionSource))
			{
				if (task != await Task.WhenAny(task, taskCompletionSource.Task).ConfigureAwait(continueOnCapturedContext: false))
				{
					task.ContinueWith((Task<T> t) => t.Exception);
					throw new OperationCanceledException(cancellationToken);
				}
			}
			return await task.ConfigureAwait(continueOnCapturedContext: false);
		}
	}
}
namespace System.Linq
{
	public static class RecordCollectionExtension
	{
		public static IEnumerable<AddressRecord> AddressRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<AddressRecord>();
		}

		public static IEnumerable<ARecord> ARecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<ARecord>();
		}

		public static IEnumerable<NsRecord> NsRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<NsRecord>();
		}

		public static IEnumerable<CNameRecord> CnameRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<CNameRecord>();
		}

		[CLSCompliant(false)]
		public static IEnumerable<SoaRecord> SoaRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<SoaRecord>();
		}

		public static IEnumerable<MbRecord> MbRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<MbRecord>();
		}

		public static IEnumerable<MgRecord> MgRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<MgRecord>();
		}

		public static IEnumerable<MrRecord> MrRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<MrRecord>();
		}

		public static IEnumerable<NullRecord> NullRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<NullRecord>();
		}

		public static IEnumerable<WksRecord> WksRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<WksRecord>();
		}

		public static IEnumerable<PtrRecord> PtrRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<PtrRecord>();
		}

		public static IEnumerable<HInfoRecord> HInfoRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<HInfoRecord>();
		}

		[CLSCompliant(false)]
		public static IEnumerable<MxRecord> MxRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<MxRecord>();
		}

		public static IEnumerable<TxtRecord> TxtRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<TxtRecord>();
		}

		public static IEnumerable<RpRecord> RpRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<RpRecord>();
		}

		public static IEnumerable<AfsDbRecord> AfsDbRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<AfsDbRecord>();
		}

		public static IEnumerable<AaaaRecord> AaaaRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<AaaaRecord>();
		}

		[CLSCompliant(false)]
		public static IEnumerable<SrvRecord> SrvRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<SrvRecord>();
		}

		public static IEnumerable<NAPtrRecord> NAPtrRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<NAPtrRecord>();
		}

		public static IEnumerable<UriRecord> UriRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<UriRecord>();
		}

		public static IEnumerable<CaaRecord> CaaRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<CaaRecord>();
		}

		public static IEnumerable<TlsaRecord> TlsaRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<TlsaRecord>();
		}

		public static IEnumerable<RRSigRecord> RRSigRecords(this IEnumerable<DnsResourceRecord> records)
		{
			return records.OfType<RRSigRecord>();
		}

		public static IEnumerable<DnsResourceRecord> OfRecordType(this IEnumerable<DnsResourceRecord> records, ResourceRecordType type)
		{
			return records.Where((DnsResourceRecord p) => p.RecordType == type);
		}
	}
}
namespace System.Net
{
	public static class IpAddressExtensions
	{
		public static string GetArpaName(this IPAddress ip)
		{
			byte[] addressBytes = ip.GetAddressBytes();
			Array.Reverse(addressBytes);
			if (ip.AddressFamily == AddressFamily.InterNetworkV6)
			{
				return addressBytes.SelectMany((byte b) => new int[2]
				{
					b & 0xF,
					(b >> 4) & 0xF
				}).Aggregate(new StringBuilder(), (StringBuilder s, int b) => s.Append(b.ToString("x")).Append('.'))?.ToString() + "ip6.arpa.";
			}
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				return string.Join(".", addressBytes) + ".in-addr.arpa.";
			}
			throw new ArgumentException($"Unsupported address family '{ip.AddressFamily}'.", "ip");
		}
	}
}
namespace System.IO
{
	internal struct RowConfigReader
	{
		private readonly string _buffer;

		private readonly StringComparison _comparisonKind;

		private int _currentIndex;

		public RowConfigReader(string buffer)
		{
			_buffer = buffer;
			_comparisonKind = StringComparison.Ordinal;
			_currentIndex = 0;
		}

		public RowConfigReader(string buffer, StringComparison comparisonKind)
		{
			_buffer = buffer;
			_comparisonKind = comparisonKind;
			_currentIndex = 0;
		}

		public string GetNextValue(string key)
		{
			if (!TryGetNextValue(key, out var value))
			{
				throw new InvalidOperationException("Couldn't get next value with key " + key);
			}
			return value;
		}

		public bool TryGetNextValue(string key, out string value)
		{
			if (_currentIndex >= _buffer.Length)
			{
				value = null;
				return false;
			}
			if (!TryFindNextKeyOccurrence(key, _currentIndex, out var keyIndex))
			{
				value = null;
				return false;
			}
			int startIndex = keyIndex + key.Length;
			int num = _buffer.IndexOf(Environment.NewLine, startIndex, _comparisonKind);
			int num2;
			if (num == -1)
			{
				num = _buffer.Length - 1;
				num2 = num;
			}
			else
			{
				num2 = num - 1;
			}
			int count = num - keyIndex;
			int num3 = _buffer.LastIndexOf('\t', num, count);
			if (num3 == -1)
			{
				num3 = _buffer.LastIndexOf(' ', num, count);
			}
			int num4 = num3 + 1;
			int num5 = num2 - num3;
			if (num4 <= keyIndex || num4 == -1 || num5 == 0)
			{
				value = null;
				return false;
			}
			value = _buffer.Substring(num4, num5);
			_currentIndex = num + 1;
			return true;
		}

		private bool TryFindNextKeyOccurrence(string key, int startIndex, out int keyIndex)
		{
			while (true)
			{
				keyIndex = _buffer.IndexOf(key, startIndex, _comparisonKind);
				if (keyIndex == -1)
				{
					return false;
				}
				if ((keyIndex == 0 || (keyIndex >= Environment.NewLine.Length && _buffer.Substring(keyIndex - Environment.NewLine.Length, Environment.NewLine.Length) == Environment.NewLine)) && HasFollowingWhitespace(keyIndex, key.Length))
				{
					break;
				}
				startIndex += key.Length;
			}
			return true;
		}

		private bool HasFollowingWhitespace(int keyIndex, int length)
		{
			if (keyIndex + length < _buffer.Length)
			{
				if (_buffer[keyIndex + length] != ' ')
				{
					return _buffer[keyIndex + length] == '\t';
				}
				return true;
			}
			return false;
		}

		public int GetNextValueAsInt32(string key)
		{
			string nextValue = GetNextValue(key);
			if (int.TryParse(nextValue, out var result))
			{
				return result;
			}
			throw new InvalidOperationException("Unable to parse value " + nextValue + " of key " + key + " as an Int32.");
		}

		public static string ReadFirstValueFromString(string data, string key)
		{
			return new RowConfigReader(data).GetNextValue(key);
		}
	}
}
namespace DnsClient
{
	public class DnsDatagramReader
	{
		public const int IPv6Length = 16;

		public const int IPv4Length = 4;

		private const byte ReferenceByte = 192;

		private const string ACEPrefix = "xn--";

		private const int MaxRecursion = 100;

		private readonly byte[] _ipV4Buffer = new byte[4];

		private readonly byte[] _ipV6Buffer = new byte[16];

		private readonly ArraySegment<byte> _data;

		private readonly int _count;

		public int Index { get; private set; }

		public bool DataAvailable
		{
			get
			{
				if (_count - _data.Offset > 0)
				{
					return Index < _count;
				}
				return false;
			}
		}

		public DnsDatagramReader(ArraySegment<byte> data, int startIndex = 0)
		{
			if (startIndex < 0 || startIndex > data.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			_data = data;
			_count = data.Count;
			Index = startIndex;
		}

		public string ReadStringWithLengthPrefix()
		{
			byte length = ReadByte();
			return ReadString(length);
		}

		public string ReadString(int length)
		{
			if (_count < Index + length)
			{
				throw new DnsResponseParseException("Cannot read string.", _data.ToArray(), Index, length);
			}
			string result = Encoding.ASCII.GetString(_data.Array, _data.Offset + Index, length);
			Index += length;
			return result;
		}

		public static string ParseString(ArraySegment<byte> data)
		{
			return ParseString(new DnsDatagramReader(data), data.Count);
		}

		public static string ParseString(DnsDatagramReader reader, int length)
		{
			if (reader._count < reader.Index + length)
			{
				throw new DnsResponseParseException("Cannot parse string.", reader._data.ToArray(), reader.Index, length);
			}
			StringBuilder stringBuilder = StringBuilderObjectPool.Default.Get();
			for (int i = 0; i < length; i++)
			{
				byte b = reader.ReadByte();
				char c = (char)b;
				if (b < 32 || b > 126)
				{
					stringBuilder.Append("\\" + b.ToString("000", CultureInfo.InvariantCulture));
					continue;
				}
				switch (c)
				{
				case ';':
					stringBuilder.Append("\\;");
					break;
				case '\\':
					stringBuilder.Append("\\\\");
					break;
				case '"':
					stringBuilder.Append("\\\"");
					break;
				default:
					stringBuilder.Append(c);
					break;
				}
			}
			string result = stringBuilder.ToString();
			StringBuilderObjectPool.Default.Return(stringBuilder);
			return result;
		}

		public static string ReadUTF8String(ArraySegment<byte> data)
		{
			return Encoding.UTF8.GetString(data.Array, data.Offset, data.Count);
		}

		public byte ReadByte()
		{
			if (_count < Index + 1)
			{
				throw new DnsResponseParseException("Cannot read byte.", _data.ToArray(), Index, 1);
			}
			return _data.Array[_data.Offset + Index++];
		}

		public ArraySegment<byte> ReadBytes(int length)
		{
			if (_count < Index + length)
			{
				throw new DnsResponseParseException("Cannot read bytes.", _data.ToArray(), Index, length);
			}
			ArraySegment<byte> result = new ArraySegment<byte>(_data.Array, _data.Offset + Index, length);
			Index += length;
			return result;
		}

		public ArraySegment<byte> ReadBytesToEnd(int startIndex, int lengthOfRawData)
		{
			int num = Index - startIndex;
			int length = lengthOfRawData - num;
			return ReadBytes(length);
		}

		public IPAddress ReadIPAddress()
		{
			if (_count < Index + 4)
			{
				throw new DnsResponseParseException($"Cannot read IPv4 address, expected {4} bytes.", _data.ToArray(), Index, 4);
			}
			_ipV4Buffer[0] = _data.Array[_data.Offset + Index];
			_ipV4Buffer[1] = _data.Array[_data.Offset + Index + 1];
			_ipV4Buffer[2] = _data.Array[_data.Offset + Index + 2];
			_ipV4Buffer[3] = _data.Array[_data.Offset + Index + 3];
			Advance(4);
			return new IPAddress(_ipV4Buffer);
		}

		public IPAddress ReadIPv6Address()
		{
			if (_count < Index + 16)
			{
				throw new DnsResponseParseException($"Cannot read IPv6 address, expected {16} bytes.", _data.ToArray(), Index, 16);
			}
			for (int i = 0; i < 16; i++)
			{
				_ipV6Buffer[i] = _data.Array[_data.Offset + Index + i];
			}
			Advance(16);
			return new IPAddress(_ipV6Buffer);
		}

		public void Advance(int length)
		{
			if (_count < Index + length)
			{
				throw new DnsResponseParseException("Cannot advance the reader.", _data.ToArray(), Index, length);
			}
			Index += length;
		}

		public DnsString ReadDnsName()
		{
			StringBuilder stringBuilder = StringBuilderObjectPool.Default.Get();
			StringBuilder stringBuilder2 = StringBuilderObjectPool.Default.Get();
			foreach (ArraySegment<byte> item in ReadLabels())
			{
				foreach (byte item2 in item)
				{
					char c = (char)item2;
					if (item2 < 32 || item2 > 126)
					{
						stringBuilder.Append("\\" + item2.ToString("000"));
						continue;
					}
					switch (c)
					{
					case ';':
						stringBuilder.Append("\\;");
						break;
					case '\\':
						stringBuilder.Append("\\\\");
						break;
					case '"':
						stringBuilder.Append("\\\"");
						break;
					default:
						stringBuilder.Append(c);
						break;
					}
				}
				stringBuilder.Append('.');
				string text = Encoding.UTF8.GetString(item.Array, item.Offset, item.Count);
				if (text.StartsWith("xn--", StringComparison.Ordinal))
				{
					try
					{
						text = DnsString.s_idn.GetUnicode(text);
					}
					catch
					{
					}
				}
				stringBuilder2.Append(text);
				stringBuilder2.Append('.');
			}
			string text2 = stringBuilder.ToString();
			if (text2.Length == 0 || text2[text2.Length - 1] != '.')
			{
				text2 += ".";
			}
			string text3 = stringBuilder2.ToString();
			if (text3.Length == 0 || text3[text3.Length - 1] != '.')
			{
				text3 += ".";
			}
			StringBuilderObjectPool.Default.Return(stringBuilder);
			StringBuilderObjectPool.Default.Return(stringBuilder2);
			return new DnsString(text3, text2);
		}

		public DnsString ReadQuestionQueryString()
		{
			StringBuilder stringBuilder = StringBuilderObjectPool.Default.Get();
			foreach (ArraySegment<byte> item in ReadLabels())
			{
				string value = Encoding.UTF8.GetString(item.Array, item.Offset, item.Count);
				stringBuilder.Append(value);
				stringBuilder.Append('.');
			}
			string query = stringBuilder.ToString();
			StringBuilderObjectPool.Default.Return(stringBuilder);
			return DnsString.FromResponseQueryString(query);
		}

		public IReadOnlyList<ArraySegment<byte>> ReadLabels(int recursion = 0)
		{
			if (recursion++ >= 100)
			{
				throw new DnsResponseParseException("Max recursion reached.", _data.ToArray(), Index);
			}
			List<ArraySegment<byte>> list = new List<ArraySegment<byte>>(10);
			byte b;
			while ((b = ReadByte()) != 0)
			{
				if ((b & 0xC0) != 0)
				{
					int num = ((b & 0x3F) << 8) | ReadByte();
					if (num < _data.Array.Length - 1)
					{
						IReadOnlyList<ArraySegment<byte>> collection = new DnsDatagramReader(_data.SubArrayFromOriginal(num)).ReadLabels(recursion);
						list.AddRange(collection);
						return list;
					}
					Index--;
					list.Add(_data.SubArray(Index, b));
					Advance(b);
				}
				else
				{
					if (Index + b >= _count)
					{
						throw new DnsResponseParseException("Found invalid label position.", _data.ToArray(), Index, b);
					}
					ArraySegment<byte> item = _data.SubArray(Index, b);
					list.Add(item);
					Advance(b);
				}
			}
			return list;
		}

		public ushort ReadUInt16()
		{
			if (_count < Index + 2)
			{
				throw new DnsResponseParseException("Cannot read more Int16.", _data.ToArray(), Index, 2);
			}
			ushort result = BitConverter.ToUInt16(_data.Array, _data.Offset + Index);
			Index += 2;
			return result;
		}

		public ushort ReadUInt16NetworkOrder()
		{
			if (_count < Index + 2)
			{
				throw new DnsResponseParseException("Cannot read more Int16.", _data.ToArray(), Index, 2);
			}
			return (ushort)((ReadByte() << 8) | ReadByte());
		}

		public uint ReadUInt32NetworkOrder()
		{
			if (_count < Index + 4)
			{
				throw new DnsResponseParseException("Cannot read more Int32.", _data.ToArray(), Index, 4);
			}
			return (uint)((ReadUInt16NetworkOrder() << 16) | ReadUInt16NetworkOrder());
		}

		internal void SanitizeResult(int expectedIndex, int dataLength)
		{
			if (Index != expectedIndex)
			{
				throw new DnsResponseParseException($"Record reader index out of sync. Expected to read till {expectedIndex} but tried to read till index {Index}.", _data.ToArray(), Index, dataLength);
			}
		}
	}
	internal static class ArraySegmentExtensions
	{
		public static ArraySegment<T> SubArray<T>(this ArraySegment<T> array, int startIndex, int length)
		{
			return new ArraySegment<T>(array.Array, array.Offset + startIndex, length);
		}

		public static ArraySegment<T> SubArrayFromOriginal<T>(this ArraySegment<T> array, int startIndex)
		{
			return new ArraySegment<T>(array.Array, startIndex, array.Array.Length - startIndex);
		}
	}
	internal class DnsDatagramWriter : IDisposable
	{
		public const int BufferSize = 1024;

		private const byte DotByte = 46;

		private readonly PooledBytes _pooledBytes;

		private readonly ArraySegment<byte> _buffer;

		public ArraySegment<byte> Data => new ArraySegment<byte>(_buffer.Array, 0, Index);

		public int Index { get; set; }

		public DnsDatagramWriter()
		{
			_pooledBytes = new PooledBytes(1024);
			_buffer = new ArraySegment<byte>(_pooledBytes.Buffer, 0, 1024);
		}

		public DnsDatagramWriter(ArraySegment<byte> useBuffer)
		{
			_buffer = useBuffer;
		}

		public virtual void WriteHostName(string queryName)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(queryName);
			int num = 0;
			int num2 = 0;
			if (bytes.Length <= 1)
			{
				WriteByte(0);
				return;
			}
			byte[] array = bytes;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == 46)
				{
					WriteByte((byte)(num2 - num));
					WriteBytes(bytes, num, num2 - num);
					num = num2 + 1;
				}
				num2++;
			}
			WriteByte(0);
		}

		public virtual void WriteStringWithLengthPrefix(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			int num = bytes.Length;
			if (num > 255)
			{
				throw new ArgumentException("Value is too long.", "value");
			}
			WriteByte((byte)num);
			WriteBytes(bytes, num);
		}

		public virtual void WriteByte(byte b)
		{
			_buffer.Array[_buffer.Offset + Index++] = b;
		}

		public virtual void WriteBytes(byte[] data, int length)
		{
			WriteBytes(data, 0, length);
		}

		public virtual void WriteBytes(byte[] data, int dataOffset, int length)
		{
			Buffer.BlockCopy(data, dataOffset, _buffer.Array, _buffer.Offset + Index, length);
			Index += length;
		}

		public virtual void WriteInt16NetworkOrder(short value)
		{
			byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
			WriteBytes(bytes, bytes.Length);
		}

		public virtual void WriteInt32NetworkOrder(int value)
		{
			byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
			WriteBytes(bytes, bytes.Length);
		}

		public virtual void WriteUInt16NetworkOrder(ushort value)
		{
			WriteInt16NetworkOrder((short)value);
		}

		public virtual void WriteUInt32NetworkOrder(uint value)
		{
			WriteInt32NetworkOrder((int)value);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_pooledBytes?.Dispose();
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}
	}
	internal enum DnsMessageHandleType
	{
		None,
		UDP,
		TCP
	}
	internal abstract class DnsMessageHandler
	{
		public abstract DnsMessageHandleType Type { get; }

		public abstract DnsResponseMessage Query(IPEndPoint endpoint, DnsRequestMessage request, TimeSpan timeout);

		public abstract Task<DnsResponseMessage> QueryAsync(IPEndPoint endpoint, DnsRequestMessage request, CancellationToken cancellationToken);

		public static bool IsTransientException(Exception exception)
		{
			if (exception is IOException)
			{
				exception = exception?.InnerException ?? exception;
			}
			if (exception is SocketException { SocketErrorCode: var socketErrorCode })
			{
				switch (socketErrorCode)
				{
				case SocketError.OperationAborted:
				case SocketError.ConnectionAborted:
				case SocketError.ConnectionReset:
				case SocketError.TimedOut:
				case SocketError.TryAgain:
					return true;
				}
			}
			return false;
		}

		protected static void ValidateResponse(DnsRequestMessage request, DnsResponseMessage response)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			if (request.Header.Id != response.Header.Id)
			{
				throw new DnsXidMismatchException(request.Header.Id, response.Header.Id);
			}
		}

		public virtual void GetRequestData(DnsRequestMessage request, DnsDatagramWriter writer)
		{
			DnsQuestion question = request.Question;
			writer.WriteInt16NetworkOrder((short)request.Header.Id);
			writer.WriteUInt16NetworkOrder(request.Header.RawFlags);
			writer.WriteInt16NetworkOrder(1);
			writer.WriteInt16NetworkOrder(0);
			writer.WriteInt16NetworkOrder(0);
			if (request.QuerySettings.UseExtendedDns)
			{
				writer.WriteInt16NetworkOrder(1);
			}
			else
			{
				writer.WriteInt16NetworkOrder(0);
			}
			writer.WriteHostName(question.QueryName);
			writer.WriteUInt16NetworkOrder((ushort)question.QuestionType);
			writer.WriteUInt16NetworkOrder((ushort)question.QuestionClass);
			if (request.QuerySettings.UseExtendedDns)
			{
				OptRecord optRecord = new OptRecord(request.QuerySettings.ExtendedDnsBufferSize, 0, request.QuerySettings.RequestDnsSecRecords);
				writer.WriteHostName("");
				writer.WriteUInt16NetworkOrder((ushort)optRecord.RecordType);
				writer.WriteUInt16NetworkOrder((ushort)optRecord.RecordClass);
				writer.WriteUInt32NetworkOrder((ushort)optRecord.InitialTimeToLive);
				writer.WriteUInt16NetworkOrder(0);
			}
		}

		public virtual DnsResponseMessage GetResponseMessage(ArraySegment<byte> responseData)
		{
			DnsDatagramReader dnsDatagramReader = new DnsDatagramReader(responseData);
			DnsRecordFactory dnsRecordFactory = new DnsRecordFactory(dnsDatagramReader);
			ushort id = dnsDatagramReader.ReadUInt16NetworkOrder();
			ushort flags = dnsDatagramReader.ReadUInt16NetworkOrder();
			ushort num = dnsDatagramReader.ReadUInt16NetworkOrder();
			ushort num2 = dnsDatagramReader.ReadUInt16NetworkOrder();
			ushort num3 = dnsDatagramReader.ReadUInt16NetworkOrder();
			ushort num4 = dnsDatagramReader.ReadUInt16NetworkOrder();
			DnsResponseMessage dnsResponseMessage = new DnsResponseMessage(new DnsResponseHeader(id, flags, num, num2, num4, num3), responseData.Count);
			for (int i = 0; i < num; i++)
			{
				DnsQuestion question = new DnsQuestion(dnsDatagramReader.ReadQuestionQueryString(), (QueryType)dnsDatagramReader.ReadUInt16NetworkOrder(), (QueryClass)dnsDatagramReader.ReadUInt16NetworkOrder());
				dnsResponseMessage.AddQuestion(question);
			}
			for (int j = 0; j < num2; j++)
			{
				ResourceRecordInfo info = dnsRecordFactory.ReadRecordInfo();
				DnsResourceRecord record = dnsRecordFactory.GetRecord(info);
				dnsResponseMessage.AddAnswer(record);
			}
			for (int k = 0; k < num3; k++)
			{
				ResourceRecordInfo info2 = dnsRecordFactory.ReadRecordInfo();
				DnsResourceRecord record2 = dnsRecordFactory.GetRecord(info2);
				dnsResponseMessage.AddAuthority(record2);
			}
			for (int l = 0; l < num4; l++)
			{
				ResourceRecordInfo info3 = dnsRecordFactory.ReadRecordInfo();
				DnsResourceRecord record3 = dnsRecordFactory.GetRecord(info3);
				dnsResponseMessage.AddAdditional(record3);
			}
			return dnsResponseMessage;
		}
	}
	public enum DnsOpCode : short
	{
		Query,
		[Obsolete("obsolete per RFC")]
		IQuery,
		Status,
		Unassinged3,
		Notify,
		Update,
		Unassinged6,
		Unassinged7,
		Unassinged8,
		Unassinged9,
		Unassinged10,
		Unassinged11,
		Unassinged12,
		Unassinged13,
		Unassinged14,
		Unassinged15
	}
	public static class DnsQueryExtensions
	{
		public static IPHostEntry GetHostEntry(this IDnsQuery query, string hostNameOrAddress)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (string.IsNullOrWhiteSpace(hostNameOrAddress))
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			if (IPAddress.TryParse(hostNameOrAddress, out var address))
			{
				return query.GetHostEntry(address);
			}
			return GetHostEntryFromName(query, hostNameOrAddress);
		}

		public static Task<IPHostEntry> GetHostEntryAsync(this IDnsQuery query, string hostNameOrAddress)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (string.IsNullOrWhiteSpace(hostNameOrAddress))
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			if (IPAddress.TryParse(hostNameOrAddress, out var address))
			{
				return query.GetHostEntryAsync(address);
			}
			return GetHostEntryFromNameAsync(query, hostNameOrAddress);
		}

		public static IPHostEntry GetHostEntry(this IDnsQuery query, IPAddress address)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			string hostName = query.GetHostName(address);
			if (string.IsNullOrWhiteSpace(hostName))
			{
				return null;
			}
			return GetHostEntryFromName(query, hostName);
		}

		public static async Task<IPHostEntry> GetHostEntryAsync(this IDnsQuery query, IPAddress address)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			string text = await query.GetHostNameAsync(address).ConfigureAwait(continueOnCapturedContext: false);
			if (string.IsNullOrWhiteSpace(text))
			{
				return null;
			}
			return await GetHostEntryFromNameAsync(query, text).ConfigureAwait(continueOnCapturedContext: false);
		}

		private static IPHostEntry GetHostEntryFromName(IDnsQuery query, string hostName)
		{
			if (string.IsNullOrWhiteSpace(hostName))
			{
				throw new ArgumentNullException("hostName");
			}
			DnsString dnsString = DnsString.FromResponseQueryString(hostName);
			IDnsQueryResponse dnsQueryResponse = query.Query(dnsString, QueryType.A);
			DnsResourceRecord[] allRecords = Enumerable.Concat(second: query.Query(dnsString, QueryType.AAAA).Answers, first: dnsQueryResponse.Answers).ToArray();
			return GetHostEntryProcessResult(dnsString, allRecords);
		}

		private static async Task<IPHostEntry> GetHostEntryFromNameAsync(IDnsQuery query, string hostName)
		{
			if (string.IsNullOrWhiteSpace(hostName))
			{
				throw new ArgumentNullException("hostName");
			}
			DnsString hostString = DnsString.FromResponseQueryString(hostName);
			Task<IDnsQueryResponse> ipv4Result = query.QueryAsync(hostString, QueryType.A);
			Task<IDnsQueryResponse> ipv6Result = query.QueryAsync(hostString, QueryType.AAAA);
			await Task.WhenAll<IDnsQueryResponse>(ipv4Result, ipv6Result).ConfigureAwait(continueOnCapturedContext: false);
			DnsResourceRecord[] allRecords = ipv4Result.Result.Answers.Concat(ipv6Result.Result.Answers).ToArray();
			return GetHostEntryProcessResult(hostString, allRecords);
		}

		private static IPHostEntry GetHostEntryProcessResult(DnsString hostString, DnsResourceRecord[] allRecords)
		{
			var array = (from p in allRecords.OfType<AddressRecord>()
				select new
				{
					Address = p.Address,
					Alias = DnsString.FromResponseQueryString(p.DomainName)
				}).ToArray();
			IPHostEntry iPHostEntry = new IPHostEntry
			{
				Aliases = new string[0],
				AddressList = array.Select(p => p.Address).ToArray()
			};
			if (array.Length > 1)
			{
				if (array.Any(p => !p.Alias.Equals(hostString)))
				{
					iPHostEntry.Aliases = (from p in array
						select p.Alias.ToString() into p
						select p.Substring(0, p.Length - 1)).Distinct().ToArray();
				}
			}
			else if (array.Length == 1 && allRecords.Any((DnsResourceRecord p) => !DnsString.FromResponseQueryString(p.DomainName).Equals(hostString)))
			{
				iPHostEntry.Aliases = (from p in allRecords
					select p.DomainName.ToString() into p
					select p.Substring(0, p.Length - 1)).Distinct().ToArray();
			}
			iPHostEntry.HostName = hostString.Value.Substring(0, hostString.Value.Length - 1);
			return iPHostEntry;
		}

		public static string GetHostName(this IDnsQuery query, IPAddress address)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return GetHostNameAsyncProcessResult(query.QueryReverse(address));
		}

		public static async Task<string> GetHostNameAsync(this IDnsQuery query, IPAddress address)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return GetHostNameAsyncProcessResult(await query.QueryReverseAsync(address).ConfigureAwait(continueOnCapturedContext: false));
		}

		private static string GetHostNameAsyncProcessResult(IDnsQueryResponse result)
		{
			if (result.HasError)
			{
				return null;
			}
			DnsString dnsString = result.Answers.PtrRecords().FirstOrDefault()?.PtrDomainName;
			if (string.IsNullOrWhiteSpace(dnsString))
			{
				return null;
			}
			return dnsString.Value.Substring(0, dnsString.Value.Length - 1);
		}

		public static ServiceHostEntry[] ResolveService(this IDnsQuery query, string baseDomain, string serviceName, ProtocolType protocol)
		{
			if (protocol == ProtocolType.IP || protocol == ProtocolType.Unknown)
			{
				return query.ResolveService(baseDomain, serviceName);
			}
			return query.ResolveService(baseDomain, serviceName, protocol.ToString());
		}

		public static Task<ServiceHostEntry[]> ResolveServiceAsync(this IDnsQuery query, string baseDomain, string serviceName, ProtocolType protocol)
		{
			if (protocol == ProtocolType.IP || protocol == ProtocolType.Unknown)
			{
				return query.ResolveServiceAsync(baseDomain, serviceName);
			}
			return query.ResolveServiceAsync(baseDomain, serviceName, protocol.ToString());
		}

		public static ServiceHostEntry[] ResolveService(this IDnsQuery query, string baseDomain, string serviceName, string tag = null)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (baseDomain == null)
			{
				throw new ArgumentNullException("baseDomain");
			}
			if (string.IsNullOrWhiteSpace(serviceName))
			{
				throw new ArgumentNullException("serviceName");
			}
			string query2 = ConcatServiceName(baseDomain, serviceName, tag);
			return ResolveServiceProcessResult(query.Query(query2, QueryType.SRV));
		}

		public static ServiceHostEntry[] ResolveService(this IDnsQuery query, string service)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (string.IsNullOrWhiteSpace(service))
			{
				throw new ArgumentNullException("service");
			}
			return ResolveServiceProcessResult(query.Query(service, QueryType.SRV));
		}

		public static async Task<ServiceHostEntry[]> ResolveServiceAsync(this IDnsQuery query, string baseDomain, string serviceName, string tag = null)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (baseDomain == null)
			{
				throw new ArgumentNullException("baseDomain");
			}
			if (string.IsNullOrWhiteSpace(serviceName))
			{
				throw new ArgumentNullException("serviceName");
			}
			string query2 = ConcatServiceName(baseDomain, serviceName, tag);
			return ResolveServiceProcessResult(await query.QueryAsync(query2, QueryType.SRV).ConfigureAwait(continueOnCapturedContext: false));
		}

		public static async Task<ServiceHostEntry[]> ResolveServiceAsync(this IDnsQuery query, string service)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (string.IsNullOrWhiteSpace(service))
			{
				throw new ArgumentNullException("service");
			}
			return ResolveServiceProcessResult(await query.QueryAsync(service, QueryType.SRV).ConfigureAwait(continueOnCapturedContext: false));
		}

		public static string ConcatServiceName(string baseDomain, string serviceName, string tag = null)
		{
			if (!string.IsNullOrWhiteSpace(tag))
			{
				return "_" + serviceName + "._" + tag + "." + baseDomain + ".";
			}
			return serviceName + "." + baseDomain + ".";
		}

		public static ServiceHostEntry[] ResolveServiceProcessResult(IDnsQueryResponse result)
		{
			List<ServiceHostEntry> list = new List<ServiceHostEntry>();
			if (result == null || result.HasError)
			{
				return list.ToArray();
			}
			foreach (SrvRecord entry in result.Answers.SrvRecords())
			{
				IEnumerable<IPAddress> source = from p in result.Additionals.OfType<AddressRecord>()
					where p.DomainName.Equals(entry.Target)
					select p.Address;
				DnsString dnsString = (from p in result.Additionals.OfType<CNameRecord>()
					where p.DomainName.Equals(entry.Target)
					select p.CanonicalName).FirstOrDefault() ?? entry.Target;
				list.Add(new ServiceHostEntry
				{
					AddressList = source.ToArray(),
					HostName = dnsString,
					Port = entry.Port,
					Priority = entry.Priority,
					Weight = entry.Weight
				});
			}
			return list.ToArray();
		}
	}
	public class ServiceHostEntry : IPHostEntry
	{
		public int Port { get; set; }

		public int Priority { get; set; }

		public int Weight { get; set; }
	}
	public class DnsQueryOptions
	{
		public const int MinimumBufferSize = 512;

		public const int MaximumBufferSize = 4096;

		private static readonly TimeSpan s_defaultTimeout = TimeSpan.FromSeconds(5.0);

		private static readonly TimeSpan s_infiniteTimeout = System.Threading.Timeout.InfiniteTimeSpan;

		private static readonly TimeSpan s_maxTimeout = TimeSpan.FromMilliseconds(2147483647.0);

		private TimeSpan _timeout = s_defaultTimeout;

		private int _ednsBufferSize = 4096;

		private TimeSpan _failedResultsCacheDuration = s_defaultTimeout;

		public bool EnableAuditTrail { get; set; }

		public bool UseCache { get; set; } = true;

		public bool Recursion { get; set; } = true;

		public int Retries { get; set; } = 2;

		public bool ThrowDnsErrors { get; set; }

		public bool UseRandomNameServer { get; set; } = true;

		public bool ContinueOnDnsError { get; set; } = true;

		public bool ContinueOnEmptyResponse { get; set; } = true;

		public TimeSpan Timeout
		{
			get
			{
				return _timeout;
			}
			set
			{
				if ((value <= TimeSpan.Zero || value > s_maxTimeout) && value != s_infiniteTimeout)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_timeout = value;
			}
		}

		public bool UseTcpFallback { get; set; } = true;

		public bool UseTcpOnly { get; set; }

		public int ExtendedDnsBufferSize
		{
			get
			{
				return _ednsBufferSize;
			}
			set
			{
				_ednsBufferSize = ((value < 512) ? 512 : ((value > 4096) ? 4096 : value));
			}
		}

		public bool RequestDnsSecRecords { get; set; }

		public bool CacheFailedResults { get; set; }

		public TimeSpan FailedResultsCacheDuration
		{
			get
			{
				return _failedResultsCacheDuration;
			}
			set
			{
				if ((value <= TimeSpan.Zero || value > s_maxTimeout) && value != s_infiniteTimeout)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_failedResultsCacheDuration = value;
			}
		}

		public static implicit operator DnsQuerySettings(DnsQueryOptions fromOptions)
		{
			if (fromOptions == null)
			{
				return null;
			}
			return new DnsQuerySettings(fromOptions);
		}
	}
	public class DnsQueryAndServerOptions : DnsQueryOptions
	{
		public IReadOnlyList<NameServer> NameServers { get; } = new NameServer[0];

		public DnsQueryAndServerOptions()
		{
		}

		public DnsQueryAndServerOptions(params NameServer[] nameServers)
		{
			if (nameServers != null && nameServers.Length != 0)
			{
				NameServers = nameServers.ToList();
				return;
			}
			throw new ArgumentNullException("nameServers");
		}

		public DnsQueryAndServerOptions(params IPEndPoint[] nameServers)
			: this(nameServers?.Select((Func<IPEndPoint, NameServer>)((IPEndPoint p) => p)).ToArray())
		{
		}

		public DnsQueryAndServerOptions(params IPAddress[] nameServers)
			: this(nameServers?.Select((Func<IPAddress, NameServer>)((IPAddress p) => p)).ToArray())
		{
		}

		public static implicit operator DnsQueryAndServerSettings(DnsQueryAndServerOptions fromOptions)
		{
			if (fromOptions == null)
			{
				return null;
			}
			return new DnsQueryAndServerSettings(fromOptions);
		}
	}
	public class LookupClientOptions : DnsQueryAndServerOptions
	{
		private static readonly TimeSpan s_infiniteTimeout = System.Threading.Timeout.InfiniteTimeSpan;

		private static readonly TimeSpan s_maxTimeout = TimeSpan.FromMilliseconds(2147483647.0);

		private TimeSpan? _minimumCacheTimeout;

		private TimeSpan? _maximumCacheTimeout;

		public bool AutoResolveNameServers { get; set; } = true;

		public TimeSpan? MinimumCacheTimeout
		{
			get
			{
				return _minimumCacheTimeout;
			}
			set
			{
				if (value.HasValue && (value < TimeSpan.Zero || value > s_maxTimeout) && value != s_infiniteTimeout)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value == TimeSpan.Zero)
				{
					_minimumCacheTimeout = null;
				}
				else
				{
					_minimumCacheTimeout = value;
				}
			}
		}

		public TimeSpan? MaximumCacheTimeout
		{
			get
			{
				return _maximumCacheTimeout;
			}
			set
			{
				if (value.HasValue && (value < TimeSpan.Zero || value > s_maxTimeout) && value != s_infiniteTimeout)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value == TimeSpan.Zero)
				{
					_maximumCacheTimeout = null;
				}
				else
				{
					_maximumCacheTimeout = value;
				}
			}
		}

		public LookupClientOptions()
		{
		}

		public LookupClientOptions(params NameServer[] nameServers)
			: base(nameServers)
		{
			AutoResolveNameServers = false;
		}

		public LookupClientOptions(params IPEndPoint[] nameServers)
			: base(nameServers)
		{
			AutoResolveNameServers = false;
		}

		public LookupClientOptions(params IPAddress[] nameServers)
			: base(nameServers)
		{
			AutoResolveNameServers = false;
		}
	}
	public class DnsQuerySettings : IEquatable<DnsQuerySettings>
	{
		public bool EnableAuditTrail { get; }

		public bool UseCache { get; }

		public bool Recursion { get; }

		public int Retries { get; }

		public bool ThrowDnsErrors { get; }

		public bool UseRandomNameServer { get; }

		public bool ContinueOnDnsError { get; }

		public bool ContinueOnEmptyResponse { get; }

		public TimeSpan Timeout { get; }

		public bool UseTcpFallback { get; }

		public bool UseTcpOnly { get; }

		public bool UseExtendedDns
		{
			get
			{
				if (ExtendedDnsBufferSize <= 512)
				{
					return RequestDnsSecRecords;
				}
				return true;
			}
		}

		public int ExtendedDnsBufferSize { get; }

		public bool RequestDnsSecRecords { get; }

		public bool CacheFailedResults { get; }

		public TimeSpan FailedResultsCacheDuration { get; }

		public DnsQuerySettings(DnsQueryOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			ContinueOnDnsError = options.ContinueOnDnsError;
			ContinueOnEmptyResponse = options.ContinueOnEmptyResponse;
			EnableAuditTrail = options.EnableAuditTrail;
			Recursion = options.Recursion;
			Retries = options.Retries;
			ThrowDnsErrors = options.ThrowDnsErrors;
			Timeout = options.Timeout;
			UseCache = options.UseCache;
			UseRandomNameServer = options.UseRandomNameServer;
			UseTcpFallback = options.UseTcpFallback;
			UseTcpOnly = options.UseTcpOnly;
			ExtendedDnsBufferSize = options.ExtendedDnsBufferSize;
			RequestDnsSecRecords = options.RequestDnsSecRecords;
			CacheFailedResults = options.CacheFailedResults;
			FailedResultsCacheDuration = options.FailedResultsCacheDuration;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			return Equals(obj as DnsQuerySettings);
		}

		public bool Equals(DnsQuerySettings other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (EnableAuditTrail == other.EnableAuditTrail && UseCache == other.UseCache && Recursion == other.Recursion && Retries == other.Retries && ThrowDnsErrors == other.ThrowDnsErrors && UseRandomNameServer == other.UseRandomNameServer && ContinueOnDnsError == other.ContinueOnDnsError && ContinueOnEmptyResponse == other.ContinueOnEmptyResponse && Timeout.Equals(other.Timeout) && UseTcpFallback == other.UseTcpFallback && UseTcpOnly == other.UseTcpOnly && ExtendedDnsBufferSize == other.ExtendedDnsBufferSize && RequestDnsSecRecords == other.RequestDnsSecRecords && CacheFailedResults == other.CacheFailedResults)
			{
				return FailedResultsCacheDuration.Equals(other.FailedResultsCacheDuration);
			}
			return false;
		}
	}
	public class DnsQueryAndServerSettings : DnsQuerySettings, IEquatable<DnsQueryAndServerSettings>
	{
		private readonly NameServer[] _endpoints;

		private readonly Random _rnd = new Random();

		public IReadOnlyList<NameServer> NameServers => _endpoints;

		public DnsQueryAndServerSettings(DnsQueryAndServerOptions options)
			: base(options)
		{
			_endpoints = options.NameServers?.ToArray() ?? new NameServer[0];
		}

		public DnsQueryAndServerSettings(DnsQueryAndServerOptions options, IReadOnlyCollection<NameServer> overrideServers)
			: this(options)
		{
			_endpoints = overrideServers?.ToArray() ?? throw new ArgumentNullException("overrideServers");
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			return Equals(obj as DnsQueryAndServerSettings);
		}

		public bool Equals(DnsQueryAndServerSettings other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (NameServers.SequenceEqual(other.NameServers))
			{
				return Equals((DnsQuerySettings)other);
			}
			return false;
		}

		internal IReadOnlyList<NameServer> ShuffleNameServers()
		{
			if (_endpoints.Length > 1 && base.UseRandomNameServer)
			{
				NameServer[] array = _endpoints.ToArray();
				for (int num = array.Length; num > 0; num--)
				{
					int num2 = _rnd.Next(0, num);
					NameServer nameServer = array[num2];
					array[num2] = array[num - 1];
					array[num - 1] = nameServer;
				}
				return array;
			}
			return NameServers;
		}
	}
	public class LookupClientSettings : DnsQueryAndServerSettings, IEquatable<LookupClientSettings>
	{
		public TimeSpan? MinimumCacheTimeout { get; }

		public TimeSpan? MaximumCacheTimeout { get; }

		public LookupClientSettings(LookupClientOptions options)
			: base(options)
		{
			MinimumCacheTimeout = options.MinimumCacheTimeout;
			MaximumCacheTimeout = options.MaximumCacheTimeout;
		}

		internal LookupClientSettings(LookupClientOptions options, IReadOnlyCollection<NameServer> overrideServers)
			: base(options, overrideServers)
		{
			MinimumCacheTimeout = options.MinimumCacheTimeout;
			MaximumCacheTimeout = options.MaximumCacheTimeout;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			return Equals(obj as LookupClientSettings);
		}

		public bool Equals(LookupClientSettings other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (object.Equals(MinimumCacheTimeout, other.MinimumCacheTimeout) && object.Equals(MaximumCacheTimeout, other.MaximumCacheTimeout))
			{
				return Equals((DnsQueryAndServerSettings)other);
			}
			return false;
		}
	}
	internal class TruncatedQueryResponse : IDnsQueryResponse
	{
		public IReadOnlyList<DnsQuestion> Questions
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IReadOnlyList<DnsResourceRecord> Additionals
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IEnumerable<DnsResourceRecord> AllRecords
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IReadOnlyList<DnsResourceRecord> Answers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IReadOnlyList<DnsResourceRecord> Authorities
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string AuditTrail
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string ErrorMessage
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool HasError
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public DnsResponseHeader Header
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int MessageSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public NameServer NameServer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public DnsQuerySettings Settings
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
	public class DnsQueryResponse : IDnsQueryResponse
	{
		private int? _hashCode;

		public NameServer NameServer { get; }

		public IReadOnlyList<DnsResourceRecord> Additionals { get; }

		public IEnumerable<DnsResourceRecord> AllRecords => Answers.Concat(Additionals).Concat(Authorities);

		public string AuditTrail { get; private set; }

		public IReadOnlyList<DnsResourceRecord> Answers { get; }

		public IReadOnlyList<DnsResourceRecord> Authorities { get; }

		public string ErrorMessage => DnsResponseCodeText.GetErrorText((DnsResponseCode)Header.ResponseCode);

		public bool HasError
		{
			get
			{
				DnsResponseHeader header = Header;
				if (header == null)
				{
					return true;
				}
				return header.ResponseCode != DnsHeaderResponseCode.NoError;
			}
		}

		public DnsResponseHeader Header { get; }

		public IReadOnlyList<DnsQuestion> Questions { get; }

		public int MessageSize { get; }

		public DnsQuerySettings Settings { get; }

		internal DnsQueryResponse(DnsResponseMessage dnsResponseMessage, NameServer nameServer, DnsQuerySettings settings)
		{
			if (dnsResponseMessage == null)
			{
				throw new ArgumentNullException("dnsResponseMessage");
			}
			Header = dnsResponseMessage.Header;
			MessageSize = dnsResponseMessage.MessageSize;
			Questions = dnsResponseMessage.Questions.ToArray();
			Answers = dnsResponseMessage.Answers.ToArray();
			Additionals = dnsResponseMessage.Additionals.ToArray();
			Authorities = dnsResponseMessage.Authorities.ToArray();
			NameServer = nameServer ?? throw new ArgumentNullException("nameServer");
			Settings = settings;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (!(obj is DnsQueryResponse dnsQueryResponse))
			{
				return false;
			}
			if (Header.ToString().Equals(dnsQueryResponse.Header.ToString(), StringComparison.OrdinalIgnoreCase) && string.Join("", Questions).Equals(string.Join("", dnsQueryResponse.Questions), StringComparison.OrdinalIgnoreCase))
			{
				return string.Join("", AllRecords).Equals(string.Join("", dnsQueryResponse.AllRecords), StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		public override int GetHashCode()
		{
			if (!_hashCode.HasValue)
			{
				string text = Header.ToString() + string.Join("", Questions) + string.Join("", AllRecords);
				_hashCode = text.GetHashCode(StringComparison.Ordinal);
			}
			return _hashCode.Value;
		}

		internal static void SetAuditTrail(IDnsQueryResponse response, string value)
		{
			if (response is DnsQueryResponse dnsQueryResponse)
			{
				dnsQueryResponse.AuditTrail = value;
			}
		}
	}
	public class DnsQuestion
	{
		public DnsString QueryName { get; }

		public QueryClass QuestionClass { get; }

		public QueryType QuestionType { get; }

		public DnsQuestion(string query, QueryType questionType, QueryClass questionClass = QueryClass.IN)
			: this(DnsString.Parse(query), questionType, questionClass)
		{
		}

		public DnsQuestion(DnsString query, QueryType questionType, QueryClass questionClass = QueryClass.IN)
		{
			QueryName = query ?? throw new ArgumentNullException("query");
			QuestionType = questionType;
			QuestionClass = questionClass;
		}

		public override string ToString()
		{
			return ToString(0);
		}

		public string ToString(int offset = -32)
		{
			string text = ((offset == 0) ? string.Empty : "\t");
			return string.Format("{0," + offset + "} {1}{2} {1}{3}", QueryName.Original, text, QuestionClass, QuestionType);
		}
	}
	internal class DnsRecordFactory
	{
		private readonly DnsDatagramReader _reader;

		public DnsRecordFactory(DnsDatagramReader reader)
		{
			_reader = reader ?? throw new ArgumentNullException("reader");
		}

		public ResourceRecordInfo ReadRecordInfo()
		{
			return new ResourceRecordInfo(_reader.ReadQuestionQueryString(), (ResourceRecordType)_reader.ReadUInt16NetworkOrder(), (QueryClass)_reader.ReadUInt16NetworkOrder(), (int)_reader.ReadUInt32NetworkOrder(), _reader.ReadUInt16NetworkOrder());
		}

		public DnsResourceRecord GetRecord(ResourceRecordInfo info)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			int index = _reader.Index;
			DnsResourceRecord result = info.RecordType switch
			{
				ResourceRecordType.A => new ARecord(info, _reader.ReadIPAddress()), 
				ResourceRecordType.NS => new NsRecord(info, _reader.ReadDnsName()), 
				ResourceRecordType.CNAME => new CNameRecord(info, _reader.ReadDnsName()), 
				ResourceRecordType.SOA => ResolveSoaRecord(info), 
				ResourceRecordType.MB => new MbRecord(info, _reader.ReadDnsName()), 
				ResourceRecordType.MG => new MgRecord(info, _reader.ReadDnsName()), 
				ResourceRecordType.MR => new MrRecord(info, _reader.ReadDnsName()), 
				ResourceRecordType.NULL => new NullRecord(info, _reader.ReadBytes(info.RawDataLength).ToArray()), 
				ResourceRecordType.WKS => ResolveWksRecord(info), 
				ResourceRecordType.PTR => new PtrRecord(info, _reader.ReadDnsName()), 
				ResourceRecordType.HINFO => new HInfoRecord(info, _reader.ReadStringWithLengthPrefix(), _reader.ReadStringWithLengthPrefix()), 
				ResourceRecordType.MINFO => new MInfoRecord(info, _reader.ReadDnsName(), _reader.ReadDnsName()), 
				ResourceRecordType.MX => ResolveMXRecord(info), 
				ResourceRecordType.TXT => ResolveTxtRecord(info), 
				ResourceRecordType.RP => new RpRecord(info, _reader.ReadDnsName(), _reader.ReadDnsName()), 
				ResourceRecordType.AFSDB => new AfsDbRecord(info, (AfsType)_reader.ReadUInt16NetworkOrder(), _reader.ReadDnsName()), 
				ResourceRecordType.AAAA => new AaaaRecord(info, _reader.ReadIPv6Address()), 
				ResourceRecordType.SRV => ResolveSrvRecord(info), 
				ResourceRecordType.NAPTR => ResolveNaptrRecord(info), 
				ResourceRecordType.OPT => ResolveOptRecord(info), 
				ResourceRecordType.DS => ResolveDsRecord(info), 
				ResourceRecordType.SSHFP => ResolveSshfpRecord(info), 
				ResourceRecordType.RRSIG => ResolveRRSigRecord(info), 
				ResourceRecordType.NSEC => ResolveNSecRecord(info), 
				ResourceRecordType.DNSKEY => ResolveDnsKeyRecord(info), 
				ResourceRecordType.NSEC3 => ResolveNSec3Record(info), 
				ResourceRecordType.NSEC3PARAM => ResolveNSec3ParamRecord(info), 
				ResourceRecordType.TLSA => ResolveTlsaRecord(info), 
				ResourceRecordType.SPF => ResolveTxtRecord(info), 
				ResourceRecordType.URI => ResolveUriRecord(info), 
				ResourceRecordType.CAA => ResolveCaaRecord(info), 
				_ => new UnknownRecord(info, _reader.ReadBytes(info.RawDataLength).ToArray()), 
			};
			_reader.SanitizeResult(index + info.RawDataLength, info.RawDataLength);
			return result;
		}

		private DnsResourceRecord ResolveSoaRecord(ResourceRecordInfo info)
		{
			DnsString mName = _reader.ReadDnsName();
			DnsString rName = _reader.ReadDnsName();
			uint serial = _reader.ReadUInt32NetworkOrder();
			uint refresh = _reader.ReadUInt32NetworkOrder();
			uint retry = _reader.ReadUInt32NetworkOrder();
			uint expire = _reader.ReadUInt32NetworkOrder();
			uint minimum = _reader.ReadUInt32NetworkOrder();
			return new SoaRecord(info, mName, rName, serial, refresh, retry, expire, minimum);
		}

		private DnsResourceRecord ResolveWksRecord(ResourceRecordInfo info)
		{
			IPAddress address = _reader.ReadIPAddress();
			byte protocol = _reader.ReadByte();
			return new WksRecord(info, address, protocol, _reader.ReadBytes(info.RawDataLength - 5).ToArray());
		}

		private DnsResourceRecord ResolveMXRecord(ResourceRecordInfo info)
		{
			ushort preference = _reader.ReadUInt16NetworkOrder();
			DnsString domainName = _reader.ReadDnsName();
			return new MxRecord(info, preference, domainName);
		}

		private DnsResourceRecord ResolveTxtRecord(ResourceRecordInfo info)
		{
			int index = _reader.Index;
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			while (_reader.Index - index < info.RawDataLength)
			{
				byte length = _reader.ReadByte();
				ArraySegment<byte> data = _reader.ReadBytes(length);
				string item = DnsDatagramReader.ParseString(data);
				string item2 = DnsDatagramReader.ReadUTF8String(data);
				list.Add(item);
				list2.Add(item2);
			}
			return new TxtRecord(info, list.ToArray(), list2.ToArray());
		}

		private DnsResourceRecord ResolveSrvRecord(ResourceRecordInfo info)
		{
			ushort priority = _reader.ReadUInt16NetworkOrder();
			ushort weight = _reader.ReadUInt16NetworkOrder();
			ushort port = _reader.ReadUInt16NetworkOrder();
			DnsString target = _reader.ReadDnsName();
			return new SrvRecord(info, priority, weight, port, target);
		}

		private DnsResourceRecord ResolveNaptrRecord(ResourceRecordInfo info)
		{
			ushort order = _reader.ReadUInt16NetworkOrder();
			ushort preference = _reader.ReadUInt16NetworkOrder();
			string flags = _reader.ReadStringWithLengthPrefix();
			string services = _reader.ReadStringWithLengthPrefix();
			string regexp = _reader.ReadStringWithLengthPrefix();
			DnsString replacement = _reader.ReadDnsName();
			return new NAPtrRecord(info, order, preference, flags, services, regexp, replacement);
		}

		private DnsResourceRecord ResolveOptRecord(ResourceRecordInfo info)
		{
			byte[] data = _reader.ReadBytes(info.RawDataLength).ToArray();
			return new OptRecord((int)info.RecordClass, info.InitialTimeToLive, info.RawDataLength, data);
		}

		private DnsResourceRecord ResolveDsRecord(ResourceRecordInfo info)
		{
			int index = _reader.Index;
			ushort keyTag = _reader.ReadUInt16NetworkOrder();
			byte algorithm = _reader.ReadByte();
			byte digestType = _reader.ReadByte();
			byte[] digest = _reader.ReadBytesToEnd(index, info.RawDataLength).ToArray();
			return new DsRecord(info, keyTag, algorithm, digestType, digest);
		}

		private DnsResourceRecord ResolveSshfpRecord(ResourceRecordInfo info)
		{
			SshfpAlgorithm algorithm = (SshfpAlgorithm)_reader.ReadByte();
			SshfpFingerprintType fingerprintType = (SshfpFingerprintType)_reader.ReadByte();
			byte[] source = _reader.ReadBytes(info.RawDataLength - 2).ToArray();
			string fingerprint = string.Join(string.Empty, source.Select((byte b) => b.ToString("X2")));
			return new SshfpRecord(info, algorithm, fingerprintType, fingerprint);
		}

		private DnsResourceRecord ResolveRRSigRecord(ResourceRecordInfo info)
		{
			int index = _reader.Index;
			ushort coveredType = _reader.ReadUInt16NetworkOrder();
			byte algorithm = _reader.ReadByte();
			byte labels = _reader.ReadByte();
			uint num = _reader.ReadUInt32NetworkOrder();
			uint num2 = _reader.ReadUInt32NetworkOrder();
			uint num3 = _reader.ReadUInt32NetworkOrder();
			ushort keyTag = _reader.ReadUInt16NetworkOrder();
			DnsString signersName = _reader.ReadDnsName();
			byte[] signature = _reader.ReadBytesToEnd(index, info.RawDataLength).ToArray();
			return new RRSigRecord(info, coveredType, algorithm, labels, num, num2, num3, keyTag, signersName, signature);
		}

		private DnsResourceRecord ResolveNSecRecord(ResourceRecordInfo info)
		{
			int index = _reader.Index;
			DnsString nextDomainName = _reader.ReadDnsName();
			byte[] typeBitMaps = _reader.ReadBytesToEnd(index, info.RawDataLength).ToArray();
			return new NSecRecord(info, nextDomainName, typeBitMaps);
		}

		private DnsResourceRecord ResolveNSec3Record(ResourceRecordInfo info)
		{
			int index = _reader.Index;
			byte hashAlgorithm = _reader.ReadByte();
			byte flags = _reader.ReadByte();
			ushort iterations = _reader.ReadUInt16NetworkOrder();
			byte length = _reader.ReadByte();
			byte[] salt = _reader.ReadBytes(length).ToArray();
			byte length2 = _reader.ReadByte();
			byte[] nextOwnersName = _reader.ReadBytes(length2).ToArray();
			byte[] bitmap = _reader.ReadBytesToEnd(index, info.RawDataLength).ToArray();
			return new NSec3Record(info, hashAlgorithm, flags, iterations, salt, nextOwnersName, bitmap);
		}

		private DnsResourceRecord ResolveNSec3ParamRecord(ResourceRecordInfo info)
		{
			byte hashAlgorithm = _reader.ReadByte();
			byte flags = _reader.ReadByte();
			ushort iterations = _reader.ReadUInt16NetworkOrder();
			byte length = _reader.ReadByte();
			byte[] salt = _reader.ReadBytes(length).ToArray();
			return new NSec3ParamRecord(info, hashAlgorithm, flags, iterations, salt);
		}

		private DnsResourceRecord ResolveDnsKeyRecord(ResourceRecordInfo info)
		{
			int index = _reader.Index;
			int flags = _reader.ReadUInt16NetworkOrder();
			byte protocol = _reader.ReadByte();
			byte algorithm = _reader.ReadByte();
			byte[] publicKey = _reader.ReadBytesToEnd(index, info.RawDataLength).ToArray();
			return new DnsKeyRecord(info, flags, protocol, algorithm, publicKey);
		}

		private DnsResourceRecord ResolveTlsaRecord(ResourceRecordInfo info)
		{
			int index = _reader.Index;
			byte certificateUsage = _reader.ReadByte();
			byte selector = _reader.ReadByte();
			byte matchingType = _reader.ReadByte();
			byte[] certificateAssociationData = _reader.ReadBytesToEnd(index, info.RawDataLength).ToArray();
			return new TlsaRecord(info, certificateUsage, selector, matchingType, certificateAssociationData);
		}

		private DnsResourceRecord ResolveUriRecord(ResourceRecordInfo info)
		{
			ushort priority = _reader.ReadUInt16NetworkOrder();
			ushort weight = _reader.ReadUInt16NetworkOrder();
			string target = _reader.ReadString(info.RawDataLength - 4);
			return new UriRecord(info, priority, weight, target);
		}

		private DnsResourceRecord ResolveCaaRecord(ResourceRecordInfo info)
		{
			byte flags = _reader.ReadByte();
			string text = _reader.ReadStringWithLengthPrefix();
			string value = DnsDatagramReader.ParseString(_reader, info.RawDataLength - 2 - text.Length);
			return new CaaRecord(info, flags, text, value);
		}
	}
	internal class DnsRequestHeader
	{
		private static readonly Random s_random = new Random();

		public const int HeaderLength = 12;

		private ushort _flags;

		public ushort RawFlags => _flags;

		internal DnsHeaderFlag HeaderFlags
		{
			get
			{
				return (DnsHeaderFlag)_flags;
			}
			set
			{
				_flags &= 65519;
				_flags &= 65503;
				_flags &= 65471;
				_flags &= 32767;
				_flags &= 64511;
				_flags &= 65023;
				_flags &= 65279;
				_flags &= 65407;
				_flags |= (ushort)value;
			}
		}

		public int Id { get; private set; }

		public DnsOpCode OpCode
		{
			get
			{
				return (DnsOpCode)((DnsHeader.OPCodeMask & _flags) >> (int)DnsHeader.OPCodeShift);
			}
			set
			{
				_flags &= (ushort)(~DnsHeader.OPCodeMask);
				_flags |= (ushort)(((ushort)value << (int)DnsHeader.OPCodeShift) & DnsHeader.OPCodeMask);
			}
		}

		public ushort RCode
		{
			get
			{
				return (ushort)(DnsHeader.RCodeMask & _flags);
			}
			set
			{
				_flags &= (ushort)(~DnsHeader.RCodeMask);
				_flags |= (ushort)(value & DnsHeader.RCodeMask);
			}
		}

		public bool UseRecursion
		{
			get
			{
				return HeaderFlags.HasFlag(DnsHeaderFlag.RecursionDesired);
			}
			set
			{
				if (value)
				{
					_flags |= 256;
				}
				else
				{
					_flags &= 65279;
				}
			}
		}

		public DnsRequestHeader(DnsOpCode queryKind)
			: this(useRecursion: true, queryKind)
		{
		}

		public DnsRequestHeader(bool useRecursion, DnsOpCode queryKind)
		{
			Id = GetNextUniqueId();
			OpCode = queryKind;
			UseRecursion = useRecursion;
		}

		public override string ToString()
		{
			return $"{Id} - Qs: {1} Recursion: {UseRecursion} OpCode: {OpCode}";
		}

		public void RefreshId()
		{
			Id = GetNextUniqueId();
		}

		private static ushort GetNextUniqueId()
		{
			return (ushort)s_random.Next(1, 65535);
		}
	}
	[DebuggerDisplay("Request:{Header} => {Question}")]
	internal class DnsRequestMessage
	{
		public DnsRequestHeader Header { get; }

		public DnsQuestion Question { get; }

		public DnsQuerySettings QuerySettings { get; }

		public DnsRequestMessage(DnsRequestHeader header, DnsQuestion question, DnsQuerySettings dnsQuerySettings = null)
		{
			Header = header ?? throw new ArgumentNullException("header");
			Question = question ?? throw new ArgumentNullException("question");
			QuerySettings = dnsQuerySettings ?? new DnsQuerySettings(new DnsQueryOptions());
		}

		public override string ToString()
		{
			return $"{Header} => {Question}";
		}
	}
	public enum DnsHeaderResponseCode : short
	{
		NoError,
		FormatError,
		ServerFailure,
		NotExistentDomain,
		NotImplemented,
		Refused,
		ExistingDomain,
		ExistingResourceRecordSet,
		MissingResourceRecordSet,
		NotAuthorized,
		NotZone,
		Unassinged11,
		Unassinged12,
		Unassinged13,
		Unassinged14,
		Unassinged15
	}
	public enum DnsResponseCode
	{
		NoError = 0,
		FormatError = 1,
		ServerFailure = 2,
		NotExistentDomain = 3,
		NotImplemented = 4,
		Refused = 5,
		ExistingDomain = 6,
		ExistingResourceRecordSet = 7,
		MissingResourceRecordSet = 8,
		NotAuthorized = 9,
		NotZone = 10,
		Unassinged11 = 11,
		Unassinged12 = 12,
		Unassinged13 = 13,
		Unassinged14 = 14,
		Unassinged15 = 15,
		BadVersionOrBadSignature = 16,
		BadKey = 17,
		BadTime = 18,
		BadMode = 19,
		BadName = 20,
		BadAlgorithm = 21,
		BadTruncation = 22,
		BadCookie = 23,
		Unassigned = 666,
		ConnectionTimeout = 999
	}
	[Serializable]
	public class DnsResponseException : Exception
	{
		public DnsResponseCode Code { get; }

		public string AuditTrail { get; internal set; }

		public string DnsError { get; }

		public DnsResponseException()
			: base("Unknown Error")
		{
			Code = DnsResponseCode.Unassigned;
			DnsError = DnsResponseCodeText.GetErrorText(Code);
		}

		public DnsResponseException(string message)
			: base(message)
		{
			Code = DnsResponseCode.Unassigned;
			DnsError = DnsResponseCodeText.GetErrorText(Code);
		}

		public DnsResponseException(DnsResponseCode code)
			: base(DnsResponseCodeText.GetErrorText(code))
		{
			Code = code;
			DnsError = DnsResponseCodeText.GetErrorText(Code);
		}

		public DnsResponseException(string message, Exception innerException)
			: base(message, innerException)
		{
			Code = DnsResponseCode.Unassigned;
			DnsError = DnsResponseCodeText.GetErrorText(Code);
		}

		public DnsResponseException(DnsResponseCode code, string message)
			: base(message)
		{
			Code = code;
			DnsError = DnsResponseCodeText.GetErrorText(Code);
		}

		public DnsResponseException(DnsResponseCode code, string message, Exception innerException)
			: base(message, innerException)
		{
			Code = code;
			DnsError = DnsResponseCodeText.GetErrorText(Code);
		}
	}
	internal static class DnsResponseCodeText
	{
		internal const string BADALG = "Algorithm not supported";

		internal const string BADCOOKIE = "Bad/missing Server Cookie";

		internal const string BADKEY = "Key not recognized";

		internal const string BADMODE = "Bad TKEY Mode";

		internal const string BADNAME = "Duplicate key name";

		internal const string BADSIG = "TSIG Signature Failure";

		internal const string BADTIME = "Signature out of time window";

		internal const string BADTRUNC = "Bad Truncation";

		internal const string BADVERS = "Bad OPT Version";

		internal const string FormErr = "Format Error";

		internal const string NoError = "No Error";

		internal const string NotAuth = "Server Not Authoritative for zone or Not Authorized";

		internal const string NotImp = "Not Implemented";

		internal const string NotZone = "Name not contained in zone";

		internal const string NXDomain = "Non-Existent Domain";

		internal const string NXRRSet = "RR Set that should exist does not";

		internal const string Refused = "Query Refused";

		internal const string ServFail = "Server Failure";

		internal const string Unassigned = "Unknown Error";

		internal const string YXDomain = "Name Exists when it should not";

		internal const string YXRRSet = "RR Set Exists when it should not";

		private static readonly Dictionary<DnsResponseCode, string> s_errors = new Dictionary<DnsResponseCode, string>
		{
			{
				DnsResponseCode.NoError,
				"No Error"
			},
			{
				DnsResponseCode.FormatError,
				"Format Error"
			},
			{
				DnsResponseCode.ServerFailure,
				"Server Failure"
			},
			{
				DnsResponseCode.NotExistentDomain,
				"Non-Existent Domain"
			},
			{
				DnsResponseCode.NotImplemented,
				"Not Implemented"
			},
			{
				DnsResponseCode.Refused,
				"Query Refused"
			},
			{
				DnsResponseCode.ExistingDomain,
				"Name Exists when it should not"
			},
			{
				DnsResponseCode.ExistingResourceRecordSet,
				"RR Set Exists when it should not"
			},
			{
				DnsResponseCode.MissingResourceRecordSet,
				"RR Set that should exist does not"
			},
			{
				DnsResponseCode.NotAuthorized,
				"Server Not Authoritative for zone or Not Authorized"
			},
			{
				DnsResponseCode.NotZone,
				"Name not contained in zone"
			},
			{
				DnsResponseCode.BadVersionOrBadSignature,
				"Bad OPT Version"
			},
			{
				DnsResponseCode.BadKey,
				"Key not recognized"
			},
			{
				DnsResponseCode.BadTime,
				"Signature out of time window"
			},
			{
				DnsResponseCode.BadMode,
				"Bad TKEY Mode"
			},
			{
				DnsResponseCode.BadName,
				"Duplicate key name"
			},
			{
				DnsResponseCode.BadAlgorithm,
				"Algorithm not supported"
			},
			{
				DnsResponseCode.BadTruncation,
				"Bad Truncation"
			},
			{
				DnsResponseCode.BadCookie,
				"Bad/missing Server Cookie"
			}
		};

		public static string GetErrorText(DnsResponseCode code)
		{
			if (!s_errors.ContainsKey(code))
			{
				return "Unknown Error";
			}
			return s_errors[code];
		}
	}
	public class DnsResponseHeader
	{
		private readonly ushort _flags;

		public int AdditionalCount { get; }

		public int AnswerCount { get; }

		public bool FutureUse => HasFlag(DnsHeaderFlag.FutureUse);

		public bool HasAuthorityAnswer => HasFlag(DnsHeaderFlag.HasAuthorityAnswer);

		internal DnsHeaderFlag HeaderFlags => (DnsHeaderFlag)_flags;

		public int Id { get; }

		public bool IsAuthenticData => HasFlag(DnsHeaderFlag.IsAuthenticData);

		public bool IsCheckingDisabled => HasFlag(DnsHeaderFlag.IsCheckingDisabled);

		public bool HasQuery => HasFlag(DnsHeaderFlag.HasQuery);

		public int NameServerCount { get; }

		public DnsOpCode OPCode => (DnsOpCode)((DnsHeader.OPCodeMask & _flags) >> (int)DnsHeader.OPCodeShift);

		public int QuestionCount { get; }

		public bool RecursionAvailable => HasFlag(DnsHeaderFlag.RecursionAvailable);

		public DnsHeaderResponseCode ResponseCode => (DnsHeaderResponseCode)(_flags & DnsHeader.RCodeMask);

		public bool ResultTruncated => HasFlag(DnsHeaderFlag.ResultTruncated);

		public bool RecursionDesired => HasFlag(DnsHeaderFlag.RecursionDesired);

		[CLSCompliant(false)]
		public DnsResponseHeader(int id, ushort flags, int questionCount, int answerCount, int additionalCount, int serverCount)
		{
			Id = id;
			_flags = flags;
			QuestionCount = questionCount;
			AnswerCount = answerCount;
			AdditionalCount = additionalCount;
			NameServerCount = serverCount;
		}

		private bool HasFlag(DnsHeaderFlag flag)
		{
			return (HeaderFlags & flag) != 0;
		}

		public override string ToString()
		{
			string arg = $";; ->>HEADER<<- opcode: {OPCode}, status: {DnsResponseCodeText.GetErrorText((DnsResponseCode)ResponseCode)}, id: {Id}";
			string[] source = new string[7]
			{
				HasQuery ? "qr" : "",
				HasAuthorityAnswer ? "aa" : "",
				RecursionDesired ? "rd" : "",
				RecursionAvailable ? "ra" : "",
				ResultTruncated ? "tc" : "",
				IsCheckingDisabled ? "cd" : "",
				IsAuthenticData ? "ad" : ""
			};
			string arg2 = string.Join(" ", source.Where((string p) => p != ""));
			return $"{arg}\r\n;; flags: {arg2}; QUERY: {QuestionCount}, " + $"ANSWER: {AnswerCount}, AUTHORITY: {NameServerCount}, ADDITIONAL: {AdditionalCount}";
		}
	}
	internal class DnsResponseMessage
	{
		public List<DnsResourceRecord> Additionals { get; } = new List<DnsResourceRecord>();

		public List<DnsResourceRecord> Answers { get; } = new List<DnsResourceRecord>();

		public List<DnsResourceRecord> Authorities { get; } = new List<DnsResourceRecord>();

		public DnsResponseHeader Header { get; }

		public int MessageSize { get; }

		public List<DnsQuestion> Questions { get; } = new List<DnsQuestion>();

		public DnsResponseMessage(DnsResponseHeader header, int messageSize)
		{
			Header = header ?? throw new ArgumentNullException("header");
			MessageSize = messageSize;
		}

		public void AddAdditional(DnsResourceRecord record)
		{
			if (record == null)
			{
				throw new ArgumentNullException("record");
			}
			Additionals.Add(record);
		}

		public void AddAnswer(DnsResourceRecord record)
		{
			if (record == null)
			{
				throw new ArgumentNullException("record");
			}
			Answers.Add(record);
		}

		public void AddAuthority(DnsResourceRecord record)
		{
			if (record == null)
			{
				throw new ArgumentNullException("record");
			}
			Authorities.Add(record);
		}

		public void AddQuestion(DnsQuestion question)
		{
			if (question == null)
			{
				throw new ArgumentNullException("question");
			}
			Questions.Add(question);
		}

		public DnsQueryResponse AsQueryResponse(NameServer nameServer, DnsQuerySettings settings)
		{
			return new DnsQueryResponse(this, nameServer, settings);
		}

		public static DnsResponseMessage Combine(List<DnsResponseMessage> messages)
		{
			if (messages.Count <= 1)
			{
				return messages.FirstOrDefault();
			}
			DnsResponseMessage dnsResponseMessage = messages[0];
			DnsResponseMessage dnsResponseMessage2 = new DnsResponseMessage(new DnsResponseHeader(dnsResponseMessage.Header.Id, (ushort)dnsResponseMessage.Header.HeaderFlags, dnsResponseMessage.Header.QuestionCount, messages.Sum((DnsResponseMessage p) => p.Header.AnswerCount), messages.Sum((DnsResponseMessage p) => p.Header.AdditionalCount), dnsResponseMessage.Header.NameServerCount), messages.Sum((DnsResponseMessage p) => p.MessageSize));
			dnsResponseMessage2.Questions.AddRange(dnsResponseMessage.Questions);
			dnsResponseMessage2.Additionals.AddRange(messages.SelectMany((DnsResponseMessage p) => p.Additionals));
			dnsResponseMessage2.Answers.AddRange(messages.SelectMany((DnsResponseMessage p) => p.Answers));
			dnsResponseMessage2.Authorities.AddRange(messages.SelectMany((DnsResponseMessage p) => p.Authorities));
			return dnsResponseMessage2;
		}
	}
	[Serializable]
	public class DnsResponseParseException : Exception
	{
		private static readonly Func<int, int, int, string, string, string> s_defaultMessage = (int dataLength, int index, int length, string message, string dataDump) => string.Format(CultureInfo.InvariantCulture, "Response parser error, {1} bytes available, tried to read {2} bytes at index {3}.{0}{4}{0}[{5}].", Environment.NewLine, dataLength, length, index, message, dataDump);

		public byte[] ResponseData { get; }

		public int Index { get; }

		public int ReadLength { get; }

		public DnsResponseParseException()
		{
		}

		public DnsResponseParseException(string message)
			: base(message)
		{
		}

		public DnsResponseParseException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public DnsResponseParseException(string message, byte[] data, int index = 0, int length = 0, Exception innerException = null)
			: this(s_defaultMessage(data.Length, index, length, message, FormatData(data, index, length)), innerException)
		{
			ResponseData = data ?? throw new ArgumentNullException("data");
			Index = index;
			ReadLength = length;
		}

		private static string FormatData(byte[] data, int index, int length)
		{
			if (data == null || data.Length == 0)
			{
				return string.Empty;
			}
			return Convert.ToBase64String(data.Take(index + length + 100).ToArray());
		}
	}
	public class DnsString
	{
		public const string ACEPrefix = "xn--";

		public const int LabelMaxLength = 63;

		public const int QueryMaxLength = 255;

		public static readonly DnsString RootLabel = new DnsString(".", ".");

		internal static readonly IdnMapping s_idn = new IdnMapping();

		internal const char Dot = '.';

		internal const string DotStr = ".";

		public string Original { get; }

		public string Value { get; }

		public int? NumberOfLabels { get; }

		internal DnsString(string original, string value, int? numLabels = null)
		{
			Original = original;
			Value = value;
			NumberOfLabels = numLabels;
		}

		public static implicit operator string(DnsString name)
		{
			return name?.Value;
		}

		public static DnsString operator +(DnsString a, DnsString b)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a");
			}
			if (b == null)
			{
				throw new ArgumentNullException("b");
			}
			string text = a.Value + ((b.Value.Length > 1) ? b.Value : string.Empty);
			return new DnsString(text, text);
		}

		public static DnsString operator +(DnsString a, string b)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a");
			}
			if (string.IsNullOrWhiteSpace(b))
			{
				throw new ArgumentException("'b' cannot be null or empty.", "b");
			}
			b = ((b[0] == '.') ? b.Substring(1) : b);
			DnsString dnsString = Parse(b);
			return a + dnsString;
		}

		public override string ToString()
		{
			return Value;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode(StringComparison.Ordinal);
		}

		public override bool Equals(object obj)
		{
			return obj?.ToString().Equals(Value, StringComparison.Ordinal) ?? false;
		}

		public static DnsString Parse(string query)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (query.Length > 1 && query[0] == '.')
			{
				throw new ArgumentException("'" + query + "' is not a legal name, found leading root label.", "query");
			}
			if (query.Length == 0 || (query.Length == 1 && query.Equals(".", StringComparison.Ordinal)))
			{
				return RootLabel;
			}
			foreach (char c in query)
			{
				if (c == '.')
				{
					if (num2 > 63)
					{
						throw new ArgumentException($"Label '{num3 + 1}' is longer than {63} bytes.", "query");
					}
					num3++;
					num2 = 0;
					continue;
				}
				num2++;
				num++;
				switch (c)
				{
				case '-':
				case '_':
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
				case 'g':
				case 'h':
				case 'i':
				case 'j':
				case 'k':
				case 'l':
				case 'm':
				case 'n':
				case 'o':
				case 'p':
				case 'q':
				case 'r':
				case 's':
				case 't':
				case 'u':
				case 'v':
				case 'w':
				case 'x':
				case 'y':
				case 'z':
					continue;
				}
				if ((c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))
				{
					continue;
				}
				try
				{
					string text = s_idn.GetAscii(query);
					if (text[text.Length - 1] != '.')
					{
						text += ".";
					}
					string[] array = text.Split(new char[1] { '.' }, StringSplitOptions.RemoveEmptyEntries);
					return new DnsString(query, text, array.Length);
				}
				catch (Exception innerException)
				{
					throw new ArgumentException("'" + query + "' is not a valid hostname.", "query", innerException);
				}
			}
			if (num2 > 0)
			{
				num3++;
				if (num2 > 63)
				{
					throw new ArgumentException($"Label '{num3}' is longer than {63} bytes.", "query");
				}
			}
			if (num + num3 + 1 > 255)
			{
				throw new ArgumentException($"Octet length of '{query}' exceeds maximum of {255} bytes.", "query");
			}
			if (query[query.Length - 1] != '.')
			{
				return new DnsString(query, query + ".", num3);
			}
			return new DnsString(query, query, num3);
		}

		public static DnsString FromResponseQueryString(string query)
		{
			string text = query;
			if (query.Length == 0 || query[query.Length - 1] != '.')
			{
				text += ".";
			}
			if (text.Contains("xn--", StringComparison.Ordinal))
			{
				string unicode = s_idn.GetUnicode(text);
				return new DnsString(query, unicode);
			}
			return new DnsString(query, text);
		}
	}
	internal class DnsTcpMessageHandler : DnsMessageHandler
	{
		private class ClientPool : IDisposable
		{
			public class ClientEntry
			{
				public TcpClient Client { get; }

				public IPEndPoint Endpoint { get; }

				public int StartMillis { get; set; } = Environment.TickCount & 0x7FFFFFFF;

				public int MaxLiveTime { get; set; } = 5000;

				public ClientEntry(TcpClient client, IPEndPoint endpoint)
				{
					Client = client;
					Endpoint = endpoint;
				}

				public void DisposeClient()
				{
					try
					{
						Client.Dispose();
					}
					catch
					{
					}
				}
			}

			private bool _disposedValue;

			private readonly bool _enablePool;

			private ConcurrentQueue<ClientEntry> _clients = new ConcurrentQueue<ClientEntry>();

			private readonly IPEndPoint _endpoint;

			public ClientPool(bool enablePool, IPEndPoint endpoint)
			{
				_enablePool = enablePool;
				_endpoint = endpoint;
			}

			public async Task<ClientEntry> GetNextClient()
			{
				if (_disposedValue)
				{
					throw new ObjectDisposedException("ClientPool");
				}
				ClientEntry entry = null;
				if (_enablePool)
				{
					while (entry == null && !TryDequeue(out entry))
					{
						entry = new ClientEntry(new TcpClient(_endpoint.AddressFamily)
						{
							LingerState = new LingerOption(enable: true, 0)
						}, _endpoint);
						await entry.Client.ConnectAsync(_endpoint.Address, _endpoint.Port).ConfigureAwait(continueOnCapturedContext: false);
					}
				}
				else
				{
					entry = new ClientEntry(new TcpClient(_endpoint.AddressFamily), _endpoint);
					await entry.Client.ConnectAsync(_endpoint.Address, _endpoint.Port).ConfigureAwait(continueOnCapturedContext: false);
				}
				return entry;
			}

			public void Enqueue(ClientEntry entry)
			{
				if (_disposedValue)
				{
					throw new ObjectDisposedException("ClientPool");
				}
				if (entry == null)
				{
					throw new ArgumentNullException("entry");
				}
				if (!entry.Client.Client.RemoteEndPoint.Equals(_endpoint))
				{
					throw new ArgumentException("Invalid endpoint.");
				}
				if (_enablePool && entry.Client.Connected && entry.StartMillis + entry.MaxLiveTime >= (Environment.TickCount & 0x7FFFFFFF))
				{
					_clients.Enqueue(entry);
				}
				else
				{
					entry.DisposeClient();
				}
			}

			public bool TryDequeue(out ClientEntry entry)
			{
				if (_disposedValue)
				{
					throw new ObjectDisposedException("ClientPool");
				}
				bool result;
				while ((result = _clients.TryDequeue(out entry)) && (!entry.Client.Connected || entry.StartMillis + entry.MaxLiveTime < (Environment.TickCount & 0x7FFFFFFF)))
				{
					entry.DisposeClient();
				}
				return result;
			}

			protected virtual void Dispose(bool disposing)
			{
				if (_disposedValue)
				{
					return;
				}
				if (disposing)
				{
					foreach (ClientEntry client in _clients)
					{
						client.DisposeClient();
					}
					_clients = new ConcurrentQueue<ClientEntry>();
				}
				_disposedValue = true;
			}

			public void Dispose()
			{
				Dispose(disposing: true);
			}
		}

		private readonly ConcurrentDictionary<IPEndPoint, ClientPool> _pools = new ConcurrentDictionary<IPEndPoint, ClientPool>();

		public override DnsMessageHandleType Type { get; } = DnsMessageHandleType.TCP;

		public override DnsResponseMessage Query(IPEndPoint endpoint, DnsRequestMessage request, TimeSpan timeout)
		{
			if (timeout.TotalMilliseconds != -1.0 && timeout.TotalMilliseconds < 2147483647.0)
			{
				using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(timeout))
				{
					return QueryAsync(endpoint, request, cancellationTokenSource.Token).WithCancellation(cancellationTokenSource.Token).ConfigureAwait(continueOnCapturedContext: false).GetAwaiter()
						.GetResult();
				}
			}
			return QueryAsync(endpoint, request, CancellationToken.None).ConfigureAwait(continueOnCapturedContext: false).GetAwaiter().GetResult();
		}

		public override async Task<DnsResponseMessage> QueryAsync(IPEndPoint server, DnsRequestMessage request, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ClientPool pool;
			while (!_pools.TryGetValue(server, out pool))
			{
				_pools.TryAdd(server, new ClientPool(enablePool: true, server));
			}
			ClientPool.ClientEntry entry = await pool.GetNextClient().ConfigureAwait(continueOnCapturedContext: false);
			using (cancellationToken.Register(delegate
			{
				if (entry != null)
				{
					entry.DisposeClient();
				}
			}))
			{
				_ = 1;
				try
				{
					DnsResponseMessage dnsResponseMessage = await QueryAsyncInternal(entry.Client, request, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					DnsMessageHandler.ValidateResponse(request, dnsResponseMessage);
					pool.Enqueue(entry);
					return dnsResponseMessage;
				}
				catch
				{
					entry.DisposeClient();
					throw;
				}
			}
		}

		private async Task<DnsResponseMessage> QueryAsyncInternal(TcpClient client, DnsRequestMessage request, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			NetworkStream stream = client.GetStream();
			using (PooledBytes memory = new PooledBytes(1026))
			{
				using DnsDatagramWriter writer = new DnsDatagramWriter(new ArraySegment<byte>(memory.Buffer, 2, memory.Buffer.Length - 2));
				GetRequestData(request, writer);
				int index = writer.Index;
				memory.Buffer[0] = (byte)((index >> 8) & 0xFF);
				memory.Buffer[1] = (byte)(index & 0xFF);
				await stream.WriteAsync(memory.Buffer, 0, index + 2, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				await stream.FlushAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			if (!stream.CanRead)
			{
				throw new TimeoutException();
			}
			cancellationToken.ThrowIfCancellationRequested();
			List<DnsResponseMessage> responses = new List<DnsResponseMessage>();
			int num6 = default(int);
			do
			{
				int length;
				using (PooledBytes memory = new PooledBytes(2))
				{
					int num = 0;
					while (true)
					{
						int num2 = num;
						int num3;
						if ((num = num2 + (num3 = await stream.ReadAsync(memory.Buffer, num, 2, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))) >= 2)
						{
							break;
						}
						if (num3 <= 0)
						{
							throw new TimeoutException();
						}
					}
					length = (memory.Buffer[0] << 8) | memory.Buffer[1];
				}
				if (length <= 0)
				{
					throw new TimeoutException();
				}
				using PooledBytes memory = new PooledBytes(length);
				int num4 = 0;
				int num2 = ((length > 4096) ? 4096 : length);
				while (true)
				{
					bool flag = !cancellationToken.IsCancellationRequested;
					if (flag)
					{
						int num5 = num4;
						flag = (num4 = num5 + (num6 = await stream.ReadAsync(memory.Buffer, num4, num2, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))) < length;
					}
					if (!flag)
					{
						break;
					}
					if (num6 <= 0)
					{
						throw new TimeoutException();
					}
					if (num4 + num2 > length)
					{
						num2 = length - num4;
						if (num2 <= 0)
						{
							break;
						}
					}
				}
				DnsResponseMessage responseMessage = GetResponseMessage(new ArraySegment<byte>(memory.Buffer, 0, num4));
				responses.Add(responseMessage);
			}
			while (stream.DataAvailable && !cancellationToken.IsCancellationRequested);
			return DnsResponseMessage.Combine(responses);
		}
	}
	internal class DnsUdpMessageHandler : DnsMessageHandler
	{
		private const int MaxSize = 4096;

		public override DnsMessageHandleType Type { get; } = DnsMessageHandleType.UDP;

		public override DnsResponseMessage Query(IPEndPoint server, DnsRequestMessage request, TimeSpan timeout)
		{
			UdpClient udpClient = new UdpClient(server.AddressFamily);
			try
			{
				int num = ((timeout.TotalMilliseconds >= 2147483647.0) ? (-1) : ((int)timeout.TotalMilliseconds));
				udpClient.Client.ReceiveTimeout = num;
				udpClient.Client.SendTimeout = num;
				using (DnsDatagramWriter dnsDatagramWriter = new DnsDatagramWriter())
				{
					GetRequestData(request, dnsDatagramWriter);
					udpClient.Client.SendTo(dnsDatagramWriter.Data.Array, dnsDatagramWriter.Data.Offset, dnsDatagramWriter.Data.Count, SocketFlags.None, server);
				}
				int num2 = ((udpClient.Available > 4096) ? udpClient.Available : 4096);
				using PooledBytes pooledBytes = new PooledBytes(num2);
				int count = udpClient.Client.Receive(pooledBytes.Buffer, 0, num2, SocketFlags.None);
				DnsResponseMessage responseMessage = GetResponseMessage(new ArraySegment<byte>(pooledBytes.Buffer, 0, count));
				DnsMessageHandler.ValidateResponse(request, responseMessage);
				return responseMessage;
			}
			finally
			{
				try
				{
					udpClient.Dispose();
				}
				catch
				{
				}
			}
		}

		public override async Task<DnsResponseMessage> QueryAsync(IPEndPoint endpoint, DnsRequestMessage request, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			UdpClient udpClient = new UdpClient(endpoint.AddressFamily);
			try
			{
				using (cancellationToken.Register(delegate
				{
					udpClient.Dispose();
				}))
				{
					using (DnsDatagramWriter writer = new DnsDatagramWriter())
					{
						GetRequestData(request, writer);
						await udpClient.SendAsync(writer.Data.Array, writer.Data.Count, endpoint).ConfigureAwait(continueOnCapturedContext: false);
					}
					int length = ((udpClient.Available > 4096) ? udpClient.Available : 4096);
					using PooledBytes memory = new PooledBytes(length);
					int count = await udpClient.Client.ReceiveAsync(new ArraySegment<byte>(memory.Buffer), SocketFlags.None).ConfigureAwait(continueOnCapturedContext: false);
					DnsResponseMessage responseMessage = GetResponseMessage(new ArraySegment<byte>(memory.Buffer, 0, count));
					DnsMessageHandler.ValidateResponse(request, responseMessage);
					return responseMessage;
				}
			}
			catch (SocketException ex) when (ex.SocketErrorCode == SocketError.OperationAborted)
			{
				throw new TimeoutException();
			}
			catch (ObjectDisposedException)
			{
				throw new TimeoutException();
			}
			finally
			{
				try
				{
					udpClient.Dispose();
				}
				catch
				{
				}
			}
		}
	}
	[Serializable]
	public class DnsXidMismatchException : Exception
	{
		public int RequestXid { get; }

		public int ResponseXid { get; }

		public DnsXidMismatchException(int requestXid, int responseXid)
		{
			RequestXid = requestXid;
			ResponseXid = responseXid;
		}
	}
	public interface IDnsQuery
	{
		IDnsQueryResponse Query(string query, QueryType queryType, QueryClass queryClass = QueryClass.IN);

		IDnsQueryResponse Query(DnsQuestion question);

		IDnsQueryResponse Query(DnsQuestion question, DnsQueryAndServerOptions queryOptions);

		IDnsQueryResponse QueryCache(DnsQuestion question);

		IDnsQueryResponse QueryCache(string query, QueryType queryType, QueryClass queryClass = QueryClass.IN);

		Task<IDnsQueryResponse> QueryAsync(string query, QueryType queryType, QueryClass queryClass = QueryClass.IN, CancellationToken cancellationToken = default(CancellationToken));

		Task<IDnsQueryResponse> QueryAsync(DnsQuestion question, CancellationToken cancellationToken = default(CancellationToken));

		Task<IDnsQueryResponse> QueryAsync(DnsQuestion question, DnsQueryAndServerOptions queryOptions, CancellationToken cancellationToken = default(CancellationToken));

		IDnsQueryResponse QueryReverse(IPAddress ipAddress);

		IDnsQueryResponse QueryReverse(IPAddress ipAddress, DnsQueryAndServerOptions queryOptions);

		Task<IDnsQueryResponse> QueryReverseAsync(IPAddress ipAddress, CancellationToken cancellationToken = default(CancellationToken));

		Task<IDnsQueryResponse> QueryReverseAsync(IPAddress ipAddress, DnsQueryAndServerOptions queryOptions, CancellationToken cancellationToken = default(CancellationToken));

		IDnsQueryResponse QueryServer(IReadOnlyCollection<NameServer> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN);

		IDnsQueryResponse QueryServer(IReadOnlyCollection<NameServer> servers, DnsQuestion question);

		IDnsQueryResponse QueryServer(IReadOnlyCollection<NameServer> servers, DnsQuestion question, DnsQueryOptions queryOptions);

		IDnsQueryResponse QueryServer(IReadOnlyCollection<IPEndPoint> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN);

		IDnsQueryResponse QueryServer(IReadOnlyCollection<IPAddress> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN);

		Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<NameServer> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN, CancellationToken cancellationToken = default(CancellationToken));

		Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<NameServer> servers, DnsQuestion question, CancellationToken cancellationToken = default(CancellationToken));

		Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<NameServer> servers, DnsQuestion question, DnsQueryOptions queryOptions, CancellationToken cancellationToken = default(CancellationToken));

		Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<IPAddress> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN, CancellationToken cancellationToken = default(CancellationToken));

		Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<IPEndPoint> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN, CancellationToken cancellationToken = default(CancellationToken));

		IDnsQueryResponse QueryServerReverse(IReadOnlyCollection<IPAddress> servers, IPAddress ipAddress);

		IDnsQueryResponse QueryServerReverse(IReadOnlyCollection<IPEndPoint> servers, IPAddress ipAddress);

		IDnsQueryResponse QueryServerReverse(IReadOnlyCollection<NameServer> servers, IPAddress ipAddress);

		IDnsQueryResponse QueryServerReverse(IReadOnlyCollection<NameServer> servers, IPAddress ipAddress, DnsQueryOptions queryOptions);

		Task<IDnsQueryResponse> QueryServerReverseAsync(IReadOnlyCollection<IPAddress> servers, IPAddress ipAddress, CancellationToken cancellationToken = default(CancellationToken));

		Task<IDnsQueryResponse> QueryServerReverseAsync(IReadOnlyCollection<IPEndPoint> servers, IPAddress ipAddress, CancellationToken cancellationToken = default(CancellationToken));

		Task<IDnsQueryResponse> QueryServerReverseAsync(IReadOnlyCollection<NameServer> servers, IPAddress ipAddress, CancellationToken cancellationToken = default(CancellationToken));

		Task<IDnsQueryResponse> QueryServerReverseAsync(IReadOnlyCollection<NameServer> servers, IPAddress ipAddress, DnsQueryOptions queryOptions, CancellationToken cancellationToken = default(CancellationToken));
	}
	public interface IDnsQueryResponse
	{
		IReadOnlyList<DnsQuestion> Questions { get; }

		IReadOnlyList<DnsResourceRecord> Additionals { get; }

		IEnumerable<DnsResourceRecord> AllRecords { get; }

		IReadOnlyList<DnsResourceRecord> Answers { get; }

		IReadOnlyList<DnsResourceRecord> Authorities { get; }

		string AuditTrail { get; }

		string ErrorMessage { get; }

		bool HasError { get; }

		DnsResponseHeader Header { get; }

		int MessageSize { get; }

		NameServer NameServer { get; }

		DnsQuerySettings Settings { get; }
	}
	public interface ILookupClient : IDnsQuery
	{
		IReadOnlyCollection<NameServer> NameServers { get; }

		LookupClientSettings Settings { get; }

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		TimeSpan? MinimumCacheTimeout { get; set; }

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		bool EnableAuditTrail { get; set; }

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		bool UseCache { get; set; }

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		bool Recursion { get; set; }

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		int Retries { get; set; }

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		bool ThrowDnsErrors { get; set; }

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		bool UseRandomNameServer { get; set; }

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		bool ContinueOnDnsError { get; set; }

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		TimeSpan Timeout { get; set; }

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		bool UseTcpFallback { get; set; }

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		bool UseTcpOnly { get; set; }
	}
	internal class DisposableIntPtr : IDisposable
	{
		private IntPtr _ptr;

		public IntPtr Ptr => _ptr;

		public bool IsValid { get; private set; } = true;

		private DisposableIntPtr()
		{
		}

		public static DisposableIntPtr Alloc(int size)
		{
			DisposableIntPtr disposableIntPtr = new DisposableIntPtr();
			try
			{
				disposableIntPtr._ptr = Marshal.AllocHGlobal(size);
			}
			catch (OutOfMemoryException)
			{
				disposableIntPtr.IsValid = false;
			}
			return disposableIntPtr;
		}

		public void Dispose()
		{
			Marshal.FreeHGlobal(_ptr);
		}
	}
	public class LookupClient : ILookupClient, IDnsQuery
	{
		private enum HandleError
		{
			None,
			Throw,
			RetryCurrentServer,
			RetryNextServer,
			ReturnResponse
		}

		private class SkipWorker
		{
			private readonly Action _worker;

			private readonly int _skipFor = 5000;

			private int _lastRun;

			public SkipWorker(Action worker, int skip = 5000)
			{
				_worker = worker ?? throw new ArgumentNullException("worker");
				if (skip <= 1)
				{
					throw new ArgumentOutOfRangeException("skip");
				}
				_skipFor = skip;
				_lastRun = Environment.TickCount & 0x7FFFFFFF;
			}

			public void MaybeDoWork()
			{
				if (_lastRun + _skipFor < (Environment.TickCount & 0x7FFFFFFF))
				{
					int lastRun = _lastRun;
					if (Interlocked.CompareExchange(ref _lastRun, Environment.TickCount & 0x7FFFFFFF, lastRun) == lastRun)
					{
						_worker();
					}
				}
			}
		}

		private const int LogEventStartQuery = 1;

		private const int LogEventQuery = 2;

		private const int LogEventQueryCachedResult = 3;

		private const int LogEventQueryTruncated = 5;

		private const int LogEventQuerySuccess = 10;

		private const int LogEventQueryReturnResponseError = 11;

		private const int LogEventQuerySuccessEmpty = 12;

		private const int LogEventQueryRetryErrorNextServer = 20;

		private const int LogEventQueryRetryErrorSameServer = 21;

		private const int LogEventQueryFail = 90;

		private const int LogEventQueryBadTruncation = 91;

		private const int LogEventResponseOpt = 31;

		private const int LogEventResponseMissingOpt = 80;

		private readonly LookupClientOptions _originalOptions;

		private readonly DnsMessageHandler _messageHandler;

		private readonly DnsMessageHandler _tcpFallbackHandler;

		private readonly ILogger _logger;

		private readonly SkipWorker _skipper;

		private IReadOnlyCollection<NameServer> _resolvedNameServers;

		public IReadOnlyCollection<NameServer> NameServers => Settings.NameServers;

		public LookupClientSettings Settings { get; private set; }

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		public TimeSpan? MinimumCacheTimeout
		{
			get
			{
				return Settings.MinimumCacheTimeout;
			}
			set
			{
				if (Settings.MinimumCacheTimeout != value)
				{
					_originalOptions.MinimumCacheTimeout = value;
					Settings = new LookupClientSettings(_originalOptions, Settings.NameServers);
				}
			}
		}

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		public bool UseTcpFallback
		{
			get
			{
				return Settings.UseTcpFallback;
			}
			set
			{
				if (Settings.UseTcpFallback != value)
				{
					_originalOptions.UseTcpFallback = value;
					Settings = new LookupClientSettings(_originalOptions, Settings.NameServers);
				}
			}
		}

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		public bool UseTcpOnly
		{
			get
			{
				return Settings.UseTcpOnly;
			}
			set
			{
				if (Settings.UseTcpOnly != value)
				{
					_originalOptions.UseTcpOnly = value;
					Settings = new LookupClientSettings(_originalOptions, Settings.NameServers);
				}
			}
		}

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		public bool EnableAuditTrail
		{
			get
			{
				return Settings.EnableAuditTrail;
			}
			set
			{
				if (Settings.EnableAuditTrail != value)
				{
					_originalOptions.EnableAuditTrail = value;
					Settings = new LookupClientSettings(_originalOptions, Settings.NameServers);
				}
			}
		}

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		public bool Recursion
		{
			get
			{
				return Settings.Recursion;
			}
			set
			{
				if (Settings.Recursion != value)
				{
					_originalOptions.Recursion = value;
					Settings = new LookupClientSettings(_originalOptions, Settings.NameServers);
				}
			}
		}

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		public int Retries
		{
			get
			{
				return Settings.Retries;
			}
			set
			{
				if (Settings.Retries != value)
				{
					_originalOptions.Retries = value;
					Settings = new LookupClientSettings(_originalOptions, Settings.NameServers);
				}
			}
		}

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		public bool ThrowDnsErrors
		{
			get
			{
				return Settings.ThrowDnsErrors;
			}
			set
			{
				if (Settings.ThrowDnsErrors != value)
				{
					_originalOptions.ThrowDnsErrors = value;
					Settings = new LookupClientSettings(_originalOptions, Settings.NameServers);
				}
			}
		}

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		public TimeSpan Timeout
		{
			get
			{
				return Settings.Timeout;
			}
			set
			{
				if (Settings.Timeout != value)
				{
					_originalOptions.Timeout = value;
					Settings = new LookupClientSettings(_originalOptions, Settings.NameServers);
				}
			}
		}

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		public bool UseCache
		{
			get
			{
				return Settings.UseCache;
			}
			set
			{
				if (Settings.UseCache != value)
				{
					_originalOptions.UseCache = value;
					Settings = new LookupClientSettings(_originalOptions, Settings.NameServers);
				}
			}
		}

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		public bool UseRandomNameServer
		{
			get
			{
				return Settings.UseRandomNameServer;
			}
			set
			{
				if (Settings.UseRandomNameServer != value)
				{
					_originalOptions.UseRandomNameServer = value;
					Settings = new LookupClientSettings(_originalOptions, Settings.NameServers);
				}
			}
		}

		[Obsolete("This property will be removed from LookupClient in the next version. Use LookupClientOptions to initialize LookupClient instead.")]
		public bool ContinueOnDnsError
		{
			get
			{
				return Settings.ContinueOnDnsError;
			}
			set
			{
				if (Settings.ContinueOnDnsError != value)
				{
					_originalOptions.ContinueOnDnsError = value;
					Settings = new LookupClientSettings(_originalOptions, Settings.NameServers);
				}
			}
		}

		internal ResponseCache Cache { get; }

		public LookupClient()
			: this(new LookupClientOptions())
		{
		}

		public LookupClient(params IPAddress[] nameServers)
			: this(new LookupClientOptions(nameServers))
		{
		}

		public LookupClient(IPAddress address, int port)
			: this(new LookupClientOptions(new NameServer(address, port)))
		{
		}

		public LookupClient(params IPEndPoint[] nameServers)
			: this(new LookupClientOptions(nameServers))
		{
		}

		public LookupClient(params NameServer[] nameServers)
			: this(new LookupClientOptions(nameServers))
		{
		}

		public LookupClient(LookupClientOptions options)
			: this(options, null, null)
		{
		}

		internal LookupClient(LookupClientOptions options, DnsMessageHandler udpHandler = null, DnsMessageHandler tcpHandler = null)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			_originalOptions = options;
			_logger = Logging.LoggerFactory.CreateLogger(GetType().FullName);
			_messageHandler = udpHandler ?? new DnsUdpMessageHandler();
			_tcpFallbackHandler = tcpHandler ?? new DnsTcpMessageHandler();
			if (_messageHandler.Type != DnsMessageHandleType.UDP)
			{
				throw new ArgumentException("UDP message handler's type must be UDP.", "udpHandler");
			}
			if (_tcpFallbackHandler.Type != DnsMessageHandleType.TCP)
			{
				throw new ArgumentException("TCP message handler's type must be TCP.", "tcpHandler");
			}
			IReadOnlyCollection<NameServer> readOnlyCollection = (IReadOnlyCollection<NameServer>)(object)(_originalOptions.NameServers?.ToArray() ?? new NameServer[0]);
			if (options.AutoResolveNameServers)
			{
				_resolvedNameServers = NameServer.ResolveNameServers(skipIPv6SiteLocal: true, fallbackToGooglePublicDns: false);
				readOnlyCollection = (IReadOnlyCollection<NameServer>)(object)readOnlyCollection.Concat(_resolvedNameServers).ToArray();
				_skipper = new SkipWorker(delegate
				{
					if (_logger.IsEnabled(LogLevel.Debug))
					{
						_logger.LogDebug("Checking resolved name servers for network changes...");
					}
					CheckResolvedNameservers();
				}, 60000);
			}
			readOnlyCollection = NameServer.ValidateNameServers(readOnlyCollection, _logger);
			Settings = new LookupClientSettings(options, readOnlyCollection);
			Cache = new ResponseCache(enabled: true, Settings.MinimumCacheTimeout, Settings.MaximumCacheTimeout, Settings.FailedResultsCacheDuration);
		}

		private void CheckResolvedNameservers()
		{
			try
			{
				IReadOnlyCollection<NameServer> readOnlyCollection = NameServer.ResolveNameServers(skipIPv6SiteLocal: true, fallbackToGooglePublicDns: false);
				if (readOnlyCollection == null || readOnlyCollection.Count == 0)
				{
					_logger.LogWarning("Could not resolve any name servers, keeping current configuration.");
					return;
				}
				if (_resolvedNameServers.SequenceEqual(readOnlyCollection))
				{
					if (_logger.IsEnabled(LogLevel.Debug))
					{
						_logger.LogDebug("No name server changes detected, still using {0}", string.Join(",", readOnlyCollection));
					}
					return;
				}
				if (_logger.IsEnabled(LogLevel.Information))
				{
					_logger.LogInformation("Found changes in local network, configured name servers now are: {0}", string.Join(",", readOnlyCollection));
				}
				_resolvedNameServers = readOnlyCollection;
				NameServer[] overrideServers = _originalOptions.NameServers.Concat(_resolvedNameServers).ToArray();
				Settings = new LookupClientSettings(_originalOptions, (IReadOnlyCollection<NameServer>)(object)overrideServers);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, "Error trying to resolve name servers.");
			}
		}

		public IDnsQueryResponse QueryReverse(IPAddress ipAddress)
		{
			return Query(GetReverseQuestion(ipAddress));
		}

		public IDnsQueryResponse QueryReverse(IPAddress ipAddress, DnsQueryAndServerOptions queryOptions)
		{
			return Query(GetReverseQuestion(ipAddress), queryOptions);
		}

		public Task<IDnsQueryResponse> QueryReverseAsync(IPAddress ipAddress, CancellationToken cancellationToken = default(CancellationToken))
		{
			return QueryAsync(GetReverseQuestion(ipAddress), cancellationToken);
		}

		public Task<IDnsQueryResponse> QueryReverseAsync(IPAddress ipAddress, DnsQueryAndServerOptions queryOptions, CancellationToken cancellationToken = default(CancellationToken))
		{
			return QueryAsync(GetReverseQuestion(ipAddress), queryOptions, cancellationToken);
		}

		public IDnsQueryResponse Query(string query, QueryType queryType, QueryClass queryClass = QueryClass.IN)
		{
			return Query(new DnsQuestion(query, queryType, queryClass));
		}

		public IDnsQueryResponse Query(DnsQuestion question)
		{
			if (question == null)
			{
				throw new ArgumentNullException("question");
			}
			DnsQueryAndServerSettings settings = GetSettings();
			return QueryInternal(question, settings, settings.ShuffleNameServers());
		}

		public IDnsQueryResponse Query(DnsQuestion question, DnsQueryAndServerOptions queryOptions)
		{
			if (question == null)
			{
				throw new ArgumentNullException("question");
			}
			if (queryOptions == null)
			{
				throw new ArgumentNullException("queryOptions");
			}
			DnsQueryAndServerSettings settings = GetSettings(queryOptions);
			return QueryInternal(question, settings, settings.ShuffleNameServers());
		}

		public IDnsQueryResponse QueryCache(string query, QueryType queryType, QueryClass queryClass = QueryClass.IN)
		{
			return QueryCache(new DnsQuestion(query, queryType, queryClass));
		}

		public IDnsQueryResponse QueryCache(DnsQuestion question)
		{
			if (question == null)
			{
				throw new ArgumentNullException("question");
			}
			DnsQueryAndServerSettings settings = GetSettings();
			return QueryCache(question, settings);
		}

		public Task<IDnsQueryResponse> QueryAsync(string query, QueryType queryType, QueryClass queryClass = QueryClass.IN, CancellationToken cancellationToken = default(CancellationToken))
		{
			return QueryAsync(new DnsQuestion(query, queryType, queryClass), cancellationToken);
		}

		public Task<IDnsQueryResponse> QueryAsync(DnsQuestion question, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (question == null)
			{
				throw new ArgumentNullException("question");
			}
			DnsQueryAndServerSettings settings = GetSettings();
			return QueryInternalAsync(question, settings, settings.ShuffleNameServers(), cancellationToken);
		}

		public Task<IDnsQueryResponse> QueryAsync(DnsQuestion question, DnsQueryAndServerOptions queryOptions, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (question == null)
			{
				throw new ArgumentNullException("question");
			}
			if (queryOptions == null)
			{
				throw new ArgumentNullException("queryOptions");
			}
			DnsQueryAndServerSettings settings = GetSettings(queryOptions);
			return QueryInternalAsync(question, settings, settings.ShuffleNameServers(), cancellationToken);
		}

		public IDnsQueryResponse QueryServer(IReadOnlyCollection<IPAddress> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN)
		{
			return QueryServer((IReadOnlyCollection<NameServer>)(object)NameServer.Convert(servers), new DnsQuestion(query, queryType, queryClass));
		}

		public IDnsQueryResponse QueryServer(IReadOnlyCollection<IPEndPoint> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN)
		{
			return QueryServer((IReadOnlyCollection<NameServer>)(object)NameServer.Convert(servers), new DnsQuestion(query, queryType, queryClass));
		}

		public IDnsQueryResponse QueryServer(IReadOnlyCollection<NameServer> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN)
		{
			return QueryServer(servers, new DnsQuestion(query, queryType, queryClass));
		}

		public IDnsQueryResponse QueryServer(IReadOnlyCollection<NameServer> servers, DnsQuestion question)
		{
			if (servers == null)
			{
				throw new ArgumentNullException("servers");
			}
			if (servers.Count == 0)
			{
				throw new ArgumentOutOfRangeException("servers", "List of configured name servers must not be empty.");
			}
			servers = NameServer.ValidateNameServers(servers, _logger);
			return QueryInternal(question, Settings, servers);
		}

		public IDnsQueryResponse QueryServer(IReadOnlyCollection<NameServer> servers, DnsQuestion question, DnsQueryOptions queryOptions)
		{
			if (servers == null)
			{
				throw new ArgumentNullException("servers");
			}
			if (servers.Count == 0)
			{
				throw new ArgumentOutOfRangeException("servers", "List of configured name servers must not be empty.");
			}
			servers = NameServer.ValidateNameServers(servers, _logger);
			return QueryInternal(question, queryOptions, servers);
		}

		public Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<IPAddress> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN, CancellationToken cancellationToken = default(CancellationToken))
		{
			return QueryServerAsync((IReadOnlyCollection<NameServer>)(object)NameServer.Convert(servers), new DnsQuestion(query, queryType, queryClass), cancellationToken);
		}

		public Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<IPEndPoint> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN, CancellationToken cancellationToken = default(CancellationToken))
		{
			return QueryServerAsync((IReadOnlyCollection<NameServer>)(object)NameServer.Convert(servers), new DnsQuestion(query, queryType, queryClass), cancellationToken);
		}

		public Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<NameServer> servers, string query, QueryType queryType, QueryClass queryClass = QueryClass.IN, CancellationToken cancellationToken = default(CancellationToken))
		{
			return QueryServerAsync(servers, new DnsQuestion(query, queryType, queryClass), cancellationToken);
		}

		public Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<NameServer> servers, DnsQuestion question, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (servers == null)
			{
				throw new ArgumentNullException("servers");
			}
			if (servers.Count == 0)
			{
				throw new ArgumentOutOfRangeException("servers", "List of configured name servers must not be empty.");
			}
			servers = NameServer.ValidateNameServers(servers, _logger);
			return QueryInternalAsync(question, Settings, servers, cancellationToken);
		}

		public Task<IDnsQueryResponse> QueryServerAsync(IReadOnlyCollection<NameServer> servers, DnsQuestion question, DnsQueryOptions queryOptions, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (servers == null)
			{
				throw new ArgumentNullException("servers");
			}
			if (servers.Count == 0)
			{
				throw new ArgumentOutOfRangeException("servers", "List of configured name servers must not be empty.");
			}
			servers = NameServer.ValidateNameServers(servers, _logger);
			return QueryInternalAsync(question, queryOptions, servers, cancellationToken);
		}

		public IDnsQueryResponse QueryServerReverse(IReadOnlyCollection<IPAddress> servers, IPAddress ipAddress)
		{
			return QueryServerReverse((IReadOnlyCollection<NameServer>)(object)NameServer.Convert(servers), ipAddress);
		}

		public IDnsQueryResponse QueryServerReverse(IReadOnlyCollection<IPEndPoint> servers, IPAddress ipAddress)
		{
			return QueryServerReverse((IReadOnlyCollection<NameServer>)(object)NameServer.Convert(servers), ipAddress);
		}

		public IDnsQueryResponse QueryServerReverse(IReadOnlyCollection<NameServer> servers, IPAddress ipAddress)
		{
			return QueryServer(servers, GetReverseQuestion(ipAddress));
		}

		public IDnsQueryResponse QueryServerReverse(IReadOnlyCollection<NameServer> servers, IPAddress ipAddress, DnsQueryOptions queryOptions)
		{
			return QueryServer(servers, GetReverseQuestion(ipAddress), queryOptions);
		}

		public Task<IDnsQueryResponse> QueryServerReverseAsync(IReadOnlyCollection<IPAddress> servers, IPAddress ipAddress, CancellationToken cancellationToken = default(CancellationToken))
		{
			return QueryServerReverseAsync((IReadOnlyCollection<NameServer>)(object)NameServer.Convert(servers), ipAddress, cancellationToken);
		}

		public Task<IDnsQueryResponse> QueryServerReverseAsync(IReadOnlyCollection<IPEndPoint> servers, IPAddress ipAddress, CancellationToken cancellationToken = default(CancellationToken))
		{
			return QueryServerReverseAsync((IReadOnlyCollection<NameServer>)(object)NameServer.Convert(servers), ipAddress, cancellationToken);
		}

		public Task<IDnsQueryResponse> QueryServerReverseAsync(IReadOnlyCollection<NameServer> servers, IPAddress ipAddress, CancellationToken cancellationToken = default(CancellationToken))
		{
			return QueryServerAsync(servers, GetReverseQuestion(ipAddress), cancellationToken);
		}

		public Task<IDnsQueryResponse> QueryServerReverseAsync(IReadOnlyCollection<NameServer> servers, IPAddress ipAddress, DnsQueryOptions queryOptions, CancellationToken cancellationToken = default(CancellationToken))
		{
			return QueryServerAsync(servers, GetReverseQuestion(ipAddress), queryOptions, cancellationToken);
		}

		internal DnsQueryAndServerSettings GetSettings(DnsQueryAndServerOptions queryOptions = null)
		{
			if (_originalOptions.AutoResolveNameServers)
			{
				_skipper?.MaybeDoWork();
			}
			if (queryOptions == null)
			{
				return Settings;
			}
			if (queryOptions.NameServers == null || queryOptions.NameServers.Count == 0)
			{
				return new DnsQueryAndServerSettings(queryOptions, Settings.NameServers);
			}
			return queryOptions;
		}

		private IDnsQueryResponse QueryInternal(DnsQuestion question, DnsQuerySettings queryOptions, IReadOnlyCollection<NameServer> servers)
		{
			if (servers == null)
			{
				throw new ArgumentNullException("servers");
			}
			if (question == null)
			{
				throw new ArgumentNullException("question");
			}
			if (queryOptions == null)
			{
				throw new ArgumentNullException("queryOptions");
			}
			if (servers.Count == 0)
			{
				throw new ArgumentOutOfRangeException("servers", "List of configured name servers must not be empty.");
			}
			DnsRequestHeader dnsRequestHeader = new DnsRequestHeader(queryOptions.Recursion, DnsOpCode.Query);
			DnsRequestMessage dnsRequestMessage = new DnsRequestMessage(dnsRequestHeader, question, queryOptions);
			DnsMessageHandler dnsMessageHandler = (queryOptions.UseTcpOnly ? _tcpFallbackHandler : _messageHandler);
			LookupClientAudit lookupClientAudit = (queryOptions.EnableAuditTrail ? new LookupClientAudit(queryOptions) : null);
			if (_logger.IsEnabled(LogLevel.Debug))
			{
				_logger.LogDebug(1, "Begin query [{0}] via {1} => {2} on [{3}].", dnsRequestHeader, dnsMessageHandler.Type, question, string.Join(", ", servers));
			}
			IDnsQueryResponse dnsQueryResponse = ResolveQuery(servers.ToList(), queryOptions, dnsMessageHandler, dnsRequestMessage, lookupClientAudit);
			if (!(dnsQueryResponse is TruncatedQueryResponse))
			{
				return dnsQueryResponse;
			}
			if (!queryOptions.UseTcpFallback)
			{
				throw new DnsResponseException(DnsResponseCode.Unassigned, "Response was truncated and UseTcpFallback is disabled, unable to resolve the question.")
				{
					AuditTrail = lookupClientAudit?.Build(dnsQueryResponse)
				};
			}
			dnsRequestMessage.Header.RefreshId();
			IDnsQueryResponse dnsQueryResponse2 = ResolveQuery(servers.ToList(), queryOptions, _tcpFallbackHandler, dnsRequestMessage, lookupClientAudit);
			if (dnsQueryResponse2 is TruncatedQueryResponse)
			{
				throw new DnsResponseException("Unexpected truncated result from TCP response.")
				{
					AuditTrail = lookupClientAudit?.Build(dnsQueryResponse2)
				};
			}
			return dnsQueryResponse2;
		}

		private async Task<IDnsQueryResponse> QueryInternalAsync(DnsQuestion question, DnsQuerySettings queryOptions, IReadOnlyCollection<NameServer> servers, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (servers == null)
			{
				throw new ArgumentNullException("servers");
			}
			if (question == null)
			{
				throw new ArgumentNullException("question");
			}
			if (queryOptions == null)
			{
				throw new ArgumentNullException("queryOptions");
			}
			if (servers.Count == 0)
			{
				throw new ArgumentOutOfRangeException("servers", "List of configured name servers must not be empty.");
			}
			DnsRequestHeader dnsRequestHeader = new DnsRequestHeader(queryOptions.Recursion, DnsOpCode.Query);
			DnsRequestMessage request = new DnsRequestMessage(dnsRequestHeader, question, queryOptions);
			DnsMessageHandler dnsMessageHandler = (queryOptions.UseTcpOnly ? _tcpFallbackHandler : _messageHandler);
			LookupClientAudit audit = (queryOptions.EnableAuditTrail ? new LookupClientAudit(queryOptions) : null);
			if (_logger.IsEnabled(LogLevel.Debug))
			{
				_logger.LogDebug(1, "Begin query [{0}] via {1} => {2} on [{3}].", dnsRequestHeader, dnsMessageHandler.Type, question, string.Join(", ", servers));
			}
			IDnsQueryResponse dnsQueryResponse = await ResolveQueryAsync(servers.ToList(), queryOptions, dnsMessageHandler, request, audit, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (!(dnsQueryResponse is TruncatedQueryResponse))
			{
				return dnsQueryResponse;
			}
			if (!queryOptions.UseTcpFallback)
			{
				throw new DnsResponseException(DnsResponseCode.Unassigned, "Response was truncated and UseTcpFallback is disabled, unable to resolve the question.")
				{
					AuditTrail = audit?.Build(dnsQueryResponse)
				};
			}
			request.Header.RefreshId();
			IDnsQueryResponse dnsQueryResponse2 = await ResolveQueryAsync(servers.ToList(), queryOptions, _tcpFallbackHandler, request, audit, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (dnsQueryResponse2 is TruncatedQueryResponse)
			{
				throw new DnsResponseException("Unexpected truncated result from TCP response.")
				{
					AuditTrail = audit?.Build(dnsQueryResponse2)
				};
			}
			return dnsQueryResponse2;
		}

		private IDnsQueryResponse ResolveQuery(IReadOnlyList<NameServer> servers, DnsQuerySettings settings, DnsMessageHandler handler, DnsRequestMessage request, LookupClientAudit audit = null)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			for (int i = 0; i < servers.Count; i++)
			{
				NameServer nameServer = servers[i];
				bool flag = i >= servers.Count - 1;
				if (i > 0)
				{
					request.Header.RefreshId();
				}
				if (settings.EnableAuditTrail && !flag)
				{
					audit?.AuditRetryNextServer();
				}
				string text = string.Empty;
				if (settings.UseCache)
				{
					text = ResponseCache.GetCacheKey(request.Question);
					if (TryGetCachedResult(text, request, settings, out var response))
					{
						return response;
					}
				}
				int num = 0;
				do
				{
					if (num > 0)
					{
						request.Header.RefreshId();
					}
					num++;
					bool isLastTry = num > settings.Retries;
					IDnsQueryResponse dnsQueryResponse = null;
					if (_logger.IsEnabled(LogLevel.Debug))
					{
						_logger.LogDebug(2, "TryResolve {0} via {1} => {2} on {3}, try {4}/{5}.", request.Header.Id, handler.Type, request.Question, nameServer, num, settings.Retries + 1);
					}
					try
					{
						audit?.StartTimer();
						DnsResponseMessage dnsResponseMessage = handler.Query(nameServer.IPEndPoint, request, settings.Timeout);
						dnsQueryResponse = ProcessResponseMessage(audit, request, dnsResponseMessage, settings, nameServer, handler.Type, servers.Count, flag, out var handleError);
						if (dnsQueryResponse is TruncatedQueryResponse)
						{
							return dnsQueryResponse;
						}
						audit?.AuditEnd(dnsQueryResponse, nameServer);
						audit?.Build(dnsQueryResponse);
						if (dnsQueryResponse.HasError)
						{
							throw new DnsResponseException((DnsResponseCode)dnsResponseMessage.Header.ResponseCode)
							{
								AuditTrail = audit?.Build()
							};
						}
						if (handleError == HandleError.RetryNextServer)
						{
							break;
						}
						if (settings.UseCache)
						{
							Cache.Add(text, dnsQueryResponse);
						}
						return dnsQueryResponse;
					}
					catch (DnsXidMismatchException ex)
					{
						switch (HandleDnsXidMismatchException(ex, request, settings, handler.Type, flag, isLastTry, num))
						{
						case HandleError.RetryNextServer:
							break;
						default:
							throw;
						case HandleError.RetryCurrentServer:
							continue;
						}
					}
					catch (DnsResponseParseException ex2)
					{
						switch (HandleDnsResponeParseException(ex2, request, handler.Type, flag))
						{
						case HandleError.RetryNextServer:
							break;
						case HandleError.ReturnResponse:
							return new TruncatedQueryResponse();
						default:
							throw;
						}
					}
					catch (DnsResponseException ex3)
					{
						switch (HandleDnsResponseException(ex3, request, settings, nameServer, handler.Type, flag, isLastTry, num))
						{
						case HandleError.Throw:
							throw;
						case HandleError.RetryNextServer:
							break;
						default:
							if (dnsQueryResponse == null)
							{
								throw;
							}
							if (settings.UseCache && settings.CacheFailedResults)
							{
								Cache.Add(text, dnsQueryResponse, cacheFailures: true);
							}
							return dnsQueryResponse;
						case HandleError.RetryCurrentServer:
							continue;
						}
					}
					catch (Exception ex4) when (ex4 is TimeoutException || DnsMessageHandler.IsTransientException(ex4) || ex4 is OperationCanceledException)
					{
						switch (HandleTimeoutException(ex4, request, settings, nameServer, handler.Type, flag, isLastTry, num))
						{
						case HandleError.RetryNextServer:
							break;
						default:
							throw new DnsResponseException(DnsResponseCode.ConnectionTimeout, $"Query {request.Header.Id} => {request.Question} on {nameServer} timed out or is a transient error.", ex4)
							{
								AuditTrail = audit?.Build()
							};
						case HandleError.RetryCurrentServer:
							continue;
						}
					}
					catch (ArgumentException)
					{
						throw;
					}
					catch (InvalidOperationException)
					{
						throw;
					}
					catch (Exception ex7)
					{
						audit?.AuditException(ex7);
						switch (HandleUnhandledException(ex7, request, nameServer, handler.Type, flag))
						{
						case HandleError.RetryNextServer:
							break;
						default:
							throw new DnsResponseException(DnsResponseCode.Unassigned, $"Query {request.Header.Id} => {request.Question} on {nameServer} failed with an error.", ex7)
							{
								AuditTrail = audit?.Build()
							};
						case HandleError.RetryCurrentServer:
							continue;
						}
					}
					break;
				}
				while (num <= settings.Retries);
			}
			throw new DnsResponseException(DnsResponseCode.ConnectionTimeout, "No connection could be established to any of the following name servers: " + string.Join(", ", servers) + ".")
			{
				AuditTrail = audit?.Build()
			};
		}

		private async Task<IDnsQueryResponse> ResolveQueryAsync(IReadOnlyList<NameServer> servers, DnsQuerySettings settings, DnsMessageHandler handler, DnsRequestMessage request, LookupClientAudit audit = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			for (int serverIndex = 0; serverIndex < servers.Count; serverIndex++)
			{
				NameServer serverInfo = servers[serverIndex];
				bool isLastServer = serverIndex >= servers.Count - 1;
				if (serverIndex > 0)
				{
					request.Header.RefreshId();
				}
				if (settings.EnableAuditTrail && serverIndex > 0 && !isLastServer)
				{
					audit?.AuditRetryNextServer();
				}
				string cacheKey = string.Empty;
				if (settings.UseCache)
				{
					cacheKey = ResponseCache.GetCacheKey(request.Question);
					if (TryGetCachedResult(cacheKey, request, settings, out var response))
					{
						return response;
					}
				}
				int tries = 0;
				do
				{
					if (tries > 0)
					{
						request.Header.RefreshId();
					}
					tries++;
					bool isLastTry = tries > settings.Retries;
					IDnsQueryResponse lastQueryResponse = null;
					if (_logger.IsEnabled(LogLevel.Debug))
					{
						_logger.LogDebug(2, "TryResolve {0} via {1} => {2} on {3}, try {4}/{5}.", request.Header.Id, handler.Type, request.Question, serverInfo, tries, settings.Retries + 1);
					}
					try
					{
						cancellationToken.ThrowIfCancellationRequested();
						audit?.StartTimer();
						DnsResponseMessage dnsResponseMessage;
						if (settings.Timeout != System.Threading.Timeout.InfiniteTimeSpan || (cancellationToken != CancellationToken.None && cancellationToken.CanBeCanceled))
						{
							CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(settings.Timeout);
							CancellationTokenSource cancellationTokenSource2 = null;
							if (cancellationToken != CancellationToken.None)
							{
								cancellationTokenSource2 = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenSource.Token, cancellationToken);
							}
							using (cancellationTokenSource)
							{
								using (cancellationTokenSource2)
								{
									dnsResponseMessage = await handler.QueryAsync(serverInfo.IPEndPoint, request, (cancellationTokenSource2 ?? cancellationTokenSource).Token).WithCancellation((cancellationTokenSource2 ?? cancellationTokenSource).Token).ConfigureAwait(continueOnCapturedContext: false);
								}
							}
						}
						else
						{
							dnsResponseMessage = await handler.QueryAsync(serverInfo.IPEndPoint, request, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						}
						lastQueryResponse = ProcessResponseMessage(audit, request, dnsResponseMessage, settings, serverInfo, handler.Type, servers.Count, isLastServer, out var handleError);
						if (lastQueryResponse is TruncatedQueryResponse)
						{
							return lastQueryResponse;
						}
						audit?.AuditEnd(lastQueryResponse, serverInfo);
						audit?.Build(lastQueryResponse);
						if (lastQueryResponse.HasError)
						{
							throw new DnsResponseException((DnsResponseCode)dnsResponseMessage.Header.ResponseCode)
							{
								AuditTrail = audit?.Build()
							};
						}
						if (handleError == HandleError.RetryNextServer)
						{
							break;
						}
						if (settings.UseCache)
						{
							Cache.Add(cacheKey, lastQueryResponse);
						}
						return lastQueryResponse;
					}
					catch (DnsXidMismatchException ex)
					{
						switch (HandleDnsXidMismatchException(ex, request, settings, handler.Type, isLastServer, isLastTry, tries))
						{
						case HandleError.RetryNextServer:
							break;
						default:
							throw;
						case HandleError.RetryCurrentServer:
							continue;
						}
					}
					catch (DnsResponseParseException ex2)
					{
						switch (HandleDnsResponeParseException(ex2, request, handler.Type, isLastServer))
						{
						case HandleError.RetryNextServer:
							break;
						case HandleError.ReturnResponse:
							return new TruncatedQueryResponse();
						default:
							throw;
						}
					}
					catch (DnsResponseException ex3)
					{
						switch (HandleDnsResponseException(ex3, request, settings, serverInfo, handler.Type, isLastServer, isLastTry, tries))
						{
						case HandleError.Throw:
							throw;
						case HandleError.RetryNextServer:
							break;
						default:
							if (lastQueryResponse == null)
							{
								throw;
							}
							if (settings.UseCache && settings.CacheFailedResults)
							{
								Cache.Add(cacheKey, lastQueryResponse, cacheFailures: true);
							}
							return lastQueryResponse;
						case HandleError.RetryCurrentServer:
							continue;
						}
					}
					catch (Exception ex4) when (ex4 is TimeoutException || DnsMessageHandler.IsTransientException(ex4) || ex4 is OperationCanceledException)
					{
						if (!cancellationToken.IsCancellationRequested)
						{
							switch (HandleTimeoutException(ex4, request, settings, serverInfo, handler.Type, isLastServer, isLastTry, tries))
							{
							case HandleError.RetryNextServer:
								goto end_IL_0677;
							case HandleError.RetryCurrentServer:
								continue;
							}
						}
						throw new DnsResponseException(DnsResponseCode.ConnectionTimeout, $"Query {request.Header.Id} => {request.Question} on {serverInfo} timed out or is a transient error.", ex4)
						{
							AuditTrail = audit?.Build()
						};
						end_IL_0677:;
					}
					catch (ArgumentException)
					{
						throw;
					}
					catch (InvalidOperationException)
					{
						throw;
					}
					catch (Exception ex7)
					{
						audit?.AuditException(ex7);
						switch (HandleUnhandledException(ex7, request, serverInfo, handler.Type, isLastServer))
						{
						case HandleError.RetryNextServer:
							break;
						default:
							throw new DnsResponseException(DnsResponseCode.Unassigned, $"Query {request.Header.Id} => {request.Question} on {serverInfo} failed with an error.", ex7)
							{
								AuditTrail = audit?.Build()
							};
						case HandleError.RetryCurrentServer:
							continue;
						}
					}
					break;
				}
				while (tries <= settings.Retries);
			}
			throw new DnsResponseException(DnsResponseCode.ConnectionTimeout, "No connection could be established to any of the following name servers: " + string.Join(", ", servers) + ".")
			{
				AuditTrail = audit?.Build()
			};
		}

		private IDnsQueryResponse QueryCache(DnsQuestion question, DnsQuerySettings settings)
		{
			if (question == null)
			{
				throw new ArgumentNullException("question");
			}
			DnsRequestMessage dnsRequestMessage = new DnsRequestMessage(new DnsRequestHeader(useRecursion: false, DnsOpCode.Query), question);
			string cacheKey = ResponseCache.GetCacheKey(dnsRequestMessage.Question);
			if (TryGetCachedResult(cacheKey, dnsRequestMessage, settings, out var response))
			{
				return response;
			}
			return null;
		}

		private HandleError HandleDnsResponseException(DnsResponseException ex, DnsRequestMessage request, DnsQuerySettings settings, NameServer nameServer, DnsMessageHandleType handleType, bool isLastServer, bool isLastTry, int currentTry)
		{
			HandleError handleError = ((!isLastServer) ? HandleError.RetryNextServer : (settings.ThrowDnsErrors ? HandleError.Throw : HandleError.ReturnResponse));
			if (!settings.ContinueOnDnsError)
			{
				handleError = (settings.ThrowDnsErrors ? HandleError.Throw : HandleError.ReturnResponse);
			}
			else if (!isLastTry && (ex.Code == DnsResponseCode.ServerFailure || ex.Code == DnsResponseCode.FormatError))
			{
				handleError = HandleError.RetryCurrentServer;
			}
			if (_logger.IsEnabled(LogLevel.Information))
			{
				int eventId = 0;
				string text = "Query {0} via {1} => {2} on {3} returned a response error '{4}'.";
				switch (handleError)
				{
				case HandleError.Throw:
					eventId = 90;
					text += " Throwing the error.";
					break;
				case HandleError.ReturnResponse:
					eventId = 11;
					text += " Returning response.";
					break;
				case HandleError.RetryCurrentServer:
					eventId = 21;
					text += " Re-trying {5}/{6}....";
					break;
				case HandleError.RetryNextServer:
					eventId = 20;
					text += " Trying next server.";
					break;
				}
				if (handleError == HandleError.RetryCurrentServer)
				{
					_logger.LogInformation(eventId, text, request.Header.Id, handleType, request.Question, nameServer, ex.DnsError, currentTry + 1, settings.Retries + 1);
				}
				else
				{
					_logger.LogInformation(eventId, text, request.Header.Id, handleType, request.Question, nameServer, ex.DnsError);
				}
			}
			return handleError;
		}

		private HandleError HandleDnsXidMismatchException(DnsXidMismatchException ex, DnsRequestMessage request, DnsQuerySettings settings, DnsMessageHandleType handleType, bool isLastServer, bool isLastTry, int currentTry)
		{
			if (isLastServer && isLastTry)
			{
				_logger.LogError(90, ex, "Query {0} via {1} => {2} xid mismatch {3}. Throwing the error.", ex.RequestXid, handleType, request.Question, ex.ResponseXid);
				return HandleError.Throw;
			}
			if (isLastTry)
			{
				_logger.LogError(20, ex, "Query {0} via {1} => {2} xid mismatch {3}. Trying next server.", ex.RequestXid, handleType, request.Question, ex.ResponseXid);
				return HandleError.RetryNextServer;
			}
			_logger.LogWarning(20, ex, "Query {0} via {1} => {2} xid mismatch {3}. Re-trying {4}/{5}...", ex.RequestXid, handleType, request.Question, ex.ResponseXid, currentTry, settings.Retries + 1);
			return HandleError.RetryCurrentServer;
		}

		private HandleError HandleDnsResponeParseException(DnsResponseParseException ex, DnsRequestMessage request, DnsMessageHandleType handleType, bool isLastServer)
		{
			if (handleType == DnsMessageHandleType.UDP && (ex.ResponseData.Length <= 512 || ex.ReadLength + ex.Index > ex.ResponseData.Length))
			{
				_logger.LogError(91, ex, "Query {0} via {1} => {2} error parsing the response. The response seems to be truncated without TC flag set! Re-trying via TCP anyways.", request.Header.Id, handleType, request.Question);
				return HandleError.ReturnResponse;
			}
			if (isLastServer)
			{
				_logger.LogError(90, ex, "Query {0} via {1} => {2} error parsing the response. Throwing the error.", request.Header.Id, handleType, request.Question);
				return HandleError.Throw;
			}
			_logger.LogWarning(20, ex, "Query {0} via {1} => {2} error parsing the response. Trying next server.", request.Header.Id, handleType, request.Question);
			return HandleError.RetryNextServer;
		}

		private HandleError HandleTimeoutException(Exception ex, DnsRequestMessage request, DnsQuerySettings settings, NameServer nameServer, DnsMessageHandleType handleType, bool isLastServer, bool isLastTry, int currentTry)
		{
			if (isLastTry && isLastServer)
			{
				if (_logger.IsEnabled(LogLevel.Information))
				{
					_logger.LogInformation(90, ex, "Query {0} via {1} => {2} on {3} timed out or is a transient error. Throwing the error.", request.Header.Id, handleType, request.Question, nameServer);
				}
				return HandleError.Throw;
			}
			if (isLastTry)
			{
				if (_logger.IsEnabled(LogLevel.Information))
				{
					_logger.LogInformation(20, ex, "Query {0} via {1} => {2} on {3} timed out or is a transient error. Trying next server", request.Header.Id, handleType, request.Question, nameServer);
				}
				return HandleError.RetryNextServer;
			}
			if (_logger.IsEnabled(LogLevel.Information))
			{
				_logger.LogInformation(21, ex, "Query {0} via {1} => {2} on {3} timed out or is a transient error. Re-trying {4}/{5}...", request.Header.Id, handleType, request.Question, nameServer, currentTry, settings.Retries + 1);
			}
			return HandleError.RetryCurrentServer;
		}

		private HandleError HandleUnhandledException(Exception ex, DnsRequestMessage request, NameServer nameServer, DnsMessageHandleType handleType, bool isLastServer)
		{
			if (_logger.IsEnabled(LogLevel.Warning))
			{
				_logger.LogWarning(isLastServer ? 90 : 20, ex, "Query {0} via {1} => {2} on {3} failed with an error." + (isLastServer ? " Throwing the error." : " Trying next server."), handleType, request.Header.Id, request.Question, nameServer);
			}
			if (isLastServer)
			{
				return HandleError.Throw;
			}
			return HandleError.RetryNextServer;
		}

		private IDnsQueryResponse ProcessResponseMessage(LookupClientAudit audit, DnsRequestMessage request, DnsResponseMessage response, DnsQuerySettings settings, NameServer nameServer, DnsMessageHandleType handleType, int serverCount, bool isLastServer, out HandleError handleError)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			handleError = HandleError.None;
			if (response.Header.ResultTruncated)
			{
				audit?.AuditTruncatedRetryTcp();
				if (_logger.IsEnabled(LogLevel.Information))
				{
					_logger.LogInformation(5, "Query {0} via {1} => {2} was truncated, re-trying with TCP.", request.Header.Id, handleType, request.Question);
				}
				return new TruncatedQueryResponse();
			}
			if (request.Header.Id != response.Header.Id)
			{
				_logger.LogWarning("Request header id {0} does not match response header {1}. This might be due to some non-standard configuration in your network.", request.Header.Id, response.Header.Id);
			}
			audit?.AuditResolveServers(serverCount);
			audit?.AuditResponseHeader(response.Header);
			if (response.Header.ResponseCode != DnsHeaderResponseCode.NoError)
			{
				audit?.AuditResponseError(response.Header.ResponseCode);
			}
			else if (_logger.IsEnabled(LogLevel.Debug))
			{
				_logger.LogDebug(10, "Got {0} answers for query {1} via {2} => {3} from {4}.", response.Answers.Count, request.Header.Id, handleType, request.Question, nameServer);
			}
			HandleOptRecords(settings, audit, nameServer, response);
			DnsQueryResponse dnsQueryResponse = response.AsQueryResponse(nameServer, settings);
			if (!dnsQueryResponse.HasError && !isLastServer && settings.ContinueOnEmptyResponse)
			{
				if (dnsQueryResponse.Answers.Count == 0)
				{
					handleError = HandleError.RetryNextServer;
				}
				else if (request.Question.QuestionType != QueryType.ANY && request.Question.QuestionType != QueryType.AXFR && ((request.Question.QuestionType != QueryType.A && request.Question.QuestionType != QueryType.AAAA) || !dnsQueryResponse.Answers.OfRecordType(ResourceRecordType.CNAME).Any()) && (request.Question.QuestionType != QueryType.NS || !dnsQueryResponse.Authorities.Any()) && !dnsQueryResponse.Answers.OfRecordType((ResourceRecordType)request.Question.QuestionType).Any())
				{
					handleError = HandleError.RetryNextServer;
				}
				if (handleError == HandleError.RetryNextServer && _logger.IsEnabled(LogLevel.Information))
				{
					_logger.LogInformation(12, "Got no answer for query {0} via {1} => {2} from {3}. Trying next server.", request.Header.Id, handleType, request.Question, nameServer);
				}
			}
			return dnsQueryResponse;
		}

		private bool TryGetCachedResult(string cacheKey, DnsRequestMessage request, DnsQuerySettings settings, out IDnsQueryResponse response)
		{
			response = null;
			if (settings.UseCache)
			{
				response = Cache.Get(cacheKey);
				if (response != null)
				{
					if (_logger.IsEnabled(LogLevel.Debug))
					{
						_logger.LogDebug(3, "Got cached result for query {0} => {1}.", request.Header.Id, request.Question);
					}
					if (settings.EnableAuditTrail)
					{
						LookupClientAudit lookupClientAudit = new LookupClientAudit(settings);
						lookupClientAudit.AuditCachedItem(response);
						lookupClientAudit.Build(response);
					}
					return true;
				}
			}
			return false;
		}

		private void HandleOptRecords(DnsQuerySettings settings, LookupClientAudit audit, NameServer serverInfo, DnsResponseMessage response)
		{
			if (!settings.UseExtendedDns)
			{
				return;
			}
			DnsResourceRecord dnsResourceRecord = response.Additionals.OfRecordType(ResourceRecordType.OPT).FirstOrDefault();
			if (dnsResourceRecord == null)
			{
				if (_logger.IsEnabled(LogLevel.Information))
				{
					_logger.LogInformation(80, "Response {0} => {1} is missing the requested OPT record.", response.Header.Id, response.Questions.FirstOrDefault());
				}
			}
			else if (dnsResourceRecord is OptRecord optRecord)
			{
				audit?.AuditOptPseudo();
				serverInfo.SupportedUdpPayloadSize = optRecord.UdpSize;
				audit?.AuditEdnsOpt(optRecord.UdpSize, optRecord.Version, optRecord.IsDnsSecOk, optRecord.ResponseCodeEx);
				if (_logger.IsEnabled(LogLevel.Debug))
				{
					_logger.LogDebug(31, "Response {0} => {1} opt record sets buffer of {2} to {3}.", response.Header.Id, response.Questions.FirstOrDefault(), serverInfo, optRecord.UdpSize);
				}
			}
		}

		public static DnsQuestion GetReverseQuestion(IPAddress ipAddress)
		{
			if (ipAddress == null)
			{
				throw new ArgumentNullException("ipAddress");
			}
			return new DnsQuestion(ipAddress.GetArpaName(), QueryType.PTR);
		}
	}
	internal class LookupClientAudit
	{
		private static readonly int s_printOffset = -32;

		private readonly StringBuilder _auditWriter = new StringBuilder();

		private Stopwatch _swatch;

		public DnsQuerySettings Settings { get; }

		public LookupClientAudit(DnsQuerySettings settings)
		{
			Settings = settings ?? throw new ArgumentNullException("settings");
		}

		public void AuditCachedItem(IDnsQueryResponse response)
		{
			if (Settings.EnableAuditTrail)
			{
				StartTimer();
				_auditWriter.AppendLine("; (cached result)");
				AuditResponseHeader(response.Header);
				AuditOptPseudo();
				DnsResourceRecord dnsResourceRecord = response.Additionals.OfRecordType(ResourceRecordType.OPT).FirstOrDefault();
				if (dnsResourceRecord != null && dnsResourceRecord is OptRecord optRecord)
				{
					AuditEdnsOpt(optRecord.UdpSize, optRecord.Version, optRecord.IsDnsSecOk, optRecord.ResponseCodeEx);
				}
				AuditEnd(response, response.NameServer);
			}
		}

		public void StartTimer()
		{
			if (Settings.EnableAuditTrail)
			{
				_swatch = Stopwatch.StartNew();
				_swatch.Restart();
			}
		}

		public void AuditResolveServers(int count)
		{
			if (Settings.EnableAuditTrail)
			{
				_auditWriter.AppendLine($"; ({count} server found)");
			}
		}

		public string Build(IDnsQueryResponse response = null)
		{
			if (!Settings.EnableAuditTrail)
			{
				return string.Empty;
			}
			string text = _auditWriter.ToString();
			if (response != null)
			{
				DnsQueryResponse.SetAuditTrail(response, text);
			}
			return text;
		}

		public void AuditTruncatedRetryTcp()
		{
			if (Settings.EnableAuditTrail)
			{
				_auditWriter.AppendLine(";; Truncated, retrying in TCP mode.");
				_auditWriter.AppendLine();
			}
		}

		public void AuditResponseError(DnsHeaderResponseCode responseCode)
		{
			if (Settings.EnableAuditTrail)
			{
				_auditWriter.AppendLine(";; ERROR: " + DnsResponseCodeText.GetErrorText((DnsResponseCode)responseCode));
			}
		}

		public void AuditOptPseudo()
		{
			if (Settings.EnableAuditTrail)
			{
				_auditWriter.AppendLine(";; OPT PSEUDOSECTION:");
			}
		}

		public void AuditResponseHeader(DnsResponseHeader header)
		{
			if (Settings.EnableAuditTrail)
			{
				_auditWriter.AppendLine(";; Got answer:");
				_auditWriter.AppendLine(header.ToString());
				if (header.RecursionDesired && !header.RecursionAvailable)
				{
					_auditWriter.AppendLine(";; WARNING: recursion requested but not available");
				}
				_auditWriter.AppendLine();
			}
		}

		public void AuditEdnsOpt(short udpSize, byte version, bool doFlag, DnsResponseCode responseCode)
		{
			if (Settings.EnableAuditTrail)
			{
				_auditWriter.AppendLine(string.Format("; EDNS: version: {0}, flags:{1}; UDP: {2}; code: {3}", version, doFlag ? " do" : string.Empty, udpSize, responseCode));
			}
		}

		public void AuditEnd(IDnsQueryResponse queryResponse, NameServer nameServer)
		{
			if (queryResponse == null)
			{
				throw new ArgumentNullException("queryResponse");
			}
			if (nameServer == null)
			{
				throw new ArgumentNullException("nameServer");
			}
			if (!Settings.EnableAuditTrail)
			{
				return;
			}
			long elapsedMilliseconds = _swatch.ElapsedMilliseconds;
			if (queryResponse != null)
			{
				if (queryResponse.Questions.Count > 0)
				{
					_auditWriter.AppendLine(";; QUESTION SECTION:");
					foreach (DnsQuestion question in queryResponse.Questions)
					{
						_auditWriter.AppendLine(question.ToString(s_printOffset));
					}
					_auditWriter.AppendLine();
				}
				if (queryResponse.Answers.Count > 0)
				{
					_auditWriter.AppendLine(";; ANSWER SECTION:");
					foreach (DnsResourceRecord answer in queryResponse.Answers)
					{
						_auditWriter.AppendLine(answer.ToString(s_printOffset));
					}
					_auditWriter.AppendLine();
				}
				if (queryResponse.Authorities.Count > 0)
				{
					_auditWriter.AppendLine(";; AUTHORITIES SECTION:");
					foreach (DnsResourceRecord authority in queryResponse.Authorities)
					{
						_auditWriter.AppendLine(authority.ToString(s_printOffset));
					}
					_auditWriter.AppendLine();
				}
				DnsResourceRecord[] array = queryResponse.Additionals.Where((DnsResourceRecord p) => !(p is OptRecord)).ToArray();
				if (array.Length != 0)
				{
					_auditWriter.AppendLine(";; ADDITIONALS SECTION:");
					DnsResourceRecord[] array2 = array;
					foreach (DnsResourceRecord dnsResourceRecord in array2)
					{
						_auditWriter.AppendLine(dnsResourceRecord.ToString(s_printOffset));
					}
					_auditWriter.AppendLine();
				}
			}
			_auditWriter.AppendLine($";; Query time: {elapsedMilliseconds} msec");
			_auditWriter.AppendLine($";; SERVER: {nameServer.Address}#{nameServer.Port}");
			_auditWriter.AppendLine(";; WHEN: " + DateTime.UtcNow.ToString("ddd MMM dd HH:mm:ss K yyyy", CultureInfo.InvariantCulture));
			_auditWriter.AppendLine($";; MSG SIZE  rcvd: {queryResponse.MessageSize}");
		}

		public void AuditException(Exception ex)
		{
			if (Settings.EnableAuditTrail)
			{
				if (ex is DnsResponseException ex2)
				{
					_auditWriter.AppendLine(";; Error: " + DnsResponseCodeText.GetErrorText(ex2.Code) + " " + (ex2.InnerException?.Message ?? ex2.Message));
				}
				else if (ex is AggregateException ex3)
				{
					_auditWriter.AppendLine(";; Error: " + (ex3.InnerException?.Message ?? ex3.Message));
				}
				else
				{
					_auditWriter.AppendLine(";; Error: " + ex.Message);
				}
				if (Debugger.IsAttached)
				{
					_auditWriter.AppendLine(ex.ToString());
				}
			}
		}

		public void AuditRetryNextServer()
		{
			if (Settings.EnableAuditTrail)
			{
				_auditWriter.AppendLine();
				_auditWriter.AppendLine("; Trying next server.");
			}
		}
	}
	public class NameServer : IEquatable<NameServer>
	{
		public const int DefaultPort = 53;

		public static readonly IPEndPoint GooglePublicDns = new IPEndPoint(IPAddress.Parse("8.8.4.4"), 53);

		public static readonly IPEndPoint GooglePublicDns2 = new IPEndPoint(IPAddress.Parse("8.8.8.8"), 53);

		public static readonly IPEndPoint GooglePublicDnsIPv6 = new IPEndPoint(IPAddress.Parse("2001:4860:4860::8844"), 53);

		public static readonly IPEndPoint GooglePublicDns2IPv6 = new IPEndPoint(IPAddress.Parse("2001:4860:4860::8888"), 53);

		public static readonly IPEndPoint Cloudflare = new IPEndPoint(IPAddress.Parse("1.1.1.1"), 53);

		public static readonly IPEndPoint Cloudflare2 = new IPEndPoint(IPAddress.Parse("1.0.0.1"), 53);

		public static readonly IPEndPoint CloudflareIPv6 = new IPEndPoint(IPAddress.Parse("2606:4700:4700::1111"), 53);

		public static readonly IPEndPoint Cloudflare2IPv6 = new IPEndPoint(IPAddress.Parse("2606:4700:4700::1001"), 53);

		internal const string EtcResolvConfFile = "/etc/resolv.conf";

		public string Address => IPEndPoint.Address.ToString();

		public int Port => IPEndPoint.Port;

		public AddressFamily AddressFamily => IPEndPoint.AddressFamily;

		public int? SupportedUdpPayloadSize { get; internal set; }

		internal IPEndPoint IPEndPoint { get; }

		public string DnsSuffix { get; }

		public NameServer(IPAddress endPoint)
			: this(new IPEndPoint(endPoint, 53))
		{
		}

		public NameServer(IPAddress endPoint, int port)
			: this(new IPEndPoint(endPoint, port))
		{
		}

		public NameServer(IPEndPoint endPoint)
		{
			IPEndPoint = endPoint ?? throw new ArgumentNullException("endPoint");
		}

		public NameServer(IPAddress endPoint, string dnsSuffix)
			: this(new IPEndPoint(endPoint, 53), dnsSuffix)
		{
		}

		public NameServer(IPAddress endPoint, int port, string dnsSuffix)
			: this(new IPEndPoint(endPoint, port), dnsSuffix)
		{
		}

		public NameServer(IPEndPoint endPoint, string dnsSuffix)
			: this(endPoint)
		{
			DnsSuffix = (string.IsNullOrWhiteSpace(dnsSuffix) ? null : dnsSuffix);
		}

		public static implicit operator NameServer(IPEndPoint endPoint)
		{
			if (endPoint == null)
			{
				return null;
			}
			return new NameServer(endPoint);
		}

		public static implicit operator NameServer(IPAddress address)
		{
			if (address == null)
			{
				return null;
			}
			return new NameServer(address);
		}

		internal static NameServer[] Convert(IReadOnlyCollection<IPAddress> addresses)
		{
			return addresses?.Select((Func<IPAddress, NameServer>)((IPAddress p) => p)).ToArray();
		}

		internal static NameServer[] Convert(IReadOnlyCollection<IPEndPoint> addresses)
		{
			return addresses?.Select((Func<IPEndPoint, NameServer>)((IPEndPoint p) => p)).ToArray();
		}

		public override string ToString()
		{
			return IPEndPoint.ToString();
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as NameServer);
		}

		public bool Equals(NameServer other)
		{
			if (other != null)
			{
				return EqualityComparer<IPEndPoint>.Default.Equals(IPEndPoint, other.IPEndPoint);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return EqualityComparer<IPEndPoint>.Default.GetHashCode(IPEndPoint);
		}

		public static IReadOnlyCollection<NameServer> ResolveNameServers(bool skipIPv6SiteLocal = true, bool fallbackToGooglePublicDns = true)
		{
			IReadOnlyCollection<NameServer> readOnlyCollection = (IReadOnlyCollection<NameServer>)(object)new NameServer[0];
			List<Exception> list = new List<Exception>();
			ILogger logger = Logging.LoggerFactory?.CreateLogger(typeof(NameServer).FullName);
			logger?.LogDebug("Starting to resolve NameServers, skipIPv6SiteLocal:{0}.", skipIPv6SiteLocal);
			try
			{
				readOnlyCollection = QueryNetworkInterfaces();
			}
			catch (Exception ex)
			{
				logger?.LogWarning(ex, "Resolving name servers using .NET framework failed.");
				list.Add(ex);
			}
			if (list.Count > 0)
			{
				logger?.LogDebug("Using native path to resolve servers.");
				try
				{
					readOnlyCollection = ResolveNameServersNative();
					list.Clear();
				}
				catch (Exception ex2)
				{
					logger?.LogWarning(ex2, "Resolving name servers using native implementation failed.");
					list.Add(ex2);
				}
			}
			try
			{
				IReadOnlyCollection<NameServer> readOnlyCollection2 = ResolveNameResolutionPolicyServers();
				if (readOnlyCollection2.Count != 0)
				{
					HashSet<NameServer> hashSet = new HashSet<NameServer>();
					foreach (NameServer item in readOnlyCollection2)
					{
						hashSet.Add(item);
					}
					foreach (NameServer item2 in readOnlyCollection)
					{
						hashSet.Add(item2);
					}
					readOnlyCollection = hashSet;
				}
			}
			catch (Exception exception)
			{
				logger?.LogInformation(exception, "Resolving name servers from NRPT failed.");
			}
			IReadOnlyCollection<NameServer> readOnlyCollection3 = (IReadOnlyCollection<NameServer>)(object)readOnlyCollection.Where((NameServer p) => (p.IPEndPoint.Address.AddressFamily == AddressFamily.InterNetwork || p.IPEndPoint.Address.AddressFamily == AddressFamily.InterNetworkV6) && (!p.IPEndPoint.Address.IsIPv6SiteLocal || !skipIPv6SiteLocal)).ToArray();
			try
			{
				readOnlyCollection3 = ValidateNameServers(readOnlyCollection3, logger);
			}
			catch (Exception ex3)
			{
				logger?.LogWarning(ex3, "NameServer validation failed.");
				list.Add(ex3);
			}
			if (readOnlyCollection3.Count == 0)
			{
				if (!fallbackToGooglePublicDns && list.Count > 0)
				{
					throw new InvalidOperationException("Could not resolve any NameServers.", list.First());
				}
				if (fallbackToGooglePublicDns)
				{
					logger?.LogWarning("Could not resolve any NameServers, falling back to Google public servers.");
					return (IReadOnlyCollection<NameServer>)(object)new NameServer[4] { GooglePublicDns, GooglePublicDns2, GooglePublicDnsIPv6, GooglePublicDns2IPv6 };
				}
			}
			logger?.LogDebug("Resolved {0} name servers: [{1}].", readOnlyCollection3.Count, string.Join(",", readOnlyCollection3.AsEnumerable()));
			return readOnlyCollection3;
		}

		public static IReadOnlyCollection<NameServer> ResolveNameServersNative()
		{
			List<NameServer> list = new List<NameServer>();
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				try
				{
					FixedNetworkInformation fixedInformation = FixedNetworkInformation.GetFixedInformation();
					foreach (IPAddress dnsAddress in fixedInformation.DnsAddresses)
					{
						list.Add(new NameServer(dnsAddress, 53, fixedInformation.DomainName));
					}
				}
				catch
				{
				}
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				try
				{
					list = StringParsingHelpers.ParseDnsAddressesFromResolvConfFile("/etc/resolv.conf");
				}
				catch (Exception ex) when (ex is FileNotFoundException || ex is UnauthorizedAccessException)
				{
				}
			}
			return list;
		}

		public static IReadOnlyCollection<NameServer> ResolveNameResolutionPolicyServers()
		{
			return NameResolutionPolicy.Resolve();
		}

		internal static IReadOnlyCollection<NameServer> ValidateNameServers(IReadOnlyCollection<NameServer> servers, ILogger logger = null)
		{
			NameServer[] array = servers.Where((NameServer p) => !p.IPEndPoint.Address.Equals(IPAddress.Any) && !p.IPEndPoint.Address.Equals(IPAddress.IPv6Any)).ToArray();
			if (array.Length != servers.Count)
			{
				logger?.LogWarning("Unsupported ANY address cannot be used as name server.");
				if (array.Length == 0)
				{
					throw new InvalidOperationException("Unsupported ANY address cannot be used as name server and no other servers are configured to fall back to.");
				}
			}
			return (IReadOnlyCollection<NameServer>)(object)array;
		}

		private static IReadOnlyCollection<NameServer> QueryNetworkInterfaces()
		{
			HashSet<NameServer> hashSet = new HashSet<NameServer>();
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			if (allNetworkInterfaces == null)
			{
				return (IReadOnlyCollection<NameServer>)(object)hashSet.ToArray();
			}
			foreach (NetworkInterface item in allNetworkInterfaces.Where((NetworkInterface p) => p != null && (p.OperationalStatus == OperationalStatus.Up || p.OperationalStatus == OperationalStatus.Unknown) && p.NetworkInterfaceType != NetworkInterfaceType.Loopback))
			{
				IPInterfaceProperties iPInterfaceProperties = item?.GetIPProperties();
				if (iPInterfaceProperties?.DnsAddresses == null)
				{
					continue;
				}
				foreach (IPAddress dnsAddress in iPInterfaceProperties.DnsAddresses)
				{
					hashSet.Add(new NameServer(dnsAddress, 53, iPInterfaceProperties.DnsSuffix));
				}
			}
			return (IReadOnlyCollection<NameServer>)(object)hashSet.ToArray();
		}
	}
	public enum QueryClass : short
	{
		IN = 1,
		CS,
		CH,
		HS
	}
	public enum QueryType
	{
		A = 1,
		NS = 2,
		[Obsolete("Use MX")]
		MD = 3,
		[Obsolete("Use MX")]
		MF = 4,
		CNAME = 5,
		SOA = 6,
		MB = 7,
		MG = 8,
		MR = 9,
		NULL = 10,
		WKS = 11,
		PTR = 12,
		HINFO = 13,
		MINFO = 14,
		MX = 15,
		TXT = 16,
		RP = 17,
		AFSDB = 18,
		AAAA = 28,
		SRV = 33,
		NAPTR = 35,
		DS = 43,
		RRSIG = 46,
		NSEC = 47,
		DNSKEY = 48,
		NSEC3 = 50,
		NSEC3PARAM = 51,
		TLSA = 52,
		SPF = 99,
		AXFR = 252,
		ANY = 255,
		URI = 256,
		CAA = 257,
		SSHFP = 44
	}
	internal class ResponseCache
	{
		private class ResponseEntry
		{
			public DateTimeOffset ExpiresAt { get; }

			public DateTimeOffset Created { get; }

			public double TTL { get; set; }

			public IDnsQueryResponse Response { get; }

			public bool IsExpiredFor(DateTimeOffset forDate)
			{
				return forDate >= ExpiresAt;
			}

			public ResponseEntry(IDnsQueryResponse response, double ttlInMS)
			{
				Response = response;
				TTL = ttlInMS;
				Created = DateTimeOffset.UtcNow;
				ExpiresAt = Created.AddMilliseconds(TTL);
			}
		}

		private static readonly TimeSpan s_infiniteTimeout = Timeout.InfiniteTimeSpan;

		private static readonly TimeSpan s_maxTimeout = TimeSpan.FromMilliseconds(2147483647.0);

		private static readonly TimeSpan s_defaultFailureTimeout = TimeSpan.FromSeconds(5.0);

		private static readonly int s_cleanupInterval = (int)TimeSpan.FromMinutes(10.0).TotalMilliseconds;

		private readonly ConcurrentDictionary<string, ResponseEntry> _cache = new ConcurrentDictionary<string, ResponseEntry>();

		private readonly object _cleanupLock = new object();

		private bool _cleanupRunning;

		private int _lastCleanup;

		private TimeSpan? _minimumTimeout;

		private TimeSpan? _maximumTimeout;

		private TimeSpan _failureEntryTimeout = s_defaultFailureTimeout;

		public int Count => _cache.Count;

		public bool Enabled { get; set; } = true;

		public TimeSpan? MinimumTimout
		{
			get
			{
				return _minimumTimeout;
			}
			set
			{
				if (value.HasValue && (value < TimeSpan.Zero || value > s_maxTimeout) && value != s_infiniteTimeout)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_minimumTimeout = value;
			}
		}

		public TimeSpan? MaximumTimeout
		{
			get
			{
				return _maximumTimeout;
			}
			set
			{
				if (value.HasValue && (value < TimeSpan.Zero || value > s_maxTimeout) && value != s_infiniteTimeout)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_maximumTimeout = value;
			}
		}

		public TimeSpan FailureEntryTimeout
		{
			get
			{
				return _failureEntryTimeout;
			}
			set
			{
				if ((value < TimeSpan.Zero || value > s_maxTimeout) && value != s_infiniteTimeout)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_failureEntryTimeout = value;
			}
		}

		public ResponseCache(bool enabled = true, TimeSpan? minimumTimout = null, TimeSpan? maximumTimeout = null, TimeSpan? failureEntryTimeout = null)
		{
			Enabled = enabled;
			MinimumTimout = minimumTimout;
			MaximumTimeout = maximumTimeout;
			if (failureEntryTimeout.HasValue)
			{
				FailureEntryTimeout = failureEntryTimeout.Value;
			}
		}

		public static string GetCacheKey(DnsQuestion question)
		{
			if (question == null)
			{
				throw new ArgumentNullException("question");
			}
			return question.QueryName.Value + ":" + (short)question.QuestionClass + ":" + (short)question.QuestionType;
		}

		public IDnsQueryResponse Get(string key)
		{
			double? effectiveTtl;
			return Get(key, out effectiveTtl);
		}

		public IDnsQueryResponse Get(string key, out double? effectiveTtl)
		{
			effectiveTtl = null;
			if (key == null)
			{
				throw new ArgumentNullException(key);
			}
			if (!Enabled)
			{
				return null;
			}
			if (_cache.TryGetValue(key, out var value))
			{
				effectiveTtl = value.TTL;
				if (!value.IsExpiredFor(DateTimeOffset.UtcNow))
				{
					StartCleanup();
					return value.Response;
				}
				_cache.TryRemove(key, out var _);
			}
			return null;
		}

		public bool Add(string key, IDnsQueryResponse response, bool cacheFailures = false)
		{
			if (key == null)
			{
				throw new ArgumentNullException(key);
			}
			if (Enabled && response != null && (cacheFailures || (!response.HasError && response.Answers.Count > 0)))
			{
				if (response.Answers.Count == 0)
				{
					ResponseEntry value = new ResponseEntry(response, FailureEntryTimeout.TotalMilliseconds);
					StartCleanup();
					return _cache.TryAdd(key, value);
				}
				IEnumerable<DnsResourceRecord> source = response.AllRecords.Where((DnsResourceRecord p) => !(p is OptRecord));
				if (source.Any())
				{
					double num = (double)source.Min((DnsResourceRecord p) => p.InitialTimeToLive) * 1000.0;
					if (MinimumTimout == Timeout.InfiniteTimeSpan)
					{
						num = s_maxTimeout.TotalMilliseconds;
					}
					else if (MinimumTimout.HasValue && num < MinimumTimout.Value.TotalMilliseconds)
					{
						num = MinimumTimout.Value.TotalMilliseconds;
					}
					if (MaximumTimeout.HasValue && MaximumTimeout != Timeout.InfiniteTimeSpan && num > MaximumTimeout.Value.TotalMilliseconds)
					{
						num = MaximumTimeout.Value.TotalMilliseconds;
					}
					if (num < 1.0)
					{
						return false;
					}
					ResponseEntry value2 = new ResponseEntry(response, num);
					StartCleanup();
					return _cache.TryAdd(key, value2);
				}
			}
			StartCleanup();
			return false;
		}

		private static void DoCleanup(ResponseCache cache)
		{
			cache._cleanupRunning = true;
			DateTimeOffset utcNow = DateTimeOffset.UtcNow;
			foreach (KeyValuePair<string, ResponseEntry> item in cache._cache)
			{
				if (item.Value.IsExpiredFor(utcNow))
				{
					cache._cache.TryRemove(item.Key, out var _);
				}
			}
			cache._cleanupRunning = false;
		}

		private void StartCleanup()
		{
			if (!Enabled)
			{
				return;
			}
			int num = Environment.TickCount & 0x7FFFFFFF;
			if (_lastCleanup + s_cleanupInterval < 0 || num + s_cleanupInterval < 0)
			{
				_lastCleanup = 0;
			}
			if (_cleanupRunning || _lastCleanup + s_cleanupInterval >= num)
			{
				return;
			}
			lock (_cleanupLock)
			{
				if (!_cleanupRunning && _lastCleanup + s_cleanupInterval < num)
				{
					_lastCleanup = num;
					Task.Factory.StartNew(delegate(object state)
					{
						DoCleanup((ResponseCache)state);
					}, this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
				}
			}
		}
	}
	public static class Tracing
	{
		internal class TraceLoggerFactory : ILoggerFactory
		{
			private class TraceLogger : ILogger
			{
				private readonly string _name;

				public TraceLogger(string name)
				{
					_name = name ?? throw new ArgumentNullException("name");
				}

				public bool IsEnabled(LogLevel logLevel)
				{
					return Source.Switch.ShouldTrace(GetTraceEventType(logLevel));
				}

				public void Log(LogLevel logLevel, int eventId, Exception exception, string message, params object[] args)
				{
					string text = "[" + _name + "] ";
					if (message != null)
					{
						text += string.Format(message, args);
					}
					if (exception != null)
					{
						text = text + Environment.NewLine + exception;
					}
					Source.TraceEvent(GetTraceEventType(logLevel), eventId, text);
				}

				private static TraceEventType GetTraceEventType(LogLevel logLevel)
				{
					switch (logLevel)
					{
					case LogLevel.Critical:
						return TraceEventType.Critical;
					case LogLevel.Error:
						return TraceEventType.Error;
					case LogLevel.Warning:
						return TraceEventType.Warning;
					case LogLevel.Information:
						return TraceEventType.Information;
					case LogLevel.Trace:
					case LogLevel.Debug:
						return TraceEventType.Verbose;
					default:
						return (TraceEventType)0;
					}
				}
			}

			public ILogger CreateLogger(string categoryName)
			{
				return new TraceLogger(categoryName);
			}
		}

		public static TraceSource Source { get; } = new TraceSource("DnsClient", SourceLevels.Error);
	}
	public static class Logging
	{
		public static ILoggerFactory LoggerFactory { get; set; } = new Tracing.TraceLoggerFactory();
	}
}
namespace DnsClient.Windows
{
	internal static class NameResolutionPolicy
	{
		private static readonly char[] s_splitOn = new char[1] { ';' };

		internal static IReadOnlyCollection<NameServer> Resolve(bool includeGenericServers = true, bool includeDirectAccessServers = true)
		{
			HashSet<NameServer> hashSet = new HashSet<NameServer>();
			if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				return hashSet;
			}
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Policies\\Microsoft\\Windows NT\\DNSClient\\DnsPolicyConfig\\");
			RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Dnscache\\Parameters\\DnsPolicyConfig\\");
			try
			{
				HashSet<string> hashSet2 = new HashSet<string>();
				if (registryKey != null)
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					foreach (string item in subKeyNames)
					{
						hashSet2.Add(item);
					}
				}
				if (registryKey2 != null)
				{
					string[] subKeyNames = registryKey2.GetSubKeyNames();
					foreach (string item2 in subKeyNames)
					{
						hashSet2.Add(item2);
					}
				}
				foreach (string item3 in hashSet2)
				{
					RegistryKey registryKey3 = registryKey?.OpenSubKey(item3) ?? registryKey2?.OpenSubKey(item3);
					if (registryKey3 == null)
					{
						continue;
					}
					using (registryKey3)
					{
						string[] names = registryKey3.GetValue("Name") as string[];
						string dnsServers = registryKey3.GetValue("GenericDNSServers")?.ToString();
						string dnsServers2 = registryKey3.GetValue("DirectAccessDNSServers")?.ToString();
						if (includeGenericServers)
						{
							AddServers(hashSet, names, dnsServers);
						}
						if (includeDirectAccessServers)
						{
							AddServers(hashSet, names, dnsServers2);
						}
					}
				}
			}
			finally
			{
				registryKey?.Dispose();
				registryKey2?.Dispose();
			}
			return (IReadOnlyCollection<NameServer>)(object)hashSet.ToArray();
		}

		private static void AddServers(HashSet<NameServer> nameServers, string[] names, string dnsServers)
		{
			if (string.IsNullOrWhiteSpace(dnsServers))
			{
				return;
			}
			string[] array = dnsServers.Split(s_splitOn, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				if (!IPAddress.TryParse(array[i], out var address) || (address.AddressFamily != AddressFamily.InterNetwork && address.AddressFamily != AddressFamily.InterNetworkV6) || names == null)
				{
					continue;
				}
				foreach (string item in names.Where((string n) => n.StartsWith(".")).Distinct())
				{
					nameServers.Add(new NameServer(address, item));
				}
			}
		}
	}
}
namespace DnsClient.Windows.IpHlpApi
{
	internal class FixedNetworkInformation
	{
		public ICollection<IPAddress> DnsAddresses { get; private set; }

		public string DomainName { get; private set; }

		public string HostName { get; private set; }

		private FixedNetworkInformation()
		{
		}

		public static FixedNetworkInformation GetFixedInformation()
		{
			FixedNetworkInformation fixedNetworkInformation = new FixedNetworkInformation();
			uint pOutBufLen = 0u;
			uint networkParams = global::Interop.IpHlpApi.GetNetworkParams(IntPtr.Zero, ref pOutBufLen);
			if (networkParams == 111)
			{
				using DisposableIntPtr disposableIntPtr = DisposableIntPtr.Alloc((int)pOutBufLen);
				if (disposableIntPtr.IsValid)
				{
					networkParams = global::Interop.IpHlpApi.GetNetworkParams(disposableIntPtr.Ptr, ref pOutBufLen);
					if (networkParams == 0)
					{
						global::Interop.IpHlpApi.FIXED_INFO fIXED_INFO = Marshal.PtrToStructure<global::Interop.IpHlpApi.FIXED_INFO>(disposableIntPtr.Ptr);
						List<IPAddress> list = new List<IPAddress>();
						global::Interop.IpHlpApi.IP_ADDR_STRING iP_ADDR_STRING = fIXED_INFO.DnsServerList;
						if (IPAddress.TryParse(iP_ADDR_STRING.IpAddress, out var address))
						{
							list.Add(address);
							while (iP_ADDR_STRING.Next != IntPtr.Zero)
							{
								iP_ADDR_STRING = Marshal.PtrToStructure<global::Interop.IpHlpApi.IP_ADDR_STRING>(iP_ADDR_STRING.Next);
								if (IPAddress.TryParse(iP_ADDR_STRING.IpAddress, out address))
								{
									list.Add(address);
								}
							}
						}
						fixedNetworkInformation.HostName = fIXED_INFO.hostName;
						fixedNetworkInformation.DomainName = fIXED_INFO.domainName;
						fixedNetworkInformation.DnsAddresses = list.ToArray();
						return fixedNetworkInformation;
					}
					throw new Win32Exception((int)networkParams);
				}
				throw new OutOfMemoryException();
			}
			return fixedNetworkInformation;
		}
	}
}
namespace DnsClient.Linux
{
	internal static class StringParsingHelpers
	{
		internal static string ParseDnsSuffixFromResolvConfFile(string filePath)
		{
			string buffer = File.ReadAllText(filePath);
			if (!new RowConfigReader(buffer).TryGetNextValue("search", out var value))
			{
				return string.Empty;
			}
			return value;
		}

		internal static List<NameServer> ParseDnsAddressesFromResolvConfFile(string filePath)
		{
			string buffer = File.ReadAllText(filePath);
			RowConfigReader rowConfigReader = new RowConfigReader(buffer);
			List<NameServer> list = new List<NameServer>();
			string value;
			while (rowConfigReader.TryGetNextValue("nameserver", out value))
			{
				if (IPAddress.TryParse(value, out var address))
				{
					list.Add(address);
				}
			}
			return list;
		}
	}
}
namespace DnsClient.Internal
{
	public static class Base32Hex
	{
		public static byte[] FromBase32HexString(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (input.Length == 0)
			{
				return new byte[0];
			}
			input = input.TrimEnd('=');
			int num = input.Length * 5 / 8;
			byte[] array = new byte[num];
			byte b = 0;
			byte b2 = 8;
			int num2 = 0;
			foreach (int item in input.Select(CharToValue))
			{
				if (b2 > 5)
				{
					int num3 = item << b2 - 5;
					b = (byte)(b | num3);
					b2 -= 5;
				}
				else
				{
					int num3 = item >> 5 - b2;
					b = (byte)(b | num3);
					array[num2++] = b;
					b = (byte)(item << 3 + b2);
					b2 += 3;
				}
			}
			if (num2 != num)
			{
				array[num2] = b;
			}
			return array;
		}

		public static string ToBase32HexString(byte[] input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (input.Length == 0)
			{
				return string.Empty;
			}
			int num = (int)Math.Ceiling((double)input.Length / 5.0) * 8;
			char[] array = new char[num];
			byte b = 0;
			byte b2 = 5;
			int num2 = 0;
			foreach (byte b3 in input)
			{
				b = (byte)(b | (b3 >> 8 - b2));
				array[num2++] = ValueToChar(b);
				if (b2 < 4)
				{
					b = (byte)((b3 >> 3 - b2) & 0x1F);
					array[num2++] = ValueToChar(b);
					b2 += 5;
				}
				b2 -= 3;
				b = (byte)((b3 << (int)b2) & 0x1F);
			}
			if (num2 == num)
			{
				return new string(array);
			}
			array[num2++] = ValueToChar(b);
			while (num2 != num)
			{
				array[num2++] = '=';
			}
			return new string(array);
		}

		private static int CharToValue(char c)
		{
			char c2 = c;
			if (c2 <= ':' && c2 >= '0')
			{
				return c2 - 48;
			}
			if (c2 <= 'V' && c2 >= 'A')
			{
				return c2 - 55;
			}
			throw new ArgumentException("Character is not a Base32 character.", "c");
		}

		private static char ValueToChar(byte b)
		{
			if (b < 10)
			{
				return (char)(b + 48);
			}
			if (b <= 32)
			{
				return (char)(b + 55);
			}
			throw new ArgumentException("Byte is not a value Base32 value.", "b");
		}
	}
	public interface ILogger
	{
		void Log(LogLevel logLevel, int eventId, Exception exception, string message, params object[] args);

		bool IsEnabled(LogLevel logLevel);
	}
	public interface ILoggerFactory
	{
		ILogger CreateLogger(string categoryName);
	}
	public static class LoggerExtensions
	{
		public static void LogDebug(this ILogger logger, int eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Debug, eventId, exception, message, args);
		}

		public static void LogDebug(this ILogger logger, int eventId, string message, params object[] args)
		{
			logger.Log(LogLevel.Debug, eventId, message, args);
		}

		public static void LogDebug(this ILogger logger, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Debug, exception, message, args);
		}

		public static void LogDebug(this ILogger logger, string message, params object[] args)
		{
			logger.Log(LogLevel.Debug, message, args);
		}

		public static void LogTrace(this ILogger logger, int eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Trace, eventId, exception, message, args);
		}

		public static void LogTrace(this ILogger logger, int eventId, string message, params object[] args)
		{
			logger.Log(LogLevel.Trace, eventId, message, args);
		}

		public static void LogTrace(this ILogger logger, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Trace, exception, message, args);
		}

		public static void LogTrace(this ILogger logger, string message, params object[] args)
		{
			logger.Log(LogLevel.Trace, message, args);
		}

		public static void LogInformation(this ILogger logger, int eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Information, eventId, exception, message, args);
		}

		public static void LogInformation(this ILogger logger, int eventId, string message, params object[] args)
		{
			logger.Log(LogLevel.Information, eventId, message, args);
		}

		public static void LogInformation(this ILogger logger, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Information, exception, message, args);
		}

		public static void LogInformation(this ILogger logger, string message, params object[] args)
		{
			logger.Log(LogLevel.Information, message, args);
		}

		public static void LogWarning(this ILogger logger, int eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Warning, eventId, exception, message, args);
		}

		public static void LogWarning(this ILogger logger, int eventId, string message, params object[] args)
		{
			logger.Log(LogLevel.Warning, eventId, message, args);
		}

		public static void LogWarning(this ILogger logger, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Warning, exception, message, args);
		}

		public static void LogWarning(this ILogger logger, string message, params object[] args)
		{
			logger.Log(LogLevel.Warning, message, args);
		}

		public static void LogError(this ILogger logger, int eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Error, eventId, exception, message, args);
		}

		public static void LogError(this ILogger logger, int eventId, string message, params object[] args)
		{
			logger.Log(LogLevel.Error, eventId, message, args);
		}

		public static void LogError(this ILogger logger, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Error, exception, message, args);
		}

		public static void LogError(this ILogger logger, string message, params object[] args)
		{
			logger.Log(LogLevel.Error, message, args);
		}

		public static void LogCritical(this ILogger logger, int eventId, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Critical, eventId, exception, message, args);
		}

		public static void LogCritical(this ILogger logger, int eventId, string message, params object[] args)
		{
			logger.Log(LogLevel.Critical, eventId, message, args);
		}

		public static void LogCritical(this ILogger logger, Exception exception, string message, params object[] args)
		{
			logger.Log(LogLevel.Critical, exception, message, args);
		}

		public static void LogCritical(this ILogger logger, string message, params object[] args)
		{
			logger.Log(LogLevel.Critical, message, args);
		}

		public static void Log(this ILogger logger, LogLevel logLevel, string message, params object[] args)
		{
			logger.Log(logLevel, 0, null, message, args);
		}

		public static void Log(this ILogger logger, LogLevel logLevel, int eventId, string message, params object[] args)
		{
			logger.Log(logLevel, eventId, null, message, args);
		}

		public static void Log(this ILogger logger, LogLevel logLevel, Exception exception, string message, params object[] args)
		{
			logger.Log(logLevel, 0, exception, message, args);
		}
	}
	public enum LogLevel
	{
		Trace,
		Debug,
		Information,
		Warning,
		Error,
		Critical,
		None
	}
	internal class NullLoggerFactory : ILoggerFactory
	{
		private class NullLogger : ILogger
		{
			public bool IsEnabled(LogLevel logLevel)
			{
				return false;
			}

			public void Log(LogLevel logLevel, int eventId, Exception exception, string message, params object[] args)
			{
			}
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new NullLogger();
		}
	}
	public sealed class PooledBytes : IDisposable
	{
		private static readonly ArrayPool<byte> s_pool = ArrayPool<byte>.Create(16384, 100);

		private int _length;

		private ArraySegment<byte> _buffer;

		private bool _disposed;

		public byte[] Buffer
		{
			get
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("PooledBytes");
				}
				return _buffer.Array;
			}
		}

		public ArraySegment<byte> BufferSegment
		{
			get
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("PooledBytes");
				}
				return _buffer;
			}
		}

		public PooledBytes(int length)
		{
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			_length = length;
			_buffer = new ArraySegment<byte>(s_pool.Rent(length), 0, _length);
		}

		public void Extend(int length)
		{
			byte[] array = s_pool.Rent(_length + length);
			System.Buffer.BlockCopy(_buffer.Array, 0, array, 0, _length);
			s_pool.Return(_buffer.Array);
			_length += length;
			_buffer = new ArraySegment<byte>(array, 0, _length);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (disposing && !_disposed)
			{
				_disposed = true;
				s_pool.Return(_buffer.Array, clearArray: true);
			}
		}
	}
	public class StringBuilderObjectPool
	{
		private readonly ObjectPool<StringBuilder> _pool;

		public static StringBuilderObjectPool Default { get; } = new StringBuilderObjectPool();

		public StringBuilderObjectPool(int initialCapacity = 200, int maxPooledCapacity = 2048)
		{
			_pool = new DefaultObjectPoolProvider().CreateStringBuilderPool(initialCapacity, maxPooledCapacity);
		}

		public StringBuilder Get()
		{
			return _pool.Get();
		}

		public void Return(StringBuilder value)
		{
			_pool.Return(value);
		}
	}
	internal interface IPooledObjectPolicy<T>
	{
		T Create();

		bool Return(T obj);
	}
	internal class DefaultObjectPool<T> : ObjectPool<T> where T : class
	{
		[DebuggerDisplay("{Element}")]
		private protected struct ObjectWrapper
		{
			public T Element;
		}

		private protected readonly ObjectWrapper[] _items;

		private protected readonly IPooledObjectPolicy<T> _policy;

		private protected readonly bool _isDefaultPolicy;

		private protected T _firstItem;

		private protected readonly PooledObjectPolicy<T> _fastPolicy;

		public DefaultObjectPool(IPooledObjectPolicy<T> policy)
			: this(policy, Environment.ProcessorCount * 2)
		{
		}

		public DefaultObjectPool(IPooledObjectPolicy<T> policy, int maximumRetained)
		{
			_policy = policy ?? throw new ArgumentNullException("policy");
			_fastPolicy = policy as PooledObjectPolicy<T>;
			_isDefaultPolicy = IsDefaultPolicy();
			_items = new ObjectWrapper[maximumRetained - 1];
			bool IsDefaultPolicy()
			{
				Type type = policy.GetType();
				if (type.IsGenericType)
				{
					return type.GetGenericTypeDefinition() == typeof(DefaultPooledObjectPolicy<>);
				}
				return false;
			}
		}

		public override T Get()
		{
			T val = _firstItem;
			if (val == null || Interlocked.CompareExchange(ref _firstItem, null, val) != val)
			{
				ObjectWrapper[] items = _items;
				for (int i = 0; i < items.Length; i++)
				{
					val = items[i].Element;
					if (val != null && Interlocked.CompareExchange(ref items[i].Element, null, val) == val)
					{
						return val;
					}
				}
				val = Create();
			}
			return val;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private T Create()
		{
			PooledObjectPolicy<T> fastPolicy = _fastPolicy;
			return ((fastPolicy != null) ? fastPolicy.Create() : null) ?? _policy.Create();
		}

		public override void Return(T obj)
		{
			if ((_isDefaultPolicy || (_fastPolicy?.Return(obj) ?? _policy.Return(obj))) && (_firstItem != null || Interlocked.CompareExchange(ref _firstItem, obj, null) != null))
			{
				ObjectWrapper[] items = _items;
				for (int i = 0; i < items.Length && Interlocked.CompareExchange(ref items[i].Element, obj, null) != null; i++)
				{
				}
			}
		}
	}
	internal class DefaultObjectPoolProvider : ObjectPoolProvider
	{
		public int MaximumRetained { get; set; } = Environment.ProcessorCount * 2;

		public override ObjectPool<T> Create<T>(IPooledObjectPolicy<T> policy)
		{
			if (policy == null)
			{
				throw new ArgumentNullException("policy");
			}
			return new DefaultObjectPool<T>(policy, MaximumRetained);
		}
	}
	internal class DefaultPooledObjectPolicy<T> : PooledObjectPolicy<T> where T : class, new()
	{
		public override T Create()
		{
			return new T();
		}

		public override bool Return(T obj)
		{
			return true;
		}
	}
	internal abstract class ObjectPool<T> where T : class
	{
		public abstract T Get();

		public abstract void Return(T obj);
	}
	internal static class ObjectPool
	{
		public static ObjectPool<T> Create<T>(IPooledObjectPolicy<T> policy = null) where T : class, new()
		{
			return new DefaultObjectPoolProvider().Create(policy ?? new DefaultPooledObjectPolicy<T>());
		}
	}
	internal abstract class ObjectPoolProvider
	{
		public ObjectPool<T> Create<T>() where T : class, new()
		{
			return Create(new DefaultPooledObjectPolicy<T>());
		}

		public abstract ObjectPool<T> Create<T>(IPooledObjectPolicy<T> policy) where T : class;
	}
	internal abstract class PooledObjectPolicy<T> : IPooledObjectPolicy<T>
	{
		public abstract T Create();

		public abstract bool Return(T obj);
	}
	internal class StringBuilderPooledObjectPolicy : PooledObjectPolicy<StringBuilder>
	{
		public int InitialCapacity { get; set; } = 100;

		public int MaximumRetainedCapacity { get; set; } = 4096;

		public override StringBuilder Create()
		{
			return new StringBuilder(InitialCapacity);
		}

		public override bool Return(StringBuilder obj)
		{
			if (obj.Capacity > MaximumRetainedCapacity)
			{
				return false;
			}
			obj.Clear();
			return true;
		}
	}
	internal static class ObjectPoolProviderExtensions
	{
		public static ObjectPool<StringBuilder> CreateStringBuilderPool(this ObjectPoolProvider provider)
		{
			return provider.Create(new StringBuilderPooledObjectPolicy());
		}

		public static ObjectPool<StringBuilder> CreateStringBuilderPool(this ObjectPoolProvider provider, int initialCapacity, int maximumRetainedCapacity)
		{
			StringBuilderPooledObjectPolicy policy = new StringBuilderPooledObjectPolicy
			{
				InitialCapacity = initialCapacity,
				MaximumRetainedCapacity = maximumRetainedCapacity
			};
			return provider.Create(policy);
		}
	}
}
namespace DnsClient.Protocol
{
	[Flags]
	internal enum DnsHeaderFlag : ushort
	{
		IsCheckingDisabled = 0x10,
		IsAuthenticData = 0x20,
		FutureUse = 0x40,
		RecursionAvailable = 0x80,
		RecursionDesired = 0x100,
		ResultTruncated = 0x200,
		HasAuthorityAnswer = 0x400,
		HasQuery = 0x8000
	}
	internal static class DnsHeader
	{
		public static readonly ushort OPCodeMask = 30720;

		public static readonly ushort OPCodeShift = 11;

		public static readonly ushort RCodeMask = 15;
	}
	public class AaaaRecord : AddressRecord
	{
		public AaaaRecord(ResourceRecordInfo info, IPAddress address)
			: base(info, address)
		{
		}
	}
	public class AddressRecord : DnsResourceRecord
	{
		public IPAddress Address { get; }

		public AddressRecord(ResourceRecordInfo info, IPAddress address)
			: base(info)
		{
			Address = address ?? throw new ArgumentNullException("address");
		}

		private protected override string RecordToString()
		{
			return Address.ToString();
		}
	}
	public class AfsDbRecord : DnsResourceRecord
	{
		public AfsType SubType { get; }

		public DnsString Hostname { get; }

		public AfsDbRecord(ResourceRecordInfo info, AfsType type, DnsString name)
			: base(info)
		{
			SubType = type;
			Hostname = name ?? throw new ArgumentNullException("name");
		}

		private protected override string RecordToString()
		{
			return $"{(int)SubType} {Hostname}";
		}
	}
	public enum AfsType
	{
		Afs = 1,
		Dce
	}
	public class ARecord : AddressRecord
	{
		public ARecord(ResourceRecordInfo info, IPAddress address)
			: base(info, address)
		{
		}
	}
	public class CaaRecord : DnsResourceRecord
	{
		public byte Flags { get; }

		public string Tag { get; }

		public string Value { get; }

		public CaaRecord(ResourceRecordInfo info, byte flags, string tag, string value)
			: base(info)
		{
			Flags = flags;
			Tag = tag ?? throw new ArgumentNullException("tag");
			Value = value ?? throw new ArgumentNullException("value");
		}

		private protected override string RecordToString()
		{
			return $"{Flags} {Tag} \"{Value}\"";
		}
	}
	public class CNameRecord : DnsResourceRecord
	{
		public DnsString CanonicalName { get; }

		public CNameRecord(ResourceRecordInfo info, DnsString canonicalName)
			: base(info)
		{
			CanonicalName = canonicalName ?? throw new ArgumentNullException("canonicalName");
		}

		private protected override string RecordToString()
		{
			return CanonicalName.Value;
		}
	}
	public class DnsKeyRecord : DnsResourceRecord
	{
		public int Flags { get; }

		public byte Protocol { get; }

		public DnsSecurityAlgorithm Algorithm { get; }

		public IReadOnlyList<byte> PublicKey { get; }

		public string PublicKeyAsString { get; }

		public DnsKeyRecord(ResourceRecordInfo info, int flags, byte protocol, byte algorithm, byte[] publicKey)
			: base(info)
		{
			Flags = flags;
			Protocol = protocol;
			Algorithm = (DnsSecurityAlgorithm)algorithm;
			PublicKey = publicKey ?? throw new ArgumentNullException("publicKey");
			PublicKeyAsString = Convert.ToBase64String(publicKey);
		}

		private protected override string RecordToString()
		{
			return $"{Flags} {Protocol} {Algorithm} {PublicKeyAsString}";
		}
	}
	public abstract class DnsResourceRecord : ResourceRecordInfo
	{
		public DnsResourceRecord(ResourceRecordInfo info)
		{
			DnsString domainName = info?.DomainName ?? throw new ArgumentNullException("info");
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			ResourceRecordType recordType = info.RecordType;
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			QueryClass recordClass = info.RecordClass;
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			int initialTimeToLive = info.InitialTimeToLive;
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base..ctor(domainName, recordType, recordClass, initialTimeToLive, info.RawDataLength);
		}

		public override string ToString()
		{
			return ToString();
		}

		public virtual string ToString(int offset = 0)
		{
			string text = ((offset == 0) ? string.Empty : "\t");
			return string.Format("{0," + offset + "} {1}{2} {1}{3} {1}{4} {1}{5}", base.DomainName, text, base.TimeToLive, base.RecordClass, base.RecordType, RecordToString());
		}

		private protected abstract string RecordToString();
	}
	public class ResourceRecordInfo
	{
		private readonly int _ticks;

		public DnsString DomainName { get; }

		public ResourceRecordType RecordType { get; }

		public QueryClass RecordClass { get; }

		public int TimeToLive
		{
			get
			{
				int num = (int)((double)(Environment.TickCount & 0x7FFFFFFF) / 1000.0);
				if (num < _ticks)
				{
					return 0;
				}
				int num2 = InitialTimeToLive - (num - _ticks);
				if (num2 >= 0)
				{
					return num2;
				}
				return 0;
			}
		}

		public int InitialTimeToLive { get; internal set; }

		public int RawDataLength { get; }

		public ResourceRecordInfo(string domainName, ResourceRecordType recordType, QueryClass recordClass, int timeToLive, int rawDataLength)
			: this(DnsString.Parse(domainName), recordType, recordClass, timeToLive, rawDataLength)
		{
		}

		public ResourceRecordInfo(DnsString domainName, ResourceRecordType recordType, QueryClass recordClass, int timeToLive, int rawDataLength)
		{
			DomainName = domainName ?? throw new ArgumentNullException("domainName");
			RecordType = recordType;
			RecordClass = recordClass;
			RawDataLength = rawDataLength;
			InitialTimeToLive = timeToLive;
			_ticks = (int)((double)(Environment.TickCount & 0x7FFFFFFF) / 1000.0);
		}
	}
	public enum DnsSecurityAlgorithm
	{
		None = 0,
		RSAMD5 = 1,
		DH = 2,
		DSA = 3,
		RSASHA1 = 5,
		DSA_NSEC3_SHA1 = 6,
		RSASHA1_NSEC3_SHA1 = 7,
		RSASHA256 = 8,
		RSASHA512 = 10,
		ECCGOST = 12,
		ECDSAP256SHA256 = 13,
		ECDSAP384SHA384 = 14,
		ED25519 = 15,
		ED448 = 16,
		INDIRECT = 252,
		PRIVATEDNS = 253,
		PRIVATEOID = 254
	}
	public class DsRecord : DnsResourceRecord
	{
		public int KeyTag { get; }

		public DnsSecurityAlgorithm Algorithm { get; }

		public byte DigestType { get; }

		public IReadOnlyList<byte> Digest { get; }

		public string DigestAsString { get; }

		public DsRecord(ResourceRecordInfo info, int keyTag, byte algorithm, byte digestType, byte[] digest)
			: base(info)
		{
			KeyTag = keyTag;
			Algorithm = (DnsSecurityAlgorithm)algorithm;
			DigestType = digestType;
			Digest = digest ?? throw new ArgumentNullException("digest");
			DigestAsString = string.Join(string.Empty, digest.Select((byte b) => b.ToString("X2")));
		}

		private protected override string RecordToString()
		{
			return $"{KeyTag} {Algorithm} {DigestType} {DigestAsString}";
		}
	}
	public class EmptyRecord : DnsResourceRecord
	{
		public EmptyRecord(ResourceRecordInfo info)
			: base(info)
		{
		}

		private protected override string RecordToString()
		{
			return string.Empty;
		}
	}
	public class HInfoRecord : DnsResourceRecord
	{
		public string Cpu { get; }

		public string OS { get; }

		public HInfoRecord(ResourceRecordInfo info, string cpu, string os)
			: base(info)
		{
			Cpu = cpu;
			OS = os;
		}

		private protected override string RecordToString()
		{
			return "\"" + Cpu + "\" \"" + OS + "\"";
		}
	}
	public class MbRecord : DnsResourceRecord
	{
		public DnsString MadName { get; }

		public MbRecord(ResourceRecordInfo info, DnsString domainName)
			: base(info)
		{
			MadName = domainName ?? throw new ArgumentNullException("domainName");
		}

		private protected override string RecordToString()
		{
			return MadName.Value;
		}
	}
	public class MgRecord : DnsResourceRecord
	{
		public DnsString MgName { get; }

		public MgRecord(ResourceRecordInfo info, DnsString domainName)
			: base(info)
		{
			MgName = domainName ?? throw new ArgumentNullException("domainName");
		}

		private protected override string RecordToString()
		{
			return MgName.Value;
		}
	}
	public class MInfoRecord : DnsResourceRecord
	{
		public DnsString RMailBox { get; }

		public DnsString EmailBox { get; }

		public MInfoRecord(ResourceRecordInfo info, DnsString rmailBox, DnsString emailBox)
			: base(info)
		{
			RMailBox = rmailBox ?? throw new ArgumentNullException("rmailBox");
			EmailBox = emailBox ?? throw new ArgumentNullException("emailBox");
		}

		private protected override string RecordToString()
		{
			return $"{RMailBox} {EmailBox}";
		}
	}
	public class MrRecord : DnsResourceRecord
	{
		public DnsString NewName { get; }

		public MrRecord(ResourceRecordInfo info, DnsString name)
			: base(info)
		{
			NewName = name ?? throw new ArgumentNullException("name");
		}

		private protected override string RecordToString()
		{
			return NewName.Value;
		}
	}
	[CLSCompliant(false)]
	public class MxRecord : DnsResourceRecord
	{
		public ushort Preference { get; }

		public DnsString Exchange { get; }

		public MxRecord(ResourceRecordInfo info, ushort preference, DnsString domainName)
			: base(info)
		{
			Preference = preference;
			Exchange = domainName ?? throw new ArgumentNullException("domainName");
		}

		private protected override string RecordToString()
		{
			return $"{Preference} {Exchange}";
		}
	}
	public class NAPtrRecord : DnsResourceRecord
	{
		public const string ServiceKeySip = "E2U+SIP";

		public const string ServiceKeyEmail = "E2U+EMAIL";

		public const string ServiceKeyWeb = "E2U+WEB";

		public const string ServiceKeySipUdp = "SIP+D2U";

		public const string ServiceKeySipTcp = "SIP+D2T";

		public const string ServiceKeySipsTcp = "SIPS+D2T";

		public const string ServiceKeySipWebsocket = "SIP+D2W";

		public const string ServiceKeySipsWebsocket = "SIPS+D2W";

		public const char AFlag = 'A';

		public const char PFlag = 'P';

		public const char SFlag = 'S';

		public const char UFlag = 'U';

		public int Order { get; }

		public int Preference { get; }

		public string Flags { get; }

		public string Services { get; }

		public string RegularExpression { get; }

		public DnsString Replacement { get; }

		public NAPtrRecord(ResourceRecordInfo info, int order, int preference, string flags, string services, string regexp, DnsString replacement)
			: base(info)
		{
			Order = order;
			Preference = preference;
			Flags = flags;
			Services = services ?? throw new ArgumentNullException("services");
			RegularExpression = regexp;
			Replacement = replacement ?? throw new ArgumentNullException("replacement");
		}

		private protected override string RecordToString()
		{
			return $"{Order} {Preference} \"{Flags}\" \"{Services}\" \"{RegularExpression}\" {Replacement}";
		}
	}
	public class NSec3ParamRecord : DnsResourceRecord
	{
		public byte HashAlgorithm { get; }

		public byte Flags { get; }

		public int Iterations { get; }

		public byte[] Salt { get; }

		public string SaltAsString { get; }

		public NSec3ParamRecord(ResourceRecordInfo info, byte hashAlgorithm, byte flags, int iterations, byte[] salt)
			: base(info)
		{
			HashAlgorithm = hashAlgorithm;
			Flags = flags;
			Iterations = iterations;
			Salt = salt ?? throw new ArgumentNullException("salt");
			SaltAsString = ((Salt.Length == 0) ? "-" : string.Join(string.Empty, Salt.Select((byte b) => b.ToString("X2"))));
		}

		private protected override string RecordToString()
		{
			return $"{HashAlgorithm} {Flags} {Iterations} {SaltAsString}";
		}
	}
	public class NSec3Record : DnsResourceRecord
	{
		public byte HashAlgorithm { get; }

		public byte Flags { get; }

		public int Iterations { get; }

		public byte[] Salt { get; }

		public string SaltAsString { get; }

		public byte[] NextOwnersName { get; }

		public string NextOwnersNameAsString { get; }

		public IReadOnlyList<byte> TypeBitMapsRaw { get; }

		public IReadOnlyList<ResourceRecordType> TypeBitMaps { get; }

		public NSec3Record(ResourceRecordInfo info, byte hashAlgorithm, byte flags, int iterations, byte[] salt, byte[] nextOwnersName, byte[] bitmap)
			: base(info)
		{
			HashAlgorithm = hashAlgorithm;
			Flags = flags;
			Iterations = iterations;
			Salt = salt ?? throw new ArgumentNullException("salt");
			TypeBitMapsRaw = bitmap ?? throw new ArgumentNullException("bitmap");
			SaltAsString = ((Salt.Length == 0) ? "-" : string.Join(string.Empty, Salt.Select((byte b) => b.ToString("X2"))));
			NextOwnersName = nextOwnersName ?? throw new ArgumentNullException("nextOwnersName");
			try
			{
				NextOwnersNameAsString = Base32Hex.ToBase32HexString(nextOwnersName);
			}
			catch
			{
			}
			TypeBitMaps = (from p in NSecRecord.ReadBitmap(bitmap)
				orderby p
				select (ResourceRecordType)p).ToArray();
		}

		private protected override string RecordToString()
		{
			return string.Format("{0} {1} {2} {3} {4} {5}", HashAlgorithm, Flags, Iterations, SaltAsString, NextOwnersNameAsString, string.Join(" ", TypeBitMaps));
		}
	}
	public class NSecRecord : DnsResourceRecord
	{
		public DnsString NextDomainName { get; }

		public IReadOnlyList<byte> TypeBitMapsRaw { get; }

		public IReadOnlyList<ResourceRecordType> TypeBitMaps { get; }

		public NSecRecord(ResourceRecordInfo info, DnsString nextDomainName, byte[] typeBitMaps)
			: base(info)
		{
			NextDomainName = nextDomainName ?? throw new ArgumentNullException("nextDomainName");
			TypeBitMapsRaw = typeBitMaps ?? throw new ArgumentNullException("typeBitMaps");
			TypeBitMaps = (from p in ReadBitmap(typeBitMaps)
				orderby p
				select (ResourceRecordType)p).ToArray();
		}

		private protected override string RecordToString()
		{
			return string.Format("{0} {1}", NextDomainName, string.Join(" ", TypeBitMaps));
		}

		internal static IEnumerable<int> ReadBitmap(byte[] data)
		{
			if (data.Length < 2)
			{
				throw new DnsResponseParseException("Invalid bitmap length, less than 2 bytes available.");
			}
			int n = 0;
			while (n < data.Length)
			{
				byte window = data[n++];
				byte len = data[n++];
				if (window == 0 && len == 0)
				{
					continue;
				}
				if (len > 32)
				{
					throw new DnsResponseParseException("Invalid bitmap length, more than 32 bytes in a window.");
				}
				if (n + len > data.Length)
				{
					throw new DnsResponseParseException("Invalid bitmap length, more bytes requested then available.");
				}
				for (int k = 0; k < len; k++)
				{
					if (n >= data.Length)
					{
						break;
					}
					byte val = data[n++];
					int bit = 0;
					while (bit < 8)
					{
						if ((val & 1) == 1)
						{
							yield return 7 - bit + 8 * k + 256 * window;
						}
						int num = bit + 1;
						bit = num;
						val >>= 1;
					}
				}
			}
		}

		internal static IEnumerable<byte> WriteBitmap(ushort[] values)
		{
			Array.Sort(values);
			int num = -1;
			bool[] windowsInUse = new bool[256];
			int maxVal = 0;
			byte[] typebits = new byte[8192];
			int used = 0;
			foreach (ushort num2 in values)
			{
				if (num2 != num)
				{
					num = num2;
					typebits[num2 / 8] |= (byte)(128 >> num2 % 8);
					windowsInUse[num2 / 256] = true;
					if (num2 > maxVal)
					{
						maxVal = num2;
					}
				}
			}
			for (int block = 0; block <= maxVal / 256; block++)
			{
				int blockLen = 0;
				if (!windowsInUse[block])
				{
					continue;
				}
				for (int j = 0; j < 32; j++)
				{
					if (typebits[block * 32 + j] != 0)
					{
						blockLen = j + 1;
					}
				}
				if (blockLen != 0)
				{
					yield return (byte)block;
					yield return (byte)blockLen;
					for (int k = 0; k < blockLen; k++)
					{
						yield return typebits[block * 32 + k];
					}
					used += blockLen + 2;
				}
			}
		}
	}
	public class NsRecord : DnsResourceRecord
	{
		public DnsString NSDName { get; }

		public NsRecord(ResourceRecordInfo info, DnsString name)
			: base(info)
		{
			NSDName = name ?? throw new ArgumentNullException("name");
		}

		private protected override string RecordToString()
		{
			return NSDName.Value;
		}
	}
	public class NullRecord : DnsResourceRecord
	{
		public byte[] Anything { get; }

		public string AsString { get; }

		public NullRecord(ResourceRecordInfo info, byte[] anything)
			: base(info)
		{
			Anything = anything ?? throw new ArgumentNullException("anything");
			try
			{
				AsString = Encoding.UTF8.GetString(anything);
			}
			catch
			{
			}
		}

		private protected override string RecordToString()
		{
			return $"\\# {Anything.Length} {AsString}";
		}
	}
	public class PtrRecord : DnsResourceRecord
	{
		public DnsString PtrDomainName { get; }

		public PtrRecord(ResourceRecordInfo info, DnsString ptrDomainName)
			: base(info)
		{
			PtrDomainName = ptrDomainName ?? throw new ArgumentNullException("ptrDomainName");
		}

		private protected override string RecordToString()
		{
			return PtrDomainName.Value;
		}
	}
	public enum ResourceRecordType
	{
		A = 1,
		NS = 2,
		[Obsolete("Use MX")]
		MD = 3,
		[Obsolete("Use MX")]
		MF = 4,
		CNAME = 5,
		SOA = 6,
		MB = 7,
		MG = 8,
		MR = 9,
		NULL = 10,
		WKS = 11,
		PTR = 12,
		HINFO = 13,
		MINFO = 14,
		MX = 15,
		TXT = 16,
		RP = 17,
		AFSDB = 18,
		AAAA = 28,
		SRV = 33,
		NAPTR = 35,
		OPT = 41,
		DS = 43,
		SSHFP = 44,
		RRSIG = 46,
		NSEC = 47,
		DNSKEY = 48,
		NSEC3 = 50,
		NSEC3PARAM = 51,
		TLSA = 52,
		SPF = 99,
		URI = 256,
		CAA = 257
	}
	public class RpRecord : DnsResourceRecord
	{
		public DnsString MailboxDomainName { get; }

		public DnsString TextDomainName { get; }

		public RpRecord(ResourceRecordInfo info, DnsString mailbox, DnsString textName)
			: base(info)
		{
			MailboxDomainName = mailbox ?? throw new ArgumentNullException("mailbox");
			TextDomainName = textName ?? throw new ArgumentNullException("textName");
		}

		private protected override string RecordToString()
		{
			return $"{MailboxDomainName} {TextDomainName}";
		}
	}
	public class RRSigRecord : DnsResourceRecord
	{
		public ResourceRecordType CoveredType { get; }

		public DnsSecurityAlgorithm Algorithm { get; }

		public byte Labels { get; }

		public long OriginalTtl { get; }

		public DateTimeOffset SignatureExpiration { get; }

		public DateTimeOffset SignatureInception { get; }

		public int KeyTag { get; }

		public DnsString SignersName { get; }

		public IReadOnlyList<byte> Signature { get; }

		public string SignatureAsString { get; }

		public RRSigRecord(ResourceRecordInfo info, int coveredType, byte algorithm, byte labels, long originalTtl, long signatureExpiration, long signatureInception, int keyTag, DnsString signersName, byte[] signature)
			: base(info)
		{
			CoveredType = (ResourceRecordType)coveredType;
			Algorithm = (DnsSecurityAlgorithm)algorithm;
			Labels = labels;
			OriginalTtl = originalTtl;
			SignatureExpiration = FromUnixTimeSeconds(signatureExpiration);
			SignatureInception = FromUnixTimeSeconds(signatureInception);
			KeyTag = keyTag;
			SignersName = signersName ?? throw new ArgumentNullException("signersName");
			Signature = signature ?? throw new ArgumentNullException("signature");
			SignatureAsString = Convert.ToBase64String(signature);
		}

		private protected override string RecordToString()
		{
			return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}", CoveredType, Algorithm, Labels, OriginalTtl, SignatureExpiration.ToString("yyyyMMddHHmmss"), SignatureInception.ToString("yyyyMMddHHmmss"), KeyTag, SignersName, SignatureAsString);
		}

		private static DateTimeOffset FromUnixTimeSeconds(long seconds)
		{
			return new DateTimeOffset(seconds * 10000000 + new DateTime(1970, 1, 1, 0, 0, 0).Ticks, TimeSpan.Zero);
		}
	}
	[CLSCompliant(false)]
	public class SoaRecord : DnsResourceRecord
	{
		public uint Expire { get; }

		public uint Minimum { get; }

		public DnsString MName { get; }

		public uint Refresh { get; }

		public uint Retry { get; }

		public DnsString RName { get; }

		public uint Serial { get; }

		public SoaRecord(ResourceRecordInfo info, DnsString mName, DnsString rName, uint serial, uint refresh, uint retry, uint expire, uint minimum)
			: base(info)
		{
			MName = mName ?? throw new ArgumentNullException("mName");
			RName = rName ?? throw new ArgumentNullException("rName");
			Serial = serial;
			Refresh = refresh;
			Retry = retry;
			Expire = expire;
			Minimum = minimum;
		}

		private protected override string RecordToString()
		{
			return $"{MName} {RName} {Serial} {Refresh} {Retry} {Expire} {Minimum}";
		}
	}
	[CLSCompliant(false)]
	public class SrvRecord : DnsResourceRecord
	{
		public ushort Port { get; }

		public ushort Priority { get; }

		public DnsString Target { get; }

		public ushort Weight { get; }

		public SrvRecord(ResourceRecordInfo info, ushort priority, ushort weight, ushort port, DnsString target)
			: base(info)
		{
			Priority = priority;
			Weight = weight;
			Port = port;
			Target = target ?? throw new ArgumentNullException("target");
		}

		private protected override string RecordToString()
		{
			return $"{Priority} {Weight} {Port} {Target}";
		}
	}
	public class SshfpRecord : DnsResourceRecord
	{
		public SshfpAlgorithm Algorithm { get; }

		public SshfpFingerprintType FingerprintType { get; }

		public string Fingerprint { get; }

		public SshfpRecord(ResourceRecordInfo info, SshfpAlgorithm algorithm, SshfpFingerprintType fingerprintType, string fingerprint)
			: base(info)
		{
			Algorithm = algorithm;
			FingerprintType = fingerprintType;
			Fingerprint = fingerprint;
		}

		private protected override string RecordToString()
		{
			return $"{(int)Algorithm} {(int)FingerprintType} {Fingerprint}";
		}
	}
	public enum SshfpAlgorithm
	{
		Reserved,
		RSA,
		DSS,
		ECDSA,
		Ed25519
	}
	public enum SshfpFingerprintType
	{
		Reserved,
		SHA1,
		SHA256
	}
	public class TlsaRecord : DnsResourceRecord
	{
		public TlsaCertificateUsage CertificateUsage { get; }

		public TlsaSelector Selector { get; }

		public TlsaMatchingType MatchingType { get; }

		public IReadOnlyList<byte> CertificateAssociationData { get; }

		public string CertificateAssociationDataAsString { get; }

		public TlsaRecord(ResourceRecordInfo info, byte certificateUsage, byte selector, byte matchingType, byte[] certificateAssociationData)
			: base(info)
		{
			CertificateUsage = (TlsaCertificateUsage)certificateUsage;
			Selector = (TlsaSelector)selector;
			MatchingType = (TlsaMatchingType)matchingType;
			CertificateAssociationData = certificateAssociationData ?? throw new ArgumentNullException("certificateAssociationData");
			CertificateAssociationDataAsString = string.Join(string.Empty, certificateAssociationData.Select((byte b) => b.ToString("X2")));
		}

		private protected override string RecordToString()
		{
			return $"{CertificateUsage} {Selector} {MatchingType} {CertificateAssociationDataAsString}";
		}
	}
	public enum TlsaCertificateUsage : byte
	{
		PKIXTA,
		PKIXEE,
		DANETA,
		DANEEE
	}
	public enum TlsaSelector : byte
	{
		FullCertificate,
		PublicKey
	}
	public enum TlsaMatchingType : byte
	{
		ExactMatch,
		SHA256,
		SHA512
	}
	public class TxtRecord : DnsResourceRecord
	{
		public ICollection<string> EscapedText { get; }

		public ICollection<string> Text { get; }

		public TxtRecord(ResourceRecordInfo info, string[] values, string[] utf8Values)
			: base(info)
		{
			EscapedText = values ?? throw new ArgumentNullException("values");
			Text = utf8Values ?? throw new ArgumentNullException("utf8Values");
		}

		private protected override string RecordToString()
		{
			return string.Join(" ", EscapedText.Select((string p) => "\"" + p + "\"")).Trim();
		}
	}
	public class UnknownRecord : DnsResourceRecord
	{
		public IReadOnlyList<byte> Data { get; }

		public string DataAsString { get; }

		public UnknownRecord(ResourceRecordInfo info, byte[] data)
			: base(info)
		{
			Data = data ?? throw new ArgumentNullException("data");
			DataAsString = Convert.ToBase64String(data);
		}

		private protected override string RecordToString()
		{
			return DataAsString;
		}
	}
	public class UriRecord : DnsResourceRecord
	{
		public string Target { get; set; }

		public int Priority { get; set; }

		public int Weight { get; set; }

		public UriRecord(ResourceRecordInfo info, int priority, int weight, string target)
			: base(info)
		{
			Target = target ?? throw new ArgumentNullException("target");
			Priority = priority;
			Weight = weight;
		}

		private protected override string RecordToString()
		{
			return $"{Priority} {Weight} \"{Target}\"";
		}
	}
	public class WksRecord : DnsResourceRecord
	{
		public IPAddress Address { get; }

		public ProtocolType Protocol { get; }

		public byte[] Bitmap { get; }

		public int[] Ports { get; }

		public WksRecord(ResourceRecordInfo info, IPAddress address, int protocol, byte[] bitmap)
			: base(info)
		{
			Address = address ?? throw new ArgumentNullException("address");
			Protocol = (ProtocolType)protocol;
			Bitmap = bitmap ?? throw new ArgumentNullException("bitmap");
			Ports = GetPorts(bitmap);
		}

		private protected override string RecordToString()
		{
			return string.Format("{0} {1} {2}", Address, Protocol, string.Join(" ", Ports));
		}

		private static int[] GetPorts(byte[] data)
		{
			int num = 0;
			int num2 = data.Length;
			List<int> list = new List<int>();
			if (data.Length == 0)
			{
				return list.ToArray();
			}
			while (num < num2)
			{
				byte b = data[num++];
				if (b == 0)
				{
					continue;
				}
				for (int num3 = 7; num3 >= 0; num3--)
				{
					if ((b & (1 << num3)) != 0)
					{
						list.Add(num * 8 - num3 - 1);
					}
				}
			}
			return list.ToArray();
		}
	}
}
namespace DnsClient.Protocol.Options
{
	public class OptRecord : DnsResourceRecord
	{
		private const uint ResponseCodeMask = 4278190080u;

		private const int ResponseCodeShift = 20;

		private const uint VersionMask = 16711680u;

		private const int VersionShift = 16;

		public DnsResponseCode ResponseCodeEx
		{
			get
			{
				return (DnsResponseCode)((base.InitialTimeToLive & 0xFF000000u) >> 20);
			}
			set
			{
				base.InitialTimeToLive &= 16777215;
				base.InitialTimeToLive |= (int)(((int)value << 20) & 0xFF000000u);
			}
		}

		public short UdpSize => (short)base.RecordClass;

		public byte Version
		{
			get
			{
				return (byte)((long)((ulong)base.InitialTimeToLive & 0xFF0000uL) >> 16);
			}
			set
			{
				base.InitialTimeToLive &= -16711681;
				base.InitialTimeToLive |= (int)((long)(value << 16) & 0xFF0000L);
			}
		}

		public bool IsDnsSecOk
		{
			get
			{
				return (base.InitialTimeToLive & 0x8000) != 0;
			}
			set
			{
				if (value)
				{
					base.InitialTimeToLive |= 32768;
				}
				else
				{
					base.InitialTimeToLive &= 32767;
				}
			}
		}

		public byte[] Data { get; }

		public OptRecord(int size = 4096, int version = 0, bool doFlag = false, int length = 0, byte[] data = null)
			: base(new ResourceRecordInfo(DnsString.RootLabel, ResourceRecordType.OPT, (QueryClass)size, version, length))
		{
			Data = data;
			IsDnsSecOk = doFlag;
		}

		public OptRecord(int size, int ttlFlag, int length, byte[] data)
			: base(new ResourceRecordInfo(DnsString.RootLabel, ResourceRecordType.OPT, (QueryClass)size, ttlFlag, length))
		{
			Data = data;
		}

		private protected override string RecordToString()
		{
			return $"OPT {base.RecordClass}.";
		}
	}
}
