#define TRACE
using System;
using System.Buffers;
using System.ClientModel.Primitives;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Buffers;
using Azure.Core.Diagnostics;
using Azure.Core.Json;
using Azure.Core.JsonPatch;
using Azure.Core.Pipeline;
using Azure.Core.Serialization;
using Azure.Core.Shared;
using Microsoft.CodeAnalysis;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: InternalsVisibleTo("Azure.Core.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100d15ddcb29688295338af4b7686603fe614abd555e09efba8fb88ee09e1f7b1ccaeed2e8f823fa9eef3fdd60217fc012ea67d2479751a0b8c087a4185541b851bd8b16f8d91b840e51b1cb0ba6fe647997e57429265e85ef62d565db50a69ae1647d54d7bd855e4db3d8a91510e5bcbd0edfbbecaa20a7bd9ae74593daa7b11b4")]
[assembly: InternalsVisibleTo("Azure.Core.Perf, PublicKey=0024000004800000940000000602000000240000525341310004000001000100d15ddcb29688295338af4b7686603fe614abd555e09efba8fb88ee09e1f7b1ccaeed2e8f823fa9eef3fdd60217fc012ea67d2479751a0b8c087a4185541b851bd8b16f8d91b840e51b1cb0ba6fe647997e57429265e85ef62d565db50a69ae1647d54d7bd855e4db3d8a91510e5bcbd0edfbbecaa20a7bd9ae74593daa7b11b4")]
[assembly: TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = ".NET Standard 2.0")]
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("© Microsoft Corporation. All rights reserved.")]
[assembly: AssemblyDescription("This is the implementation of the Azure Client Pipeline")]
[assembly: AssemblyFileVersion("1.4000.24.30605")]
[assembly: AssemblyInformationalVersion("1.40.0+242d8afc16f3a4b18e7ecb8381049d61f7099242")]
[assembly: AssemblyProduct("Azure .NET SDK")]
[assembly: AssemblyTitle("Microsoft Azure Client Pipeline")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/Azure/azure-sdk-for-net")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("1.40.0.0")]
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
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	internal sealed class ScopedRefAttribute : Attribute
	{
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
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	internal sealed class ExperimentalAttribute : Attribute
	{
		public string DiagnosticId { get; }

		public string? UrlFormat { get; set; }

		public ExperimentalAttribute(string diagnosticId)
		{
			DiagnosticId = diagnosticId;
		}
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	internal sealed class AllowNullAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	internal sealed class DisallowNullAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	internal sealed class MaybeNullAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	internal sealed class NotNullAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		public bool ReturnValue { get; }

		public MaybeNullWhenAttribute(bool returnValue)
		{
			ReturnValue = returnValue;
		}
	}
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		public bool ReturnValue { get; }

		public NotNullWhenAttribute(bool returnValue)
		{
			ReturnValue = returnValue;
		}
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	internal sealed class NotNullIfNotNullAttribute : Attribute
	{
		public string ParameterName { get; }

		public NotNullIfNotNullAttribute(string parameterName)
		{
			ParameterName = parameterName;
		}
	}
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	internal sealed class DoesNotReturnAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		public bool ParameterValue { get; }

		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			ParameterValue = parameterValue;
		}
	}
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
	public abstract class AsyncPageable<T> : IAsyncEnumerable<T> where T : notnull
	{
		private class StaticPageable : AsyncPageable<T>
		{
			private readonly IEnumerable<Page<T>> _pages;

			public StaticPageable(IEnumerable<Page<T>> pages)
			{
				_pages = pages;
			}

			public override async IAsyncEnumerable<Page<T>> AsPages(string? continuationToken = null, int? pageSizeHint = null)
			{
				bool shouldReturnPages = continuationToken == null;
				foreach (Page<T> page in _pages)
				{
					if (shouldReturnPages)
					{
						yield return page;
					}
					else if (continuationToken == page.ContinuationToken)
					{
						shouldReturnPages = true;
					}
				}
			}
		}

		protected virtual CancellationToken CancellationToken { get; }

		protected AsyncPageable()
		{
			CancellationToken = CancellationToken.None;
		}

		protected AsyncPageable(CancellationToken cancellationToken)
		{
			CancellationToken = cancellationToken;
		}

		public abstract IAsyncEnumerable<Page<T>> AsPages(string? continuationToken = null, int? pageSizeHint = null);

		public virtual async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
		{
			ConfiguredCancelableAsyncEnumerable<Page<T>>.Enumerator asyncEnumerator = AsPages().ConfigureAwait(continueOnCapturedContext: false).WithCancellation(cancellationToken).GetAsyncEnumerator();
			object obj = null;
			int num = 0;
			try
			{
				while (await asyncEnumerator.MoveNextAsync())
				{
					Page<T> current = asyncEnumerator.Current;
					foreach (T value in current.Values)
					{
						yield return value;
					}
				}
			}
			catch (object obj2)
			{
				obj = obj2;
			}
			await asyncEnumerator.DisposeAsync();
			object obj3 = obj;
			if (obj3 != null)
			{
				ExceptionDispatchInfo.Capture((obj3 as Exception) ?? throw obj3).Throw();
			}
			_ = num;
		}

		public static AsyncPageable<T> FromPages(IEnumerable<Page<T>> pages)
		{
			return new StaticPageable(pages);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string? ToString()
		{
			return base.ToString();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? obj)
		{
			return base.Equals(obj);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
	public class AzureKeyCredential
	{
		private string _key;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Key
		{
			get
			{
				return Volatile.Read(ref _key);
			}
			private set
			{
				Volatile.Write(ref _key, value);
			}
		}

		public AzureKeyCredential(string key)
		{
			Update(key);
		}

		public void Update(string key)
		{
			Argument.AssertNotNullOrEmpty(key, "key");
			Key = key;
		}
	}
	public class AzureNamedKeyCredential
	{
		private Tuple<string, string> _namedKey;

		public string Name => Volatile.Read(ref _namedKey).Item1;

		public AzureNamedKeyCredential(string name, string key)
		{
			Update(name, key);
		}

		public void Update(string name, string key)
		{
			Argument.AssertNotNullOrEmpty(name, "name");
			Argument.AssertNotNullOrEmpty(key, "key");
			Volatile.Write(ref _namedKey, Tuple.Create(name, key));
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Deconstruct(out string name, out string key)
		{
			Tuple<string, string> tuple = Volatile.Read(ref _namedKey);
			name = tuple.Item1;
			key = tuple.Item2;
		}
	}
	public class AzureSasCredential
	{
		private string _signature;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Signature
		{
			get
			{
				return Volatile.Read(ref _signature);
			}
			private set
			{
				Volatile.Write(ref _signature, value);
			}
		}

		public AzureSasCredential(string signature)
		{
			Argument.AssertNotNullOrWhiteSpace(signature, "signature");
			Signature = signature;
		}

		public void Update(string signature)
		{
			Argument.AssertNotNullOrWhiteSpace(signature, "signature");
			Signature = signature;
		}
	}
	[Flags]
	public enum ErrorOptions
	{
		Default = 0,
		NoThrow = 1
	}
	[JsonConverter(typeof(ETagConverter))]
	public readonly struct ETag : IEquatable<ETag>
	{
		private const char QuoteCharacter = '"';

		private const string QuoteString = "\"";

		private const string WeakETagPrefix = "W/\"";

		private const string DefaultFormat = "G";

		private const string HeaderFormat = "H";

		private readonly string? _value;

		public static readonly ETag All = new ETag("*");

		public ETag(string etag)
		{
			_value = etag;
		}

		public static bool operator ==(ETag left, ETag right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ETag left, ETag right)
		{
			return !left.Equals(right);
		}

		public bool Equals(ETag other)
		{
			return string.Equals(_value, other._value, StringComparison.Ordinal);
		}

		public bool Equals(string? other)
		{
			return string.Equals(_value, other, StringComparison.Ordinal);
		}

		public override bool Equals(object? obj)
		{
			if (obj is ETag other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return _value.GetHashCodeOrdinal();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return ToString("G");
		}

		public string ToString(string format)
		{
			if (_value == null)
			{
				return string.Empty;
			}
			if (!(format == "H"))
			{
				if (format == "G")
				{
					return _value;
				}
				throw new ArgumentException("Invalid format string.");
			}
			return (!IsValidQuotedFormat(_value)) ? ("\"" + _value + "\"") : _value;
		}

		internal static ETag Parse(string value)
		{
			if (value == All._value)
			{
				return All;
			}
			if (!IsValidQuotedFormat(value))
			{
				throw new ArgumentException("The value should be equal to * , be wrapped in quotes, or be wrapped in quotes prefixed by W/", "value");
			}
			if (value.StartsWith("W/\"", StringComparison.Ordinal))
			{
				return new ETag(value);
			}
			return new ETag(value.Trim(new char[1] { '"' }));
		}

		private static bool IsValidQuotedFormat(string value)
		{
			if ((!value.StartsWith("\"", StringComparison.Ordinal) && !value.StartsWith("W/\"", StringComparison.Ordinal)) || !value.EndsWith("\"", StringComparison.Ordinal))
			{
				return value == All._value;
			}
			return true;
		}
	}
	internal class ETagConverter : JsonConverter<ETag>
	{
		public override ETag Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			string text = reader.GetString();
			if (text == null)
			{
				return default(ETag);
			}
			return new ETag(text);
		}

		public override void Write(Utf8JsonWriter writer, ETag value, JsonSerializerOptions options)
		{
			if (value == default(ETag))
			{
				writer.WriteNullValue();
			}
			else
			{
				writer.WriteStringValue(value.ToString("H"));
			}
		}
	}
	public class HttpAuthorization
	{
		public string Scheme { get; }

		public string Parameter { get; }

		public HttpAuthorization(string scheme, string parameter)
		{
			Argument.AssertNotNullOrWhiteSpace(scheme, "scheme");
			Argument.AssertNotNullOrWhiteSpace(parameter, "parameter");
			Scheme = scheme;
			Parameter = parameter;
		}

		public override string ToString()
		{
			return Scheme + " " + Parameter;
		}
	}
	public readonly struct HttpRange : IEquatable<HttpRange>
	{
		private const string Unit = "bytes";

		public long Offset { get; }

		public long? Length { get; }

		public HttpRange(long offset = 0L, long? length = null)
		{
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (length.HasValue && length <= 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			Offset = offset;
			Length = length;
		}

		public override string ToString()
		{
			if (Length.HasValue && Length != 0)
			{
				long num = Offset + Length.Value - 1;
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}={1}-{2}", "bytes", Offset, num));
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}={1}-", "bytes", Offset));
		}

		public static bool operator ==(HttpRange left, HttpRange right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(HttpRange left, HttpRange right)
		{
			return !(left == right);
		}

		public bool Equals(HttpRange other)
		{
			if (Offset == other.Offset)
			{
				return Length == other.Length;
			}
			return false;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? obj)
		{
			if (obj is HttpRange other)
			{
				return Equals(other);
			}
			return false;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return HashCodeBuilder.Combine(Offset, Length);
		}
	}
	internal class ResponseDebugView<T>
	{
		private readonly Response<T> _response;

		public Response GetRawResponse => _response.GetRawResponse();

		public T Value => _response.Value;

		public ResponseDebugView(Response<T> response)
		{
			_response = response;
		}
	}
	internal class ValueResponse<T> : Response<T>
	{
		private readonly Response _response;

		public override T Value { get; }

		public ValueResponse(Response response, T value)
		{
			_response = response;
			Value = value;
		}

		public override Response GetRawResponse()
		{
			return _response;
		}
	}
	public class JsonPatchDocument
	{
		private readonly ReadOnlyMemory<byte> _rawDocument;

		private readonly ObjectSerializer _serializer;

		private readonly Collection<JsonPatchOperation> _operations;

		public JsonPatchDocument()
			: this(default(ReadOnlyMemory<byte>))
		{
		}

		public JsonPatchDocument(ObjectSerializer serializer)
			: this(default(ReadOnlyMemory<byte>), serializer)
		{
		}

		public JsonPatchDocument(ReadOnlyMemory<byte> rawDocument)
			: this(rawDocument, new JsonObjectSerializer())
		{
		}

		public JsonPatchDocument(ReadOnlyMemory<byte> rawDocument, ObjectSerializer serializer)
		{
			_operations = new Collection<JsonPatchOperation>();
			_rawDocument = rawDocument;
			_serializer = serializer ?? throw new ArgumentNullException("serializer");
		}

		public void AppendAddRaw(string path, string rawJsonValue)
		{
			_operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Add, path, null, rawJsonValue));
		}

		public void AppendAdd<T>(string path, T value)
		{
			_operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Add, path, null, Serialize(value)));
		}

		public void AppendReplaceRaw(string path, string rawJsonValue)
		{
			_operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Replace, path, null, rawJsonValue));
		}

		public void AppendReplace<T>(string path, T value)
		{
			_operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Replace, path, null, Serialize(value)));
		}

		public void AppendCopy(string from, string path)
		{
			_operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Copy, path, from, null));
		}

		public void AppendMove(string from, string path)
		{
			_operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Move, path, from, null));
		}

		public void AppendRemove(string path)
		{
			_operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Remove, path, null, null));
		}

		public void AppendTestRaw(string path, string rawJsonValue)
		{
			_operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Test, path, null, rawJsonValue));
		}

		public void AppendTest<T>(string path, T value)
		{
			_operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Test, path, null, Serialize(value)));
		}

		public ReadOnlyMemory<byte> ToBytes()
		{
			if (!_rawDocument.IsEmpty && _operations.Count == 0)
			{
				return _rawDocument;
			}
			using MemoryStream memoryStream = new MemoryStream();
			using (Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream))
			{
				WriteTo(writer);
			}
			return MemoryExtensions.AsMemory(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
		}

		public override string ToString()
		{
			return Encoding.UTF8.GetString(ToBytes().ToArray());
		}

		private void WriteTo(Utf8JsonWriter writer)
		{
			writer.WriteStartArray();
			if (!_rawDocument.IsEmpty)
			{
				using JsonDocument jsonDocument = JsonDocument.Parse(_rawDocument);
				foreach (JsonElement item in jsonDocument.RootElement.EnumerateArray())
				{
					item.WriteTo(writer);
				}
			}
			foreach (JsonPatchOperation operation in _operations)
			{
				writer.WriteStartObject();
				writer.WriteString("op", operation.Kind.ToString());
				if (operation.From != null)
				{
					writer.WriteString("from", operation.From);
				}
				writer.WriteString("path", operation.Path);
				if (operation.RawJsonValue != null)
				{
					using JsonDocument jsonDocument2 = JsonDocument.Parse(operation.RawJsonValue);
					writer.WritePropertyName("value");
					jsonDocument2.WriteTo(writer);
				}
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
		}

		private string Serialize<T>(T value)
		{
			using MemoryStream memoryStream = new MemoryStream();
			_serializer.Serialize(memoryStream, value, typeof(T), default(CancellationToken));
			return Encoding.UTF8.GetString(memoryStream.ToArray());
		}
	}
	public class MatchConditions
	{
		public ETag? IfMatch { get; set; }

		public ETag? IfNoneMatch { get; set; }
	}
	public abstract class NullableResponse<T>
	{
		private const string NoValue = "<null>";

		public abstract bool HasValue { get; }

		public abstract T? Value { get; }

		public abstract Response GetRawResponse();

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? obj)
		{
			return base.Equals(obj);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("Status: {0}, Value: {1}", GetRawResponse()?.Status, HasValue ? ((object)Value) : "<null>");
		}
	}
	public abstract class Operation
	{
		public abstract string Id { get; }

		public abstract bool HasCompleted { get; }

		public static Operation<T> Rehydrate<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] T>(HttpPipeline pipeline, RehydrationToken rehydrationToken, ClientOptions? options = null) where T : IPersistableModel<T>
		{
			Argument.AssertNotNull(pipeline, "pipeline");
			Argument.AssertNotNull(rehydrationToken, "rehydrationToken");
			GenericOperationSource<T> operationSource = new GenericOperationSource<T>();
			NextLinkOperationImplementation nextLinkOperationImplementation = (NextLinkOperationImplementation)NextLinkOperationImplementation.Create(pipeline, rehydrationToken);
			IOperation<T> operation = NextLinkOperationImplementation.Create(operationSource, nextLinkOperationImplementation);
			OperationState<T> operationState = operation.UpdateStateAsync(async: false, default(CancellationToken)).EnsureCompleted();
			return new RehydrationOperation<T>(nextLinkOperationImplementation, operationState, operation, options);
		}

		public static Operation Rehydrate(HttpPipeline pipeline, RehydrationToken rehydrationToken, ClientOptions? options = null)
		{
			Argument.AssertNotNull(pipeline, "pipeline");
			Argument.AssertNotNull(rehydrationToken, "rehydrationToken");
			NextLinkOperationImplementation obj = (NextLinkOperationImplementation)NextLinkOperationImplementation.Create(pipeline, rehydrationToken);
			OperationState operationState = obj.UpdateStateAsync(async: false, default(CancellationToken)).EnsureCompleted();
			return new RehydrationOperation(obj, operationState);
		}

		public static async Task<Operation<T>> RehydrateAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] T>(HttpPipeline pipeline, RehydrationToken rehydrationToken, ClientOptions? options = null) where T : IPersistableModel<T>
		{
			Argument.AssertNotNull(pipeline, "pipeline");
			Argument.AssertNotNull(rehydrationToken, "rehydrationToken");
			IOperationSource<T> operationSource = new GenericOperationSource<T>();
			NextLinkOperationImplementation nextLinkOperation = (NextLinkOperationImplementation)NextLinkOperationImplementation.Create(pipeline, rehydrationToken);
			IOperation<T> operation = NextLinkOperationImplementation.Create(operationSource, nextLinkOperation);
			return new RehydrationOperation<T>(nextLinkOperation, await operation.UpdateStateAsync(async: true, default(CancellationToken)).ConfigureAwait(continueOnCapturedContext: false), operation, options);
		}

		public static async Task<Operation> RehydrateAsync(HttpPipeline pipeline, RehydrationToken rehydrationToken, ClientOptions? options = null)
		{
			Argument.AssertNotNull(pipeline, "pipeline");
			Argument.AssertNotNull(rehydrationToken, "rehydrationToken");
			NextLinkOperationImplementation nextLinkOperation = (NextLinkOperationImplementation)NextLinkOperationImplementation.Create(pipeline, rehydrationToken);
			return new RehydrationOperation(nextLinkOperation, await nextLinkOperation.UpdateStateAsync(async: true, default(CancellationToken)).ConfigureAwait(continueOnCapturedContext: false));
		}

		public virtual RehydrationToken? GetRehydrationToken()
		{
			return null;
		}

		public abstract Response GetRawResponse();

		public abstract ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default(CancellationToken));

		public abstract Response UpdateStatus(CancellationToken cancellationToken = default(CancellationToken));

		public virtual async ValueTask<Response> WaitForCompletionResponseAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return await new OperationPoller().WaitForCompletionResponseAsync(this, null, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public virtual async ValueTask<Response> WaitForCompletionResponseAsync(TimeSpan pollingInterval, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await new OperationPoller().WaitForCompletionResponseAsync(this, pollingInterval, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public virtual async ValueTask<Response> WaitForCompletionResponseAsync(DelayStrategy delayStrategy, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await new OperationPoller(delayStrategy).WaitForCompletionResponseAsync(this, null, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public virtual Response WaitForCompletionResponse(CancellationToken cancellationToken = default(CancellationToken))
		{
			return new OperationPoller().WaitForCompletionResponse(this, null, cancellationToken);
		}

		public virtual Response WaitForCompletionResponse(TimeSpan pollingInterval, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new OperationPoller().WaitForCompletionResponse(this, pollingInterval, cancellationToken);
		}

		public virtual Response WaitForCompletionResponse(DelayStrategy delayStrategy, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new OperationPoller(delayStrategy).WaitForCompletionResponse(this, null, cancellationToken);
		}

		internal static T GetValue<T>(ref T? value) where T : class
		{
			if (value == null)
			{
				throw new InvalidOperationException("The operation has not completed yet.");
			}
			return value;
		}

		internal static T GetValue<T>(ref T? value) where T : struct
		{
			if (!value.HasValue)
			{
				throw new InvalidOperationException("The operation has not completed yet.");
			}
			return value.Value;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? obj)
		{
			return base.Equals(obj);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string? ToString()
		{
			return base.ToString();
		}
	}
	public abstract class Operation<T> : Operation where T : notnull
	{
		public abstract T Value { get; }

		public abstract bool HasValue { get; }

		public virtual Response<T> WaitForCompletion(CancellationToken cancellationToken = default(CancellationToken))
		{
			return new OperationPoller().WaitForCompletion(this, null, cancellationToken);
		}

		public virtual Response<T> WaitForCompletion(TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			return new OperationPoller().WaitForCompletion(this, pollingInterval, cancellationToken);
		}

		public virtual async ValueTask<Response<T>> WaitForCompletionAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return await new OperationPoller().WaitForCompletionAsync(this, null, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public virtual async ValueTask<Response<T>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			return await new OperationPoller().WaitForCompletionAsync(this, pollingInterval, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public virtual Response<T> WaitForCompletion(DelayStrategy delayStrategy, CancellationToken cancellationToken)
		{
			return new OperationPoller(delayStrategy).WaitForCompletion(this, null, cancellationToken);
		}

		public virtual async ValueTask<Response<T>> WaitForCompletionAsync(DelayStrategy delayStrategy, CancellationToken cancellationToken)
		{
			return await new OperationPoller(delayStrategy).WaitForCompletionAsync(this, null, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override async ValueTask<Response> WaitForCompletionResponseAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return (await WaitForCompletionAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).GetRawResponse();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override async ValueTask<Response> WaitForCompletionResponseAsync(TimeSpan pollingInterval, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (await WaitForCompletionAsync(pollingInterval, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).GetRawResponse();
		}
	}
	public abstract class Page<T>
	{
		private class PageCore : Page<T>
		{
			private readonly Response _response;

			public override IReadOnlyList<T> Values { get; }

			public override string? ContinuationToken { get; }

			public PageCore(IReadOnlyList<T> values, string? continuationToken, Response response)
			{
				_response = response;
				Values = values;
				ContinuationToken = continuationToken;
			}

			public override Response GetRawResponse()
			{
				return _response;
			}
		}

		public abstract IReadOnlyList<T> Values { get; }

		public abstract string? ContinuationToken { get; }

		public abstract Response GetRawResponse();

		public static Page<T> FromValues(IReadOnlyList<T> values, string? continuationToken, Response response)
		{
			return new PageCore(values, continuationToken, response);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string? ToString()
		{
			return base.ToString();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? obj)
		{
			return base.Equals(obj);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
	public abstract class Pageable<T> : IEnumerable<T>, IEnumerable where T : notnull
	{
		private class StaticPageable : Pageable<T>
		{
			private readonly IEnumerable<Page<T>> _pages;

			public StaticPageable(IEnumerable<Page<T>> pages)
			{
				_pages = pages;
			}

			public override IEnumerable<Page<T>> AsPages(string? continuationToken = null, int? pageSizeHint = null)
			{
				bool shouldReturnPages = continuationToken == null;
				foreach (Page<T> page in _pages)
				{
					if (shouldReturnPages)
					{
						yield return page;
					}
					else if (continuationToken == page.ContinuationToken)
					{
						shouldReturnPages = true;
					}
				}
			}
		}

		protected virtual CancellationToken CancellationToken { get; }

		protected Pageable()
		{
			CancellationToken = CancellationToken.None;
		}

		protected Pageable(CancellationToken cancellationToken)
		{
			CancellationToken = cancellationToken;
		}

		public abstract IEnumerable<Page<T>> AsPages(string? continuationToken = null, int? pageSizeHint = null);

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string? ToString()
		{
			return base.ToString();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public virtual IEnumerator<T> GetEnumerator()
		{
			foreach (Page<T> item in AsPages())
			{
				foreach (T value in item.Values)
				{
					yield return value;
				}
			}
		}

		public static Pageable<T> FromPages(IEnumerable<Page<T>> pages)
		{
			return new StaticPageable(pages);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? obj)
		{
			return base.Equals(obj);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
	public abstract class PageableOperation<T> : Operation<AsyncPageable<T>> where T : notnull
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override AsyncPageable<T> Value => GetValuesAsync();

		public abstract AsyncPageable<T> GetValuesAsync(CancellationToken cancellationToken = default(CancellationToken));

		public abstract Pageable<T> GetValues(CancellationToken cancellationToken = default(CancellationToken));
	}
	public class RequestConditions : MatchConditions
	{
		public DateTimeOffset? IfModifiedSince { get; set; }

		public DateTimeOffset? IfUnmodifiedSince { get; set; }
	}
	public class RequestContext
	{
		private bool _frozen;

		private (int Status, bool IsError)[]? _statusCodes;

		private ResponseClassificationHandler[]? _handlers;

		internal (int Status, bool IsError)[]? StatusCodes => _statusCodes;

		internal ResponseClassificationHandler[]? Handlers => _handlers;

		internal List<(HttpPipelinePosition Position, HttpPipelinePolicy Policy)>? Policies { get; private set; }

		public ErrorOptions ErrorOptions { get; set; }

		public CancellationToken CancellationToken { get; set; } = CancellationToken.None;

		public static implicit operator RequestContext(ErrorOptions options)
		{
			return new RequestContext
			{
				ErrorOptions = options
			};
		}

		public void AddPolicy(HttpPipelinePolicy policy, HttpPipelinePosition position)
		{
			if (Policies == null)
			{
				List<(HttpPipelinePosition, HttpPipelinePolicy)> list = (Policies = new List<(HttpPipelinePosition, HttpPipelinePolicy)>());
			}
			Policies.Add((position, policy));
		}

		public void AddClassifier(int statusCode, bool isError)
		{
			Argument.AssertInRange(statusCode, 100, 599, "statusCode");
			if (_frozen)
			{
				throw new InvalidOperationException("Cannot modify classifiers after this type has been used in a method call.");
			}
			int num = ((_statusCodes != null) ? _statusCodes.Length : 0);
			Array.Resize(ref _statusCodes, num + 1);
			Array.Copy(_statusCodes, 0, _statusCodes, 1, num);
			_statusCodes[0] = (Status: statusCode, IsError: isError);
		}

		public void AddClassifier(ResponseClassificationHandler classifier)
		{
			if (_frozen)
			{
				throw new InvalidOperationException("Cannot modify classifiers after this type has been used in a method call.");
			}
			int num = ((_handlers != null) ? _handlers.Length : 0);
			Array.Resize(ref _handlers, num + 1);
			Array.Copy(_handlers, 0, _handlers, 1, num);
			_handlers[0] = classifier;
		}

		internal void Freeze()
		{
			_frozen = true;
		}

		internal ResponseClassifier Apply(ResponseClassifier classifier)
		{
			if (_statusCodes == null && _handlers == null)
			{
				return classifier;
			}
			if (classifier is StatusCodeClassifier statusCodeClassifier)
			{
				StatusCodeClassifier statusCodeClassifier2 = statusCodeClassifier.Clone();
				statusCodeClassifier2.Handlers = _handlers;
				if (_statusCodes != null)
				{
					(int, bool)[] statusCodes = _statusCodes;
					for (int i = 0; i < statusCodes.Length; i++)
					{
						(int, bool) tuple = statusCodes[i];
						statusCodeClassifier2.AddClassifier(tuple.Item1, tuple.Item2);
					}
				}
				return statusCodeClassifier2;
			}
			return new ChainingClassifier(_statusCodes, _handlers, classifier);
		}
	}
	[Serializable]
	public class RequestFailedException : Exception, ISerializable
	{
		internal class ErrorResponse
		{
			[JsonPropertyName("error")]
			public ResponseError? Error { get; set; }
		}

		private readonly struct ErrorDetails
		{
			public string Message { get; }

			public string? ErrorCode { get; }

			public IDictionary<string, string>? Data { get; }

			public ErrorDetails(string message, string? errorCode, IDictionary<string, string>? data)
			{
				Message = message;
				ErrorCode = errorCode;
				Data = data;
			}
		}

		private const string DefaultMessage = "Service request failed.";

		internal const string NoContentOnSuccessMessage = "Service request succeeded. Response content and headers are not included to avoid logging sensitive data.";

		private readonly Response? _response;

		public int Status { get; }

		public string? ErrorCode { get; }

		public RequestFailedException(string message)
			: this(0, message)
		{
		}

		public RequestFailedException(string message, Exception? innerException)
			: this(0, message, innerException)
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public RequestFailedException(int status, string message)
			: this(status, message, null)
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public RequestFailedException(int status, string message, Exception? innerException)
			: this(status, message, null, innerException)
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public RequestFailedException(int status, string message, string? errorCode, Exception? innerException)
			: base(message, innerException)
		{
			Status = status;
			ErrorCode = errorCode;
		}

		private RequestFailedException(int status, (string Message, ResponseError? Error) details)
			: this(status, details.Message, details.Error?.Code, null)
		{
		}

		private RequestFailedException(int status, ErrorDetails details, Exception? innerException)
			: this(status, details.Message, details.ErrorCode, innerException)
		{
			if (details.Data == null)
			{
				return;
			}
			foreach (KeyValuePair<string, string> datum in details.Data)
			{
				Data.Add(datum.Key, datum.Value);
			}
		}

		public RequestFailedException(Response response)
			: this(response, null)
		{
		}

		public RequestFailedException(Response response, Exception? innerException)
			: this(response, innerException, null)
		{
		}

		public RequestFailedException(Response response, Exception? innerException, RequestFailedDetailsParser? detailsParser)
			: this(response.Status, CreateExceptionDetails(response, detailsParser), innerException)
		{
			_response = response;
		}

		protected RequestFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			Status = info.GetInt32("Status");
			ErrorCode = info.GetString("ErrorCode");
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			Argument.AssertNotNull(info, "info");
			info.AddValue("Status", Status);
			info.AddValue("ErrorCode", ErrorCode);
			base.GetObjectData(info, context);
		}

		public Response? GetRawResponse()
		{
			return _response;
		}

		private static ErrorDetails CreateExceptionDetails(Response response, RequestFailedDetailsParser? parser)
		{
			BufferResponseIfNeeded(response);
			if (parser == null)
			{
				parser = response.RequestFailedDetailsParser;
			}
			if (!(parser?.TryParse(response, out ResponseError error, out IDictionary<string, string> data) ?? DefaultRequestFailedDetailsParser.TryParseDetails(response, out error, out data)))
			{
				error = null;
				data = null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			AppendStatusAndReason(response, error, stringBuilder);
			AppendErrorCodeAndAdditionalInfo(error, data, stringBuilder);
			if (response.IsError)
			{
				AppendContentAndHeaders(response, stringBuilder);
			}
			else
			{
				stringBuilder.AppendLine().AppendLine("Service request succeeded. Response content and headers are not included to avoid logging sensitive data.");
			}
			return new ErrorDetails(stringBuilder.ToString(), error?.Code, data);
		}

		private static void AppendContentAndHeaders(Response response, StringBuilder messageBuilder)
		{
			if (response.ContentStream is MemoryStream && ContentTypeUtilities.TryGetTextEncoding(response.Headers.ContentType, out var _))
			{
				messageBuilder.AppendLine().AppendLine("Content:").AppendLine(response.Content.ToString());
			}
			messageBuilder.AppendLine().AppendLine("Headers:");
			foreach (HttpHeader header in response.Headers)
			{
				string text = response.Sanitizer.SanitizeHeader(header.Name, header.Value);
				string value = header.Name + ": " + text;
				messageBuilder.AppendLine(value);
			}
		}

		private static void AppendErrorCodeAndAdditionalInfo(ResponseError? error, IDictionary<string, string>? additionalInfo, StringBuilder messageBuilder)
		{
			if (!string.IsNullOrWhiteSpace(error?.Code))
			{
				messageBuilder.Append("ErrorCode: ").Append(error?.Code).AppendLine();
			}
			if (additionalInfo == null || additionalInfo.Count <= 0)
			{
				return;
			}
			messageBuilder.AppendLine().AppendLine("Additional Information:");
			foreach (KeyValuePair<string, string> item in additionalInfo)
			{
				messageBuilder.Append(item.Key).Append(": ").AppendLine(item.Value);
			}
		}

		private static void AppendStatusAndReason(Response response, ResponseError? error, StringBuilder messageBuilder)
		{
			messageBuilder.AppendLine(error?.Message ?? "Service request failed.").Append("Status: ").Append(response.Status.ToString(CultureInfo.InvariantCulture));
			if (!string.IsNullOrEmpty(response.ReasonPhrase))
			{
				messageBuilder.Append(" (").Append(response.ReasonPhrase).AppendLine(")");
			}
			else
			{
				messageBuilder.AppendLine();
			}
		}

		private static void BufferResponseIfNeeded(Response response)
		{
			Stream contentStream = response.ContentStream;
			if ((contentStream != null && !(contentStream is MemoryStream)) || 1 == 0)
			{
				MemoryStream memoryStream = new MemoryStream();
				response.ContentStream.CopyTo(memoryStream);
				response.ContentStream.Dispose();
				memoryStream.Position = 0L;
				response.ContentStream = memoryStream;
			}
		}
	}
	public abstract class Response : IDisposable
	{
		private static readonly BinaryData s_EmptyBinaryData = new BinaryData(Array.Empty<byte>());

		public abstract int Status { get; }

		public abstract string ReasonPhrase { get; }

		public abstract Stream? ContentStream { get; set; }

		public abstract string ClientRequestId { get; set; }

		public virtual ResponseHeaders Headers => new ResponseHeaders(this);

		public virtual BinaryData Content
		{
			get
			{
				if (ContentStream == null)
				{
					return s_EmptyBinaryData;
				}
				if (!(ContentStream is MemoryStream memoryStream))
				{
					throw new InvalidOperationException("The response is not fully buffered.");
				}
				if (memoryStream.TryGetBuffer(out var buffer))
				{
					return new BinaryData(buffer.AsMemory());
				}
				return new BinaryData(memoryStream.ToArray());
			}
		}

		public virtual bool IsError { get; internal set; }

		internal HttpMessageSanitizer Sanitizer { get; set; } = HttpMessageSanitizer.Default;

		internal RequestFailedDetailsParser? RequestFailedDetailsParser { get; set; }

		public abstract void Dispose();

		protected internal abstract bool TryGetHeader(string name, [NotNullWhen(true)] out string? value);

		protected internal abstract bool TryGetHeaderValues(string name, [NotNullWhen(true)] out IEnumerable<string>? values);

		protected internal abstract bool ContainsHeader(string name);

		protected internal abstract IEnumerable<HttpHeader> EnumerateHeaders();

		public static Response<T> FromValue<T>(T value, Response response)
		{
			return new ValueResponse<T>(response, value);
		}

		public override string ToString()
		{
			return $"Status: {Status}, ReasonPhrase: {ReasonPhrase}";
		}

		internal static void DisposeStreamIfNotBuffered(ref Stream? stream)
		{
			if (!(stream is MemoryStream))
			{
				stream?.Dispose();
				stream = null;
			}
		}
	}
	[JsonConverter(typeof(Converter))]
	[TypeReferenceType(true, new string[] { "Target", "Details" })]
	public sealed class ResponseError
	{
		internal class Converter : JsonConverter<ResponseError?>
		{
			public override ResponseError? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);
				return Read(jsonDocument.RootElement);
			}

			private static ResponseError? Read(JsonElement element)
			{
				if (element.ValueKind == JsonValueKind.Null)
				{
					return null;
				}
				string code = null;
				if (element.TryGetProperty("code", out var value))
				{
					code = value.GetString();
				}
				string message = null;
				if (element.TryGetProperty("message", out value))
				{
					message = value.GetString();
				}
				string target = null;
				if (element.TryGetProperty("target", out value))
				{
					target = value.GetString();
				}
				ResponseInnerError innerError = null;
				if (element.TryGetProperty("innererror", out value))
				{
					innerError = ResponseInnerError.Converter.Read(value);
				}
				List<ResponseError> list = null;
				if (element.TryGetProperty("details", out value) && value.ValueKind == JsonValueKind.Array)
				{
					foreach (JsonElement item in value.EnumerateArray())
					{
						ResponseError responseError = Read(item);
						if (responseError != null)
						{
							if (list == null)
							{
								list = new List<ResponseError>();
							}
							list.Add(responseError);
						}
					}
				}
				return new ResponseError(code, message, target, element.Clone(), innerError, list);
			}

			public override void Write(Utf8JsonWriter writer, ResponseError? value, JsonSerializerOptions options)
			{
				throw new NotImplementedException();
			}
		}

		private readonly JsonElement _element;

		public string? Code { get; }

		public string? Message { get; }

		internal ResponseInnerError? InnerError { get; }

		internal string? Target { get; }

		internal IReadOnlyList<ResponseError> Details { get; }

		[InitializationConstructor]
		public ResponseError(string? code, string? message)
			: this(code, message, null, default(JsonElement))
		{
		}

		[SerializationConstructor]
		internal ResponseError(string? code, string? message, string? target, JsonElement element, ResponseInnerError? innerError = null, IReadOnlyList<ResponseError>? details = null)
		{
			_element = element;
			Code = code;
			Message = message;
			InnerError = innerError;
			Target = target;
			Details = details ?? Array.Empty<ResponseError>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Append(stringBuilder, includeRaw: true);
			return stringBuilder.ToString();
		}

		internal void Append(StringBuilder builder, bool includeRaw)
		{
			builder.AppendFormat(CultureInfo.InvariantCulture, "{0}: {1}{2}", Code, Message, Environment.NewLine);
			if (Target != null)
			{
				builder.AppendFormat(CultureInfo.InvariantCulture, "Target: {0}{1}", Target, Environment.NewLine);
			}
			ResponseInnerError innerError = InnerError;
			if (innerError != null)
			{
				builder.AppendLine();
				builder.AppendLine("Inner Errors:");
				while (innerError != null)
				{
					builder.AppendLine(innerError.Code);
					innerError = innerError.InnerError;
				}
			}
			if (Details.Count > 0)
			{
				builder.AppendLine();
				builder.AppendLine("Details:");
				foreach (ResponseError detail in Details)
				{
					detail.Append(builder, includeRaw: false);
				}
			}
			if (includeRaw && _element.ValueKind != JsonValueKind.Undefined)
			{
				builder.AppendLine();
				builder.AppendLine("Raw:");
				builder.Append(_element.GetRawText());
			}
		}
	}
	[JsonConverter(typeof(Converter))]
	internal sealed class ResponseInnerError
	{
		internal class Converter : JsonConverter<ResponseInnerError?>
		{
			public override ResponseInnerError? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);
				return Read(jsonDocument.RootElement);
			}

			internal static ResponseInnerError? Read(JsonElement element)
			{
				if (element.ValueKind == JsonValueKind.Null)
				{
					return null;
				}
				string code = null;
				if (element.TryGetProperty("code", out var value))
				{
					code = value.GetString();
				}
				ResponseInnerError innerError = null;
				if (element.TryGetProperty("innererror", out value))
				{
					innerError = Read(value);
				}
				return new ResponseInnerError(code, innerError, element.Clone());
			}

			public override void Write(Utf8JsonWriter writer, ResponseInnerError? value, JsonSerializerOptions options)
			{
				throw new NotImplementedException();
			}
		}

		private readonly JsonElement _innerErrorElement;

		public string? Code { get; }

		public ResponseInnerError? InnerError { get; }

		internal ResponseInnerError(string? code, ResponseInnerError? innerError, JsonElement innerErrorElement)
		{
			_innerErrorElement = innerErrorElement;
			Code = code;
			InnerError = innerError;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Append(stringBuilder);
			return stringBuilder.ToString();
		}

		internal void Append(StringBuilder builder)
		{
			builder.AppendFormat(CultureInfo.InvariantCulture, "{0}: {1}", Code, Environment.NewLine);
			if (InnerError != null)
			{
				builder.AppendLine("Inner Error:");
				builder.Append(InnerError);
			}
		}
	}
	[DebuggerTypeProxy(typeof(ResponseDebugView<>))]
	public abstract class Response<T> : NullableResponse<T>
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HasValue => true;

		public override T Value => Value;

		public static implicit operator T(Response<T> response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response", $"The implicit cast from Response<{typeof(T)}> to {typeof(T)} failed because the Response<{typeof(T)}> was null.");
			}
			return response.Value;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? obj)
		{
			return base.Equals(obj);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
	public static class AzureCoreExtensions
	{
		public static T? ToObject<T>(this BinaryData data, ObjectSerializer serializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (T)serializer.Deserialize(data.ToStream(), typeof(T), cancellationToken);
		}

		public static async ValueTask<T?> ToObjectAsync<T>(this BinaryData data, ObjectSerializer serializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (T)(await serializer.DeserializeAsync(data.ToStream(), typeof(T), cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		public static object? ToObjectFromJson(this BinaryData data)
		{
			return data.ToObjectFromJson<JsonElement>().GetObject();
		}

		public static dynamic ToDynamicFromJson(this BinaryData utf8Json)
		{
			DynamicDataOptions options = new DynamicDataOptions();
			return utf8Json.ToDynamicFromJson(options);
		}

		public static dynamic ToDynamicFromJson(this BinaryData utf8Json, JsonPropertyNames propertyNameFormat, string dateTimeFormat = "o")
		{
			DynamicDataOptions options = new DynamicDataOptions
			{
				PropertyNameFormat = propertyNameFormat,
				DateTimeFormat = dateTimeFormat
			};
			return utf8Json.ToDynamicFromJson(options);
		}

		internal static dynamic ToDynamicFromJson(this BinaryData utf8Json, DynamicDataOptions options)
		{
			return new DynamicData(MutableJsonDocument.Parse(utf8Json, DynamicDataOptions.ToSerializerOptions(options)).RootElement, options);
		}

		private static object? GetObject(this in JsonElement element)
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
	}
	public class SyncAsyncEventArgs : EventArgs
	{
		public bool IsRunningSynchronously { get; }

		public CancellationToken CancellationToken { get; }

		public SyncAsyncEventArgs(bool isRunningSynchronously, CancellationToken cancellationToken = default(CancellationToken))
		{
			IsRunningSynchronously = isRunningSynchronously;
			CancellationToken = cancellationToken;
		}
	}
	public enum WaitUntil
	{
		Completed,
		Started
	}
}
namespace Azure.Messaging
{
	[JsonConverter(typeof(CloudEventConverter))]
	public class CloudEvent
	{
		private string? _id;

		private string? _source;

		private string? _type;

		public BinaryData? Data { get; set; }

		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				Argument.AssertNotNull(value, "value");
				_id = value;
			}
		}

		internal CloudEventDataFormat DataFormat { get; set; }

		public string Source
		{
			get
			{
				return _source;
			}
			set
			{
				Argument.AssertNotNull(value, "value");
				_source = value;
			}
		}

		public string Type
		{
			get
			{
				return _type;
			}
			set
			{
				Argument.AssertNotNull(value, "value");
				_type = value;
			}
		}

		internal string? SpecVersion { get; set; }

		public DateTimeOffset? Time { get; set; } = DateTimeOffset.UtcNow;

		public string? DataSchema { get; set; }

		public string? DataContentType { get; set; }

		internal Type? DataSerializationType { get; }

		public string? Subject { get; set; }

		public IDictionary<string, object> ExtensionAttributes { get; } = new CloudEventExtensionAttributes<string, object>();

		public CloudEvent(string source, string type, object? jsonSerializableData, Type? dataSerializationType = null)
		{
			if (jsonSerializableData is BinaryData)
			{
				throw new InvalidOperationException("This constructor does not support BinaryData. Use the constructor that takes a BinaryData instance.");
			}
			Source = source;
			Type = type;
			Id = Guid.NewGuid().ToString();
			DataFormat = CloudEventDataFormat.Json;
			Data = new BinaryData(jsonSerializableData, null, dataSerializationType ?? jsonSerializableData?.GetType());
			SpecVersion = "1.0";
		}

		public CloudEvent(string source, string type, BinaryData? data, string? dataContentType, CloudEventDataFormat dataFormat = CloudEventDataFormat.Binary)
		{
			Source = source;
			Type = type;
			DataContentType = dataContentType;
			Id = Guid.NewGuid().ToString();
			DataFormat = dataFormat;
			Data = data;
			SpecVersion = "1.0";
		}

		internal CloudEvent()
		{
		}

		public static CloudEvent[] ParseMany(BinaryData json, bool skipValidation = false)
		{
			Argument.AssertNotNull(json, "json");
			CloudEvent[] array = null;
			JsonDocument jsonDocument = JsonDocument.Parse(json);
			if (jsonDocument.RootElement.ValueKind == JsonValueKind.Object)
			{
				array = new CloudEvent[1] { CloudEventConverter.DeserializeCloudEvent(jsonDocument.RootElement, skipValidation) };
			}
			else if (jsonDocument.RootElement.ValueKind == JsonValueKind.Array)
			{
				array = new CloudEvent[jsonDocument.RootElement.GetArrayLength()];
				int num = 0;
				foreach (JsonElement item in jsonDocument.RootElement.EnumerateArray())
				{
					array[num++] = CloudEventConverter.DeserializeCloudEvent(item, skipValidation);
				}
			}
			return array ?? Array.Empty<CloudEvent>();
		}

		public static CloudEvent? Parse(BinaryData json, bool skipValidation = false)
		{
			Argument.AssertNotNull(json, "json");
			using JsonDocument jsonDocument = JsonDocument.Parse(json);
			CloudEvent result = null;
			if (jsonDocument.RootElement.ValueKind == JsonValueKind.Object)
			{
				result = CloudEventConverter.DeserializeCloudEvent(jsonDocument.RootElement, skipValidation);
			}
			else if (jsonDocument.RootElement.ValueKind == JsonValueKind.Array)
			{
				if (jsonDocument.RootElement.GetArrayLength() > 1)
				{
					throw new ArgumentException("The BinaryData instance contains JSON from multiple cloud events. This method should only be used with BinaryData containing a single cloud event. " + Environment.NewLine + "To parse multiple events, use the ParseMany overload.");
				}
				using JsonElement.ArrayEnumerator arrayEnumerator = jsonDocument.RootElement.EnumerateArray().GetEnumerator();
				if (arrayEnumerator.MoveNext())
				{
					result = CloudEventConverter.DeserializeCloudEvent(arrayEnumerator.Current, skipValidation);
				}
			}
			return result;
		}
	}
	internal class CloudEventConstants
	{
		public const string SpecVersion = "specversion";

		public const string Id = "id";

		public const string Source = "source";

		public const string Type = "type";

		public const string DataContentType = "datacontenttype";

		public const string DataSchema = "dataschema";

		public const string Subject = "subject";

		public const string Time = "time";

		public const string Data = "data";

		public const string DataBase64 = "data_base64";

		public const string ErrorSkipValidationSuggestion = "The `skipValidation` parameter can be set to 'true' in the CloudEvent.Parse or CloudEvent.ParseEvents method to skip this validation.";
	}
	internal class CloudEventConverter : JsonConverter<CloudEvent>
	{
		public override CloudEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);
			return DeserializeCloudEvent(jsonDocument.RootElement, skipValidation: false);
		}

		internal static CloudEvent DeserializeCloudEvent(JsonElement element, bool skipValidation)
		{
			CloudEvent cloudEvent = new CloudEvent();
			foreach (JsonProperty item in element.EnumerateObject())
			{
				if (item.NameEquals("id"))
				{
					if (item.Value.ValueKind != JsonValueKind.Null)
					{
						cloudEvent.Id = item.Value.GetString();
					}
				}
				else if (item.NameEquals("source"))
				{
					if (item.Value.ValueKind != JsonValueKind.Null)
					{
						cloudEvent.Source = item.Value.GetString();
					}
				}
				else if (item.NameEquals("data"))
				{
					cloudEvent.Data = new BinaryData(item.Value);
					cloudEvent.DataFormat = CloudEventDataFormat.Json;
				}
				else if (item.NameEquals("data_base64"))
				{
					if (item.Value.ValueKind != JsonValueKind.Null)
					{
						cloudEvent.Data = BinaryData.FromBytes(item.Value.GetBytesFromBase64());
						cloudEvent.DataFormat = CloudEventDataFormat.Binary;
					}
				}
				else if (item.NameEquals("type"))
				{
					if (item.Value.ValueKind != JsonValueKind.Null)
					{
						cloudEvent.Type = item.Value.GetString();
					}
				}
				else if (item.NameEquals("time"))
				{
					if (item.Value.ValueKind != JsonValueKind.Null)
					{
						cloudEvent.Time = item.Value.GetDateTimeOffset();
					}
				}
				else if (item.NameEquals("specversion"))
				{
					cloudEvent.SpecVersion = item.Value.GetString();
				}
				else if (item.NameEquals("dataschema"))
				{
					cloudEvent.DataSchema = item.Value.GetString();
				}
				else if (item.NameEquals("datacontenttype"))
				{
					cloudEvent.DataContentType = item.Value.GetString();
				}
				else if (item.NameEquals("subject"))
				{
					cloudEvent.Subject = item.Value.GetString();
				}
				else if (!skipValidation)
				{
					cloudEvent.ExtensionAttributes.Add(item.Name, GetObject(item.Value));
				}
				else
				{
					((CloudEventExtensionAttributes<string, object>)cloudEvent.ExtensionAttributes).AddWithoutValidation(item.Name, GetObject(item.Value));
				}
			}
			if (!skipValidation)
			{
				if (cloudEvent.Source == null)
				{
					throw new ArgumentException("The source property must be specified in each CloudEvent. " + Environment.NewLine + "The `skipValidation` parameter can be set to 'true' in the CloudEvent.Parse or CloudEvent.ParseEvents method to skip this validation.");
				}
				if (cloudEvent.Type == null)
				{
					throw new ArgumentException("The type property must be specified in each CloudEvent. " + Environment.NewLine + "The `skipValidation` parameter can be set to 'true' in the CloudEvent.Parse or CloudEvent.ParseEvents method to skip this validation.");
				}
				if (cloudEvent.Id == null)
				{
					throw new ArgumentException("The Id property must be specified in each CloudEvent. " + Environment.NewLine + "The `skipValidation` parameter can be set to 'true' in the CloudEvent.Parse or CloudEvent.ParseEvents method to skip this validation.");
				}
				if (cloudEvent.SpecVersion != "1.0")
				{
					if (cloudEvent.SpecVersion == null)
					{
						throw new ArgumentException("The specverion was not set in at least one of the events in the payload. This type only supports specversion '1.0', which must be set for each event. " + Environment.NewLine + "The `skipValidation` parameter can be set to 'true' in the CloudEvent.Parse or CloudEvent.ParseEvents method to skip this validation." + Environment.NewLine + element, "element");
					}
					throw new ArgumentException("The specverion value of '" + cloudEvent.SpecVersion + "' is not supported by CloudEvent. This type only supports specversion '1.0'. " + Environment.NewLine + "The `skipValidation` parameter can be set to 'true' in the CloudEvent.Parse or CloudEvent.ParseEvents method to skip this validation." + Environment.NewLine + element, "element");
				}
			}
			return cloudEvent;
		}

		public override void Write(Utf8JsonWriter writer, CloudEvent value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("id");
			writer.WriteStringValue(value.Id);
			writer.WritePropertyName("source");
			writer.WriteStringValue(value.Source);
			writer.WritePropertyName("type");
			writer.WriteStringValue(value.Type);
			if (value.Data != null)
			{
				switch (value.DataFormat)
				{
				case CloudEventDataFormat.Binary:
					writer.WritePropertyName("data_base64");
					writer.WriteBase64StringValue(value.Data.ToArray());
					break;
				case CloudEventDataFormat.Json:
				{
					using (JsonDocument jsonDocument = JsonDocument.Parse(value.Data.ToMemory()))
					{
						writer.WritePropertyName("data");
						jsonDocument.RootElement.WriteTo(writer);
					}
					break;
				}
				}
			}
			if (value.Time.HasValue)
			{
				writer.WritePropertyName("time");
				writer.WriteStringValue(value.Time.Value);
			}
			writer.WritePropertyName("specversion");
			writer.WriteStringValue(value.SpecVersion);
			if (value.DataSchema != null)
			{
				writer.WritePropertyName("dataschema");
				writer.WriteStringValue(value.DataSchema);
			}
			if (value.DataContentType != null)
			{
				writer.WritePropertyName("datacontenttype");
				writer.WriteStringValue(value.DataContentType);
			}
			if (value.Subject != null)
			{
				writer.WritePropertyName("subject");
				writer.WriteStringValue(value.Subject);
			}
			foreach (KeyValuePair<string, object> extensionAttribute in value.ExtensionAttributes)
			{
				writer.WritePropertyName(extensionAttribute.Key);
				WriteObjectValue(writer, extensionAttribute.Value);
			}
			writer.WriteEndObject();
		}

		private static void WriteObjectValue(Utf8JsonWriter writer, object? value)
		{
			if (value != null)
			{
				if (!(value is byte[] inArray))
				{
					if (!(value is ReadOnlyMemory<byte> readOnlyMemory))
					{
						if (!(value is int value2))
						{
							if (!(value is string value3))
							{
								if (!(value is bool value4))
								{
									if (!(value is Guid value5))
									{
										if (!(value is Uri uri))
										{
											if (!(value is DateTimeOffset value6))
											{
												if (!(value is DateTime value7))
												{
													if (!(value is IEnumerable<KeyValuePair<string, object>> enumerable))
													{
														if (value is IEnumerable<object> enumerable2)
														{
															writer.WriteStartArray();
															foreach (object item in enumerable2)
															{
																WriteObjectValue(writer, item);
															}
															writer.WriteEndArray();
															return;
														}
														throw new NotSupportedException("Not supported type " + value.GetType());
													}
													writer.WriteStartObject();
													foreach (KeyValuePair<string, object> item2 in enumerable)
													{
														writer.WritePropertyName(item2.Key);
														WriteObjectValue(writer, item2.Value);
													}
													writer.WriteEndObject();
												}
												else
												{
													writer.WriteStringValue(value7);
												}
											}
											else
											{
												writer.WriteStringValue(value6);
											}
										}
										else
										{
											writer.WriteStringValue(uri.ToString());
										}
									}
									else
									{
										writer.WriteStringValue(value5);
									}
								}
								else
								{
									writer.WriteBooleanValue(value4);
								}
							}
							else
							{
								writer.WriteStringValue(value3);
							}
						}
						else
						{
							writer.WriteNumberValue(value2);
						}
					}
					else
					{
						writer.WriteStringValue(Convert.ToBase64String(readOnlyMemory.ToArray()));
					}
				}
				else
				{
					writer.WriteStringValue(Convert.ToBase64String(inArray));
				}
			}
			else
			{
				writer.WriteNullValue();
			}
		}

		private static object? GetObject(in JsonElement element)
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
						dictionary.Add(item.Name, GetObject(item.Value));
					}
					return dictionary;
				}
			}
			case JsonValueKind.Array:
			{
				List<object> list = new List<object>();
				foreach (JsonElement item2 in element.EnumerateArray())
				{
					list.Add(GetObject(item2));
				}
				return list.ToArray();
			}
			default:
				throw new NotSupportedException("Not supported value kind " + element.ValueKind);
			}
		}
	}
	public enum CloudEventDataFormat
	{
		Binary,
		Json
	}
	internal class CloudEventExtensionAttributes<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable where TKey : class
	{
		private readonly Dictionary<TKey, TValue> _backingDictionary;

		private static readonly HashSet<string> s_reservedAttributes = new HashSet<string> { "specversion", "id", "source", "type", "datacontenttype", "dataschema", "subject", "time", "data", "data_base64" };

		public TValue this[TKey key]
		{
			get
			{
				return _backingDictionary[key];
			}
			set
			{
				ValidateAttribute(key as string, value);
				_backingDictionary[key] = value;
			}
		}

		public ICollection<TKey> Keys => _backingDictionary.Keys;

		public ICollection<TValue> Values => _backingDictionary.Values;

		public int Count => _backingDictionary.Count;

		public bool IsReadOnly => ((ICollection<KeyValuePair<TKey, TValue>>)_backingDictionary).IsReadOnly;

		public CloudEventExtensionAttributes()
		{
			_backingDictionary = new Dictionary<TKey, TValue>();
		}

		public void Add(TKey key, TValue value)
		{
			ValidateAttribute(key as string, value);
			_backingDictionary.Add(key, value);
		}

		public void AddWithoutValidation(TKey key, TValue value)
		{
			_backingDictionary.Add(key, value);
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			ValidateAttribute(item.Key as string, item.Value);
			((ICollection<KeyValuePair<TKey, TValue>>)_backingDictionary).Add(item);
		}

		public void Clear()
		{
			_backingDictionary.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)_backingDictionary).Contains(item);
		}

		public bool ContainsKey(TKey key)
		{
			return _backingDictionary.ContainsKey(key);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<TKey, TValue>>)_backingDictionary).CopyTo(array, arrayIndex);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return _backingDictionary.GetEnumerator();
		}

		public bool Remove(TKey key)
		{
			return _backingDictionary.Remove(key);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)_backingDictionary).Remove(item);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return _backingDictionary.TryGetValue(key, out value);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _backingDictionary.GetEnumerator();
		}

		private static void ValidateAttribute(string? name, object? value)
		{
			Argument.AssertNotNullOrEmpty(name, "name");
			Argument.AssertNotNull(value, "value");
			if (s_reservedAttributes.Contains(name))
			{
				throw new ArgumentException("Attribute name cannot use the reserved attribute: '" + name + "'", "name");
			}
			foreach (char c in name)
			{
				if ((c < '0' || c > '9') && (c < 'a' || c > 'z'))
				{
					throw new ArgumentException($"Invalid character in extension attribute name: '{c}'. " + "CloudEvent attribute names must consist of lower-case letters ('a' to 'z') or digits ('0' to '9') from the ASCII character set.", "name");
				}
			}
			if (value is string || value is byte[] || value is ReadOnlyMemory<byte> || value is int || value is bool || value is Uri || value is DateTime || value is DateTimeOffset)
			{
				return;
			}
			throw new ArgumentException($"Values of type {value.GetType()} are not supported. " + "Attribute values must be of type string, bool, byte, int, Uri, DateTime, or DateTimeOffset.");
		}
	}
	public class MessageContent
	{
		public virtual BinaryData? Data { get; set; }

		public virtual ContentType? ContentType
		{
			get
			{
				return ContentTypeCore;
			}
			set
			{
				ContentTypeCore = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual ContentType? ContentTypeCore { get; set; }

		public virtual bool IsReadOnly { get; }
	}
}
namespace Azure.Core
{
	public struct AccessToken
	{
		public string Token { get; }

		public DateTimeOffset ExpiresOn { get; }

		public DateTimeOffset? RefreshOn { get; }

		public AccessToken(string accessToken, DateTimeOffset expiresOn)
		{
			RefreshOn = null;
			Token = accessToken;
			ExpiresOn = expiresOn;
		}

		public AccessToken(string accessToken, DateTimeOffset expiresOn, DateTimeOffset? refreshOn)
		{
			Token = accessToken;
			ExpiresOn = expiresOn;
			RefreshOn = refreshOn;
		}

		public override bool Equals(object? obj)
		{
			if (obj is AccessToken accessToken)
			{
				if (accessToken.ExpiresOn == ExpiresOn)
				{
					return accessToken.Token == Token;
				}
				return false;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCodeBuilder.Combine(Token, ExpiresOn);
		}
	}
	public readonly struct AzureLocation : IEquatable<AzureLocation>
	{
		private const char Space = ' ';

		private static Dictionary<string, AzureLocation> PublicCloudLocations { get; } = new Dictionary<string, AzureLocation>();

		public static AzureLocation EastAsia { get; } = CreateStaticReference("eastasia", "East Asia");

		public static AzureLocation SoutheastAsia { get; } = CreateStaticReference("southeastasia", "Southeast Asia");

		public static AzureLocation CentralUS { get; } = CreateStaticReference("centralus", "Central US");

		public static AzureLocation EastUS { get; } = CreateStaticReference("eastus", "East US");

		public static AzureLocation EastUS2 { get; } = CreateStaticReference("eastus2", "East US 2");

		public static AzureLocation WestUS { get; } = CreateStaticReference("westus", "West US");

		public static AzureLocation WestUS2 { get; } = CreateStaticReference("westus2", "West US 2");

		public static AzureLocation WestUS3 { get; } = CreateStaticReference("westus3", "West US 3");

		public static AzureLocation NorthCentralUS { get; } = CreateStaticReference("northcentralus", "North Central US");

		public static AzureLocation SouthCentralUS { get; } = CreateStaticReference("southcentralus", "South Central US");

		public static AzureLocation NorthEurope { get; } = CreateStaticReference("northeurope", "North Europe");

		public static AzureLocation WestEurope { get; } = CreateStaticReference("westeurope", "West Europe");

		public static AzureLocation JapanWest { get; } = CreateStaticReference("japanwest", "Japan West");

		public static AzureLocation JapanEast { get; } = CreateStaticReference("japaneast", "Japan East");

		public static AzureLocation BrazilSouth { get; } = CreateStaticReference("brazilsouth", "Brazil South");

		public static AzureLocation AustraliaEast { get; } = CreateStaticReference("australiaeast", "Australia East");

		public static AzureLocation AustraliaSoutheast { get; } = CreateStaticReference("australiasoutheast", "Australia Southeast");

		public static AzureLocation SouthIndia { get; } = CreateStaticReference("southindia", "South India");

		public static AzureLocation CentralIndia { get; } = CreateStaticReference("centralindia", "Central India");

		public static AzureLocation WestIndia { get; } = CreateStaticReference("westindia", "West India");

		public static AzureLocation CanadaCentral { get; } = CreateStaticReference("canadacentral", "Canada Central");

		public static AzureLocation CanadaEast { get; } = CreateStaticReference("canadaeast", "Canada East");

		public static AzureLocation UKSouth { get; } = CreateStaticReference("uksouth", "UK South");

		public static AzureLocation UKWest { get; } = CreateStaticReference("ukwest", "UK West");

		public static AzureLocation WestCentralUS { get; } = CreateStaticReference("westcentralus", "West Central US");

		public static AzureLocation KoreaCentral { get; } = CreateStaticReference("koreacentral", "Korea Central");

		public static AzureLocation KoreaSouth { get; } = CreateStaticReference("koreasouth", "Korea South");

		public static AzureLocation FranceCentral { get; } = CreateStaticReference("francecentral", "France Central");

		public static AzureLocation FranceSouth { get; } = CreateStaticReference("francesouth", "France South");

		public static AzureLocation AustraliaCentral { get; } = CreateStaticReference("australiacentral", "Australia Central");

		public static AzureLocation AustraliaCentral2 { get; } = CreateStaticReference("australiacentral2", "Australia Central 2");

		public static AzureLocation UAECentral { get; } = CreateStaticReference("uaecentral", "UAE Central");

		public static AzureLocation UAENorth { get; } = CreateStaticReference("uaenorth", "UAE North");

		public static AzureLocation SouthAfricaNorth { get; } = CreateStaticReference("southafricanorth", "South Africa North");

		public static AzureLocation SouthAfricaWest { get; } = CreateStaticReference("southafricawest", "South Africa West");

		public static AzureLocation SwedenCentral { get; } = CreateStaticReference("swedencentral", "Sweden Central");

		public static AzureLocation SwedenSouth { get; } = CreateStaticReference("swedensouth", "Sweden South");

		public static AzureLocation SwitzerlandNorth { get; } = CreateStaticReference("switzerlandnorth", "Switzerland North");

		public static AzureLocation SwitzerlandWest { get; } = CreateStaticReference("switzerlandwest", "Switzerland West");

		public static AzureLocation GermanyNorth { get; } = CreateStaticReference("germanynorth", "Germany North");

		public static AzureLocation GermanyWestCentral { get; } = CreateStaticReference("germanywestcentral", "Germany West Central");

		public static AzureLocation GermanyCentral { get; } = CreateStaticReference("germanycentral", "Germany Central");

		public static AzureLocation GermanyNorthEast { get; } = CreateStaticReference("germanynortheast", "Germany Northeast");

		public static AzureLocation NorwayWest { get; } = CreateStaticReference("norwaywest", "Norway West");

		public static AzureLocation NorwayEast { get; } = CreateStaticReference("norwayeast", "Norway East");

		public static AzureLocation BrazilSoutheast { get; } = CreateStaticReference("brazilsoutheast", "Brazil Southeast");

		public static AzureLocation ChinaNorth { get; } = CreateStaticReference("chinanorth", "China North");

		public static AzureLocation ChinaEast { get; } = CreateStaticReference("chinaeast", "China East");

		public static AzureLocation ChinaNorth2 { get; } = CreateStaticReference("chinanorth2", "China North 2");

		public static AzureLocation ChinaNorth3 { get; } = CreateStaticReference("chinanorth3", "China North 3");

		public static AzureLocation ChinaEast2 { get; } = CreateStaticReference("chinaeast2", "China East 2");

		public static AzureLocation ChinaEast3 { get; } = CreateStaticReference("chinaeast3", "China East 3");

		public static AzureLocation QatarCentral { get; } = CreateStaticReference("qatarcentral", "Qatar Central");

		public static AzureLocation USDoDCentral { get; } = CreateStaticReference("usdodcentral", "US DoD Central");

		public static AzureLocation USDoDEast { get; } = CreateStaticReference("usdodeast", "US DoD East");

		public static AzureLocation USGovArizona { get; } = CreateStaticReference("usgovarizona", "US Gov Arizona");

		public static AzureLocation USGovTexas { get; } = CreateStaticReference("usgovtexas", "US Gov Texas");

		public static AzureLocation USGovVirginia { get; } = CreateStaticReference("usgovvirginia", "US Gov Virginia");

		public static AzureLocation USGovIowa { get; } = CreateStaticReference("usgoviowa", "US Gov Iowa");

		public static AzureLocation IsraelCentral { get; } = CreateStaticReference("israelcentral", "Israel Central");

		public static AzureLocation ItalyNorth { get; } = CreateStaticReference("italynorth", "Italy North");

		public static AzureLocation PolandCentral { get; } = CreateStaticReference("polandcentral", "Poland Central");

		public string Name { get; }

		public string? DisplayName { get; }

		public AzureLocation(string location)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			Name = GetNameFromDisplayName(location, out var foundSpace);
			string key = (foundSpace ? Name : location.ToLowerInvariant());
			if (PublicCloudLocations.TryGetValue(key, out var value))
			{
				Name = value.Name;
				DisplayName = value.DisplayName;
			}
			else
			{
				DisplayName = (foundSpace ? location : null);
			}
		}

		public AzureLocation(string name, string displayName)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Name = name;
			DisplayName = displayName;
		}

		private static string GetNameFromDisplayName(string name, out bool foundSpace)
		{
			foundSpace = false;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in name)
			{
				if (c == ' ')
				{
					foundSpace = true;
				}
				else
				{
					stringBuilder.Append(char.ToLowerInvariant(c));
				}
			}
			if (!foundSpace)
			{
				return name;
			}
			return stringBuilder.ToString();
		}

		private static AzureLocation CreateStaticReference(string name, string displayName)
		{
			AzureLocation azureLocation = new AzureLocation(name, displayName);
			PublicCloudLocations.Add(name, azureLocation);
			return azureLocation;
		}

		public override string ToString()
		{
			return Name;
		}

		public static implicit operator AzureLocation(string location)
		{
			if (location != null && PublicCloudLocations.TryGetValue(location, out var value))
			{
				return value;
			}
			return new AzureLocation(location);
		}

		public bool Equals(AzureLocation other)
		{
			return Name == other.Name;
		}

		public static implicit operator string(AzureLocation location)
		{
			return location.ToString();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (!(obj is AzureLocation other))
			{
				return false;
			}
			return Equals(other);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}

		public static bool operator ==(AzureLocation left, AzureLocation right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(AzureLocation left, AzureLocation right)
		{
			return !left.Equals(right);
		}
	}
	internal class ChainingClassifier : ResponseClassifier
	{
		private class StatusCodeHandler : ResponseClassificationHandler
		{
			private (int Status, bool IsError)[] _statusCodes;

			public StatusCodeHandler((int Status, bool IsError)[] statusCodes)
			{
				_statusCodes = statusCodes;
			}

			public override bool TryClassify(HttpMessage message, out bool isError)
			{
				(int, bool)[] statusCodes = _statusCodes;
				for (int i = 0; i < statusCodes.Length; i++)
				{
					(int, bool) tuple = statusCodes[i];
					if (tuple.Item1 == message.Response.Status)
					{
						isError = tuple.Item2;
						return true;
					}
				}
				isError = false;
				return false;
			}
		}

		private ResponseClassificationHandler[]? _handlers;

		private ResponseClassifier _endOfChain;

		public ChainingClassifier((int Status, bool IsError)[]? statusCodes, ResponseClassificationHandler[]? handlers, ResponseClassifier endOfChain)
		{
			if (handlers != null)
			{
				AddClassifiers(handlers);
			}
			if (statusCodes != null)
			{
				StatusCodeHandler[] array = new StatusCodeHandler[1]
				{
					new StatusCodeHandler(statusCodes)
				};
				ResponseClassificationHandler[] array2 = array;
				AddClassifiers(new ReadOnlySpan<ResponseClassificationHandler>(array2));
			}
			_endOfChain = endOfChain;
		}

		public override bool IsErrorResponse(HttpMessage message)
		{
			if (_handlers != null)
			{
				ResponseClassificationHandler[] handlers = _handlers;
				for (int i = 0; i < handlers.Length; i++)
				{
					if (handlers[i].TryClassify(message, out var isError))
					{
						return isError;
					}
				}
			}
			return _endOfChain.IsErrorResponse(message);
		}

		private void AddClassifiers(ReadOnlySpan<ResponseClassificationHandler> handlers)
		{
			int num = ((_handlers != null) ? _handlers.Length : 0);
			Array.Resize(ref _handlers, num + handlers.Length);
			Span<ResponseClassificationHandler> destination = new Span<ResponseClassificationHandler>(_handlers, num, handlers.Length);
			handlers.CopyTo(destination);
		}
	}
	public abstract class ClientOptions
	{
		private HttpPipelineTransport _transport;

		internal bool IsCustomTransportSet { get; private set; }

		public static ClientOptions Default { get; private set; } = new DefaultClientOptions();

		public HttpPipelineTransport Transport
		{
			get
			{
				return _transport;
			}
			set
			{
				_transport = value ?? throw new ArgumentNullException("value");
				IsCustomTransportSet = true;
			}
		}

		public DiagnosticsOptions Diagnostics { get; }

		public RetryOptions Retry { get; }

		public HttpPipelinePolicy? RetryPolicy { get; set; }

		internal List<(HttpPipelinePosition Position, HttpPipelinePolicy Policy)>? Policies { get; private set; }

		internal static void ResetDefaultOptions()
		{
			Default = new DefaultClientOptions();
		}

		protected ClientOptions()
			: this(Default, null)
		{
		}

		protected ClientOptions(DiagnosticsOptions? diagnostics)
			: this(Default, diagnostics)
		{
		}

		internal ClientOptions(ClientOptions? clientOptions, DiagnosticsOptions? diagnostics)
		{
			if (clientOptions != null)
			{
				Retry = new RetryOptions(clientOptions.Retry);
				RetryPolicy = clientOptions.RetryPolicy;
				Diagnostics = diagnostics ?? new DiagnosticsOptions(clientOptions.Diagnostics);
				_transport = clientOptions.Transport;
				if (clientOptions.Policies != null)
				{
					Policies = new List<(HttpPipelinePosition, HttpPipelinePolicy)>(clientOptions.Policies);
				}
			}
			else
			{
				_transport = HttpPipelineTransport.Create();
				Diagnostics = new DiagnosticsOptions(null);
				Retry = new RetryOptions(null);
			}
		}

		public void AddPolicy(HttpPipelinePolicy policy, HttpPipelinePosition position)
		{
			if (position != HttpPipelinePosition.PerCall && position != HttpPipelinePosition.PerRetry && position != HttpPipelinePosition.BeforeTransport)
			{
				throw new ArgumentOutOfRangeException("position", position, null);
			}
			if (Policies == null)
			{
				List<(HttpPipelinePosition, HttpPipelinePolicy)> list = (Policies = new List<(HttpPipelinePosition, HttpPipelinePolicy)>());
			}
			Policies.Add((position, policy));
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? obj)
		{
			return base.Equals(obj);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string? ToString()
		{
			return base.ToString();
		}
	}
	public readonly struct ContentType : IEquatable<ContentType>, IEquatable<string>
	{
		private readonly string _contentType;

		public static ContentType ApplicationJson { get; } = new ContentType("application/json");

		public static ContentType ApplicationOctetStream { get; } = new ContentType("application/octet-stream");

		public static ContentType TextPlain { get; } = new ContentType("text/plain");

		public ContentType(string contentType)
		{
			Argument.AssertNotNull(contentType, "contentType");
			_contentType = contentType;
		}

		public static implicit operator ContentType(string contentType)
		{
			return new ContentType(contentType);
		}

		public bool Equals(ContentType other)
		{
			return string.Equals(_contentType, other._contentType, StringComparison.Ordinal);
		}

		public bool Equals(string? other)
		{
			return string.Equals(_contentType, other, StringComparison.Ordinal);
		}

		public override bool Equals(object? obj)
		{
			if (!(obj is ContentType other) || !Equals(other))
			{
				if (obj is string text)
				{
					return text.Equals(_contentType, StringComparison.Ordinal);
				}
				return false;
			}
			return true;
		}

		public override int GetHashCode()
		{
			return _contentType?.GetHashCode() ?? 0;
		}

		public static bool operator ==(ContentType left, ContentType right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ContentType left, ContentType right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			return _contentType ?? "";
		}
	}
	internal class DefaultClientOptions : ClientOptions
	{
		public DefaultClientOptions()
			: base(null, null)
		{
			base.Diagnostics.IsTelemetryEnabled = (!EnvironmentVariableToBool(Environment.GetEnvironmentVariable("AZURE_TELEMETRY_DISABLED"))) ?? true;
			base.Diagnostics.IsDistributedTracingEnabled = (!EnvironmentVariableToBool(Environment.GetEnvironmentVariable("AZURE_TRACING_DISABLED"))) ?? true;
		}

		private static bool? EnvironmentVariableToBool(string? value)
		{
			if (string.Equals(bool.TrueString, value, StringComparison.OrdinalIgnoreCase) || string.Equals("1", value, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (string.Equals(bool.FalseString, value, StringComparison.OrdinalIgnoreCase) || string.Equals("0", value, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			return null;
		}
	}
	public abstract class DelayStrategy
	{
		internal const double DefaultJitterFactor = 0.2;

		private readonly Random _random = new ThreadSafeRandom();

		private readonly double _minJitterFactor;

		private readonly double _maxJitterFactor;

		private readonly TimeSpan _maxDelay;

		protected DelayStrategy(TimeSpan? maxDelay = null, double jitterFactor = 0.2)
		{
			_minJitterFactor = 1.0 - jitterFactor;
			_maxJitterFactor = 1.0 + jitterFactor;
			_maxDelay = maxDelay ?? RetryOptions.DefaultMaxDelay;
		}

		public static DelayStrategy CreateExponentialDelayStrategy(TimeSpan? initialDelay = null, TimeSpan? maxDelay = null)
		{
			TimeSpan valueOrDefault = initialDelay.GetValueOrDefault();
			if (!initialDelay.HasValue)
			{
				valueOrDefault = RetryOptions.DefaultInitialDelay;
				initialDelay = valueOrDefault;
			}
			valueOrDefault = maxDelay.GetValueOrDefault();
			if (!maxDelay.HasValue)
			{
				valueOrDefault = RetryOptions.DefaultMaxDelay;
				maxDelay = valueOrDefault;
			}
			return new ExponentialDelayStrategy(initialDelay, maxDelay);
		}

		public static DelayStrategy CreateFixedDelayStrategy(TimeSpan? delay = null)
		{
			return new FixedDelayStrategy(delay ?? RetryOptions.DefaultInitialDelay);
		}

		protected abstract TimeSpan GetNextDelayCore(Response? response, int retryNumber);

		public TimeSpan GetNextDelay(Response? response, int retryNumber)
		{
			TimeSpan val = response?.Headers.RetryAfter ?? TimeSpan.Zero;
			TimeSpan nextDelayCore = GetNextDelayCore(response, retryNumber);
			TimeSpan val2 = Min(ApplyJitter(nextDelayCore), _maxDelay);
			return Max(val, val2);
		}

		private TimeSpan ApplyJitter(TimeSpan delay)
		{
			double num = _random.NextDouble();
			num = num * (_maxJitterFactor - _minJitterFactor) + _minJitterFactor;
			return TimeSpan.FromMilliseconds(delay.TotalMilliseconds * num);
		}

		protected static TimeSpan Max(TimeSpan val1, TimeSpan val2)
		{
			if (!(val1 > val2))
			{
				return val2;
			}
			return val1;
		}

		protected static TimeSpan Min(TimeSpan val1, TimeSpan val2)
		{
			if (!(val1 < val2))
			{
				return val2;
			}
			return val1;
		}
	}
	public static class DelegatedTokenCredential
	{
		private class StaticTokenCredential : TokenCredential
		{
			private readonly Func<TokenRequestContext, CancellationToken, AccessToken> _getToken;

			private readonly Func<TokenRequestContext, CancellationToken, ValueTask<AccessToken>> _getTokenAsync;

			internal StaticTokenCredential(Func<TokenRequestContext, CancellationToken, AccessToken> getToken, Func<TokenRequestContext, CancellationToken, ValueTask<AccessToken>> getTokenAsync)
			{
				_getToken = getToken;
				_getTokenAsync = getTokenAsync;
			}

			internal StaticTokenCredential(Func<TokenRequestContext, CancellationToken, AccessToken> getToken)
			{
				_getToken = getToken;
				_getTokenAsync = (TokenRequestContext context, CancellationToken token) => new ValueTask<AccessToken>(_getToken(context, token));
			}

			public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
			{
				return _getTokenAsync(requestContext, cancellationToken);
			}

			public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
			{
				return _getToken(requestContext, cancellationToken);
			}
		}

		public static TokenCredential Create(Func<TokenRequestContext, CancellationToken, AccessToken> getToken, Func<TokenRequestContext, CancellationToken, ValueTask<AccessToken>> getTokenAsync)
		{
			return new StaticTokenCredential(getToken, getTokenAsync);
		}

		public static TokenCredential Create(Func<TokenRequestContext, CancellationToken, AccessToken> getToken)
		{
			return new StaticTokenCredential(getToken);
		}
	}
	public class DiagnosticsOptions
	{
		private const int MaxApplicationIdLength = 24;

		private string? _applicationId;

		public bool IsLoggingEnabled { get; set; } = true;

		public bool IsDistributedTracingEnabled { get; set; } = true;

		public bool IsTelemetryEnabled { get; set; }

		public bool IsLoggingContentEnabled { get; set; }

		public int LoggedContentSizeLimit { get; set; } = 4096;

		public IList<string> LoggedHeaderNames { get; internal set; }

		public IList<string> LoggedQueryParameters { get; internal set; }

		public string? ApplicationId
		{
			get
			{
				return _applicationId;
			}
			set
			{
				if (value != null && value.Length > 24)
				{
					throw new ArgumentOutOfRangeException("value", string.Format("{0} must be shorter than {1} characters", "ApplicationId", 25));
				}
				_applicationId = value;
			}
		}

		public static string? DefaultApplicationId
		{
			get
			{
				return ClientOptions.Default.Diagnostics.ApplicationId;
			}
			set
			{
				ClientOptions.Default.Diagnostics.ApplicationId = value;
			}
		}

		protected internal DiagnosticsOptions()
			: this(ClientOptions.Default.Diagnostics)
		{
		}

		internal DiagnosticsOptions(DiagnosticsOptions? diagnosticsOptions)
		{
			if (diagnosticsOptions != null)
			{
				ApplicationId = diagnosticsOptions.ApplicationId;
				IsLoggingEnabled = diagnosticsOptions.IsLoggingEnabled;
				IsTelemetryEnabled = diagnosticsOptions.IsTelemetryEnabled;
				LoggedHeaderNames = new List<string>(diagnosticsOptions.LoggedHeaderNames);
				LoggedQueryParameters = new List<string>(diagnosticsOptions.LoggedQueryParameters);
				LoggedContentSizeLimit = diagnosticsOptions.LoggedContentSizeLimit;
				IsDistributedTracingEnabled = diagnosticsOptions.IsDistributedTracingEnabled;
				IsLoggingContentEnabled = diagnosticsOptions.IsLoggingContentEnabled;
				return;
			}
			LoggedHeaderNames = new List<string>
			{
				"x-ms-request-id", "x-ms-client-request-id", "x-ms-return-client-request-id", "traceparent", "MS-CV", "Accept", "Cache-Control", "Connection", "Content-Length", "Content-Type",
				"Date", "ETag", "Expires", "If-Match", "If-Modified-Since", "If-None-Match", "If-Unmodified-Since", "Last-Modified", "Pragma", "Request-Id",
				"Retry-After", "Server", "Transfer-Encoding", "User-Agent", "WWW-Authenticate"
			};
			LoggedQueryParameters = new List<string> { "api-version" };
			IsTelemetryEnabled = (!EnvironmentVariableToBool(Environment.GetEnvironmentVariable("AZURE_TELEMETRY_DISABLED"))) ?? true;
			IsDistributedTracingEnabled = (!EnvironmentVariableToBool(Environment.GetEnvironmentVariable("AZURE_TRACING_DISABLED"))) ?? true;
		}

		private static bool? EnvironmentVariableToBool(string? value)
		{
			if (string.Equals(bool.TrueString, value, StringComparison.OrdinalIgnoreCase) || string.Equals("1", value, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (string.Equals(bool.FalseString, value, StringComparison.OrdinalIgnoreCase) || string.Equals("0", value, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			return null;
		}
	}
	internal class ExponentialDelayStrategy : DelayStrategy
	{
		private readonly TimeSpan _delay;

		public ExponentialDelayStrategy(TimeSpan? delay = null, TimeSpan? maxDelay = null)
			: base(maxDelay)
		{
			_delay = delay ?? RetryOptions.DefaultInitialDelay;
		}

		protected override TimeSpan GetNextDelayCore(Response? response, int retryNumber)
		{
			double num = 1 << retryNumber - 1;
			TimeSpan delay = _delay;
			return TimeSpan.FromMilliseconds(num * delay.TotalMilliseconds);
		}
	}
	internal class FixedDelayStrategy : DelayStrategy
	{
		private readonly TimeSpan _delay;

		public FixedDelayStrategy(TimeSpan delay)
			: base(TimeSpan.FromMilliseconds(delay.TotalMilliseconds))
		{
			_delay = delay;
		}

		protected override TimeSpan GetNextDelayCore(Response? response, int retryNumber)
		{
			return _delay;
		}
	}
	public readonly struct HttpHeader : IEquatable<HttpHeader>
	{
		public static class Names
		{
			public static string Date => "Date";

			public static string XMsDate => "x-ms-date";

			public static string ContentType => "Content-Type";

			public static string ContentLength => "Content-Length";

			public static string ETag => "ETag";

			public static string XMsRequestId => "x-ms-request-id";

			public static string UserAgent => "User-Agent";

			public static string Accept => "Accept";

			public static string Authorization => "Authorization";

			public static string Range => "Range";

			public static string XMsRange => "x-ms-range";

			public static string IfMatch => "If-Match";

			public static string IfNoneMatch => "If-None-Match";

			public static string IfModifiedSince => "If-Modified-Since";

			public static string IfUnmodifiedSince => "If-Unmodified-Since";

			public static string Prefer => "Prefer";

			public static string Referer => "Referer";

			public static string Host => "Host";

			public static string ContentDisposition => "Content-Disposition";

			public static string WwwAuthenticate => "WWW-Authenticate";
		}

		public static class Common
		{
			private const string ApplicationJson = "application/json";

			private const string ApplicationOctetStream = "application/octet-stream";

			private const string ApplicationFormUrlEncoded = "application/x-www-form-urlencoded";

			public static readonly HttpHeader JsonContentType = new HttpHeader(Names.ContentType, "application/json");

			public static readonly HttpHeader JsonAccept = new HttpHeader(Names.Accept, "application/json");

			public static readonly HttpHeader OctetStreamContentType = new HttpHeader(Names.ContentType, "application/octet-stream");

			public static readonly HttpHeader FormUrlEncodedContentType = new HttpHeader(Names.ContentType, "application/x-www-form-urlencoded");
		}

		public string Name { get; }

		public string Value { get; }

		public HttpHeader(string name, string value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name shouldn't be null or empty", "name");
			}
			Name = name;
			Value = value;
		}

		public override int GetHashCode()
		{
			HashCodeBuilder hashCodeBuilder = default(HashCodeBuilder);
			hashCodeBuilder.Add(Name, StringComparer.OrdinalIgnoreCase);
			hashCodeBuilder.Add(Value);
			return hashCodeBuilder.ToHashCode();
		}

		public override bool Equals(object? obj)
		{
			if (obj is HttpHeader other)
			{
				return Equals(other);
			}
			return false;
		}

		public override string ToString()
		{
			return Name + ":" + Value;
		}

		public bool Equals(HttpHeader other)
		{
			if (string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase))
			{
				return Value.Equals(other.Value, StringComparison.Ordinal);
			}
			return false;
		}
	}
	public sealed class HttpMessage : IDisposable
	{
		private class ResponseShouldNotBeUsedStream : Stream
		{
			public Stream Original { get; }

			public override bool CanRead
			{
				get
				{
					throw CreateException();
				}
			}

			public override bool CanSeek
			{
				get
				{
					throw CreateException();
				}
			}

			public override bool CanWrite
			{
				get
				{
					throw CreateException();
				}
			}

			public override long Length
			{
				get
				{
					throw CreateException();
				}
			}

			public override long Position
			{
				get
				{
					throw CreateException();
				}
				set
				{
					throw CreateException();
				}
			}

			public ResponseShouldNotBeUsedStream(Stream original)
			{
				Original = original;
			}

			private static Exception CreateException()
			{
				return new InvalidOperationException("The operation has called ExtractResponseContent and will provide the stream as part of its response type.");
			}

			public override void Flush()
			{
				throw CreateException();
			}

			public override int Read(byte[] buffer, int offset, int count)
			{
				throw CreateException();
			}

			public override long Seek(long offset, SeekOrigin origin)
			{
				throw CreateException();
			}

			public override void SetLength(long value)
			{
				throw CreateException();
			}

			public override void Write(byte[] buffer, int offset, int count)
			{
				throw CreateException();
			}
		}

		private class MessagePropertyKey
		{
		}

		private ArrayBackedPropertyBag<ulong, object> _propertyBag;

		private Response? _response;

		public Request Request { get; }

		public Response Response
		{
			get
			{
				if (_response == null)
				{
					throw new InvalidOperationException("Response was not set, make sure SendAsync was called");
				}
				return _response;
			}
			set
			{
				_response = value;
			}
		}

		public bool HasResponse => _response != null;

		public CancellationToken CancellationToken { get; internal set; }

		public ResponseClassifier ResponseClassifier { get; set; }

		public bool BufferResponse { get; set; }

		public TimeSpan? NetworkTimeout { get; set; }

		internal int RetryNumber { get; set; }

		internal DateTimeOffset ProcessingStartTime { get; set; }

		public MessageProcessingContext ProcessingContext => new MessageProcessingContext(this);

		internal List<(HttpPipelinePosition Position, HttpPipelinePolicy Policy)>? Policies { get; set; }

		public HttpMessage(Request request, ResponseClassifier responseClassifier)
		{
			Argument.AssertNotNull(request, "Request");
			Request = request;
			ResponseClassifier = responseClassifier;
			BufferResponse = true;
			_propertyBag = new ArrayBackedPropertyBag<ulong, object>();
		}

		internal void ClearResponse()
		{
			_response = null;
		}

		internal void ApplyRequestContext(RequestContext? context, ResponseClassifier? classifier)
		{
			if (context == null)
			{
				return;
			}
			context.Freeze();
			List<(HttpPipelinePosition Position, HttpPipelinePolicy Policy)>? policies = context.Policies;
			if (policies != null && policies.Count > 0)
			{
				if (Policies == null)
				{
					List<(HttpPipelinePosition, HttpPipelinePolicy)> list = (Policies = new List<(HttpPipelinePosition, HttpPipelinePolicy)>(context.Policies.Count));
				}
				Policies.AddRange(context.Policies);
			}
			if (classifier != null)
			{
				ResponseClassifier = context.Apply(classifier);
			}
		}

		public bool TryGetProperty(string name, out object? value)
		{
			value = null;
			if (_propertyBag.IsEmpty || !_propertyBag.TryGetValue((ulong)(long)typeof(MessagePropertyKey).TypeHandle.Value, out object value2))
			{
				return false;
			}
			return ((Dictionary<string, object>)value2).TryGetValue(name, out value);
		}

		public void SetProperty(string name, object value)
		{
			Dictionary<string, object> dictionary;
			if (!_propertyBag.TryGetValue((ulong)(long)typeof(MessagePropertyKey).TypeHandle.Value, out object value2))
			{
				dictionary = new Dictionary<string, object>();
				_propertyBag.Set((ulong)(long)typeof(MessagePropertyKey).TypeHandle.Value, dictionary);
			}
			else
			{
				dictionary = (Dictionary<string, object>)value2;
			}
			dictionary[name] = value;
		}

		public bool TryGetProperty(Type type, out object? value)
		{
			return _propertyBag.TryGetValue((ulong)(long)type.TypeHandle.Value, out value);
		}

		public void SetProperty(Type type, object value)
		{
			_propertyBag.Set((ulong)(long)type.TypeHandle.Value, value);
		}

		public Stream? ExtractResponseContent()
		{
			Stream stream = _response?.ContentStream;
			if (!(stream is ResponseShouldNotBeUsedStream responseShouldNotBeUsedStream))
			{
				if (stream != null)
				{
					_response.ContentStream = new ResponseShouldNotBeUsedStream(_response.ContentStream);
					return stream;
				}
				return null;
			}
			return responseShouldNotBeUsedStream.Original;
		}

		public void Dispose()
		{
			Request.Dispose();
			_propertyBag.Dispose();
			Response response = _response;
			if (response != null)
			{
				_response = null;
				response.Dispose();
			}
		}
	}
	public enum HttpPipelinePosition
	{
		PerCall,
		PerRetry,
		BeforeTransport
	}
	internal struct ArrayBackedPropertyBag<TKey, TValue> where TKey : struct, IEquatable<TKey>
	{
		private (TKey Key, TValue Value) _first;

		private (TKey Key, TValue Value) _second;

		private (TKey Key, TValue Value)[]? _rest;

		private int _count;

		private readonly object _lock;

		public int Count => _count;

		public bool IsEmpty => _count == 0;

		public ArrayBackedPropertyBag()
		{
			_first = default((TKey, TValue));
			_second = default((TKey, TValue));
			_rest = null;
			_count = 0;
			_lock = new object();
		}

		public void GetAt(int index, out TKey key, out TValue value)
		{
			(key, value) = index switch
			{
				0 => _first, 
				1 => _second, 
				_ => GetRest()[index - 2], 
			};
		}

		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			int index = GetIndex(key);
			if (index < 0)
			{
				value = default(TValue);
				return false;
			}
			value = GetAt(index);
			return true;
		}

		public bool TryAdd(TKey key, TValue value, out TValue? existingValue)
		{
			int index = GetIndex(key);
			if (index >= 0)
			{
				existingValue = GetAt(index);
				return false;
			}
			AddInternal(key, value);
			existingValue = default(TValue);
			return true;
		}

		public void Set(TKey key, TValue value)
		{
			int index = GetIndex(key);
			if (index < 0)
			{
				AddInternal(key, value);
			}
			else
			{
				SetAt(index, (Key: key, Value: value));
			}
		}

		public bool TryRemove(TKey key)
		{
			switch (_count)
			{
			case 0:
				return false;
			case 1:
				if (IsFirst(key))
				{
					_first = default((TKey, TValue));
					_count--;
					return true;
				}
				return false;
			case 2:
				if (IsFirst(key))
				{
					_first = _second;
					_second = default((TKey, TValue));
					_count--;
					return true;
				}
				if (IsSecond(key))
				{
					_second = default((TKey, TValue));
					_count--;
					return true;
				}
				return false;
			default:
			{
				(TKey, TValue)[] rest = GetRest();
				if (IsFirst(key))
				{
					_first = _second;
					_second = rest[0];
					_count--;
					Array.Copy(rest, 1, rest, 0, _count - 2);
					rest[_count - 2] = default((TKey, TValue));
					return true;
				}
				if (IsSecond(key))
				{
					_second = rest[0];
					_count--;
					Array.Copy(rest, 1, rest, 0, _count - 2);
					rest[_count - 2] = default((TKey, TValue));
					return true;
				}
				for (int i = 0; i < _count - 2; i++)
				{
					if (rest[i].Item1.Equals(key))
					{
						_count--;
						Array.Copy(rest, i + 1, rest, i, _count - 2 - i);
						rest[_count - 2] = default((TKey, TValue));
						return true;
					}
				}
				return false;
			}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsFirst(TKey key)
		{
			return _first.Key.Equals(key);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsSecond(TKey key)
		{
			return _second.Key.Equals(key);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void AddInternal(TKey key, TValue value)
		{
			switch (_count)
			{
			case 0:
				_first = (Key: key, Value: value);
				_count = 1;
				return;
			case 1:
				if (IsFirst(key))
				{
					_first = (Key: _first.Key, Value: value);
					return;
				}
				_second = (Key: key, Value: value);
				_count = 2;
				return;
			}
			if (_rest == null)
			{
				_rest = ArrayPool<(TKey, TValue)>.Shared.Rent(8);
				_rest[_count++ - 2] = (Key: key, Value: value);
				return;
			}
			if (_rest.Length <= _count)
			{
				(TKey, TValue)[] array = ArrayPool<(TKey, TValue)>.Shared.Rent(_rest.Length << 1);
				_rest.CopyTo(array, 0);
				(TKey, TValue)[] rest = _rest;
				_rest = array;
				ArrayPool<(TKey, TValue)>.Shared.Return(rest, clearArray: true);
			}
			_rest[_count++ - 2] = (Key: key, Value: value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetAt(int index, (TKey Key, TValue Value) value)
		{
			switch (index)
			{
			case 0:
				_first = value;
				break;
			case 1:
				_second = value;
				break;
			default:
				GetRest()[index - 2] = value;
				break;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private TValue GetAt(int index)
		{
			return index switch
			{
				0 => _first.Value, 
				1 => _second.Value, 
				_ => GetRest()[index - 2].Value, 
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int GetIndex(TKey key)
		{
			if (_count == 0)
			{
				return -1;
			}
			if (_count > 0 && _first.Key.Equals(key))
			{
				return 0;
			}
			if (_count > 1 && _second.Key.Equals(key))
			{
				return 1;
			}
			if (_count <= 2)
			{
				return -1;
			}
			(TKey, TValue)[] rest = GetRest();
			int num = _count - 2;
			for (int i = 0; i < num; i++)
			{
				if (rest[i].Item1.Equals(key))
				{
					return i + 2;
				}
			}
			return -1;
		}

		public void Dispose()
		{
			_count = 0;
			_first = default((TKey, TValue));
			_second = default((TKey, TValue));
			lock (_lock)
			{
				if (_rest != null)
				{
					(TKey, TValue)[] rest = _rest;
					_rest = null;
					ArrayPool<(TKey, TValue)>.Shared.Return(rest, clearArray: true);
				}
			}
		}

		private (TKey Key, TValue Value)[] GetRest()
		{
			return _rest ?? throw new InvalidOperationException(string.Format("{0} field is null while {1} == {2}", "_rest", "_count", _count));
		}

		[Conditional("DEBUG")]
		private void CheckDisposed()
		{
		}
	}
	internal class GenericOperationSource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] T> : IOperationSource<T> where T : IPersistableModel<T>
	{
		T IOperationSource<T>.CreateResult(Response response, CancellationToken cancellationToken)
		{
			return CreateResult(response);
		}

		ValueTask<T> IOperationSource<T>.CreateResultAsync(Response response, CancellationToken cancellationToken)
		{
			return new ValueTask<T>(CreateResult(response));
		}

		private T CreateResult(Response response)
		{
			return ModelReaderWriter.Read<T>(response.Content);
		}
	}
	internal class RehydrationOperation : Operation
	{
		private readonly NextLinkOperationImplementation _nextLinkOperation;

		private readonly OperationInternal _operation;

		public override string Id => _nextLinkOperation.OperationId;

		public override bool HasCompleted => _operation.HasCompleted;

		public RehydrationOperation(NextLinkOperationImplementation nextLinkOperation, OperationState operationState, ClientOptions? options = null)
		{
			_nextLinkOperation = nextLinkOperation;
			_operation = (operationState.HasCompleted ? (_operation = new OperationInternal(operationState)) : new OperationInternal(_nextLinkOperation, new ClientDiagnostics(options ?? ClientOptions.Default), operationState.RawResponse));
		}

		public override RehydrationToken? GetRehydrationToken()
		{
			return _nextLinkOperation.GetRehydrationToken();
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
	}
	internal class RehydrationOperation<T> : Operation<T> where T : IPersistableModel<T>
	{
		private readonly OperationInternal<T> _operation;

		private readonly NextLinkOperationImplementation _nextLinkOperation;

		public override T Value => _operation.Value;

		public override bool HasValue => _operation.HasValue;

		public override string Id => _nextLinkOperation.OperationId;

		public override bool HasCompleted => _operation.HasCompleted;

		public RehydrationOperation(NextLinkOperationImplementation nextLinkOperation, OperationState<T> operationState, IOperation<T> operation, ClientOptions? options = null)
		{
			_nextLinkOperation = nextLinkOperation;
			_operation = (operationState.HasCompleted ? new OperationInternal<T>(operationState) : new OperationInternal<T>(operation, new ClientDiagnostics(options ?? ClientOptions.Default), operationState.RawResponse));
		}

		public override RehydrationToken? GetRehydrationToken()
		{
			return _nextLinkOperation.GetRehydrationToken();
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
	}
	internal class RuntimeInformationWrapper
	{
		public virtual string FrameworkDescription => RuntimeInformation.FrameworkDescription;

		public virtual string OSDescription => RuntimeInformation.OSDescription;

		public virtual Architecture OSArchitecture => RuntimeInformation.OSArchitecture;

		public virtual Architecture ProcessArchitecture => RuntimeInformation.ProcessArchitecture;

		public virtual bool IsOSPlatform(OSPlatform osPlatform)
		{
			return RuntimeInformation.IsOSPlatform(osPlatform);
		}
	}
	internal static class StringExtensions
	{
		public static int IndexOfOrdinal(this string s, char c)
		{
			return s.IndexOf(c);
		}

		public static int GetHashCodeOrdinal(this string? s)
		{
			if (s == null)
			{
				return 0;
			}
			return StringComparer.Ordinal.GetHashCode(s);
		}
	}
	public readonly struct MessageProcessingContext
	{
		private readonly HttpMessage _message;

		public DateTimeOffset StartTime
		{
			get
			{
				return _message.ProcessingStartTime;
			}
			internal set
			{
				_message.ProcessingStartTime = value;
			}
		}

		public int RetryNumber
		{
			get
			{
				return _message.RetryNumber;
			}
			set
			{
				_message.RetryNumber = value;
			}
		}

		internal MessageProcessingContext(HttpMessage message)
		{
			_message = message;
		}
	}
	internal class BufferedReadStream : Stream
	{
		private const byte CR = 13;

		private const byte LF = 10;

		private readonly Stream _inner;

		private readonly byte[] _buffer;

		private readonly ArrayPool<byte> _bytePool;

		private int _bufferOffset;

		private int _bufferCount;

		private bool _disposed;

		public ArraySegment<byte> BufferedData => new ArraySegment<byte>(_buffer, _bufferOffset, _bufferCount);

		public override bool CanRead
		{
			get
			{
				if (!_inner.CanRead)
				{
					return _bufferCount > 0;
				}
				return true;
			}
		}

		public override bool CanSeek => _inner.CanSeek;

		public override bool CanTimeout => _inner.CanTimeout;

		public override bool CanWrite => _inner.CanWrite;

		public override long Length => _inner.Length;

		public override long Position
		{
			get
			{
				return _inner.Position - _bufferCount;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", value, "Position must be positive.");
				}
				if (value == Position)
				{
					return;
				}
				if (value <= _inner.Position)
				{
					int num = (int)(_inner.Position - value);
					if (num <= _bufferCount)
					{
						_bufferOffset += num;
						_bufferCount -= num;
					}
					else
					{
						_bufferOffset = 0;
						_bufferCount = 0;
						_inner.Position = value;
					}
				}
				else
				{
					_bufferOffset = 0;
					_bufferCount = 0;
					_inner.Position = value;
				}
			}
		}

		public BufferedReadStream(Stream inner, int bufferSize)
			: this(inner, bufferSize, ArrayPool<byte>.Shared)
		{
		}

		public BufferedReadStream(Stream inner, int bufferSize, ArrayPool<byte> bytePool)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			_inner = inner;
			_bytePool = bytePool;
			_buffer = bytePool.Rent(bufferSize);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				Position = offset;
				break;
			case SeekOrigin.Current:
				Position += offset;
				break;
			default:
				Position = Length + offset;
				break;
			}
			return Position;
		}

		public override void SetLength(long value)
		{
			_inner.SetLength(value);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (!_disposed)
			{
				_disposed = true;
				_bytePool.Return(_buffer);
				if (disposing)
				{
					_inner.Dispose();
				}
			}
		}

		public override void Flush()
		{
			_inner.Flush();
		}

		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return _inner.FlushAsync(cancellationToken);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			_inner.Write(buffer, offset, count);
		}

		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return _inner.WriteAsync(buffer, offset, count, cancellationToken);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			ValidateBuffer(buffer, offset, count);
			if (_bufferCount > 0)
			{
				int num = Math.Min(_bufferCount, count);
				Buffer.BlockCopy(_buffer, _bufferOffset, buffer, offset, num);
				_bufferOffset += num;
				_bufferCount -= num;
				return num;
			}
			return _inner.Read(buffer, offset, count);
		}

		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			ValidateBuffer(buffer, offset, count);
			if (_bufferCount > 0)
			{
				int num = Math.Min(_bufferCount, count);
				Buffer.BlockCopy(_buffer, _bufferOffset, buffer, offset, num);
				_bufferOffset += num;
				_bufferCount -= num;
				return num;
			}
			return await _inner.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public bool EnsureBuffered()
		{
			if (_bufferCount > 0)
			{
				return true;
			}
			_bufferOffset = 0;
			_bufferCount = _inner.Read(_buffer, 0, _buffer.Length);
			return _bufferCount > 0;
		}

		public async Task<bool> EnsureBufferedAsync(CancellationToken cancellationToken)
		{
			if (_bufferCount > 0)
			{
				return true;
			}
			_bufferOffset = 0;
			_bufferCount = await _inner.ReadAsync(_buffer, 0, _buffer.Length, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return _bufferCount > 0;
		}

		public bool EnsureBuffered(int minCount)
		{
			if (minCount > _buffer.Length)
			{
				throw new ArgumentOutOfRangeException("minCount", minCount, "The value must be smaller than the buffer size: " + _buffer.Length.ToString(CultureInfo.InvariantCulture));
			}
			while (_bufferCount < minCount)
			{
				if (_bufferOffset > 0)
				{
					if (_bufferCount > 0)
					{
						Buffer.BlockCopy(_buffer, _bufferOffset, _buffer, 0, _bufferCount);
					}
					_bufferOffset = 0;
				}
				int num = _inner.Read(_buffer, _bufferOffset + _bufferCount, _buffer.Length - _bufferCount - _bufferOffset);
				_bufferCount += num;
				if (num == 0)
				{
					return false;
				}
			}
			return true;
		}

		public async Task<bool> EnsureBufferedAsync(int minCount, CancellationToken cancellationToken)
		{
			if (minCount > _buffer.Length)
			{
				throw new ArgumentOutOfRangeException("minCount", minCount, "The value must be smaller than the buffer size: " + _buffer.Length.ToString(CultureInfo.InvariantCulture));
			}
			while (_bufferCount < minCount)
			{
				if (_bufferOffset > 0)
				{
					if (_bufferCount > 0)
					{
						Buffer.BlockCopy(_buffer, _bufferOffset, _buffer, 0, _bufferCount);
					}
					_bufferOffset = 0;
				}
				int num = await _inner.ReadAsync(_buffer, _bufferOffset + _bufferCount, _buffer.Length - _bufferCount - _bufferOffset, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				_bufferCount += num;
				if (num == 0)
				{
					return false;
				}
			}
			return true;
		}

		public string ReadLine(int lengthLimit)
		{
			CheckDisposed();
			using MemoryStream memoryStream = new MemoryStream(200);
			bool foundCR = false;
			bool foundCRLF = false;
			bool foundLF = false;
			while (!(foundCRLF || foundLF) && EnsureBuffered())
			{
				if (memoryStream.Length > lengthLimit)
				{
					throw new InvalidDataException($"Line length limit {lengthLimit} exceeded.");
				}
				ProcessLineChar(memoryStream, ref foundCR, ref foundCRLF, ref foundLF);
			}
			return DecodeLine(memoryStream, foundCRLF, foundLF);
		}

		public async Task<string> ReadLineAsync(int lengthLimit, CancellationToken cancellationToken)
		{
			CheckDisposed();
			using MemoryStream builder = new MemoryStream(200);
			bool foundCR = false;
			bool foundCRLF = false;
			bool foundLF = false;
			while (true)
			{
				bool flag = !(foundCRLF || foundLF);
				if (flag)
				{
					flag = await EnsureBufferedAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
				if (!flag)
				{
					break;
				}
				if (builder.Length > lengthLimit)
				{
					throw new InvalidDataException($"Line length limit {lengthLimit} exceeded.");
				}
				ProcessLineChar(builder, ref foundCR, ref foundCRLF, ref foundLF);
			}
			return DecodeLine(builder, foundCRLF, foundLF);
		}

		private void ProcessLineChar(MemoryStream builder, ref bool foundCR, ref bool foundCRLF, ref bool foundLF)
		{
			byte b = _buffer[_bufferOffset];
			builder.WriteByte(b);
			_bufferOffset++;
			_bufferCount--;
			if (b == 10)
			{
				foundLF = true;
				foundCRLF = foundCR & foundLF;
			}
			else
			{
				foundCR = b == 13;
			}
		}

		private static string DecodeLine(MemoryStream builder, bool foundCRLF, bool foundLF)
		{
			long num = (foundCRLF ? (builder.Length - 2) : ((!foundLF) ? builder.Length : (builder.Length - 1)));
			long num2 = num;
			return Encoding.UTF8.GetString(builder.ToArray(), 0, (int)num2);
		}

		private void CheckDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("BufferedReadStream");
			}
		}

		private static void ValidateBuffer(byte[] buffer, int offset, int count)
		{
			new ArraySegment<byte>(buffer, offset, count);
			if (count == 0)
			{
				throw new ArgumentOutOfRangeException("count", "The value must be greater than zero.");
			}
		}
	}
	internal struct KeyValueAccumulator
	{
		private Dictionary<string, string[]?> _accumulator;

		private Dictionary<string, List<string>> _expandingAccumulator;

		public bool HasValues => ValueCount > 0;

		public int KeyCount => _accumulator?.Count ?? 0;

		public int ValueCount { get; private set; }

		public void Append(string key, string value)
		{
			if (_accumulator == null)
			{
				_accumulator = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
			}
			if (_accumulator.TryGetValue(key, out string[] value2))
			{
				if (value2 != null && value2.Length == 0)
				{
					_expandingAccumulator[key].Add(value);
				}
				else if (value2 != null && value2.Length == 1)
				{
					_accumulator[key] = new string[2]
					{
						value2[0],
						value
					};
				}
				else if (value2 != null)
				{
					_accumulator[key] = null;
					if (_expandingAccumulator == null)
					{
						_expandingAccumulator = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
					}
					List<string> list = new List<string>(8);
					list.Add(value2[0]);
					list.Add(value2[1]);
					list.Add(value);
					_expandingAccumulator[key] = list;
				}
			}
			else
			{
				_accumulator[key] = new string[1] { value };
			}
			ValueCount++;
		}

		public Dictionary<string, string[]?> GetResults()
		{
			if (_expandingAccumulator != null)
			{
				foreach (KeyValuePair<string, List<string>> item in _expandingAccumulator)
				{
					_accumulator[item.Key] = item.Value.ToArray();
				}
			}
			return _accumulator ?? new Dictionary<string, string[]>(0, StringComparer.OrdinalIgnoreCase);
		}
	}
	internal class MultipartBoundary
	{
		private readonly int[] _skipTable = new int[256];

		private readonly string _boundary;

		private bool _expectLeadingCrlf;

		public bool ExpectLeadingCrlf
		{
			get
			{
				return _expectLeadingCrlf;
			}
			set
			{
				if (value != _expectLeadingCrlf)
				{
					_expectLeadingCrlf = value;
					Initialize(_boundary, _expectLeadingCrlf);
				}
			}
		}

		public byte[] BoundaryBytes { get; private set; }

		public int FinalBoundaryLength { get; private set; }

		public MultipartBoundary(string boundary, bool expectLeadingCrlf = true)
		{
			if (boundary == null)
			{
				throw new ArgumentNullException("boundary");
			}
			_boundary = boundary;
			_expectLeadingCrlf = expectLeadingCrlf;
			Initialize(_boundary, _expectLeadingCrlf);
		}

		private void Initialize(string boundary, bool expectLeadingCrlf)
		{
			if (expectLeadingCrlf)
			{
				BoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary);
			}
			else
			{
				BoundaryBytes = Encoding.UTF8.GetBytes("--" + boundary);
			}
			FinalBoundaryLength = BoundaryBytes.Length + 2;
			int num = BoundaryBytes.Length;
			for (int i = 0; i < _skipTable.Length; i++)
			{
				_skipTable[i] = num;
			}
			for (int j = 0; j < num; j++)
			{
				_skipTable[BoundaryBytes[j]] = Math.Max(1, num - 1 - j);
			}
		}

		public int GetSkipValue(byte input)
		{
			return _skipTable[input];
		}
	}
	internal class MultipartReader
	{
		public const int DefaultHeadersCountLimit = 16;

		public const int DefaultHeadersLengthLimit = 16384;

		private const int DefaultBufferSize = 4096;

		private readonly BufferedReadStream _stream;

		private readonly MultipartBoundary _boundary;

		private MultipartReaderStream _currentStream;

		public int HeadersCountLimit { get; set; } = 16;

		public int HeadersLengthLimit { get; set; } = 16384;

		public long? BodyLengthLimit { get; set; }

		public bool ExpectBoundariesWithCRLF { get; set; } = true;

		public MultipartReader(string boundary, Stream stream)
			: this(boundary, stream, 4096)
		{
		}

		public MultipartReader(string boundary, Stream stream, int bufferSize)
		{
			if (boundary == null)
			{
				throw new ArgumentNullException("boundary");
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (bufferSize < boundary.Length + 8)
			{
				throw new ArgumentOutOfRangeException("bufferSize", bufferSize, "Insufficient buffer space, the buffer must be larger than the boundary: " + boundary);
			}
			_stream = new BufferedReadStream(stream, bufferSize);
			_boundary = new MultipartBoundary(boundary, expectLeadingCrlf: false);
			_currentStream = new MultipartReaderStream(_stream, _boundary)
			{
				LengthLimit = HeadersLengthLimit
			};
		}

		public async Task<MultipartSection> ReadNextSectionAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			await _currentStream.DrainAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (_currentStream.FinalBoundaryFound)
			{
				await _stream.DrainAsync(HeadersLengthLimit, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				return null;
			}
			Dictionary<string, string[]> headers = await ReadHeadersAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			_boundary.ExpectLeadingCrlf = ExpectBoundariesWithCRLF;
			_currentStream = new MultipartReaderStream(_stream, _boundary)
			{
				LengthLimit = BodyLengthLimit
			};
			long? baseStreamOffset = (_stream.CanSeek ? new long?(_stream.Position) : ((long?)null));
			return new MultipartSection
			{
				Headers = headers,
				Body = _currentStream,
				BaseStreamOffset = baseStreamOffset
			};
		}

		private async Task<Dictionary<string, string[]>> ReadHeadersAsync(CancellationToken cancellationToken)
		{
			int totalSize = 0;
			KeyValueAccumulator accumulator = default(KeyValueAccumulator);
			string text = await _stream.ReadLineAsync(HeadersLengthLimit - totalSize, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			while (!string.IsNullOrEmpty(text))
			{
				if (HeadersLengthLimit - totalSize < text.Length)
				{
					throw new InvalidDataException($"Multipart headers length limit {HeadersLengthLimit} exceeded.");
				}
				totalSize += text.Length;
				int num = text.IndexOf(':');
				if (num <= 0)
				{
					throw new InvalidDataException("Invalid header line: " + text);
				}
				string key = text.Substring(0, num);
				string value = text.Substring(num + 1, text.Length - num - 1).Trim();
				accumulator.Append(key, value);
				if (accumulator.KeyCount > HeadersCountLimit)
				{
					throw new InvalidDataException($"Multipart headers count limit {HeadersCountLimit} exceeded.");
				}
				text = await _stream.ReadLineAsync(HeadersLengthLimit - totalSize, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			return accumulator.GetResults();
		}
	}
	internal sealed class MultipartReaderStream : Stream
	{
		private readonly MultipartBoundary _boundary;

		private readonly BufferedReadStream _innerStream;

		private readonly ArrayPool<byte> _bytePool;

		private readonly long _innerOffset;

		private long _position;

		private long _observedLength;

		private bool _finished;

		public bool FinalBoundaryFound { get; private set; }

		public long? LengthLimit { get; set; }

		public override bool CanRead => true;

		public override bool CanSeek => _innerStream.CanSeek;

		public override bool CanWrite => false;

		public override long Length => _observedLength;

		public override long Position
		{
			get
			{
				return _position;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", value, "The Position must be positive.");
				}
				if (value > _observedLength)
				{
					throw new ArgumentOutOfRangeException("value", value, "The Position must be less than length.");
				}
				_position = value;
				if (_position < _observedLength)
				{
					_finished = false;
				}
			}
		}

		public MultipartReaderStream(BufferedReadStream stream, MultipartBoundary boundary)
			: this(stream, boundary, ArrayPool<byte>.Shared)
		{
		}

		public MultipartReaderStream(BufferedReadStream stream, MultipartBoundary boundary, ArrayPool<byte> bytePool)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (boundary == null)
			{
				throw new ArgumentNullException("boundary");
			}
			_bytePool = bytePool;
			_innerStream = stream;
			_innerOffset = (_innerStream.CanSeek ? _innerStream.Position : 0);
			_boundary = boundary;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				Position = offset;
				break;
			case SeekOrigin.Current:
				Position += offset;
				break;
			default:
				Position = Length + offset;
				break;
			}
			return Position;
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		public override void Flush()
		{
			throw new NotSupportedException();
		}

		private void PositionInnerStream()
		{
			if (_innerStream.CanSeek && _innerStream.Position != _innerOffset + _position)
			{
				_innerStream.Position = _innerOffset + _position;
			}
		}

		private int UpdatePosition(int read)
		{
			_position += read;
			if (_observedLength < _position)
			{
				_observedLength = _position;
				if (LengthLimit.HasValue && _observedLength > LengthLimit.GetValueOrDefault())
				{
					throw new InvalidDataException($"Multipart body length limit {LengthLimit.GetValueOrDefault()} exceeded.");
				}
			}
			return read;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (_finished)
			{
				return 0;
			}
			PositionInnerStream();
			if (!_innerStream.EnsureBuffered(_boundary.FinalBoundaryLength))
			{
				throw new IOException("Unexpected end of Stream, the content may have already been read by another component. ");
			}
			ArraySegment<byte> bufferedData = _innerStream.BufferedData;
			int read;
			if (SubMatch(bufferedData, _boundary.BoundaryBytes, out var matchOffset, out var _))
			{
				if (matchOffset > bufferedData.Offset)
				{
					read = _innerStream.Read(buffer, offset, Math.Min(count, matchOffset - bufferedData.Offset));
					return UpdatePosition(read);
				}
				int num = _boundary.BoundaryBytes.Length;
				byte[] array = _bytePool.Rent(num);
				read = _innerStream.Read(array, 0, num);
				_bytePool.Return(array);
				string text = _innerStream.ReadLine(100);
				text = text.Trim();
				if (string.Equals("--", text, StringComparison.Ordinal))
				{
					FinalBoundaryFound = true;
				}
				_finished = true;
				return 0;
			}
			read = _innerStream.Read(buffer, offset, Math.Min(count, bufferedData.Count));
			return UpdatePosition(read);
		}

		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (_finished)
			{
				return 0;
			}
			PositionInnerStream();
			if (!(await _innerStream.EnsureBufferedAsync(_boundary.FinalBoundaryLength, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
			{
				throw new IOException("Unexpected end of Stream, the content may have already been read by another component. ");
			}
			ArraySegment<byte> bufferedData = _innerStream.BufferedData;
			int read;
			if (SubMatch(bufferedData, _boundary.BoundaryBytes, out var matchOffset, out var _))
			{
				if (matchOffset > bufferedData.Offset)
				{
					read = _innerStream.Read(buffer, offset, Math.Min(count, matchOffset - bufferedData.Offset));
					return UpdatePosition(read);
				}
				int num = _boundary.BoundaryBytes.Length;
				byte[] array = _bytePool.Rent(num);
				_innerStream.Read(array, 0, num);
				_bytePool.Return(array);
				string b = (await _innerStream.ReadLineAsync(100, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).Trim();
				if (string.Equals("--", b, StringComparison.Ordinal))
				{
					FinalBoundaryFound = true;
				}
				_finished = true;
				return 0;
			}
			read = _innerStream.Read(buffer, offset, Math.Min(count, bufferedData.Count));
			return UpdatePosition(read);
		}

		private bool SubMatch(ArraySegment<byte> segment1, byte[] matchBytes, out int matchOffset, out int matchCount)
		{
			matchCount = 0;
			int num = matchBytes.Length - 1;
			byte b = matchBytes[num];
			int num2 = segment1.Offset + segment1.Count - matchBytes.Length;
			matchOffset = segment1.Offset;
			while (matchOffset < num2)
			{
				byte b2 = segment1.Array[matchOffset + num];
				if (b2 == b && CompareBuffers(segment1.Array, matchOffset, matchBytes, 0, num) == 0)
				{
					matchCount = matchBytes.Length;
					return true;
				}
				matchOffset += _boundary.GetSkipValue(b2);
			}
			int num3 = segment1.Offset + segment1.Count;
			matchCount = 0;
			while (matchOffset < num3)
			{
				int num4 = num3 - matchOffset;
				matchCount = 0;
				while (matchCount < matchBytes.Length && matchCount < num4)
				{
					if (matchBytes[matchCount] != segment1.Array[matchOffset + matchCount])
					{
						matchCount = 0;
						break;
					}
					matchCount++;
				}
				if (matchCount > 0)
				{
					break;
				}
				matchOffset++;
			}
			return matchCount > 0;
		}

		private static int CompareBuffers(byte[] buffer1, int offset1, byte[] buffer2, int offset2, int count)
		{
			while (count-- > 0)
			{
				if (buffer1[offset1] != buffer2[offset2])
				{
					return buffer1[offset1] - buffer2[offset2];
				}
				offset1++;
				offset2++;
			}
			return 0;
		}
	}
	internal class MultipartSection
	{
		public Dictionary<string, string[]>? Headers { get; set; }

		public Stream Body { get; set; }

		public long? BaseStreamOffset { get; set; }
	}
	internal static class StreamHelperExtensions
	{
		private const int _maxReadBufferSize = 4096;

		public static Task DrainAsync(this Stream stream, CancellationToken cancellationToken)
		{
			return stream.DrainAsync(ArrayPool<byte>.Shared, null, cancellationToken);
		}

		public static Task DrainAsync(this Stream stream, long? limit, CancellationToken cancellationToken)
		{
			return stream.DrainAsync(ArrayPool<byte>.Shared, limit, cancellationToken);
		}

		public static async Task DrainAsync(this Stream stream, ArrayPool<byte> bytePool, long? limit, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			byte[] buffer = bytePool.Rent(4096);
			long total = 0L;
			try
			{
				for (int num = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(continueOnCapturedContext: false); num > 0; num = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
				{
					cancellationToken.ThrowIfCancellationRequested();
					if (limit.HasValue && limit.GetValueOrDefault() - total < num)
					{
						throw new InvalidDataException($"The stream exceeded the data limit {limit.GetValueOrDefault()}.");
					}
					total += num;
				}
			}
			finally
			{
				bytePool.Return(buffer);
			}
		}
	}
	public static class MultipartResponse
	{
		private const int KB = 1024;

		private const int ResponseLineSize = 4096;

		private const string MultipartContentTypePrefix = "multipart/mixed; boundary=";

		private const string ContentIdName = "Content-ID";

		internal static InvalidOperationException InvalidBatchContentType(string contentType)
		{
			return new InvalidOperationException("Expected " + HttpHeader.Names.ContentType + " to start with multipart/mixed; boundary= but received " + contentType);
		}

		internal static InvalidOperationException InvalidHttpStatusLine(string statusLine)
		{
			return new InvalidOperationException("Expected an HTTP status line, not " + statusLine);
		}

		internal static InvalidOperationException InvalidHttpHeaderLine(string headerLine)
		{
			return new InvalidOperationException("Expected an HTTP header line, not " + headerLine);
		}

		public static Response[] Parse(Response response, bool expectCrLf, CancellationToken cancellationToken)
		{
			return ParseAsync(response, expectCrLf, async: false, cancellationToken).EnsureCompleted();
		}

		public static async Task<Response[]> ParseAsync(Response response, bool expectCrLf, CancellationToken cancellationToken)
		{
			return await ParseAsync(response, expectCrLf, async: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		internal static async Task<Response[]> ParseAsync(Response parentResponse, bool expectBoundariesWithCRLF, bool async, CancellationToken cancellationToken)
		{
			Stream contentStream = parentResponse.ContentStream;
			string contentType = parentResponse.Headers.ContentType;
			if (!GetBoundary(contentType, out var batchBoundary))
			{
				throw InvalidBatchContentType(contentType);
			}
			Dictionary<int, Response> responses = new Dictionary<int, Response>();
			List<Response> responsesWithoutId = new List<Response>();
			MultipartReader reader = new MultipartReader(batchBoundary, contentStream)
			{
				ExpectBoundariesWithCRLF = expectBoundariesWithCRLF
			};
			for (MultipartSection multipartSection = await reader.GetNextSectionAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false); multipartSection != null; multipartSection = await reader.GetNextSectionAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
			{
				bool flag = true;
				if (multipartSection.Headers.TryGetValue(HttpHeader.Names.ContentType, out string[] value) && value.Length == 1 && GetBoundary(value[0], out var batchBoundary2))
				{
					reader = new MultipartReader(batchBoundary2, multipartSection.Body)
					{
						ExpectBoundariesWithCRLF = true
					};
				}
				else
				{
					if (!multipartSection.Headers.TryGetValue("Content-ID", out string[] value2) || value2.Length != 1 || !int.TryParse(value2[0], out var result))
					{
						result = 0;
						flag = false;
					}
					MemoryResponse response = new MemoryResponse
					{
						RequestFailedDetailsParser = parentResponse.RequestFailedDetailsParser,
						Sanitizer = parentResponse.Sanitizer
					};
					if (flag)
					{
						responses[result] = response;
					}
					else
					{
						responsesWithoutId.Add(response);
					}
					using BufferedReadStream body = new BufferedReadStream(multipartSection.Body, 4096);
					string text = await body.ReadLineAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					string[] array = text.Split(new char[1] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
					if (array.Length != 3)
					{
						throw InvalidHttpStatusLine(text);
					}
					response.SetStatus(int.Parse(array[1], CultureInfo.InvariantCulture));
					response.SetReasonPhrase(array[2]);
					text = await body.ReadLineAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					while (!string.IsNullOrEmpty(text))
					{
						int num = text.IndexOf(':');
						if (num <= 0)
						{
							throw InvalidHttpHeaderLine(text);
						}
						string name = text.Substring(0, num);
						string value3 = text.Substring(num + 1, text.Length - num - 1).Trim();
						response.AddHeader(name, value3);
						text = await body.ReadLineAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					}
					MemoryStream responseContent = new MemoryStream();
					if (async)
					{
						await body.CopyToAsync(responseContent, (int)body.Length, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					}
					else
					{
						body.CopyTo(responseContent);
					}
					responseContent.Seek(0L, SeekOrigin.Begin);
					response.ContentStream = responseContent;
				}
			}
			Response[] array2 = new Response[responses.Count + responsesWithoutId.Count];
			for (int i = 0; i < responses.Count; i++)
			{
				array2[i] = responses[i];
			}
			for (int j = responses.Count; j < array2.Length; j++)
			{
				array2[j] = responsesWithoutId[j - responses.Count];
			}
			return array2;
		}

		internal static async Task<string> ReadLineAsync(this BufferedReadStream stream, bool async, CancellationToken cancellationToken)
		{
			return (!async) ? stream.ReadLine(4096) : (await stream.ReadLineAsync(4096, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		internal static async Task<MultipartSection> GetNextSectionAsync(this MultipartReader reader, bool async, CancellationToken cancellationToken)
		{
			return (!async) ? reader.ReadNextSectionAsync(cancellationToken).GetAwaiter().GetResult() : (await reader.ReadNextSectionAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		private static bool GetBoundary(string contentType, out string batchBoundary)
		{
			if (contentType == null || !contentType.StartsWith("multipart/mixed; boundary=", StringComparison.Ordinal))
			{
				batchBoundary = null;
				return false;
			}
			batchBoundary = contentType.Substring("multipart/mixed; boundary=".Length);
			return true;
		}
	}
	public abstract class RequestFailedDetailsParser
	{
		public abstract bool TryParse(Response response, out ResponseError? error, out IDictionary<string, string>? data);
	}
	public readonly struct RehydrationToken : IJsonModel<RehydrationToken>, IPersistableModel<RehydrationToken>, IJsonModel<object>, IPersistableModel<object>
	{
		public string Id { get; }

		internal string Version { get; }

		internal string HeaderSource { get; }

		internal string NextRequestUri { get; }

		internal string InitialUri { get; }

		internal RequestMethod RequestMethod { get; }

		internal string? LastKnownLocation { get; }

		internal string FinalStateVia { get; }

		internal RehydrationToken(string id, string? version, string headerSource, string nextRequestUri, string initialUri, RequestMethod requestMethod, string? lastKnownLocation, string finalStateVia)
		{
			Id = "NOT_SET";
			Version = "1.0.0";
			Id = id;
			if (version != null)
			{
				Version = version;
			}
			HeaderSource = headerSource;
			NextRequestUri = nextRequestUri;
			InitialUri = initialUri;
			RequestMethod = requestMethod;
			LastKnownLocation = lastKnownLocation;
			FinalStateVia = finalStateVia;
		}

		internal RehydrationToken DeserializeRehydrationToken(JsonElement element, ModelReaderWriterOptions options)
		{
			string text = ((options.Format == "W") ? ((IPersistableModel<RehydrationToken>)this).GetFormatFromOptions(options) : options.Format);
			if (text != "J")
			{
				throw new FormatException("The model RehydrationToken does not support '" + text + "' format.");
			}
			if (element.ValueKind == JsonValueKind.Null)
			{
				throw new InvalidOperationException("Cannot deserialize a null value to a non-nullable RehydrationToken");
			}
			string id = "NOT_SET";
			string version = string.Empty;
			string headerSource = string.Empty;
			string nextRequestUri = string.Empty;
			string initialUri = string.Empty;
			RequestMethod requestMethod = default(RequestMethod);
			string lastKnownLocation = null;
			string finalStateVia = string.Empty;
			foreach (JsonProperty item in element.EnumerateObject())
			{
				if (item.NameEquals("id"u8))
				{
					if (item.Value.ValueKind == JsonValueKind.Null)
					{
						continue;
					}
					id = item.Value.GetString();
				}
				if (item.NameEquals("version"u8))
				{
					version = item.Value.GetString();
				}
				else if (item.NameEquals("headerSource"u8))
				{
					headerSource = item.Value.GetString();
				}
				else if (item.NameEquals("nextRequestUri"u8))
				{
					nextRequestUri = item.Value.GetString();
				}
				else if (item.NameEquals("initialUri"u8))
				{
					initialUri = item.Value.GetString();
				}
				else if (item.NameEquals("requestMethod"u8))
				{
					string method = item.Value.GetString();
					requestMethod = new RequestMethod(method);
				}
				else if (item.NameEquals("lastKnownLocation"u8))
				{
					if (item.Value.ValueKind != JsonValueKind.Null)
					{
						lastKnownLocation = item.Value.GetString();
					}
				}
				else if (item.NameEquals("finalStateVia"u8))
				{
					finalStateVia = item.Value.GetString();
				}
			}
			return new RehydrationToken(id, version, headerSource, nextRequestUri, initialUri, requestMethod, lastKnownLocation, finalStateVia);
		}

		void IJsonModel<RehydrationToken>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("id"u8);
			writer.WriteStringValue(Id);
			writer.WritePropertyName("version"u8);
			writer.WriteStringValue(Version);
			writer.WritePropertyName("headerSource"u8);
			writer.WriteStringValue(HeaderSource);
			writer.WritePropertyName("nextRequestUri"u8);
			writer.WriteStringValue(NextRequestUri);
			writer.WritePropertyName("initialUri"u8);
			writer.WriteStringValue(InitialUri);
			writer.WritePropertyName("requestMethod"u8);
			writer.WriteStringValue(RequestMethod.ToString());
			writer.WritePropertyName("lastKnownLocation"u8);
			writer.WriteStringValue(LastKnownLocation);
			writer.WritePropertyName("finalStateVia"u8);
			writer.WriteStringValue(FinalStateVia);
			writer.WriteEndObject();
		}

		RehydrationToken IJsonModel<RehydrationToken>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
		{
			string text = ((options.Format == "W") ? ((IPersistableModel<RehydrationToken>)this).GetFormatFromOptions(options) : options.Format);
			if (text != "J")
			{
				throw new FormatException("The model RehydrationToken does not support '" + text + "' format.");
			}
			using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);
			return DeserializeRehydrationToken(jsonDocument.RootElement, options);
		}

		BinaryData IPersistableModel<RehydrationToken>.Write(ModelReaderWriterOptions options)
		{
			if (((options.Format == "W") ? ((IPersistableModel<RehydrationToken>)this).GetFormatFromOptions(options) : options.Format) == "J")
			{
				return ModelReaderWriter.Write(this, options);
			}
			throw new FormatException("The model RehydrationToken does not support '" + options.Format + "' format.");
		}

		RehydrationToken IPersistableModel<RehydrationToken>.Create(BinaryData data, ModelReaderWriterOptions options)
		{
			if (((options.Format == "W") ? ((IPersistableModel<RehydrationToken>)this).GetFormatFromOptions(options) : options.Format) == "J")
			{
				using (JsonDocument jsonDocument = JsonDocument.Parse(data))
				{
					return DeserializeRehydrationToken(jsonDocument.RootElement, options);
				}
			}
			throw new FormatException("The model RehydrationToken does not support '" + options.Format + "' format.");
		}

		string IPersistableModel<RehydrationToken>.GetFormatFromOptions(ModelReaderWriterOptions options)
		{
			return "J";
		}

		BinaryData IPersistableModel<object>.Write(ModelReaderWriterOptions options)
		{
			return ((IPersistableModel<RehydrationToken>)this).Write(options);
		}

		object IPersistableModel<object>.Create(BinaryData data, ModelReaderWriterOptions options)
		{
			return ((IPersistableModel<RehydrationToken>)this).Create(data, options);
		}

		string IPersistableModel<object>.GetFormatFromOptions(ModelReaderWriterOptions options)
		{
			return "J";
		}

		void IJsonModel<object>.Write(Utf8JsonWriter writer, ModelReaderWriterOptions options)
		{
			((IJsonModel<RehydrationToken>)this).Write(writer, options);
		}

		object IJsonModel<object>.Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options)
		{
			return ((IJsonModel<RehydrationToken>)this).Create(ref reader, options);
		}
	}
	public abstract class Request : IDisposable
	{
		private RequestUriBuilder? _uri;

		public virtual RequestUriBuilder Uri
		{
			get
			{
				return _uri ?? (_uri = new RequestUriBuilder());
			}
			set
			{
				Argument.AssertNotNull(value, "value");
				_uri = value;
			}
		}

		public virtual RequestMethod Method { get; set; }

		public virtual RequestContent? Content { get; set; }

		public abstract string ClientRequestId { get; set; }

		public RequestHeaders Headers => new RequestHeaders(this);

		protected internal abstract void AddHeader(string name, string value);

		protected internal abstract bool TryGetHeader(string name, [NotNullWhen(true)] out string? value);

		protected internal abstract bool TryGetHeaderValues(string name, [NotNullWhen(true)] out IEnumerable<string>? values);

		protected internal abstract bool ContainsHeader(string name);

		protected internal virtual void SetHeader(string name, string value)
		{
			RemoveHeader(name);
			AddHeader(name, value);
		}

		protected internal abstract bool RemoveHeader(string name);

		protected internal abstract IEnumerable<HttpHeader> EnumerateHeaders();

		public abstract void Dispose();
	}
	public abstract class RequestContent : IDisposable
	{
		private sealed class StreamContent : RequestContent
		{
			private const int CopyToBufferSize = 81920;

			private readonly Stream _stream;

			private readonly long _origin;

			public StreamContent(Stream stream)
			{
				if (!stream.CanSeek)
				{
					throw new ArgumentException("stream must be seekable", "stream");
				}
				_origin = stream.Position;
				_stream = stream;
			}

			public override void WriteTo(Stream stream, CancellationToken cancellationToken)
			{
				_stream.Seek(_origin, SeekOrigin.Begin);
				byte[] array = ArrayPool<byte>.Shared.Rent(81920);
				try
				{
					while (true)
					{
						CancellationHelper.ThrowIfCancellationRequested(cancellationToken);
						int num = _stream.Read(array, 0, array.Length);
						if (num == 0)
						{
							break;
						}
						CancellationHelper.ThrowIfCancellationRequested(cancellationToken);
						stream.Write(array, 0, num);
					}
				}
				finally
				{
					stream.Flush();
					ArrayPool<byte>.Shared.Return(array, clearArray: true);
				}
			}

			public override bool TryComputeLength(out long length)
			{
				if (_stream.CanSeek)
				{
					length = _stream.Length - _origin;
					return true;
				}
				length = 0L;
				return false;
			}

			public override async Task WriteToAsync(Stream stream, CancellationToken cancellation)
			{
				_stream.Seek(_origin, SeekOrigin.Begin);
				await _stream.CopyToAsync(stream, 81920, cancellation).ConfigureAwait(continueOnCapturedContext: false);
			}

			public override void Dispose()
			{
				_stream.Dispose();
			}
		}

		private sealed class ArrayContent : RequestContent
		{
			private readonly byte[] _bytes;

			private readonly int _contentStart;

			private readonly int _contentLength;

			public ArrayContent(byte[] bytes, int index, int length)
			{
				_bytes = bytes;
				_contentStart = index;
				_contentLength = length;
			}

			public override void Dispose()
			{
			}

			public override void WriteTo(Stream stream, CancellationToken cancellation)
			{
				stream.Write(_bytes, _contentStart, _contentLength);
			}

			public override bool TryComputeLength(out long length)
			{
				length = _contentLength;
				return true;
			}

			public override async Task WriteToAsync(Stream stream, CancellationToken cancellation)
			{
				await stream.WriteAsync(_bytes, _contentStart, _contentLength, cancellation).ConfigureAwait(continueOnCapturedContext: false);
			}
		}

		private sealed class MemoryContent : RequestContent
		{
			private readonly ReadOnlyMemory<byte> _bytes;

			public MemoryContent(ReadOnlyMemory<byte> bytes)
			{
				_bytes = bytes;
			}

			public override void Dispose()
			{
			}

			public override void WriteTo(Stream stream, CancellationToken cancellation)
			{
				byte[] array = _bytes.ToArray();
				stream.Write(array, 0, array.Length);
			}

			public override bool TryComputeLength(out long length)
			{
				length = _bytes.Length;
				return true;
			}

			public override async Task WriteToAsync(Stream stream, CancellationToken cancellation)
			{
				await AzureBaseBuffersExtensions.WriteAsync(stream, _bytes, cancellation).ConfigureAwait(continueOnCapturedContext: false);
			}
		}

		private sealed class ReadOnlySequenceContent : RequestContent
		{
			private readonly ReadOnlySequence<byte> _bytes;

			public ReadOnlySequenceContent(ReadOnlySequence<byte> bytes)
			{
				_bytes = bytes;
			}

			public override void Dispose()
			{
			}

			public override void WriteTo(Stream stream, CancellationToken cancellation)
			{
				byte[] array = BuffersExtensions.ToArray(in _bytes);
				stream.Write(array, 0, array.Length);
			}

			public override bool TryComputeLength(out long length)
			{
				length = _bytes.Length;
				return true;
			}

			public override async Task WriteToAsync(Stream stream, CancellationToken cancellation)
			{
				await stream.WriteAsync(_bytes, cancellation).ConfigureAwait(continueOnCapturedContext: false);
			}
		}

		private sealed class DynamicDataContent : RequestContent
		{
			private readonly DynamicData _data;

			public DynamicDataContent(DynamicData data)
			{
				_data = data;
			}

			public override void Dispose()
			{
				_data.Dispose();
			}

			public override void WriteTo(Stream stream, CancellationToken cancellation)
			{
				_data.WriteTo(stream);
			}

			public override bool TryComputeLength(out long length)
			{
				length = 0L;
				return false;
			}

			public override Task WriteToAsync(Stream stream, CancellationToken cancellation)
			{
				_data.WriteTo(stream);
				return Task.CompletedTask;
			}
		}

		internal const string SerializationRequiresUnreferencedCode = "This method uses reflection-based serialization which is incompatible with trimming. Try using one of the 'Create' overloads that doesn't wrap a serialized version of an object.";

		private static readonly Encoding s_UTF8NoBomEncoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

		public static RequestContent Create(Stream stream)
		{
			return new StreamContent(stream);
		}

		public static RequestContent Create(byte[] bytes)
		{
			return new ArrayContent(bytes, 0, bytes.Length);
		}

		public static RequestContent Create(byte[] bytes, int index, int length)
		{
			return new ArrayContent(bytes, index, length);
		}

		public static RequestContent Create(ReadOnlyMemory<byte> bytes)
		{
			return new MemoryContent(bytes);
		}

		public static RequestContent Create(ReadOnlySequence<byte> bytes)
		{
			return new ReadOnlySequenceContent(bytes);
		}

		public static RequestContent Create(string content)
		{
			return Create(s_UTF8NoBomEncoding.GetBytes(content));
		}

		public static RequestContent Create(BinaryData content)
		{
			return new MemoryContent(content.ToMemory());
		}

		public static RequestContent Create(DynamicData content)
		{
			return new DynamicDataContent(content);
		}

		[RequiresUnreferencedCode("This method uses reflection-based serialization which is incompatible with trimming. Try using one of the 'Create' overloads that doesn't wrap a serialized version of an object.")]
		[RequiresDynamicCode("This method uses reflection-based serialization which is incompatible with trimming. Try using one of the 'Create' overloads that doesn't wrap a serialized version of an object.")]
		public static RequestContent Create(object serializable)
		{
			return Create(serializable, JsonObjectSerializer.Default);
		}

		[RequiresUnreferencedCode("This method uses reflection-based serialization which is incompatible with trimming. Try using one of the 'Create' overloads that doesn't wrap a serialized version of an object.")]
		[RequiresDynamicCode("This method uses reflection-based serialization which is incompatible with trimming. Try using one of the 'Create' overloads that doesn't wrap a serialized version of an object.")]
		public static RequestContent Create(object serializable, ObjectSerializer? serializer)
		{
			return Create((serializer ?? JsonObjectSerializer.Default).Serialize(serializable));
		}

		[RequiresUnreferencedCode("This method uses reflection-based serialization which is incompatible with trimming. Try using one of the 'Create' overloads that doesn't wrap a serialized version of an object.")]
		[RequiresDynamicCode("This method uses reflection-based serialization which is incompatible with trimming. Try using one of the 'Create' overloads that doesn't wrap a serialized version of an object.")]
		public static RequestContent Create(object serializable, JsonPropertyNames propertyNameFormat, string dateTimeFormat = "o")
		{
			return Create(new JsonObjectSerializer(DynamicDataOptions.ToSerializerOptions(new DynamicDataOptions
			{
				PropertyNameFormat = propertyNameFormat,
				DateTimeFormat = dateTimeFormat
			})).Serialize(serializable));
		}

		public static implicit operator RequestContent(string content)
		{
			return Create(content);
		}

		public static implicit operator RequestContent(BinaryData content)
		{
			return Create(content);
		}

		public static implicit operator RequestContent(DynamicData content)
		{
			return Create(content);
		}

		public abstract Task WriteToAsync(Stream stream, CancellationToken cancellation);

		public abstract void WriteTo(Stream stream, CancellationToken cancellation);

		public abstract bool TryComputeLength(out long length);

		public abstract void Dispose();
	}
	public readonly struct RequestHeaders : IEnumerable<HttpHeader>, IEnumerable
	{
		private readonly Request _request;

		internal RequestHeaders(Request request)
		{
			_request = request;
		}

		public IEnumerator<HttpHeader> GetEnumerator()
		{
			return _request.EnumerateHeaders().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _request.EnumerateHeaders().GetEnumerator();
		}

		public void Add(HttpHeader header)
		{
			_request.AddHeader(header.Name, header.Value);
		}

		public void Add(string name, string value)
		{
			_request.AddHeader(name, value);
		}

		public bool TryGetValue(string name, [NotNullWhen(true)] out string? value)
		{
			return _request.TryGetHeader(name, out value);
		}

		public bool TryGetValues(string name, [NotNullWhen(true)] out IEnumerable<string>? values)
		{
			return _request.TryGetHeaderValues(name, out values);
		}

		public bool Contains(string name)
		{
			return _request.ContainsHeader(name);
		}

		public void SetValue(string name, string value)
		{
			_request.SetHeader(name, value);
		}

		public bool Remove(string name)
		{
			return _request.RemoveHeader(name);
		}
	}
	public readonly struct RequestMethod : IEquatable<RequestMethod>
	{
		public string Method { get; }

		public static RequestMethod Get { get; } = new RequestMethod("GET");

		public static RequestMethod Post { get; } = new RequestMethod("POST");

		public static RequestMethod Put { get; } = new RequestMethod("PUT");

		public static RequestMethod Patch { get; } = new RequestMethod("PATCH");

		public static RequestMethod Delete { get; } = new RequestMethod("DELETE");

		public static RequestMethod Head { get; } = new RequestMethod("HEAD");

		public static RequestMethod Options { get; } = new RequestMethod("OPTIONS");

		public static RequestMethod Trace { get; } = new RequestMethod("TRACE");

		public RequestMethod(string method)
		{
			Argument.AssertNotNull(method, "method");
			Method = method.ToUpperInvariant();
		}

		public static RequestMethod Parse(string method)
		{
			Argument.AssertNotNull(method, "method");
			if (method.Length == 3)
			{
				if (string.Equals(method, "GET", StringComparison.OrdinalIgnoreCase))
				{
					return Get;
				}
				if (string.Equals(method, "PUT", StringComparison.OrdinalIgnoreCase))
				{
					return Put;
				}
			}
			else if (method.Length == 4)
			{
				if (string.Equals(method, "POST", StringComparison.OrdinalIgnoreCase))
				{
					return Post;
				}
				if (string.Equals(method, "HEAD", StringComparison.OrdinalIgnoreCase))
				{
					return Head;
				}
			}
			else
			{
				if (string.Equals(method, "PATCH", StringComparison.OrdinalIgnoreCase))
				{
					return Patch;
				}
				if (string.Equals(method, "DELETE", StringComparison.OrdinalIgnoreCase))
				{
					return Delete;
				}
				if (string.Equals(method, "OPTIONS", StringComparison.OrdinalIgnoreCase))
				{
					return Options;
				}
				if (string.Equals(method, "TRACE", StringComparison.OrdinalIgnoreCase))
				{
					return Trace;
				}
			}
			return new RequestMethod(method);
		}

		public bool Equals(RequestMethod other)
		{
			return string.Equals(Method, other.Method, StringComparison.Ordinal);
		}

		public override bool Equals(object? obj)
		{
			if (obj is RequestMethod other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Method.GetHashCodeOrdinal();
		}

		public static bool operator ==(RequestMethod left, RequestMethod right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(RequestMethod left, RequestMethod right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			return Method ?? "<null>";
		}
	}
	public class RequestUriBuilder
	{
		private const char QuerySeparator = '?';

		private const char PathSeparator = '/';

		private readonly StringBuilder _pathAndQuery = new StringBuilder();

		private int _queryIndex = -1;

		private Uri? _uri;

		private int _port;

		private string? _host;

		private string? _scheme;

		public string? Scheme
		{
			get
			{
				return _scheme;
			}
			set
			{
				ResetUri();
				_scheme = value;
			}
		}

		public string? Host
		{
			get
			{
				return _host;
			}
			set
			{
				ResetUri();
				_host = value;
			}
		}

		public int Port
		{
			get
			{
				return _port;
			}
			set
			{
				ResetUri();
				_port = value;
			}
		}

		public string Query
		{
			get
			{
				if (!HasQuery)
				{
					return string.Empty;
				}
				return _pathAndQuery.ToString(_queryIndex, _pathAndQuery.Length - _queryIndex);
			}
			set
			{
				ResetUri();
				if (HasQuery)
				{
					_pathAndQuery.Remove(_queryIndex, _pathAndQuery.Length - _queryIndex);
					_queryIndex = -1;
				}
				if (!string.IsNullOrEmpty(value))
				{
					_queryIndex = _pathAndQuery.Length;
					if (value[0] != '?')
					{
						_pathAndQuery.Append('?');
					}
					_pathAndQuery.Append(value);
				}
			}
		}

		public string Path
		{
			get
			{
				if (!HasQuery)
				{
					return _pathAndQuery.ToString();
				}
				return _pathAndQuery.ToString(0, _queryIndex);
			}
			set
			{
				if (HasQuery)
				{
					_pathAndQuery.Remove(0, _queryIndex);
					_pathAndQuery.Insert(0, value);
					_queryIndex = value.Length;
				}
				else
				{
					_pathAndQuery.Remove(0, _pathAndQuery.Length);
					_pathAndQuery.Append(value);
				}
			}
		}

		protected bool HasPath => PathLength > 0;

		protected bool HasQuery => _queryIndex != -1;

		private int PathLength
		{
			get
			{
				if (!HasQuery)
				{
					return _pathAndQuery.Length;
				}
				return _queryIndex;
			}
		}

		private int QueryLength
		{
			get
			{
				if (!HasQuery)
				{
					return 0;
				}
				return _pathAndQuery.Length - _queryIndex;
			}
		}

		public string PathAndQuery => _pathAndQuery.ToString();

		private bool HasDefaultPortForScheme
		{
			get
			{
				if (Port != 80 || !string.Equals(Scheme, "http", StringComparison.InvariantCultureIgnoreCase))
				{
					if (Port == 443)
					{
						return string.Equals(Scheme, "https", StringComparison.InvariantCultureIgnoreCase);
					}
					return false;
				}
				return true;
			}
		}

		public void Reset(Uri value)
		{
			Scheme = value.Scheme;
			Host = value.Host;
			Port = value.Port;
			Path = value.AbsolutePath;
			Query = value.Query;
			_uri = value;
		}

		public Uri ToUri()
		{
			if (_uri == null)
			{
				_uri = new Uri(ToString());
			}
			return _uri;
		}

		public void AppendQuery(string name, string value)
		{
			AppendQuery(name, value, escapeValue: true);
		}

		public void AppendQuery(string name, string value, bool escapeValue)
		{
			if (escapeValue && !string.IsNullOrEmpty(value))
			{
				value = Uri.EscapeDataString(value);
			}
			AppendQuery(MemoryExtensions.AsSpan(name), MemoryExtensions.AsSpan(value), escapeValue: false);
		}

		public void AppendQuery(ReadOnlySpan<char> name, ReadOnlySpan<char> value, bool escapeValue)
		{
			ResetUri();
			if (!HasQuery)
			{
				_queryIndex = _pathAndQuery.Length;
				_pathAndQuery.Append('?');
			}
			else if (QueryLength != 1 || _pathAndQuery[_queryIndex] != '?')
			{
				_pathAndQuery.Append('&');
			}
			_pathAndQuery.Append(name.ToString());
			_pathAndQuery.Append('=');
			if (escapeValue && !value.IsEmpty)
			{
				_pathAndQuery.Append(Uri.EscapeDataString(value.ToString()));
			}
			else
			{
				_pathAndQuery.Append(value.ToString());
			}
		}

		public void AppendPath(string value)
		{
			AppendPath(value, escape: true);
		}

		public void AppendPath(string value, bool escape)
		{
			if (!string.IsNullOrEmpty(value))
			{
				AppendPath(MemoryExtensions.AsSpan(value), escape);
			}
		}

		public void AppendPath(ReadOnlySpan<char> value, bool escape)
		{
			if (!value.IsEmpty)
			{
				ResetUri();
				int start = 0;
				if (PathLength == 1 && _pathAndQuery[0] == '/' && value[0] == '/')
				{
					start = 1;
				}
				string text = value.Slice(start).ToString();
				if (escape)
				{
					text = Uri.EscapeDataString(text);
				}
				if (HasQuery)
				{
					_pathAndQuery.Insert(_queryIndex, text);
					_queryIndex += text.Length;
				}
				else
				{
					_pathAndQuery.Append(text);
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder((Scheme?.Length ?? 0) + 3 + (Host?.Length ?? 0) + _pathAndQuery.Length + 10);
			stringBuilder.Append(Scheme);
			stringBuilder.Append("://");
			stringBuilder.Append(Host);
			if (!HasDefaultPortForScheme)
			{
				stringBuilder.Append(':');
				stringBuilder.Append(Port);
			}
			if (_pathAndQuery.Length == 0 || _pathAndQuery[0] != '/')
			{
				stringBuilder.Append('/');
			}
			stringBuilder.Append((object)_pathAndQuery);
			return stringBuilder.ToString();
		}

		private void ResetUri()
		{
			_uri = null;
		}
	}
	public sealed class ResourceIdentifier : IEquatable<ResourceIdentifier>, IComparable<ResourceIdentifier>
	{
		private enum SpecialType
		{
			None,
			Subscription,
			ResourceGroup,
			Location,
			Provider
		}

		private readonly struct ResourceIdentifierParts
		{
			public ResourceIdentifier Parent { get; }

			public ResourceType ResourceType { get; }

			public string ResourceName { get; }

			public bool IsProviderResource { get; }

			public SpecialType SpecialType { get; }

			public ResourceIdentifierParts(ResourceIdentifier parent, ResourceType resourceType, string resourceName, bool isProviderResource, SpecialType specialType)
			{
				Parent = parent;
				ResourceType = resourceType;
				ResourceName = resourceName;
				IsProviderResource = isProviderResource;
				SpecialType = specialType;
			}
		}

		internal const char Separator = '/';

		private const string RootStringValue = "/";

		private const string ProvidersKey = "providers";

		private const string SubscriptionsKey = "subscriptions";

		private const string LocationsKey = "locations";

		private const string ResourceGroupKey = "resourcegroups";

		private const string SubscriptionStart = "/subscriptions/";

		private const string ProviderStart = "/providers/";

		private bool _initialized;

		private string? _stringValue;

		private ResourceType _resourceType;

		private string _name;

		private ResourceIdentifier? _parent;

		private bool _isProviderResource;

		private string? _subscriptionId;

		private string? _provider;

		private AzureLocation? _location;

		private string? _resourceGroupName;

		public static readonly ResourceIdentifier Root = new ResourceIdentifier(null, ResourceType.Tenant, string.Empty, isProviderResource: false, SpecialType.None);

		private string StringValue => _stringValue ?? (_stringValue = ToResourceString());

		public ResourceType ResourceType => GetValue(ref _resourceType);

		public string Name => GetValue(ref _name);

		public ResourceIdentifier? Parent => GetValue(ref _parent);

		internal bool IsProviderResource => GetValue(ref _isProviderResource);

		public string? SubscriptionId => GetValue(ref _subscriptionId);

		public string? Provider => GetValue(ref _provider);

		public AzureLocation? Location => GetValue(ref _location);

		public string? ResourceGroupName => GetValue(ref _resourceGroupName);

		public ResourceIdentifier(string resourceId)
		{
			Argument.AssertNotNullOrEmpty(resourceId, "resourceId");
			_stringValue = resourceId;
			if (resourceId.Length == 1 && resourceId[0] == '/')
			{
				Init(null, ResourceType.Tenant, string.Empty, isProviderResource: false, SpecialType.None);
			}
		}

		private ResourceIdentifier(ResourceIdentifier? parent, ResourceType resourceType, string resourceName, bool isProviderResource, SpecialType specialType)
		{
			Init(parent, resourceType, resourceName, isProviderResource, specialType);
		}

		private void Init(ResourceIdentifier? parent, ResourceType resourceType, string resourceName, bool isProviderResource, SpecialType specialType)
		{
			if ((object)parent != null)
			{
				_provider = parent.Provider;
				_subscriptionId = parent.SubscriptionId;
				_location = parent.Location;
				_resourceGroupName = parent.ResourceGroupName;
			}
			switch (specialType)
			{
			case SpecialType.ResourceGroup:
				_resourceGroupName = resourceName;
				break;
			case SpecialType.Subscription:
				_subscriptionId = resourceName;
				break;
			case SpecialType.Provider:
				_provider = resourceName;
				break;
			case SpecialType.Location:
				_location = resourceName;
				break;
			}
			_resourceType = resourceType;
			_name = resourceName;
			_isProviderResource = isProviderResource;
			_parent = parent;
			if ((object)parent == null)
			{
				_stringValue = "/";
			}
			_initialized = true;
		}

		private string? Parse()
		{
			ReadOnlySpan<char> remaining = MemoryExtensions.AsSpan(_stringValue);
			if (!remaining.StartsWith(MemoryExtensions.AsSpan("/subscriptions/"), StringComparison.OrdinalIgnoreCase) && !remaining.StartsWith(MemoryExtensions.AsSpan("/providers/"), StringComparison.OrdinalIgnoreCase))
			{
				return "The ResourceIdentifier must start with /subscriptions/ or /providers/.";
			}
			remaining = ((remaining[remaining.Length - 1] == '/') ? remaining.Slice(1, remaining.Length - 2) : remaining.Slice(1));
			ReadOnlySpan<char> nextWord = PopNextWord(ref remaining);
			string nextParts = GetNextParts(Root, ref remaining, ref nextWord, out var parts);
			if (nextParts != null)
			{
				return nextParts;
			}
			ResourceIdentifierParts value = parts.Value;
			while (!nextWord.IsEmpty)
			{
				ResourceIdentifier resourceIdentifier = new ResourceIdentifier(value.Parent, value.ResourceType, value.ResourceName, value.IsProviderResource, value.SpecialType);
				if (value.SpecialType == SpecialType.Subscription)
				{
					nextParts = resourceIdentifier.CheckSubscriptionFormat();
					if (nextParts != null)
					{
						return nextParts;
					}
				}
				nextParts = GetNextParts(resourceIdentifier, ref remaining, ref nextWord, out parts);
				if (nextParts != null)
				{
					return nextParts;
				}
				value = parts.Value;
			}
			Init(value.Parent, value.ResourceType, value.ResourceName, value.IsProviderResource, value.SpecialType);
			if (value.SpecialType != SpecialType.Subscription)
			{
				return null;
			}
			return CheckSubscriptionFormat();
		}

		private string? CheckSubscriptionFormat()
		{
			if (_subscriptionId != null && !Guid.TryParse(_subscriptionId, out var _))
			{
				return "The GUID for subscription is invalid " + _subscriptionId + ".";
			}
			return null;
		}

		private T GetValue<T>(ref T value)
		{
			if (!_initialized)
			{
				string text = Parse();
				if (text != null)
				{
					throw new FormatException(text);
				}
			}
			return value;
		}

		private static ResourceType ChooseResourceType(ReadOnlySpan<char> resourceTypeName, ResourceIdentifier parent, out SpecialType specialType)
		{
			if (MemoryExtensions.Equals(resourceTypeName, MemoryExtensions.AsSpan("resourcegroups"), StringComparison.OrdinalIgnoreCase))
			{
				specialType = SpecialType.ResourceGroup;
				if (parent.ResourceType == ResourceType.Subscription)
				{
					return ResourceType.ResourceGroup;
				}
			}
			else
			{
				if (MemoryExtensions.Equals(resourceTypeName, MemoryExtensions.AsSpan("subscriptions"), StringComparison.OrdinalIgnoreCase) && parent.ResourceType == ResourceType.Tenant)
				{
					specialType = SpecialType.Subscription;
					return ResourceType.Subscription;
				}
				specialType = (MemoryExtensions.Equals(resourceTypeName, MemoryExtensions.AsSpan("locations"), StringComparison.OrdinalIgnoreCase) ? SpecialType.Location : SpecialType.None);
			}
			return parent.ResourceType.AppendChild(resourceTypeName.ToString());
		}

		private static string? GetNextParts(ResourceIdentifier parent, ref ReadOnlySpan<char> remaining, ref ReadOnlySpan<char> nextWord, out ResourceIdentifierParts? parts)
		{
			parts = null;
			ReadOnlySpan<char> readOnlySpan = nextWord;
			ReadOnlySpan<char> readOnlySpan2 = PopNextWord(ref remaining);
			if (readOnlySpan2.IsEmpty)
			{
				if (MemoryExtensions.Equals(readOnlySpan, MemoryExtensions.AsSpan("subscriptions"), StringComparison.OrdinalIgnoreCase) || MemoryExtensions.Equals(readOnlySpan, MemoryExtensions.AsSpan("resourcegroups"), StringComparison.OrdinalIgnoreCase))
				{
					return "The ResourceIdentifier is missing the key for " + readOnlySpan.ToString() + ".";
				}
				if (parent.ResourceType == ResourceType.ResourceGroup)
				{
					return "Expected providers path segment after resourcegroups.";
				}
				nextWord = readOnlySpan2;
				SpecialType specialType = (MemoryExtensions.Equals(readOnlySpan, MemoryExtensions.AsSpan("locations"), StringComparison.OrdinalIgnoreCase) ? SpecialType.Location : SpecialType.None);
				ResourceType resourceType = parent.ResourceType.AppendChild(readOnlySpan.ToString());
				parts = new ResourceIdentifierParts(parent, new ResourceType(resourceType), string.Empty, isProviderResource: false, specialType);
				return null;
			}
			ReadOnlySpan<char> readOnlySpan3 = PopNextWord(ref remaining);
			if (MemoryExtensions.Equals(readOnlySpan, MemoryExtensions.AsSpan("providers"), StringComparison.OrdinalIgnoreCase))
			{
				if (readOnlySpan3.IsEmpty || MemoryExtensions.Equals(readOnlySpan3, MemoryExtensions.AsSpan("providers"), StringComparison.OrdinalIgnoreCase))
				{
					if (parent.ResourceType == ResourceType.Subscription || parent.ResourceType == ResourceType.Tenant)
					{
						nextWord = readOnlySpan3;
						parts = new ResourceIdentifierParts(parent, ResourceType.Provider, readOnlySpan2.ToString(), isProviderResource: true, SpecialType.Provider);
						return null;
					}
					return "Provider resource can only come after the root or subscriptions.";
				}
				ReadOnlySpan<char> readOnlySpan4 = PopNextWord(ref remaining);
				if (!readOnlySpan4.IsEmpty)
				{
					nextWord = PopNextWord(ref remaining);
					SpecialType specialType2 = (MemoryExtensions.Equals(readOnlySpan3, MemoryExtensions.AsSpan("locations"), StringComparison.OrdinalIgnoreCase) ? SpecialType.Location : SpecialType.None);
					parts = new ResourceIdentifierParts(parent, new ResourceType(readOnlySpan2.ToString(), readOnlySpan3.ToString()), readOnlySpan4.ToString(), isProviderResource: true, specialType2);
					return null;
				}
				return "Invalid resource id.";
			}
			nextWord = readOnlySpan3;
			parts = new ResourceIdentifierParts(parent, ChooseResourceType(readOnlySpan, parent, out var specialType3), readOnlySpan2.ToString(), isProviderResource: false, specialType3);
			return null;
		}

		private static ReadOnlySpan<char> PopNextWord(scoped ref ReadOnlySpan<char> remaining)
		{
			int num = remaining.IndexOf('/');
			if (num < 0)
			{
				ReadOnlySpan<char> result = remaining.Slice(0);
				remaining = ReadOnlySpan<char>.Empty;
				return result;
			}
			ReadOnlySpan<char> result2 = remaining.Slice(0, num);
			remaining = remaining.Slice(num + 1);
			return result2;
		}

		private string ToResourceString()
		{
			if ((object)Parent == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder((Parent == Root) ? string.Empty : Parent.StringValue);
			if (!IsProviderResource)
			{
				stringBuilder.Append('/').Append(ResourceType.GetLastType());
				if (!string.IsNullOrWhiteSpace(Name))
				{
					stringBuilder.Append('/').Append(Name);
				}
			}
			else
			{
				stringBuilder.Append("/providers/").Append(ResourceType).Append('/')
					.Append(Name);
			}
			return stringBuilder.ToString();
		}

		public override string ToString()
		{
			return StringValue;
		}

		public bool Equals(ResourceIdentifier? other)
		{
			if ((object)other == null)
			{
				return false;
			}
			return StringValue.Equals(other.StringValue, StringComparison.OrdinalIgnoreCase);
		}

		public int CompareTo(ResourceIdentifier? other)
		{
			if ((object)other == null)
			{
				return 1;
			}
			return string.Compare(StringValue, other.StringValue, StringComparison.OrdinalIgnoreCase);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? obj)
		{
			if (obj is ResourceIdentifier resourceIdentifier)
			{
				return resourceIdentifier.Equals(this);
			}
			if (obj is string resourceId)
			{
				return Equals(new ResourceIdentifier(resourceId));
			}
			return false;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(StringValue);
		}

		public static implicit operator string?(ResourceIdentifier id)
		{
			return id?.StringValue;
		}

		public static bool operator ==(ResourceIdentifier left, ResourceIdentifier right)
		{
			return left?.Equals(right) ?? ((object)right == null);
		}

		public static bool operator !=(ResourceIdentifier left, ResourceIdentifier right)
		{
			if ((object)left == null)
			{
				return (object)right != null;
			}
			return !left.Equals(right);
		}

		public static bool operator <(ResourceIdentifier left, ResourceIdentifier right)
		{
			if ((object)left != null)
			{
				return left.CompareTo(right) < 0;
			}
			return (object)right != null;
		}

		public static bool operator <=(ResourceIdentifier left, ResourceIdentifier right)
		{
			if ((object)left != null)
			{
				return left.CompareTo(right) <= 0;
			}
			return true;
		}

		public static bool operator >(ResourceIdentifier left, ResourceIdentifier right)
		{
			if ((object)left != null)
			{
				return left.CompareTo(right) > 0;
			}
			return false;
		}

		public static bool operator >=(ResourceIdentifier left, ResourceIdentifier right)
		{
			if ((object)left != null)
			{
				return left.CompareTo(right) >= 0;
			}
			return (object)right == null;
		}

		public static ResourceIdentifier Parse(string input)
		{
			ResourceIdentifier resourceIdentifier = new ResourceIdentifier(input);
			string text = resourceIdentifier.Parse();
			if (text != null)
			{
				throw new FormatException(text);
			}
			return resourceIdentifier;
		}

		public static bool TryParse(string? input, out ResourceIdentifier? result)
		{
			result = null;
			if (string.IsNullOrEmpty(input))
			{
				return false;
			}
			result = new ResourceIdentifier(input);
			if (result.Parse() == null)
			{
				return true;
			}
			result = null;
			return false;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public ResourceIdentifier AppendProviderResource(string providerNamespace, string resourceType, string resourceName)
		{
			ValidateProviderResourceParameters(providerNamespace, resourceType, resourceName);
			SpecialType specialType = (resourceType.Equals("locations", StringComparison.OrdinalIgnoreCase) ? SpecialType.Location : SpecialType.None);
			return new ResourceIdentifier(this, new ResourceType(providerNamespace, resourceType), resourceName, isProviderResource: true, specialType);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public ResourceIdentifier AppendChildResource(string childResourceType, string childResourceName)
		{
			ValidateChildResourceParameters(childResourceType, childResourceName);
			SpecialType specialType;
			return new ResourceIdentifier(this, ChooseResourceType(MemoryExtensions.AsSpan(childResourceType), this, out specialType), childResourceName, isProviderResource: false, specialType);
		}

		private static void ValidateProviderResourceParameters(string providerNamespace, string resourceType, string resourceName)
		{
			ValidatePathSegment(providerNamespace, "providerNamespace");
			ValidatePathSegment(resourceType, "resourceType");
			ValidatePathSegment(resourceName, "resourceName");
		}

		private static void ValidateChildResourceParameters(string childResourceType, string childResourceName)
		{
			ValidatePathSegment(childResourceType, "childResourceType");
			ValidatePathSegment(childResourceName, "childResourceName");
		}

		private static void ValidatePathSegment(string segment, string parameterName)
		{
			Argument.AssertNotNullOrWhiteSpace(segment, "segment");
			if (Enumerable.Contains(segment, '/'))
			{
				throw new ArgumentOutOfRangeException(parameterName, parameterName + " must be a single path segment");
			}
		}
	}
	public readonly struct ResourceType : IEquatable<ResourceType>
	{
		internal static ResourceType Tenant = new ResourceType("Microsoft.Resources", "tenants", "Microsoft.Resources/tenants");

		internal static ResourceType Subscription = new ResourceType("Microsoft.Resources", "subscriptions", "Microsoft.Resources/subscriptions");

		internal static ResourceType ResourceGroup = new ResourceType("Microsoft.Resources", "resourceGroups", "Microsoft.Resources/resourceGroups");

		internal static ResourceType Provider = new ResourceType("Microsoft.Resources", "providers", "Microsoft.Resources/providers");

		internal const string ResourceNamespace = "Microsoft.Resources";

		private readonly string _stringValue;

		public string Namespace { get; }

		public string Type { get; }

		public ResourceType(string resourceType)
		{
			Argument.AssertNotNullOrWhiteSpace(resourceType, "resourceType");
			int num = resourceType.IndexOf('/');
			if (num == -1 || resourceType.Length < 3)
			{
				throw new ArgumentOutOfRangeException("resourceType");
			}
			_stringValue = resourceType;
			Namespace = resourceType.Substring(0, num);
			Type = resourceType.Substring(num + 1);
		}

		internal ResourceType(string providerNamespace, string name)
		{
			Namespace = providerNamespace;
			Type = name;
			_stringValue = Namespace + "/" + Type;
		}

		private ResourceType(string providerNamespace, string name, string fullName)
		{
			Namespace = providerNamespace;
			Type = name;
			_stringValue = fullName;
		}

		internal ResourceType AppendChild(string childType)
		{
			return new ResourceType(Namespace, $"{Type}{'/'}{childType}");
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public string GetLastType()
		{
			return Type.Substring(Type.LastIndexOf('/') + 1);
		}

		public static implicit operator ResourceType(string resourceType)
		{
			return new ResourceType(resourceType);
		}

		public static implicit operator string(ResourceType resourceType)
		{
			return resourceType._stringValue;
		}

		public static bool operator ==(ResourceType left, ResourceType right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ResourceType left, ResourceType right)
		{
			return !left.Equals(right);
		}

		public bool Equals(ResourceType other)
		{
			return string.Equals(_stringValue, other._stringValue, StringComparison.OrdinalIgnoreCase);
		}

		public override string ToString()
		{
			return _stringValue;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? other)
		{
			if (other == null)
			{
				return false;
			}
			if (other is ResourceType other2)
			{
				return Equals(other2);
			}
			if (other is string text)
			{
				return Equals(text);
			}
			return false;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(_stringValue);
		}
	}
	public abstract class ResponseClassificationHandler
	{
		public abstract bool TryClassify(HttpMessage message, out bool isError);
	}
	public class ResponseClassifier
	{
		internal static ResponseClassifier Shared { get; } = new ResponseClassifier();

		public virtual bool IsRetriableResponse(HttpMessage message)
		{
			switch (message.Response.Status)
			{
			case 408:
			case 429:
			case 500:
			case 502:
			case 503:
			case 504:
				return true;
			default:
				return false;
			}
		}

		public virtual bool IsRetriableException(Exception exception)
		{
			if (!(exception is IOException))
			{
				if (exception is RequestFailedException ex)
				{
					return ex.Status == 0;
				}
				return false;
			}
			return true;
		}

		public virtual bool IsRetriable(HttpMessage message, Exception exception)
		{
			if (!IsRetriableException(exception))
			{
				if (exception is OperationCanceledException)
				{
					return !message.CancellationToken.IsCancellationRequested;
				}
				return false;
			}
			return true;
		}

		public virtual bool IsErrorResponse(HttpMessage message)
		{
			int num = message.Response.Status / 100;
			if (num != 4)
			{
				return num == 5;
			}
			return true;
		}
	}
	public readonly struct ResponseHeaders : IEnumerable<HttpHeader>, IEnumerable
	{
		private const string RetryAfterHeaderName = "Retry-After";

		private const string RetryAfterMsHeaderName = "retry-after-ms";

		private const string XRetryAfterMsHeaderName = "x-ms-retry-after-ms";

		private readonly Response _response;

		public DateTimeOffset? Date
		{
			get
			{
				if (!TryGetValue(HttpHeader.Names.Date, out string value) && !TryGetValue(HttpHeader.Names.XMsDate, out value))
				{
					return null;
				}
				return DateTimeOffset.Parse(value, CultureInfo.InvariantCulture);
			}
		}

		public string? ContentType
		{
			get
			{
				if (!TryGetValue(HttpHeader.Names.ContentType, out string value))
				{
					return null;
				}
				return value;
			}
		}

		public int? ContentLength
		{
			get
			{
				if (!TryGetValue(HttpHeader.Names.ContentLength, out string value))
				{
					return null;
				}
				if (!int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
				{
					throw new OverflowException($"Failed to parse value of 'Content-Length' header: '{value}'.  If value exceeds {int.MaxValue}, please use 'Response.Headers.ContentLengthLong' instead.");
				}
				return result;
			}
		}

		public long? ContentLengthLong
		{
			get
			{
				if (!TryGetValue(HttpHeader.Names.ContentLength, out string value))
				{
					return null;
				}
				return long.Parse(value, CultureInfo.InvariantCulture);
			}
		}

		public ETag? ETag
		{
			get
			{
				if (!TryGetValue(HttpHeader.Names.ETag, out string value))
				{
					return null;
				}
				return Azure.ETag.Parse(value);
			}
		}

		public string? RequestId
		{
			get
			{
				if (!TryGetValue(HttpHeader.Names.XMsRequestId, out string value))
				{
					return null;
				}
				return value;
			}
		}

		internal TimeSpan? RetryAfter
		{
			get
			{
				if ((TryGetValue("retry-after-ms", out string value) || TryGetValue("x-ms-retry-after-ms", out value)) && int.TryParse(value, out var result))
				{
					return TimeSpan.FromMilliseconds(result);
				}
				if (TryGetValue("Retry-After", out value))
				{
					if (int.TryParse(value, out var result2))
					{
						return TimeSpan.FromSeconds(result2);
					}
					if (DateTimeOffset.TryParse(value, out var result3))
					{
						return result3 - DateTimeOffset.Now;
					}
				}
				return null;
			}
		}

		internal ResponseHeaders(Response response)
		{
			_response = response;
		}

		public IEnumerator<HttpHeader> GetEnumerator()
		{
			return _response.EnumerateHeaders().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _response.EnumerateHeaders().GetEnumerator();
		}

		public bool TryGetValue(string name, [NotNullWhen(true)] out string? value)
		{
			return _response.TryGetHeader(name, out value);
		}

		public bool TryGetValues(string name, [NotNullWhen(true)] out IEnumerable<string>? values)
		{
			return _response.TryGetHeaderValues(name, out values);
		}

		public bool Contains(string name)
		{
			return _response.ContainsHeader(name);
		}
	}
	public enum RetryMode
	{
		Fixed,
		Exponential
	}
	public class RetryOptions
	{
		internal const int DefaultMaxRetries = 3;

		internal static readonly TimeSpan DefaultMaxDelay = TimeSpan.FromMinutes(1.0);

		internal static readonly TimeSpan DefaultInitialDelay = TimeSpan.FromSeconds(0.8);

		public int MaxRetries { get; set; } = 3;

		public TimeSpan Delay { get; set; } = TimeSpan.FromSeconds(0.8);

		public TimeSpan MaxDelay { get; set; } = TimeSpan.FromMinutes(1.0);

		public RetryMode Mode { get; set; } = RetryMode.Exponential;

		public TimeSpan NetworkTimeout { get; set; } = TimeSpan.FromSeconds(100.0);

		internal RetryOptions()
			: this(ClientOptions.Default.Retry)
		{
		}

		internal RetryOptions(RetryOptions? retryOptions)
		{
			if (retryOptions != null)
			{
				MaxRetries = retryOptions.MaxRetries;
				Delay = retryOptions.Delay;
				MaxDelay = retryOptions.MaxDelay;
				Mode = retryOptions.Mode;
				NetworkTimeout = retryOptions.NetworkTimeout;
			}
		}
	}
	public class StatusCodeClassifier : ResponseClassifier
	{
		private const int Length = 10;

		private ulong[] _successCodes;

		internal ResponseClassificationHandler[]? Handlers { get; set; }

		public StatusCodeClassifier(ReadOnlySpan<ushort> successStatusCodes)
		{
			_successCodes = new ulong[10];
			ReadOnlySpan<ushort> readOnlySpan = successStatusCodes;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				int statusCode = readOnlySpan[i];
				AddClassifier(statusCode, isError: false);
			}
		}

		private StatusCodeClassifier(ulong[] successCodes, ResponseClassificationHandler[]? handlers)
		{
			_successCodes = successCodes;
			Handlers = handlers;
		}

		public override bool IsErrorResponse(HttpMessage message)
		{
			if (Handlers != null)
			{
				ResponseClassificationHandler[] handlers = Handlers;
				for (int i = 0; i < handlers.Length; i++)
				{
					if (handlers[i].TryClassify(message, out var isError))
					{
						return isError;
					}
				}
			}
			return !IsSuccessCode(message.Response.Status);
		}

		internal virtual StatusCodeClassifier Clone()
		{
			ulong[] array = new ulong[10];
			Array.Copy(_successCodes, array, 10);
			return new StatusCodeClassifier(array, Handlers);
		}

		internal void AddClassifier(int statusCode, bool isError)
		{
			Argument.AssertInRange(statusCode, 0, 639, "statusCode");
			int num = statusCode >> 6;
			int num2 = statusCode & 0x3F;
			ulong num3 = (ulong)(1L << num2);
			ulong num4 = _successCodes[num];
			num4 = (isError ? (num4 & ~num3) : (num4 | num3));
			_successCodes[num] = num4;
		}

		private bool IsSuccessCode(int statusCode)
		{
			int num = statusCode >> 6;
			int num2 = statusCode & 0x3F;
			ulong num3 = (ulong)(1L << num2);
			return (_successCodes[num] & num3) != 0;
		}
	}
	public delegate Task SyncAsyncEventHandler<T>(T e) where T : SyncAsyncEventArgs;
	public class TelemetryDetails
	{
		private const int MaxApplicationIdLength = 24;

		private readonly string _userAgent;

		public Assembly Assembly { get; }

		public string? ApplicationId { get; }

		public TelemetryDetails(Assembly assembly, string? applicationId = null)
			: this(assembly, applicationId, new RuntimeInformationWrapper())
		{
		}

		internal TelemetryDetails(Assembly assembly, string? applicationId = null, RuntimeInformationWrapper? runtimeInformation = null)
		{
			Argument.AssertNotNull(assembly, "assembly");
			if (applicationId != null && applicationId.Length > 24)
			{
				throw new ArgumentOutOfRangeException("applicationId", string.Format("{0} must be shorter than {1} characters", "applicationId", 25));
			}
			Assembly = assembly;
			ApplicationId = applicationId;
			_userAgent = GenerateUserAgentString(assembly, applicationId, runtimeInformation);
		}

		public void Apply(HttpMessage message)
		{
			message.SetProperty(typeof(UserAgentValueKey), ToString());
		}

		internal static string GenerateUserAgentString(Assembly clientAssembly, string? applicationId = null, RuntimeInformationWrapper? runtimeInformation = null)
		{
			string text = (clientAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>() ?? throw new InvalidOperationException("AssemblyInformationalVersionAttribute is required on client SDK assembly '" + clientAssembly.FullName + "'.")).InformationalVersion;
			string text2 = clientAssembly.GetName().Name;
			if (text2.StartsWith("Azure.", StringComparison.Ordinal))
			{
				text2 = text2.Substring("Azure.".Length);
			}
			int num = text.IndexOfOrdinal('+');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			if (runtimeInformation == null)
			{
				runtimeInformation = new RuntimeInformationWrapper();
			}
			string text3 = (ContainsNonAscii(runtimeInformation.OSDescription) ? WebUtility.UrlEncode(runtimeInformation.OSDescription) : runtimeInformation.OSDescription);
			string text4 = EscapeProductInformation("(" + runtimeInformation.FrameworkDescription + "; " + text3 + ")");
			if (applicationId == null)
			{
				return "azsdk-net-" + text2 + "/" + text + " " + text4;
			}
			return applicationId + " azsdk-net-" + text2 + "/" + text + " " + text4;
		}

		public override string ToString()
		{
			return _userAgent;
		}

		private static string EscapeProductInformation(string productInfo)
		{
			bool flag = false;
			try
			{
				flag = ProductInfoHeaderValue.TryParse(productInfo, out var _);
			}
			catch (Exception)
			{
			}
			if (flag)
			{
				return productInfo;
			}
			StringBuilder stringBuilder = new StringBuilder(productInfo.Length + 2);
			stringBuilder.Append('(');
			for (int i = 1; i < productInfo.Length - 1; i++)
			{
				char c = productInfo[i];
				switch (c)
				{
				case '(':
				case ')':
					stringBuilder.Append('\\');
					break;
				case '\\':
					if (i + 1 < productInfo.Length - 1)
					{
						char c2 = productInfo[i + 1];
						if (c2 == '\\' || c2 == '(' || c2 == ')')
						{
							stringBuilder.Append(c);
							stringBuilder.Append(c2);
							i++;
							continue;
						}
						stringBuilder.Append('\\');
					}
					else
					{
						stringBuilder.Append('\\');
					}
					break;
				}
				stringBuilder.Append(c);
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		private static bool ContainsNonAscii(string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] > '\u007f')
				{
					return true;
				}
			}
			return false;
		}
	}
	public abstract class TokenCredential
	{
		[CallerShouldAudit("https://aka.ms/azsdk/callershouldaudit/identity")]
		public abstract ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken);

		[CallerShouldAudit("https://aka.ms/azsdk/callershouldaudit/identity")]
		public abstract AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken);
	}
	public readonly struct TokenRequestContext
	{
		public string[] Scopes { get; }

		public string? ParentRequestId { get; }

		public string? Claims { get; }

		public string? TenantId { get; }

		public bool IsCaeEnabled { get; }

		public TokenRequestContext(string[] scopes, string? parentRequestId)
		{
			IsCaeEnabled = false;
			Scopes = scopes;
			ParentRequestId = parentRequestId;
			Claims = null;
			TenantId = null;
		}

		public TokenRequestContext(string[] scopes, string? parentRequestId, string? claims)
		{
			IsCaeEnabled = false;
			Scopes = scopes;
			ParentRequestId = parentRequestId;
			Claims = claims;
			TenantId = null;
		}

		public TokenRequestContext(string[] scopes, string? parentRequestId, string? claims, string? tenantId)
		{
			IsCaeEnabled = false;
			Scopes = scopes;
			ParentRequestId = parentRequestId;
			Claims = claims;
			TenantId = tenantId;
		}

		public TokenRequestContext(string[] scopes, string? parentRequestId = null, string? claims = null, string? tenantId = null, bool isCaeEnabled = false)
		{
			Scopes = scopes;
			ParentRequestId = parentRequestId;
			Claims = claims;
			TenantId = tenantId;
			IsCaeEnabled = isCaeEnabled;
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
	internal sealed class AsyncLockWithValue<T>
	{
		public readonly struct LockOrValue : IDisposable
		{
			private readonly AsyncLockWithValue<T>? _owner;

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

			public LockOrValue(AsyncLockWithValue<T> owner, long index)
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
	internal static class Argument
	{
		public static void AssertNotNull<T>([AllowNull][NotNull] T value, string name)
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

		public static void AssertNotNullOrEmpty<T>([AllowNull][NotNull] IEnumerable<T> value, string name)
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

		public static void AssertNotNullOrEmpty([AllowNull][NotNull] string value, string name)
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

		public static void AssertNotNullOrWhiteSpace([AllowNull][NotNull] string value, string name)
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

		public static T CheckNotNull<T>([AllowNull][NotNull] T value, string name) where T : class
		{
			AssertNotNull(value, name);
			return value;
		}

		public static string CheckNotNullOrEmpty([AllowNull][NotNull] string value, string name)
		{
			AssertNotNullOrEmpty(value, name);
			return value;
		}

		public static void AssertNull<T>([AllowNull] T value, string name, [AllowNull] string message = null)
		{
			if (value != null)
			{
				throw new ArgumentException(message ?? "Value must be null.", name);
			}
		}
	}
	internal static class AuthorizationChallengeParser
	{
		public static string? GetChallengeParameterFromResponse(Response response, string challengeScheme, string challengeParameter)
		{
			if (!response.Headers.TryGetValue(HttpHeader.Names.WwwAuthenticate, out string value))
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
	internal class AzureKeyCredentialPolicy : HttpPipelineSynchronousPolicy
	{
		private readonly string _name;

		private readonly AzureKeyCredential _credential;

		private readonly string? _prefix;

		public AzureKeyCredentialPolicy(AzureKeyCredential credential, string name, string? prefix = null)
		{
			if (credential == null)
			{
				throw new ArgumentNullException("credential");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Value cannot be an empty string.", "name");
			}
			_credential = credential;
			_name = name;
			_prefix = prefix;
		}

		public override void OnSendingRequest(HttpMessage message)
		{
			base.OnSendingRequest(message);
			message.Request.Headers.SetValue(_name, (_prefix != null) ? (_prefix + " " + _credential.Key) : _credential.Key);
		}
	}
	internal class AzureSasCredentialSynchronousPolicy : HttpPipelineSynchronousPolicy
	{
		private class AzureSasSignatureHistory
		{
		}

		private readonly AzureSasCredential _credential;

		public AzureSasCredentialSynchronousPolicy(AzureSasCredential credential)
		{
			Argument.AssertNotNull(credential, "credential");
			_credential = credential;
		}

		public override void OnSendingRequest(HttpMessage message)
		{
			string query = message.Request.Uri.Query;
			string text = _credential.Signature;
			if (text.StartsWith("?", StringComparison.InvariantCulture))
			{
				text = text.Substring(1);
			}
			if (!query.Contains(text))
			{
				query = ((!message.TryGetProperty(typeof(AzureSasSignatureHistory), out object value) || !(value is string text2) || !query.Contains(text2)) ? (string.IsNullOrEmpty(query) ? ("?" + text) : (query + "&" + text)) : query.Replace(text2, text));
				message.Request.Uri.Query = query;
				message.SetProperty(typeof(AzureSasSignatureHistory), text);
				base.OnSendingRequest(message);
			}
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
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	internal class CallerShouldAuditAttribute : Attribute
	{
		public string Reason { get; set; }

		public CallerShouldAuditAttribute(string reason)
		{
			Reason = reason;
		}
	}
	internal class DictionaryHeaders
	{
		private readonly Dictionary<string, object> _headers = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

		public void AddHeader(string name, string value)
		{
			if (!_headers.TryGetValue(name, out var value2))
			{
				_headers[name] = value;
				return;
			}
			if (value2 is List<string> list)
			{
				list.Add(value);
				return;
			}
			_headers[name] = new List<string>
			{
				value2 as string,
				value
			};
		}

		public bool TryGetHeader(string name, out string value)
		{
			if (_headers.TryGetValue(name, out var value2))
			{
				if (value2 is List<string> values)
				{
					value = JoinHeaderValue(values);
				}
				else
				{
					value = value2 as string;
				}
				return true;
			}
			value = null;
			return false;
		}

		public bool TryGetHeaderValues(string name, out IEnumerable<string> values)
		{
			if (_headers.TryGetValue(name, out var value))
			{
				if (value is List<string> list)
				{
					values = list;
				}
				else
				{
					values = new List<string> { value as string };
				}
				return true;
			}
			values = null;
			return false;
		}

		public bool ContainsHeader(string name)
		{
			return _headers.ContainsKey(name);
		}

		public void SetHeader(string name, string value)
		{
			_headers[name] = value;
		}

		public bool RemoveHeader(string name)
		{
			return _headers.Remove(name);
		}

		public IEnumerable<HttpHeader> EnumerateHeaders()
		{
			return _headers.Select((KeyValuePair<string, object> h) => new HttpHeader(h.Key, (h.Value is List<string> values) ? JoinHeaderValue(values) : (h.Value as string)));
		}

		private static string JoinHeaderValue(IEnumerable<string> values)
		{
			return string.Join(",", values);
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
	internal struct HashCodeBuilder
	{
		private static readonly uint s_seed = GenerateGlobalSeed();

		private const uint Prime1 = 2654435761u;

		private const uint Prime2 = 2246822519u;

		private const uint Prime3 = 3266489917u;

		private const uint Prime4 = 668265263u;

		private const uint Prime5 = 374761393u;

		private uint _v1;

		private uint _v2;

		private uint _v3;

		private uint _v4;

		private uint _queue1;

		private uint _queue2;

		private uint _queue3;

		private uint _length;

		private static uint GenerateGlobalSeed()
		{
			return (uint)new Random().Next();
		}

		public static int Combine<T1>(T1 value1)
		{
			uint queuedValue = (uint)(value1?.GetHashCode() ?? 0);
			return (int)MixFinal(QueueRound(MixEmptyState() + 4, queuedValue));
		}

		public static int Combine<T1, T2>(T1 value1, T2 value2)
		{
			uint queuedValue = (uint)(value1?.GetHashCode() ?? 0);
			uint queuedValue2 = (uint)(value2?.GetHashCode() ?? 0);
			return (int)MixFinal(QueueRound(QueueRound(MixEmptyState() + 8, queuedValue), queuedValue2));
		}

		public static int Combine<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
		{
			uint queuedValue = (uint)(value1?.GetHashCode() ?? 0);
			uint queuedValue2 = (uint)(value2?.GetHashCode() ?? 0);
			uint queuedValue3 = (uint)(value3?.GetHashCode() ?? 0);
			return (int)MixFinal(QueueRound(QueueRound(QueueRound(MixEmptyState() + 12, queuedValue), queuedValue2), queuedValue3));
		}

		public static int Combine<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
		{
			uint input = (uint)(value1?.GetHashCode() ?? 0);
			uint input2 = (uint)(value2?.GetHashCode() ?? 0);
			uint input3 = (uint)(value3?.GetHashCode() ?? 0);
			uint input4 = (uint)(value4?.GetHashCode() ?? 0);
			Initialize(out var v, out var v2, out var v3, out var v4);
			v = Round(v, input);
			v2 = Round(v2, input2);
			v3 = Round(v3, input3);
			v4 = Round(v4, input4);
			return (int)MixFinal(MixState(v, v2, v3, v4) + 16);
		}

		public static int Combine<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
		{
			uint input = (uint)(value1?.GetHashCode() ?? 0);
			uint input2 = (uint)(value2?.GetHashCode() ?? 0);
			uint input3 = (uint)(value3?.GetHashCode() ?? 0);
			uint input4 = (uint)(value4?.GetHashCode() ?? 0);
			uint queuedValue = (uint)(value5?.GetHashCode() ?? 0);
			Initialize(out var v, out var v2, out var v3, out var v4);
			v = Round(v, input);
			v2 = Round(v2, input2);
			v3 = Round(v3, input3);
			v4 = Round(v4, input4);
			return (int)MixFinal(QueueRound(MixState(v, v2, v3, v4) + 20, queuedValue));
		}

		public static int Combine<T1, T2, T3, T4, T5, T6>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
		{
			uint input = (uint)(value1?.GetHashCode() ?? 0);
			uint input2 = (uint)(value2?.GetHashCode() ?? 0);
			uint input3 = (uint)(value3?.GetHashCode() ?? 0);
			uint input4 = (uint)(value4?.GetHashCode() ?? 0);
			uint queuedValue = (uint)(value5?.GetHashCode() ?? 0);
			uint queuedValue2 = (uint)(value6?.GetHashCode() ?? 0);
			Initialize(out var v, out var v2, out var v3, out var v4);
			v = Round(v, input);
			v2 = Round(v2, input2);
			v3 = Round(v3, input3);
			v4 = Round(v4, input4);
			return (int)MixFinal(QueueRound(QueueRound(MixState(v, v2, v3, v4) + 24, queuedValue), queuedValue2));
		}

		public static int Combine<T1, T2, T3, T4, T5, T6, T7>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7)
		{
			uint input = (uint)(value1?.GetHashCode() ?? 0);
			uint input2 = (uint)(value2?.GetHashCode() ?? 0);
			uint input3 = (uint)(value3?.GetHashCode() ?? 0);
			uint input4 = (uint)(value4?.GetHashCode() ?? 0);
			uint queuedValue = (uint)(value5?.GetHashCode() ?? 0);
			uint queuedValue2 = (uint)(value6?.GetHashCode() ?? 0);
			uint queuedValue3 = (uint)(value7?.GetHashCode() ?? 0);
			Initialize(out var v, out var v2, out var v3, out var v4);
			v = Round(v, input);
			v2 = Round(v2, input2);
			v3 = Round(v3, input3);
			v4 = Round(v4, input4);
			return (int)MixFinal(QueueRound(QueueRound(QueueRound(MixState(v, v2, v3, v4) + 28, queuedValue), queuedValue2), queuedValue3));
		}

		public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8)
		{
			uint input = (uint)(value1?.GetHashCode() ?? 0);
			uint input2 = (uint)(value2?.GetHashCode() ?? 0);
			uint input3 = (uint)(value3?.GetHashCode() ?? 0);
			uint input4 = (uint)(value4?.GetHashCode() ?? 0);
			uint input5 = (uint)(value5?.GetHashCode() ?? 0);
			uint input6 = (uint)(value6?.GetHashCode() ?? 0);
			uint input7 = (uint)(value7?.GetHashCode() ?? 0);
			uint input8 = (uint)(value8?.GetHashCode() ?? 0);
			Initialize(out var v, out var v2, out var v3, out var v4);
			v = Round(v, input);
			v2 = Round(v2, input2);
			v3 = Round(v3, input3);
			v4 = Round(v4, input4);
			v = Round(v, input5);
			v2 = Round(v2, input6);
			v3 = Round(v3, input7);
			v4 = Round(v4, input8);
			return (int)MixFinal(MixState(v, v2, v3, v4) + 32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Initialize(out uint v1, out uint v2, out uint v3, out uint v4)
		{
			v1 = (uint)((int)s_seed + -1640531535 + -2048144777);
			v2 = s_seed + 2246822519u;
			v3 = s_seed;
			v4 = s_seed - 2654435761u;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint Round(uint hash, uint input)
		{
			return RotateLeft(hash + (uint)((int)input * -2048144777), 13) * 2654435761u;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint QueueRound(uint hash, uint queuedValue)
		{
			return RotateLeft(hash + (uint)((int)queuedValue * -1028477379), 17) * 668265263;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint MixState(uint v1, uint v2, uint v3, uint v4)
		{
			return RotateLeft(v1, 1) + RotateLeft(v2, 7) + RotateLeft(v3, 12) + RotateLeft(v4, 18);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint RotateLeft(uint value, int offset)
		{
			return (value << offset) | (value >> 64 - offset);
		}

		private static uint MixEmptyState()
		{
			return s_seed + 374761393;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint MixFinal(uint hash)
		{
			hash ^= hash >> 15;
			hash *= 2246822519u;
			hash ^= hash >> 13;
			hash *= 3266489917u;
			hash ^= hash >> 16;
			return hash;
		}

		public void Add<T>(T value)
		{
			Add(value?.GetHashCode() ?? 0);
		}

		public void Add<T>(T value, IEqualityComparer<T>? comparer)
		{
			Add((value != null) ? (comparer?.GetHashCode(value) ?? value.GetHashCode()) : 0);
		}

		private void Add(int value)
		{
			uint num = _length++;
			switch (num % 4)
			{
			case 0u:
				_queue1 = (uint)value;
				return;
			case 1u:
				_queue2 = (uint)value;
				return;
			case 2u:
				_queue3 = (uint)value;
				return;
			}
			if (num == 3)
			{
				Initialize(out _v1, out _v2, out _v3, out _v4);
			}
			_v1 = Round(_v1, _queue1);
			_v2 = Round(_v2, _queue2);
			_v3 = Round(_v3, _queue3);
			_v4 = Round(_v4, (uint)value);
		}

		public int ToHashCode()
		{
			uint length = _length;
			uint num = length % 4;
			uint num2 = ((length < 4) ? MixEmptyState() : MixState(_v1, _v2, _v3, _v4));
			num2 += length * 4;
			if (num != 0)
			{
				num2 = QueueRound(num2, _queue1);
				if (num > 1)
				{
					num2 = QueueRound(num2, _queue2);
					if (num > 2)
					{
						num2 = QueueRound(num2, _queue3);
					}
				}
			}
			return (int)MixFinal(num2);
		}

		[Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes. Use ToHashCode to retrieve the computed hash code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			throw new NotSupportedException();
		}

		[Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? obj)
		{
			throw new NotSupportedException();
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

		internal static HttpMessageSanitizer Default = new HttpMessageSanitizer(Array.Empty<string>(), Array.Empty<string>());

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
	internal interface IOperationSource<T>
	{
		T CreateResult(Response response, CancellationToken cancellationToken);

		ValueTask<T> CreateResultAsync(Response response, CancellationToken cancellationToken);
	}
	[AttributeUsage(AttributeTargets.Constructor)]
	internal class InitializationConstructorAttribute : Attribute
	{
	}
	internal class MemoryResponse : Response
	{
		private const int NoStatusCode = 0;

		private const string XmsClientRequestIdName = "x-ms-client-request-id";

		private int _status;

		private string _reasonPhrase;

		private readonly IDictionary<string, List<string>> _headers = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

		public override int Status => _status;

		public override string ReasonPhrase => _reasonPhrase;

		public override Stream ContentStream { get; set; }

		public override string ClientRequestId
		{
			get
			{
				if (!TryGetHeader("x-ms-client-request-id", out var value))
				{
					return null;
				}
				return value;
			}
			set
			{
				SetHeader("x-ms-client-request-id", value);
			}
		}

		public void SetStatus(int status)
		{
			_status = status;
		}

		public void SetReasonPhrase(string reasonPhrase)
		{
			_reasonPhrase = reasonPhrase;
		}

		public void SetContent(byte[] content)
		{
			ContentStream = new MemoryStream(content);
		}

		public void SetContent(string content)
		{
			SetContent(Encoding.UTF8.GetBytes(content));
		}

		public override void Dispose()
		{
			ContentStream?.Dispose();
		}

		public void SetHeader(string name, string value)
		{
			SetHeader(name, new List<string> { value });
		}

		public void SetHeader(string name, IEnumerable<string> values)
		{
			_headers[name] = values.ToList();
		}

		public void AddHeader(string name, string value)
		{
			if (!_headers.TryGetValue(name, out var value2))
			{
				value2 = (_headers[name] = new List<string>());
			}
			value2.Add(value);
		}

		protected internal override bool ContainsHeader(string name)
		{
			return _headers.ContainsKey(name);
		}

		protected internal override IEnumerable<HttpHeader> EnumerateHeaders()
		{
			return _headers.Select((KeyValuePair<string, List<string>> header) => new HttpHeader(header.Key, JoinHeaderValues(header.Value)));
		}

		protected internal override bool TryGetHeader(string name, out string value)
		{
			if (_headers.TryGetValue(name, out var value2))
			{
				value = JoinHeaderValues(value2);
				return true;
			}
			value = null;
			return false;
		}

		protected internal override bool TryGetHeaderValues(string name, out IEnumerable<string> values)
		{
			List<string> value;
			bool result = _headers.TryGetValue(name, out value);
			values = value;
			return result;
		}

		private static string JoinHeaderValues(IEnumerable<string> values)
		{
			return string.Join(",", values);
		}
	}
	internal class NextLinkOperationImplementation : IOperation
	{
		private sealed class EmptyResponse : Response
		{
			public override int Status { get; }

			public override string ReasonPhrase { get; }

			public override Stream? ContentStream
			{
				get
				{
					return null;
				}
				set
				{
					throw new InvalidOperationException("Should not set ContentStream for an empty response.");
				}
			}

			public override string ClientRequestId { get; set; }

			public EmptyResponse(HttpStatusCode status, string clientRequestId)
			{
				Status = (int)status;
				ReasonPhrase = status.ToString();
				ClientRequestId = clientRequestId;
			}

			public override void Dispose()
			{
			}

			protected internal override bool ContainsHeader(string name)
			{
				return false;
			}

			protected internal override IEnumerable<HttpHeader> EnumerateHeaders()
			{
				return Array.Empty<HttpHeader>();
			}

			protected internal override bool TryGetHeader(string name, out string value)
			{
				value = string.Empty;
				return false;
			}

			protected internal override bool TryGetHeaderValues(string name, out IEnumerable<string> values)
			{
				values = Array.Empty<string>();
				return false;
			}
		}

		private enum HeaderSource
		{
			None,
			OperationLocation,
			AzureAsyncOperation,
			Location
		}

		private class CompletedOperation : IOperation
		{
			private readonly OperationState _operationState;

			public CompletedOperation(OperationState operationState)
			{
				_operationState = operationState;
			}

			public ValueTask<OperationState> UpdateStateAsync(bool async, CancellationToken cancellationToken)
			{
				return new ValueTask<OperationState>(_operationState);
			}
		}

		private sealed class OperationToOperationOfT<T> : IOperation<T>
		{
			private readonly IOperationSource<T> _operationSource;

			private readonly IOperation _operation;

			public OperationToOperationOfT(IOperationSource<T> operationSource, IOperation operation)
			{
				_operationSource = operationSource;
				_operation = operation;
			}

			public async ValueTask<OperationState<T>> UpdateStateAsync(bool async, CancellationToken cancellationToken)
			{
				OperationState state = await _operation.UpdateStateAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (state.HasSucceeded)
				{
					T val = ((!async) ? _operationSource.CreateResult(state.RawResponse, cancellationToken) : (await _operationSource.CreateResultAsync(state.RawResponse, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
					T value = val;
					return OperationState<T>.Success(state.RawResponse, value);
				}
				if (state.HasCompleted)
				{
					return OperationState<T>.Failure(state.RawResponse, state.OperationFailedException);
				}
				return OperationState<T>.Pending(state.RawResponse);
			}
		}

		internal const string NotSet = "NOT_SET";

		internal const string RehydrationTokenVersion = "1.0.0";

		private const string ApiVersionParam = "api-version";

		private static readonly string[] FailureStates = new string[2] { "failed", "canceled" };

		private static readonly string[] SuccessStates = new string[1] { "succeeded" };

		private readonly HeaderSource _headerSource;

		private readonly Uri _startRequestUri;

		private readonly OperationFinalStateVia _finalStateVia;

		private readonly HttpPipeline _pipeline;

		private readonly string? _apiVersion;

		private string? _lastKnownLocation;

		private string _nextRequestUri;

		public string OperationId { get; private set; } = "NOT_SET";

		public RequestMethod RequestMethod { get; }

		public static IOperation Create(HttpPipeline pipeline, RequestMethod requestMethod, Uri startRequestUri, Response response, OperationFinalStateVia finalStateVia, bool skipApiVersionOverride = false, string? apiVersionOverrideValue = null)
		{
			string text = null;
			text = ((apiVersionOverrideValue == null) ? ((!skipApiVersionOverride && TryGetApiVersion(startRequestUri, out var apiVersion)) ? apiVersion.ToString() : null) : apiVersionOverrideValue);
			string nextRequestUri;
			bool isNextRequestPolling;
			HeaderSource headerSource = GetHeaderSource(requestMethod, startRequestUri, response, text, out nextRequestUri, out isNextRequestPolling);
			if (headerSource == HeaderSource.None && IsFinalState(response, headerSource, out OperationState? failureState, out string _))
			{
				return new CompletedOperation(failureState ?? GetOperationStateFromFinalResponse(requestMethod, response));
			}
			if (!response.Headers.TryGetValue("Location", out string value))
			{
				value = null;
			}
			return new NextLinkOperationImplementation(pipeline, requestMethod, startRequestUri, nextRequestUri, headerSource, value, finalStateVia, text, null, isNextRequestPolling);
		}

		public static IOperation<T> Create<T>(IOperationSource<T> operationSource, HttpPipeline pipeline, RequestMethod requestMethod, Uri startRequestUri, Response response, OperationFinalStateVia finalStateVia, bool skipApiVersionOverride = false, string? apiVersionOverrideValue = null)
		{
			IOperation operation = Create(pipeline, requestMethod, startRequestUri, response, finalStateVia, skipApiVersionOverride, apiVersionOverrideValue);
			return new OperationToOperationOfT<T>(operationSource, operation);
		}

		public static IOperation<T> Create<T>(IOperationSource<T> operationSource, IOperation operation)
		{
			return new OperationToOperationOfT<T>(operationSource, operation);
		}

		public static IOperation Create(HttpPipeline pipeline, RehydrationToken rehydrationToken)
		{
			AssertNotNull(rehydrationToken, "rehydrationToken");
			AssertNotNull(pipeline, "pipeline");
			using JsonDocument jsonDocument = JsonDocument.Parse(ModelReaderWriter.Write(rehydrationToken, ModelReaderWriterOptions.Json));
			JsonElement rootElement = jsonDocument.RootElement;
			if (!Uri.TryCreate(rootElement.GetProperty("initialUri").GetString(), UriKind.Absolute, out var result))
			{
				throw new ArgumentException("\"initialUri\" property on \"rehydrationToken\" is an invalid Uri", "rehydrationToken");
			}
			string nextRequestUri = rootElement.GetProperty("nextRequestUri").GetString();
			string method = rootElement.GetProperty("requestMethod").GetString();
			RequestMethod requestMethod = new RequestMethod(method);
			string lastKnownLocation = rootElement.GetProperty("lastKnownLocation").GetString();
			string value = rootElement.GetProperty("finalStateVia").GetString();
			OperationFinalStateVia finalStateVia = ((!Enum.IsDefined(typeof(OperationFinalStateVia), value)) ? OperationFinalStateVia.Location : ((OperationFinalStateVia)Enum.Parse(typeof(OperationFinalStateVia), value)));
			string value2 = rootElement.GetProperty("headerSource").GetString();
			return new NextLinkOperationImplementation(headerSource: Enum.IsDefined(typeof(HeaderSource), value2) ? ((HeaderSource)Enum.Parse(typeof(HeaderSource), value2)) : HeaderSource.None, pipeline: pipeline, requestMethod: requestMethod, startRequestUri: result, nextRequestUri: nextRequestUri, lastKnownLocation: lastKnownLocation, finalStateVia: finalStateVia, apiVersion: null, operationId: rehydrationToken.Id);
		}

		private NextLinkOperationImplementation(HttpPipeline pipeline, RequestMethod requestMethod, Uri startRequestUri, string nextRequestUri, HeaderSource headerSource, string? lastKnownLocation, OperationFinalStateVia finalStateVia, string? apiVersion, string? operationId = null, bool isNextRequestPolling = false)
		{
			AssertNotNull(pipeline, "pipeline");
			AssertNotNull(requestMethod, "requestMethod");
			AssertNotNull(startRequestUri, "startRequestUri");
			AssertNotNull(nextRequestUri, "nextRequestUri");
			AssertNotNull(headerSource, "headerSource");
			AssertNotNull(finalStateVia, "finalStateVia");
			RequestMethod = requestMethod;
			_headerSource = headerSource;
			_startRequestUri = startRequestUri;
			_nextRequestUri = nextRequestUri;
			_lastKnownLocation = lastKnownLocation;
			_finalStateVia = finalStateVia;
			_pipeline = pipeline;
			_apiVersion = apiVersion;
			if (operationId != null)
			{
				OperationId = operationId;
			}
			else if (isNextRequestPolling)
			{
				OperationId = ParseOperationId(startRequestUri, nextRequestUri);
			}
		}

		private static string ParseOperationId(Uri startRequestUri, string nextRequestUri)
		{
			if (Uri.TryCreate(nextRequestUri, UriKind.Absolute, out var result) && result.Scheme != "file")
			{
				return result.Segments.Last();
			}
			return new Uri(startRequestUri, nextRequestUri).Segments.Last();
		}

		public RehydrationToken GetRehydrationToken()
		{
			return GetRehydrationToken(RequestMethod, _startRequestUri, _nextRequestUri, _headerSource.ToString(), _lastKnownLocation, _finalStateVia.ToString(), OperationId);
		}

		public static RehydrationToken GetRehydrationToken(RequestMethod requestMethod, Uri startRequestUri, Response response, OperationFinalStateVia finalStateVia)
		{
			AssertNotNull(requestMethod, "requestMethod");
			AssertNotNull(startRequestUri, "startRequestUri");
			AssertNotNull(response, "response");
			AssertNotNull(finalStateVia, "finalStateVia");
			string nextRequestUri;
			bool isNextRequestPolling;
			HeaderSource headerSource = GetHeaderSource(requestMethod, startRequestUri, response, null, out nextRequestUri, out isNextRequestPolling);
			if (!response.Headers.TryGetValue("Location", out string value))
			{
				value = null;
			}
			return GetRehydrationToken(requestMethod, startRequestUri, nextRequestUri, headerSource.ToString(), value, finalStateVia.ToString(), isNextRequestPolling ? ParseOperationId(startRequestUri, nextRequestUri) : null);
		}

		public static RehydrationToken GetRehydrationToken(RequestMethod requestMethod, Uri startRequestUri, string nextRequestUri, string headerSource, string? lastKnownLocation, string finalStateVia, string? operationId = null)
		{
			return ModelReaderWriter.Read<RehydrationToken>(new BinaryData(string.Format("{{\"version\":\"{0}\",\"id\":{1},\"requestMethod\":\"{2}\",\"initialUri\":\"{3}\",\"nextRequestUri\":\"{4}\",\"headerSource\":\"{5}\",\"finalStateVia\":\"{6}\",\"lastKnownLocation\":{7}}}", "1.0.0", ConstructStringValue(operationId), requestMethod, startRequestUri.AbsoluteUri, nextRequestUri, headerSource, finalStateVia, ConstructStringValue(lastKnownLocation))));
		}

		private static string? ConstructStringValue(string? value)
		{
			if (value != null)
			{
				return "\"" + value + "\"";
			}
			return "null";
		}

		public async ValueTask<OperationState> UpdateStateAsync(bool async, CancellationToken cancellationToken)
		{
			Response response = ((!async) ? GetResponse(_nextRequestUri, cancellationToken) : (await GetResponseAsync(_nextRequestUri, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
			Response response2 = response;
			OperationState? failureState;
			string resourceLocation;
			bool flag = IsFinalState(response2, _headerSource, out failureState, out resourceLocation);
			if (failureState.HasValue)
			{
				return failureState.Value;
			}
			if (flag)
			{
				string finalUri = GetFinalUri(resourceLocation);
				Response response3;
				if (finalUri != null)
				{
					response = ((!async) ? GetResponse(finalUri, cancellationToken) : (await GetResponseAsync(finalUri, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
					response3 = response;
				}
				else
				{
					response3 = response2;
				}
				return GetOperationStateFromFinalResponse(RequestMethod, response3);
			}
			UpdateNextRequestUri(response2.Headers);
			return OperationState.Pending(response2);
		}

		private static OperationState GetOperationStateFromFinalResponse(RequestMethod requestMethod, Response response)
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
				return OperationState.Success(response);
			}
			return OperationState.Failure(response);
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
					OperationId = ParseOperationId(_startRequestUri, _nextRequestUri);
				}
				break;
			}
			case HeaderSource.AzureAsyncOperation:
			{
				if (headers.TryGetValue("Azure-AsyncOperation", out string value3))
				{
					_nextRequestUri = AppendOrReplaceApiVersion(value3, _apiVersion);
					OperationId = ParseOperationId(_startRequestUri, _nextRequestUri);
				}
				break;
			}
			case HeaderSource.Location:
				if (flag)
				{
					_nextRequestUri = AppendOrReplaceApiVersion(value, _apiVersion);
					OperationId = ParseOperationId(_startRequestUri, _nextRequestUri);
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
			if ((uint)(headerSource - 1) > 1u)
			{
				return null;
			}
			if (RequestMethod == RequestMethod.Delete)
			{
				return null;
			}
			switch (_finalStateVia)
			{
			case OperationFinalStateVia.LocationOverride:
				if (!string.IsNullOrEmpty(_lastKnownLocation))
				{
					return _lastKnownLocation;
				}
				break;
			case OperationFinalStateVia.AzureAsyncOperation:
			case OperationFinalStateVia.OperationLocation:
				if (RequestMethod == RequestMethod.Post)
				{
					return null;
				}
				break;
			case OperationFinalStateVia.OriginalUri:
				return _startRequestUri.AbsoluteUri;
			}
			if (resourceLocation != null)
			{
				return resourceLocation;
			}
			if (RequestMethod == RequestMethod.Put || RequestMethod == RequestMethod.Patch)
			{
				return _startRequestUri.AbsoluteUri;
			}
			if (!string.IsNullOrEmpty(_lastKnownLocation))
			{
				return _lastKnownLocation;
			}
			return null;
		}

		private Response GetResponse(string uri, CancellationToken cancellationToken)
		{
			using HttpMessage httpMessage = CreateRequest(uri);
			_pipeline.Send(httpMessage, cancellationToken);
			if (httpMessage.Response.Status == 404 && RequestMethod == RequestMethod.Delete)
			{
				return new EmptyResponse(HttpStatusCode.NoContent, httpMessage.Response.ClientRequestId);
			}
			return httpMessage.Response;
		}

		private async ValueTask<Response> GetResponseAsync(string uri, CancellationToken cancellationToken)
		{
			using HttpMessage message = CreateRequest(uri);
			await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (message.Response.Status == 404 && RequestMethod == RequestMethod.Delete)
			{
				return new EmptyResponse(HttpStatusCode.NoContent, message.Response.ClientRequestId);
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

		private static bool IsFinalState(Response response, HeaderSource headerSource, out OperationState? failureState, out string? resourceLocation)
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
						string value3 = GetRequiredString(in value).ToLowerInvariant();
						if (FailureStates.Contains<string>(value3))
						{
							failureState = OperationState.Failure(response);
							return true;
						}
						if (!SuccessStates.Contains<string>(value3))
						{
							return false;
						}
						bool flag = (uint)(headerSource - 1) <= 1u;
						if (flag && rootElement.TryGetProperty("resourceLocation", out var value4))
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
			failureState = OperationState.Failure(response);
			return true;
		}

		private static string GetRequiredString(in JsonElement element)
		{
			return element.GetString() ?? throw new InvalidOperationException($"The requested operation requires an element of type 'String', but the target element has type '{element.ValueKind}'.");
		}

		private static bool ShouldIgnoreHeader(RequestMethod method, Response response)
		{
			if (method.Method == RequestMethod.Patch.Method)
			{
				return response.Status == 200;
			}
			return false;
		}

		private static HeaderSource GetHeaderSource(RequestMethod requestMethod, Uri requestUri, Response response, string? apiVersion, out string nextRequestUri, out bool isNextRequestPolling)
		{
			isNextRequestPolling = false;
			if (ShouldIgnoreHeader(requestMethod, response))
			{
				nextRequestUri = requestUri.AbsoluteUri;
				return HeaderSource.None;
			}
			ResponseHeaders headers = response.Headers;
			if (headers.TryGetValue("Operation-Location", out string value))
			{
				nextRequestUri = AppendOrReplaceApiVersion(value, apiVersion);
				isNextRequestPolling = true;
				return HeaderSource.OperationLocation;
			}
			if (headers.TryGetValue("Azure-AsyncOperation", out string value2))
			{
				nextRequestUri = AppendOrReplaceApiVersion(value2, apiVersion);
				isNextRequestPolling = true;
				return HeaderSource.AzureAsyncOperation;
			}
			if (headers.TryGetValue("Location", out string value3))
			{
				nextRequestUri = AppendOrReplaceApiVersion(value3, apiVersion);
				isNextRequestPolling = true;
				return HeaderSource.Location;
			}
			nextRequestUri = requestUri.AbsoluteUri;
			return HeaderSource.None;
		}

		private static void AssertNotNull<T>(T value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}
	}
	internal abstract class OperationInternalBase
	{
		private readonly ClientDiagnostics _diagnostics;

		private readonly IReadOnlyDictionary<string, string>? _scopeAttributes;

		private readonly DelayStrategy? _fallbackStrategy;

		private readonly AsyncLockWithValue<Response> _responseLock;

		private readonly string _waitForCompletionResponseScopeName;

		protected readonly string _updateStatusScopeName;

		protected readonly string _waitForCompletionScopeName;

		public abstract Response RawResponse { get; }

		public abstract bool HasCompleted { get; }

		protected OperationInternalBase(Response rawResponse)
		{
			_diagnostics = new ClientDiagnostics(ClientOptions.Default);
			_updateStatusScopeName = string.Empty;
			_waitForCompletionResponseScopeName = string.Empty;
			_waitForCompletionScopeName = string.Empty;
			_scopeAttributes = null;
			_fallbackStrategy = null;
			_responseLock = new AsyncLockWithValue<Response>(rawResponse);
		}

		protected OperationInternalBase(ClientDiagnostics clientDiagnostics, string operationTypeName, IEnumerable<KeyValuePair<string, string>>? scopeAttributes = null, DelayStrategy? fallbackStrategy = null)
		{
			_diagnostics = clientDiagnostics;
			_updateStatusScopeName = operationTypeName + ".UpdateStatus";
			_waitForCompletionResponseScopeName = operationTypeName + ".WaitForCompletionResponse";
			_waitForCompletionScopeName = operationTypeName + ".WaitForCompletion";
			_scopeAttributes = scopeAttributes?.ToDictionary<KeyValuePair<string, string>, string, string>((KeyValuePair<string, string> kvp) => kvp.Key, (KeyValuePair<string, string> kvp) => kvp.Value);
			_fallbackStrategy = fallbackStrategy;
			_responseLock = new AsyncLockWithValue<Response>();
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
			using AsyncLockWithValue<Response>.LockOrValue lockOrValue = await _responseLock.GetLockOrValueAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (lockOrValue.HasValue)
			{
				return lockOrValue.Value;
			}
			using DiagnosticScope scope = CreateScope(scopeName);
			_ = 1;
			try
			{
				OperationPoller operationPoller = new OperationPoller(_fallbackStrategy);
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

		protected DiagnosticScope CreateScope(string scopeName)
		{
			DiagnosticScope result = _diagnostics.CreateScope(scopeName);
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
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal readonly struct VoidValue
	{
	}
	internal enum OperationFinalStateVia
	{
		AzureAsyncOperation,
		Location,
		OriginalUri,
		OperationLocation,
		LocationOverride
	}
	internal class OperationInternal : OperationInternalBase
	{
		private class OperationToOperationOfTProxy : IOperation<VoidValue>
		{
			private readonly IOperation _operation;

			public OperationToOperationOfTProxy(IOperation operation)
			{
				_operation = operation;
			}

			public async ValueTask<OperationState<VoidValue>> UpdateStateAsync(bool async, CancellationToken cancellationToken)
			{
				OperationState operationState = await _operation.UpdateStateAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (!operationState.HasCompleted)
				{
					return OperationState<VoidValue>.Pending(operationState.RawResponse);
				}
				if (operationState.HasSucceeded)
				{
					return OperationState<VoidValue>.Success(operationState.RawResponse, default(VoidValue));
				}
				return OperationState<VoidValue>.Failure(operationState.RawResponse, operationState.OperationFailedException);
			}
		}

		private readonly OperationInternal<VoidValue> _internalOperation;

		public override Response RawResponse => _internalOperation.RawResponse;

		public override bool HasCompleted => _internalOperation.HasCompleted;

		public static OperationInternal Succeeded(Response rawResponse)
		{
			return new OperationInternal(OperationState.Success(rawResponse));
		}

		public static OperationInternal Failed(Response rawResponse, RequestFailedException operationFailedException)
		{
			return new OperationInternal(OperationState.Failure(rawResponse, operationFailedException));
		}

		public OperationInternal(IOperation operation, ClientDiagnostics clientDiagnostics, Response rawResponse, string? operationTypeName = null, IEnumerable<KeyValuePair<string, string>>? scopeAttributes = null, DelayStrategy? fallbackStrategy = null)
			: base(clientDiagnostics, operationTypeName ?? operation.GetType().Name, scopeAttributes, fallbackStrategy)
		{
			_internalOperation = new OperationInternal<VoidValue>(new OperationToOperationOfTProxy(operation), clientDiagnostics, rawResponse, operationTypeName ?? operation.GetType().Name, scopeAttributes, fallbackStrategy);
		}

		internal OperationInternal(OperationState finalState)
			: base(finalState.RawResponse)
		{
			_internalOperation = (finalState.HasSucceeded ? OperationInternal<VoidValue>.Succeeded(finalState.RawResponse, default(VoidValue)) : OperationInternal<VoidValue>.Failed(finalState.RawResponse, finalState.OperationFailedException));
		}

		protected override async ValueTask<Response> UpdateStatusAsync(bool async, CancellationToken cancellationToken)
		{
			return (!async) ? _internalOperation.UpdateStatus(cancellationToken) : (await _internalOperation.UpdateStatusAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}
	}
	internal interface IOperation
	{
		ValueTask<OperationState> UpdateStateAsync(bool async, CancellationToken cancellationToken);
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

		public static OperationState Success(Response rawResponse)
		{
			if (rawResponse == null)
			{
				throw new ArgumentNullException("rawResponse");
			}
			return new OperationState(rawResponse, hasCompleted: true, hasSucceeded: true, null);
		}

		public static OperationState Failure(Response rawResponse, RequestFailedException? operationFailedException = null)
		{
			if (rawResponse == null)
			{
				throw new ArgumentNullException("rawResponse");
			}
			return new OperationState(rawResponse, hasCompleted: true, hasSucceeded: false, operationFailedException);
		}

		public static OperationState Pending(Response rawResponse)
		{
			if (rawResponse == null)
			{
				throw new ArgumentNullException("rawResponse");
			}
			return new OperationState(rawResponse, hasCompleted: false, hasSucceeded: false, null);
		}
	}
	internal class OperationInternal<T> : OperationInternalBase
	{
		private class FinalOperation : IOperation<T>
		{
			public ValueTask<OperationState<T>> UpdateStateAsync(bool async, CancellationToken cancellationToken)
			{
				throw new NotSupportedException("The operation has already completed");
			}
		}

		private readonly IOperation<T> _operation;

		private readonly AsyncLockWithValue<OperationState<T>> _stateLock;

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

		public static OperationInternal<T> Succeeded(Response rawResponse, T value)
		{
			return new OperationInternal<T>(OperationState<T>.Success(rawResponse, value));
		}

		public static OperationInternal<T> Failed(Response rawResponse, RequestFailedException operationFailedException)
		{
			return new OperationInternal<T>(OperationState<T>.Failure(rawResponse, operationFailedException));
		}

		public OperationInternal(IOperation<T> operation, ClientDiagnostics clientDiagnostics, Response rawResponse, string? operationTypeName = null, IEnumerable<KeyValuePair<string, string>>? scopeAttributes = null, DelayStrategy? fallbackStrategy = null)
			: base(clientDiagnostics, operationTypeName ?? operation.GetType().Name, scopeAttributes, fallbackStrategy)
		{
			_operation = operation;
			_rawResponse = rawResponse;
			_stateLock = new AsyncLockWithValue<OperationState<T>>();
		}

		internal OperationInternal(OperationState<T> finalState)
			: base(finalState.RawResponse)
		{
			_operation = new FinalOperation();
			_rawResponse = finalState.RawResponse;
			_stateLock = new AsyncLockWithValue<OperationState<T>>(finalState);
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
			using AsyncLockWithValue<OperationState<T>>.LockOrValue asyncLock = await _stateLock.GetLockOrValueAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (asyncLock.HasValue)
			{
				return GetResponseFromState(asyncLock.Value);
			}
			using DiagnosticScope scope = CreateScope(_updateStatusScopeName);
			_ = 1;
			try
			{
				OperationState<T> operationState = await _operation.UpdateStateAsync(async, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (!operationState.HasCompleted)
				{
					Interlocked.Exchange(ref _rawResponse, operationState.RawResponse);
					return operationState.RawResponse;
				}
				if (!operationState.HasSucceeded && operationState.OperationFailedException == null)
				{
					operationState = OperationState<T>.Failure(operationState.RawResponse, new RequestFailedException(operationState.RawResponse));
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

		private static Response GetResponseFromState(OperationState<T> state)
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
		ValueTask<OperationState<T>> UpdateStateAsync(bool async, CancellationToken cancellationToken);
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

		public static OperationState<T> Success(Response rawResponse, T value)
		{
			if (rawResponse == null)
			{
				throw new ArgumentNullException("rawResponse");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return new OperationState<T>(rawResponse, hasCompleted: true, hasSucceeded: true, value, null);
		}

		public static OperationState<T> Failure(Response rawResponse, RequestFailedException? operationFailedException = null)
		{
			if (rawResponse == null)
			{
				throw new ArgumentNullException("rawResponse");
			}
			return new OperationState<T>(rawResponse, hasCompleted: true, hasSucceeded: false, default(T), operationFailedException);
		}

		public static OperationState<T> Pending(Response rawResponse)
		{
			if (rawResponse == null)
			{
				throw new ArgumentNullException("rawResponse");
			}
			return new OperationState<T>(rawResponse, hasCompleted: false, hasSucceeded: false, default(T), null);
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
	[AttributeUsage(AttributeTargets.Constructor)]
	internal class SerializationConstructorAttribute : Attribute
	{
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

		internal static Exception CreateOperationCanceledException(Exception? innerException, CancellationToken cancellationToken, string? message = null)
		{
			return new TaskCanceledException(message ?? s_cancellationMessage, innerException);
		}

		private static void ThrowOperationCanceledException(Exception? innerException, CancellationToken cancellationToken)
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
	internal sealed class OperationPoller
	{
		private readonly DelayStrategy _delayStrategy;

		public OperationPoller(DelayStrategy? strategy = null)
		{
			_delayStrategy = strategy ?? new FixedDelayWithNoJitterStrategy();
		}

		public ValueTask<Response> WaitForCompletionResponseAsync(Operation operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			return WaitForCompletionAsync(async: true, operation, delayHint, cancellationToken);
		}

		public Response WaitForCompletionResponse(Operation operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			return WaitForCompletionAsync(async: false, operation, delayHint, cancellationToken).EnsureCompleted();
		}

		public ValueTask<Response> WaitForCompletionResponseAsync(OperationInternalBase operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			return WaitForCompletionAsync(async: true, operation, delayHint, cancellationToken);
		}

		public Response WaitForCompletionResponse(OperationInternalBase operation, TimeSpan? delayHint, CancellationToken cancellationToken)
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

		public async ValueTask<Response<T>> WaitForCompletionAsync<T>(OperationInternal<T> operation, TimeSpan? delayHint, CancellationToken cancellationToken) where T : notnull
		{
			Response response = await WaitForCompletionAsync(async: true, operation, delayHint, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return Response.FromValue(operation.Value, response);
		}

		public Response<T> WaitForCompletion<T>(OperationInternal<T> operation, TimeSpan? delayHint, CancellationToken cancellationToken) where T : notnull
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
				DelayStrategy delayStrategy = (delayHint.HasValue ? new FixedDelayWithNoJitterStrategy(delayHint.Value) : _delayStrategy);
				int num = retryNumber + 1;
				retryNumber = num;
				await Delay(async, delayStrategy.GetNextDelay(response2, num), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			return operation.GetRawResponse();
		}

		private async ValueTask<Response> WaitForCompletionAsync(bool async, OperationInternalBase operation, TimeSpan? delayHint, CancellationToken cancellationToken)
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
				DelayStrategy delayStrategy = (delayHint.HasValue ? new FixedDelayWithNoJitterStrategy(delayHint.Value) : _delayStrategy);
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
	[AttributeUsage(AttributeTargets.Class)]
	internal class TypeReferenceTypeAttribute : Attribute
	{
		public bool IgnoreExtraProperties { get; }

		public string[] InternalPropertiesToInclude { get; }

		public TypeReferenceTypeAttribute()
			: this(ignoreExtraProperties: false, Array.Empty<string>())
		{
		}

		public TypeReferenceTypeAttribute(bool ignoreExtraProperties, string[] internalPropertiesToInclude)
		{
			IgnoreExtraProperties = ignoreExtraProperties;
			InternalPropertiesToInclude = internalPropertiesToInclude;
		}
	}
}
namespace Azure.Core.Shared
{
	internal static class EventSourceEventFormatting
	{
		public static string Format(EventWrittenEventArgs eventData)
		{
			object[] array = eventData.Payload?.ToArray() ?? Array.Empty<object>();
			ProcessPayloadArray(array);
			if (eventData.Message != null)
			{
				try
				{
					return string.Format(CultureInfo.InvariantCulture, eventData.Message, array);
				}
				catch (FormatException)
				{
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(eventData.EventName);
			if (!string.IsNullOrWhiteSpace(eventData.Message))
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("Message").Append(" = ").Append(eventData.Message);
			}
			if (eventData.PayloadNames != null)
			{
				for (int i = 0; i < eventData.PayloadNames.Count; i++)
				{
					stringBuilder.AppendLine();
					stringBuilder.Append(eventData.PayloadNames[i]).Append(" = ").Append(array[i]);
				}
			}
			return stringBuilder.ToString();
		}

		private static void ProcessPayloadArray(object?[] payloadArray)
		{
			for (int i = 0; i < payloadArray.Length; i++)
			{
				payloadArray[i] = FormatValue(payloadArray[i]);
			}
		}

		private static object? FormatValue(object? o)
		{
			if (o is byte[] array)
			{
				StringBuilder stringBuilder = new StringBuilder();
				byte[] array2 = array;
				foreach (byte b in array2)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
				}
				return stringBuilder.ToString();
			}
			return o;
		}
	}
}
namespace Azure.Core.JsonPatch
{
	internal readonly struct JsonPatchOperation
	{
		public JsonPatchOperationKind Kind { get; }

		public string Path { get; }

		public string? From { get; }

		public string? RawJsonValue { get; }

		public JsonPatchOperation(JsonPatchOperationKind kind, string path, string? from, string? rawJsonValue)
		{
			Kind = kind;
			Path = path;
			From = from;
			RawJsonValue = rawJsonValue;
		}
	}
	internal readonly struct JsonPatchOperationKind
	{
		private readonly string _operation;

		public static JsonPatchOperationKind Add { get; } = new JsonPatchOperationKind("add");

		public static JsonPatchOperationKind Remove { get; } = new JsonPatchOperationKind("remove");

		public static JsonPatchOperationKind Replace { get; } = new JsonPatchOperationKind("replace");

		public static JsonPatchOperationKind Move { get; } = new JsonPatchOperationKind("move");

		public static JsonPatchOperationKind Copy { get; } = new JsonPatchOperationKind("copy");

		public static JsonPatchOperationKind Test { get; } = new JsonPatchOperationKind("test");

		private JsonPatchOperationKind(string operation)
		{
			_operation = operation;
		}

		public override string ToString()
		{
			return _operation;
		}
	}
}
namespace Azure.Core.Pipeline
{
	internal class GeoRedundantFallbackPolicy : HttpPipelineSynchronousPolicy
	{
		private class HostAffinityKey
		{
		}

		private class PrimaryHostKey
		{
		}

		private class Fallback
		{
			private int _index;

			private long _ticks;

			private readonly TimeSpan _cooldown;

			public string[] Hosts { get; }

			public int Index => Volatile.Read(ref _index);

			public Fallback(string[] hosts, TimeSpan cooldown)
			{
				Hosts = hosts;
				_index = -1;
				_ticks = -1L;
				_cooldown = cooldown;
			}

			public void AdvanceIfNeeded(HttpMessage message)
			{
				int index = Index;
				long comparand = Volatile.Read(ref _ticks);
				if ((index == -1 && message.Request.Uri.Host.Equals(GetPrimaryHost(message), StringComparison.Ordinal)) || (index != -1 && message.Request.Uri.Host.Equals(Hosts[index], StringComparison.Ordinal)))
				{
					int num = index + 1;
					if (num >= Hosts.Length)
					{
						num = -1;
					}
					Interlocked.CompareExchange(ref _index, num, index);
					if (index == -1)
					{
						Interlocked.CompareExchange(ref _ticks, Stopwatch.GetTimestamp(), comparand);
					}
				}
			}

			public void ResetPrimaryIfNeeded()
			{
				long num = Volatile.Read(ref _ticks);
				int index = Index;
				if (num != -1)
				{
					long num2 = Stopwatch.GetTimestamp() - num;
					TimeSpan cooldown = _cooldown;
					if (num2 >= cooldown.Ticks)
					{
						Interlocked.CompareExchange(ref _index, -1, index);
						Interlocked.CompareExchange(ref _ticks, -1L, num);
					}
				}
			}
		}

		private readonly Fallback _writeFallback;

		private readonly Fallback _readFallback;

		public GeoRedundantFallbackPolicy(string[]? readFallbackHosts, string[]? writeFallbackHosts, TimeSpan? primaryCoolDown = null)
		{
			TimeSpan cooldown = primaryCoolDown ?? TimeSpan.FromMinutes(10.0);
			_writeFallback = new Fallback(writeFallbackHosts ?? Array.Empty<string>(), cooldown);
			_readFallback = new Fallback(readFallbackHosts ?? Array.Empty<string>(), cooldown);
		}

		public static void SetHostAffinity(HttpMessage message, bool hostAffinity)
		{
			message.SetProperty(typeof(HostAffinityKey), hostAffinity);
		}

		private static bool GetHostAffinity(HttpMessage message)
		{
			if (message.TryGetProperty(typeof(HostAffinityKey), out object value))
			{
				if (value is bool)
				{
					return (bool)value;
				}
				return false;
			}
			return false;
		}

		private static void SetPrimaryHost(HttpMessage message)
		{
			message.SetProperty(typeof(PrimaryHostKey), message.Request.Uri.Host);
		}

		private static string GetPrimaryHost(HttpMessage message)
		{
			message.TryGetProperty(typeof(PrimaryHostKey), out object value);
			return (string)value;
		}

		public override void OnSendingRequest(HttpMessage message)
		{
			if (message.HasResponse || GetHostAffinity(message) || message.Request.Uri.Host == null)
			{
				return;
			}
			Fallback fallback = ((message.Request.Method == RequestMethod.Get || message.Request.Method == RequestMethod.Head) ? _readFallback : _writeFallback);
			if (fallback.Hosts.Length != 0)
			{
				if (message.ProcessingContext.RetryNumber == 0)
				{
					SetPrimaryHost(message);
					UpdateHostIfNeeded(message, fallback);
				}
				else
				{
					fallback.AdvanceIfNeeded(message);
					UpdateHostIfNeeded(message, fallback);
				}
			}
		}

		private static void UpdateHostIfNeeded(HttpMessage message, Fallback fallback)
		{
			fallback.ResetPrimaryIfNeeded();
			int index = fallback.Index;
			message.Request.Uri.Host = ((index != -1) ? fallback.Hosts[index] : GetPrimaryHost(message));
		}
	}
	public class BearerTokenAuthenticationPolicy : HttpPipelinePolicy
	{
		private class AccessTokenCache
		{
			private readonly struct AuthHeaderValueInfo
			{
				public string HeaderValue { get; }

				public DateTimeOffset ExpiresOn { get; }

				public DateTimeOffset RefreshOn { get; }

				public AuthHeaderValueInfo(string headerValue, DateTimeOffset expiresOn, DateTimeOffset refreshOn)
				{
					HeaderValue = headerValue;
					ExpiresOn = expiresOn;
					RefreshOn = refreshOn;
				}
			}

			private class TokenRequestState
			{
				public TokenRequestContext CurrentContext { get; }

				public TaskCompletionSource<AuthHeaderValueInfo> CurrentTokenTcs { get; }

				public TaskCompletionSource<AuthHeaderValueInfo>? BackgroundTokenUpdateTcs { get; }

				public TokenRequestState(TokenRequestContext currentContext, TaskCompletionSource<AuthHeaderValueInfo> currentTokenTcs, TaskCompletionSource<AuthHeaderValueInfo>? backgroundTokenUpdateTcs)
				{
					CurrentContext = currentContext;
					CurrentTokenTcs = currentTokenTcs;
					BackgroundTokenUpdateTcs = backgroundTokenUpdateTcs;
				}

				public bool IsCurrentContextMismatched(TokenRequestContext context)
				{
					if ((context.Scopes == null || MemoryExtensions.AsSpan(context.Scopes).SequenceEqual(MemoryExtensions.AsSpan(CurrentContext.Scopes))) && (context.Claims == null || string.Equals(context.Claims, CurrentContext.Claims)))
					{
						if (context.TenantId != null)
						{
							return !string.Equals(context.TenantId, CurrentContext.TenantId);
						}
						return false;
					}
					return true;
				}

				public bool IsBackgroundTokenAvailable(DateTimeOffset now)
				{
					if (BackgroundTokenUpdateTcs != null && BackgroundTokenUpdateTcs.Task.Status == TaskStatus.RanToCompletion)
					{
						return BackgroundTokenUpdateTcs.Task.Result.ExpiresOn > now;
					}
					return false;
				}

				public bool IsCurrentTokenTcsFailedOrExpired(DateTimeOffset now)
				{
					if (CurrentTokenTcs.Task.Status == TaskStatus.RanToCompletion)
					{
						return now >= CurrentTokenTcs.Task.Result.ExpiresOn;
					}
					return true;
				}

				public bool TokenNeedsBackgroundRefresh(DateTimeOffset now)
				{
					if (now >= CurrentTokenTcs.Task.Result.RefreshOn)
					{
						return BackgroundTokenUpdateTcs == null;
					}
					return false;
				}

				public static TokenRequestState FromNewContext(TokenRequestContext newContext)
				{
					return new TokenRequestState(newContext, new TaskCompletionSource<AuthHeaderValueInfo>(TaskCreationOptions.RunContinuationsAsynchronously), null);
				}

				public TokenRequestState WithDefaultBackgroundUpdateTcs()
				{
					return new TokenRequestState(CurrentContext, CurrentTokenTcs, null);
				}

				public TokenRequestState WithBackgroundUpdateTcsAsCurrent()
				{
					return new TokenRequestState(CurrentContext, BackgroundTokenUpdateTcs, null);
				}

				public TokenRequestState WithNewCurrentTokenTcs()
				{
					return new TokenRequestState(CurrentContext, new TaskCompletionSource<AuthHeaderValueInfo>(TaskCreationOptions.RunContinuationsAsynchronously), BackgroundTokenUpdateTcs);
				}

				public TokenRequestState WithNewBackroundUpdateTokenTcs()
				{
					return new TokenRequestState(CurrentContext, CurrentTokenTcs, new TaskCompletionSource<AuthHeaderValueInfo>(TaskCreationOptions.RunContinuationsAsynchronously));
				}

				public async ValueTask<AuthHeaderValueInfo> GetCurrentHeaderValue(bool async, bool checkForCompletion = false, CancellationToken cancellationToken = default(CancellationToken))
				{
					if (async)
					{
						if (checkForCompletion & !CurrentTokenTcs.Task.IsCompleted)
						{
							await CurrentTokenTcs.Task.AwaitWithCancellation(cancellationToken);
						}
						return await CurrentTokenTcs.Task.ConfigureAwait(continueOnCapturedContext: false);
					}
					if (checkForCompletion & !CurrentTokenTcs.Task.IsCompleted)
					{
						try
						{
							CurrentTokenTcs.Task.Wait(cancellationToken);
						}
						catch (AggregateException)
						{
						}
					}
					return CurrentTokenTcs.Task.EnsureCompleted();
				}
			}

			private readonly object _syncObj = new object();

			private readonly TokenCredential _credential;

			private readonly TimeSpan _tokenRefreshOffset;

			private readonly TimeSpan _tokenRefreshRetryDelay;

			private TokenRequestState? _state;

			public AccessTokenCache(TokenCredential credential, TimeSpan tokenRefreshOffset, TimeSpan tokenRefreshRetryDelay)
			{
				_credential = credential;
				_tokenRefreshOffset = tokenRefreshOffset;
				_tokenRefreshRetryDelay = tokenRefreshRetryDelay;
			}

			public async ValueTask<string> GetAuthHeaderValueAsync(HttpMessage message, TokenRequestContext context, bool async)
			{
				int maxCancellationRetries = 3;
				TokenRequestState localState;
				AuthHeaderValueInfo headerValueInfo;
				while (true)
				{
					if (RefreshTokenRequestState(context, out localState))
					{
						if (localState.BackgroundTokenUpdateTcs != null)
						{
							break;
						}
						try
						{
							await SetResultOnTcsFromCredentialAsync(context, localState.CurrentTokenTcs, async, message.CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						}
						catch (OperationCanceledException)
						{
							localState.CurrentTokenTcs.SetCanceled();
						}
						catch (Exception exception)
						{
							localState.CurrentTokenTcs.SetException(exception);
						}
					}
					try
					{
						headerValueInfo = await localState.GetCurrentHeaderValue(async, checkForCompletion: true, message.CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						return headerValueInfo.HeaderValue;
					}
					catch (TaskCanceledException) when (!message.CancellationToken.IsCancellationRequested)
					{
						maxCancellationRetries--;
						if (!message.CancellationToken.CanBeCanceled && maxCancellationRetries <= 0)
						{
							throw;
						}
					}
				}
				headerValueInfo = await localState.GetCurrentHeaderValue(async).ConfigureAwait(continueOnCapturedContext: false);
				Task.Run(() => GetHeaderValueFromCredentialInBackgroundAsync(localState.BackgroundTokenUpdateTcs, headerValueInfo, context, async));
				return headerValueInfo.HeaderValue;
			}

			private bool RefreshTokenRequestState(TokenRequestContext context, out TokenRequestState updatedState)
			{
				TokenRequestState state = _state;
				if (state != null && state.CurrentTokenTcs.Task.IsCompleted && !state.IsCurrentContextMismatched(context))
				{
					DateTimeOffset utcNow = DateTimeOffset.UtcNow;
					if (!state.IsBackgroundTokenAvailable(utcNow) && !state.IsCurrentTokenTcsFailedOrExpired(utcNow) && !state.TokenNeedsBackgroundRefresh(utcNow))
					{
						updatedState = state;
						return false;
					}
				}
				lock (_syncObj)
				{
					if (_state == null || _state.IsCurrentContextMismatched(context))
					{
						_state = TokenRequestState.FromNewContext(context);
						updatedState = _state;
						return true;
					}
					if (!_state.CurrentTokenTcs.Task.IsCompleted)
					{
						if (_state.BackgroundTokenUpdateTcs != null)
						{
							_state = _state.WithDefaultBackgroundUpdateTcs();
						}
						updatedState = _state;
						return false;
					}
					DateTimeOffset utcNow2 = DateTimeOffset.UtcNow;
					if (_state.IsBackgroundTokenAvailable(utcNow2))
					{
						_state = _state.WithBackgroundUpdateTcsAsCurrent();
					}
					if (_state.IsCurrentTokenTcsFailedOrExpired(utcNow2))
					{
						_state = _state.WithNewCurrentTokenTcs();
						updatedState = _state;
						return true;
					}
					if (_state.TokenNeedsBackgroundRefresh(utcNow2))
					{
						_state = _state.WithNewBackroundUpdateTokenTcs();
						updatedState = _state;
						return true;
					}
					updatedState = _state;
					return false;
				}
			}

			private async ValueTask GetHeaderValueFromCredentialInBackgroundAsync(TaskCompletionSource<AuthHeaderValueInfo> backgroundUpdateTcs, AuthHeaderValueInfo currentAuthHeaderInfo, TokenRequestContext context, bool async)
			{
				CancellationTokenSource cts = new CancellationTokenSource(_tokenRefreshRetryDelay);
				try
				{
					await SetResultOnTcsFromCredentialAsync(context, backgroundUpdateTcs, async, cts.Token).ConfigureAwait(continueOnCapturedContext: false);
				}
				catch (OperationCanceledException ex) when (cts.IsCancellationRequested)
				{
					backgroundUpdateTcs.SetResult(new AuthHeaderValueInfo(currentAuthHeaderInfo.HeaderValue, currentAuthHeaderInfo.ExpiresOn, DateTimeOffset.UtcNow));
					AzureCoreEventSource.Singleton.BackgroundRefreshFailed(context.ParentRequestId ?? string.Empty, ex.ToString());
				}
				catch (Exception ex2)
				{
					backgroundUpdateTcs.SetResult(new AuthHeaderValueInfo(currentAuthHeaderInfo.HeaderValue, currentAuthHeaderInfo.ExpiresOn, DateTimeOffset.UtcNow + _tokenRefreshRetryDelay));
					AzureCoreEventSource.Singleton.BackgroundRefreshFailed(context.ParentRequestId ?? string.Empty, ex2.ToString());
				}
				finally
				{
					cts.Dispose();
				}
			}

			private async ValueTask SetResultOnTcsFromCredentialAsync(TokenRequestContext context, TaskCompletionSource<AuthHeaderValueInfo> targetTcs, bool async, CancellationToken cancellationToken)
			{
				AccessToken accessToken = ((!async) ? _credential.GetToken(context, cancellationToken) : (await _credential.GetTokenAsync(context, cancellationToken).ConfigureAwait(continueOnCapturedContext: false)));
				AccessToken accessToken2 = accessToken;
				targetTcs.SetResult(new AuthHeaderValueInfo("Bearer " + accessToken2.Token, accessToken2.ExpiresOn, accessToken2.RefreshOn.HasValue ? accessToken2.RefreshOn.Value : (accessToken2.ExpiresOn - _tokenRefreshOffset)));
			}
		}

		private string[] _scopes;

		private readonly AccessTokenCache _accessTokenCache;

		public BearerTokenAuthenticationPolicy(TokenCredential credential, string scope)
			: this(credential, new string[1] { scope })
		{
		}

		public BearerTokenAuthenticationPolicy(TokenCredential credential, IEnumerable<string> scopes)
			: this(credential, scopes, TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(30.0))
		{
		}

		internal BearerTokenAuthenticationPolicy(TokenCredential credential, IEnumerable<string> scopes, TimeSpan tokenRefreshOffset, TimeSpan tokenRefreshRetryDelay)
		{
			Argument.AssertNotNull(credential, "credential");
			Argument.AssertNotNull(scopes, "scopes");
			_scopes = scopes.ToArray();
			_accessTokenCache = new AccessTokenCache(credential, tokenRefreshOffset, tokenRefreshRetryDelay);
		}

		public override ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			return ProcessAsync(message, pipeline, async: true);
		}

		public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			ProcessAsync(message, pipeline, async: false).EnsureCompleted();
		}

		protected virtual ValueTask AuthorizeRequestAsync(HttpMessage message)
		{
			TokenRequestContext context = new TokenRequestContext(_scopes, message.Request.ClientRequestId);
			return AuthenticateAndAuthorizeRequestAsync(message, context);
		}

		protected virtual void AuthorizeRequest(HttpMessage message)
		{
			TokenRequestContext context = new TokenRequestContext(_scopes, message.Request.ClientRequestId);
			AuthenticateAndAuthorizeRequest(message, context);
		}

		protected virtual ValueTask<bool> AuthorizeRequestOnChallengeAsync(HttpMessage message)
		{
			return default(ValueTask<bool>);
		}

		protected virtual bool AuthorizeRequestOnChallenge(HttpMessage message)
		{
			return false;
		}

		private async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline, bool async)
		{
			if (message.Request.Uri.Scheme != Uri.UriSchemeHttps)
			{
				throw new InvalidOperationException("Bearer token authentication is not permitted for non TLS protected (https) endpoints.");
			}
			if (async)
			{
				await AuthorizeRequestAsync(message).ConfigureAwait(continueOnCapturedContext: false);
				await HttpPipelinePolicy.ProcessNextAsync(message, pipeline).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				AuthorizeRequest(message);
				HttpPipelinePolicy.ProcessNext(message, pipeline);
			}
			if (message.Response.Status != 401 || !message.Response.Headers.Contains(HttpHeader.Names.WwwAuthenticate))
			{
				return;
			}
			if (async)
			{
				if (await AuthorizeRequestOnChallengeAsync(message).ConfigureAwait(continueOnCapturedContext: false))
				{
					await HttpPipelinePolicy.ProcessNextAsync(message, pipeline).ConfigureAwait(continueOnCapturedContext: false);
				}
			}
			else if (AuthorizeRequestOnChallenge(message))
			{
				HttpPipelinePolicy.ProcessNext(message, pipeline);
			}
		}

		protected async ValueTask AuthenticateAndAuthorizeRequestAsync(HttpMessage message, TokenRequestContext context)
		{
			string value = await _accessTokenCache.GetAuthHeaderValueAsync(message, context, async: true).ConfigureAwait(continueOnCapturedContext: false);
			message.Request.Headers.SetValue(HttpHeader.Names.Authorization, value);
		}

		protected void AuthenticateAndAuthorizeRequest(HttpMessage message, TokenRequestContext context)
		{
			string value = _accessTokenCache.GetAuthHeaderValueAsync(message, context, async: false).EnsureCompleted();
			message.Request.Headers.SetValue(HttpHeader.Names.Authorization, value);
		}
	}
	public sealed class DisposableHttpPipeline : HttpPipeline, IDisposable
	{
		private bool isTransportOwnedInternally;

		internal DisposableHttpPipeline(HttpPipelineTransport transport, int perCallIndex, int perRetryIndex, HttpPipelinePolicy[] policies, ResponseClassifier responseClassifier, bool isTransportOwnedInternally)
			: base(transport, perCallIndex, perRetryIndex, policies, responseClassifier)
		{
			this.isTransportOwnedInternally = isTransportOwnedInternally;
		}

		public void Dispose()
		{
			if (isTransportOwnedInternally)
			{
				(_transport as IDisposable)?.Dispose();
			}
		}
	}
	public class HttpClientTransport : HttpPipelineTransport, IDisposable
	{
		private sealed class HttpClientTransportRequest : Request
		{
			private readonly struct IgnoreCaseString : IEquatable<IgnoreCaseString>
			{
				private readonly string _value;

				public IgnoreCaseString(string value)
				{
					_value = value;
				}

				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				public bool Equals(IgnoreCaseString other)
				{
					return string.Equals(_value, other._value, StringComparison.OrdinalIgnoreCase);
				}

				public override bool Equals(object? obj)
				{
					if (obj is IgnoreCaseString other)
					{
						return Equals(other);
					}
					return false;
				}

				public override int GetHashCode()
				{
					return _value.GetHashCode();
				}

				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				public static bool operator ==(IgnoreCaseString left, IgnoreCaseString right)
				{
					return left.Equals(right);
				}

				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				public static bool operator !=(IgnoreCaseString left, IgnoreCaseString right)
				{
					return !left.Equals(right);
				}

				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				public static implicit operator string(IgnoreCaseString ics)
				{
					return ics._value;
				}
			}

			private sealed class PipelineContentAdapter : HttpContent
			{
				private readonly RequestContent _pipelineContent;

				private readonly CancellationToken _cancellationToken;

				public PipelineContentAdapter(RequestContent pipelineContent, CancellationToken cancellationToken)
				{
					_pipelineContent = pipelineContent;
					_cancellationToken = cancellationToken;
				}

				protected override async Task SerializeToStreamAsync(Stream stream, TransportContext? context)
				{
					await _pipelineContent.WriteToAsync(stream, _cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}

				protected override bool TryComputeLength(out long length)
				{
					return _pipelineContent.TryComputeLength(out length);
				}
			}

			private string? _clientRequestId;

			private ArrayBackedPropertyBag<IgnoreCaseString, object> _headers;

			private static readonly HttpMethod s_patch = new HttpMethod("PATCH");

			public override string ClientRequestId
			{
				get
				{
					return _clientRequestId ?? (_clientRequestId = Guid.NewGuid().ToString());
				}
				set
				{
					Argument.AssertNotNull(value, "value");
					_clientRequestId = value;
				}
			}

			public HttpClientTransportRequest()
			{
				Method = RequestMethod.Get;
				_headers = new ArrayBackedPropertyBag<IgnoreCaseString, object>();
			}

			protected internal override void SetHeader(string name, string value)
			{
				_headers.Set(new IgnoreCaseString(name), value);
			}

			protected internal override void AddHeader(string name, string value)
			{
				if (_headers.TryAdd(new IgnoreCaseString(name), value, out object existingValue))
				{
					return;
				}
				if (!(existingValue is string item))
				{
					if (existingValue is List<string> list)
					{
						list.Add(value);
					}
				}
				else
				{
					_headers.Set(new IgnoreCaseString(name), new List<string> { item, value });
				}
			}

			protected internal override bool TryGetHeader(string name, [NotNullWhen(true)] out string? value)
			{
				if (_headers.TryGetValue(new IgnoreCaseString(name), out object value2))
				{
					value = GetHttpHeaderValue(name, value2);
					return true;
				}
				value = null;
				return false;
			}

			protected internal override bool TryGetHeaderValues(string name, [NotNullWhen(true)] out IEnumerable<string>? values)
			{
				if (_headers.TryGetValue(new IgnoreCaseString(name), out object value))
				{
					IEnumerable<string> enumerable;
					if (!(value is string text))
					{
						if (!(value is List<string> list))
						{
							throw new InvalidOperationException($"Unexpected type for header {name}: {value.GetType()}");
						}
						enumerable = list;
					}
					else
					{
						enumerable = new string[1] { text };
					}
					values = enumerable;
					return true;
				}
				values = null;
				return false;
			}

			protected internal override bool ContainsHeader(string name)
			{
				object value;
				return _headers.TryGetValue(new IgnoreCaseString(name), out value);
			}

			protected internal override bool RemoveHeader(string name)
			{
				return _headers.TryRemove(new IgnoreCaseString(name));
			}

			protected internal override IEnumerable<HttpHeader> EnumerateHeaders()
			{
				for (int i = 0; i < _headers.Count; i++)
				{
					_headers.GetAt(i, out IgnoreCaseString key, out object value);
					yield return new HttpHeader(key, GetHttpHeaderValue(key, value));
				}
			}

			public HttpRequestMessage BuildRequestMessage(CancellationToken cancellation)
			{
				HttpMethod method = ToHttpClientMethod(Method);
				Uri requestUri = Uri.ToUri();
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage(method, requestUri);
				PipelineContentAdapter pipelineContentAdapter = (PipelineContentAdapter)(httpRequestMessage.Content = ((Content != null) ? new PipelineContentAdapter(Content, cancellation) : null));
				for (int i = 0; i < _headers.Count; i++)
				{
					_headers.GetAt(i, out IgnoreCaseString key, out object value);
					AuthenticationHeaderValue parsedValue;
					if (!(value is string text))
					{
						if (value is List<string> values && !httpRequestMessage.Headers.TryAddWithoutValidation(key, values) && pipelineContentAdapter != null && !pipelineContentAdapter.Headers.TryAddWithoutValidation(key, values))
						{
							throw new InvalidOperationException($"Unable to add header {key} to header collection.");
						}
					}
					else if (key == HttpHeader.Names.Authorization && AuthenticationHeaderValue.TryParse(text, out parsedValue))
					{
						httpRequestMessage.Headers.Authorization = parsedValue;
					}
					else if (!httpRequestMessage.Headers.TryAddWithoutValidation(key, text) && pipelineContentAdapter != null && !pipelineContentAdapter.Headers.TryAddWithoutValidation(key, text))
					{
						throw new InvalidOperationException($"Unable to add header {key} to header collection.");
					}
				}
				AddPropertiesForBlazor(httpRequestMessage);
				return httpRequestMessage;
			}

			private static void AddPropertiesForBlazor(HttpRequestMessage currentRequest)
			{
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
				{
					SetPropertiesOrOptions(currentRequest, "WebAssemblyFetchOptions", new Dictionary<string, object> { { "cache", "no-store" } });
					SetPropertiesOrOptions(currentRequest, "WebAssemblyEnableStreamingResponse", value: true);
				}
			}

			private static string GetHttpHeaderValue(string headerName, object value)
			{
				if (!(value is string result))
				{
					if (value is List<string> values)
					{
						return string.Join(",", values);
					}
					throw new InvalidOperationException($"Unexpected type for header {headerName}: {value?.GetType()}");
				}
				return result;
			}

			public override void Dispose()
			{
				_headers.Dispose();
				RequestContent content = Content;
				if (content != null)
				{
					Content = null;
					content.Dispose();
				}
			}

			public override string ToString()
			{
				return BuildRequestMessage(default(CancellationToken)).ToString();
			}

			private static HttpMethod ToHttpClientMethod(RequestMethod requestMethod)
			{
				string method = requestMethod.Method;
				if (method.Length == 3)
				{
					if (string.Equals(method, "GET", StringComparison.OrdinalIgnoreCase))
					{
						return HttpMethod.Get;
					}
					if (string.Equals(method, "PUT", StringComparison.OrdinalIgnoreCase))
					{
						return HttpMethod.Put;
					}
				}
				else if (method.Length == 4)
				{
					if (string.Equals(method, "POST", StringComparison.OrdinalIgnoreCase))
					{
						return HttpMethod.Post;
					}
					if (string.Equals(method, "HEAD", StringComparison.OrdinalIgnoreCase))
					{
						return HttpMethod.Head;
					}
				}
				else
				{
					if (string.Equals(method, "PATCH", StringComparison.OrdinalIgnoreCase))
					{
						return s_patch;
					}
					if (string.Equals(method, "DELETE", StringComparison.OrdinalIgnoreCase))
					{
						return HttpMethod.Delete;
					}
				}
				return new HttpMethod(method);
			}
		}

		private sealed class HttpClientTransportResponse : Response
		{
			private readonly HttpResponseMessage _responseMessage;

			private readonly HttpContent _responseContent;

			private Stream? _contentStream;

			public override int Status => (int)_responseMessage.StatusCode;

			public override string ReasonPhrase => _responseMessage.ReasonPhrase ?? string.Empty;

			public override Stream? ContentStream
			{
				get
				{
					return _contentStream;
				}
				set
				{
					_responseMessage.Content = null;
					_contentStream = value;
				}
			}

			public override string ClientRequestId { get; set; }

			public HttpClientTransportResponse(string requestId, HttpResponseMessage responseMessage, Stream? contentStream)
			{
				ClientRequestId = requestId ?? throw new ArgumentNullException("requestId");
				_responseMessage = responseMessage ?? throw new ArgumentNullException("responseMessage");
				_contentStream = contentStream;
				_responseContent = _responseMessage.Content;
			}

			protected internal override bool TryGetHeader(string name, [NotNullWhen(true)] out string? value)
			{
				return HttpClientTransport.TryGetHeader((HttpHeaders)_responseMessage.Headers, _responseContent, name, out value);
			}

			protected internal override bool TryGetHeaderValues(string name, [NotNullWhen(true)] out IEnumerable<string>? values)
			{
				return HttpClientTransport.TryGetHeader((HttpHeaders)_responseMessage.Headers, _responseContent, name, out values);
			}

			protected internal override bool ContainsHeader(string name)
			{
				return HttpClientTransport.ContainsHeader((HttpHeaders)_responseMessage.Headers, _responseContent, name);
			}

			protected internal override IEnumerable<HttpHeader> EnumerateHeaders()
			{
				return GetHeaders(_responseMessage.Headers, _responseContent);
			}

			public override void Dispose()
			{
				_responseMessage?.Dispose();
				Response.DisposeStreamIfNotBuffered(ref _contentStream);
			}

			public override string ToString()
			{
				return _responseMessage.ToString();
			}
		}

		internal const string MessageForServerCertificateCallback = "MessageForServerCertificateCallback";

		public static readonly HttpClientTransport Shared = new HttpClientTransport();

		internal HttpClient Client { get; }

		public HttpClientTransport()
			: this(CreateDefaultClient())
		{
		}

		internal HttpClientTransport(HttpPipelineTransportOptions? options = null)
			: this(CreateDefaultClient(options))
		{
		}

		public HttpClientTransport(HttpMessageHandler messageHandler)
		{
			Client = new HttpClient(messageHandler) ?? throw new ArgumentNullException("messageHandler");
		}

		public HttpClientTransport(HttpClient client)
		{
			Client = client ?? throw new ArgumentNullException("client");
		}

		public sealed override Request CreateRequest()
		{
			return new HttpClientTransportRequest();
		}

		public override void Process(HttpMessage message)
		{
			ProcessAsync(message).AsTask().GetAwaiter().GetResult();
		}

		public override ValueTask ProcessAsync(HttpMessage message)
		{
			return ProcessSyncOrAsync(message, async: true);
		}

		private async ValueTask ProcessSyncOrAsync(HttpMessage message, bool async)
		{
			using HttpRequestMessage httpRequest = BuildRequestMessage(message);
			SetPropertiesOrOptions(httpRequest, "MessageForServerCertificateCallback", message);
			Stream contentStream = null;
			message.ClearResponse();
			HttpResponseMessage responseMessage;
			try
			{
				responseMessage = await Client.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, message.CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (responseMessage.Content != null)
				{
					contentStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(continueOnCapturedContext: false);
				}
			}
			catch (OperationCanceledException ex) when (CancellationHelper.ShouldWrapInOperationCanceledException(ex, message.CancellationToken))
			{
				throw CancellationHelper.CreateOperationCanceledException(ex, message.CancellationToken);
			}
			catch (HttpRequestException ex2)
			{
				throw new RequestFailedException(ex2.Message, ex2);
			}
			message.Response = new HttpClientTransportResponse(message.Request.ClientRequestId, responseMessage, contentStream);
		}

		private static HttpClient CreateDefaultClient(HttpPipelineTransportOptions? options = null)
		{
			HttpMessageHandler httpMessageHandler = CreateDefaultHandler(options);
			SetProxySettings(httpMessageHandler);
			ServicePointHelpers.SetLimits(httpMessageHandler);
			return new HttpClient(httpMessageHandler)
			{
				Timeout = Timeout.InfiniteTimeSpan
			};
		}

		private static HttpMessageHandler CreateDefaultHandler(HttpPipelineTransportOptions? options = null)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
			{
				return new HttpClientHandler();
			}
			return ApplyOptionsToHandler(new HttpClientHandler
			{
				AllowAutoRedirect = false,
				UseCookies = UseCookies()
			}, options);
		}

		private static void SetProxySettings(HttpMessageHandler messageHandler)
		{
			if (!RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")) && HttpEnvironmentProxy.TryCreate(out var proxy) && messageHandler is HttpClientHandler httpClientHandler)
			{
				httpClientHandler.Proxy = proxy;
			}
		}

		private static HttpClientHandler ApplyOptionsToHandler(HttpClientHandler httpHandler, HttpPipelineTransportOptions? options)
		{
			if (options == null || RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
			{
				return httpHandler;
			}
			if (options.ServerCertificateCustomValidationCallback != null)
			{
				httpHandler.ServerCertificateCustomValidationCallback = (HttpRequestMessage _, X509Certificate2 certificate2, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors) => options.ServerCertificateCustomValidationCallback(new ServerCertificateCustomValidationArgs(certificate2, x509Chain, sslPolicyErrors));
			}
			foreach (X509Certificate2 clientCertificate in options.ClientCertificates)
			{
				httpHandler.ClientCertificates.Add(clientCertificate);
			}
			return httpHandler;
		}

		public void Dispose()
		{
			if (this != Shared)
			{
				Client.Dispose();
			}
			GC.SuppressFinalize(this);
		}

		private static void SetPropertiesOrOptions<T>(HttpRequestMessage httpRequest, string name, T value)
		{
			httpRequest.Properties[name] = value;
		}

		private static bool UseCookies()
		{
			return AppContextSwitchHelper.GetConfigValue("Azure.Core.Pipeline.HttpClientTransport.EnableCookies", "AZURE_CORE_HTTPCLIENT_ENABLE_COOKIES");
		}

		private static HttpRequestMessage BuildRequestMessage(HttpMessage message)
		{
			return ((message.Request as HttpClientTransportRequest) ?? throw new InvalidOperationException("the request is not compatible with the transport")).BuildRequestMessage(message.CancellationToken);
		}

		internal static bool TryGetHeader(HttpHeaders headers, HttpContent? content, string name, [NotNullWhen(true)] out string? value)
		{
			if (TryGetHeader(headers, content, name, out IEnumerable<string> values))
			{
				value = JoinHeaderValues(values);
				return true;
			}
			value = null;
			return false;
		}

		internal static bool TryGetHeader(HttpHeaders headers, HttpContent? content, string name, [NotNullWhen(true)] out IEnumerable<string>? values)
		{
			if (!headers.TryGetValues(name, out values))
			{
				return content?.Headers.TryGetValues(name, out values) ?? false;
			}
			return true;
		}

		internal static IEnumerable<HttpHeader> GetHeaders(HttpHeaders headers, HttpContent? content)
		{
			foreach (KeyValuePair<string, IEnumerable<string>> header in headers)
			{
				yield return new HttpHeader(header.Key, JoinHeaderValues(header.Value));
			}
			if (content == null)
			{
				yield break;
			}
			foreach (KeyValuePair<string, IEnumerable<string>> header2 in content.Headers)
			{
				yield return new HttpHeader(header2.Key, JoinHeaderValues(header2.Value));
			}
		}

		internal static bool RemoveHeader(HttpHeaders headers, HttpContent? content, string name)
		{
			if (headers.TryGetValues(name, out var values) && headers.Remove(name))
			{
				return true;
			}
			if (content != null && content.Headers.TryGetValues(name, out values))
			{
				return content.Headers.Remove(name);
			}
			return false;
		}

		internal static bool ContainsHeader(HttpHeaders headers, HttpContent? content, string name)
		{
			if (headers.TryGetValues(name, out var values))
			{
				return true;
			}
			return content?.Headers.TryGetValues(name, out values) ?? false;
		}

		private static string JoinHeaderValues(IEnumerable<string> values)
		{
			return string.Join(",", values);
		}
	}
	public class HttpPipeline
	{
		private class HttpMessagePropertiesScope : IDisposable
		{
			private readonly HttpMessagePropertiesScope? _parent;

			private bool _disposed;

			public Dictionary<string, object?> Properties { get; }

			internal HttpMessagePropertiesScope(IDictionary<string, object?> messageProperties, HttpMessagePropertiesScope? parent)
			{
				if (parent != null)
				{
					Properties = new Dictionary<string, object>(parent.Properties);
					foreach (KeyValuePair<string, object> messageProperty in messageProperties)
					{
						Properties[messageProperty.Key] = messageProperty.Value;
					}
				}
				else
				{
					Properties = new Dictionary<string, object>(messageProperties);
				}
				_parent = parent;
			}

			public void Dispose()
			{
				if (!_disposed)
				{
					CurrentHttpMessagePropertiesScope.Value = _parent;
					_disposed = true;
				}
			}
		}

		private static readonly AsyncLocal<HttpMessagePropertiesScope?> CurrentHttpMessagePropertiesScope = new AsyncLocal<HttpMessagePropertiesScope>();

		private protected readonly HttpPipelineTransport _transport;

		private readonly ReadOnlyMemory<HttpPipelinePolicy> _pipeline;

		private readonly bool _internallyConstructed;

		private readonly int _perCallIndex;

		private readonly int _perRetryIndex;

		public ResponseClassifier ResponseClassifier { get; }

		public HttpPipeline(HttpPipelineTransport transport, HttpPipelinePolicy[]? policies = null, ResponseClassifier? responseClassifier = null)
		{
			_transport = transport ?? throw new ArgumentNullException("transport");
			ResponseClassifier = responseClassifier ?? Azure.Core.ResponseClassifier.Shared;
			if (policies == null)
			{
				policies = Array.Empty<HttpPipelinePolicy>();
			}
			HttpPipelinePolicy[] array = new HttpPipelinePolicy[policies.Length + 1];
			array[policies.Length] = new HttpPipelineTransportPolicy(_transport, ClientDiagnostics.CreateMessageSanitizer(new DiagnosticsOptions()));
			policies.CopyTo(array, 0);
			_pipeline = array;
		}

		internal HttpPipeline(HttpPipelineTransport transport, int perCallIndex, int perRetryIndex, HttpPipelinePolicy[] pipeline, ResponseClassifier responseClassifier)
		{
			ResponseClassifier = responseClassifier ?? throw new ArgumentNullException("responseClassifier");
			_transport = transport ?? throw new ArgumentNullException("transport");
			_pipeline = pipeline ?? throw new ArgumentNullException("pipeline");
			_perCallIndex = perCallIndex;
			_perRetryIndex = perRetryIndex;
			_internallyConstructed = true;
		}

		public Request CreateRequest()
		{
			return _transport.CreateRequest();
		}

		public HttpMessage CreateMessage()
		{
			return new HttpMessage(CreateRequest(), ResponseClassifier);
		}

		public HttpMessage CreateMessage(RequestContext? context)
		{
			return CreateMessage(context, null);
		}

		public HttpMessage CreateMessage(RequestContext? context, ResponseClassifier? classifier = null)
		{
			HttpMessage httpMessage = CreateMessage();
			if (classifier != null)
			{
				httpMessage.ResponseClassifier = classifier;
			}
			httpMessage.ApplyRequestContext(context, classifier);
			return httpMessage;
		}

		public ValueTask SendAsync(HttpMessage message, CancellationToken cancellationToken)
		{
			message.CancellationToken = cancellationToken;
			message.ProcessingStartTime = DateTimeOffset.UtcNow;
			AddHttpMessageProperties(message);
			if (message.Policies == null || message.Policies.Count == 0)
			{
				return _pipeline.Span[0].ProcessAsync(message, _pipeline.Slice(1));
			}
			return SendAsync(message);
		}

		private async ValueTask SendAsync(HttpMessage message)
		{
			int minimumLength = _pipeline.Length + message.Policies.Count;
			HttpPipelinePolicy[] policies = ArrayPool<HttpPipelinePolicy>.Shared.Rent(minimumLength);
			try
			{
				ReadOnlyMemory<HttpPipelinePolicy> readOnlyMemory = CreateRequestPipeline(policies, message.Policies);
				await readOnlyMemory.Span[0].ProcessAsync(message, readOnlyMemory.Slice(1)).ConfigureAwait(continueOnCapturedContext: false);
			}
			finally
			{
				ArrayPool<HttpPipelinePolicy>.Shared.Return(policies);
			}
		}

		public void Send(HttpMessage message, CancellationToken cancellationToken)
		{
			message.CancellationToken = cancellationToken;
			message.ProcessingStartTime = DateTimeOffset.UtcNow;
			AddHttpMessageProperties(message);
			if (message.Policies == null || message.Policies.Count == 0)
			{
				_pipeline.Span[0].Process(message, _pipeline.Slice(1));
				return;
			}
			int minimumLength = _pipeline.Length + message.Policies.Count;
			HttpPipelinePolicy[] array = ArrayPool<HttpPipelinePolicy>.Shared.Rent(minimumLength);
			try
			{
				ReadOnlyMemory<HttpPipelinePolicy> readOnlyMemory = CreateRequestPipeline(array, message.Policies);
				readOnlyMemory.Span[0].Process(message, readOnlyMemory.Slice(1));
			}
			finally
			{
				ArrayPool<HttpPipelinePolicy>.Shared.Return(array);
			}
		}

		public async ValueTask<Response> SendRequestAsync(Request request, CancellationToken cancellationToken)
		{
			HttpMessage message = new HttpMessage(request, ResponseClassifier);
			await SendAsync(message, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return message.Response;
		}

		public Response SendRequest(Request request, CancellationToken cancellationToken)
		{
			HttpMessage httpMessage = new HttpMessage(request, ResponseClassifier);
			Send(httpMessage, cancellationToken);
			return httpMessage.Response;
		}

		public static IDisposable CreateClientRequestIdScope(string? clientRequestId)
		{
			return CreateHttpMessagePropertiesScope(new Dictionary<string, object> { { "x-ms-client-request-id", clientRequestId } });
		}

		public static IDisposable CreateHttpMessagePropertiesScope(IDictionary<string, object?> messageProperties)
		{
			Argument.AssertNotNull(messageProperties, "messageProperties");
			CurrentHttpMessagePropertiesScope.Value = new HttpMessagePropertiesScope(messageProperties, CurrentHttpMessagePropertiesScope.Value);
			return CurrentHttpMessagePropertiesScope.Value;
		}

		private ReadOnlyMemory<HttpPipelinePolicy> CreateRequestPipeline(HttpPipelinePolicy[] policies, List<(HttpPipelinePosition Position, HttpPipelinePolicy Policy)> customPolicies)
		{
			if (!_internallyConstructed)
			{
				throw new InvalidOperationException("Cannot send messages with per-request policies if the pipeline wasn't constructed with HttpPipelineBuilder.");
			}
			ReadOnlySpan<HttpPipelinePolicy> span = _pipeline.Span;
			int num = span.Length - 1;
			span.Slice(0, _perCallIndex).CopyTo(policies);
			int perCallIndex = _perCallIndex;
			int num2 = AddCustomPolicies(customPolicies, policies, HttpPipelinePosition.PerCall, perCallIndex);
			perCallIndex += num2;
			num2 = _perRetryIndex - _perCallIndex;
			span.Slice(_perCallIndex, num2).CopyTo(MemoryExtensions.AsSpan(policies, perCallIndex, num2));
			perCallIndex += num2;
			num2 = AddCustomPolicies(customPolicies, policies, HttpPipelinePosition.PerRetry, perCallIndex);
			perCallIndex += num2;
			num2 = num - _perRetryIndex;
			span.Slice(_perRetryIndex, num2).CopyTo(MemoryExtensions.AsSpan(policies, perCallIndex, num2));
			perCallIndex += num2;
			num2 = AddCustomPolicies(customPolicies, policies, HttpPipelinePosition.BeforeTransport, perCallIndex);
			perCallIndex += num2;
			policies[perCallIndex] = span[num];
			return new ReadOnlyMemory<HttpPipelinePolicy>(policies, 0, perCallIndex + 1);
		}

		private static int AddCustomPolicies(List<(HttpPipelinePosition Position, HttpPipelinePolicy Policy)> source, HttpPipelinePolicy[] target, HttpPipelinePosition position, int start)
		{
			int num = 0;
			if (source != null)
			{
				foreach (var item in source)
				{
					if (item.Position == position)
					{
						target[start + num] = item.Policy;
						num++;
					}
				}
			}
			return num;
		}

		private static void AddHttpMessageProperties(HttpMessage message)
		{
			if (CurrentHttpMessagePropertiesScope.Value == null)
			{
				return;
			}
			foreach (KeyValuePair<string, object> property in CurrentHttpMessagePropertiesScope.Value.Properties)
			{
				if (property.Value != null)
				{
					message.SetProperty(property.Key, property.Value);
				}
			}
		}
	}
	public static class HttpPipelineBuilder
	{
		private static int DefaultPolicyCount = 8;

		public static HttpPipeline Build(ClientOptions options, params HttpPipelinePolicy[] perRetryPolicies)
		{
			return Build(options, Array.Empty<HttpPipelinePolicy>(), perRetryPolicies, ResponseClassifier.Shared);
		}

		public static HttpPipeline Build(ClientOptions options, HttpPipelinePolicy[] perCallPolicies, HttpPipelinePolicy[] perRetryPolicies, ResponseClassifier? responseClassifier)
		{
			HttpPipelineOptions obj = new HttpPipelineOptions(options)
			{
				ResponseClassifier = responseClassifier
			};
			((List<HttpPipelinePolicy>)obj.PerCallPolicies).AddRange(perCallPolicies);
			((List<HttpPipelinePolicy>)obj.PerRetryPolicies).AddRange(perRetryPolicies);
			(ResponseClassifier, HttpPipelineTransport, int, int, HttpPipelinePolicy[], bool) tuple = BuildInternal(obj, null);
			return new HttpPipeline(tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item1);
		}

		public static DisposableHttpPipeline Build(ClientOptions options, HttpPipelinePolicy[] perCallPolicies, HttpPipelinePolicy[] perRetryPolicies, HttpPipelineTransportOptions transportOptions, ResponseClassifier? responseClassifier)
		{
			Argument.AssertNotNull(transportOptions, "transportOptions");
			HttpPipelineOptions obj = new HttpPipelineOptions(options)
			{
				ResponseClassifier = responseClassifier
			};
			((List<HttpPipelinePolicy>)obj.PerCallPolicies).AddRange(perCallPolicies);
			((List<HttpPipelinePolicy>)obj.PerRetryPolicies).AddRange(perRetryPolicies);
			(ResponseClassifier, HttpPipelineTransport, int, int, HttpPipelinePolicy[], bool) tuple = BuildInternal(obj, transportOptions);
			return new DisposableHttpPipeline(tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item1, tuple.Item6);
		}

		public static HttpPipeline Build(HttpPipelineOptions options)
		{
			(ResponseClassifier, HttpPipelineTransport, int, int, HttpPipelinePolicy[], bool) tuple = BuildInternal(options, null);
			return new HttpPipeline(tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item1);
		}

		public static DisposableHttpPipeline Build(HttpPipelineOptions options, HttpPipelineTransportOptions transportOptions)
		{
			Argument.AssertNotNull(transportOptions, "transportOptions");
			(ResponseClassifier, HttpPipelineTransport, int, int, HttpPipelinePolicy[], bool) tuple = BuildInternal(options, transportOptions);
			return new DisposableHttpPipeline(tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item1, tuple.Item6);
		}

		internal static (ResponseClassifier Classifier, HttpPipelineTransport Transport, int PerCallIndex, int PerRetryIndex, HttpPipelinePolicy[] Policies, bool IsTransportOwned) BuildInternal(HttpPipelineOptions buildOptions, HttpPipelineTransportOptions? defaultTransportOptions)
		{
			Argument.AssertNotNull(buildOptions.PerCallPolicies, "PerCallPolicies");
			Argument.AssertNotNull(buildOptions.PerRetryPolicies, "PerRetryPolicies");
			List<HttpPipelinePolicy> policies = new List<HttpPipelinePolicy>(DefaultPolicyCount + (buildOptions.ClientOptions.Policies?.Count ?? 0) + buildOptions.PerCallPolicies.Count + buildOptions.PerRetryPolicies.Count);
			DiagnosticsOptions diagnostics = buildOptions.ClientOptions.Diagnostics;
			HttpMessageSanitizer httpMessageSanitizer = new HttpMessageSanitizer(diagnostics.LoggedQueryParameters.ToArray(), diagnostics.LoggedHeaderNames.ToArray());
			bool isDistributedTracingEnabled = buildOptions.ClientOptions.Diagnostics.IsDistributedTracingEnabled;
			policies.Add(ReadClientRequestIdPolicy.Shared);
			AddNonNullPolicies(buildOptions.PerCallPolicies.ToArray());
			AddUserPolicies(HttpPipelinePosition.PerCall);
			int count = policies.Count;
			policies.Add(ClientRequestIdPolicy.Shared);
			if (diagnostics.IsTelemetryEnabled)
			{
				policies.Add(CreateTelemetryPolicy(buildOptions.ClientOptions));
			}
			RetryOptions retry = buildOptions.ClientOptions.Retry;
			policies.Add(buildOptions.ClientOptions.RetryPolicy ?? new RetryPolicy(retry.MaxRetries, (retry.Mode == RetryMode.Exponential) ? DelayStrategy.CreateExponentialDelayStrategy(retry.Delay, retry.MaxDelay) : DelayStrategy.CreateFixedDelayStrategy(retry.Delay)));
			RedirectPolicy redirectPolicy = (((!(defaultTransportOptions?.IsClientRedirectEnabled)) ?? true) ? RedirectPolicy.Shared : new RedirectPolicy(allowAutoRedirect: true));
			RedirectPolicy item = redirectPolicy;
			policies.Add(item);
			AddNonNullPolicies(buildOptions.PerRetryPolicies.ToArray());
			AddUserPolicies(HttpPipelinePosition.PerRetry);
			int count2 = policies.Count;
			if (diagnostics.IsLoggingEnabled)
			{
				string name = buildOptions.ClientOptions.GetType().Assembly.GetName().Name;
				policies.Add(new LoggingPolicy(diagnostics.IsLoggingContentEnabled, diagnostics.LoggedContentSizeLimit, httpMessageSanitizer, name));
			}
			policies.Add(new ResponseBodyPolicy(buildOptions.ClientOptions.Retry.NetworkTimeout));
			policies.Add(new RequestActivityPolicy(isDistributedTracingEnabled, ClientDiagnostics.GetResourceProviderNamespace(buildOptions.ClientOptions.GetType().Assembly), httpMessageSanitizer));
			AddUserPolicies(HttpPipelinePosition.BeforeTransport);
			HttpPipelineTransport httpPipelineTransport = buildOptions.ClientOptions.Transport;
			bool item2 = false;
			if (defaultTransportOptions != null)
			{
				if (buildOptions.ClientOptions.IsCustomTransportSet)
				{
					AzureCoreEventSource.Singleton.PipelineTransportOptionsNotApplied(buildOptions.ClientOptions.GetType());
				}
				else
				{
					httpPipelineTransport = HttpPipelineTransport.Create(defaultTransportOptions);
					item2 = true;
				}
			}
			policies.Add(new HttpPipelineTransportPolicy(httpPipelineTransport, httpMessageSanitizer, buildOptions.RequestFailedDetailsParser));
			HttpPipelineOptions httpPipelineOptions = buildOptions;
			if (httpPipelineOptions.ResponseClassifier == null)
			{
				ResponseClassifier responseClassifier = (httpPipelineOptions.ResponseClassifier = ResponseClassifier.Shared);
			}
			return (Classifier: buildOptions.ResponseClassifier, Transport: httpPipelineTransport, PerCallIndex: count, PerRetryIndex: count2, Policies: policies.ToArray(), IsTransportOwned: item2);
			void AddNonNullPolicies(HttpPipelinePolicy[] policiesToAdd)
			{
				foreach (HttpPipelinePolicy httpPipelinePolicy in policiesToAdd)
				{
					if (httpPipelinePolicy != null)
					{
						policies.Add(httpPipelinePolicy);
					}
				}
			}
			void AddUserPolicies(HttpPipelinePosition position)
			{
				if (buildOptions.ClientOptions.Policies != null)
				{
					foreach (var policy in buildOptions.ClientOptions.Policies)
					{
						if (policy.Position == position && policy.Policy != null)
						{
							policies.Add(policy.Policy);
						}
					}
				}
			}
		}

		internal static TelemetryPolicy CreateTelemetryPolicy(ClientOptions options)
		{
			return new TelemetryPolicy(new TelemetryDetails(options.GetType().Assembly, options.Diagnostics.ApplicationId));
		}
	}
	public class HttpPipelineOptions
	{
		public ClientOptions ClientOptions { get; }

		public IList<HttpPipelinePolicy> PerCallPolicies { get; }

		public IList<HttpPipelinePolicy> PerRetryPolicies { get; }

		public ResponseClassifier? ResponseClassifier { get; set; }

		public RequestFailedDetailsParser RequestFailedDetailsParser { get; set; }

		public HttpPipelineOptions(ClientOptions options)
		{
			Argument.AssertNotNull(options, "options");
			ClientOptions = options;
			PerCallPolicies = new List<HttpPipelinePolicy>();
			PerRetryPolicies = new List<HttpPipelinePolicy>();
			RequestFailedDetailsParser = new DefaultRequestFailedDetailsParser();
		}
	}
	public abstract class HttpPipelinePolicy
	{
		public abstract ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline);

		public abstract void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline);

		protected static ValueTask ProcessNextAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			return pipeline.Span[0].ProcessAsync(message, pipeline.Slice(1));
		}

		protected static void ProcessNext(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			pipeline.Span[0].Process(message, pipeline.Slice(1));
		}
	}
	[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
	public abstract class HttpPipelineSynchronousPolicy : HttpPipelinePolicy
	{
		private static Type[] _onReceivedResponseParameters = new Type[1] { typeof(HttpMessage) };

		private readonly bool _hasOnReceivedResponse = true;

		protected HttpPipelineSynchronousPolicy()
		{
			MethodInfo method = GetType().GetMethod("OnReceivedResponse", BindingFlags.Instance | BindingFlags.Public, null, _onReceivedResponseParameters, null);
			if (method != null)
			{
				_hasOnReceivedResponse = method.GetBaseDefinition().DeclaringType != method.DeclaringType;
			}
		}

		public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			OnSendingRequest(message);
			HttpPipelinePolicy.ProcessNext(message, pipeline);
			OnReceivedResponse(message);
		}

		public override ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			if (!_hasOnReceivedResponse)
			{
				OnSendingRequest(message);
				return HttpPipelinePolicy.ProcessNextAsync(message, pipeline);
			}
			return InnerProcessAsync(message, pipeline);
		}

		private async ValueTask InnerProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			OnSendingRequest(message);
			await HttpPipelinePolicy.ProcessNextAsync(message, pipeline).ConfigureAwait(continueOnCapturedContext: false);
			OnReceivedResponse(message);
		}

		public virtual void OnSendingRequest(HttpMessage message)
		{
		}

		public virtual void OnReceivedResponse(HttpMessage message)
		{
		}
	}
	public abstract class HttpPipelineTransport
	{
		public abstract void Process(HttpMessage message);

		public abstract ValueTask ProcessAsync(HttpMessage message);

		public abstract Request CreateRequest();

		internal static HttpPipelineTransport Create(HttpPipelineTransportOptions? options = null)
		{
			if (options == null)
			{
				return HttpClientTransport.Shared;
			}
			return new HttpClientTransport(options);
		}
	}
	public class HttpPipelineTransportOptions
	{
		public Func<ServerCertificateCustomValidationArgs, bool>? ServerCertificateCustomValidationCallback { get; set; }

		public IList<X509Certificate2> ClientCertificates { get; }

		public bool IsClientRedirectEnabled { get; set; }

		public HttpPipelineTransportOptions()
		{
			ClientCertificates = new List<X509Certificate2>();
		}
	}
	internal class ClientRequestIdPolicy : HttpPipelineSynchronousPolicy
	{
		internal const string ClientRequestIdHeader = "x-ms-client-request-id";

		internal const string EchoClientRequestId = "x-ms-return-client-request-id";

		public static ClientRequestIdPolicy Shared { get; } = new ClientRequestIdPolicy();

		protected ClientRequestIdPolicy()
		{
		}

		public override void OnSendingRequest(HttpMessage message)
		{
			message.Request.Headers.SetValue("x-ms-client-request-id", message.Request.ClientRequestId);
			message.Request.Headers.SetValue("x-ms-return-client-request-id", "true");
		}
	}
	internal class DefaultRequestFailedDetailsParser : RequestFailedDetailsParser
	{
		public override bool TryParse(Response response, out ResponseError? error, out IDictionary<string, string>? data)
		{
			return TryParseDetails(response, out error, out data);
		}

		public static bool TryParseDetails(Response response, out ResponseError? error, out IDictionary<string, string>? data)
		{
			error = null;
			data = null;
			try
			{
				string text = response.Content.ToString();
				if (text == null || !text.StartsWith("{", StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				error = JsonSerializer.Deserialize<RequestFailedException.ErrorResponse>(text)?.Error;
				if (error == null)
				{
					error = JsonSerializer.Deserialize<ResponseError>(text);
				}
			}
			catch (Exception)
			{
			}
			return error != null;
		}
	}
	internal sealed class HttpEnvironmentProxy : IWebProxy
	{
		private const string EnvAllProxyUC = "ALL_PROXY";

		private const string EnvAllProxyLC = "all_proxy";

		private const string EnvHttpProxyLC = "http_proxy";

		private const string EnvHttpProxyUC = "HTTP_PROXY";

		private const string EnvHttpsProxyLC = "https_proxy";

		private const string EnvHttpsProxyUC = "HTTPS_PROXY";

		private const string EnvNoProxyLC = "no_proxy";

		private const string EnvNoProxyUC = "NO_PROXY";

		private const string EnvCGI = "GATEWAY_INTERFACE";

		private readonly Uri _httpProxyUri;

		private readonly Uri _httpsProxyUri;

		private readonly string[] _bypass;

		private readonly ICredentials _credentials;

		public ICredentials Credentials
		{
			get
			{
				return _credentials;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public static bool TryCreate(out IWebProxy proxy)
		{
			Uri uri = GetUriFromString(Environment.GetEnvironmentVariable("http_proxy"));
			if (uri == null && Environment.GetEnvironmentVariable("GATEWAY_INTERFACE") == null)
			{
				uri = GetUriFromString(Environment.GetEnvironmentVariable("HTTP_PROXY"));
			}
			Uri uri2 = GetUriFromString(Environment.GetEnvironmentVariable("https_proxy")) ?? GetUriFromString(Environment.GetEnvironmentVariable("HTTPS_PROXY"));
			if (uri == null || uri2 == null)
			{
				Uri uri3 = GetUriFromString(Environment.GetEnvironmentVariable("all_proxy")) ?? GetUriFromString(Environment.GetEnvironmentVariable("ALL_PROXY"));
				if (uri == null)
				{
					uri = uri3;
				}
				if (uri2 == null)
				{
					uri2 = uri3;
				}
			}
			if (uri == null && uri2 == null)
			{
				proxy = null;
				return false;
			}
			string bypassList = Environment.GetEnvironmentVariable("no_proxy") ?? Environment.GetEnvironmentVariable("NO_PROXY");
			proxy = new HttpEnvironmentProxy(uri, uri2, bypassList);
			return true;
		}

		private HttpEnvironmentProxy(Uri httpProxy, Uri httpsProxy, string bypassList)
		{
			_httpProxyUri = httpProxy;
			_httpsProxyUri = httpsProxy;
			_credentials = HttpEnvironmentProxyCredentials.TryCreate(httpProxy, httpsProxy);
			if (string.IsNullOrWhiteSpace(bypassList))
			{
				return;
			}
			string[] array = bypassList.Split(new char[1] { ',' });
			List<string> list = new List<string>(array.Length);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i].Trim();
				if (text.Length > 0)
				{
					list.Add(text);
				}
			}
			if (list.Count > 0)
			{
				_bypass = list.ToArray();
			}
		}

		private static Uri GetUriFromString(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			if (value.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
			{
				value = value.Substring(7);
			}
			string text = null;
			string text2 = null;
			ushort result = 80;
			int num = value.LastIndexOf('@');
			if (num != -1)
			{
				string text3 = value.Substring(0, num);
				try
				{
					text3 = Uri.UnescapeDataString(text3);
				}
				catch
				{
				}
				value = value.Substring(num + 1);
				num = text3.IndexOfOrdinal(':');
				if (num == -1)
				{
					text = text3;
				}
				else
				{
					text = text3.Substring(0, num);
					text2 = text3.Substring(num + 1);
				}
			}
			int num2 = value.IndexOfOrdinal(']');
			num = value.LastIndexOf(':');
			string host;
			if (num == -1 || (num2 != -1 && num < num2))
			{
				host = value;
			}
			else
			{
				host = value.Substring(0, num);
				int i;
				for (i = num + 1; i < value.Length && char.IsDigit(value[i]); i++)
				{
				}
				if (!ushort.TryParse(value.Substring(num + 1, i - num - 1), out result))
				{
					return null;
				}
			}
			try
			{
				UriBuilder uriBuilder = new UriBuilder("http", host, result);
				if (text != null)
				{
					uriBuilder.UserName = Uri.EscapeDataString(text);
				}
				if (text2 != null)
				{
					uriBuilder.Password = Uri.EscapeDataString(text2);
				}
				return uriBuilder.Uri;
			}
			catch
			{
			}
			return null;
		}

		private bool IsMatchInBypassList(Uri input)
		{
			if (_bypass != null)
			{
				string[] bypass = _bypass;
				foreach (string text in bypass)
				{
					if (text[0] == '.')
					{
						if (text.Length - 1 == input.Host.Length && string.Compare(text, 1, input.Host, 0, input.Host.Length, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return true;
						}
						if (input.Host.EndsWith(text, StringComparison.OrdinalIgnoreCase))
						{
							return true;
						}
					}
					else if (string.Equals(text, input.Host, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		public Uri GetProxy(Uri uri)
		{
			if (!(uri.Scheme == Uri.UriSchemeHttp))
			{
				return _httpsProxyUri;
			}
			return _httpProxyUri;
		}

		public bool IsBypassed(Uri uri)
		{
			if (!(GetProxy(uri) == null))
			{
				return IsMatchInBypassList(uri);
			}
			return true;
		}
	}
	internal sealed class HttpEnvironmentProxyCredentials : ICredentials
	{
		private readonly NetworkCredential _httpCred;

		private readonly NetworkCredential _httpsCred;

		private readonly Uri _httpProxy;

		private readonly Uri _httpsProxy;

		public HttpEnvironmentProxyCredentials(Uri httpProxy, NetworkCredential httpCred, Uri httpsProxy, NetworkCredential httpsCred)
		{
			_httpCred = httpCred;
			_httpsCred = httpsCred;
			_httpProxy = httpProxy;
			_httpsProxy = httpsProxy;
		}

		public NetworkCredential GetCredential(Uri uri, string authType)
		{
			if (uri == null)
			{
				return null;
			}
			if (!uri.Equals(_httpProxy))
			{
				if (!uri.Equals(_httpsProxy))
				{
					return null;
				}
				return _httpsCred;
			}
			return _httpCred;
		}

		public static HttpEnvironmentProxyCredentials TryCreate(Uri httpProxy, Uri httpsProxy)
		{
			NetworkCredential networkCredential = null;
			NetworkCredential networkCredential2 = null;
			if (httpProxy != null)
			{
				networkCredential = GetCredentialsFromString(httpProxy.UserInfo);
			}
			if (httpsProxy != null)
			{
				networkCredential2 = GetCredentialsFromString(httpsProxy.UserInfo);
			}
			if (networkCredential == null && networkCredential2 == null)
			{
				return null;
			}
			return new HttpEnvironmentProxyCredentials(httpProxy, networkCredential, httpsProxy, networkCredential2);
		}

		private static NetworkCredential GetCredentialsFromString(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return null;
			}
			value = Uri.UnescapeDataString(value);
			string password = "";
			string domain = null;
			int num = value.IndexOfOrdinal(':');
			if (num != -1)
			{
				password = value.Substring(num + 1);
				value = value.Substring(0, num);
			}
			num = value.IndexOfOrdinal('\\');
			if (num != -1)
			{
				domain = value.Substring(0, num);
				value = value.Substring(num + 1);
			}
			return new NetworkCredential(value, password, domain);
		}
	}
	internal class HttpPipelineTransportPolicy : HttpPipelinePolicy
	{
		private readonly HttpPipelineTransport _transport;

		private readonly HttpMessageSanitizer _sanitizer;

		private readonly RequestFailedDetailsParser? _errorParser;

		public HttpPipelineTransportPolicy(HttpPipelineTransport transport, HttpMessageSanitizer sanitizer, RequestFailedDetailsParser? failureContentExtractor = null)
		{
			_transport = transport;
			_sanitizer = sanitizer;
			_errorParser = failureContentExtractor;
		}

		public override async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			await _transport.ProcessAsync(message).ConfigureAwait(continueOnCapturedContext: false);
			message.Response.RequestFailedDetailsParser = _errorParser;
			message.Response.Sanitizer = _sanitizer;
			message.Response.IsError = message.ResponseClassifier.IsErrorResponse(message);
		}

		public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			_transport.Process(message);
			message.Response.RequestFailedDetailsParser = _errorParser;
			message.Response.Sanitizer = _sanitizer;
			message.Response.IsError = message.ResponseClassifier.IsErrorResponse(message);
		}
	}
	internal class LoggingPolicy : HttpPipelinePolicy
	{
		private class LoggingStream : ReadOnlyStream
		{
			private readonly string _requestId;

			private readonly ContentEventSourceWrapper _eventSourceWrapper;

			private int _maxLoggedBytes;

			private readonly Stream _originalStream;

			private readonly bool _error;

			private readonly Encoding? _textEncoding;

			private int _blockNumber;

			public override bool CanRead => _originalStream.CanRead;

			public override bool CanSeek => _originalStream.CanSeek;

			public override long Length => _originalStream.Length;

			public override long Position
			{
				get
				{
					return _originalStream.Position;
				}
				set
				{
					_originalStream.Position = value;
				}
			}

			public LoggingStream(string requestId, ContentEventSourceWrapper eventSourceWrapper, int maxLoggedBytes, Stream originalStream, bool error, Encoding? textEncoding)
			{
				_requestId = requestId;
				_eventSourceWrapper = eventSourceWrapper;
				_maxLoggedBytes = maxLoggedBytes;
				_originalStream = originalStream;
				_error = error;
				_textEncoding = textEncoding;
			}

			public override long Seek(long offset, SeekOrigin origin)
			{
				return _originalStream.Seek(offset, origin);
			}

			public override int Read(byte[] buffer, int offset, int count)
			{
				int count2;
				int result = (count2 = _originalStream.Read(buffer, offset, count));
				DecrementLength(ref count2);
				LogBuffer(buffer, offset, count2);
				return result;
			}

			private void LogBuffer(byte[] buffer, int offset, int count)
			{
				if (count != 0)
				{
					_eventSourceWrapper.Log(_requestId, _error, buffer, offset, count, _textEncoding, _blockNumber);
					_blockNumber++;
				}
			}

			public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				int count2;
				int result = (count2 = await _originalStream.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
				DecrementLength(ref count2);
				LogBuffer(buffer, offset, count2);
				return result;
			}

			public override void Close()
			{
				_originalStream.Close();
			}

			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				_originalStream.Dispose();
			}

			private void DecrementLength(ref int count)
			{
				int num = Math.Min(count, _maxLoggedBytes);
				count = num;
				_maxLoggedBytes -= count;
			}
		}

		private readonly struct ContentEventSourceWrapper
		{
			private enum EventType
			{
				Request,
				Response,
				ErrorResponse
			}

			private class MaxLengthStream : MemoryStream
			{
				private int _bytesLeft;

				public MaxLengthStream(int maxLength)
				{
					_bytesLeft = maxLength;
				}

				public override void Write(byte[] buffer, int offset, int count)
				{
					DecrementLength(ref count);
					if (count > 0)
					{
						base.Write(buffer, offset, count);
					}
				}

				public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
				{
					if (count <= 0)
					{
						return Task.CompletedTask;
					}
					return base.WriteAsync(buffer, offset, count, cancellationToken);
				}

				private void DecrementLength(ref int count)
				{
					int num = Math.Min(count, _bytesLeft);
					count = num;
					_bytesLeft -= count;
				}
			}

			private const int CopyBufferSize = 8192;

			private readonly AzureCoreEventSource? _eventSource;

			private readonly int _maxLength;

			private readonly CancellationToken _cancellationToken;

			public ContentEventSourceWrapper(AzureCoreEventSource eventSource, bool logContent, int maxLength, CancellationToken cancellationToken)
			{
				_eventSource = (logContent ? eventSource : null);
				_maxLength = maxLength;
				_cancellationToken = cancellationToken;
			}

			public async ValueTask LogAsync(string requestId, bool isError, Stream? stream, Encoding? textEncoding, bool async)
			{
				EventType eventType = ResponseOrError(isError);
				if (stream != null && IsEnabled(eventType))
				{
					Log(requestId, eventType, await FormatAsync(stream, async).ConfigureAwait(continueOnCapturedContext: false).EnsureCompleted(async), textEncoding);
				}
			}

			public async ValueTask LogAsync(string requestId, RequestContent? content, Encoding? textEncoding, bool async)
			{
				EventType eventType = EventType.Request;
				if (content != null && IsEnabled(eventType))
				{
					Log(requestId, eventType, await FormatAsync(content, async).ConfigureAwait(continueOnCapturedContext: false).EnsureCompleted(async), textEncoding);
				}
			}

			public void Log(string requestId, bool isError, byte[] buffer, int offset, int length, Encoding? textEncoding, int? block = null)
			{
				EventType eventType = ResponseOrError(isError);
				if (buffer != null && IsEnabled(eventType))
				{
					int num = Math.Min(length, _maxLength);
					byte[] array;
					if (length == num && offset == 0)
					{
						array = buffer;
					}
					else
					{
						array = new byte[num];
						Array.Copy(buffer, offset, array, 0, num);
					}
					Log(requestId, eventType, array, textEncoding, block);
				}
			}

			public bool IsEnabled(bool isError)
			{
				return IsEnabled(ResponseOrError(isError));
			}

			private bool IsEnabled(EventType errorResponse)
			{
				if (_eventSource != null)
				{
					if (!_eventSource.IsEnabled(EventLevel.Informational, EventKeywords.All))
					{
						if (errorResponse == EventType.ErrorResponse)
						{
							return _eventSource.IsEnabled(EventLevel.Warning, EventKeywords.All);
						}
						return false;
					}
					return true;
				}
				return false;
			}

			private async ValueTask<byte[]> FormatAsync(RequestContent requestContent, bool async)
			{
				using MaxLengthStream memoryStream = new MaxLengthStream(_maxLength);
				if (async)
				{
					await requestContent.WriteToAsync(memoryStream, _cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
				else
				{
					requestContent.WriteTo(memoryStream, _cancellationToken);
				}
				return memoryStream.ToArray();
			}

			private async ValueTask<byte[]> FormatAsync(Stream content, bool async)
			{
				using MaxLengthStream memoryStream = new MaxLengthStream(_maxLength);
				if (async)
				{
					await content.CopyToAsync(memoryStream, 8192, _cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
				else
				{
					content.CopyTo(memoryStream);
				}
				content.Seek(0L, SeekOrigin.Begin);
				return memoryStream.ToArray();
			}

			private void Log(string requestId, EventType eventType, byte[] bytes, Encoding? textEncoding, int? block = null)
			{
				AzureCoreEventSource eventSource = _eventSource;
				switch (eventType)
				{
				case EventType.Request:
					eventSource.RequestContent(requestId, bytes, textEncoding);
					break;
				case EventType.Response:
					if (block.HasValue)
					{
						eventSource.ResponseContentBlock(requestId, block.Value, bytes, textEncoding);
					}
					else
					{
						eventSource.ResponseContent(requestId, bytes, textEncoding);
					}
					break;
				case EventType.ErrorResponse:
					if (block.HasValue)
					{
						eventSource.ErrorResponseContentBlock(requestId, block.Value, bytes, textEncoding);
					}
					else
					{
						eventSource.ErrorResponseContent(requestId, bytes, textEncoding);
					}
					break;
				}
			}

			private static EventType ResponseOrError(bool isError)
			{
				if (!isError)
				{
					return EventType.Response;
				}
				return EventType.ErrorResponse;
			}
		}

		private const double RequestTooLongTime = 3.0;

		private static readonly AzureCoreEventSource s_eventSource = AzureCoreEventSource.Singleton;

		private readonly bool _logContent;

		private readonly int _maxLength;

		private readonly HttpMessageSanitizer _sanitizer;

		private readonly string? _assemblyName;

		public LoggingPolicy(bool logContent, int maxLength, HttpMessageSanitizer sanitizer, string? assemblyName)
		{
			_sanitizer = sanitizer;
			_logContent = logContent;
			_maxLength = maxLength;
			_assemblyName = assemblyName;
		}

		public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			if (!s_eventSource.IsEnabled())
			{
				HttpPipelinePolicy.ProcessNext(message, pipeline);
			}
			else
			{
				ProcessAsync(message, pipeline, async: false).EnsureCompleted();
			}
		}

		public override ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			if (!s_eventSource.IsEnabled())
			{
				return HttpPipelinePolicy.ProcessNextAsync(message, pipeline);
			}
			return ProcessAsync(message, pipeline, async: true);
		}

		private async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline, bool async)
		{
			Request request = message.Request;
			s_eventSource.Request(request, _assemblyName, _sanitizer);
			Encoding encoding = null;
			if (request.TryGetHeader(HttpHeader.Names.ContentType, out string value))
			{
				ContentTypeUtilities.TryGetTextEncoding(value, out encoding);
			}
			ContentEventSourceWrapper logWrapper = new ContentEventSourceWrapper(s_eventSource, _logContent, _maxLength, message.CancellationToken);
			await logWrapper.LogAsync(request.ClientRequestId, request.Content, encoding, async).ConfigureAwait(continueOnCapturedContext: false).EnsureCompleted(async);
			long before = Stopwatch.GetTimestamp();
			try
			{
				if (async)
				{
					await HttpPipelinePolicy.ProcessNextAsync(message, pipeline).ConfigureAwait(continueOnCapturedContext: false);
				}
				else
				{
					HttpPipelinePolicy.ProcessNext(message, pipeline);
				}
			}
			catch (Exception ex)
			{
				s_eventSource.ExceptionResponse(request.ClientRequestId, ex.ToString());
				throw;
			}
			long timestamp = Stopwatch.GetTimestamp();
			Response response = message.Response;
			bool isError = response.IsError;
			ContentTypeUtilities.TryGetTextEncoding(response.Headers.ContentType, out var encoding2);
			int num;
			if (response.ContentStream != null)
			{
				Stream? contentStream = response.ContentStream;
				if (contentStream != null && !contentStream.CanSeek)
				{
					num = (logWrapper.IsEnabled(isError) ? 1 : 0);
					goto IL_026a;
				}
			}
			num = 0;
			goto IL_026a;
			IL_026a:
			double elapsed = (double)(timestamp - before) / (double)Stopwatch.Frequency;
			if (isError)
			{
				s_eventSource.ErrorResponse(response, _sanitizer, elapsed);
			}
			else
			{
				s_eventSource.Response(response, _sanitizer, elapsed);
			}
			if (num == 0)
			{
				await logWrapper.LogAsync(response.ClientRequestId, isError, response.ContentStream, encoding2, async).ConfigureAwait(continueOnCapturedContext: false).EnsureCompleted(async);
			}
			else
			{
				response.ContentStream = new LoggingStream(response.ClientRequestId, logWrapper, _maxLength, response.ContentStream, isError, encoding2);
			}
			if (elapsed > 3.0)
			{
				s_eventSource.ResponseDelay(response.ClientRequestId, elapsed);
			}
		}
	}
	internal class ReadClientRequestIdPolicy : HttpPipelineSynchronousPolicy
	{
		public const string MessagePropertyKey = "x-ms-client-request-id";

		public static ReadClientRequestIdPolicy Shared { get; } = new ReadClientRequestIdPolicy();

		protected ReadClientRequestIdPolicy()
		{
		}

		public override void OnSendingRequest(HttpMessage message)
		{
			object value2;
			if (message.Request.Headers.TryGetValue("x-ms-client-request-id", out string value))
			{
				message.Request.ClientRequestId = value;
			}
			else if (message.TryGetProperty("x-ms-client-request-id", out value2))
			{
				if (!(value2 is string clientRequestId))
				{
					throw new ArgumentException(string.Format("{0} http message property must be a string but was {1}", "x-ms-client-request-id", value2?.GetType()));
				}
				message.Request.ClientRequestId = clientRequestId;
			}
		}
	}
	internal abstract class ReadOnlyStream : Stream
	{
		public override bool CanWrite => false;

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Flush()
		{
		}
	}
	internal class ReadTimeoutStream : ReadOnlyStream
	{
		private readonly Stream _stream;

		private TimeSpan _readTimeout;

		private CancellationTokenSource _cancellationTokenSource;

		public override bool CanRead => _stream.CanRead;

		public override bool CanSeek => _stream.CanSeek;

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

		public override int ReadTimeout
		{
			get
			{
				return (int)_readTimeout.TotalMilliseconds;
			}
			set
			{
				_readTimeout = TimeSpan.FromMilliseconds(value);
				UpdateReadTimeout();
			}
		}

		public ReadTimeoutStream(Stream stream, TimeSpan readTimeout)
		{
			_stream = stream;
			_readTimeout = readTimeout;
			UpdateReadTimeout();
			InitializeTokenSource();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			bool dispose;
			CancellationTokenSource cancellationTokenSource = StartTimeout(default(CancellationToken), out dispose);
			try
			{
				return _stream.Read(buffer, offset, count);
			}
			catch (IOException inner)
			{
				ResponseBodyPolicy.ThrowIfCancellationRequestedOrTimeout(default(CancellationToken), cancellationTokenSource.Token, inner, _readTimeout);
				throw;
			}
			catch (ObjectDisposedException inner2)
			{
				ResponseBodyPolicy.ThrowIfCancellationRequestedOrTimeout(default(CancellationToken), cancellationTokenSource.Token, inner2, _readTimeout);
				throw;
			}
			catch (OperationCanceledException inner3)
			{
				ResponseBodyPolicy.ThrowIfCancellationRequestedOrTimeout(default(CancellationToken), cancellationTokenSource.Token, inner3, _readTimeout);
				throw;
			}
			finally
			{
				StopTimeout(cancellationTokenSource, dispose);
			}
		}

		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			bool dispose;
			CancellationTokenSource source = StartTimeout(cancellationToken, out dispose);
			try
			{
				return await _stream.ReadAsync(buffer, offset, count, source.Token).ConfigureAwait(continueOnCapturedContext: false);
			}
			catch (IOException inner)
			{
				ResponseBodyPolicy.ThrowIfCancellationRequestedOrTimeout(cancellationToken, source.Token, inner, _readTimeout);
				throw;
			}
			catch (ObjectDisposedException inner2)
			{
				ResponseBodyPolicy.ThrowIfCancellationRequestedOrTimeout(cancellationToken, source.Token, inner2, _readTimeout);
				throw;
			}
			catch (OperationCanceledException inner3)
			{
				ResponseBodyPolicy.ThrowIfCancellationRequestedOrTimeout(cancellationToken, source.Token, inner3, _readTimeout);
				throw;
			}
			finally
			{
				StopTimeout(source, dispose);
			}
		}

		private CancellationTokenSource StartTimeout(CancellationToken additionalToken, out bool dispose)
		{
			if (_cancellationTokenSource.IsCancellationRequested)
			{
				InitializeTokenSource();
			}
			CancellationTokenSource result;
			if (additionalToken.CanBeCanceled)
			{
				result = CancellationTokenSource.CreateLinkedTokenSource(additionalToken, _cancellationTokenSource.Token);
				dispose = true;
			}
			else
			{
				result = _cancellationTokenSource;
				dispose = false;
			}
			_cancellationTokenSource.CancelAfter(_readTimeout);
			return result;
		}

		private void InitializeTokenSource()
		{
			_cancellationTokenSource = new CancellationTokenSource();
			_cancellationTokenSource.Token.Register(delegate(object state)
			{
				((ReadTimeoutStream)state).DisposeStream();
			}, this);
		}

		private void DisposeStream()
		{
			_stream.Dispose();
		}

		private void StopTimeout(CancellationTokenSource source, bool dispose)
		{
			_cancellationTokenSource.CancelAfter(Timeout.InfiniteTimeSpan);
			if (dispose)
			{
				source.Dispose();
			}
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return _stream.Seek(offset, origin);
		}

		private void UpdateReadTimeout()
		{
			try
			{
				if (_stream.CanTimeout)
				{
					_stream.ReadTimeout = (int)_readTimeout.TotalMilliseconds;
				}
			}
			catch
			{
			}
		}

		public override void Close()
		{
			_stream.Close();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			_stream.Dispose();
			_cancellationTokenSource.Dispose();
		}
	}
	internal class RequestActivityPolicy : HttpPipelinePolicy
	{
		private readonly bool _isDistributedTracingEnabled;

		private readonly string? _resourceProviderNamespace;

		private readonly HttpMessageSanitizer _sanitizer;

		private const string TraceParentHeaderName = "traceparent";

		private const string TraceStateHeaderName = "tracestate";

		private const string RequestIdHeaderName = "Request-Id";

		private static readonly DiagnosticListener s_diagnosticSource = new DiagnosticListener("Azure.Core");

		private static readonly ActivitySource s_activitySource = new ActivitySource("Azure.Core.Http");

		private bool ShouldCreateActivity
		{
			get
			{
				if (_isDistributedTracingEnabled)
				{
					if (!s_diagnosticSource.IsEnabled())
					{
						return IsActivitySourceEnabled;
					}
					return true;
				}
				return false;
			}
		}

		private bool IsActivitySourceEnabled
		{
			get
			{
				if (_isDistributedTracingEnabled)
				{
					return s_activitySource.HasListeners();
				}
				return false;
			}
		}

		public RequestActivityPolicy(bool isDistributedTracingEnabled, string? resourceProviderNamespace, HttpMessageSanitizer httpMessageSanitizer)
		{
			_isDistributedTracingEnabled = isDistributedTracingEnabled;
			_resourceProviderNamespace = resourceProviderNamespace;
			_sanitizer = httpMessageSanitizer;
		}

		public override ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			if (ShouldCreateActivity)
			{
				return ProcessAsync(message, pipeline, async: true);
			}
			return ProcessNextAsync(message, pipeline, async: true);
		}

		public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			if (ShouldCreateActivity)
			{
				ProcessAsync(message, pipeline, async: false).EnsureCompleted();
			}
			else
			{
				ProcessNextAsync(message, pipeline, async: false).EnsureCompleted();
			}
		}

		private async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline, bool async)
		{
			using DiagnosticScope scope = CreateDiagnosticScope(message);
			bool isActivitySourceEnabled = IsActivitySourceEnabled;
			scope.SetDisplayName(message.Request.Method.Method);
			scope.AddAttribute(isActivitySourceEnabled ? "http.request.method" : "http.method", message.Request.Method.Method);
			scope.AddAttribute(isActivitySourceEnabled ? "url.full" : "http.url", message.Request.Uri, (RequestUriBuilder u) => _sanitizer.SanitizeUrl(u.ToString()));
			scope.AddAttribute(isActivitySourceEnabled ? "az.client_request_id" : "requestId", message.Request.ClientRequestId);
			if (message.RetryNumber > 0)
			{
				scope.AddIntegerAttribute("http.request.resend_count", message.RetryNumber);
			}
			if (isActivitySourceEnabled)
			{
				string host = message.Request.Uri.Host;
				if (host != null)
				{
					scope.AddAttribute("server.address", host);
					scope.AddIntegerAttribute("server.port", message.Request.Uri.Port);
				}
			}
			scope.AddAttribute("az.namespace", _resourceProviderNamespace);
			if (!isActivitySourceEnabled && message.Request.Headers.TryGetValue("User-Agent", out string value))
			{
				scope.AddAttribute("http.user_agent", value);
			}
			scope.Start();
			try
			{
				if (async)
				{
					await ProcessNextAsync(message, pipeline, async: true).ConfigureAwait(continueOnCapturedContext: false);
				}
				else
				{
					ProcessNextAsync(message, pipeline, async: false).EnsureCompleted();
				}
			}
			catch (Exception exception)
			{
				scope.Failed(exception);
				throw;
			}
			string text = message.Response.Status.ToString(CultureInfo.InvariantCulture);
			if (isActivitySourceEnabled)
			{
				scope.AddIntegerAttribute("http.response.status_code", message.Response.Status);
			}
			else
			{
				scope.AddAttribute("http.status_code", text);
			}
			string requestId = message.Response.Headers.RequestId;
			if (requestId != null)
			{
				string name = (isActivitySourceEnabled ? "az.service_request_id" : "serviceRequestId");
				scope.AddAttribute(name, requestId);
			}
			if (message.Response.IsError)
			{
				scope.Failed(text);
			}
			if (!isActivitySourceEnabled)
			{
				scope.AddAttribute("otel.status_code", message.Response.IsError ? "ERROR" : "UNSET");
			}
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The values being passed into Write have the commonly used properties being preserved with DynamicallyAccessedMembers.")]
		private DiagnosticScope CreateDiagnosticScope<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(T sourceArgs)
		{
			return new DiagnosticScope("Azure.Core.Http.Request", s_diagnosticSource, sourceArgs, s_activitySource, ActivityKind.Client, suppressNestedClientActivities: false);
		}

		private static ValueTask ProcessNextAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline, bool async)
		{
			Activity current = Activity.Current;
			if (current != null)
			{
				string value = current.Id ?? string.Empty;
				if (current.IdFormat == ActivityIdFormat.W3C)
				{
					if (!message.Request.Headers.Contains("traceparent"))
					{
						message.Request.Headers.Add("traceparent", value);
						string traceStateString = current.TraceStateString;
						if (traceStateString != null)
						{
							message.Request.Headers.Add("tracestate", traceStateString);
						}
					}
				}
				else if (!message.Request.Headers.Contains("Request-Id"))
				{
					message.Request.Headers.Add("Request-Id", value);
				}
			}
			if (async)
			{
				return HttpPipelinePolicy.ProcessNextAsync(message, pipeline);
			}
			HttpPipelinePolicy.ProcessNext(message, pipeline);
			return default(ValueTask);
		}
	}
	internal class ResponseBodyPolicy : HttpPipelinePolicy
	{
		private const int DefaultCopyBufferSize = 81920;

		private readonly TimeSpan _networkTimeout;

		public ResponseBodyPolicy(TimeSpan networkTimeout)
		{
			_networkTimeout = networkTimeout;
		}

		public override ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			return ProcessAsync(message, pipeline, async: true);
		}

		public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			ProcessAsync(message, pipeline, async: false).EnsureCompleted();
		}

		private async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline, bool async)
		{
			CancellationToken oldToken = message.CancellationToken;
			using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(oldToken);
			TimeSpan networkTimeout = _networkTimeout;
			TimeSpan? networkTimeout2 = message.NetworkTimeout;
			if (networkTimeout2.HasValue)
			{
				TimeSpan valueOrDefault = networkTimeout2.GetValueOrDefault();
				networkTimeout = valueOrDefault;
			}
			cts.CancelAfter(networkTimeout);
			try
			{
				message.CancellationToken = cts.Token;
				if (async)
				{
					await HttpPipelinePolicy.ProcessNextAsync(message, pipeline).ConfigureAwait(continueOnCapturedContext: false);
				}
				else
				{
					HttpPipelinePolicy.ProcessNext(message, pipeline);
				}
			}
			catch (OperationCanceledException inner)
			{
				ThrowIfCancellationRequestedOrTimeout(oldToken, cts.Token, inner, networkTimeout);
				throw;
			}
			finally
			{
				message.CancellationToken = oldToken;
				cts.CancelAfter(-1);
			}
			Stream responseContentStream = message.Response.ContentStream;
			if (responseContentStream == null || responseContentStream.CanSeek)
			{
				return;
			}
			if (message.BufferResponse)
			{
				if (networkTimeout != Timeout.InfiniteTimeSpan || oldToken.CanBeCanceled)
				{
					cts.Token.Register(delegate(object state)
					{
						((Stream)state)?.Dispose();
					}, responseContentStream);
				}
				try
				{
					MemoryStream bufferedStream = new MemoryStream();
					if (async)
					{
						await CopyToAsync(responseContentStream, bufferedStream, cts).ConfigureAwait(continueOnCapturedContext: false);
					}
					else
					{
						CopyTo(responseContentStream, bufferedStream, cts);
					}
					responseContentStream.Dispose();
					bufferedStream.Position = 0L;
					message.Response.ContentStream = bufferedStream;
					return;
				}
				catch (Exception ex) when (((ex is ObjectDisposedException || ex is IOException || ex is OperationCanceledException || ex is NotSupportedException) ? 1 : 0) != 0)
				{
					ThrowIfCancellationRequestedOrTimeout(oldToken, cts.Token, ex, networkTimeout);
					throw;
				}
			}
			if (networkTimeout != Timeout.InfiniteTimeSpan)
			{
				message.Response.ContentStream = new ReadTimeoutStream(responseContentStream, networkTimeout);
			}
		}

		private async Task CopyToAsync(Stream source, Stream destination, CancellationTokenSource cancellationTokenSource)
		{
			byte[] buffer = ArrayPool<byte>.Shared.Rent(81920);
			try
			{
				while (true)
				{
					cancellationTokenSource.CancelAfter(_networkTimeout);
					int num = await source.ReadAsync(buffer, 0, buffer.Length, cancellationTokenSource.Token).ConfigureAwait(continueOnCapturedContext: false);
					if (num == 0)
					{
						break;
					}
					await AzureBaseBuffersExtensions.WriteAsync(destination, new ReadOnlyMemory<byte>(buffer, 0, num), cancellationTokenSource.Token).ConfigureAwait(continueOnCapturedContext: false);
				}
			}
			finally
			{
				cancellationTokenSource.CancelAfter(Timeout.InfiniteTimeSpan);
				ArrayPool<byte>.Shared.Return(buffer);
			}
		}

		private void CopyTo(Stream source, Stream destination, CancellationTokenSource cancellationTokenSource)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(81920);
			try
			{
				int count;
				while ((count = source.Read(array, 0, array.Length)) != 0)
				{
					cancellationTokenSource.Token.ThrowIfCancellationRequested();
					cancellationTokenSource.CancelAfter(_networkTimeout);
					destination.Write(array, 0, count);
				}
			}
			finally
			{
				cancellationTokenSource.CancelAfter(Timeout.InfiniteTimeSpan);
				ArrayPool<byte>.Shared.Return(array);
			}
		}

		internal static void ThrowIfCancellationRequestedOrTimeout(CancellationToken originalToken, CancellationToken timeoutToken, Exception? inner, TimeSpan timeout)
		{
			CancellationHelper.ThrowIfCancellationRequested(originalToken);
			if (timeoutToken.IsCancellationRequested)
			{
				throw CancellationHelper.CreateOperationCanceledException(inner, timeoutToken, $"The operation was cancelled because it exceeded the configured timeout of {timeout:g}. " + "Network timeout can be adjusted in ClientOptions.Retry.NetworkTimeout.");
			}
		}
	}
	internal class TelemetryPolicy : HttpPipelineSynchronousPolicy
	{
		private readonly string _defaultHeader;

		public TelemetryPolicy(TelemetryDetails telemetryDetails)
		{
			_defaultHeader = telemetryDetails.ToString();
		}

		public override void OnSendingRequest(HttpMessage message)
		{
			if (message.TryGetProperty(typeof(UserAgentValueKey), out object value))
			{
				message.Request.Headers.Add(HttpHeader.Names.UserAgent, (string)value);
			}
			else
			{
				message.Request.Headers.Add(HttpHeader.Names.UserAgent, _defaultHeader);
			}
		}
	}
	internal class ThreadSafeRandom : Random
	{
		private readonly Random _random = new Random();

		public override int Next()
		{
			lock (_random)
			{
				return _random.Next();
			}
		}

		public override int Next(int minValue, int maxValue)
		{
			lock (_random)
			{
				return _random.Next(minValue, maxValue);
			}
		}

		public override int Next(int maxValue)
		{
			lock (_random)
			{
				return _random.Next(maxValue);
			}
		}

		public override double NextDouble()
		{
			lock (_random)
			{
				return _random.NextDouble();
			}
		}

		public override void NextBytes(byte[] buffer)
		{
			lock (_random)
			{
				_random.NextBytes(buffer);
			}
		}
	}
	internal class UserAgentValueKey
	{
	}
	public sealed class RedirectPolicy : HttpPipelinePolicy
	{
		private class AllowRedirectsValueKey
		{
		}

		private readonly int _maxAutomaticRedirections;

		private readonly bool _allowAutoRedirects;

		internal static RedirectPolicy Shared { get; } = new RedirectPolicy(allowAutoRedirect: false);

		internal RedirectPolicy(bool allowAutoRedirect)
		{
			_allowAutoRedirects = allowAutoRedirect;
			_maxAutomaticRedirections = 50;
		}

		public static void SetAllowAutoRedirect(HttpMessage message, bool allowAutoRedirect)
		{
			message.SetProperty(typeof(AllowRedirectsValueKey), allowAutoRedirect);
		}

		internal async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline, bool async)
		{
			if (async)
			{
				await HttpPipelinePolicy.ProcessNextAsync(message, pipeline).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				HttpPipelinePolicy.ProcessNext(message, pipeline);
			}
			uint redirectCount = 0u;
			Request request = message.Request;
			Response response = message.Response;
			if (!AllowAutoRedirect(message))
			{
				return;
			}
			Uri uriForRedirect;
			while ((uriForRedirect = GetUriForRedirect(request, message.Response)) != null)
			{
				redirectCount++;
				if (redirectCount > _maxAutomaticRedirections)
				{
					if (AzureCoreEventSource.Singleton.IsEnabled())
					{
						AzureCoreEventSource.Singleton.RequestRedirectCountExceeded(request.ClientRequestId, request.Uri.ToString(), uriForRedirect.ToString());
					}
					break;
				}
				response.Dispose();
				request.Headers.Remove(HttpHeader.Names.Authorization);
				AzureCoreEventSource.Singleton.RequestRedirect(request, uriForRedirect, response);
				request.Uri.Reset(uriForRedirect);
				if (RequestRequiresForceGet(response.Status, request.Method))
				{
					request.Method = RequestMethod.Get;
					request.Content = null;
				}
				if (async)
				{
					await HttpPipelinePolicy.ProcessNextAsync(message, pipeline).ConfigureAwait(continueOnCapturedContext: false);
				}
				else
				{
					HttpPipelinePolicy.ProcessNext(message, pipeline);
				}
				response = message.Response;
			}
		}

		private static Uri? GetUriForRedirect(Request request, Response response)
		{
			int status = response.Status;
			if ((uint)(status - 300) > 3u && (uint)(status - 307) > 1u)
			{
				return null;
			}
			if (!response.Headers.TryGetValue("Location", out string value))
			{
				return null;
			}
			if (!TryParseValue(value, out Uri parsedValue))
			{
				return null;
			}
			Uri uri = request.Uri.ToUri();
			if (!parsedValue.IsAbsoluteUri)
			{
				parsedValue = new Uri(uri, parsedValue);
			}
			string fragment = uri.Fragment;
			if (!string.IsNullOrEmpty(fragment) && string.IsNullOrEmpty(parsedValue.Fragment))
			{
				parsedValue = new UriBuilder(parsedValue)
				{
					Fragment = fragment
				}.Uri;
			}
			if (IsSupportedSecureScheme(uri.Scheme) && !IsSupportedSecureScheme(parsedValue.Scheme))
			{
				if (AzureCoreEventSource.Singleton.IsEnabled())
				{
					AzureCoreEventSource.Singleton.RequestRedirectBlocked(request.ClientRequestId, uri.ToString(), parsedValue.ToString());
				}
				return null;
			}
			return parsedValue;
		}

		private static bool RequestRequiresForceGet(int statusCode, RequestMethod requestMethod)
		{
			switch (statusCode)
			{
			case 300:
			case 301:
			case 302:
				return requestMethod == RequestMethod.Post;
			case 303:
				if (requestMethod != RequestMethod.Get)
				{
					return requestMethod != RequestMethod.Head;
				}
				return false;
			default:
				return false;
			}
		}

		internal static bool IsSupportedSecureScheme(string scheme)
		{
			if (!string.Equals(scheme, "https", StringComparison.OrdinalIgnoreCase))
			{
				return IsSecureWebSocketScheme(scheme);
			}
			return true;
		}

		internal static bool IsSecureWebSocketScheme(string scheme)
		{
			return string.Equals(scheme, "wss", StringComparison.OrdinalIgnoreCase);
		}

		public override ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			return ProcessAsync(message, pipeline, async: true);
		}

		public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			ProcessAsync(message, pipeline, async: false).EnsureCompleted();
		}

		private static bool TryParseValue([NotNullWhen(true)] string? value, [NotNullWhen(true)] out Uri? parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}
			string text = value;
			if (!Uri.TryCreate(text, UriKind.RelativeOrAbsolute, out var result))
			{
				text = DecodeUtf8FromString(text);
				if (!Uri.TryCreate(text, UriKind.RelativeOrAbsolute, out result))
				{
					return false;
				}
			}
			parsedValue = result;
			return true;
		}

		private static string DecodeUtf8FromString(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return input;
			}
			bool flag = false;
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] > 'ÿ')
				{
					return input;
				}
				if (input[i] > '\u007f')
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				byte[] array = new byte[input.Length];
				for (int j = 0; j < input.Length; j++)
				{
					if (input[j] > 'ÿ')
					{
						return input;
					}
					array[j] = (byte)input[j];
				}
				try
				{
					return Encoding.GetEncoding("utf-8", EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback).GetString(array, 0, array.Length);
				}
				catch (ArgumentException)
				{
				}
			}
			return input;
		}

		private bool AllowAutoRedirect(HttpMessage message)
		{
			if (message.TryGetProperty(typeof(AllowRedirectsValueKey), out object value))
			{
				return (bool)value;
			}
			return _allowAutoRedirects;
		}
	}
	public class RetryPolicy : HttpPipelinePolicy
	{
		private readonly int _maxRetries;

		private readonly DelayStrategy _delayStrategy;

		public RetryPolicy(int maxRetries = 3, DelayStrategy? delayStrategy = null)
		{
			_maxRetries = maxRetries;
			_delayStrategy = delayStrategy ?? DelayStrategy.CreateExponentialDelayStrategy();
		}

		public override ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			return ProcessAsync(message, pipeline, async: true);
		}

		public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			ProcessAsync(message, pipeline, async: false).EnsureCompleted();
		}

		private async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline, bool async)
		{
			List<Exception> exceptions = null;
			Exception lastException;
			while (true)
			{
				lastException = null;
				long before = Stopwatch.GetTimestamp();
				if (async)
				{
					await OnSendingRequestAsync(message).ConfigureAwait(continueOnCapturedContext: false);
				}
				else
				{
					OnSendingRequest(message);
				}
				try
				{
					if (async)
					{
						await HttpPipelinePolicy.ProcessNextAsync(message, pipeline).ConfigureAwait(continueOnCapturedContext: false);
					}
					else
					{
						HttpPipelinePolicy.ProcessNext(message, pipeline);
					}
				}
				catch (Exception ex)
				{
					if (exceptions == null)
					{
						exceptions = new List<Exception>();
					}
					exceptions.Add(ex);
					lastException = ex;
				}
				if (async)
				{
					await OnRequestSentAsync(message).ConfigureAwait(continueOnCapturedContext: false);
				}
				else
				{
					OnRequestSent(message);
				}
				long timestamp = Stopwatch.GetTimestamp();
				double elapsed = (double)(timestamp - before) / (double)Stopwatch.Frequency;
				bool flag = false;
				if (lastException != null || (message.HasResponse && message.Response.IsError))
				{
					bool flag2 = ((!async) ? ShouldRetry(message, lastException) : (await ShouldRetryAsync(message, lastException).ConfigureAwait(continueOnCapturedContext: false)));
					flag = flag2;
				}
				if (!flag)
				{
					break;
				}
				TimeSpan? retryAfter = (message.HasResponse ? message.Response.Headers.RetryAfter : ((TimeSpan?)null));
				TimeSpan timeSpan = ((!async) ? GetNextDelay(message, retryAfter) : (await GetNextDelayAsync(message, retryAfter).ConfigureAwait(continueOnCapturedContext: false)));
				TimeSpan timeSpan2 = timeSpan;
				if (timeSpan2 > TimeSpan.Zero)
				{
					if (async)
					{
						await WaitAsync(timeSpan2, message.CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					}
					else
					{
						Wait(timeSpan2, message.CancellationToken);
					}
				}
				if (message.HasResponse)
				{
					message.Response.ContentStream?.Dispose();
				}
				message.RetryNumber++;
				AzureCoreEventSource.Singleton.RequestRetrying(message.Request.ClientRequestId, message.RetryNumber, elapsed);
			}
			if (lastException != null)
			{
				if (exceptions.Count == 1)
				{
					ExceptionDispatchInfo.Capture(lastException).Throw();
				}
				throw new AggregateException(string.Format("Retry failed after {0} tries. Retry settings can be adjusted in {1}.{2}", message.RetryNumber + 1, "ClientOptions", "Retry") + " or by configuring a custom retry policy in ClientOptions.RetryPolicy.", exceptions);
			}
		}

		internal virtual async Task WaitAsync(TimeSpan time, CancellationToken cancellationToken)
		{
			await Task.Delay(time, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		internal virtual void Wait(TimeSpan time, CancellationToken cancellationToken)
		{
			cancellationToken.WaitHandle.WaitOne(time);
		}

		protected internal virtual bool ShouldRetry(HttpMessage message, Exception? exception)
		{
			return ShouldRetryInternal(message, exception);
		}

		protected internal virtual ValueTask<bool> ShouldRetryAsync(HttpMessage message, Exception? exception)
		{
			return new ValueTask<bool>(ShouldRetryInternal(message, exception));
		}

		private bool ShouldRetryInternal(HttpMessage message, Exception? exception)
		{
			if (message.RetryNumber < _maxRetries)
			{
				if (exception != null)
				{
					return message.ResponseClassifier.IsRetriable(message, exception);
				}
				return message.ResponseClassifier.IsRetriableResponse(message);
			}
			return false;
		}

		internal TimeSpan GetNextDelay(HttpMessage message, TimeSpan? retryAfter)
		{
			return GetNextDelayInternal(message);
		}

		internal ValueTask<TimeSpan> GetNextDelayAsync(HttpMessage message, TimeSpan? retryAfter)
		{
			return new ValueTask<TimeSpan>(GetNextDelayInternal(message));
		}

		protected internal virtual void OnSendingRequest(HttpMessage message)
		{
		}

		protected internal virtual ValueTask OnSendingRequestAsync(HttpMessage message)
		{
			return default(ValueTask);
		}

		protected internal virtual void OnRequestSent(HttpMessage message)
		{
		}

		protected internal virtual ValueTask OnRequestSentAsync(HttpMessage message)
		{
			return default(ValueTask);
		}

		private TimeSpan GetNextDelayInternal(HttpMessage message)
		{
			return _delayStrategy.GetNextDelay(message.HasResponse ? message.Response : null, message.RetryNumber + 1);
		}
	}
	public class ServerCertificateCustomValidationArgs
	{
		public X509Certificate2? Certificate { get; }

		public X509Chain? CertificateAuthorityChain { get; }

		public SslPolicyErrors SslPolicyErrors { get; }

		public ServerCertificateCustomValidationArgs(X509Certificate2? certificate, X509Chain? certificateAuthorityChain, SslPolicyErrors sslPolicyErrors)
		{
			Certificate = certificate;
			CertificateAuthorityChain = certificateAuthorityChain;
			SslPolicyErrors = sslPolicyErrors;
		}
	}
	internal static class ServicePointHelpers
	{
		private const int RuntimeDefaultConnectionLimit = 2;

		private const int IncreasedConnectionLimit = 50;

		private const int IncreasedConnectionLeaseTimeout = 300000;

		private static TimeSpan DefaultConnectionLeaseTimeoutTimeSpan = Timeout.InfiniteTimeSpan;

		private static TimeSpan IncreasedConnectionLeaseTimeoutTimeSpan = TimeSpan.FromMilliseconds(300000.0);

		public static void SetLimits(HttpMessageHandler messageHandler)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
			{
				return;
			}
			try
			{
				if (messageHandler is HttpClientHandler { MaxConnectionsPerServer: 2 } httpClientHandler)
				{
					httpClientHandler.MaxConnectionsPerServer = 50;
				}
			}
			catch (NotSupportedException)
			{
			}
			catch (NotImplementedException)
			{
			}
		}
	}
	internal class ClientDiagnostics : DiagnosticScopeFactory
	{
		public ClientDiagnostics(ClientOptions options, bool? suppressNestedClientActivities = null)
			: this(options.GetType().Namespace, GetResourceProviderNamespace(options.GetType().Assembly), options.Diagnostics, suppressNestedClientActivities)
		{
		}

		public ClientDiagnostics(string optionsNamespace, string? providerNamespace, DiagnosticsOptions diagnosticsOptions, bool? suppressNestedClientActivities = null)
			: base(optionsNamespace, providerNamespace, diagnosticsOptions.IsDistributedTracingEnabled, suppressNestedClientActivities ?? true, isStable: true)
		{
		}

		internal static HttpMessageSanitizer CreateMessageSanitizer(DiagnosticsOptions diagnostics)
		{
			return new HttpMessageSanitizer(diagnostics.LoggedQueryParameters.ToArray(), diagnostics.LoggedHeaderNames.ToArray());
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
	internal static class ContentTypeUtilities
	{
		public static bool TryGetTextEncoding(string contentType, out Encoding encoding)
		{
			if (contentType == null)
			{
				encoding = null;
				return false;
			}
			int num = contentType.IndexOf("; charset=", StringComparison.OrdinalIgnoreCase);
			if (num != -1 && MemoryExtensions.AsSpan(contentType).Slice(num + "; charset=".Length).StartsWith(MemoryExtensions.AsSpan("utf-8"), StringComparison.OrdinalIgnoreCase))
			{
				encoding = Encoding.UTF8;
				return true;
			}
			if (contentType.StartsWith("text/", StringComparison.OrdinalIgnoreCase) || contentType.EndsWith("json", StringComparison.OrdinalIgnoreCase) || contentType.EndsWith("xml", StringComparison.OrdinalIgnoreCase) || contentType.EndsWith("-urlencoded", StringComparison.OrdinalIgnoreCase) || contentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase) || contentType.StartsWith("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase))
			{
				encoding = Encoding.UTF8;
				return true;
			}
			encoding = null;
			return false;
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
			SupportsActivitySource = AppContextSwitchHelper.GetConfigValue("Azure.Experimental.EnableActivitySource", "AZURE_EXPERIMENTAL_ENABLE_ACTIVITY_SOURCE");
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
		public DiagnosticScope CreateScope(string name, ActivityKind kind = ActivityKind.Internal)
		{
			if (_source == null)
			{
				return default(DiagnosticScope);
			}
			DiagnosticScope result = new DiagnosticScope(name, _source, null, GetActivitySource(_source.Name, name), kind, _suppressNestedClientActivities);
			if (_resourceProviderNamespace != null)
			{
				result.AddAttribute("az.namespace", _resourceProviderNamespace);
			}
			return result;
		}

		private ActivitySource? GetActivitySource(string ns, string name)
		{
			if (!(_isStable | ActivityExtensions.SupportsActivitySource))
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
namespace Azure.Core.Buffers
{
	internal static class AzureBaseBuffersExtensions
	{
		private const int DefaultCopyBufferSize = 81920;

		public static async Task WriteAsync(this Stream stream, ReadOnlyMemory<byte> buffer, CancellationToken cancellation = default(CancellationToken))
		{
			Argument.AssertNotNull(stream, "stream");
			if (buffer.Length == 0)
			{
				return;
			}
			byte[] array = null;
			try
			{
				if (MemoryMarshal.TryGetArray(buffer, out var segment))
				{
					await stream.WriteAsync(segment.Array, segment.Offset, segment.Count, cancellation).ConfigureAwait(continueOnCapturedContext: false);
					return;
				}
				array = ArrayPool<byte>.Shared.Rent(buffer.Length);
				if (!buffer.TryCopyTo(array))
				{
					throw new Exception("could not rent large enough buffer.");
				}
				await stream.WriteAsync(array, 0, buffer.Length, cancellation).ConfigureAwait(continueOnCapturedContext: false);
			}
			finally
			{
				if (array != null)
				{
					ArrayPool<byte>.Shared.Return(array);
				}
			}
		}

		public static async Task WriteAsync(this Stream stream, ReadOnlySequence<byte> buffer, CancellationToken cancellation = default(CancellationToken))
		{
			Argument.AssertNotNull(stream, "stream");
			if (buffer.Length == 0L)
			{
				return;
			}
			byte[] array = null;
			try
			{
				ReadOnlySequence<byte>.Enumerator enumerator = buffer.GetEnumerator();
				while (enumerator.MoveNext())
				{
					ReadOnlyMemory<byte> current = enumerator.Current;
					if (MemoryMarshal.TryGetArray(current, out var segment))
					{
						await stream.WriteAsync(segment.Array, segment.Offset, segment.Count, cancellation).ConfigureAwait(continueOnCapturedContext: false);
						continue;
					}
					if (array == null || array.Length < current.Length)
					{
						if (array != null)
						{
							ArrayPool<byte>.Shared.Return(array);
						}
						array = ArrayPool<byte>.Shared.Rent(current.Length);
					}
					if (!current.TryCopyTo(array))
					{
						throw new Exception("could not rent large enough buffer.");
					}
					await stream.WriteAsync(array, 0, current.Length, cancellation).ConfigureAwait(continueOnCapturedContext: false);
				}
			}
			finally
			{
				if (array != null)
				{
					ArrayPool<byte>.Shared.Return(array);
				}
			}
		}

		public static async Task CopyToAsync(this Stream source, Stream destination, CancellationToken cancellationToken)
		{
			byte[] buffer = ArrayPool<byte>.Shared.Rent(81920);
			object obj = null;
			try
			{
				while (true)
				{
					int num = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					if (num == 0)
					{
						break;
					}
					await WriteAsync(destination, new ReadOnlyMemory<byte>(buffer, 0, num), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
			}
			catch (object obj2)
			{
				obj = obj2;
			}
			await destination.FlushAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			ArrayPool<byte>.Shared.Return(buffer);
			object obj3 = obj;
			if (obj3 != null)
			{
				ExceptionDispatchInfo.Capture((obj3 as Exception) ?? throw obj3).Throw();
			}
		}

		public static void CopyTo(this Stream source, Stream destination, CancellationToken cancellationToken)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(81920);
			try
			{
				int count;
				while ((count = source.Read(array, 0, array.Length)) != 0)
				{
					cancellationToken.ThrowIfCancellationRequested();
					destination.Write(array, 0, count);
				}
			}
			finally
			{
				destination.Flush();
				ArrayPool<byte>.Shared.Return(array);
			}
		}
	}
}
namespace Azure.Core.GeoJson
{
	public readonly struct GeoArray<T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
	{
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			private readonly GeoArray<T> _array;

			private int _index;

			object IEnumerator.Current => Current;

			public T Current => _array[_index];

			internal Enumerator(GeoArray<T> array)
			{
				this = default(Enumerator);
				_array = array;
				_index = -1;
			}

			public bool MoveNext()
			{
				_index++;
				return _index < _array.Count;
			}

			public void Reset()
			{
				_index = -1;
			}

			public void Dispose()
			{
			}
		}

		private readonly object _container;

		public T this[int index]
		{
			get
			{
				object container = _container;
				if (!(container is T[] array))
				{
					if (!(container is GeoPointCollection geoPointCollection))
					{
						if (!(container is GeoLineStringCollection geoLineStringCollection))
						{
							if (!(container is GeoPolygon geoPolygon))
							{
								if (container is GeoPolygonCollection geoPolygonCollection)
								{
									return (T)(object)geoPolygonCollection.Polygons[index].Coordinates;
								}
								return default(T);
							}
							return (T)(object)geoPolygon.Rings[index].Coordinates;
						}
						return (T)(object)geoLineStringCollection.Lines[index].Coordinates;
					}
					return (T)(object)geoPointCollection.Points[index].Coordinates;
				}
				return array[index];
			}
		}

		public int Count
		{
			get
			{
				object container = _container;
				if (!(container is T[] array))
				{
					if (!(container is GeoPointCollection geoPointCollection))
					{
						if (!(container is GeoLineStringCollection geoLineStringCollection))
						{
							if (!(container is GeoPolygon geoPolygon))
							{
								if (container is GeoPolygonCollection geoPolygonCollection)
								{
									return geoPolygonCollection.Polygons.Count;
								}
								return 0;
							}
							return geoPolygon.Rings.Count;
						}
						return geoLineStringCollection.Lines.Count;
					}
					return geoPointCollection.Points.Count;
				}
				return array.Length;
			}
		}

		internal GeoArray(object container)
		{
			_container = container;
		}

		public Enumerator GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	public sealed class GeoBoundingBox : IEquatable<GeoBoundingBox>
	{
		public double West { get; }

		public double South { get; }

		public double East { get; }

		public double North { get; }

		public double? MinAltitude { get; }

		public double? MaxAltitude { get; }

		public double this[int index]
		{
			get
			{
				double? minAltitude = MinAltitude;
				if (minAltitude.HasValue)
				{
					double valueOrDefault = minAltitude.GetValueOrDefault();
					switch (MaxAltitude)
					{
					case 0.0:
						return West;
					case 1.0:
						return South;
					case 2.0:
						return valueOrDefault;
					case 3.0:
						return East;
					case 4.0:
						return North;
					case 5.0:
					{
						double result;
						return result;
					}
					default:
						throw new IndexOutOfRangeException();
					case null:
						break;
					}
				}
				return index switch
				{
					0 => West, 
					1 => South, 
					2 => East, 
					3 => North, 
					_ => throw new IndexOutOfRangeException(), 
				};
			}
		}

		public GeoBoundingBox(double west, double south, double east, double north)
			: this(west, south, east, north, null, null)
		{
		}

		public GeoBoundingBox(double west, double south, double east, double north, double? minAltitude, double? maxAltitude)
		{
			West = west;
			South = south;
			East = east;
			North = north;
			MinAltitude = minAltitude;
			MaxAltitude = maxAltitude;
		}

		public bool Equals(GeoBoundingBox? other)
		{
			if (other == null)
			{
				return false;
			}
			if (West.Equals(other.West) && South.Equals(other.South) && East.Equals(other.East) && North.Equals(other.North) && Nullable.Equals(MinAltitude, other.MinAltitude))
			{
				return Nullable.Equals(MaxAltitude, other.MaxAltitude);
			}
			return false;
		}

		public override bool Equals(object? obj)
		{
			if (obj is GeoBoundingBox other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCodeBuilder.Combine(West, South, East, North, MinAltitude, MaxAltitude);
		}

		public override string ToString()
		{
			double? minAltitude = MinAltitude;
			if (minAltitude.HasValue)
			{
				double valueOrDefault = minAltitude.GetValueOrDefault();
				minAltitude = MaxAltitude;
				if (minAltitude.HasValue)
				{
					double valueOrDefault2 = minAltitude.GetValueOrDefault();
					return $"[{West}, {South}, {valueOrDefault}, {East}, {North}, {valueOrDefault2}]";
				}
			}
			return $"[{West}, {South}, {East}, {North}]";
		}
	}
	[JsonConverter(typeof(GeoJsonConverter))]
	public sealed class GeoCollection : GeoObject, IReadOnlyList<GeoObject>, IEnumerable<GeoObject>, IEnumerable, IReadOnlyCollection<GeoObject>
	{
		internal IReadOnlyList<GeoObject> Geometries { get; }

		public int Count => Geometries.Count;

		public GeoObject this[int index] => Geometries[index];

		public override GeoObjectType Type { get; } = GeoObjectType.GeometryCollection;

		public GeoCollection(IEnumerable<GeoObject> geometries)
			: this(geometries, null, GeoObject.DefaultProperties)
		{
		}

		public GeoCollection(IEnumerable<GeoObject> geometries, GeoBoundingBox? boundingBox, IReadOnlyDictionary<string, object?> customProperties)
			: base(boundingBox, customProperties)
		{
			Argument.AssertNotNull(geometries, "geometries");
			Geometries = geometries.ToArray();
		}

		public IEnumerator<GeoObject> GetEnumerator()
		{
			return Geometries.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	internal sealed class GeoJsonConverter : JsonConverter<GeoObject>
	{
		private const string PointType = "Point";

		private const string LineStringType = "LineString";

		private const string MultiPointType = "MultiPoint";

		private const string PolygonType = "Polygon";

		private const string MultiLineStringType = "MultiLineString";

		private const string MultiPolygonType = "MultiPolygon";

		private const string GeometryCollectionType = "GeometryCollection";

		private const string TypeProperty = "type";

		private const string GeometriesProperty = "geometries";

		private const string CoordinatesProperty = "coordinates";

		private const string BBoxProperty = "bbox";

		public override bool CanConvert(Type typeToConvert)
		{
			return typeof(GeoObject).IsAssignableFrom(typeToConvert);
		}

		public override GeoObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return Read(JsonDocument.ParseValue(ref reader).RootElement);
		}

		public override void Write(Utf8JsonWriter writer, GeoObject value, JsonSerializerOptions options)
		{
			Write(writer, value);
		}

		internal static GeoObject Read(JsonElement element)
		{
			string text = GetRequiredProperty(element, "type").GetString();
			GeoBoundingBox boundingBox = ReadBoundingBox(in element);
			if (text == "GeometryCollection")
			{
				List<GeoObject> list = new List<GeoObject>();
				foreach (JsonElement item in GetRequiredProperty(element, "geometries").EnumerateArray())
				{
					list.Add(Read(item));
				}
				return new GeoCollection(list, boundingBox, ReadAdditionalProperties(in element, "geometries"));
			}
			IReadOnlyDictionary<string, object> customProperties = ReadAdditionalProperties(in element);
			JsonElement requiredProperty = GetRequiredProperty(element, "coordinates");
			switch (text)
			{
			case "Point":
				return new GeoPoint(ReadCoordinate(requiredProperty), boundingBox, customProperties);
			case "LineString":
				return new GeoLineString(ReadCoordinates(requiredProperty), boundingBox, customProperties);
			case "MultiPoint":
			{
				List<GeoPoint> list4 = new List<GeoPoint>();
				foreach (GeoPosition item2 in ReadCoordinates(requiredProperty))
				{
					list4.Add(new GeoPoint(item2, null, GeoObject.DefaultProperties));
				}
				return new GeoPointCollection(list4, boundingBox, customProperties);
			}
			case "Polygon":
			{
				List<GeoLinearRing> list6 = new List<GeoLinearRing>();
				foreach (JsonElement item3 in requiredProperty.EnumerateArray())
				{
					list6.Add(new GeoLinearRing(ReadCoordinates(item3)));
				}
				return new GeoPolygon(list6, boundingBox, customProperties);
			}
			case "MultiLineString":
			{
				List<GeoLineString> list5 = new List<GeoLineString>();
				foreach (JsonElement item4 in requiredProperty.EnumerateArray())
				{
					list5.Add(new GeoLineString(ReadCoordinates(item4), null, GeoObject.DefaultProperties));
				}
				return new GeoLineStringCollection(list5, boundingBox, customProperties);
			}
			case "MultiPolygon":
			{
				List<GeoPolygon> list2 = new List<GeoPolygon>();
				foreach (JsonElement item5 in requiredProperty.EnumerateArray())
				{
					List<GeoLinearRing> list3 = new List<GeoLinearRing>();
					foreach (JsonElement item6 in item5.EnumerateArray())
					{
						list3.Add(new GeoLinearRing(ReadCoordinates(item6)));
					}
					list2.Add(new GeoPolygon(list3));
				}
				return new GeoPolygonCollection(list2, boundingBox, customProperties);
			}
			default:
				throw new NotSupportedException("Unsupported geometry type '" + text + "' ");
			}
		}

		private static GeoBoundingBox? ReadBoundingBox(in JsonElement element)
		{
			GeoBoundingBox result = null;
			if (element.TryGetProperty("bbox", out var value))
			{
				result = value.GetArrayLength() switch
				{
					4 => new GeoBoundingBox(value[0].GetDouble(), value[1].GetDouble(), value[2].GetDouble(), value[3].GetDouble()), 
					6 => new GeoBoundingBox(value[0].GetDouble(), value[1].GetDouble(), value[3].GetDouble(), value[4].GetDouble(), value[2].GetDouble(), value[5].GetDouble()), 
					_ => throw new JsonException("Only 2 or 3 element coordinates supported"), 
				};
			}
			return result;
		}

		private static IReadOnlyDictionary<string, object?> ReadAdditionalProperties(in JsonElement element, string knownProperty = "coordinates")
		{
			Dictionary<string, object> dictionary = null;
			foreach (JsonProperty item in element.EnumerateObject())
			{
				string name = item.Name;
				if (!name.Equals("type", StringComparison.Ordinal) && !name.Equals("bbox", StringComparison.Ordinal) && !name.Equals(knownProperty, StringComparison.Ordinal))
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<string, object>();
					}
					dictionary.Add(name, ReadAdditionalPropertyValue(item.Value));
				}
			}
			IReadOnlyDictionary<string, object> readOnlyDictionary = dictionary;
			return readOnlyDictionary ?? GeoObject.DefaultProperties;
		}

		private static object? ReadAdditionalPropertyValue(in JsonElement element)
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
						dictionary.Add(item.Name, ReadAdditionalPropertyValue(item.Value));
					}
					return dictionary;
				}
			}
			case JsonValueKind.Array:
			{
				List<object> list = new List<object>();
				foreach (JsonElement item2 in element.EnumerateArray())
				{
					list.Add(ReadAdditionalPropertyValue(item2));
				}
				return list.ToArray();
			}
			default:
				throw new NotSupportedException("Not supported value kind " + element.ValueKind);
			}
		}

		private static IReadOnlyList<GeoPosition> ReadCoordinates(JsonElement coordinatesElement)
		{
			GeoPosition[] array = new GeoPosition[coordinatesElement.GetArrayLength()];
			int num = 0;
			foreach (JsonElement item in coordinatesElement.EnumerateArray())
			{
				array[num] = ReadCoordinate(item);
				num++;
			}
			return array;
		}

		private static GeoPosition ReadCoordinate(JsonElement coordinate)
		{
			int arrayLength = coordinate.GetArrayLength();
			if (arrayLength < 2 || arrayLength > 3)
			{
				throw new JsonException("Only 2 or 3 element coordinates supported");
			}
			double longitude = coordinate[0].GetDouble();
			double latitude = coordinate[1].GetDouble();
			double? altitude = null;
			if (arrayLength > 2)
			{
				altitude = coordinate[2].GetDouble();
			}
			return new GeoPosition(longitude, latitude, altitude);
		}

		internal static void Write(Utf8JsonWriter writer, GeoObject value)
		{
			writer.WriteStartObject();
			if (!(value is GeoPoint geoPoint))
			{
				if (!(value is GeoLineString geoLineString))
				{
					if (!(value is GeoPolygon geoPolygon))
					{
						if (!(value is GeoPointCollection geoPointCollection))
						{
							if (!(value is GeoLineStringCollection geoLineStringCollection))
							{
								if (!(value is GeoPolygonCollection geoPolygonCollection))
								{
									if (!(value is GeoCollection geoCollection))
									{
										throw new NotSupportedException($"Geometry type '{value?.GetType()}' not supported");
									}
									WriteType("GeometryCollection");
									writer.WritePropertyName("geometries");
									writer.WriteStartArray();
									foreach (GeoObject geometry in geoCollection.Geometries)
									{
										Write(writer, geometry);
									}
									writer.WriteEndArray();
								}
								else
								{
									WriteType("MultiPolygon");
									writer.WritePropertyName("coordinates");
									writer.WriteStartArray();
									foreach (GeoPolygon polygon in geoPolygonCollection.Polygons)
									{
										writer.WriteStartArray();
										foreach (GeoLinearRing ring in polygon.Rings)
										{
											WritePositions(ring.Coordinates);
										}
										writer.WriteEndArray();
									}
									writer.WriteEndArray();
								}
							}
							else
							{
								WriteType("MultiLineString");
								writer.WritePropertyName("coordinates");
								writer.WriteStartArray();
								foreach (GeoLineString line in geoLineStringCollection.Lines)
								{
									WritePositions(line.Coordinates);
								}
								writer.WriteEndArray();
							}
						}
						else
						{
							WriteType("MultiPoint");
							writer.WritePropertyName("coordinates");
							writer.WriteStartArray();
							foreach (GeoPoint point in geoPointCollection.Points)
							{
								WritePosition(point.Coordinates);
							}
							writer.WriteEndArray();
						}
					}
					else
					{
						WriteType("Polygon");
						writer.WritePropertyName("coordinates");
						writer.WriteStartArray();
						foreach (GeoLinearRing ring2 in geoPolygon.Rings)
						{
							WritePositions(ring2.Coordinates);
						}
						writer.WriteEndArray();
					}
				}
				else
				{
					WriteType("LineString");
					writer.WritePropertyName("coordinates");
					WritePositions(geoLineString.Coordinates);
				}
			}
			else
			{
				WriteType("Point");
				writer.WritePropertyName("coordinates");
				WritePosition(geoPoint.Coordinates);
			}
			GeoBoundingBox boundingBox = value.BoundingBox;
			if (boundingBox != null)
			{
				writer.WritePropertyName("bbox");
				writer.WriteStartArray();
				writer.WriteNumberValue(boundingBox.West);
				writer.WriteNumberValue(boundingBox.South);
				if (boundingBox.MinAltitude.HasValue)
				{
					writer.WriteNumberValue(boundingBox.MinAltitude.Value);
				}
				writer.WriteNumberValue(boundingBox.East);
				writer.WriteNumberValue(boundingBox.North);
				if (boundingBox.MaxAltitude.HasValue)
				{
					writer.WriteNumberValue(boundingBox.MaxAltitude.Value);
				}
				writer.WriteEndArray();
			}
			foreach (KeyValuePair<string, object> customProperty in value.CustomProperties)
			{
				writer.WritePropertyName(customProperty.Key);
				WriteAdditionalPropertyValue(writer, customProperty.Value);
			}
			writer.WriteEndObject();
			void WritePosition(GeoPosition type)
			{
				writer.WriteStartArray();
				WritePositionValues(type);
				writer.WriteEndArray();
			}
			void WritePositionValues(GeoPosition type)
			{
				writer.WriteNumberValue(type.Longitude);
				writer.WriteNumberValue(type.Latitude);
				if (type.Altitude.HasValue)
				{
					writer.WriteNumberValue(type.Altitude.Value);
				}
			}
			void WritePositions(GeoArray<GeoPosition> positions)
			{
				writer.WriteStartArray();
				foreach (GeoPosition item in positions)
				{
					WritePosition(item);
				}
				writer.WriteEndArray();
			}
			void WriteType(string type)
			{
				writer.WriteString("type", type);
			}
		}

		private static void WriteAdditionalPropertyValue(Utf8JsonWriter writer, object? value)
		{
			if (value != null)
			{
				if (!(value is int value2))
				{
					if (!(value is double value3))
					{
						if (!(value is float value4))
						{
							if (!(value is long value5))
							{
								if (!(value is string value6))
								{
									if (!(value is bool value7))
									{
										if (!(value is IEnumerable<KeyValuePair<string, object>> enumerable))
										{
											if (value is IEnumerable<object> enumerable2)
											{
												writer.WriteStartArray();
												foreach (object item in enumerable2)
												{
													WriteAdditionalPropertyValue(writer, item);
												}
												writer.WriteEndArray();
												return;
											}
											throw new NotSupportedException("Not supported type " + value.GetType());
										}
										writer.WriteStartObject();
										foreach (KeyValuePair<string, object> item2 in enumerable)
										{
											writer.WritePropertyName(item2.Key);
											WriteAdditionalPropertyValue(writer, item2.Value);
										}
										writer.WriteEndObject();
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
				writer.WriteNullValue();
			}
		}

		private static JsonElement GetRequiredProperty(JsonElement element, string name)
		{
			if (!element.TryGetProperty(name, out var value))
			{
				throw new JsonException("GeoJSON object expected to have '" + name + "' property.");
			}
			return value;
		}
	}
	public sealed class GeoLinearRing
	{
		public GeoArray<GeoPosition> Coordinates { get; }

		public GeoLinearRing(IEnumerable<GeoPosition> coordinates)
		{
			Argument.AssertNotNull(coordinates, "coordinates");
			Coordinates = new GeoArray<GeoPosition>(coordinates.ToArray());
			if (Coordinates.Count < 4)
			{
				throw new ArgumentException("The linear ring is required to have at least 4 coordinates");
			}
			if (Coordinates[0] != Coordinates[Coordinates.Count - 1])
			{
				throw new ArgumentException("The first and last coordinate of the linear ring are required to be equal");
			}
		}
	}
	[JsonConverter(typeof(GeoJsonConverter))]
	public sealed class GeoLineString : GeoObject
	{
		public GeoArray<GeoPosition> Coordinates { get; }

		public override GeoObjectType Type { get; } = GeoObjectType.LineString;

		public GeoLineString(IEnumerable<GeoPosition> coordinates)
			: this(coordinates, null, GeoObject.DefaultProperties)
		{
		}

		public GeoLineString(IEnumerable<GeoPosition> coordinates, GeoBoundingBox? boundingBox, IReadOnlyDictionary<string, object?> customProperties)
			: base(boundingBox, customProperties)
		{
			Coordinates = new GeoArray<GeoPosition>(coordinates.ToArray());
		}
	}
	[JsonConverter(typeof(GeoJsonConverter))]
	public sealed class GeoLineStringCollection : GeoObject, IReadOnlyList<GeoLineString>, IEnumerable<GeoLineString>, IEnumerable, IReadOnlyCollection<GeoLineString>
	{
		internal IReadOnlyList<GeoLineString> Lines { get; }

		public int Count => Lines.Count;

		public GeoLineString this[int index] => Lines[index];

		public GeoArray<GeoArray<GeoPosition>> Coordinates => new GeoArray<GeoArray<GeoPosition>>(this);

		public override GeoObjectType Type { get; } = GeoObjectType.MultiLineString;

		public GeoLineStringCollection(IEnumerable<GeoLineString> lines)
			: this(lines, null, GeoObject.DefaultProperties)
		{
		}

		public GeoLineStringCollection(IEnumerable<GeoLineString> lines, GeoBoundingBox? boundingBox, IReadOnlyDictionary<string, object?> customProperties)
			: base(boundingBox, customProperties)
		{
			Argument.AssertNotNull(lines, "lines");
			Lines = lines.ToArray();
		}

		public IEnumerator<GeoLineString> GetEnumerator()
		{
			return Lines.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	[JsonConverter(typeof(GeoJsonConverter))]
	public abstract class GeoObject
	{
		internal static readonly IReadOnlyDictionary<string, object?> DefaultProperties = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());

		internal IReadOnlyDictionary<string, object?> CustomProperties { get; }

		public abstract GeoObjectType Type { get; }

		public GeoBoundingBox? BoundingBox { get; }

		internal GeoObject(GeoBoundingBox? boundingBox, IReadOnlyDictionary<string, object?> customProperties)
		{
			Argument.AssertNotNull(customProperties, "customProperties");
			BoundingBox = boundingBox;
			CustomProperties = customProperties;
		}

		public bool TryGetCustomProperty(string name, out object? value)
		{
			return CustomProperties.TryGetValue(name, out value);
		}

		public override string ToString()
		{
			using MemoryStream memoryStream = new MemoryStream();
			using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream);
			GeoJsonConverter.Write(utf8JsonWriter, this);
			utf8JsonWriter.Flush();
			return Encoding.UTF8.GetString(memoryStream.ToArray());
		}

		public static GeoObject Parse(string json)
		{
			using JsonDocument jsonDocument = JsonDocument.Parse(json);
			return GeoJsonConverter.Read(jsonDocument.RootElement);
		}
	}
	public enum GeoObjectType
	{
		Point,
		MultiPoint,
		Polygon,
		MultiPolygon,
		LineString,
		MultiLineString,
		GeometryCollection
	}
	[JsonConverter(typeof(GeoJsonConverter))]
	public sealed class GeoPoint : GeoObject
	{
		public GeoPosition Coordinates { get; }

		public override GeoObjectType Type { get; }

		public GeoPoint(double longitude, double latitude)
			: this(new GeoPosition(longitude, latitude), null, GeoObject.DefaultProperties)
		{
		}

		public GeoPoint(double longitude, double latitude, double? altitude)
			: this(new GeoPosition(longitude, latitude, altitude), null, GeoObject.DefaultProperties)
		{
		}

		public GeoPoint(GeoPosition position)
			: this(position, null, GeoObject.DefaultProperties)
		{
		}

		public GeoPoint(GeoPosition position, GeoBoundingBox? boundingBox, IReadOnlyDictionary<string, object?> customProperties)
			: base(boundingBox, customProperties)
		{
			Coordinates = position;
		}
	}
	[JsonConverter(typeof(GeoJsonConverter))]
	public sealed class GeoPointCollection : GeoObject, IReadOnlyList<GeoPoint>, IEnumerable<GeoPoint>, IEnumerable, IReadOnlyCollection<GeoPoint>
	{
		internal IReadOnlyList<GeoPoint> Points { get; }

		public int Count => Points.Count;

		public GeoPoint this[int index] => Points[index];

		public GeoArray<GeoPosition> Coordinates => new GeoArray<GeoPosition>(this);

		public override GeoObjectType Type { get; } = GeoObjectType.MultiPoint;

		public GeoPointCollection(IEnumerable<GeoPoint> points)
			: this(points, null, GeoObject.DefaultProperties)
		{
		}

		public GeoPointCollection(IEnumerable<GeoPoint> points, GeoBoundingBox? boundingBox, IReadOnlyDictionary<string, object?> customProperties)
			: base(boundingBox, customProperties)
		{
			Argument.AssertNotNull(points, "points");
			Points = points.ToArray();
		}

		public IEnumerator<GeoPoint> GetEnumerator()
		{
			return Points.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	[JsonConverter(typeof(GeoJsonConverter))]
	public sealed class GeoPolygon : GeoObject
	{
		public IReadOnlyList<GeoLinearRing> Rings { get; }

		public GeoLinearRing OuterRing => Rings[0];

		public GeoArray<GeoArray<GeoPosition>> Coordinates => new GeoArray<GeoArray<GeoPosition>>(this);

		public override GeoObjectType Type { get; } = GeoObjectType.Polygon;

		public GeoPolygon(IEnumerable<GeoPosition> positions)
			: this(new GeoLinearRing[1]
			{
				new GeoLinearRing(positions)
			}, null, GeoObject.DefaultProperties)
		{
		}

		public GeoPolygon(IEnumerable<GeoLinearRing> rings)
			: this(rings, null, GeoObject.DefaultProperties)
		{
		}

		public GeoPolygon(IEnumerable<GeoLinearRing> rings, GeoBoundingBox? boundingBox, IReadOnlyDictionary<string, object?> customProperties)
			: base(boundingBox, customProperties)
		{
			Argument.AssertNotNull(rings, "rings");
			Rings = rings.ToArray();
		}
	}
	[JsonConverter(typeof(GeoJsonConverter))]
	public sealed class GeoPolygonCollection : GeoObject, IReadOnlyList<GeoPolygon>, IEnumerable<GeoPolygon>, IEnumerable, IReadOnlyCollection<GeoPolygon>
	{
		internal IReadOnlyList<GeoPolygon> Polygons { get; }

		public int Count => Polygons.Count;

		public GeoPolygon this[int index] => Polygons[index];

		public GeoArray<GeoArray<GeoArray<GeoPosition>>> Coordinates => new GeoArray<GeoArray<GeoArray<GeoPosition>>>(this);

		public override GeoObjectType Type { get; } = GeoObjectType.MultiPolygon;

		public GeoPolygonCollection(IEnumerable<GeoPolygon> polygons)
			: this(polygons, null, GeoObject.DefaultProperties)
		{
		}

		public GeoPolygonCollection(IEnumerable<GeoPolygon> polygons, GeoBoundingBox? boundingBox, IReadOnlyDictionary<string, object?> customProperties)
			: base(boundingBox, customProperties)
		{
			Argument.AssertNotNull(polygons, "polygons");
			Polygons = polygons.ToArray();
		}

		public IEnumerator<GeoPolygon> GetEnumerator()
		{
			return Polygons.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	public readonly struct GeoPosition : IEquatable<GeoPosition>
	{
		public double? Altitude { get; }

		public double Longitude { get; }

		public double Latitude { get; }

		public double this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return Longitude;
				case 1:
					return Latitude;
				case 2:
					if (Altitude.HasValue)
					{
						return Altitude.Value;
					}
					break;
				}
				throw new IndexOutOfRangeException();
			}
		}

		public int Count
		{
			get
			{
				if (Altitude.HasValue)
				{
					return 3;
				}
				return 2;
			}
		}

		public GeoPosition(double longitude, double latitude)
			: this(longitude, latitude, null)
		{
		}

		public GeoPosition(double longitude, double latitude, double? altitude)
		{
			Longitude = longitude;
			Latitude = latitude;
			Altitude = altitude;
		}

		public bool Equals(GeoPosition other)
		{
			if (Nullable.Equals(Altitude, other.Altitude) && Longitude.Equals(other.Longitude))
			{
				return Latitude.Equals(other.Latitude);
			}
			return false;
		}

		public override bool Equals(object? obj)
		{
			if (obj is GeoPosition other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCodeBuilder.Combine(Longitude, Latitude, Altitude);
		}

		public static bool operator ==(GeoPosition left, GeoPosition right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(GeoPosition left, GeoPosition right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			if (!Altitude.HasValue)
			{
				return $"[{Longitude:G17}, {Latitude:G17}]";
			}
			return $"[{Longitude:G17}, {Latitude:G17}, {Altitude.Value:G17}]";
		}
	}
}
namespace Azure.Core.Extensions
{
	public interface IAzureClientBuilder<TClient, TOptions> where TOptions : class
	{
	}
	public interface IAzureClientFactoryBuilder
	{
		IAzureClientBuilder<TClient, TOptions> RegisterClientFactory<TClient, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TOptions>(Func<TOptions, TClient> clientFactory) where TOptions : class;
	}
	public interface IAzureClientFactoryBuilderWithConfiguration<in TConfiguration> : IAzureClientFactoryBuilder
	{
		[RequiresUnreferencedCode("Binding strongly typed objects to configuration values is not supported with trimming. Use the Configuration Binder Source Generator (EnableConfigurationBindingGenerator=true) instead.")]
		[RequiresDynamicCode("Binding strongly typed objects to configuration values requires generating dynamic code at runtime, for example instantiating generic types. Use the Configuration Binder Source Generator (EnableConfigurationBindingGenerator=true) instead.")]
		IAzureClientBuilder<TClient, TOptions> RegisterClientFactory<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TClient, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TOptions>(TConfiguration configuration) where TOptions : class;
	}
	public interface IAzureClientFactoryBuilderWithCredential
	{
		IAzureClientBuilder<TClient, TOptions> RegisterClientFactory<TClient, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TOptions>(Func<TOptions, TokenCredential, TClient> clientFactory, bool requiresCredential = true) where TOptions : class;
	}
}
namespace Azure.Core.Json
{
	internal struct MutableJsonChange
	{
		public string Path { get; }

		public int Index { get; }

		public object? Value { get; }

		public string? AddedPropertyName { get; }

		public MutableJsonChangeKind ChangeKind { get; }

		public readonly JsonValueKind ValueKind
		{
			get
			{
				object value = Value;
				if (value != null)
				{
					if (!(value is bool))
					{
						if (!(value is string))
						{
							if (!(value is DateTime))
							{
								if (!(value is DateTimeOffset))
								{
									if (!(value is Guid))
									{
										if (!(value is byte))
										{
											if (!(value is sbyte))
											{
												if (!(value is short))
												{
													if (!(value is ushort))
													{
														if (!(value is int))
														{
															if (!(value is uint))
															{
																if (!(value is long))
																{
																	if (!(value is ulong))
																	{
																		if (!(value is float))
																		{
																			if (!(value is double))
																			{
																				if (!(value is decimal))
																				{
																					if (!(value is JsonElement { ValueKind: var valueKind }))
																					{
																						throw new InvalidOperationException($"Unrecognized change type '{Value.GetType()}'.");
																					}
																					return valueKind;
																				}
																				return JsonValueKind.Number;
																			}
																			return JsonValueKind.Number;
																		}
																		return JsonValueKind.Number;
																	}
																	return JsonValueKind.Number;
																}
																return JsonValueKind.Number;
															}
															return JsonValueKind.Number;
														}
														return JsonValueKind.Number;
													}
													return JsonValueKind.Number;
												}
												return JsonValueKind.Number;
											}
											return JsonValueKind.Number;
										}
										return JsonValueKind.Number;
									}
									return JsonValueKind.String;
								}
								return JsonValueKind.String;
							}
							return JsonValueKind.String;
						}
						return JsonValueKind.String;
					}
					return ((bool)value) ? JsonValueKind.True : JsonValueKind.False;
				}
				return JsonValueKind.Null;
			}
		}

		public MutableJsonChange(string path, int index, object? value, MutableJsonChangeKind changeKind, string? addedPropertyName)
		{
			Path = path;
			Index = index;
			Value = value;
			ChangeKind = changeKind;
			AddedPropertyName = addedPropertyName;
		}

		internal readonly void EnsureString()
		{
			if (ValueKind != JsonValueKind.String)
			{
				throw new InvalidOperationException($"Expected a 'String' kind but was '{ValueKind}'.");
			}
		}

		internal readonly void EnsureNumber()
		{
			if (ValueKind != JsonValueKind.Number)
			{
				throw new InvalidOperationException($"Expected a 'Number' kind but was '{ValueKind}'.");
			}
		}

		internal readonly void EnsureArray()
		{
			if (ValueKind != JsonValueKind.Array)
			{
				throw new InvalidOperationException($"Expected an 'Array' kind but was '{ValueKind}'.");
			}
		}

		internal readonly int GetArrayLength()
		{
			EnsureArray();
			if (Value is JsonElement jsonElement)
			{
				return jsonElement.GetArrayLength();
			}
			throw new InvalidOperationException($"Expected an 'Array' kind but was '{ValueKind}'.");
		}

		internal bool IsDescendant(string path)
		{
			return IsDescendant(MemoryExtensions.AsSpan(path));
		}

		internal bool IsDescendant(ReadOnlySpan<char> ancestorPath)
		{
			return IsDescendant(ancestorPath, MemoryExtensions.AsSpan(Path));
		}

		internal static bool IsDescendant(ReadOnlySpan<char> ancestorPath, ReadOnlySpan<char> descendantPath)
		{
			if (ancestorPath.Length == 0)
			{
				return descendantPath.Length > 0;
			}
			if (descendantPath.Length > ancestorPath.Length && descendantPath.StartsWith(ancestorPath))
			{
				return descendantPath[ancestorPath.Length] == '\u0001';
			}
			return false;
		}

		internal bool IsDirectDescendant(string path)
		{
			if (!IsDescendant(path))
			{
				return false;
			}
			string[] array = path.Split(new char[1] { '\u0001' });
			int num = ((!string.IsNullOrEmpty(array[0])) ? array.Length : 0);
			int num2 = Path.Split(new char[1] { '\u0001' }).Length;
			return num == num2 - 1;
		}

		internal bool IsLessThan(ReadOnlySpan<char> otherPath)
		{
			return MemoryExtensions.AsSpan(Path).SequenceCompareTo(otherPath) < 0;
		}

		internal bool IsGreaterThan(ReadOnlySpan<char> otherPath)
		{
			return MemoryExtensions.AsSpan(Path).SequenceCompareTo(otherPath) > 0;
		}

		internal string AsString()
		{
			if (Value == null)
			{
				return "null";
			}
			return Value.ToString();
		}

		public override string ToString()
		{
			return $"Path={Path}; Value={Value}; Kind={ValueKind}; ChangeKind={ChangeKind}";
		}
	}
	internal enum MutableJsonChangeKind
	{
		PropertyUpdate,
		PropertyAddition,
		PropertyRemoval
	}
	[RequiresUnreferencedCode("This class utilizes reflection-based JSON serialization and deserialization which is not compatible with trimming.")]
	[RequiresDynamicCode("This class utilizes reflection-based JSON serialization and deserialization which is not compatible with trimming.")]
	[JsonConverter(typeof(MutableJsonDocumentConverter))]
	internal sealed class MutableJsonDocument : IDisposable
	{
		internal class ChangeTracker
		{
			internal ref struct SegmentEnumerator
			{
				private readonly ReadOnlySpan<char> _path;

				private int _start;

				private int _segmentLength;

				private ReadOnlySpan<char> _current;

				public readonly ReadOnlySpan<char> Current => _current;

				public SegmentEnumerator(ReadOnlySpan<char> path)
				{
					_segmentLength = 0;
					_current = default(ReadOnlySpan<char>);
					_start = 0;
					_path = path;
				}

				public readonly SegmentEnumerator GetEnumerator()
				{
					return this;
				}

				public bool MoveNext()
				{
					if (_start > _path.Length)
					{
						return false;
					}
					_segmentLength = _path.Slice(_start).IndexOf('\u0001');
					if (_segmentLength == -1)
					{
						_segmentLength = _path.Length - _start;
					}
					_current = _path.Slice(_start, _segmentLength);
					_start += _segmentLength + 1;
					return true;
				}
			}

			private List<MutableJsonChange>? _changes;

			internal const char Delimiter = '\u0001';

			internal bool HasChanges
			{
				get
				{
					if (_changes != null)
					{
						return _changes.Count > 0;
					}
					return false;
				}
			}

			internal bool AncestorChanged(string path, int highWaterMark)
			{
				if (_changes == null)
				{
					return false;
				}
				bool flag = false;
				while (!flag && path.Length > 0)
				{
					path = PopProperty(path);
					flag = TryGetChange(path, in highWaterMark, out var _);
				}
				return flag;
			}

			internal bool DescendantChanged(string path, int highWaterMark)
			{
				if (_changes == null)
				{
					return false;
				}
				bool result = false;
				for (int num = _changes.Count - 1; num > highWaterMark; num--)
				{
					if (_changes[num].IsDescendant(path))
					{
						return true;
					}
				}
				return result;
			}

			internal bool TryGetChange(string path, in int lastAppliedChange, out MutableJsonChange change)
			{
				return TryGetChange(MemoryExtensions.AsSpan(path), in lastAppliedChange, out change);
			}

			internal bool TryGetChange(ReadOnlySpan<char> path, in int lastAppliedChange, out MutableJsonChange change)
			{
				if (_changes == null)
				{
					change = default(MutableJsonChange);
					return false;
				}
				for (int num = _changes.Count - 1; num > lastAppliedChange; num--)
				{
					MutableJsonChange mutableJsonChange = _changes[num];
					if (MemoryExtensions.AsSpan(mutableJsonChange.Path).SequenceEqual(path))
					{
						change = mutableJsonChange;
						return true;
					}
				}
				change = default(MutableJsonChange);
				return false;
			}

			internal int AddChange(string path, object? value, MutableJsonChangeKind changeKind = MutableJsonChangeKind.PropertyUpdate, string? addedPropertyName = null)
			{
				if (_changes == null)
				{
					_changes = new List<MutableJsonChange>();
				}
				int count = _changes.Count;
				_changes.Add(new MutableJsonChange(path, count, value, changeKind, addedPropertyName));
				return count;
			}

			internal IEnumerable<MutableJsonChange> GetAddedProperties(string path, int highWaterMark)
			{
				if (_changes == null)
				{
					yield break;
				}
				for (int i = _changes.Count - 1; i > highWaterMark; i--)
				{
					MutableJsonChange mutableJsonChange = _changes[i];
					if (mutableJsonChange.IsDirectDescendant(path) && mutableJsonChange.ChangeKind == MutableJsonChangeKind.PropertyAddition)
					{
						yield return mutableJsonChange;
					}
				}
			}

			internal IEnumerable<MutableJsonChange> GetRemovedProperties(string path, int highWaterMark)
			{
				if (_changes == null)
				{
					yield break;
				}
				for (int i = _changes.Count - 1; i > highWaterMark; i--)
				{
					MutableJsonChange mutableJsonChange = _changes[i];
					if (mutableJsonChange.IsDirectDescendant(path) && mutableJsonChange.ChangeKind == MutableJsonChangeKind.PropertyRemoval)
					{
						yield return mutableJsonChange;
					}
				}
			}

			internal MutableJsonChange? GetFirstMergePatchChange(ReadOnlySpan<char> rootPath, out int maxPathLength)
			{
				maxPathLength = -1;
				if (_changes == null)
				{
					return null;
				}
				MutableJsonChange? result = null;
				for (int num = _changes.Count - 1; num >= 0; num--)
				{
					MutableJsonChange value = _changes[num];
					if (MemoryExtensions.AsSpan(value.Path).StartsWith(rootPath) && (!result.HasValue || value.IsLessThan(MemoryExtensions.AsSpan(result.Value.Path))))
					{
						result = value;
					}
					if (value.Path.Length > maxPathLength)
					{
						maxPathLength = value.Path.Length;
					}
				}
				return result;
			}

			internal MutableJsonChange? GetNextMergePatchChange(ReadOnlySpan<char> rootPath, ReadOnlySpan<char> lastChangePath)
			{
				if (_changes == null)
				{
					return null;
				}
				MutableJsonChange? result = null;
				for (int num = _changes.Count - 1; num >= 0; num--)
				{
					MutableJsonChange value = _changes[num];
					if (MemoryExtensions.AsSpan(value.Path).StartsWith(rootPath) && value.IsGreaterThan(lastChangePath) && (!result.HasValue || value.IsLessThan(MemoryExtensions.AsSpan(result.Value.Path))) && !value.IsDescendant(lastChangePath))
					{
						result = value;
					}
				}
				return result;
			}

			internal bool WasRemoved(string path, int highWaterMark)
			{
				if (_changes == null)
				{
					return false;
				}
				for (int num = _changes.Count - 1; num > highWaterMark; num--)
				{
					MutableJsonChange mutableJsonChange = _changes[num];
					if (mutableJsonChange.Path == path && mutableJsonChange.ChangeKind == MutableJsonChangeKind.PropertyRemoval)
					{
						return true;
					}
				}
				return false;
			}

			internal static SegmentEnumerator Split(ReadOnlySpan<char> path)
			{
				return new SegmentEnumerator(path);
			}

			internal static string PushIndex(string path, int index)
			{
				return PushProperty(path, $"{index}");
			}

			internal static string PopIndex(string path)
			{
				return PopProperty(path);
			}

			internal static string PushProperty(string path, string value)
			{
				if (path.Length == 0)
				{
					return value;
				}
				return path + '\u0001' + value;
			}

			internal static void PushProperty(Span<char> path, ref int pathLength, ReadOnlySpan<char> value)
			{
				if (pathLength == 0)
				{
					value.Slice(0, value.Length).CopyTo(path);
					pathLength = value.Length;
				}
				else
				{
					path[pathLength] = '\u0001';
					value.Slice(0, value.Length).CopyTo(path.Slice(pathLength + 1));
					pathLength += value.Length + 1;
				}
			}

			internal static string PopProperty(string path)
			{
				int num = path.LastIndexOf('\u0001');
				if (num == -1)
				{
					return string.Empty;
				}
				return path.Substring(0, num);
			}

			internal static void PopProperty(Span<char> path, ref int pathLength)
			{
				int num = path.Slice(0, pathLength).LastIndexOf('\u0001');
				pathLength = ((num != -1) ? num : 0);
			}
		}

		[RequiresUnreferencedCode("Using MutableJsonDocument or MutableJsonDocumentConverter is not compatible with trimming due to reflection-based serialization.")]
		[RequiresDynamicCode("Using MutableJsonDocument or MutableJsonDocumentConverter is not compatible with trimming due to reflection-based serialization.")]
		private class MutableJsonDocumentConverter : JsonConverter<MutableJsonDocument>
		{
			public const string classIsIncompatibleWithTrimming = "Using MutableJsonDocument or MutableJsonDocumentConverter is not compatible with trimming due to reflection-based serialization.";

			public override MutableJsonDocument Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				return Parse(ref reader);
			}

			public override void Write(Utf8JsonWriter writer, MutableJsonDocument value, JsonSerializerOptions options)
			{
				value.WriteTo(writer);
			}
		}

		private static readonly JsonSerializerOptions DefaultSerializerOptions = new JsonSerializerOptions();

		private readonly ReadOnlyMemory<byte> _original;

		private readonly JsonDocument _originalDocument;

		private readonly JsonSerializerOptions _serializerOptions;

		internal const string SerializationRequiresUnreferencedCodeClass = "This class utilizes reflection-based JSON serialization and deserialization which is not compatible with trimming.";

		private ChangeTracker? _changeTracker;

		internal JsonSerializerOptions SerializerOptions => _serializerOptions;

		internal ChangeTracker Changes
		{
			get
			{
				if (_changeTracker == null)
				{
					_changeTracker = new ChangeTracker();
				}
				return _changeTracker;
			}
		}

		public MutableJsonElement RootElement => new MutableJsonElement(this, _originalDocument.RootElement, string.Empty);

		public void WriteTo(Stream stream, string? format = null)
		{
			Argument.AssertNotNull(stream, "stream");
			switch (format)
			{
			case "J":
			case null:
				WriteJson(stream);
				break;
			case "P":
				WritePatch(stream);
				break;
			default:
				AssertInvalidFormat(format);
				break;
			}
		}

		internal void AssertInvalidFormat(string? format)
		{
			throw new FormatException("Unsupported format " + format + ". Supported formats are: \"J\" - JSON, \"P\" - JSON Merge Patch.");
		}

		private void WriteJson(Stream stream)
		{
			if (!Changes.HasChanges)
			{
				WriteOriginal(stream);
				return;
			}
			using Utf8JsonWriter writer = new Utf8JsonWriter(stream);
			RootElement.WriteTo(writer);
		}

		private void WriteOriginal(Stream stream)
		{
			if (_original.Length == 0)
			{
				using (Utf8JsonWriter writer = new Utf8JsonWriter(stream))
				{
					_originalDocument.WriteTo(writer);
					return;
				}
			}
			Write(stream, _original.Span);
		}

		private void WritePatch(Stream stream)
		{
			if (!Changes.HasChanges)
			{
				return;
			}
			using Utf8JsonWriter writer = new Utf8JsonWriter(stream);
			RootElement.WritePatch(writer);
		}

		public void WriteTo(Utf8JsonWriter writer)
		{
			Argument.AssertNotNull(writer, "writer");
			if (!Changes.HasChanges)
			{
				_originalDocument.RootElement.WriteTo(writer);
			}
			else
			{
				RootElement.WriteTo(writer);
			}
		}

		private static void Write(Stream stream, ReadOnlySpan<byte> buffer)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			try
			{
				buffer.CopyTo(array);
				stream.Write(array, 0, buffer.Length);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array);
			}
		}

		public static MutableJsonDocument Parse(ReadOnlyMemory<byte> utf8Json, JsonSerializerOptions? serializerOptions = null)
		{
			return new MutableJsonDocument(JsonDocument.Parse(utf8Json), utf8Json, serializerOptions);
		}

		public static MutableJsonDocument Parse(ref Utf8JsonReader reader, JsonSerializerOptions? serializerOptions = null)
		{
			return new MutableJsonDocument(JsonDocument.ParseValue(ref reader), default(ReadOnlyMemory<byte>), serializerOptions);
		}

		public static MutableJsonDocument Parse(BinaryData utf8Json, JsonSerializerOptions? serializerOptions = null)
		{
			return new MutableJsonDocument(JsonDocument.Parse(utf8Json), utf8Json.ToMemory(), serializerOptions);
		}

		public static MutableJsonDocument Parse(string json, JsonSerializerOptions? serializerOptions = null)
		{
			Memory<byte> memory = MemoryExtensions.AsMemory(Encoding.UTF8.GetBytes(json));
			return new MutableJsonDocument(JsonDocument.Parse(memory), memory, serializerOptions);
		}

		public void Dispose()
		{
			_originalDocument.Dispose();
		}

		private MutableJsonDocument(JsonDocument document, ReadOnlyMemory<byte> utf8Json, JsonSerializerOptions? serializerOptions)
		{
			_originalDocument = document;
			_original = utf8Json;
			_serializerOptions = serializerOptions ?? DefaultSerializerOptions;
		}
	}
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "The constructor is marked as RequiresUnreferencedCode, since the whole struct is incompatible with trimming.")]
	[UnconditionalSuppressMessage("ReflectionAnalysis", "IL3050", Justification = "The constructor is marked as RequiresDynamicCode, since the whole struct is incompatible with AOT.")]
	[JsonConverter(typeof(MutableJsonElementConverter))]
	internal readonly struct MutableJsonElement
	{
		[DebuggerDisplay("{Current,nq}")]
		public struct ArrayEnumerator : IEnumerable<MutableJsonElement>, IEnumerable, IEnumerator<MutableJsonElement>, IEnumerator, IDisposable
		{
			private readonly MutableJsonElement _element;

			private readonly int _length;

			private int _index;

			public MutableJsonElement Current
			{
				get
				{
					if (_index < 0)
					{
						return default(MutableJsonElement);
					}
					return _element.GetIndexElement(_index);
				}
			}

			object IEnumerator.Current => Current;

			internal ArrayEnumerator(MutableJsonElement element)
			{
				element.EnsureArray();
				_element = element;
				_length = element._element.GetArrayLength();
				_index = -1;
			}

			public ArrayEnumerator GetEnumerator()
			{
				ArrayEnumerator result = this;
				result._index = -1;
				return result;
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			IEnumerator<MutableJsonElement> IEnumerable<MutableJsonElement>.GetEnumerator()
			{
				return GetEnumerator();
			}

			public void Reset()
			{
				_index = -1;
			}

			public bool MoveNext()
			{
				_index++;
				return _index < _length;
			}

			public void Dispose()
			{
				_index = _length;
			}
		}

		[RequiresUnreferencedCode("Using MutableJsonElement or MutableJsonElementConverter is not compatible with trimming due to reflection-based serialization.")]
		private class MutableJsonElementConverter : JsonConverter<MutableJsonElement>
		{
			public override MutableJsonElement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				return MutableJsonDocument.Parse(ref reader).RootElement;
			}

			public override void Write(Utf8JsonWriter writer, MutableJsonElement value, JsonSerializerOptions options)
			{
				value.WriteTo(writer);
			}
		}

		[DebuggerDisplay("{Current,nq}")]
		public struct ObjectEnumerator : IEnumerable<(string Name, MutableJsonElement Value)>, IEnumerable, IEnumerator<(string Name, MutableJsonElement Value)>, IEnumerator, IDisposable
		{
			private readonly MutableJsonElement _target;

			private JsonElement.ObjectEnumerator _enumerator;

			public (string Name, MutableJsonElement Value) Current => (Name: _enumerator.Current.Name, Value: _target.GetProperty(_enumerator.Current.Name));

			object IEnumerator.Current => _enumerator.Current;

			internal ObjectEnumerator(MutableJsonElement target)
			{
				target.EnsureObject();
				_target = target;
				_enumerator = target.GetJsonElement().EnumerateObject();
			}

			public ObjectEnumerator GetEnumerator()
			{
				ObjectEnumerator result = this;
				result._enumerator = _enumerator.GetEnumerator();
				return result;
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			IEnumerator<(string Name, MutableJsonElement Value)> IEnumerable<(string, MutableJsonElement)>.GetEnumerator()
			{
				return GetEnumerator();
			}

			public void Dispose()
			{
				_enumerator.Dispose();
			}

			public void Reset()
			{
				_enumerator.Reset();
			}

			public bool MoveNext()
			{
				return _enumerator.MoveNext();
			}
		}

		internal const int MaxStackLimit = 1024;

		private readonly MutableJsonDocument _root;

		private readonly JsonElement _element;

		private readonly string _path;

		private readonly int _highWaterMark;

		internal const string SerializationRequiresUnreferencedCodeConstructor = "This struct utilizes reflection-based JSON serialization which is not compatible with trimming.";

		internal const string SerializationRequiresUnreferencedCodeMethod = "This method utilizes reflection-based JSON serialization which is not compatible with trimming.";

		private MutableJsonDocument.ChangeTracker Changes => _root.Changes;

		public JsonValueKind? ValueKind
		{
			get
			{
				if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
				{
					return change.ValueKind;
				}
				return _element.ValueKind;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal string DebuggerDisplay => $"ValueKind = {ValueKind} : \"{ToString()}\"";

		[RequiresUnreferencedCode("This struct utilizes reflection-based JSON serialization which is not compatible with trimming.")]
		[RequiresDynamicCode("This struct utilizes reflection-based JSON serialization which is not compatible with trimming.")]
		internal MutableJsonElement(MutableJsonDocument root, JsonElement element, string path, int highWaterMark = -1)
		{
			_element = element;
			_root = root;
			_path = path;
			_highWaterMark = highWaterMark;
		}

		public MutableJsonElement GetProperty(string name)
		{
			return GetProperty(MemoryExtensions.AsSpan(name));
		}

		public MutableJsonElement GetProperty(ReadOnlySpan<char> name)
		{
			if (!TryGetProperty(name, out var value))
			{
				throw new InvalidOperationException("'" + _path + "' does not contain property called '" + GetString(name, 0, name.Length) + "'");
			}
			return value;
		}

		public bool TryGetProperty(string name, out MutableJsonElement value)
		{
			return TryGetProperty(MemoryExtensions.AsSpan(name), out value);
		}

		public bool TryGetProperty(ReadOnlySpan<char> name, out MutableJsonElement value)
		{
			EnsureValid();
			EnsureObject();
			char[] array = null;
			int length = name.Length;
			length += ((_path.Length > 0) ? (_path.Length + 1) : 0);
			Span<char> span = ((length > 1024) ? ((Span<char>)(array = ArrayPool<char>.Shared.Rent(length))) : stackalloc char[length]);
			Span<char> span2 = span;
			try
			{
				MemoryExtensions.AsSpan(_path).CopyTo(span2);
				int pathLength = _path.Length;
				MutableJsonDocument.ChangeTracker.PushProperty(span2, ref pathLength, name);
				if (Changes.TryGetChange(span2, in _highWaterMark, out var change))
				{
					if (change.ChangeKind == MutableJsonChangeKind.PropertyRemoval)
					{
						value = default(MutableJsonElement);
						return false;
					}
					value = new MutableJsonElement(_root, SerializeToJsonElement(change.Value, _root.SerializerOptions), GetString(span2, 0, pathLength), change.Index);
					return true;
				}
				if (!_element.TryGetProperty(name, out var value2))
				{
					value = default(MutableJsonElement);
					return false;
				}
				value = new MutableJsonElement(_root, value2, GetString(span2, 0, pathLength), _highWaterMark);
				return true;
			}
			finally
			{
				if (array != null)
				{
					ArrayPool<char>.Shared.Return(array);
				}
			}
		}

		private static string GetString(ReadOnlySpan<char> value, int start, int end)
		{
			return new string(value.Slice(start, end).ToArray());
		}

		public int GetArrayLength()
		{
			EnsureValid();
			EnsureArray();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				return change.GetArrayLength();
			}
			return _element.GetArrayLength();
		}

		internal MutableJsonElement GetIndexElement(int index)
		{
			EnsureValid();
			EnsureArray();
			string path = MutableJsonDocument.ChangeTracker.PushIndex(_path, index);
			if (Changes.TryGetChange(path, in _highWaterMark, out var change))
			{
				return new MutableJsonElement(_root, SerializeToJsonElement(change.Value, _root.SerializerOptions), path, change.Index);
			}
			return new MutableJsonElement(_root, _element[index], path, _highWaterMark);
		}

		public bool TryGetDouble(out double value)
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureNumber();
				object value2 = change.Value;
				if (!(value2 is double num))
				{
					if (!(value2 is JsonElement jsonElement))
					{
						if (value2 == null)
						{
							value = 0.0;
							return false;
						}
						value = (double)change.Value;
						return true;
					}
					return jsonElement.TryGetDouble(out value);
				}
				value = num;
				return true;
			}
			return _element.TryGetDouble(out value);
		}

		public double GetDouble()
		{
			if (!TryGetDouble(out var value))
			{
				throw new FormatException(GetFormatExceptionText(_path, typeof(double)));
			}
			return value;
		}

		private static string GetFormatExceptionText(string path, Type type)
		{
			return $"Element at '{path}' cannot be formatted as type '{type}.";
		}

		public bool TryGetInt32(out int value)
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureNumber();
				object value2 = change.Value;
				if (!(value2 is int num))
				{
					if (!(value2 is JsonElement jsonElement))
					{
						if (value2 == null)
						{
							value = 0;
							return false;
						}
						value = (int)change.Value;
						return true;
					}
					return jsonElement.TryGetInt32(out value);
				}
				value = num;
				return true;
			}
			return _element.TryGetInt32(out value);
		}

		public int GetInt32()
		{
			if (!TryGetInt32(out var value))
			{
				throw new FormatException(GetFormatExceptionText(_path, typeof(int)));
			}
			return value;
		}

		public bool TryGetInt64(out long value)
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureNumber();
				object value2 = change.Value;
				if (!(value2 is long num))
				{
					if (!(value2 is JsonElement jsonElement))
					{
						if (value2 == null)
						{
							value = 0L;
							return false;
						}
						value = (long)change.Value;
						return true;
					}
					return jsonElement.TryGetInt64(out value);
				}
				value = num;
				return true;
			}
			return _element.TryGetInt64(out value);
		}

		public long GetInt64()
		{
			if (!TryGetInt64(out var value))
			{
				throw new FormatException(GetFormatExceptionText(_path, typeof(long)));
			}
			return value;
		}

		public bool TryGetSingle(out float value)
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureNumber();
				object value2 = change.Value;
				if (!(value2 is float num))
				{
					if (!(value2 is JsonElement jsonElement))
					{
						if (value2 == null)
						{
							value = 0f;
							return false;
						}
						value = (float)change.Value;
						return true;
					}
					return jsonElement.TryGetSingle(out value);
				}
				value = num;
				return true;
			}
			return _element.TryGetSingle(out value);
		}

		public float GetSingle()
		{
			if (!TryGetSingle(out var value))
			{
				throw new FormatException(GetFormatExceptionText(_path, typeof(float)));
			}
			return value;
		}

		public string? GetString()
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureString();
				object value = change.Value;
				if (!(value is string result))
				{
					if (!(value is JsonElement jsonElement))
					{
						if (value == null)
						{
							return null;
						}
						throw new InvalidOperationException("Element at '" + _path + "' is not a string.");
					}
					return jsonElement.GetString();
				}
				return result;
			}
			return _element.GetString();
		}

		public bool GetBoolean()
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				object value = change.Value;
				if (!(value is bool result))
				{
					if (value is JsonElement jsonElement)
					{
						return jsonElement.GetBoolean();
					}
					throw new InvalidOperationException("Element at '" + _path + "' is not a bool.");
				}
				return result;
			}
			return _element.GetBoolean();
		}

		public bool TryGetByte(out byte value)
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureNumber();
				object value2 = change.Value;
				if (!(value2 is byte b))
				{
					if (!(value2 is JsonElement jsonElement))
					{
						if (value2 == null)
						{
							value = 0;
							return false;
						}
						value = (byte)change.Value;
						return true;
					}
					return jsonElement.TryGetByte(out value);
				}
				value = b;
				return true;
			}
			return _element.TryGetByte(out value);
		}

		public byte GetByte()
		{
			if (!TryGetByte(out var value))
			{
				throw new FormatException(GetFormatExceptionText(_path, typeof(byte)));
			}
			return value;
		}

		public bool TryGetDateTime(out DateTime value)
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureString();
				object value2 = change.Value;
				if (!(value2 is DateTime dateTime))
				{
					if (!(value2 is DateTimeOffset) && !(value2 is string))
					{
						if (!(value2 is JsonElement jsonElement))
						{
							if (value2 == null)
							{
								value = default(DateTime);
								return false;
							}
							throw new InvalidOperationException($"Element {change.Value} cannot be converted to DateTime.");
						}
						return jsonElement.TryGetDateTime(out value);
					}
					return SerializeToJsonElement(change.Value, _root.SerializerOptions).TryGetDateTime(out value);
				}
				value = dateTime;
				return true;
			}
			return _element.TryGetDateTime(out value);
		}

		public DateTime GetDateTime()
		{
			if (!TryGetDateTime(out var value))
			{
				throw new FormatException(GetFormatExceptionText(_path, typeof(DateTime)));
			}
			return value;
		}

		public bool TryGetDateTimeOffset(out DateTimeOffset value)
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureString();
				object value2 = change.Value;
				if (!(value2 is DateTimeOffset dateTimeOffset))
				{
					if (!(value2 is DateTime) && !(value2 is string))
					{
						if (!(value2 is JsonElement jsonElement))
						{
							if (value2 == null)
							{
								value = default(DateTimeOffset);
								return false;
							}
							throw new InvalidOperationException($"Element {change.Value} cannot be converted to DateTimeOffset.");
						}
						return jsonElement.TryGetDateTimeOffset(out value);
					}
					return SerializeToJsonElement(change.Value, _root.SerializerOptions).TryGetDateTimeOffset(out value);
				}
				value = dateTimeOffset;
				return true;
			}
			return _element.TryGetDateTimeOffset(out value);
		}

		public DateTimeOffset GetDateTimeOffset()
		{
			if (!TryGetDateTimeOffset(out var value))
			{
				throw new FormatException(GetFormatExceptionText(_path, typeof(DateTimeOffset)));
			}
			return value;
		}

		public bool TryGetDecimal(out decimal value)
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureNumber();
				object value2 = change.Value;
				if (!(value2 is decimal num))
				{
					if (!(value2 is JsonElement jsonElement))
					{
						if (value2 == null)
						{
							value = default(decimal);
							return false;
						}
						value = (decimal)change.Value;
						return true;
					}
					return jsonElement.TryGetDecimal(out value);
				}
				value = num;
				return true;
			}
			return _element.TryGetDecimal(out value);
		}

		public decimal GetDecimal()
		{
			if (!TryGetDecimal(out var value))
			{
				throw new FormatException(GetFormatExceptionText(_path, typeof(decimal)));
			}
			return value;
		}

		public bool TryGetGuid(out Guid value)
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureString();
				object value2 = change.Value;
				if (!(value2 is Guid guid))
				{
					if (!(value2 is string))
					{
						if (!(value2 is JsonElement jsonElement))
						{
							if (value2 == null)
							{
								value = default(Guid);
								return false;
							}
							throw new InvalidOperationException($"Element {change.Value} cannot be converted to Guid.");
						}
						return jsonElement.TryGetGuid(out value);
					}
					return SerializeToJsonElement(change.Value, _root.SerializerOptions).TryGetGuid(out value);
				}
				value = guid;
				return true;
			}
			return _element.TryGetGuid(out value);
		}

		public Guid GetGuid()
		{
			if (!TryGetGuid(out var value))
			{
				throw new FormatException(GetFormatExceptionText(_path, typeof(Guid)));
			}
			return value;
		}

		public bool TryGetInt16(out short value)
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureNumber();
				object value2 = change.Value;
				if (!(value2 is short num))
				{
					if (!(value2 is JsonElement jsonElement))
					{
						if (value2 == null)
						{
							value = 0;
							return false;
						}
						value = (short)change.Value;
						return true;
					}
					return jsonElement.TryGetInt16(out value);
				}
				value = num;
				return true;
			}
			return _element.TryGetInt16(out value);
		}

		public short GetInt16()
		{
			if (!TryGetInt16(out var value))
			{
				throw new FormatException(GetFormatExceptionText(_path, typeof(short)));
			}
			return value;
		}

		public bool TryGetSByte(out sbyte value)
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureNumber();
				object value2 = change.Value;
				if (!(value2 is sbyte b))
				{
					if (!(value2 is JsonElement jsonElement))
					{
						if (value2 == null)
						{
							value = 0;
							return false;
						}
						value = (sbyte)change.Value;
						return true;
					}
					return jsonElement.TryGetSByte(out value);
				}
				value = b;
				return true;
			}
			return _element.TryGetSByte(out value);
		}

		public sbyte GetSByte()
		{
			if (!TryGetSByte(out var value))
			{
				throw new FormatException(GetFormatExceptionText(_path, typeof(sbyte)));
			}
			return value;
		}

		public bool TryGetUInt16(out ushort value)
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureNumber();
				object value2 = change.Value;
				if (!(value2 is ushort num))
				{
					if (!(value2 is JsonElement jsonElement))
					{
						if (value2 == null)
						{
							value = 0;
							return false;
						}
						value = (ushort)change.Value;
						return true;
					}
					return jsonElement.TryGetUInt16(out value);
				}
				value = num;
				return true;
			}
			return _element.TryGetUInt16(out value);
		}

		public ushort GetUInt16()
		{
			if (!TryGetUInt16(out var value))
			{
				throw new FormatException(GetFormatExceptionText(_path, typeof(ushort)));
			}
			return value;
		}

		public bool TryGetUInt32(out uint value)
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureNumber();
				object value2 = change.Value;
				if (!(value2 is uint num))
				{
					if (!(value2 is JsonElement jsonElement))
					{
						if (value2 == null)
						{
							value = 0u;
							return false;
						}
						value = (uint)change.Value;
						return true;
					}
					return jsonElement.TryGetUInt32(out value);
				}
				value = num;
				return true;
			}
			return _element.TryGetUInt32(out value);
		}

		public uint GetUInt32()
		{
			if (!TryGetUInt32(out var value))
			{
				throw new FormatException(GetFormatExceptionText(_path, typeof(uint)));
			}
			return value;
		}

		public bool TryGetUInt64(out ulong value)
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				change.EnsureNumber();
				object value2 = change.Value;
				if (!(value2 is ulong num))
				{
					if (!(value2 is JsonElement jsonElement))
					{
						if (value2 == null)
						{
							value = 0uL;
							return false;
						}
						value = (ulong)change.Value;
						return true;
					}
					return jsonElement.TryGetUInt64(out value);
				}
				value = num;
				return true;
			}
			return _element.TryGetUInt64(out value);
		}

		public ulong GetUInt64()
		{
			if (!TryGetUInt64(out var value))
			{
				throw new FormatException(GetFormatExceptionText(_path, typeof(ulong)));
			}
			return value;
		}

		public ArrayEnumerator EnumerateArray()
		{
			EnsureValid();
			EnsureArray();
			return new ArrayEnumerator(this);
		}

		public ObjectEnumerator EnumerateObject()
		{
			EnsureValid();
			EnsureObject();
			return new ObjectEnumerator(this);
		}

		public void RemoveProperty(string name)
		{
			EnsureValid();
			EnsureObject();
			if (!_element.TryGetProperty(name, out var _))
			{
				throw new InvalidOperationException("Object does not have property: '" + name + "'.");
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, null, MutableJsonChangeKind.PropertyRemoval);
		}

		public void Set(double value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, double value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(int value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, int value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(long value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, long value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(float value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, float value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(string value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, string value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void SetNull()
		{
			EnsureValid();
			Changes.AddChange(_path, null);
		}

		public MutableJsonElement SetPropertyNull(string name)
		{
			if (TryGetProperty(name, out var value))
			{
				value.SetNull();
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, null, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(bool value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, bool value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(byte value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, byte value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(sbyte value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, sbyte value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(short value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, short value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(ushort value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, ushort value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(uint value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, uint value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(ulong value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, ulong value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(decimal value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, decimal value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(Guid value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, Guid value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(DateTime value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, DateTime value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(DateTimeOffset value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, DateTimeOffset value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public void Set(JsonElement value)
		{
			EnsureValid();
			Changes.AddChange(_path, value);
		}

		public MutableJsonElement SetProperty(string name, JsonElement value)
		{
			if (TryGetProperty(name, out var value2))
			{
				value2.Set(value);
				return this;
			}
			string path = MutableJsonDocument.ChangeTracker.PushProperty(_path, name);
			Changes.AddChange(path, value, MutableJsonChangeKind.PropertyAddition, name);
			return this;
		}

		public override string ToString()
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				return change.AsString();
			}
			if (Changes.DescendantChanged(_path, _highWaterMark))
			{
				return Encoding.UTF8.GetString(GetRawBytes());
			}
			return _element.ToString() ?? "null";
		}

		[RequiresUnreferencedCode("This method utilizes reflection-based JSON serialization which is not compatible with trimming.")]
		[RequiresDynamicCode("This method utilizes reflection-based JSON serialization which is not compatible with trimming.")]
		internal static JsonElement SerializeToJsonElement(object? value, JsonSerializerOptions? options = null)
		{
			return ParseFromBytes(JsonSerializer.SerializeToUtf8Bytes(value, options));
		}

		private static JsonElement ParseFromBytes(byte[] bytes)
		{
			using JsonDocument jsonDocument = JsonDocument.Parse(bytes);
			return jsonDocument.RootElement.Clone();
		}

		internal JsonElement GetJsonElement()
		{
			EnsureValid();
			if (Changes.TryGetChange(_path, in _highWaterMark, out var change))
			{
				return SerializeToJsonElement(change.Value, _root.SerializerOptions);
			}
			if (Changes.DescendantChanged(_path, _highWaterMark))
			{
				return ParseFromBytes(GetRawBytes());
			}
			return _element;
		}

		private byte[] GetRawBytes()
		{
			using MemoryStream memoryStream = new MemoryStream();
			using (Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream))
			{
				WriteTo(writer);
			}
			return memoryStream.ToArray();
		}

		internal static Utf8JsonReader GetReaderForElement(JsonElement element)
		{
			using MemoryStream memoryStream = new MemoryStream();
			using (Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream))
			{
				element.WriteTo(writer);
			}
			return new Utf8JsonReader(MemoryExtensions.AsSpan(memoryStream.GetBuffer()).Slice(0, (int)memoryStream.Position));
		}

		internal void DisposeRoot()
		{
			_root.Dispose();
		}

		private void EnsureObject()
		{
			if (_element.ValueKind != JsonValueKind.Object)
			{
				throw new InvalidOperationException($"Expected an 'Object' type but was '{_element.ValueKind}'.");
			}
		}

		private void EnsureArray()
		{
			if (_element.ValueKind != JsonValueKind.Array)
			{
				throw new InvalidOperationException($"Expected an 'Array' type but was '{_element.ValueKind}'.");
			}
		}

		private void EnsureValid()
		{
			if (Changes.AncestorChanged(_path, _highWaterMark))
			{
				throw new InvalidOperationException("An ancestor node of this element has unapplied changes.  Please re-request this property from the RootElement.");
			}
		}

		internal void WriteTo(Utf8JsonWriter writer, string format)
		{
			if (!(format == "J"))
			{
				if (format == "P")
				{
					WritePatch(writer);
				}
				else
				{
					_root.AssertInvalidFormat(format);
				}
			}
			else
			{
				WriteTo(writer);
			}
		}

		internal void WriteTo(Utf8JsonWriter writer)
		{
			WriteElement(_path, _highWaterMark, _element, writer);
		}

		private void WriteElement(string path, int highWaterMark, JsonElement element, Utf8JsonWriter writer)
		{
			if (Changes.TryGetChange(path, in highWaterMark, out var change))
			{
				if (!(change.Value is JsonElement jsonElement))
				{
					WritePrimitiveChange(change, writer);
					return;
				}
				element = jsonElement;
				highWaterMark = change.Index;
			}
			if (Changes.DescendantChanged(path, highWaterMark))
			{
				switch (element.ValueKind)
				{
				case JsonValueKind.Object:
					WriteObject(path, highWaterMark, element, writer);
					break;
				case JsonValueKind.Array:
					WriteArray(path, highWaterMark, element, writer);
					break;
				default:
					throw new InvalidOperationException("Element doesn't have descendants.");
				}
			}
			else
			{
				element.WriteTo(writer);
			}
		}

		private void WriteObject(string path, int highWaterMark, JsonElement element, Utf8JsonWriter writer)
		{
			writer.WriteStartObject();
			foreach (JsonProperty item in element.EnumerateObject())
			{
				string path2 = MutableJsonDocument.ChangeTracker.PushProperty(path, item.Name);
				if (!Changes.WasRemoved(path2, highWaterMark))
				{
					writer.WritePropertyName(item.Name);
					WriteElement(path2, highWaterMark, item.Value, writer);
				}
			}
			foreach (MutableJsonChange addedProperty in Changes.GetAddedProperties(path, highWaterMark))
			{
				string addedPropertyName = addedProperty.AddedPropertyName;
				string path3 = MutableJsonDocument.ChangeTracker.PushProperty(path, addedPropertyName);
				writer.WritePropertyName(addedPropertyName);
				if (addedProperty.Value is JsonElement element2)
				{
					WriteElement(path3, highWaterMark, element2, writer);
				}
				else
				{
					WritePrimitiveChange(addedProperty, writer);
				}
			}
			writer.WriteEndObject();
		}

		private void WriteArray(string path, int highWaterMark, JsonElement element, Utf8JsonWriter writer)
		{
			writer.WriteStartArray();
			int num = 0;
			foreach (JsonElement item in element.EnumerateArray())
			{
				string path2 = MutableJsonDocument.ChangeTracker.PushIndex(path, num++);
				WriteElement(path2, highWaterMark, item, writer);
			}
			writer.WriteEndArray();
		}

		private void WritePrimitiveChange(MutableJsonChange change, Utf8JsonWriter writer)
		{
			object value = change.Value;
			if (!(value is bool value2))
			{
				if (!(value is byte value3))
				{
					if (!(value is sbyte value4))
					{
						if (!(value is short value5))
						{
							if (!(value is ushort value6))
							{
								if (!(value is int value7))
								{
									if (!(value is uint value8))
									{
										if (!(value is long value9))
										{
											if (!(value is ulong value10))
											{
												if (!(value is float value11))
												{
													if (!(value is double value12))
													{
														if (!(value is decimal value13))
														{
															if (!(value is string value14))
															{
																if (!(value is DateTime value15))
																{
																	if (!(value is DateTimeOffset value16))
																	{
																		if (!(value is Guid value17))
																		{
																			if (value != null)
																			{
																				throw new InvalidOperationException($"Unrecognized change type '{change.Value.GetType()}'.");
																			}
																			writer.WriteNullValue();
																		}
																		else
																		{
																			writer.WriteStringValue(value17);
																		}
																	}
																	else
																	{
																		writer.WriteStringValue(value16);
																	}
																}
																else
																{
																	writer.WriteStringValue(value15);
																}
															}
															else
															{
																writer.WriteStringValue(value14);
															}
														}
														else
														{
															writer.WriteNumberValue(value13);
														}
													}
													else
													{
														writer.WriteNumberValue(value12);
													}
												}
												else
												{
													writer.WriteNumberValue(value11);
												}
											}
											else
											{
												writer.WriteNumberValue(value10);
											}
										}
										else
										{
											writer.WriteNumberValue(value9);
										}
									}
									else
									{
										writer.WriteNumberValue(value8);
									}
								}
								else
								{
									writer.WriteNumberValue(value7);
								}
							}
							else
							{
								writer.WriteNumberValue(value6);
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
				else
				{
					writer.WriteNumberValue(value3);
				}
			}
			else
			{
				writer.WriteBooleanValue(value2);
			}
		}

		internal void WritePatch(Utf8JsonWriter writer)
		{
			if (!Changes.HasChanges)
			{
				return;
			}
			if (ValueKind == JsonValueKind.Array)
			{
				WriteTo(writer);
				return;
			}
			ReadOnlySpan<char> readOnlySpan = MemoryExtensions.AsSpan(_path);
			int maxPathLength;
			MutableJsonChange? mutableJsonChange = Changes.GetFirstMergePatchChange(readOnlySpan, out maxPathLength);
			Span<char> span = ((maxPathLength > 1024) ? ((Span<char>)new char[maxPathLength]) : stackalloc char[maxPathLength]);
			Span<char> span2 = span;
			int targetLength = 0;
			CopyTo(span2, ref targetLength, readOnlySpan);
			MutableJsonElement patchElement = this;
			span = ((maxPathLength > 1024) ? ((Span<char>)new char[maxPathLength]) : stackalloc char[maxPathLength]);
			Span<char> span3 = span;
			int targetLength2 = 0;
			CopyTo(span3, ref targetLength2, readOnlySpan);
			writer.WriteStartObject();
			while (mutableJsonChange.HasValue)
			{
				ReadOnlySpan<char> changePath = MemoryExtensions.AsSpan(mutableJsonChange.Value.Path);
				targetLength2 = readOnlySpan.Length;
				CloseOpenObjects(writer, changePath, span2, ref targetLength, ref patchElement);
				OpenAncestorObjects(writer, changePath.Slice(readOnlySpan.Length), changePath.Length, span3, ref targetLength2, span2, ref targetLength, ref patchElement);
				ReadOnlySpan<char> lastSegment = GetLastSegment(span3.Slice(0, targetLength2));
				writer.WritePropertyName(lastSegment);
				WritePatchValue(writer, mutableJsonChange.Value, span2.Slice(0, targetLength), patchElement);
				MutableJsonDocument.ChangeTracker.PopProperty(span2, ref targetLength);
				patchElement = GetPropertyFromRoot(span2.Slice(0, targetLength));
				mutableJsonChange = Changes.GetNextMergePatchChange(readOnlySpan, span3.Slice(0, targetLength2));
			}
			CloseFinalObjects(writer, span2.Slice(readOnlySpan.Length, targetLength));
			writer.WriteEndObject();
		}

		private static ReadOnlySpan<char> GetFirstSegment(ReadOnlySpan<char> path)
		{
			int num = path.IndexOf('\u0001');
			if (num != -1)
			{
				return path.Slice(0, num);
			}
			return path;
		}

		private static ReadOnlySpan<char> GetLastSegment(ReadOnlySpan<char> path)
		{
			int num = path.LastIndexOf('\u0001');
			if (num != -1)
			{
				return path.Slice(num + 1);
			}
			return path;
		}

		private static void CopyTo(Span<char> target, ref int targetLength, ReadOnlySpan<char> value)
		{
			value.CopyTo(target);
			targetLength = value.Length;
		}

		private void CloseOpenObjects(Utf8JsonWriter writer, ReadOnlySpan<char> changePath, Span<char> patchPath, ref int patchPathLength, ref MutableJsonElement patchElement)
		{
			bool flag = false;
			while (!MutableJsonChange.IsDescendant(patchPath.Slice(0, patchPathLength), changePath))
			{
				writer.WriteEndObject();
				MutableJsonDocument.ChangeTracker.PopProperty(patchPath, ref patchPathLength);
				flag = true;
			}
			if (flag)
			{
				patchElement = GetPropertyFromRoot(patchPath.Slice(0, patchPathLength));
			}
		}

		private void OpenAncestorObjects(Utf8JsonWriter writer, ReadOnlySpan<char> path, in int changePathLength, Span<char> currentPath, ref int currentPathLength, Span<char> patchPath, ref int patchPathLength, ref MutableJsonElement patchElement)
		{
			MutableJsonDocument.ChangeTracker.SegmentEnumerator enumerator = MutableJsonDocument.ChangeTracker.Split(path).GetEnumerator();
			while (enumerator.MoveNext())
			{
				ReadOnlySpan<char> current = enumerator.Current;
				if (current.Length == 0)
				{
					continue;
				}
				MutableJsonDocument.ChangeTracker.PushProperty(currentPath, ref currentPathLength, current);
				if (!patchPath.Slice(0, patchPathLength).StartsWith(currentPath.Slice(0, currentPathLength)) && currentPath.Slice(0, currentPathLength).StartsWith(patchPath.Slice(0, patchPathLength)))
				{
					MutableJsonDocument.ChangeTracker.PushProperty(patchPath, ref patchPathLength, current);
					if (!patchElement.TryGetProperty(current, out patchElement) || patchElement.ValueKind == JsonValueKind.Array || changePathLength == currentPathLength)
					{
						break;
					}
					writer.WritePropertyName(current);
					writer.WriteStartObject();
				}
			}
		}

		private void CloseFinalObjects(Utf8JsonWriter writer, ReadOnlySpan<char> patchPath)
		{
			MutableJsonDocument.ChangeTracker.SegmentEnumerator enumerator = MutableJsonDocument.ChangeTracker.Split(patchPath).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Length > 0)
				{
					writer.WriteEndObject();
				}
			}
		}

		private void WritePatchValue(Utf8JsonWriter writer, MutableJsonChange change, ReadOnlySpan<char> patchPath, MutableJsonElement patchElement)
		{
			switch (change.ChangeKind)
			{
			case MutableJsonChangeKind.PropertyRemoval:
				writer.WriteNullValue();
				break;
			case MutableJsonChangeKind.PropertyAddition:
				patchElement.WriteTo(writer);
				break;
			case MutableJsonChangeKind.PropertyUpdate:
				if (patchElement.ValueKind == JsonValueKind.Object)
				{
					WriteObjectUpdate(writer, patchPath, patchElement);
				}
				else
				{
					patchElement.WriteTo(writer);
				}
				break;
			}
		}

		private void WriteObjectUpdate(Utf8JsonWriter writer, ReadOnlySpan<char> path, MutableJsonElement patchElement)
		{
			bool flag = false;
			foreach (JsonProperty item in GetOriginal(path).EnumerateObject())
			{
				if (!patchElement.TryGetProperty(item.Name, out var _))
				{
					if (!flag)
					{
						writer.WriteStartObject();
						flag = true;
					}
					writer.WritePropertyName(item.Name);
					writer.WriteNullValue();
				}
			}
			if (flag)
			{
				foreach (var item2 in patchElement.EnumerateObject())
				{
					writer.WritePropertyName(item2.Name);
					item2.Value.WriteTo(writer);
				}
				writer.WriteEndObject();
			}
			else
			{
				patchElement.WriteTo(writer);
			}
		}

		private MutableJsonElement GetPropertyFromRoot(ReadOnlySpan<char> path)
		{
			MutableJsonElement result = _root.RootElement;
			MutableJsonDocument.ChangeTracker.SegmentEnumerator enumerator = MutableJsonDocument.ChangeTracker.Split(path).GetEnumerator();
			while (enumerator.MoveNext())
			{
				ReadOnlySpan<char> current = enumerator.Current;
				if (current.Length > 0)
				{
					result = result.GetProperty(current);
				}
			}
			return result;
		}

		private JsonElement GetOriginal(ReadOnlySpan<char> path)
		{
			JsonElement result = _root.RootElement._element;
			MutableJsonDocument.ChangeTracker.SegmentEnumerator enumerator = MutableJsonDocument.ChangeTracker.Split(path).GetEnumerator();
			while (enumerator.MoveNext())
			{
				ReadOnlySpan<char> current = enumerator.Current;
				if (current.Length > 0)
				{
					result = result.GetProperty(current);
				}
			}
			return result;
		}
	}
}
namespace Azure.Core.Serialization
{
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[RequiresUnreferencedCode("This class utilizes reflection-based JSON serialization and deserialization which is not compatible with trimming.")]
	[RequiresDynamicCode("This class utilizes reflection-based JSON serialization and deserialization which is not compatible with trimming.")]
	[JsonConverter(typeof(DynamicDataJsonConverter))]
	public sealed class DynamicData : IDisposable, IDynamicMetaObjectProvider
	{
		internal class AllowList
		{
			public static void AssertAllowedValue<T>(T value)
			{
				if (value == null || IsAllowedValue(value))
				{
					return;
				}
				throw new NotSupportedException($"Assigning this value is not supported, either because its type '{value.GetType()}' is not allowed, or because it contains unallowed types.");
			}

			private static bool IsAllowedValue<T>(T value)
			{
				if (value == null)
				{
					return true;
				}
				Type type = value.GetType();
				if (IsAllowedType(type))
				{
					return true;
				}
				if (IsAllowedCollectionValue(type, value))
				{
					return true;
				}
				return IsAllowedAnonymousValue(type, value);
			}

			private static bool IsAllowedType(Type type)
			{
				if (!type.IsPrimitive && !(type == typeof(decimal)) && !(type == typeof(string)) && !(type == typeof(DateTime)) && !(type == typeof(DateTimeOffset)) && !(type == typeof(TimeSpan)) && !(type == typeof(Uri)) && !(type == typeof(Guid)) && !(type == typeof(ETag)) && !(type == typeof(JsonElement)) && !(type == typeof(JsonDocument)))
				{
					return type == typeof(DynamicData);
				}
				return true;
			}

			private static bool IsAllowedCollectionValue<T>(Type type, T value)
			{
				if (!IsAllowedArrayValue(type, value) && !IsAllowedListValue(type, value))
				{
					return IsAllowedDictionaryValue(type, value);
				}
				return true;
			}

			private static bool IsAllowedArrayValue<T>(Type type, T value)
			{
				if (!(value is Array enumerable))
				{
					return false;
				}
				Type elementType = type.GetElementType();
				if (elementType == null)
				{
					return false;
				}
				if (elementType.IsPrimitive || elementType == typeof(string))
				{
					return true;
				}
				return IsAllowedEnumerableValue(elementType, enumerable);
			}

			private static bool IsAllowedListValue<T>(Type type, T value)
			{
				if (value == null)
				{
					return true;
				}
				if (!type.IsGenericType)
				{
					return false;
				}
				if (type.GetGenericTypeDefinition() != typeof(List<>))
				{
					return false;
				}
				Type type2 = type.GetGenericArguments()[0];
				if (type2.IsPrimitive || type2 == typeof(string))
				{
					return true;
				}
				return IsAllowedEnumerableValue(type2, (IEnumerable)(object)value);
			}

			private static bool IsAllowedDictionaryValue<T>(Type type, T value)
			{
				if (value == null)
				{
					return true;
				}
				if (!type.IsGenericType)
				{
					return false;
				}
				if (type.GetGenericTypeDefinition() != typeof(Dictionary<, >))
				{
					return false;
				}
				Type[] genericArguments = type.GetGenericArguments();
				if (genericArguments[0] != typeof(string))
				{
					return false;
				}
				if (genericArguments[1].IsPrimitive || genericArguments[1] == typeof(string))
				{
					return true;
				}
				return IsAllowedEnumerableValue(genericArguments[1], ((IDictionary)(object)value).Values);
			}

			private static bool IsAllowedEnumerableValue(Type elementType, IEnumerable enumerable)
			{
				foreach (object item in enumerable)
				{
					if (item != null)
					{
						if (item.GetType() != elementType && !IsAllowedType(item.GetType()))
						{
							return false;
						}
						if (!IsAllowedValue(item))
						{
							return false;
						}
					}
				}
				return true;
			}

			private static bool IsAllowedAnonymousValue<T>(Type type, T value)
			{
				if (!IsAnonymousType(type))
				{
					return false;
				}
				PropertyInfo[] properties = type.GetProperties();
				for (int i = 0; i < properties.Length; i++)
				{
					if (!IsAllowedValue(properties[i].GetValue(value)))
					{
						return false;
					}
				}
				return true;
			}

			private static bool IsAnonymousType(Type type)
			{
				return type.Name.StartsWith("<>f__AnonymousType");
			}
		}

		[DebuggerDisplay("{Current,nq}")]
		internal struct ArrayEnumerator : IEnumerable<DynamicData>, IEnumerable, IEnumerator<DynamicData>, IEnumerator, IDisposable
		{
			private MutableJsonElement.ArrayEnumerator _enumerator;

			private readonly DynamicDataOptions _options;

			public DynamicData Current => new DynamicData(_enumerator.Current, _options);

			object IEnumerator.Current => Current;

			internal ArrayEnumerator(MutableJsonElement.ArrayEnumerator enumerator, DynamicDataOptions options)
			{
				_enumerator = enumerator;
				_options = options;
			}

			public ArrayEnumerator GetEnumerator()
			{
				return new ArrayEnumerator(_enumerator.GetEnumerator(), _options);
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			IEnumerator<DynamicData> IEnumerable<DynamicData>.GetEnumerator()
			{
				return GetEnumerator();
			}

			public void Reset()
			{
				_enumerator.Reset();
			}

			public bool MoveNext()
			{
				return _enumerator.MoveNext();
			}

			public void Dispose()
			{
				_enumerator.Dispose();
			}
		}

		internal class DynamicDateTimeConverter : JsonConverter<DateTime>
		{
			public string Format { get; }

			public DynamicDateTimeConverter(string format)
			{
				Format = format;
			}

			public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				if (Format.Equals("x", StringComparison.InvariantCultureIgnoreCase))
				{
					return DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()).UtcDateTime;
				}
				return DateTime.Parse(reader.GetString() ?? throw new JsonException($"Failed to read 'string' value at JSON position {reader.Position}."), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToUniversalTime();
			}

			public override void Write(Utf8JsonWriter writer, DateTime dateTimeValue, JsonSerializerOptions options)
			{
				if (Format.Equals("x", StringComparison.InvariantCultureIgnoreCase))
				{
					if (dateTimeValue.Kind != DateTimeKind.Utc)
					{
						throw new NotSupportedException($"DateTime {dateTimeValue} has a Kind of {dateTimeValue.Kind}. Azure SDK requires it to be UTC. You can call DateTime.SpecifyKind to change Kind property value to DateTimeKind.Utc.");
					}
					long num = ((DateTimeOffset)dateTimeValue).ToUnixTimeSeconds();
					long value = num;
					writer.WriteNumberValue(value);
				}
				else
				{
					if (dateTimeValue.Kind != DateTimeKind.Utc)
					{
						throw new NotSupportedException($"DateTime {dateTimeValue} has a Kind of {dateTimeValue.Kind}. Azure SDK requires it to be UTC. You can call DateTime.SpecifyKind to change Kind property value to DateTimeKind.Utc.");
					}
					string text = dateTimeValue.ToUniversalTime().ToString(Format, CultureInfo.InvariantCulture);
					string value2 = text;
					writer.WriteStringValue(value2);
				}
			}
		}

		internal class DynamicDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
		{
			public string Format { get; }

			public DynamicDateTimeOffsetConverter(string format)
			{
				Format = format;
			}

			public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				if (Format.Equals("x", StringComparison.InvariantCultureIgnoreCase))
				{
					return DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()).ToUniversalTime();
				}
				return DateTimeOffset.Parse(reader.GetString() ?? throw new JsonException($"Failed to read 'string' value at JSON position {reader.Position}."), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
			}

			public override void Write(Utf8JsonWriter writer, DateTimeOffset dateTimeOffsetValue, JsonSerializerOptions options)
			{
				if (Format.Equals("x", StringComparison.InvariantCultureIgnoreCase))
				{
					long value = dateTimeOffsetValue.ToUniversalTime().ToUnixTimeSeconds();
					writer.WriteNumberValue(value);
				}
				else
				{
					string value2 = dateTimeOffsetValue.ToUniversalTime().UtcDateTime.ToString(Format, CultureInfo.InvariantCulture);
					writer.WriteStringValue(value2);
				}
			}
		}

		internal class DynamicTimeSpanConverter : JsonConverter<TimeSpan>
		{
			public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				return TimeSpan.ParseExact(reader.GetString() ?? throw new JsonException($"Failed to read 'string' value at JSON position {reader.Position}."), "c", CultureInfo.InvariantCulture);
			}

			public override void Write(Utf8JsonWriter writer, TimeSpan timeValue, JsonSerializerOptions options)
			{
				string value = timeValue.ToString("c", CultureInfo.InvariantCulture);
				writer.WriteStringValue(value);
			}
		}

		[RequiresUnreferencedCode("Using DynamicData or DynamicDataConverter is not compatible with trimming due to reflection-based serialization.")]
		[RequiresDynamicCode("Using DynamicData or DynamicDataConverter is not compatible with trimming due to reflection-based serialization.")]
		private class DynamicDataJsonConverter : JsonConverter<DynamicData>
		{
			public const string ClassIsIncompatibleWithTrimming = "Using DynamicData or DynamicDataConverter is not compatible with trimming due to reflection-based serialization.";

			public override DynamicData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				return new DynamicData(MutableJsonDocument.Parse(ref reader).RootElement, DynamicDataOptions.FromSerializerOptions(options));
			}

			public override void Write(Utf8JsonWriter writer, DynamicData value, JsonSerializerOptions options)
			{
				value._element.WriteTo(writer);
			}
		}

		private class MetaObject : DynamicMetaObject
		{
			private DynamicData _value;

			internal MetaObject(Expression parameter, IDynamicMetaObjectProvider value)
				: base(parameter, BindingRestrictions.Empty, value)
			{
				_value = (DynamicData)value;
			}

			public override IEnumerable<string> GetDynamicMemberNames()
			{
				if (_value._element.ValueKind == JsonValueKind.Object)
				{
					return from p in _value._element.EnumerateObject()
						select p.Name;
				}
				return Array.Empty<string>();
			}

			public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
			{
				UnaryExpression instance = Expression.Convert(base.Expression, base.LimitType);
				Expression[] arguments = new Expression[1] { Expression.Constant(binder.Name) };
				MethodCallExpression expression = Expression.Call(instance, GetPropertyMethod, arguments);
				BindingRestrictions typeRestriction = BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
				return new DynamicMetaObject(expression, typeRestriction);
			}

			public override DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
			{
				UnaryExpression instance = Expression.Convert(base.Expression, base.LimitType);
				Expression[] arguments = new Expression[1] { Expression.Convert(indexes[0].Expression, typeof(object)) };
				MethodCallExpression expression = Expression.Call(instance, GetViaIndexerMethod, arguments);
				BindingRestrictions typeRestriction = BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
				return new DynamicMetaObject(expression, typeRestriction);
			}

			public override DynamicMetaObject BindConvert(ConvertBinder binder)
			{
				Expression expression = Expression.Convert(base.Expression, base.LimitType);
				BindingRestrictions typeRestriction = BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
				if (binder.Type == typeof(IEnumerable))
				{
					return new DynamicMetaObject(Expression.Call(expression, GetEnumerableMethod), typeRestriction);
				}
				if (binder.Type == typeof(IDisposable))
				{
					return new DynamicMetaObject(Expression.Convert(expression, binder.Type), typeRestriction);
				}
				if (CastFromOperators.TryGetValue(binder.Type, out MethodInfo value))
				{
					return new DynamicMetaObject(Expression.Call(value, expression), typeRestriction);
				}
				return new DynamicMetaObject(Expression.Call(expression, "ConvertTo", new Type[1] { binder.Type }), typeRestriction);
			}

			public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
			{
				UnaryExpression instance = Expression.Convert(base.Expression, base.LimitType);
				Expression[] arguments = new Expression[2]
				{
					Expression.Constant(binder.Name),
					Expression.Convert(value.Expression, typeof(object))
				};
				MethodCallExpression expression = Expression.Call(instance, SetPropertyMethod, arguments);
				BindingRestrictions typeRestriction = BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
				return new DynamicMetaObject(expression, typeRestriction);
			}

			public override DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
			{
				UnaryExpression instance = Expression.Convert(base.Expression, base.LimitType);
				Expression[] arguments = new Expression[2]
				{
					Expression.Convert(indexes[0].Expression, typeof(object)),
					Expression.Convert(value.Expression, typeof(object))
				};
				MethodCallExpression expression = Expression.Call(instance, SetViaIndexerMethod, arguments);
				BindingRestrictions typeRestriction = BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
				return new DynamicMetaObject(expression, typeRestriction);
			}
		}

		[DebuggerDisplay("{Current,nq}")]
		internal struct ObjectEnumerator : IEnumerable<DynamicDataProperty>, IEnumerable, IEnumerator<DynamicDataProperty>, IEnumerator, IDisposable
		{
			private MutableJsonElement.ObjectEnumerator _enumerator;

			private readonly DynamicDataOptions _options;

			public DynamicDataProperty Current => new DynamicDataProperty(_enumerator.Current.Name, new DynamicData(_enumerator.Current.Value, _options));

			object IEnumerator.Current => Current;

			internal ObjectEnumerator(MutableJsonElement.ObjectEnumerator enumerator, DynamicDataOptions options)
			{
				_enumerator = enumerator;
				_options = options;
			}

			public ObjectEnumerator GetEnumerator()
			{
				return new ObjectEnumerator(_enumerator.GetEnumerator(), _options);
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			IEnumerator<DynamicDataProperty> IEnumerable<DynamicDataProperty>.GetEnumerator()
			{
				return GetEnumerator();
			}

			public void Reset()
			{
				_enumerator.Reset();
			}

			public bool MoveNext()
			{
				return _enumerator.MoveNext();
			}

			public void Dispose()
			{
				_enumerator.Dispose();
			}
		}

		internal const string RoundTripFormat = "o";

		internal const string UnixFormat = "x";

		private static readonly MethodInfo GetPropertyMethod = typeof(DynamicData).GetMethod("GetProperty", BindingFlags.Instance | BindingFlags.NonPublic);

		private static readonly MethodInfo SetPropertyMethod = typeof(DynamicData).GetMethod("SetProperty", BindingFlags.Instance | BindingFlags.NonPublic);

		private static readonly MethodInfo GetEnumerableMethod = typeof(DynamicData).GetMethod("GetEnumerable", BindingFlags.Instance | BindingFlags.NonPublic);

		private static readonly MethodInfo GetViaIndexerMethod = typeof(DynamicData).GetMethod("GetViaIndexer", BindingFlags.Instance | BindingFlags.NonPublic);

		private static readonly MethodInfo SetViaIndexerMethod = typeof(DynamicData).GetMethod("SetViaIndexer", BindingFlags.Instance | BindingFlags.NonPublic);

		private MutableJsonElement _element;

		private readonly DynamicDataOptions _options;

		private readonly JsonSerializerOptions _serializerOptions;

		internal const string SerializationRequiresUnreferencedCodeClass = "This class utilizes reflection-based JSON serialization and deserialization which is not compatible with trimming.";

		private static readonly Dictionary<Type, MethodInfo> CastFromOperators = (from method in typeof(DynamicData).GetMethods(BindingFlags.Static | BindingFlags.Public)
			where method.Name == "op_Explicit" || method.Name == "op_Implicit"
			select method).ToDictionary((MethodInfo method) => method.ReturnType);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay => _element.DebuggerDisplay;

		internal DynamicData(MutableJsonElement element, DynamicDataOptions options)
		{
			_element = element;
			_options = options;
			_serializerOptions = DynamicDataOptions.ToSerializerOptions(options);
		}

		internal void WriteTo(Stream stream)
		{
			using Utf8JsonWriter writer = new Utf8JsonWriter(stream);
			_element.WriteTo(writer);
		}

		private object? GetProperty(string name)
		{
			Argument.AssertNotNullOrEmpty(name, "name");
			if (_element.ValueKind == JsonValueKind.Array && name == "Length")
			{
				return _element.GetArrayLength();
			}
			if (_element.TryGetProperty(name, out var value))
			{
				if (value.ValueKind == JsonValueKind.Null)
				{
					return null;
				}
				return new DynamicData(value, _options);
			}
			if (_options.PropertyNameFormat != JsonPropertyNames.UseExact && _element.TryGetProperty(FormatPropertyName(name), out value))
			{
				if (value.ValueKind == JsonValueKind.Null)
				{
					return null;
				}
				return new DynamicData(value, _options);
			}
			return null;
		}

		private string FormatPropertyName(string value)
		{
			return _options.PropertyNameFormat switch
			{
				JsonPropertyNames.UseExact => value, 
				JsonPropertyNames.CamelCase => JsonNamingPolicy.CamelCase.ConvertName(value), 
				_ => throw new NotSupportedException($"Unknown value for DynamicDataOptions.PropertyNamingConvention: '{_options.PropertyNameFormat}'."), 
			};
		}

		private object? GetViaIndexer(object index)
		{
			if (!(index is string text))
			{
				if (index is int index2)
				{
					MutableJsonElement indexElement = _element.GetIndexElement(index2);
					if (indexElement.ValueKind == JsonValueKind.Null)
					{
						return null;
					}
					return new DynamicData(indexElement, _options);
				}
				throw new InvalidOperationException($"Tried to access indexer with an unsupported index type: {index}");
			}
			if (_element.TryGetProperty(text, out var value))
			{
				if (value.ValueKind == JsonValueKind.Null)
				{
					return null;
				}
				return new DynamicData(value, _options);
			}
			throw new KeyNotFoundException("Could not find JSON member with name '" + text + "'.");
		}

		private IEnumerable GetEnumerable()
		{
			return _element.ValueKind switch
			{
				JsonValueKind.Array => new ArrayEnumerator(_element.EnumerateArray(), _options), 
				JsonValueKind.Object => new ObjectEnumerator(_element.EnumerateObject(), _options), 
				_ => throw new InvalidCastException($"Unable to enumerate JSON element of kind '{_element.ValueKind}'.  Cannot cast value to IEnumerable."), 
			};
		}

		private object? SetProperty(string name, object value)
		{
			Argument.AssertNotNullOrEmpty(name, "name");
			AllowList.AssertAllowedValue(value);
			if (HasTypeConverter(value))
			{
				value = ConvertType(value);
			}
			if (_options.PropertyNameFormat == JsonPropertyNames.UseExact || _element.TryGetProperty(name, out var _))
			{
				SetPropertyInternal(name, value);
				return null;
			}
			SetPropertyInternal(FormatPropertyName(name), value);
			return null;
		}

		private static bool HasTypeConverter(object value)
		{
			if (!(value is DateTime))
			{
				if (!(value is DateTimeOffset))
				{
					if (value is TimeSpan)
					{
						return true;
					}
					return false;
				}
				return true;
			}
			return true;
		}

		private JsonElement ConvertType(object value)
		{
			return MutableJsonElement.SerializeToJsonElement(value, _serializerOptions);
		}

		private object? SetViaIndexer(object index, object value)
		{
			AllowList.AssertAllowedValue(value);
			if (!(index is string name))
			{
				if (index is int index2)
				{
					MutableJsonElement element = _element.GetIndexElement(index2);
					SetInternal(ref element, value);
					return new DynamicData(element, _options);
				}
				throw new InvalidOperationException($"Tried to access indexer with an unsupported index type: {index}");
			}
			SetPropertyInternal(name, value);
			return null;
		}

		private void SetPropertyInternal(string name, object value)
		{
			if (!(value is bool value2))
			{
				if (!(value is string value3))
				{
					if (!(value is byte value4))
					{
						if (!(value is sbyte value5))
						{
							if (!(value is short value6))
							{
								if (!(value is ushort value7))
								{
									if (!(value is int value8))
									{
										if (!(value is uint value9))
										{
											if (!(value is long value10))
											{
												if (!(value is ulong value11))
												{
													if (!(value is float value12))
													{
														if (!(value is double value13))
														{
															if (!(value is decimal value14))
															{
																if (!(value is DateTime value15))
																{
																	if (!(value is DateTimeOffset value16))
																	{
																		if (!(value is Guid value17))
																		{
																			if (value != null)
																			{
																				if (value is JsonElement value18)
																				{
																					_element = _element.SetProperty(name, value18);
																					return;
																				}
																				JsonElement value19 = ConvertType(value);
																				_element = _element.SetProperty(name, value19);
																			}
																			else
																			{
																				_element = _element.SetPropertyNull(name);
																			}
																		}
																		else
																		{
																			_element = _element.SetProperty(name, value17);
																		}
																	}
																	else
																	{
																		_element = _element.SetProperty(name, value16);
																	}
																}
																else
																{
																	_element = _element.SetProperty(name, value15);
																}
															}
															else
															{
																_element = _element.SetProperty(name, value14);
															}
														}
														else
														{
															_element = _element.SetProperty(name, value13);
														}
													}
													else
													{
														_element = _element.SetProperty(name, value12);
													}
												}
												else
												{
													_element = _element.SetProperty(name, value11);
												}
											}
											else
											{
												_element = _element.SetProperty(name, value10);
											}
										}
										else
										{
											_element = _element.SetProperty(name, value9);
										}
									}
									else
									{
										_element = _element.SetProperty(name, value8);
									}
								}
								else
								{
									_element = _element.SetProperty(name, value7);
								}
							}
							else
							{
								_element = _element.SetProperty(name, value6);
							}
						}
						else
						{
							_element = _element.SetProperty(name, value5);
						}
					}
					else
					{
						_element = _element.SetProperty(name, value4);
					}
				}
				else
				{
					_element = _element.SetProperty(name, value3);
				}
			}
			else
			{
				_element = _element.SetProperty(name, value2);
			}
		}

		private void SetInternal(ref MutableJsonElement element, object value)
		{
			if (!(value is bool value2))
			{
				if (!(value is string value3))
				{
					if (!(value is byte value4))
					{
						if (!(value is sbyte value5))
						{
							if (!(value is short value6))
							{
								if (!(value is ushort value7))
								{
									if (!(value is int value8))
									{
										if (!(value is uint value9))
										{
											if (!(value is long value10))
											{
												if (!(value is ulong value11))
												{
													if (!(value is float value12))
													{
														if (!(value is double value13))
														{
															if (!(value is decimal value14))
															{
																if (!(value is DateTime value15))
																{
																	if (!(value is DateTimeOffset value16))
																	{
																		if (!(value is Guid value17))
																		{
																			if (value != null)
																			{
																				if (value is JsonElement value18)
																				{
																					element.Set(value18);
																					return;
																				}
																				JsonElement value19 = ConvertType(value);
																				element.Set(value19);
																			}
																			else
																			{
																				element.SetNull();
																			}
																		}
																		else
																		{
																			element.Set(value17);
																		}
																	}
																	else
																	{
																		element.Set(value16);
																	}
																}
																else
																{
																	element.Set(value15);
																}
															}
															else
															{
																element.Set(value14);
															}
														}
														else
														{
															element.Set(value13);
														}
													}
													else
													{
														element.Set(value12);
													}
												}
												else
												{
													element.Set(value11);
												}
											}
											else
											{
												element.Set(value10);
											}
										}
										else
										{
											element.Set(value9);
										}
									}
									else
									{
										element.Set(value8);
									}
								}
								else
								{
									element.Set(value7);
								}
							}
							else
							{
								element.Set(value6);
							}
						}
						else
						{
							element.Set(value5);
						}
					}
					else
					{
						element.Set(value4);
					}
				}
				else
				{
					element.Set(value3);
				}
			}
			else
			{
				element.Set(value2);
			}
		}

		private T? ConvertTo<T>()
		{
			JsonElement jsonElement = _element.GetJsonElement();
			try
			{
				Utf8JsonReader reader = MutableJsonElement.GetReaderForElement(jsonElement);
				return JsonSerializer.Deserialize<T>(ref reader, _serializerOptions);
			}
			catch (JsonException innerException)
			{
				throw new InvalidCastException($"Unable to convert value of kind '{jsonElement.ValueKind}' to type '{typeof(T)}'.", innerException);
			}
		}

		public override string ToString()
		{
			return _element.ToString();
		}

		public void Dispose()
		{
			_element.DisposeRoot();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object? obj)
		{
			if (obj == null)
			{
				return _element.ValueKind == JsonValueKind.Null;
			}
			if (_element.ValueKind == JsonValueKind.Null)
			{
				if (obj is DynamicData other)
				{
					return Equals(other);
				}
				return false;
			}
			if (!(obj is string text))
			{
				if (!(obj is bool flag))
				{
					if (!(obj is byte b))
					{
						if (!(obj is sbyte b2))
						{
							if (!(obj is short num))
							{
								if (!(obj is ushort num2))
								{
									if (!(obj is int num3))
									{
										if (!(obj is uint num4))
										{
											if (!(obj is long num5))
											{
												if (!(obj is ulong num6))
												{
													if (!(obj is float num7))
													{
														if (!(obj is double num8))
														{
															if (!(obj is decimal num9))
															{
																if (obj is DynamicData other2)
																{
																	return Equals(other2);
																}
																return base.Equals(obj);
															}
															decimal value;
															return _element.ValueKind == JsonValueKind.Number && _element.TryGetDecimal(out value) && num9 == value;
														}
														double value2;
														return _element.ValueKind == JsonValueKind.Number && _element.TryGetDouble(out value2) && num8 == value2;
													}
													float value3;
													return _element.ValueKind == JsonValueKind.Number && _element.TryGetSingle(out value3) && num7 == value3;
												}
												ulong value4;
												return _element.ValueKind == JsonValueKind.Number && _element.TryGetUInt64(out value4) && num6 == value4;
											}
											long value5;
											return _element.ValueKind == JsonValueKind.Number && _element.TryGetInt64(out value5) && num5 == value5;
										}
										uint value6;
										return _element.ValueKind == JsonValueKind.Number && _element.TryGetUInt32(out value6) && num4 == value6;
									}
									int value7;
									return _element.ValueKind == JsonValueKind.Number && _element.TryGetInt32(out value7) && num3 == value7;
								}
								ushort value8;
								return _element.ValueKind == JsonValueKind.Number && _element.TryGetUInt16(out value8) && num2 == value8;
							}
							short value9;
							return _element.ValueKind == JsonValueKind.Number && _element.TryGetInt16(out value9) && num == value9;
						}
						sbyte value10;
						return _element.ValueKind == JsonValueKind.Number && _element.TryGetSByte(out value10) && b2 == value10;
					}
					byte value11;
					return _element.ValueKind == JsonValueKind.Number && _element.TryGetByte(out value11) && b == value11;
				}
				return (_element.ValueKind == JsonValueKind.True || _element.ValueKind == JsonValueKind.False) && _element.GetBoolean() == flag;
			}
			return _element.ValueKind == JsonValueKind.String && _element.GetString() == text;
		}

		internal bool Equals(DynamicData other)
		{
			if ((object)other == null)
			{
				return _element.ValueKind == JsonValueKind.Null;
			}
			if (_element.ValueKind != other._element.ValueKind)
			{
				return false;
			}
			return _element.ValueKind switch
			{
				JsonValueKind.String => _element.GetString() == other._element.GetString(), 
				JsonValueKind.Number => NumberEqual(other), 
				JsonValueKind.True => true, 
				JsonValueKind.False => true, 
				JsonValueKind.Null => true, 
				_ => base.Equals(other), 
			};
		}

		private bool NumberEqual(DynamicData other)
		{
			if (_element.TryGetDouble(out var value))
			{
				if (other._element.TryGetDouble(out var value2))
				{
					return value == value2;
				}
				return false;
			}
			if (_element.TryGetInt64(out var value3))
			{
				if (other._element.TryGetInt64(out var value4))
				{
					return value3 == value4;
				}
				return false;
			}
			return false;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return _element.GetHashCode();
		}

		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return new MetaObject(parameter, this);
		}

		public static implicit operator bool(DynamicData value)
		{
			try
			{
				return value._element.GetBoolean();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(bool), value._element), innerException);
			}
		}

		public static implicit operator string?(DynamicData value)
		{
			try
			{
				return value._element.GetString();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(string), value._element), innerException);
			}
		}

		public static implicit operator byte(DynamicData value)
		{
			try
			{
				return value._element.GetByte();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(byte), value._element), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidCastException(GetInvalidFormatExceptionText(typeof(byte), value._element), innerException2);
			}
		}

		public static implicit operator sbyte(DynamicData value)
		{
			try
			{
				return value._element.GetSByte();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(sbyte), value._element), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidCastException(GetInvalidFormatExceptionText(typeof(sbyte), value._element), innerException2);
			}
		}

		public static implicit operator short(DynamicData value)
		{
			try
			{
				return value._element.GetInt16();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(short), value._element), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidCastException(GetInvalidFormatExceptionText(typeof(short), value._element), innerException2);
			}
		}

		public static implicit operator ushort(DynamicData value)
		{
			try
			{
				return value._element.GetUInt16();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(ushort), value._element), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidCastException(GetInvalidFormatExceptionText(typeof(ushort), value._element), innerException2);
			}
		}

		public static implicit operator int(DynamicData value)
		{
			try
			{
				return value._element.GetInt32();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(int), value._element), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidCastException(GetInvalidFormatExceptionText(typeof(int), value._element), innerException2);
			}
		}

		public static implicit operator uint(DynamicData value)
		{
			try
			{
				return value._element.GetUInt32();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(uint), value._element), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidCastException(GetInvalidFormatExceptionText(typeof(uint), value._element), innerException2);
			}
		}

		public static implicit operator long(DynamicData value)
		{
			try
			{
				return value._element.GetInt64();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(long), value._element), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidCastException(GetInvalidFormatExceptionText(typeof(long), value._element), innerException2);
			}
		}

		public static implicit operator ulong(DynamicData value)
		{
			try
			{
				return value._element.GetUInt64();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(ulong), value._element), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidCastException(GetInvalidFormatExceptionText(typeof(ulong), value._element), innerException2);
			}
		}

		public static implicit operator float(DynamicData value)
		{
			try
			{
				return value._element.GetSingle();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(float), value._element), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidCastException(GetInvalidFormatExceptionText(typeof(float), value._element), innerException2);
			}
		}

		public static implicit operator double(DynamicData value)
		{
			try
			{
				return value._element.GetDouble();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(double), value._element), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidCastException(GetInvalidFormatExceptionText(typeof(double), value._element), innerException2);
			}
		}

		public static implicit operator decimal(DynamicData value)
		{
			try
			{
				return value._element.GetDecimal();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(decimal), value._element), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidCastException(GetInvalidFormatExceptionText(typeof(decimal), value._element), innerException2);
			}
		}

		public static explicit operator DateTime(DynamicData value)
		{
			try
			{
				if (value._options.DateTimeFormat.Equals("x", StringComparison.InvariantCultureIgnoreCase))
				{
					return value.ConvertTo<DateTime>();
				}
				return value._element.GetDateTime();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(DateTime), value._element), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidCastException(GetInvalidFormatExceptionText(typeof(DateTime), value._element), innerException2);
			}
		}

		public static explicit operator DateTimeOffset(DynamicData value)
		{
			try
			{
				if (value._options.DateTimeFormat.Equals("x", StringComparison.InvariantCultureIgnoreCase))
				{
					return value.ConvertTo<DateTimeOffset>();
				}
				return value._element.GetDateTimeOffset();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(DateTimeOffset), value._element), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidCastException(GetInvalidFormatExceptionText(typeof(DateTimeOffset), value._element), innerException2);
			}
		}

		public static explicit operator Guid(DynamicData value)
		{
			try
			{
				return value._element.GetGuid();
			}
			catch (InvalidOperationException innerException)
			{
				throw new InvalidCastException(GetInvalidKindExceptionText(typeof(Guid), value._element), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new InvalidCastException(GetInvalidFormatExceptionText(typeof(Guid), value._element), innerException2);
			}
		}

		public static bool operator ==(DynamicData? left, object? right)
		{
			return left?.Equals(right) ?? (right == null);
		}

		public static bool operator !=(DynamicData? left, object? right)
		{
			return !(left == right);
		}

		private static string GetInvalidKindExceptionText(Type target, MutableJsonElement element)
		{
			return $"Unable to cast element to '{target}'.  Element has kind '{element.ValueKind}'.";
		}

		private static string GetInvalidFormatExceptionText(Type target, MutableJsonElement element)
		{
			return $"Unable to cast element to '{target}'.  Element has value '{element}'.";
		}
	}
	internal class DynamicDataOptions
	{
		public JsonPropertyNames PropertyNameFormat { get; set; }

		public string DateTimeFormat { get; set; }

		public DynamicDataOptions()
		{
			DateTimeFormat = "o";
		}

		public DynamicDataOptions(DynamicDataOptions options)
		{
			PropertyNameFormat = options.PropertyNameFormat;
			DateTimeFormat = options.DateTimeFormat;
		}

		internal static JsonSerializerOptions ToSerializerOptions(DynamicDataOptions options)
		{
			JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
			{
				Converters = 
				{
					(JsonConverter)new DynamicData.DynamicTimeSpanConverter(),
					(JsonConverter)new DynamicData.DynamicDateTimeConverter(options.DateTimeFormat),
					(JsonConverter)new DynamicData.DynamicDateTimeOffsetConverter(options.DateTimeFormat)
				}
			};
			JsonPropertyNames propertyNameFormat = options.PropertyNameFormat;
			if (propertyNameFormat != JsonPropertyNames.UseExact && propertyNameFormat == JsonPropertyNames.CamelCase)
			{
				jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
			}
			return jsonSerializerOptions;
		}

		internal static DynamicDataOptions FromSerializerOptions(JsonSerializerOptions options)
		{
			DynamicDataOptions dynamicDataOptions = new DynamicDataOptions();
			if (options.Converters.FirstOrDefault((JsonConverter c) => c is DynamicData.DynamicDateTimeConverter) is DynamicData.DynamicDateTimeConverter dynamicDateTimeConverter)
			{
				dynamicDataOptions.DateTimeFormat = dynamicDateTimeConverter.Format;
			}
			if (options.PropertyNamingPolicy == JsonNamingPolicy.CamelCase)
			{
				dynamicDataOptions.PropertyNameFormat = JsonPropertyNames.CamelCase;
			}
			return dynamicDataOptions;
		}
	}
	internal readonly struct DynamicDataProperty : IDynamicMetaObjectProvider
	{
		private class MetaObject : DynamicMetaObject
		{
			private static readonly string[] _memberNames = new string[2] { "Name", "Value" };

			internal MetaObject(Expression parameter, IDynamicMetaObjectProvider value)
				: base(parameter, BindingRestrictions.Empty, value)
			{
			}

			public override IEnumerable<string> GetDynamicMemberNames()
			{
				return _memberNames;
			}

			public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
			{
				MemberExpression expression = Expression.Property(Expression.Convert(base.Expression, base.LimitType), binder.Name);
				BindingRestrictions typeRestriction = BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
				return new DynamicMetaObject(expression, typeRestriction);
			}
		}

		public string Name { get; }

		public DynamicData Value { get; }

		internal DynamicDataProperty(string name, DynamicData value)
		{
			Name = name;
			Value = value;
		}

		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return new MetaObject(parameter, this);
		}
	}
	public interface IMemberNameConverter
	{
		string? ConvertMemberName(MemberInfo member);
	}
	[RequiresUnreferencedCode("This class uses reflection-based JSON serialization and deserialization that is not compatible with trimming.")]
	[RequiresDynamicCode("This class uses reflection-based JSON serialization and deserialization that is not compatible with trimming.")]
	public class JsonObjectSerializer : ObjectSerializer, IMemberNameConverter
	{
		private const int JsonIgnoreConditionAlways = 1;

		private static PropertyInfo? s_jsonIgnoreAttributeCondition;

		private static bool s_jsonIgnoreAttributeConditionInitialized;

		private readonly ConcurrentDictionary<MemberInfo, string?> _cache;

		private readonly JsonSerializerOptions _options;

		public static JsonObjectSerializer Default { get; } = new JsonObjectSerializer();

		public JsonObjectSerializer()
			: this(new JsonSerializerOptions())
		{
		}

		public JsonObjectSerializer(JsonSerializerOptions options)
		{
			_options = options ?? throw new ArgumentNullException("options");
			_cache = new ConcurrentDictionary<MemberInfo, string>();
		}

		public override void Serialize(Stream stream, object? value, Type inputType, CancellationToken cancellationToken)
		{
			byte[] array = JsonSerializer.SerializeToUtf8Bytes(value, inputType, _options);
			stream.Write(array, 0, array.Length);
		}

		public override async ValueTask SerializeAsync(Stream stream, object? value, Type inputType, CancellationToken cancellationToken)
		{
			await JsonSerializer.SerializeAsync(stream, value, inputType, _options, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		public override object? Deserialize(Stream stream, Type returnType, CancellationToken cancellationToken)
		{
			using MemoryStream memoryStream = new MemoryStream();
			stream.CopyTo(memoryStream);
			return JsonSerializer.Deserialize(memoryStream.ToArray(), returnType, _options);
		}

		public override ValueTask<object?> DeserializeAsync(Stream stream, Type returnType, CancellationToken cancellationToken)
		{
			return JsonSerializer.DeserializeAsync(stream, returnType, _options, cancellationToken);
		}

		public override BinaryData Serialize(object? value, Type? inputType = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SerializeToBinaryDataInternal(value, inputType);
		}

		public override ValueTask<BinaryData> SerializeAsync(object? value, Type? inputType = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new ValueTask<BinaryData>(SerializeToBinaryDataInternal(value, inputType));
		}

		private BinaryData SerializeToBinaryDataInternal(object? value, Type? inputType)
		{
			return new BinaryData(JsonSerializer.SerializeToUtf8Bytes(value, inputType ?? value?.GetType() ?? typeof(object), _options));
		}

		string? IMemberNameConverter.ConvertMemberName(MemberInfo member)
		{
			Argument.AssertNotNull(member, "member");
			return _cache.GetOrAdd(member, delegate(MemberInfo m)
			{
				if (m is PropertyInfo propertyInfo)
				{
					if (propertyInfo.GetIndexParameters().Length != 0)
					{
						return (string?)null;
					}
					MethodInfo getMethod = propertyInfo.GetMethod;
					if ((object)getMethod == null || !getMethod.IsPublic)
					{
						MethodInfo setMethod = propertyInfo.SetMethod;
						if ((object)setMethod == null || !setMethod.IsPublic)
						{
							goto IL_0064;
						}
					}
					JsonIgnoreAttribute customAttribute = propertyInfo.GetCustomAttribute<JsonIgnoreAttribute>();
					if (customAttribute != null && GetCondition(customAttribute) == 1)
					{
						return (string?)null;
					}
					if (propertyInfo.GetCustomAttribute<JsonExtensionDataAttribute>() != null)
					{
						return (string?)null;
					}
					return GetPropertyName(propertyInfo);
				}
				goto IL_0064;
				IL_0064:
				return (string?)null;
			});
		}

		private static int GetCondition(JsonIgnoreAttribute attribute)
		{
			if (!s_jsonIgnoreAttributeConditionInitialized)
			{
				s_jsonIgnoreAttributeCondition = typeof(JsonIgnoreAttribute).GetProperty("Condition", BindingFlags.Instance | BindingFlags.Public);
				s_jsonIgnoreAttributeConditionInitialized = true;
			}
			if (s_jsonIgnoreAttributeCondition != null)
			{
				return (int)s_jsonIgnoreAttributeCondition.GetValue(attribute);
			}
			return 1;
		}

		private string GetPropertyName(MemberInfo memberInfo)
		{
			JsonPropertyNameAttribute customAttribute = memberInfo.GetCustomAttribute<JsonPropertyNameAttribute>(inherit: false);
			if (customAttribute != null)
			{
				return customAttribute.Name ?? throw new InvalidOperationException($"The JSON property name for '{memberInfo.DeclaringType}.{memberInfo.Name}' cannot be null.");
			}
			if (_options.PropertyNamingPolicy != null)
			{
				return _options.PropertyNamingPolicy.ConvertName(memberInfo.Name) ?? throw new InvalidOperationException($"The JSON property name for '{memberInfo.DeclaringType}.{memberInfo.Name}' cannot be null.");
			}
			return memberInfo.Name;
		}
	}
	public enum JsonPropertyNames
	{
		UseExact,
		CamelCase
	}
	public abstract class ObjectSerializer
	{
		public abstract void Serialize(Stream stream, object? value, Type inputType, CancellationToken cancellationToken);

		public abstract ValueTask SerializeAsync(Stream stream, object? value, Type inputType, CancellationToken cancellationToken);

		public abstract object? Deserialize(Stream stream, Type returnType, CancellationToken cancellationToken);

		public abstract ValueTask<object?> DeserializeAsync(Stream stream, Type returnType, CancellationToken cancellationToken);

		public virtual BinaryData Serialize(object? value, Type? inputType = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SerializeToBinaryDataInternalAsync(value, inputType, async: false, cancellationToken).EnsureCompleted();
		}

		public virtual async ValueTask<BinaryData> SerializeAsync(object? value, Type? inputType = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await SerializeToBinaryDataInternalAsync(value, inputType, async: true, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}

		private async ValueTask<BinaryData> SerializeToBinaryDataInternalAsync(object? value, Type? inputType, bool async, CancellationToken cancellationToken)
		{
			using MemoryStream stream = new MemoryStream();
			if ((object)inputType == null)
			{
				inputType = value?.GetType() ?? typeof(object);
			}
			if (async)
			{
				await SerializeAsync(stream, value, inputType, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				Serialize(stream, value, inputType, cancellationToken);
			}
			return new BinaryData(MemoryExtensions.AsMemory(stream.GetBuffer(), 0, (int)stream.Position));
		}
	}
}
namespace Azure.Core.Diagnostics
{
	[EventSource(Name = "Azure-Core")]
	internal sealed class AzureCoreEventSource : AzureEventSource
	{
		private const string EventSourceName = "Azure-Core";

		private const int RequestEvent = 1;

		private const int RequestContentEvent = 2;

		private const int ResponseEvent = 5;

		private const int ResponseContentEvent = 6;

		private const int ResponseDelayEvent = 7;

		private const int ErrorResponseEvent = 8;

		private const int ErrorResponseContentEvent = 9;

		private const int RequestRetryingEvent = 10;

		private const int ResponseContentBlockEvent = 11;

		private const int ErrorResponseContentBlockEvent = 12;

		private const int ResponseContentTextEvent = 13;

		private const int ErrorResponseContentTextEvent = 14;

		private const int ResponseContentTextBlockEvent = 15;

		private const int ErrorResponseContentTextBlockEvent = 16;

		private const int RequestContentTextEvent = 17;

		private const int ExceptionResponseEvent = 18;

		private const int BackgroundRefreshFailedEvent = 19;

		private const int RequestRedirectEvent = 20;

		private const int RequestRedirectBlockedEvent = 21;

		private const int RequestRedirectCountExceededEvent = 22;

		private const int PipelineTransportOptionsNotAppliedEvent = 23;

		public static AzureCoreEventSource Singleton { get; } = new AzureCoreEventSource();

		private AzureCoreEventSource()
			: base("Azure-Core")
		{
		}

		[Event(19, Level = EventLevel.Informational, Message = "Background token refresh [{0}] failed with exception {1}")]
		public void BackgroundRefreshFailed(string requestId, string exception)
		{
			WriteEvent(19, requestId, exception);
		}

		[NonEvent]
		public void Request(Request request, string? assemblyName, HttpMessageSanitizer sanitizer)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.None))
			{
				Request(request.ClientRequestId, request.Method.ToString(), sanitizer.SanitizeUrl(request.Uri.ToString()), FormatHeaders(request.Headers, sanitizer), assemblyName);
			}
		}

		[Event(1, Level = EventLevel.Informational, Message = "Request [{0}] {1} {2}\r\n{3}client assembly: {4}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void Request(string requestId, string method, string uri, string headers, string? clientAssembly)
		{
			WriteEvent(1, requestId, method, uri, headers, clientAssembly);
		}

		[NonEvent]
		public void RequestContent(string requestId, byte[] content, Encoding? textEncoding)
		{
			if (IsEnabled(EventLevel.Verbose, EventKeywords.None))
			{
				if (textEncoding != null)
				{
					RequestContentText(requestId, textEncoding.GetString(content));
				}
				else
				{
					RequestContent(requestId, content);
				}
			}
		}

		[Event(2, Level = EventLevel.Verbose, Message = "Request [{0}] content: {1}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with an array with primitive type elements.")]
		public void RequestContent(string requestId, byte[] content)
		{
			WriteEvent(2, requestId, content);
		}

		[Event(17, Level = EventLevel.Verbose, Message = "Request [{0}] content: {1}")]
		public void RequestContentText(string requestId, string content)
		{
			WriteEvent(17, requestId, content);
		}

		[NonEvent]
		public void Response(Response response, HttpMessageSanitizer sanitizer, double elapsed)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.None))
			{
				Response(response.ClientRequestId, response.Status, response.ReasonPhrase, FormatHeaders(response.Headers, sanitizer), elapsed);
			}
		}

		[Event(5, Level = EventLevel.Informational, Message = "Response [{0}] {1} {2} ({4:00.0}s)\r\n{3}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void Response(string requestId, int status, string reasonPhrase, string headers, double seconds)
		{
			WriteEvent(5, requestId, status, reasonPhrase, headers, seconds);
		}

		[NonEvent]
		public void ResponseContent(string requestId, byte[] content, Encoding? textEncoding)
		{
			if (IsEnabled(EventLevel.Verbose, EventKeywords.None))
			{
				if (textEncoding != null)
				{
					ResponseContentText(requestId, textEncoding.GetString(content));
				}
				else
				{
					ResponseContent(requestId, content);
				}
			}
		}

		[Event(6, Level = EventLevel.Verbose, Message = "Response [{0}] content: {1}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with an array with primitive type elements.")]
		public void ResponseContent(string requestId, byte[] content)
		{
			WriteEvent(6, requestId, content);
		}

		[Event(13, Level = EventLevel.Verbose, Message = "Response [{0}] content: {1}")]
		public void ResponseContentText(string requestId, string content)
		{
			WriteEvent(13, requestId, content);
		}

		[NonEvent]
		public void ResponseContentBlock(string requestId, int blockNumber, byte[] content, Encoding? textEncoding)
		{
			if (IsEnabled(EventLevel.Verbose, EventKeywords.None))
			{
				if (textEncoding != null)
				{
					ResponseContentTextBlock(requestId, blockNumber, textEncoding.GetString(content));
				}
				else
				{
					ResponseContentBlock(requestId, blockNumber, content);
				}
			}
		}

		[Event(11, Level = EventLevel.Verbose, Message = "Response [{0}] content block {1}: {2}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with an array with primitive type elements.")]
		public void ResponseContentBlock(string requestId, int blockNumber, byte[] content)
		{
			WriteEvent(11, requestId, blockNumber, content);
		}

		[Event(15, Level = EventLevel.Verbose, Message = "Response [{0}] content block {1}: {2}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void ResponseContentTextBlock(string requestId, int blockNumber, string content)
		{
			WriteEvent(15, requestId, blockNumber, content);
		}

		[NonEvent]
		public void ErrorResponse(Response response, HttpMessageSanitizer sanitizer, double elapsed)
		{
			if (IsEnabled(EventLevel.Warning, EventKeywords.None))
			{
				ErrorResponse(response.ClientRequestId, response.Status, response.ReasonPhrase, FormatHeaders(response.Headers, sanitizer), elapsed);
			}
		}

		[Event(8, Level = EventLevel.Warning, Message = "Error response [{0}] {1} {2} ({4:00.0}s)\r\n{3}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void ErrorResponse(string requestId, int status, string reasonPhrase, string headers, double seconds)
		{
			WriteEvent(8, requestId, status, reasonPhrase, headers, seconds);
		}

		[NonEvent]
		public void ErrorResponseContent(string requestId, byte[] content, Encoding? textEncoding)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.None))
			{
				if (textEncoding != null)
				{
					ErrorResponseContentText(requestId, textEncoding.GetString(content));
				}
				else
				{
					ErrorResponseContent(requestId, content);
				}
			}
		}

		[Event(9, Level = EventLevel.Informational, Message = "Error response [{0}] content: {1}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with an array with primitive type elements.")]
		public void ErrorResponseContent(string requestId, byte[] content)
		{
			WriteEvent(9, requestId, content);
		}

		[Event(14, Level = EventLevel.Informational, Message = "Error response [{0}] content: {1}")]
		public void ErrorResponseContentText(string requestId, string content)
		{
			WriteEvent(14, requestId, content);
		}

		[NonEvent]
		public void ErrorResponseContentBlock(string requestId, int blockNumber, byte[] content, Encoding? textEncoding)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.None))
			{
				if (textEncoding != null)
				{
					ErrorResponseContentTextBlock(requestId, blockNumber, textEncoding.GetString(content));
				}
				else
				{
					ErrorResponseContentBlock(requestId, blockNumber, content);
				}
			}
		}

		[Event(12, Level = EventLevel.Informational, Message = "Error response [{0}] content block {1}: {2}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with an array with primitive type elements.")]
		public void ErrorResponseContentBlock(string requestId, int blockNumber, byte[] content)
		{
			WriteEvent(12, requestId, blockNumber, content);
		}

		[Event(16, Level = EventLevel.Informational, Message = "Error response [{0}] content block {1}: {2}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void ErrorResponseContentTextBlock(string requestId, int blockNumber, string content)
		{
			WriteEvent(16, requestId, blockNumber, content);
		}

		[Event(10, Level = EventLevel.Informational, Message = "Request [{0}] attempt number {1} took {2:00.0}s")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void RequestRetrying(string requestId, int retryNumber, double seconds)
		{
			WriteEvent(10, requestId, retryNumber, seconds);
		}

		[Event(7, Level = EventLevel.Warning, Message = "Response [{0}] took {1:00.0}s")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void ResponseDelay(string requestId, double seconds)
		{
			WriteEvent(7, requestId, seconds);
		}

		[Event(18, Level = EventLevel.Informational, Message = "Request [{0}] exception {1}")]
		public void ExceptionResponse(string requestId, string exception)
		{
			WriteEvent(18, requestId, exception);
		}

		[NonEvent]
		public void RequestRedirect(Request request, Uri redirectUri, Response response)
		{
			if (IsEnabled(EventLevel.Verbose, EventKeywords.None))
			{
				RequestRedirect(request.ClientRequestId, request.Uri.ToString(), redirectUri.ToString(), response.Status);
			}
		}

		[Event(20, Level = EventLevel.Verbose, Message = "Request [{0}] Redirecting from {1} to {2} in response to status code {3}")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "WriteEvent is used with primitive types.")]
		public void RequestRedirect(string requestId, string from, string to, int status)
		{
			WriteEvent(20, requestId, from, to, status);
		}

		[Event(21, Level = EventLevel.Warning, Message = "Request [{0}] Insecure HTTPS to HTTP redirect from {1} to {2} was blocked.")]
		public void RequestRedirectBlocked(string requestId, string from, string to)
		{
			WriteEvent(21, requestId, from, to);
		}

		[Event(22, Level = EventLevel.Warning, Message = "Request [{0}] Exceeded max number of redirects. Redirect from {1} to {2} blocked.")]
		public void RequestRedirectCountExceeded(string requestId, string from, string to)
		{
			WriteEvent(22, requestId, from, to);
		}

		[NonEvent]
		public void PipelineTransportOptionsNotApplied(Type optionsType)
		{
			if (IsEnabled(EventLevel.Informational, EventKeywords.None))
			{
				PipelineTransportOptionsNotApplied(optionsType.FullName ?? string.Empty);
			}
		}

		[Event(23, Level = EventLevel.Informational, Message = "The client requires transport configuration but it was not applied because custom transport was provided. Type: {0}")]
		public void PipelineTransportOptionsNotApplied(string optionsType)
		{
			WriteEvent(23, optionsType);
		}

		[NonEvent]
		private static string FormatHeaders(IEnumerable<HttpHeader> headers, HttpMessageSanitizer sanitizer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (HttpHeader header in headers)
			{
				stringBuilder.Append(header.Name);
				stringBuilder.Append(':');
				stringBuilder.Append(sanitizer.SanitizeHeader(header.Name, header.Value));
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}
	}
	public class AzureEventSourceListener : EventListener
	{
		public const string TraitName = "AzureEventSource";

		public const string TraitValue = "true";

		private readonly List<EventSource> _eventSources = new List<EventSource>();

		private readonly Action<EventWrittenEventArgs, string> _log;

		private readonly EventLevel _level;

		public AzureEventSourceListener(Action<EventWrittenEventArgs, string> log, EventLevel level)
		{
			_log = log ?? throw new ArgumentNullException("log");
			_level = level;
			foreach (EventSource eventSource in _eventSources)
			{
				OnEventSourceCreated(eventSource);
			}
			_eventSources.Clear();
		}

		protected sealed override void OnEventSourceCreated(EventSource eventSource)
		{
			base.OnEventSourceCreated(eventSource);
			if (_log == null)
			{
				_eventSources.Add(eventSource);
			}
			if (eventSource.GetTrait("AzureEventSource") == "true")
			{
				EnableEvents(eventSource, _level);
			}
		}

		protected sealed override void OnEventWritten(EventWrittenEventArgs eventData)
		{
			if (eventData.EventId != -1)
			{
				_log?.Invoke(eventData, EventSourceEventFormatting.Format(eventData));
			}
		}

		public static AzureEventSourceListener CreateConsoleLogger(EventLevel level = EventLevel.Informational)
		{
			return new AzureEventSourceListener(delegate(EventWrittenEventArgs eventData, string text)
			{
				Console.WriteLine("[{1}] {0}: {2}", eventData.EventSource.Name, eventData.Level, text);
			}, level);
		}

		public static AzureEventSourceListener CreateTraceLogger(EventLevel level = EventLevel.Informational)
		{
			return new AzureEventSourceListener(delegate(EventWrittenEventArgs eventData, string text)
			{
				Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "[{0}] {1}", eventData.Level, text), eventData.EventSource.Name);
			}, level);
		}
	}
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
namespace Azure.Core.Cryptography
{
	public interface IKeyEncryptionKey
	{
		string KeyId { get; }

		byte[] WrapKey(string algorithm, ReadOnlyMemory<byte> key, CancellationToken cancellationToken = default(CancellationToken));

		Task<byte[]> WrapKeyAsync(string algorithm, ReadOnlyMemory<byte> key, CancellationToken cancellationToken = default(CancellationToken));

		byte[] UnwrapKey(string algorithm, ReadOnlyMemory<byte> encryptedKey, CancellationToken cancellationToken = default(CancellationToken));

		Task<byte[]> UnwrapKeyAsync(string algorithm, ReadOnlyMemory<byte> encryptedKey, CancellationToken cancellationToken = default(CancellationToken));
	}
	public interface IKeyEncryptionKeyResolver
	{
		IKeyEncryptionKey Resolve(string keyId, CancellationToken cancellationToken = default(CancellationToken));

		Task<IKeyEncryptionKey> ResolveAsync(string keyId, CancellationToken cancellationToken = default(CancellationToken));
	}
}
