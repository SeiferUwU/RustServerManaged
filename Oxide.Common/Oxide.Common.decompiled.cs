using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using Microsoft.CodeAnalysis;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: TargetFramework(".NETFramework,Version=v4.8", FrameworkDisplayName = ".NET Framework 4.8")]
[assembly: AssemblyCompany("Oxide Team and Contributors")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("(c) 2013-2024 Oxide Team and Contributors")]
[assembly: AssemblyDescription("Abstraction layer for the Oxide modding framework")]
[assembly: AssemblyFileVersion("2.0.27.0")]
[assembly: AssemblyInformationalVersion("2.0.27+d6a34349353fe4707dc83a745e6aeeb3f05c9209")]
[assembly: AssemblyProduct("Oxide.Common")]
[assembly: AssemblyTitle("Oxide.Common")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/OxideMod/Oxide.Abstractions")]
[assembly: AssemblyVersion("2.0.27.0")]
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
namespace System
{
	[ExcludeFromCodeCoverage]
	public readonly struct Index : IEquatable<Index>
	{
		private static class ThrowHelper
		{
			[DoesNotReturn]
			public static void ThrowValueArgumentOutOfRange_NeedNonNegNumException()
			{
				throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
			}
		}

		private readonly int _value;

		public static Index Start => new Index(0);

		public static Index End => new Index(-1);

		public int Value
		{
			get
			{
				if (_value < 0)
				{
					return ~_value;
				}
				return _value;
			}
		}

		public bool IsFromEnd => _value < 0;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Index(int value, bool fromEnd = false)
		{
			if (value < 0)
			{
				ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
			}
			if (fromEnd)
			{
				_value = ~value;
			}
			else
			{
				_value = value;
			}
		}

		private Index(int value)
		{
			_value = value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Index FromStart(int value)
		{
			if (value < 0)
			{
				ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
			}
			return new Index(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Index FromEnd(int value)
		{
			if (value < 0)
			{
				ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
			}
			return new Index(~value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int GetOffset(int length)
		{
			int num = _value;
			if (IsFromEnd)
			{
				num += length + 1;
			}
			return num;
		}

		public override bool Equals([NotNullWhen(true)] object? value)
		{
			if (value is Index)
			{
				return _value == ((Index)value)._value;
			}
			return false;
		}

		public bool Equals(Index other)
		{
			return _value == other._value;
		}

		public override int GetHashCode()
		{
			return _value;
		}

		public static implicit operator Index(int value)
		{
			return FromStart(value);
		}

		public override string ToString()
		{
			if (IsFromEnd)
			{
				return ToStringFromEnd();
			}
			return ((uint)Value).ToString();
		}

		private string ToStringFromEnd()
		{
			return "^" + Value;
		}
	}
	[ExcludeFromCodeCoverage]
	public readonly struct Range : IEquatable<Range>
	{
		private static class HashHelpers
		{
			public static int Combine(int h1, int h2)
			{
				return (((h1 << 5) | (h1 >>> 27)) + h1) ^ h2;
			}
		}

		private static class ThrowHelper
		{
			[DoesNotReturn]
			public static void ThrowArgumentOutOfRangeException()
			{
				throw new ArgumentOutOfRangeException("length");
			}
		}

		public Index Start { get; }

		public Index End { get; }

		public static Range All => Index.Start..Index.End;

		public Range(Index start, Index end)
		{
			Start = start;
			End = end;
		}

		public override bool Equals([NotNullWhen(true)] object? value)
		{
			if (value is Range { Start: var start } range && start.Equals(Start))
			{
				return range.End.Equals(End);
			}
			return false;
		}

		public bool Equals(Range other)
		{
			if (other.Start.Equals(Start))
			{
				return other.End.Equals(End);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashHelpers.Combine(Start.GetHashCode(), End.GetHashCode());
		}

		public override string ToString()
		{
			return Start.ToString() + ".." + End;
		}

		public static Range StartAt(Index start)
		{
			return start..Index.End;
		}

		public static Range EndAt(Index end)
		{
			return Index.Start..end;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public (int Offset, int Length) GetOffsetAndLength(int length)
		{
			Index start = Start;
			int num = ((!start.IsFromEnd) ? start.Value : (length - start.Value));
			Index end = End;
			int num2 = ((!end.IsFromEnd) ? end.Value : (length - end.Value));
			if ((uint)num2 > (uint)length || (uint)num > (uint)num2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return (Offset: num, Length: num2 - num);
		}
	}
}
namespace System.Runtime.InteropServices
{
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class SuppressGCTransitionAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class UnmanagedCallersOnlyAttribute : Attribute
	{
		public Type[]? CallConvs;

		public string? EntryPoint;
	}
}
namespace System.Runtime.Versioning
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class RequiresPreviewFeaturesAttribute : Attribute
	{
		public string? Message { get; }

		public string? Url { get; set; }

		public RequiresPreviewFeaturesAttribute()
		{
		}

		public RequiresPreviewFeaturesAttribute(string? message)
		{
			Message = message;
		}
	}
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class ObsoletedOSPlatformAttribute : Attribute
	{
		public string? Message { get; }

		public string? Url { get; set; }

		public ObsoletedOSPlatformAttribute(string platformName)
		{
		}

		public ObsoletedOSPlatformAttribute(string platformName, string? message)
		{
			Message = message;
		}
	}
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class SupportedOSPlatformAttribute : Attribute
	{
		public SupportedOSPlatformAttribute(string platformName)
		{
		}
	}
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class SupportedOSPlatformGuardAttribute : Attribute
	{
		public SupportedOSPlatformGuardAttribute(string platformName)
		{
		}
	}
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class TargetPlatformAttribute : Attribute
	{
		public TargetPlatformAttribute(string platformName)
		{
		}
	}
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class UnsupportedOSPlatformAttribute : Attribute
	{
		public string? Message { get; }

		public UnsupportedOSPlatformAttribute(string platformName)
		{
		}

		public UnsupportedOSPlatformAttribute(string platformName, string? message)
		{
			Message = message;
		}
	}
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class UnsupportedOSPlatformGuardAttribute : Attribute
	{
		public UnsupportedOSPlatformGuardAttribute(string platformName)
		{
		}
	}
}
namespace System.Runtime.CompilerServices
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	[ExcludeFromCodeCoverage]
	public sealed class AsyncMethodBuilderAttribute : Attribute
	{
		public Type BuilderType { get; }

		public AsyncMethodBuilderAttribute(Type builderType)
		{
			BuilderType = builderType;
		}
	}
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class CallerArgumentExpressionAttribute : Attribute
	{
		public string ParameterName { get; }

		public CallerArgumentExpressionAttribute(string parameterName)
		{
			ParameterName = parameterName;
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class CompilerFeatureRequiredAttribute : Attribute
	{
		public const string RefStructs = "RefStructs";

		public const string RequiredMembers = "RequiredMembers";

		public string FeatureName { get; }

		public bool IsOptional { get; set; }

		public CompilerFeatureRequiredAttribute(string featureName)
		{
			FeatureName = featureName;
		}
	}
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class InterpolatedStringHandlerArgumentAttribute : Attribute
	{
		public string[] Arguments { get; }

		public InterpolatedStringHandlerArgumentAttribute(string argument)
		{
			Arguments = new string[1] { argument };
		}

		public InterpolatedStringHandlerArgumentAttribute(params string[] arguments)
		{
			Arguments = arguments;
		}
	}
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class InterpolatedStringHandlerAttribute : Attribute
	{
	}
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ExcludeFromCodeCoverage]
	public static class IsExternalInit
	{
	}
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class ModuleInitializerAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class RequiredMemberAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Interface, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class SkipLocalsInitAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class DisableRuntimeMarshallingAttribute : Attribute
	{
	}
}
namespace System.Diagnostics
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class StackTraceHiddenAttribute : Attribute
	{
	}
}
namespace System.Diagnostics.CodeAnalysis
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class AllowNullAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class DisallowNullAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class DoesNotReturnAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class DoesNotReturnIfAttribute : Attribute
	{
		public bool ParameterValue { get; }

		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			ParameterValue = parameterValue;
		}
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class MaybeNullAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class MaybeNullWhenAttribute : Attribute
	{
		public bool ReturnValue { get; }

		public MaybeNullWhenAttribute(bool returnValue)
		{
			ReturnValue = returnValue;
		}
	}
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	[ExcludeFromCodeCoverage]
	public sealed class MemberNotNullAttribute : Attribute
	{
		public string[] Members { get; }

		public MemberNotNullAttribute(string member)
		{
			Members = new string[1] { member };
		}

		public MemberNotNullAttribute(params string[] members)
		{
			Members = members;
		}
	}
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	[ExcludeFromCodeCoverage]
	public sealed class MemberNotNullWhenAttribute : Attribute
	{
		public bool ReturnValue { get; }

		public string[] Members { get; }

		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			ReturnValue = returnValue;
			Members = new string[1] { member };
		}

		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			ReturnValue = returnValue;
			Members = members;
		}
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class NotNullAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class NotNullIfNotNullAttribute : Attribute
	{
		public string ParameterName { get; }

		public NotNullIfNotNullAttribute(string parameterName)
		{
			ParameterName = parameterName;
		}
	}
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class NotNullWhenAttribute : Attribute
	{
		public bool ReturnValue { get; }

		public NotNullWhenAttribute(bool returnValue)
		{
			ReturnValue = returnValue;
		}
	}
	[AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class SetsRequiredMembersAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class StringSyntaxAttribute : Attribute
	{
		public const string CompositeFormat = "CompositeFormat";

		public const string DateOnlyFormat = "DateOnlyFormat";

		public const string DateTimeFormat = "DateTimeFormat";

		public const string EnumFormat = "EnumFormat";

		public const string GuidFormat = "GuidFormat";

		public const string Json = "Json";

		public const string NumericFormat = "NumericFormat";

		public const string Regex = "Regex";

		public const string TimeOnlyFormat = "TimeOnlyFormat";

		public const string TimeSpanFormat = "TimeSpanFormat";

		public const string Uri = "Uri";

		public const string Xml = "Xml";

		public string Syntax { get; }

		public object?[] Arguments { get; }

		public StringSyntaxAttribute(string syntax)
		{
			Syntax = syntax;
			Arguments = new object[0];
		}

		public StringSyntaxAttribute(string syntax, params object?[] arguments)
		{
			Syntax = syntax;
			Arguments = arguments;
		}
	}
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	[ExcludeFromCodeCoverage]
	public sealed class UnscopedRefAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class DynamicallyAccessedMembersAttribute : Attribute
	{
		public DynamicallyAccessedMemberTypes MemberTypes { get; }

		public DynamicallyAccessedMembersAttribute(DynamicallyAccessedMemberTypes memberTypes)
		{
			MemberTypes = memberTypes;
		}
	}
	[Flags]
	public enum DynamicallyAccessedMemberTypes
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
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class DynamicDependencyAttribute : Attribute
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
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event, Inherited = false, AllowMultiple = false)]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class RequiresAssemblyFilesAttribute : Attribute
	{
		public string? Message { get; }

		public string? Url { get; set; }

		public RequiresAssemblyFilesAttribute()
		{
		}

		public RequiresAssemblyFilesAttribute(string message)
		{
			Message = message;
		}
	}
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class RequiresDynamicCodeAttribute : Attribute
	{
		public string Message { get; }

		public string? Url { get; set; }

		public RequiresDynamicCodeAttribute(string message)
		{
			Message = message;
		}
	}
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class RequiresUnreferencedCodeAttribute : Attribute
	{
		public string Message { get; }

		public string? Url { get; set; }

		public RequiresUnreferencedCodeAttribute(string message)
		{
			Message = message;
		}
	}
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	[ExcludeFromCodeCoverage]
	[Conditional("MULTI_TARGETING_SUPPORT_ATTRIBUTES")]
	public sealed class UnconditionalSuppressMessageAttribute : Attribute
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
}
namespace Oxide
{
	public static class EnvironmentHelper
	{
		public const string SECTION_DELIMITER = ":";

		public const string OXIDE_PREFIX = "OXIDE_";

		public static string GetVariable(string key)
		{
			return Environment.GetEnvironmentVariable(NormalizeKey(key));
		}

		public static void SetVariable(string key, string value, bool throwOnExisting = false, bool force = false)
		{
			key = NormalizeKey(key);
			string text = ((!force) ? Environment.GetEnvironmentVariable(key) : null);
			if (text != null)
			{
				if (throwOnExisting)
				{
					throw new InvalidOperationException("'" + key + "' has existing value of '" + text + "' to override set 'force' to 'true'");
				}
			}
			else
			{
				Environment.SetEnvironmentVariable(key, value);
			}
		}

		private static string NormalizeKey(string key)
		{
			key = key.Trim();
			if (string.IsNullOrEmpty(key))
			{
				return null;
			}
			key = (key.StartsWith("OXIDE_", StringComparison.InvariantCultureIgnoreCase) ? ("OXIDE_" + key.Substring(6)) : ("OXIDE_" + key));
			return key.Replace(":", "__").ToUpperInvariant();
		}
	}
	public static class ExtensionMethods
	{
		public static AssemblyMetadataAttribute[] Metadata(this Assembly assembly)
		{
			return Attribute.GetCustomAttributes(assembly, typeof(AssemblyMetadataAttribute), inherit: false) as AssemblyMetadataAttribute[];
		}

		public static string[] Metadata(this Assembly assembly, string key)
		{
			return (from meta in assembly.Metadata()
				where meta.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)
				select meta.Value).ToArray();
		}

		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
		{
			HashSet<T> hashSet = new HashSet<T>();
			foreach (T item in collection)
			{
				hashSet.Add(item);
			}
			return hashSet;
		}

		public static bool TryPop<T>(this Stack<T> stack, out T value)
		{
			if (stack.Count > 0)
			{
				value = stack.Pop();
				return true;
			}
			value = default(T);
			return false;
		}

		public static bool TryPeek<T>(this Stack<T> stack, out T value)
		{
			if (stack.Count > 0)
			{
				value = stack.Peek();
				return true;
			}
			value = default(T);
			return false;
		}

		public static bool TryDequeue<T>(this Queue<T> queue, out T value)
		{
			if (queue.Count > 0)
			{
				value = queue.Dequeue();
				return true;
			}
			value = default(T);
			return false;
		}

		public static bool TryPeek<T>(this Queue<T> queue, out T value)
		{
			if (queue.Count > 0)
			{
				value = queue.Peek();
				return true;
			}
			value = default(T);
			return false;
		}
	}
}
namespace Oxide.Pooling
{
	public interface IArrayPoolProvider<out T> : IPoolProvider<T[]>, IPoolProvider
	{
		T[] Take(int length);
	}
	public interface IPoolFactory
	{
		IPoolProvider<T> GetProvider<T>();

		bool IsHandledType<T>();

		IDisposable RegisterProvider<TProvider>(out TProvider provider, params object[] args) where TProvider : IPoolProvider;
	}
	public interface IPoolProvider
	{
		void Return(object item);
	}
	public interface IPoolProvider<out T> : IPoolProvider
	{
		T Take();
	}
	public static class PoolingExtensions
	{
		public static IArrayPoolProvider<T> GetArrayProvider<T>(this IPoolFactory factory)
		{
			return factory.GetProvider<T[]>() as IArrayPoolProvider<T>;
		}

		public static T[] PooledCopy<T>(this IArrayPoolProvider<T> provider, T[] source, int offset, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			T[] array = provider?.Take(count) ?? throw new ArgumentNullException("provider");
			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = source[offset];
					offset++;
				}
				return array;
			}
			catch
			{
				provider.Return(array);
				throw;
			}
		}

		public static T[] PooledCopy<T>(this IPoolFactory factory, T[] source, int offset, int count)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			return (factory.GetProvider<T[]>() as IArrayPoolProvider<T>).PooledCopy(source, offset, count);
		}
	}
}
