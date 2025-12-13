using System;
using System.Buffers;
using System.ClientModel.Internal;
using System.ClientModel.Primitives;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: InternalsVisibleTo("System.ClientModel.Tests.Internal, PublicKey=0024000004800000940000000602000000240000525341310004000001000100d15ddcb29688295338af4b7686603fe614abd555e09efba8fb88ee09e1f7b1ccaeed2e8f823fa9eef3fdd60217fc012ea67d2479751a0b8c087a4185541b851bd8b16f8d91b840e51b1cb0ba6fe647997e57429265e85ef62d565db50a69ae1647d54d7bd855e4db3d8a91510e5bcbd0edfbbecaa20a7bd9ae74593daa7b11b4")]
[assembly: InternalsVisibleTo("System.ClientModel.Tests.Internal.Perf, PublicKey=0024000004800000940000000602000000240000525341310004000001000100d15ddcb29688295338af4b7686603fe614abd555e09efba8fb88ee09e1f7b1ccaeed2e8f823fa9eef3fdd60217fc012ea67d2479751a0b8c087a4185541b851bd8b16f8d91b840e51b1cb0ba6fe647997e57429265e85ef62d565db50a69ae1647d54d7bd855e4db3d8a91510e5bcbd0edfbbecaa20a7bd9ae74593daa7b11b4")]
[assembly: TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = ".NET Standard 2.0")]
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("© Microsoft Corporation. All rights reserved.")]
[assembly: AssemblyDescription("Contains building blocks for clients that call cloud services.")]
[assembly: AssemblyFileVersion("1.0.24.5302")]
[assembly: AssemblyInformationalVersion("1.0.0+8ffa7d7f26bb2c5e3dadf74b2aa9c9ba9c9d9208")]
[assembly: AssemblyProduct("Azure .NET SDK")]
[assembly: AssemblyTitle("System.ClientModel")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/Azure/azure-sdk-for-net")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: AssemblyVersion("1.0.0.0")]
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
namespace System.ClientModel.Internal
{
	internal class ModelWriter : ModelWriter<object>
	{
		public ModelWriter(IJsonModel<object> model, ModelReaderWriterOptions options)
			: base(model, options)
		{
		}
	}
	internal class ModelWriter<T> : IDisposable
	{
		private sealed class SequenceBuilder : IBufferWriter<byte>, IDisposable
		{
			private struct Buffer
			{
				public byte[] Array;

				public int Written;
			}

			private volatile Buffer[] _buffers;

			private volatile int _count;

			private readonly int _segmentSize;

			private readonly object _lock = new object();

			public SequenceBuilder(int segmentSize = 16384)
			{
				_segmentSize = segmentSize;
				_buffers = Array.Empty<Buffer>();
			}

			public void Advance(int bytesWritten)
			{
				ref Buffer reference = ref _buffers[_count - 1];
				reference.Written += bytesWritten;
				if (reference.Written > reference.Array.Length)
				{
					throw new ArgumentOutOfRangeException("bytesWritten");
				}
			}

			public Memory<byte> GetMemory(int sizeHint = 0)
			{
				if (sizeHint < 256)
				{
					sizeHint = 256;
				}
				int sizeToRent = ((sizeHint > _segmentSize) ? sizeHint : _segmentSize);
				if (_buffers.Length == 0)
				{
					ExpandBuffers(sizeToRent);
				}
				ref Buffer reference = ref _buffers[_count - 1];
				Memory<byte> result = MemoryExtensions.AsMemory(reference.Array, reference.Written);
				if (result.Length >= sizeHint)
				{
					return result;
				}
				ExpandBuffers(sizeToRent);
				return _buffers[_count - 1].Array;
			}

			private void ExpandBuffers(int sizeToRent)
			{
				lock (_lock)
				{
					int num = ((_count == 0) ? 1 : (_count * 2));
					Buffer[] array = new Buffer[num];
					if (_count > 0)
					{
						_buffers.CopyTo(array, 0);
					}
					_buffers = array;
					_buffers[_count].Array = ArrayPool<byte>.Shared.Rent(sizeToRent);
					_count = ((num == 1) ? num : (_count + 1));
				}
			}

			public Span<byte> GetSpan(int sizeHint = 0)
			{
				return GetMemory(sizeHint).Span;
			}

			public void Dispose()
			{
				int count;
				Buffer[] buffers;
				lock (_lock)
				{
					count = _count;
					buffers = _buffers;
					_count = 0;
					_buffers = Array.Empty<Buffer>();
				}
				for (int i = 0; i < count; i++)
				{
					ArrayPool<byte>.Shared.Return(buffers[i].Array);
				}
			}

			public bool TryComputeLength(out long length)
			{
				length = 0L;
				for (int i = 0; i < _count; i++)
				{
					length += _buffers[i].Written;
				}
				return true;
			}

			public void CopyTo(Stream stream, CancellationToken cancellation)
			{
				for (int i = 0; i < _count; i++)
				{
					cancellation.ThrowIfCancellationRequested();
					Buffer buffer = _buffers[i];
					stream.Write(buffer.Array, 0, buffer.Written);
				}
			}

			public async Task CopyToAsync(Stream stream, CancellationToken cancellation)
			{
				for (int i = 0; i < _count; i++)
				{
					cancellation.ThrowIfCancellationRequested();
					Buffer buffer = _buffers[i];
					await stream.WriteAsync(buffer.Array, 0, buffer.Written, cancellation).ConfigureAwait(continueOnCapturedContext: false);
				}
			}
		}

		private readonly IJsonModel<T> _model;

		private readonly ModelReaderWriterOptions _options;

		private readonly object _writeLock = new object();

		private readonly object _readLock = new object();

		private volatile SequenceBuilder? _sequenceBuilder;

		private volatile bool _isDisposed;

		private volatile int _readCount;

		private ManualResetEvent? _readersFinished;

		private ManualResetEvent ReadersFinished => _readersFinished ?? (_readersFinished = new ManualResetEvent(initialState: true));

		public ModelWriter(IJsonModel<T> model, ModelReaderWriterOptions options)
		{
			_model = model;
			_options = options;
		}

		private SequenceBuilder GetSequenceBuilder()
		{
			if (_sequenceBuilder == null)
			{
				lock (_writeLock)
				{
					if (_isDisposed)
					{
						throw new ObjectDisposedException("ModelWriter");
					}
					if (_sequenceBuilder == null)
					{
						SequenceBuilder sequenceBuilder = new SequenceBuilder();
						using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(sequenceBuilder);
						_model.Write(utf8JsonWriter, _options);
						utf8JsonWriter.Flush();
						_sequenceBuilder = sequenceBuilder;
					}
				}
			}
			return _sequenceBuilder;
		}

		internal void CopyTo(Stream stream, CancellationToken cancellation)
		{
			SequenceBuilder sequenceBuilder = GetSequenceBuilder();
			IncrementRead();
			try
			{
				sequenceBuilder.CopyTo(stream, cancellation);
			}
			finally
			{
				DecrementRead();
			}
		}

		internal bool TryComputeLength(out long length)
		{
			SequenceBuilder sequenceBuilder = GetSequenceBuilder();
			IncrementRead();
			try
			{
				return sequenceBuilder.TryComputeLength(out length);
			}
			finally
			{
				DecrementRead();
			}
		}

		internal async Task CopyToAsync(Stream stream, CancellationToken cancellation)
		{
			SequenceBuilder sequenceBuilder = GetSequenceBuilder();
			IncrementRead();
			try
			{
				await sequenceBuilder.CopyToAsync(stream, cancellation).ConfigureAwait(continueOnCapturedContext: false);
			}
			finally
			{
				DecrementRead();
			}
		}

		public BinaryData ToBinaryData()
		{
			SequenceBuilder sequenceBuilder = GetSequenceBuilder();
			IncrementRead();
			try
			{
				sequenceBuilder.TryComputeLength(out var length);
				if (length > int.MaxValue)
				{
					throw new InvalidOperationException($"Length of serialized model is too long.  Value was {length} max is {int.MaxValue}");
				}
				using MemoryStream memoryStream = new MemoryStream((int)length);
				sequenceBuilder.CopyTo(memoryStream, default(CancellationToken));
				return new BinaryData(MemoryExtensions.AsMemory(memoryStream.GetBuffer(), 0, (int)memoryStream.Position));
			}
			finally
			{
				DecrementRead();
			}
		}

		public void Dispose()
		{
			if (_isDisposed)
			{
				return;
			}
			lock (_writeLock)
			{
				if (!_isDisposed)
				{
					_isDisposed = true;
					if (_readersFinished == null || _readersFinished.WaitOne())
					{
						_sequenceBuilder?.Dispose();
					}
					_sequenceBuilder = null;
					_readersFinished?.Dispose();
					_readersFinished = null;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void IncrementRead()
		{
			if (_isDisposed)
			{
				throw new ObjectDisposedException("ModelWriter");
			}
			lock (_readLock)
			{
				_readCount++;
				ReadersFinished.Reset();
			}
			if (_isDisposed)
			{
				DecrementRead();
				throw new ObjectDisposedException("ModelWriter");
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void DecrementRead()
		{
			lock (_readLock)
			{
				_readCount--;
				if (_readCount == 0)
				{
					ReadersFinished.Set();
				}
			}
		}
	}
}
namespace System.ClientModel.Primitives
{
	public interface IJsonModel<out T> : IPersistableModel<T>
	{
		void Write(Utf8JsonWriter writer, ModelReaderWriterOptions options);

		T Create(ref Utf8JsonReader reader, ModelReaderWriterOptions options);
	}
	public interface IPersistableModel<out T>
	{
		BinaryData Write(ModelReaderWriterOptions options);

		T Create(BinaryData data, ModelReaderWriterOptions options);

		string GetFormatFromOptions(ModelReaderWriterOptions options);
	}
	[RequiresUnreferencedCode("The constructors of the type being deserialized are dynamically accessed and may be trimmed.")]
	internal class JsonModelConverter : JsonConverter<IJsonModel<object>>
	{
		public ModelReaderWriterOptions Options { get; }

		public JsonModelConverter()
			: this(ModelReaderWriterOptions.Json)
		{
		}

		public JsonModelConverter(ModelReaderWriterOptions options)
		{
			Options = options;
		}

		public override bool CanConvert(Type typeToConvert)
		{
			return !Attribute.IsDefined(typeToConvert, typeof(JsonConverterAttribute));
		}

		public override IJsonModel<object> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);
			return (IJsonModel<object>)ModelReaderWriter.Read(BinaryData.FromString(jsonDocument.RootElement.GetRawText()), typeToConvert, Options);
		}

		public override void Write(Utf8JsonWriter writer, IJsonModel<object> value, JsonSerializerOptions options)
		{
			value.Write(writer, Options);
		}
	}
	public static class ModelReaderWriter
	{
		public static BinaryData Write<T>(T model, ModelReaderWriterOptions? options = null) where T : IPersistableModel<T>
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (options == null)
			{
				options = ModelReaderWriterOptions.Json;
			}
			if (IsJsonFormatRequested(model, options) && model is IJsonModel<T> model2)
			{
				using ModelWriter<T> modelWriter = new ModelWriter<T>(model2, options);
				return modelWriter.ToBinaryData();
			}
			return model.Write(options);
		}

		public static BinaryData Write(object model, ModelReaderWriterOptions? options = null)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (options == null)
			{
				options = ModelReaderWriterOptions.Json;
			}
			if (!(model is IPersistableModel<object> persistableModel))
			{
				throw new InvalidOperationException(model.GetType().Name + " does not implement IPersistableModel");
			}
			if (IsJsonFormatRequested(persistableModel, options) && model is IJsonModel<object> model2)
			{
				using ModelWriter<object> modelWriter = new ModelWriter<object>(model2, options);
				return modelWriter.ToBinaryData();
			}
			return persistableModel.Write(options);
		}

		public static T? Read<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] T>(BinaryData data, ModelReaderWriterOptions? options = null) where T : IPersistableModel<T>
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (options == null)
			{
				options = ModelReaderWriterOptions.Json;
			}
			return GetInstance<T>().Create(data, options);
		}

		public static object? Read(BinaryData data, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] Type returnType, ModelReaderWriterOptions? options = null)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if ((object)returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			if (options == null)
			{
				options = ModelReaderWriterOptions.Json;
			}
			return GetInstance(returnType).Create(data, options);
		}

		private static IPersistableModel<object> GetInstance([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] Type returnType)
		{
			return (GetObjectInstance(returnType) as IPersistableModel<object>) ?? throw new InvalidOperationException(returnType.Name + " does not implement IPersistableModel");
		}

		private static IPersistableModel<T> GetInstance<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] T>() where T : IPersistableModel<T>
		{
			return (GetObjectInstance(typeof(T)) as IPersistableModel<T>) ?? throw new InvalidOperationException(typeof(T).Name + " does not implement IPersistableModel");
		}

		private static object GetObjectInstance([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] Type returnType)
		{
			PersistableModelProxyAttribute persistableModelProxyAttribute = Attribute.GetCustomAttribute(returnType, typeof(PersistableModelProxyAttribute), inherit: false) as PersistableModelProxyAttribute;
			Type type = ((persistableModelProxyAttribute == null) ? returnType : persistableModelProxyAttribute.ProxyType);
			if (returnType.IsAbstract && persistableModelProxyAttribute == null)
			{
				throw new InvalidOperationException(returnType.Name + " must be decorated with PersistableModelProxyAttribute to be used with ModelReaderWriter");
			}
			return Activator.CreateInstance(type, nonPublic: true) ?? throw new InvalidOperationException("Unable to create instance of " + type.Name + ".");
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsJsonFormatRequested<T>(IPersistableModel<T> model, ModelReaderWriterOptions options)
		{
			if (!(options.Format == "J"))
			{
				if (options.Format == "W")
				{
					return model.GetFormatFromOptions(options) == "J";
				}
				return false;
			}
			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsJsonFormatRequested(IPersistableModel<object> model, ModelReaderWriterOptions options)
		{
			return IsJsonFormatRequested<object>(model, options);
		}
	}
	public class ModelReaderWriterOptions
	{
		private static ModelReaderWriterOptions? s_jsonOptions;

		private static ModelReaderWriterOptions? s_xmlOptions;

		public static ModelReaderWriterOptions Json => s_jsonOptions ?? (s_jsonOptions = new ModelReaderWriterOptions("J"));

		public static ModelReaderWriterOptions Xml => s_xmlOptions ?? (s_xmlOptions = new ModelReaderWriterOptions("X"));

		public string Format { get; }

		public ModelReaderWriterOptions(string format)
		{
			Format = format;
		}
	}
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class PersistableModelProxyAttribute : Attribute
	{
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
		public Type ProxyType { get; }

		public PersistableModelProxyAttribute([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] Type proxyType)
		{
			ProxyType = proxyType;
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
