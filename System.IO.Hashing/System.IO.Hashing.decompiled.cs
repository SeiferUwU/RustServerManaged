using System;
using System.Buffers;
using System.Buffers.Binary;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using FxResources.System.IO.Hashing;
using Microsoft.CodeAnalysis;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = "")]
[assembly: CLSCompliant(true)]
[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
[assembly: AssemblyDefaultAlias("System.IO.Hashing")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: AssemblyMetadata(".NETFrameworkAssembly", "")]
[assembly: AssemblyMetadata("Serviceable", "True")]
[assembly: AssemblyMetadata("PreferInbox", "True")]
[assembly: AssemblyMetadata("IsTrimmable", "True")]
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyCopyright("© Microsoft Corporation. All rights reserved.")]
[assembly: AssemblyDescription("Provides non-cryptographic hash algorithms, such as CRC-32.\r\n\r\nCommonly Used Types:\r\nSystem.IO.Hashing.Crc32\r\nSystem.IO.Hashing.XxHash32")]
[assembly: AssemblyFileVersion("6.0.21.52210")]
[assembly: AssemblyInformationalVersion("6.0.0+4822e3c3aa77eb82b2fb33c9321f923cf11ddde6")]
[assembly: AssemblyProduct("Microsoft® .NET")]
[assembly: AssemblyTitle("System.IO.Hashing")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/dotnet/runtime")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("6.0.0.0")]
[module: UnverifiableCode]
[module: NullablePublicOnly(false)]
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
	internal sealed class NullablePublicOnlyAttribute : Attribute
	{
		public readonly bool IncludesInternals;

		public NullablePublicOnlyAttribute(bool P_0)
		{
			IncludesInternals = P_0;
		}
	}
}
namespace FxResources.System.IO.Hashing
{
	internal static class SR
	{
	}
}
namespace System
{
	internal static class SR
	{
		private static readonly bool s_usingResourceKeys = AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", out var isEnabled) && isEnabled;

		private static ResourceManager s_resourceManager;

		internal static ResourceManager ResourceManager => s_resourceManager ?? (s_resourceManager = new ResourceManager(typeof(SR)));

		internal static string Argument_DestinationTooShort => GetResourceString("Argument_DestinationTooShort");

		internal static string NotSupported_GetHashCode => GetResourceString("NotSupported_GetHashCode");

		private static bool UsingResourceKeys()
		{
			return s_usingResourceKeys;
		}

		internal static string GetResourceString(string resourceKey)
		{
			if (UsingResourceKeys())
			{
				return resourceKey;
			}
			string result = null;
			try
			{
				result = ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			return result;
		}

		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string resourceString = GetResourceString(resourceKey);
			if (!(resourceKey == resourceString) && resourceString != null)
			{
				return resourceString;
			}
			return defaultString;
		}

		internal static string Format(string resourceFormat, object p1)
		{
			if (UsingResourceKeys())
			{
				return string.Join(", ", resourceFormat, p1);
			}
			return string.Format(resourceFormat, p1);
		}

		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (UsingResourceKeys())
			{
				return string.Join(", ", resourceFormat, p1, p2);
			}
			return string.Format(resourceFormat, p1, p2);
		}

		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (UsingResourceKeys())
			{
				return string.Join(", ", resourceFormat, p1, p2, p3);
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args != null)
			{
				if (UsingResourceKeys())
				{
					return resourceFormat + ", " + string.Join(", ", args);
				}
				return string.Format(resourceFormat, args);
			}
			return resourceFormat;
		}

		internal static string Format(IFormatProvider provider, string resourceFormat, object p1)
		{
			if (UsingResourceKeys())
			{
				return string.Join(", ", resourceFormat, p1);
			}
			return string.Format(provider, resourceFormat, p1);
		}

		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2)
		{
			if (UsingResourceKeys())
			{
				return string.Join(", ", resourceFormat, p1, p2);
			}
			return string.Format(provider, resourceFormat, p1, p2);
		}

		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2, object p3)
		{
			if (UsingResourceKeys())
			{
				return string.Join(", ", resourceFormat, p1, p2, p3);
			}
			return string.Format(provider, resourceFormat, p1, p2, p3);
		}

		internal static string Format(IFormatProvider provider, string resourceFormat, params object[] args)
		{
			if (args != null)
			{
				if (UsingResourceKeys())
				{
					return resourceFormat + ", " + string.Join(", ", args);
				}
				return string.Format(provider, resourceFormat, args);
			}
			return resourceFormat;
		}
	}
}
namespace System.Diagnostics.CodeAnalysis
{
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
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullAttribute : Attribute
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
	internal sealed class MemberNotNullWhenAttribute : Attribute
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
}
namespace System.Numerics
{
	internal static class BitOperations
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint RotateLeft(uint value, int offset)
		{
			return (value << offset) | (value >> 32 - offset);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong RotateLeft(ulong value, int offset)
		{
			return (value << offset) | (value >> 64 - offset);
		}
	}
}
namespace System.IO.Hashing
{
	public sealed class Crc32 : NonCryptographicHashAlgorithm
	{
		private const uint InitialState = uint.MaxValue;

		private const int Size = 4;

		private uint _crc = uint.MaxValue;

		private static readonly uint[] s_crcLookup = GenerateReflectedTable(3988292384u);

		public Crc32()
			: base(4)
		{
		}

		public override void Append(ReadOnlySpan<byte> source)
		{
			_crc = Update(_crc, source);
		}

		public override void Reset()
		{
			_crc = uint.MaxValue;
		}

		protected override void GetCurrentHashCore(Span<byte> destination)
		{
			BinaryPrimitives.WriteUInt32LittleEndian(destination, ~_crc);
		}

		protected override void GetHashAndResetCore(Span<byte> destination)
		{
			BinaryPrimitives.WriteUInt32LittleEndian(destination, ~_crc);
			_crc = uint.MaxValue;
		}

		public static byte[] Hash(byte[] source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Hash(new ReadOnlySpan<byte>(source));
		}

		public static byte[] Hash(ReadOnlySpan<byte> source)
		{
			byte[] array = new byte[4];
			StaticHash(source, array);
			return array;
		}

		public static bool TryHash(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
		{
			if (destination.Length < 4)
			{
				bytesWritten = 0;
				return false;
			}
			bytesWritten = StaticHash(source, destination);
			return true;
		}

		public static int Hash(ReadOnlySpan<byte> source, Span<byte> destination)
		{
			if (destination.Length < 4)
			{
				throw new ArgumentException(System.SR.Argument_DestinationTooShort, "destination");
			}
			return StaticHash(source, destination);
		}

		private static int StaticHash(ReadOnlySpan<byte> source, Span<byte> destination)
		{
			uint crc = uint.MaxValue;
			crc = Update(crc, source);
			BinaryPrimitives.WriteUInt32LittleEndian(destination, ~crc);
			return 4;
		}

		private static uint Update(uint crc, ReadOnlySpan<byte> source)
		{
			for (int i = 0; i < source.Length; i++)
			{
				byte b = (byte)crc;
				b ^= source[i];
				crc = s_crcLookup[b] ^ (crc >> 8);
			}
			return crc;
		}

		private static uint[] GenerateReflectedTable(uint reflectedPolynomial)
		{
			uint[] array = new uint[256];
			for (int i = 0; i < 256; i++)
			{
				uint num = (uint)i;
				for (int j = 0; j < 8; j++)
				{
					num = (((num & 1) != 0) ? ((num >> 1) ^ reflectedPolynomial) : (num >> 1));
				}
				array[i] = num;
			}
			return array;
		}
	}
	public sealed class Crc64 : NonCryptographicHashAlgorithm
	{
		private const ulong InitialState = 0uL;

		private const int Size = 8;

		private ulong _crc;

		private static readonly ulong[] s_crcLookup = GenerateTable(4823603603198064275uL);

		public Crc64()
			: base(8)
		{
		}

		public override void Append(ReadOnlySpan<byte> source)
		{
			_crc = Update(_crc, source);
		}

		public override void Reset()
		{
			_crc = 0uL;
		}

		protected override void GetCurrentHashCore(Span<byte> destination)
		{
			BinaryPrimitives.WriteUInt64BigEndian(destination, _crc);
		}

		protected override void GetHashAndResetCore(Span<byte> destination)
		{
			BinaryPrimitives.WriteUInt64BigEndian(destination, _crc);
			_crc = 0uL;
		}

		public static byte[] Hash(byte[] source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Hash(new ReadOnlySpan<byte>(source));
		}

		public static byte[] Hash(ReadOnlySpan<byte> source)
		{
			byte[] array = new byte[8];
			StaticHash(source, array);
			return array;
		}

		public static bool TryHash(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
		{
			if (destination.Length < 8)
			{
				bytesWritten = 0;
				return false;
			}
			bytesWritten = StaticHash(source, destination);
			return true;
		}

		public static int Hash(ReadOnlySpan<byte> source, Span<byte> destination)
		{
			if (destination.Length < 8)
			{
				throw new ArgumentException(System.SR.Argument_DestinationTooShort, "destination");
			}
			return StaticHash(source, destination);
		}

		private static int StaticHash(ReadOnlySpan<byte> source, Span<byte> destination)
		{
			ulong crc = 0uL;
			crc = Update(crc, source);
			BinaryPrimitives.WriteUInt64BigEndian(destination, crc);
			return 8;
		}

		private static ulong Update(ulong crc, ReadOnlySpan<byte> source)
		{
			for (int i = 0; i < source.Length; i++)
			{
				ulong num = crc >> 56;
				num ^= source[i];
				crc = s_crcLookup[num] ^ (crc << 8);
			}
			return crc;
		}

		private static ulong[] GenerateTable(ulong polynomial)
		{
			ulong[] array = new ulong[256];
			for (int i = 0; i < 256; i++)
			{
				ulong num = (ulong)((long)i << 56);
				for (int j = 0; j < 8; j++)
				{
					num = (((num & 0x8000000000000000uL) != 0L) ? ((num << 1) ^ polynomial) : (num << 1));
				}
				array[i] = num;
			}
			return array;
		}
	}
	public sealed class XxHash32 : NonCryptographicHashAlgorithm
	{
		private struct State
		{
			private const uint Prime32_1 = 2654435761u;

			private const uint Prime32_2 = 2246822519u;

			private const uint Prime32_3 = 3266489917u;

			private const uint Prime32_4 = 668265263u;

			private const uint Prime32_5 = 374761393u;

			private uint _acc1;

			private uint _acc2;

			private uint _acc3;

			private uint _acc4;

			private readonly uint _smallAcc;

			private bool _hadFullStripe;

			internal State(uint seed)
			{
				_acc1 = seed + 606290984;
				_acc2 = seed + 2246822519u;
				_acc3 = seed;
				_acc4 = seed - 2654435761u;
				_smallAcc = seed + 374761393;
				_hadFullStripe = false;
			}

			internal void ProcessStripe(ReadOnlySpan<byte> source)
			{
				source = source.Slice(0, 16);
				_acc1 = ApplyRound(_acc1, source);
				_acc2 = ApplyRound(_acc2, source.Slice(4));
				_acc3 = ApplyRound(_acc3, source.Slice(8));
				_acc4 = ApplyRound(_acc4, source.Slice(12));
				_hadFullStripe = true;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private readonly uint Converge()
			{
				return BitOperations.RotateLeft(_acc1, 1) + BitOperations.RotateLeft(_acc2, 7) + BitOperations.RotateLeft(_acc3, 12) + BitOperations.RotateLeft(_acc4, 18);
			}

			private static uint ApplyRound(uint acc, ReadOnlySpan<byte> lane)
			{
				acc += (uint)((int)BinaryPrimitives.ReadUInt32LittleEndian(lane) * -2048144777);
				acc = BitOperations.RotateLeft(acc, 13);
				acc *= 2654435761u;
				return acc;
			}

			internal readonly uint Complete(int length, ReadOnlySpan<byte> remaining)
			{
				uint num = (_hadFullStripe ? Converge() : _smallAcc);
				num += (uint)length;
				while (remaining.Length >= 4)
				{
					uint num2 = BinaryPrimitives.ReadUInt32LittleEndian(remaining);
					num += (uint)((int)num2 * -1028477379);
					num = BitOperations.RotateLeft(num, 17);
					num *= 668265263;
					remaining = remaining.Slice(4);
				}
				for (int i = 0; i < remaining.Length; i++)
				{
					uint num3 = remaining[i];
					num += num3 * 374761393;
					num = BitOperations.RotateLeft(num, 11);
					num *= 2654435761u;
				}
				num ^= num >> 15;
				num *= 2246822519u;
				num ^= num >> 13;
				num *= 3266489917u;
				return num ^ (num >> 16);
			}
		}

		private const int HashSize = 4;

		private const int StripeSize = 16;

		private readonly uint _seed;

		private State _state;

		private byte[] _holdback;

		private int _length;

		public XxHash32()
			: this(0)
		{
		}

		public XxHash32(int seed)
			: base(4)
		{
			_seed = (uint)seed;
			Reset();
		}

		public override void Reset()
		{
			_state = new State(_seed);
			_length = 0;
		}

		public override void Append(ReadOnlySpan<byte> source)
		{
			int num = _length & 0xF;
			if (num != 0)
			{
				int num2 = 16 - num;
				if (source.Length <= num2)
				{
					source.CopyTo(MemoryExtensions.AsSpan(_holdback, num));
					_length += source.Length;
					return;
				}
				source.Slice(0, num2).CopyTo(MemoryExtensions.AsSpan(_holdback, num));
				_state.ProcessStripe(_holdback);
				source = source.Slice(num2);
				_length += num2;
			}
			while (source.Length >= 16)
			{
				_state.ProcessStripe(source);
				source = source.Slice(16);
				_length += 16;
			}
			if (source.Length > 0)
			{
				if (_holdback == null)
				{
					_holdback = new byte[16];
				}
				source.CopyTo(_holdback);
				_length += source.Length;
			}
		}

		protected override void GetCurrentHashCore(Span<byte> destination)
		{
			int num = _length & 0xF;
			ReadOnlySpan<byte> remaining = ReadOnlySpan<byte>.Empty;
			if (num > 0)
			{
				remaining = new ReadOnlySpan<byte>(_holdback, 0, num);
			}
			uint value = _state.Complete(_length, remaining);
			BinaryPrimitives.WriteUInt32BigEndian(destination, value);
		}

		public static byte[] Hash(byte[] source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Hash(new ReadOnlySpan<byte>(source));
		}

		public static byte[] Hash(byte[] source, int seed)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Hash(new ReadOnlySpan<byte>(source), seed);
		}

		public static byte[] Hash(ReadOnlySpan<byte> source, int seed = 0)
		{
			byte[] array = new byte[4];
			StaticHash(source, array, seed);
			return array;
		}

		public static bool TryHash(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten, int seed = 0)
		{
			if (destination.Length < 4)
			{
				bytesWritten = 0;
				return false;
			}
			bytesWritten = StaticHash(source, destination, seed);
			return true;
		}

		public static int Hash(ReadOnlySpan<byte> source, Span<byte> destination, int seed = 0)
		{
			if (destination.Length < 4)
			{
				throw new ArgumentException(System.SR.Argument_DestinationTooShort, "destination");
			}
			return StaticHash(source, destination, seed);
		}

		private static int StaticHash(ReadOnlySpan<byte> source, Span<byte> destination, int seed)
		{
			int length = source.Length;
			State state = new State((uint)seed);
			while (source.Length > 16)
			{
				state.ProcessStripe(source);
				source = source.Slice(16);
			}
			uint value = state.Complete(length, source);
			BinaryPrimitives.WriteUInt32BigEndian(destination, value);
			return 4;
		}
	}
	public sealed class XxHash64 : NonCryptographicHashAlgorithm
	{
		private struct State
		{
			private const ulong Prime64_1 = 11400714785074694791uL;

			private const ulong Prime64_2 = 14029467366897019727uL;

			private const ulong Prime64_3 = 1609587929392839161uL;

			private const ulong Prime64_4 = 9650029242287828579uL;

			private const ulong Prime64_5 = 2870177450012600261uL;

			private ulong _acc1;

			private ulong _acc2;

			private ulong _acc3;

			private ulong _acc4;

			private readonly ulong _smallAcc;

			private bool _hadFullStripe;

			internal State(ulong seed)
			{
				_acc1 = seed + 6983438078262162902L;
				_acc2 = seed + 14029467366897019727uL;
				_acc3 = seed;
				_acc4 = seed - 11400714785074694791uL;
				_smallAcc = seed + 2870177450012600261L;
				_hadFullStripe = false;
			}

			internal void ProcessStripe(ReadOnlySpan<byte> source)
			{
				source = source.Slice(0, 32);
				_acc1 = ApplyRound(_acc1, source);
				_acc2 = ApplyRound(_acc2, source.Slice(8));
				_acc3 = ApplyRound(_acc3, source.Slice(16));
				_acc4 = ApplyRound(_acc4, source.Slice(24));
				_hadFullStripe = true;
			}

			private static ulong MergeAccumulator(ulong acc, ulong accN)
			{
				acc ^= ApplyRound(0uL, accN);
				acc *= 11400714785074694791uL;
				acc += 9650029242287828579uL;
				return acc;
			}

			private readonly ulong Converge()
			{
				ulong acc = BitOperations.RotateLeft(_acc1, 1) + BitOperations.RotateLeft(_acc2, 7) + BitOperations.RotateLeft(_acc3, 12) + BitOperations.RotateLeft(_acc4, 18);
				acc = MergeAccumulator(acc, _acc1);
				acc = MergeAccumulator(acc, _acc2);
				acc = MergeAccumulator(acc, _acc3);
				return MergeAccumulator(acc, _acc4);
			}

			private static ulong ApplyRound(ulong acc, ReadOnlySpan<byte> lane)
			{
				return ApplyRound(acc, BinaryPrimitives.ReadUInt64LittleEndian(lane));
			}

			private static ulong ApplyRound(ulong acc, ulong lane)
			{
				acc += (ulong)((long)lane * -4417276706812531889L);
				acc = BitOperations.RotateLeft(acc, 31);
				acc *= 11400714785074694791uL;
				return acc;
			}

			internal readonly ulong Complete(int length, ReadOnlySpan<byte> remaining)
			{
				ulong num = (_hadFullStripe ? Converge() : _smallAcc);
				num += (ulong)length;
				while (remaining.Length >= 8)
				{
					ulong lane = BinaryPrimitives.ReadUInt64LittleEndian(remaining);
					num ^= ApplyRound(0uL, lane);
					num = BitOperations.RotateLeft(num, 27);
					num *= 11400714785074694791uL;
					num += 9650029242287828579uL;
					remaining = remaining.Slice(8);
				}
				if (remaining.Length >= 4)
				{
					ulong num2 = BinaryPrimitives.ReadUInt32LittleEndian(remaining);
					num ^= (ulong)((long)num2 * -7046029288634856825L);
					num = BitOperations.RotateLeft(num, 23);
					num *= 14029467366897019727uL;
					num += 1609587929392839161L;
					remaining = remaining.Slice(4);
				}
				for (int i = 0; i < remaining.Length; i++)
				{
					ulong num3 = remaining[i];
					num ^= num3 * 2870177450012600261L;
					num = BitOperations.RotateLeft(num, 11);
					num *= 11400714785074694791uL;
				}
				num ^= num >> 33;
				num *= 14029467366897019727uL;
				num ^= num >> 29;
				num *= 1609587929392839161L;
				return num ^ (num >> 32);
			}
		}

		private const int HashSize = 8;

		private const int StripeSize = 32;

		private readonly ulong _seed;

		private State _state;

		private byte[] _holdback;

		private int _length;

		public XxHash64()
			: this(0L)
		{
		}

		public XxHash64(long seed)
			: base(8)
		{
			_seed = (ulong)seed;
			Reset();
		}

		public override void Reset()
		{
			_state = new State(_seed);
			_length = 0;
		}

		public override void Append(ReadOnlySpan<byte> source)
		{
			int num = _length & 0x1F;
			if (num != 0)
			{
				int num2 = 32 - num;
				if (source.Length <= num2)
				{
					source.CopyTo(MemoryExtensions.AsSpan(_holdback, num));
					_length += source.Length;
					return;
				}
				source.Slice(0, num2).CopyTo(MemoryExtensions.AsSpan(_holdback, num));
				_state.ProcessStripe(_holdback);
				source = source.Slice(num2);
				_length += num2;
			}
			while (source.Length >= 32)
			{
				_state.ProcessStripe(source);
				source = source.Slice(32);
				_length += 32;
			}
			if (source.Length > 0)
			{
				if (_holdback == null)
				{
					_holdback = new byte[32];
				}
				source.CopyTo(_holdback);
				_length += source.Length;
			}
		}

		protected override void GetCurrentHashCore(Span<byte> destination)
		{
			int num = _length & 0x1F;
			ReadOnlySpan<byte> remaining = ReadOnlySpan<byte>.Empty;
			if (num > 0)
			{
				remaining = new ReadOnlySpan<byte>(_holdback, 0, num);
			}
			ulong value = _state.Complete(_length, remaining);
			BinaryPrimitives.WriteUInt64BigEndian(destination, value);
		}

		public static byte[] Hash(byte[] source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Hash(new ReadOnlySpan<byte>(source), 0L);
		}

		public static byte[] Hash(byte[] source, long seed)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Hash(new ReadOnlySpan<byte>(source), seed);
		}

		public static byte[] Hash(ReadOnlySpan<byte> source, long seed = 0L)
		{
			byte[] array = new byte[8];
			StaticHash(source, array, seed);
			return array;
		}

		public static bool TryHash(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten, long seed = 0L)
		{
			if (destination.Length < 8)
			{
				bytesWritten = 0;
				return false;
			}
			bytesWritten = StaticHash(source, destination, seed);
			return true;
		}

		public static int Hash(ReadOnlySpan<byte> source, Span<byte> destination, long seed = 0L)
		{
			if (destination.Length < 8)
			{
				throw new ArgumentException(System.SR.Argument_DestinationTooShort, "destination");
			}
			return StaticHash(source, destination, seed);
		}

		private static int StaticHash(ReadOnlySpan<byte> source, Span<byte> destination, long seed)
		{
			int length = source.Length;
			State state = new State((ulong)seed);
			while (source.Length > 32)
			{
				state.ProcessStripe(source);
				source = source.Slice(32);
			}
			ulong value = state.Complete(length, source);
			BinaryPrimitives.WriteUInt64BigEndian(destination, value);
			return 8;
		}
	}
	public abstract class NonCryptographicHashAlgorithm
	{
		public int HashLengthInBytes { get; }

		protected NonCryptographicHashAlgorithm(int hashLengthInBytes)
		{
			if (hashLengthInBytes < 1)
			{
				throw new ArgumentOutOfRangeException("hashLengthInBytes");
			}
			HashLengthInBytes = hashLengthInBytes;
		}

		public abstract void Append(ReadOnlySpan<byte> source);

		public abstract void Reset();

		protected abstract void GetCurrentHashCore(Span<byte> destination);

		public void Append(byte[] source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			Append(new ReadOnlySpan<byte>(source));
		}

		public void Append(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(4096);
			while (true)
			{
				int num = stream.Read(array, 0, array.Length);
				if (num == 0)
				{
					break;
				}
				Append(new ReadOnlySpan<byte>(array, 0, num));
			}
			ArrayPool<byte>.Shared.Return(array);
		}

		public Task AppendAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return AppendAsyncCore(stream, cancellationToken);
		}

		private async Task AppendAsyncCore(Stream stream, CancellationToken cancellationToken)
		{
			byte[] buffer = ArrayPool<byte>.Shared.Rent(4096);
			while (true)
			{
				int num = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				if (num == 0)
				{
					break;
				}
				Append(new ReadOnlySpan<byte>(buffer, 0, num));
			}
			ArrayPool<byte>.Shared.Return(buffer);
		}

		public byte[] GetCurrentHash()
		{
			byte[] array = new byte[HashLengthInBytes];
			GetCurrentHashCore(array);
			return array;
		}

		public bool TryGetCurrentHash(Span<byte> destination, out int bytesWritten)
		{
			if (destination.Length < HashLengthInBytes)
			{
				bytesWritten = 0;
				return false;
			}
			GetCurrentHashCore(destination.Slice(0, HashLengthInBytes));
			bytesWritten = HashLengthInBytes;
			return true;
		}

		public int GetCurrentHash(Span<byte> destination)
		{
			if (destination.Length < HashLengthInBytes)
			{
				throw new ArgumentException(System.SR.Argument_DestinationTooShort, "destination");
			}
			GetCurrentHashCore(destination.Slice(0, HashLengthInBytes));
			return HashLengthInBytes;
		}

		public byte[] GetHashAndReset()
		{
			byte[] array = new byte[HashLengthInBytes];
			GetHashAndResetCore(array);
			return array;
		}

		public bool TryGetHashAndReset(Span<byte> destination, out int bytesWritten)
		{
			if (destination.Length < HashLengthInBytes)
			{
				bytesWritten = 0;
				return false;
			}
			GetHashAndResetCore(destination.Slice(0, HashLengthInBytes));
			bytesWritten = HashLengthInBytes;
			return true;
		}

		public int GetHashAndReset(Span<byte> destination)
		{
			if (destination.Length < HashLengthInBytes)
			{
				throw new ArgumentException(System.SR.Argument_DestinationTooShort, "destination");
			}
			GetHashAndResetCore(destination.Slice(0, HashLengthInBytes));
			return HashLengthInBytes;
		}

		protected virtual void GetHashAndResetCore(Span<byte> destination)
		{
			GetCurrentHashCore(destination);
			Reset();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use GetCurrentHash() to retrieve the computed hash code.", true)]
		public override int GetHashCode()
		{
			throw new NotSupportedException(System.SR.NotSupported_GetHashCode);
		}
	}
}
