using System;
using System.Buffers.Binary;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using FxResources.System.Diagnostics.DiagnosticSource;
using Microsoft.CodeAnalysis;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = "")]
[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
[assembly: AssemblyDefaultAlias("System.Diagnostics.DiagnosticSource")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: AssemblyMetadata(".NETFrameworkAssembly", "")]
[assembly: AssemblyMetadata("Serviceable", "True")]
[assembly: AssemblyMetadata("PreferInbox", "True")]
[assembly: AssemblyMetadata("IsTrimmable", "True")]
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyCopyright("© Microsoft Corporation. All rights reserved.")]
[assembly: AssemblyDescription("Provides Classes that allow you to decouple code logging rich (unserializable) diagnostics/telemetry (e.g. framework) from code that consumes it (e.g. tools)\r\n\r\nCommonly Used Types:\r\nSystem.Diagnostics.DiagnosticListener\r\nSystem.Diagnostics.DiagnosticSource")]
[assembly: AssemblyFileVersion("6.0.1523.11507")]
[assembly: AssemblyInformationalVersion("6.0.15+5edef4b20babd4c3ddac7460e536f86fd0f2d724")]
[assembly: AssemblyProduct("Microsoft® .NET")]
[assembly: AssemblyTitle("System.Diagnostics.DiagnosticSource")]
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
namespace FxResources.System.Diagnostics.DiagnosticSource
{
	internal static class SR
	{
	}
}
namespace System
{
	internal static class HexConverter
	{
		public enum Casing : uint
		{
			Upper = 0u,
			Lower = 8224u
		}

		public static ReadOnlySpan<byte> CharToHexLookup => new byte[256]
		{
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 0, 1,
			2, 3, 4, 5, 6, 7, 8, 9, 255, 255,
			255, 255, 255, 255, 255, 10, 11, 12, 13, 14,
			15, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 10, 11, 12,
			13, 14, 15, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
			255, 255, 255, 255, 255, 255
		};

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ToBytesBuffer(byte value, Span<byte> buffer, int startingIndex = 0, Casing casing = Casing.Upper)
		{
			uint num = (uint)(((value & 0xF0) << 4) + (value & 0xF) - 35209);
			uint num2 = ((((0 - num) & 0x7070) >> 4) + num + 47545) | (uint)casing;
			buffer[startingIndex + 1] = (byte)num2;
			buffer[startingIndex] = (byte)(num2 >> 8);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ToCharsBuffer(byte value, Span<char> buffer, int startingIndex = 0, Casing casing = Casing.Upper)
		{
			uint num = (uint)(((value & 0xF0) << 4) + (value & 0xF) - 35209);
			uint num2 = ((((0 - num) & 0x7070) >> 4) + num + 47545) | (uint)casing;
			buffer[startingIndex + 1] = (char)(num2 & 0xFF);
			buffer[startingIndex] = (char)(num2 >> 8);
		}

		public static void EncodeToUtf16(ReadOnlySpan<byte> bytes, Span<char> chars, Casing casing = Casing.Upper)
		{
			for (int i = 0; i < bytes.Length; i++)
			{
				ToCharsBuffer(bytes[i], chars, i * 2, casing);
			}
		}

		public static string ToString(ReadOnlySpan<byte> bytes, Casing casing = Casing.Upper)
		{
			Span<char> span = default(Span<char>);
			if (bytes.Length > 16)
			{
				char[] array = new char[bytes.Length * 2];
				span = MemoryExtensions.AsSpan(array);
			}
			else
			{
				span = stackalloc char[bytes.Length * 2];
			}
			int num = 0;
			ReadOnlySpan<byte> readOnlySpan = bytes;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				byte value = readOnlySpan[i];
				ToCharsBuffer(value, span, num, casing);
				num += 2;
			}
			return span.ToString();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char ToCharUpper(int value)
		{
			value &= 0xF;
			value += 48;
			if (value > 57)
			{
				value += 7;
			}
			return (char)value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char ToCharLower(int value)
		{
			value &= 0xF;
			value += 48;
			if (value > 57)
			{
				value += 39;
			}
			return (char)value;
		}

		public static bool TryDecodeFromUtf16(ReadOnlySpan<char> chars, Span<byte> bytes)
		{
			int charsProcessed;
			return TryDecodeFromUtf16(chars, bytes, out charsProcessed);
		}

		public static bool TryDecodeFromUtf16(ReadOnlySpan<char> chars, Span<byte> bytes, out int charsProcessed)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			while (num2 < bytes.Length)
			{
				num3 = FromChar(chars[num + 1]);
				num4 = FromChar(chars[num]);
				if ((num3 | num4) == 255)
				{
					break;
				}
				bytes[num2++] = (byte)((num4 << 4) | num3);
				num += 2;
			}
			if (num3 == 255)
			{
				num++;
			}
			charsProcessed = num;
			return (num3 | num4) != 255;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int FromChar(int c)
		{
			if (c < CharToHexLookup.Length)
			{
				return CharToHexLookup[c];
			}
			return 255;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int FromUpperChar(int c)
		{
			if (c <= 71)
			{
				return CharToHexLookup[c];
			}
			return 255;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int FromLowerChar(int c)
		{
			switch (c)
			{
			case 48:
			case 49:
			case 50:
			case 51:
			case 52:
			case 53:
			case 54:
			case 55:
			case 56:
			case 57:
				return c - 48;
			case 97:
			case 98:
			case 99:
			case 100:
			case 101:
			case 102:
				return c - 97 + 10;
			default:
				return 255;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsHexChar(int c)
		{
			if (IntPtr.Size == 8)
			{
				ulong num = (uint)(c - 48);
				ulong num2 = (ulong)(-17875860044349952L << (int)num);
				ulong num3 = num - 64;
				if ((long)(num2 & num3) >= 0L)
				{
					return false;
				}
				return true;
			}
			return FromChar(c) != 255;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsHexUpperChar(int c)
		{
			if ((uint)(c - 48) > 9u)
			{
				return (uint)(c - 65) <= 5u;
			}
			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsHexLowerChar(int c)
		{
			if ((uint)(c - 48) > 9u)
			{
				return (uint)(c - 97) <= 5u;
			}
			return true;
		}
	}
	internal static class SR
	{
		private static readonly bool s_usingResourceKeys = AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", out var isEnabled) && isEnabled;

		private static ResourceManager s_resourceManager;

		internal static ResourceManager ResourceManager => s_resourceManager ?? (s_resourceManager = new ResourceManager(typeof(SR)));

		internal static string ActivityIdFormatInvalid => GetResourceString("ActivityIdFormatInvalid");

		internal static string ActivityNotRunning => GetResourceString("ActivityNotRunning");

		internal static string ActivityNotStarted => GetResourceString("ActivityNotStarted");

		internal static string ActivityStartAlreadyStarted => GetResourceString("ActivityStartAlreadyStarted");

		internal static string EndTimeNotUtc => GetResourceString("EndTimeNotUtc");

		internal static string OperationNameInvalid => GetResourceString("OperationNameInvalid");

		internal static string ParentIdAlreadySet => GetResourceString("ParentIdAlreadySet");

		internal static string ParentIdInvalid => GetResourceString("ParentIdInvalid");

		internal static string SetFormatOnStartedActivity => GetResourceString("SetFormatOnStartedActivity");

		internal static string SetLinkInvalid => GetResourceString("SetLinkInvalid");

		internal static string SetParentIdOnActivityWithParent => GetResourceString("SetParentIdOnActivityWithParent");

		internal static string StartTimeNotUtc => GetResourceString("StartTimeNotUtc");

		internal static string KeyAlreadyExist => GetResourceString("KeyAlreadyExist");

		internal static string InvalidTraceParent => GetResourceString("InvalidTraceParent");

		internal static string UnableAccessServicePointTable => GetResourceString("UnableAccessServicePointTable");

		internal static string UnableToInitialize => GetResourceString("UnableToInitialize");

		internal static string UnsupportedType => GetResourceString("UnsupportedType");

		internal static string Arg_BufferTooSmall => GetResourceString("Arg_BufferTooSmall");

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
namespace System.Runtime.Versioning
{
	internal abstract class OSPlatformAttribute : Attribute
	{
		public string PlatformName { get; }

		private protected OSPlatformAttribute(string platformName)
		{
			PlatformName = platformName;
		}
	}
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	internal sealed class TargetPlatformAttribute : OSPlatformAttribute
	{
		public TargetPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class SupportedOSPlatformAttribute : OSPlatformAttribute
	{
		public SupportedOSPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class UnsupportedOSPlatformAttribute : OSPlatformAttribute
	{
		public UnsupportedOSPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class SupportedOSPlatformGuardAttribute : OSPlatformAttribute
	{
		public SupportedOSPlatformGuardAttribute(string platformName)
			: base(platformName)
		{
		}
	}
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class UnsupportedOSPlatformGuardAttribute : OSPlatformAttribute
	{
		public UnsupportedOSPlatformGuardAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
namespace System.Diagnostics
{
	public abstract class DiagnosticSource
	{
		internal const string WriteRequiresUnreferencedCode = "The type of object being written to DiagnosticSource cannot be discovered statically.";

		[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
		public abstract void Write(string name, object? value);

		public abstract bool IsEnabled(string name);

		public virtual bool IsEnabled(string name, object? arg1, object? arg2 = null)
		{
			return IsEnabled(name);
		}

		[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
		public Activity StartActivity(Activity activity, object? args)
		{
			activity.Start();
			Write(activity.OperationName + ".Start", args);
			return activity;
		}

		[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
		public void StopActivity(Activity activity, object? args)
		{
			if (activity.Duration == TimeSpan.Zero)
			{
				activity.SetEndTime(Activity.GetUtcNow());
			}
			Write(activity.OperationName + ".Stop", args);
			activity.Stop();
		}

		public virtual void OnActivityImport(Activity activity, object? payload)
		{
		}

		public virtual void OnActivityExport(Activity activity, object? payload)
		{
		}
	}
	public class DiagnosticListener : DiagnosticSource, IObservable<KeyValuePair<string, object?>>, IDisposable
	{
		private sealed class DiagnosticSubscription : IDisposable
		{
			internal IObserver<KeyValuePair<string, object>> Observer;

			internal Predicate<string> IsEnabled1Arg;

			internal Func<string, object, object, bool> IsEnabled3Arg;

			internal Action<Activity, object> OnActivityImport;

			internal Action<Activity, object> OnActivityExport;

			internal DiagnosticListener Owner;

			internal DiagnosticSubscription Next;

			public void Dispose()
			{
				DiagnosticSubscription subscriptions;
				DiagnosticSubscription value;
				do
				{
					subscriptions = Owner._subscriptions;
					value = Remove(subscriptions, this);
				}
				while (Interlocked.CompareExchange(ref Owner._subscriptions, value, subscriptions) != subscriptions);
			}

			private static DiagnosticSubscription Remove(DiagnosticSubscription subscriptions, DiagnosticSubscription subscription)
			{
				if (subscriptions == null)
				{
					return null;
				}
				if (subscriptions.Observer == subscription.Observer && subscriptions.IsEnabled1Arg == subscription.IsEnabled1Arg && subscriptions.IsEnabled3Arg == subscription.IsEnabled3Arg)
				{
					return subscriptions.Next;
				}
				return new DiagnosticSubscription
				{
					Observer = subscriptions.Observer,
					Owner = subscriptions.Owner,
					IsEnabled1Arg = subscriptions.IsEnabled1Arg,
					IsEnabled3Arg = subscriptions.IsEnabled3Arg,
					Next = Remove(subscriptions.Next, subscription)
				};
			}
		}

		private sealed class AllListenerObservable : IObservable<DiagnosticListener>
		{
			internal sealed class AllListenerSubscription : IDisposable
			{
				private readonly AllListenerObservable _owner;

				internal readonly IObserver<DiagnosticListener> Subscriber;

				internal AllListenerSubscription Next;

				internal AllListenerSubscription(AllListenerObservable owner, IObserver<DiagnosticListener> subscriber, AllListenerSubscription next)
				{
					_owner = owner;
					Subscriber = subscriber;
					Next = next;
				}

				public void Dispose()
				{
					if (_owner.Remove(this))
					{
						Subscriber.OnCompleted();
					}
				}
			}

			private AllListenerSubscription _subscriptions;

			public IDisposable Subscribe(IObserver<DiagnosticListener> observer)
			{
				lock (s_allListenersLock)
				{
					for (DiagnosticListener diagnosticListener = s_allListeners; diagnosticListener != null; diagnosticListener = diagnosticListener._next)
					{
						observer.OnNext(diagnosticListener);
					}
					_subscriptions = new AllListenerSubscription(this, observer, _subscriptions);
					return _subscriptions;
				}
			}

			internal void OnNewDiagnosticListener(DiagnosticListener diagnosticListener)
			{
				for (AllListenerSubscription allListenerSubscription = _subscriptions; allListenerSubscription != null; allListenerSubscription = allListenerSubscription.Next)
				{
					allListenerSubscription.Subscriber.OnNext(diagnosticListener);
				}
			}

			private bool Remove(AllListenerSubscription subscription)
			{
				lock (s_allListenersLock)
				{
					if (_subscriptions == subscription)
					{
						_subscriptions = subscription.Next;
						return true;
					}
					if (_subscriptions != null)
					{
						AllListenerSubscription allListenerSubscription = _subscriptions;
						while (allListenerSubscription.Next != null)
						{
							if (allListenerSubscription.Next == subscription)
							{
								allListenerSubscription.Next = allListenerSubscription.Next.Next;
								return true;
							}
							allListenerSubscription = allListenerSubscription.Next;
						}
					}
					return false;
				}
			}
		}

		private volatile DiagnosticSubscription _subscriptions;

		private DiagnosticListener _next;

		private bool _disposed;

		private static DiagnosticListener s_allListeners;

		private static volatile AllListenerObservable s_allListenerObservable;

		private static readonly object s_allListenersLock = new object();

		public static IObservable<DiagnosticListener> AllListeners => s_allListenerObservable ?? Interlocked.CompareExchange(ref s_allListenerObservable, new AllListenerObservable(), null) ?? s_allListenerObservable;

		public string Name { get; private set; }

		public virtual IDisposable Subscribe(IObserver<KeyValuePair<string, object?>> observer, Predicate<string>? isEnabled)
		{
			if (isEnabled == null)
			{
				return SubscribeInternal(observer, null, null, null, null);
			}
			Predicate<string> localIsEnabled = isEnabled;
			return SubscribeInternal(observer, isEnabled, (string name, object arg1, object arg2) => localIsEnabled(name), null, null);
		}

		public virtual IDisposable Subscribe(IObserver<KeyValuePair<string, object?>> observer, Func<string, object?, object?, bool>? isEnabled)
		{
			if (isEnabled != null)
			{
				return SubscribeInternal(observer, (string name) => IsEnabled(name, null), isEnabled, null, null);
			}
			return SubscribeInternal(observer, null, null, null, null);
		}

		public virtual IDisposable Subscribe(IObserver<KeyValuePair<string, object?>> observer)
		{
			return SubscribeInternal(observer, null, null, null, null);
		}

		public DiagnosticListener(string name)
		{
			Name = name;
			lock (s_allListenersLock)
			{
				s_allListenerObservable?.OnNewDiagnosticListener(this);
				_next = s_allListeners;
				s_allListeners = this;
			}
			GC.KeepAlive(DiagnosticSourceEventSource.Log);
		}

		public virtual void Dispose()
		{
			lock (s_allListenersLock)
			{
				if (_disposed)
				{
					return;
				}
				_disposed = true;
				if (s_allListeners == this)
				{
					s_allListeners = s_allListeners._next;
				}
				else
				{
					for (DiagnosticListener next = s_allListeners; next != null; next = next._next)
					{
						if (next._next == this)
						{
							next._next = _next;
							break;
						}
					}
				}
				_next = null;
			}
			DiagnosticSubscription location = null;
			Interlocked.Exchange(ref location, _subscriptions);
			while (location != null)
			{
				location.Observer.OnCompleted();
				location = location.Next;
			}
		}

		public override string ToString()
		{
			return Name ?? string.Empty;
		}

		public bool IsEnabled()
		{
			return _subscriptions != null;
		}

		public override bool IsEnabled(string name)
		{
			for (DiagnosticSubscription diagnosticSubscription = _subscriptions; diagnosticSubscription != null; diagnosticSubscription = diagnosticSubscription.Next)
			{
				if (diagnosticSubscription.IsEnabled1Arg == null || diagnosticSubscription.IsEnabled1Arg(name))
				{
					return true;
				}
			}
			return false;
		}

		public override bool IsEnabled(string name, object? arg1, object? arg2 = null)
		{
			for (DiagnosticSubscription diagnosticSubscription = _subscriptions; diagnosticSubscription != null; diagnosticSubscription = diagnosticSubscription.Next)
			{
				if (diagnosticSubscription.IsEnabled3Arg == null || diagnosticSubscription.IsEnabled3Arg(name, arg1, arg2))
				{
					return true;
				}
			}
			return false;
		}

		[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
		public override void Write(string name, object? value)
		{
			for (DiagnosticSubscription diagnosticSubscription = _subscriptions; diagnosticSubscription != null; diagnosticSubscription = diagnosticSubscription.Next)
			{
				diagnosticSubscription.Observer.OnNext(new KeyValuePair<string, object>(name, value));
			}
		}

		private IDisposable SubscribeInternal(IObserver<KeyValuePair<string, object>> observer, Predicate<string> isEnabled1Arg, Func<string, object, object, bool> isEnabled3Arg, Action<Activity, object> onActivityImport, Action<Activity, object> onActivityExport)
		{
			if (_disposed)
			{
				return new DiagnosticSubscription
				{
					Owner = this
				};
			}
			DiagnosticSubscription diagnosticSubscription = new DiagnosticSubscription
			{
				Observer = observer,
				IsEnabled1Arg = isEnabled1Arg,
				IsEnabled3Arg = isEnabled3Arg,
				OnActivityImport = onActivityImport,
				OnActivityExport = onActivityExport,
				Owner = this,
				Next = _subscriptions
			};
			while (Interlocked.CompareExchange(ref _subscriptions, diagnosticSubscription, diagnosticSubscription.Next) != diagnosticSubscription.Next)
			{
				diagnosticSubscription.Next = _subscriptions;
			}
			return diagnosticSubscription;
		}

		public override void OnActivityImport(Activity activity, object? payload)
		{
			for (DiagnosticSubscription diagnosticSubscription = _subscriptions; diagnosticSubscription != null; diagnosticSubscription = diagnosticSubscription.Next)
			{
				diagnosticSubscription.OnActivityImport?.Invoke(activity, payload);
			}
		}

		public override void OnActivityExport(Activity activity, object? payload)
		{
			for (DiagnosticSubscription diagnosticSubscription = _subscriptions; diagnosticSubscription != null; diagnosticSubscription = diagnosticSubscription.Next)
			{
				diagnosticSubscription.OnActivityExport?.Invoke(activity, payload);
			}
		}

		public virtual IDisposable Subscribe(IObserver<KeyValuePair<string, object?>> observer, Func<string, object?, object?, bool>? isEnabled, Action<Activity, object?>? onActivityImport = null, Action<Activity, object?>? onActivityExport = null)
		{
			if (isEnabled != null)
			{
				return SubscribeInternal(observer, (string name) => IsEnabled(name, null), isEnabled, onActivityImport, onActivityExport);
			}
			return SubscribeInternal(observer, null, null, onActivityImport, onActivityExport);
		}
	}
	[EventSource(Name = "Microsoft-Diagnostics-DiagnosticSource")]
	[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2113:ReflectionToRequiresUnreferencedCode", Justification = "In EventSource, EnsureDescriptorsInitialized's use of GetType preserves methods on Delegate and MulticastDelegate because the nested type OverrideEventProvider's base type EventProvider defines a delegate. This includes Delegate and MulticastDelegate methods which require unreferenced code, but EnsureDescriptorsInitialized does not access these members and is safe to call.")]
	[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2115:ReflectionToDynamicallyAccessedMembers", Justification = "In EventSource, EnsureDescriptorsInitialized's use of GetType preserves methods on Delegate and MulticastDelegate because the nested type OverrideEventProvider's base type EventProvider defines a delegate. This includes Delegate and MulticastDelegate methods which have dynamically accessed members requirements, but EnsureDescriptorsInitialized does not access these members and is safe to call.")]
	internal sealed class DiagnosticSourceEventSource : EventSource
	{
		public static class Keywords
		{
			public const EventKeywords Messages = (EventKeywords)1L;

			public const EventKeywords Events = (EventKeywords)2L;

			public const EventKeywords IgnoreShortCutKeywords = (EventKeywords)2048L;

			public const EventKeywords AspNetCoreHosting = (EventKeywords)4096L;

			public const EventKeywords EntityFrameworkCoreCommands = (EventKeywords)8192L;
		}

		[Flags]
		internal enum ActivityEvents
		{
			None = 0,
			ActivityStart = 1,
			ActivityStop = 2,
			All = 3
		}

		internal sealed class FilterAndTransform
		{
			public FilterAndTransform Next;

			internal const string c_ActivitySourcePrefix = "[AS]";

			private IDisposable _diagnosticsListenersSubscription;

			private Subscriptions _liveSubscriptions;

			private readonly bool _noImplicitTransforms;

			private ImplicitTransformEntry _firstImplicitTransformsEntry;

			private ConcurrentDictionary<Type, TransformSpec> _implicitTransformsTable;

			private readonly TransformSpec _explicitTransforms;

			private readonly DiagnosticSourceEventSource _eventSource;

			internal string SourceName { get; set; }

			internal string ActivityName { get; set; }

			internal ActivityEvents Events { get; set; }

			internal ActivitySamplingResult SamplingResult { get; set; }

			public static void CreateFilterAndTransformList(ref FilterAndTransform specList, string filterAndPayloadSpecs, DiagnosticSourceEventSource eventSource)
			{
				DestroyFilterAndTransformList(ref specList, eventSource);
				if (filterAndPayloadSpecs == null)
				{
					filterAndPayloadSpecs = "";
				}
				int num = filterAndPayloadSpecs.Length;
				while (true)
				{
					if (0 < num && char.IsWhiteSpace(filterAndPayloadSpecs[num - 1]))
					{
						num--;
						continue;
					}
					int num2 = filterAndPayloadSpecs.LastIndexOf('\n', num - 1, num);
					int i = 0;
					if (0 <= num2)
					{
						i = num2 + 1;
					}
					for (; i < num && char.IsWhiteSpace(filterAndPayloadSpecs[i]); i++)
					{
					}
					if (IsActivitySourceEntry(filterAndPayloadSpecs, i, num))
					{
						AddNewActivitySourceTransform(filterAndPayloadSpecs, i, num, eventSource);
					}
					else
					{
						specList = new FilterAndTransform(filterAndPayloadSpecs, i, num, eventSource, specList);
					}
					num = num2;
					if (num < 0)
					{
						break;
					}
				}
				if (eventSource._activitySourceSpecs != null)
				{
					NormalizeActivitySourceSpecsList(eventSource);
					CreateActivityListener(eventSource);
				}
			}

			public static void DestroyFilterAndTransformList(ref FilterAndTransform specList, DiagnosticSourceEventSource eventSource)
			{
				eventSource._activityListener?.Dispose();
				eventSource._activityListener = null;
				eventSource._activitySourceSpecs = null;
				FilterAndTransform filterAndTransform = specList;
				specList = null;
				while (filterAndTransform != null)
				{
					filterAndTransform.Dispose();
					filterAndTransform = filterAndTransform.Next;
				}
			}

			public FilterAndTransform(string filterAndPayloadSpec, int startIdx, int endIdx, DiagnosticSourceEventSource eventSource, FilterAndTransform next)
			{
				FilterAndTransform filterAndTransform = this;
				Next = next;
				_eventSource = eventSource;
				string listenerNameFilter = null;
				string eventNameFilter = null;
				string text = null;
				int num = startIdx;
				int num2 = endIdx;
				int num3 = filterAndPayloadSpec.IndexOf(':', startIdx, endIdx - startIdx);
				if (0 <= num3)
				{
					num2 = num3;
					num = num3 + 1;
				}
				int num4 = filterAndPayloadSpec.IndexOf('/', startIdx, num2 - startIdx);
				if (0 <= num4)
				{
					listenerNameFilter = filterAndPayloadSpec.Substring(startIdx, num4 - startIdx);
					int num5 = filterAndPayloadSpec.IndexOf('@', num4 + 1, num2 - num4 - 1);
					if (0 <= num5)
					{
						text = filterAndPayloadSpec.Substring(num5 + 1, num2 - num5 - 1);
						eventNameFilter = filterAndPayloadSpec.Substring(num4 + 1, num5 - num4 - 1);
					}
					else
					{
						eventNameFilter = filterAndPayloadSpec.Substring(num4 + 1, num2 - num4 - 1);
					}
				}
				else if (startIdx < num2)
				{
					listenerNameFilter = filterAndPayloadSpec.Substring(startIdx, num2 - startIdx);
				}
				_eventSource.Message("DiagnosticSource: Enabling '" + (listenerNameFilter ?? "*") + "/" + (eventNameFilter ?? "*") + "'");
				if (num < endIdx && filterAndPayloadSpec[num] == '-')
				{
					_eventSource.Message("DiagnosticSource: suppressing implicit transforms.");
					_noImplicitTransforms = true;
					num++;
				}
				if (num < endIdx)
				{
					while (true)
					{
						int num6 = num;
						int num7 = filterAndPayloadSpec.LastIndexOf(';', endIdx - 1, endIdx - num);
						if (0 <= num7)
						{
							num6 = num7 + 1;
						}
						if (num6 < endIdx)
						{
							if (_eventSource.IsEnabled(EventLevel.Informational, (EventKeywords)1L))
							{
								_eventSource.Message("DiagnosticSource: Parsing Explicit Transform '" + filterAndPayloadSpec.Substring(num6, endIdx - num6) + "'");
							}
							_explicitTransforms = new TransformSpec(filterAndPayloadSpec, num6, endIdx, _explicitTransforms);
						}
						if (num == num6)
						{
							break;
						}
						endIdx = num7;
					}
				}
				Action<string, string, IEnumerable<KeyValuePair<string, string>>> writeEvent = null;
				if (text != null && text.Contains("Activity"))
				{
					writeEvent = text switch
					{
						"Activity1Start" => _eventSource.Activity1Start, 
						"Activity1Stop" => _eventSource.Activity1Stop, 
						"Activity2Start" => _eventSource.Activity2Start, 
						"Activity2Stop" => _eventSource.Activity2Stop, 
						"RecursiveActivity1Start" => _eventSource.RecursiveActivity1Start, 
						"RecursiveActivity1Stop" => _eventSource.RecursiveActivity1Stop, 
						_ => null, 
					};
					if (writeEvent == null)
					{
						_eventSource.Message("DiagnosticSource: Could not find Event to log Activity " + text);
					}
				}
				if (writeEvent == null)
				{
					writeEvent = _eventSource.Event;
				}
				_diagnosticsListenersSubscription = DiagnosticListener.AllListeners.Subscribe(new CallbackObserver<DiagnosticListener>(delegate(DiagnosticListener newListener)
				{
					if (listenerNameFilter == null || listenerNameFilter == newListener.Name)
					{
						filterAndTransform._eventSource.NewDiagnosticListener(newListener.Name);
						Predicate<string> isEnabled = null;
						if (eventNameFilter != null)
						{
							isEnabled = (string eventName) => eventNameFilter == eventName;
						}
						IDisposable subscription = newListener.Subscribe(new CallbackObserver<KeyValuePair<string, object>>(OnEventWritten), isEnabled);
						filterAndTransform._liveSubscriptions = new Subscriptions(subscription, filterAndTransform._liveSubscriptions);
					}
					[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "DiagnosticSource.Write is marked with RequiresUnreferencedCode.")]
					void OnEventWritten(KeyValuePair<string, object> evnt)
					{
						if (eventNameFilter == null || !(eventNameFilter != evnt.Key))
						{
							List<KeyValuePair<string, string>> arg = filterAndTransform.Morph(evnt.Value);
							string key = evnt.Key;
							writeEvent(newListener.Name, key, arg);
						}
					}
				}));
			}

			internal FilterAndTransform(string filterAndPayloadSpec, int endIdx, int colonIdx, string activitySourceName, string activityName, ActivityEvents events, ActivitySamplingResult samplingResult, DiagnosticSourceEventSource eventSource)
			{
				_eventSource = eventSource;
				Next = _eventSource._activitySourceSpecs;
				_eventSource._activitySourceSpecs = this;
				SourceName = activitySourceName;
				ActivityName = activityName;
				Events = events;
				SamplingResult = samplingResult;
				if (colonIdx < 0)
				{
					return;
				}
				int num = colonIdx + 1;
				if (num < endIdx && filterAndPayloadSpec[num] == '-')
				{
					_eventSource.Message("DiagnosticSource: suppressing implicit transforms.");
					_noImplicitTransforms = true;
					num++;
				}
				if (num >= endIdx)
				{
					return;
				}
				while (true)
				{
					int num2 = num;
					int num3 = filterAndPayloadSpec.LastIndexOf(';', endIdx - 1, endIdx - num);
					if (0 <= num3)
					{
						num2 = num3 + 1;
					}
					if (num2 < endIdx)
					{
						if (_eventSource.IsEnabled(EventLevel.Informational, (EventKeywords)1L))
						{
							_eventSource.Message("DiagnosticSource: Parsing Explicit Transform '" + filterAndPayloadSpec.Substring(num2, endIdx - num2) + "'");
						}
						_explicitTransforms = new TransformSpec(filterAndPayloadSpec, num2, endIdx, _explicitTransforms);
					}
					if (num != num2)
					{
						endIdx = num3;
						continue;
					}
					break;
				}
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal static bool IsActivitySourceEntry(string filterAndPayloadSpec, int startIdx, int endIdx)
			{
				return MemoryExtensions.AsSpan(filterAndPayloadSpec, startIdx, endIdx - startIdx).StartsWith(MemoryExtensions.AsSpan("[AS]"), StringComparison.Ordinal);
			}

			internal static void AddNewActivitySourceTransform(string filterAndPayloadSpec, int startIdx, int endIdx, DiagnosticSourceEventSource eventSource)
			{
				ActivityEvents events = ActivityEvents.All;
				ActivitySamplingResult samplingResult = ActivitySamplingResult.AllDataAndRecorded;
				int num = filterAndPayloadSpec.IndexOf(':', startIdx + "[AS]".Length, endIdx - startIdx - "[AS]".Length);
				ReadOnlySpan<char> readOnlySpan = MemoryExtensions.AsSpan(filterAndPayloadSpec, startIdx + "[AS]".Length, ((num >= 0) ? num : endIdx) - startIdx - "[AS]".Length).Trim();
				int num2 = readOnlySpan.IndexOf('/');
				ReadOnlySpan<char> span;
				if (num2 >= 0)
				{
					span = readOnlySpan.Slice(0, num2).Trim();
					ReadOnlySpan<char> readOnlySpan2 = readOnlySpan.Slice(num2 + 1, readOnlySpan.Length - num2 - 1).Trim();
					int num3 = readOnlySpan2.IndexOf('-');
					ReadOnlySpan<char> span2;
					if (num3 >= 0)
					{
						span2 = readOnlySpan2.Slice(0, num3).Trim();
						readOnlySpan2 = readOnlySpan2.Slice(num3 + 1, readOnlySpan2.Length - num3 - 1).Trim();
						if (readOnlySpan2.Length > 0)
						{
							if (MemoryExtensions.Equals(readOnlySpan2, MemoryExtensions.AsSpan("Propagate"), StringComparison.OrdinalIgnoreCase))
							{
								samplingResult = ActivitySamplingResult.PropagationData;
							}
							else
							{
								if (!MemoryExtensions.Equals(readOnlySpan2, MemoryExtensions.AsSpan("Record"), StringComparison.OrdinalIgnoreCase))
								{
									return;
								}
								samplingResult = ActivitySamplingResult.AllData;
							}
						}
					}
					else
					{
						span2 = readOnlySpan2;
					}
					if (span2.Length > 0)
					{
						if (MemoryExtensions.Equals(span2, MemoryExtensions.AsSpan("Start"), StringComparison.OrdinalIgnoreCase))
						{
							events = ActivityEvents.ActivityStart;
						}
						else
						{
							if (!MemoryExtensions.Equals(span2, MemoryExtensions.AsSpan("Stop"), StringComparison.OrdinalIgnoreCase))
							{
								return;
							}
							events = ActivityEvents.ActivityStop;
						}
					}
				}
				else
				{
					span = readOnlySpan;
				}
				string activityName = null;
				int num4 = span.IndexOf('+');
				if (num4 >= 0)
				{
					activityName = span.Slice(num4 + 1).Trim().ToString();
					span = span.Slice(0, num4).Trim();
				}
				FilterAndTransform filterAndTransform = new FilterAndTransform(filterAndPayloadSpec, endIdx, num, span.ToString(), activityName, events, samplingResult, eventSource);
			}

			private static ActivitySamplingResult Sample(string activitySourceName, string activityName, DiagnosticSourceEventSource eventSource)
			{
				FilterAndTransform filterAndTransform = eventSource._activitySourceSpecs;
				ActivitySamplingResult activitySamplingResult = ActivitySamplingResult.None;
				ActivitySamplingResult activitySamplingResult2 = ActivitySamplingResult.None;
				while (filterAndTransform != null)
				{
					if (filterAndTransform.ActivityName == null || filterAndTransform.ActivityName == activityName)
					{
						if (activitySourceName == filterAndTransform.SourceName)
						{
							if (filterAndTransform.SamplingResult > activitySamplingResult)
							{
								activitySamplingResult = filterAndTransform.SamplingResult;
							}
							if (activitySamplingResult >= ActivitySamplingResult.AllDataAndRecorded)
							{
								return activitySamplingResult;
							}
						}
						else if (filterAndTransform.SourceName == "*")
						{
							if (activitySamplingResult != ActivitySamplingResult.None)
							{
								return activitySamplingResult;
							}
							if (filterAndTransform.SamplingResult > activitySamplingResult2)
							{
								activitySamplingResult2 = filterAndTransform.SamplingResult;
							}
						}
					}
					filterAndTransform = filterAndTransform.Next;
				}
				if (activitySamplingResult == ActivitySamplingResult.None)
				{
					return activitySamplingResult2;
				}
				return activitySamplingResult;
			}

			internal static void CreateActivityListener(DiagnosticSourceEventSource eventSource)
			{
				eventSource._activityListener = new ActivityListener();
				eventSource._activityListener.SampleUsingParentId = delegate(ref ActivityCreationOptions<string> activityOptions)
				{
					return Sample(activityOptions.Source.Name, activityOptions.Name, eventSource);
				};
				eventSource._activityListener.Sample = delegate(ref ActivityCreationOptions<ActivityContext> activityOptions)
				{
					return Sample(activityOptions.Source.Name, activityOptions.Name, eventSource);
				};
				eventSource._activityListener.ShouldListenTo = delegate(ActivitySource activitySource)
				{
					for (FilterAndTransform filterAndTransform = eventSource._activitySourceSpecs; filterAndTransform != null; filterAndTransform = filterAndTransform.Next)
					{
						if (activitySource.Name == filterAndTransform.SourceName || filterAndTransform.SourceName == "*")
						{
							return true;
						}
					}
					return false;
				};
				eventSource._activityListener.ActivityStarted = delegate(Activity activity)
				{
					OnActivityStarted(eventSource, activity);
				};
				eventSource._activityListener.ActivityStopped = delegate(Activity activity)
				{
					OnActivityStopped(eventSource, activity);
				};
				ActivitySource.AddActivityListener(eventSource._activityListener);
			}

			[DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(Activity))]
			[DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ActivityContext))]
			[DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ActivityEvent))]
			[DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ActivityLink))]
			[DynamicDependency("Ticks", typeof(DateTime))]
			[DynamicDependency("Ticks", typeof(TimeSpan))]
			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Activity's properties are being preserved with the DynamicDependencies on OnActivityStarted.")]
			private static void OnActivityStarted(DiagnosticSourceEventSource eventSource, Activity activity)
			{
				for (FilterAndTransform filterAndTransform = eventSource._activitySourceSpecs; filterAndTransform != null; filterAndTransform = filterAndTransform.Next)
				{
					if ((filterAndTransform.Events & ActivityEvents.ActivityStart) != ActivityEvents.None && (activity.Source.Name == filterAndTransform.SourceName || filterAndTransform.SourceName == "*") && (filterAndTransform.ActivityName == null || filterAndTransform.ActivityName == activity.OperationName))
					{
						eventSource.ActivityStart(activity.Source.Name, activity.OperationName, filterAndTransform.Morph(activity));
						break;
					}
				}
			}

			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Activity's properties are being preserved with the DynamicDependencies on OnActivityStarted.")]
			private static void OnActivityStopped(DiagnosticSourceEventSource eventSource, Activity activity)
			{
				for (FilterAndTransform filterAndTransform = eventSource._activitySourceSpecs; filterAndTransform != null; filterAndTransform = filterAndTransform.Next)
				{
					if ((filterAndTransform.Events & ActivityEvents.ActivityStop) != ActivityEvents.None && (activity.Source.Name == filterAndTransform.SourceName || filterAndTransform.SourceName == "*") && (filterAndTransform.ActivityName == null || filterAndTransform.ActivityName == activity.OperationName))
					{
						eventSource.ActivityStop(activity.Source.Name, activity.OperationName, filterAndTransform.Morph(activity));
						break;
					}
				}
			}

			internal static void NormalizeActivitySourceSpecsList(DiagnosticSourceEventSource eventSource)
			{
				FilterAndTransform filterAndTransform = eventSource._activitySourceSpecs;
				FilterAndTransform filterAndTransform2 = null;
				FilterAndTransform filterAndTransform3 = null;
				FilterAndTransform filterAndTransform4 = null;
				FilterAndTransform filterAndTransform5 = null;
				while (filterAndTransform != null)
				{
					if (filterAndTransform.SourceName == "*")
					{
						if (filterAndTransform4 == null)
						{
							filterAndTransform4 = (filterAndTransform5 = filterAndTransform);
						}
						else
						{
							filterAndTransform5.Next = filterAndTransform;
							filterAndTransform5 = filterAndTransform;
						}
					}
					else if (filterAndTransform2 == null)
					{
						filterAndTransform2 = (filterAndTransform3 = filterAndTransform);
					}
					else
					{
						filterAndTransform3.Next = filterAndTransform;
						filterAndTransform3 = filterAndTransform;
					}
					filterAndTransform = filterAndTransform.Next;
				}
				if (filterAndTransform2 != null && filterAndTransform4 != null)
				{
					filterAndTransform3.Next = filterAndTransform4;
					filterAndTransform5.Next = null;
					eventSource._activitySourceSpecs = filterAndTransform2;
				}
			}

			private void Dispose()
			{
				if (_diagnosticsListenersSubscription != null)
				{
					_diagnosticsListenersSubscription.Dispose();
					_diagnosticsListenersSubscription = null;
				}
				if (_liveSubscriptions != null)
				{
					Subscriptions subscriptions = _liveSubscriptions;
					_liveSubscriptions = null;
					while (subscriptions != null)
					{
						subscriptions.Subscription.Dispose();
						subscriptions = subscriptions.Next;
					}
				}
			}

			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2112:ReflectionToRequiresUnreferencedCode", Justification = "In EventSource, EnsureDescriptorsInitialized's use of GetType preserves this method which requires unreferenced code, but EnsureDescriptorsInitialized does not access this member and is safe to call.")]
			[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
			public List<KeyValuePair<string, string>> Morph(object args)
			{
				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
				if (args != null)
				{
					if (!_noImplicitTransforms)
					{
						Type type = args.GetType();
						ImplicitTransformEntry firstImplicitTransformsEntry = _firstImplicitTransformsEntry;
						TransformSpec transformSpec;
						if (firstImplicitTransformsEntry != null && firstImplicitTransformsEntry.Type == type)
						{
							transformSpec = firstImplicitTransformsEntry.Transforms;
						}
						else if (firstImplicitTransformsEntry == null)
						{
							transformSpec = MakeImplicitTransforms(type);
							Interlocked.CompareExchange(ref _firstImplicitTransformsEntry, new ImplicitTransformEntry
							{
								Type = type,
								Transforms = transformSpec
							}, null);
						}
						else
						{
							if (_implicitTransformsTable == null)
							{
								Interlocked.CompareExchange(ref _implicitTransformsTable, new ConcurrentDictionary<Type, TransformSpec>(1, 8), null);
							}
							transformSpec = _implicitTransformsTable.GetOrAdd(type, (Type transformType) => MakeImplicitTransformsWrapper(transformType));
						}
						if (transformSpec != null)
						{
							for (TransformSpec transformSpec2 = transformSpec; transformSpec2 != null; transformSpec2 = transformSpec2.Next)
							{
								list.Add(transformSpec2.Morph(args));
							}
						}
					}
					if (_explicitTransforms != null)
					{
						for (TransformSpec transformSpec3 = _explicitTransforms; transformSpec3 != null; transformSpec3 = transformSpec3.Next)
						{
							KeyValuePair<string, string> item = transformSpec3.Morph(args);
							if (item.Value != null)
							{
								list.Add(item);
							}
						}
					}
				}
				return list;
				[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "The Morph method has RequiresUnreferencedCode, but the trimmer can't see through lamdba calls.")]
				static TransformSpec MakeImplicitTransformsWrapper(Type transformType)
				{
					return MakeImplicitTransforms(transformType);
				}
			}

			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2112:ReflectionToRequiresUnreferencedCode", Justification = "In EventSource, EnsureDescriptorsInitialized's use of GetType preserves this method which requires unreferenced code, but EnsureDescriptorsInitialized does not access this member and is safe to call.")]
			[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
			private static TransformSpec MakeImplicitTransforms(Type type)
			{
				TransformSpec transformSpec = null;
				TypeInfo typeInfo = type.GetTypeInfo();
				PropertyInfo[] properties = typeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (PropertyInfo propertyInfo in properties)
				{
					if (!(propertyInfo.GetMethod == null) && propertyInfo.GetMethod.GetParameters().Length == 0)
					{
						transformSpec = new TransformSpec(propertyInfo.Name, 0, propertyInfo.Name.Length, transformSpec);
					}
				}
				return Reverse(transformSpec);
			}

			private static TransformSpec Reverse(TransformSpec list)
			{
				TransformSpec transformSpec = null;
				while (list != null)
				{
					TransformSpec next = list.Next;
					list.Next = transformSpec;
					transformSpec = list;
					list = next;
				}
				return transformSpec;
			}
		}

		internal sealed class ImplicitTransformEntry
		{
			public Type Type;

			public TransformSpec Transforms;
		}

		internal sealed class TransformSpec
		{
			internal sealed class PropertySpec
			{
				private class PropertyFetch
				{
					private sealed class RefTypedFetchProperty<TObject, TProperty> : PropertyFetch
					{
						private readonly Func<TObject, TProperty> _propertyFetch;

						public RefTypedFetchProperty(Type type, PropertyInfo property)
							: base(type)
						{
							_propertyFetch = (Func<TObject, TProperty>)property.GetMethod.CreateDelegate(typeof(Func<TObject, TProperty>));
						}

						public override object Fetch(object obj)
						{
							return _propertyFetch((TObject)obj);
						}
					}

					private delegate TProperty StructFunc<TStruct, TProperty>(ref TStruct thisArg);

					private sealed class ValueTypedFetchProperty<TStruct, TProperty> : PropertyFetch
					{
						private readonly StructFunc<TStruct, TProperty> _propertyFetch;

						public ValueTypedFetchProperty(Type type, PropertyInfo property)
							: base(type)
						{
							_propertyFetch = (StructFunc<TStruct, TProperty>)property.GetMethod.CreateDelegate(typeof(StructFunc<TStruct, TProperty>));
						}

						public override object Fetch(object obj)
						{
							TStruct thisArg = (TStruct)obj;
							return _propertyFetch(ref thisArg);
						}
					}

					private sealed class CurrentActivityPropertyFetch : PropertyFetch
					{
						public CurrentActivityPropertyFetch()
							: base(null)
						{
						}

						public override object Fetch(object obj)
						{
							return Activity.Current;
						}
					}

					private sealed class EnumeratePropertyFetch<ElementType> : PropertyFetch
					{
						public EnumeratePropertyFetch(Type type)
							: base(type)
						{
						}

						public override object Fetch(object obj)
						{
							return string.Join(",", (IEnumerable<ElementType>)obj);
						}
					}

					internal Type Type { get; }

					public PropertyFetch(Type type)
					{
						Type = type;
					}

					[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2112:ReflectionToRequiresUnreferencedCode", Justification = "In EventSource, EnsureDescriptorsInitialized's use of GetType preserves this method which requires unreferenced code, but EnsureDescriptorsInitialized does not access this member and is safe to call.")]
					[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
					public static PropertyFetch FetcherForProperty(Type type, string propertyName)
					{
						if (propertyName == null)
						{
							return new PropertyFetch(type);
						}
						if (propertyName == "*Activity")
						{
							return new CurrentActivityPropertyFetch();
						}
						TypeInfo typeInfo = type.GetTypeInfo();
						if (propertyName == "*Enumerate")
						{
							Type[] interfaces = typeInfo.GetInterfaces();
							foreach (Type type2 in interfaces)
							{
								TypeInfo typeInfo2 = type2.GetTypeInfo();
								if (typeInfo2.IsGenericType && !(typeInfo2.GetGenericTypeDefinition() != typeof(IEnumerable<>)))
								{
									Type type3 = typeInfo2.GetGenericArguments()[0];
									Type type4 = typeof(EnumeratePropertyFetch<>).GetTypeInfo().MakeGenericType(type3);
									return (PropertyFetch)Activator.CreateInstance(type4, type);
								}
							}
							Log.Message($"*Enumerate applied to non-enumerable type {type}");
							return new PropertyFetch(type);
						}
						PropertyInfo propertyInfo = typeInfo.GetDeclaredProperty(propertyName);
						if (propertyInfo == null)
						{
							PropertyInfo[] properties = typeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
							foreach (PropertyInfo propertyInfo2 in properties)
							{
								if (propertyInfo2.Name == propertyName)
								{
									propertyInfo = propertyInfo2;
									break;
								}
							}
						}
						if (propertyInfo == null)
						{
							Log.Message($"Property {propertyName} not found on {type}. Ensure the name is spelled correctly. If you published the application with PublishTrimmed=true, ensure the property was not trimmed away.");
							return new PropertyFetch(type);
						}
						MethodInfo getMethod = propertyInfo.GetMethod;
						if ((object)getMethod == null || !getMethod.IsStatic)
						{
							MethodInfo setMethod = propertyInfo.SetMethod;
							if ((object)setMethod == null || !setMethod.IsStatic)
							{
								Type type5 = (typeInfo.IsValueType ? typeof(ValueTypedFetchProperty<, >) : typeof(RefTypedFetchProperty<, >));
								Type type6 = type5.GetTypeInfo().MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType);
								return (PropertyFetch)Activator.CreateInstance(type6, type, propertyInfo);
							}
						}
						Log.Message("Property " + propertyName + " is static.");
						return new PropertyFetch(type);
					}

					public virtual object Fetch(object obj)
					{
						return null;
					}
				}

				private const string CurrentActivityPropertyName = "*Activity";

				private const string EnumeratePropertyName = "*Enumerate";

				public PropertySpec Next;

				private readonly string _propertyName;

				private volatile PropertyFetch _fetchForExpectedType;

				public bool IsStatic { get; private set; }

				public PropertySpec(string propertyName, PropertySpec next)
				{
					Next = next;
					_propertyName = propertyName;
					if (_propertyName == "*Activity")
					{
						IsStatic = true;
					}
				}

				[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2112:ReflectionToRequiresUnreferencedCode", Justification = "In EventSource, EnsureDescriptorsInitialized's use of GetType preserves this method which requires unreferenced code, but EnsureDescriptorsInitialized does not access this member and is safe to call.")]
				[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
				public object Fetch(object obj)
				{
					PropertyFetch propertyFetch = _fetchForExpectedType;
					Type type = obj?.GetType();
					if (propertyFetch == null || propertyFetch.Type != type)
					{
						propertyFetch = (_fetchForExpectedType = PropertyFetch.FetcherForProperty(type, _propertyName));
					}
					object result = null;
					try
					{
						result = propertyFetch.Fetch(obj);
					}
					catch (Exception arg)
					{
						Log.Message($"Property {type}.{_propertyName} threw the exception {arg}");
					}
					return result;
				}
			}

			public TransformSpec Next;

			private readonly string _outputName;

			private readonly PropertySpec _fetches;

			public TransformSpec(string transformSpec, int startIdx, int endIdx, TransformSpec next = null)
			{
				Next = next;
				int num = transformSpec.IndexOf('=', startIdx, endIdx - startIdx);
				if (0 <= num)
				{
					_outputName = transformSpec.Substring(startIdx, num - startIdx);
					startIdx = num + 1;
				}
				while (startIdx < endIdx)
				{
					int num2 = transformSpec.LastIndexOf('.', endIdx - 1, endIdx - startIdx);
					int num3 = startIdx;
					if (0 <= num2)
					{
						num3 = num2 + 1;
					}
					string text = transformSpec.Substring(num3, endIdx - num3);
					_fetches = new PropertySpec(text, _fetches);
					if (_outputName == null)
					{
						_outputName = text;
					}
					endIdx = num2;
				}
			}

			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2112:ReflectionToRequiresUnreferencedCode", Justification = "In EventSource, EnsureDescriptorsInitialized's use of GetType preserves this method which requires unreferenced code, but EnsureDescriptorsInitialized does not access this member and is safe to call.")]
			[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
			public KeyValuePair<string, string> Morph(object obj)
			{
				for (PropertySpec propertySpec = _fetches; propertySpec != null; propertySpec = propertySpec.Next)
				{
					if (obj != null || propertySpec.IsStatic)
					{
						obj = propertySpec.Fetch(obj);
					}
				}
				return new KeyValuePair<string, string>(_outputName, obj?.ToString());
			}
		}

		internal sealed class CallbackObserver<T> : IObserver<T>
		{
			private readonly Action<T> _callback;

			public CallbackObserver(Action<T> callback)
			{
				_callback = callback;
			}

			public void OnCompleted()
			{
			}

			public void OnError(Exception error)
			{
			}

			public void OnNext(T value)
			{
				_callback(value);
			}
		}

		internal sealed class Subscriptions
		{
			public IDisposable Subscription;

			public Subscriptions Next;

			public Subscriptions(IDisposable subscription, Subscriptions next)
			{
				Subscription = subscription;
				Next = next;
			}
		}

		public static DiagnosticSourceEventSource Log = new DiagnosticSourceEventSource();

		private readonly string AspNetCoreHostingKeywordValue = "Microsoft.AspNetCore/Microsoft.AspNetCore.Hosting.BeginRequest@Activity1Start:-httpContext.Request.Method;httpContext.Request.Host;httpContext.Request.Path;httpContext.Request.QueryString\nMicrosoft.AspNetCore/Microsoft.AspNetCore.Hosting.EndRequest@Activity1Stop:-httpContext.TraceIdentifier;httpContext.Response.StatusCode";

		private readonly string EntityFrameworkCoreCommandsKeywordValue = "Microsoft.EntityFrameworkCore/Microsoft.EntityFrameworkCore.BeforeExecuteCommand@Activity2Start:-Command.Connection.DataSource;Command.Connection.Database;Command.CommandText\nMicrosoft.EntityFrameworkCore/Microsoft.EntityFrameworkCore.AfterExecuteCommand@Activity2Stop:-";

		private volatile bool _false;

		private FilterAndTransform _specs;

		private FilterAndTransform _activitySourceSpecs;

		private ActivityListener _activityListener;

		[Event(1, Keywords = (EventKeywords)1L)]
		public void Message(string Message)
		{
			WriteEvent(1, Message);
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(2, Keywords = (EventKeywords)2L)]
		private void Event(string SourceName, string EventName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			WriteEvent(2, SourceName, EventName, Arguments);
		}

		[Event(3, Keywords = (EventKeywords)2L)]
		private void EventJson(string SourceName, string EventName, string ArgmentsJson)
		{
			WriteEvent(3, SourceName, EventName, ArgmentsJson);
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(4, Keywords = (EventKeywords)2L)]
		private void Activity1Start(string SourceName, string EventName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			WriteEvent(4, SourceName, EventName, Arguments);
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(5, Keywords = (EventKeywords)2L)]
		private void Activity1Stop(string SourceName, string EventName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			WriteEvent(5, SourceName, EventName, Arguments);
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(6, Keywords = (EventKeywords)2L)]
		private void Activity2Start(string SourceName, string EventName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			WriteEvent(6, SourceName, EventName, Arguments);
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(7, Keywords = (EventKeywords)2L)]
		private void Activity2Stop(string SourceName, string EventName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			WriteEvent(7, SourceName, EventName, Arguments);
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(8, Keywords = (EventKeywords)2L, ActivityOptions = EventActivityOptions.Recursive)]
		private void RecursiveActivity1Start(string SourceName, string EventName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			WriteEvent(8, SourceName, EventName, Arguments);
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(9, Keywords = (EventKeywords)2L, ActivityOptions = EventActivityOptions.Recursive)]
		private void RecursiveActivity1Stop(string SourceName, string EventName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			WriteEvent(9, SourceName, EventName, Arguments);
		}

		[Event(10, Keywords = (EventKeywords)2L)]
		private void NewDiagnosticListener(string SourceName)
		{
			WriteEvent(10, SourceName);
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(11, Keywords = (EventKeywords)2L, ActivityOptions = EventActivityOptions.Recursive)]
		private void ActivityStart(string SourceName, string ActivityName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			WriteEvent(11, SourceName, ActivityName, Arguments);
		}

		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(12, Keywords = (EventKeywords)2L, ActivityOptions = EventActivityOptions.Recursive)]
		private void ActivityStop(string SourceName, string ActivityName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			WriteEvent(12, SourceName, ActivityName, Arguments);
		}

		private DiagnosticSourceEventSource()
			: base(EventSourceSettings.EtwSelfDescribingEventFormat)
		{
		}

		[NonEvent]
		protected override void OnEventCommand(EventCommandEventArgs command)
		{
			BreakPointWithDebuggerFuncEval();
			lock (this)
			{
				if ((command.Command == EventCommand.Update || command.Command == EventCommand.Enable) && IsEnabled(EventLevel.Informational, (EventKeywords)2L))
				{
					string value = null;
					command.Arguments.TryGetValue("FilterAndPayloadSpecs", out value);
					if (!IsEnabled(EventLevel.Informational, (EventKeywords)2048L))
					{
						if (IsEnabled(EventLevel.Informational, (EventKeywords)4096L))
						{
							value = NewLineSeparate(value, AspNetCoreHostingKeywordValue);
						}
						if (IsEnabled(EventLevel.Informational, (EventKeywords)8192L))
						{
							value = NewLineSeparate(value, EntityFrameworkCoreCommandsKeywordValue);
						}
					}
					FilterAndTransform.CreateFilterAndTransformList(ref _specs, value, this);
				}
				else if (command.Command == EventCommand.Update || command.Command == EventCommand.Disable)
				{
					FilterAndTransform.DestroyFilterAndTransformList(ref _specs, this);
				}
			}
		}

		private static string NewLineSeparate(string str1, string str2)
		{
			if (string.IsNullOrEmpty(str1))
			{
				return str2;
			}
			return str1 + "\n" + str2;
		}

		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		[NonEvent]
		private void BreakPointWithDebuggerFuncEval()
		{
			new object();
			while (_false)
			{
				_false = false;
			}
		}
	}
	public class Activity : IDisposable
	{
		private sealed class BaggageLinkedList : IEnumerable<KeyValuePair<string, string>>, IEnumerable
		{
			private DiagNode<KeyValuePair<string, string>> _first;

			public DiagNode<KeyValuePair<string, string>> First => _first;

			public BaggageLinkedList(KeyValuePair<string, string> firstValue, bool set = false)
			{
				_first = ((set && firstValue.Value == null) ? null : new DiagNode<KeyValuePair<string, string>>(firstValue));
			}

			public void Add(KeyValuePair<string, string> value)
			{
				DiagNode<KeyValuePair<string, string>> diagNode = new DiagNode<KeyValuePair<string, string>>(value);
				lock (this)
				{
					diagNode.Next = _first;
					_first = diagNode;
				}
			}

			public void Set(KeyValuePair<string, string> value)
			{
				if (value.Value == null)
				{
					Remove(value.Key);
					return;
				}
				lock (this)
				{
					for (DiagNode<KeyValuePair<string, string>> diagNode = _first; diagNode != null; diagNode = diagNode.Next)
					{
						if (diagNode.Value.Key == value.Key)
						{
							diagNode.Value = value;
							return;
						}
					}
					DiagNode<KeyValuePair<string, string>> diagNode2 = new DiagNode<KeyValuePair<string, string>>(value);
					diagNode2.Next = _first;
					_first = diagNode2;
				}
			}

			public void Remove(string key)
			{
				lock (this)
				{
					if (_first == null)
					{
						return;
					}
					if (_first.Value.Key == key)
					{
						_first = _first.Next;
						return;
					}
					DiagNode<KeyValuePair<string, string>> diagNode = _first;
					while (diagNode.Next != null)
					{
						if (diagNode.Next.Value.Key == key)
						{
							diagNode.Next = diagNode.Next.Next;
							break;
						}
						diagNode = diagNode.Next;
					}
				}
			}

			public Enumerator<KeyValuePair<string, string>> GetEnumerator()
			{
				return new Enumerator<KeyValuePair<string, string>>(_first);
			}

			IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
			{
				return GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		private sealed class TagsLinkedList : IEnumerable<KeyValuePair<string, object>>, IEnumerable
		{
			private DiagNode<KeyValuePair<string, object>> _first;

			private DiagNode<KeyValuePair<string, object>> _last;

			private StringBuilder _stringBuilder;

			public TagsLinkedList(KeyValuePair<string, object> firstValue, bool set = false)
			{
				_last = (_first = ((set && firstValue.Value == null) ? null : new DiagNode<KeyValuePair<string, object>>(firstValue)));
			}

			public TagsLinkedList(IEnumerator<KeyValuePair<string, object>> e)
			{
				_last = (_first = new DiagNode<KeyValuePair<string, object>>(e.Current));
				while (e.MoveNext())
				{
					_last.Next = new DiagNode<KeyValuePair<string, object>>(e.Current);
					_last = _last.Next;
				}
			}

			public TagsLinkedList(IEnumerable<KeyValuePair<string, object>> list)
			{
				Add(list);
			}

			public void Add(IEnumerable<KeyValuePair<string, object>> list)
			{
				IEnumerator<KeyValuePair<string, object>> enumerator = list.GetEnumerator();
				if (enumerator.MoveNext())
				{
					if (_first == null)
					{
						_last = (_first = new DiagNode<KeyValuePair<string, object>>(enumerator.Current));
					}
					else
					{
						_last.Next = new DiagNode<KeyValuePair<string, object>>(enumerator.Current);
						_last = _last.Next;
					}
					while (enumerator.MoveNext())
					{
						_last.Next = new DiagNode<KeyValuePair<string, object>>(enumerator.Current);
						_last = _last.Next;
					}
				}
			}

			public void Add(KeyValuePair<string, object> value)
			{
				DiagNode<KeyValuePair<string, object>> diagNode = new DiagNode<KeyValuePair<string, object>>(value);
				lock (this)
				{
					if (_first == null)
					{
						_first = (_last = diagNode);
						return;
					}
					_last.Next = diagNode;
					_last = diagNode;
				}
			}

			public object Get(string key)
			{
				for (DiagNode<KeyValuePair<string, object>> diagNode = _first; diagNode != null; diagNode = diagNode.Next)
				{
					if (diagNode.Value.Key == key)
					{
						return diagNode.Value.Value;
					}
				}
				return null;
			}

			public void Remove(string key)
			{
				lock (this)
				{
					if (_first == null)
					{
						return;
					}
					if (_first.Value.Key == key)
					{
						_first = _first.Next;
						if (_first == null)
						{
							_last = null;
						}
						return;
					}
					DiagNode<KeyValuePair<string, object>> diagNode = _first;
					while (diagNode.Next != null)
					{
						if (diagNode.Next.Value.Key == key)
						{
							if (_last == diagNode.Next)
							{
								_last = diagNode;
							}
							diagNode.Next = diagNode.Next.Next;
							break;
						}
						diagNode = diagNode.Next;
					}
				}
			}

			public void Set(KeyValuePair<string, object> value)
			{
				if (value.Value == null)
				{
					Remove(value.Key);
					return;
				}
				lock (this)
				{
					for (DiagNode<KeyValuePair<string, object>> diagNode = _first; diagNode != null; diagNode = diagNode.Next)
					{
						if (diagNode.Value.Key == value.Key)
						{
							diagNode.Value = value;
							return;
						}
					}
					DiagNode<KeyValuePair<string, object>> diagNode2 = new DiagNode<KeyValuePair<string, object>>(value);
					if (_first == null)
					{
						_first = (_last = diagNode2);
						return;
					}
					_last.Next = diagNode2;
					_last = diagNode2;
				}
			}

			public Enumerator<KeyValuePair<string, object>> GetEnumerator()
			{
				return new Enumerator<KeyValuePair<string, object>>(_first);
			}

			IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
			{
				return GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public IEnumerable<KeyValuePair<string, string>> EnumerateStringValues()
			{
				for (DiagNode<KeyValuePair<string, object>> current = _first; current != null; current = current.Next)
				{
					if (current.Value.Value is string || current.Value.Value == null)
					{
						yield return new KeyValuePair<string, string>(current.Value.Key, (string)current.Value.Value);
					}
				}
			}

			public override string ToString()
			{
				lock (this)
				{
					if (_first == null)
					{
						return string.Empty;
					}
					if (_stringBuilder == null)
					{
						_stringBuilder = new StringBuilder();
					}
					_stringBuilder.Append(_first.Value.Key);
					_stringBuilder.Append(':');
					_stringBuilder.Append(_first.Value.Value);
					for (DiagNode<KeyValuePair<string, object>> next = _first.Next; next != null; next = next.Next)
					{
						_stringBuilder.Append(", ");
						_stringBuilder.Append(next.Value.Key);
						_stringBuilder.Append(':');
						_stringBuilder.Append(next.Value.Value);
					}
					string result = _stringBuilder.ToString();
					_stringBuilder.Clear();
					return result;
				}
			}
		}

		[Flags]
		private enum State : byte
		{
			None = 0,
			FormatUnknown = 0,
			FormatHierarchical = 1,
			FormatW3C = 2,
			FormatFlags = 3,
			IsFinished = 0x80
		}

		private static readonly IEnumerable<KeyValuePair<string, string>> s_emptyBaggageTags = new KeyValuePair<string, string>[0];

		private static readonly IEnumerable<KeyValuePair<string, object>> s_emptyTagObjects = new KeyValuePair<string, object>[0];

		private static readonly IEnumerable<ActivityLink> s_emptyLinks = new ActivityLink[0];

		private static readonly IEnumerable<ActivityEvent> s_emptyEvents = new ActivityEvent[0];

		private static readonly ActivitySource s_defaultSource = new ActivitySource(string.Empty);

		private const byte ActivityTraceFlagsIsSet = 128;

		private const int RequestIdMaxLength = 1024;

		private static readonly string s_uniqSuffix = "-" + GetRandomNumber().ToString("x") + ".";

		private static long s_currentRootId = (uint)GetRandomNumber();

		private static ActivityIdFormat s_defaultIdFormat;

		private string _traceState;

		private State _state;

		private int _currentChildId;

		private string _id;

		private string _rootId;

		private string _parentId;

		private string _parentSpanId;

		private string _traceId;

		private string _spanId;

		private byte _w3CIdFlags;

		private byte _parentTraceFlags;

		private TagsLinkedList _tags;

		private BaggageLinkedList _baggage;

		private DiagLinkedList<ActivityLink> _links;

		private DiagLinkedList<ActivityEvent> _events;

		private Dictionary<string, object> _customProperties;

		private string _displayName;

		private ActivityStatusCode _statusCode;

		private string _statusDescription;

		private Activity _previousActiveActivity;

		private static readonly AsyncLocal<Activity> s_current = new AsyncLocal<Activity>();

		public static bool ForceDefaultIdFormat { get; set; }

		public ActivityStatusCode Status => _statusCode;

		public string? StatusDescription => _statusDescription;

		public ActivityKind Kind { get; private set; }

		public string OperationName { get; }

		public string DisplayName
		{
			get
			{
				return _displayName ?? OperationName;
			}
			set
			{
				_displayName = value ?? throw new ArgumentNullException("value");
			}
		}

		public ActivitySource Source { get; private set; }

		public Activity? Parent { get; private set; }

		public TimeSpan Duration { get; private set; }

		public DateTime StartTimeUtc { get; private set; }

		public string? Id
		{
			get
			{
				if (_id == null && _spanId != null)
				{
					Span<char> buffer = stackalloc char[2];
					System.HexConverter.ToCharsBuffer((byte)(-129 & _w3CIdFlags), buffer, 0, System.HexConverter.Casing.Lower);
					string value = "00-" + _traceId + "-" + _spanId + "-" + buffer;
					Interlocked.CompareExchange(ref _id, value, null);
				}
				return _id;
			}
		}

		public string? ParentId
		{
			get
			{
				if (_parentId == null)
				{
					if (_parentSpanId != null)
					{
						Span<char> buffer = stackalloc char[2];
						System.HexConverter.ToCharsBuffer((byte)(-129 & _parentTraceFlags), buffer, 0, System.HexConverter.Casing.Lower);
						string value = "00-" + _traceId + "-" + _parentSpanId + "-" + buffer;
						Interlocked.CompareExchange(ref _parentId, value, null);
					}
					else if (Parent != null)
					{
						Interlocked.CompareExchange(ref _parentId, Parent.Id, null);
					}
				}
				return _parentId;
			}
		}

		public string? RootId
		{
			get
			{
				if (_rootId == null)
				{
					string text = null;
					if (Id != null)
					{
						text = GetRootId(Id);
					}
					else if (ParentId != null)
					{
						text = GetRootId(ParentId);
					}
					if (text != null)
					{
						Interlocked.CompareExchange(ref _rootId, text, null);
					}
				}
				return _rootId;
			}
		}

		public IEnumerable<KeyValuePair<string, string?>> Tags => _tags?.EnumerateStringValues() ?? s_emptyBaggageTags;

		public IEnumerable<KeyValuePair<string, object?>> TagObjects
		{
			get
			{
				IEnumerable<KeyValuePair<string, object>> tags = _tags;
				return tags ?? s_emptyTagObjects;
			}
		}

		public IEnumerable<ActivityEvent> Events
		{
			get
			{
				IEnumerable<ActivityEvent> events = _events;
				return events ?? s_emptyEvents;
			}
		}

		public IEnumerable<ActivityLink> Links
		{
			get
			{
				IEnumerable<ActivityLink> links = _links;
				return links ?? s_emptyLinks;
			}
		}

		public IEnumerable<KeyValuePair<string, string?>> Baggage
		{
			get
			{
				for (Activity activity = this; activity != null; activity = activity.Parent)
				{
					if (activity._baggage != null)
					{
						return Iterate(activity);
					}
				}
				return s_emptyBaggageTags;
				static IEnumerable<KeyValuePair<string, string>> Iterate(Activity parent)
				{
					do
					{
						if (parent._baggage != null)
						{
							for (DiagNode<KeyValuePair<string, string>> current = parent._baggage.First; current != null; current = current.Next)
							{
								yield return current.Value;
							}
						}
						parent = parent.Parent;
					}
					while (parent != null);
				}
			}
		}

		public ActivityContext Context => new ActivityContext(TraceId, SpanId, ActivityTraceFlags, TraceStateString);

		public string? TraceStateString
		{
			get
			{
				for (Activity activity = this; activity != null; activity = activity.Parent)
				{
					string traceState = activity._traceState;
					if (traceState != null)
					{
						return traceState;
					}
				}
				return null;
			}
			set
			{
				_traceState = value;
			}
		}

		public ActivitySpanId SpanId
		{
			get
			{
				if (_spanId == null && _id != null && IdFormat == ActivityIdFormat.W3C)
				{
					string value = ActivitySpanId.CreateFromString(MemoryExtensions.AsSpan(_id, 36, 16)).ToHexString();
					Interlocked.CompareExchange(ref _spanId, value, null);
				}
				return new ActivitySpanId(_spanId);
			}
		}

		public ActivityTraceId TraceId
		{
			get
			{
				if (_traceId == null)
				{
					TrySetTraceIdFromParent();
				}
				return new ActivityTraceId(_traceId);
			}
		}

		public bool Recorded => (ActivityTraceFlags & ActivityTraceFlags.Recorded) != 0;

		public bool IsAllDataRequested { get; set; }

		public ActivityTraceFlags ActivityTraceFlags
		{
			get
			{
				if (!W3CIdFlagsSet)
				{
					TrySetTraceFlagsFromParent();
				}
				return (ActivityTraceFlags)(-129 & _w3CIdFlags);
			}
			set
			{
				_w3CIdFlags = (byte)(0x80 | (byte)value);
			}
		}

		public ActivitySpanId ParentSpanId
		{
			get
			{
				if (_parentSpanId == null)
				{
					string text = null;
					if (_parentId != null && IsW3CId(_parentId))
					{
						try
						{
							text = ActivitySpanId.CreateFromString(MemoryExtensions.AsSpan(_parentId, 36, 16)).ToHexString();
						}
						catch
						{
						}
					}
					else if (Parent != null && Parent.IdFormat == ActivityIdFormat.W3C)
					{
						text = Parent.SpanId.ToHexString();
					}
					if (text != null)
					{
						Interlocked.CompareExchange(ref _parentSpanId, text, null);
					}
				}
				return new ActivitySpanId(_parentSpanId);
			}
		}

		public static Func<ActivityTraceId>? TraceIdGenerator { get; set; }

		public static ActivityIdFormat DefaultIdFormat
		{
			get
			{
				if (s_defaultIdFormat == ActivityIdFormat.Unknown)
				{
					s_defaultIdFormat = ActivityIdFormat.Hierarchical;
				}
				return s_defaultIdFormat;
			}
			set
			{
				if (ActivityIdFormat.Hierarchical > value || value > ActivityIdFormat.W3C)
				{
					throw new ArgumentException(System.SR.ActivityIdFormatInvalid);
				}
				s_defaultIdFormat = value;
			}
		}

		private bool W3CIdFlagsSet => (_w3CIdFlags & 0x80) != 0;

		private bool IsFinished
		{
			get
			{
				return (_state & State.IsFinished) != 0;
			}
			set
			{
				if (value)
				{
					_state |= State.IsFinished;
				}
				else
				{
					_state &= ~State.IsFinished;
				}
			}
		}

		public ActivityIdFormat IdFormat
		{
			get
			{
				return (ActivityIdFormat)(_state & State.FormatFlags);
			}
			private set
			{
				_state = (State)((uint)(_state & ~State.FormatFlags) | (uint)(byte)((byte)value & 3));
			}
		}

		public static Activity? Current
		{
			get
			{
				return s_current.Value;
			}
			set
			{
				if (ValidateSetCurrent(value))
				{
					SetCurrent(value);
				}
			}
		}

		public Activity SetStatus(ActivityStatusCode code, string? description = null)
		{
			_statusCode = code;
			_statusDescription = ((code == ActivityStatusCode.Error) ? description : null);
			return this;
		}

		public string? GetBaggageItem(string key)
		{
			foreach (KeyValuePair<string, string> item in Baggage)
			{
				if (key == item.Key)
				{
					return item.Value;
				}
			}
			return null;
		}

		public object? GetTagItem(string key)
		{
			return _tags?.Get(key) ?? null;
		}

		public Activity(string operationName)
		{
			Source = s_defaultSource;
			IsAllDataRequested = true;
			if (string.IsNullOrEmpty(operationName))
			{
				NotifyError(new ArgumentException(System.SR.OperationNameInvalid));
			}
			OperationName = operationName;
		}

		public Activity AddTag(string key, string? value)
		{
			return AddTag(key, (object?)value);
		}

		public Activity AddTag(string key, object? value)
		{
			KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(key, value);
			if (_tags != null || Interlocked.CompareExchange(ref _tags, new TagsLinkedList(keyValuePair), null) != null)
			{
				_tags.Add(keyValuePair);
			}
			return this;
		}

		public Activity SetTag(string key, object? value)
		{
			KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(key, value);
			if (_tags != null || Interlocked.CompareExchange(ref _tags, new TagsLinkedList(keyValuePair, set: true), null) != null)
			{
				_tags.Set(keyValuePair);
			}
			return this;
		}

		public Activity AddEvent(ActivityEvent e)
		{
			if (_events != null || Interlocked.CompareExchange(ref _events, new DiagLinkedList<ActivityEvent>(e), null) != null)
			{
				_events.Add(e);
			}
			return this;
		}

		public Activity AddBaggage(string key, string? value)
		{
			KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(key, value);
			if (_baggage != null || Interlocked.CompareExchange(ref _baggage, new BaggageLinkedList(keyValuePair), null) != null)
			{
				_baggage.Add(keyValuePair);
			}
			return this;
		}

		public Activity SetBaggage(string key, string? value)
		{
			KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(key, value);
			if (_baggage != null || Interlocked.CompareExchange(ref _baggage, new BaggageLinkedList(keyValuePair, set: true), null) != null)
			{
				_baggage.Set(keyValuePair);
			}
			return this;
		}

		public Activity SetParentId(string parentId)
		{
			if (Parent != null)
			{
				NotifyError(new InvalidOperationException(System.SR.SetParentIdOnActivityWithParent));
			}
			else if (ParentId != null || _parentSpanId != null)
			{
				NotifyError(new InvalidOperationException(System.SR.ParentIdAlreadySet));
			}
			else if (string.IsNullOrEmpty(parentId))
			{
				NotifyError(new ArgumentException(System.SR.ParentIdInvalid));
			}
			else
			{
				_parentId = parentId;
			}
			return this;
		}

		public Activity SetParentId(ActivityTraceId traceId, ActivitySpanId spanId, ActivityTraceFlags activityTraceFlags = ActivityTraceFlags.None)
		{
			if (Parent != null)
			{
				NotifyError(new InvalidOperationException(System.SR.SetParentIdOnActivityWithParent));
			}
			else if (ParentId != null || _parentSpanId != null)
			{
				NotifyError(new InvalidOperationException(System.SR.ParentIdAlreadySet));
			}
			else
			{
				_traceId = traceId.ToHexString();
				_parentSpanId = spanId.ToHexString();
				ActivityTraceFlags = activityTraceFlags;
				_parentTraceFlags = (byte)activityTraceFlags;
			}
			return this;
		}

		public Activity SetStartTime(DateTime startTimeUtc)
		{
			if (startTimeUtc.Kind != DateTimeKind.Utc)
			{
				NotifyError(new InvalidOperationException(System.SR.StartTimeNotUtc));
			}
			else
			{
				StartTimeUtc = startTimeUtc;
			}
			return this;
		}

		public Activity SetEndTime(DateTime endTimeUtc)
		{
			if (endTimeUtc.Kind != DateTimeKind.Utc)
			{
				NotifyError(new InvalidOperationException(System.SR.EndTimeNotUtc));
			}
			else
			{
				Duration = endTimeUtc - StartTimeUtc;
				if (Duration.Ticks <= 0)
				{
					Duration = new TimeSpan(1L);
				}
			}
			return this;
		}

		public Activity Start()
		{
			if (_id != null || _spanId != null)
			{
				NotifyError(new InvalidOperationException(System.SR.ActivityStartAlreadyStarted));
			}
			else
			{
				_previousActiveActivity = Current;
				if (_parentId == null && _parentSpanId == null && _previousActiveActivity != null)
				{
					Parent = _previousActiveActivity;
				}
				if (StartTimeUtc == default(DateTime))
				{
					StartTimeUtc = GetUtcNow();
				}
				if (IdFormat == ActivityIdFormat.Unknown)
				{
					IdFormat = (ForceDefaultIdFormat ? DefaultIdFormat : ((Parent != null) ? Parent.IdFormat : ((_parentSpanId != null) ? ActivityIdFormat.W3C : ((_parentId == null) ? DefaultIdFormat : ((!IsW3CId(_parentId)) ? ActivityIdFormat.Hierarchical : ActivityIdFormat.W3C)))));
				}
				if (IdFormat == ActivityIdFormat.W3C)
				{
					GenerateW3CId();
				}
				else
				{
					_id = GenerateHierarchicalId();
				}
				SetCurrent(this);
				Source.NotifyActivityStart(this);
			}
			return this;
		}

		public void Stop()
		{
			if (_id == null && _spanId == null)
			{
				NotifyError(new InvalidOperationException(System.SR.ActivityNotStarted));
			}
			else if (!IsFinished)
			{
				IsFinished = true;
				if (Duration == TimeSpan.Zero)
				{
					SetEndTime(GetUtcNow());
				}
				Source.NotifyActivityStop(this);
				SetCurrent(_previousActiveActivity);
			}
		}

		public Activity SetIdFormat(ActivityIdFormat format)
		{
			if (_id != null || _spanId != null)
			{
				NotifyError(new InvalidOperationException(System.SR.SetFormatOnStartedActivity));
			}
			else
			{
				IdFormat = format;
			}
			return this;
		}

		private static bool IsW3CId(string id)
		{
			if (id.Length == 55 && (('0' <= id[0] && id[0] <= '9') || ('a' <= id[0] && id[0] <= 'f')) && (('0' <= id[1] && id[1] <= '9') || ('a' <= id[1] && id[1] <= 'f')))
			{
				if (id[0] == 'f')
				{
					return id[1] != 'f';
				}
				return true;
			}
			return false;
		}

		internal static bool TryConvertIdToContext(string traceParent, string traceState, out ActivityContext context)
		{
			context = default(ActivityContext);
			if (!IsW3CId(traceParent))
			{
				return false;
			}
			ReadOnlySpan<char> idData = MemoryExtensions.AsSpan(traceParent, 3, 32);
			ReadOnlySpan<char> idData2 = MemoryExtensions.AsSpan(traceParent, 36, 16);
			if (!ActivityTraceId.IsLowerCaseHexAndNotAllZeros(idData) || !ActivityTraceId.IsLowerCaseHexAndNotAllZeros(idData2) || !System.HexConverter.IsHexLowerChar(traceParent[53]) || !System.HexConverter.IsHexLowerChar(traceParent[54]))
			{
				return false;
			}
			context = new ActivityContext(new ActivityTraceId(idData.ToString()), new ActivitySpanId(idData2.ToString()), (ActivityTraceFlags)ActivityTraceId.HexByteFromChars(traceParent[53], traceParent[54]), traceState);
			return true;
		}

		public void Dispose()
		{
			if (!IsFinished)
			{
				Stop();
			}
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		public void SetCustomProperty(string propertyName, object? propertyValue)
		{
			if (_customProperties == null)
			{
				Interlocked.CompareExchange(ref _customProperties, new Dictionary<string, object>(), null);
			}
			lock (_customProperties)
			{
				if (propertyValue == null)
				{
					_customProperties.Remove(propertyName);
				}
				else
				{
					_customProperties[propertyName] = propertyValue;
				}
			}
		}

		public object? GetCustomProperty(string propertyName)
		{
			if (_customProperties == null)
			{
				return null;
			}
			lock (_customProperties)
			{
				object value;
				return _customProperties.TryGetValue(propertyName, out value) ? value : null;
			}
		}

		internal static Activity Create(ActivitySource source, string name, ActivityKind kind, string parentId, ActivityContext parentContext, IEnumerable<KeyValuePair<string, object>> tags, IEnumerable<ActivityLink> links, DateTimeOffset startTime, ActivityTagsCollection samplerTags, ActivitySamplingResult request, bool startIt, ActivityIdFormat idFormat)
		{
			Activity activity = new Activity(name);
			activity.Source = source;
			activity.Kind = kind;
			activity.IdFormat = idFormat;
			if (links != null)
			{
				using IEnumerator<ActivityLink> enumerator = links.GetEnumerator();
				if (enumerator.MoveNext())
				{
					activity._links = new DiagLinkedList<ActivityLink>(enumerator);
				}
			}
			if (tags != null)
			{
				using IEnumerator<KeyValuePair<string, object>> enumerator2 = tags.GetEnumerator();
				if (enumerator2.MoveNext())
				{
					activity._tags = new TagsLinkedList(enumerator2);
				}
			}
			if (samplerTags != null)
			{
				if (activity._tags == null)
				{
					activity._tags = new TagsLinkedList(samplerTags);
				}
				else
				{
					activity._tags.Add(samplerTags);
				}
			}
			if (parentId != null)
			{
				activity._parentId = parentId;
			}
			else if (parentContext != default(ActivityContext))
			{
				activity._traceId = parentContext.TraceId.ToString();
				if (parentContext.SpanId != default(ActivitySpanId))
				{
					activity._parentSpanId = parentContext.SpanId.ToString();
				}
				activity.ActivityTraceFlags = parentContext.TraceFlags;
				activity._parentTraceFlags = (byte)parentContext.TraceFlags;
				activity._traceState = parentContext.TraceState;
			}
			activity.IsAllDataRequested = request == ActivitySamplingResult.AllData || request == ActivitySamplingResult.AllDataAndRecorded;
			if (request == ActivitySamplingResult.AllDataAndRecorded)
			{
				activity.ActivityTraceFlags |= ActivityTraceFlags.Recorded;
			}
			if (startTime != default(DateTimeOffset))
			{
				activity.StartTimeUtc = startTime.UtcDateTime;
			}
			if (startIt)
			{
				activity.Start();
			}
			return activity;
		}

		private void GenerateW3CId()
		{
			if (_traceId == null && !TrySetTraceIdFromParent())
			{
				_traceId = (TraceIdGenerator?.Invoke() ?? ActivityTraceId.CreateRandom()).ToHexString();
			}
			if (!W3CIdFlagsSet)
			{
				TrySetTraceFlagsFromParent();
			}
			_spanId = ActivitySpanId.CreateRandom().ToHexString();
		}

		private static void NotifyError(Exception exception)
		{
			try
			{
				throw exception;
			}
			catch
			{
			}
		}

		private string GenerateHierarchicalId()
		{
			if (Parent != null)
			{
				return AppendSuffix(Parent.Id, Interlocked.Increment(ref Parent._currentChildId).ToString(), '.');
			}
			if (ParentId != null)
			{
				string text = ((ParentId[0] == '|') ? ParentId : ("|" + ParentId));
				char c = text[text.Length - 1];
				if (c != '.' && c != '_')
				{
					text += ".";
				}
				return AppendSuffix(text, Interlocked.Increment(ref s_currentRootId).ToString("x"), '_');
			}
			return GenerateRootId();
		}

		private string GetRootId(string id)
		{
			if (IdFormat == ActivityIdFormat.W3C)
			{
				return id.Substring(3, 32);
			}
			int num = id.IndexOf('.');
			if (num < 0)
			{
				num = id.Length;
			}
			int num2 = ((id[0] == '|') ? 1 : 0);
			return id.Substring(num2, num - num2);
		}

		private string AppendSuffix(string parentId, string suffix, char delimiter)
		{
			if (parentId.Length + suffix.Length < 1024)
			{
				return parentId + suffix + delimiter;
			}
			int num = 1015;
			while (num > 1 && parentId[num - 1] != '.' && parentId[num - 1] != '_')
			{
				num--;
			}
			if (num == 1)
			{
				return GenerateRootId();
			}
			string text = ((int)GetRandomNumber()).ToString("x8");
			return parentId.Substring(0, num) + text + "#";
		}

		private unsafe static long GetRandomNumber()
		{
			Guid guid = Guid.NewGuid();
			return *(long*)(&guid);
		}

		private static bool ValidateSetCurrent(Activity activity)
		{
			bool flag = activity == null || (activity.Id != null && !activity.IsFinished);
			if (!flag)
			{
				NotifyError(new InvalidOperationException(System.SR.ActivityNotRunning));
			}
			return flag;
		}

		private bool TrySetTraceIdFromParent()
		{
			if (Parent != null && Parent.IdFormat == ActivityIdFormat.W3C)
			{
				_traceId = Parent.TraceId.ToHexString();
			}
			else if (_parentId != null && IsW3CId(_parentId))
			{
				try
				{
					_traceId = ActivityTraceId.CreateFromString(MemoryExtensions.AsSpan(_parentId, 3, 32)).ToHexString();
				}
				catch
				{
				}
			}
			return _traceId != null;
		}

		private void TrySetTraceFlagsFromParent()
		{
			if (W3CIdFlagsSet)
			{
				return;
			}
			if (Parent != null)
			{
				ActivityTraceFlags = Parent.ActivityTraceFlags;
			}
			else if (_parentId != null && IsW3CId(_parentId))
			{
				if (System.HexConverter.IsHexLowerChar(_parentId[53]) && System.HexConverter.IsHexLowerChar(_parentId[54]))
				{
					_w3CIdFlags = (byte)(ActivityTraceId.HexByteFromChars(_parentId[53], _parentId[54]) | 0x80);
				}
				else
				{
					_w3CIdFlags = 128;
				}
			}
		}

		private static void SetCurrent(Activity activity)
		{
			s_current.Value = activity;
		}

		internal static DateTime GetUtcNow()
		{
			return DateTime.UtcNow;
		}

		private static string GenerateRootId()
		{
			return "|" + Interlocked.Increment(ref s_currentRootId).ToString("x") + s_uniqSuffix;
		}
	}
	[Flags]
	public enum ActivityTraceFlags
	{
		None = 0,
		Recorded = 1
	}
	public enum ActivityIdFormat
	{
		Unknown,
		Hierarchical,
		W3C
	}
	public readonly struct ActivityTraceId : IEquatable<ActivityTraceId>
	{
		private readonly string _hexString;

		internal ActivityTraceId(string hexString)
		{
			_hexString = hexString;
		}

		public static ActivityTraceId CreateRandom()
		{
			Span<byte> span = stackalloc byte[16];
			SetToRandomBytes(span);
			return CreateFromBytes(span);
		}

		public static ActivityTraceId CreateFromBytes(ReadOnlySpan<byte> idData)
		{
			if (idData.Length != 16)
			{
				throw new ArgumentOutOfRangeException("idData");
			}
			return new ActivityTraceId(System.HexConverter.ToString(idData, System.HexConverter.Casing.Lower));
		}

		public static ActivityTraceId CreateFromUtf8String(ReadOnlySpan<byte> idData)
		{
			return new ActivityTraceId(idData);
		}

		public static ActivityTraceId CreateFromString(ReadOnlySpan<char> idData)
		{
			if (idData.Length != 32 || !IsLowerCaseHexAndNotAllZeros(idData))
			{
				throw new ArgumentOutOfRangeException("idData");
			}
			return new ActivityTraceId(idData.ToString());
		}

		public string ToHexString()
		{
			return _hexString ?? "00000000000000000000000000000000";
		}

		public override string ToString()
		{
			return ToHexString();
		}

		public static bool operator ==(ActivityTraceId traceId1, ActivityTraceId traceId2)
		{
			return traceId1._hexString == traceId2._hexString;
		}

		public static bool operator !=(ActivityTraceId traceId1, ActivityTraceId traceId2)
		{
			return traceId1._hexString != traceId2._hexString;
		}

		public bool Equals(ActivityTraceId traceId)
		{
			return _hexString == traceId._hexString;
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			if (obj is ActivityTraceId activityTraceId)
			{
				return _hexString == activityTraceId._hexString;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return ToHexString().GetHashCode();
		}

		private ActivityTraceId(ReadOnlySpan<byte> idData)
		{
			if (idData.Length != 32)
			{
				throw new ArgumentOutOfRangeException("idData");
			}
			Span<ulong> span = stackalloc ulong[2];
			if (!Utf8Parser.TryParse(idData.Slice(0, 16), out span[0], out int bytesConsumed, 'x'))
			{
				_hexString = CreateRandom()._hexString;
				return;
			}
			if (!Utf8Parser.TryParse(idData.Slice(16, 16), out span[1], out bytesConsumed, 'x'))
			{
				_hexString = CreateRandom()._hexString;
				return;
			}
			if (BitConverter.IsLittleEndian)
			{
				span[0] = BinaryPrimitives.ReverseEndianness(span[0]);
				span[1] = BinaryPrimitives.ReverseEndianness(span[1]);
			}
			_hexString = System.HexConverter.ToString(MemoryMarshal.AsBytes(span), System.HexConverter.Casing.Lower);
		}

		public void CopyTo(Span<byte> destination)
		{
			SetSpanFromHexChars(MemoryExtensions.AsSpan(ToHexString()), destination);
		}

		internal static void SetToRandomBytes(Span<byte> outBytes)
		{
			RandomNumberGenerator current = RandomNumberGenerator.Current;
			Unsafe.WriteUnaligned(ref outBytes[0], current.Next());
			if (outBytes.Length == 16)
			{
				Unsafe.WriteUnaligned(ref outBytes[8], current.Next());
			}
		}

		internal static void SetSpanFromHexChars(ReadOnlySpan<char> charData, Span<byte> outBytes)
		{
			for (int i = 0; i < outBytes.Length; i++)
			{
				outBytes[i] = HexByteFromChars(charData[i * 2], charData[i * 2 + 1]);
			}
		}

		internal static byte HexByteFromChars(char char1, char char2)
		{
			int num = System.HexConverter.FromLowerChar(char1);
			int num2 = System.HexConverter.FromLowerChar(char2);
			if ((num | num2) == 255)
			{
				throw new ArgumentOutOfRangeException("idData");
			}
			return (byte)((num << 4) | num2);
		}

		internal static bool IsLowerCaseHexAndNotAllZeros(ReadOnlySpan<char> idData)
		{
			bool result = false;
			for (int i = 0; i < idData.Length; i++)
			{
				char c = idData[i];
				if (!System.HexConverter.IsHexLowerChar(c))
				{
					return false;
				}
				if (c != '0')
				{
					result = true;
				}
			}
			return result;
		}
	}
	public readonly struct ActivitySpanId : IEquatable<ActivitySpanId>
	{
		private readonly string _hexString;

		internal ActivitySpanId(string hexString)
		{
			_hexString = hexString;
		}

		public unsafe static ActivitySpanId CreateRandom()
		{
			ulong num = default(ulong);
			ActivityTraceId.SetToRandomBytes(new Span<byte>(&num, 8));
			return new ActivitySpanId(System.HexConverter.ToString(new ReadOnlySpan<byte>(&num, 8), System.HexConverter.Casing.Lower));
		}

		public static ActivitySpanId CreateFromBytes(ReadOnlySpan<byte> idData)
		{
			if (idData.Length != 8)
			{
				throw new ArgumentOutOfRangeException("idData");
			}
			return new ActivitySpanId(System.HexConverter.ToString(idData, System.HexConverter.Casing.Lower));
		}

		public static ActivitySpanId CreateFromUtf8String(ReadOnlySpan<byte> idData)
		{
			return new ActivitySpanId(idData);
		}

		public static ActivitySpanId CreateFromString(ReadOnlySpan<char> idData)
		{
			if (idData.Length != 16 || !ActivityTraceId.IsLowerCaseHexAndNotAllZeros(idData))
			{
				throw new ArgumentOutOfRangeException("idData");
			}
			return new ActivitySpanId(idData.ToString());
		}

		public string ToHexString()
		{
			return _hexString ?? "0000000000000000";
		}

		public override string ToString()
		{
			return ToHexString();
		}

		public static bool operator ==(ActivitySpanId spanId1, ActivitySpanId spandId2)
		{
			return spanId1._hexString == spandId2._hexString;
		}

		public static bool operator !=(ActivitySpanId spanId1, ActivitySpanId spandId2)
		{
			return spanId1._hexString != spandId2._hexString;
		}

		public bool Equals(ActivitySpanId spanId)
		{
			return _hexString == spanId._hexString;
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			if (obj is ActivitySpanId activitySpanId)
			{
				return _hexString == activitySpanId._hexString;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return ToHexString().GetHashCode();
		}

		private unsafe ActivitySpanId(ReadOnlySpan<byte> idData)
		{
			if (idData.Length != 16)
			{
				throw new ArgumentOutOfRangeException("idData");
			}
			if (!Utf8Parser.TryParse(idData, out ulong value, out int _, 'x'))
			{
				_hexString = CreateRandom()._hexString;
				return;
			}
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			_hexString = System.HexConverter.ToString(new ReadOnlySpan<byte>(&value, 8), System.HexConverter.Casing.Lower);
		}

		public void CopyTo(Span<byte> destination)
		{
			ActivityTraceId.SetSpanFromHexChars(MemoryExtensions.AsSpan(ToHexString()), destination);
		}
	}
	public enum ActivityStatusCode
	{
		Unset,
		Ok,
		Error
	}
	public class ActivityTagsCollection : IDictionary<string, object?>, ICollection<KeyValuePair<string, object?>>, IEnumerable<KeyValuePair<string, object?>>, IEnumerable
	{
		public struct Enumerator : IEnumerator<KeyValuePair<string, object?>>, IEnumerator, IDisposable
		{
			private List<KeyValuePair<string, object>>.Enumerator _enumerator;

			public KeyValuePair<string, object?> Current => _enumerator.Current;

			object IEnumerator.Current => ((IEnumerator)_enumerator).Current;

			internal Enumerator(List<KeyValuePair<string, object>> list)
			{
				_enumerator = list.GetEnumerator();
			}

			public void Dispose()
			{
				_enumerator.Dispose();
			}

			public bool MoveNext()
			{
				return _enumerator.MoveNext();
			}

			void IEnumerator.Reset()
			{
				((IEnumerator)_enumerator).Reset();
			}
		}

		private List<KeyValuePair<string, object>> _list = new List<KeyValuePair<string, object>>();

		public object? this[string key]
		{
			get
			{
				int num = FindIndex(key);
				if (num >= 0)
				{
					return _list[num].Value;
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = FindIndex(key);
				if (value == null)
				{
					if (num >= 0)
					{
						_list.RemoveAt(num);
					}
				}
				else if (num >= 0)
				{
					_list[num] = new KeyValuePair<string, object>(key, value);
				}
				else
				{
					_list.Add(new KeyValuePair<string, object>(key, value));
				}
			}
		}

		public ICollection<string> Keys
		{
			get
			{
				List<string> list = new List<string>(_list.Count);
				foreach (KeyValuePair<string, object> item in _list)
				{
					list.Add(item.Key);
				}
				return list;
			}
		}

		public ICollection<object?> Values
		{
			get
			{
				List<object> list = new List<object>(_list.Count);
				foreach (KeyValuePair<string, object> item in _list)
				{
					list.Add(item.Value);
				}
				return list;
			}
		}

		public bool IsReadOnly => false;

		public int Count => _list.Count;

		public ActivityTagsCollection()
		{
		}

		public ActivityTagsCollection(IEnumerable<KeyValuePair<string, object?>> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			foreach (KeyValuePair<string, object> item in list)
			{
				if (item.Key != null)
				{
					this[item.Key] = item.Value;
				}
			}
		}

		public void Add(string key, object? value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = FindIndex(key);
			if (num >= 0)
			{
				throw new InvalidOperationException(System.SR.Format(System.SR.KeyAlreadyExist, key));
			}
			_list.Add(new KeyValuePair<string, object>(key, value));
		}

		public void Add(KeyValuePair<string, object?> item)
		{
			if (item.Key == null)
			{
				throw new ArgumentNullException("item");
			}
			int num = FindIndex(item.Key);
			if (num >= 0)
			{
				throw new InvalidOperationException(System.SR.Format(System.SR.KeyAlreadyExist, item.Key));
			}
			_list.Add(item);
		}

		public void Clear()
		{
			_list.Clear();
		}

		public bool Contains(KeyValuePair<string, object?> item)
		{
			return _list.Contains(item);
		}

		public bool ContainsKey(string key)
		{
			return FindIndex(key) >= 0;
		}

		public void CopyTo(KeyValuePair<string, object?>[] array, int arrayIndex)
		{
			_list.CopyTo(array, arrayIndex);
		}

		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			return new Enumerator(_list);
		}

		public Enumerator GetEnumerator()
		{
			return new Enumerator(_list);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Enumerator(_list);
		}

		public bool Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = FindIndex(key);
			if (num >= 0)
			{
				_list.RemoveAt(num);
				return true;
			}
			return false;
		}

		public bool Remove(KeyValuePair<string, object?> item)
		{
			return _list.Remove(item);
		}

		public bool TryGetValue(string key, out object? value)
		{
			int num = FindIndex(key);
			if (num >= 0)
			{
				value = _list[num].Value;
				return true;
			}
			value = null;
			return false;
		}

		private int FindIndex(string key)
		{
			for (int i = 0; i < _list.Count; i++)
			{
				if (_list[i].Key == key)
				{
					return i;
				}
			}
			return -1;
		}
	}
	public readonly struct ActivityContext : IEquatable<ActivityContext>
	{
		public ActivityTraceId TraceId { get; }

		public ActivitySpanId SpanId { get; }

		public ActivityTraceFlags TraceFlags { get; }

		public string? TraceState { get; }

		public bool IsRemote { get; }

		public ActivityContext(ActivityTraceId traceId, ActivitySpanId spanId, ActivityTraceFlags traceFlags, string? traceState = null, bool isRemote = false)
		{
			TraceId = traceId;
			SpanId = spanId;
			TraceFlags = traceFlags;
			TraceState = traceState;
			IsRemote = isRemote;
		}

		public static bool TryParse(string? traceParent, string? traceState, out ActivityContext context)
		{
			if (traceParent == null)
			{
				context = default(ActivityContext);
				return false;
			}
			return Activity.TryConvertIdToContext(traceParent, traceState, out context);
		}

		public static ActivityContext Parse(string traceParent, string? traceState)
		{
			if (traceParent == null)
			{
				throw new ArgumentNullException("traceParent");
			}
			if (!Activity.TryConvertIdToContext(traceParent, traceState, out var context))
			{
				throw new ArgumentException(System.SR.InvalidTraceParent);
			}
			return context;
		}

		public bool Equals(ActivityContext value)
		{
			if (SpanId.Equals(value.SpanId) && TraceId.Equals(value.TraceId) && TraceFlags == value.TraceFlags && TraceState == value.TraceState)
			{
				return IsRemote == value.IsRemote;
			}
			return false;
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			if (!(obj is ActivityContext value))
			{
				return false;
			}
			return Equals(value);
		}

		public static bool operator ==(ActivityContext left, ActivityContext right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ActivityContext left, ActivityContext right)
		{
			return !(left == right);
		}

		public override int GetHashCode()
		{
			if (this == default(ActivityContext))
			{
				return 0;
			}
			int num = 5381;
			num = (num << 5) + num + TraceId.GetHashCode();
			num = (num << 5) + num + SpanId.GetHashCode();
			num = (int)((num << 5) + num + TraceFlags);
			return (num << 5) + num + ((TraceState != null) ? TraceState.GetHashCode() : 0);
		}
	}
	public readonly struct ActivityCreationOptions<T>
	{
		private readonly ActivityTagsCollection _samplerTags;

		private readonly ActivityContext _context;

		public ActivitySource Source { get; }

		public string Name { get; }

		public ActivityKind Kind { get; }

		public T Parent { get; }

		public IEnumerable<KeyValuePair<string, object?>>? Tags { get; }

		public IEnumerable<ActivityLink>? Links { get; }

		public ActivityTagsCollection SamplingTags
		{
			get
			{
				if (_samplerTags == null)
				{
					Unsafe.AsRef(in _samplerTags) = new ActivityTagsCollection();
				}
				return _samplerTags;
			}
		}

		public ActivityTraceId TraceId
		{
			get
			{
				if (Parent is ActivityContext && IdFormat == ActivityIdFormat.W3C && _context == default(ActivityContext))
				{
					ActivityTraceId traceId = Activity.TraceIdGenerator?.Invoke() ?? ActivityTraceId.CreateRandom();
					Unsafe.AsRef(in _context) = new ActivityContext(traceId, default(ActivitySpanId), ActivityTraceFlags.None);
				}
				return _context.TraceId;
			}
		}

		internal ActivityIdFormat IdFormat { get; }

		internal ActivityCreationOptions(ActivitySource source, string name, T parent, ActivityKind kind, IEnumerable<KeyValuePair<string, object>> tags, IEnumerable<ActivityLink> links, ActivityIdFormat idFormat)
		{
			Source = source;
			Name = name;
			Kind = kind;
			Parent = parent;
			Tags = tags;
			Links = links;
			IdFormat = idFormat;
			if (IdFormat == ActivityIdFormat.Unknown && Activity.ForceDefaultIdFormat)
			{
				IdFormat = Activity.DefaultIdFormat;
			}
			_samplerTags = null;
			if (parent is ActivityContext activityContext && activityContext != default(ActivityContext))
			{
				_context = activityContext;
				if (IdFormat == ActivityIdFormat.Unknown)
				{
					IdFormat = ActivityIdFormat.W3C;
				}
			}
			else if (parent is string text && text != null)
			{
				if (IdFormat != ActivityIdFormat.Hierarchical)
				{
					if (ActivityContext.TryParse(text, null, out _context))
					{
						IdFormat = ActivityIdFormat.W3C;
					}
					if (IdFormat == ActivityIdFormat.Unknown)
					{
						IdFormat = ActivityIdFormat.Hierarchical;
					}
				}
				else
				{
					_context = default(ActivityContext);
				}
			}
			else
			{
				_context = default(ActivityContext);
				if (IdFormat == ActivityIdFormat.Unknown)
				{
					IdFormat = ((Activity.Current != null) ? Activity.Current.IdFormat : Activity.DefaultIdFormat);
				}
			}
		}

		internal ActivityTagsCollection GetSamplingTags()
		{
			return _samplerTags;
		}

		internal ActivityContext GetContext()
		{
			return _context;
		}
	}
	public enum ActivitySamplingResult
	{
		None,
		PropagationData,
		AllData,
		AllDataAndRecorded
	}
	public readonly struct ActivityEvent
	{
		private static readonly ActivityTagsCollection s_emptyTags = new ActivityTagsCollection();

		public string Name { get; }

		public DateTimeOffset Timestamp { get; }

		public IEnumerable<KeyValuePair<string, object?>> Tags { get; }

		public ActivityEvent(string name)
			: this(name, DateTimeOffset.UtcNow, s_emptyTags)
		{
		}

		public ActivityEvent(string name, DateTimeOffset timestamp = default(DateTimeOffset), ActivityTagsCollection? tags = null)
		{
			Name = name ?? string.Empty;
			Tags = tags ?? s_emptyTags;
			Timestamp = ((timestamp != default(DateTimeOffset)) ? timestamp : DateTimeOffset.UtcNow);
		}
	}
	public enum ActivityKind
	{
		Internal,
		Server,
		Client,
		Producer,
		Consumer
	}
	public readonly struct ActivityLink : IEquatable<ActivityLink>
	{
		public ActivityContext Context { get; }

		public IEnumerable<KeyValuePair<string, object?>>? Tags { get; }

		public ActivityLink(ActivityContext context, ActivityTagsCollection? tags = null)
		{
			Context = context;
			Tags = tags;
		}

		public override bool Equals([NotNullWhen(true)] object? obj)
		{
			if (obj is ActivityLink value)
			{
				return Equals(value);
			}
			return false;
		}

		public bool Equals(ActivityLink value)
		{
			if (Context == value.Context)
			{
				return value.Tags == Tags;
			}
			return false;
		}

		public static bool operator ==(ActivityLink left, ActivityLink right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ActivityLink left, ActivityLink right)
		{
			return !left.Equals(right);
		}

		public override int GetHashCode()
		{
			if (this == default(ActivityLink))
			{
				return 0;
			}
			int num = 5381;
			num = (num << 5) + num + Context.GetHashCode();
			if (Tags != null)
			{
				foreach (KeyValuePair<string, object> tag in Tags)
				{
					num = (num << 5) + num + tag.Key.GetHashCode();
					if (tag.Value != null)
					{
						num = (num << 5) + num + tag.Value.GetHashCode();
					}
				}
			}
			return num;
		}
	}
	public delegate ActivitySamplingResult SampleActivity<T>(ref ActivityCreationOptions<T> options);
	public sealed class ActivityListener : IDisposable
	{
		public Action<Activity>? ActivityStarted { get; set; }

		public Action<Activity>? ActivityStopped { get; set; }

		public Func<ActivitySource, bool>? ShouldListenTo { get; set; }

		public SampleActivity<string>? SampleUsingParentId { get; set; }

		public SampleActivity<ActivityContext>? Sample { get; set; }

		public void Dispose()
		{
			ActivitySource.DetachListener(this);
		}
	}
	public sealed class ActivitySource : IDisposable
	{
		internal delegate void Function<T, TParent>(T item, ref ActivityCreationOptions<TParent> data, ref ActivitySamplingResult samplingResult, ref ActivityCreationOptions<ActivityContext> dataWithContext);

		private static readonly SynchronizedList<ActivitySource> s_activeSources = new SynchronizedList<ActivitySource>();

		private static readonly SynchronizedList<ActivityListener> s_allListeners = new SynchronizedList<ActivityListener>();

		private SynchronizedList<ActivityListener> _listeners;

		public string Name { get; }

		public string? Version { get; }

		public ActivitySource(string name, string? version = "")
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Name = name;
			Version = version;
			s_activeSources.Add(this);
			if (s_allListeners.Count > 0)
			{
				s_allListeners.EnumWithAction(delegate(ActivityListener listener, object source)
				{
					Func<ActivitySource, bool> shouldListenTo = listener.ShouldListenTo;
					if (shouldListenTo != null)
					{
						ActivitySource activitySource = (ActivitySource)source;
						if (shouldListenTo(activitySource))
						{
							activitySource.AddListener(listener);
						}
					}
				}, this);
			}
			GC.KeepAlive(DiagnosticSourceEventSource.Log);
		}

		public bool HasListeners()
		{
			SynchronizedList<ActivityListener> listeners = _listeners;
			if (listeners != null)
			{
				return listeners.Count > 0;
			}
			return false;
		}

		public Activity? CreateActivity(string name, ActivityKind kind)
		{
			return CreateActivity(name, kind, default(ActivityContext), null, null, null, default(DateTimeOffset), startIt: false);
		}

		public Activity? CreateActivity(string name, ActivityKind kind, ActivityContext parentContext, IEnumerable<KeyValuePair<string, object?>>? tags = null, IEnumerable<ActivityLink>? links = null, ActivityIdFormat idFormat = ActivityIdFormat.Unknown)
		{
			return CreateActivity(name, kind, parentContext, null, tags, links, default(DateTimeOffset), startIt: false, idFormat);
		}

		public Activity? CreateActivity(string name, ActivityKind kind, string parentId, IEnumerable<KeyValuePair<string, object?>>? tags = null, IEnumerable<ActivityLink>? links = null, ActivityIdFormat idFormat = ActivityIdFormat.Unknown)
		{
			return CreateActivity(name, kind, default(ActivityContext), parentId, tags, links, default(DateTimeOffset), startIt: false, idFormat);
		}

		public Activity? StartActivity([CallerMemberName] string name = "", ActivityKind kind = ActivityKind.Internal)
		{
			return CreateActivity(name, kind, default(ActivityContext), null, null, null, default(DateTimeOffset));
		}

		public Activity? StartActivity(string name, ActivityKind kind, ActivityContext parentContext, IEnumerable<KeyValuePair<string, object?>>? tags = null, IEnumerable<ActivityLink>? links = null, DateTimeOffset startTime = default(DateTimeOffset))
		{
			return CreateActivity(name, kind, parentContext, null, tags, links, startTime);
		}

		public Activity? StartActivity(string name, ActivityKind kind, string parentId, IEnumerable<KeyValuePair<string, object?>>? tags = null, IEnumerable<ActivityLink>? links = null, DateTimeOffset startTime = default(DateTimeOffset))
		{
			return CreateActivity(name, kind, default(ActivityContext), parentId, tags, links, startTime);
		}

		public Activity? StartActivity(ActivityKind kind, ActivityContext parentContext = default(ActivityContext), IEnumerable<KeyValuePair<string, object?>>? tags = null, IEnumerable<ActivityLink>? links = null, DateTimeOffset startTime = default(DateTimeOffset), [CallerMemberName] string name = "")
		{
			return CreateActivity(name, kind, parentContext, null, tags, links, startTime);
		}

		private Activity CreateActivity(string name, ActivityKind kind, ActivityContext context, string parentId, IEnumerable<KeyValuePair<string, object>> tags, IEnumerable<ActivityLink> links, DateTimeOffset startTime, bool startIt = true, ActivityIdFormat idFormat = ActivityIdFormat.Unknown)
		{
			SynchronizedList<ActivityListener> listeners = _listeners;
			if (listeners == null || listeners.Count == 0)
			{
				return null;
			}
			Activity result = null;
			ActivitySamplingResult samplingResult = ActivitySamplingResult.None;
			ActivityTagsCollection activityTagsCollection;
			if (parentId != null)
			{
				ActivityCreationOptions<string> activityCreationOptions = default(ActivityCreationOptions<string>);
				ActivityCreationOptions<ActivityContext> dataWithContext = default(ActivityCreationOptions<ActivityContext>);
				activityCreationOptions = new ActivityCreationOptions<string>(this, name, parentId, kind, tags, links, idFormat);
				if (activityCreationOptions.IdFormat == ActivityIdFormat.W3C)
				{
					dataWithContext = new ActivityCreationOptions<ActivityContext>(this, name, activityCreationOptions.GetContext(), kind, tags, links, ActivityIdFormat.W3C);
				}
				listeners.EnumWithFunc(delegate(ActivityListener listener, ref ActivityCreationOptions<string> reference, ref ActivitySamplingResult reference2, ref ActivityCreationOptions<ActivityContext> options)
				{
					SampleActivity<string> sampleUsingParentId = listener.SampleUsingParentId;
					if (sampleUsingParentId != null)
					{
						ActivitySamplingResult activitySamplingResult = sampleUsingParentId(ref reference);
						if (activitySamplingResult > reference2)
						{
							reference2 = activitySamplingResult;
						}
					}
					else if (reference.IdFormat == ActivityIdFormat.W3C)
					{
						SampleActivity<ActivityContext> sample = listener.Sample;
						if (sample != null)
						{
							ActivitySamplingResult activitySamplingResult2 = sample(ref options);
							if (activitySamplingResult2 > reference2)
							{
								reference2 = activitySamplingResult2;
							}
						}
					}
				}, ref activityCreationOptions, ref samplingResult, ref dataWithContext);
				if (context == default(ActivityContext))
				{
					if (activityCreationOptions.GetContext() != default(ActivityContext))
					{
						context = activityCreationOptions.GetContext();
						parentId = null;
					}
					else if (dataWithContext.GetContext() != default(ActivityContext))
					{
						context = dataWithContext.GetContext();
						parentId = null;
					}
				}
				activityTagsCollection = activityCreationOptions.GetSamplingTags();
				ActivityTagsCollection samplingTags = dataWithContext.GetSamplingTags();
				if (samplingTags != null)
				{
					if (activityTagsCollection == null)
					{
						activityTagsCollection = samplingTags;
					}
					else
					{
						foreach (KeyValuePair<string, object?> item in samplingTags)
						{
							activityTagsCollection.Add(item);
						}
					}
				}
				idFormat = activityCreationOptions.IdFormat;
			}
			else
			{
				bool flag = context == default(ActivityContext) && Activity.Current != null;
				ActivityCreationOptions<ActivityContext> data = new ActivityCreationOptions<ActivityContext>(this, name, flag ? Activity.Current.Context : context, kind, tags, links, idFormat);
				listeners.EnumWithFunc(delegate(ActivityListener listener, ref ActivityCreationOptions<ActivityContext> options, ref ActivitySamplingResult reference, ref ActivityCreationOptions<ActivityContext> unused)
				{
					SampleActivity<ActivityContext> sample = listener.Sample;
					if (sample != null)
					{
						ActivitySamplingResult activitySamplingResult = sample(ref options);
						if (activitySamplingResult > reference)
						{
							reference = activitySamplingResult;
						}
					}
				}, ref data, ref samplingResult, ref data);
				if (!flag)
				{
					context = data.GetContext();
				}
				activityTagsCollection = data.GetSamplingTags();
				idFormat = data.IdFormat;
			}
			if (samplingResult != ActivitySamplingResult.None)
			{
				result = Activity.Create(this, name, kind, parentId, context, tags, links, startTime, activityTagsCollection, samplingResult, startIt, idFormat);
			}
			return result;
		}

		public void Dispose()
		{
			_listeners = null;
			s_activeSources.Remove(this);
		}

		public static void AddActivityListener(ActivityListener listener)
		{
			if (listener == null)
			{
				throw new ArgumentNullException("listener");
			}
			if (!s_allListeners.AddIfNotExist(listener))
			{
				return;
			}
			s_activeSources.EnumWithAction(delegate(ActivitySource source, object obj)
			{
				Func<ActivitySource, bool> shouldListenTo = ((ActivityListener)obj).ShouldListenTo;
				if (shouldListenTo != null && shouldListenTo(source))
				{
					source.AddListener((ActivityListener)obj);
				}
			}, listener);
		}

		internal void AddListener(ActivityListener listener)
		{
			if (_listeners == null)
			{
				Interlocked.CompareExchange(ref _listeners, new SynchronizedList<ActivityListener>(), null);
			}
			_listeners.AddIfNotExist(listener);
		}

		internal static void DetachListener(ActivityListener listener)
		{
			s_allListeners.Remove(listener);
			s_activeSources.EnumWithAction(delegate(ActivitySource source, object obj)
			{
				source._listeners?.Remove((ActivityListener)obj);
			}, listener);
		}

		internal void NotifyActivityStart(Activity activity)
		{
			SynchronizedList<ActivityListener> listeners = _listeners;
			if (listeners != null && listeners.Count > 0)
			{
				listeners.EnumWithAction(delegate(ActivityListener listener, object obj)
				{
					listener.ActivityStarted?.Invoke((Activity)obj);
				}, activity);
			}
		}

		internal void NotifyActivityStop(Activity activity)
		{
			SynchronizedList<ActivityListener> listeners = _listeners;
			if (listeners != null && listeners.Count > 0)
			{
				listeners.EnumWithAction(delegate(ActivityListener listener, object obj)
				{
					listener.ActivityStopped?.Invoke((Activity)obj);
				}, activity);
			}
		}
	}
	internal sealed class SynchronizedList<T>
	{
		private readonly List<T> _list;

		private uint _version;

		public int Count => _list.Count;

		public SynchronizedList()
		{
			_list = new List<T>();
		}

		public void Add(T item)
		{
			lock (_list)
			{
				_list.Add(item);
				_version++;
			}
		}

		public bool AddIfNotExist(T item)
		{
			lock (_list)
			{
				if (!_list.Contains(item))
				{
					_list.Add(item);
					_version++;
					return true;
				}
				return false;
			}
		}

		public bool Remove(T item)
		{
			lock (_list)
			{
				if (_list.Remove(item))
				{
					_version++;
					return true;
				}
				return false;
			}
		}

		public void EnumWithFunc<TParent>(ActivitySource.Function<T, TParent> func, ref ActivityCreationOptions<TParent> data, ref ActivitySamplingResult samplingResult, ref ActivityCreationOptions<ActivityContext> dataWithContext)
		{
			uint version = _version;
			int num = 0;
			while (num < _list.Count)
			{
				T item;
				lock (_list)
				{
					if (version != _version)
					{
						version = _version;
						num = 0;
						continue;
					}
					item = _list[num];
					num++;
				}
				func(item, ref data, ref samplingResult, ref dataWithContext);
			}
		}

		public void EnumWithAction(Action<T, object> action, object arg)
		{
			uint version = _version;
			int num = 0;
			while (num < _list.Count)
			{
				T arg2;
				lock (_list)
				{
					if (version != _version)
					{
						version = _version;
						num = 0;
						continue;
					}
					arg2 = _list[num];
					num++;
				}
				action(arg2, arg);
			}
		}
	}
	internal sealed class DiagNode<T>
	{
		public T Value;

		public DiagNode<T> Next;

		public DiagNode(T value)
		{
			Value = value;
		}
	}
	internal sealed class DiagLinkedList<T> : IEnumerable<T>, IEnumerable
	{
		private DiagNode<T> _first;

		private DiagNode<T> _last;

		public DiagNode<T> First => _first;

		public DiagLinkedList()
		{
		}

		public DiagLinkedList(T firstValue)
		{
			_last = (_first = new DiagNode<T>(firstValue));
		}

		public DiagLinkedList(IEnumerator<T> e)
		{
			_last = (_first = new DiagNode<T>(e.Current));
			while (e.MoveNext())
			{
				_last.Next = new DiagNode<T>(e.Current);
				_last = _last.Next;
			}
		}

		public void Clear()
		{
			lock (this)
			{
				_first = (_last = null);
			}
		}

		private void UnsafeAdd(DiagNode<T> newNode)
		{
			if (_first == null)
			{
				_first = (_last = newNode);
				return;
			}
			_last.Next = newNode;
			_last = newNode;
		}

		public void Add(T value)
		{
			DiagNode<T> newNode = new DiagNode<T>(value);
			lock (this)
			{
				UnsafeAdd(newNode);
			}
		}

		public bool AddIfNotExist(T value, Func<T, T, bool> compare)
		{
			lock (this)
			{
				for (DiagNode<T> diagNode = _first; diagNode != null; diagNode = diagNode.Next)
				{
					if (compare(value, diagNode.Value))
					{
						return false;
					}
				}
				DiagNode<T> newNode = new DiagNode<T>(value);
				UnsafeAdd(newNode);
				return true;
			}
		}

		public T Remove(T value, Func<T, T, bool> compare)
		{
			lock (this)
			{
				DiagNode<T> diagNode = _first;
				if (diagNode == null)
				{
					return default(T);
				}
				if (compare(diagNode.Value, value))
				{
					_first = diagNode.Next;
					if (_first == null)
					{
						_last = null;
					}
					return diagNode.Value;
				}
				for (DiagNode<T> next = diagNode.Next; next != null; next = next.Next)
				{
					if (compare(next.Value, value))
					{
						diagNode.Next = next.Next;
						if (_last == next)
						{
							_last = diagNode;
						}
						return next.Value;
					}
					diagNode = next;
				}
				return default(T);
			}
		}

		public void AddFront(T value)
		{
			DiagNode<T> diagNode = new DiagNode<T>(value);
			lock (this)
			{
				diagNode.Next = _first;
				_first = diagNode;
			}
		}

		public Enumerator<T> GetEnumerator()
		{
			return new Enumerator<T>(_first);
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	internal struct Enumerator<T> : IEnumerator<T>, IEnumerator, IDisposable
	{
		private DiagNode<T> _nextNode;

		[AllowNull]
		[MaybeNull]
		private T _currentItem;

		public T Current => _currentItem;

		object IEnumerator.Current => Current;

		public Enumerator(DiagNode<T> head)
		{
			_nextNode = head;
			_currentItem = default(T);
		}

		public bool MoveNext()
		{
			if (_nextNode == null)
			{
				_currentItem = default(T);
				return false;
			}
			_currentItem = _nextNode.Value;
			_nextNode = _nextNode.Next;
			return true;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		public void Dispose()
		{
		}
	}
	public abstract class DistributedContextPropagator
	{
		public delegate void PropagatorGetterCallback(object? carrier, string fieldName, out string? fieldValue, out IEnumerable<string>? fieldValues);

		public delegate void PropagatorSetterCallback(object? carrier, string fieldName, string fieldValue);

		private static DistributedContextPropagator s_current = CreateDefaultPropagator();

		internal const string TraceParent = "traceparent";

		internal const string RequestId = "Request-Id";

		internal const string TraceState = "tracestate";

		internal const string Baggage = "baggage";

		internal const string CorrelationContext = "Correlation-Context";

		internal const char Space = ' ';

		internal const char Tab = '\t';

		internal const char Comma = ',';

		internal const char Semicolon = ';';

		internal const string CommaWithSpace = ", ";

		internal static readonly char[] s_trimmingSpaceCharacters = new char[2] { ' ', '\t' };

		public abstract IReadOnlyCollection<string> Fields { get; }

		public static DistributedContextPropagator Current
		{
			get
			{
				return s_current;
			}
			set
			{
				s_current = value ?? throw new ArgumentNullException("value");
			}
		}

		public abstract void Inject(Activity? activity, object? carrier, PropagatorSetterCallback? setter);

		public abstract void ExtractTraceIdAndState(object? carrier, PropagatorGetterCallback? getter, out string? traceId, out string? traceState);

		public abstract IEnumerable<KeyValuePair<string, string?>>? ExtractBaggage(object? carrier, PropagatorGetterCallback? getter);

		public static DistributedContextPropagator CreateDefaultPropagator()
		{
			return LegacyPropagator.Instance;
		}

		public static DistributedContextPropagator CreatePassThroughPropagator()
		{
			return PassThroughPropagator.Instance;
		}

		public static DistributedContextPropagator CreateNoOutputPropagator()
		{
			return NoOutputPropagator.Instance;
		}

		internal static void InjectBaggage(object carrier, IEnumerable<KeyValuePair<string, string>> baggage, PropagatorSetterCallback setter)
		{
			using IEnumerator<KeyValuePair<string, string>> enumerator = baggage.GetEnumerator();
			if (enumerator.MoveNext())
			{
				StringBuilder stringBuilder = new StringBuilder();
				do
				{
					KeyValuePair<string, string> current = enumerator.Current;
					stringBuilder.Append(WebUtility.UrlEncode(current.Key)).Append('=').Append(WebUtility.UrlEncode(current.Value))
						.Append(", ");
				}
				while (enumerator.MoveNext());
				setter(carrier, "Correlation-Context", stringBuilder.ToString(0, stringBuilder.Length - 2));
			}
		}
	}
	internal sealed class LegacyPropagator : DistributedContextPropagator
	{
		internal static DistributedContextPropagator Instance { get; } = new LegacyPropagator();

		public override IReadOnlyCollection<string> Fields { get; } = new ReadOnlyCollection<string>(new string[5] { "traceparent", "Request-Id", "tracestate", "baggage", "Correlation-Context" });

		public override void Inject(Activity activity, object carrier, PropagatorSetterCallback setter)
		{
			if (activity == null || setter == null)
			{
				return;
			}
			string id = activity.Id;
			if (id == null)
			{
				return;
			}
			if (activity.IdFormat == ActivityIdFormat.W3C)
			{
				setter(carrier, "traceparent", id);
				if (!string.IsNullOrEmpty(activity.TraceStateString))
				{
					setter(carrier, "tracestate", activity.TraceStateString);
				}
			}
			else
			{
				setter(carrier, "Request-Id", id);
			}
			DistributedContextPropagator.InjectBaggage(carrier, activity.Baggage, setter);
		}

		public override void ExtractTraceIdAndState(object carrier, PropagatorGetterCallback getter, out string traceId, out string traceState)
		{
			if (getter == null)
			{
				traceId = null;
				traceState = null;
				return;
			}
			getter(carrier, "traceparent", out traceId, out var fieldValues);
			if (traceId == null)
			{
				getter(carrier, "Request-Id", out traceId, out fieldValues);
			}
			getter(carrier, "tracestate", out traceState, out fieldValues);
		}

		public override IEnumerable<KeyValuePair<string, string>> ExtractBaggage(object carrier, PropagatorGetterCallback getter)
		{
			if (getter == null)
			{
				return null;
			}
			getter(carrier, "baggage", out var fieldValue, out var fieldValues);
			IEnumerable<KeyValuePair<string, string>> baggage = null;
			if (fieldValue == null || !TryExtractBaggage(fieldValue, out baggage))
			{
				getter(carrier, "Correlation-Context", out fieldValue, out fieldValues);
				if (fieldValue != null)
				{
					TryExtractBaggage(fieldValue, out baggage);
				}
			}
			return baggage;
		}

		internal static bool TryExtractBaggage(string baggageString, out IEnumerable<KeyValuePair<string, string>> baggage)
		{
			baggage = null;
			List<KeyValuePair<string, string>> list = null;
			if (string.IsNullOrEmpty(baggageString))
			{
				return true;
			}
			int i = 0;
			while (true)
			{
				if (i < baggageString.Length && (baggageString[i] == ' ' || baggageString[i] == '\t'))
				{
					i++;
					continue;
				}
				if (i >= baggageString.Length)
				{
					break;
				}
				int num = i;
				for (; i < baggageString.Length && baggageString[i] != ' ' && baggageString[i] != '\t' && baggageString[i] != '='; i++)
				{
				}
				if (i >= baggageString.Length)
				{
					break;
				}
				int num2 = i;
				if (baggageString[i] != '=')
				{
					for (; i < baggageString.Length && (baggageString[i] == ' ' || baggageString[i] == '\t'); i++)
					{
					}
					if (i >= baggageString.Length || baggageString[i] != '=')
					{
						break;
					}
				}
				for (i++; i < baggageString.Length && (baggageString[i] == ' ' || baggageString[i] == '\t'); i++)
				{
				}
				if (i >= baggageString.Length)
				{
					break;
				}
				int num3 = i;
				for (; i < baggageString.Length && baggageString[i] != ' ' && baggageString[i] != '\t' && baggageString[i] != ',' && baggageString[i] != ';'; i++)
				{
				}
				if (num < num2 && num3 < i)
				{
					if (list == null)
					{
						list = new List<KeyValuePair<string, string>>();
					}
					list.Insert(0, new KeyValuePair<string, string>(WebUtility.UrlDecode(baggageString.Substring(num, num2 - num)).Trim(DistributedContextPropagator.s_trimmingSpaceCharacters), WebUtility.UrlDecode(baggageString.Substring(num3, i - num3)).Trim(DistributedContextPropagator.s_trimmingSpaceCharacters)));
				}
				for (; i < baggageString.Length && baggageString[i] != ','; i++)
				{
				}
				i++;
				if (i >= baggageString.Length)
				{
					break;
				}
			}
			baggage = list;
			return list != null;
		}
	}
	internal sealed class NoOutputPropagator : DistributedContextPropagator
	{
		internal static DistributedContextPropagator Instance { get; } = new NoOutputPropagator();

		public override IReadOnlyCollection<string> Fields { get; } = LegacyPropagator.Instance.Fields;

		public override void Inject(Activity activity, object carrier, PropagatorSetterCallback setter)
		{
		}

		public override void ExtractTraceIdAndState(object carrier, PropagatorGetterCallback getter, out string traceId, out string traceState)
		{
			LegacyPropagator.Instance.ExtractTraceIdAndState(carrier, getter, out traceId, out traceState);
		}

		public override IEnumerable<KeyValuePair<string, string>> ExtractBaggage(object carrier, PropagatorGetterCallback getter)
		{
			return LegacyPropagator.Instance.ExtractBaggage(carrier, getter);
		}
	}
	internal sealed class PassThroughPropagator : DistributedContextPropagator
	{
		internal static DistributedContextPropagator Instance { get; } = new PassThroughPropagator();

		public override IReadOnlyCollection<string> Fields { get; } = LegacyPropagator.Instance.Fields;

		public override void Inject(Activity activity, object carrier, PropagatorSetterCallback setter)
		{
			if (setter == null)
			{
				return;
			}
			GetRootId(out var parentId, out var traceState, out var isW3c, out var baggage);
			if (parentId != null)
			{
				setter(carrier, isW3c ? "traceparent" : "Request-Id", parentId);
				if (!string.IsNullOrEmpty(traceState))
				{
					setter(carrier, "tracestate", traceState);
				}
				if (baggage != null)
				{
					DistributedContextPropagator.InjectBaggage(carrier, baggage, setter);
				}
			}
		}

		public override void ExtractTraceIdAndState(object carrier, PropagatorGetterCallback getter, out string traceId, out string traceState)
		{
			LegacyPropagator.Instance.ExtractTraceIdAndState(carrier, getter, out traceId, out traceState);
		}

		public override IEnumerable<KeyValuePair<string, string>> ExtractBaggage(object carrier, PropagatorGetterCallback getter)
		{
			return LegacyPropagator.Instance.ExtractBaggage(carrier, getter);
		}

		private static void GetRootId(out string parentId, out string traceState, out bool isW3c, out IEnumerable<KeyValuePair<string, string>> baggage)
		{
			Activity activity = Activity.Current;
			while (true)
			{
				Activity activity2 = activity?.Parent;
				if (activity2 == null)
				{
					break;
				}
				activity = activity2;
			}
			traceState = activity?.TraceStateString;
			parentId = activity?.ParentId ?? activity?.Id;
			isW3c = parentId != null && Activity.TryConvertIdToContext(parentId, traceState, out var _);
			baggage = activity?.Baggage;
		}
	}
	internal sealed class RandomNumberGenerator
	{
		[ThreadStatic]
		private static RandomNumberGenerator t_random;

		private ulong _s0;

		private ulong _s1;

		private ulong _s2;

		private ulong _s3;

		public static RandomNumberGenerator Current
		{
			get
			{
				if (t_random == null)
				{
					t_random = new RandomNumberGenerator();
				}
				return t_random;
			}
		}

		public unsafe RandomNumberGenerator()
		{
			do
			{
				Guid guid = Guid.NewGuid();
				Guid guid2 = Guid.NewGuid();
				ulong* ptr = (ulong*)(&guid);
				ulong* ptr2 = (ulong*)(&guid2);
				_s0 = *ptr;
				_s1 = ptr[1];
				_s2 = *ptr2;
				_s3 = ptr2[1];
				_s0 = (_s0 & 0xFFFFFFFFFFFFFFFL) | (_s1 & 0xF000000000000000uL);
				_s2 = (_s2 & 0xFFFFFFFFFFFFFFFL) | (_s3 & 0xF000000000000000uL);
				_s1 = (_s1 & 0xFFFFFFFFFFFFFF3FuL) | (_s0 & 0xC0);
				_s3 = (_s3 & 0xFFFFFFFFFFFFFF3FuL) | (_s2 & 0xC0);
			}
			while ((_s0 | _s1 | _s2 | _s3) == 0L);
		}

		private ulong Rol64(ulong x, int k)
		{
			return (x << k) | (x >> 64 - k);
		}

		public long Next()
		{
			ulong result = Rol64(_s1 * 5, 7) * 9;
			ulong num = _s1 << 17;
			_s2 ^= _s0;
			_s3 ^= _s1;
			_s1 ^= _s2;
			_s0 ^= _s3;
			_s2 ^= num;
			_s3 = Rol64(_s3, 45);
			return (long)result;
		}
	}
	public struct TagList : IList<KeyValuePair<string, object?>>, ICollection<KeyValuePair<string, object?>>, IEnumerable<KeyValuePair<string, object?>>, IEnumerable, IReadOnlyList<KeyValuePair<string, object?>>, IReadOnlyCollection<KeyValuePair<string, object?>>
	{
		public struct Enumerator : IEnumerator<KeyValuePair<string, object?>>, IEnumerator, IDisposable
		{
			private TagList _tagList;

			private int _index;

			public KeyValuePair<string, object?> Current => _tagList[_index];

			object IEnumerator.Current => _tagList[_index];

			internal Enumerator(in TagList tagList)
			{
				_index = -1;
				_tagList = tagList;
			}

			public void Dispose()
			{
				_index = _tagList.Count;
			}

			public bool MoveNext()
			{
				_index++;
				return _index < _tagList.Count;
			}

			public void Reset()
			{
				_index = -1;
			}
		}

		internal KeyValuePair<string, object> Tag1;

		internal KeyValuePair<string, object> Tag2;

		internal KeyValuePair<string, object> Tag3;

		internal KeyValuePair<string, object> Tag4;

		internal KeyValuePair<string, object> Tag5;

		internal KeyValuePair<string, object> Tag6;

		internal KeyValuePair<string, object> Tag7;

		internal KeyValuePair<string, object> Tag8;

		private int _tagsCount;

		private KeyValuePair<string, object>[] _overflowTags;

		private const int OverflowAdditionalCapacity = 8;

		public readonly int Count => _tagsCount;

		public readonly bool IsReadOnly => false;

		public KeyValuePair<string, object?> this[int index]
		{
			readonly get
			{
				if ((uint)index >= (uint)_tagsCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (_overflowTags != null)
				{
					return _overflowTags[index];
				}
				return index switch
				{
					0 => Tag1, 
					1 => Tag2, 
					2 => Tag3, 
					3 => Tag4, 
					4 => Tag5, 
					5 => Tag6, 
					6 => Tag7, 
					7 => Tag8, 
					_ => default(KeyValuePair<string, object>), 
				};
			}
			set
			{
				if ((uint)index >= (uint)_tagsCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (_overflowTags != null)
				{
					_overflowTags[index] = value;
					return;
				}
				switch (index)
				{
				case 0:
					Tag1 = value;
					break;
				case 1:
					Tag2 = value;
					break;
				case 2:
					Tag3 = value;
					break;
				case 3:
					Tag4 = value;
					break;
				case 4:
					Tag5 = value;
					break;
				case 5:
					Tag6 = value;
					break;
				case 6:
					Tag7 = value;
					break;
				case 7:
					Tag8 = value;
					break;
				}
			}
		}

		internal readonly KeyValuePair<string, object?>[]? Tags => _overflowTags;

		public TagList(ReadOnlySpan<KeyValuePair<string, object?>> tagList)
		{
			this = default(TagList);
			_tagsCount = tagList.Length;
			switch (_tagsCount)
			{
			case 8:
				Tag8 = tagList[7];
				goto case 7;
			case 7:
				Tag7 = tagList[6];
				goto case 6;
			case 6:
				Tag6 = tagList[5];
				goto case 5;
			case 5:
				Tag5 = tagList[4];
				goto case 4;
			case 4:
				Tag4 = tagList[3];
				goto case 3;
			case 3:
				Tag3 = tagList[2];
				goto case 2;
			case 2:
				Tag2 = tagList[1];
				goto case 1;
			case 1:
				Tag1 = tagList[0];
				break;
			case 0:
				break;
			default:
				_overflowTags = new KeyValuePair<string, object>[_tagsCount + 8];
				tagList.CopyTo(_overflowTags);
				break;
			}
		}

		public void Add(string key, object? value)
		{
			Add(new KeyValuePair<string, object>(key, value));
		}

		public void Add(KeyValuePair<string, object?> tag)
		{
			if (_overflowTags != null)
			{
				if (_tagsCount == _overflowTags.Length)
				{
					Array.Resize(ref _overflowTags, _tagsCount + 8);
				}
				_overflowTags[_tagsCount++] = tag;
				return;
			}
			switch (_tagsCount)
			{
			default:
				return;
			case 0:
				Tag1 = tag;
				break;
			case 1:
				Tag2 = tag;
				break;
			case 2:
				Tag3 = tag;
				break;
			case 3:
				Tag4 = tag;
				break;
			case 4:
				Tag5 = tag;
				break;
			case 5:
				Tag6 = tag;
				break;
			case 6:
				Tag7 = tag;
				break;
			case 7:
				Tag8 = tag;
				break;
			case 8:
				MoveTagsToTheArray();
				_overflowTags[8] = tag;
				break;
			}
			_tagsCount++;
		}

		public readonly void CopyTo(Span<KeyValuePair<string, object?>> tags)
		{
			if (tags.Length < _tagsCount)
			{
				throw new ArgumentException(System.SR.Arg_BufferTooSmall);
			}
			if (_overflowTags != null)
			{
				Span<KeyValuePair<string, object>> span = MemoryExtensions.AsSpan(_overflowTags);
				span = span.Slice(0, _tagsCount);
				span.CopyTo(tags);
				return;
			}
			switch (_tagsCount)
			{
			default:
				return;
			case 8:
				tags[7] = Tag8;
				goto case 7;
			case 7:
				tags[6] = Tag7;
				goto case 6;
			case 6:
				tags[5] = Tag6;
				goto case 5;
			case 5:
				tags[4] = Tag5;
				goto case 4;
			case 4:
				tags[3] = Tag4;
				goto case 3;
			case 3:
				tags[2] = Tag3;
				goto case 2;
			case 2:
				tags[1] = Tag2;
				break;
			case 1:
				break;
			case 0:
				return;
			}
			tags[0] = Tag1;
		}

		public readonly void CopyTo(KeyValuePair<string, object?>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if ((uint)arrayIndex >= array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			CopyTo(MemoryExtensions.AsSpan(array).Slice(arrayIndex));
		}

		public void Insert(int index, KeyValuePair<string, object?> item)
		{
			if ((uint)index > (uint)_tagsCount)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index == _tagsCount)
			{
				Add(item);
				return;
			}
			if (_tagsCount == 8 && _overflowTags == null)
			{
				MoveTagsToTheArray();
			}
			if (_overflowTags != null)
			{
				if (_tagsCount == _overflowTags.Length)
				{
					Array.Resize(ref _overflowTags, _tagsCount + 8);
				}
				for (int num = _tagsCount; num > index; num--)
				{
					_overflowTags[num] = _overflowTags[num - 1];
				}
				_overflowTags[index] = item;
				_tagsCount++;
				return;
			}
			switch (index)
			{
			default:
				return;
			case 0:
				Tag8 = Tag7;
				Tag7 = Tag6;
				Tag6 = Tag5;
				Tag5 = Tag4;
				Tag4 = Tag3;
				Tag3 = Tag2;
				Tag2 = Tag1;
				Tag1 = item;
				break;
			case 1:
				Tag8 = Tag7;
				Tag7 = Tag6;
				Tag6 = Tag5;
				Tag5 = Tag4;
				Tag4 = Tag3;
				Tag3 = Tag2;
				Tag2 = item;
				break;
			case 2:
				Tag8 = Tag7;
				Tag7 = Tag6;
				Tag6 = Tag5;
				Tag5 = Tag4;
				Tag4 = Tag3;
				Tag3 = item;
				break;
			case 3:
				Tag8 = Tag7;
				Tag7 = Tag6;
				Tag6 = Tag5;
				Tag5 = Tag4;
				Tag4 = item;
				break;
			case 4:
				Tag8 = Tag7;
				Tag7 = Tag6;
				Tag6 = Tag5;
				Tag5 = item;
				break;
			case 5:
				Tag8 = Tag7;
				Tag7 = Tag6;
				Tag6 = item;
				break;
			case 6:
				Tag8 = Tag7;
				Tag7 = item;
				break;
			}
			_tagsCount++;
		}

		public void RemoveAt(int index)
		{
			if ((uint)index >= (uint)_tagsCount)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (_overflowTags != null)
			{
				for (int i = index; i < _tagsCount - 1; i++)
				{
					_overflowTags[i] = _overflowTags[i + 1];
				}
				_tagsCount--;
				return;
			}
			switch (index)
			{
			case 0:
				Tag1 = Tag2;
				goto case 1;
			case 1:
				Tag2 = Tag3;
				goto case 2;
			case 2:
				Tag3 = Tag4;
				goto case 3;
			case 3:
				Tag4 = Tag5;
				goto case 4;
			case 4:
				Tag5 = Tag6;
				goto case 5;
			case 5:
				Tag6 = Tag7;
				goto case 6;
			case 6:
				Tag7 = Tag8;
				break;
			}
			_tagsCount--;
		}

		public void Clear()
		{
			_tagsCount = 0;
		}

		public readonly bool Contains(KeyValuePair<string, object?> item)
		{
			return IndexOf(item) >= 0;
		}

		public bool Remove(KeyValuePair<string, object?> item)
		{
			int num = IndexOf(item);
			if (num >= 0)
			{
				RemoveAt(num);
				return true;
			}
			return false;
		}

		public readonly IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
		{
			return new Enumerator(in this);
		}

		readonly IEnumerator IEnumerable.GetEnumerator()
		{
			return new Enumerator(in this);
		}

		public readonly int IndexOf(KeyValuePair<string, object?> item)
		{
			if (_overflowTags != null)
			{
				for (int i = 0; i < _tagsCount; i++)
				{
					if (TagsEqual(_overflowTags[i], item))
					{
						return i;
					}
				}
				return -1;
			}
			switch (_tagsCount)
			{
			case 1:
				if (TagsEqual(Tag1, item))
				{
					return 0;
				}
				break;
			case 2:
				if (TagsEqual(Tag1, item))
				{
					return 0;
				}
				if (TagsEqual(Tag2, item))
				{
					return 1;
				}
				break;
			case 3:
				if (TagsEqual(Tag1, item))
				{
					return 0;
				}
				if (TagsEqual(Tag2, item))
				{
					return 1;
				}
				if (TagsEqual(Tag3, item))
				{
					return 2;
				}
				break;
			case 4:
				if (TagsEqual(Tag1, item))
				{
					return 0;
				}
				if (TagsEqual(Tag2, item))
				{
					return 1;
				}
				if (TagsEqual(Tag3, item))
				{
					return 2;
				}
				if (TagsEqual(Tag4, item))
				{
					return 3;
				}
				break;
			case 5:
				if (TagsEqual(Tag1, item))
				{
					return 0;
				}
				if (TagsEqual(Tag2, item))
				{
					return 1;
				}
				if (TagsEqual(Tag3, item))
				{
					return 2;
				}
				if (TagsEqual(Tag4, item))
				{
					return 3;
				}
				if (TagsEqual(Tag5, item))
				{
					return 4;
				}
				break;
			case 6:
				if (TagsEqual(Tag1, item))
				{
					return 0;
				}
				if (TagsEqual(Tag2, item))
				{
					return 1;
				}
				if (TagsEqual(Tag3, item))
				{
					return 2;
				}
				if (TagsEqual(Tag4, item))
				{
					return 3;
				}
				if (TagsEqual(Tag5, item))
				{
					return 4;
				}
				if (TagsEqual(Tag6, item))
				{
					return 5;
				}
				break;
			case 7:
				if (TagsEqual(Tag1, item))
				{
					return 0;
				}
				if (TagsEqual(Tag2, item))
				{
					return 1;
				}
				if (TagsEqual(Tag3, item))
				{
					return 2;
				}
				if (TagsEqual(Tag4, item))
				{
					return 3;
				}
				if (TagsEqual(Tag5, item))
				{
					return 4;
				}
				if (TagsEqual(Tag6, item))
				{
					return 5;
				}
				if (TagsEqual(Tag7, item))
				{
					return 6;
				}
				break;
			case 8:
				if (TagsEqual(Tag1, item))
				{
					return 0;
				}
				if (TagsEqual(Tag2, item))
				{
					return 1;
				}
				if (TagsEqual(Tag3, item))
				{
					return 2;
				}
				if (TagsEqual(Tag4, item))
				{
					return 3;
				}
				if (TagsEqual(Tag5, item))
				{
					return 4;
				}
				if (TagsEqual(Tag6, item))
				{
					return 5;
				}
				if (TagsEqual(Tag7, item))
				{
					return 6;
				}
				if (TagsEqual(Tag8, item))
				{
					return 7;
				}
				break;
			}
			return -1;
		}

		private static bool TagsEqual(KeyValuePair<string, object> tag1, KeyValuePair<string, object> tag2)
		{
			if (tag1.Key != tag2.Key)
			{
				return false;
			}
			if (tag1.Value == null)
			{
				if (tag2.Value != null)
				{
					return false;
				}
			}
			else if (!tag1.Value.Equals(tag2.Value))
			{
				return false;
			}
			return true;
		}

		private void MoveTagsToTheArray()
		{
			_overflowTags = new KeyValuePair<string, object>[16];
			_overflowTags[0] = Tag1;
			_overflowTags[1] = Tag2;
			_overflowTags[2] = Tag3;
			_overflowTags[3] = Tag4;
			_overflowTags[4] = Tag5;
			_overflowTags[5] = Tag6;
			_overflowTags[6] = Tag7;
			_overflowTags[7] = Tag8;
		}
	}
}
namespace System.Diagnostics.Metrics
{
	[UnsupportedOSPlatform("browser")]
	[SecuritySafeCritical]
	internal sealed class AggregationManager
	{
		public const double MinCollectionTimeSecs = 0.1;

		private static readonly QuantileAggregation s_defaultHistogramConfig = new QuantileAggregation(0.5, 0.95, 0.99);

		private readonly List<Predicate<Instrument>> _instrumentConfigFuncs = new List<Predicate<Instrument>>();

		private TimeSpan _collectionPeriod;

		private readonly ConcurrentDictionary<Instrument, InstrumentState> _instrumentStates = new ConcurrentDictionary<Instrument, InstrumentState>();

		private readonly CancellationTokenSource _cts = new CancellationTokenSource();

		private Thread _collectThread;

		private readonly MeterListener _listener;

		private int _currentTimeSeries;

		private int _currentHistograms;

		private readonly int _maxTimeSeries;

		private readonly int _maxHistograms;

		private readonly Action<Instrument, LabeledAggregationStatistics> _collectMeasurement;

		private readonly Action<DateTime, DateTime> _beginCollection;

		private readonly Action<DateTime, DateTime> _endCollection;

		private readonly Action<Instrument> _beginInstrumentMeasurements;

		private readonly Action<Instrument> _endInstrumentMeasurements;

		private readonly Action<Instrument> _instrumentPublished;

		private readonly Action _initialInstrumentEnumerationComplete;

		private readonly Action<Exception> _collectionError;

		private readonly Action _timeSeriesLimitReached;

		private readonly Action _histogramLimitReached;

		private readonly Action<Exception> _observableInstrumentCallbackError;

		public AggregationManager(int maxTimeSeries, int maxHistograms, Action<Instrument, LabeledAggregationStatistics> collectMeasurement, Action<DateTime, DateTime> beginCollection, Action<DateTime, DateTime> endCollection, Action<Instrument> beginInstrumentMeasurements, Action<Instrument> endInstrumentMeasurements, Action<Instrument> instrumentPublished, Action initialInstrumentEnumerationComplete, Action<Exception> collectionError, Action timeSeriesLimitReached, Action histogramLimitReached, Action<Exception> observableInstrumentCallbackError)
		{
			_maxTimeSeries = maxTimeSeries;
			_maxHistograms = maxHistograms;
			_collectMeasurement = collectMeasurement;
			_beginCollection = beginCollection;
			_endCollection = endCollection;
			_beginInstrumentMeasurements = beginInstrumentMeasurements;
			_endInstrumentMeasurements = endInstrumentMeasurements;
			_instrumentPublished = instrumentPublished;
			_initialInstrumentEnumerationComplete = initialInstrumentEnumerationComplete;
			_collectionError = collectionError;
			_timeSeriesLimitReached = timeSeriesLimitReached;
			_histogramLimitReached = histogramLimitReached;
			_observableInstrumentCallbackError = observableInstrumentCallbackError;
			_listener = new MeterListener
			{
				InstrumentPublished = delegate(Instrument instrument, MeterListener listener)
				{
					_instrumentPublished(instrument);
					InstrumentState instrumentState = GetInstrumentState(instrument);
					if (instrumentState != null)
					{
						_beginInstrumentMeasurements(instrument);
						listener.EnableMeasurementEvents(instrument, instrumentState);
					}
				},
				MeasurementsCompleted = delegate(Instrument instrument, object cookie)
				{
					_endInstrumentMeasurements(instrument);
					RemoveInstrumentState(instrument, (InstrumentState)cookie);
				}
			};
			_listener.SetMeasurementEventCallback(delegate(Instrument i, double m, ReadOnlySpan<KeyValuePair<string, object>> l, object c)
			{
				((InstrumentState)c).Update(m, l);
			});
			_listener.SetMeasurementEventCallback(delegate(Instrument i, float m, ReadOnlySpan<KeyValuePair<string, object>> l, object c)
			{
				((InstrumentState)c).Update(m, l);
			});
			_listener.SetMeasurementEventCallback(delegate(Instrument i, long m, ReadOnlySpan<KeyValuePair<string, object>> l, object c)
			{
				((InstrumentState)c).Update(m, l);
			});
			_listener.SetMeasurementEventCallback(delegate(Instrument i, int m, ReadOnlySpan<KeyValuePair<string, object>> l, object c)
			{
				((InstrumentState)c).Update(m, l);
			});
			_listener.SetMeasurementEventCallback(delegate(Instrument i, short m, ReadOnlySpan<KeyValuePair<string, object>> l, object c)
			{
				((InstrumentState)c).Update(m, l);
			});
			_listener.SetMeasurementEventCallback(delegate(Instrument i, byte m, ReadOnlySpan<KeyValuePair<string, object>> l, object c)
			{
				((InstrumentState)c).Update((int)m, l);
			});
			_listener.SetMeasurementEventCallback(delegate(Instrument i, decimal m, ReadOnlySpan<KeyValuePair<string, object>> l, object c)
			{
				((InstrumentState)c).Update((double)m, l);
			});
		}

		public void Include(string meterName)
		{
			Include((Instrument i) => i.Meter.Name == meterName);
		}

		public void Include(string meterName, string instrumentName)
		{
			Include((Instrument i) => i.Meter.Name == meterName && i.Name == instrumentName);
		}

		private void Include(Predicate<Instrument> instrumentFilter)
		{
			lock (this)
			{
				_instrumentConfigFuncs.Add(instrumentFilter);
			}
		}

		public AggregationManager SetCollectionPeriod(TimeSpan collectionPeriod)
		{
			lock (this)
			{
				_collectionPeriod = collectionPeriod;
				return this;
			}
		}

		public void Start()
		{
			_collectThread = new Thread((ThreadStart)delegate
			{
				CollectWorker(_cts.Token);
			});
			_collectThread.IsBackground = true;
			_collectThread.Name = "MetricsEventSource CollectWorker";
			_collectThread.Start();
			_listener.Start();
			_initialInstrumentEnumerationComplete();
		}

		private void CollectWorker(CancellationToken cancelToken)
		{
			try
			{
				double num = -1.0;
				lock (this)
				{
					num = _collectionPeriod.TotalSeconds;
				}
				DateTime utcNow = DateTime.UtcNow;
				DateTime arg = utcNow;
				while (!cancelToken.IsCancellationRequested)
				{
					DateTime utcNow2 = DateTime.UtcNow;
					double totalSeconds = (utcNow2 - utcNow).TotalSeconds;
					double value = Math.Ceiling(totalSeconds / num) * num;
					DateTime dateTime = utcNow.AddSeconds(value);
					DateTime dateTime2 = arg.AddSeconds(num);
					if (dateTime <= dateTime2)
					{
						dateTime = dateTime2;
					}
					TimeSpan timeout = dateTime - utcNow2;
					if (!cancelToken.WaitHandle.WaitOne(timeout))
					{
						_beginCollection(arg, dateTime);
						Collect();
						_endCollection(arg, dateTime);
						arg = dateTime;
						continue;
					}
					break;
				}
			}
			catch (Exception obj)
			{
				_collectionError(obj);
			}
		}

		public void Dispose()
		{
			_cts.Cancel();
			if (_collectThread != null)
			{
				_collectThread.Join();
				_collectThread = null;
			}
			_listener.Dispose();
		}

		private void RemoveInstrumentState(Instrument instrument, InstrumentState state)
		{
			_instrumentStates.TryRemove(instrument, out var _);
		}

		private InstrumentState GetInstrumentState(Instrument instrument)
		{
			if (!_instrumentStates.TryGetValue(instrument, out var value))
			{
				lock (this)
				{
					foreach (Predicate<Instrument> instrumentConfigFunc in _instrumentConfigFuncs)
					{
						if (instrumentConfigFunc(instrument))
						{
							value = BuildInstrumentState(instrument);
							if (value != null)
							{
								_instrumentStates.TryAdd(instrument, value);
								_instrumentStates.TryGetValue(instrument, out value);
							}
							break;
						}
					}
				}
			}
			return value;
		}

		internal InstrumentState BuildInstrumentState(Instrument instrument)
		{
			Func<Aggregator> aggregatorFactory = GetAggregatorFactory(instrument);
			if (aggregatorFactory == null)
			{
				return null;
			}
			Type type = aggregatorFactory.GetType().GenericTypeArguments[0];
			Type type2 = typeof(InstrumentState<>).MakeGenericType(type);
			return (InstrumentState)Activator.CreateInstance(type2, aggregatorFactory);
		}

		private Func<Aggregator> GetAggregatorFactory(Instrument instrument)
		{
			Type type = instrument.GetType();
			Type type2 = null;
			type2 = (type.IsGenericType ? type.GetGenericTypeDefinition() : null);
			if (type2 == typeof(Counter<>))
			{
				return delegate
				{
					lock (this)
					{
						return CheckTimeSeriesAllowed() ? new RateSumAggregator() : null;
					}
				};
			}
			if (type2 == typeof(ObservableCounter<>))
			{
				return delegate
				{
					lock (this)
					{
						return CheckTimeSeriesAllowed() ? new RateAggregator() : null;
					}
				};
			}
			if (type2 == typeof(ObservableGauge<>))
			{
				return delegate
				{
					lock (this)
					{
						return CheckTimeSeriesAllowed() ? new LastValue() : null;
					}
				};
			}
			if (type2 == typeof(Histogram<>))
			{
				return delegate
				{
					lock (this)
					{
						return (!CheckTimeSeriesAllowed() || !CheckHistogramAllowed()) ? null : new ExponentialHistogramAggregator(s_defaultHistogramConfig);
					}
				};
			}
			return null;
		}

		private bool CheckTimeSeriesAllowed()
		{
			if (_currentTimeSeries < _maxTimeSeries)
			{
				_currentTimeSeries++;
				return true;
			}
			if (_currentTimeSeries == _maxTimeSeries)
			{
				_currentTimeSeries++;
				_timeSeriesLimitReached();
				return false;
			}
			return false;
		}

		private bool CheckHistogramAllowed()
		{
			if (_currentHistograms < _maxHistograms)
			{
				_currentHistograms++;
				return true;
			}
			if (_currentHistograms == _maxHistograms)
			{
				_currentHistograms++;
				_histogramLimitReached();
				return false;
			}
			return false;
		}

		internal void Collect()
		{
			try
			{
				_listener.RecordObservableInstruments();
			}
			catch (Exception obj)
			{
				_observableInstrumentCallbackError(obj);
			}
			foreach (KeyValuePair<Instrument, InstrumentState> kv in _instrumentStates)
			{
				kv.Value.Collect(kv.Key, delegate(LabeledAggregationStatistics labeledAggStats)
				{
					_collectMeasurement(kv.Key, labeledAggStats);
				});
			}
		}
	}
	internal abstract class Aggregator
	{
		public abstract void Update(double measurement);

		public abstract IAggregationStatistics Collect();
	}
	internal interface IAggregationStatistics
	{
	}
	internal readonly struct QuantileValue
	{
		public double Quantile { get; }

		public double Value { get; }

		public QuantileValue(double quantile, double value)
		{
			Quantile = quantile;
			Value = value;
		}
	}
	internal sealed class HistogramStatistics : IAggregationStatistics
	{
		public QuantileValue[] Quantiles { get; }

		internal HistogramStatistics(QuantileValue[] quantiles)
		{
			Quantiles = quantiles;
		}
	}
	internal sealed class LabeledAggregationStatistics
	{
		public KeyValuePair<string, string>[] Labels { get; }

		public IAggregationStatistics AggregationStatistics { get; }

		public LabeledAggregationStatistics(IAggregationStatistics stats, params KeyValuePair<string, string>[] labels)
		{
			AggregationStatistics = stats;
			Labels = labels;
		}
	}
	[SecuritySafeCritical]
	internal struct AggregatorStore<TAggregator> where TAggregator : Aggregator
	{
		private volatile object _stateUnion;

		private volatile AggregatorLookupFunc<TAggregator> _cachedLookupFunc;

		private readonly Func<TAggregator> _createAggregatorFunc;

		public AggregatorStore(Func<TAggregator> createAggregator)
		{
			_stateUnion = null;
			_cachedLookupFunc = null;
			_createAggregatorFunc = createAggregator;
		}

		public TAggregator GetAggregator(ReadOnlySpan<KeyValuePair<string, object>> labels)
		{
			AggregatorLookupFunc<TAggregator> cachedLookupFunc = _cachedLookupFunc;
			if (cachedLookupFunc != null && cachedLookupFunc(labels, out var aggregator))
			{
				return aggregator;
			}
			return GetAggregatorSlow(labels);
		}

		private TAggregator GetAggregatorSlow(ReadOnlySpan<KeyValuePair<string, object>> labels)
		{
			TAggregator aggregator;
			bool flag = (_cachedLookupFunc = LabelInstructionCompiler.Create(ref this, _createAggregatorFunc, labels))(labels, out aggregator);
			return aggregator;
		}

		public void Collect(Action<LabeledAggregationStatistics> visitFunc)
		{
			object stateUnion = _stateUnion;
			object stateUnion2 = _stateUnion;
			if (!(stateUnion2 is TAggregator val))
			{
				if (!(stateUnion2 is FixedSizeLabelNameDictionary<StringSequence1, ObjectSequence1, TAggregator> fixedSizeLabelNameDictionary))
				{
					if (!(stateUnion2 is FixedSizeLabelNameDictionary<StringSequence2, ObjectSequence2, TAggregator> fixedSizeLabelNameDictionary2))
					{
						if (!(stateUnion2 is FixedSizeLabelNameDictionary<StringSequence3, ObjectSequence3, TAggregator> fixedSizeLabelNameDictionary3))
						{
							if (!(stateUnion2 is FixedSizeLabelNameDictionary<StringSequenceMany, ObjectSequenceMany, TAggregator> fixedSizeLabelNameDictionary4))
							{
								if (stateUnion2 is MultiSizeLabelNameDictionary<TAggregator> multiSizeLabelNameDictionary)
								{
									multiSizeLabelNameDictionary.Collect(visitFunc);
								}
							}
							else
							{
								fixedSizeLabelNameDictionary4.Collect(visitFunc);
							}
						}
						else
						{
							fixedSizeLabelNameDictionary3.Collect(visitFunc);
						}
					}
					else
					{
						fixedSizeLabelNameDictionary2.Collect(visitFunc);
					}
				}
				else
				{
					fixedSizeLabelNameDictionary.Collect(visitFunc);
				}
			}
			else
			{
				IAggregationStatistics stats = val.Collect();
				visitFunc(new LabeledAggregationStatistics(stats));
			}
		}

		public TAggregator GetAggregator()
		{
			MultiSizeLabelNameDictionary<TAggregator> multiSizeLabelNameDictionary2;
			while (true)
			{
				object stateUnion = _stateUnion;
				if (stateUnion == null)
				{
					TAggregator val = _createAggregatorFunc();
					if (val == null)
					{
						return val;
					}
					if (Interlocked.CompareExchange(ref _stateUnion, val, null) == null)
					{
						return val;
					}
					continue;
				}
				if (stateUnion is TAggregator result)
				{
					return result;
				}
				if (stateUnion is MultiSizeLabelNameDictionary<TAggregator> multiSizeLabelNameDictionary)
				{
					return multiSizeLabelNameDictionary.GetNoLabelAggregator(_createAggregatorFunc);
				}
				multiSizeLabelNameDictionary2 = new MultiSizeLabelNameDictionary<TAggregator>(stateUnion);
				if (Interlocked.CompareExchange(ref _stateUnion, multiSizeLabelNameDictionary2, stateUnion) == stateUnion)
				{
					break;
				}
			}
			return multiSizeLabelNameDictionary2.GetNoLabelAggregator(_createAggregatorFunc);
		}

		public ConcurrentDictionary<TObjectSequence, TAggregator> GetLabelValuesDictionary<TStringSequence, TObjectSequence>(in TStringSequence names) where TStringSequence : IStringSequence, IEquatable<TStringSequence> where TObjectSequence : IObjectSequence, IEquatable<TObjectSequence>
		{
			MultiSizeLabelNameDictionary<TAggregator> multiSizeLabelNameDictionary2;
			while (true)
			{
				object stateUnion = _stateUnion;
				if (stateUnion == null)
				{
					FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator> fixedSizeLabelNameDictionary = new FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator>();
					if (Interlocked.CompareExchange(ref _stateUnion, fixedSizeLabelNameDictionary, null) == null)
					{
						return fixedSizeLabelNameDictionary.GetValuesDictionary(in names);
					}
					continue;
				}
				if (stateUnion is FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator> fixedSizeLabelNameDictionary2)
				{
					return fixedSizeLabelNameDictionary2.GetValuesDictionary(in names);
				}
				if (stateUnion is MultiSizeLabelNameDictionary<TAggregator> multiSizeLabelNameDictionary)
				{
					return multiSizeLabelNameDictionary.GetFixedSizeLabelNameDictionary<TStringSequence, TObjectSequence>().GetValuesDictionary(in names);
				}
				multiSizeLabelNameDictionary2 = new MultiSizeLabelNameDictionary<TAggregator>(stateUnion);
				if (Interlocked.CompareExchange(ref _stateUnion, multiSizeLabelNameDictionary2, stateUnion) == stateUnion)
				{
					break;
				}
			}
			return multiSizeLabelNameDictionary2.GetFixedSizeLabelNameDictionary<TStringSequence, TObjectSequence>().GetValuesDictionary(in names);
		}
	}
	internal class MultiSizeLabelNameDictionary<TAggregator> where TAggregator : Aggregator
	{
		private TAggregator NoLabelAggregator;

		private FixedSizeLabelNameDictionary<StringSequence1, ObjectSequence1, TAggregator> Label1;

		private FixedSizeLabelNameDictionary<StringSequence2, ObjectSequence2, TAggregator> Label2;

		private FixedSizeLabelNameDictionary<StringSequence3, ObjectSequence3, TAggregator> Label3;

		private FixedSizeLabelNameDictionary<StringSequenceMany, ObjectSequenceMany, TAggregator> LabelMany;

		public MultiSizeLabelNameDictionary(object initialLabelNameDict)
		{
			NoLabelAggregator = null;
			Label1 = null;
			Label2 = null;
			Label3 = null;
			LabelMany = null;
			if (!(initialLabelNameDict is TAggregator noLabelAggregator))
			{
				if (!(initialLabelNameDict is FixedSizeLabelNameDictionary<StringSequence1, ObjectSequence1, TAggregator> label))
				{
					if (!(initialLabelNameDict is FixedSizeLabelNameDictionary<StringSequence2, ObjectSequence2, TAggregator> label2))
					{
						if (!(initialLabelNameDict is FixedSizeLabelNameDictionary<StringSequence3, ObjectSequence3, TAggregator> label3))
						{
							if (initialLabelNameDict is FixedSizeLabelNameDictionary<StringSequenceMany, ObjectSequenceMany, TAggregator> labelMany)
							{
								LabelMany = labelMany;
							}
						}
						else
						{
							Label3 = label3;
						}
					}
					else
					{
						Label2 = label2;
					}
				}
				else
				{
					Label1 = label;
				}
			}
			else
			{
				NoLabelAggregator = noLabelAggregator;
			}
		}

		public TAggregator GetNoLabelAggregator(Func<TAggregator> createFunc)
		{
			if (NoLabelAggregator == null)
			{
				TAggregator val = createFunc();
				if (val != null)
				{
					Interlocked.CompareExchange(ref NoLabelAggregator, val, null);
				}
			}
			return NoLabelAggregator;
		}

		public FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator> GetFixedSizeLabelNameDictionary<TStringSequence, TObjectSequence>() where TStringSequence : IStringSequence, IEquatable<TStringSequence> where TObjectSequence : IObjectSequence, IEquatable<TObjectSequence>
		{
			TStringSequence val = default(TStringSequence);
			if (!(val is StringSequence1))
			{
				if (!(val is StringSequence2))
				{
					if (!(val is StringSequence3))
					{
						if (val is StringSequenceMany)
						{
							if (LabelMany == null)
							{
								Interlocked.CompareExchange(ref LabelMany, new FixedSizeLabelNameDictionary<StringSequenceMany, ObjectSequenceMany, TAggregator>(), null);
							}
							return (FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator>)(object)LabelMany;
						}
						return null;
					}
					if (Label3 == null)
					{
						Interlocked.CompareExchange(ref Label3, new FixedSizeLabelNameDictionary<StringSequence3, ObjectSequence3, TAggregator>(), null);
					}
					return (FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator>)(object)Label3;
				}
				if (Label2 == null)
				{
					Interlocked.CompareExchange(ref Label2, new FixedSizeLabelNameDictionary<StringSequence2, ObjectSequence2, TAggregator>(), null);
				}
				return (FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator>)(object)Label2;
			}
			if (Label1 == null)
			{
				Interlocked.CompareExchange(ref Label1, new FixedSizeLabelNameDictionary<StringSequence1, ObjectSequence1, TAggregator>(), null);
			}
			return (FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator>)(object)Label1;
		}

		public void Collect(Action<LabeledAggregationStatistics> visitFunc)
		{
			if (NoLabelAggregator != null)
			{
				IAggregationStatistics stats = NoLabelAggregator.Collect();
				visitFunc(new LabeledAggregationStatistics(stats));
			}
			Label1?.Collect(visitFunc);
			Label2?.Collect(visitFunc);
			Label3?.Collect(visitFunc);
			LabelMany?.Collect(visitFunc);
		}
	}
	internal struct LabelInstruction
	{
		public int SourceIndex { get; }

		public string LabelName { get; }

		public LabelInstruction(int sourceIndex, string labelName)
		{
			SourceIndex = sourceIndex;
			LabelName = labelName;
		}
	}
	internal delegate bool AggregatorLookupFunc<TAggregator>(ReadOnlySpan<KeyValuePair<string, object>> labels, out TAggregator aggregator);
	[SecurityCritical]
	internal static class LabelInstructionCompiler
	{
		public static AggregatorLookupFunc<TAggregator> Create<TAggregator>(ref AggregatorStore<TAggregator> aggregatorStore, Func<TAggregator> createAggregatorFunc, ReadOnlySpan<KeyValuePair<string, object>> labels) where TAggregator : Aggregator
		{
			LabelInstruction[] array = Compile(labels);
			Array.Sort(array, (LabelInstruction a, LabelInstruction b) => string.CompareOrdinal(a.LabelName, b.LabelName));
			int expectedLabels = labels.Length;
			switch (array.Length)
			{
			case 0:
			{
				TAggregator defaultAggregator = aggregatorStore.GetAggregator();
				return delegate(ReadOnlySpan<KeyValuePair<string, object>> l, out TAggregator aggregator)
				{
					if (l.Length != expectedLabels)
					{
						aggregator = null;
						return false;
					}
					aggregator = defaultAggregator;
					return true;
				};
			}
			case 1:
			{
				ConcurrentDictionary<ObjectSequence1, TAggregator> labelValuesDictionary2 = aggregatorStore.GetLabelValuesDictionary<StringSequence1, ObjectSequence1>(new StringSequence1(array[0].LabelName));
				LabelInstructionInterpretter<ObjectSequence1, TAggregator> labelInstructionInterpretter2 = new LabelInstructionInterpretter<ObjectSequence1, TAggregator>(expectedLabels, array, labelValuesDictionary2, createAggregatorFunc);
				return labelInstructionInterpretter2.GetAggregator;
			}
			case 2:
			{
				ConcurrentDictionary<ObjectSequence2, TAggregator> labelValuesDictionary4 = aggregatorStore.GetLabelValuesDictionary<StringSequence2, ObjectSequence2>(new StringSequence2(array[0].LabelName, array[1].LabelName));
				LabelInstructionInterpretter<ObjectSequence2, TAggregator> labelInstructionInterpretter4 = new LabelInstructionInterpretter<ObjectSequence2, TAggregator>(expectedLabels, array, labelValuesDictionary4, createAggregatorFunc);
				return labelInstructionInterpretter4.GetAggregator;
			}
			case 3:
			{
				ConcurrentDictionary<ObjectSequence3, TAggregator> labelValuesDictionary3 = aggregatorStore.GetLabelValuesDictionary<StringSequence3, ObjectSequence3>(new StringSequence3(array[0].LabelName, array[1].LabelName, array[2].LabelName));
				LabelInstructionInterpretter<ObjectSequence3, TAggregator> labelInstructionInterpretter3 = new LabelInstructionInterpretter<ObjectSequence3, TAggregator>(expectedLabels, array, labelValuesDictionary3, createAggregatorFunc);
				return labelInstructionInterpretter3.GetAggregator;
			}
			default:
			{
				string[] array2 = new string[array.Length];
				for (int num = 0; num < array.Length; num++)
				{
					array2[num] = array[num].LabelName;
				}
				ConcurrentDictionary<ObjectSequenceMany, TAggregator> labelValuesDictionary = aggregatorStore.GetLabelValuesDictionary<StringSequenceMany, ObjectSequenceMany>(new StringSequenceMany(array2));
				LabelInstructionInterpretter<ObjectSequenceMany, TAggregator> labelInstructionInterpretter = new LabelInstructionInterpretter<ObjectSequenceMany, TAggregator>(expectedLabels, array, labelValuesDictionary, createAggregatorFunc);
				return labelInstructionInterpretter.GetAggregator;
			}
			}
		}

		private static LabelInstruction[] Compile(ReadOnlySpan<KeyValuePair<string, object>> labels)
		{
			LabelInstruction[] array = new LabelInstruction[labels.Length];
			for (int i = 0; i < labels.Length; i++)
			{
				int num = i;
				int sourceIndex = i;
				KeyValuePair<string, object> keyValuePair = labels[i];
				array[num] = new LabelInstruction(sourceIndex, keyValuePair.Key);
			}
			return array;
		}
	}
	[SecurityCritical]
	internal class LabelInstructionInterpretter<TObjectSequence, TAggregator> where TObjectSequence : struct, IObjectSequence, IEquatable<TObjectSequence> where TAggregator : Aggregator
	{
		private int _expectedLabelCount;

		private LabelInstruction[] _instructions;

		private ConcurrentDictionary<TObjectSequence, TAggregator> _valuesDict;

		private Func<TObjectSequence, TAggregator> _createAggregator;

		public LabelInstructionInterpretter(int expectedLabelCount, LabelInstruction[] instructions, ConcurrentDictionary<TObjectSequence, TAggregator> valuesDict, Func<TAggregator> createAggregator)
		{
			_expectedLabelCount = expectedLabelCount;
			_instructions = instructions;
			_valuesDict = valuesDict;
			_createAggregator = (TObjectSequence _) => createAggregator();
		}

		public bool GetAggregator(ReadOnlySpan<KeyValuePair<string, object>> labels, out TAggregator aggregator)
		{
			aggregator = null;
			if (labels.Length != _expectedLabelCount)
			{
				return false;
			}
			TObjectSequence val = default(TObjectSequence);
			if (val is ObjectSequenceMany)
			{
				val = (TObjectSequence)(object)new ObjectSequenceMany(new object[_expectedLabelCount]);
			}
			for (int i = 0; i < _instructions.Length; i++)
			{
				LabelInstruction labelInstruction = _instructions[i];
				string labelName = labelInstruction.LabelName;
				KeyValuePair<string, object> keyValuePair = labels[labelInstruction.SourceIndex];
				if (labelName != keyValuePair.Key)
				{
					return false;
				}
				int i2 = i;
				keyValuePair = labels[labelInstruction.SourceIndex];
				val[i2] = keyValuePair.Value;
			}
			if (!_valuesDict.TryGetValue(val, out aggregator))
			{
				aggregator = _createAggregator(val);
				if (aggregator == null)
				{
					return true;
				}
				aggregator = _valuesDict.GetOrAdd(val, aggregator);
			}
			return true;
		}
	}
	internal class FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator> : ConcurrentDictionary<TStringSequence, ConcurrentDictionary<TObjectSequence, TAggregator>> where TStringSequence : IStringSequence, IEquatable<TStringSequence> where TObjectSequence : IObjectSequence, IEquatable<TObjectSequence> where TAggregator : Aggregator
	{
		public void Collect(Action<LabeledAggregationStatistics> visitFunc)
		{
			using IEnumerator<KeyValuePair<TStringSequence, ConcurrentDictionary<TObjectSequence, TAggregator>>> enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<TStringSequence, ConcurrentDictionary<TObjectSequence, TAggregator>> current = enumerator.Current;
				TStringSequence key = current.Key;
				foreach (KeyValuePair<TObjectSequence, TAggregator> item in current.Value)
				{
					TObjectSequence key2 = item.Key;
					KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[key.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = new KeyValuePair<string, string>(key[i], key2[i]?.ToString() ?? "");
					}
					IAggregationStatistics stats = item.Value.Collect();
					visitFunc(new LabeledAggregationStatistics(stats, array));
				}
			}
		}

		public ConcurrentDictionary<TObjectSequence, TAggregator> GetValuesDictionary(in TStringSequence names)
		{
			return GetOrAdd(names, (TStringSequence _) => new ConcurrentDictionary<TObjectSequence, TAggregator>());
		}
	}
	public sealed class Counter<T> : Instrument<T> where T : struct
	{
		internal Counter(Meter meter, string name, string unit, string description)
			: base(meter, name, unit, description)
		{
			Publish();
		}

		public void Add(T delta)
		{
			RecordMeasurement(delta);
		}

		public void Add(T delta, KeyValuePair<string, object?> tag)
		{
			RecordMeasurement(delta, tag);
		}

		public void Add(T delta, KeyValuePair<string, object?> tag1, KeyValuePair<string, object?> tag2)
		{
			RecordMeasurement(delta, tag1, tag2);
		}

		public void Add(T delta, KeyValuePair<string, object?> tag1, KeyValuePair<string, object?> tag2, KeyValuePair<string, object?> tag3)
		{
			RecordMeasurement(delta, tag1, tag2, tag3);
		}

		public void Add(T delta, ReadOnlySpan<KeyValuePair<string, object?>> tags)
		{
			RecordMeasurement(delta, tags);
		}

		public void Add(T delta, params KeyValuePair<string, object?>[] tags)
		{
			RecordMeasurement(delta, MemoryExtensions.AsSpan(tags));
		}

		public void Add(T delta, in TagList tagList)
		{
			RecordMeasurement(delta, in tagList);
		}
	}
	internal sealed class QuantileAggregation
	{
		public double[] Quantiles { get; set; }

		public double MaxRelativeError { get; set; } = 0.001;

		public QuantileAggregation(params double[] quantiles)
		{
			Quantiles = quantiles;
			Array.Sort(Quantiles);
		}
	}
	internal sealed class ExponentialHistogramAggregator : Aggregator
	{
		private struct Bucket
		{
			public double Value;

			public int Count;

			public Bucket(double value, int count)
			{
				Value = value;
				Count = count;
			}
		}

		private const int ExponentArraySize = 4096;

		private const int ExponentShift = 52;

		private const double MinRelativeError = 0.0001;

		private readonly QuantileAggregation _config;

		private int[][] _counters;

		private int _count;

		private readonly int _mantissaMax;

		private readonly int _mantissaMask;

		private readonly int _mantissaShift;

		public ExponentialHistogramAggregator(QuantileAggregation config)
		{
			_config = config;
			_counters = new int[4096][];
			if (_config.MaxRelativeError < 0.0001)
			{
				throw new ArgumentException();
			}
			int num = (int)Math.Ceiling(Math.Log(1.0 / _config.MaxRelativeError, 2.0)) - 1;
			_mantissaShift = 52 - num;
			_mantissaMax = 1 << num;
			_mantissaMask = _mantissaMax - 1;
		}

		public override IAggregationStatistics Collect()
		{
			int[][] counters;
			int count;
			lock (this)
			{
				counters = _counters;
				count = _count;
				_counters = new int[4096][];
				_count = 0;
			}
			QuantileValue[] array = new QuantileValue[_config.Quantiles.Length];
			int num = 0;
			if (num == _config.Quantiles.Length)
			{
				return new HistogramStatistics(array);
			}
			count -= GetInvalidCount(counters);
			int num2 = QuantileToRank(_config.Quantiles[num], count);
			int num3 = 0;
			foreach (Bucket item in IterateBuckets(counters))
			{
				num3 += item.Count;
				while (num3 > num2)
				{
					array[num] = new QuantileValue(_config.Quantiles[num], item.Value);
					num++;
					if (num == _config.Quantiles.Length)
					{
						return new HistogramStatistics(array);
					}
					num2 = QuantileToRank(_config.Quantiles[num], count);
				}
			}
			return new HistogramStatistics(Array.Empty<QuantileValue>());
		}

		private int GetInvalidCount(int[][] counters)
		{
			int[] array = counters[2047];
			int[] array2 = counters[4095];
			int num = 0;
			if (array != null)
			{
				int[] array3 = array;
				foreach (int num2 in array3)
				{
					num += num2;
				}
			}
			if (array2 != null)
			{
				int[] array4 = array2;
				foreach (int num3 in array4)
				{
					num += num3;
				}
			}
			return num;
		}

		private IEnumerable<Bucket> IterateBuckets(int[][] counters)
		{
			for (int exponent = 4094; exponent >= 2048; exponent--)
			{
				int[] mantissaCounts = counters[exponent];
				if (mantissaCounts != null)
				{
					for (int mantissa = _mantissaMax - 1; mantissa >= 0; mantissa--)
					{
						int num = mantissaCounts[mantissa];
						if (num > 0)
						{
							yield return new Bucket(GetBucketCanonicalValue(exponent, mantissa), num);
						}
					}
				}
			}
			for (int exponent = 0; exponent < 2047; exponent++)
			{
				int[] mantissaCounts = counters[exponent];
				if (mantissaCounts == null)
				{
					continue;
				}
				for (int mantissa = 0; mantissa < _mantissaMax; mantissa++)
				{
					int num2 = mantissaCounts[mantissa];
					if (num2 > 0)
					{
						yield return new Bucket(GetBucketCanonicalValue(exponent, mantissa), num2);
					}
				}
			}
		}

		public override void Update(double measurement)
		{
			lock (this)
			{
				ulong num = (ulong)BitConverter.DoubleToInt64Bits(measurement);
				int num2 = (int)(num >> 52);
				int num3 = (int)(num >> _mantissaShift) & _mantissaMask;
				ref int[] reference = ref _counters[num2];
				if (reference == null)
				{
					reference = new int[_mantissaMax];
				}
				reference[num3]++;
				_count++;
			}
		}

		private int QuantileToRank(double quantile, int count)
		{
			return Math.Min(Math.Max(0, (int)(quantile * (double)count)), count - 1);
		}

		private double GetBucketCanonicalValue(int exponent, int mantissa)
		{
			long value = ((long)exponent << 52) | ((long)mantissa << _mantissaShift);
			return BitConverter.Int64BitsToDouble(value);
		}
	}
	public sealed class Histogram<T> : Instrument<T> where T : struct
	{
		internal Histogram(Meter meter, string name, string unit, string description)
			: base(meter, name, unit, description)
		{
			Publish();
		}

		public void Record(T value)
		{
			RecordMeasurement(value);
		}

		public void Record(T value, KeyValuePair<string, object?> tag)
		{
			RecordMeasurement(value, tag);
		}

		public void Record(T value, KeyValuePair<string, object?> tag1, KeyValuePair<string, object?> tag2)
		{
			RecordMeasurement(value, tag1, tag2);
		}

		public void Record(T value, KeyValuePair<string, object?> tag1, KeyValuePair<string, object?> tag2, KeyValuePair<string, object?> tag3)
		{
			RecordMeasurement(value, tag1, tag2, tag3);
		}

		public void Record(T value, ReadOnlySpan<KeyValuePair<string, object?>> tags)
		{
			RecordMeasurement(value, tags);
		}

		public void Record(T value, params KeyValuePair<string, object?>[] tags)
		{
			RecordMeasurement(value, MemoryExtensions.AsSpan(tags));
		}

		public void Record(T value, in TagList tagList)
		{
			RecordMeasurement(value, in tagList);
		}
	}
	public abstract class Instrument
	{
		internal readonly DiagLinkedList<ListenerSubscription> _subscriptions = new DiagLinkedList<ListenerSubscription>();

		internal static KeyValuePair<string, object?>[] EmptyTags => Array.Empty<KeyValuePair<string, object>>();

		internal static object SyncObject { get; } = new object();

		public Meter Meter { get; }

		public string Name { get; }

		public string? Description { get; }

		public string? Unit { get; }

		public bool Enabled => _subscriptions.First != null;

		public virtual bool IsObservable => false;

		protected Instrument(Meter meter, string name, string? unit, string? description)
		{
			if (meter == null)
			{
				throw new ArgumentNullException("meter");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Meter = meter;
			Name = name;
			Description = description;
			Unit = unit;
		}

		protected void Publish()
		{
			List<MeterListener> list = null;
			lock (SyncObject)
			{
				if (Meter.Disposed || !Meter.AddInstrument(this))
				{
					return;
				}
				list = MeterListener.GetAllListeners();
			}
			if (list == null)
			{
				return;
			}
			foreach (MeterListener item in list)
			{
				item.InstrumentPublished?.Invoke(this, item);
			}
		}

		internal void NotifyForUnpublishedInstrument()
		{
			for (DiagNode<ListenerSubscription> diagNode = _subscriptions.First; diagNode != null; diagNode = diagNode.Next)
			{
				diagNode.Value.Listener.DisableMeasurementEvents(this);
			}
			_subscriptions.Clear();
		}

		internal static void ValidateTypeParameter<T>()
		{
			Type typeFromHandle = typeof(T);
			if (typeFromHandle != typeof(byte) && typeFromHandle != typeof(short) && typeFromHandle != typeof(int) && typeFromHandle != typeof(long) && typeFromHandle != typeof(double) && typeFromHandle != typeof(float) && typeFromHandle != typeof(decimal))
			{
				throw new InvalidOperationException(System.SR.Format(System.SR.UnsupportedType, typeFromHandle));
			}
		}

		internal object EnableMeasurement(ListenerSubscription subscription, out bool oldStateStored)
		{
			oldStateStored = false;
			if (!_subscriptions.AddIfNotExist(subscription, (ListenerSubscription s1, ListenerSubscription s2) => s1.Listener == s2.Listener))
			{
				ListenerSubscription listenerSubscription = _subscriptions.Remove(subscription, (ListenerSubscription s1, ListenerSubscription s2) => s1.Listener == s2.Listener);
				_subscriptions.AddIfNotExist(subscription, (ListenerSubscription s1, ListenerSubscription s2) => s1.Listener == s2.Listener);
				oldStateStored = listenerSubscription.Listener == subscription.Listener;
				return listenerSubscription.State;
			}
			return false;
		}

		internal object DisableMeasurements(MeterListener listener)
		{
			return _subscriptions.Remove(new ListenerSubscription(listener), (ListenerSubscription s1, ListenerSubscription s2) => s1.Listener == s2.Listener).State;
		}

		internal virtual void Observe(MeterListener listener)
		{
			throw new InvalidOperationException();
		}

		internal object GetSubscriptionState(MeterListener listener)
		{
			for (DiagNode<ListenerSubscription> diagNode = _subscriptions.First; diagNode != null; diagNode = diagNode.Next)
			{
				if (listener == diagNode.Value.Listener)
				{
					return diagNode.Value.State;
				}
			}
			return null;
		}
	}
	public abstract class Instrument<T> : Instrument where T : struct
	{
		[ThreadStatic]
		private KeyValuePair<string, object>[] ts_tags;

		private const int MaxTagsCount = 8;

		protected Instrument(Meter meter, string name, string? unit, string? description)
			: base(meter, name, unit, description)
		{
			Instrument.ValidateTypeParameter<T>();
		}

		protected void RecordMeasurement(T measurement)
		{
			RecordMeasurement(measurement, MemoryExtensions.AsSpan(Instrument.EmptyTags));
		}

		protected void RecordMeasurement(T measurement, ReadOnlySpan<KeyValuePair<string, object?>> tags)
		{
			for (DiagNode<ListenerSubscription> diagNode = _subscriptions.First; diagNode != null; diagNode = diagNode.Next)
			{
				diagNode.Value.Listener.NotifyMeasurement(this, measurement, tags, diagNode.Value.State);
			}
		}

		protected void RecordMeasurement(T measurement, KeyValuePair<string, object?> tag)
		{
			KeyValuePair<string, object>[] array = ts_tags ?? new KeyValuePair<string, object>[8];
			ts_tags = null;
			array[0] = tag;
			RecordMeasurement(measurement, MemoryExtensions.AsSpan(array).Slice(0, 1));
			ts_tags = array;
		}

		protected void RecordMeasurement(T measurement, KeyValuePair<string, object?> tag1, KeyValuePair<string, object?> tag2)
		{
			KeyValuePair<string, object>[] array = ts_tags ?? new KeyValuePair<string, object>[8];
			ts_tags = null;
			array[0] = tag1;
			array[1] = tag2;
			RecordMeasurement(measurement, MemoryExtensions.AsSpan(array).Slice(0, 2));
			ts_tags = array;
		}

		protected void RecordMeasurement(T measurement, KeyValuePair<string, object?> tag1, KeyValuePair<string, object?> tag2, KeyValuePair<string, object?> tag3)
		{
			KeyValuePair<string, object>[] array = ts_tags ?? new KeyValuePair<string, object>[8];
			ts_tags = null;
			array[0] = tag1;
			array[1] = tag2;
			array[2] = tag3;
			RecordMeasurement(measurement, MemoryExtensions.AsSpan(array).Slice(0, 3));
			ts_tags = array;
		}

		protected void RecordMeasurement(T measurement, in TagList tagList)
		{
			KeyValuePair<string, object>[] tags = tagList.Tags;
			if (tags != null)
			{
				RecordMeasurement(measurement, MemoryExtensions.AsSpan(tags).Slice(0, tagList.Count));
				return;
			}
			tags = ts_tags ?? new KeyValuePair<string, object>[8];
			switch (tagList.Count)
			{
			default:
				return;
			case 8:
				tags[7] = tagList.Tag8;
				goto case 7;
			case 7:
				tags[6] = tagList.Tag7;
				goto case 6;
			case 6:
				tags[5] = tagList.Tag6;
				goto case 5;
			case 5:
				tags[4] = tagList.Tag5;
				goto case 4;
			case 4:
				tags[3] = tagList.Tag4;
				goto case 3;
			case 3:
				tags[2] = tagList.Tag3;
				goto case 2;
			case 2:
				tags[1] = tagList.Tag2;
				break;
			case 1:
				break;
			case 0:
				return;
			}
			tags[0] = tagList.Tag1;
			ts_tags = null;
			RecordMeasurement(measurement, MemoryExtensions.AsSpan(tags).Slice(0, tagList.Count));
			ts_tags = tags;
		}
	}
	internal abstract class InstrumentState
	{
		[SecuritySafeCritical]
		public abstract void Update(double measurement, ReadOnlySpan<KeyValuePair<string, object>> labels);

		public abstract void Collect(Instrument instrument, Action<LabeledAggregationStatistics> aggregationVisitFunc);
	}
	internal sealed class InstrumentState<TAggregator> : InstrumentState where TAggregator : Aggregator
	{
		private AggregatorStore<TAggregator> _aggregatorStore;

		public InstrumentState(Func<TAggregator> createAggregatorFunc)
		{
			_aggregatorStore = new AggregatorStore<TAggregator>(createAggregatorFunc);
		}

		public override void Collect(Instrument instrument, Action<LabeledAggregationStatistics> aggregationVisitFunc)
		{
			_aggregatorStore.Collect(aggregationVisitFunc);
		}

		[SecuritySafeCritical]
		public override void Update(double measurement, ReadOnlySpan<KeyValuePair<string, object>> labels)
		{
			_aggregatorStore.GetAggregator(labels)?.Update(measurement);
		}
	}
	internal sealed class LastValue : Aggregator
	{
		private double? _lastValue;

		public override void Update(double value)
		{
			_lastValue = value;
		}

		public override IAggregationStatistics Collect()
		{
			lock (this)
			{
				LastValueStatistics result = new LastValueStatistics(_lastValue);
				_lastValue = null;
				return result;
			}
		}
	}
	internal sealed class LastValueStatistics : IAggregationStatistics
	{
		public double? LastValue { get; }

		internal LastValueStatistics(double? lastValue)
		{
			LastValue = lastValue;
		}
	}
	public readonly struct Measurement<T> where T : struct
	{
		private readonly KeyValuePair<string, object>[] _tags;

		public ReadOnlySpan<KeyValuePair<string, object?>> Tags => MemoryExtensions.AsSpan(_tags);

		public T Value { get; }

		public Measurement(T value)
		{
			_tags = Instrument.EmptyTags;
			Value = value;
		}

		public Measurement(T value, IEnumerable<KeyValuePair<string, object?>>? tags)
		{
			_tags = ToArray(tags);
			Value = value;
		}

		public Measurement(T value, params KeyValuePair<string, object?>[]? tags)
		{
			if (tags != null)
			{
				_tags = new KeyValuePair<string, object>[tags.Length];
				tags.CopyTo(_tags, 0);
			}
			else
			{
				_tags = Instrument.EmptyTags;
			}
			Value = value;
		}

		public Measurement(T value, ReadOnlySpan<KeyValuePair<string, object?>> tags)
		{
			_tags = tags.ToArray();
			Value = value;
		}

		private static KeyValuePair<string, object>[] ToArray(IEnumerable<KeyValuePair<string, object>> tags)
		{
			if (tags != null)
			{
				return new List<KeyValuePair<string, object>>(tags).ToArray();
			}
			return Instrument.EmptyTags;
		}
	}
	public class Meter : IDisposable
	{
		private static readonly List<Meter> s_allMeters = new List<Meter>();

		private List<Instrument> _instruments = new List<Instrument>();

		internal bool Disposed { get; private set; }

		public string Name { get; }

		public string? Version { get; }

		public Meter(string name)
			: this(name, null)
		{
		}

		public Meter(string name, string? version)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Name = name;
			Version = version;
			lock (Instrument.SyncObject)
			{
				s_allMeters.Add(this);
			}
			GC.KeepAlive(MetricsEventSource.Log);
		}

		public Counter<T> CreateCounter<T>(string name, string? unit = null, string? description = null) where T : struct
		{
			return new Counter<T>(this, name, unit, description);
		}

		public Histogram<T> CreateHistogram<T>(string name, string? unit = null, string? description = null) where T : struct
		{
			return new Histogram<T>(this, name, unit, description);
		}

		public ObservableCounter<T> CreateObservableCounter<T>(string name, Func<T> observeValue, string? unit = null, string? description = null) where T : struct
		{
			return new ObservableCounter<T>(this, name, observeValue, unit, description);
		}

		public ObservableCounter<T> CreateObservableCounter<T>(string name, Func<Measurement<T>> observeValue, string? unit = null, string? description = null) where T : struct
		{
			return new ObservableCounter<T>(this, name, observeValue, unit, description);
		}

		public ObservableCounter<T> CreateObservableCounter<T>(string name, Func<IEnumerable<Measurement<T>>> observeValues, string? unit = null, string? description = null) where T : struct
		{
			return new ObservableCounter<T>(this, name, observeValues, unit, description);
		}

		public ObservableGauge<T> CreateObservableGauge<T>(string name, Func<T> observeValue, string? unit = null, string? description = null) where T : struct
		{
			return new ObservableGauge<T>(this, name, observeValue, unit, description);
		}

		public ObservableGauge<T> CreateObservableGauge<T>(string name, Func<Measurement<T>> observeValue, string? unit = null, string? description = null) where T : struct
		{
			return new ObservableGauge<T>(this, name, observeValue, unit, description);
		}

		public ObservableGauge<T> CreateObservableGauge<T>(string name, Func<IEnumerable<Measurement<T>>> observeValues, string? unit = null, string? description = null) where T : struct
		{
			return new ObservableGauge<T>(this, name, observeValues, unit, description);
		}

		public void Dispose()
		{
			List<Instrument> list = null;
			lock (Instrument.SyncObject)
			{
				if (Disposed)
				{
					return;
				}
				Disposed = true;
				s_allMeters.Remove(this);
				list = _instruments;
				_instruments = new List<Instrument>();
			}
			if (list == null)
			{
				return;
			}
			foreach (Instrument item in list)
			{
				item.NotifyForUnpublishedInstrument();
			}
		}

		internal bool AddInstrument(Instrument instrument)
		{
			if (!_instruments.Contains(instrument))
			{
				_instruments.Add(instrument);
				return true;
			}
			return false;
		}

		internal static List<Instrument> GetPublishedInstruments()
		{
			List<Instrument> list = null;
			if (s_allMeters.Count > 0)
			{
				list = new List<Instrument>();
				foreach (Meter s_allMeter in s_allMeters)
				{
					foreach (Instrument instrument in s_allMeter._instruments)
					{
						list.Add(instrument);
					}
				}
			}
			return list;
		}
	}
	public delegate void MeasurementCallback<T>(Instrument instrument, T measurement, ReadOnlySpan<KeyValuePair<string, object?>> tags, object? state) where T : struct;
	public sealed class MeterListener : IDisposable
	{
		private static List<MeterListener> s_allStartedListeners = new List<MeterListener>();

		private DiagLinkedList<Instrument> _enabledMeasurementInstruments = new DiagLinkedList<Instrument>();

		private bool _disposed;

		private MeasurementCallback<byte> _byteMeasurementCallback = delegate
		{
		};

		private MeasurementCallback<short> _shortMeasurementCallback = delegate
		{
		};

		private MeasurementCallback<int> _intMeasurementCallback = delegate
		{
		};

		private MeasurementCallback<long> _longMeasurementCallback = delegate
		{
		};

		private MeasurementCallback<float> _floatMeasurementCallback = delegate
		{
		};

		private MeasurementCallback<double> _doubleMeasurementCallback = delegate
		{
		};

		private MeasurementCallback<decimal> _decimalMeasurementCallback = delegate
		{
		};

		public Action<Instrument, MeterListener>? InstrumentPublished { get; set; }

		public Action<Instrument, object?>? MeasurementsCompleted { get; set; }

		public void EnableMeasurementEvents(Instrument instrument, object? state = null)
		{
			bool oldStateStored = false;
			bool flag = false;
			object arg = null;
			lock (Instrument.SyncObject)
			{
				if (instrument != null && !_disposed && !instrument.Meter.Disposed)
				{
					_enabledMeasurementInstruments.AddIfNotExist(instrument, (Instrument instrument2, Instrument instrument3) => instrument2 == instrument3);
					arg = instrument.EnableMeasurement(new ListenerSubscription(this, state), out oldStateStored);
					flag = true;
				}
			}
			if (flag)
			{
				if (oldStateStored && MeasurementsCompleted != null)
				{
					MeasurementsCompleted?.Invoke(instrument, arg);
				}
			}
			else
			{
				MeasurementsCompleted?.Invoke(instrument, state);
			}
		}

		public object? DisableMeasurementEvents(Instrument instrument)
		{
			object obj = null;
			lock (Instrument.SyncObject)
			{
				if (instrument == null || _enabledMeasurementInstruments.Remove(instrument, (Instrument instrument2, Instrument instrument3) => instrument2 == instrument3) == null)
				{
					return null;
				}
				obj = instrument.DisableMeasurements(this);
			}
			MeasurementsCompleted?.Invoke(instrument, obj);
			return obj;
		}

		public void SetMeasurementEventCallback<T>(MeasurementCallback<T>? measurementCallback) where T : struct
		{
			if (measurementCallback is MeasurementCallback<byte> measurementCallback2)
			{
				_byteMeasurementCallback = ((measurementCallback == null) ? ((MeasurementCallback<byte>)delegate
				{
				}) : measurementCallback2);
				return;
			}
			if (measurementCallback is MeasurementCallback<int> measurementCallback3)
			{
				_intMeasurementCallback = ((measurementCallback == null) ? ((MeasurementCallback<int>)delegate
				{
				}) : measurementCallback3);
				return;
			}
			if (measurementCallback is MeasurementCallback<float> measurementCallback4)
			{
				_floatMeasurementCallback = ((measurementCallback == null) ? ((MeasurementCallback<float>)delegate
				{
				}) : measurementCallback4);
				return;
			}
			if (measurementCallback is MeasurementCallback<double> measurementCallback5)
			{
				_doubleMeasurementCallback = ((measurementCallback == null) ? ((MeasurementCallback<double>)delegate
				{
				}) : measurementCallback5);
				return;
			}
			if (measurementCallback is MeasurementCallback<decimal> measurementCallback6)
			{
				_decimalMeasurementCallback = ((measurementCallback == null) ? ((MeasurementCallback<decimal>)delegate
				{
				}) : measurementCallback6);
				return;
			}
			if (measurementCallback is MeasurementCallback<short> measurementCallback7)
			{
				_shortMeasurementCallback = ((measurementCallback == null) ? ((MeasurementCallback<short>)delegate
				{
				}) : measurementCallback7);
				return;
			}
			if (measurementCallback is MeasurementCallback<long> measurementCallback8)
			{
				_longMeasurementCallback = ((measurementCallback == null) ? ((MeasurementCallback<long>)delegate
				{
				}) : measurementCallback8);
				return;
			}
			throw new InvalidOperationException(System.SR.Format(System.SR.UnsupportedType, typeof(T)));
		}

		public void Start()
		{
			List<Instrument> list = null;
			lock (Instrument.SyncObject)
			{
				if (_disposed)
				{
					return;
				}
				if (!s_allStartedListeners.Contains(this))
				{
					s_allStartedListeners.Add(this);
					list = Meter.GetPublishedInstruments();
				}
			}
			if (list == null)
			{
				return;
			}
			foreach (Instrument item in list)
			{
				InstrumentPublished?.Invoke(item, this);
			}
		}

		public void RecordObservableInstruments()
		{
			List<Exception> list = null;
			for (DiagNode<Instrument> diagNode = _enabledMeasurementInstruments.First; diagNode != null; diagNode = diagNode.Next)
			{
				if (diagNode.Value.IsObservable)
				{
					try
					{
						diagNode.Value.Observe(this);
					}
					catch (Exception item)
					{
						if (list == null)
						{
							list = new List<Exception>();
						}
						list.Add(item);
					}
				}
			}
			if (list != null)
			{
				throw new AggregateException(list);
			}
		}

		public void Dispose()
		{
			Dictionary<Instrument, object> dictionary = null;
			Action<Instrument, object> measurementsCompleted = MeasurementsCompleted;
			lock (Instrument.SyncObject)
			{
				if (_disposed)
				{
					return;
				}
				_disposed = true;
				s_allStartedListeners.Remove(this);
				DiagNode<Instrument> diagNode = _enabledMeasurementInstruments.First;
				if (diagNode != null && measurementsCompleted != null)
				{
					dictionary = new Dictionary<Instrument, object>();
					do
					{
						object value = diagNode.Value.DisableMeasurements(this);
						dictionary.Add(diagNode.Value, value);
						diagNode = diagNode.Next;
					}
					while (diagNode != null);
					_enabledMeasurementInstruments.Clear();
				}
			}
			if (dictionary == null)
			{
				return;
			}
			foreach (KeyValuePair<Instrument, object> item in dictionary)
			{
				measurementsCompleted?.Invoke(item.Key, item.Value);
			}
		}

		internal static List<MeterListener> GetAllListeners()
		{
			if (s_allStartedListeners.Count != 0)
			{
				return new List<MeterListener>(s_allStartedListeners);
			}
			return null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void NotifyMeasurement<T>(Instrument instrument, T measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state) where T : struct
		{
			if (typeof(T) == typeof(byte))
			{
				_byteMeasurementCallback(instrument, (byte)(object)measurement, tags, state);
			}
			if (typeof(T) == typeof(short))
			{
				_shortMeasurementCallback(instrument, (short)(object)measurement, tags, state);
			}
			if (typeof(T) == typeof(int))
			{
				_intMeasurementCallback(instrument, (int)(object)measurement, tags, state);
			}
			if (typeof(T) == typeof(long))
			{
				_longMeasurementCallback(instrument, (long)(object)measurement, tags, state);
			}
			if (typeof(T) == typeof(float))
			{
				_floatMeasurementCallback(instrument, (float)(object)measurement, tags, state);
			}
			if (typeof(T) == typeof(double))
			{
				_doubleMeasurementCallback(instrument, (double)(object)measurement, tags, state);
			}
			if (typeof(T) == typeof(decimal))
			{
				_decimalMeasurementCallback(instrument, (decimal)(object)measurement, tags, state);
			}
		}
	}
	internal readonly struct ListenerSubscription
	{
		internal MeterListener Listener { get; }

		internal object State { get; }

		internal ListenerSubscription(MeterListener listener, object state = null)
		{
			Listener = listener;
			State = state;
		}
	}
	[EventSource(Name = "System.Diagnostics.Metrics")]
	internal sealed class MetricsEventSource : EventSource
	{
		public static class Keywords
		{
			public const EventKeywords Messages = (EventKeywords)1L;

			public const EventKeywords TimeSeriesValues = (EventKeywords)2L;

			public const EventKeywords InstrumentPublishing = (EventKeywords)4L;
		}

		private sealed class CommandHandler
		{
			private AggregationManager _aggregationManager;

			private string _sessionId = "";

			private static readonly char[] s_instrumentSeperators = new char[4] { '\r', '\n', ',', ';' };

			public MetricsEventSource Parent { get; private set; }

			public CommandHandler(MetricsEventSource parent)
			{
				Parent = parent;
			}

			public void OnEventCommand(EventCommandEventArgs command)
			{
				try
				{
					if (command.Command == EventCommand.Update || command.Command == EventCommand.Disable || command.Command == EventCommand.Enable)
					{
						if (_aggregationManager != null)
						{
							if (command.Command == EventCommand.Enable || command.Command == EventCommand.Update)
							{
								Parent.MultipleSessionsNotSupportedError(_sessionId);
								return;
							}
							_aggregationManager.Dispose();
							_aggregationManager = null;
							Parent.Message("Previous session with id " + _sessionId + " is stopped");
						}
						_sessionId = "";
					}
					if ((command.Command != EventCommand.Update && command.Command != EventCommand.Enable) || command.Arguments == null)
					{
						return;
					}
					if (command.Arguments.TryGetValue("SessionId", out var value))
					{
						_sessionId = value;
						Parent.Message("SessionId argument received: " + _sessionId);
					}
					else
					{
						_sessionId = Guid.NewGuid().ToString();
						Parent.Message("New session started. SessionId auto-generated: " + _sessionId);
					}
					double num = 1.0;
					double result = num;
					if (command.Arguments.TryGetValue("RefreshInterval", out var value2))
					{
						Parent.Message("RefreshInterval argument received: " + value2);
						if (!double.TryParse(value2, out result))
						{
							Parent.Message($"Failed to parse RefreshInterval. Using default {num}s.");
							result = num;
						}
						else if (result < 0.1)
						{
							Parent.Message($"RefreshInterval too small. Using minimum interval {0.1} seconds.");
							result = 0.1;
						}
					}
					else
					{
						Parent.Message($"No RefreshInterval argument received. Using default {num}s.");
						result = num;
					}
					int num2 = 1000;
					int result2;
					if (command.Arguments.TryGetValue("MaxTimeSeries", out var value3))
					{
						Parent.Message("MaxTimeSeries argument received: " + value3);
						if (!int.TryParse(value3, out result2))
						{
							Parent.Message($"Failed to parse MaxTimeSeries. Using default {num2}");
							result2 = num2;
						}
					}
					else
					{
						Parent.Message($"No MaxTimeSeries argument received. Using default {num2}");
						result2 = num2;
					}
					int num3 = 20;
					int result3;
					if (command.Arguments.TryGetValue("MaxHistograms", out var value4))
					{
						Parent.Message("MaxHistograms argument received: " + value4);
						if (!int.TryParse(value4, out result3))
						{
							Parent.Message($"Failed to parse MaxHistograms. Using default {num3}");
							result3 = num3;
						}
					}
					else
					{
						Parent.Message($"No MaxHistogram argument received. Using default {num3}");
						result3 = num3;
					}
					string sessionId = _sessionId;
					_aggregationManager = new AggregationManager(result2, result3, delegate(Instrument i, LabeledAggregationStatistics s)
					{
						TransmitMetricValue(i, s, sessionId);
					}, delegate(DateTime startIntervalTime, DateTime endIntervalTime)
					{
						Parent.CollectionStart(sessionId, startIntervalTime, endIntervalTime);
					}, delegate(DateTime startIntervalTime, DateTime endIntervalTime)
					{
						Parent.CollectionStop(sessionId, startIntervalTime, endIntervalTime);
					}, delegate(Instrument i)
					{
						Parent.BeginInstrumentReporting(sessionId, i.Meter.Name, i.Meter.Version, i.Name, i.GetType().Name, i.Unit, i.Description);
					}, delegate(Instrument i)
					{
						Parent.EndInstrumentReporting(sessionId, i.Meter.Name, i.Meter.Version, i.Name, i.GetType().Name, i.Unit, i.Description);
					}, delegate(Instrument i)
					{
						Parent.InstrumentPublished(sessionId, i.Meter.Name, i.Meter.Version, i.Name, i.GetType().Name, i.Unit, i.Description);
					}, delegate
					{
						Parent.InitialInstrumentEnumerationComplete(sessionId);
					}, delegate(Exception ex)
					{
						Parent.Error(sessionId, ex.ToString());
					}, delegate
					{
						Parent.TimeSeriesLimitReached(sessionId);
					}, delegate
					{
						Parent.HistogramLimitReached(sessionId);
					}, delegate(Exception ex)
					{
						Parent.ObservableInstrumentCallbackError(sessionId, ex.ToString());
					});
					_aggregationManager.SetCollectionPeriod(TimeSpan.FromSeconds(result));
					if (command.Arguments.TryGetValue("Metrics", out var value5))
					{
						Parent.Message("Metrics argument received: " + value5);
						ParseSpecs(value5);
					}
					else
					{
						Parent.Message("No Metrics argument received");
					}
					_aggregationManager.Start();
				}
				catch (Exception e) when (LogError(e))
				{
				}
			}

			private bool LogError(Exception e)
			{
				Parent.Error(_sessionId, e.ToString());
				return false;
			}

			[UnsupportedOSPlatform("browser")]
			private void ParseSpecs(string metricsSpecs)
			{
				if (metricsSpecs == null)
				{
					return;
				}
				string[] array = metricsSpecs.Split(s_instrumentSeperators, StringSplitOptions.RemoveEmptyEntries);
				string[] array2 = array;
				foreach (string text in array2)
				{
					if (!MetricSpec.TryParse(text, out var spec))
					{
						Parent.Message("Failed to parse metric spec: " + text);
						continue;
					}
					Parent.Message($"Parsed metric: {spec}");
					if (spec.InstrumentName != null)
					{
						_aggregationManager.Include(spec.MeterName, spec.InstrumentName);
					}
					else
					{
						_aggregationManager.Include(spec.MeterName);
					}
				}
			}

			private void TransmitMetricValue(Instrument instrument, LabeledAggregationStatistics stats, string sessionId)
			{
				if (stats.AggregationStatistics is RateStatistics rateStatistics)
				{
					Log.CounterRateValuePublished(sessionId, instrument.Meter.Name, instrument.Meter.Version, instrument.Name, instrument.Unit, FormatTags(stats.Labels), rateStatistics.Delta.HasValue ? rateStatistics.Delta.Value.ToString(CultureInfo.InvariantCulture) : "");
				}
				else if (stats.AggregationStatistics is LastValueStatistics lastValueStatistics)
				{
					Log.GaugeValuePublished(sessionId, instrument.Meter.Name, instrument.Meter.Version, instrument.Name, instrument.Unit, FormatTags(stats.Labels), lastValueStatistics.LastValue.HasValue ? lastValueStatistics.LastValue.Value.ToString(CultureInfo.InvariantCulture) : "");
				}
				else if (stats.AggregationStatistics is HistogramStatistics histogramStatistics)
				{
					Log.HistogramValuePublished(sessionId, instrument.Meter.Name, instrument.Meter.Version, instrument.Name, instrument.Unit, FormatTags(stats.Labels), FormatQuantiles(histogramStatistics.Quantiles));
				}
			}

			private string FormatTags(KeyValuePair<string, string>[] labels)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < labels.Length; i++)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}={1}", labels[i].Key, labels[i].Value);
					if (i != labels.Length - 1)
					{
						stringBuilder.Append(',');
					}
				}
				return stringBuilder.ToString();
			}

			private string FormatQuantiles(QuantileValue[] quantiles)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < quantiles.Length; i++)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}={1}", quantiles[i].Quantile, quantiles[i].Value);
					if (i != quantiles.Length - 1)
					{
						stringBuilder.Append(';');
					}
				}
				return stringBuilder.ToString();
			}
		}

		private class MetricSpec
		{
			private const char MeterInstrumentSeparator = '\\';

			public string MeterName { get; private set; }

			public string InstrumentName { get; private set; }

			public MetricSpec(string meterName, string instrumentName)
			{
				MeterName = meterName;
				InstrumentName = instrumentName;
			}

			public static bool TryParse(string text, out MetricSpec spec)
			{
				int num = text.IndexOf('\\');
				if (num == -1)
				{
					spec = new MetricSpec(text.Trim(), null);
					return true;
				}
				string meterName = text.Substring(0, num).Trim();
				string instrumentName = text.Substring(num + 1).Trim();
				spec = new MetricSpec(meterName, instrumentName);
				return true;
			}

			public override string ToString()
			{
				if (InstrumentName == null)
				{
					return MeterName;
				}
				return MeterName + "\\" + InstrumentName;
			}
		}

		public static readonly MetricsEventSource Log = new MetricsEventSource();

		private CommandHandler _handler;

		private CommandHandler Handler
		{
			get
			{
				if (_handler == null)
				{
					Interlocked.CompareExchange(ref _handler, new CommandHandler(this), null);
				}
				return _handler;
			}
		}

		private MetricsEventSource()
		{
		}

		[Event(1, Keywords = (EventKeywords)1L)]
		public void Message(string Message)
		{
			WriteEvent(1, Message);
		}

		[Event(2, Keywords = (EventKeywords)2L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void CollectionStart(string sessionId, DateTime intervalStartTime, DateTime intervalEndTime)
		{
			WriteEvent(2, sessionId, intervalStartTime, intervalEndTime);
		}

		[Event(3, Keywords = (EventKeywords)2L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void CollectionStop(string sessionId, DateTime intervalStartTime, DateTime intervalEndTime)
		{
			WriteEvent(3, sessionId, intervalStartTime, intervalEndTime);
		}

		[Event(4, Keywords = (EventKeywords)2L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void CounterRateValuePublished(string sessionId, string meterName, string meterVersion, string instrumentName, string unit, string tags, string rate)
		{
			WriteEvent(4, sessionId, meterName, meterVersion ?? "", instrumentName, unit ?? "", tags, rate);
		}

		[Event(5, Keywords = (EventKeywords)2L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void GaugeValuePublished(string sessionId, string meterName, string meterVersion, string instrumentName, string unit, string tags, string lastValue)
		{
			WriteEvent(5, sessionId, meterName, meterVersion ?? "", instrumentName, unit ?? "", tags, lastValue);
		}

		[Event(6, Keywords = (EventKeywords)2L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void HistogramValuePublished(string sessionId, string meterName, string meterVersion, string instrumentName, string unit, string tags, string quantiles)
		{
			WriteEvent(6, sessionId, meterName, meterVersion ?? "", instrumentName, unit ?? "", tags, quantiles);
		}

		[Event(7, Keywords = (EventKeywords)2L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void BeginInstrumentReporting(string sessionId, string meterName, string meterVersion, string instrumentName, string instrumentType, string unit, string description)
		{
			WriteEvent(7, sessionId, meterName, meterVersion ?? "", instrumentName, instrumentType, unit ?? "", description ?? "");
		}

		[Event(8, Keywords = (EventKeywords)2L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void EndInstrumentReporting(string sessionId, string meterName, string meterVersion, string instrumentName, string instrumentType, string unit, string description)
		{
			WriteEvent(8, sessionId, meterName, meterVersion ?? "", instrumentName, instrumentType, unit ?? "", description ?? "");
		}

		[Event(9, Keywords = (EventKeywords)7L)]
		public void Error(string sessionId, string errorMessage)
		{
			WriteEvent(9, sessionId, errorMessage);
		}

		[Event(10, Keywords = (EventKeywords)6L)]
		public void InitialInstrumentEnumerationComplete(string sessionId)
		{
			WriteEvent(10, sessionId);
		}

		[Event(11, Keywords = (EventKeywords)4L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void InstrumentPublished(string sessionId, string meterName, string meterVersion, string instrumentName, string instrumentType, string unit, string description)
		{
			WriteEvent(11, sessionId, meterName, meterVersion ?? "", instrumentName, instrumentType, unit ?? "", description ?? "");
		}

		[Event(12, Keywords = (EventKeywords)2L)]
		public void TimeSeriesLimitReached(string sessionId)
		{
			WriteEvent(12, sessionId);
		}

		[Event(13, Keywords = (EventKeywords)2L)]
		public void HistogramLimitReached(string sessionId)
		{
			WriteEvent(13, sessionId);
		}

		[Event(14, Keywords = (EventKeywords)2L)]
		public void ObservableInstrumentCallbackError(string sessionId, string errorMessage)
		{
			WriteEvent(14, sessionId, errorMessage);
		}

		[Event(15, Keywords = (EventKeywords)7L)]
		public void MultipleSessionsNotSupportedError(string runningSessionId)
		{
			WriteEvent(15, runningSessionId);
		}

		[NonEvent]
		protected override void OnEventCommand(EventCommandEventArgs command)
		{
			lock (this)
			{
				Handler.OnEventCommand(command);
			}
		}
	}
	internal struct ObjectSequence1 : IEquatable<ObjectSequence1>, IObjectSequence
	{
		public object Value1;

		public object this[int i]
		{
			get
			{
				if (i == 0)
				{
					return Value1;
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (i == 0)
				{
					Value1 = value;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		public ObjectSequence1(object value1)
		{
			Value1 = value1;
		}

		public override int GetHashCode()
		{
			return Value1?.GetHashCode() ?? 0;
		}

		public bool Equals(ObjectSequence1 other)
		{
			if (Value1 != null)
			{
				return Value1.Equals(other.Value1);
			}
			return other.Value1 == null;
		}

		public override bool Equals(object obj)
		{
			if (obj is ObjectSequence1 other)
			{
				return Equals(other);
			}
			return false;
		}
	}
	internal struct ObjectSequence2 : IEquatable<ObjectSequence2>, IObjectSequence
	{
		public object Value1;

		public object Value2;

		public object this[int i]
		{
			get
			{
				return i switch
				{
					0 => Value1, 
					1 => Value2, 
					_ => throw new IndexOutOfRangeException(), 
				};
			}
			set
			{
				switch (i)
				{
				case 0:
					Value1 = value;
					break;
				case 1:
					Value2 = value;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		public ObjectSequence2(object value1, object value2)
		{
			Value1 = value1;
			Value2 = value2;
		}

		public bool Equals(ObjectSequence2 other)
		{
			if ((Value1 == null) ? (other.Value1 == null) : Value1.Equals(other.Value1))
			{
				if (Value2 != null)
				{
					return Value2.Equals(other.Value2);
				}
				return other.Value2 == null;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is ObjectSequence2 other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (Value1?.GetHashCode() ?? 0) ^ (Value2?.GetHashCode() ?? 0);
		}
	}
	internal struct ObjectSequence3 : IEquatable<ObjectSequence3>, IObjectSequence
	{
		public object Value1;

		public object Value2;

		public object Value3;

		public object this[int i]
		{
			get
			{
				return i switch
				{
					0 => Value1, 
					1 => Value2, 
					2 => Value3, 
					_ => throw new IndexOutOfRangeException(), 
				};
			}
			set
			{
				switch (i)
				{
				case 0:
					Value1 = value;
					break;
				case 1:
					Value2 = value;
					break;
				case 2:
					Value3 = value;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		public ObjectSequence3(object value1, object value2, object value3)
		{
			Value1 = value1;
			Value2 = value2;
			Value3 = value3;
		}

		public bool Equals(ObjectSequence3 other)
		{
			if (((Value1 == null) ? (other.Value1 == null) : Value1.Equals(other.Value1)) && ((Value2 == null) ? (other.Value2 == null) : Value2.Equals(other.Value2)))
			{
				if (Value3 != null)
				{
					return Value3.Equals(other.Value3);
				}
				return other.Value3 == null;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is ObjectSequence3 other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (Value1?.GetHashCode() ?? 0) ^ (Value2?.GetHashCode() ?? 0) ^ (Value3?.GetHashCode() ?? 0);
		}
	}
	internal struct ObjectSequenceMany : IEquatable<ObjectSequenceMany>, IObjectSequence
	{
		private readonly object[] _values;

		public object this[int i]
		{
			get
			{
				return _values[i];
			}
			set
			{
				_values[i] = value;
			}
		}

		public ObjectSequenceMany(object[] values)
		{
			_values = values;
		}

		public bool Equals(ObjectSequenceMany other)
		{
			if (_values.Length != other._values.Length)
			{
				return false;
			}
			for (int i = 0; i < _values.Length; i++)
			{
				object obj = _values[i];
				object obj2 = other._values[i];
				if (obj == null)
				{
					if (obj2 != null)
					{
						return false;
					}
				}
				else if (!obj.Equals(obj2))
				{
					return false;
				}
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			if (obj is ObjectSequenceMany other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < _values.Length; i++)
			{
				num <<= 3;
				object obj = _values[i];
				if (obj != null)
				{
					num ^= obj.GetHashCode();
				}
			}
			return num;
		}
	}
	internal interface IObjectSequence
	{
		object this[int i] { get; set; }
	}
	public sealed class ObservableCounter<T> : ObservableInstrument<T> where T : struct
	{
		private object _callback;

		internal ObservableCounter(Meter meter, string name, Func<T> observeValue, string unit, string description)
			: base(meter, name, unit, description)
		{
			if (observeValue == null)
			{
				throw new ArgumentNullException("observeValue");
			}
			_callback = observeValue;
			Publish();
		}

		internal ObservableCounter(Meter meter, string name, Func<Measurement<T>> observeValue, string unit, string description)
			: base(meter, name, unit, description)
		{
			if (observeValue == null)
			{
				throw new ArgumentNullException("observeValue");
			}
			_callback = observeValue;
			Publish();
		}

		internal ObservableCounter(Meter meter, string name, Func<IEnumerable<Measurement<T>>> observeValues, string unit, string description)
			: base(meter, name, unit, description)
		{
			if (observeValues == null)
			{
				throw new ArgumentNullException("observeValues");
			}
			_callback = observeValues;
			Publish();
		}

		protected override IEnumerable<Measurement<T>> Observe()
		{
			return Observe(_callback);
		}
	}
	public sealed class ObservableGauge<T> : ObservableInstrument<T> where T : struct
	{
		private object _callback;

		internal ObservableGauge(Meter meter, string name, Func<T> observeValue, string unit, string description)
			: base(meter, name, unit, description)
		{
			if (observeValue == null)
			{
				throw new ArgumentNullException("observeValue");
			}
			_callback = observeValue;
			Publish();
		}

		internal ObservableGauge(Meter meter, string name, Func<Measurement<T>> observeValue, string unit, string description)
			: base(meter, name, unit, description)
		{
			if (observeValue == null)
			{
				throw new ArgumentNullException("observeValue");
			}
			_callback = observeValue;
			Publish();
		}

		internal ObservableGauge(Meter meter, string name, Func<IEnumerable<Measurement<T>>> observeValues, string unit, string description)
			: base(meter, name, unit, description)
		{
			if (observeValues == null)
			{
				throw new ArgumentNullException("observeValues");
			}
			_callback = observeValues;
			Publish();
		}

		protected override IEnumerable<Measurement<T>> Observe()
		{
			return Observe(_callback);
		}
	}
	public abstract class ObservableInstrument<T> : Instrument where T : struct
	{
		public override bool IsObservable => true;

		protected ObservableInstrument(Meter meter, string name, string? unit, string? description)
			: base(meter, name, unit, description)
		{
			Instrument.ValidateTypeParameter<T>();
		}

		protected abstract IEnumerable<Measurement<T>> Observe();

		internal override void Observe(MeterListener listener)
		{
			object subscriptionState = GetSubscriptionState(listener);
			IEnumerable<Measurement<T>> enumerable = Observe();
			if (enumerable == null)
			{
				return;
			}
			foreach (Measurement<T> item in enumerable)
			{
				listener.NotifyMeasurement(this, item.Value, item.Tags, subscriptionState);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal IEnumerable<Measurement<T>> Observe(object callback)
		{
			if (callback is Func<T> func)
			{
				return new Measurement<T>[1]
				{
					new Measurement<T>(func())
				};
			}
			if (callback is Func<Measurement<T>> func2)
			{
				return new Measurement<T>[1] { func2() };
			}
			if (callback is Func<IEnumerable<Measurement<T>>> func3)
			{
				return func3();
			}
			return null;
		}
	}
	internal sealed class RateSumAggregator : Aggregator
	{
		private double _sum;

		public override void Update(double value)
		{
			lock (this)
			{
				_sum += value;
			}
		}

		public override IAggregationStatistics Collect()
		{
			lock (this)
			{
				RateStatistics result = new RateStatistics(_sum);
				_sum = 0.0;
				return result;
			}
		}
	}
	internal sealed class RateAggregator : Aggregator
	{
		private double? _prevValue;

		private double _value;

		public override void Update(double value)
		{
			lock (this)
			{
				_value = value;
			}
		}

		public override IAggregationStatistics Collect()
		{
			lock (this)
			{
				double? delta = null;
				if (_prevValue.HasValue)
				{
					delta = _value - _prevValue.Value;
				}
				RateStatistics result = new RateStatistics(delta);
				_prevValue = _value;
				return result;
			}
		}
	}
	internal sealed class RateStatistics : IAggregationStatistics
	{
		public double? Delta { get; }

		public RateStatistics(double? delta)
		{
			Delta = delta;
		}
	}
	internal struct StringSequence1 : IEquatable<StringSequence1>, IStringSequence
	{
		public string Value1;

		public string this[int i]
		{
			get
			{
				if (i == 0)
				{
					return Value1;
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (i == 0)
				{
					Value1 = value;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		public int Length => 1;

		public StringSequence1(string value1)
		{
			Value1 = value1;
		}

		public override int GetHashCode()
		{
			return Value1.GetHashCode();
		}

		public bool Equals(StringSequence1 other)
		{
			return Value1 == other.Value1;
		}

		public override bool Equals(object obj)
		{
			if (obj is StringSequence1 other)
			{
				return Equals(other);
			}
			return false;
		}
	}
	internal struct StringSequence2 : IEquatable<StringSequence2>, IStringSequence
	{
		public string Value1;

		public string Value2;

		public string this[int i]
		{
			get
			{
				return i switch
				{
					0 => Value1, 
					1 => Value2, 
					_ => throw new IndexOutOfRangeException(), 
				};
			}
			set
			{
				switch (i)
				{
				case 0:
					Value1 = value;
					break;
				case 1:
					Value2 = value;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		public int Length => 2;

		public StringSequence2(string value1, string value2)
		{
			Value1 = value1;
			Value2 = value2;
		}

		public bool Equals(StringSequence2 other)
		{
			if (Value1 == other.Value1)
			{
				return Value2 == other.Value2;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is StringSequence2 other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (Value1?.GetHashCode() ?? 0) ^ (Value2?.GetHashCode() ?? 0);
		}
	}
	internal struct StringSequence3 : IEquatable<StringSequence3>, IStringSequence
	{
		public string Value1;

		public string Value2;

		public string Value3;

		public string this[int i]
		{
			get
			{
				return i switch
				{
					0 => Value1, 
					1 => Value2, 
					2 => Value3, 
					_ => throw new IndexOutOfRangeException(), 
				};
			}
			set
			{
				switch (i)
				{
				case 0:
					Value1 = value;
					break;
				case 1:
					Value2 = value;
					break;
				case 2:
					Value3 = value;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		public int Length => 3;

		public StringSequence3(string value1, string value2, string value3)
		{
			Value1 = value1;
			Value2 = value2;
			Value3 = value3;
		}

		public bool Equals(StringSequence3 other)
		{
			if (Value1 == other.Value1 && Value2 == other.Value2)
			{
				return Value3 == other.Value3;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is StringSequence3 other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (Value1?.GetHashCode() ?? 0) ^ (Value2?.GetHashCode() ?? 0) ^ (Value3?.GetHashCode() ?? 0);
		}
	}
	internal struct StringSequenceMany : IEquatable<StringSequenceMany>, IStringSequence
	{
		private readonly string[] _values;

		public string this[int i]
		{
			get
			{
				return _values[i];
			}
			set
			{
				_values[i] = value;
			}
		}

		public int Length => _values.Length;

		public StringSequenceMany(string[] values)
		{
			_values = values;
		}

		public Span<string> AsSpan()
		{
			return MemoryExtensions.AsSpan(_values);
		}

		public bool Equals(StringSequenceMany other)
		{
			if (_values.Length != other._values.Length)
			{
				return false;
			}
			for (int i = 0; i < _values.Length; i++)
			{
				if (_values[i] != other._values[i])
				{
					return false;
				}
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			if (obj is StringSequenceMany other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < _values.Length; i++)
			{
				num <<= 3;
				num ^= _values[i].GetHashCode();
			}
			return num;
		}
	}
	internal interface IStringSequence
	{
		string this[int i] { get; set; }

		int Length { get; }
	}
}
namespace System.Diagnostics.CodeAnalysis
{
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class DynamicDependencyAttribute : Attribute
	{
		public string MemberSignature { get; }

		public DynamicallyAccessedMemberTypes MemberTypes { get; }

		public Type Type { get; }

		public string TypeName { get; }

		public string AssemblyName { get; }

		public string Condition { get; set; }

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
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	internal sealed class RequiresUnreferencedCodeAttribute : Attribute
	{
		public string Message { get; }

		public string Url { get; set; }

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

		public string Scope { get; set; }

		public string Target { get; set; }

		public string MessageId { get; set; }

		public string Justification { get; set; }

		public UnconditionalSuppressMessageAttribute(string category, string checkId)
		{
			Category = category;
			CheckId = checkId;
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
