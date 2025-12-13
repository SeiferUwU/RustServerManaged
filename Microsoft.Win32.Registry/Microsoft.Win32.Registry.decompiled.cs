using System;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using FxResources.Microsoft.Win32.Registry;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: AssemblyTitle("Microsoft.Win32.Registry")]
[assembly: AssemblyDescription("Microsoft.Win32.Registry")]
[assembly: AssemblyDefaultAlias("Microsoft.Win32.Registry")]
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyProduct("Microsoft® .NET Framework")]
[assembly: AssemblyCopyright("© Microsoft Corporation.  All rights reserved.")]
[assembly: AssemblyFileVersion("4.6.24705.01")]
[assembly: AssemblyInformationalVersion("4.6.24705.01. Commit Hash: 4d1af962ca0fede10beb01d197367c2f90e92c97")]
[assembly: CLSCompliant(true)]
[assembly: AssemblyMetadata(".NETFrameworkAssembly", "")]
[assembly: AssemblyMetadata("Serviceable", "True")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("4.0.1.0")]
[assembly: TypeForwardedTo(typeof(Registry))]
[assembly: TypeForwardedTo(typeof(RegistryHive))]
[assembly: TypeForwardedTo(typeof(RegistryKey))]
[assembly: TypeForwardedTo(typeof(RegistryOptions))]
[assembly: TypeForwardedTo(typeof(RegistryValueKind))]
[assembly: TypeForwardedTo(typeof(RegistryValueOptions))]
[assembly: TypeForwardedTo(typeof(RegistryView))]
[assembly: TypeForwardedTo(typeof(SafeRegistryHandle))]
[assembly: TypeForwardedTo(typeof(RegistryRights))]
[module: UnverifiableCode]
namespace FxResources.Microsoft.Win32.Registry
{
	internal static class SR
	{
	}
}
namespace System
{
	internal static class SR
	{
		private static ResourceManager s_resourceManager;

		private const string s_resourcesName = "FxResources.Microsoft.Win32.Registry.SR";

		private static ResourceManager ResourceManager
		{
			get
			{
				if (s_resourceManager == null)
				{
					s_resourceManager = new ResourceManager(ResourceType);
				}
				return s_resourceManager;
			}
		}

		internal static string Arg_RegSubKeyAbsent => GetResourceString("Arg_RegSubKeyAbsent", null);

		internal static string Arg_RegKeyDelHive => GetResourceString("Arg_RegKeyDelHive", null);

		internal static string Arg_RegKeyNoRemoteConnect => GetResourceString("Arg_RegKeyNoRemoteConnect", null);

		internal static string Arg_RegKeyOutOfRange => GetResourceString("Arg_RegKeyOutOfRange", null);

		internal static string Arg_RegKeyNotFound => GetResourceString("Arg_RegKeyNotFound", null);

		internal static string Arg_RegKeyStrLenBug => GetResourceString("Arg_RegKeyStrLenBug", null);

		internal static string Arg_RegValStrLenBug => GetResourceString("Arg_RegValStrLenBug", null);

		internal static string Arg_RegBadKeyKind => GetResourceString("Arg_RegBadKeyKind", null);

		internal static string Arg_RegGetOverflowBug => GetResourceString("Arg_RegGetOverflowBug", null);

		internal static string Arg_RegSetMismatchedKind => GetResourceString("Arg_RegSetMismatchedKind", null);

		internal static string Arg_RegSetBadArrType => GetResourceString("Arg_RegSetBadArrType", null);

		internal static string Arg_RegSetStrArrNull => GetResourceString("Arg_RegSetStrArrNull", null);

		internal static string Arg_RegInvalidKeyName => GetResourceString("Arg_RegInvalidKeyName", null);

		internal static string Arg_DllInitFailure => GetResourceString("Arg_DllInitFailure", null);

		internal static string Arg_EnumIllegalVal => GetResourceString("Arg_EnumIllegalVal", null);

		internal static string Arg_RegSubKeyValueAbsent => GetResourceString("Arg_RegSubKeyValueAbsent", null);

		internal static string Argument_InvalidRegistryOptionsCheck => GetResourceString("Argument_InvalidRegistryOptionsCheck", null);

		internal static string Argument_InvalidRegistryViewCheck => GetResourceString("Argument_InvalidRegistryViewCheck", null);

		internal static string Argument_InvalidRegistryKeyPermissionCheck => GetResourceString("Argument_InvalidRegistryKeyPermissionCheck", null);

		internal static string InvalidOperation_RegRemoveSubKey => GetResourceString("InvalidOperation_RegRemoveSubKey", null);

		internal static string ObjectDisposed_RegKeyClosed => GetResourceString("ObjectDisposed_RegKeyClosed", null);

		internal static string Security_RegistryPermission => GetResourceString("Security_RegistryPermission", null);

		internal static string UnauthorizedAccess_RegistryKeyGeneric_Key => GetResourceString("UnauthorizedAccess_RegistryKeyGeneric_Key", null);

		internal static string UnauthorizedAccess_RegistryNoWrite => GetResourceString("UnauthorizedAccess_RegistryNoWrite", null);

		internal static string UnknownError_Num => GetResourceString("UnknownError_Num", null);

		internal static Type ResourceType => typeof(FxResources.Microsoft.Win32.Registry.SR);

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string text = null;
			try
			{
				text = ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			if (defaultString != null && resourceKey.Equals(text, StringComparison.Ordinal))
			{
				return defaultString;
			}
			return text;
		}

		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args != null)
			{
				if (UsingResourceKeys())
				{
					return resourceFormat + string.Join(", ", args);
				}
				return string.Format(resourceFormat, args);
			}
			return resourceFormat;
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
	}
}
