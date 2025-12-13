using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Security;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;
using Mono.Security.Interface;
using Unity;

[assembly: NeutralResourcesLanguage("en-US")]
[assembly: ComVisible(false)]
[assembly: InternalsVisibleTo("System.Net.Http.WebRequest, PublicKey=002400000480000094000000060200000024000052534131000400000100010007d1fa57c4aed9f0a32e84aa0faefd0de9e8fd6aec8f87fb03766c834c99921eb23be79ad9d5dcc1dd9ad236132102900b723cf980957fc4e177108fc607774f29e8320e92ea05ece4e821c0a5efe8f1645c4c0c93c1ab99285d622caa652c1dfad63d745d6f2de5f17e5eaf0fc4963d261c8a12436518206dc093344d5ad293")]
[assembly: AssemblyDelaySign(true)]
[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: CLSCompliant(true)]
[assembly: AssemblyTitle("System.Net.Http.dll")]
[assembly: AssemblyDescription("System.Net.Http.dll")]
[assembly: AssemblyDefaultAlias("System.Net.Http.dll")]
[assembly: AssemblyCompany("Mono development team")]
[assembly: AssemblyProduct("Mono Common Language Infrastructure")]
[assembly: AssemblyCopyright("(c) Various Mono authors")]
[assembly: SatelliteContractVersion("4.0.0.0")]
[assembly: AssemblyInformationalVersion("4.6.57.0")]
[assembly: AssemblyFileVersion("4.6.57.0")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("4.0.0.0")]
[module: UnverifiableCode]
namespace System.Net.Http
{
	internal static class ConnectHelper
	{
		internal sealed class CertificateCallbackMapper
		{
			public readonly Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> FromHttpClientHandler;

			public readonly RemoteCertificateValidationCallback ForSocketsHttpHandler;

			public CertificateCallbackMapper(Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> fromHttpClientHandler)
			{
				FromHttpClientHandler = fromHttpClientHandler;
				ForSocketsHttpHandler = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => FromHttpClientHandler(new HttpRequestMessage(HttpMethod.Get, (string)sender), certificate as X509Certificate2, chain, sslPolicyErrors);
			}
		}

		private sealed class ConnectEventArgs : SocketAsyncEventArgs
		{
			public AsyncTaskMethodBuilder Builder { get; private set; }

			public CancellationToken CancellationToken { get; private set; }

			public void Initialize(CancellationToken cancellationToken)
			{
				CancellationToken = cancellationToken;
				AsyncTaskMethodBuilder builder = default(AsyncTaskMethodBuilder);
				_ = builder.Task;
				Builder = builder;
			}

			public void Clear()
			{
				CancellationToken = default(CancellationToken);
			}

			protected override void OnCompleted(SocketAsyncEventArgs _)
			{
				switch (base.SocketError)
				{
				case SocketError.Success:
					Builder.SetResult();
					return;
				case SocketError.OperationAborted:
				case SocketError.ConnectionAborted:
					if (CancellationToken.IsCancellationRequested)
					{
						Builder.SetException(CancellationHelper.CreateOperationCanceledException(null, CancellationToken));
						return;
					}
					break;
				}
				Builder.SetException(new SocketException((int)base.SocketError));
			}
		}

		private static readonly ConcurrentQueue<ConnectEventArgs>.Segment s_connectEventArgs = new ConcurrentQueue<ConnectEventArgs>.Segment(ConcurrentQueue<ConnectEventArgs>.Segment.RoundUpToPowerOf2(Math.Max(2, Environment.ProcessorCount)));

		public static async ValueTask<(Socket, Stream)> ConnectAsync(string host, int port, CancellationToken cancellationToken)
		{
			if (!s_connectEventArgs.TryDequeue(out var saea))
			{
				saea = new ConnectEventArgs();
			}
			try
			{
				saea.Initialize(cancellationToken);
				saea.RemoteEndPoint = new DnsEndPoint(host, port);
				if (Socket.ConnectAsync(SocketType.Stream, ProtocolType.Tcp, saea))
				{
					using (cancellationToken.Register(delegate(object s)
					{
						Socket.CancelConnectAsync((SocketAsyncEventArgs)s);
					}, saea))
					{
						await saea.Builder.Task.ConfigureAwait(continueOnCapturedContext: false);
					}
				}
				else if (saea.SocketError != SocketError.Success)
				{
					throw new SocketException((int)saea.SocketError);
				}
				Socket connectSocket = saea.ConnectSocket;
				connectSocket.NoDelay = true;
				return (connectSocket, new NetworkStream(connectSocket, ownsSocket: true));
			}
			catch (Exception ex)
			{
				throw CancellationHelper.ShouldWrapInOperationCanceledException(ex, cancellationToken) ? CancellationHelper.CreateOperationCanceledException(ex, cancellationToken) : new HttpRequestException(ex.Message, ex);
			}
			finally
			{
				saea.Clear();
				if (!s_connectEventArgs.TryEnqueue(saea))
				{
					saea.Dispose();
				}
			}
		}

		public static ValueTask<SslStream> EstablishSslConnectionAsync(SslClientAuthenticationOptions sslOptions, HttpRequestMessage request, Stream stream, CancellationToken cancellationToken)
		{
			RemoteCertificateValidationCallback remoteCertificateValidationCallback = sslOptions.RemoteCertificateValidationCallback;
			if (remoteCertificateValidationCallback != null && remoteCertificateValidationCallback.Target is CertificateCallbackMapper certificateCallbackMapper)
			{
				sslOptions = sslOptions.ShallowClone();
				Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> localFromHttpClientHandler = certificateCallbackMapper.FromHttpClientHandler;
				HttpRequestMessage localRequest = request;
				sslOptions.RemoteCertificateValidationCallback = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => localFromHttpClientHandler(localRequest, certificate as X509Certificate2, chain, sslPolicyErrors);
			}
			return EstablishSslConnectionAsyncCore(stream, sslOptions, cancellationToken);
		}

		private static async ValueTask<SslStream> EstablishSslConnectionAsyncCore(Stream stream, SslClientAuthenticationOptions sslOptions, CancellationToken cancellationToken)
		{
			SslStream sslStream = new SslStream(stream);
			CancellationTokenRegistration ctr = cancellationToken.Register(delegate(object s)
			{
				((Stream)s).Dispose();
			}, stream);
			try
			{
				await sslStream.AuthenticateAsClientAsync(sslOptions, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			catch (Exception ex)
			{
				sslStream.Dispose();
				if (CancellationHelper.ShouldWrapInOperationCanceledException(ex, cancellationToken))
				{
					throw CancellationHelper.CreateOperationCanceledException(ex, cancellationToken);
				}
				throw new HttpRequestException("The SSL connection could not be established, see inner exception.", ex);
			}
			finally
			{
				ctr.Dispose();
			}
			if (cancellationToken.IsCancellationRequested)
			{
				sslStream.Dispose();
				throw CancellationHelper.CreateOperationCanceledException(null, cancellationToken);
			}
			return sslStream;
		}
	}
	internal static class HttpUtilities
	{
		internal static Version DefaultRequestVersion => HttpVersion.Version20;

		internal static Version DefaultResponseVersion => HttpVersion.Version11;

		internal static bool IsHttpUri(Uri uri)
		{
			return IsSupportedScheme(uri.Scheme);
		}

		internal static bool IsSupportedScheme(string scheme)
		{
			if (!IsSupportedNonSecureScheme(scheme))
			{
				return IsSupportedSecureScheme(scheme);
			}
			return true;
		}

		internal static bool IsSupportedNonSecureScheme(string scheme)
		{
			if (!string.Equals(scheme, "http", StringComparison.OrdinalIgnoreCase))
			{
				return IsNonSecureWebSocketScheme(scheme);
			}
			return true;
		}

		internal static bool IsSupportedSecureScheme(string scheme)
		{
			if (!string.Equals(scheme, "https", StringComparison.OrdinalIgnoreCase))
			{
				return IsSecureWebSocketScheme(scheme);
			}
			return true;
		}

		internal static bool IsNonSecureWebSocketScheme(string scheme)
		{
			return string.Equals(scheme, "ws", StringComparison.OrdinalIgnoreCase);
		}

		internal static bool IsSecureWebSocketScheme(string scheme)
		{
			return string.Equals(scheme, "wss", StringComparison.OrdinalIgnoreCase);
		}

		internal static Task ContinueWithStandard<T>(this Task<T> task, object state, Action<Task<T>, object> continuation)
		{
			return task.ContinueWith(continuation, state, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}
	}
	internal static class CancellationHelper
	{
		private static readonly string s_cancellationMessage = new OperationCanceledException().Message;

		internal static bool ShouldWrapInOperationCanceledException(Exception exception, CancellationToken cancellationToken)
		{
			if (!(exception is OperationCanceledException))
			{
				return cancellationToken.IsCancellationRequested;
			}
			return false;
		}

		internal static Exception CreateOperationCanceledException(Exception innerException, CancellationToken cancellationToken)
		{
			return new TaskCanceledException(s_cancellationMessage, innerException, cancellationToken);
		}

		private static void ThrowOperationCanceledException(Exception innerException, CancellationToken cancellationToken)
		{
			throw CreateOperationCanceledException(innerException, cancellationToken);
		}

		internal static void ThrowIfCancellationRequested(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				ThrowOperationCanceledException(null, cancellationToken);
			}
		}
	}
	/// <summary>The default message handler used by <see cref="T:System.Net.Http.HttpClient" />.</summary>
	public class HttpClientHandler : HttpMessageHandler
	{
		private readonly IMonoHttpClientHandler _delegatingHandler;

		private ClientCertificateOption _clientCertificateOptions;

		/// <summary>Gets a cached delegate that always returns <see langword="true" />.</summary>
		/// <returns>A cached delegate that always returns <see langword="true" />.</returns>
		public static Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> DangerousAcceptAnyServerCertificateValidator
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether the handler supports automatic response content decompression.</summary>
		/// <returns>
		///   <see langword="true" /> if the if the handler supports automatic response content decompression; otherwise <see langword="false" />. The default value is <see langword="true" />.</returns>
		public virtual bool SupportsAutomaticDecompression => _delegatingHandler.SupportsAutomaticDecompression;

		/// <summary>Gets a value that indicates whether the handler supports proxy settings.</summary>
		/// <returns>
		///   <see langword="true" /> if the if the handler supports proxy settings; otherwise <see langword="false" />. The default value is <see langword="true" />.</returns>
		public virtual bool SupportsProxy => true;

		/// <summary>Gets a value that indicates whether the handler supports configuration settings for the <see cref="P:System.Net.Http.HttpClientHandler.AllowAutoRedirect" /> and <see cref="P:System.Net.Http.HttpClientHandler.MaxAutomaticRedirections" /> properties.</summary>
		/// <returns>
		///   <see langword="true" /> if the if the handler supports configuration settings for the <see cref="P:System.Net.Http.HttpClientHandler.AllowAutoRedirect" /> and <see cref="P:System.Net.Http.HttpClientHandler.MaxAutomaticRedirections" /> properties; otherwise <see langword="false" />. The default value is <see langword="true" />.</returns>
		public virtual bool SupportsRedirectConfiguration => true;

		/// <summary>Gets or sets a value that indicates whether the handler uses the  <see cref="P:System.Net.Http.HttpClientHandler.CookieContainer" /> property  to store server cookies and uses these cookies when sending requests.</summary>
		/// <returns>
		///   <see langword="true" /> if the if the handler supports uses the  <see cref="P:System.Net.Http.HttpClientHandler.CookieContainer" /> property  to store server cookies and uses these cookies when sending requests; otherwise <see langword="false" />. The default value is <see langword="true" />.</returns>
		public bool UseCookies
		{
			get
			{
				return _delegatingHandler.UseCookies;
			}
			set
			{
				_delegatingHandler.UseCookies = value;
			}
		}

		/// <summary>Gets or sets the cookie container used to store server cookies by the handler.</summary>
		/// <returns>The cookie container used to store server cookies by the handler.</returns>
		public CookieContainer CookieContainer
		{
			get
			{
				return _delegatingHandler.CookieContainer;
			}
			set
			{
				_delegatingHandler.CookieContainer = value;
			}
		}

		/// <summary>Gets or sets a value that indicates if the certificate is automatically picked from the certificate store or if the caller is allowed to pass in a specific client certificate.</summary>
		/// <returns>The collection of security certificates associated with this handler.</returns>
		public ClientCertificateOption ClientCertificateOptions
		{
			get
			{
				return _clientCertificateOptions;
			}
			set
			{
				switch (value)
				{
				case ClientCertificateOption.Manual:
					ThrowForModifiedManagedSslOptionsIfStarted();
					_clientCertificateOptions = value;
					_delegatingHandler.SslOptions.LocalCertificateSelectionCallback = (object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers) => CertificateHelper.GetEligibleClientCertificate(ClientCertificates);
					break;
				case ClientCertificateOption.Automatic:
					ThrowForModifiedManagedSslOptionsIfStarted();
					_clientCertificateOptions = value;
					_delegatingHandler.SslOptions.LocalCertificateSelectionCallback = (object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers) => CertificateHelper.GetEligibleClientCertificate();
					break;
				default:
					throw new ArgumentOutOfRangeException("value");
				}
			}
		}

		/// <summary>Gets the collection of security certificates that are associated requests to the server.</summary>
		/// <returns>The X509CertificateCollection that is presented to the server when performing certificate based client authentication.</returns>
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (ClientCertificateOptions != ClientCertificateOption.Manual)
				{
					throw new InvalidOperationException(global::SR.Format("The {0} property must be set to '{1}' to use this property.", "ClientCertificateOptions", "Manual"));
				}
				return _delegatingHandler.SslOptions.ClientCertificates ?? (_delegatingHandler.SslOptions.ClientCertificates = new X509CertificateCollection());
			}
		}

		/// <summary>Gets or sets a callback method to validate the server certificate.</summary>
		/// <returns>A callback method to validate the server certificate.</returns>
		public Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> ServerCertificateCustomValidationCallback
		{
			get
			{
				return (_delegatingHandler.SslOptions.RemoteCertificateValidationCallback?.Target as ConnectHelper.CertificateCallbackMapper)?.FromHttpClientHandler;
			}
			set
			{
				ThrowForModifiedManagedSslOptionsIfStarted();
				_delegatingHandler.SslOptions.RemoteCertificateValidationCallback = ((value != null) ? new ConnectHelper.CertificateCallbackMapper(value).ForSocketsHttpHandler : null);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the certificate is checked against the certificate authority revocation list.</summary>
		/// <returns>
		///   <see langword="true" /> if the certificate revocation list is checked; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Framework 4.7.1 only: This property is not implemented.</exception>
		public bool CheckCertificateRevocationList
		{
			get
			{
				return _delegatingHandler.SslOptions.CertificateRevocationCheckMode == X509RevocationMode.Online;
			}
			set
			{
				ThrowForModifiedManagedSslOptionsIfStarted();
				_delegatingHandler.SslOptions.CertificateRevocationCheckMode = (value ? X509RevocationMode.Online : X509RevocationMode.NoCheck);
			}
		}

		/// <summary>Gets or sets the TLS/SSL protocol used by the <see cref="T:System.Net.Http.HttpClient" /> objects managed by the HttpClientHandler object.</summary>
		/// <returns>One of the values defined in the <see cref="T:System.Security.Authentication.SslProtocols" /> enumeration.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Framework 4.7.1 only: This property is not implemented.</exception>
		public SslProtocols SslProtocols
		{
			get
			{
				return _delegatingHandler.SslOptions.EnabledSslProtocols;
			}
			set
			{
				ThrowForModifiedManagedSslOptionsIfStarted();
				_delegatingHandler.SslOptions.EnabledSslProtocols = value;
			}
		}

		/// <summary>Gets or sets the type of decompression method used by the handler for automatic decompression of the HTTP content response.</summary>
		/// <returns>The automatic decompression method used by the handler.</returns>
		public DecompressionMethods AutomaticDecompression
		{
			get
			{
				return _delegatingHandler.AutomaticDecompression;
			}
			set
			{
				_delegatingHandler.AutomaticDecompression = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the handler uses a proxy for requests.</summary>
		/// <returns>
		///   <see langword="true" /> if the handler should use a proxy for requests; otherwise <see langword="false" />. The default value is <see langword="true" />.</returns>
		public bool UseProxy
		{
			get
			{
				return _delegatingHandler.UseProxy;
			}
			set
			{
				_delegatingHandler.UseProxy = value;
			}
		}

		/// <summary>Gets or sets proxy information used by the handler.</summary>
		/// <returns>The proxy information used by the handler. The default value is <see langword="null" />.</returns>
		public IWebProxy Proxy
		{
			get
			{
				return _delegatingHandler.Proxy;
			}
			set
			{
				_delegatingHandler.Proxy = value;
			}
		}

		/// <summary>When the default (system) proxy is being used, gets or sets the credentials to submit to the default proxy server for authentication. The default proxy is used only when <see cref="P:System.Net.Http.HttpClientHandler.UseProxy" /> is set to <see langword="true" /> and <see cref="P:System.Net.Http.HttpClientHandler.Proxy" /> is set to <see langword="null" />.</summary>
		/// <returns>The credentials needed to authenticate a request to the default proxy server.</returns>
		public ICredentials DefaultProxyCredentials
		{
			get
			{
				return _delegatingHandler.DefaultProxyCredentials;
			}
			set
			{
				_delegatingHandler.DefaultProxyCredentials = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the handler sends an Authorization header with the request.</summary>
		/// <returns>
		///   <see langword="true" /> for the handler to send an HTTP Authorization header with requests after authentication has taken place; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public bool PreAuthenticate
		{
			get
			{
				return _delegatingHandler.PreAuthenticate;
			}
			set
			{
				_delegatingHandler.PreAuthenticate = value;
			}
		}

		/// <summary>Gets or sets a value that controls whether default credentials are sent with requests by the handler.</summary>
		/// <returns>
		///   <see langword="true" /> if the default credentials are used; otherwise <see langword="false" />. The default value is <see langword="false" />.</returns>
		public bool UseDefaultCredentials
		{
			get
			{
				return _delegatingHandler.Credentials == CredentialCache.DefaultCredentials;
			}
			set
			{
				if (value)
				{
					_delegatingHandler.Credentials = CredentialCache.DefaultCredentials;
				}
				else if (_delegatingHandler.Credentials == CredentialCache.DefaultCredentials)
				{
					_delegatingHandler.Credentials = null;
				}
			}
		}

		/// <summary>Gets or sets authentication information used by this handler.</summary>
		/// <returns>The authentication credentials associated with the handler. The default is <see langword="null" />.</returns>
		public ICredentials Credentials
		{
			get
			{
				return _delegatingHandler.Credentials;
			}
			set
			{
				_delegatingHandler.Credentials = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the handler should follow redirection responses.</summary>
		/// <returns>
		///   <see langword="true" /> if the handler should follow redirection responses; otherwise <see langword="false" />. The default value is <see langword="true" />.</returns>
		public bool AllowAutoRedirect
		{
			get
			{
				return _delegatingHandler.AllowAutoRedirect;
			}
			set
			{
				_delegatingHandler.AllowAutoRedirect = value;
			}
		}

		/// <summary>Gets or sets the maximum number of redirects that the handler follows.</summary>
		/// <returns>The maximum number of redirection responses that the handler follows. The default value is 50.</returns>
		public int MaxAutomaticRedirections
		{
			get
			{
				return _delegatingHandler.MaxAutomaticRedirections;
			}
			set
			{
				_delegatingHandler.MaxAutomaticRedirections = value;
			}
		}

		/// <summary>Gets or sets the maximum number of concurrent connections (per server endpoint) allowed when making requests using an <see cref="T:System.Net.Http.HttpClient" /> object. Note that the limit is per server endpoint, so for example a value of 256 would permit 256 concurrent connections to http://www.adatum.com/ and another 256 to http://www.adventure-works.com/.</summary>
		/// <returns>The maximum number of concurrent connections (per server endpoint) allowed by an <see cref="T:System.Net.Http.HttpClient" /> object.</returns>
		public int MaxConnectionsPerServer
		{
			get
			{
				return _delegatingHandler.MaxConnectionsPerServer;
			}
			set
			{
				_delegatingHandler.MaxConnectionsPerServer = value;
			}
		}

		/// <summary>Gets or sets the maximum length, in kilobytes (1024 bytes), of the response headers. For example, if the value is 64, then 65536 bytes are allowed for the maximum response headers' length.</summary>
		/// <returns>The maximum length, in kilobytes (1024 bytes), of the response headers.</returns>
		public int MaxResponseHeadersLength
		{
			get
			{
				return _delegatingHandler.MaxResponseHeadersLength;
			}
			set
			{
				_delegatingHandler.MaxResponseHeadersLength = value;
			}
		}

		/// <summary>Gets or sets the maximum request content buffer size used by the handler.</summary>
		/// <returns>The maximum request content buffer size in bytes. The default value is 2 gigabytes.</returns>
		public long MaxRequestContentBufferSize
		{
			get
			{
				return _delegatingHandler.MaxRequestContentBufferSize;
			}
			set
			{
				_delegatingHandler.MaxRequestContentBufferSize = value;
			}
		}

		/// <summary>Gets a writable dictionary (that is, a map) of custom properties for the <see cref="T:System.Net.Http.HttpClient" /> requests. The dictionary is initialized empty; you can insert and query key-value pairs for your custom handlers and special processing.</summary>
		/// <returns>a writable dictionary of custom properties.</returns>
		public IDictionary<string, object> Properties => _delegatingHandler.Properties;

		private static IMonoHttpClientHandler CreateDefaultHandler()
		{
			return new MonoWebRequestHandler();
		}

		/// <summary>Creates an instance of a <see cref="T:System.Net.Http.HttpClientHandler" /> class.</summary>
		public HttpClientHandler()
			: this(CreateDefaultHandler())
		{
		}

		internal HttpClientHandler(IMonoHttpClientHandler handler)
		{
			_delegatingHandler = handler;
			ClientCertificateOptions = ClientCertificateOption.Manual;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpClientHandler" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_delegatingHandler.Dispose();
			}
			base.Dispose(disposing);
		}

		private void ThrowForModifiedManagedSslOptionsIfStarted()
		{
			_delegatingHandler.SslOptions = _delegatingHandler.SslOptions;
		}

		internal void SetWebRequestTimeout(TimeSpan timeout)
		{
			_delegatingHandler.SetWebRequestTimeout(timeout);
		}

		/// <summary>Creates an instance of  <see cref="T:System.Net.Http.HttpResponseMessage" /> based on the information provided in the <see cref="T:System.Net.Http.HttpRequestMessage" /> as an operation that will not block.</summary>
		/// <param name="request">The HTTP request message.</param>
		/// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was <see langword="null" />.</exception>
		protected internal override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return _delegatingHandler.SendAsync(request, cancellationToken);
		}
	}
	internal interface IMonoHttpClientHandler : IDisposable
	{
		bool SupportsAutomaticDecompression { get; }

		bool UseCookies { get; set; }

		CookieContainer CookieContainer { get; set; }

		SslClientAuthenticationOptions SslOptions { get; set; }

		DecompressionMethods AutomaticDecompression { get; set; }

		bool UseProxy { get; set; }

		IWebProxy Proxy { get; set; }

		ICredentials DefaultProxyCredentials { get; set; }

		bool PreAuthenticate { get; set; }

		ICredentials Credentials { get; set; }

		bool AllowAutoRedirect { get; set; }

		int MaxAutomaticRedirections { get; set; }

		int MaxConnectionsPerServer { get; set; }

		int MaxResponseHeadersLength { get; set; }

		long MaxRequestContentBufferSize { get; set; }

		IDictionary<string, object> Properties { get; }

		Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);

		void SetWebRequestTimeout(TimeSpan timeout);
	}
	internal class MonoWebRequestHandler : IMonoHttpClientHandler, IDisposable
	{
		private static long groupCounter;

		private bool allowAutoRedirect;

		private DecompressionMethods automaticDecompression;

		private CookieContainer cookieContainer;

		private ICredentials credentials;

		private int maxAutomaticRedirections;

		private long maxRequestContentBufferSize;

		private bool preAuthenticate;

		private IWebProxy proxy;

		private bool useCookies;

		private bool useProxy;

		private SslClientAuthenticationOptions sslOptions;

		private bool allowPipelining;

		private RequestCachePolicy cachePolicy;

		private AuthenticationLevel authenticationLevel;

		private TimeSpan continueTimeout;

		private TokenImpersonationLevel impersonationLevel;

		private int maxResponseHeadersLength;

		private int readWriteTimeout;

		private RemoteCertificateValidationCallback serverCertificateValidationCallback;

		private bool unsafeAuthenticatedConnectionSharing;

		private bool sentRequest;

		private string connectionGroupName;

		private TimeSpan? timeout;

		private bool disposed;

		public bool AllowAutoRedirect
		{
			get
			{
				return allowAutoRedirect;
			}
			set
			{
				EnsureModifiability();
				allowAutoRedirect = value;
			}
		}

		public DecompressionMethods AutomaticDecompression
		{
			get
			{
				return automaticDecompression;
			}
			set
			{
				EnsureModifiability();
				automaticDecompression = value;
			}
		}

		public CookieContainer CookieContainer
		{
			get
			{
				return cookieContainer ?? (cookieContainer = new CookieContainer());
			}
			set
			{
				EnsureModifiability();
				cookieContainer = value;
			}
		}

		public ICredentials Credentials
		{
			get
			{
				return credentials;
			}
			set
			{
				EnsureModifiability();
				credentials = value;
			}
		}

		public int MaxAutomaticRedirections
		{
			get
			{
				return maxAutomaticRedirections;
			}
			set
			{
				EnsureModifiability();
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				maxAutomaticRedirections = value;
			}
		}

		public long MaxRequestContentBufferSize
		{
			get
			{
				return maxRequestContentBufferSize;
			}
			set
			{
				EnsureModifiability();
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				maxRequestContentBufferSize = value;
			}
		}

		public bool PreAuthenticate
		{
			get
			{
				return preAuthenticate;
			}
			set
			{
				EnsureModifiability();
				preAuthenticate = value;
			}
		}

		public IWebProxy Proxy
		{
			get
			{
				return proxy;
			}
			set
			{
				EnsureModifiability();
				if (!UseProxy)
				{
					throw new InvalidOperationException();
				}
				proxy = value;
			}
		}

		public virtual bool SupportsAutomaticDecompression => true;

		public virtual bool SupportsProxy => true;

		public virtual bool SupportsRedirectConfiguration => true;

		public bool UseCookies
		{
			get
			{
				return useCookies;
			}
			set
			{
				EnsureModifiability();
				useCookies = value;
			}
		}

		public bool UseProxy
		{
			get
			{
				return useProxy;
			}
			set
			{
				EnsureModifiability();
				useProxy = value;
			}
		}

		public bool AllowPipelining
		{
			get
			{
				return allowPipelining;
			}
			set
			{
				EnsureModifiability();
				allowPipelining = value;
			}
		}

		public RequestCachePolicy CachePolicy
		{
			get
			{
				return cachePolicy;
			}
			set
			{
				EnsureModifiability();
				cachePolicy = value;
			}
		}

		public AuthenticationLevel AuthenticationLevel
		{
			get
			{
				return authenticationLevel;
			}
			set
			{
				EnsureModifiability();
				authenticationLevel = value;
			}
		}

		[System.MonoTODO]
		public TimeSpan ContinueTimeout
		{
			get
			{
				return continueTimeout;
			}
			set
			{
				EnsureModifiability();
				continueTimeout = value;
			}
		}

		public TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				return impersonationLevel;
			}
			set
			{
				EnsureModifiability();
				impersonationLevel = value;
			}
		}

		public int MaxResponseHeadersLength
		{
			get
			{
				return maxResponseHeadersLength;
			}
			set
			{
				EnsureModifiability();
				maxResponseHeadersLength = value;
			}
		}

		public int ReadWriteTimeout
		{
			get
			{
				return readWriteTimeout;
			}
			set
			{
				EnsureModifiability();
				readWriteTimeout = value;
			}
		}

		public RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				return serverCertificateValidationCallback;
			}
			set
			{
				EnsureModifiability();
				serverCertificateValidationCallback = value;
			}
		}

		public bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return unsafeAuthenticatedConnectionSharing;
			}
			set
			{
				EnsureModifiability();
				unsafeAuthenticatedConnectionSharing = value;
			}
		}

		public SslClientAuthenticationOptions SslOptions
		{
			get
			{
				return sslOptions ?? (sslOptions = new SslClientAuthenticationOptions());
			}
			set
			{
				EnsureModifiability();
				sslOptions = value;
			}
		}

		public ICredentials DefaultProxyCredentials
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public int MaxConnectionsPerServer
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public IDictionary<string, object> Properties
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public MonoWebRequestHandler()
		{
			allowAutoRedirect = true;
			maxAutomaticRedirections = 50;
			maxRequestContentBufferSize = 2147483647L;
			useCookies = true;
			useProxy = true;
			allowPipelining = true;
			authenticationLevel = AuthenticationLevel.MutualAuthRequested;
			cachePolicy = WebRequest.DefaultCachePolicy;
			continueTimeout = TimeSpan.FromMilliseconds(350.0);
			impersonationLevel = TokenImpersonationLevel.Delegation;
			maxResponseHeadersLength = HttpWebRequest.DefaultMaximumResponseHeadersLength;
			readWriteTimeout = 300000;
			serverCertificateValidationCallback = null;
			unsafeAuthenticatedConnectionSharing = false;
			connectionGroupName = "HttpClientHandler" + Interlocked.Increment(ref groupCounter);
		}

		internal void EnsureModifiability()
		{
			if (sentRequest)
			{
				throw new InvalidOperationException("This instance has already started one or more requests. Properties can only be modified before sending the first request.");
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !disposed)
			{
				Volatile.Write(ref disposed, value: true);
				ServicePointManager.CloseConnectionGroup(connectionGroupName);
			}
		}

		private bool GetConnectionKeepAlive(HttpRequestHeaders headers)
		{
			return headers.Connection.Any((string l) => string.Equals(l, "Keep-Alive", StringComparison.OrdinalIgnoreCase));
		}

		internal virtual HttpWebRequest CreateWebRequest(HttpRequestMessage request)
		{
			HttpWebRequest httpWebRequest;
			if (HttpUtilities.IsSupportedSecureScheme(request.RequestUri.Scheme))
			{
				httpWebRequest = new HttpWebRequest(request.RequestUri, Mono.Net.Security.MonoTlsProviderFactory.GetProviderInternal(), MonoTlsSettings.CopyDefaultSettings());
				httpWebRequest.TlsSettings.ClientCertificateSelectionCallback = (string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => SslOptions.LocalCertificateSelectionCallback(this, t, lc, rc, ai);
			}
			else
			{
				httpWebRequest = new HttpWebRequest(request.RequestUri);
			}
			httpWebRequest.ThrowOnError = false;
			httpWebRequest.AllowWriteStreamBuffering = false;
			if (request.Version == HttpVersion.Version20)
			{
				httpWebRequest.ProtocolVersion = HttpVersion.Version11;
			}
			else
			{
				httpWebRequest.ProtocolVersion = request.Version;
			}
			httpWebRequest.ConnectionGroupName = connectionGroupName;
			httpWebRequest.Method = request.Method.Method;
			if (httpWebRequest.ProtocolVersion == HttpVersion.Version10)
			{
				httpWebRequest.KeepAlive = GetConnectionKeepAlive(request.Headers);
			}
			else
			{
				httpWebRequest.KeepAlive = request.Headers.ConnectionClose != true;
			}
			if (allowAutoRedirect)
			{
				httpWebRequest.AllowAutoRedirect = true;
				httpWebRequest.MaximumAutomaticRedirections = maxAutomaticRedirections;
			}
			else
			{
				httpWebRequest.AllowAutoRedirect = false;
			}
			httpWebRequest.AutomaticDecompression = automaticDecompression;
			httpWebRequest.PreAuthenticate = preAuthenticate;
			if (useCookies)
			{
				httpWebRequest.CookieContainer = CookieContainer;
			}
			httpWebRequest.Credentials = credentials;
			if (useProxy)
			{
				httpWebRequest.Proxy = proxy;
			}
			else
			{
				httpWebRequest.Proxy = null;
			}
			httpWebRequest.ServicePoint.Expect100Continue = request.Headers.ExpectContinue == true;
			if (timeout.HasValue)
			{
				httpWebRequest.Timeout = (int)timeout.Value.TotalMilliseconds;
			}
			httpWebRequest.ServerCertificateValidationCallback = SslOptions.RemoteCertificateValidationCallback;
			WebHeaderCollection headers = httpWebRequest.Headers;
			foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
			{
				IEnumerable<string> enumerable = header.Value;
				if (header.Key == "Host")
				{
					httpWebRequest.Host = request.Headers.Host;
					continue;
				}
				if (header.Key == "Transfer-Encoding")
				{
					enumerable = enumerable.Where((string l) => l != "chunked");
				}
				string singleHeaderString = PlatformHelper.GetSingleHeaderString(header.Key, enumerable);
				if (singleHeaderString != null)
				{
					headers.AddInternal(header.Key, singleHeaderString);
				}
			}
			return httpWebRequest;
		}

		private HttpResponseMessage CreateResponseMessage(HttpWebResponse wr, HttpRequestMessage requestMessage, CancellationToken cancellationToken)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(wr.StatusCode);
			httpResponseMessage.RequestMessage = requestMessage;
			httpResponseMessage.ReasonPhrase = wr.StatusDescription;
			httpResponseMessage.Content = PlatformHelper.CreateStreamContent(wr.GetResponseStream(), cancellationToken);
			WebHeaderCollection headers = wr.Headers;
			for (int i = 0; i < headers.Count; i++)
			{
				string key = headers.GetKey(i);
				string[] values = headers.GetValues(i);
				HttpHeaders httpHeaders = ((!PlatformHelper.IsContentHeader(key)) ? ((HttpHeaders)httpResponseMessage.Headers) : ((HttpHeaders)httpResponseMessage.Content.Headers));
				httpHeaders.TryAddWithoutValidation(key, values);
			}
			requestMessage.RequestUri = wr.ResponseUri;
			return httpResponseMessage;
		}

		private static bool MethodHasBody(HttpMethod method)
		{
			switch (method.Method)
			{
			case "HEAD":
			case "GET":
			case "MKCOL":
			case "CONNECT":
			case "TRACE":
				return false;
			default:
				return true;
			}
		}

		public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (disposed)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
			FieldInfo field = typeof(CancellationToken).GetField("_source", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
			CancellationTokenSource obj = (CancellationTokenSource)field.GetValue(cancellationToken);
			field = typeof(CancellationTokenSource).GetField("_timer", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
			Timer timer = (Timer)field.GetValue(obj);
			if (timer != null)
			{
				field = typeof(Timer).GetField("due_time_ms", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
				timeout = TimeSpan.FromMilliseconds((long)field.GetValue(timer));
			}
			Volatile.Write(ref sentRequest, value: true);
			HttpWebRequest wrequest = CreateWebRequest(request);
			HttpWebResponse wresponse = null;
			try
			{
				using (cancellationToken.Register(delegate(object l)
				{
					((HttpWebRequest)l).Abort();
				}, wrequest))
				{
					HttpContent content = request.Content;
					if (content != null)
					{
						WebHeaderCollection headers = wrequest.Headers;
						foreach (KeyValuePair<string, IEnumerable<string>> header in content.Headers)
						{
							foreach (string item in header.Value)
							{
								headers.AddInternal(header.Key, item);
							}
						}
						if (request.Headers.TransferEncodingChunked == true)
						{
							wrequest.SendChunked = true;
						}
						else
						{
							long? contentLength = content.Headers.ContentLength;
							if (contentLength.HasValue)
							{
								wrequest.ContentLength = contentLength.Value;
							}
							else
							{
								if (MaxRequestContentBufferSize == 0L)
								{
									throw new InvalidOperationException("The content length of the request content can't be determined. Either set TransferEncodingChunked to true, load content into buffer, or set MaxRequestContentBufferSize.");
								}
								await content.LoadIntoBufferAsync(MaxRequestContentBufferSize).ConfigureAwait(continueOnCapturedContext: false);
								wrequest.ContentLength = content.Headers.ContentLength.Value;
							}
						}
						wrequest.ResendContentFactory = content.CopyToAsync;
						using Stream stream = await wrequest.GetRequestStreamAsync().ConfigureAwait(continueOnCapturedContext: false);
						await request.Content.CopyToAsync(stream).ConfigureAwait(continueOnCapturedContext: false);
					}
					else if (MethodHasBody(request.Method))
					{
						wrequest.ContentLength = 0L;
					}
					wresponse = (HttpWebResponse)(await wrequest.GetResponseAsync().ConfigureAwait(continueOnCapturedContext: false));
				}
			}
			catch (WebException ex)
			{
				if (ex.Status != WebExceptionStatus.RequestCanceled)
				{
					throw new HttpRequestException("An error occurred while sending the request", ex);
				}
			}
			catch (IOException inner)
			{
				throw new HttpRequestException("An error occurred while sending the request", inner);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				TaskCompletionSource<HttpResponseMessage> taskCompletionSource = new TaskCompletionSource<HttpResponseMessage>();
				taskCompletionSource.SetCanceled();
				return await taskCompletionSource.Task;
			}
			return CreateResponseMessage(wresponse, request, cancellationToken);
		}

		void IMonoHttpClientHandler.SetWebRequestTimeout(TimeSpan timeout)
		{
			this.timeout = timeout;
		}
	}
	internal static class PlatformHelper
	{
		internal static bool IsContentHeader(string name)
		{
			return HttpHeaders.GetKnownHeaderKind(name) == HttpHeaderKind.Content;
		}

		internal static string GetSingleHeaderString(string name, IEnumerable<string> values)
		{
			return HttpHeaders.GetSingleHeaderString(name, values);
		}

		internal static StreamContent CreateStreamContent(Stream stream, CancellationToken cancellationToken)
		{
			return new StreamContent(stream, cancellationToken);
		}
	}
	/// <summary>Provides HTTP content based on a byte array.</summary>
	public class ByteArrayContent : HttpContent
	{
		private readonly byte[] content;

		private readonly int offset;

		private readonly int count;

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.ByteArrayContent" /> class.</summary>
		/// <param name="content">The content used to initialize the <see cref="T:System.Net.Http.ByteArrayContent" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> parameter is <see langword="null" />.</exception>
		public ByteArrayContent(byte[] content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			this.content = content;
			count = content.Length;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.ByteArrayContent" /> class.</summary>
		/// <param name="content">The content used to initialize the <see cref="T:System.Net.Http.ByteArrayContent" />.</param>
		/// <param name="offset">The offset, in bytes, in the <paramref name="content" /> parameter used to initialize the <see cref="T:System.Net.Http.ByteArrayContent" />.</param>
		/// <param name="count">The number of bytes in the <paramref name="content" /> starting from the <paramref name="offset" /> parameter used to initialize the <see cref="T:System.Net.Http.ByteArrayContent" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than zero.  
		///  -or-  
		///  The <paramref name="offset" /> parameter is greater than the length of content specified by the <paramref name="content" /> parameter.  
		///  -or-  
		///  The <paramref name="count" /> parameter is less than zero.  
		///  -or-  
		///  The <paramref name="count" /> parameter is greater than the length of content specified by the <paramref name="content" /> parameter - minus the <paramref name="offset" /> parameter.</exception>
		public ByteArrayContent(byte[] content, int offset, int count)
			: this(content)
		{
			if (offset < 0 || offset > this.count)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > this.count - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.offset = offset;
			this.count = count;
		}

		/// <summary>Creates an HTTP content stream as an asynchronous operation for reading whose backing store is memory from the <see cref="T:System.Net.Http.ByteArrayContent" />.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		protected override Task<Stream> CreateContentReadStreamAsync()
		{
			return Task.FromResult((Stream)new MemoryStream(content, offset, count));
		}

		/// <summary>Serialize and write the byte array provided in the constructor to an HTTP content stream as an asynchronous operation.</summary>
		/// <param name="stream">The target stream.</param>
		/// <param name="context">Information about the transport, like channel binding token. This parameter may be <see langword="null" />.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			return stream.WriteAsync(content, offset, count);
		}

		/// <summary>Determines whether a byte array has a valid length in bytes.</summary>
		/// <param name="length">The length in bytes of the byte array.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="length" /> is a valid length; otherwise, <see langword="false" />.</returns>
		protected internal override bool TryComputeLength(out long length)
		{
			length = count;
			return true;
		}
	}
	/// <summary>Specifies how client certificates are provided.</summary>
	public enum ClientCertificateOption
	{
		/// <summary>The application manually provides the client certificates to the <see cref="T:System.Net.Http.WebRequestHandler" />. This value is the default.</summary>
		Manual,
		/// <summary>The <see cref="T:System.Net.Http.HttpClientHandler" /> will attempt to provide  all available client certificates  automatically.</summary>
		Automatic
	}
	/// <summary>A type for HTTP handlers that delegate the processing of HTTP response messages to another handler, called the inner handler.</summary>
	public abstract class DelegatingHandler : HttpMessageHandler
	{
		private bool disposed;

		private HttpMessageHandler handler;

		/// <summary>Gets or sets the inner handler which processes the HTTP response messages.</summary>
		/// <returns>The inner handler for HTTP response messages.</returns>
		public HttpMessageHandler InnerHandler
		{
			get
			{
				return handler;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("InnerHandler");
				}
				handler = value;
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.DelegatingHandler" /> class.</summary>
		protected DelegatingHandler()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.DelegatingHandler" /> class with a specific inner handler.</summary>
		/// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
		protected DelegatingHandler(HttpMessageHandler innerHandler)
		{
			if (innerHandler == null)
			{
				throw new ArgumentNullException("innerHandler");
			}
			InnerHandler = innerHandler;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.DelegatingHandler" />, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && !disposed)
			{
				disposed = true;
				if (InnerHandler != null)
				{
					InnerHandler.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.</summary>
		/// <param name="request">The HTTP request message to send to the server.</param>
		/// <param name="cancellationToken">A cancellation token to cancel operation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was <see langword="null" />.</exception>
		protected internal override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (InnerHandler == null)
			{
				throw new InvalidOperationException("The inner handler has not been assigned.");
			}
			return InnerHandler.SendAsync(request, cancellationToken);
		}
	}
	/// <summary>A container for name/value tuples encoded using application/x-www-form-urlencoded MIME type.</summary>
	public class FormUrlEncodedContent : ByteArrayContent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.FormUrlEncodedContent" /> class with a specific collection of name/value pairs.</summary>
		/// <param name="nameValueCollection">A collection of name/value pairs.</param>
		public FormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
			: base(EncodeContent(nameValueCollection))
		{
			base.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
		}

		private static byte[] EncodeContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
		{
			if (nameValueCollection == null)
			{
				throw new ArgumentNullException("nameValueCollection");
			}
			List<byte> list = new List<byte>();
			foreach (KeyValuePair<string, string> item in nameValueCollection)
			{
				if (list.Count != 0)
				{
					list.Add(38);
				}
				byte[] array = SerializeValue(item.Key);
				if (array != null)
				{
					list.AddRange(array);
				}
				list.Add(61);
				array = SerializeValue(item.Value);
				if (array != null)
				{
					list.AddRange(array);
				}
			}
			return list.ToArray();
		}

		private static byte[] SerializeValue(string value)
		{
			if (value == null)
			{
				return null;
			}
			value = Uri.EscapeDataString(value).Replace("%20", "+");
			return Encoding.ASCII.GetBytes(value);
		}
	}
	/// <summary>Provides a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.</summary>
	public class HttpClient : HttpMessageInvoker
	{
		private static readonly TimeSpan TimeoutDefault = TimeSpan.FromSeconds(100.0);

		private Uri base_address;

		private CancellationTokenSource cts;

		private bool disposed;

		private HttpRequestHeaders headers;

		private long buffer_size;

		private TimeSpan timeout;

		/// <summary>Gets or sets the base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.</summary>
		/// <returns>The base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.</returns>
		public Uri BaseAddress
		{
			get
			{
				return base_address;
			}
			set
			{
				base_address = value;
			}
		}

		/// <summary>Gets the headers which should be sent with each request.</summary>
		/// <returns>The headers which should be sent with each request.</returns>
		public HttpRequestHeaders DefaultRequestHeaders => headers ?? (headers = new HttpRequestHeaders());

		/// <summary>Gets or sets the maximum number of bytes to buffer when reading the response content.</summary>
		/// <returns>The maximum number of bytes to buffer when reading the response content. The default value for this property is 2 gigabytes.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The size specified is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">An operation has already been started on the current instance.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		public long MaxResponseContentBufferSize
		{
			get
			{
				return buffer_size;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				buffer_size = value;
			}
		}

		/// <summary>Gets or sets the timespan to wait before the request times out.</summary>
		/// <returns>The timespan to wait before the request times out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The timeout specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.InfiniteTimeSpan" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An operation has already been started on the current instance.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed.</exception>
		public TimeSpan Timeout
		{
			get
			{
				return timeout;
			}
			set
			{
				if (value != System.Threading.Timeout.InfiniteTimeSpan && (value <= TimeSpan.Zero || value.TotalMilliseconds > 2147483647.0))
				{
					throw new ArgumentOutOfRangeException();
				}
				timeout = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpClient" /> class.</summary>
		public HttpClient()
			: this(new HttpClientHandler(), disposeHandler: true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpClient" /> class with a specific handler.</summary>
		/// <param name="handler">The HTTP handler stack to use for sending requests.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="handler" /> is <see langword="null" />.</exception>
		public HttpClient(HttpMessageHandler handler)
			: this(handler, disposeHandler: true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpClient" /> class with a specific handler.</summary>
		/// <param name="handler">The <see cref="T:System.Net.Http.HttpMessageHandler" /> responsible for processing the HTTP response messages.</param>
		/// <param name="disposeHandler">
		///   <see langword="true" /> if the inner handler should be disposed of by HttpClient.Dispose, <see langword="false" /> if you intend to reuse the inner handler.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="handler" /> is <see langword="null" />.</exception>
		public HttpClient(HttpMessageHandler handler, bool disposeHandler)
			: base(handler, disposeHandler)
		{
			buffer_size = 2147483647L;
			timeout = TimeoutDefault;
			cts = new CancellationTokenSource();
		}

		/// <summary>Cancel all pending requests on this instance.</summary>
		public void CancelPendingRequests()
		{
			using CancellationTokenSource cancellationTokenSource = Interlocked.Exchange(ref cts, new CancellationTokenSource());
			cancellationTokenSource.Cancel();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpClient" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && !disposed)
			{
				disposed = true;
				cts.Cancel();
				cts.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Send a DELETE request to the specified Uri as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> DeleteAsync(string requestUri)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri));
		}

		/// <summary>Send a DELETE request to the specified Uri with a cancellation token as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri), cancellationToken);
		}

		/// <summary>Send a DELETE request to the specified Uri as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri));
		}

		/// <summary>Send a DELETE request to the specified Uri with a cancellation token as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri), cancellationToken);
		}

		/// <summary>Send a GET request to the specified Uri as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> GetAsync(string requestUri)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri));
		}

		/// <summary>Send a GET request to the specified Uri with a cancellation token as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri), cancellationToken);
		}

		/// <summary>Send a GET request to the specified Uri with an HTTP completion option as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="completionOption">An HTTP completion option value that indicates when the operation should be considered completed.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri), completionOption);
		}

		/// <summary>Send a GET request to the specified Uri with an HTTP completion option and a cancellation token as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="completionOption">An HTTP  completion option value that indicates when the operation should be considered completed.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri), completionOption, cancellationToken);
		}

		/// <summary>Send a GET request to the specified Uri as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> GetAsync(Uri requestUri)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri));
		}

		/// <summary>Send a GET request to the specified Uri with a cancellation token as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri), cancellationToken);
		}

		/// <summary>Send a GET request to the specified Uri with an HTTP completion option as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="completionOption">An HTTP completion option value that indicates when the operation should be considered completed.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri), completionOption);
		}

		/// <summary>Send a GET request to the specified Uri with an HTTP completion option and a cancellation token as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="completionOption">An HTTP  completion option value that indicates when the operation should be considered completed.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri), completionOption, cancellationToken);
		}

		/// <summary>Send a POST request to the specified Uri as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Post, requestUri)
			{
				Content = content
			});
		}

		/// <summary>Send a POST request with a cancellation token as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Post, requestUri)
			{
				Content = content
			}, cancellationToken);
		}

		/// <summary>Send a POST request to the specified Uri as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Post, requestUri)
			{
				Content = content
			});
		}

		/// <summary>Send a POST request with a cancellation token as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Post, requestUri)
			{
				Content = content
			}, cancellationToken);
		}

		/// <summary>Send a PUT request to the specified Uri as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Put, requestUri)
			{
				Content = content
			});
		}

		/// <summary>Send a PUT request with a cancellation token as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Put, requestUri)
			{
				Content = content
			}, cancellationToken);
		}

		/// <summary>Send a PUT request to the specified Uri as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Put, requestUri)
			{
				Content = content
			});
		}

		/// <summary>Send a PUT request with a cancellation token as an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <param name="content">The HTTP request content sent to the server.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return SendAsync(new HttpRequestMessage(HttpMethod.Put, requestUri)
			{
				Content = content
			}, cancellationToken);
		}

		/// <summary>Send an HTTP request as an asynchronous operation.</summary>
		/// <param name="request">The HTTP request message to send.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
		{
			return SendAsync(request, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
		}

		/// <summary>Send an HTTP request as an asynchronous operation.</summary>
		/// <param name="request">The HTTP request message to send.</param>
		/// <param name="completionOption">When the operation should complete (as soon as a response is available or after reading the whole response content).</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
		{
			return SendAsync(request, completionOption, CancellationToken.None);
		}

		/// <summary>Send an HTTP request as an asynchronous operation.</summary>
		/// <param name="request">The HTTP request message to send.</param>
		/// <param name="cancellationToken">The cancellation token to cancel operation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
		}

		/// <summary>Send an HTTP request as an asynchronous operation.</summary>
		/// <param name="request">The HTTP request message to send.</param>
		/// <param name="completionOption">When the operation should complete (as soon as a response is available or after reading the whole response content).</param>
		/// <param name="cancellationToken">The cancellation token to cancel operation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The request message was already sent by the <see cref="T:System.Net.Http.HttpClient" /> instance.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (request.SetIsUsed())
			{
				throw new InvalidOperationException("Cannot send the same request message multiple times");
			}
			Uri requestUri = request.RequestUri;
			if (requestUri == null)
			{
				if (base_address == null)
				{
					throw new InvalidOperationException("The request URI must either be an absolute URI or BaseAddress must be set");
				}
				request.RequestUri = base_address;
			}
			else if (!requestUri.IsAbsoluteUri || (requestUri.Scheme == Uri.UriSchemeFile && requestUri.OriginalString.StartsWith("/", StringComparison.Ordinal)))
			{
				if (base_address == null)
				{
					throw new InvalidOperationException("The request URI must either be an absolute URI or BaseAddress must be set");
				}
				request.RequestUri = new Uri(base_address, requestUri);
			}
			if (headers != null)
			{
				request.Headers.AddHeaders(headers);
			}
			return SendAsyncWorker(request, completionOption, cancellationToken);
		}

		private async Task<HttpResponseMessage> SendAsyncWorker(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
			using CancellationTokenSource lcts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, cancellationToken);
			if (handler is HttpClientHandler httpClientHandler)
			{
				httpClientHandler.SetWebRequestTimeout(timeout);
			}
			lcts.CancelAfter(timeout);
			HttpResponseMessage response = await (base.SendAsync(request, lcts.Token) ?? throw new InvalidOperationException("Handler failed to return a value")).ConfigureAwait(continueOnCapturedContext: false);
			if (response == null)
			{
				throw new InvalidOperationException("Handler failed to return a response");
			}
			if (response.Content != null && (completionOption & HttpCompletionOption.ResponseHeadersRead) == 0)
			{
				await response.Content.LoadIntoBufferAsync(MaxResponseContentBufferSize).ConfigureAwait(continueOnCapturedContext: false);
			}
			return response;
		}

		/// <summary>Sends a GET request to the specified Uri and return the response body as a byte array in an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public async Task<byte[]> GetByteArrayAsync(string requestUri)
		{
			using HttpResponseMessage resp = await GetAsync(requestUri, HttpCompletionOption.ResponseContentRead).ConfigureAwait(continueOnCapturedContext: false);
			resp.EnsureSuccessStatusCode();
			return await resp.Content.ReadAsByteArrayAsync().ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Send a GET request to the specified Uri and return the response body as a byte array in an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public async Task<byte[]> GetByteArrayAsync(Uri requestUri)
		{
			using HttpResponseMessage resp = await GetAsync(requestUri, HttpCompletionOption.ResponseContentRead).ConfigureAwait(continueOnCapturedContext: false);
			resp.EnsureSuccessStatusCode();
			return await resp.Content.ReadAsByteArrayAsync().ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Send a GET request to the specified Uri and return the response body as a stream in an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public async Task<Stream> GetStreamAsync(string requestUri)
		{
			HttpResponseMessage obj = await GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(continueOnCapturedContext: false);
			obj.EnsureSuccessStatusCode();
			return await obj.Content.ReadAsStreamAsync().ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Send a GET request to the specified Uri and return the response body as a stream in an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public async Task<Stream> GetStreamAsync(Uri requestUri)
		{
			HttpResponseMessage obj = await GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(continueOnCapturedContext: false);
			obj.EnsureSuccessStatusCode();
			return await obj.Content.ReadAsStreamAsync().ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Send a GET request to the specified Uri and return the response body as a string in an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public async Task<string> GetStringAsync(string requestUri)
		{
			using HttpResponseMessage resp = await GetAsync(requestUri, HttpCompletionOption.ResponseContentRead).ConfigureAwait(continueOnCapturedContext: false);
			resp.EnsureSuccessStatusCode();
			return await resp.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Send a GET request to the specified Uri and return the response body as a string in an asynchronous operation.</summary>
		/// <param name="requestUri">The Uri the request is sent to.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
		public async Task<string> GetStringAsync(Uri requestUri)
		{
			using HttpResponseMessage resp = await GetAsync(requestUri, HttpCompletionOption.ResponseContentRead).ConfigureAwait(continueOnCapturedContext: false);
			resp.EnsureSuccessStatusCode();
			return await resp.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
		}

		public Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content)
		{
			throw new PlatformNotSupportedException();
		}

		public Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			throw new PlatformNotSupportedException();
		}

		public Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content)
		{
			throw new PlatformNotSupportedException();
		}

		public Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			throw new PlatformNotSupportedException();
		}
	}
	/// <summary>Indicates if <see cref="T:System.Net.Http.HttpClient" /> operations should be considered completed either as soon as a response is available, or after reading the entire response message including the content.</summary>
	public enum HttpCompletionOption
	{
		/// <summary>The operation should complete after reading the entire response including the content.</summary>
		ResponseContentRead,
		/// <summary>The operation should complete as soon as a response is available and headers are read. The content is not read yet.</summary>
		ResponseHeadersRead
	}
	/// <summary>A base class representing an HTTP entity body and content headers.</summary>
	public abstract class HttpContent : IDisposable
	{
		private sealed class FixedMemoryStream : MemoryStream
		{
			private readonly long maxSize;

			public FixedMemoryStream(long maxSize)
			{
				this.maxSize = maxSize;
			}

			private void CheckOverflow(int count)
			{
				if (Length + count > maxSize)
				{
					throw new HttpRequestException($"Cannot write more bytes to the buffer than the configured maximum buffer size: {maxSize}");
				}
			}

			public override void WriteByte(byte value)
			{
				CheckOverflow(1);
				base.WriteByte(value);
			}

			public override void Write(byte[] buffer, int offset, int count)
			{
				CheckOverflow(count);
				base.Write(buffer, offset, count);
			}
		}

		private FixedMemoryStream buffer;

		private Stream stream;

		private bool disposed;

		private HttpContentHeaders headers;

		/// <summary>Gets the HTTP content headers as defined in RFC 2616.</summary>
		/// <returns>The content headers as defined in RFC 2616.</returns>
		public HttpContentHeaders Headers => headers ?? (headers = new HttpContentHeaders(this));

		internal long? LoadedBufferLength
		{
			get
			{
				if (buffer != null)
				{
					return buffer.Length;
				}
				return null;
			}
		}

		internal void CopyTo(Stream stream)
		{
			CopyToAsync(stream).Wait();
		}

		/// <summary>Serialize the HTTP content into a stream of bytes and copies it to the stream object provided as the <paramref name="stream" /> parameter.</summary>
		/// <param name="stream">The target stream.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		public Task CopyToAsync(Stream stream)
		{
			return CopyToAsync(stream, null);
		}

		/// <summary>Serialize the HTTP content into a stream of bytes and copies it to the stream object provided as the <paramref name="stream" /> parameter.</summary>
		/// <param name="stream">The target stream.</param>
		/// <param name="context">Information about the transport (channel binding token, for example). This parameter may be <see langword="null" />.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		public Task CopyToAsync(Stream stream, TransportContext context)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (buffer != null)
			{
				return buffer.CopyToAsync(stream);
			}
			return SerializeToStreamAsync(stream, context);
		}

		/// <summary>Serialize the HTTP content to a memory stream as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		protected virtual async Task<Stream> CreateContentReadStreamAsync()
		{
			await LoadIntoBufferAsync().ConfigureAwait(continueOnCapturedContext: false);
			return buffer;
		}

		private static FixedMemoryStream CreateFixedMemoryStream(long maxBufferSize)
		{
			return new FixedMemoryStream(maxBufferSize);
		}

		/// <summary>Releases the unmanaged resources and disposes of the managed resources used by the <see cref="T:System.Net.Http.HttpContent" />.</summary>
		public void Dispose()
		{
			Dispose(disposing: true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpContent" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !disposed)
			{
				disposed = true;
				if (buffer != null)
				{
					buffer.Dispose();
				}
			}
		}

		/// <summary>Serialize the HTTP content to a memory buffer as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		public Task LoadIntoBufferAsync()
		{
			return LoadIntoBufferAsync(2147483647L);
		}

		/// <summary>Serialize the HTTP content to a memory buffer as an asynchronous operation.</summary>
		/// <param name="maxBufferSize">The maximum size, in bytes, of the buffer to use.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		public async Task LoadIntoBufferAsync(long maxBufferSize)
		{
			if (disposed)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
			if (buffer == null)
			{
				buffer = CreateFixedMemoryStream(maxBufferSize);
				await SerializeToStreamAsync(buffer, null).ConfigureAwait(continueOnCapturedContext: false);
				buffer.Seek(0L, SeekOrigin.Begin);
			}
		}

		/// <summary>Serialize the HTTP content and return a stream that represents the content as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		public async Task<Stream> ReadAsStreamAsync()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(GetType().ToString());
			}
			if (buffer != null)
			{
				return new MemoryStream(buffer.GetBuffer(), 0, (int)buffer.Length, writable: false);
			}
			if (stream == null)
			{
				stream = await CreateContentReadStreamAsync().ConfigureAwait(continueOnCapturedContext: false);
			}
			return stream;
		}

		/// <summary>Serialize the HTTP content to a byte array as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		public async Task<byte[]> ReadAsByteArrayAsync()
		{
			await LoadIntoBufferAsync().ConfigureAwait(continueOnCapturedContext: false);
			return buffer.ToArray();
		}

		/// <summary>Serialize the HTTP content to a string as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		public async Task<string> ReadAsStringAsync()
		{
			await LoadIntoBufferAsync().ConfigureAwait(continueOnCapturedContext: false);
			if (buffer.Length == 0L)
			{
				return string.Empty;
			}
			byte[] array = buffer.GetBuffer();
			int num = (int)buffer.Length;
			int preambleLength = 0;
			Encoding encoding;
			if (headers != null && headers.ContentType != null && headers.ContentType.CharSet != null)
			{
				encoding = Encoding.GetEncoding(headers.ContentType.CharSet);
				preambleLength = StartsWith(array, num, encoding.GetPreamble());
			}
			else
			{
				encoding = GetEncodingFromBuffer(array, num, ref preambleLength) ?? Encoding.UTF8;
			}
			return encoding.GetString(array, preambleLength, num - preambleLength);
		}

		private static Encoding GetEncodingFromBuffer(byte[] buffer, int length, ref int preambleLength)
		{
			Encoding[] array = new Encoding[3]
			{
				Encoding.UTF8,
				Encoding.UTF32,
				Encoding.Unicode
			};
			foreach (Encoding encoding in array)
			{
				if ((preambleLength = StartsWith(buffer, length, encoding.GetPreamble())) != 0)
				{
					return encoding;
				}
			}
			return null;
		}

		private static int StartsWith(byte[] array, int length, byte[] value)
		{
			if (length < value.Length)
			{
				return 0;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (array[i] != value[i])
				{
					return 0;
				}
			}
			return value.Length;
		}

		internal Task SerializeToStreamAsync_internal(Stream stream, TransportContext context)
		{
			return SerializeToStreamAsync(stream, context);
		}

		/// <summary>Serialize the HTTP content to a stream as an asynchronous operation.</summary>
		/// <param name="stream">The target stream.</param>
		/// <param name="context">Information about the transport (channel binding token, for example). This parameter may be <see langword="null" />.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		protected abstract Task SerializeToStreamAsync(Stream stream, TransportContext context);

		/// <summary>Determines whether the HTTP content has a valid length in bytes.</summary>
		/// <param name="length">The length in bytes of the HTTP content.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="length" /> is a valid length; otherwise, <see langword="false" />.</returns>
		protected internal abstract bool TryComputeLength(out long length);

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpContent" /> class.</summary>
		protected HttpContent()
		{
		}
	}
	/// <summary>A base type for HTTP message handlers.</summary>
	public abstract class HttpMessageHandler : IDisposable
	{
		/// <summary>Releases the unmanaged resources and disposes of the managed resources used by the <see cref="T:System.Net.Http.HttpMessageHandler" />.</summary>
		public void Dispose()
		{
			Dispose(disposing: true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpMessageHandler" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Send an HTTP request as an asynchronous operation.</summary>
		/// <param name="request">The HTTP request message to send.</param>
		/// <param name="cancellationToken">The cancellation token to cancel operation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was <see langword="null" />.</exception>
		protected internal abstract Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpMessageHandler" /> class.</summary>
		protected HttpMessageHandler()
		{
		}
	}
	/// <summary>A specialty class that allows applications to call the <see cref="M:System.Net.Http.HttpMessageInvoker.SendAsync(System.Net.Http.HttpRequestMessage,System.Threading.CancellationToken)" /> method on an HTTP handler chain.</summary>
	public class HttpMessageInvoker : IDisposable
	{
		private protected HttpMessageHandler handler;

		private readonly bool disposeHandler;

		/// <summary>Initializes an instance of a <see cref="T:System.Net.Http.HttpMessageInvoker" /> class with a specific <see cref="T:System.Net.Http.HttpMessageHandler" />.</summary>
		/// <param name="handler">The <see cref="T:System.Net.Http.HttpMessageHandler" /> responsible for processing the HTTP response messages.</param>
		public HttpMessageInvoker(HttpMessageHandler handler)
			: this(handler, disposeHandler: true)
		{
		}

		/// <summary>Initializes an instance of a <see cref="T:System.Net.Http.HttpMessageInvoker" /> class with a specific <see cref="T:System.Net.Http.HttpMessageHandler" />.</summary>
		/// <param name="handler">The <see cref="T:System.Net.Http.HttpMessageHandler" /> responsible for processing the HTTP response messages.</param>
		/// <param name="disposeHandler">
		///   <see langword="true" /> if the inner handler should be disposed of by Dispose(), <see langword="false" /> if you intend to reuse the inner handler.</param>
		public HttpMessageInvoker(HttpMessageHandler handler, bool disposeHandler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.handler = handler;
			this.disposeHandler = disposeHandler;
		}

		/// <summary>Releases the unmanaged resources and disposes of the managed resources used by the <see cref="T:System.Net.Http.HttpMessageInvoker" />.</summary>
		public void Dispose()
		{
			Dispose(disposing: true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpMessageInvoker" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && disposeHandler && handler != null)
			{
				handler.Dispose();
				handler = null;
			}
		}

		/// <summary>Send an HTTP request as an asynchronous operation.</summary>
		/// <param name="request">The HTTP request message to send.</param>
		/// <param name="cancellationToken">The cancellation token to cancel operation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was <see langword="null" />.</exception>
		public virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return handler.SendAsync(request, cancellationToken);
		}
	}
	/// <summary>A helper class for retrieving and comparing standard HTTP methods and for creating new HTTP methods.</summary>
	public class HttpMethod : IEquatable<HttpMethod>
	{
		private static readonly HttpMethod delete_method = new HttpMethod("DELETE");

		private static readonly HttpMethod get_method = new HttpMethod("GET");

		private static readonly HttpMethod head_method = new HttpMethod("HEAD");

		private static readonly HttpMethod options_method = new HttpMethod("OPTIONS");

		private static readonly HttpMethod post_method = new HttpMethod("POST");

		private static readonly HttpMethod put_method = new HttpMethod("PUT");

		private static readonly HttpMethod trace_method = new HttpMethod("TRACE");

		private readonly string method;

		/// <summary>Represents an HTTP DELETE protocol method.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		public static HttpMethod Delete => delete_method;

		/// <summary>Represents an HTTP GET protocol method.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		public static HttpMethod Get => get_method;

		/// <summary>Represents an HTTP HEAD protocol method. The HEAD method is identical to GET except that the server only returns message-headers in the response, without a message-body.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		public static HttpMethod Head => head_method;

		/// <summary>An HTTP method.</summary>
		/// <returns>An HTTP method represented as a <see cref="T:System.String" />.</returns>
		public string Method => method;

		/// <summary>Represents an HTTP OPTIONS protocol method.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		public static HttpMethod Options => options_method;

		/// <summary>Represents an HTTP POST protocol method that is used to post a new entity as an addition to a URI.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		public static HttpMethod Post => post_method;

		/// <summary>Represents an HTTP PUT protocol method that is used to replace an entity identified by a URI.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		public static HttpMethod Put => put_method;

		/// <summary>Represents an HTTP TRACE protocol method.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		public static HttpMethod Trace => trace_method;

		public static HttpMethod Patch
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpMethod" /> class with a specific HTTP method.</summary>
		/// <param name="method">The HTTP method.</param>
		public HttpMethod(string method)
		{
			if (string.IsNullOrEmpty(method))
			{
				throw new ArgumentException("method");
			}
			Parser.Token.Check(method);
			this.method = method;
		}

		/// <summary>The equality operator for comparing two <see cref="T:System.Net.Http.HttpMethod" /> objects.</summary>
		/// <param name="left">The left <see cref="T:System.Net.Http.HttpMethod" /> to an equality operator.</param>
		/// <param name="right">The right  <see cref="T:System.Net.Http.HttpMethod" /> to an equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <paramref name="left" /> and <paramref name="right" /> parameters are equal; otherwise, <see langword="false" />.</returns>
		public static bool operator ==(HttpMethod left, HttpMethod right)
		{
			if ((object)left == null || (object)right == null)
			{
				return (object)left == right;
			}
			return left.Equals(right);
		}

		/// <summary>The inequality operator for comparing two <see cref="T:System.Net.Http.HttpMethod" /> objects.</summary>
		/// <param name="left">The left <see cref="T:System.Net.Http.HttpMethod" /> to an inequality operator.</param>
		/// <param name="right">The right  <see cref="T:System.Net.Http.HttpMethod" /> to an inequality operator.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <paramref name="left" /> and <paramref name="right" /> parameters are inequal; otherwise, <see langword="false" />.</returns>
		public static bool operator !=(HttpMethod left, HttpMethod right)
		{
			return !(left == right);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Net.Http.HttpMethod" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <param name="other">The HTTP method to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equal to the current object; otherwise, <see langword="false" />.</returns>
		public bool Equals(HttpMethod other)
		{
			return string.Equals(method, other.method, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (obj is HttpMethod other)
			{
				return Equals(other);
			}
			return false;
		}

		/// <summary>Serves as a hash function for this type.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		public override int GetHashCode()
		{
			return method.GetHashCode();
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string representing the current object.</returns>
		public override string ToString()
		{
			return method;
		}
	}
	/// <summary>A base class for exceptions thrown by the <see cref="T:System.Net.Http.HttpClient" /> and <see cref="T:System.Net.Http.HttpMessageHandler" /> classes.</summary>
	[Serializable]
	public class HttpRequestException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestException" /> class.</summary>
		public HttpRequestException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestException" /> class with a specific message that describes the current exception.</summary>
		/// <param name="message">A message that describes the current exception.</param>
		public HttpRequestException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestException" /> class with a specific message that describes the current exception and an inner exception.</summary>
		/// <param name="message">A message that describes the current exception.</param>
		/// <param name="inner">The inner exception.</param>
		public HttpRequestException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
	/// <summary>Represents a HTTP request message.</summary>
	public class HttpRequestMessage : IDisposable
	{
		private HttpRequestHeaders headers;

		private HttpMethod method;

		private Version version;

		private Dictionary<string, object> properties;

		private Uri uri;

		private bool is_used;

		private bool disposed;

		/// <summary>Gets or sets the contents of the HTTP message.</summary>
		/// <returns>The content of a message</returns>
		public HttpContent Content { get; set; }

		/// <summary>Gets the collection of HTTP request headers.</summary>
		/// <returns>The collection of HTTP request headers.</returns>
		public HttpRequestHeaders Headers => headers ?? (headers = new HttpRequestHeaders());

		/// <summary>Gets or sets the HTTP method used by the HTTP request message.</summary>
		/// <returns>The HTTP method used by the request message. The default is the GET method.</returns>
		public HttpMethod Method
		{
			get
			{
				return method;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("method");
				}
				method = value;
			}
		}

		/// <summary>Gets a set of properties for the HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
		public IDictionary<string, object> Properties => properties ?? (properties = new Dictionary<string, object>());

		/// <summary>Gets or sets the <see cref="T:System.Uri" /> used for the HTTP request.</summary>
		/// <returns>The <see cref="T:System.Uri" /> used for the HTTP request.</returns>
		public Uri RequestUri
		{
			get
			{
				return uri;
			}
			set
			{
				if (value != null && value.IsAbsoluteUri && !IsAllowedAbsoluteUri(value))
				{
					throw new ArgumentException("Only http or https scheme is allowed");
				}
				uri = value;
			}
		}

		/// <summary>Gets or sets the HTTP message version.</summary>
		/// <returns>The HTTP message version. The default is 1.1.</returns>
		public Version Version
		{
			get
			{
				return version ?? HttpVersion.Version11;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Version");
				}
				version = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestMessage" /> class.</summary>
		public HttpRequestMessage()
		{
			method = HttpMethod.Get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestMessage" /> class with an HTTP method and a request <see cref="T:System.Uri" />.</summary>
		/// <param name="method">The HTTP method.</param>
		/// <param name="requestUri">A string that represents the request  <see cref="T:System.Uri" />.</param>
		public HttpRequestMessage(HttpMethod method, string requestUri)
			: this(method, string.IsNullOrEmpty(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpRequestMessage" /> class with an HTTP method and a request <see cref="T:System.Uri" />.</summary>
		/// <param name="method">The HTTP method.</param>
		/// <param name="requestUri">The <see cref="T:System.Uri" /> to request.</param>
		public HttpRequestMessage(HttpMethod method, Uri requestUri)
		{
			Method = method;
			RequestUri = requestUri;
		}

		private static bool IsAllowedAbsoluteUri(Uri uri)
		{
			if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
			{
				return true;
			}
			if (uri.Scheme == Uri.UriSchemeFile && uri.OriginalString.StartsWith("/", StringComparison.Ordinal))
			{
				return true;
			}
			return false;
		}

		/// <summary>Releases the unmanaged resources and disposes of the managed resources used by the <see cref="T:System.Net.Http.HttpRequestMessage" />.</summary>
		public void Dispose()
		{
			Dispose(disposing: true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpRequestMessage" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !disposed)
			{
				disposed = true;
				if (Content != null)
				{
					Content.Dispose();
				}
			}
		}

		internal bool SetIsUsed()
		{
			if (is_used)
			{
				return true;
			}
			is_used = true;
			return false;
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string representation of the current object.</returns>
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Method: ").Append(method);
			stringBuilder.Append(", RequestUri: '").Append((RequestUri != null) ? RequestUri.ToString() : "<null>");
			stringBuilder.Append("', Version: ").Append(Version);
			stringBuilder.Append(", Content: ").Append((Content != null) ? Content.ToString() : "<null>");
			stringBuilder.Append(", Headers:\r\n{\r\n").Append(Headers);
			if (Content != null)
			{
				stringBuilder.Append(Content.Headers);
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
	/// <summary>Represents a HTTP response message including the status code and data.</summary>
	public class HttpResponseMessage : IDisposable
	{
		private HttpResponseHeaders headers;

		private HttpResponseHeaders trailingHeaders;

		private string reasonPhrase;

		private HttpStatusCode statusCode;

		private Version version;

		private bool disposed;

		/// <summary>Gets or sets the content of a HTTP response message.</summary>
		/// <returns>The content of the HTTP response message.</returns>
		public HttpContent Content { get; set; }

		/// <summary>Gets the collection of HTTP response headers.</summary>
		/// <returns>The collection of HTTP response headers.</returns>
		public HttpResponseHeaders Headers => headers ?? (headers = new HttpResponseHeaders());

		/// <summary>Gets a value that indicates if the HTTP response was successful.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="P:System.Net.Http.HttpResponseMessage.StatusCode" /> was in the range 200-299; otherwise, <see langword="false" />.</returns>
		public bool IsSuccessStatusCode
		{
			get
			{
				if (statusCode >= HttpStatusCode.OK)
				{
					return statusCode < HttpStatusCode.MultipleChoices;
				}
				return false;
			}
		}

		/// <summary>Gets or sets the reason phrase which typically is sent by servers together with the status code.</summary>
		/// <returns>The reason phrase sent by the server.</returns>
		public string ReasonPhrase
		{
			get
			{
				return reasonPhrase ?? HttpStatusDescription.Get(statusCode);
			}
			set
			{
				reasonPhrase = value;
			}
		}

		/// <summary>Gets or sets the request message which led to this response message.</summary>
		/// <returns>The request message which led to this response message.</returns>
		public HttpRequestMessage RequestMessage { get; set; }

		/// <summary>Gets or sets the status code of the HTTP response.</summary>
		/// <returns>The status code of the HTTP response.</returns>
		public HttpStatusCode StatusCode
		{
			get
			{
				return statusCode;
			}
			set
			{
				if (value < (HttpStatusCode)0)
				{
					throw new ArgumentOutOfRangeException();
				}
				statusCode = value;
			}
		}

		/// <summary>Gets or sets the HTTP message version.</summary>
		/// <returns>The HTTP message version. The default is 1.1.</returns>
		public Version Version
		{
			get
			{
				return version ?? HttpVersion.Version11;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Version");
				}
				version = value;
			}
		}

		public HttpResponseHeaders TrailingHeaders
		{
			get
			{
				if (trailingHeaders == null)
				{
					trailingHeaders = new HttpResponseHeaders();
				}
				return trailingHeaders;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpResponseMessage" /> class.</summary>
		public HttpResponseMessage()
			: this(HttpStatusCode.OK)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpResponseMessage" /> class with a specific <see cref="P:System.Net.Http.HttpResponseMessage.StatusCode" />.</summary>
		/// <param name="statusCode">The status code of the HTTP response.</param>
		public HttpResponseMessage(HttpStatusCode statusCode)
		{
			StatusCode = statusCode;
		}

		/// <summary>Releases the unmanaged resources and disposes of unmanaged resources used by the <see cref="T:System.Net.Http.HttpResponseMessage" />.</summary>
		public void Dispose()
		{
			Dispose(disposing: true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpResponseMessage" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !disposed)
			{
				disposed = true;
				if (Content != null)
				{
					Content.Dispose();
				}
			}
		}

		/// <summary>Throws an exception if the <see cref="P:System.Net.Http.HttpResponseMessage.IsSuccessStatusCode" /> property for the HTTP response is <see langword="false" />.</summary>
		/// <returns>The HTTP response message if the call is successful.</returns>
		/// <exception cref="T:System.Net.Http.HttpRequestException">The HTTP response is unsuccessful.</exception>
		public HttpResponseMessage EnsureSuccessStatusCode()
		{
			if (IsSuccessStatusCode)
			{
				return this;
			}
			throw new HttpRequestException($"{(int)statusCode} ({ReasonPhrase})");
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string representation of the current object.</returns>
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StatusCode: ").Append((int)StatusCode);
			stringBuilder.Append(", ReasonPhrase: '").Append(ReasonPhrase ?? "<null>");
			stringBuilder.Append("', Version: ").Append(Version);
			stringBuilder.Append(", Content: ").Append((Content != null) ? Content.ToString() : "<null>");
			stringBuilder.Append(", Headers:\r\n{\r\n").Append(Headers);
			if (Content != null)
			{
				stringBuilder.Append(Content.Headers);
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
	/// <summary>A base type for handlers which only do some small processing of request and/or response messages.</summary>
	public abstract class MessageProcessingHandler : DelegatingHandler
	{
		/// <summary>Creates an instance of a <see cref="T:System.Net.Http.MessageProcessingHandler" /> class.</summary>
		protected MessageProcessingHandler()
		{
		}

		/// <summary>Creates an instance of a <see cref="T:System.Net.Http.MessageProcessingHandler" /> class with a specific inner handler.</summary>
		/// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
		protected MessageProcessingHandler(HttpMessageHandler innerHandler)
			: base(innerHandler)
		{
		}

		/// <summary>Performs processing on each request sent to the server.</summary>
		/// <param name="request">The HTTP request message to process.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The HTTP request message that was processed.</returns>
		protected abstract HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken);

		/// <summary>Perform processing on each response from the server.</summary>
		/// <param name="response">The HTTP response message to process.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The HTTP response message that was processed.</returns>
		protected abstract HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken);

		/// <summary>Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.</summary>
		/// <param name="request">The HTTP request message to send to the server.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="request" /> was <see langword="null" />.</exception>
		protected internal sealed override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			request = ProcessRequest(request, cancellationToken);
			return ProcessResponse(await base.SendAsync(request, cancellationToken).ConfigureAwait(continueOnCapturedContext: false), cancellationToken);
		}
	}
	/// <summary>Provides a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized using the multipart/* content type specification.</summary>
	public class MultipartContent : HttpContent, IEnumerable<HttpContent>, IEnumerable
	{
		private List<HttpContent> nested_content;

		private readonly string boundary;

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartContent" /> class.</summary>
		public MultipartContent()
			: this("mixed")
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartContent" /> class.</summary>
		/// <param name="subtype">The subtype of the multipart content.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="subtype" /> was <see langword="null" /> or contains only white space characters.</exception>
		public MultipartContent(string subtype)
			: this(subtype, Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture))
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartContent" /> class.</summary>
		/// <param name="subtype">The subtype of the multipart content.</param>
		/// <param name="boundary">The boundary string for the multipart content.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="subtype" /> was <see langword="null" /> or an empty string.  
		///  The <paramref name="boundary" /> was <see langword="null" /> or contains only white space characters.  
		///  -or-  
		///  The <paramref name="boundary" /> ends with a space character.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the <paramref name="boundary" /> was greater than 70.</exception>
		public MultipartContent(string subtype, string boundary)
		{
			if (string.IsNullOrWhiteSpace(subtype))
			{
				throw new ArgumentException("boundary");
			}
			if (string.IsNullOrWhiteSpace(boundary))
			{
				throw new ArgumentException("boundary");
			}
			if (boundary.Length > 70)
			{
				throw new ArgumentOutOfRangeException("boundary");
			}
			if (boundary.Last() == ' ' || !IsValidRFC2049(boundary))
			{
				throw new ArgumentException("boundary");
			}
			this.boundary = boundary;
			nested_content = new List<HttpContent>(2);
			base.Headers.ContentType = new MediaTypeHeaderValue("multipart/" + subtype)
			{
				Parameters = 
				{
					new NameValueHeaderValue("boundary", "\"" + boundary + "\"")
				}
			};
		}

		private static bool IsValidRFC2049(string s)
		{
			foreach (char c in s)
			{
				if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && (c < '0' || c > '9'))
				{
					switch (c)
					{
					case '\'':
					case '(':
					case ')':
					case '+':
					case ',':
					case '-':
					case '.':
					case '/':
					case ':':
					case '=':
					case '?':
						continue;
					}
					return false;
				}
			}
			return true;
		}

		/// <summary>Add multipart HTTP content to a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized using the multipart/* content type specification.</summary>
		/// <param name="content">The HTTP content to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> was <see langword="null" />.</exception>
		public virtual void Add(HttpContent content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (nested_content == null)
			{
				nested_content = new List<HttpContent>();
			}
			nested_content.Add(content);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.MultipartContent" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (HttpContent item in nested_content)
				{
					item.Dispose();
				}
				nested_content = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Serialize the multipart HTTP content to a stream as an asynchronous operation.</summary>
		/// <param name="stream">The target stream.</param>
		/// <param name="context">Information about the transport (channel binding token, for example). This parameter may be <see langword="null" />.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append('-').Append('-');
			sb.Append(boundary);
			sb.Append('\r').Append('\n');
			byte[] bytes;
			for (int i = 0; i < nested_content.Count; i++)
			{
				HttpContent c = nested_content[i];
				foreach (KeyValuePair<string, IEnumerable<string>> header in c.Headers)
				{
					sb.Append(header.Key);
					sb.Append(':').Append(' ');
					foreach (string item in header.Value)
					{
						sb.Append(item);
					}
					sb.Append('\r').Append('\n');
				}
				sb.Append('\r').Append('\n');
				bytes = Encoding.ASCII.GetBytes(sb.ToString());
				sb.Clear();
				await stream.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(continueOnCapturedContext: false);
				await c.SerializeToStreamAsync_internal(stream, context).ConfigureAwait(continueOnCapturedContext: false);
				if (i != nested_content.Count - 1)
				{
					sb.Append('\r').Append('\n');
					sb.Append('-').Append('-');
					sb.Append(boundary);
					sb.Append('\r').Append('\n');
				}
			}
			sb.Append('\r').Append('\n');
			sb.Append('-').Append('-');
			sb.Append(boundary);
			sb.Append('-').Append('-');
			sb.Append('\r').Append('\n');
			bytes = Encoding.ASCII.GetBytes(sb.ToString());
			await stream.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(continueOnCapturedContext: false);
		}

		/// <summary>Determines whether the HTTP multipart content has a valid length in bytes.</summary>
		/// <param name="length">The length in bytes of the HHTP content.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="length" /> is a valid length; otherwise, <see langword="false" />.</returns>
		protected internal override bool TryComputeLength(out long length)
		{
			length = 12 + 2 * boundary.Length;
			for (int i = 0; i < nested_content.Count; i++)
			{
				HttpContent httpContent = nested_content[i];
				foreach (KeyValuePair<string, IEnumerable<string>> header in httpContent.Headers)
				{
					length += header.Key.Length;
					length += 4L;
					foreach (string item in header.Value)
					{
						length += item.Length;
					}
				}
				if (!httpContent.TryComputeLength(out var length2))
				{
					return false;
				}
				length += 2L;
				length += length2;
				if (i != nested_content.Count - 1)
				{
					length += 6L;
					length += boundary.Length;
				}
			}
			return true;
		}

		/// <summary>Returns an enumerator that iterates through the collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized using the multipart/* content type specification.</summary>
		/// <returns>An object that can be used to iterate through the collection.</returns>
		public IEnumerator<HttpContent> GetEnumerator()
		{
			return nested_content.GetEnumerator();
		}

		/// <summary>The explicit implementation of the <see cref="M:System.Net.Http.MultipartContent.GetEnumerator" /> method.</summary>
		/// <returns>An object that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return nested_content.GetEnumerator();
		}
	}
	/// <summary>Provides a container for content encoded using multipart/form-data MIME type.</summary>
	public class MultipartFormDataContent : MultipartContent
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartFormDataContent" /> class.</summary>
		public MultipartFormDataContent()
			: base("form-data")
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartFormDataContent" /> class.</summary>
		/// <param name="boundary">The boundary string for the multipart form data content.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="boundary" /> was <see langword="null" /> or contains only white space characters.  
		///  -or-  
		///  The <paramref name="boundary" /> ends with a space character.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the <paramref name="boundary" /> was greater than 70.</exception>
		public MultipartFormDataContent(string boundary)
			: base("form-data", boundary)
		{
		}

		/// <summary>Add HTTP content to a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized to multipart/form-data MIME type.</summary>
		/// <param name="content">The HTTP content to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> was <see langword="null" />.</exception>
		public override void Add(HttpContent content)
		{
			base.Add(content);
			AddContentDisposition(content, null, null);
		}

		/// <summary>Add HTTP content to a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized to multipart/form-data MIME type.</summary>
		/// <param name="content">The HTTP content to add to the collection.</param>
		/// <param name="name">The name for the HTTP content to add.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> was <see langword="null" /> or contains only white space characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> was <see langword="null" />.</exception>
		public void Add(HttpContent content, string name)
		{
			base.Add(content);
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("name");
			}
			AddContentDisposition(content, name, null);
		}

		/// <summary>Add HTTP content to a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized to multipart/form-data MIME type.</summary>
		/// <param name="content">The HTTP content to add to the collection.</param>
		/// <param name="name">The name for the HTTP content to add.</param>
		/// <param name="fileName">The file name for the HTTP content to add to the collection.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> was <see langword="null" /> or contains only white space characters.  
		///  -or-  
		///  The <paramref name="fileName" /> was <see langword="null" /> or contains only white space characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> was <see langword="null" />.</exception>
		public void Add(HttpContent content, string name, string fileName)
		{
			base.Add(content);
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("name");
			}
			if (string.IsNullOrWhiteSpace(fileName))
			{
				throw new ArgumentException("fileName");
			}
			AddContentDisposition(content, name, fileName);
		}

		private void AddContentDisposition(HttpContent content, string name, string fileName)
		{
			HttpContentHeaders httpContentHeaders = content.Headers;
			if (httpContentHeaders.ContentDisposition == null)
			{
				httpContentHeaders.ContentDisposition = new ContentDispositionHeaderValue("form-data")
				{
					Name = name,
					FileName = fileName,
					FileNameStar = fileName
				};
			}
		}
	}
	/// <summary>Provides HTTP content based on a stream.</summary>
	public class StreamContent : HttpContent
	{
		private readonly Stream content;

		private readonly int bufferSize;

		private readonly CancellationToken cancellationToken;

		private readonly long startPosition;

		private bool contentCopied;

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.StreamContent" /> class.</summary>
		/// <param name="content">The content used to initialize the <see cref="T:System.Net.Http.StreamContent" />.</param>
		public StreamContent(Stream content)
			: this(content, 16384)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.StreamContent" /> class.</summary>
		/// <param name="content">The content used to initialize the <see cref="T:System.Net.Http.StreamContent" />.</param>
		/// <param name="bufferSize">The size, in bytes, of the buffer for the <see cref="T:System.Net.Http.StreamContent" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> was <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="bufferSize" /> was less than or equal to zero.</exception>
		public StreamContent(Stream content, int bufferSize)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			this.content = content;
			this.bufferSize = bufferSize;
			if (content.CanSeek)
			{
				startPosition = content.Position;
			}
		}

		internal StreamContent(Stream content, CancellationToken cancellationToken)
			: this(content)
		{
			this.cancellationToken = cancellationToken;
		}

		/// <summary>Write the HTTP stream content to a memory stream as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		protected override Task<Stream> CreateContentReadStreamAsync()
		{
			return Task.FromResult(content);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.StreamContent" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				content.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Serialize the HTTP content to a stream as an asynchronous operation.</summary>
		/// <param name="stream">The target stream.</param>
		/// <param name="context">Information about the transport (channel binding token, for example). This parameter may be <see langword="null" />.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			if (contentCopied)
			{
				if (!content.CanSeek)
				{
					throw new InvalidOperationException("The stream was already consumed. It cannot be read again.");
				}
				content.Seek(startPosition, SeekOrigin.Begin);
			}
			else
			{
				contentCopied = true;
			}
			return content.CopyToAsync(stream, bufferSize, cancellationToken);
		}

		/// <summary>Determines whether the stream content has a valid length in bytes.</summary>
		/// <param name="length">The length in bytes of the stream content.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="length" /> is a valid length; otherwise, <see langword="false" />.</returns>
		protected internal override bool TryComputeLength(out long length)
		{
			if (!content.CanSeek)
			{
				length = 0L;
				return false;
			}
			length = content.Length - startPosition;
			return true;
		}
	}
	/// <summary>Provides HTTP content based on a string.</summary>
	public class StringContent : ByteArrayContent
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.StringContent" /> class.</summary>
		/// <param name="content">The content used to initialize the <see cref="T:System.Net.Http.StringContent" />.</param>
		public StringContent(string content)
			: this(content, null, null)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.StringContent" /> class.</summary>
		/// <param name="content">The content used to initialize the <see cref="T:System.Net.Http.StringContent" />.</param>
		/// <param name="encoding">The encoding to use for the content.</param>
		public StringContent(string content, Encoding encoding)
			: this(content, encoding, null)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.StringContent" /> class.</summary>
		/// <param name="content">The content used to initialize the <see cref="T:System.Net.Http.StringContent" />.</param>
		/// <param name="encoding">The encoding to use for the content.</param>
		/// <param name="mediaType">The media type to use for the content.</param>
		public StringContent(string content, Encoding encoding, string mediaType)
			: base(GetByteArray(content, encoding))
		{
			base.Headers.ContentType = new MediaTypeHeaderValue(mediaType ?? "text/plain")
			{
				CharSet = (encoding ?? Encoding.UTF8).WebName
			};
		}

		private static byte[] GetByteArray(string content, Encoding encoding)
		{
			return (encoding ?? Encoding.UTF8).GetBytes(content);
		}
	}
	public sealed class ReadOnlyMemoryContent : HttpContent
	{
		public ReadOnlyMemoryContent(ReadOnlyMemory<byte> content)
		{
			throw new PlatformNotSupportedException();
		}

		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			throw new PlatformNotSupportedException();
		}

		protected internal override bool TryComputeLength(out long length)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
namespace System.Net.Http.Headers
{
	/// <summary>Represents authentication information in Authorization, ProxyAuthorization, WWW-Authenticate, and Proxy-Authenticate header values.</summary>
	public class AuthenticationHeaderValue : ICloneable
	{
		/// <summary>Gets the credentials containing the authentication information of the user agent for the resource being requested.</summary>
		/// <returns>The credentials containing the authentication information.</returns>
		public string Parameter { get; private set; }

		/// <summary>Gets the scheme to use for authorization.</summary>
		/// <returns>The scheme to use for authorization.</returns>
		public string Scheme { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> class.</summary>
		/// <param name="scheme">The scheme to use for authorization.</param>
		public AuthenticationHeaderValue(string scheme)
			: this(scheme, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> class.</summary>
		/// <param name="scheme">The scheme to use for authorization.</param>
		/// <param name="parameter">The credentials containing the authentication information of the user agent for the resource being requested.</param>
		public AuthenticationHeaderValue(string scheme, string parameter)
		{
			Parser.Token.Check(scheme);
			Scheme = scheme;
			Parameter = parameter;
		}

		private AuthenticationHeaderValue()
		{
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (obj is AuthenticationHeaderValue authenticationHeaderValue && string.Equals(authenticationHeaderValue.Scheme, Scheme, StringComparison.OrdinalIgnoreCase))
			{
				return authenticationHeaderValue.Parameter == Parameter;
			}
			return false;
		}

		/// <summary>Serves as a hash function for an  <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			int num = Scheme.ToLowerInvariant().GetHashCode();
			if (!string.IsNullOrEmpty(Parameter))
			{
				num ^= Parameter.ToLowerInvariant().GetHashCode();
			}
			return num;
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents authentication header value information.</param>
		/// <returns>An <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid authentication header value information.</exception>
		public static AuthenticationHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out AuthenticationHeaderValue parsedValue)
		{
			if (TryParseElement(new Lexer(input), out parsedValue, out var t) && (Token.Type)t == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		internal static bool TryParse(string input, int minimalCount, out List<AuthenticationHeaderValue> result)
		{
			return CollectionParser.TryParse(input, minimalCount, (ElementTryParser<AuthenticationHeaderValue>)TryParseElement, out result);
		}

		private static bool TryParseElement(Lexer lexer, out AuthenticationHeaderValue parsedValue, out Token t)
		{
			t = lexer.Scan();
			if ((Token.Type)t != Token.Type.Token)
			{
				parsedValue = null;
				return false;
			}
			parsedValue = new AuthenticationHeaderValue();
			parsedValue.Scheme = lexer.GetStringValue(t);
			t = lexer.Scan();
			if ((Token.Type)t == Token.Type.Token)
			{
				parsedValue.Parameter = lexer.GetRemainingStringValue(t.StartPosition);
				t = new Token(Token.Type.End, 0, 0);
			}
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			if (Parameter == null)
			{
				return Scheme;
			}
			return Scheme + " " + Parameter;
		}
	}
	/// <summary>Represents the value of the Cache-Control header.</summary>
	public class CacheControlHeaderValue : ICloneable
	{
		private List<NameValueHeaderValue> extensions;

		private List<string> no_cache_headers;

		private List<string> private_headers;

		/// <summary>Cache-extension tokens, each with an optional assigned value.</summary>
		/// <returns>A collection of cache-extension tokens each with an optional assigned value.</returns>
		public ICollection<NameValueHeaderValue> Extensions => extensions ?? (extensions = new List<NameValueHeaderValue>());

		/// <summary>The maximum age, specified in seconds, that the HTTP client is willing to accept a response.</summary>
		/// <returns>The time in seconds.</returns>
		public TimeSpan? MaxAge { get; set; }

		/// <summary>Whether an HTTP client is willing to accept a response that has exceeded its expiration time.</summary>
		/// <returns>
		///   <see langword="true" /> if the HTTP client is willing to accept a response that has exceed the expiration time; otherwise, <see langword="false" />.</returns>
		public bool MaxStale { get; set; }

		/// <summary>The maximum time, in seconds, an HTTP client is willing to accept a response that has exceeded its expiration time.</summary>
		/// <returns>The time in seconds.</returns>
		public TimeSpan? MaxStaleLimit { get; set; }

		/// <summary>The freshness lifetime, in seconds, that an HTTP client is willing to accept a response.</summary>
		/// <returns>The time in seconds.</returns>
		public TimeSpan? MinFresh { get; set; }

		/// <summary>Whether the origin server require revalidation of a cache entry on any subsequent use when the cache entry becomes stale.</summary>
		/// <returns>
		///   <see langword="true" /> if the origin server requires revalidation of a cache entry on any subsequent use when the entry becomes stale; otherwise, <see langword="false" />.</returns>
		public bool MustRevalidate { get; set; }

		/// <summary>Whether an HTTP client is willing to accept a cached response.</summary>
		/// <returns>
		///   <see langword="true" /> if the HTTP client is willing to accept a cached response; otherwise, <see langword="false" />.</returns>
		public bool NoCache { get; set; }

		/// <summary>A collection of fieldnames in the "no-cache" directive in a cache-control header field on an HTTP response.</summary>
		/// <returns>A collection of fieldnames.</returns>
		public ICollection<string> NoCacheHeaders => no_cache_headers ?? (no_cache_headers = new List<string>());

		/// <summary>Whether a cache must not store any part of either the HTTP request mressage or any response.</summary>
		/// <returns>
		///   <see langword="true" /> if a cache must not store any part of either the HTTP request mressage or any response; otherwise, <see langword="false" />.</returns>
		public bool NoStore { get; set; }

		/// <summary>Whether a cache or proxy must not change any aspect of the entity-body.</summary>
		/// <returns>
		///   <see langword="true" /> if a cache or proxy must not change any aspect of the entity-body; otherwise, <see langword="false" />.</returns>
		public bool NoTransform { get; set; }

		/// <summary>Whether a cache should either respond using a cached entry that is consistent with the other constraints of the HTTP request, or respond with a 504 (Gateway Timeout) status.</summary>
		/// <returns>
		///   <see langword="true" /> if a cache should either respond using a cached entry that is consistent with the other constraints of the HTTP request, or respond with a 504 (Gateway Timeout) status; otherwise, <see langword="false" />.</returns>
		public bool OnlyIfCached { get; set; }

		/// <summary>Whether all or part of the HTTP response message is intended for a single user and must not be cached by a shared cache.</summary>
		/// <returns>
		///   <see langword="true" /> if the HTTP response message is intended for a single user and must not be cached by a shared cache; otherwise, <see langword="false" />.</returns>
		public bool Private { get; set; }

		/// <summary>A collection fieldnames in the "private" directive in a cache-control header field on an HTTP response.</summary>
		/// <returns>A collection of fieldnames.</returns>
		public ICollection<string> PrivateHeaders => private_headers ?? (private_headers = new List<string>());

		/// <summary>Whether the origin server require revalidation of a cache entry on any subsequent use when the cache entry becomes stale for shared user agent caches.</summary>
		/// <returns>
		///   <see langword="true" /> if the origin server requires revalidation of a cache entry on any subsequent use when the entry becomes stale for shared user agent caches; otherwise, <see langword="false" />.</returns>
		public bool ProxyRevalidate { get; set; }

		/// <summary>Whether an HTTP response may be cached by any cache, even if it would normally be non-cacheable or cacheable only within a non- shared cache.</summary>
		/// <returns>
		///   <see langword="true" /> if the HTTP response may be cached by any cache, even if it would normally be non-cacheable or cacheable only within a non- shared cache; otherwise, <see langword="false" />.</returns>
		public bool Public { get; set; }

		/// <summary>The shared maximum age, specified in seconds, in an HTTP response that overrides the "max-age" directive in a cache-control header or an Expires header for a shared cache.</summary>
		/// <returns>The time in seconds.</returns>
		public TimeSpan? SharedMaxAge { get; set; }

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			CacheControlHeaderValue cacheControlHeaderValue = (CacheControlHeaderValue)MemberwiseClone();
			if (extensions != null)
			{
				cacheControlHeaderValue.extensions = new List<NameValueHeaderValue>();
				foreach (NameValueHeaderValue extension in extensions)
				{
					cacheControlHeaderValue.extensions.Add(extension);
				}
			}
			if (no_cache_headers != null)
			{
				cacheControlHeaderValue.no_cache_headers = new List<string>();
				foreach (string no_cache_header in no_cache_headers)
				{
					cacheControlHeaderValue.no_cache_headers.Add(no_cache_header);
				}
			}
			if (private_headers != null)
			{
				cacheControlHeaderValue.private_headers = new List<string>();
				foreach (string private_header in private_headers)
				{
					cacheControlHeaderValue.private_headers.Add(private_header);
				}
			}
			return cacheControlHeaderValue;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is CacheControlHeaderValue cacheControlHeaderValue))
			{
				return false;
			}
			TimeSpan? maxAge = MaxAge;
			TimeSpan? maxAge2 = cacheControlHeaderValue.MaxAge;
			if (maxAge.HasValue == maxAge2.HasValue && (!maxAge.HasValue || !(maxAge.GetValueOrDefault() != maxAge2.GetValueOrDefault())) && MaxStale == cacheControlHeaderValue.MaxStale && !(MaxStaleLimit != cacheControlHeaderValue.MaxStaleLimit))
			{
				maxAge = MinFresh;
				maxAge2 = cacheControlHeaderValue.MinFresh;
				if (maxAge.HasValue == maxAge2.HasValue && (!maxAge.HasValue || !(maxAge.GetValueOrDefault() != maxAge2.GetValueOrDefault())) && MustRevalidate == cacheControlHeaderValue.MustRevalidate && NoCache == cacheControlHeaderValue.NoCache && NoStore == cacheControlHeaderValue.NoStore && NoTransform == cacheControlHeaderValue.NoTransform && OnlyIfCached == cacheControlHeaderValue.OnlyIfCached && Private == cacheControlHeaderValue.Private && ProxyRevalidate == cacheControlHeaderValue.ProxyRevalidate && Public == cacheControlHeaderValue.Public && !(SharedMaxAge != cacheControlHeaderValue.SharedMaxAge))
				{
					if (extensions.SequenceEqual(cacheControlHeaderValue.extensions) && no_cache_headers.SequenceEqual(cacheControlHeaderValue.no_cache_headers))
					{
						return private_headers.SequenceEqual(cacheControlHeaderValue.private_headers);
					}
					return false;
				}
			}
			return false;
		}

		/// <summary>Serves as a hash function for a  <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return (((((((((((((((29 * 29 + HashCodeCalculator.Calculate(extensions)) * 29 + MaxAge.GetHashCode()) * 29 + MaxStale.GetHashCode()) * 29 + MaxStaleLimit.GetHashCode()) * 29 + MinFresh.GetHashCode()) * 29 + MustRevalidate.GetHashCode()) * 29 + HashCodeCalculator.Calculate(no_cache_headers)) * 29 + NoCache.GetHashCode()) * 29 + NoStore.GetHashCode()) * 29 + NoTransform.GetHashCode()) * 29 + OnlyIfCached.GetHashCode()) * 29 + Private.GetHashCode()) * 29 + HashCodeCalculator.Calculate(private_headers)) * 29 + ProxyRevalidate.GetHashCode()) * 29 + Public.GetHashCode()) * 29 + SharedMaxAge.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents cache-control header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid cache-control header value information.</exception>
		public static CacheControlHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out CacheControlHeaderValue parsedValue)
		{
			parsedValue = null;
			if (input == null)
			{
				return true;
			}
			CacheControlHeaderValue cacheControlHeaderValue = new CacheControlHeaderValue();
			Lexer lexer = new Lexer(input);
			Token token;
			do
			{
				token = lexer.Scan();
				if ((Token.Type)token != Token.Type.Token)
				{
					return false;
				}
				string stringValue = lexer.GetStringValue(token);
				bool flag = false;
				switch (stringValue)
				{
				case "no-store":
					cacheControlHeaderValue.NoStore = true;
					break;
				case "no-transform":
					cacheControlHeaderValue.NoTransform = true;
					break;
				case "only-if-cached":
					cacheControlHeaderValue.OnlyIfCached = true;
					break;
				case "public":
					cacheControlHeaderValue.Public = true;
					break;
				case "must-revalidate":
					cacheControlHeaderValue.MustRevalidate = true;
					break;
				case "proxy-revalidate":
					cacheControlHeaderValue.ProxyRevalidate = true;
					break;
				case "max-stale":
				{
					cacheControlHeaderValue.MaxStale = true;
					token = lexer.Scan();
					if ((Token.Type)token != Token.Type.SeparatorEqual)
					{
						flag = true;
						break;
					}
					token = lexer.Scan();
					if ((Token.Type)token != Token.Type.Token)
					{
						return false;
					}
					TimeSpan? maxStaleLimit = lexer.TryGetTimeSpanValue(token);
					if (!maxStaleLimit.HasValue)
					{
						return false;
					}
					cacheControlHeaderValue.MaxStaleLimit = maxStaleLimit;
					break;
				}
				case "max-age":
				case "s-maxage":
				case "min-fresh":
				{
					token = lexer.Scan();
					if ((Token.Type)token != Token.Type.SeparatorEqual)
					{
						return false;
					}
					token = lexer.Scan();
					if ((Token.Type)token != Token.Type.Token)
					{
						return false;
					}
					TimeSpan? maxStaleLimit = lexer.TryGetTimeSpanValue(token);
					if (!maxStaleLimit.HasValue)
					{
						return false;
					}
					switch (stringValue.Length)
					{
					case 7:
						cacheControlHeaderValue.MaxAge = maxStaleLimit;
						break;
					case 8:
						cacheControlHeaderValue.SharedMaxAge = maxStaleLimit;
						break;
					default:
						cacheControlHeaderValue.MinFresh = maxStaleLimit;
						break;
					}
					break;
				}
				case "private":
				case "no-cache":
				{
					if (stringValue.Length == 7)
					{
						cacheControlHeaderValue.Private = true;
					}
					else
					{
						cacheControlHeaderValue.NoCache = true;
					}
					token = lexer.Scan();
					if ((Token.Type)token != Token.Type.SeparatorEqual)
					{
						flag = true;
						break;
					}
					token = lexer.Scan();
					if ((Token.Type)token != Token.Type.QuotedString)
					{
						return false;
					}
					string[] array = lexer.GetQuotedStringValue(token).Split(',');
					for (int i = 0; i < array.Length; i++)
					{
						string item = array[i].Trim('\t', ' ');
						if (stringValue.Length == 7)
						{
							cacheControlHeaderValue.PrivateHeaders.Add(item);
							continue;
						}
						cacheControlHeaderValue.NoCache = true;
						cacheControlHeaderValue.NoCacheHeaders.Add(item);
					}
					break;
				}
				default:
				{
					string stringValue2 = lexer.GetStringValue(token);
					string value = null;
					token = lexer.Scan();
					if ((Token.Type)token == Token.Type.SeparatorEqual)
					{
						token = lexer.Scan();
						Token.Type kind = token.Kind;
						if ((uint)(kind - 2) > 1u)
						{
							return false;
						}
						value = lexer.GetStringValue(token);
					}
					else
					{
						flag = true;
					}
					cacheControlHeaderValue.Extensions.Add(NameValueHeaderValue.Create(stringValue2, value));
					break;
				}
				}
				if (!flag)
				{
					token = lexer.Scan();
				}
			}
			while ((Token.Type)token == Token.Type.SeparatorComma);
			if ((Token.Type)token != Token.Type.End)
			{
				return false;
			}
			parsedValue = cacheControlHeaderValue;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (NoStore)
			{
				stringBuilder.Append("no-store");
				stringBuilder.Append(", ");
			}
			if (NoTransform)
			{
				stringBuilder.Append("no-transform");
				stringBuilder.Append(", ");
			}
			if (OnlyIfCached)
			{
				stringBuilder.Append("only-if-cached");
				stringBuilder.Append(", ");
			}
			if (Public)
			{
				stringBuilder.Append("public");
				stringBuilder.Append(", ");
			}
			if (MustRevalidate)
			{
				stringBuilder.Append("must-revalidate");
				stringBuilder.Append(", ");
			}
			if (ProxyRevalidate)
			{
				stringBuilder.Append("proxy-revalidate");
				stringBuilder.Append(", ");
			}
			if (NoCache)
			{
				stringBuilder.Append("no-cache");
				if (no_cache_headers != null)
				{
					stringBuilder.Append("=\"");
					no_cache_headers.ToStringBuilder(stringBuilder);
					stringBuilder.Append("\"");
				}
				stringBuilder.Append(", ");
			}
			if (MaxAge.HasValue)
			{
				stringBuilder.Append("max-age=");
				stringBuilder.Append(MaxAge.Value.TotalSeconds.ToString(CultureInfo.InvariantCulture));
				stringBuilder.Append(", ");
			}
			if (SharedMaxAge.HasValue)
			{
				stringBuilder.Append("s-maxage=");
				stringBuilder.Append(SharedMaxAge.Value.TotalSeconds.ToString(CultureInfo.InvariantCulture));
				stringBuilder.Append(", ");
			}
			if (MaxStale)
			{
				stringBuilder.Append("max-stale");
				if (MaxStaleLimit.HasValue)
				{
					stringBuilder.Append("=");
					stringBuilder.Append(MaxStaleLimit.Value.TotalSeconds.ToString(CultureInfo.InvariantCulture));
				}
				stringBuilder.Append(", ");
			}
			if (MinFresh.HasValue)
			{
				stringBuilder.Append("min-fresh=");
				stringBuilder.Append(MinFresh.Value.TotalSeconds.ToString(CultureInfo.InvariantCulture));
				stringBuilder.Append(", ");
			}
			if (Private)
			{
				stringBuilder.Append("private");
				if (private_headers != null)
				{
					stringBuilder.Append("=\"");
					private_headers.ToStringBuilder(stringBuilder);
					stringBuilder.Append("\"");
				}
				stringBuilder.Append(", ");
			}
			extensions.ToStringBuilder(stringBuilder);
			if (stringBuilder.Length > 2 && stringBuilder[stringBuilder.Length - 2] == ',' && stringBuilder[stringBuilder.Length - 1] == ' ')
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
			}
			return stringBuilder.ToString();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.CacheControlHeaderValue" /> class.</summary>
		public CacheControlHeaderValue()
		{
		}
	}
	internal static class CollectionExtensions
	{
		public static bool SequenceEqual<TSource>(this List<TSource> first, List<TSource> second)
		{
			if (first == null)
			{
				if (second != null)
				{
					return second.Count == 0;
				}
				return true;
			}
			if (second == null)
			{
				if (first != null)
				{
					return first.Count == 0;
				}
				return true;
			}
			return Enumerable.SequenceEqual(first, second);
		}

		public static void SetValue(this List<NameValueHeaderValue> parameters, string key, string value)
		{
			for (int i = 0; i < parameters.Count; i++)
			{
				if (string.Equals(parameters[i].Name, key, StringComparison.OrdinalIgnoreCase))
				{
					if (value == null)
					{
						parameters.RemoveAt(i);
					}
					else
					{
						parameters[i].Value = value;
					}
					return;
				}
			}
			if (!string.IsNullOrEmpty(value))
			{
				parameters.Add(new NameValueHeaderValue(key, value));
			}
		}

		public static string ToString<T>(this List<T> list)
		{
			if (list == null || list.Count == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < list.Count; i++)
			{
				stringBuilder.Append("; ");
				stringBuilder.Append(list[i]);
			}
			return stringBuilder.ToString();
		}

		public static void ToStringBuilder<T>(this List<T> list, StringBuilder sb)
		{
			if (list == null || list.Count == 0)
			{
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					sb.Append(", ");
				}
				sb.Append(list[i]);
			}
		}
	}
	internal delegate bool ElementTryParser<T>(Lexer lexer, out T parsedValue, out Token token);
	internal static class CollectionParser
	{
		public static bool TryParse<T>(string input, int minimalCount, ElementTryParser<T> parser, out List<T> result) where T : class
		{
			Lexer lexer = new Lexer(input);
			result = new List<T>();
			Token token;
			do
			{
				if (!parser(lexer, out var parsedValue, out token))
				{
					return false;
				}
				if (parsedValue != null)
				{
					result.Add(parsedValue);
				}
			}
			while ((Token.Type)token == Token.Type.SeparatorComma);
			if ((Token.Type)token == Token.Type.End)
			{
				if (minimalCount > result.Count)
				{
					result = null;
					return false;
				}
				return true;
			}
			result = null;
			return false;
		}

		public static bool TryParse(string input, int minimalCount, out List<string> result)
		{
			return TryParse(input, minimalCount, (ElementTryParser<string>)TryParseStringElement, out result);
		}

		public static bool TryParseRepetition(string input, int minimalCount, out List<string> result)
		{
			return TryParseRepetition(input, minimalCount, (ElementTryParser<string>)TryParseStringElement, out result);
		}

		private static bool TryParseStringElement(Lexer lexer, out string parsedValue, out Token t)
		{
			t = lexer.Scan();
			if ((Token.Type)t == Token.Type.Token)
			{
				parsedValue = lexer.GetStringValue(t);
				if (parsedValue.Length == 0)
				{
					parsedValue = null;
				}
				t = lexer.Scan();
			}
			else
			{
				parsedValue = null;
			}
			return true;
		}

		public static bool TryParseRepetition<T>(string input, int minimalCount, ElementTryParser<T> parser, out List<T> result) where T : class
		{
			Lexer lexer = new Lexer(input);
			result = new List<T>();
			Token token;
			do
			{
				if (!parser(lexer, out var parsedValue, out token))
				{
					return false;
				}
				if (parsedValue != null)
				{
					result.Add(parsedValue);
				}
			}
			while ((Token.Type)token != Token.Type.End);
			if (minimalCount > result.Count)
			{
				return false;
			}
			return true;
		}
	}
	/// <summary>Represents the value of the Content-Disposition header.</summary>
	public class ContentDispositionHeaderValue : ICloneable
	{
		private string dispositionType;

		private List<NameValueHeaderValue> parameters;

		/// <summary>The date at which   the file was created.</summary>
		/// <returns>The file creation date.</returns>
		public DateTimeOffset? CreationDate
		{
			get
			{
				return GetDateValue("creation-date");
			}
			set
			{
				SetDateValue("creation-date", value);
			}
		}

		/// <summary>The disposition type for a content body part.</summary>
		/// <returns>The disposition type.</returns>
		public string DispositionType
		{
			get
			{
				return dispositionType;
			}
			set
			{
				Parser.Token.Check(value);
				dispositionType = value;
			}
		}

		/// <summary>A suggestion for how to construct a filename for   storing the message payload to be used if the entity is   detached and stored in a separate file.</summary>
		/// <returns>A suggested filename.</returns>
		public string FileName
		{
			get
			{
				string text = FindParameter("filename");
				if (text == null)
				{
					return null;
				}
				return DecodeValue(text, extendedNotation: false);
			}
			set
			{
				if (value != null)
				{
					value = EncodeBase64Value(value);
				}
				SetValue("filename", value);
			}
		}

		/// <summary>A suggestion for how to construct filenames for   storing message payloads to be used if the entities are    detached and stored in a separate files.</summary>
		/// <returns>A suggested filename of the form filename*.</returns>
		public string FileNameStar
		{
			get
			{
				string text = FindParameter("filename*");
				if (text == null)
				{
					return null;
				}
				return DecodeValue(text, extendedNotation: true);
			}
			set
			{
				if (value != null)
				{
					value = EncodeRFC5987(value);
				}
				SetValue("filename*", value);
			}
		}

		/// <summary>The date at   which the file was last modified.</summary>
		/// <returns>The file modification date.</returns>
		public DateTimeOffset? ModificationDate
		{
			get
			{
				return GetDateValue("modification-date");
			}
			set
			{
				SetDateValue("modification-date", value);
			}
		}

		/// <summary>The name for a content body part.</summary>
		/// <returns>The name for the content body part.</returns>
		public string Name
		{
			get
			{
				string text = FindParameter("name");
				if (text == null)
				{
					return null;
				}
				return DecodeValue(text, extendedNotation: false);
			}
			set
			{
				if (value != null)
				{
					value = EncodeBase64Value(value);
				}
				SetValue("name", value);
			}
		}

		/// <summary>A set of parameters included the Content-Disposition header.</summary>
		/// <returns>A collection of parameters.</returns>
		public ICollection<NameValueHeaderValue> Parameters => parameters ?? (parameters = new List<NameValueHeaderValue>());

		/// <summary>The date the file was last read.</summary>
		/// <returns>The last read date.</returns>
		public DateTimeOffset? ReadDate
		{
			get
			{
				return GetDateValue("read-date");
			}
			set
			{
				SetDateValue("read-date", value);
			}
		}

		/// <summary>The approximate size, in bytes, of the file.</summary>
		/// <returns>The approximate size, in bytes.</returns>
		public long? Size
		{
			get
			{
				if (Parser.Long.TryParse(FindParameter("size"), out var result))
				{
					return result;
				}
				return null;
			}
			set
			{
				if (!value.HasValue)
				{
					SetValue("size", null);
					return;
				}
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				SetValue("size", value.Value.ToString(CultureInfo.InvariantCulture));
			}
		}

		private ContentDispositionHeaderValue()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> class.</summary>
		/// <param name="dispositionType">A string that contains a <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" />.</param>
		public ContentDispositionHeaderValue(string dispositionType)
		{
			DispositionType = dispositionType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> class.</summary>
		/// <param name="source">A <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" />.</param>
		protected ContentDispositionHeaderValue(ContentDispositionHeaderValue source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			dispositionType = source.dispositionType;
			if (source.parameters == null)
			{
				return;
			}
			foreach (NameValueHeaderValue parameter in source.parameters)
			{
				Parameters.Add(new NameValueHeaderValue(parameter));
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return new ContentDispositionHeaderValue(this);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (obj is ContentDispositionHeaderValue contentDispositionHeaderValue && string.Equals(contentDispositionHeaderValue.dispositionType, dispositionType, StringComparison.OrdinalIgnoreCase))
			{
				return contentDispositionHeaderValue.parameters.SequenceEqual(parameters);
			}
			return false;
		}

		private string FindParameter(string name)
		{
			if (parameters == null)
			{
				return null;
			}
			foreach (NameValueHeaderValue parameter in parameters)
			{
				if (string.Equals(parameter.Name, name, StringComparison.OrdinalIgnoreCase))
				{
					return parameter.Value;
				}
			}
			return null;
		}

		private DateTimeOffset? GetDateValue(string name)
		{
			string text = FindParameter(name);
			if (text == null || text == null)
			{
				return null;
			}
			if (text.Length < 3)
			{
				return null;
			}
			if (text[0] == '"')
			{
				text = text.Substring(1, text.Length - 2);
			}
			if (Lexer.TryGetDateValue(text, out var value))
			{
				return value;
			}
			return null;
		}

		private static string EncodeBase64Value(string value)
		{
			bool flag = value.Length > 1 && value[0] == '"' && value[value.Length - 1] == '"';
			if (flag)
			{
				value = value.Substring(1, value.Length - 2);
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] > '\u007f')
				{
					Encoding uTF = Encoding.UTF8;
					return $"\"=?{uTF.WebName}?B?{Convert.ToBase64String(uTF.GetBytes(value))}?=\"";
				}
			}
			if (flag || !Lexer.IsValidToken(value))
			{
				return "\"" + value + "\"";
			}
			return value;
		}

		private static string EncodeRFC5987(string value)
		{
			Encoding uTF = Encoding.UTF8;
			StringBuilder stringBuilder = new StringBuilder(value.Length + 11);
			stringBuilder.Append(uTF.WebName);
			stringBuilder.Append('\'');
			stringBuilder.Append('\'');
			foreach (char c in value)
			{
				if (c > '\u007f')
				{
					byte[] bytes = uTF.GetBytes(new char[1] { c });
					foreach (byte b in bytes)
					{
						stringBuilder.Append('%');
						stringBuilder.Append(b.ToString("X2"));
					}
				}
				else if (!Lexer.IsValidCharacter(c) || c == '*' || c == '?' || c == '%')
				{
					stringBuilder.Append(Uri.HexEscape(c));
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		private static string DecodeValue(string value, bool extendedNotation)
		{
			if (value.Length < 2)
			{
				return value;
			}
			string[] array;
			Encoding encoding;
			if (value[0] == '"')
			{
				array = value.Split('?');
				if (array.Length != 5 || array[0] != "\"=" || array[4] != "=\"" || (array[2] != "B" && array[2] != "b"))
				{
					return value;
				}
				try
				{
					encoding = Encoding.GetEncoding(array[1]);
					return encoding.GetString(Convert.FromBase64String(array[3]));
				}
				catch
				{
					return value;
				}
			}
			if (!extendedNotation)
			{
				return value;
			}
			array = value.Split('\'');
			if (array.Length != 3)
			{
				return null;
			}
			try
			{
				encoding = Encoding.GetEncoding(array[0]);
			}
			catch
			{
				return null;
			}
			value = array[2];
			if (value.IndexOf('%') < 0)
			{
				return value;
			}
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array2 = null;
			int num = 0;
			int index = 0;
			while (index < value.Length)
			{
				char c = value[index];
				if (c == '%')
				{
					char c2 = c;
					c = Uri.HexUnescape(value, ref index);
					if (c != c2)
					{
						if (array2 == null)
						{
							array2 = new byte[value.Length - index + 1];
						}
						array2[num++] = (byte)c;
						continue;
					}
				}
				else
				{
					index++;
				}
				if (num != 0)
				{
					stringBuilder.Append(encoding.GetChars(array2, 0, num));
					num = 0;
				}
				stringBuilder.Append(c);
			}
			if (num != 0)
			{
				stringBuilder.Append(encoding.GetChars(array2, 0, num));
			}
			return stringBuilder.ToString();
		}

		/// <summary>Serves as a hash function for an  <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return dispositionType.ToLowerInvariant().GetHashCode() ^ HashCodeCalculator.Calculate(parameters);
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents content disposition header value information.</param>
		/// <returns>An <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid content disposition header value information.</exception>
		public static ContentDispositionHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		private void SetDateValue(string key, DateTimeOffset? value)
		{
			SetValue(key, (!value.HasValue) ? null : ("\"" + value.Value.ToString("r", CultureInfo.InvariantCulture) + "\""));
		}

		private void SetValue(string key, string value)
		{
			if (parameters == null)
			{
				parameters = new List<NameValueHeaderValue>();
			}
			parameters.SetValue(key, value);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return dispositionType + CollectionExtensions.ToString(parameters);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out ContentDispositionHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			Token token = lexer.Scan();
			if (token.Kind != Token.Type.Token)
			{
				return false;
			}
			List<NameValueHeaderValue> result = null;
			string stringValue = lexer.GetStringValue(token);
			token = lexer.Scan();
			switch (token.Kind)
			{
			case Token.Type.SeparatorSemicolon:
				if (!NameValueHeaderValue.TryParseParameters(lexer, out result, out token) || (Token.Type)token != Token.Type.End)
				{
					return false;
				}
				break;
			default:
				return false;
			case Token.Type.End:
				break;
			}
			parsedValue = new ContentDispositionHeaderValue
			{
				dispositionType = stringValue,
				parameters = result
			};
			return true;
		}
	}
	/// <summary>Represents the value of the Content-Range header.</summary>
	public class ContentRangeHeaderValue : ICloneable
	{
		private string unit = "bytes";

		/// <summary>Gets the position at which to start sending data.</summary>
		/// <returns>The position, in bytes, at which to start sending data.</returns>
		public long? From { get; private set; }

		/// <summary>Gets whether the Content-Range header has a length specified.</summary>
		/// <returns>
		///   <see langword="true" /> if the Content-Range has a length specified; otherwise, <see langword="false" />.</returns>
		public bool HasLength => Length.HasValue;

		/// <summary>Gets whether the Content-Range has a range specified.</summary>
		/// <returns>
		///   <see langword="true" /> if the Content-Range has a range specified; otherwise, <see langword="false" />.</returns>
		public bool HasRange => From.HasValue;

		/// <summary>Gets the length of the full entity-body.</summary>
		/// <returns>The length of the full entity-body.</returns>
		public long? Length { get; private set; }

		/// <summary>Gets the position at which to stop sending data.</summary>
		/// <returns>The position at which to stop sending data.</returns>
		public long? To { get; private set; }

		/// <summary>The range units used.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains range units.</returns>
		public string Unit
		{
			get
			{
				return unit;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Unit");
				}
				Parser.Token.Check(value);
				unit = value;
			}
		}

		private ContentRangeHeaderValue()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> class.</summary>
		/// <param name="length">The starting or ending point of the range, in bytes.</param>
		public ContentRangeHeaderValue(long length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			Length = length;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> class.</summary>
		/// <param name="from">The position, in bytes, at which to start sending data.</param>
		/// <param name="to">The position, in bytes, at which to stop sending data.</param>
		public ContentRangeHeaderValue(long from, long to)
		{
			if (from < 0 || from > to)
			{
				throw new ArgumentOutOfRangeException("from");
			}
			From = from;
			To = to;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> class.</summary>
		/// <param name="from">The position, in bytes, at which to start sending data.</param>
		/// <param name="to">The position, in bytes, at which to stop sending data.</param>
		/// <param name="length">The starting or ending point of the range, in bytes.</param>
		public ContentRangeHeaderValue(long from, long to, long length)
			: this(from, to)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (to > length)
			{
				throw new ArgumentOutOfRangeException("to");
			}
			Length = length;
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>Determines whether the specified Object is equal to the current <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is ContentRangeHeaderValue { Length: var length } contentRangeHeaderValue))
			{
				return false;
			}
			if (length == Length && contentRangeHeaderValue.From == From && contentRangeHeaderValue.To == To)
			{
				return string.Equals(contentRangeHeaderValue.unit, unit, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return Unit.GetHashCode() ^ Length.GetHashCode() ^ From.GetHashCode() ^ To.GetHashCode() ^ unit.ToLowerInvariant().GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents content range header value information.</param>
		/// <returns>An <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid content range header value information.</exception>
		public static ContentRangeHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out ContentRangeHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			Token token = lexer.Scan();
			if ((Token.Type)token != Token.Type.Token)
			{
				return false;
			}
			ContentRangeHeaderValue contentRangeHeaderValue = new ContentRangeHeaderValue();
			contentRangeHeaderValue.unit = lexer.GetStringValue(token);
			token = lexer.Scan();
			if ((Token.Type)token != Token.Type.Token)
			{
				return false;
			}
			if (!lexer.IsStarStringValue(token))
			{
				if (!lexer.TryGetNumericValue(token, out long value))
				{
					string stringValue = lexer.GetStringValue(token);
					if (stringValue.Length < 3)
					{
						return false;
					}
					string[] array = stringValue.Split('-');
					if (array.Length != 2)
					{
						return false;
					}
					if (!long.TryParse(array[0], NumberStyles.None, CultureInfo.InvariantCulture, out value))
					{
						return false;
					}
					contentRangeHeaderValue.From = value;
					if (!long.TryParse(array[1], NumberStyles.None, CultureInfo.InvariantCulture, out value))
					{
						return false;
					}
					contentRangeHeaderValue.To = value;
				}
				else
				{
					contentRangeHeaderValue.From = value;
					token = lexer.Scan(recognizeDash: true);
					if ((Token.Type)token != Token.Type.SeparatorDash)
					{
						return false;
					}
					token = lexer.Scan();
					if (!lexer.TryGetNumericValue(token, out value))
					{
						return false;
					}
					contentRangeHeaderValue.To = value;
				}
			}
			token = lexer.Scan();
			if ((Token.Type)token != Token.Type.SeparatorSlash)
			{
				return false;
			}
			token = lexer.Scan();
			if (!lexer.IsStarStringValue(token))
			{
				if (!lexer.TryGetNumericValue(token, out long value2))
				{
					return false;
				}
				contentRangeHeaderValue.Length = value2;
			}
			token = lexer.Scan();
			if ((Token.Type)token != Token.Type.End)
			{
				return false;
			}
			parsedValue = contentRangeHeaderValue;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(unit);
			stringBuilder.Append(" ");
			if (!From.HasValue)
			{
				stringBuilder.Append("*");
			}
			else
			{
				stringBuilder.Append(From.Value.ToString(CultureInfo.InvariantCulture));
				stringBuilder.Append("-");
				stringBuilder.Append(To.Value.ToString(CultureInfo.InvariantCulture));
			}
			stringBuilder.Append("/");
			stringBuilder.Append((!Length.HasValue) ? "*" : Length.Value.ToString(CultureInfo.InvariantCulture));
			return stringBuilder.ToString();
		}
	}
	/// <summary>Represents an entity-tag header value.</summary>
	public class EntityTagHeaderValue : ICloneable
	{
		private static readonly EntityTagHeaderValue any = new EntityTagHeaderValue
		{
			Tag = "*"
		};

		/// <summary>Gets the entity-tag header value.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" />.</returns>
		public static EntityTagHeaderValue Any => any;

		/// <summary>Gets whether the entity-tag is prefaced by a weakness indicator.</summary>
		/// <returns>
		///   <see langword="true" /> if the entity-tag is prefaced by a weakness indicator; otherwise, <see langword="false" />.</returns>
		public bool IsWeak { get; internal set; }

		/// <summary>Gets the opaque quoted string.</summary>
		/// <returns>An opaque quoted string.</returns>
		public string Tag { get; internal set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> class.</summary>
		/// <param name="tag">A string that contains an <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" />.</param>
		public EntityTagHeaderValue(string tag)
		{
			Parser.Token.CheckQuotedString(tag);
			Tag = tag;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> class.</summary>
		/// <param name="tag">A string that contains an  <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" />.</param>
		/// <param name="isWeak">A value that indicates if this entity-tag header is a weak validator. If the entity-tag header is weak validator, then <paramref name="isWeak" /> should be set to <see langword="true" />. If the entity-tag header is a strong validator, then <paramref name="isWeak" /> should be set to <see langword="false" />.</param>
		public EntityTagHeaderValue(string tag, bool isWeak)
			: this(tag)
		{
			IsWeak = isWeak;
		}

		internal EntityTagHeaderValue()
		{
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (obj is EntityTagHeaderValue entityTagHeaderValue && entityTagHeaderValue.Tag == Tag)
			{
				return string.Equals(entityTagHeaderValue.Tag, Tag, StringComparison.Ordinal);
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return IsWeak.GetHashCode() ^ Tag.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents entity tag header value information.</param>
		/// <returns>An <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid entity tag header value information.</exception>
		public static EntityTagHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out EntityTagHeaderValue parsedValue)
		{
			if (TryParseElement(new Lexer(input), out parsedValue, out var t) && (Token.Type)t == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		private static bool TryParseElement(Lexer lexer, out EntityTagHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan();
			bool isWeak = false;
			if ((Token.Type)t == Token.Type.Token)
			{
				string stringValue = lexer.GetStringValue(t);
				if (stringValue == "*")
				{
					parsedValue = any;
					t = lexer.Scan();
					return true;
				}
				if (stringValue != "W" || lexer.PeekChar() != 47)
				{
					return false;
				}
				isWeak = true;
				lexer.EatChar();
				t = lexer.Scan();
			}
			if ((Token.Type)t != Token.Type.QuotedString)
			{
				return false;
			}
			parsedValue = new EntityTagHeaderValue();
			parsedValue.Tag = lexer.GetStringValue(t);
			parsedValue.IsWeak = isWeak;
			t = lexer.Scan();
			return true;
		}

		internal static bool TryParse(string input, int minimalCount, out List<EntityTagHeaderValue> result)
		{
			return CollectionParser.TryParse(input, minimalCount, (ElementTryParser<EntityTagHeaderValue>)TryParseElement, out result);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			if (!IsWeak)
			{
				return Tag;
			}
			return "W/" + Tag;
		}
	}
	internal static class HashCodeCalculator
	{
		public static int Calculate<T>(ICollection<T> list)
		{
			if (list == null)
			{
				return 0;
			}
			int num = 17;
			foreach (T item in list)
			{
				num = num * 29 + item.GetHashCode();
			}
			return num;
		}
	}
	internal delegate bool TryParseDelegate<T>(string value, out T result);
	internal delegate bool TryParseListDelegate<T>(string value, int minimalCount, out List<T> result);
	internal abstract class HeaderInfo
	{
		private class HeaderTypeInfo<T, U> : HeaderInfo where U : class
		{
			private readonly TryParseDelegate<T> parser;

			public HeaderTypeInfo(string name, TryParseDelegate<T> parser, HttpHeaderKind headerKind)
				: base(name, headerKind)
			{
				this.parser = parser;
			}

			public override void AddToCollection(object collection, object value)
			{
				HttpHeaderValueCollection<U> httpHeaderValueCollection = (HttpHeaderValueCollection<U>)collection;
				if (value is List<U> values)
				{
					httpHeaderValueCollection.AddRange(values);
				}
				else
				{
					httpHeaderValueCollection.Add((U)value);
				}
			}

			protected override object CreateCollection(HttpHeaders headers, HeaderInfo headerInfo)
			{
				return new HttpHeaderValueCollection<U>(headers, headerInfo);
			}

			public override List<string> ToStringCollection(object collection)
			{
				if (collection == null)
				{
					return null;
				}
				HttpHeaderValueCollection<U> httpHeaderValueCollection = (HttpHeaderValueCollection<U>)collection;
				if (httpHeaderValueCollection.Count == 0)
				{
					if (httpHeaderValueCollection.InvalidValues == null)
					{
						return null;
					}
					return new List<string>(httpHeaderValueCollection.InvalidValues);
				}
				List<string> list = new List<string>();
				foreach (U item in httpHeaderValueCollection)
				{
					list.Add(item.ToString());
				}
				if (httpHeaderValueCollection.InvalidValues != null)
				{
					list.AddRange(httpHeaderValueCollection.InvalidValues);
				}
				return list;
			}

			public override bool TryParse(string value, out object result)
			{
				T result3;
				bool result2 = parser(value, out result3);
				result = result3;
				return result2;
			}
		}

		private class CollectionHeaderTypeInfo<T, U> : HeaderTypeInfo<T, U> where U : class
		{
			private readonly int minimalCount;

			private readonly string separator;

			private readonly TryParseListDelegate<T> parser;

			public override string Separator => separator;

			public CollectionHeaderTypeInfo(string name, TryParseListDelegate<T> parser, HttpHeaderKind headerKind, int minimalCount, string separator)
				: base(name, (TryParseDelegate<T>)null, headerKind)
			{
				this.parser = parser;
				this.minimalCount = minimalCount;
				AllowsMany = true;
				this.separator = separator;
			}

			public override bool TryParse(string value, out object result)
			{
				if (!parser(value, minimalCount, out var result2))
				{
					result = null;
					return false;
				}
				result = result2;
				return true;
			}
		}

		public bool AllowsMany;

		public readonly HttpHeaderKind HeaderKind;

		public readonly string Name;

		public Func<object, string> CustomToString { get; private set; }

		public virtual string Separator
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		protected HeaderInfo(string name, HttpHeaderKind headerKind)
		{
			Name = name;
			HeaderKind = headerKind;
		}

		public static HeaderInfo CreateSingle<T>(string name, TryParseDelegate<T> parser, HttpHeaderKind headerKind, Func<object, string> toString = null)
		{
			return new HeaderTypeInfo<T, object>(name, parser, headerKind)
			{
				CustomToString = toString
			};
		}

		public static HeaderInfo CreateMulti<T>(string name, TryParseListDelegate<T> elementParser, HttpHeaderKind headerKind, int minimalCount = 1, string separator = ", ") where T : class
		{
			return new CollectionHeaderTypeInfo<T, T>(name, elementParser, headerKind, minimalCount, separator);
		}

		public object CreateCollection(HttpHeaders headers)
		{
			return CreateCollection(headers, this);
		}

		public abstract void AddToCollection(object collection, object value);

		protected abstract object CreateCollection(HttpHeaders headers, HeaderInfo headerInfo);

		public abstract List<string> ToStringCollection(object collection);

		public abstract bool TryParse(string value, out object result);
	}
	/// <summary>Represents the collection of Content Headers as defined in RFC 2616.</summary>
	public sealed class HttpContentHeaders : HttpHeaders
	{
		private readonly HttpContent content;

		/// <summary>Gets the value of the <see langword="Allow" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Allow" /> header on an HTTP response.</returns>
		public ICollection<string> Allow => GetValues<string>("Allow");

		/// <summary>Gets the value of the <see langword="Content-Encoding" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-Encoding" /> content header on an HTTP response.</returns>
		public ICollection<string> ContentEncoding => GetValues<string>("Content-Encoding");

		/// <summary>Gets the value of the <see langword="Content-Disposition" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-Disposition" /> content header on an HTTP response.</returns>
		public ContentDispositionHeaderValue ContentDisposition
		{
			get
			{
				return GetValue<ContentDispositionHeaderValue>("Content-Disposition");
			}
			set
			{
				AddOrRemove("Content-Disposition", value);
			}
		}

		/// <summary>Gets the value of the <see langword="Content-Language" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-Language" /> content header on an HTTP response.</returns>
		public ICollection<string> ContentLanguage => GetValues<string>("Content-Language");

		/// <summary>Gets or sets the value of the <see langword="Content-Length" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-Length" /> content header on an HTTP response.</returns>
		public long? ContentLength
		{
			get
			{
				long? value = GetValue<long?>("Content-Length");
				if (value.HasValue)
				{
					return value;
				}
				value = content.LoadedBufferLength;
				if (value.HasValue)
				{
					return value;
				}
				if (content.TryComputeLength(out var length))
				{
					SetValue("Content-Length", length);
					return length;
				}
				return null;
			}
			set
			{
				AddOrRemove("Content-Length", value);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Content-Location" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-Location" /> content header on an HTTP response.</returns>
		public Uri ContentLocation
		{
			get
			{
				return GetValue<Uri>("Content-Location");
			}
			set
			{
				AddOrRemove("Content-Location", value);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Content-MD5" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-MD5" /> content header on an HTTP response.</returns>
		public byte[] ContentMD5
		{
			get
			{
				return GetValue<byte[]>("Content-MD5");
			}
			set
			{
				AddOrRemove("Content-MD5", value, Parser.MD5.ToString);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Content-Range" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-Range" /> content header on an HTTP response.</returns>
		public ContentRangeHeaderValue ContentRange
		{
			get
			{
				return GetValue<ContentRangeHeaderValue>("Content-Range");
			}
			set
			{
				AddOrRemove("Content-Range", value);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Content-Type" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Content-Type" /> content header on an HTTP response.</returns>
		public MediaTypeHeaderValue ContentType
		{
			get
			{
				return GetValue<MediaTypeHeaderValue>("Content-Type");
			}
			set
			{
				AddOrRemove("Content-Type", value);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Expires" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Expires" /> content header on an HTTP response.</returns>
		public DateTimeOffset? Expires
		{
			get
			{
				return GetValue<DateTimeOffset?>("Expires");
			}
			set
			{
				AddOrRemove("Expires", value, Parser.DateTime.ToString);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Last-Modified" /> content header on an HTTP response.</summary>
		/// <returns>The value of the <see langword="Last-Modified" /> content header on an HTTP response.</returns>
		public DateTimeOffset? LastModified
		{
			get
			{
				return GetValue<DateTimeOffset?>("Last-Modified");
			}
			set
			{
				AddOrRemove("Last-Modified", value, Parser.DateTime.ToString);
			}
		}

		internal HttpContentHeaders(HttpContent content)
			: base(HttpHeaderKind.Content)
		{
			this.content = content;
		}

		internal HttpContentHeaders()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	[Flags]
	internal enum HttpHeaderKind
	{
		None = 0,
		Request = 1,
		Response = 2,
		Content = 4
	}
	/// <summary>Represents a collection of header values.</summary>
	/// <typeparam name="T">The header collection type.</typeparam>
	public sealed class HttpHeaderValueCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable where T : class
	{
		private readonly List<T> list;

		private readonly HttpHeaders headers;

		private readonly HeaderInfo headerInfo;

		private List<string> invalidValues;

		/// <summary>Gets the number of headers in the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		/// <returns>The number of headers in a collection</returns>
		public int Count => list.Count;

		internal List<string> InvalidValues => invalidValues;

		/// <summary>Gets a value indicating whether the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> instance is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> instance is read-only; otherwise, <see langword="false" />.</returns>
		public bool IsReadOnly => false;

		internal HttpHeaderValueCollection(HttpHeaders headers, HeaderInfo headerInfo)
		{
			list = new List<T>();
			this.headers = headers;
			this.headerInfo = headerInfo;
		}

		/// <summary>Adds an entry to the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		/// <param name="item">The item to add to the header collection.</param>
		public void Add(T item)
		{
			list.Add(item);
		}

		internal void AddRange(List<T> values)
		{
			list.AddRange(values);
		}

		internal void AddInvalidValue(string invalidValue)
		{
			if (invalidValues == null)
			{
				invalidValues = new List<string>();
			}
			invalidValues.Add(invalidValue);
		}

		/// <summary>Removes all entries from the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		public void Clear()
		{
			list.Clear();
			invalidValues = null;
		}

		/// <summary>Determines if the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> contains an item.</summary>
		/// <param name="item">The item to find to the header collection.</param>
		/// <returns>
		///   <see langword="true" /> if the entry is contained in the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> instance; otherwise, <see langword="false" /></returns>
		public bool Contains(T item)
		{
			return list.Contains(item);
		}

		/// <summary>Copies the entire <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		public void CopyTo(T[] array, int arrayIndex)
		{
			list.CopyTo(array, arrayIndex);
		}

		/// <summary>Parses and adds an entry to the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		/// <param name="input">The entry to add.</param>
		public void ParseAdd(string input)
		{
			headers.AddValue(input, headerInfo, ignoreInvalid: false);
		}

		/// <summary>Removes the specified item from the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		/// <param name="item">The item to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="item" /> was removed from the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> instance; otherwise, <see langword="false" /></returns>
		public bool Remove(T item)
		{
			return list.Remove(item);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> object. object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			string text = string.Join(headerInfo.Separator, list);
			if (invalidValues != null)
			{
				text += string.Join(headerInfo.Separator, invalidValues);
			}
			return text;
		}

		/// <summary>Determines whether the input could be parsed and added to the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		/// <param name="input">The entry to validate.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="input" /> could be parsed and added to the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> instance; otherwise, <see langword="false" /></returns>
		public bool TryParseAdd(string input)
		{
			return headers.AddValue(input, headerInfo, ignoreInvalid: true);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> instance.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" /> instance.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		internal T Find(Predicate<T> predicate)
		{
			return list.Find(predicate);
		}

		internal void Remove(Predicate<T> predicate)
		{
			T val = Find(predicate);
			if (val != null)
			{
				Remove(val);
			}
		}

		internal HttpHeaderValueCollection()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>A collection of headers and their values as defined in RFC 2616.</summary>
	public abstract class HttpHeaders : IEnumerable<KeyValuePair<string, IEnumerable<string>>>, IEnumerable
	{
		private class HeaderBucket
		{
			public object Parsed;

			private List<string> values;

			public readonly Func<object, string> CustomToString;

			public bool HasStringValues
			{
				get
				{
					if (values != null)
					{
						return values.Count > 0;
					}
					return false;
				}
			}

			public List<string> Values
			{
				get
				{
					return values ?? (values = new List<string>());
				}
				set
				{
					values = value;
				}
			}

			public HeaderBucket(object parsed, Func<object, string> converter)
			{
				Parsed = parsed;
				CustomToString = converter;
			}

			public string ParsedToString()
			{
				if (Parsed == null)
				{
					return null;
				}
				if (CustomToString != null)
				{
					return CustomToString(Parsed);
				}
				return Parsed.ToString();
			}
		}

		private static readonly Dictionary<string, HeaderInfo> known_headers;

		private readonly Dictionary<string, HeaderBucket> headers;

		private readonly HttpHeaderKind HeaderKind;

		internal bool? connectionclose;

		internal bool? transferEncodingChunked;

		static HttpHeaders()
		{
			HeaderInfo[] obj = new HeaderInfo[48]
			{
				HeaderInfo.CreateMulti<MediaTypeWithQualityHeaderValue>("Accept", MediaTypeWithQualityHeaderValue.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateMulti<StringWithQualityHeaderValue>("Accept-Charset", StringWithQualityHeaderValue.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateMulti<StringWithQualityHeaderValue>("Accept-Encoding", StringWithQualityHeaderValue.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateMulti<StringWithQualityHeaderValue>("Accept-Language", StringWithQualityHeaderValue.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateMulti<string>("Accept-Ranges", CollectionParser.TryParse, HttpHeaderKind.Response),
				HeaderInfo.CreateSingle<TimeSpan>("Age", Parser.TimeSpanSeconds.TryParse, HttpHeaderKind.Response),
				HeaderInfo.CreateMulti<string>("Allow", CollectionParser.TryParse, HttpHeaderKind.Content, 0),
				HeaderInfo.CreateSingle<AuthenticationHeaderValue>("Authorization", AuthenticationHeaderValue.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateSingle<CacheControlHeaderValue>("Cache-Control", CacheControlHeaderValue.TryParse, HttpHeaderKind.Request | HttpHeaderKind.Response),
				HeaderInfo.CreateMulti<string>("Connection", CollectionParser.TryParse, HttpHeaderKind.Request | HttpHeaderKind.Response),
				HeaderInfo.CreateSingle<ContentDispositionHeaderValue>("Content-Disposition", ContentDispositionHeaderValue.TryParse, HttpHeaderKind.Content),
				HeaderInfo.CreateMulti<string>("Content-Encoding", CollectionParser.TryParse, HttpHeaderKind.Content),
				HeaderInfo.CreateMulti<string>("Content-Language", CollectionParser.TryParse, HttpHeaderKind.Content),
				HeaderInfo.CreateSingle<long>("Content-Length", Parser.Long.TryParse, HttpHeaderKind.Content),
				HeaderInfo.CreateSingle<Uri>("Content-Location", Parser.Uri.TryParse, HttpHeaderKind.Content),
				HeaderInfo.CreateSingle<byte[]>("Content-MD5", Parser.MD5.TryParse, HttpHeaderKind.Content),
				HeaderInfo.CreateSingle<ContentRangeHeaderValue>("Content-Range", ContentRangeHeaderValue.TryParse, HttpHeaderKind.Content),
				HeaderInfo.CreateSingle<MediaTypeHeaderValue>("Content-Type", MediaTypeHeaderValue.TryParse, HttpHeaderKind.Content),
				HeaderInfo.CreateSingle<DateTimeOffset>("Date", Parser.DateTime.TryParse, HttpHeaderKind.Request | HttpHeaderKind.Response, Parser.DateTime.ToString),
				HeaderInfo.CreateSingle<EntityTagHeaderValue>("ETag", EntityTagHeaderValue.TryParse, HttpHeaderKind.Response),
				HeaderInfo.CreateMulti<NameValueWithParametersHeaderValue>("Expect", NameValueWithParametersHeaderValue.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateSingle<DateTimeOffset>("Expires", Parser.DateTime.TryParse, HttpHeaderKind.Content, Parser.DateTime.ToString),
				HeaderInfo.CreateSingle<string>("From", Parser.EmailAddress.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateSingle<string>("Host", Parser.Host.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateMulti<EntityTagHeaderValue>("If-Match", EntityTagHeaderValue.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateSingle<DateTimeOffset>("If-Modified-Since", Parser.DateTime.TryParse, HttpHeaderKind.Request, Parser.DateTime.ToString),
				HeaderInfo.CreateMulti<EntityTagHeaderValue>("If-None-Match", EntityTagHeaderValue.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateSingle<RangeConditionHeaderValue>("If-Range", RangeConditionHeaderValue.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateSingle<DateTimeOffset>("If-Unmodified-Since", Parser.DateTime.TryParse, HttpHeaderKind.Request, Parser.DateTime.ToString),
				HeaderInfo.CreateSingle<DateTimeOffset>("Last-Modified", Parser.DateTime.TryParse, HttpHeaderKind.Content, Parser.DateTime.ToString),
				HeaderInfo.CreateSingle<Uri>("Location", Parser.Uri.TryParse, HttpHeaderKind.Response),
				HeaderInfo.CreateSingle<int>("Max-Forwards", Parser.Int.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateMulti<NameValueHeaderValue>("Pragma", NameValueHeaderValue.TryParsePragma, HttpHeaderKind.Request | HttpHeaderKind.Response),
				HeaderInfo.CreateMulti<AuthenticationHeaderValue>("Proxy-Authenticate", AuthenticationHeaderValue.TryParse, HttpHeaderKind.Response),
				HeaderInfo.CreateSingle<AuthenticationHeaderValue>("Proxy-Authorization", AuthenticationHeaderValue.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateSingle<RangeHeaderValue>("Range", RangeHeaderValue.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateSingle<Uri>("Referer", Parser.Uri.TryParse, HttpHeaderKind.Request),
				HeaderInfo.CreateSingle<RetryConditionHeaderValue>("Retry-After", RetryConditionHeaderValue.TryParse, HttpHeaderKind.Response),
				HeaderInfo.CreateMulti<ProductInfoHeaderValue>("Server", ProductInfoHeaderValue.TryParse, HttpHeaderKind.Response, 1, " "),
				HeaderInfo.CreateMulti<TransferCodingWithQualityHeaderValue>("TE", TransferCodingWithQualityHeaderValue.TryParse, HttpHeaderKind.Request, 0),
				HeaderInfo.CreateMulti<string>("Trailer", CollectionParser.TryParse, HttpHeaderKind.Request | HttpHeaderKind.Response),
				HeaderInfo.CreateMulti<TransferCodingHeaderValue>("Transfer-Encoding", TransferCodingHeaderValue.TryParse, HttpHeaderKind.Request | HttpHeaderKind.Response),
				HeaderInfo.CreateMulti<ProductHeaderValue>("Upgrade", ProductHeaderValue.TryParse, HttpHeaderKind.Request | HttpHeaderKind.Response),
				HeaderInfo.CreateMulti<ProductInfoHeaderValue>("User-Agent", ProductInfoHeaderValue.TryParse, HttpHeaderKind.Request, 1, " "),
				HeaderInfo.CreateMulti<string>("Vary", CollectionParser.TryParse, HttpHeaderKind.Response),
				HeaderInfo.CreateMulti<ViaHeaderValue>("Via", ViaHeaderValue.TryParse, HttpHeaderKind.Request | HttpHeaderKind.Response),
				HeaderInfo.CreateMulti<WarningHeaderValue>("Warning", WarningHeaderValue.TryParse, HttpHeaderKind.Request | HttpHeaderKind.Response),
				HeaderInfo.CreateMulti<AuthenticationHeaderValue>("WWW-Authenticate", AuthenticationHeaderValue.TryParse, HttpHeaderKind.Response)
			};
			known_headers = new Dictionary<string, HeaderInfo>(StringComparer.OrdinalIgnoreCase);
			HeaderInfo[] array = obj;
			foreach (HeaderInfo headerInfo in array)
			{
				known_headers.Add(headerInfo.Name, headerInfo);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.HttpHeaders" /> class.</summary>
		protected HttpHeaders()
		{
			headers = new Dictionary<string, HeaderBucket>(StringComparer.OrdinalIgnoreCase);
		}

		internal HttpHeaders(HttpHeaderKind headerKind)
			: this()
		{
			HeaderKind = headerKind;
		}

		/// <summary>Adds the specified header and its value into the <see cref="T:System.Net.Http.Headers.HttpHeaders" /> collection.</summary>
		/// <param name="name">The header to add to the collection.</param>
		/// <param name="value">The content of the header.</param>
		public void Add(string name, string value)
		{
			Add(name, new string[1] { value });
		}

		/// <summary>Adds the specified header and its values into the <see cref="T:System.Net.Http.Headers.HttpHeaders" /> collection.</summary>
		/// <param name="name">The header to add to the collection.</param>
		/// <param name="values">A list of header values to add to the collection.</param>
		public void Add(string name, IEnumerable<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			AddInternal(name, values, CheckName(name), ignoreInvalid: false);
		}

		internal bool AddValue(string value, HeaderInfo headerInfo, bool ignoreInvalid)
		{
			return AddInternal(headerInfo.Name, new string[1] { value }, headerInfo, ignoreInvalid);
		}

		private bool AddInternal(string name, IEnumerable<string> values, HeaderInfo headerInfo, bool ignoreInvalid)
		{
			headers.TryGetValue(name, out var value);
			bool result = true;
			foreach (string value2 in values)
			{
				bool flag = value == null;
				if (headerInfo != null)
				{
					if (!headerInfo.TryParse(value2, out var result2))
					{
						if (ignoreInvalid)
						{
							result = false;
							continue;
						}
						throw new FormatException("Could not parse value for header '" + name + "'");
					}
					if (headerInfo.AllowsMany)
					{
						if (value == null)
						{
							value = new HeaderBucket(headerInfo.CreateCollection(this), headerInfo.CustomToString);
						}
						headerInfo.AddToCollection(value.Parsed, result2);
					}
					else
					{
						if (value != null)
						{
							throw new FormatException();
						}
						value = new HeaderBucket(result2, headerInfo.CustomToString);
					}
				}
				else
				{
					if (value == null)
					{
						value = new HeaderBucket(null, null);
					}
					value.Values.Add(value2 ?? string.Empty);
				}
				if (flag)
				{
					headers.Add(name, value);
				}
			}
			return result;
		}

		/// <summary>Returns a value that indicates whether the specified header and its value were added to the <see cref="T:System.Net.Http.Headers.HttpHeaders" /> collection without validating the provided information.</summary>
		/// <param name="name">The header to add to the collection.</param>
		/// <param name="value">The content of the header.</param>
		/// <returns>
		///   <see langword="true" /> if the specified header <paramref name="name" /> and <paramref name="value" /> could be added to the collection; otherwise <see langword="false" />.</returns>
		public bool TryAddWithoutValidation(string name, string value)
		{
			return TryAddWithoutValidation(name, new string[1] { value });
		}

		/// <summary>Returns a value that indicates whether the specified header and its values were added to the <see cref="T:System.Net.Http.Headers.HttpHeaders" /> collection without validating the provided information.</summary>
		/// <param name="name">The header to add to the collection.</param>
		/// <param name="values">The values of the header.</param>
		/// <returns>
		///   <see langword="true" /> if the specified header <paramref name="name" /> and <paramref name="values" /> could be added to the collection; otherwise <see langword="false" />.</returns>
		public bool TryAddWithoutValidation(string name, IEnumerable<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (!TryCheckName(name, out var _))
			{
				return false;
			}
			AddInternal(name, values, null, ignoreInvalid: true);
			return true;
		}

		private HeaderInfo CheckName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name");
			}
			Parser.Token.Check(name);
			if (known_headers.TryGetValue(name, out var value) && (value.HeaderKind & HeaderKind) == 0)
			{
				if (HeaderKind != HttpHeaderKind.None && ((HeaderKind | value.HeaderKind) & HttpHeaderKind.Content) != HttpHeaderKind.None)
				{
					throw new InvalidOperationException(name);
				}
				return null;
			}
			return value;
		}

		private bool TryCheckName(string name, out HeaderInfo headerInfo)
		{
			if (!Parser.Token.TryCheck(name))
			{
				headerInfo = null;
				return false;
			}
			if (known_headers.TryGetValue(name, out headerInfo) && (headerInfo.HeaderKind & HeaderKind) == 0 && HeaderKind != HttpHeaderKind.None && ((HeaderKind | headerInfo.HeaderKind) & HttpHeaderKind.Content) != HttpHeaderKind.None)
			{
				return false;
			}
			return true;
		}

		/// <summary>Removes all headers from the <see cref="T:System.Net.Http.Headers.HttpHeaders" /> collection.</summary>
		public void Clear()
		{
			connectionclose = null;
			transferEncodingChunked = null;
			headers.Clear();
		}

		/// <summary>Returns if  a specific header exists in the <see cref="T:System.Net.Http.Headers.HttpHeaders" /> collection.</summary>
		/// <param name="name">The specific header.</param>
		/// <returns>
		///   <see langword="true" /> is the specified header exists in the collection; otherwise <see langword="false" />.</returns>
		public bool Contains(string name)
		{
			CheckName(name);
			return headers.ContainsKey(name);
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Net.Http.Headers.HttpHeaders" /> instance.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Net.Http.Headers.HttpHeaders" />.</returns>
		public IEnumerator<KeyValuePair<string, IEnumerable<string>>> GetEnumerator()
		{
			foreach (KeyValuePair<string, HeaderBucket> header in headers)
			{
				HeaderBucket bucket = headers[header.Key];
				known_headers.TryGetValue(header.Key, out var value);
				List<string> allHeaderValues = GetAllHeaderValues(bucket, value);
				if (allHeaderValues != null)
				{
					yield return new KeyValuePair<string, IEnumerable<string>>(header.Key, allHeaderValues);
				}
			}
		}

		/// <summary>Gets an enumerator that can iterate through a <see cref="T:System.Net.Http.Headers.HttpHeaders" />.</summary>
		/// <returns>An instance of an implementation of an <see cref="T:System.Collections.IEnumerator" /> that can iterate through a <see cref="T:System.Net.Http.Headers.HttpHeaders" />.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>Returns all header values for a specified header stored in the <see cref="T:System.Net.Http.Headers.HttpHeaders" /> collection.</summary>
		/// <param name="name">The specified header to return values for.</param>
		/// <returns>An array of header strings.</returns>
		/// <exception cref="T:System.InvalidOperationException">The header cannot be found.</exception>
		public IEnumerable<string> GetValues(string name)
		{
			CheckName(name);
			if (!TryGetValues(name, out var values))
			{
				throw new InvalidOperationException();
			}
			return values;
		}

		/// <summary>Removes the specified header from the <see cref="T:System.Net.Http.Headers.HttpHeaders" /> collection.</summary>
		/// <param name="name">The name of the header to remove from the collection.</param>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		public bool Remove(string name)
		{
			CheckName(name);
			return headers.Remove(name);
		}

		/// <summary>Return if a specified header and specified values are stored in the <see cref="T:System.Net.Http.Headers.HttpHeaders" /> collection.</summary>
		/// <param name="name">The specified header.</param>
		/// <param name="values">The specified header values.</param>
		/// <returns>
		///   <see langword="true" /> is the specified header <paramref name="name" /> and <see langword="values" /> are stored in the collection; otherwise <see langword="false" />.</returns>
		public bool TryGetValues(string name, out IEnumerable<string> values)
		{
			if (!TryCheckName(name, out var headerInfo))
			{
				values = null;
				return false;
			}
			if (!headers.TryGetValue(name, out var value))
			{
				values = null;
				return false;
			}
			values = GetAllHeaderValues(value, headerInfo);
			return true;
		}

		internal static string GetSingleHeaderString(string key, IEnumerable<string> values)
		{
			string text = ",";
			if (known_headers.TryGetValue(key, out var value) && value.AllowsMany)
			{
				text = value.Separator;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (string value2 in values)
			{
				if (!flag)
				{
					stringBuilder.Append(text);
					if (text != " ")
					{
						stringBuilder.Append(" ");
					}
				}
				stringBuilder.Append(value2);
				flag = false;
			}
			if (flag)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.HttpHeaders" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (IEnumerator<KeyValuePair<string, IEnumerable<string>>> enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, IEnumerable<string>> current = enumerator.Current;
					stringBuilder.Append(current.Key);
					stringBuilder.Append(": ");
					stringBuilder.Append(GetSingleHeaderString(current.Key, current.Value));
					stringBuilder.Append("\r\n");
				}
			}
			return stringBuilder.ToString();
		}

		internal void AddOrRemove(string name, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				Remove(name);
			}
			else
			{
				SetValue(name, value);
			}
		}

		internal void AddOrRemove<T>(string name, T value, Func<object, string> converter = null) where T : class
		{
			if (value == null)
			{
				Remove(name);
			}
			else
			{
				SetValue(name, value, converter);
			}
		}

		internal void AddOrRemove<T>(string name, T? value) where T : struct
		{
			AddOrRemove(name, value, null);
		}

		internal void AddOrRemove<T>(string name, T? value, Func<object, string> converter) where T : struct
		{
			if (!value.HasValue)
			{
				Remove(name);
			}
			else
			{
				SetValue(name, value, converter);
			}
		}

		private List<string> GetAllHeaderValues(HeaderBucket bucket, HeaderInfo headerInfo)
		{
			List<string> list = null;
			if (headerInfo != null && headerInfo.AllowsMany)
			{
				list = headerInfo.ToStringCollection(bucket.Parsed);
			}
			else if (bucket.Parsed != null)
			{
				string text = bucket.ParsedToString();
				if (!string.IsNullOrEmpty(text))
				{
					list = new List<string>();
					list.Add(text);
				}
			}
			if (bucket.HasStringValues)
			{
				if (list == null)
				{
					list = new List<string>();
				}
				list.AddRange(bucket.Values);
			}
			return list;
		}

		internal static HttpHeaderKind GetKnownHeaderKind(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name");
			}
			if (known_headers.TryGetValue(name, out var value))
			{
				return value.HeaderKind;
			}
			return HttpHeaderKind.None;
		}

		internal T GetValue<T>(string name)
		{
			if (!headers.TryGetValue(name, out var value))
			{
				return default(T);
			}
			if (value.HasStringValues)
			{
				if (!known_headers[name].TryParse(value.Values[0], out var result))
				{
					if (!(typeof(T) == typeof(string)))
					{
						return default(T);
					}
					return (T)(object)value.Values[0];
				}
				value.Parsed = result;
				value.Values = null;
			}
			return (T)value.Parsed;
		}

		internal HttpHeaderValueCollection<T> GetValues<T>(string name) where T : class
		{
			if (!headers.TryGetValue(name, out var value))
			{
				HeaderInfo headerInfo = known_headers[name];
				value = new HeaderBucket(new HttpHeaderValueCollection<T>(this, headerInfo), headerInfo.CustomToString);
				headers.Add(name, value);
			}
			HttpHeaderValueCollection<T> httpHeaderValueCollection = (HttpHeaderValueCollection<T>)value.Parsed;
			if (value.HasStringValues)
			{
				HeaderInfo headerInfo2 = known_headers[name];
				if (httpHeaderValueCollection == null)
				{
					httpHeaderValueCollection = (HttpHeaderValueCollection<T>)(value.Parsed = new HttpHeaderValueCollection<T>(this, headerInfo2));
				}
				for (int i = 0; i < value.Values.Count; i++)
				{
					string text = value.Values[i];
					if (!headerInfo2.TryParse(text, out var result))
					{
						httpHeaderValueCollection.AddInvalidValue(text);
					}
					else
					{
						headerInfo2.AddToCollection(httpHeaderValueCollection, result);
					}
				}
				value.Values.Clear();
			}
			return httpHeaderValueCollection;
		}

		internal void SetValue<T>(string name, T value, Func<object, string> toStringConverter = null)
		{
			headers[name] = new HeaderBucket(value, toStringConverter);
		}
	}
	/// <summary>Represents the collection of Request Headers as defined in RFC 2616.</summary>
	public sealed class HttpRequestHeaders : HttpHeaders
	{
		private bool? expectContinue;

		/// <summary>Gets the value of the <see langword="Accept" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Accept" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> Accept => GetValues<MediaTypeWithQualityHeaderValue>("Accept");

		/// <summary>Gets the value of the <see langword="Accept-Charset" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Accept-Charset" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptCharset => GetValues<StringWithQualityHeaderValue>("Accept-Charset");

		/// <summary>Gets the value of the <see langword="Accept-Encoding" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Accept-Encoding" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptEncoding => GetValues<StringWithQualityHeaderValue>("Accept-Encoding");

		/// <summary>Gets the value of the <see langword="Accept-Language" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Accept-Language" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptLanguage => GetValues<StringWithQualityHeaderValue>("Accept-Language");

		/// <summary>Gets or sets the value of the <see langword="Authorization" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Authorization" /> header for an HTTP request.</returns>
		public AuthenticationHeaderValue Authorization
		{
			get
			{
				return GetValue<AuthenticationHeaderValue>("Authorization");
			}
			set
			{
				AddOrRemove("Authorization", value);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Cache-Control" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Cache-Control" /> header for an HTTP request.</returns>
		public CacheControlHeaderValue CacheControl
		{
			get
			{
				return GetValue<CacheControlHeaderValue>("Cache-Control");
			}
			set
			{
				AddOrRemove("Cache-Control", value);
			}
		}

		/// <summary>Gets the value of the <see langword="Connection" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Connection" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<string> Connection => GetValues<string>("Connection");

		/// <summary>Gets or sets a value that indicates if the <see langword="Connection" /> header for an HTTP request contains Close.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="Connection" /> header contains Close, otherwise <see langword="false" />.</returns>
		public bool? ConnectionClose
		{
			get
			{
				if (connectionclose == true || Connection.Find((string l) => string.Equals(l, "close", StringComparison.OrdinalIgnoreCase)) != null)
				{
					return true;
				}
				return connectionclose;
			}
			set
			{
				if (connectionclose != value)
				{
					Connection.Remove("close");
					if (value == true)
					{
						Connection.Add("close");
					}
					connectionclose = value;
				}
			}
		}

		internal bool ConnectionKeepAlive => Connection.Find((string l) => string.Equals(l, "Keep-Alive", StringComparison.OrdinalIgnoreCase)) != null;

		/// <summary>Gets or sets the value of the <see langword="Date" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Date" /> header for an HTTP request.</returns>
		public DateTimeOffset? Date
		{
			get
			{
				return GetValue<DateTimeOffset?>("Date");
			}
			set
			{
				AddOrRemove("Date", value, Parser.DateTime.ToString);
			}
		}

		/// <summary>Gets the value of the <see langword="Expect" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Expect" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<NameValueWithParametersHeaderValue> Expect => GetValues<NameValueWithParametersHeaderValue>("Expect");

		/// <summary>Gets or sets a value that indicates if the <see langword="Expect" /> header for an HTTP request contains Continue.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="Expect" /> header contains Continue, otherwise <see langword="false" />.</returns>
		public bool? ExpectContinue
		{
			get
			{
				if (expectContinue.HasValue)
				{
					return expectContinue;
				}
				if (TransferEncoding.Find((TransferCodingHeaderValue l) => string.Equals(l.Value, "100-continue", StringComparison.OrdinalIgnoreCase)) == null)
				{
					return null;
				}
				return true;
			}
			set
			{
				if (expectContinue != value)
				{
					Expect.Remove((NameValueWithParametersHeaderValue l) => l.Name == "100-continue");
					if (value == true)
					{
						Expect.Add(new NameValueWithParametersHeaderValue("100-continue"));
					}
					expectContinue = value;
				}
			}
		}

		/// <summary>Gets or sets the value of the <see langword="From" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="From" /> header for an HTTP request.</returns>
		public string From
		{
			get
			{
				return GetValue<string>("From");
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && !Parser.EmailAddress.TryParse(value, out value))
				{
					throw new FormatException();
				}
				AddOrRemove("From", value);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Host" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Host" /> header for an HTTP request.</returns>
		public string Host
		{
			get
			{
				return GetValue<string>("Host");
			}
			set
			{
				AddOrRemove("Host", value);
			}
		}

		/// <summary>Gets the value of the <see langword="If-Match" /> header for an HTTP request.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.  
		///  The value of the <see langword="If-Match" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<EntityTagHeaderValue> IfMatch => GetValues<EntityTagHeaderValue>("If-Match");

		/// <summary>Gets or sets the value of the <see langword="If-Modified-Since" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="If-Modified-Since" /> header for an HTTP request.</returns>
		public DateTimeOffset? IfModifiedSince
		{
			get
			{
				return GetValue<DateTimeOffset?>("If-Modified-Since");
			}
			set
			{
				AddOrRemove("If-Modified-Since", value, Parser.DateTime.ToString);
			}
		}

		/// <summary>Gets the value of the <see langword="If-None-Match" /> header for an HTTP request.</summary>
		/// <returns>Gets the value of the <see langword="If-None-Match" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<EntityTagHeaderValue> IfNoneMatch => GetValues<EntityTagHeaderValue>("If-None-Match");

		/// <summary>Gets or sets the value of the <see langword="If-Range" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="If-Range" /> header for an HTTP request.</returns>
		public RangeConditionHeaderValue IfRange
		{
			get
			{
				return GetValue<RangeConditionHeaderValue>("If-Range");
			}
			set
			{
				AddOrRemove("If-Range", value);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="If-Unmodified-Since" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="If-Unmodified-Since" /> header for an HTTP request.</returns>
		public DateTimeOffset? IfUnmodifiedSince
		{
			get
			{
				return GetValue<DateTimeOffset?>("If-Unmodified-Since");
			}
			set
			{
				AddOrRemove("If-Unmodified-Since", value, Parser.DateTime.ToString);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Max-Forwards" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Max-Forwards" /> header for an HTTP request.</returns>
		public int? MaxForwards
		{
			get
			{
				return GetValue<int?>("Max-Forwards");
			}
			set
			{
				AddOrRemove("Max-Forwards", value);
			}
		}

		/// <summary>Gets the value of the <see langword="Pragma" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Pragma" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<NameValueHeaderValue> Pragma => GetValues<NameValueHeaderValue>("Pragma");

		/// <summary>Gets or sets the value of the <see langword="Proxy-Authorization" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Proxy-Authorization" /> header for an HTTP request.</returns>
		public AuthenticationHeaderValue ProxyAuthorization
		{
			get
			{
				return GetValue<AuthenticationHeaderValue>("Proxy-Authorization");
			}
			set
			{
				AddOrRemove("Proxy-Authorization", value);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Range" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Range" /> header for an HTTP request.</returns>
		public RangeHeaderValue Range
		{
			get
			{
				return GetValue<RangeHeaderValue>("Range");
			}
			set
			{
				AddOrRemove("Range", value);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Referer" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Referer" /> header for an HTTP request.</returns>
		public Uri Referrer
		{
			get
			{
				return GetValue<Uri>("Referer");
			}
			set
			{
				AddOrRemove("Referer", value);
			}
		}

		/// <summary>Gets the value of the <see langword="TE" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="TE" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<TransferCodingWithQualityHeaderValue> TE => GetValues<TransferCodingWithQualityHeaderValue>("TE");

		/// <summary>Gets the value of the <see langword="Trailer" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Trailer" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<string> Trailer => GetValues<string>("Trailer");

		/// <summary>Gets the value of the <see langword="Transfer-Encoding" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Transfer-Encoding" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<TransferCodingHeaderValue> TransferEncoding => GetValues<TransferCodingHeaderValue>("Transfer-Encoding");

		/// <summary>Gets or sets a value that indicates if the <see langword="Transfer-Encoding" /> header for an HTTP request contains chunked.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="Transfer-Encoding" /> header contains chunked, otherwise <see langword="false" />.</returns>
		public bool? TransferEncodingChunked
		{
			get
			{
				if (transferEncodingChunked.HasValue)
				{
					return transferEncodingChunked;
				}
				if (TransferEncoding.Find((TransferCodingHeaderValue l) => string.Equals(l.Value, "chunked", StringComparison.OrdinalIgnoreCase)) == null)
				{
					return null;
				}
				return true;
			}
			set
			{
				if (value != transferEncodingChunked)
				{
					TransferEncoding.Remove((TransferCodingHeaderValue l) => l.Value == "chunked");
					if (value == true)
					{
						TransferEncoding.Add(new TransferCodingHeaderValue("chunked"));
					}
					transferEncodingChunked = value;
				}
			}
		}

		/// <summary>Gets the value of the <see langword="Upgrade" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Upgrade" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<ProductHeaderValue> Upgrade => GetValues<ProductHeaderValue>("Upgrade");

		/// <summary>Gets the value of the <see langword="User-Agent" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="User-Agent" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<ProductInfoHeaderValue> UserAgent => GetValues<ProductInfoHeaderValue>("User-Agent");

		/// <summary>Gets the value of the <see langword="Via" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Via" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<ViaHeaderValue> Via => GetValues<ViaHeaderValue>("Via");

		/// <summary>Gets the value of the <see langword="Warning" /> header for an HTTP request.</summary>
		/// <returns>The value of the <see langword="Warning" /> header for an HTTP request.</returns>
		public HttpHeaderValueCollection<WarningHeaderValue> Warning => GetValues<WarningHeaderValue>("Warning");

		internal HttpRequestHeaders()
			: base(HttpHeaderKind.Request)
		{
		}

		internal void AddHeaders(HttpRequestHeaders headers)
		{
			foreach (KeyValuePair<string, IEnumerable<string>> header in headers)
			{
				TryAddWithoutValidation(header.Key, header.Value);
			}
		}
	}
	/// <summary>Represents the collection of Response Headers as defined in RFC 2616.</summary>
	public sealed class HttpResponseHeaders : HttpHeaders
	{
		/// <summary>Gets the value of the <see langword="Accept-Ranges" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Accept-Ranges" /> header for an HTTP response.</returns>
		public HttpHeaderValueCollection<string> AcceptRanges => GetValues<string>("Accept-Ranges");

		/// <summary>Gets or sets the value of the <see langword="Age" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Age" /> header for an HTTP response.</returns>
		public TimeSpan? Age
		{
			get
			{
				return GetValue<TimeSpan?>("Age");
			}
			set
			{
				AddOrRemove("Age", value, (object l) => ((long)((TimeSpan)l).TotalSeconds).ToString());
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Cache-Control" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Cache-Control" /> header for an HTTP response.</returns>
		public CacheControlHeaderValue CacheControl
		{
			get
			{
				return GetValue<CacheControlHeaderValue>("Cache-Control");
			}
			set
			{
				AddOrRemove("Cache-Control", value);
			}
		}

		/// <summary>Gets the value of the <see langword="Connection" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Connection" /> header for an HTTP response.</returns>
		public HttpHeaderValueCollection<string> Connection => GetValues<string>("Connection");

		/// <summary>Gets or sets a value that indicates if the <see langword="Connection" /> header for an HTTP response contains Close.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="Connection" /> header contains Close, otherwise <see langword="false" />.</returns>
		public bool? ConnectionClose
		{
			get
			{
				if (connectionclose == true || Connection.Find((string l) => string.Equals(l, "close", StringComparison.OrdinalIgnoreCase)) != null)
				{
					return true;
				}
				return connectionclose;
			}
			set
			{
				if (connectionclose != value)
				{
					Connection.Remove("close");
					if (value == true)
					{
						Connection.Add("close");
					}
					connectionclose = value;
				}
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Date" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Date" /> header for an HTTP response.</returns>
		public DateTimeOffset? Date
		{
			get
			{
				return GetValue<DateTimeOffset?>("Date");
			}
			set
			{
				AddOrRemove("Date", value, Parser.DateTime.ToString);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="ETag" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="ETag" /> header for an HTTP response.</returns>
		public EntityTagHeaderValue ETag
		{
			get
			{
				return GetValue<EntityTagHeaderValue>("ETag");
			}
			set
			{
				AddOrRemove("ETag", value);
			}
		}

		/// <summary>Gets or sets the value of the <see langword="Location" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Location" /> header for an HTTP response.</returns>
		public Uri Location
		{
			get
			{
				return GetValue<Uri>("Location");
			}
			set
			{
				AddOrRemove("Location", value);
			}
		}

		/// <summary>Gets the value of the <see langword="Pragma" /> header for an HTTP response.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.HttpHeaderValueCollection`1" />.  
		///  The value of the <see langword="Pragma" /> header for an HTTP response.</returns>
		public HttpHeaderValueCollection<NameValueHeaderValue> Pragma => GetValues<NameValueHeaderValue>("Pragma");

		/// <summary>Gets the value of the <see langword="Proxy-Authenticate" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Proxy-Authenticate" /> header for an HTTP response.</returns>
		public HttpHeaderValueCollection<AuthenticationHeaderValue> ProxyAuthenticate => GetValues<AuthenticationHeaderValue>("Proxy-Authenticate");

		/// <summary>Gets or sets the value of the <see langword="Retry-After" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Retry-After" /> header for an HTTP response.</returns>
		public RetryConditionHeaderValue RetryAfter
		{
			get
			{
				return GetValue<RetryConditionHeaderValue>("Retry-After");
			}
			set
			{
				AddOrRemove("Retry-After", value);
			}
		}

		/// <summary>Gets the value of the <see langword="Server" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Server" /> header for an HTTP response.</returns>
		public HttpHeaderValueCollection<ProductInfoHeaderValue> Server => GetValues<ProductInfoHeaderValue>("Server");

		/// <summary>Gets the value of the <see langword="Trailer" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Trailer" /> header for an HTTP response.</returns>
		public HttpHeaderValueCollection<string> Trailer => GetValues<string>("Trailer");

		/// <summary>Gets the value of the <see langword="Transfer-Encoding" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Transfer-Encoding" /> header for an HTTP response.</returns>
		public HttpHeaderValueCollection<TransferCodingHeaderValue> TransferEncoding => GetValues<TransferCodingHeaderValue>("Transfer-Encoding");

		/// <summary>Gets or sets a value that indicates if the <see langword="Transfer-Encoding" /> header for an HTTP response contains chunked.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="Transfer-Encoding" /> header contains chunked, otherwise <see langword="false" />.</returns>
		public bool? TransferEncodingChunked
		{
			get
			{
				if (transferEncodingChunked.HasValue)
				{
					return transferEncodingChunked;
				}
				if (TransferEncoding.Find((TransferCodingHeaderValue l) => StringComparer.OrdinalIgnoreCase.Equals(l.Value, "chunked")) == null)
				{
					return null;
				}
				return true;
			}
			set
			{
				if (value != transferEncodingChunked)
				{
					TransferEncoding.Remove((TransferCodingHeaderValue l) => l.Value == "chunked");
					if (value == true)
					{
						TransferEncoding.Add(new TransferCodingHeaderValue("chunked"));
					}
					transferEncodingChunked = value;
				}
			}
		}

		/// <summary>Gets the value of the <see langword="Upgrade" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Upgrade" /> header for an HTTP response.</returns>
		public HttpHeaderValueCollection<ProductHeaderValue> Upgrade => GetValues<ProductHeaderValue>("Upgrade");

		/// <summary>Gets the value of the <see langword="Vary" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Vary" /> header for an HTTP response.</returns>
		public HttpHeaderValueCollection<string> Vary => GetValues<string>("Vary");

		/// <summary>Gets the value of the <see langword="Via" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Via" /> header for an HTTP response.</returns>
		public HttpHeaderValueCollection<ViaHeaderValue> Via => GetValues<ViaHeaderValue>("Via");

		/// <summary>Gets the value of the <see langword="Warning" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="Warning" /> header for an HTTP response.</returns>
		public HttpHeaderValueCollection<WarningHeaderValue> Warning => GetValues<WarningHeaderValue>("Warning");

		/// <summary>Gets the value of the <see langword="WWW-Authenticate" /> header for an HTTP response.</summary>
		/// <returns>The value of the <see langword="WWW-Authenticate" /> header for an HTTP response.</returns>
		public HttpHeaderValueCollection<AuthenticationHeaderValue> WwwAuthenticate => GetValues<AuthenticationHeaderValue>("WWW-Authenticate");

		internal HttpResponseHeaders()
			: base(HttpHeaderKind.Response)
		{
		}
	}
	internal struct Token
	{
		public enum Type
		{
			Error,
			End,
			Token,
			QuotedString,
			SeparatorEqual,
			SeparatorSemicolon,
			SeparatorSlash,
			SeparatorDash,
			SeparatorComma,
			OpenParens
		}

		public static readonly Token Empty = new Token(Type.Token, 0, 0);

		private readonly Type type;

		public int StartPosition { get; private set; }

		public int EndPosition { get; private set; }

		public Type Kind => type;

		public Token(Type type, int startPosition, int endPosition)
		{
			this = default(Token);
			this.type = type;
			StartPosition = startPosition;
			EndPosition = endPosition;
		}

		public static implicit operator Type(Token token)
		{
			return token.type;
		}

		public override string ToString()
		{
			return type.ToString();
		}
	}
	internal class Lexer
	{
		private static readonly bool[] token_chars = new bool[127]
		{
			false, false, false, false, false, false, false, false, false, false,
			false, false, false, false, false, false, false, false, false, false,
			false, false, false, false, false, false, false, false, false, false,
			false, false, false, true, false, true, true, true, true, true,
			false, false, true, true, false, true, true, false, true, true,
			true, true, true, true, true, true, true, true, false, false,
			false, false, false, false, false, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, false, false, false, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, false, true, false, true
		};

		private static readonly int last_token_char = token_chars.Length;

		private static readonly string[] dt_formats = new string[5] { "r", "dddd, dd'-'MMM'-'yy HH:mm:ss 'GMT'", "ddd MMM d HH:mm:ss yyyy", "d MMM yy H:m:s", "ddd, d MMM yyyy H:m:s zzz" };

		private readonly string s;

		private int pos;

		public int Position
		{
			get
			{
				return pos;
			}
			set
			{
				pos = value;
			}
		}

		public Lexer(string stream)
		{
			s = stream;
		}

		public string GetStringValue(Token token)
		{
			return s.Substring(token.StartPosition, token.EndPosition - token.StartPosition);
		}

		public string GetStringValue(Token start, Token end)
		{
			return s.Substring(start.StartPosition, end.EndPosition - start.StartPosition);
		}

		public string GetQuotedStringValue(Token start)
		{
			return s.Substring(start.StartPosition + 1, start.EndPosition - start.StartPosition - 2);
		}

		public string GetRemainingStringValue(int position)
		{
			if (position <= s.Length)
			{
				return s.Substring(position);
			}
			return null;
		}

		public bool IsStarStringValue(Token token)
		{
			if (token.EndPosition - token.StartPosition == 1)
			{
				return s[token.StartPosition] == '*';
			}
			return false;
		}

		public bool TryGetNumericValue(Token token, out int value)
		{
			return int.TryParse(GetStringValue(token), NumberStyles.None, CultureInfo.InvariantCulture, out value);
		}

		public bool TryGetNumericValue(Token token, out long value)
		{
			return long.TryParse(GetStringValue(token), NumberStyles.None, CultureInfo.InvariantCulture, out value);
		}

		public TimeSpan? TryGetTimeSpanValue(Token token)
		{
			if (TryGetNumericValue(token, out int value))
			{
				return TimeSpan.FromSeconds(value);
			}
			return null;
		}

		public bool TryGetDateValue(Token token, out DateTimeOffset value)
		{
			return TryGetDateValue(((Token.Type)token == Token.Type.QuotedString) ? s.Substring(token.StartPosition + 1, token.EndPosition - token.StartPosition - 2) : GetStringValue(token), out value);
		}

		public static bool TryGetDateValue(string text, out DateTimeOffset value)
		{
			return DateTimeOffset.TryParseExact(text, dt_formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal, out value);
		}

		public bool TryGetDoubleValue(Token token, out double value)
		{
			return double.TryParse(GetStringValue(token), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value);
		}

		public static bool IsValidToken(string input)
		{
			int i;
			for (i = 0; i < input.Length; i++)
			{
				if (!IsValidCharacter(input[i]))
				{
					return false;
				}
			}
			return i > 0;
		}

		public static bool IsValidCharacter(char input)
		{
			if (input < last_token_char)
			{
				return token_chars[(uint)input];
			}
			return false;
		}

		public void EatChar()
		{
			pos++;
		}

		public int PeekChar()
		{
			if (pos >= s.Length)
			{
				return -1;
			}
			return s[pos];
		}

		public bool ScanCommentOptional(out string value)
		{
			if (ScanCommentOptional(out value, out var readToken))
			{
				return true;
			}
			return (Token.Type)readToken == Token.Type.End;
		}

		public bool ScanCommentOptional(out string value, out Token readToken)
		{
			readToken = Scan();
			if ((Token.Type)readToken != Token.Type.OpenParens)
			{
				value = null;
				return false;
			}
			int num = 1;
			while (pos < s.Length)
			{
				switch (s[pos])
				{
				case '(':
					num++;
					pos++;
					continue;
				case ')':
				{
					pos++;
					if (--num > 0)
					{
						continue;
					}
					int startPosition = readToken.StartPosition;
					value = s.Substring(startPosition, pos - startPosition);
					return true;
				}
				case ' ':
				case '!':
				case '"':
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '*':
				case '+':
				case ',':
				case '-':
				case '.':
				case '/':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case ':':
				case ';':
				case '<':
				case '=':
				case '>':
				case '?':
				case '@':
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
				case 'G':
				case 'H':
				case 'I':
				case 'J':
				case 'K':
				case 'L':
				case 'M':
				case 'N':
				case 'O':
				case 'P':
				case 'Q':
				case 'R':
				case 'S':
				case 'T':
				case 'U':
				case 'V':
				case 'W':
				case 'X':
				case 'Y':
				case 'Z':
				case '[':
				case '\\':
				case ']':
				case '^':
				case '_':
				case '`':
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
				case '{':
				case '|':
				case '}':
				case '~':
					pos++;
					continue;
				}
				break;
			}
			value = null;
			return false;
		}

		public Token Scan(bool recognizeDash = false)
		{
			int startPosition = pos;
			if (s == null)
			{
				return new Token(Token.Type.Error, 0, 0);
			}
			Token.Type type;
			if (pos >= s.Length)
			{
				type = Token.Type.End;
			}
			else
			{
				type = Token.Type.Error;
				while (true)
				{
					char c = s[pos++];
					switch (c)
					{
					case '\t':
					case ' ':
						goto IL_00a7;
					case '=':
						type = Token.Type.SeparatorEqual;
						break;
					case ';':
						type = Token.Type.SeparatorSemicolon;
						break;
					case '/':
						type = Token.Type.SeparatorSlash;
						break;
					case '-':
						if (recognizeDash)
						{
							type = Token.Type.SeparatorDash;
							break;
						}
						goto default;
					case ',':
						type = Token.Type.SeparatorComma;
						break;
					case '"':
						startPosition = pos - 1;
						while (pos < s.Length)
						{
							switch (s[pos++])
							{
							case '\\':
								if (pos + 1 < s.Length)
								{
									pos++;
									continue;
								}
								break;
							case '"':
								type = Token.Type.QuotedString;
								break;
							default:
								continue;
							}
							break;
						}
						break;
					case '(':
						startPosition = pos - 1;
						type = Token.Type.OpenParens;
						break;
					default:
						if (c >= last_token_char || !token_chars[(uint)c])
						{
							break;
						}
						startPosition = pos - 1;
						type = Token.Type.Token;
						while (pos < s.Length)
						{
							c = s[pos];
							if (c >= last_token_char || !token_chars[(uint)c])
							{
								break;
							}
							pos++;
						}
						break;
					}
					break;
					IL_00a7:
					if (pos == s.Length)
					{
						type = Token.Type.End;
						break;
					}
				}
			}
			return new Token(type, startPosition, pos);
		}
	}
	/// <summary>Represents a media type used in a Content-Type header as defined in the RFC 2616.</summary>
	public class MediaTypeHeaderValue : ICloneable
	{
		internal List<NameValueHeaderValue> parameters;

		internal string media_type;

		/// <summary>Gets or sets the character set.</summary>
		/// <returns>The character set.</returns>
		public string CharSet
		{
			get
			{
				if (parameters == null)
				{
					return null;
				}
				return parameters.Find((NameValueHeaderValue l) => string.Equals(l.Name, "charset", StringComparison.OrdinalIgnoreCase))?.Value;
			}
			set
			{
				if (parameters == null)
				{
					parameters = new List<NameValueHeaderValue>();
				}
				parameters.SetValue("charset", value);
			}
		}

		/// <summary>Gets or sets the media-type header value.</summary>
		/// <returns>The media-type header value.</returns>
		public string MediaType
		{
			get
			{
				return media_type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("MediaType");
				}
				string media;
				Token? token = TryParseMediaType(new Lexer(value), out media);
				if (!token.HasValue || token.Value.Kind != Token.Type.End)
				{
					throw new FormatException();
				}
				media_type = media;
			}
		}

		/// <summary>Gets or sets the media-type header value parameters.</summary>
		/// <returns>The media-type header value parameters.</returns>
		public ICollection<NameValueHeaderValue> Parameters => parameters ?? (parameters = new List<NameValueHeaderValue>());

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> class.</summary>
		/// <param name="mediaType">The source represented as a string to initialize the new instance.</param>
		public MediaTypeHeaderValue(string mediaType)
		{
			MediaType = mediaType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> class.</summary>
		/// <param name="source">A <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> object used to initialize the new instance.</param>
		protected MediaTypeHeaderValue(MediaTypeHeaderValue source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			media_type = source.media_type;
			if (source.parameters == null)
			{
				return;
			}
			foreach (NameValueHeaderValue parameter in source.parameters)
			{
				Parameters.Add(new NameValueHeaderValue(parameter));
			}
		}

		internal MediaTypeHeaderValue()
		{
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return new MediaTypeHeaderValue(this);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is MediaTypeHeaderValue mediaTypeHeaderValue))
			{
				return false;
			}
			if (string.Equals(mediaTypeHeaderValue.media_type, media_type, StringComparison.OrdinalIgnoreCase))
			{
				return mediaTypeHeaderValue.parameters.SequenceEqual(parameters);
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return media_type.ToLowerInvariant().GetHashCode() ^ HashCodeCalculator.Calculate(parameters);
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents media type header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid media type header value information.</exception>
		public static MediaTypeHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			if (parameters == null)
			{
				return media_type;
			}
			return media_type + CollectionExtensions.ToString(parameters);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out MediaTypeHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			List<NameValueHeaderValue> result = null;
			string media;
			Token? token = TryParseMediaType(lexer, out media);
			if (!token.HasValue)
			{
				return false;
			}
			switch (token.Value.Kind)
			{
			case Token.Type.SeparatorSemicolon:
			{
				if (!NameValueHeaderValue.TryParseParameters(lexer, out result, out var t) || (Token.Type)t != Token.Type.End)
				{
					return false;
				}
				break;
			}
			default:
				return false;
			case Token.Type.End:
				break;
			}
			parsedValue = new MediaTypeHeaderValue
			{
				media_type = media,
				parameters = result
			};
			return true;
		}

		internal static Token? TryParseMediaType(Lexer lexer, out string media)
		{
			media = null;
			Token token = lexer.Scan();
			if ((Token.Type)token != Token.Type.Token)
			{
				return null;
			}
			if ((Token.Type)lexer.Scan() != Token.Type.SeparatorSlash)
			{
				return null;
			}
			Token token2 = lexer.Scan();
			if ((Token.Type)token2 != Token.Type.Token)
			{
				return null;
			}
			media = lexer.GetStringValue(token) + "/" + lexer.GetStringValue(token2);
			return lexer.Scan();
		}
	}
	/// <summary>Represents a media type with an additional quality factor used in a Content-Type header.</summary>
	public sealed class MediaTypeWithQualityHeaderValue : MediaTypeHeaderValue
	{
		/// <summary>Gets or sets the quality value for the <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" />.</summary>
		/// <returns>The quality value for the <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> object.</returns>
		public double? Quality
		{
			get
			{
				return QualityValue.GetValue(parameters);
			}
			set
			{
				QualityValue.SetValue(ref parameters, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> class.</summary>
		/// <param name="mediaType">A <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> represented as string to initialize the new instance.</param>
		public MediaTypeWithQualityHeaderValue(string mediaType)
			: base(mediaType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> class.</summary>
		/// <param name="mediaType">A <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> represented as string to initialize the new instance.</param>
		/// <param name="quality">The quality associated with this header value.</param>
		public MediaTypeWithQualityHeaderValue(string mediaType, double quality)
			: this(mediaType)
		{
			Quality = quality;
		}

		private MediaTypeWithQualityHeaderValue()
		{
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents media type with quality header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid media type with quality header value information.</exception>
		public new static MediaTypeWithQualityHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException();
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out MediaTypeWithQualityHeaderValue parsedValue)
		{
			if (TryParseElement(new Lexer(input), out parsedValue, out var t) && (Token.Type)t == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		private static bool TryParseElement(Lexer lexer, out MediaTypeWithQualityHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			List<NameValueHeaderValue> result = null;
			string media;
			Token? token = MediaTypeHeaderValue.TryParseMediaType(lexer, out media);
			if (!token.HasValue)
			{
				t = Token.Empty;
				return false;
			}
			t = token.Value;
			if ((Token.Type)t == Token.Type.SeparatorSemicolon && !NameValueHeaderValue.TryParseParameters(lexer, out result, out t))
			{
				return false;
			}
			parsedValue = new MediaTypeWithQualityHeaderValue();
			parsedValue.media_type = media;
			parsedValue.parameters = result;
			return true;
		}

		internal static bool TryParse(string input, int minimalCount, out List<MediaTypeWithQualityHeaderValue> result)
		{
			return CollectionParser.TryParse(input, minimalCount, (ElementTryParser<MediaTypeWithQualityHeaderValue>)TryParseElement, out result);
		}
	}
	/// <summary>Represents a name/value pair used in various headers as defined in RFC 2616.</summary>
	public class NameValueHeaderValue : ICloneable
	{
		internal string value;

		/// <summary>Gets the header name.</summary>
		/// <returns>The header name.</returns>
		public string Name { get; internal set; }

		/// <summary>Gets the header value.</summary>
		/// <returns>The header value.</returns>
		public string Value
		{
			get
			{
				return value;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					Lexer lexer = new Lexer(value);
					Token token = lexer.Scan();
					if ((Token.Type)lexer.Scan() != Token.Type.End || ((Token.Type)token != Token.Type.Token && (Token.Type)token != Token.Type.QuotedString))
					{
						throw new FormatException();
					}
					value = lexer.GetStringValue(token);
				}
				this.value = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> class.</summary>
		/// <param name="name">The header name.</param>
		public NameValueHeaderValue(string name)
			: this(name, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> class.</summary>
		/// <param name="name">The header name.</param>
		/// <param name="value">The header value.</param>
		public NameValueHeaderValue(string name, string value)
		{
			Parser.Token.Check(name);
			Name = name;
			Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> class.</summary>
		/// <param name="source">A <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> object used to initialize the new instance.</param>
		protected internal NameValueHeaderValue(NameValueHeaderValue source)
		{
			Name = source.Name;
			value = source.value;
		}

		internal NameValueHeaderValue()
		{
		}

		internal static NameValueHeaderValue Create(string name, string value)
		{
			return new NameValueHeaderValue
			{
				Name = name,
				value = value
			};
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return new NameValueHeaderValue(this);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			int num = Name.ToLowerInvariant().GetHashCode();
			if (!string.IsNullOrEmpty(value))
			{
				num ^= value.ToLowerInvariant().GetHashCode();
			}
			return num;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is NameValueHeaderValue nameValueHeaderValue) || !string.Equals(nameValueHeaderValue.Name, Name, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (string.IsNullOrEmpty(value))
			{
				return string.IsNullOrEmpty(nameValueHeaderValue.value);
			}
			return string.Equals(nameValueHeaderValue.value, value, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents name value header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid name value header value information.</exception>
		public static NameValueHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		internal static bool TryParsePragma(string input, int minimalCount, out List<NameValueHeaderValue> result)
		{
			return CollectionParser.TryParse(input, minimalCount, (ElementTryParser<NameValueHeaderValue>)TryParseElement, out result);
		}

		internal static bool TryParseParameters(Lexer lexer, out List<NameValueHeaderValue> result, out Token t)
		{
			List<NameValueHeaderValue> list = new List<NameValueHeaderValue>();
			result = null;
			do
			{
				Token token = lexer.Scan();
				if ((Token.Type)token != Token.Type.Token)
				{
					t = Token.Empty;
					return false;
				}
				string text = null;
				t = lexer.Scan();
				if ((Token.Type)t == Token.Type.SeparatorEqual)
				{
					t = lexer.Scan();
					if ((Token.Type)t != Token.Type.Token && (Token.Type)t != Token.Type.QuotedString)
					{
						return false;
					}
					text = lexer.GetStringValue(t);
					t = lexer.Scan();
				}
				list.Add(new NameValueHeaderValue
				{
					Name = lexer.GetStringValue(token),
					value = text
				});
			}
			while ((Token.Type)t == Token.Type.SeparatorSemicolon);
			result = list;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			if (string.IsNullOrEmpty(value))
			{
				return Name;
			}
			return Name + "=" + value;
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out NameValueHeaderValue parsedValue)
		{
			if (TryParseElement(new Lexer(input), out parsedValue, out var t) && (Token.Type)t == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		private static bool TryParseElement(Lexer lexer, out NameValueHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan();
			if ((Token.Type)t != Token.Type.Token)
			{
				return false;
			}
			parsedValue = new NameValueHeaderValue
			{
				Name = lexer.GetStringValue(t)
			};
			t = lexer.Scan();
			if ((Token.Type)t == Token.Type.SeparatorEqual)
			{
				t = lexer.Scan();
				if ((Token.Type)t != Token.Type.Token && (Token.Type)t != Token.Type.QuotedString)
				{
					return false;
				}
				parsedValue.value = lexer.GetStringValue(t);
				t = lexer.Scan();
			}
			return true;
		}
	}
	/// <summary>Represents a name/value pair with parameters used in various headers as defined in RFC 2616.</summary>
	public class NameValueWithParametersHeaderValue : NameValueHeaderValue, ICloneable
	{
		private List<NameValueHeaderValue> parameters;

		/// <summary>Gets the parameters from the <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> object.</summary>
		/// <returns>A collection containing the parameters.</returns>
		public ICollection<NameValueHeaderValue> Parameters => parameters ?? (parameters = new List<NameValueHeaderValue>());

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> class.</summary>
		/// <param name="name">The header name.</param>
		public NameValueWithParametersHeaderValue(string name)
			: base(name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> class.</summary>
		/// <param name="name">The header name.</param>
		/// <param name="value">The header value.</param>
		public NameValueWithParametersHeaderValue(string name, string value)
			: base(name, value)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> class.</summary>
		/// <param name="source">A <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> object used to initialize the new instance.</param>
		protected NameValueWithParametersHeaderValue(NameValueWithParametersHeaderValue source)
			: base(source)
		{
			if (source.parameters == null)
			{
				return;
			}
			foreach (NameValueHeaderValue parameter in source.parameters)
			{
				Parameters.Add(parameter);
			}
		}

		private NameValueWithParametersHeaderValue()
		{
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return new NameValueWithParametersHeaderValue(this);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is NameValueWithParametersHeaderValue nameValueWithParametersHeaderValue))
			{
				return false;
			}
			if (base.Equals(obj))
			{
				return nameValueWithParametersHeaderValue.parameters.SequenceEqual(parameters);
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ HashCodeCalculator.Calculate(parameters);
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents name value with parameter header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid name value with parameter header value information.</exception>
		public new static NameValueWithParametersHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			if (parameters == null || parameters.Count == 0)
			{
				return base.ToString();
			}
			return base.ToString() + CollectionExtensions.ToString(parameters);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.NameValueWithParametersHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out NameValueWithParametersHeaderValue parsedValue)
		{
			if (TryParseElement(new Lexer(input), out parsedValue, out var t) && (Token.Type)t == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		internal static bool TryParse(string input, int minimalCount, out List<NameValueWithParametersHeaderValue> result)
		{
			return CollectionParser.TryParse(input, minimalCount, (ElementTryParser<NameValueWithParametersHeaderValue>)TryParseElement, out result);
		}

		private static bool TryParseElement(Lexer lexer, out NameValueWithParametersHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan();
			if ((Token.Type)t != Token.Type.Token)
			{
				return false;
			}
			parsedValue = new NameValueWithParametersHeaderValue
			{
				Name = lexer.GetStringValue(t)
			};
			t = lexer.Scan();
			if ((Token.Type)t == Token.Type.SeparatorEqual)
			{
				t = lexer.Scan();
				if ((Token.Type)t != Token.Type.Token && (Token.Type)t != Token.Type.QuotedString)
				{
					return false;
				}
				parsedValue.value = lexer.GetStringValue(t);
				t = lexer.Scan();
			}
			if ((Token.Type)t == Token.Type.SeparatorSemicolon)
			{
				if (!NameValueHeaderValue.TryParseParameters(lexer, out var result, out t))
				{
					return false;
				}
				parsedValue.parameters = result;
			}
			return true;
		}
	}
	internal static class Parser
	{
		public static class Token
		{
			public static bool TryParse(string input, out string result)
			{
				if (input != null && Lexer.IsValidToken(input))
				{
					result = input;
					return true;
				}
				result = null;
				return false;
			}

			public static void Check(string s)
			{
				if (s == null)
				{
					throw new ArgumentNullException();
				}
				if (!Lexer.IsValidToken(s))
				{
					if (s.Length == 0)
					{
						throw new ArgumentException();
					}
					throw new FormatException(s);
				}
			}

			public static bool TryCheck(string s)
			{
				if (s == null)
				{
					return false;
				}
				return Lexer.IsValidToken(s);
			}

			public static void CheckQuotedString(string s)
			{
				if (s == null)
				{
					throw new ArgumentNullException();
				}
				Lexer lexer = new Lexer(s);
				if ((System.Net.Http.Headers.Token.Type)lexer.Scan() == System.Net.Http.Headers.Token.Type.QuotedString && (System.Net.Http.Headers.Token.Type)lexer.Scan() == System.Net.Http.Headers.Token.Type.End)
				{
					return;
				}
				if (s.Length == 0)
				{
					throw new ArgumentException();
				}
				throw new FormatException(s);
			}

			public static void CheckComment(string s)
			{
				if (s == null)
				{
					throw new ArgumentNullException();
				}
				if (!new Lexer(s).ScanCommentOptional(out var _))
				{
					if (s.Length == 0)
					{
						throw new ArgumentException();
					}
					throw new FormatException(s);
				}
			}
		}

		public static class DateTime
		{
			public new static readonly Func<object, string> ToString = (object l) => ((DateTimeOffset)l).ToString("r", CultureInfo.InvariantCulture);

			public static bool TryParse(string input, out DateTimeOffset result)
			{
				return Lexer.TryGetDateValue(input, out result);
			}
		}

		public static class EmailAddress
		{
			public static bool TryParse(string input, out string result)
			{
				try
				{
					new MailAddress(input);
					result = input;
					return true;
				}
				catch
				{
					result = null;
					return false;
				}
			}
		}

		public static class Host
		{
			public static bool TryParse(string input, out string result)
			{
				result = input;
				System.Uri result2;
				return System.Uri.TryCreate("http://u@" + input + "/", UriKind.Absolute, out result2);
			}
		}

		public static class Int
		{
			public static bool TryParse(string input, out int result)
			{
				return int.TryParse(input, NumberStyles.None, CultureInfo.InvariantCulture, out result);
			}
		}

		public static class Long
		{
			public static bool TryParse(string input, out long result)
			{
				return long.TryParse(input, NumberStyles.None, CultureInfo.InvariantCulture, out result);
			}
		}

		public static class MD5
		{
			public new static readonly Func<object, string> ToString = (object l) => Convert.ToBase64String((byte[])l);

			public static bool TryParse(string input, out byte[] result)
			{
				try
				{
					result = Convert.FromBase64String(input);
					return true;
				}
				catch
				{
					result = null;
					return false;
				}
			}
		}

		public static class TimeSpanSeconds
		{
			public static bool TryParse(string input, out TimeSpan result)
			{
				if (Int.TryParse(input, out var result2))
				{
					result = TimeSpan.FromSeconds(result2);
					return true;
				}
				result = TimeSpan.Zero;
				return false;
			}
		}

		public static class Uri
		{
			public static bool TryParse(string input, out System.Uri result)
			{
				return System.Uri.TryCreate(input, UriKind.RelativeOrAbsolute, out result);
			}

			public static void Check(string s)
			{
				if (s == null)
				{
					throw new ArgumentNullException();
				}
				if (!TryParse(s, out var _))
				{
					if (s.Length == 0)
					{
						throw new ArgumentException();
					}
					throw new FormatException(s);
				}
			}
		}
	}
	/// <summary>Represents a product token value in a User-Agent header.</summary>
	public class ProductHeaderValue : ICloneable
	{
		/// <summary>Gets the name of the product token.</summary>
		/// <returns>The name of the product token.</returns>
		public string Name { get; internal set; }

		/// <summary>Gets the version of the product token.</summary>
		/// <returns>The version of the product token.</returns>
		public string Version { get; internal set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> class.</summary>
		/// <param name="name">The product name.</param>
		public ProductHeaderValue(string name)
		{
			Parser.Token.Check(name);
			Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> class.</summary>
		/// <param name="name">The product name value.</param>
		/// <param name="version">The product version value.</param>
		public ProductHeaderValue(string name, string version)
			: this(name)
		{
			if (!string.IsNullOrEmpty(version))
			{
				Parser.Token.Check(version);
			}
			Version = version;
		}

		internal ProductHeaderValue()
		{
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is ProductHeaderValue productHeaderValue))
			{
				return false;
			}
			if (string.Equals(productHeaderValue.Name, Name, StringComparison.OrdinalIgnoreCase))
			{
				return string.Equals(productHeaderValue.Version, Version, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			int num = Name.ToLowerInvariant().GetHashCode();
			if (Version != null)
			{
				num ^= Version.ToLowerInvariant().GetHashCode();
			}
			return num;
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents product header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> instance.</returns>
		public static ProductHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out ProductHeaderValue parsedValue)
		{
			if (TryParseElement(new Lexer(input), out parsedValue, out var t) && (Token.Type)t == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		internal static bool TryParse(string input, int minimalCount, out List<ProductHeaderValue> result)
		{
			return CollectionParser.TryParse(input, minimalCount, (ElementTryParser<ProductHeaderValue>)TryParseElement, out result);
		}

		private static bool TryParseElement(Lexer lexer, out ProductHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan();
			if ((Token.Type)t != Token.Type.Token)
			{
				return false;
			}
			parsedValue = new ProductHeaderValue();
			parsedValue.Name = lexer.GetStringValue(t);
			t = lexer.Scan();
			if ((Token.Type)t == Token.Type.SeparatorSlash)
			{
				t = lexer.Scan();
				if ((Token.Type)t != Token.Type.Token)
				{
					return false;
				}
				parsedValue.Version = lexer.GetStringValue(t);
				t = lexer.Scan();
			}
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			if (Version != null)
			{
				return Name + "/" + Version;
			}
			return Name;
		}
	}
	/// <summary>Represents a value which can either be a product or a comment in a User-Agent header.</summary>
	public class ProductInfoHeaderValue : ICloneable
	{
		/// <summary>Gets the comment from the <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> object.</summary>
		/// <returns>The comment value this <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" />.</returns>
		public string Comment { get; private set; }

		/// <summary>Gets the product from the <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> object.</summary>
		/// <returns>The product value from this <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" />.</returns>
		public ProductHeaderValue Product { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> class.</summary>
		/// <param name="product">A <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> object used to initialize the new instance.</param>
		public ProductInfoHeaderValue(ProductHeaderValue product)
		{
			if (product == null)
			{
				throw new ArgumentNullException();
			}
			Product = product;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> class.</summary>
		/// <param name="comment">A comment value.</param>
		public ProductInfoHeaderValue(string comment)
		{
			Parser.Token.CheckComment(comment);
			Comment = comment;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> class.</summary>
		/// <param name="productName">The product name value.</param>
		/// <param name="productVersion">The product version value.</param>
		public ProductInfoHeaderValue(string productName, string productVersion)
		{
			Product = new ProductHeaderValue(productName, productVersion);
		}

		private ProductInfoHeaderValue()
		{
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is ProductInfoHeaderValue productInfoHeaderValue))
			{
				return false;
			}
			if (Product == null)
			{
				return productInfoHeaderValue.Comment == Comment;
			}
			return Product.Equals(productInfoHeaderValue.Product);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			if (Product == null)
			{
				return Comment.GetHashCode();
			}
			return Product.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents product info header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid product info header value information.</exception>
		public static ProductInfoHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out ProductInfoHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			if (!TryParseElement(lexer, out parsedValue) || parsedValue == null)
			{
				return false;
			}
			if ((Token.Type)lexer.Scan() != Token.Type.End)
			{
				parsedValue = null;
				return false;
			}
			return true;
		}

		internal static bool TryParse(string input, int minimalCount, out List<ProductInfoHeaderValue> result)
		{
			List<ProductInfoHeaderValue> list = new List<ProductInfoHeaderValue>();
			Lexer lexer = new Lexer(input);
			result = null;
			while (true)
			{
				if (!TryParseElement(lexer, out var parsedValue))
				{
					return false;
				}
				if (parsedValue == null)
				{
					if (list != null && minimalCount <= list.Count)
					{
						result = list;
						return true;
					}
					return false;
				}
				list.Add(parsedValue);
				switch (lexer.PeekChar())
				{
				case 9:
				case 32:
					goto IL_004e;
				case -1:
					if (minimalCount <= list.Count)
					{
						result = list;
						return true;
					}
					break;
				}
				break;
				IL_004e:
				lexer.EatChar();
			}
			return false;
		}

		private static bool TryParseElement(Lexer lexer, out ProductInfoHeaderValue parsedValue)
		{
			parsedValue = null;
			if (lexer.ScanCommentOptional(out var value, out var readToken))
			{
				if (value == null)
				{
					return false;
				}
				parsedValue = new ProductInfoHeaderValue();
				parsedValue.Comment = value;
				return true;
			}
			if ((Token.Type)readToken == Token.Type.End)
			{
				return true;
			}
			if ((Token.Type)readToken != Token.Type.Token)
			{
				return false;
			}
			ProductHeaderValue productHeaderValue = new ProductHeaderValue();
			productHeaderValue.Name = lexer.GetStringValue(readToken);
			int position = lexer.Position;
			readToken = lexer.Scan();
			if ((Token.Type)readToken == Token.Type.SeparatorSlash)
			{
				readToken = lexer.Scan();
				if ((Token.Type)readToken != Token.Type.Token)
				{
					return false;
				}
				productHeaderValue.Version = lexer.GetStringValue(readToken);
			}
			else
			{
				lexer.Position = position;
			}
			parsedValue = new ProductInfoHeaderValue(productHeaderValue);
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.ProductInfoHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			if (Product == null)
			{
				return Comment;
			}
			return Product.ToString();
		}
	}
	internal static class QualityValue
	{
		public static double? GetValue(List<NameValueHeaderValue> parameters)
		{
			if (parameters == null)
			{
				return null;
			}
			NameValueHeaderValue nameValueHeaderValue = parameters.Find((NameValueHeaderValue l) => string.Equals(l.Name, "q", StringComparison.OrdinalIgnoreCase));
			if (nameValueHeaderValue == null)
			{
				return null;
			}
			if (!double.TryParse(nameValueHeaderValue.Value, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out var result))
			{
				return null;
			}
			return result;
		}

		public static void SetValue(ref List<NameValueHeaderValue> parameters, double? value)
		{
			if (value < 0.0 || value > 1.0)
			{
				throw new ArgumentOutOfRangeException("Quality");
			}
			if (parameters == null)
			{
				parameters = new List<NameValueHeaderValue>();
			}
			parameters.SetValue("q", (!value.HasValue) ? null : value.Value.ToString(NumberFormatInfo.InvariantInfo));
		}
	}
	/// <summary>Represents an If-Range header value which can either be a date/time or an entity-tag value.</summary>
	public class RangeConditionHeaderValue : ICloneable
	{
		/// <summary>Gets the date from the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <returns>The date from the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</returns>
		public DateTimeOffset? Date { get; private set; }

		/// <summary>Gets the entity tag from the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <returns>The entity tag from the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</returns>
		public EntityTagHeaderValue EntityTag { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> class.</summary>
		/// <param name="date">A date value used to initialize the new instance.</param>
		public RangeConditionHeaderValue(DateTimeOffset date)
		{
			Date = date;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> class.</summary>
		/// <param name="entityTag">An <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> object used to initialize the new instance.</param>
		public RangeConditionHeaderValue(EntityTagHeaderValue entityTag)
		{
			if (entityTag == null)
			{
				throw new ArgumentNullException("entityTag");
			}
			EntityTag = entityTag;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> class.</summary>
		/// <param name="entityTag">An entity tag represented as a string used to initialize the new instance.</param>
		public RangeConditionHeaderValue(string entityTag)
			: this(new EntityTagHeaderValue(entityTag))
		{
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is RangeConditionHeaderValue rangeConditionHeaderValue))
			{
				return false;
			}
			if (EntityTag == null)
			{
				DateTimeOffset? date = Date;
				DateTimeOffset? date2 = rangeConditionHeaderValue.Date;
				if (date.HasValue != date2.HasValue)
				{
					return false;
				}
				if (!date.HasValue)
				{
					return true;
				}
				return date.GetValueOrDefault() == date2.GetValueOrDefault();
			}
			return EntityTag.Equals(rangeConditionHeaderValue.EntityTag);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			if (EntityTag == null)
			{
				return Date.GetHashCode();
			}
			return EntityTag.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents range condition header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid range Condition header value information.</exception>
		public static RangeConditionHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out RangeConditionHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			Token token = lexer.Scan();
			bool isWeak;
			if ((Token.Type)token == Token.Type.Token)
			{
				if (lexer.GetStringValue(token) != "W")
				{
					if (!Lexer.TryGetDateValue(input, out var value))
					{
						return false;
					}
					parsedValue = new RangeConditionHeaderValue(value);
					return true;
				}
				if (lexer.PeekChar() != 47)
				{
					return false;
				}
				isWeak = true;
				lexer.EatChar();
				token = lexer.Scan();
			}
			else
			{
				isWeak = false;
			}
			if ((Token.Type)token != Token.Type.QuotedString)
			{
				return false;
			}
			if ((Token.Type)lexer.Scan() != Token.Type.End)
			{
				return false;
			}
			parsedValue = new RangeConditionHeaderValue(new EntityTagHeaderValue
			{
				Tag = lexer.GetStringValue(token),
				IsWeak = isWeak
			});
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			if (EntityTag != null)
			{
				return EntityTag.ToString();
			}
			return Date.Value.ToString("r", CultureInfo.InvariantCulture);
		}
	}
	/// <summary>Represents a Range header value.</summary>
	public class RangeHeaderValue : ICloneable
	{
		private List<RangeItemHeaderValue> ranges;

		private string unit;

		/// <summary>Gets the ranges specified from the <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> object.</summary>
		/// <returns>The ranges from the <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> object.</returns>
		public ICollection<RangeItemHeaderValue> Ranges => ranges ?? (ranges = new List<RangeItemHeaderValue>());

		/// <summary>Gets the unit from the <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> object.</summary>
		/// <returns>The unit from the <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> object.</returns>
		public string Unit
		{
			get
			{
				return unit;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Unit");
				}
				Parser.Token.Check(value);
				unit = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> class.</summary>
		public RangeHeaderValue()
		{
			unit = "bytes";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> class with a byte range.</summary>
		/// <param name="from">The position at which to start sending data.</param>
		/// <param name="to">The position at which to stop sending data.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />  
		/// -or-  
		/// <paramref name="from" /> or <paramref name="to" /> is less than 0.</exception>
		public RangeHeaderValue(long? from, long? to)
			: this()
		{
			Ranges.Add(new RangeItemHeaderValue(from, to));
		}

		private RangeHeaderValue(RangeHeaderValue source)
			: this()
		{
			if (source.ranges == null)
			{
				return;
			}
			foreach (RangeItemHeaderValue range in source.ranges)
			{
				Ranges.Add(range);
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return new RangeHeaderValue(this);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is RangeHeaderValue rangeHeaderValue))
			{
				return false;
			}
			if (string.Equals(rangeHeaderValue.Unit, Unit, StringComparison.OrdinalIgnoreCase))
			{
				return rangeHeaderValue.ranges.SequenceEqual(ranges);
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return Unit.ToLowerInvariant().GetHashCode() ^ HashCodeCalculator.Calculate(ranges);
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents range header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid range header value information.</exception>
		public static RangeHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> information.</summary>
		/// <param name="input">he string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out RangeHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			Token token = lexer.Scan();
			if ((Token.Type)token != Token.Type.Token)
			{
				return false;
			}
			RangeHeaderValue rangeHeaderValue = new RangeHeaderValue();
			rangeHeaderValue.unit = lexer.GetStringValue(token);
			token = lexer.Scan();
			if ((Token.Type)token != Token.Type.SeparatorEqual)
			{
				return false;
			}
			do
			{
				long? num = null;
				long? num2 = null;
				bool flag = false;
				token = lexer.Scan(recognizeDash: true);
				long result;
				switch (token.Kind)
				{
				case Token.Type.SeparatorDash:
					token = lexer.Scan();
					if (!lexer.TryGetNumericValue(token, out result))
					{
						return false;
					}
					num2 = result;
					break;
				case Token.Type.Token:
				{
					string stringValue = lexer.GetStringValue(token);
					string[] array = stringValue.Split(new char[1] { '-' }, StringSplitOptions.RemoveEmptyEntries);
					if (!Parser.Long.TryParse(array[0], out result))
					{
						return false;
					}
					switch (array.Length)
					{
					case 1:
						token = lexer.Scan(recognizeDash: true);
						num = result;
						switch (token.Kind)
						{
						case Token.Type.SeparatorDash:
							token = lexer.Scan();
							if ((Token.Type)token != Token.Type.Token)
							{
								flag = true;
								break;
							}
							if (!lexer.TryGetNumericValue(token, out result))
							{
								return false;
							}
							num2 = result;
							if (!(num2 < num))
							{
								break;
							}
							return false;
						case Token.Type.End:
							if (stringValue.Length > 0 && stringValue[stringValue.Length - 1] != '-')
							{
								return false;
							}
							flag = true;
							break;
						case Token.Type.SeparatorComma:
							flag = true;
							break;
						default:
							return false;
						}
						break;
					case 2:
						num = result;
						if (!Parser.Long.TryParse(array[1], out result))
						{
							return false;
						}
						num2 = result;
						if (num2 < num)
						{
							return false;
						}
						break;
					default:
						return false;
					}
					break;
				}
				default:
					return false;
				}
				rangeHeaderValue.Ranges.Add(new RangeItemHeaderValue(num, num2));
				if (!flag)
				{
					token = lexer.Scan();
				}
			}
			while ((Token.Type)token == Token.Type.SeparatorComma);
			if ((Token.Type)token != Token.Type.End)
			{
				return false;
			}
			parsedValue = rangeHeaderValue;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.RangeHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(unit);
			stringBuilder.Append("=");
			for (int i = 0; i < Ranges.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(ranges[i]);
			}
			return stringBuilder.ToString();
		}
	}
	/// <summary>Represents a byte range in a Range header value.</summary>
	public class RangeItemHeaderValue : ICloneable
	{
		/// <summary>Gets the position at which to start sending data.</summary>
		/// <returns>The position at which to start sending data.</returns>
		public long? From { get; private set; }

		/// <summary>Gets the position at which to stop sending data.</summary>
		/// <returns>The position at which to stop sending data.</returns>
		public long? To { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> class.</summary>
		/// <param name="from">The position at which to start sending data.</param>
		/// <param name="to">The position at which to stop sending data.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="from" /> is greater than <paramref name="to" />  
		/// -or-  
		/// <paramref name="from" /> or <paramref name="to" /> is less than 0.</exception>
		public RangeItemHeaderValue(long? from, long? to)
		{
			if (!from.HasValue && !to.HasValue)
			{
				throw new ArgumentException();
			}
			if (from.HasValue && to.HasValue && from > to)
			{
				throw new ArgumentOutOfRangeException("from");
			}
			if (from < 0)
			{
				throw new ArgumentOutOfRangeException("from");
			}
			if (to < 0)
			{
				throw new ArgumentOutOfRangeException("to");
			}
			From = from;
			To = to;
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (obj is RangeItemHeaderValue { From: var num } rangeItemHeaderValue && num == From)
			{
				return rangeItemHeaderValue.To == To;
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return From.GetHashCode() ^ To.GetHashCode();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.RangeItemHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			if (!From.HasValue)
			{
				return "-" + To.Value;
			}
			if (!To.HasValue)
			{
				return From.Value + "-";
			}
			return From.Value + "-" + To.Value;
		}
	}
	/// <summary>Represents a Retry-After header value which can either be a date/time or a timespan value.</summary>
	public class RetryConditionHeaderValue : ICloneable
	{
		/// <summary>Gets the date and time offset from the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <returns>The date and time offset from the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</returns>
		public DateTimeOffset? Date { get; private set; }

		/// <summary>Gets the delta in seconds from the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <returns>The delta in seconds from the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</returns>
		public TimeSpan? Delta { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> class.</summary>
		/// <param name="date">The date and time offset used to initialize the new instance.</param>
		public RetryConditionHeaderValue(DateTimeOffset date)
		{
			Date = date;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> class.</summary>
		/// <param name="delta">The delta, in seconds, used to initialize the new instance.</param>
		public RetryConditionHeaderValue(TimeSpan delta)
		{
			if (delta.TotalSeconds > 4294967295.0)
			{
				throw new ArgumentOutOfRangeException("delta");
			}
			Delta = delta;
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (obj is RetryConditionHeaderValue { Date: var date } retryConditionHeaderValue && date == Date)
			{
				TimeSpan? delta = retryConditionHeaderValue.Delta;
				TimeSpan? delta2 = Delta;
				if (delta.HasValue != delta2.HasValue)
				{
					return false;
				}
				if (!delta.HasValue)
				{
					return true;
				}
				return delta.GetValueOrDefault() == delta2.GetValueOrDefault();
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return Date.GetHashCode() ^ Delta.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents retry condition header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid retry condition header value information.</exception>
		public static RetryConditionHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out RetryConditionHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			Token token = lexer.Scan();
			if ((Token.Type)token != Token.Type.Token)
			{
				return false;
			}
			TimeSpan? timeSpan = lexer.TryGetTimeSpanValue(token);
			if (timeSpan.HasValue)
			{
				if ((Token.Type)lexer.Scan() != Token.Type.End)
				{
					return false;
				}
				parsedValue = new RetryConditionHeaderValue(timeSpan.Value);
			}
			else
			{
				if (!Lexer.TryGetDateValue(input, out var value))
				{
					return false;
				}
				parsedValue = new RetryConditionHeaderValue(value);
			}
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			if (!Delta.HasValue)
			{
				return Date.Value.ToString("r", CultureInfo.InvariantCulture);
			}
			return Delta.Value.TotalSeconds.ToString(CultureInfo.InvariantCulture);
		}
	}
	/// <summary>Represents a string header value with an optional quality.</summary>
	public class StringWithQualityHeaderValue : ICloneable
	{
		/// <summary>Gets the quality factor from the <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> object.</summary>
		/// <returns>The quality factor from the <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> object.</returns>
		public double? Quality { get; private set; }

		/// <summary>Gets the string value from the <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> object.</summary>
		/// <returns>The string value from the <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> object.</returns>
		public string Value { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> class.</summary>
		/// <param name="value">The string used to initialize the new instance.</param>
		public StringWithQualityHeaderValue(string value)
		{
			Parser.Token.Check(value);
			Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> class.</summary>
		/// <param name="value">A string used to initialize the new instance.</param>
		/// <param name="quality">A quality factor used to initialize the new instance.</param>
		public StringWithQualityHeaderValue(string value, double quality)
			: this(value)
		{
			if (quality < 0.0 || quality > 1.0)
			{
				throw new ArgumentOutOfRangeException("quality");
			}
			Quality = quality;
		}

		private StringWithQualityHeaderValue()
		{
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>Determines whether the specified Object is equal to the current <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (obj is StringWithQualityHeaderValue stringWithQualityHeaderValue && string.Equals(stringWithQualityHeaderValue.Value, Value, StringComparison.OrdinalIgnoreCase))
			{
				return stringWithQualityHeaderValue.Quality == Quality;
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return Value.ToLowerInvariant().GetHashCode() ^ Quality.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents quality header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid string with quality header value information.</exception>
		public static StringWithQualityHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out StringWithQualityHeaderValue parsedValue)
		{
			if (TryParseElement(new Lexer(input), out parsedValue, out var t) && (Token.Type)t == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		internal static bool TryParse(string input, int minimalCount, out List<StringWithQualityHeaderValue> result)
		{
			return CollectionParser.TryParse(input, minimalCount, (ElementTryParser<StringWithQualityHeaderValue>)TryParseElement, out result);
		}

		private static bool TryParseElement(Lexer lexer, out StringWithQualityHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan();
			if ((Token.Type)t != Token.Type.Token)
			{
				return false;
			}
			StringWithQualityHeaderValue stringWithQualityHeaderValue = new StringWithQualityHeaderValue();
			stringWithQualityHeaderValue.Value = lexer.GetStringValue(t);
			t = lexer.Scan();
			if ((Token.Type)t == Token.Type.SeparatorSemicolon)
			{
				t = lexer.Scan();
				if ((Token.Type)t != Token.Type.Token)
				{
					return false;
				}
				string stringValue = lexer.GetStringValue(t);
				if (stringValue != "q" && stringValue != "Q")
				{
					return false;
				}
				t = lexer.Scan();
				if ((Token.Type)t != Token.Type.SeparatorEqual)
				{
					return false;
				}
				t = lexer.Scan();
				if (!lexer.TryGetDoubleValue(t, out var value))
				{
					return false;
				}
				if (value > 1.0)
				{
					return false;
				}
				stringWithQualityHeaderValue.Quality = value;
				t = lexer.Scan();
			}
			parsedValue = stringWithQualityHeaderValue;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.StringWithQualityHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			if (Quality.HasValue)
			{
				return Value + "; q=" + Quality.Value.ToString("0.0##", CultureInfo.InvariantCulture);
			}
			return Value;
		}
	}
	/// <summary>Represents an accept-encoding header value.</summary>
	public class TransferCodingHeaderValue : ICloneable
	{
		internal string value;

		internal List<NameValueHeaderValue> parameters;

		/// <summary>Gets the transfer-coding parameters.</summary>
		/// <returns>The transfer-coding parameters.</returns>
		public ICollection<NameValueHeaderValue> Parameters => parameters ?? (parameters = new List<NameValueHeaderValue>());

		/// <summary>Gets the transfer-coding value.</summary>
		/// <returns>The transfer-coding value.</returns>
		public string Value => value;

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> class.</summary>
		/// <param name="value">A string used to initialize the new instance.</param>
		public TransferCodingHeaderValue(string value)
		{
			Parser.Token.Check(value);
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> class.</summary>
		/// <param name="source">A <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> object used to initialize the new instance.</param>
		protected TransferCodingHeaderValue(TransferCodingHeaderValue source)
		{
			value = source.value;
			if (source.parameters == null)
			{
				return;
			}
			foreach (NameValueHeaderValue parameter in source.parameters)
			{
				Parameters.Add(new NameValueHeaderValue(parameter));
			}
		}

		internal TransferCodingHeaderValue()
		{
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return new TransferCodingHeaderValue(this);
		}

		/// <summary>Determines whether the specified Object is equal to the current <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (obj is TransferCodingHeaderValue transferCodingHeaderValue && string.Equals(value, transferCodingHeaderValue.value, StringComparison.OrdinalIgnoreCase))
			{
				return parameters.SequenceEqual(transferCodingHeaderValue.parameters);
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			int num = value.ToLowerInvariant().GetHashCode();
			if (parameters != null)
			{
				num ^= HashCodeCalculator.Calculate(parameters);
			}
			return num;
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents transfer-coding header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid transfer-coding header value information.</exception>
		public static TransferCodingHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return value + CollectionExtensions.ToString(parameters);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.TransferCodingHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out TransferCodingHeaderValue parsedValue)
		{
			if (TryParseElement(new Lexer(input), out parsedValue, out var t) && (Token.Type)t == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		internal static bool TryParse(string input, int minimalCount, out List<TransferCodingHeaderValue> result)
		{
			return CollectionParser.TryParse(input, minimalCount, (ElementTryParser<TransferCodingHeaderValue>)TryParseElement, out result);
		}

		private static bool TryParseElement(Lexer lexer, out TransferCodingHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan();
			if ((Token.Type)t != Token.Type.Token)
			{
				return false;
			}
			TransferCodingHeaderValue transferCodingHeaderValue = new TransferCodingHeaderValue();
			transferCodingHeaderValue.value = lexer.GetStringValue(t);
			t = lexer.Scan();
			if ((Token.Type)t == Token.Type.SeparatorSemicolon && (!NameValueHeaderValue.TryParseParameters(lexer, out transferCodingHeaderValue.parameters, out t) || (Token.Type)t != Token.Type.End))
			{
				return false;
			}
			parsedValue = transferCodingHeaderValue;
			return true;
		}
	}
	/// <summary>Represents an Accept-Encoding header value.with optional quality factor.</summary>
	public sealed class TransferCodingWithQualityHeaderValue : TransferCodingHeaderValue
	{
		/// <summary>Gets the quality factor from the <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" />.</summary>
		/// <returns>The quality factor from the <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" />.</returns>
		public double? Quality
		{
			get
			{
				return QualityValue.GetValue(parameters);
			}
			set
			{
				QualityValue.SetValue(ref parameters, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> class.</summary>
		/// <param name="value">A string used to initialize the new instance.</param>
		public TransferCodingWithQualityHeaderValue(string value)
			: base(value)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> class.</summary>
		/// <param name="value">A string used to initialize the new instance.</param>
		/// <param name="quality">A value for the quality factor.</param>
		public TransferCodingWithQualityHeaderValue(string value, double quality)
			: this(value)
		{
			Quality = quality;
		}

		private TransferCodingWithQualityHeaderValue()
		{
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents transfer-coding value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid transfer-coding with quality header value information.</exception>
		public new static TransferCodingWithQualityHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException();
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out TransferCodingWithQualityHeaderValue parsedValue)
		{
			if (TryParseElement(new Lexer(input), out parsedValue, out var t) && (Token.Type)t == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		internal static bool TryParse(string input, int minimalCount, out List<TransferCodingWithQualityHeaderValue> result)
		{
			return CollectionParser.TryParse(input, minimalCount, (ElementTryParser<TransferCodingWithQualityHeaderValue>)TryParseElement, out result);
		}

		private static bool TryParseElement(Lexer lexer, out TransferCodingWithQualityHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan();
			if ((Token.Type)t != Token.Type.Token)
			{
				return false;
			}
			TransferCodingWithQualityHeaderValue transferCodingWithQualityHeaderValue = new TransferCodingWithQualityHeaderValue();
			transferCodingWithQualityHeaderValue.value = lexer.GetStringValue(t);
			t = lexer.Scan();
			if ((Token.Type)t == Token.Type.SeparatorSemicolon && (!NameValueHeaderValue.TryParseParameters(lexer, out transferCodingWithQualityHeaderValue.parameters, out t) || (Token.Type)t != Token.Type.End))
			{
				return false;
			}
			parsedValue = transferCodingWithQualityHeaderValue;
			return true;
		}
	}
	/// <summary>Represents the value of a Via header.</summary>
	public class ViaHeaderValue : ICloneable
	{
		/// <summary>Gets the comment field used to identify the software of the recipient proxy or gateway.</summary>
		/// <returns>The comment field used to identify the software of the recipient proxy or gateway.</returns>
		public string Comment { get; private set; }

		/// <summary>Gets the protocol name of the received protocol.</summary>
		/// <returns>The protocol name.</returns>
		public string ProtocolName { get; private set; }

		/// <summary>Gets the protocol version of the received protocol.</summary>
		/// <returns>The protocol version.</returns>
		public string ProtocolVersion { get; private set; }

		/// <summary>Gets the host and port that the request or response was received by.</summary>
		/// <returns>The host and port that the request or response was received by.</returns>
		public string ReceivedBy { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> class.</summary>
		/// <param name="protocolVersion">The protocol version of the received protocol.</param>
		/// <param name="receivedBy">The host and port that the request or response was received by.</param>
		public ViaHeaderValue(string protocolVersion, string receivedBy)
		{
			Parser.Token.Check(protocolVersion);
			Parser.Uri.Check(receivedBy);
			ProtocolVersion = protocolVersion;
			ReceivedBy = receivedBy;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> class.</summary>
		/// <param name="protocolVersion">The protocol version of the received protocol.</param>
		/// <param name="receivedBy">The host and port that the request or response was received by.</param>
		/// <param name="protocolName">The protocol name of the received protocol.</param>
		public ViaHeaderValue(string protocolVersion, string receivedBy, string protocolName)
			: this(protocolVersion, receivedBy)
		{
			if (!string.IsNullOrEmpty(protocolName))
			{
				Parser.Token.Check(protocolName);
				ProtocolName = protocolName;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> class.</summary>
		/// <param name="protocolVersion">The protocol version of the received protocol.</param>
		/// <param name="receivedBy">The host and port that the request or response was received by.</param>
		/// <param name="protocolName">The protocol name of the received protocol.</param>
		/// <param name="comment">The comment field used to identify the software of the recipient proxy or gateway.</param>
		public ViaHeaderValue(string protocolVersion, string receivedBy, string protocolName, string comment)
			: this(protocolVersion, receivedBy, protocolName)
		{
			if (!string.IsNullOrEmpty(comment))
			{
				Parser.Token.CheckComment(comment);
				Comment = comment;
			}
		}

		private ViaHeaderValue()
		{
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is ViaHeaderValue viaHeaderValue))
			{
				return false;
			}
			if (string.Equals(viaHeaderValue.Comment, Comment, StringComparison.Ordinal) && string.Equals(viaHeaderValue.ProtocolName, ProtocolName, StringComparison.OrdinalIgnoreCase) && string.Equals(viaHeaderValue.ProtocolVersion, ProtocolVersion, StringComparison.OrdinalIgnoreCase))
			{
				return string.Equals(viaHeaderValue.ReceivedBy, ReceivedBy, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			int hashCode = ProtocolVersion.ToLowerInvariant().GetHashCode();
			hashCode ^= ReceivedBy.ToLowerInvariant().GetHashCode();
			if (!string.IsNullOrEmpty(ProtocolName))
			{
				hashCode ^= ProtocolName.ToLowerInvariant().GetHashCode();
			}
			if (!string.IsNullOrEmpty(Comment))
			{
				hashCode ^= Comment.GetHashCode();
			}
			return hashCode;
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents via header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid via header value information.</exception>
		public static ViaHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out ViaHeaderValue parsedValue)
		{
			if (TryParseElement(new Lexer(input), out parsedValue, out var t) && (Token.Type)t == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		internal static bool TryParse(string input, int minimalCount, out List<ViaHeaderValue> result)
		{
			return CollectionParser.TryParse(input, minimalCount, (ElementTryParser<ViaHeaderValue>)TryParseElement, out result);
		}

		private static bool TryParseElement(Lexer lexer, out ViaHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan();
			if ((Token.Type)t != Token.Type.Token)
			{
				return false;
			}
			Token token = lexer.Scan();
			ViaHeaderValue viaHeaderValue = new ViaHeaderValue();
			if ((Token.Type)token == Token.Type.SeparatorSlash)
			{
				token = lexer.Scan();
				if ((Token.Type)token != Token.Type.Token)
				{
					return false;
				}
				viaHeaderValue.ProtocolName = lexer.GetStringValue(t);
				viaHeaderValue.ProtocolVersion = lexer.GetStringValue(token);
				token = lexer.Scan();
			}
			else
			{
				viaHeaderValue.ProtocolVersion = lexer.GetStringValue(t);
			}
			if ((Token.Type)token != Token.Type.Token)
			{
				return false;
			}
			if (lexer.PeekChar() == 58)
			{
				lexer.EatChar();
				t = lexer.Scan();
				if ((Token.Type)t != Token.Type.Token)
				{
					return false;
				}
			}
			else
			{
				t = token;
			}
			viaHeaderValue.ReceivedBy = lexer.GetStringValue(token, t);
			if (lexer.ScanCommentOptional(out var value, out t))
			{
				t = lexer.Scan();
			}
			viaHeaderValue.Comment = value;
			parsedValue = viaHeaderValue;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			string text = ((ProtocolName != null) ? (ProtocolName + "/" + ProtocolVersion + " " + ReceivedBy) : (ProtocolVersion + " " + ReceivedBy));
			if (Comment == null)
			{
				return text;
			}
			return text + " " + Comment;
		}
	}
	/// <summary>Represents a warning value used by the Warning header.</summary>
	public class WarningHeaderValue : ICloneable
	{
		/// <summary>Gets the host that attached the warning.</summary>
		/// <returns>The host that attached the warning.</returns>
		public string Agent { get; private set; }

		/// <summary>Gets the specific warning code.</summary>
		/// <returns>The specific warning code.</returns>
		public int Code { get; private set; }

		/// <summary>Gets the date/time stamp of the warning.</summary>
		/// <returns>The date/time stamp of the warning.</returns>
		public DateTimeOffset? Date { get; private set; }

		/// <summary>Gets a quoted-string containing the warning text.</summary>
		/// <returns>A quoted-string containing the warning text.</returns>
		public string Text { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> class.</summary>
		/// <param name="code">The specific warning code.</param>
		/// <param name="agent">The host that attached the warning.</param>
		/// <param name="text">A quoted-string containing the warning text.</param>
		public WarningHeaderValue(int code, string agent, string text)
		{
			if (!IsCodeValid(code))
			{
				throw new ArgumentOutOfRangeException("code");
			}
			Parser.Uri.Check(agent);
			Parser.Token.CheckQuotedString(text);
			Code = code;
			Agent = agent;
			Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> class.</summary>
		/// <param name="code">The specific warning code.</param>
		/// <param name="agent">The host that attached the warning.</param>
		/// <param name="text">A quoted-string containing the warning text.</param>
		/// <param name="date">The date/time stamp of the warning.</param>
		public WarningHeaderValue(int code, string agent, string text, DateTimeOffset date)
			: this(code, agent, text)
		{
			Date = date;
		}

		private WarningHeaderValue()
		{
		}

		private static bool IsCodeValid(int code)
		{
			if (code >= 0)
			{
				return code < 1000;
			}
			return false;
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> instance.</summary>
		/// <returns>Returns a copy of the current instance.</returns>
		object ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is WarningHeaderValue warningHeaderValue))
			{
				return false;
			}
			if (Code == warningHeaderValue.Code && string.Equals(warningHeaderValue.Agent, Agent, StringComparison.OrdinalIgnoreCase) && Text == warningHeaderValue.Text)
			{
				DateTimeOffset? date = Date;
				DateTimeOffset? date2 = warningHeaderValue.Date;
				if (date.HasValue != date2.HasValue)
				{
					return false;
				}
				if (!date.HasValue)
				{
					return true;
				}
				return date.GetValueOrDefault() == date2.GetValueOrDefault();
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return Code.GetHashCode() ^ Agent.ToLowerInvariant().GetHashCode() ^ Text.GetHashCode() ^ Date.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents authentication header value information.</param>
		/// <returns>Returns a <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid authentication header value information.</exception>
		public static WarningHeaderValue Parse(string input)
		{
			if (TryParse(input, out var parsedValue))
			{
				return parsedValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		public static bool TryParse(string input, out WarningHeaderValue parsedValue)
		{
			if (TryParseElement(new Lexer(input), out parsedValue, out var t) && (Token.Type)t == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		internal static bool TryParse(string input, int minimalCount, out List<WarningHeaderValue> result)
		{
			return CollectionParser.TryParse(input, minimalCount, (ElementTryParser<WarningHeaderValue>)TryParseElement, out result);
		}

		private static bool TryParseElement(Lexer lexer, out WarningHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan();
			if ((Token.Type)t != Token.Type.Token)
			{
				return false;
			}
			if (!lexer.TryGetNumericValue(t, out int value) || !IsCodeValid(value))
			{
				return false;
			}
			t = lexer.Scan();
			if ((Token.Type)t != Token.Type.Token)
			{
				return false;
			}
			Token token = t;
			if (lexer.PeekChar() == 58)
			{
				lexer.EatChar();
				token = lexer.Scan();
				if ((Token.Type)token != Token.Type.Token)
				{
					return false;
				}
			}
			WarningHeaderValue warningHeaderValue = new WarningHeaderValue();
			warningHeaderValue.Code = value;
			warningHeaderValue.Agent = lexer.GetStringValue(t, token);
			t = lexer.Scan();
			if ((Token.Type)t != Token.Type.QuotedString)
			{
				return false;
			}
			warningHeaderValue.Text = lexer.GetStringValue(t);
			t = lexer.Scan();
			if ((Token.Type)t == Token.Type.QuotedString)
			{
				if (!lexer.TryGetDateValue(t, out var value2))
				{
					return false;
				}
				warningHeaderValue.Date = value2;
				t = lexer.Scan();
			}
			parsedValue = warningHeaderValue;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			string text = Code.ToString("000") + " " + Agent + " " + Text;
			if (Date.HasValue)
			{
				text = text + " \"" + Date.Value.ToString("r", CultureInfo.InvariantCulture) + "\"";
			}
			return text;
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
