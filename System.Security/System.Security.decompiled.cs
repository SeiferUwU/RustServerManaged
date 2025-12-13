using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Asn1;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.Pkcs.Asn1;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Internal.Cryptography;
using Internal.Cryptography.Pal.AnyOS;
using Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: SatelliteContractVersion("4.0.0.0")]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: AssemblyDelaySign(true)]
[assembly: AssemblyDescription("System.Security.dll")]
[assembly: AssemblyTitle("System.Security.dll")]
[assembly: AssemblyFileVersion("4.6.57.0")]
[assembly: AssemblyInformationalVersion("4.6.57.0")]
[assembly: AssemblyCompany("Mono development team")]
[assembly: AssemblyProduct("Mono Common Language Infrastructure")]
[assembly: AssemblyCopyright("(c) Various Mono authors")]
[assembly: AssemblyDefaultAlias("System.Security.dll")]
[assembly: AllowPartiallyTrustedCallers]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("4.0.0.0")]
[module: UnverifiableCode]
internal static class Interop
{
	internal class Kernel32
	{
		private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		private const int FORMAT_MESSAGE_FROM_HMODULE = 2048;

		private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;

		private const int ERROR_INSUFFICIENT_BUFFER = 122;

		private const int InitialBufferSize = 256;

		private const int BufferSizeIncreaseFactor = 4;

		private const int MaxAllowedBufferSize = 66560;

		[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Unicode, EntryPoint = "FormatMessageW", SetLastError = true)]
		private static extern int FormatMessage(int dwFlags, IntPtr lpSource, uint dwMessageId, int dwLanguageId, [Out] StringBuilder lpBuffer, int nSize, IntPtr[] arguments);

		internal static string GetMessage(int errorCode)
		{
			return GetMessage(IntPtr.Zero, errorCode);
		}

		internal static string GetMessage(IntPtr moduleHandle, int errorCode)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			do
			{
				if (TryGetErrorMessage(moduleHandle, errorCode, stringBuilder, out var errorMsg))
				{
					return errorMsg;
				}
				stringBuilder.Capacity *= 4;
			}
			while (stringBuilder.Capacity < 66560);
			return $"Unknown error (0x{errorCode:x})";
		}

		private static bool TryGetErrorMessage(IntPtr moduleHandle, int errorCode, StringBuilder sb, out string errorMsg)
		{
			errorMsg = "";
			int num = 12800;
			if (moduleHandle != IntPtr.Zero)
			{
				num |= 0x800;
			}
			if (FormatMessage(num, moduleHandle, (uint)errorCode, 0, sb, sb.Capacity, null) != 0)
			{
				int num2;
				for (num2 = sb.Length; num2 > 0; num2--)
				{
					char c = sb[num2 - 1];
					if (c > ' ' && c != '.')
					{
						break;
					}
				}
				errorMsg = sb.ToString(0, num2);
			}
			else
			{
				if (Marshal.GetLastWin32Error() == 122)
				{
					return false;
				}
				errorMsg = $"Unknown error (0x{errorCode:x})";
			}
			return true;
		}
	}

	internal class Crypt32
	{
		[Flags]
		internal enum CryptProtectDataFlags
		{
			CRYPTPROTECT_UI_FORBIDDEN = 1,
			CRYPTPROTECT_LOCAL_MACHINE = 4,
			CRYPTPROTECT_CRED_SYNC = 8,
			CRYPTPROTECT_AUDIT = 0x10,
			CRYPTPROTECT_NO_RECOVERY = 0x20,
			CRYPTPROTECT_VERIFY_PROTECTION = 0x40
		}

		internal struct DATA_BLOB
		{
			internal uint cbData;

			internal IntPtr pbData;

			internal DATA_BLOB(IntPtr handle, uint size)
			{
				cbData = size;
				pbData = handle;
			}
		}

		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CryptProtectData([In] ref DATA_BLOB pDataIn, [In] string szDataDescr, [In] ref DATA_BLOB pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] CryptProtectDataFlags dwFlags, out DATA_BLOB pDataOut);

		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CryptUnprotectData([In] ref DATA_BLOB pDataIn, [In] IntPtr ppszDataDescr, [In] ref DATA_BLOB pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] CryptProtectDataFlags dwFlags, out DATA_BLOB pDataOut);
	}

	internal class Errors
	{
		internal const int ERROR_SUCCESS = 0;

		internal const int ERROR_INVALID_FUNCTION = 1;

		internal const int ERROR_FILE_NOT_FOUND = 2;

		internal const int ERROR_PATH_NOT_FOUND = 3;

		internal const int ERROR_ACCESS_DENIED = 5;

		internal const int ERROR_INVALID_HANDLE = 6;

		internal const int ERROR_NOT_ENOUGH_MEMORY = 8;

		internal const int ERROR_INVALID_DATA = 13;

		internal const int ERROR_INVALID_DRIVE = 15;

		internal const int ERROR_NO_MORE_FILES = 18;

		internal const int ERROR_NOT_READY = 21;

		internal const int ERROR_BAD_COMMAND = 22;

		internal const int ERROR_BAD_LENGTH = 24;

		internal const int ERROR_SHARING_VIOLATION = 32;

		internal const int ERROR_LOCK_VIOLATION = 33;

		internal const int ERROR_HANDLE_EOF = 38;

		internal const int ERROR_BAD_NETPATH = 53;

		internal const int ERROR_BAD_NET_NAME = 67;

		internal const int ERROR_FILE_EXISTS = 80;

		internal const int ERROR_INVALID_PARAMETER = 87;

		internal const int ERROR_BROKEN_PIPE = 109;

		internal const int ERROR_SEM_TIMEOUT = 121;

		internal const int ERROR_CALL_NOT_IMPLEMENTED = 120;

		internal const int ERROR_INSUFFICIENT_BUFFER = 122;

		internal const int ERROR_INVALID_NAME = 123;

		internal const int ERROR_NEGATIVE_SEEK = 131;

		internal const int ERROR_DIR_NOT_EMPTY = 145;

		internal const int ERROR_BAD_PATHNAME = 161;

		internal const int ERROR_LOCK_FAILED = 167;

		internal const int ERROR_BUSY = 170;

		internal const int ERROR_ALREADY_EXISTS = 183;

		internal const int ERROR_BAD_EXE_FORMAT = 193;

		internal const int ERROR_ENVVAR_NOT_FOUND = 203;

		internal const int ERROR_FILENAME_EXCED_RANGE = 206;

		internal const int ERROR_EXE_MACHINE_TYPE_MISMATCH = 216;

		internal const int ERROR_PIPE_BUSY = 231;

		internal const int ERROR_NO_DATA = 232;

		internal const int ERROR_PIPE_NOT_CONNECTED = 233;

		internal const int ERROR_MORE_DATA = 234;

		internal const int ERROR_NO_MORE_ITEMS = 259;

		internal const int ERROR_DIRECTORY = 267;

		internal const int ERROR_PARTIAL_COPY = 299;

		internal const int ERROR_ARITHMETIC_OVERFLOW = 534;

		internal const int ERROR_PIPE_CONNECTED = 535;

		internal const int ERROR_PIPE_LISTENING = 536;

		internal const int ERROR_OPERATION_ABORTED = 995;

		internal const int ERROR_IO_INCOMPLETE = 996;

		internal const int ERROR_IO_PENDING = 997;

		internal const int ERROR_NO_TOKEN = 1008;

		internal const int ERROR_DLL_INIT_FAILED = 1114;

		internal const int ERROR_COUNTER_TIMEOUT = 1121;

		internal const int ERROR_NO_ASSOCIATION = 1155;

		internal const int ERROR_DDE_FAIL = 1156;

		internal const int ERROR_DLL_NOT_FOUND = 1157;

		internal const int ERROR_NOT_FOUND = 1168;

		internal const int ERROR_NETWORK_UNREACHABLE = 1231;

		internal const int ERROR_NON_ACCOUNT_SID = 1257;

		internal const int ERROR_NOT_ALL_ASSIGNED = 1300;

		internal const int ERROR_UNKNOWN_REVISION = 1305;

		internal const int ERROR_INVALID_OWNER = 1307;

		internal const int ERROR_INVALID_PRIMARY_GROUP = 1308;

		internal const int ERROR_NO_SUCH_PRIVILEGE = 1313;

		internal const int ERROR_PRIVILEGE_NOT_HELD = 1314;

		internal const int ERROR_INVALID_ACL = 1336;

		internal const int ERROR_INVALID_SECURITY_DESCR = 1338;

		internal const int ERROR_INVALID_SID = 1337;

		internal const int ERROR_BAD_IMPERSONATION_LEVEL = 1346;

		internal const int ERROR_CANT_OPEN_ANONYMOUS = 1347;

		internal const int ERROR_NO_SECURITY_ON_OBJECT = 1350;

		internal const int ERROR_CLASS_ALREADY_EXISTS = 1410;

		internal const int ERROR_TRUSTED_RELATIONSHIP_FAILURE = 1789;

		internal const int ERROR_RESOURCE_LANG_NOT_FOUND = 1815;

		internal const int EFail = -2147467259;

		internal const int E_FILENOTFOUND = -2147024894;
	}

	internal static class Libraries
	{
		internal const string Advapi32 = "advapi32.dll";

		internal const string BCrypt = "BCrypt.dll";

		internal const string CoreComm_L1_1_1 = "api-ms-win-core-comm-l1-1-1.dll";

		internal const string Crypt32 = "crypt32.dll";

		internal const string Error_L1 = "api-ms-win-core-winrt-error-l1-1-0.dll";

		internal const string HttpApi = "httpapi.dll";

		internal const string IpHlpApi = "iphlpapi.dll";

		internal const string Kernel32 = "kernel32.dll";

		internal const string Memory_L1_3 = "api-ms-win-core-memory-l1-1-3.dll";

		internal const string Mswsock = "mswsock.dll";

		internal const string NCrypt = "ncrypt.dll";

		internal const string NtDll = "ntdll.dll";

		internal const string Odbc32 = "odbc32.dll";

		internal const string OleAut32 = "oleaut32.dll";

		internal const string PerfCounter = "perfcounter.dll";

		internal const string RoBuffer = "api-ms-win-core-winrt-robuffer-l1-1-0.dll";

		internal const string Secur32 = "secur32.dll";

		internal const string Shell32 = "shell32.dll";

		internal const string SspiCli = "sspicli.dll";

		internal const string User32 = "user32.dll";

		internal const string Version = "version.dll";

		internal const string WebSocket = "websocket.dll";

		internal const string WinHttp = "winhttp.dll";

		internal const string Ws2_32 = "ws2_32.dll";

		internal const string Wtsapi32 = "wtsapi32.dll";

		internal const string CompressionNative = "clrcompression.dll";
	}
}
namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access encrypted data and memory. This class cannot be inherited.</summary>
	[Serializable]
	public sealed class DataProtectionPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		private const int version = 1;

		private DataProtectionPermissionFlags _flags;

		/// <summary>Gets or sets the data and memory protection flags.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value is not a valid combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values.</exception>
		public DataProtectionPermissionFlags Flags
		{
			get
			{
				return _flags;
			}
			set
			{
				if ((value & ~DataProtectionPermissionFlags.AllFlags) != DataProtectionPermissionFlags.NoFlags)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "DataProtectionPermissionFlags");
				}
				_flags = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.DataProtectionPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value.</exception>
		public DataProtectionPermission(PermissionState state)
		{
			if (System.Security.Permissions.PermissionHelper.CheckPermissionState(state, allowUnrestricted: true) == PermissionState.Unrestricted)
			{
				_flags = DataProtectionPermissionFlags.AllFlags;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.DataProtectionPermission" /> class with the specified permission flags.</summary>
		/// <param name="flag">A bitwise combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flag" /> is not a valid combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values.</exception>
		public DataProtectionPermission(DataProtectionPermissionFlags flag)
		{
			Flags = flag;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		public bool IsUnrestricted()
		{
			return _flags == DataProtectionPermissionFlags.AllFlags;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		public override IPermission Copy()
		{
			return new DataProtectionPermission(_flags);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and does not specify a permission of the same type as the current permission.</exception>
		public override IPermission Intersect(IPermission target)
		{
			DataProtectionPermission dataProtectionPermission = Cast(target);
			if (dataProtectionPermission == null)
			{
				return null;
			}
			if (IsUnrestricted() && dataProtectionPermission.IsUnrestricted())
			{
				return new DataProtectionPermission(PermissionState.Unrestricted);
			}
			if (IsUnrestricted())
			{
				return dataProtectionPermission.Copy();
			}
			if (dataProtectionPermission.IsUnrestricted())
			{
				return Copy();
			}
			return new DataProtectionPermission(_flags & dataProtectionPermission._flags);
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and does not specify a permission of the same type as the current permission.</exception>
		public override IPermission Union(IPermission target)
		{
			DataProtectionPermission dataProtectionPermission = Cast(target);
			if (dataProtectionPermission == null)
			{
				return Copy();
			}
			if (IsUnrestricted() || dataProtectionPermission.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			return new DataProtectionPermission(_flags | dataProtectionPermission._flags);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission to test for the subset relationship. This permission must be the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and does not specify a permission of the same type as the current permission.</exception>
		public override bool IsSubsetOf(IPermission target)
		{
			DataProtectionPermission dataProtectionPermission = Cast(target);
			if (dataProtectionPermission == null)
			{
				return _flags == DataProtectionPermissionFlags.NoFlags;
			}
			if (dataProtectionPermission.IsUnrestricted())
			{
				return true;
			}
			if (IsUnrestricted())
			{
				return false;
			}
			return (_flags & ~dataProtectionPermission._flags) == 0;
		}

		/// <summary>Reconstructs a permission with a specific state from an XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding used to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a valid permission element.  
		/// -or-  
		/// The version number of <paramref name="securityElement" /> is not supported.</exception>
		public override void FromXml(SecurityElement securityElement)
		{
			System.Security.Permissions.PermissionHelper.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			_flags = (DataProtectionPermissionFlags)Enum.Parse(typeof(DataProtectionPermissionFlags), securityElement.Attribute("Flags"));
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including state information.</returns>
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = System.Security.Permissions.PermissionHelper.Element(typeof(DataProtectionPermission), 1);
			securityElement.AddAttribute("Flags", _flags.ToString());
			return securityElement;
		}

		private DataProtectionPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			DataProtectionPermission obj = target as DataProtectionPermission;
			if (obj == null)
			{
				System.Security.Permissions.PermissionHelper.ThrowInvalidPermission(target, typeof(DataProtectionPermission));
			}
			return obj;
		}
	}
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.DataProtectionPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	[Serializable]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	public sealed class DataProtectionPermissionAttribute : CodeAccessSecurityAttribute
	{
		private DataProtectionPermissionFlags _flags;

		/// <summary>Gets or sets the data protection permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.DataProtectionPermissionFlags.NoFlags" />.</returns>
		public DataProtectionPermissionFlags Flags
		{
			get
			{
				return _flags;
			}
			set
			{
				if ((value & DataProtectionPermissionFlags.AllFlags) != value)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid flags {0}"), value), "DataProtectionPermissionFlags");
				}
				_flags = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether data can be encrypted using the <see cref="T:System.Security.Cryptography.ProtectedData" /> class.</summary>
		/// <returns>
		///   <see langword="true" /> if data can be encrypted; otherwise, <see langword="false" />.</returns>
		public bool ProtectData
		{
			get
			{
				return (_flags & DataProtectionPermissionFlags.ProtectData) != 0;
			}
			set
			{
				if (value)
				{
					_flags |= DataProtectionPermissionFlags.ProtectData;
				}
				else
				{
					_flags &= ~DataProtectionPermissionFlags.ProtectData;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether data can be unencrypted using the <see cref="T:System.Security.Cryptography.ProtectedData" /> class.</summary>
		/// <returns>
		///   <see langword="true" /> if data can be unencrypted; otherwise, <see langword="false" />.</returns>
		public bool UnprotectData
		{
			get
			{
				return (_flags & DataProtectionPermissionFlags.UnprotectData) != 0;
			}
			set
			{
				if (value)
				{
					_flags |= DataProtectionPermissionFlags.UnprotectData;
				}
				else
				{
					_flags &= ~DataProtectionPermissionFlags.UnprotectData;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether memory can be encrypted using the <see cref="T:System.Security.Cryptography.ProtectedMemory" /> class.</summary>
		/// <returns>
		///   <see langword="true" /> if memory can be encrypted; otherwise, <see langword="false" />.</returns>
		public bool ProtectMemory
		{
			get
			{
				return (_flags & DataProtectionPermissionFlags.ProtectMemory) != 0;
			}
			set
			{
				if (value)
				{
					_flags |= DataProtectionPermissionFlags.ProtectMemory;
				}
				else
				{
					_flags &= ~DataProtectionPermissionFlags.ProtectMemory;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether memory can be unencrypted using the <see cref="T:System.Security.Cryptography.ProtectedMemory" /> class.</summary>
		/// <returns>
		///   <see langword="true" /> if memory can be unencrypted; otherwise, <see langword="false" />.</returns>
		public bool UnprotectMemory
		{
			get
			{
				return (_flags & DataProtectionPermissionFlags.UnprotectMemory) != 0;
			}
			set
			{
				if (value)
				{
					_flags |= DataProtectionPermissionFlags.UnprotectMemory;
				}
				else
				{
					_flags &= ~DataProtectionPermissionFlags.UnprotectMemory;
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.DataProtectionPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		public DataProtectionPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.DataProtectionPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.DataProtectionPermission" /> that corresponds to the attribute.</returns>
		public override IPermission CreatePermission()
		{
			DataProtectionPermission dataProtectionPermission = null;
			if (base.Unrestricted)
			{
				return new DataProtectionPermission(PermissionState.Unrestricted);
			}
			return new DataProtectionPermission(_flags);
		}
	}
	/// <summary>Specifies the access permissions for encrypting data and memory.</summary>
	[Serializable]
	[Flags]
	public enum DataProtectionPermissionFlags
	{
		/// <summary>No protection abilities.</summary>
		NoFlags = 0,
		/// <summary>The ability to encrypt data.</summary>
		ProtectData = 1,
		/// <summary>The ability to unencrypt data.</summary>
		UnprotectData = 2,
		/// <summary>The ability to encrypt memory.</summary>
		ProtectMemory = 4,
		/// <summary>The ability to unencrypt memory.</summary>
		UnprotectMemory = 8,
		/// <summary>The ability to encrypt data, encrypt memory, unencrypt data, and unencrypt memory.</summary>
		AllFlags = 0xF
	}
	internal sealed class PermissionHelper
	{
		internal static SecurityElement Element(Type type, int version)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		internal static PermissionState CheckPermissionState(PermissionState state, bool allowUnrestricted)
		{
			switch (state)
			{
			case PermissionState.Unrestricted:
				if (!allowUnrestricted)
				{
					throw new ArgumentException(Locale.GetText("Unrestricted isn't not allowed for identity permissions."), "state");
				}
				break;
			default:
				throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), state), "state");
			case PermissionState.None:
				break;
			}
			return state;
		}

		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Attribute("class") == null)
			{
				throw new ArgumentException(Locale.GetText("Missing 'class' attribute."), parameterName);
			}
			int num = minimumVersion;
			string text = se.Attribute("version");
			if (text != null)
			{
				try
				{
					num = int.Parse(text);
				}
				catch (Exception innerException)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Couldn't parse version from '{0}'."), text), parameterName, innerException);
				}
			}
			if (num < minimumVersion || num > maximumVersion)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Unknown version '{0}', expected versions between ['{1}','{2}']."), num, minimumVersion, maximumVersion), parameterName);
			}
			return num;
		}

		internal static bool IsUnrestricted(SecurityElement se)
		{
			string text = se.Attribute("Unrestricted");
			if (text == null)
			{
				return false;
			}
			return string.Compare(text, bool.TrueString, ignoreCase: true, CultureInfo.InvariantCulture) == 0;
		}

		internal static void ThrowInvalidPermission(IPermission target, Type expected)
		{
			throw new ArgumentException(string.Format(Locale.GetText("Invalid permission type '{0}', expected type '{1}'."), target.GetType(), expected), "target");
		}
	}
}
namespace System.Security.Cryptography
{
	/// <summary>Contains a type and a collection of values associated with that type.</summary>
	public sealed class CryptographicAttributeObject
	{
		private readonly Oid _oid;

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.Oid" /> object that specifies the object identifier for the attribute.</summary>
		/// <returns>The object identifier for the attribute.</returns>
		public Oid Oid => new Oid(_oid);

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> collection that contains the set of values that are associated with the attribute.</summary>
		/// <returns>The set of values that is associated with the attribute.</returns>
		public AsnEncodedDataCollection Values { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> class using an attribute represented by the specified <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
		/// <param name="oid">The attribute to store in this <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object.</param>
		public CryptographicAttributeObject(Oid oid)
			: this(oid, new AsnEncodedDataCollection())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> class using an attribute represented by the specified <see cref="T:System.Security.Cryptography.Oid" /> object and the set of values associated with that attribute represented by the specified <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> collection.</summary>
		/// <param name="oid">The attribute to store in this <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object.</param>
		/// <param name="values">The set of values associated with the attribute represented by the <paramref name="oid" /> parameter.</param>
		/// <exception cref="T:System.InvalidOperationException">The collection contains duplicate items.</exception>
		public CryptographicAttributeObject(Oid oid, AsnEncodedDataCollection values)
		{
			_oid = new Oid(oid);
			if (values == null)
			{
				Values = new AsnEncodedDataCollection();
				return;
			}
			AsnEncodedDataEnumerator enumerator = values.GetEnumerator();
			while (enumerator.MoveNext())
			{
				AsnEncodedData current = enumerator.Current;
				if (!string.Equals(current.Oid.Value, oid.Value, StringComparison.Ordinal))
				{
					throw new InvalidOperationException(global::SR.Format("AsnEncodedData element in the collection has wrong Oid value: expected = '{0}', actual = '{1}'.", oid.Value, current.Oid.Value));
				}
			}
			Values = values;
		}
	}
	/// <summary>Contains a set of <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> objects.</summary>
	public sealed class CryptographicAttributeObjectCollection : ICollection, IEnumerable
	{
		private readonly List<CryptographicAttributeObject> _list;

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object at the specified index in the collection.</summary>
		/// <param name="index">An <see cref="T:System.Int32" /> value that represents the zero-based index of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to retrieve.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object at the specified index.</returns>
		public CryptographicAttributeObject this[int index] => _list[index];

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		public int Count => _list.Count;

		/// <summary>Gets a value that indicates whether access to the collection is synchronized, or thread safe.</summary>
		/// <returns>
		///   <see langword="true" /> if access to the collection is thread safe; otherwise <see langword="false" />.</returns>
		public bool IsSynchronized => false;

		/// <summary>Gets an <see cref="T:System.Object" /> object used to synchronize access to the collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> object used to synchronize access to the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		public object SyncRoot => this;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> class.</summary>
		public CryptographicAttributeObjectCollection()
		{
			_list = new List<CryptographicAttributeObject>();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> class, adding a specified <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> to the collection.</summary>
		/// <param name="attribute">A <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object that is added to the collection.</param>
		public CryptographicAttributeObjectCollection(CryptographicAttributeObject attribute)
		{
			_list = new List<CryptographicAttributeObject>();
			_list.Add(attribute);
		}

		/// <summary>Adds the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to the collection.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to add to the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the method returns the zero-based index of the added item; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public int Add(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			return Add(new CryptographicAttributeObject(asnEncodedData.Oid, new AsnEncodedDataCollection(asnEncodedData)));
		}

		/// <summary>Adds the specified <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to the collection.</summary>
		/// <param name="attribute">The <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to add to the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the method returns the zero-based index of the added item; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified item already exists in the collection.</exception>
		public int Add(CryptographicAttributeObject attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			string value = attribute.Oid.Value;
			for (int i = 0; i < _list.Count; i++)
			{
				CryptographicAttributeObject cryptographicAttributeObject = _list[i];
				if (cryptographicAttributeObject.Values == attribute.Values)
				{
					throw new InvalidOperationException("Duplicate items are not allowed in the collection.");
				}
				string value2 = cryptographicAttributeObject.Oid.Value;
				if (string.Equals(value, value2, StringComparison.OrdinalIgnoreCase))
				{
					if (string.Equals(value, "1.2.840.113549.1.9.5", StringComparison.OrdinalIgnoreCase))
					{
						throw new CryptographicException("Cannot add multiple PKCS 9 signing time attributes.");
					}
					AsnEncodedDataEnumerator enumerator = attribute.Values.GetEnumerator();
					while (enumerator.MoveNext())
					{
						AsnEncodedData current = enumerator.Current;
						cryptographicAttributeObject.Values.Add(current);
					}
					return i;
				}
			}
			int count = _list.Count;
			_list.Add(attribute);
			return count;
		}

		internal void AddWithoutMerge(CryptographicAttributeObject attribute)
		{
			_list.Add(attribute);
		}

		/// <summary>Removes the specified <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object from the collection.</summary>
		/// <param name="attribute">The <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attribute" /> is <see langword="null" />.</exception>
		public void Remove(CryptographicAttributeObject attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			_list.Remove(attribute);
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectEnumerator" /> object for the collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the method returns a <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectEnumerator" /> object that can be used to enumerate the collection; otherwise, <see langword="false" />.</returns>
		public CryptographicAttributeObjectEnumerator GetEnumerator()
		{
			return new CryptographicAttributeObjectEnumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CryptographicAttributeObjectEnumerator(this);
		}

		/// <summary>Copies the elements of this <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection to an <see cref="T:System.Array" /> array, starting at a particular index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> array that is the destination of the elements copied from this <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" />. The <see cref="T:System.Array" /> array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (index > array.Length - Count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			for (int i = 0; i < Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection to an array of <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> objects.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> objects that the collection is copied to.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> to which the collection is to be copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see langword="null" /> was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		public void CopyTo(CryptographicAttributeObject[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (index > array.Length - Count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			_list.CopyTo(array, index);
		}
	}
	/// <summary>Provides enumeration functionality for the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection. This class cannot be inherited.</summary>
	public sealed class CryptographicAttributeObjectEnumerator : IEnumerator
	{
		private readonly CryptographicAttributeObjectCollection _attributes;

		private int _current;

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object from the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object that represents the current cryptographic attribute in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		public CryptographicAttributeObject Current => _attributes[_current];

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object from the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object that represents the current cryptographic attribute in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		object IEnumerator.Current => _attributes[_current];

		internal CryptographicAttributeObjectEnumerator(CryptographicAttributeObjectCollection attributes)
		{
			_attributes = attributes;
			_current = -1;
		}

		/// <summary>Advances the enumeration to the next <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object; <see langword="false" /> if the enumerator is at the end of the enumeration.</returns>
		public bool MoveNext()
		{
			if (_current >= _attributes.Count - 1)
			{
				return false;
			}
			_current++;
			return true;
		}

		/// <summary>Resets the enumeration to the first <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		public void Reset()
		{
			_current = -1;
		}

		internal CryptographicAttributeObjectEnumerator()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Specifies the scope of the data protection to be applied by the <see cref="M:System.Security.Cryptography.ProtectedData.Protect(System.Byte[],System.Byte[],System.Security.Cryptography.DataProtectionScope)" /> method.</summary>
	public enum DataProtectionScope
	{
		/// <summary>The protected data is associated with the current user. Only threads running under the current user context can unprotect the data.</summary>
		CurrentUser,
		/// <summary>The protected data is associated with the machine context. Any process running on the computer can unprotect data. This enumeration value is usually used in server-specific applications that run on a server where untrusted users are not allowed access.</summary>
		LocalMachine
	}
	/// <summary>Provides methods for encrypting and decrypting data. This class cannot be inherited.</summary>
	public static class ProtectedData
	{
		private static readonly byte[] s_nonEmpty = new byte[1];

		/// <summary>Encrypts the data in a specified byte array and returns a byte array that contains the encrypted data.</summary>
		/// <param name="userData">A byte array that contains data to encrypt.</param>
		/// <param name="optionalEntropy">An optional additional byte array used to increase the complexity of the encryption, or <see langword="null" /> for no additional complexity.</param>
		/// <param name="scope">One of the enumeration values that specifies the scope of encryption.</param>
		/// <returns>A byte array representing the encrypted data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="userData" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The encryption failed.</exception>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this method.</exception>
		/// <exception cref="T:System.OutOfMemoryException">The system ran out of memory while encrypting the data.</exception>
		public static byte[] Protect(byte[] userData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			return ProtectOrUnprotect(userData, optionalEntropy, scope, protect: true);
		}

		/// <summary>Decrypts the data in a specified byte array and returns a byte array that contains the decrypted data.</summary>
		/// <param name="encryptedData">A byte array containing data encrypted using the <see cref="M:System.Security.Cryptography.ProtectedData.Protect(System.Byte[],System.Byte[],System.Security.Cryptography.DataProtectionScope)" /> method.</param>
		/// <param name="optionalEntropy">An optional additional byte array that was used to encrypt the data, or <see langword="null" /> if the additional byte array was not used.</param>
		/// <param name="scope">One of the enumeration values that specifies the scope of data protection that was used to encrypt the data.</param>
		/// <returns>A byte array representing the decrypted data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="encryptedData" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The decryption failed.</exception>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this method.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		public static byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			return ProtectOrUnprotect(encryptedData, optionalEntropy, scope, protect: false);
		}

		private unsafe static byte[] ProtectOrUnprotect(byte[] inputData, byte[] optionalEntropy, DataProtectionScope scope, bool protect)
		{
			fixed (byte* ptr = ((inputData.Length == 0) ? s_nonEmpty : inputData))
			{
				fixed (byte* ptr2 = optionalEntropy)
				{
					global::Interop.Crypt32.DATA_BLOB pDataIn = new global::Interop.Crypt32.DATA_BLOB((IntPtr)ptr, (uint)inputData.Length);
					global::Interop.Crypt32.DATA_BLOB pOptionalEntropy = default(global::Interop.Crypt32.DATA_BLOB);
					if (optionalEntropy != null)
					{
						pOptionalEntropy = new global::Interop.Crypt32.DATA_BLOB((IntPtr)ptr2, (uint)optionalEntropy.Length);
					}
					global::Interop.Crypt32.CryptProtectDataFlags cryptProtectDataFlags = global::Interop.Crypt32.CryptProtectDataFlags.CRYPTPROTECT_UI_FORBIDDEN;
					if (scope == DataProtectionScope.LocalMachine)
					{
						cryptProtectDataFlags |= global::Interop.Crypt32.CryptProtectDataFlags.CRYPTPROTECT_LOCAL_MACHINE;
					}
					global::Interop.Crypt32.DATA_BLOB pDataOut = default(global::Interop.Crypt32.DATA_BLOB);
					try
					{
						if (!(protect ? global::Interop.Crypt32.CryptProtectData(ref pDataIn, null, ref pOptionalEntropy, IntPtr.Zero, IntPtr.Zero, cryptProtectDataFlags, out pDataOut) : global::Interop.Crypt32.CryptUnprotectData(ref pDataIn, IntPtr.Zero, ref pOptionalEntropy, IntPtr.Zero, IntPtr.Zero, cryptProtectDataFlags, out pDataOut)))
						{
							int lastWin32Error = Marshal.GetLastWin32Error();
							if (protect && ErrorMayBeCausedByUnloadedProfile(lastWin32Error))
							{
								throw new CryptographicException("The data protection operation was unsuccessful. This may have been caused by not having the user profile loaded for the current thread's user context, which may be the case when the thread is impersonating.");
							}
							throw lastWin32Error.ToCryptographicException();
						}
						if (pDataOut.pbData == IntPtr.Zero)
						{
							throw new OutOfMemoryException();
						}
						int cbData = (int)pDataOut.cbData;
						byte[] array = new byte[cbData];
						Marshal.Copy(pDataOut.pbData, array, 0, cbData);
						return array;
					}
					finally
					{
						if (pDataOut.pbData != IntPtr.Zero)
						{
							int cbData2 = (int)pDataOut.cbData;
							byte* ptr3 = (byte*)(void*)pDataOut.pbData;
							for (int i = 0; i < cbData2; i++)
							{
								ptr3[i] = 0;
							}
							Marshal.FreeHGlobal(pDataOut.pbData);
						}
					}
				}
			}
		}

		private static bool ErrorMayBeCausedByUnloadedProfile(int errorCode)
		{
			if (errorCode != -2147024894)
			{
				return errorCode == 2;
			}
			return true;
		}
	}
	/// <summary>Provides the base class for data protectors.</summary>
	public abstract class DataProtector
	{
		private string m_applicationName;

		private string m_primaryPurpose;

		private IEnumerable<string> m_specificPurposes;

		private volatile byte[] m_hashedPurpose;

		/// <summary>Gets the name of the application.</summary>
		/// <returns>The name of the application.</returns>
		protected string ApplicationName => m_applicationName;

		/// <summary>Specifies whether the hash is prepended to the text array before encryption.</summary>
		/// <returns>Always <see langword="true" />.</returns>
		protected virtual bool PrependHashedPurposeToPlaintext => true;

		/// <summary>Gets the primary purpose for the protected data.</summary>
		/// <returns>The primary purpose for the protected data.</returns>
		protected string PrimaryPurpose => m_primaryPurpose;

		/// <summary>Gets the specific purposes for the protected data.</summary>
		/// <returns>A collection of the specific purposes for the protected data.</returns>
		protected IEnumerable<string> SpecificPurposes => m_specificPurposes;

		/// <summary>Creates a new instance of the <see cref="T:System.Security.Cryptography.DataProtector" /> class by using the provided application name, primary purpose, and specific purposes.</summary>
		/// <param name="applicationName">The name of the application.</param>
		/// <param name="primaryPurpose">The primary purpose for the protected data.</param>
		/// <param name="specificPurposes">The specific purposes for the protected data.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="applicationName" /> is an empty string or <see langword="null" />.  
		/// -or-  
		/// <paramref name="primaryPurpose" /> is an empty string or <see langword="null" />.  
		/// -or-  
		/// <paramref name="specificPurposes" /> contains an empty string or <see langword="null" />.</exception>
		protected DataProtector(string applicationName, string primaryPurpose, string[] specificPurposes)
		{
			if (string.IsNullOrWhiteSpace(applicationName))
			{
				throw new ArgumentException("Invalid application name and/or purpose", "applicationName");
			}
			if (string.IsNullOrWhiteSpace(primaryPurpose))
			{
				throw new ArgumentException("Invalid application name and/or purpose", "primaryPurpose");
			}
			if (specificPurposes != null)
			{
				for (int i = 0; i < specificPurposes.Length; i++)
				{
					if (string.IsNullOrWhiteSpace(specificPurposes[i]))
					{
						throw new ArgumentException("Invalid application name and/or purpose", "specificPurposes");
					}
				}
			}
			m_applicationName = applicationName;
			m_primaryPurpose = primaryPurpose;
			List<string> list = new List<string>();
			if (specificPurposes != null)
			{
				list.AddRange(specificPurposes);
			}
			m_specificPurposes = list;
		}

		/// <summary>Creates a hash of the property values specified by the constructor.</summary>
		/// <returns>An array of bytes that contain the hash of the <see cref="P:System.Security.Cryptography.DataProtector.ApplicationName" />, <see cref="P:System.Security.Cryptography.DataProtector.PrimaryPurpose" />, and <see cref="P:System.Security.Cryptography.DataProtector.SpecificPurposes" /> properties.</returns>
		protected virtual byte[] GetHashedPurpose()
		{
			if (m_hashedPurpose == null)
			{
				using HashAlgorithm hashAlgorithm = HashAlgorithm.Create("System.Security.Cryptography.Sha256Cng");
				using (BinaryWriter binaryWriter = new BinaryWriter(new CryptoStream(new MemoryStream(), hashAlgorithm, CryptoStreamMode.Write), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true)))
				{
					binaryWriter.Write(ApplicationName);
					binaryWriter.Write(PrimaryPurpose);
					foreach (string specificPurpose in SpecificPurposes)
					{
						binaryWriter.Write(specificPurpose);
					}
				}
				m_hashedPurpose = hashAlgorithm.Hash;
			}
			return m_hashedPurpose;
		}

		/// <summary>Determines if re-encryption is required for the specified encrypted data.</summary>
		/// <param name="encryptedData">The encrypted data to be evaluated.</param>
		/// <returns>
		///   <see langword="true" /> if the data must be re-encrypted; otherwise, <see langword="false" />.</returns>
		public abstract bool IsReprotectRequired(byte[] encryptedData);

		/// <summary>Creates an instance of a data protector implementation by using the specified class name of the data protector, the application name, the primary purpose, and the specific purposes.</summary>
		/// <param name="providerClass">The class name for the data protector.</param>
		/// <param name="applicationName">The name of the application.</param>
		/// <param name="primaryPurpose">The primary purpose for the protected data.</param>
		/// <param name="specificPurposes">The specific purposes for the protected data.</param>
		/// <returns>A data protector implementation object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerClass" /> is <see langword="null" />.</exception>
		public static DataProtector Create(string providerClass, string applicationName, string primaryPurpose, params string[] specificPurposes)
		{
			if (providerClass == null)
			{
				throw new ArgumentNullException("providerClass");
			}
			return (DataProtector)CryptoConfig.CreateFromName(providerClass, applicationName, primaryPurpose, specificPurposes);
		}

		/// <summary>Protects the specified user data.</summary>
		/// <param name="userData">The data to be protected.</param>
		/// <returns>A byte array that contains the encrypted data.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="userData" /> is <see langword="null" />.</exception>
		public byte[] Protect(byte[] userData)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			if (PrependHashedPurposeToPlaintext)
			{
				byte[] hashedPurpose = GetHashedPurpose();
				byte[] array = new byte[userData.Length + hashedPurpose.Length];
				Array.Copy(hashedPurpose, 0, array, 0, hashedPurpose.Length);
				Array.Copy(userData, 0, array, hashedPurpose.Length, userData.Length);
				userData = array;
			}
			return ProviderProtect(userData);
		}

		/// <summary>Specifies the delegate method in the derived class that the <see cref="M:System.Security.Cryptography.DataProtector.Protect(System.Byte[])" /> method in the base class calls back into.</summary>
		/// <param name="userData">The data to be encrypted.</param>
		/// <returns>A byte array that contains the encrypted data.</returns>
		protected abstract byte[] ProviderProtect(byte[] userData);

		/// <summary>Specifies the delegate method in the derived class that the <see cref="M:System.Security.Cryptography.DataProtector.Unprotect(System.Byte[])" /> method in the base class calls back into.</summary>
		/// <param name="encryptedData">The data to be unencrypted.</param>
		/// <returns>The unencrypted data.</returns>
		protected abstract byte[] ProviderUnprotect(byte[] encryptedData);

		/// <summary>Unprotects the specified protected data.</summary>
		/// <param name="encryptedData">The encrypted data to be unprotected.</param>
		/// <returns>A byte array that contains the plain-text data.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="encryptedData" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="encryptedData" /> contained an invalid purpose.</exception>
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public byte[] Unprotect(byte[] encryptedData)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (PrependHashedPurposeToPlaintext)
			{
				byte[] array = ProviderUnprotect(encryptedData);
				byte[] hashedPurpose = GetHashedPurpose();
				bool flag = array.Length >= hashedPurpose.Length;
				for (int i = 0; i < hashedPurpose.Length; i++)
				{
					if (hashedPurpose[i] != array[i % array.Length])
					{
						flag = false;
					}
				}
				if (!flag)
				{
					throw new CryptographicException("Invalid data protection purpose");
				}
				byte[] array2 = new byte[array.Length - hashedPurpose.Length];
				Array.Copy(array, hashedPurpose.Length, array2, 0, array2.Length);
				return array2;
			}
			return ProviderUnprotect(encryptedData);
		}
	}
	/// <summary>Specifies the scope of memory protection to be applied by the <see cref="M:System.Security.Cryptography.ProtectedMemory.Protect(System.Byte[],System.Security.Cryptography.MemoryProtectionScope)" /> method.</summary>
	public enum MemoryProtectionScope
	{
		/// <summary>Only code running in the same process as the code that called the <see cref="M:System.Security.Cryptography.ProtectedMemory.Protect(System.Byte[],System.Security.Cryptography.MemoryProtectionScope)" /> method can unprotect memory.</summary>
		SameProcess,
		/// <summary>All code in any process can unprotect memory that was protected using the <see cref="M:System.Security.Cryptography.ProtectedMemory.Protect(System.Byte[],System.Security.Cryptography.MemoryProtectionScope)" /> method.</summary>
		CrossProcess,
		/// <summary>Only code running in the same user context as the code that called the <see cref="M:System.Security.Cryptography.ProtectedMemory.Protect(System.Byte[],System.Security.Cryptography.MemoryProtectionScope)" /> method can unprotect memory.</summary>
		SameLogon
	}
	/// <summary>Provides methods for protecting and unprotecting memory. This class cannot be inherited.</summary>
	public sealed class ProtectedMemory
	{
		private enum MemoryProtectionImplementation
		{
			Unknown = 0,
			Win32RtlEncryptMemory = 1,
			Win32CryptoProtect = 2,
			Unsupported = int.MinValue
		}

		private const int BlockSize = 16;

		private static MemoryProtectionImplementation impl;

		private ProtectedMemory()
		{
		}

		/// <summary>Protects the specified data.</summary>
		/// <param name="userData">The byte array containing data in memory to protect. The array must be a multiple of 16 bytes.</param>
		/// <param name="scope">One of the enumeration values that specifies the scope of memory protection.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="userData" /> must be 16 bytes in length or in multiples of 16 bytes.</exception>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this method. This method can be used only with the Windows 2000 or later operating systems.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="userData" /> is <see langword="null" />.</exception>
		[System.MonoTODO("only supported on Windows 2000 SP3 and later")]
		public static void Protect(byte[] userData, MemoryProtectionScope scope)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			Check(userData.Length, scope);
			try
			{
				uint cbData = (uint)userData.Length;
				switch (impl)
				{
				case MemoryProtectionImplementation.Win32RtlEncryptMemory:
				{
					int num = RtlEncryptMemory(userData, cbData, (uint)scope);
					if (num < 0)
					{
						throw new CryptographicException(Locale.GetText("Error. NTSTATUS = {0}.", num));
					}
					break;
				}
				case MemoryProtectionImplementation.Win32CryptoProtect:
					if (!CryptProtectMemory(userData, cbData, (uint)scope))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					break;
				default:
					throw new PlatformNotSupportedException();
				}
			}
			catch
			{
				impl = MemoryProtectionImplementation.Unsupported;
				throw new PlatformNotSupportedException();
			}
		}

		/// <summary>Unprotects data in memory that was protected using the <see cref="M:System.Security.Cryptography.ProtectedMemory.Protect(System.Byte[],System.Security.Cryptography.MemoryProtectionScope)" /> method.</summary>
		/// <param name="encryptedData">The byte array in memory to unencrypt.</param>
		/// <param name="scope">One of the enumeration values that specifies the scope of memory protection.</param>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this method. This method can be used only with the Windows 2000 or later operating systems.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="encryptedData" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="encryptedData" /> is empty.  
		/// -or-  
		/// This call was not implemented.  
		/// -or-  
		/// NTSTATUS contains an error.</exception>
		[System.MonoTODO("only supported on Windows 2000 SP3 and later")]
		public static void Unprotect(byte[] encryptedData, MemoryProtectionScope scope)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			Check(encryptedData.Length, scope);
			try
			{
				uint cbData = (uint)encryptedData.Length;
				switch (impl)
				{
				case MemoryProtectionImplementation.Win32RtlEncryptMemory:
				{
					int num = RtlDecryptMemory(encryptedData, cbData, (uint)scope);
					if (num < 0)
					{
						throw new CryptographicException(Locale.GetText("Error. NTSTATUS = {0}.", num));
					}
					break;
				}
				case MemoryProtectionImplementation.Win32CryptoProtect:
					if (!CryptUnprotectMemory(encryptedData, cbData, (uint)scope))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					break;
				default:
					throw new PlatformNotSupportedException();
				}
			}
			catch
			{
				impl = MemoryProtectionImplementation.Unsupported;
				throw new PlatformNotSupportedException();
			}
		}

		private static void Detect()
		{
			OperatingSystem oSVersion = Environment.OSVersion;
			if (oSVersion.Platform == PlatformID.Win32NT)
			{
				Version version = oSVersion.Version;
				if (version.Major < 5)
				{
					impl = MemoryProtectionImplementation.Unsupported;
				}
				else if (version.Major == 5)
				{
					if (version.Minor < 2)
					{
						impl = MemoryProtectionImplementation.Win32RtlEncryptMemory;
					}
					else
					{
						impl = MemoryProtectionImplementation.Win32CryptoProtect;
					}
				}
				else
				{
					impl = MemoryProtectionImplementation.Win32CryptoProtect;
				}
			}
			else
			{
				impl = MemoryProtectionImplementation.Unsupported;
			}
		}

		private static void Check(int size, MemoryProtectionScope scope)
		{
			if (size % 16 != 0)
			{
				throw new CryptographicException(Locale.GetText("Not a multiple of {0} bytes.", 16));
			}
			if (scope < MemoryProtectionScope.SameProcess || scope > MemoryProtectionScope.SameLogon)
			{
				throw new ArgumentException(Locale.GetText("Invalid enum value for '{0}'.", "MemoryProtectionScope"), "scope");
			}
			switch (impl)
			{
			case MemoryProtectionImplementation.Unknown:
				Detect();
				break;
			case MemoryProtectionImplementation.Unsupported:
				throw new PlatformNotSupportedException();
			}
		}

		[DllImport("advapi32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "SystemFunction040", SetLastError = true)]
		[SuppressUnmanagedCodeSecurity]
		private static extern int RtlEncryptMemory(byte[] pData, uint cbData, uint dwFlags);

		[DllImport("advapi32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "SystemFunction041", SetLastError = true)]
		[SuppressUnmanagedCodeSecurity]
		private static extern int RtlDecryptMemory(byte[] pData, uint cbData, uint dwFlags);

		[DllImport("crypt32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
		[SuppressUnmanagedCodeSecurity]
		private static extern bool CryptProtectMemory(byte[] pData, uint cbData, uint dwFlags);

		[DllImport("crypt32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
		[SuppressUnmanagedCodeSecurity]
		private static extern bool CryptUnprotectMemory(byte[] pData, uint cbData, uint dwFlags);
	}
}
namespace System.Security.Cryptography.Translation
{
	internal class SR
	{
		public const string Cryptography_DataProtector_InvalidAppNameOrPurpose = "Invalid application name and/or purpose";

		public const string Cryptography_DataProtector_InvalidPurpose = "Invalid data protection purpose";

		public const string ArgumentOutOfRange_Index = "Index was out of range.  Must be non-negative and less than the size of the collection.";

		public const string Arg_EmptyOrNullString = "String cannot be empty or null.";

		public const string Cryptography_Partial_Chain = "A certificate chain could not be built to a trusted root authority.";

		public const string Cryptography_Xml_BadWrappedKeySize = "Bad wrapped key size.";

		public const string Cryptography_Xml_CipherValueElementRequired = "A Cipher Data element should have either a CipherValue or a CipherReference element.";

		public const string Cryptography_Xml_CreateHashAlgorithmFailed = "Could not create hash algorithm object.";

		public const string Cryptography_Xml_CreateTransformFailed = "Could not create the XML transformation identified by the URI {0}.";

		public const string Cryptography_Xml_CreatedKeyFailed = "Failed to create signing key.";

		public const string Cryptography_Xml_DigestMethodRequired = "A DigestMethod must be specified on a Reference prior to generating XML.";

		public const string Cryptography_Xml_DigestValueRequired = "A Reference must contain a DigestValue.";

		public const string Cryptography_Xml_EnvelopedSignatureRequiresContext = "An XmlDocument context is required for enveloped transforms.";

		public const string Cryptography_Xml_InvalidElement = "Malformed element {0}.";

		public const string Cryptography_Xml_InvalidEncryptionProperty = "Malformed encryption property element.";

		public const string Cryptography_Xml_InvalidKeySize = "The key size should be a non negative integer.";

		public const string Cryptography_Xml_InvalidReference = "Malformed reference element.";

		public const string Cryptography_Xml_InvalidSignatureLength = "The length of the signature with a MAC should be less than the hash output length.";

		public const string Cryptography_Xml_InvalidSignatureLength2 = "The length in bits of the signature with a MAC should be a multiple of 8.";

		public const string Cryptography_Xml_InvalidX509IssuerSerialNumber = "X509 issuer serial number is invalid.";

		public const string Cryptography_Xml_KeyInfoRequired = "A KeyInfo element is required to check the signature.";

		public const string Cryptography_Xml_KW_BadKeySize = "The length of the encrypted data in Key Wrap is either 32, 40 or 48 bytes.";

		public const string Cryptography_Xml_LoadKeyFailed = "Signing key is not loaded.";

		public const string Cryptography_Xml_MissingAlgorithm = "Symmetric algorithm is not specified.";

		public const string Cryptography_Xml_MissingCipherData = "Cipher data is not specified.";

		public const string Cryptography_Xml_MissingDecryptionKey = "Unable to retrieve the decryption key.";

		public const string Cryptography_Xml_MissingEncryptionKey = "Unable to retrieve the encryption key.";

		public const string Cryptography_Xml_NotSupportedCryptographicTransform = "The specified cryptographic transform is not supported.";

		public const string Cryptography_Xml_ReferenceElementRequired = "At least one Reference element is required.";

		public const string Cryptography_Xml_ReferenceTypeRequired = "The Reference type must be set in an EncryptedReference object.";

		public const string Cryptography_Xml_SelfReferenceRequiresContext = "An XmlDocument context is required to resolve the Reference Uri {0}.";

		public const string Cryptography_Xml_SignatureDescriptionNotCreated = "SignatureDescription could not be created for the signature algorithm supplied.";

		public const string Cryptography_Xml_SignatureMethodKeyMismatch = "The key does not fit the SignatureMethod.";

		public const string Cryptography_Xml_SignatureMethodRequired = "A signature method is required.";

		public const string Cryptography_Xml_SignatureValueRequired = "Signature requires a SignatureValue.";

		public const string Cryptography_Xml_SignedInfoRequired = "Signature requires a SignedInfo.";

		public const string Cryptography_Xml_TransformIncorrectInputType = "The input type was invalid for this transform.";

		public const string Cryptography_Xml_IncorrectObjectType = "Type of input object is invalid.";

		public const string Cryptography_Xml_UnknownTransform = "Unknown transform has been encountered.";

		public const string Cryptography_Xml_UriNotResolved = "Unable to resolve Uri {0}.";

		public const string Cryptography_Xml_UriNotSupported = " The specified Uri is not supported.";

		public const string Cryptography_Xml_UriRequired = "A Uri attribute is required for a CipherReference element.";

		public const string Cryptography_Xml_XrmlMissingContext = "Null Context property encountered.";

		public const string Cryptography_Xml_XrmlMissingIRelDecryptor = "IRelDecryptor is required.";

		public const string Cryptography_Xml_XrmlMissingIssuer = "Issuer node is required.";

		public const string Cryptography_Xml_XrmlMissingLicence = "License node is required.";

		public const string Cryptography_Xml_XrmlUnableToDecryptGrant = "Unable to decrypt grant content.";

		public const string NotSupported_KeyAlgorithm = "The certificate key algorithm is not supported.";

		public const string Log_ActualHashValue = "Actual hash value: {0}";

		public const string Log_BeginCanonicalization = "Beginning canonicalization using \"{0}\" ({1}).";

		public const string Log_BeginSignatureComputation = "Beginning signature computation.";

		public const string Log_BeginSignatureVerification = "Beginning signature verification.";

		public const string Log_BuildX509Chain = "Building and verifying the X509 chain for certificate {0}.";

		public const string Log_CanonicalizationSettings = "Canonicalization transform is using resolver {0} and base URI \"{1}\".";

		public const string Log_CanonicalizedOutput = "Output of canonicalization transform: {0}";

		public const string Log_CertificateChain = "Certificate chain:";

		public const string Log_CheckSignatureFormat = "Checking signature format using format validator \"[{0}] {1}.{2}\".";

		public const string Log_CheckSignedInfo = "Checking signature on SignedInfo with id \"{0}\".";

		public const string Log_FormatValidationSuccessful = "Signature format validation was successful.";

		public const string Log_FormatValidationNotSuccessful = "Signature format validation failed.";

		public const string Log_KeyUsages = "Found key usages \"{0}\" in extension {1} on certificate {2}.";

		public const string Log_NoNamespacesPropagated = "No namespaces are being propagated.";

		public const string Log_PropagatingNamespace = "Propagating namespace {0}=\"{1}\".";

		public const string Log_RawSignatureValue = "Raw signature: {0}";

		public const string Log_ReferenceHash = "Reference {0} hashed with \"{1}\" ({2}) has hash value {3}, expected hash value {4}.";

		public const string Log_RevocationMode = "Revocation mode for chain building: {0}.";

		public const string Log_RevocationFlag = "Revocation flag for chain building: {0}.";

		public const string Log_SigningAsymmetric = "Calculating signature with key {0} using signature description {1}, hash algorithm {2}, and asymmetric signature formatter {3}.";

		public const string Log_SigningHmac = "Calculating signature using keyed hash algorithm {0}.";

		public const string Log_SigningReference = "Hashing reference {0}, Uri \"{1}\", Id \"{2}\", Type \"{3}\" with hash algorithm \"{4}\" ({5}).";

		public const string Log_TransformedReferenceContents = "Transformed reference contents: {0}";

		public const string Log_UnsafeCanonicalizationMethod = "Canonicalization method \"{0}\" is not on the safe list. Safe canonicalization methods are: {1}.";

		public const string Log_UrlTimeout = "URL retrieval timeout for chain building: {0}.";

		public const string Log_VerificationFailed = "Verification failed checking {0}.";

		public const string Log_VerificationFailed_References = "references";

		public const string Log_VerificationFailed_SignedInfo = "SignedInfo";

		public const string Log_VerificationFailed_X509Chain = "X509 chain verification";

		public const string Log_VerificationFailed_X509KeyUsage = "X509 key usage verification";

		public const string Log_VerificationFlag = "Verification flags for chain building: {0}.";

		public const string Log_VerificationTime = "Verification time for chain building: {0}.";

		public const string Log_VerificationWithKeySuccessful = "Verification with key {0} was successful.";

		public const string Log_VerificationWithKeyNotSuccessful = "Verification with key {0} was not successful.";

		public const string Log_VerifyReference = "Processing reference {0}, Uri \"{1}\", Id \"{2}\", Type \"{3}\".";

		public const string Log_VerifySignedInfoAsymmetric = "Verifying SignedInfo using key {0}, signature description {1}, hash algorithm {2}, and asymmetric signature deformatter {3}.";

		public const string Log_VerifySignedInfoHmac = "Verifying SignedInfo using keyed hash algorithm {0}.";

		public const string Log_X509ChainError = "Error building X509 chain: {0}: {1}.";

		public const string Log_XmlContext = "Using context: {0}";

		public const string Log_SignedXmlRecursionLimit = "Signed xml recursion limit hit while trying to decrypt the key. Reference {0} hashed with \"{1}\" and ({2}).";

		public const string Log_UnsafeTransformMethod = "Transform method \"{0}\" is not on the safe list. Safe transform methods are: {1}.";

		public const string Arg_RankMultiDimNotSupported = "Only single dimensional arrays are supported for the requested action.";

		public const string Argument_InvalidOffLen = "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.";

		public const string Argument_InvalidOidValue = "The OID value was invalid.";

		public const string Cryptography_Asn_EnumeratedValueRequiresNonFlagsEnum = "ASN.1 Enumerated values only apply to enum types without the [Flags] attribute.";

		public const string Cryptography_Asn_NamedBitListRequiresFlagsEnum = "Named bit list operations require an enum with the [Flags] attribute.";

		public const string Cryptography_Asn_NamedBitListValueTooBig = "The encoded named bit list value is larger than the value size of the '{0}' enum.";

		public const string Cryptography_Asn_UniversalValueIsFixed = "Tags with TagClass Universal must have the appropriate TagValue value for the data type being read or written.";

		public const string Cryptography_Asn_UnusedBitCountRange = "Unused bit count must be between 0 and 7, inclusive.";

		public const string Cryptography_AsnSerializer_AmbiguousFieldType = "Field '{0}' of type '{1}' has ambiguous type '{2}', an attribute derived from AsnTypeAttribute is required.";

		public const string Cryptography_AsnSerializer_Choice_AllowNullNonNullable = "[Choice].AllowNull=true is not valid because type '{0}' cannot have a null value.";

		public const string Cryptography_AsnSerializer_Choice_ConflictingTagMapping = "The tag ({0} {1}) for field '{2}' on type '{3}' already is associated in this context with field '{4}' on type '{5}'.";

		public const string Cryptography_AsnSerializer_Choice_DefaultValueDisallowed = "Field '{0}' on [Choice] type '{1}' has a default value, which is not permitted.";

		public const string Cryptography_AsnSerializer_Choice_NoChoiceWasMade = "An instance of [Choice] type '{0}' has no non-null fields.";

		public const string Cryptography_AsnSerializer_Choice_NonNullableField = "Field '{0}' on [Choice] type '{1}' can not be assigned a null value.";

		public const string Cryptography_AsnSerializer_Choice_TooManyValues = "Fields '{0}' and '{1}' on type '{2}' are both non-null when only one value is permitted.";

		public const string Cryptography_AsnSerializer_Choice_TypeCycle = "Field '{0}' on [Choice] type '{1}' has introduced a type chain cycle.";

		public const string Cryptography_AsnSerializer_MultipleAsnTypeAttributes = "Field '{0}' on type '{1}' has multiple attributes deriving from '{2}' when at most one is permitted.";

		public const string Cryptography_AsnSerializer_NoJaggedArrays = "Type '{0}' cannot be serialized or deserialized because it is an array of arrays.";

		public const string Cryptography_AsnSerializer_NoMultiDimensionalArrays = "Type '{0}' cannot be serialized or deserialized because it is a multi-dimensional array.";

		public const string Cryptography_AsnSerializer_NoOpenTypes = "Type '{0}' cannot be serialized or deserialized because it is not sealed or has unbound generic parameters.";

		public const string Cryptography_AsnSerializer_Optional_NonNullableField = "Field '{0}' on type '{1}' is declared [OptionalValue], but it can not be assigned a null value.";

		public const string Cryptography_AsnSerializer_PopulateFriendlyNameOnString = "Field '{0}' on type '{1}' has [ObjectIdentifier].PopulateFriendlyName set to true, which is not applicable to a string.  Change the field to '{2}' or set PopulateFriendlyName to false.";

		public const string Cryptography_AsnSerializer_SetValueException = "Unable to set field {0} on type {1}.";

		public const string Cryptography_AsnSerializer_SpecificTagChoice = "Field '{0}' on type '{1}' has specified an implicit tag value via [ExpectedTag] for [Choice] type '{2}'. ExplicitTag must be true, or the [ExpectedTag] attribute removed.";

		public const string Cryptography_AsnSerializer_UnexpectedTypeForAttribute = "Field '{0}' of type '{1}' has an effective type of '{2}' when one of ({3}) was expected.";

		public const string Cryptography_AsnSerializer_UtcTimeTwoDigitYearMaxTooSmall = "Field '{0}' on type '{1}' has a [UtcTime] TwoDigitYearMax value ({2}) smaller than the minimum (99).";

		public const string Cryptography_AsnSerializer_UnhandledType = "Could not determine how to serialize or deserialize type '{0}'.";

		public const string Cryptography_AsnWriter_EncodeUnbalancedStack = "Encode cannot be called while a Sequence or SetOf is still open.";

		public const string Cryptography_AsnWriter_PopWrongTag = "Cannot pop the requested tag as it is not currently in progress.";

		public const string Cryptography_BadHashValue = "The hash value is not correct.";

		public const string Cryptography_BadSignature = "Invalid signature.";

		public const string Cryptography_Cms_CannotDetermineSignatureAlgorithm = "Could not determine signature algorithm for the signer certificate.";

		public const string Cryptography_Cms_IncompleteCertChain = "The certificate chain is incomplete, the self-signed root authority could not be determined.";

		public const string Cryptography_Cms_Invalid_Originator_Identifier_Choice = "Invalid originator identifier choice {0} found in decoded CMS.";

		public const string Cryptography_Cms_Invalid_Subject_Identifier_Type = "The subject identifier type {0} is not valid.";

		public const string Cryptography_Cms_InvalidMessageType = "Invalid cryptographic message type.";

		public const string Cryptography_Cms_InvalidSignerHashForSignatureAlg = "SignerInfo digest algorithm '{0}' is not valid for signature algorithm '{1}'.";

		public const string Cryptography_Cms_Key_Agree_Date_Not_Available = "The Date property is not available for none KID key agree recipient.";

		public const string Cryptography_Cms_MessageNotEncrypted = "The CMS message is not encrypted.";

		public const string Cryptography_Cms_MessageNotSigned = "The CMS message is not signed.";

		public const string Cryptography_Cms_MissingAuthenticatedAttribute = "The cryptographic message does not contain an expected authenticated attribute.";

		public const string Cryptography_Cms_NoCounterCounterSigner = "Only one level of counter-signatures are supported on this platform.";

		public const string Cryptography_Cms_NoRecipients = "The recipients collection is empty. You must specify at least one recipient. This platform does not implement the certificate picker UI.";

		public const string Cryptography_Cms_NoSignerCert = "No signer certificate was provided. This platform does not implement the certificate picker UI.";

		public const string Cryptography_Cms_NoSignerAtIndex = "The signed cryptographic message does not have a signer for the specified signer index.";

		public const string Cryptography_Cms_RecipientNotFound = "The enveloped-data message does not contain the specified recipient.";

		public const string Cryptography_Cms_RecipientType_NotSupported = "The recipient type '{0}' is not supported for encryption or decryption on this platform.";

		public const string Cryptography_Cms_Sign_Empty_Content = "Cannot create CMS signature for empty content.";

		public const string Cryptography_Cms_SignerNotFound = "Cannot find the original signer.";

		public const string Cryptography_Cms_Signing_RequiresPrivateKey = "A certificate with a private key is required.";

		public const string Cryptography_Cms_TrustFailure = "Certificate trust could not be established. The first reported error is: {0}";

		public const string Cryptography_Cms_UnknownAlgorithm = "Unknown algorithm '{0}'.";

		public const string Cryptography_Cms_UnknownKeySpec = "Unable to determine the type of key handle from this keyspec {0}.";

		public const string Cryptography_Cms_WrongKeyUsage = "The certificate is not valid for the requested usage.";

		public const string Cryptography_Pkcs_InvalidSignatureParameters = "Invalid signature paramters.";

		public const string Cryptography_Pkcs9_AttributeMismatch = "The parameter should be a PKCS 9 attribute.";

		public const string Cryptography_Pkcs9_MultipleSigningTimeNotAllowed = "Cannot add multiple PKCS 9 signing time attributes.";

		public const string Cryptography_Pkcs_PssParametersMissing = "PSS parameters were not present.";

		public const string Cryptography_Pkcs_PssParametersHashMismatch = "This platform requires that the PSS hash algorithm ({0}) match the data digest algorithm ({1}).";

		public const string Cryptography_Pkcs_PssParametersMgfHashMismatch = "This platform does not support the MGF hash algorithm ({0}) being different from the signature hash algorithm ({1}).";

		public const string Cryptography_Pkcs_PssParametersMgfNotSupported = "Mask generation function '{0}' is not supported by this platform.";

		public const string Cryptography_Pkcs_PssParametersSaltMismatch = "PSS salt size {0} is not supported by this platform with hash algorithm {1}.";

		public const string Cryptography_TimestampReq_BadNonce = "The response from the timestamping server did not match the request nonce.";

		public const string Cryptography_TimestampReq_BadResponse = "The response from the timestamping server was not understood.";

		public const string Cryptography_TimestampReq_Failure = "The timestamping server did not grant the request. The request status is '{0}' with failure info '{1}'.";

		public const string Cryptography_TimestampReq_NoCertFound = "The timestamping request required the TSA certificate in the response, but it was not found.";

		public const string Cryptography_TimestampReq_UnexpectedCertFound = "The timestamping request required the TSA certificate not be included in the response, but certificates were present.";

		public const string InvalidOperation_DuplicateItemNotAllowed = "Duplicate items are not allowed in the collection.";

		public const string InvalidOperation_WrongOidInAsnCollection = "AsnEncodedData element in the collection has wrong Oid value: expected = '{0}', actual = '{1}'.";

		public const string PlatformNotSupported_CryptographyPkcs = "System.Security.Cryptography.Pkcs is only supported on Windows platforms.";

		public const string Cryptography_Der_Invalid_Encoding = "ASN1 corrupted data.";

		public const string Cryptography_Invalid_IA5String = "The string contains a character not in the 7 bit ASCII character set.";

		public const string Cryptography_UnknownHashAlgorithm = "'{0}' is not a known hash algorithm.";

		public const string Cryptography_WriteEncodedValue_OneValueAtATime = "The input to WriteEncodedValue must represent a single encoded value with no trailing data.";
	}
}
namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Displays user interface dialogs that allow you to select and view X.509 certificates. This class cannot be inherited.</summary>
	public static class X509Certificate2UI
	{
		/// <summary>Displays a dialog box that contains the properties of an X.509 certificate and its associated certificate chain.</summary>
		/// <param name="certificate">The X.509 certificate to display.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificate" /> parameter is invalid.</exception>
		[System.MonoTODO]
		public static void DisplayCertificate(X509Certificate2 certificate)
		{
			DisplayCertificate(certificate, IntPtr.Zero);
		}

		/// <summary>Displays a dialog box that contains the properties of an X.509 certificate and its associated certificate chain using a handle to a parent window.</summary>
		/// <param name="certificate">The X.509 certificate to display.</param>
		/// <param name="hwndParent">A handle to the parent window to use for the display dialog.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificate" /> parameter is invalid.</exception>
		[System.MonoTODO]
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.SafeTopLevelWindows)]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public static void DisplayCertificate(X509Certificate2 certificate, IntPtr hwndParent)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			certificate.GetRawCertData();
			throw new NotImplementedException();
		}

		/// <summary>Displays a dialog box for selecting an X.509 certificate from a certificate collection.</summary>
		/// <param name="certificates">A collection of X.509 certificates to select from.</param>
		/// <param name="title">The title of the dialog box.</param>
		/// <param name="message">A descriptive message to guide the user.  The message is displayed in the dialog box.</param>
		/// <param name="selectionFlag">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SelectionFlag" /> values that specifies whether single or multiple selections are allowed.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object that contains the selected certificate or certificates.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="selectionFlag" /> parameter is not a valid flag.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificates" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificates" /> parameter is invalid.</exception>
		[System.MonoTODO]
		public static X509Certificate2Collection SelectFromCollection(X509Certificate2Collection certificates, string title, string message, X509SelectionFlag selectionFlag)
		{
			return SelectFromCollection(certificates, title, message, selectionFlag, IntPtr.Zero);
		}

		/// <summary>Displays a dialog box for selecting an X.509 certificate from a certificate collection using a handle to a parent window.</summary>
		/// <param name="certificates">A collection of X.509 certificates to select from.</param>
		/// <param name="title">The title of the dialog box.</param>
		/// <param name="message">A descriptive message to guide the user.  The message is displayed in the dialog box.</param>
		/// <param name="selectionFlag">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SelectionFlag" /> values that specifies whether single or multiple selections are allowed.</param>
		/// <param name="hwndParent">A handle to the parent window to use for the display dialog box.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object that contains the selected certificate or certificates.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="selectionFlag" /> parameter is not a valid flag.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificates" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificates" /> parameter is invalid.</exception>
		[System.MonoTODO]
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.SafeTopLevelWindows)]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public static X509Certificate2Collection SelectFromCollection(X509Certificate2Collection certificates, string title, string message, X509SelectionFlag selectionFlag, IntPtr hwndParent)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			if (selectionFlag < X509SelectionFlag.SingleSelection || selectionFlag > X509SelectionFlag.MultiSelection)
			{
				throw new ArgumentException("selectionFlag");
			}
			throw new NotImplementedException();
		}
	}
	/// <summary>Specifies the type of selection requested using the <see cref="Overload:System.Security.Cryptography.X509Certificates.X509Certificate2UI.SelectFromCollection" /> method.</summary>
	public enum X509SelectionFlag
	{
		/// <summary>A single selection. The UI allows the user to select one X.509 certificate.</summary>
		SingleSelection,
		/// <summary>A multiple selection. The user can use the SHIFT or CRTL keys to select more than one X.509 certificate.</summary>
		MultiSelection
	}
}
namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;<see langword="X509IssuerSerial" />&gt; element of an XML digital signature.</summary>
	public struct X509IssuerSerial
	{
		/// <summary>Gets or sets an X.509 certificate issuer's distinguished name.</summary>
		/// <returns>An X.509 certificate issuer's distinguished name.</returns>
		public string IssuerName { get; set; }

		/// <summary>Gets or sets an X.509 certificate issuer's serial number.</summary>
		/// <returns>An X.509 certificate issuer's serial number.</returns>
		public string SerialNumber { get; set; }

		internal X509IssuerSerial(string issuerName, string serialNumber)
		{
			this = default(X509IssuerSerial);
			IssuerName = issuerName;
			SerialNumber = serialNumber;
		}
	}
	internal abstract class AncestralNamespaceContextManager
	{
		internal ArrayList _ancestorStack = new ArrayList();

		internal NamespaceFrame GetScopeAt(int i)
		{
			return (NamespaceFrame)_ancestorStack[i];
		}

		internal NamespaceFrame GetCurrentScope()
		{
			return GetScopeAt(_ancestorStack.Count - 1);
		}

		protected XmlAttribute GetNearestRenderedNamespaceWithMatchingPrefix(string nsPrefix, out int depth)
		{
			XmlAttribute xmlAttribute = null;
			depth = -1;
			for (int num = _ancestorStack.Count - 1; num >= 0; num--)
			{
				if ((xmlAttribute = GetScopeAt(num).GetRendered(nsPrefix)) != null)
				{
					depth = num;
					return xmlAttribute;
				}
			}
			return null;
		}

		protected XmlAttribute GetNearestUnrenderedNamespaceWithMatchingPrefix(string nsPrefix, out int depth)
		{
			XmlAttribute xmlAttribute = null;
			depth = -1;
			for (int num = _ancestorStack.Count - 1; num >= 0; num--)
			{
				if ((xmlAttribute = GetScopeAt(num).GetUnrendered(nsPrefix)) != null)
				{
					depth = num;
					return xmlAttribute;
				}
			}
			return null;
		}

		internal void EnterElementContext()
		{
			_ancestorStack.Add(new NamespaceFrame());
		}

		internal void ExitElementContext()
		{
			_ancestorStack.RemoveAt(_ancestorStack.Count - 1);
		}

		internal abstract void TrackNamespaceNode(XmlAttribute attr, SortedList nsListToRender, Hashtable nsLocallyDeclared);

		internal abstract void TrackXmlNamespaceNode(XmlAttribute attr, SortedList nsListToRender, SortedList attrListToRender, Hashtable nsLocallyDeclared);

		internal abstract void GetNamespacesToRender(XmlElement element, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared);

		internal void LoadUnrenderedNamespaces(Hashtable nsLocallyDeclared)
		{
			object[] array = new object[nsLocallyDeclared.Count];
			nsLocallyDeclared.Values.CopyTo(array, 0);
			object[] array2 = array;
			foreach (object obj in array2)
			{
				AddUnrendered((XmlAttribute)obj);
			}
		}

		internal void LoadRenderedNamespaces(SortedList nsRenderedList)
		{
			foreach (object key in nsRenderedList.GetKeyList())
			{
				AddRendered((XmlAttribute)key);
			}
		}

		internal void AddRendered(XmlAttribute attr)
		{
			GetCurrentScope().AddRendered(attr);
		}

		internal void AddUnrendered(XmlAttribute attr)
		{
			GetCurrentScope().AddUnrendered(attr);
		}
	}
	internal class AttributeSortOrder : IComparer
	{
		internal AttributeSortOrder()
		{
		}

		public int Compare(object a, object b)
		{
			XmlNode xmlNode = a as XmlNode;
			XmlNode xmlNode2 = b as XmlNode;
			if (xmlNode == null || xmlNode2 == null)
			{
				throw new ArgumentException();
			}
			int num = string.CompareOrdinal(xmlNode.NamespaceURI, xmlNode2.NamespaceURI);
			if (num != 0)
			{
				return num;
			}
			return string.CompareOrdinal(xmlNode.LocalName, xmlNode2.LocalName);
		}
	}
	internal class C14NAncestralNamespaceContextManager : AncestralNamespaceContextManager
	{
		internal C14NAncestralNamespaceContextManager()
		{
		}

		private void GetNamespaceToRender(string nsPrefix, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			foreach (XmlAttribute key in nsListToRender.GetKeyList())
			{
				if (Utils.HasNamespacePrefix(key, nsPrefix))
				{
					return;
				}
			}
			foreach (XmlAttribute key2 in attrListToRender.GetKeyList())
			{
				if (key2.LocalName.Equals(nsPrefix))
				{
					return;
				}
			}
			XmlAttribute xmlAttribute = (XmlAttribute)nsLocallyDeclared[nsPrefix];
			int depth;
			XmlAttribute nearestRenderedNamespaceWithMatchingPrefix = GetNearestRenderedNamespaceWithMatchingPrefix(nsPrefix, out depth);
			if (xmlAttribute != null)
			{
				if (Utils.IsNonRedundantNamespaceDecl(xmlAttribute, nearestRenderedNamespaceWithMatchingPrefix))
				{
					nsLocallyDeclared.Remove(nsPrefix);
					if (Utils.IsXmlNamespaceNode(xmlAttribute))
					{
						attrListToRender.Add(xmlAttribute, null);
					}
					else
					{
						nsListToRender.Add(xmlAttribute, null);
					}
				}
				return;
			}
			int depth2;
			XmlAttribute nearestUnrenderedNamespaceWithMatchingPrefix = GetNearestUnrenderedNamespaceWithMatchingPrefix(nsPrefix, out depth2);
			if (nearestUnrenderedNamespaceWithMatchingPrefix != null && depth2 > depth && Utils.IsNonRedundantNamespaceDecl(nearestUnrenderedNamespaceWithMatchingPrefix, nearestRenderedNamespaceWithMatchingPrefix))
			{
				if (Utils.IsXmlNamespaceNode(nearestUnrenderedNamespaceWithMatchingPrefix))
				{
					attrListToRender.Add(nearestUnrenderedNamespaceWithMatchingPrefix, null);
				}
				else
				{
					nsListToRender.Add(nearestUnrenderedNamespaceWithMatchingPrefix, null);
				}
			}
		}

		internal override void GetNamespacesToRender(XmlElement element, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			XmlAttribute xmlAttribute = null;
			object[] array = new object[nsLocallyDeclared.Count];
			nsLocallyDeclared.Values.CopyTo(array, 0);
			object[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				xmlAttribute = (XmlAttribute)array2[i];
				int depth;
				XmlAttribute nearestRenderedNamespaceWithMatchingPrefix = GetNearestRenderedNamespaceWithMatchingPrefix(Utils.GetNamespacePrefix(xmlAttribute), out depth);
				if (Utils.IsNonRedundantNamespaceDecl(xmlAttribute, nearestRenderedNamespaceWithMatchingPrefix))
				{
					nsLocallyDeclared.Remove(Utils.GetNamespacePrefix(xmlAttribute));
					if (Utils.IsXmlNamespaceNode(xmlAttribute))
					{
						attrListToRender.Add(xmlAttribute, null);
					}
					else
					{
						nsListToRender.Add(xmlAttribute, null);
					}
				}
			}
			for (int num = _ancestorStack.Count - 1; num >= 0; num--)
			{
				foreach (XmlAttribute value in GetScopeAt(num).GetUnrendered().Values)
				{
					if (value != null)
					{
						GetNamespaceToRender(Utils.GetNamespacePrefix(value), attrListToRender, nsListToRender, nsLocallyDeclared);
					}
				}
			}
		}

		internal override void TrackNamespaceNode(XmlAttribute attr, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			nsLocallyDeclared.Add(Utils.GetNamespacePrefix(attr), attr);
		}

		internal override void TrackXmlNamespaceNode(XmlAttribute attr, SortedList nsListToRender, SortedList attrListToRender, Hashtable nsLocallyDeclared)
		{
			nsLocallyDeclared.Add(Utils.GetNamespacePrefix(attr), attr);
		}
	}
	internal class CanonicalXml
	{
		private CanonicalXmlDocument _c14nDoc;

		private C14NAncestralNamespaceContextManager _ancMgr;

		internal CanonicalXml(Stream inputStream, bool includeComments, XmlResolver resolver, string strBaseUri)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			_c14nDoc = new CanonicalXmlDocument(defaultNodeSetInclusionState: true, includeComments);
			_c14nDoc.XmlResolver = resolver;
			_c14nDoc.Load(Utils.PreProcessStreamInput(inputStream, resolver, strBaseUri));
			_ancMgr = new C14NAncestralNamespaceContextManager();
		}

		internal CanonicalXml(XmlDocument document, XmlResolver resolver)
			: this(document, resolver, includeComments: false)
		{
		}

		internal CanonicalXml(XmlDocument document, XmlResolver resolver, bool includeComments)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			_c14nDoc = new CanonicalXmlDocument(defaultNodeSetInclusionState: true, includeComments);
			_c14nDoc.XmlResolver = resolver;
			_c14nDoc.Load(new XmlNodeReader(document));
			_ancMgr = new C14NAncestralNamespaceContextManager();
		}

		internal CanonicalXml(XmlNodeList nodeList, XmlResolver resolver, bool includeComments)
		{
			if (nodeList == null)
			{
				throw new ArgumentNullException("nodeList");
			}
			XmlDocument ownerDocument = Utils.GetOwnerDocument(nodeList);
			if (ownerDocument == null)
			{
				throw new ArgumentException("nodeList");
			}
			_c14nDoc = new CanonicalXmlDocument(defaultNodeSetInclusionState: false, includeComments);
			_c14nDoc.XmlResolver = resolver;
			_c14nDoc.Load(new XmlNodeReader(ownerDocument));
			_ancMgr = new C14NAncestralNamespaceContextManager();
			MarkInclusionStateForNodes(nodeList, ownerDocument, _c14nDoc);
		}

		private static void MarkNodeAsIncluded(XmlNode node)
		{
			if (node is ICanonicalizableNode)
			{
				((ICanonicalizableNode)node).IsInNodeSet = true;
			}
		}

		private static void MarkInclusionStateForNodes(XmlNodeList nodeList, XmlDocument inputRoot, XmlDocument root)
		{
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			CanonicalXmlNodeList canonicalXmlNodeList2 = new CanonicalXmlNodeList();
			canonicalXmlNodeList.Add(inputRoot);
			canonicalXmlNodeList2.Add(root);
			int num = 0;
			do
			{
				XmlNode xmlNode = canonicalXmlNodeList[num];
				XmlNode xmlNode2 = canonicalXmlNodeList2[num];
				XmlNodeList childNodes = xmlNode.ChildNodes;
				XmlNodeList childNodes2 = xmlNode2.ChildNodes;
				for (int i = 0; i < childNodes.Count; i++)
				{
					canonicalXmlNodeList.Add(childNodes[i]);
					canonicalXmlNodeList2.Add(childNodes2[i]);
					if (Utils.NodeInList(childNodes[i], nodeList))
					{
						MarkNodeAsIncluded(childNodes2[i]);
					}
					XmlAttributeCollection attributes = childNodes[i].Attributes;
					if (attributes == null)
					{
						continue;
					}
					for (int j = 0; j < attributes.Count; j++)
					{
						if (Utils.NodeInList(attributes[j], nodeList))
						{
							MarkNodeAsIncluded(childNodes2[i].Attributes.Item(j));
						}
					}
				}
				num++;
			}
			while (num < canonicalXmlNodeList.Count);
		}

		internal byte[] GetBytes()
		{
			StringBuilder stringBuilder = new StringBuilder();
			_c14nDoc.Write(stringBuilder, DocPosition.BeforeRootElement, _ancMgr);
			return new UTF8Encoding(encoderShouldEmitUTF8Identifier: false).GetBytes(stringBuilder.ToString());
		}

		internal byte[] GetDigestedBytes(HashAlgorithm hash)
		{
			_c14nDoc.WriteHash(hash, DocPosition.BeforeRootElement, _ancMgr);
			hash.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
			byte[] result = (byte[])hash.Hash.Clone();
			hash.Initialize();
			return result;
		}
	}
	internal class CanonicalXmlAttribute : XmlAttribute, ICanonicalizableNode
	{
		private bool _isInNodeSet;

		public bool IsInNodeSet
		{
			get
			{
				return _isInNodeSet;
			}
			set
			{
				_isInNodeSet = value;
			}
		}

		public CanonicalXmlAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(prefix, localName, namespaceURI, doc)
		{
			IsInNodeSet = defaultNodeSetInclusionState;
		}

		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			strBuilder.Append(" " + Name + "=\"");
			strBuilder.Append(Utils.EscapeAttributeValue(Value));
			strBuilder.Append("\"");
		}

		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			UTF8Encoding uTF8Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
			byte[] bytes = uTF8Encoding.GetBytes(" " + Name + "=\"");
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			bytes = uTF8Encoding.GetBytes(Utils.EscapeAttributeValue(Value));
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			bytes = uTF8Encoding.GetBytes("\"");
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
		}
	}
	internal class CanonicalXmlCDataSection : XmlCDataSection, ICanonicalizableNode
	{
		private bool _isInNodeSet;

		public bool IsInNodeSet
		{
			get
			{
				return _isInNodeSet;
			}
			set
			{
				_isInNodeSet = value;
			}
		}

		public CanonicalXmlCDataSection(string data, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(data, doc)
		{
			_isInNodeSet = defaultNodeSetInclusionState;
		}

		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (IsInNodeSet)
			{
				strBuilder.Append(Utils.EscapeCData(Data));
			}
		}

		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (IsInNodeSet)
			{
				byte[] bytes = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false).GetBytes(Utils.EscapeCData(Data));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}
	}
	internal class CanonicalXmlComment : XmlComment, ICanonicalizableNode
	{
		private bool _isInNodeSet;

		private bool _includeComments;

		public bool IsInNodeSet
		{
			get
			{
				return _isInNodeSet;
			}
			set
			{
				_isInNodeSet = value;
			}
		}

		public bool IncludeComments => _includeComments;

		public CanonicalXmlComment(string comment, XmlDocument doc, bool defaultNodeSetInclusionState, bool includeComments)
			: base(comment, doc)
		{
			_isInNodeSet = defaultNodeSetInclusionState;
			_includeComments = includeComments;
		}

		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (IsInNodeSet && IncludeComments)
			{
				if (docPos == DocPosition.AfterRootElement)
				{
					strBuilder.Append('\n');
				}
				strBuilder.Append("<!--");
				strBuilder.Append(Value);
				strBuilder.Append("-->");
				if (docPos == DocPosition.BeforeRootElement)
				{
					strBuilder.Append('\n');
				}
			}
		}

		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (IsInNodeSet && IncludeComments)
			{
				UTF8Encoding uTF8Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
				byte[] bytes = uTF8Encoding.GetBytes("(char) 10");
				if (docPos == DocPosition.AfterRootElement)
				{
					hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				}
				bytes = uTF8Encoding.GetBytes("<!--");
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				bytes = uTF8Encoding.GetBytes(Value);
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				bytes = uTF8Encoding.GetBytes("-->");
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				if (docPos == DocPosition.BeforeRootElement)
				{
					bytes = uTF8Encoding.GetBytes("(char) 10");
					hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				}
			}
		}
	}
	internal class CanonicalXmlDocument : XmlDocument, ICanonicalizableNode
	{
		private bool _defaultNodeSetInclusionState;

		private bool _includeComments;

		private bool _isInNodeSet;

		public bool IsInNodeSet
		{
			get
			{
				return _isInNodeSet;
			}
			set
			{
				_isInNodeSet = value;
			}
		}

		public CanonicalXmlDocument(bool defaultNodeSetInclusionState, bool includeComments)
		{
			base.PreserveWhitespace = true;
			_includeComments = includeComments;
			_isInNodeSet = (_defaultNodeSetInclusionState = defaultNodeSetInclusionState);
		}

		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			docPos = DocPosition.BeforeRootElement;
			foreach (XmlNode childNode in ChildNodes)
			{
				if (childNode.NodeType == XmlNodeType.Element)
				{
					CanonicalizationDispatcher.Write(childNode, strBuilder, DocPosition.InRootElement, anc);
					docPos = DocPosition.AfterRootElement;
				}
				else
				{
					CanonicalizationDispatcher.Write(childNode, strBuilder, docPos, anc);
				}
			}
		}

		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			docPos = DocPosition.BeforeRootElement;
			foreach (XmlNode childNode in ChildNodes)
			{
				if (childNode.NodeType == XmlNodeType.Element)
				{
					CanonicalizationDispatcher.WriteHash(childNode, hash, DocPosition.InRootElement, anc);
					docPos = DocPosition.AfterRootElement;
				}
				else
				{
					CanonicalizationDispatcher.WriteHash(childNode, hash, docPos, anc);
				}
			}
		}

		public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlElement(prefix, localName, namespaceURI, this, _defaultNodeSetInclusionState);
		}

		public override XmlAttribute CreateAttribute(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlAttribute(prefix, localName, namespaceURI, this, _defaultNodeSetInclusionState);
		}

		protected override XmlAttribute CreateDefaultAttribute(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlAttribute(prefix, localName, namespaceURI, this, _defaultNodeSetInclusionState);
		}

		public override XmlText CreateTextNode(string text)
		{
			return new CanonicalXmlText(text, this, _defaultNodeSetInclusionState);
		}

		public override XmlWhitespace CreateWhitespace(string prefix)
		{
			return new CanonicalXmlWhitespace(prefix, this, _defaultNodeSetInclusionState);
		}

		public override XmlSignificantWhitespace CreateSignificantWhitespace(string text)
		{
			return new CanonicalXmlSignificantWhitespace(text, this, _defaultNodeSetInclusionState);
		}

		public override XmlProcessingInstruction CreateProcessingInstruction(string target, string data)
		{
			return new CanonicalXmlProcessingInstruction(target, data, this, _defaultNodeSetInclusionState);
		}

		public override XmlComment CreateComment(string data)
		{
			return new CanonicalXmlComment(data, this, _defaultNodeSetInclusionState, _includeComments);
		}

		public override XmlEntityReference CreateEntityReference(string name)
		{
			return new CanonicalXmlEntityReference(name, this, _defaultNodeSetInclusionState);
		}

		public override XmlCDataSection CreateCDataSection(string data)
		{
			return new CanonicalXmlCDataSection(data, this, _defaultNodeSetInclusionState);
		}
	}
	internal class CanonicalXmlElement : XmlElement, ICanonicalizableNode
	{
		private bool _isInNodeSet;

		public bool IsInNodeSet
		{
			get
			{
				return _isInNodeSet;
			}
			set
			{
				_isInNodeSet = value;
			}
		}

		public CanonicalXmlElement(string prefix, string localName, string namespaceURI, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(prefix, localName, namespaceURI, doc)
		{
			_isInNodeSet = defaultNodeSetInclusionState;
		}

		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			Hashtable nsLocallyDeclared = new Hashtable();
			SortedList sortedList = new SortedList(new NamespaceSortOrder());
			SortedList sortedList2 = new SortedList(new AttributeSortOrder());
			XmlAttributeCollection xmlAttributeCollection = Attributes;
			if (xmlAttributeCollection != null)
			{
				foreach (XmlAttribute item in xmlAttributeCollection)
				{
					if (((CanonicalXmlAttribute)item).IsInNodeSet || Utils.IsNamespaceNode(item) || Utils.IsXmlNamespaceNode(item))
					{
						if (Utils.IsNamespaceNode(item))
						{
							anc.TrackNamespaceNode(item, sortedList, nsLocallyDeclared);
						}
						else if (Utils.IsXmlNamespaceNode(item))
						{
							anc.TrackXmlNamespaceNode(item, sortedList, sortedList2, nsLocallyDeclared);
						}
						else if (IsInNodeSet)
						{
							sortedList2.Add(item, null);
						}
					}
				}
			}
			if (!Utils.IsCommittedNamespace(this, Prefix, NamespaceURI))
			{
				string text = ((Prefix.Length > 0) ? ("xmlns:" + Prefix) : "xmlns");
				XmlAttribute xmlAttribute2 = OwnerDocument.CreateAttribute(text);
				xmlAttribute2.Value = NamespaceURI;
				anc.TrackNamespaceNode(xmlAttribute2, sortedList, nsLocallyDeclared);
			}
			if (IsInNodeSet)
			{
				anc.GetNamespacesToRender(this, sortedList2, sortedList, nsLocallyDeclared);
				strBuilder.Append("<" + Name);
				foreach (object key in sortedList.GetKeyList())
				{
					(key as CanonicalXmlAttribute).Write(strBuilder, docPos, anc);
				}
				foreach (object key2 in sortedList2.GetKeyList())
				{
					(key2 as CanonicalXmlAttribute).Write(strBuilder, docPos, anc);
				}
				strBuilder.Append(">");
			}
			anc.EnterElementContext();
			anc.LoadUnrenderedNamespaces(nsLocallyDeclared);
			anc.LoadRenderedNamespaces(sortedList);
			foreach (XmlNode childNode in ChildNodes)
			{
				CanonicalizationDispatcher.Write(childNode, strBuilder, docPos, anc);
			}
			anc.ExitElementContext();
			if (IsInNodeSet)
			{
				strBuilder.Append("</" + Name + ">");
			}
		}

		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			Hashtable nsLocallyDeclared = new Hashtable();
			SortedList sortedList = new SortedList(new NamespaceSortOrder());
			SortedList sortedList2 = new SortedList(new AttributeSortOrder());
			UTF8Encoding uTF8Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
			XmlAttributeCollection xmlAttributeCollection = Attributes;
			if (xmlAttributeCollection != null)
			{
				foreach (XmlAttribute item in xmlAttributeCollection)
				{
					if (((CanonicalXmlAttribute)item).IsInNodeSet || Utils.IsNamespaceNode(item) || Utils.IsXmlNamespaceNode(item))
					{
						if (Utils.IsNamespaceNode(item))
						{
							anc.TrackNamespaceNode(item, sortedList, nsLocallyDeclared);
						}
						else if (Utils.IsXmlNamespaceNode(item))
						{
							anc.TrackXmlNamespaceNode(item, sortedList, sortedList2, nsLocallyDeclared);
						}
						else if (IsInNodeSet)
						{
							sortedList2.Add(item, null);
						}
					}
				}
			}
			if (!Utils.IsCommittedNamespace(this, Prefix, NamespaceURI))
			{
				string text = ((Prefix.Length > 0) ? ("xmlns:" + Prefix) : "xmlns");
				XmlAttribute xmlAttribute2 = OwnerDocument.CreateAttribute(text);
				xmlAttribute2.Value = NamespaceURI;
				anc.TrackNamespaceNode(xmlAttribute2, sortedList, nsLocallyDeclared);
			}
			if (IsInNodeSet)
			{
				anc.GetNamespacesToRender(this, sortedList2, sortedList, nsLocallyDeclared);
				byte[] bytes = uTF8Encoding.GetBytes("<" + Name);
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				foreach (object key in sortedList.GetKeyList())
				{
					(key as CanonicalXmlAttribute).WriteHash(hash, docPos, anc);
				}
				foreach (object key2 in sortedList2.GetKeyList())
				{
					(key2 as CanonicalXmlAttribute).WriteHash(hash, docPos, anc);
				}
				bytes = uTF8Encoding.GetBytes(">");
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
			anc.EnterElementContext();
			anc.LoadUnrenderedNamespaces(nsLocallyDeclared);
			anc.LoadRenderedNamespaces(sortedList);
			foreach (XmlNode childNode in ChildNodes)
			{
				CanonicalizationDispatcher.WriteHash(childNode, hash, docPos, anc);
			}
			anc.ExitElementContext();
			if (IsInNodeSet)
			{
				byte[] bytes = uTF8Encoding.GetBytes("</" + Name + ">");
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}
	}
	internal class CanonicalXmlEntityReference : XmlEntityReference, ICanonicalizableNode
	{
		private bool _isInNodeSet;

		public bool IsInNodeSet
		{
			get
			{
				return _isInNodeSet;
			}
			set
			{
				_isInNodeSet = value;
			}
		}

		public CanonicalXmlEntityReference(string name, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(name, doc)
		{
			_isInNodeSet = defaultNodeSetInclusionState;
		}

		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (IsInNodeSet)
			{
				CanonicalizationDispatcher.WriteGenericNode(this, strBuilder, docPos, anc);
			}
		}

		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (IsInNodeSet)
			{
				CanonicalizationDispatcher.WriteHashGenericNode(this, hash, docPos, anc);
			}
		}
	}
	internal class CanonicalXmlNodeList : XmlNodeList, IList, ICollection, IEnumerable
	{
		private ArrayList _nodeArray;

		public override int Count => _nodeArray.Count;

		public bool IsFixedSize => _nodeArray.IsFixedSize;

		public bool IsReadOnly => _nodeArray.IsReadOnly;

		object IList.this[int index]
		{
			get
			{
				return _nodeArray[index];
			}
			set
			{
				if (!(value is XmlNode))
				{
					throw new ArgumentException("Type of input object is invalid.", "value");
				}
				_nodeArray[index] = value;
			}
		}

		public object SyncRoot => _nodeArray.SyncRoot;

		public bool IsSynchronized => _nodeArray.IsSynchronized;

		internal CanonicalXmlNodeList()
		{
			_nodeArray = new ArrayList();
		}

		public override XmlNode Item(int index)
		{
			return (XmlNode)_nodeArray[index];
		}

		public override IEnumerator GetEnumerator()
		{
			return _nodeArray.GetEnumerator();
		}

		public int Add(object value)
		{
			if (!(value is XmlNode))
			{
				throw new ArgumentException("Type of input object is invalid.", "node");
			}
			return _nodeArray.Add(value);
		}

		public void Clear()
		{
			_nodeArray.Clear();
		}

		public bool Contains(object value)
		{
			return _nodeArray.Contains(value);
		}

		public int IndexOf(object value)
		{
			return _nodeArray.IndexOf(value);
		}

		public void Insert(int index, object value)
		{
			if (!(value is XmlNode))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			_nodeArray.Insert(index, value);
		}

		public void Remove(object value)
		{
			_nodeArray.Remove(value);
		}

		public void RemoveAt(int index)
		{
			_nodeArray.RemoveAt(index);
		}

		public void CopyTo(Array array, int index)
		{
			_nodeArray.CopyTo(array, index);
		}
	}
	internal class CanonicalXmlProcessingInstruction : XmlProcessingInstruction, ICanonicalizableNode
	{
		private bool _isInNodeSet;

		public bool IsInNodeSet
		{
			get
			{
				return _isInNodeSet;
			}
			set
			{
				_isInNodeSet = value;
			}
		}

		public CanonicalXmlProcessingInstruction(string target, string data, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(target, data, doc)
		{
			_isInNodeSet = defaultNodeSetInclusionState;
		}

		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (IsInNodeSet)
			{
				if (docPos == DocPosition.AfterRootElement)
				{
					strBuilder.Append('\n');
				}
				strBuilder.Append("<?");
				strBuilder.Append(Name);
				if (Value != null && Value.Length > 0)
				{
					strBuilder.Append(" " + Value);
				}
				strBuilder.Append("?>");
				if (docPos == DocPosition.BeforeRootElement)
				{
					strBuilder.Append('\n');
				}
			}
		}

		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (IsInNodeSet)
			{
				UTF8Encoding uTF8Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
				byte[] bytes;
				if (docPos == DocPosition.AfterRootElement)
				{
					bytes = uTF8Encoding.GetBytes("(char) 10");
					hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				}
				bytes = uTF8Encoding.GetBytes("<?");
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				bytes = uTF8Encoding.GetBytes(Name);
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				if (Value != null && Value.Length > 0)
				{
					bytes = uTF8Encoding.GetBytes(" " + Value);
					hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				}
				bytes = uTF8Encoding.GetBytes("?>");
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				if (docPos == DocPosition.BeforeRootElement)
				{
					bytes = uTF8Encoding.GetBytes("(char) 10");
					hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				}
			}
		}
	}
	internal class CanonicalXmlSignificantWhitespace : XmlSignificantWhitespace, ICanonicalizableNode
	{
		private bool _isInNodeSet;

		public bool IsInNodeSet
		{
			get
			{
				return _isInNodeSet;
			}
			set
			{
				_isInNodeSet = value;
			}
		}

		public CanonicalXmlSignificantWhitespace(string strData, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(strData, doc)
		{
			_isInNodeSet = defaultNodeSetInclusionState;
		}

		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				strBuilder.Append(Utils.EscapeWhitespaceData(Value));
			}
		}

		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				byte[] bytes = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false).GetBytes(Utils.EscapeWhitespaceData(Value));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}
	}
	internal class CanonicalXmlText : XmlText, ICanonicalizableNode
	{
		private bool _isInNodeSet;

		public bool IsInNodeSet
		{
			get
			{
				return _isInNodeSet;
			}
			set
			{
				_isInNodeSet = value;
			}
		}

		public CanonicalXmlText(string strData, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(strData, doc)
		{
			_isInNodeSet = defaultNodeSetInclusionState;
		}

		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (IsInNodeSet)
			{
				strBuilder.Append(Utils.EscapeTextData(Value));
			}
		}

		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (IsInNodeSet)
			{
				byte[] bytes = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false).GetBytes(Utils.EscapeTextData(Value));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}
	}
	internal class CanonicalXmlWhitespace : XmlWhitespace, ICanonicalizableNode
	{
		private bool _isInNodeSet;

		public bool IsInNodeSet
		{
			get
			{
				return _isInNodeSet;
			}
			set
			{
				_isInNodeSet = value;
			}
		}

		public CanonicalXmlWhitespace(string strData, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(strData, doc)
		{
			_isInNodeSet = defaultNodeSetInclusionState;
		}

		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				strBuilder.Append(Utils.EscapeWhitespaceData(Value));
			}
		}

		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				byte[] bytes = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false).GetBytes(Utils.EscapeWhitespaceData(Value));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}
	}
	internal class CanonicalizationDispatcher
	{
		private CanonicalizationDispatcher()
		{
		}

		public static void Write(XmlNode node, StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (node is ICanonicalizableNode)
			{
				((ICanonicalizableNode)node).Write(strBuilder, docPos, anc);
			}
			else
			{
				WriteGenericNode(node, strBuilder, docPos, anc);
			}
		}

		public static void WriteGenericNode(XmlNode node, StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				Write(childNode, strBuilder, docPos, anc);
			}
		}

		public static void WriteHash(XmlNode node, HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (node is ICanonicalizableNode)
			{
				((ICanonicalizableNode)node).WriteHash(hash, docPos, anc);
			}
			else
			{
				WriteHashGenericNode(node, hash, docPos, anc);
			}
		}

		public static void WriteHashGenericNode(XmlNode node, HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				WriteHash(childNode, hash, docPos, anc);
			}
		}
	}
	internal enum CertUsageType
	{
		Verification,
		Decryption
	}
	/// <summary>Represents the <see langword="&lt;CipherData&gt;" /> element in XML encryption. This class cannot be inherited.</summary>
	public sealed class CipherData
	{
		private XmlElement _cachedXml;

		private CipherReference _cipherReference;

		private byte[] _cipherValue;

		private bool CacheValid => _cachedXml != null;

		/// <summary>Gets or sets the <see langword="&lt;CipherReference&gt;" /> element.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" /> property was set to <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" /> property was set more than once.</exception>
		public CipherReference CipherReference
		{
			get
			{
				return _cipherReference;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (CipherValue != null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				_cipherReference = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the <see langword="&lt;CipherValue&gt;" /> element.</summary>
		/// <returns>A byte array that represents the <see langword="&lt;CipherValue&gt;" /> element.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property was set to <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property was set more than once.</exception>
		public byte[] CipherValue
		{
			get
			{
				return _cipherValue;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (CipherReference != null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				_cipherValue = (byte[])value.Clone();
				_cachedXml = null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> class.</summary>
		public CipherData()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> class using a byte array as the <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> value.</summary>
		/// <param name="cipherValue">The encrypted data to use for the <see langword="&lt;CipherValue&gt;" /> element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cipherValue" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property has already been set.</exception>
		public CipherData(byte[] cipherValue)
		{
			CipherValue = cipherValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> class using a <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> object.</summary>
		/// <param name="cipherReference">The <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> object to use.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cipherValue" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property has already been set.</exception>
		public CipherData(CipherReference cipherReference)
		{
			CipherReference = cipherReference;
		}

		/// <summary>Gets the XML values for the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</summary>
		/// <returns>A <see cref="T:System.Xml.XmlElement" /> object that represents the XML information for the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property and the <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" /> property are <see langword="null" />.</exception>
		public XmlElement GetXml()
		{
			if (CacheValid)
			{
				return _cachedXml;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("CipherData", "http://www.w3.org/2001/04/xmlenc#");
			if (CipherValue != null)
			{
				XmlElement xmlElement2 = document.CreateElement("CipherValue", "http://www.w3.org/2001/04/xmlenc#");
				xmlElement2.AppendChild(document.CreateTextNode(Convert.ToBase64String(CipherValue)));
				xmlElement.AppendChild(xmlElement2);
			}
			else
			{
				if (CipherReference == null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				xmlElement.AppendChild(CipherReference.GetXml(document));
			}
			return xmlElement;
		}

		/// <summary>Loads XML data from an <see cref="T:System.Xml.XmlElement" /> into a <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> that represents the XML data to load.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property and the <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" /> property are <see langword="null" />.</exception>
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			XmlNode xmlNode = value.SelectSingleNode("enc:CipherValue", xmlNamespaceManager);
			XmlNode xmlNode2 = value.SelectSingleNode("enc:CipherReference", xmlNamespaceManager);
			if (xmlNode != null)
			{
				if (xmlNode2 != null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				_cipherValue = Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlNode.InnerText));
			}
			else
			{
				if (xmlNode2 == null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				_cipherReference = new CipherReference();
				_cipherReference.LoadXml((XmlElement)xmlNode2);
			}
			_cachedXml = value;
		}
	}
	/// <summary>Represents the <see langword="&lt;CipherReference&gt;" /> element in XML encryption. This class cannot be inherited.</summary>
	public sealed class CipherReference : EncryptedReference
	{
		private byte[] _cipherValue;

		internal byte[] CipherValue
		{
			get
			{
				if (!base.CacheValid)
				{
					return null;
				}
				return _cipherValue;
			}
			set
			{
				_cipherValue = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> class.</summary>
		public CipherReference()
		{
			base.ReferenceType = "CipherReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> class using the specified Uniform Resource Identifier (URI).</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) pointing to the encrypted data.</param>
		public CipherReference(string uri)
			: base(uri)
		{
			base.ReferenceType = "CipherReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> class using the specified Uniform Resource Identifier (URI) and transform chain information.</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) pointing to the encrypted data.</param>
		/// <param name="transformChain">A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms to do on the encrypted data.</param>
		public CipherReference(string uri, TransformChain transformChain)
			: base(uri, transformChain)
		{
			base.ReferenceType = "CipherReference";
		}

		/// <summary>Returns the XML representation of a <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> that represents the <see langword="&lt;CipherReference&gt;" /> element in XML encryption.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> value is <see langword="null" />.</exception>
		public override XmlElement GetXml()
		{
			if (base.CacheValid)
			{
				return _cachedXml;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal new XmlElement GetXml(XmlDocument document)
		{
			if (base.ReferenceType == null)
			{
				throw new CryptographicException("The Reference type must be set in an EncryptedReference object.");
			}
			XmlElement xmlElement = document.CreateElement(base.ReferenceType, "http://www.w3.org/2001/04/xmlenc#");
			if (!string.IsNullOrEmpty(base.Uri))
			{
				xmlElement.SetAttribute("URI", base.Uri);
			}
			if (base.TransformChain.Count > 0)
			{
				xmlElement.AppendChild(base.TransformChain.GetXml(document, "http://www.w3.org/2001/04/xmlenc#"));
			}
			return xmlElement;
		}

		/// <summary>Loads XML information into the <see langword="&lt;CipherReference&gt;" /> element in XML encryption.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object that represents an XML element to use as the reference.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> provided is <see langword="null" />.</exception>
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.ReferenceType = value.LocalName;
			string attribute = Utils.GetAttribute(value, "URI", "http://www.w3.org/2001/04/xmlenc#");
			base.Uri = attribute ?? throw new CryptographicException("A Uri attribute is required for a CipherReference element.");
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			XmlNode xmlNode = value.SelectSingleNode("enc:Transforms", xmlNamespaceManager);
			if (xmlNode != null)
			{
				base.TransformChain.LoadXml(xmlNode as XmlElement);
			}
			_cachedXml = value;
		}
	}
	internal static class CryptoHelpers
	{
		private static readonly char[] _invalidChars = new char[5] { ',', '`', '[', '*', '&' };

		public static object CreateFromKnownName(string name)
		{
			return name switch
			{
				"http://www.w3.org/TR/2001/REC-xml-c14n-20010315" => new XmlDsigC14NTransform(), 
				"http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments" => new XmlDsigC14NWithCommentsTransform(), 
				"http://www.w3.org/2001/10/xml-exc-c14n#" => new XmlDsigExcC14NTransform(), 
				"http://www.w3.org/2001/10/xml-exc-c14n#WithComments" => new XmlDsigExcC14NWithCommentsTransform(), 
				"http://www.w3.org/2000/09/xmldsig#base64" => new XmlDsigBase64Transform(), 
				"http://www.w3.org/TR/1999/REC-xpath-19991116" => new XmlDsigXPathTransform(), 
				"http://www.w3.org/TR/1999/REC-xslt-19991116" => new XmlDsigXsltTransform(), 
				"http://www.w3.org/2000/09/xmldsig#enveloped-signature" => new XmlDsigEnvelopedSignatureTransform(), 
				"http://www.w3.org/2002/07/decrypt#XML" => new XmlDecryptionTransform(), 
				"urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform" => new XmlLicenseTransform(), 
				"http://www.w3.org/2000/09/xmldsig# X509Data" => new KeyInfoX509Data(), 
				"http://www.w3.org/2000/09/xmldsig# KeyName" => new KeyInfoName(), 
				"http://www.w3.org/2000/09/xmldsig# KeyValue/DSAKeyValue" => new DSAKeyValue(), 
				"http://www.w3.org/2000/09/xmldsig# KeyValue/RSAKeyValue" => new RSAKeyValue(), 
				"http://www.w3.org/2000/09/xmldsig# RetrievalMethod" => new KeyInfoRetrievalMethod(), 
				"http://www.w3.org/2001/04/xmlenc# EncryptedKey" => new KeyInfoEncryptedKey(), 
				"http://www.w3.org/2000/09/xmldsig#dsa-sha1" => new DSASignatureDescription(), 
				"System.Security.Cryptography.DSASignatureDescription" => new DSASignatureDescription(), 
				"http://www.w3.org/2000/09/xmldsig#rsa-sha1" => new RSAPKCS1SHA1SignatureDescription(), 
				"System.Security.Cryptography.RSASignatureDescription" => new RSAPKCS1SHA1SignatureDescription(), 
				"http://www.w3.org/2001/04/xmldsig-more#rsa-sha256" => new RSAPKCS1SHA256SignatureDescription(), 
				"http://www.w3.org/2001/04/xmldsig-more#rsa-sha384" => new RSAPKCS1SHA384SignatureDescription(), 
				"http://www.w3.org/2001/04/xmldsig-more#rsa-sha512" => new RSAPKCS1SHA512SignatureDescription(), 
				"http://www.w3.org/2000/09/xmldsig#sha1" => SHA1.Create(), 
				"MD5" => MD5.Create(), 
				"http://www.w3.org/2001/04/xmldsig-more#hmac-md5" => new HMACMD5(), 
				"http://www.w3.org/2001/04/xmlenc#tripledes-cbc" => TripleDES.Create(), 
				_ => null, 
			};
		}

		public static T CreateFromName<T>(string name) where T : class
		{
			if (name == null || name.IndexOfAny(_invalidChars) >= 0)
			{
				return null;
			}
			try
			{
				return (CreateFromKnownName(name) ?? CryptoConfig.CreateFromName(name)) as T;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
	[Serializable]
	public class CryptoSignedXmlRecursionException : XmlException
	{
		public CryptoSignedXmlRecursionException()
		{
		}

		public CryptoSignedXmlRecursionException(string message)
			: base(message)
		{
		}

		public CryptoSignedXmlRecursionException(string message, Exception inner)
			: base(message, inner)
		{
		}

		protected CryptoSignedXmlRecursionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	/// <summary>Represents the <see cref="T:System.Security.Cryptography.DSA" /> private key of the <see langword="&lt;KeyInfo&gt;" /> element.</summary>
	public class DSAKeyValue : KeyInfoClause
	{
		private DSA _key;

		private const string KeyValueElementName = "KeyValue";

		private const string DSAKeyValueElementName = "DSAKeyValue";

		private const string PElementName = "P";

		private const string QElementName = "Q";

		private const string GElementName = "G";

		private const string JElementName = "J";

		private const string YElementName = "Y";

		private const string SeedElementName = "Seed";

		private const string PgenCounterElementName = "PgenCounter";

		/// <summary>Gets or sets the key value represented by a <see cref="T:System.Security.Cryptography.DSA" /> object.</summary>
		/// <returns>The public key represented by a <see cref="T:System.Security.Cryptography.DSA" /> object.</returns>
		public DSA Key
		{
			get
			{
				return _key;
			}
			set
			{
				_key = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> class with a new, randomly-generated <see cref="T:System.Security.Cryptography.DSA" /> public key.</summary>
		public DSAKeyValue()
		{
			_key = DSA.Create();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> class with the specified <see cref="T:System.Security.Cryptography.DSA" /> public key.</summary>
		/// <param name="key">The instance of an implementation of the <see cref="T:System.Security.Cryptography.DSA" /> class that holds the public key.</param>
		public DSAKeyValue(DSA key)
		{
			_key = key;
		}

		/// <summary>Returns the XML representation of a <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> element.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> element.</returns>
		public override XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			DSAParameters dSAParameters = _key.ExportParameters(includePrivateParameters: false);
			XmlElement xmlElement = xmlDocument.CreateElement("KeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement2 = xmlDocument.CreateElement("DSAKeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement3 = xmlDocument.CreateElement("P", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement3.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dSAParameters.P)));
			xmlElement2.AppendChild(xmlElement3);
			XmlElement xmlElement4 = xmlDocument.CreateElement("Q", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement4.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dSAParameters.Q)));
			xmlElement2.AppendChild(xmlElement4);
			XmlElement xmlElement5 = xmlDocument.CreateElement("G", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement5.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dSAParameters.G)));
			xmlElement2.AppendChild(xmlElement5);
			XmlElement xmlElement6 = xmlDocument.CreateElement("Y", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement6.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dSAParameters.Y)));
			xmlElement2.AppendChild(xmlElement6);
			if (dSAParameters.J != null)
			{
				XmlElement xmlElement7 = xmlDocument.CreateElement("J", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement7.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dSAParameters.J)));
				xmlElement2.AppendChild(xmlElement7);
			}
			if (dSAParameters.Seed != null)
			{
				XmlElement xmlElement8 = xmlDocument.CreateElement("Seed", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement8.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dSAParameters.Seed)));
				xmlElement2.AppendChild(xmlElement8);
				XmlElement xmlElement9 = xmlDocument.CreateElement("PgenCounter", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement9.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(Utils.ConvertIntToByteArray(dSAParameters.Counter))));
				xmlElement2.AppendChild(xmlElement9);
			}
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> state from an XML element.</summary>
		/// <param name="value">The XML element to load the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> state from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter is not a valid <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> XML element.</exception>
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Name != "KeyValue" || value.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				throw new CryptographicException("Root element must be KeyValue element in namepsace http://www.w3.org/2000/09/xmldsig#");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
			XmlNode obj = value.SelectSingleNode("dsig:DSAKeyValue", xmlNamespaceManager) ?? throw new CryptographicException("KeyValue must contain child element DSAKeyValue");
			XmlNode xmlNode = obj.SelectSingleNode("dsig:Y", xmlNamespaceManager);
			if (xmlNode == null)
			{
				throw new CryptographicException("Y is missing");
			}
			XmlNode xmlNode2 = obj.SelectSingleNode("dsig:P", xmlNamespaceManager);
			XmlNode xmlNode3 = obj.SelectSingleNode("dsig:Q", xmlNamespaceManager);
			if ((xmlNode2 == null && xmlNode3 != null) || (xmlNode2 != null && xmlNode3 == null))
			{
				throw new CryptographicException("P and Q can only occour in combination");
			}
			XmlNode xmlNode4 = obj.SelectSingleNode("dsig:G", xmlNamespaceManager);
			XmlNode xmlNode5 = obj.SelectSingleNode("dsig:J", xmlNamespaceManager);
			XmlNode xmlNode6 = obj.SelectSingleNode("dsig:Seed", xmlNamespaceManager);
			XmlNode xmlNode7 = obj.SelectSingleNode("dsig:PgenCounter", xmlNamespaceManager);
			if ((xmlNode6 == null && xmlNode7 != null) || (xmlNode6 != null && xmlNode7 == null))
			{
				throw new CryptographicException("Seed and PgenCounter can only occur in combination");
			}
			try
			{
				Key.ImportParameters(new DSAParameters
				{
					P = ((xmlNode2 != null) ? Convert.FromBase64String(xmlNode2.InnerText) : null),
					Q = ((xmlNode3 != null) ? Convert.FromBase64String(xmlNode3.InnerText) : null),
					G = ((xmlNode4 != null) ? Convert.FromBase64String(xmlNode4.InnerText) : null),
					Y = Convert.FromBase64String(xmlNode.InnerText),
					J = ((xmlNode5 != null) ? Convert.FromBase64String(xmlNode5.InnerText) : null),
					Seed = ((xmlNode6 != null) ? Convert.FromBase64String(xmlNode6.InnerText) : null),
					Counter = ((xmlNode7 != null) ? Utils.ConvertByteArrayToInt(Convert.FromBase64String(xmlNode7.InnerText)) : 0)
				});
			}
			catch (Exception inner)
			{
				throw new CryptographicException("An error occurred parsing the key components", inner);
			}
		}
	}
	internal class DSASignatureDescription : SignatureDescription
	{
		private const string HashAlgorithm = "SHA1";

		public DSASignatureDescription()
		{
			base.KeyAlgorithm = typeof(DSA).AssemblyQualifiedName;
			base.FormatterAlgorithm = typeof(DSASignatureFormatter).AssemblyQualifiedName;
			base.DeformatterAlgorithm = typeof(DSASignatureDeformatter).AssemblyQualifiedName;
			base.DigestAlgorithm = "SHA1";
		}

		public sealed override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter obj = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(base.DeformatterAlgorithm);
			obj.SetKey(key);
			obj.SetHashAlgorithm("SHA1");
			return obj;
		}

		public sealed override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter obj = (AsymmetricSignatureFormatter)CryptoConfig.CreateFromName(base.FormatterAlgorithm);
			obj.SetKey(key);
			obj.SetHashAlgorithm("SHA1");
			return obj;
		}

		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA1.Create();
		}
	}
	/// <summary>Represents the object element of an XML signature that holds data to be signed.</summary>
	public class DataObject
	{
		private string _id;

		private string _mimeType;

		private string _encoding;

		private CanonicalXmlNodeList _elData;

		private XmlElement _cachedXml;

		/// <summary>Gets or sets the identification of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The name of the element that contains data to be used.</returns>
		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the MIME type of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The MIME type of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object. The default is <see langword="null" />.</returns>
		public string MimeType
		{
			get
			{
				return _mimeType;
			}
			set
			{
				_mimeType = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the encoding of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The type of encoding of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</returns>
		public string Encoding
		{
			get
			{
				return _encoding;
			}
			set
			{
				_encoding = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the data value of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The data of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value used to set the property is <see langword="null" />.</exception>
		public XmlNodeList Data
		{
			get
			{
				return _elData;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				_elData = new CanonicalXmlNodeList();
				foreach (XmlNode item in value)
				{
					_elData.Add(item);
				}
				_cachedXml = null;
			}
		}

		private bool CacheValid => _cachedXml != null;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> class.</summary>
		public DataObject()
		{
			_cachedXml = null;
			_elData = new CanonicalXmlNodeList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> class with the specified identification, MIME type, encoding, and data.</summary>
		/// <param name="id">The identification to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" /> with.</param>
		/// <param name="mimeType">The MIME type of the data used to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" />.</param>
		/// <param name="encoding">The encoding of the data used to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" />.</param>
		/// <param name="data">The data to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" /> with.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		public DataObject(string id, string mimeType, string encoding, XmlElement data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			_id = id;
			_mimeType = mimeType;
			_encoding = encoding;
			_elData = new CanonicalXmlNodeList();
			_elData.Add(data);
			_cachedXml = null;
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</returns>
		public XmlElement GetXml()
		{
			if (CacheValid)
			{
				return _cachedXml;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("Object", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(_id))
			{
				xmlElement.SetAttribute("Id", _id);
			}
			if (!string.IsNullOrEmpty(_mimeType))
			{
				xmlElement.SetAttribute("MimeType", _mimeType);
			}
			if (!string.IsNullOrEmpty(_encoding))
			{
				xmlElement.SetAttribute("Encoding", _encoding);
			}
			if (_elData != null)
			{
				foreach (XmlNode elDatum in _elData)
				{
					xmlElement.AppendChild(document.ImportNode(elDatum, deep: true));
				}
			}
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.DataObject" /> state from an XML element.</summary>
		/// <param name="value">The XML element to load the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> state from.</param>
		/// <exception cref="T:System.ArgumentNullException">The value from the XML element is <see langword="null" />.</exception>
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			_id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			_mimeType = Utils.GetAttribute(value, "MimeType", "http://www.w3.org/2000/09/xmldsig#");
			_encoding = Utils.GetAttribute(value, "Encoding", "http://www.w3.org/2000/09/xmldsig#");
			foreach (XmlNode childNode in value.ChildNodes)
			{
				_elData.Add(childNode);
			}
			_cachedXml = value;
		}
	}
	/// <summary>Represents the <see langword="&lt;DataReference&gt;" /> element used in XML encryption. This class cannot be inherited.</summary>
	public sealed class DataReference : EncryptedReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> class.</summary>
		public DataReference()
		{
			base.ReferenceType = "DataReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> class using the specified Uniform Resource Identifier (URI).</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) that points to the encrypted data.</param>
		public DataReference(string uri)
			: base(uri)
		{
			base.ReferenceType = "DataReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> class using the specified Uniform Resource Identifier (URI) and a <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) that points to the encrypted data.</param>
		/// <param name="transformChain">A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms to do on the encrypted data.</param>
		public DataReference(string uri, TransformChain transformChain)
			: base(uri, transformChain)
		{
			base.ReferenceType = "DataReference";
		}
	}
	internal enum DocPosition
	{
		BeforeRootElement,
		InRootElement,
		AfterRootElement
	}
	/// <summary>Represents the <see langword="&lt;EncryptedData&gt;" /> element in XML encryption. This class cannot be inherited.</summary>
	public sealed class EncryptedData : EncryptedType
	{
		/// <summary>Loads XML information into the <see langword="&lt;EncryptedData&gt;" /> element in XML encryption.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object representing an XML element to use for the <see langword="&lt;EncryptedData&gt;" /> element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> provided is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain a &lt;<see langword="CypherData" />&gt; node.</exception>
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			Id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2001/04/xmlenc#");
			Type = Utils.GetAttribute(value, "Type", "http://www.w3.org/2001/04/xmlenc#");
			MimeType = Utils.GetAttribute(value, "MimeType", "http://www.w3.org/2001/04/xmlenc#");
			Encoding = Utils.GetAttribute(value, "Encoding", "http://www.w3.org/2001/04/xmlenc#");
			XmlNode xmlNode = value.SelectSingleNode("enc:EncryptionMethod", xmlNamespaceManager);
			EncryptionMethod = new EncryptionMethod();
			if (xmlNode != null)
			{
				EncryptionMethod.LoadXml(xmlNode as XmlElement);
			}
			base.KeyInfo = new KeyInfo();
			XmlNode xmlNode2 = value.SelectSingleNode("ds:KeyInfo", xmlNamespaceManager);
			if (xmlNode2 != null)
			{
				base.KeyInfo.LoadXml(xmlNode2 as XmlElement);
			}
			XmlNode xmlNode3 = value.SelectSingleNode("enc:CipherData", xmlNamespaceManager);
			if (xmlNode3 == null)
			{
				throw new CryptographicException("Cipher data is not specified.");
			}
			CipherData = new CipherData();
			CipherData.LoadXml(xmlNode3 as XmlElement);
			XmlNode xmlNode4 = value.SelectSingleNode("enc:EncryptionProperties", xmlNamespaceManager);
			if (xmlNode4 != null)
			{
				XmlNodeList xmlNodeList = xmlNode4.SelectNodes("enc:EncryptionProperty", xmlNamespaceManager);
				if (xmlNodeList != null)
				{
					foreach (XmlNode item in xmlNodeList)
					{
						EncryptionProperty encryptionProperty = new EncryptionProperty();
						encryptionProperty.LoadXml(item as XmlElement);
						EncryptionProperties.Add(encryptionProperty);
					}
				}
			}
			_cachedXml = value;
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> that represents the <see langword="&lt;EncryptedData&gt;" /> element in XML encryption.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> value is <see langword="null" />.</exception>
		public override XmlElement GetXml()
		{
			if (base.CacheValid)
			{
				return _cachedXml;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("EncryptedData", "http://www.w3.org/2001/04/xmlenc#");
			if (!string.IsNullOrEmpty(Id))
			{
				xmlElement.SetAttribute("Id", Id);
			}
			if (!string.IsNullOrEmpty(Type))
			{
				xmlElement.SetAttribute("Type", Type);
			}
			if (!string.IsNullOrEmpty(MimeType))
			{
				xmlElement.SetAttribute("MimeType", MimeType);
			}
			if (!string.IsNullOrEmpty(Encoding))
			{
				xmlElement.SetAttribute("Encoding", Encoding);
			}
			if (EncryptionMethod != null)
			{
				xmlElement.AppendChild(EncryptionMethod.GetXml(document));
			}
			if (base.KeyInfo.Count > 0)
			{
				xmlElement.AppendChild(base.KeyInfo.GetXml(document));
			}
			if (CipherData == null)
			{
				throw new CryptographicException("Cipher data is not specified.");
			}
			xmlElement.AppendChild(CipherData.GetXml(document));
			if (EncryptionProperties.Count > 0)
			{
				XmlElement xmlElement2 = document.CreateElement("EncryptionProperties", "http://www.w3.org/2001/04/xmlenc#");
				for (int i = 0; i < EncryptionProperties.Count; i++)
				{
					EncryptionProperty encryptionProperty = EncryptionProperties.Item(i);
					xmlElement2.AppendChild(encryptionProperty.GetXml(document));
				}
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> class.</summary>
		public EncryptedData()
		{
		}
	}
	/// <summary>Represents the <see langword="&lt;EncryptedKey&gt;" /> element in XML encryption. This class cannot be inherited.</summary>
	public sealed class EncryptedKey : EncryptedType
	{
		private string _recipient;

		private string _carriedKeyName;

		private ReferenceList _referenceList;

		/// <summary>Gets or sets the optional <see langword="Recipient" /> attribute in XML encryption.</summary>
		/// <returns>A string representing the value of the <see langword="Recipient" /> attribute.</returns>
		public string Recipient
		{
			get
			{
				if (_recipient == null)
				{
					_recipient = string.Empty;
				}
				return _recipient;
			}
			set
			{
				_recipient = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the optional <see langword="&lt;CarriedKeyName&gt;" /> element in XML encryption.</summary>
		/// <returns>A string that represents a name for the key value.</returns>
		public string CarriedKeyName
		{
			get
			{
				return _carriedKeyName;
			}
			set
			{
				_carriedKeyName = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the <see langword="&lt;ReferenceList&gt;" /> element in XML encryption.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object.</returns>
		public ReferenceList ReferenceList
		{
			get
			{
				if (_referenceList == null)
				{
					_referenceList = new ReferenceList();
				}
				return _referenceList;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> class.</summary>
		public EncryptedKey()
		{
		}

		/// <summary>Adds a <see langword="&lt;DataReference&gt;" /> element to the <see langword="&lt;ReferenceList&gt;" /> element.</summary>
		/// <param name="dataReference">A <see cref="T:System.Security.Cryptography.Xml.DataReference" /> object to add to the <see cref="P:System.Security.Cryptography.Xml.EncryptedKey.ReferenceList" /> property.</param>
		public void AddReference(DataReference dataReference)
		{
			ReferenceList.Add(dataReference);
		}

		/// <summary>Adds a <see langword="&lt;KeyReference&gt;" /> element to the <see langword="&lt;ReferenceList&gt;" /> element.</summary>
		/// <param name="keyReference">A <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to add to the <see cref="P:System.Security.Cryptography.Xml.EncryptedKey.ReferenceList" /> property.</param>
		public void AddReference(KeyReference keyReference)
		{
			ReferenceList.Add(keyReference);
		}

		/// <summary>Loads the specified XML information into the <see langword="&lt;EncryptedKey&gt;" /> element in XML encryption.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> representing an XML element to use for the <see langword="&lt;EncryptedKey&gt;" /> element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain a <see cref="T:System.Security.Cryptography.Xml.CipherData" /> element.</exception>
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			Id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2001/04/xmlenc#");
			Type = Utils.GetAttribute(value, "Type", "http://www.w3.org/2001/04/xmlenc#");
			MimeType = Utils.GetAttribute(value, "MimeType", "http://www.w3.org/2001/04/xmlenc#");
			Encoding = Utils.GetAttribute(value, "Encoding", "http://www.w3.org/2001/04/xmlenc#");
			Recipient = Utils.GetAttribute(value, "Recipient", "http://www.w3.org/2001/04/xmlenc#");
			XmlNode xmlNode = value.SelectSingleNode("enc:EncryptionMethod", xmlNamespaceManager);
			EncryptionMethod = new EncryptionMethod();
			if (xmlNode != null)
			{
				EncryptionMethod.LoadXml(xmlNode as XmlElement);
			}
			base.KeyInfo = new KeyInfo();
			XmlNode xmlNode2 = value.SelectSingleNode("ds:KeyInfo", xmlNamespaceManager);
			if (xmlNode2 != null)
			{
				base.KeyInfo.LoadXml(xmlNode2 as XmlElement);
			}
			XmlNode xmlNode3 = value.SelectSingleNode("enc:CipherData", xmlNamespaceManager);
			if (xmlNode3 == null)
			{
				throw new CryptographicException("Cipher data is not specified.");
			}
			CipherData = new CipherData();
			CipherData.LoadXml(xmlNode3 as XmlElement);
			XmlNode xmlNode4 = value.SelectSingleNode("enc:EncryptionProperties", xmlNamespaceManager);
			if (xmlNode4 != null)
			{
				XmlNodeList xmlNodeList = xmlNode4.SelectNodes("enc:EncryptionProperty", xmlNamespaceManager);
				if (xmlNodeList != null)
				{
					foreach (XmlNode item in xmlNodeList)
					{
						EncryptionProperty encryptionProperty = new EncryptionProperty();
						encryptionProperty.LoadXml(item as XmlElement);
						EncryptionProperties.Add(encryptionProperty);
					}
				}
			}
			XmlNode xmlNode6 = value.SelectSingleNode("enc:CarriedKeyName", xmlNamespaceManager);
			if (xmlNode6 != null)
			{
				CarriedKeyName = xmlNode6.InnerText;
			}
			XmlNode xmlNode7 = value.SelectSingleNode("enc:ReferenceList", xmlNamespaceManager);
			if (xmlNode7 != null)
			{
				XmlNodeList xmlNodeList2 = xmlNode7.SelectNodes("enc:DataReference", xmlNamespaceManager);
				if (xmlNodeList2 != null)
				{
					foreach (XmlNode item2 in xmlNodeList2)
					{
						DataReference dataReference = new DataReference();
						dataReference.LoadXml(item2 as XmlElement);
						ReferenceList.Add(dataReference);
					}
				}
				XmlNodeList xmlNodeList3 = xmlNode7.SelectNodes("enc:KeyReference", xmlNamespaceManager);
				if (xmlNodeList3 != null)
				{
					foreach (XmlNode item3 in xmlNodeList3)
					{
						KeyReference keyReference = new KeyReference();
						keyReference.LoadXml(item3 as XmlElement);
						ReferenceList.Add(keyReference);
					}
				}
			}
			_cachedXml = value;
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> that represents the <see langword="&lt;EncryptedKey&gt;" /> element in XML encryption.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> value is <see langword="null" />.</exception>
		public override XmlElement GetXml()
		{
			if (base.CacheValid)
			{
				return _cachedXml;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("EncryptedKey", "http://www.w3.org/2001/04/xmlenc#");
			if (!string.IsNullOrEmpty(Id))
			{
				xmlElement.SetAttribute("Id", Id);
			}
			if (!string.IsNullOrEmpty(Type))
			{
				xmlElement.SetAttribute("Type", Type);
			}
			if (!string.IsNullOrEmpty(MimeType))
			{
				xmlElement.SetAttribute("MimeType", MimeType);
			}
			if (!string.IsNullOrEmpty(Encoding))
			{
				xmlElement.SetAttribute("Encoding", Encoding);
			}
			if (!string.IsNullOrEmpty(Recipient))
			{
				xmlElement.SetAttribute("Recipient", Recipient);
			}
			if (EncryptionMethod != null)
			{
				xmlElement.AppendChild(EncryptionMethod.GetXml(document));
			}
			if (base.KeyInfo.Count > 0)
			{
				xmlElement.AppendChild(base.KeyInfo.GetXml(document));
			}
			if (CipherData == null)
			{
				throw new CryptographicException("Cipher data is not specified.");
			}
			xmlElement.AppendChild(CipherData.GetXml(document));
			if (EncryptionProperties.Count > 0)
			{
				XmlElement xmlElement2 = document.CreateElement("EncryptionProperties", "http://www.w3.org/2001/04/xmlenc#");
				for (int i = 0; i < EncryptionProperties.Count; i++)
				{
					EncryptionProperty encryptionProperty = EncryptionProperties.Item(i);
					xmlElement2.AppendChild(encryptionProperty.GetXml(document));
				}
				xmlElement.AppendChild(xmlElement2);
			}
			if (ReferenceList.Count > 0)
			{
				XmlElement xmlElement3 = document.CreateElement("ReferenceList", "http://www.w3.org/2001/04/xmlenc#");
				for (int j = 0; j < ReferenceList.Count; j++)
				{
					xmlElement3.AppendChild(ReferenceList[j].GetXml(document));
				}
				xmlElement.AppendChild(xmlElement3);
			}
			if (CarriedKeyName != null)
			{
				XmlElement xmlElement4 = document.CreateElement("CarriedKeyName", "http://www.w3.org/2001/04/xmlenc#");
				XmlText newChild = document.CreateTextNode(CarriedKeyName);
				xmlElement4.AppendChild(newChild);
				xmlElement.AppendChild(xmlElement4);
			}
			return xmlElement;
		}
	}
	/// <summary>Represents the abstract base class used in XML encryption from which the <see cref="T:System.Security.Cryptography.Xml.CipherReference" />, <see cref="T:System.Security.Cryptography.Xml.KeyReference" />, and <see cref="T:System.Security.Cryptography.Xml.DataReference" /> classes derive.</summary>
	public abstract class EncryptedReference
	{
		private string _uri;

		private string _referenceType;

		private TransformChain _transformChain;

		internal XmlElement _cachedXml;

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <returns>The Uniform Resource Identifier (URI) of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.EncryptedReference.Uri" /> property was set to <see langword="null" />.</exception>
		public string Uri
		{
			get
			{
				return _uri;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("A Uri attribute is required for a CipherReference element.");
				}
				_uri = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the transform chain of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms used on the encrypted data.</returns>
		public TransformChain TransformChain
		{
			get
			{
				if (_transformChain == null)
				{
					_transformChain = new TransformChain();
				}
				return _transformChain;
			}
			set
			{
				_transformChain = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets a reference type.</summary>
		/// <returns>The reference type of the encrypted data.</returns>
		protected string ReferenceType
		{
			get
			{
				return _referenceType;
			}
			set
			{
				_referenceType = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets a value that indicates whether the cache is valid.</summary>
		/// <returns>
		///   <see langword="true" /> if the cache is valid; otherwise, <see langword="false" />.</returns>
		protected internal bool CacheValid => _cachedXml != null;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> class.</summary>
		protected EncryptedReference()
			: this(string.Empty, new TransformChain())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> class using the specified Uniform Resource Identifier (URI).</summary>
		/// <param name="uri">The Uniform Resource Identifier (URI) that points to the data to encrypt.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="uri" /> parameter is <see langword="null" />.</exception>
		protected EncryptedReference(string uri)
			: this(uri, new TransformChain())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> class using the specified Uniform Resource Identifier (URI) and transform chain.</summary>
		/// <param name="uri">The Uniform Resource Identifier (URI) that points to the data to encrypt.</param>
		/// <param name="transformChain">A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms to be done on the data to encrypt.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="uri" /> parameter is <see langword="null" />.</exception>
		protected EncryptedReference(string uri, TransformChain transformChain)
		{
			TransformChain = transformChain;
			Uri = uri;
			_cachedXml = null;
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to the current transform chain of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <param name="transform">A <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to add to the transform chain.</param>
		public void AddTransform(Transform transform)
		{
			TransformChain.Add(transform);
		}

		/// <summary>Returns the XML representation of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents the values of the <see langword="&lt;EncryptedReference&gt;" /> element in XML encryption.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.EncryptedReference.ReferenceType" /> property is <see langword="null" />.</exception>
		public virtual XmlElement GetXml()
		{
			if (CacheValid)
			{
				return _cachedXml;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal XmlElement GetXml(XmlDocument document)
		{
			if (ReferenceType == null)
			{
				throw new CryptographicException("The Reference type must be set in an EncryptedReference object.");
			}
			XmlElement xmlElement = document.CreateElement(ReferenceType, "http://www.w3.org/2001/04/xmlenc#");
			if (!string.IsNullOrEmpty(_uri))
			{
				xmlElement.SetAttribute("URI", _uri);
			}
			if (TransformChain.Count > 0)
			{
				xmlElement.AppendChild(TransformChain.GetXml(document, "http://www.w3.org/2000/09/xmldsig#"));
			}
			return xmlElement;
		}

		/// <summary>Loads an XML element into an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object that represents an XML element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		public virtual void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			ReferenceType = value.LocalName;
			string attribute = Utils.GetAttribute(value, "URI", "http://www.w3.org/2001/04/xmlenc#");
			if (attribute == null)
			{
				throw new ArgumentNullException("A Uri attribute is required for a CipherReference element.");
			}
			Uri = attribute;
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlNode xmlNode = value.SelectSingleNode("ds:Transforms", xmlNamespaceManager);
			if (xmlNode != null)
			{
				TransformChain.LoadXml(xmlNode as XmlElement);
			}
			_cachedXml = value;
		}
	}
	/// <summary>Represents the abstract base class from which the classes <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> and <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> derive.</summary>
	public abstract class EncryptedType
	{
		private string _id;

		private string _type;

		private string _mimeType;

		private string _encoding;

		private EncryptionMethod _encryptionMethod;

		private CipherData _cipherData;

		private EncryptionPropertyCollection _props;

		private KeyInfo _keyInfo;

		internal XmlElement _cachedXml;

		internal bool CacheValid => _cachedXml != null;

		/// <summary>Gets or sets the <see langword="Id" /> attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string of the <see langword="Id" /> attribute of the <see langword="&lt;EncryptedType&gt;" /> element.</returns>
		public virtual string Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the <see langword="Type" /> attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string that describes the text form of the encrypted data.</returns>
		public virtual string Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the <see langword="MimeType" /> attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string that describes the media type of the encrypted data.</returns>
		public virtual string MimeType
		{
			get
			{
				return _mimeType;
			}
			set
			{
				_mimeType = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the <see langword="Encoding" /> attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string that describes the encoding of the encrypted data.</returns>
		public virtual string Encoding
		{
			get
			{
				return _encoding;
			}
			set
			{
				_encoding = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets of sets the <see langword="&lt;KeyInfo&gt;" /> element in XML encryption.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</returns>
		public KeyInfo KeyInfo
		{
			get
			{
				if (_keyInfo == null)
				{
					_keyInfo = new KeyInfo();
				}
				return _keyInfo;
			}
			set
			{
				_keyInfo = value;
			}
		}

		/// <summary>Gets or sets the <see langword="&lt;EncryptionMethod&gt;" /> element for XML encryption.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> object that represents the <see langword="&lt;EncryptionMethod&gt;" /> element.</returns>
		public virtual EncryptionMethod EncryptionMethod
		{
			get
			{
				return _encryptionMethod;
			}
			set
			{
				_encryptionMethod = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the <see langword="&lt;EncryptionProperties&gt;" /> element in XML encryption.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		public virtual EncryptionPropertyCollection EncryptionProperties
		{
			get
			{
				if (_props == null)
				{
					_props = new EncryptionPropertyCollection();
				}
				return _props;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> value for an instance of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> class.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.EncryptedType.CipherData" /> property was set to <see langword="null" />.</exception>
		public virtual CipherData CipherData
		{
			get
			{
				if (_cipherData == null)
				{
					_cipherData = new CipherData();
				}
				return _cipherData;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				_cipherData = value;
				_cachedXml = null;
			}
		}

		/// <summary>Adds an <see langword="&lt;EncryptionProperty&gt;" /> child element to the <see langword="&lt;EncryptedProperties&gt;" /> element in the current <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> object in XML encryption.</summary>
		/// <param name="ep">An <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</param>
		public void AddProperty(EncryptionProperty ep)
		{
			EncryptionProperties.Add(ep);
		}

		/// <summary>Loads XML information into the <see langword="&lt;EncryptedType&gt;" /> element in XML encryption.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object representing an XML element to use in the <see langword="&lt;EncryptedType&gt;" /> element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> provided is <see langword="null" />.</exception>
		public abstract void LoadXml(XmlElement value);

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents the <see langword="&lt;EncryptedType&gt;" /> element in XML encryption.</returns>
		public abstract XmlElement GetXml();

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> class.</summary>
		protected EncryptedType()
		{
		}
	}
	/// <summary>Represents the process model for implementing XML encryption.</summary>
	public class EncryptedXml
	{
		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for XML encryption syntax and processing. This field is constant.</summary>
		public const string XmlEncNamespaceUrl = "http://www.w3.org/2001/04/xmlenc#";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for an XML encryption element. This field is constant.</summary>
		public const string XmlEncElementUrl = "http://www.w3.org/2001/04/xmlenc#Element";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for XML encryption element content. This field is constant.</summary>
		public const string XmlEncElementContentUrl = "http://www.w3.org/2001/04/xmlenc#Content";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the XML encryption <see langword="&lt;EncryptedKey&gt;" /> element. This field is constant.</summary>
		public const string XmlEncEncryptedKeyUrl = "http://www.w3.org/2001/04/xmlenc#EncryptedKey";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the Digital Encryption Standard (DES) algorithm. This field is constant.</summary>
		public const string XmlEncDESUrl = "http://www.w3.org/2001/04/xmlenc#des-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the Triple DES algorithm. This field is constant.</summary>
		public const string XmlEncTripleDESUrl = "http://www.w3.org/2001/04/xmlenc#tripledes-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 128-bit Advanced Encryption Standard (AES) algorithm (also known as the Rijndael algorithm). This field is constant.</summary>
		public const string XmlEncAES128Url = "http://www.w3.org/2001/04/xmlenc#aes128-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 256-bit Advanced Encryption Standard (AES) algorithm (also known as the Rijndael algorithm). This field is constant.</summary>
		public const string XmlEncAES256Url = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 192-bit Advanced Encryption Standard (AES) algorithm (also known as the Rijndael algorithm). This field is constant.</summary>
		public const string XmlEncAES192Url = "http://www.w3.org/2001/04/xmlenc#aes192-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the RSA Public Key Cryptography Standard (PKCS) Version 1.5 algorithm. This field is constant.</summary>
		public const string XmlEncRSA15Url = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the RSA Optimal Asymmetric Encryption Padding (OAEP) encryption algorithm. This field is constant.</summary>
		public const string XmlEncRSAOAEPUrl = "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the TRIPLEDES key wrap algorithm. This field is constant.</summary>
		public const string XmlEncTripleDESKeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 128-bit Advanced Encryption Standard (AES) Key Wrap algorithm (also known as the Rijndael Key Wrap algorithm). This field is constant.</summary>
		public const string XmlEncAES128KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes128";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 256-bit Advanced Encryption Standard (AES) Key Wrap algorithm (also known as the Rijndael Key Wrap algorithm). This field is constant.</summary>
		public const string XmlEncAES256KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes256";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 192-bit Advanced Encryption Standard (AES) Key Wrap algorithm (also known as the Rijndael Key Wrap algorithm). This field is constant.</summary>
		public const string XmlEncAES192KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes192";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the SHA-256 algorithm. This field is constant.</summary>
		public const string XmlEncSHA256Url = "http://www.w3.org/2001/04/xmlenc#sha256";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the SHA-512 algorithm. This field is constant.</summary>
		public const string XmlEncSHA512Url = "http://www.w3.org/2001/04/xmlenc#sha512";

		private XmlDocument _document;

		private Evidence _evidence;

		private XmlResolver _xmlResolver;

		private const int _capacity = 4;

		private Hashtable _keyNameMapping;

		private PaddingMode _padding;

		private CipherMode _mode;

		private Encoding _encoding;

		private string _recipient;

		private int _xmlDsigSearchDepthCounter;

		private int _xmlDsigSearchDepth;

		/// <summary>Gets or sets the XML digital signature recursion depth to prevent infinite recursion and stack overflow. This might happen if the digital signature XML contains the URI which then points back to the original XML.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.</returns>
		public int XmlDSigSearchDepth
		{
			get
			{
				return _xmlDsigSearchDepth;
			}
			set
			{
				_xmlDsigSearchDepth = value;
			}
		}

		/// <summary>Gets or sets the evidence of the <see cref="T:System.Xml.XmlDocument" /> object from which the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object is constructed.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object.</returns>
		public Evidence DocumentEvidence
		{
			get
			{
				return _evidence;
			}
			set
			{
				_evidence = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlResolver" /> object used by the Document Object Model (DOM) to resolve external XML references.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlResolver" /> object.</returns>
		public XmlResolver Resolver
		{
			get
			{
				return _xmlResolver;
			}
			set
			{
				_xmlResolver = value;
			}
		}

		/// <summary>Gets or sets the padding mode used for XML encryption.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.PaddingMode" /> values that specifies the type of padding used for encryption.</returns>
		public PaddingMode Padding
		{
			get
			{
				return _padding;
			}
			set
			{
				_padding = value;
			}
		}

		/// <summary>Gets or sets the cipher mode used for XML encryption.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.CipherMode" /> values.</returns>
		public CipherMode Mode
		{
			get
			{
				return _mode;
			}
			set
			{
				_mode = value;
			}
		}

		/// <summary>Gets or sets the encoding used for XML encryption.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object.</returns>
		public Encoding Encoding
		{
			get
			{
				return _encoding;
			}
			set
			{
				_encoding = value;
			}
		}

		/// <summary>Gets or sets the recipient of the encrypted key information.</summary>
		/// <returns>The recipient of the encrypted key information.</returns>
		public string Recipient
		{
			get
			{
				if (_recipient == null)
				{
					_recipient = string.Empty;
				}
				return _recipient;
			}
			set
			{
				_recipient = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class.</summary>
		public EncryptedXml()
			: this(new XmlDocument())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class using the specified XML document.</summary>
		/// <param name="document">An <see cref="T:System.Xml.XmlDocument" /> object used to initialize the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object.</param>
		public EncryptedXml(XmlDocument document)
			: this(document, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class using the specified XML document and evidence.</summary>
		/// <param name="document">An <see cref="T:System.Xml.XmlDocument" /> object used to initialize the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object.</param>
		/// <param name="evidence">An <see cref="T:System.Security.Policy.Evidence" /> object associated with the <see cref="T:System.Xml.XmlDocument" /> object.</param>
		public EncryptedXml(XmlDocument document, Evidence evidence)
		{
			_document = document;
			_evidence = evidence;
			_xmlResolver = null;
			_padding = PaddingMode.ISO10126;
			_mode = CipherMode.CBC;
			_encoding = Encoding.UTF8;
			_keyNameMapping = new Hashtable(4);
			_xmlDsigSearchDepth = 20;
		}

		private bool IsOverXmlDsigRecursionLimit()
		{
			if (_xmlDsigSearchDepthCounter > XmlDSigSearchDepth)
			{
				return true;
			}
			return false;
		}

		private byte[] GetCipherValue(CipherData cipherData)
		{
			if (cipherData == null)
			{
				throw new ArgumentNullException("cipherData");
			}
			WebResponse webResponse = null;
			Stream stream = null;
			if (cipherData.CipherValue != null)
			{
				return cipherData.CipherValue;
			}
			if (cipherData.CipherReference != null)
			{
				if (cipherData.CipherReference.CipherValue != null)
				{
					return cipherData.CipherReference.CipherValue;
				}
				Stream stream2 = null;
				if (cipherData.CipherReference.Uri == null)
				{
					throw new CryptographicException(" The specified Uri is not supported.");
				}
				if (cipherData.CipherReference.Uri.Length == 0)
				{
					string baseUri = ((_document == null) ? null : _document.BaseURI);
					stream2 = (cipherData.CipherReference.TransformChain ?? throw new CryptographicException(" The specified Uri is not supported.")).TransformToOctetStream(_document, _xmlResolver, baseUri);
				}
				else
				{
					if (cipherData.CipherReference.Uri[0] != '#')
					{
						throw new CryptographicException("Unable to resolve Uri {0}.", cipherData.CipherReference.Uri);
					}
					string idValue = Utils.ExtractIdFromLocalUri(cipherData.CipherReference.Uri);
					XmlElement idElement = GetIdElement(_document, idValue);
					if (idElement == null || idElement.OuterXml == null)
					{
						throw new CryptographicException(" The specified Uri is not supported.");
					}
					stream = new MemoryStream(_encoding.GetBytes(idElement.OuterXml));
					string baseUri2 = ((_document == null) ? null : _document.BaseURI);
					stream2 = (cipherData.CipherReference.TransformChain ?? throw new CryptographicException(" The specified Uri is not supported.")).TransformToOctetStream(stream, _xmlResolver, baseUri2);
				}
				byte[] array = null;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					Utils.Pump(stream2, memoryStream);
					array = memoryStream.ToArray();
					webResponse?.Close();
					stream?.Close();
					stream2.Close();
				}
				cipherData.CipherReference.CipherValue = array;
				return array;
			}
			throw new CryptographicException("Cipher data is not specified.");
		}

		/// <summary>Determines how to resolve internal Uniform Resource Identifier (URI) references.</summary>
		/// <param name="document">An <see cref="T:System.Xml.XmlDocument" /> object that contains an element with an ID value.</param>
		/// <param name="idValue">A string that represents the ID value.</param>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that contains an ID indicating how internal Uniform Resource Identifiers (URIs) are to be resolved.</returns>
		public virtual XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			return SignedXml.DefaultGetIdElement(document, idValue);
		}

		/// <summary>Retrieves the decryption initialization vector (IV) from an <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <param name="encryptedData">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object that contains the initialization vector (IV) to retrieve.</param>
		/// <param name="symmetricAlgorithmUri">The Uniform Resource Identifier (URI) that describes the cryptographic algorithm associated with the <paramref name="encryptedData" /> value.</param>
		/// <returns>A byte array that contains the decryption initialization vector (IV).</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedData" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="encryptedData" /> parameter has an <see cref="P:System.Security.Cryptography.Xml.EncryptedType.EncryptionMethod" /> property that is null.  
		///  -or-  
		///  The value of the <paramref name="symmetricAlgorithmUrisymAlgUri" /> parameter is not a supported algorithm.</exception>
		public virtual byte[] GetDecryptionIV(EncryptedData encryptedData, string symmetricAlgorithmUri)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			int num = 0;
			if (symmetricAlgorithmUri == null)
			{
				if (encryptedData.EncryptionMethod == null)
				{
					throw new CryptographicException("Symmetric algorithm is not specified.");
				}
				symmetricAlgorithmUri = encryptedData.EncryptionMethod.KeyAlgorithm;
			}
			switch (symmetricAlgorithmUri)
			{
			case "http://www.w3.org/2001/04/xmlenc#des-cbc":
			case "http://www.w3.org/2001/04/xmlenc#tripledes-cbc":
				num = 8;
				break;
			case "http://www.w3.org/2001/04/xmlenc#aes128-cbc":
			case "http://www.w3.org/2001/04/xmlenc#aes192-cbc":
			case "http://www.w3.org/2001/04/xmlenc#aes256-cbc":
				num = 16;
				break;
			default:
				throw new CryptographicException(" The specified Uri is not supported.");
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(GetCipherValue(encryptedData.CipherData), 0, array, 0, array.Length);
			return array;
		}

		/// <summary>Retrieves the decryption key from the specified <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <param name="encryptedData">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object that contains the decryption key to retrieve.</param>
		/// <param name="symmetricAlgorithmUri">The size of the decryption key to retrieve.</param>
		/// <returns>A <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> object associated with the decryption key.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedData" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The encryptedData parameter has an <see cref="P:System.Security.Cryptography.Xml.EncryptedType.EncryptionMethod" /> property that is null.  
		///  -or-  
		///  The encrypted key cannot be retrieved using the specified parameters.</exception>
		public virtual SymmetricAlgorithm GetDecryptionKey(EncryptedData encryptedData, string symmetricAlgorithmUri)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (encryptedData.KeyInfo == null)
			{
				return null;
			}
			IEnumerator enumerator = encryptedData.KeyInfo.GetEnumerator();
			EncryptedKey encryptedKey = null;
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is KeyInfoName { Value: var value })
				{
					if ((SymmetricAlgorithm)_keyNameMapping[value] != null)
					{
						return (SymmetricAlgorithm)_keyNameMapping[value];
					}
					XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(_document.NameTable);
					xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
					XmlNodeList xmlNodeList = _document.SelectNodes("//enc:EncryptedKey", xmlNamespaceManager);
					if (xmlNodeList == null)
					{
						break;
					}
					foreach (XmlNode item in xmlNodeList)
					{
						XmlElement value2 = item as XmlElement;
						EncryptedKey encryptedKey2 = new EncryptedKey();
						encryptedKey2.LoadXml(value2);
						if (encryptedKey2.CarriedKeyName == value && encryptedKey2.Recipient == Recipient)
						{
							encryptedKey = encryptedKey2;
							break;
						}
					}
					break;
				}
				if (enumerator.Current is KeyInfoRetrievalMethod keyInfoRetrievalMethod)
				{
					string idValue = Utils.ExtractIdFromLocalUri(keyInfoRetrievalMethod.Uri);
					encryptedKey = new EncryptedKey();
					encryptedKey.LoadXml(GetIdElement(_document, idValue));
					break;
				}
				if (enumerator.Current is KeyInfoEncryptedKey keyInfoEncryptedKey)
				{
					encryptedKey = keyInfoEncryptedKey.EncryptedKey;
					break;
				}
			}
			if (encryptedKey != null)
			{
				if (symmetricAlgorithmUri == null)
				{
					if (encryptedData.EncryptionMethod == null)
					{
						throw new CryptographicException("Symmetric algorithm is not specified.");
					}
					symmetricAlgorithmUri = encryptedData.EncryptionMethod.KeyAlgorithm;
				}
				byte[] array = DecryptEncryptedKey(encryptedKey);
				if (array == null)
				{
					throw new CryptographicException("Unable to retrieve the decryption key.");
				}
				SymmetricAlgorithm obj = CryptoHelpers.CreateFromName<SymmetricAlgorithm>(symmetricAlgorithmUri) ?? throw new CryptographicException("Symmetric algorithm is not specified.");
				obj.Key = array;
				return obj;
			}
			return null;
		}

		/// <summary>Determines the key represented by the <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> element.</summary>
		/// <param name="encryptedKey">The <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object that contains the key to retrieve.</param>
		/// <returns>A byte array that contains the key.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedKey" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="encryptedKey" /> parameter is not the Triple DES Key Wrap algorithm or the Advanced Encryption Standard (AES) Key Wrap algorithm (also called Rijndael).</exception>
		public virtual byte[] DecryptEncryptedKey(EncryptedKey encryptedKey)
		{
			if (encryptedKey == null)
			{
				throw new ArgumentNullException("encryptedKey");
			}
			if (encryptedKey.KeyInfo == null)
			{
				return null;
			}
			IEnumerator enumerator = encryptedKey.KeyInfo.GetEnumerator();
			EncryptedKey encryptedKey2 = null;
			bool flag = false;
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is KeyInfoName { Value: var value })
				{
					object obj = _keyNameMapping[value];
					if (obj == null)
					{
						break;
					}
					if (encryptedKey.CipherData == null || encryptedKey.CipherData.CipherValue == null)
					{
						throw new CryptographicException("Symmetric algorithm is not specified.");
					}
					if (obj is SymmetricAlgorithm)
					{
						return DecryptKey(encryptedKey.CipherData.CipherValue, (SymmetricAlgorithm)obj);
					}
					flag = encryptedKey.EncryptionMethod != null && encryptedKey.EncryptionMethod.KeyAlgorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";
					return DecryptKey(encryptedKey.CipherData.CipherValue, (RSA)obj, flag);
				}
				if (enumerator.Current is KeyInfoX509Data keyInfoX509Data)
				{
					X509Certificate2Enumerator enumerator2 = Utils.BuildBagOfCerts(keyInfoX509Data, CertUsageType.Decryption).GetEnumerator();
					while (enumerator2.MoveNext())
					{
						using RSA rSA = enumerator2.Current.GetRSAPrivateKey();
						if (rSA != null)
						{
							if (encryptedKey.CipherData == null || encryptedKey.CipherData.CipherValue == null)
							{
								throw new CryptographicException("Symmetric algorithm is not specified.");
							}
							flag = encryptedKey.EncryptionMethod != null && encryptedKey.EncryptionMethod.KeyAlgorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";
							return DecryptKey(encryptedKey.CipherData.CipherValue, rSA, flag);
						}
					}
					break;
				}
				if (enumerator.Current is KeyInfoRetrievalMethod keyInfoRetrievalMethod)
				{
					string idValue = Utils.ExtractIdFromLocalUri(keyInfoRetrievalMethod.Uri);
					encryptedKey2 = new EncryptedKey();
					encryptedKey2.LoadXml(GetIdElement(_document, idValue));
					try
					{
						_xmlDsigSearchDepthCounter++;
						if (IsOverXmlDsigRecursionLimit())
						{
							throw new CryptoSignedXmlRecursionException();
						}
						return DecryptEncryptedKey(encryptedKey2);
					}
					finally
					{
						_xmlDsigSearchDepthCounter--;
					}
				}
				if (!(enumerator.Current is KeyInfoEncryptedKey { EncryptedKey: var encryptedKey3 }))
				{
					continue;
				}
				byte[] array = DecryptEncryptedKey(encryptedKey3);
				if (array != null)
				{
					SymmetricAlgorithm symmetricAlgorithm = CryptoHelpers.CreateFromName<SymmetricAlgorithm>(encryptedKey.EncryptionMethod.KeyAlgorithm);
					if (symmetricAlgorithm == null)
					{
						throw new CryptographicException("Symmetric algorithm is not specified.");
					}
					symmetricAlgorithm.Key = array;
					if (encryptedKey.CipherData == null || encryptedKey.CipherData.CipherValue == null)
					{
						throw new CryptographicException("Symmetric algorithm is not specified.");
					}
					symmetricAlgorithm.Key = array;
					return DecryptKey(encryptedKey.CipherData.CipherValue, symmetricAlgorithm);
				}
			}
			return null;
		}

		/// <summary>Defines a mapping between a key name and a symmetric key or an asymmetric key.</summary>
		/// <param name="keyName">The name to map to <paramref name="keyObject" />.</param>
		/// <param name="keyObject">The symmetric key to map to <paramref name="keyName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyName" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="keyObject" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="keyObject" /> parameter is not an RSA algorithm or a symmetric key.</exception>
		public void AddKeyNameMapping(string keyName, object keyObject)
		{
			if (keyName == null)
			{
				throw new ArgumentNullException("keyName");
			}
			if (keyObject == null)
			{
				throw new ArgumentNullException("keyObject");
			}
			if (!(keyObject is SymmetricAlgorithm) && !(keyObject is RSA))
			{
				throw new CryptographicException("The specified cryptographic transform is not supported.");
			}
			_keyNameMapping.Add(keyName, keyObject);
		}

		/// <summary>Resets all key name mapping.</summary>
		public void ClearKeyNameMappings()
		{
			_keyNameMapping.Clear();
		}

		/// <summary>Encrypts the outer XML of an element using the specified X.509 certificate.</summary>
		/// <param name="inputElement">The XML element to encrypt.</param>
		/// <param name="certificate">The X.509 certificate to use for encryption.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> element that represents the encrypted XML data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="certificate" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The value of the <paramref name="certificate" /> parameter does not represent an RSA key algorithm.</exception>
		public EncryptedData Encrypt(XmlElement inputElement, X509Certificate2 certificate)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			using RSA rSA = certificate.GetRSAPublicKey();
			if (rSA == null)
			{
				throw new NotSupportedException("The certificate key algorithm is not supported.");
			}
			EncryptedData obj = new EncryptedData
			{
				Type = "http://www.w3.org/2001/04/xmlenc#Element",
				EncryptionMethod = new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#aes256-cbc")
			};
			EncryptedKey encryptedKey = new EncryptedKey();
			encryptedKey.EncryptionMethod = new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#rsa-1_5");
			encryptedKey.KeyInfo.AddClause(new KeyInfoX509Data(certificate));
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			encryptedKey.CipherData.CipherValue = EncryptKey(rijndaelManaged.Key, rSA, useOAEP: false);
			KeyInfoEncryptedKey clause = new KeyInfoEncryptedKey(encryptedKey);
			obj.KeyInfo.AddClause(clause);
			obj.CipherData.CipherValue = EncryptData(inputElement, rijndaelManaged, content: false);
			return obj;
		}

		/// <summary>Encrypts the outer XML of an element using the specified key in the key mapping table.</summary>
		/// <param name="inputElement">The XML element to encrypt.</param>
		/// <param name="keyName">A key name that can be found in the key mapping table.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object that represents the encrypted XML data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="keyName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="keyName" /> parameter does not match a registered key name pair.  
		///  -or-  
		///  The cryptographic key described by the <paramref name="keyName" /> parameter is not supported.</exception>
		public EncryptedData Encrypt(XmlElement inputElement, string keyName)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (keyName == null)
			{
				throw new ArgumentNullException("keyName");
			}
			object obj = null;
			if (_keyNameMapping != null)
			{
				obj = _keyNameMapping[keyName];
			}
			if (obj == null)
			{
				throw new CryptographicException("Unable to retrieve the encryption key.");
			}
			SymmetricAlgorithm symmetricAlgorithm = obj as SymmetricAlgorithm;
			RSA rsa = obj as RSA;
			EncryptedData encryptedData = new EncryptedData();
			encryptedData.Type = "http://www.w3.org/2001/04/xmlenc#Element";
			encryptedData.EncryptionMethod = new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#aes256-cbc");
			string algorithm = null;
			if (symmetricAlgorithm == null)
			{
				algorithm = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";
			}
			else if (symmetricAlgorithm is TripleDES)
			{
				algorithm = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";
			}
			else
			{
				if (!(symmetricAlgorithm is Rijndael) && !(symmetricAlgorithm is Aes))
				{
					throw new CryptographicException("The specified cryptographic transform is not supported.");
				}
				switch (symmetricAlgorithm.KeySize)
				{
				case 128:
					algorithm = "http://www.w3.org/2001/04/xmlenc#kw-aes128";
					break;
				case 192:
					algorithm = "http://www.w3.org/2001/04/xmlenc#kw-aes192";
					break;
				case 256:
					algorithm = "http://www.w3.org/2001/04/xmlenc#kw-aes256";
					break;
				}
			}
			EncryptedKey encryptedKey = new EncryptedKey();
			encryptedKey.EncryptionMethod = new EncryptionMethod(algorithm);
			encryptedKey.KeyInfo.AddClause(new KeyInfoName(keyName));
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			encryptedKey.CipherData.CipherValue = ((symmetricAlgorithm == null) ? EncryptKey(rijndaelManaged.Key, rsa, useOAEP: false) : EncryptKey(rijndaelManaged.Key, symmetricAlgorithm));
			KeyInfoEncryptedKey clause = new KeyInfoEncryptedKey(encryptedKey);
			encryptedData.KeyInfo.AddClause(clause);
			encryptedData.CipherData.CipherValue = EncryptData(inputElement, rijndaelManaged, content: false);
			return encryptedData;
		}

		/// <summary>Decrypts all <see langword="&lt;EncryptedData&gt;" /> elements of the XML document that were specified during initialization of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic key used to decrypt the document was not found.</exception>
		public void DecryptDocument()
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(_document.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			XmlNodeList xmlNodeList = _document.SelectNodes("//enc:EncryptedData", xmlNamespaceManager);
			if (xmlNodeList == null)
			{
				return;
			}
			foreach (XmlNode item in xmlNodeList)
			{
				XmlElement xmlElement = item as XmlElement;
				EncryptedData encryptedData = new EncryptedData();
				encryptedData.LoadXml(xmlElement);
				SymmetricAlgorithm decryptionKey = GetDecryptionKey(encryptedData, null);
				if (decryptionKey == null)
				{
					throw new CryptographicException("Unable to retrieve the decryption key.");
				}
				byte[] decryptedData = DecryptData(encryptedData, decryptionKey);
				ReplaceData(xmlElement, decryptedData);
			}
		}

		/// <summary>Encrypts data in the specified byte array using the specified symmetric algorithm.</summary>
		/// <param name="plaintext">The data to encrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric algorithm to use for encryption.</param>
		/// <returns>A byte array of encrypted data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="plaintext" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="symmetricAlgorithm" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The data could not be encrypted using the specified parameters.</exception>
		public byte[] EncryptData(byte[] plaintext, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (plaintext == null)
			{
				throw new ArgumentNullException("plaintext");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			CipherMode mode = symmetricAlgorithm.Mode;
			PaddingMode padding = symmetricAlgorithm.Padding;
			byte[] array = null;
			try
			{
				symmetricAlgorithm.Mode = _mode;
				symmetricAlgorithm.Padding = _padding;
				array = symmetricAlgorithm.CreateEncryptor().TransformFinalBlock(plaintext, 0, plaintext.Length);
			}
			finally
			{
				symmetricAlgorithm.Mode = mode;
				symmetricAlgorithm.Padding = padding;
			}
			byte[] array2 = null;
			if (_mode == CipherMode.ECB)
			{
				array2 = array;
			}
			else
			{
				byte[] iV = symmetricAlgorithm.IV;
				array2 = new byte[array.Length + iV.Length];
				Buffer.BlockCopy(iV, 0, array2, 0, iV.Length);
				Buffer.BlockCopy(array, 0, array2, iV.Length, array.Length);
			}
			return array2;
		}

		/// <summary>Encrypts the specified element or its contents using the specified symmetric algorithm.</summary>
		/// <param name="inputElement">The element or its contents to encrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric algorithm to use for encryption.</param>
		/// <param name="content">
		///   <see langword="true" /> to encrypt only the contents of the element; <see langword="false" /> to encrypt the entire element.</param>
		/// <returns>A byte array that contains the encrypted data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="symmetricAlgorithm" /> parameter is <see langword="null" />.</exception>
		public byte[] EncryptData(XmlElement inputElement, SymmetricAlgorithm symmetricAlgorithm, bool content)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			byte[] plaintext = (content ? _encoding.GetBytes(inputElement.InnerXml) : _encoding.GetBytes(inputElement.OuterXml));
			return EncryptData(plaintext, symmetricAlgorithm);
		}

		/// <summary>Decrypts an <see langword="&lt;EncryptedData&gt;" /> element using the specified symmetric algorithm.</summary>
		/// <param name="encryptedData">The data to decrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric key used to decrypt <paramref name="encryptedData" />.</param>
		/// <returns>A byte array that contains the raw decrypted plain text.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedData" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="symmetricAlgorithm" /> parameter is <see langword="null" />.</exception>
		public byte[] DecryptData(EncryptedData encryptedData, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			byte[] cipherValue = GetCipherValue(encryptedData.CipherData);
			CipherMode mode = symmetricAlgorithm.Mode;
			PaddingMode padding = symmetricAlgorithm.Padding;
			byte[] iV = symmetricAlgorithm.IV;
			byte[] array = null;
			if (_mode != CipherMode.ECB)
			{
				array = GetDecryptionIV(encryptedData, null);
			}
			byte[] array2 = null;
			try
			{
				int num = 0;
				if (array != null)
				{
					symmetricAlgorithm.IV = array;
					num = array.Length;
				}
				symmetricAlgorithm.Mode = _mode;
				symmetricAlgorithm.Padding = _padding;
				return symmetricAlgorithm.CreateDecryptor().TransformFinalBlock(cipherValue, num, cipherValue.Length - num);
			}
			finally
			{
				symmetricAlgorithm.Mode = mode;
				symmetricAlgorithm.Padding = padding;
				symmetricAlgorithm.IV = iV;
			}
		}

		/// <summary>Replaces an <see langword="&lt;EncryptedData&gt;" /> element with a specified decrypted sequence of bytes.</summary>
		/// <param name="inputElement">The <see langword="&lt;EncryptedData&gt;" /> element to replace.</param>
		/// <param name="decryptedData">The decrypted data to replace <paramref name="inputElement" /> with.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="decryptedData" /> parameter is <see langword="null" />.</exception>
		public void ReplaceData(XmlElement inputElement, byte[] decryptedData)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (decryptedData == null)
			{
				throw new ArgumentNullException("decryptedData");
			}
			XmlNode parentNode = inputElement.ParentNode;
			if (parentNode.NodeType == XmlNodeType.Document)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				using (StringReader input = new StringReader(_encoding.GetString(decryptedData)))
				{
					using XmlReader reader = XmlReader.Create(input, Utils.GetSecureXmlReaderSettings(_xmlResolver));
					xmlDocument.Load(reader);
				}
				XmlNode newChild = inputElement.OwnerDocument.ImportNode(xmlDocument.DocumentElement, deep: true);
				parentNode.RemoveChild(inputElement);
				parentNode.AppendChild(newChild);
				return;
			}
			XmlNode xmlNode = parentNode.OwnerDocument.CreateElement(parentNode.Prefix, parentNode.LocalName, parentNode.NamespaceURI);
			try
			{
				parentNode.AppendChild(xmlNode);
				xmlNode.InnerXml = _encoding.GetString(decryptedData);
				XmlNode xmlNode2 = xmlNode.FirstChild;
				XmlNode nextSibling = inputElement.NextSibling;
				while (xmlNode2 != null)
				{
					XmlNode nextSibling2 = xmlNode2.NextSibling;
					parentNode.InsertBefore(xmlNode2, nextSibling);
					xmlNode2 = nextSibling2;
				}
			}
			finally
			{
				parentNode.RemoveChild(xmlNode);
			}
			parentNode.RemoveChild(inputElement);
		}

		/// <summary>Replaces the specified element with the specified <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <param name="inputElement">The element to replace with an <see langword="&lt;EncryptedData&gt;" /> element.</param>
		/// <param name="encryptedData">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object to replace the <paramref name="inputElement" /> parameter with.</param>
		/// <param name="content">
		///   <see langword="true" /> to replace only the contents of the element; <see langword="false" /> to replace the entire element.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="encryptedData" /> parameter is <see langword="null" />.</exception>
		public static void ReplaceElement(XmlElement inputElement, EncryptedData encryptedData, bool content)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			XmlElement xml = encryptedData.GetXml(inputElement.OwnerDocument);
			if (content)
			{
				Utils.RemoveAllChildren(inputElement);
				inputElement.AppendChild(xml);
			}
			else
			{
				inputElement.ParentNode.ReplaceChild(xml, inputElement);
			}
		}

		/// <summary>Encrypts a key using a symmetric algorithm that a recipient uses to decrypt an <see langword="&lt;EncryptedData&gt;" /> element.</summary>
		/// <param name="keyData">The key to encrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric key used to encrypt <paramref name="keyData" />.</param>
		/// <returns>A byte array that represents the encrypted value of the <paramref name="keyData" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="symmetricAlgorithm" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="symmetricAlgorithm" /> parameter is not the Triple DES Key Wrap algorithm or the Advanced Encryption Standard (AES) Key Wrap algorithm (also called Rijndael).</exception>
		public static byte[] EncryptKey(byte[] keyData, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			if (symmetricAlgorithm is TripleDES)
			{
				return SymmetricKeyWrap.TripleDESKeyWrapEncrypt(symmetricAlgorithm.Key, keyData);
			}
			if (symmetricAlgorithm is Rijndael || symmetricAlgorithm is Aes)
			{
				return SymmetricKeyWrap.AESKeyWrapEncrypt(symmetricAlgorithm.Key, keyData);
			}
			throw new CryptographicException("The specified cryptographic transform is not supported.");
		}

		/// <summary>Encrypts the key that a recipient uses to decrypt an <see langword="&lt;EncryptedData&gt;" /> element.</summary>
		/// <param name="keyData">The key to encrypt.</param>
		/// <param name="rsa">The asymmetric key used to encrypt <paramref name="keyData" />.</param>
		/// <param name="useOAEP">A value that specifies whether to use Optimal Asymmetric Encryption Padding (OAEP).</param>
		/// <returns>A byte array that represents the encrypted value of the <paramref name="keyData" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="rsa" /> parameter is <see langword="null" />.</exception>
		public static byte[] EncryptKey(byte[] keyData, RSA rsa, bool useOAEP)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			if (useOAEP)
			{
				return new RSAOAEPKeyExchangeFormatter(rsa).CreateKeyExchange(keyData);
			}
			return new RSAPKCS1KeyExchangeFormatter(rsa).CreateKeyExchange(keyData);
		}

		/// <summary>Decrypts an <see langword="&lt;EncryptedKey&gt;" /> element using a symmetric algorithm.</summary>
		/// <param name="keyData">An array of bytes that represents an encrypted <see langword="&lt;EncryptedKey&gt;" /> element.</param>
		/// <param name="symmetricAlgorithm">The symmetric key used to decrypt <paramref name="keyData" />.</param>
		/// <returns>A byte array that contains the plain text key.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="symmetricAlgorithm" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="symmetricAlgorithm" /> element is not the Triple DES Key Wrap algorithm or the Advanced Encryption Standard (AES) Key Wrap algorithm (also called Rijndael).</exception>
		public static byte[] DecryptKey(byte[] keyData, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			if (symmetricAlgorithm is TripleDES)
			{
				return SymmetricKeyWrap.TripleDESKeyWrapDecrypt(symmetricAlgorithm.Key, keyData);
			}
			if (symmetricAlgorithm is Rijndael || symmetricAlgorithm is Aes)
			{
				return SymmetricKeyWrap.AESKeyWrapDecrypt(symmetricAlgorithm.Key, keyData);
			}
			throw new CryptographicException("The specified cryptographic transform is not supported.");
		}

		/// <summary>Decrypts an <see langword="&lt;EncryptedKey&gt;" /> element using an asymmetric algorithm.</summary>
		/// <param name="keyData">An array of bytes that represents an encrypted <see langword="&lt;EncryptedKey&gt;" /> element.</param>
		/// <param name="rsa">The asymmetric key used to decrypt <paramref name="keyData" />.</param>
		/// <param name="useOAEP">A value that specifies whether to use Optimal Asymmetric Encryption Padding (OAEP).</param>
		/// <returns>A byte array that contains the plain text key.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="rsa" /> parameter is <see langword="null" />.</exception>
		public static byte[] DecryptKey(byte[] keyData, RSA rsa, bool useOAEP)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			if (useOAEP)
			{
				return new RSAOAEPKeyExchangeDeformatter(rsa).DecryptKeyExchange(keyData);
			}
			return new RSAPKCS1KeyExchangeDeformatter(rsa).DecryptKeyExchange(keyData);
		}
	}
	/// <summary>Encapsulates the encryption algorithm used for XML encryption.</summary>
	public class EncryptionMethod
	{
		private XmlElement _cachedXml;

		private int _keySize;

		private string _algorithm;

		private bool CacheValid => _cachedXml != null;

		/// <summary>Gets or sets the algorithm key size used for XML encryption.</summary>
		/// <returns>The algorithm key size, in bits, used for XML encryption.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Security.Cryptography.Xml.EncryptionMethod.KeySize" /> property was set to a value that was less than 0.</exception>
		public int KeySize
		{
			get
			{
				return _keySize;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", "The key size should be a non negative integer.");
				}
				_keySize = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets a Uniform Resource Identifier (URI) that describes the algorithm to use for XML encryption.</summary>
		/// <returns>A Uniform Resource Identifier (URI) that describes the algorithm to use for XML encryption.</returns>
		public string KeyAlgorithm
		{
			get
			{
				return _algorithm;
			}
			set
			{
				_algorithm = value;
				_cachedXml = null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class.</summary>
		public EncryptionMethod()
		{
			_cachedXml = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class specifying an algorithm Uniform Resource Identifier (URI).</summary>
		/// <param name="algorithm">The Uniform Resource Identifier (URI) that describes the algorithm represented by an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class.</param>
		public EncryptionMethod(string algorithm)
		{
			_algorithm = algorithm;
			_cachedXml = null;
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlElement" /> object that encapsulates an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that encapsulates an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class.</returns>
		public XmlElement GetXml()
		{
			if (CacheValid)
			{
				return _cachedXml;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("EncryptionMethod", "http://www.w3.org/2001/04/xmlenc#");
			if (!string.IsNullOrEmpty(_algorithm))
			{
				xmlElement.SetAttribute("Algorithm", _algorithm);
			}
			if (_keySize > 0)
			{
				XmlElement xmlElement2 = document.CreateElement("KeySize", "http://www.w3.org/2001/04/xmlenc#");
				xmlElement2.AppendChild(document.CreateTextNode(_keySize.ToString(null, null)));
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> object to match.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object to parse.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The key size expressed in the <paramref name="value" /> parameter was less than 0.</exception>
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			_algorithm = Utils.GetAttribute(value, "Algorithm", "http://www.w3.org/2001/04/xmlenc#");
			XmlNode xmlNode = value.SelectSingleNode("enc:KeySize", xmlNamespaceManager);
			if (xmlNode != null)
			{
				KeySize = Convert.ToInt32(Utils.DiscardWhiteSpaces(xmlNode.InnerText), null);
			}
			_cachedXml = value;
		}
	}
	/// <summary>Represents the <see langword="&lt;EncryptionProperty&gt;" /> element used in XML encryption. This class cannot be inherited.</summary>
	public sealed class EncryptionProperty
	{
		private string _target;

		private string _id;

		private XmlElement _elemProp;

		private XmlElement _cachedXml;

		/// <summary>Gets the ID of the current <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</returns>
		public string Id => _id;

		/// <summary>Gets the target of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</summary>
		/// <returns>The target of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</returns>
		public string Target => _target;

		/// <summary>Gets or sets an <see cref="T:System.Xml.XmlElement" /> object that represents an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.EncryptionProperty.PropertyElement" /> property was set to <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Xml.XmlElement.LocalName" /> property of the value set to the <see cref="P:System.Security.Cryptography.Xml.EncryptionProperty.PropertyElement" /> property is not "EncryptionProperty".  
		///  -or-  
		///  The <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property of the value set to the <see cref="P:System.Security.Cryptography.Xml.EncryptionProperty.PropertyElement" /> property is not "http://www.w3.org/2001/04/xmlenc#".</exception>
		public XmlElement PropertyElement
		{
			get
			{
				return _elemProp;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.LocalName != "EncryptionProperty" || value.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
				{
					throw new CryptographicException("Malformed encryption property element.");
				}
				_elemProp = value;
				_cachedXml = null;
			}
		}

		private bool CacheValid => _cachedXml != null;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class.</summary>
		public EncryptionProperty()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class using an <see cref="T:System.Xml.XmlElement" /> object.</summary>
		/// <param name="elementProperty">An <see cref="T:System.Xml.XmlElement" /> object to use for initialization.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elementProperty" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Xml.XmlElement.LocalName" /> property of the <paramref name="elementProperty" /> parameter is not "EncryptionProperty".  
		///  -or-  
		///  The <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property of the <paramref name="elementProperty" /> parameter is not "http://www.w3.org/2001/04/xmlenc#".</exception>
		public EncryptionProperty(XmlElement elementProperty)
		{
			if (elementProperty == null)
			{
				throw new ArgumentNullException("elementProperty");
			}
			if (elementProperty.LocalName != "EncryptionProperty" || elementProperty.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
			{
				throw new CryptographicException("Malformed encryption property element.");
			}
			_elemProp = elementProperty;
			_cachedXml = null;
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlElement" /> object that encapsulates an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that encapsulates an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class.</returns>
		public XmlElement GetXml()
		{
			if (CacheValid)
			{
				return _cachedXml;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal XmlElement GetXml(XmlDocument document)
		{
			return document.ImportNode(_elemProp, deep: true) as XmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to match.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object to parse.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Xml.XmlElement.LocalName" /> property of the <paramref name="value" /> parameter is not "EncryptionProperty".  
		///  -or-  
		///  The <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property of the <paramref name="value" /> parameter is not "http://www.w3.org/2001/04/xmlenc#".</exception>
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "EncryptionProperty" || value.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
			{
				throw new CryptographicException("Malformed encryption property element.");
			}
			_cachedXml = value;
			_id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2001/04/xmlenc#");
			_target = Utils.GetAttribute(value, "Target", "http://www.w3.org/2001/04/xmlenc#");
			_elemProp = value;
		}
	}
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> classes used in XML encryption. This class cannot be inherited.</summary>
	public sealed class EncryptionPropertyCollection : IList, ICollection, IEnumerable
	{
		private ArrayList _props;

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		public int Count => _props.Count;

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object has a fixed size; otherwise, <see langword="false" />.</returns>
		public bool IsFixedSize => _props.IsFixedSize;

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is read-only; otherwise, <see langword="false" />.</returns>
		public bool IsReadOnly => _props.IsReadOnly;

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to return.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</returns>
		[IndexerName("ItemOf")]
		public EncryptionProperty this[int index]
		{
			get
			{
				return (EncryptionProperty)((IList)this)[index];
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <param name="index">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The element at the specified index.</returns>
		object IList.this[int index]
		{
			get
			{
				return _props[index];
			}
			set
			{
				if (!(value is EncryptionProperty))
				{
					throw new ArgumentException("Type of input object is invalid.", "value");
				}
				_props[index] = value;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		public object SyncRoot => _props.SyncRoot;

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		public bool IsSynchronized => _props.IsSynchronized;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> class.</summary>
		public EncryptionPropertyCollection()
		{
			_props = new ArrayList();
		}

		/// <summary>Returns an enumerator that iterates through an <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through an <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		public IEnumerator GetEnumerator()
		{
			return _props.GetEnumerator();
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		int IList.Add(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			return _props.Add(value);
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <param name="value">An <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to add to the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		/// <returns>The position at which the new element is inserted.</returns>
		public int Add(EncryptionProperty value)
		{
			return _props.Add(value);
		}

		/// <summary>Removes all items from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		public void Clear()
		{
			_props.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		bool IList.Contains(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			return _props.Contains(value);
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object contains a specific <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to locate in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object is found in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object; otherwise, <see langword="false" />.</returns>
		public bool Contains(EncryptionProperty value)
		{
			return _props.Contains(value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		int IList.IndexOf(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			return _props.IndexOf(value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to locate in the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		/// <returns>The index of <paramref name="value" /> if found in the collection; otherwise, -1.</returns>
		public int IndexOf(EncryptionProperty value)
		{
			return _props.IndexOf(value);
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		void IList.Insert(int index, object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			_props.Insert(index, value);
		}

		/// <summary>Inserts an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object into the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object at the specified position.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">An <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to insert into the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		public void Insert(int index, EncryptionProperty value)
		{
			_props.Insert(index, value);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> uses an incorrect object type.</exception>
		void IList.Remove(object value)
		{
			if (!(value is EncryptionProperty))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			_props.Remove(value);
		}

		/// <summary>Removes the first occurrence of a specific <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to remove from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</param>
		public void Remove(EncryptionProperty value)
		{
			_props.Remove(value);
		}

		/// <summary>Removes the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to remove.</param>
		public void RemoveAt(int index)
		{
			_props.RemoveAt(index);
		}

		/// <summary>Returns the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to return.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object at the specified index.</returns>
		public EncryptionProperty Item(int index)
		{
			return (EncryptionProperty)_props[index];
		}

		/// <summary>Copies the elements of the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object to an array, starting at a particular array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the elements copied from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		public void CopyTo(Array array, int index)
		{
			_props.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object to an array of <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> objects, starting at a particular array index.</summary>
		/// <param name="array">The one-dimensional array of  <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> objects that is the destination of the elements copied from the <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		public void CopyTo(EncryptionProperty[] array, int index)
		{
			_props.CopyTo(array, index);
		}
	}
	internal class ExcAncestralNamespaceContextManager : AncestralNamespaceContextManager
	{
		private Hashtable _inclusivePrefixSet;

		internal ExcAncestralNamespaceContextManager(string inclusiveNamespacesPrefixList)
		{
			_inclusivePrefixSet = Utils.TokenizePrefixListString(inclusiveNamespacesPrefixList);
		}

		private bool HasNonRedundantInclusivePrefix(XmlAttribute attr)
		{
			string namespacePrefix = Utils.GetNamespacePrefix(attr);
			int depth;
			if (_inclusivePrefixSet.ContainsKey(namespacePrefix))
			{
				return Utils.IsNonRedundantNamespaceDecl(attr, GetNearestRenderedNamespaceWithMatchingPrefix(namespacePrefix, out depth));
			}
			return false;
		}

		private void GatherNamespaceToRender(string nsPrefix, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			foreach (XmlAttribute key in nsListToRender.GetKeyList())
			{
				if (Utils.HasNamespacePrefix(key, nsPrefix))
				{
					return;
				}
			}
			XmlAttribute xmlAttribute = (XmlAttribute)nsLocallyDeclared[nsPrefix];
			int depth;
			XmlAttribute nearestRenderedNamespaceWithMatchingPrefix = GetNearestRenderedNamespaceWithMatchingPrefix(nsPrefix, out depth);
			if (xmlAttribute != null)
			{
				if (Utils.IsNonRedundantNamespaceDecl(xmlAttribute, nearestRenderedNamespaceWithMatchingPrefix))
				{
					nsLocallyDeclared.Remove(nsPrefix);
					nsListToRender.Add(xmlAttribute, null);
				}
			}
			else
			{
				int depth2;
				XmlAttribute nearestUnrenderedNamespaceWithMatchingPrefix = GetNearestUnrenderedNamespaceWithMatchingPrefix(nsPrefix, out depth2);
				if (nearestUnrenderedNamespaceWithMatchingPrefix != null && depth2 > depth && Utils.IsNonRedundantNamespaceDecl(nearestUnrenderedNamespaceWithMatchingPrefix, nearestRenderedNamespaceWithMatchingPrefix))
				{
					nsListToRender.Add(nearestUnrenderedNamespaceWithMatchingPrefix, null);
				}
			}
		}

		internal override void GetNamespacesToRender(XmlElement element, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			GatherNamespaceToRender(element.Prefix, nsListToRender, nsLocallyDeclared);
			foreach (XmlAttribute key in attrListToRender.GetKeyList())
			{
				string prefix = key.Prefix;
				if (prefix.Length > 0)
				{
					GatherNamespaceToRender(prefix, nsListToRender, nsLocallyDeclared);
				}
			}
		}

		internal override void TrackNamespaceNode(XmlAttribute attr, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			if (!Utils.IsXmlPrefixDefinitionNode(attr))
			{
				if (HasNonRedundantInclusivePrefix(attr))
				{
					nsListToRender.Add(attr, null);
				}
				else
				{
					nsLocallyDeclared.Add(Utils.GetNamespacePrefix(attr), attr);
				}
			}
		}

		internal override void TrackXmlNamespaceNode(XmlAttribute attr, SortedList nsListToRender, SortedList attrListToRender, Hashtable nsLocallyDeclared)
		{
			attrListToRender.Add(attr, null);
		}
	}
	internal class ExcCanonicalXml
	{
		private CanonicalXmlDocument _c14nDoc;

		private ExcAncestralNamespaceContextManager _ancMgr;

		internal ExcCanonicalXml(Stream inputStream, bool includeComments, string inclusiveNamespacesPrefixList, XmlResolver resolver, string strBaseUri)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			_c14nDoc = new CanonicalXmlDocument(defaultNodeSetInclusionState: true, includeComments);
			_c14nDoc.XmlResolver = resolver;
			_c14nDoc.Load(Utils.PreProcessStreamInput(inputStream, resolver, strBaseUri));
			_ancMgr = new ExcAncestralNamespaceContextManager(inclusiveNamespacesPrefixList);
		}

		internal ExcCanonicalXml(XmlDocument document, bool includeComments, string inclusiveNamespacesPrefixList, XmlResolver resolver)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			_c14nDoc = new CanonicalXmlDocument(defaultNodeSetInclusionState: true, includeComments);
			_c14nDoc.XmlResolver = resolver;
			_c14nDoc.Load(new XmlNodeReader(document));
			_ancMgr = new ExcAncestralNamespaceContextManager(inclusiveNamespacesPrefixList);
		}

		internal ExcCanonicalXml(XmlNodeList nodeList, bool includeComments, string inclusiveNamespacesPrefixList, XmlResolver resolver)
		{
			if (nodeList == null)
			{
				throw new ArgumentNullException("nodeList");
			}
			XmlDocument ownerDocument = Utils.GetOwnerDocument(nodeList);
			if (ownerDocument == null)
			{
				throw new ArgumentException("nodeList");
			}
			_c14nDoc = new CanonicalXmlDocument(defaultNodeSetInclusionState: false, includeComments);
			_c14nDoc.XmlResolver = resolver;
			_c14nDoc.Load(new XmlNodeReader(ownerDocument));
			_ancMgr = new ExcAncestralNamespaceContextManager(inclusiveNamespacesPrefixList);
			MarkInclusionStateForNodes(nodeList, ownerDocument, _c14nDoc);
		}

		internal byte[] GetBytes()
		{
			StringBuilder stringBuilder = new StringBuilder();
			_c14nDoc.Write(stringBuilder, DocPosition.BeforeRootElement, _ancMgr);
			return new UTF8Encoding(encoderShouldEmitUTF8Identifier: false).GetBytes(stringBuilder.ToString());
		}

		internal byte[] GetDigestedBytes(HashAlgorithm hash)
		{
			_c14nDoc.WriteHash(hash, DocPosition.BeforeRootElement, _ancMgr);
			hash.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
			byte[] result = (byte[])hash.Hash.Clone();
			hash.Initialize();
			return result;
		}

		private static void MarkInclusionStateForNodes(XmlNodeList nodeList, XmlDocument inputRoot, XmlDocument root)
		{
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			CanonicalXmlNodeList canonicalXmlNodeList2 = new CanonicalXmlNodeList();
			canonicalXmlNodeList.Add(inputRoot);
			canonicalXmlNodeList2.Add(root);
			int num = 0;
			do
			{
				XmlNode xmlNode = canonicalXmlNodeList[num];
				XmlNode xmlNode2 = canonicalXmlNodeList2[num];
				XmlNodeList childNodes = xmlNode.ChildNodes;
				XmlNodeList childNodes2 = xmlNode2.ChildNodes;
				for (int i = 0; i < childNodes.Count; i++)
				{
					canonicalXmlNodeList.Add(childNodes[i]);
					canonicalXmlNodeList2.Add(childNodes2[i]);
					if (Utils.NodeInList(childNodes[i], nodeList))
					{
						MarkNodeAsIncluded(childNodes2[i]);
					}
					XmlAttributeCollection attributes = childNodes[i].Attributes;
					if (attributes == null)
					{
						continue;
					}
					for (int j = 0; j < attributes.Count; j++)
					{
						if (Utils.NodeInList(attributes[j], nodeList))
						{
							MarkNodeAsIncluded(childNodes2[i].Attributes.Item(j));
						}
					}
				}
				num++;
			}
			while (num < canonicalXmlNodeList.Count);
		}

		private static void MarkNodeAsIncluded(XmlNode node)
		{
			if (node is ICanonicalizableNode)
			{
				((ICanonicalizableNode)node).IsInNodeSet = true;
			}
		}
	}
	internal interface ICanonicalizableNode
	{
		bool IsInNodeSet { get; set; }

		void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc);

		void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc);
	}
	/// <summary>Defines methods that decrypt an XrML <see langword="&lt;encryptedGrant&gt;" /> element.</summary>
	public interface IRelDecryptor
	{
		/// <summary>Decrypts an XrML <see langword="&lt;encryptedGrant&gt;" /> element that is contained within a <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="encryptionMethod">An <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> object that encapsulates the algorithm used for XML encryption.</param>
		/// <param name="keyInfo">A <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object that contains an asymmetric key to use for decryption.</param>
		/// <param name="toDecrypt">A stream object that contains an <see langword="&lt;encryptedGrant&gt;" /> element to decrypt.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that contains a decrypted <see langword="&lt;encryptedGrant&gt;" /> element.</returns>
		Stream Decrypt(EncryptionMethod encryptionMethod, KeyInfo keyInfo, Stream toDecrypt);
	}
	/// <summary>Represents an XML digital signature or XML encryption <see langword="&lt;KeyInfo&gt;" /> element.</summary>
	public class KeyInfo : IEnumerable
	{
		private string _id;

		private ArrayList _keyInfoClauses;

		/// <summary>Gets or sets the key information identity.</summary>
		/// <returns>The key information identity.</returns>
		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <returns>The number of <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</returns>
		public int Count => _keyInfoClauses.Count;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> class.</summary>
		public KeyInfo()
		{
			_keyInfoClauses = new ArrayList();
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</returns>
		public XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("KeyInfo", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(_id))
			{
				xmlElement.SetAttribute("Id", _id);
			}
			for (int i = 0; i < _keyInfoClauses.Count; i++)
			{
				XmlElement xml = ((KeyInfoClause)_keyInfoClauses[i]).GetXml(xmlDocument);
				if (xml != null)
				{
					xmlElement.AppendChild(xml);
				}
			}
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> state.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			_id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			if (!Utils.VerifyAttributes(value, "Id"))
			{
				throw new CryptographicException("Malformed element {0}.", "KeyInfo");
			}
			for (XmlNode xmlNode = value.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode is XmlElement xmlElement)
				{
					string text = xmlElement.NamespaceURI + " " + xmlElement.LocalName;
					if (text == "http://www.w3.org/2000/09/xmldsig# KeyValue")
					{
						if (!Utils.VerifyAttributes(xmlElement, (string[])null))
						{
							throw new CryptographicException("Malformed element {0}.", "KeyInfo/KeyValue");
						}
						foreach (XmlNode childNode in xmlElement.ChildNodes)
						{
							if (childNode is XmlElement xmlElement2)
							{
								text = text + "/" + xmlElement2.LocalName;
								break;
							}
						}
					}
					KeyInfoClause keyInfoClause = CryptoHelpers.CreateFromName<KeyInfoClause>(text);
					if (keyInfoClause == null)
					{
						keyInfoClause = new KeyInfoNode();
					}
					keyInfoClause.LoadXml(xmlElement);
					AddClause(keyInfoClause);
				}
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> that represents a particular type of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> information to the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <param name="clause">The <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</param>
		public void AddClause(KeyInfoClause clause)
		{
			_keyInfoClauses.Add(clause);
		}

		/// <summary>Returns an enumerator of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects in the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <returns>An enumerator of the subelements of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> that can be used to iterate through the collection.</returns>
		public IEnumerator GetEnumerator()
		{
			return _keyInfoClauses.GetEnumerator();
		}

		/// <summary>Returns an enumerator of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects of the specified type in the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <param name="requestedObjectType">The type of object to enumerate.</param>
		/// <returns>An enumerator of the subelements of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> that can be used to iterate through the collection.</returns>
		public IEnumerator GetEnumerator(Type requestedObjectType)
		{
			ArrayList arrayList = new ArrayList();
			IEnumerator enumerator = _keyInfoClauses.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object current = enumerator.Current;
				if (requestedObjectType.Equals(current.GetType()))
				{
					arrayList.Add(current);
				}
			}
			return arrayList.GetEnumerator();
		}
	}
	/// <summary>Represents the abstract base class from which all implementations of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> subelements inherit.</summary>
	public abstract class KeyInfoClause
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" />.</summary>
		protected KeyInfoClause()
		{
		}

		/// <summary>When overridden in a derived class, returns an XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" />.</summary>
		/// <returns>An XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" />.</returns>
		public abstract XmlElement GetXml();

		internal virtual XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xml = GetXml();
			return (XmlElement)xmlDocument.ImportNode(xml, deep: true);
		}

		/// <summary>When overridden in a derived class, parses the input <see cref="T:System.Xml.XmlElement" /> and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> to match.</summary>
		/// <param name="element">The <see cref="T:System.Xml.XmlElement" /> that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" />.</param>
		public abstract void LoadXml(XmlElement element);
	}
	/// <summary>Wraps the <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> class, it to be placed as a subelement of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> class.</summary>
	public class KeyInfoEncryptedKey : KeyInfoClause
	{
		private EncryptedKey _encryptedKey;

		/// <summary>Gets or sets an <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object that encapsulates an encrypted key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object that encapsulates an encrypted key.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.KeyInfoEncryptedKey.EncryptedKey" /> property is <see langword="null" />.</exception>
		public EncryptedKey EncryptedKey
		{
			get
			{
				return _encryptedKey;
			}
			set
			{
				_encryptedKey = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> class.</summary>
		public KeyInfoEncryptedKey()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> class using an <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object.</summary>
		/// <param name="encryptedKey">An <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object that encapsulates an encrypted key.</param>
		public KeyInfoEncryptedKey(EncryptedKey encryptedKey)
		{
			_encryptedKey = encryptedKey;
		}

		/// <summary>Returns an XML representation of a <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object.</summary>
		/// <returns>An XML representation of a <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The encrypted key is <see langword="null" />.</exception>
		public override XmlElement GetXml()
		{
			if (_encryptedKey == null)
			{
				throw new CryptographicException("Malformed element {0}.", "KeyInfoEncryptedKey");
			}
			return _encryptedKey.GetXml();
		}

		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			if (_encryptedKey == null)
			{
				throw new CryptographicException("Malformed element {0}.", "KeyInfoEncryptedKey");
			}
			return _encryptedKey.GetXml(xmlDocument);
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object to match.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlElement" /> object that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object.</param>
		public override void LoadXml(XmlElement value)
		{
			_encryptedKey = new EncryptedKey();
			_encryptedKey.LoadXml(value);
		}
	}
	/// <summary>Represents a <see langword="&lt;KeyName&gt;" /> subelement of an XMLDSIG or XML Encryption <see langword="&lt;KeyInfo&gt;" /> element.</summary>
	public class KeyInfoName : KeyInfoClause
	{
		private string _keyName;

		/// <summary>Gets or sets the string identifier contained within a <see langword="&lt;KeyName&gt;" /> element.</summary>
		/// <returns>The string identifier that is the value of the <see langword="&lt;KeyName&gt;" /> element.</returns>
		public string Value
		{
			get
			{
				return _keyName;
			}
			set
			{
				_keyName = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> class.</summary>
		public KeyInfoName()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> class by specifying the string identifier that is the value of the <see langword="&lt;KeyName&gt;" /> element.</summary>
		/// <param name="keyName">The string identifier that is the value of the <see langword="&lt;KeyName&gt;" /> element.</param>
		public KeyInfoName(string keyName)
		{
			Value = keyName;
		}

		/// <summary>Returns an XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object.</summary>
		/// <returns>An XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object.</returns>
		public override XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("KeyName", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement.AppendChild(xmlDocument.CreateTextNode(_keyName));
			return xmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object to match.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlElement" /> object that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			_keyName = value.InnerText.Trim();
		}
	}
	/// <summary>Handles <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> subelements that do not have specific implementations or handlers registered on the machine.</summary>
	public class KeyInfoNode : KeyInfoClause
	{
		private XmlElement _node;

		/// <summary>Gets or sets the XML content of the current <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />.</summary>
		/// <returns>The XML content of the current <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />.</returns>
		public XmlElement Value
		{
			get
			{
				return _node;
			}
			set
			{
				_node = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" /> class.</summary>
		public KeyInfoNode()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" /> class with content taken from the specified <see cref="T:System.Xml.XmlElement" />.</summary>
		/// <param name="node">An XML element from which to take the content used to create the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />.</param>
		public KeyInfoNode(XmlElement node)
		{
			_node = node;
		}

		/// <summary>Returns an XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />.</summary>
		/// <returns>An XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />.</returns>
		public override XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			return xmlDocument.ImportNode(_node, deep: true) as XmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" /> to match.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlElement" /> that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />.</param>
		public override void LoadXml(XmlElement value)
		{
			_node = value;
		}
	}
	/// <summary>References <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> objects stored at a different location when using XMLDSIG or XML encryption.</summary>
	public class KeyInfoRetrievalMethod : KeyInfoClause
	{
		private string _uri;

		private string _type;

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</summary>
		/// <returns>The Uniform Resource Identifier (URI) of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</returns>
		public string Uri
		{
			get
			{
				return _uri;
			}
			set
			{
				_uri = value;
			}
		}

		/// <summary>Gets or sets a Uniform Resource Identifier (URI) that describes the type of data to be retrieved.</summary>
		/// <returns>A Uniform Resource Identifier (URI) that describes the type of data to be retrieved.</returns>
		public string Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> class.</summary>
		public KeyInfoRetrievalMethod()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> class with the specified Uniform Resource Identifier (URI) pointing to the referenced <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <param name="strUri">The Uniform Resource Identifier (URI) of the information to be referenced by the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" />.</param>
		public KeyInfoRetrievalMethod(string strUri)
		{
			_uri = strUri;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> class with the specified Uniform Resource Identifier (URI) pointing to the referenced <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object and the URI that describes the type of data to retrieve.</summary>
		/// <param name="strUri">The Uniform Resource Identifier (URI) of the information to be referenced by the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" />.</param>
		/// <param name="typeName">The URI that describes the type of data to retrieve.</param>
		public KeyInfoRetrievalMethod(string strUri, string typeName)
		{
			_uri = strUri;
			_type = typeName;
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</returns>
		public override XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("RetrievalMethod", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(_uri))
			{
				xmlElement.SetAttribute("URI", _uri);
			}
			if (!string.IsNullOrEmpty(_type))
			{
				xmlElement.SetAttribute("Type", _type);
			}
			return xmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object to match.</summary>
		/// <param name="value">The XML element that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			_uri = Utils.GetAttribute(value, "URI", "http://www.w3.org/2000/09/xmldsig#");
			_type = Utils.GetAttribute(value, "Type", "http://www.w3.org/2000/09/xmldsig#");
		}
	}
	/// <summary>Represents an <see langword="&lt;X509Data&gt;" /> subelement of an XMLDSIG or XML Encryption <see langword="&lt;KeyInfo&gt;" /> element.</summary>
	public class KeyInfoX509Data : KeyInfoClause
	{
		private ArrayList _certificates;

		private ArrayList _issuerSerials;

		private ArrayList _subjectKeyIds;

		private ArrayList _subjectNames;

		private byte[] _CRL;

		/// <summary>Gets a list of the X.509v3 certificates contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>A list of the X.509 certificates contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		public ArrayList Certificates => _certificates;

		/// <summary>Gets a list of the subject key identifiers (SKIs) contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>A list of the subject key identifiers (SKIs) contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		public ArrayList SubjectKeyIds => _subjectKeyIds;

		/// <summary>Gets a list of the subject names of the entities contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>A list of the subject names of the entities contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		public ArrayList SubjectNames => _subjectNames;

		/// <summary>Gets a list of <see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" /> structures that represent an issuer name and serial number pair.</summary>
		/// <returns>A list of <see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" /> structures that represent an issuer name and serial number pair.</returns>
		public ArrayList IssuerSerials => _issuerSerials;

		/// <summary>Gets or sets the Certificate Revocation List (CRL) contained within the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>The Certificate Revocation List (CRL) contained within the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		public byte[] CRL
		{
			get
			{
				return _CRL;
			}
			set
			{
				_CRL = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class.</summary>
		public KeyInfoX509Data()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class from the specified ASN.1 DER encoding of an X.509v3 certificate.</summary>
		/// <param name="rgbCert">The ASN.1 DER encoding of an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> from.</param>
		public KeyInfoX509Data(byte[] rgbCert)
		{
			X509Certificate2 certificate = new X509Certificate2(rgbCert);
			AddCertificate(certificate);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class from the specified X.509v3 certificate.</summary>
		/// <param name="cert">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cert" /> parameter is <see langword="null" />.</exception>
		public KeyInfoX509Data(X509Certificate cert)
		{
			AddCertificate(cert);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class from the specified X.509v3 certificate.</summary>
		/// <param name="cert">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> from.</param>
		/// <param name="includeOption">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509IncludeOption" /> values that specifies how much of the certificate chain to include.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cert" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate has only a partial certificate chain.</exception>
		public KeyInfoX509Data(X509Certificate cert, X509IncludeOption includeOption)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			X509Certificate2 certificate = new X509Certificate2(cert);
			X509ChainElementCollection x509ChainElementCollection = null;
			X509Chain x509Chain = null;
			switch (includeOption)
			{
			case X509IncludeOption.ExcludeRoot:
			{
				x509Chain = new X509Chain();
				x509Chain.Build(certificate);
				if (x509Chain.ChainStatus.Length != 0 && (x509Chain.ChainStatus[0].Status & X509ChainStatusFlags.PartialChain) == X509ChainStatusFlags.PartialChain)
				{
					throw new CryptographicException("A certificate chain could not be built to a trusted root authority.");
				}
				x509ChainElementCollection = x509Chain.ChainElements;
				for (int i = 0; i < (Utils.IsSelfSigned(x509Chain) ? 1 : (x509ChainElementCollection.Count - 1)); i++)
				{
					AddCertificate(x509ChainElementCollection[i].Certificate);
				}
				break;
			}
			case X509IncludeOption.EndCertOnly:
				AddCertificate(certificate);
				break;
			case X509IncludeOption.WholeChain:
			{
				x509Chain = new X509Chain();
				x509Chain.Build(certificate);
				if (x509Chain.ChainStatus.Length != 0 && (x509Chain.ChainStatus[0].Status & X509ChainStatusFlags.PartialChain) == X509ChainStatusFlags.PartialChain)
				{
					throw new CryptographicException("A certificate chain could not be built to a trusted root authority.");
				}
				x509ChainElementCollection = x509Chain.ChainElements;
				X509ChainElementEnumerator enumerator = x509ChainElementCollection.GetEnumerator();
				while (enumerator.MoveNext())
				{
					X509ChainElement current = enumerator.Current;
					AddCertificate(current.Certificate);
				}
				break;
			}
			}
		}

		/// <summary>Adds the specified X.509v3 certificate to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" />.</summary>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is <see langword="null" />.</exception>
		public void AddCertificate(X509Certificate certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (_certificates == null)
			{
				_certificates = new ArrayList();
			}
			X509Certificate2 value = new X509Certificate2(certificate);
			_certificates.Add(value);
		}

		/// <summary>Adds the specified subject key identifier (SKI) byte array to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="subjectKeyId">A byte array that represents the subject key identifier (SKI) to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		public void AddSubjectKeyId(byte[] subjectKeyId)
		{
			if (_subjectKeyIds == null)
			{
				_subjectKeyIds = new ArrayList();
			}
			_subjectKeyIds.Add(subjectKeyId);
		}

		/// <summary>Adds the specified subject key identifier (SKI) string to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="subjectKeyId">A string that represents the subject key identifier (SKI) to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		public void AddSubjectKeyId(string subjectKeyId)
		{
			if (_subjectKeyIds == null)
			{
				_subjectKeyIds = new ArrayList();
			}
			_subjectKeyIds.Add(Utils.DecodeHexString(subjectKeyId));
		}

		/// <summary>Adds the subject name of the entity that was issued an X.509v3 certificate to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="subjectName">The name of the entity that was issued an X.509 certificate to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		public void AddSubjectName(string subjectName)
		{
			if (_subjectNames == null)
			{
				_subjectNames = new ArrayList();
			}
			_subjectNames.Add(subjectName);
		}

		/// <summary>Adds the specified issuer name and serial number pair to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="issuerName">The issuer name portion of the pair to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		/// <param name="serialNumber">The serial number portion of the pair to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		public void AddIssuerSerial(string issuerName, string serialNumber)
		{
			if (string.IsNullOrEmpty(issuerName))
			{
				throw new ArgumentException("String cannot be empty or null.", "issuerName");
			}
			if (string.IsNullOrEmpty(serialNumber))
			{
				throw new ArgumentException("String cannot be empty or null.", "serialNumber");
			}
			if (!BigInteger.TryParse(serialNumber, NumberStyles.AllowHexSpecifier, NumberFormatInfo.CurrentInfo, out var result))
			{
				throw new ArgumentException("X509 issuer serial number is invalid.", "serialNumber");
			}
			if (_issuerSerials == null)
			{
				_issuerSerials = new ArrayList();
			}
			_issuerSerials.Add(Utils.CreateX509IssuerSerial(issuerName, result.ToString()));
		}

		internal void InternalAddIssuerSerial(string issuerName, string serialNumber)
		{
			if (_issuerSerials == null)
			{
				_issuerSerials = new ArrayList();
			}
			_issuerSerials.Add(Utils.CreateX509IssuerSerial(issuerName, serialNumber));
		}

		private void Clear()
		{
			_CRL = null;
			if (_subjectKeyIds != null)
			{
				_subjectKeyIds.Clear();
			}
			if (_subjectNames != null)
			{
				_subjectNames.Clear();
			}
			if (_issuerSerials != null)
			{
				_issuerSerials.Clear();
			}
			if (_certificates != null)
			{
				_certificates.Clear();
			}
		}

		/// <summary>Returns an XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>An XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		public override XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("X509Data", "http://www.w3.org/2000/09/xmldsig#");
			if (_issuerSerials != null)
			{
				foreach (X509IssuerSerial issuerSerial in _issuerSerials)
				{
					XmlElement xmlElement2 = xmlDocument.CreateElement("X509IssuerSerial", "http://www.w3.org/2000/09/xmldsig#");
					XmlElement xmlElement3 = xmlDocument.CreateElement("X509IssuerName", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement3.AppendChild(xmlDocument.CreateTextNode(issuerSerial.IssuerName));
					xmlElement2.AppendChild(xmlElement3);
					XmlElement xmlElement4 = xmlDocument.CreateElement("X509SerialNumber", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement4.AppendChild(xmlDocument.CreateTextNode(issuerSerial.SerialNumber));
					xmlElement2.AppendChild(xmlElement4);
					xmlElement.AppendChild(xmlElement2);
				}
			}
			if (_subjectKeyIds != null)
			{
				foreach (byte[] subjectKeyId in _subjectKeyIds)
				{
					XmlElement xmlElement5 = xmlDocument.CreateElement("X509SKI", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement5.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(subjectKeyId)));
					xmlElement.AppendChild(xmlElement5);
				}
			}
			if (_subjectNames != null)
			{
				foreach (string subjectName in _subjectNames)
				{
					XmlElement xmlElement6 = xmlDocument.CreateElement("X509SubjectName", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement6.AppendChild(xmlDocument.CreateTextNode(subjectName));
					xmlElement.AppendChild(xmlElement6);
				}
			}
			if (_certificates != null)
			{
				foreach (X509Certificate certificate in _certificates)
				{
					XmlElement xmlElement7 = xmlDocument.CreateElement("X509Certificate", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement7.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(certificate.GetRawCertData())));
					xmlElement.AppendChild(xmlElement7);
				}
			}
			if (_CRL != null)
			{
				XmlElement xmlElement8 = xmlDocument.CreateElement("X509CRL", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement8.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(_CRL)));
				xmlElement.AppendChild(xmlElement8);
			}
			return xmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object to match.</summary>
		/// <param name="element">The <see cref="T:System.Xml.XmlElement" /> object that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="element" /> parameter does not contain an &lt;<see langword="X509IssuerName" />&gt; node.  
		///  -or-  
		///  The <paramref name="element" /> parameter does not contain an &lt;<see langword="X509SerialNumber" />&gt; node.</exception>
		public override void LoadXml(XmlElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(element.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlNodeList xmlNodeList = element.SelectNodes("ds:X509IssuerSerial", xmlNamespaceManager);
			XmlNodeList xmlNodeList2 = element.SelectNodes("ds:X509SKI", xmlNamespaceManager);
			XmlNodeList xmlNodeList3 = element.SelectNodes("ds:X509SubjectName", xmlNamespaceManager);
			XmlNodeList xmlNodeList4 = element.SelectNodes("ds:X509Certificate", xmlNamespaceManager);
			XmlNodeList xmlNodeList5 = element.SelectNodes("ds:X509CRL", xmlNamespaceManager);
			if (xmlNodeList5.Count == 0 && xmlNodeList.Count == 0 && xmlNodeList2.Count == 0 && xmlNodeList3.Count == 0 && xmlNodeList4.Count == 0)
			{
				throw new CryptographicException("Malformed element {0}.", "X509Data");
			}
			Clear();
			if (xmlNodeList5.Count != 0)
			{
				_CRL = Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlNodeList5.Item(0).InnerText));
			}
			foreach (XmlNode item in xmlNodeList)
			{
				XmlNode xmlNode = item.SelectSingleNode("ds:X509IssuerName", xmlNamespaceManager);
				XmlNode xmlNode2 = item.SelectSingleNode("ds:X509SerialNumber", xmlNamespaceManager);
				if (xmlNode == null || xmlNode2 == null)
				{
					throw new CryptographicException("Malformed element {0}.", "IssuerSerial");
				}
				InternalAddIssuerSerial(xmlNode.InnerText.Trim(), xmlNode2.InnerText.Trim());
			}
			foreach (XmlNode item2 in xmlNodeList2)
			{
				AddSubjectKeyId(Convert.FromBase64String(Utils.DiscardWhiteSpaces(item2.InnerText)));
			}
			foreach (XmlNode item3 in xmlNodeList3)
			{
				AddSubjectName(item3.InnerText.Trim());
			}
			foreach (XmlNode item4 in xmlNodeList4)
			{
				AddCertificate(new X509Certificate2(Convert.FromBase64String(Utils.DiscardWhiteSpaces(item4.InnerText))));
			}
		}
	}
	/// <summary>Represents the <see langword="&lt;KeyReference&gt;" /> element used in XML encryption. This class cannot be inherited.</summary>
	public sealed class KeyReference : EncryptedReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> class for XML encryption.</summary>
		public KeyReference()
		{
			base.ReferenceType = "KeyReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> class for XML encryption using the supplied Uniform Resource Identifier (URI).</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) that points to the encrypted key.</param>
		public KeyReference(string uri)
			: base(uri)
		{
			base.ReferenceType = "KeyReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> class for XML encryption using the specified Uniform Resource Identifier (URI) and a <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) that points to the encrypted key.</param>
		/// <param name="transformChain">A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms to do on the encrypted key.</param>
		public KeyReference(string uri, TransformChain transformChain)
			: base(uri, transformChain)
		{
			base.ReferenceType = "KeyReference";
		}
	}
	internal class MyXmlDocument : XmlDocument
	{
		protected override XmlAttribute CreateDefaultAttribute(string prefix, string localName, string namespaceURI)
		{
			return CreateAttribute(prefix, localName, namespaceURI);
		}
	}
	internal class NamespaceFrame
	{
		private Hashtable _rendered = new Hashtable();

		private Hashtable _unrendered = new Hashtable();

		internal NamespaceFrame()
		{
		}

		internal void AddRendered(XmlAttribute attr)
		{
			_rendered.Add(Utils.GetNamespacePrefix(attr), attr);
		}

		internal XmlAttribute GetRendered(string nsPrefix)
		{
			return (XmlAttribute)_rendered[nsPrefix];
		}

		internal void AddUnrendered(XmlAttribute attr)
		{
			_unrendered.Add(Utils.GetNamespacePrefix(attr), attr);
		}

		internal XmlAttribute GetUnrendered(string nsPrefix)
		{
			return (XmlAttribute)_unrendered[nsPrefix];
		}

		internal Hashtable GetUnrendered()
		{
			return _unrendered;
		}
	}
	internal class NamespaceSortOrder : IComparer
	{
		internal NamespaceSortOrder()
		{
		}

		public int Compare(object a, object b)
		{
			XmlNode xmlNode = a as XmlNode;
			XmlNode xmlNode2 = b as XmlNode;
			if (xmlNode == null || xmlNode2 == null)
			{
				throw new ArgumentException();
			}
			bool flag = Utils.IsDefaultNamespaceNode(xmlNode);
			bool flag2 = Utils.IsDefaultNamespaceNode(xmlNode2);
			if (flag && flag2)
			{
				return 0;
			}
			if (flag)
			{
				return -1;
			}
			if (flag2)
			{
				return 1;
			}
			return string.CompareOrdinal(xmlNode.LocalName, xmlNode2.LocalName);
		}
	}
	/// <summary>Represents the &lt;<see langword="RSAKeyValue" />&gt; element of an XML signature.</summary>
	public class RSAKeyValue : KeyInfoClause
	{
		private RSA _key;

		private const string KeyValueElementName = "KeyValue";

		private const string RSAKeyValueElementName = "RSAKeyValue";

		private const string ModulusElementName = "Modulus";

		private const string ExponentElementName = "Exponent";

		/// <summary>Gets or sets the instance of <see cref="T:System.Security.Cryptography.RSA" /> that holds the public key.</summary>
		/// <returns>The instance of <see cref="T:System.Security.Cryptography.RSA" /> that holds the public key.</returns>
		public RSA Key
		{
			get
			{
				return _key;
			}
			set
			{
				_key = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.RSAKeyValue" /> class with a new randomly generated <see cref="T:System.Security.Cryptography.RSA" /> public key.</summary>
		public RSAKeyValue()
		{
			_key = RSA.Create();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.RSAKeyValue" /> class with the specified <see cref="T:System.Security.Cryptography.RSA" /> public key.</summary>
		/// <param name="key">The instance of an implementation of <see cref="T:System.Security.Cryptography.RSA" /> that holds the public key.</param>
		public RSAKeyValue(RSA key)
		{
			_key = key;
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.RSA" /> key clause.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.RSA" /> key clause.</returns>
		public override XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			RSAParameters rSAParameters = _key.ExportParameters(includePrivateParameters: false);
			XmlElement xmlElement = xmlDocument.CreateElement("KeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement2 = xmlDocument.CreateElement("RSAKeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement3 = xmlDocument.CreateElement("Modulus", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement3.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(rSAParameters.Modulus)));
			xmlElement2.AppendChild(xmlElement3);
			XmlElement xmlElement4 = xmlDocument.CreateElement("Exponent", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement4.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(rSAParameters.Exponent)));
			xmlElement2.AppendChild(xmlElement4);
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}

		/// <summary>Loads an <see cref="T:System.Security.Cryptography.RSA" /> key clause from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.RSA" /> key clause.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter is not a valid <see cref="T:System.Security.Cryptography.RSA" /> key clause XML element.</exception>
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "KeyValue" || value.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				throw new CryptographicException("Root element must be KeyValue element in namespace http://www.w3.org/2000/09/xmldsig#");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
			XmlNode xmlNode = value.SelectSingleNode("dsig:RSAKeyValue", xmlNamespaceManager);
			if (xmlNode == null)
			{
				throw new CryptographicException("KeyValue must contain child element RSAKeyValue");
			}
			try
			{
				Key.ImportParameters(new RSAParameters
				{
					Modulus = Convert.FromBase64String(xmlNode.SelectSingleNode("dsig:Modulus", xmlNamespaceManager).InnerText),
					Exponent = Convert.FromBase64String(xmlNode.SelectSingleNode("dsig:Exponent", xmlNamespaceManager).InnerText)
				});
			}
			catch (Exception inner)
			{
				throw new CryptographicException("An error occurred parsing the Modulus and Exponent elements", inner);
			}
		}
	}
	internal class RSAPKCS1SHA1SignatureDescription : RSAPKCS1SignatureDescription
	{
		public RSAPKCS1SHA1SignatureDescription()
			: base("SHA1")
		{
		}

		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA1.Create();
		}
	}
	internal class RSAPKCS1SHA256SignatureDescription : RSAPKCS1SignatureDescription
	{
		public RSAPKCS1SHA256SignatureDescription()
			: base("SHA256")
		{
		}

		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA256.Create();
		}
	}
	internal class RSAPKCS1SHA384SignatureDescription : RSAPKCS1SignatureDescription
	{
		public RSAPKCS1SHA384SignatureDescription()
			: base("SHA384")
		{
		}

		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA384.Create();
		}
	}
	internal class RSAPKCS1SHA512SignatureDescription : RSAPKCS1SignatureDescription
	{
		public RSAPKCS1SHA512SignatureDescription()
			: base("SHA512")
		{
		}

		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA512.Create();
		}
	}
	internal abstract class RSAPKCS1SignatureDescription : SignatureDescription
	{
		public RSAPKCS1SignatureDescription(string hashAlgorithmName)
		{
			base.KeyAlgorithm = typeof(RSA).AssemblyQualifiedName;
			base.FormatterAlgorithm = typeof(RSAPKCS1SignatureFormatter).AssemblyQualifiedName;
			base.DeformatterAlgorithm = typeof(RSAPKCS1SignatureDeformatter).AssemblyQualifiedName;
			base.DigestAlgorithm = hashAlgorithmName;
		}

		public sealed override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter obj = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(base.DeformatterAlgorithm);
			obj.SetKey(key);
			obj.SetHashAlgorithm(base.DigestAlgorithm);
			return obj;
		}

		public sealed override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter obj = (AsymmetricSignatureFormatter)CryptoConfig.CreateFromName(base.FormatterAlgorithm);
			obj.SetKey(key);
			obj.SetHashAlgorithm(base.DigestAlgorithm);
			return obj;
		}

		public abstract override HashAlgorithm CreateDigest();
	}
	/// <summary>Represents the <see langword="&lt;reference&gt;" /> element of an XML signature.</summary>
	public class Reference
	{
		internal const string DefaultDigestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";

		private string _id;

		private string _uri;

		private string _type;

		private TransformChain _transformChain;

		private string _digestMethod;

		private byte[] _digestValue;

		private HashAlgorithm _hashAlgorithm;

		private object _refTarget;

		private ReferenceTargetType _refTargetType;

		private XmlElement _cachedXml;

		private SignedXml _signedXml;

		internal CanonicalXmlNodeList _namespaces;

		/// <summary>Gets or sets the ID of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />. The default is <see langword="null" />.</returns>
		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Uri" /> of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The <see cref="T:System.Uri" /> of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		public string Uri
		{
			get
			{
				return _uri;
			}
			set
			{
				_uri = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the type of the object being signed.</summary>
		/// <returns>The type of the object being signed.</returns>
		public string Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the digest method Uniform Resource Identifier (URI) of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The digest method URI of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />. The default value is "http://www.w3.org/2000/09/xmldsig#sha1".</returns>
		public string DigestMethod
		{
			get
			{
				return _digestMethod;
			}
			set
			{
				_digestMethod = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the digest value of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The digest value of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		public byte[] DigestValue
		{
			get
			{
				return _digestValue;
			}
			set
			{
				_digestValue = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets the transform chain of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The transform chain of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		public TransformChain TransformChain
		{
			get
			{
				if (_transformChain == null)
				{
					_transformChain = new TransformChain();
				}
				return _transformChain;
			}
			set
			{
				_transformChain = value;
				_cachedXml = null;
			}
		}

		internal bool CacheValid => _cachedXml != null;

		internal SignedXml SignedXml
		{
			get
			{
				return _signedXml;
			}
			set
			{
				_signedXml = value;
			}
		}

		internal ReferenceTargetType ReferenceTargetType => _refTargetType;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> class with default properties.</summary>
		public Reference()
		{
			_transformChain = new TransformChain();
			_refTarget = null;
			_refTargetType = ReferenceTargetType.UriReference;
			_cachedXml = null;
			_digestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> class with a hash value of the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> with which to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.Reference" />.</param>
		public Reference(Stream stream)
		{
			_transformChain = new TransformChain();
			_refTarget = stream;
			_refTargetType = ReferenceTargetType.Stream;
			_cachedXml = null;
			_digestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> class with the specified <see cref="T:System.Uri" />.</summary>
		/// <param name="uri">The <see cref="T:System.Uri" /> with which to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.Reference" />.</param>
		public Reference(string uri)
		{
			_transformChain = new TransformChain();
			_refTarget = uri;
			_uri = uri;
			_refTargetType = ReferenceTargetType.UriReference;
			_cachedXml = null;
			_digestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";
		}

		internal Reference(XmlElement element)
		{
			_transformChain = new TransformChain();
			_refTarget = element;
			_refTargetType = ReferenceTargetType.XmlElement;
			_cachedXml = null;
			_digestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.Reference.DigestMethod" /> property is <see langword="null" />.  
		///  -or-  
		///  The <see cref="P:System.Security.Cryptography.Xml.Reference.DigestValue" /> property is <see langword="null" />.</exception>
		public XmlElement GetXml()
		{
			if (CacheValid)
			{
				return _cachedXml;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("Reference", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(_id))
			{
				xmlElement.SetAttribute("Id", _id);
			}
			if (_uri != null)
			{
				xmlElement.SetAttribute("URI", _uri);
			}
			if (!string.IsNullOrEmpty(_type))
			{
				xmlElement.SetAttribute("Type", _type);
			}
			if (TransformChain.Count != 0)
			{
				xmlElement.AppendChild(TransformChain.GetXml(document, "http://www.w3.org/2000/09/xmldsig#"));
			}
			if (string.IsNullOrEmpty(_digestMethod))
			{
				throw new CryptographicException("A DigestMethod must be specified on a Reference prior to generating XML.");
			}
			XmlElement xmlElement2 = document.CreateElement("DigestMethod", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement2.SetAttribute("Algorithm", _digestMethod);
			xmlElement.AppendChild(xmlElement2);
			if (DigestValue == null)
			{
				if (_hashAlgorithm.Hash == null)
				{
					throw new CryptographicException("A Reference must contain a DigestValue.");
				}
				DigestValue = _hashAlgorithm.Hash;
			}
			XmlElement xmlElement3 = document.CreateElement("DigestValue", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement3.AppendChild(document.CreateTextNode(Convert.ToBase64String(_digestValue)));
			xmlElement.AppendChild(xmlElement3);
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.Reference" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.Reference" /> state.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain any transforms.  
		///  -or-  
		///  The <paramref name="value" /> parameter contains an unknown transform.</exception>
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			_id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			_uri = Utils.GetAttribute(value, "URI", "http://www.w3.org/2000/09/xmldsig#");
			_type = Utils.GetAttribute(value, "Type", "http://www.w3.org/2000/09/xmldsig#");
			if (!Utils.VerifyAttributes(value, new string[3] { "Id", "URI", "Type" }))
			{
				throw new CryptographicException("Malformed element {0}.", "Reference");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			bool flag = false;
			TransformChain = new TransformChain();
			XmlNodeList xmlNodeList = value.SelectNodes("ds:Transforms", xmlNamespaceManager);
			if (xmlNodeList != null && xmlNodeList.Count != 0)
			{
				if (xmlNodeList.Count > 1)
				{
					throw new CryptographicException("Malformed element {0}.", "Reference/Transforms");
				}
				flag = true;
				XmlElement xmlElement = xmlNodeList[0] as XmlElement;
				if (!Utils.VerifyAttributes(xmlElement, (string[])null))
				{
					throw new CryptographicException("Malformed element {0}.", "Reference/Transforms");
				}
				XmlNodeList xmlNodeList2 = xmlElement.SelectNodes("ds:Transform", xmlNamespaceManager);
				if (xmlNodeList2 != null)
				{
					if (xmlNodeList2.Count != xmlElement.SelectNodes("*").Count)
					{
						throw new CryptographicException("Malformed element {0}.", "Reference/Transforms");
					}
					if (xmlNodeList2.Count > 10)
					{
						throw new CryptographicException("Malformed element {0}.", "Reference/Transforms");
					}
					foreach (XmlNode item in xmlNodeList2)
					{
						XmlElement xmlElement2 = item as XmlElement;
						string attribute = Utils.GetAttribute(xmlElement2, "Algorithm", "http://www.w3.org/2000/09/xmldsig#");
						if (attribute == null || !Utils.VerifyAttributes(xmlElement2, "Algorithm"))
						{
							throw new CryptographicException("Unknown transform has been encountered.");
						}
						Transform transform = CryptoHelpers.CreateFromName<Transform>(attribute);
						if (transform == null)
						{
							throw new CryptographicException("Unknown transform has been encountered.");
						}
						AddTransform(transform);
						transform.LoadInnerXml(xmlElement2.ChildNodes);
						if (!(transform is XmlDsigEnvelopedSignatureTransform))
						{
							continue;
						}
						XmlNode xmlNode = xmlElement2.SelectSingleNode("ancestor::ds:Signature[1]", xmlNamespaceManager);
						XmlNodeList xmlNodeList3 = xmlElement2.SelectNodes("//ds:Signature", xmlNamespaceManager);
						if (xmlNodeList3 == null)
						{
							continue;
						}
						int num = 0;
						foreach (XmlNode item2 in xmlNodeList3)
						{
							num++;
							if (item2 == xmlNode)
							{
								((XmlDsigEnvelopedSignatureTransform)transform).SignaturePosition = num;
								break;
							}
						}
					}
				}
			}
			XmlNodeList xmlNodeList4 = value.SelectNodes("ds:DigestMethod", xmlNamespaceManager);
			if (xmlNodeList4 == null || xmlNodeList4.Count == 0 || xmlNodeList4.Count > 1)
			{
				throw new CryptographicException("Malformed element {0}.", "Reference/DigestMethod");
			}
			XmlElement element = xmlNodeList4[0] as XmlElement;
			_digestMethod = Utils.GetAttribute(element, "Algorithm", "http://www.w3.org/2000/09/xmldsig#");
			if (_digestMethod == null || !Utils.VerifyAttributes(element, "Algorithm"))
			{
				throw new CryptographicException("Malformed element {0}.", "Reference/DigestMethod");
			}
			XmlNodeList xmlNodeList5 = value.SelectNodes("ds:DigestValue", xmlNamespaceManager);
			if (xmlNodeList5 == null || xmlNodeList5.Count == 0 || xmlNodeList5.Count > 1)
			{
				throw new CryptographicException("Malformed element {0}.", "Reference/DigestValue");
			}
			XmlElement xmlElement3 = xmlNodeList5[0] as XmlElement;
			_digestValue = Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlElement3.InnerText));
			if (!Utils.VerifyAttributes(xmlElement3, (string[])null))
			{
				throw new CryptographicException("Malformed element {0}.", "Reference/DigestValue");
			}
			int num2 = (flag ? 3 : 2);
			if (value.SelectNodes("*").Count != num2)
			{
				throw new CryptographicException("Malformed element {0}.", "Reference");
			}
			_cachedXml = value;
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to the list of transforms to be performed on the data before passing it to the digest algorithm.</summary>
		/// <param name="transform">The transform to be added to the list of transforms.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="transform" /> parameter is <see langword="null" />.</exception>
		public void AddTransform(Transform transform)
		{
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			transform.Reference = this;
			TransformChain.Add(transform);
		}

		internal void UpdateHashValue(XmlDocument document, CanonicalXmlNodeList refList)
		{
			DigestValue = CalculateHashValue(document, refList);
		}

		internal byte[] CalculateHashValue(XmlDocument document, CanonicalXmlNodeList refList)
		{
			_hashAlgorithm = CryptoHelpers.CreateFromName<HashAlgorithm>(_digestMethod);
			if (_hashAlgorithm == null)
			{
				throw new CryptographicException("Could not create hash algorithm object.");
			}
			string text = ((document == null) ? (Environment.CurrentDirectory + "\\") : document.BaseURI);
			Stream stream = null;
			WebResponse webResponse = null;
			Stream stream2 = null;
			XmlResolver xmlResolver = null;
			byte[] array = null;
			try
			{
				switch (_refTargetType)
				{
				case ReferenceTargetType.Stream:
					xmlResolver = (SignedXml.ResolverSet ? SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
					stream = TransformChain.TransformToOctetStream((Stream)_refTarget, xmlResolver, text);
					break;
				case ReferenceTargetType.UriReference:
					if (_uri == null)
					{
						xmlResolver = (SignedXml.ResolverSet ? SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
						stream = TransformChain.TransformToOctetStream((Stream)null, xmlResolver, text);
						break;
					}
					if (_uri.Length == 0)
					{
						if (document == null)
						{
							throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, "An XmlDocument context is required to resolve the Reference Uri {0}.", _uri));
						}
						xmlResolver = (SignedXml.ResolverSet ? SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
						XmlDocument document2 = Utils.DiscardComments(Utils.PreProcessDocumentInput(document, xmlResolver, text));
						stream = TransformChain.TransformToOctetStream(document2, xmlResolver, text);
						break;
					}
					if (_uri[0] == '#')
					{
						bool discardComments = true;
						string idFromLocalUri = Utils.GetIdFromLocalUri(_uri, out discardComments);
						if (idFromLocalUri == "xpointer(/)")
						{
							if (document == null)
							{
								throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, "An XmlDocument context is required to resolve the Reference Uri {0}.", _uri));
							}
							xmlResolver = (SignedXml.ResolverSet ? SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
							stream = TransformChain.TransformToOctetStream(Utils.PreProcessDocumentInput(document, xmlResolver, text), xmlResolver, text);
							break;
						}
						XmlElement xmlElement = SignedXml.GetIdElement(document, idFromLocalUri);
						if (xmlElement != null)
						{
							_namespaces = Utils.GetPropagatedAttributes(xmlElement.ParentNode as XmlElement);
						}
						if (xmlElement == null && refList != null)
						{
							foreach (XmlNode @ref in refList)
							{
								if (@ref is XmlElement xmlElement2 && Utils.HasAttribute(xmlElement2, "Id", "http://www.w3.org/2000/09/xmldsig#") && Utils.GetAttribute(xmlElement2, "Id", "http://www.w3.org/2000/09/xmldsig#").Equals(idFromLocalUri))
								{
									xmlElement = xmlElement2;
									if (_signedXml._context != null)
									{
										_namespaces = Utils.GetPropagatedAttributes(_signedXml._context);
									}
									break;
								}
							}
						}
						if (xmlElement == null)
						{
							throw new CryptographicException("Malformed reference element.");
						}
						XmlDocument xmlDocument = Utils.PreProcessElementInput(xmlElement, xmlResolver, text);
						Utils.AddNamespaces(xmlDocument.DocumentElement, _namespaces);
						xmlResolver = (SignedXml.ResolverSet ? SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
						if (discardComments)
						{
							XmlDocument document3 = Utils.DiscardComments(xmlDocument);
							stream = TransformChain.TransformToOctetStream(document3, xmlResolver, text);
						}
						else
						{
							stream = TransformChain.TransformToOctetStream(xmlDocument, xmlResolver, text);
						}
						break;
					}
					throw new CryptographicException("Unable to resolve Uri {0}.", _uri);
				case ReferenceTargetType.XmlElement:
					xmlResolver = (SignedXml.ResolverSet ? SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
					stream = TransformChain.TransformToOctetStream(Utils.PreProcessElementInput((XmlElement)_refTarget, xmlResolver, text), xmlResolver, text);
					break;
				default:
					throw new CryptographicException("Unable to resolve Uri {0}.", _uri);
				}
				stream = SignedXmlDebugLog.LogReferenceData(this, stream);
				return _hashAlgorithm.ComputeHash(stream);
			}
			finally
			{
				stream?.Close();
				webResponse?.Close();
				stream2?.Close();
			}
		}
	}
	/// <summary>Represents the <see langword="&lt;ReferenceList&gt;" /> element used in XML encryption. This class cannot be inherited.</summary>
	public sealed class ReferenceList : IList, ICollection, IEnumerable
	{
		private ArrayList _references;

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object.</returns>
		public int Count => _references.Count;

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to return.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object at the specified index.</returns>
		[IndexerName("ItemOf")]
		public EncryptedReference this[int index]
		{
			get
			{
				return Item(index);
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />.</exception>
		object IList.this[int index]
		{
			get
			{
				return _references[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!(value is DataReference) && !(value is KeyReference))
				{
					throw new ArgumentException("Type of input object is invalid.", "value");
				}
				_references[index] = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, <see langword="false" />.</returns>
		bool IList.IsFixedSize => _references.IsFixedSize;

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, <see langword="false" />.</returns>
		bool IList.IsReadOnly => _references.IsReadOnly;

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object.</returns>
		public object SyncRoot => _references.SyncRoot;

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		public bool IsSynchronized => _references.IsSynchronized;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> class.</summary>
		public ReferenceList()
		{
			_references = new ArrayList();
		}

		/// <summary>Returns an enumerator that iterates through a <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through a <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</returns>
		public IEnumerator GetEnumerator()
		{
			return _references.GetEnumerator();
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		/// <param name="value">A <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to add to the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</param>
		/// <returns>The position at which the new element was inserted.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter is not a <see cref="T:System.Security.Cryptography.Xml.DataReference" /> object.  
		///  -or-  
		///  The <paramref name="value" /> parameter is not a <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		public int Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is DataReference) && !(value is KeyReference))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			return _references.Add(value);
		}

		/// <summary>Removes all items from the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		public void Clear()
		{
			_references.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection contains a specific <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to locate in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object is found in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection; otherwise, <see langword="false" />.</returns>
		public bool Contains(object value)
		{
			return _references.Contains(value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to locate in the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</param>
		/// <returns>The index of <paramref name="value" /> if found in the collection; otherwise, -1.</returns>
		public int IndexOf(object value)
		{
			return _references.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object into the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection at the specified position.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">A <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to insert into the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter is not a <see cref="T:System.Security.Cryptography.Xml.DataReference" /> object.  
		///  -or-  
		///  The <paramref name="value" /> parameter is not a <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		public void Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is DataReference) && !(value is KeyReference))
			{
				throw new ArgumentException("Type of input object is invalid.", "value");
			}
			_references.Insert(index, value);
		}

		/// <summary>Removes the first occurrence of a specific <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object from the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to remove from the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> collection.</param>
		public void Remove(object value)
		{
			_references.Remove(value);
		}

		/// <summary>Removes the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to remove.</param>
		public void RemoveAt(int index)
		{
			_references.RemoveAt(index);
		}

		/// <summary>Returns the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object to return.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.DataReference" /> or <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> object at the specified index.</returns>
		public EncryptedReference Item(int index)
		{
			return (EncryptedReference)_references[index];
		}

		/// <summary>Copies the elements of the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object to an array, starting at a specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the elements copied from the <see cref="T:System.Security.Cryptography.Xml.ReferenceList" /> object. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		public void CopyTo(Array array, int index)
		{
			_references.CopyTo(array, index);
		}
	}
	internal enum ReferenceTargetType
	{
		Stream,
		XmlElement,
		UriReference
	}
	/// <summary>Represents the <see langword="&lt;Signature&gt;" /> element of an XML signature.</summary>
	public class Signature
	{
		private string _id;

		private SignedInfo _signedInfo;

		private byte[] _signatureValue;

		private string _signatureValueId;

		private KeyInfo _keyInfo;

		private IList _embeddedObjects;

		private CanonicalXmlNodeList _referencedItems;

		private SignedXml _signedXml;

		internal SignedXml SignedXml
		{
			get
			{
				return _signedXml;
			}
			set
			{
				_signedXml = value;
			}
		}

		/// <summary>Gets or sets the ID of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />. The default is <see langword="null" />.</returns>
		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</returns>
		public SignedInfo SignedInfo
		{
			get
			{
				return _signedInfo;
			}
			set
			{
				_signedInfo = value;
				if (SignedXml != null && _signedInfo != null)
				{
					_signedInfo.SignedXml = SignedXml;
				}
			}
		}

		/// <summary>Gets or sets the value of the digital signature.</summary>
		/// <returns>A byte array that contains the value of the digital signature.</returns>
		public byte[] SignatureValue
		{
			get
			{
				return _signatureValue;
			}
			set
			{
				_signatureValue = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</returns>
		public KeyInfo KeyInfo
		{
			get
			{
				if (_keyInfo == null)
				{
					_keyInfo = new KeyInfo();
				}
				return _keyInfo;
			}
			set
			{
				_keyInfo = value;
			}
		}

		/// <summary>Gets or sets a list of objects to be signed.</summary>
		/// <returns>A list of objects to be signed.</returns>
		public IList ObjectList
		{
			get
			{
				return _embeddedObjects;
			}
			set
			{
				_embeddedObjects = value;
			}
		}

		internal CanonicalXmlNodeList ReferencedItems => _referencedItems;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Signature" /> class.</summary>
		public Signature()
		{
			_embeddedObjects = new ArrayList();
			_referencedItems = new CanonicalXmlNodeList();
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.Signature" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.Signature.SignedInfo" /> property is <see langword="null" />.  
		///  -or-  
		///  The <see cref="P:System.Security.Cryptography.Xml.Signature.SignatureValue" /> property is <see langword="null" />.</exception>
		public XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("Signature", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(_id))
			{
				xmlElement.SetAttribute("Id", _id);
			}
			if (_signedInfo == null)
			{
				throw new CryptographicException("Signature requires a SignedInfo.");
			}
			xmlElement.AppendChild(_signedInfo.GetXml(document));
			if (_signatureValue == null)
			{
				throw new CryptographicException("Signature requires a SignatureValue.");
			}
			XmlElement xmlElement2 = document.CreateElement("SignatureValue", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement2.AppendChild(document.CreateTextNode(Convert.ToBase64String(_signatureValue)));
			if (!string.IsNullOrEmpty(_signatureValueId))
			{
				xmlElement2.SetAttribute("Id", _signatureValueId);
			}
			xmlElement.AppendChild(xmlElement2);
			if (KeyInfo.Count > 0)
			{
				xmlElement.AppendChild(KeyInfo.GetXml(document));
			}
			foreach (object embeddedObject in _embeddedObjects)
			{
				if (embeddedObject is DataObject dataObject)
				{
					xmlElement.AppendChild(dataObject.GetXml(document));
				}
			}
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.Signature" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.Signature" /> state.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.Signature.SignatureValue" />.  
		///  -or-  
		///  The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.Signature.SignedInfo" />.</exception>
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!value.LocalName.Equals("Signature"))
			{
				throw new CryptographicException("Malformed element {0}.", "Signature");
			}
			_id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			if (!Utils.VerifyAttributes(value, "Id"))
			{
				throw new CryptographicException("Malformed element {0}.", "Signature");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			int num = 0;
			XmlNodeList xmlNodeList = value.SelectNodes("ds:SignedInfo", xmlNamespaceManager);
			if (xmlNodeList == null || xmlNodeList.Count == 0 || xmlNodeList.Count > 1)
			{
				throw new CryptographicException("Malformed element {0}.", "SignedInfo");
			}
			XmlElement value2 = xmlNodeList[0] as XmlElement;
			num += xmlNodeList.Count;
			SignedInfo = new SignedInfo();
			SignedInfo.LoadXml(value2);
			XmlNodeList xmlNodeList2 = value.SelectNodes("ds:SignatureValue", xmlNamespaceManager);
			if (xmlNodeList2 == null || xmlNodeList2.Count == 0 || xmlNodeList2.Count > 1)
			{
				throw new CryptographicException("Malformed element {0}.", "SignatureValue");
			}
			XmlElement xmlElement = xmlNodeList2[0] as XmlElement;
			num += xmlNodeList2.Count;
			_signatureValue = Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlElement.InnerText));
			_signatureValueId = Utils.GetAttribute(xmlElement, "Id", "http://www.w3.org/2000/09/xmldsig#");
			if (!Utils.VerifyAttributes(xmlElement, "Id"))
			{
				throw new CryptographicException("Malformed element {0}.", "SignatureValue");
			}
			XmlNodeList xmlNodeList3 = value.SelectNodes("ds:KeyInfo", xmlNamespaceManager);
			_keyInfo = new KeyInfo();
			if (xmlNodeList3 != null)
			{
				if (xmlNodeList3.Count > 1)
				{
					throw new CryptographicException("Malformed element {0}.", "KeyInfo");
				}
				foreach (XmlNode item in xmlNodeList3)
				{
					if (item is XmlElement value3)
					{
						_keyInfo.LoadXml(value3);
					}
				}
				num += xmlNodeList3.Count;
			}
			XmlNodeList xmlNodeList4 = value.SelectNodes("ds:Object", xmlNamespaceManager);
			_embeddedObjects.Clear();
			if (xmlNodeList4 != null)
			{
				foreach (XmlNode item2 in xmlNodeList4)
				{
					if (item2 is XmlElement value4)
					{
						DataObject dataObject = new DataObject();
						dataObject.LoadXml(value4);
						_embeddedObjects.Add(dataObject);
					}
				}
				num += xmlNodeList4.Count;
			}
			XmlNodeList xmlNodeList5 = value.SelectNodes("//*[@Id]", xmlNamespaceManager);
			if (xmlNodeList5 != null)
			{
				foreach (XmlNode item3 in xmlNodeList5)
				{
					_referencedItems.Add(item3);
				}
			}
			if (value.SelectNodes("*").Count != num)
			{
				throw new CryptographicException("Malformed element {0}.", "Signature");
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.DataObject" /> to the list of objects to be signed.</summary>
		/// <param name="dataObject">The <see cref="T:System.Security.Cryptography.Xml.DataObject" /> to be added to the list of objects to be signed.</param>
		public void AddObject(DataObject dataObject)
		{
			_embeddedObjects.Add(dataObject);
		}
	}
	/// <summary>Contains information about the canonicalization algorithm and signature algorithm used for the XML signature.</summary>
	public class SignedInfo : ICollection, IEnumerable
	{
		private string _id;

		private string _canonicalizationMethod;

		private string _signatureMethod;

		private string _signatureLength;

		private ArrayList _references;

		private XmlElement _cachedXml;

		private SignedXml _signedXml;

		private Transform _canonicalizationMethodTransform;

		internal SignedXml SignedXml
		{
			get
			{
				return _signedXml;
			}
			set
			{
				_signedXml = value;
			}
		}

		/// <summary>Gets the number of references in the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The number of references in the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		public int Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether the collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		public bool IsReadOnly
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether the collection is synchronized.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is synchronized; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		public bool IsSynchronized
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets an object to use for synchronization.</summary>
		/// <returns>An object to use for synchronization.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		public object SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the ID of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the canonicalization algorithm that is used before signing for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The canonicalization algorithm used before signing for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		public string CanonicalizationMethod
		{
			get
			{
				if (_canonicalizationMethod == null)
				{
					return "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
				}
				return _canonicalizationMethod;
			}
			set
			{
				_canonicalizationMethod = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object used for canonicalization.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.Transform" /> object used for canonicalization.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <see cref="T:System.Security.Cryptography.Xml.Transform" /> is <see langword="null" />.</exception>
		public Transform CanonicalizationMethodObject
		{
			get
			{
				if (_canonicalizationMethodTransform == null)
				{
					_canonicalizationMethodTransform = CryptoHelpers.CreateFromName<Transform>(CanonicalizationMethod);
					if (_canonicalizationMethodTransform == null)
					{
						throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, "Could not create the XML transformation identified by the URI {0}.", CanonicalizationMethod));
					}
					_canonicalizationMethodTransform.SignedXml = SignedXml;
					_canonicalizationMethodTransform.Reference = null;
				}
				return _canonicalizationMethodTransform;
			}
		}

		/// <summary>Gets or sets the name of the algorithm used for signature generation and validation for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The name of the algorithm used for signature generation and validation for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		public string SignatureMethod
		{
			get
			{
				return _signatureMethod;
			}
			set
			{
				_signatureMethod = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets or sets the length of the signature for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The length of the signature for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		public string SignatureLength
		{
			get
			{
				return _signatureLength;
			}
			set
			{
				_signatureLength = value;
				_cachedXml = null;
			}
		}

		/// <summary>Gets a list of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> objects of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>A list of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> elements of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		public ArrayList References => _references;

		internal bool CacheValid
		{
			get
			{
				if (_cachedXml == null)
				{
					return false;
				}
				foreach (Reference reference in References)
				{
					if (!reference.CacheValid)
					{
						return false;
					}
				}
				return true;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> class.</summary>
		public SignedInfo()
		{
			_references = new ArrayList();
		}

		/// <summary>Returns an enumerator that iterates through the collection of references.</summary>
		/// <returns>An enumerator that iterates through the collection of references.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		public IEnumerator GetEnumerator()
		{
			throw new NotSupportedException();
		}

		/// <summary>Copies the elements of this instance into an <see cref="T:System.Array" /> object, starting at a specified index in the array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object that holds the collection's elements.</param>
		/// <param name="index">The beginning index in the array where the elements are copied.</param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		public void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> instance.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.SignedInfo.SignatureMethod" /> property is <see langword="null" />.  
		///  -or-  
		///  The <see cref="P:System.Security.Cryptography.Xml.SignedInfo.References" /> property is empty.</exception>
		public XmlElement GetXml()
		{
			if (CacheValid)
			{
				return _cachedXml;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("SignedInfo", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(_id))
			{
				xmlElement.SetAttribute("Id", _id);
			}
			XmlElement xml = CanonicalizationMethodObject.GetXml(document, "CanonicalizationMethod");
			xmlElement.AppendChild(xml);
			if (string.IsNullOrEmpty(_signatureMethod))
			{
				throw new CryptographicException("A signature method is required.");
			}
			XmlElement xmlElement2 = document.CreateElement("SignatureMethod", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement2.SetAttribute("Algorithm", _signatureMethod);
			if (_signatureLength != null)
			{
				XmlElement xmlElement3 = document.CreateElement(null, "HMACOutputLength", "http://www.w3.org/2000/09/xmldsig#");
				XmlText newChild = document.CreateTextNode(_signatureLength);
				xmlElement3.AppendChild(newChild);
				xmlElement2.AppendChild(xmlElement3);
			}
			xmlElement.AppendChild(xmlElement2);
			if (_references.Count == 0)
			{
				throw new CryptographicException("At least one Reference element is required.");
			}
			for (int i = 0; i < _references.Count; i++)
			{
				Reference reference = (Reference)_references[i];
				xmlElement.AppendChild(reference.GetXml(document));
			}
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> state.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter is not a valid <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> element.  
		///  -or-  
		///  The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.SignedInfo.CanonicalizationMethod" /> property.  
		///  -or-  
		///  The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.SignedInfo.SignatureMethod" /> property.</exception>
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!value.LocalName.Equals("SignedInfo"))
			{
				throw new CryptographicException("Malformed element {0}.", "SignedInfo");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			int num = 0;
			_id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			if (!Utils.VerifyAttributes(value, "Id"))
			{
				throw new CryptographicException("Malformed element {0}.", "SignedInfo");
			}
			XmlNodeList xmlNodeList = value.SelectNodes("ds:CanonicalizationMethod", xmlNamespaceManager);
			if (xmlNodeList == null || xmlNodeList.Count == 0 || xmlNodeList.Count > 1)
			{
				throw new CryptographicException("Malformed element {0}.", "SignedInfo/CanonicalizationMethod");
			}
			XmlElement xmlElement = xmlNodeList.Item(0) as XmlElement;
			num += xmlNodeList.Count;
			_canonicalizationMethod = Utils.GetAttribute(xmlElement, "Algorithm", "http://www.w3.org/2000/09/xmldsig#");
			if (_canonicalizationMethod == null || !Utils.VerifyAttributes(xmlElement, "Algorithm"))
			{
				throw new CryptographicException("Malformed element {0}.", "SignedInfo/CanonicalizationMethod");
			}
			_canonicalizationMethodTransform = null;
			if (xmlElement.ChildNodes.Count > 0)
			{
				CanonicalizationMethodObject.LoadInnerXml(xmlElement.ChildNodes);
			}
			XmlNodeList xmlNodeList2 = value.SelectNodes("ds:SignatureMethod", xmlNamespaceManager);
			if (xmlNodeList2 == null || xmlNodeList2.Count == 0 || xmlNodeList2.Count > 1)
			{
				throw new CryptographicException("Malformed element {0}.", "SignedInfo/SignatureMethod");
			}
			XmlElement xmlElement2 = xmlNodeList2.Item(0) as XmlElement;
			num += xmlNodeList2.Count;
			_signatureMethod = Utils.GetAttribute(xmlElement2, "Algorithm", "http://www.w3.org/2000/09/xmldsig#");
			if (_signatureMethod == null || !Utils.VerifyAttributes(xmlElement2, "Algorithm"))
			{
				throw new CryptographicException("Malformed element {0}.", "SignedInfo/SignatureMethod");
			}
			if (xmlElement2.SelectSingleNode("ds:HMACOutputLength", xmlNamespaceManager) is XmlElement xmlElement3)
			{
				_signatureLength = xmlElement3.InnerXml;
			}
			_references.Clear();
			XmlNodeList xmlNodeList3 = value.SelectNodes("ds:Reference", xmlNamespaceManager);
			if (xmlNodeList3 != null)
			{
				if (xmlNodeList3.Count > 100)
				{
					throw new CryptographicException("Malformed element {0}.", "SignedInfo/Reference");
				}
				foreach (XmlNode item in xmlNodeList3)
				{
					XmlElement value2 = item as XmlElement;
					Reference reference = new Reference();
					AddReference(reference);
					reference.LoadXml(value2);
				}
				num += xmlNodeList3.Count;
				if (value.SelectNodes("*").Count != num)
				{
					throw new CryptographicException("Malformed element {0}.", "SignedInfo");
				}
			}
			_cachedXml = value;
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Reference" /> object to the list of references to digest and sign.</summary>
		/// <param name="reference">The reference to add to the list of references.</param>
		/// <exception cref="T:System.ArgumentNullException">The reference parameter is <see langword="null" />.</exception>
		public void AddReference(Reference reference)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			reference.SignedXml = SignedXml;
			_references.Add(reference);
		}
	}
	/// <summary>Provides a wrapper on a core XML signature object to facilitate creating XML signatures.</summary>
	public class SignedXml
	{
		private class ReferenceLevelSortOrder : IComparer
		{
			private ArrayList _references;

			public ArrayList References
			{
				get
				{
					return _references;
				}
				set
				{
					_references = value;
				}
			}

			public int Compare(object a, object b)
			{
				Reference reference = a as Reference;
				Reference reference2 = b as Reference;
				int index = 0;
				int index2 = 0;
				int num = 0;
				foreach (Reference reference3 in References)
				{
					if (reference3 == reference)
					{
						index = num;
					}
					if (reference3 == reference2)
					{
						index2 = num;
					}
					num++;
				}
				int referenceLevel = reference.SignedXml.GetReferenceLevel(index, References);
				int referenceLevel2 = reference2.SignedXml.GetReferenceLevel(index2, References);
				return referenceLevel.CompareTo(referenceLevel2);
			}
		}

		/// <summary>Represents the <see cref="T:System.Security.Cryptography.Xml.Signature" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		protected Signature m_signature;

		/// <summary>Represents the name of the installed key to be used for signing the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		protected string m_strSigningKeyName;

		private AsymmetricAlgorithm _signingKey;

		private XmlDocument _containingDocument;

		private IEnumerator _keyInfoEnum;

		private X509Certificate2Collection _x509Collection;

		private IEnumerator _x509Enum;

		private bool[] _refProcessed;

		private int[] _refLevelCache;

		internal XmlResolver _xmlResolver;

		internal XmlElement _context;

		private bool _bResolverSet;

		private Func<SignedXml, bool> _signatureFormatValidator = DefaultSignatureFormatValidator;

		private Collection<string> _safeCanonicalizationMethods;

		private static IList<string> s_knownCanonicalizationMethods;

		private static IList<string> s_defaultSafeTransformMethods;

		private const string XmlDsigMoreHMACMD5Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-md5";

		private const string XmlDsigMoreHMACSHA256Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";

		private const string XmlDsigMoreHMACSHA384Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";

		private const string XmlDsigMoreHMACSHA512Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";

		private const string XmlDsigMoreHMACRIPEMD160Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160";

		private EncryptedXml _exml;

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard namespace for XML digital signatures. This field is constant.</summary>
		public const string XmlDsigNamespaceUrl = "http://www.w3.org/2000/09/xmldsig#";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard minimal canonicalization algorithm for XML digital signatures. This field is constant.</summary>
		public const string XmlDsigMinimalCanonicalizationUrl = "http://www.w3.org/2000/09/xmldsig#minimal";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard canonicalization algorithm for XML digital signatures. This field is constant.</summary>
		public const string XmlDsigCanonicalizationUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard canonicalization algorithm for XML digital signatures and includes comments. This field is constant.</summary>
		public const string XmlDsigCanonicalizationWithCommentsUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.SHA1" /> digest method for XML digital signatures. This field is constant.</summary>
		public const string XmlDsigSHA1Url = "http://www.w3.org/2000/09/xmldsig#sha1";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.DSA" /> algorithm for XML digital signatures. This field is constant.</summary>
		public const string XmlDsigDSAUrl = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.RSA" /> signature method for XML digital signatures. This field is constant.</summary>
		public const string XmlDsigRSASHA1Url = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.HMACSHA1" /> algorithm for XML digital signatures. This field is constant.</summary>
		public const string XmlDsigHMACSHA1Url = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.SHA256" /> digest method for XML digital signatures. This field is constant.</summary>
		public const string XmlDsigSHA256Url = "http://www.w3.org/2001/04/xmlenc#sha256";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the  <see cref="T:System.Security.Cryptography.RSA" /> SHA-256 signature method variation for XML digital signatures. This field is constant.</summary>
		public const string XmlDsigRSASHA256Url = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.SHA384" /> digest method for XML digital signatures. This field is constant.</summary>
		public const string XmlDsigSHA384Url = "http://www.w3.org/2001/04/xmldsig-more#sha384";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the  <see cref="T:System.Security.Cryptography.RSA" /> SHA-384 signature method variation for XML digital signatures. This field is constant.</summary>
		public const string XmlDsigRSASHA384Url = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the standard <see cref="T:System.Security.Cryptography.SHA512" /> digest method for XML digital signatures. This field is constant.</summary>
		public const string XmlDsigSHA512Url = "http://www.w3.org/2001/04/xmlenc#sha512";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the  <see cref="T:System.Security.Cryptography.RSA" /> SHA-512 signature method variation for XML digital signatures. This field is constant.</summary>
		public const string XmlDsigRSASHA512Url = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the Canonical XML transformation. This field is constant.</summary>
		public const string XmlDsigC14NTransformUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the Canonical XML transformation, with comments. This field is constant.</summary>
		public const string XmlDsigC14NWithCommentsTransformUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

		/// <summary>Represents the Uniform Resource Identifier (URI) for exclusive XML canonicalization. This field is constant.</summary>
		public const string XmlDsigExcC14NTransformUrl = "http://www.w3.org/2001/10/xml-exc-c14n#";

		/// <summary>Represents the Uniform Resource Identifier (URI) for exclusive XML canonicalization, with comments. This field is constant.</summary>
		public const string XmlDsigExcC14NWithCommentsTransformUrl = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the base 64 transformation. This field is constant.</summary>
		public const string XmlDsigBase64TransformUrl = "http://www.w3.org/2000/09/xmldsig#base64";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the XML Path Language (XPath). This field is constant.</summary>
		public const string XmlDsigXPathTransformUrl = "http://www.w3.org/TR/1999/REC-xpath-19991116";

		/// <summary>Represents the Uniform Resource Identifier (URI) for XSLT transformations. This field is constant.</summary>
		public const string XmlDsigXsltTransformUrl = "http://www.w3.org/TR/1999/REC-xslt-19991116";

		/// <summary>Represents the Uniform Resource Identifier (URI) for enveloped signature transformation. This field is constant.</summary>
		public const string XmlDsigEnvelopedSignatureTransformUrl = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the XML mode decryption transformation. This field is constant.</summary>
		public const string XmlDecryptionTransformUrl = "http://www.w3.org/2002/07/decrypt#XML";

		/// <summary>Represents the Uniform Resource Identifier (URI) for the license transform algorithm used to normalize XrML licenses for signatures.</summary>
		public const string XmlLicenseTransformUrl = "urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform";

		private bool _bCacheValid;

		private byte[] _digestedSignedInfo;

		/// <summary>Gets or sets the name of the installed key to be used for signing the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The name of the installed key to be used for signing the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		public string SigningKeyName
		{
			get
			{
				return m_strSigningKeyName;
			}
			set
			{
				m_strSigningKeyName = value;
			}
		}

		/// <summary>Sets the current <see cref="T:System.Xml.XmlResolver" /> object.</summary>
		/// <returns>The current <see cref="T:System.Xml.XmlResolver" /> object. The defaults is a <see cref="T:System.Xml.XmlSecureResolver" /> object.</returns>
		public XmlResolver Resolver
		{
			set
			{
				_xmlResolver = value;
				_bResolverSet = true;
			}
		}

		internal bool ResolverSet => _bResolverSet;

		/// <summary>Gets a delegate that will be called to validate the format (not the cryptographic security) of an XML signature.</summary>
		/// <returns>
		///   <see langword="true" /> if the format is acceptable; otherwise, <see langword="false" />.</returns>
		public Func<SignedXml, bool> SignatureFormatValidator
		{
			get
			{
				return _signatureFormatValidator;
			}
			set
			{
				_signatureFormatValidator = value;
			}
		}

		/// <summary>Gets the names of methods whose canonicalization algorithms are explicitly allowed.</summary>
		/// <returns>A collection of the names of methods that safely produce canonical XML.</returns>
		public Collection<string> SafeCanonicalizationMethods => _safeCanonicalizationMethods;

		/// <summary>Gets or sets the asymmetric algorithm key used for signing a <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The asymmetric algorithm key used for signing the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		public AsymmetricAlgorithm SigningKey
		{
			get
			{
				return _signingKey;
			}
			set
			{
				_signingKey = value;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object that defines the XML encryption processing rules.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object that defines the XML encryption processing rules.</returns>
		public EncryptedXml EncryptedXml
		{
			get
			{
				if (_exml == null)
				{
					_exml = new EncryptedXml(_containingDocument);
				}
				return _exml;
			}
			set
			{
				_exml = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.Xml.Signature" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.Signature" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		public Signature Signature => m_signature;

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		public SignedInfo SignedInfo => m_signature.SignedInfo;

		/// <summary>Gets the signature method of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The signature method of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		public string SignatureMethod => m_signature.SignedInfo.SignatureMethod;

		/// <summary>Gets the length of the signature for the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The length of the signature for the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		public string SignatureLength => m_signature.SignedInfo.SignatureLength;

		/// <summary>Gets the signature value of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>A byte array that contains the signature value of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		public byte[] SignatureValue => m_signature.SignatureValue;

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object of the current <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</returns>
		public KeyInfo KeyInfo
		{
			get
			{
				return m_signature.KeyInfo;
			}
			set
			{
				m_signature.KeyInfo = value;
			}
		}

		private static IList<string> KnownCanonicalizationMethods
		{
			get
			{
				if (s_knownCanonicalizationMethods == null)
				{
					s_knownCanonicalizationMethods = new List<string> { "http://www.w3.org/TR/2001/REC-xml-c14n-20010315", "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments", "http://www.w3.org/2001/10/xml-exc-c14n#", "http://www.w3.org/2001/10/xml-exc-c14n#WithComments" };
				}
				return s_knownCanonicalizationMethods;
			}
		}

		private static IList<string> DefaultSafeTransformMethods
		{
			get
			{
				if (s_defaultSafeTransformMethods == null)
				{
					s_defaultSafeTransformMethods = new List<string> { "http://www.w3.org/2000/09/xmldsig#enveloped-signature", "http://www.w3.org/2000/09/xmldsig#base64", "urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform", "http://www.w3.org/2002/07/decrypt#XML" };
				}
				return s_defaultSafeTransformMethods;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> class.</summary>
		public SignedXml()
		{
			Initialize(null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> class from the specified XML document.</summary>
		/// <param name="document">The <see cref="T:System.Xml.XmlDocument" /> object to use to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.SignedXml" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="document" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="document" /> parameter contains a null <see cref="P:System.Xml.XmlDocument.DocumentElement" /> property.</exception>
		public SignedXml(XmlDocument document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			Initialize(document.DocumentElement);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> class from the specified <see cref="T:System.Xml.XmlElement" /> object.</summary>
		/// <param name="elem">The <see cref="T:System.Xml.XmlElement" /> object to use to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.SignedXml" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elem" /> parameter is <see langword="null" />.</exception>
		public SignedXml(XmlElement elem)
		{
			if (elem == null)
			{
				throw new ArgumentNullException("elem");
			}
			Initialize(elem);
		}

		private void Initialize(XmlElement element)
		{
			_containingDocument = element?.OwnerDocument;
			_context = element;
			m_signature = new Signature();
			m_signature.SignedXml = this;
			m_signature.SignedInfo = new SignedInfo();
			_signingKey = null;
			_safeCanonicalizationMethods = new Collection<string>(KnownCanonicalizationMethods);
		}

		/// <summary>Returns the XML representation of a <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.Signature" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignedInfo" /> property is <see langword="null" />.  
		///  -or-  
		///  The <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureValue" /> property is <see langword="null" />.</exception>
		public XmlElement GetXml()
		{
			if (_containingDocument != null)
			{
				return m_signature.GetXml(_containingDocument);
			}
			return m_signature.GetXml();
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> state from an XML element.</summary>
		/// <param name="value">The XML element to load the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> state from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureValue" /> property.  
		///  -or-  
		///  The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignedInfo" /> property.</exception>
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			m_signature.LoadXml(value);
			if (_context == null)
			{
				_context = value;
			}
			_bCacheValid = false;
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Reference" /> object to the <see cref="T:System.Security.Cryptography.Xml.SignedXml" /> object that describes a digest method, digest value, and transform to use for creating an XML digital signature.</summary>
		/// <param name="reference">The  <see cref="T:System.Security.Cryptography.Xml.Reference" /> object that describes a digest method, digest value, and transform to use for creating an XML digital signature.</param>
		public void AddReference(Reference reference)
		{
			m_signature.SignedInfo.AddReference(reference);
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object to the list of objects to be signed.</summary>
		/// <param name="dataObject">The <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object to add to the list of objects to be signed.</param>
		public void AddObject(DataObject dataObject)
		{
			m_signature.AddObject(dataObject);
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies using the public key in the signature.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.AsymmetricAlgorithm.SignatureAlgorithm" /> property of the public key in the signature does not match the <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureMethod" /> property.  
		///  -or-  
		///  The signature description could not be created.  
		///  -or  
		///  The hash algorithm could not be created.</exception>
		public bool CheckSignature()
		{
			AsymmetricAlgorithm signingKey;
			return CheckSignatureReturningKey(out signingKey);
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies using the public key in the signature.</summary>
		/// <param name="signingKey">When this method returns, contains the implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> that holds the public key in the signature. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies using the public key in the signature; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="signingKey" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.AsymmetricAlgorithm.SignatureAlgorithm" /> property of the public key in the signature does not match the <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureMethod" /> property.  
		///  -or-  
		///  The signature description could not be created.  
		///  -or  
		///  The hash algorithm could not be created.</exception>
		public bool CheckSignatureReturningKey(out AsymmetricAlgorithm signingKey)
		{
			SignedXmlDebugLog.LogBeginSignatureVerification(this, _context);
			signingKey = null;
			bool flag = false;
			AsymmetricAlgorithm asymmetricAlgorithm = null;
			if (!CheckSignatureFormat())
			{
				return false;
			}
			do
			{
				asymmetricAlgorithm = GetPublicKey();
				if (asymmetricAlgorithm != null)
				{
					flag = CheckSignature(asymmetricAlgorithm);
					SignedXmlDebugLog.LogVerificationResult(this, asymmetricAlgorithm, flag);
				}
			}
			while (asymmetricAlgorithm != null && !flag);
			signingKey = asymmetricAlgorithm;
			return flag;
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified key.</summary>
		/// <param name="key">The implementation of the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> property that holds the key to be used to verify the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.AsymmetricAlgorithm.SignatureAlgorithm" /> property of the <paramref name="key" /> parameter does not match the <see cref="P:System.Security.Cryptography.Xml.SignedXml.SignatureMethod" /> property.  
		///  -or-  
		///  The signature description could not be created.  
		///  -or  
		///  The hash algorithm could not be created.</exception>
		public bool CheckSignature(AsymmetricAlgorithm key)
		{
			if (!CheckSignatureFormat())
			{
				return false;
			}
			if (!CheckSignedInfo(key))
			{
				SignedXmlDebugLog.LogVerificationFailure(this, "SignedInfo");
				return false;
			}
			if (!CheckDigestedReferences())
			{
				SignedXmlDebugLog.LogVerificationFailure(this, "references");
				return false;
			}
			SignedXmlDebugLog.LogVerificationResult(this, key, verified: true);
			return true;
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified message authentication code (MAC) algorithm.</summary>
		/// <param name="macAlg">The implementation of <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> that holds the MAC to be used to verify the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified MAC; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="macAlg" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.HashAlgorithm.HashSize" /> property of the specified <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> object is not valid.  
		///  -or-  
		///  The <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property is <see langword="null" />.  
		///  -or-  
		///  The cryptographic transform used to check the signature could not be created.</exception>
		public bool CheckSignature(KeyedHashAlgorithm macAlg)
		{
			if (!CheckSignatureFormat())
			{
				return false;
			}
			if (!CheckSignedInfo(macAlg))
			{
				SignedXmlDebugLog.LogVerificationFailure(this, "SignedInfo");
				return false;
			}
			if (!CheckDigestedReferences())
			{
				SignedXmlDebugLog.LogVerificationFailure(this, "references");
				return false;
			}
			SignedXmlDebugLog.LogVerificationResult(this, macAlg, verified: true);
			return true;
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property verifies for the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object and, optionally, whether the certificate is valid.</summary>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to use to verify the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property.</param>
		/// <param name="verifySignatureOnly">
		///   <see langword="true" /> to verify the signature only; <see langword="false" /> to verify both the signature and certificate.</param>
		/// <returns>
		///   <see langword="true" /> if the signature is valid; otherwise, <see langword="false" />.  
		/// -or-  
		/// <see langword="true" /> if the signature and certificate are valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A signature description could not be created for the <paramref name="certificate" /> parameter.</exception>
		public bool CheckSignature(X509Certificate2 certificate, bool verifySignatureOnly)
		{
			if (!verifySignatureOnly)
			{
				X509ExtensionEnumerator enumerator = certificate.Extensions.GetEnumerator();
				while (enumerator.MoveNext())
				{
					X509Extension current = enumerator.Current;
					if (string.Compare(current.Oid.Value, "2.5.29.15", StringComparison.OrdinalIgnoreCase) == 0)
					{
						X509KeyUsageExtension x509KeyUsageExtension = new X509KeyUsageExtension();
						x509KeyUsageExtension.CopyFrom(current);
						SignedXmlDebugLog.LogVerifyKeyUsage(this, certificate, x509KeyUsageExtension);
						if ((x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.DigitalSignature) != X509KeyUsageFlags.None || (x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.NonRepudiation) != X509KeyUsageFlags.None)
						{
							break;
						}
						SignedXmlDebugLog.LogVerificationFailure(this, "X509 key usage verification");
						return false;
					}
				}
				X509Chain x509Chain = new X509Chain();
				x509Chain.ChainPolicy.ExtraStore.AddRange(BuildBagOfCerts());
				bool num = x509Chain.Build(certificate);
				SignedXmlDebugLog.LogVerifyX509Chain(this, x509Chain, certificate);
				if (!num)
				{
					SignedXmlDebugLog.LogVerificationFailure(this, "X509 chain verification");
					return false;
				}
			}
			using (AsymmetricAlgorithm key = Utils.GetAnyPublicKey(certificate))
			{
				if (!CheckSignature(key))
				{
					return false;
				}
			}
			SignedXmlDebugLog.LogVerificationResult(this, certificate, verified: true);
			return true;
		}

		/// <summary>Computes an XML digital signature.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.SignedXml.SigningKey" /> property is <see langword="null" />.  
		///  -or-  
		///  The <see cref="P:System.Security.Cryptography.Xml.SignedXml.SigningKey" /> property is not a <see cref="T:System.Security.Cryptography.DSA" /> object or <see cref="T:System.Security.Cryptography.RSA" /> object.  
		///  -or-  
		///  The key could not be loaded.</exception>
		public void ComputeSignature()
		{
			SignedXmlDebugLog.LogBeginSignatureComputation(this, _context);
			BuildDigestedReferences();
			AsymmetricAlgorithm signingKey = SigningKey;
			if (signingKey == null)
			{
				throw new CryptographicException("Signing key is not loaded.");
			}
			if (SignedInfo.SignatureMethod == null)
			{
				if (signingKey is DSA)
				{
					SignedInfo.SignatureMethod = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
				}
				else
				{
					if (!(signingKey is RSA))
					{
						throw new CryptographicException("Failed to create signing key.");
					}
					if (SignedInfo.SignatureMethod == null)
					{
						SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
					}
				}
			}
			SignatureDescription signatureDescription = CryptoHelpers.CreateFromName<SignatureDescription>(SignedInfo.SignatureMethod);
			if (signatureDescription == null)
			{
				throw new CryptographicException("SignatureDescription could not be created for the signature algorithm supplied.");
			}
			HashAlgorithm hashAlgorithm = signatureDescription.CreateDigest();
			if (hashAlgorithm == null)
			{
				throw new CryptographicException("Could not create hash algorithm object.");
			}
			GetC14NDigest(hashAlgorithm);
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = signatureDescription.CreateFormatter(signingKey);
			SignedXmlDebugLog.LogSigning(this, signingKey, signatureDescription, hashAlgorithm, asymmetricSignatureFormatter);
			m_signature.SignatureValue = asymmetricSignatureFormatter.CreateSignature(hashAlgorithm);
		}

		/// <summary>Computes an XML digital signature using the specified message authentication code (MAC) algorithm.</summary>
		/// <param name="macAlg">A <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> object that holds the MAC to be used to compute the value of the <see cref="P:System.Security.Cryptography.Xml.SignedXml.Signature" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="macAlg" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> object specified by the <paramref name="macAlg" /> parameter is not an instance of <see cref="T:System.Security.Cryptography.HMACSHA1" />.  
		///  -or-  
		///  The <see cref="P:System.Security.Cryptography.HashAlgorithm.HashSize" /> property of the specified <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> object is not valid.  
		///  -or-  
		///  The cryptographic transform used to check the signature could not be created.</exception>
		public void ComputeSignature(KeyedHashAlgorithm macAlg)
		{
			if (macAlg == null)
			{
				throw new ArgumentNullException("macAlg");
			}
			if (!(macAlg is HMAC hMAC))
			{
				throw new CryptographicException("The key does not fit the SignatureMethod.");
			}
			int num = ((m_signature.SignedInfo.SignatureLength != null) ? Convert.ToInt32(m_signature.SignedInfo.SignatureLength, null) : hMAC.HashSize);
			if (num < 0 || num > hMAC.HashSize)
			{
				throw new CryptographicException("The length of the signature with a MAC should be less than the hash output length.");
			}
			if (num % 8 != 0)
			{
				throw new CryptographicException("The length in bits of the signature with a MAC should be a multiple of 8.");
			}
			BuildDigestedReferences();
			switch (hMAC.HashName)
			{
			case "SHA1":
				SignedInfo.SignatureMethod = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";
				break;
			case "SHA256":
				SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
				break;
			case "SHA384":
				SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";
				break;
			case "SHA512":
				SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";
				break;
			case "MD5":
				SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-md5";
				break;
			case "RIPEMD160":
				SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160";
				break;
			default:
				throw new CryptographicException("The key does not fit the SignatureMethod.");
			}
			byte[] c14NDigest = GetC14NDigest(hMAC);
			SignedXmlDebugLog.LogSigning(this, hMAC);
			m_signature.SignatureValue = new byte[num / 8];
			Buffer.BlockCopy(c14NDigest, 0, m_signature.SignatureValue, 0, num / 8);
		}

		/// <summary>Returns the public key of a signature.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object that contains the public key of the signature, or <see langword="null" /> if the key cannot be found.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.SignedXml.KeyInfo" /> property is <see langword="null" />.</exception>
		protected virtual AsymmetricAlgorithm GetPublicKey()
		{
			if (KeyInfo == null)
			{
				throw new CryptographicException("A KeyInfo element is required to check the signature.");
			}
			if (_x509Enum != null)
			{
				AsymmetricAlgorithm nextCertificatePublicKey = GetNextCertificatePublicKey();
				if (nextCertificatePublicKey != null)
				{
					return nextCertificatePublicKey;
				}
			}
			if (_keyInfoEnum == null)
			{
				_keyInfoEnum = KeyInfo.GetEnumerator();
			}
			while (_keyInfoEnum.MoveNext())
			{
				if (_keyInfoEnum.Current is RSAKeyValue rSAKeyValue)
				{
					return rSAKeyValue.Key;
				}
				if (_keyInfoEnum.Current is DSAKeyValue dSAKeyValue)
				{
					return dSAKeyValue.Key;
				}
				if (!(_keyInfoEnum.Current is KeyInfoX509Data keyInfoX509Data))
				{
					continue;
				}
				_x509Collection = Utils.BuildBagOfCerts(keyInfoX509Data, CertUsageType.Verification);
				if (_x509Collection.Count > 0)
				{
					_x509Enum = _x509Collection.GetEnumerator();
					AsymmetricAlgorithm nextCertificatePublicKey2 = GetNextCertificatePublicKey();
					if (nextCertificatePublicKey2 != null)
					{
						return nextCertificatePublicKey2;
					}
				}
			}
			return null;
		}

		private X509Certificate2Collection BuildBagOfCerts()
		{
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			if (KeyInfo != null)
			{
				foreach (KeyInfoClause item in KeyInfo)
				{
					if (item is KeyInfoX509Data keyInfoX509Data)
					{
						x509Certificate2Collection.AddRange(Utils.BuildBagOfCerts(keyInfoX509Data, CertUsageType.Verification));
					}
				}
			}
			return x509Certificate2Collection;
		}

		private AsymmetricAlgorithm GetNextCertificatePublicKey()
		{
			while (_x509Enum.MoveNext())
			{
				X509Certificate2 x509Certificate = (X509Certificate2)_x509Enum.Current;
				if (x509Certificate != null)
				{
					return Utils.GetAnyPublicKey(x509Certificate);
				}
			}
			return null;
		}

		/// <summary>Returns the <see cref="T:System.Xml.XmlElement" /> object with the specified ID from the specified <see cref="T:System.Xml.XmlDocument" /> object.</summary>
		/// <param name="document">The <see cref="T:System.Xml.XmlDocument" /> object to retrieve the <see cref="T:System.Xml.XmlElement" /> object from.</param>
		/// <param name="idValue">The ID of the <see cref="T:System.Xml.XmlElement" /> object to retrieve from the <see cref="T:System.Xml.XmlDocument" /> object.</param>
		/// <returns>The <see cref="T:System.Xml.XmlElement" /> object with the specified ID from the specified <see cref="T:System.Xml.XmlDocument" /> object, or <see langword="null" /> if it could not be found.</returns>
		public virtual XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			return DefaultGetIdElement(document, idValue);
		}

		internal static XmlElement DefaultGetIdElement(XmlDocument document, string idValue)
		{
			if (document == null)
			{
				return null;
			}
			try
			{
				XmlConvert.VerifyNCName(idValue);
			}
			catch (XmlException)
			{
				return null;
			}
			XmlElement elementById = document.GetElementById(idValue);
			if (elementById != null)
			{
				XmlDocument xmlDocument = (XmlDocument)document.CloneNode(deep: true);
				XmlElement elementById2 = xmlDocument.GetElementById(idValue);
				if (elementById2 != null)
				{
					elementById2.Attributes.RemoveAll();
					if (xmlDocument.GetElementById(idValue) != null)
					{
						throw new CryptographicException("Malformed reference element.");
					}
				}
				return elementById;
			}
			elementById = GetSingleReferenceTarget(document, "Id", idValue);
			if (elementById != null)
			{
				return elementById;
			}
			elementById = GetSingleReferenceTarget(document, "id", idValue);
			if (elementById != null)
			{
				return elementById;
			}
			return GetSingleReferenceTarget(document, "ID", idValue);
		}

		private static bool DefaultSignatureFormatValidator(SignedXml signedXml)
		{
			if (signedXml.DoesSignatureUseTruncatedHmac())
			{
				return false;
			}
			if (!signedXml.DoesSignatureUseSafeCanonicalizationMethod())
			{
				return false;
			}
			return true;
		}

		private bool DoesSignatureUseTruncatedHmac()
		{
			if (SignedInfo.SignatureLength == null)
			{
				return false;
			}
			HMAC hMAC = CryptoHelpers.CreateFromName<HMAC>(SignatureMethod);
			if (hMAC == null)
			{
				return false;
			}
			int result = 0;
			if (!int.TryParse(SignedInfo.SignatureLength, out result))
			{
				return true;
			}
			return result != hMAC.HashSize;
		}

		private bool DoesSignatureUseSafeCanonicalizationMethod()
		{
			foreach (string safeCanonicalizationMethod in SafeCanonicalizationMethods)
			{
				if (string.Equals(safeCanonicalizationMethod, SignedInfo.CanonicalizationMethod, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			SignedXmlDebugLog.LogUnsafeCanonicalizationMethod(this, SignedInfo.CanonicalizationMethod, SafeCanonicalizationMethods);
			return false;
		}

		private bool ReferenceUsesSafeTransformMethods(Reference reference)
		{
			TransformChain transformChain = reference.TransformChain;
			int count = transformChain.Count;
			for (int i = 0; i < count; i++)
			{
				Transform transform = transformChain[i];
				if (!IsSafeTransform(transform.Algorithm))
				{
					return false;
				}
			}
			return true;
		}

		private bool IsSafeTransform(string transformAlgorithm)
		{
			foreach (string safeCanonicalizationMethod in SafeCanonicalizationMethods)
			{
				if (string.Equals(safeCanonicalizationMethod, transformAlgorithm, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			foreach (string defaultSafeTransformMethod in DefaultSafeTransformMethods)
			{
				if (string.Equals(defaultSafeTransformMethod, transformAlgorithm, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			SignedXmlDebugLog.LogUnsafeTransformMethod(this, transformAlgorithm, SafeCanonicalizationMethods, DefaultSafeTransformMethods);
			return false;
		}

		private byte[] GetC14NDigest(HashAlgorithm hash)
		{
			bool flag = hash is KeyedHashAlgorithm;
			if (flag || !_bCacheValid || !SignedInfo.CacheValid)
			{
				string text = ((_containingDocument == null) ? null : _containingDocument.BaseURI);
				XmlResolver xmlResolver = (_bResolverSet ? _xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
				XmlDocument xmlDocument = Utils.PreProcessElementInput(SignedInfo.GetXml(), xmlResolver, text);
				CanonicalXmlNodeList namespaces = ((_context == null) ? null : Utils.GetPropagatedAttributes(_context));
				SignedXmlDebugLog.LogNamespacePropagation(this, namespaces);
				Utils.AddNamespaces(xmlDocument.DocumentElement, namespaces);
				Transform canonicalizationMethodObject = SignedInfo.CanonicalizationMethodObject;
				canonicalizationMethodObject.Resolver = xmlResolver;
				canonicalizationMethodObject.BaseURI = text;
				SignedXmlDebugLog.LogBeginCanonicalization(this, canonicalizationMethodObject);
				canonicalizationMethodObject.LoadInput(xmlDocument);
				SignedXmlDebugLog.LogCanonicalizedOutput(this, canonicalizationMethodObject);
				_digestedSignedInfo = canonicalizationMethodObject.GetDigestedOutput(hash);
				_bCacheValid = !flag;
			}
			return _digestedSignedInfo;
		}

		private int GetReferenceLevel(int index, ArrayList references)
		{
			if (_refProcessed[index])
			{
				return _refLevelCache[index];
			}
			_refProcessed[index] = true;
			Reference reference = (Reference)references[index];
			if (reference.Uri == null || reference.Uri.Length == 0 || (reference.Uri.Length > 0 && reference.Uri[0] != '#'))
			{
				_refLevelCache[index] = 0;
				return 0;
			}
			if (reference.Uri.Length > 0 && reference.Uri[0] == '#')
			{
				string text = Utils.ExtractIdFromLocalUri(reference.Uri);
				if (text == "xpointer(/)")
				{
					_refLevelCache[index] = 0;
					return 0;
				}
				for (int i = 0; i < references.Count; i++)
				{
					if (((Reference)references[i]).Id == text)
					{
						_refLevelCache[index] = GetReferenceLevel(i, references) + 1;
						return _refLevelCache[index];
					}
				}
				_refLevelCache[index] = 0;
				return 0;
			}
			throw new CryptographicException("Malformed reference element.");
		}

		private void BuildDigestedReferences()
		{
			ArrayList references = SignedInfo.References;
			_refProcessed = new bool[references.Count];
			_refLevelCache = new int[references.Count];
			ReferenceLevelSortOrder referenceLevelSortOrder = new ReferenceLevelSortOrder();
			referenceLevelSortOrder.References = references;
			ArrayList arrayList = new ArrayList();
			foreach (Reference item in references)
			{
				arrayList.Add(item);
			}
			arrayList.Sort(referenceLevelSortOrder);
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			foreach (DataObject @object in m_signature.ObjectList)
			{
				canonicalXmlNodeList.Add(@object.GetXml());
			}
			foreach (Reference item2 in arrayList)
			{
				if (item2.DigestMethod == null)
				{
					item2.DigestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";
				}
				SignedXmlDebugLog.LogSigningReference(this, item2);
				item2.UpdateHashValue(_containingDocument, canonicalXmlNodeList);
				if (item2.Id != null)
				{
					canonicalXmlNodeList.Add(item2.GetXml());
				}
			}
		}

		private bool CheckDigestedReferences()
		{
			ArrayList references = m_signature.SignedInfo.References;
			for (int i = 0; i < references.Count; i++)
			{
				Reference reference = (Reference)references[i];
				if (!ReferenceUsesSafeTransformMethods(reference))
				{
					return false;
				}
				SignedXmlDebugLog.LogVerifyReference(this, reference);
				byte[] array = null;
				try
				{
					array = reference.CalculateHashValue(_containingDocument, m_signature.ReferencedItems);
				}
				catch (CryptoSignedXmlRecursionException)
				{
					SignedXmlDebugLog.LogSignedXmlRecursionLimit(this, reference);
					return false;
				}
				SignedXmlDebugLog.LogVerifyReferenceHash(this, reference, array, reference.DigestValue);
				if (!CryptographicEquals(array, reference.DigestValue))
				{
					return false;
				}
			}
			return true;
		}

		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private static bool CryptographicEquals(byte[] a, byte[] b)
		{
			int num = 0;
			if (a.Length != b.Length)
			{
				return false;
			}
			int num2 = a.Length;
			for (int i = 0; i < num2; i++)
			{
				num |= a[i] - b[i];
			}
			return num == 0;
		}

		private bool CheckSignatureFormat()
		{
			if (_signatureFormatValidator == null)
			{
				return true;
			}
			SignedXmlDebugLog.LogBeginCheckSignatureFormat(this, _signatureFormatValidator);
			bool result = _signatureFormatValidator(this);
			SignedXmlDebugLog.LogFormatValidationResult(this, result);
			return result;
		}

		private bool CheckSignedInfo(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			SignedXmlDebugLog.LogBeginCheckSignedInfo(this, m_signature.SignedInfo);
			SignatureDescription signatureDescription = CryptoHelpers.CreateFromName<SignatureDescription>(SignatureMethod);
			if (signatureDescription == null)
			{
				throw new CryptographicException("SignatureDescription could not be created for the signature algorithm supplied.");
			}
			Type type = Type.GetType(signatureDescription.KeyAlgorithm);
			if (!IsKeyTheCorrectAlgorithm(key, type))
			{
				return false;
			}
			HashAlgorithm hashAlgorithm = signatureDescription.CreateDigest();
			if (hashAlgorithm == null)
			{
				throw new CryptographicException("Could not create hash algorithm object.");
			}
			byte[] c14NDigest = GetC14NDigest(hashAlgorithm);
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = signatureDescription.CreateDeformatter(key);
			SignedXmlDebugLog.LogVerifySignedInfo(this, key, signatureDescription, hashAlgorithm, asymmetricSignatureDeformatter, c14NDigest, m_signature.SignatureValue);
			return asymmetricSignatureDeformatter.VerifySignature(c14NDigest, m_signature.SignatureValue);
		}

		private bool CheckSignedInfo(KeyedHashAlgorithm macAlg)
		{
			if (macAlg == null)
			{
				throw new ArgumentNullException("macAlg");
			}
			SignedXmlDebugLog.LogBeginCheckSignedInfo(this, m_signature.SignedInfo);
			int num = ((m_signature.SignedInfo.SignatureLength != null) ? Convert.ToInt32(m_signature.SignedInfo.SignatureLength, null) : macAlg.HashSize);
			if (num < 0 || num > macAlg.HashSize)
			{
				throw new CryptographicException("The length of the signature with a MAC should be less than the hash output length.");
			}
			if (num % 8 != 0)
			{
				throw new CryptographicException("The length in bits of the signature with a MAC should be a multiple of 8.");
			}
			if (m_signature.SignatureValue == null)
			{
				throw new CryptographicException("Signature requires a SignatureValue.");
			}
			if (m_signature.SignatureValue.Length != num / 8)
			{
				throw new CryptographicException("The length of the signature with a MAC should be less than the hash output length.");
			}
			byte[] c14NDigest = GetC14NDigest(macAlg);
			SignedXmlDebugLog.LogVerifySignedInfo(this, macAlg, c14NDigest, m_signature.SignatureValue);
			for (int i = 0; i < m_signature.SignatureValue.Length; i++)
			{
				if (m_signature.SignatureValue[i] != c14NDigest[i])
				{
					return false;
				}
			}
			return true;
		}

		private static XmlElement GetSingleReferenceTarget(XmlDocument document, string idAttributeName, string idValue)
		{
			string xpath = "//*[@" + idAttributeName + "=\"" + idValue + "\"]";
			XmlNodeList xmlNodeList = document.SelectNodes(xpath);
			if (xmlNodeList == null || xmlNodeList.Count == 0)
			{
				return null;
			}
			if (xmlNodeList.Count == 1)
			{
				return xmlNodeList[0] as XmlElement;
			}
			throw new CryptographicException("Malformed reference element.");
		}

		private static bool IsKeyTheCorrectAlgorithm(AsymmetricAlgorithm key, Type expectedType)
		{
			Type type = key.GetType();
			if (type == expectedType)
			{
				return true;
			}
			if (expectedType.IsSubclassOf(type))
			{
				return true;
			}
			while (expectedType != null && expectedType.BaseType != typeof(AsymmetricAlgorithm))
			{
				expectedType = expectedType.BaseType;
			}
			if (expectedType == null)
			{
				return false;
			}
			if (type.IsSubclassOf(expectedType))
			{
				return true;
			}
			return false;
		}
	}
	internal static class SignedXmlDebugLog
	{
		internal enum SignedXmlDebugEvent
		{
			BeginCanonicalization,
			BeginCheckSignatureFormat,
			BeginCheckSignedInfo,
			BeginSignatureComputation,
			BeginSignatureVerification,
			CanonicalizedData,
			FormatValidationResult,
			NamespacePropagation,
			ReferenceData,
			SignatureVerificationResult,
			Signing,
			SigningReference,
			VerificationFailure,
			VerifyReference,
			VerifySignedInfo,
			X509Verification,
			UnsafeCanonicalizationMethod,
			UnsafeTransformMethod
		}

		private const string NullString = "(null)";

		private static TraceSource s_traceSource = new TraceSource("System.Security.Cryptography.Xml.SignedXml");

		private static volatile bool s_haveVerboseLogging;

		private static volatile bool s_verboseLogging;

		private static volatile bool s_haveInformationLogging;

		private static volatile bool s_informationLogging;

		private static bool InformationLoggingEnabled
		{
			get
			{
				if (!s_haveInformationLogging)
				{
					s_informationLogging = s_traceSource.Switch.ShouldTrace(TraceEventType.Information);
					s_haveInformationLogging = true;
				}
				return s_informationLogging;
			}
		}

		private static bool VerboseLoggingEnabled
		{
			get
			{
				if (!s_haveVerboseLogging)
				{
					s_verboseLogging = s_traceSource.Switch.ShouldTrace(TraceEventType.Verbose);
					s_haveVerboseLogging = true;
				}
				return s_verboseLogging;
			}
		}

		private static string FormatBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return "(null)";
			}
			StringBuilder stringBuilder = new StringBuilder(bytes.Length * 2);
			foreach (byte b in bytes)
			{
				stringBuilder.Append(b.ToString("x2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		private static string GetKeyName(object key)
		{
			ICspAsymmetricAlgorithm cspAsymmetricAlgorithm = key as ICspAsymmetricAlgorithm;
			X509Certificate x509Certificate = key as X509Certificate;
			X509Certificate2 x509Certificate2 = key as X509Certificate2;
			string text = null;
			return string.Format(arg1: (cspAsymmetricAlgorithm != null && cspAsymmetricAlgorithm.CspKeyContainerInfo.KeyContainerName != null) ? string.Format(CultureInfo.InvariantCulture, "\"{0}\"", cspAsymmetricAlgorithm.CspKeyContainerInfo.KeyContainerName) : ((x509Certificate2 != null) ? string.Format(CultureInfo.InvariantCulture, "\"{0}\"", x509Certificate2.GetNameInfo(X509NameType.SimpleName, forIssuer: false)) : ((x509Certificate == null) ? key.GetHashCode().ToString("x8", CultureInfo.InvariantCulture) : string.Format(CultureInfo.InvariantCulture, "\"{0}\"", x509Certificate.Subject))), provider: CultureInfo.InvariantCulture, format: "{0}#{1}", arg0: key.GetType().Name);
		}

		private static string GetObjectId(object o)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}#{1}", o.GetType().Name, o.GetHashCode().ToString("x8", CultureInfo.InvariantCulture));
		}

		private static string GetOidName(Oid oid)
		{
			string text = oid.FriendlyName;
			if (string.IsNullOrEmpty(text))
			{
				text = oid.Value;
			}
			return text;
		}

		internal static void LogBeginCanonicalization(SignedXml signedXml, Transform canonicalizationTransform)
		{
			if (InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Beginning canonicalization using \"{0}\" ({1}).", canonicalizationTransform.Algorithm, canonicalizationTransform.GetType().Name);
				WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.BeginCanonicalization, data);
			}
			if (VerboseLoggingEnabled)
			{
				string data2 = string.Format(CultureInfo.InvariantCulture, "Canonicalization transform is using resolver {0} and base URI \"{1}\".", canonicalizationTransform.Resolver.GetType(), canonicalizationTransform.BaseURI);
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.BeginCanonicalization, data2);
			}
		}

		internal static void LogBeginCheckSignatureFormat(SignedXml signedXml, Func<SignedXml, bool> formatValidator)
		{
			if (InformationLoggingEnabled)
			{
				MethodInfo method = formatValidator.Method;
				string data = string.Format(CultureInfo.InvariantCulture, "Checking signature format using format validator \"[{0}] {1}.{2}\".", method.Module.Assembly.FullName, method.DeclaringType.FullName, method.Name);
				WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.BeginCheckSignatureFormat, data);
			}
		}

		internal static void LogBeginCheckSignedInfo(SignedXml signedXml, SignedInfo signedInfo)
		{
			if (InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Checking signature on SignedInfo with id \"{0}\".", (signedInfo.Id != null) ? signedInfo.Id : "(null)");
				WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.BeginCheckSignedInfo, data);
			}
		}

		internal static void LogBeginSignatureComputation(SignedXml signedXml, XmlElement context)
		{
			if (InformationLoggingEnabled)
			{
				WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.BeginSignatureComputation, "Beginning signature computation.");
			}
			if (VerboseLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Using context: {0}", (context != null) ? context.OuterXml : "(null)");
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.BeginSignatureComputation, data);
			}
		}

		internal static void LogBeginSignatureVerification(SignedXml signedXml, XmlElement context)
		{
			if (InformationLoggingEnabled)
			{
				WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.BeginSignatureVerification, "Beginning signature verification.");
			}
			if (VerboseLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Using context: {0}", (context != null) ? context.OuterXml : "(null)");
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.BeginSignatureVerification, data);
			}
		}

		internal static void LogCanonicalizedOutput(SignedXml signedXml, Transform canonicalizationTransform)
		{
			if (VerboseLoggingEnabled)
			{
				using (StreamReader streamReader = new StreamReader(canonicalizationTransform.GetOutput(typeof(Stream)) as Stream))
				{
					string data = string.Format(CultureInfo.InvariantCulture, "Output of canonicalization transform: {0}", streamReader.ReadToEnd());
					WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.CanonicalizedData, data);
				}
			}
		}

		internal static void LogFormatValidationResult(SignedXml signedXml, bool result)
		{
			if (InformationLoggingEnabled)
			{
				string data = (result ? "Signature format validation was successful." : "Signature format validation failed.");
				WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.FormatValidationResult, data);
			}
		}

		internal static void LogUnsafeCanonicalizationMethod(SignedXml signedXml, string algorithm, IEnumerable<string> validAlgorithms)
		{
			if (!InformationLoggingEnabled)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string validAlgorithm in validAlgorithms)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.AppendFormat("\"{0}\"", validAlgorithm);
			}
			string data = string.Format(CultureInfo.InvariantCulture, "Canonicalization method \"{0}\" is not on the safe list. Safe canonicalization methods are: {1}.", algorithm, stringBuilder.ToString());
			WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.UnsafeCanonicalizationMethod, data);
		}

		internal static void LogUnsafeTransformMethod(SignedXml signedXml, string algorithm, IEnumerable<string> validC14nAlgorithms, IEnumerable<string> validTransformAlgorithms)
		{
			if (!InformationLoggingEnabled)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string validC14nAlgorithm in validC14nAlgorithms)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.AppendFormat("\"{0}\"", validC14nAlgorithm);
			}
			foreach (string validTransformAlgorithm in validTransformAlgorithms)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.AppendFormat("\"{0}\"", validTransformAlgorithm);
			}
			string data = string.Format(CultureInfo.InvariantCulture, "Transform method \"{0}\" is not on the safe list. Safe transform methods are: {1}.", algorithm, stringBuilder.ToString());
			WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.UnsafeTransformMethod, data);
		}

		internal static void LogNamespacePropagation(SignedXml signedXml, XmlNodeList namespaces)
		{
			if (!InformationLoggingEnabled)
			{
				return;
			}
			if (namespaces != null)
			{
				foreach (XmlAttribute @namespace in namespaces)
				{
					string data = string.Format(CultureInfo.InvariantCulture, "Propagating namespace {0}=\"{1}\".", @namespace.Name, @namespace.Value);
					WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.NamespacePropagation, data);
				}
				return;
			}
			WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.NamespacePropagation, "No namespaces are being propagated.");
		}

		internal static Stream LogReferenceData(Reference reference, Stream data)
		{
			if (VerboseLoggingEnabled)
			{
				MemoryStream memoryStream = new MemoryStream();
				byte[] array = new byte[4096];
				int num = 0;
				do
				{
					num = data.Read(array, 0, array.Length);
					memoryStream.Write(array, 0, num);
				}
				while (num == array.Length);
				string data2 = string.Format(CultureInfo.InvariantCulture, "Transformed reference contents: {0}", Encoding.UTF8.GetString(memoryStream.ToArray()));
				WriteLine(reference, TraceEventType.Verbose, SignedXmlDebugEvent.ReferenceData, data2);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				return memoryStream;
			}
			return data;
		}

		internal static void LogSigning(SignedXml signedXml, object key, SignatureDescription signatureDescription, HashAlgorithm hash, AsymmetricSignatureFormatter asymmetricSignatureFormatter)
		{
			if (InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Calculating signature with key {0} using signature description {1}, hash algorithm {2}, and asymmetric signature formatter {3}.", GetKeyName(key), signatureDescription.GetType().Name, hash.GetType().Name, asymmetricSignatureFormatter.GetType().Name);
				WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.Signing, data);
			}
		}

		internal static void LogSigning(SignedXml signedXml, KeyedHashAlgorithm key)
		{
			if (InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Calculating signature using keyed hash algorithm {0}.", key.GetType().Name);
				WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.Signing, data);
			}
		}

		internal static void LogSigningReference(SignedXml signedXml, Reference reference)
		{
			if (VerboseLoggingEnabled)
			{
				HashAlgorithm hashAlgorithm = CryptoHelpers.CreateFromName<HashAlgorithm>(reference.DigestMethod);
				string text = ((hashAlgorithm == null) ? "null" : hashAlgorithm.GetType().Name);
				string data = string.Format(CultureInfo.InvariantCulture, "Hashing reference {0}, Uri \"{1}\", Id \"{2}\", Type \"{3}\" with hash algorithm \"{4}\" ({5}).", GetObjectId(reference), reference.Uri, reference.Id, reference.Type, reference.DigestMethod, text);
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.SigningReference, data);
			}
		}

		internal static void LogVerificationFailure(SignedXml signedXml, string failureLocation)
		{
			if (InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Verification failed checking {0}.", failureLocation);
				WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.VerificationFailure, data);
			}
		}

		internal static void LogVerificationResult(SignedXml signedXml, object key, bool verified)
		{
			if (InformationLoggingEnabled)
			{
				string format = (verified ? "Verification with key {0} was successful." : "Verification with key {0} was not successful.");
				string data = string.Format(CultureInfo.InvariantCulture, format, GetKeyName(key));
				WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.SignatureVerificationResult, data);
			}
		}

		internal static void LogVerifyKeyUsage(SignedXml signedXml, X509Certificate certificate, X509KeyUsageExtension keyUsages)
		{
			if (InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Found key usages \"{0}\" in extension {1} on certificate {2}.", keyUsages.KeyUsages, GetOidName(keyUsages.Oid), GetKeyName(certificate));
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.X509Verification, data);
			}
		}

		internal static void LogVerifyReference(SignedXml signedXml, Reference reference)
		{
			if (InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Processing reference {0}, Uri \"{1}\", Id \"{2}\", Type \"{3}\".", GetObjectId(reference), reference.Uri, reference.Id, reference.Type);
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.VerifyReference, data);
			}
		}

		internal static void LogVerifyReferenceHash(SignedXml signedXml, Reference reference, byte[] actualHash, byte[] expectedHash)
		{
			if (VerboseLoggingEnabled)
			{
				HashAlgorithm hashAlgorithm = CryptoHelpers.CreateFromName<HashAlgorithm>(reference.DigestMethod);
				string text = ((hashAlgorithm == null) ? "null" : hashAlgorithm.GetType().Name);
				string data = string.Format(CultureInfo.InvariantCulture, "Reference {0} hashed with \"{1}\" ({2}) has hash value {3}, expected hash value {4}.", GetObjectId(reference), reference.DigestMethod, text, FormatBytes(actualHash), FormatBytes(expectedHash));
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.VerifyReference, data);
			}
		}

		internal static void LogVerifySignedInfo(SignedXml signedXml, AsymmetricAlgorithm key, SignatureDescription signatureDescription, HashAlgorithm hashAlgorithm, AsymmetricSignatureDeformatter asymmetricSignatureDeformatter, byte[] actualHashValue, byte[] signatureValue)
		{
			if (InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Verifying SignedInfo using key {0}, signature description {1}, hash algorithm {2}, and asymmetric signature deformatter {3}.", GetKeyName(key), signatureDescription.GetType().Name, hashAlgorithm.GetType().Name, asymmetricSignatureDeformatter.GetType().Name);
				WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.VerifySignedInfo, data);
			}
			if (VerboseLoggingEnabled)
			{
				string data2 = string.Format(CultureInfo.InvariantCulture, "Actual hash value: {0}", FormatBytes(actualHashValue));
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.VerifySignedInfo, data2);
				string data3 = string.Format(CultureInfo.InvariantCulture, "Raw signature: {0}", FormatBytes(signatureValue));
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.VerifySignedInfo, data3);
			}
		}

		internal static void LogVerifySignedInfo(SignedXml signedXml, KeyedHashAlgorithm mac, byte[] actualHashValue, byte[] signatureValue)
		{
			if (InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Verifying SignedInfo using keyed hash algorithm {0}.", mac.GetType().Name);
				WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.VerifySignedInfo, data);
			}
			if (VerboseLoggingEnabled)
			{
				string data2 = string.Format(CultureInfo.InvariantCulture, "Actual hash value: {0}", FormatBytes(actualHashValue));
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.VerifySignedInfo, data2);
				string data3 = string.Format(CultureInfo.InvariantCulture, "Raw signature: {0}", FormatBytes(signatureValue));
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.VerifySignedInfo, data3);
			}
		}

		internal static void LogVerifyX509Chain(SignedXml signedXml, X509Chain chain, X509Certificate certificate)
		{
			if (InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Building and verifying the X509 chain for certificate {0}.", GetKeyName(certificate));
				WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.X509Verification, data);
			}
			if (VerboseLoggingEnabled)
			{
				string data2 = string.Format(CultureInfo.InvariantCulture, "Revocation mode for chain building: {0}.", chain.ChainPolicy.RevocationFlag);
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.X509Verification, data2);
				string data3 = string.Format(CultureInfo.InvariantCulture, "Revocation flag for chain building: {0}.", chain.ChainPolicy.RevocationFlag);
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.X509Verification, data3);
				string data4 = string.Format(CultureInfo.InvariantCulture, "Verification flags for chain building: {0}.", chain.ChainPolicy.VerificationFlags);
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.X509Verification, data4);
				string data5 = string.Format(CultureInfo.InvariantCulture, "Verification time for chain building: {0}.", chain.ChainPolicy.VerificationTime);
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.X509Verification, data5);
				string data6 = string.Format(CultureInfo.InvariantCulture, "URL retrieval timeout for chain building: {0}.", chain.ChainPolicy.UrlRetrievalTimeout);
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.X509Verification, data6);
			}
			if (InformationLoggingEnabled)
			{
				X509ChainStatus[] chainStatus = chain.ChainStatus;
				for (int i = 0; i < chainStatus.Length; i++)
				{
					X509ChainStatus x509ChainStatus = chainStatus[i];
					if (x509ChainStatus.Status != X509ChainStatusFlags.NoError)
					{
						string data7 = string.Format(CultureInfo.InvariantCulture, "Error building X509 chain: {0}: {1}.", x509ChainStatus.Status, x509ChainStatus.StatusInformation);
						WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.X509Verification, data7);
					}
				}
			}
			if (VerboseLoggingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Certificate chain:");
				X509ChainElementEnumerator enumerator = chain.ChainElements.GetEnumerator();
				while (enumerator.MoveNext())
				{
					X509ChainElement current = enumerator.Current;
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " {0}", GetKeyName(current.Certificate));
				}
				WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugEvent.X509Verification, stringBuilder.ToString());
			}
		}

		internal static void LogSignedXmlRecursionLimit(SignedXml signedXml, Reference reference)
		{
			if (InformationLoggingEnabled)
			{
				HashAlgorithm hashAlgorithm = CryptoHelpers.CreateFromName<HashAlgorithm>(reference.DigestMethod);
				string arg = ((hashAlgorithm == null) ? "null" : hashAlgorithm.GetType().Name);
				string data = string.Format(CultureInfo.InvariantCulture, "Signed xml recursion limit hit while trying to decrypt the key. Reference {0} hashed with \"{1}\" and ({2}).", GetObjectId(reference), reference.DigestMethod, arg);
				WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugEvent.VerifySignedInfo, data);
			}
		}

		private static void WriteLine(object source, TraceEventType eventType, SignedXmlDebugEvent eventId, string data)
		{
		}
	}
	internal static class SymmetricKeyWrap
	{
		private static readonly byte[] s_rgbTripleDES_KW_IV = new byte[8] { 74, 221, 162, 44, 121, 232, 33, 5 };

		private static readonly byte[] s_rgbAES_KW_IV = new byte[8] { 166, 166, 166, 166, 166, 166, 166, 166 };

		internal static byte[] TripleDESKeyWrapEncrypt(byte[] rgbKey, byte[] rgbWrappedKeyData)
		{
			byte[] src;
			using (SHA1 sHA = SHA1.Create())
			{
				src = sHA.ComputeHash(rgbWrappedKeyData);
			}
			byte[] array = new byte[8];
			using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
			{
				randomNumberGenerator.GetBytes(array);
			}
			byte[] array2 = new byte[rgbWrappedKeyData.Length + 8];
			TripleDES tripleDES = null;
			ICryptoTransform cryptoTransform = null;
			ICryptoTransform cryptoTransform2 = null;
			try
			{
				tripleDES = TripleDES.Create();
				tripleDES.Padding = PaddingMode.None;
				cryptoTransform = tripleDES.CreateEncryptor(rgbKey, array);
				cryptoTransform2 = tripleDES.CreateEncryptor(rgbKey, s_rgbTripleDES_KW_IV);
				Buffer.BlockCopy(rgbWrappedKeyData, 0, array2, 0, rgbWrappedKeyData.Length);
				Buffer.BlockCopy(src, 0, array2, rgbWrappedKeyData.Length, 8);
				byte[] array3 = cryptoTransform.TransformFinalBlock(array2, 0, array2.Length);
				byte[] array4 = new byte[array.Length + array3.Length];
				Buffer.BlockCopy(array, 0, array4, 0, array.Length);
				Buffer.BlockCopy(array3, 0, array4, array.Length, array3.Length);
				Array.Reverse(array4);
				return cryptoTransform2.TransformFinalBlock(array4, 0, array4.Length);
			}
			finally
			{
				cryptoTransform2?.Dispose();
				cryptoTransform?.Dispose();
				tripleDES?.Dispose();
			}
		}

		internal static byte[] TripleDESKeyWrapDecrypt(byte[] rgbKey, byte[] rgbEncryptedWrappedKeyData)
		{
			if (rgbEncryptedWrappedKeyData.Length != 32 && rgbEncryptedWrappedKeyData.Length != 40 && rgbEncryptedWrappedKeyData.Length != 48)
			{
				throw new CryptographicException("The length of the encrypted data in Key Wrap is either 32, 40 or 48 bytes.");
			}
			TripleDES tripleDES = null;
			ICryptoTransform cryptoTransform = null;
			ICryptoTransform cryptoTransform2 = null;
			try
			{
				tripleDES = TripleDES.Create();
				tripleDES.Padding = PaddingMode.None;
				cryptoTransform = tripleDES.CreateDecryptor(rgbKey, s_rgbTripleDES_KW_IV);
				byte[] array = cryptoTransform.TransformFinalBlock(rgbEncryptedWrappedKeyData, 0, rgbEncryptedWrappedKeyData.Length);
				Array.Reverse(array);
				byte[] array2 = new byte[8];
				Buffer.BlockCopy(array, 0, array2, 0, 8);
				byte[] array3 = new byte[array.Length - array2.Length];
				Buffer.BlockCopy(array, 8, array3, 0, array3.Length);
				cryptoTransform2 = tripleDES.CreateDecryptor(rgbKey, array2);
				byte[] array4 = cryptoTransform2.TransformFinalBlock(array3, 0, array3.Length);
				byte[] array5 = new byte[array4.Length - 8];
				Buffer.BlockCopy(array4, 0, array5, 0, array5.Length);
				using SHA1 sHA = SHA1.Create();
				byte[] array6 = sHA.ComputeHash(array5);
				int num = array5.Length;
				int num2 = 0;
				while (num < array4.Length)
				{
					if (array4[num] != array6[num2])
					{
						throw new CryptographicException("Bad wrapped key size.");
					}
					num++;
					num2++;
				}
				return array5;
			}
			finally
			{
				cryptoTransform2?.Dispose();
				cryptoTransform?.Dispose();
				tripleDES?.Dispose();
			}
		}

		internal static byte[] AESKeyWrapEncrypt(byte[] rgbKey, byte[] rgbWrappedKeyData)
		{
			int num = rgbWrappedKeyData.Length >> 3;
			if (rgbWrappedKeyData.Length % 8 != 0 || num <= 0)
			{
				throw new CryptographicException("The length of the encrypted data in Key Wrap is either 32, 40 or 48 bytes.");
			}
			Aes aes = null;
			ICryptoTransform cryptoTransform = null;
			try
			{
				aes = Aes.Create();
				aes.Key = rgbKey;
				aes.Mode = CipherMode.ECB;
				aes.Padding = PaddingMode.None;
				cryptoTransform = aes.CreateEncryptor();
				if (num == 1)
				{
					byte[] array = new byte[s_rgbAES_KW_IV.Length + rgbWrappedKeyData.Length];
					Buffer.BlockCopy(s_rgbAES_KW_IV, 0, array, 0, s_rgbAES_KW_IV.Length);
					Buffer.BlockCopy(rgbWrappedKeyData, 0, array, s_rgbAES_KW_IV.Length, rgbWrappedKeyData.Length);
					return cryptoTransform.TransformFinalBlock(array, 0, array.Length);
				}
				long num2 = 0L;
				byte[] array2 = new byte[num + 1 << 3];
				Buffer.BlockCopy(rgbWrappedKeyData, 0, array2, 8, rgbWrappedKeyData.Length);
				byte[] array3 = new byte[8];
				byte[] array4 = new byte[16];
				Buffer.BlockCopy(s_rgbAES_KW_IV, 0, array3, 0, 8);
				for (int i = 0; i <= 5; i++)
				{
					for (int j = 1; j <= num; j++)
					{
						num2 = j + i * num;
						Buffer.BlockCopy(array3, 0, array4, 0, 8);
						Buffer.BlockCopy(array2, 8 * j, array4, 8, 8);
						byte[] array5 = cryptoTransform.TransformFinalBlock(array4, 0, 16);
						for (int k = 0; k < 8; k++)
						{
							byte b = (byte)((num2 >> 8 * (7 - k)) & 0xFF);
							array3[k] = (byte)(b ^ array5[k]);
						}
						Buffer.BlockCopy(array5, 8, array2, 8 * j, 8);
					}
				}
				Buffer.BlockCopy(array3, 0, array2, 0, 8);
				return array2;
			}
			finally
			{
				cryptoTransform?.Dispose();
				aes?.Dispose();
			}
		}

		internal static byte[] AESKeyWrapDecrypt(byte[] rgbKey, byte[] rgbEncryptedWrappedKeyData)
		{
			int num = (rgbEncryptedWrappedKeyData.Length >> 3) - 1;
			if (rgbEncryptedWrappedKeyData.Length % 8 != 0 || num <= 0)
			{
				throw new CryptographicException("The length of the encrypted data in Key Wrap is either 32, 40 or 48 bytes.");
			}
			byte[] array = new byte[num << 3];
			Aes aes = null;
			ICryptoTransform cryptoTransform = null;
			try
			{
				aes = Aes.Create();
				aes.Key = rgbKey;
				aes.Mode = CipherMode.ECB;
				aes.Padding = PaddingMode.None;
				cryptoTransform = aes.CreateDecryptor();
				if (num == 1)
				{
					byte[] array2 = cryptoTransform.TransformFinalBlock(rgbEncryptedWrappedKeyData, 0, rgbEncryptedWrappedKeyData.Length);
					for (int i = 0; i < 8; i++)
					{
						if (array2[i] != s_rgbAES_KW_IV[i])
						{
							throw new CryptographicException("Bad wrapped key size.");
						}
					}
					Buffer.BlockCopy(array2, 8, array, 0, 8);
					return array;
				}
				long num2 = 0L;
				Buffer.BlockCopy(rgbEncryptedWrappedKeyData, 8, array, 0, array.Length);
				byte[] array3 = new byte[8];
				byte[] array4 = new byte[16];
				Buffer.BlockCopy(rgbEncryptedWrappedKeyData, 0, array3, 0, 8);
				for (int num3 = 5; num3 >= 0; num3--)
				{
					for (int num4 = num; num4 >= 1; num4--)
					{
						num2 = num4 + num3 * num;
						for (int j = 0; j < 8; j++)
						{
							byte b = (byte)((num2 >> 8 * (7 - j)) & 0xFF);
							array3[j] ^= b;
						}
						Buffer.BlockCopy(array3, 0, array4, 0, 8);
						Buffer.BlockCopy(array, 8 * (num4 - 1), array4, 8, 8);
						byte[] src = cryptoTransform.TransformFinalBlock(array4, 0, 16);
						Buffer.BlockCopy(src, 8, array, 8 * (num4 - 1), 8);
						Buffer.BlockCopy(src, 0, array3, 0, 8);
					}
				}
				for (int k = 0; k < 8; k++)
				{
					if (array3[k] != s_rgbAES_KW_IV[k])
					{
						throw new CryptographicException("Bad wrapped key size.");
					}
				}
				return array;
			}
			finally
			{
				cryptoTransform?.Dispose();
				aes?.Dispose();
			}
		}
	}
	/// <summary>Represents the abstract base class from which all <see langword="&lt;Transform&gt;" /> elements that can be used in an XML digital signature derive.</summary>
	public abstract class Transform
	{
		private string _algorithm;

		private string _baseUri;

		internal XmlResolver _xmlResolver;

		private bool _bResolverSet;

		private SignedXml _signedXml;

		private Reference _reference;

		private Hashtable _propagatedNamespaces;

		private XmlElement _context;

		internal string BaseURI
		{
			get
			{
				return _baseUri;
			}
			set
			{
				_baseUri = value;
			}
		}

		internal SignedXml SignedXml
		{
			get
			{
				return _signedXml;
			}
			set
			{
				_signedXml = value;
			}
		}

		internal Reference Reference
		{
			get
			{
				return _reference;
			}
			set
			{
				_reference = value;
			}
		}

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) that identifies the algorithm performed by the current transform.</summary>
		/// <returns>The URI that identifies the algorithm performed by the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		public string Algorithm
		{
			get
			{
				return _algorithm;
			}
			set
			{
				_algorithm = value;
			}
		}

		/// <summary>Sets the current <see cref="T:System.Xml.XmlResolver" /> object.</summary>
		/// <returns>The current <see cref="T:System.Xml.XmlResolver" /> object. This property defaults to an <see cref="T:System.Xml.XmlSecureResolver" /> object.</returns>
		public XmlResolver Resolver
		{
			internal get
			{
				return _xmlResolver;
			}
			set
			{
				_xmlResolver = value;
				_bResolverSet = true;
			}
		}

		internal bool ResolverSet => _bResolverSet;

		/// <summary>When overridden in a derived class, gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.Transform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.Transform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		public abstract Type[] InputTypes { get; }

		/// <summary>When overridden in a derived class, gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.Transform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.Transform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		public abstract Type[] OutputTypes { get; }

		/// <summary>Gets or sets an <see cref="T:System.Xml.XmlElement" /> object that represents the document context under which the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object is running.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents the document context under which the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object is running.</returns>
		public XmlElement Context
		{
			get
			{
				if (_context != null)
				{
					return _context;
				}
				Reference reference = Reference;
				return ((reference == null) ? SignedXml : reference.SignedXml)?._context;
			}
			set
			{
				_context = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Collections.Hashtable" /> object that contains the namespaces that are propagated into the signature.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> object that contains the namespaces that are propagated into the signature.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.Transform.PropagatedNamespaces" /> property was set to <see langword="null" />.</exception>
		public Hashtable PropagatedNamespaces
		{
			get
			{
				if (_propagatedNamespaces != null)
				{
					return _propagatedNamespaces;
				}
				Reference reference = Reference;
				SignedXml signedXml = ((reference == null) ? SignedXml : reference.SignedXml);
				if (reference != null && (reference.ReferenceTargetType != ReferenceTargetType.UriReference || string.IsNullOrEmpty(reference.Uri) || reference.Uri[0] != '#'))
				{
					_propagatedNamespaces = new Hashtable(0);
					return _propagatedNamespaces;
				}
				CanonicalXmlNodeList canonicalXmlNodeList = null;
				if (reference != null)
				{
					canonicalXmlNodeList = reference._namespaces;
				}
				else if (signedXml?._context != null)
				{
					canonicalXmlNodeList = Utils.GetPropagatedAttributes(signedXml._context);
				}
				if (canonicalXmlNodeList == null)
				{
					_propagatedNamespaces = new Hashtable(0);
					return _propagatedNamespaces;
				}
				_propagatedNamespaces = new Hashtable(canonicalXmlNodeList.Count);
				foreach (XmlNode item in canonicalXmlNodeList)
				{
					string key = ((item.Prefix.Length > 0) ? (item.Prefix + ":" + item.LocalName) : item.LocalName);
					if (!_propagatedNamespaces.Contains(key))
					{
						_propagatedNamespaces.Add(key, item.Value);
					}
				}
				return _propagatedNamespaces;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Transform" /> class.</summary>
		protected Transform()
		{
		}

		internal bool AcceptsType(Type inputType)
		{
			if (InputTypes != null)
			{
				for (int i = 0; i < InputTypes.Length; i++)
				{
					if (inputType == InputTypes[i] || inputType.IsSubclassOf(InputTypes[i]))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Returns the XML representation of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <returns>The XML representation of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		public XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			return GetXml(xmlDocument);
		}

		internal XmlElement GetXml(XmlDocument document)
		{
			return GetXml(document, "Transform");
		}

		internal XmlElement GetXml(XmlDocument document, string name)
		{
			XmlElement xmlElement = document.CreateElement(name, "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(Algorithm))
			{
				xmlElement.SetAttribute("Algorithm", Algorithm);
			}
			XmlNodeList innerXml = GetInnerXml();
			if (innerXml != null)
			{
				foreach (XmlNode item in innerXml)
				{
					xmlElement.AppendChild(document.ImportNode(item, deep: true));
				}
			}
			return xmlElement;
		}

		/// <summary>When overridden in a derived class, parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a <see langword="&lt;Transform&gt;" /> element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to match the <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that specifies transform-specific content for the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</param>
		public abstract void LoadInnerXml(XmlNodeList nodeList);

		/// <summary>When overridden in a derived class, returns an XML representation of the parameters of the <see cref="T:System.Security.Cryptography.Xml.Transform" /> object that are suitable to be included as subelements of an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object in an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</returns>
		protected abstract XmlNodeList GetInnerXml();

		/// <summary>When overridden in a derived class, loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</param>
		public abstract void LoadInput(object obj);

		/// <summary>When overridden in a derived class, returns the output of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		public abstract object GetOutput();

		/// <summary>When overridden in a derived class, returns the output of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object of the specified type.</summary>
		/// <param name="type">The type of the output to return. This must be one of the types in the <see cref="P:System.Security.Cryptography.Xml.Transform.OutputTypes" /> property.</param>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.Transform" /> object as an object of the specified type.</returns>
		public abstract object GetOutput(Type type);

		/// <summary>When overridden in a derived class, returns the digest associated with a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</summary>
		/// <param name="hash">The <see cref="T:System.Security.Cryptography.HashAlgorithm" /> object used to create a digest.</param>
		/// <returns>The digest associated with a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object.</returns>
		public virtual byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return hash.ComputeHash((Stream)GetOutput(typeof(Stream)));
		}
	}
	/// <summary>Defines an ordered list of <see cref="T:System.Security.Cryptography.Xml.Transform" /> objects that is applied to unsigned content prior to digest calculation.</summary>
	public class TransformChain
	{
		private ArrayList _transforms;

		/// <summary>Gets the number of transforms in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <returns>The number of transforms in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</returns>
		public int Count => _transforms.Count;

		/// <summary>Gets the transform at the specified index in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <param name="index">The index into the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that specifies which transform to return.</param>
		/// <returns>The transform at the specified index in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> parameter is greater than the number of transforms.</exception>
		public Transform this[int index]
		{
			get
			{
				if (index >= _transforms.Count)
				{
					throw new ArgumentException("Index was out of range. Must be non-negative and less than the size of the collection.", "index");
				}
				return (Transform)_transforms[index];
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> class.</summary>
		public TransformChain()
		{
			_transforms = new ArrayList();
		}

		/// <summary>Adds a transform to the list of transforms to be applied to the unsigned content prior to digest calculation.</summary>
		/// <param name="transform">The transform to add to the list of transforms.</param>
		public void Add(Transform transform)
		{
			if (transform != null)
			{
				_transforms.Add(transform);
			}
		}

		/// <summary>Returns an enumerator of the transforms in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <returns>An enumerator of the transforms in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</returns>
		public IEnumerator GetEnumerator()
		{
			return _transforms.GetEnumerator();
		}

		internal Stream TransformToOctetStream(object inputObject, Type inputType, XmlResolver resolver, string baseUri)
		{
			object obj = inputObject;
			foreach (Transform transform in _transforms)
			{
				if (obj == null || transform.AcceptsType(obj.GetType()))
				{
					transform.Resolver = resolver;
					transform.BaseURI = baseUri;
					transform.LoadInput(obj);
					obj = transform.GetOutput();
				}
				else if (obj is Stream)
				{
					if (!transform.AcceptsType(typeof(XmlDocument)))
					{
						throw new CryptographicException("The input type was invalid for this transform.");
					}
					Stream obj2 = obj as Stream;
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.PreserveWhitespace = true;
					XmlReader reader = Utils.PreProcessStreamInput(obj2, resolver, baseUri);
					xmlDocument.Load(reader);
					transform.LoadInput(xmlDocument);
					obj2.Close();
					obj = transform.GetOutput();
				}
				else if (obj is XmlNodeList)
				{
					if (!transform.AcceptsType(typeof(Stream)))
					{
						throw new CryptographicException("The input type was invalid for this transform.");
					}
					MemoryStream memoryStream = new MemoryStream(new CanonicalXml((XmlNodeList)obj, resolver, includeComments: false).GetBytes());
					transform.LoadInput(memoryStream);
					obj = transform.GetOutput();
					memoryStream.Close();
				}
				else
				{
					if (!(obj is XmlDocument))
					{
						throw new CryptographicException("The input type was invalid for this transform.");
					}
					if (!transform.AcceptsType(typeof(Stream)))
					{
						throw new CryptographicException("The input type was invalid for this transform.");
					}
					MemoryStream memoryStream2 = new MemoryStream(new CanonicalXml((XmlDocument)obj, resolver).GetBytes());
					transform.LoadInput(memoryStream2);
					obj = transform.GetOutput();
					memoryStream2.Close();
				}
			}
			if (obj is Stream)
			{
				return obj as Stream;
			}
			if (obj is XmlNodeList)
			{
				return new MemoryStream(new CanonicalXml((XmlNodeList)obj, resolver, includeComments: false).GetBytes());
			}
			if (obj is XmlDocument)
			{
				return new MemoryStream(new CanonicalXml((XmlDocument)obj, resolver).GetBytes());
			}
			throw new CryptographicException("The input type was invalid for this transform.");
		}

		internal Stream TransformToOctetStream(Stream input, XmlResolver resolver, string baseUri)
		{
			return TransformToOctetStream(input, typeof(Stream), resolver, baseUri);
		}

		internal Stream TransformToOctetStream(XmlDocument document, XmlResolver resolver, string baseUri)
		{
			return TransformToOctetStream(document, typeof(XmlDocument), resolver, baseUri);
		}

		internal XmlElement GetXml(XmlDocument document, string ns)
		{
			XmlElement xmlElement = document.CreateElement("Transforms", ns);
			foreach (Transform transform in _transforms)
			{
				if (transform != null)
				{
					XmlElement xml = transform.GetXml(document);
					if (xml != null)
					{
						xmlElement.AppendChild(xml);
					}
				}
			}
			return xmlElement;
		}

		internal void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlNodeList xmlNodeList = value.SelectNodes("ds:Transform", xmlNamespaceManager);
			if (xmlNodeList.Count == 0)
			{
				throw new CryptographicException("Malformed element {0}.", "Transforms");
			}
			_transforms.Clear();
			for (int i = 0; i < xmlNodeList.Count; i++)
			{
				XmlElement xmlElement = (XmlElement)xmlNodeList.Item(i);
				Transform transform = CryptoHelpers.CreateFromName<Transform>(Utils.GetAttribute(xmlElement, "Algorithm", "http://www.w3.org/2000/09/xmldsig#"));
				if (transform == null)
				{
					throw new CryptographicException("Unknown transform has been encountered.");
				}
				transform.LoadInnerXml(xmlElement.ChildNodes);
				_transforms.Add(transform);
			}
		}
	}
	internal class Utils
	{
		internal const int MaxCharactersInDocument = 0;

		internal const long MaxCharactersFromEntities = 10000000L;

		internal const int XmlDsigSearchDepth = 20;

		private static readonly char[] s_hexValues = new char[16]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};

		internal const int MaxTransformsPerReference = 10;

		internal const int MaxReferencesPerSignedInfo = 100;

		private Utils()
		{
		}

		private static bool HasNamespace(XmlElement element, string prefix, string value)
		{
			if (IsCommittedNamespace(element, prefix, value))
			{
				return true;
			}
			if (element.Prefix == prefix && element.NamespaceURI == value)
			{
				return true;
			}
			return false;
		}

		internal static bool IsCommittedNamespace(XmlElement element, string prefix, string value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			string name = ((prefix.Length > 0) ? ("xmlns:" + prefix) : "xmlns");
			if (element.HasAttribute(name) && element.GetAttribute(name) == value)
			{
				return true;
			}
			return false;
		}

		internal static bool IsRedundantNamespace(XmlElement element, string prefix, string value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			for (XmlNode parentNode = element.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
			{
				if (parentNode is XmlElement element2 && HasNamespace(element2, prefix, value))
				{
					return true;
				}
			}
			return false;
		}

		internal static string GetAttribute(XmlElement element, string localName, string namespaceURI)
		{
			string text = (element.HasAttribute(localName) ? element.GetAttribute(localName) : null);
			if (text == null && element.HasAttribute(localName, namespaceURI))
			{
				text = element.GetAttribute(localName, namespaceURI);
			}
			return text;
		}

		internal static bool HasAttribute(XmlElement element, string localName, string namespaceURI)
		{
			if (!element.HasAttribute(localName))
			{
				return element.HasAttribute(localName, namespaceURI);
			}
			return true;
		}

		internal static bool VerifyAttributes(XmlElement element, string expectedAttrName)
		{
			return VerifyAttributes(element, (expectedAttrName == null) ? null : new string[1] { expectedAttrName });
		}

		internal static bool VerifyAttributes(XmlElement element, string[] expectedAttrNames)
		{
			foreach (XmlAttribute attribute in element.Attributes)
			{
				bool flag = attribute.Name == "xmlns" || attribute.Name.StartsWith("xmlns:") || attribute.Name == "xml:space" || attribute.Name == "xml:lang" || attribute.Name == "xml:base";
				int num = 0;
				while (!flag && expectedAttrNames != null && num < expectedAttrNames.Length)
				{
					flag = attribute.Name == expectedAttrNames[num];
					num++;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		internal static bool IsNamespaceNode(XmlNode n)
		{
			if (n.NodeType == XmlNodeType.Attribute)
			{
				if (!n.Prefix.Equals("xmlns"))
				{
					if (n.Prefix.Length == 0)
					{
						return n.LocalName.Equals("xmlns");
					}
					return false;
				}
				return true;
			}
			return false;
		}

		internal static bool IsXmlNamespaceNode(XmlNode n)
		{
			if (n.NodeType == XmlNodeType.Attribute)
			{
				return n.Prefix.Equals("xml");
			}
			return false;
		}

		internal static bool IsDefaultNamespaceNode(XmlNode n)
		{
			bool num = n.NodeType == XmlNodeType.Attribute && n.Prefix.Length == 0 && n.LocalName.Equals("xmlns");
			bool flag = IsXmlNamespaceNode(n);
			return num || flag;
		}

		internal static bool IsEmptyDefaultNamespaceNode(XmlNode n)
		{
			if (IsDefaultNamespaceNode(n))
			{
				return n.Value.Length == 0;
			}
			return false;
		}

		internal static string GetNamespacePrefix(XmlAttribute a)
		{
			if (a.Prefix.Length != 0)
			{
				return a.LocalName;
			}
			return string.Empty;
		}

		internal static bool HasNamespacePrefix(XmlAttribute a, string nsPrefix)
		{
			return GetNamespacePrefix(a).Equals(nsPrefix);
		}

		internal static bool IsNonRedundantNamespaceDecl(XmlAttribute a, XmlAttribute nearestAncestorWithSamePrefix)
		{
			if (nearestAncestorWithSamePrefix == null)
			{
				return !IsEmptyDefaultNamespaceNode(a);
			}
			return !nearestAncestorWithSamePrefix.Value.Equals(a.Value);
		}

		internal static bool IsXmlPrefixDefinitionNode(XmlAttribute a)
		{
			return false;
		}

		internal static string DiscardWhiteSpaces(string inputBuffer)
		{
			return DiscardWhiteSpaces(inputBuffer, 0, inputBuffer.Length);
		}

		internal static string DiscardWhiteSpaces(string inputBuffer, int inputOffset, int inputCount)
		{
			int num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					num++;
				}
			}
			char[] array = new char[inputCount - num];
			num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (!char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					array[num++] = inputBuffer[inputOffset + i];
				}
			}
			return new string(array);
		}

		internal static void SBReplaceCharWithString(StringBuilder sb, char oldChar, string newString)
		{
			int num = 0;
			int length = newString.Length;
			while (num < sb.Length)
			{
				if (sb[num] == oldChar)
				{
					sb.Remove(num, 1);
					sb.Insert(num, newString);
					num += length;
				}
				else
				{
					num++;
				}
			}
		}

		internal static XmlReader PreProcessStreamInput(Stream inputStream, XmlResolver xmlResolver, string baseUri)
		{
			XmlReaderSettings secureXmlReaderSettings = GetSecureXmlReaderSettings(xmlResolver);
			return XmlReader.Create(inputStream, secureXmlReaderSettings, baseUri);
		}

		internal static XmlReaderSettings GetSecureXmlReaderSettings(XmlResolver xmlResolver)
		{
			return new XmlReaderSettings
			{
				XmlResolver = xmlResolver,
				DtdProcessing = DtdProcessing.Parse,
				MaxCharactersFromEntities = 10000000L,
				MaxCharactersInDocument = 0L
			};
		}

		internal static XmlDocument PreProcessDocumentInput(XmlDocument document, XmlResolver xmlResolver, string baseUri)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			MyXmlDocument myXmlDocument = new MyXmlDocument();
			myXmlDocument.PreserveWhitespace = document.PreserveWhitespace;
			using TextReader input = new StringReader(document.OuterXml);
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.XmlResolver = xmlResolver;
			xmlReaderSettings.DtdProcessing = DtdProcessing.Parse;
			xmlReaderSettings.MaxCharactersFromEntities = 10000000L;
			xmlReaderSettings.MaxCharactersInDocument = 0L;
			XmlReader reader = XmlReader.Create(input, xmlReaderSettings, baseUri);
			myXmlDocument.Load(reader);
			return myXmlDocument;
		}

		internal static XmlDocument PreProcessElementInput(XmlElement elem, XmlResolver xmlResolver, string baseUri)
		{
			if (elem == null)
			{
				throw new ArgumentNullException("elem");
			}
			MyXmlDocument myXmlDocument = new MyXmlDocument();
			myXmlDocument.PreserveWhitespace = true;
			using TextReader input = new StringReader(elem.OuterXml);
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.XmlResolver = xmlResolver;
			xmlReaderSettings.DtdProcessing = DtdProcessing.Parse;
			xmlReaderSettings.MaxCharactersFromEntities = 10000000L;
			xmlReaderSettings.MaxCharactersInDocument = 0L;
			XmlReader reader = XmlReader.Create(input, xmlReaderSettings, baseUri);
			myXmlDocument.Load(reader);
			return myXmlDocument;
		}

		internal static XmlDocument DiscardComments(XmlDocument document)
		{
			XmlNodeList xmlNodeList = document.SelectNodes("//comment()");
			if (xmlNodeList != null)
			{
				foreach (XmlNode item in xmlNodeList)
				{
					item.ParentNode.RemoveChild(item);
				}
			}
			return document;
		}

		internal static XmlNodeList AllDescendantNodes(XmlNode node, bool includeComments)
		{
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			CanonicalXmlNodeList canonicalXmlNodeList2 = new CanonicalXmlNodeList();
			CanonicalXmlNodeList canonicalXmlNodeList3 = new CanonicalXmlNodeList();
			CanonicalXmlNodeList canonicalXmlNodeList4 = new CanonicalXmlNodeList();
			int num = 0;
			canonicalXmlNodeList2.Add(node);
			do
			{
				XmlNode xmlNode = canonicalXmlNodeList2[num];
				XmlNodeList childNodes = xmlNode.ChildNodes;
				if (childNodes != null)
				{
					foreach (XmlNode item in childNodes)
					{
						if (includeComments || !(item is XmlComment))
						{
							canonicalXmlNodeList2.Add(item);
						}
					}
				}
				if (xmlNode.Attributes != null)
				{
					foreach (XmlNode attribute in xmlNode.Attributes)
					{
						if (attribute.LocalName == "xmlns" || attribute.Prefix == "xmlns")
						{
							canonicalXmlNodeList4.Add(attribute);
						}
						else
						{
							canonicalXmlNodeList3.Add(attribute);
						}
					}
				}
				num++;
			}
			while (num < canonicalXmlNodeList2.Count);
			foreach (XmlNode item2 in canonicalXmlNodeList2)
			{
				canonicalXmlNodeList.Add(item2);
			}
			foreach (XmlNode item3 in canonicalXmlNodeList3)
			{
				canonicalXmlNodeList.Add(item3);
			}
			foreach (XmlNode item4 in canonicalXmlNodeList4)
			{
				canonicalXmlNodeList.Add(item4);
			}
			return canonicalXmlNodeList;
		}

		internal static bool NodeInList(XmlNode node, XmlNodeList nodeList)
		{
			foreach (XmlNode node2 in nodeList)
			{
				if (node2 == node)
				{
					return true;
				}
			}
			return false;
		}

		internal static string GetIdFromLocalUri(string uri, out bool discardComments)
		{
			string text = uri.Substring(1);
			discardComments = true;
			if (text.StartsWith("xpointer(id(", StringComparison.Ordinal))
			{
				int num = text.IndexOf("id(", StringComparison.Ordinal);
				int num2 = text.IndexOf(")", StringComparison.Ordinal);
				if (num2 < 0 || num2 < num + 3)
				{
					throw new CryptographicException("Malformed reference element.");
				}
				text = text.Substring(num + 3, num2 - num - 3);
				text = text.Replace("'", "");
				text = text.Replace("\"", "");
				discardComments = false;
			}
			return text;
		}

		internal static string ExtractIdFromLocalUri(string uri)
		{
			string text = uri.Substring(1);
			if (text.StartsWith("xpointer(id(", StringComparison.Ordinal))
			{
				int num = text.IndexOf("id(", StringComparison.Ordinal);
				int num2 = text.IndexOf(")", StringComparison.Ordinal);
				if (num2 < 0 || num2 < num + 3)
				{
					throw new CryptographicException("Malformed reference element.");
				}
				text = text.Substring(num + 3, num2 - num - 3);
				text = text.Replace("'", "");
				text = text.Replace("\"", "");
			}
			return text;
		}

		internal static void RemoveAllChildren(XmlElement inputElement)
		{
			XmlNode xmlNode = inputElement.FirstChild;
			while (xmlNode != null)
			{
				XmlNode nextSibling = xmlNode.NextSibling;
				inputElement.RemoveChild(xmlNode);
				xmlNode = nextSibling;
			}
		}

		internal static long Pump(Stream input, Stream output)
		{
			if (input is MemoryStream { Position: 0L } memoryStream)
			{
				memoryStream.WriteTo(output);
				return memoryStream.Length;
			}
			byte[] buffer = new byte[4096];
			long num = 0L;
			int num2;
			while ((num2 = input.Read(buffer, 0, 4096)) > 0)
			{
				output.Write(buffer, 0, num2);
				num += num2;
			}
			return num;
		}

		internal static Hashtable TokenizePrefixListString(string s)
		{
			Hashtable hashtable = new Hashtable();
			if (s != null)
			{
				string[] array = s.Split((char[])null);
				foreach (string text in array)
				{
					if (text.Equals("#default"))
					{
						hashtable.Add(string.Empty, true);
					}
					else if (text.Length > 0)
					{
						hashtable.Add(text, true);
					}
				}
			}
			return hashtable;
		}

		internal static string EscapeWhitespaceData(string data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(data);
			SBReplaceCharWithString(stringBuilder, '\r', "&#xD;");
			return stringBuilder.ToString();
		}

		internal static string EscapeTextData(string data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(data);
			stringBuilder.Replace("&", "&amp;");
			stringBuilder.Replace("<", "&lt;");
			stringBuilder.Replace(">", "&gt;");
			SBReplaceCharWithString(stringBuilder, '\r', "&#xD;");
			return stringBuilder.ToString();
		}

		internal static string EscapeCData(string data)
		{
			return EscapeTextData(data);
		}

		internal static string EscapeAttributeValue(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value);
			stringBuilder.Replace("&", "&amp;");
			stringBuilder.Replace("<", "&lt;");
			stringBuilder.Replace("\"", "&quot;");
			SBReplaceCharWithString(stringBuilder, '\t', "&#x9;");
			SBReplaceCharWithString(stringBuilder, '\n', "&#xA;");
			SBReplaceCharWithString(stringBuilder, '\r', "&#xD;");
			return stringBuilder.ToString();
		}

		internal static XmlDocument GetOwnerDocument(XmlNodeList nodeList)
		{
			foreach (XmlNode node in nodeList)
			{
				if (node.OwnerDocument != null)
				{
					return node.OwnerDocument;
				}
			}
			return null;
		}

		internal static void AddNamespaces(XmlElement elem, CanonicalXmlNodeList namespaces)
		{
			if (namespaces == null)
			{
				return;
			}
			foreach (XmlNode @namespace in namespaces)
			{
				string text = ((@namespace.Prefix.Length > 0) ? (@namespace.Prefix + ":" + @namespace.LocalName) : @namespace.LocalName);
				if (!elem.HasAttribute(text) && (!text.Equals("xmlns") || elem.Prefix.Length != 0))
				{
					XmlAttribute xmlAttribute = elem.OwnerDocument.CreateAttribute(text);
					xmlAttribute.Value = @namespace.Value;
					elem.SetAttributeNode(xmlAttribute);
				}
			}
		}

		internal static void AddNamespaces(XmlElement elem, Hashtable namespaces)
		{
			if (namespaces == null)
			{
				return;
			}
			foreach (string key in namespaces.Keys)
			{
				if (!elem.HasAttribute(key))
				{
					XmlAttribute xmlAttribute = elem.OwnerDocument.CreateAttribute(key);
					xmlAttribute.Value = namespaces[key] as string;
					elem.SetAttributeNode(xmlAttribute);
				}
			}
		}

		internal static CanonicalXmlNodeList GetPropagatedAttributes(XmlElement elem)
		{
			if (elem == null)
			{
				return null;
			}
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			XmlNode xmlNode = elem;
			if (xmlNode == null)
			{
				return null;
			}
			bool flag = true;
			while (xmlNode != null)
			{
				if (!(xmlNode is XmlElement xmlElement))
				{
					xmlNode = xmlNode.ParentNode;
					continue;
				}
				if (!IsCommittedNamespace(xmlElement, xmlElement.Prefix, xmlElement.NamespaceURI) && !IsRedundantNamespace(xmlElement, xmlElement.Prefix, xmlElement.NamespaceURI))
				{
					string name = ((xmlElement.Prefix.Length > 0) ? ("xmlns:" + xmlElement.Prefix) : "xmlns");
					XmlAttribute xmlAttribute = elem.OwnerDocument.CreateAttribute(name);
					xmlAttribute.Value = xmlElement.NamespaceURI;
					canonicalXmlNodeList.Add(xmlAttribute);
				}
				if (xmlElement.HasAttributes)
				{
					foreach (XmlAttribute attribute in xmlElement.Attributes)
					{
						if (flag && attribute.LocalName == "xmlns")
						{
							XmlAttribute xmlAttribute3 = elem.OwnerDocument.CreateAttribute("xmlns");
							xmlAttribute3.Value = attribute.Value;
							canonicalXmlNodeList.Add(xmlAttribute3);
							flag = false;
						}
						else if (attribute.Prefix == "xmlns" || attribute.Prefix == "xml")
						{
							canonicalXmlNodeList.Add(attribute);
						}
						else if (attribute.NamespaceURI.Length > 0 && !IsCommittedNamespace(xmlElement, attribute.Prefix, attribute.NamespaceURI) && !IsRedundantNamespace(xmlElement, attribute.Prefix, attribute.NamespaceURI))
						{
							string name2 = ((attribute.Prefix.Length > 0) ? ("xmlns:" + attribute.Prefix) : "xmlns");
							XmlAttribute xmlAttribute4 = elem.OwnerDocument.CreateAttribute(name2);
							xmlAttribute4.Value = attribute.NamespaceURI;
							canonicalXmlNodeList.Add(xmlAttribute4);
						}
					}
				}
				xmlNode = xmlNode.ParentNode;
			}
			return canonicalXmlNodeList;
		}

		internal static byte[] ConvertIntToByteArray(int dwInput)
		{
			byte[] array = new byte[8];
			int num = 0;
			if (dwInput == 0)
			{
				return new byte[1];
			}
			int num2 = dwInput;
			while (num2 > 0)
			{
				int num3 = num2 % 256;
				array[num] = (byte)num3;
				num2 = (num2 - num3) / 256;
				num++;
			}
			byte[] array2 = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = array[num - i - 1];
			}
			return array2;
		}

		internal static int ConvertByteArrayToInt(byte[] input)
		{
			int num = 0;
			for (int i = 0; i < input.Length; i++)
			{
				num *= 256;
				num += input[i];
			}
			return num;
		}

		internal static int GetHexArraySize(byte[] hex)
		{
			int num = hex.Length;
			while (num-- > 0 && hex[num] == 0)
			{
			}
			return num + 1;
		}

		internal static X509IssuerSerial CreateX509IssuerSerial(string issuerName, string serialNumber)
		{
			if (issuerName == null || issuerName.Length == 0)
			{
				throw new ArgumentException("String cannot be empty or null.", "issuerName");
			}
			if (serialNumber == null || serialNumber.Length == 0)
			{
				throw new ArgumentException("String cannot be empty or null.", "serialNumber");
			}
			return new X509IssuerSerial
			{
				IssuerName = issuerName,
				SerialNumber = serialNumber
			};
		}

		internal static X509Certificate2Collection BuildBagOfCerts(KeyInfoX509Data keyInfoX509Data, CertUsageType certUsageType)
		{
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			ArrayList arrayList = ((certUsageType == CertUsageType.Decryption) ? new ArrayList() : null);
			if (keyInfoX509Data.Certificates != null)
			{
				foreach (X509Certificate2 certificate in keyInfoX509Data.Certificates)
				{
					switch (certUsageType)
					{
					case CertUsageType.Verification:
						x509Certificate2Collection.Add(certificate);
						break;
					case CertUsageType.Decryption:
						arrayList.Add(CreateX509IssuerSerial(certificate.IssuerName.Name, certificate.SerialNumber));
						break;
					}
				}
			}
			if (keyInfoX509Data.SubjectNames == null && keyInfoX509Data.IssuerSerials == null && keyInfoX509Data.SubjectKeyIds == null && arrayList == null)
			{
				return x509Certificate2Collection;
			}
			X509Store[] array = new X509Store[2];
			string storeName = ((certUsageType == CertUsageType.Verification) ? "AddressBook" : "My");
			array[0] = new X509Store(storeName, StoreLocation.CurrentUser);
			array[1] = new X509Store(storeName, StoreLocation.LocalMachine);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					continue;
				}
				X509Certificate2Collection x509Certificate2Collection2 = null;
				try
				{
					array[i].Open(OpenFlags.OpenExistingOnly);
					x509Certificate2Collection2 = array[i].Certificates;
					array[i].Close();
					if (keyInfoX509Data.SubjectNames != null)
					{
						foreach (string subjectName in keyInfoX509Data.SubjectNames)
						{
							x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindBySubjectDistinguishedName, subjectName, validOnly: false);
						}
					}
					if (keyInfoX509Data.IssuerSerials != null)
					{
						foreach (X509IssuerSerial issuerSerial in keyInfoX509Data.IssuerSerials)
						{
							x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindByIssuerDistinguishedName, issuerSerial.IssuerName, validOnly: false);
							x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindBySerialNumber, issuerSerial.SerialNumber, validOnly: false);
						}
					}
					if (keyInfoX509Data.SubjectKeyIds != null)
					{
						foreach (byte[] subjectKeyId in keyInfoX509Data.SubjectKeyIds)
						{
							string findValue2 = EncodeHexString(subjectKeyId);
							x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindBySubjectKeyIdentifier, findValue2, validOnly: false);
						}
					}
					if (arrayList != null)
					{
						foreach (X509IssuerSerial item in arrayList)
						{
							x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindByIssuerDistinguishedName, item.IssuerName, validOnly: false);
							x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindBySerialNumber, item.SerialNumber, validOnly: false);
						}
					}
				}
				catch (CryptographicException)
				{
				}
				catch (PlatformNotSupportedException)
				{
				}
				if (x509Certificate2Collection2 != null)
				{
					x509Certificate2Collection.AddRange(x509Certificate2Collection2);
				}
			}
			return x509Certificate2Collection;
		}

		internal static string EncodeHexString(byte[] sArray)
		{
			return EncodeHexString(sArray, 0u, (uint)sArray.Length);
		}

		internal static string EncodeHexString(byte[] sArray, uint start, uint end)
		{
			string result = null;
			if (sArray != null)
			{
				char[] array = new char[(end - start) * 2];
				uint num = start;
				uint num2 = 0u;
				for (; num < end; num++)
				{
					uint num3 = (uint)((sArray[num] & 0xF0) >> 4);
					array[num2++] = s_hexValues[num3];
					num3 = (uint)(sArray[num] & 0xF);
					array[num2++] = s_hexValues[num3];
				}
				result = new string(array);
			}
			return result;
		}

		internal static byte[] DecodeHexString(string s)
		{
			string text = DiscardWhiteSpaces(s);
			uint num = (uint)text.Length / 2u;
			byte[] array = new byte[num];
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				array[i] = (byte)((HexToByte(text[num2]) << 4) | HexToByte(text[num2 + 1]));
				num2 += 2;
			}
			return array;
		}

		internal static byte HexToByte(char val)
		{
			if (val <= '9' && val >= '0')
			{
				return (byte)(val - 48);
			}
			if (val >= 'a' && val <= 'f')
			{
				return (byte)(val - 97 + 10);
			}
			if (val >= 'A' && val <= 'F')
			{
				return (byte)(val - 65 + 10);
			}
			return byte.MaxValue;
		}

		internal static bool IsSelfSigned(X509Chain chain)
		{
			X509ChainElementCollection chainElements = chain.ChainElements;
			if (chainElements.Count != 1)
			{
				return false;
			}
			X509Certificate2 certificate = chainElements[0].Certificate;
			if (string.Compare(certificate.SubjectName.Name, certificate.IssuerName.Name, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			return false;
		}

		internal static AsymmetricAlgorithm GetAnyPublicKey(X509Certificate2 certificate)
		{
			return certificate.GetRSAPublicKey();
		}
	}
	/// <summary>Specifies the order of XML Digital Signature and XML Encryption operations when both are performed on the same document.</summary>
	public class XmlDecryptionTransform : Transform
	{
		private Type[] _inputTypes = new Type[2]
		{
			typeof(Stream),
			typeof(XmlDocument)
		};

		private Type[] _outputTypes = new Type[1] { typeof(XmlDocument) };

		private XmlNodeList _encryptedDataList;

		private ArrayList _arrayListUri;

		private EncryptedXml _exml;

		private XmlDocument _containingDocument;

		private XmlNamespaceManager _nsm;

		private const string XmlDecryptionTransformNamespaceUrl = "http://www.w3.org/2002/07/decrypt#";

		private ArrayList ExceptUris
		{
			get
			{
				if (_arrayListUri == null)
				{
					_arrayListUri = new ArrayList();
				}
				return _arrayListUri;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object that contains information about the keys necessary to decrypt an XML document.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object that contains information about the keys necessary to decrypt an XML document.</returns>
		public EncryptedXml EncryptedXml
		{
			get
			{
				if (_exml != null)
				{
					return _exml;
				}
				Reference reference = base.Reference;
				SignedXml signedXml = ((reference == null) ? base.SignedXml : reference.SignedXml);
				if (signedXml == null || signedXml.EncryptedXml == null)
				{
					_exml = new EncryptedXml(_containingDocument);
				}
				else
				{
					_exml = signedXml.EncryptedXml;
				}
				return _exml;
			}
			set
			{
				_exml = value;
			}
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</returns>
		public override Type[] InputTypes => _inputTypes;

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</returns>
		public override Type[] OutputTypes => _outputTypes;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> class.</summary>
		public XmlDecryptionTransform()
		{
			base.Algorithm = "http://www.w3.org/2002/07/decrypt#XML";
		}

		/// <summary>Determines whether the ID attribute of an <see cref="T:System.Xml.XmlElement" /> object matches a specified value.</summary>
		/// <param name="inputElement">An <see cref="T:System.Xml.XmlElement" /> object with an ID attribute to compare with <paramref name="idValue" />.</param>
		/// <param name="idValue">The value to compare with the ID attribute of <paramref name="inputElement" />.</param>
		/// <returns>
		///   <see langword="true" /> if the ID attribute of the <paramref name="inputElement" /> parameter matches the <paramref name="idValue" /> parameter; otherwise, <see langword="false" />.</returns>
		protected virtual bool IsTargetElement(XmlElement inputElement, string idValue)
		{
			if (inputElement == null)
			{
				return false;
			}
			if (inputElement.GetAttribute("Id") == idValue || inputElement.GetAttribute("id") == idValue || inputElement.GetAttribute("ID") == idValue)
			{
				return true;
			}
			return false;
		}

		/// <summary>Adds a Uniform Resource Identifier (URI) to exclude from processing.</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) to exclude from processing</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="uri" /> parameter is <see langword="null" />.</exception>
		public void AddExceptUri(string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			ExceptUris.Add(uri);
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a <see langword="&lt;Transform&gt;" /> element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object to match the <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that specifies transform-specific content for the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="nodeList" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The Uniform Resource Identifier (URI) value of an <see cref="T:System.Xml.XmlNode" /> object in <paramref name="nodeList" /> was not found.  
		///  -or-  
		///  The length of the URI value of an <see cref="T:System.Xml.XmlNode" /> object in <paramref name="nodeList" /> is 0.  
		///  -or-  
		///  The first character of the URI value of an <see cref="T:System.Xml.XmlNode" /> object in <paramref name="nodeList" /> is not '#'.</exception>
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new CryptographicException("Unknown transform has been encountered.");
			}
			ExceptUris.Clear();
			foreach (XmlNode node in nodeList)
			{
				if (node is XmlElement xmlElement)
				{
					if (!(xmlElement.LocalName == "Except") || !(xmlElement.NamespaceURI == "http://www.w3.org/2002/07/decrypt#"))
					{
						throw new CryptographicException("Unknown transform has been encountered.");
					}
					string attribute = Utils.GetAttribute(xmlElement, "URI", "http://www.w3.org/2002/07/decrypt#");
					if (attribute == null || attribute.Length == 0 || attribute[0] != '#')
					{
						throw new CryptographicException("A Uri attribute is required for a CipherReference element.");
					}
					if (!Utils.VerifyAttributes(xmlElement, "URI"))
					{
						throw new CryptographicException("Unknown transform has been encountered.");
					}
					string value = Utils.ExtractIdFromLocalUri(attribute);
					ExceptUris.Add(value);
				}
			}
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object that are suitable to be included as subelements of an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object in an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</returns>
		protected override XmlNodeList GetInnerXml()
		{
			if (ExceptUris.Count == 0)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("Transform", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(base.Algorithm))
			{
				xmlElement.SetAttribute("Algorithm", base.Algorithm);
			}
			foreach (string exceptUri in ExceptUris)
			{
				XmlElement xmlElement2 = xmlDocument.CreateElement("Except", "http://www.w3.org/2002/07/decrypt#");
				xmlElement2.SetAttribute("URI", exceptUri);
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement.ChildNodes;
		}

		/// <summary>When overridden in a derived class, loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		public override void LoadInput(object obj)
		{
			if (obj is Stream)
			{
				LoadStreamInput((Stream)obj);
			}
			else if (obj is XmlDocument)
			{
				LoadXmlDocumentInput((XmlDocument)obj);
			}
		}

		private void LoadStreamInput(Stream stream)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			XmlResolver xmlResolver = (base.ResolverSet ? _xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI));
			XmlReader reader = Utils.PreProcessStreamInput(stream, xmlResolver, base.BaseURI);
			xmlDocument.Load(reader);
			_containingDocument = xmlDocument;
			_nsm = new XmlNamespaceManager(_containingDocument.NameTable);
			_nsm.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			_encryptedDataList = xmlDocument.SelectNodes("//enc:EncryptedData", _nsm);
		}

		private void LoadXmlDocumentInput(XmlDocument document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			_containingDocument = document;
			_nsm = new XmlNamespaceManager(document.NameTable);
			_nsm.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			_encryptedDataList = document.SelectNodes("//enc:EncryptedData", _nsm);
		}

		private void ReplaceEncryptedData(XmlElement encryptedDataElement, byte[] decrypted)
		{
			XmlNode parentNode = encryptedDataElement.ParentNode;
			if (parentNode.NodeType == XmlNodeType.Document)
			{
				parentNode.InnerXml = EncryptedXml.Encoding.GetString(decrypted);
			}
			else
			{
				EncryptedXml.ReplaceData(encryptedDataElement, decrypted);
			}
		}

		private bool ProcessEncryptedDataItem(XmlElement encryptedDataElement)
		{
			if (ExceptUris.Count > 0)
			{
				for (int i = 0; i < ExceptUris.Count; i++)
				{
					if (IsTargetElement(encryptedDataElement, (string)ExceptUris[i]))
					{
						return false;
					}
				}
			}
			EncryptedData encryptedData = new EncryptedData();
			encryptedData.LoadXml(encryptedDataElement);
			SymmetricAlgorithm decryptionKey = EncryptedXml.GetDecryptionKey(encryptedData, null);
			if (decryptionKey == null)
			{
				throw new CryptographicException("Unable to retrieve the decryption key.");
			}
			byte[] decrypted = EncryptedXml.DecryptData(encryptedData, decryptionKey);
			ReplaceEncryptedData(encryptedDataElement, decrypted);
			return true;
		}

		private void ProcessElementRecursively(XmlNodeList encryptedDatas)
		{
			if (encryptedDatas == null || encryptedDatas.Count == 0)
			{
				return;
			}
			Queue queue = new Queue();
			foreach (XmlNode encryptedData in encryptedDatas)
			{
				queue.Enqueue(encryptedData);
			}
			XmlNode xmlNode = queue.Dequeue() as XmlNode;
			while (xmlNode != null)
			{
				if (xmlNode is XmlElement { LocalName: "EncryptedData", NamespaceURI: "http://www.w3.org/2001/04/xmlenc#", NextSibling: var nextSibling, ParentNode: var parentNode } xmlElement && ProcessEncryptedDataItem(xmlElement))
				{
					XmlNode xmlNode2 = parentNode.FirstChild;
					while (xmlNode2 != null && xmlNode2.NextSibling != nextSibling)
					{
						xmlNode2 = xmlNode2.NextSibling;
					}
					if (xmlNode2 != null)
					{
						XmlNodeList xmlNodeList = xmlNode2.SelectNodes("//enc:EncryptedData", _nsm);
						if (xmlNodeList.Count > 0)
						{
							foreach (XmlNode item in xmlNodeList)
							{
								queue.Enqueue(item);
							}
						}
					}
				}
				if (queue.Count != 0)
				{
					xmlNode = queue.Dequeue() as XmlNode;
					continue;
				}
				break;
			}
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A decryption key could not be found.</exception>
		public override object GetOutput()
		{
			if (_encryptedDataList != null)
			{
				ProcessElementRecursively(_encryptedDataList);
			}
			Utils.AddNamespaces(_containingDocument.DocumentElement, base.PropagatedNamespaces);
			return _containingDocument;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <param name="type">The type of the output to return. <see cref="T:System.Xml.XmlNodeList" /> is the only valid type for this parameter.</param>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not an <see cref="T:System.Xml.XmlNodeList" /> object.</exception>
		public override object GetOutput(Type type)
		{
			if (type == typeof(XmlDocument))
			{
				return (XmlDocument)GetOutput();
			}
			throw new ArgumentException("The input type was invalid for this transform.", "type");
		}
	}
	/// <summary>Represents the <see langword="Base64" /> decoding transform as defined in Section 6.6.2 of the XMLDSIG specification.</summary>
	public class XmlDsigBase64Transform : Transform
	{
		private Type[] _inputTypes = new Type[3]
		{
			typeof(Stream),
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		private Type[] _outputTypes = new Type[1] { typeof(Stream) };

		private CryptoStream _cs;

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigBase64Transform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigBase64Transform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</returns>
		public override Type[] InputTypes => _inputTypes;

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigBase64Transform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigBase64Transform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</returns>
		public override Type[] OutputTypes => _outputTypes;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> class.</summary>
		public XmlDsigBase64Transform()
		{
			base.Algorithm = "http://www.w3.org/2000/09/xmldsig#base64";
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a <see langword="&lt;Transform&gt;" /> element; this method is not supported because the <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object has no inner XML elements.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</param>
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
		}

		/// <summary>Returns an XML representation of the parameters of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object that are suitable to be included as subelements of an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object in an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</returns>
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is a <see cref="T:System.IO.Stream" /> and it is <see langword="null" />.</exception>
		public override void LoadInput(object obj)
		{
			if (obj is Stream)
			{
				LoadStreamInput((Stream)obj);
			}
			else if (obj is XmlNodeList)
			{
				LoadXmlNodeListInput((XmlNodeList)obj);
			}
			else if (obj is XmlDocument)
			{
				LoadXmlNodeListInput(((XmlDocument)obj).SelectNodes("//."));
			}
		}

		private void LoadStreamInput(Stream inputStream)
		{
			if (inputStream == null)
			{
				throw new ArgumentException("obj");
			}
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = new byte[1024];
			int num;
			do
			{
				num = inputStream.Read(array, 0, 1024);
				if (num <= 0)
				{
					continue;
				}
				int num2 = 0;
				int i;
				for (i = 0; i < num && !char.IsWhiteSpace((char)array[i]); i++)
				{
				}
				num2 = i;
				for (i++; i < num; i++)
				{
					if (!char.IsWhiteSpace((char)array[i]))
					{
						array[num2] = array[i];
						num2++;
					}
				}
				memoryStream.Write(array, 0, num2);
			}
			while (num > 0);
			memoryStream.Position = 0L;
			_cs = new CryptoStream(memoryStream, new FromBase64Transform(), CryptoStreamMode.Read);
		}

		private void LoadXmlNodeListInput(XmlNodeList nodeList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (XmlNode node in nodeList)
			{
				XmlNode xmlNode = node.SelectSingleNode("self::text()");
				if (xmlNode != null)
				{
					stringBuilder.Append(xmlNode.OuterXml);
				}
			}
			byte[] bytes = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false).GetBytes(stringBuilder.ToString());
			int num = 0;
			int i;
			for (i = 0; i < bytes.Length && !char.IsWhiteSpace((char)bytes[i]); i++)
			{
			}
			num = i;
			for (i++; i < bytes.Length; i++)
			{
				if (!char.IsWhiteSpace((char)bytes[i]))
				{
					bytes[num] = bytes[i];
					num++;
				}
			}
			MemoryStream stream = new MemoryStream(bytes, 0, num);
			_cs = new CryptoStream(stream, new FromBase64Transform(), CryptoStreamMode.Read);
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object.</returns>
		public override object GetOutput()
		{
			return _cs;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object of type <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="type">The type of the output to return. <see cref="T:System.IO.Stream" /> is the only valid type for this parameter.</param>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigBase64Transform" /> object of type <see cref="T:System.IO.Stream" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a <see cref="T:System.IO.Stream" /> object.</exception>
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			return _cs;
		}
	}
	/// <summary>Represents the C14N XML canonicalization transform for a digital signature as defined by the World Wide Web Consortium (W3C), without comments.</summary>
	public class XmlDsigC14NTransform : Transform
	{
		private Type[] _inputTypes = new Type[3]
		{
			typeof(Stream),
			typeof(XmlDocument),
			typeof(XmlNodeList)
		};

		private Type[] _outputTypes = new Type[1] { typeof(Stream) };

		private CanonicalXml _cXml;

		private bool _includeComments;

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</returns>
		public override Type[] InputTypes => _inputTypes;

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object; the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object return only objects of one of these types.</returns>
		public override Type[] OutputTypes => _outputTypes;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> class.</summary>
		public XmlDsigC14NTransform()
		{
			base.Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> class with comments, if specified.</summary>
		/// <param name="includeComments">
		///   <see langword="true" /> to include comments; otherwise, <see langword="false" />.</param>
		public XmlDsigC14NTransform(bool includeComments)
		{
			_includeComments = includeComments;
			base.Algorithm = (includeComments ? "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments" : "http://www.w3.org/TR/2001/REC-xml-c14n-20010315");
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a <see langword="&lt;Transform&gt;" /> element; this method is not supported because this element has no inner XML elements.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</param>
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList != null && nodeList.Count > 0)
			{
				throw new CryptographicException("Unknown transform has been encountered.");
			}
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object that are suitable to be included as subelements of an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object in an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</returns>
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is a <see cref="T:System.IO.Stream" /> object and it is <see langword="null" />.</exception>
		public override void LoadInput(object obj)
		{
			XmlResolver resolver = (base.ResolverSet ? _xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI));
			if (obj is Stream)
			{
				_cXml = new CanonicalXml((Stream)obj, _includeComments, resolver, base.BaseURI);
				return;
			}
			if (obj is XmlDocument)
			{
				_cXml = new CanonicalXml((XmlDocument)obj, resolver, _includeComments);
				return;
			}
			if (obj is XmlNodeList)
			{
				_cXml = new CanonicalXml((XmlNodeList)obj, resolver, _includeComments);
				return;
			}
			throw new ArgumentException("Type of input object is invalid.", "obj");
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</returns>
		public override object GetOutput()
		{
			return new MemoryStream(_cXml.GetBytes());
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object of type <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="type">The type of the output to return. <see cref="T:System.IO.Stream" /> is the only valid type for this parameter.</param>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object of type <see cref="T:System.IO.Stream" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a <see cref="T:System.IO.Stream" /> object.</exception>
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			return new MemoryStream(_cXml.GetBytes());
		}

		/// <summary>Returns the digest associated with an <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <param name="hash">The <see cref="T:System.Security.Cryptography.HashAlgorithm" /> object used to create a digest.</param>
		/// <returns>The digest associated with an <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</returns>
		public override byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return _cXml.GetDigestedBytes(hash);
		}
	}
	/// <summary>Represents the C14N XML canonicalization transform for a digital signature as defined by the World Wide Web Consortium (W3C), with comments.</summary>
	public class XmlDsigC14NWithCommentsTransform : XmlDsigC14NTransform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NWithCommentsTransform" /> class.</summary>
		public XmlDsigC14NWithCommentsTransform()
			: base(includeComments: true)
		{
			base.Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";
		}
	}
	/// <summary>Represents the enveloped signature transform for an XML digital signature as defined by the W3C.</summary>
	public class XmlDsigEnvelopedSignatureTransform : Transform
	{
		private Type[] _inputTypes = new Type[3]
		{
			typeof(Stream),
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		private Type[] _outputTypes = new Type[2]
		{
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		private XmlNodeList _inputNodeList;

		private bool _includeComments;

		private XmlNamespaceManager _nsm;

		private XmlDocument _containingDocument;

		private int _signaturePosition;

		internal int SignaturePosition
		{
			set
			{
				_signaturePosition = value;
			}
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		public override Type[] InputTypes => _inputTypes;

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		public override Type[] OutputTypes => _outputTypes;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> class.</summary>
		public XmlDsigEnvelopedSignatureTransform()
		{
			base.Algorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> class with comments, if specified.</summary>
		/// <param name="includeComments">
		///   <see langword="true" /> to include comments; otherwise, <see langword="false" />.</param>
		public XmlDsigEnvelopedSignatureTransform(bool includeComments)
		{
			_includeComments = includeComments;
			base.Algorithm = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> as transform-specific content of a <see langword="&lt;Transform&gt;" /> element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object to match the <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</param>
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList != null && nodeList.Count > 0)
			{
				throw new CryptographicException("Unknown transform has been encountered.");
			}
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object that are suitable to be included as subelements of an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object in an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</returns>
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The containing XML document is <see langword="null" />.</exception>
		public override void LoadInput(object obj)
		{
			if (obj is Stream)
			{
				LoadStreamInput((Stream)obj);
			}
			else if (obj is XmlNodeList)
			{
				LoadXmlNodeListInput((XmlNodeList)obj);
			}
			else if (obj is XmlDocument)
			{
				LoadXmlDocumentInput((XmlDocument)obj);
			}
		}

		private void LoadStreamInput(Stream stream)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			XmlResolver xmlResolver = (base.ResolverSet ? _xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI));
			XmlReader reader = Utils.PreProcessStreamInput(stream, xmlResolver, base.BaseURI);
			xmlDocument.Load(reader);
			_containingDocument = xmlDocument;
			if (_containingDocument == null)
			{
				throw new CryptographicException("An XmlDocument context is required for enveloped transforms.");
			}
			_nsm = new XmlNamespaceManager(_containingDocument.NameTable);
			_nsm.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
		}

		private void LoadXmlNodeListInput(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new ArgumentNullException("nodeList");
			}
			_containingDocument = Utils.GetOwnerDocument(nodeList);
			if (_containingDocument == null)
			{
				throw new CryptographicException("An XmlDocument context is required for enveloped transforms.");
			}
			_nsm = new XmlNamespaceManager(_containingDocument.NameTable);
			_nsm.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
			_inputNodeList = nodeList;
		}

		private void LoadXmlDocumentInput(XmlDocument doc)
		{
			if (doc == null)
			{
				throw new ArgumentNullException("doc");
			}
			_containingDocument = doc;
			_nsm = new XmlNamespaceManager(_containingDocument.NameTable);
			_nsm.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The containing XML document is <see langword="null" />.</exception>
		public override object GetOutput()
		{
			if (_containingDocument == null)
			{
				throw new CryptographicException("An XmlDocument context is required for enveloped transforms.");
			}
			if (_inputNodeList != null)
			{
				if (_signaturePosition == 0)
				{
					return _inputNodeList;
				}
				XmlNodeList xmlNodeList = _containingDocument.SelectNodes("//dsig:Signature", _nsm);
				if (xmlNodeList == null)
				{
					return _inputNodeList;
				}
				CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
				{
					foreach (XmlNode inputNode in _inputNodeList)
					{
						if (inputNode == null)
						{
							continue;
						}
						if (Utils.IsXmlNamespaceNode(inputNode) || Utils.IsNamespaceNode(inputNode))
						{
							canonicalXmlNodeList.Add(inputNode);
							continue;
						}
						try
						{
							XmlNode xmlNode2 = inputNode.SelectSingleNode("ancestor-or-self::dsig:Signature[1]", _nsm);
							int num = 0;
							foreach (XmlNode item in xmlNodeList)
							{
								num++;
								if (item == xmlNode2)
								{
									break;
								}
							}
							if (xmlNode2 == null || (xmlNode2 != null && num != _signaturePosition))
							{
								canonicalXmlNodeList.Add(inputNode);
							}
						}
						catch
						{
						}
					}
					return canonicalXmlNodeList;
				}
			}
			XmlNodeList xmlNodeList2 = _containingDocument.SelectNodes("//dsig:Signature", _nsm);
			if (xmlNodeList2 == null)
			{
				return _containingDocument;
			}
			if (xmlNodeList2.Count < _signaturePosition || _signaturePosition <= 0)
			{
				return _containingDocument;
			}
			xmlNodeList2[_signaturePosition - 1].ParentNode.RemoveChild(xmlNodeList2[_signaturePosition - 1]);
			return _containingDocument;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object of type <see cref="T:System.Xml.XmlNodeList" />.</summary>
		/// <param name="type">The type of the output to return. <see cref="T:System.Xml.XmlNodeList" /> is the only valid type for this parameter.</param>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object of type <see cref="T:System.Xml.XmlNodeList" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not an <see cref="T:System.Xml.XmlNodeList" /> object.</exception>
		public override object GetOutput(Type type)
		{
			if (type == typeof(XmlNodeList) || type.IsSubclassOf(typeof(XmlNodeList)))
			{
				if (_inputNodeList == null)
				{
					_inputNodeList = Utils.AllDescendantNodes(_containingDocument, includeComments: true);
				}
				return (XmlNodeList)GetOutput();
			}
			if (type == typeof(XmlDocument) || type.IsSubclassOf(typeof(XmlDocument)))
			{
				if (_inputNodeList != null)
				{
					throw new ArgumentException("The input type was invalid for this transform.", "type");
				}
				return (XmlDocument)GetOutput();
			}
			throw new ArgumentException("The input type was invalid for this transform.", "type");
		}
	}
	/// <summary>Represents the exclusive C14N XML canonicalization transform for a digital signature as defined by the World Wide Web Consortium (W3C), without comments.</summary>
	public class XmlDsigExcC14NTransform : Transform
	{
		private Type[] _inputTypes = new Type[3]
		{
			typeof(Stream),
			typeof(XmlDocument),
			typeof(XmlNodeList)
		};

		private Type[] _outputTypes = new Type[1] { typeof(Stream) };

		private bool _includeComments;

		private string _inclusiveNamespacesPrefixList;

		private ExcCanonicalXml _excCanonicalXml;

		/// <summary>Gets or sets a string that contains namespace prefixes to canonicalize using the standard canonicalization algorithm.</summary>
		/// <returns>A string that contains namespace prefixes to canonicalize using the standard canonicalization algorithm.</returns>
		public string InclusiveNamespacesPrefixList
		{
			get
			{
				return _inclusiveNamespacesPrefixList;
			}
			set
			{
				_inclusiveNamespacesPrefixList = value;
			}
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</returns>
		public override Type[] InputTypes => _inputTypes;

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object; the <see cref="Overload:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object return only objects of one of these types.</returns>
		public override Type[] OutputTypes => _outputTypes;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> class.</summary>
		public XmlDsigExcC14NTransform()
			: this(includeComments: false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> class specifying a value that determines whether to include comments.</summary>
		/// <param name="includeComments">
		///   <see langword="true" /> to include comments; otherwise, <see langword="false" />.</param>
		public XmlDsigExcC14NTransform(bool includeComments)
			: this(includeComments, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> class specifying a list of namespace prefixes to canonicalize using the standard canonicalization algorithm.</summary>
		/// <param name="inclusiveNamespacesPrefixList">The namespace prefixes to canonicalize using the standard canonicalization algorithm.</param>
		public XmlDsigExcC14NTransform(string inclusiveNamespacesPrefixList)
			: this(includeComments: false, inclusiveNamespacesPrefixList)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> class specifying whether to include comments, and specifying a list of namespace prefixes.</summary>
		/// <param name="includeComments">
		///   <see langword="true" /> to include comments; otherwise, <see langword="false" />.</param>
		/// <param name="inclusiveNamespacesPrefixList">The namespace prefixes to canonicalize using the standard canonicalization algorithm.</param>
		public XmlDsigExcC14NTransform(bool includeComments, string inclusiveNamespacesPrefixList)
		{
			_includeComments = includeComments;
			_inclusiveNamespacesPrefixList = inclusiveNamespacesPrefixList;
			base.Algorithm = (includeComments ? "http://www.w3.org/2001/10/xml-exc-c14n#WithComments" : "http://www.w3.org/2001/10/xml-exc-c14n#");
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a <see langword="&lt;Transform&gt;" /> element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object to match the <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that specifies transform-specific content for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</param>
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				return;
			}
			foreach (XmlNode node in nodeList)
			{
				if (!(node is XmlElement xmlElement))
				{
					continue;
				}
				if (xmlElement.LocalName.Equals("InclusiveNamespaces") && xmlElement.NamespaceURI.Equals("http://www.w3.org/2001/10/xml-exc-c14n#") && Utils.HasAttribute(xmlElement, "PrefixList", "http://www.w3.org/2000/09/xmldsig#"))
				{
					if (!Utils.VerifyAttributes(xmlElement, "PrefixList"))
					{
						throw new CryptographicException("Unknown transform has been encountered.");
					}
					InclusiveNamespacesPrefixList = Utils.GetAttribute(xmlElement, "PrefixList", "http://www.w3.org/2000/09/xmldsig#");
					break;
				}
				throw new CryptographicException("Unknown transform has been encountered.");
			}
		}

		/// <summary>When overridden in a derived class, loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is not a <see cref="T:System.IO.Stream" /> object.  
		///  -or-  
		///  The <paramref name="obj" /> parameter is not an <see cref="T:System.Xml.XmlDocument" /> object.  
		///  -or-  
		///  The <paramref name="obj" /> parameter is not an <see cref="T:System.Xml.XmlNodeList" /> object.</exception>
		public override void LoadInput(object obj)
		{
			XmlResolver resolver = (base.ResolverSet ? _xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI));
			if (obj is Stream)
			{
				_excCanonicalXml = new ExcCanonicalXml((Stream)obj, _includeComments, _inclusiveNamespacesPrefixList, resolver, base.BaseURI);
				return;
			}
			if (obj is XmlDocument)
			{
				_excCanonicalXml = new ExcCanonicalXml((XmlDocument)obj, _includeComments, _inclusiveNamespacesPrefixList, resolver);
				return;
			}
			if (obj is XmlNodeList)
			{
				_excCanonicalXml = new ExcCanonicalXml((XmlNodeList)obj, _includeComments, _inclusiveNamespacesPrefixList, resolver);
				return;
			}
			throw new ArgumentException("Type of input object is invalid.", "obj");
		}

		/// <summary>Returns an XML representation of the parameters of a <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object that are suitable to be included as subelements of an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object in an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</returns>
		protected override XmlNodeList GetInnerXml()
		{
			if (InclusiveNamespacesPrefixList == null)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("Transform", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(base.Algorithm))
			{
				xmlElement.SetAttribute("Algorithm", base.Algorithm);
			}
			XmlElement xmlElement2 = xmlDocument.CreateElement("InclusiveNamespaces", "http://www.w3.org/2001/10/xml-exc-c14n#");
			xmlElement2.SetAttribute("PrefixList", InclusiveNamespacesPrefixList);
			xmlElement.AppendChild(xmlElement2);
			return xmlElement.ChildNodes;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</returns>
		public override object GetOutput()
		{
			return new MemoryStream(_excCanonicalXml.GetBytes());
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object as an object of the specified type.</summary>
		/// <param name="type">The type of the output to return. This must be one of the types in the <see cref="P:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform.OutputTypes" /> property.</param>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object as an object of the specified type.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a <see cref="T:System.IO.Stream" /> object.  
		///  -or-  
		///  The <paramref name="type" /> parameter does not derive from a <see cref="T:System.IO.Stream" /> object.</exception>
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			return new MemoryStream(_excCanonicalXml.GetBytes());
		}

		/// <summary>Returns the digest associated with a <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</summary>
		/// <param name="hash">The <see cref="T:System.Security.Cryptography.HashAlgorithm" /> object used to create a digest.</param>
		/// <returns>The digest associated with a <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NTransform" /> object.</returns>
		public override byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return _excCanonicalXml.GetDigestedBytes(hash);
		}
	}
	/// <summary>Represents the exclusive C14N XML canonicalization transform for a digital signature as defined by the World Wide Web Consortium (W3C), with comments.</summary>
	public class XmlDsigExcC14NWithCommentsTransform : XmlDsigExcC14NTransform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform" /> class.</summary>
		public XmlDsigExcC14NWithCommentsTransform()
			: base(includeComments: true)
		{
			base.Algorithm = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform" /> class specifying a list of namespace prefixes to canonicalize using the standard canonicalization algorithm.</summary>
		/// <param name="inclusiveNamespacesPrefixList">The namespace prefixes to canonicalize using the standard canonicalization algorithm.</param>
		public XmlDsigExcC14NWithCommentsTransform(string inclusiveNamespacesPrefixList)
			: base(includeComments: true, inclusiveNamespacesPrefixList)
		{
			base.Algorithm = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";
		}
	}
	/// <summary>Represents the XPath transform for a digital signature as defined by the W3C.</summary>
	public class XmlDsigXPathTransform : Transform
	{
		private Type[] _inputTypes = new Type[3]
		{
			typeof(Stream),
			typeof(XmlNodeList),
			typeof(XmlDocument)
		};

		private Type[] _outputTypes = new Type[1] { typeof(XmlNodeList) };

		private string _xpathexpr;

		private XmlDocument _document;

		private XmlNamespaceManager _nsm;

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXPathTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXPathTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</returns>
		public override Type[] InputTypes => _inputTypes;

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXPathTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object; the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXPathTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object return only objects of one of these types.</returns>
		public override Type[] OutputTypes => _outputTypes;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> class.</summary>
		public XmlDsigXPathTransform()
		{
			base.Algorithm = "http://www.w3.org/TR/1999/REC-xpath-19991116";
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a <see langword="&lt;Transform&gt;" /> element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object to match the <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="nodeList" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="nodeList" /> parameter does not contain an <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> element.</exception>
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new CryptographicException("Unknown transform has been encountered.");
			}
			foreach (XmlNode node in nodeList)
			{
				string text = null;
				string text2 = null;
				if (!(node is XmlElement xmlElement))
				{
					continue;
				}
				if (xmlElement.LocalName == "XPath")
				{
					_xpathexpr = xmlElement.InnerXml.Trim(null);
					XmlNameTable nameTable = new XmlNodeReader(xmlElement).NameTable;
					_nsm = new XmlNamespaceManager(nameTable);
					if (!Utils.VerifyAttributes(xmlElement, (string)null))
					{
						throw new CryptographicException("Unknown transform has been encountered.");
					}
					foreach (XmlAttribute attribute in xmlElement.Attributes)
					{
						if (attribute.Prefix == "xmlns")
						{
							text = attribute.LocalName;
							text2 = attribute.Value;
							if (text == null)
							{
								text = xmlElement.Prefix;
								text2 = xmlElement.NamespaceURI;
							}
							_nsm.AddNamespace(text, text2);
						}
					}
					break;
				}
				throw new CryptographicException("Unknown transform has been encountered.");
			}
			if (_xpathexpr == null)
			{
				throw new CryptographicException("Unknown transform has been encountered.");
			}
		}

		/// <summary>Returns an XML representation of the parameters of a <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object that are suitable to be included as subelements of an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object in an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</returns>
		protected override XmlNodeList GetInnerXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement(null, "XPath", "http://www.w3.org/2000/09/xmldsig#");
			if (_nsm != null)
			{
				foreach (string item in _nsm)
				{
					switch (item)
					{
					case "xml":
					case "xmlns":
					case null:
						continue;
					}
					if (item.Length > 0)
					{
						xmlElement.SetAttribute("xmlns:" + item, _nsm.LookupNamespace(item));
					}
				}
			}
			xmlElement.InnerXml = _xpathexpr;
			xmlDocument.AppendChild(xmlElement);
			return xmlDocument.ChildNodes;
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</param>
		public override void LoadInput(object obj)
		{
			if (obj is Stream)
			{
				LoadStreamInput((Stream)obj);
			}
			else if (obj is XmlNodeList)
			{
				LoadXmlNodeListInput((XmlNodeList)obj);
			}
			else if (obj is XmlDocument)
			{
				LoadXmlDocumentInput((XmlDocument)obj);
			}
		}

		private void LoadStreamInput(Stream stream)
		{
			XmlResolver xmlResolver = (base.ResolverSet ? _xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI));
			XmlReader reader = Utils.PreProcessStreamInput(stream, xmlResolver, base.BaseURI);
			_document = new XmlDocument();
			_document.PreserveWhitespace = true;
			_document.Load(reader);
		}

		private void LoadXmlNodeListInput(XmlNodeList nodeList)
		{
			XmlResolver resolver = (base.ResolverSet ? _xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI));
			using MemoryStream stream = new MemoryStream(new CanonicalXml(nodeList, resolver, includeComments: true).GetBytes());
			LoadStreamInput(stream);
		}

		private void LoadXmlDocumentInput(XmlDocument doc)
		{
			_document = doc;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object.</returns>
		public override object GetOutput()
		{
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			if (!string.IsNullOrEmpty(_xpathexpr))
			{
				XPathNavigator xPathNavigator = _document.CreateNavigator();
				XPathNodeIterator xPathNodeIterator = xPathNavigator.Select("//. | //@*");
				XPathExpression xPathExpression = xPathNavigator.Compile("boolean(" + _xpathexpr + ")");
				xPathExpression.SetContext(_nsm);
				while (xPathNodeIterator.MoveNext())
				{
					XmlNode node = ((IHasXmlNode)xPathNodeIterator.Current).GetNode();
					if ((bool)xPathNodeIterator.Current.Evaluate(xPathExpression))
					{
						canonicalXmlNodeList.Add(node);
					}
				}
				xPathNodeIterator = xPathNavigator.Select("//namespace::*");
				while (xPathNodeIterator.MoveNext())
				{
					XmlNode node2 = ((IHasXmlNode)xPathNodeIterator.Current).GetNode();
					canonicalXmlNodeList.Add(node2);
				}
			}
			return canonicalXmlNodeList;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object of type <see cref="T:System.Xml.XmlNodeList" />.</summary>
		/// <param name="type">The type of the output to return. <see cref="T:System.Xml.XmlNodeList" /> is the only valid type for this parameter.</param>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXPathTransform" /> object of type <see cref="T:System.Xml.XmlNodeList" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not an <see cref="T:System.Xml.XmlNodeList" /> object.</exception>
		public override object GetOutput(Type type)
		{
			if (type != typeof(XmlNodeList) && !type.IsSubclassOf(typeof(XmlNodeList)))
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			return (XmlNodeList)GetOutput();
		}
	}
	/// <summary>Represents the XSLT transform for a digital signature as defined by the W3C.</summary>
	public class XmlDsigXsltTransform : Transform
	{
		private Type[] _inputTypes = new Type[3]
		{
			typeof(Stream),
			typeof(XmlDocument),
			typeof(XmlNodeList)
		};

		private Type[] _outputTypes = new Type[1] { typeof(Stream) };

		private XmlNodeList _xslNodes;

		private string _xslFragment;

		private Stream _inputStream;

		private bool _includeComments;

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXsltTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXsltTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</returns>
		public override Type[] InputTypes => _inputTypes;

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXsltTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXsltTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</returns>
		public override Type[] OutputTypes => _outputTypes;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> class.</summary>
		public XmlDsigXsltTransform()
		{
			base.Algorithm = "http://www.w3.org/TR/1999/REC-xslt-19991116";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> class with comments, if specified.</summary>
		/// <param name="includeComments">
		///   <see langword="true" /> to include comments; otherwise, <see langword="false" />.</param>
		public XmlDsigXsltTransform(bool includeComments)
		{
			_includeComments = includeComments;
			base.Algorithm = "http://www.w3.org/TR/1999/REC-xslt-19991116";
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a <see langword="&lt;Transform&gt;" /> element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object to match the <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that encapsulates an XSLT style sheet to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object. This style sheet is applied to the document loaded by the <see cref="M:System.Security.Cryptography.Xml.XmlDsigXsltTransform.LoadInput(System.Object)" /> method.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="nodeList" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="nodeList" /> parameter does not contain an <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</exception>
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new CryptographicException("Unknown transform has been encountered.");
			}
			XmlElement xmlElement = null;
			int num = 0;
			foreach (XmlNode node in nodeList)
			{
				if (node is XmlWhitespace)
				{
					continue;
				}
				if (node is XmlElement)
				{
					if (num != 0)
					{
						throw new CryptographicException("Unknown transform has been encountered.");
					}
					xmlElement = node as XmlElement;
					num++;
				}
				else
				{
					num++;
				}
			}
			if (num != 1 || xmlElement == null)
			{
				throw new CryptographicException("Unknown transform has been encountered.");
			}
			_xslNodes = nodeList;
			_xslFragment = xmlElement.OuterXml.Trim(null);
		}

		/// <summary>Returns an XML representation of the parameters of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object that are suitable to be included as subelements of an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object in an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</returns>
		protected override XmlNodeList GetInnerXml()
		{
			return _xslNodes;
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</param>
		public override void LoadInput(object obj)
		{
			if (_inputStream != null)
			{
				_inputStream.Close();
			}
			_inputStream = new MemoryStream();
			if (obj is Stream)
			{
				_inputStream = (Stream)obj;
			}
			else if (obj is XmlNodeList)
			{
				byte[] bytes = new CanonicalXml((XmlNodeList)obj, null, _includeComments).GetBytes();
				if (bytes != null)
				{
					_inputStream.Write(bytes, 0, bytes.Length);
					_inputStream.Flush();
					_inputStream.Position = 0L;
				}
			}
			else if (obj is XmlDocument)
			{
				byte[] bytes2 = new CanonicalXml((XmlDocument)obj, null, _includeComments).GetBytes();
				if (bytes2 != null)
				{
					_inputStream.Write(bytes2, 0, bytes2.Length);
					_inputStream.Flush();
					_inputStream.Position = 0L;
				}
			}
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object.</returns>
		public override object GetOutput()
		{
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.XmlResolver = null;
			xmlReaderSettings.MaxCharactersFromEntities = 10000000L;
			xmlReaderSettings.MaxCharactersInDocument = 0L;
			using StringReader input = new StringReader(_xslFragment);
			XmlReader stylesheet = XmlReader.Create((TextReader)input, xmlReaderSettings, (string)null);
			xslCompiledTransform.Load(stylesheet, XsltSettings.Default, null);
			XPathDocument input2 = new XPathDocument(XmlReader.Create(_inputStream, xmlReaderSettings, base.BaseURI), XmlSpace.Preserve);
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter results = new XmlTextWriter(memoryStream, null);
			xslCompiledTransform.Transform(input2, null, results);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object of type <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="type">The type of the output to return. <see cref="T:System.IO.Stream" /> is the only valid type for this parameter.</param>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigXsltTransform" /> object of type <see cref="T:System.IO.Stream" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a <see cref="T:System.IO.Stream" /> object.</exception>
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			return (Stream)GetOutput();
		}
	}
	/// <summary>Represents the license transform algorithm used to normalize XrML licenses for signatures.</summary>
	public class XmlLicenseTransform : Transform
	{
		private Type[] _inputTypes = new Type[1] { typeof(XmlDocument) };

		private Type[] _outputTypes = new Type[1] { typeof(XmlDocument) };

		private XmlNamespaceManager _namespaceManager;

		private XmlDocument _license;

		private IRelDecryptor _relDecryptor;

		private const string ElementIssuer = "issuer";

		private const string NamespaceUriCore = "urn:mpeg:mpeg21:2003:01-REL-R-NS";

		/// <summary>Gets an array of types that are valid inputs to the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>An array of types that are valid inputs to the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object; you can pass only objects of one of these types to the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		public override Type[] InputTypes => _inputTypes;

		/// <summary>Gets an array of types that are valid outputs from the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlLicenseTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		public override Type[] OutputTypes => _outputTypes;

		/// <summary>Gets or sets the decryptor of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>The decryptor of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		public IRelDecryptor Decryptor
		{
			get
			{
				return _relDecryptor;
			}
			set
			{
				_relDecryptor = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> class.</summary>
		public XmlLicenseTransform()
		{
			base.Algorithm = "urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform";
		}

		private void DecryptEncryptedGrants(XmlNodeList encryptedGrantList, IRelDecryptor decryptor)
		{
			XmlElement xmlElement = null;
			XmlElement xmlElement2 = null;
			XmlElement xmlElement3 = null;
			EncryptionMethod encryptionMethod = null;
			KeyInfo keyInfo = null;
			CipherData cipherData = null;
			int i = 0;
			for (int count = encryptedGrantList.Count; i < count; i++)
			{
				xmlElement = encryptedGrantList[i].SelectSingleNode("//r:encryptedGrant/enc:EncryptionMethod", _namespaceManager) as XmlElement;
				xmlElement2 = encryptedGrantList[i].SelectSingleNode("//r:encryptedGrant/dsig:KeyInfo", _namespaceManager) as XmlElement;
				xmlElement3 = encryptedGrantList[i].SelectSingleNode("//r:encryptedGrant/enc:CipherData", _namespaceManager) as XmlElement;
				if (xmlElement != null && xmlElement2 != null && xmlElement3 != null)
				{
					encryptionMethod = new EncryptionMethod();
					keyInfo = new KeyInfo();
					cipherData = new CipherData();
					encryptionMethod.LoadXml(xmlElement);
					keyInfo.LoadXml(xmlElement2);
					cipherData.LoadXml(xmlElement3);
					MemoryStream memoryStream = null;
					Stream stream = null;
					StreamReader streamReader = null;
					try
					{
						memoryStream = new MemoryStream(cipherData.CipherValue);
						stream = _relDecryptor.Decrypt(encryptionMethod, keyInfo, memoryStream);
						if (stream == null || stream.Length == 0L)
						{
							throw new CryptographicException("Unable to decrypt grant content.");
						}
						streamReader = new StreamReader(stream);
						string innerXml = streamReader.ReadToEnd();
						encryptedGrantList[i].ParentNode.InnerXml = innerXml;
					}
					finally
					{
						memoryStream?.Close();
						stream?.Close();
						streamReader?.Close();
					}
					encryptionMethod = null;
					keyInfo = null;
					cipherData = null;
				}
				xmlElement = null;
				xmlElement2 = null;
				xmlElement3 = null;
			}
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object that are suitable to be included as subelements of an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object in an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</returns>
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Returns the output of an <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>The output of the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		public override object GetOutput()
		{
			return _license;
		}

		/// <summary>Returns the output of an <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <param name="type">The type of the output to return. <see cref="T:System.Xml.XmlDocument" /> is the only valid type for this parameter.</param>
		/// <returns>The output of the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not an <see cref="T:System.Xml.XmlDocument" /> object.</exception>
		public override object GetOutput(Type type)
		{
			if (type != typeof(XmlDocument) && !type.IsSubclassOf(typeof(XmlDocument)))
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			return GetOutput();
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a <see langword="&lt;Transform&gt;" /> element; this method is not supported because the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object has no inner XML elements.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that encapsulates the transform to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</param>
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList != null && nodeList.Count > 0)
			{
				throw new CryptographicException("Unknown transform has been encountered.");
			}
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object. The type of the input object must be <see cref="T:System.Xml.XmlDocument" />.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The context was not set before this transform was invoked.  
		///  -or-  
		///  The <see langword="&lt;issuer&gt;" /> element was not set before this transform was invoked.  
		///  -or-  
		///  The <see langword="&lt;license&gt;" /> element was not set before this transform was invoked.  
		///  -or-  
		///  The <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.Decryptor" /> property was not set before this transform was invoked.</exception>
		public override void LoadInput(object obj)
		{
			if (base.Context == null)
			{
				throw new CryptographicException("Null Context property encountered.");
			}
			_license = new XmlDocument();
			_license.PreserveWhitespace = true;
			_namespaceManager = new XmlNamespaceManager(_license.NameTable);
			_namespaceManager.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
			_namespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			_namespaceManager.AddNamespace("r", "urn:mpeg:mpeg21:2003:01-REL-R-NS");
			XmlElement xmlElement = null;
			XmlElement xmlElement2 = null;
			XmlNode xmlNode = null;
			if (!(base.Context.SelectSingleNode("ancestor-or-self::r:issuer[1]", _namespaceManager) is XmlElement xmlElement3))
			{
				throw new CryptographicException("Issuer node is required.");
			}
			xmlNode = xmlElement3.SelectSingleNode("descendant-or-self::dsig:Signature[1]", _namespaceManager) as XmlElement;
			xmlNode?.ParentNode.RemoveChild(xmlNode);
			if (!(xmlElement3.SelectSingleNode("ancestor-or-self::r:license[1]", _namespaceManager) is XmlElement xmlElement4))
			{
				throw new CryptographicException("License node is required.");
			}
			XmlNodeList xmlNodeList = xmlElement4.SelectNodes("descendant-or-self::r:license[1]/r:issuer", _namespaceManager);
			int i = 0;
			for (int count = xmlNodeList.Count; i < count; i++)
			{
				if (xmlNodeList[i] != xmlElement3 && xmlNodeList[i].LocalName == "issuer" && xmlNodeList[i].NamespaceURI == "urn:mpeg:mpeg21:2003:01-REL-R-NS")
				{
					xmlNodeList[i].ParentNode.RemoveChild(xmlNodeList[i]);
				}
			}
			XmlNodeList xmlNodeList2 = xmlElement4.SelectNodes("/r:license/r:grant/r:encryptedGrant", _namespaceManager);
			if (xmlNodeList2.Count > 0)
			{
				if (_relDecryptor == null)
				{
					throw new CryptographicException("IRelDecryptor is required.");
				}
				DecryptEncryptedGrants(xmlNodeList2, _relDecryptor);
			}
			_license.InnerXml = xmlElement4.OuterXml;
		}
	}
}
namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class defines the algorithm used for a cryptographic operation.</summary>
	public sealed class AlgorithmIdentifier
	{
		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.Oid" /> property sets or retrieves the <see cref="T:System.Security.Cryptography.Oid" /> object that specifies the object identifier for the algorithm.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the algorithm.</returns>
		public Oid Oid { get; set; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.KeyLength" /> property sets or retrieves the key length, in bits. This property is not used for algorithms that use a fixed key length.</summary>
		/// <returns>An int value that represents the key length, in bits.</returns>
		public int KeyLength { get; set; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.Parameters" /> property sets or retrieves any parameters required by the algorithm.</summary>
		/// <returns>An array of byte values that specifies any parameters required by the algorithm.</returns>
		public byte[] Parameters { get; set; } = Array.Empty<byte>();

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class by using a set of default parameters.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public AlgorithmIdentifier()
			: this(Oid.FromOidValue("1.2.840.113549.3.7", OidGroup.EncryptionAlgorithm), 0)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor(System.Security.Cryptography.Oid)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class with the specified algorithm identifier.</summary>
		/// <param name="oid">An object identifier for the algorithm.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public AlgorithmIdentifier(Oid oid)
			: this(oid, 0)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.AlgorithmIdentifier.#ctor(System.Security.Cryptography.Oid,System.Int32)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> class with the specified algorithm identifier and key length.</summary>
		/// <param name="oid">An object identifier for the algorithm.</param>
		/// <param name="keyLength">The length, in bits, of the key.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public AlgorithmIdentifier(Oid oid, int keyLength)
		{
			Oid = oid;
			KeyLength = keyLength;
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> class defines the recipient of a CMS/PKCS #7 message.</summary>
	public sealed class CmsRecipient
	{
		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipient.RecipientIdentifierType" /> property retrieves the type of the identifier of the recipient.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the type of the identifier of the recipient.</returns>
		public SubjectIdentifierType RecipientIdentifierType { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipient.Certificate" /> property retrieves the certificate associated with the recipient.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that holds the certificate associated with the recipient.</returns>
		public X509Certificate2 Certificate { get; }

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipient.#ctor(System.Security.Cryptography.X509Certificates.X509Certificate2)" /> constructor constructs an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> class by using the specified recipient certificate.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the recipient certificate.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public CmsRecipient(X509Certificate2 certificate)
			: this(SubjectIdentifierType.IssuerAndSerialNumber, certificate)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipient.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.X509Certificates.X509Certificate2)" /> constructor constructs an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> class by using the specified recipient identifier type and recipient certificate.</summary>
		/// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the type of the identifier of the recipient.</param>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the recipient certificate.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public CmsRecipient(SubjectIdentifierType recipientIdentifierType, X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			switch (recipientIdentifierType)
			{
			case SubjectIdentifierType.Unknown:
				recipientIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
				break;
			default:
				throw new CryptographicException(global::SR.Format("The subject identifier type {0} is not valid.", recipientIdentifierType));
			case SubjectIdentifierType.IssuerAndSerialNumber:
			case SubjectIdentifierType.SubjectKeyIdentifier:
				break;
			}
			RecipientIdentifierType = recipientIdentifierType;
			Certificate = certificate;
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> class represents a set of <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> objects. <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> implements the <see cref="T:System.Collections.ICollection" /> interface.</summary>
	public sealed class CmsRecipientCollection : ICollection, IEnumerable
	{
		private readonly List<CmsRecipient> _recipients;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Item(System.Int32)" /> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object at the specified index in the collection.</summary>
		/// <param name="index">An <see cref="T:System.Int32" /> value that represents the index in the collection. The index is zero based.</param>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		public CmsRecipient this[int index]
		{
			get
			{
				if (index < 0 || index >= _recipients.Count)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				return _recipients[index];
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Count" /> property retrieves the number of items in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that represents the number of items in the collection.</returns>
		public int Count => _recipients.Count;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.IsSynchronized" /> property retrieves whether access to the collection is synchronized, or thread safe. This property always returns <see langword="false" />, which means that the collection is not thread safe.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value of <see langword="false" />, which means that the collection is not thread safe.</returns>
		public bool IsSynchronized => false;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientCollection.SyncRoot" /> property retrieves an <see cref="T:System.Object" /> object used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> object that is used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		public object SyncRoot => this;

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> class.</summary>
		public CmsRecipientCollection()
		{
			_recipients = new List<CmsRecipient>();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.#ctor(System.Security.Cryptography.Pkcs.CmsRecipient)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> class and adds the specified recipient.</summary>
		/// <param name="recipient">An instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> class that represents the specified CMS/PKCS #7 recipient.</param>
		public CmsRecipientCollection(CmsRecipient recipient)
		{
			_recipients = new List<CmsRecipient>(1);
			_recipients.Add(recipient);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> class and adds recipients based on the specified subject identifier and set of certificates that identify the recipients.</summary>
		/// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the type of subject identifier.</param>
		/// <param name="certificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that contains the certificates that identify the recipients.</param>
		public CmsRecipientCollection(SubjectIdentifierType recipientIdentifierType, X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new NullReferenceException();
			}
			_recipients = new List<CmsRecipient>(certificates.Count);
			for (int i = 0; i < certificates.Count; i++)
			{
				_recipients.Add(new CmsRecipient(recipientIdentifierType, certificates[i]));
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Add(System.Security.Cryptography.Pkcs.CmsRecipient)" /> method adds a recipient to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <param name="recipient">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the recipient to add to the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</param>
		/// <returns>If the method succeeds, the method returns an <see cref="T:System.Int32" /> value that represents the zero-based position where the recipient is to be inserted.  
		///  If the method fails, it throws an exception.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="recipient" /> is <see langword="null" />.</exception>
		public int Add(CmsRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			int count = _recipients.Count;
			_recipients.Add(recipient);
			return count;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.Remove(System.Security.Cryptography.Pkcs.CmsRecipient)" /> method removes a recipient from the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <param name="recipient">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the recipient to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="recipient" /> is <see langword="null" />.</exception>
		public void Remove(CmsRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			_recipients.Remove(recipient);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		public CmsRecipientEnumerator GetEnumerator()
		{
			return new CmsRecipientEnumerator(this);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.System#Collections#IEnumerable#GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CmsRecipientEnumerator(this);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.CopyTo(System.Array,System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection to an array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object to which the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not large enough to hold the specified elements.  
		///
		/// -or-  
		///
		/// <paramref name="array" /> does not contain the proper number of dimensions.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of elements in <paramref name="array" />.</exception>
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (index > array.Length - Count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			for (int i = 0; i < Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientCollection.CopyTo(System.Security.Cryptography.Pkcs.CmsRecipient[],System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection to a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> array.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> objects where the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index for the array of <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> objects in <paramref name="array" /> to which the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not large enough to hold the specified elements.  
		///
		/// -or-  
		///
		/// <paramref name="array" /> does not contain the proper number of dimensions.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of elements in <paramref name="array" />.</exception>
		public void CopyTo(CmsRecipient[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (index > array.Length - Count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			_recipients.CopyTo(array, index);
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> class provides enumeration functionality for the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection. <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> implements the <see cref="T:System.Collections.IEnumerator" /> interface.</summary>
	public sealed class CmsRecipientEnumerator : IEnumerator
	{
		private readonly CmsRecipientCollection _recipients;

		private int _current;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the current recipient in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		public CmsRecipient Current => _recipients[_current];

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.System#Collections#IEnumerator#Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the current recipient in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		object IEnumerator.Current => _recipients[_current];

		internal CmsRecipientEnumerator(CmsRecipientCollection recipients)
		{
			_recipients = recipients;
			_current = -1;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.MoveNext" /> method advances the enumeration to the next <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object; <see langword="false" /> if the enumeration moved past the last item in the enumeration.</returns>
		public bool MoveNext()
		{
			if (_current >= _recipients.Count - 1)
			{
				return false;
			}
			_current++;
			return true;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.Reset" /> method resets the enumeration to the first <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		public void Reset()
		{
			_current = -1;
		}

		internal CmsRecipientEnumerator()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	internal abstract class CmsSignature
	{
		private class DSACmsSignature : CmsSignature
		{
			private readonly HashAlgorithmName _expectedDigest;

			private readonly string _signatureAlgorithm;

			internal DSACmsSignature(string signatureAlgorithm, HashAlgorithmName expectedDigest)
			{
				_signatureAlgorithm = signatureAlgorithm;
				_expectedDigest = expectedDigest;
			}

			internal override bool VerifySignature(byte[] valueHash, byte[] signature, string digestAlgorithmOid, HashAlgorithmName digestAlgorithmName, ReadOnlyMemory<byte>? signatureParameters, X509Certificate2 certificate)
			{
				if (_expectedDigest != digestAlgorithmName)
				{
					throw new CryptographicException(global::SR.Format("SignerInfo digest algorithm '{0}' is not valid for signature algorithm '{1}'.", digestAlgorithmOid, _signatureAlgorithm));
				}
				DSA dSAPublicKey = certificate.GetDSAPublicKey();
				if (dSAPublicKey == null)
				{
					return false;
				}
				byte[] array = new byte[2 * dSAPublicKey.ExportParameters(includePrivateParameters: false).Q.Length];
				if (!DsaDerToIeee(signature, array))
				{
					return false;
				}
				return dSAPublicKey.VerifySignature(valueHash, array);
			}

			protected override bool Sign(byte[] dataHash, HashAlgorithmName hashAlgorithmName, X509Certificate2 certificate, bool silent, out Oid signatureAlgorithm, out byte[] signatureValue)
			{
				DSA dSA = PkcsPal.Instance.GetPrivateKeyForSigning<DSA>(certificate, silent) ?? certificate.GetDSAPublicKey();
				if (dSA == null)
				{
					signatureAlgorithm = null;
					signatureValue = null;
					return false;
				}
				string text = ((hashAlgorithmName == HashAlgorithmName.SHA1) ? "1.2.840.10040.4.3" : ((hashAlgorithmName == HashAlgorithmName.SHA256) ? "2.16.840.1.101.3.4.3.2" : ((hashAlgorithmName == HashAlgorithmName.SHA384) ? "2.16.840.1.101.3.4.3.3" : ((hashAlgorithmName == HashAlgorithmName.SHA512) ? "2.16.840.1.101.3.4.3.4" : null))));
				if (text == null)
				{
					signatureAlgorithm = null;
					signatureValue = null;
					return false;
				}
				signatureAlgorithm = new Oid(text, text);
				byte[] array = dSA.CreateSignature(dataHash);
				signatureValue = DsaIeeeToDer(new ReadOnlySpan<byte>(array));
				return true;
			}
		}

		private class ECDsaCmsSignature : CmsSignature
		{
			private readonly HashAlgorithmName _expectedDigest;

			private readonly string _signatureAlgorithm;

			internal ECDsaCmsSignature(string signatureAlgorithm, HashAlgorithmName expectedDigest)
			{
				_signatureAlgorithm = signatureAlgorithm;
				_expectedDigest = expectedDigest;
			}

			internal override bool VerifySignature(byte[] valueHash, byte[] signature, string digestAlgorithmOid, HashAlgorithmName digestAlgorithmName, ReadOnlyMemory<byte>? signatureParameters, X509Certificate2 certificate)
			{
				if (_expectedDigest != digestAlgorithmName)
				{
					throw new CryptographicException(global::SR.Format("SignerInfo digest algorithm '{0}' is not valid for signature algorithm '{1}'.", digestAlgorithmOid, _signatureAlgorithm));
				}
				ECDsa eCDsaPublicKey = certificate.GetECDsaPublicKey();
				if (eCDsaPublicKey == null)
				{
					return false;
				}
				byte[] array = new byte[eCDsaPublicKey.KeySize / 4];
				if (!DsaDerToIeee(signature, array))
				{
					return false;
				}
				return eCDsaPublicKey.VerifyHash(valueHash, array);
			}

			protected override bool Sign(byte[] dataHash, HashAlgorithmName hashAlgorithmName, X509Certificate2 certificate, bool silent, out Oid signatureAlgorithm, out byte[] signatureValue)
			{
				ECDsa eCDsa = PkcsPal.Instance.GetPrivateKeyForSigning<ECDsa>(certificate, silent) ?? certificate.GetECDsaPublicKey();
				if (eCDsa == null)
				{
					signatureAlgorithm = null;
					signatureValue = null;
					return false;
				}
				string text = ((hashAlgorithmName == HashAlgorithmName.SHA1) ? "1.2.840.10045.4.1" : ((hashAlgorithmName == HashAlgorithmName.SHA256) ? "1.2.840.10045.4.3.2" : ((hashAlgorithmName == HashAlgorithmName.SHA384) ? "1.2.840.10045.4.3.3" : ((hashAlgorithmName == HashAlgorithmName.SHA512) ? "1.2.840.10045.4.3.4" : null))));
				if (text == null)
				{
					signatureAlgorithm = null;
					signatureValue = null;
					return false;
				}
				signatureAlgorithm = new Oid(text, text);
				signatureValue = DsaIeeeToDer(eCDsa.SignHash(dataHash));
				return true;
			}
		}

		private abstract class RSACmsSignature : CmsSignature
		{
			private readonly string _signatureAlgorithm;

			private readonly HashAlgorithmName? _expectedDigest;

			protected RSACmsSignature(string signatureAlgorithm, HashAlgorithmName? expectedDigest)
			{
				_signatureAlgorithm = signatureAlgorithm;
				_expectedDigest = expectedDigest;
			}

			internal override bool VerifySignature(byte[] valueHash, byte[] signature, string digestAlgorithmOid, HashAlgorithmName digestAlgorithmName, ReadOnlyMemory<byte>? signatureParameters, X509Certificate2 certificate)
			{
				if (_expectedDigest.HasValue && _expectedDigest.Value != digestAlgorithmName)
				{
					throw new CryptographicException(global::SR.Format("SignerInfo digest algorithm '{0}' is not valid for signature algorithm '{1}'.", digestAlgorithmOid, _signatureAlgorithm));
				}
				RSASignaturePadding signaturePadding = GetSignaturePadding(signatureParameters, digestAlgorithmOid, digestAlgorithmName, valueHash.Length);
				return certificate.GetRSAPublicKey()?.VerifyHash(valueHash, signature, digestAlgorithmName, signaturePadding) ?? false;
			}

			protected abstract RSASignaturePadding GetSignaturePadding(ReadOnlyMemory<byte>? signatureParameters, string digestAlgorithmOid, HashAlgorithmName digestAlgorithmName, int digestValueLength);
		}

		private sealed class RSAPkcs1CmsSignature : RSACmsSignature
		{
			public RSAPkcs1CmsSignature(string signatureAlgorithm, HashAlgorithmName? expectedDigest)
				: base(signatureAlgorithm, expectedDigest)
			{
			}

			protected override RSASignaturePadding GetSignaturePadding(ReadOnlyMemory<byte>? signatureParameters, string digestAlgorithmOid, HashAlgorithmName digestAlgorithmName, int digestValueLength)
			{
				if (!signatureParameters.HasValue)
				{
					return RSASignaturePadding.Pkcs1;
				}
				Span<byte> span = stackalloc byte[2];
				span[0] = 5;
				span[1] = 0;
				if (span.SequenceEqual(signatureParameters.Value.Span))
				{
					return RSASignaturePadding.Pkcs1;
				}
				throw new CryptographicException("Invalid signature paramters.");
			}

			protected override bool Sign(byte[] dataHash, HashAlgorithmName hashAlgorithmName, X509Certificate2 certificate, bool silent, out Oid signatureAlgorithm, out byte[] signatureValue)
			{
				RSA rSA = PkcsPal.Instance.GetPrivateKeyForSigning<RSA>(certificate, silent) ?? certificate.GetRSAPublicKey();
				if (rSA == null)
				{
					signatureAlgorithm = null;
					signatureValue = null;
					return false;
				}
				signatureAlgorithm = new Oid("1.2.840.113549.1.1.1", "1.2.840.113549.1.1.1");
				signatureValue = rSA.SignHash(dataHash, hashAlgorithmName, RSASignaturePadding.Pkcs1);
				return true;
			}
		}

		private class RSAPssCmsSignature : RSACmsSignature
		{
			public RSAPssCmsSignature()
				: base(null, null)
			{
			}

			protected override RSASignaturePadding GetSignaturePadding(ReadOnlyMemory<byte>? signatureParameters, string digestAlgorithmOid, HashAlgorithmName digestAlgorithmName, int digestValueLength)
			{
				if (!signatureParameters.HasValue)
				{
					throw new CryptographicException("PSS parameters were not present.");
				}
				PssParamsAsn pssParamsAsn = AsnSerializer.Deserialize<PssParamsAsn>(signatureParameters.Value, AsnEncodingRules.DER);
				if (pssParamsAsn.HashAlgorithm.Algorithm.Value != digestAlgorithmOid)
				{
					throw new CryptographicException(global::SR.Format("This platform requires that the PSS hash algorithm ({0}) match the data digest algorithm ({1}).", pssParamsAsn.HashAlgorithm.Algorithm.Value, digestAlgorithmOid));
				}
				if (pssParamsAsn.TrailerField != 1)
				{
					throw new CryptographicException("Invalid signature paramters.");
				}
				if (pssParamsAsn.SaltLength != digestValueLength)
				{
					throw new CryptographicException(global::SR.Format("PSS salt size {0} is not supported by this platform with hash algorithm {1}.", pssParamsAsn.SaltLength, digestAlgorithmName.Name));
				}
				if (pssParamsAsn.MaskGenAlgorithm.Algorithm.Value != "1.2.840.113549.1.1.8")
				{
					throw new CryptographicException("Mask generation function '{0}' is not supported by this platform.", pssParamsAsn.MaskGenAlgorithm.Algorithm.Value);
				}
				if (!pssParamsAsn.MaskGenAlgorithm.Parameters.HasValue)
				{
					throw new CryptographicException("Invalid signature paramters.");
				}
				AlgorithmIdentifierAsn algorithmIdentifierAsn = AsnSerializer.Deserialize<AlgorithmIdentifierAsn>(pssParamsAsn.MaskGenAlgorithm.Parameters.Value, AsnEncodingRules.DER);
				if (algorithmIdentifierAsn.Algorithm.Value != digestAlgorithmOid)
				{
					throw new CryptographicException(global::SR.Format("This platform does not support the MGF hash algorithm ({0}) being different from the signature hash algorithm ({1}).", algorithmIdentifierAsn.Algorithm.Value, digestAlgorithmOid));
				}
				return RSASignaturePadding.Pss;
			}

			protected override bool Sign(byte[] dataHash, HashAlgorithmName hashAlgorithmName, X509Certificate2 certificate, bool silent, out Oid signatureAlgorithm, out byte[] signatureValue)
			{
				throw new CryptographicException();
			}
		}

		private static readonly Dictionary<string, CmsSignature> s_lookup;

		static CmsSignature()
		{
			s_lookup = new Dictionary<string, CmsSignature>();
			PrepareRegistrationRsa(s_lookup);
			PrepareRegistrationDsa(s_lookup);
			PrepareRegistrationECDsa(s_lookup);
		}

		private static void PrepareRegistrationRsa(Dictionary<string, CmsSignature> lookup)
		{
			lookup.Add("1.2.840.113549.1.1.1", new RSAPkcs1CmsSignature(null, null));
			lookup.Add("1.2.840.113549.1.1.5", new RSAPkcs1CmsSignature("1.2.840.113549.1.1.5", HashAlgorithmName.SHA1));
			lookup.Add("1.2.840.113549.1.1.11", new RSAPkcs1CmsSignature("1.2.840.113549.1.1.11", HashAlgorithmName.SHA256));
			lookup.Add("1.2.840.113549.1.1.12", new RSAPkcs1CmsSignature("1.2.840.113549.1.1.12", HashAlgorithmName.SHA384));
			lookup.Add("1.2.840.113549.1.1.13", new RSAPkcs1CmsSignature("1.2.840.113549.1.1.13", HashAlgorithmName.SHA512));
			lookup.Add("1.2.840.113549.1.1.10", new RSAPssCmsSignature());
		}

		private static void PrepareRegistrationDsa(Dictionary<string, CmsSignature> lookup)
		{
			lookup.Add("1.2.840.10040.4.3", new DSACmsSignature("1.2.840.10040.4.3", HashAlgorithmName.SHA1));
			lookup.Add("2.16.840.1.101.3.4.3.2", new DSACmsSignature("2.16.840.1.101.3.4.3.2", HashAlgorithmName.SHA256));
			lookup.Add("2.16.840.1.101.3.4.3.3", new DSACmsSignature("2.16.840.1.101.3.4.3.3", HashAlgorithmName.SHA384));
			lookup.Add("2.16.840.1.101.3.4.3.4", new DSACmsSignature("2.16.840.1.101.3.4.3.4", HashAlgorithmName.SHA512));
			lookup.Add("1.2.840.10040.4.1", new DSACmsSignature(null, default(HashAlgorithmName)));
		}

		private static void PrepareRegistrationECDsa(Dictionary<string, CmsSignature> lookup)
		{
			lookup.Add("1.2.840.10045.4.1", new ECDsaCmsSignature("1.2.840.10045.4.1", HashAlgorithmName.SHA1));
			lookup.Add("1.2.840.10045.4.3.2", new ECDsaCmsSignature("1.2.840.10045.4.3.2", HashAlgorithmName.SHA256));
			lookup.Add("1.2.840.10045.4.3.3", new ECDsaCmsSignature("1.2.840.10045.4.3.3", HashAlgorithmName.SHA384));
			lookup.Add("1.2.840.10045.4.3.4", new ECDsaCmsSignature("1.2.840.10045.4.3.4", HashAlgorithmName.SHA512));
			lookup.Add("1.2.840.10045.2.1", new ECDsaCmsSignature(null, default(HashAlgorithmName)));
		}

		internal abstract bool VerifySignature(byte[] valueHash, byte[] signature, string digestAlgorithmOid, HashAlgorithmName digestAlgorithmName, ReadOnlyMemory<byte>? signatureParameters, X509Certificate2 certificate);

		protected abstract bool Sign(byte[] dataHash, HashAlgorithmName hashAlgorithmName, X509Certificate2 certificate, bool silent, out Oid signatureAlgorithm, out byte[] signatureValue);

		internal static CmsSignature Resolve(string signatureAlgorithmOid)
		{
			if (s_lookup.TryGetValue(signatureAlgorithmOid, out var value))
			{
				return value;
			}
			return null;
		}

		internal static bool Sign(byte[] dataHash, HashAlgorithmName hashAlgorithmName, X509Certificate2 certificate, bool silent, out Oid oid, out ReadOnlyMemory<byte> signatureValue)
		{
			CmsSignature cmsSignature = Resolve(certificate.GetKeyAlgorithm());
			if (cmsSignature == null)
			{
				oid = null;
				signatureValue = default(ReadOnlyMemory<byte>);
				return false;
			}
			byte[] signatureValue2;
			bool result = cmsSignature.Sign(dataHash, hashAlgorithmName, certificate, silent, out oid, out signatureValue2);
			signatureValue = signatureValue2;
			return result;
		}

		private static bool DsaDerToIeee(ReadOnlyMemory<byte> derSignature, Span<byte> ieeeSignature)
		{
			int num = ieeeSignature.Length / 2;
			try
			{
				AsnReader asnReader = new AsnReader(derSignature, AsnEncodingRules.DER);
				AsnReader asnReader2 = asnReader.ReadSequence();
				if (asnReader.HasData)
				{
					return false;
				}
				ieeeSignature.Clear();
				ReadOnlyMemory<byte> integerBytes = asnReader2.GetIntegerBytes();
				ReadOnlySpan<byte> readOnlySpan = integerBytes.Span;
				if (readOnlySpan.Length > num && readOnlySpan[0] == 0)
				{
					readOnlySpan = readOnlySpan.Slice(1);
				}
				if (readOnlySpan.Length <= num)
				{
					readOnlySpan.CopyTo(ieeeSignature.Slice(num - readOnlySpan.Length, readOnlySpan.Length));
				}
				integerBytes = asnReader2.GetIntegerBytes();
				readOnlySpan = integerBytes.Span;
				if (readOnlySpan.Length > num && readOnlySpan[0] == 0)
				{
					readOnlySpan = readOnlySpan.Slice(1);
				}
				if (readOnlySpan.Length <= num)
				{
					readOnlySpan.CopyTo(ieeeSignature.Slice(num + num - readOnlySpan.Length, readOnlySpan.Length));
				}
				return !asnReader2.HasData;
			}
			catch (CryptographicException)
			{
				return false;
			}
		}

		private static byte[] DsaIeeeToDer(ReadOnlySpan<byte> ieeeSignature)
		{
			int num = ieeeSignature.Length / 2;
			using AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER);
			asnWriter.PushSequence();
			byte[] array = new byte[num + 1];
			Span<byte> destination = new Span<byte>(array, 1, num);
			ieeeSignature.Slice(0, num).CopyTo(destination);
			Array.Reverse(array);
			BigInteger value = new BigInteger(array);
			asnWriter.WriteInteger(value);
			array[0] = 0;
			ieeeSignature.Slice(num, num).CopyTo(destination);
			Array.Reverse(array);
			value = new BigInteger(array);
			asnWriter.WriteInteger(value);
			asnWriter.PopSequence();
			return asnWriter.Encode();
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class provides signing functionality.</summary>
	public sealed class CmsSigner
	{
		private static readonly Oid s_defaultAlgorithm = Oid.FromOidValue("1.3.14.3.2.26", OidGroup.HashAlgorithm);

		private SubjectIdentifierType _signerIdentifierType;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.Certificate" /> property sets or retrieves the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</summary>
		/// <returns>An  <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</returns>
		public X509Certificate2 Certificate { get; set; }

		public AsymmetricAlgorithm PrivateKey { get; set; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.Certificates" /> property retrieves the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that contains certificates associated with the message to be signed.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents the collection of  certificates associated with the message to be signed.</returns>
		public X509Certificate2Collection Certificates { get; private set; } = new X509Certificate2Collection();

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.DigestAlgorithm" /> property sets or retrieves the <see cref="T:System.Security.Cryptography.Oid" /> that represents the hash algorithm used with the signature.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the hash algorithm used with the signature.</returns>
		public Oid DigestAlgorithm { get; set; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.IncludeOption" /> property sets or retrieves the option that controls whether the root and entire chain associated with the signing certificate are included with the created CMS/PKCS #7 message.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.X509Certificates.X509IncludeOption" /> enumeration that specifies how much of the X509 certificate chain should be included in the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object. The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.IncludeOption" /> property can be one of the following <see cref="T:System.Security.Cryptography.X509Certificates.X509IncludeOption" /> members.  
		///   Name  
		///
		///   Value  
		///
		///   Meaning  
		///
		///  <see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.None" /> 0  
		///
		///   The certificate chain is not included.  
		///
		///  <see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.ExcludeRoot" /> 1  
		///
		///   The certificate chain, except for the root certificate, is included.  
		///
		///  <see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.EndCertOnly" /> 2  
		///
		///   Only the end certificate is included.  
		///
		///  <see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.WholeChain" /> 3  
		///
		///   The certificate chain, including the root certificate, is included.</returns>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		public X509IncludeOption IncludeOption { get; set; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.SignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection of signed attributes to be associated with the resulting <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> content. Signed attributes are signed along with the specified content.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the signed attributes. If there are no signed attributes, the property is an empty collection.</returns>
		public CryptographicAttributeObjectCollection SignedAttributes { get; private set; } = new CryptographicAttributeObjectCollection();

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.UnsignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection of unsigned PKCS #9 attributes to be associated with the resulting <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> content. Unsigned attributes can be modified without invalidating the signature.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the unsigned attributes. If there are no unsigned attributes, the property is an empty collection.</returns>
		public CryptographicAttributeObjectCollection UnsignedAttributes { get; private set; } = new CryptographicAttributeObjectCollection();

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.SignerIdentifierType" /> property sets or retrieves the type of the identifier of the signer.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the type of the identifier of the signer.</returns>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		public SubjectIdentifierType SignerIdentifierType
		{
			get
			{
				return _signerIdentifierType;
			}
			set
			{
				if (value < SubjectIdentifierType.IssuerAndSerialNumber || value > SubjectIdentifierType.NoSignature)
				{
					throw new ArgumentException(global::SR.Format("The subject identifier type {0} is not valid.", value));
				}
				_signerIdentifierType = value;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class by using a default subject identifier type.</summary>
		public CmsSigner()
			: this(SubjectIdentifierType.IssuerAndSerialNumber, null)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified subject identifier type.</summary>
		/// <param name="signerIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the signer identifier type.</param>
		public CmsSigner(SubjectIdentifierType signerIdentifierType)
			: this(signerIdentifierType, null)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.X509Certificates.X509Certificate2)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified signing certificate.</summary>
		/// <param name="certificate">An    <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</param>
		public CmsSigner(X509Certificate2 certificate)
			: this(SubjectIdentifierType.IssuerAndSerialNumber, certificate)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.CspParameters)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified cryptographic service provider (CSP) parameters. <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.CspParameters)" /> is useful when you know the specific CSP and private key to use for signing.</summary>
		/// <param name="parameters">A <see cref="T:System.Security.Cryptography.CspParameters" /> object that represents the set of CSP parameters to use.</param>
		public CmsSigner(CspParameters parameters)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.X509Certificates.X509Certificate2)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified signer identifier type and signing certificate.</summary>
		/// <param name="signerIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the signer identifier type.</param>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</param>
		public CmsSigner(SubjectIdentifierType signerIdentifierType, X509Certificate2 certificate)
		{
			switch (signerIdentifierType)
			{
			case SubjectIdentifierType.Unknown:
				_signerIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
				IncludeOption = X509IncludeOption.ExcludeRoot;
				break;
			case SubjectIdentifierType.IssuerAndSerialNumber:
				_signerIdentifierType = signerIdentifierType;
				IncludeOption = X509IncludeOption.ExcludeRoot;
				break;
			case SubjectIdentifierType.SubjectKeyIdentifier:
				_signerIdentifierType = signerIdentifierType;
				IncludeOption = X509IncludeOption.ExcludeRoot;
				break;
			case SubjectIdentifierType.NoSignature:
				_signerIdentifierType = signerIdentifierType;
				IncludeOption = X509IncludeOption.None;
				break;
			default:
				_signerIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
				IncludeOption = X509IncludeOption.ExcludeRoot;
				break;
			}
			Certificate = certificate;
			DigestAlgorithm = new Oid(s_defaultAlgorithm);
		}

		internal void CheckCertificateValue()
		{
			if (SignerIdentifierType != SubjectIdentifierType.NoSignature)
			{
				if (Certificate == null)
				{
					throw new PlatformNotSupportedException("No signer certificate was provided. This platform does not implement the certificate picker UI.");
				}
				if (!Certificate.HasPrivateKey)
				{
					throw new CryptographicException("A certificate with a private key is required.");
				}
			}
		}

		internal SignerInfoAsn Sign(ReadOnlyMemory<byte> data, string contentTypeOid, bool silent, out X509Certificate2Collection chainCerts)
		{
			HashAlgorithmName digestAlgorithm = Helpers.GetDigestAlgorithm(DigestAlgorithm);
			IncrementalHash hasher = IncrementalHash.CreateHash(digestAlgorithm);
			Helpers.AppendData(hasher, data.Span);
			byte[] hashAndReset = hasher.GetHashAndReset();
			SignerInfoAsn result = new SignerInfoAsn
			{
				DigestAlgorithm = 
				{
					Algorithm = DigestAlgorithm
				}
			};
			CryptographicAttributeObjectCollection signedAttributes = SignedAttributes;
			if ((signedAttributes != null && signedAttributes.Count > 0) || contentTypeOid != "1.2.840.113549.1.7.1")
			{
				List<AttributeAsn> list = BuildAttributes(SignedAttributes);
				using (AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER))
				{
					asnWriter.PushSetOf();
					asnWriter.WriteOctetString(hashAndReset);
					asnWriter.PopSetOf();
					list.Add(new AttributeAsn
					{
						AttrType = new Oid("1.2.840.113549.1.9.4", "1.2.840.113549.1.9.4"),
						AttrValues = asnWriter.Encode()
					});
				}
				if (contentTypeOid != null)
				{
					using AsnWriter asnWriter2 = new AsnWriter(AsnEncodingRules.DER);
					asnWriter2.PushSetOf();
					asnWriter2.WriteObjectIdentifier(contentTypeOid);
					asnWriter2.PopSetOf();
					list.Add(new AttributeAsn
					{
						AttrType = new Oid("1.2.840.113549.1.9.3", "1.2.840.113549.1.9.3"),
						AttrValues = asnWriter2.Encode()
					});
				}
				using (AsnWriter asnWriter3 = AsnSerializer.Serialize(new SignedAttributesSet
				{
					SignedAttributes = Helpers.NormalizeSet(list.ToArray(), delegate(byte[] normalized)
					{
						AsnReader asnReader = new AsnReader(normalized, AsnEncodingRules.DER);
						Helpers.AppendData(hasher, asnReader.PeekContentBytes().Span);
					})
				}, AsnEncodingRules.BER))
				{
					result.SignedAttributes = asnWriter3.Encode();
				}
				hashAndReset = hasher.GetHashAndReset();
			}
			switch (SignerIdentifierType)
			{
			case SubjectIdentifierType.IssuerAndSerialNumber:
			{
				byte[] serialNumber = Certificate.GetSerialNumber();
				Array.Reverse(serialNumber);
				result.Sid.IssuerAndSerialNumber = new IssuerAndSerialNumberAsn
				{
					Issuer = Certificate.IssuerName.RawData,
					SerialNumber = serialNumber
				};
				result.Version = 1;
				break;
			}
			case SubjectIdentifierType.SubjectKeyIdentifier:
				result.Sid.SubjectKeyIdentifier = Certificate.GetSubjectKeyIdentifier();
				result.Version = 3;
				break;
			case SubjectIdentifierType.NoSignature:
				result.Sid.IssuerAndSerialNumber = new IssuerAndSerialNumberAsn
				{
					Issuer = SubjectIdentifier.DummySignerEncodedValue,
					SerialNumber = new byte[1]
				};
				result.Version = 1;
				break;
			default:
				throw new CryptographicException();
			}
			if (UnsignedAttributes != null && UnsignedAttributes.Count > 0)
			{
				List<AttributeAsn> list2 = BuildAttributes(UnsignedAttributes);
				result.UnsignedAttributes = Helpers.NormalizeSet(list2.ToArray());
			}
			if (!CmsSignature.Sign(hashAndReset, digestAlgorithm, Certificate, silent, out var oid, out var signatureValue))
			{
				throw new CryptographicException("Could not determine signature algorithm for the signer certificate.");
			}
			result.SignatureValue = signatureValue;
			result.SignatureAlgorithm.Algorithm = oid;
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			x509Certificate2Collection.AddRange(Certificates);
			if (SignerIdentifierType != SubjectIdentifierType.NoSignature)
			{
				if (IncludeOption == X509IncludeOption.EndCertOnly)
				{
					x509Certificate2Collection.Add(Certificate);
				}
				else if (IncludeOption != X509IncludeOption.None)
				{
					X509Chain x509Chain = new X509Chain();
					x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
					x509Chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
					if (!x509Chain.Build(Certificate))
					{
						X509ChainStatus[] chainStatus = x509Chain.ChainStatus;
						foreach (X509ChainStatus x509ChainStatus in chainStatus)
						{
							if (x509ChainStatus.Status == X509ChainStatusFlags.PartialChain)
							{
								throw new CryptographicException("The certificate chain is incomplete, the self-signed root authority could not be determined.");
							}
						}
					}
					X509ChainElementCollection chainElements = x509Chain.ChainElements;
					int count = chainElements.Count;
					int num2 = count - 1;
					if (num2 == 0)
					{
						num2 = -1;
					}
					for (int num3 = 0; num3 < count; num3++)
					{
						X509Certificate2 certificate = chainElements[num3].Certificate;
						if (num3 == num2 && IncludeOption == X509IncludeOption.ExcludeRoot && certificate.SubjectName.RawData.AsSpan().SequenceEqual(certificate.IssuerName.RawData))
						{
							break;
						}
						x509Certificate2Collection.Add(certificate);
					}
				}
			}
			chainCerts = x509Certificate2Collection;
			return result;
		}

		internal static List<AttributeAsn> BuildAttributes(CryptographicAttributeObjectCollection attributes)
		{
			List<AttributeAsn> list = new List<AttributeAsn>();
			if (attributes == null || attributes.Count == 0)
			{
				return list;
			}
			CryptographicAttributeObjectEnumerator enumerator = attributes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				CryptographicAttributeObject current = enumerator.Current;
				using AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER);
				asnWriter.PushSetOf();
				AsnEncodedDataEnumerator enumerator2 = current.Values.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					AsnEncodedData current2 = enumerator2.Current;
					asnWriter.WriteEncodedValue(current2.RawData);
				}
				asnWriter.PopSetOf();
				AttributeAsn item = new AttributeAsn
				{
					AttrType = current.Oid,
					AttrValues = asnWriter.Encode()
				};
				list.Add(item);
			}
			return list;
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> class represents the CMS/PKCS #7 ContentInfo data structure as defined in the CMS/PKCS #7 standards document. This data structure is the basis for all CMS/PKCS #7 messages.</summary>
	public sealed class ContentInfo
	{
		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.ContentInfo.ContentType" /> property  retrieves the <see cref="T:System.Security.Cryptography.Oid" /> object that contains the <paramref name="object identifier" /> (OID)  of the content type of the inner content of the CMS/PKCS #7 message.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that contains the OID value that represents the content type.</returns>
		public Oid ContentType { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.ContentInfo.Content" /> property  retrieves the content of the CMS/PKCS #7 message.</summary>
		/// <returns>An array of byte values that represents the content data.</returns>
		public byte[] Content { get; }

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.#ctor(System.Byte[])" /> constructor  creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> class by using an array of byte values as the data and a default <paramref name="object identifier" /> (OID) that represents the content type.</summary>
		/// <param name="content">An array of byte values that represents the data from which to create the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument.</exception>
		public ContentInfo(byte[] content)
			: this(Oid.FromOidValue("1.2.840.113549.1.7.1", OidGroup.ExtensionOrAttribute), content)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.#ctor(System.Security.Cryptography.Oid,System.Byte[])" /> constructor  creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> class by using the specified content type and an array of byte values as the data.</summary>
		/// <param name="contentType">An <see cref="T:System.Security.Cryptography.Oid" /> object that contains an object identifier (OID) that specifies the content type of the content. This can be data, digestedData, encryptedData, envelopedData, hashedData, signedAndEnvelopedData, or signedData.  For more information, see  Remarks.</param>
		/// <param name="content">An array of byte values that represents the data from which to create the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument.</exception>
		public ContentInfo(Oid contentType, byte[] content)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			ContentType = contentType;
			Content = content;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.GetContentType(System.Byte[])" /> static method  retrieves the outer content type of the encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> message represented by an array of byte values.</summary>
		/// <param name="encodedMessage">An array of byte values that represents the encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> message from which to retrieve the outer content type.</param>
		/// <returns>If the method succeeds, the method returns an <see cref="T:System.Security.Cryptography.Oid" /> object that contains the outer content type of the specified encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> message.  
		///  If the method fails, it throws an exception.</returns>
		/// <exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred during a cryptographic operation.</exception>
		public static Oid GetContentType(byte[] encodedMessage)
		{
			if (encodedMessage == null)
			{
				throw new ArgumentNullException("encodedMessage");
			}
			return PkcsPal.Instance.GetEncodedMessageType(encodedMessage);
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class represents a CMS/PKCS #7 structure for enveloped data.</summary>
	public sealed class EnvelopedCms
	{
		private enum LastCall
		{
			Ctor = 1,
			Encrypt,
			Decode,
			Decrypt
		}

		private DecryptorPal _decryptorPal;

		private byte[] _encodedMessage;

		private LastCall _lastCall;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.Version" /> property retrieves the version of the enveloped CMS/PKCS #7 message.</summary>
		/// <returns>An int value that represents the version of the enveloped CMS/PKCS #7 message.</returns>
		public int Version { get; private set; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentInfo" /> property retrieves the inner content information for the enveloped CMS/PKCS #7 message.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the inner content information from the enveloped CMS/PKCS #7 message.</returns>
		public ContentInfo ContentInfo { get; private set; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentEncryptionAlgorithm" /> property retrieves the identifier of the algorithm used to encrypt the content.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that represents the algorithm identifier.</returns>
		public AlgorithmIdentifier ContentEncryptionAlgorithm { get; private set; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.Certificates" /> property retrieves the set of certificates associated with the enveloped CMS/PKCS #7 message.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents the X.509 certificates used with the enveloped CMS/PKCS #7 message. If no certificates exist, the property value is an empty collection.</returns>
		public X509Certificate2Collection Certificates { get; private set; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.UnprotectedAttributes" /> property retrieves the unprotected (unencrypted) attributes associated with the enveloped CMS/PKCS #7 message. Unprotected attributes are not encrypted, and so do not have data confidentiality within an <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the unprotected attributes. If no unprotected attributes exist, the property value is an empty collection.</returns>
		public CryptographicAttributeObjectCollection UnprotectedAttributes { get; private set; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.RecipientInfos" /> property retrieves the recipient information associated with the enveloped CMS/PKCS #7 message.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection that represents the recipient information. If no recipients exist, the property value is an empty collection.</returns>
		public RecipientInfoCollection RecipientInfos
		{
			get
			{
				switch (_lastCall)
				{
				case LastCall.Ctor:
					return new RecipientInfoCollection();
				case LastCall.Encrypt:
					throw PkcsPal.Instance.CreateRecipientInfosAfterEncryptException();
				case LastCall.Decode:
				case LastCall.Decrypt:
					return _decryptorPal.RecipientInfos;
				default:
					throw new InvalidOperationException();
				}
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class.</summary>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		public EnvelopedCms()
			: this(new ContentInfo(Array.Empty<byte>()))
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified content information as the inner content type.</summary>
		/// <param name="contentInfo">An instance of the <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentInfo" /> class that represents the content and its type.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		public EnvelopedCms(ContentInfo contentInfo)
			: this(contentInfo, new AlgorithmIdentifier(Oid.FromOidValue("1.2.840.113549.3.7", OidGroup.EncryptionAlgorithm)))
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo,System.Security.Cryptography.Pkcs.AlgorithmIdentifier)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified content information and encryption algorithm. The specified content information is to be used as the inner content type.</summary>
		/// <param name="contentInfo">A  <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the content and its type.</param>
		/// <param name="encryptionAlgorithm">An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that specifies the encryption algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		public EnvelopedCms(ContentInfo contentInfo, AlgorithmIdentifier encryptionAlgorithm)
		{
			if (contentInfo == null)
			{
				throw new ArgumentNullException("contentInfo");
			}
			if (encryptionAlgorithm == null)
			{
				throw new ArgumentNullException("encryptionAlgorithm");
			}
			Version = 0;
			ContentInfo = contentInfo;
			ContentEncryptionAlgorithm = encryptionAlgorithm;
			Certificates = new X509Certificate2Collection();
			UnprotectedAttributes = new CryptographicAttributeObjectCollection();
			_decryptorPal = null;
			_lastCall = LastCall.Ctor;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt(System.Security.Cryptography.Pkcs.CmsRecipient)" /> method encrypts the contents of the CMS/PKCS #7 message by using the specified recipient information.</summary>
		/// <param name="recipient">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the recipient information.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public void Encrypt(CmsRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			Encrypt(new CmsRecipientCollection(recipient));
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt(System.Security.Cryptography.Pkcs.CmsRecipientCollection)" /> method encrypts the contents of the CMS/PKCS #7 message by using the information for the specified list of recipients. The message is encrypted by using a message encryption key with a symmetric encryption algorithm such as triple DES. The message encryption key is then encrypted with the public key of each recipient.</summary>
		/// <param name="recipients">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection that represents the information for the list of recipients.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public void Encrypt(CmsRecipientCollection recipients)
		{
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			if (recipients.Count == 0)
			{
				throw new PlatformNotSupportedException("The recipients collection is empty. You must specify at least one recipient. This platform does not implement the certificate picker UI.");
			}
			if (_decryptorPal != null)
			{
				_decryptorPal.Dispose();
				_decryptorPal = null;
			}
			_encodedMessage = PkcsPal.Instance.Encrypt(recipients, ContentInfo, ContentEncryptionAlgorithm, Certificates, UnprotectedAttributes);
			_lastCall = LastCall.Encrypt;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encode" /> method encodes the contents of the enveloped CMS/PKCS #7 message and returns it as an array of byte values. Encryption must be done before encoding.</summary>
		/// <returns>If the method succeeds, the method returns an array of byte values that represent the encoded information.  
		///  If the method fails, it throws an exception.</returns>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		public byte[] Encode()
		{
			if (_encodedMessage == null)
			{
				throw new InvalidOperationException("The CMS message is not encrypted.");
			}
			return _encodedMessage.CloneByteArray();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decode(System.Byte[])" /> method decodes the specified enveloped CMS/PKCS #7 message and resets all member variables in the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> object.</summary>
		/// <param name="encodedMessage">An array of byte values that represent the information to be decoded.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		public void Decode(byte[] encodedMessage)
		{
			if (encodedMessage == null)
			{
				throw new ArgumentNullException("encodedMessage");
			}
			if (_decryptorPal != null)
			{
				_decryptorPal.Dispose();
				_decryptorPal = null;
			}
			_decryptorPal = PkcsPal.Instance.Decode(encodedMessage, out var version, out var contentInfo, out var contentEncryptionAlgorithm, out var originatorCerts, out var unprotectedAttributes);
			Version = version;
			ContentInfo = contentInfo;
			ContentEncryptionAlgorithm = contentEncryptionAlgorithm;
			Certificates = originatorCerts;
			UnprotectedAttributes = unprotectedAttributes;
			_encodedMessage = contentInfo.Content.CloneByteArray();
			_lastCall = LastCall.Decode;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt" /> method searches the current user and computer My stores for the appropriate certificate and private key.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		public void Decrypt()
		{
			DecryptContent(RecipientInfos, null);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo)" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the private key associated with the certificate identified by the specified recipient information.</summary>
		/// <param name="recipientInfo">A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object that represents the recipient information that identifies the certificate associated with the private key to use for the decryption.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		public void Decrypt(RecipientInfo recipientInfo)
		{
			if (recipientInfo == null)
			{
				throw new ArgumentNullException("recipientInfo");
			}
			DecryptContent(new RecipientInfoCollection(recipientInfo), null);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the private key associated with the certificate identified by the specified recipient information and by using the specified certificate collection.  The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches the specified certificate collection and the My certificate store for the proper certificate to use for the decryption.</summary>
		/// <param name="recipientInfo">A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object that represents the recipient information to use for the decryption.</param>
		/// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents additional certificates to use for the decryption. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches this certificate collection and the My certificate store for the proper certificate to use for the decryption.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		public void Decrypt(RecipientInfo recipientInfo, X509Certificate2Collection extraStore)
		{
			if (recipientInfo == null)
			{
				throw new ArgumentNullException("recipientInfo");
			}
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			DecryptContent(new RecipientInfoCollection(recipientInfo), extraStore);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the specified certificate collection. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches the specified certificate collection and the My certificate store for the proper certificate to use for the decryption.</summary>
		/// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents additional certificates to use for the decryption. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches this certificate collection and the My certificate store for the proper certificate to use for the decryption.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		public void Decrypt(X509Certificate2Collection extraStore)
		{
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			DecryptContent(RecipientInfos, extraStore);
		}

		private void DecryptContent(RecipientInfoCollection recipientInfos, X509Certificate2Collection extraStore)
		{
			switch (_lastCall)
			{
			case LastCall.Ctor:
				throw new InvalidOperationException("The CMS message is not encrypted.");
			case LastCall.Encrypt:
				throw PkcsPal.Instance.CreateDecryptAfterEncryptException();
			case LastCall.Decrypt:
				throw PkcsPal.Instance.CreateDecryptTwiceException();
			default:
				throw new InvalidOperationException();
			case LastCall.Decode:
			{
				extraStore = extraStore ?? new X509Certificate2Collection();
				X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
				PkcsPal.Instance.AddCertsFromStoreForDecryption(x509Certificate2Collection);
				x509Certificate2Collection.AddRange(extraStore);
				X509Certificate2Collection certificates = Certificates;
				ContentInfo contentInfo = null;
				Exception exception = PkcsPal.Instance.CreateRecipientsNotFoundException();
				RecipientInfoEnumerator enumerator = recipientInfos.GetEnumerator();
				while (enumerator.MoveNext())
				{
					RecipientInfo current = enumerator.Current;
					X509Certificate2 x509Certificate = x509Certificate2Collection.TryFindMatchingCertificate(current.RecipientIdentifier);
					if (x509Certificate == null)
					{
						exception = PkcsPal.Instance.CreateRecipientsNotFoundException();
						continue;
					}
					contentInfo = _decryptorPal.TryDecrypt(current, x509Certificate, certificates, extraStore, out exception);
					if (exception == null)
					{
						break;
					}
				}
				if (exception != null)
				{
					throw exception;
				}
				ContentInfo = contentInfo;
				_encodedMessage = contentInfo.Content.CloneByteArray();
				_lastCall = LastCall.Decrypt;
				break;
			}
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified subject identifier type and content information. The specified content information is to be used as the inner content type.</summary>
		/// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the means of identifying the recipient.</param>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the content and its type.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		public EnvelopedCms(SubjectIdentifierType recipientIdentifierType, ContentInfo contentInfo)
			: this(contentInfo)
		{
			if (recipientIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier)
			{
				Version = 2;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo,System.Security.Cryptography.Pkcs.AlgorithmIdentifier)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified subject identifier type, content information, and encryption algorithm. The specified content information is to be used as the inner content type.</summary>
		/// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the means of identifying the recipient.</param>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the content and its type.</param>
		/// <param name="encryptionAlgorithm">An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that specifies the encryption algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		public EnvelopedCms(SubjectIdentifierType recipientIdentifierType, ContentInfo contentInfo, AlgorithmIdentifier encryptionAlgorithm)
			: this(contentInfo, encryptionAlgorithm)
		{
			if (recipientIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier)
			{
				Version = 2;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt" /> method encrypts the contents of the CMS/PKCS #7 message.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public void Encrypt()
		{
			Encrypt(new CmsRecipientCollection());
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo" /> class defines key agreement recipient information. Key agreement algorithms typically use the Diffie-Hellman key agreement algorithm, in which the two parties that establish a shared cryptographic key both take part in its generation and, by definition, agree on that key. This is in contrast to key transport algorithms, in which one party generates the key unilaterally and sends, or transports it, to the other party.</summary>
	public sealed class KeyAgreeRecipientInfo : RecipientInfo
	{
		private volatile SubjectIdentifier _lazyRecipientIdentifier;

		private volatile AlgorithmIdentifier _lazyKeyEncryptionAlgorithm;

		private volatile byte[] _lazyEncryptedKey;

		private volatile SubjectIdentifierOrKey _lazyOriginatorIdentifierKey;

		private DateTime? _lazyDate;

		private volatile CryptographicAttributeObject _lazyOtherKeyAttribute;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.Version" /> property retrieves the version of the key agreement recipient. This is automatically set for  objects in this class, and the value  implies that the recipient is taking part in a key agreement algorithm.</summary>
		/// <returns>The version of the <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo" /> object.</returns>
		public override int Version => Pal.Version;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.RecipientIdentifier" /> property retrieves the identifier of the recipient.</summary>
		/// <returns>The identifier of the recipient.</returns>
		public override SubjectIdentifier RecipientIdentifier => _lazyRecipientIdentifier ?? (_lazyRecipientIdentifier = Pal.RecipientIdentifier);

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.KeyEncryptionAlgorithm" /> property retrieves the algorithm used to perform the key agreement.</summary>
		/// <returns>The value of the algorithm used to perform the key agreement.</returns>
		public override AlgorithmIdentifier KeyEncryptionAlgorithm => _lazyKeyEncryptionAlgorithm ?? (_lazyKeyEncryptionAlgorithm = Pal.KeyEncryptionAlgorithm);

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.EncryptedKey" /> property retrieves the encrypted recipient keying material.</summary>
		/// <returns>An array of byte values that contain the encrypted recipient keying material.</returns>
		public override byte[] EncryptedKey => _lazyEncryptedKey ?? (_lazyEncryptedKey = Pal.EncryptedKey);

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.OriginatorIdentifierOrKey" /> property retrieves information about the originator of the key agreement for key agreement algorithms that warrant it.</summary>
		/// <returns>An object that contains information about the originator of the key agreement.</returns>
		public SubjectIdentifierOrKey OriginatorIdentifierOrKey => _lazyOriginatorIdentifierKey ?? (_lazyOriginatorIdentifierKey = Pal.OriginatorIdentifierOrKey);

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.Date" /> property retrieves the date and time of the start of the key agreement protocol by the originator.</summary>
		/// <returns>The date and time of the start of the key agreement protocol by the originator.</returns>
		/// <exception cref="T:System.InvalidOperationException">The recipient identifier type is not a subject key identifier.</exception>
		public DateTime Date
		{
			get
			{
				if (!_lazyDate.HasValue)
				{
					_lazyDate = Pal.Date;
					Interlocked.MemoryBarrier();
				}
				return _lazyDate.Value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.OtherKeyAttribute" /> property retrieves attributes of the keying material.</summary>
		/// <returns>The attributes of the keying material.</returns>
		/// <exception cref="T:System.InvalidOperationException">The recipient identifier type is not a subject key identifier.</exception>
		public CryptographicAttributeObject OtherKeyAttribute => _lazyOtherKeyAttribute ?? (_lazyOtherKeyAttribute = Pal.OtherKeyAttribute);

		private new KeyAgreeRecipientInfoPal Pal => (KeyAgreeRecipientInfoPal)base.Pal;

		internal KeyAgreeRecipientInfo(KeyAgreeRecipientInfoPal pal)
			: base(RecipientInfoType.KeyAgreement, pal)
		{
		}

		internal KeyAgreeRecipientInfo()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo" /> class defines key transport recipient information.        Key transport algorithms typically use the RSA algorithm, in which  an originator establishes a shared cryptographic key with a recipient by generating that key and  then transporting it to the recipient. This is in contrast to key agreement algorithms, in which the two parties that will be using a cryptographic key both take part in its generation, thereby mutually agreeing to that key.</summary>
	public sealed class KeyTransRecipientInfo : RecipientInfo
	{
		private volatile SubjectIdentifier _lazyRecipientIdentifier;

		private volatile AlgorithmIdentifier _lazyKeyEncryptionAlgorithm;

		private volatile byte[] _lazyEncryptedKey;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.Version" /> property retrieves the version of the key transport recipient. The version of the key transport recipient is automatically set for  objects in this class, and the value  implies that the recipient is taking part in a key transport algorithm.</summary>
		/// <returns>An int value that represents the version of the key transport <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object.</returns>
		public override int Version => Pal.Version;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.RecipientIdentifier" /> property retrieves the subject identifier associated with the encrypted content.</summary>
		/// <returns>A   <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> object that  stores the identifier of the recipient taking part in the key transport.</returns>
		public override SubjectIdentifier RecipientIdentifier => _lazyRecipientIdentifier ?? (_lazyRecipientIdentifier = Pal.RecipientIdentifier);

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.KeyEncryptionAlgorithm" /> property retrieves the key encryption algorithm used to encrypt the content encryption key.</summary>
		/// <returns>An  <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that stores the key encryption algorithm identifier.</returns>
		public override AlgorithmIdentifier KeyEncryptionAlgorithm => _lazyKeyEncryptionAlgorithm ?? (_lazyKeyEncryptionAlgorithm = Pal.KeyEncryptionAlgorithm);

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.EncryptedKey" /> property retrieves the encrypted key for this key transport recipient.</summary>
		/// <returns>An array of byte values that represents the encrypted key.</returns>
		public override byte[] EncryptedKey => _lazyEncryptedKey ?? (_lazyEncryptedKey = Pal.EncryptedKey);

		private new KeyTransRecipientInfoPal Pal => (KeyTransRecipientInfoPal)base.Pal;

		internal KeyTransRecipientInfo(KeyTransRecipientInfoPal pal)
			: base(RecipientInfoType.KeyTransport, pal)
		{
		}

		internal KeyTransRecipientInfo()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Represents an attribute used for CMS/PKCS #7 and PKCS #9 operations.</summary>
	public class Pkcs9AttributeObject : AsnEncodedData
	{
		/// <summary>Gets an <see cref="T:System.Security.Cryptography.Oid" /> object that represents the type of attribute associated with this <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> object.</summary>
		/// <returns>An object that represents the type of attribute associated with this <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> object.</returns>
		public new Oid Oid => base.Oid;

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> class.</summary>
		public Pkcs9AttributeObject()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> class using a specified string representation of an object identifier (OID) as the attribute type and a specified ASN.1 encoded data as the attribute value.</summary>
		/// <param name="oid">The string representation of an OID that represents the PKCS #9 attribute type.</param>
		/// <param name="encodedData">An array of byte values that contains the PKCS #9 attribute value.</param>
		public Pkcs9AttributeObject(string oid, byte[] encodedData)
			: this(new AsnEncodedData(oid, encodedData))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> class using a specified <see cref="T:System.Security.Cryptography.Oid" /> object as the attribute type and a specified ASN.1 encoded data as the attribute value.</summary>
		/// <param name="oid">An object that represents the PKCS #9 attribute type.</param>
		/// <param name="encodedData">An array of byte values that represents the PKCS #9 attribute value.</param>
		public Pkcs9AttributeObject(Oid oid, byte[] encodedData)
			: this(new AsnEncodedData(oid, encodedData))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> class using a specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object as its attribute type and value.</summary>
		/// <param name="asnEncodedData">An object that contains the PKCS #9 attribute type and value to use.</param>
		/// <exception cref="T:System.ArgumentException">The length of the <paramref name="Value" /> member of the <paramref name="Oid" /> member of <paramref name="asnEncodedData" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Oid" /> member of <paramref name="asnEncodedData" /> is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="Value" /> member of the <paramref name="Oid" /> member of <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		public Pkcs9AttributeObject(AsnEncodedData asnEncodedData)
			: base(asnEncodedData)
		{
			if (asnEncodedData.Oid == null)
			{
				throw new ArgumentNullException("Oid");
			}
			if ((base.Oid.Value ?? throw new ArgumentNullException("oid.Value")).Length == 0)
			{
				throw new ArgumentException("String cannot be empty or null.", "oid.Value");
			}
		}

		internal Pkcs9AttributeObject(Oid oid)
		{
			base.Oid = oid;
		}

		/// <summary>Copies a PKCS #9 attribute type and value for this <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9AttributeObject" /> from the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">An object that contains the PKCS #9 attribute type and value to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asnEncodeData" /> does not represent a compatible attribute type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			if (!(asnEncodedData is Pkcs9AttributeObject))
			{
				throw new ArgumentException("The parameter should be a PKCS 9 attribute.");
			}
			base.CopyFrom(asnEncodedData);
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9ContentType" /> class defines the type of the content of a CMS/PKCS #7 message.</summary>
	public sealed class Pkcs9ContentType : Pkcs9AttributeObject
	{
		private volatile Oid _lazyContentType;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9ContentType.ContentType" /> property gets an <see cref="T:System.Security.Cryptography.Oid" /> object that contains the content type.</summary>
		/// <returns>An  <see cref="T:System.Security.Cryptography.Oid" /> object that contains the content type.</returns>
		public Oid ContentType => _lazyContentType ?? (_lazyContentType = Decode(base.RawData));

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9ContentType.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9ContentType" /> class.</summary>
		public Pkcs9ContentType()
			: base(Oid.FromOidValue("1.2.840.113549.1.9.3", OidGroup.ExtensionOrAttribute))
		{
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			_lazyContentType = null;
		}

		private static Oid Decode(byte[] rawData)
		{
			if (rawData == null)
			{
				return null;
			}
			return new Oid(PkcsPal.Instance.DecodeOid(rawData));
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class defines the description of the content of a CMS/PKCS #7 message.</summary>
	public sealed class Pkcs9DocumentDescription : Pkcs9AttributeObject
	{
		private volatile string _lazyDocumentDescription;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.DocumentDescription" /> property retrieves the document description.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the document description.</returns>
		public string DocumentDescription => _lazyDocumentDescription ?? (_lazyDocumentDescription = Decode(base.RawData));

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class.</summary>
		public Pkcs9DocumentDescription()
			: base(new Oid("1.3.6.1.4.1.311.88.2.2"))
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor(System.String)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class by using the specified description of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="documentDescription">An instance of the <see cref="T:System.String" /> class that specifies the description for the CMS/PKCS #7 message.</param>
		public Pkcs9DocumentDescription(string documentDescription)
			: base("1.3.6.1.4.1.311.88.2.2", Encode(documentDescription))
		{
			_lazyDocumentDescription = documentDescription;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor(System.Byte[])" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class by using the specified array of byte values as the encoded description of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="encodedDocumentDescription">An array of byte values that specifies the encoded description of the CMS/PKCS #7 message.</param>
		public Pkcs9DocumentDescription(byte[] encodedDocumentDescription)
			: base("1.3.6.1.4.1.311.88.2.2", encodedDocumentDescription)
		{
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			_lazyDocumentDescription = null;
		}

		private static string Decode(byte[] rawData)
		{
			if (rawData == null)
			{
				return null;
			}
			return PkcsPal.Instance.DecodeOctetString(rawData).OctetStringToUnicode();
		}

		private static byte[] Encode(string documentDescription)
		{
			if (documentDescription == null)
			{
				throw new ArgumentNullException("documentDescription");
			}
			byte[] octets = documentDescription.UnicodeToOctetString();
			return PkcsPal.Instance.EncodeOctetString(octets);
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class defines the name of a CMS/PKCS #7 message.</summary>
	public sealed class Pkcs9DocumentName : Pkcs9AttributeObject
	{
		private volatile string _lazyDocumentName;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.DocumentName" /> property retrieves the document name.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the document name.</returns>
		public string DocumentName => _lazyDocumentName ?? (_lazyDocumentName = Decode(base.RawData));

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class.</summary>
		public Pkcs9DocumentName()
			: base(new Oid("1.3.6.1.4.1.311.88.2.1"))
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor(System.String)" /> constructor creates an instance of the  <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class by using the specified name for the CMS/PKCS #7 message.</summary>
		/// <param name="documentName">A  <see cref="T:System.String" /> object that specifies the name for the CMS/PKCS #7 message.</param>
		public Pkcs9DocumentName(string documentName)
			: base("1.3.6.1.4.1.311.88.2.1", Encode(documentName))
		{
			_lazyDocumentName = documentName;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor(System.Byte[])" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class by using the specified array of byte values as the encoded name of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="encodedDocumentName">An array of byte values that specifies the encoded name of the CMS/PKCS #7 message.</param>
		public Pkcs9DocumentName(byte[] encodedDocumentName)
			: base("1.3.6.1.4.1.311.88.2.1", encodedDocumentName)
		{
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			_lazyDocumentName = null;
		}

		private static string Decode(byte[] rawData)
		{
			if (rawData == null)
			{
				return null;
			}
			return PkcsPal.Instance.DecodeOctetString(rawData).OctetStringToUnicode();
		}

		private static byte[] Encode(string documentName)
		{
			if (documentName == null)
			{
				throw new ArgumentNullException("documentName");
			}
			byte[] octets = documentName.UnicodeToOctetString();
			return PkcsPal.Instance.EncodeOctetString(octets);
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest" /> class defines the message digest of a CMS/PKCS #7 message.</summary>
	public sealed class Pkcs9MessageDigest : Pkcs9AttributeObject
	{
		private volatile byte[] _lazyMessageDigest;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest.MessageDigest" /> property retrieves the message digest.</summary>
		/// <returns>An array of byte values that contains the message digest.</returns>
		public byte[] MessageDigest => _lazyMessageDigest ?? (_lazyMessageDigest = Decode(base.RawData));

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest" /> class.</summary>
		public Pkcs9MessageDigest()
			: base(Oid.FromOidValue("1.2.840.113549.1.9.4", OidGroup.ExtensionOrAttribute))
		{
		}

		internal Pkcs9MessageDigest(ReadOnlySpan<byte> signatureDigest)
		{
			using AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER);
			asnWriter.WriteOctetString(signatureDigest);
			base.RawData = asnWriter.Encode();
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			_lazyMessageDigest = null;
		}

		private static byte[] Decode(byte[] rawData)
		{
			if (rawData == null)
			{
				return null;
			}
			return PkcsPal.Instance.DecodeOctetString(rawData);
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> class defines the signing date and time of a signature. A <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> object can  be used as an authenticated attribute of a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object when an authenticated date and time are to accompany a digital signature.</summary>
	public sealed class Pkcs9SigningTime : Pkcs9AttributeObject
	{
		private DateTime? _lazySigningTime;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.SigningTime" /> property retrieves a <see cref="T:System.DateTime" /> structure that represents the date and time that the message was signed.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> structure that contains the date and time the document was signed.</returns>
		public DateTime SigningTime
		{
			get
			{
				if (!_lazySigningTime.HasValue)
				{
					_lazySigningTime = Decode(base.RawData);
					Interlocked.MemoryBarrier();
				}
				return _lazySigningTime.Value;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> class.</summary>
		public Pkcs9SigningTime()
			: this(DateTime.Now)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.#ctor(System.DateTime)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> class by using the specified signing date and time.</summary>
		/// <param name="signingTime">A <see cref="T:System.DateTime" /> structure that represents the signing date and time of the signature.</param>
		public Pkcs9SigningTime(DateTime signingTime)
			: base("1.2.840.113549.1.9.5", Encode(signingTime))
		{
			_lazySigningTime = signingTime;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.#ctor(System.Byte[])" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> class by using the specified array of byte values as the encoded signing date and time of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="encodedSigningTime">An array of byte values that specifies the encoded signing date and time of the CMS/PKCS #7 message.</param>
		public Pkcs9SigningTime(byte[] encodedSigningTime)
			: base("1.2.840.113549.1.9.5", encodedSigningTime)
		{
		}

		/// <summary>Copies information from a <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			_lazySigningTime = null;
		}

		private static DateTime Decode(byte[] rawData)
		{
			if (rawData == null)
			{
				return default(DateTime);
			}
			return PkcsPal.Instance.DecodeUtcTime(rawData);
		}

		private static byte[] Encode(DateTime signingTime)
		{
			return PkcsPal.Instance.EncodeUtcTime(signingTime);
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.PublicKeyInfo" /> class represents information associated with a public key.</summary>
	public sealed class PublicKeyInfo
	{
		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.PublicKeyInfo.Algorithm" /> property retrieves the algorithm identifier associated with the public key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that represents the algorithm.</returns>
		public AlgorithmIdentifier Algorithm { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.PublicKeyInfo.KeyValue" /> property retrieves the value of the encoded public component of the public key pair.</summary>
		/// <returns>An array of byte values  that represents the encoded public component of the public key pair.</returns>
		public byte[] KeyValue { get; }

		internal PublicKeyInfo(AlgorithmIdentifier algorithm, byte[] keyValue)
		{
			Algorithm = algorithm;
			KeyValue = keyValue;
		}

		internal PublicKeyInfo()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> class represents information about a CMS/PKCS #7 message recipient. The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> class is an abstract class inherited by the <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo" /> and <see cref="T:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo" /> classes.</summary>
	public abstract class RecipientInfo
	{
		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.Type" /> property retrieves the type of the recipient. The type of the recipient determines which of two major protocols is used to establish a key between the originator and the recipient of a CMS/PKCS #7 message.</summary>
		/// <returns>A value of the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoType" /> enumeration that defines the type of the recipient.</returns>
		public RecipientInfoType Type { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.Version" /> abstract property retrieves the version of the recipient information. Derived classes automatically set this property for their objects, and the value indicates whether it is using PKCS #7 or Cryptographic Message Syntax (CMS) to protect messages. The version also implies whether the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object establishes a cryptographic key by a key agreement algorithm or a key transport algorithm.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that represents the version of the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object.</returns>
		public abstract int Version { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.RecipientIdentifier" /> abstract property retrieves the identifier of the recipient.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> object that contains the identifier of the recipient.</returns>
		public abstract SubjectIdentifier RecipientIdentifier { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.KeyEncryptionAlgorithm" /> abstract property retrieves the algorithm used to perform the key establishment.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that contains the value of the algorithm used to establish the key between the originator and recipient of the CMS/PKCS #7 message.</returns>
		public abstract AlgorithmIdentifier KeyEncryptionAlgorithm { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.EncryptedKey" /> abstract property retrieves the encrypted recipient keying material.</summary>
		/// <returns>An array of byte values that contain the encrypted recipient keying material.</returns>
		public abstract byte[] EncryptedKey { get; }

		internal RecipientInfoPal Pal { get; }

		internal RecipientInfo(RecipientInfoType type, RecipientInfoPal pal)
		{
			Type = type;
			Pal = pal;
		}

		internal RecipientInfo()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> class represents a collection of <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> objects. <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> implements the <see cref="T:System.Collections.ICollection" /> interface.</summary>
	public sealed class RecipientInfoCollection : ICollection, IEnumerable
	{
		private readonly RecipientInfo[] _recipientInfos;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.Item(System.Int32)" /> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object at the specified index in the collection.</summary>
		/// <param name="index">An int value that represents the index in the collection. The index is zero based.</param>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		public RecipientInfo this[int index]
		{
			get
			{
				if (index < 0 || index >= _recipientInfos.Length)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				return _recipientInfos[index];
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.Count" /> property retrieves the number of items in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>An int value that represents the number of items in the collection.</returns>
		public int Count => _recipientInfos.Length;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.IsSynchronized" /> property retrieves whether access to the collection is synchronized, or thread safe. This property always returns <see langword="false" />, which means the collection is not thread safe.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value of <see langword="false" />, which means the collection is not thread safe.</returns>
		public bool IsSynchronized => false;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoCollection.SyncRoot" /> property retrieves an <see cref="T:System.Object" /> object used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> object used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		public object SyncRoot => this;

		internal RecipientInfoCollection()
		{
			_recipientInfos = Array.Empty<RecipientInfo>();
		}

		internal RecipientInfoCollection(RecipientInfo recipientInfo)
		{
			_recipientInfos = new RecipientInfo[1] { recipientInfo };
		}

		internal RecipientInfoCollection(ICollection<RecipientInfo> recipientInfos)
		{
			_recipientInfos = new RecipientInfo[recipientInfos.Count];
			recipientInfos.CopyTo(_recipientInfos, 0);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		public RecipientInfoEnumerator GetEnumerator()
		{
			return new RecipientInfoEnumerator(this);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.System#Collections#IEnumerable#GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.CopyTo(System.Array,System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection to an array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object to which  the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (index > array.Length - Count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			for (int i = 0; i < Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoCollection.CopyTo(System.Security.Cryptography.Pkcs.RecipientInfo[],System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection to a <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> array.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> objects where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		public void CopyTo(RecipientInfo[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			_recipientInfos.CopyTo(array, index);
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> class provides enumeration functionality for the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection. <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> implements the <see cref="T:System.Collections.IEnumerator" /> interface.</summary>
	public sealed class RecipientInfoEnumerator : IEnumerator
	{
		private readonly RecipientInfoCollection _recipientInfos;

		private int _current;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object that represents the current recipient information structure in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		public RecipientInfo Current => _recipientInfos[_current];

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.System#Collections#IEnumerator#Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object that represents the current recipient information structure in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		object IEnumerator.Current => _recipientInfos[_current];

		internal RecipientInfoEnumerator(RecipientInfoCollection RecipientInfos)
		{
			_recipientInfos = RecipientInfos;
			_current = -1;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.MoveNext" /> method advances the enumeration to the next <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>This method returns a bool that specifies whether the enumeration successfully advanced. If the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object, the method returns <see langword="true" />. If the enumeration moved past the last item in the enumeration, it returns <see langword="false" />.</returns>
		public bool MoveNext()
		{
			if (_current >= _recipientInfos.Count - 1)
			{
				return false;
			}
			_current++;
			return true;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.Reset" /> method resets the enumeration to the first <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		public void Reset()
		{
			_current = -1;
		}

		internal RecipientInfoEnumerator()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoType" /> enumeration defines the types of recipient information.</summary>
	public enum RecipientInfoType
	{
		/// <summary>The recipient information type is unknown.</summary>
		Unknown,
		/// <summary>Key transport recipient information.</summary>
		KeyTransport,
		/// <summary>Key agreement recipient information.</summary>
		KeyAgreement
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> class enables signing and verifying of CMS/PKCS #7 messages.</summary>
	public sealed class SignedCms
	{
		private static readonly Oid s_cmsDataOid = Oid.FromOidValue("1.2.840.113549.1.7.1", OidGroup.ExtensionOrAttribute);

		private SignedDataAsn _signedData;

		private bool _hasData;

		private Memory<byte> _heldData;

		private ReadOnlyMemory<byte>? _heldContent;

		private bool _hasPkcs7Content;

		private string _contentType;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.Version" /> property retrieves the version of the CMS/PKCS #7 message.</summary>
		/// <returns>An int value that represents the CMS/PKCS #7 message version.</returns>
		public int Version { get; private set; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.ContentInfo" /> property retrieves the inner contents of the encoded CMS/PKCS #7 message.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the contents of the encoded CMS/PKCS #7 message.</returns>
		public ContentInfo ContentInfo { get; private set; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.Detached" /> property retrieves whether the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> object is for a detached signature.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> object is for a detached signature. If this property is <see langword="true" />, the signature is detached. If this property is <see langword="false" />, the signature is not detached.</returns>
		public bool Detached { get; private set; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.Certificates" /> property retrieves the certificates associated with the encoded CMS/PKCS #7 message.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents the set of certificates for the encoded CMS/PKCS #7 message.</returns>
		public X509Certificate2Collection Certificates
		{
			get
			{
				X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
				if (!_hasData)
				{
					return x509Certificate2Collection;
				}
				CertificateChoiceAsn[] certificateSet = _signedData.CertificateSet;
				if (certificateSet == null)
				{
					return x509Certificate2Collection;
				}
				CertificateChoiceAsn[] array = certificateSet;
				for (int i = 0; i < array.Length; i++)
				{
					CertificateChoiceAsn certificateChoiceAsn = array[i];
					x509Certificate2Collection.Add(new X509Certificate2(certificateChoiceAsn.Certificate.Value.ToArray()));
				}
				return x509Certificate2Collection;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.SignerInfos" /> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection associated with the CMS/PKCS #7 message.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> object that represents the signer information for the CMS/PKCS #7 message.</returns>
		public SignerInfoCollection SignerInfos
		{
			get
			{
				if (!_hasData)
				{
					return new SignerInfoCollection();
				}
				return new SignerInfoCollection(_signedData.SignerInfos, this);
			}
		}

		private static ContentInfo MakeEmptyContentInfo()
		{
			return new ContentInfo(new Oid(s_cmsDataOid), Array.Empty<byte>());
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> class.</summary>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		public SignedCms()
			: this(SubjectIdentifierType.IssuerAndSerialNumber, MakeEmptyContentInfo(), detached: false)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> class by using the specified subject identifier type as the default subject identifier type for signers.</summary>
		/// <param name="signerIdentifierType">A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> member that specifies the default subject identifier type for signers.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		public SignedCms(SubjectIdentifierType signerIdentifierType)
			: this(signerIdentifierType, MakeEmptyContentInfo(), detached: false)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> class by using the specified content information as the inner content.</summary>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that specifies the content information as the inner content of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> message.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		public SignedCms(ContentInfo contentInfo)
			: this(SubjectIdentifierType.IssuerAndSerialNumber, contentInfo, detached: false)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> class by using the specified subject identifier type as the default subject identifier type for signers and content information as the inner content.</summary>
		/// <param name="signerIdentifierType">A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> member that specifies the default subject identifier type for signers.</param>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that specifies the content information as the inner content of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> message.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		public SignedCms(SubjectIdentifierType signerIdentifierType, ContentInfo contentInfo)
			: this(signerIdentifierType, contentInfo, detached: false)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo,System.Boolean)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> class by using the specified content information as the inner content and by using the detached state.</summary>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that specifies the content information as the inner content of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> message.</param>
		/// <param name="detached">A <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> object is for a detached signature. If <paramref name="detached" /> is <see langword="true" />, the signature is detached. If <paramref name="detached" /> is <see langword="false" />, the signature is not detached.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		public SignedCms(ContentInfo contentInfo, bool detached)
			: this(SubjectIdentifierType.IssuerAndSerialNumber, contentInfo, detached)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo,System.Boolean)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> class by using the specified subject identifier type as the default subject identifier type for signers, the content information as the inner content, and by using the detached state.</summary>
		/// <param name="signerIdentifierType">A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> member that specifies the default subject identifier type for signers.</param>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that specifies the content information as the inner content of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> message.</param>
		/// <param name="detached">A <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> object is for a detached signature. If <paramref name="detached" /> is <see langword="true" />, the signature is detached. If detached is <see langword="false" />, the signature is not detached.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		public SignedCms(SubjectIdentifierType signerIdentifierType, ContentInfo contentInfo, bool detached)
		{
			if (contentInfo == null)
			{
				throw new ArgumentNullException("contentInfo");
			}
			if (contentInfo.Content == null)
			{
				throw new ArgumentNullException("contentInfo.Content");
			}
			ContentInfo = contentInfo;
			Detached = detached;
			Version = 0;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.Encode" /> method encodes the information in the object into a CMS/PKCS #7 message.</summary>
		/// <returns>An array of byte values that represents the encoded message. The encoded message can be decoded by the <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.Decode(System.Byte[])" /> method.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		public byte[] Encode()
		{
			if (!_hasData)
			{
				throw new InvalidOperationException("The CMS message is not signed.");
			}
			try
			{
				return Helpers.EncodeContentInfo(_signedData, "1.2.840.113549.1.7.2");
			}
			catch (CryptographicException)
			{
				if (Detached)
				{
					throw;
				}
				SignedDataAsn value = _signedData;
				value.EncapContentInfo.Content = null;
				using (AsnWriter asnWriter = AsnSerializer.Serialize(value, AsnEncodingRules.DER))
				{
					value = AsnSerializer.Deserialize<SignedDataAsn>(asnWriter.Encode(), AsnEncodingRules.BER);
				}
				value.EncapContentInfo.Content = _signedData.EncapContentInfo.Content;
				return Helpers.EncodeContentInfo(value, "1.2.840.113549.1.7.2", AsnEncodingRules.BER);
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.Decode(System.Byte[])" /> method decodes an encoded <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> message. Upon successful decoding, the decoded information can be retrieved from the properties of the <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> object.</summary>
		/// <param name="encodedMessage">Array of byte values that represents the encoded CMS/PKCS #7 message to be decoded.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public void Decode(byte[] encodedMessage)
		{
			if (encodedMessage == null)
			{
				throw new ArgumentNullException("encodedMessage");
			}
			Decode(new ReadOnlyMemory<byte>(encodedMessage));
		}

		internal void Decode(ReadOnlyMemory<byte> encodedMessage)
		{
			int bytesRead;
			ContentInfoAsn contentInfoAsn = AsnSerializer.Deserialize<ContentInfoAsn>(encodedMessage, AsnEncodingRules.BER, out bytesRead);
			if (contentInfoAsn.ContentType != "1.2.840.113549.1.7.2")
			{
				throw new CryptographicException("Invalid cryptographic message type.");
			}
			_heldData = contentInfoAsn.Content.ToArray();
			_signedData = AsnSerializer.Deserialize<SignedDataAsn>(_heldData, AsnEncodingRules.BER);
			_contentType = _signedData.EncapContentInfo.ContentType;
			_hasPkcs7Content = false;
			if (!Detached)
			{
				ReadOnlyMemory<byte>? content = _signedData.EncapContentInfo.Content;
				ReadOnlyMemory<byte> value;
				if (content.HasValue)
				{
					value = GetContent(content.Value, _contentType);
					_hasPkcs7Content = content.Value.Length == value.Length;
				}
				else
				{
					value = ReadOnlyMemory<byte>.Empty;
				}
				_heldContent = value;
				ContentInfo = new ContentInfo(new Oid(_contentType), value.ToArray());
			}
			else
			{
				_heldContent = ContentInfo.Content.CloneByteArray();
			}
			Version = _signedData.Version;
			_hasData = true;
		}

		internal static ReadOnlyMemory<byte> GetContent(ReadOnlyMemory<byte> wrappedContent, string contentType)
		{
			byte[] array = null;
			int bytesWritten = 0;
			try
			{
				AsnReader asnReader = new AsnReader(wrappedContent, AsnEncodingRules.BER);
				if (asnReader.TryGetPrimitiveOctetStringBytes(out var contents))
				{
					return contents;
				}
				array = ArrayPool<byte>.Shared.Rent(wrappedContent.Length);
				if (!asnReader.TryCopyOctetStringBytes(array, out bytesWritten))
				{
					throw new CryptographicException();
				}
				return array.AsSpan(0, bytesWritten).ToArray();
			}
			catch (Exception)
			{
				if (contentType == "1.2.840.113549.1.7.1")
				{
					throw;
				}
				return wrappedContent;
			}
			finally
			{
				if (array != null)
				{
					array.AsSpan(0, bytesWritten).Clear();
					ArrayPool<byte>.Shared.Return(array);
				}
			}
		}

		/// <summary>Creates a signature and adds the signature to the CMS/PKCS #7 message.</summary>
		/// <exception cref="T:System.InvalidOperationException">.NET Framework (all versions) and .NET Core 3.0 and later: The recipient certificate is not specified.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Core version 2.2 and earlier: No signer certificate was provided.</exception>
		public void ComputeSignature()
		{
			throw new PlatformNotSupportedException("No signer certificate was provided. This platform does not implement the certificate picker UI.");
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.ComputeSignature(System.Security.Cryptography.Pkcs.CmsSigner)" /> method creates a signature using the specified signer and adds the signature to the CMS/PKCS #7 message.</summary>
		/// <param name="signer">A <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object that represents the signer.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public void ComputeSignature(CmsSigner signer)
		{
			ComputeSignature(signer, silent: true);
		}

		/// <summary>Creates a signature using the specified signer and adds the signature to the CMS/PKCS #7 message.</summary>
		/// <param name="signer">A <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object that represents the signer.</param>
		/// <param name="silent">This parameter is not used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="signer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">.NET Framework only: A signing certificate is not specified.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Core only: A signing certificate is not specified.</exception>
		public void ComputeSignature(CmsSigner signer, bool silent)
		{
			if (signer == null)
			{
				throw new ArgumentNullException("signer");
			}
			if (ContentInfo.Content.Length == 0)
			{
				throw new CryptographicException("Cannot create CMS signature for empty content.");
			}
			ReadOnlyMemory<byte> data = _heldContent ?? ((ReadOnlyMemory<byte>)ContentInfo.Content);
			string text = _contentType ?? ContentInfo.ContentType.Value ?? "1.2.840.113549.1.7.1";
			X509Certificate2Collection chainCerts;
			SignerInfoAsn signerInfoAsn = signer.Sign(data, text, silent, out chainCerts);
			bool flag = false;
			if (!_hasData)
			{
				flag = true;
				_signedData = new SignedDataAsn
				{
					DigestAlgorithms = Array.Empty<AlgorithmIdentifierAsn>(),
					SignerInfos = Array.Empty<SignerInfoAsn>(),
					EncapContentInfo = new EncapsulatedContentInfoAsn
					{
						ContentType = text
					}
				};
				if (!Detached)
				{
					using AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER);
					asnWriter.WriteOctetString(data.Span);
					_signedData.EncapContentInfo.Content = asnWriter.Encode();
				}
				_hasData = true;
			}
			int num = _signedData.SignerInfos.Length;
			Array.Resize(ref _signedData.SignerInfos, num + 1);
			_signedData.SignerInfos[num] = signerInfoAsn;
			UpdateCertificatesFromAddition(chainCerts);
			ConsiderDigestAddition(signerInfoAsn.DigestAlgorithm);
			UpdateMetadata();
			if (flag)
			{
				Reencode();
			}
		}

		/// <summary>Removes the signature at the specified index of the <see cref="P:System.Security.Cryptography.Pkcs.SignedCms.SignerInfos" /> collection.</summary>
		/// <param name="index">The zero-based index of the signature to remove.</param>
		/// <exception cref="T:System.InvalidOperationException">A CMS/PKCS #7 message is not signed, and <paramref name="index" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than the signature count minus 1.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The signature could not be removed.  
		///  -or-  
		///  An internal cryptographic error occurred.</exception>
		public void RemoveSignature(int index)
		{
			if (!_hasData)
			{
				throw new InvalidOperationException("The CMS message is not signed.");
			}
			if (index < 0 || index >= _signedData.SignerInfos.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			AlgorithmIdentifierAsn digestAlgorithm = _signedData.SignerInfos[index].DigestAlgorithm;
			Helpers.RemoveAt(ref _signedData.SignerInfos, index);
			ConsiderDigestRemoval(digestAlgorithm);
			UpdateMetadata();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.RemoveSignature(System.Security.Cryptography.Pkcs.SignerInfo)" /> method removes the signature for the specified <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object.</summary>
		/// <param name="signerInfo">A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object that represents the countersignature being removed.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public void RemoveSignature(SignerInfo signerInfo)
		{
			if (signerInfo == null)
			{
				throw new ArgumentNullException("signerInfo");
			}
			int num = SignerInfos.FindIndexForSigner(signerInfo);
			if (num < 0)
			{
				throw new CryptographicException("Cannot find the original signer.");
			}
			RemoveSignature(num);
		}

		internal ReadOnlySpan<byte> GetHashableContentSpan()
		{
			ReadOnlyMemory<byte> value = _heldContent.Value;
			if (!_hasPkcs7Content)
			{
				return value.Span;
			}
			return new AsnReader(value, AsnEncodingRules.BER).PeekContentBytes().Span;
		}

		internal void Reencode()
		{
			ContentInfo contentInfo = ContentInfo;
			try
			{
				byte[] encodedMessage = Encode();
				if (Detached)
				{
					_heldContent = null;
				}
				Decode(encodedMessage);
			}
			finally
			{
				ContentInfo = contentInfo;
			}
		}

		private void UpdateMetadata()
		{
			int version = 1;
			if ((_contentType ?? ContentInfo.ContentType.Value) != "1.2.840.113549.1.7.1")
			{
				version = 3;
			}
			else if (_signedData.SignerInfos.Any((SignerInfoAsn si) => si.Version == 3))
			{
				version = 3;
			}
			Version = version;
			_signedData.Version = version;
		}

		private void ConsiderDigestAddition(AlgorithmIdentifierAsn candidate)
		{
			int num = _signedData.DigestAlgorithms.Length;
			for (int i = 0; i < num; i++)
			{
				if (candidate.Equals(ref _signedData.DigestAlgorithms[i]))
				{
					return;
				}
			}
			Array.Resize(ref _signedData.DigestAlgorithms, num + 1);
			_signedData.DigestAlgorithms[num] = candidate;
		}

		private void ConsiderDigestRemoval(AlgorithmIdentifierAsn candidate)
		{
			bool flag = true;
			for (int i = 0; i < _signedData.SignerInfos.Length; i++)
			{
				if (candidate.Equals(ref _signedData.SignerInfos[i].DigestAlgorithm))
				{
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				return;
			}
			for (int j = 0; j < _signedData.DigestAlgorithms.Length; j++)
			{
				if (candidate.Equals(ref _signedData.DigestAlgorithms[j]))
				{
					Helpers.RemoveAt(ref _signedData.DigestAlgorithms, j);
					break;
				}
			}
		}

		internal void UpdateCertificatesFromAddition(X509Certificate2Collection newCerts)
		{
			if (newCerts.Count == 0)
			{
				return;
			}
			CertificateChoiceAsn[] certificateSet = _signedData.CertificateSet;
			int num = ((certificateSet != null) ? certificateSet.Length : 0);
			if (num > 0 || newCerts.Count > 1)
			{
				HashSet<X509Certificate2> hashSet = new HashSet<X509Certificate2>(Certificates.OfType<X509Certificate2>());
				for (int i = 0; i < newCerts.Count; i++)
				{
					X509Certificate2 item = newCerts[i];
					if (!hashSet.Add(item))
					{
						newCerts.RemoveAt(i);
						i--;
					}
				}
			}
			if (newCerts.Count != 0)
			{
				if (_signedData.CertificateSet == null)
				{
					_signedData.CertificateSet = new CertificateChoiceAsn[newCerts.Count];
				}
				else
				{
					Array.Resize(ref _signedData.CertificateSet, num + newCerts.Count);
				}
				for (int j = num; j < _signedData.CertificateSet.Length; j++)
				{
					_signedData.CertificateSet[j] = new CertificateChoiceAsn
					{
						Certificate = newCerts[j - num].RawData
					};
				}
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Boolean)" /> method verifies the digital signatures on the signed CMS/PKCS #7 message and, optionally, validates the signers' certificates.</summary>
		/// <param name="verifySignatureOnly">A <see cref="T:System.Boolean" /> value that specifies whether only the digital signatures are verified without the signers' certificates being validated.  
		///  If <paramref name="verifySignatureOnly" /> is <see langword="true" />, only the digital signatures are verified. If it is <see langword="false" />, the digital signatures are verified, the signers' certificates are validated, and the purposes of the certificates are validated. The purposes of a certificate are considered valid if the certificate has no key usage or if the key usage supports digital signatures or nonrepudiation.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		public void CheckSignature(bool verifySignatureOnly)
		{
			CheckSignature(new X509Certificate2Collection(), verifySignatureOnly);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" /> method verifies the digital signatures on the signed CMS/PKCS #7 message by using the specified collection of certificates and, optionally, validates the signers' certificates.</summary>
		/// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object that can be used to validate the certificate chain. If no additional certificates are to be used to validate the certificate chain, use <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Boolean)" /> instead of <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" />.</param>
		/// <param name="verifySignatureOnly">A <see cref="T:System.Boolean" /> value that specifies whether only the digital signatures are verified without the signers' certificates being validated.  
		///  If <paramref name="verifySignatureOnly" /> is <see langword="true" />, only the digital signatures are verified. If it is <see langword="false" />, the digital signatures are verified, the signers' certificates are validated, and the purposes of the certificates are validated. The purposes of a certificate are considered valid if the certificate has no key usage or if the key usage supports digital signatures or nonrepudiation.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		public void CheckSignature(X509Certificate2Collection extraStore, bool verifySignatureOnly)
		{
			if (!_hasData)
			{
				throw new InvalidOperationException("The CMS message is not signed.");
			}
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			CheckSignatures(SignerInfos, extraStore, verifySignatureOnly);
		}

		private static void CheckSignatures(SignerInfoCollection signers, X509Certificate2Collection extraStore, bool verifySignatureOnly)
		{
			if (signers.Count < 1)
			{
				throw new CryptographicException("The signed cryptographic message does not have a signer for the specified signer index.");
			}
			SignerInfoEnumerator enumerator = signers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				SignerInfo current = enumerator.Current;
				current.CheckSignature(extraStore, verifySignatureOnly);
				SignerInfoCollection counterSignerInfos = current.CounterSignerInfos;
				if (counterSignerInfos.Count > 0)
				{
					CheckSignatures(counterSignerInfos, extraStore, verifySignatureOnly);
				}
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckHash" /> method verifies the data integrity of the CMS/PKCS #7 message. <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckHash" /> is a specialized method used in specific security infrastructure applications that only wish to check the hash of the CMS message, rather than perform a full digital signature verification. <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckHash" /> does not authenticate the author nor sender of the message because this method does not involve verifying a digital signature. For general-purpose checking of the integrity and authenticity of a CMS/PKCS #7 message, use the <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Boolean)" /> or <see cref="M:System.Security.Cryptography.Pkcs.SignedCms.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" /> methods.</summary>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		public void CheckHash()
		{
			if (!_hasData)
			{
				throw new InvalidOperationException("The CMS message is not signed.");
			}
			SignerInfoCollection signerInfos = SignerInfos;
			if (signerInfos.Count < 1)
			{
				throw new CryptographicException("The signed cryptographic message does not have a signer for the specified signer index.");
			}
			SignerInfoEnumerator enumerator = signerInfos.GetEnumerator();
			while (enumerator.MoveNext())
			{
				SignerInfo current = enumerator.Current;
				if (current.SignerIdentifier.Type == SubjectIdentifierType.NoSignature)
				{
					current.CheckHash();
				}
			}
		}

		internal ref SignedDataAsn GetRawData()
		{
			return ref _signedData;
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> class represents a signer associated with a <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> object that represents a CMS/PKCS #7 message.</summary>
	public sealed class SignerInfo
	{
		private readonly Oid _digestAlgorithm;

		private readonly AttributeAsn[] _signedAttributes;

		private readonly ReadOnlyMemory<byte>? _signedAttributesMemory;

		private readonly Oid _signatureAlgorithm;

		private readonly ReadOnlyMemory<byte>? _signatureAlgorithmParameters;

		private readonly ReadOnlyMemory<byte> _signature;

		private readonly AttributeAsn[] _unsignedAttributes;

		private readonly SignedCms _document;

		private X509Certificate2 _signerCertificate;

		private SignerInfo _parentSignerInfo;

		private CryptographicAttributeObjectCollection _parsedSignedAttrs;

		private CryptographicAttributeObjectCollection _parsedUnsignedAttrs;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.Version" /> property retrieves the signer information version.</summary>
		/// <returns>An int value that specifies the signer information version.</returns>
		public int Version { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.SignerIdentifier" /> property retrieves the certificate identifier of the signer associated with the signer information.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> object that uniquely identifies the certificate associated with the signer information.</returns>
		public SubjectIdentifier SignerIdentifier { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.SignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection of signed attributes that is associated with the signer information. Signed attributes are signed along with the rest of the message content.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the signed attributes. If there are no signed attributes, the property is an empty collection.</returns>
		public CryptographicAttributeObjectCollection SignedAttributes
		{
			get
			{
				if (_parsedSignedAttrs == null)
				{
					_parsedSignedAttrs = MakeAttributeCollection(_signedAttributes);
				}
				return _parsedSignedAttrs;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.UnsignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection of unsigned attributes that is associated with the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> content. Unsigned attributes can be modified without invalidating the signature.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the unsigned attributes. If there are no unsigned attributes, the property is an empty collection.</returns>
		public CryptographicAttributeObjectCollection UnsignedAttributes
		{
			get
			{
				if (_parsedUnsignedAttrs == null)
				{
					_parsedUnsignedAttrs = MakeAttributeCollection(_unsignedAttributes);
				}
				return _parsedUnsignedAttrs;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.Certificate" /> property retrieves the signing certificate associated with the signer information.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</returns>
		public X509Certificate2 Certificate
		{
			get
			{
				if (_signerCertificate == null)
				{
					_signerCertificate = FindSignerCertificate();
				}
				return _signerCertificate;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.CounterSignerInfos" /> property retrieves the set of counter signers associated with the signer information.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection that represents the counter signers for the signer information. If there are no counter signers, the property is an empty collection.</returns>
		public SignerInfoCollection CounterSignerInfos
		{
			get
			{
				if (_parentSignerInfo != null || _unsignedAttributes == null || _unsignedAttributes.Length == 0)
				{
					return new SignerInfoCollection();
				}
				return GetCounterSigners(_unsignedAttributes);
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.DigestAlgorithm" /> property retrieves the <see cref="T:System.Security.Cryptography.Oid" /> object that represents the hash algorithm used in the computation of the signatures.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the hash algorithm used with the signature.</returns>
		public Oid DigestAlgorithm => new Oid(_digestAlgorithm);

		/// <summary>Gets the identifier for the signature algorithm used by the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object.</summary>
		/// <returns>The identifier for the signature algorithm used by the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object.</returns>
		public Oid SignatureAlgorithm => new Oid(_signatureAlgorithm);

		internal SignerInfo(ref SignerInfoAsn parsedData, SignedCms ownerDocument)
		{
			Version = parsedData.Version;
			SignerIdentifier = new SubjectIdentifier(parsedData.Sid);
			_digestAlgorithm = parsedData.DigestAlgorithm.Algorithm;
			_signedAttributesMemory = parsedData.SignedAttributes;
			_signatureAlgorithm = parsedData.SignatureAlgorithm.Algorithm;
			_signatureAlgorithmParameters = parsedData.SignatureAlgorithm.Parameters;
			_signature = parsedData.SignatureValue;
			_unsignedAttributes = parsedData.UnsignedAttributes;
			if (_signedAttributesMemory.HasValue)
			{
				_signedAttributes = AsnSerializer.Deserialize<SignedAttributesSet>(_signedAttributesMemory.Value, AsnEncodingRules.BER).SignedAttributes;
			}
			_document = ownerDocument;
		}

		internal ReadOnlyMemory<byte> GetSignatureMemory()
		{
			return _signature;
		}

		/// <summary>Retrieves the signature for the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object.</summary>
		/// <returns>The signature for the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object.</returns>
		public byte[] GetSignature()
		{
			return _signature.ToArray();
		}

		private SignerInfoCollection GetCounterSigners(AttributeAsn[] unsignedAttrs)
		{
			List<SignerInfo> list = new List<SignerInfo>();
			for (int i = 0; i < unsignedAttrs.Length; i++)
			{
				AttributeAsn attributeAsn = unsignedAttrs[i];
				if (attributeAsn.AttrType.Value == "1.2.840.113549.1.9.6")
				{
					AsnReader asnReader = new AsnReader(attributeAsn.AttrValues, AsnEncodingRules.BER);
					AsnReader asnReader2 = asnReader.ReadSetOf();
					if (asnReader.HasData)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					while (asnReader2.HasData)
					{
						SignerInfoAsn parsedData = AsnSerializer.Deserialize<SignerInfoAsn>(asnReader2.GetEncodedValue(), AsnEncodingRules.BER);
						SignerInfo item = new SignerInfo(ref parsedData, _document)
						{
							_parentSignerInfo = this
						};
						list.Add(item);
					}
				}
			}
			return new SignerInfoCollection(list.ToArray());
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.ComputeCounterSignature" /> method prompts the user to select a signing certificate, creates a countersignature, and adds the signature to the CMS/PKCS #7 message. Countersignatures are restricted to one level.</summary>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public void ComputeCounterSignature()
		{
			throw new PlatformNotSupportedException("No signer certificate was provided. This platform does not implement the certificate picker UI.");
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.ComputeCounterSignature(System.Security.Cryptography.Pkcs.CmsSigner)" /> method creates a countersignature by using the specified signer and adds the signature to the CMS/PKCS #7 message. Countersignatures are restricted to one level.</summary>
		/// <param name="signer">A <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object that represents the counter signer.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public void ComputeCounterSignature(CmsSigner signer)
		{
			if (_parentSignerInfo != null)
			{
				throw new CryptographicException("Only one level of counter-signatures are supported on this platform.");
			}
			if (signer == null)
			{
				throw new ArgumentNullException("signer");
			}
			signer.CheckCertificateValue();
			int num = _document.SignerInfos.FindIndexForSigner(this);
			if (num < 0)
			{
				throw new CryptographicException("Cannot find the original signer.");
			}
			SignerInfo signerInfo = _document.SignerInfos[num];
			X509Certificate2Collection chainCerts;
			SignerInfoAsn value = signer.Sign(signerInfo._signature, null, silent: false, out chainCerts);
			AttributeAsn attributeAsn;
			using (AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER))
			{
				asnWriter.PushSetOf();
				AsnSerializer.Serialize(value, asnWriter);
				asnWriter.PopSetOf();
				attributeAsn = new AttributeAsn
				{
					AttrType = new Oid("1.2.840.113549.1.9.6", "1.2.840.113549.1.9.6"),
					AttrValues = asnWriter.Encode()
				};
			}
			ref SignerInfoAsn reference = ref _document.GetRawData().SignerInfos[num];
			int num2;
			if (reference.UnsignedAttributes == null)
			{
				reference.UnsignedAttributes = new AttributeAsn[1];
				num2 = 0;
			}
			else
			{
				num2 = reference.UnsignedAttributes.Length;
				Array.Resize(ref reference.UnsignedAttributes, num2 + 1);
			}
			reference.UnsignedAttributes[num2] = attributeAsn;
			_document.UpdateCertificatesFromAddition(chainCerts);
			_document.Reencode();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.RemoveCounterSignature(System.Int32)" /> method removes the countersignature at the specified index of the <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.CounterSignerInfos" /> collection.</summary>
		/// <param name="index">The zero-based index of the countersignature to remove.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public void RemoveCounterSignature(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("childIndex");
			}
			int num = _document.SignerInfos.FindIndexForSigner(this);
			if (num < 0)
			{
				throw new CryptographicException("Cannot find the original signer.");
			}
			ref SignerInfoAsn reference = ref _document.GetRawData().SignerInfos[num];
			if (reference.UnsignedAttributes == null)
			{
				throw new CryptographicException("The signed cryptographic message does not have a signer for the specified signer index.");
			}
			int num2 = -1;
			int num3 = -1;
			bool flag = false;
			int num4 = 0;
			AttributeAsn[] unsignedAttributes = reference.UnsignedAttributes;
			for (int i = 0; i < unsignedAttributes.Length; i++)
			{
				AttributeAsn attributeAsn = unsignedAttributes[i];
				if (!(attributeAsn.AttrType.Value == "1.2.840.113549.1.9.6"))
				{
					continue;
				}
				AsnReader asnReader = new AsnReader(attributeAsn.AttrValues, AsnEncodingRules.BER);
				AsnReader asnReader2 = asnReader.ReadSetOf();
				if (asnReader.HasData)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				int num5 = 0;
				while (asnReader2.HasData)
				{
					asnReader2.GetEncodedValue();
					if (num4 == index)
					{
						num2 = i;
						num3 = num5;
					}
					num4++;
					num5++;
				}
				if (num3 == 0 && num5 == 1)
				{
					flag = true;
				}
				if (num2 >= 0)
				{
					break;
				}
			}
			if (num2 < 0)
			{
				throw new CryptographicException("The signed cryptographic message does not have a signer for the specified signer index.");
			}
			if (flag)
			{
				if (unsignedAttributes.Length == 1)
				{
					reference.UnsignedAttributes = null;
				}
				else
				{
					Helpers.RemoveAt(ref reference.UnsignedAttributes, num2);
				}
				return;
			}
			ref AttributeAsn reference2 = ref unsignedAttributes[num2];
			using AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.BER);
			asnWriter.PushSetOf();
			AsnReader asnReader3 = new AsnReader(reference2.AttrValues, asnWriter.RuleSet);
			AsnReader asnReader4 = asnReader3.ReadSetOf();
			asnReader3.ThrowIfNotEmpty();
			int num6 = 0;
			while (asnReader4.HasData)
			{
				ReadOnlyMemory<byte> encodedValue = asnReader4.GetEncodedValue();
				if (num6 != num3)
				{
					asnWriter.WriteEncodedValue(encodedValue);
				}
				num6++;
			}
			asnWriter.PopSetOf();
			reference2.AttrValues = asnWriter.Encode();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.RemoveCounterSignature(System.Security.Cryptography.Pkcs.SignerInfo)" /> method removes the countersignature for the specified <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object.</summary>
		/// <param name="counterSignerInfo">A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object that represents the countersignature being removed.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public void RemoveCounterSignature(SignerInfo counterSignerInfo)
		{
			if (counterSignerInfo == null)
			{
				throw new ArgumentNullException("counterSignerInfo");
			}
			SignerInfoCollection signerInfos = _document.SignerInfos;
			int num = signerInfos.FindIndexForSigner(this);
			if (num < 0)
			{
				throw new CryptographicException("Cannot find the original signer.");
			}
			num = signerInfos[num].CounterSignerInfos.FindIndexForSigner(counterSignerInfo);
			if (num < 0)
			{
				throw new CryptographicException("Cannot find the original signer.");
			}
			RemoveCounterSignature(num);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)" /> method verifies the digital signature of the message and, optionally, validates the certificate.</summary>
		/// <param name="verifySignatureOnly">A bool value that specifies whether only the digital signature is verified. If <paramref name="verifySignatureOnly" /> is <see langword="true" />, only the signature is verified. If <paramref name="verifySignatureOnly" /> is <see langword="false" />, the digital signature is verified, the certificate chain is validated, and the purposes of the certificates are validated. The purposes of the certificate are considered valid if the certificate has no key usage or if the key usage supports digital signature or nonrepudiation.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		public void CheckSignature(bool verifySignatureOnly)
		{
			CheckSignature(new X509Certificate2Collection(), verifySignatureOnly);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" /> method verifies the digital signature of the message by using the specified collection of certificates and, optionally, validates the certificate.</summary>
		/// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object that can be used to validate the chain. If no additional certificates are to be used to validate the chain, use <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)" /> instead of <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" />.</param>
		/// <param name="verifySignatureOnly">A bool value that specifies whether only the digital signature is verified. If <paramref name="verifySignatureOnly" /> is <see langword="true" />, only the signature is verified. If <paramref name="verifySignatureOnly" /> is <see langword="false" />, the digital signature is verified, the certificate chain is validated, and the purposes of the certificates are validated. The purposes of the certificate are considered valid if the certificate has no key usage or if the key usage supports digital signature or nonrepudiation.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		public void CheckSignature(X509Certificate2Collection extraStore, bool verifySignatureOnly)
		{
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			X509Certificate2 x509Certificate = Certificate;
			if (x509Certificate == null)
			{
				x509Certificate = FindSignerCertificate(SignerIdentifier, extraStore);
				if (x509Certificate == null)
				{
					throw new CryptographicException("Cannot find the original signer.");
				}
			}
			Verify(extraStore, x509Certificate, verifySignatureOnly);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash" /> method verifies the data integrity of the CMS/PKCS #7 message signer information. <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash" /> is a specialized method used in specific security infrastructure applications in which the subject uses the HashOnly member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration when setting up a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object. <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash" /> does not authenticate the signer information because this method does not involve verifying a digital signature. For general-purpose checking of the integrity and authenticity of CMS/PKCS #7 message signer information and countersignatures, use the <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)" /> or <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" /> methods.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		public void CheckHash()
		{
			if (!CheckHash(compatMode: false) && !CheckHash(compatMode: true))
			{
				throw new CryptographicException("Invalid signature.");
			}
		}

		private bool CheckHash(bool compatMode)
		{
			using IncrementalHash incrementalHash = PrepareDigest(compatMode);
			if (incrementalHash == null)
			{
				return false;
			}
			byte[] hashAndReset = incrementalHash.GetHashAndReset();
			return _signature.Span.SequenceEqual(hashAndReset);
		}

		private X509Certificate2 FindSignerCertificate()
		{
			return FindSignerCertificate(SignerIdentifier, _document.Certificates);
		}

		private static X509Certificate2 FindSignerCertificate(SubjectIdentifier signerIdentifier, X509Certificate2Collection extraStore)
		{
			if (extraStore == null || extraStore.Count == 0)
			{
				return null;
			}
			X509Certificate2Collection x509Certificate2Collection = null;
			X509Certificate2 x509Certificate = null;
			switch (signerIdentifier.Type)
			{
			case SubjectIdentifierType.IssuerAndSerialNumber:
			{
				X509IssuerSerial x509IssuerSerial = (X509IssuerSerial)signerIdentifier.Value;
				x509Certificate2Collection = extraStore.Find(X509FindType.FindBySerialNumber, x509IssuerSerial.SerialNumber, validOnly: false);
				X509Certificate2Enumerator enumerator = x509Certificate2Collection.GetEnumerator();
				while (enumerator.MoveNext())
				{
					X509Certificate2 current = enumerator.Current;
					if (current.IssuerName.Name == x509IssuerSerial.IssuerName)
					{
						x509Certificate = current;
						break;
					}
				}
				break;
			}
			case SubjectIdentifierType.SubjectKeyIdentifier:
				x509Certificate2Collection = extraStore.Find(X509FindType.FindBySubjectKeyIdentifier, signerIdentifier.Value, validOnly: false);
				if (x509Certificate2Collection.Count > 0)
				{
					x509Certificate = x509Certificate2Collection[0];
				}
				break;
			}
			if (x509Certificate2Collection != null)
			{
				X509Certificate2Enumerator enumerator = x509Certificate2Collection.GetEnumerator();
				while (enumerator.MoveNext())
				{
					X509Certificate2 current2 = enumerator.Current;
					if (current2 != x509Certificate)
					{
						current2.Dispose();
					}
				}
			}
			return x509Certificate;
		}

		private IncrementalHash PrepareDigest(bool compatMode)
		{
			IncrementalHash incrementalHash = IncrementalHash.CreateHash(GetDigestAlgorithm());
			if (_parentSignerInfo == null)
			{
				if (_document.Detached)
				{
					ref SignedDataAsn rawData = ref _document.GetRawData();
					ReadOnlyMemory<byte>? content = rawData.EncapContentInfo.Content;
					if (content.HasValue)
					{
						Helpers.AppendData(incrementalHash, SignedCms.GetContent(content.Value, rawData.EncapContentInfo.ContentType).Span);
					}
				}
				Helpers.AppendData(incrementalHash, _document.GetHashableContentSpan());
			}
			else
			{
				Helpers.AppendData(incrementalHash, _parentSignerInfo._signature.Span);
			}
			bool flag = _parentSignerInfo != null || _signedAttributes != null;
			if (_signedAttributes != null)
			{
				byte[] hashAndReset = incrementalHash.GetHashAndReset();
				using AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER);
				if (compatMode)
				{
					asnWriter.PushSequence();
				}
				else
				{
					asnWriter.PushSetOf();
				}
				AttributeAsn[] signedAttributes = _signedAttributes;
				for (int i = 0; i < signedAttributes.Length; i++)
				{
					AttributeAsn attributeAsn = signedAttributes[i];
					AsnSerializer.Serialize(attributeAsn, asnWriter);
					if (attributeAsn.AttrType.Value == "1.2.840.113549.1.9.4")
					{
						CryptographicAttributeObject cryptographicAttributeObject = MakeAttribute(attributeAsn);
						if (cryptographicAttributeObject.Values.Count != 1)
						{
							throw new CryptographicException("The hash value is not correct.");
						}
						Pkcs9MessageDigest pkcs9MessageDigest = (Pkcs9MessageDigest)cryptographicAttributeObject.Values[0];
						if (!hashAndReset.AsSpan().SequenceEqual(pkcs9MessageDigest.MessageDigest))
						{
							throw new CryptographicException("The hash value is not correct.");
						}
						flag = false;
					}
				}
				if (compatMode)
				{
					asnWriter.PopSequence();
					byte[] array = asnWriter.Encode();
					array[0] = 49;
					incrementalHash.AppendData(array);
				}
				else
				{
					asnWriter.PopSetOf();
					incrementalHash.AppendData(asnWriter.Encode());
				}
			}
			else if (compatMode)
			{
				return null;
			}
			if (flag)
			{
				throw new CryptographicException("The cryptographic message does not contain an expected authenticated attribute.");
			}
			return incrementalHash;
		}

		private void Verify(X509Certificate2Collection extraStore, X509Certificate2 certificate, bool verifySignatureOnly)
		{
			CmsSignature cmsSignature = CmsSignature.Resolve(SignatureAlgorithm.Value);
			if (cmsSignature == null)
			{
				throw new CryptographicException("Unknown algorithm '{0}'.", SignatureAlgorithm.Value);
			}
			if (!VerifySignature(cmsSignature, certificate, compatMode: false) && !VerifySignature(cmsSignature, certificate, compatMode: true))
			{
				throw new CryptographicException("Invalid signature.");
			}
			if (verifySignatureOnly)
			{
				return;
			}
			X509Chain x509Chain = new X509Chain();
			x509Chain.ChainPolicy.ExtraStore.AddRange(extraStore);
			x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
			x509Chain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot;
			if (!x509Chain.Build(certificate))
			{
				throw new CryptographicException("Certificate trust could not be established. The first reported error is: {0}", x509Chain.ChainStatus.FirstOrDefault().StatusInformation);
			}
			X509ExtensionEnumerator enumerator = certificate.Extensions.GetEnumerator();
			while (enumerator.MoveNext())
			{
				X509Extension current = enumerator.Current;
				if (current.Oid.Value == "2.5.29.15")
				{
					X509KeyUsageExtension x509KeyUsageExtension = current as X509KeyUsageExtension;
					if (x509KeyUsageExtension == null)
					{
						x509KeyUsageExtension = new X509KeyUsageExtension();
						x509KeyUsageExtension.CopyFrom(current);
					}
					if ((x509KeyUsageExtension.KeyUsages & (X509KeyUsageFlags.NonRepudiation | X509KeyUsageFlags.DigitalSignature)) == 0)
					{
						throw new CryptographicException("The certificate is not valid for the requested usage.");
					}
				}
			}
		}

		private bool VerifySignature(CmsSignature signatureProcessor, X509Certificate2 certificate, bool compatMode)
		{
			using IncrementalHash incrementalHash = PrepareDigest(compatMode);
			if (incrementalHash == null)
			{
				return false;
			}
			byte[] hashAndReset = incrementalHash.GetHashAndReset();
			byte[] signature = _signature.ToArray();
			return signatureProcessor.VerifySignature(hashAndReset, signature, DigestAlgorithm.Value, incrementalHash.AlgorithmName, _signatureAlgorithmParameters, certificate);
		}

		private HashAlgorithmName GetDigestAlgorithm()
		{
			return Helpers.GetDigestAlgorithm(DigestAlgorithm.Value);
		}

		internal static CryptographicAttributeObjectCollection MakeAttributeCollection(AttributeAsn[] attributes)
		{
			CryptographicAttributeObjectCollection cryptographicAttributeObjectCollection = new CryptographicAttributeObjectCollection();
			if (attributes == null)
			{
				return cryptographicAttributeObjectCollection;
			}
			foreach (AttributeAsn attribute in attributes)
			{
				cryptographicAttributeObjectCollection.AddWithoutMerge(MakeAttribute(attribute));
			}
			return cryptographicAttributeObjectCollection;
		}

		private static CryptographicAttributeObject MakeAttribute(AttributeAsn attribute)
		{
			Oid oid = new Oid(attribute.AttrType);
			AsnReader asnReader = new AsnReader(attribute.AttrValues, AsnEncodingRules.BER);
			AsnReader asnReader2 = asnReader.ReadSetOf();
			if (asnReader.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			AsnEncodedDataCollection asnEncodedDataCollection = new AsnEncodedDataCollection();
			while (asnReader2.HasData)
			{
				byte[] encodedAttribute = asnReader2.GetEncodedValue().ToArray();
				asnEncodedDataCollection.Add(Helpers.CreateBestPkcs9AttributeObjectAvailable(oid, encodedAttribute));
			}
			return new CryptographicAttributeObject(oid, asnEncodedDataCollection);
		}

		internal SignerInfo()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> class represents a collection of <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> objects. <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> implements the <see cref="T:System.Collections.ICollection" /> interface.</summary>
	public sealed class SignerInfoCollection : ICollection, IEnumerable
	{
		private readonly SignerInfo[] _signerInfos;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.Item(System.Int32)" /> property retrieves the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object at the specified index in the collection.</summary>
		/// <param name="index">An int value that represents the index in the collection. The index is zero based.</param>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object  at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		public SignerInfo this[int index]
		{
			get
			{
				if (index < 0 || index >= _signerInfos.Length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return _signerInfos[index];
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.Count" /> property retrieves the number of items in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>An int value that represents the number of items in the collection.</returns>
		public int Count => _signerInfos.Length;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.IsSynchronized" /> property retrieves whether access to the collection is synchronized, or thread safe. This property always returns <see langword="false" />, which means the collection is not thread safe.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value of <see langword="false" />, which means the collection is not thread safe.</returns>
		public bool IsSynchronized => false;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoCollection.SyncRoot" /> property retrieves an <see cref="T:System.Object" /> object is used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> object is used to synchronize access to the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		public object SyncRoot => this;

		internal SignerInfoCollection()
		{
			_signerInfos = Array.Empty<SignerInfo>();
		}

		internal SignerInfoCollection(SignerInfo[] signerInfos)
		{
			_signerInfos = signerInfos;
		}

		internal SignerInfoCollection(SignerInfoAsn[] signedDataSignerInfos, SignedCms ownerDocument)
		{
			_signerInfos = new SignerInfo[signedDataSignerInfos.Length];
			for (int i = 0; i < signedDataSignerInfos.Length; i++)
			{
				_signerInfos[i] = new SignerInfo(ref signedDataSignerInfos[i], ownerDocument);
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		public SignerInfoEnumerator GetEnumerator()
		{
			return new SignerInfoEnumerator(this);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.System#Collections#IEnumerable#GetEnumerator" /> method returns a <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object for the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> object that can be used to enumerate the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SignerInfoEnumerator(this);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.CopyTo(System.Array,System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection to an array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object to which the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (index + Count > array.Length)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			for (int i = 0; i < Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoCollection.CopyTo(System.Security.Cryptography.Pkcs.SignerInfo[],System.Int32)" /> method copies the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection to a <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> array.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> objects where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is to be copied.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection is copied.</param>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		public void CopyTo(SignerInfo[] array, int index)
		{
			((ICollection)this).CopyTo((Array)array, index);
		}

		internal int FindIndexForSigner(SignerInfo signer)
		{
			SubjectIdentifier signerIdentifier = signer.SignerIdentifier;
			X509IssuerSerial x509IssuerSerial = default(X509IssuerSerial);
			if (signerIdentifier.Type == SubjectIdentifierType.IssuerAndSerialNumber)
			{
				x509IssuerSerial = (X509IssuerSerial)signerIdentifier.Value;
			}
			for (int i = 0; i < _signerInfos.Length; i++)
			{
				SubjectIdentifier signerIdentifier2 = _signerInfos[i].SignerIdentifier;
				if (signerIdentifier2.Type != signerIdentifier.Type)
				{
					continue;
				}
				bool flag = false;
				switch (signerIdentifier.Type)
				{
				case SubjectIdentifierType.IssuerAndSerialNumber:
				{
					X509IssuerSerial x509IssuerSerial2 = (X509IssuerSerial)signerIdentifier2.Value;
					if (x509IssuerSerial2.IssuerName == x509IssuerSerial.IssuerName && x509IssuerSerial2.SerialNumber == x509IssuerSerial.SerialNumber)
					{
						flag = true;
					}
					break;
				}
				case SubjectIdentifierType.SubjectKeyIdentifier:
					if ((string)signerIdentifier.Value == (string)signerIdentifier2.Value)
					{
						flag = true;
					}
					break;
				case SubjectIdentifierType.NoSignature:
					flag = true;
					break;
				default:
					throw new CryptographicException();
				}
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> class provides enumeration functionality for the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection. <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> implements the <see cref="T:System.Collections.IEnumerator" /> interface.</summary>
	public sealed class SignerInfoEnumerator : IEnumerator
	{
		private readonly SignerInfoCollection _signerInfos;

		private int _position;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object that represents the current signer information structure in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		public SignerInfo Current => _signerInfos[_position];

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.System#Collections#IEnumerator#Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object that represents the current signer information structure in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		object IEnumerator.Current => _signerInfos[_position];

		private SignerInfoEnumerator()
		{
		}

		internal SignerInfoEnumerator(SignerInfoCollection signerInfos)
		{
			_signerInfos = signerInfos;
			_position = -1;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.MoveNext" /> method advances the enumeration to the next   <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>This method returns a bool value that specifies whether the enumeration successfully advanced. If the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object, the method returns <see langword="true" />. If the enumeration moved past the last item in the enumeration, it returns <see langword="false" />.</returns>
		public bool MoveNext()
		{
			int num = _position + 1;
			if (num >= _signerInfos.Count)
			{
				return false;
			}
			_position = num;
			return true;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.Reset" /> method resets the enumeration to the first <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		public void Reset()
		{
			_position = -1;
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> class defines the type of the identifier of a subject, such as a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> or a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" />.  The subject can be identified by the certificate issuer and serial number or the subject key.</summary>
	public sealed class SubjectIdentifier
	{
		private const string DummySignerSubjectName = "CN=Dummy Signer";

		internal static readonly byte[] DummySignerEncodedValue = new X500DistinguishedName("CN=Dummy Signer").RawData;

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> property retrieves the type of subject identifier. The subject can be identified by the certificate issuer and serial number or the subject key.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that identifies the type of subject.</returns>
		public SubjectIdentifierType Type { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Value" /> property retrieves the value of the subject identifier. Use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> property to determine the type of subject identifier, and use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Value" /> property to retrieve the corresponding value.</summary>
		/// <returns>An <see cref="T:System.Object" /> object that represents the value of the subject identifier. This <see cref="T:System.Object" /> can be one of the following objects as determined by the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> property.  
		///  <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> property  
		///
		///   Object  
		///
		///   IssuerAndSerialNumber  
		///
		///  <see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" /> SubjectKeyIdentifier  
		///
		///  <see cref="T:System.String" /></returns>
		public object Value { get; }

		internal SubjectIdentifier(SubjectIdentifierType type, object value)
		{
			Type = type;
			Value = value;
		}

		internal SubjectIdentifier(SignerIdentifierAsn signerIdentifierAsn)
			: this(signerIdentifierAsn.IssuerAndSerialNumber, signerIdentifierAsn.SubjectKeyIdentifier)
		{
		}

		internal SubjectIdentifier(IssuerAndSerialNumberAsn? issuerAndSerialNumber, ReadOnlyMemory<byte>? subjectKeyIdentifier)
		{
			if (issuerAndSerialNumber.HasValue)
			{
				IssuerAndSerialNumberAsn value = issuerAndSerialNumber.Value;
				ReadOnlySpan<byte> span = value.Issuer.Span;
				value = issuerAndSerialNumber.Value;
				ReadOnlySpan<byte> span2 = value.SerialNumber.Span;
				bool flag = false;
				for (int i = 0; i < span2.Length; i++)
				{
					if (span2[i] != 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag && DummySignerEncodedValue.AsSpan().SequenceEqual(span))
				{
					Type = SubjectIdentifierType.NoSignature;
					Value = null;
				}
				else
				{
					Type = SubjectIdentifierType.IssuerAndSerialNumber;
					X500DistinguishedName x500DistinguishedName = new X500DistinguishedName(span.ToArray());
					Value = new X509IssuerSerial(x500DistinguishedName.Name, span2.ToBigEndianHex());
				}
			}
			else
			{
				if (!subjectKeyIdentifier.HasValue)
				{
					throw new CryptographicException();
				}
				Type = SubjectIdentifierType.SubjectKeyIdentifier;
				Value = subjectKeyIdentifier.Value.Span.ToBigEndianHex();
			}
		}

		internal SubjectIdentifier()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey" /> class defines the type of the identifier of a subject, such as a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> or a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" />.  The subject can be identified by the certificate issuer and serial number, the hash of the subject key, or the subject key.</summary>
	public sealed class SubjectIdentifierOrKey
	{
		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> property retrieves the type of subject identifier or key. The subject can be identified by the certificate issuer and serial number, the hash of the subject key, or the subject key.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKeyType" /> enumeration that specifies the type of subject identifier.</returns>
		public SubjectIdentifierOrKeyType Type { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Value" /> property retrieves the value of the subject identifier or  key. Use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> property to determine the type of subject identifier or key, and use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Value" /> property to retrieve the corresponding value.</summary>
		/// <returns>An <see cref="T:System.Object" /> object that represents the value of the subject identifier or key. This <see cref="T:System.Object" /> can be one of the following objects as determined by the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> property.  
		///  <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> property  
		///
		///   Object  
		///
		///   IssuerAndSerialNumber  
		///
		///  <see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" /> SubjectKeyIdentifier  
		///
		///  <see cref="T:System.String" /> PublicKeyInfo  
		///
		///  <see cref="T:System.Security.Cryptography.Pkcs.PublicKeyInfo" /></returns>
		public object Value { get; }

		internal SubjectIdentifierOrKey(SubjectIdentifierOrKeyType type, object value)
		{
			Type = type;
			Value = value;
		}

		internal SubjectIdentifierOrKey()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKeyType" /> enumeration defines how a subject is identified.</summary>
	public enum SubjectIdentifierOrKeyType
	{
		/// <summary>The type is unknown.</summary>
		Unknown,
		/// <summary>The subject is identified by the certificate issuer and serial number.</summary>
		IssuerAndSerialNumber,
		/// <summary>The subject is identified by the hash of the subject key.</summary>
		SubjectKeyIdentifier,
		/// <summary>The subject is identified by the public key.</summary>
		PublicKeyInfo
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration defines the type of subject identifier.</summary>
	public enum SubjectIdentifierType
	{
		/// <summary>The type of subject identifier is unknown.</summary>
		Unknown,
		/// <summary>The subject is identified by the certificate issuer and serial number.</summary>
		IssuerAndSerialNumber,
		/// <summary>The subject is identified by the hash of the subject's public key. The hash algorithm used is determined by the signature algorithm suite in the subject's certificate.</summary>
		SubjectKeyIdentifier,
		/// <summary>The subject is identified as taking part in an integrity check operation that uses only a hashing algorithm.</summary>
		NoSignature
	}
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeKeyChoice" /> enumeration defines the type of key used in a key agreement protocol.</summary>
	public enum KeyAgreeKeyChoice
	{
		/// <summary>The key agreement key type is unknown.</summary>
		Unknown,
		/// <summary>The key agreement key is ephemeral, existing only for the duration of the key agreement protocol.</summary>
		EphemeralKey,
		/// <summary>The key agreement key is static, existing for an extended period of time.</summary>
		StaticKey
	}
	public sealed class Pkcs12Builder
	{
		public bool IsSealed
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public void AddSafeContentsEncrypted(Pkcs12SafeContents safeContents, byte[] passwordBytes, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		public void AddSafeContentsEncrypted(Pkcs12SafeContents safeContents, ReadOnlySpan<byte> passwordBytes, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		public void AddSafeContentsEncrypted(Pkcs12SafeContents safeContents, ReadOnlySpan<char> password, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		public void AddSafeContentsEncrypted(Pkcs12SafeContents safeContents, string password, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		public void AddSafeContentsUnencrypted(Pkcs12SafeContents safeContents)
		{
			throw new PlatformNotSupportedException();
		}

		public byte[] Encode()
		{
			throw new PlatformNotSupportedException();
		}

		public void SealWithMac(ReadOnlySpan<char> password, HashAlgorithmName hashAlgorithm, int iterationCount)
		{
			throw new PlatformNotSupportedException();
		}

		public void SealWithMac(string password, HashAlgorithmName hashAlgorithm, int iterationCount)
		{
			throw new PlatformNotSupportedException();
		}

		public void SealWithoutIntegrity()
		{
			throw new PlatformNotSupportedException();
		}

		public bool TryEncode(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}
	}
	public sealed class Pkcs12CertBag : Pkcs12SafeBag
	{
		public ReadOnlyMemory<byte> EncodedCertificate
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public bool IsX509Certificate
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public Pkcs12CertBag(Oid certificateType, ReadOnlyMemory<byte> encodedCertificate)
			: base(null, default(ReadOnlyMemory<byte>))
		{
			throw new PlatformNotSupportedException();
		}

		public X509Certificate2 GetCertificate()
		{
			throw new PlatformNotSupportedException();
		}

		public Oid GetCertificateType()
		{
			throw new PlatformNotSupportedException();
		}
	}
	public enum Pkcs12ConfidentialityMode
	{
		None = 1,
		Password = 2,
		PublicKey = 3,
		Unknown = 0
	}
	public sealed class Pkcs12Info
	{
		public ReadOnlyCollection<Pkcs12SafeContents> AuthenticatedSafe
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public Pkcs12IntegrityMode IntegrityMode
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		internal Pkcs12Info()
		{
			throw new PlatformNotSupportedException();
		}

		public static Pkcs12Info Decode(ReadOnlyMemory<byte> encodedBytes, out int bytesConsumed, bool skipCopy = false)
		{
			throw new PlatformNotSupportedException();
		}

		public bool VerifyMac(ReadOnlySpan<char> password)
		{
			throw new PlatformNotSupportedException();
		}

		public bool VerifyMac(string password)
		{
			throw new PlatformNotSupportedException();
		}
	}
	public enum Pkcs12IntegrityMode
	{
		None = 1,
		Password = 2,
		PublicKey = 3,
		Unknown = 0
	}
	public sealed class Pkcs12KeyBag : Pkcs12SafeBag
	{
		public ReadOnlyMemory<byte> Pkcs8PrivateKey
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public Pkcs12KeyBag(ReadOnlyMemory<byte> pkcs8PrivateKey, bool skipCopy = false)
			: base(null, default(ReadOnlyMemory<byte>))
		{
			throw new PlatformNotSupportedException();
		}
	}
	public abstract class Pkcs12SafeBag
	{
		public CryptographicAttributeObjectCollection Attributes
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public ReadOnlyMemory<byte> EncodedBagValue
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		protected Pkcs12SafeBag(string bagIdValue, ReadOnlyMemory<byte> encodedBagValue, bool skipCopy = false)
		{
			throw new PlatformNotSupportedException();
		}

		public byte[] Encode()
		{
			throw new PlatformNotSupportedException();
		}

		public Oid GetBagId()
		{
			throw new PlatformNotSupportedException();
		}

		public bool TryEncode(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}
	}
	public sealed class Pkcs12SafeContents
	{
		public Pkcs12ConfidentialityMode ConfidentialityMode
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public bool IsReadOnly
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public Pkcs12CertBag AddCertificate(X509Certificate2 certificate)
		{
			throw new PlatformNotSupportedException();
		}

		public Pkcs12KeyBag AddKeyUnencrypted(AsymmetricAlgorithm key)
		{
			throw new PlatformNotSupportedException();
		}

		public Pkcs12SafeContentsBag AddNestedContents(Pkcs12SafeContents safeContents)
		{
			throw new PlatformNotSupportedException();
		}

		public void AddSafeBag(Pkcs12SafeBag safeBag)
		{
			throw new PlatformNotSupportedException();
		}

		public Pkcs12SecretBag AddSecret(Oid secretType, ReadOnlyMemory<byte> secretValue)
		{
			throw new PlatformNotSupportedException();
		}

		public Pkcs12ShroudedKeyBag AddShroudedKey(AsymmetricAlgorithm key, byte[] passwordBytes, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		public Pkcs12ShroudedKeyBag AddShroudedKey(AsymmetricAlgorithm key, ReadOnlySpan<byte> passwordBytes, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		public Pkcs12ShroudedKeyBag AddShroudedKey(AsymmetricAlgorithm key, ReadOnlySpan<char> password, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		public Pkcs12ShroudedKeyBag AddShroudedKey(AsymmetricAlgorithm key, string password, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		public void Decrypt(byte[] passwordBytes)
		{
			throw new PlatformNotSupportedException();
		}

		public void Decrypt(ReadOnlySpan<byte> passwordBytes)
		{
			throw new PlatformNotSupportedException();
		}

		public void Decrypt(ReadOnlySpan<char> password)
		{
			throw new PlatformNotSupportedException();
		}

		public void Decrypt(string password)
		{
			throw new PlatformNotSupportedException();
		}

		public IEnumerable<Pkcs12SafeBag> GetBags()
		{
			throw new PlatformNotSupportedException();
		}
	}
	public sealed class Pkcs12SafeContentsBag : Pkcs12SafeBag
	{
		public Pkcs12SafeContents SafeContents
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		internal Pkcs12SafeContentsBag()
			: base(null, default(ReadOnlyMemory<byte>))
		{
			throw new PlatformNotSupportedException();
		}
	}
	public sealed class Pkcs12SecretBag : Pkcs12SafeBag
	{
		public ReadOnlyMemory<byte> SecretValue
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		internal Pkcs12SecretBag()
			: base(null, default(ReadOnlyMemory<byte>))
		{
			throw new PlatformNotSupportedException();
		}

		public Oid GetSecretType()
		{
			throw new PlatformNotSupportedException();
		}
	}
	public sealed class Pkcs12ShroudedKeyBag : Pkcs12SafeBag
	{
		public ReadOnlyMemory<byte> EncryptedPkcs8PrivateKey
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public Pkcs12ShroudedKeyBag(ReadOnlyMemory<byte> encryptedPkcs8PrivateKey, bool skipCopy = false)
			: base(null, default(ReadOnlyMemory<byte>))
		{
			throw new PlatformNotSupportedException();
		}
	}
	public sealed class Pkcs8PrivateKeyInfo
	{
		public Oid AlgorithmId
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public ReadOnlyMemory<byte>? AlgorithmParameters
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public CryptographicAttributeObjectCollection Attributes
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public ReadOnlyMemory<byte> PrivateKeyBytes
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public Pkcs8PrivateKeyInfo(Oid algorithmId, ReadOnlyMemory<byte>? algorithmParameters, ReadOnlyMemory<byte> privateKey, bool skipCopies = false)
		{
			throw new PlatformNotSupportedException();
		}

		public static Pkcs8PrivateKeyInfo Create(AsymmetricAlgorithm privateKey)
		{
			throw new PlatformNotSupportedException();
		}

		public static Pkcs8PrivateKeyInfo Decode(ReadOnlyMemory<byte> source, out int bytesRead, bool skipCopy = false)
		{
			throw new PlatformNotSupportedException();
		}

		public static Pkcs8PrivateKeyInfo DecryptAndDecode(ReadOnlySpan<byte> passwordBytes, ReadOnlyMemory<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		public static Pkcs8PrivateKeyInfo DecryptAndDecode(ReadOnlySpan<char> password, ReadOnlyMemory<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		public byte[] Encode()
		{
			throw new PlatformNotSupportedException();
		}

		public byte[] Encrypt(ReadOnlySpan<byte> passwordBytes, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		public byte[] Encrypt(ReadOnlySpan<char> password, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		public bool TryEncode(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		public bool TryEncrypt(ReadOnlySpan<byte> passwordBytes, PbeParameters pbeParameters, Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		public bool TryEncrypt(ReadOnlySpan<char> password, PbeParameters pbeParameters, Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}
	}
	public sealed class Pkcs9LocalKeyId : Pkcs9AttributeObject
	{
		public ReadOnlyMemory<byte> KeyId
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public Pkcs9LocalKeyId()
		{
			throw new PlatformNotSupportedException();
		}

		public Pkcs9LocalKeyId(byte[] keyId)
		{
			throw new PlatformNotSupportedException();
		}

		public Pkcs9LocalKeyId(ReadOnlySpan<byte> keyId)
		{
			throw new PlatformNotSupportedException();
		}
	}
	public sealed class Rfc3161TimestampRequest
	{
		public bool HasExtensions
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public Oid HashAlgorithmId
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public Oid RequestedPolicyId
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public bool RequestSignerCertificate
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public int Version
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		internal Rfc3161TimestampRequest()
		{
			throw new PlatformNotSupportedException();
		}

		public static Rfc3161TimestampRequest CreateFromData(ReadOnlySpan<byte> data, HashAlgorithmName hashAlgorithm, Oid requestedPolicyId = null, ReadOnlyMemory<byte>? nonce = null, bool requestSignerCertificates = false, X509ExtensionCollection extensions = null)
		{
			throw new PlatformNotSupportedException();
		}

		public static Rfc3161TimestampRequest CreateFromHash(ReadOnlyMemory<byte> hash, HashAlgorithmName hashAlgorithm, Oid requestedPolicyId = null, ReadOnlyMemory<byte>? nonce = null, bool requestSignerCertificates = false, X509ExtensionCollection extensions = null)
		{
			throw new PlatformNotSupportedException();
		}

		public static Rfc3161TimestampRequest CreateFromHash(ReadOnlyMemory<byte> hash, Oid hashAlgorithmId, Oid requestedPolicyId = null, ReadOnlyMemory<byte>? nonce = null, bool requestSignerCertificates = false, X509ExtensionCollection extensions = null)
		{
			throw new PlatformNotSupportedException();
		}

		public static Rfc3161TimestampRequest CreateFromSignerInfo(SignerInfo signerInfo, HashAlgorithmName hashAlgorithm, Oid requestedPolicyId = null, ReadOnlyMemory<byte>? nonce = null, bool requestSignerCertificates = false, X509ExtensionCollection extensions = null)
		{
			throw new PlatformNotSupportedException();
		}

		public byte[] Encode()
		{
			throw new PlatformNotSupportedException();
		}

		public X509ExtensionCollection GetExtensions()
		{
			throw new PlatformNotSupportedException();
		}

		public ReadOnlyMemory<byte> GetMessageHash()
		{
			throw new PlatformNotSupportedException();
		}

		public ReadOnlyMemory<byte>? GetNonce()
		{
			throw new PlatformNotSupportedException();
		}

		public Rfc3161TimestampToken ProcessResponse(ReadOnlyMemory<byte> responseBytes, out int bytesConsumed)
		{
			throw new PlatformNotSupportedException();
		}

		public static bool TryDecode(ReadOnlyMemory<byte> encodedBytes, out Rfc3161TimestampRequest request, out int bytesConsumed)
		{
			throw new PlatformNotSupportedException();
		}

		public bool TryEncode(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}
	}
	public sealed class Rfc3161TimestampToken
	{
		public Rfc3161TimestampTokenInfo TokenInfo
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		internal Rfc3161TimestampToken()
		{
			throw new PlatformNotSupportedException();
		}

		public SignedCms AsSignedCms()
		{
			throw new PlatformNotSupportedException();
		}

		public static bool TryDecode(ReadOnlyMemory<byte> encodedBytes, out Rfc3161TimestampToken token, out int bytesConsumed)
		{
			throw new PlatformNotSupportedException();
		}

		public bool VerifySignatureForData(ReadOnlySpan<byte> data, out X509Certificate2 signerCertificate, X509Certificate2Collection extraCandidates = null)
		{
			throw new PlatformNotSupportedException();
		}

		public bool VerifySignatureForHash(ReadOnlySpan<byte> hash, HashAlgorithmName hashAlgorithm, out X509Certificate2 signerCertificate, X509Certificate2Collection extraCandidates = null)
		{
			throw new PlatformNotSupportedException();
		}

		public bool VerifySignatureForHash(ReadOnlySpan<byte> hash, Oid hashAlgorithmId, out X509Certificate2 signerCertificate, X509Certificate2Collection extraCandidates = null)
		{
			throw new PlatformNotSupportedException();
		}

		public bool VerifySignatureForSignerInfo(SignerInfo signerInfo, out X509Certificate2 signerCertificate, X509Certificate2Collection extraCandidates = null)
		{
			throw new PlatformNotSupportedException();
		}
	}
	public sealed class Rfc3161TimestampTokenInfo
	{
		public long? AccuracyInMicroseconds
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public bool HasExtensions
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public Oid HashAlgorithmId
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public bool IsOrdering
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public Oid PolicyId
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public DateTimeOffset Timestamp
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public int Version
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		public Rfc3161TimestampTokenInfo(Oid policyId, Oid hashAlgorithmId, ReadOnlyMemory<byte> messageHash, ReadOnlyMemory<byte> serialNumber, DateTimeOffset timestamp, long? accuracyInMicroseconds = null, bool isOrdering = false, ReadOnlyMemory<byte>? nonce = null, ReadOnlyMemory<byte>? timestampAuthorityName = null, X509ExtensionCollection extensions = null)
		{
			throw new PlatformNotSupportedException();
		}

		public byte[] Encode()
		{
			throw new PlatformNotSupportedException();
		}

		public X509ExtensionCollection GetExtensions()
		{
			throw new PlatformNotSupportedException();
		}

		public ReadOnlyMemory<byte> GetMessageHash()
		{
			throw new PlatformNotSupportedException();
		}

		public ReadOnlyMemory<byte>? GetNonce()
		{
			throw new PlatformNotSupportedException();
		}

		public ReadOnlyMemory<byte> GetSerialNumber()
		{
			throw new PlatformNotSupportedException();
		}

		public ReadOnlyMemory<byte>? GetTimestampAuthorityName()
		{
			throw new PlatformNotSupportedException();
		}

		public static bool TryDecode(ReadOnlyMemory<byte> encodedBytes, out Rfc3161TimestampTokenInfo timestampTokenInfo, out int bytesConsumed)
		{
			throw new PlatformNotSupportedException();
		}

		public bool TryEncode(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
namespace System.Security.Cryptography.Pkcs.Asn1
{
	internal struct AlgorithmIdentifierAsn
	{
		internal static readonly ReadOnlyMemory<byte> ExplicitDerNull = new byte[2] { 5, 0 };

		[ObjectIdentifier(PopulateFriendlyName = true)]
		public Oid Algorithm;

		[OptionalValue]
		[AnyValue]
		public ReadOnlyMemory<byte>? Parameters;

		internal bool Equals(ref AlgorithmIdentifierAsn other)
		{
			if (Algorithm.Value != other.Algorithm.Value)
			{
				return false;
			}
			bool flag = RepresentsNull(Parameters);
			bool flag2 = RepresentsNull(other.Parameters);
			if (flag != flag2)
			{
				return false;
			}
			if (flag)
			{
				return true;
			}
			ReadOnlyMemory<byte> value = Parameters.Value;
			ReadOnlySpan<byte> span = value.Span;
			value = other.Parameters.Value;
			return span.SequenceEqual(value.Span);
		}

		private static bool RepresentsNull(ReadOnlyMemory<byte>? parameters)
		{
			if (!parameters.HasValue)
			{
				return true;
			}
			ReadOnlySpan<byte> span = parameters.Value.Span;
			if (span.Length != 2)
			{
				return false;
			}
			if (span[0] != 5)
			{
				return false;
			}
			return span[1] == 0;
		}
	}
	internal struct AttributeAsn
	{
		public Oid AttrType;

		[AnyValue]
		public ReadOnlyMemory<byte> AttrValues;
	}
	[Choice]
	internal struct CertificateChoiceAsn
	{
		[ExpectedTag(TagClass.Universal, 16)]
		[AnyValue]
		public ReadOnlyMemory<byte>? Certificate;
	}
	internal struct ContentInfoAsn
	{
		[ObjectIdentifier]
		public string ContentType;

		[AnyValue]
		[ExpectedTag(0, ExplicitTag = true)]
		public ReadOnlyMemory<byte> Content;
	}
	internal struct EncapsulatedContentInfoAsn
	{
		[ObjectIdentifier]
		public string ContentType;

		[AnyValue]
		[ExpectedTag(0, ExplicitTag = true)]
		[OptionalValue]
		public ReadOnlyMemory<byte>? Content;
	}
	internal struct EncryptedContentInfoAsn
	{
		[ObjectIdentifier]
		internal string ContentType;

		internal AlgorithmIdentifierAsn ContentEncryptionAlgorithm;

		[ExpectedTag(0)]
		[OctetString]
		[OptionalValue]
		internal ReadOnlyMemory<byte>? EncryptedContent;
	}
	internal struct EnvelopedDataAsn
	{
		public int Version;

		[OptionalValue]
		[ExpectedTag(0)]
		public OriginatorInfoAsn OriginatorInfo;

		[SetOf]
		public RecipientInfoAsn[] RecipientInfos;

		public EncryptedContentInfoAsn EncryptedContentInfo;

		[OptionalValue]
		[ExpectedTag(1)]
		[SetOf]
		public AttributeAsn[] UnprotectedAttributes;
	}
	[Choice]
	internal struct GeneralName
	{
		[ExpectedTag(0, ExplicitTag = true)]
		internal OtherName? OtherName;

		[IA5String]
		[ExpectedTag(1, ExplicitTag = true)]
		internal string Rfc822Name;

		[IA5String]
		[ExpectedTag(2, ExplicitTag = true)]
		internal string DnsName;

		[AnyValue]
		[ExpectedTag(3, ExplicitTag = true)]
		internal ReadOnlyMemory<byte>? X400Address;

		[AnyValue]
		[ExpectedTag(4, ExplicitTag = true)]
		internal ReadOnlyMemory<byte>? DirectoryName;

		[ExpectedTag(5, ExplicitTag = true)]
		internal EdiPartyName? EdiPartyName;

		[IA5String]
		[ExpectedTag(6, ExplicitTag = true)]
		internal string Uri;

		[OctetString]
		[ExpectedTag(7, ExplicitTag = true)]
		internal ReadOnlyMemory<byte>? IPAddress;

		[ExpectedTag(8, ExplicitTag = true)]
		[ObjectIdentifier]
		internal string RegisteredId;
	}
	internal struct OtherName
	{
		internal string TypeId;

		[ExpectedTag(0, ExplicitTag = true)]
		[AnyValue]
		internal ReadOnlyMemory<byte> Value;
	}
	internal struct EdiPartyName
	{
		[OptionalValue]
		internal DirectoryString? NameAssigner;

		internal DirectoryString PartyName;
	}
	[Choice]
	internal struct DirectoryString
	{
		[ExpectedTag(TagClass.Universal, 20)]
		internal ReadOnlyMemory<byte>? TeletexString;

		[PrintableString]
		internal string PrintableString;

		[ExpectedTag(TagClass.Universal, 28)]
		internal ReadOnlyMemory<byte>? UniversalString;

		[UTF8String]
		internal string Utf8String;

		[BMPString]
		internal string BMPString;
	}
	internal struct IssuerAndSerialNumberAsn
	{
		[AnyValue]
		public ReadOnlyMemory<byte> Issuer;

		[Integer]
		public ReadOnlyMemory<byte> SerialNumber;
	}
	[Choice]
	internal struct KeyAgreeRecipientIdentifierAsn
	{
		internal IssuerAndSerialNumberAsn? IssuerAndSerialNumber;

		[ExpectedTag(0)]
		internal RecipientKeyIdentifier RKeyId;
	}
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class KeyAgreeRecipientInfoAsn
	{
		internal int Version;

		[ExpectedTag(0, ExplicitTag = true)]
		internal OriginatorIdentifierOrKeyAsn Originator;

		[ExpectedTag(1, ExplicitTag = true)]
		[OctetString]
		[OptionalValue]
		internal ReadOnlyMemory<byte>? Ukm;

		internal AlgorithmIdentifierAsn KeyEncryptionAlgorithm;

		internal RecipientEncryptedKeyAsn[] RecipientEncryptedKeys;
	}
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class KeyTransRecipientInfoAsn
	{
		internal int Version;

		internal RecipientIdentifierAsn Rid;

		internal AlgorithmIdentifierAsn KeyEncryptionAlgorithm;

		[OctetString]
		internal ReadOnlyMemory<byte> EncryptedKey;
	}
	internal struct MessageImprint
	{
		internal AlgorithmIdentifierAsn HashAlgorithm;

		[OctetString]
		internal ReadOnlyMemory<byte> HashedMessage;
	}
	[Choice]
	internal struct OriginatorIdentifierOrKeyAsn
	{
		internal IssuerAndSerialNumberAsn? IssuerAndSerialNumber;

		[OctetString]
		[ExpectedTag(0)]
		internal ReadOnlyMemory<byte>? SubjectKeyIdentifier;

		[ExpectedTag(1)]
		internal OriginatorPublicKeyAsn OriginatorKey;
	}
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class OriginatorInfoAsn
	{
		[OptionalValue]
		[ExpectedTag(0)]
		[SetOf]
		public CertificateChoiceAsn[] CertificateSet;

		[OptionalValue]
		[ExpectedTag(1)]
		[AnyValue]
		public ReadOnlyMemory<byte>? RevocationInfoChoices;
	}
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class OriginatorPublicKeyAsn
	{
		internal AlgorithmIdentifierAsn Algorithm;

		[BitString]
		internal ReadOnlyMemory<byte> PublicKey;
	}
	internal struct OtherKeyAttributeAsn
	{
		[ObjectIdentifier]
		internal string KeyAttrId;

		[OptionalValue]
		[AnyValue]
		internal ReadOnlyMemory<byte>? KeyAttr;
	}
	internal struct PssParamsAsn
	{
		[ExpectedTag(0, ExplicitTag = true)]
		[DefaultValue(new byte[]
		{
			160, 9, 48, 7, 6, 5, 43, 14, 3, 2,
			26
		})]
		public AlgorithmIdentifierAsn HashAlgorithm;

		[ExpectedTag(1, ExplicitTag = true)]
		[DefaultValue(new byte[]
		{
			161, 22, 48, 20, 6, 9, 42, 134, 72, 134,
			247, 13, 1, 1, 8, 48, 9, 6, 5, 43,
			14, 3, 2, 26
		})]
		public AlgorithmIdentifierAsn MaskGenAlgorithm;

		[ExpectedTag(2, ExplicitTag = true)]
		[DefaultValue(new byte[] { 162, 3, 2, 1, 20 })]
		public int SaltLength;

		[ExpectedTag(3, ExplicitTag = true)]
		[DefaultValue(new byte[] { 163, 3, 2, 1, 1 })]
		public int TrailerField;
	}
	internal struct Rc2CbcParameters
	{
		private static readonly byte[] s_rc2EkbEncoding = new byte[256]
		{
			189, 86, 234, 242, 162, 241, 172, 42, 176, 147,
			209, 156, 27, 51, 253, 208, 48, 4, 182, 220,
			125, 223, 50, 75, 247, 203, 69, 155, 49, 187,
			33, 90, 65, 159, 225, 217, 74, 77, 158, 218,
			160, 104, 44, 195, 39, 95, 128, 54, 62, 238,
			251, 149, 26, 254, 206, 168, 52, 169, 19, 240,
			166, 63, 216, 12, 120, 36, 175, 35, 82, 193,
			103, 23, 245, 102, 144, 231, 232, 7, 184, 96,
			72, 230, 30, 83, 243, 146, 164, 114, 140, 8,
			21, 110, 134, 0, 132, 250, 244, 127, 138, 66,
			25, 246, 219, 205, 20, 141, 80, 18, 186, 60,
			6, 78, 236, 179, 53, 17, 161, 136, 142, 43,
			148, 153, 183, 113, 116, 211, 228, 191, 58, 222,
			150, 14, 188, 10, 237, 119, 252, 55, 107, 3,
			121, 137, 98, 198, 215, 192, 210, 124, 106, 139,
			34, 163, 91, 5, 93, 2, 117, 213, 97, 227,
			24, 143, 85, 81, 173, 31, 11, 94, 133, 229,
			194, 87, 99, 202, 61, 108, 180, 197, 204, 112,
			178, 145, 89, 13, 71, 32, 200, 79, 88, 224,
			1, 226, 22, 56, 196, 111, 59, 15, 101, 70,
			190, 126, 45, 123, 130, 249, 64, 181, 29, 115,
			248, 235, 38, 199, 135, 151, 37, 84, 177, 40,
			170, 152, 157, 165, 100, 109, 122, 212, 16, 129,
			68, 239, 73, 214, 174, 46, 221, 118, 92, 47,
			167, 28, 201, 9, 105, 154, 131, 207, 41, 57,
			185, 233, 76, 255, 67, 171
		};

		internal int Rc2Version;

		[OctetString]
		internal ReadOnlyMemory<byte> Iv;

		internal Rc2CbcParameters(ReadOnlyMemory<byte> iv, int keySize)
		{
			if (keySize > 255)
			{
				Rc2Version = keySize;
			}
			else
			{
				Rc2Version = s_rc2EkbEncoding[keySize];
			}
			Iv = iv;
		}

		internal int GetEffectiveKeyBits()
		{
			if (Rc2Version > 255)
			{
				return Rc2Version;
			}
			return Array.IndexOf(s_rc2EkbEncoding, (byte)Rc2Version);
		}
	}
	internal struct RecipientEncryptedKeyAsn
	{
		internal KeyAgreeRecipientIdentifierAsn Rid;

		[OctetString]
		internal ReadOnlyMemory<byte> EncryptedKey;
	}
	[Choice]
	internal struct RecipientIdentifierAsn
	{
		internal IssuerAndSerialNumberAsn? IssuerAndSerialNumber;

		[ExpectedTag(0)]
		[OctetString]
		internal ReadOnlyMemory<byte>? SubjectKeyIdentifier;
	}
	[Choice]
	internal struct RecipientInfoAsn
	{
		internal KeyTransRecipientInfoAsn Ktri;

		[ExpectedTag(1)]
		internal KeyAgreeRecipientInfoAsn Kari;
	}
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class RecipientKeyIdentifier
	{
		[OctetString]
		internal ReadOnlyMemory<byte> SubjectKeyIdentifier;

		[OptionalValue]
		[GeneralizedTime]
		internal DateTimeOffset? Date;

		[OptionalValue]
		internal OtherKeyAttributeAsn? Other;
	}
	internal struct Rfc3161Accuracy
	{
		[OptionalValue]
		internal int? Seconds;

		[OptionalValue]
		[ExpectedTag(0)]
		internal int? Millis;

		[ExpectedTag(1)]
		[OptionalValue]
		internal int? Micros;

		internal long TotalMicros => 1000000L * (long)Seconds.GetValueOrDefault() + 1000L * (long)Millis.GetValueOrDefault() + Micros.GetValueOrDefault();

		internal Rfc3161Accuracy(long accuracyInMicroseconds)
		{
			if (accuracyInMicroseconds < 0)
			{
				throw new ArgumentOutOfRangeException("accuracyInMicroseconds");
			}
			long result;
			long result2;
			long num = Math.DivRem(Math.DivRem(accuracyInMicroseconds, 1000L, out result), 1000L, out result2);
			if (num != 0L)
			{
				Seconds = checked((int)num);
			}
			else
			{
				Seconds = null;
			}
			if (result2 != 0L)
			{
				Millis = (int)result2;
			}
			else
			{
				Millis = null;
			}
			if (result != 0L)
			{
				Micros = (int)result;
			}
			else
			{
				Micros = null;
			}
		}
	}
	internal struct Rfc3161TimeStampReq
	{
		public int Version;

		public MessageImprint MessageImprint;

		[OptionalValue]
		public Oid ReqPolicy;

		[Integer]
		[OptionalValue]
		public ReadOnlyMemory<byte>? Nonce;

		[DefaultValue(new byte[] { 1, 1, 0 })]
		public bool CertReq;

		[ExpectedTag(0)]
		[OptionalValue]
		internal X509ExtensionAsn[] Extensions;
	}
	internal struct Rfc3161TimeStampResp
	{
		public PkiStatusInfo Status;

		[AnyValue]
		[OptionalValue]
		public ReadOnlyMemory<byte>? TimeStampToken;
	}
	internal struct PkiStatusInfo
	{
		public int Status;

		[OptionalValue]
		[AnyValue]
		[ExpectedTag(TagClass.Universal, 16)]
		public ReadOnlyMemory<byte>? StatusString;

		[OptionalValue]
		public PkiFailureInfo? FailInfo;
	}
	[Flags]
	internal enum PkiFailureInfo
	{
		None = 0,
		BadAlg = 1,
		BadMessageCheck = 2,
		BadRequest = 4,
		BadTime = 8,
		BadCertId = 0x10,
		BadDataFormat = 0x20,
		WrongAuthority = 0x40,
		IncorrectData = 0x80,
		MissingTimeStamp = 0x100,
		BadPop = 0x200,
		CertRevoked = 0x400,
		CertConfirmed = 0x800,
		WrongIntegrity = 0x1000,
		BadRecipientNonce = 0x2000,
		TimeNotAvailable = 0x4000,
		UnacceptedPolicy = 0x8000,
		UnacceptedExtension = 0x10000,
		AddInfoNotAvailable = 0x20000,
		BadSenderNonce = 0x40000,
		BadCertTemplate = 0x80000,
		SignerNotTrusted = 0x100000,
		TransactionIdInUse = 0x200000,
		UnsupportedVersion = 0x400000,
		NotAuthorized = 0x800000,
		SystemUnavail = 0x1000000,
		SystemFailure = 0x2000000,
		DuplicateCertReq = 0x4000000
	}
	internal enum PkiStatus
	{
		Granted,
		GrantedWithMods,
		Rejection,
		Waiting,
		RevocationWarning,
		RevocationNotification,
		KeyUpdateWarning
	}
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class Rfc3161TstInfo
	{
		internal int Version;

		[ObjectIdentifier(PopulateFriendlyName = true)]
		internal Oid Policy;

		internal MessageImprint MessageImprint;

		[Integer]
		internal ReadOnlyMemory<byte> SerialNumber;

		[GeneralizedTime(DisallowFractions = false)]
		internal DateTimeOffset GenTime;

		[OptionalValue]
		internal Rfc3161Accuracy? Accuracy;

		[DefaultValue(new byte[] { 1, 1, 0 })]
		internal bool Ordering;

		[Integer]
		[OptionalValue]
		internal ReadOnlyMemory<byte>? Nonce;

		[ExpectedTag(0, ExplicitTag = true)]
		[OptionalValue]
		internal GeneralName? Tsa;

		[ExpectedTag(1)]
		[OptionalValue]
		internal X509ExtensionAsn[] Extensions;
	}
	internal struct SignedDataAsn
	{
		public int Version;

		[SetOf]
		public AlgorithmIdentifierAsn[] DigestAlgorithms;

		public EncapsulatedContentInfoAsn EncapContentInfo;

		[ExpectedTag(0)]
		[SetOf]
		[OptionalValue]
		public CertificateChoiceAsn[] CertificateSet;

		[AnyValue]
		[ExpectedTag(1)]
		[OptionalValue]
		public ReadOnlyMemory<byte>? RevocationInfoChoices;

		[SetOf]
		public SignerInfoAsn[] SignerInfos;
	}
	[Choice]
	internal struct SignerIdentifierAsn
	{
		public IssuerAndSerialNumberAsn? IssuerAndSerialNumber;

		[OctetString]
		[ExpectedTag(0)]
		public ReadOnlyMemory<byte>? SubjectKeyIdentifier;
	}
	internal struct SignerInfoAsn
	{
		public int Version;

		public SignerIdentifierAsn Sid;

		public AlgorithmIdentifierAsn DigestAlgorithm;

		[ExpectedTag(0)]
		[OptionalValue]
		[AnyValue]
		public ReadOnlyMemory<byte>? SignedAttributes;

		public AlgorithmIdentifierAsn SignatureAlgorithm;

		[OctetString]
		public ReadOnlyMemory<byte> SignatureValue;

		[ExpectedTag(1)]
		[SetOf]
		[OptionalValue]
		public AttributeAsn[] UnsignedAttributes;
	}
	[Choice]
	internal struct SignedAttributesSet
	{
		[SetOf]
		[ExpectedTag(0)]
		public AttributeAsn[] SignedAttributes;
	}
	internal struct SigningCertificateAsn
	{
		public EssCertId[] Certs;

		[OptionalValue]
		public PolicyInformation[] Policies;
	}
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class EssCertId
	{
		[OctetString]
		public ReadOnlyMemory<byte> Hash;

		[OptionalValue]
		public CadesIssuerSerial? IssuerSerial;
	}
	internal struct CadesIssuerSerial
	{
		public GeneralName[] Issuer;

		[Integer]
		public ReadOnlyMemory<byte> SerialNumber;
	}
	internal struct PolicyInformation
	{
		[ObjectIdentifier]
		public string PolicyIdentifier;

		[OptionalValue]
		public PolicyQualifierInfo[] PolicyQualifiers;
	}
	internal struct PolicyQualifierInfo
	{
		[ObjectIdentifier]
		public string PolicyQualifierId;

		[AnyValue]
		public ReadOnlyMemory<byte> Qualifier;
	}
	internal struct SigningCertificateV2Asn
	{
		public EssCertIdV2[] Certs;

		[OptionalValue]
		public PolicyInformation[] Policies;
	}
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class EssCertIdV2
	{
		[DefaultValue(new byte[]
		{
			48, 11, 6, 9, 96, 134, 72, 1, 101, 3,
			4, 2, 1
		})]
		public AlgorithmIdentifierAsn HashAlgorithm;

		[OctetString]
		public ReadOnlyMemory<byte> Hash;

		[OptionalValue]
		public CadesIssuerSerial? IssuerSerial;
	}
	internal struct X509ExtensionAsn
	{
		[ObjectIdentifier]
		internal string ExtnId;

		[DefaultValue(new byte[] { 1, 1, 0 })]
		internal bool Critical;

		[OctetString]
		internal ReadOnlyMemory<byte> ExtnValue;

		public X509ExtensionAsn(X509Extension extension, bool copyValue = true)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			ExtnId = extension.Oid.Value;
			Critical = extension.Critical;
			ExtnValue = (copyValue ? extension.RawData.CloneByteArray() : extension.RawData);
		}
	}
}
namespace System.Security.Cryptography.Asn1
{
	internal class AsnSerializationConstraintException : CryptographicException
	{
		public AsnSerializationConstraintException()
		{
		}

		public AsnSerializationConstraintException(string message)
			: base(message)
		{
		}

		public AsnSerializationConstraintException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
	internal class AsnAmbiguousFieldTypeException : AsnSerializationConstraintException
	{
		public AsnAmbiguousFieldTypeException(FieldInfo fieldInfo, Type ambiguousType)
			: base(global::SR.Format("Field '{0}' of type '{1}' has ambiguous type '{2}', an attribute derived from AsnTypeAttribute is required.", fieldInfo.Name, fieldInfo.DeclaringType.FullName, ambiguousType.Namespace))
		{
		}
	}
	internal class AsnSerializerInvalidDefaultException : AsnSerializationConstraintException
	{
		internal AsnSerializerInvalidDefaultException()
		{
		}

		internal AsnSerializerInvalidDefaultException(Exception innerException)
			: base(string.Empty, innerException)
		{
		}
	}
	internal static class AsnSerializer
	{
		private delegate void Serializer(object value, AsnWriter writer);

		private delegate object Deserializer(AsnReader reader);

		private delegate bool TryDeserializer<T>(AsnReader reader, out T value);

		private struct SerializerFieldData
		{
			internal bool WasCustomized;

			internal UniversalTagNumber? TagType;

			internal bool? PopulateOidFriendlyName;

			internal bool IsAny;

			internal bool IsCollection;

			internal byte[] DefaultContents;

			internal bool HasExplicitTag;

			internal bool SpecifiedTag;

			internal bool IsOptional;

			internal int? TwoDigitYearMax;

			internal Asn1Tag ExpectedTag;

			internal bool? DisallowGeneralizedTimeFractions;
		}

		private const BindingFlags FieldFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		private static readonly ConcurrentDictionary<Type, FieldInfo[]> s_orderedFields = new ConcurrentDictionary<Type, FieldInfo[]>();

		private static Deserializer TryOrFail<T>(TryDeserializer<T> tryDeserializer)
		{
			return delegate(AsnReader reader)
			{
				if (tryDeserializer(reader, out var value))
				{
					return value;
				}
				throw new CryptographicException("ASN1 corrupted data.");
			};
		}

		private static FieldInfo[] GetOrderedFields(Type typeT)
		{
			return s_orderedFields.GetOrAdd(typeT, delegate(Type t)
			{
				FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (fields.Length == 0)
				{
					return Array.Empty<FieldInfo>();
				}
				try
				{
					_ = fields[0].MetadataToken;
				}
				catch (InvalidOperationException)
				{
					return fields;
				}
				Array.Sort(fields, (FieldInfo x, FieldInfo y) => x.MetadataToken.CompareTo(y.MetadataToken));
				return fields;
			});
		}

		private static ChoiceAttribute GetChoiceAttribute(Type typeT)
		{
			ChoiceAttribute customAttribute = typeT.GetCustomAttribute<ChoiceAttribute>(inherit: false);
			if (customAttribute == null)
			{
				return null;
			}
			if (customAttribute.AllowNull && !CanBeNull(typeT))
			{
				throw new AsnSerializationConstraintException(global::SR.Format("[Choice].AllowNull=true is not valid because type '{0}' cannot have a null value.", typeT.FullName));
			}
			return customAttribute;
		}

		private static bool CanBeNull(Type t)
		{
			if (t.IsValueType)
			{
				if (t.IsGenericType)
				{
					return t.GetGenericTypeDefinition() == typeof(Nullable<>);
				}
				return false;
			}
			return true;
		}

		private static void PopulateChoiceLookup(Dictionary<(TagClass, int), LinkedList<FieldInfo>> lookup, Type typeT, LinkedList<FieldInfo> currentSet)
		{
			FieldInfo[] orderedFields = GetOrderedFields(typeT);
			foreach (FieldInfo fieldInfo in orderedFields)
			{
				Type fieldType = fieldInfo.FieldType;
				if (!CanBeNull(fieldType))
				{
					throw new AsnSerializationConstraintException(global::SR.Format("Field '{0}' on [Choice] type '{1}' can not be assigned a null value.", fieldInfo.Name, fieldInfo.DeclaringType.FullName));
				}
				fieldType = UnpackIfNullable(fieldType);
				if (currentSet.Contains(fieldInfo))
				{
					throw new AsnSerializationConstraintException(global::SR.Format("Field '{0}' on [Choice] type '{1}' has introduced a type chain cycle.", fieldInfo.Name, fieldInfo.DeclaringType.FullName));
				}
				LinkedListNode<FieldInfo> node = new LinkedListNode<FieldInfo>(fieldInfo);
				currentSet.AddLast(node);
				if (GetChoiceAttribute(fieldType) != null)
				{
					PopulateChoiceLookup(lookup, fieldType, currentSet);
				}
				else
				{
					GetFieldInfo(fieldType, fieldInfo, out var serializerFieldData);
					if (serializerFieldData.DefaultContents != null)
					{
						throw new AsnSerializationConstraintException(global::SR.Format("Field '{0}' on [Choice] type '{1}' has a default value, which is not permitted.", fieldInfo.Name, fieldInfo.DeclaringType.FullName));
					}
					(TagClass, int) key = (serializerFieldData.ExpectedTag.TagClass, serializerFieldData.ExpectedTag.TagValue);
					if (lookup.TryGetValue(key, out var value))
					{
						FieldInfo value2 = value.Last.Value;
						throw new AsnSerializationConstraintException(global::SR.Format("The tag ({0} {1}) for field '{2}' on type '{3}' already is associated in this context with field '{4}' on type '{5}'.", serializerFieldData.ExpectedTag.TagClass, serializerFieldData.ExpectedTag.TagValue, fieldInfo.Name, fieldInfo.DeclaringType.FullName, value2.Name, value2.DeclaringType.FullName));
					}
					lookup.Add(key, new LinkedList<FieldInfo>(currentSet));
				}
				currentSet.RemoveLast();
			}
		}

		private static void SerializeChoice(Type typeT, object value, AsnWriter writer)
		{
			Dictionary<(TagClass, int), LinkedList<FieldInfo>> lookup = new Dictionary<(TagClass, int), LinkedList<FieldInfo>>();
			LinkedList<FieldInfo> currentSet = new LinkedList<FieldInfo>();
			PopulateChoiceLookup(lookup, typeT, currentSet);
			FieldInfo fieldInfo = null;
			object value2 = null;
			if (value == null)
			{
				if (GetChoiceAttribute(typeT).AllowNull)
				{
					writer.WriteNull();
					return;
				}
			}
			else
			{
				FieldInfo[] orderedFields = GetOrderedFields(typeT);
				foreach (FieldInfo fieldInfo2 in orderedFields)
				{
					object value3 = fieldInfo2.GetValue(value);
					if (value3 != null)
					{
						if (fieldInfo != null)
						{
							throw new AsnSerializationConstraintException(global::SR.Format("Fields '{0}' and '{1}' on type '{2}' are both non-null when only one value is permitted.", fieldInfo2.Name, fieldInfo.Name, typeT.FullName));
						}
						fieldInfo = fieldInfo2;
						value2 = value3;
					}
				}
			}
			if (fieldInfo == null)
			{
				throw new AsnSerializationConstraintException(global::SR.Format("An instance of [Choice] type '{0}' has no non-null fields.", typeT.FullName));
			}
			GetSerializer(fieldInfo.FieldType, fieldInfo)(value2, writer);
		}

		private static object DeserializeChoice(AsnReader reader, Type typeT)
		{
			Dictionary<(TagClass, int), LinkedList<FieldInfo>> dictionary = new Dictionary<(TagClass, int), LinkedList<FieldInfo>>();
			LinkedList<FieldInfo> currentSet = new LinkedList<FieldInfo>();
			PopulateChoiceLookup(dictionary, typeT, currentSet);
			Asn1Tag asn1Tag = reader.PeekTag();
			if (asn1Tag == Asn1Tag.Null)
			{
				if (GetChoiceAttribute(typeT).AllowNull)
				{
					reader.ReadNull();
					return null;
				}
				throw new CryptographicException("ASN1 corrupted data.");
			}
			(TagClass, int) key = (asn1Tag.TagClass, asn1Tag.TagValue);
			if (dictionary.TryGetValue(key, out var value))
			{
				LinkedListNode<FieldInfo> linkedListNode = value.Last;
				FieldInfo value2 = linkedListNode.Value;
				object obj = Activator.CreateInstance(value2.DeclaringType);
				object value3 = GetDeserializer(value2.FieldType, value2)(reader);
				value2.SetValue(obj, value3);
				while (linkedListNode.Previous != null)
				{
					linkedListNode = linkedListNode.Previous;
					value2 = linkedListNode.Value;
					object obj2 = Activator.CreateInstance(value2.DeclaringType);
					value2.SetValue(obj2, obj);
					obj = obj2;
				}
				return obj;
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		private static void SerializeCustomType(Type typeT, object value, AsnWriter writer, Asn1Tag tag)
		{
			writer.PushSequence(tag);
			FieldInfo[] fields = typeT.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (FieldInfo fieldInfo in fields)
			{
				GetSerializer(fieldInfo.FieldType, fieldInfo)(fieldInfo.GetValue(value), writer);
			}
			writer.PopSequence(tag);
		}

		private static object DeserializeCustomType(AsnReader reader, Type typeT, Asn1Tag expectedTag)
		{
			object obj = Activator.CreateInstance(typeT);
			AsnReader asnReader = reader.ReadSequence(expectedTag);
			FieldInfo[] fields = typeT.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (FieldInfo fieldInfo in fields)
			{
				Deserializer deserializer = GetDeserializer(fieldInfo.FieldType, fieldInfo);
				try
				{
					fieldInfo.SetValue(obj, deserializer(asnReader));
				}
				catch (Exception inner)
				{
					throw new CryptographicException(global::SR.Format("Unable to set field {0} on type {1}.", fieldInfo.Name, fieldInfo.DeclaringType.FullName), inner);
				}
			}
			asnReader.ThrowIfNotEmpty();
			return obj;
		}

		private static Deserializer ExplicitValueDeserializer(Deserializer valueDeserializer, Asn1Tag expectedTag)
		{
			return (AsnReader reader) => ExplicitValueDeserializer(reader, valueDeserializer, expectedTag);
		}

		private static object ExplicitValueDeserializer(AsnReader reader, Deserializer valueDeserializer, Asn1Tag expectedTag)
		{
			AsnReader asnReader = reader.ReadSequence(expectedTag);
			object result = valueDeserializer(asnReader);
			asnReader.ThrowIfNotEmpty();
			return result;
		}

		private static Deserializer DefaultValueDeserializer(Deserializer valueDeserializer, bool isOptional, byte[] defaultContents, Asn1Tag? expectedTag)
		{
			return (AsnReader reader) => DefaultValueDeserializer(reader, expectedTag, valueDeserializer, defaultContents, isOptional);
		}

		private static object DefaultValueDeserializer(AsnReader reader, Asn1Tag? expectedTag, Deserializer valueDeserializer, byte[] defaultContents, bool isOptional)
		{
			if (reader.HasData)
			{
				Asn1Tag asn1Tag = reader.PeekTag();
				if (!expectedTag.HasValue || asn1Tag.AsPrimitive() == expectedTag.Value.AsPrimitive())
				{
					return valueDeserializer(reader);
				}
			}
			if (isOptional)
			{
				return null;
			}
			if (defaultContents != null)
			{
				return DefaultValue(defaultContents, valueDeserializer);
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		private static Serializer GetSerializer(Type typeT, FieldInfo fieldInfo)
		{
			byte[] defaultContents;
			bool isOptional;
			Asn1Tag? explicitTag;
			Serializer literalValueSerializer = GetSimpleSerializer(typeT, fieldInfo, out defaultContents, out isOptional, out explicitTag);
			Serializer serializer = literalValueSerializer;
			if (isOptional)
			{
				serializer = delegate(object obj, AsnWriter writer)
				{
					if (obj != null)
					{
						literalValueSerializer(obj, writer);
					}
				};
			}
			else if (defaultContents != null)
			{
				serializer = delegate(object obj, AsnWriter writer)
				{
					AsnReader asnReader;
					using (AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER))
					{
						literalValueSerializer(obj, asnWriter);
						asnReader = new AsnReader(asnWriter.Encode(), AsnEncodingRules.DER);
					}
					ReadOnlySpan<byte> span = asnReader.GetEncodedValue().Span;
					bool flag = false;
					if (span.Length == defaultContents.Length)
					{
						flag = true;
						for (int i = 0; i < span.Length; i++)
						{
							if (span[i] != defaultContents[i])
							{
								flag = false;
								break;
							}
						}
					}
					if (!flag)
					{
						literalValueSerializer(obj, writer);
					}
				};
			}
			if (explicitTag.HasValue)
			{
				return delegate(object obj, AsnWriter writer)
				{
					using AsnWriter asnWriter = new AsnWriter(writer.RuleSet);
					serializer(obj, asnWriter);
					if (asnWriter.Encode().Length != 0)
					{
						writer.PushSequence(explicitTag.Value);
						serializer(obj, writer);
						writer.PopSequence(explicitTag.Value);
					}
				};
			}
			return serializer;
		}

		private static Serializer GetSimpleSerializer(Type typeT, FieldInfo fieldInfo, out byte[] defaultContents, out bool isOptional, out Asn1Tag? explicitTag)
		{
			if (!typeT.IsSealed || typeT.ContainsGenericParameters)
			{
				throw new AsnSerializationConstraintException(global::SR.Format("Type '{0}' cannot be serialized or deserialized because it is not sealed or has unbound generic parameters.", typeT.FullName));
			}
			GetFieldInfo(typeT, fieldInfo, out var fieldData);
			defaultContents = fieldData.DefaultContents;
			isOptional = fieldData.IsOptional;
			typeT = UnpackIfNullable(typeT);
			bool flag = GetChoiceAttribute(typeT) != null;
			Asn1Tag tag;
			if (fieldData.HasExplicitTag)
			{
				explicitTag = fieldData.ExpectedTag;
				tag = new Asn1Tag(fieldData.TagType.GetValueOrDefault());
			}
			else
			{
				explicitTag = null;
				tag = fieldData.ExpectedTag;
			}
			if (typeT.IsPrimitive)
			{
				return GetPrimitiveSerializer(typeT, tag);
			}
			if (typeT.IsEnum)
			{
				if (typeT.GetCustomAttributes(typeof(FlagsAttribute), inherit: false).Length != 0)
				{
					return delegate(object value, AsnWriter writer)
					{
						writer.WriteNamedBitList(tag, value);
					};
				}
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteEnumeratedValue(tag, value);
				};
			}
			if (typeT == typeof(string))
			{
				if (fieldData.TagType == UniversalTagNumber.ObjectIdentifier)
				{
					return delegate(object value, AsnWriter writer)
					{
						writer.WriteObjectIdentifier(tag, (string)value);
					};
				}
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteCharacterString(tag, fieldData.TagType.Value, (string)value);
				};
			}
			if (typeT == typeof(ReadOnlyMemory<byte>) && !fieldData.IsCollection)
			{
				if (fieldData.IsAny)
				{
					if (fieldData.SpecifiedTag && !fieldData.HasExplicitTag)
					{
						return delegate(object value, AsnWriter writer)
						{
							ReadOnlyMemory<byte> preEncodedValue = (ReadOnlyMemory<byte>)value;
							if (!Asn1Tag.TryParse(preEncodedValue.Span, out var tag2, out var _) || tag2.AsPrimitive() != fieldData.ExpectedTag.AsPrimitive())
							{
								throw new CryptographicException("ASN1 corrupted data.");
							}
							writer.WriteEncodedValue(preEncodedValue);
						};
					}
					return delegate(object value, AsnWriter writer)
					{
						writer.WriteEncodedValue((ReadOnlyMemory<byte>)value);
					};
				}
				if (fieldData.TagType == UniversalTagNumber.BitString)
				{
					return delegate(object value, AsnWriter writer)
					{
						writer.WriteBitString(tag, ((ReadOnlyMemory<byte>)value).Span);
					};
				}
				if (fieldData.TagType == UniversalTagNumber.OctetString)
				{
					return delegate(object value, AsnWriter writer)
					{
						writer.WriteOctetString(tag, ((ReadOnlyMemory<byte>)value).Span);
					};
				}
				if (fieldData.TagType == UniversalTagNumber.Integer)
				{
					return delegate(object value, AsnWriter writer)
					{
						writer.WriteInteger(tag, ((ReadOnlyMemory<byte>)value).Span);
					};
				}
				throw new CryptographicException();
			}
			if (typeT == typeof(Oid))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteObjectIdentifier(fieldData.ExpectedTag, (Oid)value);
				};
			}
			if (typeT.IsArray)
			{
				if (typeT.GetArrayRank() != 1)
				{
					throw new AsnSerializationConstraintException(global::SR.Format("Type '{0}' cannot be serialized or deserialized because it is a multi-dimensional array.", typeT.FullName));
				}
				Type elementType = typeT.GetElementType();
				if (elementType.IsArray)
				{
					throw new AsnSerializationConstraintException(global::SR.Format("Type '{0}' cannot be serialized or deserialized because it is an array of arrays.", typeT.FullName));
				}
				Serializer serializer = GetSerializer(elementType, null);
				if (fieldData.TagType == UniversalTagNumber.Set)
				{
					return delegate(object value, AsnWriter writer)
					{
						writer.PushSetOf(tag);
						foreach (object item in (Array)value)
						{
							serializer(item, writer);
						}
						writer.PopSetOf(tag);
					};
				}
				return delegate(object value, AsnWriter writer)
				{
					writer.PushSequence(tag);
					foreach (object item2 in (Array)value)
					{
						serializer(item2, writer);
					}
					writer.PopSequence(tag);
				};
			}
			if (typeT == typeof(DateTimeOffset))
			{
				if (fieldData.TagType == UniversalTagNumber.UtcTime)
				{
					return delegate(object value, AsnWriter writer)
					{
						writer.WriteUtcTime(tag, (DateTimeOffset)value);
					};
				}
				if (fieldData.TagType == UniversalTagNumber.GeneralizedTime)
				{
					return delegate(object value, AsnWriter writer)
					{
						writer.WriteGeneralizedTime(tag, (DateTimeOffset)value, fieldData.DisallowGeneralizedTimeFractions.Value);
					};
				}
				throw new CryptographicException();
			}
			if (typeT == typeof(BigInteger))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(tag, (BigInteger)value);
				};
			}
			if (typeT.IsLayoutSequential)
			{
				if (flag)
				{
					return delegate(object value, AsnWriter writer)
					{
						SerializeChoice(typeT, value, writer);
					};
				}
				if (fieldData.TagType == UniversalTagNumber.Sequence)
				{
					return delegate(object value, AsnWriter writer)
					{
						SerializeCustomType(typeT, value, writer, tag);
					};
				}
			}
			throw new AsnSerializationConstraintException(global::SR.Format("Could not determine how to serialize or deserialize type '{0}'.", typeT.FullName));
		}

		private static Deserializer GetDeserializer(Type typeT, FieldInfo fieldInfo)
		{
			SerializerFieldData fieldData;
			Deserializer deserializer = GetSimpleDeserializer(typeT, fieldInfo, out fieldData);
			if (fieldData.HasExplicitTag)
			{
				deserializer = ExplicitValueDeserializer(deserializer, fieldData.ExpectedTag);
			}
			if (fieldData.IsOptional || fieldData.DefaultContents != null)
			{
				Asn1Tag? expectedTag = null;
				if (fieldData.SpecifiedTag || fieldData.TagType.HasValue)
				{
					expectedTag = fieldData.ExpectedTag;
				}
				deserializer = DefaultValueDeserializer(deserializer, fieldData.IsOptional, fieldData.DefaultContents, expectedTag);
			}
			return deserializer;
		}

		private static Deserializer GetSimpleDeserializer(Type typeT, FieldInfo fieldInfo, out SerializerFieldData fieldData)
		{
			if (!typeT.IsSealed || typeT.ContainsGenericParameters)
			{
				throw new AsnSerializationConstraintException(global::SR.Format("Type '{0}' cannot be serialized or deserialized because it is not sealed or has unbound generic parameters.", typeT.FullName));
			}
			GetFieldInfo(typeT, fieldInfo, out fieldData);
			SerializerFieldData localFieldData = fieldData;
			typeT = UnpackIfNullable(typeT);
			if (fieldData.IsAny)
			{
				if (typeT == typeof(ReadOnlyMemory<byte>))
				{
					Asn1Tag matchTag = fieldData.ExpectedTag;
					if (fieldData.HasExplicitTag || !fieldData.SpecifiedTag)
					{
						return (AsnReader reader) => reader.GetEncodedValue();
					}
					return delegate(AsnReader reader)
					{
						Asn1Tag asn1Tag = reader.PeekTag();
						if (matchTag.TagClass != asn1Tag.TagClass || matchTag.TagValue != asn1Tag.TagValue)
						{
							throw new CryptographicException("ASN1 corrupted data.");
						}
						return reader.GetEncodedValue();
					};
				}
				throw new AsnSerializationConstraintException(global::SR.Format("Could not determine how to serialize or deserialize type '{0}'.", typeT.FullName));
			}
			if (GetChoiceAttribute(typeT) != null)
			{
				return (AsnReader reader) => DeserializeChoice(reader, typeT);
			}
			Asn1Tag expectedTag = (fieldData.HasExplicitTag ? new Asn1Tag(fieldData.TagType.Value) : fieldData.ExpectedTag);
			if (typeT.IsPrimitive)
			{
				return GetPrimitiveDeserializer(typeT, expectedTag);
			}
			if (typeT.IsEnum)
			{
				if (typeT.GetCustomAttributes(typeof(FlagsAttribute), inherit: false).Length != 0)
				{
					return (AsnReader reader) => reader.GetNamedBitListValue(expectedTag, typeT);
				}
				return (AsnReader reader) => reader.GetEnumeratedValue(expectedTag, typeT);
			}
			if (typeT == typeof(string))
			{
				if (fieldData.TagType == UniversalTagNumber.ObjectIdentifier)
				{
					return (AsnReader reader) => reader.ReadObjectIdentifierAsString(expectedTag);
				}
				return (AsnReader reader) => reader.GetCharacterString(expectedTag, localFieldData.TagType.Value);
			}
			if (typeT == typeof(ReadOnlyMemory<byte>) && !fieldData.IsCollection)
			{
				if (fieldData.TagType == UniversalTagNumber.BitString)
				{
					return delegate(AsnReader reader)
					{
						if (reader.TryGetPrimitiveBitStringValue(expectedTag, out var unusedBitCount, out var value))
						{
							return value;
						}
						int length = reader.PeekEncodedValue().Length;
						byte[] array = ArrayPool<byte>.Shared.Rent(length);
						try
						{
							if (!reader.TryCopyBitStringBytes(expectedTag, array, out unusedBitCount, out var bytesWritten))
							{
								throw new CryptographicException();
							}
							return new ReadOnlyMemory<byte>(array.AsSpan(0, bytesWritten).ToArray());
						}
						finally
						{
							Array.Clear(array, 0, length);
							ArrayPool<byte>.Shared.Return(array);
						}
					};
				}
				if (fieldData.TagType == UniversalTagNumber.OctetString)
				{
					return delegate(AsnReader reader)
					{
						if (reader.TryGetPrimitiveOctetStringBytes(expectedTag, out var contents))
						{
							return contents;
						}
						int length = reader.PeekEncodedValue().Length;
						byte[] array = ArrayPool<byte>.Shared.Rent(length);
						try
						{
							if (!reader.TryCopyOctetStringBytes(expectedTag, array, out var bytesWritten))
							{
								throw new CryptographicException();
							}
							return new ReadOnlyMemory<byte>(array.AsSpan(0, bytesWritten).ToArray());
						}
						finally
						{
							Array.Clear(array, 0, length);
							ArrayPool<byte>.Shared.Return(array);
						}
					};
				}
				if (fieldData.TagType == UniversalTagNumber.Integer)
				{
					return (AsnReader reader) => reader.GetIntegerBytes(expectedTag);
				}
				throw new CryptographicException();
			}
			if (typeT == typeof(Oid))
			{
				bool skipFriendlyName = fieldData.PopulateOidFriendlyName != true;
				return (AsnReader reader) => reader.ReadObjectIdentifier(expectedTag, skipFriendlyName);
			}
			if (typeT.IsArray)
			{
				if (typeT.GetArrayRank() != 1)
				{
					throw new AsnSerializationConstraintException(global::SR.Format("Type '{0}' cannot be serialized or deserialized because it is a multi-dimensional array.", typeT.FullName));
				}
				Type baseType = typeT.GetElementType();
				if (baseType.IsArray)
				{
					throw new AsnSerializationConstraintException(global::SR.Format("Type '{0}' cannot be serialized or deserialized because it is an array of arrays.", typeT.FullName));
				}
				return delegate(AsnReader reader)
				{
					LinkedList<object> linkedList = new LinkedList<object>();
					AsnReader asnReader = ((localFieldData.TagType != UniversalTagNumber.Set) ? reader.ReadSequence(expectedTag) : reader.ReadSetOf(expectedTag));
					Deserializer deserializer = GetDeserializer(baseType, null);
					while (asnReader.HasData)
					{
						LinkedListNode<object> node = new LinkedListNode<object>(deserializer(asnReader));
						linkedList.AddLast(node);
					}
					object[] array = Enumerable.ToArray(linkedList);
					Array array2 = Array.CreateInstance(baseType, array.Length);
					Array.Copy(array, array2, array.Length);
					return array2;
				};
			}
			if (typeT == typeof(DateTimeOffset))
			{
				if (fieldData.TagType == UniversalTagNumber.UtcTime)
				{
					if (fieldData.TwoDigitYearMax.HasValue)
					{
						return (AsnReader reader) => reader.GetUtcTime(expectedTag, localFieldData.TwoDigitYearMax.Value);
					}
					return (AsnReader reader) => reader.GetUtcTime(expectedTag);
				}
				if (fieldData.TagType == UniversalTagNumber.GeneralizedTime)
				{
					bool disallowFractions = fieldData.DisallowGeneralizedTimeFractions.Value;
					return (AsnReader reader) => reader.GetGeneralizedTime(expectedTag, disallowFractions);
				}
				throw new CryptographicException();
			}
			if (typeT == typeof(BigInteger))
			{
				return (AsnReader reader) => reader.GetInteger(expectedTag);
			}
			if (typeT.IsLayoutSequential && fieldData.TagType == UniversalTagNumber.Sequence)
			{
				return (AsnReader reader) => DeserializeCustomType(reader, typeT, expectedTag);
			}
			throw new AsnSerializationConstraintException(global::SR.Format("Could not determine how to serialize or deserialize type '{0}'.", typeT.FullName));
		}

		private static object DefaultValue(byte[] defaultContents, Deserializer valueDeserializer)
		{
			try
			{
				AsnReader asnReader = new AsnReader(defaultContents, AsnEncodingRules.DER);
				object result = valueDeserializer(asnReader);
				if (asnReader.HasData)
				{
					throw new AsnSerializerInvalidDefaultException();
				}
				return result;
			}
			catch (AsnSerializerInvalidDefaultException)
			{
				throw;
			}
			catch (CryptographicException innerException)
			{
				throw new AsnSerializerInvalidDefaultException(innerException);
			}
		}

		private static void GetFieldInfo(Type typeT, FieldInfo fieldInfo, out SerializerFieldData serializerFieldData)
		{
			serializerFieldData = default(SerializerFieldData);
			object[] array = fieldInfo?.GetCustomAttributes(typeof(AsnTypeAttribute), inherit: false) ?? Array.Empty<object>();
			if (array.Length > 1)
			{
				throw new AsnSerializationConstraintException(global::SR.Format(fieldInfo.Name, fieldInfo.DeclaringType.FullName, typeof(AsnTypeAttribute).FullName));
			}
			Type type = UnpackIfNullable(typeT);
			if (array.Length == 1)
			{
				object obj = array[0];
				serializerFieldData.WasCustomized = true;
				Type[] array2;
				if (obj is AnyValueAttribute)
				{
					serializerFieldData.IsAny = true;
					array2 = new Type[1] { typeof(ReadOnlyMemory<byte>) };
				}
				else if (obj is IntegerAttribute)
				{
					array2 = new Type[1] { typeof(ReadOnlyMemory<byte>) };
					serializerFieldData.TagType = UniversalTagNumber.Integer;
				}
				else if (obj is BitStringAttribute)
				{
					array2 = new Type[1] { typeof(ReadOnlyMemory<byte>) };
					serializerFieldData.TagType = UniversalTagNumber.BitString;
				}
				else if (obj is OctetStringAttribute)
				{
					array2 = new Type[1] { typeof(ReadOnlyMemory<byte>) };
					serializerFieldData.TagType = UniversalTagNumber.OctetString;
				}
				else if (obj is ObjectIdentifierAttribute objectIdentifierAttribute)
				{
					serializerFieldData.PopulateOidFriendlyName = objectIdentifierAttribute.PopulateFriendlyName;
					array2 = new Type[2]
					{
						typeof(Oid),
						typeof(string)
					};
					serializerFieldData.TagType = UniversalTagNumber.ObjectIdentifier;
					if (objectIdentifierAttribute.PopulateFriendlyName && type == typeof(string))
					{
						throw new AsnSerializationConstraintException(global::SR.Format("Field '{0}' on type '{1}' has [ObjectIdentifier].PopulateFriendlyName set to true, which is not applicable to a string.  Change the field to '{2}' or set PopulateFriendlyName to false.", fieldInfo.Name, fieldInfo.DeclaringType.FullName, typeof(Oid).FullName));
					}
				}
				else if (obj is BMPStringAttribute)
				{
					array2 = new Type[1] { typeof(string) };
					serializerFieldData.TagType = UniversalTagNumber.BMPString;
				}
				else if (obj is IA5StringAttribute)
				{
					array2 = new Type[1] { typeof(string) };
					serializerFieldData.TagType = UniversalTagNumber.IA5String;
				}
				else if (obj is UTF8StringAttribute)
				{
					array2 = new Type[1] { typeof(string) };
					serializerFieldData.TagType = UniversalTagNumber.UTF8String;
				}
				else if (obj is PrintableStringAttribute)
				{
					array2 = new Type[1] { typeof(string) };
					serializerFieldData.TagType = UniversalTagNumber.PrintableString;
				}
				else if (obj is VisibleStringAttribute)
				{
					array2 = new Type[1] { typeof(string) };
					serializerFieldData.TagType = UniversalTagNumber.VisibleString;
				}
				else if (obj is SequenceOfAttribute)
				{
					serializerFieldData.IsCollection = true;
					array2 = null;
					serializerFieldData.TagType = UniversalTagNumber.Sequence;
				}
				else if (obj is SetOfAttribute)
				{
					serializerFieldData.IsCollection = true;
					array2 = null;
					serializerFieldData.TagType = UniversalTagNumber.Set;
				}
				else if (obj is UtcTimeAttribute utcTimeAttribute)
				{
					array2 = new Type[1] { typeof(DateTimeOffset) };
					serializerFieldData.TagType = UniversalTagNumber.UtcTime;
					if (utcTimeAttribute.TwoDigitYearMax != 0)
					{
						serializerFieldData.TwoDigitYearMax = utcTimeAttribute.TwoDigitYearMax;
						if (serializerFieldData.TwoDigitYearMax < 99)
						{
							throw new AsnSerializationConstraintException(global::SR.Format("Field '{0}' on type '{1}' has a [UtcTime] TwoDigitYearMax value ({2}) smaller than the minimum (99).", fieldInfo.Name, fieldInfo.DeclaringType.FullName, serializerFieldData.TwoDigitYearMax));
						}
					}
				}
				else
				{
					if (!(obj is GeneralizedTimeAttribute generalizedTimeAttribute))
					{
						throw new CryptographicException();
					}
					array2 = new Type[1] { typeof(DateTimeOffset) };
					serializerFieldData.TagType = UniversalTagNumber.GeneralizedTime;
					serializerFieldData.DisallowGeneralizedTimeFractions = generalizedTimeAttribute.DisallowFractions;
				}
				if (!serializerFieldData.IsCollection && Array.IndexOf(array2, type) < 0)
				{
					throw new AsnSerializationConstraintException(global::SR.Format("Field '{0}' of type '{1}' has an effective type of '{2}' when one of ({3}) was expected.", fieldInfo.Name, fieldInfo.DeclaringType.Namespace, type.FullName, string.Join(", ", array2.Select((Type t) => t.FullName))));
				}
			}
			serializerFieldData.DefaultContents = (fieldInfo?.GetCustomAttribute<DefaultValueAttribute>(inherit: false))?.EncodedBytes;
			if (!serializerFieldData.TagType.HasValue && !serializerFieldData.IsAny)
			{
				if (type == typeof(bool))
				{
					serializerFieldData.TagType = UniversalTagNumber.Boolean;
				}
				else if (type == typeof(sbyte) || type == typeof(byte) || type == typeof(short) || type == typeof(ushort) || type == typeof(int) || type == typeof(uint) || type == typeof(long) || type == typeof(ulong) || type == typeof(BigInteger))
				{
					serializerFieldData.TagType = UniversalTagNumber.Integer;
				}
				else if (type.IsLayoutSequential)
				{
					serializerFieldData.TagType = UniversalTagNumber.Sequence;
				}
				else
				{
					if (type == typeof(ReadOnlyMemory<byte>) || type == typeof(string) || type == typeof(DateTimeOffset))
					{
						throw new AsnAmbiguousFieldTypeException(fieldInfo, type);
					}
					if (type == typeof(Oid))
					{
						serializerFieldData.TagType = UniversalTagNumber.ObjectIdentifier;
					}
					else if (type.IsArray)
					{
						serializerFieldData.TagType = UniversalTagNumber.Sequence;
					}
					else if (type.IsEnum)
					{
						if (typeT.GetCustomAttributes(typeof(FlagsAttribute), inherit: false).Length != 0)
						{
							serializerFieldData.TagType = UniversalTagNumber.BitString;
						}
						else
						{
							serializerFieldData.TagType = UniversalTagNumber.Enumerated;
						}
					}
					else if (fieldInfo != null)
					{
						throw new AsnSerializationConstraintException();
					}
				}
			}
			serializerFieldData.IsOptional = fieldInfo?.GetCustomAttribute<OptionalValueAttribute>(inherit: false) != null;
			if (serializerFieldData.IsOptional && !CanBeNull(typeT))
			{
				throw new AsnSerializationConstraintException(global::SR.Format("Field '{0}' on type '{1}' is declared [OptionalValue], but it can not be assigned a null value.", fieldInfo.Name, fieldInfo.DeclaringType.FullName));
			}
			bool flag = GetChoiceAttribute(typeT) != null;
			ExpectedTagAttribute expectedTagAttribute = fieldInfo?.GetCustomAttribute<ExpectedTagAttribute>(inherit: false);
			if (expectedTagAttribute != null)
			{
				if (flag && !expectedTagAttribute.ExplicitTag)
				{
					throw new AsnSerializationConstraintException(global::SR.Format("Field '{0}' on type '{1}' has specified an implicit tag value via [ExpectedTag] for [Choice] type '{2}'. ExplicitTag must be true, or the [ExpectedTag] attribute removed.", fieldInfo.Name, fieldInfo.DeclaringType.FullName, typeT.FullName));
				}
				serializerFieldData.ExpectedTag = new Asn1Tag(expectedTagAttribute.TagClass, expectedTagAttribute.TagValue);
				serializerFieldData.HasExplicitTag = expectedTagAttribute.ExplicitTag;
				serializerFieldData.SpecifiedTag = true;
			}
			else
			{
				if (flag)
				{
					serializerFieldData.TagType = null;
				}
				serializerFieldData.SpecifiedTag = false;
				serializerFieldData.HasExplicitTag = false;
				serializerFieldData.ExpectedTag = new Asn1Tag(serializerFieldData.TagType.GetValueOrDefault());
			}
		}

		private static Type UnpackIfNullable(Type typeT)
		{
			return Nullable.GetUnderlyingType(typeT) ?? typeT;
		}

		private static Deserializer GetPrimitiveDeserializer(Type typeT, Asn1Tag tag)
		{
			if (typeT == typeof(bool))
			{
				return (AsnReader reader) => reader.ReadBoolean(tag);
			}
			if (typeT == typeof(int))
			{
				return TryOrFail(delegate(AsnReader reader, out int value)
				{
					return reader.TryReadInt32(tag, out value);
				});
			}
			if (typeT == typeof(uint))
			{
				return TryOrFail(delegate(AsnReader reader, out uint value)
				{
					return reader.TryReadUInt32(tag, out value);
				});
			}
			if (typeT == typeof(short))
			{
				return TryOrFail(delegate(AsnReader reader, out short value)
				{
					return reader.TryReadInt16(tag, out value);
				});
			}
			if (typeT == typeof(ushort))
			{
				return TryOrFail(delegate(AsnReader reader, out ushort value)
				{
					return reader.TryReadUInt16(tag, out value);
				});
			}
			if (typeT == typeof(byte))
			{
				return TryOrFail(delegate(AsnReader reader, out byte value)
				{
					return reader.TryReadUInt8(tag, out value);
				});
			}
			if (typeT == typeof(sbyte))
			{
				return TryOrFail(delegate(AsnReader reader, out sbyte value)
				{
					return reader.TryReadInt8(tag, out value);
				});
			}
			if (typeT == typeof(long))
			{
				return TryOrFail(delegate(AsnReader reader, out long value)
				{
					return reader.TryReadInt64(tag, out value);
				});
			}
			if (typeT == typeof(ulong))
			{
				return TryOrFail(delegate(AsnReader reader, out ulong value)
				{
					return reader.TryReadUInt64(tag, out value);
				});
			}
			throw new AsnSerializationConstraintException(global::SR.Format("Could not determine how to serialize or deserialize type '{0}'.", typeT.FullName));
		}

		private static Serializer GetPrimitiveSerializer(Type typeT, Asn1Tag primitiveTag)
		{
			if (typeT == typeof(bool))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteBoolean(primitiveTag, (bool)value);
				};
			}
			if (typeT == typeof(int))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (int)value);
				};
			}
			if (typeT == typeof(uint))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (uint)value);
				};
			}
			if (typeT == typeof(short))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (short)value);
				};
			}
			if (typeT == typeof(ushort))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (ushort)value);
				};
			}
			if (typeT == typeof(byte))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (byte)value);
				};
			}
			if (typeT == typeof(sbyte))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (sbyte)value);
				};
			}
			if (typeT == typeof(long))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (long)value);
				};
			}
			if (typeT == typeof(ulong))
			{
				return delegate(object value, AsnWriter writer)
				{
					writer.WriteInteger(primitiveTag, (ulong)value);
				};
			}
			throw new AsnSerializationConstraintException(global::SR.Format("Could not determine how to serialize or deserialize type '{0}'.", typeT.FullName));
		}

		public static T Deserialize<T>(ReadOnlyMemory<byte> source, AsnEncodingRules ruleSet)
		{
			Deserializer deserializer = GetDeserializer(typeof(T), null);
			AsnReader asnReader = new AsnReader(source, ruleSet);
			T result = (T)deserializer(asnReader);
			asnReader.ThrowIfNotEmpty();
			return result;
		}

		public static T Deserialize<T>(ReadOnlyMemory<byte> source, AsnEncodingRules ruleSet, out int bytesRead)
		{
			Deserializer deserializer = GetDeserializer(typeof(T), null);
			AsnReader asnReader = new AsnReader(source, ruleSet);
			ReadOnlyMemory<byte> readOnlyMemory = asnReader.PeekEncodedValue();
			T result = (T)deserializer(asnReader);
			bytesRead = readOnlyMemory.Length;
			return result;
		}

		public static AsnWriter Serialize<T>(T value, AsnEncodingRules ruleSet)
		{
			AsnWriter asnWriter = new AsnWriter(ruleSet);
			try
			{
				Serialize(value, asnWriter);
				return asnWriter;
			}
			catch
			{
				asnWriter.Dispose();
				throw;
			}
		}

		public static void Serialize<T>(T value, AsnWriter existingWriter)
		{
			if (existingWriter == null)
			{
				throw new ArgumentNullException("existingWriter");
			}
			GetSerializer(typeof(T), null)(value, existingWriter);
		}
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class ExpectedTagAttribute : Attribute
	{
		public TagClass TagClass { get; }

		public int TagValue { get; }

		public bool ExplicitTag { get; set; }

		public ExpectedTagAttribute(int tagValue)
			: this(TagClass.ContextSpecific, tagValue)
		{
		}

		public ExpectedTagAttribute(TagClass tagClass, int tagValue)
		{
			TagClass = tagClass;
			TagValue = tagValue;
		}
	}
	internal abstract class AsnTypeAttribute : Attribute
	{
		internal AsnTypeAttribute()
		{
		}
	}
	internal abstract class AsnEncodingRuleAttribute : Attribute
	{
		internal AsnEncodingRuleAttribute()
		{
		}
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class OctetStringAttribute : AsnTypeAttribute
	{
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class BitStringAttribute : AsnTypeAttribute
	{
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class AnyValueAttribute : AsnTypeAttribute
	{
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class ObjectIdentifierAttribute : AsnTypeAttribute
	{
		public bool PopulateFriendlyName { get; set; }
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class BMPStringAttribute : AsnTypeAttribute
	{
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class IA5StringAttribute : AsnTypeAttribute
	{
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class UTF8StringAttribute : AsnTypeAttribute
	{
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class PrintableStringAttribute : AsnTypeAttribute
	{
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class VisibleStringAttribute : AsnTypeAttribute
	{
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class SequenceOfAttribute : AsnTypeAttribute
	{
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class SetOfAttribute : AsnTypeAttribute
	{
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class IntegerAttribute : AsnTypeAttribute
	{
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class UtcTimeAttribute : AsnTypeAttribute
	{
		public int TwoDigitYearMax { get; set; }
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class GeneralizedTimeAttribute : AsnTypeAttribute
	{
		public bool DisallowFractions { get; set; }
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class OptionalValueAttribute : AsnEncodingRuleAttribute
	{
	}
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class DefaultValueAttribute : AsnEncodingRuleAttribute
	{
		internal byte[] EncodedBytes { get; }

		public ReadOnlyMemory<byte> EncodedValue => EncodedBytes;

		public DefaultValueAttribute(params byte[] encodedValue)
		{
			EncodedBytes = encodedValue;
		}
	}
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	internal sealed class ChoiceAttribute : Attribute
	{
		public bool AllowNull { get; set; }
	}
	internal enum AsnEncodingRules
	{
		BER,
		CER,
		DER
	}
	internal enum TagClass : byte
	{
		Universal = 0,
		Application = 64,
		ContextSpecific = 128,
		Private = 192
	}
	internal enum UniversalTagNumber
	{
		EndOfContents = 0,
		Boolean = 1,
		Integer = 2,
		BitString = 3,
		OctetString = 4,
		Null = 5,
		ObjectIdentifier = 6,
		ObjectDescriptor = 7,
		External = 8,
		InstanceOf = 8,
		Real = 9,
		Enumerated = 10,
		Embedded = 11,
		UTF8String = 12,
		RelativeObjectIdentifier = 13,
		Time = 14,
		Sequence = 16,
		SequenceOf = 16,
		Set = 17,
		SetOf = 17,
		NumericString = 18,
		PrintableString = 19,
		TeletexString = 20,
		T61String = 20,
		VideotexString = 21,
		IA5String = 22,
		UtcTime = 23,
		GeneralizedTime = 24,
		GraphicString = 25,
		VisibleString = 26,
		ISO646String = 26,
		GeneralString = 27,
		UniversalString = 28,
		UnrestrictedCharacterString = 29,
		BMPString = 30,
		Date = 31,
		TimeOfDay = 32,
		DateTime = 33,
		Duration = 34,
		ObjectIdentifierIRI = 35,
		RelativeObjectIdentifierIRI = 36
	}
	internal struct Asn1Tag : IEquatable<Asn1Tag>
	{
		private const byte ClassMask = 192;

		private const byte ConstructedMask = 32;

		private const byte ControlMask = 224;

		private const byte TagNumberMask = 31;

		internal static readonly Asn1Tag EndOfContents = new Asn1Tag((byte)0, 0);

		internal static readonly Asn1Tag Boolean = new Asn1Tag((byte)0, 1);

		internal static readonly Asn1Tag Integer = new Asn1Tag((byte)0, 2);

		internal static readonly Asn1Tag PrimitiveBitString = new Asn1Tag((byte)0, 3);

		internal static readonly Asn1Tag ConstructedBitString = new Asn1Tag(32, 3);

		internal static readonly Asn1Tag PrimitiveOctetString = new Asn1Tag((byte)0, 4);

		internal static readonly Asn1Tag ConstructedOctetString = new Asn1Tag(32, 4);

		internal static readonly Asn1Tag Null = new Asn1Tag((byte)0, 5);

		internal static readonly Asn1Tag ObjectIdentifier = new Asn1Tag((byte)0, 6);

		internal static readonly Asn1Tag Enumerated = new Asn1Tag((byte)0, 10);

		internal static readonly Asn1Tag Sequence = new Asn1Tag(32, 16);

		internal static readonly Asn1Tag SetOf = new Asn1Tag(32, 17);

		internal static readonly Asn1Tag UtcTime = new Asn1Tag((byte)0, 23);

		internal static readonly Asn1Tag GeneralizedTime = new Asn1Tag((byte)0, 24);

		private readonly byte _controlFlags;

		private readonly int _tagValue;

		public TagClass TagClass => (TagClass)(_controlFlags & 0xC0);

		public bool IsConstructed => (_controlFlags & 0x20) != 0;

		public int TagValue => _tagValue;

		private Asn1Tag(byte controlFlags, int tagValue)
		{
			_controlFlags = (byte)(controlFlags & 0xE0);
			_tagValue = tagValue;
		}

		public Asn1Tag(UniversalTagNumber universalTagNumber, bool isConstructed = false)
			: this((byte)(isConstructed ? 32 : 0), (int)universalTagNumber)
		{
			if (universalTagNumber < UniversalTagNumber.EndOfContents || universalTagNumber > UniversalTagNumber.RelativeObjectIdentifierIRI || universalTagNumber == (UniversalTagNumber)15)
			{
				throw new ArgumentOutOfRangeException("universalTagNumber");
			}
		}

		public Asn1Tag(TagClass tagClass, int tagValue, bool isConstructed = false)
			: this((byte)((uint)tagClass | (uint)(isConstructed ? 32 : 0)), tagValue)
		{
			if ((int)tagClass < 0 || (int)tagClass > 192)
			{
				throw new ArgumentOutOfRangeException("tagClass");
			}
			if (tagValue < 0)
			{
				throw new ArgumentOutOfRangeException("tagValue");
			}
		}

		public Asn1Tag AsConstructed()
		{
			return new Asn1Tag((byte)(_controlFlags | 0x20), _tagValue);
		}

		public Asn1Tag AsPrimitive()
		{
			return new Asn1Tag((byte)(_controlFlags & -33), _tagValue);
		}

		public static bool TryParse(ReadOnlySpan<byte> source, out Asn1Tag tag, out int bytesRead)
		{
			tag = default(Asn1Tag);
			bytesRead = 0;
			if (source.IsEmpty)
			{
				return false;
			}
			byte b = source[bytesRead];
			bytesRead++;
			uint num = (uint)(b & 0x1F);
			if (num == 31)
			{
				num = 0u;
				byte b2;
				do
				{
					if (source.Length <= bytesRead)
					{
						bytesRead = 0;
						return false;
					}
					b2 = source[bytesRead];
					byte b3 = (byte)(b2 & 0x7F);
					bytesRead++;
					if (num >= 33554432)
					{
						bytesRead = 0;
						return false;
					}
					num <<= 7;
					num |= b3;
					if (num == 0)
					{
						bytesRead = 0;
						return false;
					}
				}
				while ((b2 & 0x80) == 128);
				if (num <= 30)
				{
					bytesRead = 0;
					return false;
				}
				if (num > int.MaxValue)
				{
					bytesRead = 0;
					return false;
				}
			}
			tag = new Asn1Tag(b, (int)num);
			return true;
		}

		public int CalculateEncodedSize()
		{
			if (TagValue < 31)
			{
				return 1;
			}
			if (TagValue <= 127)
			{
				return 2;
			}
			if (TagValue <= 16383)
			{
				return 3;
			}
			if (TagValue <= 2097151)
			{
				return 4;
			}
			if (TagValue <= 268435455)
			{
				return 5;
			}
			return 6;
		}

		public bool TryWrite(Span<byte> destination, out int bytesWritten)
		{
			int num = CalculateEncodedSize();
			if (destination.Length < num)
			{
				bytesWritten = 0;
				return false;
			}
			if (num == 1)
			{
				byte b = (byte)(_controlFlags | TagValue);
				destination[0] = b;
				bytesWritten = 1;
				return true;
			}
			byte b2 = (byte)(_controlFlags | 0x1F);
			destination[0] = b2;
			int num2 = TagValue;
			int num3 = num - 1;
			while (num2 > 0)
			{
				int num4 = num2 & 0x7F;
				if (num2 != TagValue)
				{
					num4 |= 0x80;
				}
				destination[num3] = (byte)num4;
				num2 >>= 7;
				num3--;
			}
			bytesWritten = num;
			return true;
		}

		public bool Equals(Asn1Tag other)
		{
			if (_controlFlags == other._controlFlags)
			{
				return _tagValue == other._tagValue;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is Asn1Tag)
			{
				return Equals((Asn1Tag)obj);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (_controlFlags << 24) ^ _tagValue;
		}

		public static bool operator ==(Asn1Tag left, Asn1Tag right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Asn1Tag left, Asn1Tag right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			string text = ((TagClass != TagClass.Universal) ? (TagClass.ToString() + "-" + TagValue) : ((UniversalTagNumber)TagValue/*cast due to .constrained prefix*/).ToString());
			if (IsConstructed)
			{
				return "Constructed " + text;
			}
			return text;
		}
	}
	internal static class AsnCharacterStringEncodings
	{
		private static readonly Encoding s_utf8Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

		private static readonly Encoding s_bmpEncoding = new BMPEncoding();

		private static readonly Encoding s_ia5Encoding = new IA5Encoding();

		private static readonly Encoding s_visibleStringEncoding = new VisibleStringEncoding();

		private static readonly Encoding s_printableStringEncoding = new PrintableStringEncoding();

		internal static Encoding GetEncoding(UniversalTagNumber encodingType)
		{
			return encodingType switch
			{
				UniversalTagNumber.UTF8String => s_utf8Encoding, 
				UniversalTagNumber.PrintableString => s_printableStringEncoding, 
				UniversalTagNumber.IA5String => s_ia5Encoding, 
				UniversalTagNumber.VisibleString => s_visibleStringEncoding, 
				UniversalTagNumber.BMPString => s_bmpEncoding, 
				_ => throw new ArgumentOutOfRangeException("encodingType", encodingType, null), 
			};
		}
	}
	internal abstract class SpanBasedEncoding : Encoding
	{
		protected SpanBasedEncoding()
			: base(0, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback)
		{
		}

		protected abstract int GetBytes(ReadOnlySpan<char> chars, Span<byte> bytes, bool write);

		protected abstract int GetChars(ReadOnlySpan<byte> bytes, Span<char> chars, bool write);

		public override int GetByteCount(char[] chars, int index, int count)
		{
			return GetByteCount(new ReadOnlySpan<char>(chars, index, count));
		}

		public unsafe override int GetByteCount(char* chars, int count)
		{
			return GetByteCount(new ReadOnlySpan<char>(chars, count));
		}

		public override int GetByteCount(string s)
		{
			return GetByteCount(s.AsSpan());
		}

		public new int GetByteCount(ReadOnlySpan<char> chars)
		{
			return GetBytes(chars, Span<byte>.Empty, write: false);
		}

		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return GetBytes(new ReadOnlySpan<char>(chars, charIndex, charCount), new Span<byte>(bytes, byteIndex, bytes.Length - byteIndex), write: true);
		}

		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			return GetBytes(new ReadOnlySpan<char>(chars, charCount), new Span<byte>(bytes, byteCount), write: true);
		}

		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return GetCharCount(new ReadOnlySpan<byte>(bytes, index, count));
		}

		public unsafe override int GetCharCount(byte* bytes, int count)
		{
			return GetCharCount(new ReadOnlySpan<byte>(bytes, count));
		}

		public new int GetCharCount(ReadOnlySpan<byte> bytes)
		{
			return GetChars(bytes, Span<char>.Empty, write: false);
		}

		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return GetChars(new ReadOnlySpan<byte>(bytes, byteIndex, byteCount), new Span<char>(chars, charIndex, chars.Length - charIndex), write: true);
		}

		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			return GetChars(new ReadOnlySpan<byte>(bytes, byteCount), new Span<char>(chars, charCount), write: true);
		}
	}
	internal class IA5Encoding : RestrictedAsciiStringEncoding
	{
		internal IA5Encoding()
			: base(0, 127)
		{
		}
	}
	internal class VisibleStringEncoding : RestrictedAsciiStringEncoding
	{
		internal VisibleStringEncoding()
			: base(32, 126)
		{
		}
	}
	internal class PrintableStringEncoding : RestrictedAsciiStringEncoding
	{
		internal PrintableStringEncoding()
			: base("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 '()+,-./:=?")
		{
		}
	}
	internal abstract class RestrictedAsciiStringEncoding : SpanBasedEncoding
	{
		private readonly bool[] _isAllowed;

		protected RestrictedAsciiStringEncoding(byte minCharAllowed, byte maxCharAllowed)
		{
			bool[] array = new bool[128];
			for (byte b = minCharAllowed; b <= maxCharAllowed; b++)
			{
				array[b] = true;
			}
			_isAllowed = array;
		}

		protected RestrictedAsciiStringEncoding(IEnumerable<char> allowedChars)
		{
			bool[] array = new bool[127];
			foreach (char allowedChar in allowedChars)
			{
				if (allowedChar >= array.Length)
				{
					throw new ArgumentOutOfRangeException("allowedChars");
				}
				array[(uint)allowedChar] = true;
			}
			_isAllowed = array;
		}

		public override int GetMaxByteCount(int charCount)
		{
			return charCount;
		}

		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}

		protected override int GetBytes(ReadOnlySpan<char> chars, Span<byte> bytes, bool write)
		{
			if (chars.IsEmpty)
			{
				return 0;
			}
			for (int i = 0; i < chars.Length; i++)
			{
				char c = chars[i];
				if ((uint)c >= (uint)_isAllowed.Length || !_isAllowed[(uint)c])
				{
					base.EncoderFallback.CreateFallbackBuffer().Fallback(c, i);
					throw new CryptographicException();
				}
				if (write)
				{
					bytes[i] = (byte)c;
				}
			}
			return chars.Length;
		}

		protected override int GetChars(ReadOnlySpan<byte> bytes, Span<char> chars, bool write)
		{
			if (bytes.IsEmpty)
			{
				return 0;
			}
			for (int i = 0; i < bytes.Length; i++)
			{
				byte b = bytes[i];
				if ((uint)b >= (uint)_isAllowed.Length || !_isAllowed[b])
				{
					base.DecoderFallback.CreateFallbackBuffer().Fallback(new byte[1] { b }, i);
					throw new CryptographicException();
				}
				if (write)
				{
					chars[i] = (char)b;
				}
			}
			return bytes.Length;
		}
	}
	internal class BMPEncoding : SpanBasedEncoding
	{
		protected override int GetBytes(ReadOnlySpan<char> chars, Span<byte> bytes, bool write)
		{
			if (chars.IsEmpty)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < chars.Length; i++)
			{
				char c = chars[i];
				if (char.IsSurrogate(c))
				{
					base.EncoderFallback.CreateFallbackBuffer().Fallback(c, i);
					throw new CryptographicException();
				}
				ushort num2 = c;
				if (write)
				{
					bytes[num + 1] = (byte)num2;
					bytes[num] = (byte)(num2 >> 8);
				}
				num += 2;
			}
			return num;
		}

		protected override int GetChars(ReadOnlySpan<byte> bytes, Span<char> chars, bool write)
		{
			if (bytes.IsEmpty)
			{
				return 0;
			}
			if (bytes.Length % 2 != 0)
			{
				base.DecoderFallback.CreateFallbackBuffer().Fallback(bytes.Slice(bytes.Length - 1).ToArray(), bytes.Length - 1);
				throw new CryptographicException();
			}
			int num = 0;
			for (int i = 0; i < bytes.Length; i += 2)
			{
				char c = (char)((bytes[i] << 8) | bytes[i + 1]);
				if (char.IsSurrogate(c))
				{
					base.DecoderFallback.CreateFallbackBuffer().Fallback(bytes.Slice(i, 2).ToArray(), i);
					throw new CryptographicException();
				}
				if (write)
				{
					chars[num] = c;
				}
				num++;
			}
			return num;
		}

		public override int GetMaxByteCount(int charCount)
		{
			return checked(charCount * 2);
		}

		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount / 2;
		}
	}
	internal class SetOfValueComparer : IComparer<ReadOnlyMemory<byte>>
	{
		internal static SetOfValueComparer Instance { get; } = new SetOfValueComparer();

		public int Compare(ReadOnlyMemory<byte> x, ReadOnlyMemory<byte> y)
		{
			ReadOnlySpan<byte> span = x.Span;
			ReadOnlySpan<byte> span2 = y.Span;
			int num = Math.Min(x.Length, y.Length);
			int num3;
			for (int i = 0; i < num; i++)
			{
				byte num2 = span[i];
				byte b = span2[i];
				num3 = num2 - b;
				if (num3 != 0)
				{
					return num3;
				}
			}
			num3 = x.Length - y.Length;
			if (num3 != 0)
			{
				return num3;
			}
			return 0;
		}
	}
	internal class AsnReader
	{
		private delegate void BitStringCopyAction(ReadOnlyMemory<byte> value, byte normalizedLastByte, Span<byte> destination);

		internal const int MaxCERSegmentSize = 1000;

		private const int EndOfContentsEncodedLength = 2;

		private ReadOnlyMemory<byte> _data;

		private readonly AsnEncodingRules _ruleSet;

		private const byte HmsState = 0;

		private const byte FracState = 1;

		private const byte SuffixState = 2;

		public bool HasData => !_data.IsEmpty;

		public AsnReader(ReadOnlyMemory<byte> data, AsnEncodingRules ruleSet)
		{
			CheckEncodingRules(ruleSet);
			_data = data;
			_ruleSet = ruleSet;
		}

		public void ThrowIfNotEmpty()
		{
			if (HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
		}

		public static bool TryPeekTag(ReadOnlySpan<byte> source, out Asn1Tag tag, out int bytesRead)
		{
			return Asn1Tag.TryParse(source, out tag, out bytesRead);
		}

		public Asn1Tag PeekTag()
		{
			if (TryPeekTag(_data.Span, out var tag, out var _))
			{
				return tag;
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		private static bool TryReadLength(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet, out int? length, out int bytesRead)
		{
			length = null;
			bytesRead = 0;
			CheckEncodingRules(ruleSet);
			if (source.IsEmpty)
			{
				return false;
			}
			byte b = source[bytesRead];
			bytesRead++;
			if (b == 128)
			{
				if (ruleSet == AsnEncodingRules.DER)
				{
					bytesRead = 0;
					return false;
				}
				return true;
			}
			if (b < 128)
			{
				length = b;
				return true;
			}
			if (b == byte.MaxValue)
			{
				bytesRead = 0;
				return false;
			}
			byte b2 = (byte)(b & -129);
			if (b2 + 1 > source.Length)
			{
				bytesRead = 0;
				return false;
			}
			bool flag = ruleSet == AsnEncodingRules.DER || ruleSet == AsnEncodingRules.CER;
			if (flag && b2 > 4)
			{
				bytesRead = 0;
				return false;
			}
			uint num = 0u;
			for (int i = 0; i < b2; i++)
			{
				byte b3 = source[bytesRead];
				bytesRead++;
				if (num == 0)
				{
					if (flag && b3 == 0)
					{
						bytesRead = 0;
						return false;
					}
					if (!flag && b3 != 0 && b2 - i > 4)
					{
						bytesRead = 0;
						return false;
					}
				}
				num <<= 8;
				num |= b3;
			}
			if (num > int.MaxValue)
			{
				bytesRead = 0;
				return false;
			}
			if (flag && num < 128)
			{
				bytesRead = 0;
				return false;
			}
			length = (int)num;
			return true;
		}

		internal Asn1Tag ReadTagAndLength(out int? contentsLength, out int bytesRead)
		{
			if (TryPeekTag(_data.Span, out var tag, out var bytesRead2) && TryReadLength(_data.Slice(bytesRead2).Span, _ruleSet, out var length, out var bytesRead3))
			{
				int num = bytesRead2 + bytesRead3;
				if (tag.IsConstructed)
				{
					if (_ruleSet == AsnEncodingRules.CER && length.HasValue)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
				}
				else if (!length.HasValue)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				bytesRead = num;
				contentsLength = length;
				return tag;
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		private static void ValidateEndOfContents(Asn1Tag tag, int? length, int headerLength)
		{
			if (tag.IsConstructed || length != 0 || headerLength != 2)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
		}

		private int SeekEndOfContents(ReadOnlyMemory<byte> source)
		{
			int num = 0;
			AsnReader asnReader = new AsnReader(source, _ruleSet);
			int num2 = 1;
			while (asnReader.HasData)
			{
				int? contentsLength;
				int bytesRead;
				Asn1Tag asn1Tag = asnReader.ReadTagAndLength(out contentsLength, out bytesRead);
				if (asn1Tag == Asn1Tag.EndOfContents)
				{
					ValidateEndOfContents(asn1Tag, contentsLength, bytesRead);
					num2--;
					if (num2 == 0)
					{
						return num;
					}
				}
				if (!contentsLength.HasValue)
				{
					num2++;
					asnReader._data = asnReader._data.Slice(bytesRead);
					num += bytesRead;
				}
				else
				{
					ReadOnlyMemory<byte> readOnlyMemory = Slice(asnReader._data, 0, bytesRead + contentsLength.Value);
					asnReader._data = asnReader._data.Slice(readOnlyMemory.Length);
					num += readOnlyMemory.Length;
				}
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		public ReadOnlyMemory<byte> PeekEncodedValue()
		{
			ReadTagAndLength(out var contentsLength, out var bytesRead);
			if (!contentsLength.HasValue)
			{
				int num = SeekEndOfContents(_data.Slice(bytesRead));
				return Slice(_data, 0, bytesRead + num + 2);
			}
			return Slice(_data, 0, bytesRead + contentsLength.Value);
		}

		public ReadOnlyMemory<byte> PeekContentBytes()
		{
			ReadTagAndLength(out var contentsLength, out var bytesRead);
			if (!contentsLength.HasValue)
			{
				return Slice(_data, bytesRead, SeekEndOfContents(_data.Slice(bytesRead)));
			}
			return Slice(_data, bytesRead, contentsLength.Value);
		}

		public ReadOnlyMemory<byte> GetEncodedValue()
		{
			ReadOnlyMemory<byte> result = PeekEncodedValue();
			_data = _data.Slice(result.Length);
			return result;
		}

		private static bool ReadBooleanValue(ReadOnlySpan<byte> source, AsnEncodingRules ruleSet)
		{
			if (source.Length != 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			switch (source[0])
			{
			case 0:
				return false;
			default:
				if (ruleSet == AsnEncodingRules.DER || ruleSet == AsnEncodingRules.CER)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				break;
			case byte.MaxValue:
				break;
			}
			return true;
		}

		public bool ReadBoolean()
		{
			return ReadBoolean(Asn1Tag.Boolean);
		}

		public bool ReadBoolean(Asn1Tag expectedTag)
		{
			int? contentsLength;
			int bytesRead;
			Asn1Tag tag = ReadTagAndLength(out contentsLength, out bytesRead);
			CheckExpectedTag(tag, expectedTag, UniversalTagNumber.Boolean);
			if (tag.IsConstructed)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			bool result = ReadBooleanValue(Slice(_data, bytesRead, contentsLength.Value).Span, _ruleSet);
			_data = _data.Slice(bytesRead + contentsLength.Value);
			return result;
		}

		private ReadOnlyMemory<byte> GetIntegerContents(Asn1Tag expectedTag, UniversalTagNumber tagNumber, out int headerLength)
		{
			int? contentsLength;
			Asn1Tag tag = ReadTagAndLength(out contentsLength, out headerLength);
			CheckExpectedTag(tag, expectedTag, tagNumber);
			if (tag.IsConstructed || contentsLength < 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			ReadOnlyMemory<byte> result = Slice(_data, headerLength, contentsLength.Value);
			ReadOnlySpan<byte> span = result.Span;
			if (result.Length > 1)
			{
				ushort num = (ushort)((ushort)((span[0] << 8) | span[1]) & 0xFF80);
				if (num == 0 || num == 65408)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
			}
			return result;
		}

		public ReadOnlyMemory<byte> GetIntegerBytes()
		{
			return GetIntegerBytes(Asn1Tag.Integer);
		}

		public ReadOnlyMemory<byte> GetIntegerBytes(Asn1Tag expectedTag)
		{
			int headerLength;
			ReadOnlyMemory<byte> integerContents = GetIntegerContents(expectedTag, UniversalTagNumber.Integer, out headerLength);
			_data = _data.Slice(headerLength + integerContents.Length);
			return integerContents;
		}

		public BigInteger GetInteger()
		{
			return GetInteger(Asn1Tag.Integer);
		}

		public BigInteger GetInteger(Asn1Tag expectedTag)
		{
			int headerLength;
			ReadOnlyMemory<byte> integerContents = GetIntegerContents(expectedTag, UniversalTagNumber.Integer, out headerLength);
			byte[] array = ArrayPool<byte>.Shared.Rent(integerContents.Length);
			BigInteger result;
			try
			{
				byte value = (byte)(((integerContents.Span[0] & 0x80) != 0) ? byte.MaxValue : 0);
				new Span<byte>(array, integerContents.Length, array.Length - integerContents.Length).Fill(value);
				integerContents.CopyTo(array);
				AsnWriter.Reverse(new Span<byte>(array, 0, integerContents.Length));
				result = new BigInteger(array);
			}
			finally
			{
				Array.Clear(array, 0, array.Length);
				ArrayPool<byte>.Shared.Return(array);
			}
			_data = _data.Slice(headerLength + integerContents.Length);
			return result;
		}

		private bool TryReadSignedInteger(int sizeLimit, Asn1Tag expectedTag, UniversalTagNumber tagNumber, out long value)
		{
			int headerLength;
			ReadOnlyMemory<byte> integerContents = GetIntegerContents(expectedTag, tagNumber, out headerLength);
			if (integerContents.Length > sizeLimit)
			{
				value = 0L;
				return false;
			}
			ReadOnlySpan<byte> span = integerContents.Span;
			long num = (((span[0] & 0x80) != 0) ? (-1) : 0);
			for (int i = 0; i < integerContents.Length; i++)
			{
				num <<= 8;
				num |= span[i];
			}
			_data = _data.Slice(headerLength + integerContents.Length);
			value = num;
			return true;
		}

		private bool TryReadUnsignedInteger(int sizeLimit, Asn1Tag expectedTag, UniversalTagNumber tagNumber, out ulong value)
		{
			int headerLength;
			ReadOnlyMemory<byte> integerContents = GetIntegerContents(expectedTag, tagNumber, out headerLength);
			ReadOnlySpan<byte> readOnlySpan = integerContents.Span;
			int length = integerContents.Length;
			if ((readOnlySpan[0] & 0x80) != 0)
			{
				value = 0uL;
				return false;
			}
			if (readOnlySpan.Length > 1 && readOnlySpan[0] == 0)
			{
				readOnlySpan = readOnlySpan.Slice(1);
			}
			if (readOnlySpan.Length > sizeLimit)
			{
				value = 0uL;
				return false;
			}
			ulong num = 0uL;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				num <<= 8;
				num |= readOnlySpan[i];
			}
			_data = _data.Slice(headerLength + length);
			value = num;
			return true;
		}

		public bool TryReadInt32(out int value)
		{
			return TryReadInt32(Asn1Tag.Integer, out value);
		}

		public bool TryReadInt32(Asn1Tag expectedTag, out int value)
		{
			if (TryReadSignedInteger(4, expectedTag, UniversalTagNumber.Integer, out var value2))
			{
				value = (int)value2;
				return true;
			}
			value = 0;
			return false;
		}

		public bool TryReadUInt32(out uint value)
		{
			return TryReadUInt32(Asn1Tag.Integer, out value);
		}

		public bool TryReadUInt32(Asn1Tag expectedTag, out uint value)
		{
			if (TryReadUnsignedInteger(4, expectedTag, UniversalTagNumber.Integer, out var value2))
			{
				value = (uint)value2;
				return true;
			}
			value = 0u;
			return false;
		}

		public bool TryReadInt64(out long value)
		{
			return TryReadInt64(Asn1Tag.Integer, out value);
		}

		public bool TryReadInt64(Asn1Tag expectedTag, out long value)
		{
			return TryReadSignedInteger(8, expectedTag, UniversalTagNumber.Integer, out value);
		}

		public bool TryReadUInt64(out ulong value)
		{
			return TryReadUInt64(Asn1Tag.Integer, out value);
		}

		public bool TryReadUInt64(Asn1Tag expectedTag, out ulong value)
		{
			return TryReadUnsignedInteger(8, expectedTag, UniversalTagNumber.Integer, out value);
		}

		public bool TryReadInt16(out short value)
		{
			return TryReadInt16(Asn1Tag.Integer, out value);
		}

		public bool TryReadInt16(Asn1Tag expectedTag, out short value)
		{
			if (TryReadSignedInteger(2, expectedTag, UniversalTagNumber.Integer, out var value2))
			{
				value = (short)value2;
				return true;
			}
			value = 0;
			return false;
		}

		public bool TryReadUInt16(out ushort value)
		{
			return TryReadUInt16(Asn1Tag.Integer, out value);
		}

		public bool TryReadUInt16(Asn1Tag expectedTag, out ushort value)
		{
			if (TryReadUnsignedInteger(2, expectedTag, UniversalTagNumber.Integer, out var value2))
			{
				value = (ushort)value2;
				return true;
			}
			value = 0;
			return false;
		}

		public bool TryReadInt8(out sbyte value)
		{
			return TryReadInt8(Asn1Tag.Integer, out value);
		}

		public bool TryReadInt8(Asn1Tag expectedTag, out sbyte value)
		{
			if (TryReadSignedInteger(1, expectedTag, UniversalTagNumber.Integer, out var value2))
			{
				value = (sbyte)value2;
				return true;
			}
			value = 0;
			return false;
		}

		public bool TryReadUInt8(out byte value)
		{
			return TryReadUInt8(Asn1Tag.Integer, out value);
		}

		public bool TryReadUInt8(Asn1Tag expectedTag, out byte value)
		{
			if (TryReadUnsignedInteger(1, expectedTag, UniversalTagNumber.Integer, out var value2))
			{
				value = (byte)value2;
				return true;
			}
			value = 0;
			return false;
		}

		private void ParsePrimitiveBitStringContents(ReadOnlyMemory<byte> source, out int unusedBitCount, out ReadOnlyMemory<byte> value, out byte normalizedLastByte)
		{
			if (_ruleSet == AsnEncodingRules.CER && source.Length > 1000)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (source.Length == 0)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			ReadOnlySpan<byte> span = source.Span;
			unusedBitCount = span[0];
			if (unusedBitCount > 7)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (source.Length == 1)
			{
				if (unusedBitCount > 0)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				value = ReadOnlyMemory<byte>.Empty;
				normalizedLastByte = 0;
				return;
			}
			int num = -1 << unusedBitCount;
			byte b = span[span.Length - 1];
			byte b2 = (byte)(b & num);
			if (b2 != b && (_ruleSet == AsnEncodingRules.DER || _ruleSet == AsnEncodingRules.CER))
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			normalizedLastByte = b2;
			value = source.Slice(1);
		}

		private static void CopyBitStringValue(ReadOnlyMemory<byte> value, byte normalizedLastByte, Span<byte> destination)
		{
			if (value.Length != 0)
			{
				value.Span.CopyTo(destination);
				destination[value.Length - 1] = normalizedLastByte;
			}
		}

		private int CountConstructedBitString(ReadOnlyMemory<byte> source, bool isIndefinite)
		{
			Span<byte> empty = Span<byte>.Empty;
			int lastUnusedBitCount;
			int bytesRead;
			return ProcessConstructedBitString(source, empty, null, isIndefinite, out lastUnusedBitCount, out bytesRead);
		}

		private void CopyConstructedBitString(ReadOnlyMemory<byte> source, Span<byte> destination, bool isIndefinite, out int unusedBitCount, out int bytesRead, out int bytesWritten)
		{
			bytesWritten = ProcessConstructedBitString(source, destination, delegate(ReadOnlyMemory<byte> value, byte lastByte, Span<byte> dest)
			{
				CopyBitStringValue(value, lastByte, dest);
			}, isIndefinite, out unusedBitCount, out bytesRead);
		}

		private int ProcessConstructedBitString(ReadOnlyMemory<byte> source, Span<byte> destination, BitStringCopyAction copyAction, bool isIndefinite, out int lastUnusedBitCount, out int bytesRead)
		{
			lastUnusedBitCount = 0;
			bytesRead = 0;
			int num = 1000;
			AsnReader asnReader = new AsnReader(source, _ruleSet);
			Stack<(AsnReader, bool, int)> stack = null;
			int num2 = 0;
			Asn1Tag asn1Tag = Asn1Tag.ConstructedBitString;
			Span<byte> destination2 = destination;
			do
			{
				IL_01f2:
				if (asnReader.HasData)
				{
					asn1Tag = asnReader.ReadTagAndLength(out var contentsLength, out var bytesRead2);
					if (asn1Tag == Asn1Tag.PrimitiveBitString)
					{
						if (lastUnusedBitCount != 0)
						{
							throw new CryptographicException("ASN1 corrupted data.");
						}
						if (_ruleSet == AsnEncodingRules.CER && num != 1000)
						{
							throw new CryptographicException("ASN1 corrupted data.");
						}
						ReadOnlyMemory<byte> source2 = Slice(asnReader._data, bytesRead2, contentsLength.Value);
						ParsePrimitiveBitStringContents(source2, out lastUnusedBitCount, out var value, out var normalizedLastByte);
						int num3 = bytesRead2 + source2.Length;
						asnReader._data = asnReader._data.Slice(num3);
						bytesRead += num3;
						num2 += value.Length;
						num = source2.Length;
						if (copyAction != null)
						{
							copyAction(value, normalizedLastByte, destination2);
							destination2 = destination2.Slice(value.Length);
						}
						goto IL_01f2;
					}
					if (!(asn1Tag == Asn1Tag.EndOfContents && isIndefinite))
					{
						if (asn1Tag == Asn1Tag.ConstructedBitString)
						{
							if (_ruleSet == AsnEncodingRules.CER)
							{
								throw new CryptographicException("ASN1 corrupted data.");
							}
							if (stack == null)
							{
								stack = new Stack<(AsnReader, bool, int)>();
							}
							stack.Push((asnReader, isIndefinite, bytesRead));
							asnReader = new AsnReader(Slice(asnReader._data, bytesRead2, contentsLength), _ruleSet);
							bytesRead = bytesRead2;
							isIndefinite = !contentsLength.HasValue;
							goto IL_01f2;
						}
						throw new CryptographicException("ASN1 corrupted data.");
					}
					ValidateEndOfContents(asn1Tag, contentsLength, bytesRead2);
					bytesRead += bytesRead2;
					if (stack != null && stack.Count > 0)
					{
						(AsnReader, bool, int) tuple = stack.Pop();
						AsnReader item = tuple.Item1;
						bool item2 = tuple.Item2;
						int item3 = tuple.Item3;
						item._data = item._data.Slice(bytesRead);
						bytesRead += item3;
						isIndefinite = item2;
						asnReader = item;
						goto IL_01f2;
					}
				}
				if (isIndefinite && asn1Tag != Asn1Tag.EndOfContents)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (stack != null && stack.Count > 0)
				{
					(AsnReader, bool, int) tuple2 = stack.Pop();
					AsnReader item4 = tuple2.Item1;
					bool item5 = tuple2.Item2;
					int item6 = tuple2.Item3;
					asnReader = item4;
					asnReader._data = asnReader._data.Slice(bytesRead);
					isIndefinite = item5;
					bytesRead += item6;
				}
				else
				{
					asnReader = null;
				}
			}
			while (asnReader != null);
			return num2;
		}

		private bool TryCopyConstructedBitStringValue(ReadOnlyMemory<byte> source, Span<byte> dest, bool isIndefinite, out int unusedBitCount, out int bytesRead, out int bytesWritten)
		{
			int num = CountConstructedBitString(source, isIndefinite);
			if (_ruleSet == AsnEncodingRules.CER && num < 1000)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (dest.Length < num)
			{
				unusedBitCount = 0;
				bytesRead = 0;
				bytesWritten = 0;
				return false;
			}
			CopyConstructedBitString(source, dest, isIndefinite, out unusedBitCount, out bytesRead, out bytesWritten);
			return true;
		}

		private bool TryGetPrimitiveBitStringValue(Asn1Tag expectedTag, out Asn1Tag actualTag, out int? contentsLength, out int headerLength, out int unusedBitCount, out ReadOnlyMemory<byte> value, out byte normalizedLastByte)
		{
			actualTag = ReadTagAndLength(out contentsLength, out headerLength);
			CheckExpectedTag(actualTag, expectedTag, UniversalTagNumber.BitString);
			if (actualTag.IsConstructed)
			{
				if (_ruleSet == AsnEncodingRules.DER)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				unusedBitCount = 0;
				value = default(ReadOnlyMemory<byte>);
				normalizedLastByte = 0;
				return false;
			}
			ReadOnlyMemory<byte> source = Slice(_data, headerLength, contentsLength.Value);
			ParsePrimitiveBitStringContents(source, out unusedBitCount, out value, out normalizedLastByte);
			return true;
		}

		public bool TryGetPrimitiveBitStringValue(out int unusedBitCount, out ReadOnlyMemory<byte> contents)
		{
			return TryGetPrimitiveBitStringValue(Asn1Tag.PrimitiveBitString, out unusedBitCount, out contents);
		}

		public bool TryGetPrimitiveBitStringValue(Asn1Tag expectedTag, out int unusedBitCount, out ReadOnlyMemory<byte> value)
		{
			Asn1Tag actualTag;
			int? contentsLength;
			int headerLength;
			byte normalizedLastByte;
			bool flag = TryGetPrimitiveBitStringValue(expectedTag, out actualTag, out contentsLength, out headerLength, out unusedBitCount, out value, out normalizedLastByte);
			if (flag)
			{
				if (value.Length != 0 && normalizedLastByte != value.Span[value.Length - 1])
				{
					unusedBitCount = 0;
					value = default(ReadOnlyMemory<byte>);
					return false;
				}
				_data = _data.Slice(headerLength + value.Length + 1);
			}
			return flag;
		}

		public bool TryCopyBitStringBytes(Span<byte> destination, out int unusedBitCount, out int bytesWritten)
		{
			return TryCopyBitStringBytes(Asn1Tag.PrimitiveBitString, destination, out unusedBitCount, out bytesWritten);
		}

		public bool TryCopyBitStringBytes(Asn1Tag expectedTag, Span<byte> destination, out int unusedBitCount, out int bytesWritten)
		{
			if (TryGetPrimitiveBitStringValue(expectedTag, out var _, out var contentsLength, out var headerLength, out unusedBitCount, out var value, out var normalizedLastByte))
			{
				if (value.Length > destination.Length)
				{
					bytesWritten = 0;
					unusedBitCount = 0;
					return false;
				}
				CopyBitStringValue(value, normalizedLastByte, destination);
				bytesWritten = value.Length;
				_data = _data.Slice(headerLength + value.Length + 1);
				return true;
			}
			int bytesRead;
			bool num = TryCopyConstructedBitStringValue(Slice(_data, headerLength, contentsLength), destination, !contentsLength.HasValue, out unusedBitCount, out bytesRead, out bytesWritten);
			if (num)
			{
				_data = _data.Slice(headerLength + bytesRead);
			}
			return num;
		}

		public TFlagsEnum GetNamedBitListValue<TFlagsEnum>() where TFlagsEnum : struct
		{
			return GetNamedBitListValue<TFlagsEnum>(Asn1Tag.PrimitiveBitString);
		}

		public TFlagsEnum GetNamedBitListValue<TFlagsEnum>(Asn1Tag expectedTag) where TFlagsEnum : struct
		{
			Type typeFromHandle = typeof(TFlagsEnum);
			return (TFlagsEnum)Enum.ToObject(typeFromHandle, GetNamedBitListValue(expectedTag, typeFromHandle));
		}

		public Enum GetNamedBitListValue(Type tFlagsEnum)
		{
			return GetNamedBitListValue(Asn1Tag.PrimitiveBitString, tFlagsEnum);
		}

		public Enum GetNamedBitListValue(Asn1Tag expectedTag, Type tFlagsEnum)
		{
			Type enumUnderlyingType = tFlagsEnum.GetEnumUnderlyingType();
			if (!tFlagsEnum.IsDefined(typeof(FlagsAttribute), inherit: false))
			{
				throw new ArgumentException("Named bit list operations require an enum with the [Flags] attribute.", "tFlagsEnum");
			}
			Span<byte> destination = stackalloc byte[Marshal.SizeOf(enumUnderlyingType)];
			ReadOnlyMemory<byte> data = _data;
			try
			{
				if (!TryCopyBitStringBytes(expectedTag, destination, out var unusedBitCount, out var bytesWritten))
				{
					throw new CryptographicException(global::SR.Format("The encoded named bit list value is larger than the value size of the '{0}' enum.", tFlagsEnum.Name));
				}
				if (bytesWritten == 0)
				{
					return (Enum)Enum.ToObject(tFlagsEnum, 0);
				}
				ReadOnlySpan<byte> valueSpan = destination.Slice(0, bytesWritten);
				if (_ruleSet == AsnEncodingRules.DER || _ruleSet == AsnEncodingRules.CER)
				{
					byte num = valueSpan[bytesWritten - 1];
					byte b = (byte)(1 << unusedBitCount);
					if ((num & b) == 0)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
				}
				return (Enum)Enum.ToObject(tFlagsEnum, InterpretNamedBitListReversed(valueSpan));
			}
			catch
			{
				_data = data;
				throw;
			}
		}

		private static long InterpretNamedBitListReversed(ReadOnlySpan<byte> valueSpan)
		{
			long num = 0L;
			long num2 = 1L;
			for (int i = 0; i < valueSpan.Length; i++)
			{
				byte b = valueSpan[i];
				for (int num3 = 7; num3 >= 0; num3--)
				{
					int num4 = 1 << num3;
					if ((b & num4) != 0)
					{
						num |= num2;
					}
					num2 <<= 1;
				}
			}
			return num;
		}

		public ReadOnlyMemory<byte> GetEnumeratedBytes()
		{
			return GetEnumeratedBytes(Asn1Tag.Enumerated);
		}

		public ReadOnlyMemory<byte> GetEnumeratedBytes(Asn1Tag expectedTag)
		{
			int headerLength;
			ReadOnlyMemory<byte> integerContents = GetIntegerContents(expectedTag, UniversalTagNumber.Enumerated, out headerLength);
			_data = _data.Slice(headerLength + integerContents.Length);
			return integerContents;
		}

		public TEnum GetEnumeratedValue<TEnum>() where TEnum : struct
		{
			Type typeFromHandle = typeof(TEnum);
			return (TEnum)Enum.ToObject(typeFromHandle, GetEnumeratedValue(typeFromHandle));
		}

		public TEnum GetEnumeratedValue<TEnum>(Asn1Tag expectedTag) where TEnum : struct
		{
			Type typeFromHandle = typeof(TEnum);
			return (TEnum)Enum.ToObject(typeFromHandle, GetEnumeratedValue(expectedTag, typeFromHandle));
		}

		public Enum GetEnumeratedValue(Type tEnum)
		{
			return GetEnumeratedValue(Asn1Tag.Enumerated, tEnum);
		}

		public Enum GetEnumeratedValue(Asn1Tag expectedTag, Type tEnum)
		{
			Type enumUnderlyingType = tEnum.GetEnumUnderlyingType();
			if (tEnum.IsDefined(typeof(FlagsAttribute), inherit: false))
			{
				throw new ArgumentException("ASN.1 Enumerated values only apply to enum types without the [Flags] attribute.", "tEnum");
			}
			int sizeLimit = Marshal.SizeOf(enumUnderlyingType);
			if (enumUnderlyingType == typeof(int) || enumUnderlyingType == typeof(long) || enumUnderlyingType == typeof(short) || enumUnderlyingType == typeof(sbyte))
			{
				if (!TryReadSignedInteger(sizeLimit, expectedTag, UniversalTagNumber.Enumerated, out var value))
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return (Enum)Enum.ToObject(tEnum, value);
			}
			if (enumUnderlyingType == typeof(uint) || enumUnderlyingType == typeof(ulong) || enumUnderlyingType == typeof(ushort) || enumUnderlyingType == typeof(byte))
			{
				if (!TryReadUnsignedInteger(sizeLimit, expectedTag, UniversalTagNumber.Enumerated, out var value2))
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return (Enum)Enum.ToObject(tEnum, value2);
			}
			throw new CryptographicException();
		}

		private bool TryGetPrimitiveOctetStringBytes(Asn1Tag expectedTag, out Asn1Tag actualTag, out int? contentLength, out int headerLength, out ReadOnlyMemory<byte> contents, UniversalTagNumber universalTagNumber = UniversalTagNumber.OctetString)
		{
			actualTag = ReadTagAndLength(out contentLength, out headerLength);
			CheckExpectedTag(actualTag, expectedTag, universalTagNumber);
			if (actualTag.IsConstructed)
			{
				if (_ruleSet == AsnEncodingRules.DER)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				contents = default(ReadOnlyMemory<byte>);
				return false;
			}
			ReadOnlyMemory<byte> readOnlyMemory = Slice(_data, headerLength, contentLength.Value);
			if (_ruleSet == AsnEncodingRules.CER && readOnlyMemory.Length > 1000)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			contents = readOnlyMemory;
			return true;
		}

		private bool TryGetPrimitiveOctetStringBytes(Asn1Tag expectedTag, UniversalTagNumber universalTagNumber, out ReadOnlyMemory<byte> contents)
		{
			if (TryGetPrimitiveOctetStringBytes(expectedTag, out var _, out var _, out var headerLength, out contents, universalTagNumber))
			{
				_data = _data.Slice(headerLength + contents.Length);
				return true;
			}
			return false;
		}

		public bool TryGetPrimitiveOctetStringBytes(out ReadOnlyMemory<byte> contents)
		{
			return TryGetPrimitiveOctetStringBytes(Asn1Tag.PrimitiveOctetString, out contents);
		}

		public bool TryGetPrimitiveOctetStringBytes(Asn1Tag expectedTag, out ReadOnlyMemory<byte> contents)
		{
			return TryGetPrimitiveOctetStringBytes(expectedTag, UniversalTagNumber.OctetString, out contents);
		}

		private int CountConstructedOctetString(ReadOnlyMemory<byte> source, bool isIndefinite)
		{
			int bytesRead;
			int num = CopyConstructedOctetString(source, Span<byte>.Empty, write: false, isIndefinite, out bytesRead);
			if (_ruleSet == AsnEncodingRules.CER && num <= 1000)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return num;
		}

		private void CopyConstructedOctetString(ReadOnlyMemory<byte> source, Span<byte> destination, bool isIndefinite, out int bytesRead, out int bytesWritten)
		{
			bytesWritten = CopyConstructedOctetString(source, destination, write: true, isIndefinite, out bytesRead);
		}

		private int CopyConstructedOctetString(ReadOnlyMemory<byte> source, Span<byte> destination, bool write, bool isIndefinite, out int bytesRead)
		{
			bytesRead = 0;
			int num = 1000;
			AsnReader asnReader = new AsnReader(source, _ruleSet);
			Stack<(AsnReader, bool, int)> stack = null;
			int num2 = 0;
			Asn1Tag asn1Tag = Asn1Tag.ConstructedBitString;
			Span<byte> destination2 = destination;
			do
			{
				IL_01f2:
				if (asnReader.HasData)
				{
					asn1Tag = asnReader.ReadTagAndLength(out var contentsLength, out var bytesRead2);
					if (asn1Tag == Asn1Tag.PrimitiveOctetString)
					{
						if (_ruleSet == AsnEncodingRules.CER && num != 1000)
						{
							throw new CryptographicException("ASN1 corrupted data.");
						}
						ReadOnlyMemory<byte> readOnlyMemory = Slice(asnReader._data, bytesRead2, contentsLength.Value);
						int num3 = bytesRead2 + readOnlyMemory.Length;
						asnReader._data = asnReader._data.Slice(num3);
						bytesRead += num3;
						num2 += readOnlyMemory.Length;
						num = readOnlyMemory.Length;
						if (_ruleSet == AsnEncodingRules.CER && num > 1000)
						{
							throw new CryptographicException("ASN1 corrupted data.");
						}
						if (write)
						{
							readOnlyMemory.Span.CopyTo(destination2);
							destination2 = destination2.Slice(readOnlyMemory.Length);
						}
						goto IL_01f2;
					}
					if (!(asn1Tag == Asn1Tag.EndOfContents && isIndefinite))
					{
						if (asn1Tag == Asn1Tag.ConstructedOctetString)
						{
							if (_ruleSet == AsnEncodingRules.CER)
							{
								throw new CryptographicException("ASN1 corrupted data.");
							}
							if (stack == null)
							{
								stack = new Stack<(AsnReader, bool, int)>();
							}
							stack.Push((asnReader, isIndefinite, bytesRead));
							asnReader = new AsnReader(Slice(asnReader._data, bytesRead2, contentsLength), _ruleSet);
							bytesRead = bytesRead2;
							isIndefinite = !contentsLength.HasValue;
							goto IL_01f2;
						}
						throw new CryptographicException("ASN1 corrupted data.");
					}
					ValidateEndOfContents(asn1Tag, contentsLength, bytesRead2);
					bytesRead += bytesRead2;
					if (stack != null && stack.Count > 0)
					{
						(AsnReader, bool, int) tuple = stack.Pop();
						AsnReader item = tuple.Item1;
						bool item2 = tuple.Item2;
						int item3 = tuple.Item3;
						item._data = item._data.Slice(bytesRead);
						bytesRead += item3;
						isIndefinite = item2;
						asnReader = item;
						goto IL_01f2;
					}
				}
				if (isIndefinite && asn1Tag != Asn1Tag.EndOfContents)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (stack != null && stack.Count > 0)
				{
					(AsnReader, bool, int) tuple2 = stack.Pop();
					AsnReader item4 = tuple2.Item1;
					bool item5 = tuple2.Item2;
					int item6 = tuple2.Item3;
					asnReader = item4;
					asnReader._data = asnReader._data.Slice(bytesRead);
					isIndefinite = item5;
					bytesRead += item6;
				}
				else
				{
					asnReader = null;
				}
			}
			while (asnReader != null);
			return num2;
		}

		private bool TryCopyConstructedOctetStringContents(ReadOnlyMemory<byte> source, Span<byte> dest, bool isIndefinite, out int bytesRead, out int bytesWritten)
		{
			bytesRead = 0;
			int num = CountConstructedOctetString(source, isIndefinite);
			if (dest.Length < num)
			{
				bytesWritten = 0;
				return false;
			}
			CopyConstructedOctetString(source, dest, isIndefinite, out bytesRead, out bytesWritten);
			return true;
		}

		public bool TryCopyOctetStringBytes(Span<byte> destination, out int bytesWritten)
		{
			return TryCopyOctetStringBytes(Asn1Tag.PrimitiveOctetString, destination, out bytesWritten);
		}

		public bool TryCopyOctetStringBytes(Asn1Tag expectedTag, Span<byte> destination, out int bytesWritten)
		{
			if (TryGetPrimitiveOctetStringBytes(expectedTag, out var _, out var contentLength, out var headerLength, out var contents))
			{
				if (contents.Length > destination.Length)
				{
					bytesWritten = 0;
					return false;
				}
				contents.Span.CopyTo(destination);
				bytesWritten = contents.Length;
				_data = _data.Slice(headerLength + contents.Length);
				return true;
			}
			int bytesRead;
			bool num = TryCopyConstructedOctetStringContents(Slice(_data, headerLength, contentLength), destination, !contentLength.HasValue, out bytesRead, out bytesWritten);
			if (num)
			{
				_data = _data.Slice(headerLength + bytesRead);
			}
			return num;
		}

		public void ReadNull()
		{
			ReadNull(Asn1Tag.Null);
		}

		public void ReadNull(Asn1Tag expectedTag)
		{
			int? contentsLength;
			int bytesRead;
			Asn1Tag tag = ReadTagAndLength(out contentsLength, out bytesRead);
			CheckExpectedTag(tag, expectedTag, UniversalTagNumber.Null);
			if (tag.IsConstructed || contentsLength != 0)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			_data = _data.Slice(bytesRead);
		}

		private static void ReadSubIdentifier(ReadOnlySpan<byte> source, out int bytesRead, out long? smallValue, out BigInteger? largeValue)
		{
			if (source[0] == 128)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			int num = -1;
			int i;
			for (i = 0; i < source.Length; i++)
			{
				if ((source[i] & 0x80) == 0)
				{
					num = i;
					break;
				}
			}
			if (num < 0)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			bytesRead = num + 1;
			long num2 = 0L;
			if (bytesRead <= 9)
			{
				for (i = 0; i < bytesRead; i++)
				{
					byte b = source[i];
					num2 <<= 7;
					num2 |= (byte)(b & 0x7F);
				}
				largeValue = null;
				smallValue = num2;
				return;
			}
			int minimumLength = (bytesRead / 8 + 1) * 7;
			byte[] array = ArrayPool<byte>.Shared.Rent(minimumLength);
			Array.Clear(array, 0, array.Length);
			Span<byte> destination = array;
			Span<byte> destination2 = stackalloc byte[8];
			int num3 = bytesRead;
			i = bytesRead - 8;
			while (num3 > 0)
			{
				byte b2 = source[i];
				num2 <<= 7;
				num2 |= (byte)(b2 & 0x7F);
				i++;
				if (i >= num3)
				{
					BinaryPrimitives.WriteInt64LittleEndian(destination2, num2);
					destination2.Slice(0, 7).CopyTo(destination);
					destination = destination.Slice(7);
					num2 = 0L;
					num3 -= 8;
					i = Math.Max(0, num3 - 8);
				}
			}
			int length = array.Length - destination.Length;
			largeValue = new BigInteger(array);
			smallValue = null;
			Array.Clear(array, 0, length);
			ArrayPool<byte>.Shared.Return(array);
		}

		private string ReadObjectIdentifierAsString(Asn1Tag expectedTag, out int totalBytesRead)
		{
			int? contentsLength;
			int bytesRead;
			Asn1Tag tag = ReadTagAndLength(out contentsLength, out bytesRead);
			CheckExpectedTag(tag, expectedTag, UniversalTagNumber.ObjectIdentifier);
			if (tag.IsConstructed || contentsLength < 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			ReadOnlySpan<byte> source = Slice(_data, bytesRead, contentsLength.Value).Span;
			StringBuilder stringBuilder = new StringBuilder((byte)source.Length * 4);
			ReadSubIdentifier(source, out var bytesRead2, out var smallValue, out var largeValue);
			if (smallValue.HasValue)
			{
				long num = smallValue.Value;
				byte value;
				if (num < 40)
				{
					value = 0;
				}
				else if (num < 80)
				{
					value = 1;
					num -= 40;
				}
				else
				{
					value = 2;
					num -= 80;
				}
				stringBuilder.Append(value);
				stringBuilder.Append('.');
				stringBuilder.Append(num);
			}
			else
			{
				BigInteger value2 = largeValue.Value;
				byte value = 2;
				value2 -= (BigInteger)80;
				stringBuilder.Append(value);
				stringBuilder.Append('.');
				stringBuilder.Append(value2.ToString());
			}
			source = source.Slice(bytesRead2);
			while (!source.IsEmpty)
			{
				ReadSubIdentifier(source, out bytesRead2, out smallValue, out largeValue);
				stringBuilder.Append('.');
				if (smallValue.HasValue)
				{
					stringBuilder.Append(smallValue.Value);
				}
				else
				{
					stringBuilder.Append(largeValue.Value.ToString());
				}
				source = source.Slice(bytesRead2);
			}
			totalBytesRead = bytesRead + contentsLength.Value;
			return stringBuilder.ToString();
		}

		public string ReadObjectIdentifierAsString()
		{
			return ReadObjectIdentifierAsString(Asn1Tag.ObjectIdentifier);
		}

		public string ReadObjectIdentifierAsString(Asn1Tag expectedTag)
		{
			int totalBytesRead;
			string result = ReadObjectIdentifierAsString(expectedTag, out totalBytesRead);
			_data = _data.Slice(totalBytesRead);
			return result;
		}

		public Oid ReadObjectIdentifier(bool skipFriendlyName = false)
		{
			return ReadObjectIdentifier(Asn1Tag.ObjectIdentifier, skipFriendlyName);
		}

		public Oid ReadObjectIdentifier(Asn1Tag expectedTag, bool skipFriendlyName = false)
		{
			int totalBytesRead;
			string text = ReadObjectIdentifierAsString(expectedTag, out totalBytesRead);
			Oid result = (skipFriendlyName ? new Oid(text, text) : new Oid(text));
			_data = _data.Slice(totalBytesRead);
			return result;
		}

		private bool TryCopyCharacterStringBytes(Asn1Tag expectedTag, UniversalTagNumber universalTagNumber, Span<byte> destination, out int bytesRead, out int bytesWritten)
		{
			if (TryGetPrimitiveOctetStringBytes(expectedTag, out var _, out var contentLength, out var headerLength, out var contents, universalTagNumber))
			{
				bytesWritten = contents.Length;
				if (destination.Length < bytesWritten)
				{
					bytesWritten = 0;
					bytesRead = 0;
					return false;
				}
				contents.Span.CopyTo(destination);
				bytesRead = headerLength + bytesWritten;
				return true;
			}
			int bytesRead2;
			bool num = TryCopyConstructedOctetStringContents(Slice(_data, headerLength, contentLength), destination, !contentLength.HasValue, out bytesRead2, out bytesWritten);
			if (num)
			{
				bytesRead = headerLength + bytesRead2;
				return num;
			}
			bytesRead = 0;
			return num;
		}

		private unsafe static bool TryCopyCharacterString(ReadOnlySpan<byte> source, Span<char> destination, Encoding encoding, out int charsWritten)
		{
			if (source.Length == 0)
			{
				charsWritten = 0;
				return true;
			}
			fixed (byte* reference = &MemoryMarshal.GetReference(source))
			{
				fixed (char* reference2 = &MemoryMarshal.GetReference(destination))
				{
					try
					{
						if (encoding.GetCharCount(reference, source.Length) > destination.Length)
						{
							charsWritten = 0;
							return false;
						}
						charsWritten = encoding.GetChars(reference, source.Length, reference2, destination.Length);
					}
					catch (DecoderFallbackException inner)
					{
						throw new CryptographicException("ASN1 corrupted data.", inner);
					}
					return true;
				}
			}
		}

		private unsafe string GetCharacterString(Asn1Tag expectedTag, UniversalTagNumber universalTagNumber, Encoding encoding)
		{
			byte[] rented = null;
			int bytesRead;
			ReadOnlySpan<byte> octetStringContents = GetOctetStringContents(expectedTag, universalTagNumber, out bytesRead, ref rented);
			try
			{
				string result;
				if (octetStringContents.Length == 0)
				{
					result = string.Empty;
				}
				else
				{
					fixed (byte* reference = &MemoryMarshal.GetReference(octetStringContents))
					{
						try
						{
							result = encoding.GetString(reference, octetStringContents.Length);
						}
						catch (DecoderFallbackException inner)
						{
							throw new CryptographicException("ASN1 corrupted data.", inner);
						}
					}
				}
				_data = _data.Slice(bytesRead);
				return result;
			}
			finally
			{
				if (rented != null)
				{
					Array.Clear(rented, 0, octetStringContents.Length);
					ArrayPool<byte>.Shared.Return(rented);
				}
			}
		}

		private bool TryCopyCharacterString(Asn1Tag expectedTag, UniversalTagNumber universalTagNumber, Encoding encoding, Span<char> destination, out int charsWritten)
		{
			byte[] rented = null;
			int bytesRead;
			ReadOnlySpan<byte> octetStringContents = GetOctetStringContents(expectedTag, universalTagNumber, out bytesRead, ref rented);
			try
			{
				bool num = TryCopyCharacterString(octetStringContents, destination, encoding, out charsWritten);
				if (num)
				{
					_data = _data.Slice(bytesRead);
				}
				return num;
			}
			finally
			{
				if (rented != null)
				{
					Array.Clear(rented, 0, octetStringContents.Length);
					ArrayPool<byte>.Shared.Return(rented);
				}
			}
		}

		public bool TryGetPrimitiveCharacterStringBytes(UniversalTagNumber encodingType, out ReadOnlyMemory<byte> contents)
		{
			return TryGetPrimitiveCharacterStringBytes(new Asn1Tag(encodingType), encodingType, out contents);
		}

		public bool TryGetPrimitiveCharacterStringBytes(Asn1Tag expectedTag, UniversalTagNumber encodingType, out ReadOnlyMemory<byte> contents)
		{
			CheckCharacterStringEncodingType(encodingType);
			return TryGetPrimitiveOctetStringBytes(expectedTag, encodingType, out contents);
		}

		public bool TryCopyCharacterStringBytes(UniversalTagNumber encodingType, Span<byte> destination, out int bytesWritten)
		{
			return TryCopyCharacterStringBytes(new Asn1Tag(encodingType), encodingType, destination, out bytesWritten);
		}

		public bool TryCopyCharacterStringBytes(Asn1Tag expectedTag, UniversalTagNumber encodingType, Span<byte> destination, out int bytesWritten)
		{
			CheckCharacterStringEncodingType(encodingType);
			int bytesRead;
			bool num = TryCopyCharacterStringBytes(expectedTag, encodingType, destination, out bytesRead, out bytesWritten);
			if (num)
			{
				_data = _data.Slice(bytesRead);
			}
			return num;
		}

		public bool TryCopyCharacterString(UniversalTagNumber encodingType, Span<char> destination, out int charsWritten)
		{
			return TryCopyCharacterString(new Asn1Tag(encodingType), encodingType, destination, out charsWritten);
		}

		public bool TryCopyCharacterString(Asn1Tag expectedTag, UniversalTagNumber encodingType, Span<char> destination, out int charsWritten)
		{
			Encoding encoding = AsnCharacterStringEncodings.GetEncoding(encodingType);
			return TryCopyCharacterString(expectedTag, encodingType, encoding, destination, out charsWritten);
		}

		public string GetCharacterString(UniversalTagNumber encodingType)
		{
			return GetCharacterString(new Asn1Tag(encodingType), encodingType);
		}

		public string GetCharacterString(Asn1Tag expectedTag, UniversalTagNumber encodingType)
		{
			Encoding encoding = AsnCharacterStringEncodings.GetEncoding(encodingType);
			return GetCharacterString(expectedTag, encodingType, encoding);
		}

		public AsnReader ReadSequence()
		{
			return ReadSequence(Asn1Tag.Sequence);
		}

		public AsnReader ReadSequence(Asn1Tag expectedTag)
		{
			int? contentsLength;
			int bytesRead;
			Asn1Tag tag = ReadTagAndLength(out contentsLength, out bytesRead);
			CheckExpectedTag(tag, expectedTag, UniversalTagNumber.Sequence);
			if (!tag.IsConstructed)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			int num = 0;
			if (!contentsLength.HasValue)
			{
				contentsLength = SeekEndOfContents(_data.Slice(bytesRead));
				num = 2;
			}
			ReadOnlyMemory<byte> data = Slice(_data, bytesRead, contentsLength.Value);
			_data = _data.Slice(bytesRead + data.Length + num);
			return new AsnReader(data, _ruleSet);
		}

		public AsnReader ReadSetOf(bool skipSortOrderValidation = false)
		{
			return ReadSetOf(Asn1Tag.SetOf, skipSortOrderValidation);
		}

		public AsnReader ReadSetOf(Asn1Tag expectedTag, bool skipSortOrderValidation = false)
		{
			int? contentsLength;
			int bytesRead;
			Asn1Tag tag = ReadTagAndLength(out contentsLength, out bytesRead);
			CheckExpectedTag(tag, expectedTag, UniversalTagNumber.Set);
			if (!tag.IsConstructed)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			int num = 0;
			if (!contentsLength.HasValue)
			{
				contentsLength = SeekEndOfContents(_data.Slice(bytesRead));
				num = 2;
			}
			ReadOnlyMemory<byte> data = Slice(_data, bytesRead, contentsLength.Value);
			if (!skipSortOrderValidation && (_ruleSet == AsnEncodingRules.DER || _ruleSet == AsnEncodingRules.CER))
			{
				AsnReader asnReader = new AsnReader(data, _ruleSet);
				ReadOnlyMemory<byte> readOnlyMemory = ReadOnlyMemory<byte>.Empty;
				SetOfValueComparer instance = SetOfValueComparer.Instance;
				while (asnReader.HasData)
				{
					ReadOnlyMemory<byte> y = readOnlyMemory;
					readOnlyMemory = asnReader.GetEncodedValue();
					if (instance.Compare(readOnlyMemory, y) < 0)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
				}
			}
			_data = _data.Slice(bytesRead + data.Length + num);
			return new AsnReader(data, _ruleSet);
		}

		private static int ParseNonNegativeIntAndSlice(ref ReadOnlySpan<byte> data, int bytesToRead)
		{
			int result = ParseNonNegativeInt(Slice(data, 0, bytesToRead));
			data = data.Slice(bytesToRead);
			return result;
		}

		private static int ParseNonNegativeInt(ReadOnlySpan<byte> data)
		{
			if (Utf8Parser.TryParse(data, out uint value, out int bytesConsumed, '\0') && value <= int.MaxValue && bytesConsumed == data.Length)
			{
				return (int)value;
			}
			throw new CryptographicException("ASN1 corrupted data.");
		}

		private DateTimeOffset ParseUtcTime(ReadOnlySpan<byte> contentOctets, int twoDigitYearMax)
		{
			if ((_ruleSet == AsnEncodingRules.DER || _ruleSet == AsnEncodingRules.CER) && contentOctets.Length != 13)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (contentOctets.Length < 11 || contentOctets.Length > 17 || (contentOctets.Length & 1) != 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			ReadOnlySpan<byte> data = contentOctets;
			int num = ParseNonNegativeIntAndSlice(ref data, 2);
			int month = ParseNonNegativeIntAndSlice(ref data, 2);
			int day = ParseNonNegativeIntAndSlice(ref data, 2);
			int hour = ParseNonNegativeIntAndSlice(ref data, 2);
			int minute = ParseNonNegativeIntAndSlice(ref data, 2);
			int second = 0;
			int hours = 0;
			int num2 = 0;
			bool flag = false;
			if (contentOctets.Length == 17 || contentOctets.Length == 13)
			{
				second = ParseNonNegativeIntAndSlice(ref data, 2);
			}
			if (contentOctets.Length == 11 || contentOctets.Length == 13)
			{
				if (data[0] != 90)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
			}
			else
			{
				if (data[0] == 45)
				{
					flag = true;
				}
				else if (data[0] != 43)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				data = data.Slice(1);
				hours = ParseNonNegativeIntAndSlice(ref data, 2);
				num2 = ParseNonNegativeIntAndSlice(ref data, 2);
			}
			if (num2 > 59)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			TimeSpan timeSpan = new TimeSpan(hours, num2, 0);
			if (flag)
			{
				timeSpan = -timeSpan;
			}
			int num3 = twoDigitYearMax / 100;
			if (num > twoDigitYearMax % 100)
			{
				num3--;
			}
			int year = num3 * 100 + num;
			try
			{
				return new DateTimeOffset(year, month, day, hour, minute, second, timeSpan);
			}
			catch (Exception inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		public DateTimeOffset GetUtcTime(int twoDigitYearMax = 2049)
		{
			return GetUtcTime(Asn1Tag.UtcTime, twoDigitYearMax);
		}

		public DateTimeOffset GetUtcTime(Asn1Tag expectedTag, int twoDigitYearMax = 2049)
		{
			byte[] rented = null;
			Span<byte> tmpSpace = stackalloc byte[17];
			int bytesRead;
			ReadOnlySpan<byte> octetStringContents = GetOctetStringContents(expectedTag, UniversalTagNumber.UtcTime, out bytesRead, ref rented, tmpSpace);
			DateTimeOffset result = ParseUtcTime(octetStringContents, twoDigitYearMax);
			if (rented != null)
			{
				Array.Clear(rented, 0, octetStringContents.Length);
				ArrayPool<byte>.Shared.Return(rented);
			}
			_data = _data.Slice(bytesRead);
			return result;
		}

		private static byte? ParseGeneralizedTime_GetNextState(byte octet)
		{
			switch (octet)
			{
			case 43:
			case 45:
			case 90:
				return 2;
			case 44:
			case 46:
				return 1;
			default:
				return null;
			}
		}

		private static DateTimeOffset ParseGeneralizedTime(AsnEncodingRules ruleSet, ReadOnlySpan<byte> contentOctets, bool disallowFractions)
		{
			bool flag = ruleSet == AsnEncodingRules.DER || ruleSet == AsnEncodingRules.CER;
			if (flag && contentOctets.Length < 15)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (contentOctets.Length < 10)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			ReadOnlySpan<byte> data = contentOctets;
			int year = ParseNonNegativeIntAndSlice(ref data, 4);
			int month = ParseNonNegativeIntAndSlice(ref data, 2);
			int day = ParseNonNegativeIntAndSlice(ref data, 2);
			int hour = ParseNonNegativeIntAndSlice(ref data, 2);
			int? num = null;
			int? num2 = null;
			ulong value = 0uL;
			ulong num3 = 1uL;
			byte b = byte.MaxValue;
			TimeSpan? timeSpan = null;
			bool flag2 = false;
			byte b2 = 0;
			while (b2 == 0 && data.Length != 0)
			{
				byte? b3 = ParseGeneralizedTime_GetNextState(data[0]);
				if (!b3.HasValue)
				{
					if (!num.HasValue)
					{
						num = ParseNonNegativeIntAndSlice(ref data, 2);
						continue;
					}
					if (num2.HasValue)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					num2 = ParseNonNegativeIntAndSlice(ref data, 2);
				}
				else
				{
					b2 = b3.Value;
				}
			}
			if (b2 == 1)
			{
				if (disallowFractions)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				switch (data[0])
				{
				case 44:
					if (flag)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					break;
				default:
					throw new CryptographicException();
				case 46:
					break;
				}
				data = data.Slice(1);
				if (data.IsEmpty)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (!Utf8Parser.TryParse(SliceAtMost(data, 12), out value, out int bytesConsumed, '\0') || bytesConsumed == 0)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				b = (byte)(value % 10);
				for (int i = 0; i < bytesConsumed; i++)
				{
					num3 *= 10;
				}
				data = data.Slice(bytesConsumed);
				uint value2;
				while (Utf8Parser.TryParse(SliceAtMost(data, 9), out value2, out bytesConsumed, '\0'))
				{
					data = data.Slice(bytesConsumed);
					b = (byte)(value2 % 10);
				}
				if (data.Length != 0)
				{
					byte? b4 = ParseGeneralizedTime_GetNextState(data[0]);
					if (!b4.HasValue)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					b2 = b4.Value;
				}
			}
			if (b2 == 2)
			{
				byte b5 = data[0];
				data = data.Slice(1);
				if (b5 == 90)
				{
					timeSpan = TimeSpan.Zero;
					flag2 = true;
				}
				else
				{
					bool flag3 = b5 switch
					{
						43 => false, 
						45 => true, 
						_ => throw new CryptographicException("ASN1 corrupted data."), 
					};
					if (data.IsEmpty)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					int hours = ParseNonNegativeIntAndSlice(ref data, 2);
					int num4 = 0;
					if (data.Length != 0)
					{
						num4 = ParseNonNegativeIntAndSlice(ref data, 2);
					}
					if (num4 > 59)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					TimeSpan timeSpan2 = new TimeSpan(hours, num4, 0);
					if (flag3)
					{
						timeSpan2 = -timeSpan2;
					}
					timeSpan = timeSpan2;
				}
			}
			if (!data.IsEmpty)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (flag)
			{
				if (!flag2 || !num2.HasValue)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (b == 0)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
			}
			double num5 = (double)value / (double)num3;
			TimeSpan timeSpan3 = TimeSpan.Zero;
			if (!num.HasValue)
			{
				num = 0;
				num2 = 0;
				if (value != 0L)
				{
					timeSpan3 = new TimeSpan((long)(num5 * 36000000000.0));
				}
			}
			else if (!num2.HasValue)
			{
				num2 = 0;
				if (value != 0L)
				{
					timeSpan3 = new TimeSpan((long)(num5 * 600000000.0));
				}
			}
			else if (value != 0L)
			{
				timeSpan3 = new TimeSpan((long)(num5 * 10000000.0));
			}
			try
			{
				DateTimeOffset dateTimeOffset = (timeSpan.HasValue ? new DateTimeOffset(year, month, day, hour, num.Value, num2.Value, timeSpan.Value) : new DateTimeOffset(new DateTime(year, month, day, hour, num.Value, num2.Value)));
				return dateTimeOffset + timeSpan3;
			}
			catch (Exception inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		public DateTimeOffset GetGeneralizedTime(bool disallowFractions = false)
		{
			return GetGeneralizedTime(Asn1Tag.GeneralizedTime, disallowFractions);
		}

		public DateTimeOffset GetGeneralizedTime(Asn1Tag expectedTag, bool disallowFractions = false)
		{
			byte[] rented = null;
			int bytesRead;
			ReadOnlySpan<byte> octetStringContents = GetOctetStringContents(expectedTag, UniversalTagNumber.GeneralizedTime, out bytesRead, ref rented);
			DateTimeOffset result = ParseGeneralizedTime(_ruleSet, octetStringContents, disallowFractions);
			if (rented != null)
			{
				Array.Clear(rented, 0, octetStringContents.Length);
				ArrayPool<byte>.Shared.Return(rented);
			}
			_data = _data.Slice(bytesRead);
			return result;
		}

		private ReadOnlySpan<byte> GetOctetStringContents(Asn1Tag expectedTag, UniversalTagNumber universalTagNumber, out int bytesRead, ref byte[] rented, Span<byte> tmpSpace = default(Span<byte>))
		{
			if (TryGetPrimitiveOctetStringBytes(expectedTag, out var _, out var contentLength, out var headerLength, out var contents, universalTagNumber))
			{
				bytesRead = headerLength + contents.Length;
				return contents.Span;
			}
			ReadOnlyMemory<byte> source = Slice(_data, headerLength, contentLength);
			bool isIndefinite = !contentLength.HasValue;
			int num = CountConstructedOctetString(source, isIndefinite);
			if (tmpSpace.Length < num)
			{
				rented = ArrayPool<byte>.Shared.Rent(num);
				tmpSpace = rented;
			}
			CopyConstructedOctetString(source, tmpSpace, isIndefinite, out var bytesRead2, out var bytesWritten);
			bytesRead = headerLength + bytesRead2;
			return tmpSpace.Slice(0, bytesWritten);
		}

		private static ReadOnlySpan<byte> SliceAtMost(ReadOnlySpan<byte> source, int longestPermitted)
		{
			return source[..Math.Min(longestPermitted, source.Length)];
		}

		private static ReadOnlySpan<byte> Slice(ReadOnlySpan<byte> source, int offset, int length)
		{
			if (length < 0 || source.Length - offset < length)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return source.Slice(offset, length);
		}

		private static ReadOnlyMemory<byte> Slice(ReadOnlyMemory<byte> source, int offset, int? length)
		{
			if (!length.HasValue)
			{
				return source.Slice(offset);
			}
			int value = length.Value;
			if (value < 0 || source.Length - offset < value)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return source.Slice(offset, value);
		}

		private static void CheckEncodingRules(AsnEncodingRules ruleSet)
		{
			if (ruleSet != AsnEncodingRules.BER && ruleSet != AsnEncodingRules.CER && ruleSet != AsnEncodingRules.DER)
			{
				throw new ArgumentOutOfRangeException("ruleSet");
			}
		}

		private static void CheckExpectedTag(Asn1Tag tag, Asn1Tag expectedTag, UniversalTagNumber tagNumber)
		{
			if (expectedTag.TagClass == TagClass.Universal && expectedTag.TagValue != (int)tagNumber)
			{
				throw new ArgumentException("Tags with TagClass Universal must have the appropriate TagValue value for the data type being read or written.", "expectedTag");
			}
			if (expectedTag.TagClass != tag.TagClass || expectedTag.TagValue != tag.TagValue)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
		}

		private static void CheckCharacterStringEncodingType(UniversalTagNumber encodingType)
		{
			switch (encodingType)
			{
			case UniversalTagNumber.UTF8String:
			case UniversalTagNumber.NumericString:
			case UniversalTagNumber.PrintableString:
			case UniversalTagNumber.TeletexString:
			case UniversalTagNumber.VideotexString:
			case UniversalTagNumber.IA5String:
			case UniversalTagNumber.GraphicString:
			case UniversalTagNumber.VisibleString:
			case UniversalTagNumber.GeneralString:
			case UniversalTagNumber.UniversalString:
			case UniversalTagNumber.BMPString:
				return;
			}
			throw new ArgumentOutOfRangeException("encodingType");
		}
	}
	internal sealed class AsnWriter : IDisposable
	{
		private class ArrayIndexSetOfValueComparer : IComparer<(int, int)>
		{
			private readonly byte[] _data;

			public ArrayIndexSetOfValueComparer(byte[] data)
			{
				_data = data;
			}

			public int Compare((int, int) x, (int, int) y)
			{
				int item = x.Item1;
				int item2 = x.Item2;
				int item3 = y.Item1;
				int item4 = y.Item2;
				int num = SetOfValueComparer.Instance.Compare(new ReadOnlyMemory<byte>(_data, item, item2), new ReadOnlyMemory<byte>(_data, item3, item4));
				if (num == 0)
				{
					return item - item3;
				}
				return num;
			}
		}

		private byte[] _buffer;

		private int _offset;

		private Stack<(Asn1Tag, int)> _nestingStack;

		public AsnEncodingRules RuleSet { get; }

		public AsnWriter(AsnEncodingRules ruleSet)
		{
			if (ruleSet != AsnEncodingRules.BER && ruleSet != AsnEncodingRules.CER && ruleSet != AsnEncodingRules.DER)
			{
				throw new ArgumentOutOfRangeException("ruleSet");
			}
			RuleSet = ruleSet;
		}

		public void Dispose()
		{
			_nestingStack = null;
			if (_buffer != null)
			{
				Array.Clear(_buffer, 0, _offset);
				ArrayPool<byte>.Shared.Return(_buffer);
				_buffer = null;
				_offset = 0;
			}
		}

		private void EnsureWriteCapacity(int pendingCount)
		{
			if (pendingCount < 0)
			{
				throw new OverflowException();
			}
			if (_buffer == null || _buffer.Length - _offset < pendingCount)
			{
				int num = checked(_offset + pendingCount + 1023) / 1024;
				byte[] array = ArrayPool<byte>.Shared.Rent(1024 * num);
				if (_buffer != null)
				{
					Buffer.BlockCopy(_buffer, 0, array, 0, _offset);
					Array.Clear(_buffer, 0, _offset);
					ArrayPool<byte>.Shared.Return(_buffer);
				}
				_buffer = array;
			}
		}

		private void WriteTag(Asn1Tag tag)
		{
			int num = tag.CalculateEncodedSize();
			EnsureWriteCapacity(num);
			if (!tag.TryWrite(_buffer.AsSpan(_offset, num), out var bytesWritten) || bytesWritten != num)
			{
				throw new CryptographicException();
			}
			_offset += num;
		}

		private void WriteLength(int length)
		{
			if (length == -1)
			{
				EnsureWriteCapacity(1);
				_buffer[_offset] = 128;
				_offset++;
				return;
			}
			if (length < 128)
			{
				EnsureWriteCapacity(1 + length);
				_buffer[_offset] = (byte)length;
				_offset++;
				return;
			}
			int encodedLengthSubsequentByteCount = GetEncodedLengthSubsequentByteCount(length);
			EnsureWriteCapacity(encodedLengthSubsequentByteCount + 1 + length);
			_buffer[_offset] = (byte)(0x80 | encodedLengthSubsequentByteCount);
			int num = _offset + encodedLengthSubsequentByteCount;
			int num2 = length;
			do
			{
				_buffer[num] = (byte)num2;
				num2 >>= 8;
				num--;
			}
			while (num2 > 0);
			_offset += encodedLengthSubsequentByteCount + 1;
		}

		private static int GetEncodedLengthSubsequentByteCount(int length)
		{
			if (length <= 127)
			{
				return 0;
			}
			if (length <= 255)
			{
				return 1;
			}
			if (length <= 65535)
			{
				return 2;
			}
			if (length <= 16777215)
			{
				return 3;
			}
			return 4;
		}

		public void WriteEncodedValue(ReadOnlyMemory<byte> preEncodedValue)
		{
			AsnReader asnReader = new AsnReader(preEncodedValue, RuleSet);
			asnReader.GetEncodedValue();
			if (asnReader.HasData)
			{
				throw new ArgumentException("The input to WriteEncodedValue must represent a single encoded value with no trailing data.", "preEncodedValue");
			}
			EnsureWriteCapacity(preEncodedValue.Length);
			preEncodedValue.Span.CopyTo(_buffer.AsSpan(_offset));
			_offset += preEncodedValue.Length;
		}

		private void WriteEndOfContents()
		{
			EnsureWriteCapacity(2);
			_buffer[_offset++] = 0;
			_buffer[_offset++] = 0;
		}

		public void WriteBoolean(bool value)
		{
			WriteBooleanCore(Asn1Tag.Boolean, value);
		}

		public void WriteBoolean(Asn1Tag tag, bool value)
		{
			CheckUniversalTag(tag, UniversalTagNumber.Boolean);
			WriteBooleanCore(tag.AsPrimitive(), value);
		}

		private void WriteBooleanCore(Asn1Tag tag, bool value)
		{
			WriteTag(tag);
			WriteLength(1);
			_buffer[_offset] = (byte)(value ? 255u : 0u);
			_offset++;
		}

		public void WriteInteger(long value)
		{
			WriteIntegerCore(Asn1Tag.Integer, value);
		}

		public void WriteInteger(ulong value)
		{
			WriteNonNegativeIntegerCore(Asn1Tag.Integer, value);
		}

		public void WriteInteger(BigInteger value)
		{
			WriteIntegerCore(Asn1Tag.Integer, value);
		}

		public void WriteInteger(ReadOnlySpan<byte> value)
		{
			WriteIntegerCore(Asn1Tag.Integer, value);
		}

		public void WriteInteger(Asn1Tag tag, long value)
		{
			CheckUniversalTag(tag, UniversalTagNumber.Integer);
			WriteIntegerCore(tag.AsPrimitive(), value);
		}

		private void WriteIntegerCore(Asn1Tag tag, long value)
		{
			if (value >= 0)
			{
				WriteNonNegativeIntegerCore(tag, (ulong)value);
				return;
			}
			int num = ((value >= -128) ? 1 : ((value >= -32768) ? 2 : ((value >= -8388608) ? 3 : ((value >= int.MinValue) ? 4 : ((value >= -549755813888L) ? 5 : ((value >= -140737488355328L) ? 6 : ((value < -36028797018963968L) ? 8 : 7)))))));
			WriteTag(tag);
			WriteLength(num);
			long num2 = value;
			int num3 = _offset + num - 1;
			do
			{
				_buffer[num3] = (byte)num2;
				num2 >>= 8;
				num3--;
			}
			while (num3 >= _offset);
			_offset += num;
		}

		public void WriteInteger(Asn1Tag tag, ulong value)
		{
			CheckUniversalTag(tag, UniversalTagNumber.Integer);
			WriteNonNegativeIntegerCore(tag.AsPrimitive(), value);
		}

		private void WriteNonNegativeIntegerCore(Asn1Tag tag, ulong value)
		{
			int num = ((value < 128) ? 1 : ((value < 32768) ? 2 : ((value < 8388608) ? 3 : ((value < 2147483648u) ? 4 : ((value < 549755813888L) ? 5 : ((value < 140737488355328L) ? 6 : ((value < 36028797018963968L) ? 7 : ((value >= 9223372036854775808uL) ? 9 : 8))))))));
			WriteTag(tag);
			WriteLength(num);
			ulong num2 = value;
			int num3 = _offset + num - 1;
			do
			{
				_buffer[num3] = (byte)num2;
				num2 >>= 8;
				num3--;
			}
			while (num3 >= _offset);
			_offset += num;
		}

		public void WriteInteger(Asn1Tag tag, BigInteger value)
		{
			CheckUniversalTag(tag, UniversalTagNumber.Integer);
			WriteIntegerCore(tag.AsPrimitive(), value);
		}

		public void WriteInteger(Asn1Tag tag, ReadOnlySpan<byte> value)
		{
			CheckUniversalTag(tag, UniversalTagNumber.Integer);
			WriteIntegerCore(tag.AsPrimitive(), value);
		}

		private void WriteIntegerCore(Asn1Tag tag, ReadOnlySpan<byte> value)
		{
			if (value.IsEmpty)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (value.Length > 1)
			{
				ushort num = (ushort)((ushort)((value[0] << 8) | value[1]) & 0xFF80);
				if (num == 0 || num == 65408)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
			}
			WriteTag(tag);
			WriteLength(value.Length);
			value.CopyTo(_buffer.AsSpan(_offset));
			_offset += value.Length;
		}

		private void WriteIntegerCore(Asn1Tag tag, BigInteger value)
		{
			byte[] array = value.ToByteArray();
			Array.Reverse(array);
			WriteTag(tag);
			WriteLength(array.Length);
			Buffer.BlockCopy(array, 0, _buffer, _offset, array.Length);
			_offset += array.Length;
		}

		public void WriteBitString(ReadOnlySpan<byte> bitString, int unusedBitCount = 0)
		{
			WriteBitStringCore(Asn1Tag.PrimitiveBitString, bitString, unusedBitCount);
		}

		public void WriteBitString(Asn1Tag tag, ReadOnlySpan<byte> bitString, int unusedBitCount = 0)
		{
			CheckUniversalTag(tag, UniversalTagNumber.BitString);
			WriteBitStringCore(tag, bitString, unusedBitCount);
		}

		private void WriteBitStringCore(Asn1Tag tag, ReadOnlySpan<byte> bitString, int unusedBitCount)
		{
			if (unusedBitCount < 0 || unusedBitCount > 7)
			{
				throw new ArgumentOutOfRangeException("unusedBitCount", unusedBitCount, "Unused bit count must be between 0 and 7, inclusive.");
			}
			if (bitString.Length == 0 && unusedBitCount != 0)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			int num = (1 << unusedBitCount) - 1;
			if ((((!bitString.IsEmpty) ? bitString[bitString.Length - 1] : 0) & num) != 0)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (RuleSet == AsnEncodingRules.CER && bitString.Length >= 1000)
			{
				WriteConstructedCerBitString(tag, bitString, unusedBitCount);
				return;
			}
			WriteTag(tag.AsPrimitive());
			WriteLength(bitString.Length + 1);
			_buffer[_offset] = (byte)unusedBitCount;
			_offset++;
			bitString.CopyTo(_buffer.AsSpan(_offset));
			_offset += bitString.Length;
		}

		private void WriteConstructedCerBitString(Asn1Tag tag, ReadOnlySpan<byte> payload, int unusedBitCount)
		{
			WriteTag(tag.AsConstructed());
			WriteLength(-1);
			int result;
			int num = Math.DivRem(payload.Length, 999, out result);
			int num2 = ((result != 0) ? (3 + result + GetEncodedLengthSubsequentByteCount(result)) : 0);
			int pendingCount = num * 1004 + num2 + 2;
			EnsureWriteCapacity(pendingCount);
			_ = _buffer;
			_ = _offset;
			ReadOnlySpan<byte> readOnlySpan = payload;
			Asn1Tag primitiveBitString = Asn1Tag.PrimitiveBitString;
			Span<byte> destination;
			while (readOnlySpan.Length > 999)
			{
				WriteTag(primitiveBitString);
				WriteLength(1000);
				_buffer[_offset] = 0;
				_offset++;
				destination = _buffer.AsSpan(_offset);
				readOnlySpan.Slice(0, 999).CopyTo(destination);
				readOnlySpan = readOnlySpan.Slice(999);
				_offset += 999;
			}
			WriteTag(primitiveBitString);
			WriteLength(readOnlySpan.Length + 1);
			_buffer[_offset] = (byte)unusedBitCount;
			_offset++;
			destination = _buffer.AsSpan(_offset);
			readOnlySpan.CopyTo(destination);
			_offset += readOnlySpan.Length;
			WriteEndOfContents();
		}

		public void WriteNamedBitList(object enumValue)
		{
			if (enumValue == null)
			{
				throw new ArgumentNullException("enumValue");
			}
			WriteNamedBitList(Asn1Tag.PrimitiveBitString, enumValue);
		}

		public void WriteNamedBitList<TEnum>(TEnum enumValue) where TEnum : struct
		{
			WriteNamedBitList(Asn1Tag.PrimitiveBitString, enumValue);
		}

		public void WriteNamedBitList(Asn1Tag tag, object enumValue)
		{
			if (enumValue == null)
			{
				throw new ArgumentNullException("enumValue");
			}
			WriteNamedBitList(tag, enumValue.GetType(), enumValue);
		}

		public void WriteNamedBitList<TEnum>(Asn1Tag tag, TEnum enumValue) where TEnum : struct
		{
			WriteNamedBitList(tag, typeof(TEnum), enumValue);
		}

		private void WriteNamedBitList(Asn1Tag tag, Type tEnum, object enumValue)
		{
			Type enumUnderlyingType = tEnum.GetEnumUnderlyingType();
			if (!tEnum.IsDefined(typeof(FlagsAttribute), inherit: false))
			{
				throw new ArgumentException("Named bit list operations require an enum with the [Flags] attribute.", "tEnum");
			}
			ulong integralValue = ((!(enumUnderlyingType == typeof(ulong))) ? ((ulong)Convert.ToInt64(enumValue)) : Convert.ToUInt64(enumValue));
			WriteNamedBitList(tag, integralValue);
		}

		private void WriteNamedBitList(Asn1Tag tag, ulong integralValue)
		{
			Span<byte> span = stackalloc byte[8];
			span.Clear();
			int num = -1;
			int num2 = 0;
			while (integralValue != 0L)
			{
				if ((integralValue & 1) != 0L)
				{
					span[num2 / 8] |= (byte)(128 >> num2 % 8);
					num = num2;
				}
				integralValue >>= 1;
				num2++;
			}
			if (num < 0)
			{
				WriteBitString(tag, ReadOnlySpan<byte>.Empty);
				return;
			}
			int length = num / 8 + 1;
			int unusedBitCount = 7 - num % 8;
			WriteBitString(tag, span.Slice(0, length), unusedBitCount);
		}

		public void WriteOctetString(ReadOnlySpan<byte> octetString)
		{
			WriteOctetString(Asn1Tag.PrimitiveOctetString, octetString);
		}

		public void WriteOctetString(Asn1Tag tag, ReadOnlySpan<byte> octetString)
		{
			CheckUniversalTag(tag, UniversalTagNumber.OctetString);
			WriteOctetStringCore(tag, octetString);
		}

		private void WriteOctetStringCore(Asn1Tag tag, ReadOnlySpan<byte> octetString)
		{
			if (RuleSet == AsnEncodingRules.CER && octetString.Length > 1000)
			{
				WriteConstructedCerOctetString(tag, octetString);
				return;
			}
			WriteTag(tag.AsPrimitive());
			WriteLength(octetString.Length);
			octetString.CopyTo(_buffer.AsSpan(_offset));
			_offset += octetString.Length;
		}

		private void WriteConstructedCerOctetString(Asn1Tag tag, ReadOnlySpan<byte> payload)
		{
			WriteTag(tag.AsConstructed());
			WriteLength(-1);
			int result;
			int num = Math.DivRem(payload.Length, 1000, out result);
			int num2 = ((result != 0) ? (2 + result + GetEncodedLengthSubsequentByteCount(result)) : 0);
			int pendingCount = num * 1004 + num2 + 2;
			EnsureWriteCapacity(pendingCount);
			_ = _buffer;
			_ = _offset;
			ReadOnlySpan<byte> readOnlySpan = payload;
			Asn1Tag primitiveOctetString = Asn1Tag.PrimitiveOctetString;
			Span<byte> destination;
			while (readOnlySpan.Length > 1000)
			{
				WriteTag(primitiveOctetString);
				WriteLength(1000);
				destination = _buffer.AsSpan(_offset);
				readOnlySpan.Slice(0, 1000).CopyTo(destination);
				_offset += 1000;
				readOnlySpan = readOnlySpan.Slice(1000);
			}
			WriteTag(primitiveOctetString);
			WriteLength(readOnlySpan.Length);
			destination = _buffer.AsSpan(_offset);
			readOnlySpan.CopyTo(destination);
			_offset += readOnlySpan.Length;
			WriteEndOfContents();
		}

		public void WriteNull()
		{
			WriteNullCore(Asn1Tag.Null);
		}

		public void WriteNull(Asn1Tag tag)
		{
			CheckUniversalTag(tag, UniversalTagNumber.Null);
			WriteNullCore(tag.AsPrimitive());
		}

		private void WriteNullCore(Asn1Tag tag)
		{
			WriteTag(tag);
			WriteLength(0);
		}

		public void WriteObjectIdentifier(Oid oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			WriteObjectIdentifier(oid.Value);
		}

		public void WriteObjectIdentifier(string oidValue)
		{
			if (oidValue == null)
			{
				throw new ArgumentNullException("oidValue");
			}
			WriteObjectIdentifier(oidValue.AsSpan());
		}

		public void WriteObjectIdentifier(ReadOnlySpan<char> oidValue)
		{
			WriteObjectIdentifierCore(Asn1Tag.ObjectIdentifier, oidValue);
		}

		public void WriteObjectIdentifier(Asn1Tag tag, Oid oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			WriteObjectIdentifier(tag, oid.Value);
		}

		public void WriteObjectIdentifier(Asn1Tag tag, string oidValue)
		{
			if (oidValue == null)
			{
				throw new ArgumentNullException("oidValue");
			}
			WriteObjectIdentifier(tag, oidValue.AsSpan());
		}

		public void WriteObjectIdentifier(Asn1Tag tag, ReadOnlySpan<char> oidValue)
		{
			CheckUniversalTag(tag, UniversalTagNumber.ObjectIdentifier);
			WriteObjectIdentifierCore(tag.AsPrimitive(), oidValue);
		}

		private void WriteObjectIdentifierCore(Asn1Tag tag, ReadOnlySpan<char> oidValue)
		{
			if (oidValue.Length < 3)
			{
				throw new CryptographicException("The OID value was invalid.");
			}
			if (oidValue[1] != '.')
			{
				throw new CryptographicException("The OID value was invalid.");
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(oidValue.Length / 2);
			int num = 0;
			try
			{
				int num2 = oidValue[0] switch
				{
					'0' => 0, 
					'1' => 1, 
					'2' => 2, 
					_ => throw new CryptographicException("The OID value was invalid."), 
				};
				ReadOnlySpan<char> oidValue2 = oidValue.Slice(2);
				BigInteger subIdentifier = ParseSubIdentifier(ref oidValue2);
				subIdentifier += (BigInteger)(40 * num2);
				int num3 = EncodeSubIdentifier(array.AsSpan(num), ref subIdentifier);
				num += num3;
				while (!oidValue2.IsEmpty)
				{
					subIdentifier = ParseSubIdentifier(ref oidValue2);
					num3 = EncodeSubIdentifier(array.AsSpan(num), ref subIdentifier);
					num += num3;
				}
				WriteTag(tag);
				WriteLength(num);
				Buffer.BlockCopy(array, 0, _buffer, _offset, num);
				_offset += num;
			}
			finally
			{
				Array.Clear(array, 0, num);
				ArrayPool<byte>.Shared.Return(array);
			}
		}

		private static BigInteger ParseSubIdentifier(ref ReadOnlySpan<char> oidValue)
		{
			int num = oidValue.IndexOf('.');
			if (num == -1)
			{
				num = oidValue.Length;
			}
			else if (num == oidValue.Length - 1)
			{
				throw new CryptographicException("The OID value was invalid.");
			}
			BigInteger zero = BigInteger.Zero;
			for (int i = 0; i < num; i++)
			{
				if (i > 0 && zero == 0L)
				{
					throw new CryptographicException("The OID value was invalid.");
				}
				zero *= (BigInteger)10;
				zero += (BigInteger)AtoI(oidValue[i]);
			}
			oidValue = oidValue.Slice(Math.Min(oidValue.Length, num + 1));
			return zero;
		}

		private static int AtoI(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return c - 48;
			}
			throw new CryptographicException("The OID value was invalid.");
		}

		private static int EncodeSubIdentifier(Span<byte> dest, ref BigInteger subIdentifier)
		{
			if (subIdentifier.IsZero)
			{
				dest[0] = 0;
				return 1;
			}
			BigInteger bigInteger = subIdentifier;
			int num = 0;
			do
			{
				byte b = (byte)(bigInteger & 127);
				if (subIdentifier != bigInteger)
				{
					b |= 0x80;
				}
				bigInteger >>= 7;
				dest[num] = b;
				num++;
			}
			while (bigInteger != BigInteger.Zero);
			Reverse(dest.Slice(0, num));
			return num;
		}

		public void WriteEnumeratedValue(object enumValue)
		{
			if (enumValue == null)
			{
				throw new ArgumentNullException("enumValue");
			}
			WriteEnumeratedValue(Asn1Tag.Enumerated, enumValue);
		}

		public void WriteEnumeratedValue<TEnum>(TEnum value) where TEnum : struct
		{
			WriteEnumeratedValue(Asn1Tag.Enumerated, value);
		}

		public void WriteEnumeratedValue(Asn1Tag tag, object enumValue)
		{
			if (enumValue == null)
			{
				throw new ArgumentNullException("enumValue");
			}
			WriteEnumeratedValue(tag.AsPrimitive(), enumValue.GetType(), enumValue);
		}

		public void WriteEnumeratedValue<TEnum>(Asn1Tag tag, TEnum value) where TEnum : struct
		{
			WriteEnumeratedValue(tag.AsPrimitive(), typeof(TEnum), value);
		}

		private void WriteEnumeratedValue(Asn1Tag tag, Type tEnum, object enumValue)
		{
			CheckUniversalTag(tag, UniversalTagNumber.Enumerated);
			Type enumUnderlyingType = tEnum.GetEnumUnderlyingType();
			if (tEnum.IsDefined(typeof(FlagsAttribute), inherit: false))
			{
				throw new ArgumentException("ASN.1 Enumerated values only apply to enum types without the [Flags] attribute.", "tEnum");
			}
			if (enumUnderlyingType == typeof(ulong))
			{
				ulong value = Convert.ToUInt64(enumValue);
				WriteNonNegativeIntegerCore(tag, value);
			}
			else
			{
				long value2 = Convert.ToInt64(enumValue);
				WriteIntegerCore(tag, value2);
			}
		}

		public void PushSequence()
		{
			PushSequenceCore(Asn1Tag.Sequence);
		}

		public void PushSequence(Asn1Tag tag)
		{
			CheckUniversalTag(tag, UniversalTagNumber.Sequence);
			PushSequenceCore(tag.AsConstructed());
		}

		private void PushSequenceCore(Asn1Tag tag)
		{
			PushTag(tag.AsConstructed());
		}

		public void PopSequence()
		{
			PopSequence(Asn1Tag.Sequence);
		}

		public void PopSequence(Asn1Tag tag)
		{
			CheckUniversalTag(tag, UniversalTagNumber.Sequence);
			PopSequenceCore(tag.AsConstructed());
		}

		private void PopSequenceCore(Asn1Tag tag)
		{
			PopTag(tag);
		}

		public void PushSetOf()
		{
			PushSetOf(Asn1Tag.SetOf);
		}

		public void PushSetOf(Asn1Tag tag)
		{
			CheckUniversalTag(tag, UniversalTagNumber.Set);
			PushSetOfCore(tag.AsConstructed());
		}

		private void PushSetOfCore(Asn1Tag tag)
		{
			PushTag(tag);
		}

		public void PopSetOf()
		{
			PopSetOfCore(Asn1Tag.SetOf);
		}

		public void PopSetOf(Asn1Tag tag)
		{
			CheckUniversalTag(tag, UniversalTagNumber.Set);
			PopSetOfCore(tag.AsConstructed());
		}

		private void PopSetOfCore(Asn1Tag tag)
		{
			bool sortContents = RuleSet == AsnEncodingRules.CER || RuleSet == AsnEncodingRules.DER;
			PopTag(tag, sortContents);
		}

		public void WriteUtcTime(DateTimeOffset value)
		{
			WriteUtcTimeCore(Asn1Tag.UtcTime, value);
		}

		public void WriteUtcTime(Asn1Tag tag, DateTimeOffset value)
		{
			CheckUniversalTag(tag, UniversalTagNumber.UtcTime);
			WriteUtcTimeCore(tag.AsPrimitive(), value);
		}

		public void WriteUtcTime(DateTimeOffset value, int minLegalYear)
		{
			if (minLegalYear <= value.Year && value.Year < minLegalYear + 100)
			{
				WriteUtcTime(value);
				return;
			}
			throw new ArgumentOutOfRangeException("value");
		}

		private void WriteUtcTimeCore(Asn1Tag tag, DateTimeOffset value)
		{
			WriteTag(tag);
			WriteLength(13);
			DateTimeOffset dateTimeOffset = value.ToUniversalTime();
			int year = dateTimeOffset.Year;
			int month = dateTimeOffset.Month;
			int day = dateTimeOffset.Day;
			int hour = dateTimeOffset.Hour;
			int minute = dateTimeOffset.Minute;
			int second = dateTimeOffset.Second;
			Span<byte> span = _buffer.AsSpan(_offset);
			StandardFormat format = new StandardFormat('D', 2);
			if (!Utf8Formatter.TryFormat(year % 100, span.Slice(0, 2), out var bytesWritten, format) || !Utf8Formatter.TryFormat(month, span.Slice(2, 2), out bytesWritten, format) || !Utf8Formatter.TryFormat(day, span.Slice(4, 2), out bytesWritten, format) || !Utf8Formatter.TryFormat(hour, span.Slice(6, 2), out bytesWritten, format) || !Utf8Formatter.TryFormat(minute, span.Slice(8, 2), out bytesWritten, format) || !Utf8Formatter.TryFormat(second, span.Slice(10, 2), out bytesWritten, format))
			{
				throw new CryptographicException();
			}
			_buffer[_offset + 12] = 90;
			_offset += 13;
		}

		public void WriteGeneralizedTime(DateTimeOffset value, bool omitFractionalSeconds = false)
		{
			WriteGeneralizedTimeCore(Asn1Tag.GeneralizedTime, value, omitFractionalSeconds);
		}

		public void WriteGeneralizedTime(Asn1Tag tag, DateTimeOffset value, bool omitFractionalSeconds = false)
		{
			CheckUniversalTag(tag, UniversalTagNumber.GeneralizedTime);
			WriteGeneralizedTimeCore(tag.AsPrimitive(), value, omitFractionalSeconds);
		}

		private void WriteGeneralizedTimeCore(Asn1Tag tag, DateTimeOffset value, bool omitFractionalSeconds)
		{
			DateTimeOffset dateTimeOffset = value.ToUniversalTime();
			if (dateTimeOffset.Year > 9999)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			Span<byte> destination = default(Span<byte>);
			if (!omitFractionalSeconds)
			{
				long num = dateTimeOffset.Ticks % 10000000;
				if (num != 0L)
				{
					destination = stackalloc byte[9];
					if (!Utf8Formatter.TryFormat((decimal)num / 10000000m, destination, out var bytesWritten, new StandardFormat('G')))
					{
						throw new CryptographicException();
					}
					destination = destination.Slice(1, bytesWritten - 1);
				}
			}
			int length = 15 + destination.Length;
			WriteTag(tag);
			WriteLength(length);
			int year = dateTimeOffset.Year;
			int month = dateTimeOffset.Month;
			int day = dateTimeOffset.Day;
			int hour = dateTimeOffset.Hour;
			int minute = dateTimeOffset.Minute;
			int second = dateTimeOffset.Second;
			Span<byte> span = _buffer.AsSpan(_offset);
			StandardFormat format = new StandardFormat('D', 4);
			StandardFormat format2 = new StandardFormat('D', 2);
			if (!Utf8Formatter.TryFormat(year, span.Slice(0, 4), out var bytesWritten2, format) || !Utf8Formatter.TryFormat(month, span.Slice(4, 2), out bytesWritten2, format2) || !Utf8Formatter.TryFormat(day, span.Slice(6, 2), out bytesWritten2, format2) || !Utf8Formatter.TryFormat(hour, span.Slice(8, 2), out bytesWritten2, format2) || !Utf8Formatter.TryFormat(minute, span.Slice(10, 2), out bytesWritten2, format2) || !Utf8Formatter.TryFormat(second, span.Slice(12, 2), out bytesWritten2, format2))
			{
				throw new CryptographicException();
			}
			_offset += 14;
			destination.CopyTo(span.Slice(14));
			_offset += destination.Length;
			_buffer[_offset] = 90;
			_offset++;
		}

		public bool TryEncode(Span<byte> dest, out int bytesWritten)
		{
			Stack<(Asn1Tag, int)> nestingStack = _nestingStack;
			if (nestingStack != null && nestingStack.Count != 0)
			{
				throw new InvalidOperationException("Encode cannot be called while a Sequence or SetOf is still open.");
			}
			if (dest.Length < _offset)
			{
				bytesWritten = 0;
				return false;
			}
			if (_offset == 0)
			{
				bytesWritten = 0;
				return true;
			}
			bytesWritten = _offset;
			_buffer.AsSpan(0, _offset).CopyTo(dest);
			return true;
		}

		public byte[] Encode()
		{
			Stack<(Asn1Tag, int)> nestingStack = _nestingStack;
			if (nestingStack != null && nestingStack.Count != 0)
			{
				throw new InvalidOperationException("Encode cannot be called while a Sequence or SetOf is still open.");
			}
			if (_offset == 0)
			{
				return Array.Empty<byte>();
			}
			return _buffer.AsSpan(0, _offset).ToArray();
		}

		public ReadOnlySpan<byte> EncodeAsSpan()
		{
			Stack<(Asn1Tag, int)> nestingStack = _nestingStack;
			if (nestingStack != null && nestingStack.Count != 0)
			{
				throw new InvalidOperationException("Encode cannot be called while a Sequence or SetOf is still open.");
			}
			if (_offset == 0)
			{
				return ReadOnlySpan<byte>.Empty;
			}
			return new ReadOnlySpan<byte>(_buffer, 0, _offset);
		}

		private void PushTag(Asn1Tag tag)
		{
			if (_nestingStack == null)
			{
				_nestingStack = new Stack<(Asn1Tag, int)>();
			}
			WriteTag(tag);
			_nestingStack.Push((tag, _offset));
			WriteLength(-1);
		}

		private void PopTag(Asn1Tag tag, bool sortContents = false)
		{
			if (_nestingStack == null || _nestingStack.Count == 0)
			{
				throw new ArgumentException("Cannot pop the requested tag as it is not currently in progress.", "tag");
			}
			var (asn1Tag, num) = _nestingStack.Peek();
			if (asn1Tag != tag)
			{
				throw new ArgumentException("Cannot pop the requested tag as it is not currently in progress.", "tag");
			}
			_nestingStack.Pop();
			if (sortContents)
			{
				SortContents(_buffer, num + 1, _offset);
			}
			if (RuleSet == AsnEncodingRules.CER)
			{
				WriteEndOfContents();
				return;
			}
			int num2 = _offset - 1 - num;
			int encodedLengthSubsequentByteCount = GetEncodedLengthSubsequentByteCount(num2);
			if (encodedLengthSubsequentByteCount == 0)
			{
				_buffer[num] = (byte)num2;
				return;
			}
			EnsureWriteCapacity(encodedLengthSubsequentByteCount);
			int num3 = num + 1;
			Buffer.BlockCopy(_buffer, num3, _buffer, num3 + encodedLengthSubsequentByteCount, num2);
			int offset = _offset;
			_offset = num;
			WriteLength(num2);
			_offset = offset + encodedLengthSubsequentByteCount;
		}

		public void WriteCharacterString(UniversalTagNumber encodingType, string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			WriteCharacterString(encodingType, str.AsSpan());
		}

		public void WriteCharacterString(UniversalTagNumber encodingType, ReadOnlySpan<char> str)
		{
			Encoding encoding = AsnCharacterStringEncodings.GetEncoding(encodingType);
			WriteCharacterStringCore(new Asn1Tag(encodingType), encoding, str);
		}

		public void WriteCharacterString(Asn1Tag tag, UniversalTagNumber encodingType, string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			WriteCharacterString(tag, encodingType, str.AsSpan());
		}

		public void WriteCharacterString(Asn1Tag tag, UniversalTagNumber encodingType, ReadOnlySpan<char> str)
		{
			CheckUniversalTag(tag, encodingType);
			Encoding encoding = AsnCharacterStringEncodings.GetEncoding(encodingType);
			WriteCharacterStringCore(tag, encoding, str);
		}

		private unsafe void WriteCharacterStringCore(Asn1Tag tag, Encoding encoding, ReadOnlySpan<char> str)
		{
			int num = -1;
			if (RuleSet == AsnEncodingRules.CER)
			{
				fixed (char* reference = &MemoryMarshal.GetReference(str))
				{
					num = encoding.GetByteCount(reference, str.Length);
					if (num > 1000)
					{
						WriteConstructedCerCharacterString(tag, encoding, str, num);
						return;
					}
				}
			}
			fixed (char* reference2 = &MemoryMarshal.GetReference(str))
			{
				if (num < 0)
				{
					num = encoding.GetByteCount(reference2, str.Length);
				}
				WriteTag(tag.AsPrimitive());
				WriteLength(num);
				Span<byte> span = _buffer.AsSpan(_offset, num);
				fixed (byte* reference3 = &MemoryMarshal.GetReference(span))
				{
					if (encoding.GetBytes(reference2, str.Length, reference3, span.Length) != num)
					{
						throw new InvalidOperationException();
					}
				}
				_offset += num;
			}
		}

		private unsafe void WriteConstructedCerCharacterString(Asn1Tag tag, Encoding encoding, ReadOnlySpan<char> str, int size)
		{
			byte[] array;
			fixed (char* reference = &MemoryMarshal.GetReference(str))
			{
				array = ArrayPool<byte>.Shared.Rent(size);
				fixed (byte* bytes = array)
				{
					if (encoding.GetBytes(reference, str.Length, bytes, array.Length) != size)
					{
						throw new InvalidOperationException();
					}
				}
			}
			WriteConstructedCerOctetString(tag, array.AsSpan(0, size));
			Array.Clear(array, 0, size);
			ArrayPool<byte>.Shared.Return(array);
		}

		private static void SortContents(byte[] buffer, int start, int end)
		{
			int num = end - start;
			if (num == 0)
			{
				return;
			}
			AsnReader asnReader = new AsnReader(new ReadOnlyMemory<byte>(buffer, start, num), AsnEncodingRules.BER);
			List<(int, int)> list = new List<(int, int)>();
			int num2 = start;
			while (asnReader.HasData)
			{
				ReadOnlyMemory<byte> encodedValue = asnReader.GetEncodedValue();
				list.Add((num2, encodedValue.Length));
				num2 += encodedValue.Length;
			}
			ArrayIndexSetOfValueComparer comparer = new ArrayIndexSetOfValueComparer(buffer);
			list.Sort(comparer);
			byte[] array = ArrayPool<byte>.Shared.Rent(num);
			num2 = 0;
			foreach (var (srcOffset, num3) in list)
			{
				Buffer.BlockCopy(buffer, srcOffset, array, num2, num3);
				num2 += num3;
			}
			Buffer.BlockCopy(array, 0, buffer, start, num);
			Array.Clear(array, 0, num);
			ArrayPool<byte>.Shared.Return(array);
		}

		internal static void Reverse(Span<byte> span)
		{
			int num = 0;
			int num2 = span.Length - 1;
			while (num < num2)
			{
				byte b = span[num];
				span[num] = span[num2];
				span[num2] = b;
				num++;
				num2--;
			}
		}

		private static void CheckUniversalTag(Asn1Tag tag, UniversalTagNumber universalTagNumber)
		{
			if (tag.TagClass == TagClass.Universal && tag.TagValue != (int)universalTagNumber)
			{
				throw new ArgumentException("Tags with TagClass Universal must have the appropriate TagValue value for the data type being read or written.", "tag");
			}
		}
	}
}
namespace Internal.Cryptography
{
	internal static class CryptoThrowHelper
	{
		private sealed class WindowsCryptographicException : CryptographicException
		{
			public WindowsCryptographicException(int hr, string message)
				: base(message)
			{
				base.HResult = hr;
			}
		}

		public static CryptographicException ToCryptographicException(this int hr)
		{
			string message = global::Interop.Kernel32.GetMessage(hr);
			return new WindowsCryptographicException(hr, message);
		}
	}
	internal abstract class DecryptorPal : IDisposable
	{
		public RecipientInfoCollection RecipientInfos { get; }

		internal DecryptorPal(RecipientInfoCollection recipientInfos)
		{
			RecipientInfos = recipientInfos;
		}

		public abstract ContentInfo TryDecrypt(RecipientInfo recipientInfo, X509Certificate2 cert, X509Certificate2Collection originatorCerts, X509Certificate2Collection extraStore, out Exception exception);

		public abstract void Dispose();
	}
	internal static class Helpers
	{
		private struct Certificate
		{
			internal TbsCertificateLite TbsCertificate;

			internal AlgorithmIdentifierAsn AlgorithmIdentifier;

			[BitString]
			internal ReadOnlyMemory<byte> SignatureValue;
		}

		private struct TbsCertificateLite
		{
			[ExpectedTag(0, ExplicitTag = true)]
			[DefaultValue(new byte[] { 160, 3, 2, 1, 0 })]
			internal int Version;

			[Integer]
			internal ReadOnlyMemory<byte> SerialNumber;

			internal AlgorithmIdentifierAsn AlgorithmIdentifier;

			[AnyValue]
			[ExpectedTag(TagClass.Universal, 16)]
			internal ReadOnlyMemory<byte> Issuer;

			[AnyValue]
			[ExpectedTag(TagClass.Universal, 16)]
			internal ReadOnlyMemory<byte> Validity;

			[AnyValue]
			[ExpectedTag(TagClass.Universal, 16)]
			internal ReadOnlyMemory<byte> Subject;

			[AnyValue]
			[ExpectedTag(TagClass.Universal, 16)]
			internal ReadOnlyMemory<byte> SubjectPublicKeyInfo;

			[BitString]
			[ExpectedTag(1)]
			[OptionalValue]
			internal ReadOnlyMemory<byte>? IssuerUniqueId;

			[OptionalValue]
			[BitString]
			[ExpectedTag(2)]
			internal ReadOnlyMemory<byte>? SubjectUniqueId;

			[ExpectedTag(3)]
			[AnyValue]
			[OptionalValue]
			internal ReadOnlyMemory<byte>? Extensions;
		}

		internal struct AsnSet<T>
		{
			[SetOf]
			public T[] SetData;
		}

		internal static void AppendData(this IncrementalHash hasher, ReadOnlySpan<byte> data)
		{
			hasher.AppendData(data.ToArray());
		}

		internal static HashAlgorithmName GetDigestAlgorithm(Oid oid)
		{
			return GetDigestAlgorithm(oid.Value);
		}

		internal static HashAlgorithmName GetDigestAlgorithm(string oidValue)
		{
			return oidValue switch
			{
				"1.2.840.113549.2.5" => HashAlgorithmName.MD5, 
				"1.3.14.3.2.26" => HashAlgorithmName.SHA1, 
				"2.16.840.1.101.3.4.2.1" => HashAlgorithmName.SHA256, 
				"2.16.840.1.101.3.4.2.2" => HashAlgorithmName.SHA384, 
				"2.16.840.1.101.3.4.2.3" => HashAlgorithmName.SHA512, 
				_ => throw new CryptographicException("'{0}' is not a known hash algorithm.", oidValue), 
			};
		}

		internal static string GetOidFromHashAlgorithm(HashAlgorithmName algName)
		{
			if (algName == HashAlgorithmName.MD5)
			{
				return "1.2.840.113549.2.5";
			}
			if (algName == HashAlgorithmName.SHA1)
			{
				return "1.3.14.3.2.26";
			}
			if (algName == HashAlgorithmName.SHA256)
			{
				return "2.16.840.1.101.3.4.2.1";
			}
			if (algName == HashAlgorithmName.SHA384)
			{
				return "2.16.840.1.101.3.4.2.2";
			}
			if (algName == HashAlgorithmName.SHA512)
			{
				return "2.16.840.1.101.3.4.2.3";
			}
			throw new CryptographicException("Unknown algorithm '{0}'.", algName.Name);
		}

		public static byte[] Resize(this byte[] a, int size)
		{
			Array.Resize(ref a, size);
			return a;
		}

		public static void RemoveAt<T>(ref T[] arr, int idx)
		{
			if (arr.Length == 1)
			{
				arr = Array.Empty<T>();
				return;
			}
			T[] array = new T[arr.Length - 1];
			if (idx != 0)
			{
				Array.Copy(arr, 0, array, 0, idx);
			}
			if (idx < array.Length)
			{
				Array.Copy(arr, idx + 1, array, idx, array.Length - idx);
			}
			arr = array;
		}

		public static T[] NormalizeSet<T>(T[] setItems, Action<byte[]> encodedValueProcessor = null)
		{
			byte[] array = AsnSerializer.Serialize(new AsnSet<T>
			{
				SetData = setItems
			}, AsnEncodingRules.DER).Encode();
			AsnSet<T> asnSet = AsnSerializer.Deserialize<AsnSet<T>>(array, AsnEncodingRules.DER);
			encodedValueProcessor?.Invoke(array);
			return asnSet.SetData;
		}

		internal static byte[] EncodeContentInfo<T>(T value, string contentType, AsnEncodingRules ruleSet = AsnEncodingRules.DER)
		{
			using AsnWriter asnWriter = AsnSerializer.Serialize(value, ruleSet);
			using AsnWriter asnWriter2 = AsnSerializer.Serialize(new ContentInfoAsn
			{
				ContentType = contentType,
				Content = asnWriter.Encode()
			}, ruleSet);
			return asnWriter2.Encode();
		}

		public static CmsRecipientCollection DeepCopy(this CmsRecipientCollection recipients)
		{
			CmsRecipientCollection cmsRecipientCollection = new CmsRecipientCollection();
			CmsRecipientEnumerator enumerator = recipients.GetEnumerator();
			while (enumerator.MoveNext())
			{
				CmsRecipient current = enumerator.Current;
				X509Certificate2 certificate = current.Certificate;
				CmsRecipient recipient = new CmsRecipient(certificate: new X509Certificate2(certificate.Handle), recipientIdentifierType: current.RecipientIdentifierType);
				cmsRecipientCollection.Add(recipient);
				GC.KeepAlive(certificate);
			}
			return cmsRecipientCollection;
		}

		public static byte[] UnicodeToOctetString(this string s)
		{
			byte[] array = new byte[2 * (s.Length + 1)];
			Encoding.Unicode.GetBytes(s, 0, s.Length, array, 0);
			return array;
		}

		public static string OctetStringToUnicode(this byte[] octets)
		{
			if (octets.Length < 2)
			{
				return string.Empty;
			}
			return Encoding.Unicode.GetString(octets, 0, octets.Length - 2);
		}

		public static X509Certificate2Collection GetStoreCertificates(StoreName storeName, StoreLocation storeLocation, bool openExistingOnly)
		{
			using X509Store x509Store = new X509Store(storeName, storeLocation);
			OpenFlags openFlags = OpenFlags.IncludeArchived;
			if (openExistingOnly)
			{
				openFlags |= OpenFlags.OpenExistingOnly;
			}
			x509Store.Open(openFlags);
			return x509Store.Certificates;
		}

		public static X509Certificate2 TryFindMatchingCertificate(this X509Certificate2Collection certs, SubjectIdentifier recipientIdentifier)
		{
			switch (recipientIdentifier.Type)
			{
			case SubjectIdentifierType.IssuerAndSerialNumber:
			{
				X509IssuerSerial x509IssuerSerial = (X509IssuerSerial)recipientIdentifier.Value;
				byte[] ba2 = x509IssuerSerial.SerialNumber.ToSerialBytes();
				string issuerName = x509IssuerSerial.IssuerName;
				X509Certificate2Enumerator enumerator = certs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					X509Certificate2 current2 = enumerator.Current;
					if (AreByteArraysEqual(current2.GetSerialNumber(), ba2) && current2.Issuer == issuerName)
					{
						return current2;
					}
				}
				break;
			}
			case SubjectIdentifierType.SubjectKeyIdentifier:
			{
				byte[] ba = ((string)recipientIdentifier.Value).ToSkiBytes();
				X509Certificate2Enumerator enumerator = certs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					X509Certificate2 current = enumerator.Current;
					byte[] subjectKeyIdentifier = PkcsPal.Instance.GetSubjectKeyIdentifier(current);
					if (AreByteArraysEqual(ba, subjectKeyIdentifier))
					{
						return current;
					}
				}
				break;
			}
			default:
				throw new CryptographicException();
			}
			return null;
		}

		private static bool AreByteArraysEqual(byte[] ba1, byte[] ba2)
		{
			if (ba1.Length != ba2.Length)
			{
				return false;
			}
			for (int i = 0; i < ba1.Length; i++)
			{
				if (ba1[i] != ba2[i])
				{
					return false;
				}
			}
			return true;
		}

		private static byte[] ToSkiBytes(this string skiString)
		{
			return skiString.UpperHexStringToByteArray();
		}

		public static string ToSkiString(this byte[] skiBytes)
		{
			return ToUpperHexString(skiBytes);
		}

		public static string ToBigEndianHex(this ReadOnlySpan<byte> bytes)
		{
			return ToUpperHexString(bytes);
		}

		private static byte[] ToSerialBytes(this string serialString)
		{
			byte[] array = serialString.UpperHexStringToByteArray();
			Array.Reverse(array);
			return array;
		}

		public static string ToSerialString(this byte[] serialBytes)
		{
			serialBytes = serialBytes.CloneByteArray();
			Array.Reverse(serialBytes);
			return ToUpperHexString(serialBytes);
		}

		private static string ToUpperHexString(ReadOnlySpan<byte> ba)
		{
			StringBuilder stringBuilder = new StringBuilder(ba.Length * 2);
			for (int i = 0; i < ba.Length; i++)
			{
				stringBuilder.Append(ba[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		private static byte[] UpperHexStringToByteArray(this string normalizedString)
		{
			byte[] array = new byte[normalizedString.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				char c = normalizedString[i * 2];
				byte b = (byte)(UpperHexCharToNybble(c) << 4);
				c = normalizedString[i * 2 + 1];
				b |= UpperHexCharToNybble(c);
				array[i] = b;
			}
			return array;
		}

		private static byte UpperHexCharToNybble(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return (byte)(c - 48);
			}
			if (c >= 'A' && c <= 'F')
			{
				return (byte)(c - 65 + 10);
			}
			throw new CryptographicException();
		}

		public static Pkcs9AttributeObject CreateBestPkcs9AttributeObjectAvailable(Oid oid, byte[] encodedAttribute)
		{
			Pkcs9AttributeObject pkcs9AttributeObject = new Pkcs9AttributeObject(oid, encodedAttribute);
			switch (oid.Value)
			{
			case "1.3.6.1.4.1.311.88.2.1":
				pkcs9AttributeObject = Upgrade<Pkcs9DocumentName>(pkcs9AttributeObject);
				break;
			case "1.3.6.1.4.1.311.88.2.2":
				pkcs9AttributeObject = Upgrade<Pkcs9DocumentDescription>(pkcs9AttributeObject);
				break;
			case "1.2.840.113549.1.9.5":
				pkcs9AttributeObject = Upgrade<Pkcs9SigningTime>(pkcs9AttributeObject);
				break;
			case "1.2.840.113549.1.9.3":
				pkcs9AttributeObject = Upgrade<Pkcs9ContentType>(pkcs9AttributeObject);
				break;
			case "1.2.840.113549.1.9.4":
				pkcs9AttributeObject = Upgrade<Pkcs9MessageDigest>(pkcs9AttributeObject);
				break;
			}
			return pkcs9AttributeObject;
		}

		private static T Upgrade<T>(Pkcs9AttributeObject basicAttribute) where T : Pkcs9AttributeObject, new()
		{
			T val = new T();
			val.CopyFrom(basicAttribute);
			return val;
		}

		public static byte[] GetSubjectKeyIdentifier(this X509Certificate2 certificate)
		{
			X509Extension x509Extension = certificate.Extensions["2.5.29.14"];
			if (x509Extension != null)
			{
				if (new AsnReader(x509Extension.RawData, AsnEncodingRules.DER).TryGetPrimitiveOctetStringBytes(out var contents))
				{
					return contents.ToArray();
				}
				throw new CryptographicException("ASN1 corrupted data.");
			}
			using HashAlgorithm hashAlgorithm = SHA1.Create();
			return hashAlgorithm.ComputeHash(GetSubjectPublicKeyInfo(certificate).ToArray());
		}

		internal static byte[] OneShot(this ICryptoTransform transform, byte[] data)
		{
			return transform.OneShot(data, 0, data.Length);
		}

		internal static byte[] OneShot(this ICryptoTransform transform, byte[] data, int offset, int length)
		{
			if (transform.CanTransformMultipleBlocks)
			{
				return transform.TransformFinalBlock(data, offset, length);
			}
			using MemoryStream memoryStream = new MemoryStream();
			using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
			{
				cryptoStream.Write(data, offset, length);
			}
			return memoryStream.ToArray();
		}

		private static ReadOnlyMemory<byte> GetSubjectPublicKeyInfo(X509Certificate2 certificate)
		{
			return AsnSerializer.Deserialize<Certificate>(certificate.RawData, AsnEncodingRules.DER).TbsCertificate.SubjectPublicKeyInfo;
		}
	}
	internal abstract class KeyAgreeRecipientInfoPal : RecipientInfoPal
	{
		public abstract DateTime Date { get; }

		public abstract SubjectIdentifierOrKey OriginatorIdentifierOrKey { get; }

		public abstract CryptographicAttributeObject OtherKeyAttribute { get; }

		internal KeyAgreeRecipientInfoPal()
		{
		}
	}
	internal static class KeyLengths
	{
		public const int Rc2_40Bit = 40;

		public const int Rc2_56Bit = 56;

		public const int Rc2_64Bit = 64;

		public const int Rc2_128Bit = 128;

		public const int Rc4Max_128Bit = 128;

		public const int Des_64Bit = 64;

		public const int TripleDes_192Bit = 192;

		public const int DefaultKeyLengthForRc2AndRc4 = 128;
	}
	internal abstract class KeyTransRecipientInfoPal : RecipientInfoPal
	{
		internal KeyTransRecipientInfoPal()
		{
		}
	}
	internal static class Oids
	{
		public const string Rc2Cbc = "1.2.840.113549.3.2";

		public const string Rc4 = "1.2.840.113549.3.4";

		public const string TripleDesCbc = "1.2.840.113549.3.7";

		public const string DesCbc = "1.3.14.3.2.7";

		public const string Aes128Cbc = "2.16.840.1.101.3.4.1.2";

		public const string Aes192Cbc = "2.16.840.1.101.3.4.1.22";

		public const string Aes256Cbc = "2.16.840.1.101.3.4.1.42";

		public const string Rsa = "1.2.840.113549.1.1.1";

		public const string RsaOaep = "1.2.840.113549.1.1.7";

		public const string RsaPss = "1.2.840.113549.1.1.10";

		public const string RsaPkcs1Sha1 = "1.2.840.113549.1.1.5";

		public const string RsaPkcs1Sha256 = "1.2.840.113549.1.1.11";

		public const string RsaPkcs1Sha384 = "1.2.840.113549.1.1.12";

		public const string RsaPkcs1Sha512 = "1.2.840.113549.1.1.13";

		public const string Esdh = "1.2.840.113549.1.9.16.3.5";

		public const string SigningTime = "1.2.840.113549.1.9.5";

		public const string ContentType = "1.2.840.113549.1.9.3";

		public const string DocumentDescription = "1.3.6.1.4.1.311.88.2.2";

		public const string MessageDigest = "1.2.840.113549.1.9.4";

		public const string CounterSigner = "1.2.840.113549.1.9.6";

		public const string SigningCertificate = "1.2.840.113549.1.9.16.2.12";

		public const string SigningCertificateV2 = "1.2.840.113549.1.9.16.2.47";

		public const string DocumentName = "1.3.6.1.4.1.311.88.2.1";

		public const string CmsRc2Wrap = "1.2.840.113549.1.9.16.3.7";

		public const string Cms3DesWrap = "1.2.840.113549.1.9.16.3.6";

		public const string Pkcs7Data = "1.2.840.113549.1.7.1";

		public const string Pkcs7Signed = "1.2.840.113549.1.7.2";

		public const string Pkcs7Enveloped = "1.2.840.113549.1.7.3";

		public const string Pkcs7SignedEnveloped = "1.2.840.113549.1.7.4";

		public const string Pkcs7Hashed = "1.2.840.113549.1.7.5";

		public const string Pkcs7Encrypted = "1.2.840.113549.1.7.6";

		public const string Md5 = "1.2.840.113549.2.5";

		public const string Sha1 = "1.3.14.3.2.26";

		public const string Sha256 = "2.16.840.1.101.3.4.2.1";

		public const string Sha384 = "2.16.840.1.101.3.4.2.2";

		public const string Sha512 = "2.16.840.1.101.3.4.2.3";

		public const string DsaPublicKey = "1.2.840.10040.4.1";

		public const string DsaWithSha1 = "1.2.840.10040.4.3";

		public const string DsaWithSha256 = "2.16.840.1.101.3.4.3.2";

		public const string DsaWithSha384 = "2.16.840.1.101.3.4.3.3";

		public const string DsaWithSha512 = "2.16.840.1.101.3.4.3.4";

		public const string EcPublicKey = "1.2.840.10045.2.1";

		public const string ECDsaWithSha1 = "1.2.840.10045.4.1";

		public const string ECDsaWithSha256 = "1.2.840.10045.4.3.2";

		public const string ECDsaWithSha384 = "1.2.840.10045.4.3.3";

		public const string ECDsaWithSha512 = "1.2.840.10045.4.3.4";

		public const string Mgf1 = "1.2.840.113549.1.1.8";

		public const string SubjectKeyIdentifier = "2.5.29.14";

		public const string KeyUsage = "2.5.29.15";

		public const string TstInfo = "1.2.840.113549.1.9.16.1.4";

		public const string TimeStampingPurpose = "1.3.6.1.5.5.7.3.8";
	}
	internal abstract class PkcsPal
	{
		private static readonly PkcsPal s_instance = new ManagedPkcsPal();

		public static PkcsPal Instance => s_instance;

		public abstract byte[] Encrypt(CmsRecipientCollection recipients, ContentInfo contentInfo, AlgorithmIdentifier contentEncryptionAlgorithm, X509Certificate2Collection originatorCerts, CryptographicAttributeObjectCollection unprotectedAttributes);

		public abstract DecryptorPal Decode(byte[] encodedMessage, out int version, out ContentInfo contentInfo, out AlgorithmIdentifier contentEncryptionAlgorithm, out X509Certificate2Collection originatorCerts, out CryptographicAttributeObjectCollection unprotectedAttributes);

		public abstract byte[] EncodeOctetString(byte[] octets);

		public abstract byte[] DecodeOctetString(byte[] encodedOctets);

		public abstract byte[] EncodeUtcTime(DateTime utcTime);

		public abstract DateTime DecodeUtcTime(byte[] encodedUtcTime);

		public abstract string DecodeOid(byte[] encodedOid);

		public abstract Oid GetEncodedMessageType(byte[] encodedMessage);

		public abstract void AddCertsFromStoreForDecryption(X509Certificate2Collection certs);

		public abstract Exception CreateRecipientsNotFoundException();

		public abstract Exception CreateRecipientInfosAfterEncryptException();

		public abstract Exception CreateDecryptAfterEncryptException();

		public abstract Exception CreateDecryptTwiceException();

		public abstract byte[] GetSubjectKeyIdentifier(X509Certificate2 certificate);

		public abstract T GetPrivateKeyForSigning<T>(X509Certificate2 certificate, bool silent) where T : AsymmetricAlgorithm;

		public abstract T GetPrivateKeyForDecryption<T>(X509Certificate2 certificate, bool silent) where T : AsymmetricAlgorithm;
	}
	internal abstract class RecipientInfoPal
	{
		public abstract byte[] EncryptedKey { get; }

		public abstract AlgorithmIdentifier KeyEncryptionAlgorithm { get; }

		public abstract SubjectIdentifier RecipientIdentifier { get; }

		public abstract int Version { get; }

		internal RecipientInfoPal()
		{
		}
	}
}
namespace Internal.Cryptography.Pal.AnyOS
{
	internal static class AsnHelpers
	{
		internal static SubjectIdentifierOrKey ToSubjectIdentifierOrKey(this OriginatorIdentifierOrKeyAsn originator)
		{
			if (originator.IssuerAndSerialNumber.HasValue)
			{
				X500DistinguishedName x500DistinguishedName = new X500DistinguishedName(originator.IssuerAndSerialNumber.Value.Issuer.ToArray());
				return new SubjectIdentifierOrKey(SubjectIdentifierOrKeyType.IssuerAndSerialNumber, new X509IssuerSerial(x500DistinguishedName.Name, originator.IssuerAndSerialNumber.Value.SerialNumber.Span.ToBigEndianHex()));
			}
			if (originator.SubjectKeyIdentifier.HasValue)
			{
				return new SubjectIdentifierOrKey(SubjectIdentifierOrKeyType.SubjectKeyIdentifier, originator.SubjectKeyIdentifier.Value.Span.ToBigEndianHex());
			}
			if (originator.OriginatorKey != null)
			{
				OriginatorPublicKeyAsn originatorKey = originator.OriginatorKey;
				return new SubjectIdentifierOrKey(SubjectIdentifierOrKeyType.PublicKeyInfo, new PublicKeyInfo(originatorKey.Algorithm.ToPresentationObject(), originatorKey.PublicKey.ToArray()));
			}
			return new SubjectIdentifierOrKey(SubjectIdentifierOrKeyType.Unknown, string.Empty);
		}

		internal static AlgorithmIdentifier ToPresentationObject(this AlgorithmIdentifierAsn asn)
		{
			int keyLength;
			switch (asn.Algorithm.Value)
			{
			case "1.2.840.113549.3.2":
			{
				if (!asn.Parameters.HasValue)
				{
					keyLength = 0;
					break;
				}
				int effectiveKeyBits = AsnSerializer.Deserialize<Rc2CbcParameters>(asn.Parameters.Value, AsnEncodingRules.BER).GetEffectiveKeyBits();
				switch (effectiveKeyBits)
				{
				case 40:
				case 56:
				case 64:
				case 128:
					keyLength = effectiveKeyBits;
					break;
				default:
					keyLength = 0;
					break;
				}
				break;
			}
			case "1.2.840.113549.3.4":
			{
				if (!asn.Parameters.HasValue)
				{
					keyLength = 0;
					break;
				}
				int bytesWritten = 0;
				AsnReader asnReader = new AsnReader(asn.Parameters.Value, AsnEncodingRules.BER);
				if (asnReader.PeekTag() != Asn1Tag.Null)
				{
					if (asnReader.TryGetPrimitiveOctetStringBytes(out var contents))
					{
						bytesWritten = contents.Length;
					}
					else
					{
						Span<byte> destination = stackalloc byte[16];
						if (!asnReader.TryCopyOctetStringBytes(destination, out bytesWritten))
						{
							throw new CryptographicException();
						}
					}
				}
				keyLength = 128 - 8 * bytesWritten;
				break;
			}
			case "1.3.14.3.2.7":
				keyLength = 64;
				break;
			case "1.2.840.113549.3.7":
				keyLength = 192;
				break;
			default:
				keyLength = 0;
				break;
			}
			return new AlgorithmIdentifier(new Oid(asn.Algorithm), keyLength);
		}
	}
	internal sealed class ManagedPkcsPal : PkcsPal
	{
		private sealed class ManagedDecryptorPal : DecryptorPal
		{
			private byte[] _dataCopy;

			private EnvelopedDataAsn _envelopedData;

			public ManagedDecryptorPal(byte[] dataCopy, EnvelopedDataAsn envelopedDataAsn, RecipientInfoCollection recipientInfos)
				: base(recipientInfos)
			{
				_dataCopy = dataCopy;
				_envelopedData = envelopedDataAsn;
			}

			public unsafe override ContentInfo TryDecrypt(RecipientInfo recipientInfo, X509Certificate2 cert, X509Certificate2Collection originatorCerts, X509Certificate2Collection extraStore, out Exception exception)
			{
				if (recipientInfo.Pal is ManagedKeyTransPal managedKeyTransPal)
				{
					byte[] array = managedKeyTransPal.DecryptCek(cert, out exception);
					byte[] array2;
					fixed (byte* ptr = array)
					{
						try
						{
							if (exception != null)
							{
								return null;
							}
							ReadOnlyMemory<byte>? encryptedContent = _envelopedData.EncryptedContentInfo.EncryptedContent;
							if (!encryptedContent.HasValue)
							{
								exception = null;
								return new ContentInfo(new Oid(_envelopedData.EncryptedContentInfo.ContentType), Array.Empty<byte>());
							}
							array2 = DecryptContent(encryptedContent.Value, array, out exception);
						}
						finally
						{
							if (array != null)
							{
								Array.Clear(array, 0, array.Length);
							}
						}
					}
					if (exception != null)
					{
						return null;
					}
					if (_envelopedData.EncryptedContentInfo.ContentType == "1.2.840.113549.1.7.1")
					{
						byte[] array3 = null;
						try
						{
							AsnReader asnReader = new AsnReader(array2, AsnEncodingRules.BER);
							if (asnReader.TryGetPrimitiveOctetStringBytes(out var contents))
							{
								array2 = contents.ToArray();
							}
							else
							{
								array3 = ArrayPool<byte>.Shared.Rent(array2.Length);
								if (asnReader.TryCopyOctetStringBytes(array3, out var bytesWritten))
								{
									Span<byte> span = new Span<byte>(array3, 0, bytesWritten);
									array2 = span.ToArray();
									span.Clear();
								}
							}
						}
						catch (CryptographicException)
						{
						}
						finally
						{
							if (array3 != null)
							{
								ArrayPool<byte>.Shared.Return(array3);
							}
						}
					}
					else
					{
						array2 = GetAsnSequenceWithContentNoValidation(array2);
					}
					exception = null;
					return new ContentInfo(new Oid(_envelopedData.EncryptedContentInfo.ContentType), array2);
				}
				exception = new CryptographicException("The recipient type '{0}' is not supported for encryption or decryption on this platform.", recipientInfo.Type.ToString());
				return null;
			}

			private static byte[] GetAsnSequenceWithContentNoValidation(ReadOnlySpan<byte> content)
			{
				using AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.BER);
				asnWriter.WriteOctetString(content);
				byte[] array = asnWriter.Encode();
				array[0] = 48;
				return array;
			}

			private byte[] DecryptContent(ReadOnlyMemory<byte> encryptedContent, byte[] cek, out Exception exception)
			{
				exception = null;
				int length = encryptedContent.Length;
				byte[] array = ArrayPool<byte>.Shared.Rent(length);
				try
				{
					encryptedContent.CopyTo(array);
					using SymmetricAlgorithm symmetricAlgorithm = OpenAlgorithm(_envelopedData.EncryptedContentInfo.ContentEncryptionAlgorithm);
					using ICryptoTransform transform = symmetricAlgorithm.CreateDecryptor(cek, symmetricAlgorithm.IV);
					return transform.OneShot(array, 0, length);
				}
				catch (CryptographicException ex)
				{
					exception = ex;
					return null;
				}
				finally
				{
					Array.Clear(array, 0, length);
					ArrayPool<byte>.Shared.Return(array);
					array = null;
				}
			}

			public override void Dispose()
			{
			}
		}

		private sealed class ManagedKeyAgreePal : KeyAgreeRecipientInfoPal
		{
			private readonly KeyAgreeRecipientInfoAsn _asn;

			private readonly int _index;

			public override byte[] EncryptedKey => _asn.RecipientEncryptedKeys[_index].EncryptedKey.ToArray();

			public override AlgorithmIdentifier KeyEncryptionAlgorithm => _asn.KeyEncryptionAlgorithm.ToPresentationObject();

			public override SubjectIdentifier RecipientIdentifier => new SubjectIdentifier(_asn.RecipientEncryptedKeys[_index].Rid.IssuerAndSerialNumber, _asn.RecipientEncryptedKeys[_index].Rid.RKeyId?.SubjectKeyIdentifier);

			public override int Version => _asn.Version;

			public override DateTime Date
			{
				get
				{
					KeyAgreeRecipientIdentifierAsn rid = _asn.RecipientEncryptedKeys[_index].Rid;
					if (rid.RKeyId == null)
					{
						throw new InvalidOperationException("The Date property is not available for none KID key agree recipient.");
					}
					if (!rid.RKeyId.Date.HasValue)
					{
						return DateTime.FromFileTimeUtc(0L);
					}
					return rid.RKeyId.Date.Value.LocalDateTime;
				}
			}

			public override SubjectIdentifierOrKey OriginatorIdentifierOrKey => _asn.Originator.ToSubjectIdentifierOrKey();

			public override CryptographicAttributeObject OtherKeyAttribute
			{
				get
				{
					KeyAgreeRecipientIdentifierAsn rid = _asn.RecipientEncryptedKeys[_index].Rid;
					if (rid.RKeyId == null)
					{
						throw new InvalidOperationException("The Date property is not available for none KID key agree recipient.");
					}
					if (!rid.RKeyId.Other.HasValue)
					{
						return null;
					}
					Oid oid = new Oid(rid.RKeyId.Other.Value.KeyAttrId);
					byte[] encodedData = Array.Empty<byte>();
					if (rid.RKeyId.Other.Value.KeyAttr.HasValue)
					{
						encodedData = rid.RKeyId.Other.Value.KeyAttr.Value.ToArray();
					}
					AsnEncodedDataCollection values = new AsnEncodedDataCollection(new Pkcs9AttributeObject(oid, encodedData));
					return new CryptographicAttributeObject(oid, values);
				}
			}

			internal ManagedKeyAgreePal(KeyAgreeRecipientInfoAsn asn, int index)
			{
				_asn = asn;
				_index = index;
			}
		}

		private sealed class ManagedKeyTransPal : KeyTransRecipientInfoPal
		{
			private readonly KeyTransRecipientInfoAsn _asn;

			public override byte[] EncryptedKey => _asn.EncryptedKey.ToArray();

			public override AlgorithmIdentifier KeyEncryptionAlgorithm => _asn.KeyEncryptionAlgorithm.ToPresentationObject();

			public override SubjectIdentifier RecipientIdentifier => new SubjectIdentifier(_asn.Rid.IssuerAndSerialNumber, _asn.Rid.SubjectKeyIdentifier);

			public override int Version => _asn.Version;

			internal ManagedKeyTransPal(KeyTransRecipientInfoAsn asn)
			{
				_asn = asn;
			}

			internal byte[] DecryptCek(X509Certificate2 cert, out Exception exception)
			{
				ReadOnlyMemory<byte>? parameters = _asn.KeyEncryptionAlgorithm.Parameters;
				string value = _asn.KeyEncryptionAlgorithm.Algorithm.Value;
				RSAEncryptionPadding padding;
				if (!(value == "1.2.840.113549.1.1.1"))
				{
					if (!(value == "1.2.840.113549.1.1.7"))
					{
						exception = new CryptographicException("Unknown algorithm '{0}'.", _asn.KeyEncryptionAlgorithm.Algorithm.Value);
						return null;
					}
					if (parameters.HasValue && !parameters.Value.Span.SequenceEqual(s_rsaOaepSha1Parameters))
					{
						exception = new CryptographicException("ASN1 corrupted data.");
						return null;
					}
					padding = RSAEncryptionPadding.OaepSHA1;
				}
				else
				{
					if (parameters.HasValue && !parameters.Value.Span.SequenceEqual(s_rsaPkcsParameters))
					{
						exception = new CryptographicException("ASN1 corrupted data.");
						return null;
					}
					padding = RSAEncryptionPadding.Pkcs1;
				}
				byte[] array = null;
				int length = 0;
				try
				{
					using RSA rSA = cert.GetRSAPrivateKey();
					if (rSA == null)
					{
						exception = new CryptographicException("A certificate with a private key is required.");
						return null;
					}
					exception = null;
					return rSA.Decrypt(_asn.EncryptedKey.Span.ToArray(), padding);
				}
				catch (CryptographicException ex)
				{
					exception = ex;
					return null;
				}
				finally
				{
					if (array != null)
					{
						Array.Clear(array, 0, length);
						ArrayPool<byte>.Shared.Return(array);
					}
				}
			}
		}

		private static readonly byte[] s_invalidEmptyOid = new byte[2] { 6, 0 };

		private static readonly byte[] s_rsaPkcsParameters = new byte[2] { 5, 0 };

		private static readonly byte[] s_rsaOaepSha1Parameters = new byte[2] { 48, 0 };

		public override byte[] EncodeOctetString(byte[] octets)
		{
			using AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER);
			asnWriter.WriteOctetString(octets);
			return asnWriter.Encode();
		}

		public override byte[] DecodeOctetString(byte[] encodedOctets)
		{
			AsnReader asnReader = new AsnReader(encodedOctets, AsnEncodingRules.BER);
			Span<byte> destination = stackalloc byte[256];
			ReadOnlySpan<byte> readOnlySpan = default(Span<byte>);
			byte[] array = null;
			try
			{
				if (!asnReader.TryGetPrimitiveOctetStringBytes(out var contents))
				{
					if (asnReader.TryCopyOctetStringBytes(destination, out var bytesWritten))
					{
						readOnlySpan = destination.Slice(0, bytesWritten);
					}
					else
					{
						array = ArrayPool<byte>.Shared.Rent(asnReader.PeekContentBytes().Length);
						if (!asnReader.TryCopyOctetStringBytes(array, out bytesWritten))
						{
							throw new CryptographicException();
						}
						readOnlySpan = new ReadOnlySpan<byte>(array, 0, bytesWritten);
					}
				}
				else
				{
					readOnlySpan = contents.Span;
				}
				if (asnReader.HasData)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return readOnlySpan.ToArray();
			}
			finally
			{
				if (array != null)
				{
					Array.Clear(array, 0, readOnlySpan.Length);
					ArrayPool<byte>.Shared.Return(array);
				}
			}
		}

		public override byte[] EncodeUtcTime(DateTime utcTime)
		{
			using AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER);
			try
			{
				if (utcTime.Kind == DateTimeKind.Unspecified)
				{
					asnWriter.WriteUtcTime(utcTime.ToLocalTime(), 1950);
				}
				else
				{
					asnWriter.WriteUtcTime(utcTime, 1950);
				}
				return asnWriter.Encode();
			}
			catch (ArgumentException ex)
			{
				throw new CryptographicException(ex.Message, ex);
			}
		}

		public override DateTime DecodeUtcTime(byte[] encodedUtcTime)
		{
			AsnReader asnReader = new AsnReader(encodedUtcTime, AsnEncodingRules.BER);
			DateTimeOffset utcTime = asnReader.GetUtcTime();
			if (asnReader.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return utcTime.UtcDateTime;
		}

		public override string DecodeOid(byte[] encodedOid)
		{
			if (s_invalidEmptyOid.AsSpan().SequenceEqual(encodedOid))
			{
				return string.Empty;
			}
			AsnReader asnReader = new AsnReader(encodedOid, AsnEncodingRules.BER);
			string result = asnReader.ReadObjectIdentifierAsString();
			if (asnReader.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return result;
		}

		public override Oid GetEncodedMessageType(byte[] encodedMessage)
		{
			ContentInfoAsn contentInfoAsn = AsnSerializer.Deserialize<ContentInfoAsn>(new AsnReader(encodedMessage, AsnEncodingRules.BER).GetEncodedValue(), AsnEncodingRules.BER);
			switch (contentInfoAsn.ContentType)
			{
			case "1.2.840.113549.1.7.1":
			case "1.2.840.113549.1.7.2":
			case "1.2.840.113549.1.7.3":
			case "1.2.840.113549.1.7.4":
			case "1.2.840.113549.1.7.5":
			case "1.2.840.113549.1.7.6":
				return new Oid(contentInfoAsn.ContentType);
			default:
				throw new CryptographicException("Invalid cryptographic message type.");
			}
		}

		public override DecryptorPal Decode(byte[] encodedMessage, out int version, out ContentInfo contentInfo, out AlgorithmIdentifier contentEncryptionAlgorithm, out X509Certificate2Collection originatorCerts, out CryptographicAttributeObjectCollection unprotectedAttributes)
		{
			ContentInfoAsn contentInfoAsn = AsnSerializer.Deserialize<ContentInfoAsn>(new AsnReader(encodedMessage, AsnEncodingRules.BER).GetEncodedValue(), AsnEncodingRules.BER);
			if (contentInfoAsn.ContentType != "1.2.840.113549.1.7.3")
			{
				throw new CryptographicException("Invalid cryptographic message type.");
			}
			byte[] array = contentInfoAsn.Content.ToArray();
			EnvelopedDataAsn envelopedDataAsn = AsnSerializer.Deserialize<EnvelopedDataAsn>(array, AsnEncodingRules.BER);
			version = envelopedDataAsn.Version;
			contentInfo = new ContentInfo(new Oid(envelopedDataAsn.EncryptedContentInfo.ContentType), envelopedDataAsn.EncryptedContentInfo.EncryptedContent?.ToArray() ?? Array.Empty<byte>());
			contentEncryptionAlgorithm = envelopedDataAsn.EncryptedContentInfo.ContentEncryptionAlgorithm.ToPresentationObject();
			originatorCerts = new X509Certificate2Collection();
			if (envelopedDataAsn.OriginatorInfo != null && envelopedDataAsn.OriginatorInfo.CertificateSet != null)
			{
				CertificateChoiceAsn[] certificateSet = envelopedDataAsn.OriginatorInfo.CertificateSet;
				for (int i = 0; i < certificateSet.Length; i++)
				{
					CertificateChoiceAsn certificateChoiceAsn = certificateSet[i];
					if (certificateChoiceAsn.Certificate.HasValue)
					{
						originatorCerts.Add(new X509Certificate2(certificateChoiceAsn.Certificate.Value.ToArray()));
					}
				}
			}
			unprotectedAttributes = SignerInfo.MakeAttributeCollection(envelopedDataAsn.UnprotectedAttributes);
			List<RecipientInfo> list = new List<RecipientInfo>();
			RecipientInfoAsn[] recipientInfos = envelopedDataAsn.RecipientInfos;
			for (int i = 0; i < recipientInfos.Length; i++)
			{
				RecipientInfoAsn recipientInfoAsn = recipientInfos[i];
				if (recipientInfoAsn.Ktri != null)
				{
					list.Add(new KeyTransRecipientInfo(new ManagedKeyTransPal(recipientInfoAsn.Ktri)));
					continue;
				}
				if (recipientInfoAsn.Kari != null)
				{
					for (int j = 0; j < recipientInfoAsn.Kari.RecipientEncryptedKeys.Length; j++)
					{
						list.Add(new KeyAgreeRecipientInfo(new ManagedKeyAgreePal(recipientInfoAsn.Kari, j)));
					}
					continue;
				}
				throw new CryptographicException();
			}
			return new ManagedDecryptorPal(array, envelopedDataAsn, new RecipientInfoCollection(list));
		}

		public unsafe override byte[] Encrypt(CmsRecipientCollection recipients, ContentInfo contentInfo, AlgorithmIdentifier contentEncryptionAlgorithm, X509Certificate2Collection originatorCerts, CryptographicAttributeObjectCollection unprotectedAttributes)
		{
			byte[] cek;
			byte[] parameterBytes;
			byte[] encryptedContent = EncryptContent(contentInfo, contentEncryptionAlgorithm, out cek, out parameterBytes);
			fixed (byte* ptr = cek)
			{
				try
				{
					return Encrypt(recipients, contentInfo, contentEncryptionAlgorithm, originatorCerts, unprotectedAttributes, encryptedContent, cek, parameterBytes);
				}
				finally
				{
					Array.Clear(cek, 0, cek.Length);
				}
			}
		}

		private static byte[] Encrypt(CmsRecipientCollection recipients, ContentInfo contentInfo, AlgorithmIdentifier contentEncryptionAlgorithm, X509Certificate2Collection originatorCerts, CryptographicAttributeObjectCollection unprotectedAttributes, byte[] encryptedContent, byte[] cek, byte[] parameterBytes)
		{
			EnvelopedDataAsn value = new EnvelopedDataAsn
			{
				EncryptedContentInfo = 
				{
					ContentType = contentInfo.ContentType.Value,
					ContentEncryptionAlgorithm = 
					{
						Algorithm = contentEncryptionAlgorithm.Oid,
						Parameters = parameterBytes
					},
					EncryptedContent = encryptedContent
				}
			};
			if (unprotectedAttributes != null && unprotectedAttributes.Count > 0)
			{
				List<AttributeAsn> list = CmsSigner.BuildAttributes(unprotectedAttributes);
				value.UnprotectedAttributes = Helpers.NormalizeSet(list.ToArray());
			}
			if (originatorCerts != null && originatorCerts.Count > 0)
			{
				CertificateChoiceAsn[] array = new CertificateChoiceAsn[originatorCerts.Count];
				for (int i = 0; i < originatorCerts.Count; i++)
				{
					array[i].Certificate = originatorCerts[i].RawData;
				}
				value.OriginatorInfo = new OriginatorInfoAsn
				{
					CertificateSet = array
				};
			}
			value.RecipientInfos = new RecipientInfoAsn[recipients.Count];
			bool flag = true;
			for (int j = 0; j < recipients.Count; j++)
			{
				CmsRecipient cmsRecipient = recipients[j];
				if (cmsRecipient.Certificate.GetKeyAlgorithm() == "1.2.840.113549.1.1.1")
				{
					value.RecipientInfos[j].Ktri = MakeKtri(cek, cmsRecipient, out var v0Recipient);
					flag = flag && v0Recipient;
					continue;
				}
				throw new CryptographicException("Unknown algorithm '{0}'.", cmsRecipient.Certificate.GetKeyAlgorithm());
			}
			if (value.OriginatorInfo != null || !flag || value.UnprotectedAttributes != null)
			{
				value.Version = 2;
			}
			return Helpers.EncodeContentInfo(value, "1.2.840.113549.1.7.3");
		}

		private byte[] EncryptContent(ContentInfo contentInfo, AlgorithmIdentifier contentEncryptionAlgorithm, out byte[] cek, out byte[] parameterBytes)
		{
			using SymmetricAlgorithm symmetricAlgorithm = OpenAlgorithm(contentEncryptionAlgorithm);
			using ICryptoTransform transform = symmetricAlgorithm.CreateEncryptor();
			cek = symmetricAlgorithm.Key;
			if (symmetricAlgorithm is RC2)
			{
				using AsnWriter asnWriter = AsnSerializer.Serialize(new Rc2CbcParameters(symmetricAlgorithm.IV, symmetricAlgorithm.KeySize), AsnEncodingRules.DER);
				parameterBytes = asnWriter.Encode();
			}
			else
			{
				parameterBytes = EncodeOctetString(symmetricAlgorithm.IV);
			}
			byte[] content = contentInfo.Content;
			if (contentInfo.ContentType.Value == "1.2.840.113549.1.7.1")
			{
				content = EncodeOctetString(content);
				return transform.OneShot(content);
			}
			if (contentInfo.Content.Length == 0)
			{
				return transform.OneShot(contentInfo.Content);
			}
			AsnReader asnReader = new AsnReader(contentInfo.Content, AsnEncodingRules.BER);
			return transform.OneShot(asnReader.PeekContentBytes().ToArray());
		}

		public override Exception CreateRecipientsNotFoundException()
		{
			return new CryptographicException("The enveloped-data message does not contain the specified recipient.");
		}

		public override Exception CreateRecipientInfosAfterEncryptException()
		{
			return CreateInvalidMessageTypeException();
		}

		public override Exception CreateDecryptAfterEncryptException()
		{
			return CreateInvalidMessageTypeException();
		}

		public override Exception CreateDecryptTwiceException()
		{
			return CreateInvalidMessageTypeException();
		}

		private static Exception CreateInvalidMessageTypeException()
		{
			return new CryptographicException("Invalid cryptographic message type.");
		}

		private static KeyTransRecipientInfoAsn MakeKtri(byte[] cek, CmsRecipient recipient, out bool v0Recipient)
		{
			KeyTransRecipientInfoAsn keyTransRecipientInfoAsn = new KeyTransRecipientInfoAsn();
			if (recipient.RecipientIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier)
			{
				keyTransRecipientInfoAsn.Version = 2;
				keyTransRecipientInfoAsn.Rid.SubjectKeyIdentifier = recipient.Certificate.GetSubjectKeyIdentifier();
			}
			else
			{
				if (recipient.RecipientIdentifierType != SubjectIdentifierType.IssuerAndSerialNumber)
				{
					throw new CryptographicException("The subject identifier type {0} is not valid.", recipient.RecipientIdentifierType.ToString());
				}
				byte[] serialNumber = recipient.Certificate.GetSerialNumber();
				Array.Reverse(serialNumber);
				IssuerAndSerialNumberAsn value = new IssuerAndSerialNumberAsn
				{
					Issuer = recipient.Certificate.IssuerName.RawData,
					SerialNumber = serialNumber
				};
				keyTransRecipientInfoAsn.Rid.IssuerAndSerialNumber = value;
			}
			RSAEncryptionPadding padding;
			if (recipient.Certificate.GetKeyAlgorithm() == "1.2.840.113549.1.1.7")
			{
				padding = RSAEncryptionPadding.OaepSHA1;
				keyTransRecipientInfoAsn.KeyEncryptionAlgorithm.Algorithm = new Oid("1.2.840.113549.1.1.7", "1.2.840.113549.1.1.7");
				keyTransRecipientInfoAsn.KeyEncryptionAlgorithm.Parameters = s_rsaOaepSha1Parameters;
			}
			else
			{
				padding = RSAEncryptionPadding.Pkcs1;
				keyTransRecipientInfoAsn.KeyEncryptionAlgorithm.Algorithm = new Oid("1.2.840.113549.1.1.1", "1.2.840.113549.1.1.1");
				keyTransRecipientInfoAsn.KeyEncryptionAlgorithm.Parameters = s_rsaPkcsParameters;
			}
			using (RSA rSA = recipient.Certificate.GetRSAPublicKey())
			{
				keyTransRecipientInfoAsn.EncryptedKey = rSA.Encrypt(cek, padding);
			}
			v0Recipient = keyTransRecipientInfoAsn.Version == 0;
			return keyTransRecipientInfoAsn;
		}

		public override void AddCertsFromStoreForDecryption(X509Certificate2Collection certs)
		{
			certs.AddRange(Helpers.GetStoreCertificates(StoreName.My, StoreLocation.CurrentUser, openExistingOnly: false));
			try
			{
				certs.AddRange(Helpers.GetStoreCertificates(StoreName.My, StoreLocation.LocalMachine, openExistingOnly: false));
			}
			catch (CryptographicException)
			{
			}
		}

		public override byte[] GetSubjectKeyIdentifier(X509Certificate2 certificate)
		{
			return certificate.GetSubjectKeyIdentifier();
		}

		public override T GetPrivateKeyForSigning<T>(X509Certificate2 certificate, bool silent)
		{
			return GetPrivateKey<T>(certificate);
		}

		public override T GetPrivateKeyForDecryption<T>(X509Certificate2 certificate, bool silent)
		{
			return GetPrivateKey<T>(certificate);
		}

		private T GetPrivateKey<T>(X509Certificate2 certificate) where T : AsymmetricAlgorithm
		{
			if (typeof(T) == typeof(RSA))
			{
				return (T)(AsymmetricAlgorithm)certificate.GetRSAPrivateKey();
			}
			if (typeof(T) == typeof(ECDsa))
			{
				return (T)(AsymmetricAlgorithm)certificate.GetECDsaPrivateKey();
			}
			return null;
		}

		private static SymmetricAlgorithm OpenAlgorithm(AlgorithmIdentifierAsn contentEncryptionAlgorithm)
		{
			SymmetricAlgorithm symmetricAlgorithm = OpenAlgorithm(contentEncryptionAlgorithm.Algorithm);
			if (symmetricAlgorithm is RC2)
			{
				if (!contentEncryptionAlgorithm.Parameters.HasValue)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				Rc2CbcParameters rc2CbcParameters = AsnSerializer.Deserialize<Rc2CbcParameters>(contentEncryptionAlgorithm.Parameters.Value, AsnEncodingRules.BER);
				symmetricAlgorithm.KeySize = rc2CbcParameters.GetEffectiveKeyBits();
				symmetricAlgorithm.IV = rc2CbcParameters.Iv.ToArray();
			}
			else
			{
				if (!contentEncryptionAlgorithm.Parameters.HasValue)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				AsnReader asnReader = new AsnReader(contentEncryptionAlgorithm.Parameters.Value, AsnEncodingRules.BER);
				if (asnReader.TryGetPrimitiveOctetStringBytes(out var contents))
				{
					symmetricAlgorithm.IV = contents.ToArray();
				}
				else
				{
					byte[] array = new byte[symmetricAlgorithm.BlockSize / 8];
					if (!asnReader.TryCopyOctetStringBytes(array, out var bytesWritten) || bytesWritten != array.Length)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					symmetricAlgorithm.IV = array;
				}
			}
			return symmetricAlgorithm;
		}

		private static SymmetricAlgorithm OpenAlgorithm(AlgorithmIdentifier algorithmIdentifier)
		{
			SymmetricAlgorithm symmetricAlgorithm = OpenAlgorithm(algorithmIdentifier.Oid);
			if (symmetricAlgorithm is RC2)
			{
				if (algorithmIdentifier.KeyLength != 0)
				{
					symmetricAlgorithm.KeySize = algorithmIdentifier.KeyLength;
				}
				else
				{
					symmetricAlgorithm.KeySize = 128;
				}
			}
			return symmetricAlgorithm;
		}

		private static SymmetricAlgorithm OpenAlgorithm(Oid algorithmIdentifier)
		{
			SymmetricAlgorithm symmetricAlgorithm;
			switch (algorithmIdentifier.Value)
			{
			case "1.2.840.113549.3.2":
				symmetricAlgorithm = RC2.Create();
				break;
			case "1.3.14.3.2.7":
				symmetricAlgorithm = DES.Create();
				break;
			case "1.2.840.113549.3.7":
				symmetricAlgorithm = TripleDES.Create();
				break;
			case "2.16.840.1.101.3.4.1.2":
				symmetricAlgorithm = Aes.Create();
				symmetricAlgorithm.KeySize = 128;
				break;
			case "2.16.840.1.101.3.4.1.22":
				symmetricAlgorithm = Aes.Create();
				symmetricAlgorithm.KeySize = 192;
				break;
			case "2.16.840.1.101.3.4.1.42":
				symmetricAlgorithm = Aes.Create();
				symmetricAlgorithm.KeySize = 256;
				break;
			default:
				throw new CryptographicException("Unknown algorithm '{0}'.", algorithmIdentifier.Value);
			}
			symmetricAlgorithm.Padding = PaddingMode.PKCS7;
			symmetricAlgorithm.Mode = CipherMode.CBC;
			return symmetricAlgorithm;
		}
	}
}
namespace System.Security.Cryptography
{
	/// <summary>Provides simple data protection methods.</summary>
	public sealed class DpapiDataProtector : DataProtector
	{
		/// <summary>Gets or sets the scope of the data protection.</summary>
		/// <returns>One of the enumeration values that specifies the scope of the data protection (either the current user or the local machine). The default is <see cref="F:System.Security.Cryptography.DataProtectionScope.CurrentUser" />.</returns>
		public DataProtectionScope Scope
		{
			[CompilerGenerated]
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return default(DataProtectionScope);
			}
			[CompilerGenerated]
			set
			{
				Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Security.Cryptography.DpapiDataProtector" /> class by using the specified application name, primary purpose, and specific purposes.</summary>
		/// <param name="appName">The name of the application.</param>
		/// <param name="primaryPurpose">The primary purpose for the data protector.</param>
		/// <param name="specificPurpose">The specific purpose(s) for the data protector.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="appName" /> is an empty string or <see langword="null" />.  
		/// -or-  
		/// <paramref name="primaryPurpose" /> is an empty string or <see langword="null" />.  
		/// -or-  
		/// <paramref name="specificPurposes" /> contains an empty string or <see langword="null" />.</exception>
		[SecuritySafeCritical]
		[DataProtectionPermission(SecurityAction.Demand, Unrestricted = true)]
		public DpapiDataProtector(string appName, string primaryPurpose, string[] specificPurpose)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Determines if the data must be re-encrypted.</summary>
		/// <param name="encryptedData">The encrypted data to be checked.</param>
		/// <returns>
		///   <see langword="true" /> if the data must be re-encrypted; otherwise, <see langword="false" />.</returns>
		public override bool IsReprotectRequired(byte[] encryptedData)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		[SecuritySafeCritical]
		[DataProtectionPermission(SecurityAction.Assert, ProtectData = true)]
		protected override byte[] ProviderProtect(byte[] userData)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		[SecuritySafeCritical]
		[DataProtectionPermission(SecurityAction.Assert, UnprotectData = true)]
		protected override byte[] ProviderUnprotect(byte[] encryptedData)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return null;
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
