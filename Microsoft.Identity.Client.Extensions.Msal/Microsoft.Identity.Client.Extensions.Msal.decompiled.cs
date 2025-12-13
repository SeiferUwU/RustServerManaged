#define TRACE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.Identity.Client.Extensions.Msal.Accessors;
using Microsoft.Identity.Extensions;
using Microsoft.Identity.Extensions.Mac;
using Microsoft.Win32.SafeHandles;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: InternalsVisibleTo("Microsoft.Identity.Test.Unit, PublicKey=00240000048000009400000006020000002400005253413100040000010001002D96616729B54F6D013D71559A017F50AA4861487226C523959D1579B93F3FDF71C08B980FD3130062B03D3DE115C4B84E7AC46AEF5E192A40E7457D5F3A08F66CEAB71143807F2C3CB0DA5E23B38F0559769978406F6E5D30CEADD7985FC73A5A609A8B74A1DF0A29399074A003A226C943D480FEC96DBEC7106A87896539AD")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2,             PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]
[assembly: InternalsVisibleTo("Microsoft.Identity.Test.Unit, PublicKey=00240000048000009400000006020000002400005253413100040000010001002d96616729b54f6d013d71559a017f50aa4861487226c523959d1579b93f3fdf71c08b980fd3130062b03d3de115c4b84e7ac46aef5e192a40e7457d5f3a08f66ceab71143807f2c3cb0da5e23b38f0559769978406f6e5d30ceadd7985fc73a5a609a8b74a1df0a29399074a003a226c943d480fec96dbec7106a87896539ad")]
[assembly: InternalsVisibleTo("Microsoft.Identity.Test.Integration.NetCore, PublicKey=00240000048000009400000006020000002400005253413100040000010001002d96616729b54f6d013d71559a017f50aa4861487226c523959d1579b93f3fdf71c08b980fd3130062b03d3de115c4b84e7ac46aef5e192a40e7457d5f3a08f66ceab71143807f2c3cb0da5e23b38f0559769978406f6e5d30ceadd7985fc73a5a609a8b74a1df0a29399074a003a226c943d480fec96dbec7106a87896539ad")]
[assembly: InternalsVisibleTo("Microsoft.Identity.Test.Integration.NetFx, PublicKey=00240000048000009400000006020000002400005253413100040000010001002d96616729b54f6d013d71559a017f50aa4861487226c523959d1579b93f3fdf71c08b980fd3130062b03d3de115c4b84e7ac46aef5e192a40e7457d5f3a08f66ceab71143807f2c3cb0da5e23b38f0559769978406f6e5d30ceadd7985fc73a5a609a8b74a1df0a29399074a003a226c943d480fec96dbec7106a87896539ad")]
[assembly: TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = "")]
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("© Microsoft Corporation. All rights reserved.")]
[assembly: AssemblyDescription("This package contains the public client (desktop) caching to Microsoft Authentication Library for .NET (MSAL.NET)")]
[assembly: AssemblyFileVersion("4.61.3.0")]
[assembly: AssemblyInformationalVersion("4.61.3+88df64013795d4e3716e1f677600f8164ffdb542")]
[assembly: AssemblyProduct("Microsoft Authentication Library")]
[assembly: AssemblyTitle("Microsoft.Identity.Client.Extensions.Msal")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/AzureAD/microsoft-authentication-library-for-dotnet")]
[assembly: AssemblyVersion("4.61.3.0")]
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
namespace Microsoft.Identity.Extensions
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	internal class InteropException : Exception
	{
		public int ErrorCode { get; }

		private string DebuggerDisplay => $"{Message} [0x{ErrorCode:x}]";

		public InteropException()
		{
		}

		public InteropException(string message, int errorCode)
			: base(message + " .Error code: " + errorCode)
		{
			ErrorCode = errorCode;
		}

		public InteropException(string message, int errorCode, Exception innerException)
			: base(message + ". Error code: " + errorCode, innerException)
		{
			ErrorCode = errorCode;
		}
	}
}
namespace Microsoft.Identity.Extensions.Mac
{
	internal static class CoreFoundation
	{
		private const string CoreFoundationFrameworkLib = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

		public static readonly IntPtr Handle;

		public static readonly IntPtr kCFBooleanTrue;

		public static readonly IntPtr kCFBooleanFalse;

		static CoreFoundation()
		{
			Handle = LibSystem.dlopen("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", 0);
			kCFBooleanTrue = LibSystem.GetGlobal(Handle, "kCFBooleanTrue");
			kCFBooleanFalse = LibSystem.GetGlobal(Handle, "kCFBooleanFalse");
		}

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr CFArrayCreateMutable(IntPtr allocator, long capacity, IntPtr callbacks);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void CFArrayInsertValueAtIndex(IntPtr theArray, long idx, IntPtr value);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern long CFArrayGetCount(IntPtr theArray);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr CFArrayGetValueAtIndex(IntPtr theArray, long idx);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr CFDictionaryCreateMutable(IntPtr allocator, long capacity, IntPtr keyCallBacks, IntPtr valueCallBacks);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void CFDictionaryAddValue(IntPtr theDict, IntPtr key, IntPtr value);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr CFDictionaryGetValue(IntPtr theDict, IntPtr key);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern bool CFDictionaryGetValueIfPresent(IntPtr theDict, IntPtr key, out IntPtr value);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr CFStringCreateWithBytes(IntPtr alloc, byte[] bytes, long numBytes, CFStringEncoding encoding, bool isExternalRepresentation);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern long CFStringGetLength(IntPtr theString);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern bool CFStringGetCString(IntPtr theString, IntPtr buffer, long bufferSize, CFStringEncoding encoding);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void CFRetain(IntPtr cf);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void CFRelease(IntPtr cf);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int CFGetTypeID(IntPtr cf);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int CFStringGetTypeID();

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int CFDataGetTypeID();

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int CFDictionaryGetTypeID();

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int CFArrayGetTypeID();

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr CFDataGetBytePtr(IntPtr theData);

		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int CFDataGetLength(IntPtr theData);
	}
	internal enum CFStringEncoding
	{
		kCFStringEncodingUTF8 = 134217984
	}
	internal static class LibSystem
	{
		private const string LibSystemLib = "/usr/lib/libSystem.dylib";

		[DllImport("/usr/lib/libSystem.dylib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr dlopen(string name, int flags);

		[DllImport("/usr/lib/libSystem.dylib", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr dlsym(IntPtr handle, string symbol);

		public static IntPtr GetGlobal(IntPtr handle, string symbol)
		{
			return (IntPtr)Marshal.PtrToStructure(dlsym(handle, symbol), typeof(IntPtr));
		}
	}
	[DebuggerDisplay("{DebuggerDisplay}")]
	internal class MacOSKeychainCredential
	{
		public string Service { get; }

		public string Account { get; }

		public string Label { get; }

		public byte[] Password { get; }

		private string DebuggerDisplay => Label + " [Service: " + Service + ", Account: " + Account + "]";

		internal MacOSKeychainCredential(string service, string account, byte[] password, string label)
		{
			Service = service;
			Account = account;
			Password = password;
			Label = label;
		}
	}
	internal static class SecurityFramework
	{
		private const string SecurityFrameworkLib = "/System/Library/Frameworks/Security.framework/Security";

		public static readonly IntPtr Handle;

		public static readonly IntPtr kSecClass;

		public static readonly IntPtr kSecMatchLimit;

		public static readonly IntPtr kSecReturnAttributes;

		public static readonly IntPtr kSecReturnRef;

		public static readonly IntPtr kSecReturnPersistentRef;

		public static readonly IntPtr kSecClassGenericPassword;

		public static readonly IntPtr kSecMatchLimitOne;

		public static readonly IntPtr kSecMatchItemList;

		public static readonly IntPtr kSecAttrLabel;

		public static readonly IntPtr kSecAttrAccount;

		public static readonly IntPtr kSecAttrService;

		public static readonly IntPtr kSecValueRef;

		public static readonly IntPtr kSecValueData;

		public static readonly IntPtr kSecReturnData;

		public const int CallerSecuritySession = -1;

		public const int OK = 0;

		public const int ErrorSecNoSuchKeychain = -25294;

		public const int ErrorSecInvalidKeychain = -25295;

		public const int ErrorSecAuthFailed = -25293;

		public const int ErrorSecDuplicateItem = -25299;

		public const int ErrorSecItemNotFound = -25300;

		public const int ErrorSecInteractionNotAllowed = -25308;

		public const int ErrorSecInteractionRequired = -25315;

		public const int ErrorSecNoSuchAttr = -25303;

		public const int ErrSecUserCanceled = -128;

		static SecurityFramework()
		{
			Handle = LibSystem.dlopen("/System/Library/Frameworks/Security.framework/Security", 0);
			kSecClass = LibSystem.GetGlobal(Handle, "kSecClass");
			kSecMatchLimit = LibSystem.GetGlobal(Handle, "kSecMatchLimit");
			kSecReturnAttributes = LibSystem.GetGlobal(Handle, "kSecReturnAttributes");
			kSecReturnRef = LibSystem.GetGlobal(Handle, "kSecReturnRef");
			kSecReturnPersistentRef = LibSystem.GetGlobal(Handle, "kSecReturnPersistentRef");
			kSecClassGenericPassword = LibSystem.GetGlobal(Handle, "kSecClassGenericPassword");
			kSecMatchLimitOne = LibSystem.GetGlobal(Handle, "kSecMatchLimitOne");
			kSecMatchItemList = LibSystem.GetGlobal(Handle, "kSecMatchItemList");
			kSecAttrLabel = LibSystem.GetGlobal(Handle, "kSecAttrLabel");
			kSecAttrAccount = LibSystem.GetGlobal(Handle, "kSecAttrAccount");
			kSecAttrService = LibSystem.GetGlobal(Handle, "kSecAttrService");
			kSecValueRef = LibSystem.GetGlobal(Handle, "kSecValueRef");
			kSecValueData = LibSystem.GetGlobal(Handle, "kSecValueData");
			kSecReturnData = LibSystem.GetGlobal(Handle, "kSecReturnData");
		}

		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SessionGetInfo(int session, out int sessionId, out SessionAttributeBits attributes);

		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecAccessCreate(IntPtr descriptor, IntPtr trustedList, out IntPtr accessRef);

		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemCreateFromContent(IntPtr itemClass, IntPtr attrList, uint length, IntPtr data, IntPtr keychainRef, IntPtr initialAccess, out IntPtr itemRef);

		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainAddGenericPassword(IntPtr keychain, uint serviceNameLength, string serviceName, uint accountNameLength, string accountName, uint passwordLength, byte[] passwordData, out IntPtr itemRef);

		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainFindGenericPassword(IntPtr keychainOrArray, uint serviceNameLength, string serviceName, uint accountNameLength, string accountName, out uint passwordLength, out IntPtr passwordData, out IntPtr itemRef);

		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemModifyAttributesAndData(IntPtr itemRef, IntPtr attrList, uint length, byte[] data);

		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemDelete(IntPtr itemRef);

		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemFreeContent(IntPtr attrList, IntPtr data);

		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemFreeAttributesAndData(IntPtr attrList, IntPtr data);

		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecItemCopyMatching(IntPtr query, out IntPtr result);

		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemCopyFromPersistentReference(IntPtr persistentItemRef, out IntPtr itemRef);

		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemCopyContent(IntPtr itemRef, IntPtr itemClass, IntPtr attrList, out uint length, out IntPtr outData);

		public static void ThrowIfError(int error, string defaultErrorMessage = "Unknown error.")
		{
			switch (error)
			{
			case 0:
				break;
			case -25294:
				throw new InteropException("The keychain does not exist.", error);
			case -25295:
				throw new InteropException("The keychain is not valid.", error);
			case -25293:
				throw new InteropException("KeyChain authorization/authentication failed.", error);
			case -25299:
				throw new InteropException("KeyChain - the item already exists.", error);
			case -25300:
				throw new InteropException("KeyChain - the item cannot be found.", error);
			case -25308:
				throw new InteropException("KeyChain - interaction with the Security Server is not allowed.", error);
			case -25315:
				throw new InteropException("KeyChain - user interaction is required.", error);
			case -25303:
				throw new InteropException("KeyChain - the attribute does not exist.", error);
			case -128:
				throw new InteropException("KeyChain - user cancelled the operation.", error);
			default:
				throw new InteropException(defaultErrorMessage, error);
			}
		}
	}
	[Flags]
	internal enum SessionAttributeBits
	{
		SessionIsRoot = 1,
		SessionHasGraphicAccess = 0x10,
		SessionHasTty = 0x20,
		SessionIsRemote = 0x1000
	}
	internal struct SecKeychainAttributeInfo
	{
		public uint Count;

		public IntPtr Tag;

		public IntPtr Format;
	}
	internal struct SecKeychainAttributeList
	{
		public uint Count;

		public IntPtr Attributes;
	}
	internal struct SecKeychainAttribute
	{
		public SecKeychainAttrType Tag;

		public uint Length;

		public IntPtr Data;
	}
	internal enum CssmDbAttributeFormat : uint
	{
		String,
		SInt32,
		UInt32,
		BigNum,
		Real,
		TimeDate,
		Blob,
		MultiUInt32,
		Complex
	}
	internal enum SecKeychainAttrType : uint
	{
		AccountItem = 1633903476u
	}
}
namespace Microsoft.Identity.Client.Extensions.Msal
{
	internal class DpApiEncryptedFileAccessor : ICacheAccessor
	{
		private readonly string _cacheFilePath;

		private readonly TraceSourceLogger _logger;

		private readonly ICacheAccessor _unencryptedFileAccessor;

		public DpApiEncryptedFileAccessor(string cacheFilePath, TraceSourceLogger logger)
		{
			if (string.IsNullOrEmpty(cacheFilePath))
			{
				throw new ArgumentNullException("cacheFilePath");
			}
			_cacheFilePath = cacheFilePath;
			_logger = logger ?? throw new ArgumentNullException("logger");
			_unencryptedFileAccessor = new FileAccessor(_cacheFilePath, setOwnerOnlyPermissions: false, _logger);
		}

		public void Clear()
		{
			_logger.LogInformation("Clearing cache");
			_unencryptedFileAccessor.Clear();
		}

		public ICacheAccessor CreateForPersistenceValidation()
		{
			return new DpApiEncryptedFileAccessor(_cacheFilePath + ".test", _logger);
		}

		public byte[] Read()
		{
			byte[] array = _unencryptedFileAccessor.Read();
			if (array != null && array.Length != 0)
			{
				_logger.LogInformation("Unprotecting the data");
				array = ProtectedData.Unprotect(array, null, DataProtectionScope.CurrentUser);
			}
			return array;
		}

		public void Write(byte[] data)
		{
			if (data.Length != 0)
			{
				_logger.LogInformation("Protecting the data");
				data = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
			}
			_unencryptedFileAccessor.Write(data);
		}
	}
	internal class FileAccessor : ICacheAccessor
	{
		private readonly string _cacheFilePath;

		private readonly TraceSourceLogger _logger;

		private readonly bool _setOwnerOnlyPermission;

		internal FileAccessor(string cacheFilePath, bool setOwnerOnlyPermissions, TraceSourceLogger logger)
		{
			_cacheFilePath = cacheFilePath;
			_setOwnerOnlyPermission = setOwnerOnlyPermissions;
			_logger = logger ?? throw new ArgumentNullException("logger");
		}

		public void Clear()
		{
			_logger.LogInformation("Deleting cache file");
			FileIOWithRetries.DeleteCacheFile(_cacheFilePath, _logger);
		}

		public ICacheAccessor CreateForPersistenceValidation()
		{
			return new FileAccessor(_cacheFilePath + ".test", _setOwnerOnlyPermission, _logger);
		}

		public byte[] Read()
		{
			_logger.LogInformation("Reading from file");
			byte[] fileData = null;
			bool flag = File.Exists(_cacheFilePath);
			_logger.LogInformation($"Cache file exists? '{flag}'");
			if (flag)
			{
				FileIOWithRetries.TryProcessFile(delegate
				{
					fileData = File.ReadAllBytes(_cacheFilePath);
					_logger.LogInformation($"Read '{fileData.Length}' bytes from the file");
				}, _logger);
			}
			return fileData;
		}

		public void Write(byte[] data)
		{
			FileIOWithRetries.CreateAndWriteToFile(_cacheFilePath, data, _setOwnerOnlyPermission, _logger);
		}
	}
	internal interface ICacheAccessor
	{
		void Clear();

		byte[] Read();

		void Write(byte[] data);

		ICacheAccessor CreateForPersistenceValidation();
	}
	internal class LinuxKeyringAccessor : ICacheAccessor
	{
		private readonly TraceSourceLogger _logger;

		private IntPtr _libsecretSchema = IntPtr.Zero;

		private readonly string _cacheFilePath;

		private readonly string _keyringCollection;

		private readonly string _keyringSchemaName;

		private readonly string _keyringSecretLabel;

		private readonly string _attributeKey1;

		private readonly string _attributeValue1;

		private readonly string _attributeKey2;

		private readonly string _attributeValue2;

		public LinuxKeyringAccessor(string cacheFilePath, string keyringCollection, string keyringSchemaName, string keyringSecretLabel, string attributeKey1, string attributeValue1, string attributeKey2, string attributeValue2, TraceSourceLogger logger)
		{
			if (string.IsNullOrWhiteSpace(cacheFilePath))
			{
				throw new ArgumentNullException("cacheFilePath");
			}
			if (string.IsNullOrWhiteSpace(attributeKey1))
			{
				throw new ArgumentNullException("attributeKey1");
			}
			if (string.IsNullOrWhiteSpace(attributeValue1))
			{
				throw new ArgumentNullException("attributeValue1");
			}
			if (string.IsNullOrWhiteSpace(attributeKey2))
			{
				throw new ArgumentNullException("attributeKey2");
			}
			if (string.IsNullOrWhiteSpace(attributeValue2))
			{
				throw new ArgumentNullException("attributeValue2");
			}
			_cacheFilePath = cacheFilePath;
			_keyringCollection = keyringCollection;
			_keyringSchemaName = keyringSchemaName;
			_keyringSecretLabel = keyringSecretLabel;
			_attributeKey1 = attributeKey1;
			_attributeValue1 = attributeValue1;
			_attributeKey2 = attributeKey2;
			_attributeValue2 = attributeValue2;
			_logger = logger ?? throw new ArgumentNullException("logger");
		}

		public ICacheAccessor CreateForPersistenceValidation()
		{
			return new LinuxKeyringAccessor(_cacheFilePath + ".test", _keyringCollection, _keyringSchemaName, "MSAL Persistence Test", _attributeKey1, "test", _attributeKey2, "test", _logger);
		}

		public void Clear()
		{
			_logger.LogInformation("Clearing cache");
			FileIOWithRetries.DeleteCacheFile(_cacheFilePath, _logger);
			_logger.LogInformation("Before deleting secret from Linux keyring");
			Libsecret.secret_password_clear_sync(GetLibsecretSchema(), IntPtr.Zero, out var error, _attributeKey1, _attributeValue1, _attributeKey2, _attributeValue2, IntPtr.Zero);
			if (error != IntPtr.Zero)
			{
				try
				{
					GError gError = (GError)Marshal.PtrToStructure(error, typeof(GError));
					throw new InteropException(string.Format("An error was encountered while clearing secret from keyring in the {0} domain:'{1}' code:'{2}' message:'{3}'", "Storage", gError.Domain, gError.Code, gError.Message), gError.Code);
				}
				catch (Exception ex)
				{
					throw new InteropException(string.Format("An exception was encountered while processing libsecret error information during clearing secret in the {0} ex:'{1}'", "Storage", ex), 0, ex);
				}
			}
			_logger.LogInformation("After deleting secret from linux keyring");
		}

		public byte[] Read()
		{
			_logger.LogInformation("ReadDataCore, Before reading from linux keyring");
			byte[] array = null;
			IntPtr error;
			string text = Libsecret.secret_password_lookup_sync(GetLibsecretSchema(), IntPtr.Zero, out error, _attributeKey1, _attributeValue1, _attributeKey2, _attributeValue2, IntPtr.Zero);
			if (error != IntPtr.Zero)
			{
				try
				{
					GError gError = (GError)Marshal.PtrToStructure(error, typeof(GError));
					throw new InteropException(string.Format("An error was encountered while reading secret from keyring in the {0} domain:'{1}' code:'{2}' message:'{3}'", "Storage", gError.Domain, gError.Code, gError.Message), gError.Code);
				}
				catch (Exception ex)
				{
					throw new InteropException(string.Format("An exception was encountered while processing libsecret error information during reading in the {0} ex:'{1}'", "Storage", ex), 0, ex);
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				_logger.LogWarning("No matching secret found in the keyring");
			}
			else
			{
				_logger.LogInformation("Base64 decoding the secret string");
				array = Convert.FromBase64String(text);
				_logger.LogInformation($"ReadDataCore, read '{array?.Length}' bytes from the keyring");
			}
			return array;
		}

		public void Write(byte[] data)
		{
			_logger.LogInformation("Before saving to linux keyring");
			Libsecret.secret_password_store_sync(GetLibsecretSchema(), _keyringCollection, _keyringSecretLabel, Convert.ToBase64String(data), IntPtr.Zero, out var error, _attributeKey1, _attributeValue1, _attributeKey2, _attributeValue2, IntPtr.Zero);
			if (error != IntPtr.Zero)
			{
				try
				{
					GError gError = (GError)Marshal.PtrToStructure(error, typeof(GError));
					throw new InteropException(string.Format("An error was encountered while saving secret to keyring in the {0} domain:'{1}' code:'{2}' message:'{3}'", "Storage", gError.Domain, gError.Code, gError.Message), gError.Code);
				}
				catch (Exception innerException)
				{
					throw new InteropException("An exception was encountered while processing libsecret error information during saving in the Storage", 0, innerException);
				}
			}
			_logger.LogInformation("After saving to linux keyring");
			FileIOWithRetries.TouchFile(_cacheFilePath, _logger);
		}

		private IntPtr GetLibsecretSchema()
		{
			if (_libsecretSchema == IntPtr.Zero)
			{
				_logger.LogInformation("Before creating libsecret schema");
				_libsecretSchema = Libsecret.secret_schema_new(_keyringSchemaName, 2, _attributeKey1, 0, _attributeKey2, 0, IntPtr.Zero);
				if (_libsecretSchema == IntPtr.Zero)
				{
					throw new InteropException("Failed to create libsecret schema from the {nameof(Storage)}", 0);
				}
				_logger.LogInformation("After creating libsecret schema");
			}
			return _libsecretSchema;
		}
	}
	internal class MacKeychainAccessor : ICacheAccessor
	{
		private readonly string _cacheFilePath;

		private readonly string _service;

		private readonly string _account;

		private readonly TraceSourceLogger _logger;

		private readonly MacOSKeychain _keyChain;

		public MacKeychainAccessor(string cacheFilePath, string keyChainServiceName, string keyChainAccountName, TraceSourceLogger logger)
		{
			if (string.IsNullOrWhiteSpace(cacheFilePath))
			{
				throw new ArgumentNullException("cacheFilePath");
			}
			if (string.IsNullOrWhiteSpace(keyChainServiceName))
			{
				throw new ArgumentNullException("keyChainServiceName");
			}
			if (string.IsNullOrWhiteSpace(keyChainAccountName))
			{
				throw new ArgumentNullException("keyChainAccountName");
			}
			_cacheFilePath = cacheFilePath;
			_service = keyChainServiceName;
			_account = keyChainAccountName;
			_logger = logger ?? throw new ArgumentNullException("logger");
			_keyChain = new MacOSKeychain();
		}

		public void Clear()
		{
			_logger.LogInformation("Clearing cache");
			FileIOWithRetries.DeleteCacheFile(_cacheFilePath, _logger);
			_logger.LogInformation("Before delete mac keychain service: " + _service + " account " + _account);
			_keyChain.Remove(_service, _account);
			_logger.LogInformation("After delete mac keychain service: " + _service + " account " + _account);
		}

		public byte[] Read()
		{
			_logger.LogInformation("ReadDataCore, Before reading from mac keychain service: " + _service + " account " + _account);
			MacOSKeychainCredential macOSKeychainCredential = _keyChain.Get(_service, _account);
			_logger.LogInformation($"ReadDataCore, After reading mac keychain {(macOSKeychainCredential?.Password?.Length).GetValueOrDefault()} chars service: {_service} account {_account}");
			return macOSKeychainCredential?.Password;
		}

		public void Write(byte[] data)
		{
			_logger.LogInformation("Before write to mac keychain service: " + _service + " account " + _account);
			_keyChain.AddOrUpdate(_service, _account, data);
			_logger.LogInformation("After write to mac keychain service: " + _service + " account " + _account);
			FileIOWithRetries.TouchFile(_cacheFilePath, _logger);
		}

		public ICacheAccessor CreateForPersistenceValidation()
		{
			return new MacKeychainAccessor(_cacheFilePath + ".test", _service + Guid.NewGuid().ToString(), _account, _logger);
		}

		public override string ToString()
		{
			return "MacKeyChain accessor pointing to: service " + _service + ", account " + _account + ", file " + _cacheFilePath;
		}
	}
	public class CacheChangedEventArgs : EventArgs
	{
		public readonly IEnumerable<string> AccountsAdded;

		public readonly IEnumerable<string> AccountsRemoved;

		public CacheChangedEventArgs(IEnumerable<string> added, IEnumerable<string> removed)
		{
			AccountsAdded = added;
			AccountsRemoved = removed;
		}
	}
	internal static class FileIOWithRetries
	{
		private const int FileLockRetryCount = 20;

		private const int FileLockRetryWaitInMs = 200;

		internal static void DeleteCacheFile(string filePath, TraceSourceLogger logger)
		{
			bool flag = File.Exists(filePath);
			logger.LogInformation($"DeleteCacheFile Cache file exists '{flag}'");
			TryProcessFile(delegate
			{
				logger.LogInformation("Before deleting the cache file");
				try
				{
					File.Delete(filePath);
				}
				catch (Exception arg)
				{
					logger.LogError($"Problem deleting the cache file '{arg}'");
				}
				logger.LogInformation("After deleting the cache file.");
			}, logger);
		}

		internal static void CreateAndWriteToFile(string filePath, byte[] data, bool setChmod600, TraceSourceLogger logger)
		{
			EnsureParentDirectoryExists(filePath, logger);
			logger.LogInformation("Writing cache file");
			TryProcessFile(delegate
			{
				if (setChmod600)
				{
					logger.LogInformation("Writing file with chmod 600");
					FileWithPermissions.WriteToNewFileWithOwnerRWPermissions(filePath, data);
				}
				else
				{
					logger.LogInformation("Writing file without special permissions");
					File.WriteAllBytes(filePath, data);
				}
			}, logger);
		}

		private static void EnsureParentDirectoryExists(string filePath, TraceSourceLogger logger)
		{
			if (!Directory.Exists(Path.GetDirectoryName(filePath)))
			{
				string directoryName = Path.GetDirectoryName(filePath);
				logger.LogInformation("Creating directory '" + directoryName + "'");
				Directory.CreateDirectory(directoryName);
			}
		}

		internal static void TouchFile(string filePath, TraceSourceLogger logger)
		{
			EnsureParentDirectoryExists(filePath, logger);
			logger.LogInformation("Touching file...");
			TryProcessFile(delegate
			{
				if (!File.Exists(filePath))
				{
					logger.LogInformation("File " + filePath + " does not exist. Creating it..");
					File.Create(filePath).Dispose();
				}
				File.SetLastWriteTimeUtc(filePath, DateTime.UtcNow);
			}, logger);
		}

		internal static void TryProcessFile(Action action, TraceSourceLogger logger)
		{
			for (int i = 0; i <= 20; i++)
			{
				try
				{
					action();
					break;
				}
				catch (Exception arg)
				{
					Thread.Sleep(TimeSpan.FromMilliseconds(200.0));
					if (i == 20)
					{
						logger.LogError($"An exception was encountered while processing the cache file ex:'{arg}'");
					}
					else
					{
						logger.LogWarning($"An exception was encountered while processing the cache file. Operation will be retried. Ex:'{arg}'");
					}
				}
			}
		}
	}
	public class MsalCacheHelper
	{
		public const string LinuxKeyRingDefaultCollection = "default";

		public const string LinuxKeyRingSessionCollection = "session";

		private static readonly Lazy<TraceSourceLogger> s_staticLogger = new Lazy<TraceSourceLogger>(() => new TraceSourceLogger(EnvUtils.GetNewTraceSource("MsalCacheHelperSingleton")));

		private readonly object _lockObject = new object();

		private readonly StorageCreationProperties _storageCreationProperties;

		private readonly TraceSourceLogger _logger;

		private HashSet<string> _knownAccountIds;

		private readonly FileSystemWatcher _cacheWatcher;

		private EventHandler<CacheChangedEventArgs> _cacheChangedEventHandler;

		internal CrossPlatLock CacheLock { get; private set; }

		internal Storage CacheStore { get; }

		public static string UserRootDirectory => SharedUtilities.GetUserRootDirectory();

		public event EventHandler<CacheChangedEventArgs> CacheChanged
		{
			add
			{
				if (!_storageCreationProperties.IsCacheEventConfigured)
				{
					throw new InvalidOperationException("To use this event, please configure the clientId and the authority using  StorageCreationPropertiesBuilder.WithCacheChangedEvent");
				}
				_cacheChangedEventHandler = (EventHandler<CacheChangedEventArgs>)Delegate.Combine(_cacheChangedEventHandler, value);
			}
			remove
			{
				_cacheChangedEventHandler = (EventHandler<CacheChangedEventArgs>)Delegate.Remove(_cacheChangedEventHandler, value);
			}
		}

		private static async Task<HashSet<string>> GetAccountIdentifiersNoLockAsync(StorageCreationProperties storageCreationProperties, TraceSourceLogger logger)
		{
			HashSet<string> accountIdentifiers = new HashSet<string>();
			if (storageCreationProperties.IsCacheEventConfigured && File.Exists(storageCreationProperties.CacheFilePath))
			{
				IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder.Create(storageCreationProperties.ClientId).WithAuthority(storageCreationProperties.Authority).Build();
				publicClientApplication.UserTokenCache.SetBeforeAccess(delegate(TokenCacheNotificationArgs args)
				{
					Storage storage = null;
					try
					{
						storage = Storage.Create(storageCreationProperties, s_staticLogger.Value.Source);
						byte[] array = null;
						try
						{
							array = storage.ReadData();
						}
						catch
						{
						}
						if (array != null)
						{
							args.TokenCache.DeserializeMsalV3(array);
						}
					}
					catch (Exception ex)
					{
						logger.LogError("An error occured while reading the token cache: " + ex);
						logger.LogError("Deleting the token cache as it might be corrupt.");
						storage.Clear(ignoreExceptions: true);
					}
				});
				foreach (IAccount item in await publicClientApplication.GetAccountsAsync().ConfigureAwait(continueOnCapturedContext: false))
				{
					accountIdentifiers.Add(item.HomeAccountId.Identifier);
				}
			}
			return accountIdentifiers;
		}

		private MsalCacheHelper(StorageCreationProperties storageCreationProperties, TraceSource logger, HashSet<string> knownAccountIds, FileSystemWatcher cacheWatcher)
		{
			_logger = ((logger == null) ? s_staticLogger.Value : new TraceSourceLogger(logger));
			_storageCreationProperties = storageCreationProperties;
			CacheStore = Storage.Create(_storageCreationProperties, _logger.Source);
			_knownAccountIds = knownAccountIds;
			_cacheWatcher = cacheWatcher;
			if (_cacheWatcher != null)
			{
				_cacheWatcher.Changed += OnCacheFileChangedAsync;
				_cacheWatcher.Deleted += OnCacheFileChangedAsync;
			}
		}

		private async void OnCacheFileChangedAsync(object sender, FileSystemEventArgs args)
		{
			if (_cacheChangedEventHandler?.GetInvocationList() == null)
			{
				return;
			}
			EventHandler<CacheChangedEventArgs> cacheChangedEventHandler = _cacheChangedEventHandler;
			if (cacheChangedEventHandler == null || cacheChangedEventHandler.GetInvocationList().Length == 0)
			{
				return;
			}
			try
			{
				IEnumerable<string> enumerable;
				IEnumerable<string> enumerable2;
				using (CreateCrossPlatLock(_storageCreationProperties))
				{
					HashSet<string> hashSet = await GetAccountIdentifiersNoLockAsync(_storageCreationProperties, _logger).ConfigureAwait(continueOnCapturedContext: false);
					IEnumerable<string> second = hashSet.Intersect(_knownAccountIds);
					enumerable = _knownAccountIds.Except(second);
					enumerable2 = hashSet.Except(second);
					_knownAccountIds = hashSet;
				}
				if (enumerable2.Any() || enumerable.Any())
				{
					_cacheChangedEventHandler(sender, new CacheChangedEventArgs(enumerable2, enumerable));
				}
			}
			catch (Exception arg)
			{
				_logger.LogWarning($"Exception within File Watcher : {arg}");
			}
		}

		internal MsalCacheHelper(ITokenCache userTokenCache, Storage store, TraceSource logger = null)
		{
			_logger = ((logger == null) ? s_staticLogger.Value : new TraceSourceLogger(logger));
			CacheStore = store;
			_storageCreationProperties = store.StorageCreationProperties;
			RegisterCache(userTokenCache);
		}

		public static async Task<MsalCacheHelper> CreateAsync(StorageCreationProperties storageCreationProperties, TraceSource logger = null)
		{
			if (storageCreationProperties == null)
			{
				throw new ArgumentNullException("storageCreationProperties");
			}
			using (CreateCrossPlatLock(storageCreationProperties))
			{
				TraceSourceLogger logger2 = ((logger == null) ? s_staticLogger.Value : new TraceSourceLogger(logger));
				HashSet<string> knownAccountIds = null;
				FileSystemWatcher fileSystemWatcher = null;
				if (storageCreationProperties.IsCacheEventConfigured)
				{
					knownAccountIds = await GetAccountIdentifiersNoLockAsync(storageCreationProperties, logger2).ConfigureAwait(continueOnCapturedContext: false);
					fileSystemWatcher = new FileSystemWatcher(storageCreationProperties.CacheDirectory, storageCreationProperties.CacheFileName);
				}
				MsalCacheHelper msalCacheHelper = new MsalCacheHelper(storageCreationProperties, logger, knownAccountIds, fileSystemWatcher);
				try
				{
					if (!SharedUtilities.IsMonoPlatform() && storageCreationProperties.IsCacheEventConfigured)
					{
						fileSystemWatcher.EnableRaisingEvents = true;
					}
				}
				catch (PlatformNotSupportedException)
				{
					msalCacheHelper._logger.LogError("Cannot fire the CacheChanged event because the target framework does not support FileSystemWatcher. This is a known issue with Mono.");
				}
				return msalCacheHelper;
			}
		}

		public void RegisterCache(ITokenCache tokenCache)
		{
			if (tokenCache == null)
			{
				throw new ArgumentNullException("tokenCache");
			}
			_logger.LogInformation("Registering token cache with on disk storage");
			tokenCache.SetBeforeAccess(BeforeAccessNotification);
			tokenCache.SetAfterAccess(AfterAccessNotification);
			_logger.LogInformation("Done initializing");
		}

		public void UnregisterCache(ITokenCache tokenCache)
		{
			if (tokenCache == null)
			{
				throw new ArgumentNullException("tokenCache");
			}
			tokenCache.SetBeforeAccess(null);
			tokenCache.SetAfterAccess(null);
		}

		[Obsolete("Applications should not delete the entire cache to log out all users. Instead, call app.RemoveAsync(IAccount) for each account in the cache. ", false)]
		public void Clear()
		{
			using (CreateCrossPlatLock(_storageCreationProperties))
			{
				CacheStore.Clear(ignoreExceptions: true);
			}
		}

		public byte[] LoadUnencryptedTokenCache()
		{
			using (CreateCrossPlatLock(_storageCreationProperties))
			{
				return CacheStore.ReadData();
			}
		}

		public void SaveUnencryptedTokenCache(byte[] tokenCache)
		{
			using (CreateCrossPlatLock(_storageCreationProperties))
			{
				CacheStore.WriteData(tokenCache);
			}
		}

		private static CrossPlatLock CreateCrossPlatLock(StorageCreationProperties storageCreationProperties)
		{
			return new CrossPlatLock(storageCreationProperties.CacheFilePath + ".lockfile", storageCreationProperties.LockRetryDelay, storageCreationProperties.LockRetryCount);
		}

		internal void BeforeAccessNotification(TokenCacheNotificationArgs args)
		{
			_logger.LogInformation("Before access");
			_logger.LogInformation("Acquiring lock for token cache");
			CacheLock = CreateCrossPlatLock(_storageCreationProperties);
			_logger.LogInformation("Before access, the store has changed");
			byte[] array;
			try
			{
				array = CacheStore.ReadData();
			}
			catch (Exception)
			{
				_logger.LogError("Could not read the token cache. Ignoring. See previous error message.");
				return;
			}
			_logger.LogInformation($"Read '{array?.Length}' bytes from storage");
			lock (_lockObject)
			{
				try
				{
					_logger.LogInformation("Deserializing the store");
					args.TokenCache.DeserializeMsalV3(array, shouldClearExistingCache: true);
				}
				catch (Exception arg)
				{
					_logger.LogError(string.Format("An exception was encountered while deserializing the {0} : {1}", "MsalCacheHelper", arg));
					_logger.LogError("No data found in the store, clearing the cache in memory.");
					CacheStore.Clear(ignoreExceptions: true);
					throw;
				}
			}
		}

		internal void AfterAccessNotification(TokenCacheNotificationArgs args)
		{
			try
			{
				_logger.LogInformation("After access");
				if (!args.HasStateChanged)
				{
					return;
				}
				_logger.LogInformation("After access, cache in memory HasChanged");
				byte[] array;
				try
				{
					array = args.TokenCache.SerializeMsalV3();
				}
				catch (Exception arg)
				{
					_logger.LogError(string.Format("An exception was encountered while serializing the {0} : {1}", "MsalCacheHelper", arg));
					_logger.LogError("No data found in the store, clearing the cache in memory.");
					CacheStore.Clear(ignoreExceptions: true);
					throw;
				}
				if (array != null)
				{
					_logger.LogInformation($"Serializing '{array.Length}' bytes");
					try
					{
						CacheStore.WriteData(array);
						return;
					}
					catch (Exception)
					{
						_logger.LogError("Could not write the token cache. Ignoring. See previous error message.");
						return;
					}
				}
			}
			finally
			{
				ReleaseFileLock();
			}
		}

		private void ReleaseFileLock()
		{
			CrossPlatLock cacheLock = CacheLock;
			CacheLock = null;
			cacheLock?.Dispose();
			_logger.LogInformation("Released lock");
		}

		public void VerifyPersistence()
		{
			CacheStore.VerifyPersistence();
		}
	}
	public class MsalCachePersistenceException : Exception
	{
		public MsalCachePersistenceException()
		{
		}

		public MsalCachePersistenceException(string message)
			: base(message)
		{
		}

		public MsalCachePersistenceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected MsalCachePersistenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	public sealed class CrossPlatLock : IDisposable
	{
		internal const int LockfileRetryDelayDefault = 100;

		internal const int LockfileRetryCountDefault = 600;

		private FileStream _lockFileStream;

		public CrossPlatLock(string lockfilePath, int lockFileRetryDelay = 100, int lockFileRetryCount = 600)
		{
			Exception innerException = null;
			FileStream fileStream = null;
			Directory.CreateDirectory(Path.GetDirectoryName(lockfilePath));
			string value = $"{SharedUtilities.GetCurrentProcessId()} {SharedUtilities.GetCurrentProcessName()}";
			for (int i = 0; i < lockFileRetryCount; i++)
			{
				try
				{
					FileShare share = FileShare.None;
					if (SharedUtilities.IsWindowsPlatform())
					{
						share = FileShare.Read;
					}
					FileOptions options = FileOptions.DeleteOnClose;
					if (SharedUtilities.IsMonoPlatform())
					{
						options = FileOptions.None;
					}
					fileStream = new FileStream(lockfilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, share, 4096, options);
					if (SharedUtilities.IsMonoPlatform())
					{
						fileStream.Lock(0L, 0L);
					}
					using StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8, 4096, leaveOpen: true);
					streamWriter.WriteLine(value);
				}
				catch (IOException ex)
				{
					fileStream?.Dispose();
					fileStream = null;
					innerException = ex;
					Thread.Sleep(lockFileRetryDelay);
					continue;
				}
				catch (UnauthorizedAccessException ex2)
				{
					fileStream?.Dispose();
					fileStream = null;
					innerException = ex2;
					Thread.Sleep(lockFileRetryDelay);
					continue;
				}
				break;
			}
			_lockFileStream = fileStream ?? throw new InvalidOperationException("Could not get access to the shared lock file.", innerException);
		}

		public void Dispose()
		{
			_lockFileStream?.Dispose();
			_lockFileStream = null;
		}
	}
	internal static class EnvUtils
	{
		internal const string TraceLevelEnvVarName = "IDENTITYEXTENSIONTRACELEVEL";

		private const string DefaultTraceSource = "Microsoft.Identity.Client.Extensions.TraceSource";

		internal static TraceSource GetNewTraceSource(string sourceName)
		{
			if (sourceName == null)
			{
				sourceName = "Microsoft.Identity.Client.Extensions.TraceSource";
			}
			SourceLevels defaultLevel = SourceLevels.Warning;
			string environmentVariable = Environment.GetEnvironmentVariable("IDENTITYEXTENSIONTRACELEVEL");
			if (!string.IsNullOrEmpty(environmentVariable) && Enum.TryParse<SourceLevels>(environmentVariable, ignoreCase: true, out var result))
			{
				defaultLevel = result;
			}
			return new TraceSource(sourceName, defaultLevel);
		}
	}
	internal struct GError
	{
		public uint Domain;

		public int Code;

		public string Message;
	}
	internal static class Libsecret
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

		[DllImport("libsecret-1.so.0", CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr secret_schema_new(string name, int flags, string attribute1, int attribute1Type, string attribute2, int attribute2Type, IntPtr end);

		[DllImport("libsecret-1.so.0", CallingConvention = CallingConvention.StdCall)]
		public static extern int secret_password_store_sync(IntPtr schema, string collection, string label, string password, IntPtr cancellable, out IntPtr error, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value, IntPtr end);

		[DllImport("libsecret-1.so.0", CallingConvention = CallingConvention.StdCall)]
		public static extern string secret_password_lookup_sync(IntPtr schema, IntPtr cancellable, out IntPtr error, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value, IntPtr end);

		[DllImport("libsecret-1.so.0", CallingConvention = CallingConvention.StdCall)]
		public static extern int secret_password_clear_sync(IntPtr schema, IntPtr cancellable, out IntPtr error, string attribute1Type, string attribute1Value, string attribute2Type, string attribute2Value, IntPtr end);
	}
	internal static class LinuxNativeMethods
	{
		public const int RootUserId = 0;

		[DllImport("libc")]
		public static extern int getuid();
	}
	internal class MacOSKeychain
	{
		private readonly string _namespace;

		public MacOSKeychain(string @namespace = null)
		{
			_namespace = @namespace;
		}

		public MacOSKeychainCredential Get(string service, string account)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr result = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			try
			{
				intPtr = CoreFoundation.CFDictionaryCreateMutable(IntPtr.Zero, 0L, IntPtr.Zero, IntPtr.Zero);
				CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecClass, SecurityFramework.kSecClassGenericPassword);
				CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecMatchLimit, SecurityFramework.kSecMatchLimitOne);
				CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecReturnData, CoreFoundation.kCFBooleanTrue);
				CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecReturnAttributes, CoreFoundation.kCFBooleanTrue);
				if (!string.IsNullOrWhiteSpace(service))
				{
					intPtr2 = CreateCFStringUtf8(CreateServiceName(service));
					CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecAttrService, intPtr2);
				}
				if (!string.IsNullOrWhiteSpace(account))
				{
					intPtr3 = CreateCFStringUtf8(account);
					CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecAttrAccount, intPtr3);
				}
				int num = SecurityFramework.SecItemCopyMatching(intPtr, out result);
				switch (num)
				{
				case 0:
				{
					int num2 = CoreFoundation.CFGetTypeID(result);
					if (num2 == CoreFoundation.CFDictionaryGetTypeID())
					{
						return CreateCredentialFromAttributes(result);
					}
					throw new InteropException($"Unknown keychain search result type CFTypeID: {num2}.", -1);
				}
				case -25300:
					return null;
				default:
					SecurityFramework.ThrowIfError(num);
					return null;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(intPtr2);
				}
				if (intPtr3 != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(intPtr3);
				}
				if (result != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(result);
				}
			}
		}

		public void AddOrUpdate(string service, string account, byte[] secretBytes)
		{
			IntPtr passwordData = IntPtr.Zero;
			IntPtr itemRef = IntPtr.Zero;
			string text = CreateServiceName(service);
			uint length = (uint)text.Length;
			uint accountNameLength = (uint)(account?.Length ?? 0);
			try
			{
				uint passwordLength;
				int num = SecurityFramework.SecKeychainFindGenericPassword(IntPtr.Zero, length, text, accountNameLength, account, out passwordLength, out passwordData, out itemRef);
				switch (num)
				{
				case 0:
					SecurityFramework.ThrowIfError(SecurityFramework.SecKeychainItemModifyAttributesAndData(itemRef, IntPtr.Zero, (uint)secretBytes.Length, secretBytes), "Could not update existing item");
					break;
				case -25300:
					SecurityFramework.ThrowIfError(SecurityFramework.SecKeychainAddGenericPassword(IntPtr.Zero, length, text, accountNameLength, account, (uint)secretBytes.Length, secretBytes, out itemRef), "Could not create new item");
					break;
				default:
					SecurityFramework.ThrowIfError(num);
					break;
				}
			}
			finally
			{
				if (passwordData != IntPtr.Zero)
				{
					SecurityFramework.SecKeychainItemFreeContent(IntPtr.Zero, passwordData);
				}
				if (itemRef != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(itemRef);
				}
			}
		}

		public bool Remove(string service, string account)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr result = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			try
			{
				intPtr = CoreFoundation.CFDictionaryCreateMutable(IntPtr.Zero, 0L, IntPtr.Zero, IntPtr.Zero);
				CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecClass, SecurityFramework.kSecClassGenericPassword);
				CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecMatchLimit, SecurityFramework.kSecMatchLimitOne);
				CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecReturnRef, CoreFoundation.kCFBooleanTrue);
				if (!string.IsNullOrWhiteSpace(service))
				{
					intPtr2 = CreateCFStringUtf8(CreateServiceName(service));
					CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecAttrService, intPtr2);
				}
				if (!string.IsNullOrWhiteSpace(account))
				{
					intPtr3 = CreateCFStringUtf8(account);
					CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecAttrAccount, intPtr3);
				}
				int num = SecurityFramework.SecItemCopyMatching(intPtr, out result);
				switch (num)
				{
				case 0:
					SecurityFramework.ThrowIfError(SecurityFramework.SecKeychainItemDelete(result));
					return true;
				case -25300:
					return false;
				default:
					SecurityFramework.ThrowIfError(num);
					return false;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(intPtr);
				}
				if (result != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(result);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(intPtr2);
				}
				if (intPtr3 != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(intPtr3);
				}
			}
		}

		private static IntPtr CreateCFStringUtf8(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			return CoreFoundation.CFStringCreateWithBytes(IntPtr.Zero, bytes, bytes.Length, CFStringEncoding.kCFStringEncodingUTF8, isExternalRepresentation: false);
		}

		private static MacOSKeychainCredential CreateCredentialFromAttributes(IntPtr attributes)
		{
			string stringAttribute = GetStringAttribute(attributes, SecurityFramework.kSecAttrService);
			string stringAttribute2 = GetStringAttribute(attributes, SecurityFramework.kSecAttrAccount);
			byte[] byteArrayAtrribute = GetByteArrayAtrribute(attributes, SecurityFramework.kSecValueData);
			string stringAttribute3 = GetStringAttribute(attributes, SecurityFramework.kSecAttrLabel);
			return new MacOSKeychainCredential(stringAttribute, stringAttribute2, byteArrayAtrribute, stringAttribute3);
		}

		private static byte[] GetByteArrayAtrribute(IntPtr dict, IntPtr key)
		{
			if (dict == IntPtr.Zero)
			{
				return null;
			}
			if (CoreFoundation.CFDictionaryGetValueIfPresent(dict, key, out var value) && value != IntPtr.Zero && CoreFoundation.CFGetTypeID(value) == CoreFoundation.CFDataGetTypeID())
			{
				int num = CoreFoundation.CFDataGetLength(value);
				if (num > 0)
				{
					IntPtr source = CoreFoundation.CFDataGetBytePtr(value);
					byte[] array = new byte[num];
					Marshal.Copy(source, array, 0, num);
					return array;
				}
			}
			return null;
		}

		private static string GetStringAttribute(IntPtr dict, IntPtr key)
		{
			if (dict == IntPtr.Zero)
			{
				return null;
			}
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				if (CoreFoundation.CFDictionaryGetValueIfPresent(dict, key, out var value) && value != IntPtr.Zero)
				{
					if (CoreFoundation.CFGetTypeID(value) == CoreFoundation.CFStringGetTypeID())
					{
						int num = (int)CoreFoundation.CFStringGetLength(value);
						int num2 = num + 1;
						intPtr = Marshal.AllocHGlobal(num2);
						if (CoreFoundation.CFStringGetCString(value, intPtr, num2, CFStringEncoding.kCFStringEncodingUTF8))
						{
							return Marshal.PtrToStringAuto(intPtr, num);
						}
					}
					if (CoreFoundation.CFGetTypeID(value) == CoreFoundation.CFDataGetTypeID())
					{
						int num3 = CoreFoundation.CFDataGetLength(value);
						if (num3 > 0)
						{
							return Marshal.PtrToStringAuto(CoreFoundation.CFDataGetBytePtr(value), num3);
						}
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return null;
		}

		private string CreateServiceName(string service)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrWhiteSpace(_namespace))
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}:", _namespace);
			}
			stringBuilder.Append(service);
			return stringBuilder.ToString();
		}
	}
	public static class SharedUtilities
	{
		private static readonly string s_homeEnvVar = Environment.GetEnvironmentVariable("HOME");

		private static readonly string s_lognameEnvVar = Environment.GetEnvironmentVariable("LOGNAME");

		private static readonly string s_userEnvVar = Environment.GetEnvironmentVariable("USER");

		private static readonly string s_lNameEnvVar = Environment.GetEnvironmentVariable("LNAME");

		private static readonly string s_usernameEnvVar = Environment.GetEnvironmentVariable("USERNAME");

		private static readonly Lazy<bool> s_isMono = new Lazy<bool>(() => Type.GetType("Mono.Runtime") != null);

		private static string s_processName = null;

		private static int s_processId = 0;

		public static bool IsWindowsPlatform()
		{
			return Environment.OSVersion.Platform == PlatformID.Win32NT;
		}

		public static bool IsMacPlatform()
		{
			return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
		}

		public static bool IsLinuxPlatform()
		{
			return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
		}

		internal static bool IsMonoPlatform()
		{
			return s_isMono.Value;
		}

		internal static int GetCurrentProcessId()
		{
			if (s_processId == 0)
			{
				using Process process = Process.GetCurrentProcess();
				s_processId = process.Id;
				s_processName = process.ProcessName;
			}
			return s_processId;
		}

		internal static string GetCurrentProcessName()
		{
			if (string.IsNullOrEmpty(s_processName))
			{
				using Process process = Process.GetCurrentProcess();
				s_processName = process.ProcessName;
				s_processId = process.Id;
			}
			return s_processName;
		}

		public static string GetUserRootDirectory()
		{
			if (IsWindowsPlatform())
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			}
			return GetUserHomeDirOnUnix();
		}

		private static string GetUserHomeDirOnUnix()
		{
			if (IsWindowsPlatform())
			{
				throw new NotSupportedException();
			}
			if (!string.IsNullOrEmpty(s_homeEnvVar))
			{
				return s_homeEnvVar;
			}
			string text = null;
			if (!string.IsNullOrEmpty(s_lognameEnvVar))
			{
				text = s_lognameEnvVar;
			}
			else if (!string.IsNullOrEmpty(s_userEnvVar))
			{
				text = s_userEnvVar;
			}
			else if (!string.IsNullOrEmpty(s_lNameEnvVar))
			{
				text = s_lNameEnvVar;
			}
			else if (!string.IsNullOrEmpty(s_usernameEnvVar))
			{
				text = s_usernameEnvVar;
			}
			if (IsMacPlatform())
			{
				if (string.IsNullOrEmpty(text))
				{
					return null;
				}
				return Path.Combine("/Users", text);
			}
			if (IsLinuxPlatform())
			{
				if (LinuxNativeMethods.getuid() == 0)
				{
					return "/root";
				}
				if (string.IsNullOrEmpty(text))
				{
					return null;
				}
				return Path.Combine("/home", text);
			}
			throw new NotSupportedException();
		}
	}
	public class StorageCreationProperties
	{
		public readonly string CacheFileName;

		public readonly string CacheDirectory;

		public readonly string MacKeyChainServiceName;

		public readonly string MacKeyChainAccountName;

		public readonly string KeyringSchemaName;

		public readonly string KeyringCollection;

		public readonly string KeyringSecretLabel;

		public readonly KeyValuePair<string, string> KeyringAttribute1;

		public readonly KeyValuePair<string, string> KeyringAttribute2;

		public readonly int LockRetryDelay;

		public readonly bool UseLinuxUnencryptedFallback;

		public readonly bool UseUnencryptedFallback;

		public readonly int LockRetryCount;

		public string CacheFilePath { get; }

		public string ClientId { get; }

		public string Authority { get; }

		internal bool IsCacheEventConfigured
		{
			get
			{
				if (!string.IsNullOrEmpty(ClientId))
				{
					return !string.IsNullOrEmpty(Authority);
				}
				return false;
			}
		}

		internal StorageCreationProperties(string cacheFileName, string cacheDirectory, string macKeyChainServiceName, string macKeyChainAccountName, bool useLinuxPlaintextFallback, bool usePlaintextFallback, string keyringSchemaName, string keyringCollection, string keyringSecretLabel, KeyValuePair<string, string> keyringAttribute1, KeyValuePair<string, string> keyringAttribute2, int lockRetryDelay, int lockRetryCount, string clientId, string authority)
		{
			CacheFileName = cacheFileName;
			CacheDirectory = cacheDirectory;
			CacheFilePath = Path.Combine(CacheDirectory, CacheFileName);
			UseLinuxUnencryptedFallback = useLinuxPlaintextFallback;
			UseUnencryptedFallback = usePlaintextFallback;
			MacKeyChainServiceName = macKeyChainServiceName;
			MacKeyChainAccountName = macKeyChainAccountName;
			KeyringSchemaName = keyringSchemaName;
			KeyringCollection = keyringCollection;
			KeyringSecretLabel = keyringSecretLabel;
			KeyringAttribute1 = keyringAttribute1;
			KeyringAttribute2 = keyringAttribute2;
			ClientId = clientId;
			Authority = authority;
			LockRetryDelay = lockRetryDelay;
			LockRetryCount = lockRetryCount;
			Validate();
		}

		private void Validate()
		{
			if (UseLinuxUnencryptedFallback && UseUnencryptedFallback)
			{
				throw new ArgumentException("UseLinuxUnencryptedFallback and UseUnencryptedFallback are mutually exclusive. UseLinuxUnencryptedFallback is the safer option. ");
			}
			if ((UseLinuxUnencryptedFallback || UseUnencryptedFallback) && (!string.IsNullOrEmpty(KeyringSecretLabel) || !string.IsNullOrEmpty(KeyringSchemaName) || !string.IsNullOrEmpty(KeyringCollection)))
			{
				throw new ArgumentException("Using plaintext storage is mutually exclusive with other Linux storage options. ");
			}
			if (UseUnencryptedFallback && (!string.IsNullOrEmpty(MacKeyChainServiceName) || !string.IsNullOrEmpty(MacKeyChainAccountName)))
			{
				throw new ArgumentException("Using plaintext storage is mutually exclusive with other Mac storage options. ");
			}
		}
	}
	public class StorageCreationPropertiesBuilder
	{
		private readonly string _cacheFileName;

		private readonly string _cacheDirectory;

		private string _clientId;

		private string _authority;

		private string _macKeyChainServiceName;

		private string _macKeyChainAccountName;

		private string _keyringSchemaName;

		private string _keyringCollection;

		private string _keyringSecretLabel;

		private KeyValuePair<string, string> _keyringAttribute1;

		private KeyValuePair<string, string> _keyringAttribute2;

		private int _lockRetryDelay = 100;

		private int _lockRetryCount = 600;

		private bool _useLinuxPlaintextFallback;

		private bool _usePlaintextFallback;

		[Obsolete("Use StorageCreationPropertiesBuilder(string, string) instead. If you need to consume the CacheChanged event then also use WithCacheChangedEvent(string, string)", false)]
		public StorageCreationPropertiesBuilder(string cacheFileName, string cacheDirectory, string clientId)
		{
			_cacheFileName = cacheFileName;
			_cacheDirectory = cacheDirectory;
			_clientId = clientId;
			_authority = "https://login.microsoftonline.com/common";
		}

		public StorageCreationPropertiesBuilder(string cacheFileName, string cacheDirectory)
		{
			_cacheFileName = cacheFileName;
			_cacheDirectory = cacheDirectory;
		}

		public StorageCreationProperties Build()
		{
			return new StorageCreationProperties(_cacheFileName, _cacheDirectory, _macKeyChainServiceName, _macKeyChainAccountName, _useLinuxPlaintextFallback, _usePlaintextFallback, _keyringSchemaName, _keyringCollection, _keyringSecretLabel, _keyringAttribute1, _keyringAttribute2, _lockRetryDelay, _lockRetryCount, _clientId, _authority);
		}

		public StorageCreationPropertiesBuilder WithMacKeyChain(string serviceName, string accountName)
		{
			_macKeyChainServiceName = serviceName;
			_macKeyChainAccountName = accountName;
			return this;
		}

		public StorageCreationPropertiesBuilder WithCacheChangedEvent(string clientId, string authority = "https://login.microsoftonline.com/common")
		{
			_clientId = clientId;
			_authority = authority;
			return this;
		}

		public StorageCreationPropertiesBuilder CustomizeLockRetry(int lockRetryDelay, int lockRetryCount)
		{
			if (lockRetryDelay < 1)
			{
				throw new ArgumentOutOfRangeException("lockRetryDelay");
			}
			if (lockRetryCount < 1)
			{
				throw new ArgumentOutOfRangeException("lockRetryCount");
			}
			_lockRetryCount = lockRetryCount;
			_lockRetryDelay = lockRetryDelay;
			return this;
		}

		public StorageCreationPropertiesBuilder WithLinuxKeyring(string schemaName, string collection, string secretLabel, KeyValuePair<string, string> attribute1, KeyValuePair<string, string> attribute2)
		{
			if (string.IsNullOrEmpty(schemaName))
			{
				throw new ArgumentNullException("schemaName");
			}
			_keyringSchemaName = schemaName;
			_keyringCollection = collection;
			_keyringSecretLabel = secretLabel;
			_keyringAttribute1 = attribute1;
			_keyringAttribute2 = attribute2;
			return this;
		}

		public StorageCreationPropertiesBuilder WithLinuxUnprotectedFile()
		{
			_useLinuxPlaintextFallback = true;
			return this;
		}

		public StorageCreationPropertiesBuilder WithUnprotectedFile()
		{
			_usePlaintextFallback = true;
			return this;
		}
	}
	public class TraceSourceLogger
	{
		public TraceSource Source { get; }

		public TraceSourceLogger(TraceSource traceSource)
		{
			Source = traceSource;
		}

		public void LogInformation(string message)
		{
			Source.TraceEvent(TraceEventType.Information, 0, FormatLogMessage(message));
		}

		public void LogError(string message)
		{
			Source.TraceEvent(TraceEventType.Error, 0, FormatLogMessage(message));
		}

		public void LogWarning(string message)
		{
			Source.TraceEvent(TraceEventType.Warning, 0, FormatLogMessage(message));
		}

		private static string FormatLogMessage(string message)
		{
			return "[MSAL.Extension][" + DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture) + "] " + message;
		}
	}
	public class Storage
	{
		private readonly TraceSourceLogger _logger;

		internal const string PersistenceValidationDummyData = "msal_persistence_test";

		private static readonly Lazy<TraceSourceLogger> s_staticLogger = new Lazy<TraceSourceLogger>(() => new TraceSourceLogger(EnvUtils.GetNewTraceSource("MsalCacheHelperSingleton")));

		internal ICacheAccessor CacheAccessor { get; }

		internal StorageCreationProperties StorageCreationProperties { get; }

		public static Storage Create(StorageCreationProperties creationProperties, TraceSource logger = null)
		{
			TraceSourceLogger logger2 = ((logger == null) ? s_staticLogger.Value : new TraceSourceLogger(logger));
			ICacheAccessor cacheAccessor;
			if (creationProperties.UseUnencryptedFallback)
			{
				cacheAccessor = new FileAccessor(creationProperties.CacheFilePath, setOwnerOnlyPermissions: true, logger2);
			}
			else if (SharedUtilities.IsWindowsPlatform())
			{
				cacheAccessor = new DpApiEncryptedFileAccessor(creationProperties.CacheFilePath, logger2);
			}
			else if (SharedUtilities.IsMacPlatform())
			{
				cacheAccessor = new MacKeychainAccessor(creationProperties.CacheFilePath, creationProperties.MacKeyChainServiceName, creationProperties.MacKeyChainAccountName, logger2);
			}
			else
			{
				if (!SharedUtilities.IsLinuxPlatform())
				{
					throw new PlatformNotSupportedException();
				}
				if (creationProperties.UseLinuxUnencryptedFallback)
				{
					cacheAccessor = new FileAccessor(creationProperties.CacheFilePath, setOwnerOnlyPermissions: true, logger2);
				}
				else
				{
					string cacheFilePath = creationProperties.CacheFilePath;
					string keyringCollection = creationProperties.KeyringCollection;
					string keyringSchemaName = creationProperties.KeyringSchemaName;
					string keyringSecretLabel = creationProperties.KeyringSecretLabel;
					KeyValuePair<string, string> keyringAttribute = creationProperties.KeyringAttribute1;
					string key = keyringAttribute.Key;
					keyringAttribute = creationProperties.KeyringAttribute1;
					string value = keyringAttribute.Value;
					keyringAttribute = creationProperties.KeyringAttribute2;
					string key2 = keyringAttribute.Key;
					keyringAttribute = creationProperties.KeyringAttribute2;
					cacheAccessor = new LinuxKeyringAccessor(cacheFilePath, keyringCollection, keyringSchemaName, keyringSecretLabel, key, value, key2, keyringAttribute.Value, logger2);
				}
			}
			return new Storage(creationProperties, cacheAccessor, logger2);
		}

		internal Storage(StorageCreationProperties creationProperties, ICacheAccessor cacheAccessor, TraceSourceLogger logger)
		{
			StorageCreationProperties = creationProperties;
			_logger = logger;
			CacheAccessor = cacheAccessor;
			_logger.LogInformation("Initialized 'Storage'");
		}

		public byte[] ReadData()
		{
			try
			{
				_logger.LogInformation("Reading Data");
				byte[] array = CacheAccessor.Read();
				_logger.LogInformation($"Got '{((array != null) ? array.Length : 0)}' bytes from file storage");
				return array ?? Array.Empty<byte>();
			}
			catch (Exception arg)
			{
				_logger.LogError(string.Format("An exception was encountered while reading data from the {0} : {1}", "Storage", arg));
				throw;
			}
		}

		public void WriteData(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			try
			{
				_logger.LogInformation($"Got '{data?.Length}' bytes to write to storage");
				CacheAccessor.Write(data);
			}
			catch (Exception arg)
			{
				_logger.LogError(string.Format("An exception was encountered while writing data to {0} : {1}", "Storage", arg));
				throw;
			}
		}

		public void Clear(bool ignoreExceptions = false)
		{
			try
			{
				_logger.LogInformation("Clearing the cache file");
				CacheAccessor.Clear();
			}
			catch (Exception arg)
			{
				_logger.LogError(string.Format("An exception was encountered while clearing data from {0} : {1}", "Storage", arg));
				if (!ignoreExceptions)
				{
					throw;
				}
			}
		}

		public void VerifyPersistence()
		{
			ICacheAccessor cacheAccessor = CacheAccessor.CreateForPersistenceValidation();
			try
			{
				_logger.LogInformation("[Verify Persistence] Writing Data ");
				cacheAccessor.Write(Encoding.UTF8.GetBytes("msal_persistence_test"));
				_logger.LogInformation("[Verify Persistence] Reading Data ");
				byte[] array = cacheAccessor.Read();
				if (array == null || array.Length == 0)
				{
					throw new MsalCachePersistenceException("Persistence check failed. Data was written but it could not be read. Possible cause: on Linux, LibSecret is installed but D-Bus isn't running because it cannot be started over SSH.");
				}
				string text = Encoding.UTF8.GetString(array);
				if (!string.Equals("msal_persistence_test", text, StringComparison.Ordinal))
				{
					throw new MsalCachePersistenceException("Persistence check failed. Data written msal_persistence_test is different from data read " + text);
				}
			}
			catch (InteropException ex)
			{
				throw new MsalCachePersistenceException($"Persistence check failed. Reason: {ex.Message}. OS error code {ex.ErrorCode}.", ex);
			}
			catch (Exception ex2) when (!(ex2 is MsalCachePersistenceException))
			{
				throw new MsalCachePersistenceException("Persistence check failed. Inspect inner exception for details", ex2);
			}
			finally
			{
				try
				{
					_logger.LogInformation("[Verify Persistence] Clearing data");
					cacheAccessor.Clear();
				}
				catch (Exception ex3)
				{
					_logger.LogError("[Verify Persistence] Could not clear the test data: " + ex3);
				}
			}
		}
	}
}
namespace Microsoft.Identity.Client.Extensions.Msal.Accessors
{
	internal static class FileWithPermissions
	{
		[DllImport("libc", EntryPoint = "creat", SetLastError = true)]
		private static extern int PosixCreate([MarshalAs(UnmanagedType.LPStr)] string pathname, int mode);

		[DllImport("libc", EntryPoint = "chmod", SetLastError = true)]
		private static extern int PosixChmod([MarshalAs(UnmanagedType.LPStr)] string pathname, int mode);

		public static void WriteToNewFileWithOwnerRWPermissions(string path, byte[] data)
		{
			if (SharedUtilities.IsWindowsPlatform())
			{
				WriteToNewFileWithOwnerRWPermissionsWindows(path, data);
				return;
			}
			if (SharedUtilities.IsMacPlatform() || SharedUtilities.IsLinuxPlatform())
			{
				WriteToNewFileWithOwnerRWPermissionsUnix(path, data);
				return;
			}
			throw new PlatformNotSupportedException();
		}

		private static void WriteToNewFileWithOwnerRWPermissionsUnix(string path, byte[] data)
		{
			int mode = Convert.ToInt32("600", 8);
			int num = PosixCreate(path, mode);
			if (num == -1)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				using (File.Create(path))
				{
				}
				File.Delete(path);
				throw new InvalidOperationException($"libc creat() failed with last error code {lastWin32Error}, but File.Create did not");
			}
			using FileStream fileStream = new FileStream(new SafeFileHandle((IntPtr)num, ownsHandle: true), FileAccess.ReadWrite);
			fileStream.Write(data, 0, data.Length);
		}

		private static void WriteToNewFileWithOwnerRWPermissionsWindows(string filePath, byte[] data)
		{
			FileSecurity fileSecurity = new FileSecurity();
			FileSystemRights fileSystemRights = FileSystemRights.Write | FileSystemRights.Read;
			fileSecurity.AddAccessRule(new FileSystemAccessRule(WindowsIdentity.GetCurrent().Name, fileSystemRights, InheritanceFlags.None, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
			fileSecurity.SetAccessRuleProtection(isProtected: true, preserveInheritance: false);
			FileStream fileStream = null;
			try
			{
				fileStream = FileSystemAclExtensions.Create(new FileInfo(filePath), FileMode.Create, fileSystemRights, FileShare.Read, data.Length, FileOptions.None, fileSecurity);
				fileStream.Write(data, 0, data.Length);
			}
			finally
			{
				fileStream?.Dispose();
			}
		}
	}
}
