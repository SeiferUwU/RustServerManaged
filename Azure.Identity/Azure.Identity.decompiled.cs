using System;
using System.Buffers;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Azure.Core;
using Azure.Core.Diagnostics;
using Azure.Core.Pipeline;
using Microsoft.CodeAnalysis;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.Identity.Client.Extensions.Msal;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: InternalsVisibleTo("Microsoft.Extensions.Azure.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100d15ddcb29688295338af4b7686603fe614abd555e09efba8fb88ee09e1f7b1ccaeed2e8f823fa9eef3fdd60217fc012ea67d2479751a0b8c087a4185541b851bd8b16f8d91b840e51b1cb0ba6fe647997e57429265e85ef62d565db50a69ae1647d54d7bd855e4db3d8a91510e5bcbd0edfbbecaa20a7bd9ae74593daa7b11b4")]
[assembly: InternalsVisibleTo("Azure.Identity.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100d15ddcb29688295338af4b7686603fe614abd555e09efba8fb88ee09e1f7b1ccaeed2e8f823fa9eef3fdd60217fc012ea67d2479751a0b8c087a4185541b851bd8b16f8d91b840e51b1cb0ba6fe647997e57429265e85ef62d565db50a69ae1647d54d7bd855e4db3d8a91510e5bcbd0edfbbecaa20a7bd9ae74593daa7b11b4")]
[assembly: InternalsVisibleTo("Azure.Identity.Broker, PublicKey=0024000004800000940000000602000000240000525341310004000001000100097ad52abbeaa2e1a1982747cc0106534f65cfea6707eaed696a3a63daea80de2512746801a7e47f88e7781e71af960d89ba2e25561f70b0e2dbc93319e0af1961a719ccf5a4d28709b2b57a5d29b7c09dc8d269a490ebe2651c4b6e6738c27c5fb2c02469fe9757f0a3479ac310d6588a50a28d7dd431b907fd325e18b9e8ed")]
[assembly: InternalsVisibleTo("Azure.Identity.Broker.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100d15ddcb29688295338af4b7686603fe614abd555e09efba8fb88ee09e1f7b1ccaeed2e8f823fa9eef3fdd60217fc012ea67d2479751a0b8c087a4185541b851bd8b16f8d91b840e51b1cb0ba6fe647997e57429265e85ef62d565db50a69ae1647d54d7bd855e4db3d8a91510e5bcbd0edfbbecaa20a7bd9ae74593daa7b11b4")]
[assembly: InternalsVisibleTo("Azure.Identity.BrokeredAuthentication, PublicKey=0024000004800000940000000602000000240000525341310004000001000100097ad52abbeaa2e1a1982747cc0106534f65cfea6707eaed696a3a63daea80de2512746801a7e47f88e7781e71af960d89ba2e25561f70b0e2dbc93319e0af1961a719ccf5a4d28709b2b57a5d29b7c09dc8d269a490ebe2651c4b6e6738c27c5fb2c02469fe9757f0a3479ac310d6588a50a28d7dd431b907fd325e18b9e8ed")]
[assembly: InternalsVisibleTo("Azure.Identity.BrokeredAuthentication.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100d15ddcb29688295338af4b7686603fe614abd555e09efba8fb88ee09e1f7b1ccaeed2e8f823fa9eef3fdd60217fc012ea67d2479751a0b8c087a4185541b851bd8b16f8d91b840e51b1cb0ba6fe647997e57429265e85ef62d565db50a69ae1647d54d7bd855e4db3d8a91510e5bcbd0edfbbecaa20a7bd9ae74593daa7b11b4")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2, PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]
[assembly: AzureResourceProviderNamespace("Microsoft.AAD")]
[assembly: TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = ".NET Standard 2.0")]
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("© Microsoft Corporation. All rights reserved.")]
[assembly: AssemblyDescription("This is the implementation of the Azure SDK Client Library for Azure Identity")]
[assembly: AssemblyFileVersion("1.1200.24.31701")]
[assembly: AssemblyInformationalVersion("1.12.0+75fb76b92b160a9b4566ec0f2956ba1da1101b23")]
[assembly: AssemblyProduct("Azure .NET SDK")]
[assembly: AssemblyTitle("Microsoft Azure.Identity Component")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/Azure/azure-sdk-for-net")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("1.12.0.0")]
[module: UnverifiableCode]
[module: RefSafetyRules(11)]
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
	internal sealed class IsReadOnlyAttribute : Attribute
	{
	}
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	internal sealed class IsByRefLikeAttribute : Attribute
	{
	}
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
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	[AttributeUsage(AttributeTargets.Module, AllowMultiple = false, Inherited = false)]
	internal sealed class RefSafetyRulesAttribute : Attribute
	{
		public readonly int Version;

		public RefSafetyRulesAttribute(int P_0)
		{
			Version = P_0;
		}
	}
}
namespace System.Diagnostics.CodeAnalysis
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	internal sealed class RequiresDynamicCodeAttribute : Attribute
	{
		public string Message { get; }

		public string? Url { get; set; }

		public RequiresDynamicCodeAttribute(string message)
		{
			Message = message;
		}
	}
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	internal sealed class RequiresUnreferencedCodeAttribute : Attribute
	{
		public string Message { get; }

		public string? Url { get; set; }

		public RequiresUnreferencedCodeAttribute(string message)
		{
			Message = message;
		}
	}
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	internal sealed class UnconditionalSuppressMessageAttribute : Attribute
	{
		public string Category { get; }

		public string CheckId { get; }

		public string? Scope { get; set; }

		public string? Target { get; set; }

		public string? MessageId { get; set; }

		public string? Justification { get; set; }

		public UnconditionalSuppressMessageAttribute(string category, string checkId)
		{
			Category = category;
			CheckId = checkId;
		}
	}
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class DynamicDependencyAttribute : Attribute
	{
		public string? MemberSignature { get; }

		public DynamicallyAccessedMemberTypes MemberTypes { get; }

		public Type? Type { get; }

		public string? TypeName { get; }

		public string? AssemblyName { get; }

		public string? Condition { get; set; }

		public DynamicDependencyAttribute(string memberSignature)
		{
			MemberSignature = memberSignature;
		}

		public DynamicDependencyAttribute(string memberSignature, Type type)
		{
			MemberSignature = memberSignature;
			Type = type;
		}

		public DynamicDependencyAttribute(string memberSignature, string typeName, string assemblyName)
		{
			MemberSignature = memberSignature;
			TypeName = typeName;
			AssemblyName = assemblyName;
		}

		public DynamicDependencyAttribute(DynamicallyAccessedMemberTypes memberTypes, Type type)
		{
			MemberTypes = memberTypes;
			Type = type;
		}

		public DynamicDependencyAttribute(DynamicallyAccessedMemberTypes memberTypes, string typeName, string assemblyName)
		{
			MemberTypes = memberTypes;
			TypeName = typeName;
			AssemblyName = assemblyName;
		}
	}
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, Inherited = false)]
	internal sealed class DynamicallyAccessedMembersAttribute : Attribute
	{
		public DynamicallyAccessedMemberTypes MemberTypes { get; }

		public DynamicallyAccessedMembersAttribute(DynamicallyAccessedMemberTypes memberTypes)
		{
			MemberTypes = memberTypes;
		}
	}
	[Flags]
	internal enum DynamicallyAccessedMemberTypes
	{
		None = 0,
		PublicParameterlessConstructor = 1,
		PublicConstructors = 3,
		NonPublicConstructors = 4,
		PublicMethods = 8,
		NonPublicMethods = 0x10,
		PublicFields = 0x20,
		NonPublicFields = 0x40,
		PublicNestedTypes = 0x80,
		NonPublicNestedTypes = 0x100,
		PublicProperties = 0x200,
		NonPublicProperties = 0x400,
		PublicEvents = 0x800,
		NonPublicEvents = 0x1000,
		Interfaces = 0x2000,
		All = -1
	}
}
namespace Azure
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class FriendAttribute : Attribute
	{
		public string FriendAssembly { get; }

		public FriendAttribute(string friendAssembly)
		{
			FriendAssembly = friendAssembly;
		}
	}
}
namespace Azure.Core
{
	internal class HttpPipelineMessageHandler : HttpMessageHandler
	{
		private readonly HttpPipeline _pipeline;

		public HttpPipelineMessageHandler(HttpPipeline pipeline)
		{
			_pipeline = pipeline;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			Request request2 = await ToPipelineRequestAsync(request).ConfigureAwait(continueOnCapturedContext: false);
			return ToHttpResponseMessage(await _pipeline.SendRequestAsync(request2, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		private async Task<Request> ToPipelineRequestAsync(HttpRequestMessage request)
		{
			Request pipelineRequest = _pipeline.CreateRequest();
			pipelineRequest.Method = RequestMethod.Parse(request.Method.Method);
			pipelineRequest.Uri.Reset(request.RequestUri);
			Request request2 = pipelineRequest;
			request2.Content = await ToPipelineRequestContentAsync(request.Content).ConfigureAwait(continueOnCapturedContext: false);
			foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
			{
				foreach (string item in header.Value)
				{
					pipelineRequest.Headers.Add(header.Key, item);
				}
			}
			if (request.Content != null)
			{
				foreach (KeyValuePair<string, IEnumerable<string>> header2 in request.Content.Headers)
				{
					foreach (string item2 in header2.Value)
					{
						pipelineRequest.Headers.Add(header2.Key, item2);
					}
				}
			}
			return pipelineRequest;
		}

		private static HttpResponseMessage ToHttpResponseMessage(Response response)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage
			{
				StatusCode = (HttpStatusCode)response.Status
			};
			if (response.ContentStream != null)
			{
				httpResponseMessage.Content = new StreamContent(response.ContentStream);
			}
			foreach (HttpHeader header in response.Headers)
			{
				if (response.Headers.TryGetValues(header.Name, out IEnumerable<string> values) && !httpResponseMessage.Headers.TryAddWithoutValidation(header.Name, values) && (httpResponseMessage.Content == null || !httpResponseMessage.Content.Headers.TryAddWithoutValidation(header.Name, values)))
				{
					throw new InvalidOperationException("Unable to add header to response or content");
				}
			}
			return httpResponseMessage;
		}

		private static async Task<RequestContent> ToPipelineRequestContentAsync(HttpContent content)
		{
			if (content != null)
			{
				return RequestContent.Create(await content.ReadAsStreamAsync().ConfigureAwait(continueOnCapturedContext: false));
			}
			return null;
		}
	}
	internal static class AppContextSwitchHelper
	{
		public static bool GetConfigValue(string appContexSwitchName, string environmentVariableName)
		{
			if (AppContext.TryGetSwitch(appContexSwitchName, out var isEnabled))
			{
				return isEnabled;
			}
			string environmentVariable = Environment.GetEnvironmentVariable(environmentVariableName);
			if (environmentVariable != null && (environmentVariable.Equals("true", StringComparison.OrdinalIgnoreCase) || environmentVariable.Equals("1")))
			{
				return true;
			}
			return false;
		}
	}
	internal static class Argument
	{
		public static void AssertNotNull<T>(T value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		public static void AssertNotNull<T>(T? value, string name) where T : struct
		{
			if (!value.HasValue)
			{
				throw new ArgumentNullException(name);
			}
		}

		public static void AssertNotNullOrEmpty<T>(IEnumerable<T> value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
			if (value is ICollection<T> { Count: 0 })
			{
				throw new ArgumentException("Value cannot be an empty collection.", name);
			}
			if (value is ICollection { Count: 0 })
			{
				throw new ArgumentException("Value cannot be an empty collection.", name);
			}
			using IEnumerator<T> enumerator = value.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				throw new ArgumentException("Value cannot be an empty collection.", name);
			}
		}

		public static void AssertNotNullOrEmpty(string value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
			if (value.Length == 0)
			{
				throw new ArgumentException("Value cannot be an empty string.", name);
			}
		}

		public static void AssertNotNullOrWhiteSpace(string value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException("Value cannot be empty or contain only white-space characters.", name);
			}
		}

		public static void AssertNotDefault<T>(ref T value, string name) where T : struct, IEquatable<T>
		{
			if (value.Equals(default(T)))
			{
				throw new ArgumentException("Value cannot be empty.", name);
			}
		}

		public static void AssertInRange<T>(T value, T minimum, T maximum, string name) where T : notnull, IComparable<T>
		{
			if (minimum.CompareTo(value) > 0)
			{
				throw new ArgumentOutOfRangeException(name, "Value is less than the minimum allowed.");
			}
			if (maximum.CompareTo(value) < 0)
			{
				throw new ArgumentOutOfRangeException(name, "Value is greater than the maximum allowed.");
			}
		}

		public static void AssertEnumDefined(Type enumType, object value, string name)
		{
			if (!Enum.IsDefined(enumType, value))
			{
				throw new ArgumentException("Value not defined for " + enumType.FullName + ".", name);
			}
		}

		public static T CheckNotNull<T>(T value, string name) where T : class
		{
			AssertNotNull(value, name);
			return value;
		}

		public static string CheckNotNullOrEmpty(string value, string name)
		{
			AssertNotNullOrEmpty(value, name);
			return value;
		}

		public static void AssertNull<T>(T value, string name, string? message = null)
		{
			if (value != null)
			{
				throw new ArgumentException(message ?? "Value must be null.", name);
			}
		}
	}
	internal sealed class ArrayBufferWriter<T> : IBufferWriter<T>
	{
		private T[] _buffer;

		private const int DefaultInitialBufferSize = 256;

		public ReadOnlyMemory<T> WrittenMemory => MemoryExtensions.AsMemory(_buffer, 0, WrittenCount);

		public ReadOnlySpan<T> WrittenSpan => MemoryExtensions.AsSpan(_buffer, 0, WrittenCount);

		public int WrittenCount { get; private set; }

		public int Capacity => _buffer.Length;

		public int FreeCapacity => _buffer.Length - WrittenCount;

		public ArrayBufferWriter()
		{
			_buffer = Array.Empty<T>();
			WrittenCount = 0;
		}

		public ArrayBufferWriter(int initialCapacity)
		{
			if (initialCapacity <= 0)
			{
				throw new ArgumentException("initialCapacity");
			}
			_buffer = new T[initialCapacity];
			WrittenCount = 0;
		}

		public void Clear()
		{
			MemoryExtensions.AsSpan(_buffer, 0, WrittenCount).Clear();
			WrittenCount = 0;
		}

		public void Advance(int count)
		{
			if (count < 0)
			{
				throw new ArgumentException("count");
			}
			if (WrittenCount > _buffer.Length - count)
			{
				ThrowInvalidOperationException_AdvancedTooFar(_buffer.Length);
			}
			WrittenCount += count;
		}

		public Memory<T> GetMemory(int sizeHint = 0)
		{
			CheckAndResizeBuffer(sizeHint);
			return MemoryExtensions.AsMemory(_buffer, WrittenCount);
		}

		public Span<T> GetSpan(int sizeHint = 0)
		{
			CheckAndResizeBuffer(sizeHint);
			return MemoryExtensions.AsSpan(_buffer, WrittenCount);
		}

		private void CheckAndResizeBuffer(int sizeHint)
		{
			if (sizeHint < 0)
			{
				throw new ArgumentException("sizeHint");
			}
			if (sizeHint == 0)
			{
				sizeHint = 1;
			}
			if (sizeHint > FreeCapacity)
			{
				int num = Math.Max(sizeHint, _buffer.Length);
				if (_buffer.Length == 0)
				{
					num = Math.Max(num, 256);
				}
				int newSize = checked(_buffer.Length + num);
				Array.Resize(ref _buffer, newSize);
			}
		}

		private static void ThrowInvalidOperationException_AdvancedTooFar(int capacity)
		{
			throw new InvalidOperationException($"Advanced past capacity of {capacity}");
		}
	}
	internal sealed class AsyncLockWithValue<T>
	{
		public readonly struct LockOrValue : IDisposable
		{
			private readonly Azure.Core.AsyncLockWithValue<T>? _owner;

			private readonly T? _value;

			private readonly long _index;

			public bool HasValue => _owner == null;

			public T Value
			{
				get
				{
					if (!HasValue)
					{
						throw new InvalidOperationException("Value isn't set");
					}
					return _value;
				}
			}

			public LockOrValue(T value)
			{
				_owner = null;
				_value = value;
				_index = 0L;
			}

			public LockOrValue(Azure.Core.AsyncLockWithValue<T> owner, long index)
			{
				_owner = owner;
				_index = index;
				_value = default(T);
			}

			public void SetValue(T value)
			{
				if (_owner != null)
				{
					_owner.SetValue(value, in _index);
					return;
				}
				throw new InvalidOperationException("Value for the lock is set already");
			}

			public void Dispose()
			{
				_owner?.Reset(in _index);
			}
		}

		private readonly object _syncObj = new object();

		private Queue<TaskCompletionSource<LockOrValue>>? _waiters;

		private bool _isLocked;

		private bool _hasValue;

		private long _index;

		private T? _value;

		public bool HasValue
		{
			get
			{
				lock (_syncObj)
				{
					return _hasValue;
				}
			}
		}

		public AsyncLockWithValue()
		{
		}

		public AsyncLockWithValue(T value)
		{
			_hasValue = true;
			_value = value;
		}

		public bool TryGetValue(out T? value)
		{
			lock (_syncObj)
			{
				if (_hasValue)
				{
					value = _value;
					return true;
				}
			}
			value = default(T);
			return false;
		}

		public async ValueTask<LockOrValue> GetLockOrValueAsync(bool async, CancellationToken cancellationToken = default(CancellationToken))
		{
			TaskCompletionSource<LockOrValue> valueTcs;
			lock (_syncObj)
			{
				if (_hasValue)
				{
					return new LockOrValue(_value);
				}
				if (!_isLocked)
				{
					_isLocked = true;
					_index++;
					return new LockOrValue(this, _index);
				}
				cancellationToken.ThrowIfCancellationRequested();
				if (_waiters == null)
				{
					_waiters = new Queue<TaskCompletionSource<LockOrValue>>();
				}
				valueTcs = new TaskCompletionSource<LockOrValue>(async ? TaskCreationOptions.RunContinuationsAsynchronously : TaskCreationOptions.None);
				_waiters.Enqueue(valueTcs);
			}
			try
			{
				if (async)
				{
					return await valueTcs.Task.AwaitWithCancellation(cancellationToken);
				}
				valueTcs.Task.Wait(cancellationToken);
				return valueTcs.Task.EnsureCompleted();
			}
			catch (OperationCanceledException)
			{
				if (valueTcs.TrySetCanceled(cancellationToken))
				{
					throw;
				}
				return valueTcs.Task.Result;
			}
		}

		private void SetValue(T value, in long lockIndex)
		{
			Queue<TaskCompletionSource<LockOrValue>> waiters;
			lock (_syncObj)
			{
				if (lockIndex != _index)
				{
					throw new InvalidOperationException(string.Format("Disposed {0} tries to set value. Current index: {1}, {2} index: {3}", "LockOrValue", _index, "LockOrValue", lockIndex));
				}
				_value = value;
				_hasValue = true;
				_index = 0L;
				_isLocked = false;
				if (_waiters == null)
				{
					return;
				}
				waiters = _waiters;
				_waiters = null;
			}
			while (waiters.Count > 0)
			{
				waiters.Dequeue().TrySetResult(new LockOrValue(value));
			}
		}

		private void Reset(in long lockIndex)
		{
			UnlockOrGetNextWaiter(in lockIndex, out TaskCompletionSource<LockOrValue> nextWaiter);
			while (nextWaiter != null && !nextWaiter.TrySetResult(new LockOrValue(this, lockIndex + 1)))
			{
				UnlockOrGetNextWaiter(in lockIndex, out nextWaiter);
			}
		}

		private void UnlockOrGetNextWaiter(in long lockIndex, out TaskCompletionSource<LockOrValue>? nextWaiter)
		{
			lock (_syncObj)
			{
				nextWaiter = null;
				if (!_isLocked || lockIndex != _index)
				{
					return;
				}
				_index = lockIndex + 1;
				if (_waiters == null)
				{
					_isLocked = false;
					return;
				}
				while (_waiters.Count > 0)
				{
					nextWaiter = _waiters.Dequeue();
					if (!nextWaiter.Task.IsCompleted)
					{
						return;
					}
				}
				_isLocked = false;
			}
		}
	}
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	internal class AzureResourceProviderNamespaceAttribute : Attribute
	{
		public string ResourceProviderNamespace { get; }

		public AzureResourceProviderNamespaceAttribute(string resourceProviderNamespace)
		{
			ResourceProviderNamespace = resourceProviderNamespace;
		}
	}
	internal static class Base64Url
	{
		public static byte[] Decode(string encoded)
		{
			encoded = new StringBuilder(encoded).Replace('-', '+').Replace('_', '/').Append('=', (encoded.Length % 4 != 0) ? (4 - encoded.Length % 4) : 0)
				.ToString();
			return Convert.FromBase64String(encoded);
		}

		public static string Encode(byte[] bytes)
		{
			return new StringBuilder(Convert.ToBase64String(bytes)).Replace('+', '-').Replace('/', '_').Replace("=", "")
				.ToString();
		}

		internal static string DecodeString(string encoded)
		{
			return Encoding.UTF8.GetString(Decode(encoded));
		}

		internal static string EncodeString(string value)
		{
			return Encode(Encoding.UTF8.GetBytes(value));
		}
	}
	internal class HttpMessageSanitizer
	{
		private const string LogAllValue = "*";

		private readonly bool _logAllHeaders;

		private readonly bool _logFullQueries;

		private readonly string[] _allowedQueryParameters;

		private readonly string _redactedPlaceholder;

		private readonly HashSet<string> _allowedHeaders;

		[ThreadStatic]
		private static StringBuilder? s_cachedStringBuilder;

		private const int MaxCachedStringBuilderCapacity = 1024;

		internal static Azure.Core.HttpMessageSanitizer Default = new Azure.Core.HttpMessageSanitizer(Array.Empty<string>(), Array.Empty<string>());

		public HttpMessageSanitizer(string[] allowedQueryParameters, string[] allowedHeaders, string redactedPlaceholder = "REDACTED")
		{
			_logAllHeaders = allowedHeaders.Contains<string>("*");
			_logFullQueries = allowedQueryParameters.Contains<string>("*");
			_allowedQueryParameters = allowedQueryParameters;
			_redactedPlaceholder = redactedPlaceholder;
			_allowedHeaders = new HashSet<string>(allowedHeaders, StringComparer.InvariantCultureIgnoreCase);
		}

		public string SanitizeHeader(string name, string value)
		{
			if (_logAllHeaders || _allowedHeaders.Contains(name))
			{
				return value;
			}
			return _redactedPlaceholder;
		}

		public string SanitizeUrl(string url)
		{
			if (_logFullQueries)
			{
				return url;
			}
			int num = url.IndexOf('?');
			if (num == -1)
			{
				return url;
			}
			StringBuilder stringBuilder = null;
			int num2 = num + 1;
			ReadOnlySpan<char> span = MemoryExtensions.AsSpan(url, num + 1);
			while (span.Length > 0)
			{
				int num3 = span.IndexOf('&');
				int num4 = span.IndexOf('=');
				bool flag = false;
				if ((num3 == -1 && num4 == -1) || (num3 != -1 && (num4 == -1 || num4 > num3)))
				{
					num4 = num3;
					flag = true;
				}
				if (num4 == -1)
				{
					num4 = span.Length;
				}
				num3 = ((num3 != -1) ? (num3 + 1) : span.Length);
				ReadOnlySpan<char> span2 = span.Slice(0, num4);
				bool flag2 = false;
				string[] allowedQueryParameters = _allowedQueryParameters;
				foreach (string text in allowedQueryParameters)
				{
					if (MemoryExtensions.Equals(span2, MemoryExtensions.AsSpan(text), StringComparison.OrdinalIgnoreCase))
					{
						flag2 = true;
						break;
					}
				}
				int num5 = num3;
				int length = num4;
				if (flag2 || flag)
				{
					if (stringBuilder == null)
					{
						num2 += num5;
					}
					else
					{
						AppendReadOnlySpan(stringBuilder, span.Slice(0, num5));
					}
				}
				else
				{
					if (stringBuilder == null)
					{
						stringBuilder = RentStringBuilder(url.Length).Append(url, 0, num2);
					}
					AppendReadOnlySpan(stringBuilder, span.Slice(0, length)).Append('=').Append(_redactedPlaceholder);
					if (span[num3 - 1] == '&')
					{
						stringBuilder.Append('&');
					}
				}
				span = span.Slice(num5);
			}
			if (stringBuilder != null)
			{
				return ToStringAndReturnStringBuilder(stringBuilder);
			}
			return url;
			static StringBuilder AppendReadOnlySpan(StringBuilder builder, ReadOnlySpan<char> readOnlySpan2)
			{
				ReadOnlySpan<char> readOnlySpan = readOnlySpan2;
				for (int j = 0; j < readOnlySpan.Length; j++)
				{
					char value = readOnlySpan[j];
					builder.Append(value);
				}
				return builder;
			}
		}

		private static StringBuilder RentStringBuilder(int capacity)
		{
			if (capacity <= 1024)
			{
				StringBuilder stringBuilder = s_cachedStringBuilder;
				if (stringBuilder != null && stringBuilder.Capacity >= capacity)
				{
					s_cachedStringBuilder = null;
					return stringBuilder;
				}
			}
			return new StringBuilder(capacity);
		}

		private static string ToStringAndReturnStringBuilder(StringBuilder builder)
		{
			string result = builder.ToString();
			if (builder.Capacity <= 1024)
			{
				s_cachedStringBuilder = builder.Clear();
			}
			return result;
		}
	}
	internal static class LightweightPkcs8Decoder
	{
		private static readonly byte[] s_derIntegerZero = new byte[3] { 2, 1, 0 };

		private static readonly byte[] s_rsaAlgorithmId = new byte[15]
		{
			48, 13, 6, 9, 42, 134, 72, 134, 247, 13,
			1, 1, 1, 5, 0
		};

		internal static byte[] ReadBitString(byte[] data, ref int offset)
		{
			if (data[offset++] != 3)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			int num = ReadLength(data, ref offset);
			if (num == 0)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			int num2 = data[offset++];
			if (num2 > 7)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			Span<byte> span = MemoryExtensions.AsSpan(data, offset, num - 1);
			int num3 = -1 << num2;
			byte b = (byte)(span[span.Length - 1] & num3);
			byte[] array = new byte[span.Length];
			Buffer.BlockCopy(data, offset, array, 0, span.Length);
			array[span.Length - 1] = b;
			offset += span.Length;
			return array;
		}

		internal static string ReadObjectIdentifier(byte[] data, ref int offset)
		{
			if (data[offset++] != 6)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			int num = ReadLength(data, ref offset);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = offset; i < offset + num; i++)
			{
				byte b = data[i];
				if (i == offset)
				{
					byte value;
					if (b < 40)
					{
						value = 0;
					}
					else
					{
						if (b >= 80)
						{
							throw new InvalidDataException("Unsupported PKCS#8 Data");
						}
						value = 1;
						b -= 40;
					}
					stringBuilder.Append(value).Append('.').Append(b);
					continue;
				}
				if (b < 128)
				{
					stringBuilder.Append('.').Append(b);
					continue;
				}
				stringBuilder.Append('.');
				if (b == 128)
				{
					throw new InvalidDataException("Invalid PKCS#8 Data");
				}
				int num2 = -1;
				for (int j = i; j < offset + num; j++)
				{
					if ((data[j] & 0x80) == 0)
					{
						num2 = j;
						break;
					}
				}
				if (num2 < 0)
				{
					throw new InvalidDataException("Invalid PKCS#8 Data");
				}
				int num3 = num2 + 1;
				if (num3 <= i + 4)
				{
					int num4 = 0;
					for (int j = i; j < num3; j++)
					{
						b = data[j];
						num4 <<= 7;
						num4 |= (byte)(b & 0x7F);
					}
					stringBuilder.Append(num4);
					i = num2;
					continue;
				}
				throw new InvalidDataException("Unsupported PKCS#8 Data");
			}
			offset += num;
			return stringBuilder.ToString();
		}

		internal static byte[] ReadOctetString(byte[] data, ref int offset)
		{
			if (data[offset++] != 4)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			int num = ReadLength(data, ref offset);
			byte[] array = new byte[num];
			Buffer.BlockCopy(data, offset, array, 0, num);
			offset += num;
			return array;
		}

		private static int ReadLength(byte[] data, ref int offset)
		{
			byte b = data[offset++];
			if (b < 128)
			{
				return b;
			}
			int num = b & 0x7F;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				num2 <<= 8;
				num2 |= data[offset++];
				if (num2 > 65535)
				{
					throw new InvalidDataException("Invalid PKCS#8 Data");
				}
			}
			return num2;
		}

		private static byte[] ReadUnsignedInteger(byte[] data, ref int offset, int targetSize = 0)
		{
			if (data[offset++] != 2)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			int num = ReadLength(data, ref offset);
			if (num < 1 || data[offset] >= 128)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			byte[] array;
			if (num == 1)
			{
				array = new byte[num];
				array[0] = data[offset++];
				return array;
			}
			if (data[offset] == 0)
			{
				offset++;
				num--;
			}
			if (targetSize != 0)
			{
				if (num > targetSize)
				{
					throw new InvalidDataException("Invalid PKCS#8 Data");
				}
				array = new byte[targetSize];
			}
			else
			{
				array = new byte[num];
			}
			Buffer.BlockCopy(data, offset, array, array.Length - num, num);
			offset += num;
			return array;
		}

		private static int ReadPayloadTagLength(byte[] data, ref int offset, byte tagValue)
		{
			if (data[offset++] != tagValue)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			return ReadLength(data, ref offset);
		}

		private static void ConsumeFullPayloadTag(byte[] data, ref int offset, byte tagValue)
		{
			if (data[offset++] != tagValue)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			int num = ReadLength(data, ref offset);
			if (data.Length - offset != num)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
		}

		private static void ConsumeMatch(byte[] data, ref int offset, byte[] toMatch)
		{
			if (data.Length - offset > toMatch.Length && data.Skip(offset).Take(toMatch.Length).SequenceEqual(toMatch))
			{
				offset += toMatch.Length;
				return;
			}
			throw new InvalidDataException("Invalid PKCS#8 Data");
		}

		public static RSA DecodeRSAPkcs8(byte[] pkcs8Bytes)
		{
			int offset = 0;
			ConsumeFullPayloadTag(pkcs8Bytes, ref offset, 48);
			ConsumeMatch(pkcs8Bytes, ref offset, s_derIntegerZero);
			ConsumeMatch(pkcs8Bytes, ref offset, s_rsaAlgorithmId);
			ConsumeFullPayloadTag(pkcs8Bytes, ref offset, 4);
			ConsumeFullPayloadTag(pkcs8Bytes, ref offset, 48);
			ConsumeMatch(pkcs8Bytes, ref offset, s_derIntegerZero);
			RSAParameters parameters = default(RSAParameters);
			parameters.Modulus = ReadUnsignedInteger(pkcs8Bytes, ref offset);
			parameters.Exponent = ReadUnsignedInteger(pkcs8Bytes, ref offset);
			parameters.D = ReadUnsignedInteger(pkcs8Bytes, ref offset, parameters.Modulus.Length);
			int targetSize = (parameters.Modulus.Length + 1) / 2;
			parameters.P = ReadUnsignedInteger(pkcs8Bytes, ref offset, targetSize);
			parameters.Q = ReadUnsignedInteger(pkcs8Bytes, ref offset, targetSize);
			parameters.DP = ReadUnsignedInteger(pkcs8Bytes, ref offset, targetSize);
			parameters.DQ = ReadUnsignedInteger(pkcs8Bytes, ref offset, targetSize);
			parameters.InverseQ = ReadUnsignedInteger(pkcs8Bytes, ref offset, targetSize);
			if (offset != pkcs8Bytes.Length)
			{
				throw new InvalidDataException("Invalid PKCS#8 Data");
			}
			RSA rSA = RSA.Create();
			rSA.ImportParameters(parameters);
			return rSA;
		}

		public static string DecodePrivateKeyOid(byte[] pkcs8Bytes)
		{
			int offset = 0;
			ConsumeFullPayloadTag(pkcs8Bytes, ref offset, 48);
			ConsumeMatch(pkcs8Bytes, ref offset, s_derIntegerZero);
			ReadPayloadTagLength(pkcs8Bytes, ref offset, 48);
			return ReadObjectIdentifier(pkcs8Bytes, ref offset);
		}
	}
	internal static class PemReader
	{
		private delegate void ImportPrivateKeyDelegate(ReadOnlySpan<byte> blob, out int bytesRead);

		public enum KeyType
		{
			Unknown = -1,
			Auto,
			RSA,
			ECDsa
		}

		public ref struct PemField
		{
			public int Start { get; }

			public ReadOnlySpan<char> Label { get; }

			public ReadOnlySpan<char> Data { get; }

			public int Length { get; }

			internal PemField(int start, ReadOnlySpan<char> label, ReadOnlySpan<char> data, int length)
			{
				Start = start;
				Label = label;
				Data = data;
				Length = length;
			}

			public byte[] FromBase64Data()
			{
				return Convert.FromBase64String(Data.ToString());
			}
		}

		private const string Prolog = "-----BEGIN ";

		private const string Epilog = "-----END ";

		private const string LabelEnd = "-----";

		private const string RSAAlgorithmId = "1.2.840.113549.1.1.1";

		private const string ECDsaAlgorithmId = "1.2.840.10045.2.1";

		private static bool s_rsaInitializedImportPkcs8PrivateKeyMethod;

		private static MethodInfo s_rsaImportPkcs8PrivateKeyMethod;

		private static MethodInfo s_rsaCopyWithPrivateKeyMethod;

		public static X509Certificate2 LoadCertificate(ReadOnlySpan<char> data, byte[] cer = null, KeyType keyType = KeyType.Auto, bool allowCertificateOnly = false, X509KeyStorageFlags keyStorageFlags = X509KeyStorageFlags.DefaultKeySet)
		{
			byte[] array = null;
			PemField field;
			while (TryRead(data, out field))
			{
				if (MemoryExtensions.Equals(field.Label, MemoryExtensions.AsSpan("CERTIFICATE"), StringComparison.Ordinal))
				{
					cer = field.FromBase64Data();
				}
				else if (MemoryExtensions.Equals(field.Label, MemoryExtensions.AsSpan("PRIVATE KEY"), StringComparison.Ordinal))
				{
					array = field.FromBase64Data();
				}
				int num = field.Start + field.Length;
				if (num >= data.Length)
				{
					break;
				}
				data = data.Slice(num);
			}
			if (cer == null)
			{
				throw new InvalidDataException("The certificate is missing the public key");
			}
			if (array == null)
			{
				if (allowCertificateOnly)
				{
					return new X509Certificate2(cer, (string)null, keyStorageFlags);
				}
				throw new InvalidDataException("The certificate is missing the private key");
			}
			if (keyType == KeyType.Auto)
			{
				string text = LightweightPkcs8Decoder.DecodePrivateKeyOid(array);
				KeyType keyType2;
				if (!(text == "1.2.840.113549.1.1.1"))
				{
					if (!(text == "1.2.840.10045.2.1"))
					{
						throw new NotSupportedException("The private key algorithm ID " + text + " is not supported");
					}
					keyType2 = KeyType.ECDsa;
				}
				else
				{
					keyType2 = KeyType.RSA;
				}
				keyType = keyType2;
			}
			if (keyType == KeyType.ECDsa)
			{
				return null ?? throw new NotSupportedException("Reading an ECDsa certificate from a PEM file is not supported");
			}
			return CreateRsaCertificate(cer, array, keyStorageFlags);
		}

		private static X509Certificate2 CreateRsaCertificate(byte[] cer, byte[] key, X509KeyStorageFlags keyStorageFlags)
		{
			if (!s_rsaInitializedImportPkcs8PrivateKeyMethod)
			{
				s_rsaImportPkcs8PrivateKeyMethod = typeof(RSA).GetMethod("ImportPkcs8PrivateKey", BindingFlags.Instance | BindingFlags.Public, null, new Type[2]
				{
					typeof(ReadOnlySpan<byte>),
					typeof(int).MakeByRefType()
				}, null);
				s_rsaInitializedImportPkcs8PrivateKeyMethod = true;
			}
			if ((object)s_rsaCopyWithPrivateKeyMethod == null)
			{
				s_rsaCopyWithPrivateKeyMethod = typeof(RSACertificateExtensions).GetMethod("CopyWithPrivateKey", BindingFlags.Static | BindingFlags.Public, null, new Type[2]
				{
					typeof(X509Certificate2),
					typeof(RSA)
				}, null) ?? throw new PlatformNotSupportedException("The current platform does not support reading a private key from a PEM file");
			}
			RSA rSA = null;
			try
			{
				if (s_rsaImportPkcs8PrivateKeyMethod != null)
				{
					rSA = RSA.Create();
					((ImportPrivateKeyDelegate)s_rsaImportPkcs8PrivateKeyMethod.CreateDelegate(typeof(ImportPrivateKeyDelegate), rSA))(key, out var bytesRead);
					if (key.Length != bytesRead)
					{
						throw new InvalidDataException("Invalid PKCS#8 Data");
					}
				}
				else
				{
					rSA = LightweightPkcs8Decoder.DecodeRSAPkcs8(key);
				}
				using X509Certificate2 x509Certificate = new X509Certificate2(cer, (string)null, keyStorageFlags);
				X509Certificate2 x509Certificate2 = (X509Certificate2)s_rsaCopyWithPrivateKeyMethod.Invoke(null, new object[2] { x509Certificate, rSA });
				if (x509Certificate2.PrivateKey == null)
				{
					x509Certificate2.PrivateKey = rSA;
				}
				rSA = null;
				return x509Certificate2;
			}
			finally
			{
				rSA?.Dispose();
			}
		}

		public static bool TryRead(ReadOnlySpan<char> data, out PemField field)
		{
			field = default(PemField);
			int num = data.IndexOf(MemoryExtensions.AsSpan("-----BEGIN "));
			if (num < 0)
			{
				return false;
			}
			ReadOnlySpan<char> readOnlySpan = data.Slice(num + "-----BEGIN ".Length);
			int num2 = readOnlySpan.IndexOf(MemoryExtensions.AsSpan("-----"));
			if (num2 < 0)
			{
				return false;
			}
			readOnlySpan = readOnlySpan.Slice(0, num2);
			int num3 = num + "-----BEGIN ".Length + num2 + "-----".Length;
			data = data.Slice(num3);
			string text = "-----END " + readOnlySpan.ToString() + "-----";
			num2 = data.IndexOf(MemoryExtensions.AsSpan(text));
			if (num2 < 0)
			{
				return false;
			}
			int length = num3 + num2 + text.Length - num;
			field = new PemField(num, readOnlySpan, data.Slice(0, num2), length);
			return true;
		}
	}
}
namespace Azure.Core.Pipeline
{
	internal class ClientDiagnostics : Azure.Core.Pipeline.DiagnosticScopeFactory
	{
		public ClientDiagnostics(ClientOptions options, bool? suppressNestedClientActivities = null)
			: this(options.GetType().Namespace, GetResourceProviderNamespace(options.GetType().Assembly), options.Diagnostics, suppressNestedClientActivities)
		{
		}

		public ClientDiagnostics(string optionsNamespace, string? providerNamespace, DiagnosticsOptions diagnosticsOptions, bool? suppressNestedClientActivities = null)
			: base(optionsNamespace, providerNamespace, diagnosticsOptions.IsDistributedTracingEnabled, suppressNestedClientActivities ?? true, isStable: true)
		{
		}

		internal static Azure.Core.HttpMessageSanitizer CreateMessageSanitizer(DiagnosticsOptions diagnostics)
		{
			return new Azure.Core.HttpMessageSanitizer(diagnostics.LoggedQueryParameters.ToArray(), diagnostics.LoggedHeaderNames.ToArray());
		}

		internal static string? GetResourceProviderNamespace(Assembly assembly)
		{
			foreach (CustomAttributeData customAttributesDatum in assembly.GetCustomAttributesData())
			{
				if (customAttributesDatum.AttributeType.FullName == "Azure.Core.AzureResourceProviderNamespaceAttribute")
				{
					return customAttributesDatum.ConstructorArguments.Single().Value as string;
				}
			}
			return null;
		}
	}
	internal readonly struct DiagnosticScope : IDisposable
	{
		private class DiagnosticActivity : Activity
		{
			public new IEnumerable<Activity> Links { get; set; } = Array.Empty<Activity>();

			public DiagnosticActivity(string operationName)
				: base(operationName)
			{
			}
		}

		private class ActivityAdapter : IDisposable
		{
			private readonly ActivitySource? _activitySource;

			private readonly DiagnosticSource _diagnosticSource;

			private readonly string _activityName;

			private readonly ActivityKind _kind;

			private readonly object? _diagnosticSourceArgs;

			private Activity? _currentActivity;

			private Activity? _sampleOutActivity;

			private ActivityTagsCollection? _tagCollection;

			private DateTimeOffset _startTime;

			private List<ActivityLink>? _links;

			private string? _traceparent;

			private string? _tracestate;

			private string? _displayName;

			public ActivityAdapter(ActivitySource? activitySource, DiagnosticSource diagnosticSource, string activityName, ActivityKind kind, object? diagnosticSourceArgs)
			{
				_activitySource = activitySource;
				_diagnosticSource = diagnosticSource;
				_activityName = activityName;
				_kind = kind;
				_diagnosticSourceArgs = diagnosticSourceArgs;
			}

			public void AddTag(string name, object value)
			{
				if (_sampleOutActivity != null)
				{
					return;
				}
				if (_currentActivity == null)
				{
					if (_tagCollection == null)
					{
						_tagCollection = new ActivityTagsCollection();
					}
					_tagCollection[name] = value;
				}
				else
				{
					AddObjectTag(name, value);
				}
			}

			private IReadOnlyList<Activity> GetDiagnosticSourceLinkCollection()
			{
				if (_links == null)
				{
					return Array.Empty<Activity>();
				}
				List<Activity> list = new List<Activity>();
				foreach (ActivityLink link in _links)
				{
					Activity activity = new Activity("LinkedActivity");
					activity.SetIdFormat(ActivityIdFormat.W3C);
					if (link.Context != default(ActivityContext))
					{
						activity.SetParentId(ActivityContextToTraceParent(link.Context));
						activity.TraceStateString = link.Context.TraceState;
					}
					if (link.Tags != null)
					{
						foreach (KeyValuePair<string, object> tag in link.Tags)
						{
							if (tag.Value != null)
							{
								activity.AddTag(tag.Key, tag.Value.ToString());
							}
						}
					}
					list.Add(activity);
				}
				return list;
			}

			private static string ActivityContextToTraceParent(ActivityContext context)
			{
				string text = ((context.TraceFlags == ActivityTraceFlags.None) ? "00" : "01");
				return "00-" + context.TraceId.ToString() + "-" + context.SpanId.ToString() + "-" + text;
			}

			public void AddLink(string traceparent, string? tracestate, IDictionary<string, object?>? attributes)
			{
				ActivityContext.TryParse(traceparent, tracestate, out var context);
				ActivityLink item = new ActivityLink(context, (attributes == null) ? null : new ActivityTagsCollection(attributes));
				if (_links == null)
				{
					_links = new List<ActivityLink>();
				}
				_links.Add(item);
			}

			[DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.PublicProperties, typeof(Activity))]
			[DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.PublicProperties, typeof(DiagnosticActivity))]
			public Activity? Start()
			{
				_currentActivity = StartActivitySourceActivity();
				if (_currentActivity != null)
				{
					if (!_currentActivity.IsAllDataRequested)
					{
						_sampleOutActivity = _currentActivity;
						_currentActivity = null;
						return null;
					}
					_currentActivity.SetTag("az.schema_url", "https://opentelemetry.io/schemas/1.23.0");
				}
				else
				{
					if (!_diagnosticSource.IsEnabled(_activityName, _diagnosticSourceArgs))
					{
						return null;
					}
					switch (_kind)
					{
					case ActivityKind.Internal:
						AddTag("kind", "internal");
						break;
					case ActivityKind.Server:
						AddTag("kind", "server");
						break;
					case ActivityKind.Client:
						AddTag("kind", "client");
						break;
					case ActivityKind.Producer:
						AddTag("kind", "producer");
						break;
					case ActivityKind.Consumer:
						AddTag("kind", "consumer");
						break;
					}
					_currentActivity = new DiagnosticActivity(_activityName)
					{
						Links = GetDiagnosticSourceLinkCollection()
					};
					_currentActivity.SetIdFormat(ActivityIdFormat.W3C);
					if (_startTime != default(DateTimeOffset))
					{
						_currentActivity.SetStartTime(_startTime.UtcDateTime);
					}
					if (_tagCollection != null)
					{
						foreach (KeyValuePair<string, object?> item in _tagCollection)
						{
							AddObjectTag(item.Key, item.Value);
						}
					}
					if (_traceparent != null)
					{
						_currentActivity.SetParentId(_traceparent);
					}
					if (_tracestate != null)
					{
						_currentActivity.TraceStateString = _tracestate;
					}
					_currentActivity.Start();
				}
				WriteStartEvent();
				if (_displayName != null)
				{
					_currentActivity.DisplayName = _displayName;
				}
				return _currentActivity;
			}

			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The values being passed into Write have the commonly used properties being preserved with DynamicDependency on the ActivityAdapter.Start() method, or the responsibility is on the user of this struct since the struct constructor is marked with RequiresUnreferencedCode.")]
			private void WriteStartEvent()
			{
				_diagnosticSource.Write(_activityName + ".Start", _diagnosticSourceArgs ?? _currentActivity);
			}

			public void SetDisplayName(string displayName)
			{
				_displayName = displayName;
				if (_currentActivity != null)
				{
					_currentActivity.DisplayName = _displayName;
				}
			}

			private Activity? StartActivitySourceActivity()
			{
				if (_activitySource == null)
				{
					return null;
				}
				ActivityContext.TryParse(_traceparent, _tracestate, out var context);
				return _activitySource.StartActivity(_activityName, _kind, context, _tagCollection, _links, _startTime);
			}

			public void SetStartTime(DateTime startTime)
			{
				_startTime = startTime;
				_currentActivity?.SetStartTime(startTime);
			}

			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The Exception being passed into this method has the commonly used properties being preserved with DynamicallyAccessedMemberTypes.")]
			public void MarkFailed<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(T? exception, string? errorCode)
			{
				if (exception != null)
				{
					_diagnosticSource?.Write(_activityName + ".Exception", exception);
				}
				if (errorCode == null && exception != null)
				{
					errorCode = exception.GetType().FullName;
				}
				if (errorCode == null)
				{
					errorCode = "_OTHER";
				}
				_currentActivity?.SetTag("error.type", errorCode);
				_currentActivity?.SetStatus(ActivityStatusCode.Error, exception?.ToString());
			}

			public void SetTraceContext(string traceparent, string? tracestate)
			{
				if (_currentActivity != null)
				{
					throw new InvalidOperationException("Traceparent can not be set after the activity is started.");
				}
				_traceparent = traceparent;
				_tracestate = tracestate;
			}

			private void AddObjectTag(string name, object value)
			{
				ActivitySource? activitySource = _activitySource;
				if (activitySource != null && activitySource.HasListeners())
				{
					_currentActivity?.SetTag(name, value);
				}
				else
				{
					_currentActivity?.AddTag(name, value.ToString());
				}
			}

			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "The class constructor is marked with RequiresUnreferencedCode.")]
			public void Dispose()
			{
				Activity activity = _currentActivity ?? _sampleOutActivity;
				if (activity != null)
				{
					if (activity.Duration == TimeSpan.Zero)
					{
						activity.SetEndTime(DateTime.UtcNow);
					}
					_diagnosticSource.Write(_activityName + ".Stop", _diagnosticSourceArgs);
					activity.Dispose();
					_currentActivity = null;
					_sampleOutActivity = null;
				}
			}
		}

		private const string AzureSdkScopeLabel = "az.sdk.scope";

		internal const string OpenTelemetrySchemaAttribute = "az.schema_url";

		internal const string OpenTelemetrySchemaVersion = "https://opentelemetry.io/schemas/1.23.0";

		private static readonly object AzureSdkScopeValue = bool.TrueString;

		private readonly ActivityAdapter? _activityAdapter;

		private readonly bool _suppressNestedClientActivities;

		public bool IsEnabled { get; }

		[RequiresUnreferencedCode("The diagnosticSourceArgs are used in a call to DiagnosticSource.Write, all necessary properties need to be preserved on the type being passed in using DynamicDependency attributes.")]
		internal DiagnosticScope(string scopeName, DiagnosticListener source, object? diagnosticSourceArgs, ActivitySource? activitySource, ActivityKind kind, bool suppressNestedClientActivities)
		{
			_suppressNestedClientActivities = (kind == ActivityKind.Client || kind == ActivityKind.Internal) && suppressNestedClientActivities;
			bool flag = activitySource?.HasListeners() ?? false;
			IsEnabled = source.IsEnabled() || flag;
			if (_suppressNestedClientActivities)
			{
				IsEnabled &= !AzureSdkScopeValue.Equals(Activity.Current?.GetCustomProperty("az.sdk.scope"));
			}
			_activityAdapter = (IsEnabled ? new ActivityAdapter(activitySource, source, scopeName, kind, diagnosticSourceArgs) : null);
		}

		public void AddAttribute(string name, string? value)
		{
			if (value != null)
			{
				_activityAdapter?.AddTag(name, value);
			}
		}

		public void AddIntegerAttribute(string name, int value)
		{
			_activityAdapter?.AddTag(name, value);
		}

		public void AddLongAttribute(string name, long value)
		{
			_activityAdapter?.AddTag(name, value);
		}

		public void AddAttribute<T>(string name, T value, Func<T, string> format)
		{
			if (_activityAdapter != null && value != null)
			{
				string value2 = format(value);
				_activityAdapter.AddTag(name, value2);
			}
		}

		public void AddLink(string traceparent, string? tracestate, IDictionary<string, object?>? attributes = null)
		{
			_activityAdapter?.AddLink(traceparent, tracestate, attributes);
		}

		public void Start()
		{
			Activity activity = _activityAdapter?.Start();
			if (_suppressNestedClientActivities)
			{
				activity?.SetCustomProperty("az.sdk.scope", AzureSdkScopeValue);
			}
		}

		public void SetDisplayName(string displayName)
		{
			_activityAdapter?.SetDisplayName(displayName);
		}

		public void SetStartTime(DateTime dateTime)
		{
			_activityAdapter?.SetStartTime(dateTime);
		}

		public void SetTraceContext(string traceparent, string? tracestate = null)
		{
			_activityAdapter?.SetTraceContext(traceparent, tracestate);
		}

		public void Dispose()
		{
			_activityAdapter?.Dispose();
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The Exception being passed into this method has public properties preserved on the inner method MarkFailed.The public property System.Exception.TargetSite.get is not compatible with trimming and produces a warning when preserving all public properties. Since we do not use this property, andneither does Application Insights, we can suppress the warning coming from the inner method.")]
		public void Failed(Exception exception)
		{
			if (exception is RequestFailedException ex)
			{
				string errorCode = (string.IsNullOrEmpty(ex.ErrorCode) ? null : ex.ErrorCode);
				_activityAdapter?.MarkFailed(exception, errorCode);
			}
			else
			{
				_activityAdapter?.MarkFailed(exception, null);
			}
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The public property System.Exception.TargetSite.get is not compatible with trimming and produces a warning when preserving all public properties. Since we do not use this property, and neither does Application Insights, we can suppress the warning coming from the inner method.")]
		public void Failed(string errorCode)
		{
			_activityAdapter?.MarkFailed<Exception>(null, errorCode);
		}
	}
	internal static class ActivityExtensions
	{
		public static bool SupportsActivitySource { get; private set; }

		static ActivityExtensions()
		{
			ResetFeatureSwitch();
		}

		public static void ResetFeatureSwitch()
		{
			SupportsActivitySource = Azure.Core.AppContextSwitchHelper.GetConfigValue("Azure.Experimental.EnableActivitySource", "AZURE_EXPERIMENTAL_ENABLE_ACTIVITY_SOURCE");
		}
	}
	internal class DiagnosticScopeFactory
	{
		private static Dictionary<string, DiagnosticListener>? _listeners;

		private readonly string? _resourceProviderNamespace;

		private readonly DiagnosticListener? _source;

		private readonly bool _suppressNestedClientActivities;

		private readonly bool _isStable;

		private static readonly ConcurrentDictionary<string, ActivitySource?> ActivitySources = new ConcurrentDictionary<string, ActivitySource>();

		public bool IsActivityEnabled { get; }

		public DiagnosticScopeFactory(string clientNamespace, string? resourceProviderNamespace, bool isActivityEnabled, bool suppressNestedClientActivities = true, bool isStable = false)
		{
			_resourceProviderNamespace = resourceProviderNamespace;
			IsActivityEnabled = isActivityEnabled;
			_suppressNestedClientActivities = suppressNestedClientActivities;
			_isStable = isStable;
			if (!IsActivityEnabled)
			{
				return;
			}
			Dictionary<string, DiagnosticListener> dictionary = LazyInitializer.EnsureInitialized(ref _listeners);
			lock (dictionary)
			{
				if (!dictionary.TryGetValue(clientNamespace, out _source))
				{
					_source = new DiagnosticListener(clientNamespace);
					dictionary[clientNamespace] = _source;
				}
			}
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "The DiagnosticScope constructor is marked as RequiresUnreferencedCode because of the usage of the diagnosticSourceArgs parameter. Since we are passing in null here we can suppress this warning.")]
		public Azure.Core.Pipeline.DiagnosticScope CreateScope(string name, ActivityKind kind = ActivityKind.Internal)
		{
			if (_source == null)
			{
				return default(Azure.Core.Pipeline.DiagnosticScope);
			}
			Azure.Core.Pipeline.DiagnosticScope result = new Azure.Core.Pipeline.DiagnosticScope(name, _source, null, GetActivitySource(_source.Name, name), kind, _suppressNestedClientActivities);
			if (_resourceProviderNamespace != null)
			{
				result.AddAttribute("az.namespace", _resourceProviderNamespace);
			}
			return result;
		}

		private ActivitySource? GetActivitySource(string ns, string name)
		{
			if (!(_isStable | Azure.Core.Pipeline.ActivityExtensions.SupportsActivitySource))
			{
				return null;
			}
			int num = name.IndexOf(".", StringComparison.OrdinalIgnoreCase);
			string key = ns + "." + ((num < 0) ? name : name.Substring(0, num));
			return ActivitySources.GetOrAdd(key, (string n) => new ActivitySource(n));
		}
	}
	internal static class TaskExtensions
	{
		public readonly struct Enumerable<T> : IEnumerable<T>, IEnumerable
		{
			private readonly IAsyncEnumerable<T> _asyncEnumerable;

			public Enumerable(IAsyncEnumerable<T> asyncEnumerable)
			{
				_asyncEnumerable = asyncEnumerable;
			}

			public Enumerator<T> GetEnumerator()
			{
				return new Enumerator<T>(_asyncEnumerable.GetAsyncEnumerator());
			}

			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return new Enumerator<T>(_asyncEnumerable.GetAsyncEnumerator());
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		public readonly struct Enumerator<T> : IEnumerator<T>, IEnumerator, IDisposable
		{
			private readonly IAsyncEnumerator<T> _asyncEnumerator;

			public T Current => _asyncEnumerator.Current;

			object IEnumerator.Current => Current;

			public Enumerator(IAsyncEnumerator<T> asyncEnumerator)
			{
				_asyncEnumerator = asyncEnumerator;
			}

			public bool MoveNext()
			{
				return _asyncEnumerator.MoveNextAsync().EnsureCompleted();
			}

			public void Reset()
			{
				throw new NotSupportedException($"{GetType()} is a synchronous wrapper for {_asyncEnumerator.GetType()} async enumerator, which can't be reset, so IEnumerable.Reset() calls aren't supported.");
			}

			public void Dispose()
			{
				_asyncEnumerator.DisposeAsync().EnsureCompleted();
			}
		}

		public readonly struct WithCancellationTaskAwaitable
		{
			private readonly CancellationToken _cancellationToken;

			private readonly ConfiguredTaskAwaitable _awaitable;

			public WithCancellationTaskAwaitable(Task task, CancellationToken cancellationToken)
			{
				_awaitable = task.ConfigureAwait(continueOnCapturedContext: false);
				_cancellationToken = cancellationToken;
			}

			public WithCancellationTaskAwaiter GetAwaiter()
			{
				ConfiguredTaskAwaitable awaitable = _awaitable;
				return new WithCancellationTaskAwaiter(awaitable.GetAwaiter(), _cancellationToken);
			}
		}

		public readonly struct WithCancellationTaskAwaitable<T>
		{
			private readonly CancellationToken _cancellationToken;

			private readonly ConfiguredTaskAwaitable<T> _awaitable;

			public WithCancellationTaskAwaitable(Task<T> task, CancellationToken cancellationToken)
			{
				_awaitable = task.ConfigureAwait(continueOnCapturedContext: false);
				_cancellationToken = cancellationToken;
			}

			public WithCancellationTaskAwaiter<T> GetAwaiter()
			{
				ConfiguredTaskAwaitable<T> awaitable = _awaitable;
				return new WithCancellationTaskAwaiter<T>(awaitable.GetAwaiter(), _cancellationToken);
			}
		}

		public readonly struct WithCancellationValueTaskAwaitable<T>
		{
			private readonly CancellationToken _cancellationToken;

			private readonly ConfiguredValueTaskAwaitable<T> _awaitable;

			public WithCancellationValueTaskAwaitable(ValueTask<T> task, CancellationToken cancellationToken)
			{
				_awaitable = task.ConfigureAwait(continueOnCapturedContext: false);
				_cancellationToken = cancellationToken;
			}

			public WithCancellationValueTaskAwaiter<T> GetAwaiter()
			{
				return new WithCancellationValueTaskAwaiter<T>(_awaitable.GetAwaiter(), _cancellationToken);
			}
		}

		public readonly struct WithCancellationTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			private readonly CancellationToken _cancellationToken;

			private readonly ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _taskAwaiter;

			public bool IsCompleted
			{
				get
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter taskAwaiter = _taskAwaiter;
					if (!taskAwaiter.IsCompleted)
					{
						CancellationToken cancellationToken = _cancellationToken;
						return cancellationToken.IsCancellationRequested;
					}
					return true;
				}
			}

			public WithCancellationTaskAwaiter(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter, CancellationToken cancellationToken)
			{
				_taskAwaiter = awaiter;
				_cancellationToken = cancellationToken;
			}

			public void OnCompleted(Action continuation)
			{
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter taskAwaiter = _taskAwaiter;
				taskAwaiter.OnCompleted(WrapContinuation(in continuation));
			}

			public void UnsafeOnCompleted(Action continuation)
			{
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter taskAwaiter = _taskAwaiter;
				taskAwaiter.UnsafeOnCompleted(WrapContinuation(in continuation));
			}

			public void GetResult()
			{
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter taskAwaiter = _taskAwaiter;
				if (!taskAwaiter.IsCompleted)
				{
					CancellationToken cancellationToken = _cancellationToken;
					cancellationToken.ThrowIfCancellationRequested();
				}
				taskAwaiter = _taskAwaiter;
				taskAwaiter.GetResult();
			}

			private Action WrapContinuation(in Action originalContinuation)
			{
				CancellationToken cancellationToken = _cancellationToken;
				if (!cancellationToken.CanBeCanceled)
				{
					return originalContinuation;
				}
				return new WithCancellationContinuationWrapper(originalContinuation, _cancellationToken).Continuation;
			}
		}

		public readonly struct WithCancellationTaskAwaiter<T> : ICriticalNotifyCompletion, INotifyCompletion
		{
			private readonly CancellationToken _cancellationToken;

			private readonly ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter _taskAwaiter;

			public bool IsCompleted
			{
				get
				{
					ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter taskAwaiter = _taskAwaiter;
					if (!taskAwaiter.IsCompleted)
					{
						CancellationToken cancellationToken = _cancellationToken;
						return cancellationToken.IsCancellationRequested;
					}
					return true;
				}
			}

			public WithCancellationTaskAwaiter(ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter awaiter, CancellationToken cancellationToken)
			{
				_taskAwaiter = awaiter;
				_cancellationToken = cancellationToken;
			}

			public void OnCompleted(Action continuation)
			{
				ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter taskAwaiter = _taskAwaiter;
				taskAwaiter.OnCompleted(WrapContinuation(in continuation));
			}

			public void UnsafeOnCompleted(Action continuation)
			{
				ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter taskAwaiter = _taskAwaiter;
				taskAwaiter.UnsafeOnCompleted(WrapContinuation(in continuation));
			}

			public T GetResult()
			{
				ConfiguredTaskAwaitable<T>.ConfiguredTaskAwaiter taskAwaiter = _taskAwaiter;
				if (!taskAwaiter.IsCompleted)
				{
					CancellationToken cancellationToken = _cancellationToken;
					cancellationToken.ThrowIfCancellationRequested();
				}
				taskAwaiter = _taskAwaiter;
				return taskAwaiter.GetResult();
			}

			private Action WrapContinuation(in Action originalContinuation)
			{
				CancellationToken cancellationToken = _cancellationToken;
				if (!cancellationToken.CanBeCanceled)
				{
					return originalContinuation;
				}
				return new WithCancellationContinuationWrapper(originalContinuation, _cancellationToken).Continuation;
			}
		}

		public readonly struct WithCancellationValueTaskAwaiter<T> : ICriticalNotifyCompletion, INotifyCompletion
		{
			private readonly CancellationToken _cancellationToken;

			private readonly ConfiguredValueTaskAwaitable<T>.ConfiguredValueTaskAwaiter _taskAwaiter;

			public bool IsCompleted
			{
				get
				{
					if (!_taskAwaiter.IsCompleted)
					{
						CancellationToken cancellationToken = _cancellationToken;
						return cancellationToken.IsCancellationRequested;
					}
					return true;
				}
			}

			public WithCancellationValueTaskAwaiter(ConfiguredValueTaskAwaitable<T>.ConfiguredValueTaskAwaiter awaiter, CancellationToken cancellationToken)
			{
				_taskAwaiter = awaiter;
				_cancellationToken = cancellationToken;
			}

			public void OnCompleted(Action continuation)
			{
				_taskAwaiter.OnCompleted(WrapContinuation(in continuation));
			}

			public void UnsafeOnCompleted(Action continuation)
			{
				_taskAwaiter.UnsafeOnCompleted(WrapContinuation(in continuation));
			}

			public T GetResult()
			{
				if (!_taskAwaiter.IsCompleted)
				{
					CancellationToken cancellationToken = _cancellationToken;
					cancellationToken.ThrowIfCancellationRequested();
				}
				return _taskAwaiter.GetResult();
			}

			private Action WrapContinuation(in Action originalContinuation)
			{
				CancellationToken cancellationToken = _cancellationToken;
				if (!cancellationToken.CanBeCanceled)
				{
					return originalContinuation;
				}
				return new WithCancellationContinuationWrapper(originalContinuation, _cancellationToken).Continuation;
			}
		}

		private class WithCancellationContinuationWrapper
		{
			private Action _originalContinuation;

			private readonly CancellationTokenRegistration _registration;

			public Action Continuation { get; }

			public WithCancellationContinuationWrapper(Action originalContinuation, CancellationToken cancellationToken)
			{
				Action callback = ContinuationImplementation;
				_originalContinuation = originalContinuation;
				_registration = cancellationToken.Register(callback);
				Continuation = callback;
			}

			private void ContinuationImplementation()
			{
				Action action = Interlocked.Exchange(ref _originalContinuation, null);
				if (action != null)
				{
					CancellationTokenRegistration registration = _registration;
					registration.Dispose();
					action();
				}
			}
		}

		public static WithCancellationTaskAwaitable AwaitWithCancellation(this Task task, CancellationToken cancellationToken)
		{
			return new WithCancellationTaskAwaitable(task, cancellationToken);
		}

		public static WithCancellationTaskAwaitable<T> AwaitWithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
		{
			return new WithCancellationTaskAwaitable<T>(task, cancellationToken);
		}

		public static WithCancellationValueTaskAwaitable<T> AwaitWithCancellation<T>(this ValueTask<T> task, CancellationToken cancellationToken)
		{
			return new WithCancellationValueTaskAwaitable<T>(task, cancellationToken);
		}

		public static T EnsureCompleted<T>(this Task<T> task)
		{
			return task.GetAwaiter().GetResult();
		}

		public static void EnsureCompleted(this Task task)
		{
			task.GetAwaiter().GetResult();
		}

		public static T EnsureCompleted<T>(this ValueTask<T> task)
		{
			return task.GetAwaiter().GetResult();
		}

		public static void EnsureCompleted(this ValueTask task)
		{
			task.GetAwaiter().GetResult();
		}

		public static Enumerable<T> EnsureSyncEnumerable<T>(this IAsyncEnumerable<T> asyncEnumerable)
		{
			return new Enumerable<T>(asyncEnumerable);
		}

		public static ConfiguredValueTaskAwaitable<T> EnsureCompleted<T>(this ConfiguredValueTaskAwaitable<T> awaitable, bool async)
		{
			return awaitable;
		}

		public static ConfiguredValueTaskAwaitable EnsureCompleted(this ConfiguredValueTaskAwaitable awaitable, bool async)
		{
			return awaitable;
		}

		[Conditional("DEBUG")]
		private static void VerifyTaskCompleted(bool isCompleted)
		{
			if (!isCompleted)
			{
				if (Debugger.IsAttached)
				{
					Debugger.Break();
				}
				throw new InvalidOperationException("Task is not completed");
			}
		}
	}
}
namespace Azure.Core.Diagnostics
{
	internal abstract class AzureEventSource : EventSource
	{
		private const string SharedDataKey = "_AzureEventSourceNamesInUse";

		private static readonly HashSet<string> NamesInUse;

		private static readonly string[] MainEventSourceTraits;

		static AzureEventSource()
		{
			MainEventSourceTraits = new string[2] { "AzureEventSource", "true" };
			HashSet<string> hashSet = AppDomain.CurrentDomain.GetData("_AzureEventSourceNamesInUse") as HashSet<string>;
			if (hashSet == null)
			{
				hashSet = new HashSet<string>();
				AppDomain.CurrentDomain.SetData("_AzureEventSourceNamesInUse", hashSet);
			}
			NamesInUse = hashSet;
		}

		protected AzureEventSource(string eventSourceName)
			: base(DeduplicateName(eventSourceName), EventSourceSettings.Default, MainEventSourceTraits)
		{
		}

		private static string DeduplicateName(string eventSourceName)
		{
			try
			{
				lock (NamesInUse)
				{
					foreach (EventSource source in EventSource.GetSources())
					{
						NamesInUse.Add(source.Name);
					}
					if (!NamesInUse.Contains(eventSourceName))
					{
						NamesInUse.Add(eventSourceName);
						return eventSourceName;
					}
					int num = 1;
					string text;
					while (true)
					{
						text = $"{eventSourceName}-{num}";
						if (!NamesInUse.Contains(text))
						{
							break;
						}
						num++;
					}
					NamesInUse.Add(text);
					return text;
				}
			}
			catch (NotImplementedException)
			{
				return eventSourceName;
			}
		}
	}
}
namespace Azure.Identity
{
	internal static class AbstractAcquireTokenParameterBuilderExtensions
	{
		public static async ValueTask<AuthenticationResult> ExecuteAsync<T>(this AbstractAcquireTokenParameterBuilder<T> builder, bool async, CancellationToken cancellationToken) where T : AbstractAcquireTokenParameterBuilder<T>
		{
			return (!async) ? builder.ExecuteAsync(cancellationToken).GetAwaiter().GetResult() : (await builder.ExecuteAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}
	}
	internal class AppServiceManagedIdentitySource : ManagedIdentitySource
	{
		private const string MsiEndpointInvalidUriError = "The environment variable MSI_ENDPOINT contains an invalid Uri.";

		private readonly Uri _endpoint;

		private readonly string _secret;

		private readonly string _clientId;

		private readonly string _resourceId;

		protected virtual string AppServiceMsiApiVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		protected virtual string SecretHeaderName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		protected virtual string ClientIdHeaderName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		protected static bool TryValidateEnvVars(string msiEndpoint, string secret, out Uri endpointUri)
		{
			endpointUri = null;
			if (string.IsNullOrEmpty(msiEndpoint) || string.IsNullOrEmpty(secret))
			{
				return false;
			}
			try
			{
				endpointUri = new Uri(msiEndpoint);
			}
			catch (FormatException innerException)
			{
				throw new AuthenticationFailedException("The environment variable MSI_ENDPOINT contains an invalid Uri.", innerException);
			}
			return true;
		}

		protected AppServiceManagedIdentitySource(CredentialPipeline pipeline, Uri endpoint, string secret, ManagedIdentityClientOptions options)
			: base(pipeline)
		{
			_endpoint = endpoint;
			_secret = secret;
			_clientId = options.ClientId;
			_resourceId = options.ResourceIdentifier?.ToString();
		}

		protected override Request CreateRequest(string[] scopes)
		{
			string value = ScopeUtilities.ScopesToResource(scopes);
			Request request = base.Pipeline.HttpPipeline.CreateRequest();
			request.Method = RequestMethod.Get;
			request.Headers.Add(SecretHeaderName, _secret);
			request.Uri.Reset(_endpoint);
			request.Uri.AppendQuery("api-version", AppServiceMsiApiVersion);
			request.Uri.AppendQuery("resource", value);
			if (!string.IsNullOrEmpty(_clientId))
			{
				request.Uri.AppendQuery(ClientIdHeaderName, _clientId);
			}
			if (!string.IsNullOrEmpty(_resourceId))
			{
				request.Uri.AppendQuery("mi_res_id", _resourceId);
			}
			return request;
		}
	}
	internal class AppServiceV2017ManagedIdentitySource : AppServiceManagedIdentitySource
	{
		protected override string AppServiceMsiApiVersion => "2017-09-01";

		protected override string SecretHeaderName => "secret";

		protected override string ClientIdHeaderName => "clientid";

		public static ManagedIdentitySource TryCreate(ManagedIdentityClientOptions options)
		{
			string msiSecret = EnvironmentVariables.MsiSecret;
			if (!AppServiceManagedIdentitySource.TryValidateEnvVars(EnvironmentVariables.MsiEndpoint, msiSecret, out var endpointUri))
			{
				return null;
			}
			return new AppServiceV2017ManagedIdentitySource(options.Pipeline, endpointUri, msiSecret, options);
		}

		internal AppServiceV2017ManagedIdentitySource(CredentialPipeline pipeline, Uri endpoint, string secret, ManagedIdentityClientOptions options)
			: base(pipeline, endpoint, secret, options)
		{
		}
	}
	internal class AppServiceV2019ManagedIdentitySource : AppServiceManagedIdentitySource
	{
		protected override string AppServiceMsiApiVersion => "2019-08-01";

		protected override string SecretHeaderName => "X-IDENTITY-HEADER";

		protected override string ClientIdHeaderName => "client_id";

		public static ManagedIdentitySource TryCreate(ManagedIdentityClientOptions options)
		{
			string identityHeader = EnvironmentVariables.IdentityHeader;
			if (!AppServiceManagedIdentitySource.TryValidateEnvVars(EnvironmentVariables.IdentityEndpoint, identityHeader, out var endpointUri))
			{
				return null;
			}
			return new AppServiceV2019ManagedIdentitySource(options.Pipeline, endpointUri, identityHeader, options);
		}

		public AppServiceV2019ManagedIdentitySource(CredentialPipeline pipeline, Uri endpoint, string secret, ManagedIdentityClientOptions options)
			: base(pipeline, endpoint, secret, options)
		{
		}
	}
	internal class AuthenticationAccount : IAccount
	{
		private AuthenticationRecord _profile;

		string IAccount.Username => _profile.Username;

		string IAccount.Environment => _profile.Authority;

		AccountId IAccount.HomeAccountId => _profile.AccountId;

		internal AuthenticationAccount(AuthenticationRecord profile)
		{
			_profile = profile;
		}

		public static explicit operator AuthenticationAccount(AuthenticationRecord profile)
		{
			return new AuthenticationAccount(profile);
		}

		public static explicit operator AuthenticationRecord(AuthenticationAccount account)
		{
			return account._profile;
		}
	}
	[Serializable]
	public class AuthenticationFailedException : Exception
	{
		public AuthenticationFailedException(string message)
			: this(message, null)
		{
		}

		public AuthenticationFailedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected AuthenticationFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	public class AuthenticationRecord
	{
		internal const string CurrentVersion = "1.0";

		private const string UsernamePropertyName = "username";

		private const string AuthorityPropertyName = "authority";

		private const string HomeAccountIdPropertyName = "homeAccountId";

		private const string TenantIdPropertyName = "tenantId";

		private const string ClientIdPropertyName = "clientId";

		private const string VersionPropertyName = "version";

		private static readonly JsonEncodedText s_usernamePropertyNameBytes = JsonEncodedText.Encode("username");

		private static readonly JsonEncodedText s_authorityPropertyNameBytes = JsonEncodedText.Encode("authority");

		private static readonly JsonEncodedText s_homeAccountIdPropertyNameBytes = JsonEncodedText.Encode("homeAccountId");

		private static readonly JsonEncodedText s_tenantIdPropertyNameBytes = JsonEncodedText.Encode("tenantId");

		private static readonly JsonEncodedText s_clientIdPropertyNameBytes = JsonEncodedText.Encode("clientId");

		private static readonly JsonEncodedText s_versionPropertyNameBytes = JsonEncodedText.Encode("version");

		public string Username { get; private set; }

		public string Authority { get; private set; }

		public string HomeAccountId => AccountId.Identifier;

		public string TenantId { get; private set; }

		public string ClientId { get; private set; }

		internal AccountId AccountId { get; private set; }

		internal string Version { get; private set; } = "1.0";

		internal AuthenticationRecord()
		{
		}

		internal AuthenticationRecord(AuthenticationResult authResult, string clientId)
		{
			Username = authResult.Account.Username;
			Authority = authResult.Account.Environment;
			AccountId = authResult.Account.HomeAccountId;
			TenantId = authResult.TenantId;
			ClientId = clientId;
		}

		internal AuthenticationRecord(string username, string authority, string homeAccountId, string tenantId, string clientId)
		{
			Username = username;
			Authority = authority;
			AccountId = BuildAccountIdFromString(homeAccountId);
			TenantId = tenantId;
			ClientId = clientId;
		}

		public void Serialize(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			SerializeAsync(stream, async: false, cancellationToken).EnsureCompleted();
		}

		public async Task SerializeAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			await SerializeAsync(stream, async: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public static AuthenticationRecord Deserialize(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return DeserializeAsync(stream, async: false, cancellationToken).EnsureCompleted();
		}

		public static async Task<AuthenticationRecord> DeserializeAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return await DeserializeAsync(stream, async: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async Task SerializeAsync(Stream stream, bool async, CancellationToken cancellationToken)
		{
			using Utf8JsonWriter json = new Utf8JsonWriter(stream);
			json.WriteStartObject();
			json.WriteString(s_usernamePropertyNameBytes, Username);
			json.WriteString(s_authorityPropertyNameBytes, Authority);
			json.WriteString(s_homeAccountIdPropertyNameBytes, HomeAccountId);
			json.WriteString(s_tenantIdPropertyNameBytes, TenantId);
			json.WriteString(s_clientIdPropertyNameBytes, ClientId);
			json.WriteString(s_versionPropertyNameBytes, Version);
			json.WriteEndObject();
			if (async)
			{
				await json.FlushAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				json.Flush();
			}
		}

		private static async Task<AuthenticationRecord> DeserializeAsync(Stream stream, bool async, CancellationToken cancellationToken)
		{
			AuthenticationRecord authProfile = new AuthenticationRecord();
			JsonDocument jsonDocument;
			if (async)
			{
				jsonDocument = await JsonDocument.ParseAsync(stream, default(JsonDocumentOptions), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				jsonDocument = JsonDocument.Parse(stream);
			}
			using JsonDocument jsonDocument2 = jsonDocument;
			foreach (JsonProperty item in jsonDocument2.RootElement.EnumerateObject())
			{
				switch (item.Name)
				{
				case "username":
					authProfile.Username = item.Value.GetString();
					break;
				case "authority":
					authProfile.Authority = item.Value.GetString();
					break;
				case "homeAccountId":
					authProfile.AccountId = BuildAccountIdFromString(item.Value.GetString());
					break;
				case "tenantId":
					authProfile.TenantId = item.Value.GetString();
					break;
				case "clientId":
					authProfile.ClientId = item.Value.GetString();
					break;
				case "version":
					authProfile.Version = item.Value.GetString();
					if (authProfile.Version != "1.0")
					{
						throw new InvalidOperationException("Attempted to deserialize an AuthenticationRecord with a version that is not the current version. Expected: '1.0', Actual: '" + authProfile.Version + "'");
					}
					break;
				}
			}
			return authProfile;
		}

		private static AccountId BuildAccountIdFromString(string homeAccountId)
		{
			string[] array = homeAccountId.Split(new char[1] { '.' });
			if (array.Length == 2)
			{
				return new AccountId(homeAccountId, array[0], array[1]);
			}
			return new AccountId(homeAccountId);
		}
	}
	[Serializable]
	public class AuthenticationRequiredException : CredentialUnavailableException
	{
		public TokenRequestContext TokenRequestContext { get; }

		public AuthenticationRequiredException(string message, TokenRequestContext context)
			: this(message, context, null)
		{
		}

		public AuthenticationRequiredException(string message, TokenRequestContext context, Exception innerException)
			: base(message, innerException)
		{
			TokenRequestContext = context;
		}

		protected AuthenticationRequiredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	internal static class AuthenticationResultExtensions
	{
		public static AccessToken ToAccessToken(this AuthenticationResult result)
		{
			return new AccessToken(result.AccessToken, result.ExpiresOn, result.AuthenticationResultMetadata?.RefreshOn);
		}
	}
	internal class AzureArcManagedIdentitySource : ManagedIdentitySource
	{
		private const string IdentityEndpointInvalidUriError = "The environment variable IDENTITY_ENDPOINT contains an invalid Uri.";

		private const string NoChallengeErrorMessage = "Did not receive expected WWW-Authenticate header in the response from Azure Arc Managed Identity Endpoint.";

		private const string InvalidChallangeErrorMessage = "The WWW-Authenticate header in the response from Azure Arc Managed Identity Endpoint did not match the expected format.";

		private const string UserAssignedNotSupportedErrorMessage = "User assigned identity is not supported by the Azure Arc Managed Identity Endpoint. To authenticate with the system assigned identity omit the client id when constructing the ManagedIdentityCredential, or if authenticating with the DefaultAzureCredential ensure the AZURE_CLIENT_ID environment variable is not set.";

		private const string ArcApiVersion = "2019-11-01";

		private readonly string _clientId;

		private readonly Uri _endpoint;

		public static ManagedIdentitySource TryCreate(ManagedIdentityClientOptions options)
		{
			string identityEndpoint = EnvironmentVariables.IdentityEndpoint;
			string imdsEndpoint = EnvironmentVariables.ImdsEndpoint;
			if (string.IsNullOrEmpty(identityEndpoint) || string.IsNullOrEmpty(imdsEndpoint))
			{
				return null;
			}
			if (!Uri.TryCreate(identityEndpoint, UriKind.Absolute, out var result))
			{
				throw new AuthenticationFailedException("The environment variable IDENTITY_ENDPOINT contains an invalid Uri.");
			}
			return new AzureArcManagedIdentitySource(result, options);
		}

		internal AzureArcManagedIdentitySource(Uri endpoint, ManagedIdentityClientOptions options)
			: base(options.Pipeline)
		{
			_endpoint = endpoint;
			_clientId = options.ClientId;
			if (!string.IsNullOrEmpty(_clientId) || null != options.ResourceIdentifier)
			{
				AzureIdentityEventSource.Singleton.UserAssignedManagedIdentityNotSupported("Azure Arc");
			}
		}

		protected override Request CreateRequest(string[] scopes)
		{
			if (!string.IsNullOrEmpty(_clientId))
			{
				throw new AuthenticationFailedException("User assigned identity is not supported by the Azure Arc Managed Identity Endpoint. To authenticate with the system assigned identity omit the client id when constructing the ManagedIdentityCredential, or if authenticating with the DefaultAzureCredential ensure the AZURE_CLIENT_ID environment variable is not set.");
			}
			string value = ScopeUtilities.ScopesToResource(scopes);
			Request request = base.Pipeline.HttpPipeline.CreateRequest();
			request.Method = RequestMethod.Get;
			request.Headers.Add("Metadata", "true");
			request.Uri.Reset(_endpoint);
			request.Uri.AppendQuery("api-version", "2019-11-01");
			request.Uri.AppendQuery("resource", value);
			return request;
		}

		protected override async ValueTask<AccessToken> HandleResponseAsync(bool async, TokenRequestContext context, HttpMessage message, CancellationToken cancellationToken)
		{
			Response response = message.Response;
			if (response.Status == 401)
			{
				if (!response.Headers.TryGetValue("WWW-Authenticate", out string value))
				{
					throw new AuthenticationFailedException("Did not receive expected WWW-Authenticate header in the response from Azure Arc Managed Identity Endpoint.");
				}
				string[] array = value.Split(new char[1] { '=' }, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length != 2)
				{
					throw new AuthenticationFailedException("The WWW-Authenticate header in the response from Azure Arc Managed Identity Endpoint did not match the expected format.");
				}
				string filePath = array[1];
				ValidatePath(filePath);
				string value2 = "Basic " + File.ReadAllText(array[1]);
				using Request request = CreateRequest(context.Scopes);
				request.Headers.Add("Authorization", value2);
				HttpMessage challengeResponseMessage = base.Pipeline.HttpPipeline.CreateMessage();
				challengeResponseMessage.Request.Method = request.Method;
				challengeResponseMessage.Request.Uri.Reset(request.Uri.ToUri());
				foreach (HttpHeader header in request.Headers)
				{
					challengeResponseMessage.Request.Headers.Add(header.Name, header.Value);
				}
				HttpMessage httpMessage = challengeResponseMessage;
				Response response2 = ((!async) ? base.Pipeline.HttpPipeline.SendRequest(request, cancellationToken) : (await base.Pipeline.HttpPipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
				httpMessage.Response = response2;
				return await base.HandleResponseAsync(async, context, challengeResponseMessage, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			return await base.HandleResponseAsync(async, context, message, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private void ValidatePath(string filePath)
		{
			if (!filePath.EndsWith(".key"))
			{
				throw new AuthenticationFailedException("The secret key file failed validation. File name is invalid.");
			}
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				string value = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "AzureConnectedMachineAgent", "Tokens");
				if (!filePath.StartsWith(value))
				{
					throw new AuthenticationFailedException("The secret key file failed validation. File path is invalid.");
				}
			}
			if (Environment.OSVersion.Platform == PlatformID.Unix)
			{
				string value2 = Path.Combine("/", "var", "opt", "azcmagent", "tokens");
				if (!filePath.StartsWith(value2))
				{
					throw new AuthenticationFailedException("The secret key file failed validation. File path is invalid.");
				}
			}
			if (new FileInfo(filePath).Length > 4096)
			{
				throw new AuthenticationFailedException("The secret key file failed validation. File is too large.");
			}
		}
	}
	public static class AzureAuthorityHosts
	{
		private const string AzurePublicCloudHostUrl = "https://login.microsoftonline.com/";

		private const string AzureChinaHostUrl = "https://login.chinacloudapi.cn/";

		private const string AzureGermanyHostUrl = "https://login.microsoftonline.de/";

		private const string AzureGovernmentHostUrl = "https://login.microsoftonline.us/";

		public static Uri AzurePublicCloud { get; } = new Uri("https://login.microsoftonline.com/");

		public static Uri AzureChina { get; } = new Uri("https://login.chinacloudapi.cn/");

		public static Uri AzureGermany { get; } = new Uri("https://login.microsoftonline.de/");

		public static Uri AzureGovernment { get; } = new Uri("https://login.microsoftonline.us/");

		internal static Uri GetDefault()
		{
			if (EnvironmentVariables.AuthorityHost != null)
			{
				return new Uri(EnvironmentVariables.AuthorityHost);
			}
			return AzurePublicCloud;
		}

		internal static string GetDefaultScope(Uri authorityHost)
		{
			return authorityHost.AbsoluteUri switch
			{
				"https://login.microsoftonline.com/" => "https://management.azure.com//.default", 
				"https://login.chinacloudapi.cn/" => "https://management.chinacloudapi.cn/.default", 
				"https://login.microsoftonline.de/" => "https://management.microsoftazure.de/.default", 
				"https://login.microsoftonline.us/" => "https://management.usgovcloudapi.net/.default", 
				_ => null, 
			};
		}

		internal static Uri GetDeviceCodeRedirectUri(Uri authorityHost)
		{
			return new Uri(authorityHost, "/common/oauth2/nativeclient");
		}
	}
	[EventSource(Name = "Azure-Identity")]
	internal sealed class AzureIdentityEventSource : Azure.Core.Diagnostics.AzureEventSource
	{
		private const string EventSourceName = "Azure-Identity";

		private const int GetTokenEvent = 1;

		private const int GetTokenSucceededEvent = 2;

		private const int GetTokenFailedEvent = 3;

		private const int ProbeImdsEndpointEvent = 4;

		private const int ImdsEndpointFoundEvent = 5;

		private const int ImdsEndpointUnavailableEvent = 6;

		private const int MsalLogVerboseEvent = 7;

		private const int MsalLogInfoEvent = 8;

		private const int MsalLogWarningEvent = 9;

		private const int MsalLogErrorEvent = 10;

		private const int InteractiveAuthenticationThreadPoolExecutionEvent = 11;

		private const int InteractiveAuthenticationInlineExecutionEvent = 12;

		private const int DefaultAzureCredentialCredentialSelectedEvent = 13;

		private const int ProcessRunnerErrorEvent = 14;

		private const int ProcessRunnerInfoEvent = 15;

		private const int UsernamePasswordCredentialAcquireTokenSilentFailedEvent = 16;

		private const int TenantIdDiscoveredAndNotUsedEvent = 17;

		private const int TenantIdDiscoveredAndUsedEvent = 18;

		internal const int AuthenticatedAccountDetailsEvent = 19;

		internal const int UnableToParseAccountDetailsFromTokenEvent = 20;

		private const int UserAssignedManagedIdentityNotSupportedEvent = 21;

		private const int ServiceFabricManagedIdentityRuntimeConfigurationNotSupportedEvent = 22;

		internal const string TenantIdDiscoveredAndNotUsedEventMessage = "A token was request for a different tenant than was configured on the credential, but the configured value was used since multi tenant authentication has been disabled. Configured TenantId: {0}, Requested TenantId {1}";

		internal const string TenantIdDiscoveredAndUsedEventMessage = "A token was requested for a different tenant than was configured on the credential, and the requested tenant id was used to authenticate. Configured TenantId: {0}, Requested TenantId {1}";

		internal const string AuthenticatedAccountDetailsMessage = "Client ID: {0}. Tenant ID: {1}. User Principal Name: {2} Object ID: {3}";

		internal const string Unavailable = "<not available>";

		internal const string UnableToParseAccountDetailsFromTokenMessage = "Unable to parse account details from the Access Token";

		internal const string UserAssignedManagedIdentityNotSupportedMessage = "User assigned managed identities are not supported in the {0} environment.";

		internal const string ServiceFabricManagedIdentityRuntimeConfigurationNotSupportedMessage = "Service Fabric user assigned managed identity ClientId or ResourceId is not configurable at runtime.";

		public static AzureIdentityEventSource Singleton { get; } = new AzureIdentityEventSource();

		private AzureIdentityEventSource()
			: base("Azure-Identity")
		{
		}

		[NonEvent]
		public void GetToken(string method, TokenRequestContext context)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				GetToken(method, FormatStringArray(context.Scopes), context.ParentRequestId);
			}
		}

		[Event(1, Level = EventLevel.Informational, Message = "{0} invoked. Scopes: {1} ParentRequestId: {2}")]
		public void GetToken(string method, string scopes, string parentRequestId)
		{
			WriteEvent(1, method, scopes, parentRequestId);
		}

		[NonEvent]
		public void GetTokenSucceeded(string method, TokenRequestContext context, DateTimeOffset expiresOn)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				GetTokenSucceeded(method, FormatStringArray(context.Scopes), context.ParentRequestId, expiresOn.ToString("O", CultureInfo.InvariantCulture));
			}
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Parameters to this method are primitive and are trimmer safe.")]
		[Event(2, Level = EventLevel.Informational, Message = "{0} succeeded. Scopes: {1} ParentRequestId: {2} ExpiresOn: {3}")]
		public void GetTokenSucceeded(string method, string scopes, string parentRequestId, string expiresOn)
		{
			WriteEvent(2, method, scopes, parentRequestId, expiresOn);
		}

		[NonEvent]
		public void GetTokenFailed(string method, TokenRequestContext context, Exception ex)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				GetTokenFailed(method, FormatStringArray(context.Scopes), context.ParentRequestId, FormatException(ex));
			}
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Parameters to this method are primitive and are trimmer safe.")]
		[Event(3, Level = EventLevel.Informational, Message = "{0} was unable to retrieve an access token. Scopes: {1} ParentRequestId: {2} Exception: {3}")]
		public void GetTokenFailed(string method, string scopes, string parentRequestId, string exception)
		{
			WriteEvent(3, method, scopes, parentRequestId, exception);
		}

		[NonEvent]
		public void ProbeImdsEndpoint(Uri uri)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				ProbeImdsEndpoint(uri.AbsoluteUri);
			}
		}

		[Event(4, Level = EventLevel.Informational, Message = "Probing IMDS endpoint for availability. Endpoint: {0}")]
		public void ProbeImdsEndpoint(string uri)
		{
			WriteEvent(4, uri);
		}

		[NonEvent]
		public void ImdsEndpointFound(Uri uri)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				ImdsEndpointFound(uri.AbsoluteUri);
			}
		}

		[Event(5, Level = EventLevel.Informational, Message = "IMDS endpoint is available. Endpoint: {0}")]
		public void ImdsEndpointFound(string uri)
		{
			WriteEvent(5, uri);
		}

		[NonEvent]
		public void ImdsEndpointUnavailable(Uri uri, string error)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				ImdsEndpointUnavailable(uri.AbsoluteUri, error);
			}
		}

		[NonEvent]
		public void ImdsEndpointUnavailable(Uri uri, Exception e)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				ImdsEndpointUnavailable(uri.AbsoluteUri, FormatException(e));
			}
		}

		[Event(6, Level = EventLevel.Informational, Message = "IMDS endpoint is not available. Endpoint: {0}. Error: {1}")]
		public void ImdsEndpointUnavailable(string uri, string error)
		{
			WriteEvent(6, uri, error);
		}

		[NonEvent]
		private static string FormatException(Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			do
			{
				if (flag)
				{
					stringBuilder.AppendLine().Append(" ---> ");
				}
				stringBuilder.Append(ex.GetType().FullName).Append(" (0x").Append(ex.HResult.ToString("x", CultureInfo.InvariantCulture))
					.Append("): ")
					.Append(ex.Message);
				ex = ex.InnerException;
				flag = true;
			}
			while (ex != null);
			return stringBuilder.ToString();
		}

		[NonEvent]
		public void LogMsal(LogLevel level, string message)
		{
			switch (level)
			{
			case LogLevel.Error:
				if (IsEnabled(EventLevel.Error, EventKeywords.All))
				{
					LogMsalError(message);
				}
				break;
			case LogLevel.Warning:
				if (IsEnabled(EventLevel.Warning, EventKeywords.All))
				{
					LogMsalWarning(message);
				}
				break;
			case LogLevel.Info:
				if (IsEnabled(EventLevel.Informational, EventKeywords.All))
				{
					LogMsalInformational(message);
				}
				break;
			case LogLevel.Verbose:
				if (IsEnabled(EventLevel.Verbose, EventKeywords.All))
				{
					LogMsalVerbose(message);
				}
				break;
			}
		}

		[Event(10, Level = EventLevel.Error, Message = "{0}")]
		public void LogMsalError(string message)
		{
			WriteEvent(10, message);
		}

		[Event(9, Level = EventLevel.Warning, Message = "{0}")]
		public void LogMsalWarning(string message)
		{
			WriteEvent(9, message);
		}

		[Event(8, Level = EventLevel.Informational, Message = "{0}")]
		public void LogMsalInformational(string message)
		{
			WriteEvent(8, message);
		}

		[Event(7, Level = EventLevel.Verbose, Message = "{0}")]
		public void LogMsalVerbose(string message)
		{
			WriteEvent(7, message);
		}

		[NonEvent]
		private static string FormatStringArray(string[] array)
		{
			return new StringBuilder("[ ").Append(string.Join(", ", array)).Append(" ]").ToString();
		}

		[Event(11, Level = EventLevel.Informational, Message = "Executing interactive authentication workflow via Task.Run.")]
		public void InteractiveAuthenticationExecutingOnThreadPool()
		{
			WriteEvent(11);
		}

		[Event(12, Level = EventLevel.Informational, Message = "Executing interactive authentication workflow inline.")]
		public void InteractiveAuthenticationExecutingInline()
		{
			WriteEvent(12);
		}

		[Event(13, Level = EventLevel.Informational, Message = "DefaultAzureCredential credential selected: {0}")]
		public void DefaultAzureCredentialCredentialSelected(string credentialType)
		{
			WriteEvent(13, credentialType);
		}

		[NonEvent]
		public void ProcessRunnerError(string message)
		{
			if (IsEnabled(EventLevel.Error, EventKeywords.All))
			{
				LogProcessRunnerError(message);
			}
		}

		[Event(14, Level = EventLevel.Error, Message = "{0}")]
		public void LogProcessRunnerError(string message)
		{
			WriteEvent(14, message);
		}

		[NonEvent]
		public void ProcessRunnerInformational(string message)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				LogProcessRunnerInformational(message);
			}
		}

		[Event(15, Level = EventLevel.Informational, Message = "{0}")]
		public void LogProcessRunnerInformational(string message)
		{
			WriteEvent(15, message);
		}

		[NonEvent]
		public void UsernamePasswordCredentialAcquireTokenSilentFailed(Exception e)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				UsernamePasswordCredentialAcquireTokenSilentFailed(FormatException(e));
			}
		}

		[Event(16, Level = EventLevel.Informational, Message = "UsernamePasswordCredential failed to acquire token silently. Error: {1}")]
		public void UsernamePasswordCredentialAcquireTokenSilentFailed(string error)
		{
			WriteEvent(16, error);
		}

		[Event(17, Level = EventLevel.Informational, Message = "A token was request for a different tenant than was configured on the credential, but the configured value was used since multi tenant authentication has been disabled. Configured TenantId: {0}, Requested TenantId {1}")]
		public void TenantIdDiscoveredAndNotUsed(string explicitTenantId, string contextTenantId)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				WriteEvent(17, explicitTenantId, contextTenantId);
			}
		}

		[Event(18, Level = EventLevel.Informational, Message = "A token was requested for a different tenant than was configured on the credential, and the requested tenant id was used to authenticate. Configured TenantId: {0}, Requested TenantId {1}")]
		public void TenantIdDiscoveredAndUsed(string explicitTenantId, string contextTenantId)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				WriteEvent(18, explicitTenantId, contextTenantId);
			}
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Parameters to this method are primitive and are trimmer safe.")]
		[Event(19, Level = EventLevel.Informational, Message = "Client ID: {0}. Tenant ID: {1}. User Principal Name: {2} Object ID: {3}")]
		public void AuthenticatedAccountDetails(string clientId, string tenantId, string upn, string objectId)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				WriteEvent(19, clientId ?? "<not available>", tenantId ?? "<not available>", upn ?? "<not available>", objectId ?? "<not available>");
			}
		}

		[Event(20, Level = EventLevel.Informational, Message = "Unable to parse account details from the Access Token")]
		internal void UnableToParseAccountDetailsFromToken()
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				WriteEvent(20);
			}
		}

		[Event(21, Level = EventLevel.Warning, Message = "User assigned managed identities are not supported in the {0} environment.")]
		public void UserAssignedManagedIdentityNotSupported(string environment)
		{
			if (IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				WriteEvent(21, environment);
			}
		}

		[Event(22, Level = EventLevel.Warning, Message = "Service Fabric user assigned managed identity ClientId or ResourceId is not configurable at runtime.")]
		public void ServiceFabricManagedIdentityRuntimeConfigurationNotSupported()
		{
			if (IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				WriteEvent(22);
			}
		}
	}
	internal class CloudShellManagedIdentitySource : ManagedIdentitySource
	{
		private readonly Uri _endpoint;

		private const string MsiEndpointInvalidUriError = "The environment variable MSI_ENDPOINT contains an invalid Uri.";

		public static ManagedIdentitySource TryCreate(ManagedIdentityClientOptions options)
		{
			string msiEndpoint = EnvironmentVariables.MsiEndpoint;
			if (string.IsNullOrEmpty(msiEndpoint))
			{
				return null;
			}
			Uri endpoint;
			try
			{
				endpoint = new Uri(msiEndpoint);
			}
			catch (FormatException innerException)
			{
				throw new AuthenticationFailedException("The environment variable MSI_ENDPOINT contains an invalid Uri.", innerException);
			}
			return new CloudShellManagedIdentitySource(endpoint, options);
		}

		public CloudShellManagedIdentitySource(Uri endpoint, ManagedIdentityClientOptions options)
			: base(options.Pipeline)
		{
			_endpoint = endpoint;
			if (!string.IsNullOrEmpty(options.ClientId) || null != options.ResourceIdentifier)
			{
				AzureIdentityEventSource.Singleton.UserAssignedManagedIdentityNotSupported("Cloud Shell");
			}
		}

		protected override Request CreateRequest(string[] scopes)
		{
			string stringToEscape = ScopeUtilities.ScopesToResource(scopes);
			Request request = base.Pipeline.HttpPipeline.CreateRequest();
			request.Method = RequestMethod.Post;
			request.Headers.Add(HttpHeader.Common.FormUrlEncodedContentType);
			request.Uri.Reset(_endpoint);
			request.Headers.Add("Metadata", "true");
			string s = "resource=" + Uri.EscapeDataString(stringToEscape);
			ReadOnlyMemory<byte> bytes = MemoryExtensions.AsMemory(Encoding.UTF8.GetBytes(s));
			request.Content = RequestContent.Create(bytes);
			return request;
		}
	}
	internal class Constants
	{
		public const string OrganizationsTenantId = "organizations";

		public const string AdfsTenantId = "adfs";

		public const string DeveloperSignOnClientId = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";

		public const int SharedTokenCacheAccessRetryCount = 100;

		public static readonly TimeSpan SharedTokenCacheAccessRetryDelay = TimeSpan.FromMilliseconds(600.0);

		public const string DefaultRedirectUrl = "http://localhost";

		public static readonly string DefaultMsalTokenCacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ".IdentityService");

		public const string DefaultMsalTokenCacheKeychainService = "Microsoft.Developer.IdentityService";

		public const string DefaultMsalTokenCacheKeychainAccount = "MSALCache";

		public const string DefaultMsalTokenCacheKeyringLabel = "MSALCache";

		public const string DefaultMsalTokenCacheKeyringSchema = "msal.cache";

		public const string DefaultMsalTokenCacheKeyringCollection = "default";

		public static readonly KeyValuePair<string, string> DefaultMsaltokenCacheKeyringAttribute1 = new KeyValuePair<string, string>("MsalClientID", "Microsoft.Developer.IdentityService");

		public static readonly KeyValuePair<string, string> DefaultMsaltokenCacheKeyringAttribute2 = new KeyValuePair<string, string>("Microsoft.Developer.IdentityService", "1.0.0.0");

		public const string DefaultMsalTokenCacheName = "msal.cache";

		public const string CaeEnabledCacheSuffix = ".cae";

		public const string CaeDisabledCacheSuffix = ".nocae";

		public const string ManagedIdentityClientId = "client_id";

		public const string ManagedIdentityResourceId = "mi_res_id";

		public static string SharedTokenCacheFilePath => Path.Combine(DefaultMsalTokenCacheDirectory, "msal.cache");
	}
	internal readonly struct CredentialDiagnosticScope : IDisposable
	{
		private readonly string _name;

		private readonly Azure.Core.Pipeline.DiagnosticScope _scope;

		private readonly TokenRequestContext _context;

		private readonly IScopeHandler _scopeHandler;

		public CredentialDiagnosticScope(Azure.Core.Pipeline.ClientDiagnostics diagnostics, string name, TokenRequestContext context, IScopeHandler scopeHandler)
		{
			_name = name;
			_scope = scopeHandler.CreateScope(diagnostics, name);
			_context = context;
			_scopeHandler = scopeHandler;
		}

		public void Start()
		{
			AzureIdentityEventSource.Singleton.GetToken(_name, _context);
			_scopeHandler.Start(_name, in _scope);
		}

		public AccessToken Succeeded(AccessToken token)
		{
			AzureIdentityEventSource.Singleton.GetTokenSucceeded(_name, _context, token.ExpiresOn);
			return token;
		}

		public Exception FailWrapAndThrow(Exception ex, string additionalMessage = null, bool isCredentialUnavailable = false)
		{
			bool num = TryWrapException(ref ex, additionalMessage);
			RegisterFailed(ex);
			if (!num)
			{
				ExceptionDispatchInfo.Capture(ex).Throw();
			}
			throw ex;
		}

		private void RegisterFailed(Exception ex)
		{
			AzureIdentityEventSource.Singleton.GetTokenFailed(_name, _context, ex);
			_scopeHandler.Fail(_name, in _scope, ex);
		}

		private bool TryWrapException(ref Exception exception, string additionalMessageText = null, bool isCredentialUnavailable = false)
		{
			if (exception is OperationCanceledException || exception is AuthenticationFailedException)
			{
				return false;
			}
			if (exception is AggregateException ex)
			{
				CredentialUnavailableException ex2 = ex.Flatten().InnerExceptions.OfType<CredentialUnavailableException>().FirstOrDefault();
				if (ex2 != null)
				{
					exception = new CredentialUnavailableException(ex2.Message, ex);
					return true;
				}
			}
			string text = _name.Substring(0, _name.IndexOf('.')) + " authentication failed: " + exception.Message;
			if (additionalMessageText != null)
			{
				text = text + "\n" + additionalMessageText;
			}
			exception = (isCredentialUnavailable ? new CredentialUnavailableException(text, exception) : new AuthenticationFailedException(text, exception));
			return true;
		}

		public void Dispose()
		{
			_scopeHandler.Dispose(_name, in _scope);
		}
	}
	internal class CredentialPipeline
	{
		private class CredentialResponseClassifier : ResponseClassifier
		{
			public override bool IsRetriableResponse(HttpMessage message)
			{
				if (!base.IsRetriableResponse(message))
				{
					return message.Response.Status == 404;
				}
				return true;
			}
		}

		private class ScopeHandler : IScopeHandler
		{
			public Azure.Core.Pipeline.DiagnosticScope CreateScope(Azure.Core.Pipeline.ClientDiagnostics diagnostics, string name)
			{
				return diagnostics.CreateScope(name);
			}

			public void Start(string name, in Azure.Core.Pipeline.DiagnosticScope scope)
			{
				scope.Start();
			}

			public void Dispose(string name, in Azure.Core.Pipeline.DiagnosticScope scope)
			{
				scope.Dispose();
			}

			public void Fail(string name, in Azure.Core.Pipeline.DiagnosticScope scope, Exception exception)
			{
				scope.Failed(exception);
			}

			void IScopeHandler.Start(string name, in Azure.Core.Pipeline.DiagnosticScope scope)
			{
				Start(name, in scope);
			}

			void IScopeHandler.Dispose(string name, in Azure.Core.Pipeline.DiagnosticScope scope)
			{
				Dispose(name, in scope);
			}

			void IScopeHandler.Fail(string name, in Azure.Core.Pipeline.DiagnosticScope scope, Exception exception)
			{
				Fail(name, in scope, exception);
			}
		}

		private static readonly Lazy<CredentialPipeline> s_singleton = new Lazy<CredentialPipeline>(() => new CredentialPipeline(new TokenCredentialOptions()));

		private static readonly IScopeHandler _defaultScopeHandler = new ScopeHandler();

		public HttpPipeline HttpPipeline { get; }

		public Azure.Core.Pipeline.ClientDiagnostics Diagnostics { get; }

		private CredentialPipeline(TokenCredentialOptions options)
		{
			HttpPipeline = HttpPipelineBuilder.Build(new HttpPipelineOptions(options)
			{
				RequestFailedDetailsParser = new ManagedIdentityRequestFailedDetailsParser()
			});
			Diagnostics = new Azure.Core.Pipeline.ClientDiagnostics(options);
		}

		public CredentialPipeline(HttpPipeline httpPipeline, Azure.Core.Pipeline.ClientDiagnostics diagnostics)
		{
			HttpPipeline = httpPipeline;
			Diagnostics = diagnostics;
		}

		public static CredentialPipeline GetInstance(TokenCredentialOptions options, bool IsManagedIdentityCredential = false)
		{
			if (!IsManagedIdentityCredential)
			{
				if (options != null)
				{
					return new CredentialPipeline(options);
				}
				return s_singleton.Value;
			}
			return configureOptionsForManagedIdentity(options);
		}

		private static CredentialPipeline configureOptionsForManagedIdentity(TokenCredentialOptions options)
		{
			TokenCredentialOptions tokenCredentialOptions = ((!(options is DefaultAzureCredentialOptions defaultAzureCredentialOptions)) ? (options?.Clone<TokenCredentialOptions>() ?? new TokenCredentialOptions()) : defaultAzureCredentialOptions.Clone<DefaultAzureCredentialOptions>());
			TokenCredentialOptions tokenCredentialOptions2 = tokenCredentialOptions;
			tokenCredentialOptions2.Retry.MaxRetries = 5;
			tokenCredentialOptions = tokenCredentialOptions2;
			if (tokenCredentialOptions.RetryPolicy == null)
			{
				HttpPipelinePolicy httpPipelinePolicy = (tokenCredentialOptions.RetryPolicy = new DefaultAzureCredentialImdsRetryPolicy(tokenCredentialOptions2.Retry));
			}
			tokenCredentialOptions2.IsChainedCredential = tokenCredentialOptions2 is DefaultAzureCredentialOptions;
			return new CredentialPipeline(tokenCredentialOptions2);
		}

		public IConfidentialClientApplication CreateMsalConfidentialClient(string tenantId, string clientId, string clientSecret)
		{
			return ConfidentialClientApplicationBuilder.Create(clientId).WithHttpClientFactory(new HttpPipelineClientFactory(HttpPipeline)).WithTenantId(tenantId)
				.WithClientSecret(clientSecret)
				.Build();
		}

		public CredentialDiagnosticScope StartGetTokenScope(string fullyQualifiedMethod, TokenRequestContext context)
		{
			IScopeHandler scopeHandler = ScopeGroupHandler.Current ?? _defaultScopeHandler;
			CredentialDiagnosticScope result = new CredentialDiagnosticScope(Diagnostics, fullyQualifiedMethod, context, scopeHandler);
			result.Start();
			return result;
		}

		public CredentialDiagnosticScope StartGetTokenScopeGroup(string fullyQualifiedMethod, TokenRequestContext context)
		{
			ScopeGroupHandler scopeHandler = new ScopeGroupHandler(fullyQualifiedMethod);
			CredentialDiagnosticScope result = new CredentialDiagnosticScope(Diagnostics, fullyQualifiedMethod, context, scopeHandler);
			result.Start();
			return result;
		}
	}
	public class AuthorizationCodeCredential : TokenCredential
	{
		private readonly string _authCode;

		private readonly string _clientId;

		private readonly CredentialPipeline _pipeline;

		private AuthenticationRecord _record;

		private readonly string _redirectUri;

		private readonly string _tenantId;

		internal readonly string[] AdditionallyAllowedTenantIds;

		internal MsalConfidentialClient Client { get; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		protected AuthorizationCodeCredential()
		{
		}

		public AuthorizationCodeCredential(string tenantId, string clientId, string clientSecret, string authorizationCode)
			: this(tenantId, clientId, clientSecret, authorizationCode, null)
		{
		}

		public AuthorizationCodeCredential(string tenantId, string clientId, string clientSecret, string authorizationCode, AuthorizationCodeCredentialOptions options)
			: this(tenantId, clientId, clientSecret, authorizationCode, options, null)
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public AuthorizationCodeCredential(string tenantId, string clientId, string clientSecret, string authorizationCode, TokenCredentialOptions options)
			: this(tenantId, clientId, clientSecret, authorizationCode, options, null)
		{
		}

		internal AuthorizationCodeCredential(string tenantId, string clientId, string clientSecret, string authorizationCode, TokenCredentialOptions options, MsalConfidentialClient client, CredentialPipeline pipeline = null)
		{
			Validations.ValidateTenantId(tenantId, "tenantId");
			_tenantId = tenantId;
			Azure.Core.Argument.AssertNotNull(clientSecret, "clientSecret");
			Azure.Core.Argument.AssertNotNull(clientId, "clientId");
			Azure.Core.Argument.AssertNotNull(authorizationCode, "authorizationCode");
			_clientId = clientId;
			_authCode = authorizationCode;
			_pipeline = pipeline ?? CredentialPipeline.GetInstance(options ?? new TokenCredentialOptions());
			_redirectUri = ((!(options is AuthorizationCodeCredentialOptions authorizationCodeCredentialOptions)) ? null : authorizationCodeCredentialOptions.RedirectUri?.AbsoluteUri);
			Client = client ?? new MsalConfidentialClient(_pipeline, tenantId, clientId, clientSecret, _redirectUri, options);
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds((options as ISupportsAdditionallyAllowedTenants)?.AdditionallyAllowedTenants);
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetTokenImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await GetTokenImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("AuthorizationCodeCredential.GetToken", requestContext);
			string tenantId = null;
			try
			{
				tenantId = TenantIdResolver.Resolve(_tenantId, requestContext, AdditionallyAllowedTenantIds);
				AccessToken token = ((_record != null) ? (await Client.AcquireTokenSilentAsync(requestContext.Scopes, (AuthenticationAccount)_record, tenantId, _redirectUri, requestContext.Claims, requestContext.IsCaeEnabled, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).ToAccessToken() : (await AcquireTokenWithCode(async, requestContext, tenantId, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
				return scope.Succeeded(token);
			}
			catch (MsalUiRequiredException)
			{
			}
			catch (Exception ex2)
			{
				throw scope.FailWrapAndThrow(ex2);
			}
			try
			{
				return scope.Succeeded(await AcquireTokenWithCode(async, requestContext, tenantId, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
			}
			catch (Exception ex3)
			{
				throw scope.FailWrapAndThrow(ex3);
			}
		}

		private async Task<AccessToken> AcquireTokenWithCode(bool async, TokenRequestContext requestContext, string tenantId, CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = await Client.AcquireTokenByAuthorizationCodeAsync(requestContext.Scopes, _authCode, tenantId, _redirectUri, requestContext.Claims, requestContext.IsCaeEnabled, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			_record = new AuthenticationRecord(authenticationResult, _clientId);
			return authenticationResult.ToAccessToken();
		}
	}
	public class AuthorizationCodeCredentialOptions : TokenCredentialOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		public Uri RedirectUri { get; set; }

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		public bool DisableInstanceDiscovery { get; set; }
	}
	internal class AzureApplicationCredential : TokenCredential
	{
		private readonly ChainedTokenCredential _credential;

		public AzureApplicationCredential()
			: this(new AzureApplicationCredentialOptions(), null, null)
		{
		}

		public AzureApplicationCredential(AzureApplicationCredentialOptions options)
			: this(options ?? new AzureApplicationCredentialOptions(), null, null)
		{
		}

		internal AzureApplicationCredential(AzureApplicationCredentialOptions options, EnvironmentCredential environmentCredential = null, ManagedIdentityCredential managedIdentityCredential = null)
		{
			_credential = new ChainedTokenCredential(environmentCredential ?? new EnvironmentCredential(options), managedIdentityCredential ?? new ManagedIdentityCredential(options.ManagedIdentityClientId));
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetTokenImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await GetTokenImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return (!async) ? _credential.GetToken(requestContext, cancellationToken) : (await _credential.GetTokenAsync(requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}
	}
	internal class AzureApplicationCredentialOptions : TokenCredentialOptions
	{
		public string ManagedIdentityClientId { get; set; } = GetNonEmptyStringOrNull(EnvironmentVariables.ClientId);

		private static string GetNonEmptyStringOrNull(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return null;
			}
			return str;
		}
	}
	public class AzureCliCredential : TokenCredential
	{
		internal const string AzureCLINotInstalled = "Azure CLI not installed";

		internal const string AzNotLogIn = "Please run 'az login' to set up account";

		internal const string WinAzureCLIError = "'az' is not recognized";

		internal const string AzureCliTimeoutError = "Azure CLI authentication timed out.";

		internal const string AzureCliFailedError = "Azure CLI authentication failed due to an unknown error.";

		internal const string Troubleshoot = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/azclicredential/troubleshoot";

		internal const string InteractiveLoginRequired = "Azure CLI could not login. Interactive login is required.";

		internal const string CLIInternalError = "CLIInternalError: The command failed with an unexpected error. Here is the traceback:";

		private static readonly string DefaultPathWindows = EnvironmentVariables.ProgramFilesX86 + "\\Microsoft SDKs\\Azure\\CLI2\\wbin;" + EnvironmentVariables.ProgramFiles + "\\Microsoft SDKs\\Azure\\CLI2\\wbin";

		private static readonly string DefaultWorkingDirWindows = Environment.GetFolderPath(Environment.SpecialFolder.System);

		private const string DefaultPathNonWindows = "/usr/bin:/usr/local/bin";

		private const string DefaultWorkingDirNonWindows = "/bin/";

		private const string RefreshTokeExpired = "The provided authorization code or refresh token has expired due to inactivity. Send a new interactive authorization request for this user and resource.";

		private static readonly string DefaultPath = (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? DefaultPathWindows : "/usr/bin:/usr/local/bin");

		private static readonly string DefaultWorkingDir = (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? DefaultWorkingDirWindows : "/bin/");

		private static readonly Regex AzNotFoundPattern = new Regex("az:(.*)not found");

		private readonly string _path;

		private readonly CredentialPipeline _pipeline;

		private readonly IProcessService _processService;

		private readonly bool _logPII;

		private readonly bool _logAccountDetails;

		internal bool _isChainedCredential;

		internal TimeSpan ProcessTimeout { get; private set; }

		internal string TenantId { get; }

		internal string[] AdditionallyAllowedTenantIds { get; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		public AzureCliCredential()
			: this(CredentialPipeline.GetInstance(null), null)
		{
		}

		public AzureCliCredential(AzureCliCredentialOptions options)
			: this(CredentialPipeline.GetInstance(null), null, options)
		{
		}

		internal AzureCliCredential(CredentialPipeline pipeline, IProcessService processService, AzureCliCredentialOptions options = null)
		{
			_logPII = options?.IsUnsafeSupportLoggingEnabled ?? false;
			_logAccountDetails = options?.Diagnostics?.IsAccountIdentifierLoggingEnabled == true;
			_pipeline = pipeline;
			_path = ((!string.IsNullOrEmpty(EnvironmentVariables.Path)) ? EnvironmentVariables.Path : DefaultPath);
			_processService = processService ?? ProcessService.Default;
			TenantId = Validations.ValidateTenantId(options?.TenantId, "options.TenantId", allowNull: true);
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds(((ISupportsAdditionallyAllowedTenants)options)?.AdditionallyAllowedTenants);
			ProcessTimeout = options?.ProcessTimeout ?? TimeSpan.FromSeconds(13.0);
			_isChainedCredential = options?.IsChainedCredential ?? false;
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetTokenImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await GetTokenImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("AzureCliCredential.GetToken", requestContext);
			try
			{
				return scope.Succeeded(await RequestCliAccessTokenAsync(async, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex);
			}
		}

		private async ValueTask<AccessToken> RequestCliAccessTokenAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			string text = ScopeUtilities.ScopesToResource(context.Scopes);
			string tenantId = TenantIdResolver.Resolve(TenantId, context, AdditionallyAllowedTenantIds);
			Validations.ValidateTenantId(tenantId, "TenantId", allowNull: true);
			ScopeUtilities.ValidateScope(text);
			GetFileNameAndArguments(text, tenantId, out var fileName, out var argument);
			ProcessStartInfo azureCliProcessStartInfo = GetAzureCliProcessStartInfo(fileName, argument);
			using ProcessRunner processRunner = new ProcessRunner(_processService.Create(azureCliProcessStartInfo), ProcessTimeout, _logPII, cancellationToken);
			string output;
			try
			{
				string text2 = ((!async) ? processRunner.Run() : (await processRunner.RunAsync().ConfigureAwait(continueOnCapturedContext: false)));
				output = text2;
			}
			catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
			{
				if (_isChainedCredential)
				{
					throw new CredentialUnavailableException("Azure CLI authentication timed out.");
				}
				throw new AuthenticationFailedException("Azure CLI authentication timed out.");
			}
			catch (InvalidOperationException ex2)
			{
				bool num = ex2.Message.StartsWith("'az' is not recognized", StringComparison.CurrentCultureIgnoreCase);
				bool flag = AzNotFoundPattern.IsMatch(ex2.Message);
				if (num || flag)
				{
					throw new CredentialUnavailableException("Azure CLI not installed");
				}
				bool flag2 = ex2.Message.Contains("AADSTS");
				if ((ex2.Message.IndexOf("az login", StringComparison.OrdinalIgnoreCase) != -1 || ex2.Message.IndexOf("az account set", StringComparison.OrdinalIgnoreCase) != -1) && !flag2)
				{
					throw new CredentialUnavailableException("Please run 'az login' to set up account");
				}
				if ((ex2.Message.IndexOf("Azure CLI authentication failed due to an unknown error.", StringComparison.OrdinalIgnoreCase) != -1 && ex2.Message.IndexOf("The provided authorization code or refresh token has expired due to inactivity. Send a new interactive authorization request for this user and resource.", StringComparison.OrdinalIgnoreCase) != -1) || ex2.Message.IndexOf("CLIInternalError", StringComparison.OrdinalIgnoreCase) != -1)
				{
					throw new CredentialUnavailableException("Azure CLI could not login. Interactive login is required.");
				}
				if (_isChainedCredential)
				{
					throw new CredentialUnavailableException("Azure CLI authentication failed due to an unknown error. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/azclicredential/troubleshoot " + ex2.Message);
				}
				throw new AuthenticationFailedException("Azure CLI authentication failed due to an unknown error. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/azclicredential/troubleshoot " + ex2.Message);
			}
			AccessToken result = DeserializeOutput(output);
			if (_logAccountDetails)
			{
				(string, string, string, string) tuple = TokenHelper.ParseAccountInfoFromToken(result.Token);
				AzureIdentityEventSource.Singleton.AuthenticatedAccountDetails(tuple.Item1, tuple.Item2 ?? TenantId, tuple.Item3, tuple.Item4);
			}
			return result;
		}

		private ProcessStartInfo GetAzureCliProcessStartInfo(string fileName, string argument)
		{
			return new ProcessStartInfo
			{
				FileName = fileName,
				Arguments = argument,
				UseShellExecute = false,
				ErrorDialog = false,
				CreateNoWindow = true,
				WorkingDirectory = DefaultWorkingDir,
				Environment = { { "PATH", _path } }
			};
		}

		private static void GetFileNameAndArguments(string resource, string tenantId, out string fileName, out string argument)
		{
			string text = ((tenantId != null) ? ("az account get-access-token --output json --resource " + resource + " --tenant " + tenantId) : ("az account get-access-token --output json --resource " + resource));
			string text2 = text;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "cmd.exe");
				argument = "/d /c \"" + text2 + "\"";
			}
			else
			{
				fileName = "/bin/sh";
				argument = "-c \"" + text2 + "\"";
			}
		}

		private static AccessToken DeserializeOutput(string output)
		{
			using JsonDocument jsonDocument = JsonDocument.Parse(output);
			JsonElement rootElement = jsonDocument.RootElement;
			string? accessToken = rootElement.GetProperty("accessToken").GetString();
			JsonElement value;
			DateTimeOffset expiresOn = (rootElement.TryGetProperty("expires_on", out value) ? DateTimeOffset.FromUnixTimeSeconds(value.GetInt64()) : DateTimeOffset.ParseExact(rootElement.GetProperty("expiresOn").GetString(), "yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal));
			return new AccessToken(accessToken, expiresOn);
		}
	}
	public class AzureCliCredentialOptions : TokenCredentialOptions, ISupportsAdditionallyAllowedTenants
	{
		public string TenantId { get; set; }

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		public TimeSpan? ProcessTimeout { get; set; }
	}
	public class AzureDeveloperCliCredential : TokenCredential
	{
		internal const string AzdCliNotInstalled = "Azure Developer CLI could not be found.";

		internal const string AzdNotLogIn = "Please run 'azd auth login' from a command prompt to authenticate before using this credential.";

		internal const string WinAzdCliError = "'azd' is not recognized";

		internal const string AzdCliTimeoutError = "Azure Developer CLI authentication timed out.";

		internal const string AzdCliFailedError = "Azure Developer CLI authentication failed due to an unknown error.";

		internal const string Troubleshoot = "Please visit https://aka.ms/azure-dev for installation instructions and then, once installed, authenticate to your Azure account using 'azd auth login'.";

		internal const string InteractiveLoginRequired = "Azure Developer CLI could not login. Interactive login is required.";

		internal const string AzdCLIInternalError = "AzdCLIInternalError: The command failed with an unexpected error. Here is the traceback:";

		private static readonly string DefaultWorkingDirWindows = Environment.GetFolderPath(Environment.SpecialFolder.System);

		private const string DefaultWorkingDirNonWindows = "/bin/";

		private const string RefreshTokeExpired = "The provided authorization code or refresh token has expired due to inactivity. Send a new interactive authorization request for this user and resource.";

		private static readonly string DefaultWorkingDir = (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? DefaultWorkingDirWindows : "/bin/");

		private static readonly Regex AzdNotFoundPattern = new Regex("azd:(.*)not found");

		private readonly CredentialPipeline _pipeline;

		private readonly IProcessService _processService;

		private readonly bool _logPII;

		private readonly bool _logAccountDetails;

		internal bool _isChainedCredential;

		internal TimeSpan ProcessTimeout { get; private set; }

		internal string TenantId { get; }

		internal string[] AdditionallyAllowedTenantIds { get; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		public AzureDeveloperCliCredential()
			: this(CredentialPipeline.GetInstance(null), null)
		{
		}

		public AzureDeveloperCliCredential(AzureDeveloperCliCredentialOptions options)
			: this(CredentialPipeline.GetInstance(null), null, options)
		{
		}

		internal AzureDeveloperCliCredential(CredentialPipeline pipeline, IProcessService processService, AzureDeveloperCliCredentialOptions options = null)
		{
			_logPII = options?.IsUnsafeSupportLoggingEnabled ?? false;
			_logAccountDetails = options?.Diagnostics?.IsAccountIdentifierLoggingEnabled == true;
			_pipeline = pipeline;
			_processService = processService ?? ProcessService.Default;
			TenantId = Validations.ValidateTenantId(options?.TenantId, "options.TenantId", allowNull: true);
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds(((ISupportsAdditionallyAllowedTenants)options)?.AdditionallyAllowedTenants);
			ProcessTimeout = options?.ProcessTimeout ?? TimeSpan.FromSeconds(13.0);
			_isChainedCredential = options?.IsChainedCredential ?? false;
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetTokenImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await GetTokenImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("AzureDeveloperCliCredential.GetToken", requestContext);
			try
			{
				return scope.Succeeded(await RequestCliAccessTokenAsync(async, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex);
			}
		}

		private async ValueTask<AccessToken> RequestCliAccessTokenAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			string tenantId = TenantIdResolver.Resolve(TenantId, context, AdditionallyAllowedTenantIds);
			Validations.ValidateTenantId(tenantId, "TenantId", allowNull: true);
			string[] scopes = context.Scopes;
			for (int i = 0; i < scopes.Length; i++)
			{
				ScopeUtilities.ValidateScope(scopes[i]);
			}
			GetFileNameAndArguments(context.Scopes, tenantId, out var fileName, out var argument);
			ProcessStartInfo azureDeveloperCliProcessStartInfo = GetAzureDeveloperCliProcessStartInfo(fileName, argument);
			using ProcessRunner processRunner = new ProcessRunner(_processService.Create(azureDeveloperCliProcessStartInfo), ProcessTimeout, _logPII, cancellationToken);
			string output;
			try
			{
				string text = ((!async) ? processRunner.Run() : (await processRunner.RunAsync().ConfigureAwait(continueOnCapturedContext: false)));
				output = text;
			}
			catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
			{
				if (_isChainedCredential)
				{
					throw new CredentialUnavailableException("Azure Developer CLI authentication timed out.");
				}
				throw new AuthenticationFailedException("Azure Developer CLI authentication timed out.");
			}
			catch (InvalidOperationException ex2)
			{
				bool num = ex2.Message.StartsWith("'azd' is not recognized", StringComparison.CurrentCultureIgnoreCase);
				bool flag = AzdNotFoundPattern.IsMatch(ex2.Message);
				if (num || flag)
				{
					throw new CredentialUnavailableException("Azure Developer CLI could not be found.");
				}
				bool flag2 = ex2.Message.Contains("AADSTS");
				if (ex2.Message.IndexOf("azd auth login", StringComparison.OrdinalIgnoreCase) != -1 && !flag2)
				{
					throw new CredentialUnavailableException("Please run 'azd auth login' from a command prompt to authenticate before using this credential.");
				}
				if ((ex2.Message.IndexOf("Azure Developer CLI authentication failed due to an unknown error.", StringComparison.OrdinalIgnoreCase) != -1 && ex2.Message.IndexOf("The provided authorization code or refresh token has expired due to inactivity. Send a new interactive authorization request for this user and resource.", StringComparison.OrdinalIgnoreCase) != -1) || ex2.Message.IndexOf("CLIInternalError", StringComparison.OrdinalIgnoreCase) != -1)
				{
					throw new CredentialUnavailableException("Azure Developer CLI could not login. Interactive login is required.");
				}
				if (_isChainedCredential)
				{
					throw new CredentialUnavailableException("Azure Developer CLI authentication failed due to an unknown error. Please visit https://aka.ms/azure-dev for installation instructions and then, once installed, authenticate to your Azure account using 'azd auth login'. " + ex2.Message);
				}
				throw new AuthenticationFailedException("Azure Developer CLI authentication failed due to an unknown error. Please visit https://aka.ms/azure-dev for installation instructions and then, once installed, authenticate to your Azure account using 'azd auth login'. " + ex2.Message);
			}
			AccessToken result = DeserializeOutput(output);
			if (_logAccountDetails)
			{
				(string, string, string, string) tuple = TokenHelper.ParseAccountInfoFromToken(result.Token);
				AzureIdentityEventSource.Singleton.AuthenticatedAccountDetails(tuple.Item1, tuple.Item2 ?? TenantId, tuple.Item3, tuple.Item4);
			}
			return result;
		}

		private static ProcessStartInfo GetAzureDeveloperCliProcessStartInfo(string fileName, string argument)
		{
			return new ProcessStartInfo
			{
				FileName = fileName,
				Arguments = argument,
				UseShellExecute = false,
				ErrorDialog = false,
				CreateNoWindow = true,
				WorkingDirectory = DefaultWorkingDir
			};
		}

		private static void GetFileNameAndArguments(string[] scopes, string tenantId, out string fileName, out string argument)
		{
			string text = string.Join(" ", scopes.Select((string scope) => "--scope " + scope));
			string text2 = ((tenantId != null) ? ("azd auth token --output json " + text + " --tenant-id " + tenantId) : ("azd auth token --output json " + text));
			string text3 = text2;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "cmd.exe");
				argument = "/d /c \"" + text3 + "\"";
			}
			else
			{
				fileName = "/bin/sh";
				argument = "-c \"" + text3 + "\"";
			}
		}

		private static AccessToken DeserializeOutput(string output)
		{
			using JsonDocument jsonDocument = JsonDocument.Parse(output);
			JsonElement rootElement = jsonDocument.RootElement;
			string? accessToken = rootElement.GetProperty("token").GetString();
			DateTimeOffset dateTimeOffset = rootElement.GetProperty("expiresOn").GetDateTimeOffset();
			return new AccessToken(accessToken, dateTimeOffset);
		}
	}
	public class AzureDeveloperCliCredentialOptions : TokenCredentialOptions, ISupportsAdditionallyAllowedTenants
	{
		public string TenantId { get; set; }

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		public TimeSpan? ProcessTimeout { get; set; }
	}
	public class AzurePipelinesCredential : TokenCredential
	{
		private const string Troubleshooting = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/azurepipelinescredential/troubleshoot";

		internal readonly string[] AdditionallyAllowedTenantIds;

		private const string OIDC_API_VERSION = "7.1";

		internal string SystemAccessToken { get; }

		internal string TenantId { get; }

		internal string ServiceConnectionId { get; }

		internal MsalConfidentialClient Client { get; }

		internal CredentialPipeline Pipeline { get; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		protected AzurePipelinesCredential()
		{
		}

		public AzurePipelinesCredential(string tenantId, string clientId, string serviceConnectionId, string systemAccessToken, AzurePipelinesCredentialOptions options = null)
		{
			AzurePipelinesCredential azurePipelinesCredential = this;
			Azure.Core.Argument.AssertNotNull(systemAccessToken, "systemAccessToken");
			Azure.Core.Argument.AssertNotNull(clientId, "clientId");
			Azure.Core.Argument.AssertNotNull(tenantId, "tenantId");
			Azure.Core.Argument.AssertNotNull(serviceConnectionId, "serviceConnectionId");
			SystemAccessToken = systemAccessToken;
			ServiceConnectionId = serviceConnectionId;
			if (options == null)
			{
				options = new AzurePipelinesCredentialOptions();
			}
			TenantId = Validations.ValidateTenantId(tenantId, "tenantId");
			Pipeline = options.Pipeline ?? CredentialPipeline.GetInstance(options);
			Func<CancellationToken, Task<string>> assertionCallback = async delegate(CancellationToken cancellationToken)
			{
				HttpMessage message = azurePipelinesCredential.CreateOidcRequestMessage(options ?? new AzurePipelinesCredentialOptions());
				await azurePipelinesCredential.Pipeline.HttpPipeline.SendAsync(message, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				return azurePipelinesCredential.GetOidcTokenResponse(message);
			};
			Client = options?.MsalClient ?? new MsalConfidentialClient(Pipeline, TenantId, clientId, assertionCallback, options);
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds(((ISupportsAdditionallyAllowedTenants)options)?.AdditionallyAllowedTenants);
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return GetTokenCoreAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return await GetTokenCoreAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		internal async ValueTask<AccessToken> GetTokenCoreAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = Pipeline.StartGetTokenScope("AzurePipelinesCredential.GetToken", requestContext);
			try
			{
				string tenantId = TenantIdResolver.Resolve(TenantId, requestContext, AdditionallyAllowedTenantIds);
				return scope.Succeeded((await Client.AcquireTokenForClientAsync(requestContext.Scopes, tenantId, requestContext.Claims, requestContext.IsCaeEnabled, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).ToAccessToken());
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex, "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/azurepipelinescredential/troubleshoot");
			}
		}

		internal HttpMessage CreateOidcRequestMessage(AzurePipelinesCredentialOptions options)
		{
			string text = options.OidcRequestUri ?? throw new CredentialUnavailableException("AzurePipelinesCredential is not available: Ensure that you're running this task in an Azure Pipeline so that following missing system variable(s) can be defined: SYSTEM_OIDCREQUESTURI is not set.");
			string systemAccessToken = SystemAccessToken;
			HttpMessage httpMessage = Pipeline.HttpPipeline.CreateMessage();
			Uri value = new Uri(text + "?api-version=7.1&serviceConnectionId=" + ServiceConnectionId);
			httpMessage.Request.Uri.Reset(value);
			httpMessage.Request.Headers.SetValue(HttpHeader.Names.Authorization, "Bearer " + systemAccessToken);
			httpMessage.Request.Headers.SetValue(HttpHeader.Names.ContentType, "application/json");
			httpMessage.Request.Method = RequestMethod.Post;
			return httpMessage;
		}

		internal string GetOidcTokenResponse(HttpMessage message)
		{
			string text = null;
			try
			{
				Utf8JsonReader utf8JsonReader = new Utf8JsonReader(message.Response.Content);
				while (text == null && utf8JsonReader.Read())
				{
					if (utf8JsonReader.TokenType == JsonTokenType.PropertyName && utf8JsonReader.GetString() == "oidcToken")
					{
						utf8JsonReader.Read();
						text = utf8JsonReader.GetString();
					}
				}
			}
			catch
			{
			}
			if (text == null)
			{
				string text2 = "OIDC token not found in response. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/azurepipelinescredential/troubleshoot";
				if (message.Response.Status != 200)
				{
					text2 += $"\n\nResponse= {message.Response.Content}";
				}
				throw new AuthenticationFailedException(text2);
			}
			return text;
		}
	}
	public class AzurePipelinesCredentialOptions : TokenCredentialOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants, ISupportsTokenCachePersistenceOptions
	{
		internal CredentialPipeline Pipeline { get; set; }

		internal MsalConfidentialClient MsalClient { get; set; }

		internal string OidcRequestUri { get; set; } = Environment.GetEnvironmentVariable("SYSTEM_OIDCREQUESTURI");

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		public bool DisableInstanceDiscovery { get; set; }

		public TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }
	}
	public class AzurePowerShellCredential : TokenCredential
	{
		private readonly CredentialPipeline _pipeline;

		private readonly IProcessService _processService;

		private const string Troubleshooting = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/powershellcredential/troubleshoot";

		internal const string AzurePowerShellFailedError = "Azure PowerShell authentication failed due to an unknown error. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/powershellcredential/troubleshoot";

		private const string RunConnectAzAccountToLogin = "Run Connect-AzAccount to login";

		private const string NoAccountsWereFoundInTheCache = "No accounts were found in the cache";

		private const string CannotRetrieveAccessToken = "cannot retrieve access token";

		private const string AzurePowerShellNoAzAccountModule = "NoAzAccountModule";

		private static readonly string DefaultWorkingDirWindows = Environment.GetFolderPath(Environment.SpecialFolder.System);

		private const string DefaultWorkingDirNonWindows = "/bin/";

		private static readonly string DefaultWorkingDir = (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? DefaultWorkingDirWindows : "/bin/");

		private readonly bool _logPII;

		private readonly bool _logAccountDetails;

		internal readonly bool _isChainedCredential;

		internal const string AzurePowerShellNotLogInError = "Please run 'Connect-AzAccount' to set up account.";

		internal const string AzurePowerShellModuleNotInstalledError = "Az.Accounts module >= 2.2.0 is not installed.";

		internal const string PowerShellNotInstalledError = "PowerShell is not installed.";

		internal const string AzurePowerShellTimeoutError = "Azure PowerShell authentication timed out.";

		internal TimeSpan ProcessTimeout { get; private set; }

		internal bool UseLegacyPowerShell { get; set; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		internal string TenantId { get; }

		internal string[] AdditionallyAllowedTenantIds { get; }

		public AzurePowerShellCredential()
			: this(null, null, null)
		{
		}

		public AzurePowerShellCredential(AzurePowerShellCredentialOptions options)
			: this(options, null, null)
		{
		}

		internal AzurePowerShellCredential(AzurePowerShellCredentialOptions options, CredentialPipeline pipeline, IProcessService processService)
		{
			UseLegacyPowerShell = false;
			_logPII = options?.IsUnsafeSupportLoggingEnabled ?? false;
			_logAccountDetails = options?.Diagnostics?.IsAccountIdentifierLoggingEnabled == true;
			TenantId = Validations.ValidateTenantId(options?.TenantId, "options.TenantId", allowNull: true);
			_pipeline = pipeline ?? CredentialPipeline.GetInstance(options);
			_processService = processService ?? ProcessService.Default;
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds(((ISupportsAdditionallyAllowedTenants)options)?.AdditionallyAllowedTenants);
			ProcessTimeout = options?.ProcessTimeout ?? TimeSpan.FromSeconds(10.0);
			_isChainedCredential = options?.IsChainedCredential ?? false;
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return GetTokenImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await GetTokenImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("AzurePowerShellCredential.GetToken", requestContext);
			try
			{
				AccessToken token = await RequestAzurePowerShellAccessTokenAsync(async, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (_logAccountDetails)
				{
					(string, string, string, string) tuple = TokenHelper.ParseAccountInfoFromToken(token.Token);
					AzureIdentityEventSource.Singleton.AuthenticatedAccountDetails(tuple.Item1, tuple.Item2 ?? TenantId, tuple.Item3, tuple.Item4);
				}
				return scope.Succeeded(token);
			}
			catch (CredentialUnavailableException ex) when (!UseLegacyPowerShell && ex.Message == "PowerShell is not installed." && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				UseLegacyPowerShell = true;
				try
				{
					AccessToken token2 = await RequestAzurePowerShellAccessTokenAsync(async, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					if (_logAccountDetails)
					{
						AzureIdentityEventSource.Singleton.AuthenticatedAccountDetails(null, TenantId, null, null);
					}
					return scope.Succeeded(token2);
				}
				catch (Exception ex2)
				{
					throw scope.FailWrapAndThrow(ex2, null, _isChainedCredential);
				}
			}
			catch (Exception ex3)
			{
				throw scope.FailWrapAndThrow(ex3, null, _isChainedCredential);
			}
		}

		private async ValueTask<AccessToken> RequestAzurePowerShellAccessTokenAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			string text = ScopeUtilities.ScopesToResource(context.Scopes);
			string tenantId = TenantIdResolver.Resolve(TenantId, context, AdditionallyAllowedTenantIds);
			Validations.ValidateTenantId(tenantId, "TenantId", allowNull: true);
			ScopeUtilities.ValidateScope(text);
			GetFileNameAndArguments(text, tenantId, out var fileName, out var argument);
			ProcessStartInfo azurePowerShellProcessStartInfo = GetAzurePowerShellProcessStartInfo(fileName, argument);
			using ProcessRunner processRunner = new ProcessRunner(_processService.Create(azurePowerShellProcessStartInfo), ProcessTimeout, _logPII, cancellationToken);
			string output;
			try
			{
				string text2 = ((!async) ? processRunner.Run() : (await processRunner.RunAsync().ConfigureAwait(continueOnCapturedContext: false)));
				output = text2;
				CheckForErrors(output, processRunner.ExitCode);
				ValidateResult(output);
			}
			catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
			{
				throw new AuthenticationFailedException("Azure PowerShell authentication timed out.");
			}
			catch (InvalidOperationException ex2)
			{
				CheckForErrors(ex2.Message, processRunner.ExitCode);
				if (_isChainedCredential)
				{
					throw new CredentialUnavailableException("Azure PowerShell authentication failed due to an unknown error. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/powershellcredential/troubleshoot " + ex2.Message);
				}
				throw new AuthenticationFailedException("Azure PowerShell authentication failed due to an unknown error. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/powershellcredential/troubleshoot " + ex2.Message);
			}
			return DeserializeOutput(output);
		}

		private static void CheckForErrors(string output, int exitCode)
		{
			int num = (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? 9009 : 127);
			if ((exitCode == num || output.IndexOf("not found", StringComparison.OrdinalIgnoreCase) != -1 || output.IndexOf("is not recognized", StringComparison.OrdinalIgnoreCase) != -1) && output.IndexOf("AADSTS", StringComparison.OrdinalIgnoreCase) == -1)
			{
				throw new CredentialUnavailableException("PowerShell is not installed.");
			}
			if (output.IndexOf("NoAzAccountModule", StringComparison.OrdinalIgnoreCase) != -1)
			{
				throw new CredentialUnavailableException("Az.Accounts module >= 2.2.0 is not installed.");
			}
			if (output.IndexOf("Run Connect-AzAccount to login", StringComparison.OrdinalIgnoreCase) != -1 || output.IndexOf("No accounts were found in the cache", StringComparison.OrdinalIgnoreCase) != -1 || output.IndexOf("cannot retrieve access token", StringComparison.OrdinalIgnoreCase) != -1)
			{
				throw new CredentialUnavailableException("Please run 'Connect-AzAccount' to set up account.");
			}
		}

		private static void ValidateResult(string output)
		{
			if (output.IndexOf("<Property Name=\"Token\" Type=\"System.String\">", StringComparison.OrdinalIgnoreCase) < 0)
			{
				throw new CredentialUnavailableException("PowerShell did not return a valid response.");
			}
		}

		private static ProcessStartInfo GetAzurePowerShellProcessStartInfo(string fileName, string argument)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = fileName;
			processStartInfo.Arguments = argument;
			processStartInfo.UseShellExecute = false;
			processStartInfo.ErrorDialog = false;
			processStartInfo.CreateNoWindow = true;
			processStartInfo.WorkingDirectory = DefaultWorkingDir;
			processStartInfo.Environment["POWERSHELL_UPDATECHECK"] = "Off";
			return processStartInfo;
		}

		private void GetFileNameAndArguments(string resource, string tenantId, out string fileName, out string argument)
		{
			string text = "pwsh -NoProfile -NonInteractive -EncodedCommand";
			if (UseLegacyPowerShell)
			{
				text = "powershell -NoProfile -NonInteractive -EncodedCommand";
			}
			string text2 = ((tenantId == null) ? string.Empty : (" -TenantId " + tenantId));
			string text3 = Base64Encode("\r\n$ErrorActionPreference = 'Stop'\r\n[version]$minimumVersion = '2.2.0'\r\n\r\n$m = Import-Module Az.Accounts -MinimumVersion $minimumVersion -PassThru -ErrorAction SilentlyContinue\r\n\r\nif (! $m) {\r\n    Write-Output 'NoAzAccountModule'\r\n    exit\r\n}\r\n\r\n$token = Get-AzAccessToken -ResourceUrl '" + resource + "'" + text2 + "\r\n$customToken = New-Object -TypeName psobject\r\n$customToken | Add-Member -MemberType NoteProperty -Name Token -Value $token.Token\r\n$customToken | Add-Member -MemberType NoteProperty -Name ExpiresOn -Value $token.ExpiresOn.ToUnixTimeSeconds()\r\n\r\n$x = $customToken | ConvertTo-Xml\r\nreturn $x.Objects.FirstChild.OuterXml\r\n");
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				fileName = Path.Combine(DefaultWorkingDirWindows, "cmd.exe");
				argument = "/d /c \"" + text + " \"" + text3 + "\" \" & exit";
			}
			else
			{
				fileName = "/bin/sh";
				argument = "-c \"" + text + " \"" + text3 + "\" \"";
			}
		}

		private static AccessToken DeserializeOutput(string output)
		{
			XDocument xDocument = XDocument.Parse(output);
			string text = null;
			DateTimeOffset dateTimeOffset = default(DateTimeOffset);
			if (xDocument?.Root == null)
			{
				throw new CredentialUnavailableException("Error parsing token response.");
			}
			foreach (XElement item in xDocument.Root.Elements())
			{
				string text2 = item.Attribute("Name")?.Value;
				if (!(text2 == "Token"))
				{
					if (text2 == "ExpiresOn")
					{
						dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(item.Value));
					}
				}
				else
				{
					text = item.Value;
				}
				if (dateTimeOffset != default(DateTimeOffset) && text != null)
				{
					break;
				}
			}
			if (text == null)
			{
				throw new CredentialUnavailableException("Error parsing token response.");
			}
			return new AccessToken(text, dateTimeOffset);
		}

		private static string Base64Encode(string text)
		{
			return Convert.ToBase64String(Encoding.Unicode.GetBytes(text));
		}
	}
	public class AzurePowerShellCredentialOptions : TokenCredentialOptions, ISupportsAdditionallyAllowedTenants
	{
		public string TenantId { get; set; }

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		public TimeSpan? ProcessTimeout { get; set; } = TimeSpan.FromSeconds(10.0);
	}
	public class BrowserCustomizationOptions
	{
		internal SystemWebViewOptions SystemBrowserOptions;

		public bool? UseEmbeddedWebView { get; set; }

		private SystemWebViewOptions systemWebViewOptions
		{
			get
			{
				if (SystemBrowserOptions == null)
				{
					SystemBrowserOptions = new SystemWebViewOptions();
				}
				return SystemBrowserOptions;
			}
		}

		public string SuccessMessage
		{
			get
			{
				return systemWebViewOptions.HtmlMessageSuccess;
			}
			set
			{
				systemWebViewOptions.HtmlMessageSuccess = value;
			}
		}

		public string ErrorMessage
		{
			get
			{
				return systemWebViewOptions.HtmlMessageError;
			}
			set
			{
				systemWebViewOptions.HtmlMessageError = value;
			}
		}
	}
	public class ChainedTokenCredential : TokenCredential
	{
		private const string AggregateAllUnavailableErrorMessage = "The ChainedTokenCredential failed to retrieve a token from the included credentials.";

		private const string AuthenticationFailedErrorMessage = "The ChainedTokenCredential failed due to an unhandled exception: ";

		private readonly TokenCredential[] _sources;

		protected ChainedTokenCredential()
		{
			_sources = Array.Empty<TokenCredential>();
		}

		public ChainedTokenCredential(params TokenCredential[] sources)
		{
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			if (sources.Length == 0)
			{
				throw new ArgumentException("sources must not be empty", "sources");
			}
			for (int i = 0; i < sources.Length; i++)
			{
				if (sources[i] == null)
				{
					throw new ArgumentException("sources must not contain null", "sources");
				}
			}
			_sources = sources;
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetTokenImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await GetTokenImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			ScopeGroupHandler groupScopeHandler = new ScopeGroupHandler(null);
			try
			{
				List<CredentialUnavailableException> exceptions = new List<CredentialUnavailableException>();
				TokenCredential[] sources = _sources;
				foreach (TokenCredential tokenCredential in sources)
				{
					try
					{
						AccessToken result = ((!async) ? tokenCredential.GetToken(requestContext, cancellationToken) : (await tokenCredential.GetTokenAsync(requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
						groupScopeHandler.Dispose(null, default(Azure.Core.Pipeline.DiagnosticScope));
						return result;
					}
					catch (CredentialUnavailableException item)
					{
						exceptions.Add(item);
					}
					catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
					{
						throw new AuthenticationFailedException("The ChainedTokenCredential failed due to an unhandled exception: " + ex.Message, ex);
					}
				}
				throw CredentialUnavailableException.CreateAggregateException("The ChainedTokenCredential failed to retrieve a token from the included credentials.", exceptions);
			}
			catch (Exception exception)
			{
				groupScopeHandler.Fail(null, default(Azure.Core.Pipeline.DiagnosticScope), exception);
				throw;
			}
		}
	}
	public class ClientAssertionCredential : TokenCredential
	{
		internal readonly string[] AdditionallyAllowedTenantIds;

		internal string TenantId { get; }

		internal string ClientId { get; }

		internal MsalConfidentialClient Client { get; }

		internal CredentialPipeline Pipeline { get; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		protected ClientAssertionCredential()
		{
		}

		public ClientAssertionCredential(string tenantId, string clientId, Func<CancellationToken, Task<string>> assertionCallback, ClientAssertionCredentialOptions options = null)
		{
			Azure.Core.Argument.AssertNotNull(clientId, "clientId");
			TenantId = Validations.ValidateTenantId(tenantId, "tenantId");
			ClientId = clientId;
			Pipeline = options?.Pipeline ?? CredentialPipeline.GetInstance(options);
			Client = options?.MsalClient ?? new MsalConfidentialClient(Pipeline, tenantId, clientId, assertionCallback, options);
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds(((ISupportsAdditionallyAllowedTenants)options)?.AdditionallyAllowedTenants);
		}

		public ClientAssertionCredential(string tenantId, string clientId, Func<string> assertionCallback, ClientAssertionCredentialOptions options = null)
		{
			Azure.Core.Argument.AssertNotNull(clientId, "clientId");
			TenantId = Validations.ValidateTenantId(tenantId, "tenantId");
			ClientId = clientId;
			Client = options?.MsalClient ?? new MsalConfidentialClient(options?.Pipeline ?? CredentialPipeline.GetInstance(options), tenantId, clientId, assertionCallback, options);
			Pipeline = options?.Pipeline ?? options?.Pipeline ?? CredentialPipeline.GetInstance(options);
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds(((ISupportsAdditionallyAllowedTenants)options)?.AdditionallyAllowedTenants);
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			using CredentialDiagnosticScope credentialDiagnosticScope = Pipeline.StartGetTokenScope("ClientAssertionCredential.GetToken", requestContext);
			try
			{
				string tenantId = TenantIdResolver.Resolve(TenantId, requestContext, AdditionallyAllowedTenantIds);
				AuthenticationResult result = Client.AcquireTokenForClientAsync(requestContext.Scopes, tenantId, requestContext.Claims, requestContext.IsCaeEnabled, async: false, cancellationToken).EnsureCompleted();
				return credentialDiagnosticScope.Succeeded(result.ToAccessToken());
			}
			catch (Exception ex)
			{
				throw credentialDiagnosticScope.FailWrapAndThrow(ex);
			}
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			using CredentialDiagnosticScope scope = Pipeline.StartGetTokenScope("ClientAssertionCredential.GetToken", requestContext);
			try
			{
				string tenantId = TenantIdResolver.Resolve(TenantId, requestContext, AdditionallyAllowedTenantIds);
				return scope.Succeeded((await Client.AcquireTokenForClientAsync(requestContext.Scopes, tenantId, requestContext.Claims, requestContext.IsCaeEnabled, async: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).ToAccessToken());
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex);
			}
		}
	}
	public class ClientAssertionCredentialOptions : TokenCredentialOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants, ISupportsTokenCachePersistenceOptions
	{
		internal CredentialPipeline Pipeline { get; set; }

		internal MsalConfidentialClient MsalClient { get; set; }

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		public bool DisableInstanceDiscovery { get; set; }

		public TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }
	}
	public class ClientCertificateCredential : TokenCredential
	{
		internal const string Troubleshooting = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/clientcertificatecredential/troubleshoot";

		private readonly CredentialPipeline _pipeline;

		internal readonly string[] AdditionallyAllowedTenantIds;

		internal string TenantId { get; }

		internal string ClientId { get; }

		internal IX509Certificate2Provider ClientCertificateProvider { get; }

		internal MsalConfidentialClient Client { get; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		protected ClientCertificateCredential()
		{
		}

		public ClientCertificateCredential(string tenantId, string clientId, string clientCertificatePath)
			: this(tenantId, clientId, clientCertificatePath, null, null, null, null)
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public ClientCertificateCredential(string tenantId, string clientId, string clientCertificatePath, TokenCredentialOptions options)
			: this(tenantId, clientId, clientCertificatePath, null, options, null, null)
		{
		}

		public ClientCertificateCredential(string tenantId, string clientId, string clientCertificatePath, ClientCertificateCredentialOptions options)
			: this(tenantId, clientId, clientCertificatePath, null, options, null, null)
		{
		}

		public ClientCertificateCredential(string tenantId, string clientId, X509Certificate2 clientCertificate)
			: this(tenantId, clientId, clientCertificate, null, null, null)
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public ClientCertificateCredential(string tenantId, string clientId, X509Certificate2 clientCertificate, TokenCredentialOptions options)
			: this(tenantId, clientId, clientCertificate, options, null, null)
		{
		}

		public ClientCertificateCredential(string tenantId, string clientId, X509Certificate2 clientCertificate, ClientCertificateCredentialOptions options)
			: this(tenantId, clientId, clientCertificate, options, null, null)
		{
		}

		internal ClientCertificateCredential(string tenantId, string clientId, string certificatePath, string certificatePassword, TokenCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
			: this(tenantId, clientId, new X509Certificate2FromFileProvider(certificatePath ?? throw new ArgumentNullException("certificatePath"), certificatePassword), options, pipeline, client)
		{
		}

		internal ClientCertificateCredential(string tenantId, string clientId, X509Certificate2 certificate, TokenCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
			: this(tenantId, clientId, new X509Certificate2FromObjectProvider(certificate ?? throw new ArgumentNullException("certificate")), options, pipeline, client)
		{
		}

		internal ClientCertificateCredential(string tenantId, string clientId, IX509Certificate2Provider certificateProvider, TokenCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
		{
			TenantId = Validations.ValidateTenantId(tenantId, "tenantId");
			ClientId = clientId ?? throw new ArgumentNullException("clientId");
			ClientCertificateProvider = certificateProvider;
			_pipeline = pipeline ?? CredentialPipeline.GetInstance(options);
			ClientCertificateCredentialOptions clientCertificateCredentialOptions = options as ClientCertificateCredentialOptions;
			Client = client ?? new MsalConfidentialClient(_pipeline, tenantId, clientId, certificateProvider, clientCertificateCredentialOptions?.SendCertificateChain ?? false, options);
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds((options as ISupportsAdditionallyAllowedTenants)?.AdditionallyAllowedTenants);
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			using CredentialDiagnosticScope credentialDiagnosticScope = _pipeline.StartGetTokenScope("ClientCertificateCredential.GetToken", requestContext);
			try
			{
				string tenantId = TenantIdResolver.Resolve(TenantId, requestContext, AdditionallyAllowedTenantIds);
				AuthenticationResult result = Client.AcquireTokenForClientAsync(requestContext.Scopes, tenantId, requestContext.Claims, requestContext.IsCaeEnabled, async: false, cancellationToken).EnsureCompleted();
				return credentialDiagnosticScope.Succeeded(result.ToAccessToken());
			}
			catch (Exception ex)
			{
				throw credentialDiagnosticScope.FailWrapAndThrow(ex, "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/clientcertificatecredential/troubleshoot");
			}
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("ClientCertificateCredential.GetToken", requestContext);
			try
			{
				string tenantId = TenantIdResolver.Resolve(TenantId, requestContext, AdditionallyAllowedTenantIds);
				return scope.Succeeded((await Client.AcquireTokenForClientAsync(requestContext.Scopes, tenantId, requestContext.Claims, requestContext.IsCaeEnabled, async: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).ToAccessToken());
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex);
			}
		}
	}
	public class ClientCertificateCredentialOptions : TokenCredentialOptions, ISupportsTokenCachePersistenceOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		public TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }

		public bool SendCertificateChain { get; set; }

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		public bool DisableInstanceDiscovery { get; set; }
	}
	public class ClientSecretCredential : TokenCredential
	{
		private readonly CredentialPipeline _pipeline;

		internal readonly string[] AdditionallyAllowedTenantIds;

		internal MsalConfidentialClient Client { get; }

		internal string TenantId { get; }

		internal string ClientId { get; }

		internal string ClientSecret { get; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		protected ClientSecretCredential()
		{
		}

		public ClientSecretCredential(string tenantId, string clientId, string clientSecret)
			: this(tenantId, clientId, clientSecret, null, null, null)
		{
		}

		public ClientSecretCredential(string tenantId, string clientId, string clientSecret, ClientSecretCredentialOptions options)
			: this(tenantId, clientId, clientSecret, options, null, null)
		{
		}

		public ClientSecretCredential(string tenantId, string clientId, string clientSecret, TokenCredentialOptions options)
			: this(tenantId, clientId, clientSecret, options, null, null)
		{
		}

		internal ClientSecretCredential(string tenantId, string clientId, string clientSecret, TokenCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
		{
			Azure.Core.Argument.AssertNotNull(clientId, "clientId");
			Azure.Core.Argument.AssertNotNull(clientSecret, "clientSecret");
			TenantId = Validations.ValidateTenantId(tenantId, "tenantId");
			ClientId = clientId ?? throw new ArgumentNullException("clientId");
			ClientSecret = clientSecret;
			_pipeline = pipeline ?? CredentialPipeline.GetInstance(options);
			Client = client ?? new MsalConfidentialClient(_pipeline, tenantId, clientId, clientSecret, null, options);
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds((options as ISupportsAdditionallyAllowedTenants)?.AdditionallyAllowedTenants);
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("ClientSecretCredential.GetToken", requestContext);
			try
			{
				string tenantId = TenantIdResolver.Resolve(TenantId, requestContext, AdditionallyAllowedTenantIds);
				return scope.Succeeded((await Client.AcquireTokenForClientAsync(requestContext.Scopes, tenantId, requestContext.Claims, requestContext.IsCaeEnabled, async: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).ToAccessToken());
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex);
			}
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			using CredentialDiagnosticScope credentialDiagnosticScope = _pipeline.StartGetTokenScope("ClientSecretCredential.GetToken", requestContext);
			try
			{
				string tenantId = TenantIdResolver.Resolve(TenantId, requestContext, AdditionallyAllowedTenantIds);
				AuthenticationResult result = Client.AcquireTokenForClientAsync(requestContext.Scopes, tenantId, requestContext.Claims, requestContext.IsCaeEnabled, async: false, cancellationToken).EnsureCompleted();
				return credentialDiagnosticScope.Succeeded(result.ToAccessToken());
			}
			catch (Exception ex)
			{
				throw credentialDiagnosticScope.FailWrapAndThrow(ex);
			}
		}
	}
	public class ClientSecretCredentialOptions : TokenCredentialOptions, ISupportsTokenCachePersistenceOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		public TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		public bool DisableInstanceDiscovery { get; set; }
	}
	public class DefaultAzureCredential : TokenCredential
	{
		private const string Troubleshooting = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/defaultazurecredential/troubleshoot";

		private const string DefaultExceptionMessage = "DefaultAzureCredential failed to retrieve a token from the included credentials. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/defaultazurecredential/troubleshoot";

		private const string UnhandledExceptionMessage = "DefaultAzureCredential authentication failed due to an unhandled exception: ";

		private readonly CredentialPipeline _pipeline;

		private readonly Azure.Core.AsyncLockWithValue<TokenCredential> _credentialLock;

		internal TokenCredential[] _sources;

		protected DefaultAzureCredential()
			: this(includeInteractiveCredentials: false)
		{
		}

		public DefaultAzureCredential(bool includeInteractiveCredentials = false)
			: this(includeInteractiveCredentials ? new DefaultAzureCredentialOptions
			{
				ExcludeInteractiveBrowserCredential = false
			} : null)
		{
		}

		public DefaultAzureCredential(DefaultAzureCredentialOptions options)
			: this(new DefaultAzureCredentialFactory(ValidateAuthorityHostOption(options)))
		{
		}

		internal DefaultAzureCredential(DefaultAzureCredentialFactory factory)
		{
			_pipeline = factory.Pipeline;
			_sources = factory.CreateCredentialChain();
			_credentialLock = new Azure.Core.AsyncLockWithValue<TokenCredential>();
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetTokenImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await GetTokenImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScopeGroup("DefaultAzureCredential.GetToken", requestContext);
			_ = 2;
			try
			{
				using Azure.Core.AsyncLockWithValue<TokenCredential>.LockOrValue asyncLock = await _credentialLock.GetLockOrValueAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				AccessToken token;
				if (asyncLock.HasValue)
				{
					token = await GetTokenFromCredentialAsync(asyncLock.Value, requestContext, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
				else
				{
					(AccessToken, TokenCredential) obj = await GetTokenFromSourcesAsync(_sources, requestContext, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					token = obj.Item1;
					TokenCredential item = obj.Item2;
					_sources = null;
					asyncLock.SetValue(item);
					AzureIdentityEventSource.Singleton.DefaultAzureCredentialCredentialSelected(item.GetType().FullName);
				}
				return scope.Succeeded(token);
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex);
			}
		}

		private static async ValueTask<AccessToken> GetTokenFromCredentialAsync(TokenCredential credential, TokenRequestContext requestContext, bool async, CancellationToken cancellationToken)
		{
			try
			{
				return (!async) ? credential.GetToken(requestContext, cancellationToken) : (await credential.GetTokenAsync(requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
			}
			catch (Exception ex) when (!(ex is CredentialUnavailableException))
			{
				throw new AuthenticationFailedException("DefaultAzureCredential authentication failed due to an unhandled exception: ", ex);
			}
		}

		private static async ValueTask<(AccessToken Token, TokenCredential Credential)> GetTokenFromSourcesAsync(TokenCredential[] sources, TokenRequestContext requestContext, bool async, CancellationToken cancellationToken)
		{
			List<CredentialUnavailableException> exceptions = new List<CredentialUnavailableException>();
			for (int i = 0; i < sources.Length && sources[i] != null; i++)
			{
				try
				{
					AccessToken item = ((!async) ? sources[i].GetToken(requestContext, cancellationToken) : (await sources[i].GetTokenAsync(requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
					return (item, sources[i]);
				}
				catch (CredentialUnavailableException item2)
				{
					exceptions.Add(item2);
				}
			}
			throw CredentialUnavailableException.CreateAggregateException("DefaultAzureCredential failed to retrieve a token from the included credentials. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/defaultazurecredential/troubleshoot", exceptions);
		}

		private static DefaultAzureCredentialOptions ValidateAuthorityHostOption(DefaultAzureCredentialOptions options)
		{
			Validations.ValidateAuthorityHost(options?.AuthorityHost ?? AzureAuthorityHosts.GetDefault());
			return options;
		}
	}
	public class DefaultAzureCredentialOptions : TokenCredentialOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		private struct UpdateTracker<T>
		{
			private bool _updated;

			private T _value;

			public T Value
			{
				get
				{
					return _value;
				}
				set
				{
					_value = value;
					_updated = true;
				}
			}

			public bool Updated => _updated;

			public UpdateTracker(T initialValue)
			{
				_value = initialValue;
				_updated = false;
			}
		}

		private UpdateTracker<string> _tenantId = new UpdateTracker<string>(EnvironmentVariables.TenantId);

		private UpdateTracker<string> _interactiveBrowserTenantId = new UpdateTracker<string>(EnvironmentVariables.TenantId);

		private UpdateTracker<string> _sharedTokenCacheTenantId = new UpdateTracker<string>(EnvironmentVariables.TenantId);

		private UpdateTracker<string> _visualStudioTenantId = new UpdateTracker<string>(EnvironmentVariables.TenantId);

		private UpdateTracker<string> _visualStudioCodeTenantId = new UpdateTracker<string>(EnvironmentVariables.TenantId);

		public string TenantId
		{
			get
			{
				return _tenantId.Value;
			}
			set
			{
				if (_interactiveBrowserTenantId.Updated && value != _interactiveBrowserTenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and InteractiveBrowserTenantId. TenantId is preferred, and is functionally equivalent. InteractiveBrowserTenantId exists only to provide backwards compatibility.");
				}
				if (_sharedTokenCacheTenantId.Updated && value != _sharedTokenCacheTenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and SharedTokenCacheTenantId. TenantId is preferred, and is functionally equivalent. SharedTokenCacheTenantId exists only to provide backwards compatibility.");
				}
				if (_visualStudioTenantId.Updated && value != _visualStudioTenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and VisualStudioTenantId. TenantId is preferred, and is functionally equivalent. VisualStudioTenantId exists only to provide backwards compatibility.");
				}
				if (_visualStudioCodeTenantId.Updated && value != _visualStudioCodeTenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and VisualStudioCodeTenantId. TenantId is preferred, and is functionally equivalent. VisualStudioCodeTenantId exists only to provide backwards compatibility.");
				}
				_tenantId.Value = value;
				_interactiveBrowserTenantId.Value = value;
				_sharedTokenCacheTenantId.Value = value;
				_visualStudioCodeTenantId.Value = value;
				_visualStudioTenantId.Value = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public string InteractiveBrowserTenantId
		{
			get
			{
				return _interactiveBrowserTenantId.Value;
			}
			set
			{
				if (_tenantId.Updated && value != _tenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and InteractiveBrowserTenantId. TenantId is preferred, and is functionally equivalent. InteractiveBrowserTenantId exists only to provide backwards compatibility.");
				}
				_interactiveBrowserTenantId.Value = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public string SharedTokenCacheTenantId
		{
			get
			{
				return _sharedTokenCacheTenantId.Value;
			}
			set
			{
				if (_tenantId.Updated && value != _tenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and SharedTokenCacheTenantId. TenantId is preferred, and is functionally equivalent. SharedTokenCacheTenantId exists only to provide backwards compatibility.");
				}
				_sharedTokenCacheTenantId.Value = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public string VisualStudioTenantId
		{
			get
			{
				return _visualStudioTenantId.Value;
			}
			set
			{
				if (_tenantId.Updated && value != _tenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and VisualStudioTenantId. TenantId is preferred, and is functionally equivalent. VisualStudioTenantId exists only to provide backwards compatibility.");
				}
				_visualStudioTenantId.Value = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public string VisualStudioCodeTenantId
		{
			get
			{
				return _visualStudioCodeTenantId.Value;
			}
			set
			{
				if (_tenantId.Updated && value != _tenantId.Value)
				{
					throw new InvalidOperationException("Applications should not set both TenantId and VisualStudioCodeTenantId. TenantId is preferred, and is functionally equivalent. VisualStudioCodeTenantId exists only to provide backwards compatibility.");
				}
				_visualStudioCodeTenantId.Value = value;
			}
		}

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = EnvironmentVariables.AdditionallyAllowedTenants;

		public string SharedTokenCacheUsername { get; set; } = EnvironmentVariables.Username;

		public string InteractiveBrowserCredentialClientId { get; set; }

		public string WorkloadIdentityClientId { get; set; } = EnvironmentVariables.ClientId;

		public string ManagedIdentityClientId { get; set; } = EnvironmentVariables.ClientId;

		public ResourceIdentifier ManagedIdentityResourceId { get; set; }

		public TimeSpan? CredentialProcessTimeout { get; set; } = TimeSpan.FromSeconds(30.0);

		public bool ExcludeEnvironmentCredential { get; set; }

		public bool ExcludeWorkloadIdentityCredential { get; set; }

		public bool ExcludeManagedIdentityCredential { get; set; }

		public bool ExcludeAzureDeveloperCliCredential { get; set; }

		public bool ExcludeSharedTokenCacheCredential { get; set; } = true;

		public bool ExcludeInteractiveBrowserCredential { get; set; } = true;

		public bool ExcludeAzureCliCredential { get; set; }

		public bool ExcludeVisualStudioCredential { get; set; }

		public bool ExcludeVisualStudioCodeCredential { get; set; } = true;

		public bool ExcludeAzurePowerShellCredential { get; set; }

		public bool DisableInstanceDiscovery { get; set; }

		internal override T Clone<T>()
		{
			T val = base.Clone<T>();
			if (val is DefaultAzureCredentialOptions defaultAzureCredentialOptions)
			{
				defaultAzureCredentialOptions._tenantId = _tenantId;
				defaultAzureCredentialOptions._interactiveBrowserTenantId = _interactiveBrowserTenantId;
				defaultAzureCredentialOptions._sharedTokenCacheTenantId = _sharedTokenCacheTenantId;
				defaultAzureCredentialOptions._visualStudioTenantId = _visualStudioTenantId;
				defaultAzureCredentialOptions._visualStudioCodeTenantId = _visualStudioCodeTenantId;
				defaultAzureCredentialOptions.SharedTokenCacheUsername = SharedTokenCacheUsername;
				defaultAzureCredentialOptions.InteractiveBrowserCredentialClientId = InteractiveBrowserCredentialClientId;
				defaultAzureCredentialOptions.WorkloadIdentityClientId = WorkloadIdentityClientId;
				defaultAzureCredentialOptions.ManagedIdentityClientId = ManagedIdentityClientId;
				defaultAzureCredentialOptions.ManagedIdentityResourceId = ManagedIdentityResourceId;
				defaultAzureCredentialOptions.CredentialProcessTimeout = CredentialProcessTimeout;
				defaultAzureCredentialOptions.ExcludeEnvironmentCredential = ExcludeEnvironmentCredential;
				defaultAzureCredentialOptions.ExcludeWorkloadIdentityCredential = ExcludeWorkloadIdentityCredential;
				defaultAzureCredentialOptions.ExcludeManagedIdentityCredential = ExcludeManagedIdentityCredential;
				defaultAzureCredentialOptions.ExcludeAzureDeveloperCliCredential = ExcludeAzureDeveloperCliCredential;
				defaultAzureCredentialOptions.ExcludeSharedTokenCacheCredential = ExcludeSharedTokenCacheCredential;
				defaultAzureCredentialOptions.ExcludeInteractiveBrowserCredential = ExcludeInteractiveBrowserCredential;
				defaultAzureCredentialOptions.ExcludeAzureCliCredential = ExcludeAzureCliCredential;
				defaultAzureCredentialOptions.ExcludeVisualStudioCredential = ExcludeVisualStudioCredential;
				defaultAzureCredentialOptions.ExcludeVisualStudioCodeCredential = ExcludeVisualStudioCodeCredential;
				defaultAzureCredentialOptions.ExcludeAzurePowerShellCredential = ExcludeAzurePowerShellCredential;
			}
			return val;
		}
	}
	public class DeviceCodeCredential : TokenCredential
	{
		private readonly string _tenantId;

		internal readonly string[] AdditionallyAllowedTenantIds;

		private const string AuthenticationRequiredMessage = "Interactive authentication is needed to acquire token. Call Authenticate to initiate the device code authentication.";

		private const string NoDefaultScopeMessage = "Authenticating in this environment requires specifying a TokenRequestContext.";

		internal MsalPublicClient Client { get; set; }

		internal string ClientId { get; }

		internal bool DisableAutomaticAuthentication { get; }

		internal AuthenticationRecord Record { get; private set; }

		internal Func<DeviceCodeInfo, CancellationToken, Task> DeviceCodeCallback { get; }

		internal CredentialPipeline Pipeline { get; }

		internal string DefaultScope { get; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		public DeviceCodeCredential()
			: this(DefaultDeviceCodeHandler, null, "04b07795-8ddb-461a-bbee-02f9e1bf7b46", null, null)
		{
		}

		public DeviceCodeCredential(DeviceCodeCredentialOptions options)
			: this(options?.DeviceCodeCallback ?? new Func<DeviceCodeInfo, CancellationToken, Task>(DefaultDeviceCodeHandler), options?.TenantId, options?.ClientId ?? "04b07795-8ddb-461a-bbee-02f9e1bf7b46", options, null)
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public DeviceCodeCredential(Func<DeviceCodeInfo, CancellationToken, Task> deviceCodeCallback, string clientId, TokenCredentialOptions options = null)
			: this(deviceCodeCallback, null, clientId, options, null)
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public DeviceCodeCredential(Func<DeviceCodeInfo, CancellationToken, Task> deviceCodeCallback, string tenantId, string clientId, TokenCredentialOptions options = null)
			: this(deviceCodeCallback, Validations.ValidateTenantId(tenantId, "tenantId", allowNull: true), clientId, options, null)
		{
		}

		internal DeviceCodeCredential(Func<DeviceCodeInfo, CancellationToken, Task> deviceCodeCallback, string tenantId, string clientId, TokenCredentialOptions options, CredentialPipeline pipeline)
			: this(deviceCodeCallback, tenantId, clientId, options, pipeline, null)
		{
		}

		internal DeviceCodeCredential(Func<DeviceCodeInfo, CancellationToken, Task> deviceCodeCallback, string tenantId, string clientId, TokenCredentialOptions options, CredentialPipeline pipeline, MsalPublicClient client)
		{
			Azure.Core.Argument.AssertNotNull(clientId, "clientId");
			Azure.Core.Argument.AssertNotNull(deviceCodeCallback, "deviceCodeCallback");
			_tenantId = tenantId;
			ClientId = clientId;
			DeviceCodeCallback = deviceCodeCallback;
			DisableAutomaticAuthentication = (options as DeviceCodeCredentialOptions)?.DisableAutomaticAuthentication ?? false;
			Record = (options as DeviceCodeCredentialOptions)?.AuthenticationRecord;
			Pipeline = pipeline ?? CredentialPipeline.GetInstance(options);
			DefaultScope = AzureAuthorityHosts.GetDefaultScope(options?.AuthorityHost ?? AzureAuthorityHosts.GetDefault());
			Client = client ?? new MsalPublicClient(Pipeline, tenantId, ClientId, AzureAuthorityHosts.GetDeviceCodeRedirectUri(options?.AuthorityHost ?? AzureAuthorityHosts.GetDefault()).AbsoluteUri, options);
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds((options as ISupportsAdditionallyAllowedTenants)?.AdditionallyAllowedTenants);
		}

		public virtual AuthenticationRecord Authenticate(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (DefaultScope == null)
			{
				throw new CredentialUnavailableException("Authenticating in this environment requires specifying a TokenRequestContext.");
			}
			return Authenticate(new TokenRequestContext(new string[1] { DefaultScope }), cancellationToken);
		}

		public virtual async Task<AuthenticationRecord> AuthenticateAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (DefaultScope == null)
			{
				throw new CredentialUnavailableException("Authenticating in this environment requires specifying a TokenRequestContext.");
			}
			return await AuthenticateAsync(new TokenRequestContext(new string[1] { DefaultScope }), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public virtual AuthenticationRecord Authenticate(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return AuthenticateImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public virtual async Task<AuthenticationRecord> AuthenticateAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await AuthenticateImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetTokenImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await GetTokenImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		internal static Task DefaultDeviceCodeHandler(DeviceCodeInfo deviceCodeInfo, CancellationToken cancellationToken)
		{
			Console.WriteLine(deviceCodeInfo.Message);
			return Task.CompletedTask;
		}

		private async Task<AuthenticationRecord> AuthenticateImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = Pipeline.StartGetTokenScope("DeviceCodeCredential.Authenticate", requestContext);
			try
			{
				scope.Succeeded(await GetTokenViaDeviceCodeAsync(requestContext, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
				return Record;
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex);
			}
		}

		private async ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = Pipeline.StartGetTokenScope("DeviceCodeCredential.GetToken", requestContext);
			_ = 1;
			try
			{
				Exception innerException = null;
				string tenantId = TenantIdResolver.Resolve(_tenantId, requestContext, AdditionallyAllowedTenantIds);
				if (Record != null)
				{
					try
					{
						return scope.Succeeded((await Client.AcquireTokenSilentAsync(requestContext.Scopes, requestContext.Claims, Record, tenantId, requestContext.IsCaeEnabled, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).ToAccessToken());
					}
					catch (MsalUiRequiredException ex)
					{
						innerException = ex;
					}
				}
				if (DisableAutomaticAuthentication)
				{
					throw new AuthenticationRequiredException("Interactive authentication is needed to acquire token. Call Authenticate to initiate the device code authentication.", requestContext, innerException);
				}
				return scope.Succeeded(await GetTokenViaDeviceCodeAsync(requestContext, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
			}
			catch (Exception ex2)
			{
				throw scope.FailWrapAndThrow(ex2);
			}
		}

		private async Task<AccessToken> GetTokenViaDeviceCodeAsync(TokenRequestContext context, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = await Client.AcquireTokenWithDeviceCodeAsync(context.Scopes, context.Claims, (DeviceCodeResult code) => DeviceCodeCallbackImpl(code, cancellationToken), context.IsCaeEnabled, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			Record = new AuthenticationRecord(authenticationResult, ClientId);
			return authenticationResult.ToAccessToken();
		}

		private Task DeviceCodeCallbackImpl(DeviceCodeResult deviceCode, CancellationToken cancellationToken)
		{
			return DeviceCodeCallback(new DeviceCodeInfo(deviceCode), cancellationToken);
		}
	}
	public class DeviceCodeCredentialOptions : TokenCredentialOptions, ISupportsTokenCachePersistenceOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		private string _tenantId;

		public bool DisableAutomaticAuthentication { get; set; }

		public string TenantId
		{
			get
			{
				return _tenantId;
			}
			set
			{
				_tenantId = Validations.ValidateTenantId(value, null, allowNull: true);
			}
		}

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		public string ClientId { get; set; } = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";

		public TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }

		public AuthenticationRecord AuthenticationRecord { get; set; }

		public Func<DeviceCodeInfo, CancellationToken, Task> DeviceCodeCallback { get; set; }

		public bool DisableInstanceDiscovery { get; set; }
	}
	public class EnvironmentCredential : TokenCredential
	{
		private const string UnavailableErrorMessage = "EnvironmentCredential authentication unavailable. Environment variables are not fully configured. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/environmentcredential/troubleshoot";

		private readonly CredentialPipeline _pipeline;

		internal TokenCredential Credential { get; }

		public EnvironmentCredential()
			: this(CredentialPipeline.GetInstance(null))
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public EnvironmentCredential(TokenCredentialOptions options)
			: this(CredentialPipeline.GetInstance(options), options)
		{
		}

		public EnvironmentCredential(EnvironmentCredentialOptions options)
			: this(CredentialPipeline.GetInstance(options), options)
		{
		}

		internal EnvironmentCredential(CredentialPipeline pipeline, TokenCredentialOptions options = null)
		{
			_pipeline = pipeline;
			options = options ?? new EnvironmentCredentialOptions();
			EnvironmentCredentialOptions environmentCredentialOptions = (options as EnvironmentCredentialOptions) ?? options.Clone<EnvironmentCredentialOptions>();
			string tenantId = environmentCredentialOptions.TenantId;
			string clientId = environmentCredentialOptions.ClientId;
			string clientSecret = environmentCredentialOptions.ClientSecret;
			string clientCertificatePath = environmentCredentialOptions.ClientCertificatePath;
			string clientCertificatePassword = environmentCredentialOptions.ClientCertificatePassword;
			bool sendCertificateChain = environmentCredentialOptions.SendCertificateChain;
			string username = environmentCredentialOptions.Username;
			string password = environmentCredentialOptions.Password;
			if (!string.IsNullOrEmpty(tenantId) && !string.IsNullOrEmpty(clientId))
			{
				if (!string.IsNullOrEmpty(clientSecret))
				{
					Credential = new ClientSecretCredential(tenantId, clientId, clientSecret, environmentCredentialOptions, _pipeline, environmentCredentialOptions.MsalConfidentialClient);
				}
				else if (!string.IsNullOrEmpty(clientCertificatePath))
				{
					ClientCertificateCredentialOptions clientCertificateCredentialOptions = environmentCredentialOptions.Clone<ClientCertificateCredentialOptions>();
					clientCertificateCredentialOptions.SendCertificateChain = sendCertificateChain;
					Credential = new ClientCertificateCredential(tenantId, clientId, clientCertificatePath, clientCertificatePassword, clientCertificateCredentialOptions, _pipeline, environmentCredentialOptions.MsalConfidentialClient);
				}
				else if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
				{
					Credential = new UsernamePasswordCredential(username, password, tenantId, clientId, environmentCredentialOptions, _pipeline, environmentCredentialOptions.MsalPublicClient);
				}
			}
		}

		internal EnvironmentCredential(CredentialPipeline pipeline, TokenCredential credential)
		{
			_pipeline = pipeline;
			Credential = credential;
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetTokenImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await GetTokenImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("EnvironmentCredential.GetToken", requestContext);
			if (Credential == null)
			{
				throw scope.FailWrapAndThrow(new CredentialUnavailableException("EnvironmentCredential authentication unavailable. Environment variables are not fully configured. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/environmentcredential/troubleshoot"));
			}
			try
			{
				AccessToken accessToken = ((!async) ? Credential.GetToken(requestContext, cancellationToken) : (await Credential.GetTokenAsync(requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
				AccessToken token = accessToken;
				return scope.Succeeded(token);
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex);
			}
		}
	}
	public class EnvironmentCredentialOptions : TokenCredentialOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		internal string TenantId { get; set; } = EnvironmentVariables.TenantId;

		internal string ClientId { get; set; } = EnvironmentVariables.ClientId;

		internal string ClientSecret { get; set; } = EnvironmentVariables.ClientSecret;

		internal string ClientCertificatePath { get; set; } = EnvironmentVariables.ClientCertificatePath;

		internal string ClientCertificatePassword { get; set; } = EnvironmentVariables.ClientCertificatePassword;

		internal bool SendCertificateChain { get; set; } = EnvironmentVariables.ClientSendCertificateChain;

		internal string Username { get; set; } = EnvironmentVariables.Username;

		internal string Password { get; set; } = EnvironmentVariables.Password;

		internal MsalConfidentialClient MsalConfidentialClient { get; set; }

		internal MsalPublicClient MsalPublicClient { get; set; }

		public bool DisableInstanceDiscovery { get; set; }

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = EnvironmentVariables.AdditionallyAllowedTenants;
	}
	public class InteractiveBrowserCredential : TokenCredential
	{
		private const string AuthenticationRequiredMessage = "Interactive authentication is needed to acquire token. Call Authenticate to interactively authenticate.";

		private const string NoDefaultScopeMessage = "Authenticating in this environment requires specifying a TokenRequestContext.";

		internal string TenantId { get; }

		internal string[] AdditionallyAllowedTenantIds { get; }

		internal string ClientId { get; }

		internal string LoginHint { get; }

		internal BrowserCustomizationOptions BrowserCustomization { get; }

		internal MsalPublicClient Client { get; }

		internal CredentialPipeline Pipeline { get; }

		internal bool DisableAutomaticAuthentication { get; }

		internal AuthenticationRecord Record { get; set; }

		internal string DefaultScope { get; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		internal bool UseOperatingSystemAccount { get; }

		public InteractiveBrowserCredential()
			: this(null, "04b07795-8ddb-461a-bbee-02f9e1bf7b46", null, null)
		{
		}

		public InteractiveBrowserCredential(InteractiveBrowserCredentialOptions options)
			: this(options?.TenantId, options?.ClientId ?? "04b07795-8ddb-461a-bbee-02f9e1bf7b46", options, null)
		{
			DisableAutomaticAuthentication = options?.DisableAutomaticAuthentication ?? false;
			Record = options?.AuthenticationRecord;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public InteractiveBrowserCredential(string clientId)
			: this(null, clientId, null, null)
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public InteractiveBrowserCredential(string tenantId, string clientId, TokenCredentialOptions options = null)
			: this(Validations.ValidateTenantId(tenantId, "tenantId", allowNull: true), clientId, options, null, null)
		{
			Azure.Core.Argument.AssertNotNull(clientId, "clientId");
		}

		internal InteractiveBrowserCredential(string tenantId, string clientId, TokenCredentialOptions options, CredentialPipeline pipeline)
			: this(tenantId, clientId, options, pipeline, null)
		{
			Azure.Core.Argument.AssertNotNull(clientId, "clientId");
		}

		internal InteractiveBrowserCredential(string tenantId, string clientId, TokenCredentialOptions options, CredentialPipeline pipeline, MsalPublicClient client)
		{
			ClientId = clientId;
			TenantId = tenantId;
			Pipeline = pipeline ?? CredentialPipeline.GetInstance(options);
			LoginHint = (options as InteractiveBrowserCredentialOptions)?.LoginHint;
			string redirectUrl = (options as InteractiveBrowserCredentialOptions)?.RedirectUri?.AbsoluteUri ?? "http://localhost";
			DefaultScope = AzureAuthorityHosts.GetDefaultScope(options?.AuthorityHost ?? AzureAuthorityHosts.GetDefault());
			Client = client ?? new MsalPublicClient(Pipeline, tenantId, clientId, redirectUrl, options);
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds((options as ISupportsAdditionallyAllowedTenants)?.AdditionallyAllowedTenants);
			Record = (options as InteractiveBrowserCredentialOptions)?.AuthenticationRecord;
			BrowserCustomization = (options as InteractiveBrowserCredentialOptions)?.BrowserCustomization;
			UseOperatingSystemAccount = (options as IMsalPublicClientInitializerOptions)?.UseDefaultBrokerAccount ?? false;
		}

		public virtual AuthenticationRecord Authenticate(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (DefaultScope == null)
			{
				throw new CredentialUnavailableException("Authenticating in this environment requires specifying a TokenRequestContext.");
			}
			return Authenticate(new TokenRequestContext(new string[1] { DefaultScope }), cancellationToken);
		}

		public virtual async Task<AuthenticationRecord> AuthenticateAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (DefaultScope == null)
			{
				throw new CredentialUnavailableException("Authenticating in this environment requires specifying a TokenRequestContext.");
			}
			return await AuthenticateAsync(new TokenRequestContext(new string[1] { DefaultScope }), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public virtual AuthenticationRecord Authenticate(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return AuthenticateImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public virtual async Task<AuthenticationRecord> AuthenticateAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await AuthenticateImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetTokenImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await GetTokenImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async Task<AuthenticationRecord> AuthenticateImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = Pipeline.StartGetTokenScope("InteractiveBrowserCredential.Authenticate", requestContext);
			try
			{
				scope.Succeeded(await GetTokenViaBrowserLoginAsync(requestContext, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
				return Record;
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex);
			}
		}

		private async ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = Pipeline.StartGetTokenScope("InteractiveBrowserCredential.GetToken", requestContext);
			_ = 2;
			try
			{
				Exception innerException = null;
				string tenantId = TenantIdResolver.Resolve(TenantId ?? Record?.TenantId, requestContext, AdditionallyAllowedTenantIds);
				if (Record != null || UseOperatingSystemAccount)
				{
					try
					{
						AuthenticationResult result = ((Record != null) ? (await Client.AcquireTokenSilentAsync(requestContext.Scopes, requestContext.Claims, Record, tenantId, requestContext.IsCaeEnabled, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)) : (await Client.AcquireTokenSilentAsync(requestContext.Scopes, requestContext.Claims, PublicClientApplication.OperatingSystemAccount, tenantId, requestContext.IsCaeEnabled, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
						return scope.Succeeded(result.ToAccessToken());
					}
					catch (MsalUiRequiredException ex)
					{
						innerException = ex;
					}
				}
				if (DisableAutomaticAuthentication)
				{
					throw new AuthenticationRequiredException("Interactive authentication is needed to acquire token. Call Authenticate to interactively authenticate.", requestContext, innerException);
				}
				return scope.Succeeded(await GetTokenViaBrowserLoginAsync(requestContext, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
			}
			catch (Exception ex2)
			{
				throw scope.FailWrapAndThrow(ex2);
			}
		}

		private async Task<AccessToken> GetTokenViaBrowserLoginAsync(TokenRequestContext context, bool async, CancellationToken cancellationToken)
		{
			Prompt prompt = ((LoginHint != null) ? Prompt.NoPrompt : Prompt.SelectAccount);
			Prompt prompt2 = prompt;
			string tenantId = TenantIdResolver.Resolve(TenantId ?? Record?.TenantId, context, AdditionallyAllowedTenantIds);
			AuthenticationResult authenticationResult = await Client.AcquireTokenInteractiveAsync(context.Scopes, context.Claims, prompt2, LoginHint, tenantId, context.IsCaeEnabled, BrowserCustomization, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			Record = new AuthenticationRecord(authenticationResult, ClientId);
			return new AccessToken(authenticationResult.AccessToken, authenticationResult.ExpiresOn);
		}
	}
	public class InteractiveBrowserCredentialOptions : TokenCredentialOptions, ISupportsTokenCachePersistenceOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		private string _tenantId;

		public bool DisableAutomaticAuthentication { get; set; }

		public string TenantId
		{
			get
			{
				return _tenantId;
			}
			set
			{
				_tenantId = Validations.ValidateTenantId(value, null, allowNull: true);
			}
		}

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		public string ClientId { get; set; } = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";

		public TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }

		public Uri RedirectUri { get; set; }

		public AuthenticationRecord AuthenticationRecord { get; set; }

		public string LoginHint { get; set; }

		public bool DisableInstanceDiscovery { get; set; }

		public BrowserCustomizationOptions BrowserCustomization { get; set; }
	}
	internal interface ISupportsAdditionallyAllowedTenants
	{
		IList<string> AdditionallyAllowedTenants { get; }
	}
	internal interface ISupportsDisableInstanceDiscovery
	{
		bool DisableInstanceDiscovery { get; set; }
	}
	internal interface ISupportsTokenCachePersistenceOptions
	{
		TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }
	}
	public class ManagedIdentityCredential : TokenCredential
	{
		internal const string MsiUnavailableError = "No managed identity endpoint found.";

		private readonly CredentialPipeline _pipeline;

		private readonly string _clientId;

		private readonly bool _logAccountDetails;

		private const string Troubleshooting = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/managedidentitycredential/troubleshoot";

		internal ManagedIdentityClient Client { get; }

		protected ManagedIdentityCredential()
		{
		}

		public ManagedIdentityCredential(string clientId = null, TokenCredentialOptions options = null)
			: this(new ManagedIdentityClient(new ManagedIdentityClientOptions
			{
				ClientId = clientId,
				Pipeline = CredentialPipeline.GetInstance(options, IsManagedIdentityCredential: true),
				Options = options
			}))
		{
			_logAccountDetails = options?.Diagnostics?.IsAccountIdentifierLoggingEnabled == true;
		}

		public ManagedIdentityCredential(ResourceIdentifier resourceId, TokenCredentialOptions options = null)
			: this(new ManagedIdentityClient(new ManagedIdentityClientOptions
			{
				ResourceIdentifier = resourceId,
				Pipeline = CredentialPipeline.GetInstance(options, IsManagedIdentityCredential: true),
				Options = options
			}))
		{
			_logAccountDetails = options?.Diagnostics?.IsAccountIdentifierLoggingEnabled == true;
			_clientId = resourceId.ToString();
		}

		internal ManagedIdentityCredential(string clientId, CredentialPipeline pipeline, TokenCredentialOptions options = null, bool preserveTransport = false)
			: this(new ManagedIdentityClient(new ManagedIdentityClientOptions
			{
				Pipeline = pipeline,
				ClientId = clientId,
				PreserveTransport = preserveTransport,
				Options = options
			}))
		{
			_clientId = clientId;
		}

		internal ManagedIdentityCredential(ResourceIdentifier resourceId, CredentialPipeline pipeline, TokenCredentialOptions options, bool preserveTransport = false)
			: this(new ManagedIdentityClient(new ManagedIdentityClientOptions
			{
				Pipeline = pipeline,
				ResourceIdentifier = resourceId,
				PreserveTransport = preserveTransport,
				Options = options
			}))
		{
			_clientId = resourceId.ToString();
		}

		internal ManagedIdentityCredential(ManagedIdentityClient client)
		{
			_pipeline = client.Pipeline;
			Client = client;
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await GetTokenImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetTokenImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		private async ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("ManagedIdentityCredential.GetToken", requestContext);
			try
			{
				AccessToken token = await Client.AuthenticateAsync(async, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (_logAccountDetails)
				{
					(string, string, string, string) tuple = TokenHelper.ParseAccountInfoFromToken(token.Token);
					AzureIdentityEventSource.Singleton.AuthenticatedAccountDetails(tuple.Item1 ?? _clientId, tuple.Item2, tuple.Item3, tuple.Item4);
				}
				return scope.Succeeded(token);
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex, "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/managedidentitycredential/troubleshoot");
			}
		}
	}
	public class OnBehalfOfCredential : TokenCredential
	{
		private readonly string _tenantId;

		private readonly CredentialPipeline _pipeline;

		private readonly string _clientId;

		private readonly string _clientSecret;

		private readonly UserAssertion _userAssertion;

		internal readonly string[] AdditionallyAllowedTenantIds;

		internal MsalConfidentialClient Client { get; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		protected OnBehalfOfCredential()
		{
		}

		public OnBehalfOfCredential(string tenantId, string clientId, X509Certificate2 clientCertificate, string userAssertion)
			: this(tenantId, clientId, clientCertificate, userAssertion, null, null, null)
		{
		}

		public OnBehalfOfCredential(string tenantId, string clientId, X509Certificate2 clientCertificate, string userAssertion, OnBehalfOfCredentialOptions options)
			: this(tenantId, clientId, clientCertificate, userAssertion, options, null, null)
		{
		}

		public OnBehalfOfCredential(string tenantId, string clientId, string clientSecret, string userAssertion)
			: this(tenantId, clientId, clientSecret, userAssertion, null, null, null)
		{
		}

		public OnBehalfOfCredential(string tenantId, string clientId, string clientSecret, string userAssertion, OnBehalfOfCredentialOptions options)
			: this(tenantId, clientId, clientSecret, userAssertion, options, null, null)
		{
		}

		public OnBehalfOfCredential(string tenantId, string clientId, Func<CancellationToken, Task<string>> clientAssertionCallback, string userAssertion, OnBehalfOfCredentialOptions options = null)
			: this(tenantId, clientId, null, clientAssertionCallback, userAssertion, options, null, null)
		{
		}

		public OnBehalfOfCredential(string tenantId, string clientId, Func<string> clientAssertionCallback, string userAssertion, OnBehalfOfCredentialOptions options = null)
			: this(tenantId, clientId, clientAssertionCallback, null, userAssertion, options, null, null)
		{
		}

		internal OnBehalfOfCredential(string tenantId, string clientId, X509Certificate2 certificate, string userAssertion, OnBehalfOfCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
			: this(tenantId, clientId, new X509Certificate2FromObjectProvider(certificate ?? throw new ArgumentNullException("certificate")), userAssertion, options, pipeline, client)
		{
		}

		internal OnBehalfOfCredential(string tenantId, string clientId, IX509Certificate2Provider certificateProvider, string userAssertion, OnBehalfOfCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
		{
			_tenantId = Validations.ValidateTenantId(tenantId, "tenantId");
			_clientId = clientId ?? throw new ArgumentNullException("clientId");
			_pipeline = pipeline ?? CredentialPipeline.GetInstance(options);
			if (options == null)
			{
				options = new OnBehalfOfCredentialOptions();
			}
			_userAssertion = new UserAssertion(userAssertion);
			Client = client ?? new MsalConfidentialClient(_pipeline, tenantId, clientId, certificateProvider, options.SendCertificateChain, options);
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds(((ISupportsAdditionallyAllowedTenants)options)?.AdditionallyAllowedTenants);
		}

		internal OnBehalfOfCredential(string tenantId, string clientId, string clientSecret, string userAssertion, OnBehalfOfCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
		{
			Azure.Core.Argument.AssertNotNull(clientId, "clientId");
			Azure.Core.Argument.AssertNotNull(clientSecret, "clientSecret");
			if (options == null)
			{
				options = new OnBehalfOfCredentialOptions();
			}
			_pipeline = pipeline ?? CredentialPipeline.GetInstance(options);
			_tenantId = Validations.ValidateTenantId(tenantId, "tenantId");
			_clientId = clientId;
			_clientSecret = clientSecret;
			_userAssertion = new UserAssertion(userAssertion);
			Client = client ?? new MsalConfidentialClient(_pipeline, _tenantId, _clientId, _clientSecret, null, options);
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds(options?.AdditionallyAllowedTenants);
		}

		internal OnBehalfOfCredential(string tenantId, string clientId, Func<string> clientAssertionCallback, Func<CancellationToken, Task<string>> clientAssertionCallbackAsync, string userAssertion, OnBehalfOfCredentialOptions options, CredentialPipeline pipeline, MsalConfidentialClient client)
		{
			Azure.Core.Argument.AssertNotNull(clientId, "clientId");
			if (options == null)
			{
				options = new OnBehalfOfCredentialOptions();
			}
			_pipeline = pipeline ?? CredentialPipeline.GetInstance(options);
			_tenantId = Validations.ValidateTenantId(tenantId, "tenantId");
			_clientId = clientId;
			_userAssertion = new UserAssertion(userAssertion);
			MsalConfidentialClient msalConfidentialClient;
			if (client != null)
			{
				msalConfidentialClient = client;
			}
			else if (clientAssertionCallback != null)
			{
				msalConfidentialClient = new MsalConfidentialClient(_pipeline, _tenantId, _clientId, clientAssertionCallback, options);
			}
			else
			{
				if (clientAssertionCallbackAsync == null)
				{
					throw new ArgumentNullException("nameof(clientAssertionCallback)");
				}
				msalConfidentialClient = new MsalConfidentialClient(_pipeline, _tenantId, _clientId, clientAssertionCallbackAsync, options);
			}
			Client = msalConfidentialClient;
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds(options?.AdditionallyAllowedTenants);
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return GetTokenInternalAsync(requestContext, async: false, cancellationToken).EnsureCompleted();
		}

		public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return GetTokenInternalAsync(requestContext, async: true, cancellationToken);
		}

		internal async ValueTask<AccessToken> GetTokenInternalAsync(TokenRequestContext requestContext, bool async, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("OnBehalfOfCredential.GetToken", requestContext);
			try
			{
				string tenantId = TenantIdResolver.Resolve(_tenantId, requestContext, AdditionallyAllowedTenantIds);
				return (await Client.AcquireTokenOnBehalfOfAsync(requestContext.Scopes, tenantId, _userAssertion, requestContext.Claims, requestContext.IsCaeEnabled, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).ToAccessToken();
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex);
			}
		}
	}
	public class OnBehalfOfCredentialOptions : TokenCredentialOptions, ISupportsTokenCachePersistenceOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		public TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }

		public bool SendCertificateChain { get; set; }

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		public bool DisableInstanceDiscovery { get; set; }
	}
	public class SharedTokenCacheCredential : TokenCredential
	{
		internal const string NoAccountsInCacheMessage = "SharedTokenCacheCredential authentication unavailable. No accounts were found in the cache.";

		internal const string MultipleAccountsInCacheMessage = "SharedTokenCacheCredential authentication unavailable. Multiple accounts were found in the cache. Use username and tenant id to disambiguate.";

		internal const string NoMatchingAccountsInCacheMessage = "SharedTokenCacheCredential authentication unavailable. No account matching the specified{0}{1} was found in the cache.";

		internal const string MultipleMatchingAccountsInCacheMessage = "SharedTokenCacheCredential authentication unavailable. Multiple accounts matching the specified{0}{1} were found in the cache.";

		private static readonly SharedTokenCacheCredentialOptions s_DefaultCacheOptions = new SharedTokenCacheCredentialOptions();

		private readonly CredentialPipeline _pipeline;

		private readonly bool _skipTenantValidation;

		private readonly AuthenticationRecord _record;

		private readonly Azure.Core.AsyncLockWithValue<IAccount> _accountAsyncLock;

		internal string TenantId { get; }

		internal string Username { get; }

		internal MsalPublicClient Client { get; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		internal bool UseOperatingSystemAccount { get; }

		public SharedTokenCacheCredential()
			: this(null, null, null, null, null)
		{
		}

		public SharedTokenCacheCredential(SharedTokenCacheCredentialOptions options)
			: this(options?.TenantId, options?.Username, options, null, null)
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public SharedTokenCacheCredential(string username, TokenCredentialOptions options = null)
			: this(null, username, options, null, null)
		{
		}

		internal SharedTokenCacheCredential(string tenantId, string username, TokenCredentialOptions options, CredentialPipeline pipeline)
			: this(tenantId, username, options, pipeline, null)
		{
		}

		internal SharedTokenCacheCredential(string tenantId, string username, TokenCredentialOptions options, CredentialPipeline pipeline, MsalPublicClient client)
		{
			TenantId = tenantId;
			Username = username;
			SharedTokenCacheCredentialOptions sharedTokenCacheCredentialOptions = options as SharedTokenCacheCredentialOptions;
			_skipTenantValidation = sharedTokenCacheCredentialOptions?.EnableGuestTenantAuthentication ?? false;
			_record = sharedTokenCacheCredentialOptions?.AuthenticationRecord;
			_pipeline = pipeline ?? CredentialPipeline.GetInstance(options);
			Client = client ?? new MsalPublicClient(_pipeline, tenantId, sharedTokenCacheCredentialOptions?.ClientId ?? "04b07795-8ddb-461a-bbee-02f9e1bf7b46", null, options ?? s_DefaultCacheOptions);
			_accountAsyncLock = new Azure.Core.AsyncLockWithValue<IAccount>();
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			UseOperatingSystemAccount = (options as IMsalPublicClientInitializerOptions)?.UseDefaultBrokerAccount ?? false;
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetTokenImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await GetTokenImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("SharedTokenCacheCredential.GetToken", requestContext);
			_ = 1;
			try
			{
				string tenantId = TenantIdResolver.Resolve(TenantId, requestContext, TenantIdResolverBase.AllTenants);
				IAccount account = ((!UseOperatingSystemAccount) ? (await GetAccountAsync(tenantId, requestContext.IsCaeEnabled, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)) : PublicClientApplication.OperatingSystemAccount);
				IAccount account2 = account;
				return scope.Succeeded((await Client.AcquireTokenSilentAsync(requestContext.Scopes, requestContext.Claims, account2, tenantId, requestContext.IsCaeEnabled, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).ToAccessToken());
			}
			catch (MsalUiRequiredException innerException)
			{
				throw scope.FailWrapAndThrow(new CredentialUnavailableException("SharedTokenCacheCredential authentication unavailable. Token acquisition failed for user " + Username + ". Ensure that you have authenticated with a developer tool that supports Azure single sign on.", innerException));
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex);
			}
		}

		private async ValueTask<IAccount> GetAccountAsync(string tenantId, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			using Azure.Core.AsyncLockWithValue<IAccount>.LockOrValue asyncLock = await _accountAsyncLock.GetLockOrValueAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (asyncLock.HasValue)
			{
				return asyncLock.Value;
			}
			IAccount account;
			if (_record != null)
			{
				account = new AuthenticationAccount(_record);
				asyncLock.SetValue(account);
				return account;
			}
			List<IAccount> obj = await Client.GetAccountsAsync(async, enableCae, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (obj.Count == 0)
			{
				throw new CredentialUnavailableException("SharedTokenCacheCredential authentication unavailable. No accounts were found in the cache.");
			}
			List<IAccount> list = obj.Where((IAccount a) => (string.IsNullOrEmpty(Username) || string.Compare(a.Username, Username, StringComparison.OrdinalIgnoreCase) == 0) && (_skipTenantValidation || string.IsNullOrEmpty(tenantId) || string.Compare(a.HomeAccountId?.TenantId, tenantId, StringComparison.OrdinalIgnoreCase) == 0)).ToList();
			if (_skipTenantValidation && list.Count > 1)
			{
				list = list.Where((IAccount a) => string.IsNullOrEmpty(tenantId) || string.Compare(a.HomeAccountId?.TenantId, tenantId, StringComparison.OrdinalIgnoreCase) == 0).ToList();
			}
			if (list.Count != 1)
			{
				throw new CredentialUnavailableException(GetCredentialUnavailableMessage(list));
			}
			account = list[0];
			asyncLock.SetValue(account);
			return account;
		}

		private string GetCredentialUnavailableMessage(List<IAccount> filteredAccounts)
		{
			if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(TenantId))
			{
				return string.Format(CultureInfo.InvariantCulture, "SharedTokenCacheCredential authentication unavailable. Multiple accounts were found in the cache. Use username and tenant id to disambiguate.");
			}
			string arg = (string.IsNullOrEmpty(Username) ? string.Empty : (" username: " + Username));
			string arg2 = (string.IsNullOrEmpty(TenantId) ? string.Empty : (" tenantId: " + TenantId));
			if (filteredAccounts.Count == 0)
			{
				return string.Format(CultureInfo.InvariantCulture, "SharedTokenCacheCredential authentication unavailable. No account matching the specified{0}{1} was found in the cache.", arg, arg2);
			}
			return string.Format(CultureInfo.InvariantCulture, "SharedTokenCacheCredential authentication unavailable. Multiple accounts matching the specified{0}{1} were found in the cache.", arg, arg2);
		}
	}
	public class SharedTokenCacheCredentialOptions : TokenCredentialOptions, ISupportsTokenCachePersistenceOptions, ISupportsDisableInstanceDiscovery
	{
		private string _tenantId;

		private TokenCachePersistenceOptions _tokenCachePersistenceOptions;

		internal static readonly TokenCachePersistenceOptions s_defaulTokenCachetPersistenceOptions = new TokenCachePersistenceOptions();

		public string ClientId { get; set; } = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";

		public string Username { get; set; }

		public string TenantId
		{
			get
			{
				return _tenantId;
			}
			set
			{
				_tenantId = Validations.ValidateTenantId(value, null, allowNull: true);
			}
		}

		public bool EnableGuestTenantAuthentication { get; set; }

		public AuthenticationRecord AuthenticationRecord { get; set; }

		public TokenCachePersistenceOptions TokenCachePersistenceOptions
		{
			get
			{
				return _tokenCachePersistenceOptions;
			}
			set
			{
				Azure.Core.Argument.AssertNotNull(value, "value");
				_tokenCachePersistenceOptions = value;
			}
		}

		public bool DisableInstanceDiscovery { get; set; }

		public SharedTokenCacheCredentialOptions()
			: this(null)
		{
		}

		public SharedTokenCacheCredentialOptions(TokenCachePersistenceOptions tokenCacheOptions)
		{
			TokenCachePersistenceOptions = tokenCacheOptions ?? s_defaulTokenCachetPersistenceOptions;
		}
	}
	public class TokenCredentialOptions : ClientOptions
	{
		private Uri _authorityHost;

		public Uri AuthorityHost
		{
			get
			{
				return _authorityHost ?? AzureAuthorityHosts.GetDefault();
			}
			set
			{
				_authorityHost = Validations.ValidateAuthorityHost(value);
			}
		}

		public bool IsUnsafeSupportLoggingEnabled { get; set; }

		internal bool IsChainedCredential { get; set; }

		internal TenantIdResolverBase TenantIdResolver { get; set; } = TenantIdResolverBase.Default;

		public new TokenCredentialDiagnosticsOptions Diagnostics => base.Diagnostics as TokenCredentialDiagnosticsOptions;

		public TokenCredentialOptions()
			: base(new TokenCredentialDiagnosticsOptions())
		{
		}

		internal virtual T Clone<T>() where T : TokenCredentialOptions, new()
		{
			T val = new T();
			val.AuthorityHost = AuthorityHost;
			val.IsUnsafeSupportLoggingEnabled = IsUnsafeSupportLoggingEnabled;
			val.Diagnostics.IsAccountIdentifierLoggingEnabled = Diagnostics.IsAccountIdentifierLoggingEnabled;
			CloneIfImplemented(this, val, delegate(ISupportsDisableInstanceDiscovery o, ISupportsDisableInstanceDiscovery c)
			{
				c.DisableInstanceDiscovery = o.DisableInstanceDiscovery;
			});
			CloneIfImplemented(this, val, delegate(ISupportsTokenCachePersistenceOptions o, ISupportsTokenCachePersistenceOptions c)
			{
				c.TokenCachePersistenceOptions = o.TokenCachePersistenceOptions;
			});
			CloneIfImplemented(this, val, delegate(ISupportsAdditionallyAllowedTenants o, ISupportsAdditionallyAllowedTenants c)
			{
				CloneListItems(o.AdditionallyAllowedTenants, c.AdditionallyAllowedTenants);
			});
			if (base.Transport != ClientOptions.Default.Transport)
			{
				val.Transport = base.Transport;
			}
			val.Diagnostics.ApplicationId = Diagnostics.ApplicationId;
			val.Diagnostics.IsLoggingEnabled = Diagnostics.IsLoggingEnabled;
			val.Diagnostics.IsTelemetryEnabled = Diagnostics.IsTelemetryEnabled;
			val.Diagnostics.LoggedContentSizeLimit = Diagnostics.LoggedContentSizeLimit;
			val.Diagnostics.IsDistributedTracingEnabled = Diagnostics.IsDistributedTracingEnabled;
			val.Diagnostics.IsLoggingContentEnabled = Diagnostics.IsLoggingContentEnabled;
			CloneListItems(Diagnostics.LoggedHeaderNames, val.Diagnostics.LoggedHeaderNames);
			CloneListItems(Diagnostics.LoggedQueryParameters, val.Diagnostics.LoggedQueryParameters);
			val.RetryPolicy = base.RetryPolicy;
			val.Retry.MaxRetries = base.Retry.MaxRetries;
			val.Retry.Delay = base.Retry.Delay;
			val.Retry.MaxDelay = base.Retry.MaxDelay;
			val.Retry.Mode = base.Retry.Mode;
			val.Retry.NetworkTimeout = base.Retry.NetworkTimeout;
			return val;
		}

		private static void CloneListItems<T>(IList<T> original, IList<T> clone)
		{
			clone.Clear();
			foreach (T item in original)
			{
				clone.Add(item);
			}
		}

		private static void CloneIfImplemented<T>(TokenCredentialOptions original, TokenCredentialOptions clone, Action<T, T> cloneOperation) where T : class
		{
			if (original is T arg && clone is T arg2)
			{
				cloneOperation(arg, arg2);
			}
		}
	}
	public class UsernamePasswordCredential : TokenCredential
	{
		private const string NoDefaultScopeMessage = "Authenticating in this environment requires specifying a TokenRequestContext.";

		private const string Troubleshooting = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/usernamepasswordcredential/troubleshoot";

		private readonly string _clientId;

		private readonly CredentialPipeline _pipeline;

		private readonly string _username;

		private readonly string _password;

		private AuthenticationRecord _record;

		private readonly string _tenantId;

		internal string[] AdditionallyAllowedTenantIds { get; }

		internal MsalPublicClient Client { get; }

		internal string DefaultScope { get; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		protected UsernamePasswordCredential()
		{
		}

		public UsernamePasswordCredential(string username, string password, string tenantId, string clientId)
			: this(username, password, tenantId, clientId, (TokenCredentialOptions)null)
		{
		}

		public UsernamePasswordCredential(string username, string password, string tenantId, string clientId, TokenCredentialOptions options)
			: this(username, password, tenantId, clientId, options, null, null)
		{
		}

		public UsernamePasswordCredential(string username, string password, string tenantId, string clientId, UsernamePasswordCredentialOptions options)
			: this(username, password, tenantId, clientId, options, null, null)
		{
		}

		internal UsernamePasswordCredential(string username, string password, string tenantId, string clientId, TokenCredentialOptions options, CredentialPipeline pipeline, MsalPublicClient client)
		{
			Azure.Core.Argument.AssertNotNull(username, "username");
			Azure.Core.Argument.AssertNotNull(password, "password");
			Azure.Core.Argument.AssertNotNull(clientId, "clientId");
			_tenantId = Validations.ValidateTenantId(tenantId, "tenantId");
			_username = username;
			_password = password;
			_clientId = clientId;
			if (options is UsernamePasswordCredentialOptions { AuthenticationRecord: not null } usernamePasswordCredentialOptions)
			{
				_record = usernamePasswordCredentialOptions.AuthenticationRecord;
			}
			_pipeline = pipeline ?? CredentialPipeline.GetInstance(options);
			DefaultScope = AzureAuthorityHosts.GetDefaultScope(options?.AuthorityHost ?? AzureAuthorityHosts.GetDefault());
			Client = client ?? new MsalPublicClient(_pipeline, tenantId, clientId, null, options);
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds((options as ISupportsAdditionallyAllowedTenants)?.AdditionallyAllowedTenants);
		}

		public virtual AuthenticationRecord Authenticate(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (DefaultScope == null)
			{
				throw new CredentialUnavailableException("Authenticating in this environment requires specifying a TokenRequestContext.");
			}
			return Authenticate(new TokenRequestContext(new string[1] { DefaultScope }), cancellationToken);
		}

		public virtual async Task<AuthenticationRecord> AuthenticateAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (DefaultScope == null)
			{
				throw new CredentialUnavailableException("Authenticating in this environment requires specifying a TokenRequestContext.");
			}
			return await AuthenticateAsync(new TokenRequestContext(new string[1] { DefaultScope }), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public virtual AuthenticationRecord Authenticate(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			AuthenticateImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
			return _record;
		}

		public virtual async Task<AuthenticationRecord> AuthenticateAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			await AuthenticateImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return _record;
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetTokenImplAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await GetTokenImplAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async Task<AuthenticationResult> AuthenticateImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("UsernamePasswordCredential.Authenticate", requestContext);
			try
			{
				string tenantId = TenantIdResolver.Resolve(_tenantId, requestContext, AdditionallyAllowedTenantIds);
				AuthenticationResult authenticationResult = await Client.AcquireTokenByUsernamePasswordAsync(requestContext.Scopes, requestContext.Claims, _username, _password, tenantId, requestContext.IsCaeEnabled, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				_record = new AuthenticationRecord(authenticationResult, _clientId);
				return authenticationResult;
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex, "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/usernamepasswordcredential/troubleshoot");
			}
		}

		private async Task<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("UsernamePasswordCredential.GetToken", requestContext);
			_ = 1;
			try
			{
				if (_record != null)
				{
					string tenantId = TenantIdResolver.Resolve(_tenantId, requestContext, AdditionallyAllowedTenantIds);
					try
					{
						return scope.Succeeded((await Client.AcquireTokenSilentAsync(requestContext.Scopes, requestContext.Claims, _record, tenantId, requestContext.IsCaeEnabled, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).ToAccessToken());
					}
					catch (MsalUiRequiredException e)
					{
						AzureIdentityEventSource.Singleton.UsernamePasswordCredentialAcquireTokenSilentFailed(e);
					}
				}
				return scope.Succeeded((await AuthenticateImplAsync(async, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).ToAccessToken());
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex, "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/usernamepasswordcredential/troubleshoot");
			}
		}
	}
	public class UsernamePasswordCredentialOptions : TokenCredentialOptions, ISupportsTokenCachePersistenceOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		public TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		public bool DisableInstanceDiscovery { get; set; }

		internal AuthenticationRecord AuthenticationRecord { get; set; }
	}
	public class VisualStudioCodeCredential : TokenCredential
	{
		private const string CredentialsSection = "VS Code Azure";

		private const string ClientId = "aebc6443-996d-45c2-90f0-388ff96faa56";

		private readonly IVisualStudioCodeAdapter _vscAdapter;

		private readonly IFileSystemService _fileSystem;

		private readonly CredentialPipeline _pipeline;

		private const string _commonTenant = "common";

		private const string Troubleshooting = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/vscodecredential/troubleshoot";

		internal string TenantId { get; }

		internal string[] AdditionallyAllowedTenantIds { get; }

		internal MsalPublicClient Client { get; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		public VisualStudioCodeCredential()
			: this(null, null, null, null, null)
		{
		}

		public VisualStudioCodeCredential(VisualStudioCodeCredentialOptions options)
			: this(options, null, null, null, null)
		{
		}

		internal VisualStudioCodeCredential(VisualStudioCodeCredentialOptions options, CredentialPipeline pipeline, MsalPublicClient client, IFileSystemService fileSystem, IVisualStudioCodeAdapter vscAdapter)
		{
			TenantId = options?.TenantId;
			_pipeline = pipeline ?? CredentialPipeline.GetInstance(options);
			Client = client ?? new MsalPublicClient(_pipeline, TenantId, "aebc6443-996d-45c2-90f0-388ff96faa56", null, options);
			_fileSystem = fileSystem ?? FileSystemService.Default;
			_vscAdapter = vscAdapter ?? GetVscAdapter();
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds(((ISupportsAdditionallyAllowedTenants)options)?.AdditionallyAllowedTenants);
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return await GetTokenImplAsync(requestContext, async: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return GetTokenImplAsync(requestContext, async: false, cancellationToken).EnsureCompleted();
		}

		private async ValueTask<AccessToken> GetTokenImplAsync(TokenRequestContext requestContext, bool async, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("VisualStudioCodeCredential.GetToken", requestContext);
			try
			{
				GetUserSettings(out var tenant, out var environmentName);
				string text = TenantIdResolver.Resolve(TenantId, requestContext, AdditionallyAllowedTenantIds) ?? tenant;
				if (string.Equals(text, "adfs", StringComparison.Ordinal))
				{
					throw new CredentialUnavailableException("VisualStudioCodeCredential authentication unavailable. ADFS tenant / authorities are not supported.");
				}
				AzureCloudInstance azureCloudInstance = GetAzureCloudInstance(environmentName);
				string storedCredentials = GetStoredCredentials(environmentName);
				return scope.Succeeded((await Client.AcquireTokenByRefreshTokenAsync(requestContext.Scopes, requestContext.Claims, storedCredentials, azureCloudInstance, text, requestContext.IsCaeEnabled, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).ToAccessToken());
			}
			catch (MsalUiRequiredException innerException)
			{
				throw scope.FailWrapAndThrow(new CredentialUnavailableException("VisualStudioCodeCredential authentication unavailable. Token acquisition failed. Ensure that you have authenticated in VSCode Azure Account. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/vscodecredential/troubleshoot", innerException));
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex, "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/vscodecredential/troubleshoot");
			}
		}

		private string GetStoredCredentials(string environmentName)
		{
			try
			{
				string credentials = _vscAdapter.GetCredentials("VS Code Azure", environmentName);
				if (!IsRefreshTokenString(credentials))
				{
					throw new CredentialUnavailableException("Need to re-authenticate user in VSCode Azure Account.");
				}
				return credentials;
			}
			catch (Exception ex) when (!(ex is OperationCanceledException) && !(ex is CredentialUnavailableException))
			{
				throw new CredentialUnavailableException("Stored credentials not found. Need to authenticate user in VSCode Azure Account. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/vscodecredential/troubleshoot", ex);
			}
		}

		private static bool IsRefreshTokenString(string str)
		{
			foreach (uint num in str)
			{
				if ((num < 48 || num > 57) && (num < 65 || num > 90) && (num < 97 || num > 122) && num != 95 && num != 45 && num != 46)
				{
					return false;
				}
			}
			return true;
		}

		private void GetUserSettings(out string tenant, out string environmentName)
		{
			string userSettingsPath = _vscAdapter.GetUserSettingsPath();
			tenant = TenantId ?? "common";
			environmentName = "AzureCloud";
			try
			{
				JsonElement rootElement = JsonDocument.Parse(_fileSystem.ReadAllText(userSettingsPath)).RootElement;
				if (rootElement.TryGetProperty("azure.tenant", out var value))
				{
					tenant = value.GetString();
				}
				if (rootElement.TryGetProperty("azure.cloud", out var value2))
				{
					environmentName = value2.GetString();
				}
			}
			catch (IOException)
			{
			}
			catch (JsonException)
			{
			}
		}

		private static IVisualStudioCodeAdapter GetVscAdapter()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				return new WindowsVisualStudioCodeAdapter();
			}
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				return new MacosVisualStudioCodeAdapter();
			}
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				return new LinuxVisualStudioCodeAdapter();
			}
			throw new PlatformNotSupportedException();
		}

		private static AzureCloudInstance GetAzureCloudInstance(string name)
		{
			return name switch
			{
				"AzureCloud" => AzureCloudInstance.AzurePublic, 
				"AzureChina" => AzureCloudInstance.AzureChina, 
				"AzureGermanCloud" => AzureCloudInstance.AzureGermany, 
				"AzureUSGovernment" => AzureCloudInstance.AzureUsGovernment, 
				_ => AzureCloudInstance.AzurePublic, 
			};
		}
	}
	public class VisualStudioCodeCredentialOptions : TokenCredentialOptions, ISupportsAdditionallyAllowedTenants
	{
		private string _tenantId;

		public string TenantId
		{
			get
			{
				return _tenantId;
			}
			set
			{
				_tenantId = Validations.ValidateTenantId(value, null, allowNull: true);
			}
		}

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();
	}
	public class VisualStudioCredential : TokenCredential
	{
		private readonly struct VisualStudioTokenProvider : IComparable<VisualStudioTokenProvider>
		{
			private readonly int _preference;

			public string Path { get; }

			public string[] Arguments { get; }

			public VisualStudioTokenProvider(string path, string[] arguments, int preference)
			{
				Path = path;
				Arguments = arguments;
				_preference = preference;
			}

			public int CompareTo(VisualStudioTokenProvider other)
			{
				int preference = _preference;
				return preference.CompareTo(other._preference);
			}
		}

		private static readonly string TokenProviderFilePath = Path.Combine(".IdentityService", "AzureServiceAuth", "tokenprovider.json");

		private const string ResourceArgumentName = "--resource";

		private const string TenantArgumentName = "--tenant";

		private readonly CredentialPipeline _pipeline;

		private readonly IFileSystemService _fileSystem;

		private readonly IProcessService _processService;

		private readonly bool _logPII;

		private readonly bool _logAccountDetails;

		internal bool _isChainedCredential;

		internal string TenantId { get; }

		internal string[] AdditionallyAllowedTenantIds { get; }

		internal TimeSpan ProcessTimeout { get; private set; }

		internal TenantIdResolverBase TenantIdResolver { get; }

		public VisualStudioCredential()
			: this(null)
		{
		}

		public VisualStudioCredential(VisualStudioCredentialOptions options)
			: this(options?.TenantId, CredentialPipeline.GetInstance(options), null, null, options)
		{
		}

		internal VisualStudioCredential(string tenantId, CredentialPipeline pipeline, IFileSystemService fileSystem, IProcessService processService, VisualStudioCredentialOptions options = null)
		{
			_logPII = options?.IsUnsafeSupportLoggingEnabled ?? false;
			_logAccountDetails = options?.Diagnostics?.IsAccountIdentifierLoggingEnabled == true;
			TenantId = tenantId;
			_pipeline = pipeline ?? CredentialPipeline.GetInstance(null);
			_fileSystem = fileSystem ?? FileSystemService.Default;
			_processService = processService ?? ProcessService.Default;
			TenantIdResolver = options?.TenantIdResolver ?? TenantIdResolverBase.Default;
			AdditionallyAllowedTenantIds = TenantIdResolver.ResolveAddionallyAllowedTenantIds(((ISupportsAdditionallyAllowedTenants)options)?.AdditionallyAllowedTenants);
			ProcessTimeout = options?.ProcessTimeout ?? TimeSpan.FromSeconds(30.0);
			_isChainedCredential = options?.IsChainedCredential ?? false;
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return await GetTokenImplAsync(requestContext, async: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			return GetTokenImplAsync(requestContext, async: false, cancellationToken).EnsureCompleted();
		}

		private async ValueTask<AccessToken> GetTokenImplAsync(TokenRequestContext requestContext, bool async, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("VisualStudioCredential.GetToken", requestContext);
			try
			{
				if (string.Equals(TenantId, "adfs", StringComparison.Ordinal))
				{
					throw new CredentialUnavailableException("VisualStudioCredential authentication unavailable. ADFS tenant/authorities are not supported.");
				}
				string tokenProviderPath = GetTokenProviderPath();
				VisualStudioTokenProvider[] tokenProviders = GetTokenProviders(tokenProviderPath);
				string resource = ScopeUtilities.ScopesToResource(requestContext.Scopes);
				List<ProcessStartInfo> processStartInfos = GetProcessStartInfos(tokenProviders, resource, requestContext, cancellationToken);
				if (processStartInfos.Count == 0)
				{
					throw new CredentialUnavailableException("No installed instance of Visual Studio was found");
				}
				AccessToken token = await RunProcessesAsync(processStartInfos, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (_logAccountDetails)
				{
					(string, string, string, string) tuple = TokenHelper.ParseAccountInfoFromToken(token.Token);
					AzureIdentityEventSource.Singleton.AuthenticatedAccountDetails(tuple.Item1, tuple.Item2 ?? TenantId, tuple.Item3, tuple.Item4);
				}
				return scope.Succeeded(token);
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex, null, _isChainedCredential);
			}
		}

		private static string GetTokenProviderPath()
		{
			string text;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				if (string.IsNullOrEmpty(text))
				{
					text = Environment.GetEnvironmentVariable("LOCALAPPDATA");
					if (string.IsNullOrEmpty(text))
					{
						throw new CredentialUnavailableException("Can't find the Local Application Data folder. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/vscredential/troubleshoot");
					}
				}
			}
			else
			{
				text = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			}
			return Path.Combine(text, TokenProviderFilePath);
		}

		private async Task<AccessToken> RunProcessesAsync(List<ProcessStartInfo> processStartInfos, bool async, CancellationToken cancellationToken)
		{
			List<Exception> exceptions = new List<Exception>();
			foreach (ProcessStartInfo processStartInfo in processStartInfos)
			{
				string output = string.Empty;
				try
				{
					using ProcessRunner processRunner = new ProcessRunner(_processService.Create(processStartInfo), ProcessTimeout, _logPII, cancellationToken);
					string text = ((!async) ? processRunner.Run() : (await processRunner.RunAsync().ConfigureAwait(continueOnCapturedContext: false)));
					output = text;
					JsonElement rootElement = JsonDocument.Parse(output).RootElement;
					string accessToken = rootElement.GetProperty("access_token").GetString();
					DateTimeOffset dateTimeOffset = rootElement.GetProperty("expires_on").GetDateTimeOffset();
					return new AccessToken(accessToken, dateTimeOffset);
				}
				catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
				{
					exceptions.Add(new CredentialUnavailableException($"Process \"{processStartInfo.FileName}\" has failed to get access token in {ProcessTimeout.TotalSeconds} seconds."));
				}
				catch (JsonException innerException)
				{
					exceptions.Add(new CredentialUnavailableException("Process \"" + processStartInfo.FileName + "\" has non-json output: " + output + ".", innerException));
				}
				catch (Exception ex2) when (!(ex2 is OperationCanceledException))
				{
					if (_isChainedCredential)
					{
						exceptions.Add(new CredentialUnavailableException("Process \"" + processStartInfo.FileName + "\" has failed with unexpected error: " + ex2.Message + ".", ex2));
					}
					else
					{
						exceptions.Add(new AuthenticationFailedException("Process \"" + processStartInfo.FileName + "\" has failed with unexpected error: " + ex2.Message + ".", ex2));
					}
				}
			}
			switch (exceptions.Count)
			{
			case 0:
				throw new CredentialUnavailableException("No installed instance of Visual Studio was able to get credentials.");
			case 1:
				ExceptionDispatchInfo.Capture(exceptions[0]).Throw();
				return default(AccessToken);
			default:
				throw new AggregateException(exceptions);
			}
		}

		private List<ProcessStartInfo> GetProcessStartInfos(VisualStudioTokenProvider[] visualStudioTokenProviders, string resource, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			List<ProcessStartInfo> list = new List<ProcessStartInfo>();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < visualStudioTokenProviders.Length; i++)
			{
				VisualStudioTokenProvider visualStudioTokenProvider = visualStudioTokenProviders[i];
				cancellationToken.ThrowIfCancellationRequested();
				if (!_fileSystem.FileExists(visualStudioTokenProvider.Path))
				{
					continue;
				}
				stringBuilder.Clear();
				string[] arguments = visualStudioTokenProvider.Arguments;
				if (arguments != null && arguments.Length != 0)
				{
					string[] arguments2 = visualStudioTokenProvider.Arguments;
					foreach (string value in arguments2)
					{
						stringBuilder.Append(value).Append(' ');
					}
				}
				stringBuilder.Append("--resource").Append(' ').Append(resource);
				string text = TenantIdResolver.Resolve(TenantId, requestContext, AdditionallyAllowedTenantIds);
				if (text != null)
				{
					stringBuilder.Append(' ').Append("--tenant").Append(' ')
						.Append(text);
				}
				ProcessStartInfo item = new ProcessStartInfo
				{
					FileName = visualStudioTokenProvider.Path,
					Arguments = stringBuilder.ToString(),
					ErrorDialog = false,
					CreateNoWindow = true
				};
				list.Add(item);
			}
			return list;
		}

		private VisualStudioTokenProvider[] GetTokenProviders(string tokenProviderPath)
		{
			string tokenProviderContent = GetTokenProviderContent(tokenProviderPath);
			try
			{
				using JsonDocument jsonDocument = JsonDocument.Parse(tokenProviderContent);
				JsonElement property = jsonDocument.RootElement.GetProperty("TokenProviders");
				VisualStudioTokenProvider[] array = new VisualStudioTokenProvider[property.GetArrayLength()];
				for (int i = 0; i < array.Length; i++)
				{
					JsonElement element = property[i];
					string path = element.GetProperty("Path").GetString();
					int @int = element.GetProperty("Preference").GetInt32();
					string[] stringArrayPropertyValue = GetStringArrayPropertyValue(element, "Arguments");
					array[i] = new VisualStudioTokenProvider(path, stringArrayPropertyValue, @int);
				}
				Array.Sort(array);
				return array;
			}
			catch (JsonException innerException)
			{
				throw new CredentialUnavailableException("File found at \"" + tokenProviderPath + "\" isn't a valid JSON file", innerException);
			}
			catch (Exception innerException2)
			{
				throw new CredentialUnavailableException("JSON file found at \"" + tokenProviderPath + "\" has invalid schema.", innerException2);
			}
		}

		private string GetTokenProviderContent(string tokenProviderPath)
		{
			try
			{
				return _fileSystem.ReadAllText(tokenProviderPath);
			}
			catch (FileNotFoundException innerException)
			{
				throw new CredentialUnavailableException("Visual Studio Token provider file not found at " + tokenProviderPath, innerException);
			}
			catch (IOException innerException2)
			{
				throw new CredentialUnavailableException("Visual Studio Token provider can't be accessed at " + tokenProviderPath, innerException2);
			}
		}

		private static string[] GetStringArrayPropertyValue(JsonElement element, string name)
		{
			JsonElement property = element.GetProperty(name);
			string[] array = new string[property.GetArrayLength()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = property[i].GetString();
			}
			return array;
		}
	}
	public class VisualStudioCredentialOptions : TokenCredentialOptions, ISupportsAdditionallyAllowedTenants
	{
		private string _tenantId;

		public string TenantId
		{
			get
			{
				return _tenantId;
			}
			set
			{
				_tenantId = Validations.ValidateTenantId(value, null, allowNull: true);
			}
		}

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		public TimeSpan? ProcessTimeout { get; set; }
	}
	public class WorkloadIdentityCredential : TokenCredential
	{
		private const string UnavailableErrorMessage = "WorkloadIdentityCredential authentication unavailable. The workload options are not fully configured. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/workloadidentitycredential/troubleshoot";

		private readonly FileContentsCache _tokenFileCache;

		private readonly ClientAssertionCredential _clientAssertionCredential;

		private readonly CredentialPipeline _pipeline;

		internal MsalConfidentialClient Client => _clientAssertionCredential?.Client;

		internal string[] AdditionallyAllowedTenantIds => _clientAssertionCredential?.AdditionallyAllowedTenantIds;

		public WorkloadIdentityCredential()
			: this(null)
		{
		}

		public WorkloadIdentityCredential(WorkloadIdentityCredentialOptions options)
		{
			options = options ?? new WorkloadIdentityCredentialOptions();
			if (!string.IsNullOrEmpty(options.TenantId) && !string.IsNullOrEmpty(options.ClientId) && !string.IsNullOrEmpty(options.TokenFilePath))
			{
				_tokenFileCache = new FileContentsCache(options.TokenFilePath);
				ClientAssertionCredentialOptions clientAssertionCredentialOptions = options.Clone<ClientAssertionCredentialOptions>();
				clientAssertionCredentialOptions.Pipeline = options.Pipeline;
				clientAssertionCredentialOptions.MsalClient = options.MsalClient;
				_clientAssertionCredential = new ClientAssertionCredential(options.TenantId, options.ClientId, _tokenFileCache.GetTokenFileContentsAsync, clientAssertionCredentialOptions);
			}
			_pipeline = _clientAssertionCredential?.Pipeline ?? CredentialPipeline.GetInstance(null);
		}

		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetTokenCoreAsync(async: false, requestContext, cancellationToken).EnsureCompleted();
		}

		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await GetTokenCoreAsync(async: true, requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async ValueTask<AccessToken> GetTokenCoreAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			using CredentialDiagnosticScope scope = _pipeline.StartGetTokenScope("WorkloadIdentityCredential.GetToken", requestContext);
			try
			{
				if (_clientAssertionCredential == null)
				{
					throw new CredentialUnavailableException("WorkloadIdentityCredential authentication unavailable. The workload options are not fully configured. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/workloadidentitycredential/troubleshoot");
				}
				AccessToken accessToken = ((!async) ? _clientAssertionCredential.GetToken(requestContext, cancellationToken) : (await _clientAssertionCredential.GetTokenAsync(requestContext, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
				AccessToken token = accessToken;
				return scope.Succeeded(token);
			}
			catch (Exception ex)
			{
				throw scope.FailWrapAndThrow(ex);
			}
		}
	}
	public class WorkloadIdentityCredentialOptions : TokenCredentialOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		public string TenantId { get; set; } = EnvironmentVariables.TenantId;

		public string ClientId { get; set; } = EnvironmentVariables.ClientId;

		public string TokenFilePath { get; set; } = EnvironmentVariables.AzureFederatedTokenFile;

		public bool DisableInstanceDiscovery { get; set; }

		public IList<string> AdditionallyAllowedTenants { get; internal set; } = EnvironmentVariables.AdditionallyAllowedTenants;

		internal CredentialPipeline Pipeline { get; set; }

		internal MsalConfidentialClient MsalClient { get; set; }
	}
	[Serializable]
	public class CredentialUnavailableException : AuthenticationFailedException
	{
		public CredentialUnavailableException(string message)
			: this(message, null)
		{
		}

		public CredentialUnavailableException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		internal static CredentialUnavailableException CreateAggregateException(string message, IList<CredentialUnavailableException> exceptions)
		{
			if (exceptions.Count == 1)
			{
				return exceptions[0];
			}
			StringBuilder stringBuilder = new StringBuilder(message);
			foreach (CredentialUnavailableException exception in exceptions)
			{
				stringBuilder.Append(Environment.NewLine).Append("- ").Append(exception.Message);
			}
			AggregateException innerException = new AggregateException("Multiple exceptions were encountered while attempting to authenticate.", exceptions);
			return new CredentialUnavailableException(stringBuilder.ToString(), innerException);
		}

		protected CredentialUnavailableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	internal class DefaultAzureCredentialFactory
	{
		private static readonly TokenCredential[] s_defaultCredentialChain = new DefaultAzureCredentialFactory(new DefaultAzureCredentialOptions()).CreateCredentialChain();

		private bool _useDefaultCredentialChain;

		public DefaultAzureCredentialOptions Options { get; }

		public CredentialPipeline Pipeline { get; }

		public DefaultAzureCredentialFactory(DefaultAzureCredentialOptions options)
			: this(options, CredentialPipeline.GetInstance(options))
		{
		}

		protected DefaultAzureCredentialFactory(DefaultAzureCredentialOptions options, CredentialPipeline pipeline)
		{
			Pipeline = pipeline;
			_useDefaultCredentialChain = options == null;
			Options = options?.Clone<DefaultAzureCredentialOptions>() ?? new DefaultAzureCredentialOptions();
		}

		public TokenCredential[] CreateCredentialChain()
		{
			if (_useDefaultCredentialChain)
			{
				return s_defaultCredentialChain;
			}
			List<TokenCredential> list = new List<TokenCredential>(10);
			if (!Options.ExcludeEnvironmentCredential)
			{
				list.Add(CreateEnvironmentCredential());
			}
			if (!Options.ExcludeWorkloadIdentityCredential)
			{
				list.Add(CreateWorkloadIdentityCredential());
			}
			if (!Options.ExcludeManagedIdentityCredential)
			{
				list.Add(CreateManagedIdentityCredential());
			}
			if (!Options.ExcludeSharedTokenCacheCredential)
			{
				list.Add(CreateSharedTokenCacheCredential());
			}
			if (!Options.ExcludeVisualStudioCredential)
			{
				list.Add(CreateVisualStudioCredential());
			}
			if (!Options.ExcludeVisualStudioCodeCredential)
			{
				list.Add(CreateVisualStudioCodeCredential());
			}
			if (!Options.ExcludeAzureCliCredential)
			{
				list.Add(CreateAzureCliCredential());
			}
			if (!Options.ExcludeAzurePowerShellCredential)
			{
				list.Add(CreateAzurePowerShellCredential());
			}
			if (!Options.ExcludeAzureDeveloperCliCredential)
			{
				list.Add(CreateAzureDeveloperCliCredential());
			}
			if (!Options.ExcludeInteractiveBrowserCredential)
			{
				list.Add(CreateInteractiveBrowserCredential());
			}
			if (list.Count == 0)
			{
				throw new ArgumentException("At least one credential type must be included in the authentication flow.", "options");
			}
			return list.ToArray();
		}

		public virtual TokenCredential CreateEnvironmentCredential()
		{
			EnvironmentCredentialOptions environmentCredentialOptions = Options.Clone<EnvironmentCredentialOptions>();
			if (!string.IsNullOrEmpty(environmentCredentialOptions.TenantId))
			{
				environmentCredentialOptions.TenantId = Options.TenantId;
			}
			return new EnvironmentCredential(Pipeline, environmentCredentialOptions);
		}

		public virtual TokenCredential CreateWorkloadIdentityCredential()
		{
			WorkloadIdentityCredentialOptions workloadIdentityCredentialOptions = Options.Clone<WorkloadIdentityCredentialOptions>();
			workloadIdentityCredentialOptions.ClientId = Options.WorkloadIdentityClientId;
			workloadIdentityCredentialOptions.TenantId = Options.TenantId;
			workloadIdentityCredentialOptions.Pipeline = Pipeline;
			return new WorkloadIdentityCredential(workloadIdentityCredentialOptions);
		}

		public virtual TokenCredential CreateManagedIdentityCredential()
		{
			DefaultAzureCredentialOptions defaultAzureCredentialOptions = Options.Clone<DefaultAzureCredentialOptions>();
			defaultAzureCredentialOptions.IsChainedCredential = true;
			return new ManagedIdentityCredential(new ManagedIdentityClient(new ManagedIdentityClientOptions
			{
				ResourceIdentifier = defaultAzureCredentialOptions.ManagedIdentityResourceId,
				ClientId = defaultAzureCredentialOptions.ManagedIdentityClientId,
				Pipeline = CredentialPipeline.GetInstance(defaultAzureCredentialOptions, IsManagedIdentityCredential: true),
				Options = defaultAzureCredentialOptions,
				InitialImdsConnectionTimeout = TimeSpan.FromSeconds(1.0),
				ExcludeTokenExchangeManagedIdentitySource = defaultAzureCredentialOptions.ExcludeWorkloadIdentityCredential
			}));
		}

		public virtual TokenCredential CreateSharedTokenCacheCredential()
		{
			SharedTokenCacheCredentialOptions sharedTokenCacheCredentialOptions = Options.Clone<SharedTokenCacheCredentialOptions>();
			sharedTokenCacheCredentialOptions.TenantId = Options.SharedTokenCacheTenantId;
			sharedTokenCacheCredentialOptions.Username = Options.SharedTokenCacheUsername;
			return new SharedTokenCacheCredential(Options.SharedTokenCacheTenantId, Options.SharedTokenCacheUsername, sharedTokenCacheCredentialOptions, Pipeline);
		}

		public virtual TokenCredential CreateInteractiveBrowserCredential()
		{
			InteractiveBrowserCredentialOptions interactiveBrowserCredentialOptions = Options.Clone<InteractiveBrowserCredentialOptions>();
			interactiveBrowserCredentialOptions.TokenCachePersistenceOptions = new TokenCachePersistenceOptions();
			interactiveBrowserCredentialOptions.TenantId = Options.InteractiveBrowserTenantId;
			return new InteractiveBrowserCredential(Options.InteractiveBrowserTenantId, Options.InteractiveBrowserCredentialClientId ?? "04b07795-8ddb-461a-bbee-02f9e1bf7b46", interactiveBrowserCredentialOptions, Pipeline);
		}

		public virtual TokenCredential CreateAzureDeveloperCliCredential()
		{
			AzureDeveloperCliCredentialOptions azureDeveloperCliCredentialOptions = Options.Clone<AzureDeveloperCliCredentialOptions>();
			azureDeveloperCliCredentialOptions.TenantId = Options.TenantId;
			azureDeveloperCliCredentialOptions.ProcessTimeout = Options.CredentialProcessTimeout;
			azureDeveloperCliCredentialOptions.IsChainedCredential = true;
			return new AzureDeveloperCliCredential(Pipeline, null, azureDeveloperCliCredentialOptions);
		}

		public virtual TokenCredential CreateAzureCliCredential()
		{
			AzureCliCredentialOptions azureCliCredentialOptions = Options.Clone<AzureCliCredentialOptions>();
			azureCliCredentialOptions.TenantId = Options.TenantId;
			azureCliCredentialOptions.ProcessTimeout = Options.CredentialProcessTimeout;
			azureCliCredentialOptions.IsChainedCredential = true;
			return new AzureCliCredential(Pipeline, null, azureCliCredentialOptions);
		}

		public virtual TokenCredential CreateVisualStudioCredential()
		{
			VisualStudioCredentialOptions visualStudioCredentialOptions = Options.Clone<VisualStudioCredentialOptions>();
			visualStudioCredentialOptions.TenantId = Options.VisualStudioTenantId;
			visualStudioCredentialOptions.ProcessTimeout = Options.CredentialProcessTimeout;
			visualStudioCredentialOptions.IsChainedCredential = true;
			return new VisualStudioCredential(Options.VisualStudioTenantId, Pipeline, null, null, visualStudioCredentialOptions);
		}

		public virtual TokenCredential CreateVisualStudioCodeCredential()
		{
			VisualStudioCodeCredentialOptions visualStudioCodeCredentialOptions = Options.Clone<VisualStudioCodeCredentialOptions>();
			visualStudioCodeCredentialOptions.TenantId = Options.VisualStudioCodeTenantId;
			visualStudioCodeCredentialOptions.IsChainedCredential = true;
			return new VisualStudioCodeCredential(visualStudioCodeCredentialOptions, Pipeline, null, null, null);
		}

		public virtual TokenCredential CreateAzurePowerShellCredential()
		{
			AzurePowerShellCredentialOptions azurePowerShellCredentialOptions = Options.Clone<AzurePowerShellCredentialOptions>();
			azurePowerShellCredentialOptions.TenantId = Options.TenantId;
			azurePowerShellCredentialOptions.ProcessTimeout = Options.CredentialProcessTimeout;
			azurePowerShellCredentialOptions.IsChainedCredential = true;
			return new AzurePowerShellCredential(azurePowerShellCredentialOptions, Pipeline, null);
		}
	}
	public struct DeviceCodeInfo
	{
		public string UserCode { get; private set; }

		public string DeviceCode { get; private set; }

		public Uri VerificationUri { get; private set; }

		public DateTimeOffset ExpiresOn { get; private set; }

		public string Message { get; private set; }

		public string ClientId { get; private set; }

		public IReadOnlyCollection<string> Scopes { get; private set; }

		internal DeviceCodeInfo(DeviceCodeResult deviceCode)
			: this(deviceCode.UserCode, deviceCode.DeviceCode, new Uri(deviceCode.VerificationUrl), deviceCode.ExpiresOn, deviceCode.Message, deviceCode.ClientId, deviceCode.Scopes)
		{
		}

		internal DeviceCodeInfo(string userCode, string deviceCode, Uri verificationUri, DateTimeOffset expiresOn, string message, string clientId, IReadOnlyCollection<string> scopes)
		{
			UserCode = userCode;
			DeviceCode = deviceCode;
			VerificationUri = verificationUri;
			ExpiresOn = expiresOn;
			Message = message;
			ClientId = clientId;
			Scopes = scopes;
		}
	}
	internal class EnvironmentVariables
	{
		public static string Username => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_USERNAME"));

		public static string Password => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_PASSWORD"));

		public static string TenantId => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_TENANT_ID"));

		public static List<string> AdditionallyAllowedTenants => (Environment.GetEnvironmentVariable("AZURE_ADDITIONALLY_ALLOWED_TENANTS") ?? string.Empty).Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

		public static string ClientId => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_CLIENT_ID"));

		public static string ClientSecret => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET"));

		public static string ClientCertificatePath => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_CLIENT_CERTIFICATE_PATH"));

		public static string ClientCertificatePassword => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_CLIENT_CERTIFICATE_PASSWORD"));

		public static bool ClientSendCertificateChain => EnvironmentVariableToBool(Environment.GetEnvironmentVariable("AZURE_CLIENT_SEND_CERTIFICATE_CHAIN"));

		public static string IdentityEndpoint => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("IDENTITY_ENDPOINT"));

		public static string IdentityHeader => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("IDENTITY_HEADER"));

		public static string MsiEndpoint => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("MSI_ENDPOINT"));

		public static string MsiSecret => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("MSI_SECRET"));

		public static string ImdsEndpoint => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("IMDS_ENDPOINT"));

		public static string IdentityServerThumbprint => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("IDENTITY_SERVER_THUMBPRINT"));

		public static string PodIdentityEndpoint => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_POD_IDENTITY_AUTHORITY_HOST"));

		public static string Path => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("PATH"));

		public static string ProgramFilesX86 => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("ProgramFiles(x86)"));

		public static string ProgramFiles => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("ProgramFiles"));

		public static string AuthorityHost => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_AUTHORITY_HOST"));

		public static string AzureRegionalAuthorityName => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_REGIONAL_AUTHORITY_NAME"));

		public static string AzureFederatedTokenFile => GetNonEmptyStringOrNull(Environment.GetEnvironmentVariable("AZURE_FEDERATED_TOKEN_FILE"));

		private static string GetNonEmptyStringOrNull(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return null;
			}
			return str;
		}

		private static bool EnvironmentVariableToBool(string str)
		{
			if (!string.Equals(bool.TrueString, str, StringComparison.OrdinalIgnoreCase))
			{
				return string.Equals("1", str, StringComparison.OrdinalIgnoreCase);
			}
			return true;
		}
	}
	internal class FileContentsCache
	{
		private SemaphoreSlim _lock = new SemaphoreSlim(1);

		private readonly string _tokenFilePath;

		private string _tokenFileContents;

		private DateTimeOffset _refreshOn = DateTimeOffset.MinValue;

		private readonly TimeSpan _refreshInterval;

		public FileContentsCache(string tokenFilePath, TimeSpan? refreshInterval = null)
		{
			_refreshInterval = refreshInterval ?? TimeSpan.FromMinutes(5.0);
			_tokenFilePath = tokenFilePath;
		}

		public async Task<string> GetTokenFileContentsAsync(CancellationToken cancellationToken)
		{
			if (_refreshOn <= DateTimeOffset.UtcNow)
			{
				await _lock.WaitAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				try
				{
					if (_refreshOn <= DateTimeOffset.UtcNow)
					{
						using StreamReader reader = File.OpenText(_tokenFilePath);
						_tokenFileContents = await reader.ReadToEndAsync().ConfigureAwait(continueOnCapturedContext: false);
						_refreshOn = DateTimeOffset.UtcNow + _refreshInterval;
					}
				}
				finally
				{
					_lock.Release();
				}
			}
			return _tokenFileContents;
		}
	}
	internal class FileSystemService : IFileSystemService
	{
		public static IFileSystemService Default { get; } = new FileSystemService();

		private FileSystemService()
		{
		}

		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		public string ReadAllText(string path)
		{
			return File.ReadAllText(path);
		}
	}
	internal class HttpPipelineClientFactory : IMsalHttpClientFactory
	{
		private readonly HttpPipeline _pipeline;

		public HttpPipelineClientFactory(HttpPipeline pipeline)
		{
			_pipeline = pipeline;
		}

		public HttpClient GetHttpClient()
		{
			return new HttpClient(new HttpPipelineMessageHandler(_pipeline));
		}
	}
	internal class IdentityCompatSwitches
	{
		internal const string DisableInteractiveThreadpoolExecutionSwitchName = "Azure.Identity.DisableInteractiveBrowserThreadpoolExecution";

		internal const string DisableInteractiveThreadpoolExecutionEnvVar = "AZURE_IDENTITY_DISABLE_INTERACTIVEBROWSERTHREADPOOLEXECUTION";

		internal const string DisableCP1ExecutionSwitchName = "Azure.Identity.DisableCP1";

		internal const string DisableCP1ExecutionEnvVar = "AZURE_IDENTITY_DISABLE_CP1";

		internal const string DisableMultiTenantAuthSwitchName = "Azure.Identity.DisableMultiTenantAuth";

		internal const string DisableMultiTenantAuthEnvVar = "AZURE_IDENTITY_DISABLE_MULTITENANTAUTH";

		public static bool DisableInteractiveBrowserThreadpoolExecution => Azure.Core.AppContextSwitchHelper.GetConfigValue("Azure.Identity.DisableInteractiveBrowserThreadpoolExecution", "AZURE_IDENTITY_DISABLE_INTERACTIVEBROWSERTHREADPOOLEXECUTION");

		public static bool DisableCP1 => Azure.Core.AppContextSwitchHelper.GetConfigValue("Azure.Identity.DisableCP1", "AZURE_IDENTITY_DISABLE_CP1");

		public static bool DisableTenantDiscovery => Azure.Core.AppContextSwitchHelper.GetConfigValue("Azure.Identity.DisableMultiTenantAuth", "AZURE_IDENTITY_DISABLE_MULTITENANTAUTH");
	}
	public static class IdentityModelFactory
	{
		public static AuthenticationRecord AuthenticationRecord(string username, string authority, string homeAccountId, string tenantId, string clientId)
		{
			return new AuthenticationRecord(username, authority, homeAccountId, tenantId, clientId);
		}

		public static DeviceCodeInfo DeviceCodeInfo(string userCode, string deviceCode, Uri verificationUri, DateTimeOffset expiresOn, string message, string clientId, IReadOnlyCollection<string> scopes)
		{
			return new DeviceCodeInfo(userCode, deviceCode, verificationUri, expiresOn, message, clientId, scopes);
		}
	}
	internal interface IFileSystemService
	{
		bool FileExists(string path);

		string ReadAllText(string path);
	}
	internal class ImdsManagedIdentitySource : ManagedIdentitySource
	{
		private class ImdsRequestFailedDetailsParser : RequestFailedDetailsParser
		{
			private readonly string _baseMessage;

			public ImdsRequestFailedDetailsParser(string baseMessage)
			{
				_baseMessage = baseMessage;
			}

			public override bool TryParse(Response response, out ResponseError error, out IDictionary<string, string> data)
			{
				error = new ResponseError(null, _baseMessage);
				data = null;
				return true;
			}
		}

		internal class ProbeRequestResponseException : Exception
		{
		}

		internal static readonly Uri s_imdsEndpoint = new Uri("http://169.254.169.254/metadata/identity/oauth2/token");

		internal const string imddsTokenPath = "/metadata/identity/oauth2/token";

		internal const string metadataHeaderName = "Metadata";

		private const string ImdsApiVersion = "2018-02-01";

		internal const string IdentityUnavailableError = "ManagedIdentityCredential authentication unavailable. The requested identity has not been assigned to this resource.";

		internal const string NoResponseError = "ManagedIdentityCredential authentication unavailable. No response received from the managed identity endpoint.";

		internal const string TimeoutError = "ManagedIdentityCredential authentication unavailable. The request to the managed identity endpoint timed out.";

		internal const string GatewayError = "ManagedIdentityCredential authentication unavailable. The request failed due to a gateway error.";

		internal const string AggregateError = "ManagedIdentityCredential authentication unavailable. Multiple attempts failed to obtain a token from the managed identity endpoint.";

		private readonly string _clientId;

		private readonly string _resourceId;

		private readonly Uri _imdsEndpoint;

		private bool _isFirstRequest = true;

		private TimeSpan? _imdsNetworkTimeout;

		private bool _isChainedCredential;

		internal ImdsManagedIdentitySource(ManagedIdentityClientOptions options)
			: base(options.Pipeline)
		{
			_clientId = options.ClientId;
			_resourceId = options.ResourceIdentifier?.ToString();
			_imdsNetworkTimeout = options.InitialImdsConnectionTimeout;
			_isChainedCredential = options.Options?.IsChainedCredential ?? false;
			_imdsEndpoint = GetImdsUri();
		}

		internal static Uri GetImdsUri()
		{
			if (!string.IsNullOrEmpty(EnvironmentVariables.PodIdentityEndpoint))
			{
				return new UriBuilder(EnvironmentVariables.PodIdentityEndpoint)
				{
					Path = "/metadata/identity/oauth2/token"
				}.Uri;
			}
			return s_imdsEndpoint;
		}

		protected override Request CreateRequest(string[] scopes)
		{
			string value = ScopeUtilities.ScopesToResource(scopes);
			Request request = base.Pipeline.HttpPipeline.CreateRequest();
			request.Method = RequestMethod.Get;
			if (!_isFirstRequest || !_isChainedCredential)
			{
				SetNonProbeRequest(request);
			}
			request.Uri.Reset(_imdsEndpoint);
			request.Uri.AppendQuery("api-version", "2018-02-01");
			request.Uri.AppendQuery("resource", value);
			if (!string.IsNullOrEmpty(_clientId))
			{
				request.Uri.AppendQuery("client_id", _clientId);
			}
			if (!string.IsNullOrEmpty(_resourceId))
			{
				request.Uri.AppendQuery("mi_res_id", _resourceId);
			}
			return request;
		}

		protected override HttpMessage CreateHttpMessage(Request request)
		{
			HttpMessage httpMessage = base.CreateHttpMessage(request);
			if (_isFirstRequest && _isChainedCredential)
			{
				httpMessage.NetworkTimeout = _imdsNetworkTimeout;
			}
			return httpMessage;
		}

		public override async ValueTask<AccessToken> AuthenticateAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			AccessToken result = default(AccessToken);
			int num;
			try
			{
				result = await base.AuthenticateAsync(async, context, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				return result;
			}
			catch (RequestFailedException ex) when (ex.Status == 0)
			{
				if (ex.InnerException is TaskCanceledException)
				{
					throw;
				}
				throw new CredentialUnavailableException("ManagedIdentityCredential authentication unavailable. No response received from the managed identity endpoint.", ex);
			}
			catch (TaskCanceledException innerException)
			{
				throw new CredentialUnavailableException("ManagedIdentityCredential authentication unavailable. No response received from the managed identity endpoint.", innerException);
			}
			catch (AggregateException innerException2)
			{
				throw new CredentialUnavailableException("ManagedIdentityCredential authentication unavailable. Multiple attempts failed to obtain a token from the managed identity endpoint.", innerException2);
			}
			catch (CredentialUnavailableException)
			{
				throw;
			}
			catch (ProbeRequestResponseException)
			{
				num = 1;
			}
			if (num != 1)
			{
				return result;
			}
			return await base.AuthenticateAsync(async, context, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		protected override async ValueTask<AccessToken> HandleResponseAsync(bool async, TokenRequestContext context, HttpMessage message, CancellationToken cancellationToken)
		{
			Response response = message.Response;
			_imdsNetworkTimeout = null;
			_isFirstRequest = false;
			string text;
			switch (response.Status)
			{
			case 400:
				if (IsProbRequest(message))
				{
					throw new ProbeRequestResponseException();
				}
				text = "ManagedIdentityCredential authentication unavailable. The requested identity has not been assigned to this resource.";
				break;
			case 502:
				text = "ManagedIdentityCredential authentication unavailable. The request failed due to a gateway error.";
				break;
			case 504:
				text = "ManagedIdentityCredential authentication unavailable. The request failed due to a gateway error.";
				break;
			default:
				text = null;
				break;
			}
			string text2 = text;
			if (text2 != null)
			{
				string content = new RequestFailedException(response, null, new ImdsRequestFailedDetailsParser(text2)).Message;
				string text3 = await ManagedIdentitySource.GetMessageFromResponse(response, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (text3 != null)
				{
					content = content + Environment.NewLine + text3;
				}
				throw new CredentialUnavailableException(content);
			}
			return await base.HandleResponseAsync(async, context, message, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public static bool IsProbRequest(HttpMessage message)
		{
			string value;
			if (message.Request.Uri.Host == s_imdsEndpoint.Host && message.Request.Uri.Path == s_imdsEndpoint.AbsolutePath)
			{
				return !message.Request.Headers.TryGetValue("Metadata", out value);
			}
			return false;
		}

		public static void SetNonProbeRequest(Request request)
		{
			request.Headers.Add("Metadata", "true");
		}
	}
	[Friend("Azure.Identity.Broker")]
	internal interface IMsalPublicClientInitializerOptions
	{
		Action<PublicClientApplicationBuilder> BeforeBuildClient { get; }

		bool UseDefaultBrokerAccount { get; set; }
	}
	internal class InMemoryTokenCacheOptions : UnsafeTokenCacheOptions
	{
		protected internal override Task<ReadOnlyMemory<byte>> RefreshCacheAsync()
		{
			return Task.FromResult(default(ReadOnlyMemory<byte>));
		}

		protected internal override Task TokenCacheUpdatedAsync(TokenCacheUpdatedArgs tokenCacheUpdatedArgs)
		{
			return Task.CompletedTask;
		}
	}
	internal interface IProcess : IDisposable
	{
		bool HasExited { get; }

		int ExitCode { get; }

		ProcessStartInfo StartInfo { get; set; }

		event EventHandler Exited;

		event DataReceivedEventHandler OutputDataReceived;

		event DataReceivedEventHandler ErrorDataReceived;

		bool Start();

		void Kill();

		void BeginOutputReadLine();

		void BeginErrorReadLine();
	}
	internal interface IProcessService
	{
		IProcess Create(ProcessStartInfo startInfo);
	}
	internal interface IScopeHandler
	{
		Azure.Core.Pipeline.DiagnosticScope CreateScope(Azure.Core.Pipeline.ClientDiagnostics diagnostics, string name);

		void Start(string name, in Azure.Core.Pipeline.DiagnosticScope scope);

		void Dispose(string name, in Azure.Core.Pipeline.DiagnosticScope scope);

		void Fail(string name, in Azure.Core.Pipeline.DiagnosticScope scope, Exception exception);
	}
	internal interface IVisualStudioCodeAdapter
	{
		string GetUserSettingsPath();

		string GetCredentials(string serviceName, string accountName);
	}
	internal interface IX509Certificate2Provider
	{
		ValueTask<X509Certificate2> GetCertificateAsync(bool async, CancellationToken cancellationToken);
	}
	internal static class LinuxNativeMethods
	{
		public enum SecretSchemaAttributeType
		{
			SECRET_SCHEMA_ATTRIBUTE_STRING,
			SECRET_SCHEMA_ATTRIBUTE_INTEGER,
			SECRET_SCHEMA_ATTRIBUTE_BOOLEAN
		}

		public enum SecretSchemaFlags
		{
			SECRET_SCHEMA_NONE = 0,
			SECRET_SCHEMA_DONT_MATCH_NAME = 2
		}

		internal struct GError
		{
			public uint Domain;

			public int Code;

			public string Message;
		}

		private static class Imports
		{
			[DllImport("libsecret-1.so.0", BestFitMapping = false, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
			[DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
			public static extern IntPtr secret_schema_new(string name, int flags, string attribute1, int attribute1Type, string attribute2, int attribute2Type, IntPtr end);

			[DllImport("libsecret-1.so.0", CallingConvention = CallingConvention.StdCall)]
			public static extern void secret_schema_unref(IntPtr schema);

			[DllImport("libsecret-1.so.0", BestFitMapping = false, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
			public static extern IntPtr secret_password_lookup_sync(IntPtr schema, IntPtr cancellable, out IntPtr error, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value, IntPtr end);

			[DllImport("libsecret-1.so.0", BestFitMapping = false, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
			public static extern int secret_password_store_sync(IntPtr schema, string collection, string label, string password, IntPtr cancellable, out IntPtr error, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value, IntPtr end);

			[DllImport("libsecret-1.so.0", BestFitMapping = false, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
			public static extern int secret_password_clear_sync(IntPtr schema, IntPtr cancellable, out IntPtr error, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value, IntPtr end);

			[DllImport("libsecret-1.so.0", CallingConvention = CallingConvention.StdCall)]
			public static extern void secret_password_free(IntPtr password);
		}

		public const string SECRET_COLLECTION_SESSION = "session";

		public static IntPtr secret_schema_new(string name, SecretSchemaFlags flags, string attribute1, SecretSchemaAttributeType attribute1Type, string attribute2, SecretSchemaAttributeType attribute2Type)
		{
			return Imports.secret_schema_new(name, (int)flags, attribute1, (int)attribute1Type, attribute2, (int)attribute2Type, IntPtr.Zero);
		}

		public static string secret_password_lookup_sync(IntPtr schemaPtr, IntPtr cancellable, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value)
		{
			IntPtr error;
			IntPtr intPtr = Imports.secret_password_lookup_sync(schemaPtr, cancellable, out error, attribute1Type, attribute1Value, attribute2Type, attribute2Value, IntPtr.Zero);
			HandleError(error, "An error was encountered while reading secret from keyring");
			if (!(intPtr != IntPtr.Zero))
			{
				return null;
			}
			return Marshal.PtrToStringAnsi(intPtr);
		}

		public static void secret_password_store_sync(IntPtr schemaPtr, string collection, string label, string password, IntPtr cancellable, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value)
		{
			Imports.secret_password_store_sync(schemaPtr, collection, label, password, cancellable, out var error, attribute1Type, attribute1Value, attribute2Type, attribute2Value, IntPtr.Zero);
			HandleError(error, "An error was encountered while writing secret to keyring");
		}

		public static void secret_password_clear_sync(IntPtr schemaPtr, IntPtr cancellable, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value)
		{
			Imports.secret_password_clear_sync(schemaPtr, cancellable, out var error, attribute1Type, attribute1Value, attribute2Type, attribute2Value, IntPtr.Zero);
			HandleError(error, "An error was encountered while clearing secret from keyring ");
		}

		public static void secret_password_free(IntPtr passwordPtr)
		{
			if (passwordPtr != IntPtr.Zero)
			{
				Imports.secret_password_free(passwordPtr);
			}
		}

		public static void secret_schema_unref(IntPtr schemaPtr)
		{
			if (schemaPtr != IntPtr.Zero)
			{
				Imports.secret_schema_unref(schemaPtr);
			}
		}

		private static void HandleError(IntPtr errorPtr, string errorMessage)
		{
			if (errorPtr == IntPtr.Zero)
			{
				return;
			}
			GError gError;
			try
			{
				gError = Marshal.PtrToStructure<GError>(errorPtr);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"An exception was encountered while processing libsecret error: {ex}", ex);
			}
			throw new InvalidOperationException($"{errorMessage}, domain:'{gError.Domain}', code:'{gError.Code}', message:'{gError.Message}'");
		}
	}
	internal sealed class LinuxVisualStudioCodeAdapter : IVisualStudioCodeAdapter
	{
		private static readonly string s_userSettingsJsonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Code", "User", "settings.json");

		public string GetUserSettingsPath()
		{
			return s_userSettingsJsonPath;
		}

		public string GetCredentials(string serviceName, string accountName)
		{
			Azure.Core.Argument.AssertNotNullOrEmpty(serviceName, "serviceName");
			Azure.Core.Argument.AssertNotNullOrEmpty(accountName, "accountName");
			IntPtr schemaPtr = GetLibsecretSchema();
			try
			{
				return LookupPassword(in schemaPtr, serviceName, accountName);
			}
			finally
			{
				LinuxNativeMethods.secret_schema_unref(schemaPtr);
			}
		}

		private static string LookupPassword(in IntPtr schemaPtr, string serviceName, string accountName)
		{
			return LinuxNativeMethods.secret_password_lookup_sync(schemaPtr, IntPtr.Zero, "service", serviceName, "account", accountName);
		}

		private static IntPtr GetLibsecretSchema()
		{
			return LinuxNativeMethods.secret_schema_new("org.freedesktop.Secret.Generic", LinuxNativeMethods.SecretSchemaFlags.SECRET_SCHEMA_DONT_MATCH_NAME, "service", LinuxNativeMethods.SecretSchemaAttributeType.SECRET_SCHEMA_ATTRIBUTE_STRING, "account", LinuxNativeMethods.SecretSchemaAttributeType.SECRET_SCHEMA_ATTRIBUTE_STRING);
		}
	}
	internal static class MacosNativeMethods
	{
		public static class Imports
		{
			private const string CoreFoundationLibrary = "/System/Library/Frameworks/CoreFoundation.framework/Versions/A/CoreFoundation";

			private const string SecurityLibrary = "/System/Library/Frameworks/Security.framework/Security";

			[DllImport("/System/Library/Frameworks/CoreFoundation.framework/Versions/A/CoreFoundation", CharSet = CharSet.Unicode)]
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
			public static extern void CFRelease(IntPtr cfRef);

			[DllImport("/System/Library/Frameworks/Security.framework/Security")]
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
			public static extern int SecKeychainFindGenericPassword(IntPtr keychainOrArray, int serviceNameLength, byte[] serviceName, int accountNameLength, byte[] accountName, out int passwordLength, out IntPtr passwordData, out IntPtr itemRef);

			[DllImport("/System/Library/Frameworks/Security.framework/Security")]
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
			public static extern int SecKeychainAddGenericPassword(IntPtr keychain, int serviceNameLength, byte[] serviceName, int accountNameLength, byte[] accountName, int passwordLength, byte[] passwordData, out IntPtr itemRef);

			[DllImport("/System/Library/Frameworks/Security.framework/Security")]
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
			public static extern int SecKeychainItemDelete(IntPtr itemRef);

			[DllImport("/System/Library/Frameworks/Security.framework/Security")]
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
			public static extern int SecKeychainItemFreeContent(IntPtr attrList, IntPtr data);

			[DllImport("/System/Library/Frameworks/Security.framework/Security")]
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
			public static extern IntPtr SecCopyErrorMessageString(int status, IntPtr reserved);
		}

		public const int SecStatusCodeSuccess = 0;

		public const int SecStatusCodeNoSuchKeychain = -25294;

		public const int SecStatusCodeInvalidKeychain = -25295;

		public const int SecStatusCodeAuthFailed = -25293;

		public const int SecStatusCodeDuplicateItem = -25299;

		public const int SecStatusCodeItemNotFound = -25300;

		public const int SecStatusCodeInteractionNotAllowed = -25308;

		public const int SecStatusCodeInteractionRequired = -25315;

		public const int SecStatusCodeNoSuchAttr = -25303;

		public static void SecKeychainFindGenericPassword(IntPtr keychainOrArray, string serviceName, string accountName, out int passwordLength, out IntPtr credentialsPtr, out IntPtr itemRef)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(serviceName);
			byte[] bytes2 = Encoding.UTF8.GetBytes(accountName);
			ThrowIfError(Imports.SecKeychainFindGenericPassword(keychainOrArray, bytes.Length, bytes, bytes2.Length, bytes2, out passwordLength, out credentialsPtr, out itemRef));
		}

		public static void SecKeychainAddGenericPassword(IntPtr keychainOrArray, string serviceName, string accountName, string password, out IntPtr itemRef)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(serviceName);
			byte[] bytes2 = Encoding.UTF8.GetBytes(accountName);
			byte[] bytes3 = Encoding.UTF8.GetBytes(password);
			ThrowIfError(Imports.SecKeychainAddGenericPassword(keychainOrArray, bytes.Length, bytes, bytes2.Length, bytes2, password.Length, bytes3, out itemRef));
		}

		public static void SecKeychainItemDelete(IntPtr itemRef)
		{
			ThrowIfError(Imports.SecKeychainItemDelete(itemRef));
		}

		public static void SecKeychainItemFreeContent(IntPtr attrList, IntPtr data)
		{
			ThrowIfError(Imports.SecKeychainItemFreeContent(attrList, data));
		}

		public static void CFRelease(IntPtr cfRef)
		{
			if (cfRef != IntPtr.Zero)
			{
				Imports.CFRelease(cfRef);
			}
		}

		private static void ThrowIfError(int status)
		{
			if (status != 0)
			{
				throw new InvalidOperationException(GetErrorMessageString(status));
			}
		}

		private static string GetErrorMessageString(int status)
		{
			return status switch
			{
				-25294 => $"The keychain does not exist. [{status}]", 
				-25295 => $"The keychain is not valid. [{status}]", 
				-25293 => $"Authorization/Authentication failed. [{status}]", 
				-25299 => $"The item already exists. [{status}]", 
				-25300 => $"The item cannot be found. [{status}]", 
				-25308 => $"Interaction with the Security Server is not allowed. [{status}]", 
				-25315 => $"User interaction is required. [{status}]", 
				-25303 => $"The attribute does not exist. [{status}]", 
				_ => $"Unknown error. [{status}]", 
			};
		}
	}
	internal sealed class MacosVisualStudioCodeAdapter : IVisualStudioCodeAdapter
	{
		private static readonly string s_userSettingsJsonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Library", "Application Support", "Code", "User", "settings.json");

		public string GetUserSettingsPath()
		{
			return s_userSettingsJsonPath;
		}

		public string GetCredentials(string serviceName, string accountName)
		{
			Azure.Core.Argument.AssertNotNullOrEmpty(serviceName, "serviceName");
			Azure.Core.Argument.AssertNotNullOrEmpty(accountName, "accountName");
			IntPtr credentialsPtr = IntPtr.Zero;
			IntPtr itemRef = IntPtr.Zero;
			try
			{
				MacosNativeMethods.SecKeychainFindGenericPassword(IntPtr.Zero, serviceName, accountName, out var passwordLength, out credentialsPtr, out itemRef);
				if (passwordLength <= 0)
				{
					throw new InvalidOperationException("No password found");
				}
				return Marshal.PtrToStringAnsi(credentialsPtr, passwordLength);
			}
			finally
			{
				try
				{
					MacosNativeMethods.SecKeychainItemFreeContent(IntPtr.Zero, credentialsPtr);
				}
				finally
				{
					MacosNativeMethods.CFRelease(itemRef);
				}
			}
		}
	}
	internal class ManagedIdentityClient
	{
		internal const string MsiUnavailableError = "ManagedIdentityCredential authentication unavailable. No Managed Identity endpoint found.";

		internal Lazy<ManagedIdentitySource> _identitySource;

		private MsalConfidentialClient _msal;

		internal CredentialPipeline Pipeline { get; }

		protected internal string ClientId { get; }

		internal ResourceIdentifier ResourceIdentifier { get; }

		protected ManagedIdentityClient()
		{
		}

		public ManagedIdentityClient(CredentialPipeline pipeline, string clientId = null)
			: this(new ManagedIdentityClientOptions
			{
				Pipeline = pipeline,
				ClientId = clientId
			})
		{
		}

		public ManagedIdentityClient(CredentialPipeline pipeline, ResourceIdentifier resourceId)
			: this(new ManagedIdentityClientOptions
			{
				Pipeline = pipeline,
				ResourceIdentifier = resourceId
			})
		{
		}

		public ManagedIdentityClient(ManagedIdentityClientOptions options)
		{
			if (options.ClientId != null && options.ResourceIdentifier != null)
			{
				throw new ArgumentException("ManagedIdentityClientOptions cannot specify both ResourceIdentifier and ClientId.");
			}
			ClientId = (string.IsNullOrEmpty(options.ClientId) ? null : options.ClientId);
			ResourceIdentifier = (string.IsNullOrEmpty(options.ResourceIdentifier) ? null : options.ResourceIdentifier);
			Pipeline = options.Pipeline;
			_identitySource = new Lazy<ManagedIdentitySource>(() => SelectManagedIdentitySource(options));
			_msal = new MsalConfidentialClient(Pipeline, "MANAGED-IDENTITY-RESOURCE-TENENT", ClientId ?? "SYSTEM-ASSIGNED-MANAGED-IDENTITY", AppTokenProviderImpl, options.Options);
		}

		public async ValueTask<AccessToken> AuthenticateAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			return (await _msal.AcquireTokenForClientAsync(context.Scopes, context.TenantId, context.Claims, context.IsCaeEnabled, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).ToAccessToken();
		}

		public virtual async ValueTask<AccessToken> AuthenticateCoreAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			return await _identitySource.Value.AuthenticateAsync(async, context, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async Task<AppTokenProviderResult> AppTokenProviderImpl(AppTokenProviderParameters parameters)
		{
			TokenRequestContext context = new TokenRequestContext(parameters.Scopes.ToArray(), null, parameters.Claims, null, isCaeEnabled: false);
			AccessToken accessToken = await AuthenticateCoreAsync(async: true, context, parameters.CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return new AppTokenProviderResult
			{
				AccessToken = accessToken.Token,
				ExpiresInSeconds = Math.Max(Convert.ToInt64((accessToken.ExpiresOn - DateTimeOffset.UtcNow).TotalSeconds), 1L)
			};
		}

		private static ManagedIdentitySource SelectManagedIdentitySource(ManagedIdentityClientOptions options)
		{
			return ServiceFabricManagedIdentitySource.TryCreate(options) ?? AppServiceV2019ManagedIdentitySource.TryCreate(options) ?? AppServiceV2017ManagedIdentitySource.TryCreate(options) ?? CloudShellManagedIdentitySource.TryCreate(options) ?? AzureArcManagedIdentitySource.TryCreate(options) ?? TokenExchangeManagedIdentitySource.TryCreate(options) ?? new ImdsManagedIdentitySource(options);
		}
	}
	internal class ManagedIdentityClientOptions
	{
		public TokenCredentialOptions Options { get; set; }

		public string ClientId { get; set; }

		public ResourceIdentifier ResourceIdentifier { get; set; }

		public bool PreserveTransport { get; set; }

		public TimeSpan? InitialImdsConnectionTimeout { get; set; }

		public CredentialPipeline Pipeline { get; set; }

		public bool ExcludeTokenExchangeManagedIdentitySource { get; set; }
	}
	internal class ManagedIdentityRequestFailedDetailsParser : RequestFailedDetailsParser
	{
		public override bool TryParse(Response response, out ResponseError error, out IDictionary<string, string> data)
		{
			data = new Dictionary<string, string>();
			error = null;
			try
			{
				string text = response.Content.ToString();
				if (text == null || !text.StartsWith("{", StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				string message = ManagedIdentitySource.GetMessageFromResponse(response, async: false, CancellationToken.None).EnsureCompleted();
				error = new ResponseError(null, message);
				return true;
			}
			catch
			{
				error = new ResponseError(null, "Managed Identity response was not in the expected format. See the inner exception for details.");
				return true;
			}
		}
	}
	internal abstract class ManagedIdentitySource
	{
		private class ManagedIdentityResponseClassifier : ResponseClassifier
		{
			public override bool IsRetriableResponse(HttpMessage message)
			{
				return message.Response.Status switch
				{
					404 => true, 
					410 => true, 
					502 => false, 
					_ => base.IsRetriableResponse(message), 
				};
			}
		}

		internal const string AuthenticationResponseInvalidFormatError = "Invalid response, the authentication response was not in the expected format.";

		internal const string UnexpectedResponse = "Managed Identity response was not in the expected format. See the inner exception for details.";

		private ManagedIdentityResponseClassifier _responseClassifier;

		protected internal CredentialPipeline Pipeline { get; }

		protected internal string ClientId { get; }

		protected ManagedIdentitySource(CredentialPipeline pipeline)
		{
			Pipeline = pipeline;
			_responseClassifier = new ManagedIdentityResponseClassifier();
		}

		public virtual async ValueTask<AccessToken> AuthenticateAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			using HttpMessage message = CreateHttpMessage(CreateRequest(context.Scopes));
			if (async)
			{
				await Pipeline.HttpPipeline.SendAsync(message, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				Pipeline.HttpPipeline.Send(message, cancellationToken);
			}
			return await HandleResponseAsync(async, context, message, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		protected virtual async ValueTask<AccessToken> HandleResponseAsync(bool async, TokenRequestContext context, HttpMessage message, CancellationToken cancellationToken)
		{
			Exception innerException = null;
			Response response = message.Response;
			try
			{
				if (response.Status == 200)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						throw new TaskCanceledException();
					}
					JsonDocument jsonDocument = ((!async) ? JsonDocument.Parse(response.ContentStream) : (await JsonDocument.ParseAsync(response.ContentStream, default(JsonDocumentOptions), cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
					using JsonDocument jsonDocument2 = jsonDocument;
					return GetTokenFromResponse(jsonDocument2.RootElement);
				}
			}
			catch (JsonException innerException2)
			{
				throw new CredentialUnavailableException("Managed Identity response was not in the expected format. See the inner exception for details.", innerException2);
			}
			catch (Exception innerException3) when (response.Status == 200)
			{
				throw new RequestFailedException("Response from Managed Identity was successful, but the operation timed out prior to completion.", innerException3);
			}
			catch (Exception ex)
			{
				innerException = ex;
			}
			if (response.Status == 403)
			{
				string text = response.Content.ToString();
				if (text.Contains("unreachable"))
				{
					throw new CredentialUnavailableException("Managed Identity response was not in the expected format. See the inner exception for details.", new Exception(text));
				}
			}
			throw new RequestFailedException(response, innerException);
		}

		protected abstract Request CreateRequest(string[] scopes);

		protected virtual HttpMessage CreateHttpMessage(Request request)
		{
			return new HttpMessage(request, _responseClassifier);
		}

		internal static async Task<string> GetMessageFromResponse(Response response, bool async, CancellationToken cancellationToken)
		{
			if (response?.ContentStream == null || !response.ContentStream.CanRead || response.ContentStream.Length == 0L)
			{
				return null;
			}
			try
			{
				response.ContentStream.Position = 0L;
				JsonDocument jsonDocument = ((!async) ? JsonDocument.Parse(response.ContentStream) : (await JsonDocument.ParseAsync(response.ContentStream, default(JsonDocumentOptions), cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
				using JsonDocument jsonDocument2 = jsonDocument;
				return GetMessageFromResponse(jsonDocument2.RootElement);
			}
			catch
			{
				return "Response was not in a valid json format.";
			}
		}

		protected static string GetMessageFromResponse(in JsonElement root)
		{
			foreach (JsonProperty item in root.EnumerateObject())
			{
				if (item.Name == "Message")
				{
					return item.Value.GetString();
				}
			}
			return null;
		}

		private static AccessToken GetTokenFromResponse(in JsonElement root)
		{
			string text = null;
			DateTimeOffset? dateTimeOffset = null;
			foreach (JsonProperty item in root.EnumerateObject())
			{
				string name = item.Name;
				if (!(name == "access_token"))
				{
					if (name == "expires_on")
					{
						dateTimeOffset = TryParseExpiresOn(item.Value);
					}
				}
				else
				{
					text = item.Value.GetString();
				}
			}
			if (text != null && dateTimeOffset.HasValue)
			{
				return new AccessToken(text, dateTimeOffset.Value, InferManagedIdentityRefreshInValue(dateTimeOffset.Value));
			}
			throw new AuthenticationFailedException("Invalid response, the authentication response was not in the expected format.");
		}

		private static DateTimeOffset? TryParseExpiresOn(JsonElement jsonExpiresOn)
		{
			if ((jsonExpiresOn.ValueKind == JsonValueKind.Number && jsonExpiresOn.TryGetInt64(out var value)) || (jsonExpiresOn.ValueKind == JsonValueKind.String && long.TryParse(jsonExpiresOn.GetString(), out value)))
			{
				return DateTimeOffset.FromUnixTimeSeconds(value);
			}
			if (jsonExpiresOn.ValueKind == JsonValueKind.String && DateTimeOffset.TryParse(jsonExpiresOn.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
			{
				return result;
			}
			return null;
		}

		private static DateTimeOffset? InferManagedIdentityRefreshInValue(DateTimeOffset expiresOn)
		{
			if (expiresOn > DateTimeOffset.UtcNow.AddHours(2.0) && expiresOn < DateTimeOffset.MaxValue)
			{
				return expiresOn.AddTicks(-(expiresOn.Ticks - DateTimeOffset.UtcNow.Ticks) / 2);
			}
			return null;
		}
	}
	internal class MsalCacheHelperWrapper
	{
		private MsalCacheHelper _helper;

		public virtual async Task InitializeAsync(StorageCreationProperties storageCreationProperties, TraceSource logger = null)
		{
			_helper = await MsalCacheHelper.CreateAsync(storageCreationProperties, logger).ConfigureAwait(continueOnCapturedContext: false);
		}

		public virtual void VerifyPersistence()
		{
			_helper.VerifyPersistence();
		}

		public virtual void RegisterCache(ITokenCache tokenCache)
		{
			_helper.RegisterCache(tokenCache);
		}

		public virtual void UnregisterCache(ITokenCache tokenCache)
		{
			_helper.UnregisterCache(tokenCache);
		}

		public virtual byte[] LoadUnencryptedTokenCache()
		{
			return _helper.LoadUnencryptedTokenCache();
		}

		public virtual void SaveUnencryptedTokenCache(byte[] tokenCache)
		{
			_helper.SaveUnencryptedTokenCache(tokenCache);
		}
	}
	internal class MsalCacheReader
	{
		private readonly string _cachePath;

		private readonly string _cacheLockPath;

		private readonly int _cacheRetryCount;

		private readonly TimeSpan _cacheRetryDelay;

		private DateTimeOffset _lastReadTime;

		public MsalCacheReader(ITokenCache cache, string cachePath, int cacheRetryCount, TimeSpan cacheRetryDelay)
		{
			_cachePath = cachePath;
			_cacheLockPath = cachePath + ".lockfile";
			_cacheRetryCount = cacheRetryCount;
			_cacheRetryDelay = cacheRetryDelay;
			cache.SetBeforeAccessAsync(OnBeforeAccessAsync);
		}

		private async Task OnBeforeAccessAsync(TokenCacheNotificationArgs args)
		{
			_ = 1;
			try
			{
				DateTime cacheTimestamp = File.GetLastWriteTimeUtc(_cachePath);
				if (!File.Exists(_cachePath) || !(_lastReadTime < cacheTimestamp))
				{
					return;
				}
				using (await SentinelFileLock.AcquireAsync(_cacheLockPath, _cacheRetryCount, _cacheRetryDelay).ConfigureAwait(continueOnCapturedContext: false))
				{
					byte[] array = await ReadCacheFromProtectedStorageAsync().ConfigureAwait(continueOnCapturedContext: false);
					_lastReadTime = cacheTimestamp;
					if (array != null)
					{
						args.TokenCache.DeserializeMsalV3(array);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private async Task<byte[]> ReadCacheFromProtectedStorageAsync()
		{
			using FileStream file = File.OpenRead(_cachePath);
			byte[] protectedBytes = new byte[file.Length];
			await file.ReadAsync(protectedBytes, 0, protectedBytes.Length).ConfigureAwait(continueOnCapturedContext: false);
			return ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope.CurrentUser);
		}
	}
	internal abstract class MsalClientBase<TClient> where TClient : IClientApplicationBase
	{
		private readonly Azure.Core.AsyncLockWithValue<(TClient Client, TokenCache Cache)> _clientAsyncLock;

		private readonly Azure.Core.AsyncLockWithValue<(TClient Client, TokenCache Cache)> _clientWithCaeAsyncLock;

		private readonly bool _logAccountDetails;

		private readonly TokenCachePersistenceOptions _tokenCachePersistenceOptions;

		protected string[] cp1Capabilities = new string[1] { "CP1" };

		protected internal bool IsSupportLoggingEnabled { get; }

		protected internal bool DisableInstanceDiscovery { get; }

		protected internal CredentialPipeline Pipeline { get; }

		internal string TenantId { get; }

		internal string ClientId { get; }

		internal Uri AuthorityHost { get; }

		protected MsalClientBase()
		{
		}

		protected MsalClientBase(CredentialPipeline pipeline, string tenantId, string clientId, TokenCredentialOptions options)
		{
			Validations.ValidateAuthorityHost(options?.AuthorityHost);
			AuthorityHost = options?.AuthorityHost ?? AzureAuthorityHosts.GetDefault();
			_logAccountDetails = options?.Diagnostics?.IsAccountIdentifierLoggingEnabled == true;
			DisableInstanceDiscovery = options is ISupportsDisableInstanceDiscovery supportsDisableInstanceDiscovery && supportsDisableInstanceDiscovery.DisableInstanceDiscovery;
			_tokenCachePersistenceOptions = (options as ISupportsTokenCachePersistenceOptions)?.TokenCachePersistenceOptions;
			IsSupportLoggingEnabled = options?.IsUnsafeSupportLoggingEnabled ?? false;
			Pipeline = pipeline;
			TenantId = tenantId;
			ClientId = clientId;
			_clientAsyncLock = new Azure.Core.AsyncLockWithValue<(TClient, TokenCache)>();
			_clientWithCaeAsyncLock = new Azure.Core.AsyncLockWithValue<(TClient, TokenCache)>();
		}

		protected abstract ValueTask<TClient> CreateClientAsync(bool enableCae, bool async, CancellationToken cancellationToken);

		protected async ValueTask<TClient> GetClientAsync(bool enableCae, bool async, CancellationToken cancellationToken)
		{
			Azure.Core.AsyncLockWithValue<(TClient, TokenCache)>.LockOrValue lockOrValue = ((!enableCae) ? (await _clientAsyncLock.GetLockOrValueAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)) : (await _clientWithCaeAsyncLock.GetLockOrValueAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
			using Azure.Core.AsyncLockWithValue<(TClient Client, TokenCache Cache)>.LockOrValue asyncLock = lockOrValue;
			if (asyncLock.HasValue)
			{
				return asyncLock.Value.Client;
			}
			TClient client = await CreateClientAsync(enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			TokenCache tokenCache = null;
			if (_tokenCachePersistenceOptions != null)
			{
				tokenCache = new TokenCache(_tokenCachePersistenceOptions, enableCae);
				await tokenCache.RegisterCache(async, client.UserTokenCache, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (client is IConfidentialClientApplication confidentialClientApplication)
				{
					await tokenCache.RegisterCache(async, confidentialClientApplication.AppTokenCache, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
			}
			asyncLock.SetValue((client, tokenCache));
			return client;
		}

		protected void LogMsal(LogLevel level, string message, bool isPii)
		{
			if (!isPii || IsSupportLoggingEnabled)
			{
				AzureIdentityEventSource.Singleton.LogMsal(level, message);
			}
		}

		protected void LogAccountDetails(AuthenticationResult result)
		{
			if (_logAccountDetails)
			{
				(string, string, string, string) tuple = TokenHelper.ParseAccountInfoFromToken(result.AccessToken);
				AzureIdentityEventSource.Singleton.AuthenticatedAccountDetails(tuple.Item1, tuple.Item2 ?? result.TenantId, tuple.Item3 ?? result.Account?.Username, tuple.Item4 ?? result.UniqueId);
			}
		}

		internal async ValueTask<TokenCache> GetTokenCache(bool enableCae)
		{
			Azure.Core.AsyncLockWithValue<(TClient, TokenCache)>.LockOrValue lockOrValue = ((!enableCae) ? (await _clientAsyncLock.GetLockOrValueAsync(async: true).ConfigureAwait(continueOnCapturedContext: false)) : (await _clientWithCaeAsyncLock.GetLockOrValueAsync(async: true).ConfigureAwait(continueOnCapturedContext: false)));
			using Azure.Core.AsyncLockWithValue<(TClient, TokenCache)>.LockOrValue lockOrValue2 = lockOrValue;
			return lockOrValue2.HasValue ? lockOrValue2.Value.Item2 : null;
		}
	}
	internal class MsalConfidentialClient : MsalClientBase<IConfidentialClientApplication>
	{
		internal readonly string _clientSecret;

		internal readonly bool _includeX5CClaimHeader;

		internal readonly IX509Certificate2Provider _certificateProvider;

		private readonly Func<string> _clientAssertionCallback;

		private readonly Func<CancellationToken, Task<string>> _clientAssertionCallbackAsync;

		private readonly Func<AppTokenProviderParameters, Task<AppTokenProviderResult>> _appTokenProviderCallback;

		internal string RedirectUrl { get; }

		internal string RegionalAuthority { get; } = EnvironmentVariables.AzureRegionalAuthorityName;

		protected MsalConfidentialClient()
		{
		}

		public MsalConfidentialClient(CredentialPipeline pipeline, string tenantId, string clientId, string clientSecret, string redirectUrl, TokenCredentialOptions options)
			: base(pipeline, tenantId, clientId, options)
		{
			_clientSecret = clientSecret;
			RedirectUrl = redirectUrl;
		}

		public MsalConfidentialClient(CredentialPipeline pipeline, string tenantId, string clientId, IX509Certificate2Provider certificateProvider, bool includeX5CClaimHeader, TokenCredentialOptions options)
			: base(pipeline, tenantId, clientId, options)
		{
			_includeX5CClaimHeader = includeX5CClaimHeader;
			_certificateProvider = certificateProvider;
		}

		public MsalConfidentialClient(CredentialPipeline pipeline, string tenantId, string clientId, Func<string> assertionCallback, TokenCredentialOptions options)
			: base(pipeline, tenantId, clientId, options)
		{
			_clientAssertionCallback = assertionCallback;
		}

		public MsalConfidentialClient(CredentialPipeline pipeline, string tenantId, string clientId, Func<CancellationToken, Task<string>> assertionCallback, TokenCredentialOptions options)
			: base(pipeline, tenantId, clientId, options)
		{
			_clientAssertionCallbackAsync = assertionCallback;
		}

		public MsalConfidentialClient(CredentialPipeline pipeline, string tenantId, string clientId, Func<AppTokenProviderParameters, Task<AppTokenProviderResult>> appTokenProviderCallback, TokenCredentialOptions options)
			: base(pipeline, tenantId, clientId, options)
		{
			_appTokenProviderCallback = appTokenProviderCallback;
		}

		protected override ValueTask<IConfidentialClientApplication> CreateClientAsync(bool enableCae, bool async, CancellationToken cancellationToken)
		{
			return CreateClientCoreAsync(enableCae, async, cancellationToken);
		}

		protected virtual async ValueTask<IConfidentialClientApplication> CreateClientCoreAsync(bool enableCae, bool async, CancellationToken cancellationToken)
		{
			string[] array = (enableCae ? cp1Capabilities : Array.Empty<string>());
			ConfidentialClientApplicationBuilder confidentialClientApplicationBuilder = ConfidentialClientApplicationBuilder.Create(base.ClientId).WithHttpClientFactory(new HttpPipelineClientFactory(base.Pipeline.HttpPipeline));
			LogCallback loggingCallback = base.LogMsal;
			bool? enablePiiLogging = base.IsSupportLoggingEnabled;
			ConfidentialClientApplicationBuilder confClientBuilder = confidentialClientApplicationBuilder.WithLogging(loggingCallback, null, enablePiiLogging);
			if (_appTokenProviderCallback != null)
			{
				confClientBuilder.WithAppTokenProvider(_appTokenProviderCallback).WithAuthority(base.AuthorityHost.AbsoluteUri, base.TenantId, validateAuthority: false).WithInstanceDiscovery(enableInstanceDiscovery: false);
			}
			else
			{
				confClientBuilder.WithAuthority(base.AuthorityHost.AbsoluteUri, base.TenantId);
				if (base.DisableInstanceDiscovery)
				{
					confClientBuilder.WithInstanceDiscovery(enableInstanceDiscovery: false);
				}
			}
			if (array.Length != 0)
			{
				confClientBuilder.WithClientCapabilities(array);
			}
			if (_clientSecret != null)
			{
				confClientBuilder.WithClientSecret(_clientSecret);
			}
			if (_clientAssertionCallback != null)
			{
				if (_clientAssertionCallbackAsync != null)
				{
					throw new InvalidOperationException("Cannot set both _clientAssertionCallback and _clientAssertionCallbackAsync");
				}
				confClientBuilder.WithClientAssertion(_clientAssertionCallback);
			}
			if (_clientAssertionCallbackAsync != null)
			{
				if (_clientAssertionCallback != null)
				{
					throw new InvalidOperationException("Cannot set both _clientAssertionCallback and _clientAssertionCallbackAsync");
				}
				confClientBuilder.WithClientAssertion(_clientAssertionCallbackAsync);
			}
			if (_certificateProvider != null)
			{
				confClientBuilder.WithCertificate(await _certificateProvider.GetCertificateAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
			}
			if (_appTokenProviderCallback == null && !string.IsNullOrEmpty(RegionalAuthority))
			{
				confClientBuilder.WithAzureRegion(RegionalAuthority);
			}
			if (!string.IsNullOrEmpty(RedirectUrl))
			{
				confClientBuilder.WithRedirectUri(RedirectUrl);
			}
			return confClientBuilder.Build();
		}

		public virtual async ValueTask<AuthenticationResult> AcquireTokenForClientAsync(string[] scopes, string tenantId, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult result = await AcquireTokenForClientCoreAsync(scopes, tenantId, claims, enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			LogAccountDetails(result);
			return result;
		}

		public virtual async ValueTask<AuthenticationResult> AcquireTokenForClientCoreAsync(string[] scopes, string tenantId, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenForClientParameterBuilder acquireTokenForClientParameterBuilder = (await GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).AcquireTokenForClient(scopes).WithSendX5C(_includeX5CClaimHeader);
			if (!string.IsNullOrEmpty(tenantId))
			{
				UriBuilder uriBuilder = new UriBuilder(base.AuthorityHost)
				{
					Path = tenantId
				};
				acquireTokenForClientParameterBuilder.WithTenantIdFromAuthority(uriBuilder.Uri);
			}
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenForClientParameterBuilder.WithClaims(claims);
			}
			return await acquireTokenForClientParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public virtual async ValueTask<AuthenticationResult> AcquireTokenSilentAsync(string[] scopes, AuthenticationAccount account, string tenantId, string redirectUri, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult result = await AcquireTokenSilentCoreAsync(scopes, account, tenantId, redirectUri, claims, enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			LogAccountDetails(result);
			return result;
		}

		public virtual async ValueTask<AuthenticationResult> AcquireTokenSilentCoreAsync(string[] scopes, AuthenticationAccount account, string tenantId, string redirectUri, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenSilentParameterBuilder acquireTokenSilentParameterBuilder = (await GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).AcquireTokenSilent(scopes, account);
			if (!string.IsNullOrEmpty(tenantId))
			{
				UriBuilder uriBuilder = new UriBuilder(base.AuthorityHost)
				{
					Path = tenantId
				};
				acquireTokenSilentParameterBuilder.WithTenantIdFromAuthority(uriBuilder.Uri);
			}
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenSilentParameterBuilder.WithClaims(claims);
			}
			return await acquireTokenSilentParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public virtual async ValueTask<AuthenticationResult> AcquireTokenByAuthorizationCodeAsync(string[] scopes, string code, string tenantId, string redirectUri, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult result = await AcquireTokenByAuthorizationCodeCoreAsync(scopes, code, tenantId, redirectUri, claims, enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			LogAccountDetails(result);
			return result;
		}

		public virtual async ValueTask<AuthenticationResult> AcquireTokenByAuthorizationCodeCoreAsync(string[] scopes, string code, string tenantId, string redirectUri, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenByAuthorizationCodeParameterBuilder acquireTokenByAuthorizationCodeParameterBuilder = (await GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).AcquireTokenByAuthorizationCode(scopes, code);
			if (!string.IsNullOrEmpty(tenantId))
			{
				UriBuilder uriBuilder = new UriBuilder(base.AuthorityHost)
				{
					Path = tenantId
				};
				acquireTokenByAuthorizationCodeParameterBuilder.WithTenantIdFromAuthority(uriBuilder.Uri);
			}
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenByAuthorizationCodeParameterBuilder.WithClaims(claims);
			}
			return await acquireTokenByAuthorizationCodeParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public virtual async ValueTask<AuthenticationResult> AcquireTokenOnBehalfOfAsync(string[] scopes, string tenantId, UserAssertion userAssertionValue, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult result = await AcquireTokenOnBehalfOfCoreAsync(scopes, tenantId, userAssertionValue, claims, enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			LogAccountDetails(result);
			return result;
		}

		public virtual async ValueTask<AuthenticationResult> AcquireTokenOnBehalfOfCoreAsync(string[] scopes, string tenantId, UserAssertion userAssertionValue, string claims, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenOnBehalfOfParameterBuilder acquireTokenOnBehalfOfParameterBuilder = (await GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).AcquireTokenOnBehalfOf(scopes, userAssertionValue).WithSendX5C(_includeX5CClaimHeader);
			if (!string.IsNullOrEmpty(tenantId))
			{
				UriBuilder uriBuilder = new UriBuilder(base.AuthorityHost)
				{
					Path = tenantId
				};
				acquireTokenOnBehalfOfParameterBuilder.WithTenantIdFromAuthority(uriBuilder.Uri);
			}
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenOnBehalfOfParameterBuilder.WithClaims(claims);
			}
			return await acquireTokenOnBehalfOfParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}
	}
	internal class MsalPublicClient : MsalClientBase<IPublicClientApplication>
	{
		private Action<PublicClientApplicationBuilder> _beforeBuildClient;

		internal string RedirectUrl { get; }

		protected MsalPublicClient()
		{
		}

		public MsalPublicClient(CredentialPipeline pipeline, string tenantId, string clientId, string redirectUrl, TokenCredentialOptions options)
			: base(pipeline, tenantId, clientId, options)
		{
			RedirectUrl = redirectUrl;
			if (options is IMsalPublicClientInitializerOptions msalPublicClientInitializerOptions)
			{
				_beforeBuildClient = msalPublicClientInitializerOptions.BeforeBuildClient;
			}
		}

		protected override ValueTask<IPublicClientApplication> CreateClientAsync(bool enableCae, bool async, CancellationToken cancellationToken)
		{
			return CreateClientCoreAsync(enableCae, async, cancellationToken);
		}

		protected virtual ValueTask<IPublicClientApplication> CreateClientCoreAsync(bool enableCae, bool async, CancellationToken cancellationToken)
		{
			string[] array = (enableCae ? cp1Capabilities : Array.Empty<string>());
			Uri uri = new UriBuilder(base.AuthorityHost.Scheme, base.AuthorityHost.Host, base.AuthorityHost.Port, base.TenantId ?? "organizations").Uri;
			PublicClientApplicationBuilder publicClientApplicationBuilder = PublicClientApplicationBuilder.Create(base.ClientId).WithAuthority(uri).WithHttpClientFactory(new HttpPipelineClientFactory(base.Pipeline.HttpPipeline));
			LogCallback loggingCallback = base.LogMsal;
			bool? enablePiiLogging = base.IsSupportLoggingEnabled;
			PublicClientApplicationBuilder publicClientApplicationBuilder2 = publicClientApplicationBuilder.WithLogging(loggingCallback, null, enablePiiLogging);
			if (!string.IsNullOrEmpty(RedirectUrl))
			{
				publicClientApplicationBuilder2 = publicClientApplicationBuilder2.WithRedirectUri(RedirectUrl);
			}
			if (array.Length != 0)
			{
				publicClientApplicationBuilder2.WithClientCapabilities(array);
			}
			if (_beforeBuildClient != null)
			{
				_beforeBuildClient(publicClientApplicationBuilder2);
			}
			if (base.DisableInstanceDiscovery)
			{
				publicClientApplicationBuilder2.WithInstanceDiscovery(enableInstanceDiscovery: false);
			}
			return new ValueTask<IPublicClientApplication>(publicClientApplicationBuilder2.Build());
		}

		public async ValueTask<List<IAccount>> GetAccountsAsync(bool async, bool enableCae, CancellationToken cancellationToken)
		{
			return await GetAccountsCoreAsync(async, enableCae, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		protected virtual async ValueTask<List<IAccount>> GetAccountsCoreAsync(bool async, bool enableCae, CancellationToken cancellationToken)
		{
			return await GetAccountsAsync(await GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false), async).ConfigureAwait(continueOnCapturedContext: false);
		}

		public async ValueTask<AuthenticationResult> AcquireTokenSilentAsync(string[] scopes, string claims, IAccount account, string tenantId, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult result = await AcquireTokenSilentCoreAsync(scopes, claims, account, tenantId, enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			LogAccountDetails(result);
			return result;
		}

		protected virtual async ValueTask<AuthenticationResult> AcquireTokenSilentCoreAsync(string[] scopes, string claims, IAccount account, string tenantId, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenSilentParameterBuilder acquireTokenSilentParameterBuilder = (await GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).AcquireTokenSilent(scopes, account);
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenSilentParameterBuilder.WithClaims(claims);
			}
			if (tenantId != null)
			{
				UriBuilder uriBuilder = new UriBuilder(base.AuthorityHost)
				{
					Path = (base.TenantId ?? tenantId)
				};
				acquireTokenSilentParameterBuilder.WithTenantIdFromAuthority(uriBuilder.Uri);
			}
			return await acquireTokenSilentParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public async ValueTask<AuthenticationResult> AcquireTokenSilentAsync(string[] scopes, string claims, AuthenticationRecord record, string tenantId, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult result = await AcquireTokenSilentCoreAsync(scopes, claims, record, tenantId, enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			LogAccountDetails(result);
			return result;
		}

		protected virtual async ValueTask<AuthenticationResult> AcquireTokenSilentCoreAsync(string[] scopes, string claims, AuthenticationRecord record, string tenantId, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenSilentParameterBuilder acquireTokenSilentParameterBuilder = (await GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).AcquireTokenSilent(scopes, (AuthenticationAccount)record);
			if (tenantId != null || record.TenantId != null)
			{
				UriBuilder uriBuilder = new UriBuilder(base.AuthorityHost)
				{
					Path = (tenantId ?? record.TenantId)
				};
				acquireTokenSilentParameterBuilder.WithTenantIdFromAuthority(uriBuilder.Uri);
			}
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenSilentParameterBuilder.WithClaims(claims);
			}
			return await acquireTokenSilentParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public async ValueTask<AuthenticationResult> AcquireTokenInteractiveAsync(string[] scopes, string claims, Prompt prompt, string loginHint, string tenantId, bool enableCae, BrowserCustomizationOptions browserOptions, bool async, CancellationToken cancellationToken)
		{
			if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA && !IdentityCompatSwitches.DisableInteractiveBrowserThreadpoolExecution)
			{
				AzureIdentityEventSource.Singleton.InteractiveAuthenticationExecutingOnThreadPool();
				return Task.Run(async delegate
				{
					AuthenticationResult result2 = await AcquireTokenInteractiveCoreAsync(scopes, claims, prompt, loginHint, tenantId, enableCae, browserOptions, async: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					LogAccountDetails(result2);
					return result2;
				}).GetAwaiter().GetResult();
			}
			AzureIdentityEventSource.Singleton.InteractiveAuthenticationExecutingInline();
			AuthenticationResult result = await AcquireTokenInteractiveCoreAsync(scopes, claims, prompt, loginHint, tenantId, enableCae, browserOptions, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			LogAccountDetails(result);
			return result;
		}

		protected virtual async ValueTask<AuthenticationResult> AcquireTokenInteractiveCoreAsync(string[] scopes, string claims, Prompt prompt, string loginHint, string tenantId, bool enableCae, BrowserCustomizationOptions browserOptions, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenInteractiveParameterBuilder acquireTokenInteractiveParameterBuilder = (await GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).AcquireTokenInteractive(scopes).WithPrompt(prompt);
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenInteractiveParameterBuilder.WithClaims(claims);
			}
			if (loginHint != null)
			{
				acquireTokenInteractiveParameterBuilder.WithLoginHint(loginHint);
			}
			if (tenantId != null)
			{
				UriBuilder uriBuilder = new UriBuilder(base.AuthorityHost)
				{
					Path = tenantId
				};
				acquireTokenInteractiveParameterBuilder.WithTenantIdFromAuthority(uriBuilder.Uri);
			}
			if (browserOptions != null)
			{
				if (browserOptions.UseEmbeddedWebView.HasValue)
				{
					acquireTokenInteractiveParameterBuilder.WithUseEmbeddedWebView(browserOptions.UseEmbeddedWebView.Value);
				}
				if (browserOptions.SystemBrowserOptions != null)
				{
					acquireTokenInteractiveParameterBuilder.WithSystemWebViewOptions(browserOptions.SystemBrowserOptions);
				}
			}
			return await acquireTokenInteractiveParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public async ValueTask<AuthenticationResult> AcquireTokenByUsernamePasswordAsync(string[] scopes, string claims, string username, string password, string tenantId, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult result = await AcquireTokenByUsernamePasswordCoreAsync(scopes, claims, username, password, tenantId, enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			LogAccountDetails(result);
			return result;
		}

		protected virtual async ValueTask<AuthenticationResult> AcquireTokenByUsernamePasswordCoreAsync(string[] scopes, string claims, string username, string password, string tenantId, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenByUsernamePasswordParameterBuilder acquireTokenByUsernamePasswordParameterBuilder = (await GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).AcquireTokenByUsernamePassword(scopes, username, password);
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenByUsernamePasswordParameterBuilder.WithClaims(claims);
			}
			if (!string.IsNullOrEmpty(tenantId))
			{
				UriBuilder uriBuilder = new UriBuilder(base.AuthorityHost)
				{
					Path = tenantId
				};
				acquireTokenByUsernamePasswordParameterBuilder.WithTenantIdFromAuthority(uriBuilder.Uri);
			}
			return await acquireTokenByUsernamePasswordParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public async ValueTask<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(string[] scopes, string claims, Func<DeviceCodeResult, Task> deviceCodeCallback, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult result = await AcquireTokenWithDeviceCodeCoreAsync(scopes, claims, deviceCodeCallback, enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			LogAccountDetails(result);
			return result;
		}

		protected virtual async ValueTask<AuthenticationResult> AcquireTokenWithDeviceCodeCoreAsync(string[] scopes, string claims, Func<DeviceCodeResult, Task> deviceCodeCallback, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenWithDeviceCodeParameterBuilder acquireTokenWithDeviceCodeParameterBuilder = (await GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).AcquireTokenWithDeviceCode(scopes, deviceCodeCallback);
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenWithDeviceCodeParameterBuilder.WithClaims(claims);
			}
			return await acquireTokenWithDeviceCodeParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public async ValueTask<AuthenticationResult> AcquireTokenByRefreshTokenAsync(string[] scopes, string claims, string refreshToken, AzureCloudInstance azureCloudInstance, string tenant, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AuthenticationResult result = await AcquireTokenByRefreshTokenCoreAsync(scopes, claims, refreshToken, azureCloudInstance, tenant, enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			LogAccountDetails(result);
			return result;
		}

		protected virtual async ValueTask<AuthenticationResult> AcquireTokenByRefreshTokenCoreAsync(string[] scopes, string claims, string refreshToken, AzureCloudInstance azureCloudInstance, string tenant, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			AcquireTokenByRefreshTokenParameterBuilder acquireTokenByRefreshTokenParameterBuilder = ((IByRefreshToken)(await GetClientAsync(enableCae, async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))).AcquireTokenByRefreshToken(scopes, refreshToken);
			if (!string.IsNullOrEmpty(claims))
			{
				acquireTokenByRefreshTokenParameterBuilder.WithClaims(claims);
			}
			if (!string.IsNullOrEmpty(base.TenantId))
			{
				UriBuilder uriBuilder = new UriBuilder(base.AuthorityHost)
				{
					Path = tenant
				};
				acquireTokenByRefreshTokenParameterBuilder.WithTenantIdFromAuthority(uriBuilder.Uri);
			}
			return await acquireTokenByRefreshTokenParameterBuilder.ExecuteAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private static async ValueTask<List<IAccount>> GetAccountsAsync(IPublicClientApplication client, bool async)
		{
			IEnumerable<IAccount> source = ((!async) ? client.GetAccountsAsync().GetAwaiter().GetResult() : (await client.GetAccountsAsync().ConfigureAwait(continueOnCapturedContext: false)));
			return source.ToList();
		}
	}
	internal class DefaultAzureCredentialImdsRetryPolicy : RetryPolicy
	{
		public DefaultAzureCredentialImdsRetryPolicy(RetryOptions retryOptions, DelayStrategy delayStrategy = null)
			: base(retryOptions.MaxRetries, delayStrategy ?? DelayStrategy.CreateExponentialDelayStrategy(retryOptions.Delay, retryOptions.MaxDelay))
		{
		}

		protected override bool ShouldRetry(HttpMessage message, Exception exception)
		{
			if (ImdsManagedIdentitySource.IsProbRequest(message))
			{
				return false;
			}
			return base.ShouldRetry(message, exception);
		}

		protected override ValueTask<bool> ShouldRetryAsync(HttpMessage message, Exception exception)
		{
			if (ImdsManagedIdentitySource.IsProbRequest(message))
			{
				return default(ValueTask<bool>);
			}
			return base.ShouldRetryAsync(message, exception);
		}

		public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			base.Process(message, pipeline);
		}

		public override ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			return base.ProcessAsync(message, pipeline);
		}
	}
	internal sealed class ProcessRunner : IDisposable
	{
		private readonly IProcess _process;

		private readonly TimeSpan _timeout;

		private readonly TaskCompletionSource<string> _tcs;

		private readonly TaskCompletionSource<ICollection<string>> _outputTcs;

		private readonly TaskCompletionSource<ICollection<string>> _errorTcs;

		private readonly ICollection<string> _outputData;

		private readonly ICollection<string> _errorData;

		private readonly CancellationToken _cancellationToken;

		private readonly CancellationTokenSource _timeoutCts;

		private CancellationTokenRegistration _ctRegistration;

		private bool _logPII;

		public int ExitCode => _process.ExitCode;

		public ProcessRunner(IProcess process, TimeSpan timeout, bool logPII, CancellationToken cancellationToken)
		{
			_logPII = logPII;
			_process = process;
			_timeout = timeout;
			if (_logPII)
			{
				AzureIdentityEventSource.Singleton.ProcessRunnerInformational("Running process `" + process.StartInfo.FileName + "' with arguments " + string.Join(", ", process.StartInfo.Arguments));
			}
			_outputData = new List<string>();
			_errorData = new List<string>();
			_outputTcs = new TaskCompletionSource<ICollection<string>>(TaskCreationOptions.RunContinuationsAsynchronously);
			_errorTcs = new TaskCompletionSource<ICollection<string>>(TaskCreationOptions.RunContinuationsAsynchronously);
			_tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
			if (timeout.TotalMilliseconds >= 0.0)
			{
				_timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
				_cancellationToken = _timeoutCts.Token;
			}
			else
			{
				_cancellationToken = cancellationToken;
			}
		}

		public Task<string> RunAsync()
		{
			StartProcess();
			return _tcs.Task;
		}

		public string Run()
		{
			StartProcess();
			return _tcs.Task.GetAwaiter().GetResult();
		}

		private void StartProcess()
		{
			if (!TrySetCanceled() && !_tcs.Task.IsCompleted)
			{
				_process.StartInfo.UseShellExecute = false;
				_process.StartInfo.RedirectStandardOutput = true;
				_process.StartInfo.RedirectStandardError = true;
				_process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs args)
				{
					OnDataReceived(args, _outputData, _outputTcs);
				};
				_process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs args)
				{
					OnDataReceived(args, _errorData, _errorTcs);
				};
				_process.Exited += delegate
				{
					HandleExitAsync();
				};
				_timeoutCts?.CancelAfter(_timeout);
				if (!_process.Start())
				{
					TrySetException(new InvalidOperationException("Failed to start process '" + _process.StartInfo.FileName + "'"));
				}
				_process.BeginOutputReadLine();
				_process.BeginErrorReadLine();
				CancellationToken cancellationToken = _cancellationToken;
				_ctRegistration = cancellationToken.Register(HandleCancel, useSynchronizationContext: false);
			}
		}

		private async ValueTask HandleExitAsync()
		{
			if (_process.ExitCode == 0)
			{
				ICollection<string> values = await _outputTcs.Task.ConfigureAwait(continueOnCapturedContext: false);
				TrySetResult(string.Join(Environment.NewLine, values));
			}
			else
			{
				ICollection<string> values2 = await _errorTcs.Task.ConfigureAwait(continueOnCapturedContext: false);
				TrySetException(new InvalidOperationException(string.Join(Environment.NewLine, values2)));
			}
		}

		private void HandleCancel()
		{
			if (_tcs.Task.IsCompleted)
			{
				return;
			}
			if (!_process.HasExited)
			{
				try
				{
					_process.Kill();
				}
				catch (Exception exception)
				{
					TrySetException(exception);
					return;
				}
			}
			TrySetCanceled();
		}

		private static void OnDataReceived(DataReceivedEventArgs args, ICollection<string> data, TaskCompletionSource<ICollection<string>> tcs)
		{
			if (args.Data != null)
			{
				data.Add(args.Data);
			}
			else
			{
				tcs.SetResult(data);
			}
		}

		private void TrySetResult(string result)
		{
			_tcs.TrySetResult(result);
		}

		private bool TrySetCanceled()
		{
			CancellationToken cancellationToken = _cancellationToken;
			if (cancellationToken.IsCancellationRequested)
			{
				_tcs.TrySetCanceled(_cancellationToken);
			}
			cancellationToken = _cancellationToken;
			return cancellationToken.IsCancellationRequested;
		}

		private void TrySetException(Exception exception)
		{
			if (_logPII)
			{
				AzureIdentityEventSource.Singleton.ProcessRunnerError(exception.ToString());
			}
			_tcs.TrySetException(exception);
		}

		public void Dispose()
		{
			_tcs.TrySetCanceled();
			_process.Dispose();
			_ctRegistration.Dispose();
			_timeoutCts?.Dispose();
		}
	}
	internal class ProcessService : IProcessService
	{
		private class ProcessWrapper : IProcess, IDisposable
		{
			private readonly Process _process;

			public bool HasExited => _process.HasExited;

			public int ExitCode => _process.ExitCode;

			public ProcessStartInfo StartInfo
			{
				get
				{
					return _process.StartInfo;
				}
				set
				{
					_process.StartInfo = value;
				}
			}

			public event EventHandler Exited
			{
				add
				{
					_process.Exited += value;
				}
				remove
				{
					_process.Exited -= value;
				}
			}

			public event DataReceivedEventHandler OutputDataReceived
			{
				add
				{
					_process.OutputDataReceived += value;
				}
				remove
				{
					_process.OutputDataReceived -= value;
				}
			}

			public event DataReceivedEventHandler ErrorDataReceived
			{
				add
				{
					_process.ErrorDataReceived += value;
				}
				remove
				{
					_process.ErrorDataReceived -= value;
				}
			}

			public ProcessWrapper(ProcessStartInfo processStartInfo)
			{
				_process = new Process
				{
					StartInfo = processStartInfo,
					EnableRaisingEvents = true
				};
			}

			public bool Start()
			{
				return _process.Start();
			}

			public void Kill()
			{
				_process.Kill();
			}

			public void BeginOutputReadLine()
			{
				_process.BeginOutputReadLine();
			}

			public void BeginErrorReadLine()
			{
				_process.BeginErrorReadLine();
			}

			public void Dispose()
			{
				_process.Dispose();
			}
		}

		public static IProcessService Default { get; } = new ProcessService();

		private ProcessService()
		{
		}

		public IProcess Create(ProcessStartInfo startInfo)
		{
			return new ProcessWrapper(startInfo);
		}
	}
	internal sealed class ScopeGroupHandler : IScopeHandler
	{
		private class ChildScopeInfo
		{
			public Azure.Core.Pipeline.ClientDiagnostics Diagnostics { get; }

			public string Name { get; }

			public DateTime StartDateTime { get; set; }

			public Exception Exception { get; set; }

			public ChildScopeInfo(Azure.Core.Pipeline.ClientDiagnostics diagnostics, string name)
			{
				Diagnostics = diagnostics;
				Name = name;
			}
		}

		private static readonly AsyncLocal<IScopeHandler> _currentAsyncLocal = new AsyncLocal<IScopeHandler>();

		private readonly string _groupName;

		private Dictionary<string, ChildScopeInfo> _childScopes;

		public static IScopeHandler Current => _currentAsyncLocal.Value;

		public ScopeGroupHandler(string groupName)
		{
			_groupName = groupName;
			_currentAsyncLocal.Value = this;
		}

		public Azure.Core.Pipeline.DiagnosticScope CreateScope(Azure.Core.Pipeline.ClientDiagnostics diagnostics, string name)
		{
			if (IsGroup(name))
			{
				return diagnostics.CreateScope(name);
			}
			if (_childScopes == null)
			{
				_childScopes = new Dictionary<string, ChildScopeInfo>();
			}
			_childScopes[name] = new ChildScopeInfo(diagnostics, name);
			return default(Azure.Core.Pipeline.DiagnosticScope);
		}

		public void Start(string name, in Azure.Core.Pipeline.DiagnosticScope scope)
		{
			if (IsGroup(name))
			{
				scope.Start();
			}
			else
			{
				_childScopes[name].StartDateTime = DateTime.UtcNow;
			}
		}

		public void Dispose(string name, in Azure.Core.Pipeline.DiagnosticScope scope)
		{
			if (IsGroup(name))
			{
				ChildScopeInfo childScopeInfo = _childScopes?.Values.LastOrDefault((ChildScopeInfo i) => i.Exception == null);
				if (childScopeInfo != null)
				{
					SucceedChildScope(childScopeInfo);
				}
				scope.Dispose();
				_currentAsyncLocal.Value = null;
			}
		}

		public void Fail(string name, in Azure.Core.Pipeline.DiagnosticScope scope, Exception exception)
		{
			if (_childScopes == null)
			{
				scope.Failed(exception);
			}
			else if (IsGroup(name))
			{
				if (exception is OperationCanceledException)
				{
					FailChildScope(_childScopes.Values.Last((ChildScopeInfo i) => i.Exception == exception));
				}
				else
				{
					foreach (ChildScopeInfo value in _childScopes.Values)
					{
						FailChildScope(value);
					}
				}
				scope.Failed(exception);
			}
			else
			{
				_childScopes[name].Exception = exception;
			}
		}

		private static void SucceedChildScope(ChildScopeInfo scopeInfo)
		{
			using Azure.Core.Pipeline.DiagnosticScope diagnosticScope = scopeInfo.Diagnostics.CreateScope(scopeInfo.Name);
			diagnosticScope.SetStartTime(scopeInfo.StartDateTime);
			diagnosticScope.Start();
		}

		private static void FailChildScope(ChildScopeInfo scopeInfo)
		{
			using Azure.Core.Pipeline.DiagnosticScope diagnosticScope = scopeInfo.Diagnostics.CreateScope(scopeInfo.Name);
			diagnosticScope.SetStartTime(scopeInfo.StartDateTime);
			diagnosticScope.Start();
			diagnosticScope.Failed(scopeInfo.Exception);
		}

		private bool IsGroup(string name)
		{
			return string.Equals(name, _groupName, StringComparison.Ordinal);
		}

		void IScopeHandler.Start(string name, in Azure.Core.Pipeline.DiagnosticScope scope)
		{
			Start(name, in scope);
		}

		void IScopeHandler.Dispose(string name, in Azure.Core.Pipeline.DiagnosticScope scope)
		{
			Dispose(name, in scope);
		}

		void IScopeHandler.Fail(string name, in Azure.Core.Pipeline.DiagnosticScope scope, Exception exception)
		{
			Fail(name, in scope, exception);
		}
	}
	internal static class ScopeUtilities
	{
		private const string DefaultSuffix = "/.default";

		private const string ScopePattern = "^[0-9a-zA-Z-_.:/]+$";

		internal const string InvalidScopeMessage = "The specified scope is not in expected format. Only alphanumeric characters, '.', '-', ':', '_', and '/' are allowed";

		private static readonly Regex scopeRegex = new Regex("^[0-9a-zA-Z-_.:/]+$");

		public static string ScopesToResource(string[] scopes)
		{
			if (scopes == null)
			{
				throw new ArgumentNullException("scopes");
			}
			if (scopes.Length != 1)
			{
				throw new ArgumentException("To convert to a resource string the specified array must be exactly length 1", "scopes");
			}
			if (!scopes[0].EndsWith("/.default", StringComparison.Ordinal))
			{
				return scopes[0];
			}
			return scopes[0].Remove(scopes[0].LastIndexOf("/.default", StringComparison.Ordinal));
		}

		public static string[] ResourceToScopes(string resource)
		{
			return new string[1] { resource + "/.default" };
		}

		public static void ValidateScope(string scope)
		{
			if (!scopeRegex.IsMatch(scope))
			{
				throw new ArgumentException("The specified scope is not in expected format. Only alphanumeric characters, '.', '-', ':', '_', and '/' are allowed", "scope");
			}
		}
	}
	internal class SentinelFileLock : IDisposable
	{
		private FileStream _lockFileStream;

		private const int DefaultFileBufferSize = 4096;

		private SentinelFileLock(FileStream lockFileStream)
		{
			_lockFileStream = lockFileStream;
		}

		public static async Task<SentinelFileLock> AcquireAsync(string lockfilePath, int lockFileRetryCount, TimeSpan lockFileRetryDelay)
		{
			Exception exception = null;
			FileStream fileStream = null;
			Directory.CreateDirectory(Path.GetDirectoryName(lockfilePath));
			for (int tryCount = 0; tryCount < lockFileRetryCount; tryCount++)
			{
				try
				{
					fileStream = new FileStream(lockfilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read, 4096, FileOptions.DeleteOnClose);
					using StreamWriter writer = new StreamWriter(fileStream, Encoding.UTF8, 4096, leaveOpen: true);
					await writer.WriteLineAsync($"{Process.GetCurrentProcess().Id} {Process.GetCurrentProcess().ProcessName}").ConfigureAwait(continueOnCapturedContext: false);
				}
				catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
				{
					exception = ex;
					await Task.Delay(lockFileRetryDelay).ConfigureAwait(continueOnCapturedContext: false);
					continue;
				}
				break;
			}
			if (fileStream == null)
			{
				throw new InvalidOperationException("Could not get access to the shared lock file.", exception);
			}
			return new SentinelFileLock(fileStream);
		}

		public void Dispose()
		{
			_lockFileStream?.Dispose();
			_lockFileStream = null;
		}
	}
	internal class ServiceFabricManagedIdentitySource : ManagedIdentitySource
	{
		private const string ServiceFabricMsiApiVersion = "2019-07-01-preview";

		private const string IdentityEndpointInvalidUriError = "The environment variable IDENTITY_ENDPOINT contains an invalid Uri.";

		private readonly Uri _endpoint;

		private readonly string _identityHeaderValue;

		private readonly string _clientId;

		private readonly string _resourceId;

		public static ManagedIdentitySource TryCreate(ManagedIdentityClientOptions options)
		{
			string identityEndpoint = EnvironmentVariables.IdentityEndpoint;
			string identityHeader = EnvironmentVariables.IdentityHeader;
			string identityServerThumbprint = EnvironmentVariables.IdentityServerThumbprint;
			if (string.IsNullOrEmpty(identityEndpoint) || string.IsNullOrEmpty(identityHeader) || string.IsNullOrEmpty(identityServerThumbprint))
			{
				return null;
			}
			if (!Uri.TryCreate(identityEndpoint, UriKind.Absolute, out var result))
			{
				throw new AuthenticationFailedException("The environment variable IDENTITY_ENDPOINT contains an invalid Uri.");
			}
			CredentialPipeline credentialPipeline = options.Pipeline;
			if (!options.PreserveTransport)
			{
				credentialPipeline = new CredentialPipeline(HttpPipelineBuilder.Build(new TokenCredentialOptions
				{
					Transport = GetServiceFabricMITransport()
				}), credentialPipeline.Diagnostics);
			}
			return new ServiceFabricManagedIdentitySource(credentialPipeline, result, identityHeader, options);
		}

		internal static HttpClientTransport GetServiceFabricMITransport()
		{
			return new HttpClientTransport(new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = ValidateMsiServerCertificate
			});
		}

		internal ServiceFabricManagedIdentitySource(CredentialPipeline pipeline, Uri endpoint, string identityHeaderValue, ManagedIdentityClientOptions options)
			: base(pipeline)
		{
			_endpoint = endpoint;
			_identityHeaderValue = identityHeaderValue;
			_clientId = options.ClientId;
			_resourceId = options.ResourceIdentifier?.ToString();
			if (!string.IsNullOrEmpty(options.ClientId) || null != options.ResourceIdentifier)
			{
				AzureIdentityEventSource.Singleton.ServiceFabricManagedIdentityRuntimeConfigurationNotSupported();
			}
		}

		protected override Request CreateRequest(string[] scopes)
		{
			string value = ScopeUtilities.ScopesToResource(scopes);
			Request request = base.Pipeline.HttpPipeline.CreateRequest();
			request.Method = RequestMethod.Get;
			request.Headers.Add("secret", _identityHeaderValue);
			request.Uri.Reset(_endpoint);
			request.Uri.AppendQuery("api-version", "2019-07-01-preview");
			request.Uri.AppendQuery("resource", value);
			if (!string.IsNullOrEmpty(_clientId))
			{
				request.Uri.AppendQuery("client_id", _clientId);
			}
			if (!string.IsNullOrEmpty(_resourceId))
			{
				request.Uri.AppendQuery("mi_res_id", _resourceId);
			}
			return request;
		}

		private static bool ValidateMsiServerCertificate(HttpRequestMessage message, X509Certificate2 cert, X509Chain certChain, SslPolicyErrors policyErrors)
		{
			if (policyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			return string.Compare(cert.GetCertHashString(), EnvironmentVariables.IdentityServerThumbprint, StringComparison.OrdinalIgnoreCase) == 0;
		}
	}
	internal static class StringExtensions
	{
		public static SecureString ToSecureString(this string plainString)
		{
			if (plainString == null)
			{
				return null;
			}
			SecureString secureString = new SecureString();
			char[] array = plainString.ToCharArray();
			foreach (char c in array)
			{
				secureString.AppendChar(c);
			}
			return secureString;
		}
	}
	internal class TenantIdResolver : TenantIdResolverBase
	{
		public override string Resolve(string explicitTenantId, TokenRequestContext context, string[] additionallyAllowedTenantIds)
		{
			bool disableTenantDiscovery = IdentityCompatSwitches.DisableTenantDiscovery;
			if (context.TenantId != explicitTenantId && context.TenantId != null && explicitTenantId != null)
			{
				if (disableTenantDiscovery || explicitTenantId == "adfs")
				{
					AzureIdentityEventSource.Singleton.TenantIdDiscoveredAndNotUsed(explicitTenantId, context.TenantId);
				}
				else
				{
					AzureIdentityEventSource.Singleton.TenantIdDiscoveredAndUsed(explicitTenantId, context.TenantId);
				}
			}
			string text = (disableTenantDiscovery ? explicitTenantId : ((!(explicitTenantId == "adfs")) ? (context.TenantId ?? explicitTenantId) : explicitTenantId));
			string text2 = text;
			if (explicitTenantId != null && text2 != explicitTenantId && additionallyAllowedTenantIds != TenantIdResolverBase.AllTenants && Array.BinarySearch(additionallyAllowedTenantIds, text2, StringComparer.OrdinalIgnoreCase) < 0)
			{
				throw new AuthenticationFailedException("The current credential is not configured to acquire tokens for tenant " + text2 + ". To enable acquiring tokens for this tenant add it to the AdditionallyAllowedTenants on the credential options, or add \"*\" to AdditionallyAllowedTenants to allow acquiring tokens for any tenant. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/multitenant/troubleshoot");
			}
			return text2;
		}

		public override string[] ResolveAddionallyAllowedTenantIds(IList<string> additionallyAllowedTenants)
		{
			if (additionallyAllowedTenants == null || additionallyAllowedTenants.Count == 0)
			{
				return Array.Empty<string>();
			}
			if (additionallyAllowedTenants.Contains("*"))
			{
				return TenantIdResolverBase.AllTenants;
			}
			return additionallyAllowedTenants.OrderBy((string s) => s).ToArray();
		}
	}
	internal abstract class TenantIdResolverBase
	{
		public static readonly string[] AllTenants = new string[1] { "*" };

		public static TenantIdResolver Default => new TenantIdResolver();

		public abstract string Resolve(string explicitTenantId, TokenRequestContext context, string[] additionallyAllowedTenantIds);

		public abstract string[] ResolveAddionallyAllowedTenantIds(IList<string> additionallyAllowedTenants);
	}
	internal class TokenCache
	{
		private class CacheTimestamp
		{
			private DateTimeOffset _timestamp;

			public DateTimeOffset Value => _timestamp;

			public CacheTimestamp()
			{
				Update();
			}

			public DateTimeOffset Update()
			{
				_timestamp = DateTimeOffset.UtcNow;
				return _timestamp;
			}
		}

		private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

		private DateTimeOffset _lastUpdated;

		private ConditionalWeakTable<object, CacheTimestamp> _cacheAccessMap;

		internal Func<IPublicClientApplication> _publicClientApplicationFactory;

		private readonly bool _allowUnencryptedStorage;

		private readonly string _name;

		private readonly bool _persistToDisk;

		private Azure.Core.AsyncLockWithValue<MsalCacheHelperWrapper> cacheHelperLock = new Azure.Core.AsyncLockWithValue<MsalCacheHelperWrapper>();

		private readonly MsalCacheHelperWrapper _cacheHelperWrapper;

		internal Func<TokenCacheUpdatedArgs, Task> TokenCacheUpdatedAsync;

		internal Func<TokenCacheRefreshArgs, CancellationToken, Task<TokenCacheData>> RefreshCacheFromOptionsAsync;

		internal byte[] Data { get; private set; }

		internal bool IsCaeEnabled { get; }

		public TokenCache(TokenCachePersistenceOptions options = null, bool enableCae = false)
			: this(options, null, null, enableCae)
		{
		}

		internal TokenCache(TokenCachePersistenceOptions options, MsalCacheHelperWrapper cacheHelperWrapper, Func<IPublicClientApplication> publicApplicationFactory = null, bool enableCae = false)
		{
			_cacheHelperWrapper = cacheHelperWrapper ?? new MsalCacheHelperWrapper();
			_publicClientApplicationFactory = publicApplicationFactory ?? ((Func<IPublicClientApplication>)(() => PublicClientApplicationBuilder.Create(Guid.NewGuid().ToString()).Build()));
			IsCaeEnabled = enableCae;
			if (options is UnsafeTokenCacheOptions unsafeTokenCacheOptions)
			{
				TokenCacheUpdatedAsync = unsafeTokenCacheOptions.TokenCacheUpdatedAsync;
				RefreshCacheFromOptionsAsync = unsafeTokenCacheOptions.RefreshCacheAsync;
				_lastUpdated = DateTimeOffset.UtcNow;
				_cacheAccessMap = new ConditionalWeakTable<object, CacheTimestamp>();
			}
			else
			{
				_allowUnencryptedStorage = options?.UnsafeAllowUnencryptedStorage ?? false;
				_name = (options?.Name ?? "msal.cache") + (enableCae ? ".cae" : ".nocae");
				_persistToDisk = true;
			}
		}

		internal virtual async Task RegisterCache(bool async, ITokenCache tokenCache, CancellationToken cancellationToken)
		{
			if (_persistToDisk)
			{
				(await GetCacheHelperAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).RegisterCache(tokenCache);
				return;
			}
			if (async)
			{
				await _lock.WaitAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				_lock.Wait(cancellationToken);
			}
			try
			{
				if (!_cacheAccessMap.TryGetValue(tokenCache, out var _))
				{
					tokenCache.SetBeforeAccessAsync(OnBeforeCacheAccessAsync);
					tokenCache.SetAfterAccessAsync(OnAfterCacheAccessAsync);
					_cacheAccessMap.Add(tokenCache, new CacheTimestamp());
				}
			}
			finally
			{
				_lock.Release();
			}
		}

		private async Task OnBeforeCacheAccessAsync(TokenCacheNotificationArgs args)
		{
			await _lock.WaitAsync().ConfigureAwait(continueOnCapturedContext: false);
			try
			{
				if (RefreshCacheFromOptionsAsync != null)
				{
					Data = (await RefreshCacheFromOptionsAsync(new TokenCacheRefreshArgs(args, IsCaeEnabled), default(CancellationToken)).ConfigureAwait(continueOnCapturedContext: false)).CacheBytes.ToArray();
				}
				args.TokenCache.DeserializeMsalV3(Data, shouldClearExistingCache: true);
				_cacheAccessMap.GetOrCreateValue(args.TokenCache).Update();
			}
			finally
			{
				_lock.Release();
			}
		}

		private async Task OnAfterCacheAccessAsync(TokenCacheNotificationArgs args)
		{
			if (args.HasStateChanged)
			{
				await UpdateCacheDataAsync(args.TokenCache).ConfigureAwait(continueOnCapturedContext: false);
			}
		}

		private async Task UpdateCacheDataAsync(ITokenCacheSerializer tokenCache)
		{
			await _lock.WaitAsync().ConfigureAwait(continueOnCapturedContext: false);
			try
			{
				if (!_cacheAccessMap.TryGetValue(tokenCache, out var value) || value.Value < _lastUpdated)
				{
					Data = await MergeCacheData(Data, tokenCache.SerializeMsalV3()).ConfigureAwait(continueOnCapturedContext: false);
				}
				else
				{
					Data = tokenCache.SerializeMsalV3();
				}
				if (TokenCacheUpdatedAsync != null)
				{
					byte[] array = Data.ToArray();
					await TokenCacheUpdatedAsync(new TokenCacheUpdatedArgs(array, IsCaeEnabled)).ConfigureAwait(continueOnCapturedContext: false);
				}
				_lastUpdated = _cacheAccessMap.GetOrCreateValue(tokenCache).Update();
			}
			finally
			{
				_lock.Release();
			}
		}

		private async Task<byte[]> MergeCacheData(byte[] cacheA, byte[] cacheB)
		{
			byte[] merged = null;
			IPublicClientApplication client = _publicClientApplicationFactory();
			client.UserTokenCache.SetBeforeAccess(delegate(TokenCacheNotificationArgs args)
			{
				args.TokenCache.DeserializeMsalV3(cacheA);
			});
			await client.GetAccountsAsync().ConfigureAwait(continueOnCapturedContext: false);
			client.UserTokenCache.SetBeforeAccess(delegate(TokenCacheNotificationArgs args)
			{
				args.TokenCache.DeserializeMsalV3(cacheB);
			});
			client.UserTokenCache.SetAfterAccess(delegate(TokenCacheNotificationArgs args)
			{
				merged = args.TokenCache.SerializeMsalV3();
			});
			await client.GetAccountsAsync().ConfigureAwait(continueOnCapturedContext: false);
			return merged;
		}

		private async Task<MsalCacheHelperWrapper> GetCacheHelperAsync(bool async, CancellationToken cancellationToken)
		{
			using Azure.Core.AsyncLockWithValue<MsalCacheHelperWrapper>.LockOrValue asyncLock = await cacheHelperLock.GetLockOrValueAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (asyncLock.HasValue)
			{
				return asyncLock.Value;
			}
			MsalCacheHelperWrapper cacheHelper = default(MsalCacheHelperWrapper);
			try
			{
				cacheHelper = await GetProtectedCacheHelperAsync(async, _name).ConfigureAwait(continueOnCapturedContext: false);
				cacheHelper.VerifyPersistence();
			}
			catch (MsalCachePersistenceException ex)
			{
				if (_allowUnencryptedStorage)
				{
					cacheHelper = await GetFallbackCacheHelperAsync(async, _name).ConfigureAwait(continueOnCapturedContext: false);
					cacheHelper.VerifyPersistence();
				}
				else
				{
					ExceptionDispatchInfo.Capture((ex as Exception) ?? throw ex).Throw();
				}
			}
			asyncLock.SetValue(cacheHelper);
			return cacheHelper;
		}

		private async Task<MsalCacheHelperWrapper> GetProtectedCacheHelperAsync(bool async, string name)
		{
			StorageCreationProperties storageProperties = new StorageCreationPropertiesBuilder(name, Constants.DefaultMsalTokenCacheDirectory).WithMacKeyChain("Microsoft.Developer.IdentityService", name).WithLinuxKeyring("msal.cache", "default", name, Constants.DefaultMsaltokenCacheKeyringAttribute1, Constants.DefaultMsaltokenCacheKeyringAttribute2).Build();
			return await InitializeCacheHelper(async, storageProperties).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async Task<MsalCacheHelperWrapper> GetFallbackCacheHelperAsync(bool async, string name = "msal.cache")
		{
			StorageCreationProperties storageProperties = new StorageCreationPropertiesBuilder(name, Constants.DefaultMsalTokenCacheDirectory).WithMacKeyChain("Microsoft.Developer.IdentityService", name).WithLinuxUnprotectedFile().Build();
			return await InitializeCacheHelper(async, storageProperties).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async Task<MsalCacheHelperWrapper> InitializeCacheHelper(bool async, StorageCreationProperties storageProperties)
		{
			if (async)
			{
				await _cacheHelperWrapper.InitializeAsync(storageProperties).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				_cacheHelperWrapper.InitializeAsync(storageProperties).GetAwaiter().GetResult();
			}
			return _cacheHelperWrapper;
		}
	}
	public struct TokenCacheData
	{
		public ReadOnlyMemory<byte> CacheBytes { get; }

		public TokenCacheData(ReadOnlyMemory<byte> cacheBytes)
		{
			CacheBytes = cacheBytes;
		}
	}
	public class TokenCachePersistenceOptions
	{
		public string Name { get; set; }

		public bool UnsafeAllowUnencryptedStorage { get; set; }

		internal TokenCachePersistenceOptions Clone()
		{
			return new TokenCachePersistenceOptions
			{
				Name = Name,
				UnsafeAllowUnencryptedStorage = UnsafeAllowUnencryptedStorage
			};
		}
	}
	public class TokenCacheRefreshArgs
	{
		public string SuggestedCacheKey { get; }

		public bool IsCaeEnabled { get; }

		internal TokenCacheRefreshArgs(TokenCacheNotificationArgs args, bool enableCae)
		{
			SuggestedCacheKey = args.SuggestedCacheKey;
			IsCaeEnabled = enableCae;
		}
	}
	public class TokenCacheUpdatedArgs
	{
		public ReadOnlyMemory<byte> UnsafeCacheData { get; }

		public bool IsCaeEnabled { get; }

		internal TokenCacheUpdatedArgs(ReadOnlyMemory<byte> cacheData, bool enableCae)
		{
			UnsafeCacheData = cacheData;
			IsCaeEnabled = enableCae;
		}
	}
	public class TokenCredentialDiagnosticsOptions : DiagnosticsOptions
	{
		public bool IsAccountIdentifierLoggingEnabled { get; set; }
	}
	internal class TokenExchangeManagedIdentitySource : ManagedIdentitySource
	{
		private class TokenFileCache
		{
			private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

			private readonly string _tokenFilePath;

			private string _tokenFileContents;

			private DateTimeOffset _refreshOn = DateTimeOffset.MinValue;

			public TokenFileCache(string tokenFilePath)
			{
				_tokenFilePath = tokenFilePath;
			}

			public async Task<string> GetTokenFileContentsAsync(CancellationToken cancellationToken)
			{
				if (_refreshOn <= DateTimeOffset.UtcNow)
				{
					try
					{
						await semaphore.WaitAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						if (_refreshOn <= DateTimeOffset.UtcNow)
						{
							_tokenFileContents = await ReadAllTextAsync(_tokenFilePath).ConfigureAwait(continueOnCapturedContext: false);
							_refreshOn = DateTimeOffset.UtcNow.AddMinutes(5.0);
						}
					}
					finally
					{
						semaphore.Release();
					}
				}
				return _tokenFileContents;
			}
		}

		private TokenFileCache _tokenFileCache;

		private ClientAssertionCredential _clientAssertionCredential;

		private static readonly int DefaultBufferSize = 4096;

		private TokenExchangeManagedIdentitySource(CredentialPipeline pipeline, string tenantId, string clientId, string tokenFilePath)
			: base(pipeline)
		{
			_tokenFileCache = new TokenFileCache(tokenFilePath);
			_clientAssertionCredential = new ClientAssertionCredential(tenantId, clientId, _tokenFileCache.GetTokenFileContentsAsync, new ClientAssertionCredentialOptions
			{
				Pipeline = pipeline
			});
		}

		public static ManagedIdentitySource TryCreate(ManagedIdentityClientOptions options)
		{
			string azureFederatedTokenFile = EnvironmentVariables.AzureFederatedTokenFile;
			string tenantId = EnvironmentVariables.TenantId;
			string text = options.ClientId ?? EnvironmentVariables.ClientId;
			if (options.ExcludeTokenExchangeManagedIdentitySource || string.IsNullOrEmpty(azureFederatedTokenFile) || string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new TokenExchangeManagedIdentitySource(options.Pipeline, tenantId, text, azureFederatedTokenFile);
		}

		public override async ValueTask<AccessToken> AuthenticateAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			return (!async) ? _clientAssertionCredential.GetToken(context, cancellationToken) : (await _clientAssertionCredential.GetTokenAsync(context, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		protected override Request CreateRequest(string[] scopes)
		{
			throw new NotImplementedException();
		}

		internal static Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ReadAllTextAsync(path, Encoding.UTF8, cancellationToken);
		}

		internal static Task<string> ReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken = default(CancellationToken))
		{
			Azure.Core.Argument.AssertNotNullOrEmpty(path, "path");
			Azure.Core.Argument.AssertNotNull(encoding, "encoding");
			if (!cancellationToken.IsCancellationRequested)
			{
				return InternalReadAllTextAsync(path, encoding, cancellationToken);
			}
			return Task.FromCanceled<string>(cancellationToken);
		}

		private static async Task<string> InternalReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken)
		{
			char[] buffer = null;
			StreamReader sr = AsyncStreamReader(path, encoding);
			try
			{
				cancellationToken.ThrowIfCancellationRequested();
				buffer = ArrayPool<char>.Shared.Rent(sr.CurrentEncoding.GetMaxCharCount(DefaultBufferSize));
				StringBuilder sb = new StringBuilder();
				int totalRead = 0;
				while (true)
				{
					int num = await sr.ReadAsync(buffer, totalRead, DefaultBufferSize - totalRead).ConfigureAwait(continueOnCapturedContext: false);
					if (num == 0)
					{
						break;
					}
					sb.Append(buffer, 0, num);
					totalRead += num;
				}
				return sb.ToString();
			}
			finally
			{
				sr.Dispose();
				if (buffer != null)
				{
					ArrayPool<char>.Shared.Return(buffer);
				}
			}
		}

		private static StreamReader AsyncStreamReader(string path, Encoding encoding)
		{
			return new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, FileOptions.Asynchronous | FileOptions.SequentialScan), encoding, detectEncodingFromByteOrderMarks: true);
		}
	}
	internal static class TokenHelper
	{
		public static (string ClientId, string TenantId, string Upn, string ObjectId) ParseAccountInfoFromToken(string token)
		{
			Azure.Core.Argument.AssertNotNullOrEmpty(token, "token");
			string[] array = token.Split(new char[1] { '.' });
			if (array.Length != 3)
			{
				throw new ArgumentException("Invalid token", "token");
			}
			(string, string, string, string) result = default((string, string, string, string));
			try
			{
				string text = array[1].Replace('_', '/').Replace('-', '+');
				switch (array[1].Length % 4)
				{
				case 2:
					text += "==";
					break;
				case 3:
					text += "=";
					break;
				}
				Utf8JsonReader utf8JsonReader = new Utf8JsonReader(Convert.FromBase64String(text));
				while (utf8JsonReader.Read())
				{
					if (utf8JsonReader.TokenType == JsonTokenType.PropertyName)
					{
						switch (utf8JsonReader.GetString())
						{
						case "appid":
							utf8JsonReader.Read();
							result.Item1 = utf8JsonReader.GetString();
							break;
						case "tid":
							utf8JsonReader.Read();
							result.Item2 = utf8JsonReader.GetString();
							break;
						case "upn":
							utf8JsonReader.Read();
							result.Item3 = utf8JsonReader.GetString();
							break;
						case "oid":
							utf8JsonReader.Read();
							result.Item4 = utf8JsonReader.GetString();
							break;
						default:
							utf8JsonReader.Read();
							break;
						}
					}
				}
			}
			catch
			{
				AzureIdentityEventSource.Singleton.UnableToParseAccountDetailsFromToken();
			}
			return result;
		}
	}
	public abstract class UnsafeTokenCacheOptions : TokenCachePersistenceOptions
	{
		protected internal abstract Task TokenCacheUpdatedAsync(TokenCacheUpdatedArgs tokenCacheUpdatedArgs);

		protected internal abstract Task<ReadOnlyMemory<byte>> RefreshCacheAsync();

		protected internal virtual async Task<TokenCacheData> RefreshCacheAsync(TokenCacheRefreshArgs args, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new TokenCacheData(await RefreshCacheAsync().ConfigureAwait(continueOnCapturedContext: false));
		}
	}
	internal static class Validations
	{
		internal const string InvalidTenantIdErrorMessage = "Invalid tenant id provided. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names";

		private const string NullTenantIdErrorMessage = "Tenant id cannot be null. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names";

		private const string NonTlsAuthorityHostErrorMessage = "Authority host must be a TLS protected (https) endpoint.";

		internal const string NoWindowsPowerShellLegacyErrorMessage = "PowerShell Legacy is only supported in Windows.";

		public static string ValidateTenantId(string tenantId, string argumentName = null, bool allowNull = false)
		{
			if (tenantId != null)
			{
				if (tenantId.Length == 0)
				{
					throw (argumentName != null) ? new ArgumentException("Invalid tenant id provided. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names", argumentName) : new ArgumentException("Invalid tenant id provided. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names");
				}
				for (int i = 0; i < tenantId.Length; i++)
				{
					if (!IsValidTenantCharacter(tenantId[i]))
					{
						throw (argumentName != null) ? new ArgumentException("Invalid tenant id provided. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names", argumentName) : new ArgumentException("Invalid tenant id provided. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names");
					}
				}
			}
			else if (!allowNull)
			{
				throw (argumentName != null) ? new ArgumentNullException(argumentName, "Tenant id cannot be null. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names") : new ArgumentNullException("Invalid tenant id provided. You can locate your tenant id by following the instructions listed here: https://learn.microsoft.com/partner-center/find-ids-and-domain-names", (Exception)null);
			}
			return tenantId;
		}

		public static Uri ValidateAuthorityHost(Uri authorityHost)
		{
			if (authorityHost != null && string.Compare(authorityHost.Scheme, "https", StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException("Authority host must be a TLS protected (https) endpoint.");
			}
			return authorityHost;
		}

		public static bool CanUseLegacyPowerShell(bool useLegacyPowerShell)
		{
			if (useLegacyPowerShell && !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				throw new ArgumentException("PowerShell Legacy is only supported in Windows.");
			}
			return useLegacyPowerShell;
		}

		private static bool IsValidTenantCharacter(char c)
		{
			if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && (c < '0' || c > '9') && c != '.')
			{
				return c == '-';
			}
			return true;
		}
	}
	internal static class WindowsNativeMethods
	{
		public enum CRED_PERSIST : uint
		{
			CRED_PERSIST_SESSION = 1u,
			CRED_PERSIST_LOCAL_MACHINE,
			CRED_PERSIST_ENTERPRISE
		}

		public enum CRED_TYPE
		{
			GENERIC = 1,
			DOMAIN_PASSWORD,
			DOMAIN_CERTIFICATE,
			DOMAIN_VISIBLE_PASSWORD,
			MAXIMUM
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct CredentialData
		{
			public uint Flags;

			public CRED_TYPE Type;

			public string TargetName;

			public string Comment;

			public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;

			public uint CredentialBlobSize;

			public IntPtr CredentialBlob;

			public CRED_PERSIST Persist;

			public uint AttributeCount;

			public IntPtr Attributes;

			public string TargetAlias;

			public string UserName;
		}

		private static class Imports
		{
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			[DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
			public static extern uint FormatMessage(uint dwFlags, IntPtr lpSource, int dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer, uint nSize, IntPtr pArguments);

			[DllImport("ntdll.dll")]
			[DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
			public static extern int RtlNtStatusToDosError(int Status);

			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredReadW", SetLastError = true)]
			[DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
			public static extern bool CredRead(string target, CRED_TYPE type, int reservedFlag, out IntPtr userCredential);

			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredWriteW", SetLastError = true)]
			[DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
			public static extern bool CredWrite(IntPtr userCredential, int reservedFlag);

			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredDeleteW", SetLastError = true)]
			[DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
			public static extern bool CredDelete(string target, CRED_TYPE type, int reservedFlag);

			[DllImport("advapi32.dll", SetLastError = true)]
			[DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
			public static extern void CredFree([In] IntPtr buffer);
		}

		public const int ERROR_NOT_FOUND = 1168;

		public const uint FORMAT_MESSAGE_ALLOCATE_BUFFER = 256u;

		public const uint FORMAT_MESSAGE_IGNORE_INSERTS = 512u;

		public const uint FORMAT_MESSAGE_FROM_SYSTEM = 4096u;

		public static IntPtr CredRead(string target, CRED_TYPE type)
		{
			ThrowIfFailed(Imports.CredRead(target, type, 0, out var userCredential), "CredRead");
			return userCredential;
		}

		public static void CredWrite(IntPtr userCredential)
		{
			ThrowIfFailed(Imports.CredWrite(userCredential, 0), "CredWrite");
		}

		public static void CredDelete(string target, CRED_TYPE type)
		{
			ThrowIfFailed(Imports.CredDelete(target, type, 0), "CredDelete");
		}

		public static void CredFree(IntPtr userCredential)
		{
			if (userCredential != IntPtr.Zero)
			{
				Imports.CredFree(userCredential);
			}
		}

		private static void ThrowIfFailed(bool isSucceeded, [CallerMemberName] string methodName = null)
		{
			if (isSucceeded)
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			throw new InvalidOperationException((lastWin32Error == 1168) ? (methodName + " has failed but error is unknown.") : MessageFromErrorCode(lastWin32Error));
		}

		private static string MessageFromErrorCode(int errorCode)
		{
			uint dwFlags = 4864u;
			IntPtr lpBuffer = IntPtr.Zero;
			string text = null;
			try
			{
				if (Imports.FormatMessage(dwFlags, IntPtr.Zero, errorCode, 0u, ref lpBuffer, 0u, IntPtr.Zero) == 0)
				{
					return new Win32Exception(Imports.RtlNtStatusToDosError(errorCode)).Message;
				}
			}
			finally
			{
				if (lpBuffer != IntPtr.Zero)
				{
					text = Marshal.PtrToStringUni(lpBuffer);
					Marshal.FreeHGlobal(lpBuffer);
				}
			}
			return text ?? string.Empty;
		}
	}
	internal sealed class WindowsVisualStudioCodeAdapter : IVisualStudioCodeAdapter
	{
		private static readonly string s_userSettingsJsonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Code", "User", "settings.json");

		public string GetUserSettingsPath()
		{
			return s_userSettingsJsonPath;
		}

		public string GetCredentials(string serviceName, string accountName)
		{
			IntPtr intPtr = WindowsNativeMethods.CredRead(serviceName + "/" + accountName, WindowsNativeMethods.CRED_TYPE.GENERIC);
			try
			{
				WindowsNativeMethods.CredentialData credentialData = Marshal.PtrToStructure<WindowsNativeMethods.CredentialData>(intPtr);
				return Marshal.PtrToStringAnsi(credentialData.CredentialBlob, (int)credentialData.CredentialBlobSize);
			}
			finally
			{
				WindowsNativeMethods.CredFree(intPtr);
			}
		}
	}
	internal class X509Certificate2FromFileProvider : IX509Certificate2Provider
	{
		private delegate void ImportPkcs8PrivateKeyDelegate(ReadOnlySpan<byte> blob, out int bytesRead);

		private X509Certificate2 Certificate { get; set; }

		internal string CertificatePath { get; }

		internal string CertificatePassword { get; }

		public X509Certificate2FromFileProvider(string clientCertificatePath, string certificatePassword)
		{
			Azure.Core.Argument.AssertNotNull(clientCertificatePath, "clientCertificatePath");
			CertificatePath = clientCertificatePath ?? throw new ArgumentNullException("clientCertificatePath");
			CertificatePassword = certificatePassword;
		}

		public ValueTask<X509Certificate2> GetCertificateAsync(bool async, CancellationToken cancellationToken)
		{
			if (Certificate != null)
			{
				return new ValueTask<X509Certificate2>(Certificate);
			}
			string text = Path.GetExtension(CertificatePath).ToLowerInvariant();
			if (!(text == ".pfx"))
			{
				if (text == ".pem")
				{
					if (CertificatePassword != null)
					{
						throw new CredentialUnavailableException("Password protection for PEM encoded certificates is not supported.");
					}
					return LoadCertificateFromPemFileAsync(async, CertificatePath, cancellationToken);
				}
				throw new CredentialUnavailableException("Only .pfx and .pem files are supported.");
			}
			return LoadCertificateFromPfxFileAsync(async, CertificatePath, CertificatePassword, cancellationToken);
		}

		private async ValueTask<X509Certificate2> LoadCertificateFromPfxFileAsync(bool async, string clientCertificatePath, string certificatePassword, CancellationToken cancellationToken)
		{
			if (Certificate != null)
			{
				return Certificate;
			}
			try
			{
				if (!async)
				{
					Certificate = new X509Certificate2(clientCertificatePath, certificatePassword);
				}
				else
				{
					List<byte> certContents = new List<byte>();
					byte[] buf = new byte[4096];
					int offset = 0;
					using (Stream s = File.OpenRead(clientCertificatePath))
					{
						int num;
						do
						{
							num = await s.ReadAsync(buf, offset, buf.Length, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
							for (int i = 0; i < num; i++)
							{
								certContents.Add(buf[i]);
							}
						}
						while (num != 0);
					}
					Certificate = new X509Certificate2(certContents.ToArray(), certificatePassword);
				}
				return Certificate;
			}
			catch (Exception ex) when (!(ex is OperationCanceledException))
			{
				throw new CredentialUnavailableException("Could not load certificate file", ex);
			}
		}

		private async ValueTask<X509Certificate2> LoadCertificateFromPemFileAsync(bool async, string clientCertificatePath, CancellationToken cancellationToken)
		{
			if (Certificate != null)
			{
				return Certificate;
			}
			try
			{
				string text;
				if (!async)
				{
					text = File.ReadAllText(clientCertificatePath);
				}
				else
				{
					cancellationToken.ThrowIfCancellationRequested();
					using StreamReader sr = new StreamReader(clientCertificatePath);
					text = await sr.ReadToEndAsync().ConfigureAwait(continueOnCapturedContext: false);
				}
				Certificate = PemReader.LoadCertificate(MemoryExtensions.AsSpan(text), null, PemReader.KeyType.RSA);
				return Certificate;
			}
			catch (Exception ex) when (!(ex is OperationCanceledException))
			{
				throw new CredentialUnavailableException("Could not load certificate file", ex);
			}
		}
	}
	internal class X509Certificate2FromObjectProvider : IX509Certificate2Provider
	{
		private X509Certificate2 Certificate { get; }

		public X509Certificate2FromObjectProvider(X509Certificate2 clientCertificate)
		{
			Certificate = clientCertificate ?? throw new ArgumentNullException("clientCertificate");
		}

		public ValueTask<X509Certificate2> GetCertificateAsync(bool async, CancellationToken cancellationToken)
		{
			return new ValueTask<X509Certificate2>(Certificate);
		}
	}
}
