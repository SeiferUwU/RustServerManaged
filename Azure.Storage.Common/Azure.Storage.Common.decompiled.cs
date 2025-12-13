using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Hashing;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Azure.Core;
using Azure.Core.Cryptography;
using Azure.Core.Pipeline;
using Azure.Storage.Sas;
using Microsoft.CodeAnalysis;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: InternalsVisibleTo("Azure.Storage.Common.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100d15ddcb29688295338af4b7686603fe614abd555e09efba8fb88ee09e1f7b1ccaeed2e8f823fa9eef3fdd60217fc012ea67d2479751a0b8c087a4185541b851bd8b16f8d91b840e51b1cb0ba6fe647997e57429265e85ef62d565db50a69ae1647d54d7bd855e4db3d8a91510e5bcbd0edfbbecaa20a7bd9ae74593daa7b11b4")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2, PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]
[assembly: TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = ".NET Standard 2.0")]
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("© Microsoft Corporation. All rights reserved.")]
[assembly: AssemblyDescription("\r\n      This client library enables working with the Microsoft Azure Storage services which include the blob and file services for storing binary and text data, and the queue service for storing messages that may be accessed by a client.\r\n      For this release see notes - https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/storage/Azure.Storage.Common/README.md and https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/storage/Azure.Storage.Common/CHANGELOG.md\r\n      in addition to the breaking changes https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/storage/Azure.Storage.Common/BreakingChanges.txt\r\n      Microsoft Azure Storage quickstarts and tutorials - https://docs.microsoft.com/en-us/azure/storage/\r\n      Microsoft Azure Storage REST API Reference - https://docs.microsoft.com/en-us/rest/api/storageservices/\r\n    ")]
[assembly: AssemblyFileVersion("12.1800.123.56305")]
[assembly: AssemblyInformationalVersion("12.18.1+675cf1fc091d02e385f4f8455beab2e9a40adc58")]
[assembly: AssemblyProduct("Azure .NET SDK")]
[assembly: AssemblyTitle("Microsoft Azure.Storage.Common client library")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/Azure/azure-sdk-for-net")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: AssemblyVersion("12.18.1.0")]
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
	internal class NoBodyResponse<T> : Response<T>
	{
		private class ResponseBodyNotFoundException : Exception
		{
			public int Status { get; }

			public ResponseBodyNotFoundException(int status, string message)
				: base(message)
			{
				Status = status;
			}
		}

		private readonly Response _response;

		public override bool HasValue => false;

		public override T Value
		{
			get
			{
				throw new ResponseBodyNotFoundException(_response.Status, _response.ReasonPhrase);
			}
		}

		public NoBodyResponse(Response response)
		{
			_response = response;
		}

		public override Response GetRawResponse()
		{
			return _response;
		}

		public override string ToString()
		{
			return $"Status: {GetRawResponse().Status}, Service returned no content";
		}
	}
}
namespace Azure.Core
{
	internal static class AuthorizationChallengeParser
	{
		public static string? GetChallengeParameterFromResponse(Response response, string challengeScheme, string challengeParameter)
		{
			if (response.Status != 401 || !response.Headers.TryGetValue(HttpHeader.Names.WwwAuthenticate, out string value))
			{
				return null;
			}
			ReadOnlySpan<char> other = MemoryExtensions.AsSpan(challengeScheme);
			ReadOnlySpan<char> other2 = MemoryExtensions.AsSpan(challengeParameter);
			ReadOnlySpan<char> headerValue = MemoryExtensions.AsSpan(value);
			ReadOnlySpan<char> challengeKey;
			while (TryGetNextChallenge(ref headerValue, out challengeKey))
			{
				ReadOnlySpan<char> paramKey;
				ReadOnlySpan<char> paramValue;
				while (TryGetNextParameter(ref headerValue, out paramKey, out paramValue))
				{
					if (MemoryExtensions.Equals(challengeKey, other, StringComparison.OrdinalIgnoreCase) && MemoryExtensions.Equals(paramKey, other2, StringComparison.OrdinalIgnoreCase))
					{
						return paramValue.ToString();
					}
				}
			}
			return null;
		}

		internal static bool TryGetNextChallenge(ref ReadOnlySpan<char> headerValue, out ReadOnlySpan<char> challengeKey)
		{
			challengeKey = default(ReadOnlySpan<char>);
			headerValue = headerValue.TrimStart(' ');
			int num = headerValue.IndexOf(' ');
			if (num < 0)
			{
				return false;
			}
			challengeKey = headerValue.Slice(0, num);
			headerValue = headerValue.Slice(num + 1);
			return true;
		}

		internal static bool TryGetNextParameter(ref ReadOnlySpan<char> headerValue, out ReadOnlySpan<char> paramKey, out ReadOnlySpan<char> paramValue, char separator = '=')
		{
			paramKey = default(ReadOnlySpan<char>);
			paramValue = default(ReadOnlySpan<char>);
			ReadOnlySpan<char> readOnlySpan = MemoryExtensions.AsSpan(" ,");
			headerValue = headerValue.TrimStart(readOnlySpan);
			int num = headerValue.IndexOf(' ');
			int num2 = headerValue.IndexOf(separator);
			if (num < num2 && num != -1)
			{
				return false;
			}
			if (num2 < 0)
			{
				return false;
			}
			paramKey = headerValue.Slice(0, num2).Trim();
			headerValue = headerValue.Slice(num2 + 1);
			int num3 = headerValue.IndexOf('"');
			headerValue = headerValue.Slice(num3 + 1);
			if (num3 >= 0)
			{
				paramValue = headerValue.Slice(0, headerValue.IndexOf('"'));
			}
			else
			{
				int num4 = headerValue.IndexOfAny(readOnlySpan);
				if (num4 >= 0)
				{
					paramValue = headerValue.Slice(0, num4);
				}
				else
				{
					paramValue = headerValue;
				}
			}
			if (headerValue != paramValue)
			{
				headerValue = headerValue.Slice(paramValue.Length + 1);
			}
			return true;
		}
	}
	[AttributeUsage(AttributeTargets.Method)]
	internal class CallerShouldAuditAttribute : Attribute
	{
		public string? Reason { get; set; }
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
	internal class ChangeTrackingDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>> where TKey : notnull
	{
		private IDictionary<TKey, TValue>? _innerDictionary;

		public bool IsUndefined => _innerDictionary == null;

		public int Count
		{
			get
			{
				if (IsUndefined)
				{
					return 0;
				}
				return EnsureDictionary().Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				if (IsUndefined)
				{
					return false;
				}
				return EnsureDictionary().IsReadOnly;
			}
		}

		public TValue this[TKey key]
		{
			get
			{
				if (IsUndefined)
				{
					throw new KeyNotFoundException("key");
				}
				return EnsureDictionary()[key];
			}
			set
			{
				EnsureDictionary()[key] = value;
			}
		}

		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Keys;

		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Values;

		public ICollection<TKey> Keys
		{
			get
			{
				if (IsUndefined)
				{
					return Array.Empty<TKey>();
				}
				return EnsureDictionary().Keys;
			}
		}

		public ICollection<TValue> Values
		{
			get
			{
				if (IsUndefined)
				{
					return Array.Empty<TValue>();
				}
				return EnsureDictionary().Values;
			}
		}

		public ChangeTrackingDictionary()
		{
		}

		public ChangeTrackingDictionary(Optional<IReadOnlyDictionary<TKey, TValue>> optionalDictionary)
			: this(optionalDictionary.Value)
		{
		}

		public ChangeTrackingDictionary(Optional<IDictionary<TKey, TValue>> optionalDictionary)
			: this(optionalDictionary.Value)
		{
		}

		private ChangeTrackingDictionary(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary != null)
			{
				_innerDictionary = new Dictionary<TKey, TValue>(dictionary);
			}
		}

		private ChangeTrackingDictionary(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				return;
			}
			_innerDictionary = new Dictionary<TKey, TValue>();
			foreach (KeyValuePair<TKey, TValue> item in dictionary)
			{
				_innerDictionary.Add(item);
			}
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			if (IsUndefined)
			{
				return GetEmptyEnumerator();
			}
			return EnsureDictionary().GetEnumerator();
			static IEnumerator<KeyValuePair<TKey, TValue>> GetEmptyEnumerator()
			{
				yield break;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			EnsureDictionary().Add(item);
		}

		public void Clear()
		{
			EnsureDictionary().Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			if (IsUndefined)
			{
				return false;
			}
			return EnsureDictionary().Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (!IsUndefined)
			{
				EnsureDictionary().CopyTo(array, arrayIndex);
			}
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			if (IsUndefined)
			{
				return false;
			}
			return EnsureDictionary().Remove(item);
		}

		public void Add(TKey key, TValue value)
		{
			EnsureDictionary().Add(key, value);
		}

		public bool ContainsKey(TKey key)
		{
			if (IsUndefined)
			{
				return false;
			}
			return EnsureDictionary().ContainsKey(key);
		}

		public bool Remove(TKey key)
		{
			if (IsUndefined)
			{
				return false;
			}
			return EnsureDictionary().Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			if (IsUndefined)
			{
				value = default(TValue);
				return false;
			}
			return EnsureDictionary().TryGetValue(key, out value);
		}

		private IDictionary<TKey, TValue> EnsureDictionary()
		{
			return _innerDictionary ?? (_innerDictionary = new Dictionary<TKey, TValue>());
		}
	}
	internal class ChangeTrackingList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		private IList<T>? _innerList;

		public bool IsUndefined => _innerList == null;

		public int Count
		{
			get
			{
				if (IsUndefined)
				{
					return 0;
				}
				return EnsureList().Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				if (IsUndefined)
				{
					return false;
				}
				return EnsureList().IsReadOnly;
			}
		}

		public T this[int index]
		{
			get
			{
				if (IsUndefined)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return EnsureList()[index];
			}
			set
			{
				if (IsUndefined)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				EnsureList()[index] = value;
			}
		}

		public ChangeTrackingList()
		{
		}

		public ChangeTrackingList(Optional<IList<T>> optionalList)
			: this(optionalList.Value)
		{
		}

		public ChangeTrackingList(Optional<IReadOnlyList<T>> optionalList)
			: this((IEnumerable<T>)optionalList.Value)
		{
		}

		private ChangeTrackingList(IEnumerable<T> innerList)
		{
			if (innerList != null)
			{
				_innerList = innerList.ToList();
			}
		}

		private ChangeTrackingList(IList<T> innerList)
		{
			if (innerList != null)
			{
				_innerList = innerList;
			}
		}

		public void Reset()
		{
			_innerList = null;
		}

		public IEnumerator<T> GetEnumerator()
		{
			if (IsUndefined)
			{
				return EnumerateEmpty();
			}
			return EnsureList().GetEnumerator();
			static IEnumerator<T> EnumerateEmpty()
			{
				yield break;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(T item)
		{
			EnsureList().Add(item);
		}

		public void Clear()
		{
			EnsureList().Clear();
		}

		public bool Contains(T item)
		{
			if (IsUndefined)
			{
				return false;
			}
			return EnsureList().Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			if (!IsUndefined)
			{
				EnsureList().CopyTo(array, arrayIndex);
			}
		}

		public bool Remove(T item)
		{
			if (IsUndefined)
			{
				return false;
			}
			return EnsureList().Remove(item);
		}

		public int IndexOf(T item)
		{
			if (IsUndefined)
			{
				return -1;
			}
			return EnsureList().IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			EnsureList().Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			if (IsUndefined)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			EnsureList().RemoveAt(index);
		}

		private IList<T> EnsureList()
		{
			return _innerList ?? (_innerList = new List<T>());
		}
	}
	internal class FixedDelayWithNoJitterStrategy : DelayStrategy
	{
		private static readonly TimeSpan DefaultDelay = TimeSpan.FromSeconds(1.0);

		private readonly TimeSpan _delay;

		public FixedDelayWithNoJitterStrategy(TimeSpan? suggestedDelay = null)
			: base(suggestedDelay.HasValue ? DelayStrategy.Max(suggestedDelay.Value, DefaultDelay) : DefaultDelay, 0.0)
		{
			_delay = (suggestedDelay.HasValue ? DelayStrategy.Max(suggestedDelay.Value, DefaultDelay) : DefaultDelay);
		}

		protected override TimeSpan GetNextDelayCore(Response? response, int retryNumber)
		{
			return _delay;
		}
	}
	internal class FormUrlEncodedContent : RequestContent
	{
		private List<KeyValuePair<string, string>> _values = new List<KeyValuePair<string, string>>();

		private Encoding Latin1 = Encoding.GetEncoding("iso-8859-1");

		private byte[] _bytes = Array.Empty<byte>();

		public void Add(string parameter, string value)
		{
			_values.Add(new KeyValuePair<string, string>(parameter, value));
		}

		private void BuildIfNeeded()
		{
			if (_bytes.Length == 0)
			{
				_bytes = GetContentByteArray(_values);
				_values.Clear();
			}
		}

		public override async Task WriteToAsync(Stream stream, CancellationToken cancellation)
		{
			BuildIfNeeded();
			await stream.WriteAsync(_bytes, 0, _bytes.Length, cancellation).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override void WriteTo(Stream stream, CancellationToken cancellation)
		{
			BuildIfNeeded();
			stream.Write(_bytes, 0, _bytes.Length);
		}

		public override bool TryComputeLength(out long length)
		{
			BuildIfNeeded();
			length = _bytes.Length;
			return true;
		}

		public override void Dispose()
		{
		}

		private byte[] GetContentByteArray(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
		{
			if (nameValueCollection == null)
			{
				throw new ArgumentNullException("nameValueCollection");
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> item in nameValueCollection)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append('&');
				}
				stringBuilder.Append(Encode(item.Key));
				stringBuilder.Append('=');
				stringBuilder.Append(Encode(item.Value));
			}
			return Latin1.GetBytes(stringBuilder.ToString());
		}

		private static string Encode(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				return string.Empty;
			}
			return Uri.EscapeDataString(data).Replace("%20", "+");
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

		internal static Azure.Core.HttpMessageSanitizer Default = new Azure.Core.HttpMessageSanitizer(Array.Empty<string>(), Array.Empty<string>());

		public HttpMessageSanitizer(string[] allowedQueryParameters, string[] allowedHeaders, string redactedPlaceholder = "REDACTED")
		{
			_logAllHeaders = allowedHeaders.Contains("*");
			_logFullQueries = allowedQueryParameters.Contains("*");
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
			StringBuilder stringBuilder = new StringBuilder(url.Length);
			stringBuilder.Append(url, 0, num);
			string text = url.Substring(num);
			int num2 = 1;
			stringBuilder.Append('?');
			do
			{
				int num3 = text.IndexOf('&', num2);
				int num4 = text.IndexOf('=', num2);
				bool flag = false;
				if ((num3 == -1 && num4 == -1) || (num3 != -1 && (num4 == -1 || num4 > num3)))
				{
					num4 = num3;
					flag = true;
				}
				if (num4 == -1)
				{
					num4 = text.Length;
				}
				num3 = ((num3 != -1) ? (num3 + 1) : text.Length);
				ReadOnlySpan<char> span = MemoryExtensions.AsSpan(text, num2, num4 - num2);
				bool flag2 = false;
				string[] allowedQueryParameters = _allowedQueryParameters;
				foreach (string text2 in allowedQueryParameters)
				{
					if (MemoryExtensions.Equals(span, MemoryExtensions.AsSpan(text2), StringComparison.OrdinalIgnoreCase))
					{
						flag2 = true;
						break;
					}
				}
				int num5 = num3 - num2;
				int count = num4 - num2;
				if (flag2)
				{
					stringBuilder.Append(text, num2, num5);
				}
				else if (flag)
				{
					stringBuilder.Append(text, num2, num5);
				}
				else
				{
					stringBuilder.Append(text, num2, count);
					stringBuilder.Append('=');
					stringBuilder.Append(_redactedPlaceholder);
					if (text[num3 - 1] == '&')
					{
						stringBuilder.Append('&');
					}
				}
				num2 += num5;
			}
			while (num2 < text.Length);
			return stringBuilder.ToString();
		}
	}
	internal static class HttpPipelineExtensions
	{
		internal class ErrorResponse<T> : Response<T>
		{
			private readonly Response _response;

			private readonly RequestFailedException _exception;

			public override T Value
			{
				get
				{
					throw _exception;
				}
			}

			public ErrorResponse(Response response, RequestFailedException exception)
			{
				_response = response;
				_exception = exception;
			}

			public override Response GetRawResponse()
			{
				return _response;
			}
		}

		public static async ValueTask<Response> ProcessMessageAsync(this HttpPipeline pipeline, HttpMessage message, RequestContext? requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			var (cancellationToken2, statusOption) = ApplyRequestContext(requestContext);
			if (!cancellationToken2.CanBeCanceled || !cancellationToken.CanBeCanceled)
			{
				await pipeline.SendAsync(message, cancellationToken.CanBeCanceled ? cancellationToken : cancellationToken2).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken2, cancellationToken);
				await pipeline.SendAsync(message, cts.Token).ConfigureAwait(continueOnCapturedContext: false);
			}
			if (!message.Response.IsError || statusOption == ErrorOptions.NoThrow)
			{
				return message.Response;
			}
			throw new RequestFailedException(message.Response);
		}

		public static Response ProcessMessage(this HttpPipeline pipeline, HttpMessage message, RequestContext? requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			var (cancellationToken2, errorOptions) = ApplyRequestContext(requestContext);
			if (!cancellationToken2.CanBeCanceled || !cancellationToken.CanBeCanceled)
			{
				pipeline.Send(message, cancellationToken.CanBeCanceled ? cancellationToken : cancellationToken2);
			}
			else
			{
				using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken2, cancellationToken);
				pipeline.Send(message, cancellationTokenSource.Token);
			}
			if (!message.Response.IsError || errorOptions == ErrorOptions.NoThrow)
			{
				return message.Response;
			}
			throw new RequestFailedException(message.Response);
		}

		public static async ValueTask<Response<bool>> ProcessHeadAsBoolMessageAsync(this HttpPipeline pipeline, HttpMessage message, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, RequestContext? requestContext)
		{
			Response response = await pipeline.ProcessMessageAsync(message, requestContext).ConfigureAwait(continueOnCapturedContext: false);
			switch (response.Status)
			{
			case 200:
			case 201:
			case 202:
			case 203:
			case 204:
			case 205:
			case 206:
			case 207:
			case 208:
			case 209:
			case 210:
			case 211:
			case 212:
			case 213:
			case 214:
			case 215:
			case 216:
			case 217:
			case 218:
			case 219:
			case 220:
			case 221:
			case 222:
			case 223:
			case 224:
			case 225:
			case 226:
			case 227:
			case 228:
			case 229:
			case 230:
			case 231:
			case 232:
			case 233:
			case 234:
			case 235:
			case 236:
			case 237:
			case 238:
			case 239:
			case 240:
			case 241:
			case 242:
			case 243:
			case 244:
			case 245:
			case 246:
			case 247:
			case 248:
			case 249:
			case 250:
			case 251:
			case 252:
			case 253:
			case 254:
			case 255:
			case 256:
			case 257:
			case 258:
			case 259:
			case 260:
			case 261:
			case 262:
			case 263:
			case 264:
			case 265:
			case 266:
			case 267:
			case 268:
			case 269:
			case 270:
			case 271:
			case 272:
			case 273:
			case 274:
			case 275:
			case 276:
			case 277:
			case 278:
			case 279:
			case 280:
			case 281:
			case 282:
			case 283:
			case 284:
			case 285:
			case 286:
			case 287:
			case 288:
			case 289:
			case 290:
			case 291:
			case 292:
			case 293:
			case 294:
			case 295:
			case 296:
			case 297:
			case 298:
			case 299:
				return Response.FromValue(value: true, response);
			case 400:
			case 401:
			case 402:
			case 403:
			case 404:
			case 405:
			case 406:
			case 407:
			case 408:
			case 409:
			case 410:
			case 411:
			case 412:
			case 413:
			case 414:
			case 415:
			case 416:
			case 417:
			case 418:
			case 419:
			case 420:
			case 421:
			case 422:
			case 423:
			case 424:
			case 425:
			case 426:
			case 427:
			case 428:
			case 429:
			case 430:
			case 431:
			case 432:
			case 433:
			case 434:
			case 435:
			case 436:
			case 437:
			case 438:
			case 439:
			case 440:
			case 441:
			case 442:
			case 443:
			case 444:
			case 445:
			case 446:
			case 447:
			case 448:
			case 449:
			case 450:
			case 451:
			case 452:
			case 453:
			case 454:
			case 455:
			case 456:
			case 457:
			case 458:
			case 459:
			case 460:
			case 461:
			case 462:
			case 463:
			case 464:
			case 465:
			case 466:
			case 467:
			case 468:
			case 469:
			case 470:
			case 471:
			case 472:
			case 473:
			case 474:
			case 475:
			case 476:
			case 477:
			case 478:
			case 479:
			case 480:
			case 481:
			case 482:
			case 483:
			case 484:
			case 485:
			case 486:
			case 487:
			case 488:
			case 489:
			case 490:
			case 491:
			case 492:
			case 493:
			case 494:
			case 495:
			case 496:
			case 497:
			case 498:
			case 499:
				return Response.FromValue(value: false, response);
			default:
				return new ErrorResponse<bool>(response, new RequestFailedException(response));
			}
		}

		public static Response<bool> ProcessHeadAsBoolMessage(this HttpPipeline pipeline, HttpMessage message, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, RequestContext? requestContext)
		{
			Response response = pipeline.ProcessMessage(message, requestContext);
			switch (response.Status)
			{
			case 200:
			case 201:
			case 202:
			case 203:
			case 204:
			case 205:
			case 206:
			case 207:
			case 208:
			case 209:
			case 210:
			case 211:
			case 212:
			case 213:
			case 214:
			case 215:
			case 216:
			case 217:
			case 218:
			case 219:
			case 220:
			case 221:
			case 222:
			case 223:
			case 224:
			case 225:
			case 226:
			case 227:
			case 228:
			case 229:
			case 230:
			case 231:
			case 232:
			case 233:
			case 234:
			case 235:
			case 236:
			case 237:
			case 238:
			case 239:
			case 240:
			case 241:
			case 242:
			case 243:
			case 244:
			case 245:
			case 246:
			case 247:
			case 248:
			case 249:
			case 250:
			case 251:
			case 252:
			case 253:
			case 254:
			case 255:
			case 256:
			case 257:
			case 258:
			case 259:
			case 260:
			case 261:
			case 262:
			case 263:
			case 264:
			case 265:
			case 266:
			case 267:
			case 268:
			case 269:
			case 270:
			case 271:
			case 272:
			case 273:
			case 274:
			case 275:
			case 276:
			case 277:
			case 278:
			case 279:
			case 280:
			case 281:
			case 282:
			case 283:
			case 284:
			case 285:
			case 286:
			case 287:
			case 288:
			case 289:
			case 290:
			case 291:
			case 292:
			case 293:
			case 294:
			case 295:
			case 296:
			case 297:
			case 298:
			case 299:
				return Response.FromValue(value: true, response);
			case 400:
			case 401:
			case 402:
			case 403:
			case 404:
			case 405:
			case 406:
			case 407:
			case 408:
			case 409:
			case 410:
			case 411:
			case 412:
			case 413:
			case 414:
			case 415:
			case 416:
			case 417:
			case 418:
			case 419:
			case 420:
			case 421:
			case 422:
			case 423:
			case 424:
			case 425:
			case 426:
			case 427:
			case 428:
			case 429:
			case 430:
			case 431:
			case 432:
			case 433:
			case 434:
			case 435:
			case 436:
			case 437:
			case 438:
			case 439:
			case 440:
			case 441:
			case 442:
			case 443:
			case 444:
			case 445:
			case 446:
			case 447:
			case 448:
			case 449:
			case 450:
			case 451:
			case 452:
			case 453:
			case 454:
			case 455:
			case 456:
			case 457:
			case 458:
			case 459:
			case 460:
			case 461:
			case 462:
			case 463:
			case 464:
			case 465:
			case 466:
			case 467:
			case 468:
			case 469:
			case 470:
			case 471:
			case 472:
			case 473:
			case 474:
			case 475:
			case 476:
			case 477:
			case 478:
			case 479:
			case 480:
			case 481:
			case 482:
			case 483:
			case 484:
			case 485:
			case 486:
			case 487:
			case 488:
			case 489:
			case 490:
			case 491:
			case 492:
			case 493:
			case 494:
			case 495:
			case 496:
			case 497:
			case 498:
			case 499:
				return Response.FromValue(value: false, response);
			default:
				return new ErrorResponse<bool>(response, new RequestFailedException(response));
			}
		}

		private static (CancellationToken CancellationToken, ErrorOptions ErrorOptions) ApplyRequestContext(RequestContext? requestContext)
		{
			if (requestContext == null)
			{
				return (CancellationToken: CancellationToken.None, ErrorOptions: ErrorOptions.Default);
			}
			return (CancellationToken: requestContext.CancellationToken, ErrorOptions: requestContext.ErrorOptions);
		}
	}
	internal interface IOperationSource<T>
	{
		T CreateResult(Response response, CancellationToken cancellationToken);

		ValueTask<T> CreateResultAsync(Response response, CancellationToken cancellationToken);
	}
	internal interface IUtf8JsonSerializable
	{
		void Write(Utf8JsonWriter writer);
	}
	internal interface IXmlSerializable
	{
		void Write(XmlWriter writer, string? nameHint);
	}
	internal static class JsonElementExtensions
	{
		public static object? GetObject(this in JsonElement element)
		{
			switch (element.ValueKind)
			{
			case JsonValueKind.String:
				return element.GetString();
			case JsonValueKind.Number:
			{
				if (element.TryGetInt32(out var value))
				{
					return value;
				}
				if (element.TryGetInt64(out var value2))
				{
					return value2;
				}
				return element.GetDouble();
			}
			case JsonValueKind.True:
				return true;
			case JsonValueKind.False:
				return false;
			case JsonValueKind.Undefined:
			case JsonValueKind.Null:
				return null;
			case JsonValueKind.Object:
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				{
					foreach (JsonProperty item in element.EnumerateObject())
					{
						dictionary.Add(item.Name, item.Value.GetObject());
					}
					return dictionary;
				}
			}
			case JsonValueKind.Array:
			{
				List<object> list = new List<object>();
				foreach (JsonElement item2 in element.EnumerateArray())
				{
					list.Add(item2.GetObject());
				}
				return list.ToArray();
			}
			default:
				throw new NotSupportedException("Not supported value kind " + element.ValueKind);
			}
		}

		public static byte[]? GetBytesFromBase64(this in JsonElement element, string format)
		{
			if (element.ValueKind == JsonValueKind.Null)
			{
				return null;
			}
			if (!(format == "U"))
			{
				if (format == "D")
				{
					return element.GetBytesFromBase64();
				}
				throw new ArgumentException("Format is not supported: '" + format + "'", "format");
			}
			return TypeFormatters.FromBase64UrlString(element.GetRequiredString());
		}

		public static DateTimeOffset GetDateTimeOffset(this in JsonElement element, string format)
		{
			if (format == "U" && element.ValueKind == JsonValueKind.Number)
			{
				return DateTimeOffset.FromUnixTimeSeconds(element.GetInt64());
			}
			return TypeFormatters.ParseDateTimeOffset(element.GetString(), format);
		}

		public static TimeSpan GetTimeSpan(this in JsonElement element, string format)
		{
			return TypeFormatters.ParseTimeSpan(element.GetString(), format);
		}

		public static char GetChar(this in JsonElement element)
		{
			if (element.ValueKind == JsonValueKind.String)
			{
				string text = element.GetString();
				if (text == null || text.Length != 1)
				{
					throw new NotSupportedException("Cannot convert \"" + text + "\" to a Char");
				}
				return text[0];
			}
			throw new NotSupportedException($"Cannot convert {element.ValueKind} to a Char");
		}

		[Conditional("DEBUG")]
		public static void ThrowNonNullablePropertyIsNull(this JsonProperty property)
		{
			throw new JsonException("A property '" + property.Name + "' defined as non-nullable but received as null from the service. This exception only happens in DEBUG builds of the library and would be ignored in the release build");
		}

		public static string GetRequiredString(this in JsonElement element)
		{
			return element.GetString() ?? throw new InvalidOperationException($"The requested operation requires an element of type 'String', but the target element has type '{element.ValueKind}'.");
		}
	}
	internal class NextLinkOperationImplementation : Azure.Core.IOperation
	{
		private enum HeaderSource
		{
			None,
			OperationLocation,
			AzureAsyncOperation,
			Location
		}

		private class CompletedOperation : Azure.Core.IOperation
		{
			private readonly Azure.Core.OperationState _operationState;

			public CompletedOperation(Azure.Core.OperationState operationState)
			{
				_operationState = operationState;
			}

			public ValueTask<Azure.Core.OperationState> UpdateStateAsync(bool async, CancellationToken cancellationToken)
			{
				return new ValueTask<Azure.Core.OperationState>(_operationState);
			}
		}

		private sealed class OperationToOperationOfT<T> : Azure.Core.IOperation<T>
		{
			private readonly Azure.Core.IOperationSource<T> _operationSource;

			private readonly Azure.Core.IOperation _operation;

			public OperationToOperationOfT(Azure.Core.IOperationSource<T> operationSource, Azure.Core.IOperation operation)
			{
				_operationSource = operationSource;
				_operation = operation;
			}

			public async ValueTask<Azure.Core.OperationState<T>> UpdateStateAsync(bool async, CancellationToken cancellationToken)
			{
				Azure.Core.OperationState state = await _operation.UpdateStateAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (state.HasSucceeded)
				{
					T val = ((!async) ? _operationSource.CreateResult(state.RawResponse, cancellationToken) : (await _operationSource.CreateResultAsync(state.RawResponse, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
					T value = val;
					return Azure.Core.OperationState<T>.Success(state.RawResponse, value);
				}
				if (state.HasCompleted)
				{
					return Azure.Core.OperationState<T>.Failure(state.RawResponse, state.OperationFailedException);
				}
				return Azure.Core.OperationState<T>.Pending(state.RawResponse);
			}
		}

		private const string ApiVersionParam = "api-version";

		private static readonly string[] FailureStates = new string[2] { "failed", "canceled" };

		private static readonly string[] SuccessStates = new string[1] { "succeeded" };

		private readonly HeaderSource _headerSource;

		private readonly bool _originalResponseHasLocation;

		private readonly Uri _startRequestUri;

		private readonly Azure.Core.OperationFinalStateVia _finalStateVia;

		private readonly RequestMethod _requestMethod;

		private readonly HttpPipeline _pipeline;

		private readonly string? _apiVersion;

		private string? _lastKnownLocation;

		private string _nextRequestUri;

		public static Azure.Core.IOperation Create(HttpPipeline pipeline, RequestMethod requestMethod, Uri startRequestUri, Response response, Azure.Core.OperationFinalStateVia finalStateVia, bool skipApiVersionOverride = false, string? apiVersionOverrideValue = null)
		{
			string text = null;
			text = ((apiVersionOverrideValue == null) ? ((!skipApiVersionOverride && TryGetApiVersion(startRequestUri, out var apiVersion)) ? apiVersion.ToString() : null) : apiVersionOverrideValue);
			string nextRequestUri;
			HeaderSource headerSource = GetHeaderSource(requestMethod, startRequestUri, response, text, out nextRequestUri);
			if (headerSource == HeaderSource.None && IsFinalState(response, headerSource, out Azure.Core.OperationState? failureState, out string _))
			{
				return new CompletedOperation(failureState ?? GetOperationStateFromFinalResponse(requestMethod, response));
			}
			string value;
			bool originalResponseHasLocation = response.Headers.TryGetValue("Location", out value);
			return new Azure.Core.NextLinkOperationImplementation(pipeline, requestMethod, startRequestUri, nextRequestUri, headerSource, originalResponseHasLocation, value, finalStateVia, text);
		}

		public static Azure.Core.IOperation<T> Create<T>(Azure.Core.IOperationSource<T> operationSource, HttpPipeline pipeline, RequestMethod requestMethod, Uri startRequestUri, Response response, Azure.Core.OperationFinalStateVia finalStateVia, bool skipApiVersionOverride = false, string? apiVersionOverrideValue = null)
		{
			Azure.Core.IOperation operation = Create(pipeline, requestMethod, startRequestUri, response, finalStateVia, skipApiVersionOverride, apiVersionOverrideValue);
			return new OperationToOperationOfT<T>(operationSource, operation);
		}

		private NextLinkOperationImplementation(HttpPipeline pipeline, RequestMethod requestMethod, Uri startRequestUri, string nextRequestUri, HeaderSource headerSource, bool originalResponseHasLocation, string? lastKnownLocation, Azure.Core.OperationFinalStateVia finalStateVia, string? apiVersion)
		{
			_requestMethod = requestMethod;
			_headerSource = headerSource;
			_startRequestUri = startRequestUri;
			_nextRequestUri = nextRequestUri;
			_originalResponseHasLocation = originalResponseHasLocation;
			_lastKnownLocation = lastKnownLocation;
			_finalStateVia = finalStateVia;
			_pipeline = pipeline;
			_apiVersion = apiVersion;
		}

		public async ValueTask<Azure.Core.OperationState> UpdateStateAsync(bool async, CancellationToken cancellationToken)
		{
			Response response = await GetResponseAsync(async, _nextRequestUri, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			Azure.Core.OperationState? failureState;
			string resourceLocation;
			bool flag = IsFinalState(response, _headerSource, out failureState, out resourceLocation);
			if (failureState.HasValue)
			{
				return failureState.Value;
			}
			if (flag)
			{
				string finalUri = GetFinalUri(resourceLocation);
				Response response2 = ((finalUri == null) ? response : (await GetResponseAsync(async, finalUri, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
				Response response3 = response2;
				return GetOperationStateFromFinalResponse(_requestMethod, response3);
			}
			UpdateNextRequestUri(response.Headers);
			return Azure.Core.OperationState.Pending(response);
		}

		private static Azure.Core.OperationState GetOperationStateFromFinalResponse(RequestMethod requestMethod, Response response)
		{
			switch (response.Status)
			{
			case 201:
				if (!(requestMethod == RequestMethod.Put))
				{
					break;
				}
				goto case 200;
			case 204:
				if (!(requestMethod != RequestMethod.Put) || !(requestMethod != RequestMethod.Patch))
				{
					break;
				}
				goto case 200;
			case 200:
				return Azure.Core.OperationState.Success(response);
			}
			return Azure.Core.OperationState.Failure(response);
		}

		private void UpdateNextRequestUri(ResponseHeaders headers)
		{
			string value;
			bool flag = headers.TryGetValue("Location", out value);
			if (flag)
			{
				_lastKnownLocation = value;
			}
			switch (_headerSource)
			{
			case HeaderSource.OperationLocation:
			{
				if (headers.TryGetValue("Operation-Location", out string value2))
				{
					_nextRequestUri = AppendOrReplaceApiVersion(value2, _apiVersion);
				}
				break;
			}
			case HeaderSource.AzureAsyncOperation:
			{
				if (headers.TryGetValue("Azure-AsyncOperation", out string value3))
				{
					_nextRequestUri = AppendOrReplaceApiVersion(value3, _apiVersion);
				}
				break;
			}
			case HeaderSource.Location:
				if (flag)
				{
					_nextRequestUri = AppendOrReplaceApiVersion(value, _apiVersion);
				}
				break;
			}
		}

		internal static string AppendOrReplaceApiVersion(string uri, string? apiVersion)
		{
			if (!string.IsNullOrEmpty(apiVersion))
			{
				ReadOnlySpan<char> span = MemoryExtensions.AsSpan(uri);
				ReadOnlySpan<char> value = MemoryExtensions.AsSpan("api-version");
				int num = span.IndexOf(value);
				if (num == -1)
				{
					string text = ((span.IndexOf('?') > -1) ? "&" : "?");
					return uri + text + "api-version=" + apiVersion;
				}
				int num2 = num + "api-version".Length;
				ReadOnlySpan<char> span2 = span.Slice(num2);
				bool flag = false;
				if (span2.IndexOf('=') == 0)
				{
					span2 = span2.Slice(1);
					num2++;
					flag = true;
				}
				int num3 = span2.IndexOf('&');
				ReadOnlySpan<char> readOnlySpan = span.Slice(0, num2);
				if (num3 == -1)
				{
					return readOnlySpan.ToString() + (flag ? string.Empty : "=") + apiVersion;
				}
				ReadOnlySpan<char> readOnlySpan2 = span.Slice(num3 + num2);
				return readOnlySpan.ToString() + (flag ? string.Empty : "=") + apiVersion + readOnlySpan2;
			}
			return uri;
		}

		internal static bool TryGetApiVersion(Uri startRequestUri, out ReadOnlySpan<char> apiVersion)
		{
			apiVersion = default(ReadOnlySpan<char>);
			ReadOnlySpan<char> span = MemoryExtensions.AsSpan(startRequestUri.Query);
			int num = span.IndexOf(MemoryExtensions.AsSpan("api-version"));
			if (num == -1)
			{
				return false;
			}
			num += "api-version".Length;
			ReadOnlySpan<char> span2 = span.Slice(num);
			if (span2.IndexOf('=') == 0)
			{
				span2 = span2.Slice(1);
				num++;
				int num2 = span2.IndexOf('&');
				int length = ((num2 == -1) ? (span.Length - num) : num2);
				apiVersion = span.Slice(num, length);
				return true;
			}
			return false;
		}

		private string? GetFinalUri(string? resourceLocation)
		{
			HeaderSource headerSource = _headerSource;
			if (headerSource != HeaderSource.OperationLocation && headerSource != HeaderSource.AzureAsyncOperation)
			{
				return null;
			}
			if (_requestMethod == RequestMethod.Delete)
			{
				return null;
			}
			switch (_finalStateVia)
			{
			case Azure.Core.OperationFinalStateVia.LocationOverride:
				if (_originalResponseHasLocation)
				{
					return _lastKnownLocation;
				}
				break;
			case Azure.Core.OperationFinalStateVia.AzureAsyncOperation:
			case Azure.Core.OperationFinalStateVia.OperationLocation:
				if (_requestMethod == RequestMethod.Post)
				{
					return null;
				}
				break;
			case Azure.Core.OperationFinalStateVia.OriginalUri:
				return _startRequestUri.AbsoluteUri;
			}
			if (resourceLocation != null)
			{
				return resourceLocation;
			}
			if (_requestMethod == RequestMethod.Put || _requestMethod == RequestMethod.Patch)
			{
				return _startRequestUri.AbsoluteUri;
			}
			if (_originalResponseHasLocation)
			{
				return _lastKnownLocation;
			}
			return null;
		}

		private async ValueTask<Response> GetResponseAsync(bool async, string uri, CancellationToken cancellationToken)
		{
			using HttpMessage message = CreateRequest(uri);
			if (async)
			{
				await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				_pipeline.Send(message, cancellationToken);
			}
			return message.Response;
		}

		private HttpMessage CreateRequest(string uri)
		{
			HttpMessage httpMessage = _pipeline.CreateMessage();
			Request request = httpMessage.Request;
			request.Method = RequestMethod.Get;
			if (Uri.TryCreate(uri, UriKind.Absolute, out var result) && result.Scheme != "file")
			{
				request.Uri.Reset(result);
				return httpMessage;
			}
			request.Uri.Reset(new Uri(_startRequestUri, uri));
			return httpMessage;
		}

		private static bool IsFinalState(Response response, HeaderSource headerSource, out Azure.Core.OperationState? failureState, out string? resourceLocation)
		{
			failureState = null;
			resourceLocation = null;
			if (headerSource == HeaderSource.Location)
			{
				return response.Status != 202;
			}
			int status = response.Status;
			if (status >= 200 && status <= 204)
			{
				Stream contentStream = response.ContentStream;
				if (contentStream != null && contentStream.Length > 0)
				{
					try
					{
						using JsonDocument jsonDocument = JsonDocument.Parse(response.ContentStream);
						JsonElement rootElement = jsonDocument.RootElement;
						JsonElement value;
						switch (headerSource)
						{
						default:
							goto end_IL_006c;
						case HeaderSource.None:
						{
							if (rootElement.TryGetProperty("properties", out var value2) && value2.TryGetProperty("provisioningState", out value))
							{
								break;
							}
							goto end_IL_006c;
						}
						case HeaderSource.OperationLocation:
							if (rootElement.TryGetProperty("status", out value))
							{
								break;
							}
							goto end_IL_006c;
						case HeaderSource.AzureAsyncOperation:
							if (rootElement.TryGetProperty("status", out value))
							{
								break;
							}
							goto end_IL_006c;
						}
						string value3 = value.GetRequiredString().ToLowerInvariant();
						if (FailureStates.Contains<string>(value3))
						{
							failureState = Azure.Core.OperationState.Failure(response);
							return true;
						}
						if (!SuccessStates.Contains<string>(value3))
						{
							return false;
						}
						if ((headerSource == HeaderSource.OperationLocation || headerSource == HeaderSource.AzureAsyncOperation) && rootElement.TryGetProperty("resourceLocation", out var value4))
						{
							resourceLocation = value4.GetString();
						}
						return true;
						end_IL_006c:;
					}
					finally
					{
						response.ContentStream.Position = 0L;
					}
				}
				if (headerSource == HeaderSource.None)
				{
					return true;
				}
			}
			failureState = Azure.Core.OperationState.Failure(response);
			return true;
		}

		private static bool ShouldIgnoreHeader(RequestMethod method, Response response)
		{
			if (method.Method == RequestMethod.Patch.Method)
			{
				return response.Status == 200;
			}
			return false;
		}

		private static HeaderSource GetHeaderSource(RequestMethod requestMethod, Uri requestUri, Response response, string? apiVersion, out string nextRequestUri)
		{
			if (ShouldIgnoreHeader(requestMethod, response))
			{
				nextRequestUri = requestUri.AbsoluteUri;
				return HeaderSource.None;
			}
			ResponseHeaders headers = response.Headers;
			if (headers.TryGetValue("Operation-Location", out string value))
			{
				nextRequestUri = AppendOrReplaceApiVersion(value, apiVersion);
				return HeaderSource.OperationLocation;
			}
			if (headers.TryGetValue("Azure-AsyncOperation", out string value2))
			{
				nextRequestUri = AppendOrReplaceApiVersion(value2, apiVersion);
				return HeaderSource.AzureAsyncOperation;
			}
			if (headers.TryGetValue("Location", out string value3))
			{
				nextRequestUri = AppendOrReplaceApiVersion(value3, apiVersion);
				return HeaderSource.Location;
			}
			nextRequestUri = requestUri.AbsoluteUri;
			return HeaderSource.None;
		}
	}
	internal enum OperationFinalStateVia
	{
		AzureAsyncOperation,
		Location,
		OriginalUri,
		OperationLocation,
		LocationOverride
	}
	internal static class OperationHelpers
	{
		public static T GetValue<T>(ref T? value) where T : class
		{
			if (value == null)
			{
				throw new InvalidOperationException("The operation has not completed yet.");
			}
			return value;
		}

		public static T GetValue<T>(ref T? value) where T : struct
		{
			if (!value.HasValue)
			{
				throw new InvalidOperationException("The operation has not completed yet.");
			}
			return value.Value;
		}

		public static async ValueTask<Response<TResult>> DefaultWaitForCompletionAsync<TResult>(this Operation<TResult> operation, CancellationToken cancellationToken) where TResult : notnull
		{
			return await new Azure.Core.OperationPoller().WaitForCompletionAsync(operation, null, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public static async ValueTask<Response<TResult>> DefaultWaitForCompletionAsync<TResult>(this Operation<TResult> operation, TimeSpan pollingInterval, CancellationToken cancellationToken) where TResult : notnull
		{
			return await new Azure.Core.OperationPoller().WaitForCompletionAsync(operation, pollingInterval, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public static Response<TResult> DefaultWaitForCompletion<TResult>(this Operation<TResult> operation, CancellationToken cancellationToken) where TResult : notnull
		{
			return new Azure.Core.OperationPoller().WaitForCompletion(operation, null, cancellationToken);
		}

		public static Response<TResult> DefaultWaitForCompletion<TResult>(this Operation<TResult> operation, TimeSpan pollingInterval, CancellationToken cancellationToken) where TResult : notnull
		{
			return new Azure.Core.OperationPoller().WaitForCompletion(operation, pollingInterval, cancellationToken);
		}

		public static async ValueTask<Response> DefaultWaitForCompletionResponseAsync(this Operation operation, CancellationToken cancellationToken)
		{
			return await new Azure.Core.OperationPoller().WaitForCompletionResponseAsync(operation, null, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public static async ValueTask<Response> DefaultWaitForCompletionResponseAsync(this Operation operation, TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			return await new Azure.Core.OperationPoller().WaitForCompletionResponseAsync(operation, pollingInterval, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public static Response DefaultWaitForCompletionResponse(this Operation operation, CancellationToken cancellationToken)
		{
			return new Azure.Core.OperationPoller().WaitForCompletionResponse(operation, null, cancellationToken);
		}

		public static Response DefaultWaitForCompletionResponse(this Operation operation, TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			return new Azure.Core.OperationPoller().WaitForCompletionResponse(operation, pollingInterval, cancellationToken);
		}
	}
	internal class OperationInternal : Azure.Core.OperationInternalBase
	{
		private class OperationToOperationOfTProxy : Azure.Core.IOperation<Azure.Core.VoidValue>
		{
			private readonly Azure.Core.IOperation _operation;

			public OperationToOperationOfTProxy(Azure.Core.IOperation operation)
			{
				_operation = operation;
			}

			public async ValueTask<Azure.Core.OperationState<Azure.Core.VoidValue>> UpdateStateAsync(bool async, CancellationToken cancellationToken)
			{
				Azure.Core.OperationState operationState = await _operation.UpdateStateAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (!operationState.HasCompleted)
				{
					return Azure.Core.OperationState<Azure.Core.VoidValue>.Pending(operationState.RawResponse);
				}
				if (operationState.HasSucceeded)
				{
					return Azure.Core.OperationState<Azure.Core.VoidValue>.Success(operationState.RawResponse, default(Azure.Core.VoidValue));
				}
				return Azure.Core.OperationState<Azure.Core.VoidValue>.Failure(operationState.RawResponse, operationState.OperationFailedException);
			}
		}

		private readonly Azure.Core.OperationInternal<Azure.Core.VoidValue> _internalOperation;

		public override Response RawResponse => _internalOperation.RawResponse;

		public override bool HasCompleted => _internalOperation.HasCompleted;

		public static Azure.Core.OperationInternal Succeeded(Response rawResponse)
		{
			return new Azure.Core.OperationInternal(Azure.Core.OperationState.Success(rawResponse));
		}

		public static Azure.Core.OperationInternal Failed(Response rawResponse, RequestFailedException operationFailedException)
		{
			return new Azure.Core.OperationInternal(Azure.Core.OperationState.Failure(rawResponse, operationFailedException));
		}

		public OperationInternal(Azure.Core.IOperation operation, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, Response rawResponse, string? operationTypeName = null, IEnumerable<KeyValuePair<string, string>>? scopeAttributes = null, DelayStrategy? fallbackStrategy = null)
			: base(clientDiagnostics, operationTypeName ?? operation.GetType().Name, scopeAttributes, fallbackStrategy)
		{
			_internalOperation = new Azure.Core.OperationInternal<Azure.Core.VoidValue>(new OperationToOperationOfTProxy(operation), clientDiagnostics, rawResponse, operationTypeName ?? operation.GetType().Name, scopeAttributes, fallbackStrategy);
		}

		private OperationInternal(Azure.Core.OperationState finalState)
			: base(finalState.RawResponse)
		{
			_internalOperation = (finalState.HasSucceeded ? Azure.Core.OperationInternal<Azure.Core.VoidValue>.Succeeded(finalState.RawResponse, default(Azure.Core.VoidValue)) : Azure.Core.OperationInternal<Azure.Core.VoidValue>.Failed(finalState.RawResponse, finalState.OperationFailedException));
		}

		protected override async ValueTask<Response> UpdateStatusAsync(bool async, CancellationToken cancellationToken)
		{
			return (!async) ? _internalOperation.UpdateStatus(cancellationToken) : (await _internalOperation.UpdateStatusAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}
	}
	internal interface IOperation
	{
		ValueTask<Azure.Core.OperationState> UpdateStateAsync(bool async, CancellationToken cancellationToken);
	}
	internal readonly struct OperationState
	{
		public Response RawResponse { get; }

		public bool HasCompleted { get; }

		public bool HasSucceeded { get; }

		public RequestFailedException? OperationFailedException { get; }

		private OperationState(Response rawResponse, bool hasCompleted, bool hasSucceeded, RequestFailedException? operationFailedException)
		{
			RawResponse = rawResponse;
			HasCompleted = hasCompleted;
			HasSucceeded = hasSucceeded;
			OperationFailedException = operationFailedException;
		}

		public static Azure.Core.OperationState Success(Response rawResponse)
		{
			Azure.Core.Argument.AssertNotNull(rawResponse, "rawResponse");
			return new Azure.Core.OperationState(rawResponse, hasCompleted: true, hasSucceeded: true, null);
		}

		public static Azure.Core.OperationState Failure(Response rawResponse, RequestFailedException? operationFailedException = null)
		{
			Azure.Core.Argument.AssertNotNull(rawResponse, "rawResponse");
			return new Azure.Core.OperationState(rawResponse, hasCompleted: true, hasSucceeded: false, operationFailedException);
		}

		public static Azure.Core.OperationState Pending(Response rawResponse)
		{
			Azure.Core.Argument.AssertNotNull(rawResponse, "rawResponse");
			return new Azure.Core.OperationState(rawResponse, hasCompleted: false, hasSucceeded: false, null);
		}
	}
	internal abstract class OperationInternalBase
	{
		private readonly Azure.Core.Pipeline.ClientDiagnostics _diagnostics;

		private readonly IReadOnlyDictionary<string, string>? _scopeAttributes;

		private readonly DelayStrategy? _fallbackStrategy;

		private readonly Azure.Core.AsyncLockWithValue<Response> _responseLock;

		private readonly string _waitForCompletionResponseScopeName;

		protected readonly string _updateStatusScopeName;

		protected readonly string _waitForCompletionScopeName;

		public abstract Response RawResponse { get; }

		public abstract bool HasCompleted { get; }

		protected OperationInternalBase(Response rawResponse)
		{
			_diagnostics = new Azure.Core.Pipeline.ClientDiagnostics(ClientOptions.Default);
			_updateStatusScopeName = string.Empty;
			_waitForCompletionResponseScopeName = string.Empty;
			_waitForCompletionScopeName = string.Empty;
			_scopeAttributes = null;
			_fallbackStrategy = null;
			_responseLock = new Azure.Core.AsyncLockWithValue<Response>(rawResponse);
		}

		protected OperationInternalBase(Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, string operationTypeName, IEnumerable<KeyValuePair<string, string>>? scopeAttributes = null, DelayStrategy? fallbackStrategy = null)
		{
			_diagnostics = clientDiagnostics;
			_updateStatusScopeName = operationTypeName + ".UpdateStatus";
			_waitForCompletionResponseScopeName = operationTypeName + ".WaitForCompletionResponse";
			_waitForCompletionScopeName = operationTypeName + ".WaitForCompletion";
			_scopeAttributes = scopeAttributes?.ToDictionary<KeyValuePair<string, string>, string, string>((KeyValuePair<string, string> kvp) => kvp.Key, (KeyValuePair<string, string> kvp) => kvp.Value);
			_fallbackStrategy = fallbackStrategy;
			_responseLock = new Azure.Core.AsyncLockWithValue<Response>();
		}

		public async ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken)
		{
			return await UpdateStatusAsync(async: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public Response UpdateStatus(CancellationToken cancellationToken)
		{
			return UpdateStatusAsync(async: false, cancellationToken).EnsureCompleted();
		}

		public async ValueTask<Response> WaitForCompletionResponseAsync(CancellationToken cancellationToken)
		{
			return await WaitForCompletionResponseAsync(async: true, null, _waitForCompletionResponseScopeName, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public async ValueTask<Response> WaitForCompletionResponseAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			return await WaitForCompletionResponseAsync(async: true, pollingInterval, _waitForCompletionResponseScopeName, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public Response WaitForCompletionResponse(CancellationToken cancellationToken)
		{
			return WaitForCompletionResponseAsync(async: false, null, _waitForCompletionResponseScopeName, cancellationToken).EnsureCompleted();
		}

		public Response WaitForCompletionResponse(TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			return WaitForCompletionResponseAsync(async: false, pollingInterval, _waitForCompletionResponseScopeName, cancellationToken).EnsureCompleted();
		}

		protected async ValueTask<Response> WaitForCompletionResponseAsync(bool async, TimeSpan? pollingInterval, string scopeName, CancellationToken cancellationToken)
		{
			using Azure.Core.AsyncLockWithValue<Response>.LockOrValue lockOrValue = await _responseLock.GetLockOrValueAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (lockOrValue.HasValue)
			{
				return lockOrValue.Value;
			}
			using Azure.Core.Pipeline.DiagnosticScope scope = CreateScope(scopeName);
			_ = 1;
			try
			{
				Azure.Core.OperationPoller operationPoller = new Azure.Core.OperationPoller(_fallbackStrategy);
				Response response = ((!async) ? operationPoller.WaitForCompletionResponse(this, pollingInterval, cancellationToken) : (await operationPoller.WaitForCompletionResponseAsync(this, pollingInterval, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
				Response response2 = response;
				lockOrValue.SetValue(response2);
				return response2;
			}
			catch (Exception exception)
			{
				scope.Failed(exception);
				throw;
			}
		}

		protected abstract ValueTask<Response> UpdateStatusAsync(bool async, CancellationToken cancellationToken);

		protected Azure.Core.Pipeline.DiagnosticScope CreateScope(string scopeName)
		{
			Azure.Core.Pipeline.DiagnosticScope result = _diagnostics.CreateScope(scopeName);
			if (_scopeAttributes != null)
			{
				foreach (KeyValuePair<string, string> scopeAttribute in _scopeAttributes)
				{
					result.AddAttribute(scopeAttribute.Key, scopeAttribute.Value);
				}
			}
			result.Start();
			return result;
		}
	}
	internal class OperationInternal<T> : Azure.Core.OperationInternalBase
	{
		private class FinalOperation : Azure.Core.IOperation<T>
		{
			public ValueTask<Azure.Core.OperationState<T>> UpdateStateAsync(bool async, CancellationToken cancellationToken)
			{
				throw new NotSupportedException("The operation has already completed");
			}
		}

		private readonly Azure.Core.IOperation<T> _operation;

		private readonly Azure.Core.AsyncLockWithValue<Azure.Core.OperationState<T>> _stateLock;

		private Response _rawResponse;

		public override Response RawResponse
		{
			get
			{
				if (!_stateLock.TryGetValue(out var value))
				{
					return _rawResponse;
				}
				return value.RawResponse;
			}
		}

		public override bool HasCompleted => _stateLock.HasValue;

		public bool HasValue
		{
			get
			{
				if (_stateLock.TryGetValue(out var value))
				{
					return value.HasSucceeded;
				}
				return false;
			}
		}

		public T Value
		{
			get
			{
				if (_stateLock.TryGetValue(out var value))
				{
					if (value.HasSucceeded)
					{
						return value.Value;
					}
					throw value.OperationFailedException;
				}
				throw new InvalidOperationException("The operation has not completed yet.");
			}
		}

		public static Azure.Core.OperationInternal<T> Succeeded(Response rawResponse, T value)
		{
			return new Azure.Core.OperationInternal<T>(Azure.Core.OperationState<T>.Success(rawResponse, value));
		}

		public static Azure.Core.OperationInternal<T> Failed(Response rawResponse, RequestFailedException operationFailedException)
		{
			return new Azure.Core.OperationInternal<T>(Azure.Core.OperationState<T>.Failure(rawResponse, operationFailedException));
		}

		public OperationInternal(Azure.Core.IOperation<T> operation, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, Response rawResponse, string? operationTypeName = null, IEnumerable<KeyValuePair<string, string>>? scopeAttributes = null, DelayStrategy? fallbackStrategy = null)
			: base(clientDiagnostics, operationTypeName ?? operation.GetType().Name, scopeAttributes, fallbackStrategy)
		{
			_operation = operation;
			_rawResponse = rawResponse;
			_stateLock = new Azure.Core.AsyncLockWithValue<Azure.Core.OperationState<T>>();
		}

		private OperationInternal(Azure.Core.OperationState<T> finalState)
			: base(finalState.RawResponse)
		{
			_operation = new FinalOperation();
			_rawResponse = finalState.RawResponse;
			_stateLock = new Azure.Core.AsyncLockWithValue<Azure.Core.OperationState<T>>(finalState);
		}

		public async ValueTask<Response<T>> WaitForCompletionAsync(CancellationToken cancellationToken)
		{
			return await WaitForCompletionAsync(async: true, null, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public async ValueTask<Response<T>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			return await WaitForCompletionAsync(async: true, pollingInterval, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public Response<T> WaitForCompletion(CancellationToken cancellationToken)
		{
			return WaitForCompletionAsync(async: false, null, cancellationToken).EnsureCompleted();
		}

		public Response<T> WaitForCompletion(TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			return WaitForCompletionAsync(async: false, pollingInterval, cancellationToken).EnsureCompleted();
		}

		private async ValueTask<Response<T>> WaitForCompletionAsync(bool async, TimeSpan? pollingInterval, CancellationToken cancellationToken)
		{
			Response response = await WaitForCompletionResponseAsync(async, pollingInterval, _waitForCompletionScopeName, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return Response.FromValue(Value, response);
		}

		protected override async ValueTask<Response> UpdateStatusAsync(bool async, CancellationToken cancellationToken)
		{
			using Azure.Core.AsyncLockWithValue<Azure.Core.OperationState<T>>.LockOrValue asyncLock = await _stateLock.GetLockOrValueAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (asyncLock.HasValue)
			{
				return GetResponseFromState(asyncLock.Value);
			}
			using Azure.Core.Pipeline.DiagnosticScope scope = CreateScope(_updateStatusScopeName);
			_ = 1;
			try
			{
				Azure.Core.OperationState<T> operationState = await _operation.UpdateStateAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (!operationState.HasCompleted)
				{
					Interlocked.Exchange(ref _rawResponse, operationState.RawResponse);
					return operationState.RawResponse;
				}
				if (!operationState.HasSucceeded && operationState.OperationFailedException == null)
				{
					operationState = Azure.Core.OperationState<T>.Failure(operationState.RawResponse, new RequestFailedException(operationState.RawResponse));
				}
				asyncLock.SetValue(operationState);
				return GetResponseFromState(operationState);
			}
			catch (Exception exception)
			{
				scope.Failed(exception);
				throw;
			}
		}

		private static Response GetResponseFromState(Azure.Core.OperationState<T> state)
		{
			if (state.HasSucceeded)
			{
				return state.RawResponse;
			}
			throw state.OperationFailedException;
		}
	}
	internal interface IOperation<T>
	{
		ValueTask<Azure.Core.OperationState<T>> UpdateStateAsync(bool async, CancellationToken cancellationToken);
	}
	internal readonly struct OperationState<T>
	{
		public Response RawResponse { get; }

		public bool HasCompleted { get; }

		public bool HasSucceeded { get; }

		public T? Value { get; }

		public RequestFailedException? OperationFailedException { get; }

		private OperationState(Response rawResponse, bool hasCompleted, bool hasSucceeded, T? value, RequestFailedException? operationFailedException)
		{
			RawResponse = rawResponse;
			HasCompleted = hasCompleted;
			HasSucceeded = hasSucceeded;
			Value = value;
			OperationFailedException = operationFailedException;
		}

		public static Azure.Core.OperationState<T> Success(Response rawResponse, T value)
		{
			Azure.Core.Argument.AssertNotNull(rawResponse, "rawResponse");
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return new Azure.Core.OperationState<T>(rawResponse, hasCompleted: true, hasSucceeded: true, value, null);
		}

		public static Azure.Core.OperationState<T> Failure(Response rawResponse, RequestFailedException? operationFailedException = null)
		{
			Azure.Core.Argument.AssertNotNull(rawResponse, "rawResponse");
			return new Azure.Core.OperationState<T>(rawResponse, hasCompleted: true, hasSucceeded: false, default(T), operationFailedException);
		}

		public static Azure.Core.OperationState<T> Pending(Response rawResponse)
		{
			Azure.Core.Argument.AssertNotNull(rawResponse, "rawResponse");
			return new Azure.Core.OperationState<T>(rawResponse, hasCompleted: false, hasSucceeded: false, default(T), null);
		}
	}
	internal sealed class OperationPoller
	{
		private readonly DelayStrategy _delayStrategy;

		public OperationPoller(DelayStrategy? strategy = null)
		{
			_delayStrategy = strategy ?? new Azure.Core.FixedDelayWithNoJitterStrategy();
		}

		public ValueTask<Response> WaitForCompletionResponseAsync(Operation operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			return WaitForCompletionAsync(async: true, operation, delayHint, cancellationToken);
		}

		public Response WaitForCompletionResponse(Operation operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			return WaitForCompletionAsync(async: false, operation, delayHint, cancellationToken).EnsureCompleted();
		}

		public ValueTask<Response> WaitForCompletionResponseAsync(Azure.Core.OperationInternalBase operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			return WaitForCompletionAsync(async: true, operation, delayHint, cancellationToken);
		}

		public Response WaitForCompletionResponse(Azure.Core.OperationInternalBase operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			return WaitForCompletionAsync(async: false, operation, delayHint, cancellationToken).EnsureCompleted();
		}

		public async ValueTask<Response<T>> WaitForCompletionAsync<T>(Operation<T> operation, TimeSpan? delayHint, CancellationToken cancellationToken) where T : notnull
		{
			Response response = await WaitForCompletionAsync(async: true, operation, delayHint, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return Response.FromValue(operation.Value, response);
		}

		public Response<T> WaitForCompletion<T>(Operation<T> operation, TimeSpan? delayHint, CancellationToken cancellationToken) where T : notnull
		{
			Response response = WaitForCompletionAsync(async: false, operation, delayHint, cancellationToken).EnsureCompleted();
			return Response.FromValue(operation.Value, response);
		}

		public async ValueTask<Response<T>> WaitForCompletionAsync<T>(Azure.Core.OperationInternal<T> operation, TimeSpan? delayHint, CancellationToken cancellationToken) where T : notnull
		{
			Response response = await WaitForCompletionAsync(async: true, operation, delayHint, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return Response.FromValue(operation.Value, response);
		}

		public Response<T> WaitForCompletion<T>(Azure.Core.OperationInternal<T> operation, TimeSpan? delayHint, CancellationToken cancellationToken) where T : notnull
		{
			Response response = WaitForCompletionAsync(async: false, operation, delayHint, cancellationToken).EnsureCompleted();
			return Response.FromValue(operation.Value, response);
		}

		private async ValueTask<Response> WaitForCompletionAsync(bool async, Operation operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			int retryNumber = 0;
			while (true)
			{
				Response response = ((!async) ? operation.UpdateStatus(cancellationToken) : (await operation.UpdateStatusAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
				Response response2 = response;
				if (operation.HasCompleted)
				{
					break;
				}
				DelayStrategy delayStrategy = (delayHint.HasValue ? new Azure.Core.FixedDelayWithNoJitterStrategy(delayHint.Value) : _delayStrategy);
				int num = retryNumber + 1;
				retryNumber = num;
				await Delay(async, delayStrategy.GetNextDelay(response2, num), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			return operation.GetRawResponse();
		}

		private async ValueTask<Response> WaitForCompletionAsync(bool async, Azure.Core.OperationInternalBase operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			int retryNumber = 0;
			while (true)
			{
				Response response = ((!async) ? operation.UpdateStatus(cancellationToken) : (await operation.UpdateStatusAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
				Response response2 = response;
				if (operation.HasCompleted)
				{
					break;
				}
				DelayStrategy delayStrategy = (delayHint.HasValue ? new Azure.Core.FixedDelayWithNoJitterStrategy(delayHint.Value) : _delayStrategy);
				int num = retryNumber + 1;
				retryNumber = num;
				await Delay(async, delayStrategy.GetNextDelay(response2, num), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			return operation.RawResponse;
		}

		private static async ValueTask Delay(bool async, TimeSpan delay, CancellationToken cancellationToken)
		{
			if (async)
			{
				await Task.Delay(delay, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			else if (cancellationToken.CanBeCanceled)
			{
				if (cancellationToken.WaitHandle.WaitOne(delay))
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
			}
			else
			{
				Thread.Sleep(delay);
			}
		}
	}
	internal static class Optional
	{
		public static bool IsCollectionDefined<T>(IEnumerable<T> collection)
		{
			if (collection is ChangeTrackingList<T> changeTrackingList)
			{
				return !changeTrackingList.IsUndefined;
			}
			return true;
		}

		public static bool IsCollectionDefined<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> collection)
		{
			if (collection is ChangeTrackingDictionary<TKey, TValue> changeTrackingDictionary)
			{
				return !changeTrackingDictionary.IsUndefined;
			}
			return true;
		}

		public static bool IsCollectionDefined<TKey, TValue>(IDictionary<TKey, TValue> collection)
		{
			if (collection is ChangeTrackingDictionary<TKey, TValue> changeTrackingDictionary)
			{
				return !changeTrackingDictionary.IsUndefined;
			}
			return true;
		}

		public static bool IsDefined<T>(T? value) where T : struct
		{
			return value.HasValue;
		}

		public static bool IsDefined(object value)
		{
			return value != null;
		}

		public static bool IsDefined(string value)
		{
			return value != null;
		}

		public static bool IsDefined(JsonElement value)
		{
			return value.ValueKind != JsonValueKind.Undefined;
		}

		public static IReadOnlyDictionary<TKey, TValue> ToDictionary<TKey, TValue>(Optional<IReadOnlyDictionary<TKey, TValue>> optional)
		{
			if (optional.HasValue)
			{
				return optional.Value;
			}
			return new ChangeTrackingDictionary<TKey, TValue>(optional);
		}

		public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(Optional<IDictionary<TKey, TValue>> optional)
		{
			if (optional.HasValue)
			{
				return optional.Value;
			}
			return new ChangeTrackingDictionary<TKey, TValue>(optional);
		}

		public static IReadOnlyList<T> ToList<T>(Optional<IReadOnlyList<T>> optional)
		{
			if (optional.HasValue)
			{
				return optional.Value;
			}
			return new ChangeTrackingList<T>(optional);
		}

		public static IList<T> ToList<T>(Optional<IList<T>> optional)
		{
			if (optional.HasValue)
			{
				return optional.Value;
			}
			return new ChangeTrackingList<T>(optional);
		}

		public static T? ToNullable<T>(Optional<T> optional) where T : struct
		{
			if (optional.HasValue)
			{
				return optional.Value;
			}
			return null;
		}

		public static T? ToNullable<T>(Optional<T?> optional) where T : struct
		{
			return optional.Value;
		}
	}
	internal readonly struct Optional<T>
	{
		public T Value { get; }

		public bool HasValue { get; }

		public Optional(T value)
		{
			this = default(Optional<T>);
			Value = value;
			HasValue = true;
		}

		public static implicit operator Optional<T>(T value)
		{
			return new Optional<T>(value);
		}

		public static implicit operator T(Optional<T> optional)
		{
			return optional.Value;
		}
	}
	internal static class Page
	{
		public static Page<T> FromValues<T>(IEnumerable<T> values, string continuationToken, Response response)
		{
			return Page<T>.FromValues(values.ToList(), continuationToken, response);
		}
	}
	internal static class PageableHelpers
	{
		internal class FuncAsyncPageable<T> : AsyncPageable<T> where T : notnull
		{
			private readonly Func<string?, int?, Task<Page<T>>> _firstPageFunc;

			private readonly Func<string?, int?, Task<Page<T>>>? _nextPageFunc;

			private readonly int? _defaultPageSize;

			public FuncAsyncPageable(Func<string?, int?, Task<Page<T>>> firstPageFunc, Func<string?, int?, Task<Page<T>>>? nextPageFunc, int? defaultPageSize = null)
			{
				_firstPageFunc = firstPageFunc;
				_nextPageFunc = nextPageFunc;
				_defaultPageSize = defaultPageSize;
			}

			public override async IAsyncEnumerable<Page<T>> AsPages(string? continuationToken = null, int? pageSizeHint = null)
			{
				Func<string, int?, Task<Page<T>>> func = (string.IsNullOrEmpty(continuationToken) ? _firstPageFunc : _nextPageFunc);
				if (func != null)
				{
					int? pageSize = pageSizeHint ?? _defaultPageSize;
					do
					{
						Page<T> pageResponse = await func(continuationToken, pageSize).ConfigureAwait(continueOnCapturedContext: false);
						yield return pageResponse;
						continuationToken = pageResponse.ContinuationToken;
						func = _nextPageFunc;
					}
					while (!string.IsNullOrEmpty(continuationToken) && func != null);
				}
			}
		}

		internal class FuncPageable<T> : Pageable<T> where T : notnull
		{
			private readonly Func<string?, int?, Page<T>> _firstPageFunc;

			private readonly Func<string?, int?, Page<T>>? _nextPageFunc;

			private readonly int? _defaultPageSize;

			public FuncPageable(Func<string?, int?, Page<T>> firstPageFunc, Func<string?, int?, Page<T>>? nextPageFunc, int? defaultPageSize = null)
			{
				_firstPageFunc = firstPageFunc;
				_nextPageFunc = nextPageFunc;
				_defaultPageSize = defaultPageSize;
			}

			public override IEnumerable<Page<T>> AsPages(string? continuationToken = null, int? pageSizeHint = null)
			{
				Func<string, int?, Page<T>> func = (string.IsNullOrEmpty(continuationToken) ? _firstPageFunc : _nextPageFunc);
				if (func != null)
				{
					int? pageSize = pageSizeHint ?? _defaultPageSize;
					do
					{
						Page<T> pageResponse = func(continuationToken, pageSize);
						yield return pageResponse;
						continuationToken = pageResponse.ContinuationToken;
						func = _nextPageFunc;
					}
					while (!string.IsNullOrEmpty(continuationToken) && func != null);
				}
			}
		}

		internal class AsyncPageableWrapper<T> : AsyncPageable<T> where T : notnull
		{
			private readonly PageableImplementation<T> _implementation;

			public AsyncPageableWrapper(PageableImplementation<T> implementation)
			{
				_implementation = implementation;
			}

			public override IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
			{
				return _implementation.GetAsyncEnumerator(cancellationToken);
			}

			public override IAsyncEnumerable<Page<T>> AsPages(string? continuationToken = null, int? pageSizeHint = null)
			{
				return _implementation.AsPagesAsync(continuationToken, pageSizeHint, default(CancellationToken));
			}
		}

		internal class PageableWrapper<T> : Pageable<T> where T : notnull
		{
			private readonly PageableImplementation<T> _implementation;

			public PageableWrapper(PageableImplementation<T> implementation)
			{
				_implementation = implementation;
			}

			public override IEnumerator<T> GetEnumerator()
			{
				return _implementation.GetEnumerator();
			}

			public override IEnumerable<Page<T>> AsPages(string? continuationToken = null, int? pageSizeHint = null)
			{
				return _implementation.AsPages(continuationToken, pageSizeHint);
			}
		}

		internal class PageableImplementation<T>
		{
			private readonly Response? _initialResponse;

			private readonly Func<int?, HttpMessage>? _createFirstPageRequest;

			private readonly Func<int?, string, HttpMessage>? _createNextPageRequest;

			private readonly HttpPipeline _pipeline;

			private readonly Azure.Core.Pipeline.ClientDiagnostics _clientDiagnostics;

			private readonly Func<JsonElement, T>? _valueFactory;

			private readonly Func<Response, (List<T>? Values, string? NextLink)>? _responseParser;

			private readonly string _scopeName;

			private readonly byte[] _itemPropertyName;

			private readonly byte[] _nextLinkPropertyName;

			private readonly int? _defaultPageSize;

			private readonly CancellationToken _cancellationToken;

			private readonly ErrorOptions? _errorOptions;

			public PageableImplementation(Response? initialResponse, Func<int?, HttpMessage>? createFirstPageRequest, Func<int?, string, HttpMessage>? createNextPageRequest, Func<JsonElement, T> valueFactory, HttpPipeline pipeline, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, string scopeName, string? itemPropertyName, string? nextLinkPropertyName, int? defaultPageSize, CancellationToken? cancellationToken, ErrorOptions? errorOptions)
			{
				_initialResponse = initialResponse;
				_createFirstPageRequest = createFirstPageRequest;
				_createNextPageRequest = createNextPageRequest;
				_valueFactory = ((typeof(T) == typeof(BinaryData)) ? null : valueFactory);
				_responseParser = null;
				_pipeline = pipeline;
				_clientDiagnostics = clientDiagnostics;
				_scopeName = scopeName;
				_itemPropertyName = ((itemPropertyName != null) ? Encoding.UTF8.GetBytes(itemPropertyName) : DefaultItemPropertyName);
				_nextLinkPropertyName = ((nextLinkPropertyName != null) ? Encoding.UTF8.GetBytes(nextLinkPropertyName) : DefaultNextLinkPropertyName);
				_defaultPageSize = defaultPageSize;
				_cancellationToken = cancellationToken.GetValueOrDefault();
				_errorOptions = errorOptions.GetValueOrDefault();
			}

			public PageableImplementation(Func<int?, HttpMessage>? createFirstPageRequest, Func<int?, string, HttpMessage>? createNextPageRequest, Func<Response, (List<T>? Values, string? NextLink)> responseParser, HttpPipeline pipeline, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, string scopeName, int? defaultPageSize, RequestContext? requestContext)
			{
				_createFirstPageRequest = createFirstPageRequest;
				_createNextPageRequest = createNextPageRequest;
				_valueFactory = null;
				_responseParser = responseParser;
				_pipeline = pipeline;
				_clientDiagnostics = clientDiagnostics;
				_scopeName = scopeName;
				_itemPropertyName = Array.Empty<byte>();
				_nextLinkPropertyName = Array.Empty<byte>();
				_defaultPageSize = defaultPageSize;
				_cancellationToken = requestContext?.CancellationToken ?? default(CancellationToken);
				_errorOptions = requestContext?.ErrorOptions ?? ErrorOptions.Default;
			}

			public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
			{
				string nextLink = null;
				do
				{
					if (!TryGetItemsFromResponse(await GetNextResponseAsync(null, nextLink, cancellationToken).ConfigureAwait(continueOnCapturedContext: false), out nextLink, out JsonElement.ArrayEnumerator jsonArrayEnumerator, out List<T> items))
					{
						continue;
					}
					if (_valueFactory != null)
					{
						foreach (JsonElement item in jsonArrayEnumerator)
						{
							yield return _valueFactory(item);
						}
						continue;
					}
					foreach (T item2 in items)
					{
						yield return item2;
					}
				}
				while (!string.IsNullOrEmpty(nextLink));
			}

			public async IAsyncEnumerable<Page<T>> AsPagesAsync(string? continuationToken, int? pageSizeHint, [EnumeratorCancellation] CancellationToken cancellationToken)
			{
				string nextLink = continuationToken;
				do
				{
					Response response = await GetNextResponseAsync(pageSizeHint, nextLink, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					if (response == null)
					{
						break;
					}
					yield return CreatePage(response, out nextLink);
				}
				while (!string.IsNullOrEmpty(nextLink));
			}

			public IEnumerator<T> GetEnumerator()
			{
				string nextLink = null;
				do
				{
					Response nextResponse = GetNextResponse(null, nextLink);
					if (!TryGetItemsFromResponse(nextResponse, out nextLink, out JsonElement.ArrayEnumerator jsonArrayEnumerator, out List<T> items))
					{
						continue;
					}
					if (_valueFactory != null)
					{
						foreach (JsonElement item in jsonArrayEnumerator)
						{
							yield return _valueFactory(item);
						}
						continue;
					}
					foreach (T item2 in items)
					{
						yield return item2;
					}
				}
				while (!string.IsNullOrEmpty(nextLink));
			}

			public IEnumerable<Page<T>> AsPages(string? continuationToken, int? pageSizeHint)
			{
				string nextLink = continuationToken;
				do
				{
					Response nextResponse = GetNextResponse(pageSizeHint, nextLink);
					if (nextResponse == null)
					{
						break;
					}
					yield return CreatePage(nextResponse, out nextLink);
				}
				while (!string.IsNullOrEmpty(nextLink));
			}

			private Response? GetNextResponse(int? pageSizeHint, string? nextLink)
			{
				Response response;
				HttpMessage httpMessage = CreateMessage(pageSizeHint, nextLink, out response);
				if (httpMessage == null)
				{
					return response;
				}
				using Azure.Core.Pipeline.DiagnosticScope diagnosticScope = _clientDiagnostics.CreateScope(_scopeName);
				diagnosticScope.Start();
				try
				{
					_pipeline.Send(httpMessage, _cancellationToken);
					return GetResponse(httpMessage);
				}
				catch (Exception exception)
				{
					diagnosticScope.Failed(exception);
					throw;
				}
			}

			private async ValueTask<Response?> GetNextResponseAsync(int? pageSizeHint, string? nextLink, CancellationToken cancellationToken)
			{
				Response response;
				HttpMessage message = CreateMessage(pageSizeHint, nextLink, out response);
				if (message == null)
				{
					return response;
				}
				using Azure.Core.Pipeline.DiagnosticScope scope = _clientDiagnostics.CreateScope(_scopeName);
				scope.Start();
				try
				{
					if (cancellationToken.CanBeCanceled)
					{
						CancellationToken cancellationToken2 = _cancellationToken;
						if (cancellationToken2.CanBeCanceled)
						{
							using (CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cancellationToken))
							{
								await _pipeline.SendAsync(message, cts.Token).ConfigureAwait(continueOnCapturedContext: false);
							}
							goto IL_01eb;
						}
					}
					CancellationToken cancellationToken3 = (cancellationToken.CanBeCanceled ? cancellationToken : _cancellationToken);
					await _pipeline.SendAsync(message, cancellationToken3).ConfigureAwait(continueOnCapturedContext: false);
					goto IL_01eb;
					IL_01eb:
					return GetResponse(message);
				}
				catch (Exception exception)
				{
					scope.Failed(exception);
					throw;
				}
			}

			private HttpMessage? CreateMessage(int? pageSizeHint, string? nextLink, out Response? response)
			{
				if (!string.IsNullOrEmpty(nextLink))
				{
					response = null;
					return _createNextPageRequest?.Invoke(pageSizeHint ?? _defaultPageSize, nextLink);
				}
				if (_createFirstPageRequest == null)
				{
					response = _initialResponse;
					return null;
				}
				response = null;
				return _createFirstPageRequest(pageSizeHint ?? _defaultPageSize);
			}

			private Response GetResponse(HttpMessage message)
			{
				if (message.Response.IsError && _errorOptions != ErrorOptions.NoThrow)
				{
					throw new RequestFailedException(message.Response);
				}
				return message.Response;
			}

			private bool TryGetItemsFromResponse(Response? response, out string? nextLink, out JsonElement.ArrayEnumerator jsonArrayEnumerator, out List<T>? items)
			{
				if (response == null)
				{
					nextLink = null;
					jsonArrayEnumerator = default(JsonElement.ArrayEnumerator);
					items = null;
					return false;
				}
				if (_valueFactory != null)
				{
					items = null;
					JsonDocument jsonDocument = ((response.ContentStream != null) ? JsonDocument.Parse(response.ContentStream) : JsonDocument.Parse(response.Content));
					if (_createNextPageRequest == null && _itemPropertyName.Length == 0)
					{
						nextLink = null;
						jsonArrayEnumerator = jsonDocument.RootElement.EnumerateArray();
						return true;
					}
					nextLink = (jsonDocument.RootElement.TryGetProperty(_nextLinkPropertyName, out var value) ? value.GetString() : null);
					if (jsonDocument.RootElement.TryGetProperty(_itemPropertyName, out var value2))
					{
						jsonArrayEnumerator = value2.EnumerateArray();
						return true;
					}
					jsonArrayEnumerator = default(JsonElement.ArrayEnumerator);
					return false;
				}
				jsonArrayEnumerator = default(JsonElement.ArrayEnumerator);
				(items, nextLink) = _responseParser?.Invoke(response) ?? ParseResponseForBinaryData<T>(response, _itemPropertyName, _nextLinkPropertyName);
				return items != null;
			}

			private Page<T> CreatePage(Response response, out string? nextLink)
			{
				if (!TryGetItemsFromResponse(response, out nextLink, out JsonElement.ArrayEnumerator jsonArrayEnumerator, out List<T> items))
				{
					return Page<T>.FromValues(Array.Empty<T>(), nextLink, response);
				}
				if (_valueFactory == null)
				{
					return Page<T>.FromValues(items, nextLink, response);
				}
				List<T> list = new List<T>();
				foreach (JsonElement item in jsonArrayEnumerator)
				{
					list.Add(_valueFactory(item));
				}
				return Page<T>.FromValues(list, nextLink, response);
			}
		}

		private static readonly byte[] DefaultItemPropertyName = Encoding.UTF8.GetBytes("value");

		private static readonly byte[] DefaultNextLinkPropertyName = Encoding.UTF8.GetBytes("nextLink");

		public static AsyncPageable<T> CreateAsyncPageable<T>(Func<int?, HttpMessage>? createFirstPageRequest, Func<int?, string, HttpMessage>? createNextPageRequest, Func<Response, (List<T>? Values, string? NextLink)> responseParser, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string scopeName, RequestContext? requestContext = null) where T : notnull
		{
			return new AsyncPageableWrapper<T>(new PageableImplementation<T>(createFirstPageRequest, createNextPageRequest, responseParser, pipeline, clientDiagnostics, scopeName, null, requestContext));
		}

		public static AsyncPageable<T> CreateAsyncPageable<T>(Func<int?, HttpMessage>? createFirstPageRequest, Func<int?, string, HttpMessage>? createNextPageRequest, Func<JsonElement, T> valueFactory, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string scopeName, string? itemPropertyName, string? nextLinkPropertyName, CancellationToken cancellationToken) where T : notnull
		{
			return new AsyncPageableWrapper<T>(new PageableImplementation<T>(null, createFirstPageRequest, createNextPageRequest, valueFactory, pipeline, clientDiagnostics, scopeName, itemPropertyName, nextLinkPropertyName, null, cancellationToken, null));
		}

		public static AsyncPageable<T> CreateAsyncPageable<T>(Func<int?, HttpMessage>? createFirstPageRequest, Func<int?, string, HttpMessage>? createNextPageRequest, Func<JsonElement, T> valueFactory, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string scopeName, string? itemPropertyName, string? nextLinkPropertyName, RequestContext? requestContext = null) where T : notnull
		{
			return new AsyncPageableWrapper<T>(new PageableImplementation<T>(null, createFirstPageRequest, createNextPageRequest, valueFactory, pipeline, clientDiagnostics, scopeName, itemPropertyName, nextLinkPropertyName, null, requestContext?.CancellationToken, requestContext?.ErrorOptions));
		}

		public static AsyncPageable<T> CreateAsyncPageable<T>(Response initialResponse, Func<int?, string, HttpMessage>? createNextPageRequest, Func<JsonElement, T> valueFactory, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string scopeName, string? itemPropertyName, string? nextLinkPropertyName, CancellationToken cancellationToken) where T : notnull
		{
			return new AsyncPageableWrapper<T>(new PageableImplementation<T>(initialResponse, null, createNextPageRequest, valueFactory, pipeline, clientDiagnostics, scopeName, itemPropertyName, nextLinkPropertyName, null, cancellationToken, null));
		}

		public static Pageable<T> CreatePageable<T>(Func<int?, HttpMessage>? createFirstPageRequest, Func<int?, string, HttpMessage>? createNextPageRequest, Func<Response, (List<T>? Values, string? NextLink)> responseParser, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string scopeName, RequestContext? requestContext = null) where T : notnull
		{
			return new PageableWrapper<T>(new PageableImplementation<T>(createFirstPageRequest, createNextPageRequest, responseParser, pipeline, clientDiagnostics, scopeName, null, requestContext));
		}

		public static Pageable<T> CreatePageable<T>(Func<int?, HttpMessage>? createFirstPageRequest, Func<int?, string, HttpMessage>? createNextPageRequest, Func<JsonElement, T> valueFactory, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string scopeName, string? itemPropertyName, string? nextLinkPropertyName, CancellationToken cancellationToken) where T : notnull
		{
			return new PageableWrapper<T>(new PageableImplementation<T>(null, createFirstPageRequest, createNextPageRequest, valueFactory, pipeline, clientDiagnostics, scopeName, itemPropertyName, nextLinkPropertyName, null, cancellationToken, null));
		}

		public static Pageable<T> CreatePageable<T>(Func<int?, HttpMessage>? createFirstPageRequest, Func<int?, string, HttpMessage>? createNextPageRequest, Func<JsonElement, T> valueFactory, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string scopeName, string? itemPropertyName, string? nextLinkPropertyName, RequestContext? requestContext = null) where T : notnull
		{
			return new PageableWrapper<T>(new PageableImplementation<T>(null, createFirstPageRequest, createNextPageRequest, valueFactory, pipeline, clientDiagnostics, scopeName, itemPropertyName, nextLinkPropertyName, null, requestContext?.CancellationToken, requestContext?.ErrorOptions));
		}

		public static Pageable<T> CreatePageable<T>(Response initialResponse, Func<int?, string, HttpMessage>? createNextPageRequest, Func<JsonElement, T> valueFactory, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string scopeName, string? itemPropertyName, string? nextLinkPropertyName, CancellationToken cancellationToken) where T : notnull
		{
			return new PageableWrapper<T>(new PageableImplementation<T>(initialResponse, null, createNextPageRequest, valueFactory, pipeline, clientDiagnostics, scopeName, itemPropertyName, nextLinkPropertyName, null, cancellationToken, null));
		}

		public static async ValueTask<Operation<AsyncPageable<T>>> CreateAsyncPageable<T>(WaitUntil waitUntil, HttpMessage message, Func<int?, string, HttpMessage>? createNextPageMethod, Func<JsonElement, T> valueFactory, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, Azure.Core.OperationFinalStateVia finalStateVia, string scopeName, string? itemPropertyName, string? nextLinkPropertyName, RequestContext? requestContext = null) where T : notnull
		{
			Response response = await pipeline.ProcessMessageAsync(message, requestContext).ConfigureAwait(continueOnCapturedContext: false);
			ProtocolOperation<AsyncPageable<T>> operation = new ProtocolOperation<AsyncPageable<T>>(clientDiagnostics, pipeline, message.Request, response, finalStateVia, scopeName, ResultSelector);
			if (waitUntil == WaitUntil.Completed)
			{
				await operation.WaitForCompletionAsync(requestContext?.CancellationToken ?? default(CancellationToken)).ConfigureAwait(continueOnCapturedContext: false);
			}
			return operation;
			AsyncPageable<T> ResultSelector(Response r)
			{
				return new AsyncPageableWrapper<T>(new PageableImplementation<T>(r, null, createNextPageMethod, valueFactory, pipeline, clientDiagnostics, scopeName, itemPropertyName, nextLinkPropertyName, null, requestContext?.CancellationToken, requestContext?.ErrorOptions));
			}
		}

		public static Operation<Pageable<T>> CreatePageable<T>(WaitUntil waitUntil, HttpMessage message, Func<int?, string, HttpMessage>? createNextPageMethod, Func<JsonElement, T> valueFactory, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, Azure.Core.OperationFinalStateVia finalStateVia, string scopeName, string? itemPropertyName, string? nextLinkPropertyName, RequestContext? requestContext = null) where T : notnull
		{
			Response response = pipeline.ProcessMessage(message, requestContext);
			ProtocolOperation<Pageable<T>> protocolOperation = new ProtocolOperation<Pageable<T>>(clientDiagnostics, pipeline, message.Request, response, finalStateVia, scopeName, ResultSelector);
			if (waitUntil == WaitUntil.Completed)
			{
				protocolOperation.WaitForCompletion(requestContext?.CancellationToken ?? default(CancellationToken));
			}
			return protocolOperation;
			Pageable<T> ResultSelector(Response r)
			{
				return new PageableWrapper<T>(new PageableImplementation<T>(r, null, createNextPageMethod, valueFactory, pipeline, clientDiagnostics, scopeName, itemPropertyName, nextLinkPropertyName, null, requestContext?.CancellationToken, requestContext?.ErrorOptions));
			}
		}

		public static Pageable<T> CreateEnumerable<T>(Func<int?, Page<T>> firstPageFunc, Func<string?, int?, Page<T>>? nextPageFunc, int? pageSize = null) where T : notnull
		{
			return new FuncPageable<T>((string? _, int? pageSizeHint) => firstPageFunc(pageSizeHint), nextPageFunc, pageSize);
		}

		public static AsyncPageable<T> CreateAsyncEnumerable<T>(Func<int?, Task<Page<T>>> firstPageFunc, Func<string?, int?, Task<Page<T>>>? nextPageFunc, int? pageSize = null) where T : notnull
		{
			return new FuncAsyncPageable<T>((string? _, int? pageSizeHint) => firstPageFunc(pageSizeHint), nextPageFunc, pageSize);
		}

		private static (List<T>? Values, string? NextLink) ParseResponseForBinaryData<T>(Response response, byte[] itemPropertyName, byte[] nextLinkPropertyName)
		{
			ReadOnlyMemory<byte> content = response.Content.ToMemory();
			Utf8JsonReader r = new Utf8JsonReader(content.Span);
			List<T> list = null;
			string item = null;
			if (!r.Read() || r.TokenType != JsonTokenType.StartObject)
			{
				throw new InvalidOperationException("Expected response to be JSON object");
			}
			while (r.Read())
			{
				switch (r.TokenType)
				{
				case JsonTokenType.PropertyName:
					if (r.ValueTextEquals(nextLinkPropertyName))
					{
						r.Read();
						item = r.GetString();
					}
					else if (r.ValueTextEquals(itemPropertyName))
					{
						if (!r.Read() || r.TokenType != JsonTokenType.StartArray)
						{
							throw new InvalidOperationException("Expected " + Encoding.UTF8.GetString(itemPropertyName) + " to be an array");
						}
						while (r.Read() && r.TokenType != JsonTokenType.EndArray)
						{
							object obj = ReadBinaryData(ref r, in content);
							if (list == null)
							{
								list = new List<T>();
							}
							list.Add((T)obj);
						}
					}
					else
					{
						r.Skip();
					}
					break;
				default:
					throw new Exception("Unexpected token");
				case JsonTokenType.EndObject:
					break;
				}
			}
			return (Values: list, NextLink: item);
			static object ReadBinaryData(ref Utf8JsonReader reference, in ReadOnlyMemory<byte> reference2)
			{
				switch (reference.TokenType)
				{
				case JsonTokenType.StartObject:
				case JsonTokenType.StartArray:
				{
					int num = (int)reference.TokenStartIndex;
					reference.Skip();
					int length = (int)reference.TokenStartIndex - num + 1;
					return new BinaryData(reference2.Slice(num, length));
				}
				case JsonTokenType.String:
					return new BinaryData(reference2.Slice((int)reference.TokenStartIndex, reference.ValueSpan.Length + 2));
				default:
					return new BinaryData(reference2.Slice((int)reference.TokenStartIndex, reference.ValueSpan.Length));
				}
			}
		}
	}
	internal class ProtocolOperation<T> : Operation<T>, Azure.Core.IOperation<T> where T : notnull
	{
		private readonly Func<Response, T> _resultSelector;

		private readonly Azure.Core.OperationInternal<T> _operation;

		private readonly Azure.Core.IOperation _nextLinkOperation;

		public override string Id
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public override T Value => _operation.Value;

		public override bool HasCompleted => _operation.HasCompleted;

		public override bool HasValue => _operation.HasValue;

		internal ProtocolOperation(Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, Request request, Response response, Azure.Core.OperationFinalStateVia finalStateVia, string scopeName, Func<Response, T> resultSelector)
		{
			_resultSelector = resultSelector;
			_nextLinkOperation = Azure.Core.NextLinkOperationImplementation.Create(pipeline, request.Method, request.Uri.ToUri(), response, finalStateVia);
			_operation = new Azure.Core.OperationInternal<T>(this, clientDiagnostics, response, scopeName);
		}

		public override Response GetRawResponse()
		{
			return _operation.RawResponse;
		}

		public override Response UpdateStatus(CancellationToken cancellationToken = default(CancellationToken))
		{
			return _operation.UpdateStatus(cancellationToken);
		}

		public override ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return _operation.UpdateStatusAsync(cancellationToken);
		}

		public override ValueTask<Response<T>> WaitForCompletionAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return _operation.WaitForCompletionAsync(cancellationToken);
		}

		public override ValueTask<Response<T>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken = default(CancellationToken))
		{
			return _operation.WaitForCompletionAsync(pollingInterval, cancellationToken);
		}

		async ValueTask<Azure.Core.OperationState<T>> Azure.Core.IOperation<T>.UpdateStateAsync(bool async, CancellationToken cancellationToken)
		{
			Azure.Core.OperationState operationState = await _nextLinkOperation.UpdateStateAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (operationState.HasSucceeded)
			{
				return Azure.Core.OperationState<T>.Success(operationState.RawResponse, _resultSelector(operationState.RawResponse));
			}
			if (operationState.HasCompleted)
			{
				return Azure.Core.OperationState<T>.Failure(operationState.RawResponse, operationState.OperationFailedException);
			}
			return Azure.Core.OperationState<T>.Pending(operationState.RawResponse);
		}
	}
	internal static class ProtocolOperationHelpers
	{
		private class ConvertOperation<TFrom, TTo> : Operation<TTo> where TFrom : notnull where TTo : notnull
		{
			private readonly Operation<TFrom> _operation;

			private readonly Azure.Core.Pipeline.ClientDiagnostics _diagnostics;

			private readonly string _waitForCompletionScopeName;

			private readonly string _updateStatusScopeName;

			private readonly Func<Response, TTo> _convertFunc;

			private Response<TTo>? _response;

			public override string Id => _operation.Id;

			public override TTo Value => GetOrCreateValue();

			public override bool HasValue => _operation.HasValue;

			public override bool HasCompleted => _operation.HasCompleted;

			public ConvertOperation(Operation<TFrom> operation, Azure.Core.Pipeline.ClientDiagnostics diagnostics, string operationName, Func<Response, TTo> convertFunc)
			{
				_operation = operation;
				_diagnostics = diagnostics;
				_waitForCompletionScopeName = operationName + ".WaitForCompletion";
				_updateStatusScopeName = operationName + ".UpdateStatus";
				_convertFunc = convertFunc;
				_response = null;
			}

			public override Response GetRawResponse()
			{
				return _operation.GetRawResponse();
			}

			public override Response UpdateStatus(CancellationToken cancellationToken = default(CancellationToken))
			{
				if (HasCompleted)
				{
					return GetRawResponse();
				}
				using Azure.Core.Pipeline.DiagnosticScope diagnosticScope = CreateScope(_updateStatusScopeName);
				try
				{
					return _operation.UpdateStatus(cancellationToken);
				}
				catch (Exception exception)
				{
					diagnosticScope.Failed(exception);
					throw;
				}
			}

			public override async ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default(CancellationToken))
			{
				if (HasCompleted)
				{
					return GetRawResponse();
				}
				using Azure.Core.Pipeline.DiagnosticScope scope = CreateScope(_updateStatusScopeName);
				try
				{
					return await _operation.UpdateStatusAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
				catch (Exception exception)
				{
					scope.Failed(exception);
					throw;
				}
			}

			public override Response<TTo> WaitForCompletion(CancellationToken cancellationToken = default(CancellationToken))
			{
				if (_response != null)
				{
					return _response;
				}
				using Azure.Core.Pipeline.DiagnosticScope diagnosticScope = CreateScope(_waitForCompletionScopeName);
				try
				{
					Response<TFrom> responseTFrom = _operation.WaitForCompletion(cancellationToken);
					return CreateResponseOfTTo(responseTFrom);
				}
				catch (Exception exception)
				{
					diagnosticScope.Failed(exception);
					throw;
				}
			}

			public override Response<TTo> WaitForCompletion(TimeSpan pollingInterval, CancellationToken cancellationToken)
			{
				if (_response != null)
				{
					return _response;
				}
				using Azure.Core.Pipeline.DiagnosticScope diagnosticScope = CreateScope(_waitForCompletionScopeName);
				try
				{
					Response<TFrom> responseTFrom = _operation.WaitForCompletion(pollingInterval, cancellationToken);
					return CreateResponseOfTTo(responseTFrom);
				}
				catch (Exception exception)
				{
					diagnosticScope.Failed(exception);
					throw;
				}
			}

			public override async ValueTask<Response<TTo>> WaitForCompletionAsync(CancellationToken cancellationToken = default(CancellationToken))
			{
				if (_response != null)
				{
					return _response;
				}
				using Azure.Core.Pipeline.DiagnosticScope scope = CreateScope(_waitForCompletionScopeName);
				try
				{
					return CreateResponseOfTTo(await _operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
				}
				catch (Exception exception)
				{
					scope.Failed(exception);
					throw;
				}
			}

			public override async ValueTask<Response<TTo>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
			{
				if (_response != null)
				{
					return _response;
				}
				using Azure.Core.Pipeline.DiagnosticScope scope = CreateScope(_waitForCompletionScopeName);
				try
				{
					return CreateResponseOfTTo(await _operation.WaitForCompletionAsync(pollingInterval, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
				}
				catch (Exception exception)
				{
					scope.Failed(exception);
					throw;
				}
			}

			private TTo GetOrCreateValue()
			{
				if (_response == null)
				{
					return CreateResponseOfTTo(GetRawResponse()).Value;
				}
				return _response.Value;
			}

			private Response<TTo> CreateResponseOfTTo(Response<TFrom> responseTFrom)
			{
				return CreateResponseOfTTo(responseTFrom.GetRawResponse());
			}

			private Response<TTo> CreateResponseOfTTo(Response rawResponse)
			{
				Response<TTo> value = Response.FromValue(_convertFunc(rawResponse), rawResponse);
				Interlocked.CompareExchange(ref _response, value, null);
				return _response;
			}

			private Azure.Core.Pipeline.DiagnosticScope CreateScope(string name)
			{
				Azure.Core.Pipeline.DiagnosticScope result = _diagnostics.CreateScope(name);
				result.Start();
				return result;
			}
		}

		public static Operation<TTo> Convert<TFrom, TTo>(Operation<TFrom> operation, Func<Response, TTo> convertFunc, Azure.Core.Pipeline.ClientDiagnostics diagnostics, string scopeName) where TFrom : notnull where TTo : notnull
		{
			return new ConvertOperation<TFrom, TTo>(operation, diagnostics, scopeName, convertFunc);
		}

		public static ValueTask<Operation<Azure.Core.VoidValue>> ProcessMessageWithoutResponseValueAsync(HttpPipeline pipeline, HttpMessage message, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, string scopeName, Azure.Core.OperationFinalStateVia finalStateVia, RequestContext? requestContext, WaitUntil waitUntil)
		{
			return ProcessMessageAsync(pipeline, message, clientDiagnostics, scopeName, finalStateVia, requestContext, waitUntil, (Response _) => default(Azure.Core.VoidValue));
		}

		public static Operation<Azure.Core.VoidValue> ProcessMessageWithoutResponseValue(HttpPipeline pipeline, HttpMessage message, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, string scopeName, Azure.Core.OperationFinalStateVia finalStateVia, RequestContext? requestContext, WaitUntil waitUntil)
		{
			return ProcessMessage(pipeline, message, clientDiagnostics, scopeName, finalStateVia, requestContext, waitUntil, (Response _) => default(Azure.Core.VoidValue));
		}

		public static ValueTask<Operation<BinaryData>> ProcessMessageAsync(HttpPipeline pipeline, HttpMessage message, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, string scopeName, Azure.Core.OperationFinalStateVia finalStateVia, RequestContext? requestContext, WaitUntil waitUntil)
		{
			return ProcessMessageAsync(pipeline, message, clientDiagnostics, scopeName, finalStateVia, requestContext, waitUntil, (Response r) => r.Content);
		}

		public static Operation<BinaryData> ProcessMessage(HttpPipeline pipeline, HttpMessage message, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, string scopeName, Azure.Core.OperationFinalStateVia finalStateVia, RequestContext? requestContext, WaitUntil waitUntil)
		{
			return ProcessMessage(pipeline, message, clientDiagnostics, scopeName, finalStateVia, requestContext, waitUntil, (Response r) => r.Content);
		}

		public static async ValueTask<Operation<T>> ProcessMessageAsync<T>(HttpPipeline pipeline, HttpMessage message, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, string scopeName, Azure.Core.OperationFinalStateVia finalStateVia, RequestContext? requestContext, WaitUntil waitUntil, Func<Response, T> resultSelector) where T : notnull
		{
			Response response = await pipeline.ProcessMessageAsync(message, requestContext).ConfigureAwait(continueOnCapturedContext: false);
			ProtocolOperation<T> operation = new ProtocolOperation<T>(clientDiagnostics, pipeline, message.Request, response, finalStateVia, scopeName, resultSelector);
			if (waitUntil == WaitUntil.Completed)
			{
				await operation.WaitForCompletionAsync(requestContext?.CancellationToken ?? default(CancellationToken)).ConfigureAwait(continueOnCapturedContext: false);
			}
			return operation;
		}

		public static Operation<T> ProcessMessage<T>(HttpPipeline pipeline, HttpMessage message, Azure.Core.Pipeline.ClientDiagnostics clientDiagnostics, string scopeName, Azure.Core.OperationFinalStateVia finalStateVia, RequestContext? requestContext, WaitUntil waitUntil, Func<Response, T> resultSelector) where T : notnull
		{
			Response response = pipeline.ProcessMessage(message, requestContext);
			ProtocolOperation<T> protocolOperation = new ProtocolOperation<T>(clientDiagnostics, pipeline, message.Request, response, finalStateVia, scopeName, resultSelector);
			if (waitUntil == WaitUntil.Completed)
			{
				protocolOperation.WaitForCompletion(requestContext?.CancellationToken ?? default(CancellationToken));
			}
			return protocolOperation;
		}
	}
	internal class RawRequestUriBuilder : RequestUriBuilder
	{
		private enum RawWritingPosition
		{
			Scheme,
			Host,
			Port,
			Path,
			Query
		}

		private const string SchemeSeparator = "://";

		private const char HostSeparator = '/';

		private const char PortSeparator = ':';

		private static readonly char[] HostOrPort = new char[2] { '/', ':' };

		private const char QueryBeginSeparator = '?';

		private const char QueryContinueSeparator = '&';

		private const char QueryValueSeparator = '=';

		private RawWritingPosition? _position;

		private static void GetQueryParts(ReadOnlySpan<char> queryUnparsed, out ReadOnlySpan<char> name, out ReadOnlySpan<char> value)
		{
			int num = queryUnparsed.IndexOf('=');
			if (num == -1)
			{
				name = queryUnparsed;
				value = ReadOnlySpan<char>.Empty;
			}
			else
			{
				name = queryUnparsed.Slice(0, num);
				value = queryUnparsed.Slice(num + 1);
			}
		}

		public void AppendRaw(string value, bool escape)
		{
			AppendRaw(MemoryExtensions.AsSpan(value), escape);
		}

		private void AppendRaw(ReadOnlySpan<char> value, bool escape)
		{
			if (!_position.HasValue)
			{
				if (base.HasQuery)
				{
					_position = RawWritingPosition.Query;
				}
				else if (base.HasPath)
				{
					_position = RawWritingPosition.Path;
				}
				else if (!string.IsNullOrEmpty(base.Host))
				{
					_position = RawWritingPosition.Host;
				}
				else
				{
					_position = RawWritingPosition.Scheme;
				}
			}
			while (!value.IsEmpty)
			{
				if (_position == RawWritingPosition.Scheme)
				{
					int num = value.IndexOf(MemoryExtensions.AsSpan("://"), StringComparison.InvariantCultureIgnoreCase);
					if (num == -1)
					{
						base.Scheme += value;
						value = ReadOnlySpan<char>.Empty;
						continue;
					}
					base.Scheme += value.Slice(0, num);
					base.Port = (string.Equals(base.Scheme, "https", StringComparison.OrdinalIgnoreCase) ? 443 : 80);
					value = value.Slice(num + "://".Length);
					_position = RawWritingPosition.Host;
				}
				else if (_position == RawWritingPosition.Host)
				{
					int num2 = value.IndexOfAny(HostOrPort);
					if (num2 == -1)
					{
						if (!base.HasPath)
						{
							base.Host += value;
							value = ReadOnlySpan<char>.Empty;
						}
						else
						{
							_position = RawWritingPosition.Path;
						}
					}
					else
					{
						base.Host += value.Slice(0, num2);
						_position = ((value[num2] == '/') ? RawWritingPosition.Path : RawWritingPosition.Port);
						value = value.Slice(num2 + 1);
					}
				}
				else if (_position == RawWritingPosition.Port)
				{
					int num3 = value.IndexOf('/');
					if (num3 == -1)
					{
						base.Port = int.Parse(value.ToString(), CultureInfo.InvariantCulture);
						value = ReadOnlySpan<char>.Empty;
					}
					else
					{
						base.Port = int.Parse(value.Slice(0, num3).ToString(), CultureInfo.InvariantCulture);
						value = value.Slice(num3 + 1);
					}
					_position = RawWritingPosition.Path;
				}
				else if (_position == RawWritingPosition.Path)
				{
					int num4 = value.IndexOf('?');
					if (num4 == -1)
					{
						AppendPath(value, escape);
						value = ReadOnlySpan<char>.Empty;
					}
					else
					{
						AppendPath(value.Slice(0, num4), escape);
						value = value.Slice(num4 + 1);
						_position = RawWritingPosition.Query;
					}
				}
				else if (_position == RawWritingPosition.Query)
				{
					int num5 = value.IndexOf('&');
					switch (num5)
					{
					case 0:
						value = value.Slice(1);
						break;
					case -1:
					{
						GetQueryParts(value, out var name2, out var value3);
						AppendQuery(name2, value3, escape);
						value = ReadOnlySpan<char>.Empty;
						break;
					}
					default:
					{
						GetQueryParts(value.Slice(0, num5), out var name, out var value2);
						AppendQuery(name, value2, escape);
						value = value.Slice(num5 + 1);
						break;
					}
					}
				}
			}
		}

		public void AppendRawNextLink(string nextLink, bool escape)
		{
			if (nextLink.StartsWith(Uri.UriSchemeHttp, StringComparison.InvariantCultureIgnoreCase))
			{
				Reset(new Uri(nextLink));
			}
			else
			{
				AppendRaw(nextLink, escape);
			}
		}
	}
	internal static class RequestContentHelper
	{
		public static RequestContent FromEnumerable<T>(IEnumerable<T> enumerable) where T : notnull
		{
			Utf8JsonRequestContent utf8JsonRequestContent = new Utf8JsonRequestContent();
			utf8JsonRequestContent.JsonWriter.WriteStartArray();
			foreach (T item in enumerable)
			{
				utf8JsonRequestContent.JsonWriter.WriteObjectValue(item);
			}
			utf8JsonRequestContent.JsonWriter.WriteEndArray();
			return utf8JsonRequestContent;
		}

		public static RequestContent FromEnumerable(IEnumerable<BinaryData> enumerable)
		{
			Utf8JsonRequestContent utf8JsonRequestContent = new Utf8JsonRequestContent();
			utf8JsonRequestContent.JsonWriter.WriteStartArray();
			foreach (BinaryData item in enumerable)
			{
				if (item == null)
				{
					utf8JsonRequestContent.JsonWriter.WriteNullValue();
				}
				else
				{
					JsonSerializer.Serialize(utf8JsonRequestContent.JsonWriter, JsonDocument.Parse(item.ToString()).RootElement);
				}
			}
			utf8JsonRequestContent.JsonWriter.WriteEndArray();
			return utf8JsonRequestContent;
		}

		public static RequestContent FromDictionary<T>(IDictionary<string, T> dictionary) where T : notnull
		{
			Utf8JsonRequestContent utf8JsonRequestContent = new Utf8JsonRequestContent();
			utf8JsonRequestContent.JsonWriter.WriteStartObject();
			foreach (KeyValuePair<string, T> item in dictionary)
			{
				utf8JsonRequestContent.JsonWriter.WritePropertyName(item.Key);
				utf8JsonRequestContent.JsonWriter.WriteObjectValue(item.Value);
			}
			utf8JsonRequestContent.JsonWriter.WriteEndObject();
			return utf8JsonRequestContent;
		}

		public static RequestContent FromDictionary(IDictionary<string, BinaryData> dictionary)
		{
			Utf8JsonRequestContent utf8JsonRequestContent = new Utf8JsonRequestContent();
			utf8JsonRequestContent.JsonWriter.WriteStartObject();
			foreach (KeyValuePair<string, BinaryData> item in dictionary)
			{
				utf8JsonRequestContent.JsonWriter.WritePropertyName(item.Key);
				if (item.Value == null)
				{
					utf8JsonRequestContent.JsonWriter.WriteNullValue();
				}
				else
				{
					JsonSerializer.Serialize(utf8JsonRequestContent.JsonWriter, JsonDocument.Parse(item.Value.ToString()).RootElement);
				}
			}
			utf8JsonRequestContent.JsonWriter.WriteEndObject();
			return utf8JsonRequestContent;
		}

		public static RequestContent FromObject(object value)
		{
			Utf8JsonRequestContent utf8JsonRequestContent = new Utf8JsonRequestContent();
			utf8JsonRequestContent.JsonWriter.WriteObjectValue(value);
			return utf8JsonRequestContent;
		}

		public static RequestContent FromObject(BinaryData value)
		{
			Utf8JsonRequestContent utf8JsonRequestContent = new Utf8JsonRequestContent();
			JsonSerializer.Serialize(utf8JsonRequestContent.JsonWriter, JsonDocument.Parse(value).RootElement);
			return utf8JsonRequestContent;
		}
	}
	internal static class RequestHeaderExtensions
	{
		public static void Add(this RequestHeaders headers, string name, bool value)
		{
			headers.Add(name, TypeFormatters.ToString(value));
		}

		public static void Add(this RequestHeaders headers, string name, float value)
		{
			headers.Add(name, value.ToString(TypeFormatters.DefaultNumberFormat, CultureInfo.InvariantCulture));
		}

		public static void Add(this RequestHeaders headers, string name, double value)
		{
			headers.Add(name, value.ToString(TypeFormatters.DefaultNumberFormat, CultureInfo.InvariantCulture));
		}

		public static void Add(this RequestHeaders headers, string name, int value)
		{
			headers.Add(name, value.ToString(TypeFormatters.DefaultNumberFormat, CultureInfo.InvariantCulture));
		}

		public static void Add(this RequestHeaders headers, string name, long value)
		{
			headers.Add(name, value.ToString(TypeFormatters.DefaultNumberFormat, CultureInfo.InvariantCulture));
		}

		public static void Add(this RequestHeaders headers, string name, DateTimeOffset value, string format)
		{
			headers.Add(name, TypeFormatters.ToString(value, format));
		}

		public static void Add(this RequestHeaders headers, string name, TimeSpan value, string format)
		{
			headers.Add(name, TypeFormatters.ToString(value, format));
		}

		public static void Add(this RequestHeaders headers, string name, Guid value)
		{
			headers.Add(name, value.ToString());
		}

		public static void Add(this RequestHeaders headers, string name, byte[] value, string format)
		{
			headers.Add(name, TypeFormatters.ToString(value, format));
		}

		public static void Add(this RequestHeaders headers, string name, BinaryData value, string format)
		{
			headers.Add(name, TypeFormatters.ToString(value.ToArray(), format));
		}

		public static void Add(this RequestHeaders headers, string prefix, IDictionary<string, string> headersToAdd)
		{
			foreach (KeyValuePair<string, string> item in headersToAdd)
			{
				headers.Add(prefix + item.Key, item.Value);
			}
		}

		public static void Add(this RequestHeaders headers, string name, ETag value)
		{
			headers.Add(name, value.ToString("H"));
		}

		public static void Add(this RequestHeaders headers, MatchConditions conditions)
		{
			if (conditions.IfMatch.HasValue)
			{
				headers.Add("If-Match", conditions.IfMatch.Value);
			}
			if (conditions.IfNoneMatch.HasValue)
			{
				headers.Add("If-None-Match", conditions.IfNoneMatch.Value);
			}
		}

		public static void Add(this RequestHeaders headers, RequestConditions conditions, string format)
		{
			if (conditions.IfMatch.HasValue)
			{
				headers.Add("If-Match", conditions.IfMatch.Value);
			}
			if (conditions.IfNoneMatch.HasValue)
			{
				headers.Add("If-None-Match", conditions.IfNoneMatch.Value);
			}
			if (conditions.IfModifiedSince.HasValue)
			{
				headers.Add("If-Modified-Since", conditions.IfModifiedSince.Value, format);
			}
			if (conditions.IfUnmodifiedSince.HasValue)
			{
				headers.Add("If-Unmodified-Since", conditions.IfUnmodifiedSince.Value, format);
			}
		}

		public static void AddDelimited<T>(this RequestHeaders headers, string name, IEnumerable<T> value, string delimiter)
		{
			headers.Add(name, string.Join(delimiter, value));
		}

		public static void AddDelimited<T>(this RequestHeaders headers, string name, IEnumerable<T> value, string delimiter, string format)
		{
			IEnumerable<string> values = value.Select<T, string>((T v) => TypeFormatters.ConvertToString(v, format));
			headers.Add(name, string.Join(delimiter, values));
		}
	}
	internal static class RequestUriBuilderExtensions
	{
		public static void AppendPath(this RequestUriBuilder builder, bool value, bool escape = false)
		{
			builder.AppendPath(TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendPath(this RequestUriBuilder builder, float value, bool escape = true)
		{
			builder.AppendPath(TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendPath(this RequestUriBuilder builder, double value, bool escape = true)
		{
			builder.AppendPath(TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendPath(this RequestUriBuilder builder, int value, bool escape = true)
		{
			builder.AppendPath(TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendPath(this RequestUriBuilder builder, byte[] value, string format, bool escape = true)
		{
			builder.AppendPath(TypeFormatters.ConvertToString(value, format), escape);
		}

		public static void AppendPath(this RequestUriBuilder builder, IEnumerable<string> value, bool escape = true)
		{
			builder.AppendPath(TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendPath(this RequestUriBuilder builder, DateTimeOffset value, string format, bool escape = true)
		{
			builder.AppendPath(TypeFormatters.ConvertToString(value, format), escape);
		}

		public static void AppendPath(this RequestUriBuilder builder, TimeSpan value, string format, bool escape = true)
		{
			builder.AppendPath(TypeFormatters.ConvertToString(value, format), escape);
		}

		public static void AppendPath(this RequestUriBuilder builder, Guid value, bool escape = true)
		{
			builder.AppendPath(TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendPath(this RequestUriBuilder builder, long value, bool escape = true)
		{
			builder.AppendPath(TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendQuery(this RequestUriBuilder builder, string name, bool value, bool escape = false)
		{
			builder.AppendQuery(name, TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendQuery(this RequestUriBuilder builder, string name, float value, bool escape = true)
		{
			builder.AppendQuery(name, TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendQuery(this RequestUriBuilder builder, string name, DateTimeOffset value, string format, bool escape = true)
		{
			builder.AppendQuery(name, TypeFormatters.ConvertToString(value, format), escape);
		}

		public static void AppendQuery(this RequestUriBuilder builder, string name, TimeSpan value, string format, bool escape = true)
		{
			builder.AppendQuery(name, TypeFormatters.ConvertToString(value, format), escape);
		}

		public static void AppendQuery(this RequestUriBuilder builder, string name, double value, bool escape = true)
		{
			builder.AppendQuery(name, TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendQuery(this RequestUriBuilder builder, string name, decimal value, bool escape = true)
		{
			builder.AppendQuery(name, TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendQuery(this RequestUriBuilder builder, string name, int value, bool escape = true)
		{
			builder.AppendQuery(name, TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendQuery(this RequestUriBuilder builder, string name, long value, bool escape = true)
		{
			builder.AppendQuery(name, TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendQuery(this RequestUriBuilder builder, string name, TimeSpan value, bool escape = true)
		{
			builder.AppendQuery(name, TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendQuery(this RequestUriBuilder builder, string name, byte[] value, string format, bool escape = true)
		{
			builder.AppendQuery(name, TypeFormatters.ConvertToString(value, format), escape);
		}

		public static void AppendQuery(this RequestUriBuilder builder, string name, Guid value, bool escape = true)
		{
			builder.AppendQuery(name, TypeFormatters.ConvertToString(value), escape);
		}

		public static void AppendQueryDelimited<T>(this RequestUriBuilder builder, string name, IEnumerable<T> value, string delimiter, bool escape = true)
		{
			IEnumerable<string> values = value.Select<T, string>((T v) => TypeFormatters.ConvertToString(v));
			builder.AppendQuery(name, string.Join(delimiter, values), escape);
		}

		public static void AppendQueryDelimited<T>(this RequestUriBuilder builder, string name, IEnumerable<T> value, string delimiter, string format, bool escape = true)
		{
			IEnumerable<string> values = value.Select<T, string>((T v) => TypeFormatters.ConvertToString(v, format));
			builder.AppendQuery(name, string.Join(delimiter, values), escape);
		}
	}
	internal static class ResponseHeadersExtensions
	{
		private static readonly string[] KnownFormats = new string[21]
		{
			"ddd, d MMM yyyy H:m:s 'GMT'", "ddd, d MMM yyyy H:m:s 'UTC'", "ddd, d MMM yyyy H:m:s", "d MMM yyyy H:m:s 'GMT'", "d MMM yyyy H:m:s 'UTC'", "d MMM yyyy H:m:s", "ddd, d MMM yy H:m:s 'GMT'", "ddd, d MMM yy H:m:s 'UTC'", "ddd, d MMM yy H:m:s", "d MMM yy H:m:s 'GMT'",
			"d MMM yy H:m:s 'UTC'", "d MMM yy H:m:s", "dddd, d'-'MMM'-'yy H:m:s 'GMT'", "dddd, d'-'MMM'-'yy H:m:s 'UTC'", "dddd, d'-'MMM'-'yy H:m:s zzz", "dddd, d'-'MMM'-'yy H:m:s", "ddd MMM d H:m:s yyyy", "ddd, d MMM yyyy H:m:s zzz", "ddd, d MMM yyyy H:m:s", "d MMM yyyy H:m:s zzz",
			"d MMM yyyy H:m:s"
		};

		public static bool TryGetValue(this ResponseHeaders headers, string name, out byte[]? value)
		{
			if (headers.TryGetValue(name, out string value2))
			{
				value = Convert.FromBase64String(value2);
				return true;
			}
			value = null;
			return false;
		}

		public static bool TryGetValue(this ResponseHeaders headers, string name, out TimeSpan? value)
		{
			if (headers.TryGetValue(name, out string value2))
			{
				value = XmlConvert.ToTimeSpan(value2);
				return true;
			}
			value = null;
			return false;
		}

		public static bool TryGetValue(this ResponseHeaders headers, string name, out DateTimeOffset? value)
		{
			if (headers.TryGetValue(name, out string value2))
			{
				if (DateTimeOffset.TryParseExact(value2, "r", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out var result) || DateTimeOffset.TryParseExact(value2, KnownFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowInnerWhite | DateTimeStyles.AssumeUniversal, out result))
				{
					value = result;
				}
				else
				{
					value = TypeFormatters.ParseDateTimeOffset(value2, "");
				}
				return true;
			}
			value = null;
			return false;
		}

		public static bool TryGetValue<T>(this ResponseHeaders headers, string name, out T? value) where T : struct
		{
			if (headers.TryGetValue(name, out string value2))
			{
				value = (T)Convert.ChangeType(value2, typeof(T), CultureInfo.InvariantCulture);
				return true;
			}
			value = null;
			return false;
		}

		public static bool TryGetValue<T>(this ResponseHeaders headers, string name, out T? value) where T : class
		{
			if (headers.TryGetValue(name, out string value2))
			{
				value = (T)Convert.ChangeType(value2, typeof(T), CultureInfo.InvariantCulture);
				return true;
			}
			value = null;
			return false;
		}

		public static bool TryGetValue(this ResponseHeaders headers, string prefix, out IDictionary<string, string> value)
		{
			value = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			foreach (HttpHeader item in headers)
			{
				if (item.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
				{
					value.Add(item.Name.Substring(prefix.Length), item.Value);
				}
			}
			return true;
		}
	}
	internal static class ResponseWithHeaders
	{
		public static ResponseWithHeaders<T, THeaders> FromValue<T, THeaders>(T value, THeaders headers, Response rawResponse)
		{
			return new ResponseWithHeaders<T, THeaders>(value, headers, rawResponse);
		}

		public static ResponseWithHeaders<THeaders> FromValue<THeaders>(THeaders headers, Response rawResponse)
		{
			return new ResponseWithHeaders<THeaders>(headers, rawResponse);
		}
	}
	internal class ResponseWithHeaders<THeaders>
	{
		private readonly Response _rawResponse;

		public THeaders Headers { get; }

		public ResponseWithHeaders(THeaders headers, Response rawResponse)
		{
			_rawResponse = rawResponse;
			Headers = headers;
		}

		public Response GetRawResponse()
		{
			return _rawResponse;
		}

		public static implicit operator Response(ResponseWithHeaders<THeaders> self)
		{
			return self.GetRawResponse();
		}
	}
	internal class ResponseWithHeaders<T, THeaders> : Response<T>
	{
		private readonly Response _rawResponse;

		public override T Value { get; }

		public THeaders Headers { get; }

		public ResponseWithHeaders(T value, THeaders headers, Response rawResponse)
		{
			_rawResponse = rawResponse;
			Value = value;
			Headers = headers;
		}

		public override Response GetRawResponse()
		{
			return _rawResponse;
		}

		public static implicit operator Response(ResponseWithHeaders<T, THeaders> self)
		{
			return self.GetRawResponse();
		}
	}
	internal class SequentialDelayStrategy : DelayStrategy
	{
		private static readonly TimeSpan[] _pollingSequence = new TimeSpan[8]
		{
			TimeSpan.FromSeconds(1.0),
			TimeSpan.FromSeconds(1.0),
			TimeSpan.FromSeconds(1.0),
			TimeSpan.FromSeconds(2.0),
			TimeSpan.FromSeconds(4.0),
			TimeSpan.FromSeconds(8.0),
			TimeSpan.FromSeconds(16.0),
			TimeSpan.FromSeconds(32.0)
		};

		private static readonly TimeSpan _maxDelay = _pollingSequence[_pollingSequence.Length - 1];

		public SequentialDelayStrategy()
			: base(_maxDelay, 0.0)
		{
		}

		protected override TimeSpan GetNextDelayCore(Response? response, int retryNumber)
		{
			int num = retryNumber - 1;
			if (num < _pollingSequence.Length)
			{
				return _pollingSequence[num];
			}
			return _maxDelay;
		}
	}
	internal class StringRequestContent : RequestContent
	{
		private readonly byte[] _bytes;

		public StringRequestContent(string value)
		{
			_bytes = Encoding.UTF8.GetBytes(value);
		}

		public override async Task WriteToAsync(Stream stream, CancellationToken cancellation)
		{
			await stream.WriteAsync(_bytes, 0, _bytes.Length, cancellation).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override void WriteTo(Stream stream, CancellationToken cancellation)
		{
			stream.Write(_bytes, 0, _bytes.Length);
		}

		public override bool TryComputeLength(out long length)
		{
			length = _bytes.Length;
			return true;
		}

		public override void Dispose()
		{
		}
	}
	internal class TypeFormatters
	{
		private const string RoundtripZFormat = "yyyy-MM-ddTHH:mm:ss.fffffffZ";

		public static string DefaultNumberFormat { get; } = "G";

		public static string ToString(bool value)
		{
			if (!value)
			{
				return "false";
			}
			return "true";
		}

		public static string ToString(DateTime value, string format)
		{
			if (value.Kind == DateTimeKind.Utc)
			{
				return ToString((DateTimeOffset)value, format);
			}
			throw new NotSupportedException($"DateTime {value} has a Kind of {value.Kind}. Azure SDK requires it to be UTC. You can call DateTime.SpecifyKind to change Kind property value to DateTimeKind.Utc.");
		}

		public static string ToString(DateTimeOffset value, string format)
		{
			return format switch
			{
				"D" => value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), 
				"U" => value.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture), 
				"O" => value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ", CultureInfo.InvariantCulture), 
				"o" => value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ", CultureInfo.InvariantCulture), 
				"R" => value.ToString("r", CultureInfo.InvariantCulture), 
				_ => value.ToString(format, CultureInfo.InvariantCulture), 
			};
		}

		public static string ToString(TimeSpan value, string format)
		{
			if (format == "P")
			{
				return XmlConvert.ToString(value);
			}
			return value.ToString(format, CultureInfo.InvariantCulture);
		}

		public static string ToString(byte[] value, string format)
		{
			if (!(format == "U"))
			{
				if (format == "D")
				{
					return Convert.ToBase64String(value);
				}
				throw new ArgumentException("Format is not supported: '" + format + "'", "format");
			}
			return ToBase64UrlString(value);
		}

		public static string ToBase64UrlString(byte[] value)
		{
			char[] array;
			int num;
			checked
			{
				array = new char[unchecked(checked(value.Length + 2) / 3) * 4];
				num = Convert.ToBase64CharArray(value, 0, value.Length, array, 0);
			}
			int i;
			for (i = 0; i < num; i++)
			{
				switch (array[i])
				{
				case '+':
					array[i] = '-';
					continue;
				case '/':
					array[i] = '_';
					continue;
				default:
					continue;
				case '=':
					break;
				}
				break;
			}
			return new string(array, 0, i);
		}

		public static byte[] FromBase64UrlString(string value)
		{
			int numBase64PaddingCharsToAddForDecode = GetNumBase64PaddingCharsToAddForDecode(value.Length);
			char[] array = new char[value.Length + numBase64PaddingCharsToAddForDecode];
			int i;
			for (i = 0; i < value.Length; i++)
			{
				char c = value[i];
				switch (c)
				{
				case '-':
					array[i] = '+';
					break;
				case '_':
					array[i] = '/';
					break;
				default:
					array[i] = c;
					break;
				}
			}
			for (; i < array.Length; i++)
			{
				array[i] = '=';
			}
			return Convert.FromBase64CharArray(array, 0, array.Length);
		}

		private static int GetNumBase64PaddingCharsToAddForDecode(int inputLength)
		{
			return (inputLength % 4) switch
			{
				0 => 0, 
				2 => 2, 
				3 => 1, 
				_ => throw new InvalidOperationException("Malformed input"), 
			};
		}

		public static DateTimeOffset ParseDateTimeOffset(string value, string format)
		{
			if (format == "U")
			{
				return DateTimeOffset.FromUnixTimeSeconds(long.Parse(value, CultureInfo.InvariantCulture));
			}
			return DateTimeOffset.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
		}

		public static TimeSpan ParseTimeSpan(string value, string format)
		{
			if (format == "P")
			{
				return XmlConvert.ToTimeSpan(value);
			}
			return TimeSpan.ParseExact(value, format, CultureInfo.InvariantCulture);
		}

		public static string ConvertToString(object? value, string? format = null)
		{
			if (value != null)
			{
				if (!(value is string result))
				{
					if (!(value is bool flag))
					{
						if (!(value is int) && !(value is float) && !(value is double) && !(value is long) && !(value is decimal))
						{
							if (!(value is byte[] array))
							{
								if (value is IEnumerable<string> values)
								{
									return string.Join(",", values);
								}
								if (!(value is DateTimeOffset dateTimeOffset))
								{
									if (value is TimeSpan timeSpan)
									{
										if (format != null)
										{
											return ToString(timeSpan, format);
										}
										return XmlConvert.ToString(timeSpan);
									}
									if (value is Guid guid)
									{
										return guid.ToString();
									}
									if (value is BinaryData binaryData)
									{
										return ConvertToString(binaryData.ToArray(), format);
									}
								}
								else if (format != null)
								{
									return ToString(dateTimeOffset, format);
								}
							}
							else if (format != null)
							{
								return ToString(array, format);
							}
							return value.ToString();
						}
						return ((IFormattable)value).ToString(DefaultNumberFormat, CultureInfo.InvariantCulture);
					}
					return ToString(flag);
				}
				return result;
			}
			return "null";
		}
	}
	internal class Utf8JsonRequestContent : RequestContent
	{
		private readonly MemoryStream _stream;

		private readonly RequestContent _content;

		public Utf8JsonWriter JsonWriter { get; }

		public Utf8JsonRequestContent()
		{
			_stream = new MemoryStream();
			_content = RequestContent.Create(_stream);
			JsonWriter = new Utf8JsonWriter(_stream);
		}

		public override async Task WriteToAsync(Stream stream, CancellationToken cancellation)
		{
			await JsonWriter.FlushAsync(cancellation).ConfigureAwait(continueOnCapturedContext: false);
			await _content.WriteToAsync(stream, cancellation).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override void WriteTo(Stream stream, CancellationToken cancellation)
		{
			JsonWriter.Flush();
			_content.WriteTo(stream, cancellation);
		}

		public override bool TryComputeLength(out long length)
		{
			length = JsonWriter.BytesCommitted + JsonWriter.BytesPending;
			return true;
		}

		public override void Dispose()
		{
			JsonWriter.Dispose();
			_content.Dispose();
			_stream.Dispose();
		}
	}
	internal static class Utf8JsonWriterExtensions
	{
		public static void WriteStringValue(this Utf8JsonWriter writer, DateTimeOffset value, string format)
		{
			writer.WriteStringValue(TypeFormatters.ToString(value, format));
		}

		public static void WriteStringValue(this Utf8JsonWriter writer, DateTime value, string format)
		{
			writer.WriteStringValue(TypeFormatters.ToString(value, format));
		}

		public static void WriteStringValue(this Utf8JsonWriter writer, TimeSpan value, string format)
		{
			writer.WriteStringValue(TypeFormatters.ToString(value, format));
		}

		public static void WriteStringValue(this Utf8JsonWriter writer, char value)
		{
			writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
		}

		public static void WriteNonEmptyArray(this Utf8JsonWriter writer, string name, IReadOnlyList<string> values)
		{
			if (!values.Any())
			{
				return;
			}
			writer.WriteStartArray(name);
			foreach (string value in values)
			{
				writer.WriteStringValue(value);
			}
			writer.WriteEndArray();
		}

		public static void WriteBase64StringValue(this Utf8JsonWriter writer, byte[] value, string format)
		{
			if (value == null)
			{
				writer.WriteNullValue();
			}
			else if (!(format == "U"))
			{
				if (!(format == "D"))
				{
					throw new ArgumentException("Format is not supported: '" + format + "'", "format");
				}
				writer.WriteBase64StringValue(value);
			}
			else
			{
				writer.WriteStringValue(TypeFormatters.ToBase64UrlString(value));
			}
		}

		public static void WriteNumberValue(this Utf8JsonWriter writer, DateTimeOffset value, string format)
		{
			if (format != "U")
			{
				throw new ArgumentOutOfRangeException(format, "Only 'U' format is supported when writing a DateTimeOffset as a Number.");
			}
			writer.WriteNumberValue(value.ToUnixTimeSeconds());
		}

		public static void WriteObjectValue(this Utf8JsonWriter writer, object value)
		{
			if (value != null)
			{
				if (!(value is IUtf8JsonSerializable utf8JsonSerializable))
				{
					if (!(value is byte[] array))
					{
						if (!(value is BinaryData binaryData))
						{
							if (!(value is JsonElement jsonElement))
							{
								if (!(value is int value2))
								{
									if (!(value is decimal value3))
									{
										if (!(value is double num))
										{
											if (!(value is float value4))
											{
												if (!(value is long value5))
												{
													if (!(value is string value6))
													{
														if (!(value is bool value7))
														{
															if (!(value is Guid value8))
															{
																if (!(value is DateTimeOffset value9))
																{
																	if (!(value is DateTime value10))
																	{
																		if (!(value is IEnumerable<KeyValuePair<string, object>> enumerable))
																		{
																			if (!(value is IEnumerable<object> enumerable2))
																			{
																				if (value is TimeSpan value11)
																				{
																					writer.WriteStringValue(value11, "P");
																					return;
																				}
																				throw new NotSupportedException("Not supported type " + value.GetType());
																			}
																			writer.WriteStartArray();
																			foreach (object item in enumerable2)
																			{
																				writer.WriteObjectValue(item);
																			}
																			writer.WriteEndArray();
																			return;
																		}
																		writer.WriteStartObject();
																		foreach (KeyValuePair<string, object> item2 in enumerable)
																		{
																			writer.WritePropertyName(item2.Key);
																			writer.WriteObjectValue(item2.Value);
																		}
																		writer.WriteEndObject();
																	}
																	else
																	{
																		writer.WriteStringValue(value10, "O");
																	}
																}
																else
																{
																	writer.WriteStringValue(value9, "O");
																}
															}
															else
															{
																writer.WriteStringValue(value8);
															}
														}
														else
														{
															writer.WriteBooleanValue(value7);
														}
													}
													else
													{
														writer.WriteStringValue(value6);
													}
												}
												else
												{
													writer.WriteNumberValue(value5);
												}
											}
											else
											{
												writer.WriteNumberValue(value4);
											}
										}
										else if (double.IsNaN(num))
										{
											writer.WriteStringValue("NaN");
										}
										else
										{
											writer.WriteNumberValue(num);
										}
									}
									else
									{
										writer.WriteNumberValue(value3);
									}
								}
								else
								{
									writer.WriteNumberValue(value2);
								}
							}
							else
							{
								jsonElement.WriteTo(writer);
							}
						}
						else
						{
							writer.WriteBase64StringValue(binaryData);
						}
					}
					else
					{
						writer.WriteBase64StringValue(array);
					}
				}
				else
				{
					utf8JsonSerializable.Write(writer);
				}
			}
			else
			{
				writer.WriteNullValue();
			}
		}
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal readonly struct VoidValue
	{
	}
	internal static class XElementExtensions
	{
		public static byte[] GetBytesFromBase64Value(this XElement element, string format)
		{
			if (!(format == "U"))
			{
				if (format == "D")
				{
					return Convert.FromBase64String(element.Value);
				}
				throw new ArgumentException("Format is not supported: '" + format + "'", "format");
			}
			return TypeFormatters.FromBase64UrlString(element.Value);
		}

		public static DateTimeOffset GetDateTimeOffsetValue(this XElement element, string format)
		{
			if (format == "U")
			{
				return DateTimeOffset.FromUnixTimeSeconds((long)element);
			}
			return TypeFormatters.ParseDateTimeOffset(element.Value, format);
		}

		public static TimeSpan GetTimeSpanValue(this XElement element, string format)
		{
			return TypeFormatters.ParseTimeSpan(element.Value, format);
		}

		public static object GetObjectValue(this XElement element, string format)
		{
			return element.Value;
		}
	}
	internal class XmlWriterContent : RequestContent
	{
		private readonly MemoryStream _stream;

		private readonly RequestContent _content;

		public XmlWriter XmlWriter { get; }

		public XmlWriterContent()
		{
			_stream = new MemoryStream();
			_content = RequestContent.Create(_stream);
			XmlWriter = new XmlTextWriter(_stream, Encoding.UTF8);
		}

		public override async Task WriteToAsync(Stream stream, CancellationToken cancellation)
		{
			XmlWriter.Flush();
			await _content.WriteToAsync(stream, cancellation).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override void WriteTo(Stream stream, CancellationToken cancellation)
		{
			XmlWriter.Flush();
			_content.WriteTo(stream, cancellation);
		}

		public override bool TryComputeLength(out long length)
		{
			XmlWriter.Flush();
			length = _stream.Length;
			return true;
		}

		public override void Dispose()
		{
			XmlWriter.Dispose();
			_content.Dispose();
			_stream.Dispose();
		}
	}
	internal static class XmlWriterExtensions
	{
		public static void WriteObjectValue(this XmlWriter writer, object value, string? nameHint)
		{
			if (value is IXmlSerializable xmlSerializable)
			{
				xmlSerializable.Write(writer, nameHint);
				return;
			}
			throw new NotImplementedException();
		}

		public static void WriteValue(this XmlWriter writer, DateTimeOffset value, string format)
		{
			writer.WriteValue(TypeFormatters.ToString(value, format));
		}

		public static void WriteValue(this XmlWriter writer, TimeSpan value, string format)
		{
			writer.WriteValue(TypeFormatters.ToString(value, format));
		}

		public static void WriteValue(this XmlWriter writer, byte[] value, string format)
		{
			writer.WriteValue(TypeFormatters.ToString(value, format));
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

			private List<Activity>? _links;

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
				if (_currentActivity == null)
				{
					if (_tagCollection == null)
					{
						_tagCollection = new ActivityTagsCollection();
					}
					_tagCollection.Add(name, value);
				}
				else
				{
					AddObjectTag(name, value);
				}
			}

			private List<ActivityLink>? GetActivitySourceLinkCollection()
			{
				if (_links == null)
				{
					return null;
				}
				List<ActivityLink> list = new List<ActivityLink>();
				foreach (Activity link in _links)
				{
					ActivityTagsCollection activityTagsCollection = new ActivityTagsCollection();
					foreach (KeyValuePair<string, string> tag in link.Tags)
					{
						activityTagsCollection.Add(tag.Key, tag.Value);
					}
					if (ActivityContext.TryParse(link.ParentId, link.TraceStateString, out var context))
					{
						ActivityLink item = new ActivityLink(context, activityTagsCollection);
						list.Add(item);
					}
				}
				return list;
			}

			public void AddLink(string traceparent, string? tracestate, IDictionary<string, string>? attributes)
			{
				Activity activity = new Activity("LinkedActivity");
				activity.SetParentId(traceparent);
				activity.SetIdFormat(ActivityIdFormat.W3C);
				activity.TraceStateString = tracestate;
				if (attributes != null)
				{
					foreach (KeyValuePair<string, string> attribute in attributes)
					{
						activity.AddTag(attribute.Key, attribute.Value);
					}
				}
				if (_links == null)
				{
					_links = new List<Activity>();
				}
				_links.Add(activity);
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
					_currentActivity.AddTag("az.schema_url", "https://opentelemetry.io/schemas/1.23.0");
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
					DiagnosticActivity diagnosticActivity = new DiagnosticActivity(_activityName);
					IEnumerable<Activity> links = _links;
					diagnosticActivity.Links = links ?? Array.Empty<Activity>();
					_currentActivity = diagnosticActivity;
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
				ActivityContext parentContext = ((_traceparent == null) ? default(ActivityContext) : ActivityContext.Parse(_traceparent, _tracestate));
				return _activitySource.StartActivity(_activityName, _kind, parentContext, _tagCollection, GetActivitySourceLinkCollection(), _startTime);
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
					_currentActivity?.AddTag(name, value);
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

		public void AddAttribute<T>(string name, T value, Func<T, string> format)
		{
			if (_activityAdapter != null && value != null)
			{
				string value2 = format(value);
				_activityAdapter.AddTag(name, value2);
			}
		}

		public void AddLink(string traceparent, string? tracestate, IDictionary<string, string>? attributes = null)
		{
			_activityAdapter?.AddLink(traceparent, tracestate, attributes);
		}

		public void Start()
		{
			(_activityAdapter?.Start())?.SetCustomProperty("az.sdk.scope", AzureSdkScopeValue);
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
				_activityAdapter?.MarkFailed(exception, ex.ErrorCode);
			}
			else
			{
				_activityAdapter?.MarkFailed(exception, null);
			}
		}

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
namespace Azure.Storage
{
	public class ClientSideEncryptionOptions
	{
		public ClientSideEncryptionVersion EncryptionVersion { get; }

		public IKeyEncryptionKey KeyEncryptionKey { get; set; }

		public IKeyEncryptionKeyResolver KeyResolver { get; set; }

		public string KeyWrapAlgorithm { get; set; }

		public ClientSideEncryptionOptions(ClientSideEncryptionVersion version)
		{
			EncryptionVersion = version;
		}
	}
	public enum ClientSideEncryptionVersion
	{
		[Obsolete("This version is considered insecure. Applications are encouraged to migrate to version 2.0 or to one of Azure Storage's server-side encryption solutions. See http://aka.ms/azstorageclientencryptionblog for more details.")]
		V1_0 = 1,
		V2_0
	}
	public class DownloadTransferValidationOptions
	{
		public StorageChecksumAlgorithm ChecksumAlgorithm { get; set; } = StorageChecksumAlgorithm.None;

		public bool AutoValidateChecksum { get; set; } = true;
	}
	internal static class AccountExtensions
	{
		internal static string ToPermissionsString(this AccountSasPermissions permissions)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if ((permissions & AccountSasPermissions.Read) == AccountSasPermissions.Read)
			{
				stringBuilder.Append('r');
			}
			if ((permissions & AccountSasPermissions.Write) == AccountSasPermissions.Write)
			{
				stringBuilder.Append('w');
			}
			if ((permissions & AccountSasPermissions.Delete) == AccountSasPermissions.Delete)
			{
				stringBuilder.Append('d');
			}
			if ((permissions & AccountSasPermissions.DeleteVersion) == AccountSasPermissions.DeleteVersion)
			{
				stringBuilder.Append('x');
			}
			if ((permissions & AccountSasPermissions.PermanentDelete) == AccountSasPermissions.PermanentDelete)
			{
				stringBuilder.Append('y');
			}
			if ((permissions & AccountSasPermissions.List) == AccountSasPermissions.List)
			{
				stringBuilder.Append('l');
			}
			if ((permissions & AccountSasPermissions.Add) == AccountSasPermissions.Add)
			{
				stringBuilder.Append('a');
			}
			if ((permissions & AccountSasPermissions.Create) == AccountSasPermissions.Create)
			{
				stringBuilder.Append('c');
			}
			if ((permissions & AccountSasPermissions.Update) == AccountSasPermissions.Update)
			{
				stringBuilder.Append('u');
			}
			if ((permissions & AccountSasPermissions.Process) == AccountSasPermissions.Process)
			{
				stringBuilder.Append('p');
			}
			if ((permissions & AccountSasPermissions.Tag) == AccountSasPermissions.Tag)
			{
				stringBuilder.Append('t');
			}
			if ((permissions & AccountSasPermissions.Filter) == AccountSasPermissions.Filter)
			{
				stringBuilder.Append('f');
			}
			if ((permissions & AccountSasPermissions.SetImmutabilityPolicy) == AccountSasPermissions.SetImmutabilityPolicy)
			{
				stringBuilder.Append('i');
			}
			return stringBuilder.ToString();
		}
	}
	public enum StorageChecksumAlgorithm
	{
		Auto,
		None,
		MD5,
		StorageCrc64
	}
	public class StorageCrc64HashAlgorithm : NonCryptographicHashAlgorithm
	{
		private ulong _uCRC;

		private const int _hashSizeBytes = 8;

		private StorageCrc64HashAlgorithm(ulong uCrc)
			: base(8)
		{
			_uCRC = uCrc;
		}

		public static StorageCrc64HashAlgorithm Create()
		{
			return new StorageCrc64HashAlgorithm(0uL);
		}

		public override void Reset()
		{
			_uCRC = 0uL;
		}

		public override void Append(ReadOnlySpan<byte> source)
		{
			_uCRC = StorageCrc64Calculator.ComputeSlicedSafe(source, _uCRC);
		}

		protected override void GetCurrentHashCore(Span<byte> destination)
		{
			BitConverter.GetBytes(_uCRC).CopyTo(destination);
		}
	}
	public static class StorageExtensions
	{
		public static IDisposable CreateServiceTimeoutScope(TimeSpan? timeout)
		{
			return HttpPipeline.CreateHttpMessagePropertiesScope(new Dictionary<string, object> { { "Azure.Storage.StorageServerTimeoutPolicy.Timeout", timeout } });
		}
	}
	public class StorageSharedKeyCredential
	{
		private byte[] _accountKeyValue;

		public string AccountName { get; }

		private byte[] AccountKeyValue
		{
			get
			{
				return Volatile.Read(ref _accountKeyValue);
			}
			set
			{
				Volatile.Write(ref _accountKeyValue, value);
			}
		}

		public StorageSharedKeyCredential(string accountName, string accountKey)
		{
			AccountName = accountName;
			SetAccountKey(accountKey);
		}

		public void SetAccountKey(string accountKey)
		{
			AccountKeyValue = Convert.FromBase64String(accountKey);
		}

		internal string ComputeHMACSHA256(string message)
		{
			return Convert.ToBase64String(new HMACSHA256(AccountKeyValue).ComputeHash(Encoding.UTF8.GetBytes(message)));
		}

		protected static string ComputeSasSignature(StorageSharedKeyCredential credential, string message)
		{
			return credential.ComputeHMACSHA256(message);
		}
	}
	public struct StorageTransferOptions : IEquatable<StorageTransferOptions>
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int? MaximumTransferLength
		{
			get
			{
				return checked((int?)MaximumTransferSize);
			}
			set
			{
				MaximumTransferSize = value;
			}
		}

		public long? MaximumTransferSize { get; set; }

		public int? MaximumConcurrency { get; set; }

		[EditorBrowsable(EditorBrowsableState.Never)]
		public int? InitialTransferLength
		{
			get
			{
				return checked((int?)InitialTransferSize);
			}
			set
			{
				InitialTransferSize = value;
			}
		}

		public long? InitialTransferSize { get; set; }

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			if (obj is StorageTransferOptions obj2)
			{
				return Equals(obj2);
			}
			return false;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return MaximumTransferSize.GetHashCode() ^ MaximumConcurrency.GetHashCode() ^ InitialTransferSize.GetHashCode();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool operator ==(StorageTransferOptions left, StorageTransferOptions right)
		{
			return left.Equals(right);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool operator !=(StorageTransferOptions left, StorageTransferOptions right)
		{
			return !(left == right);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Equals(StorageTransferOptions obj)
		{
			if (MaximumTransferSize == obj.MaximumTransferSize && MaximumConcurrency == obj.MaximumConcurrency)
			{
				return InitialTransferSize == obj.InitialTransferSize;
			}
			return false;
		}
	}
	public class TransferValidationOptions
	{
		public UploadTransferValidationOptions Upload { get; } = new UploadTransferValidationOptions();

		public DownloadTransferValidationOptions Download { get; } = new DownloadTransferValidationOptions();
	}
	public class UploadTransferValidationOptions
	{
		public StorageChecksumAlgorithm ChecksumAlgorithm { get; set; } = StorageChecksumAlgorithm.None;

		public ReadOnlyMemory<byte> PrecalculatedChecksum { get; set; }
	}
	internal static class Constants
	{
		internal static class ConnectionStrings
		{
			internal const int BlobEndpointPortNumber = 10000;

			internal const int QueueEndpointPortNumber = 10001;

			internal const int TableEndpointPortNumber = 10002;

			internal const string UseDevelopmentSetting = "UseDevelopmentStorage";

			internal const string DevelopmentProxyUriSetting = "DevelopmentStorageProxyUri";

			internal const string DefaultEndpointsProtocolSetting = "DefaultEndpointsProtocol";

			internal const string AccountNameSetting = "AccountName";

			internal const string AccountKeyNameSetting = "AccountKeyName";

			internal const string AccountKeySetting = "AccountKey";

			internal const string BlobEndpointSetting = "BlobEndpoint";

			internal const string QueueEndpointSetting = "QueueEndpoint";

			internal const string TableEndpointSetting = "TableEndpoint";

			internal const string FileEndpointSetting = "FileEndpoint";

			internal const string BlobSecondaryEndpointSetting = "BlobSecondaryEndpoint";

			internal const string QueueSecondaryEndpointSetting = "QueueSecondaryEndpoint";

			internal const string TableSecondaryEndpointSetting = "TableSecondaryEndpoint";

			internal const string FileSecondaryEndpointSetting = "FileSecondaryEndpoint";

			internal const string EndpointSuffixSetting = "EndpointSuffix";

			internal const string SharedAccessSignatureSetting = "SharedAccessSignature";

			internal const string DevStoreAccountName = "devstoreaccount1";

			internal const string DevStoreAccountKey = "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";

			internal const string SecondaryLocationAccountSuffix = "-secondary";

			internal const string DefaultEndpointSuffix = "core.windows.net";

			internal const string DefaultBlobHostnamePrefix = "blob";

			internal const string DefaultQueueHostnamePrefix = "queue";

			internal const string DefaultTableHostnamePrefix = "table";

			internal const string DefaultFileHostnamePrefix = "file";
		}

		internal static class HeaderNames
		{
			public const string XMsPrefix = "x-ms-";

			public const string MetadataPrefix = "x-ms-meta-";

			public const string ErrorCode = "x-ms-error-code";

			public const string RequestId = "x-ms-request-id";

			public const string ClientRequestId = "x-ms-client-request-id";

			public const string Date = "x-ms-date";

			public const string SharedKey = "SharedKey";

			public const string Authorization = "Authorization";

			public const string ContentEncoding = "Content-Encoding";

			public const string ContentLanguage = "Content-Language";

			public const string ContentLength = "Content-Length";

			public const string ContentMD5 = "Content-MD5";

			public const string ContentType = "Content-Type";

			public const string IfModifiedSince = "If-Modified-Since";

			public const string IfMatch = "If-Match";

			public const string IfNoneMatch = "If-None-Match";

			public const string IfUnmodifiedSince = "If-Unmodified-Since";

			public const string Range = "Range";

			public const string ContentRange = "Content-Range";

			public const string VersionId = "x-ms-version-id";

			public const string LeaseTime = "x-ms-lease-time";

			public const string LeaseId = "x-ms-lease-id";

			public const string LastModified = "Last-Modified";

			public const string ETag = "ETag";
		}

		internal static class ErrorCodes
		{
			public const string InternalError = "InternalError";

			public const string OperationTimedOut = "OperationTimedOut";

			public const string ServerBusy = "ServerBusy";

			public const string ContainerAlreadyExists = "ContainerAlreadyExists";

			public const string BlobAlreadyExists = "BlobAlreadyExists";
		}

		internal static class Blob
		{
			internal static class Append
			{
				public const int Pre_2022_11_02_MaxAppendBlockBytes = 4194304;

				public const int MaxAppendBlockBytes = 104857600;

				public const int MaxBlocks = 50000;
			}

			internal static class Block
			{
				public const int DefaultConcurrentTransfersCount = 5;

				public const int DefaultInitalDownloadRangeSize = 268435456;

				public const int Pre_2019_12_12_MaxUploadBytes = 268435456;

				public const long MaxUploadBytes = 5242880000L;

				public const int MaxDownloadBytes = 268435456;

				public const int Pre_2019_12_12_MaxStageBytes = 104857600;

				public const long MaxStageBytes = 4194304000L;

				public const int MaxBlocks = 50000;
			}

			internal static class Page
			{
				public const int PageSizeBytes = 512;

				public const int MaxPageBlockBytes = 4194304;
			}

			internal static class Container
			{
				public const string Name = "Blob Container";

				public const string RootName = "$root";

				public const string LogsName = "$logs";

				public const string WebName = "$web";
			}

			internal static class Lease
			{
				public const int InfiniteLeaseDuration = -1;
			}

			public const int HttpsPort = 443;

			public const string UriSubDomain = "blob";

			public const int QuickQueryDownloadSize = 4194304;

			public const string MetadataHeaderPrefix = "x-ms-meta-";

			public const string ObjectReplicationRulesHeaderPrefix = "x-ms-or-";
		}

		internal static class File
		{
			internal static class Lease
			{
				public const long InfiniteLeaseDuration = -1L;
			}

			internal static class Errors
			{
				public const string ShareUsageBytesOverflow = "ShareUsageBytes exceeds int.MaxValue. Use ShareUsageInBytes instead.";

				public const string LeaseNotPresentWithFileOperation = "LeaseNotPresentWithFileOperation";
			}

			internal static class Share
			{
				public const string Name = "Share";
			}

			public const string UriSubDomain = "file";

			public const string FileAttributesNone = "None";

			public const string FileTimeNow = "Now";

			public const string Preserve = "Preserve";

			public const string Source = "Source";

			public const string FilePermissionInherit = "Inherit";

			public const int MaxFilePermissionHeaderSize = 8192;

			public const int MaxFileUpdateRange = 4194304;

			public const string FileTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffff'Z'";

			public const string SnapshotParameterName = "sharesnapshot";

			public const string SmbProtocol = "SMB";

			public const string NfsProtocol = "NFS";
		}

		internal static class DataLake
		{
			public const string BlobUriSuffix = "blob";

			public const string BlobUriPeriodSuffix = ".blob.";

			public const string DfsUriSuffix = "dfs";

			public const string DfsUriPeriodSuffix = ".dfs.";

			public const string ErrorKey = "error";

			public const string ErrorCodeKey = "code";

			public const string ErrorMessageKey = "message";

			public const string AlreadyExists = "ContainerAlreadyExists";

			public const string FilesystemNotFound = "FilesystemNotFound";

			public const string PathNotFound = "PathNotFound";

			public const string PathAlreadyExists = "PathAlreadyExists";

			public const int DefaultConcurrentTransfersCount = 5;

			public const int Pre_2019_12_12_MaxAppendBytes = 104857600;

			public const long MaxAppendBytes = 4194304000L;

			public const string IsDirectoryKey = "hdi_isFolder";

			public const string FileSystemName = "FileSystem";

			public const string DeletionId = "deletionid";

			public const string DirectoryResourceType = "directory";

			public const string EncryptionContextHeaderName = "x-ms-encryption-context";

			public const string OwnerHeaderName = "x-ms-owner";

			public const string GroupHeaderName = "x-ms-group";

			public const string PermissionsHeaderName = "x-ms-permissions";
		}

		internal static class Queue
		{
			public const int MaxMessagesDequeue = 32;

			public const int QueueMessageMaxBytes = 65536;

			public const int StatusCodeNoContent = 204;

			public const string MessagesUri = "messages";

			public const string UriSubDomain = "queue";

			public const string QueueTraitsMetadata = "metadata";
		}

		internal static class ChangeFeed
		{
			internal static class Event
			{
				public const string Topic = "topic";

				public const string Subject = "subject";

				public const string EventType = "eventType";

				public const string EventTime = "eventTime";

				public const string EventId = "id";

				public const string Data = "data";

				public const string SchemaVersion = "schemaVersion";

				public const string MetadataVersion = "metadataVersion";
			}

			internal static class EventData
			{
				public const string Api = "api";

				public const string ClientRequestId = "clientRequestId";

				public const string RequestId = "requestId";

				public const string Etag = "etag";

				public const string ContentType = "contentType";

				public const string ContentLength = "contentLength";

				public const string BlobType = "blobType";

				public const string BlobVersionLower = "blobVersion";

				public const string ContainerVersion = "containerVersion";

				public const string BlobTier = "blobTier";

				public const string BlockBlob = "BlockBlob";

				public const string PageBlob = "PageBlob";

				public const string AppendBlob = "AppendBlob";

				public const string ContentOffset = "contentOffset";

				public const string DestinationUrl = "destinationUrl";

				public const string SourceUrl = "sourceUrl";

				public const string Url = "url";

				public const string Recursive = "recursive";

				public const string Sequencer = "sequencer";

				public const string PreviousInfo = "previousInfo";

				public const string Snapshot = "snapshot";

				public const string BlobPropertiesUpdated = "blobPropertiesUpdated";

				public const string AsyncOperationInfo = "asyncOperationInfo";

				public const string Current = "current";

				public const string Previous = "previous";

				public const string DestinationTier = "DestinationTier";

				public const string WasAsyncOperation = "WasAsyncOperation";

				public const string CopyId = "CopyId";

				public const string SoftDeletedSnapshot = "SoftDeleteSnapshot";

				public const string WasBlobSoftDeleted = "WasBlobSoftDeleted";

				public const string BlobVersion = "BlobVersion";

				public const string LastVersion = "LastVersion";

				public const string PreviousTier = "PreviousTier";

				public const string BlobTagsUpdated = "blobTagsUpdated";
			}

			public const string ChangeFeedContainerName = "$blobchangefeed";

			public const string SegmentPrefix = "idx/segments/";

			public const string InitalizationManifestPath = "/0000/";

			public const string InitalizationSegment = "1601";

			public const string MetaSegmentsPath = "meta/segments.json";

			public const long ChunkBlockDownloadSize = 1048576L;

			public const int DefaultPageSize = 5000;

			public const int LazyLoadingBlobStreamBlockSize = 3072;
		}

		internal static class QuickQuery
		{
			public const string SqlQueryType = "SQL";

			public const string Data = "data";

			public const string BytesScanned = "bytesScanned";

			public const string TotalBytes = "totalBytes";

			public const string Fatal = "fatal";

			public const string Name = "name";

			public const string Description = "description";

			public const string Position = "position";

			public const string DataRecordName = "com.microsoft.azure.storage.queryBlobContents.resultData";

			public const string ProgressRecordName = "com.microsoft.azure.storage.queryBlobContents.progress";

			public const string ErrorRecordName = "com.microsoft.azure.storage.queryBlobContents.error";

			public const string EndRecordName = "com.microsoft.azure.storage.queryBlobContents.end";

			public const string ArrowFieldTypeInt64 = "int64";

			public const string ArrowFieldTypeBool = "bool";

			public const string ArrowFieldTypeTimestamp = "timestamp[ms]";

			public const string ArrowFieldTypeString = "string";

			public const string ArrowFieldTypeDouble = "double";

			public const string ArrowFieldTypeDecimal = "decimal";
		}

		internal static class Sas
		{
			internal static class Permissions
			{
				public const char Read = 'r';

				public const char Write = 'w';

				public const char Delete = 'd';

				public const char DeleteBlobVersion = 'x';

				public const char List = 'l';

				public const char Add = 'a';

				public const char Update = 'u';

				public const char Process = 'p';

				public const char Create = 'c';

				public const char Tag = 't';

				public const char FilterByTags = 'f';

				public const char Move = 'm';

				public const char Execute = 'e';

				public const char SetImmutabilityPolicy = 'i';

				public const char ManageOwnership = 'o';

				public const char ManageAccessControl = 'p';

				public const char PermanentDelete = 'y';
			}

			internal static class Parameters
			{
				public const string Version = "sv";

				public const string VersionUpper = "SV";

				public const string Services = "ss";

				public const string ServicesUpper = "SS";

				public const string ResourceTypes = "srt";

				public const string ResourceTypesUpper = "SRT";

				public const string Protocol = "spr";

				public const string ProtocolUpper = "SPR";

				public const string StartTime = "st";

				public const string StartTimeUpper = "ST";

				public const string ExpiryTime = "se";

				public const string ExpiryTimeUpper = "SE";

				public const string IPRange = "sip";

				public const string IPRangeUpper = "SIP";

				public const string Identifier = "si";

				public const string IdentifierUpper = "SI";

				public const string Resource = "sr";

				public const string ResourceUpper = "SR";

				public const string Permissions = "sp";

				public const string PermissionsUpper = "SP";

				public const string Signature = "sig";

				public const string SignatureUpper = "SIG";

				public const string KeyObjectId = "skoid";

				public const string KeyObjectIdUpper = "SKOID";

				public const string KeyTenantId = "sktid";

				public const string KeyTenantIdUpper = "SKTID";

				public const string KeyStart = "skt";

				public const string KeyStartUpper = "SKT";

				public const string KeyExpiry = "ske";

				public const string KeyExpiryUpper = "SKE";

				public const string KeyService = "sks";

				public const string KeyServiceUpper = "SKS";

				public const string KeyVersion = "skv";

				public const string KeyVersionUpper = "SKV";

				public const string CacheControl = "rscc";

				public const string CacheControlUpper = "RSCC";

				public const string ContentDisposition = "rscd";

				public const string ContentDispositionUpper = "RSCD";

				public const string ContentEncoding = "rsce";

				public const string ContentEncodingUpper = "RSCE";

				public const string ContentLanguage = "rscl";

				public const string ContentLanguageUpper = "RSCL";

				public const string ContentType = "rsct";

				public const string ContentTypeUpper = "RSCT";

				public const string PreauthorizedAgentObjectId = "saoid";

				public const string PreauthorizedAgentObjectIdUpper = "SAOID";

				public const string AgentObjectId = "suoid";

				public const string AgentObjectIdUpper = "SUOID";

				public const string CorrelationId = "scid";

				public const string CorrelationIdUpper = "SCID";

				public const string DirectoryDepth = "sdd";

				public const string DirectoryDepthUpper = "SDD";

				public const string EncryptionScope = "ses";

				public const string EncryptionScopeUpper = "SES";
			}

			internal static class Resource
			{
				public const string BlobSnapshot = "bs";

				public const string BlobVersion = "bv";

				public const string Blob = "b";

				public const string Container = "c";

				public const string File = "f";

				public const string Share = "s";

				public const string Directory = "d";
			}

			internal static class AccountServices
			{
				public const char Blob = 'b';

				public const char Queue = 'q';

				public const char File = 'f';

				public const char Table = 't';
			}

			internal static class AccountResources
			{
				public const char Service = 's';

				public const char Container = 'c';

				public const char Object = 'o';
			}

			public static readonly List<char> ValidPermissionsInOrder = new List<char>
			{
				'r', 'a', 'c', 'w', 'd', 'x', 'y', 'l', 't', 'u',
				'p', 'f', 'm', 'e', 'i'
			};

			internal static readonly int[] PathStylePorts = new int[20]
			{
				10000, 10001, 10002, 10003, 10004, 10100, 10101, 10102, 10103, 10104,
				11000, 11001, 11002, 11003, 11004, 11100, 11101, 11102, 11103, 11104
			};
		}

		internal static class ClientSideEncryption
		{
			internal static class V2
			{
				public const int EncryptionRegionDataSize = 4194304;

				public const int NonceSize = 12;

				public const int TagSize = 16;

				public const int EncryptionRegionTotalSize = 4194332;

				public const int WrappedDataVersionLength = 8;
			}

			public const string HttpMessagePropertyKeyV1 = "Azure.Storage.StorageTelemetryPolicy.ClientSideEncryption.V1";

			public const string HttpMessagePropertyKeyV2 = "Azure.Storage.StorageTelemetryPolicy.ClientSideEncryption.V2";

			public const string AgentMetadataKey = "EncryptionLibrary";

			public const string AesCbcPkcs5Padding = "AES/CBC/PKCS5Padding";

			public const string AesCbcNoPadding = "AES/CBC/NoPadding";

			public const string EncryptionDataKey = "encryptiondata";

			public const string EncryptionMode = "FullBlob";

			public const int EncryptionBlockSize = 16;

			public const int EncryptionKeySizeBits = 256;

			public const string XMsRange = "x-ms-range";

			public const string BCRYPT_AES_ALGORITHM = "AES";

			public const string BCRYPT_CHAIN_MODE_GCM = "ChainingModeGCM";

			public const string BCRYPT_CHAINING_MODE = "ChainingMode";

			internal const string BCryptdll = "BCrypt.dll";
		}

		internal static class Xml
		{
			internal const string Code = "Code";

			internal const string Message = "Message";
		}

		internal static class GeoRedundantRead
		{
			internal const string AlternateHostKey = "AlternateHostKey";

			internal const string ResourceNotReplicated = "ResourceNotReplicated";
		}

		internal static class HttpStatusCode
		{
			internal const int NotFound = 404;

			internal const int NotModified = 304;
		}

		internal static class ServerTimeout
		{
			internal const string HttpMessagePropertyKey = "Azure.Storage.StorageServerTimeoutPolicy.Timeout";

			internal const string QueryParameterKey = "timeout";
		}

		internal static class CopyHttpAuthorization
		{
			internal static readonly string[] Scopes = new string[1] { "https://storage.azure.com/.default" };

			internal const string BearerScheme = "Bearer";
		}

		public const int KB = 1024;

		public const int MB = 1048576;

		public const int GB = 1073741824;

		public const long TB = 1099511627776L;

		public const int Base16 = 16;

		public const int MaxReliabilityRetries = 5;

		public const int MaxIdleTimeMs = 120000;

		public const string DefaultSasVersion = "2023-11-03";

		public const int MaxHashRequestDownloadRange = 4194304;

		public const int DefaultBufferSize = 4194304;

		public const int LargeBufferSize = 8388608;

		public const int LargeUploadThreshold = 104857600;

		public const int DefaultStreamingDownloadSize = 4194304;

		public const int DefaultStreamCopyBufferSize = 81920;

		public const int DefaultDownloadCopyBufferSize = 16384;

		public const int StorageCrc64SizeInBytes = 8;

		public const int MD5SizeInBytes = 16;

		public const bool DefaultTrimBlobNameSlashes = true;

		public const string CloseAllHandles = "*";

		public const string Wildcard = "*";

		public const string BlockNameFormat = "Block_{0:D5}";

		public const string SasTimeFormatSeconds = "yyyy-MM-ddTHH:mm:ssZ";

		public const string SasTimeFormatSubSeconds = "yyyy-MM-ddTHH:mm:ss.fffffffZ";

		public const string SasTimeFormatMinutes = "yyyy-MM-ddTHH:mmZ";

		public const string SasTimeFormatDays = "yyyy-MM-dd";

		public const string SnapshotParameterName = "snapshot";

		public const string VersionIdParameterName = "versionid";

		public const string ShareSnapshotParameterName = "sharesnapshot";

		public const string Https = "https";

		public const string Http = "http";

		public const string PercentSign = "%";

		public const string EncodedPercentSign = "%25";

		public const string QueryDelimiter = "?";

		public const string PathBackSlashDelimiter = "/";

		public const string FalseName = "false";

		public const string TrueName = "true";

		public const string ErrorCode = "Code";

		public const string ErrorMessage = "Message";

		public const string CommaString = ",";

		public const char CommaChar = ',';

		public const string ContentTypeApplicationXml = "application/xml";

		public const string ContentTypeApplicationJson = "application/json";

		public const string ErrorPropertyKey = "error";

		public const string DetailPropertyKey = "detail";

		public const string MessagePropertyKey = "message";

		public const string CodePropertyKey = "code";

		public const string Iso8601Format = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";

		public const string DisableRequestConditionsValidationSwitchName = "Azure.Storage.DisableRequestConditionsValidation";

		public const string DisableRequestConditionsValidationEnvVar = "AZURE_STORAGE_DISABLE_REQUEST_CONDITIONS_VALIDATION";

		public const string DefaultScope = "/.default";
	}
	internal class Errors
	{
		public static ArgumentException AccountMismatch(string accountNameCredential, string accountNameValue)
		{
			return new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Account Name Mismatch: {0} != {1}", accountNameCredential, accountNameValue));
		}

		public static InvalidOperationException AccountSasMissingData()
		{
			return new InvalidOperationException("Account SAS is missing at least one of these: ExpiryTime, Permissions, Service, or ResourceType");
		}

		public static ArgumentNullException ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		public static ArgumentException InvalidArgument(string paramName)
		{
			return new ArgumentException(paramName + " is invalid");
		}

		public static ArgumentException InvalidResourceType(char s)
		{
			return new ArgumentException($"Invalid resource type: '{s}'");
		}

		public static InvalidOperationException TaskIncomplete()
		{
			return new InvalidOperationException("Task is not completed");
		}

		public static FormatException InvalidFormat(string err)
		{
			return new FormatException(err);
		}

		public static ArgumentException ParsingConnectionStringFailed()
		{
			return new ArgumentException("Connection string parsing error");
		}

		public static ArgumentOutOfRangeException InvalidSasProtocol(string protocol, string sasProtocol)
		{
			return new ArgumentOutOfRangeException(protocol, "Invalid " + sasProtocol + " value");
		}

		public static ArgumentException InvalidService(char s)
		{
			return new ArgumentException($"Invalid service: '{s}'");
		}

		public static ArgumentException InsufficientStorageTransferOptions(long streamLength, long statedMaxBlockSize, long necessaryMinBlockSize)
		{
			return new ArgumentException($"Cannot upload {streamLength} bytes with a maximum transfer size of {statedMaxBlockSize} bytes per block. Please increase the StorageTransferOptions.MaximumTransferSize to at least {necessaryMinBlockSize}.");
		}

		public static InvalidDataException HashMismatch(string hashHeaderName)
		{
			return new InvalidDataException(hashHeaderName + " did not match hash of recieved data.");
		}

		public static InvalidDataException ChecksumMismatch(ReadOnlySpan<byte> left, ReadOnlySpan<byte> right)
		{
			return new InvalidDataException("Compared checksums did not match. Invalid data may have been written to the destination. Left: " + Convert.ToBase64String(left.ToArray()) + " Right: " + Convert.ToBase64String(right.ToArray()));
		}

		public static InvalidDataException HashMismatchOnStreamedDownload(string mismatchedRange)
		{
			return new InvalidDataException("Detected invalid data while streaming to the destination. Range " + mismatchedRange + " produced mismatched checksum.");
		}

		public static ArgumentException PrecalculatedHashNotSupportedOnSplit()
		{
			return new ArgumentException("Precalculated checksum not supported when potentially partitioning an upload.");
		}

		public static ArgumentException CannotDeferTransactionalHashVerification()
		{
			return new ArgumentException("Cannot defer transactional hash verification. Returned hash is unavailable to caller.");
		}

		public static ArgumentException CannotInitializeWriteStreamWithData()
		{
			return new ArgumentException("Initialized buffer for StorageWriteStream must be empty.");
		}

		internal static void VerifyStreamPosition(Stream stream, string streamName)
		{
			if (stream != null && stream.CanSeek && stream.Length > 0 && stream.Position >= stream.Length)
			{
				throw new ArgumentException(streamName + ".Position must be less than " + streamName + ".Length. Please set " + streamName + ".Position to the start of the data to upload.");
			}
		}

		public static void ThrowIfParamNull(object obj, string paramName)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("Param: \"" + paramName + "\" is null");
			}
		}

		internal static void CheckCryptKeySize(int keySizeInBytes)
		{
			if (keySizeInBytes != 16 && keySizeInBytes != 24 && keySizeInBytes != 32)
			{
				throw new CryptographicException("Specified key is not a valid size for this algorithm.");
			}
		}

		public static CryptographicException CryptographyAuthTagMismatch()
		{
			throw new CryptographicException("The computed authentication tag did not match the input authentication tag.");
		}

		public static ArgumentException CryptographyPlaintextCiphertextLengthMismatch()
		{
			throw new ArgumentException("Plaintext and ciphertext must have the same length.");
		}

		public static ArgumentException CryptographyInvalidNonceLength()
		{
			throw new ArgumentException("The specified nonce is not a valid size for this algorithm.");
		}

		public static ArgumentException CryptographyInvalidTagLength()
		{
			throw new ArgumentException("The specified tag is not a valid size for this algorithm.");
		}
	}
	internal class GeoRedundantReadPolicy : HttpPipelineSynchronousPolicy
	{
		private readonly string _secondaryStorageHost;

		public GeoRedundantReadPolicy(Uri secondaryStorageUri)
		{
			if (secondaryStorageUri == null)
			{
				throw Errors.ArgumentNull("secondaryStorageUri");
			}
			_secondaryStorageHost = secondaryStorageUri.Host;
		}

		public override void OnSendingRequest(HttpMessage message)
		{
			if (message.Request.Method != RequestMethod.Get && message.Request.Method != RequestMethod.Head)
			{
				return;
			}
			object value;
			string text = (message.TryGetProperty("AlternateHostKey", out value) ? (value as string) : null);
			object value2;
			if (text == null)
			{
				message.SetProperty("AlternateHostKey", _secondaryStorageHost);
			}
			else if (!message.TryGetProperty("ResourceNotReplicated", out value2) || !(bool)value2)
			{
				string host = message.Request.Uri.Host;
				if (message.HasResponse && message.Response.Status == 404 && host == _secondaryStorageHost)
				{
					message.SetProperty("ResourceNotReplicated", true);
				}
				message.Request.Uri.Host = text;
				message.SetProperty("AlternateHostKey", host);
			}
		}
	}
	internal sealed class SharedAccessSignatureCredentials
	{
		public string SasToken { get; }

		public SharedAccessSignatureCredentials(string sasToken)
		{
			SasToken = sasToken;
		}
	}
	internal static class StorageExceptionExtensions
	{
		public static string GetErrorCode(this Response response, string errorCode)
		{
			if (string.IsNullOrEmpty(errorCode))
			{
				response.Headers.TryGetValue("x-ms-error-code", out errorCode);
			}
			return errorCode;
		}

		public static bool IsUnavailable<T>(this Response<T> response)
		{
			return (response?.GetRawResponse().Status ?? 0) == 304;
		}

		public static Response<T> AsNoBodyResponse<T>(this Response rawResponse)
		{
			return new NoBodyResponse<T>(rawResponse);
		}
	}
	internal class StorageConnectionString
	{
		private static readonly KeyValuePair<string, Func<string, bool>> s_useDevelopmentStorageSetting = Setting("UseDevelopmentStorage", "true");

		private static readonly KeyValuePair<string, Func<string, bool>> s_developmentStorageProxyUriSetting = Setting("DevelopmentStorageProxyUri", IsValidUri);

		private static readonly KeyValuePair<string, Func<string, bool>> s_defaultEndpointsProtocolSetting = Setting("DefaultEndpointsProtocol", "http", "https");

		private static readonly KeyValuePair<string, Func<string, bool>> s_accountNameSetting = Setting("AccountName");

		private static readonly KeyValuePair<string, Func<string, bool>> s_accountKeyNameSetting = Setting("AccountKeyName");

		private static readonly KeyValuePair<string, Func<string, bool>> s_accountKeySetting = Setting("AccountKey", IsValidBase64String);

		private static readonly KeyValuePair<string, Func<string, bool>> s_blobEndpointSetting = Setting("BlobEndpoint", IsValidUri);

		private static readonly KeyValuePair<string, Func<string, bool>> s_queueEndpointSetting = Setting("QueueEndpoint", IsValidUri);

		private static readonly KeyValuePair<string, Func<string, bool>> s_fileEndpointSetting = Setting("FileEndpoint", IsValidUri);

		private static readonly KeyValuePair<string, Func<string, bool>> s_tableEndpointSetting = Setting("TableEndpoint", IsValidUri);

		private static readonly KeyValuePair<string, Func<string, bool>> s_blobSecondaryEndpointSetting = Setting("BlobSecondaryEndpoint", IsValidUri);

		private static readonly KeyValuePair<string, Func<string, bool>> s_queueSecondaryEndpointSetting = Setting("QueueSecondaryEndpoint", IsValidUri);

		private static readonly KeyValuePair<string, Func<string, bool>> s_fileSecondaryEndpointSetting = Setting("FileSecondaryEndpoint", IsValidUri);

		private static readonly KeyValuePair<string, Func<string, bool>> s_tableSecondaryEndpointSetting = Setting("TableSecondaryEndpoint", IsValidUri);

		private static readonly KeyValuePair<string, Func<string, bool>> s_endpointSuffixSetting = Setting("EndpointSuffix", IsValidDomain);

		private static readonly KeyValuePair<string, Func<string, bool>> s_sharedAccessSignatureSetting = Setting("SharedAccessSignature");

		private static StorageConnectionString s_devStoreAccount;

		internal string _accountName;

		private static readonly Func<IDictionary<string, string>, IDictionary<string, string>> s_validCredentials = MatchesOne(MatchesAll(AllRequired(s_accountNameSetting, s_accountKeySetting), Optional(s_accountKeyNameSetting), None(s_sharedAccessSignatureSetting)), MatchesAll(AllRequired(s_sharedAccessSignatureSetting), Optional(s_accountNameSetting), None(s_accountKeySetting, s_accountKeyNameSetting)), None(s_accountNameSetting, s_accountKeySetting, s_accountKeyNameSetting, s_sharedAccessSignatureSetting));

		internal static bool UseV1MD5 => true;

		public static StorageConnectionString DevelopmentStorageAccount
		{
			get
			{
				if (s_devStoreAccount == null)
				{
					s_devStoreAccount = GetDevelopmentStorageAccount(null);
				}
				return s_devStoreAccount;
			}
		}

		internal bool IsDevStoreAccount { get; set; }

		internal string EndpointSuffix { get; set; }

		internal IDictionary<string, string> Settings { get; set; }

		internal bool DefaultEndpoints { get; set; }

		public Uri BlobEndpoint => BlobStorageUri.PrimaryUri;

		public Uri QueueEndpoint => QueueStorageUri.PrimaryUri;

		public Uri TableEndpoint => TableStorageUri.PrimaryUri;

		public Uri FileEndpoint => FileStorageUri.PrimaryUri;

		public (Uri PrimaryUri, Uri SecondaryUri) BlobStorageUri { get; set; }

		public (Uri PrimaryUri, Uri SecondaryUri) QueueStorageUri { get; set; }

		public (Uri PrimaryUri, Uri SecondaryUri) TableStorageUri { get; set; }

		public (Uri PrimaryUri, Uri SecondaryUri) FileStorageUri { get; set; }

		public object Credentials { get; set; }

		public StorageConnectionString(object storageCredentials, (Uri Primary, Uri Secondary) blobStorageUri = default((Uri Primary, Uri Secondary)), (Uri Primary, Uri Secondary) queueStorageUri = default((Uri Primary, Uri Secondary)), (Uri Primary, Uri Secondary) tableStorageUri = default((Uri Primary, Uri Secondary)), (Uri Primary, Uri Secondary) fileStorageUri = default((Uri Primary, Uri Secondary)))
		{
			Credentials = storageCredentials;
			BlobStorageUri = blobStorageUri;
			QueueStorageUri = queueStorageUri;
			TableStorageUri = tableStorageUri;
			FileStorageUri = fileStorageUri;
			DefaultEndpoints = false;
		}

		public static StorageConnectionString Parse(string connectionString)
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				throw Errors.ArgumentNull("connectionString");
			}
			if (ParseCore(connectionString, out var accountInformation, delegate(string err)
			{
				throw Errors.InvalidFormat(err);
			}))
			{
				return accountInformation;
			}
			throw Errors.ParsingConnectionStringFailed();
		}

		public static bool TryParse(string connectionString, out StorageConnectionString account)
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				account = null;
				return false;
			}
			try
			{
				return ParseCore(connectionString, out account, delegate
				{
				});
			}
			catch (Exception)
			{
				account = null;
				return false;
			}
		}

		private static StorageConnectionString GetDevelopmentStorageAccount(Uri proxyUri)
		{
			UriBuilder obj = ((proxyUri != null) ? new UriBuilder(proxyUri.Scheme, proxyUri.Host) : new UriBuilder("http", "127.0.0.1"));
			obj.Path = "devstoreaccount1";
			obj.Port = 10000;
			Uri uri = obj.Uri;
			obj.Port = 10002;
			Uri uri2 = obj.Uri;
			obj.Port = 10001;
			Uri uri3 = obj.Uri;
			obj.Path = "devstoreaccount1-secondary";
			obj.Port = 10000;
			Uri uri4 = obj.Uri;
			obj.Port = 10001;
			Uri uri5 = obj.Uri;
			obj.Port = 10002;
			Uri uri6 = obj.Uri;
			StorageConnectionString storageConnectionString = new StorageConnectionString(new StorageSharedKeyCredential("devstoreaccount1", "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw=="), (Primary: uri, Secondary: uri4), (Primary: uri3, Secondary: uri5), (Primary: uri2, Secondary: uri6), (Primary: null, Secondary: null));
			storageConnectionString.Settings = new Dictionary<string, string>();
			storageConnectionString.Settings.Add("UseDevelopmentStorage", "true");
			if (proxyUri != null)
			{
				storageConnectionString.Settings.Add("DevelopmentStorageProxyUri", proxyUri.AbsoluteUri);
			}
			storageConnectionString.IsDevStoreAccount = true;
			return storageConnectionString;
		}

		internal static bool ParseCore(string connectionString, out StorageConnectionString accountInformation, Action<string> error)
		{
			IDictionary<string, string> settings = ParseStringIntoSettings(connectionString, error);
			if (settings == null)
			{
				accountInformation = null;
				return false;
			}
			if (MatchesSpecification(settings, AllRequired(s_useDevelopmentStorageSetting), Optional(s_developmentStorageProxyUriSetting)))
			{
				accountInformation = (settings.TryGetValue("DevelopmentStorageProxyUri", out var value) ? GetDevelopmentStorageAccount(new Uri(value)) : DevelopmentStorageAccount);
				accountInformation.Settings = s_validCredentials(settings);
				return true;
			}
			Func<IDictionary<string, string>, IDictionary<string, string>> func = Optional(s_blobEndpointSetting, s_blobSecondaryEndpointSetting, s_queueEndpointSetting, s_queueSecondaryEndpointSetting, s_fileEndpointSetting, s_fileSecondaryEndpointSetting, s_tableEndpointSetting, s_tableSecondaryEndpointSetting);
			Func<IDictionary<string, string>, IDictionary<string, string>> func2 = AtLeastOne(s_blobEndpointSetting, s_queueEndpointSetting, s_fileEndpointSetting, s_tableEndpointSetting);
			Func<IDictionary<string, string>, IDictionary<string, string>> func3 = Optional(s_blobSecondaryEndpointSetting, s_queueSecondaryEndpointSetting, s_fileSecondaryEndpointSetting, s_tableSecondaryEndpointSetting);
			Func<IDictionary<string, string>, IDictionary<string, string>> func4 = MatchesExactly(MatchesAll(MatchesOne(MatchesAll(AllRequired(s_accountKeySetting), Optional(s_accountKeyNameSetting)), AllRequired(s_sharedAccessSignatureSetting)), AllRequired(s_accountNameSetting), func, Optional(s_defaultEndpointsProtocolSetting, s_endpointSuffixSetting)));
			Func<IDictionary<string, string>, IDictionary<string, string>> func5 = MatchesExactly(MatchesAll(s_validCredentials, func2, func3));
			bool matchesAutomaticEndpointsSpec = MatchesSpecification(settings, func4);
			bool flag = MatchesSpecification(settings, func5);
			if (matchesAutomaticEndpointsSpec || flag)
			{
				if (matchesAutomaticEndpointsSpec && !settings.ContainsKey("DefaultEndpointsProtocol"))
				{
					settings.Add("DefaultEndpointsProtocol", "https");
				}
				string primary = settingOrDefault("BlobEndpoint");
				string primary2 = settingOrDefault("QueueEndpoint");
				string primary3 = settingOrDefault("TableEndpoint");
				string primary4 = settingOrDefault("FileEndpoint");
				string secondary = settingOrDefault("BlobSecondaryEndpoint");
				string secondary2 = settingOrDefault("QueueSecondaryEndpoint");
				string secondary3 = settingOrDefault("TableSecondaryEndpoint");
				string secondary4 = settingOrDefault("FileSecondaryEndpoint");
				string sasToken = settingOrDefault("SharedAccessSignature");
				if (s_isValidEndpointPair(primary, secondary) && s_isValidEndpointPair(primary2, secondary2) && s_isValidEndpointPair(primary3, secondary3) && s_isValidEndpointPair(primary4, secondary4))
				{
					accountInformation = new StorageConnectionString(GetCredentials(settings), createStorageUri(primary, secondary, sasToken, ConstructBlobEndpoint), createStorageUri(primary2, secondary2, sasToken, ConstructQueueEndpoint), createStorageUri(primary3, secondary3, sasToken, ConstructTableEndpoint), createStorageUri(primary4, secondary4, sasToken, ConstructFileEndpoint))
					{
						EndpointSuffix = settingOrDefault("EndpointSuffix"),
						Settings = s_validCredentials(settings)
					};
					accountInformation._accountName = settingOrDefault("AccountName");
					return true;
				}
			}
			accountInformation = null;
			error("No valid combination of account information found.");
			return false;
			static Uri CreateUri(string endpoint, string text)
			{
				UriBuilder uriBuilder = new UriBuilder(endpoint);
				if (!string.IsNullOrEmpty(uriBuilder.Query))
				{
					uriBuilder.Query = uriBuilder.Query + "&" + text;
				}
				else
				{
					uriBuilder.Query = text;
				}
				return uriBuilder.Uri;
			}
			(Uri Primary, Uri Secondary) createStorageUri(string text2, string text, string sasToken2, Func<IDictionary<string, string>, (Uri Primary, Uri Secondary)> factory)
			{
				if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(text2))
				{
					if (string.IsNullOrWhiteSpace(text2))
					{
						if (!matchesAutomaticEndpointsSpec || factory == null)
						{
							return (Primary: null, Secondary: null);
						}
						return factory(settings);
					}
					return (Primary: CreateUri(text2, sasToken2), Secondary: null);
				}
				return (Primary: CreateUri(text2, sasToken2), Secondary: CreateUri(text, sasToken2));
			}
			static bool s_isValidEndpointPair(string value2, string value3)
			{
				if (string.IsNullOrWhiteSpace(value2))
				{
					return string.IsNullOrWhiteSpace(value3);
				}
				return true;
			}
			string settingOrDefault(string key)
			{
				settings.TryGetValue(key, out var value2);
				return value2;
			}
		}

		private static IDictionary<string, string> ParseStringIntoSettings(string connectionString, Action<string> error)
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			string[] array = connectionString.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[1] { '=' }, 2);
				if (array2.Length != 2)
				{
					error("Settings must be of the form \"name=value\".");
					return null;
				}
				if (dictionary.ContainsKey(array2[0]))
				{
					error(string.Format(CultureInfo.InvariantCulture, "Duplicate setting '{0}' found.", array2[0]));
					return null;
				}
				dictionary.Add(array2[0], array2[1]);
			}
			return dictionary;
		}

		private static KeyValuePair<string, Func<string, bool>> Setting(string name, params string[] validValues)
		{
			return new KeyValuePair<string, Func<string, bool>>(name, (string settingValue) => validValues.Length == 0 || validValues.Contains(settingValue, StringComparer.OrdinalIgnoreCase));
		}

		private static KeyValuePair<string, Func<string, bool>> Setting(string name, Func<string, bool> isValid)
		{
			return new KeyValuePair<string, Func<string, bool>>(name, isValid);
		}

		private static bool IsValidBase64String(string settingValue)
		{
			try
			{
				Convert.FromBase64String(settingValue);
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}

		private static bool IsValidUri(string settingValue)
		{
			return Uri.IsWellFormedUriString(settingValue, UriKind.Absolute);
		}

		private static bool IsValidDomain(string settingValue)
		{
			return Uri.CheckHostName(settingValue).Equals(UriHostNameType.Dns);
		}

		private static Func<IDictionary<string, string>, IDictionary<string, string>> AllRequired(params KeyValuePair<string, Func<string, bool>>[] requiredSettings)
		{
			return delegate(IDictionary<string, string> settings)
			{
				IDictionary<string, string> dictionary = new Dictionary<string, string>(settings, StringComparer.OrdinalIgnoreCase);
				KeyValuePair<string, Func<string, bool>>[] array = requiredSettings;
				for (int i = 0; i < array.Length; i++)
				{
					KeyValuePair<string, Func<string, bool>> keyValuePair = array[i];
					if (!dictionary.TryGetValue(keyValuePair.Key, out var value) || !keyValuePair.Value(value))
					{
						return (IDictionary<string, string>)null;
					}
					dictionary.Remove(keyValuePair.Key);
				}
				return dictionary;
			};
		}

		private static Func<IDictionary<string, string>, IDictionary<string, string>> Optional(params KeyValuePair<string, Func<string, bool>>[] optionalSettings)
		{
			return delegate(IDictionary<string, string> settings)
			{
				IDictionary<string, string> dictionary = new Dictionary<string, string>(settings, StringComparer.OrdinalIgnoreCase);
				KeyValuePair<string, Func<string, bool>>[] array = optionalSettings;
				for (int i = 0; i < array.Length; i++)
				{
					KeyValuePair<string, Func<string, bool>> keyValuePair = array[i];
					if (dictionary.TryGetValue(keyValuePair.Key, out var value) && keyValuePair.Value(value))
					{
						dictionary.Remove(keyValuePair.Key);
					}
				}
				return dictionary;
			};
		}

		private static Func<IDictionary<string, string>, IDictionary<string, string>> AtLeastOne(params KeyValuePair<string, Func<string, bool>>[] atLeastOneSettings)
		{
			return delegate(IDictionary<string, string> settings)
			{
				IDictionary<string, string> dictionary = new Dictionary<string, string>(settings, StringComparer.OrdinalIgnoreCase);
				bool flag = false;
				KeyValuePair<string, Func<string, bool>>[] array = atLeastOneSettings;
				for (int i = 0; i < array.Length; i++)
				{
					KeyValuePair<string, Func<string, bool>> keyValuePair = array[i];
					if (dictionary.TryGetValue(keyValuePair.Key, out var value) && keyValuePair.Value(value))
					{
						dictionary.Remove(keyValuePair.Key);
						flag = true;
					}
				}
				return (!flag) ? null : dictionary;
			};
		}

		private static Func<IDictionary<string, string>, IDictionary<string, string>> None(params KeyValuePair<string, Func<string, bool>>[] atLeastOneSettings)
		{
			return delegate(IDictionary<string, string> settings)
			{
				IDictionary<string, string> dictionary = new Dictionary<string, string>(settings, StringComparer.OrdinalIgnoreCase);
				bool flag = false;
				KeyValuePair<string, Func<string, bool>>[] array = atLeastOneSettings;
				for (int i = 0; i < array.Length; i++)
				{
					KeyValuePair<string, Func<string, bool>> keyValuePair = array[i];
					if (dictionary.TryGetValue(keyValuePair.Key, out var value) && keyValuePair.Value(value))
					{
						flag = true;
					}
				}
				return (!flag) ? dictionary : null;
			};
		}

		private static Func<IDictionary<string, string>, IDictionary<string, string>> MatchesAll(params Func<IDictionary<string, string>, IDictionary<string, string>>[] filters)
		{
			return delegate(IDictionary<string, string> settings)
			{
				IDictionary<string, string> dictionary = new Dictionary<string, string>(settings, StringComparer.OrdinalIgnoreCase);
				Func<IDictionary<string, string>, IDictionary<string, string>>[] array = filters;
				foreach (Func<IDictionary<string, string>, IDictionary<string, string>> func in array)
				{
					if (dictionary == null)
					{
						break;
					}
					dictionary = func(dictionary);
				}
				return dictionary;
			};
		}

		private static Func<IDictionary<string, string>, IDictionary<string, string>> MatchesOne(params Func<IDictionary<string, string>, IDictionary<string, string>>[] filters)
		{
			return delegate(IDictionary<string, string> settings)
			{
				IDictionary<string, string>[] array = (from filter in filters
					select filter(new Dictionary<string, string>(settings)) into result
					where result != null
					select result).Take(2).ToArray();
				return (array.Length == 1) ? array.First() : null;
			};
		}

		private static Func<IDictionary<string, string>, IDictionary<string, string>> MatchesExactly(Func<IDictionary<string, string>, IDictionary<string, string>> filter)
		{
			return delegate(IDictionary<string, string> settings)
			{
				IDictionary<string, string> dictionary = filter(settings);
				return (dictionary != null && !dictionary.Any()) ? dictionary : null;
			};
		}

		private static bool MatchesSpecification(IDictionary<string, string> settings, params Func<IDictionary<string, string>, IDictionary<string, string>>[] constraints)
		{
			for (int i = 0; i < constraints.Length; i++)
			{
				IDictionary<string, string> dictionary = constraints[i](settings);
				if (dictionary == null)
				{
					return false;
				}
				settings = dictionary;
			}
			return !settings.Any();
		}

		private static object GetCredentials(IDictionary<string, string> settings)
		{
			settings.TryGetValue("AccountName", out var value);
			settings.TryGetValue("AccountKey", out var value2);
			settings.TryGetValue("SharedAccessSignature", out var value3);
			if (value == null || value2 == null || value3 != null)
			{
				if (value2 != null || value3 == null)
				{
					return null;
				}
				return new SharedAccessSignatureCredentials(value3);
			}
			return new StorageSharedKeyCredential(value, value2);
		}

		private static (Uri Primary, Uri Secondary) ConstructBlobEndpoint(IDictionary<string, string> settings)
		{
			return ConstructBlobEndpoint(settings["DefaultEndpointsProtocol"], settings["AccountName"], settings.ContainsKey("EndpointSuffix") ? settings["EndpointSuffix"] : null, settings.ContainsKey("SharedAccessSignature") ? settings["SharedAccessSignature"] : null);
		}

		internal static (Uri Primary, Uri Secondary) ConstructBlobEndpoint(string scheme, string accountName, string endpointSuffix, string sasToken)
		{
			if (string.IsNullOrEmpty(scheme))
			{
				throw Errors.ArgumentNull("scheme");
			}
			if (string.IsNullOrEmpty(accountName))
			{
				throw Errors.ArgumentNull("accountName");
			}
			if (string.IsNullOrEmpty(endpointSuffix))
			{
				endpointSuffix = "core.windows.net";
			}
			return ConstructUris(scheme, accountName, "blob", endpointSuffix, sasToken);
		}

		private static (Uri Primary, Uri Secondary) ConstructFileEndpoint(IDictionary<string, string> settings)
		{
			return ConstructFileEndpoint(settings["DefaultEndpointsProtocol"], settings["AccountName"], settings.ContainsKey("EndpointSuffix") ? settings["EndpointSuffix"] : null, settings.ContainsKey("SharedAccessSignature") ? settings["SharedAccessSignature"] : null);
		}

		internal static (Uri Primary, Uri Secondary) ConstructFileEndpoint(string scheme, string accountName, string endpointSuffix, string sasToken)
		{
			if (string.IsNullOrEmpty(scheme))
			{
				throw Errors.ArgumentNull("scheme");
			}
			if (string.IsNullOrEmpty(accountName))
			{
				throw Errors.ArgumentNull("accountName");
			}
			if (string.IsNullOrEmpty(endpointSuffix))
			{
				endpointSuffix = "core.windows.net";
			}
			return ConstructUris(scheme, accountName, "file", endpointSuffix, sasToken);
		}

		private static (Uri Primary, Uri Secondary) ConstructQueueEndpoint(IDictionary<string, string> settings)
		{
			return ConstructQueueEndpoint(settings["DefaultEndpointsProtocol"], settings["AccountName"], settings.ContainsKey("EndpointSuffix") ? settings["EndpointSuffix"] : null, settings.ContainsKey("SharedAccessSignature") ? settings["SharedAccessSignature"] : null);
		}

		internal static (Uri Primary, Uri Secondary) ConstructQueueEndpoint(string scheme, string accountName, string endpointSuffix, string sasToken)
		{
			if (string.IsNullOrEmpty(scheme))
			{
				throw Errors.ArgumentNull("scheme");
			}
			if (string.IsNullOrEmpty(accountName))
			{
				throw Errors.ArgumentNull("accountName");
			}
			if (string.IsNullOrEmpty(endpointSuffix))
			{
				endpointSuffix = "core.windows.net";
			}
			return ConstructUris(scheme, accountName, "queue", endpointSuffix, sasToken);
		}

		private static (Uri Primary, Uri Secondary) ConstructTableEndpoint(IDictionary<string, string> settings)
		{
			return ConstructTableEndpoint(settings["DefaultEndpointsProtocol"], settings["AccountName"], settings.ContainsKey("EndpointSuffix") ? settings["EndpointSuffix"] : null, settings.ContainsKey("SharedAccessSignature") ? settings["SharedAccessSignature"] : null);
		}

		internal static (Uri Primary, Uri Secondary) ConstructTableEndpoint(string scheme, string accountName, string endpointSuffix, string sasToken)
		{
			if (string.IsNullOrEmpty(scheme))
			{
				throw Errors.ArgumentNull("scheme");
			}
			if (string.IsNullOrEmpty(accountName))
			{
				throw Errors.ArgumentNull("accountName");
			}
			if (string.IsNullOrEmpty(endpointSuffix))
			{
				endpointSuffix = "core.windows.net";
			}
			return ConstructUris(scheme, accountName, "table", endpointSuffix, sasToken);
		}

		private static (Uri Primary, Uri Secondary) ConstructUris(string scheme, string accountName, string hostNamePrefix, string endpointSuffix, string sasToken)
		{
			UriBuilder uriBuilder = new UriBuilder
			{
				Scheme = scheme,
				Host = string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}", accountName, hostNamePrefix, endpointSuffix),
				Query = sasToken
			};
			UriBuilder uriBuilder2 = new UriBuilder();
			uriBuilder2.Scheme = scheme;
			uriBuilder2.Host = string.Format(CultureInfo.InvariantCulture, "{0}{1}.{2}.{3}", accountName, "-secondary", hostNamePrefix, endpointSuffix);
			uriBuilder2.Query = sasToken;
			UriBuilder uriBuilder3 = uriBuilder2;
			return (Primary: uriBuilder.Uri, Secondary: uriBuilder3.Uri);
		}
	}
	internal class StorageResponseClassifier : ResponseClassifier
	{
		public Uri SecondaryStorageUri { get; set; }

		public override bool IsRetriableResponse(HttpMessage message)
		{
			if (SecondaryStorageUri != null && message.Request.Uri.Host == SecondaryStorageUri.Host && message.Response.Status == 404)
			{
				return true;
			}
			if (message.Response.Status >= 400 && message.Response.Headers.TryGetValue("x-ms-error-code", out string value))
			{
				switch (value)
				{
				case "InternalError":
				case "OperationTimedOut":
				case "ServerBusy":
					return true;
				}
			}
			return base.IsRetriableResponse(message);
		}

		public override bool IsErrorResponse(HttpMessage message)
		{
			if (message.Response.Status == 409)
			{
				RequestHeaders headers = message.Request.Headers;
				if (message.Response.Headers.TryGetValue("x-ms-error-code", out string value) && (value == "ContainerAlreadyExists" || value == "BlobAlreadyExists"))
				{
					return !headers.Contains(HttpHeader.Names.IfMatch) && !headers.Contains(HttpHeader.Names.IfNoneMatch) && !headers.Contains(HttpHeader.Names.IfModifiedSince) && !headers.Contains(HttpHeader.Names.IfUnmodifiedSince);
				}
			}
			return base.IsErrorResponse(message);
		}
	}
	internal class StorageSharedKeyCredentialInternals : StorageSharedKeyCredential
	{
		private StorageSharedKeyCredentialInternals(string accountName, string accountKey)
			: base(accountName, accountKey)
		{
		}

		internal new static string ComputeSasSignature(StorageSharedKeyCredential credential, string message)
		{
			return StorageSharedKeyCredential.ComputeSasSignature(credential, message);
		}
	}
	internal sealed class StorageSharedKeyPipelinePolicy : HttpPipelineSynchronousPolicy
	{
		private const bool IncludeXMsDate = true;

		private readonly StorageSharedKeyCredential _credentials;

		public StorageSharedKeyPipelinePolicy(StorageSharedKeyCredential credentials)
		{
			_credentials = credentials;
		}

		public override void OnSendingRequest(HttpMessage message)
		{
			base.OnSendingRequest(message);
			string value = DateTimeOffset.UtcNow.ToString("r", CultureInfo.InvariantCulture);
			message.Request.Headers.SetValue("x-ms-date", value);
			string message2 = BuildStringToSign(message);
			string text = StorageSharedKeyCredentialInternals.ComputeSasSignature(_credentials, message2);
			string value2 = new AuthenticationHeaderValue("SharedKey", _credentials.AccountName + ":" + text).ToString();
			message.Request.Headers.SetValue("Authorization", value2);
		}

		private string BuildStringToSign(HttpMessage message)
		{
			message.Request.Headers.TryGetValue("Content-Encoding", out string value);
			message.Request.Headers.TryGetValue("Content-Language", out string value2);
			message.Request.Headers.TryGetValue("Content-MD5", out string value3);
			message.Request.Headers.TryGetValue("Content-Type", out string value4);
			message.Request.Headers.TryGetValue("If-Modified-Since", out string value5);
			message.Request.Headers.TryGetValue("If-Match", out string value6);
			message.Request.Headers.TryGetValue("If-None-Match", out string value7);
			message.Request.Headers.TryGetValue("If-Unmodified-Since", out string value8);
			message.Request.Headers.TryGetValue("Range", out string value9);
			string text = string.Empty;
			if (message.Request.Content != null && message.Request.Content.TryComputeLength(out var length))
			{
				text = length.ToString(CultureInfo.InvariantCulture);
			}
			Uri uri = message.Request.Uri.ToUri();
			StringBuilder stringBuilder = new StringBuilder(uri.AbsolutePath.Length + 64);
			stringBuilder.Append(message.Request.Method.ToString().ToUpperInvariant()).Append('\n');
			stringBuilder.Append(value ?? "").Append('\n');
			stringBuilder.Append(value2 ?? "").Append('\n');
			stringBuilder.Append((text == "0") ? "" : (text ?? "")).Append('\n');
			stringBuilder.Append(value3 ?? "");
			stringBuilder.Append('\n');
			stringBuilder.Append(value4 ?? "").Append('\n');
			stringBuilder.Append('\n');
			stringBuilder.Append(value5 ?? "").Append('\n');
			stringBuilder.Append(value6 ?? "").Append('\n');
			stringBuilder.Append(value7 ?? "").Append('\n');
			stringBuilder.Append(value8 ?? "").Append('\n');
			stringBuilder.Append(value9 ?? "").Append('\n');
			BuildCanonicalizedHeaders(stringBuilder, message);
			BuildCanonicalizedResource(stringBuilder, uri);
			return stringBuilder.ToString();
		}

		private static void BuildCanonicalizedHeaders(StringBuilder stringBuilder, HttpMessage message)
		{
			List<HttpHeader> list = new List<HttpHeader>();
			foreach (HttpHeader header in message.Request.Headers)
			{
				if (header.Name.StartsWith("x-ms-", StringComparison.OrdinalIgnoreCase))
				{
					list.Add(new HttpHeader(header.Name.ToLowerInvariant(), header.Value));
				}
			}
			list.Sort((HttpHeader x, HttpHeader y) => string.CompareOrdinal(x.Name, y.Name));
			foreach (HttpHeader item in list)
			{
				stringBuilder.Append(item.Name).Append(':').Append(item.Value)
					.Append('\n');
			}
		}

		private void BuildCanonicalizedResource(StringBuilder stringBuilder, Uri resource)
		{
			stringBuilder.Append('/');
			stringBuilder.Append(_credentials.AccountName);
			if (resource.AbsolutePath.Length > 0)
			{
				stringBuilder.Append(resource.AbsolutePath);
			}
			else
			{
				stringBuilder.Append('/');
			}
			IDictionary<string, string> queryParameters = resource.GetQueryParameters();
			if (queryParameters.Count <= 0)
			{
				return;
			}
			foreach (string item in queryParameters.Keys.OrderBy((string key) => key, StringComparer.Ordinal))
			{
				stringBuilder.Append('\n').Append(item.ToLowerInvariant()).Append(':')
					.Append(queryParameters[item]);
			}
		}
	}
	internal static class StorageCrc64Calculator
	{
		private static ulong poly = 11127430586519243189uL;

		private static ulong[] m_u1 = new ulong[256]
		{
			0uL, 9182541432847960441uL, 18365082865695920882uL, 9345832722727082891uL, 14511413233979602575uL, 13117883710352670710uL, 4016934769805403261uL, 5247243509741595908uL, 11969702169228410485uL, 15668617373955487500uL,
			6395407394255400071uL, 2859783479402063358uL, 8033869539610806522uL, 1157698950281609603uL, 10494487019483191816uL, 17207436825116529521uL, 8710242310496874369uL, 544390144406054648uL, 9672921298356239731uL, 17965965451118834698uL,
			12790814788510800142uL, 14910550302343712887uL, 5719566958804126716uL, 3472568952111055493uL, 16067739079221613044uL, 11642617884288424077uL, 2315397900563219206uL, 6867711082173303423uL, 1702069273413494651uL, 7561550595985681922uL,
			16808303987311785353uL, 10821560171884821744uL, 17420484620993748738uL, 10281452688370128507uL, 1088780288812109296uL, 8102801665828209801uL, 4081135393624123789uL, 5174050811428790516uL, 14303144984052136831uL, 13335169890544513542uL,
			6331237281917575543uL, 2932936320451717134uL, 12178000930637296517uL, 15451291336501017340uL, 11439133917608253432uL, 16271795144152063617uL, 6945137904222110986uL, 2237417001980464243uL, 9881180485936394371uL, 17748670208579942906uL,
			8646032624330580593uL, 617573780371024648uL, 4630795801126438412uL, 4552317850264964981uL, 13735422164346606846uL, 13974939467834563975uL, 3404138546826989302uL, 5788002041349785487uL, 15123101191971363844uL, 12578268576127440253uL,
			16599996146375981177uL, 11038877129812111616uL, 1766230306223614603uL, 7488388675408585714uL, 15517955973396180335uL, 12120359180407907350uL, 2992425542307102621uL, 6262760941951170276uL, 2177560577624218592uL, 7014021097877803673uL,
			16205603331656419602uL, 11496333698567045227uL, 8162270787248247578uL, 1020283848406030947uL, 10348101622857581032uL, 17362827168352323729uL, 13268962925398943125uL, 14360329612356938988uL, 5114174836390786919uL, 4149999036593995294uL,
			12662474563835151086uL, 15047886789205674903uL, 5865872640903434268uL, 3317240731349735781uL, 7410885347125621857uL, 1852721336781405464uL, 10954198386508228243uL, 16675652587215613930uL, 695464411657452699uL, 8559154840590169570uL,
			17832891829959241321uL, 9805981716846356240uL, 13890275808444221972uL, 13811093689104106349uL, 4474834003960928486uL, 4717306313667482015uL, 2781857646629810797uL, 6482320345254034196uL, 15584360551092445343uL, 12044936105578752486uL,
			17292065248661161186uL, 10418850627624291739uL, 1235147560742049296uL, 7947394159970475881uL, 9261591602252876824uL, 18440332504439424353uL, 9104635700529929962uL, 86933051457181587uL, 5324711670898473623uL, 3930478940865573870uL,
			13202527286532548709uL, 14435791994760012060uL, 6808277093653978604uL, 2383859105125700757uL, 11576004082699570974uL, 16125361296017327719uL, 10887802304290696035uL, 16751084157523239450uL, 7621461738281908625uL, 1633170428957798632uL,
			17899336015843713945uL, 9730527881461623520uL, 484936124168630635uL, 8778683483337193490uL, 3532460612447229206uL, 5650648632384052335uL, 14976777350817171428uL, 12733579874785513117uL, 11127430586519243189uL, 16501861805283229900uL,
			7255706616989801287uL, 2008467384902701630uL, 5984851084614205242uL, 3197703697127700035uL, 12525521883902340552uL, 15185406778534061233uL, 4355121155248437184uL, 4836460649178119865uL, 14028042195755607346uL, 13673894688497708107uL,
			17659347437411533135uL, 9978967587602237494uL, 851034636706747325uL, 8404151993655892676uL, 16324541574496495156uL, 11376828069156237133uL, 2040567696812061894uL, 7151572492026068415uL, 3165618640958787771uL, 6089000465318648258uL,
			15362738141892903497uL, 12276135533837191984uL, 4940590242968197185uL, 4323016312165290296uL, 13424492949564705459uL, 14205358170246726602uL, 10228349672781573838uL, 17482011809167009719uL, 8299998073187990588uL, 883115153111807301uL,
			7758977986698090167uL, 1496212771153551310uL, 10768261529147802693uL, 16870057623371222332uL, 11731745281806868536uL, 15970178678932056385uL, 6634481462699471562uL, 2557087418195393459uL, 14821770694251243714uL, 12889145053243924923uL,
			3705442673562810928uL, 5477099193254114121uL, 347732205828726349uL, 8916445914979620660uL, 18018485433612717247uL, 9610811077088298438uL, 1390928823314905398uL, 7792180275546222671uL, 17118309681180339140uL, 10592047673346876093uL,
			15721915763462295481uL, 11907948279947413184uL, 2662355835449236811uL, 6601263643266274354uL, 13065363431662389059uL, 14573523159059271226uL, 5443901152145348017uL, 3810730869140954312uL, 8949668007921856972uL, 242468062084315317uL,
			9434612627334964030uL, 18266752897469557319uL, 5563715293259621594uL, 3627853308494900643uL, 12964640690508068392uL, 14737282991092629329uL, 9535436420502803029uL, 18102883011221642028uL, 8829673718705766567uL, 425517318353373662uL,
			16945538107983338159uL, 10683758673333097430uL, 1582809320479393885uL, 7681369070946451748uL, 2470295121484098592uL, 6712246474782352729uL, 15894788319940951762uL, 11816127157005858731uL, 3879488993349045083uL, 5384130764573901346uL,
			14630883743593699753uL, 12998980579021361360uL, 18209271401059859924uL, 9501085674329060525uL, 173866102914363174uL, 9009242716806358623uL, 10649423341796947246uL, 17051941912459279447uL, 7860957881731147740uL, 1331177917724618405uL,
			6532681715831194529uL, 2721950576072674008uL, 11850482417207202131uL, 15788404444129941546uL, 13616554187307957208uL, 14094374762992939681uL, 4767718210251401514uL, 4414836859352883283uL, 8472769053991669079uL, 791404660239696942uL,
			10036468583241064357uL, 17592823520898193116uL, 1939744496674522541uL, 7315441871796849876uL, 16444536456726632287uL, 11193778306393792038uL, 15242923476563817250uL, 12459013669784255067uL, 3266340857915597264uL, 5925241208603601065uL,
			14129842449788916825uL, 13509030938278026528uL, 4236384526995834539uL, 5018234291620532178uL, 969872248337261270uL, 8222268228363316143uL, 17557366966674386980uL, 10144002964842903901uL, 7064921224894458412uL, 2118192263497917269uL,
			11301297264768104670uL, 16409064479275168167uL, 12351475057656208547uL, 15278375800261809626uL, 6175737528828104273uL, 3087868764414052136uL
		};

		private static ulong[] m_u32 = new ulong[2048]
		{
			0uL, 13314104777806623281uL, 4995408779574703881uL, 18272917952776127800uL, 9990817559149407762uL, 3630939668061314083uL, 14985097136918436123uL, 8588630238580288298uL, 2419595280346009423uL, 11047632078064778622uL,
			7261879336122628166uL, 15853324927293196919uL, 12336101225143050589uL, 1438778539722085228uL, 17177260477160576596uL, 6243344386453529701uL, 4839190560692018846uL, 18153290931779284143uL, 466971369176983959uL, 13744476153000549286uL,
			14523758672245256332uL, 8163885170551931581uL, 10150410278941460357uL, 3753947787305760180uL, 7114670618595086801uL, 15742703009609180128uL, 2877557079444170456uL, 11468998281230587113uL, 16724927115800166339uL, 5827608819780259314uL,
			12486688772907059402uL, 1552777088887782139uL, 9678381121384037692uL, 4509712478557409037uL, 14052292465040017973uL, 8919086911451396100uL, 933942738353967918uL, 12984779574341165343uL, 5306724880489522215uL, 17393031402499380758uL,
			12088969047850185331uL, 2234232424948285506uL, 16327770341103863162uL, 6508501205480191819uL, 3270219084831693921uL, 10783611126490213968uL, 7507895574611520360uL, 15056752904239896921uL, 14229341237190173602uL, 9060668187673563539uL,
			9235624609781601451uL, 4102414666794133146uL, 5755114158888340912uL, 17805955384257740673uL, 760268329942694585uL, 12846579258659178632uL, 16495813941225975021uL, 6641072911623610076uL, 11655217639560518628uL, 1835944114540095957uL,
			7947275282931516159uL, 15460671713970139342uL, 3105554177775564278uL, 10654415914121054151uL, 4069053834368248083uL, 9276602857674201890uL, 9019424957114818074uL, 14262439286667324459uL, 12888929698626668289uL, 728350057390060848uL,
			17838173822902792200uL, 5713061687187489337uL, 1867885476707935836uL, 11612879294349907053uL, 6683146274174357845uL, 16463609796103103332uL, 10613449760979044430uL, 3138938099822951039uL, 15427587958021472071uL, 7988539404333878646uL,
			8885721681034573709uL, 14093275110923552188uL, 4468464849896571012uL, 9711483568963269301uL, 17435386240585734559uL, 5274802209818028974uL, 13017002410960383638uL, 891885868678962343uL, 6540438169663387842uL, 16285436393877900019uL,
			2276301389390663627uL, 12056769300835696122uL, 15015791149223040720uL, 7541275098533749985uL, 10750531768509407705uL, 3311478808266183656uL, 13702389596435043375uL, 499223892686693918uL, 18121336375347127078uL, 4881577284539027735uL,
			3787009552894999101uL, 10109203332270622732uL, 8204829333588266292uL, 14490431924675641093uL, 11510228317776681824uL, 2844507408351766865uL, 15776050647768657001uL, 7073740749340991064uL, 1520536659885389170uL, 12528798419337332547uL,
			5785236389704991355uL, 16756902562832658506uL, 18230826998244854449uL, 5027665701050185856uL, 13282145823247220152uL, 42391121974267785uL, 8621696402279998627uL, 14943885792137123474uL, 3671888229080191914uL, 9957486413597237659uL,
			15894550565863032318uL, 7228834063006480335uL, 11080975318107484919uL, 2378669809208672454uL, 6211108355551128556uL, 17219365725490862557uL, 1396410507639856357uL, 12368072274182516436uL, 8138107668736496166uL, 14426557995101282327uL,
			3864963828248814895uL, 10188883840789878558uL, 18038849914229636148uL, 4797439356862238213uL, 13764583471170505533uL, 558636003161176332uL, 5864949882499341673uL, 16834823851763904344uL, 1456700114780121696uL, 12462039372163962961uL,
			15835500142712222587uL, 7135897241754845514uL, 11426123374374978674uL, 2761987960811697731uL, 3735770953415871672uL, 10024216873222930057uL, 8542024690686344113uL, 14865940313687918976uL, 13366292548348715690uL, 124886380503896219uL,
			18171423683030832547uL, 4965480621596141458uL, 1318498013465740279uL, 12288367576166281670uL, 6277876199645902078uL, 17283211067504936655uL, 11018827623122529765uL, 2319229111673053140uL, 15977078808667757292uL, 7312947801685403869uL,
			17771443362069147418uL, 5649178961375419691uL, 12966875177746962451uL, 808021769923242530uL, 8936929699793142024uL, 14178292563042218809uL, 4131238913151202817uL, 9336006171948696624uL, 15507292654829740117uL, 8066451897031594596uL,
			10549604419636057948uL, 3072170256667698541uL, 6742586972917943879uL, 16525757492564458614uL, 1783771737357924686uL, 11530351050605661055uL, 13080876339326775684uL, 958607532054332341uL, 17355705732737566349uL, 5196847935403734204uL,
			4552602778781327254uL, 9793970031557160359uL, 8826309569889003679uL, 14031081235248569006uL, 10672610478370209483uL, 3231765313995443450uL, 15082550197067499970uL, 7605111644578544627uL, 2214144898184761561uL, 11997319807368520424uL,
			6622957616532367312uL, 16369541336340076001uL, 5226553991581547317uL, 17315567511325935876uL, 998447785373387836uL, 13050870116949589517uL, 13992385572602893607uL, 8857387814886926102uL, 9763154569078055470uL, 4591563424455954463uL,
			7574019105789998202uL, 15121224969131705931uL, 3192792573832364915uL, 10703402850965664066uL, 16409658667176532584uL, 6593237266557642841uL, 12027302939855777121uL, 2174292550383558480uL, 837723427998967211uL, 12926741354437416858uL,
			5689014816703533730uL, 17741441537682890899uL, 9297314907277183929uL, 4162312760174967176uL, 14147481498681982128uL, 8975885947348913793uL, 3041073319770778340uL, 10588283589808642261uL, 8027474758883876333uL, 15538089425409847260uL,
			11570472779409982710uL, 1754046989415331527uL, 16555745023176864767uL, 6702730226991579598uL, 14904935044227204617uL, 8511175143342584888uL, 10055331402100371712uL, 3697039006890672945uL, 4935510683106863131uL, 18211227652461976106uL,
			84782243948535570uL, 13395964519670263075uL, 17243392804559997254uL, 6307825247276890999uL, 12258683510079471183uL, 1358579060414286974uL, 7343776458160383828uL, 15938069784625157477uL, 2357937968601911389uL, 10987700999469326956uL,
			10227874173201914007uL, 3834118679032310438uL, 14457668126012960670uL, 8099380120177073583uL, 528670462654445189uL, 13804383042619098292uL, 4757339618417344908uL, 18068517487440704445uL, 12422216711102257112uL, 1486653560527873513uL,
			16805135387700830417uL, 5905035327424140000uL, 2792821015279712714uL, 11387109952339348475uL, 7174610496783699651uL, 15804369120959036658uL, 0uL, 17795750414920992584uL, 15652066155665235451uL, 3440885951733652147uL,
			9707430005003888797uL, 8088619545869807573uL, 6881771903467304294uL, 12211444018068779566uL, 4158605880245666385uL, 14934326367322670361uL, 16177239091739615146uL, 1618531183280494818uL, 13763543806934608588uL, 5329687576586300804uL,
			7365251356733110071uL, 10430782732424065151uL, 8317211760491332770uL, 9627456514162279402uL, 12275655047854856537uL, 6677949350095543825uL, 17715775754843728959uL, 228593521434585975uL, 3237062366560989636uL, 15716278079943308940uL,
			5393898400245434099uL, 13559721322313744827uL, 10659375153172601608uL, 7285277797140536384uL, 14730502713466220142uL, 4222818010717792550uL, 1538556591887086485uL, 16405832406980080861uL, 16634423520982665540uL, 1165988892599602700uL,
			4606644975495812287uL, 14490652333704500215uL, 6948737735228826073uL, 10851938492356421265uL, 13355898700191087650uL, 5741697744827710314uL, 16100102641861249813uL, 2997210129391651933uL, 457187042869171950uL, 17343209908929073574uL,
			6474124733121979272uL, 12623451852133770432uL, 9290918443066424947uL, 8509777644550175035uL, 10787796800490868198uL, 7152491088056076974uL, 5513174730750660637uL, 13435802853112141653uL, 1369740940761310587uL, 16570282996205314611uL,
			14570555594281072768uL, 4378122991072214984uL, 12394928769305825207uL, 6554029092169976063uL, 8445636021435585100uL, 9494671589766665476uL, 3077113183774172970uL, 15871580726121439330uL, 17546962163284900561uL, 393046449407966617uL,
			17972623996477342179uL, 1124953203026282155uL, 2331977785199205400uL, 15468718787813366608uL, 9213289950991624574uL, 9884023296933522998uL, 12027814264650544261uL, 5772583309854864333uL, 13897475470457652146uL, 3903201379966321914uL,
			1867462721629478473uL, 17230134200194177281uL, 5074000312779205423uL, 12726412586066422887uL, 11483395489655420628uL, 7613902433661202844uL, 9945982394365904193uL, 9002712138369221129uL, 5994420258783303866uL, 11945588830717829106uL,
			914374085738343900uL, 18034584260997933716uL, 15386492461536161831uL, 2553815763781089135uL, 12948249466243958544uL, 4991775084973424728uL, 7675861599844555499uL, 11272817470906015139uL, 3820974847495073677uL, 14119313517723330757uL,
			17019555289100350070uL, 1929422917466207550uL, 2284114744097015975uL, 16809117116094664687uL, 14304982176112153948uL, 3491052535749661204uL, 11026349461501321274uL, 8066583400317413234uL, 4625822816565738945uL, 13169947943851817609uL,
			2739481881522621174uL, 15056568154612698558uL, 18389278632294045453uL, 703937903355564101uL, 11579634704575357547uL, 6216116333187147043uL, 8756245982144429968uL, 10336706602061052120uL, 7844815651933457413uL, 11108505557513489229uL,
			13108058184339952126uL, 4836331428643624630uL, 16891272042871170200uL, 2062348302526112720uL, 3701560116026653027uL, 14243093311092293163uL, 6154226367548345940uL, 11790143385404147996uL, 10114939059804099503uL, 8838402009405625575uL,
			15267075666205894345uL, 2677593222696804737uL, 786092898815933234uL, 18167511984529030266uL, 14271356128258724013uL, 3528920495235061733uL, 2249906406052564310uL, 16847527791942683166uL, 4663955570398410800uL, 13136584867098492792uL,
			11065060459665827275uL, 8032672874147504771uL, 18426579901983249148uL, 670869757795243444uL, 2778461448337575687uL, 15021799713104398415uL, 8723442665257564769uL, 10374270808524176681uL, 11545166619709728666uL, 6255393677550461138uL,
			13070192475467748367uL, 4869959624064586567uL, 7806402759932643828uL, 11142711713798442684uL, 3734925443258956946uL, 14204962704692935642uL, 16925180352754065769uL, 2023635122467890721uL, 10148000625558410846uL, 8801094125517115670uL,
			6189001354670689189uL, 11751170467248901357uL, 748522112412581571uL, 18200308687082371467uL, 15227804867322405688uL, 2712067956087923824uL, 2366451139905821161uL, 15429444960977936033uL, 18005424276738442258uL, 1087383245551616858uL,
			11988840517566607732uL, 5807354719269451324uL, 9175985095055060111uL, 9917086241372335047uL, 1828748171476687800uL, 17264039490715065584uL, 13859348433176118851uL, 3936567527537377547uL, 11517600825601618725uL, 7575485972542567533uL,
			5107631527562178270uL, 12688548247288785302uL, 6033696233052052811uL, 11911117726490188291uL, 9983550169946849456uL, 8969909641821112312uL, 15351723199689110998uL, 2592791761478218398uL, 881308959540029485uL, 18071886900781707109uL,
			7641949694990147354uL, 11311525441118574674uL, 12914889967198388961uL, 5029908667734787497uL, 17057965136019683207uL, 1895211001713995983uL, 3858845834932415100uL, 14085688848554405172uL, 4568229488194031950uL, 14524856687109953030uL,
			16596560370769988789uL, 1199618927273069565uL, 13389804964662786515uL, 5702982212268129947uL, 6982105071499322408uL, 10813810275099861856uL, 491959469050286879uL, 17304235153534587991uL, 16133166800634826468uL, 2959904050395908524uL,
			9251645633131477890uL, 8544249991080197322uL, 6436555990116558457uL, 12656250909470171441uL, 5478963763045242348uL, 13474211760440136356uL, 10754173414376441879uL, 7190360783715028831uL, 14609264512674905457uL, 4344210146760948281uL,
			1407875806711128202uL, 16536922205515311042uL, 8484612924229834685uL, 9459901414098079989uL, 12432232666374294086uL, 6520962717142945038uL, 17512491964288859936uL, 432321509989660776uL, 3044311944644715227uL, 15909147253007623571uL,
			15689631303866914826uL, 3408081684652252994uL, 39278638289454577uL, 17761281044853637817uL, 6848704699511582871uL, 12248744337429493727uL, 9672662857287249260uL, 8127597827348556324uL, 16143876930955041371uL, 1656663029868305683uL,
			4124696605052225440uL, 14973036106055254248uL, 7403120232053306054uL, 10397155777191818638uL, 13801955733624614717uL, 5295477978975447157uL, 12308452735096691880uL, 6640377587728112608uL, 8351687676139176275uL, 9588184524566087195uL,
			3199755467226494005uL, 15749338669599060861uL, 17676804018811251150uL, 263367317710270086uL, 10621245557231542009uL, 7318642105459087793uL, 5355186445393609474uL, 13593628415714133066uL, 1572185797631866468uL, 16367965679059644716uL,
			14764710094823615391uL, 4184403902100516055uL, 0uL, 8192260192693531485uL, 16384520385387062970uL, 10579310971610093031uL, 17445294202075176479uL, 9487645808758975810uL, 1259777398387980453uL, 6974776076101291000uL,
			15054798087340502869uL, 11627628094419573768uL, 3714591610324732399uL, 4772725293978712754uL, 2519554796775960906uL, 6000906318572175895uL, 13949552152202582000uL, 12692835880654611629uL, 10737672651763207617uL, 16479552155529248412uL,
			8530488506137553787uL, 563692279294040102uL, 7429183220649464798uL, 1632840715178909827uL, 9545450587957425508uL, 17710547929865700921uL, 5039109593551921812uL, 3773526413697536457uL, 12001812637144351790uL, 15510327009725822835uL,
			13255397586018483339uL, 14286650443619733462uL, 6094816860633029169uL, 2676794701239413100uL, 2224468801865124073uL, 8029398802249956276uL, 18284443984023023187uL, 10092467602768682254uL, 17060977012275107574uL, 11345698921169118635uL,
			1127384558588080204uL, 9085316763342930705uL, 14858366441298929596uL, 13799949221084206305uL, 3265681430357819654uL, 6693130561793520219uL, 4335179923219708323uL, 5591612658206395134uL, 16089150697174343449uL, 12608078313892359236uL,
			10078219187103843624uL, 18045298950508639861uL, 7547052827395072914uL, 1804894185114800335uL, 8775022333477165879uL, 610208528129632362uL, 11143777030265568653uL, 16939840397221351120uL, 6570863924763224701uL, 3062628965574433056uL,
			13281651414986427591uL, 14546950783402164122uL, 12189633721266058338uL, 15607935294051813183uL, 5353589402478826200uL, 4322052733438386565uL, 4448937603730248146uL, 5480049586572239503uL, 16058797604499912552uL, 12640634845224911925uL,
			14963191654740611021uL, 13697468149613821072uL, 3226387075058313591uL, 6734760297733626410uL, 17100359328236387975uL, 11304157148038237658uL, 1022647305271334973uL, 9187885797085768544uL, 2254769117176160408uL, 7996789495701459909uL,
			18170633526685861410uL, 10203977899723732351uL, 12159205861289221139uL, 15640698533058703182uL, 5467281111780014761uL, 4210705165571557876uL, 6531362860715639308uL, 3104333467743203665uL, 13386261123587040438uL, 14444535682067388395uL,
			8670359846439416646uL, 712570853174722587uL, 11183225316412790268uL, 16898083119299733153uL, 9964615437390687577uL, 18156734480110951940uL, 7577568646423104483uL, 1772218907306621118uL, 2549929897105824059uL, 5968371794895579750uL,
			13835807649431584641uL, 12804412130028834012uL, 15094105654790145828uL, 11586011570629080185uL, 3609788370229600670uL, 4875228338795123395uL, 17550044666954331758uL, 9385089986579072307uL, 1220417056259264724uL, 7016339823065260937uL,
			113832465580320881uL, 8080771903981116204uL, 16354233247329554123uL, 10611933455411592598uL, 13141727849526449402uL, 14398019984296241063uL, 6125257931148866112uL, 2644044672772047133uL, 4934513061080763109uL, 3875954691161242040uL,
			12041335708311022687uL, 15468644514675613442uL, 7389748111218375599uL, 1674611169817184498uL, 9650135081627196693uL, 17608207611453156936uL, 10707178804957652400uL, 16512249405559379693uL, 8644105466876773130uL, 452269960717267031uL,
			8897875207460496292uL, 777815854587973881uL, 10960099173144479006uL, 16837508876986644035uL, 9899396859023031739uL, 17929245473684410086uL, 7638169311773399809uL, 1995371405161080924uL, 12364343895119170801uL, 15719313666694922156uL,
			5257806571491912267uL, 4127472116736940310uL, 6452774150116627182uL, 2899221788499403187uL, 13469520595467252820uL, 14654036576950028041uL, 17183618765321268837uL, 11513658076643712312uL, 944058559875877087uL, 8982774151563688834uL,
			2045294610542669946uL, 7913556412137505575uL, 18375771594171537088uL, 10282592998631728541uL, 4509538234352320816uL, 5703202118082425453uL, 15993578991402919818uL, 12413145872452981975uL, 14740065545194020655uL, 13636893872504286322uL,
			3453902469802228117uL, 6800005264351546056uL, 7324397590398380877uL, 1447271697995405328uL, 9711166754478640631uL, 17830946693959024298uL, 10934562223560029522uL, 16577643941610997263uL, 8421410331143115752uL, 391282303089057973uL,
			13062725721431278616uL, 14193339314701023045uL, 6208666935486407330uL, 2853413627349914111uL, 5139237677448190471uL, 3955000834479647066uL, 11832010700513754301uL, 15385279525569695712uL, 17340719692878833292uL, 9301724962676790737uL,
			1425141706349445174uL, 7095385931588285291uL, 197241435189590163uL, 8290140892214627278uL, 16275231084505128489uL, 10407252819471035764uL, 2327234795026827737uL, 5907384102538115716uL, 14063191101689605987uL, 12869806631352179774uL,
			15155137292846208966uL, 11808750686857700507uL, 3544437814613242236uL, 4647888900695114273uL, 5099859794211648118uL, 3996546971616280875uL, 11936743589791159500uL, 15282706059068598161uL, 13032420973529754729uL, 14225944257377951540uL,
			6322481756728658643uL, 2741907763018938766uL, 10820800179681172771uL, 16689202581157916286uL, 8451767855871261593uL, 358730135091235012uL, 7219576740459201340uL, 1549757201687277665uL, 9750456677590246790uL, 17789312594549937883uL,
			15259804211971227575uL, 11706392725180894442uL, 3504985165131258125uL, 4689641746563050064uL, 2440834112518529448uL, 5795944209433268981uL, 14032679646130521874uL, 12902486341348291663uL, 227664931160641762uL, 8257373220483031999uL,
			16161543807962232408uL, 10518604751377171717uL, 17380225120797953789uL, 9260024893098540448uL, 1320527565124790343uL, 7197796669017938714uL, 6348019252612840095uL, 3001773246774643138uL, 13508885301467044901uL, 14612477262577168248uL,
			12250515862297732224uL, 15830806319446185949uL, 5288089345544094266uL, 4094845200210145639uL, 9869026122161526218uL, 17961784429549397655uL, 7751909382322484080uL, 1883790792284913709uL, 8858563276676680661uL, 819427946324326536uL,
			11064906845327338863uL, 16735010195537927730uL, 14779496222436751198uL, 13595119054397621251uL, 3349222339634368996uL, 6902350014986035897uL, 4540036513212049729uL, 5670509231386434076uL, 15879957667295973371uL, 12524563758942059686uL,
			2158968710939334667uL, 7802191304085662550uL, 18345326091064622769uL, 10315338663227985388uL, 17288210933753546260uL, 11411225366421157193uL, 904539921434534062uL, 9024461010619192307uL, 0uL, 13824762537890386270uL,
			5435843389918327255uL, 17632879567996264585uL, 10871686779836654510uL, 2955647571599356656uL, 15974271476589515385uL, 7091884143152218919uL, 1830393695483286583uL, 12014954090240984425uL, 5911295143198713312uL, 17136847308542735550uL,
			10356344933674923929uL, 3486433246119044807uL, 14183768286304437838uL, 8866939313174419216uL, 3660787390966573166uL, 10166549554642499888uL, 8764643140330461625uL, 14301506311825885415uL, 11822590286397426624uL, 2002179278080727710uL,
			17257153827578018327uL, 5811567326429637449uL, 3146722988342115417uL, 10696061943161711879uL, 6972866492238089614uL, 16077839028455922896uL, 13651706126026579959uL, 193643978992848553uL, 17733878626348838432uL, 5314257382929317758uL,
			7321574781933146332uL, 15726597597130965378uL, 3383482852370407691uL, 10461835487050756181uL, 17529286280660923250uL, 5521383145158058540uL, 13270830730307469989uL, 571986241638437883uL, 9006539716542072043uL, 14062142868687263157uL,
			4004358556161455420uL, 9820444972906259554uL, 16871141307392288581uL, 6195046438697498139uL, 11623134652859274898uL, 2204168053248247756uL, 6293445976684230834uL, 16757229616687114732uL, 2033710611994724709uL, 11809103765812205627uL,
			13945732984476179228uL, 9102441198461848130uL, 10008912073189894859uL, 3836399239677590421uL, 5634023594246450309uL, 17432166230595157467uL, 387287957985697106uL, 13440007996446051340uL, 15631975740571111211uL, 7436713287088496245uL,
			10628514765858635516uL, 3196286443948011426uL, 14643149563866292664uL, 8425538611776008422uL, 9248445511903875183uL, 4576352633966788913uL, 6766965704740815382uL, 16299225501472805704uL, 1623241691541500865uL, 12204057899130610335uL,
			15154673959339816335uL, 7893503808290579665uL, 11042766290316117080uL, 2802546455471379718uL, 4940389601680725537uL, 18110282944942901119uL, 1143972483276875766uL, 12698841025324496552uL, 18013079433084144086uL, 5053104780943399048uL,
			12868101959430486017uL, 959199400746887519uL, 8008717112322910840uL, 15059968434811763494uL, 2615275316874435503uL, 11209529030916197105uL, 16185247739280711137uL, 6865422453713194175uL, 12390092877394996278uL, 1452727108004248936uL,
			8521497235978731087uL, 14526673814343495441uL, 4408336106496495512uL, 9436978683674173126uL, 12586891953368461668uL, 1258461165104291898uL, 16555902143576621235uL, 6492234632876446189uL, 4067421223989449418uL, 9775360158263870356uL,
			8294958500777960221uL, 14755745691174154819uL, 13243752613299266899uL, 581015605248734221uL, 18204882396923696260uL, 4863835225021711834uL, 2383740469573306109uL, 11443597294758680483uL, 7672798479355180842uL, 15393353934706501236uL,
			11268047188492900618uL, 2574732424221083732uL, 15496846687460780253uL, 7553864496710449539uL, 774575915971394212uL, 13070771000547322874uL, 4742332680126892915uL, 18305806724866841133uL, 9585621990451973437uL, 4241709297048067171uL,
			14873426574176992490uL, 8192728193547447732uL, 1430180882088029843uL, 12394585292042731469uL, 6392572887896022852uL, 16676151451390594586uL, 11724263699383504923uL, 2121089868990202181uL, 16851077223552016844uL, 6197059998517555346uL,
			3777128320185643957uL, 10065653443242894059uL, 9152705267933577826uL, 13897999300888269628uL, 13533931409481630764uL, 290836911066584434uL, 17347250220556826107uL, 5721467507677504677uL, 3246483383083001730uL, 10580854551979194076uL,
			7377509360494335573uL, 15688643228598781707uL, 10410432329431939189uL, 3432346839815153963uL, 15787007616581159330uL, 7263703119834470652uL, 479268930048826331uL, 13366077611745512069uL, 5605092910942759436uL, 17443046115025369938uL,
			9880779203361451074uL, 3946551979999280412uL, 14010815637502401941uL, 9055339030367177931uL, 2287944966553751532uL, 11536821034690928306uL, 6102262117026037307uL, 16966462053679984485uL, 14352838758776668359uL, 8715849042132865433uL,
			10106209561886798096uL, 3718588204966649934uL, 5904354664847958889uL, 17161836097907159607uL, 1918398801493775038uL, 11908900341414347744uL, 16017434224645821680uL, 7030743370106881454uL, 10747459338605847847uL, 3097853238592664697uL,
			5230550633748871006uL, 17820121808727057920uL, 286357727443617417uL, 13556455681311283159uL, 17717790348440053929uL, 5348394246754318839uL, 13730844907426388350uL, 96456829629106208uL, 7151085039179287303uL, 15917600957864729177uL,
			2905454216008497872uL, 10919350577390250894uL, 17042994471957462174uL, 6007675891775040960uL, 12099799939367095625uL, 1743020221055196183uL, 8816672212992991024uL, 14231498939321060974uL, 3545707749297729255uL, 10299607147170250681uL,
			7602666906942206371uL, 15445505097399656701uL, 2516922330208583796uL, 11328395630184845610uL, 18401115182443074061uL, 4649553799906886483uL, 12984469265752892378uL, 858347258439545476uL, 8134842447978898836uL, 14933839964366484682uL,
			4290587771152733251uL, 9534215589332256029uL, 16589917001555920442uL, 6476270640392427364uL, 12489826876976068589uL, 1337475729278723763uL, 6579657874365143501uL, 16471018100179290259uL, 1162031210497468442uL, 12680783544181324100uL,
			14812443075659151971uL, 8235731557181958973uL, 9727670450043423668uL, 4117641308438436586uL, 4767480939146612218uL, 18298709056423074980uL, 668362765688847405uL, 13158933363776466291uL, 15345596958710361684uL, 7723092170951589642uL,
			11500361534702509955uL, 2324439781390929629uL, 871847009512661375uL, 12952915978289197089uL, 5149464848442167464uL, 17919258555078410742uL, 11152761759188927185uL, 2674572973411783567uL, 15107728993420899078uL, 7958427003201086040uL,
			1549151831942788424uL, 12296196055775437846uL, 6778004993722336415uL, 16270137564314293697uL, 9484665360253785830uL, 4358112990276650936uL, 14469980012328888113uL, 8580727762175145583uL, 4527479374428473617uL, 9299857127719966799uL,
			8483418594096134342uL, 14582730410298354072uL, 12108819329727785663uL, 1715949860019731425uL, 16385456387094895464uL, 6683264387893414454uL, 2860361764176059686uL, 10982423063177940088uL, 7844695634684570865uL, 15206009786165099951uL,
			12785145775792045704uL, 1060204156351819734uL, 18014970923011477343uL, 5033164917675564545uL, 0uL, 4514320678209523063uL, 9028641356419046126uL, 4893789673109002137uL, 18057282712838092252uL, 14140823758577154219uL,
			9787579346218004274uL, 13362849919600632389uL, 13972861746229016787uL, 18397069268572108196uL, 13593480783646810685uL, 9368516906576572234uL, 4283426060203969807uL, 421051327351275640uL, 5061452472281323489uL, 8690806579843881622uL,
			13189355726925488845uL, 9920330031216291770uL, 14575386022283737123uL, 17645168074733352276uL, 5589631053278762769uL, 8301345151327754854uL, 3521382429991259647uL, 1042688955219132552uL, 8566852120407939614uL, 5207713818755897193uL,
			842102654702551280uL, 3821771963736821127uL, 10122904944562646978uL, 12888702714096296629uL, 17381613159687763244uL, 14957003768750717019uL, 6542925544232015601uL, 7235932893324139398uL, 2846566779642530847uL, 1812452399036776808uL,
			11553279960366146349uL, 11453612988773330522uL, 15931857048321489347uL, 16410911631088841908uL, 11179262106557525538uL, 11926271900343968597uL, 16602690302655509708uL, 15622578504149481915uL, 7042764859982519294uL, 6853066784115627657uL,
			2085377910438265104uL, 2474400976648244327uL, 17133704240815879228uL, 15231512072787212619uL, 10415427637511794386uL, 12550721812541744037uL, 1684205309405102560uL, 3024564567684025495uL, 7643543927473642254uL, 6103859676569933433uL,
			3183682650152946927uL, 1335493476406641048uL, 5864421166226265601uL, 8053717373499464566uL, 15471813553718360371uL, 16722141158135166020uL, 12392429592176419805uL, 10762714374571814570uL, 13085851088464031202uL, 10032998001731936405uL,
			14471865786648278796uL, 17757820860391864955uL, 5693133559285061694uL, 8188674910926378313uL, 3624904798073553616uL, 930038439454805927uL, 8391291880504120625uL, 5392441788758775878uL, 666522831895613407uL, 4006479938519712424uL,
			10298467312618266861uL, 12703977009692568986uL, 17557190854334446083uL, 14772293528376997748uL, 184727973224959791uL, 4338760439380296280uL, 9213349330128524737uL, 4718209847081162934uL, 17872557009507908339uL, 14316386129853806468uL,
			9602869102622335005uL, 13538427613172854122uL, 14085529719965038588uL, 18293564631183538827uL, 13706133568231255314uL, 9264996667719557221uL, 4170755820876530208uL, 524553836579000151uL, 4948801953296488654uL, 8794328946853156281uL,
			17246372210521275155uL, 15128007430839531108uL, 10528080418065605117uL, 12447201569125624970uL, 1571535074100104911uL, 3128067081479439288uL, 7530893412511241249uL, 6207382048146905430uL, 3368410618810205120uL, 1159933233554984631uL,
			6049129135368050990uL, 7878137543449187417uL, 15287087854947284508uL, 16897703533442448235uL, 12207719353139866866uL, 10938292072174657925uL, 6367365300305893854uL, 7420660858759451817uL, 2670986952813282096uL, 1997160369252109895uL,
			11728842332452531202uL, 11268887288928838005uL, 16107434746998929132uL, 16226201395274365851uL, 11075757463536820493uL, 12038939866828860538uL, 16499170062460812259uL, 15735231285777233556uL, 7146267370556380369uL, 6740396547736557990uL,
			2188900283088129599uL, 2361750464906216264uL, 6912381268945193135uL, 7010873824684787160uL, 2495446497949700673uL, 2019738966020448054uL, 11922695969517272435uL, 11228593644287029252uL, 15580697325944498077uL, 16618237647347641066uL,
			11386267118570123388uL, 11574870218298899723uL, 16377349821852756626uL, 15991752696440170469uL, 7249809596147107232uL, 6501625386507438295uL, 1860076878909611854uL, 2843535728253735481uL, 16782583761008241250uL, 15438798836809836309uL,
			10784883577517551756uL, 12325662529681473019uL, 1333045663791226814uL, 3231890506103886537uL, 8012959877039424848uL, 5878840392740603943uL, 2958342383554351793uL, 1704667453421543366uL, 6071425981216671839uL, 7702315889552409896uL,
			15246512461515941741uL, 17091275969342761498uL, 12599474406196739459uL, 10411272900183748852uL, 369455946449919582uL, 4289261401793139497uL, 8677520878760592560uL, 5101076439280552391uL, 18426698660257049474uL, 13915804472600733429uL,
			9436419694162325868uL, 13570175851578440731uL, 14179866567660168845uL, 18045667791066065914uL, 13368140519194000483uL, 9737690885737509140uL, 4490470872076145489uL, 69609850815340070uL, 4836151373635799487uL, 9059941384608629960uL,
			12838235447379754131uL, 10127616600346784228uL, 14944841753438971517uL, 17420109012535798538uL, 5238471324457879887uL, 8508671161142395960uL, 3890798436993455009uL, 817669608583651030uL, 8341511641753060416uL, 5576888013194725687uL,
			1049107673158000302uL, 3470370288134853593uL, 9897603906592977308uL, 13257837459261045995uL, 17588657893706312562uL, 14605562368996746757uL, 16895251939020006733uL, 15335293990851668026uL, 10897536158355849123uL, 12222142498866688724uL,
			1220375353323704465uL, 3335393082175741414uL, 7900309428648430207uL, 5982362693450024712uL, 3143070148200209822uL, 1529107418875334889uL, 6256134162958878576uL, 7526735859785697799uL, 15061786825022482498uL, 17266838269488702773uL,
			12414764096293810860uL, 10586850664359182299uL, 6736821237620410240uL, 7195601590404582135uL, 2319866467109969262uL, 2204447144542146585uL, 12098258270736101980uL, 11043868011013947179uL, 15756275086898374834uL, 16433527336370644421uL,
			11282762275832987475uL, 11687538397384208932uL, 16273829789963511229uL, 16104405274056517834uL, 7353312173293554319uL, 6388955079261997048uL, 1963599176398131297uL, 2730885278789328150uL, 12734730600611787708uL, 10240284774872792779uL,
			14841321717518903634uL, 17532761585592837157uL, 5341973905626564192uL, 8396000858464455447uL, 3994320738504219790uL, 705019163686736377uL, 8165951605860780911uL, 5761615774892279320uL, 873527637750763905uL, 3655078462634319094uL,
			10073166212371119795uL, 13073111830018782148uL, 17764235659219493981uL, 14420852062050576682uL, 554183707073401201uL, 4113701362679307270uL, 8862229056480431007uL, 4925496404946208488uL, 18241973027794281645uL, 14091366777305852378uL,
			9251709388290096707uL, 13745753620313043764uL, 14292534741112760738uL, 17942162941077202133uL, 13480793095473115980uL, 9634170850892037691uL, 4377800566176259198uL, 173112430909559049uL, 4723500929812432528uL, 9163463689340423143uL,
			0uL, 1627076877180555749uL, 3254153754361111498uL, 4304766511847567919uL, 6508307508722222996uL, 5532301601655232113uL, 8609533023695135838uL, 7057067394980558267uL, 13016615017444445992uL, 11687060557970792141uL,
			11064603203310464226uL, 10311506129017994503uL, 17219066047390271676uL, 17898114823170201945uL, 14114134789961116534uL, 15369645348968741523uL, 6742375288560323899uL, 5405783375000362206uL, 8122427758785605361uL, 7362295566600601364uL,
			559464125406104239uL, 1249489845609208650uL, 3092440436427811173uL, 4358930276052383872uL, 16804606958606221843uL, 18420141604947973110uL, 14131424163184419289uL, 15170497026562715708uL, 12927534132269761927uL, 11958000392873448546uL,
			11407266381551363661uL, 9861275257238108072uL, 13484750577120647798uL, 12517751835485555603uL, 10811566750000724412uL, 9268108286718044249uL, 16244855517571210722uL, 17862925160600374279uL, 14724591133201202728uL, 15766196656536268749uL,
			1118928250812208478uL, 1806984259669771451uL, 2498979691218417300uL, 3763497483303675761uL, 6184880872855622346uL, 4846319249023796015uL, 8717860552104767744uL, 7955756313454125285uL, 16623577522202187597uL, 17304596008253965992uL,
			14671562341988250759uL, 15929044947461498210uL, 13610133830783652057uL, 12282549082655523132uL, 10505203605321052947uL, 9754078578567881462uL, 5915057869070080101uL, 4936516965315660160uL, 9169208254935317423uL, 7614209966263953994uL,
			595784637983776753uL, 2220326517403094548uL, 2697011182507326523uL, 3745091278963191262uL, 4800780284202952583uL, 6056011051257099874uL, 8049867273830073421uL, 8728634058901706152uL, 1787343054361387027uL, 1033966189054898678uL,
			3883502080015090649uL, 2553665630772597308uL, 17742918502166505647uL, 16190171914609523018uL, 15785839923567102821uL, 14809550858366016128uL, 12423642936564150075uL, 13473974734000176862uL, 9313645190084349169uL, 10940438907922783508uL,
			2237856501624416956uL, 692146337361591129uL, 3613968519339542902uL, 2644717939285350547uL, 4997959382436834600uL, 6037313204657536205uL, 7526994966607351522uL, 9142812771083198215uL, 12369761745711244692uL, 13636531376349836401uL,
			9692638498047592030uL, 10382946208019535803uL, 17435721104209535488uL, 16675868703978009573uL, 15911512626908250570uL, 14575202704056586287uL, 17995461689412730353uL, 17233077589954915348uL, 15318337272343567931uL, 13979493588949749726uL,
			11812552860237691493uL, 13076790792723671936uL, 10288347611577407919uL, 10976121562080872522uL, 5555462180541969113uL, 6596786813619888956uL, 6931573031267504403uL, 8549359584747831542uL, 1678382892091667789uL, 134643537612252328uL,
			4207421707319105159uL, 3240139875195586402uL, 11830115738140160202uL, 12878476725792760111uL, 9873033930631320320uL, 11497858900539065061uL, 18338416509870634846uL, 16783699111456417467uL, 15228419932527907988uL, 14250162118322324849uL,
			1191569275967553506uL, 440724108813659655uL, 4440653034806189096uL, 3113350345032158669uL, 5394022365014653046uL, 6651784831295072659uL, 7490182557926382524uL, 8171483103540152921uL, 9601560568405905166uL, 10640618042396122859uL,
			12112022102514199748uL, 13727536961421360417uL, 16099734547660146842uL, 14553723636986370431uL, 17457268117803412304uL, 16487718990093089461uL, 3574686108722774054uL, 2814573703972032963uL, 2067932378109797356uL, 731355853937586697uL,
			7767004160030181298uL, 9033509387969091159uL, 5107331261545194616uL, 5797376768109147549uL, 15613455359580684853uL, 14860378081345967056uL, 17692164036055753215uL, 16362624974593339418uL, 9438425375779358113uL, 10693951331724809284uL,
			12670057618718205547uL, 13349126189482473358uL, 7785154244699704605uL, 8835751604174593272uL, 4693734945791798999uL, 6320792026914275122uL, 3915963146398206601uL, 2363477722699619180uL, 1977458617861056835uL, 1001437313856567462uL,
			4475713003248833912uL, 2920694927143142557uL, 1384292674723182258uL, 405736381580945239uL, 7227937038679085804uL, 8276001746820678409uL, 5289435878570701094uL, 6913957971629236419uL, 9995918764873669200uL, 11253416759734930357uL,
			12074626409315072410uL, 12755664682801375359uL, 15053989933214703044uL, 14302884692822117409uL, 18285625542166396430uL, 16958056182352343019uL, 7173486372772960323uL, 8438019562869721510uL, 5666729273999653769uL, 6354805078915230316uL,
			4170175935399174103uL, 3408091491732606514uL, 1510504066800368669uL, 171957839906106872uL, 15503951495479842667uL, 13960473236139345550uL, 18014409663306855585uL, 17047395523660457284uL, 10194810967608725759uL, 11236401094006030618uL,
			11552345569517220661uL, 13170395417562112720uL, 14384589897994791049uL, 15074917395401259372uL, 16900118059129523011uL, 18166903079156128422uL, 11381286197612752669uL, 10044991663075163896uL, 12743886119367675095uL, 11984053505496735026uL,
			8287777973661886369uL, 7318512004219876932uL, 6786090870343910507uL, 5240360918646756750uL, 2978635386689495093uL, 4594433404804559312uL, 324028840084734975uL, 1363367273991164442uL, 11110924361083938226uL, 10134619897871545431uL,
			13193573627239777912uL, 11640807243624978333uL, 13863146062535008806uL, 15489919985389171651uL, 17098719169495663084uL, 18149035569993928713uL, 3356765784183335578uL, 4035552365312981887uL, 269287075224504656uL, 1524533240290154677uL,
			8414843414638210318uL, 7085022362333281515uL, 6480279750391172804uL, 5726922680068759329uL, 8972051685281362687uL, 7644764384895083290uL, 5884571808997396789uL, 5133746429277765840uL, 2797023761118180715uL, 3478344093092799630uL,
			862463327592519329uL, 2120241182187276100uL, 14422618499663363543uL, 16047423682136517682uL, 16505266596615020061uL, 17553612194879541240uL, 10553420664906988099uL, 9575147462387395494uL, 13788997000709970313uL, 12234259815935232108uL,
			2383138551935107012uL, 4000905309357354529uL, 881448217627319310uL, 1922757452693671403uL, 8881306069612378192uL, 7914008840551360949uL, 6226700690064317338uL, 4682941540600892031uL, 10788044730029306092uL, 9449216444646727945uL,
			13303569662590145318uL, 12541205359190082243uL, 14980365115852765048uL, 15668158861340567197uL, 16342966207080305842uL, 17607219536504112471uL, 0uL, 787345783882751359uL, 1574691567765502718uL, 2249367087917868929uL,
			3149383135531005436uL, 2403213109735319683uL, 4498734175835737858uL, 3784009074528670333uL, 6298766271062010872uL, 6738652078272902791uL, 4806426219470639366uL, 5214867102192651385uL, 8997468351671475716uL, 8516407063399170939uL,
			7568018149057340666uL, 7199627124515158405uL, 12597532542124021744uL, 11834997800763017871uL, 13477304156545805582uL, 12818430263456788593uL, 9612852438941278732uL, 10343220837673090931uL, 10429734204385302770uL, 11119648263171147149uL,
			17994936703342951432uL, 17534744561339769207uL, 17032814126798341878uL, 16613076324741389193uL, 15136036298114681332uL, 15628394505721402507uL, 14399254249030316810uL, 14787951608365313653uL, 7602072216140004491uL, 7175020041742557684uL,
			8981745304614405749uL, 8523246006843251466uL, 4812983652164716919uL, 5198862542380848136uL, 6273877681976996745uL, 6772424636753178358uL, 4473845649564555123uL, 3817781704412842508uL, 3155940635334143373uL, 2387208625622118642uL,
			1558968592112198287uL, 2256206094175911920uL, 34054142781659249uL, 762738768218817806uL, 14388034875940531067uL, 14790287011522967044uL, 15165586827024978309uL, 15608291103391960314uL, 17012429212251037319uL, 16642345343438554104uL,
			17996990598435087481uL, 17523243682474440966uL, 10431788023778705539uL, 11108147317196889596uL, 9592467452989946493uL, 10372489793556555522uL, 13506854618347304319uL, 12798326785428482048uL, 12586313106220142465uL, 11837333132517036798uL,
			15204144432280008982uL, 15569733498547987561uL, 14350040083485115368uL, 14828281804104195735uL, 17963490609228811498uL, 17556743671269659029uL, 17046492013686502932uL, 16608282541877275499uL, 9625967304329433838uL, 10338989941806010257uL,
			10397725084761696272uL, 11142210256088085871uL, 12547755363953993490uL, 11875890875194243693uL, 13544849273506356716uL, 12760332130395242643uL, 8947691299129110246uL, 8557300012202734489uL, 7635563408825685016uL, 7141528848645818727uL,
			6311881270668286746uL, 6734421048187700837uL, 4774417251244237284uL, 5237428943712386203uL, 3117937184224396574uL, 2425212076857677921uL, 4512412188351823840uL, 3779215166036632223uL, 68108285563318498uL, 728684625311346077uL,
			1525477536437635612uL, 2289697149439416163uL, 13510804007889421725uL, 12784930411702114530uL, 12563469603102752611uL, 11869060739658474012uL, 10391176462114893921uL, 11158206005852613918uL, 9650847094104459935uL, 10305226182635722720uL,
			17071371732057632357uL, 16574518719893156634uL, 17956941910883407003uL, 17572739353925126628uL, 14365754259819912089uL, 14821451597164660454uL, 15170099099554406759uL, 15594331704155864088uL, 1536688116651627117uL, 2287370539157557010uL,
			38566538791528595uL, 748779245502281196uL, 4532788318621656977uL, 3749954931616938734uL, 3115892079852313967uL, 2436704165002952720uL, 4772372213981084053uL, 5248921107556394218uL, 6332257463751820139uL, 6705160885172035092uL,
			7606021737752759401uL, 7161623535945552150uL, 8958901950746736279uL, 8554973464734968808uL, 10459284472548778311uL, 11226314016269597752uL, 9582739082848525241uL, 10237118171430241990uL, 13443821621981287611uL, 12717948025743509956uL,
			12630451988759195205uL, 11936043125331833658uL, 14289747082974300863uL, 14745444420335704000uL, 15246106277222068289uL, 15670338881773317438uL, 17148504534435626819uL, 16651651522321343036uL, 17879809108757104061uL, 17495606551782185154uL,
			4608795221377857207uL, 3825961834390039496uL, 3039885177918164041uL, 2360697263018348854uL, 1459555039613862731uL, 2210237462170263092uL, 115699616080984501uL, 825912322774820042uL, 7537913451652472143uL, 7093515249828610096uL,
			9027010236024973233uL, 8623081750063413966uL, 4839354324793174195uL, 5315903218318292428uL, 6265275352688038477uL, 6638178774124891954uL, 17895382598258220492uL, 17488635660315705523uL, 17114600024405468978uL, 16676390552546048589uL,
			15271126817651370032uL, 15636715883969557839uL, 14283057697291637454uL, 14761299417894064049uL, 12623762541336573492uL, 11951898052559907659uL, 13468842096375401674uL, 12684324953314759093uL, 9548834502488474568uL, 10261857139914596023uL,
			10474857887424772406uL, 11219343058768061513uL, 6235874368448793148uL, 6658414145951569731uL, 4850424153715355842uL, 5313435846233697725uL, 9024824376703647680uL, 8634433089727062719uL, 7558430332073264446uL, 7064395771910051905uL,
			136216571126636996uL, 796792910891580603uL, 1457369250622692154uL, 2221588863574001221uL, 3050955072875271224uL, 2358229965559007559uL, 4579394298878832326uL, 3846197276546741177uL, 4888990554929436753uL, 5274869445162206510uL,
			6197870780034327215uL, 6696417734760316880uL, 7524939139093982637uL, 7097886964746744018uL, 9058878381912119123uL, 8600379084124310060uL, 1490860306003653545uL, 2188097808050450134uL, 102162428068153687uL, 830847053555782696uL,
			4540827760385164885uL, 3884763815182998314uL, 3088958524261842091uL, 2320226514566717908uL, 17080537222693179297uL, 16710453353864057566uL, 17928882587170895199uL, 17455135671260440608uL, 14321052490023877213uL, 14723304625556104994uL,
			15232569212689940643uL, 15675273489073577436uL, 13430847441493173337uL, 12722319608591268134uL, 12662320283896323751uL, 11913340310142747608uL, 10508920826164957605uL, 11185280119633595610uL, 9515334650855385947uL, 10295356991405094436uL,
			3073376233303254234uL, 2327206207490668965uL, 4574741078315114020uL, 3860015977058501467uL, 77133077583057190uL, 864478861415337049uL, 1497558491004562392uL, 2172234011173844647uL, 9065576637243313954uL, 8584515348987662941uL,
			7499909863233877468uL, 7131518838641485987uL, 6231784159704627934uL, 6671669966965712801uL, 4873408330005905440uL, 5281849212711279967uL, 9544744427962168106uL, 10275112826710879829uL, 10497842215112788436uL, 11187756273848177835uL,
			12664514927503640278uL, 11901980186193107881uL, 13410321770344070184uL, 12751447877238137175uL, 15212043475505518802uL, 15704401683095586221uL, 14323247071891104300uL, 14711944431276310355uL, 17917803901493472558uL, 17457611759440097361uL,
			17109946929469937616uL, 16690209127429622447uL, 0uL, 12735104804238718208uL, 6170348077646248299uL, 16509444970853862507uL, 12340696155292496598uL, 2017291147968776150uL, 18366681100960566205uL, 5647410020680629949uL,
			7087766252628637383uL, 15195395291188200391uL, 4034582295937552300uL, 9746239707350891180uL, 14492082353831314449uL, 8764592317971984657uL, 11294820041361259898uL, 3171498017405012090uL, 14175532505257274766uL, 8360288316147898510uL,
			10455354299411240165uL, 2424290165905580517uL, 8069164591875104600uL, 16088898739697412696uL, 4204695827194471987uL, 10008751866545607475uL, 12026113418985642825uL, 1610450521212481097uL, 17529184635943969314uL, 4897667708912932642uL,
			983934998540085663uL, 13626641174839860383uL, 6342996034810024180uL, 16769987818756040180uL, 13666754959799896183uL, 943010859455266167uL, 16720576632295797020uL, 6392654625517126684uL, 1650821038878145185uL, 11985427320909746081uL,
			4848580331811161034uL, 17579150543333897930uL, 16138329183750209200uL, 8019488974878067632uL, 9968618824059996123uL, 4245636992636465883uL, 8409391654388943974uL, 14125548474144930150uL, 2383903687033664781uL, 10496058521142312973uL,
			8724469239847757305uL, 14533015753876459769uL, 3220901042424962194uL, 11245169644315093394uL, 15155034169900160815uL, 7128442917811298863uL, 9795335417825865284uL, 3984608091673254724uL, 1967869997080171326uL, 12390362511329207870uL,
			5687551224606534229uL, 18325731741857636181uL, 12685992069620048360uL, 49993464005580008uL, 16549823116352575619uL, 6129652152789544323uL, 5732242439398949765uL, 18388525449166714501uL, 1886021718910532334uL, 12290412006796791790uL,
			16487662723588944211uL, 6085453523643991123uL, 12785309251034253368uL, 131349165135199544uL, 3301642077756290370uL, 11343977721637208130uL, 8678633399360621609uL, 14469112605244913961uL, 9697160663622322068uL, 3904359633382109844uL,
			15218304003986685695uL, 7173786172651670527uL, 10030817736304094731uL, 4289874141128104715uL, 16038977949756135264uL, 7938099221034263136uL, 2339173952895261917uL, 10433226294353193437uL, 8491273985272931766uL, 14225533031257575606uL,
			16818783308777887948uL, 6472935006220938700uL, 13603448873973965223uL, 897631353009542311uL, 4767807374067329562uL, 17480310543733595930uL, 1696693130970731377uL, 12049366721281123953uL, 17448938479695514610uL, 4799494982008422130uL,
			12071458975870825113uL, 1673722382600095641uL, 6441802084849924388uL, 16850726552061430820uL, 920029822624186447uL, 13580803032324715855uL, 10411153299503529269uL, 2362127677123446837uL, 14256885835622597726uL, 8459603401541529950uL,
			4267491630505250787uL, 10053445452083444451uL, 7969216183346509448uL, 16007052832275252104uL, 3935739994160342652uL, 9665464722442244988uL, 7151703350820986647uL, 15241265356113914391uL, 11375102449213068458uL, 3269706996047258026uL,
			14446704874535726529uL, 8701288534308280513uL, 6107518221753503931uL, 16464717332599743931uL, 99986928011160016uL, 12816989231009062096uL, 18410916153584722541uL, 5709606562045089645uL, 12259304305579088646uL, 1917937543093005830uL,
			11464484878797899530uL, 3433473658752004618uL, 14608395860728905313uL, 8793099033037802337uL, 3772043437821064668uL, 9576011654218252508uL, 7059822771495537847uL, 15079645017149569463uL, 18250491813091111373uL, 5619062709015053517uL,
			12170907047287982246uL, 1755156051682065830uL, 6198132172524913435uL, 16625071043010131483uL, 262698330270399088uL, 12905457145152933744uL, 6603284155512580740uL, 16942468872748801924uL, 1009625548606182383uL, 13744642253938255599uL,
			17357266798721243218uL, 4637942803906882898uL, 11907549105953988921uL, 1584196738286916665uL, 4179162533076442179uL, 9890872859023141187uL, 7808719266764219688uL, 15916295665668198440uL, 10573796523720076949uL, 2450386675736288149uL,
			14347572345303341054uL, 8620170408349590270uL, 2499525798771838845uL, 10523847040318618237uL, 8579748282256209430uL, 14388241881750487830uL, 9940267621763300779uL, 4129452196704045227uL, 15876198442068526272uL, 7849695014008271296uL,
			4678347905790523834uL, 17316616521947204794uL, 1535074639393865937uL, 11957479329615361489uL, 16982547970545863532uL, 6562324367327247980uL, 13695265617067946503uL, 1059319925986838279uL, 16575923724174533363uL, 6248089815227586547uL,
			12945870012441877400uL, 222038089143299736uL, 5569676245288374309uL, 18300193818229384485uL, 1795262706019084622uL, 12129921901510450254uL, 9535614748134659124uL, 3812685555293888820uL, 15128776374847057247uL, 7009883252514117727uL,
			3393386261941462754uL, 11505452998217356258uL, 8842466239170764681uL, 14558710881881729673uL, 15030495895401333903uL, 6929670081212969359uL, 9598989964016844260uL, 3857993729185384676uL, 8923312724748924505uL, 14657483946705879897uL,
			3347444765200191282uL, 11441585205563587122uL, 12883604169699848776uL, 177874472764147528uL, 16675346561574439715uL, 6329410160110992931uL, 1840059645248372894uL, 12192680321024226718uL, 5487722586128382453uL, 18200278395451149557uL,
			1454266669001206017uL, 11858744779842329601uL, 4724255354246893674uL, 17380450266181756266uL, 13793507581462649815uL, 1139494582102912727uL, 16919206803083059900uL, 6517050241721014204uL, 8534983261010501574uL, 14325515380354411206uL,
			2581443210487391917uL, 10623726215786468269uL, 15938432366693018896uL, 7893826712404203536uL, 9840881031673877627uL, 4048168099265503611uL, 7871479988320685304uL, 15961094632196520440uL, 4079249412343018899uL, 9808921227164237971uL,
			14303406701641973294uL, 8557902263581625134uL, 10655114841319130949uL, 2549807211108184645uL, 1108396242668173887uL, 13825486643497914175uL, 6539413992094516052uL, 16896525279986955860uL, 11827338030654540009uL, 1485918629586615785uL,
			17402577068616561026uL, 4701320390536545410uL, 12215036443507007862uL, 1817387949141742710uL, 18169205413473540125uL, 5519674091758759197uL, 199973856022320032uL, 12860694426067379872uL, 6298013375142898379uL, 16706990756887912395uL,
			14688572887741355953uL, 8891343093316788913uL, 11419213124090179290uL, 3370134587175558106uL, 6961086125855024487uL, 14998834675877269607uL, 3835875086186011660uL, 9621916731792926988uL
		};

		private static ulong[] m_uX2N = new ulong[64]
		{
			36028797018963968uL, 140737488355328uL, 2147483648uL, 11127430586519243189uL, 1221807072334778582uL, 8138107668736496166uL, 3535907488026231341uL, 3441480032374409582uL, 14577399307959786116uL, 18108090348644263661uL,
			16872192058669208586uL, 6470743304385776052uL, 2356340599778112122uL, 3282589245383169411uL, 4813772690843722589uL, 2123572894795720761uL, 13361200295056722901uL, 6714339060555490282uL, 14813753520627564098uL, 2575392968416258319uL,
			3781498879954341639uL, 2029575846824038122uL, 11210212698471928928uL, 2698801079951716530uL, 5369600018057262368uL, 10474170701250152447uL, 12693693266037740021uL, 14926416049973907054uL, 12778104656887448071uL, 14865980173302332725uL,
			5898182234392518323uL, 11506987473448080863uL, 8338456165086097871uL, 6752081962152672116uL, 18050102898699742154uL, 14183399822074814198uL, 12097687167394233669uL, 10929469909668459249uL, 1876517811888844395uL, 4541923943557071381uL,
			5671875008802276871uL, 5352236642095812106uL, 3280130613932697821uL, 9694238273182980114uL, 5923064843803063040uL, 11819714483094804698uL, 15354363091388458338uL, 1261561810665423398uL, 11395176905717369489uL, 11207650524541939074uL,
			12880488140263503615uL, 3418619511318683672uL, 4382979584528688930uL, 9313537133046378584uL, 13796321753777571544uL, 9630359120624875631uL, 3783882481749783366uL, 10828018389360610332uL, 1514337215546087857uL, 16997139097890852538uL,
			10296581199785350754uL, 4611686018427387904uL, 2305843009213693952uL, 576460752303423488uL
		};

		private const ulong m_uComplement = ulong.MaxValue;

		private const int m_uBitWidth = 64;

		public static ulong ComputeSlicedSafe(ReadOnlySpan<byte> src, ulong uCrc)
		{
			int i = 0;
			ulong num = (ulong)src.Length;
			uCrc ^= 0xFFFFFFFFFFFFFFFFuL;
			ulong num2 = 0uL;
			ulong num3 = num - num % 32;
			if (num3 >= 64)
			{
				ulong num4 = 0uL;
				ulong num5 = 0uL;
				ulong num6 = 0uL;
				ulong num7 = 0uL;
				int num8 = i + (int)num3 - 32;
				num -= num3;
				num4 = uCrc;
				for (; i < num8; i += 32)
				{
					ulong num9 = BinaryPrimitives.ReadUInt64LittleEndian(src.Slice(i)) ^ num4;
					ulong num10 = BinaryPrimitives.ReadUInt64LittleEndian(src.Slice(i + 8)) ^ num5;
					ulong num11 = BinaryPrimitives.ReadUInt64LittleEndian(src.Slice(i + 16)) ^ num6;
					ulong num12 = BinaryPrimitives.ReadUInt64LittleEndian(src.Slice(i + 24)) ^ num7;
					num4 = m_u32[1792 + (num9 & 0xFF)];
					num9 >>= 8;
					num5 = m_u32[1792 + (num10 & 0xFF)];
					num10 >>= 8;
					num6 = m_u32[1792 + (num11 & 0xFF)];
					num11 >>= 8;
					num7 = m_u32[1792 + (num12 & 0xFF)];
					num12 >>= 8;
					num4 ^= m_u32[1536 + (num9 & 0xFF)];
					num9 >>= 8;
					num5 ^= m_u32[1536 + (num10 & 0xFF)];
					num10 >>= 8;
					num6 ^= m_u32[1536 + (num11 & 0xFF)];
					num11 >>= 8;
					num7 ^= m_u32[1536 + (num12 & 0xFF)];
					num12 >>= 8;
					num4 ^= m_u32[1280 + (num9 & 0xFF)];
					num9 >>= 8;
					num5 ^= m_u32[1280 + (num10 & 0xFF)];
					num10 >>= 8;
					num6 ^= m_u32[1280 + (num11 & 0xFF)];
					num11 >>= 8;
					num7 ^= m_u32[1280 + (num12 & 0xFF)];
					num12 >>= 8;
					num4 ^= m_u32[1024 + (num9 & 0xFF)];
					num9 >>= 8;
					num5 ^= m_u32[1024 + (num10 & 0xFF)];
					num10 >>= 8;
					num6 ^= m_u32[1024 + (num11 & 0xFF)];
					num11 >>= 8;
					num7 ^= m_u32[1024 + (num12 & 0xFF)];
					num12 >>= 8;
					num4 ^= m_u32[768 + (num9 & 0xFF)];
					num9 >>= 8;
					num5 ^= m_u32[768 + (num10 & 0xFF)];
					num10 >>= 8;
					num6 ^= m_u32[768 + (num11 & 0xFF)];
					num11 >>= 8;
					num7 ^= m_u32[768 + (num12 & 0xFF)];
					num12 >>= 8;
					num4 ^= m_u32[512 + (num9 & 0xFF)];
					num9 >>= 8;
					num5 ^= m_u32[512 + (num10 & 0xFF)];
					num10 >>= 8;
					num6 ^= m_u32[512 + (num11 & 0xFF)];
					num11 >>= 8;
					num7 ^= m_u32[512 + (num12 & 0xFF)];
					num12 >>= 8;
					num4 ^= m_u32[256 + (num9 & 0xFF)];
					num9 >>= 8;
					num5 ^= m_u32[256 + (num10 & 0xFF)];
					num10 >>= 8;
					num6 ^= m_u32[256 + (num11 & 0xFF)];
					num11 >>= 8;
					num7 ^= m_u32[256 + (num12 & 0xFF)];
					num12 >>= 8;
					num4 ^= m_u32[num9 & 0xFF];
					num5 ^= m_u32[num10 & 0xFF];
					num6 ^= m_u32[num11 & 0xFF];
					num7 ^= m_u32[num12 & 0xFF];
				}
				uCrc = 0uL;
				uCrc ^= BinaryPrimitives.ReadUInt64LittleEndian(src.Slice(i)) ^ num4;
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc ^= BinaryPrimitives.ReadUInt64LittleEndian(src.Slice(i + 8)) ^ num5;
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc ^= BinaryPrimitives.ReadUInt64LittleEndian(src.Slice(i + 16)) ^ num6;
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc ^= BinaryPrimitives.ReadUInt64LittleEndian(src.Slice(i + 24)) ^ num7;
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				uCrc = (uCrc >> 8) ^ m_u1[uCrc & 0xFF];
				i += 32;
			}
			num2 = 0uL;
			while (num2 < num)
			{
				uCrc = (uCrc >> 8) ^ m_u1[(uCrc ^ src[i]) & 0xFF];
				num2++;
				i++;
			}
			return uCrc ^ 0xFFFFFFFFFFFFFFFFuL;
		}

		internal static ulong Concatenate(ulong uInitialCrcAB, ulong uInitialCrcA, ulong uFinalCrcA, ulong uSizeA, ulong uInitialCrcB, ulong uFinalCrcB, ulong uSizeB)
		{
			ulong num = uFinalCrcA ^ 0xFFFFFFFFFFFFFFFFuL;
			if (uInitialCrcA != uInitialCrcAB)
			{
				num ^= MulX_N(uInitialCrcA ^ uInitialCrcAB, uSizeA);
			}
			num ^= uInitialCrcB ^ 0xFFFFFFFFFFFFFFFFuL;
			num = MulX_N(num, uSizeB);
			return num ^ uFinalCrcB;
		}

		private static ulong MulX_N(ulong a, ulong uSize)
		{
			int num = 0;
			ulong num2 = a;
			uSize >>= num;
			while (uSize != 0L)
			{
				if ((uSize & 1) == 1)
				{
					num2 = MulPoly(num2, m_uX2N[num]);
				}
				uSize >>= 1;
				num++;
			}
			return num2;
		}

		private static ulong MulPoly(ulong a, ulong b)
		{
			return MulPolyUnrolled(a, b);
		}

		private static ulong MulPolyUnrolled(ulong a, ulong b)
		{
			ulong num = poly;
			ulong num2 = (num >> 1) ^ (num * (num & 1));
			int num3 = 64;
			ulong[] array = new ulong[4]
			{
				0uL,
				num2,
				num,
				num ^ num2
			};
			int[] array2 = new int[2]
			{
				num3 - 2,
				num3 - 1
			};
			ulong[] array3 = new ulong[2]
			{
				(b >> 1) ^ array[(b & 1) << 1],
				b
			};
			ulong[] array4 = new ulong[2];
			for (int i = 0; i < num3; i += 2)
			{
				for (int j = 0; j < 2; j++)
				{
					array4[j] ^= array3[j] * ((a >> array2[j]) & 1);
					array3[j] = (array3[j] >> 2) ^ array[array3[j] & 3];
				}
				a <<= 2;
			}
			return array4[0] ^ array4[1];
		}
	}
	internal static class StorageCrc64Composer
	{
		public static Memory<byte> Compose(params (byte[] Crc64, long OriginalDataLength)[] partitions)
		{
			return Compose(partitions.AsEnumerable());
		}

		public static Memory<byte> Compose(IEnumerable<(byte[] Crc64, long OriginalDataLength)> partitions)
		{
			return new Memory<byte>(BitConverter.GetBytes(Compose(partitions.Select(((byte[] Crc64, long OriginalDataLength) tup) => (BitConverter.ToUInt64(tup.Crc64, 0), OriginalDataLength: tup.OriginalDataLength)))));
		}

		public static ulong Compose(IEnumerable<(ulong Crc64, long OriginalDataLength)> partitions)
		{
			ulong num = 0uL;
			long num2 = 0L;
			foreach (var partition in partitions)
			{
				num = StorageCrc64Calculator.Concatenate(0uL, 0uL, num, (ulong)num2, 0uL, partition.Crc64, (ulong)partition.OriginalDataLength);
				num2 += partition.OriginalDataLength;
			}
			return num;
		}
	}
	internal static class UriExtensions
	{
		public static Uri AppendToPath(this Uri uri, string segment)
		{
			UriBuilder uriBuilder = new UriBuilder(uri);
			string path = uriBuilder.Path;
			string text = ((path.Length == 0 || path[path.Length - 1] != '/') ? "/" : "");
			segment = segment.Replace("%", "%25");
			uriBuilder.Path = uriBuilder.Path + text + segment;
			return uriBuilder.Uri;
		}

		public static IDictionary<string, string> GetQueryParameters(this Uri uri)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string text = uri.Query ?? "";
			if (!string.IsNullOrEmpty(text))
			{
				if (text.StartsWith("?", ignoreCase: true, CultureInfo.InvariantCulture))
				{
					text = text.Substring(1);
				}
				string[] array = text.Split(new char[1] { '&' }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[1] { '=' }, 2);
					string key = WebUtility.UrlDecode(array2[0]);
					if (array2.Length == 1)
					{
						dictionary.Add(key, null);
					}
					else
					{
						dictionary.Add(key, WebUtility.UrlDecode(array2[1]));
					}
				}
			}
			return dictionary;
		}

		public static string GetAccountNameFromDomain(this Uri uri, string serviceSubDomain)
		{
			return GetAccountNameFromDomain(uri.Host, serviceSubDomain);
		}

		public static string GetAccountNameFromDomain(string host, string serviceSubDomain)
		{
			int num = host.IndexOf(".", StringComparison.InvariantCulture);
			if (num >= 0)
			{
				if (host.IndexOf(serviceSubDomain, num, StringComparison.InvariantCulture) <= -1)
				{
					return null;
				}
				return host.Substring(0, num);
			}
			return null;
		}

		public static string GetPath(this Uri uri)
		{
			if (uri.AbsolutePath[0] != '/')
			{
				return uri.AbsolutePath;
			}
			return uri.AbsolutePath.Substring(1);
		}

		public static bool IsHostIPEndPointStyle(this Uri uri)
		{
			if (string.IsNullOrEmpty(uri.Host) || uri.Host.IndexOf(".", StringComparison.InvariantCulture) < 0 || !IPAddress.TryParse(uri.Host, out var _))
			{
				return Constants.Sas.PathStylePorts.Contains(uri.Port);
			}
			return true;
		}

		internal static void AppendQueryParameter(this StringBuilder sb, string key, string value)
		{
			sb.Append((sb.Length > 0) ? "&" : "").Append(key).Append('=')
				.Append(value);
		}
	}
	internal sealed class UriQueryParamsCollection : Dictionary<string, string>
	{
		public UriQueryParamsCollection()
			: base((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase)
		{
		}

		public UriQueryParamsCollection(string encodedQueryParamString)
			: base((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase)
		{
			encodedQueryParamString = encodedQueryParamString ?? throw Errors.ArgumentNull("encodedQueryParamString");
			if (encodedQueryParamString.StartsWith("?", ignoreCase: true, CultureInfo.InvariantCulture))
			{
				encodedQueryParamString = encodedQueryParamString.Substring(1);
			}
			string[] array = encodedQueryParamString.Split(new char[1] { '&' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[1] { '=' }, 2);
				if (array2.Length == 1)
				{
					Add(WebUtility.UrlDecode(array2[0]), null);
				}
				else
				{
					Add(WebUtility.UrlDecode(array2[0]), WebUtility.UrlDecode(array2[1]));
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (Enumerator enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, string> current = enumerator.Current;
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append('&');
					}
					stringBuilder.Append(WebUtility.UrlEncode(current.Key)).Append('=').Append(WebUtility.UrlEncode(current.Value));
				}
			}
			return stringBuilder.ToString();
		}
	}
	internal class StorageBearerTokenChallengeAuthorizationPolicy : BearerTokenAuthenticationPolicy
	{
		private volatile string[] _scopes;

		private volatile string tenantId;

		private readonly bool _enableTenantDiscovery;

		public StorageBearerTokenChallengeAuthorizationPolicy(TokenCredential credential, string scope, bool enableTenantDiscovery)
			: base(credential, scope)
		{
			Azure.Core.Argument.AssertNotNullOrEmpty(scope, "scope");
			_scopes = new string[1] { scope };
			_enableTenantDiscovery = enableTenantDiscovery;
		}

		public StorageBearerTokenChallengeAuthorizationPolicy(TokenCredential credential, IEnumerable<string> scopes, bool enableTenantDiscovery)
			: base(credential, scopes)
		{
			Azure.Core.Argument.AssertNotNull(scopes, "scopes");
			_scopes = scopes.ToArray();
			_enableTenantDiscovery = enableTenantDiscovery;
		}

		protected override void AuthorizeRequest(HttpMessage message)
		{
			AuthorizeRequestInternal(message, async: false).EnsureCompleted();
		}

		protected override ValueTask AuthorizeRequestAsync(HttpMessage message)
		{
			return AuthorizeRequestInternal(message, async: true);
		}

		private async ValueTask AuthorizeRequestInternal(HttpMessage message, bool async)
		{
			if (tenantId != null || !_enableTenantDiscovery)
			{
				TokenRequestContext context = new TokenRequestContext(_scopes, message.Request.ClientRequestId, null, tenantId, isCaeEnabled: false);
				if (async)
				{
					await AuthenticateAndAuthorizeRequestAsync(message, context).ConfigureAwait(continueOnCapturedContext: false);
				}
				else
				{
					AuthenticateAndAuthorizeRequest(message, context);
				}
			}
		}

		protected override bool AuthorizeRequestOnChallenge(HttpMessage message)
		{
			return AuthorizeRequestOnChallengeInternalAsync(message, async: false).EnsureCompleted();
		}

		protected override ValueTask<bool> AuthorizeRequestOnChallengeAsync(HttpMessage message)
		{
			return AuthorizeRequestOnChallengeInternalAsync(message, async: true);
		}

		private async ValueTask<bool> AuthorizeRequestOnChallengeInternalAsync(HttpMessage message, bool async)
		{
			try
			{
				string challengeParameterFromResponse = Azure.Core.AuthorizationChallengeParser.GetChallengeParameterFromResponse(message.Response, "Bearer", "authorization_uri");
				tenantId = new Uri(challengeParameterFromResponse).Segments[1].Trim(new char[1] { '/' });
				string challengeParameterFromResponse2 = Azure.Core.AuthorizationChallengeParser.GetChallengeParameterFromResponse(message.Response, "Bearer", "resource_id");
				if (challengeParameterFromResponse2 != null)
				{
					challengeParameterFromResponse2 += "/.default";
					_scopes = new string[1] { challengeParameterFromResponse2 };
				}
				TokenRequestContext context = new TokenRequestContext(_scopes, message.Request.ClientRequestId, null, tenantId, isCaeEnabled: false);
				if (async)
				{
					await AuthenticateAndAuthorizeRequestAsync(message, context).ConfigureAwait(continueOnCapturedContext: false);
				}
				else
				{
					AuthenticateAndAuthorizeRequest(message, context);
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
	internal static class TransferValidationOptionsExtensions
	{
		public static StorageChecksumAlgorithm ResolveAuto(this StorageChecksumAlgorithm checksumAlgorithm)
		{
			if (checksumAlgorithm == StorageChecksumAlgorithm.Auto)
			{
				return StorageChecksumAlgorithm.StorageCrc64;
			}
			return checksumAlgorithm;
		}

		public static UploadTransferValidationOptions ToValidationOptions(this byte[] md5)
		{
			if (md5 != null)
			{
				return new UploadTransferValidationOptions
				{
					ChecksumAlgorithm = StorageChecksumAlgorithm.MD5,
					PrecalculatedChecksum = md5
				};
			}
			return null;
		}

		public static DownloadTransferValidationOptions ToValidationOptions(this bool requestTransactionalMD5)
		{
			if (!requestTransactionalMD5)
			{
				return null;
			}
			return new DownloadTransferValidationOptions
			{
				ChecksumAlgorithm = StorageChecksumAlgorithm.MD5,
				AutoValidateChecksum = false
			};
		}

		public static void CopyTo(this TransferValidationOptions source, TransferValidationOptions dest)
		{
			source.Upload.CopyTo(dest.Upload);
			source.Download.CopyTo(dest.Download);
		}

		public static void CopyTo(this UploadTransferValidationOptions source, UploadTransferValidationOptions dest)
		{
			dest.ChecksumAlgorithm = source.ChecksumAlgorithm;
			dest.PrecalculatedChecksum = source.PrecalculatedChecksum;
		}

		public static void CopyTo(this DownloadTransferValidationOptions source, DownloadTransferValidationOptions dest)
		{
			dest.ChecksumAlgorithm = source.ChecksumAlgorithm;
			dest.AutoValidateChecksum = source.AutoValidateChecksum;
		}
	}
}
namespace Azure.Storage.Shared
{
	internal interface ISupportsTenantIdChallenges
	{
		bool EnableTenantDiscovery { get; }
	}
}
namespace Azure.Storage.Sas
{
	public class AccountSasBuilder
	{
		private static readonly List<char> s_validPermissionsInOrder = new List<char>
		{
			'r', 'w', 'd', 'x', 'y', 'l', 'a', 'c', 'u', 'p',
			't', 'f', 'i'
		};

		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Version { get; set; }

		public SasProtocol Protocol { get; set; }

		public DateTimeOffset StartsOn { get; set; }

		public DateTimeOffset ExpiresOn { get; set; }

		public string Permissions { get; private set; }

		public SasIPRange IPRange { get; set; }

		public AccountSasServices Services { get; set; }

		public AccountSasResourceTypes ResourceTypes { get; set; }

		public string EncryptionScope { get; set; }

		[EditorBrowsable(EditorBrowsableState.Never)]
		public AccountSasBuilder()
		{
		}

		public AccountSasBuilder(AccountSasPermissions permissions, DateTimeOffset expiresOn, AccountSasServices services, AccountSasResourceTypes resourceTypes)
		{
			ExpiresOn = expiresOn;
			SetPermissions(permissions);
			Services = services;
			ResourceTypes = resourceTypes;
		}

		public void SetPermissions(AccountSasPermissions permissions)
		{
			Permissions = permissions.ToPermissionsString();
		}

		public void SetPermissions(string rawPermissions)
		{
			Permissions = SasExtensions.ValidateAndSanitizeRawPermissions(rawPermissions, s_validPermissionsInOrder);
		}

		[Azure.Core.CallerShouldAudit(Reason = "https://aka.ms/azsdk/callershouldaudit/storage-common")]
		public SasQueryParameters ToSasQueryParameters(StorageSharedKeyCredential sharedKeyCredential)
		{
			sharedKeyCredential = sharedKeyCredential ?? throw Errors.ArgumentNull("sharedKeyCredential");
			if (ExpiresOn == default(DateTimeOffset) || string.IsNullOrEmpty(Permissions) || ResourceTypes == (AccountSasResourceTypes)0 || Services == (AccountSasServices)0)
			{
				throw Errors.AccountSasMissingData();
			}
			Version = SasQueryParametersInternals.DefaultSasVersionInternal;
			string text = SasExtensions.FormatTimesForSasSigning(StartsOn);
			string text2 = SasExtensions.FormatTimesForSasSigning(ExpiresOn);
			string message = string.Join("\n", sharedKeyCredential.AccountName, Permissions, Services.ToPermissionsString(), ResourceTypes.ToPermissionsString(), text, text2, IPRange.ToString(), Protocol.ToProtocolString(), Version, EncryptionScope, string.Empty);
			string signature = sharedKeyCredential.ComputeHMACSHA256(message);
			string version = Version;
			AccountSasServices? services = Services;
			AccountSasResourceTypes? resourceTypes = ResourceTypes;
			SasProtocol protocol = Protocol;
			DateTimeOffset startsOn = StartsOn;
			DateTimeOffset expiresOn = ExpiresOn;
			SasIPRange iPRange = IPRange;
			string permissions = Permissions;
			string encryptionScope = EncryptionScope;
			return SasQueryParametersInternals.Create(version, services, resourceTypes, protocol, startsOn, expiresOn, iPRange, null, null, permissions, signature, null, null, null, null, null, null, null, null, null, encryptionScope);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
	[Flags]
	public enum AccountSasPermissions
	{
		Read = 1,
		Write = 2,
		Delete = 4,
		List = 8,
		Add = 0x10,
		Create = 0x20,
		Update = 0x40,
		Process = 0x80,
		Tag = 0x100,
		Filter = 0x200,
		DeleteVersion = 0x400,
		SetImmutabilityPolicy = 0x800,
		PermanentDelete = 0x1000,
		All = -1
	}
	[Flags]
	public enum AccountSasResourceTypes
	{
		Service = 1,
		Container = 2,
		Object = 4,
		All = -1
	}
	[Flags]
	public enum AccountSasServices
	{
		Blobs = 1,
		Queues = 2,
		Files = 4,
		Tables = 8,
		All = -1
	}
	public readonly struct SasIPRange : IEquatable<SasIPRange>
	{
		public IPAddress Start { get; }

		public IPAddress End { get; }

		public SasIPRange(IPAddress start, IPAddress end = null)
		{
			Start = start ?? IPAddress.None;
			End = end ?? IPAddress.None;
		}

		private static bool IsEmpty(IPAddress address)
		{
			if (address != null)
			{
				return address == IPAddress.None;
			}
			return true;
		}

		public override string ToString()
		{
			if (!IsEmpty(Start))
			{
				if (!IsEmpty(End))
				{
					return Start.ToString() + "-" + End.ToString();
				}
				return Start.ToString();
			}
			return string.Empty;
		}

		public static SasIPRange Parse(string s)
		{
			int num = s.IndexOf('-');
			if (num != -1)
			{
				return new SasIPRange(IPAddress.Parse(s.Substring(0, num)), IPAddress.Parse(s.Substring(num + 1)));
			}
			return new SasIPRange(IPAddress.Parse(s));
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			if (obj is SasIPRange other)
			{
				return Equals(other);
			}
			return false;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return (Start?.GetHashCode() ?? 0) ^ (End?.GetHashCode() ?? 0);
		}

		public static bool operator ==(SasIPRange left, SasIPRange right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(SasIPRange left, SasIPRange right)
		{
			return !(left == right);
		}

		public bool Equals(SasIPRange other)
		{
			if ((IsEmpty(Start) && IsEmpty(other.Start)) || (Start != null && Start.Equals(other.Start)))
			{
				if (!IsEmpty(End) || !IsEmpty(other.End))
				{
					if (End != null)
					{
						return End.Equals(other.End);
					}
					return false;
				}
				return true;
			}
			return false;
		}
	}
	public enum SasProtocol
	{
		None,
		HttpsAndHttp,
		Https
	}
	public class SasQueryParameters
	{
		public const string DefaultSasVersion = "2023-11-03";

		private string _version;

		private (AccountSasServices? Parsed, string Raw) _services;

		private (AccountSasResourceTypes? Parsed, string Raw) _resourceTypes;

		private SasProtocol _protocol;

		private DateTimeOffset _startTime;

		private string _startTimeString;

		private DateTimeOffset _expiryTime;

		private string _expiryTimeString;

		private SasIPRange _ipRange;

		private string _identifier;

		private string _resource;

		private string _permissions;

		private string _signature;

		private string _preauthorizedAgentObjectId;

		private string _agentObjectId;

		private string _correlationId;

		private int? _directoryDepth;

		private string _encryptionScope;

		private string _cacheControl;

		private string _contentDisposition;

		private string _contentEncoding;

		private string _contentLanguage;

		private string _contentType;

		private static readonly string[] s_sasTimeFormats = new string[4] { "yyyy-MM-ddTHH:mm:ssZ", "yyyy-MM-ddTHH:mm:ss.fffffffZ", "yyyy-MM-ddTHH:mmZ", "yyyy-MM-dd" };

		public string Version => _version ?? SasQueryParametersInternals.DefaultSasVersionInternal;

		public AccountSasServices? Services => _services.Parsed;

		public AccountSasResourceTypes? ResourceTypes => _resourceTypes.Parsed;

		public SasProtocol Protocol => _protocol;

		public DateTimeOffset StartsOn => _startTime;

		internal string StartsOnString => _startTimeString;

		public DateTimeOffset ExpiresOn => _expiryTime;

		internal string ExpiresOnString => _expiryTimeString;

		public SasIPRange IPRange => _ipRange;

		public string Identifier => _identifier ?? string.Empty;

		public string Resource => _resource ?? string.Empty;

		public string Permissions => _permissions ?? string.Empty;

		public string CacheControl => _cacheControl ?? string.Empty;

		public string ContentDisposition => _contentDisposition ?? string.Empty;

		public string ContentEncoding => _contentEncoding ?? string.Empty;

		public string ContentLanguage => _contentLanguage ?? string.Empty;

		public string ContentType => _contentType ?? string.Empty;

		public string PreauthorizedAgentObjectId => _preauthorizedAgentObjectId ?? string.Empty;

		public string AgentObjectId => _agentObjectId ?? string.Empty;

		public string CorrelationId => _correlationId ?? string.Empty;

		public int? DirectoryDepth => _directoryDepth ?? ((int?)null);

		public string EncryptionScope => _encryptionScope ?? string.Empty;

		public string Signature => _signature ?? string.Empty;

		public static SasQueryParameters Empty => new SasQueryParameters();

		protected SasQueryParameters()
		{
		}

		protected SasQueryParameters(IDictionary<string, string> values)
		{
			foreach (KeyValuePair<string, string> item in (IEnumerable<KeyValuePair<string, string>>)values.ToArray())
			{
				bool flag = true;
				switch (item.Key.ToUpperInvariant())
				{
				case "SV":
					_version = item.Value;
					break;
				case "SS":
					_services = (Parsed: SasExtensions.ParseAccountServices(item.Value), Raw: item.Value);
					break;
				case "SRT":
					_resourceTypes = (Parsed: SasExtensions.ParseResourceTypes(item.Value), Raw: item.Value);
					break;
				case "SPR":
					_protocol = SasExtensions.ParseProtocol(item.Value);
					break;
				case "ST":
					_startTimeString = item.Value;
					_startTime = ParseSasTime(item.Value);
					break;
				case "SE":
					_expiryTimeString = item.Value;
					_expiryTime = ParseSasTime(item.Value);
					break;
				case "SIP":
					_ipRange = SasIPRange.Parse(item.Value);
					break;
				case "SI":
					_identifier = item.Value;
					break;
				case "SR":
					_resource = item.Value;
					break;
				case "SP":
					_permissions = item.Value;
					break;
				case "SIG":
					_signature = item.Value;
					break;
				case "RSCC":
					_cacheControl = item.Value;
					break;
				case "RSCD":
					_contentDisposition = item.Value;
					break;
				case "RSCE":
					_contentEncoding = item.Value;
					break;
				case "RSCL":
					_contentLanguage = item.Value;
					break;
				case "RSCT":
					_contentType = item.Value;
					break;
				case "SAOID":
					_preauthorizedAgentObjectId = item.Value;
					break;
				case "SUOID":
					_agentObjectId = item.Value;
					break;
				case "SCID":
					_correlationId = item.Value;
					break;
				case "SDD":
					_directoryDepth = Convert.ToInt32(item.Value, 16);
					break;
				case "SES":
					_encryptionScope = item.Value;
					break;
				default:
					flag = false;
					break;
				}
				if (flag)
				{
					values.Remove(item.Key);
				}
			}
		}

		protected SasQueryParameters(string version, AccountSasServices? services, AccountSasResourceTypes? resourceTypes, SasProtocol protocol, DateTimeOffset startsOn, DateTimeOffset expiresOn, SasIPRange ipRange, string identifier, string resource, string permissions, string signature, string cacheControl = null, string contentDisposition = null, string contentEncoding = null, string contentLanguage = null, string contentType = null, string authorizedAadObjectId = null, string unauthorizedAadObjectId = null, string correlationId = null, int? directoryDepth = null, string encryptionScope = null)
		{
			_version = version;
			_services = (Parsed: services, Raw: services?.ToPermissionsString());
			_resourceTypes = (Parsed: resourceTypes, Raw: resourceTypes?.ToPermissionsString());
			_protocol = protocol;
			_startTime = startsOn;
			_startTimeString = startsOn.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
			_expiryTime = expiresOn;
			_expiryTimeString = expiresOn.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
			_ipRange = ipRange;
			_identifier = identifier;
			_resource = resource;
			_permissions = permissions;
			_signature = signature;
			_cacheControl = cacheControl;
			_contentDisposition = contentDisposition;
			_contentEncoding = contentEncoding;
			_contentLanguage = contentLanguage;
			_contentType = contentType;
			_preauthorizedAgentObjectId = authorizedAadObjectId;
			_agentObjectId = unauthorizedAadObjectId;
			_correlationId = correlationId;
			_directoryDepth = directoryDepth;
			_encryptionScope = encryptionScope;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		protected SasQueryParameters(string version, AccountSasServices? services, AccountSasResourceTypes? resourceTypes, SasProtocol protocol, DateTimeOffset startsOn, DateTimeOffset expiresOn, SasIPRange ipRange, string identifier, string resource, string permissions, string signature, string cacheControl = null, string contentDisposition = null, string contentEncoding = null, string contentLanguage = null, string contentType = null)
		{
			_version = version;
			_services = (Parsed: services, Raw: services?.ToPermissionsString());
			_resourceTypes = (Parsed: resourceTypes, Raw: resourceTypes?.ToPermissionsString());
			_protocol = protocol;
			_startTime = startsOn;
			_startTimeString = startsOn.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
			_expiryTime = expiresOn;
			_expiryTimeString = expiresOn.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
			_ipRange = ipRange;
			_identifier = identifier;
			_resource = resource;
			_permissions = permissions;
			_signature = signature;
			_cacheControl = cacheControl;
			_contentDisposition = contentDisposition;
			_contentEncoding = contentEncoding;
			_contentLanguage = contentLanguage;
			_contentType = contentType;
			_preauthorizedAgentObjectId = null;
			_agentObjectId = null;
			_correlationId = null;
			_directoryDepth = null;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		protected SasQueryParameters(string version, AccountSasServices? services, AccountSasResourceTypes? resourceTypes, SasProtocol protocol, DateTimeOffset startsOn, DateTimeOffset expiresOn, SasIPRange ipRange, string identifier, string resource, string permissions, string signature, string cacheControl = null, string contentDisposition = null, string contentEncoding = null, string contentLanguage = null, string contentType = null, string authorizedAadObjectId = null, string unauthorizedAadObjectId = null, string correlationId = null, int? directoryDepth = null)
		{
			_version = version;
			_services = (Parsed: services, Raw: services?.ToPermissionsString());
			_resourceTypes = (Parsed: resourceTypes, Raw: resourceTypes?.ToPermissionsString());
			_protocol = protocol;
			_startTime = startsOn;
			_startTimeString = startsOn.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
			_expiryTime = expiresOn;
			_expiryTimeString = expiresOn.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
			_ipRange = ipRange;
			_identifier = identifier;
			_resource = resource;
			_permissions = permissions;
			_signature = signature;
			_cacheControl = cacheControl;
			_contentDisposition = contentDisposition;
			_contentEncoding = contentEncoding;
			_contentLanguage = contentLanguage;
			_contentType = contentType;
			_preauthorizedAgentObjectId = authorizedAadObjectId;
			_agentObjectId = unauthorizedAadObjectId;
			_correlationId = correlationId;
			_directoryDepth = directoryDepth;
		}

		protected static SasQueryParameters Create(IDictionary<string, string> values)
		{
			return new SasQueryParameters(values);
		}

		protected static SasQueryParameters Create(string version, AccountSasServices? services, AccountSasResourceTypes? resourceTypes, SasProtocol protocol, DateTimeOffset startsOn, DateTimeOffset expiresOn, SasIPRange ipRange, string identifier, string resource, string permissions, string signature, string cacheControl = null, string contentDisposition = null, string contentEncoding = null, string contentLanguage = null, string contentType = null, string authorizedAadObjectId = null, string unauthorizedAadObjectId = null, string correlationId = null, int? directoryDepth = null, string encryptionScope = null)
		{
			return new SasQueryParameters(version, services, resourceTypes, protocol, startsOn, expiresOn, ipRange, identifier, resource, permissions, signature, cacheControl, contentDisposition, contentEncoding, contentLanguage, contentType, authorizedAadObjectId, unauthorizedAadObjectId, correlationId, directoryDepth, encryptionScope);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		protected static SasQueryParameters Create(string version, AccountSasServices? services, AccountSasResourceTypes? resourceTypes, SasProtocol protocol, DateTimeOffset startsOn, DateTimeOffset expiresOn, SasIPRange ipRange, string identifier, string resource, string permissions, string signature, string cacheControl = null, string contentDisposition = null, string contentEncoding = null, string contentLanguage = null, string contentType = null)
		{
			return new SasQueryParameters(version, services, resourceTypes, protocol, startsOn, expiresOn, ipRange, identifier, resource, permissions, signature, cacheControl, contentDisposition, contentEncoding, contentLanguage, contentType);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		protected static SasQueryParameters Create(string version, AccountSasServices? services, AccountSasResourceTypes? resourceTypes, SasProtocol protocol, DateTimeOffset startsOn, DateTimeOffset expiresOn, SasIPRange ipRange, string identifier, string resource, string permissions, string signature, string cacheControl = null, string contentDisposition = null, string contentEncoding = null, string contentLanguage = null, string contentType = null, string authorizedAadObjectId = null, string unauthorizedAadObjectId = null, string correlationId = null, int? directoryDepth = null)
		{
			return new SasQueryParameters(version, services, resourceTypes, protocol, startsOn, expiresOn, ipRange, identifier, resource, permissions, signature, cacheControl, contentDisposition, contentEncoding, contentLanguage, contentType, authorizedAadObjectId, unauthorizedAadObjectId, correlationId, directoryDepth);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			AppendProperties(stringBuilder);
			return stringBuilder.ToString();
		}

		protected internal void AppendProperties(StringBuilder stringBuilder)
		{
			if (!string.IsNullOrWhiteSpace(Version))
			{
				stringBuilder.AppendQueryParameter("sv", Version);
			}
			if (Services.HasValue)
			{
				stringBuilder.AppendQueryParameter("ss", _services.Raw);
			}
			if (ResourceTypes.HasValue)
			{
				stringBuilder.AppendQueryParameter("srt", _resourceTypes.Raw);
			}
			if (Protocol != SasProtocol.None)
			{
				stringBuilder.AppendQueryParameter("spr", Protocol.ToProtocolString());
			}
			if (StartsOn != DateTimeOffset.MinValue)
			{
				stringBuilder.AppendQueryParameter("st", WebUtility.UrlEncode(StartsOnString));
			}
			if (ExpiresOn != DateTimeOffset.MinValue)
			{
				stringBuilder.AppendQueryParameter("se", WebUtility.UrlEncode(ExpiresOnString));
			}
			string text = IPRange.ToString();
			if (text.Length > 0)
			{
				stringBuilder.AppendQueryParameter("sip", text);
			}
			if (!string.IsNullOrWhiteSpace(Identifier))
			{
				stringBuilder.AppendQueryParameter("si", Identifier);
			}
			if (!string.IsNullOrWhiteSpace(Resource))
			{
				stringBuilder.AppendQueryParameter("sr", Resource);
			}
			if (!string.IsNullOrWhiteSpace(Permissions))
			{
				stringBuilder.AppendQueryParameter("sp", Permissions);
			}
			if (!string.IsNullOrWhiteSpace(CacheControl))
			{
				stringBuilder.AppendQueryParameter("rscc", WebUtility.UrlEncode(CacheControl));
			}
			if (!string.IsNullOrWhiteSpace(ContentDisposition))
			{
				stringBuilder.AppendQueryParameter("rscd", WebUtility.UrlEncode(ContentDisposition));
			}
			if (!string.IsNullOrWhiteSpace(ContentEncoding))
			{
				stringBuilder.AppendQueryParameter("rsce", WebUtility.UrlEncode(ContentEncoding));
			}
			if (!string.IsNullOrWhiteSpace(ContentLanguage))
			{
				stringBuilder.AppendQueryParameter("rscl", WebUtility.UrlEncode(ContentLanguage));
			}
			if (!string.IsNullOrWhiteSpace(ContentType))
			{
				stringBuilder.AppendQueryParameter("rsct", WebUtility.UrlEncode(ContentType));
			}
			if (!string.IsNullOrWhiteSpace(PreauthorizedAgentObjectId))
			{
				stringBuilder.AppendQueryParameter("saoid", WebUtility.UrlEncode(PreauthorizedAgentObjectId));
			}
			if (!string.IsNullOrWhiteSpace(AgentObjectId))
			{
				stringBuilder.AppendQueryParameter("suoid", WebUtility.UrlEncode(AgentObjectId));
			}
			if (!string.IsNullOrWhiteSpace(CorrelationId))
			{
				stringBuilder.AppendQueryParameter("scid", WebUtility.UrlEncode(CorrelationId));
			}
			if (DirectoryDepth.HasValue)
			{
				stringBuilder.AppendQueryParameter("sdd", WebUtility.UrlEncode(DirectoryDepth.ToString()));
			}
			if (!string.IsNullOrWhiteSpace(EncryptionScope))
			{
				stringBuilder.AppendQueryParameter("ses", WebUtility.UrlEncode(EncryptionScope));
			}
			if (!string.IsNullOrWhiteSpace(Signature))
			{
				stringBuilder.AppendQueryParameter("sig", WebUtility.UrlEncode(Signature));
			}
		}

		private static DateTimeOffset ParseSasTime(string dateTimeString)
		{
			if (string.IsNullOrEmpty(dateTimeString))
			{
				return DateTimeOffset.MinValue;
			}
			return DateTimeOffset.ParseExact(dateTimeString, s_sasTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
		}
	}
	internal static class SasExtensions
	{
		private const string NoneName = null;

		private const string HttpsName = "https";

		private const string HttpsAndHttpName = "https,http";

		internal static string ToPermissionsString(this AccountSasResourceTypes resourceTypes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if ((resourceTypes & AccountSasResourceTypes.Service) == AccountSasResourceTypes.Service)
			{
				stringBuilder.Append('s');
			}
			if ((resourceTypes & AccountSasResourceTypes.Container) == AccountSasResourceTypes.Container)
			{
				stringBuilder.Append('c');
			}
			if ((resourceTypes & AccountSasResourceTypes.Object) == AccountSasResourceTypes.Object)
			{
				stringBuilder.Append('o');
			}
			return stringBuilder.ToString();
		}

		internal static AccountSasResourceTypes ParseResourceTypes(string s)
		{
			AccountSasResourceTypes accountSasResourceTypes = (AccountSasResourceTypes)0;
			foreach (char c in s)
			{
				AccountSasResourceTypes accountSasResourceTypes2 = accountSasResourceTypes;
				accountSasResourceTypes = (AccountSasResourceTypes)((int)accountSasResourceTypes2 | (c switch
				{
					's' => 1, 
					'c' => 2, 
					'o' => 4, 
					_ => throw Errors.InvalidResourceType(c), 
				}));
			}
			return accountSasResourceTypes;
		}

		internal static string ToProtocolString(this SasProtocol protocol)
		{
			return protocol switch
			{
				SasProtocol.Https => "https", 
				SasProtocol.HttpsAndHttp => "https,http", 
				_ => null, 
			};
		}

		public static SasProtocol ParseProtocol(string s)
		{
			if (s != null && (s == null || s.Length != 0))
			{
				if (!(s == "https"))
				{
					if (s == "https,http")
					{
						return SasProtocol.HttpsAndHttp;
					}
					throw Errors.InvalidSasProtocol("s", "SasProtocol");
				}
				return SasProtocol.Https;
			}
			return SasProtocol.None;
		}

		internal static string ToPermissionsString(this AccountSasServices services)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if ((services & AccountSasServices.Blobs) == AccountSasServices.Blobs)
			{
				stringBuilder.Append('b');
			}
			if ((services & AccountSasServices.Files) == AccountSasServices.Files)
			{
				stringBuilder.Append('f');
			}
			if ((services & AccountSasServices.Queues) == AccountSasServices.Queues)
			{
				stringBuilder.Append('q');
			}
			if ((services & AccountSasServices.Tables) == AccountSasServices.Tables)
			{
				stringBuilder.Append('t');
			}
			return stringBuilder.ToString();
		}

		internal static AccountSasServices ParseAccountServices(string s)
		{
			AccountSasServices accountSasServices = (AccountSasServices)0;
			foreach (char c in s)
			{
				AccountSasServices accountSasServices2 = accountSasServices;
				accountSasServices = (AccountSasServices)((int)accountSasServices2 | (c switch
				{
					'b' => 1, 
					'q' => 2, 
					'f' => 4, 
					't' => 8, 
					_ => throw Errors.InvalidService(c), 
				}));
			}
			return accountSasServices;
		}

		internal static string FormatTimesForSasSigning(DateTimeOffset time)
		{
			if (!(time == default(DateTimeOffset)))
			{
				return time.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
			}
			return "";
		}

		internal static void AddToBuilder(StringBuilder sb, string key, string value)
		{
			sb.Append((sb.Length > 0) ? "&" : "").Append(key).Append('=')
				.Append(value);
		}

		internal static string ValidateAndSanitizeRawPermissions(string permissions, List<char> validPermissionsInOrder)
		{
			if (permissions == null)
			{
				return null;
			}
			permissions = permissions.ToLowerInvariant();
			HashSet<char> hashSet = new HashSet<char>(validPermissionsInOrder);
			HashSet<char> hashSet2 = new HashSet<char>();
			string text = permissions;
			foreach (char c in text)
			{
				if (!hashSet.Contains(c))
				{
					throw new ArgumentException($"{c} is not a valid SAS permission");
				}
				hashSet2.Add(c);
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char item in validPermissionsInOrder)
			{
				if (hashSet2.Contains(item))
				{
					stringBuilder.Append(item);
				}
			}
			return stringBuilder.ToString();
		}
	}
	internal class SasQueryParametersInternals : SasQueryParameters
	{
		internal static string DefaultSasVersionInternal { get; set; } = "2023-11-03";

		internal new static SasQueryParameters Create(IDictionary<string, string> values)
		{
			return SasQueryParameters.Create(values);
		}

		internal new static SasQueryParameters Create(string version, AccountSasServices? services, AccountSasResourceTypes? resourceTypes, SasProtocol protocol, DateTimeOffset startsOn, DateTimeOffset expiresOn, SasIPRange ipRange, string identifier, string resource, string permissions, string signature, string cacheControl = null, string contentDisposition = null, string contentEncoding = null, string contentLanguage = null, string contentType = null, string authorizedAadObjectId = null, string unauthorizedAadObjectId = null, string correlationId = null, int? directoryDepth = null, string encryptionScope = null)
		{
			return SasQueryParameters.Create(version, services, resourceTypes, protocol, startsOn, expiresOn, ipRange, identifier, resource, permissions, signature, cacheControl, contentDisposition, contentEncoding, contentLanguage, contentType, authorizedAadObjectId, unauthorizedAadObjectId, correlationId, directoryDepth, encryptionScope);
		}
	}
}
