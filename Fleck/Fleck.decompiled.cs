using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Fleck.Handlers;
using Fleck.Helpers;
using Microsoft.CodeAnalysis;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: InternalsVisibleTo("Fleck.Tests")]
[assembly: TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = ".NET Standard 2.0")]
[assembly: AssemblyCompany("statenjason")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("Copyright Jason Staten 2010-2018. All rights reserved.")]
[assembly: AssemblyDescription("C# WebSocket Implementation")]
[assembly: AssemblyFileVersion("0.0.0.1")]
[assembly: AssemblyInformationalVersion("0.0.0.1-local+2f8363a930d887b10cb6f47ee537b7b9eb1bd5d3")]
[assembly: AssemblyProduct("Fleck")]
[assembly: AssemblyTitle("Fleck")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/statianzo/Fleck")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.1")]
[module: UnverifiableCode]
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
	internal sealed class IsUnmanagedAttribute : Attribute
	{
	}
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	internal sealed class IsByRefLikeAttribute : Attribute
	{
	}
}
namespace Fleck
{
	public class ConnectionNotAvailableException : Exception
	{
		public ConnectionNotAvailableException()
		{
		}

		public ConnectionNotAvailableException(string message)
			: base(message)
		{
		}

		public ConnectionNotAvailableException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
	public enum LogLevel
	{
		Debug,
		Info,
		Warn,
		Error
	}
	public class FleckLog
	{
		public static LogLevel Level = LogLevel.Info;

		public static Action<LogLevel, string, Exception> LogAction = delegate
		{
		};

		public static void Warn(string message, Exception ex = null)
		{
			if (Level >= LogLevel.Warn)
			{
				LogAction?.Invoke(LogLevel.Warn, message, ex);
			}
		}

		public static void Error(string message, Exception ex = null)
		{
			if (Level >= LogLevel.Error)
			{
				LogAction?.Invoke(LogLevel.Error, message, ex);
			}
		}

		[Conditional("DEBUG")]
		public static void Debug(string message, Exception ex = null)
		{
			if (Level >= LogLevel.Debug)
			{
				LogAction?.Invoke(LogLevel.Debug, message, ex);
			}
		}

		[Conditional("DEBUG")]
		public static void Info(string message, Exception ex = null)
		{
			if (Level >= LogLevel.Info)
			{
				LogAction?.Invoke(LogLevel.Info, message, ex);
			}
		}
	}
	public enum FrameType : byte
	{
		Continuation = 0,
		Text = 1,
		Binary = 2,
		Close = 8,
		Ping = 9,
		Pong = 10
	}
	public static class FrameTypeExtensions
	{
		public static bool IsDefined(this FrameType type)
		{
			if (type != FrameType.Continuation && type != FrameType.Text && type != FrameType.Binary && type != FrameType.Close && type != FrameType.Ping)
			{
				return type == FrameType.Pong;
			}
			return true;
		}
	}
	public class HandlerFactory
	{
		public static IHandler BuildHandler(WebSocketHttpRequest request, IWebSocketConnection connection)
		{
			switch (GetVersion(request))
			{
			case "7":
			case "8":
			case "13":
				return new Hybi13Handler(request, connection);
			default:
				throw new WebSocketException(1002);
			}
		}

		public static string GetVersion(WebSocketHttpRequest request)
		{
			if (request.Headers.TryGetValue("Sec-WebSocket-Version", out var value))
			{
				return value;
			}
			if (request.Headers.TryGetValue("Sec-WebSocket-Draft", out value))
			{
				return value;
			}
			if (request.Headers.ContainsKey("Sec-WebSocket-Key1"))
			{
				return "76";
			}
			return "75";
		}
	}
	public interface IHandler : IDisposable
	{
		void Receive(Span<byte> newData);

		MemoryBuffer CreateHandshake();

		MemoryBuffer FrameText(string text);

		MemoryBuffer FrameText(MemoryBuffer utf8StringBytes);

		MemoryBuffer FrameBinary(MemoryBuffer bytes);

		MemoryBuffer FramePing(MemoryBuffer bytes);

		MemoryBuffer FramePong(MemoryBuffer bytes);

		MemoryBuffer FrameClose(ushort code);
	}
	public interface ISocket
	{
		bool Connected { get; }

		IPAddress RemoteIpAddress { get; }

		int RemotePort { get; }

		Stream Stream { get; }

		bool NoDelay { get; set; }

		EndPoint LocalEndPoint { get; }

		Task<ISocket> Accept(Action<ISocket> callback, Action<Exception> error);

		Task Authenticate(X509Certificate2 certificate, SslProtocols enabledSslProtocols, Action callback, Action<Exception> error);

		void Dispose();

		void Close();

		void Bind(EndPoint ipLocal);

		void Listen(int backlog);
	}
	public delegate void BinaryDataHandler(Span<byte> data);
	public interface IWebSocketConnection
	{
		Action OnOpen { get; set; }

		Action OnClose { get; set; }

		Action<string> OnMessage { get; set; }

		BinaryDataHandler OnBinary { get; set; }

		BinaryDataHandler OnPing { get; set; }

		BinaryDataHandler OnPong { get; set; }

		Action<Exception> OnError { get; set; }

		IWebSocketConnectionInfo ConnectionInfo { get; }

		bool IsAvailable { get; }

		void Send(string message);

		void Send(MemoryBuffer message);

		void SendPing(MemoryBuffer message);

		void SendPong(MemoryBuffer message);

		void Close();
	}
	public interface IWebSocketConnectionInfo
	{
		Guid Id { get; }

		string SubProtocol { get; }

		string Origin { get; }

		string Host { get; }

		string Path { get; }

		IPAddress ClientIpAddress { get; }

		int ClientPort { get; }
	}
	public interface IWebSocketServer : IDisposable
	{
		void Start(Action<IWebSocketConnection> config);
	}
	internal static class IntExtensions
	{
		public static IMemoryOwner<byte> ToBigEndianBytes<T>(this int source)
		{
			if (typeof(T) == typeof(ushort))
			{
				return CopyToMemory((ushort)source);
			}
			if (typeof(T) == typeof(ulong))
			{
				return CopyToMemory((ulong)source);
			}
			throw new InvalidCastException("Cannot be cast to T");
		}

		public static int ToLittleEndianInt(this Span<byte> source)
		{
			if (source.Length == 2)
			{
				return CopyFromMemory<ushort>(source);
			}
			if (source.Length == 8)
			{
				return (int)CopyFromMemory<ulong>(source);
			}
			throw new ArgumentException("Unsupported Size");
		}

		private unsafe static IMemoryOwner<byte> CopyToMemory<T>(T value) where T : unmanaged
		{
			Span<byte> span = new Span<byte>(&value, sizeof(T));
			span.Reverse();
			IMemoryOwner<byte> memoryOwner = MemoryPool<byte>.Shared.Rent(sizeof(T));
			span.CopyTo(memoryOwner.Memory.Span);
			return memoryOwner;
		}

		private unsafe static T CopyFromMemory<T>(Span<byte> memory) where T : unmanaged
		{
			if (memory.Length != sizeof(T))
			{
				throw new ArgumentException($"Cannot copy from memory: expected {sizeof(T)} bytes, got {memory.Length}");
			}
			Span<byte> span = stackalloc byte[memory.Length];
			memory.CopyTo(span);
			span.Reverse();
			fixed (byte* ptr = &span[0])
			{
				return *(T*)ptr;
			}
		}
	}
	public struct MemoryBuffer
	{
		private readonly bool _fromPool;

		public byte[] Data { get; private set; }

		public int Length { get; private set; }

		public MemoryBuffer(int minimumLength)
		{
			Data = ArrayPool<byte>.Shared.Rent(minimumLength);
			Length = Data.Length;
			_fromPool = true;
		}

		internal MemoryBuffer(byte[] data, int length, bool fromPool = true)
		{
			Data = data;
			Length = length;
			_fromPool = fromPool;
		}

		public void Dispose()
		{
			if (Data != null && _fromPool)
			{
				ArrayPool<byte>.Shared.Return(Data);
			}
			Data = null;
			Length = 0;
		}

		public MemoryBuffer DontDispose()
		{
			return new MemoryBuffer(Data, Length, fromPool: false);
		}

		public MemoryBuffer Slice(int newLength)
		{
			if (newLength < 0 || newLength > Length)
			{
				throw new ArgumentOutOfRangeException("newLength");
			}
			return new MemoryBuffer(Data, newLength, _fromPool);
		}

		public static implicit operator Span<byte>(MemoryBuffer buffer)
		{
			return new Span<byte>(buffer.Data, 0, buffer.Length);
		}
	}
	public class QueuedStream : Stream
	{
		private class WriteData
		{
			public readonly byte[] Buffer;

			public readonly int Offset;

			public readonly int Count;

			public readonly AsyncCallback Callback;

			public readonly object State;

			public readonly QueuedWriteResult AsyncResult;

			public WriteData(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				Buffer = buffer;
				Offset = offset;
				Count = count;
				Callback = callback;
				State = state;
				AsyncResult = new QueuedWriteResult(state);
			}
		}

		private class QueuedWriteResult : IAsyncResult
		{
			private readonly object _state;

			public Exception Exception { get; set; }

			public IAsyncResult ActualResult { get; set; }

			public object AsyncState => _state;

			public WaitHandle AsyncWaitHandle
			{
				get
				{
					throw new NotSupportedException("Queued write operations do not support wait handle.");
				}
			}

			public bool CompletedSynchronously => false;

			public bool IsCompleted
			{
				get
				{
					if (ActualResult != null)
					{
						return ActualResult.IsCompleted;
					}
					return false;
				}
			}

			public QueuedWriteResult(object state)
			{
				_state = state;
			}
		}

		private readonly Stream _stream;

		private readonly Queue<WriteData> _queue = new Queue<WriteData>();

		private int _pendingWrite;

		private bool _disposed;

		public override bool CanRead => _stream.CanRead;

		public override bool CanSeek => _stream.CanSeek;

		public override bool CanWrite => _stream.CanWrite;

		public override long Length => _stream.Length;

		public override long Position
		{
			get
			{
				return _stream.Position;
			}
			set
			{
				_stream.Position = value;
			}
		}

		public QueuedStream(Stream stream)
		{
			_stream = stream;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return _stream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return _stream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			_stream.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("QueuedStream does not support synchronous write operations yet.");
		}

		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return _stream.BeginRead(buffer, offset, count, callback, state);
		}

		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			lock (_queue)
			{
				WriteData writeData = new WriteData(buffer, offset, count, callback, state);
				if (_pendingWrite > 0)
				{
					_queue.Enqueue(writeData);
					return writeData.AsyncResult;
				}
				return BeginWriteInternal(buffer, offset, count, callback, state, writeData);
			}
		}

		public override int EndRead(IAsyncResult asyncResult)
		{
			return _stream.EndRead(asyncResult);
		}

		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult is QueuedWriteResult)
			{
				QueuedWriteResult queuedWriteResult = asyncResult as QueuedWriteResult;
				if (queuedWriteResult.Exception != null)
				{
					throw queuedWriteResult.Exception;
				}
				if (queuedWriteResult.ActualResult == null)
				{
					throw new NotSupportedException("QueuedStream does not support synchronous write operations. Please wait for callback to be invoked before calling EndWrite.");
				}
				return;
			}
			throw new ArgumentException();
		}

		public override void Flush()
		{
			_stream.Flush();
		}

		public override void Close()
		{
			_stream.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_stream.Dispose();
				}
				_disposed = true;
			}
			base.Dispose(disposing);
		}

		private IAsyncResult BeginWriteInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, WriteData queued)
		{
			_pendingWrite++;
			IAsyncResult actualResult = _stream.BeginWrite(buffer, offset, count, delegate(IAsyncResult ar)
			{
				queued.AsyncResult.ActualResult = ar;
				try
				{
					_stream.EndWrite(ar);
				}
				catch (Exception exception)
				{
					queued.AsyncResult.Exception = exception;
				}
				lock (_queue)
				{
					_pendingWrite--;
					while (_queue.Count > 0)
					{
						WriteData writeData = _queue.Dequeue();
						try
						{
							writeData.AsyncResult.ActualResult = BeginWriteInternal(writeData.Buffer, writeData.Offset, writeData.Count, writeData.Callback, writeData.State, writeData);
						}
						catch (Exception exception2)
						{
							_pendingWrite--;
							writeData.AsyncResult.Exception = exception2;
							writeData.Callback(writeData.AsyncResult);
							continue;
						}
						break;
					}
					callback(queued.AsyncResult);
				}
			}, state);
			queued.AsyncResult.ActualResult = actualResult;
			return queued.AsyncResult;
		}
	}
	public class RequestParser
	{
		private const string pattern = "^(?<method>[^\\s]+)\\s(?<path>[^\\s]+)\\sHTTP\\/1\\.1\\r\\n((?<field_name>[^:\\r\\n]+):[^\\S\\r\\n]*(?<field_value>[^\\r\\n]*)\\r\\n)+";

		private static readonly Regex _regex = new Regex("^(?<method>[^\\s]+)\\s(?<path>[^\\s]+)\\sHTTP\\/1\\.1\\r\\n((?<field_name>[^:\\r\\n]+):[^\\S\\r\\n]*(?<field_value>[^\\r\\n]*)\\r\\n)+", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

		public static WebSocketHttpRequest Parse(ArraySegment<byte> bytes)
		{
			return Parse(bytes, "ws");
		}

		public static WebSocketHttpRequest Parse(ArraySegment<byte> bytes, string scheme)
		{
			if (bytes.Count > 4096)
			{
				return null;
			}
			string input = Encoding.UTF8.GetString(bytes.Array, bytes.Offset, bytes.Count);
			Match match = _regex.Match(input);
			if (!match.Success)
			{
				return null;
			}
			WebSocketHttpRequest webSocketHttpRequest = new WebSocketHttpRequest
			{
				Method = match.Groups["method"].Value,
				Path = match.Groups["path"].Value,
				Scheme = scheme
			};
			CaptureCollection captures = match.Groups["field_name"].Captures;
			CaptureCollection captures2 = match.Groups["field_value"].Captures;
			for (int i = 0; i < captures.Count; i++)
			{
				string key = captures[i].ToString();
				string value = captures2[i].ToString();
				webSocketHttpRequest.Headers[key] = value;
			}
			return webSocketHttpRequest;
		}
	}
	public class SocketWrapper : ISocket
	{
		public const uint KeepAliveInterval = 60000u;

		public const uint RetryInterval = 10000u;

		private static readonly byte[] keepAliveValues;

		private readonly Socket _socket;

		private CancellationTokenSource _tokenSource;

		private TaskFactory _taskFactory;

		public IPAddress RemoteIpAddress => (_socket.RemoteEndPoint as IPEndPoint)?.Address;

		public int RemotePort
		{
			get
			{
				if (!(_socket.RemoteEndPoint is IPEndPoint iPEndPoint))
				{
					return -1;
				}
				return iPEndPoint.Port;
			}
		}

		public bool Connected => _socket.Connected;

		public Stream Stream { get; private set; }

		public bool NoDelay
		{
			get
			{
				return _socket.NoDelay;
			}
			set
			{
				_socket.NoDelay = value;
			}
		}

		public EndPoint LocalEndPoint => _socket.LocalEndPoint;

		static SocketWrapper()
		{
			byte[] destinationArray = new byte[12];
			Array.Copy(BitConverter.GetBytes(1u), 0, destinationArray, 0, 4);
			Array.Copy(BitConverter.GetBytes(60000u), 0, destinationArray, 4, 4);
			Array.Copy(BitConverter.GetBytes(10000u), 0, destinationArray, 8, 4);
			keepAliveValues = destinationArray;
		}

		public SocketWrapper(Socket socket)
		{
			_tokenSource = new CancellationTokenSource();
			_taskFactory = new TaskFactory(_tokenSource.Token);
			_socket = socket;
			if (_socket.Connected)
			{
				Stream = new NetworkStream(_socket);
			}
			if (FleckRuntime.IsRunningOnWindows())
			{
				socket.IOControl(IOControlCode.KeepAliveValues, keepAliveValues, null);
			}
		}

		public Task Authenticate(X509Certificate2 certificate, SslProtocols enabledSslProtocols, Action callback, Action<Exception> error)
		{
			SslStream ssl = new SslStream(Stream, leaveInnerStreamOpen: false);
			Stream = new QueuedStream(ssl);
			Func<AsyncCallback, object, IAsyncResult> beginMethod = (AsyncCallback cb, object s) => ssl.BeginAuthenticateAsServer(certificate, clientCertificateRequired: false, enabledSslProtocols, checkCertificateRevocation: false, cb, s);
			Task task = Task.Factory.FromAsync(beginMethod, ssl.EndAuthenticateAsServer, null);
			task.ContinueWith(delegate
			{
				callback();
			}, TaskContinuationOptions.NotOnFaulted).ContinueWith(delegate(Task t)
			{
				error(t.Exception);
			}, TaskContinuationOptions.OnlyOnFaulted);
			task.ContinueWith(delegate(Task t)
			{
				error(t.Exception);
			}, TaskContinuationOptions.OnlyOnFaulted);
			return task;
		}

		public void Listen(int backlog)
		{
			_socket.Listen(backlog);
		}

		public void Bind(EndPoint endPoint)
		{
			_socket.Bind(endPoint);
		}

		public Task<ISocket> Accept(Action<ISocket> callback, Action<Exception> error)
		{
			Func<IAsyncResult, ISocket> endMethod = (IAsyncResult r) => (!_tokenSource.Token.IsCancellationRequested) ? new SocketWrapper(_socket.EndAccept(r)) : null;
			Task<ISocket> task = _taskFactory.FromAsync(_socket.BeginAccept, endMethod, null);
			task.ContinueWith(delegate(Task<ISocket> t)
			{
				callback(t.Result);
			}, TaskContinuationOptions.OnlyOnRanToCompletion).ContinueWith(delegate(Task t)
			{
				error(t.Exception);
			}, TaskContinuationOptions.OnlyOnFaulted);
			task.ContinueWith(delegate(Task<ISocket> t)
			{
				error(t.Exception);
			}, TaskContinuationOptions.OnlyOnFaulted);
			return task;
		}

		public void Dispose()
		{
			_tokenSource.Cancel();
			if (Stream != null)
			{
				Stream.Dispose();
			}
			if (_socket != null)
			{
				_socket.Dispose();
			}
		}

		public void Close()
		{
			_tokenSource.Cancel();
			if (Stream != null)
			{
				Stream.Close();
			}
			if (_socket != null)
			{
				_socket.Close();
			}
		}
	}
	internal ref struct SpanWriter
	{
		private readonly Span<byte> _data;

		public int Length { get; private set; }

		public SpanWriter(Span<byte> data)
		{
			_data = data;
			Length = 0;
		}

		public unsafe void Write<T>(T value, bool reverse = true) where T : unmanaged
		{
			if (Length + sizeof(T) >= _data.Length)
			{
				throw new ArgumentException("Cannot write past end of span");
			}
			Span<byte> span = new Span<byte>(&value, sizeof(T));
			if (reverse)
			{
				span.Reverse();
			}
			Span<byte> destination = _data.Slice(Length, sizeof(T));
			span.CopyTo(destination);
			Length += sizeof(T);
		}

		public unsafe void Write<T>(Span<T> values) where T : unmanaged
		{
			int num = sizeof(T) * values.Length;
			if (Length + num >= _data.Length)
			{
				throw new ArgumentException("Cannot write past end of span");
			}
			fixed (T* pointer = &values[0])
			{
				Span<byte> span = new Span<byte>(pointer, num);
				Span<byte> destination = _data.Slice(Length, num);
				span.CopyTo(destination);
				Length += num;
			}
		}
	}
	public class WebSocketConnection : IWebSocketConnection
	{
		private const int ReadSize = 8192;

		private readonly Action<IWebSocketConnection> _initialize;

		private readonly Func<IWebSocketConnection, WebSocketHttpRequest, IHandler> _handlerFactory;

		private readonly Func<ArraySegment<byte>, WebSocketHttpRequest> _parseRequest;

		private int _pendingSendCount;

		private byte[] _receiveBuffer;

		private int _receiveOffset;

		private bool _closing;

		private bool _closed;

		public ISocket Socket { get; set; }

		public IHandler Handler { get; set; }

		public Action OnOpen { get; set; }

		public Action OnClose { get; set; }

		public Action<string> OnMessage { get; set; }

		public BinaryDataHandler OnBinary { get; set; }

		public BinaryDataHandler OnPing { get; set; }

		public BinaryDataHandler OnPong { get; set; }

		public Action<Exception> OnError { get; set; }

		public IWebSocketConnectionInfo ConnectionInfo { get; private set; }

		public bool IsAvailable
		{
			get
			{
				if (!_closing && !_closed)
				{
					return Socket.Connected;
				}
				return false;
			}
		}

		public WebSocketConnection(ISocket socket, Action<IWebSocketConnection> initialize, Func<ArraySegment<byte>, WebSocketHttpRequest> parseRequest, Func<IWebSocketConnection, WebSocketHttpRequest, IHandler> handlerFactory)
		{
			Socket = socket;
			OnOpen = delegate
			{
			};
			OnClose = delegate
			{
			};
			OnMessage = delegate
			{
			};
			OnBinary = delegate
			{
			};
			OnPing = delegate
			{
			};
			OnPong = delegate
			{
			};
			OnError = delegate
			{
			};
			_initialize = initialize;
			_handlerFactory = handlerFactory;
			_parseRequest = parseRequest;
		}

		public void Send(string message)
		{
			SendImpl(Handler.FrameText(message));
		}

		public void SendText(MemoryBuffer utf8StringBytes)
		{
			SendImpl(Handler.FrameText(utf8StringBytes));
		}

		public void Send(MemoryBuffer message)
		{
			SendImpl(Handler.FrameBinary(message));
		}

		public void SendPing(MemoryBuffer message)
		{
			SendImpl(Handler.FramePing(message));
		}

		public void SendPong(MemoryBuffer message)
		{
			SendImpl(Handler.FramePong(message));
		}

		private void SendImpl(MemoryBuffer buffer)
		{
			if (Handler == null)
			{
				throw new InvalidOperationException("Cannot send before handshake");
			}
			if (!IsAvailable)
			{
				FleckLog.Warn("Data sent while closing or after close. Ignoring.");
			}
			else
			{
				SendBytes(buffer);
			}
		}

		public void Close()
		{
			Close(1000);
		}

		public void Close(ushort code)
		{
			if (_closing || _closed)
			{
				return;
			}
			_closing = true;
			if (Handler == null || !Socket.Connected)
			{
				CloseSocket();
				return;
			}
			MemoryBuffer bytes = Handler.FrameClose(code);
			if (bytes.Length == 0)
			{
				CloseSocket();
				return;
			}
			SendBytes(bytes, delegate(WebSocketConnection i, bool s)
			{
				i.CloseSocket();
			});
		}

		public bool CreateHandler(ArraySegment<byte> data)
		{
			WebSocketHttpRequest webSocketHttpRequest = _parseRequest(data);
			if (webSocketHttpRequest == null)
			{
				return false;
			}
			Handler = _handlerFactory(this, webSocketHttpRequest);
			if (Handler == null)
			{
				return false;
			}
			ConnectionInfo = WebSocketConnectionInfo.Create(webSocketHttpRequest, Socket.RemoteIpAddress, Socket.RemotePort);
			_initialize(this);
			MemoryBuffer bytes = Handler.CreateHandshake();
			SendBytes(bytes, delegate(WebSocketConnection instance, bool success)
			{
				if (success)
				{
					instance.OnOpen();
				}
			});
			return true;
		}

		public void StartReceiving()
		{
			if (IsAvailable)
			{
				if (_receiveBuffer == null)
				{
					_receiveBuffer = ArrayPool<byte>.Shared.Rent(8192);
				}
				Receive(_receiveBuffer, 0);
			}
		}

		private void HandleReadSuccess(int bytesRead)
		{
			if (bytesRead <= 0)
			{
				CloseSocket();
				return;
			}
			Span<byte> newData = new Span<byte>(_receiveBuffer, 0, bytesRead);
			if (Handler != null)
			{
				Handler.Receive(newData);
				Receive(_receiveBuffer, 0);
			}
			else
			{
				_receiveOffset += bytesRead;
				bool flag = CreateHandler(new ArraySegment<byte>(_receiveBuffer, 0, _receiveOffset));
				Receive(_receiveBuffer, (!flag) ? _receiveOffset : 0);
			}
		}

		private void HandleReadError(Exception e)
		{
			if (e is AggregateException)
			{
				AggregateException ex = e as AggregateException;
				HandleReadError(ex.InnerException);
				return;
			}
			if (e is ObjectDisposedException)
			{
				FleckLog.Warn("Swallowing ObjectDisposedException", e);
				return;
			}
			OnError(e);
			if (e is WebSocketException)
			{
				Close(((WebSocketException)e).StatusCode);
				return;
			}
			if (e is IOException)
			{
				Close(1006);
				return;
			}
			FleckLog.Error("Application Error", e);
			Close(1011);
		}

		private void Receive(byte[] buffer, int offset)
		{
			try
			{
				Socket.Stream.BeginRead(buffer, offset, buffer.Length - offset, delegate(IAsyncResult result)
				{
					WebSocketConnection webSocketConnection = (WebSocketConnection)result.AsyncState;
					try
					{
						int bytesRead = webSocketConnection.Socket.Stream.EndRead(result);
						webSocketConnection.HandleReadSuccess(bytesRead);
					}
					catch (Exception e2)
					{
						webSocketConnection.HandleReadError(e2);
					}
				}, this);
			}
			catch (Exception e)
			{
				HandleReadError(e);
			}
		}

		private void HandleWriteError(Exception e)
		{
			_ = e is IOException;
			OnError(e);
			CloseSocket();
		}

		private void SendBytes(MemoryBuffer bytes, Action<WebSocketConnection, bool> callback = null)
		{
			try
			{
				if (Interlocked.Increment(ref _pendingSendCount) > 2048)
				{
					bytes.Dispose();
					throw new InvalidOperationException("Too many pending sends on WebSocket connection. Disconnecting.");
				}
				Socket.Stream.BeginWrite(bytes.Data, 0, bytes.Length, delegate(IAsyncResult result)
				{
					WebSocketConnection webSocketConnection = (WebSocketConnection)result.AsyncState;
					bool arg = false;
					try
					{
						DecrementPending();
						webSocketConnection.Socket.Stream.EndWrite(result);
						arg = true;
					}
					catch (Exception e)
					{
						webSocketConnection.HandleWriteError(e);
					}
					finally
					{
						bytes.Dispose();
					}
					try
					{
						callback?.Invoke(webSocketConnection, arg);
					}
					catch (Exception obj)
					{
						webSocketConnection.OnError(obj);
					}
				}, this);
			}
			catch (Exception ex)
			{
				if (!(ex is InvalidOperationException))
				{
					DecrementPending();
				}
				HandleWriteError(ex);
			}
			void DecrementPending()
			{
				if (Interlocked.Decrement(ref _pendingSendCount) < 0)
				{
					FleckLog.Error("Pending send count on WebSocket connection has gone negative! Trying to fix it...");
					_pendingSendCount = 0;
				}
			}
		}

		private void CloseSocket()
		{
			_closing = true;
			OnClose();
			_closed = true;
			Socket.Close();
			Socket.Dispose();
			_closing = false;
			_pendingSendCount = 0;
			Handler?.Dispose();
			Handler = null;
			if (_receiveBuffer != null)
			{
				ArrayPool<byte>.Shared.Return(_receiveBuffer);
				_receiveBuffer = null;
			}
		}
	}
	public class WebSocketConnectionInfo : IWebSocketConnectionInfo
	{
		public string SubProtocol { get; private set; }

		public string Origin { get; private set; }

		public string Host { get; private set; }

		public string Path { get; private set; }

		public IPAddress ClientIpAddress { get; set; }

		public int ClientPort { get; set; }

		public Guid Id { get; set; }

		public static WebSocketConnectionInfo Create(WebSocketHttpRequest request, IPAddress clientIp, int clientPort)
		{
			return new WebSocketConnectionInfo
			{
				Origin = (request["Origin"] ?? request["Sec-WebSocket-Origin"]),
				Host = request["Host"],
				SubProtocol = request["Sec-WebSocket-Protocol"],
				Path = request.Path,
				ClientIpAddress = clientIp,
				ClientPort = clientPort
			};
		}

		private WebSocketConnectionInfo()
		{
			Id = Guid.NewGuid();
		}
	}
	public class WebSocketException : Exception
	{
		public ushort StatusCode { get; private set; }

		public WebSocketException(ushort statusCode)
		{
			StatusCode = statusCode;
		}

		public WebSocketException(ushort statusCode, string message)
			: base(message)
		{
			StatusCode = statusCode;
		}

		public WebSocketException(ushort statusCode, string message, Exception innerException)
			: base(message, innerException)
		{
			StatusCode = statusCode;
		}
	}
	public class WebSocketHttpRequest
	{
		public string Method { get; set; }

		public string Path { get; set; }

		public string Scheme { get; set; }

		public string this[string name]
		{
			get
			{
				if (!Headers.TryGetValue(name, out var value))
				{
					return null;
				}
				return value;
			}
		}

		public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
	}
	public class WebSocketServer : IWebSocketServer, IDisposable
	{
		private readonly string _scheme;

		private readonly IPAddress _locationIP;

		private Action<IWebSocketConnection> _config;

		public ISocket ListenerSocket { get; set; }

		public string Location { get; }

		public bool SupportDualStack { get; }

		public int Port { get; private set; }

		public X509Certificate2 Certificate { get; set; }

		public SslProtocols EnabledSslProtocols { get; set; }

		public bool RestartAfterListenError { get; set; }

		public bool IsSecure
		{
			get
			{
				if (_scheme == "wss")
				{
					return Certificate != null;
				}
				return false;
			}
		}

		public WebSocketServer(string location, bool supportDualStack = true)
		{
			Uri uri = new Uri(location);
			Port = uri.Port;
			Location = location;
			SupportDualStack = supportDualStack;
			_locationIP = ParseIPAddress(uri);
			_scheme = uri.Scheme;
			Socket socket = new Socket(_locationIP.AddressFamily, SocketType.Stream, ProtocolType.IP);
			if (SupportDualStack && !FleckRuntime.IsRunningOnMono() && FleckRuntime.IsRunningOnWindows())
			{
				socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, optionValue: false);
				socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
			}
			ListenerSocket = new SocketWrapper(socket);
		}

		public void Dispose()
		{
			ListenerSocket.Dispose();
		}

		private IPAddress ParseIPAddress(Uri uri)
		{
			string host = uri.Host;
			if (host == "0.0.0.0")
			{
				return IPAddress.Any;
			}
			if (host == "[0000:0000:0000:0000:0000:0000:0000:0000]")
			{
				return IPAddress.IPv6Any;
			}
			try
			{
				return IPAddress.Parse(host);
			}
			catch (Exception innerException)
			{
				throw new FormatException("Failed to parse the IP address part of the location. Please make sure you specify a valid IP address. Use 0.0.0.0 or [::] to listen on all interfaces.", innerException);
			}
		}

		public void Start(Action<IWebSocketConnection> config)
		{
			IPEndPoint ipLocal = new IPEndPoint(_locationIP, Port);
			ListenerSocket.Bind(ipLocal);
			ListenerSocket.Listen(100);
			Port = ((IPEndPoint)ListenerSocket.LocalEndPoint).Port;
			if (_scheme == "wss" && Certificate == null)
			{
				FleckLog.Error("Scheme cannot be 'wss' without a Certificate");
				return;
			}
			ListenForClients();
			_config = config;
		}

		private void ListenForClients()
		{
			ListenerSocket.Accept(OnClientConnect, delegate(Exception e)
			{
				FleckLog.Error("Listener socket is closed", e);
				if (RestartAfterListenError)
				{
					try
					{
						ListenerSocket.Dispose();
						Socket socket = new Socket(_locationIP.AddressFamily, SocketType.Stream, ProtocolType.IP);
						ListenerSocket = new SocketWrapper(socket);
						Start(_config);
					}
					catch (Exception ex)
					{
						FleckLog.Error("Listener could not be restarted", ex);
					}
				}
			});
		}

		private void OnClientConnect(ISocket clientSocket)
		{
			if (clientSocket == null)
			{
				return;
			}
			ListenForClients();
			WebSocketConnection webSocketConnection = null;
			webSocketConnection = new WebSocketConnection(clientSocket, _config, (ArraySegment<byte> bytes) => RequestParser.Parse(bytes, _scheme), (IWebSocketConnection c, WebSocketHttpRequest r) => HandlerFactory.BuildHandler(r, c));
			if (IsSecure)
			{
				clientSocket.Authenticate(Certificate, EnabledSslProtocols, webSocketConnection.StartReceiving, delegate(Exception e)
				{
					FleckLog.Warn("Failed to Authenticate", e);
				});
			}
			else
			{
				webSocketConnection.StartReceiving();
			}
		}
	}
	public static class WebSocketStatusCodes
	{
		public const ushort NormalClosure = 1000;

		public const ushort GoingAway = 1001;

		public const ushort ProtocolError = 1002;

		public const ushort UnsupportedDataType = 1003;

		public const ushort NoStatusReceived = 1005;

		public const ushort AbnormalClosure = 1006;

		public const ushort InvalidFramePayloadData = 1007;

		public const ushort PolicyViolation = 1008;

		public const ushort MessageTooBig = 1009;

		public const ushort MandatoryExt = 1010;

		public const ushort InternalServerError = 1011;

		public const ushort TLSHandshake = 1015;

		public const ushort ApplicationError = 3000;

		public static ushort[] ValidCloseCodes = new ushort[9] { 1000, 1001, 1002, 1003, 1007, 1008, 1009, 1010, 1011 };
	}
}
namespace Fleck.Helpers
{
	internal static class FleckRuntime
	{
		public static bool IsRunningOnMono()
		{
			return Type.GetType("Mono.Runtime") != null;
		}

		public static bool IsRunningOnWindows()
		{
			return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
		}
	}
}
namespace Fleck.Handlers
{
	internal class Hybi13Handler : IHandler, IDisposable
	{
		private const string WebSocketResponseGuid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

		private static readonly Encoding UTF8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

		private static readonly SHA1 SHA1 = SHA1.Create();

		private static readonly ThreadLocal<StringBuilder> StringBuilder = new ThreadLocal<StringBuilder>(() => new StringBuilder(1024));

		private readonly WebSocketHttpRequest _request;

		private readonly IWebSocketConnection _connection;

		private byte[] _data;

		private int _dataLen;

		private FrameType? _frameType;

		private byte[] _message;

		private int _messageLen;

		public Hybi13Handler(WebSocketHttpRequest request, IWebSocketConnection connection)
		{
			_request = request;
			_connection = connection;
			_data = ArrayPool<byte>.Shared.Rent(1048576);
			_dataLen = 0;
			_frameType = null;
			_message = ArrayPool<byte>.Shared.Rent(1048576);
			_messageLen = 0;
		}

		public void Dispose()
		{
			if (_data != null)
			{
				ArrayPool<byte>.Shared.Return(_data);
				_data = null;
			}
			if (_message != null)
			{
				ArrayPool<byte>.Shared.Return(_message);
				_message = null;
			}
		}

		public void Receive(Span<byte> newData)
		{
			if (newData.Length + _dataLen >= _data.Length)
			{
				throw new WebSocketException(1009);
			}
			Span<byte> destination = new Span<byte>(_data, _dataLen, newData.Length);
			newData.CopyTo(destination);
			_dataLen += newData.Length;
			ReceiveData();
		}

		public MemoryBuffer CreateHandshake()
		{
			StringBuilder value = StringBuilder.Value;
			value.Clear();
			value.Append("HTTP/1.1 101 Switching Protocols\r\n");
			value.Append("Upgrade: websocket\r\n");
			value.Append("Connection: Upgrade\r\n");
			string arg = CreateResponseKey(_request["Sec-WebSocket-Key"]);
			value.AppendFormat("Sec-WebSocket-Accept: {0}\r\n", arg);
			value.Append("\r\n");
			byte[] bytes = UTF8.GetBytes(value.ToString());
			return new MemoryBuffer(bytes, bytes.Length, fromPool: false);
		}

		public MemoryBuffer FrameText(string text)
		{
			byte[] bytes = UTF8.GetBytes(text);
			MemoryBuffer utf8StringBytes = new MemoryBuffer(bytes, bytes.Length, fromPool: false);
			return FrameText(utf8StringBytes);
		}

		public MemoryBuffer FrameText(MemoryBuffer utf8StringBytes)
		{
			return FrameData(utf8StringBytes, FrameType.Text);
		}

		public MemoryBuffer FrameBinary(MemoryBuffer bytes)
		{
			return FrameData(bytes, FrameType.Binary);
		}

		public MemoryBuffer FramePing(MemoryBuffer bytes)
		{
			return FrameData(bytes, FrameType.Ping);
		}

		public MemoryBuffer FramePong(MemoryBuffer bytes)
		{
			return FrameData(bytes, FrameType.Pong);
		}

		public unsafe MemoryBuffer FrameClose(ushort code)
		{
			Span<byte> span = new Span<byte>(&code, 2);
			span.Reverse();
			return FrameData(span, FrameType.Close);
		}

		private static MemoryBuffer FrameData(MemoryBuffer payload, FrameType frameType)
		{
			MemoryBuffer result = FrameData((Span<byte>)payload, frameType);
			payload.Dispose();
			return result;
		}

		private static MemoryBuffer FrameData(Span<byte> payload, FrameType frameType)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(payload.Length + 16);
			SpanWriter spanWriter = new SpanWriter(array);
			byte value = (byte)(frameType + 128);
			spanWriter.Write(value);
			if (payload.Length > 65535)
			{
				spanWriter.Write((byte)127, reverse: true);
				spanWriter.Write((ulong)payload.Length);
			}
			else if (payload.Length > 125)
			{
				spanWriter.Write((byte)126, reverse: true);
				spanWriter.Write((ushort)payload.Length);
			}
			else
			{
				spanWriter.Write((byte)payload.Length);
			}
			if (payload.Length > 0)
			{
				spanWriter.Write(payload);
			}
			return new MemoryBuffer(array, spanWriter.Length);
		}

		private void ReceiveData()
		{
			while (_dataLen >= 2)
			{
				bool flag = (_data[0] & 0x80) != 0;
				int num = _data[0] & 0x70;
				FrameType frameType = (FrameType)(_data[0] & 0xF);
				bool num2 = (_data[1] & 0x80) != 0;
				int num3 = _data[1] & 0x7F;
				if (!num2 || !frameType.IsDefined() || num != 0 || (frameType == FrameType.Continuation && !_frameType.HasValue))
				{
					throw new WebSocketException(1002);
				}
				int num4 = 2;
				int num5;
				switch (num3)
				{
				case 127:
					if (_dataLen < num4 + 8)
					{
						return;
					}
					num5 = new Span<byte>(_data, num4, 8).ToLittleEndianInt();
					num4 += 8;
					break;
				case 126:
					if (_dataLen < num4 + 2)
					{
						return;
					}
					num5 = new Span<byte>(_data, num4, 2).ToLittleEndianInt();
					num4 += 2;
					break;
				default:
					num5 = num3;
					break;
				}
				if (_dataLen < num4 + 4)
				{
					break;
				}
				Span<byte> span = new Span<byte>(_data, num4, 4);
				num4 += 4;
				if (_dataLen < num4 + num5)
				{
					break;
				}
				Span<byte> span2 = new Span<byte>(_data, num4, num5);
				for (int i = 0; i < num5; i++)
				{
					span2[i] ^= span[i % 4];
				}
				if (_messageLen + num5 > _message.Length)
				{
					throw new WebSocketException(1009);
				}
				Span<byte> destination = new Span<byte>(_message, _messageLen, num5);
				span2.CopyTo(destination);
				_messageLen += num5;
				int num6 = num4 + num5;
				Buffer.BlockCopy(_data, num6, _data, 0, _dataLen - num6);
				_dataLen -= num4 + num5;
				if (frameType != FrameType.Continuation)
				{
					_frameType = frameType;
				}
				if (flag && _frameType.HasValue)
				{
					ProcessFrame(_frameType.Value, new ArraySegment<byte>(_message, 0, _messageLen));
					Clear();
				}
			}
		}

		private void ProcessFrame(FrameType frameType, ArraySegment<byte> buffer)
		{
			switch (frameType)
			{
			case FrameType.Close:
				if (buffer.Count == 1 || buffer.Count > 125)
				{
					throw new WebSocketException(1002);
				}
				if (buffer.Count >= 2)
				{
					ushort num = (ushort)new Span<byte>(buffer.Array, 0, 2).ToLittleEndianInt();
					if (!WebSocketStatusCodes.ValidCloseCodes.Contains(num) && (num < 3000 || num > 4999))
					{
						throw new WebSocketException(1002);
					}
				}
				_connection.OnClose?.Invoke();
				break;
			case FrameType.Binary:
				_connection.OnBinary?.Invoke(buffer);
				break;
			case FrameType.Ping:
				_connection.OnPing?.Invoke(buffer);
				break;
			case FrameType.Pong:
				_connection.OnPong?.Invoke(buffer);
				break;
			case FrameType.Text:
				_connection.OnMessage?.Invoke(ReadUTF8PayloadData(buffer));
				break;
			}
		}

		private void Clear()
		{
			_frameType = null;
			_messageLen = 0;
		}

		internal static string CreateResponseKey(string requestKey)
		{
			string s = requestKey + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
			return Convert.ToBase64String(SHA1.ComputeHash(Encoding.ASCII.GetBytes(s)));
		}

		internal static string ReadUTF8PayloadData(ArraySegment<byte> bytes)
		{
			try
			{
				return UTF8.GetString(bytes.Array, bytes.Offset, bytes.Count);
			}
			catch (ArgumentException)
			{
				throw new WebSocketException(1007);
			}
		}
	}
}
