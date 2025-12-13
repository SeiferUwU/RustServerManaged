#define TRACE
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Configuration;
using System.Configuration.Install;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Transactions;
using System.Xml;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using MySql.Data.Common;
using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.Authentication;
using MySql.Data.MySqlClient.Properties;
using MySql.Data.MySqlClient.Replication;
using MySql.Data.Types;
using zlib;

[assembly: InternalsVisibleTo("MySql.Data.Entity.EF5, PublicKey = 0024000004800000940000000602000000240000525341310004000001000100d973bda91f71752c78294126974a41a08643168271f65fc0fb3cd45f658da01fbca75ac74067d18e7afbf1467d7a519ce0248b13719717281bb4ddd4ecd71a580dfe0912dfc3690b1d24c7e1975bf7eed90e4ab14e10501eedf763bff8ac204f955c9c15c2cf4ebf6563d8320b6ea8d1ea3807623141f4b81ae30a6c886b3ee1")]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: AssemblyInformationalVersion("6.9.5")]
[assembly: InternalsVisibleTo("MySql.Data.Entity, PublicKey = 0024000004800000940000000602000000240000525341310004000001000100d973bda91f71752c78294126974a41a08643168271f65fc0fb3cd45f658da01fbca75ac74067d18e7afbf1467d7a519ce0248b13719717281bb4ddd4ecd71a580dfe0912dfc3690b1d24c7e1975bf7eed90e4ab14e10501eedf763bff8ac204f955c9c15c2cf4ebf6563d8320b6ea8d1ea3807623141f4b81ae30a6c886b3ee1")]
[assembly: InternalsVisibleTo("MySql.Data.Entity.EF6, PublicKey = 0024000004800000940000000602000000240000525341310004000001000100d973bda91f71752c78294126974a41a08643168271f65fc0fb3cd45f658da01fbca75ac74067d18e7afbf1467d7a519ce0248b13719717281bb4ddd4ecd71a580dfe0912dfc3690b1d24c7e1975bf7eed90e4ab14e10501eedf763bff8ac204f955c9c15c2cf4ebf6563d8320b6ea8d1ea3807623141f4b81ae30a6c886b3ee1")]
[assembly: InternalsVisibleTo("MySql.Data.RT.Tests, PublicKey = 0024000004800000940000000602000000240000525341310004000001000100d973bda91f71752c78294126974a41a08643168271f65fc0fb3cd45f658da01fbca75ac74067d18e7afbf1467d7a519ce0248b13719717281bb4ddd4ecd71a580dfe0912dfc3690b1d24c7e1975bf7eed90e4ab14e10501eedf763bff8ac204f955c9c15c2cf4ebf6563d8320b6ea8d1ea3807623141f4b81ae30a6c886b3ee1")]
[assembly: InternalsVisibleTo("MySql.Fabric.Plugin, PublicKey = 0024000004800000940000000602000000240000525341310004000001000100d973bda91f71752c78294126974a41a08643168271f65fc0fb3cd45f658da01fbca75ac74067d18e7afbf1467d7a519ce0248b13719717281bb4ddd4ecd71a580dfe0912dfc3690b1d24c7e1975bf7eed90e4ab14e10501eedf763bff8ac204f955c9c15c2cf4ebf6563d8320b6ea8d1ea3807623141f4b81ae30a6c886b3ee1")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: CompilationRelaxations(8)]
[assembly: AssemblyTrademark("")]
[assembly: InternalsVisibleTo("MySql.Data.CF.Tests, PublicKey = 0024000004800000940000000602000000240000525341310004000001000100d973bda91f71752c78294126974a41a08643168271f65fc0fb3cd45f658da01fbca75ac74067d18e7afbf1467d7a519ce0248b13719717281bb4ddd4ecd71a580dfe0912dfc3690b1d24c7e1975bf7eed90e4ab14e10501eedf763bff8ac204f955c9c15c2cf4ebf6563d8320b6ea8d1ea3807623141f4b81ae30a6c886b3ee1")]
[assembly: AssemblyDescription("ADO.Net driver for MySQL")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Oracle")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("Copyright © 2004, 2013, Oracle and/or its affiliates. All rights reserved.")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]
[assembly: AssemblyTitle("MySql.Data.dll")]
[assembly: AllowPartiallyTrustedCallers]
[assembly: AssemblyDelaySign(false)]
[assembly: InternalsVisibleTo("MySql.Data.Tests, PublicKey = 0024000004800000940000000602000000240000525341310004000001000100d973bda91f71752c78294126974a41a08643168271f65fc0fb3cd45f658da01fbca75ac74067d18e7afbf1467d7a519ce0248b13719717281bb4ddd4ecd71a580dfe0912dfc3690b1d24c7e1975bf7eed90e4ab14e10501eedf763bff8ac204f955c9c15c2cf4ebf6563d8320b6ea8d1ea3807623141f4b81ae30a6c886b3ee1")]
[assembly: AssemblyVersion("6.9.5.0")]
namespace MySql.Data.MySqlClient.Authentication
{
	internal class AuthenticationPluginManager
	{
		private static Dictionary<string, PluginInfo> plugins;

		static AuthenticationPluginManager()
		{
			plugins = new Dictionary<string, PluginInfo>();
			plugins["mysql_native_password"] = new PluginInfo("MySql.Data.MySqlClient.Authentication.MySqlNativePasswordPlugin");
			plugins["sha256_password"] = new PluginInfo("MySql.Data.MySqlClient.Authentication.Sha256AuthenticationPlugin");
			plugins["authentication_windows_client"] = new PluginInfo("MySql.Data.MySqlClient.Authentication.MySqlWindowsAuthenticationPlugin");
			if (MySqlConfiguration.Settings == null || MySqlConfiguration.Settings.AuthenticationPlugins == null)
			{
				return;
			}
			foreach (AuthenticationPluginConfigurationElement authenticationPlugin in MySqlConfiguration.Settings.AuthenticationPlugins)
			{
				plugins[authenticationPlugin.Name] = new PluginInfo(authenticationPlugin.Type);
			}
		}

		public static MySqlAuthenticationPlugin GetPlugin(string method)
		{
			if (!plugins.ContainsKey(method))
			{
				throw new MySqlException(string.Format(Resources.AuthenticationMethodNotSupported, method));
			}
			return CreatePlugin(method);
		}

		private static MySqlAuthenticationPlugin CreatePlugin(string method)
		{
			PluginInfo pluginInfo = plugins[method];
			try
			{
				Type type = Type.GetType(pluginInfo.Type);
				return (MySqlAuthenticationPlugin)Activator.CreateInstance(type);
			}
			catch (Exception ex)
			{
				throw new MySqlException(string.Format(Resources.UnableToCreateAuthPlugin, method), ex);
			}
		}
	}
	internal struct PluginInfo
	{
		public string Type;

		public Assembly Assembly;

		public PluginInfo(string type)
		{
			Type = type;
			Assembly = null;
		}
	}
	public abstract class MySqlAuthenticationPlugin
	{
		private NativeDriver driver;

		protected byte[] AuthenticationData;

		protected MySqlConnectionStringBuilder Settings => driver.Settings;

		protected Version ServerVersion => new Version(driver.Version.Major, driver.Version.Minor, driver.Version.Build);

		internal ClientFlags Flags => driver.Flags;

		protected Encoding Encoding => driver.Encoding;

		public abstract string PluginName { get; }

		internal static MySqlAuthenticationPlugin GetPlugin(string method, NativeDriver driver, byte[] authData)
		{
			if (method == "mysql_old_password")
			{
				driver.Close(isOpen: true);
				throw new MySqlException(Resources.OldPasswordsNotSupported);
			}
			MySqlAuthenticationPlugin plugin = AuthenticationPluginManager.GetPlugin(method);
			if (plugin == null)
			{
				throw new MySqlException(string.Format(Resources.UnknownAuthenticationMethod, method));
			}
			plugin.driver = driver;
			plugin.SetAuthData(authData);
			return plugin;
		}

		protected virtual void SetAuthData(byte[] data)
		{
			AuthenticationData = data;
		}

		protected virtual void CheckConstraints()
		{
		}

		protected virtual void AuthenticationFailed(Exception ex)
		{
			string msg = string.Format(Resources.AuthenticationFailed, Settings.Server, GetUsername(), PluginName, ex.Message);
			throw new MySqlException(msg, ex);
		}

		protected virtual void AuthenticationSuccessful()
		{
		}

		protected virtual byte[] MoreData(byte[] data)
		{
			return null;
		}

		internal void Authenticate(bool reset)
		{
			CheckConstraints();
			MySqlPacket packet = driver.Packet;
			packet.WriteString(GetUsername());
			WritePassword(packet);
			if (((Flags & ClientFlags.CONNECT_WITH_DB) != 0 || reset) && !string.IsNullOrEmpty(Settings.Database))
			{
				packet.WriteString(Settings.Database);
			}
			if (reset)
			{
				packet.WriteInteger(8L, 2);
			}
			if ((Flags & ClientFlags.PLUGIN_AUTH) != 0)
			{
				packet.WriteString(PluginName);
			}
			driver.SetConnectAttrs();
			driver.SendPacket(packet);
			packet = ReadPacket();
			byte[] buffer = packet.Buffer;
			if (buffer[0] == 254)
			{
				if (packet.IsLastPacket)
				{
					driver.Close(isOpen: true);
					throw new MySqlException(Resources.OldPasswordsNotSupported);
				}
				HandleAuthChange(packet);
			}
			driver.ReadOk(read: false);
			AuthenticationSuccessful();
		}

		private void WritePassword(MySqlPacket packet)
		{
			bool flag = (Flags & ClientFlags.SECURE_CONNECTION) != 0;
			object password = GetPassword();
			if (password is string)
			{
				if (flag)
				{
					packet.WriteLenString((string)password);
				}
				else
				{
					packet.WriteString((string)password);
				}
				return;
			}
			if (password == null)
			{
				packet.WriteByte(0);
				return;
			}
			if (password is byte[])
			{
				packet.Write(password as byte[]);
				return;
			}
			throw new MySqlException("Unexpected password format: " + password.GetType());
		}

		private MySqlPacket ReadPacket()
		{
			try
			{
				return driver.ReadPacket();
			}
			catch (MySqlException ex)
			{
				AuthenticationFailed(ex);
				return null;
			}
		}

		private void HandleAuthChange(MySqlPacket packet)
		{
			packet.ReadByte();
			string method = packet.ReadString();
			byte[] array = new byte[packet.Length - packet.Position];
			Array.Copy(packet.Buffer, packet.Position, array, 0, array.Length);
			MySqlAuthenticationPlugin plugin = GetPlugin(method, driver, array);
			plugin.AuthenticationChange();
		}

		private void AuthenticationChange()
		{
			MySqlPacket mySqlPacket = driver.Packet;
			mySqlPacket.Clear();
			byte[] array = MoreData(null);
			while (array != null && array.Length > 0)
			{
				mySqlPacket.Clear();
				mySqlPacket.Write(array);
				driver.SendPacket(mySqlPacket);
				mySqlPacket = ReadPacket();
				byte b = mySqlPacket.Buffer[0];
				if (b != 1)
				{
					return;
				}
				byte[] array2 = new byte[mySqlPacket.Length - 1];
				Array.Copy(mySqlPacket.Buffer, 1, array2, 0, array2.Length);
				array = MoreData(array2);
			}
			ReadPacket();
		}

		public virtual string GetUsername()
		{
			return Settings.UserID;
		}

		public virtual object GetPassword()
		{
			return null;
		}
	}
	public class MySqlNativePasswordPlugin : MySqlAuthenticationPlugin
	{
		public override string PluginName => "mysql_native_password";

		protected override void SetAuthData(byte[] data)
		{
			if (data[^1] == 0)
			{
				byte[] array = new byte[data.Length - 1];
				Buffer.BlockCopy(data, 0, array, 0, data.Length - 1);
				base.SetAuthData(array);
			}
			else
			{
				base.SetAuthData(data);
			}
		}

		protected override byte[] MoreData(byte[] data)
		{
			byte[] array = GetPassword() as byte[];
			byte[] array2 = new byte[array.Length - 1];
			Array.Copy(array, 1, array2, 0, array.Length - 1);
			return array2;
		}

		public override object GetPassword()
		{
			byte[] array = Get411Password(base.Settings.Password, AuthenticationData);
			if (array != null && array.Length == 1 && array[0] == 0)
			{
				return null;
			}
			return array;
		}

		private byte[] Get411Password(string password, byte[] seedBytes)
		{
			if (password.Length == 0)
			{
				return new byte[1];
			}
			SHA1 sHA = new SHA1CryptoServiceProvider();
			byte[] array = sHA.ComputeHash(Encoding.Default.GetBytes(password));
			byte[] array2 = sHA.ComputeHash(array);
			byte[] array3 = new byte[seedBytes.Length + array2.Length];
			Array.Copy(seedBytes, 0, array3, 0, seedBytes.Length);
			Array.Copy(array2, 0, array3, seedBytes.Length, array2.Length);
			byte[] array4 = sHA.ComputeHash(array3);
			byte[] array5 = new byte[array4.Length + 1];
			array5[0] = 20;
			Array.Copy(array4, 0, array5, 1, array4.Length);
			for (int i = 1; i < array5.Length; i++)
			{
				array5[i] ^= array[i - 1];
			}
			return array5;
		}
	}
	public class Sha256AuthenticationPlugin : MySqlAuthenticationPlugin
	{
		private byte[] rawPubkey;

		public override string PluginName => "sha256_password";

		protected override byte[] MoreData(byte[] data)
		{
			rawPubkey = data;
			return GetPassword() as byte[];
		}

		public override object GetPassword()
		{
			if (base.Settings.SslMode != MySqlSslMode.None)
			{
				byte[] bytes = base.Encoding.GetBytes(base.Settings.Password);
				byte[] array = new byte[bytes.Length + 1];
				Array.Copy(bytes, 0, array, 0, bytes.Length);
				array[bytes.Length] = 0;
				return array;
			}
			throw new NotImplementedException("You can use sha256 plugin only in SSL connections in this implementation.");
		}
	}
	[SuppressUnmanagedCodeSecurity]
	internal class MySqlWindowsAuthenticationPlugin : MySqlAuthenticationPlugin
	{
		private const int SEC_E_OK = 0;

		private const int SEC_I_CONTINUE_NEEDED = 590610;

		private const int SEC_I_COMPLETE_NEEDED = 4115;

		private const int SEC_I_COMPLETE_AND_CONTINUE = 4116;

		private const int SECPKG_CRED_OUTBOUND = 2;

		private const int SECURITY_NETWORK_DREP = 0;

		private const int SECURITY_NATIVE_DREP = 16;

		private const int SECPKG_CRED_INBOUND = 1;

		private const int MAX_TOKEN_SIZE = 12288;

		private const int SECPKG_ATTR_SIZES = 0;

		private const int STANDARD_CONTEXT_ATTRIBUTES = 0;

		private SECURITY_HANDLE outboundCredentials = new SECURITY_HANDLE(0);

		private SECURITY_HANDLE clientContext = new SECURITY_HANDLE(0);

		private SECURITY_INTEGER lifetime = new SECURITY_INTEGER(0);

		private bool continueProcessing;

		private string targetName;

		public override string PluginName => "authentication_windows_client";

		protected override void CheckConstraints()
		{
			string text = string.Empty;
			int platform = (int)Environment.OSVersion.Platform;
			if (platform == 4 || platform == 128)
			{
				text = "Unix";
			}
			else if (Environment.OSVersion.Platform == PlatformID.MacOSX)
			{
				text = "Mac OS/X";
			}
			if (!string.IsNullOrEmpty(text))
			{
				throw new MySqlException(string.Format(Resources.WinAuthNotSupportOnPlatform, text));
			}
			base.CheckConstraints();
		}

		public override string GetUsername()
		{
			string username = base.GetUsername();
			if (string.IsNullOrEmpty(username))
			{
				return "auth_windows";
			}
			return username;
		}

		protected override byte[] MoreData(byte[] moreData)
		{
			if (moreData == null)
			{
				AcquireCredentials();
			}
			byte[] clientBlob = null;
			if (continueProcessing)
			{
				InitializeClient(out clientBlob, moreData, out continueProcessing);
			}
			if (!continueProcessing || clientBlob == null || clientBlob.Length == 0)
			{
				FreeCredentialsHandle(ref outboundCredentials);
				DeleteSecurityContext(ref clientContext);
				return null;
			}
			return clientBlob;
		}

		private void InitializeClient(out byte[] clientBlob, byte[] serverBlob, out bool continueProcessing)
		{
			clientBlob = null;
			continueProcessing = true;
			SecBufferDesc pOutput = new SecBufferDesc(12288);
			SECURITY_INTEGER ptsExpiry = new SECURITY_INTEGER(0);
			int num = -1;
			try
			{
				uint pfContextAttr = 0u;
				if (serverBlob == null)
				{
					num = InitializeSecurityContext(ref outboundCredentials, IntPtr.Zero, targetName, 0, 0, 0, IntPtr.Zero, 0, out clientContext, out pOutput, out pfContextAttr, out ptsExpiry);
				}
				else
				{
					SecBufferDesc SecBufferDesc = new SecBufferDesc(serverBlob);
					try
					{
						num = InitializeSecurityContext(ref outboundCredentials, ref clientContext, targetName, 0, 0, 0, ref SecBufferDesc, 0, out clientContext, out pOutput, out pfContextAttr, out ptsExpiry);
					}
					finally
					{
						SecBufferDesc.Dispose();
					}
				}
				if (4115 == num || 4116 == num)
				{
					CompleteAuthToken(ref clientContext, ref pOutput);
				}
				if (num != 0 && num != 590610 && num != 4115 && num != 4116)
				{
					throw new MySqlException("InitializeSecurityContext() failed  with errorcode " + num);
				}
				clientBlob = pOutput.GetSecBufferByteArray();
			}
			finally
			{
				pOutput.Dispose();
			}
			continueProcessing = num != 0 && num != 4115;
		}

		private string GetTargetName()
		{
			return null;
		}

		private void AcquireCredentials()
		{
			continueProcessing = true;
			int num = AcquireCredentialsHandle(null, "Negotiate", 2, IntPtr.Zero, IntPtr.Zero, 0, IntPtr.Zero, ref outboundCredentials, ref lifetime);
			if (num != 0)
			{
				throw new MySqlException("AcquireCredentialsHandle failed with errorcode" + num);
			}
		}

		[DllImport("secur32", CharSet = CharSet.Unicode)]
		private static extern int AcquireCredentialsHandle(string pszPrincipal, string pszPackage, int fCredentialUse, IntPtr PAuthenticationID, IntPtr pAuthData, int pGetKeyFn, IntPtr pvGetKeyArgument, ref SECURITY_HANDLE phCredential, ref SECURITY_INTEGER ptsExpiry);

		[DllImport("secur32", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int InitializeSecurityContext(ref SECURITY_HANDLE phCredential, IntPtr phContext, string pszTargetName, int fContextReq, int Reserved1, int TargetDataRep, IntPtr pInput, int Reserved2, out SECURITY_HANDLE phNewContext, out SecBufferDesc pOutput, out uint pfContextAttr, out SECURITY_INTEGER ptsExpiry);

		[DllImport("secur32", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int InitializeSecurityContext(ref SECURITY_HANDLE phCredential, ref SECURITY_HANDLE phContext, string pszTargetName, int fContextReq, int Reserved1, int TargetDataRep, ref SecBufferDesc SecBufferDesc, int Reserved2, out SECURITY_HANDLE phNewContext, out SecBufferDesc pOutput, out uint pfContextAttr, out SECURITY_INTEGER ptsExpiry);

		[DllImport("secur32", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int CompleteAuthToken(ref SECURITY_HANDLE phContext, ref SecBufferDesc pToken);

		[DllImport("secur32.Dll", CharSet = CharSet.Unicode)]
		public static extern int QueryContextAttributes(ref SECURITY_HANDLE phContext, uint ulAttribute, out SecPkgContext_Sizes pContextAttributes);

		[DllImport("secur32.Dll", CharSet = CharSet.Unicode)]
		public static extern int FreeCredentialsHandle(ref SECURITY_HANDLE pCred);

		[DllImport("secur32.Dll", CharSet = CharSet.Unicode)]
		public static extern int DeleteSecurityContext(ref SECURITY_HANDLE pCred);
	}
	internal struct SecBufferDesc : IDisposable
	{
		public int ulVersion;

		public int cBuffers;

		public IntPtr pBuffers;

		public SecBufferDesc(int bufferSize)
		{
			ulVersion = 0;
			cBuffers = 1;
			SecBuffer secBuffer = new SecBuffer(bufferSize);
			pBuffers = Marshal.AllocHGlobal(Marshal.SizeOf((object)secBuffer));
			Marshal.StructureToPtr((object)secBuffer, pBuffers, fDeleteOld: false);
		}

		public SecBufferDesc(byte[] secBufferBytes)
		{
			ulVersion = 0;
			cBuffers = 1;
			SecBuffer secBuffer = new SecBuffer(secBufferBytes);
			pBuffers = Marshal.AllocHGlobal(Marshal.SizeOf((object)secBuffer));
			Marshal.StructureToPtr((object)secBuffer, pBuffers, fDeleteOld: false);
		}

		public void Dispose()
		{
			if (pBuffers != IntPtr.Zero)
			{
				((SecBuffer)Marshal.PtrToStructure(pBuffers, typeof(SecBuffer))).Dispose();
				Marshal.FreeHGlobal(pBuffers);
				pBuffers = IntPtr.Zero;
			}
		}

		public byte[] GetSecBufferByteArray()
		{
			byte[] array = null;
			if (pBuffers == IntPtr.Zero)
			{
				throw new InvalidOperationException("Object has already been disposed!!!");
			}
			SecBuffer secBuffer = (SecBuffer)Marshal.PtrToStructure(pBuffers, typeof(SecBuffer));
			if (secBuffer.cbBuffer > 0)
			{
				array = new byte[secBuffer.cbBuffer];
				Marshal.Copy(secBuffer.pvBuffer, array, 0, secBuffer.cbBuffer);
			}
			return array;
		}
	}
	public enum SecBufferType
	{
		SECBUFFER_VERSION = 0,
		SECBUFFER_EMPTY = 0,
		SECBUFFER_DATA = 1,
		SECBUFFER_TOKEN = 2
	}
	public struct SecHandle
	{
		private IntPtr dwLower;

		private IntPtr dwUpper;
	}
	public struct SecBuffer : IDisposable
	{
		public int cbBuffer;

		public int BufferType;

		public IntPtr pvBuffer;

		public SecBuffer(int bufferSize)
		{
			cbBuffer = bufferSize;
			BufferType = 2;
			pvBuffer = Marshal.AllocHGlobal(bufferSize);
		}

		public SecBuffer(byte[] secBufferBytes)
		{
			cbBuffer = secBufferBytes.Length;
			BufferType = 2;
			pvBuffer = Marshal.AllocHGlobal(cbBuffer);
			Marshal.Copy(secBufferBytes, 0, pvBuffer, cbBuffer);
		}

		public SecBuffer(byte[] secBufferBytes, SecBufferType bufferType)
		{
			cbBuffer = secBufferBytes.Length;
			BufferType = (int)bufferType;
			pvBuffer = Marshal.AllocHGlobal(cbBuffer);
			Marshal.Copy(secBufferBytes, 0, pvBuffer, cbBuffer);
		}

		public void Dispose()
		{
			if (pvBuffer != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(pvBuffer);
				pvBuffer = IntPtr.Zero;
			}
		}
	}
	public struct SECURITY_INTEGER
	{
		public uint LowPart;

		public int HighPart;

		public SECURITY_INTEGER(int dummy)
		{
			LowPart = 0u;
			HighPart = 0;
		}
	}
	public struct SECURITY_HANDLE
	{
		public IntPtr LowPart;

		public IntPtr HighPart;

		public SECURITY_HANDLE(int dummy)
		{
			LowPart = (HighPart = new IntPtr(0));
		}
	}
	public struct SecPkgContext_Sizes
	{
		public uint cbMaxToken;

		public uint cbMaxSignature;

		public uint cbBlockSize;

		public uint cbSecurityTrailer;
	}
}
namespace MySql.Data.MySqlClient
{
	public class MySqlBulkLoader
	{
		private const string defaultFieldTerminator = "\t";

		private const string defaultLineTerminator = "\n";

		private const char defaultEscapeCharacter = '\\';

		private string fieldTerminator;

		private string lineTerminator;

		private string charSet;

		private string tableName;

		private int numLinesToIgnore;

		private MySqlConnection connection;

		private string filename;

		private int timeout;

		private bool local;

		private string linePrefix;

		private char fieldQuotationCharacter;

		private bool fieldQuotationOptional;

		private char escapeChar;

		private MySqlBulkLoaderPriority priority;

		private MySqlBulkLoaderConflictOption conflictOption;

		private List<string> columns;

		private List<string> expressions;

		public MySqlConnection Connection
		{
			get
			{
				return connection;
			}
			set
			{
				connection = value;
			}
		}

		public string FieldTerminator
		{
			get
			{
				return fieldTerminator;
			}
			set
			{
				fieldTerminator = value;
			}
		}

		public string LineTerminator
		{
			get
			{
				return lineTerminator;
			}
			set
			{
				lineTerminator = value;
			}
		}

		public string TableName
		{
			get
			{
				return tableName;
			}
			set
			{
				tableName = value;
			}
		}

		public string CharacterSet
		{
			get
			{
				return charSet;
			}
			set
			{
				charSet = value;
			}
		}

		public string FileName
		{
			get
			{
				return filename;
			}
			set
			{
				filename = value;
			}
		}

		public int Timeout
		{
			get
			{
				return timeout;
			}
			set
			{
				timeout = value;
			}
		}

		public bool Local
		{
			get
			{
				return local;
			}
			set
			{
				local = value;
			}
		}

		public int NumberOfLinesToSkip
		{
			get
			{
				return numLinesToIgnore;
			}
			set
			{
				numLinesToIgnore = value;
			}
		}

		public string LinePrefix
		{
			get
			{
				return linePrefix;
			}
			set
			{
				linePrefix = value;
			}
		}

		public char FieldQuotationCharacter
		{
			get
			{
				return fieldQuotationCharacter;
			}
			set
			{
				fieldQuotationCharacter = value;
			}
		}

		public bool FieldQuotationOptional
		{
			get
			{
				return fieldQuotationOptional;
			}
			set
			{
				fieldQuotationOptional = value;
			}
		}

		public char EscapeCharacter
		{
			get
			{
				return escapeChar;
			}
			set
			{
				escapeChar = value;
			}
		}

		public MySqlBulkLoaderConflictOption ConflictOption
		{
			get
			{
				return conflictOption;
			}
			set
			{
				conflictOption = value;
			}
		}

		public MySqlBulkLoaderPriority Priority
		{
			get
			{
				return priority;
			}
			set
			{
				priority = value;
			}
		}

		public List<string> Columns => columns;

		public List<string> Expressions => expressions;

		public MySqlBulkLoader(MySqlConnection connection)
		{
			Connection = connection;
			Local = true;
			FieldTerminator = "\t";
			LineTerminator = "\n";
			FieldQuotationCharacter = '\0';
			ConflictOption = MySqlBulkLoaderConflictOption.None;
			columns = new List<string>();
			expressions = new List<string>();
		}

		public int Load()
		{
			bool flag = false;
			if (Connection == null)
			{
				throw new InvalidOperationException(Resources.ConnectionNotSet);
			}
			if (connection.State != ConnectionState.Open)
			{
				flag = true;
				connection.Open();
			}
			try
			{
				string cmdText = BuildSqlCommand();
				MySqlCommand mySqlCommand = new MySqlCommand(cmdText, Connection);
				mySqlCommand.CommandTimeout = Timeout;
				return mySqlCommand.ExecuteNonQuery();
			}
			finally
			{
				if (flag)
				{
					connection.Close();
				}
			}
		}

		private string BuildSqlCommand()
		{
			StringBuilder stringBuilder = new StringBuilder("LOAD DATA ");
			if (Priority == MySqlBulkLoaderPriority.Low)
			{
				stringBuilder.Append("LOW_PRIORITY ");
			}
			else if (Priority == MySqlBulkLoaderPriority.Concurrent)
			{
				stringBuilder.Append("CONCURRENT ");
			}
			if (Local)
			{
				stringBuilder.Append("LOCAL ");
			}
			stringBuilder.Append("INFILE ");
			if (Platform.DirectorySeparatorChar == '\\')
			{
				stringBuilder.AppendFormat("'{0}' ", FileName.Replace("\\", "\\\\"));
			}
			else
			{
				stringBuilder.AppendFormat("'{0}' ", FileName);
			}
			if (ConflictOption == MySqlBulkLoaderConflictOption.Ignore)
			{
				stringBuilder.Append("IGNORE ");
			}
			else if (ConflictOption == MySqlBulkLoaderConflictOption.Replace)
			{
				stringBuilder.Append("REPLACE ");
			}
			stringBuilder.AppendFormat("INTO TABLE {0} ", TableName);
			if (CharacterSet != null)
			{
				stringBuilder.AppendFormat("CHARACTER SET {0} ", CharacterSet);
			}
			StringBuilder stringBuilder2 = new StringBuilder(string.Empty);
			if (FieldTerminator != "\t")
			{
				stringBuilder2.AppendFormat("TERMINATED BY '{0}' ", FieldTerminator);
			}
			if (FieldQuotationCharacter != 0)
			{
				stringBuilder2.AppendFormat("{0} ENCLOSED BY '{1}' ", FieldQuotationOptional ? "OPTIONALLY" : "", FieldQuotationCharacter);
			}
			if (EscapeCharacter != '\\' && EscapeCharacter != 0)
			{
				stringBuilder2.AppendFormat("ESCAPED BY '{0}' ", EscapeCharacter);
			}
			if (stringBuilder2.Length > 0)
			{
				stringBuilder.AppendFormat("FIELDS {0}", stringBuilder2.ToString());
			}
			stringBuilder2 = new StringBuilder(string.Empty);
			if (LinePrefix != null && LinePrefix.Length > 0)
			{
				stringBuilder2.AppendFormat("STARTING BY '{0}' ", LinePrefix);
			}
			if (LineTerminator != "\n")
			{
				stringBuilder2.AppendFormat("TERMINATED BY '{0}' ", LineTerminator);
			}
			if (stringBuilder2.Length > 0)
			{
				stringBuilder.AppendFormat("LINES {0}", stringBuilder2.ToString());
			}
			if (NumberOfLinesToSkip > 0)
			{
				stringBuilder.AppendFormat("IGNORE {0} LINES ", NumberOfLinesToSkip);
			}
			if (Columns.Count > 0)
			{
				stringBuilder.Append("(");
				stringBuilder.Append(Columns[0]);
				for (int i = 1; i < Columns.Count; i++)
				{
					stringBuilder.AppendFormat(",{0}", Columns[i]);
				}
				stringBuilder.Append(") ");
			}
			if (Expressions.Count > 0)
			{
				stringBuilder.Append("SET ");
				stringBuilder.Append(Expressions[0]);
				for (int j = 1; j < Expressions.Count; j++)
				{
					stringBuilder.AppendFormat(",{0}", Expressions[j]);
				}
			}
			return stringBuilder.ToString();
		}
	}
	public enum MySqlBulkLoaderPriority
	{
		None,
		Low,
		Concurrent
	}
	public enum MySqlBulkLoaderConflictOption
	{
		None,
		Replace,
		Ignore
	}
	internal class CharSetMap
	{
		private static Dictionary<string, string> defaultCollations;

		private static Dictionary<string, int> maxLengths;

		private static Dictionary<string, CharacterSet> mapping;

		private static object lockObject;

		static CharSetMap()
		{
			lockObject = new object();
			InitializeMapping();
		}

		public static CharacterSet GetCharacterSet(DBVersion version, string CharSetName)
		{
			CharacterSet characterSet = null;
			if (mapping.ContainsKey(CharSetName))
			{
				characterSet = mapping[CharSetName];
			}
			if (characterSet == null)
			{
				throw new MySqlException("Character set '" + CharSetName + "' is not supported by .Net Framework.");
			}
			return characterSet;
		}

		public static Encoding GetEncoding(DBVersion version, string CharSetName)
		{
			try
			{
				CharacterSet characterSet = GetCharacterSet(version, CharSetName);
				return Encoding.GetEncoding(characterSet.name);
			}
			catch (NotSupportedException)
			{
				return Encoding.GetEncoding("utf-8");
			}
		}

		private static void InitializeMapping()
		{
			LoadCharsetMap();
		}

		private static void LoadCharsetMap()
		{
			mapping = new Dictionary<string, CharacterSet>();
			mapping.Add("latin1", new CharacterSet("windows-1252", 1));
			mapping.Add("big5", new CharacterSet("big5", 2));
			mapping.Add("dec8", mapping["latin1"]);
			mapping.Add("cp850", new CharacterSet("ibm850", 1));
			mapping.Add("hp8", mapping["latin1"]);
			mapping.Add("koi8r", new CharacterSet("koi8-u", 1));
			mapping.Add("latin2", new CharacterSet("latin2", 1));
			mapping.Add("swe7", mapping["latin1"]);
			mapping.Add("ujis", new CharacterSet("EUC-JP", 3));
			mapping.Add("eucjpms", mapping["ujis"]);
			mapping.Add("sjis", new CharacterSet("sjis", 2));
			mapping.Add("cp932", mapping["sjis"]);
			mapping.Add("hebrew", new CharacterSet("hebrew", 1));
			mapping.Add("tis620", new CharacterSet("windows-874", 1));
			mapping.Add("euckr", new CharacterSet("euc-kr", 2));
			mapping.Add("euc_kr", mapping["euckr"]);
			mapping.Add("koi8u", new CharacterSet("koi8-u", 1));
			mapping.Add("koi8_ru", mapping["koi8u"]);
			mapping.Add("gb2312", new CharacterSet("gb2312", 2));
			mapping.Add("gbk", mapping["gb2312"]);
			mapping.Add("greek", new CharacterSet("greek", 1));
			mapping.Add("cp1250", new CharacterSet("windows-1250", 1));
			mapping.Add("win1250", mapping["cp1250"]);
			mapping.Add("latin5", new CharacterSet("latin5", 1));
			mapping.Add("armscii8", mapping["latin1"]);
			mapping.Add("utf8", new CharacterSet("utf-8", 3));
			mapping.Add("ucs2", new CharacterSet("UTF-16BE", 2));
			mapping.Add("cp866", new CharacterSet("cp866", 1));
			mapping.Add("keybcs2", mapping["latin1"]);
			mapping.Add("macce", new CharacterSet("x-mac-ce", 1));
			mapping.Add("macroman", new CharacterSet("x-mac-romanian", 1));
			mapping.Add("cp852", new CharacterSet("ibm852", 2));
			mapping.Add("latin7", new CharacterSet("iso-8859-7", 1));
			mapping.Add("cp1251", new CharacterSet("windows-1251", 1));
			mapping.Add("win1251ukr", mapping["cp1251"]);
			mapping.Add("cp1251csas", mapping["cp1251"]);
			mapping.Add("cp1251cias", mapping["cp1251"]);
			mapping.Add("win1251", mapping["cp1251"]);
			mapping.Add("cp1256", new CharacterSet("cp1256", 1));
			mapping.Add("cp1257", new CharacterSet("windows-1257", 1));
			mapping.Add("ascii", new CharacterSet("us-ascii", 1));
			mapping.Add("usa7", mapping["ascii"]);
			mapping.Add("binary", mapping["ascii"]);
			mapping.Add("latin3", new CharacterSet("latin3", 1));
			mapping.Add("latin4", new CharacterSet("latin4", 1));
			mapping.Add("latin1_de", new CharacterSet("iso-8859-1", 1));
			mapping.Add("german1", new CharacterSet("iso-8859-1", 1));
			mapping.Add("danish", new CharacterSet("iso-8859-1", 1));
			mapping.Add("czech", new CharacterSet("iso-8859-2", 1));
			mapping.Add("hungarian", new CharacterSet("iso-8859-2", 1));
			mapping.Add("croat", new CharacterSet("iso-8859-2", 1));
			mapping.Add("latvian", new CharacterSet("iso-8859-13", 1));
			mapping.Add("latvian1", new CharacterSet("iso-8859-13", 1));
			mapping.Add("estonia", new CharacterSet("iso-8859-13", 1));
			mapping.Add("dos", new CharacterSet("ibm437", 1));
			mapping.Add("utf8mb4", new CharacterSet("utf-8", 4));
			mapping.Add("utf16", new CharacterSet("utf-16BE", 2));
			mapping.Add("utf16le", new CharacterSet("utf-16", 2));
			mapping.Add("utf32", new CharacterSet("utf-32BE", 4));
		}

		internal static void InitCollections(MySqlConnection connection)
		{
			defaultCollations = new Dictionary<string, string>();
			maxLengths = new Dictionary<string, int>();
			MySqlCommand mySqlCommand = new MySqlCommand("SHOW CHARSET", connection);
			using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			while (mySqlDataReader.Read())
			{
				defaultCollations.Add(mySqlDataReader.GetString(0), mySqlDataReader.GetString(2));
				maxLengths.Add(mySqlDataReader.GetString(0), Convert.ToInt32(mySqlDataReader.GetValue(3)));
			}
		}

		internal static string GetDefaultCollation(string charset, MySqlConnection connection)
		{
			lock (lockObject)
			{
				if (defaultCollations == null)
				{
					InitCollections(connection);
				}
			}
			if (!defaultCollations.ContainsKey(charset))
			{
				return null;
			}
			return defaultCollations[charset];
		}

		internal static int GetMaxLength(string charset, MySqlConnection connection)
		{
			lock (lockObject)
			{
				if (maxLengths == null)
				{
					InitCollections(connection);
				}
			}
			if (!maxLengths.ContainsKey(charset))
			{
				return 1;
			}
			return maxLengths[charset];
		}
	}
	internal class CharacterSet
	{
		public string name;

		public int byteCount;

		public CharacterSet(string name, int byteCount)
		{
			this.name = name;
			this.byteCount = byteCount;
		}
	}
	[DesignerCategory("Code")]
	[ToolboxBitmap(typeof(MySqlCommand), "MySqlClient.resources.command.bmp")]
	public sealed class MySqlCommand : DbCommand, ICloneable, IDisposable
	{
		internal delegate object AsyncDelegate(int type, CommandBehavior behavior);

		private MySqlConnection connection;

		private MySqlTransaction curTransaction;

		private string cmdText;

		private CommandType cmdType;

		private long updatedRowCount;

		private MySqlParameterCollection parameters;

		private IAsyncResult asyncResult;

		internal long lastInsertedId;

		private PreparableStatement statement;

		private int commandTimeout;

		private bool canceled;

		private bool resetSqlSelect;

		private List<MySqlCommand> batch;

		private string batchableCommandText;

		private CommandTimer commandTimer;

		private bool useDefaultTimeout;

		private bool shouldCache;

		private int cacheAge;

		private bool internallyCreated;

		internal AsyncDelegate caller;

		internal Exception thrownException;

		[Browsable(false)]
		public long LastInsertedId => lastInsertedId;

		[Category("Data")]
		[Description("Command text to execute")]
		[Editor("MySql.Data.Common.Design.SqlCommandTextEditor,MySqlClient.Design", typeof(UITypeEditor))]
		public override string CommandText
		{
			get
			{
				return cmdText;
			}
			set
			{
				cmdText = value ?? string.Empty;
				statement = null;
				batchableCommandText = null;
				if (cmdText != null && cmdText.EndsWith("DEFAULT VALUES", StringComparison.OrdinalIgnoreCase))
				{
					cmdText = cmdText.Substring(0, cmdText.Length - 14);
					cmdText += "() VALUES ()";
				}
			}
		}

		[Description("Time to wait for command to execute")]
		[DefaultValue(30)]
		[Category("Misc")]
		public override int CommandTimeout
		{
			get
			{
				if (!useDefaultTimeout)
				{
					return commandTimeout;
				}
				return 30;
			}
			set
			{
				if (commandTimeout < 0)
				{
					Throw(new ArgumentException("Command timeout must not be negative"));
				}
				int num = Math.Min(value, 2147483);
				if (num != value)
				{
					MySqlTrace.LogWarning(connection.ServerThread, "Command timeout value too large (" + value + " seconds). Changed to max. possible value (" + num + " seconds)");
				}
				commandTimeout = num;
				useDefaultTimeout = false;
			}
		}

		[Category("Data")]
		public override CommandType CommandType
		{
			get
			{
				return cmdType;
			}
			set
			{
				cmdType = value;
			}
		}

		[Browsable(false)]
		public bool IsPrepared
		{
			get
			{
				if (statement != null)
				{
					return statement.IsPrepared;
				}
				return false;
			}
		}

		[Category("Behavior")]
		[Description("Connection used by the command")]
		public new MySqlConnection Connection
		{
			get
			{
				return connection;
			}
			set
			{
				if (connection != value)
				{
					Transaction = null;
				}
				connection = value;
				if (connection != null)
				{
					if (useDefaultTimeout)
					{
						commandTimeout = (int)connection.Settings.DefaultCommandTimeout;
						useDefaultTimeout = false;
					}
					EnableCaching = connection.Settings.TableCaching;
					CacheAge = connection.Settings.DefaultTableCacheAge;
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The parameters collection")]
		[Category("Data")]
		public new MySqlParameterCollection Parameters => parameters;

		[Browsable(false)]
		public new MySqlTransaction Transaction
		{
			get
			{
				return curTransaction;
			}
			set
			{
				curTransaction = value;
			}
		}

		public bool EnableCaching
		{
			get
			{
				return shouldCache;
			}
			set
			{
				shouldCache = value;
			}
		}

		public int CacheAge
		{
			get
			{
				return cacheAge;
			}
			set
			{
				cacheAge = value;
			}
		}

		internal List<MySqlCommand> Batch => batch;

		internal bool Canceled => canceled;

		internal string BatchableCommandText => batchableCommandText;

		internal bool InternallyCreated
		{
			get
			{
				return internallyCreated;
			}
			set
			{
				internallyCreated = value;
			}
		}

		public override UpdateRowSource UpdatedRowSource { get; set; }

		[Browsable(false)]
		public override bool DesignTimeVisible { get; set; }

		protected override DbConnection DbConnection
		{
			get
			{
				return Connection;
			}
			set
			{
				Connection = (MySqlConnection)value;
			}
		}

		protected override DbParameterCollection DbParameterCollection => Parameters;

		protected override DbTransaction DbTransaction
		{
			get
			{
				return Transaction;
			}
			set
			{
				Transaction = (MySqlTransaction)value;
			}
		}

		public MySqlCommand()
		{
			cmdType = CommandType.Text;
			parameters = new MySqlParameterCollection(this);
			cmdText = string.Empty;
			useDefaultTimeout = true;
			Constructor();
		}

		public MySqlCommand(string cmdText)
			: this()
		{
			CommandText = cmdText;
		}

		public MySqlCommand(string cmdText, MySqlConnection connection)
			: this(cmdText)
		{
			Connection = connection;
		}

		public MySqlCommand(string cmdText, MySqlConnection connection, MySqlTransaction transaction)
			: this(cmdText, connection)
		{
			curTransaction = transaction;
		}

		~MySqlCommand()
		{
			Dispose(disposing: false);
		}

		public override void Cancel()
		{
			connection.CancelQuery(connection.ConnectionTimeout);
			canceled = true;
		}

		public new MySqlParameter CreateParameter()
		{
			return (MySqlParameter)CreateDbParameter();
		}

		private void CheckState()
		{
			if (connection == null)
			{
				Throw(new InvalidOperationException("Connection must be valid and open."));
			}
			if (connection.State != ConnectionState.Open && !connection.SoftClosed)
			{
				Throw(new InvalidOperationException("Connection must be valid and open."));
			}
			if (connection.IsInUse && !internallyCreated)
			{
				Throw(new MySqlException("There is already an open DataReader associated with this Connection which must be closed first."));
			}
		}

		public override int ExecuteNonQuery()
		{
			int returnValue = -1;
			if (connection != null && connection.commandInterceptor != null && connection.commandInterceptor.ExecuteNonQuery(CommandText, ref returnValue))
			{
				return returnValue;
			}
			using MySqlDataReader mySqlDataReader = ExecuteReader();
			mySqlDataReader.Close();
			return mySqlDataReader.RecordsAffected;
		}

		internal void ClearCommandTimer()
		{
			if (commandTimer != null)
			{
				commandTimer.Dispose();
				commandTimer = null;
			}
		}

		internal void Close(MySqlDataReader reader)
		{
			if (statement != null)
			{
				statement.Close(reader);
			}
			ResetSqlSelectLimit();
			if (statement != null && connection != null && connection.driver != null)
			{
				connection.driver.CloseQuery(connection, statement.StatementId);
			}
			ClearCommandTimer();
		}

		private void ResetReader()
		{
			if (connection != null && connection.Reader != null)
			{
				connection.Reader.Close();
				connection.Reader = null;
			}
		}

		internal void ResetSqlSelectLimit()
		{
			if (resetSqlSelect)
			{
				resetSqlSelect = false;
				MySqlCommand mySqlCommand = new MySqlCommand("SET SQL_SELECT_LIMIT=DEFAULT", connection);
				mySqlCommand.internallyCreated = true;
				mySqlCommand.ExecuteNonQuery();
			}
		}

		public new MySqlDataReader ExecuteReader()
		{
			return ExecuteReader(CommandBehavior.Default);
		}

		public new MySqlDataReader ExecuteReader(CommandBehavior behavior)
		{
			MySqlDataReader returnValue = null;
			if (connection != null && connection.commandInterceptor != null && connection.commandInterceptor.ExecuteReader(CommandText, behavior, ref returnValue))
			{
				return returnValue;
			}
			bool flag = false;
			CheckState();
			Driver driver = connection.driver;
			cmdText = cmdText.Trim();
			if (string.IsNullOrEmpty(cmdText))
			{
				Throw(new InvalidOperationException(Resources.CommandTextNotInitialized));
			}
			string text = cmdText.Trim(new char[1] { ';' });
			if (connection.hasBeenOpen && !driver.HasStatus(ServerStatusFlags.InTransaction))
			{
				ReplicationManager.GetNewConnection(connection.Settings.Server, !IsReadOnlyCommand(text), connection);
			}
			lock (driver)
			{
				if (connection.Reader != null)
				{
					Throw(new MySqlException(Resources.DataReaderOpen));
				}
				Transaction current = System.Transactions.Transaction.Current;
				if (current != null)
				{
					bool flag2 = false;
					if (driver.CurrentTransaction != null)
					{
						flag2 = driver.CurrentTransaction.InRollback;
					}
					if (!flag2)
					{
						TransactionStatus transactionStatus = TransactionStatus.InDoubt;
						try
						{
							transactionStatus = current.TransactionInformation.Status;
						}
						catch (TransactionException)
						{
						}
						if (transactionStatus == TransactionStatus.Aborted)
						{
							Throw(new TransactionAbortedException());
						}
					}
				}
				commandTimer = new CommandTimer(connection, CommandTimeout);
				lastInsertedId = -1L;
				if (CommandType == CommandType.TableDirect)
				{
					text = "SELECT * FROM " + text;
				}
				else if (CommandType == CommandType.Text && text.IndexOf(" ") == -1 && AddCallStatement(text))
				{
					text = "call " + text;
				}
				if (connection.Settings.Replication && !InternallyCreated)
				{
					EnsureCommandIsReadOnly(text);
				}
				if (statement == null || !statement.IsPrepared)
				{
					if (CommandType == CommandType.StoredProcedure)
					{
						statement = new StoredProcedure(this, text);
					}
					else
					{
						statement = new PreparableStatement(this, text);
					}
				}
				statement.Resolve(preparing: false);
				HandleCommandBehaviors(behavior);
				updatedRowCount = -1L;
				try
				{
					MySqlDataReader mySqlDataReader = new MySqlDataReader(this, statement, behavior);
					connection.Reader = mySqlDataReader;
					canceled = false;
					statement.Execute();
					mySqlDataReader.NextResult();
					flag = true;
					return mySqlDataReader;
				}
				catch (TimeoutException ex2)
				{
					connection.HandleTimeoutOrThreadAbort(ex2);
					throw;
				}
				catch (ThreadAbortException ex3)
				{
					connection.HandleTimeoutOrThreadAbort(ex3);
					throw;
				}
				catch (IOException ex4)
				{
					connection.Abort();
					throw new MySqlException(Resources.FatalErrorDuringExecute, ex4);
				}
				catch (MySqlException ex5)
				{
					if (ex5.InnerException is TimeoutException)
					{
						throw;
					}
					try
					{
						ResetReader();
						ResetSqlSelectLimit();
					}
					catch (Exception)
					{
						Connection.Abort();
						throw new MySqlException(ex5.Message, isFatal: true, ex5);
					}
					if (ex5.IsQueryAborted)
					{
						return null;
					}
					if (ex5.IsFatal)
					{
						Connection.Close();
					}
					if (ex5.Number == 0)
					{
						throw new MySqlException(Resources.FatalErrorDuringExecute, ex5);
					}
					throw;
				}
				finally
				{
					if (connection != null)
					{
						if (connection.Reader == null)
						{
							ClearCommandTimer();
						}
						if (!flag)
						{
							ResetReader();
						}
					}
				}
			}
		}

		private void EnsureCommandIsReadOnly(string sql)
		{
			sql = StringUtility.ToLowerInvariant(sql);
			if (!sql.StartsWith("select") && !sql.StartsWith("show"))
			{
				Throw(new MySqlException(Resources.ReplicatedConnectionsAllowOnlyReadonlyStatements));
			}
			if (sql.EndsWith("for update") || sql.EndsWith("lock in share mode"))
			{
				Throw(new MySqlException(Resources.ReplicatedConnectionsAllowOnlyReadonlyStatements));
			}
		}

		private bool IsReadOnlyCommand(string sql)
		{
			sql = sql.ToLower();
			if (sql.StartsWith("select") || sql.StartsWith("show"))
			{
				if (!sql.EndsWith("for update"))
				{
					return !sql.EndsWith("lock in share mode");
				}
				return false;
			}
			return false;
		}

		public override object ExecuteScalar()
		{
			lastInsertedId = -1L;
			object returnValue = null;
			if (connection != null && connection.commandInterceptor.ExecuteScalar(CommandText, ref returnValue))
			{
				return returnValue;
			}
			using (MySqlDataReader mySqlDataReader = ExecuteReader())
			{
				if (mySqlDataReader.Read())
				{
					returnValue = mySqlDataReader.GetValue(0);
				}
			}
			return returnValue;
		}

		private void HandleCommandBehaviors(CommandBehavior behavior)
		{
			if ((behavior & CommandBehavior.SchemaOnly) != CommandBehavior.Default)
			{
				new MySqlCommand("SET SQL_SELECT_LIMIT=0", connection).ExecuteNonQuery();
				resetSqlSelect = true;
			}
			else if ((behavior & CommandBehavior.SingleRow) != CommandBehavior.Default)
			{
				new MySqlCommand("SET SQL_SELECT_LIMIT=1", connection).ExecuteNonQuery();
				resetSqlSelect = true;
			}
		}

		private void Prepare(int cursorPageSize)
		{
			using (new CommandTimer(Connection, CommandTimeout))
			{
				string commandText = CommandText;
				if (commandText != null && commandText.Trim().Length != 0)
				{
					if (CommandType == CommandType.StoredProcedure)
					{
						statement = new StoredProcedure(this, CommandText);
					}
					else
					{
						statement = new PreparableStatement(this, CommandText);
					}
					statement.Resolve(preparing: true);
					statement.Prepare();
				}
			}
		}

		public override void Prepare()
		{
			if (connection == null)
			{
				Throw(new InvalidOperationException("The connection property has not been set."));
			}
			if (connection.State != ConnectionState.Open)
			{
				Throw(new InvalidOperationException("The connection is not open."));
			}
			if (!connection.Settings.IgnorePrepare)
			{
				Prepare(0);
			}
		}

		internal object AsyncExecuteWrapper(int type, CommandBehavior behavior)
		{
			thrownException = null;
			try
			{
				if (type == 1)
				{
					return ExecuteReader(behavior);
				}
				return ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				thrownException = ex;
			}
			return null;
		}

		public IAsyncResult BeginExecuteReader()
		{
			return BeginExecuteReader(CommandBehavior.Default);
		}

		public IAsyncResult BeginExecuteReader(CommandBehavior behavior)
		{
			if (caller != null)
			{
				Throw(new MySqlException(Resources.UnableToStartSecondAsyncOp));
			}
			caller = AsyncExecuteWrapper;
			asyncResult = caller.BeginInvoke(1, behavior, null, null);
			return asyncResult;
		}

		public MySqlDataReader EndExecuteReader(IAsyncResult result)
		{
			result.AsyncWaitHandle.WaitOne();
			AsyncDelegate asyncDelegate = caller;
			caller = null;
			if (thrownException != null)
			{
				throw thrownException;
			}
			return (MySqlDataReader)asyncDelegate.EndInvoke(result);
		}

		public IAsyncResult BeginExecuteNonQuery(AsyncCallback callback, object stateObject)
		{
			if (caller != null)
			{
				Throw(new MySqlException(Resources.UnableToStartSecondAsyncOp));
			}
			caller = AsyncExecuteWrapper;
			asyncResult = caller.BeginInvoke(2, CommandBehavior.Default, callback, stateObject);
			return asyncResult;
		}

		public IAsyncResult BeginExecuteNonQuery()
		{
			if (caller != null)
			{
				Throw(new MySqlException(Resources.UnableToStartSecondAsyncOp));
			}
			caller = AsyncExecuteWrapper;
			asyncResult = caller.BeginInvoke(2, CommandBehavior.Default, null, null);
			return asyncResult;
		}

		public int EndExecuteNonQuery(IAsyncResult asyncResult)
		{
			asyncResult.AsyncWaitHandle.WaitOne();
			AsyncDelegate asyncDelegate = caller;
			caller = null;
			if (thrownException != null)
			{
				throw thrownException;
			}
			return (int)asyncDelegate.EndInvoke(asyncResult);
		}

		internal long EstimatedSize()
		{
			long num = CommandText.Length;
			foreach (MySqlParameter parameter in Parameters)
			{
				num += parameter.EstimatedSize();
			}
			return num;
		}

		private bool AddCallStatement(string query)
		{
			string pattern = "^|COMMIT|ROLLBACK|BEGIN|END|DO\\S+|SELECT\\S+[FROM|\\S+]|USE?\\S+|SET\\S+";
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			return regex.Matches(query).Count <= 0;
		}

		public MySqlCommand Clone()
		{
			MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection, curTransaction);
			mySqlCommand.CommandType = CommandType;
			mySqlCommand.commandTimeout = commandTimeout;
			mySqlCommand.useDefaultTimeout = useDefaultTimeout;
			mySqlCommand.batchableCommandText = batchableCommandText;
			mySqlCommand.EnableCaching = EnableCaching;
			mySqlCommand.CacheAge = CacheAge;
			PartialClone(mySqlCommand);
			foreach (MySqlParameter parameter in parameters)
			{
				mySqlCommand.Parameters.Add(parameter.Clone());
			}
			return mySqlCommand;
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		internal void AddToBatch(MySqlCommand command)
		{
			if (batch == null)
			{
				batch = new List<MySqlCommand>();
			}
			batch.Add(command);
		}

		internal string GetCommandTextForBatching()
		{
			if (batchableCommandText == null)
			{
				if (string.Compare(CommandText.Substring(0, 6), "INSERT", StringComparison.OrdinalIgnoreCase) == 0)
				{
					MySqlCommand mySqlCommand = new MySqlCommand("SELECT @@sql_mode", Connection);
					string text = StringUtility.ToUpperInvariant(mySqlCommand.ExecuteScalar().ToString());
					MySqlTokenizer mySqlTokenizer = new MySqlTokenizer(CommandText);
					mySqlTokenizer.AnsiQuotes = text.IndexOf("ANSI_QUOTES") != -1;
					mySqlTokenizer.BackslashEscapes = text.IndexOf("NO_BACKSLASH_ESCAPES") == -1;
					for (string text2 = StringUtility.ToLowerInvariant(mySqlTokenizer.NextToken()); text2 != null; text2 = mySqlTokenizer.NextToken())
					{
						if (StringUtility.ToUpperInvariant(text2) == "VALUES" && !mySqlTokenizer.Quoted)
						{
							text2 = mySqlTokenizer.NextToken();
							int num = 1;
							while (text2 != null)
							{
								batchableCommandText += text2;
								text2 = mySqlTokenizer.NextToken();
								if (text2 == "(")
								{
									num++;
								}
								else if (text2 == ")")
								{
									num--;
								}
								if (num == 0)
								{
									break;
								}
							}
							if (text2 != null)
							{
								batchableCommandText += text2;
							}
							text2 = mySqlTokenizer.NextToken();
							if (text2 != null && (text2 == "," || StringUtility.ToUpperInvariant(text2) == "ON"))
							{
								batchableCommandText = null;
								break;
							}
						}
					}
				}
				else
				{
					batchableCommandText = CommandText;
				}
			}
			return batchableCommandText;
		}

		private void Throw(Exception ex)
		{
			if (connection != null)
			{
				connection.Throw(ex);
			}
			throw ex;
		}

		public new void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected override void Dispose(bool disposing)
		{
			if (statement != null && statement.IsPrepared)
			{
				statement.CloseStatement();
			}
			base.Dispose(disposing);
		}

		private void Constructor()
		{
			UpdatedRowSource = UpdateRowSource.Both;
		}

		private void PartialClone(MySqlCommand clone)
		{
			clone.UpdatedRowSource = UpdatedRowSource;
		}

		protected override DbParameter CreateDbParameter()
		{
			return new MySqlParameter();
		}

		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return ExecuteReader(behavior);
		}
	}
	[ToolboxItem(false)]
	[DesignerCategory("Code")]
	public sealed class MySqlCommandBuilder : DbCommandBuilder
	{
		public new MySqlDataAdapter DataAdapter
		{
			get
			{
				return (MySqlDataAdapter)base.DataAdapter;
			}
			set
			{
				base.DataAdapter = value;
			}
		}

		public MySqlCommandBuilder()
		{
			QuotePrefix = (QuoteSuffix = "`");
		}

		public MySqlCommandBuilder(MySqlDataAdapter adapter)
			: this()
		{
			DataAdapter = adapter;
		}

		public static void DeriveParameters(MySqlCommand command)
		{
			if (command.CommandType != CommandType.StoredProcedure)
			{
				throw new InvalidOperationException(Resources.CanNotDeriveParametersForTextCommands);
			}
			string text = command.CommandText;
			if (text.IndexOf(".") == -1)
			{
				text = command.Connection.Database + "." + text;
			}
			try
			{
				ProcedureCacheEntry procedure = command.Connection.ProcedureCache.GetProcedure(command.Connection, text, null);
				command.Parameters.Clear();
				foreach (MySqlSchemaRow row in procedure.parameters.Rows)
				{
					MySqlParameter mySqlParameter = new MySqlParameter();
					mySqlParameter.ParameterName = string.Format("@{0}", row["PARAMETER_NAME"]);
					if (row["ORDINAL_POSITION"].Equals(0) && mySqlParameter.ParameterName == "@")
					{
						mySqlParameter.ParameterName = "@RETURN_VALUE";
					}
					mySqlParameter.Direction = GetDirection(row);
					bool unsigned = StoredProcedure.GetFlags(row["DTD_IDENTIFIER"].ToString()).IndexOf("UNSIGNED") != -1;
					bool realAsFloat = procedure.procedure.Rows[0]["SQL_MODE"].ToString().IndexOf("REAL_AS_FLOAT") != -1;
					mySqlParameter.MySqlDbType = MetaData.NameToType(row["DATA_TYPE"].ToString(), unsigned, realAsFloat, command.Connection);
					if (row["CHARACTER_MAXIMUM_LENGTH"] != null)
					{
						mySqlParameter.Size = (int)row["CHARACTER_MAXIMUM_LENGTH"];
					}
					if (row["NUMERIC_PRECISION"] != null)
					{
						mySqlParameter.Precision = Convert.ToByte(row["NUMERIC_PRECISION"]);
					}
					if (row["NUMERIC_SCALE"] != null)
					{
						mySqlParameter.Scale = Convert.ToByte(row["NUMERIC_SCALE"]);
					}
					if (mySqlParameter.MySqlDbType == MySqlDbType.Set || mySqlParameter.MySqlDbType == MySqlDbType.Enum)
					{
						mySqlParameter.PossibleValues = GetPossibleValues(row);
					}
					command.Parameters.Add(mySqlParameter);
				}
			}
			catch (InvalidOperationException ex)
			{
				throw new MySqlException(Resources.UnableToDeriveParameters, ex);
			}
		}

		private static List<string> GetPossibleValues(MySqlSchemaRow row)
		{
			string[] array = new string[2] { "ENUM", "SET" };
			string text = row["DTD_IDENTIFIER"].ToString().Trim();
			int i;
			for (i = 0; i < 2 && !text.StartsWith(array[i], StringComparison.OrdinalIgnoreCase); i++)
			{
			}
			if (i == 2)
			{
				return null;
			}
			text = text.Substring(array[i].Length).Trim();
			text = text.Trim('(', ')').Trim();
			List<string> list = new List<string>();
			MySqlTokenizer mySqlTokenizer = new MySqlTokenizer(text);
			string text2 = mySqlTokenizer.NextToken();
			int num = mySqlTokenizer.StartIndex;
			while (true)
			{
				if (text2 == null || text2 == ",")
				{
					int num2 = text.Length - 1;
					if (text2 == ",")
					{
						num2 = mySqlTokenizer.StartIndex;
					}
					string item = text.Substring(num, num2 - num).Trim('\'', '"').Trim();
					list.Add(item);
					num = mySqlTokenizer.StopIndex;
				}
				if (text2 == null)
				{
					break;
				}
				text2 = mySqlTokenizer.NextToken();
			}
			return list;
		}

		private static ParameterDirection GetDirection(MySqlSchemaRow row)
		{
			string text = row["PARAMETER_MODE"].ToString();
			if (Convert.ToInt32(row["ORDINAL_POSITION"]) == 0)
			{
				return ParameterDirection.ReturnValue;
			}
			if (text == "IN")
			{
				return ParameterDirection.Input;
			}
			if (text == "OUT")
			{
				return ParameterDirection.Output;
			}
			return ParameterDirection.InputOutput;
		}

		public new MySqlCommand GetDeleteCommand()
		{
			return (MySqlCommand)base.GetDeleteCommand();
		}

		public new MySqlCommand GetUpdateCommand()
		{
			return (MySqlCommand)base.GetUpdateCommand();
		}

		public new MySqlCommand GetInsertCommand()
		{
			return (MySqlCommand)GetInsertCommand(useColumnsForParameterNames: false);
		}

		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			if (unquotedIdentifier == null)
			{
				throw new ArgumentNullException("unquotedIdentifier");
			}
			if (unquotedIdentifier.StartsWith(QuotePrefix) && unquotedIdentifier.EndsWith(QuoteSuffix))
			{
				return unquotedIdentifier;
			}
			unquotedIdentifier = unquotedIdentifier.Replace(QuotePrefix, QuotePrefix + QuotePrefix);
			return $"{QuotePrefix}{unquotedIdentifier}{QuoteSuffix}";
		}

		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			if (quotedIdentifier == null)
			{
				throw new ArgumentNullException("quotedIdentifier");
			}
			if (!quotedIdentifier.StartsWith(QuotePrefix) || !quotedIdentifier.EndsWith(QuoteSuffix))
			{
				return quotedIdentifier;
			}
			if (quotedIdentifier.StartsWith(QuotePrefix))
			{
				quotedIdentifier = quotedIdentifier.Substring(1);
			}
			if (quotedIdentifier.EndsWith(QuoteSuffix))
			{
				quotedIdentifier = quotedIdentifier.Substring(0, quotedIdentifier.Length - 1);
			}
			quotedIdentifier = quotedIdentifier.Replace(QuotePrefix + QuotePrefix, QuotePrefix);
			return quotedIdentifier;
		}

		protected override DataTable GetSchemaTable(DbCommand sourceCommand)
		{
			DataTable schemaTable = base.GetSchemaTable(sourceCommand);
			foreach (DataRow row in schemaTable.Rows)
			{
				if (row["BaseSchemaName"].Equals(sourceCommand.Connection.Database))
				{
					row["BaseSchemaName"] = null;
				}
			}
			return schemaTable;
		}

		protected override string GetParameterName(string parameterName)
		{
			StringBuilder stringBuilder = new StringBuilder(parameterName);
			stringBuilder.Replace(" ", "");
			stringBuilder.Replace("/", "_per_");
			stringBuilder.Replace("-", "_");
			stringBuilder.Replace(")", "_cb_");
			stringBuilder.Replace("(", "_ob_");
			stringBuilder.Replace("%", "_pct_");
			stringBuilder.Replace("<", "_lt_");
			stringBuilder.Replace(">", "_gt_");
			stringBuilder.Replace(".", "_pt_");
			return $"@{stringBuilder.ToString()}";
		}

		protected override void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause)
		{
			((MySqlParameter)parameter).MySqlDbType = (MySqlDbType)row["ProviderType"];
		}

		protected override string GetParameterName(int parameterOrdinal)
		{
			return $"@p{parameterOrdinal.ToString(CultureInfo.InvariantCulture)}";
		}

		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return $"@p{parameterOrdinal.ToString(CultureInfo.InvariantCulture)}";
		}

		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			MySqlDataAdapter mySqlDataAdapter = adapter as MySqlDataAdapter;
			if (adapter != base.DataAdapter)
			{
				mySqlDataAdapter.RowUpdating += RowUpdating;
			}
			else
			{
				mySqlDataAdapter.RowUpdating -= RowUpdating;
			}
		}

		private void RowUpdating(object sender, MySqlRowUpdatingEventArgs args)
		{
			RowUpdatingHandler(args);
		}
	}
}
namespace MySql.Data.Common
{
	internal class Cache<KeyType, ValueType>
	{
		private int _capacity;

		private Queue<KeyType> _keyQ;

		private Dictionary<KeyType, ValueType> _contents;

		public ValueType this[KeyType key]
		{
			get
			{
				if (_contents.TryGetValue(key, out var value))
				{
					return value;
				}
				return default(ValueType);
			}
			set
			{
				InternalAdd(key, value);
			}
		}

		public Cache(int initialCapacity, int capacity)
		{
			_capacity = capacity;
			_contents = new Dictionary<KeyType, ValueType>(initialCapacity);
			if (capacity > 0)
			{
				_keyQ = new Queue<KeyType>(initialCapacity);
			}
		}

		public void Add(KeyType key, ValueType value)
		{
			InternalAdd(key, value);
		}

		private void InternalAdd(KeyType key, ValueType value)
		{
			if (!_contents.ContainsKey(key) && _capacity > 0)
			{
				_keyQ.Enqueue(key);
				if (_keyQ.Count > _capacity)
				{
					_contents.Remove(_keyQ.Dequeue());
				}
			}
			_contents[key] = value;
		}
	}
	internal class ContextString
	{
		private string contextMarkers;

		private bool escapeBackslash;

		public string ContextMarkers
		{
			get
			{
				return contextMarkers;
			}
			set
			{
				contextMarkers = value;
			}
		}

		public ContextString(string contextMarkers, bool escapeBackslash)
		{
			this.contextMarkers = contextMarkers;
			this.escapeBackslash = escapeBackslash;
		}

		public int IndexOf(string src, string target)
		{
			return IndexOf(src, target, 0);
		}

		public int IndexOf(string src, string target, int startIndex)
		{
			int num = src.IndexOf(target, startIndex);
			while (num != -1 && IndexInQuotes(src, num, startIndex))
			{
				num = src.IndexOf(target, num + 1);
			}
			return num;
		}

		private bool IndexInQuotes(string src, int index, int startIndex)
		{
			char c = '\0';
			bool flag = false;
			for (int i = startIndex; i < index; i++)
			{
				char c2 = src[i];
				int num = contextMarkers.IndexOf(c2);
				if (num > -1 && c == contextMarkers[num] && !flag)
				{
					c = '\0';
				}
				else if (c == '\0' && num > -1 && !flag)
				{
					c = c2;
				}
				else if (c2 == '\\' && escapeBackslash)
				{
					flag = !flag;
				}
			}
			if (c == '\0')
			{
				return flag;
			}
			return true;
		}

		public int IndexOf(string src, char target)
		{
			char c = '\0';
			bool flag = false;
			int num = 0;
			foreach (char c2 in src)
			{
				int num2 = contextMarkers.IndexOf(c2);
				if (num2 > -1 && c == contextMarkers[num2] && !flag)
				{
					c = '\0';
				}
				else if (c == '\0' && num2 > -1 && !flag)
				{
					c = c2;
				}
				else
				{
					if (c == '\0' && c2 == target)
					{
						return num;
					}
					if (c2 == '\\' && escapeBackslash)
					{
						flag = !flag;
					}
				}
				num++;
			}
			return -1;
		}

		public string[] Split(string src, string delimiters)
		{
			ArrayList arrayList = new ArrayList();
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			char c = '\0';
			foreach (char c2 in src)
			{
				if (delimiters.IndexOf(c2) != -1 && !flag)
				{
					if (c != 0)
					{
						stringBuilder.Append(c2);
					}
					else if (stringBuilder.Length > 0)
					{
						arrayList.Add(stringBuilder.ToString());
						stringBuilder.Remove(0, stringBuilder.Length);
					}
					continue;
				}
				if (c2 == '\\' && escapeBackslash)
				{
					flag = !flag;
					continue;
				}
				int num = contextMarkers.IndexOf(c2);
				if (!flag && num != -1)
				{
					if (num % 2 == 1)
					{
						if (c == contextMarkers[num - 1])
						{
							c = '\0';
						}
					}
					else if (c == contextMarkers[num + 1])
					{
						c = '\0';
					}
					else if (c == '\0')
					{
						c = c2;
					}
				}
				stringBuilder.Append(c2);
			}
			if (stringBuilder.Length > 0)
			{
				arrayList.Add(stringBuilder.ToString());
			}
			return (string[])arrayList.ToArray(typeof(string));
		}
	}
	internal class LowResolutionStopwatch
	{
		private long millis;

		private long startTime;

		public static readonly long Frequency = 1000L;

		public static readonly bool isHighResolution = false;

		public long ElapsedMilliseconds => millis;

		public TimeSpan Elapsed => new TimeSpan(0, 0, 0, 0, (int)millis);

		public LowResolutionStopwatch()
		{
			millis = 0L;
		}

		public void Start()
		{
			startTime = Environment.TickCount;
		}

		public void Stop()
		{
			long num = Environment.TickCount;
			long num2 = ((num < startTime) ? (int.MaxValue - startTime + num) : (num - startTime));
			millis += num2;
		}

		public void Reset()
		{
			millis = 0L;
			startTime = 0L;
		}

		public static LowResolutionStopwatch StartNew()
		{
			LowResolutionStopwatch lowResolutionStopwatch = new LowResolutionStopwatch();
			lowResolutionStopwatch.Start();
			return lowResolutionStopwatch;
		}

		public static long GetTimestamp()
		{
			return Environment.TickCount;
		}

		private bool IsRunning()
		{
			return startTime != 0;
		}
	}
	internal class MyNetworkStream : NetworkStream
	{
		private const int MaxRetryCount = 2;

		private Socket socket;

		public MyNetworkStream(Socket socket, bool ownsSocket)
			: base(socket, ownsSocket)
		{
			this.socket = socket;
		}

		private bool IsTimeoutException(SocketException e)
		{
			return e.SocketErrorCode == SocketError.TimedOut;
		}

		private bool IsWouldBlockException(SocketException e)
		{
			return e.SocketErrorCode == SocketError.WouldBlock;
		}

		private void HandleOrRethrowException(Exception e)
		{
			for (Exception ex = e; ex != null; ex = ex.InnerException)
			{
				if (ex is SocketException)
				{
					SocketException e2 = (SocketException)ex;
					if (IsWouldBlockException(e2))
					{
						socket.Blocking = true;
						return;
					}
					if (IsTimeoutException(e2))
					{
						return;
					}
				}
			}
			throw e;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			Exception ex = null;
			do
			{
				try
				{
					return base.Read(buffer, offset, count);
				}
				catch (Exception ex2)
				{
					ex = ex2;
					HandleOrRethrowException(ex2);
				}
			}
			while (++num < 2);
			if (ex.GetBaseException() is SocketException && IsTimeoutException((SocketException)ex.GetBaseException()))
			{
				throw new TimeoutException(ex.Message, ex);
			}
			throw ex;
		}

		public override int ReadByte()
		{
			int num = 0;
			Exception ex = null;
			do
			{
				try
				{
					return ((Stream)this).ReadByte();
				}
				catch (Exception ex2)
				{
					ex = ex2;
					HandleOrRethrowException(ex2);
				}
			}
			while (++num < 2);
			throw ex;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			int num = 0;
			Exception ex = null;
			do
			{
				try
				{
					base.Write(buffer, offset, count);
					return;
				}
				catch (Exception ex2)
				{
					ex = ex2;
					HandleOrRethrowException(ex2);
				}
			}
			while (++num < 2);
			throw ex;
		}

		public override void Flush()
		{
			int num = 0;
			Exception ex = null;
			do
			{
				try
				{
					base.Flush();
					return;
				}
				catch (Exception ex2)
				{
					ex = ex2;
					HandleOrRethrowException(ex2);
				}
			}
			while (++num < 2);
			throw ex;
		}

		public static MyNetworkStream CreateStream(MySqlConnectionStringBuilder settings, bool unix)
		{
			MyNetworkStream myNetworkStream = null;
			IPHostEntry hostEntry = GetHostEntry(settings.Server);
			IPAddress[] addressList = hostEntry.AddressList;
			foreach (IPAddress ip in addressList)
			{
				try
				{
					myNetworkStream = CreateSocketStream(settings, ip, unix);
					if (myNetworkStream != null)
					{
						break;
					}
				}
				catch (Exception ex)
				{
					if (!(ex is SocketException ex2))
					{
						throw;
					}
					if (ex2.SocketErrorCode != SocketError.ConnectionRefused)
					{
						throw;
					}
				}
			}
			return myNetworkStream;
		}

		private static IPHostEntry ParseIPAddress(string hostname)
		{
			IPHostEntry iPHostEntry = null;
			if (IPAddress.TryParse(hostname, out var address))
			{
				iPHostEntry = new IPHostEntry();
				iPHostEntry.AddressList = new IPAddress[1];
				iPHostEntry.AddressList[0] = address;
			}
			return iPHostEntry;
		}

		private static IPHostEntry GetHostEntry(string hostname)
		{
			IPHostEntry iPHostEntry = ParseIPAddress(hostname);
			if (iPHostEntry != null)
			{
				return iPHostEntry;
			}
			return Dns.GetHostEntry(hostname);
		}

		private static EndPoint CreateUnixEndPoint(string host)
		{
			Assembly assembly = Assembly.Load("Mono.Posix, Version=2.0.0.0, \t\t\t\t\r\n                Culture=neutral, PublicKeyToken=0738eb9f132ed756");
			return (EndPoint)assembly.CreateInstance("Mono.Posix.UnixEndPoint", ignoreCase: false, BindingFlags.CreateInstance, null, new object[1] { host }, null, null);
		}

		private static MyNetworkStream CreateSocketStream(MySqlConnectionStringBuilder settings, IPAddress ip, bool unix)
		{
			EndPoint remoteEP = ((Platform.IsWindows() || !unix) ? new IPEndPoint(ip, (int)settings.Port) : CreateUnixEndPoint(settings.Server));
			Socket socket = (unix ? new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP) : new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp));
			if (settings.Keepalive != 0)
			{
				SetKeepAlive(socket, settings.Keepalive);
			}
			IAsyncResult asyncResult = socket.BeginConnect(remoteEP, null, null);
			if (!asyncResult.AsyncWaitHandle.WaitOne((int)(settings.ConnectionTimeout * 1000), exitContext: false))
			{
				socket.Close();
				return null;
			}
			try
			{
				socket.EndConnect(asyncResult);
			}
			catch (Exception)
			{
				socket.Close();
				throw;
			}
			MyNetworkStream myNetworkStream = new MyNetworkStream(socket, ownsSocket: true);
			GC.SuppressFinalize(socket);
			GC.SuppressFinalize(myNetworkStream);
			return myNetworkStream;
		}

		private static void SetKeepAlive(Socket s, uint time)
		{
			uint value = 1u;
			uint value2 = 1000u;
			uint value3 = ((time <= 4294967) ? (time * 1000) : uint.MaxValue);
			byte[] array = new byte[12];
			BitConverter.GetBytes(value).CopyTo(array, 0);
			BitConverter.GetBytes(value3).CopyTo(array, 4);
			BitConverter.GetBytes(value2).CopyTo(array, 8);
			try
			{
				s.IOControl(IOControlCode.KeepAliveValues, array, null);
				return;
			}
			catch (NotImplementedException)
			{
			}
			s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 1);
		}
	}
}
namespace MySql.Data.MySqlClient
{
	internal class MySqlTokenizer
	{
		private string sql;

		private int startIndex;

		private int stopIndex;

		private bool ansiQuotes;

		private bool backslashEscapes;

		private bool returnComments;

		private bool multiLine;

		private bool sqlServerMode;

		private bool quoted;

		private bool isComment;

		private int pos;

		public string Text
		{
			get
			{
				return sql;
			}
			set
			{
				sql = value;
				pos = 0;
			}
		}

		public bool AnsiQuotes
		{
			get
			{
				return ansiQuotes;
			}
			set
			{
				ansiQuotes = value;
			}
		}

		public bool BackslashEscapes
		{
			get
			{
				return backslashEscapes;
			}
			set
			{
				backslashEscapes = value;
			}
		}

		public bool MultiLine
		{
			get
			{
				return multiLine;
			}
			set
			{
				multiLine = value;
			}
		}

		public bool SqlServerMode
		{
			get
			{
				return sqlServerMode;
			}
			set
			{
				sqlServerMode = value;
			}
		}

		public bool Quoted
		{
			get
			{
				return quoted;
			}
			private set
			{
				quoted = value;
			}
		}

		public bool IsComment => isComment;

		public int StartIndex
		{
			get
			{
				return startIndex;
			}
			set
			{
				startIndex = value;
			}
		}

		public int StopIndex
		{
			get
			{
				return stopIndex;
			}
			set
			{
				stopIndex = value;
			}
		}

		public int Position
		{
			get
			{
				return pos;
			}
			set
			{
				pos = value;
			}
		}

		public bool ReturnComments
		{
			get
			{
				return returnComments;
			}
			set
			{
				returnComments = value;
			}
		}

		public MySqlTokenizer()
		{
			backslashEscapes = true;
			multiLine = true;
			pos = 0;
		}

		public MySqlTokenizer(string input)
			: this()
		{
			sql = input;
		}

		public List<string> GetAllTokens()
		{
			List<string> list = new List<string>();
			for (string text = NextToken(); text != null; text = NextToken())
			{
				list.Add(text);
			}
			return list;
		}

		public string NextToken()
		{
			if (FindToken())
			{
				return sql.Substring(startIndex, stopIndex - startIndex);
			}
			return null;
		}

		public static bool IsParameter(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return false;
			}
			if (s[0] == '?')
			{
				return true;
			}
			if (s.Length > 1 && s[0] == '@' && s[1] != '@')
			{
				return true;
			}
			return false;
		}

		public string NextParameter()
		{
			while (FindToken())
			{
				if (stopIndex - startIndex >= 2)
				{
					char c = sql[startIndex];
					char c2 = sql[startIndex + 1];
					if (c == '?' || (c == '@' && c2 != '@'))
					{
						return sql.Substring(startIndex, stopIndex - startIndex);
					}
				}
			}
			return null;
		}

		public bool FindToken()
		{
			isComment = (quoted = false);
			startIndex = (stopIndex = -1);
			while (pos < sql.Length)
			{
				char c = sql[pos++];
				if (char.IsWhiteSpace(c))
				{
					continue;
				}
				if (c == '`' || c == '\'' || c == '"' || (c == '[' && SqlServerMode))
				{
					ReadQuotedToken(c);
				}
				else if (c == '#' || c == '-' || c == '/')
				{
					if (!ReadComment(c))
					{
						ReadSpecialToken();
					}
				}
				else
				{
					ReadUnquotedToken();
				}
				if (startIndex != -1)
				{
					return true;
				}
			}
			return false;
		}

		public string ReadParenthesis()
		{
			StringBuilder stringBuilder = new StringBuilder("(");
			_ = StartIndex;
			string text = NextToken();
			while (true)
			{
				if (text == null)
				{
					throw new InvalidOperationException("Unable to parse SQL");
				}
				stringBuilder.Append(text);
				if (text == ")" && !Quoted)
				{
					break;
				}
				text = NextToken();
			}
			return stringBuilder.ToString();
		}

		private bool ReadComment(char c)
		{
			if (c == '/' && (pos >= sql.Length || sql[pos] != '*'))
			{
				return false;
			}
			if (c == '-' && (pos + 1 >= sql.Length || sql[pos] != '-' || sql[pos + 1] != ' '))
			{
				return false;
			}
			string text = "\n";
			if (sql[pos] == '*')
			{
				text = "*/";
			}
			int num = pos - 1;
			int num2 = sql.IndexOf(text, pos);
			if (text == "\n")
			{
				num2 = sql.IndexOf('\n', pos);
			}
			num2 = (pos = ((num2 != -1) ? (num2 + text.Length) : (sql.Length - 1)));
			if (ReturnComments)
			{
				startIndex = num;
				stopIndex = num2;
				isComment = true;
			}
			return true;
		}

		private void CalculatePosition(int start, int stop)
		{
			startIndex = start;
			stopIndex = stop;
			_ = MultiLine;
		}

		private void ReadUnquotedToken()
		{
			startIndex = pos - 1;
			if (!IsSpecialCharacter(sql[startIndex]))
			{
				while (pos < sql.Length)
				{
					char c = sql[pos];
					if (char.IsWhiteSpace(c) || IsSpecialCharacter(c))
					{
						break;
					}
					pos++;
				}
			}
			Quoted = false;
			stopIndex = pos;
		}

		private void ReadSpecialToken()
		{
			startIndex = pos - 1;
			stopIndex = pos;
			Quoted = false;
		}

		private void ReadQuotedToken(char quoteChar)
		{
			if (quoteChar == '[')
			{
				quoteChar = ']';
			}
			startIndex = pos - 1;
			bool flag = false;
			bool flag2 = false;
			while (pos < sql.Length)
			{
				char c = sql[pos];
				if (c == quoteChar && !flag)
				{
					flag2 = true;
					break;
				}
				if (flag)
				{
					flag = false;
				}
				else if (c == '\\' && BackslashEscapes)
				{
					flag = true;
				}
				pos++;
			}
			if (flag2)
			{
				pos++;
			}
			Quoted = flag2;
			stopIndex = pos;
		}

		private bool IsQuoteChar(char c)
		{
			if (c != '`' && c != '\'')
			{
				return c == '"';
			}
			return true;
		}

		private bool IsParameterMarker(char c)
		{
			if (c != '@')
			{
				return c == '?';
			}
			return true;
		}

		private bool IsSpecialCharacter(char c)
		{
			if (char.IsLetterOrDigit(c) || c == '$' || c == '_' || c == '.')
			{
				return false;
			}
			if (IsParameterMarker(c))
			{
				return false;
			}
			return true;
		}
	}
}
namespace MySql.Data.Common
{
	[SuppressUnmanagedCodeSecurity]
	internal class NamedPipeStream : Stream
	{
		private const int ERROR_PIPE_BUSY = 231;

		private const int ERROR_SEM_TIMEOUT = 121;

		private SafeFileHandle handle;

		private Stream fileStream;

		private int readTimeout = -1;

		private int writeTimeout = -1;

		public override bool CanRead => fileStream.CanRead;

		public override bool CanWrite => fileStream.CanWrite;

		public override bool CanSeek
		{
			get
			{
				throw new NotSupportedException(Resources.NamedPipeNoSeek);
			}
		}

		public override long Length
		{
			get
			{
				throw new NotSupportedException(Resources.NamedPipeNoSeek);
			}
		}

		public override long Position
		{
			get
			{
				throw new NotSupportedException(Resources.NamedPipeNoSeek);
			}
			set
			{
			}
		}

		public override bool CanTimeout => true;

		public override int ReadTimeout
		{
			get
			{
				return readTimeout;
			}
			set
			{
				readTimeout = value;
			}
		}

		public override int WriteTimeout
		{
			get
			{
				return writeTimeout;
			}
			set
			{
				writeTimeout = value;
			}
		}

		public NamedPipeStream(string path, FileAccess mode, uint timeout)
		{
			Open(path, mode, timeout);
		}

		private void CancelIo()
		{
			if (!NativeMethods.CancelIo(handle.DangerousGetHandle()))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		public void Open(string path, FileAccess mode, uint timeout)
		{
			IntPtr intPtr;
			while (true)
			{
				NativeMethods.SecurityAttributes securityAttributes = new NativeMethods.SecurityAttributes();
				securityAttributes.inheritHandle = true;
				securityAttributes.Length = Marshal.SizeOf((object)securityAttributes);
				intPtr = NativeMethods.CreateFile(path, 3221225472u, 0u, securityAttributes, 3u, 1073741824u, 0u);
				if (intPtr != IntPtr.Zero)
				{
					break;
				}
				if (Marshal.GetLastWin32Error() != 231)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error(), "Error opening pipe");
				}
				LowResolutionStopwatch lowResolutionStopwatch = LowResolutionStopwatch.StartNew();
				bool flag = NativeMethods.WaitNamedPipe(path, timeout);
				lowResolutionStopwatch.Stop();
				if (!flag)
				{
					if (timeout < lowResolutionStopwatch.ElapsedMilliseconds || Marshal.GetLastWin32Error() == 121)
					{
						throw new TimeoutException("Timeout waiting for named pipe");
					}
					throw new Win32Exception(Marshal.GetLastWin32Error(), "Error waiting for pipe");
				}
				timeout -= (uint)(int)lowResolutionStopwatch.ElapsedMilliseconds;
			}
			handle = new SafeFileHandle(intPtr, ownsHandle: true);
			fileStream = new FileStream(handle, mode, 4096, isAsync: true);
		}

		public override void Flush()
		{
			fileStream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (readTimeout == -1)
			{
				return fileStream.Read(buffer, offset, count);
			}
			IAsyncResult asyncResult = fileStream.BeginRead(buffer, offset, count, null, null);
			if (asyncResult.CompletedSynchronously)
			{
				return fileStream.EndRead(asyncResult);
			}
			if (!asyncResult.AsyncWaitHandle.WaitOne(readTimeout))
			{
				CancelIo();
				throw new TimeoutException("Timeout in named pipe read");
			}
			return fileStream.EndRead(asyncResult);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (writeTimeout == -1)
			{
				fileStream.Write(buffer, offset, count);
				return;
			}
			IAsyncResult asyncResult = fileStream.BeginWrite(buffer, offset, count, null, null);
			if (asyncResult.CompletedSynchronously)
			{
				fileStream.EndWrite(asyncResult);
			}
			if (!asyncResult.AsyncWaitHandle.WaitOne(readTimeout))
			{
				CancelIo();
				throw new TimeoutException("Timeout in named pipe write");
			}
			fileStream.EndWrite(asyncResult);
		}

		public override void Close()
		{
			if (handle != null && !handle.IsInvalid && !handle.IsClosed)
			{
				fileStream.Close();
				try
				{
					handle.Close();
				}
				catch (Exception)
				{
				}
			}
		}

		public override void SetLength(long length)
		{
			throw new NotSupportedException(Resources.NamedPipeNoSetLength);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(Resources.NamedPipeNoSeek);
		}

		internal static Stream Create(string pipeName, string hostname, uint timeout)
		{
			string path = ((string.Compare(hostname, "localhost", ignoreCase: true) != 0) ? $"\\\\{hostname}\\pipe\\{pipeName}" : ("\\\\.\\pipe\\" + pipeName));
			return new NamedPipeStream(path, FileAccess.ReadWrite, timeout);
		}
	}
	internal class NativeMethods
	{
		[StructLayout(LayoutKind.Sequential)]
		public class SecurityAttributes
		{
			public int Length;

			public IntPtr securityDescriptor = IntPtr.Zero;

			public bool inheritHandle;

			public SecurityAttributes()
			{
				Length = Marshal.SizeOf(typeof(SecurityAttributes));
			}
		}

		public const uint GENERIC_READ = 2147483648u;

		public const uint GENERIC_WRITE = 1073741824u;

		public const int INVALIDpipeHandle_VALUE = -1;

		public const uint FILE_FLAG_OVERLAPPED = 1073741824u;

		public const uint FILE_FLAG_NO_BUFFERING = 536870912u;

		public const uint OPEN_EXISTING = 3u;

		private NativeMethods()
		{
		}

		[DllImport("Kernel32", CharSet = CharSet.Unicode)]
		public static extern IntPtr CreateFile(string fileName, uint desiredAccess, uint shareMode, SecurityAttributes securityAttributes, uint creationDisposition, uint flagsAndAttributes, uint templateFile);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool PeekNamedPipe(IntPtr handle, byte[] buffer, uint nBufferSize, ref uint bytesRead, ref uint bytesAvail, ref uint BytesLeftThisMessage);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ReadFile(IntPtr hFile, [Out] byte[] lpBuffer, uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, IntPtr lpOverlapped);

		[DllImport("Kernel32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool WriteFile(IntPtr hFile, [In] byte[] buffer, uint numberOfBytesToWrite, out uint numberOfBytesWritten, IntPtr lpOverlapped);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseHandle(IntPtr handle);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CancelIo(IntPtr handle);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool FlushFileBuffers(IntPtr handle);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr OpenEvent(uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr OpenFileMapping(uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

		[DllImport("kernel32.dll")]
		public static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, IntPtr dwNumberOfBytesToMap);

		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern int FlushViewOfFile(IntPtr address, uint numBytes);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WaitNamedPipe(string namedPipeName, uint timeOut);

		[DllImport("ws2_32.dll", SetLastError = true)]
		public static extern IntPtr socket(int af, int type, int protocol);

		[DllImport("ws2_32.dll", SetLastError = true)]
		public static extern int ioctlsocket(IntPtr socket, uint cmd, ref uint arg);

		[DllImport("ws2_32.dll", SetLastError = true)]
		public static extern int WSAIoctl(IntPtr s, uint dwIoControlCode, byte[] inBuffer, uint cbInBuffer, byte[] outBuffer, uint cbOutBuffer, IntPtr lpcbBytesReturned, IntPtr lpOverlapped, IntPtr lpCompletionRoutine);

		[DllImport("ws2_32.dll", SetLastError = true)]
		public static extern int WSAGetLastError();

		[DllImport("ws2_32.dll", SetLastError = true)]
		public static extern int connect(IntPtr socket, byte[] addr, int addrlen);

		[DllImport("ws2_32.dll", SetLastError = true)]
		public static extern int recv(IntPtr socket, byte[] buff, int len, int flags);

		[DllImport("ws2_32.Dll", SetLastError = true)]
		public static extern int send(IntPtr socket, byte[] buff, int len, int flags);
	}
	internal class Platform
	{
		private static bool inited;

		private static bool isMono;

		public static char DirectorySeparatorChar => Path.DirectorySeparatorChar;

		private Platform()
		{
		}

		public static bool IsWindows()
		{
			OperatingSystem oSVersion = Environment.OSVersion;
			switch (oSVersion.Platform)
			{
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.Win32NT:
				return true;
			default:
				return false;
			}
		}

		public static bool IsMono()
		{
			if (!inited)
			{
				Init();
			}
			return isMono;
		}

		private static void Init()
		{
			inited = true;
			Type type = Type.GetType("Mono.Runtime");
			isMono = (object)type != null;
		}
	}
	internal class QueryNormalizer
	{
		private static List<string> keywords;

		private List<Token> tokens = new List<Token>();

		private int pos;

		private string fullSql;

		private string queryType;

		public string QueryType => queryType;

		static QueryNormalizer()
		{
			keywords = new List<string>();
			StringReader stringReader = new StringReader(Resources.keywords);
			for (string text = stringReader.ReadLine(); text != null; text = stringReader.ReadLine())
			{
				keywords.Add(text);
			}
		}

		public string Normalize(string sql)
		{
			tokens.Clear();
			StringBuilder stringBuilder = new StringBuilder();
			fullSql = sql;
			TokenizeSql(sql);
			DetermineStatementType(tokens);
			ProcessMathSymbols(tokens);
			CollapseValueLists(tokens);
			CollapseInLists(tokens);
			CollapseWhitespace(tokens);
			foreach (Token token in tokens)
			{
				if (token.Output)
				{
					stringBuilder.Append(token.Text);
				}
			}
			return stringBuilder.ToString();
		}

		private void DetermineStatementType(List<Token> tok)
		{
			foreach (Token item in tok)
			{
				if (item.Type == TokenType.Keyword)
				{
					queryType = item.Text.ToUpperInvariant();
					break;
				}
			}
		}

		private void ProcessMathSymbols(List<Token> tok)
		{
			Token token = null;
			foreach (Token item in tok)
			{
				if (item.Type == TokenType.Symbol && (item.Text == "-" || item.Text == "+") && token != null && token.Type != TokenType.Number && token.Type != TokenType.Identifier && (token.Type != TokenType.Symbol || token.Text != ")"))
				{
					item.Output = false;
				}
				if (item.IsRealToken)
				{
					token = item;
				}
			}
		}

		private void CollapseWhitespace(List<Token> tok)
		{
			Token token = null;
			foreach (Token item in tok)
			{
				if (item.Output && item.Type == TokenType.Whitespace && token != null && token.Type == TokenType.Whitespace)
				{
					item.Output = false;
				}
				if (item.Output)
				{
					token = item;
				}
			}
		}

		private void CollapseValueLists(List<Token> tok)
		{
			int num = -1;
			while (++num < tok.Count)
			{
				Token token = tok[num];
				if (token.Type == TokenType.Keyword && token.Text.StartsWith("VALUE", StringComparison.OrdinalIgnoreCase))
				{
					CollapseValueList(tok, ref num);
				}
			}
		}

		private void CollapseValueList(List<Token> tok, ref int pos)
		{
			List<int> list = new List<int>();
			while (true)
			{
				if (++pos >= tok.Count || (tok[pos].Type == TokenType.Symbol && tok[pos].Text == ")") || pos == tok.Count - 1)
				{
					list.Add(pos);
					while (++pos < tok.Count && !tok[pos].IsRealToken)
					{
					}
					if (pos == tok.Count)
					{
						break;
					}
					if (tok[pos].Text != ",")
					{
						pos--;
						break;
					}
				}
			}
			if (list.Count >= 2)
			{
				int num = list[0];
				tok[++num] = new Token(TokenType.Whitespace, " ");
				tok[++num] = new Token(TokenType.Comment, "/* , ... */");
				num++;
				while (num <= list[list.Count - 1])
				{
					tok[num++].Output = false;
				}
			}
		}

		private void CollapseInLists(List<Token> tok)
		{
			int num = -1;
			while (++num < tok.Count)
			{
				Token token = tok[num];
				if (token.Type == TokenType.Keyword && token.Text == "IN")
				{
					CollapseInList(tok, ref num);
				}
			}
		}

		private Token GetNextRealToken(List<Token> tok, ref int pos)
		{
			while (++pos < tok.Count)
			{
				if (tok[pos].IsRealToken)
				{
					return tok[pos];
				}
			}
			return null;
		}

		private void CollapseInList(List<Token> tok, ref int pos)
		{
			Token nextRealToken = GetNextRealToken(tok, ref pos);
			if (nextRealToken == null)
			{
				return;
			}
			nextRealToken = GetNextRealToken(tok, ref pos);
			if (nextRealToken == null || nextRealToken.Type == TokenType.Keyword)
			{
				return;
			}
			int num = pos;
			while (++pos < tok.Count)
			{
				nextRealToken = tok[pos];
				if (nextRealToken.Type == TokenType.CommandComment)
				{
					return;
				}
				if (nextRealToken.IsRealToken)
				{
					if (nextRealToken.Text == "(")
					{
						return;
					}
					if (nextRealToken.Text == ")")
					{
						break;
					}
				}
			}
			int num2 = pos;
			for (int num3 = num2; num3 > num; num3--)
			{
				tok.RemoveAt(num3);
			}
			tok.Insert(++num, new Token(TokenType.Whitespace, " "));
			tok.Insert(++num, new Token(TokenType.Comment, "/* , ... */"));
			tok.Insert(++num, new Token(TokenType.Whitespace, " "));
			tok.Insert(++num, new Token(TokenType.Symbol, ")"));
		}

		private void TokenizeSql(string sql)
		{
			pos = 0;
			while (pos < sql.Length)
			{
				char c = sql[pos];
				if (!LetterStartsComment(c) || !ConsumeComment())
				{
					if (char.IsWhiteSpace(c))
					{
						ConsumeWhitespace();
					}
					else if (c == '\'' || c == '"' || c == '`')
					{
						ConsumeQuotedToken(c);
					}
					else if (!IsSpecialCharacter(c))
					{
						ConsumeUnquotedToken();
					}
					else
					{
						ConsumeSymbol();
					}
				}
			}
		}

		private bool LetterStartsComment(char c)
		{
			if (c != '#' && c != '/')
			{
				return c == '-';
			}
			return true;
		}

		private bool IsSpecialCharacter(char c)
		{
			if (char.IsLetterOrDigit(c) || c == '$' || c == '_' || c == '.')
			{
				return false;
			}
			return true;
		}

		private bool ConsumeComment()
		{
			char c = fullSql[pos];
			if (c == '/' && (pos + 1 >= fullSql.Length || fullSql[pos + 1] != '*'))
			{
				return false;
			}
			if (c == '-' && (pos + 2 >= fullSql.Length || fullSql[pos + 1] != '-' || fullSql[pos + 2] != ' '))
			{
				return false;
			}
			string text = "\n";
			if (c == '/')
			{
				text = "*/";
			}
			int num = fullSql.IndexOf(text, pos);
			num = ((num != -1) ? (num + text.Length) : (fullSql.Length - 1));
			string text2 = fullSql.Substring(pos, num - pos);
			if (text2.StartsWith("/*!", StringComparison.Ordinal))
			{
				tokens.Add(new Token(TokenType.CommandComment, text2));
			}
			pos = num;
			return true;
		}

		private void ConsumeSymbol()
		{
			char c = fullSql[pos++];
			tokens.Add(new Token(TokenType.Symbol, c.ToString()));
		}

		private void ConsumeQuotedToken(char c)
		{
			bool flag = false;
			int num = pos;
			pos++;
			while (pos < fullSql.Length)
			{
				char c2 = fullSql[pos];
				if (c2 == c && !flag)
				{
					break;
				}
				if (flag)
				{
					flag = false;
				}
				else if (c2 == '\\')
				{
					flag = true;
				}
				pos++;
			}
			pos++;
			if (c == '\'')
			{
				tokens.Add(new Token(TokenType.String, "?"));
			}
			else
			{
				tokens.Add(new Token(TokenType.Identifier, fullSql.Substring(num, pos - num)));
			}
		}

		private void ConsumeUnquotedToken()
		{
			int num = pos;
			while (pos < fullSql.Length && !IsSpecialCharacter(fullSql[pos]))
			{
				pos++;
			}
			string text = fullSql.Substring(num, pos - num);
			if (double.TryParse(text, out var _))
			{
				tokens.Add(new Token(TokenType.Number, "?"));
				return;
			}
			Token token = new Token(TokenType.Identifier, text);
			if (IsKeyword(text))
			{
				token.Type = TokenType.Keyword;
				token.Text = token.Text.ToUpperInvariant();
			}
			tokens.Add(token);
		}

		private void ConsumeWhitespace()
		{
			tokens.Add(new Token(TokenType.Whitespace, " "));
			while (pos < fullSql.Length && char.IsWhiteSpace(fullSql[pos]))
			{
				pos++;
			}
		}

		private bool IsKeyword(string word)
		{
			return keywords.Contains(word.ToUpperInvariant());
		}
	}
	internal class Token
	{
		public TokenType Type;

		public string Text;

		public bool Output;

		public bool IsRealToken
		{
			get
			{
				if (Type != TokenType.Comment && Type != TokenType.CommandComment && Type != TokenType.Whitespace)
				{
					return Output;
				}
				return false;
			}
		}

		public Token(TokenType type, string text)
		{
			Type = type;
			Text = text;
			Output = true;
		}
	}
	internal enum TokenType
	{
		Keyword,
		String,
		Number,
		Symbol,
		Identifier,
		Comment,
		CommandComment,
		Whitespace
	}
	internal class SharedMemory : IDisposable
	{
		private const uint FILE_MAP_WRITE = 2u;

		private IntPtr fileMapping;

		private IntPtr view;

		public IntPtr View => view;

		public SharedMemory(string name, IntPtr size)
		{
			fileMapping = NativeMethods.OpenFileMapping(2u, bInheritHandle: false, name);
			if (fileMapping == IntPtr.Zero)
			{
				throw new MySqlException("Cannot open file mapping " + name);
			}
			view = NativeMethods.MapViewOfFile(fileMapping, 2u, 0u, 0u, size);
		}

		~SharedMemory()
		{
			Dispose(disposing: false);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (view != IntPtr.Zero)
				{
					NativeMethods.UnmapViewOfFile(view);
					view = IntPtr.Zero;
				}
				if (fileMapping != IntPtr.Zero)
				{
					NativeMethods.CloseHandle(fileMapping);
					fileMapping = IntPtr.Zero;
				}
			}
		}
	}
	internal class SharedMemoryStream : Stream
	{
		private const int BUFFERLENGTH = 16004;

		private string memoryName;

		private EventWaitHandle serverRead;

		private EventWaitHandle serverWrote;

		private EventWaitHandle clientRead;

		private EventWaitHandle clientWrote;

		private EventWaitHandle connectionClosed;

		private SharedMemory data;

		private int bytesLeft;

		private int position;

		private int connectNumber;

		private int readTimeout = -1;

		private int writeTimeout = -1;

		public override bool CanRead => true;

		public override bool CanSeek => false;

		public override bool CanWrite => true;

		public override long Length
		{
			get
			{
				throw new NotSupportedException("SharedMemoryStream does not support seeking - length");
			}
		}

		public override long Position
		{
			get
			{
				throw new NotSupportedException("SharedMemoryStream does not support seeking - position");
			}
			set
			{
			}
		}

		public override bool CanTimeout => true;

		public override int ReadTimeout
		{
			get
			{
				return readTimeout;
			}
			set
			{
				readTimeout = value;
			}
		}

		public override int WriteTimeout
		{
			get
			{
				return writeTimeout;
			}
			set
			{
				writeTimeout = value;
			}
		}

		public SharedMemoryStream(string memName)
		{
			memoryName = memName;
		}

		public void Open(uint timeOut)
		{
			_ = connectionClosed;
			GetConnectNumber(timeOut);
			SetupEvents();
		}

		public override void Close()
		{
			if (connectionClosed == null)
			{
				return;
			}
			if (!connectionClosed.WaitOne(0))
			{
				connectionClosed.Set();
				connectionClosed.Close();
			}
			connectionClosed = null;
			EventWaitHandle[] array = new EventWaitHandle[4] { serverRead, serverWrote, clientRead, clientWrote };
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					array[i].Close();
				}
			}
			if (data != null)
			{
				data.Dispose();
				data = null;
			}
		}

		private void GetConnectNumber(uint timeOut)
		{
			EventWaitHandle eventWaitHandle;
			try
			{
				eventWaitHandle = EventWaitHandle.OpenExisting(memoryName + "_CONNECT_REQUEST");
			}
			catch (Exception)
			{
				string text = "Global\\" + memoryName;
				eventWaitHandle = EventWaitHandle.OpenExisting(text + "_CONNECT_REQUEST");
				memoryName = text;
			}
			EventWaitHandle eventWaitHandle2 = EventWaitHandle.OpenExisting(memoryName + "_CONNECT_ANSWER");
			using SharedMemory sharedMemory = new SharedMemory(memoryName + "_CONNECT_DATA", (IntPtr)4);
			if (!eventWaitHandle.Set())
			{
				throw new MySqlException("Failed to open shared memory connection");
			}
			if (!eventWaitHandle2.WaitOne((int)(timeOut * 1000), exitContext: false))
			{
				throw new MySqlException("Timeout during connection");
			}
			connectNumber = Marshal.ReadInt32(sharedMemory.View);
		}

		private void SetupEvents()
		{
			string text = memoryName + "_" + connectNumber;
			data = new SharedMemory(text + "_DATA", (IntPtr)16004);
			serverWrote = EventWaitHandle.OpenExisting(text + "_SERVER_WROTE");
			serverRead = EventWaitHandle.OpenExisting(text + "_SERVER_READ");
			clientWrote = EventWaitHandle.OpenExisting(text + "_CLIENT_WROTE");
			clientRead = EventWaitHandle.OpenExisting(text + "_CLIENT_READ");
			connectionClosed = EventWaitHandle.OpenExisting(text + "_CONNECTION_CLOSED");
			serverRead.Set();
		}

		public override void Flush()
		{
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = readTimeout;
			WaitHandle[] array = new WaitHandle[2] { serverWrote, connectionClosed };
			LowResolutionStopwatch lowResolutionStopwatch = new LowResolutionStopwatch();
			while (bytesLeft == 0)
			{
				lowResolutionStopwatch.Start();
				int num2 = WaitHandle.WaitAny(array, num);
				lowResolutionStopwatch.Stop();
				if (num2 == 258)
				{
					throw new TimeoutException("Timeout when reading from shared memory");
				}
				if (array[num2] == connectionClosed)
				{
					throw new MySqlException("Connection to server lost", isFatal: true, null);
				}
				if (readTimeout != -1)
				{
					num = readTimeout - (int)lowResolutionStopwatch.ElapsedMilliseconds;
					if (num < 0)
					{
						throw new TimeoutException("Timeout when reading from shared memory");
					}
				}
				bytesLeft = Marshal.ReadInt32(data.View);
				position = 4;
			}
			int num3 = Math.Min(count, bytesLeft);
			long num4 = data.View.ToInt64() + position;
			int num5 = 0;
			while (num5 < num3)
			{
				buffer[offset + num5] = Marshal.ReadByte((IntPtr)(num4 + num5));
				num5++;
				position++;
			}
			bytesLeft -= num3;
			if (bytesLeft == 0)
			{
				clientRead.Set();
			}
			return num3;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("SharedMemoryStream does not support seeking");
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			int num = count;
			int num2 = offset;
			WaitHandle[] array = new WaitHandle[2] { serverRead, connectionClosed };
			LowResolutionStopwatch lowResolutionStopwatch = new LowResolutionStopwatch();
			int num3 = writeTimeout;
			while (num > 0)
			{
				lowResolutionStopwatch.Start();
				int num4 = WaitHandle.WaitAny(array, num3);
				lowResolutionStopwatch.Stop();
				if (array[num4] == connectionClosed)
				{
					throw new MySqlException("Connection to server lost", isFatal: true, null);
				}
				if (num4 == 258)
				{
					throw new TimeoutException("Timeout when reading from shared memory");
				}
				if (writeTimeout != -1)
				{
					num3 = writeTimeout - (int)lowResolutionStopwatch.ElapsedMilliseconds;
					if (num3 < 0)
					{
						throw new TimeoutException("Timeout when writing to shared memory");
					}
				}
				int num5 = Math.Min(num, 16004);
				long num6 = data.View.ToInt64() + 4;
				Marshal.WriteInt32(data.View, num5);
				Marshal.Copy(buffer, num2, (IntPtr)num6, num5);
				num2 += num5;
				num -= num5;
				if (!clientWrote.Set())
				{
					throw new MySqlException("Writing to shared memory failed");
				}
			}
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException("SharedMemoryStream does not support seeking");
		}
	}
	internal class StreamCreator
	{
		private string hostList;

		private uint port;

		private string pipeName;

		private uint timeOut;

		private uint keepalive;

		private DBVersion driverVersion;

		public StreamCreator(string hosts, uint port, string pipeName, uint keepalive, DBVersion driverVersion)
		{
			hostList = hosts;
			if (hostList == null || hostList.Length == 0)
			{
				hostList = "localhost";
			}
			this.port = port;
			this.pipeName = pipeName;
			this.keepalive = keepalive;
			this.driverVersion = driverVersion;
		}

		public static Stream GetStream(string server, uint port, string pipename, uint keepalive, DBVersion v, uint timeout)
		{
			MySqlConnectionStringBuilder mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder();
			mySqlConnectionStringBuilder.Server = server;
			mySqlConnectionStringBuilder.Port = port;
			mySqlConnectionStringBuilder.PipeName = pipename;
			mySqlConnectionStringBuilder.Keepalive = keepalive;
			mySqlConnectionStringBuilder.ConnectionTimeout = timeout;
			return GetStream(mySqlConnectionStringBuilder);
		}

		public static Stream GetStream(MySqlConnectionStringBuilder settings)
		{
			return settings.ConnectionProtocol switch
			{
				MySqlConnectionProtocol.Sockets => GetTcpStream(settings), 
				MySqlConnectionProtocol.UnixSocket => GetUnixSocketStream(settings), 
				MySqlConnectionProtocol.SharedMemory => GetSharedMemoryStream(settings), 
				MySqlConnectionProtocol.Pipe => GetNamedPipeStream(settings), 
				_ => throw new InvalidOperationException(Resources.UnknownConnectionProtocol), 
			};
		}

		private static Stream GetTcpStream(MySqlConnectionStringBuilder settings)
		{
			return MyNetworkStream.CreateStream(settings, unix: false);
		}

		private static Stream GetUnixSocketStream(MySqlConnectionStringBuilder settings)
		{
			if (Platform.IsWindows())
			{
				throw new InvalidOperationException(Resources.NoUnixSocketsOnWindows);
			}
			return MyNetworkStream.CreateStream(settings, unix: true);
		}

		private static Stream GetSharedMemoryStream(MySqlConnectionStringBuilder settings)
		{
			SharedMemoryStream sharedMemoryStream = new SharedMemoryStream(settings.SharedMemoryName);
			sharedMemoryStream.Open(settings.ConnectionTimeout);
			return sharedMemoryStream;
		}

		private static Stream GetNamedPipeStream(MySqlConnectionStringBuilder settings)
		{
			return NamedPipeStream.Create(settings.PipeName, settings.Server, settings.ConnectionTimeout);
		}
	}
}
namespace MySql.Data.MySqlClient
{
	public class StringUtility
	{
		public static string ToUpperInvariant(string s)
		{
			return s.ToUpperInvariant();
		}

		public static string ToLowerInvariant(string s)
		{
			return s.ToLowerInvariant();
		}
	}
}
namespace MySql.Data.Common
{
	internal struct DBVersion
	{
		private int major;

		private int minor;

		private int build;

		private string srcString;

		public int Major => major;

		public int Minor => minor;

		public int Build => build;

		public DBVersion(string s, int major, int minor, int build)
		{
			this.major = major;
			this.minor = minor;
			this.build = build;
			srcString = s;
		}

		public static DBVersion Parse(string versionString)
		{
			int num = 0;
			int num2 = versionString.IndexOf('.', num);
			if (num2 == -1)
			{
				throw new MySqlException(Resources.BadVersionFormat);
			}
			string value = versionString.Substring(num, num2 - num).Trim();
			int num3 = Convert.ToInt32(value, NumberFormatInfo.InvariantInfo);
			num = num2 + 1;
			num2 = versionString.IndexOf('.', num);
			if (num2 == -1)
			{
				throw new MySqlException(Resources.BadVersionFormat);
			}
			value = versionString.Substring(num, num2 - num).Trim();
			int num4 = Convert.ToInt32(value, NumberFormatInfo.InvariantInfo);
			num = num2 + 1;
			int i;
			for (i = num; i < versionString.Length && char.IsDigit(versionString, i); i++)
			{
			}
			value = versionString.Substring(num, i - num).Trim();
			int num5 = Convert.ToInt32(value, NumberFormatInfo.InvariantInfo);
			return new DBVersion(versionString, num3, num4, num5);
		}

		public bool isAtLeast(int majorNum, int minorNum, int buildNum)
		{
			if (major > majorNum)
			{
				return true;
			}
			if (major == majorNum && minor > minorNum)
			{
				return true;
			}
			if (major == majorNum && minor == minorNum && build >= buildNum)
			{
				return true;
			}
			return false;
		}

		public override string ToString()
		{
			return srcString;
		}
	}
}
namespace MySql.Data.MySqlClient
{
	internal class CompressedStream : Stream
	{
		private Stream baseStream;

		private MemoryStream cache;

		private byte[] localByte;

		private byte[] inBuffer;

		private byte[] lengthBytes;

		private WeakReference inBufferRef;

		private int inPos;

		private int maxInPos;

		private ZInputStream zInStream;

		public override bool CanRead => baseStream.CanRead;

		public override bool CanWrite => baseStream.CanWrite;

		public override bool CanSeek => baseStream.CanSeek;

		public override long Length => baseStream.Length;

		public override long Position
		{
			get
			{
				return baseStream.Position;
			}
			set
			{
				baseStream.Position = value;
			}
		}

		public override bool CanTimeout => baseStream.CanTimeout;

		public override int ReadTimeout
		{
			get
			{
				return baseStream.ReadTimeout;
			}
			set
			{
				baseStream.ReadTimeout = value;
			}
		}

		public override int WriteTimeout
		{
			get
			{
				return baseStream.WriteTimeout;
			}
			set
			{
				baseStream.WriteTimeout = value;
			}
		}

		public CompressedStream(Stream baseStream)
		{
			this.baseStream = baseStream;
			localByte = new byte[1];
			lengthBytes = new byte[7];
			cache = new MemoryStream();
			inBufferRef = new WeakReference(inBuffer, trackResurrection: false);
		}

		public override void Close()
		{
			base.Close();
			baseStream.Close();
			cache.Dispose();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException(Resources.CSNoSetLength);
		}

		public override int ReadByte()
		{
			try
			{
				Read(localByte, 0, 1);
				return localByte[0];
			}
			catch (EndOfStreamException)
			{
				return -1;
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Resources.BufferCannotBeNull);
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset", Resources.OffsetMustBeValid);
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentException(Resources.BufferNotLargeEnough, "buffer");
			}
			if (inPos == maxInPos)
			{
				PrepareNextPacket();
			}
			int num = Math.Min(count, maxInPos - inPos);
			int num2 = ((zInStream == null) ? baseStream.Read(buffer, offset, num) : zInStream.read(buffer, offset, num));
			inPos += num2;
			if (inPos == maxInPos)
			{
				zInStream = null;
				if (!Platform.IsMono())
				{
					inBufferRef = new WeakReference(inBuffer, trackResurrection: false);
					inBuffer = null;
				}
			}
			return num2;
		}

		private void PrepareNextPacket()
		{
			MySqlStream.ReadFully(baseStream, lengthBytes, 0, 7);
			int num = lengthBytes[0] + (lengthBytes[1] << 8) + (lengthBytes[2] << 16);
			int num2 = lengthBytes[4] + (lengthBytes[5] << 8) + (lengthBytes[6] << 16);
			if (num2 == 0)
			{
				num2 = num;
				zInStream = null;
			}
			else
			{
				ReadNextPacket(num);
				MemoryStream in_Renamed = new MemoryStream(inBuffer);
				zInStream = new ZInputStream(in_Renamed);
				zInStream.maxInput = num;
			}
			inPos = 0;
			maxInPos = num2;
		}

		private void ReadNextPacket(int len)
		{
			if (!Platform.IsMono())
			{
				inBuffer = inBufferRef.Target as byte[];
			}
			if (inBuffer == null || inBuffer.Length < len)
			{
				inBuffer = new byte[len];
			}
			MySqlStream.ReadFully(baseStream, inBuffer, 0, len);
		}

		private MemoryStream CompressCache()
		{
			if (cache.Length < 50)
			{
				return null;
			}
			byte[] buffer = cache.GetBuffer();
			MemoryStream memoryStream = new MemoryStream();
			ZOutputStream zOutputStream = new ZOutputStream(memoryStream, -1);
			zOutputStream.Write(buffer, 0, (int)cache.Length);
			zOutputStream.finish();
			if (memoryStream.Length >= cache.Length)
			{
				return null;
			}
			return memoryStream;
		}

		private void CompressAndSendCache()
		{
			byte[] buffer = cache.GetBuffer();
			byte b = buffer[3];
			buffer[3] = 0;
			MemoryStream memoryStream = CompressCache();
			long length;
			long num;
			MemoryStream memoryStream2;
			if (memoryStream == null)
			{
				length = cache.Length;
				num = 0L;
				memoryStream2 = cache;
			}
			else
			{
				length = memoryStream.Length;
				num = cache.Length;
				memoryStream2 = memoryStream;
			}
			long length2 = memoryStream2.Length;
			int num2 = (int)length2 + 7;
			memoryStream2.SetLength(num2);
			byte[] buffer2 = memoryStream2.GetBuffer();
			Array.Copy(buffer2, 0, buffer2, 7, (int)length2);
			buffer2[0] = (byte)(length & 0xFF);
			buffer2[1] = (byte)((length >> 8) & 0xFF);
			buffer2[2] = (byte)((length >> 16) & 0xFF);
			buffer2[3] = b;
			buffer2[4] = (byte)(num & 0xFF);
			buffer2[5] = (byte)((num >> 8) & 0xFF);
			buffer2[6] = (byte)((num >> 16) & 0xFF);
			baseStream.Write(buffer2, 0, num2);
			baseStream.Flush();
			cache.SetLength(0L);
			memoryStream?.Dispose();
		}

		public override void Flush()
		{
			if (InputDone())
			{
				CompressAndSendCache();
			}
		}

		private bool InputDone()
		{
			if (baseStream is TimedStream && ((TimedStream)baseStream).IsClosed)
			{
				return false;
			}
			if (cache.Length < 4)
			{
				return false;
			}
			byte[] buffer = cache.GetBuffer();
			int num = buffer[0] + (buffer[1] << 8) + (buffer[2] << 16);
			if (cache.Length < num + 4)
			{
				return false;
			}
			return true;
		}

		public override void WriteByte(byte value)
		{
			cache.WriteByte(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			cache.Write(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return baseStream.Seek(offset, origin);
		}
	}
	[ToolboxBitmap(typeof(MySqlConnection), "MySqlClient.resources.connection.bmp")]
	[DesignerCategory("Code")]
	[ToolboxItem(true)]
	public sealed class MySqlConnection : DbConnection, IDisposable, ICloneable
	{
		internal ConnectionState connectionState;

		internal Driver driver;

		internal bool hasBeenOpen;

		private SchemaProvider schemaProvider;

		private ProcedureCache procedureCache;

		private bool isInUse;

		private PerformanceMonitor perfMonitor;

		private ExceptionInterceptor exceptionInterceptor;

		internal CommandInterceptor commandInterceptor;

		private bool isKillQueryConnection;

		private string database;

		private int commandTimeout;

		private static Cache<string, MySqlConnectionStringBuilder> connectionStringCache = new Cache<string, MySqlConnectionStringBuilder>(0, 25);

		internal PerformanceMonitor PerfMonitor => perfMonitor;

		internal ProcedureCache ProcedureCache => procedureCache;

		internal MySqlConnectionStringBuilder Settings { get; private set; }

		internal MySqlDataReader Reader
		{
			get
			{
				if (driver == null)
				{
					return null;
				}
				return driver.reader;
			}
			set
			{
				driver.reader = value;
				isInUse = driver.reader != null;
			}
		}

		internal bool SoftClosed
		{
			get
			{
				if (State == ConnectionState.Closed && driver != null)
				{
					return driver.CurrentTransaction != null;
				}
				return false;
			}
		}

		internal bool IsInUse
		{
			get
			{
				return isInUse;
			}
			set
			{
				isInUse = value;
			}
		}

		[Browsable(false)]
		public int ServerThread => driver.ThreadID;

		[Browsable(true)]
		public override string DataSource => Settings.Server;

		[Browsable(true)]
		public override int ConnectionTimeout => (int)Settings.ConnectionTimeout;

		[Browsable(true)]
		public override string Database => database;

		[Browsable(false)]
		public bool UseCompression => Settings.UseCompression;

		[Browsable(false)]
		public override ConnectionState State => connectionState;

		[Browsable(false)]
		public override string ServerVersion => driver.Version.ToString();

		[Editor("MySql.Data.MySqlClient.Design.ConnectionStringTypeEditor,MySqlClient.Design", typeof(UITypeEditor))]
		[Category("Data")]
		[Browsable(true)]
		[Description("Information used to connect to a DataSource, such as 'Server=xxx;UserId=yyy;Password=zzz;Database=dbdb'.")]
		public override string ConnectionString
		{
			get
			{
				return Settings.GetConnectionString(!hasBeenOpen || Settings.PersistSecurityInfo);
			}
			set
			{
				if (State != ConnectionState.Closed)
				{
					Throw(new MySqlException(string.Concat("Not allowed to change the 'ConnectionString' property while the connection (state=", State, ").")));
				}
				MySqlConnectionStringBuilder mySqlConnectionStringBuilder;
				lock (connectionStringCache)
				{
					if (value == null)
					{
						mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder();
					}
					else
					{
						mySqlConnectionStringBuilder = connectionStringCache[value];
						if (mySqlConnectionStringBuilder == null)
						{
							mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder(value);
							connectionStringCache.Add(value, mySqlConnectionStringBuilder);
						}
					}
				}
				Settings = mySqlConnectionStringBuilder;
				if (Settings.Database != null && Settings.Database.Length > 0)
				{
					database = Settings.Database;
				}
				if (driver != null)
				{
					driver.Settings = mySqlConnectionStringBuilder;
				}
			}
		}

		protected override DbProviderFactory DbProviderFactory => MySqlClientFactory.Instance;

		public bool IsPasswordExpired => driver.IsPasswordExpired;

		public event MySqlInfoMessageEventHandler InfoMessage;

		public MySqlConnection()
		{
			Settings = new MySqlConnectionStringBuilder();
			database = string.Empty;
		}

		public MySqlConnection(string connectionString)
			: this()
		{
			ConnectionString = connectionString;
		}

		~MySqlConnection()
		{
			Dispose(disposing: false);
		}

		internal void OnInfoMessage(MySqlInfoMessageEventArgs args)
		{
			if (this.InfoMessage != null)
			{
				this.InfoMessage(this, args);
			}
		}

		public override void EnlistTransaction(Transaction transaction)
		{
			if (transaction == null)
			{
				return;
			}
			if (driver.CurrentTransaction != null)
			{
				if (driver.CurrentTransaction.BaseTransaction == transaction)
				{
					return;
				}
				Throw(new MySqlException("Already enlisted"));
			}
			Driver driverInTransaction = DriverTransactionManager.GetDriverInTransaction(transaction);
			if (driverInTransaction != null)
			{
				if (driverInTransaction.IsInActiveUse)
				{
					Throw(new NotSupportedException(Resources.MultipleConnectionsInTransactionNotSupported));
				}
				string connectionString = driverInTransaction.Settings.ConnectionString;
				string connectionString2 = Settings.ConnectionString;
				if (string.Compare(connectionString, connectionString2, ignoreCase: true) != 0)
				{
					Throw(new NotSupportedException(Resources.MultipleConnectionsInTransactionNotSupported));
				}
				CloseFully();
				driver = driverInTransaction;
			}
			if (driver.CurrentTransaction == null)
			{
				MySqlPromotableTransaction mySqlPromotableTransaction = new MySqlPromotableTransaction(this, transaction);
				if (!transaction.EnlistPromotableSinglePhase(mySqlPromotableTransaction))
				{
					Throw(new NotSupportedException(Resources.DistributedTxnNotSupported));
				}
				driver.CurrentTransaction = mySqlPromotableTransaction;
				DriverTransactionManager.SetDriverInTransaction(driver);
				driver.IsInActiveUse = true;
			}
		}

		public new MySqlTransaction BeginTransaction()
		{
			return BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
		}

		public new MySqlTransaction BeginTransaction(System.Data.IsolationLevel iso)
		{
			if (State != ConnectionState.Open)
			{
				Throw(new InvalidOperationException(Resources.ConnectionNotOpen));
			}
			if (driver.HasStatus(ServerStatusFlags.InTransaction))
			{
				Throw(new InvalidOperationException(Resources.NoNestedTransactions));
			}
			MySqlTransaction result = new MySqlTransaction(this, iso);
			MySqlCommand mySqlCommand = new MySqlCommand("", this);
			mySqlCommand.CommandText = "SET SESSION TRANSACTION ISOLATION LEVEL ";
			switch (iso)
			{
			case System.Data.IsolationLevel.ReadCommitted:
				mySqlCommand.CommandText += "READ COMMITTED";
				break;
			case System.Data.IsolationLevel.ReadUncommitted:
				mySqlCommand.CommandText += "READ UNCOMMITTED";
				break;
			case System.Data.IsolationLevel.RepeatableRead:
				mySqlCommand.CommandText += "REPEATABLE READ";
				break;
			case System.Data.IsolationLevel.Serializable:
				mySqlCommand.CommandText += "SERIALIZABLE";
				break;
			case System.Data.IsolationLevel.Chaos:
				Throw(new NotSupportedException(Resources.ChaosNotSupported));
				break;
			case System.Data.IsolationLevel.Snapshot:
				Throw(new NotSupportedException(Resources.SnapshotNotSupported));
				break;
			}
			mySqlCommand.ExecuteNonQuery();
			mySqlCommand.CommandText = "BEGIN";
			mySqlCommand.ExecuteNonQuery();
			return result;
		}

		public override void ChangeDatabase(string databaseName)
		{
			if (databaseName == null || databaseName.Trim().Length == 0)
			{
				Throw(new ArgumentException(Resources.ParameterIsInvalid, "databaseName"));
			}
			if (State != ConnectionState.Open)
			{
				Throw(new InvalidOperationException(Resources.ConnectionNotOpen));
			}
			lock (driver)
			{
				if (Transaction.Current != null && Transaction.Current.TransactionInformation.Status == TransactionStatus.Aborted)
				{
					Throw(new TransactionAbortedException());
				}
				using (new CommandTimer(this, (int)Settings.DefaultCommandTimeout))
				{
					driver.SetDatabase(databaseName);
				}
			}
			database = databaseName;
		}

		internal void SetState(ConnectionState newConnectionState, bool broadcast)
		{
			if (newConnectionState != connectionState || broadcast)
			{
				ConnectionState originalState = connectionState;
				connectionState = newConnectionState;
				if (broadcast)
				{
					OnStateChange(new StateChangeEventArgs(originalState, connectionState));
				}
			}
		}

		public bool Ping()
		{
			if (Reader != null)
			{
				Throw(new MySqlException(Resources.DataReaderOpen));
			}
			if (driver != null && driver.Ping())
			{
				return true;
			}
			driver = null;
			SetState(ConnectionState.Closed, broadcast: true);
			return false;
		}

		public override void Open()
		{
			if (State == ConnectionState.Open)
			{
				Throw(new InvalidOperationException(Resources.ConnectionAlreadyOpen));
			}
			exceptionInterceptor = new ExceptionInterceptor(this);
			commandInterceptor = new CommandInterceptor(this);
			SetState(ConnectionState.Connecting, broadcast: true);
			AssertPermissions();
			if (Settings.AutoEnlist && Transaction.Current != null)
			{
				driver = DriverTransactionManager.GetDriverInTransaction(Transaction.Current);
				if (driver != null && (driver.IsInActiveUse || !driver.Settings.EquivalentTo(Settings)))
				{
					Throw(new NotSupportedException(Resources.MultipleConnectionsInTransactionNotSupported));
				}
			}
			try
			{
				MySqlConnectionStringBuilder settings = Settings;
				if (ReplicationManager.IsReplicationGroup(Settings.Server))
				{
					if (driver == null)
					{
						ReplicationManager.GetNewConnection(Settings.Server, master: false, this);
					}
					else
					{
						settings = driver.Settings;
					}
				}
				if (Settings.Pooling)
				{
					MySqlPool pool = MySqlPoolManager.GetPool(settings);
					if (driver == null || !driver.IsOpen)
					{
						driver = pool.GetConnection();
					}
					procedureCache = pool.ProcedureCache;
				}
				else
				{
					if (driver == null || !driver.IsOpen)
					{
						driver = Driver.Create(settings);
					}
					procedureCache = new ProcedureCache((int)Settings.ProcedureCacheSize);
				}
			}
			catch (Exception)
			{
				SetState(ConnectionState.Closed, broadcast: true);
				throw;
			}
			if (driver.Settings.UseOldSyntax)
			{
				MySqlTrace.LogWarning(ServerThread, "You are using old syntax that will be removed in future versions");
			}
			SetState(ConnectionState.Open, broadcast: false);
			driver.Configure(this);
			if ((!driver.SupportsPasswordExpiration || !driver.IsPasswordExpired) && Settings.Database != null && Settings.Database != string.Empty)
			{
				ChangeDatabase(Settings.Database);
			}
			schemaProvider = new ISSchemaProvider(this);
			perfMonitor = new PerformanceMonitor(this);
			if (Transaction.Current != null && Settings.AutoEnlist)
			{
				EnlistTransaction(Transaction.Current);
			}
			hasBeenOpen = true;
			SetState(ConnectionState.Open, broadcast: true);
		}

		public new MySqlCommand CreateCommand()
		{
			MySqlCommand mySqlCommand = new MySqlCommand();
			mySqlCommand.Connection = this;
			return mySqlCommand;
		}

		public object Clone()
		{
			MySqlConnection mySqlConnection = new MySqlConnection();
			string connectionString = Settings.ConnectionString;
			if (connectionString != null)
			{
				mySqlConnection.ConnectionString = connectionString;
			}
			return mySqlConnection;
		}

		internal void Abort()
		{
			try
			{
				driver.Close();
			}
			catch (Exception ex)
			{
				MySqlTrace.LogWarning(ServerThread, "Error occurred aborting the connection. Exception was: " + ex.Message);
			}
			finally
			{
				isInUse = false;
			}
			SetState(ConnectionState.Closed, broadcast: true);
		}

		internal void CloseFully()
		{
			if (Settings.Pooling && driver.IsOpen)
			{
				if (driver.HasStatus(ServerStatusFlags.InTransaction))
				{
					MySqlTransaction mySqlTransaction = new MySqlTransaction(this, System.Data.IsolationLevel.Unspecified);
					mySqlTransaction.Rollback();
				}
				MySqlPoolManager.ReleaseConnection(driver);
			}
			else
			{
				driver.Close();
			}
			driver = null;
		}

		public override void Close()
		{
			if (driver != null)
			{
				driver.IsPasswordExpired = false;
			}
			if (State == ConnectionState.Closed)
			{
				return;
			}
			if (Reader != null)
			{
				Reader.Close();
			}
			if (driver != null)
			{
				if (driver.CurrentTransaction == null)
				{
					CloseFully();
				}
				else
				{
					driver.IsInActiveUse = false;
				}
			}
			SetState(ConnectionState.Closed, broadcast: true);
		}

		internal string CurrentDatabase()
		{
			if (Database != null && Database.Length > 0)
			{
				return Database;
			}
			MySqlCommand mySqlCommand = new MySqlCommand("SELECT database()", this);
			return mySqlCommand.ExecuteScalar().ToString();
		}

		internal void HandleTimeoutOrThreadAbort(Exception ex)
		{
			bool isFatal = false;
			if (isKillQueryConnection)
			{
				Abort();
				if (!(ex is TimeoutException))
				{
					return;
				}
				Throw(new MySqlException(Resources.Timeout, isFatal: true, ex));
			}
			try
			{
				CancelQuery(5);
				driver.ResetTimeout(5000);
				if (Reader != null)
				{
					Reader.Close();
					Reader = null;
				}
			}
			catch (Exception ex2)
			{
				MySqlTrace.LogWarning(ServerThread, "Could not kill query,  aborting connection. Exception was " + ex2.Message);
				Abort();
				isFatal = true;
			}
			if (ex is TimeoutException)
			{
				Throw(new MySqlException(Resources.Timeout, isFatal, ex));
			}
		}

		public void CancelQuery(int timeout)
		{
			MySqlConnectionStringBuilder mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder(Settings.ConnectionString);
			mySqlConnectionStringBuilder.Pooling = false;
			mySqlConnectionStringBuilder.AutoEnlist = false;
			mySqlConnectionStringBuilder.ConnectionTimeout = (uint)timeout;
			using MySqlConnection mySqlConnection = new MySqlConnection(mySqlConnectionStringBuilder.ConnectionString);
			mySqlConnection.isKillQueryConnection = true;
			mySqlConnection.Open();
			string cmdText = "KILL QUERY " + ServerThread;
			MySqlCommand mySqlCommand = new MySqlCommand(cmdText, mySqlConnection);
			mySqlCommand.CommandTimeout = timeout;
			mySqlCommand.ExecuteNonQuery();
		}

		internal bool SetCommandTimeout(int value)
		{
			if (!hasBeenOpen)
			{
				return false;
			}
			if (commandTimeout != 0)
			{
				return false;
			}
			if (driver == null)
			{
				return false;
			}
			commandTimeout = value;
			driver.ResetTimeout(commandTimeout * 1000);
			return true;
		}

		internal void ClearCommandTimeout()
		{
			if (hasBeenOpen)
			{
				commandTimeout = 0;
				if (driver != null)
				{
					driver.ResetTimeout(0);
				}
			}
		}

		public MySqlSchemaCollection GetSchemaCollection(string collectionName, string[] restrictionValues)
		{
			if (collectionName == null)
			{
				collectionName = SchemaProvider.MetaCollection;
			}
			string[] restrictions = schemaProvider.CleanRestrictions(restrictionValues);
			return schemaProvider.GetSchema(collectionName, restrictions);
		}

		public static void ClearPool(MySqlConnection connection)
		{
			MySqlPoolManager.ClearPool(connection.Settings);
		}

		public static void ClearAllPools()
		{
			MySqlPoolManager.ClearAllPools();
		}

		internal void Throw(Exception ex)
		{
			if (exceptionInterceptor == null)
			{
				throw ex;
			}
			exceptionInterceptor.Throw(ex);
		}

		public new void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public override DataTable GetSchema()
		{
			return GetSchema(null);
		}

		public override DataTable GetSchema(string collectionName)
		{
			if (collectionName == null)
			{
				collectionName = SchemaProvider.MetaCollection;
			}
			return GetSchema(collectionName, null);
		}

		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			if (collectionName == null)
			{
				collectionName = SchemaProvider.MetaCollection;
			}
			string[] restrictions = schemaProvider.CleanRestrictions(restrictionValues);
			MySqlSchemaCollection schema = schemaProvider.GetSchema(collectionName, restrictions);
			return schema.AsDataTable();
		}

		protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
		{
			if (isolationLevel == System.Data.IsolationLevel.Unspecified)
			{
				return BeginTransaction();
			}
			return BeginTransaction(isolationLevel);
		}

		protected override DbCommand CreateDbCommand()
		{
			return CreateCommand();
		}

		private void AssertPermissions()
		{
			if (Settings.IncludeSecurityAsserts)
			{
				PermissionSet permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.AddPermission(new MySqlClientPermission(ConnectionString));
				permissionSet.Demand();
				MySqlSecurityPermission.CreatePermissionSet(includeReflectionPermission: true).Assert();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (State == ConnectionState.Open)
			{
				Close();
			}
			base.Dispose(disposing);
		}
	}
	public delegate void MySqlInfoMessageEventHandler(object sender, MySqlInfoMessageEventArgs args);
	public class MySqlInfoMessageEventArgs : EventArgs
	{
		public MySqlError[] errors;
	}
	internal class CommandTimer : IDisposable
	{
		private bool timeoutSet;

		private MySqlConnection connection;

		public CommandTimer(MySqlConnection connection, int timeout)
		{
			this.connection = connection;
			if (connection != null)
			{
				timeoutSet = connection.SetCommandTimeout(timeout);
			}
		}

		public void Dispose()
		{
			if (timeoutSet)
			{
				timeoutSet = false;
				connection.ClearCommandTimeout();
				connection = null;
			}
		}
	}
	internal class Crypt
	{
		private static void XorScramble(byte[] from, int fromIndex, byte[] to, int toIndex, byte[] password, int length)
		{
			if (fromIndex < 0 || fromIndex >= from.Length)
			{
				throw new ArgumentException(Resources.IndexMustBeValid, "fromIndex");
			}
			if (fromIndex + length > from.Length)
			{
				throw new ArgumentException(Resources.FromAndLengthTooBig, "fromIndex");
			}
			if (from == null)
			{
				throw new ArgumentException(Resources.BufferCannotBeNull, "from");
			}
			if (to == null)
			{
				throw new ArgumentException(Resources.BufferCannotBeNull, "to");
			}
			if (toIndex < 0 || toIndex >= to.Length)
			{
				throw new ArgumentException(Resources.IndexMustBeValid, "toIndex");
			}
			if (toIndex + length > to.Length)
			{
				throw new ArgumentException(Resources.IndexAndLengthTooBig, "toIndex");
			}
			if (password == null || password.Length < length)
			{
				throw new ArgumentException(Resources.PasswordMustHaveLegalChars, "password");
			}
			if (length < 0)
			{
				throw new ArgumentException(Resources.ParameterCannotBeNegative, "count");
			}
			for (int i = 0; i < length; i++)
			{
				to[toIndex++] = (byte)(from[fromIndex++] ^ password[i]);
			}
		}

		public static byte[] Get411Password(string password, string seed)
		{
			if (password.Length == 0)
			{
				return new byte[1];
			}
			SHA1 sHA = new SHA1CryptoServiceProvider();
			byte[] array = sHA.ComputeHash(Encoding.Default.GetBytes(password));
			byte[] array2 = sHA.ComputeHash(array);
			byte[] bytes = Encoding.Default.GetBytes(seed);
			byte[] array3 = new byte[bytes.Length + array2.Length];
			Array.Copy(bytes, 0, array3, 0, bytes.Length);
			Array.Copy(array2, 0, array3, bytes.Length, array2.Length);
			byte[] array4 = sHA.ComputeHash(array3);
			byte[] array5 = new byte[array4.Length + 1];
			array5[0] = 20;
			Array.Copy(array4, 0, array5, 1, array4.Length);
			for (int i = 1; i < array5.Length; i++)
			{
				array5[i] ^= array[i - 1];
			}
			return array5;
		}

		private static double rand(ref long seed1, ref long seed2, long max)
		{
			seed1 = seed1 * 3 + seed2;
			seed1 %= max;
			seed2 = (seed1 + seed2 + 33) % max;
			return (double)seed1 / (double)max;
		}

		public static string EncryptPassword(string password, string seed, bool new_ver)
		{
			long num = 1073741823L;
			if (!new_ver)
			{
				num = 33554431L;
			}
			if (password == null || password.Length == 0)
			{
				return password;
			}
			long[] array = Hash(seed);
			long[] array2 = Hash(password);
			long seed2 = (array[0] ^ array2[0]) % num;
			long seed3 = (array[1] ^ array2[1]) % num;
			if (!new_ver)
			{
				seed3 = seed2 / 2;
			}
			char[] array3 = new char[seed.Length];
			for (int i = 0; i < seed.Length; i++)
			{
				double num2 = rand(ref seed2, ref seed3, num);
				array3[i] = (char)(Math.Floor(num2 * 31.0) + 64.0);
			}
			if (new_ver)
			{
				char c = (char)Math.Floor(rand(ref seed2, ref seed3, num) * 31.0);
				for (int j = 0; j < array3.Length; j++)
				{
					array3[j] ^= c;
				}
			}
			return new string(array3);
		}

		private static long[] Hash(string P)
		{
			long num = 1345345333L;
			long num2 = 305419889L;
			long num3 = 7L;
			for (int i = 0; i < P.Length; i++)
			{
				if (P[i] != ' ' && P[i] != '\t')
				{
					long num4 = 0xFF & P[i];
					num ^= ((num & 0x3F) + num3) * num4 + (num << 8);
					num2 += (num2 << 8) ^ num;
					num3 += num4;
				}
			}
			return new long[2]
			{
				num & 0x7FFFFFFF,
				num2 & 0x7FFFFFFF
			};
		}
	}
	[DesignerCategory("Code")]
	[ToolboxBitmap(typeof(MySqlDataAdapter), "MySqlClient.resources.dataadapter.bmp")]
	[Designer("MySql.Data.MySqlClient.Design.MySqlDataAdapterDesigner,MySqlClient.Design")]
	public sealed class MySqlDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		private bool loadingDefaults;

		private int updateBatchSize;

		private List<IDbCommand> commandBatch;

		[Description("Used during Update for deleted rows in Dataset.")]
		public new MySqlCommand DeleteCommand
		{
			get
			{
				return (MySqlCommand)base.DeleteCommand;
			}
			set
			{
				base.DeleteCommand = value;
			}
		}

		[Description("Used during Update for new rows in Dataset.")]
		public new MySqlCommand InsertCommand
		{
			get
			{
				return (MySqlCommand)base.InsertCommand;
			}
			set
			{
				base.InsertCommand = value;
			}
		}

		[Description("Used during Fill/FillSchema")]
		[Category("Fill")]
		public new MySqlCommand SelectCommand
		{
			get
			{
				return (MySqlCommand)base.SelectCommand;
			}
			set
			{
				base.SelectCommand = value;
			}
		}

		[Description("Used during Update for modified rows in Dataset.")]
		public new MySqlCommand UpdateCommand
		{
			get
			{
				return (MySqlCommand)base.UpdateCommand;
			}
			set
			{
				base.UpdateCommand = value;
			}
		}

		internal bool LoadDefaults
		{
			get
			{
				return loadingDefaults;
			}
			set
			{
				loadingDefaults = value;
			}
		}

		public override int UpdateBatchSize
		{
			get
			{
				return updateBatchSize;
			}
			set
			{
				updateBatchSize = value;
			}
		}

		public event MySqlRowUpdatingEventHandler RowUpdating;

		public event MySqlRowUpdatedEventHandler RowUpdated;

		public MySqlDataAdapter()
		{
			loadingDefaults = true;
			updateBatchSize = 1;
		}

		public MySqlDataAdapter(MySqlCommand selectCommand)
			: this()
		{
			SelectCommand = selectCommand;
		}

		public MySqlDataAdapter(string selectCommandText, MySqlConnection connection)
			: this()
		{
			SelectCommand = new MySqlCommand(selectCommandText, connection);
		}

		public MySqlDataAdapter(string selectCommandText, string selectConnString)
			: this()
		{
			SelectCommand = new MySqlCommand(selectCommandText, new MySqlConnection(selectConnString));
		}

		private void OpenConnectionIfClosed(DataRowState state, List<MySqlConnection> openedConnections)
		{
			MySqlCommand mySqlCommand = null;
			switch (state)
			{
			default:
				return;
			case DataRowState.Added:
				mySqlCommand = InsertCommand;
				break;
			case DataRowState.Deleted:
				mySqlCommand = DeleteCommand;
				break;
			case DataRowState.Modified:
				mySqlCommand = UpdateCommand;
				break;
			}
			if (mySqlCommand != null && mySqlCommand.Connection != null && mySqlCommand.Connection.connectionState == ConnectionState.Closed)
			{
				mySqlCommand.Connection.Open();
				openedConnections.Add(mySqlCommand.Connection);
			}
		}

		protected override int Update(DataRow[] dataRows, DataTableMapping tableMapping)
		{
			List<MySqlConnection> list = new List<MySqlConnection>();
			try
			{
				foreach (DataRow dataRow in dataRows)
				{
					OpenConnectionIfClosed(dataRow.RowState, list);
				}
				return base.Update(dataRows, tableMapping);
			}
			finally
			{
				foreach (MySqlConnection item in list)
				{
					item.Close();
				}
			}
		}

		protected override void InitializeBatching()
		{
			commandBatch = new List<IDbCommand>();
		}

		protected override int AddToBatch(IDbCommand command)
		{
			MySqlCommand mySqlCommand = (MySqlCommand)command;
			if (mySqlCommand.BatchableCommandText == null)
			{
				mySqlCommand.GetCommandTextForBatching();
			}
			IDbCommand item = (IDbCommand)((ICloneable)command).Clone();
			commandBatch.Add(item);
			return commandBatch.Count - 1;
		}

		protected override int ExecuteBatch()
		{
			int num = 0;
			int num2 = 0;
			while (num2 < commandBatch.Count)
			{
				MySqlCommand mySqlCommand = (MySqlCommand)commandBatch[num2++];
				int num3 = num2;
				while (num3 < commandBatch.Count)
				{
					MySqlCommand mySqlCommand2 = (MySqlCommand)commandBatch[num3];
					if (mySqlCommand2.BatchableCommandText == null || mySqlCommand2.CommandText != mySqlCommand.CommandText)
					{
						break;
					}
					mySqlCommand.AddToBatch(mySqlCommand2);
					num3++;
					num2++;
				}
				num += mySqlCommand.ExecuteNonQuery();
			}
			return num;
		}

		protected override void ClearBatch()
		{
			if (commandBatch.Count > 0)
			{
				MySqlCommand mySqlCommand = (MySqlCommand)commandBatch[0];
				if (mySqlCommand.Batch != null)
				{
					mySqlCommand.Batch.Clear();
				}
			}
			commandBatch.Clear();
		}

		protected override void TerminateBatching()
		{
			ClearBatch();
			commandBatch = null;
		}

		protected override IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			return (IDataParameter)commandBatch[commandIdentifier].Parameters[parameterIndex];
		}

		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new MySqlRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new MySqlRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			if (this.RowUpdating != null)
			{
				this.RowUpdating(this, value as MySqlRowUpdatingEventArgs);
			}
		}

		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			if (this.RowUpdated != null)
			{
				this.RowUpdated(this, value as MySqlRowUpdatedEventArgs);
			}
		}
	}
	public delegate void MySqlRowUpdatingEventHandler(object sender, MySqlRowUpdatingEventArgs e);
	public delegate void MySqlRowUpdatedEventHandler(object sender, MySqlRowUpdatedEventArgs e);
	public sealed class MySqlRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		public new MySqlCommand Command
		{
			get
			{
				return (MySqlCommand)base.Command;
			}
			set
			{
				base.Command = value;
			}
		}

		public MySqlRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(row, command, statementType, tableMapping)
		{
		}
	}
	public sealed class MySqlRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		public new MySqlCommand Command => (MySqlCommand)base.Command;

		public MySqlRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(row, command, statementType, tableMapping)
		{
		}
	}
	public sealed class MySqlDataReader : DbDataReader, IDataReader, IDisposable, IDataRecord
	{
		private bool isOpen = true;

		private CommandBehavior commandBehavior;

		private MySqlCommand command;

		internal long affectedRows;

		internal Driver driver;

		private PreparableStatement statement;

		private ResultSet resultSet;

		private bool disableZeroAffectedRows;

		private MySqlConnection connection;

		internal PreparableStatement Statement => statement;

		internal MySqlCommand Command => command;

		internal ResultSet ResultSet => resultSet;

		internal CommandBehavior CommandBehavior => commandBehavior;

		public override int FieldCount
		{
			get
			{
				if (resultSet != null)
				{
					return resultSet.Size;
				}
				return 0;
			}
		}

		public override bool HasRows
		{
			get
			{
				if (resultSet != null)
				{
					return resultSet.HasRows;
				}
				return false;
			}
		}

		public override bool IsClosed => !isOpen;

		public override int RecordsAffected
		{
			get
			{
				if (disableZeroAffectedRows && affectedRows == 0)
				{
					return -1;
				}
				return (int)affectedRows;
			}
		}

		public override object this[int i] => GetValue(i);

		public override object this[string name] => this[GetOrdinal(name)];

		public override int Depth => 0;

		internal MySqlDataReader(MySqlCommand cmd, PreparableStatement statement, CommandBehavior behavior)
		{
			command = cmd;
			connection = command.Connection;
			commandBehavior = behavior;
			driver = connection.driver;
			affectedRows = -1L;
			this.statement = statement;
			if (cmd.CommandType == CommandType.StoredProcedure && cmd.UpdatedRowSource == UpdateRowSource.FirstReturnedRecord)
			{
				disableZeroAffectedRows = true;
			}
		}

		public override void Close()
		{
			if (!isOpen)
			{
				return;
			}
			bool flag = (this.commandBehavior & CommandBehavior.CloseConnection) != 0;
			CommandBehavior commandBehavior = this.commandBehavior;
			try
			{
				if (!commandBehavior.Equals(CommandBehavior.SchemaOnly))
				{
					this.commandBehavior = CommandBehavior.Default;
				}
				while (NextResult())
				{
				}
			}
			catch (MySqlException ex)
			{
				if (!ex.IsQueryAborted)
				{
					bool flag2 = false;
					for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
					{
						if (ex2 is IOException)
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						throw;
					}
				}
			}
			catch (IOException)
			{
			}
			finally
			{
				connection.Reader = null;
				this.commandBehavior = commandBehavior;
			}
			command.Close(this);
			this.commandBehavior = CommandBehavior.Default;
			if (command.Canceled && connection.driver.Version.isAtLeast(5, 1, 0))
			{
				ClearKillFlag();
			}
			if (flag)
			{
				connection.Close();
			}
			command = null;
			connection.IsInUse = false;
			connection = null;
			isOpen = false;
		}

		public bool GetBoolean(string name)
		{
			return GetBoolean(GetOrdinal(name));
		}

		public override bool GetBoolean(int i)
		{
			return Convert.ToBoolean(GetValue(i));
		}

		public byte GetByte(string name)
		{
			return GetByte(GetOrdinal(name));
		}

		public override byte GetByte(int i)
		{
			IMySqlValue fieldValue = GetFieldValue(i, checkNull: false);
			if (fieldValue is MySqlUByte mySqlUByte)
			{
				return mySqlUByte.Value;
			}
			return (byte)((MySqlByte)(object)fieldValue).Value;
		}

		public sbyte GetSByte(string name)
		{
			return GetSByte(GetOrdinal(name));
		}

		public sbyte GetSByte(int i)
		{
			IMySqlValue fieldValue = GetFieldValue(i, checkNull: false);
			if (fieldValue is MySqlByte mySqlByte)
			{
				return mySqlByte.Value;
			}
			return ((MySqlByte)(object)fieldValue).Value;
		}

		public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			if (i >= FieldCount)
			{
				Throw(new IndexOutOfRangeException());
			}
			IMySqlValue fieldValue = GetFieldValue(i, checkNull: false);
			if (!(fieldValue is MySqlBinary) && !(fieldValue is MySqlGuid))
			{
				Throw(new MySqlException("GetBytes can only be called on binary or guid columns"));
			}
			byte[] array = null;
			array = ((!(fieldValue is MySqlBinary mySqlBinary)) ? ((MySqlGuid)(object)fieldValue).Bytes : mySqlBinary.Value);
			if (buffer == null)
			{
				return array.Length;
			}
			if (bufferoffset >= buffer.Length || bufferoffset < 0)
			{
				Throw(new IndexOutOfRangeException("Buffer index must be a valid index in buffer"));
			}
			if (buffer.Length < bufferoffset + length)
			{
				Throw(new ArgumentException("Buffer is not large enough to hold the requested data"));
			}
			if (fieldOffset < 0 || ((ulong)fieldOffset >= (ulong)array.Length && (long)array.Length != 0))
			{
				Throw(new IndexOutOfRangeException("Data index must be a valid index in the field"));
			}
			if ((ulong)array.Length < (ulong)(fieldOffset + length))
			{
				length = (int)(array.Length - fieldOffset);
			}
			Buffer.BlockCopy(array, (int)fieldOffset, buffer, bufferoffset, length);
			return length;
		}

		private object ChangeType(IMySqlValue value, int fieldIndex, Type newType)
		{
			resultSet.Fields[fieldIndex].AddTypeConversion(newType);
			return Convert.ChangeType(value.Value, newType, CultureInfo.InvariantCulture);
		}

		public char GetChar(string name)
		{
			return GetChar(GetOrdinal(name));
		}

		public override char GetChar(int i)
		{
			string text = GetString(i);
			return text[0];
		}

		public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			if (i >= FieldCount)
			{
				Throw(new IndexOutOfRangeException());
			}
			string text = GetString(i);
			if (buffer == null)
			{
				return text.Length;
			}
			if (bufferoffset >= buffer.Length || bufferoffset < 0)
			{
				Throw(new IndexOutOfRangeException("Buffer index must be a valid index in buffer"));
			}
			if (buffer.Length < bufferoffset + length)
			{
				Throw(new ArgumentException("Buffer is not large enough to hold the requested data"));
			}
			if (fieldoffset < 0 || fieldoffset >= text.Length)
			{
				Throw(new IndexOutOfRangeException("Field offset must be a valid index in the field"));
			}
			if (text.Length < length)
			{
				length = text.Length;
			}
			text.CopyTo((int)fieldoffset, buffer, bufferoffset, length);
			return length;
		}

		public override string GetDataTypeName(int i)
		{
			if (!isOpen)
			{
				Throw(new Exception("No current query in data reader"));
			}
			if (i >= FieldCount)
			{
				Throw(new IndexOutOfRangeException());
			}
			IMySqlValue mySqlValue = resultSet.Values[i];
			return mySqlValue.MySqlTypeName;
		}

		public MySqlDateTime GetMySqlDateTime(string column)
		{
			return GetMySqlDateTime(GetOrdinal(column));
		}

		public MySqlDateTime GetMySqlDateTime(int column)
		{
			return (MySqlDateTime)(object)GetFieldValue(column, checkNull: true);
		}

		public DateTime GetDateTime(string column)
		{
			return GetDateTime(GetOrdinal(column));
		}

		public override DateTime GetDateTime(int i)
		{
			IMySqlValue fieldValue = GetFieldValue(i, checkNull: true);
			MySqlDateTime mySqlDateTime;
			if (fieldValue is MySqlDateTime)
			{
				mySqlDateTime = (MySqlDateTime)(object)fieldValue;
			}
			else
			{
				string s = GetString(i);
				mySqlDateTime = MySqlDateTime.Parse(s);
			}
			mySqlDateTime.TimezoneOffset = driver.timeZoneOffset;
			if (connection.Settings.ConvertZeroDateTime && !mySqlDateTime.IsValidDateTime)
			{
				return DateTime.MinValue;
			}
			return mySqlDateTime.GetDateTime();
		}

		public MySqlDecimal GetMySqlDecimal(string column)
		{
			return GetMySqlDecimal(GetOrdinal(column));
		}

		public MySqlDecimal GetMySqlDecimal(int i)
		{
			return (MySqlDecimal)(object)GetFieldValue(i, checkNull: false);
		}

		public decimal GetDecimal(string column)
		{
			return GetDecimal(GetOrdinal(column));
		}

		public override decimal GetDecimal(int i)
		{
			IMySqlValue fieldValue = GetFieldValue(i, checkNull: true);
			if (fieldValue is MySqlDecimal mySqlDecimal)
			{
				return mySqlDecimal.Value;
			}
			return Convert.ToDecimal(fieldValue.Value);
		}

		public double GetDouble(string column)
		{
			return GetDouble(GetOrdinal(column));
		}

		public override double GetDouble(int i)
		{
			IMySqlValue fieldValue = GetFieldValue(i, checkNull: true);
			if (fieldValue is MySqlDouble mySqlDouble)
			{
				return mySqlDouble.Value;
			}
			return Convert.ToDouble(fieldValue.Value);
		}

		public Type GetFieldType(string column)
		{
			return GetFieldType(GetOrdinal(column));
		}

		public override Type GetFieldType(int i)
		{
			if (!isOpen)
			{
				Throw(new Exception("No current query in data reader"));
			}
			if (i >= FieldCount)
			{
				Throw(new IndexOutOfRangeException());
			}
			IMySqlValue mySqlValue = resultSet.Values[i];
			if (mySqlValue is MySqlDateTime)
			{
				if (!connection.Settings.AllowZeroDateTime)
				{
					return typeof(DateTime);
				}
				return typeof(MySqlDateTime);
			}
			return mySqlValue.SystemType;
		}

		public float GetFloat(string column)
		{
			return GetFloat(GetOrdinal(column));
		}

		public override float GetFloat(int i)
		{
			IMySqlValue fieldValue = GetFieldValue(i, checkNull: true);
			if (fieldValue is MySqlSingle mySqlSingle)
			{
				return mySqlSingle.Value;
			}
			return Convert.ToSingle(fieldValue.Value);
		}

		public Guid GetGuid(string column)
		{
			return GetGuid(GetOrdinal(column));
		}

		public override Guid GetGuid(int i)
		{
			object value = GetValue(i);
			if (value is Guid)
			{
				return (Guid)value;
			}
			if (value is string)
			{
				return new Guid(value as string);
			}
			if (value is byte[])
			{
				byte[] array = (byte[])value;
				if (array.Length == 16)
				{
					return new Guid(array);
				}
			}
			Throw(new MySqlException(Resources.ValueNotSupportedForGuid));
			return Guid.Empty;
		}

		public short GetInt16(string column)
		{
			return GetInt16(GetOrdinal(column));
		}

		public override short GetInt16(int i)
		{
			IMySqlValue fieldValue = GetFieldValue(i, checkNull: true);
			if (fieldValue is MySqlInt16 mySqlInt)
			{
				return mySqlInt.Value;
			}
			return (short)ChangeType(fieldValue, i, typeof(short));
		}

		public int GetInt32(string column)
		{
			return GetInt32(GetOrdinal(column));
		}

		public override int GetInt32(int i)
		{
			IMySqlValue fieldValue = GetFieldValue(i, checkNull: true);
			if (fieldValue is MySqlInt32 mySqlInt)
			{
				return mySqlInt.Value;
			}
			return (int)ChangeType(fieldValue, i, typeof(int));
		}

		public long GetInt64(string column)
		{
			return GetInt64(GetOrdinal(column));
		}

		public override long GetInt64(int i)
		{
			IMySqlValue fieldValue = GetFieldValue(i, checkNull: true);
			if (fieldValue is MySqlInt64 mySqlInt)
			{
				return mySqlInt.Value;
			}
			return (long)ChangeType(fieldValue, i, typeof(long));
		}

		public override string GetName(int i)
		{
			if (!isOpen)
			{
				Throw(new Exception("No current query in data reader"));
			}
			if (i >= FieldCount)
			{
				Throw(new IndexOutOfRangeException());
			}
			return resultSet.Fields[i].ColumnName;
		}

		public override int GetOrdinal(string name)
		{
			if (!isOpen || resultSet == null)
			{
				Throw(new Exception("No current query in data reader"));
			}
			return resultSet.GetOrdinal(name);
		}

		public string GetString(string column)
		{
			return GetString(GetOrdinal(column));
		}

		public override string GetString(int i)
		{
			IMySqlValue fieldValue = GetFieldValue(i, checkNull: true);
			if (fieldValue is MySqlBinary { Value: var value })
			{
				return resultSet.Fields[i].Encoding.GetString(value, 0, value.Length);
			}
			return fieldValue.Value.ToString();
		}

		public TimeSpan GetTimeSpan(string column)
		{
			return GetTimeSpan(GetOrdinal(column));
		}

		public TimeSpan GetTimeSpan(int column)
		{
			IMySqlValue fieldValue = GetFieldValue(column, checkNull: true);
			return ((MySqlTimeSpan)(object)fieldValue).Value;
		}

		public override object GetValue(int i)
		{
			if (!isOpen)
			{
				Throw(new Exception("No current query in data reader"));
			}
			if (i >= FieldCount)
			{
				Throw(new IndexOutOfRangeException());
			}
			IMySqlValue fieldValue = GetFieldValue(i, checkNull: false);
			if (fieldValue.IsNull)
			{
				return DBNull.Value;
			}
			if (fieldValue is MySqlDateTime mySqlDateTime)
			{
				if (!mySqlDateTime.IsValidDateTime && connection.Settings.ConvertZeroDateTime)
				{
					return DateTime.MinValue;
				}
				if (connection.Settings.AllowZeroDateTime)
				{
					return fieldValue;
				}
				return mySqlDateTime.GetDateTime();
			}
			return fieldValue.Value;
		}

		public override int GetValues(object[] values)
		{
			int num = Math.Min(values.Length, FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = GetValue(i);
			}
			return num;
		}

		public ushort GetUInt16(string column)
		{
			return GetUInt16(GetOrdinal(column));
		}

		public ushort GetUInt16(int column)
		{
			IMySqlValue fieldValue = GetFieldValue(column, checkNull: true);
			if (fieldValue is MySqlUInt16 mySqlUInt)
			{
				return mySqlUInt.Value;
			}
			return (ushort)ChangeType(fieldValue, column, typeof(ushort));
		}

		public uint GetUInt32(string column)
		{
			return GetUInt32(GetOrdinal(column));
		}

		public uint GetUInt32(int column)
		{
			IMySqlValue fieldValue = GetFieldValue(column, checkNull: true);
			if (fieldValue is MySqlUInt32 mySqlUInt)
			{
				return mySqlUInt.Value;
			}
			return (uint)ChangeType(fieldValue, column, typeof(uint));
		}

		public ulong GetUInt64(string column)
		{
			return GetUInt64(GetOrdinal(column));
		}

		public ulong GetUInt64(int column)
		{
			IMySqlValue fieldValue = GetFieldValue(column, checkNull: true);
			if (fieldValue is MySqlUInt64 mySqlUInt)
			{
				return mySqlUInt.Value;
			}
			return (ulong)ChangeType(fieldValue, column, typeof(ulong));
		}

		IDataReader IDataRecord.GetData(int i)
		{
			return GetData(i);
		}

		public override bool IsDBNull(int i)
		{
			return DBNull.Value == GetValue(i);
		}

		public override bool NextResult()
		{
			if (!isOpen)
			{
				Throw(new MySqlException(Resources.NextResultIsClosed));
			}
			bool flag = command.CommandType == CommandType.TableDirect && command.EnableCaching && (commandBehavior & CommandBehavior.SequentialAccess) == 0;
			if (resultSet != null)
			{
				resultSet.Close();
				if (flag)
				{
					TableCache.AddToCache(command.CommandText, resultSet);
				}
			}
			if (resultSet != null && ((commandBehavior & CommandBehavior.SingleResult) != CommandBehavior.Default || flag))
			{
				return false;
			}
			try
			{
				do
				{
					resultSet = null;
					if (flag)
					{
						resultSet = TableCache.RetrieveFromCache(command.CommandText, command.CacheAge);
					}
					if (resultSet == null)
					{
						resultSet = driver.NextResult(Statement.StatementId, force: false);
						if (resultSet == null)
						{
							return false;
						}
						if (resultSet.IsOutputParameters && command.CommandType == CommandType.StoredProcedure)
						{
							StoredProcedure storedProcedure = statement as StoredProcedure;
							storedProcedure.ProcessOutputParameters(this);
							resultSet.Close();
							if (!storedProcedure.ServerProvidingOutputParameters)
							{
								return false;
							}
							resultSet = driver.NextResult(Statement.StatementId, force: true);
						}
						resultSet.Cached = flag;
					}
					if (resultSet.Size == 0)
					{
						Command.lastInsertedId = resultSet.InsertedId;
						if (affectedRows == -1)
						{
							affectedRows = resultSet.AffectedRows;
						}
						else
						{
							affectedRows += resultSet.AffectedRows;
						}
					}
				}
				while (resultSet.Size == 0);
				return true;
			}
			catch (MySqlException ex)
			{
				if (ex.IsFatal)
				{
					connection.Abort();
				}
				if (ex.Number == 0)
				{
					throw new MySqlException(Resources.FatalErrorReadingResult, ex);
				}
				if ((commandBehavior & CommandBehavior.CloseConnection) != CommandBehavior.Default)
				{
					Close();
				}
				throw;
			}
		}

		public override bool Read()
		{
			if (!isOpen)
			{
				Throw(new MySqlException("Invalid attempt to Read when reader is closed."));
			}
			if (resultSet == null)
			{
				return false;
			}
			try
			{
				return resultSet.NextRow(commandBehavior);
			}
			catch (TimeoutException ex)
			{
				connection.HandleTimeoutOrThreadAbort(ex);
				throw;
			}
			catch (ThreadAbortException ex2)
			{
				connection.HandleTimeoutOrThreadAbort(ex2);
				throw;
			}
			catch (MySqlException ex3)
			{
				if (ex3.IsFatal)
				{
					connection.Abort();
				}
				if (ex3.IsQueryAborted)
				{
					throw;
				}
				throw new MySqlException(Resources.FatalErrorDuringRead, ex3);
			}
		}

		private IMySqlValue GetFieldValue(int index, bool checkNull)
		{
			if (index < 0 || index >= FieldCount)
			{
				Throw(new ArgumentException(Resources.InvalidColumnOrdinal));
			}
			IMySqlValue mySqlValue = resultSet[index];
			if (checkNull && mySqlValue.IsNull)
			{
				throw new SqlNullValueException();
			}
			return mySqlValue;
		}

		private void ClearKillFlag()
		{
			string cmdText = "SELECT * FROM bogus_table LIMIT 0";
			MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection);
			mySqlCommand.InternallyCreated = true;
			try
			{
				mySqlCommand.ExecuteReader();
			}
			catch (MySqlException ex)
			{
				if (ex.Number != 1146)
				{
					throw;
				}
			}
		}

		private void ProcessOutputParameters()
		{
			if (!driver.SupportsOutputParameters || !command.IsPrepared)
			{
				AdjustOutputTypes();
			}
			if ((commandBehavior & CommandBehavior.SchemaOnly) != CommandBehavior.Default)
			{
				return;
			}
			resultSet.NextRow(commandBehavior);
			string text = "@_cnet_param_";
			for (int i = 0; i < FieldCount; i++)
			{
				string text2 = GetName(i);
				if (text2.StartsWith(text))
				{
					text2 = text2.Remove(0, text.Length);
				}
				MySqlParameter parameterFlexible = command.Parameters.GetParameterFlexible(text2, throwOnNotFound: true);
				parameterFlexible.Value = GetValue(i);
			}
		}

		private void AdjustOutputTypes()
		{
			for (int i = 0; i < FieldCount; i++)
			{
				string name = GetName(i);
				name = name.Remove(0, "_cnet_param_".Length + 1);
				MySqlParameter parameterFlexible = command.Parameters.GetParameterFlexible(name, throwOnNotFound: true);
				IMySqlValue iMySqlValue = MySqlField.GetIMySqlValue(parameterFlexible.MySqlDbType);
				if (iMySqlValue is MySqlBit mySqlBit)
				{
					mySqlBit.ReadAsString = true;
					resultSet.SetValueObject(i, mySqlBit);
				}
				else
				{
					resultSet.SetValueObject(i, iMySqlValue);
				}
			}
		}

		private void Throw(Exception ex)
		{
			if (connection != null)
			{
				connection.Throw(ex);
			}
			throw ex;
		}

		public new void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		internal new void Dispose(bool disposing)
		{
			if (disposing)
			{
				Close();
			}
		}

		~MySqlDataReader()
		{
			Dispose(disposing: false);
		}

		public MySqlGeometry GetMySqlGeometry(int i)
		{
			try
			{
				IMySqlValue fieldValue = GetFieldValue(i, checkNull: false);
				if (fieldValue is MySqlGeometry || fieldValue is MySqlBinary)
				{
					return new MySqlGeometry(MySqlDbType.Geometry, (byte[])fieldValue.Value);
				}
			}
			catch
			{
				Throw(new Exception("Can't get MySqlGeometry from value"));
			}
			return new MySqlGeometry(isNull: true);
		}

		public MySqlGeometry GetMySqlGeometry(string column)
		{
			return GetMySqlGeometry(GetOrdinal(column));
		}

		public override DataTable GetSchemaTable()
		{
			if (FieldCount == 0)
			{
				return null;
			}
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.Columns.Add("ColumnName", typeof(string));
			dataTable.Columns.Add("ColumnOrdinal", typeof(int));
			dataTable.Columns.Add("ColumnSize", typeof(int));
			dataTable.Columns.Add("NumericPrecision", typeof(int));
			dataTable.Columns.Add("NumericScale", typeof(int));
			dataTable.Columns.Add("IsUnique", typeof(bool));
			dataTable.Columns.Add("IsKey", typeof(bool));
			DataColumn dataColumn = dataTable.Columns["IsKey"];
			dataColumn.AllowDBNull = true;
			dataTable.Columns.Add("BaseCatalogName", typeof(string));
			dataTable.Columns.Add("BaseColumnName", typeof(string));
			dataTable.Columns.Add("BaseSchemaName", typeof(string));
			dataTable.Columns.Add("BaseTableName", typeof(string));
			dataTable.Columns.Add("DataType", typeof(Type));
			dataTable.Columns.Add("AllowDBNull", typeof(bool));
			dataTable.Columns.Add("ProviderType", typeof(int));
			dataTable.Columns.Add("IsAliased", typeof(bool));
			dataTable.Columns.Add("IsExpression", typeof(bool));
			dataTable.Columns.Add("IsIdentity", typeof(bool));
			dataTable.Columns.Add("IsAutoIncrement", typeof(bool));
			dataTable.Columns.Add("IsRowVersion", typeof(bool));
			dataTable.Columns.Add("IsHidden", typeof(bool));
			dataTable.Columns.Add("IsLong", typeof(bool));
			dataTable.Columns.Add("IsReadOnly", typeof(bool));
			int num = 1;
			for (int i = 0; i < FieldCount; i++)
			{
				MySqlField mySqlField = resultSet.Fields[i];
				DataRow dataRow = dataTable.NewRow();
				dataRow["ColumnName"] = mySqlField.ColumnName;
				dataRow["ColumnOrdinal"] = num++;
				dataRow["ColumnSize"] = (mySqlField.IsTextField ? (mySqlField.ColumnLength / mySqlField.MaxLength) : mySqlField.ColumnLength);
				int precision = mySqlField.Precision;
				int scale = mySqlField.Scale;
				if (precision != -1)
				{
					dataRow["NumericPrecision"] = (short)precision;
				}
				if (scale != -1)
				{
					dataRow["NumericScale"] = (short)scale;
				}
				dataRow["DataType"] = GetFieldType(i);
				dataRow["ProviderType"] = (int)mySqlField.Type;
				dataRow["IsLong"] = mySqlField.IsBlob && mySqlField.ColumnLength > 255;
				dataRow["AllowDBNull"] = mySqlField.AllowsNull;
				dataRow["IsReadOnly"] = false;
				dataRow["IsRowVersion"] = false;
				dataRow["IsUnique"] = false;
				dataRow["IsKey"] = mySqlField.IsPrimaryKey;
				dataRow["IsAutoIncrement"] = mySqlField.IsAutoIncrement;
				dataRow["BaseSchemaName"] = mySqlField.DatabaseName;
				dataRow["BaseCatalogName"] = null;
				dataRow["BaseTableName"] = mySqlField.RealTableName;
				dataRow["BaseColumnName"] = mySqlField.OriginalColumnName;
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator((IDataReader)this, (commandBehavior & CommandBehavior.CloseConnection) != 0);
		}
	}
	internal class Driver : IDisposable
	{
		protected Encoding encoding;

		protected MySqlConnectionStringBuilder connectionString;

		protected bool isOpen;

		protected DateTime creationTime;

		protected string serverCharSet;

		protected int serverCharSetIndex;

		protected Dictionary<string, string> serverProps;

		protected Dictionary<int, string> charSets;

		protected long maxPacketSize;

		internal int timeZoneOffset;

		private DateTime idleSince;

		protected MySqlPromotableTransaction currentTransaction;

		protected bool inActiveUse;

		protected MySqlPool pool;

		private bool firstResult;

		protected IDriver handler;

		internal MySqlDataReader reader;

		private bool disposeInProgress;

		internal bool isFabric;

		public DateTime IdleSince
		{
			get
			{
				return idleSince;
			}
			set
			{
				idleSince = value;
			}
		}

		public int ThreadID => handler.ThreadId;

		public DBVersion Version => handler.Version;

		public MySqlConnectionStringBuilder Settings
		{
			get
			{
				return connectionString;
			}
			set
			{
				connectionString = value;
			}
		}

		public Encoding Encoding
		{
			get
			{
				return encoding;
			}
			set
			{
				encoding = value;
			}
		}

		public MySqlPromotableTransaction CurrentTransaction
		{
			get
			{
				return currentTransaction;
			}
			set
			{
				currentTransaction = value;
			}
		}

		public bool IsInActiveUse
		{
			get
			{
				return inActiveUse;
			}
			set
			{
				inActiveUse = value;
			}
		}

		public bool IsOpen => isOpen;

		public MySqlPool Pool
		{
			get
			{
				return pool;
			}
			set
			{
				pool = value;
			}
		}

		public long MaxPacketSize => maxPacketSize;

		internal int ConnectionCharSetIndex
		{
			get
			{
				return serverCharSetIndex;
			}
			set
			{
				serverCharSetIndex = value;
			}
		}

		internal Dictionary<int, string> CharacterSets => charSets;

		public bool SupportsOutputParameters => Version.isAtLeast(5, 5, 0);

		public bool SupportsBatch => (handler.Flags & ClientFlags.MULTI_STATEMENTS) != 0;

		public bool SupportsConnectAttrs => (handler.Flags & ClientFlags.CONNECT_ATTRS) != 0;

		public bool SupportsPasswordExpiration => (handler.Flags & ClientFlags.CAN_HANDLE_EXPIRED_PASSWORD) != 0;

		public bool IsPasswordExpired { get; internal set; }

		public Driver(MySqlConnectionStringBuilder settings)
		{
			encoding = Encoding.Default;
			if (encoding == null)
			{
				throw new MySqlException(Resources.DefaultEncodingNotFound);
			}
			connectionString = settings;
			serverCharSet = "latin1";
			serverCharSetIndex = -1;
			maxPacketSize = 1024L;
			handler = new NativeDriver(this);
		}

		~Driver()
		{
			Dispose(disposing: false);
		}

		public string Property(string key)
		{
			return serverProps[key];
		}

		public bool ConnectionLifetimeExpired()
		{
			TimeSpan timeSpan = DateTime.Now.Subtract(creationTime);
			if (Settings.ConnectionLifeTime != 0 && timeSpan.TotalSeconds > (double)Settings.ConnectionLifeTime)
			{
				return true;
			}
			return false;
		}

		public static Driver Create(MySqlConnectionStringBuilder settings)
		{
			Driver driver = null;
			try
			{
				if (MySqlTrace.QueryAnalysisEnabled || settings.Logging || settings.UseUsageAdvisor)
				{
					driver = new TracingDriver(settings);
				}
			}
			catch (TypeInitializationException ex)
			{
				if (!(ex.InnerException is SecurityException))
				{
					throw ex;
				}
			}
			if (driver == null)
			{
				driver = new Driver(settings);
			}
			try
			{
				driver.Open();
				return driver;
			}
			catch
			{
				driver.Dispose();
				throw;
			}
		}

		public bool HasStatus(ServerStatusFlags flag)
		{
			return (handler.ServerStatus & flag) != 0;
		}

		public virtual void Open()
		{
			creationTime = DateTime.Now;
			handler.Open();
			isOpen = true;
		}

		public virtual void Close()
		{
			Dispose();
		}

		public virtual void Configure(MySqlConnection connection)
		{
			bool flag = false;
			if (serverProps == null)
			{
				flag = true;
				try
				{
					if (Pool != null && Settings.CacheServerProperties)
					{
						if (Pool.ServerProperties == null)
						{
							Pool.ServerProperties = LoadServerProperties(connection);
						}
						serverProps = Pool.ServerProperties;
					}
					else
					{
						serverProps = LoadServerProperties(connection);
					}
					LoadCharacterSets(connection);
				}
				catch (MySqlException ex)
				{
					if (ex.Number == 1820)
					{
						IsPasswordExpired = true;
						return;
					}
					throw;
				}
			}
			if (Settings.ConnectionReset || flag)
			{
				string text = connectionString.CharacterSet;
				if (text == null || text.Length == 0)
				{
					text = ((serverCharSetIndex < 0 || !charSets.ContainsKey(serverCharSetIndex)) ? serverCharSet : charSets[serverCharSetIndex]);
				}
				if (serverProps.ContainsKey("max_allowed_packet"))
				{
					maxPacketSize = Convert.ToInt64(serverProps["max_allowed_packet"]);
				}
				MySqlCommand mySqlCommand = new MySqlCommand("SET character_set_results=NULL", connection);
				mySqlCommand.InternallyCreated = true;
				serverProps.TryGetValue("character_set_client", out var value);
				serverProps.TryGetValue("character_set_connection", out var value2);
				if ((value != null && value.ToString() != text) || (value2 != null && value2.ToString() != text))
				{
					MySqlCommand mySqlCommand2 = new MySqlCommand("SET NAMES " + text, connection);
					mySqlCommand2.InternallyCreated = true;
					mySqlCommand2.ExecuteNonQuery();
				}
				mySqlCommand.ExecuteNonQuery();
				if (text != null)
				{
					Encoding = CharSetMap.GetEncoding(Version, text);
				}
				else
				{
					Encoding = CharSetMap.GetEncoding(Version, "latin1");
				}
				handler.Configure();
			}
		}

		private Dictionary<string, string> LoadServerProperties(MySqlConnection connection)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			MySqlCommand mySqlCommand = new MySqlCommand("SHOW VARIABLES", connection);
			try
			{
				using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
				{
					while (mySqlDataReader.Read())
					{
						string key = mySqlDataReader.GetString(0);
						string value = mySqlDataReader.GetString(1);
						dictionary[key] = value;
					}
				}
				timeZoneOffset = GetTimeZoneOffset(connection);
				return dictionary;
			}
			catch (Exception ex)
			{
				MySqlTrace.LogError(ThreadID, ex.Message);
				throw;
			}
		}

		private int GetTimeZoneOffset(MySqlConnection con)
		{
			MySqlCommand mySqlCommand = new MySqlCommand("select timediff( curtime(), utc_time() )", con);
			string text = mySqlCommand.ExecuteScalar() as string;
			if (text == null)
			{
				text = "0:00";
			}
			return int.Parse(text.Substring(0, text.IndexOf(':')));
		}

		private void LoadCharacterSets(MySqlConnection connection)
		{
			MySqlCommand mySqlCommand = new MySqlCommand("SHOW COLLATION", connection);
			try
			{
				using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
				charSets = new Dictionary<int, string>();
				while (mySqlDataReader.Read())
				{
					charSets[Convert.ToInt32(mySqlDataReader["id"], NumberFormatInfo.InvariantInfo)] = mySqlDataReader.GetString(mySqlDataReader.GetOrdinal("charset"));
				}
			}
			catch (Exception ex)
			{
				MySqlTrace.LogError(ThreadID, ex.Message);
				throw;
			}
		}

		public virtual List<MySqlError> ReportWarnings(MySqlConnection connection)
		{
			List<MySqlError> list = new List<MySqlError>();
			MySqlCommand mySqlCommand = new MySqlCommand("SHOW WARNINGS", connection);
			mySqlCommand.InternallyCreated = true;
			using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
			{
				while (mySqlDataReader.Read())
				{
					list.Add(new MySqlError(mySqlDataReader.GetString(0), mySqlDataReader.GetInt32(1), mySqlDataReader.GetString(2)));
				}
			}
			MySqlInfoMessageEventArgs e = new MySqlInfoMessageEventArgs();
			e.errors = list.ToArray();
			connection?.OnInfoMessage(e);
			return list;
		}

		public virtual void SendQuery(MySqlPacket p)
		{
			handler.SendQuery(p);
			firstResult = true;
		}

		public virtual ResultSet NextResult(int statementId, bool force)
		{
			if (!force && !firstResult && !HasStatus(ServerStatusFlags.MoreResults | ServerStatusFlags.AnotherQuery))
			{
				return null;
			}
			firstResult = false;
			int affectedRows = -1;
			long insertedId = -1L;
			int result = GetResult(statementId, ref affectedRows, ref insertedId);
			if (result == -1)
			{
				return null;
			}
			if (result > 0)
			{
				return new ResultSet(this, statementId, result);
			}
			return new ResultSet(affectedRows, insertedId);
		}

		protected virtual int GetResult(int statementId, ref int affectedRows, ref long insertedId)
		{
			return handler.GetResult(ref affectedRows, ref insertedId);
		}

		public virtual bool FetchDataRow(int statementId, int columns)
		{
			return handler.FetchDataRow(statementId, columns);
		}

		public virtual bool SkipDataRow()
		{
			return FetchDataRow(-1, 0);
		}

		public virtual void ExecuteDirect(string sql)
		{
			MySqlPacket mySqlPacket = new MySqlPacket(Encoding);
			mySqlPacket.WriteString(sql);
			SendQuery(mySqlPacket);
			NextResult(0, force: false);
		}

		public MySqlField[] GetColumns(int count)
		{
			MySqlField[] array = new MySqlField[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = new MySqlField(this);
			}
			handler.GetColumnsData(array);
			return array;
		}

		public virtual int PrepareStatement(string sql, ref MySqlField[] parameters)
		{
			return handler.PrepareStatement(sql, ref parameters);
		}

		public IMySqlValue ReadColumnValue(int index, MySqlField field, IMySqlValue value)
		{
			return handler.ReadColumnValue(index, field, value);
		}

		public void SkipColumnValue(IMySqlValue valObject)
		{
			handler.SkipColumnValue(valObject);
		}

		public void ResetTimeout(int timeoutMilliseconds)
		{
			handler.ResetTimeout(timeoutMilliseconds);
		}

		public bool Ping()
		{
			return handler.Ping();
		}

		public virtual void SetDatabase(string dbName)
		{
			handler.SetDatabase(dbName);
		}

		public virtual void ExecuteStatement(MySqlPacket packetToExecute)
		{
			handler.ExecuteStatement(packetToExecute);
		}

		public virtual void CloseStatement(int id)
		{
			handler.CloseStatement(id);
		}

		public virtual void Reset()
		{
			handler.Reset();
		}

		public virtual void CloseQuery(MySqlConnection connection, int statementId)
		{
			if (handler.WarningCount > 0)
			{
				ReportWarnings(connection);
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposeInProgress)
			{
				return;
			}
			disposeInProgress = true;
			try
			{
				ResetTimeout(1000);
				if (disposing)
				{
					handler.Close(isOpen);
				}
				if (connectionString.Pooling)
				{
					MySqlPoolManager.RemoveConnection(this);
				}
			}
			catch (Exception)
			{
				if (disposing)
				{
					throw;
				}
			}
			finally
			{
				reader = null;
				isOpen = false;
				disposeInProgress = false;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
	internal interface IDriver
	{
		int ThreadId { get; }

		DBVersion Version { get; }

		ServerStatusFlags ServerStatus { get; }

		ClientFlags Flags { get; }

		int WarningCount { get; }

		void Configure();

		void Open();

		void SendQuery(MySqlPacket packet);

		void Close(bool isOpen);

		bool Ping();

		int GetResult(ref int affectedRows, ref long insertedId);

		bool FetchDataRow(int statementId, int columns);

		int PrepareStatement(string sql, ref MySqlField[] parameters);

		void ExecuteStatement(MySqlPacket packet);

		void CloseStatement(int statementId);

		void SetDatabase(string dbName);

		void Reset();

		IMySqlValue ReadColumnValue(int index, MySqlField field, IMySqlValue valObject);

		void SkipColumnValue(IMySqlValue valueObject);

		void GetColumnsData(MySqlField[] columns);

		void ResetTimeout(int timeout);
	}
	[Serializable]
	public sealed class MySqlException : DbException
	{
		private int errorCode;

		private bool isFatal;

		public int Number => errorCode;

		internal bool IsFatal => isFatal;

		internal bool IsQueryAborted
		{
			get
			{
				if (errorCode != 1317)
				{
					return errorCode == 1028;
				}
				return true;
			}
		}

		internal MySqlException()
		{
		}

		internal MySqlException(string msg)
			: base(msg)
		{
		}

		internal MySqlException(string msg, Exception ex)
			: base(msg, ex)
		{
		}

		internal MySqlException(string msg, bool isFatal, Exception inner)
			: base(msg, inner)
		{
			this.isFatal = isFatal;
		}

		internal MySqlException(string msg, int errno, Exception inner)
			: this(msg, inner)
		{
			errorCode = errno;
			Data.Add("Server Error Code", errno);
		}

		internal MySqlException(string msg, int errno)
			: this(msg, errno, null)
		{
		}

		private MySqlException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	public sealed class MySqlConnectionStringBuilder : DbConnectionStringBuilder
	{
		internal Dictionary<string, object> values = new Dictionary<string, object>();

		private static MySqlConnectionStringOptionCollection options;

		[RefreshProperties(RefreshProperties.All)]
		[Category("Connection")]
		[Description("Server to connect to")]
		public string Server
		{
			get
			{
				return this["server"] as string;
			}
			set
			{
				this["server"] = value;
			}
		}

		[Description("Database to use initially")]
		[Category("Connection")]
		[RefreshProperties(RefreshProperties.All)]
		public string Database
		{
			get
			{
				return values["database"] as string;
			}
			set
			{
				SetValue("database", value);
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		[Category("Connection")]
		[DisplayName("Connection Protocol")]
		[Description("Protocol to use for connection to MySQL")]
		public MySqlConnectionProtocol ConnectionProtocol
		{
			get
			{
				return (MySqlConnectionProtocol)values["protocol"];
			}
			set
			{
				SetValue("protocol", value);
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Pipe Name")]
		[Description("Name of pipe to use when connecting with named pipes (Win32 only)")]
		[Category("Connection")]
		public string PipeName
		{
			get
			{
				return (string)values["pipe"];
			}
			set
			{
				SetValue("pipe", value);
			}
		}

		[Category("Connection")]
		[DisplayName("Use Compression")]
		[RefreshProperties(RefreshProperties.All)]
		[Description("Should the connection use compression")]
		public bool UseCompression
		{
			get
			{
				return (bool)values["compress"];
			}
			set
			{
				SetValue("compress", value);
			}
		}

		[Description("Allows execution of multiple SQL commands in a single statement")]
		[Category("Connection")]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Allow Batch")]
		public bool AllowBatch
		{
			get
			{
				return (bool)values["allowbatch"];
			}
			set
			{
				SetValue("allowbatch", value);
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		[Description("Enables output of diagnostic messages")]
		[Category("Connection")]
		public bool Logging
		{
			get
			{
				return (bool)values["logging"];
			}
			set
			{
				SetValue("logging", value);
			}
		}

		[DisplayName("Shared Memory Name")]
		[Description("Name of the shared memory object to use")]
		[RefreshProperties(RefreshProperties.All)]
		[Category("Connection")]
		public string SharedMemoryName
		{
			get
			{
				return (string)values["sharedmemoryname"];
			}
			set
			{
				SetValue("sharedmemoryname", value);
			}
		}

		[Description("Allows the use of old style @ syntax for parameters")]
		[Category("Connection")]
		[DisplayName("Use Old Syntax")]
		[Obsolete("Use Old Syntax is no longer needed.  See documentation")]
		[RefreshProperties(RefreshProperties.All)]
		public bool UseOldSyntax
		{
			get
			{
				return (bool)values["useoldsyntax"];
			}
			set
			{
				SetValue("useoldsyntax", value);
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		[Category("Connection")]
		[Description("Port to use for TCP/IP connections")]
		public uint Port
		{
			get
			{
				return (uint)values["port"];
			}
			set
			{
				SetValue("port", value);
			}
		}

		[Category("Connection")]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Connect Timeout")]
		[Description("The length of time (in seconds) to wait for a connection to the server before terminating the attempt and generating an error.")]
		public uint ConnectionTimeout
		{
			get
			{
				return (uint)values["connectiontimeout"];
			}
			set
			{
				uint num = Math.Min(value, 2147483u);
				if (num != value)
				{
					MySqlTrace.LogWarning(-1, "Connection timeout value too large (" + value + " seconds). Changed to max. possible value" + num + " seconds)");
				}
				SetValue("connectiontimeout", num);
			}
		}

		[DisplayName("Default Command Timeout")]
		[RefreshProperties(RefreshProperties.All)]
		[Description("The default timeout that MySqlCommand objects will use\r\n                     unless changed.")]
		[Category("Connection")]
		public uint DefaultCommandTimeout
		{
			get
			{
				return (uint)values["defaultcommandtimeout"];
			}
			set
			{
				SetValue("defaultcommandtimeout", value);
			}
		}

		[Category("Security")]
		[DisplayName("User Id")]
		[Description("Indicates the user ID to be used when connecting to the data source.")]
		[RefreshProperties(RefreshProperties.All)]
		public string UserID
		{
			get
			{
				return (string)values["user id"];
			}
			set
			{
				SetValue("user id", value);
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		[Description("Indicates the password to be used when connecting to the data source.")]
		[PasswordPropertyText(true)]
		[Category("Security")]
		public string Password
		{
			get
			{
				return (string)values["password"];
			}
			set
			{
				SetValue("password", value);
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Persist Security Info")]
		[Category("Security")]
		[Description("When false, security-sensitive information, such as the password, is not returned as part of the connection if the connection is open or has ever been in an open state.")]
		public bool PersistSecurityInfo
		{
			get
			{
				return (bool)values["persistsecurityinfo"];
			}
			set
			{
				SetValue("persistsecurityinfo", value);
			}
		}

		[Description("Should the connection use SSL.")]
		[Category("Authentication")]
		[Obsolete("Use Ssl Mode instead.")]
		internal bool Encrypt
		{
			get
			{
				return SslMode != MySqlSslMode.None;
			}
			set
			{
				SetValue("Ssl Mode", value ? MySqlSslMode.Preferred : MySqlSslMode.None);
			}
		}

		[Category("Authentication")]
		[Description("Certificate file in PKCS#12 format (.pfx)")]
		[DisplayName("Certificate File")]
		public string CertificateFile
		{
			get
			{
				return (string)values["certificatefile"];
			}
			set
			{
				SetValue("certificatefile", value);
			}
		}

		[Description("Password for certificate file")]
		[DisplayName("Certificate Password")]
		[Category("Authentication")]
		public string CertificatePassword
		{
			get
			{
				return (string)values["certificatepassword"];
			}
			set
			{
				SetValue("certificatepassword", value);
			}
		}

		[Category("Authentication")]
		[DisplayName("Certificate Store Location")]
		[Description("Certificate Store Location for client certificates")]
		[DefaultValue(MySqlCertificateStoreLocation.None)]
		public MySqlCertificateStoreLocation CertificateStoreLocation
		{
			get
			{
				return (MySqlCertificateStoreLocation)values["certificatestorelocation"];
			}
			set
			{
				SetValue("certificatestorelocation", value);
			}
		}

		[Category("Authentication")]
		[Description("Certificate thumbprint. Can be used together with Certificate Store Location parameter to uniquely identify certificate to be used for SSL authentication.")]
		[DisplayName("Certificate Thumbprint")]
		public string CertificateThumbprint
		{
			get
			{
				return (string)values["certificatethumbprint"];
			}
			set
			{
				SetValue("certificatethumbprint", value);
			}
		}

		[Description("Use windows authentication when connecting to server")]
		[Category("Authentication")]
		[DisplayName("Integrated Security")]
		[DefaultValue(false)]
		public bool IntegratedSecurity
		{
			get
			{
				return (bool)values["integratedsecurity"];
			}
			set
			{
				if (!Platform.IsWindows())
				{
					throw new MySqlException("IntegratedSecurity is supported on Windows only");
				}
				SetValue("integratedsecurity", value);
			}
		}

		[Category("Advanced")]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Allow Zero Datetime")]
		[Description("Should zero datetimes be supported")]
		[DefaultValue(false)]
		public bool AllowZeroDateTime
		{
			get
			{
				return (bool)values["allowzerodatetime"];
			}
			set
			{
				SetValue("allowzerodatetime", value);
			}
		}

		[Description("Should illegal datetime values be converted to DateTime.MinValue")]
		[RefreshProperties(RefreshProperties.All)]
		[Category("Advanced")]
		[DisplayName("Convert Zero Datetime")]
		[DefaultValue(false)]
		public bool ConvertZeroDateTime
		{
			get
			{
				return (bool)values["convertzerodatetime"];
			}
			set
			{
				SetValue("convertzerodatetime", value);
			}
		}

		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		[Category("Advanced")]
		[DisplayName("Use Usage Advisor")]
		[Description("Logs inefficient database operations")]
		public bool UseUsageAdvisor
		{
			get
			{
				return (bool)values["useusageadvisor"];
			}
			set
			{
				SetValue("useusageadvisor", value);
			}
		}

		[Category("Advanced")]
		[DefaultValue(25)]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Procedure Cache Size")]
		[Description("Indicates how many stored procedures can be cached at one time. A value of 0 effectively disables the procedure cache.")]
		public uint ProcedureCacheSize
		{
			get
			{
				return (uint)values["procedurecachesize"];
			}
			set
			{
				SetValue("procedurecachesize", value);
			}
		}

		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		[Category("Advanced")]
		[DisplayName("Use Performance Monitor")]
		[Description("Indicates that performance counters should be updated during execution.")]
		public bool UsePerformanceMonitor
		{
			get
			{
				return (bool)values["useperformancemonitor"];
			}
			set
			{
				SetValue("useperformancemonitor", value);
			}
		}

		[Category("Advanced")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(true)]
		[DisplayName("Ignore Prepare")]
		[Description("Instructs the provider to ignore any attempts to prepare a command.")]
		public bool IgnorePrepare
		{
			get
			{
				return (bool)values["ignoreprepare"];
			}
			set
			{
				SetValue("ignoreprepare", value);
			}
		}

		[Obsolete("Use CheckParameters instead")]
		[Category("Advanced")]
		[DisplayName("Use Procedure Bodies")]
		[Description("Indicates if stored procedure bodies will be available for parameter detection.")]
		[DefaultValue(true)]
		public bool UseProcedureBodies
		{
			get
			{
				return (bool)values["useprocedurebodies"];
			}
			set
			{
				SetValue("useprocedurebodies", value);
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(true)]
		[Category("Advanced")]
		[DisplayName("Auto Enlist")]
		[Description("Should the connetion automatically enlist in the active connection, if there are any.")]
		public bool AutoEnlist
		{
			get
			{
				return (bool)values["autoenlist"];
			}
			set
			{
				SetValue("autoenlist", value);
			}
		}

		[DisplayName("Respect Binary Flags")]
		[RefreshProperties(RefreshProperties.All)]
		[Description("Should binary flags on column metadata be respected.")]
		[DefaultValue(true)]
		[Category("Advanced")]
		public bool RespectBinaryFlags
		{
			get
			{
				return (bool)values["respectbinaryflags"];
			}
			set
			{
				SetValue("respectbinaryflags", value);
			}
		}

		[Category("Advanced")]
		[DefaultValue(true)]
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Treat Tiny As Boolean")]
		[Description("Should the provider treat TINYINT(1) columns as boolean.")]
		public bool TreatTinyAsBoolean
		{
			get
			{
				return (bool)values["treattinyasboolean"];
			}
			set
			{
				SetValue("treattinyasboolean", value);
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		[Category("Advanced")]
		[DisplayName("Allow User Variables")]
		[Description("Should the provider expect user variables to appear in the SQL.")]
		[DefaultValue(false)]
		public bool AllowUserVariables
		{
			get
			{
				return (bool)values["allowuservariables"];
			}
			set
			{
				SetValue("allowuservariables", value);
			}
		}

		[Description("Should this session be considered interactive?")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(false)]
		[Category("Advanced")]
		[DisplayName("Interactive Session")]
		public bool InteractiveSession
		{
			get
			{
				return (bool)values["interactivesession"];
			}
			set
			{
				SetValue("interactivesession", value);
			}
		}

		[Category("Advanced")]
		[Description("Should all server functions be treated as returning string?")]
		[DefaultValue(false)]
		[DisplayName("Functions Return String")]
		public bool FunctionsReturnString
		{
			get
			{
				return (bool)values["functionsreturnstring"];
			}
			set
			{
				SetValue("functionsreturnstring", value);
			}
		}

		[Description("Should the returned affected row count reflect affected rows instead of found rows?")]
		[DisplayName("Use Affected Rows")]
		[Category("Advanced")]
		[DefaultValue(false)]
		public bool UseAffectedRows
		{
			get
			{
				return (bool)values["useaffectedrows"];
			}
			set
			{
				SetValue("useaffectedrows", value);
			}
		}

		[Category("Advanced")]
		[DisplayName("Old Guids")]
		[Description("Treat binary(16) columns as guids")]
		[DefaultValue(false)]
		public bool OldGuids
		{
			get
			{
				return (bool)values["oldguids"];
			}
			set
			{
				SetValue("oldguids", value);
			}
		}

		[DisplayName("Keep Alive")]
		[DefaultValue(0)]
		[Description("For TCP connections, idle connection time measured in seconds, before the first keepalive packet is sent.A value of 0 indicates that keepalive is not used.")]
		public uint Keepalive
		{
			get
			{
				return (uint)values["keepalive"];
			}
			set
			{
				SetValue("keepalive", value);
			}
		}

		[DisplayName("Sql Server Mode")]
		[Description("Allow Sql Server syntax.  A value of yes allows symbols to be enclosed with [] instead of ``.  This does incur a performance hit so only use when necessary.")]
		[DefaultValue(false)]
		[Category("Advanced")]
		public bool SqlServerMode
		{
			get
			{
				return (bool)values["sqlservermode"];
			}
			set
			{
				SetValue("sqlservermode", value);
			}
		}

		[DefaultValue(false)]
		[Description("Enables or disables caching of TableDirect command.  \r\n            A value of yes enables the cache while no disables it.")]
		[Category("Advanced")]
		[DisplayName("Table Cache")]
		public bool TableCaching
		{
			get
			{
				return (bool)values["tablecaching"];
			}
			set
			{
				SetValue("tablecachig", value);
			}
		}

		[DefaultValue(60)]
		[Category("Advanced")]
		[DisplayName("Default Table Cache Age")]
		[Description("Specifies how long a TableDirect result should be cached in seconds.")]
		public int DefaultTableCacheAge
		{
			get
			{
				return (int)values["defaulttablecacheage"];
			}
			set
			{
				SetValue("defaulttablecacheage", value);
			}
		}

		[Category("Advanced")]
		[DefaultValue(true)]
		[DisplayName("Check Parameters")]
		[Description("Indicates if stored routine parameters should be checked against the server.")]
		public bool CheckParameters
		{
			get
			{
				return (bool)values["checkparameters"];
			}
			set
			{
				SetValue("checkparameters", value);
			}
		}

		[Category("Advanced")]
		[Description("Indicates if this connection is to use replicated servers.")]
		[DisplayName("Replication")]
		[DefaultValue(false)]
		public bool Replication
		{
			get
			{
				return (bool)values["replication"];
			}
			set
			{
				SetValue("replication", value);
			}
		}

		[Description("The list of interceptors that can triage thrown MySqlExceptions.")]
		[Category("Advanced")]
		[DisplayName("Exception Interceptors")]
		public string ExceptionInterceptors
		{
			get
			{
				return (string)values["exceptioninterceptors"];
			}
			set
			{
				SetValue("exceptioninterceptors", value);
			}
		}

		[Description("The list of interceptors that can intercept command operations.")]
		[Category("Advanced")]
		[DisplayName("Command Interceptors")]
		public string CommandInterceptors
		{
			get
			{
				return (string)values["commandinterceptors"];
			}
			set
			{
				SetValue("commandinterceptors", value);
			}
		}

		[DefaultValue(false)]
		[DisplayName("Include Security Asserts")]
		[Description("Include security asserts to support Medium Trust")]
		[Category("Advanced")]
		public bool IncludeSecurityAsserts
		{
			get
			{
				return (bool)values["includesecurityasserts"];
			}
			set
			{
				SetValue("includesecurityasserts", value);
			}
		}

		[Category("Pooling")]
		[DisplayName("Connection Lifetime")]
		[Description("The minimum amount of time (in seconds) for this connection to live in the pool before being destroyed.")]
		[DefaultValue(0)]
		[RefreshProperties(RefreshProperties.All)]
		public uint ConnectionLifeTime
		{
			get
			{
				return (uint)values["connectionlifetime"];
			}
			set
			{
				SetValue("connectionlifetime", value);
			}
		}

		[Category("Pooling")]
		[RefreshProperties(RefreshProperties.All)]
		[Description("When true, the connection object is drawn from the appropriate pool, or if necessary, is created and added to the appropriate pool.")]
		[DefaultValue(true)]
		public bool Pooling
		{
			get
			{
				return (bool)values["pooling"];
			}
			set
			{
				SetValue("pooling", value);
			}
		}

		[Description("The minimum number of connections allowed in the pool.")]
		[Category("Pooling")]
		[DisplayName("Minimum Pool Size")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(0)]
		public uint MinimumPoolSize
		{
			get
			{
				return (uint)values["minpoolsize"];
			}
			set
			{
				SetValue("minpoolsize", value);
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		[Category("Pooling")]
		[DisplayName("Maximum Pool Size")]
		[Description("The maximum number of connections allowed in the pool.")]
		[DefaultValue(100)]
		public uint MaximumPoolSize
		{
			get
			{
				return (uint)values["maxpoolsize"];
			}
			set
			{
				SetValue("maxpoolsize", value);
			}
		}

		[Description("When true, indicates the connection state is reset when removed from the pool.")]
		[Category("Pooling")]
		[DisplayName("Connection Reset")]
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		public bool ConnectionReset
		{
			get
			{
				return (bool)values["connectionreset"];
			}
			set
			{
				SetValue("connectionreset", value);
			}
		}

		[Category("Pooling")]
		[DisplayName("Cache Server Properties")]
		[Description("When true, server properties will be cached after the first server in the pool is created")]
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		public bool CacheServerProperties
		{
			get
			{
				return (bool)values["cacheserverproperties"];
			}
			set
			{
				SetValue("cacheserverproperties", value);
			}
		}

		[Description("Character set this connection should use")]
		[DisplayName("Character Set")]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.All)]
		[Category("Advanced")]
		public string CharacterSet
		{
			get
			{
				return (string)values["characterset"];
			}
			set
			{
				SetValue("characterset", value);
			}
		}

		[Description("Should binary blobs be treated as UTF8")]
		[Category("Advanced")]
		[DisplayName("Treat Blobs As UTF8")]
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		public bool TreatBlobsAsUTF8
		{
			get
			{
				return (bool)values["treatblobsasutf8"];
			}
			set
			{
				SetValue("treatblobsasutf8", value);
			}
		}

		[Category("Advanced")]
		[Description("Pattern that matches columns that should be treated as UTF8")]
		[RefreshProperties(RefreshProperties.All)]
		public string BlobAsUTF8IncludePattern
		{
			get
			{
				return (string)values["blobasutf8includepattern"];
			}
			set
			{
				SetValue("blobasutf8includepattern", value);
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		[Category("Advanced")]
		[Description("Pattern that matches columns that should not be treated as UTF8")]
		public string BlobAsUTF8ExcludePattern
		{
			get
			{
				return (string)values["blobasutf8excludepattern"];
			}
			set
			{
				SetValue("blobasutf8excludepattern", value);
			}
		}

		[Category("Security")]
		[Description("SSL properties for connection")]
		[DefaultValue(MySqlSslMode.None)]
		[DisplayName("Ssl Mode")]
		public MySqlSslMode SslMode
		{
			get
			{
				return (MySqlSslMode)values["sslmode"];
			}
			set
			{
				SetValue("sslmode", value);
			}
		}

		[DisplayName("Use Default Command Timeout For EF")]
		[Description("Enforces the command timeout of EFMySqlCommand to the value provided in 'DefaultCommandTimeout' property")]
		[Category("Backwards Compatibility")]
		[DefaultValue(false)]
		public bool UseDefaultCommandTimeoutForEF
		{
			get
			{
				return (bool)values["usedefaultcommandtimeoutforef"];
			}
			set
			{
				SetValue("usedefaultcommandtimeoutforef", value);
			}
		}

		public string FabricGroup { get; internal set; }

		public string ShardingTable { get; internal set; }

		public object ShardingKey { get; internal set; }

		public int? FabricServerMode { get; internal set; }

		public int? FabricScope { get; internal set; }

		internal bool HasProcAccess { get; set; }

		public override object this[string keyword]
		{
			get
			{
				MySqlConnectionStringOption option = GetOption(keyword);
				return option.Getter(this, option);
			}
			set
			{
				MySqlConnectionStringOption option = GetOption(keyword);
				option.Setter(this, option, value);
			}
		}

		static MySqlConnectionStringBuilder()
		{
			options = new MySqlConnectionStringOptionCollection();
			options.Add(new MySqlConnectionStringOption("server", "host,data source,datasource,address,addr,network address", typeof(string), "", obsolete: false));
			options.Add(new MySqlConnectionStringOption("database", "initial catalog", typeof(string), string.Empty, obsolete: false));
			options.Add(new MySqlConnectionStringOption("protocol", "connection protocol, connectionprotocol", typeof(MySqlConnectionProtocol), MySqlConnectionProtocol.Sockets, obsolete: false));
			options.Add(new MySqlConnectionStringOption("port", null, typeof(uint), 3306u, obsolete: false));
			options.Add(new MySqlConnectionStringOption("pipe", "pipe name,pipename", typeof(string), "MYSQL", obsolete: false));
			options.Add(new MySqlConnectionStringOption("compress", "use compression,usecompression", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("allowbatch", "allow batch", typeof(bool), true, obsolete: false));
			options.Add(new MySqlConnectionStringOption("logging", null, typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("sharedmemoryname", "shared memory name", typeof(string), "MYSQL", obsolete: false));
			options.Add(new MySqlConnectionStringOption("useoldsyntax", "old syntax,oldsyntax,use old syntax", typeof(bool), false, obsolete: true, delegate(MySqlConnectionStringBuilder msb, MySqlConnectionStringOption sender, object value)
			{
				MySqlTrace.LogWarning(-1, "Use Old Syntax is now obsolete.  Please see documentation");
				msb.SetValue("useoldsyntax", value);
			}, (MySqlConnectionStringBuilder msb, MySqlConnectionStringOption sender) => (bool)msb.values["useoldsyntax"]));
			options.Add(new MySqlConnectionStringOption("connectiontimeout", "connection timeout,connect timeout", typeof(uint), 15u, obsolete: false, delegate(MySqlConnectionStringBuilder msb, MySqlConnectionStringOption sender, object Value)
			{
				uint num = (uint)Convert.ChangeType(Value, sender.BaseType);
				uint num2 = Math.Min(num, 2147483u);
				if (num2 != num)
				{
					MySqlTrace.LogWarning(-1, "Connection timeout value too large (" + num + " seconds). Changed to max. possible value" + num2 + " seconds)");
				}
				msb.SetValue("connectiontimeout", num2);
			}, (MySqlConnectionStringBuilder msb, MySqlConnectionStringOption sender) => (uint)msb.values["connectiontimeout"]));
			options.Add(new MySqlConnectionStringOption("defaultcommandtimeout", "command timeout,default command timeout", typeof(uint), 30u, obsolete: false));
			options.Add(new MySqlConnectionStringOption("usedefaultcommandtimeoutforef", "use default command timeout for ef", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("user id", "uid,username,user name,user,userid", typeof(string), "", obsolete: false));
			options.Add(new MySqlConnectionStringOption("password", "pwd", typeof(string), "", obsolete: false));
			options.Add(new MySqlConnectionStringOption("persistsecurityinfo", "persist security info", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("encrypt", null, typeof(bool), false, obsolete: true, delegate(MySqlConnectionStringBuilder msb, MySqlConnectionStringOption sender, object value)
			{
				sender.ValidateValue(ref value);
				MySqlTrace.LogWarning(-1, "Encrypt is now obsolete. Use Ssl Mode instead");
				msb.SetValue("Ssl Mode", ((bool)value) ? MySqlSslMode.Preferred : MySqlSslMode.None);
			}, (MySqlConnectionStringBuilder msb, MySqlConnectionStringOption sender) => msb.SslMode != MySqlSslMode.None));
			options.Add(new MySqlConnectionStringOption("certificatefile", "certificate file", typeof(string), null, obsolete: false));
			options.Add(new MySqlConnectionStringOption("certificatepassword", "certificate password", typeof(string), null, obsolete: false));
			options.Add(new MySqlConnectionStringOption("certificatestorelocation", "certificate store location", typeof(MySqlCertificateStoreLocation), MySqlCertificateStoreLocation.None, obsolete: false));
			options.Add(new MySqlConnectionStringOption("certificatethumbprint", "certificate thumb print", typeof(string), null, obsolete: false));
			options.Add(new MySqlConnectionStringOption("integratedsecurity", "integrated security", typeof(bool), false, obsolete: false, delegate(MySqlConnectionStringBuilder msb, MySqlConnectionStringOption sender, object value)
			{
				if (!Platform.IsWindows())
				{
					throw new MySqlException("IntegratedSecurity is supported on Windows only");
				}
				msb.SetValue("Integrated Security", value);
			}, delegate(MySqlConnectionStringBuilder msb, MySqlConnectionStringOption sender)
			{
				object obj = msb.values["Integrated Security"];
				return (bool)obj;
			}));
			options.Add(new MySqlConnectionStringOption("allowzerodatetime", "allow zero datetime", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("convertzerodatetime", "convert zero datetime", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("useusageadvisor", "use usage advisor,usage advisor", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("procedurecachesize", "procedure cache size,procedure cache,procedurecache", typeof(uint), 25u, obsolete: false));
			options.Add(new MySqlConnectionStringOption("useperformancemonitor", "use performance monitor,useperfmon,perfmon", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("ignoreprepare", "ignore prepare", typeof(bool), true, obsolete: false));
			options.Add(new MySqlConnectionStringOption("useprocedurebodies", "use procedure bodies,procedure bodies", typeof(bool), true, obsolete: true, delegate(MySqlConnectionStringBuilder msb, MySqlConnectionStringOption sender, object value)
			{
				sender.ValidateValue(ref value);
				MySqlTrace.LogWarning(-1, "Use Procedure Bodies is now obsolete.  Use Check Parameters instead");
				msb.SetValue("checkparameters", value);
				msb.SetValue("useprocedurebodies", value);
			}, (MySqlConnectionStringBuilder msb, MySqlConnectionStringOption sender) => (bool)msb.values["useprocedurebodies"]));
			options.Add(new MySqlConnectionStringOption("autoenlist", "auto enlist", typeof(bool), true, obsolete: false));
			options.Add(new MySqlConnectionStringOption("respectbinaryflags", "respect binary flags", typeof(bool), true, obsolete: false));
			options.Add(new MySqlConnectionStringOption("treattinyasboolean", "treat tiny as boolean", typeof(bool), true, obsolete: false));
			options.Add(new MySqlConnectionStringOption("allowuservariables", "allow user variables", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("interactivesession", "interactive session,interactive", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("functionsreturnstring", "functions return string", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("useaffectedrows", "use affected rows", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("oldguids", "old guids", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("keepalive", "keep alive", typeof(uint), 0u, obsolete: false));
			options.Add(new MySqlConnectionStringOption("sqlservermode", "sql server mode", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("tablecaching", "table cache,tablecache", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("defaulttablecacheage", "default table cache age", typeof(int), 60, obsolete: false));
			options.Add(new MySqlConnectionStringOption("checkparameters", "check parameters", typeof(bool), true, obsolete: false));
			options.Add(new MySqlConnectionStringOption("replication", null, typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("exceptioninterceptors", "exception interceptors", typeof(string), null, obsolete: false));
			options.Add(new MySqlConnectionStringOption("commandinterceptors", "command interceptors", typeof(string), null, obsolete: false));
			options.Add(new MySqlConnectionStringOption("includesecurityasserts", "include security asserts", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("connectionlifetime", "connection lifetime", typeof(uint), 0u, obsolete: false));
			options.Add(new MySqlConnectionStringOption("pooling", null, typeof(bool), true, obsolete: false));
			options.Add(new MySqlConnectionStringOption("minpoolsize", "minimumpoolsize,min pool size,minimum pool size", typeof(uint), 0u, obsolete: false));
			options.Add(new MySqlConnectionStringOption("maxpoolsize", "maximumpoolsize,max pool size,maximum pool size", typeof(uint), 100u, obsolete: false));
			options.Add(new MySqlConnectionStringOption("connectionreset", "connection reset", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("cacheserverproperties", "cache server properties", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("characterset", "character set,charset", typeof(string), "", obsolete: false));
			options.Add(new MySqlConnectionStringOption("treatblobsasutf8", "treat blobs as utf8", typeof(bool), false, obsolete: false));
			options.Add(new MySqlConnectionStringOption("blobasutf8includepattern", null, typeof(string), "", obsolete: false));
			options.Add(new MySqlConnectionStringOption("blobasutf8excludepattern", null, typeof(string), "", obsolete: false));
			options.Add(new MySqlConnectionStringOption("sslmode", "ssl mode", typeof(MySqlSslMode), MySqlSslMode.None, obsolete: false));
		}

		public MySqlConnectionStringBuilder()
		{
			HasProcAccess = true;
			lock (this)
			{
				for (int i = 0; i < options.Options.Count; i++)
				{
					values[options.Options[i].Keyword] = options.Options[i].DefaultValue;
				}
			}
		}

		public MySqlConnectionStringBuilder(string connStr)
		{
			lock (this)
			{
				base.ConnectionString = connStr;
			}
		}

		internal Regex GetBlobAsUTF8IncludeRegex()
		{
			if (string.IsNullOrEmpty(BlobAsUTF8IncludePattern))
			{
				return null;
			}
			return new Regex(BlobAsUTF8IncludePattern);
		}

		internal Regex GetBlobAsUTF8ExcludeRegex()
		{
			if (string.IsNullOrEmpty(BlobAsUTF8ExcludePattern))
			{
				return null;
			}
			return new Regex(BlobAsUTF8ExcludePattern);
		}

		public override void Clear()
		{
			base.Clear();
			lock (this)
			{
				foreach (MySqlConnectionStringOption option in options.Options)
				{
					if (option.DefaultValue != null)
					{
						values[option.Keyword] = option.DefaultValue;
					}
					else
					{
						values[option.Keyword] = null;
					}
				}
			}
		}

		internal void SetValue(string keyword, object value)
		{
			MySqlConnectionStringOption option = GetOption(keyword);
			option.ValidateValue(ref value);
			option.Clean(this);
			if (value != null)
			{
				lock (this)
				{
					values[option.Keyword] = value;
					base[keyword] = value;
				}
			}
		}

		private MySqlConnectionStringOption GetOption(string key)
		{
			MySqlConnectionStringOption mySqlConnectionStringOption = options.Get(key);
			if (mySqlConnectionStringOption == null)
			{
				throw new ArgumentException(Resources.KeywordNotSupported, key);
			}
			return mySqlConnectionStringOption;
		}

		public override bool ContainsKey(string keyword)
		{
			MySqlConnectionStringOption mySqlConnectionStringOption = options.Get(keyword);
			return mySqlConnectionStringOption != null;
		}

		public override bool Remove(string keyword)
		{
			bool flag = false;
			lock (this)
			{
				flag = base.Remove(keyword);
			}
			if (!flag)
			{
				return false;
			}
			MySqlConnectionStringOption option = GetOption(keyword);
			lock (this)
			{
				values[option.Keyword] = option.DefaultValue;
			}
			return true;
		}

		public string GetConnectionString(bool includePass)
		{
			if (includePass)
			{
				return base.ConnectionString;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (string key in Keys)
			{
				if (string.Compare(key, "password", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(key, "pwd", StringComparison.OrdinalIgnoreCase) != 0)
				{
					stringBuilder.AppendFormat(CultureInfo.CurrentCulture, "{0}{1}={2}", new object[3]
					{
						text,
						key,
						this[key]
					});
					text = ";";
				}
			}
			return stringBuilder.ToString();
		}

		public override bool Equals(object obj)
		{
			MySqlConnectionStringBuilder mySqlConnectionStringBuilder = obj as MySqlConnectionStringBuilder;
			if (obj == null)
			{
				return false;
			}
			if (values.Count != mySqlConnectionStringBuilder.values.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, object> value in values)
			{
				if (mySqlConnectionStringBuilder.values.ContainsKey(value.Key))
				{
					object obj2 = mySqlConnectionStringBuilder.values[value.Key];
					if (obj2 == null && value.Value != null)
					{
						return false;
					}
					if (value.Value == null && obj2 != null)
					{
						return false;
					}
					if (value.Value == null && obj2 == null)
					{
						return true;
					}
					if (!obj2.Equals(value.Value))
					{
						return false;
					}
					continue;
				}
				return false;
			}
			return true;
		}
	}
}
namespace MySql.Data.Types
{
	internal interface IMySqlValue
	{
		bool IsNull { get; }

		MySqlDbType MySqlDbType { get; }

		object Value { get; }

		Type SystemType { get; }

		string MySqlTypeName { get; }

		void WriteValue(MySqlPacket packet, bool binary, object value, int length);

		IMySqlValue ReadValue(MySqlPacket packet, long length, bool isNull);

		void SkipValue(MySqlPacket packet);
	}
	[Serializable]
	public struct MySqlDateTime : IConvertible, IMySqlValue, IComparable
	{
		private bool isNull;

		private MySqlDbType type;

		private int year;

		private int month;

		private int day;

		private int hour;

		private int minute;

		private int second;

		private int millisecond;

		private int microsecond;

		public int TimezoneOffset;

		public bool IsValidDateTime
		{
			get
			{
				if (year != 0 && month != 0)
				{
					return day != 0;
				}
				return false;
			}
		}

		public int Year
		{
			get
			{
				return year;
			}
			set
			{
				year = value;
			}
		}

		public int Month
		{
			get
			{
				return month;
			}
			set
			{
				month = value;
			}
		}

		public int Day
		{
			get
			{
				return day;
			}
			set
			{
				day = value;
			}
		}

		public int Hour
		{
			get
			{
				return hour;
			}
			set
			{
				hour = value;
			}
		}

		public int Minute
		{
			get
			{
				return minute;
			}
			set
			{
				minute = value;
			}
		}

		public int Second
		{
			get
			{
				return second;
			}
			set
			{
				second = value;
			}
		}

		public int Millisecond
		{
			get
			{
				return millisecond;
			}
			set
			{
				if (value < 0 || value > 999)
				{
					throw new ArgumentOutOfRangeException("Millisecond", Resources.InvalidMillisecondValue);
				}
				millisecond = value;
				microsecond = value * 1000;
			}
		}

		public int Microsecond
		{
			get
			{
				return microsecond;
			}
			set
			{
				if (value < 0 || value > 999999)
				{
					throw new ArgumentOutOfRangeException("Microsecond", Resources.InvalidMicrosecondValue);
				}
				microsecond = value;
				millisecond = value / 1000;
			}
		}

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => type;

		object IMySqlValue.Value => GetDateTime();

		public DateTime Value => GetDateTime();

		Type IMySqlValue.SystemType => typeof(DateTime);

		string IMySqlValue.MySqlTypeName => type switch
		{
			MySqlDbType.Date => "DATE", 
			MySqlDbType.Newdate => "NEWDATE", 
			MySqlDbType.Timestamp => "TIMESTAMP", 
			_ => "DATETIME", 
		};

		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return 0uL;
		}

		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return 0;
		}

		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return 0.0;
		}

		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return GetDateTime();
		}

		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return 0f;
		}

		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return false;
		}

		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return 0;
		}

		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return 0;
		}

		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return 0;
		}

		string IConvertible.ToString(IFormatProvider provider)
		{
			return null;
		}

		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return 0;
		}

		char IConvertible.ToChar(IFormatProvider provider)
		{
			return '\0';
		}

		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return 0L;
		}

		TypeCode IConvertible.GetTypeCode()
		{
			return TypeCode.Empty;
		}

		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return 0m;
		}

		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return null;
		}

		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return 0u;
		}

		public MySqlDateTime(int year, int month, int day, int hour, int minute, int second, int microsecond)
			: this(MySqlDbType.DateTime, year, month, day, hour, minute, second, microsecond)
		{
		}

		public MySqlDateTime(DateTime dt)
			: this(MySqlDbType.DateTime, dt)
		{
		}

		public MySqlDateTime(MySqlDateTime mdt)
		{
			year = mdt.Year;
			month = mdt.Month;
			day = mdt.Day;
			hour = mdt.Hour;
			minute = mdt.Minute;
			second = mdt.Second;
			microsecond = 0;
			millisecond = 0;
			type = MySqlDbType.DateTime;
			isNull = false;
			TimezoneOffset = 0;
		}

		public MySqlDateTime(string dateTime)
			: this(Parse(dateTime))
		{
		}

		internal MySqlDateTime(MySqlDbType type, int year, int month, int day, int hour, int minute, int second, int microsecond)
		{
			isNull = false;
			this.type = type;
			this.year = year;
			this.month = month;
			this.day = day;
			this.hour = hour;
			this.minute = minute;
			this.second = second;
			this.microsecond = microsecond;
			millisecond = this.microsecond / 1000;
			TimezoneOffset = 0;
		}

		internal MySqlDateTime(MySqlDbType type, bool isNull)
			: this(type, 0, 0, 0, 0, 0, 0, 0)
		{
			this.isNull = isNull;
		}

		internal MySqlDateTime(MySqlDbType type, DateTime val)
			: this(type, 0, 0, 0, 0, 0, 0, 0)
		{
			isNull = false;
			year = val.Year;
			month = val.Month;
			day = val.Day;
			hour = val.Hour;
			minute = val.Minute;
			second = val.Second;
			Microsecond = (int)(val.Ticks % 10000000) / 10;
		}

		private void SerializeText(MySqlPacket packet, MySqlDateTime value)
		{
			string empty = string.Empty;
			empty = $"{value.Year:0000}-{value.Month:00}-{value.Day:00}";
			if (type != MySqlDbType.Date)
			{
				empty = ((value.Microsecond > 0) ? $"{empty} {value.Hour:00}:{value.Minute:00}:{value.Second:00}.{value.Microsecond:000000}" : $"{empty} {value.Hour:00}:{value.Minute:00}:{value.Second:00} ");
			}
			packet.WriteStringNoNull("'" + empty + "'");
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object value, int length)
		{
			string text = value as string;
			MySqlDateTime value2;
			if (value is DateTime)
			{
				value2 = new MySqlDateTime(type, (DateTime)value);
			}
			else if (text != null)
			{
				value2 = Parse(text);
			}
			else
			{
				if (!(value is MySqlDateTime))
				{
					throw new MySqlException("Unable to serialize date/time value.");
				}
				value2 = (MySqlDateTime)value;
			}
			if (!binary)
			{
				SerializeText(packet, value2);
				return;
			}
			if (value2.Microsecond > 0)
			{
				packet.WriteByte(11);
			}
			else
			{
				packet.WriteByte(7);
			}
			packet.WriteInteger(value2.Year, 2);
			packet.WriteByte((byte)value2.Month);
			packet.WriteByte((byte)value2.Day);
			if (type == MySqlDbType.Date)
			{
				packet.WriteByte(0);
				packet.WriteByte(0);
				packet.WriteByte(0);
			}
			else
			{
				packet.WriteByte((byte)value2.Hour);
				packet.WriteByte((byte)value2.Minute);
				packet.WriteByte((byte)value2.Second);
			}
			if (value2.Microsecond > 0)
			{
				long num = value2.Microsecond;
				for (int i = 0; i < 4; i++)
				{
					packet.WriteByte((byte)(num & 0xFF));
					num >>= 8;
				}
			}
		}

		internal static MySqlDateTime Parse(string s)
		{
			return default(MySqlDateTime).ParseMySql(s);
		}

		internal static MySqlDateTime Parse(string s, DBVersion version)
		{
			return default(MySqlDateTime).ParseMySql(s);
		}

		private MySqlDateTime ParseMySql(string s)
		{
			string[] array = s.Split('-', ' ', ':', '/', '.');
			int num = int.Parse(array[0]);
			int num2 = int.Parse(array[1]);
			int num3 = int.Parse(array[2]);
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			if (array.Length > 3)
			{
				num4 = int.Parse(array[3]);
				num5 = int.Parse(array[4]);
				num6 = int.Parse(array[5]);
			}
			if (array.Length > 6)
			{
				num7 = int.Parse(array[6].PadRight(6, '0'));
			}
			return new MySqlDateTime(type, num, num2, num3, num4, num5, num6, num7);
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			if (nullVal)
			{
				return new MySqlDateTime(type, isNull: true);
			}
			if (length >= 0)
			{
				string s = packet.ReadString(length);
				return ParseMySql(s);
			}
			long num = packet.ReadByte();
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			if (num >= 4)
			{
				num2 = packet.ReadInteger(2);
				num3 = packet.ReadByte();
				num4 = packet.ReadByte();
			}
			if (num > 4)
			{
				num5 = packet.ReadByte();
				num6 = packet.ReadByte();
				num7 = packet.ReadByte();
			}
			if (num > 7)
			{
				num8 = packet.Read3ByteInt();
				packet.ReadByte();
			}
			return new MySqlDateTime(type, num2, num3, num4, num5, num6, num7, num8);
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			int num = packet.ReadByte();
			packet.Position += num;
		}

		public DateTime GetDateTime()
		{
			if (!IsValidDateTime)
			{
				throw new MySqlConversionException("Unable to convert MySQL date/time value to System.DateTime");
			}
			DateTimeKind kind = DateTimeKind.Unspecified;
			if (type == MySqlDbType.Timestamp)
			{
				kind = ((TimezoneOffset == 0) ? DateTimeKind.Utc : DateTimeKind.Local);
			}
			return new DateTime(year, month, day, hour, minute, second, kind).AddTicks(microsecond * 10);
		}

		private static string FormatDateCustom(string format, int monthVal, int dayVal, int yearVal)
		{
			format = format.Replace("MM", "{0:00}");
			format = format.Replace("M", "{0}");
			format = format.Replace("dd", "{1:00}");
			format = format.Replace("d", "{1}");
			format = format.Replace("yyyy", "{2:0000}");
			format = format.Replace("yy", "{3:00}");
			format = format.Replace("y", "{4:0}");
			int num = yearVal - yearVal / 1000 * 1000;
			num -= num / 100 * 100;
			int num2 = num - num / 10 * 10;
			return string.Format(format, monthVal, dayVal, yearVal, num, num2);
		}

		public override string ToString()
		{
			if (IsValidDateTime)
			{
				DateTime dateTime = new DateTime(year, month, day, hour, minute, second).AddTicks(microsecond * 10);
				if (type != MySqlDbType.Date)
				{
					return dateTime.ToString();
				}
				return dateTime.ToString("d");
			}
			string text = FormatDateCustom(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern, month, day, year);
			if (type == MySqlDbType.Date)
			{
				return text;
			}
			return $"{text} {new DateTime(1, 2, 3, hour, minute, second).AddTicks(microsecond * 10).ToLongTimeString()}";
		}

		public static explicit operator DateTime(MySqlDateTime val)
		{
			if (!val.IsValidDateTime)
			{
				return DateTime.MinValue;
			}
			return val.GetDateTime();
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			string[] array = new string[3] { "DATE", "DATETIME", "TIMESTAMP" };
			MySqlDbType[] array2 = new MySqlDbType[3]
			{
				MySqlDbType.Date,
				MySqlDbType.DateTime,
				MySqlDbType.Timestamp
			};
			for (int i = 0; i < array.Length; i++)
			{
				MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
				mySqlSchemaRow["TypeName"] = array[i];
				mySqlSchemaRow["ProviderDbType"] = array2[i];
				mySqlSchemaRow["ColumnSize"] = 0;
				mySqlSchemaRow["CreateFormat"] = array[i];
				mySqlSchemaRow["CreateParameters"] = null;
				mySqlSchemaRow["DataType"] = "System.DateTime";
				mySqlSchemaRow["IsAutoincrementable"] = false;
				mySqlSchemaRow["IsBestMatch"] = true;
				mySqlSchemaRow["IsCaseSensitive"] = false;
				mySqlSchemaRow["IsFixedLength"] = true;
				mySqlSchemaRow["IsFixedPrecisionScale"] = true;
				mySqlSchemaRow["IsLong"] = false;
				mySqlSchemaRow["IsNullable"] = true;
				mySqlSchemaRow["IsSearchable"] = true;
				mySqlSchemaRow["IsSearchableWithLike"] = false;
				mySqlSchemaRow["IsUnsigned"] = false;
				mySqlSchemaRow["MaximumScale"] = 0;
				mySqlSchemaRow["MinimumScale"] = 0;
				mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
				mySqlSchemaRow["IsLiteralSupported"] = false;
				mySqlSchemaRow["LiteralPrefix"] = null;
				mySqlSchemaRow["LiteralSuffix"] = null;
				mySqlSchemaRow["NativeDataType"] = null;
			}
		}

		int IComparable.CompareTo(object obj)
		{
			MySqlDateTime mySqlDateTime = (MySqlDateTime)obj;
			if (Year < mySqlDateTime.Year)
			{
				return -1;
			}
			if (Year > mySqlDateTime.Year)
			{
				return 1;
			}
			if (Month < mySqlDateTime.Month)
			{
				return -1;
			}
			if (Month > mySqlDateTime.Month)
			{
				return 1;
			}
			if (Day < mySqlDateTime.Day)
			{
				return -1;
			}
			if (Day > mySqlDateTime.Day)
			{
				return 1;
			}
			if (Hour < mySqlDateTime.Hour)
			{
				return -1;
			}
			if (Hour > mySqlDateTime.Hour)
			{
				return 1;
			}
			if (Minute < mySqlDateTime.Minute)
			{
				return -1;
			}
			if (Minute > mySqlDateTime.Minute)
			{
				return 1;
			}
			if (Second < mySqlDateTime.Second)
			{
				return -1;
			}
			if (Second > mySqlDateTime.Second)
			{
				return 1;
			}
			if (Microsecond < mySqlDateTime.Microsecond)
			{
				return -1;
			}
			if (Microsecond > mySqlDateTime.Microsecond)
			{
				return 1;
			}
			return 0;
		}
	}
}
namespace MySql.Data.MySqlClient
{
	[TypeConverter(typeof(MySqlParameterConverter))]
	public sealed class MySqlParameter : DbParameter, IDbDataParameter, IDataParameter, ICloneable
	{
		private const int UNSIGNED_MASK = 32768;

		private const int GEOMETRY_LENGTH = 25;

		private DbType dbType;

		private object paramValue;

		private string paramName;

		private MySqlDbType mySqlDbType;

		private bool inferType = true;

		private IMySqlValue _valueObject;

		[Category("Data")]
		public override DataRowVersion SourceVersion { get; set; }

		[Category("Data")]
		public override string SourceColumn { get; set; }

		public override bool SourceColumnNullMapping { get; set; }

		public override DbType DbType
		{
			get
			{
				return dbType;
			}
			set
			{
				SetDbType(value);
				inferType = false;
			}
		}

		[Category("Misc")]
		public override string ParameterName
		{
			get
			{
				return paramName;
			}
			set
			{
				SetParameterName(value);
			}
		}

		internal MySqlParameterCollection Collection { get; set; }

		internal Encoding Encoding { get; set; }

		internal bool TypeHasBeenSet => !inferType;

		internal string BaseName
		{
			get
			{
				if (ParameterName.StartsWith("@", StringComparison.Ordinal) || ParameterName.StartsWith("?", StringComparison.Ordinal))
				{
					return ParameterName.Substring(1);
				}
				return ParameterName;
			}
		}

		[Category("Data")]
		public override ParameterDirection Direction { get; set; }

		[Browsable(false)]
		public override bool IsNullable { get; set; }

		[Category("Data")]
		[DbProviderSpecificTypeProperty(true)]
		public MySqlDbType MySqlDbType
		{
			get
			{
				return mySqlDbType;
			}
			set
			{
				SetMySqlDbType(value);
				inferType = false;
			}
		}

		[Category("Data")]
		public new byte Precision { get; set; }

		[Category("Data")]
		public new byte Scale { get; set; }

		[Category("Data")]
		public override int Size { get; set; }

		[Category("Data")]
		[TypeConverter(typeof(StringConverter))]
		public override object Value
		{
			get
			{
				return paramValue;
			}
			set
			{
				paramValue = value;
				byte[] array = value as byte[];
				string text = value as string;
				if (array != null)
				{
					Size = array.Length;
				}
				else if (text != null)
				{
					Size = text.Length;
				}
				if (inferType)
				{
					SetTypeFromValue();
				}
			}
		}

		internal IMySqlValue ValueObject
		{
			get
			{
				return _valueObject;
			}
			private set
			{
				_valueObject = value;
			}
		}

		public IList PossibleValues { get; internal set; }

		public MySqlParameter(string parameterName, MySqlDbType dbType, int size, string sourceColumn)
			: this(parameterName, dbType)
		{
			Size = size;
			Direction = ParameterDirection.Input;
			SourceColumn = sourceColumn;
			SourceVersion = DataRowVersion.Current;
		}

		public MySqlParameter(string parameterName, MySqlDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
			: this(parameterName, dbType, size, sourceColumn)
		{
			Direction = direction;
			SourceVersion = sourceVersion;
			IsNullable = isNullable;
			Precision = precision;
			Scale = scale;
			Value = value;
		}

		internal MySqlParameter(string name, MySqlDbType type, ParameterDirection dir, string col, DataRowVersion ver, object val)
			: this(name, type)
		{
			Direction = dir;
			SourceColumn = col;
			SourceVersion = ver;
			Value = val;
		}

		private void Init()
		{
			SourceVersion = DataRowVersion.Current;
			Direction = ParameterDirection.Input;
		}

		public override void ResetDbType()
		{
			inferType = true;
		}

		private void SetDbTypeFromMySqlDbType()
		{
			switch (mySqlDbType)
			{
			case MySqlDbType.Decimal:
			case MySqlDbType.NewDecimal:
				dbType = DbType.Decimal;
				break;
			case MySqlDbType.Byte:
				dbType = DbType.SByte;
				break;
			case MySqlDbType.UByte:
				dbType = DbType.Byte;
				break;
			case MySqlDbType.Int16:
				dbType = DbType.Int16;
				break;
			case MySqlDbType.UInt16:
				dbType = DbType.UInt16;
				break;
			case MySqlDbType.Int32:
			case MySqlDbType.Int24:
				dbType = DbType.Int32;
				break;
			case MySqlDbType.UInt32:
			case MySqlDbType.UInt24:
				dbType = DbType.UInt32;
				break;
			case MySqlDbType.Int64:
				dbType = DbType.Int64;
				break;
			case MySqlDbType.UInt64:
				dbType = DbType.UInt64;
				break;
			case MySqlDbType.Bit:
				dbType = DbType.UInt64;
				break;
			case MySqlDbType.Float:
				dbType = DbType.Single;
				break;
			case MySqlDbType.Double:
				dbType = DbType.Double;
				break;
			case MySqlDbType.Timestamp:
			case MySqlDbType.DateTime:
				dbType = DbType.DateTime;
				break;
			case MySqlDbType.Date:
			case MySqlDbType.Year:
			case MySqlDbType.Newdate:
				dbType = DbType.Date;
				break;
			case MySqlDbType.Time:
				dbType = DbType.Time;
				break;
			case MySqlDbType.Enum:
			case MySqlDbType.Set:
			case MySqlDbType.VarChar:
				dbType = DbType.String;
				break;
			case MySqlDbType.TinyBlob:
			case MySqlDbType.MediumBlob:
			case MySqlDbType.LongBlob:
			case MySqlDbType.Blob:
				dbType = DbType.Object;
				break;
			case MySqlDbType.String:
				dbType = DbType.StringFixedLength;
				break;
			case MySqlDbType.Guid:
				dbType = DbType.Guid;
				break;
			}
		}

		private void SetDbType(DbType db_type)
		{
			dbType = db_type;
			switch (dbType)
			{
			case DbType.Guid:
				mySqlDbType = MySqlDbType.Guid;
				break;
			case DbType.AnsiString:
			case DbType.String:
				mySqlDbType = MySqlDbType.VarChar;
				break;
			case DbType.AnsiStringFixedLength:
			case DbType.StringFixedLength:
				mySqlDbType = MySqlDbType.String;
				break;
			case DbType.Byte:
			case DbType.Boolean:
				mySqlDbType = MySqlDbType.UByte;
				break;
			case DbType.SByte:
				mySqlDbType = MySqlDbType.Byte;
				break;
			case DbType.Date:
				mySqlDbType = MySqlDbType.Date;
				break;
			case DbType.DateTime:
				mySqlDbType = MySqlDbType.DateTime;
				break;
			case DbType.Time:
				mySqlDbType = MySqlDbType.Time;
				break;
			case DbType.Single:
				mySqlDbType = MySqlDbType.Float;
				break;
			case DbType.Double:
				mySqlDbType = MySqlDbType.Double;
				break;
			case DbType.Int16:
				mySqlDbType = MySqlDbType.Int16;
				break;
			case DbType.UInt16:
				mySqlDbType = MySqlDbType.UInt16;
				break;
			case DbType.Int32:
				mySqlDbType = MySqlDbType.Int32;
				break;
			case DbType.UInt32:
				mySqlDbType = MySqlDbType.UInt32;
				break;
			case DbType.Int64:
				mySqlDbType = MySqlDbType.Int64;
				break;
			case DbType.UInt64:
				mySqlDbType = MySqlDbType.UInt64;
				break;
			case DbType.Currency:
			case DbType.Decimal:
				mySqlDbType = MySqlDbType.Decimal;
				break;
			default:
				mySqlDbType = MySqlDbType.Blob;
				break;
			}
			if (dbType == DbType.Object && paramValue is byte[] array && array.Length == 25)
			{
				mySqlDbType = MySqlDbType.Geometry;
			}
			ValueObject = MySqlField.GetIMySqlValue(mySqlDbType);
		}

		public MySqlParameter()
		{
			Init();
		}

		public MySqlParameter(string parameterName, object value)
			: this()
		{
			ParameterName = parameterName;
			Value = value;
		}

		public MySqlParameter(string parameterName, MySqlDbType dbType)
			: this(parameterName, null)
		{
			MySqlDbType = dbType;
		}

		public MySqlParameter(string parameterName, MySqlDbType dbType, int size)
			: this(parameterName, dbType)
		{
			Size = size;
		}

		private void SetParameterName(string name)
		{
			if (Collection != null)
			{
				Collection.ParameterNameChanged(this, paramName, name);
			}
			paramName = name;
		}

		public override string ToString()
		{
			return paramName;
		}

		internal int GetPSType()
		{
			return mySqlDbType switch
			{
				MySqlDbType.Bit => 32776, 
				MySqlDbType.UByte => 32769, 
				MySqlDbType.UInt64 => 32776, 
				MySqlDbType.UInt32 => 32771, 
				MySqlDbType.UInt24 => 32771, 
				MySqlDbType.UInt16 => 32770, 
				_ => (int)mySqlDbType, 
			};
		}

		internal void Serialize(MySqlPacket packet, bool binary, MySqlConnectionStringBuilder settings)
		{
			if (!binary && (paramValue == null || paramValue == DBNull.Value))
			{
				packet.WriteStringNoNull("NULL");
				return;
			}
			if (ValueObject.MySqlDbType == MySqlDbType.Guid)
			{
				MySqlGuid mySqlGuid = (MySqlGuid)(object)ValueObject;
				mySqlGuid.OldGuids = settings.OldGuids;
				ValueObject = mySqlGuid;
			}
			if (ValueObject.MySqlDbType == MySqlDbType.Geometry)
			{
				MySqlGeometry mySqlGeometryValue = (MySqlGeometry)(object)ValueObject;
				if (mySqlGeometryValue.IsNull && Value != null)
				{
					MySqlGeometry.TryParse(Value.ToString(), out mySqlGeometryValue);
				}
				ValueObject = mySqlGeometryValue;
			}
			ValueObject.WriteValue(packet, binary, paramValue, Size);
		}

		private void SetMySqlDbType(MySqlDbType mysql_dbtype)
		{
			mySqlDbType = mysql_dbtype;
			ValueObject = MySqlField.GetIMySqlValue(mySqlDbType);
			SetDbTypeFromMySqlDbType();
		}

		private void SetTypeFromValue()
		{
			if (paramValue == null || paramValue == DBNull.Value)
			{
				return;
			}
			if (paramValue is Guid)
			{
				MySqlDbType = MySqlDbType.Guid;
				return;
			}
			if (paramValue is TimeSpan)
			{
				MySqlDbType = MySqlDbType.Time;
				return;
			}
			if (paramValue is bool)
			{
				MySqlDbType = MySqlDbType.Byte;
				return;
			}
			Type type = paramValue.GetType();
			switch (type.Name)
			{
			case "SByte":
				MySqlDbType = MySqlDbType.Byte;
				return;
			case "Byte":
				MySqlDbType = MySqlDbType.UByte;
				return;
			case "Int16":
				MySqlDbType = MySqlDbType.Int16;
				return;
			case "UInt16":
				MySqlDbType = MySqlDbType.UInt16;
				return;
			case "Int32":
				MySqlDbType = MySqlDbType.Int32;
				return;
			case "UInt32":
				MySqlDbType = MySqlDbType.UInt32;
				return;
			case "Int64":
				MySqlDbType = MySqlDbType.Int64;
				return;
			case "UInt64":
				MySqlDbType = MySqlDbType.UInt64;
				return;
			case "DateTime":
				MySqlDbType = MySqlDbType.DateTime;
				return;
			case "String":
				MySqlDbType = MySqlDbType.VarChar;
				return;
			case "Single":
				MySqlDbType = MySqlDbType.Float;
				return;
			case "Double":
				MySqlDbType = MySqlDbType.Double;
				return;
			case "Decimal":
				MySqlDbType = MySqlDbType.Decimal;
				return;
			}
			if ((object)type.BaseType == typeof(Enum))
			{
				MySqlDbType = MySqlDbType.Int32;
			}
			else
			{
				MySqlDbType = MySqlDbType.Blob;
			}
		}

		public MySqlParameter Clone()
		{
			MySqlParameter mySqlParameter = new MySqlParameter(paramName, mySqlDbType, Direction, SourceColumn, SourceVersion, paramValue);
			mySqlParameter.inferType = inferType;
			return mySqlParameter;
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		internal long EstimatedSize()
		{
			if (Value == null || Value == DBNull.Value)
			{
				return 4L;
			}
			if (Value is byte[])
			{
				return (Value as byte[]).Length;
			}
			if (Value is string)
			{
				return (Value as string).Length * 4;
			}
			if (Value is decimal || Value is float)
			{
				return 64L;
			}
			return 32L;
		}
	}
	internal class MySqlParameterConverter : TypeConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if ((object)destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if ((object)destinationType == typeof(InstanceDescriptor))
			{
				ConstructorInfo constructor = typeof(MySqlParameter).GetConstructor(new Type[10]
				{
					typeof(string),
					typeof(MySqlDbType),
					typeof(int),
					typeof(ParameterDirection),
					typeof(bool),
					typeof(byte),
					typeof(byte),
					typeof(string),
					typeof(DataRowVersion),
					typeof(object)
				});
				MySqlParameter mySqlParameter = (MySqlParameter)value;
				return new InstanceDescriptor(constructor, new object[10] { mySqlParameter.ParameterName, mySqlParameter.DbType, mySqlParameter.Size, mySqlParameter.Direction, mySqlParameter.IsNullable, mySqlParameter.Precision, mySqlParameter.Scale, mySqlParameter.SourceColumn, mySqlParameter.SourceVersion, mySqlParameter.Value });
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
	[ListBindable(true)]
	[Editor("MySql.Data.MySqlClient.Design.DBParametersEditor,MySql.Design", typeof(UITypeEditor))]
	public sealed class MySqlParameterCollection : DbParameterCollection
	{
		private List<MySqlParameter> items = new List<MySqlParameter>();

		private Dictionary<string, int> indexHashCS;

		private Dictionary<string, int> indexHashCI;

		internal bool containsUnnamedParameters;

		public override bool IsFixedSize => ((IList)items).IsFixedSize;

		public override bool IsReadOnly => ((IList)items).IsReadOnly;

		public override bool IsSynchronized => ((ICollection)items).IsSynchronized;

		public override object SyncRoot => ((ICollection)items).SyncRoot;

		public override int Count => items.Count;

		public new MySqlParameter this[int index]
		{
			get
			{
				return InternalGetParameter(index);
			}
			set
			{
				InternalSetParameter(index, value);
			}
		}

		public new MySqlParameter this[string name]
		{
			get
			{
				return InternalGetParameter(name);
			}
			set
			{
				InternalSetParameter(name, value);
			}
		}

		public MySqlParameter Add(string parameterName, MySqlDbType dbType, int size, string sourceColumn)
		{
			return Add(new MySqlParameter(parameterName, dbType, size, sourceColumn));
		}

		public override void AddRange(Array values)
		{
			foreach (DbParameter value in values)
			{
				Add(value);
			}
		}

		protected override DbParameter GetParameter(string parameterName)
		{
			return InternalGetParameter(parameterName);
		}

		protected override DbParameter GetParameter(int index)
		{
			return InternalGetParameter(index);
		}

		protected override void SetParameter(string parameterName, DbParameter value)
		{
			InternalSetParameter(parameterName, value as MySqlParameter);
		}

		protected override void SetParameter(int index, DbParameter value)
		{
			InternalSetParameter(index, value as MySqlParameter);
		}

		public override int Add(object value)
		{
			if (!(value is MySqlParameter value2))
			{
				throw new MySqlException("Only MySqlParameter objects may be stored");
			}
			MySqlParameter value3 = Add(value2);
			return IndexOf(value3);
		}

		public override bool Contains(string parameterName)
		{
			return IndexOf(parameterName) != -1;
		}

		public override bool Contains(object value)
		{
			if (!(value is MySqlParameter item))
			{
				throw new ArgumentException("Argument must be of type DbParameter", "value");
			}
			return items.Contains(item);
		}

		public override void CopyTo(Array array, int index)
		{
			items.ToArray().CopyTo(array, index);
		}

		public override IEnumerator GetEnumerator()
		{
			return items.GetEnumerator();
		}

		public override void Insert(int index, object value)
		{
			if (!(value is MySqlParameter value2))
			{
				throw new MySqlException("Only MySqlParameter objects may be stored");
			}
			InternalAdd(value2, index);
		}

		public override void Remove(object value)
		{
			MySqlParameter mySqlParameter = value as MySqlParameter;
			mySqlParameter.Collection = null;
			int keyIndex = IndexOf(mySqlParameter);
			items.Remove(mySqlParameter);
			indexHashCS.Remove(mySqlParameter.ParameterName);
			indexHashCI.Remove(mySqlParameter.ParameterName);
			AdjustHashes(keyIndex, addEntry: false);
		}

		public override void RemoveAt(string parameterName)
		{
			DbParameter parameter = GetParameter(parameterName);
			Remove(parameter);
		}

		public override void RemoveAt(int index)
		{
			object value = items[index];
			Remove(value);
		}

		internal MySqlParameterCollection(MySqlCommand cmd)
		{
			indexHashCS = new Dictionary<string, int>();
			indexHashCI = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
			containsUnnamedParameters = false;
			Clear();
		}

		public MySqlParameter Add(MySqlParameter value)
		{
			return InternalAdd(value, -1);
		}

		[Obsolete("Add(String parameterName, Object value) has been deprecated.  Use AddWithValue(String parameterName, Object value)")]
		public MySqlParameter Add(string parameterName, object value)
		{
			return Add(new MySqlParameter(parameterName, value));
		}

		public MySqlParameter AddWithValue(string parameterName, object value)
		{
			return Add(new MySqlParameter(parameterName, value));
		}

		public MySqlParameter Add(string parameterName, MySqlDbType dbType)
		{
			return Add(new MySqlParameter(parameterName, dbType));
		}

		public MySqlParameter Add(string parameterName, MySqlDbType dbType, int size)
		{
			return Add(new MySqlParameter(parameterName, dbType, size));
		}

		public override void Clear()
		{
			foreach (MySqlParameter item in items)
			{
				item.Collection = null;
			}
			items.Clear();
			indexHashCS.Clear();
			indexHashCI.Clear();
		}

		private void CheckIndex(int index)
		{
			if (index < 0 || index >= Count)
			{
				throw new IndexOutOfRangeException("Parameter index is out of range.");
			}
		}

		private MySqlParameter InternalGetParameter(int index)
		{
			CheckIndex(index);
			return items[index];
		}

		private MySqlParameter InternalGetParameter(string parameterName)
		{
			int num = IndexOf(parameterName);
			if (num < 0)
			{
				if (parameterName.StartsWith("@", StringComparison.Ordinal) || parameterName.StartsWith("?", StringComparison.Ordinal))
				{
					string parameterName2 = parameterName.Substring(1);
					num = IndexOf(parameterName2);
					if (num != -1)
					{
						return items[num];
					}
				}
				throw new ArgumentException("Parameter '" + parameterName + "' not found in the collection.");
			}
			return items[num];
		}

		private void InternalSetParameter(string parameterName, MySqlParameter value)
		{
			int num = IndexOf(parameterName);
			if (num < 0)
			{
				throw new ArgumentException("Parameter '" + parameterName + "' not found in the collection.");
			}
			InternalSetParameter(num, value);
		}

		private void InternalSetParameter(int index, MySqlParameter value)
		{
			if (value == null)
			{
				throw new ArgumentException(Resources.NewValueShouldBeMySqlParameter);
			}
			CheckIndex(index);
			MySqlParameter mySqlParameter = items[index];
			indexHashCS.Remove(mySqlParameter.ParameterName);
			indexHashCI.Remove(mySqlParameter.ParameterName);
			items[index] = value;
			indexHashCS.Add(value.ParameterName, index);
			indexHashCI.Add(value.ParameterName, index);
		}

		public override int IndexOf(string parameterName)
		{
			int value = -1;
			if (!indexHashCS.TryGetValue(parameterName, out value) && !indexHashCI.TryGetValue(parameterName, out value))
			{
				return -1;
			}
			return value;
		}

		public override int IndexOf(object value)
		{
			if (!(value is MySqlParameter item))
			{
				throw new ArgumentException("Argument must be of type DbParameter", "value");
			}
			return items.IndexOf(item);
		}

		internal void ParameterNameChanged(MySqlParameter p, string oldName, string newName)
		{
			int value = IndexOf(oldName);
			indexHashCS.Remove(oldName);
			indexHashCI.Remove(oldName);
			indexHashCS.Add(newName, value);
			indexHashCI.Add(newName, value);
		}

		private MySqlParameter InternalAdd(MySqlParameter value, int index)
		{
			if (value == null)
			{
				throw new ArgumentException("The MySqlParameterCollection only accepts non-null MySqlParameter type objects.", "value");
			}
			if (string.IsNullOrEmpty(value.ParameterName))
			{
				value.ParameterName = $"Parameter{GetNextIndex()}";
			}
			if (IndexOf(value.ParameterName) >= 0)
			{
				throw new MySqlException(string.Format(Resources.ParameterAlreadyDefined, value.ParameterName));
			}
			string text = value.ParameterName;
			if (text[0] == '@' || text[0] == '?')
			{
				text = text.Substring(1, text.Length - 1);
			}
			if (IndexOf(text) >= 0)
			{
				throw new MySqlException(string.Format(Resources.ParameterAlreadyDefined, value.ParameterName));
			}
			if (index == -1)
			{
				items.Add(value);
				index = items.Count - 1;
			}
			else
			{
				items.Insert(index, value);
				AdjustHashes(index, addEntry: true);
			}
			indexHashCS.Add(value.ParameterName, index);
			indexHashCI.Add(value.ParameterName, index);
			value.Collection = this;
			return value;
		}

		private int GetNextIndex()
		{
			int num = Count + 1;
			while (true)
			{
				string key = "Parameter" + num;
				if (!indexHashCI.ContainsKey(key))
				{
					break;
				}
				num++;
			}
			return num;
		}

		private static void AdjustHash(Dictionary<string, int> hash, string parameterName, int keyIndex, bool addEntry)
		{
			if (hash.ContainsKey(parameterName))
			{
				int num = hash[parameterName];
				if (num >= keyIndex)
				{
					hash[parameterName] = ((!addEntry) ? (--num) : (++num));
				}
			}
		}

		private void AdjustHashes(int keyIndex, bool addEntry)
		{
			for (int i = 0; i < Count; i++)
			{
				string parameterName = items[i].ParameterName;
				AdjustHash(indexHashCI, parameterName, keyIndex, addEntry);
				AdjustHash(indexHashCS, parameterName, keyIndex, addEntry);
			}
		}

		private MySqlParameter GetParameterFlexibleInternal(string baseName)
		{
			int num = IndexOf(baseName);
			if (-1 == num)
			{
				num = IndexOf("?" + baseName);
			}
			if (-1 == num)
			{
				num = IndexOf("@" + baseName);
			}
			if (-1 != num)
			{
				return this[num];
			}
			return null;
		}

		internal MySqlParameter GetParameterFlexible(string parameterName, bool throwOnNotFound)
		{
			string baseName = parameterName;
			MySqlParameter parameterFlexibleInternal = GetParameterFlexibleInternal(baseName);
			if (parameterFlexibleInternal != null)
			{
				return parameterFlexibleInternal;
			}
			if (parameterName.StartsWith("@", StringComparison.Ordinal) || parameterName.StartsWith("?", StringComparison.Ordinal))
			{
				baseName = parameterName.Substring(1);
			}
			parameterFlexibleInternal = GetParameterFlexibleInternal(baseName);
			if (parameterFlexibleInternal != null)
			{
				return parameterFlexibleInternal;
			}
			if (throwOnNotFound)
			{
				throw new ArgumentException("Parameter '" + parameterName + "' not found in the collection.");
			}
			return null;
		}
	}
	public sealed class MySqlTrace
	{
		private static TraceSource source;

		protected static string qaHost;

		protected static bool qaEnabled;

		public static TraceListenerCollection Listeners => source.Listeners;

		public static SourceSwitch Switch
		{
			get
			{
				return source.Switch;
			}
			set
			{
				source.Switch = value;
			}
		}

		public static bool QueryAnalysisEnabled => qaEnabled;

		internal static TraceSource Source => source;

		static MySqlTrace()
		{
			source = new TraceSource("mysql");
			qaEnabled = false;
			foreach (TraceListener listener in source.Listeners)
			{
				if (listener.GetType().ToString().Contains("MySql.EMTrace.EMTraceListener"))
				{
					qaEnabled = true;
					break;
				}
			}
		}

		public static void EnableQueryAnalyzer(string host, int postInterval)
		{
			if (!qaEnabled)
			{
				TraceListener traceListener = (TraceListener)Activator.CreateInstance("MySql.EMTrace", "MySql.EMTrace.EMTraceListener", ignoreCase: false, BindingFlags.CreateInstance, null, new object[2] { host, postInterval }, null, null, null).Unwrap();
				if (traceListener == null)
				{
					throw new MySqlException(Resources.UnableToEnableQueryAnalysis);
				}
				source.Listeners.Add(traceListener);
				Switch.Level = SourceLevels.All;
			}
		}

		public static void DisableQueryAnalyzer()
		{
			qaEnabled = false;
			foreach (TraceListener listener in source.Listeners)
			{
				if (listener.GetType().ToString().Contains("EMTraceListener"))
				{
					source.Listeners.Remove(listener);
					break;
				}
			}
		}

		internal static void LogInformation(int id, string msg)
		{
			Source.TraceEvent(TraceEventType.Information, id, msg, MySqlTraceEventType.NonQuery, -1);
			Trace.TraceInformation(msg);
		}

		internal static void LogWarning(int id, string msg)
		{
			Source.TraceEvent(TraceEventType.Warning, id, msg, MySqlTraceEventType.NonQuery, -1);
			Trace.TraceWarning(msg);
		}

		internal static void LogError(int id, string msg)
		{
			Source.TraceEvent(TraceEventType.Error, id, msg, MySqlTraceEventType.NonQuery, -1);
			Trace.TraceError(msg);
		}

		internal static void TraceEvent(TraceEventType eventType, MySqlTraceEventType mysqlEventType, string msgFormat, params object[] args)
		{
			Source.TraceEvent(eventType, (int)mysqlEventType, msgFormat, args);
		}
	}
	public enum UsageAdvisorWarningFlags
	{
		NoIndex = 1,
		BadIndex,
		SkippedRows,
		SkippedColumns,
		FieldConversion
	}
	public enum MySqlTraceEventType
	{
		ConnectionOpened = 1,
		ConnectionClosed,
		QueryOpened,
		ResultOpened,
		ResultClosed,
		QueryClosed,
		StatementPrepared,
		StatementExecuted,
		StatementClosed,
		NonQuery,
		UsageAdvisorWarning,
		Warning,
		Error,
		QueryNormalized
	}
	public sealed class MySqlTransaction : DbTransaction, IDisposable
	{
		private System.Data.IsolationLevel level;

		private MySqlConnection conn;

		private bool open;

		protected override DbConnection DbConnection => conn;

		public new MySqlConnection Connection => conn;

		public override System.Data.IsolationLevel IsolationLevel => level;

		internal MySqlTransaction(MySqlConnection c, System.Data.IsolationLevel il)
		{
			conn = c;
			level = il;
			open = true;
		}

		~MySqlTransaction()
		{
			Dispose(disposing: false);
		}

		public new void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		internal new void Dispose(bool disposing)
		{
			if (disposing && ((conn != null && conn.State == ConnectionState.Open) || conn.SoftClosed) && open)
			{
				Rollback();
			}
		}

		public override void Commit()
		{
			if (conn == null || (conn.State != ConnectionState.Open && !conn.SoftClosed))
			{
				throw new InvalidOperationException("Connection must be valid and open to commit transaction");
			}
			if (!open)
			{
				throw new InvalidOperationException("Transaction has already been committed or is not pending");
			}
			MySqlCommand mySqlCommand = new MySqlCommand("COMMIT", conn);
			mySqlCommand.ExecuteNonQuery();
			open = false;
		}

		public override void Rollback()
		{
			if (conn == null || (conn.State != ConnectionState.Open && !conn.SoftClosed))
			{
				throw new InvalidOperationException("Connection must be valid and open to rollback transaction");
			}
			if (!open)
			{
				throw new InvalidOperationException("Transaction has already been rolled back or is not pending");
			}
			MySqlCommand mySqlCommand = new MySqlCommand("ROLLBACK", conn);
			mySqlCommand.ExecuteNonQuery();
			open = false;
		}
	}
	internal enum ColumnFlags
	{
		NOT_NULL = 1,
		PRIMARY_KEY = 2,
		UNIQUE_KEY = 4,
		MULTIPLE_KEY = 8,
		BLOB = 0x10,
		UNSIGNED = 0x20,
		ZERO_FILL = 0x40,
		BINARY = 0x80,
		ENUM = 0x100,
		AUTO_INCREMENT = 0x200,
		TIMESTAMP = 0x400,
		SET = 0x800,
		NUMBER = 0x8000
	}
	internal class MySqlField
	{
		public string CatalogName;

		public int ColumnLength;

		public string ColumnName;

		public string OriginalColumnName;

		public string TableName;

		public string RealTableName;

		public string DatabaseName;

		public Encoding Encoding;

		public int maxLength;

		protected ColumnFlags colFlags;

		protected int charSetIndex;

		protected byte precision;

		protected byte scale;

		protected MySqlDbType mySqlDbType;

		protected DBVersion connVersion;

		protected Driver driver;

		protected bool binaryOk;

		protected List<Type> typeConversions = new List<Type>();

		public int CharacterSetIndex
		{
			get
			{
				return charSetIndex;
			}
			set
			{
				charSetIndex = value;
				SetFieldEncoding();
			}
		}

		public MySqlDbType Type => mySqlDbType;

		public byte Precision
		{
			get
			{
				return precision;
			}
			set
			{
				precision = value;
			}
		}

		public byte Scale
		{
			get
			{
				return scale;
			}
			set
			{
				scale = value;
			}
		}

		public int MaxLength
		{
			get
			{
				return maxLength;
			}
			set
			{
				maxLength = value;
			}
		}

		public ColumnFlags Flags => colFlags;

		public bool IsAutoIncrement => (colFlags & ColumnFlags.AUTO_INCREMENT) > (ColumnFlags)0;

		public bool IsNumeric => (colFlags & ColumnFlags.NUMBER) > (ColumnFlags)0;

		public bool AllowsNull => (colFlags & ColumnFlags.NOT_NULL) == 0;

		public bool IsUnique => (colFlags & ColumnFlags.UNIQUE_KEY) > (ColumnFlags)0;

		public bool IsPrimaryKey => (colFlags & ColumnFlags.PRIMARY_KEY) > (ColumnFlags)0;

		public bool IsBlob
		{
			get
			{
				if ((mySqlDbType < MySqlDbType.TinyBlob || mySqlDbType > MySqlDbType.Blob) && (mySqlDbType < MySqlDbType.TinyText || mySqlDbType > MySqlDbType.Text))
				{
					return (colFlags & ColumnFlags.BLOB) > (ColumnFlags)0;
				}
				return true;
			}
		}

		public bool IsBinary
		{
			get
			{
				if (binaryOk)
				{
					return CharacterSetIndex == 63;
				}
				return false;
			}
		}

		public bool IsUnsigned => (colFlags & ColumnFlags.UNSIGNED) > (ColumnFlags)0;

		public bool IsTextField
		{
			get
			{
				if (Type != MySqlDbType.VarString && Type != MySqlDbType.VarChar && Type != MySqlDbType.String)
				{
					if (IsBlob)
					{
						return !IsBinary;
					}
					return false;
				}
				return true;
			}
		}

		public int CharacterLength => ColumnLength / MaxLength;

		public List<Type> TypeConversions => typeConversions;

		public MySqlField(Driver driver)
		{
			this.driver = driver;
			connVersion = driver.Version;
			maxLength = 1;
			binaryOk = true;
		}

		public void SetTypeAndFlags(MySqlDbType type, ColumnFlags flags)
		{
			colFlags = flags;
			mySqlDbType = type;
			if (string.IsNullOrEmpty(TableName) && string.IsNullOrEmpty(RealTableName) && IsBinary && driver.Settings.FunctionsReturnString)
			{
				CharacterSetIndex = driver.ConnectionCharSetIndex;
			}
			if (IsUnsigned)
			{
				switch (type)
				{
				case MySqlDbType.Byte:
					mySqlDbType = MySqlDbType.UByte;
					return;
				case MySqlDbType.Int16:
					mySqlDbType = MySqlDbType.UInt16;
					return;
				case MySqlDbType.Int24:
					mySqlDbType = MySqlDbType.UInt24;
					return;
				case MySqlDbType.Int32:
					mySqlDbType = MySqlDbType.UInt32;
					return;
				case MySqlDbType.Int64:
					mySqlDbType = MySqlDbType.UInt64;
					return;
				}
			}
			if (IsBlob)
			{
				if (IsBinary && driver.Settings.TreatBlobsAsUTF8)
				{
					bool flag = false;
					Regex blobAsUTF8IncludeRegex = driver.Settings.GetBlobAsUTF8IncludeRegex();
					Regex blobAsUTF8ExcludeRegex = driver.Settings.GetBlobAsUTF8ExcludeRegex();
					if (blobAsUTF8IncludeRegex != null && blobAsUTF8IncludeRegex.IsMatch(ColumnName))
					{
						flag = true;
					}
					else if (blobAsUTF8IncludeRegex == null && blobAsUTF8ExcludeRegex != null && !blobAsUTF8ExcludeRegex.IsMatch(ColumnName))
					{
						flag = true;
					}
					if (flag)
					{
						binaryOk = false;
						Encoding = Encoding.GetEncoding("UTF-8");
						charSetIndex = -1;
						maxLength = 4;
					}
				}
				if (!IsBinary)
				{
					switch (type)
					{
					case MySqlDbType.TinyBlob:
						mySqlDbType = MySqlDbType.TinyText;
						break;
					case MySqlDbType.MediumBlob:
						mySqlDbType = MySqlDbType.MediumText;
						break;
					case MySqlDbType.Blob:
						mySqlDbType = MySqlDbType.Text;
						break;
					case MySqlDbType.LongBlob:
						mySqlDbType = MySqlDbType.LongText;
						break;
					}
				}
			}
			if (driver.Settings.RespectBinaryFlags)
			{
				CheckForExceptions();
			}
			if (Type == MySqlDbType.String && CharacterLength == 36 && !driver.Settings.OldGuids)
			{
				mySqlDbType = MySqlDbType.Guid;
			}
			if (!IsBinary)
			{
				return;
			}
			if (driver.Settings.RespectBinaryFlags)
			{
				switch (type)
				{
				case MySqlDbType.String:
					mySqlDbType = MySqlDbType.Binary;
					break;
				case MySqlDbType.VarString:
				case MySqlDbType.VarChar:
					mySqlDbType = MySqlDbType.VarBinary;
					break;
				}
			}
			if (CharacterSetIndex == 63)
			{
				CharacterSetIndex = driver.ConnectionCharSetIndex;
			}
			if (Type == MySqlDbType.Binary && ColumnLength == 16 && driver.Settings.OldGuids)
			{
				mySqlDbType = MySqlDbType.Guid;
			}
		}

		public void AddTypeConversion(Type t)
		{
			if (!TypeConversions.Contains(t))
			{
				TypeConversions.Add(t);
			}
		}

		private void CheckForExceptions()
		{
			string text = string.Empty;
			if (OriginalColumnName != null)
			{
				text = StringUtility.ToUpperInvariant(OriginalColumnName);
			}
			if (text.StartsWith("CHAR(", StringComparison.Ordinal))
			{
				binaryOk = false;
			}
		}

		public IMySqlValue GetValueObject()
		{
			IMySqlValue mySqlValue = GetIMySqlValue(Type);
			if (mySqlValue is MySqlByte && ColumnLength == 1 && driver.Settings.TreatTinyAsBoolean)
			{
				MySqlByte mySqlByte = (MySqlByte)(object)mySqlValue;
				mySqlByte.TreatAsBoolean = true;
				mySqlValue = mySqlByte;
			}
			else if (mySqlValue is MySqlGuid mySqlGuid)
			{
				mySqlGuid.OldGuids = driver.Settings.OldGuids;
				mySqlValue = mySqlGuid;
			}
			return mySqlValue;
		}

		public static IMySqlValue GetIMySqlValue(MySqlDbType type)
		{
			switch (type)
			{
			case MySqlDbType.Byte:
				return default(MySqlByte);
			case MySqlDbType.UByte:
				return default(MySqlUByte);
			case MySqlDbType.Int16:
				return default(MySqlInt16);
			case MySqlDbType.UInt16:
				return default(MySqlUInt16);
			case MySqlDbType.Int32:
			case MySqlDbType.Int24:
			case MySqlDbType.Year:
				return new MySqlInt32(type, isNull: true);
			case MySqlDbType.UInt32:
			case MySqlDbType.UInt24:
				return new MySqlUInt32(type, isNull: true);
			case MySqlDbType.Bit:
				return default(MySqlBit);
			case MySqlDbType.Int64:
				return default(MySqlInt64);
			case MySqlDbType.UInt64:
				return default(MySqlUInt64);
			case MySqlDbType.Time:
				return default(MySqlTimeSpan);
			case MySqlDbType.Timestamp:
			case MySqlDbType.Date:
			case MySqlDbType.DateTime:
			case MySqlDbType.Newdate:
				return new MySqlDateTime(type, isNull: true);
			case MySqlDbType.Decimal:
			case MySqlDbType.NewDecimal:
				return default(MySqlDecimal);
			case MySqlDbType.Float:
				return default(MySqlSingle);
			case MySqlDbType.Double:
				return default(MySqlDouble);
			case (MySqlDbType)6:
			case MySqlDbType.VarString:
			case MySqlDbType.Enum:
			case MySqlDbType.Set:
			case MySqlDbType.VarChar:
			case MySqlDbType.String:
			case MySqlDbType.TinyText:
			case MySqlDbType.MediumText:
			case MySqlDbType.LongText:
			case MySqlDbType.Text:
				return new MySqlString(type, isNull: true);
			case MySqlDbType.Geometry:
				return new MySqlGeometry(type, isNull: true);
			case MySqlDbType.TinyBlob:
			case MySqlDbType.MediumBlob:
			case MySqlDbType.LongBlob:
			case MySqlDbType.Blob:
			case MySqlDbType.Binary:
			case MySqlDbType.VarBinary:
				return new MySqlBinary(type, isNull: true);
			case MySqlDbType.Guid:
				return default(MySqlGuid);
			default:
				throw new MySqlException("Unknown data type");
			}
		}

		private void SetFieldEncoding()
		{
			Dictionary<int, string> characterSets = driver.CharacterSets;
			DBVersion version = driver.Version;
			if (characterSets != null && characterSets.Count != 0 && CharacterSetIndex != -1 && characterSets[CharacterSetIndex] != null)
			{
				CharacterSet characterSet = CharSetMap.GetCharacterSet(version, characterSets[CharacterSetIndex]);
				if (characterSet.name.ToLower(CultureInfo.InvariantCulture) == "utf-8" && version.Major >= 6)
				{
					MaxLength = 4;
				}
				else
				{
					MaxLength = characterSet.byteCount;
				}
				Encoding = CharSetMap.GetEncoding(version, characterSets[CharacterSetIndex]);
			}
		}
	}
	[RunInstaller(true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class CustomInstaller : Installer
	{
		public override void Install(IDictionary stateSaver)
		{
			base.Install(stateSaver);
			AddProviderToMachineConfig();
		}

		private static void AddProviderToMachineConfig()
		{
			object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\.NETFramework\\", "InstallRoot", null);
			if (value == null)
			{
				throw new Exception("Unable to retrieve install root for .NET framework");
			}
			UpdateMachineConfigs(value.ToString(), add: true);
			string text = value.ToString();
			text = text.Substring(0, text.Length - 1);
			text = $"{text}64{Path.DirectorySeparatorChar}";
			if (Directory.Exists(text))
			{
				UpdateMachineConfigs(text, add: true);
			}
		}

		internal static void UpdateMachineConfigs(string rootPath, bool add)
		{
			string[] array = new string[2] { "v2.0.50727", "v4.0.30319" };
			string[] array2 = array;
			foreach (string text in array2)
			{
				string arg = rootPath + text;
				string path = $"{arg}\\CONFIG";
				if (Directory.Exists(path))
				{
					if (add)
					{
						AddProviderToMachineConfigInDir(path);
					}
					else
					{
						RemoveProviderFromMachineConfigInDir(path);
					}
				}
			}
		}

		private static XmlElement CreateNodeAssemblyBindingRedirection(XmlElement mysqlNode, XmlDocument doc, string oldVersion, string newVersion)
		{
			if (doc == null || mysqlNode == null)
			{
				return null;
			}
			string namespaceURI = "urn:schemas-microsoft-com:asm.v1";
			XmlElement xmlElement = (XmlElement)doc.CreateNode(XmlNodeType.Element, "dependentAssembly", namespaceURI);
			XmlElement xmlElement2 = (XmlElement)doc.CreateNode(XmlNodeType.Element, "assemblyIdentity", namespaceURI);
			xmlElement2.SetAttribute("name", "MySql.Data");
			xmlElement2.SetAttribute("publicKeyToken", "c5687fc88969c44d");
			xmlElement2.SetAttribute("culture", "neutral");
			XmlElement xmlElement3 = (XmlElement)doc.CreateNode(XmlNodeType.Element, "bindingRedirect", namespaceURI);
			xmlElement3.SetAttribute("oldVersion", oldVersion);
			xmlElement3.SetAttribute("newVersion", newVersion);
			xmlElement.AppendChild(xmlElement2);
			xmlElement.AppendChild(xmlElement3);
			mysqlNode.AppendChild(xmlElement);
			xmlElement = (XmlElement)doc.CreateNode(XmlNodeType.Element, "dependentAssembly", namespaceURI);
			xmlElement2 = (XmlElement)doc.CreateNode(XmlNodeType.Element, "assemblyIdentity", namespaceURI);
			xmlElement2.SetAttribute("name", "MySql.Data.Entity");
			xmlElement2.SetAttribute("publicKeyToken", "c5687fc88969c44d");
			xmlElement2.SetAttribute("culture", "neutral");
			xmlElement3 = (XmlElement)doc.CreateNode(XmlNodeType.Element, "bindingRedirect", namespaceURI);
			xmlElement3.SetAttribute("oldVersion", oldVersion);
			xmlElement3.SetAttribute("newVersion", newVersion);
			xmlElement.AppendChild(xmlElement2);
			xmlElement.AppendChild(xmlElement3);
			mysqlNode.AppendChild(xmlElement);
			xmlElement = (XmlElement)doc.CreateNode(XmlNodeType.Element, "dependentAssembly", namespaceURI);
			xmlElement2 = (XmlElement)doc.CreateNode(XmlNodeType.Element, "assemblyIdentity", namespaceURI);
			xmlElement2.SetAttribute("name", "MySql.Web");
			xmlElement2.SetAttribute("publicKeyToken", "c5687fc88969c44d");
			xmlElement2.SetAttribute("culture", "neutral");
			xmlElement3 = (XmlElement)doc.CreateNode(XmlNodeType.Element, "bindingRedirect", namespaceURI);
			xmlElement3.SetAttribute("oldVersion", oldVersion);
			xmlElement3.SetAttribute("newVersion", newVersion);
			xmlElement.AppendChild(xmlElement2);
			xmlElement.AppendChild(xmlElement3);
			mysqlNode.AppendChild(xmlElement);
			return mysqlNode;
		}

		private static void AddProviderToMachineConfigInDir(string path)
		{
			string text = $"{path}\\machine.config";
			if (!File.Exists(text))
			{
				return;
			}
			StreamReader streamReader = new StreamReader(text);
			string xml = streamReader.ReadToEnd();
			streamReader.Close();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			xmlDocument = RemoveOldBindingRedirection(xmlDocument);
			XmlElement xmlElement = (XmlElement)xmlDocument.CreateNode(XmlNodeType.Element, "add", "");
			xmlElement.SetAttribute("name", "MySQL Data Provider");
			xmlElement.SetAttribute("invariant", "MySql.Data.MySqlClient");
			xmlElement.SetAttribute("description", ".Net Framework Data Provider for MySQL");
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string value = string.Format("MySql.Data.MySqlClient.MySqlClientFactory, {0}", executingAssembly.FullName.Replace("Installers", "Data"));
			xmlElement.SetAttribute("type", value);
			XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("DbProviderFactories");
			foreach (XmlNode childNode in elementsByTagName[0].ChildNodes)
			{
				if (childNode.Attributes == null)
				{
					continue;
				}
				foreach (XmlAttribute attribute in childNode.Attributes)
				{
					if (attribute.Name == "invariant" && attribute.Value == "MySql.Data.MySqlClient")
					{
						elementsByTagName[0].RemoveChild(childNode);
						break;
					}
				}
			}
			elementsByTagName[0].AppendChild(xmlElement);
			try
			{
				XmlElement xmlElement2;
				if (xmlDocument.GetElementsByTagName("assemblyBinding").Count == 0)
				{
					xmlElement2 = (XmlElement)xmlDocument.CreateNode(XmlNodeType.Element, "assemblyBinding", "");
					xmlElement2.SetAttribute("xmlns", "urn:schemas-microsoft-com:asm.v1");
				}
				else
				{
					xmlElement2 = (XmlElement)xmlDocument.GetElementsByTagName("assemblyBinding")[0];
				}
				string newVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
				xmlElement2 = CreateNodeAssemblyBindingRedirection(xmlElement2, xmlDocument, "6.7.4.0", newVersion);
				XmlNodeList elementsByTagName2 = xmlDocument.GetElementsByTagName("runtime");
				elementsByTagName2[0].AppendChild(xmlElement2);
			}
			catch
			{
			}
			XmlTextWriter xmlTextWriter = new XmlTextWriter(text, null);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlDocument.Save(xmlTextWriter);
			xmlTextWriter.Flush();
			xmlTextWriter.Close();
		}

		private static XmlDocument RemoveOldBindingRedirection(XmlDocument doc)
		{
			if (doc.GetElementsByTagName("assemblyBinding").Count == 0)
			{
				return doc;
			}
			XmlNodeList childNodes = doc.GetElementsByTagName("assemblyBinding")[0].ChildNodes;
			if (childNodes != null)
			{
				int count = childNodes.Count;
				for (int i = 0; i < count; i++)
				{
					if (childNodes[0].ChildNodes[0].Attributes[0].Name == "name" && childNodes[0].ChildNodes[0].Attributes[0].Value.Contains("MySql"))
					{
						doc.GetElementsByTagName("assemblyBinding")[0].RemoveChild(childNodes[0]);
					}
				}
			}
			return doc;
		}

		public override void Uninstall(IDictionary savedState)
		{
			base.Uninstall(savedState);
			RemoveProviderFromMachineConfig();
		}

		private static void RemoveProviderFromMachineConfig()
		{
			object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\.NETFramework\\", "InstallRoot", null);
			if (value == null)
			{
				throw new Exception("Unable to retrieve install root for .NET framework");
			}
			UpdateMachineConfigs(value.ToString(), add: false);
			string text = value.ToString();
			text = text.Substring(0, text.Length - 1);
			text = $"{text}64{Path.DirectorySeparatorChar}";
			if (Directory.Exists(text))
			{
				UpdateMachineConfigs(text, add: false);
			}
		}

		private static void RemoveProviderFromMachineConfigInDir(string path)
		{
			string text = $"{path}\\machine.config";
			if (!File.Exists(text))
			{
				return;
			}
			StreamReader streamReader = new StreamReader(text);
			string xml = streamReader.ReadToEnd();
			streamReader.Close();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("DbProviderFactories");
			foreach (XmlNode childNode in elementsByTagName[0].ChildNodes)
			{
				if (childNode.Attributes != null)
				{
					string value = childNode.Attributes["name"].Value;
					if (value == "MySQL Data Provider")
					{
						elementsByTagName[0].RemoveChild(childNode);
						break;
					}
				}
			}
			try
			{
				xmlDocument = RemoveOldBindingRedirection(xmlDocument);
			}
			catch
			{
			}
			XmlTextWriter xmlTextWriter = new XmlTextWriter(text, null);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlDocument.Save(xmlTextWriter);
			xmlTextWriter.Flush();
			xmlTextWriter.Close();
		}
	}
	public abstract class BaseCommandInterceptor
	{
		protected MySqlConnection ActiveConnection { get; private set; }

		public virtual bool ExecuteScalar(string sql, ref object returnValue)
		{
			return false;
		}

		public virtual bool ExecuteNonQuery(string sql, ref int returnValue)
		{
			return false;
		}

		public virtual bool ExecuteReader(string sql, CommandBehavior behavior, ref MySqlDataReader returnValue)
		{
			return false;
		}

		public virtual void Init(MySqlConnection connection)
		{
			ActiveConnection = connection;
		}
	}
	internal abstract class Interceptor
	{
		protected MySqlConnection connection;

		protected void LoadInterceptors(string interceptorList)
		{
			if (string.IsNullOrEmpty(interceptorList))
			{
				return;
			}
			string[] array = interceptorList.Split(new char[1] { '|' });
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string typeName = ResolveType(text);
					Type type = Type.GetType(typeName);
					object o = Activator.CreateInstance(type);
					AddInterceptor(o);
				}
			}
		}

		protected abstract void AddInterceptor(object o);

		protected virtual string ResolveType(string nameOrType)
		{
			return nameOrType;
		}
	}
	internal sealed class CommandInterceptor : Interceptor
	{
		private bool insideInterceptor;

		private List<BaseCommandInterceptor> interceptors = new List<BaseCommandInterceptor>();

		public CommandInterceptor(MySqlConnection connection)
		{
			base.connection = connection;
			LoadInterceptors(connection.Settings.CommandInterceptors);
		}

		public bool ExecuteScalar(string sql, ref object returnValue)
		{
			if (insideInterceptor)
			{
				return false;
			}
			insideInterceptor = true;
			bool flag = false;
			foreach (BaseCommandInterceptor interceptor in interceptors)
			{
				flag |= interceptor.ExecuteScalar(sql, ref returnValue);
			}
			insideInterceptor = false;
			return flag;
		}

		public bool ExecuteNonQuery(string sql, ref int returnValue)
		{
			if (insideInterceptor)
			{
				return false;
			}
			insideInterceptor = true;
			bool flag = false;
			foreach (BaseCommandInterceptor interceptor in interceptors)
			{
				flag |= interceptor.ExecuteNonQuery(sql, ref returnValue);
			}
			insideInterceptor = false;
			return flag;
		}

		public bool ExecuteReader(string sql, CommandBehavior behavior, ref MySqlDataReader returnValue)
		{
			if (insideInterceptor)
			{
				return false;
			}
			insideInterceptor = true;
			bool flag = false;
			foreach (BaseCommandInterceptor interceptor in interceptors)
			{
				flag |= interceptor.ExecuteReader(sql, behavior, ref returnValue);
			}
			insideInterceptor = false;
			return flag;
		}

		protected override void AddInterceptor(object o)
		{
			if (o == null)
			{
				throw new ArgumentException($"Unable to instantiate CommandInterceptor");
			}
			if (!(o is BaseCommandInterceptor))
			{
				throw new InvalidOperationException(string.Format(Resources.TypeIsNotCommandInterceptor, o.GetType()));
			}
			BaseCommandInterceptor baseCommandInterceptor = o as BaseCommandInterceptor;
			baseCommandInterceptor.Init(connection);
			interceptors.Insert(0, (BaseCommandInterceptor)o);
		}

		protected override string ResolveType(string nameOrType)
		{
			if (MySqlConfiguration.Settings != null && MySqlConfiguration.Settings.CommandInterceptors != null)
			{
				foreach (InterceptorConfigurationElement commandInterceptor in MySqlConfiguration.Settings.CommandInterceptors)
				{
					if (string.Compare(commandInterceptor.Name, nameOrType, ignoreCase: true) == 0)
					{
						return commandInterceptor.Type;
					}
				}
			}
			return base.ResolveType(nameOrType);
		}
	}
	public abstract class BaseExceptionInterceptor
	{
		protected MySqlConnection ActiveConnection { get; private set; }

		public abstract Exception InterceptException(Exception exception);

		public virtual void Init(MySqlConnection connection)
		{
			ActiveConnection = connection;
		}
	}
	internal sealed class StandardExceptionInterceptor : BaseExceptionInterceptor
	{
		public override Exception InterceptException(Exception exception)
		{
			return exception;
		}
	}
	internal sealed class ExceptionInterceptor : Interceptor
	{
		private List<BaseExceptionInterceptor> interceptors = new List<BaseExceptionInterceptor>();

		public ExceptionInterceptor(MySqlConnection connection)
		{
			base.connection = connection;
			LoadInterceptors(connection.Settings.ExceptionInterceptors);
			interceptors.Add(new StandardExceptionInterceptor());
		}

		protected override void AddInterceptor(object o)
		{
			if (o == null)
			{
				throw new ArgumentException($"Unable to instantiate ExceptionInterceptor");
			}
			if (!(o is BaseExceptionInterceptor))
			{
				throw new InvalidOperationException(string.Format(Resources.TypeIsNotExceptionInterceptor, o.GetType()));
			}
			BaseExceptionInterceptor baseExceptionInterceptor = o as BaseExceptionInterceptor;
			baseExceptionInterceptor.Init(connection);
			interceptors.Insert(0, (BaseExceptionInterceptor)o);
		}

		public void Throw(Exception exception)
		{
			Exception ex = exception;
			foreach (BaseExceptionInterceptor interceptor in interceptors)
			{
				ex = interceptor.InterceptException(ex);
			}
			throw ex;
		}

		protected override string ResolveType(string nameOrType)
		{
			if (MySqlConfiguration.Settings != null && MySqlConfiguration.Settings.ExceptionInterceptors != null)
			{
				foreach (InterceptorConfigurationElement exceptionInterceptor in MySqlConfiguration.Settings.ExceptionInterceptors)
				{
					if (string.Compare(exceptionInterceptor.Name, nameOrType, ignoreCase: true) == 0)
					{
						return exceptionInterceptor.Type;
					}
				}
			}
			return base.ResolveType(nameOrType);
		}
	}
	internal class SchemaProvider
	{
		protected MySqlConnection connection;

		public static string MetaCollection = "MetaDataCollections";

		public SchemaProvider(MySqlConnection connectionToUse)
		{
			connection = connectionToUse;
		}

		public virtual MySqlSchemaCollection GetSchema(string collection, string[] restrictions)
		{
			if (connection.State != ConnectionState.Open)
			{
				throw new MySqlException("GetSchema can only be called on an open connection.");
			}
			collection = StringUtility.ToUpperInvariant(collection);
			MySqlSchemaCollection schemaInternal = GetSchemaInternal(collection, restrictions);
			if (schemaInternal == null)
			{
				throw new ArgumentException("Invalid collection name");
			}
			return schemaInternal;
		}

		public virtual MySqlSchemaCollection GetDatabases(string[] restrictions)
		{
			Regex regex = null;
			int num = int.Parse(connection.driver.Property("lower_case_table_names"));
			string text = "SHOW DATABASES";
			if (num == 0 && restrictions != null && restrictions.Length >= 1)
			{
				text = text + " LIKE '" + restrictions[0] + "'";
			}
			MySqlSchemaCollection mySqlSchemaCollection = QueryCollection("Databases", text);
			if (num != 0 && restrictions != null && restrictions.Length >= 1 && restrictions[0] != null)
			{
				regex = new Regex(restrictions[0], RegexOptions.IgnoreCase);
			}
			MySqlSchemaCollection mySqlSchemaCollection2 = new MySqlSchemaCollection("Databases");
			mySqlSchemaCollection2.AddColumn("CATALOG_NAME", typeof(string));
			mySqlSchemaCollection2.AddColumn("SCHEMA_NAME", typeof(string));
			foreach (MySqlSchemaRow row in mySqlSchemaCollection.Rows)
			{
				if (regex == null || regex.Match(row[0].ToString()).Success)
				{
					MySqlSchemaRow mySqlSchemaRow = mySqlSchemaCollection2.AddRow();
					mySqlSchemaRow[1] = row[0];
				}
			}
			return mySqlSchemaCollection2;
		}

		public virtual MySqlSchemaCollection GetTables(string[] restrictions)
		{
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection("Tables");
			mySqlSchemaCollection.AddColumn("TABLE_CATALOG", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_SCHEMA", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_TYPE", typeof(string));
			mySqlSchemaCollection.AddColumn("ENGINE", typeof(string));
			mySqlSchemaCollection.AddColumn("VERSION", typeof(ulong));
			mySqlSchemaCollection.AddColumn("ROW_FORMAT", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_ROWS", typeof(ulong));
			mySqlSchemaCollection.AddColumn("AVG_ROW_LENGTH", typeof(ulong));
			mySqlSchemaCollection.AddColumn("DATA_LENGTH", typeof(ulong));
			mySqlSchemaCollection.AddColumn("MAX_DATA_LENGTH", typeof(ulong));
			mySqlSchemaCollection.AddColumn("INDEX_LENGTH", typeof(ulong));
			mySqlSchemaCollection.AddColumn("DATA_FREE", typeof(ulong));
			mySqlSchemaCollection.AddColumn("AUTO_INCREMENT", typeof(ulong));
			mySqlSchemaCollection.AddColumn("CREATE_TIME", typeof(DateTime));
			mySqlSchemaCollection.AddColumn("UPDATE_TIME", typeof(DateTime));
			mySqlSchemaCollection.AddColumn("CHECK_TIME", typeof(DateTime));
			mySqlSchemaCollection.AddColumn("TABLE_COLLATION", typeof(string));
			mySqlSchemaCollection.AddColumn("CHECKSUM", typeof(ulong));
			mySqlSchemaCollection.AddColumn("CREATE_OPTIONS", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_COMMENT", typeof(string));
			string[] array = new string[4];
			if (restrictions != null && restrictions.Length >= 2)
			{
				array[0] = restrictions[1];
			}
			MySqlSchemaCollection databases = GetDatabases(array);
			if (restrictions != null)
			{
				Array.Copy(restrictions, array, Math.Min(array.Length, restrictions.Length));
			}
			foreach (MySqlSchemaRow row in databases.Rows)
			{
				array[1] = row["SCHEMA_NAME"].ToString();
				FindTables(mySqlSchemaCollection, array);
			}
			return mySqlSchemaCollection;
		}

		protected void QuoteDefaultValues(MySqlSchemaCollection schemaCollection)
		{
			if (schemaCollection == null || !schemaCollection.ContainsColumn("COLUMN_DEFAULT"))
			{
				return;
			}
			foreach (MySqlSchemaRow row in schemaCollection.Rows)
			{
				object arg = row["COLUMN_DEFAULT"];
				if (MetaData.IsTextType(row["DATA_TYPE"].ToString()))
				{
					row["COLUMN_DEFAULT"] = $"{arg}";
				}
			}
		}

		public virtual MySqlSchemaCollection GetColumns(string[] restrictions)
		{
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection("Columns");
			mySqlSchemaCollection.AddColumn("TABLE_CATALOG", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_SCHEMA", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("COLUMN_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("ORDINAL_POSITION", typeof(ulong));
			mySqlSchemaCollection.AddColumn("COLUMN_DEFAULT", typeof(string));
			mySqlSchemaCollection.AddColumn("IS_NULLABLE", typeof(string));
			mySqlSchemaCollection.AddColumn("DATA_TYPE", typeof(string));
			mySqlSchemaCollection.AddColumn("CHARACTER_MAXIMUM_LENGTH", typeof(ulong));
			mySqlSchemaCollection.AddColumn("CHARACTER_OCTET_LENGTH", typeof(ulong));
			mySqlSchemaCollection.AddColumn("NUMERIC_PRECISION", typeof(ulong));
			mySqlSchemaCollection.AddColumn("NUMERIC_SCALE", typeof(ulong));
			mySqlSchemaCollection.AddColumn("CHARACTER_SET_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("COLLATION_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("COLUMN_TYPE", typeof(string));
			mySqlSchemaCollection.AddColumn("COLUMN_KEY", typeof(string));
			mySqlSchemaCollection.AddColumn("EXTRA", typeof(string));
			mySqlSchemaCollection.AddColumn("PRIVILEGES", typeof(string));
			mySqlSchemaCollection.AddColumn("COLUMN_COMMENT", typeof(string));
			string columnRestriction = null;
			if (restrictions != null && restrictions.Length == 4)
			{
				columnRestriction = restrictions[3];
				restrictions[3] = null;
			}
			MySqlSchemaCollection tables = GetTables(restrictions);
			foreach (MySqlSchemaRow row in tables.Rows)
			{
				LoadTableColumns(mySqlSchemaCollection, row["TABLE_SCHEMA"].ToString(), row["TABLE_NAME"].ToString(), columnRestriction);
			}
			QuoteDefaultValues(mySqlSchemaCollection);
			return mySqlSchemaCollection;
		}

		private void LoadTableColumns(MySqlSchemaCollection schemaCollection, string schema, string tableName, string columnRestriction)
		{
			string cmdText = $"SHOW FULL COLUMNS FROM `{schema}`.`{tableName}`";
			MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection);
			int num = 1;
			using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			while (mySqlDataReader.Read())
			{
				string text = mySqlDataReader.GetString(0);
				if (columnRestriction == null || !(text != columnRestriction))
				{
					MySqlSchemaRow mySqlSchemaRow = schemaCollection.AddRow();
					mySqlSchemaRow["TABLE_CATALOG"] = DBNull.Value;
					mySqlSchemaRow["TABLE_SCHEMA"] = schema;
					mySqlSchemaRow["TABLE_NAME"] = tableName;
					mySqlSchemaRow["COLUMN_NAME"] = text;
					mySqlSchemaRow["ORDINAL_POSITION"] = num++;
					mySqlSchemaRow["COLUMN_DEFAULT"] = mySqlDataReader.GetValue(5);
					mySqlSchemaRow["IS_NULLABLE"] = mySqlDataReader.GetString(3);
					mySqlSchemaRow["DATA_TYPE"] = mySqlDataReader.GetString(1);
					mySqlSchemaRow["CHARACTER_MAXIMUM_LENGTH"] = DBNull.Value;
					mySqlSchemaRow["CHARACTER_OCTET_LENGTH"] = DBNull.Value;
					mySqlSchemaRow["NUMERIC_PRECISION"] = DBNull.Value;
					mySqlSchemaRow["NUMERIC_SCALE"] = DBNull.Value;
					mySqlSchemaRow["CHARACTER_SET_NAME"] = mySqlDataReader.GetValue(2);
					mySqlSchemaRow["COLLATION_NAME"] = mySqlSchemaRow["CHARACTER_SET_NAME"];
					mySqlSchemaRow["COLUMN_TYPE"] = mySqlDataReader.GetString(1);
					mySqlSchemaRow["COLUMN_KEY"] = mySqlDataReader.GetString(4);
					mySqlSchemaRow["EXTRA"] = mySqlDataReader.GetString(6);
					mySqlSchemaRow["PRIVILEGES"] = mySqlDataReader.GetString(7);
					mySqlSchemaRow["COLUMN_COMMENT"] = mySqlDataReader.GetString(8);
					ParseColumnRow(mySqlSchemaRow);
				}
			}
		}

		private static void ParseColumnRow(MySqlSchemaRow row)
		{
			string text = row["CHARACTER_SET_NAME"].ToString();
			int num = text.IndexOf('_');
			if (num != -1)
			{
				row["CHARACTER_SET_NAME"] = text.Substring(0, num);
			}
			string text2 = row["DATA_TYPE"].ToString();
			num = text2.IndexOf('(');
			if (num == -1)
			{
				return;
			}
			row["DATA_TYPE"] = text2.Substring(0, num);
			int num2 = text2.IndexOf(')', num);
			string text3 = text2.Substring(num + 1, num2 - (num + 1));
			switch (row["DATA_TYPE"].ToString().ToLower())
			{
			case "char":
			case "varchar":
				row["CHARACTER_MAXIMUM_LENGTH"] = text3;
				break;
			case "real":
			case "decimal":
			{
				string[] array = text3.Split(new char[1] { ',' });
				row["NUMERIC_PRECISION"] = array[0];
				if (array.Length == 2)
				{
					row["NUMERIC_SCALE"] = array[1];
				}
				break;
			}
			}
		}

		public virtual MySqlSchemaCollection GetIndexes(string[] restrictions)
		{
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection("Indexes");
			mySqlSchemaCollection.AddColumn("INDEX_CATALOG", typeof(string));
			mySqlSchemaCollection.AddColumn("INDEX_SCHEMA", typeof(string));
			mySqlSchemaCollection.AddColumn("INDEX_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("UNIQUE", typeof(bool));
			mySqlSchemaCollection.AddColumn("PRIMARY", typeof(bool));
			mySqlSchemaCollection.AddColumn("TYPE", typeof(string));
			mySqlSchemaCollection.AddColumn("COMMENT", typeof(string));
			int val = ((restrictions == null) ? 4 : restrictions.Length);
			string[] array = new string[Math.Max(val, 4)];
			restrictions?.CopyTo(array, 0);
			array[3] = "BASE TABLE";
			MySqlSchemaCollection tables = GetTables(array);
			foreach (MySqlSchemaRow row in tables.Rows)
			{
				string sql = string.Format("SHOW INDEX FROM `{0}`.`{1}`", MySqlHelper.DoubleQuoteString((string)row["TABLE_SCHEMA"]), MySqlHelper.DoubleQuoteString((string)row["TABLE_NAME"]));
				MySqlSchemaCollection mySqlSchemaCollection2 = QueryCollection("indexes", sql);
				foreach (MySqlSchemaRow row2 in mySqlSchemaCollection2.Rows)
				{
					long num = (long)row2["SEQ_IN_INDEX"];
					if (num == 1 && (restrictions == null || restrictions.Length != 4 || restrictions[3] == null || row2["KEY_NAME"].Equals(restrictions[3])))
					{
						MySqlSchemaRow mySqlSchemaRow = mySqlSchemaCollection.AddRow();
						mySqlSchemaRow["INDEX_CATALOG"] = null;
						mySqlSchemaRow["INDEX_SCHEMA"] = row["TABLE_SCHEMA"];
						mySqlSchemaRow["INDEX_NAME"] = row2["KEY_NAME"];
						mySqlSchemaRow["TABLE_NAME"] = row2["TABLE"];
						mySqlSchemaRow["UNIQUE"] = (long)row2["NON_UNIQUE"] == 0;
						mySqlSchemaRow["PRIMARY"] = row2["KEY_NAME"].Equals("PRIMARY");
						mySqlSchemaRow["TYPE"] = row2["INDEX_TYPE"];
						mySqlSchemaRow["COMMENT"] = row2["COMMENT"];
					}
				}
			}
			return mySqlSchemaCollection;
		}

		public virtual MySqlSchemaCollection GetIndexColumns(string[] restrictions)
		{
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection("IndexColumns");
			mySqlSchemaCollection.AddColumn("INDEX_CATALOG", typeof(string));
			mySqlSchemaCollection.AddColumn("INDEX_SCHEMA", typeof(string));
			mySqlSchemaCollection.AddColumn("INDEX_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("COLUMN_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("ORDINAL_POSITION", typeof(int));
			mySqlSchemaCollection.AddColumn("SORT_ORDER", typeof(string));
			int val = ((restrictions == null) ? 4 : restrictions.Length);
			string[] array = new string[Math.Max(val, 4)];
			restrictions?.CopyTo(array, 0);
			array[3] = "BASE TABLE";
			MySqlSchemaCollection tables = GetTables(array);
			foreach (MySqlSchemaRow row in tables.Rows)
			{
				string cmdText = string.Format("SHOW INDEX FROM `{0}`.`{1}`", row["TABLE_SCHEMA"], row["TABLE_NAME"]);
				MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection);
				using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
				while (mySqlDataReader.Read())
				{
					string text = GetString(mySqlDataReader, mySqlDataReader.GetOrdinal("KEY_NAME"));
					string text2 = GetString(mySqlDataReader, mySqlDataReader.GetOrdinal("COLUMN_NAME"));
					if (restrictions == null || ((restrictions.Length < 4 || restrictions[3] == null || !(text != restrictions[3])) && (restrictions.Length < 5 || restrictions[4] == null || !(text2 != restrictions[4]))))
					{
						MySqlSchemaRow mySqlSchemaRow = mySqlSchemaCollection.AddRow();
						mySqlSchemaRow["INDEX_CATALOG"] = null;
						mySqlSchemaRow["INDEX_SCHEMA"] = row["TABLE_SCHEMA"];
						mySqlSchemaRow["INDEX_NAME"] = text;
						mySqlSchemaRow["TABLE_NAME"] = GetString(mySqlDataReader, mySqlDataReader.GetOrdinal("TABLE"));
						mySqlSchemaRow["COLUMN_NAME"] = text2;
						mySqlSchemaRow["ORDINAL_POSITION"] = mySqlDataReader.GetValue(mySqlDataReader.GetOrdinal("SEQ_IN_INDEX"));
						mySqlSchemaRow["SORT_ORDER"] = mySqlDataReader.GetString("COLLATION");
					}
				}
			}
			return mySqlSchemaCollection;
		}

		public virtual MySqlSchemaCollection GetForeignKeys(string[] restrictions)
		{
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection("Foreign Keys");
			mySqlSchemaCollection.AddColumn("CONSTRAINT_CATALOG", typeof(string));
			mySqlSchemaCollection.AddColumn("CONSTRAINT_SCHEMA", typeof(string));
			mySqlSchemaCollection.AddColumn("CONSTRAINT_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_CATALOG", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_SCHEMA", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("MATCH_OPTION", typeof(string));
			mySqlSchemaCollection.AddColumn("UPDATE_RULE", typeof(string));
			mySqlSchemaCollection.AddColumn("DELETE_RULE", typeof(string));
			mySqlSchemaCollection.AddColumn("REFERENCED_TABLE_CATALOG", typeof(string));
			mySqlSchemaCollection.AddColumn("REFERENCED_TABLE_SCHEMA", typeof(string));
			mySqlSchemaCollection.AddColumn("REFERENCED_TABLE_NAME", typeof(string));
			string filterName = null;
			if (restrictions != null && restrictions.Length >= 4)
			{
				filterName = restrictions[3];
				restrictions[3] = null;
			}
			MySqlSchemaCollection tables = GetTables(restrictions);
			foreach (MySqlSchemaRow row in tables.Rows)
			{
				GetForeignKeysOnTable(mySqlSchemaCollection, row, filterName, includeColumns: false);
			}
			return mySqlSchemaCollection;
		}

		public virtual MySqlSchemaCollection GetForeignKeyColumns(string[] restrictions)
		{
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection("Foreign Keys");
			mySqlSchemaCollection.AddColumn("CONSTRAINT_CATALOG", typeof(string));
			mySqlSchemaCollection.AddColumn("CONSTRAINT_SCHEMA", typeof(string));
			mySqlSchemaCollection.AddColumn("CONSTRAINT_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_CATALOG", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_SCHEMA", typeof(string));
			mySqlSchemaCollection.AddColumn("TABLE_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("COLUMN_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("ORDINAL_POSITION", typeof(int));
			mySqlSchemaCollection.AddColumn("REFERENCED_TABLE_CATALOG", typeof(string));
			mySqlSchemaCollection.AddColumn("REFERENCED_TABLE_SCHEMA", typeof(string));
			mySqlSchemaCollection.AddColumn("REFERENCED_TABLE_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("REFERENCED_COLUMN_NAME", typeof(string));
			string filterName = null;
			if (restrictions != null && restrictions.Length >= 4)
			{
				filterName = restrictions[3];
				restrictions[3] = null;
			}
			MySqlSchemaCollection tables = GetTables(restrictions);
			foreach (MySqlSchemaRow row in tables.Rows)
			{
				GetForeignKeysOnTable(mySqlSchemaCollection, row, filterName, includeColumns: true);
			}
			return mySqlSchemaCollection;
		}

		private string GetSqlMode()
		{
			MySqlCommand mySqlCommand = new MySqlCommand("SELECT @@SQL_MODE", connection);
			return mySqlCommand.ExecuteScalar().ToString();
		}

		private void GetForeignKeysOnTable(MySqlSchemaCollection fkTable, MySqlSchemaRow tableToParse, string filterName, bool includeColumns)
		{
			string sqlMode = GetSqlMode();
			if (filterName != null)
			{
				filterName = StringUtility.ToLowerInvariant(filterName);
			}
			string cmdText = string.Format("SHOW CREATE TABLE `{0}`.`{1}`", tableToParse["TABLE_SCHEMA"], tableToParse["TABLE_NAME"]);
			string input = null;
			string text = null;
			MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection);
			using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
			{
				mySqlDataReader.Read();
				text = mySqlDataReader.GetString(1);
				input = StringUtility.ToLowerInvariant(text);
			}
			MySqlTokenizer mySqlTokenizer = new MySqlTokenizer(input);
			mySqlTokenizer.AnsiQuotes = sqlMode.IndexOf("ANSI_QUOTES") != -1;
			mySqlTokenizer.BackslashEscapes = sqlMode.IndexOf("NO_BACKSLASH_ESCAPES") != -1;
			while (true)
			{
				string text2 = mySqlTokenizer.NextToken();
				while (text2 != null && (text2 != "constraint" || mySqlTokenizer.Quoted))
				{
					text2 = mySqlTokenizer.NextToken();
				}
				if (text2 == null)
				{
					break;
				}
				ParseConstraint(fkTable, tableToParse, mySqlTokenizer, includeColumns);
			}
		}

		private static void ParseConstraint(MySqlSchemaCollection fkTable, MySqlSchemaRow table, MySqlTokenizer tokenizer, bool includeColumns)
		{
			string text = tokenizer.NextToken();
			MySqlSchemaRow mySqlSchemaRow = fkTable.AddRow();
			string text2 = tokenizer.NextToken();
			if (!(text2 != "foreign") && !tokenizer.Quoted)
			{
				tokenizer.NextToken();
				tokenizer.NextToken();
				mySqlSchemaRow["CONSTRAINT_CATALOG"] = table["TABLE_CATALOG"];
				mySqlSchemaRow["CONSTRAINT_SCHEMA"] = table["TABLE_SCHEMA"];
				mySqlSchemaRow["TABLE_CATALOG"] = table["TABLE_CATALOG"];
				mySqlSchemaRow["TABLE_SCHEMA"] = table["TABLE_SCHEMA"];
				mySqlSchemaRow["TABLE_NAME"] = table["TABLE_NAME"];
				mySqlSchemaRow["REFERENCED_TABLE_CATALOG"] = null;
				mySqlSchemaRow["CONSTRAINT_NAME"] = text.Trim('\'', '`');
				List<string> srcColumns = (includeColumns ? ParseColumns(tokenizer) : null);
				while (text2 != "references" || tokenizer.Quoted)
				{
					text2 = tokenizer.NextToken();
				}
				string text3 = tokenizer.NextToken();
				string text4 = tokenizer.NextToken();
				if (text4.StartsWith(".", StringComparison.Ordinal))
				{
					mySqlSchemaRow["REFERENCED_TABLE_SCHEMA"] = text3;
					mySqlSchemaRow["REFERENCED_TABLE_NAME"] = text4.Substring(1).Trim('\'', '`');
					tokenizer.NextToken();
				}
				else
				{
					mySqlSchemaRow["REFERENCED_TABLE_SCHEMA"] = table["TABLE_SCHEMA"];
					mySqlSchemaRow["REFERENCED_TABLE_NAME"] = text3.Substring(1).Trim('\'', '`');
				}
				List<string> targetColumns = (includeColumns ? ParseColumns(tokenizer) : null);
				if (includeColumns)
				{
					ProcessColumns(fkTable, mySqlSchemaRow, srcColumns, targetColumns);
				}
				else
				{
					fkTable.Rows.Add(mySqlSchemaRow);
				}
			}
		}

		private static List<string> ParseColumns(MySqlTokenizer tokenizer)
		{
			List<string> list = new List<string>();
			string text = tokenizer.NextToken();
			while (text != ")")
			{
				if (text != ",")
				{
					list.Add(text);
				}
				text = tokenizer.NextToken();
			}
			return list;
		}

		private static void ProcessColumns(MySqlSchemaCollection fkTable, MySqlSchemaRow row, List<string> srcColumns, List<string> targetColumns)
		{
			for (int i = 0; i < srcColumns.Count; i++)
			{
				MySqlSchemaRow mySqlSchemaRow = fkTable.AddRow();
				row.CopyRow(mySqlSchemaRow);
				mySqlSchemaRow["COLUMN_NAME"] = srcColumns[i];
				mySqlSchemaRow["ORDINAL_POSITION"] = i;
				mySqlSchemaRow["REFERENCED_COLUMN_NAME"] = targetColumns[i];
				fkTable.Rows.Add(mySqlSchemaRow);
			}
		}

		public virtual MySqlSchemaCollection GetUsers(string[] restrictions)
		{
			StringBuilder stringBuilder = new StringBuilder("SELECT Host, User FROM mysql.user");
			if (restrictions != null && restrictions.Length > 0)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " WHERE User LIKE '{0}'", new object[1] { restrictions[0] });
			}
			MySqlSchemaCollection mySqlSchemaCollection = QueryCollection("Users", stringBuilder.ToString());
			mySqlSchemaCollection.Columns[0].Name = "HOST";
			mySqlSchemaCollection.Columns[1].Name = "USERNAME";
			return mySqlSchemaCollection;
		}

		public virtual MySqlSchemaCollection GetProcedures(string[] restrictions)
		{
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection("Procedures");
			mySqlSchemaCollection.AddColumn("SPECIFIC_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("ROUTINE_CATALOG", typeof(string));
			mySqlSchemaCollection.AddColumn("ROUTINE_SCHEMA", typeof(string));
			mySqlSchemaCollection.AddColumn("ROUTINE_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("ROUTINE_TYPE", typeof(string));
			mySqlSchemaCollection.AddColumn("DTD_IDENTIFIER", typeof(string));
			mySqlSchemaCollection.AddColumn("ROUTINE_BODY", typeof(string));
			mySqlSchemaCollection.AddColumn("ROUTINE_DEFINITION", typeof(string));
			mySqlSchemaCollection.AddColumn("EXTERNAL_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("EXTERNAL_LANGUAGE", typeof(string));
			mySqlSchemaCollection.AddColumn("PARAMETER_STYLE", typeof(string));
			mySqlSchemaCollection.AddColumn("IS_DETERMINISTIC", typeof(string));
			mySqlSchemaCollection.AddColumn("SQL_DATA_ACCESS", typeof(string));
			mySqlSchemaCollection.AddColumn("SQL_PATH", typeof(string));
			mySqlSchemaCollection.AddColumn("SECURITY_TYPE", typeof(string));
			mySqlSchemaCollection.AddColumn("CREATED", typeof(DateTime));
			mySqlSchemaCollection.AddColumn("LAST_ALTERED", typeof(DateTime));
			mySqlSchemaCollection.AddColumn("SQL_MODE", typeof(string));
			mySqlSchemaCollection.AddColumn("ROUTINE_COMMENT", typeof(string));
			mySqlSchemaCollection.AddColumn("DEFINER", typeof(string));
			StringBuilder stringBuilder = new StringBuilder("SELECT * FROM mysql.proc WHERE 1=1");
			if (restrictions != null)
			{
				if (restrictions.Length >= 2 && restrictions[1] != null)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " AND db LIKE '{0}'", new object[1] { restrictions[1] });
				}
				if (restrictions.Length >= 3 && restrictions[2] != null)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " AND name LIKE '{0}'", new object[1] { restrictions[2] });
				}
				if (restrictions.Length >= 4 && restrictions[3] != null)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " AND type LIKE '{0}'", new object[1] { restrictions[3] });
				}
			}
			MySqlCommand mySqlCommand = new MySqlCommand(stringBuilder.ToString(), connection);
			using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			while (mySqlDataReader.Read())
			{
				MySqlSchemaRow mySqlSchemaRow = mySqlSchemaCollection.AddRow();
				mySqlSchemaRow["SPECIFIC_NAME"] = mySqlDataReader.GetString("specific_name");
				mySqlSchemaRow["ROUTINE_CATALOG"] = DBNull.Value;
				mySqlSchemaRow["ROUTINE_SCHEMA"] = mySqlDataReader.GetString("db");
				mySqlSchemaRow["ROUTINE_NAME"] = mySqlDataReader.GetString("name");
				string s = (string)(mySqlSchemaRow["ROUTINE_TYPE"] = mySqlDataReader.GetString("type"));
				mySqlSchemaRow["DTD_IDENTIFIER"] = ((StringUtility.ToLowerInvariant(s) == "function") ? ((IConvertible)mySqlDataReader.GetString("returns")) : ((IConvertible)DBNull.Value));
				mySqlSchemaRow["ROUTINE_BODY"] = "SQL";
				mySqlSchemaRow["ROUTINE_DEFINITION"] = mySqlDataReader.GetString("body");
				mySqlSchemaRow["EXTERNAL_NAME"] = DBNull.Value;
				mySqlSchemaRow["EXTERNAL_LANGUAGE"] = DBNull.Value;
				mySqlSchemaRow["PARAMETER_STYLE"] = "SQL";
				mySqlSchemaRow["IS_DETERMINISTIC"] = mySqlDataReader.GetString("is_deterministic");
				mySqlSchemaRow["SQL_DATA_ACCESS"] = mySqlDataReader.GetString("sql_data_access");
				mySqlSchemaRow["SQL_PATH"] = DBNull.Value;
				mySqlSchemaRow["SECURITY_TYPE"] = mySqlDataReader.GetString("security_type");
				mySqlSchemaRow["CREATED"] = mySqlDataReader.GetDateTime("created");
				mySqlSchemaRow["LAST_ALTERED"] = mySqlDataReader.GetDateTime("modified");
				mySqlSchemaRow["SQL_MODE"] = mySqlDataReader.GetString("sql_mode");
				mySqlSchemaRow["ROUTINE_COMMENT"] = mySqlDataReader.GetString("comment");
				mySqlSchemaRow["DEFINER"] = mySqlDataReader.GetString("definer");
			}
			return mySqlSchemaCollection;
		}

		protected virtual MySqlSchemaCollection GetCollections()
		{
			object[][] data = new object[14][]
			{
				new object[3] { "MetaDataCollections", 0, 0 },
				new object[3] { "DataSourceInformation", 0, 0 },
				new object[3] { "DataTypes", 0, 0 },
				new object[3] { "Restrictions", 0, 0 },
				new object[3] { "ReservedWords", 0, 0 },
				new object[3] { "Databases", 1, 1 },
				new object[3] { "Tables", 4, 2 },
				new object[3] { "Columns", 4, 4 },
				new object[3] { "Users", 1, 1 },
				new object[3] { "Foreign Keys", 4, 3 },
				new object[3] { "IndexColumns", 5, 4 },
				new object[3] { "Indexes", 4, 3 },
				new object[3] { "Foreign Key Columns", 4, 3 },
				new object[3] { "UDF", 1, 1 }
			};
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection("MetaDataCollections");
			mySqlSchemaCollection.AddColumn("CollectionName", typeof(string));
			mySqlSchemaCollection.AddColumn("NumberOfRestrictions", typeof(int));
			mySqlSchemaCollection.AddColumn("NumberOfIdentifierParts", typeof(int));
			FillTable(mySqlSchemaCollection, data);
			return mySqlSchemaCollection;
		}

		private MySqlSchemaCollection GetDataSourceInformation()
		{
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection("DataSourceInformation");
			mySqlSchemaCollection.AddColumn("CompositeIdentifierSeparatorPattern", typeof(string));
			mySqlSchemaCollection.AddColumn("DataSourceProductName", typeof(string));
			mySqlSchemaCollection.AddColumn("DataSourceProductVersion", typeof(string));
			mySqlSchemaCollection.AddColumn("DataSourceProductVersionNormalized", typeof(string));
			mySqlSchemaCollection.AddColumn("GroupByBehavior", typeof(GroupByBehavior));
			mySqlSchemaCollection.AddColumn("IdentifierPattern", typeof(string));
			mySqlSchemaCollection.AddColumn("IdentifierCase", typeof(IdentifierCase));
			mySqlSchemaCollection.AddColumn("OrderByColumnsInSelect", typeof(bool));
			mySqlSchemaCollection.AddColumn("ParameterMarkerFormat", typeof(string));
			mySqlSchemaCollection.AddColumn("ParameterMarkerPattern", typeof(string));
			mySqlSchemaCollection.AddColumn("ParameterNameMaxLength", typeof(int));
			mySqlSchemaCollection.AddColumn("ParameterNamePattern", typeof(string));
			mySqlSchemaCollection.AddColumn("QuotedIdentifierPattern", typeof(string));
			mySqlSchemaCollection.AddColumn("QuotedIdentifierCase", typeof(IdentifierCase));
			mySqlSchemaCollection.AddColumn("StatementSeparatorPattern", typeof(string));
			mySqlSchemaCollection.AddColumn("StringLiteralPattern", typeof(string));
			mySqlSchemaCollection.AddColumn("SupportedJoinOperators", typeof(SupportedJoinOperators));
			DBVersion version = connection.driver.Version;
			string value = $"{version.Major:0}.{version.Minor:0}.{version.Build:0}";
			MySqlSchemaRow mySqlSchemaRow = mySqlSchemaCollection.AddRow();
			mySqlSchemaRow["CompositeIdentifierSeparatorPattern"] = "\\.";
			mySqlSchemaRow["DataSourceProductName"] = "MySQL";
			mySqlSchemaRow["DataSourceProductVersion"] = connection.ServerVersion;
			mySqlSchemaRow["DataSourceProductVersionNormalized"] = value;
			mySqlSchemaRow["GroupByBehavior"] = GroupByBehavior.Unrelated;
			mySqlSchemaRow["IdentifierPattern"] = "(^\\`\\p{Lo}\\p{Lu}\\p{Ll}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Nd}@$#_]*$)|(^\\`[^\\`\\0]|\\`\\`+\\`$)|(^\\\" + [^\\\"\\0]|\\\"\\\"+\\\"$)";
			mySqlSchemaRow["IdentifierCase"] = IdentifierCase.Insensitive;
			mySqlSchemaRow["OrderByColumnsInSelect"] = false;
			mySqlSchemaRow["ParameterMarkerFormat"] = "{0}";
			mySqlSchemaRow["ParameterMarkerPattern"] = "(@[A-Za-z0-9_$#]*)";
			mySqlSchemaRow["ParameterNameMaxLength"] = 128;
			mySqlSchemaRow["ParameterNamePattern"] = "^[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\\uff3f_@#\\$]*(?=\\s+|$)";
			mySqlSchemaRow["QuotedIdentifierPattern"] = "(([^\\`]|\\`\\`)*)";
			mySqlSchemaRow["QuotedIdentifierCase"] = IdentifierCase.Sensitive;
			mySqlSchemaRow["StatementSeparatorPattern"] = ";";
			mySqlSchemaRow["StringLiteralPattern"] = "'(([^']|'')*)'";
			mySqlSchemaRow["SupportedJoinOperators"] = 15;
			mySqlSchemaCollection.Rows.Add(mySqlSchemaRow);
			return mySqlSchemaCollection;
		}

		private static MySqlSchemaCollection GetDataTypes()
		{
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection("DataTypes");
			mySqlSchemaCollection.AddColumn("TypeName", typeof(string));
			mySqlSchemaCollection.AddColumn("ProviderDbType", typeof(int));
			mySqlSchemaCollection.AddColumn("ColumnSize", typeof(long));
			mySqlSchemaCollection.AddColumn("CreateFormat", typeof(string));
			mySqlSchemaCollection.AddColumn("CreateParameters", typeof(string));
			mySqlSchemaCollection.AddColumn("DataType", typeof(string));
			mySqlSchemaCollection.AddColumn("IsAutoincrementable", typeof(bool));
			mySqlSchemaCollection.AddColumn("IsBestMatch", typeof(bool));
			mySqlSchemaCollection.AddColumn("IsCaseSensitive", typeof(bool));
			mySqlSchemaCollection.AddColumn("IsFixedLength", typeof(bool));
			mySqlSchemaCollection.AddColumn("IsFixedPrecisionScale", typeof(bool));
			mySqlSchemaCollection.AddColumn("IsLong", typeof(bool));
			mySqlSchemaCollection.AddColumn("IsNullable", typeof(bool));
			mySqlSchemaCollection.AddColumn("IsSearchable", typeof(bool));
			mySqlSchemaCollection.AddColumn("IsSearchableWithLike", typeof(bool));
			mySqlSchemaCollection.AddColumn("IsUnsigned", typeof(bool));
			mySqlSchemaCollection.AddColumn("MaximumScale", typeof(short));
			mySqlSchemaCollection.AddColumn("MinimumScale", typeof(short));
			mySqlSchemaCollection.AddColumn("IsConcurrencyType", typeof(bool));
			mySqlSchemaCollection.AddColumn("IsLiteralSupported", typeof(bool));
			mySqlSchemaCollection.AddColumn("LiteralPrefix", typeof(string));
			mySqlSchemaCollection.AddColumn("LiteralSuffix", typeof(string));
			mySqlSchemaCollection.AddColumn("NativeDataType", typeof(string));
			MySqlBit.SetDSInfo(mySqlSchemaCollection);
			MySqlBinary.SetDSInfo(mySqlSchemaCollection);
			MySqlDateTime.SetDSInfo(mySqlSchemaCollection);
			MySqlTimeSpan.SetDSInfo(mySqlSchemaCollection);
			MySqlString.SetDSInfo(mySqlSchemaCollection);
			MySqlDouble.SetDSInfo(mySqlSchemaCollection);
			MySqlSingle.SetDSInfo(mySqlSchemaCollection);
			MySqlByte.SetDSInfo(mySqlSchemaCollection);
			MySqlInt16.SetDSInfo(mySqlSchemaCollection);
			MySqlInt32.SetDSInfo(mySqlSchemaCollection);
			MySqlInt64.SetDSInfo(mySqlSchemaCollection);
			MySqlDecimal.SetDSInfo(mySqlSchemaCollection);
			MySqlUByte.SetDSInfo(mySqlSchemaCollection);
			MySqlUInt16.SetDSInfo(mySqlSchemaCollection);
			MySqlUInt32.SetDSInfo(mySqlSchemaCollection);
			MySqlUInt64.SetDSInfo(mySqlSchemaCollection);
			return mySqlSchemaCollection;
		}

		protected virtual MySqlSchemaCollection GetRestrictions()
		{
			object[][] data = new object[28][]
			{
				new object[4] { "Users", "Name", "", 0 },
				new object[4] { "Databases", "Name", "", 0 },
				new object[4] { "Tables", "Database", "", 0 },
				new object[4] { "Tables", "Schema", "", 1 },
				new object[4] { "Tables", "Table", "", 2 },
				new object[4] { "Tables", "TableType", "", 3 },
				new object[4] { "Columns", "Database", "", 0 },
				new object[4] { "Columns", "Schema", "", 1 },
				new object[4] { "Columns", "Table", "", 2 },
				new object[4] { "Columns", "Column", "", 3 },
				new object[4] { "Indexes", "Database", "", 0 },
				new object[4] { "Indexes", "Schema", "", 1 },
				new object[4] { "Indexes", "Table", "", 2 },
				new object[4] { "Indexes", "Name", "", 3 },
				new object[4] { "IndexColumns", "Database", "", 0 },
				new object[4] { "IndexColumns", "Schema", "", 1 },
				new object[4] { "IndexColumns", "Table", "", 2 },
				new object[4] { "IndexColumns", "ConstraintName", "", 3 },
				new object[4] { "IndexColumns", "Column", "", 4 },
				new object[4] { "Foreign Keys", "Database", "", 0 },
				new object[4] { "Foreign Keys", "Schema", "", 1 },
				new object[4] { "Foreign Keys", "Table", "", 2 },
				new object[4] { "Foreign Keys", "Constraint Name", "", 3 },
				new object[4] { "Foreign Key Columns", "Catalog", "", 0 },
				new object[4] { "Foreign Key Columns", "Schema", "", 1 },
				new object[4] { "Foreign Key Columns", "Table", "", 2 },
				new object[4] { "Foreign Key Columns", "Constraint Name", "", 3 },
				new object[4] { "UDF", "Name", "", 0 }
			};
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection("Restrictions");
			mySqlSchemaCollection.AddColumn("CollectionName", typeof(string));
			mySqlSchemaCollection.AddColumn("RestrictionName", typeof(string));
			mySqlSchemaCollection.AddColumn("RestrictionDefault", typeof(string));
			mySqlSchemaCollection.AddColumn("RestrictionNumber", typeof(int));
			FillTable(mySqlSchemaCollection, data);
			return mySqlSchemaCollection;
		}

		private static MySqlSchemaCollection GetReservedWords()
		{
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection("ReservedWords");
			mySqlSchemaCollection.AddColumn(DbMetaDataColumnNames.ReservedWord, typeof(string));
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MySql.Data.MySqlClient.Properties.ReservedWords.txt");
			StreamReader streamReader = new StreamReader(manifestResourceStream);
			for (string text = streamReader.ReadLine(); text != null; text = streamReader.ReadLine())
			{
				string[] array = text.Split(new char[1] { ' ' });
				string[] array2 = array;
				foreach (string value in array2)
				{
					if (!string.IsNullOrEmpty(value))
					{
						MySqlSchemaRow mySqlSchemaRow = mySqlSchemaCollection.AddRow();
						mySqlSchemaRow[0] = value;
					}
				}
			}
			streamReader.Dispose();
			manifestResourceStream.Close();
			return mySqlSchemaCollection;
		}

		protected static void FillTable(MySqlSchemaCollection dt, object[][] data)
		{
			foreach (object[] array in data)
			{
				MySqlSchemaRow mySqlSchemaRow = dt.AddRow();
				for (int j = 0; j < array.Length; j++)
				{
					mySqlSchemaRow[j] = array[j];
				}
			}
		}

		private void FindTables(MySqlSchemaCollection schema, string[] restrictions)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "SHOW TABLE STATUS FROM `{0}`", new object[1] { restrictions[1] });
			if (restrictions != null && restrictions.Length >= 3 && restrictions[2] != null)
			{
				stringBuilder2.AppendFormat(CultureInfo.InvariantCulture, " LIKE '{0}'", new object[1] { restrictions[2] });
			}
			stringBuilder.Append(stringBuilder2.ToString());
			string value = ((restrictions[1].ToLower() == "information_schema") ? "SYSTEM VIEW" : "BASE TABLE");
			MySqlCommand mySqlCommand = new MySqlCommand(stringBuilder.ToString(), connection);
			using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			while (mySqlDataReader.Read())
			{
				MySqlSchemaRow mySqlSchemaRow = schema.AddRow();
				mySqlSchemaRow["TABLE_CATALOG"] = null;
				mySqlSchemaRow["TABLE_SCHEMA"] = restrictions[1];
				mySqlSchemaRow["TABLE_NAME"] = mySqlDataReader.GetString(0);
				mySqlSchemaRow["TABLE_TYPE"] = value;
				mySqlSchemaRow["ENGINE"] = GetString(mySqlDataReader, 1);
				mySqlSchemaRow["VERSION"] = mySqlDataReader.GetValue(2);
				mySqlSchemaRow["ROW_FORMAT"] = GetString(mySqlDataReader, 3);
				mySqlSchemaRow["TABLE_ROWS"] = mySqlDataReader.GetValue(4);
				mySqlSchemaRow["AVG_ROW_LENGTH"] = mySqlDataReader.GetValue(5);
				mySqlSchemaRow["DATA_LENGTH"] = mySqlDataReader.GetValue(6);
				mySqlSchemaRow["MAX_DATA_LENGTH"] = mySqlDataReader.GetValue(7);
				mySqlSchemaRow["INDEX_LENGTH"] = mySqlDataReader.GetValue(8);
				mySqlSchemaRow["DATA_FREE"] = mySqlDataReader.GetValue(9);
				mySqlSchemaRow["AUTO_INCREMENT"] = mySqlDataReader.GetValue(10);
				mySqlSchemaRow["CREATE_TIME"] = mySqlDataReader.GetValue(11);
				mySqlSchemaRow["UPDATE_TIME"] = mySqlDataReader.GetValue(12);
				mySqlSchemaRow["CHECK_TIME"] = mySqlDataReader.GetValue(13);
				mySqlSchemaRow["TABLE_COLLATION"] = GetString(mySqlDataReader, 14);
				mySqlSchemaRow["CHECKSUM"] = mySqlDataReader.GetValue(15);
				mySqlSchemaRow["CREATE_OPTIONS"] = GetString(mySqlDataReader, 16);
				mySqlSchemaRow["TABLE_COMMENT"] = GetString(mySqlDataReader, 17);
			}
		}

		private static string GetString(MySqlDataReader reader, int index)
		{
			if (reader.IsDBNull(index))
			{
				return null;
			}
			return reader.GetString(index);
		}

		public virtual MySqlSchemaCollection GetUDF(string[] restrictions)
		{
			string text = "SELECT name,ret,dl FROM mysql.func";
			if (restrictions != null && restrictions.Length >= 1 && !string.IsNullOrEmpty(restrictions[0]))
			{
				text += $" WHERE name LIKE '{restrictions[0]}'";
			}
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection("User-defined Functions");
			mySqlSchemaCollection.AddColumn("NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("RETURN_TYPE", typeof(int));
			mySqlSchemaCollection.AddColumn("LIBRARY_NAME", typeof(string));
			MySqlCommand mySqlCommand = new MySqlCommand(text, connection);
			try
			{
				using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
				while (mySqlDataReader.Read())
				{
					MySqlSchemaRow mySqlSchemaRow = mySqlSchemaCollection.AddRow();
					mySqlSchemaRow[0] = mySqlDataReader.GetString(0);
					mySqlSchemaRow[1] = mySqlDataReader.GetInt32(1);
					mySqlSchemaRow[2] = mySqlDataReader.GetString(2);
				}
				return mySqlSchemaCollection;
			}
			catch (MySqlException ex)
			{
				if (ex.Number != 1142)
				{
					throw;
				}
				throw new MySqlException(Resources.UnableToEnumerateUDF, ex);
			}
		}

		protected virtual MySqlSchemaCollection GetSchemaInternal(string collection, string[] restrictions)
		{
			switch (collection)
			{
			case "METADATACOLLECTIONS":
				return GetCollections();
			case "DATASOURCEINFORMATION":
				return GetDataSourceInformation();
			case "DATATYPES":
				return GetDataTypes();
			case "RESTRICTIONS":
				return GetRestrictions();
			case "RESERVEDWORDS":
				return GetReservedWords();
			case "USERS":
				return GetUsers(restrictions);
			case "DATABASES":
				return GetDatabases(restrictions);
			case "UDF":
				return GetUDF(restrictions);
			default:
				if (restrictions == null)
				{
					restrictions = new string[2];
				}
				if (connection != null && connection.Database != null && connection.Database.Length > 0 && restrictions.Length > 1 && restrictions[1] == null)
				{
					restrictions[1] = connection.Database;
				}
				return collection switch
				{
					"TABLES" => GetTables(restrictions), 
					"COLUMNS" => GetColumns(restrictions), 
					"INDEXES" => GetIndexes(restrictions), 
					"INDEXCOLUMNS" => GetIndexColumns(restrictions), 
					"FOREIGN KEYS" => GetForeignKeys(restrictions), 
					"FOREIGN KEY COLUMNS" => GetForeignKeyColumns(restrictions), 
					_ => null, 
				};
			}
		}

		internal string[] CleanRestrictions(string[] restrictionValues)
		{
			string[] array = null;
			if (restrictionValues != null)
			{
				array = (string[])restrictionValues.Clone();
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					if (text != null)
					{
						array[i] = text.Trim(new char[1] { '`' });
					}
				}
			}
			return array;
		}

		protected MySqlSchemaCollection QueryCollection(string name, string sql)
		{
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection(name);
			MySqlCommand mySqlCommand = new MySqlCommand(sql, connection);
			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			for (int i = 0; i < mySqlDataReader.FieldCount; i++)
			{
				mySqlSchemaCollection.AddColumn(mySqlDataReader.GetName(i), mySqlDataReader.GetFieldType(i));
			}
			using (mySqlDataReader)
			{
				while (mySqlDataReader.Read())
				{
					MySqlSchemaRow mySqlSchemaRow = mySqlSchemaCollection.AddRow();
					for (int j = 0; j < mySqlDataReader.FieldCount; j++)
					{
						mySqlSchemaRow[j] = mySqlDataReader.GetValue(j);
					}
				}
				return mySqlSchemaCollection;
			}
		}
	}
	internal class ISSchemaProvider : SchemaProvider
	{
		public ISSchemaProvider(MySqlConnection connection)
			: base(connection)
		{
		}

		protected override MySqlSchemaCollection GetCollections()
		{
			MySqlSchemaCollection collections = base.GetCollections();
			object[][] data = new object[5][]
			{
				new object[3] { "Views", 2, 3 },
				new object[3] { "ViewColumns", 3, 4 },
				new object[3] { "Procedure Parameters", 5, 1 },
				new object[3] { "Procedures", 4, 3 },
				new object[3] { "Triggers", 2, 4 }
			};
			SchemaProvider.FillTable(collections, data);
			return collections;
		}

		protected override MySqlSchemaCollection GetRestrictions()
		{
			MySqlSchemaCollection restrictions = base.GetRestrictions();
			object[][] data = new object[20][]
			{
				new object[4] { "Procedure Parameters", "Database", "", 0 },
				new object[4] { "Procedure Parameters", "Schema", "", 1 },
				new object[4] { "Procedure Parameters", "Name", "", 2 },
				new object[4] { "Procedure Parameters", "Type", "", 3 },
				new object[4] { "Procedure Parameters", "Parameter", "", 4 },
				new object[4] { "Procedures", "Database", "", 0 },
				new object[4] { "Procedures", "Schema", "", 1 },
				new object[4] { "Procedures", "Name", "", 2 },
				new object[4] { "Procedures", "Type", "", 3 },
				new object[4] { "Views", "Database", "", 0 },
				new object[4] { "Views", "Schema", "", 1 },
				new object[4] { "Views", "Table", "", 2 },
				new object[4] { "ViewColumns", "Database", "", 0 },
				new object[4] { "ViewColumns", "Schema", "", 1 },
				new object[4] { "ViewColumns", "Table", "", 2 },
				new object[4] { "ViewColumns", "Column", "", 3 },
				new object[4] { "Triggers", "Database", "", 0 },
				new object[4] { "Triggers", "Schema", "", 1 },
				new object[4] { "Triggers", "Name", "", 2 },
				new object[4] { "Triggers", "EventObjectTable", "", 3 }
			};
			SchemaProvider.FillTable(restrictions, data);
			return restrictions;
		}

		public override MySqlSchemaCollection GetDatabases(string[] restrictions)
		{
			MySqlSchemaCollection mySqlSchemaCollection = Query("SCHEMATA", "", new string[1] { "SCHEMA_NAME" }, restrictions);
			mySqlSchemaCollection.Columns[1].Name = "database_name";
			mySqlSchemaCollection.Name = "Databases";
			return mySqlSchemaCollection;
		}

		public override MySqlSchemaCollection GetTables(string[] restrictions)
		{
			MySqlSchemaCollection mySqlSchemaCollection = Query("TABLES", "TABLE_TYPE != 'VIEW'", new string[4] { "TABLE_CATALOG", "TABLE_SCHEMA", "TABLE_NAME", "TABLE_TYPE" }, restrictions);
			mySqlSchemaCollection.Name = "Tables";
			return mySqlSchemaCollection;
		}

		public override MySqlSchemaCollection GetColumns(string[] restrictions)
		{
			MySqlSchemaCollection mySqlSchemaCollection = Query("COLUMNS", null, new string[4] { "TABLE_CATALOG", "TABLE_SCHEMA", "TABLE_NAME", "COLUMN_NAME" }, restrictions);
			mySqlSchemaCollection.RemoveColumn("CHARACTER_OCTET_LENGTH");
			mySqlSchemaCollection.Name = "Columns";
			QuoteDefaultValues(mySqlSchemaCollection);
			return mySqlSchemaCollection;
		}

		private MySqlSchemaCollection GetViews(string[] restrictions)
		{
			MySqlSchemaCollection mySqlSchemaCollection = Query("VIEWS", null, new string[3] { "TABLE_CATALOG", "TABLE_SCHEMA", "TABLE_NAME" }, restrictions);
			mySqlSchemaCollection.Name = "Views";
			return mySqlSchemaCollection;
		}

		private MySqlSchemaCollection GetViewColumns(string[] restrictions)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder("SELECT C.* FROM information_schema.columns C");
			stringBuilder2.Append(" JOIN information_schema.views V ");
			stringBuilder2.Append("ON C.table_schema=V.table_schema AND C.table_name=V.table_name ");
			if (restrictions != null && restrictions.Length >= 2 && restrictions[1] != null)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "C.table_schema='{0}' ", new object[1] { restrictions[1] });
			}
			if (restrictions != null && restrictions.Length >= 3 && restrictions[2] != null)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("AND ");
				}
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "C.table_name='{0}' ", new object[1] { restrictions[2] });
			}
			if (restrictions != null && restrictions.Length == 4 && restrictions[3] != null)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("AND ");
				}
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "C.column_name='{0}' ", new object[1] { restrictions[3] });
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder2.AppendFormat(CultureInfo.InvariantCulture, " WHERE {0}", new object[1] { stringBuilder });
			}
			MySqlSchemaCollection table = GetTable(stringBuilder2.ToString());
			table.Name = "ViewColumns";
			table.Columns[0].Name = "VIEW_CATALOG";
			table.Columns[1].Name = "VIEW_SCHEMA";
			table.Columns[2].Name = "VIEW_NAME";
			QuoteDefaultValues(table);
			return table;
		}

		private MySqlSchemaCollection GetTriggers(string[] restrictions)
		{
			MySqlSchemaCollection mySqlSchemaCollection = Query("TRIGGERS", null, new string[4] { "TRIGGER_CATALOG", "TRIGGER_SCHEMA", "EVENT_OBJECT_TABLE", "TRIGGER_NAME" }, restrictions);
			mySqlSchemaCollection.Name = "Triggers";
			return mySqlSchemaCollection;
		}

		public override MySqlSchemaCollection GetProcedures(string[] restrictions)
		{
			try
			{
				if (connection.Settings.HasProcAccess)
				{
					return base.GetProcedures(restrictions);
				}
			}
			catch (MySqlException ex)
			{
				if (ex.Number != 1142)
				{
					throw;
				}
				connection.Settings.HasProcAccess = false;
			}
			MySqlSchemaCollection mySqlSchemaCollection = Query("ROUTINES", null, new string[4] { "ROUTINE_CATALOG", "ROUTINE_SCHEMA", "ROUTINE_NAME", "ROUTINE_TYPE" }, restrictions);
			mySqlSchemaCollection.Name = "Procedures";
			return mySqlSchemaCollection;
		}

		private MySqlSchemaCollection GetProceduresWithParameters(string[] restrictions)
		{
			MySqlSchemaCollection procedures = GetProcedures(restrictions);
			procedures.AddColumn("ParameterList", typeof(string));
			foreach (MySqlSchemaRow row in procedures.Rows)
			{
				row["ParameterList"] = GetProcedureParameterLine(row);
			}
			return procedures;
		}

		private string GetProcedureParameterLine(MySqlSchemaRow isRow)
		{
			string format = "SHOW CREATE {0} `{1}`.`{2}`";
			format = string.Format(format, isRow["ROUTINE_TYPE"], isRow["ROUTINE_SCHEMA"], isRow["ROUTINE_NAME"]);
			MySqlCommand mySqlCommand = new MySqlCommand(format, connection);
			using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			mySqlDataReader.Read();
			if (mySqlDataReader.IsDBNull(2))
			{
				return null;
			}
			string text = mySqlDataReader.GetString(1);
			string text2 = mySqlDataReader.GetString(2);
			MySqlTokenizer mySqlTokenizer = new MySqlTokenizer(text2);
			mySqlTokenizer.AnsiQuotes = text.IndexOf("ANSI_QUOTES") != -1;
			mySqlTokenizer.BackslashEscapes = text.IndexOf("NO_BACKSLASH_ESCAPES") == -1;
			string text3 = mySqlTokenizer.NextToken();
			while (text3 != "(")
			{
				text3 = mySqlTokenizer.NextToken();
			}
			int num = mySqlTokenizer.StartIndex + 1;
			text3 = mySqlTokenizer.NextToken();
			while (text3 != ")" || mySqlTokenizer.Quoted)
			{
				text3 = mySqlTokenizer.NextToken();
				if (text3 == "(" && !mySqlTokenizer.Quoted)
				{
					while (text3 != ")" || mySqlTokenizer.Quoted)
					{
						text3 = mySqlTokenizer.NextToken();
					}
					text3 = mySqlTokenizer.NextToken();
				}
			}
			return text2.Substring(num, mySqlTokenizer.StartIndex - num);
		}

		private MySqlSchemaCollection GetParametersForRoutineFromIS(string[] restrictions)
		{
			string[] keys = new string[5] { "SPECIFIC_CATALOG", "SPECIFIC_SCHEMA", "SPECIFIC_NAME", "ROUTINE_TYPE", "PARAMETER_NAME" };
			StringBuilder stringBuilder = new StringBuilder("SELECT * FROM INFORMATION_SCHEMA.PARAMETERS");
			string whereClause = GetWhereClause(null, keys, restrictions);
			if (!string.IsNullOrEmpty(whereClause))
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " WHERE {0}", new object[1] { whereClause });
			}
			MySqlSchemaCollection mySqlSchemaCollection = QueryCollection("parameters", stringBuilder.ToString());
			if (mySqlSchemaCollection.Rows.Count != 0 && (string)mySqlSchemaCollection.Rows[0]["routine_type"] == "FUNCTION")
			{
				mySqlSchemaCollection.Rows[0]["parameter_mode"] = "IN";
				mySqlSchemaCollection.Rows[0]["parameter_name"] = "return_value";
			}
			return mySqlSchemaCollection;
		}

		private MySqlSchemaCollection GetParametersFromIS(string[] restrictions, MySqlSchemaCollection routines)
		{
			MySqlSchemaCollection mySqlSchemaCollection = null;
			if (routines == null || routines.Rows.Count == 0)
			{
				mySqlSchemaCollection = ((restrictions != null) ? GetParametersForRoutineFromIS(restrictions) : QueryCollection("parameters", "SELECT * FROM INFORMATION_SCHEMA.PARAMETERS WHERE 1=2"));
			}
			else
			{
				foreach (MySqlSchemaRow row in routines.Rows)
				{
					if (restrictions != null && restrictions.Length >= 3)
					{
						restrictions[2] = row["ROUTINE_NAME"].ToString();
					}
					mySqlSchemaCollection = GetParametersForRoutineFromIS(restrictions);
				}
			}
			mySqlSchemaCollection.Name = "Procedure Parameters";
			return mySqlSchemaCollection;
		}

		internal MySqlSchemaCollection CreateParametersTable()
		{
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection("Procedure Parameters");
			mySqlSchemaCollection.AddColumn("SPECIFIC_CATALOG", typeof(string));
			mySqlSchemaCollection.AddColumn("SPECIFIC_SCHEMA", typeof(string));
			mySqlSchemaCollection.AddColumn("SPECIFIC_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("ORDINAL_POSITION", typeof(int));
			mySqlSchemaCollection.AddColumn("PARAMETER_MODE", typeof(string));
			mySqlSchemaCollection.AddColumn("PARAMETER_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("DATA_TYPE", typeof(string));
			mySqlSchemaCollection.AddColumn("CHARACTER_MAXIMUM_LENGTH", typeof(int));
			mySqlSchemaCollection.AddColumn("CHARACTER_OCTET_LENGTH", typeof(int));
			mySqlSchemaCollection.AddColumn("NUMERIC_PRECISION", typeof(byte));
			mySqlSchemaCollection.AddColumn("NUMERIC_SCALE", typeof(int));
			mySqlSchemaCollection.AddColumn("CHARACTER_SET_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("COLLATION_NAME", typeof(string));
			mySqlSchemaCollection.AddColumn("DTD_IDENTIFIER", typeof(string));
			mySqlSchemaCollection.AddColumn("ROUTINE_TYPE", typeof(string));
			return mySqlSchemaCollection;
		}

		public virtual MySqlSchemaCollection GetProcedureParameters(string[] restrictions, MySqlSchemaCollection routines)
		{
			bool flag = connection.driver.Version.isAtLeast(5, 5, 3);
			try
			{
				MySqlSchemaCollection mySqlSchemaCollection = CreateParametersTable();
				GetParametersFromShowCreate(mySqlSchemaCollection, restrictions, routines);
				return mySqlSchemaCollection;
			}
			catch (Exception)
			{
				if (!flag)
				{
					throw;
				}
				return GetParametersFromIS(restrictions, routines);
			}
		}

		protected override MySqlSchemaCollection GetSchemaInternal(string collection, string[] restrictions)
		{
			MySqlSchemaCollection schemaInternal = base.GetSchemaInternal(collection, restrictions);
			if (schemaInternal != null)
			{
				return schemaInternal;
			}
			return collection switch
			{
				"VIEWS" => GetViews(restrictions), 
				"PROCEDURES" => GetProcedures(restrictions), 
				"PROCEDURES WITH PARAMETERS" => GetProceduresWithParameters(restrictions), 
				"PROCEDURE PARAMETERS" => GetProcedureParameters(restrictions, null), 
				"TRIGGERS" => GetTriggers(restrictions), 
				"VIEWCOLUMNS" => GetViewColumns(restrictions), 
				_ => null, 
			};
		}

		private static string GetWhereClause(string initial_where, string[] keys, string[] values)
		{
			StringBuilder stringBuilder = new StringBuilder(initial_where);
			if (values != null)
			{
				for (int i = 0; i < keys.Length && i < values.Length; i++)
				{
					if (values[i] != null && !(values[i] == string.Empty))
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(" AND ");
						}
						stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} LIKE '{1}'", new object[2]
						{
							keys[i],
							values[i]
						});
					}
				}
			}
			return stringBuilder.ToString();
		}

		private MySqlSchemaCollection Query(string table_name, string initial_where, string[] keys, string[] values)
		{
			StringBuilder stringBuilder = new StringBuilder("SELECT * FROM INFORMATION_SCHEMA.");
			stringBuilder.Append(table_name);
			string whereClause = GetWhereClause(initial_where, keys, values);
			if (whereClause.Length > 0)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " WHERE {0}", new object[1] { whereClause });
			}
			return GetTable(stringBuilder.ToString());
		}

		private MySqlSchemaCollection GetTable(string sql)
		{
			MySqlSchemaCollection mySqlSchemaCollection = new MySqlSchemaCollection();
			MySqlCommand mySqlCommand = new MySqlCommand(sql, connection);
			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
			for (int i = 0; i < mySqlDataReader.FieldCount; i++)
			{
				mySqlSchemaCollection.AddColumn(mySqlDataReader.GetName(i), mySqlDataReader.GetFieldType(i));
			}
			using (mySqlDataReader)
			{
				while (mySqlDataReader.Read())
				{
					MySqlSchemaRow mySqlSchemaRow = mySqlSchemaCollection.AddRow();
					for (int j = 0; j < mySqlDataReader.FieldCount; j++)
					{
						mySqlSchemaRow[j] = mySqlDataReader.GetValue(j);
					}
				}
				return mySqlSchemaCollection;
			}
		}

		public override MySqlSchemaCollection GetForeignKeys(string[] restrictions)
		{
			if (!connection.driver.Version.isAtLeast(5, 1, 16))
			{
				return base.GetForeignKeys(restrictions);
			}
			string text = "SELECT rc.constraint_catalog, rc.constraint_schema,\r\n                rc.constraint_name, kcu.table_catalog, kcu.table_schema, rc.table_name,\r\n                rc.match_option, rc.update_rule, rc.delete_rule, \r\n                NULL as referenced_table_catalog,\r\n                kcu.referenced_table_schema, rc.referenced_table_name \r\n                FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc\r\n                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu ON \r\n                kcu.constraint_catalog <=> rc.constraint_catalog AND\r\n                kcu.constraint_schema <=> rc.constraint_schema AND \r\n                kcu.constraint_name <=> rc.constraint_name AND\r\n                kcu.ORDINAL_POSITION=1 WHERE 1=1";
			StringBuilder stringBuilder = new StringBuilder();
			if (restrictions.Length >= 2 && !string.IsNullOrEmpty(restrictions[1]))
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " AND rc.constraint_schema LIKE '{0}'", new object[1] { restrictions[1] });
			}
			if (restrictions.Length >= 3 && !string.IsNullOrEmpty(restrictions[2]))
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " AND rc.table_name LIKE '{0}'", new object[1] { restrictions[2] });
			}
			if (restrictions.Length >= 4 && !string.IsNullOrEmpty(restrictions[3]))
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " AND rc.constraint_name LIKE '{0}'", new object[1] { restrictions[2] });
			}
			text += stringBuilder.ToString();
			return GetTable(text);
		}

		public override MySqlSchemaCollection GetForeignKeyColumns(string[] restrictions)
		{
			if (!connection.driver.Version.isAtLeast(5, 0, 6))
			{
				return base.GetForeignKeyColumns(restrictions);
			}
			string text = "SELECT kcu.* FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu\r\n                WHERE kcu.referenced_table_name IS NOT NULL";
			StringBuilder stringBuilder = new StringBuilder();
			if (restrictions.Length >= 2 && !string.IsNullOrEmpty(restrictions[1]))
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " AND kcu.constraint_schema LIKE '{0}'", new object[1] { restrictions[1] });
			}
			if (restrictions.Length >= 3 && !string.IsNullOrEmpty(restrictions[2]))
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " AND kcu.table_name LIKE '{0}'", new object[1] { restrictions[2] });
			}
			if (restrictions.Length >= 4 && !string.IsNullOrEmpty(restrictions[3]))
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " AND kcu.constraint_name LIKE '{0}'", new object[1] { restrictions[3] });
			}
			text += stringBuilder.ToString();
			return GetTable(text);
		}

		internal void GetParametersFromShowCreate(MySqlSchemaCollection parametersTable, string[] restrictions, MySqlSchemaCollection routines)
		{
			if (routines == null)
			{
				routines = GetSchema("procedures", restrictions);
			}
			MySqlCommand mySqlCommand = connection.CreateCommand();
			foreach (MySqlSchemaRow row in routines.Rows)
			{
				string commandText = string.Format("SHOW CREATE {0} `{1}`.`{2}`", row["ROUTINE_TYPE"], row["ROUTINE_SCHEMA"], row["ROUTINE_NAME"]);
				mySqlCommand.CommandText = commandText;
				try
				{
					string nameToRestrict = null;
					if (restrictions != null && restrictions.Length == 5 && restrictions[4] != null)
					{
						nameToRestrict = restrictions[4];
					}
					using MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
					mySqlDataReader.Read();
					string body = mySqlDataReader.GetString(2);
					mySqlDataReader.Close();
					ParseProcedureBody(parametersTable, body, row, nameToRestrict);
				}
				catch (SqlNullValueException innerException)
				{
					throw new InvalidOperationException(string.Format(Resources.UnableToRetrieveParameters, row["ROUTINE_NAME"]), innerException);
				}
			}
		}

		private void ParseProcedureBody(MySqlSchemaCollection parametersTable, string body, MySqlSchemaRow row, string nameToRestrict)
		{
			List<string> list = new List<string>(new string[3] { "IN", "OUT", "INOUT" });
			string text = row["SQL_MODE"].ToString();
			int num = 1;
			MySqlTokenizer mySqlTokenizer = new MySqlTokenizer(body);
			mySqlTokenizer.AnsiQuotes = text.IndexOf("ANSI_QUOTES") != -1;
			mySqlTokenizer.BackslashEscapes = text.IndexOf("NO_BACKSLASH_ESCAPES") == -1;
			mySqlTokenizer.ReturnComments = false;
			string text2 = mySqlTokenizer.NextToken();
			while (text2 != "(")
			{
				if (string.Compare(text2, "FUNCTION", StringComparison.OrdinalIgnoreCase) == 0 && nameToRestrict == null)
				{
					parametersTable.AddRow();
					InitParameterRow(row, parametersTable.Rows[0]);
				}
				text2 = mySqlTokenizer.NextToken();
			}
			text2 = mySqlTokenizer.NextToken();
			while (text2 != ")")
			{
				MySqlSchemaRow mySqlSchemaRow = parametersTable.NewRow();
				InitParameterRow(row, mySqlSchemaRow);
				mySqlSchemaRow["ORDINAL_POSITION"] = num++;
				string text3 = StringUtility.ToUpperInvariant(text2);
				if (!mySqlTokenizer.Quoted && list.Contains(text3))
				{
					mySqlSchemaRow["PARAMETER_MODE"] = text3;
					text2 = mySqlTokenizer.NextToken();
				}
				if (mySqlTokenizer.Quoted)
				{
					text2 = text2.Substring(1, text2.Length - 2);
				}
				mySqlSchemaRow["PARAMETER_NAME"] = text2;
				text2 = ParseDataType(mySqlSchemaRow, mySqlTokenizer);
				if (text2 == ",")
				{
					text2 = mySqlTokenizer.NextToken();
				}
				if (nameToRestrict == null || string.Compare(mySqlSchemaRow["PARAMETER_NAME"].ToString(), nameToRestrict, StringComparison.OrdinalIgnoreCase) == 0)
				{
					parametersTable.Rows.Add(mySqlSchemaRow);
				}
			}
			text2 = StringUtility.ToUpperInvariant(mySqlTokenizer.NextToken());
			if (string.Compare(text2, "RETURNS", StringComparison.OrdinalIgnoreCase) == 0)
			{
				MySqlSchemaRow mySqlSchemaRow2 = parametersTable.Rows[0];
				mySqlSchemaRow2["PARAMETER_NAME"] = "RETURN_VALUE";
				ParseDataType(mySqlSchemaRow2, mySqlTokenizer);
			}
		}

		private static void InitParameterRow(MySqlSchemaRow procedure, MySqlSchemaRow parameter)
		{
			parameter["SPECIFIC_CATALOG"] = null;
			parameter["SPECIFIC_SCHEMA"] = procedure["ROUTINE_SCHEMA"];
			parameter["SPECIFIC_NAME"] = procedure["ROUTINE_NAME"];
			parameter["PARAMETER_MODE"] = "IN";
			parameter["ORDINAL_POSITION"] = 0;
			parameter["ROUTINE_TYPE"] = procedure["ROUTINE_TYPE"];
		}

		private string ParseDataType(MySqlSchemaRow row, MySqlTokenizer tokenizer)
		{
			StringBuilder stringBuilder = new StringBuilder(StringUtility.ToUpperInvariant(tokenizer.NextToken()));
			row["DATA_TYPE"] = stringBuilder.ToString();
			string text = row["DATA_TYPE"].ToString();
			string text2 = tokenizer.NextToken();
			if (text2 == "(")
			{
				text2 = tokenizer.ReadParenthesis();
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}", new object[1] { text2 });
				if (text != "ENUM" && text != "SET")
				{
					ParseDataTypeSize(row, text2);
				}
				text2 = tokenizer.NextToken();
			}
			else
			{
				stringBuilder.Append(GetDataTypeDefaults(text, row));
			}
			while (text2 != ")" && text2 != "," && string.Compare(text2, "begin", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(text2, "return", StringComparison.OrdinalIgnoreCase) != 0)
			{
				if (string.Compare(text2, "CHARACTER", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(text2, "BINARY", StringComparison.OrdinalIgnoreCase) != 0)
				{
					if (string.Compare(text2, "SET", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(text2, "CHARSET", StringComparison.OrdinalIgnoreCase) == 0)
					{
						row["CHARACTER_SET_NAME"] = tokenizer.NextToken();
					}
					else if (string.Compare(text2, "ASCII", StringComparison.OrdinalIgnoreCase) == 0)
					{
						row["CHARACTER_SET_NAME"] = "latin1";
					}
					else if (string.Compare(text2, "UNICODE", StringComparison.OrdinalIgnoreCase) == 0)
					{
						row["CHARACTER_SET_NAME"] = "ucs2";
					}
					else if (string.Compare(text2, "COLLATE", StringComparison.OrdinalIgnoreCase) == 0)
					{
						row["COLLATION_NAME"] = tokenizer.NextToken();
					}
					else
					{
						stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " {0}", new object[1] { text2 });
					}
				}
				text2 = tokenizer.NextToken();
			}
			if (stringBuilder.Length > 0)
			{
				row["DTD_IDENTIFIER"] = stringBuilder.ToString();
			}
			if (string.IsNullOrEmpty((string)row["COLLATION_NAME"]) && !string.IsNullOrEmpty((string)row["CHARACTER_SET_NAME"]))
			{
				row["COLLATION_NAME"] = CharSetMap.GetDefaultCollation(row["CHARACTER_SET_NAME"].ToString(), connection);
			}
			if (row["CHARACTER_MAXIMUM_LENGTH"] != null)
			{
				if (row["CHARACTER_SET_NAME"] == null)
				{
					row["CHARACTER_SET_NAME"] = "";
				}
				row["CHARACTER_OCTET_LENGTH"] = CharSetMap.GetMaxLength((string)row["CHARACTER_SET_NAME"], connection) * (int)row["CHARACTER_MAXIMUM_LENGTH"];
			}
			return text2;
		}

		private static string GetDataTypeDefaults(string type, MySqlSchemaRow row)
		{
			string format = "({0},{1})";
			_ = row["NUMERIC_PRECISION"];
			if (MetaData.IsNumericType(type) && string.IsNullOrEmpty((string)row["NUMERIC_PRECISION"]))
			{
				row["NUMERIC_PRECISION"] = 10;
				row["NUMERIC_SCALE"] = 0;
				if (!MetaData.SupportScale(type))
				{
					format = "({0})";
				}
				return string.Format(format, row["NUMERIC_PRECISION"], row["NUMERIC_SCALE"]);
			}
			return string.Empty;
		}

		private static void ParseDataTypeSize(MySqlSchemaRow row, string size)
		{
			size = size.Trim('(', ')');
			string[] array = size.Split(new char[1] { ',' });
			if (!MetaData.IsNumericType(row["DATA_TYPE"].ToString()))
			{
				row["CHARACTER_MAXIMUM_LENGTH"] = int.Parse(array[0]);
				return;
			}
			row["NUMERIC_PRECISION"] = int.Parse(array[0]);
			if (array.Length == 2)
			{
				row["NUMERIC_SCALE"] = int.Parse(array[1]);
			}
		}
	}
}
namespace MySql.Data.MySqlClient.Memcached
{
	public abstract class Client
	{
		protected uint port;

		protected string server;

		protected Stream stream;

		public static Client GetInstance(string server, uint port, MemcachedFlags flags)
		{
			if ((flags | MemcachedFlags.TextProtocol) != 0)
			{
				return new TextClient(server, port);
			}
			if ((flags | MemcachedFlags.BinaryProtocol) != 0)
			{
				return new BinaryClient(server, port);
			}
			return null;
		}

		public virtual void Open()
		{
			stream = StreamCreator.GetStream(server, port, null, 10u, default(DBVersion), 60u);
		}

		public virtual void Close()
		{
			stream.Dispose();
		}

		protected Client(string server, uint port)
		{
			this.server = server;
			this.port = port;
		}

		public abstract void Add(string key, object data, TimeSpan expiration);

		public abstract void Append(string key, object data);

		public abstract void Cas(string key, object data, TimeSpan expiration, ulong casUnique);

		public abstract void Decrement(string key, int amount);

		public abstract void Delete(string key);

		public abstract void FlushAll(TimeSpan delay);

		public abstract KeyValuePair<string, object> Get(string key);

		public abstract void Increment(string key, int amount);

		public abstract void Prepend(string key, object data);

		public abstract void Replace(string key, object data, TimeSpan expiration);

		public abstract void Set(string key, object data, TimeSpan expiration);
	}
	public class BinaryClient : Client
	{
		private enum OpCodes : byte
		{
			Get = 0,
			Set = 1,
			Add = 2,
			Replace = 3,
			Delete = 4,
			Increment = 5,
			Decrement = 6,
			Quit = 7,
			Flush = 8,
			GetK = 12,
			GetKQ = 13,
			Append = 14,
			Prepend = 15,
			SASL_list_mechs = 32,
			SASL_Auth = 33,
			SASL_Step = 34
		}

		private enum MagicByte : byte
		{
			Request = 128,
			Response
		}

		private enum ResponseStatus : ushort
		{
			NoError = 0,
			KeyNotFound = 1,
			KeyExists = 2,
			ValueTooLarge = 3,
			InvalidArguments = 4,
			ItemNotStored = 5,
			IncrDecrOnNonNumericValue = 6,
			VbucketBelongsToAnotherServer = 7,
			AuthenticationError = 8,
			AuthenticationContinue = 9,
			UnknownCommand = 129,
			OutOfMemory = 130,
			NotSupported = 131,
			InternalError = 132,
			Busy = 133,
			TemporaryFailure = 134
		}

		private Encoding encoding;

		public BinaryClient(string server, uint port)
			: base(server, port)
		{
			encoding = Encoding.UTF8;
		}

		public override void Add(string key, object data, TimeSpan expiration)
		{
			SendCommand(128, 2, key, data, expiration, hasExtra: true);
		}

		public override void Append(string key, object data)
		{
			SendCommand(128, 14, key, data, TimeSpan.Zero, hasExtra: false);
		}

		public override void Cas(string key, object data, TimeSpan expiration, ulong casUnique)
		{
			throw new NotImplementedException("Not available in binary protocol");
		}

		public override void Decrement(string key, int amount)
		{
			SendCommand(128, 6, key, amount);
		}

		public override void Delete(string key)
		{
			SendCommand(128, 4, key);
		}

		public override void FlushAll(TimeSpan delay)
		{
			SendCommand(128, 8, delay);
		}

		public override KeyValuePair<string, object> Get(string key)
		{
			SendCommand(128, 0, key, out var value);
			return new KeyValuePair<string, object>(key, value);
		}

		public override void Increment(string key, int amount)
		{
			SendCommand(128, 5, key, amount);
		}

		public override void Prepend(string key, object data)
		{
			SendCommand(128, 15, key, data, TimeSpan.Zero, hasExtra: false);
		}

		public override void Replace(string key, object data, TimeSpan expiration)
		{
			SendCommand(128, 3, key, data, expiration, hasExtra: true);
		}

		public override void Set(string key, object data, TimeSpan expiration)
		{
			SendCommand(128, 1, key, data, expiration, hasExtra: true);
		}

		private void SendCommand(byte magic, byte opcode, string key, object data, TimeSpan expiration, bool hasExtra)
		{
			byte[] array = EncodeStoreCommand(magic, opcode, key, data, expiration, hasExtra);
			stream.Write(array, 0, array.Length);
			GetResponse();
		}

		private void SendCommand(byte magic, byte opcode, string key, out string value)
		{
			byte[] array = EncodeGetCommand(magic, opcode, key);
			stream.Write(array, 0, array.Length);
			byte[] response = GetResponse();
			byte[] array2 = new byte[response[4] - 4];
			Array.Copy(response, 28, array2, 0, response[4] - 4);
			value = encoding.GetString(array2, 0, array2.Length);
		}

		private void SendCommand(byte magic, byte opcode, string key)
		{
			byte[] array = EncodeGetCommand(magic, opcode, key);
			stream.Write(array, 0, array.Length);
			GetResponse();
		}

		private void SendCommand(byte magic, byte opcode, TimeSpan expiration)
		{
			byte[] array = EncodeFlushCommand(magic, opcode, expiration);
			stream.Write(array, 0, array.Length);
			GetResponse();
		}

		private void SendCommand(byte magic, byte opcode, string key, int amount)
		{
			byte[] array = EncodeIncrCommand(magic, opcode, key, amount);
			stream.Write(array, 0, array.Length);
			GetResponse();
		}

		private byte[] GetResponse()
		{
			byte[] array = new byte[24];
			stream.Read(array, 0, array.Length);
			ValidateResponse(array);
			return array;
		}

		private void ValidateResponse(byte[] res)
		{
			ushort num = (ushort)((res[6] << 8) | res[7]);
			if (num != 0)
			{
				throw new MemcachedException(((ResponseStatus)num).ToString());
			}
		}

		private byte[] EncodeStoreCommand(byte magic, byte opcode, string key, object data, TimeSpan expiration, bool hasExtra)
		{
			byte[] bytes = encoding.GetBytes(key);
			byte[] bytes2 = encoding.GetBytes(data.ToString());
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.WriteByte(magic);
			memoryStream.WriteByte(opcode);
			WriteToMemoryStream(BitConverter.GetBytes((ushort)bytes.Length), memoryStream);
			memoryStream.WriteByte(8);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			WriteToMemoryStream(BitConverter.GetBytes((uint)(bytes.Length + bytes2.Length + (hasExtra ? 8 : 0))), memoryStream);
			WriteToMemoryStream(BitConverter.GetBytes(0u), memoryStream);
			WriteToMemoryStream(BitConverter.GetBytes((ushort)0), memoryStream);
			if (hasExtra)
			{
				memoryStream.Write(new byte[4], 0, 4);
				WriteToMemoryStream(BitConverter.GetBytes((uint)expiration.TotalSeconds), memoryStream);
			}
			memoryStream.Write(bytes, 0, bytes.Length);
			memoryStream.Write(bytes2, 0, bytes2.Length);
			return memoryStream.ToArray();
		}

		private byte[] EncodeGetCommand(byte magic, byte opcode, string key)
		{
			byte[] bytes = encoding.GetBytes(key);
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.WriteByte(magic);
			memoryStream.WriteByte(opcode);
			WriteToMemoryStream(BitConverter.GetBytes((ushort)bytes.Length), memoryStream);
			memoryStream.WriteByte(8);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			WriteToMemoryStream(BitConverter.GetBytes((ushort)bytes.Length), memoryStream);
			WriteToMemoryStream(BitConverter.GetBytes(0u), memoryStream);
			WriteToMemoryStream(BitConverter.GetBytes(0u), memoryStream);
			memoryStream.Write(bytes, 0, bytes.Length);
			return memoryStream.ToArray();
		}

		private byte[] EncodeFlushCommand(byte magic, byte opcode, TimeSpan expiration)
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.WriteByte(magic);
			memoryStream.WriteByte(opcode);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(4);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			WriteToMemoryStream(BitConverter.GetBytes((ushort)4), memoryStream);
			WriteToMemoryStream(BitConverter.GetBytes(0u), memoryStream);
			WriteToMemoryStream(BitConverter.GetBytes(0u), memoryStream);
			WriteToMemoryStream(BitConverter.GetBytes((uint)expiration.TotalSeconds), memoryStream);
			return memoryStream.ToArray();
		}

		private byte[] EncodeIncrCommand(byte magic, byte opcode, string key, int amount)
		{
			byte[] bytes = encoding.GetBytes(key);
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.WriteByte(magic);
			memoryStream.WriteByte(opcode);
			WriteToMemoryStream(BitConverter.GetBytes((ushort)bytes.Length), memoryStream);
			memoryStream.WriteByte(20);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			WriteToMemoryStream(BitConverter.GetBytes((ushort)(bytes.Length + 20)), memoryStream);
			WriteToMemoryStream(BitConverter.GetBytes(0u), memoryStream);
			WriteToMemoryStream(BitConverter.GetBytes(0u), memoryStream);
			long num = amount;
			if (opcode == 6)
			{
				num *= -1;
			}
			WriteToMemoryStream(BitConverter.GetBytes(0L), memoryStream);
			TimeSpan zero = TimeSpan.Zero;
			WriteToMemoryStream(BitConverter.GetBytes((uint)zero.TotalSeconds), memoryStream);
			memoryStream.Write(bytes, 0, bytes.Length);
			return memoryStream.ToArray();
		}

		private void WriteToMemoryStream(byte[] data, MemoryStream ms)
		{
			Array.Reverse((Array)data);
			ms.Write(data, 0, data.Length);
		}
	}
	public class MemcachedException : Exception
	{
		public MemcachedException(string msg)
			: base(msg)
		{
		}

		public MemcachedException(string msg, Exception e)
			: base(msg, e)
		{
		}
	}
	public class TextClient : Client
	{
		private Encoding encoding;

		private static readonly string PROTOCOL_ADD = "add";

		private static readonly string PROTOCOL_APPEND = "append";

		private static readonly string PROTOCOL_CAS = "cas";

		private static readonly string PROTOCOL_DECREMENT = "decr";

		private static readonly string PROTOCOL_DELETE = "delete";

		private static readonly string PROTOCOL_FLUSHALL = "flush_all";

		private static readonly string PROTOCOL_GET = "get";

		private static readonly string PROTOCOL_GETS = "gets";

		private static readonly string PROTOCOL_INCREMENT = "incr";

		private static readonly string PROTOCOL_PREPEND = "prepend";

		private static readonly string PROTOCOL_REPLACE = "replace";

		private static readonly string PROTOCOL_SET = "set";

		private static readonly string VALUE = "VALUE";

		private static readonly string END = "END";

		private static readonly string ERR_ERROR = "ERROR";

		private static readonly string ERR_CLIENT_ERROR = "CLIENT_ERROR";

		private static readonly string ERR_SERVER_ERROR = "SERVER_ERROR";

		protected internal TextClient(string server, uint port)
			: base(server, port)
		{
			encoding = Encoding.UTF8;
		}

		public override void Add(string key, object data, TimeSpan expiration)
		{
			SendCommand(PROTOCOL_ADD, key, data, expiration);
		}

		public override void Append(string key, object data)
		{
			SendCommand(PROTOCOL_APPEND, key, data);
		}

		public override void Cas(string key, object data, TimeSpan expiration, ulong casUnique)
		{
			SendCommand(PROTOCOL_CAS, key, data, expiration, casUnique);
		}

		public override void Decrement(string key, int amount)
		{
			SendCommand(PROTOCOL_DECREMENT, key, amount);
		}

		public override void Delete(string key)
		{
			SendCommand(PROTOCOL_DELETE, key);
		}

		public override void FlushAll(TimeSpan delay)
		{
			SendCommand(PROTOCOL_FLUSHALL, delay);
		}

		public override KeyValuePair<string, object> Get(string key)
		{
			KeyValuePair<string, object>[] array = Gets(key);
			if (array.Length == 0)
			{
				throw new MemcachedException("Item does not exists.");
			}
			return array[0];
		}

		private KeyValuePair<string, object>[] Gets(params string[] keys)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($"{PROTOCOL_GETS}");
			for (int i = 0; i < keys.Length; i++)
			{
				stringBuilder.Append($" {keys[i]}");
			}
			stringBuilder.Append("\r\n");
			SendData(stringBuilder.ToString());
			byte[] response = GetResponse();
			return ParseGetResponse(response);
		}

		public override void Increment(string key, int amount)
		{
			SendCommand(PROTOCOL_INCREMENT, key, amount);
		}

		public override void Prepend(string key, object data)
		{
			SendCommand(PROTOCOL_PREPEND, key, data);
		}

		public override void Replace(string key, object data, TimeSpan expiration)
		{
			SendCommand(PROTOCOL_REPLACE, key, data, expiration);
		}

		public override void Set(string key, object data, TimeSpan expiration)
		{
			SendCommand(PROTOCOL_SET, key, data, expiration);
		}

		private void SendCommand(string cmd, string key, object data, TimeSpan expiration, ulong casUnique)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($"{cmd} {key} 0 {(int)expiration.TotalSeconds} ");
			byte[] bytes = encoding.GetBytes(data.ToString());
			string text = encoding.GetString(bytes, 0, bytes.Length);
			stringBuilder.Append(text.Length.ToString());
			stringBuilder.AppendFormat(" {0}", casUnique);
			stringBuilder.Append("\r\n");
			stringBuilder.Append(text);
			stringBuilder.Append("\r\n");
			SendData(stringBuilder.ToString());
			GetResponse();
		}

		private void SendCommand(string cmd, string key, object data, TimeSpan expiration)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($"{cmd} {key} 0 {(int)expiration.TotalSeconds} ");
			byte[] bytes = encoding.GetBytes(data.ToString());
			string text = encoding.GetString(bytes, 0, bytes.Length);
			stringBuilder.Append(text.Length.ToString());
			stringBuilder.Append("\r\n");
			stringBuilder.Append(text);
			stringBuilder.Append("\r\n");
			SendData(stringBuilder.ToString());
			GetResponse();
		}

		private void SendCommand(string cmd, string key, object data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($"{cmd} {key} ");
			byte[] bytes = encoding.GetBytes(data.ToString());
			string text = encoding.GetString(bytes, 0, bytes.Length);
			if (cmd == PROTOCOL_APPEND || cmd == PROTOCOL_PREPEND)
			{
				stringBuilder.Append("0 0 ");
			}
			stringBuilder.Append(text.Length.ToString());
			stringBuilder.Append("\r\n");
			stringBuilder.Append(text);
			stringBuilder.Append("\r\n");
			SendData(stringBuilder.ToString());
			GetResponse();
		}

		private void SendCommand(string cmd, string key)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($"{cmd} {key} ");
			stringBuilder.Append("\r\n");
			SendData(stringBuilder.ToString());
			GetResponse();
		}

		private void SendCommand(string cmd, string key, int amount)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($"{cmd} {key} {amount}");
			stringBuilder.Append("\r\n");
			SendData(stringBuilder.ToString());
			GetResponse();
		}

		private void SendCommand(string cmd, TimeSpan expiration)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($"{PROTOCOL_FLUSHALL} {expiration.TotalSeconds}\r\n");
			SendData(stringBuilder.ToString());
			GetResponse();
		}

		private void ValidateErrorResponse(byte[] res)
		{
			string text = encoding.GetString(res, 0, res.Length);
			if (text.StartsWith(ERR_ERROR, StringComparison.OrdinalIgnoreCase) || text.StartsWith(ERR_CLIENT_ERROR, StringComparison.OrdinalIgnoreCase) || text.StartsWith(ERR_SERVER_ERROR, StringComparison.OrdinalIgnoreCase))
			{
				throw new MemcachedException(text);
			}
		}

		private void SendData(string sData)
		{
			byte[] bytes = encoding.GetBytes(sData);
			stream.Write(bytes, 0, bytes.Length);
		}

		private KeyValuePair<string, object>[] ParseGetResponse(byte[] input)
		{
			string[] array = encoding.GetString(input, 0, input.Length).Split(new string[1] { "\r\n" }, StringSplitOptions.None);
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			int i = 0;
			string key = "";
			for (; array[i] != END && i < array.Length; i++)
			{
				if (array[i].StartsWith(VALUE, StringComparison.OrdinalIgnoreCase))
				{
					key = array[i].Split(new char[1] { ' ' })[1];
				}
				else
				{
					KeyValuePair<string, object> item = new KeyValuePair<string, object>(key, array[i]);
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		private byte[] GetResponse()
		{
			byte[] buffer = new byte[1024];
			MemoryStream memoryStream = new MemoryStream();
			for (int num = stream.Read(buffer, 0, 1024); num > 0; num = stream.Read(buffer, 0, 1024))
			{
				memoryStream.Write(buffer, 0, num);
				if (num < 1024)
				{
					break;
				}
			}
			byte[] array = memoryStream.ToArray();
			ValidateErrorResponse(array);
			return array;
		}
	}
	[Flags]
	public enum MemcachedFlags : ushort
	{
		TextProtocol = 1,
		BinaryProtocol = 2,
		Tcp = 4
	}
}
namespace MySql.Data.MySqlClient
{
	[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
	public sealed class MySqlClientFactory : DbProviderFactory, IServiceProvider
	{
		public static MySqlClientFactory Instance = new MySqlClientFactory();

		private Type dbServicesType;

		private FieldInfo mySqlDbProviderServicesInstance;

		public override bool CanCreateDataSourceEnumerator => false;

		private Type DbServicesType
		{
			get
			{
				if ((object)dbServicesType == null)
				{
					dbServicesType = Type.GetType("System.Data.Common.DbProviderServices, System.Data.Entity, \r\n                        Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", throwOnError: false);
				}
				return dbServicesType;
			}
		}

		private FieldInfo MySqlDbProviderServicesInstance
		{
			get
			{
				if ((object)mySqlDbProviderServicesInstance == null)
				{
					string fullName = Assembly.GetExecutingAssembly().FullName;
					string arg = fullName.Replace("MySql.Data", "MySql.Data.Entity");
					string arg2 = fullName.Replace("MySql.Data", "MySql.Data.Entity.EF5");
					fullName = $"MySql.Data.MySqlClient.MySqlProviderServices, {arg2}";
					Type type = Type.GetType(fullName, throwOnError: false);
					if ((object)type == null)
					{
						fullName = $"MySql.Data.MySqlClient.MySqlProviderServices, {arg}";
						type = Type.GetType(fullName, throwOnError: false);
						if ((object)type == null)
						{
							throw new DllNotFoundException(fullName);
						}
					}
					mySqlDbProviderServicesInstance = type.GetField("Instance", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
				}
				return mySqlDbProviderServicesInstance;
			}
		}

		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new MySqlCommandBuilder();
		}

		public override DbCommand CreateCommand()
		{
			return new MySqlCommand();
		}

		public override DbConnection CreateConnection()
		{
			return new MySqlConnection();
		}

		public override DbDataAdapter CreateDataAdapter()
		{
			return new MySqlDataAdapter();
		}

		public override DbParameter CreateParameter()
		{
			return new MySqlParameter();
		}

		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new MySqlConnectionStringBuilder();
		}

		object IServiceProvider.GetService(Type serviceType)
		{
			if ((object)serviceType != DbServicesType)
			{
				return null;
			}
			if ((object)MySqlDbProviderServicesInstance == null)
			{
				return null;
			}
			return MySqlDbProviderServicesInstance.GetValue(null);
		}
	}
	[Serializable]
	public sealed class MySqlClientPermission : DBDataPermission
	{
		public MySqlClientPermission(PermissionState permissionState)
			: base(permissionState)
		{
		}

		private MySqlClientPermission(MySqlClientPermission permission)
			: base(permission)
		{
		}

		internal MySqlClientPermission(MySqlClientPermissionAttribute permissionAttribute)
			: base(permissionAttribute)
		{
		}

		internal MySqlClientPermission(DBDataPermission permission)
			: base(permission)
		{
		}

		internal MySqlClientPermission(string connectionString)
			: base(PermissionState.None)
		{
			if (connectionString == null || connectionString.Length == 0)
			{
				base.Add(string.Empty, string.Empty, KeyRestrictionBehavior.AllowOnly);
			}
			else
			{
				base.Add(connectionString, string.Empty, KeyRestrictionBehavior.AllowOnly);
			}
		}

		public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			base.Add(connectionString, restrictions, behavior);
		}

		public override IPermission Copy()
		{
			return new MySqlClientPermission(this);
		}
	}
	public sealed class MySqlConfiguration : ConfigurationSection
	{
		private static MySqlConfiguration settings = ConfigurationManager.GetSection("MySQL") as MySqlConfiguration;

		public static MySqlConfiguration Settings => settings;

		[ConfigurationCollection(typeof(InterceptorConfigurationElement), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
		[ConfigurationProperty("ExceptionInterceptors", IsRequired = false)]
		public GenericConfigurationElementCollection<InterceptorConfigurationElement> ExceptionInterceptors => (GenericConfigurationElementCollection<InterceptorConfigurationElement>)base["ExceptionInterceptors"];

		[ConfigurationProperty("CommandInterceptors", IsRequired = false)]
		[ConfigurationCollection(typeof(InterceptorConfigurationElement), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
		public GenericConfigurationElementCollection<InterceptorConfigurationElement> CommandInterceptors => (GenericConfigurationElementCollection<InterceptorConfigurationElement>)base["CommandInterceptors"];

		[ConfigurationCollection(typeof(AuthenticationPluginConfigurationElement), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
		[ConfigurationProperty("AuthenticationPlugins", IsRequired = false)]
		public GenericConfigurationElementCollection<AuthenticationPluginConfigurationElement> AuthenticationPlugins => (GenericConfigurationElementCollection<AuthenticationPluginConfigurationElement>)base["AuthenticationPlugins"];

		[ConfigurationProperty("Replication", IsRequired = true)]
		public ReplicationConfigurationElement Replication
		{
			get
			{
				return (ReplicationConfigurationElement)base["Replication"];
			}
			set
			{
				base["Replication"] = value;
			}
		}
	}
	public sealed class AuthenticationPluginConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		[ConfigurationProperty("type", IsRequired = true)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}
	}
	public sealed class InterceptorConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		[ConfigurationProperty("type", IsRequired = true)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}
	}
	public sealed class GenericConfigurationElementCollection<T> : ConfigurationElementCollection, IEnumerable<T>, IEnumerable where T : ConfigurationElement, new()
	{
		private List<T> _elements = new List<T>();

		protected override ConfigurationElement CreateNewElement()
		{
			T val = new T();
			_elements.Add(val);
			return val;
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return _elements.Find((T e) => e.Equals(element));
		}

		public new IEnumerator<T> GetEnumerator()
		{
			return _elements.GetEnumerator();
		}
	}
	internal class MySqlConnectionStringOption
	{
		public delegate void SetterDelegate(MySqlConnectionStringBuilder msb, MySqlConnectionStringOption sender, object value);

		public delegate object GetterDelegate(MySqlConnectionStringBuilder msb, MySqlConnectionStringOption sender);

		public string[] Synonyms { get; private set; }

		public bool Obsolete { get; private set; }

		public Type BaseType { get; private set; }

		public string Keyword { get; private set; }

		public object DefaultValue { get; private set; }

		public SetterDelegate Setter { get; private set; }

		public GetterDelegate Getter { get; private set; }

		public MySqlConnectionStringOption(string keyword, string synonyms, Type baseType, object defaultValue, bool obsolete, SetterDelegate setter, GetterDelegate getter)
		{
			Keyword = StringUtility.ToLowerInvariant(keyword);
			if (synonyms != null)
			{
				Synonyms = StringUtility.ToLowerInvariant(synonyms).Split(new char[1] { ',' });
			}
			BaseType = baseType;
			Obsolete = obsolete;
			DefaultValue = defaultValue;
			Setter = setter;
			Getter = getter;
		}

		public MySqlConnectionStringOption(string keyword, string synonyms, Type baseType, object defaultValue, bool obsolete)
			: this(keyword, synonyms, baseType, defaultValue, obsolete, delegate(MySqlConnectionStringBuilder msb, MySqlConnectionStringOption sender, object value)
			{
				sender.ValidateValue(ref value);
				msb.SetValue(sender.Keyword, Convert.ChangeType(value, sender.BaseType));
			}, (MySqlConnectionStringBuilder msb, MySqlConnectionStringOption sender) => msb.values[sender.Keyword])
		{
		}

		public bool HasKeyword(string key)
		{
			if (Keyword == key)
			{
				return true;
			}
			if (Synonyms == null)
			{
				return false;
			}
			string[] synonyms = Synonyms;
			foreach (string text in synonyms)
			{
				if (text == key)
				{
					return true;
				}
			}
			return false;
		}

		public void Clean(MySqlConnectionStringBuilder builder)
		{
			builder.Remove(Keyword);
			if (Synonyms != null)
			{
				string[] synonyms = Synonyms;
				foreach (string keyword in synonyms)
				{
					builder.Remove(keyword);
				}
			}
		}

		public void ValidateValue(ref object value)
		{
			if (value == null)
			{
				return;
			}
			string name = BaseType.Name;
			Type type = value.GetType();
			bool result;
			if (type.Name == "String")
			{
				if ((object)BaseType == type)
				{
					return;
				}
				if ((object)BaseType == typeof(bool))
				{
					if (string.Compare("yes", (string)value, StringComparison.OrdinalIgnoreCase) == 0)
					{
						value = true;
						return;
					}
					if (string.Compare("no", (string)value, StringComparison.OrdinalIgnoreCase) == 0)
					{
						value = false;
						return;
					}
					if (bool.TryParse(value.ToString(), out result))
					{
						value = result;
						return;
					}
					throw new ArgumentException(string.Format(Resources.ValueNotCorrectType, value));
				}
			}
			if (name == "Boolean" && bool.TryParse(value.ToString(), out result))
			{
				value = result;
				return;
			}
			if (name.StartsWith("UInt64") && ulong.TryParse(value.ToString(), out var result2))
			{
				value = result2;
				return;
			}
			if (name.StartsWith("UInt32") && uint.TryParse(value.ToString(), out var result3))
			{
				value = result3;
				return;
			}
			if (name.StartsWith("Int64") && long.TryParse(value.ToString(), out var result4))
			{
				value = result4;
				return;
			}
			if (name.StartsWith("Int32") && int.TryParse(value.ToString(), out var result5))
			{
				value = result5;
				return;
			}
			Type baseType = BaseType.BaseType;
			if ((object)baseType != null && baseType.Name == "Enum" && ParseEnum(value.ToString(), out var value2))
			{
				value = value2;
				return;
			}
			throw new ArgumentException(string.Format(Resources.ValueNotCorrectType, value));
		}

		private bool ParseEnum(string requestedValue, out object value)
		{
			value = null;
			try
			{
				value = Enum.Parse(BaseType, requestedValue, ignoreCase: true);
				return true;
			}
			catch (ArgumentException)
			{
				return false;
			}
		}
	}
	internal class MySqlConnectionStringOptionCollection : Dictionary<string, MySqlConnectionStringOption>
	{
		private List<MySqlConnectionStringOption> options;

		internal List<MySqlConnectionStringOption> Options => options;

		internal MySqlConnectionStringOptionCollection()
			: base((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase)
		{
			options = new List<MySqlConnectionStringOption>();
		}

		internal void Add(MySqlConnectionStringOption option)
		{
			options.Add(option);
			Add(option.Keyword, option);
			if (option.Synonyms != null)
			{
				for (int i = 0; i < option.Synonyms.Length; i++)
				{
					Add(option.Synonyms[i], option);
				}
			}
		}

		internal MySqlConnectionStringOption Get(string keyword)
		{
			MySqlConnectionStringOption value = null;
			TryGetValue(keyword, out value);
			return value;
		}
	}
	[Flags]
	internal enum ClientFlags : ulong
	{
		LONG_PASSWORD = 1uL,
		FOUND_ROWS = 2uL,
		LONG_FLAG = 4uL,
		CONNECT_WITH_DB = 8uL,
		NO_SCHEMA = 0x10uL,
		COMPRESS = 0x20uL,
		ODBC = 0x40uL,
		LOCAL_FILES = 0x80uL,
		IGNORE_SPACE = 0x100uL,
		PROTOCOL_41 = 0x200uL,
		INTERACTIVE = 0x400uL,
		SSL = 0x800uL,
		IGNORE_SIGPIPE = 0x1000uL,
		TRANSACTIONS = 0x2000uL,
		RESERVED = 0x4000uL,
		SECURE_CONNECTION = 0x8000uL,
		MULTI_STATEMENTS = 0x10000uL,
		MULTI_RESULTS = 0x20000uL,
		PS_MULTI_RESULTS = 0x40000uL,
		PLUGIN_AUTH = 0x80000uL,
		CONNECT_ATTRS = 0x100000uL,
		CAN_HANDLE_EXPIRED_PASSWORD = 0x400000uL,
		CLIENT_SSL_VERIFY_SERVER_CERT = 0x40000000uL,
		CLIENT_REMEMBER_OPTIONS = 0x80000000uL
	}
	[Flags]
	internal enum ServerStatusFlags
	{
		InTransaction = 1,
		AutoCommitMode = 2,
		MoreResults = 4,
		AnotherQuery = 8,
		BadIndex = 0x10,
		NoIndex = 0x20,
		CursorExists = 0x40,
		LastRowSent = 0x80,
		OutputParameters = 0x1000
	}
	internal enum DBCmd : byte
	{
		SLEEP,
		QUIT,
		INIT_DB,
		QUERY,
		FIELD_LIST,
		CREATE_DB,
		DROP_DB,
		RELOAD,
		SHUTDOWN,
		STATISTICS,
		PROCESS_INFO,
		CONNECT,
		PROCESS_KILL,
		DEBUG,
		PING,
		TIME,
		DELAYED_INSERT,
		CHANGE_USER,
		BINLOG_DUMP,
		TABLE_DUMP,
		CONNECT_OUT,
		REGISTER_SLAVE,
		PREPARE,
		EXECUTE,
		LONG_DATA,
		CLOSE_STMT,
		RESET_STMT,
		SET_OPTION,
		FETCH
	}
	public enum MySqlDbType
	{
		Decimal = 0,
		Byte = 1,
		Int16 = 2,
		Int24 = 9,
		Int32 = 3,
		Int64 = 8,
		Float = 4,
		Double = 5,
		Timestamp = 7,
		Date = 10,
		Time = 11,
		DateTime = 12,
		[Obsolete("The Datetime enum value is obsolete.  Please use DateTime.")]
		Datetime = 12,
		Year = 13,
		Newdate = 14,
		VarString = 15,
		Bit = 16,
		NewDecimal = 246,
		Enum = 247,
		Set = 248,
		TinyBlob = 249,
		MediumBlob = 250,
		LongBlob = 251,
		Blob = 252,
		VarChar = 253,
		String = 254,
		Geometry = 255,
		UByte = 501,
		UInt16 = 502,
		UInt24 = 509,
		UInt32 = 503,
		UInt64 = 508,
		Binary = 600,
		VarBinary = 601,
		TinyText = 749,
		MediumText = 750,
		LongText = 751,
		Text = 752,
		Guid = 800
	}
	internal enum Field_Type : byte
	{
		DECIMAL = 0,
		BYTE = 1,
		SHORT = 2,
		LONG = 3,
		FLOAT = 4,
		DOUBLE = 5,
		NULL = 6,
		TIMESTAMP = 7,
		LONGLONG = 8,
		INT24 = 9,
		DATE = 10,
		TIME = 11,
		DATETIME = 12,
		YEAR = 13,
		NEWDATE = 14,
		ENUM = 247,
		SET = 248,
		TINY_BLOB = 249,
		MEDIUM_BLOB = 250,
		LONG_BLOB = 251,
		BLOB = 252,
		VAR_STRING = 253,
		STRING = 254
	}
	public enum MySqlConnectionProtocol
	{
		Sockets = 1,
		Socket = 1,
		Tcp = 1,
		Pipe = 2,
		NamedPipe = 2,
		UnixSocket = 3,
		Unix = 3,
		SharedMemory = 4,
		Memory = 4
	}
	public enum MySqlSslMode
	{
		None = 0,
		Preferred = 1,
		Prefered = 1,
		Required = 2,
		VerifyCA = 3,
		VerifyFull = 4
	}
	public enum MySqlDriverType
	{
		Native,
		Client,
		Embedded
	}
	public enum MySqlCertificateStoreLocation
	{
		None,
		CurrentUser,
		LocalMachine
	}
	internal class MySqlConnectAttrs
	{
		[DisplayName("_client_name")]
		public string ClientName => "MySql Connector/NET";

		[DisplayName("_pid")]
		public string PID
		{
			get
			{
				string result = string.Empty;
				try
				{
					result = Process.GetCurrentProcess().Id.ToString();
				}
				catch (Exception)
				{
				}
				return result;
			}
		}

		[DisplayName("_client_version")]
		public string ClientVersion
		{
			get
			{
				string result = string.Empty;
				try
				{
					result = Assembly.GetAssembly(typeof(MySqlConnectAttrs)).GetName().Version.ToString();
				}
				catch (Exception)
				{
				}
				return result;
			}
		}

		[DisplayName("_platform")]
		public string Platform
		{
			get
			{
				if (!Is64BitOS())
				{
					return "x86_32";
				}
				return "x86_64";
			}
		}

		[DisplayName("program_name")]
		public string ProgramName
		{
			get
			{
				string commandLine = Environment.CommandLine;
				try
				{
					string path = Environment.CommandLine.Substring(0, Environment.CommandLine.IndexOf("\" ")).Trim(new char[1] { '"' });
					commandLine = Path.GetFileName(path);
					if ((object)Assembly.GetEntryAssembly() != null)
					{
						commandLine = Assembly.GetEntryAssembly().ManifestModule.Name;
					}
				}
				catch (Exception)
				{
					commandLine = string.Empty;
				}
				return commandLine;
			}
		}

		[DisplayName("_os")]
		public string OS
		{
			get
			{
				string text = string.Empty;
				try
				{
					text = Environment.OSVersion.Platform.ToString();
					if (text == "Win32NT")
					{
						text = "Win";
						text += (Is64BitOS() ? "64" : "32");
					}
				}
				catch (Exception)
				{
				}
				return text;
			}
		}

		[DisplayName("_os_details")]
		public string OSDetails => string.Empty;

		[DisplayName("_thread")]
		public string Thread
		{
			get
			{
				string result = string.Empty;
				try
				{
					result = Process.GetCurrentProcess().Threads[0].Id.ToString();
				}
				catch (Exception)
				{
				}
				return result;
			}
		}

		private bool Is64BitOS()
		{
			return Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") == "AMD64";
		}
	}
	public class MySqlError
	{
		private string level;

		private int code;

		private string message;

		public string Level => level;

		public int Code => code;

		public string Message => message;

		public MySqlError(string level, int code, string message)
		{
			this.level = level;
			this.code = code;
			this.message = message;
		}
	}
	public enum MySqlErrorCode
	{
		None = 0,
		HashCheck = 1000,
		ISAMCheck = 1001,
		No = 1002,
		Yes = 1003,
		CannotCreateFile = 1004,
		CannotCreateTable = 1005,
		CannotCreateDatabase = 1006,
		DatabaseCreateExists = 1007,
		DatabaseDropExists = 1008,
		DatabaseDropDelete = 1009,
		DatabaseDropRemoveDir = 1010,
		CannotDeleteFile = 1011,
		CannotFindSystemRecord = 1012,
		CannotGetStatus = 1013,
		CannotGetWorkingDirectory = 1014,
		CannotLock = 1015,
		CannotOpenFile = 1016,
		FileNotFound = 1017,
		CannotReadDirectory = 1018,
		CannotSetWorkingDirectory = 1019,
		CheckRead = 1020,
		DiskFull = 1021,
		DuplicateKey = 1022,
		ErrorOnClose = 1023,
		ErrorOnRead = 1024,
		ErrorOnRename = 1025,
		ErrorOnWrite = 1026,
		FileUsed = 1027,
		FileSortAborted = 1028,
		FormNotFound = 1029,
		GetErrorNumber = 1030,
		IllegalHA = 1031,
		KeyNotFound = 1032,
		NotFormFile = 1033,
		NotKeyFile = 1034,
		OldKeyFile = 1035,
		OpenAsReadOnly = 1036,
		OutOfMemory = 1037,
		OutOfSortMemory = 1038,
		UnexepectedEOF = 1039,
		ConnectionCountError = 1040,
		OutOfResources = 1041,
		UnableToConnectToHost = 1042,
		HandshakeError = 1043,
		DatabaseAccessDenied = 1044,
		AccessDenied = 1045,
		NoDatabaseSelected = 1046,
		UnknownCommand = 1047,
		ColumnCannotBeNull = 1048,
		UnknownDatabase = 1049,
		TableExists = 1050,
		BadTable = 1051,
		NonUnique = 1052,
		ServerShutdown = 1053,
		BadFieldError = 1054,
		WrongFieldWithGroup = 1055,
		WrongGroupField = 1056,
		WrongSumSelected = 1057,
		WrongValueCount = 1058,
		TooLongIdentifier = 1059,
		DuplicateFieldName = 1060,
		DuplicateKeyName = 1061,
		DuplicateKeyEntry = 1062,
		WrongFieldSpecifier = 1063,
		ParseError = 1064,
		EmptyQuery = 1065,
		NonUniqueTable = 1066,
		InvalidDefault = 1067,
		MultiplePrimaryKey = 1068,
		TooManyKeys = 1069,
		TooManyKeysParts = 1070,
		TooLongKey = 1071,
		KeyColumnDoesNotExist = 1072,
		BlobUsedAsKey = 1073,
		TooBigFieldLength = 1074,
		WrongAutoKey = 1075,
		Ready = 1076,
		NormalShutdown = 1077,
		GotSignal = 1078,
		ShutdownComplete = 1079,
		ForcingClose = 1080,
		IPSocketError = 1081,
		NoSuchIndex = 1082,
		WrongFieldTerminators = 1083,
		BlobsAndNoTerminated = 1084,
		TextFileNotReadable = 1085,
		FileExists = 1086,
		LoadInfo = 1087,
		AlterInfo = 1088,
		WrongSubKey = 1089,
		CannotRemoveAllFields = 1090,
		CannotDropFieldOrKey = 1091,
		InsertInfo = 1092,
		UpdateTableUsed = 1093,
		NoSuchThread = 1094,
		KillDenied = 1095,
		NoTablesUsed = 1096,
		TooBigSet = 1097,
		NoUniqueLogFile = 1098,
		TableNotLockedForWrite = 1099,
		TableNotLocked = 1100,
		BlobCannotHaveDefault = 1101,
		WrongDatabaseName = 1102,
		WrongTableName = 1103,
		TooBigSelect = 1104,
		UnknownError = 1105,
		UnknownProcedure = 1106,
		WrongParameterCountToProcedure = 1107,
		WrongParametersToProcedure = 1108,
		UnknownTable = 1109,
		FieldSpecifiedTwice = 1110,
		InvalidGroupFunctionUse = 1111,
		UnsupportedExtenstion = 1112,
		TableMustHaveColumns = 1113,
		RecordFileFull = 1114,
		UnknownCharacterSet = 1115,
		TooManyTables = 1116,
		TooManyFields = 1117,
		TooBigRowSize = 1118,
		StackOverrun = 1119,
		WrongOuterJoin = 1120,
		NullColumnInIndex = 1121,
		CannotFindUDF = 1122,
		CannotInitializeUDF = 1123,
		UDFNoPaths = 1124,
		UDFExists = 1125,
		CannotOpenLibrary = 1126,
		CannotFindDLEntry = 1127,
		FunctionNotDefined = 1128,
		HostIsBlocked = 1129,
		HostNotPrivileged = 1130,
		AnonymousUser = 1131,
		PasswordNotAllowed = 1132,
		PasswordNoMatch = 1133,
		UpdateInfo = 1134,
		CannotCreateThread = 1135,
		WrongValueCountOnRow = 1136,
		CannotReopenTable = 1137,
		InvalidUseOfNull = 1138,
		RegExpError = 1139,
		MixOfGroupFunctionAndFields = 1140,
		NonExistingGrant = 1141,
		TableAccessDenied = 1142,
		ColumnAccessDenied = 1143,
		IllegalGrantForTable = 1144,
		GrantWrongHostOrUser = 1145,
		NoSuchTable = 1146,
		NonExistingTableGrant = 1147,
		NotAllowedCommand = 1148,
		SyntaxError = 1149,
		DelayedCannotChangeLock = 1150,
		TooManyDelayedThreads = 1151,
		AbortingConnection = 1152,
		PacketTooLarge = 1153,
		NetReadErrorFromPipe = 1154,
		NetFCntlError = 1155,
		NetPacketsOutOfOrder = 1156,
		NetUncompressError = 1157,
		NetReadError = 1158,
		NetReadInterrupted = 1159,
		NetErrorOnWrite = 1160,
		NetWriteInterrupted = 1161,
		TooLongString = 1162,
		TableCannotHandleBlob = 1163,
		TableCannotHandleAutoIncrement = 1164,
		DelayedInsertTableLocked = 1165,
		WrongColumnName = 1166,
		WrongKeyColumn = 1167,
		WrongMergeTable = 1168,
		DuplicateUnique = 1169,
		BlobKeyWithoutLength = 1170,
		PrimaryCannotHaveNull = 1171,
		TooManyRows = 1172,
		RequiresPrimaryKey = 1173,
		NoRAIDCompiled = 1174,
		UpdateWithoutKeysInSafeMode = 1175,
		KeyDoesNotExist = 1176,
		CheckNoSuchTable = 1177,
		CheckNotImplemented = 1178,
		CannotDoThisDuringATransaction = 1179,
		ErrorDuringCommit = 1180,
		ErrorDuringRollback = 1181,
		ErrorDuringFlushLogs = 1182,
		ErrorDuringCheckpoint = 1183,
		NewAbortingConnection = 1184,
		DumpNotImplemented = 1185,
		FlushMasterBinLogClosed = 1186,
		IndexRebuild = 1187,
		MasterError = 1188,
		MasterNetRead = 1189,
		MasterNetWrite = 1190,
		FullTextMatchingKeyNotFound = 1191,
		LockOrActiveTransaction = 1192,
		UnknownSystemVariable = 1193,
		CrashedOnUsage = 1194,
		CrashedOnRepair = 1195,
		WarningNotCompleteRollback = 1196,
		TransactionCacheFull = 1197,
		SlaveMustStop = 1198,
		SlaveNotRunning = 1199,
		BadSlave = 1200,
		MasterInfo = 1201,
		SlaveThread = 1202,
		TooManyUserConnections = 1203,
		SetConstantsOnly = 1204,
		LockWaitTimeout = 1205,
		LockTableFull = 1206,
		ReadOnlyTransaction = 1207,
		DropDatabaseWithReadLock = 1208,
		CreateDatabaseWithReadLock = 1209,
		WrongArguments = 1210,
		NoPermissionToCreateUser = 1211,
		UnionTablesInDifferentDirectory = 1212,
		LockDeadlock = 1213,
		TableCannotHandleFullText = 1214,
		CannotAddForeignConstraint = 1215,
		NoReferencedRow = 1216,
		RowIsReferenced = 1217,
		ConnectToMaster = 1218,
		QueryOnMaster = 1219,
		ErrorWhenExecutingCommand = 1220,
		WrongUsage = 1221,
		WrongNumberOfColumnsInSelect = 1222,
		CannotUpdateWithReadLock = 1223,
		MixingNotAllowed = 1224,
		DuplicateArgument = 1225,
		UserLimitReached = 1226,
		SpecifiedAccessDeniedError = 1227,
		LocalVariableError = 1228,
		GlobalVariableError = 1229,
		NotDefaultError = 1230,
		WrongValueForVariable = 1231,
		WrongTypeForVariable = 1232,
		VariableCannotBeRead = 1233,
		CannotUseOptionHere = 1234,
		NotSupportedYet = 1235,
		MasterFatalErrorReadingBinLog = 1236,
		SlaveIgnoredTable = 1237,
		IncorrectGlobalLocalVariable = 1238,
		WrongForeignKeyDefinition = 1239,
		KeyReferenceDoesNotMatchTableReference = 1240,
		OpearnColumnsError = 1241,
		SubQueryNoOneRow = 1242,
		UnknownStatementHandler = 1243,
		CorruptHelpDatabase = 1244,
		CyclicReference = 1245,
		AutoConvert = 1246,
		IllegalReference = 1247,
		DerivedMustHaveAlias = 1248,
		SelectReduced = 1249,
		TableNameNotAllowedHere = 1250,
		NotSupportedAuthMode = 1251,
		SpatialCannotHaveNull = 1252,
		CollationCharsetMismatch = 1253,
		SlaveWasRunning = 1254,
		SlaveWasNotRunning = 1255,
		TooBigForUncompress = 1256,
		ZipLibMemoryError = 1257,
		ZipLibBufferError = 1258,
		ZipLibDataError = 1259,
		CutValueGroupConcat = 1260,
		WarningTooFewRecords = 1261,
		WarningTooManyRecords = 1262,
		WarningNullToNotNull = 1263,
		WarningDataOutOfRange = 1264,
		WaningDataTruncated = 1265,
		WaningUsingOtherHandler = 1266,
		CannotAggregateTwoCollations = 1267,
		DropUserError = 1268,
		RevokeGrantsError = 1269,
		CannotAggregateThreeCollations = 1270,
		CannotAggregateNCollations = 1271,
		VariableIsNotStructure = 1272,
		UnknownCollation = 1273,
		SlaveIgnoreSSLParameters = 1274,
		ServerIsInSecureAuthMode = 1275,
		WaningFieldResolved = 1276,
		BadSlaveUntilCondition = 1277,
		MissingSkipSlave = 1278,
		ErrorUntilConditionIgnored = 1279,
		WrongNameForIndex = 1280,
		WrongNameForCatalog = 1281,
		WarningQueryCacheResize = 1282,
		BadFullTextColumn = 1283,
		UnknownKeyCache = 1284,
		WarningHostnameWillNotWork = 1285,
		UnknownStorageEngine = 1286,
		WaningDeprecatedSyntax = 1287,
		NonUpdateableTable = 1288,
		FeatureDisabled = 1289,
		OptionPreventsStatement = 1290,
		DuplicatedValueInType = 1291,
		TruncatedWrongValue = 1292,
		TooMuchAutoTimestampColumns = 1293,
		InvalidOnUpdate = 1294,
		UnsupportedPreparedStatement = 1295,
		GetErroMessage = 1296,
		GetTemporaryErrorMessage = 1297,
		UnknownTimeZone = 1298,
		WarningInvalidTimestamp = 1299,
		InvalidCharacterString = 1300,
		WarningAllowedPacketOverflowed = 1301,
		ConflictingDeclarations = 1302,
		StoredProcedureNoRecursiveCreate = 1303,
		StoredProcedureAlreadyExists = 1304,
		StoredProcedureDoesNotExist = 1305,
		StoredProcedureDropFailed = 1306,
		StoredProcedureStoreFailed = 1307,
		StoredProcedureLiLabelMismatch = 1308,
		StoredProcedureLabelRedefine = 1309,
		StoredProcedureLabelMismatch = 1310,
		StoredProcedureUninitializedVariable = 1311,
		StoredProcedureBadSelect = 1312,
		StoredProcedureBadReturn = 1313,
		StoredProcedureBadStatement = 1314,
		UpdateLogDeprecatedIgnored = 1315,
		UpdateLogDeprecatedTranslated = 1316,
		QueryInterrupted = 1317,
		StoredProcedureNumberOfArguments = 1318,
		StoredProcedureConditionMismatch = 1319,
		StoredProcedureNoReturn = 1320,
		StoredProcedureNoReturnEnd = 1321,
		StoredProcedureBadCursorQuery = 1322,
		StoredProcedureBadCursorSelect = 1323,
		StoredProcedureCursorMismatch = 1324,
		StoredProcedureAlreadyOpen = 1325,
		StoredProcedureCursorNotOpen = 1326,
		StoredProcedureUndeclaredVariabel = 1327,
		StoredProcedureWrongNumberOfFetchArguments = 1328,
		StoredProcedureFetchNoData = 1329,
		StoredProcedureDuplicateParameter = 1330,
		StoredProcedureDuplicateVariable = 1331,
		StoredProcedureDuplicateCondition = 1332,
		StoredProcedureDuplicateCursor = 1333,
		StoredProcedureCannotAlter = 1334,
		StoredProcedureSubSelectNYI = 1335,
		StatementNotAllowedInStoredFunctionOrTrigger = 1336,
		StoredProcedureVariableConditionAfterCursorHandler = 1337,
		StoredProcedureCursorAfterHandler = 1338,
		StoredProcedureCaseNotFound = 1339,
		FileParserTooBigFile = 1340,
		FileParserBadHeader = 1341,
		FileParserEOFInComment = 1342,
		FileParserErrorInParameter = 1343,
		FileParserEOFInUnknownParameter = 1344,
		ViewNoExplain = 1345,
		FrmUnknownType = 1346,
		WrongObject = 1347,
		NonUpdateableColumn = 1348,
		ViewSelectDerived = 1349,
		ViewSelectClause = 1350,
		ViewSelectVariable = 1351,
		ViewSelectTempTable = 1352,
		ViewWrongList = 1353,
		WarningViewMerge = 1354,
		WarningViewWithoutKey = 1355,
		ViewInvalid = 1356,
		StoredProcedureNoDropStoredProcedure = 1357,
		StoredProcedureGotoInHandler = 1358,
		TriggerAlreadyExists = 1359,
		TriggerDoesNotExist = 1360,
		TriggerOnViewOrTempTable = 1361,
		TriggerCannotChangeRow = 1362,
		TriggerNoSuchRowInTrigger = 1363,
		NoDefaultForField = 1364,
		DivisionByZero = 1365,
		TruncatedWrongValueForField = 1366,
		IllegalValueForType = 1367,
		ViewNonUpdatableCheck = 1368,
		ViewCheckFailed = 1369,
		PrecedureAccessDenied = 1370,
		RelayLogFail = 1371,
		PasswordLength = 1372,
		UnknownTargetBinLog = 1373,
		IOErrorLogIndexRead = 1374,
		BinLogPurgeProhibited = 1375,
		FSeekFail = 1376,
		BinLogPurgeFatalError = 1377,
		LogInUse = 1378,
		LogPurgeUnknownError = 1379,
		RelayLogInit = 1380,
		NoBinaryLogging = 1381,
		ReservedSyntax = 1382,
		WSAStartupFailed = 1383,
		DifferentGroupsProcedure = 1384,
		NoGroupForProcedure = 1385,
		OrderWithProcedure = 1386,
		LoggingProhibitsChangingOf = 1387,
		NoFileMapping = 1388,
		WrongMagic = 1389,
		PreparedStatementManyParameters = 1390,
		KeyPartZero = 1391,
		ViewChecksum = 1392,
		ViewMultiUpdate = 1393,
		ViewNoInsertFieldList = 1394,
		ViewDeleteMergeView = 1395,
		CannotUser = 1396,
		XAERNotA = 1397,
		XAERInvalid = 1398,
		XAERRemoveFail = 1399,
		XAEROutside = 1400,
		XAERRemoveError = 1401,
		XARBRollback = 1402,
		NonExistingProcedureGrant = 1403,
		ProcedureAutoGrantFail = 1404,
		ProcedureAutoRevokeFail = 1405,
		DataTooLong = 1406,
		StoredProcedureSQLState = 1407,
		StartupError = 1408,
		LoadFromFixedSizeRowsToVariable = 1409,
		CannotCreateUserWithGrant = 1410,
		WrongValueForType = 1411,
		TableDefinitionChanged = 1412,
		StoredProcedureDuplicateHandler = 1413,
		StoredProcedureNotVariableArgument = 1414,
		StoredProcedureNoReturnSet = 1415,
		CannotCreateGeometryObject = 1416,
		FailedRoutineBreaksBinLog = 1417,
		BinLogUnsafeRoutine = 1418,
		BinLogCreateRoutineNeedSuper = 1419,
		ExecuteStatementWithOpenCursor = 1420,
		StatementHasNoOpenCursor = 1421,
		CommitNotAllowedIfStoredFunctionOrTrigger = 1422,
		NoDefaultForViewField = 1423,
		StoredProcedureNoRecursion = 1424,
		TooBigScale = 1425,
		TooBigPrecision = 1426,
		MBiggerThanD = 1427,
		WrongLockOfSystemTable = 1428,
		ConnectToForeignDataSource = 1429,
		QueryOnForeignDataSource = 1430,
		ForeignDataSourceDoesNotExist = 1431,
		ForeignDataStringInvalidCannotCreate = 1432,
		ForeignDataStringInvalid = 1433,
		CannotCreateFederatedTable = 1434,
		TriggerInWrongSchema = 1435,
		StackOverrunNeedMore = 1436,
		TooLongBody = 1437,
		WarningCannotDropDefaultKeyCache = 1438,
		TooBigDisplayWidth = 1439,
		XAERDuplicateID = 1440,
		DateTimeFunctionOverflow = 1441,
		CannotUpdateUsedTableInStoredFunctionOrTrigger = 1442,
		ViewPreventUpdate = 1443,
		PreparedStatementNoRecursion = 1444,
		StoredProcedureCannotSetAutoCommit = 1445,
		MalformedDefiner = 1446,
		ViewFrmNoUser = 1447,
		ViewOtherUser = 1448,
		NoSuchUser = 1449,
		ForbidSchemaChange = 1450,
		RowIsReferenced2 = 1451,
		NoReferencedRow2 = 1452,
		StoredProcedureBadVariableShadow = 1453,
		TriggerNoDefiner = 1454,
		OldFileFormat = 1455,
		StoredProcedureRecursionLimit = 1456,
		StoredProcedureTableCorrupt = 1457,
		StoredProcedureWrongName = 1458,
		TableNeedsUpgrade = 1459,
		StoredProcedureNoAggregate = 1460,
		MaxPreparedStatementCountReached = 1461,
		ViewRecursive = 1462,
		NonGroupingFieldUsed = 1463,
		TableCannotHandleSpatialKeys = 1464,
		NoTriggersOnSystemSchema = 1465,
		RemovedSpaces = 1466,
		AutoIncrementReadFailed = 1467,
		UserNameError = 1468,
		HostNameError = 1469,
		WrongStringLength = 1470,
		NonInsertableTable = 1471,
		AdminWrongMergeTable = 1472,
		TooHighLevelOfNestingForSelect = 1473,
		NameBecomesEmpty = 1474,
		AmbiguousFieldTerm = 1475,
		ForeignServerExists = 1476,
		ForeignServerDoesNotExist = 1477,
		IllegalHACreateOption = 1478,
		PartitionRequiresValues = 1479,
		PartitionWrongValues = 1480,
		PartitionMaxValue = 1481,
		PartitionSubPartition = 1482,
		PartitionSubPartMix = 1483,
		PartitionWrongNoPart = 1484,
		PartitionWrongNoSubPart = 1485,
		WrongExpressionInParitionFunction = 1486,
		NoConstantExpressionInRangeOrListError = 1487,
		FieldNotFoundPartitionErrror = 1488,
		ListOfFieldsOnlyInHash = 1489,
		InconsistentPartitionInfo = 1490,
		PartitionFunctionNotAllowed = 1491,
		PartitionsMustBeDefined = 1492,
		RangeNotIncreasing = 1493,
		InconsistentTypeOfFunctions = 1494,
		MultipleDefinitionsConstantInListPartition = 1495,
		PartitionEntryError = 1496,
		MixHandlerError = 1497,
		PartitionNotDefined = 1498,
		TooManyPartitions = 1499,
		SubPartitionError = 1500,
		CannotCreateHandlerFile = 1501,
		BlobFieldInPartitionFunction = 1502,
		UniqueKeyNeedAllFieldsInPartitioningFunction = 1503,
		NoPartitions = 1504,
		PartitionManagementOnNoPartitioned = 1505,
		ForeignKeyOnPartitioned = 1506,
		DropPartitionNonExistent = 1507,
		DropLastPartition = 1508,
		CoalesceOnlyOnHashPartition = 1509,
		ReorganizeHashOnlyOnSameNumber = 1510,
		ReorganizeNoParameter = 1511,
		OnlyOnRangeListPartition = 1512,
		AddPartitionSubPartition = 1513,
		AddPartitionNoNewPartition = 1514,
		CoalescePartitionNoPartition = 1515,
		ReorganizePartitionNotExist = 1516,
		SameNamePartition = 1517,
		NoBinLog = 1518,
		ConsecutiveReorganizePartitions = 1519,
		ReorganizeOutsideRange = 1520,
		PartitionFunctionFailure = 1521,
		PartitionStateError = 1522,
		LimitedPartitionRange = 1523,
		PluginIsNotLoaded = 1524,
		WrongValue = 1525,
		NoPartitionForGivenValue = 1526,
		FileGroupOptionOnlyOnce = 1527,
		CreateFileGroupFailed = 1528,
		DropFileGroupFailed = 1529,
		TableSpaceAutoExtend = 1530,
		WrongSizeNumber = 1531,
		SizeOverflow = 1532,
		AlterFileGroupFailed = 1533,
		BinLogRowLogginFailed = 1534,
		BinLogRowWrongTableDefinition = 1535,
		BinLogRowRBRToSBR = 1536,
		EventAlreadyExists = 1537,
		EventStoreFailed = 1538,
		EventDoesNotExist = 1539,
		EventCannotAlter = 1540,
		EventDropFailed = 1541,
		EventIntervalNotPositiveOrTooBig = 1542,
		EventEndsBeforeStarts = 1543,
		EventExecTimeInThePast = 1544,
		EventOpenTableFailed = 1545,
		EventNeitherMExpresssionNorMAt = 1546,
		ColumnCountDoesNotMatchCorrupted = 1547,
		CannotLoadFromTable = 1548,
		EventCannotDelete = 1549,
		EventCompileError = 1550,
		EventSameName = 1551,
		EventDataTooLong = 1552,
		DropIndexForeignKey = 1553,
		WarningDeprecatedSyntaxWithVersion = 1554,
		CannotWriteLockLogTable = 1555,
		CannotLockLogTable = 1556,
		ForeignDuplicateKey = 1557,
		ColumnCountDoesNotMatchPleaseUpdate = 1558,
		TemoraryTablePreventSwitchOutOfRBR = 1559,
		StoredFunctionPreventsSwitchBinLogFormat = 1560,
		NDBCannotSwitchBinLogFormat = 1561,
		PartitionNoTemporary = 1562,
		PartitionConstantDomain = 1563,
		PartitionFunctionIsNotAllowed = 1564,
		DDLLogError = 1565,
		NullInValuesLessThan = 1566,
		WrongPartitionName = 1567,
		CannotChangeTransactionIsolation = 1568,
		DuplicateEntryAutoIncrementCase = 1569,
		EventModifyQueueError = 1570,
		EventSetVariableError = 1571,
		PartitionMergeError = 1572,
		CannotActivateLog = 1573,
		RBRNotAvailable = 1574,
		Base64DecodeError = 1575,
		EventRecursionForbidden = 1576,
		EventsDatabaseError = 1577,
		OnlyIntegersAllowed = 1578,
		UnsupportedLogEngine = 1579,
		BadLogStatement = 1580,
		CannotRenameLogTable = 1581,
		WrongParameterCountToNativeFCT = 1582,
		WrongParametersToNativeFCT = 1583,
		WrongParametersToStoredFCT = 1584,
		NativeFCTNameCollision = 1585,
		DuplicateEntryWithKeyName = 1586,
		BinLogPurgeEMFile = 1587,
		EventCannotCreateInThePast = 1588,
		EventCannotAlterInThePast = 1589,
		SlaveIncident = 1590,
		NoPartitionForGivenValueSilent = 1591,
		BinLogUnsafeStatement = 1592,
		SlaveFatalError = 1593,
		SlaveRelayLogReadFailure = 1594,
		SlaveRelayLogWriteFailure = 1595,
		SlaveCreateEventFailure = 1596,
		SlaveMasterComFailure = 1597,
		BinLogLoggingImpossible = 1598,
		ViewNoCreationContext = 1599,
		ViewInvalidCreationContext = 1600,
		StoredRoutineInvalidCreateionContext = 1601,
		TiggerCorruptedFile = 1602,
		TriggerNoCreationContext = 1603,
		TriggerInvalidCreationContext = 1604,
		EventInvalidCreationContext = 1605,
		TriggerCannotOpenTable = 1606,
		CannoCreateSubRoutine = 1607,
		SlaveAmbiguousExecMode = 1608,
		NoFormatDescriptionEventBeforeBinLogStatement = 1609,
		SlaveCorruptEvent = 1610,
		LoadDataInvalidColumn = 1611,
		LogPurgeNoFile = 1612,
		XARBTimeout = 1613,
		XARBDeadlock = 1614,
		NeedRePrepare = 1615,
		DelayedNotSupported = 1616,
		WarningNoMasterInfo = 1617,
		WarningOptionIgnored = 1618,
		WarningPluginDeleteBuiltIn = 1619,
		WarningPluginBusy = 1620,
		VariableIsReadonly = 1621,
		WarningEngineTransactionRollback = 1622,
		SlaveHeartbeatFailure = 1623,
		SlaveHeartbeatValueOutOfRange = 1624,
		NDBReplicationSchemaError = 1625,
		ConflictFunctionParseError = 1626,
		ExcepionsWriteError = 1627,
		TooLongTableComment = 1628,
		TooLongFieldComment = 1629,
		FunctionInExistentNameCollision = 1630,
		DatabaseNameError = 1631,
		TableNameErrror = 1632,
		PartitionNameError = 1633,
		SubPartitionNameError = 1634,
		TemporaryNameError = 1635,
		RenamedNameError = 1636,
		TooManyConcurrentTransactions = 1637,
		WarningNonASCIISeparatorNotImplemented = 1638,
		DebugSyncTimeout = 1639,
		DebugSyncHitLimit = 1640,
		ErrorLast = 1640
	}
	public sealed class MySqlHelper
	{
		private enum CharClass : byte
		{
			None,
			Quote,
			Backslash
		}

		private static string stringOfBackslashChars = "\\¥Š₩∖﹨＼";

		private static string stringOfQuoteChars = "\"'`\u00b4ʹʺʻʼˈˊˋ\u02d9\u0300\u0301‘’‚′‵❛❜＇";

		private static CharClass[] charClassArray = makeCharClassArray();

		private MySqlHelper()
		{
		}

		public static int ExecuteNonQuery(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
		{
			MySqlCommand mySqlCommand = new MySqlCommand();
			mySqlCommand.Connection = connection;
			mySqlCommand.CommandText = commandText;
			mySqlCommand.CommandType = CommandType.Text;
			if (commandParameters != null)
			{
				foreach (MySqlParameter value in commandParameters)
				{
					mySqlCommand.Parameters.Add(value);
				}
			}
			int result = mySqlCommand.ExecuteNonQuery();
			mySqlCommand.Parameters.Clear();
			return result;
		}

		public static int ExecuteNonQuery(string connectionString, string commandText, params MySqlParameter[] parms)
		{
			using MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
			mySqlConnection.Open();
			return ExecuteNonQuery(mySqlConnection, commandText, parms);
		}

		public static DataRow ExecuteDataRow(string connectionString, string commandText, params MySqlParameter[] parms)
		{
			DataSet dataSet = ExecuteDataset(connectionString, commandText, parms);
			if (dataSet == null)
			{
				return null;
			}
			if (dataSet.Tables.Count == 0)
			{
				return null;
			}
			if (dataSet.Tables[0].Rows.Count == 0)
			{
				return null;
			}
			return dataSet.Tables[0].Rows[0];
		}

		public static DataSet ExecuteDataset(string connectionString, string commandText)
		{
			return ExecuteDataset(connectionString, commandText, (MySqlParameter[])null);
		}

		public static DataSet ExecuteDataset(string connectionString, string commandText, params MySqlParameter[] commandParameters)
		{
			using MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
			mySqlConnection.Open();
			return ExecuteDataset(mySqlConnection, commandText, commandParameters);
		}

		public static DataSet ExecuteDataset(MySqlConnection connection, string commandText)
		{
			return ExecuteDataset(connection, commandText, (MySqlParameter[])null);
		}

		public static DataSet ExecuteDataset(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
		{
			MySqlCommand mySqlCommand = new MySqlCommand();
			mySqlCommand.Connection = connection;
			mySqlCommand.CommandText = commandText;
			mySqlCommand.CommandType = CommandType.Text;
			if (commandParameters != null)
			{
				foreach (MySqlParameter value in commandParameters)
				{
					mySqlCommand.Parameters.Add(value);
				}
			}
			MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
			DataSet dataSet = new DataSet();
			mySqlDataAdapter.Fill(dataSet);
			mySqlCommand.Parameters.Clear();
			return dataSet;
		}

		public static void UpdateDataSet(string connectionString, string commandText, DataSet ds, string tablename)
		{
			MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
			mySqlConnection.Open();
			MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(commandText, mySqlConnection);
			MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(mySqlDataAdapter);
			mySqlCommandBuilder.ToString();
			mySqlDataAdapter.Update(ds, tablename);
			mySqlConnection.Close();
		}

		private static MySqlDataReader ExecuteReader(MySqlConnection connection, MySqlTransaction transaction, string commandText, MySqlParameter[] commandParameters, bool ExternalConn)
		{
			MySqlCommand mySqlCommand = new MySqlCommand();
			mySqlCommand.Connection = connection;
			mySqlCommand.Transaction = transaction;
			mySqlCommand.CommandText = commandText;
			mySqlCommand.CommandType = CommandType.Text;
			if (commandParameters != null)
			{
				foreach (MySqlParameter value in commandParameters)
				{
					mySqlCommand.Parameters.Add(value);
				}
			}
			MySqlDataReader result = ((!ExternalConn) ? mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection) : mySqlCommand.ExecuteReader());
			mySqlCommand.Parameters.Clear();
			return result;
		}

		public static MySqlDataReader ExecuteReader(string connectionString, string commandText)
		{
			return ExecuteReader(connectionString, commandText, (MySqlParameter[])null);
		}

		public static MySqlDataReader ExecuteReader(MySqlConnection connection, string commandText)
		{
			return ExecuteReader(connection, null, commandText, null, ExternalConn: true);
		}

		public static MySqlDataReader ExecuteReader(string connectionString, string commandText, params MySqlParameter[] commandParameters)
		{
			MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
			mySqlConnection.Open();
			return ExecuteReader(mySqlConnection, null, commandText, commandParameters, ExternalConn: false);
		}

		public static MySqlDataReader ExecuteReader(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
		{
			return ExecuteReader(connection, null, commandText, commandParameters, ExternalConn: true);
		}

		public static object ExecuteScalar(string connectionString, string commandText)
		{
			return ExecuteScalar(connectionString, commandText, (MySqlParameter[])null);
		}

		public static object ExecuteScalar(string connectionString, string commandText, params MySqlParameter[] commandParameters)
		{
			using MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
			mySqlConnection.Open();
			return ExecuteScalar(mySqlConnection, commandText, commandParameters);
		}

		public static object ExecuteScalar(MySqlConnection connection, string commandText)
		{
			return ExecuteScalar(connection, commandText, (MySqlParameter[])null);
		}

		public static object ExecuteScalar(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
		{
			MySqlCommand mySqlCommand = new MySqlCommand();
			mySqlCommand.Connection = connection;
			mySqlCommand.CommandText = commandText;
			mySqlCommand.CommandType = CommandType.Text;
			if (commandParameters != null)
			{
				foreach (MySqlParameter value in commandParameters)
				{
					mySqlCommand.Parameters.Add(value);
				}
			}
			object result = mySqlCommand.ExecuteScalar();
			mySqlCommand.Parameters.Clear();
			return result;
		}

		private static CharClass[] makeCharClassArray()
		{
			CharClass[] array = new CharClass[65536];
			string text = stringOfBackslashChars;
			foreach (char c in text)
			{
				array[(uint)c] = CharClass.Backslash;
			}
			string text2 = stringOfQuoteChars;
			foreach (char c2 in text2)
			{
				array[(uint)c2] = CharClass.Quote;
			}
			return array;
		}

		private static bool needsQuoting(string s)
		{
			foreach (char c in s)
			{
				if (charClassArray[(uint)c] != CharClass.None)
				{
					return true;
				}
			}
			return false;
		}

		public static string EscapeString(string value)
		{
			if (!needsQuoting(value))
			{
				return value;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if (charClassArray[(uint)c] != CharClass.None)
				{
					stringBuilder.Append("\\");
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		public static string DoubleQuoteString(string value)
		{
			if (!needsQuoting(value))
			{
				return value;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				switch (charClassArray[(uint)c])
				{
				case CharClass.Quote:
					stringBuilder.Append(c);
					break;
				case CharClass.Backslash:
					stringBuilder.Append("\\");
					break;
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}
	}
	internal class MySqlPacket
	{
		private byte[] tempBuffer = new byte[256];

		private Encoding encoding;

		private MemoryStream buffer = new MemoryStream(5);

		private DBVersion version;

		public Encoding Encoding
		{
			get
			{
				return encoding;
			}
			set
			{
				encoding = value;
			}
		}

		public bool HasMoreData => buffer.Position < buffer.Length;

		public int Position
		{
			get
			{
				return (int)buffer.Position;
			}
			set
			{
				buffer.Position = value;
			}
		}

		public int Length
		{
			get
			{
				return (int)buffer.Length;
			}
			set
			{
				buffer.SetLength(value);
			}
		}

		public bool IsLastPacket
		{
			get
			{
				byte[] array = buffer.GetBuffer();
				if (array[0] == 254)
				{
					return Length <= 5;
				}
				return false;
			}
		}

		public byte[] Buffer => buffer.GetBuffer();

		public DBVersion Version
		{
			get
			{
				return version;
			}
			set
			{
				version = value;
			}
		}

		private MySqlPacket()
		{
			Clear();
		}

		public MySqlPacket(Encoding enc)
			: this()
		{
			Encoding = enc;
		}

		public MySqlPacket(MemoryStream stream)
			: this()
		{
			buffer = stream;
		}

		public void Clear()
		{
			Position = 4;
		}

		public byte ReadByte()
		{
			return (byte)buffer.ReadByte();
		}

		public int Read(byte[] byteBuffer, int offset, int count)
		{
			return buffer.Read(byteBuffer, offset, count);
		}

		public void WriteByte(byte b)
		{
			buffer.WriteByte(b);
		}

		public void Write(byte[] bytesToWrite)
		{
			Write(bytesToWrite, 0, bytesToWrite.Length);
		}

		public void Write(byte[] bytesToWrite, int offset, int countToWrite)
		{
			buffer.Write(bytesToWrite, offset, countToWrite);
		}

		public int ReadNBytes()
		{
			byte b = ReadByte();
			if (b < 1 || b > 4)
			{
				throw new MySqlException(Resources.IncorrectTransmission);
			}
			return ReadInteger(b);
		}

		public void SetByte(long position, byte value)
		{
			long position2 = buffer.Position;
			buffer.Position = position;
			buffer.WriteByte(value);
			buffer.Position = position2;
		}

		public long ReadFieldLength()
		{
			byte b = ReadByte();
			return b switch
			{
				251 => -1L, 
				252 => ReadInteger(2), 
				253 => ReadInteger(3), 
				254 => ReadLong(8), 
				_ => b, 
			};
		}

		public ulong ReadBitValue(int numbytes)
		{
			ulong num = 0uL;
			int num2 = (int)buffer.Position;
			byte[] array = buffer.GetBuffer();
			int num3 = 0;
			for (int i = 0; i < numbytes; i++)
			{
				num <<= num3;
				num |= array[num2++];
				num3 = 8;
			}
			buffer.Position += numbytes;
			return num;
		}

		public long ReadLong(int numbytes)
		{
			byte[] value = buffer.GetBuffer();
			int startIndex = (int)buffer.Position;
			buffer.Position += numbytes;
			return numbytes switch
			{
				2 => BitConverter.ToUInt16(value, startIndex), 
				4 => BitConverter.ToUInt32(value, startIndex), 
				8 => BitConverter.ToInt64(value, startIndex), 
				_ => throw new NotSupportedException("Only byte lengths of 2, 4, or 8 are supported"), 
			};
		}

		public ulong ReadULong(int numbytes)
		{
			byte[] value = buffer.GetBuffer();
			int startIndex = (int)buffer.Position;
			buffer.Position += numbytes;
			return numbytes switch
			{
				2 => BitConverter.ToUInt16(value, startIndex), 
				4 => BitConverter.ToUInt32(value, startIndex), 
				8 => BitConverter.ToUInt64(value, startIndex), 
				_ => throw new NotSupportedException("Only byte lengths of 2, 4, or 8 are supported"), 
			};
		}

		public int Read3ByteInt()
		{
			int num = 0;
			int num2 = (int)buffer.Position;
			byte[] array = buffer.GetBuffer();
			int num3 = 0;
			for (int i = 0; i < 3; i++)
			{
				num |= array[num2++] << num3;
				num3 += 8;
			}
			buffer.Position += 3L;
			return num;
		}

		public int ReadInteger(int numbytes)
		{
			if (numbytes == 3)
			{
				return Read3ByteInt();
			}
			return (int)ReadLong(numbytes);
		}

		public void WriteInteger(long v, int numbytes)
		{
			long num = v;
			for (int i = 0; i < numbytes; i++)
			{
				tempBuffer[i] = (byte)(num & 0xFF);
				num >>= 8;
			}
			Write(tempBuffer, 0, numbytes);
		}

		public int ReadPackedInteger()
		{
			byte b = ReadByte();
			return b switch
			{
				251 => -1, 
				252 => ReadInteger(2), 
				253 => ReadInteger(3), 
				254 => ReadInteger(4), 
				_ => b, 
			};
		}

		public void WriteLength(long length)
		{
			if (length < 251)
			{
				WriteByte((byte)length);
			}
			else if (length < 65536)
			{
				WriteByte(252);
				WriteInteger(length, 2);
			}
			else if (length < 16777216)
			{
				WriteByte(253);
				WriteInteger(length, 3);
			}
			else
			{
				WriteByte(254);
				WriteInteger(length, 4);
			}
		}

		public void WriteLenString(string s)
		{
			byte[] bytes = encoding.GetBytes(s);
			WriteLength(bytes.Length);
			Write(bytes, 0, bytes.Length);
		}

		public void WriteStringNoNull(string v)
		{
			byte[] bytes = encoding.GetBytes(v);
			Write(bytes, 0, bytes.Length);
		}

		public void WriteString(string v)
		{
			WriteStringNoNull(v);
			WriteByte(0);
		}

		public string ReadLenString()
		{
			long length = ReadPackedInteger();
			return ReadString(length);
		}

		public string ReadAsciiString(long length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			Read(tempBuffer, 0, (int)length);
			return Encoding.GetEncoding("us-ascii").GetString(tempBuffer, 0, (int)length);
		}

		public string ReadString(long length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			if (tempBuffer == null || length > tempBuffer.Length)
			{
				tempBuffer = new byte[length];
			}
			Read(tempBuffer, 0, (int)length);
			return encoding.GetString(tempBuffer, 0, (int)length);
		}

		public string ReadString()
		{
			return ReadString(encoding);
		}

		public string ReadString(Encoding theEncoding)
		{
			byte[] array = buffer.GetBuffer();
			int i;
			for (i = (int)buffer.Position; i < (int)buffer.Length && array[i] != 0 && array[i] != -1; i++)
			{
			}
			string result = theEncoding.GetString(array, (int)buffer.Position, i - (int)buffer.Position);
			buffer.Position = i + 1;
			return result;
		}
	}
	[Serializable]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	public sealed class MySqlClientPermissionAttribute : DBDataPermissionAttribute
	{
		public MySqlClientPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		public override IPermission CreatePermission()
		{
			return new MySqlClientPermission(this);
		}
	}
	internal sealed class MySqlPool
	{
		private List<Driver> inUsePool;

		private Queue<Driver> idlePool;

		private MySqlConnectionStringBuilder settings;

		private uint minSize;

		private uint maxSize;

		private ProcedureCache procedureCache;

		private bool beingCleared;

		private int available;

		private AutoResetEvent autoEvent;

		public MySqlConnectionStringBuilder Settings
		{
			get
			{
				return settings;
			}
			set
			{
				settings = value;
			}
		}

		public ProcedureCache ProcedureCache => procedureCache;

		private bool HasIdleConnections => idlePool.Count > 0;

		private int NumConnections => idlePool.Count + inUsePool.Count;

		public bool BeingCleared => beingCleared;

		internal Dictionary<string, string> ServerProperties { get; set; }

		private void EnqueueIdle(Driver driver)
		{
			driver.IdleSince = DateTime.Now;
			idlePool.Enqueue(driver);
		}

		public MySqlPool(MySqlConnectionStringBuilder settings)
		{
			minSize = settings.MinimumPoolSize;
			maxSize = settings.MaximumPoolSize;
			available = (int)maxSize;
			autoEvent = new AutoResetEvent(initialState: false);
			if (minSize > maxSize)
			{
				minSize = maxSize;
			}
			this.settings = settings;
			inUsePool = new List<Driver>((int)maxSize);
			idlePool = new Queue<Driver>((int)maxSize);
			for (int i = 0; i < minSize; i++)
			{
				EnqueueIdle(CreateNewPooledConnection());
			}
			procedureCache = new ProcedureCache((int)settings.ProcedureCacheSize);
		}

		private Driver GetPooledConnection()
		{
			Driver driver = null;
			lock (((ICollection)idlePool).SyncRoot)
			{
				if (HasIdleConnections)
				{
					driver = idlePool.Dequeue();
				}
			}
			if (driver != null)
			{
				try
				{
					driver.ResetTimeout((int)(Settings.ConnectionTimeout * 1000));
				}
				catch (Exception)
				{
					driver.Close();
					driver = null;
				}
			}
			if (driver != null)
			{
				if (!driver.Ping())
				{
					driver.Close();
					driver = null;
				}
				else if (settings.ConnectionReset)
				{
					driver.Reset();
				}
			}
			if (driver == null)
			{
				driver = CreateNewPooledConnection();
			}
			lock (((ICollection)inUsePool).SyncRoot)
			{
				inUsePool.Add(driver);
				return driver;
			}
		}

		private Driver CreateNewPooledConnection()
		{
			Driver driver = Driver.Create(settings);
			driver.Pool = this;
			return driver;
		}

		public void ReleaseConnection(Driver driver)
		{
			lock (((ICollection)inUsePool).SyncRoot)
			{
				if (inUsePool.Contains(driver))
				{
					inUsePool.Remove(driver);
				}
			}
			if (driver.ConnectionLifetimeExpired() || beingCleared)
			{
				driver.Close();
			}
			else
			{
				lock (((ICollection)idlePool).SyncRoot)
				{
					EnqueueIdle(driver);
				}
			}
			Interlocked.Increment(ref available);
			autoEvent.Set();
		}

		public void RemoveConnection(Driver driver)
		{
			lock (((ICollection)inUsePool).SyncRoot)
			{
				if (inUsePool.Contains(driver))
				{
					inUsePool.Remove(driver);
					Interlocked.Increment(ref available);
					autoEvent.Set();
				}
			}
			if (beingCleared && NumConnections == 0)
			{
				MySqlPoolManager.RemoveClearedPool(this);
			}
		}

		private Driver TryToGetDriver()
		{
			int num = Interlocked.Decrement(ref available);
			if (num < 0)
			{
				Interlocked.Increment(ref available);
				return null;
			}
			try
			{
				return GetPooledConnection();
			}
			catch (Exception ex)
			{
				MySqlTrace.LogError(-1, ex.Message);
				Interlocked.Increment(ref available);
				throw;
			}
		}

		public Driver GetConnection()
		{
			int num = (int)(settings.ConnectionTimeout * 1000);
			int num2 = num;
			DateTime now = DateTime.Now;
			while (num2 > 0)
			{
				Driver driver = TryToGetDriver();
				if (driver != null)
				{
					return driver;
				}
				if (!autoEvent.WaitOne(num2, exitContext: false))
				{
					break;
				}
				num2 = num - (int)DateTime.Now.Subtract(now).TotalMilliseconds;
			}
			throw new MySqlException(Resources.TimeoutGettingConnection);
		}

		internal void Clear()
		{
			lock (((ICollection)idlePool).SyncRoot)
			{
				beingCleared = true;
				while (idlePool.Count > 0)
				{
					Driver driver = idlePool.Dequeue();
					driver.Close();
				}
			}
		}

		internal List<Driver> RemoveOldIdleConnections()
		{
			List<Driver> list = new List<Driver>();
			DateTime now = DateTime.Now;
			lock (((ICollection)idlePool).SyncRoot)
			{
				while (idlePool.Count > minSize)
				{
					Driver driver = idlePool.Peek();
					if (driver.IdleSince.Add(new TimeSpan(0, 0, MySqlPoolManager.maxConnectionIdleTime)).CompareTo(now) < 0)
					{
						list.Add(driver);
						idlePool.Dequeue();
						continue;
					}
					break;
				}
			}
			return list;
		}
	}
	internal class MySqlPoolManager
	{
		private static Dictionary<string, MySqlPool> pools;

		private static List<MySqlPool> clearingPools;

		internal static int maxConnectionIdleTime;

		private static System.Threading.Timer timer;

		static MySqlPoolManager()
		{
			pools = new Dictionary<string, MySqlPool>();
			clearingPools = new List<MySqlPool>();
			maxConnectionIdleTime = 180;
			timer = new System.Threading.Timer(CleanIdleConnections, null, maxConnectionIdleTime * 1000 + 8000, maxConnectionIdleTime * 1000);
			AppDomain.CurrentDomain.ProcessExit += EnsureClearingPools;
			AppDomain.CurrentDomain.DomainUnload += EnsureClearingPools;
		}

		private static void EnsureClearingPools(object sender, EventArgs e)
		{
			ClearAllPools();
		}

		private static string GetKey(MySqlConnectionStringBuilder settings)
		{
			string text = "";
			lock (settings)
			{
				text = settings.ConnectionString;
			}
			if (settings.IntegratedSecurity && !settings.ConnectionReset)
			{
				try
				{
					WindowsIdentity current = WindowsIdentity.GetCurrent();
					text = text + ";" + current.User;
				}
				catch (SecurityException ex)
				{
					throw new MySqlException(Resources.NoWindowsIdentity, ex);
				}
			}
			return text;
		}

		public static MySqlPool GetPool(MySqlConnectionStringBuilder settings)
		{
			string key = GetKey(settings);
			lock (pools)
			{
				pools.TryGetValue(key, out var value);
				if (value == null)
				{
					value = new MySqlPool(settings);
					pools.Add(key, value);
				}
				else
				{
					value.Settings = settings;
				}
				return value;
			}
		}

		public static void RemoveConnection(Driver driver)
		{
			driver.Pool?.RemoveConnection(driver);
		}

		public static void ReleaseConnection(Driver driver)
		{
			driver.Pool?.ReleaseConnection(driver);
		}

		public static void ClearPool(MySqlConnectionStringBuilder settings)
		{
			string key;
			try
			{
				key = GetKey(settings);
			}
			catch (MySqlException)
			{
				return;
			}
			ClearPoolByText(key);
		}

		private static void ClearPoolByText(string key)
		{
			lock (pools)
			{
				if (pools.ContainsKey(key))
				{
					MySqlPool mySqlPool = pools[key];
					clearingPools.Add(mySqlPool);
					mySqlPool.Clear();
					pools.Remove(key);
				}
			}
		}

		public static void ClearAllPools()
		{
			lock (pools)
			{
				List<string> list = new List<string>(pools.Count);
				foreach (string key in pools.Keys)
				{
					list.Add(key);
				}
				foreach (string item in list)
				{
					ClearPoolByText(item);
				}
			}
		}

		public static void RemoveClearedPool(MySqlPool pool)
		{
			clearingPools.Remove(pool);
		}

		public static void CleanIdleConnections(object obj)
		{
			List<Driver> list = new List<Driver>();
			lock (pools)
			{
				foreach (string key in pools.Keys)
				{
					MySqlPool mySqlPool = pools[key];
					list.AddRange(mySqlPool.RemoveOldIdleConnections());
				}
			}
			foreach (Driver item in list)
			{
				item.Close();
			}
		}
	}
	internal class MySqlTransactionScope
	{
		public MySqlConnection connection;

		public Transaction baseTransaction;

		public MySqlTransaction simpleTransaction;

		public int rollbackThreadId;

		public MySqlTransactionScope(MySqlConnection con, Transaction trans, MySqlTransaction simpleTransaction)
		{
			connection = con;
			baseTransaction = trans;
			this.simpleTransaction = simpleTransaction;
		}

		public void Rollback(SinglePhaseEnlistment singlePhaseEnlistment)
		{
			Driver driver = connection.driver;
			lock (driver)
			{
				rollbackThreadId = Thread.CurrentThread.ManagedThreadId;
				while (connection.Reader != null)
				{
					Thread.Sleep(100);
				}
				simpleTransaction.Rollback();
				singlePhaseEnlistment.Aborted();
				DriverTransactionManager.RemoveDriverInTransaction(baseTransaction);
				driver.CurrentTransaction = null;
				if (connection.State == ConnectionState.Closed)
				{
					connection.CloseFully();
				}
				rollbackThreadId = 0;
			}
		}

		public void SinglePhaseCommit(SinglePhaseEnlistment singlePhaseEnlistment)
		{
			simpleTransaction.Commit();
			singlePhaseEnlistment.Committed();
			DriverTransactionManager.RemoveDriverInTransaction(baseTransaction);
			connection.driver.CurrentTransaction = null;
			if (connection.State == ConnectionState.Closed)
			{
				connection.CloseFully();
			}
		}
	}
	internal sealed class MySqlPromotableTransaction : IPromotableSinglePhaseNotification, ITransactionPromoter
	{
		[ThreadStatic]
		private static Stack<MySqlTransactionScope> globalScopeStack;

		private MySqlConnection connection;

		private Transaction baseTransaction;

		private Stack<MySqlTransactionScope> scopeStack;

		public Transaction BaseTransaction
		{
			get
			{
				if (scopeStack.Count > 0)
				{
					return scopeStack.Peek().baseTransaction;
				}
				return null;
			}
		}

		public bool InRollback
		{
			get
			{
				if (scopeStack.Count > 0)
				{
					MySqlTransactionScope mySqlTransactionScope = scopeStack.Peek();
					if (mySqlTransactionScope.rollbackThreadId == Thread.CurrentThread.ManagedThreadId)
					{
						return true;
					}
				}
				return false;
			}
		}

		public MySqlPromotableTransaction(MySqlConnection connection, Transaction baseTransaction)
		{
			this.connection = connection;
			this.baseTransaction = baseTransaction;
		}

		void IPromotableSinglePhaseNotification.Initialize()
		{
			string name = Enum.GetName(typeof(System.Transactions.IsolationLevel), baseTransaction.IsolationLevel);
			System.Data.IsolationLevel iso = (System.Data.IsolationLevel)Enum.Parse(typeof(System.Data.IsolationLevel), name);
			MySqlTransaction simpleTransaction = connection.BeginTransaction(iso);
			if (globalScopeStack == null)
			{
				globalScopeStack = new Stack<MySqlTransactionScope>();
			}
			scopeStack = globalScopeStack;
			scopeStack.Push(new MySqlTransactionScope(connection, baseTransaction, simpleTransaction));
		}

		void IPromotableSinglePhaseNotification.Rollback(SinglePhaseEnlistment singlePhaseEnlistment)
		{
			MySqlTransactionScope mySqlTransactionScope = scopeStack.Peek();
			mySqlTransactionScope.Rollback(singlePhaseEnlistment);
			scopeStack.Pop();
		}

		void IPromotableSinglePhaseNotification.SinglePhaseCommit(SinglePhaseEnlistment singlePhaseEnlistment)
		{
			scopeStack.Pop().SinglePhaseCommit(singlePhaseEnlistment);
		}

		byte[] ITransactionPromoter.Promote()
		{
			throw new NotSupportedException();
		}
	}
	internal class DriverTransactionManager
	{
		private static Hashtable driversInUse = new Hashtable();

		public static Driver GetDriverInTransaction(Transaction transaction)
		{
			lock (driversInUse.SyncRoot)
			{
				return (Driver)driversInUse[transaction.GetHashCode()];
			}
		}

		public static void SetDriverInTransaction(Driver driver)
		{
			lock (driversInUse.SyncRoot)
			{
				driversInUse[driver.CurrentTransaction.BaseTransaction.GetHashCode()] = driver;
			}
		}

		public static void RemoveDriverInTransaction(Transaction transaction)
		{
			lock (driversInUse.SyncRoot)
			{
				driversInUse.Remove(transaction.GetHashCode());
			}
		}
	}
	public class MySqlScript
	{
		private MySqlConnection connection;

		private string query;

		private string delimiter;

		public MySqlConnection Connection
		{
			get
			{
				return connection;
			}
			set
			{
				connection = value;
			}
		}

		public string Query
		{
			get
			{
				return query;
			}
			set
			{
				query = value;
			}
		}

		public string Delimiter
		{
			get
			{
				return delimiter;
			}
			set
			{
				delimiter = value;
			}
		}

		public event MySqlStatementExecutedEventHandler StatementExecuted;

		public event MySqlScriptErrorEventHandler Error;

		public event EventHandler ScriptCompleted;

		public MySqlScript()
		{
			Delimiter = ";";
		}

		public MySqlScript(MySqlConnection connection)
			: this()
		{
			this.connection = connection;
		}

		public MySqlScript(string query)
			: this()
		{
			this.query = query;
		}

		public MySqlScript(MySqlConnection connection, string query)
			: this()
		{
			this.connection = connection;
			this.query = query;
		}

		public int Execute()
		{
			bool flag = false;
			if (connection == null)
			{
				throw new InvalidOperationException(Resources.ConnectionNotSet);
			}
			if (query == null || query.Length == 0)
			{
				return 0;
			}
			if (connection.State != ConnectionState.Open)
			{
				flag = true;
				connection.Open();
			}
			bool allowUserVariables = connection.Settings.AllowUserVariables;
			connection.Settings.AllowUserVariables = true;
			try
			{
				string s = connection.driver.Property("sql_mode");
				s = StringUtility.ToUpperInvariant(s);
				bool ansiQuotes = s.IndexOf("ANSI_QUOTES") != -1;
				bool noBackslashEscapes = s.IndexOf("NO_BACKSLASH_ESCAPES") != -1;
				List<ScriptStatement> list = BreakIntoStatements(ansiQuotes, noBackslashEscapes);
				int num = 0;
				MySqlCommand mySqlCommand = new MySqlCommand(null, connection);
				foreach (ScriptStatement item in list)
				{
					if (string.IsNullOrEmpty(item.text))
					{
						continue;
					}
					mySqlCommand.CommandText = item.text;
					try
					{
						mySqlCommand.ExecuteNonQuery();
						num++;
						OnQueryExecuted(item);
					}
					catch (Exception ex)
					{
						if (this.Error == null)
						{
							throw;
						}
						if (!OnScriptError(ex))
						{
							break;
						}
					}
				}
				OnScriptCompleted();
				return num;
			}
			finally
			{
				connection.Settings.AllowUserVariables = allowUserVariables;
				if (flag)
				{
					connection.Close();
				}
			}
		}

		private void OnQueryExecuted(ScriptStatement statement)
		{
			if (this.StatementExecuted != null)
			{
				MySqlScriptEventArgs e = new MySqlScriptEventArgs();
				e.Statement = statement;
				this.StatementExecuted(this, e);
			}
		}

		private void OnScriptCompleted()
		{
			if (this.ScriptCompleted != null)
			{
				this.ScriptCompleted(this, EventArgs.Empty);
			}
		}

		private bool OnScriptError(Exception ex)
		{
			if (this.Error != null)
			{
				MySqlScriptErrorEventArgs e = new MySqlScriptErrorEventArgs(ex);
				this.Error(this, e);
				return e.Ignore;
			}
			return false;
		}

		private List<int> BreakScriptIntoLines()
		{
			List<int> list = new List<int>();
			StringReader stringReader = new StringReader(query);
			string text = stringReader.ReadLine();
			int num = 0;
			while (text != null)
			{
				list.Add(num);
				num += text.Length;
				text = stringReader.ReadLine();
			}
			return list;
		}

		private static int FindLineNumber(int position, List<int> lineNumbers)
		{
			int i;
			for (i = 0; i < lineNumbers.Count && position < lineNumbers[i]; i++)
			{
			}
			return i;
		}

		private List<ScriptStatement> BreakIntoStatements(bool ansiQuotes, bool noBackslashEscapes)
		{
			string text = Delimiter;
			int num = 0;
			List<ScriptStatement> list = new List<ScriptStatement>();
			List<int> list2 = BreakScriptIntoLines();
			MySqlTokenizer mySqlTokenizer = new MySqlTokenizer(query);
			mySqlTokenizer.AnsiQuotes = ansiQuotes;
			mySqlTokenizer.BackslashEscapes = !noBackslashEscapes;
			for (string text2 = mySqlTokenizer.NextToken(); text2 != null; text2 = mySqlTokenizer.NextToken())
			{
				if (!mySqlTokenizer.Quoted)
				{
					if (text2.ToLower(CultureInfo.InvariantCulture) == "delimiter")
					{
						mySqlTokenizer.NextToken();
						AdjustDelimiterEnd(mySqlTokenizer);
						text = query.Substring(mySqlTokenizer.StartIndex, mySqlTokenizer.StopIndex - mySqlTokenizer.StartIndex).Trim();
						num = mySqlTokenizer.StopIndex;
					}
					else
					{
						if (text.StartsWith(text2, StringComparison.OrdinalIgnoreCase) && mySqlTokenizer.StartIndex + text.Length <= query.Length && query.Substring(mySqlTokenizer.StartIndex, text.Length) == text)
						{
							text2 = text;
							mySqlTokenizer.Position = mySqlTokenizer.StartIndex + text.Length;
							mySqlTokenizer.StopIndex = mySqlTokenizer.Position;
						}
						int num2 = text2.IndexOf(text, StringComparison.OrdinalIgnoreCase);
						if (num2 != -1)
						{
							int num3 = mySqlTokenizer.StopIndex - text2.Length + num2;
							if (mySqlTokenizer.StopIndex == query.Length - 1)
							{
								num3++;
							}
							string text3 = query.Substring(num, num3 - num);
							ScriptStatement item = default(ScriptStatement);
							item.text = text3.Trim();
							item.line = FindLineNumber(num, list2);
							item.position = num - list2[item.line];
							list.Add(item);
							num = num3 + text.Length;
						}
					}
				}
			}
			if (num < query.Length - 1)
			{
				string text4 = query.Substring(num).Trim();
				if (!string.IsNullOrEmpty(text4))
				{
					ScriptStatement item2 = default(ScriptStatement);
					item2.text = text4;
					item2.line = FindLineNumber(num, list2);
					item2.position = num - list2[item2.line];
					list.Add(item2);
				}
			}
			return list;
		}

		private void AdjustDelimiterEnd(MySqlTokenizer tokenizer)
		{
			if (tokenizer.StopIndex < query.Length)
			{
				int num = tokenizer.StopIndex;
				char c = query[num];
				while (!char.IsWhiteSpace(c) && num < query.Length - 1)
				{
					c = query[++num];
				}
				tokenizer.StopIndex = num;
				tokenizer.Position = num;
			}
		}
	}
	public delegate void MySqlStatementExecutedEventHandler(object sender, MySqlScriptEventArgs args);
	public delegate void MySqlScriptErrorEventHandler(object sender, MySqlScriptErrorEventArgs args);
	public class MySqlScriptEventArgs : EventArgs
	{
		private ScriptStatement statement;

		internal ScriptStatement Statement
		{
			set
			{
				statement = value;
			}
		}

		public string StatementText => statement.text;

		public int Line => statement.line;

		public int Position => statement.position;
	}
	public class MySqlScriptErrorEventArgs : MySqlScriptEventArgs
	{
		private Exception exception;

		private bool ignore;

		public Exception Exception => exception;

		public bool Ignore
		{
			get
			{
				return ignore;
			}
			set
			{
				ignore = value;
			}
		}

		public MySqlScriptErrorEventArgs(Exception exception)
		{
			this.exception = exception;
		}
	}
	internal struct ScriptStatement
	{
		public string text;

		public int line;

		public int position;
	}
	public sealed class MySqlSecurityPermission : MarshalByRefObject
	{
		private MySqlSecurityPermission()
		{
		}

		public static PermissionSet CreatePermissionSet(bool includeReflectionPermission)
		{
			PermissionSet permissionSet = new PermissionSet(null);
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			permissionSet.AddPermission(new SocketPermission(PermissionState.Unrestricted));
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			permissionSet.AddPermission(new DnsPermission(PermissionState.Unrestricted));
			permissionSet.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			permissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			if (includeReflectionPermission)
			{
				permissionSet.AddPermission(new ReflectionPermission(PermissionState.Unrestricted));
			}
			return permissionSet;
		}
	}
	internal class MySqlStream
	{
		private byte sequenceByte;

		private int maxBlockSize;

		private ulong maxPacketSize;

		private byte[] packetHeader = new byte[4];

		private MySqlPacket packet;

		private TimedStream timedStream;

		private Stream inStream;

		private Stream outStream;

		internal Stream BaseStream => timedStream;

		public Encoding Encoding
		{
			get
			{
				return packet.Encoding;
			}
			set
			{
				packet.Encoding = value;
			}
		}

		public byte SequenceByte
		{
			get
			{
				return sequenceByte;
			}
			set
			{
				sequenceByte = value;
			}
		}

		public int MaxBlockSize
		{
			get
			{
				return maxBlockSize;
			}
			set
			{
				maxBlockSize = value;
			}
		}

		public ulong MaxPacketSize
		{
			get
			{
				return maxPacketSize;
			}
			set
			{
				maxPacketSize = value;
			}
		}

		public MySqlStream(Encoding encoding)
		{
			maxPacketSize = ulong.MaxValue;
			maxBlockSize = int.MaxValue;
			packet = new MySqlPacket(encoding);
		}

		public MySqlStream(Stream baseStream, Encoding encoding, bool compress)
			: this(encoding)
		{
			timedStream = new TimedStream(baseStream);
			Stream stream = ((!compress) ? ((Stream)timedStream) : ((Stream)new CompressedStream(timedStream)));
			inStream = new BufferedStream(stream);
			outStream = stream;
		}

		public void Close()
		{
			outStream.Close();
			inStream.Close();
			timedStream.Close();
		}

		public void ResetTimeout(int timeout)
		{
			timedStream.ResetTimeout(timeout);
		}

		public MySqlPacket ReadPacket()
		{
			LoadPacket();
			if (packet.Buffer[0] == byte.MaxValue)
			{
				packet.ReadByte();
				int errno = packet.ReadInteger(2);
				string empty = string.Empty;
				empty = ((!packet.Version.isAtLeast(5, 5, 0)) ? packet.ReadString() : packet.ReadString(Encoding.UTF8));
				if (empty.StartsWith("#", StringComparison.Ordinal))
				{
					empty.Substring(1, 5);
					empty = empty.Substring(6);
				}
				throw new MySqlException(empty, errno);
			}
			return packet;
		}

		internal static void ReadFully(Stream stream, byte[] buffer, int offset, int count)
		{
			int num = 0;
			int num2 = count;
			while (num2 > 0)
			{
				int num3 = stream.Read(buffer, offset + num, num2);
				if (num3 == 0)
				{
					throw new EndOfStreamException();
				}
				num += num3;
				num2 -= num3;
			}
		}

		public void LoadPacket()
		{
			try
			{
				packet.Length = 0;
				int num = 0;
				int num2;
				do
				{
					ReadFully(inStream, packetHeader, 0, 4);
					sequenceByte = (byte)(packetHeader[3] + 1);
					num2 = packetHeader[0] + (packetHeader[1] << 8) + (packetHeader[2] << 16);
					packet.Length += num2;
					ReadFully(inStream, packet.Buffer, num, num2);
					num += num2;
				}
				while (num2 >= maxBlockSize);
				packet.Position = 0;
			}
			catch (IOException inner)
			{
				throw new MySqlException(Resources.ReadFromStreamFailed, isFatal: true, inner);
			}
		}

		public void SendPacket(MySqlPacket packet)
		{
			byte[] buffer = packet.Buffer;
			int num = packet.Position - 4;
			if ((ulong)num > maxPacketSize)
			{
				throw new MySqlException(Resources.QueryTooLarge, 1153);
			}
			int num2 = 0;
			while (num > 0)
			{
				int num3 = ((num > maxBlockSize) ? maxBlockSize : num);
				buffer[num2] = (byte)(num3 & 0xFF);
				buffer[num2 + 1] = (byte)((num3 >> 8) & 0xFF);
				buffer[num2 + 2] = (byte)((num3 >> 16) & 0xFF);
				buffer[num2 + 3] = sequenceByte++;
				outStream.Write(buffer, num2, num3 + 4);
				outStream.Flush();
				num -= num3;
				num2 += num3;
			}
		}

		public void SendEntirePacketDirectly(byte[] buffer, int count)
		{
			buffer[0] = (byte)(count & 0xFF);
			buffer[1] = (byte)((count >> 8) & 0xFF);
			buffer[2] = (byte)((count >> 16) & 0xFF);
			buffer[3] = sequenceByte++;
			outStream.Write(buffer, 0, count + 4);
			outStream.Flush();
		}
	}
	internal class NativeDriver : IDriver
	{
		private const string AuthenticationWindowsPlugin = "authentication_windows_client";

		private const string AuthenticationWindowsUser = "auth_windows";

		private DBVersion version;

		private int threadId;

		protected string encryptionSeed;

		protected ServerStatusFlags serverStatus;

		protected MySqlStream stream;

		protected Stream baseStream;

		private BitArray nullMap;

		private MySqlPacket packet;

		private ClientFlags connectionFlags;

		private Driver owner;

		private int warnings;

		private MySqlAuthenticationPlugin authPlugin;

		public ClientFlags Flags => connectionFlags;

		public int ThreadId => threadId;

		public DBVersion Version => version;

		public ServerStatusFlags ServerStatus => serverStatus;

		public int WarningCount => warnings;

		public MySqlPacket Packet => packet;

		internal MySqlConnectionStringBuilder Settings => owner.Settings;

		internal Encoding Encoding => owner.Encoding;

		public NativeDriver(Driver owner)
		{
			this.owner = owner;
			threadId = -1;
		}

		private void HandleException(MySqlException ex)
		{
			if (ex.IsFatal)
			{
				owner.Close();
			}
		}

		internal void SendPacket(MySqlPacket p)
		{
			stream.SendPacket(p);
		}

		internal void SendEmptyPacket()
		{
			byte[] buffer = new byte[4];
			stream.SendEntirePacketDirectly(buffer, 0);
		}

		internal MySqlPacket ReadPacket()
		{
			return packet = stream.ReadPacket();
		}

		internal void ReadOk(bool read)
		{
			try
			{
				if (read)
				{
					packet = stream.ReadPacket();
				}
				if (packet.ReadByte() != 0)
				{
					throw new MySqlException("Out of sync with server", isFatal: true, null);
				}
				packet.ReadFieldLength();
				packet.ReadFieldLength();
				if (packet.HasMoreData)
				{
					serverStatus = (ServerStatusFlags)packet.ReadInteger(2);
					packet.ReadInteger(2);
					if (packet.HasMoreData)
					{
						packet.ReadLenString();
					}
				}
			}
			catch (MySqlException ex)
			{
				HandleException(ex);
				throw;
			}
		}

		public void SetDatabase(string dbName)
		{
			byte[] bytes = Encoding.GetBytes(dbName);
			packet.Clear();
			packet.WriteByte(2);
			packet.Write(bytes);
			ExecutePacket(packet);
			ReadOk(read: true);
		}

		public void Configure()
		{
			stream.MaxPacketSize = (ulong)owner.MaxPacketSize;
			stream.Encoding = Encoding;
		}

		public void Open()
		{
			try
			{
				baseStream = StreamCreator.GetStream(Settings);
				if (Settings.IncludeSecurityAsserts)
				{
					MySqlSecurityPermission.CreatePermissionSet(includeReflectionPermission: false).Assert();
				}
			}
			catch (SecurityException)
			{
				throw;
			}
			catch (Exception inner)
			{
				throw new MySqlException(Resources.UnableToConnectToHost, 1042, inner);
			}
			if (baseStream == null)
			{
				throw new MySqlException(Resources.UnableToConnectToHost, 1042);
			}
			int num = 16581375;
			stream = new MySqlStream(baseStream, Encoding, compress: false);
			stream.ResetTimeout((int)(Settings.ConnectionTimeout * 1000));
			packet = stream.ReadPacket();
			packet.ReadByte();
			string text = packet.ReadString();
			owner.isFabric = text.EndsWith("fabric", StringComparison.OrdinalIgnoreCase);
			version = DBVersion.Parse(text);
			if (!owner.isFabric && !version.isAtLeast(5, 0, 0))
			{
				throw new NotSupportedException(Resources.ServerTooOld);
			}
			threadId = packet.ReadInteger(4);
			encryptionSeed = packet.ReadString();
			num = 16777215;
			ClientFlags clientFlags = (ClientFlags)0uL;
			if (packet.HasMoreData)
			{
				clientFlags = (ClientFlags)packet.ReadInteger(2);
			}
			owner.ConnectionCharSetIndex = packet.ReadByte();
			serverStatus = (ServerStatusFlags)packet.ReadInteger(2);
			uint num2 = (uint)packet.ReadInteger(2);
			clientFlags = (ClientFlags)((ulong)clientFlags | (ulong)(num2 << 16));
			packet.Position += 11;
			string text2 = packet.ReadString();
			encryptionSeed += text2;
			string text3 = "";
			text3 = (((clientFlags & ClientFlags.PLUGIN_AUTH) == 0) ? "mysql_native_password" : packet.ReadString());
			SetConnectionFlags(clientFlags);
			packet.Clear();
			packet.WriteInteger((int)connectionFlags, 4);
			if ((clientFlags & ClientFlags.SSL) == 0)
			{
				if (Settings.SslMode != MySqlSslMode.None && Settings.SslMode != MySqlSslMode.Preferred)
				{
					string msg = string.Format(Resources.NoServerSSLSupport, Settings.Server);
					throw new MySqlException(msg);
				}
			}
			else if (Settings.SslMode != MySqlSslMode.None)
			{
				stream.SendPacket(packet);
				StartSSL();
				packet.Clear();
				packet.WriteInteger((int)connectionFlags, 4);
			}
			packet.WriteInteger(num, 4);
			packet.WriteByte(8);
			packet.Write(new byte[23]);
			Authenticate(text3, reset: false);
			if ((connectionFlags & ClientFlags.COMPRESS) != 0)
			{
				stream = new MySqlStream(baseStream, Encoding, compress: true);
			}
			packet.Version = version;
			stream.MaxBlockSize = num;
		}

		private X509CertificateCollection GetClientCertificates()
		{
			X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
			if (Settings.CertificateFile != null)
			{
				if (!Version.isAtLeast(5, 1, 0))
				{
					throw new MySqlException(Resources.FileBasedCertificateNotSupported);
				}
				X509Certificate2 value = new X509Certificate2(Settings.CertificateFile, Settings.CertificatePassword);
				x509CertificateCollection.Add(value);
				return x509CertificateCollection;
			}
			if (Settings.CertificateStoreLocation == MySqlCertificateStoreLocation.None)
			{
				return x509CertificateCollection;
			}
			StoreLocation storeLocation = ((Settings.CertificateStoreLocation == MySqlCertificateStoreLocation.CurrentUser) ? StoreLocation.CurrentUser : StoreLocation.LocalMachine);
			X509Store x509Store = new X509Store(StoreName.My, storeLocation);
			x509Store.Open(OpenFlags.OpenExistingOnly);
			if (Settings.CertificateThumbprint == null)
			{
				x509CertificateCollection.AddRange(x509Store.Certificates);
				return x509CertificateCollection;
			}
			x509CertificateCollection.AddRange(x509Store.Certificates.Find(X509FindType.FindByThumbprint, Settings.CertificateThumbprint, validOnly: true));
			if (x509CertificateCollection.Count == 0)
			{
				throw new MySqlException("Certificate with Thumbprint " + Settings.CertificateThumbprint + " not found");
			}
			return x509CertificateCollection;
		}

		private void StartSSL()
		{
			RemoteCertificateValidationCallback userCertificateValidationCallback = ServerCheckValidation;
			SslStream sslStream = new SslStream(baseStream, leaveInnerStreamOpen: true, userCertificateValidationCallback, null);
			X509CertificateCollection clientCertificates = GetClientCertificates();
			sslStream.AuthenticateAsClient(Settings.Server, clientCertificates, SslProtocols.Default, checkCertificateRevocation: false);
			baseStream = sslStream;
			stream = new MySqlStream(sslStream, Encoding, compress: false);
			stream.SequenceByte = 2;
		}

		private bool ServerCheckValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if (Settings.SslMode == MySqlSslMode.Preferred || Settings.SslMode == MySqlSslMode.Required)
			{
				return true;
			}
			if (Settings.SslMode == MySqlSslMode.VerifyCA && sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
			{
				return true;
			}
			return false;
		}

		private void SetConnectionFlags(ClientFlags serverCaps)
		{
			ClientFlags clientFlags = ClientFlags.LOCAL_FILES;
			if (!Settings.UseAffectedRows)
			{
				clientFlags |= ClientFlags.FOUND_ROWS;
			}
			clientFlags |= ClientFlags.PROTOCOL_41;
			clientFlags |= ClientFlags.TRANSACTIONS;
			if (Settings.AllowBatch)
			{
				clientFlags |= ClientFlags.MULTI_STATEMENTS;
			}
			clientFlags |= ClientFlags.MULTI_RESULTS;
			if ((serverCaps & ClientFlags.LONG_FLAG) != 0)
			{
				clientFlags |= ClientFlags.LONG_FLAG;
			}
			if ((serverCaps & ClientFlags.COMPRESS) != 0 && Settings.UseCompression)
			{
				clientFlags |= ClientFlags.COMPRESS;
			}
			clientFlags |= ClientFlags.LONG_PASSWORD;
			if (Settings.InteractiveSession)
			{
				clientFlags |= ClientFlags.INTERACTIVE;
			}
			if ((serverCaps & ClientFlags.CONNECT_WITH_DB) != 0 && Settings.Database != null && Settings.Database.Length > 0)
			{
				clientFlags |= ClientFlags.CONNECT_WITH_DB;
			}
			if ((serverCaps & ClientFlags.SECURE_CONNECTION) != 0)
			{
				clientFlags |= ClientFlags.SECURE_CONNECTION;
			}
			if ((serverCaps & ClientFlags.SSL) != 0 && Settings.SslMode != MySqlSslMode.None)
			{
				clientFlags |= ClientFlags.SSL;
			}
			if ((serverCaps & ClientFlags.PS_MULTI_RESULTS) != 0)
			{
				clientFlags |= ClientFlags.PS_MULTI_RESULTS;
			}
			if ((serverCaps & ClientFlags.PLUGIN_AUTH) != 0)
			{
				clientFlags |= ClientFlags.PLUGIN_AUTH;
			}
			if ((serverCaps & ClientFlags.CONNECT_ATTRS) != 0)
			{
				clientFlags |= ClientFlags.CONNECT_ATTRS;
			}
			if ((serverCaps & ClientFlags.CAN_HANDLE_EXPIRED_PASSWORD) != 0)
			{
				clientFlags |= ClientFlags.CAN_HANDLE_EXPIRED_PASSWORD;
			}
			connectionFlags = clientFlags;
		}

		public void Authenticate(string authMethod, bool reset)
		{
			if (authMethod != null)
			{
				byte[] bytes = Encoding.GetBytes(encryptionSeed);
				if (Settings.IntegratedSecurity)
				{
					authMethod = "authentication_windows_client";
				}
				authPlugin = MySqlAuthenticationPlugin.GetPlugin(authMethod, this, bytes);
			}
			authPlugin.Authenticate(reset);
		}

		public void Reset()
		{
			warnings = 0;
			stream.Encoding = Encoding;
			stream.SequenceByte = 0;
			packet.Clear();
			packet.WriteByte(17);
			Authenticate(null, reset: true);
		}

		public void SendQuery(MySqlPacket queryPacket)
		{
			warnings = 0;
			queryPacket.SetByte(4L, 3);
			ExecutePacket(queryPacket);
			serverStatus |= ServerStatusFlags.AnotherQuery;
		}

		public void Close(bool isOpen)
		{
			try
			{
				if (isOpen)
				{
					try
					{
						packet.Clear();
						packet.WriteByte(1);
						ExecutePacket(packet);
					}
					catch (Exception)
					{
					}
				}
				if (stream != null)
				{
					stream.Close();
				}
				stream = null;
			}
			catch (Exception)
			{
			}
		}

		public bool Ping()
		{
			try
			{
				packet.Clear();
				packet.WriteByte(14);
				ExecutePacket(packet);
				ReadOk(read: true);
				return true;
			}
			catch (Exception)
			{
				owner.Close();
				return false;
			}
		}

		public int GetResult(ref int affectedRow, ref long insertedId)
		{
			try
			{
				packet = stream.ReadPacket();
			}
			catch (TimeoutException)
			{
				throw;
			}
			catch (Exception)
			{
				serverStatus = (ServerStatusFlags)0;
				throw;
			}
			int num = (int)packet.ReadFieldLength();
			if (-1 == num)
			{
				string filename = packet.ReadString();
				SendFileToServer(filename);
				return GetResult(ref affectedRow, ref insertedId);
			}
			if (num == 0)
			{
				serverStatus &= ~(ServerStatusFlags.MoreResults | ServerStatusFlags.AnotherQuery);
				affectedRow = (int)packet.ReadFieldLength();
				insertedId = packet.ReadFieldLength();
				serverStatus = (ServerStatusFlags)packet.ReadInteger(2);
				warnings += packet.ReadInteger(2);
				if (packet.HasMoreData)
				{
					packet.ReadLenString();
				}
			}
			return num;
		}

		private void SendFileToServer(string filename)
		{
			byte[] buffer = new byte[8196];
			long num = 0L;
			try
			{
				using FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
				num = fileStream.Length;
				while (num > 0)
				{
					int num2 = fileStream.Read(buffer, 4, (int)((num > 8192) ? 8192 : num));
					stream.SendEntirePacketDirectly(buffer, num2);
					num -= num2;
				}
				stream.SendEntirePacketDirectly(buffer, 0);
			}
			catch (Exception ex)
			{
				throw new MySqlException("Error during LOAD DATA LOCAL INFILE", ex);
			}
		}

		private void ReadNullMap(int fieldCount)
		{
			nullMap = null;
			byte[] array = new byte[(fieldCount + 9) / 8];
			packet.ReadByte();
			packet.Read(array, 0, array.Length);
			nullMap = new BitArray(array);
		}

		public IMySqlValue ReadColumnValue(int index, MySqlField field, IMySqlValue valObject)
		{
			long num = -1L;
			bool isNull;
			if (nullMap != null)
			{
				isNull = nullMap[index + 2];
			}
			else
			{
				num = packet.ReadFieldLength();
				isNull = num == -1;
			}
			packet.Encoding = field.Encoding;
			packet.Version = version;
			return valObject.ReadValue(packet, num, isNull);
		}

		public void SkipColumnValue(IMySqlValue valObject)
		{
			int num = -1;
			if (nullMap == null)
			{
				num = (int)packet.ReadFieldLength();
				if (num == -1)
				{
					return;
				}
			}
			if (num > -1)
			{
				packet.Position += num;
			}
			else
			{
				valObject.SkipValue(packet);
			}
		}

		public void GetColumnsData(MySqlField[] columns)
		{
			for (int i = 0; i < columns.Length; i++)
			{
				GetColumnData(columns[i]);
			}
			ReadEOF();
		}

		private void GetColumnData(MySqlField field)
		{
			stream.Encoding = Encoding;
			packet = stream.ReadPacket();
			field.Encoding = Encoding;
			field.CatalogName = packet.ReadLenString();
			field.DatabaseName = packet.ReadLenString();
			field.TableName = packet.ReadLenString();
			field.RealTableName = packet.ReadLenString();
			field.ColumnName = packet.ReadLenString();
			field.OriginalColumnName = packet.ReadLenString();
			packet.ReadByte();
			field.CharacterSetIndex = packet.ReadInteger(2);
			field.ColumnLength = packet.ReadInteger(4);
			MySqlDbType mySqlDbType = (MySqlDbType)packet.ReadByte();
			ColumnFlags columnFlags = (ColumnFlags)(((connectionFlags & ClientFlags.LONG_FLAG) == 0) ? packet.ReadByte() : packet.ReadInteger(2));
			field.Scale = packet.ReadByte();
			if (packet.HasMoreData)
			{
				packet.ReadInteger(2);
			}
			if (mySqlDbType == MySqlDbType.Decimal || mySqlDbType == MySqlDbType.NewDecimal)
			{
				field.Precision = (byte)(field.ColumnLength - 2);
				if ((columnFlags & ColumnFlags.UNSIGNED) != 0)
				{
					field.Precision++;
				}
			}
			field.SetTypeAndFlags(mySqlDbType, columnFlags);
		}

		private void ExecutePacket(MySqlPacket packetToExecute)
		{
			try
			{
				warnings = 0;
				stream.SequenceByte = 0;
				stream.SendPacket(packetToExecute);
			}
			catch (MySqlException ex)
			{
				HandleException(ex);
				throw;
			}
		}

		public void ExecuteStatement(MySqlPacket packetToExecute)
		{
			warnings = 0;
			packetToExecute.SetByte(4L, 23);
			ExecutePacket(packetToExecute);
			serverStatus |= ServerStatusFlags.AnotherQuery;
		}

		private void CheckEOF()
		{
			if (!packet.IsLastPacket)
			{
				throw new MySqlException("Expected end of data packet");
			}
			packet.ReadByte();
			if (packet.HasMoreData)
			{
				warnings += packet.ReadInteger(2);
				serverStatus = (ServerStatusFlags)packet.ReadInteger(2);
			}
		}

		private void ReadEOF()
		{
			packet = stream.ReadPacket();
			CheckEOF();
		}

		public int PrepareStatement(string sql, ref MySqlField[] parameters)
		{
			packet.Length = sql.Length * 4 + 5;
			byte[] buffer = packet.Buffer;
			int bytes = Encoding.GetBytes(sql, 0, sql.Length, packet.Buffer, 5);
			packet.Position = bytes + 5;
			buffer[4] = 22;
			ExecutePacket(packet);
			packet = stream.ReadPacket();
			if (packet.ReadByte() != 0)
			{
				throw new MySqlException("Expected prepared statement marker");
			}
			int result = packet.ReadInteger(4);
			int num = packet.ReadInteger(2);
			int num2 = packet.ReadInteger(2);
			packet.ReadInteger(3);
			if (num2 > 0)
			{
				parameters = owner.GetColumns(num2);
				for (int i = 0; i < parameters.Length; i++)
				{
					parameters[i].Encoding = Encoding;
				}
			}
			if (num > 0)
			{
				while (num-- > 0)
				{
					packet = stream.ReadPacket();
				}
				ReadEOF();
			}
			return result;
		}

		public bool FetchDataRow(int statementId, int columns)
		{
			packet = stream.ReadPacket();
			if (packet.IsLastPacket)
			{
				CheckEOF();
				return false;
			}
			nullMap = null;
			if (statementId > 0)
			{
				ReadNullMap(columns);
			}
			return true;
		}

		public void CloseStatement(int statementId)
		{
			packet.Clear();
			packet.WriteByte(25);
			packet.WriteInteger(statementId, 4);
			stream.SequenceByte = 0;
			stream.SendPacket(packet);
		}

		public void ResetTimeout(int timeout)
		{
			if (stream != null)
			{
				stream.ResetTimeout(timeout);
			}
		}

		internal void SetConnectAttrs()
		{
			if ((connectionFlags & ClientFlags.CONNECT_ATTRS) == 0)
			{
				return;
			}
			string text = string.Empty;
			MySqlConnectAttrs mySqlConnectAttrs = new MySqlConnectAttrs();
			PropertyInfo[] properties = mySqlConnectAttrs.GetType().GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				string text2 = propertyInfo.Name;
				object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), inherit: false);
				if (customAttributes.Length > 0)
				{
					text2 = (customAttributes[0] as DisplayNameAttribute).DisplayName;
				}
				string text3 = (string)propertyInfo.GetValue(mySqlConnectAttrs, null);
				text += $"{(char)text2.Length}{text2}";
				text += $"{(char)text3.Length}{text3}";
			}
			packet.WriteLenString(text);
		}
	}
	internal class PerformanceMonitor
	{
		private MySqlConnection connection;

		public MySqlConnection Connection { get; private set; }

		public PerformanceMonitor(MySqlConnection connection)
		{
			Connection = connection;
		}

		public virtual void AddHardProcedureQuery()
		{
		}

		public virtual void AddSoftProcedureQuery()
		{
		}
	}
	internal abstract class Statement
	{
		protected MySqlCommand command;

		protected string commandText;

		private List<MySqlPacket> buffers;

		public virtual string ResolvedCommandText => commandText;

		protected Driver Driver => command.Connection.driver;

		protected MySqlConnection Connection => command.Connection;

		protected MySqlParameterCollection Parameters => command.Parameters;

		private Statement(MySqlCommand cmd)
		{
			command = cmd;
			buffers = new List<MySqlPacket>();
		}

		public Statement(MySqlCommand cmd, string text)
			: this(cmd)
		{
			commandText = text;
		}

		public virtual void Close(MySqlDataReader reader)
		{
		}

		public virtual void Resolve(bool preparing)
		{
		}

		public virtual void Execute()
		{
			BindParameters();
			ExecuteNext();
		}

		public virtual bool ExecuteNext()
		{
			if (buffers.Count == 0)
			{
				return false;
			}
			MySqlPacket p = buffers[0];
			Driver.SendQuery(p);
			buffers.RemoveAt(0);
			return true;
		}

		protected virtual void BindParameters()
		{
			MySqlParameterCollection parameters = command.Parameters;
			int num = 0;
			do
			{
				InternalBindParameters(ResolvedCommandText, parameters, null);
				if (command.Batch == null)
				{
					break;
				}
				while (num < command.Batch.Count)
				{
					MySqlCommand mySqlCommand = command.Batch[num++];
					MySqlPacket mySqlPacket = buffers[buffers.Count - 1];
					long num2 = mySqlCommand.EstimatedSize();
					if (mySqlPacket.Length - 4 + num2 > Connection.driver.MaxPacketSize)
					{
						parameters = mySqlCommand.Parameters;
						break;
					}
					buffers.RemoveAt(buffers.Count - 1);
					string resolvedCommandText = ResolvedCommandText;
					if (resolvedCommandText.StartsWith("(", StringComparison.Ordinal))
					{
						mySqlPacket.WriteStringNoNull(", ");
					}
					else
					{
						mySqlPacket.WriteStringNoNull("; ");
					}
					InternalBindParameters(resolvedCommandText, mySqlCommand.Parameters, mySqlPacket);
					if (mySqlPacket.Length - 4 > Connection.driver.MaxPacketSize)
					{
						parameters = mySqlCommand.Parameters;
						break;
					}
				}
			}
			while (num != command.Batch.Count);
		}

		private void InternalBindParameters(string sql, MySqlParameterCollection parameters, MySqlPacket packet)
		{
			bool sqlServerMode = command.Connection.Settings.SqlServerMode;
			if (packet == null)
			{
				packet = new MySqlPacket(Driver.Encoding);
				packet.Version = Driver.Version;
				packet.WriteByte(0);
			}
			MySqlTokenizer mySqlTokenizer = new MySqlTokenizer(sql);
			mySqlTokenizer.ReturnComments = true;
			mySqlTokenizer.SqlServerMode = sqlServerMode;
			int num = 0;
			string text = mySqlTokenizer.NextToken();
			int num2 = 0;
			while (text != null)
			{
				packet.WriteStringNoNull(sql.Substring(num, mySqlTokenizer.StartIndex - num));
				num = mySqlTokenizer.StopIndex;
				if (MySqlTokenizer.IsParameter(text))
				{
					if ((!parameters.containsUnnamedParameters && text.Length == 1 && num2 > 0) || (parameters.containsUnnamedParameters && text.Length > 1))
					{
						throw new MySqlException(Resources.MixedParameterNamingNotAllowed);
					}
					parameters.containsUnnamedParameters = text.Length == 1;
					if (SerializeParameter(parameters, packet, text, num2))
					{
						text = null;
					}
					num2++;
				}
				if (text != null)
				{
					if (sqlServerMode && mySqlTokenizer.Quoted && text.StartsWith("[", StringComparison.Ordinal))
					{
						text = $"`{text.Substring(1, text.Length - 2)}`";
					}
					packet.WriteStringNoNull(text);
				}
				text = mySqlTokenizer.NextToken();
			}
			buffers.Add(packet);
		}

		protected virtual bool ShouldIgnoreMissingParameter(string parameterName)
		{
			if (Connection.Settings.AllowUserVariables)
			{
				return true;
			}
			if (parameterName.StartsWith("@_cnet_param_", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (parameterName.Length > 1 && (parameterName[1] == '`' || parameterName[1] == '\''))
			{
				return true;
			}
			return false;
		}

		private bool SerializeParameter(MySqlParameterCollection parameters, MySqlPacket packet, string parmName, int parameterIndex)
		{
			MySqlParameter mySqlParameter = null;
			if (!parameters.containsUnnamedParameters)
			{
				mySqlParameter = parameters.GetParameterFlexible(parmName, throwOnNotFound: false);
			}
			else
			{
				if (parameterIndex > parameters.Count)
				{
					throw new MySqlException(Resources.ParameterIndexNotFound);
				}
				mySqlParameter = parameters[parameterIndex];
			}
			if (mySqlParameter == null)
			{
				if (parmName.StartsWith("@", StringComparison.Ordinal) && ShouldIgnoreMissingParameter(parmName))
				{
					return false;
				}
				throw new MySqlException(string.Format(Resources.ParameterMustBeDefined, parmName));
			}
			mySqlParameter.Serialize(packet, binary: false, Connection.Settings);
			return true;
		}
	}
	internal class PreparableStatement : Statement
	{
		private int executionCount;

		private int statementId;

		private BitArray nullMap;

		private List<MySqlParameter> parametersToSend = new List<MySqlParameter>();

		private MySqlPacket packet;

		private int dataPosition;

		private int nullMapPosition;

		public int ExecutionCount
		{
			get
			{
				return executionCount;
			}
			set
			{
				executionCount = value;
			}
		}

		public bool IsPrepared => statementId > 0;

		public int StatementId => statementId;

		public PreparableStatement(MySqlCommand command, string text)
			: base(command, text)
		{
		}

		public virtual void Prepare()
		{
			string stripped_sql;
			List<string> list = PrepareCommandText(out stripped_sql);
			MySqlField[] parameters = null;
			statementId = base.Driver.PrepareStatement(stripped_sql, ref parameters);
			for (int i = 0; i < list.Count; i++)
			{
				string text = list[i];
				MySqlParameter parameterFlexible = base.Parameters.GetParameterFlexible(text, throwOnNotFound: false);
				if (parameterFlexible == null)
				{
					throw new InvalidOperationException(string.Format(Resources.ParameterNotFoundDuringPrepare, text));
				}
				parameterFlexible.Encoding = parameters[i].Encoding;
				parametersToSend.Add(parameterFlexible);
			}
			int num = 0;
			if (parameters != null && parameters.Length > 0)
			{
				nullMap = new BitArray(parameters.Length);
				num = (nullMap.Count + 7) / 8;
			}
			packet = new MySqlPacket(base.Driver.Encoding);
			packet.WriteByte(0);
			packet.WriteInteger(statementId, 4);
			packet.WriteByte(0);
			packet.WriteInteger(1L, 4);
			nullMapPosition = packet.Position;
			packet.Position += num;
			packet.WriteByte(1);
			foreach (MySqlParameter item in parametersToSend)
			{
				packet.WriteInteger(item.GetPSType(), 2);
			}
			dataPosition = packet.Position;
		}

		public override void Execute()
		{
			if (!IsPrepared)
			{
				base.Execute();
				return;
			}
			packet.Position = dataPosition;
			for (int i = 0; i < parametersToSend.Count; i++)
			{
				MySqlParameter mySqlParameter = parametersToSend[i];
				nullMap[i] = mySqlParameter.Value == DBNull.Value || mySqlParameter.Value == null || mySqlParameter.Direction == ParameterDirection.Output;
				if (!nullMap[i])
				{
					packet.Encoding = mySqlParameter.Encoding;
					mySqlParameter.Serialize(packet, binary: true, base.Connection.Settings);
				}
			}
			if (nullMap != null)
			{
				nullMap.CopyTo(packet.Buffer, nullMapPosition);
			}
			executionCount++;
			base.Driver.ExecuteStatement(packet);
		}

		public override bool ExecuteNext()
		{
			if (!IsPrepared)
			{
				return base.ExecuteNext();
			}
			return false;
		}

		private List<string> PrepareCommandText(out string stripped_sql)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<string> list = new List<string>();
			int num = 0;
			string resolvedCommandText = ResolvedCommandText;
			MySqlTokenizer mySqlTokenizer = new MySqlTokenizer(resolvedCommandText);
			for (string text = mySqlTokenizer.NextParameter(); text != null; text = mySqlTokenizer.NextParameter())
			{
				if (text.IndexOf("_cnet_param_") == -1)
				{
					stringBuilder.Append(resolvedCommandText.Substring(num, mySqlTokenizer.StartIndex - num));
					stringBuilder.Append("?");
					list.Add(text);
					num = mySqlTokenizer.StopIndex;
				}
			}
			stringBuilder.Append(resolvedCommandText.Substring(num));
			stripped_sql = stringBuilder.ToString();
			return list;
		}

		public virtual void CloseStatement()
		{
			if (IsPrepared)
			{
				base.Driver.CloseStatement(statementId);
				statementId = 0;
			}
		}
	}
	internal class ProcedureCacheEntry
	{
		public MySqlSchemaCollection procedure;

		public MySqlSchemaCollection parameters;
	}
	internal class ProcedureCache
	{
		private Dictionary<int, ProcedureCacheEntry> procHash;

		private Queue<int> hashQueue;

		private int maxSize;

		public ProcedureCache(int size)
		{
			maxSize = size;
			hashQueue = new Queue<int>(maxSize);
			procHash = new Dictionary<int, ProcedureCacheEntry>(maxSize);
		}

		public ProcedureCacheEntry GetProcedure(MySqlConnection conn, string spName, string cacheKey)
		{
			ProcedureCacheEntry value = null;
			if (cacheKey != null)
			{
				int hashCode = cacheKey.GetHashCode();
				lock (procHash)
				{
					procHash.TryGetValue(hashCode, out value);
				}
			}
			if (value == null)
			{
				value = AddNew(conn, spName);
				conn.PerfMonitor.AddHardProcedureQuery();
				if (conn.Settings.Logging)
				{
					MySqlTrace.LogInformation(conn.ServerThread, string.Format(Resources.HardProcQuery, spName));
				}
			}
			else
			{
				conn.PerfMonitor.AddSoftProcedureQuery();
				if (conn.Settings.Logging)
				{
					MySqlTrace.LogInformation(conn.ServerThread, string.Format(Resources.SoftProcQuery, spName));
				}
			}
			return value;
		}

		internal string GetCacheKey(string spName, ProcedureCacheEntry proc)
		{
			string text = string.Empty;
			StringBuilder stringBuilder = new StringBuilder(spName);
			stringBuilder.Append("(");
			string text2 = "";
			if (proc.parameters != null)
			{
				foreach (MySqlSchemaRow row in proc.parameters.Rows)
				{
					if (row["ORDINAL_POSITION"].Equals(0))
					{
						text = "?=";
						continue;
					}
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}?", new object[1] { text2 });
					text2 = ",";
				}
			}
			stringBuilder.Append(")");
			return text + stringBuilder.ToString();
		}

		private ProcedureCacheEntry AddNew(MySqlConnection connection, string spName)
		{
			ProcedureCacheEntry procData = GetProcData(connection, spName);
			if (maxSize > 0)
			{
				string cacheKey = GetCacheKey(spName, procData);
				int hashCode = cacheKey.GetHashCode();
				lock (procHash)
				{
					if (procHash.Keys.Count >= maxSize)
					{
						TrimHash();
					}
					if (!procHash.ContainsKey(hashCode))
					{
						procHash[hashCode] = procData;
						hashQueue.Enqueue(hashCode);
					}
				}
			}
			return procData;
		}

		private void TrimHash()
		{
			int key = hashQueue.Dequeue();
			procHash.Remove(key);
		}

		private static ProcedureCacheEntry GetProcData(MySqlConnection connection, string spName)
		{
			string text = string.Empty;
			string text2 = spName;
			int num = spName.IndexOf(".");
			if (num != -1)
			{
				text = spName.Substring(0, num);
				text2 = spName.Substring(num + 1, spName.Length - num - 1);
			}
			string[] restrictionValues = new string[4]
			{
				null,
				(text.Length > 0) ? text : connection.CurrentDatabase(),
				text2,
				null
			};
			MySqlSchemaCollection schemaCollection = connection.GetSchemaCollection("procedures", restrictionValues);
			if (schemaCollection.Rows.Count > 1)
			{
				throw new MySqlException(Resources.ProcAndFuncSameName);
			}
			if (schemaCollection.Rows.Count == 0)
			{
				throw new MySqlException(string.Format(Resources.InvalidProcName, text2, text));
			}
			ProcedureCacheEntry procedureCacheEntry = new ProcedureCacheEntry();
			procedureCacheEntry.procedure = schemaCollection;
			ISSchemaProvider iSSchemaProvider = new ISSchemaProvider(connection);
			string[] restrictions = iSSchemaProvider.CleanRestrictions(restrictionValues);
			MySqlSchemaCollection procedureParameters = iSSchemaProvider.GetProcedureParameters(restrictions, schemaCollection);
			procedureCacheEntry.parameters = procedureParameters;
			return procedureCacheEntry;
		}
	}
	public sealed class ReplicationConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("ServerGroups", IsRequired = true)]
		[ConfigurationCollection(typeof(ReplicationServerGroupConfigurationElement), AddItemName = "Group")]
		public GenericConfigurationElementCollection<ReplicationServerGroupConfigurationElement> ServerGroups => (GenericConfigurationElementCollection<ReplicationServerGroupConfigurationElement>)base["ServerGroups"];
	}
	public sealed class ReplicationServerGroupConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		[ConfigurationProperty("groupType", IsRequired = false)]
		public string GroupType
		{
			get
			{
				return (string)base["groupType"];
			}
			set
			{
				base["groupType"] = value;
			}
		}

		[ConfigurationProperty("retryTime", IsRequired = false, DefaultValue = 60)]
		public int RetryTime
		{
			get
			{
				return (int)base["retryTime"];
			}
			set
			{
				base["retryTime"] = value;
			}
		}

		[ConfigurationProperty("Servers")]
		[ConfigurationCollection(typeof(ReplicationServerConfigurationElement), AddItemName = "Server")]
		public GenericConfigurationElementCollection<ReplicationServerConfigurationElement> Servers => (GenericConfigurationElementCollection<ReplicationServerConfigurationElement>)base["Servers"];
	}
	public sealed class ReplicationServerConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		[ConfigurationProperty("IsMaster", IsRequired = false, DefaultValue = false)]
		public bool IsMaster
		{
			get
			{
				return (bool)base["IsMaster"];
			}
			set
			{
				base["IsMaster"] = value;
			}
		}

		[ConfigurationProperty("connectionstring", IsRequired = true)]
		public string ConnectionString
		{
			get
			{
				return (string)base["connectionstring"];
			}
			set
			{
				base["connectionstring"] = value;
			}
		}
	}
}
namespace MySql.Data.MySqlClient.Replication
{
	internal static class ReplicationManager
	{
		private static List<ReplicationServerGroup> groups;

		private static object thisLock;

		internal static IList<ReplicationServerGroup> Groups { get; private set; }

		static ReplicationManager()
		{
			groups = new List<ReplicationServerGroup>();
			thisLock = new object();
			Groups = groups;
			if (MySqlConfiguration.Settings == null)
			{
				return;
			}
			foreach (ReplicationServerGroupConfigurationElement serverGroup in MySqlConfiguration.Settings.Replication.ServerGroups)
			{
				ReplicationServerGroup replicationServerGroup = AddGroup(serverGroup.Name, serverGroup.GroupType, serverGroup.RetryTime);
				foreach (ReplicationServerConfigurationElement server in serverGroup.Servers)
				{
					replicationServerGroup.AddServer(server.Name, server.IsMaster, server.ConnectionString);
				}
			}
		}

		internal static ReplicationServerGroup AddGroup(string name, int retryTime)
		{
			return AddGroup(name, null, retryTime);
		}

		internal static ReplicationServerGroup AddGroup(string name, string groupType, int retryTime)
		{
			if (string.IsNullOrEmpty(groupType))
			{
				groupType = "MySql.Data.MySqlClient.Replication.ReplicationRoundRobinServerGroup";
			}
			Type type = Type.GetType(groupType);
			ReplicationServerGroup replicationServerGroup = (ReplicationServerGroup)Activator.CreateInstance(type, name, retryTime);
			groups.Add(replicationServerGroup);
			return replicationServerGroup;
		}

		internal static ReplicationServer GetServer(string groupName, bool isMaster)
		{
			ReplicationServerGroup replicationServerGroup = GetGroup(groupName);
			return replicationServerGroup.GetServer(isMaster);
		}

		internal static ReplicationServerGroup GetGroup(string groupName)
		{
			ReplicationServerGroup replicationServerGroup = null;
			foreach (ReplicationServerGroup group in groups)
			{
				if (string.Compare(group.Name, groupName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					replicationServerGroup = group;
					break;
				}
			}
			if (replicationServerGroup == null)
			{
				throw new MySqlException(string.Format(Resources.ReplicationGroupNotFound, groupName));
			}
			return replicationServerGroup;
		}

		internal static bool IsReplicationGroup(string groupName)
		{
			foreach (ReplicationServerGroup group in groups)
			{
				if (string.Compare(group.Name, groupName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		internal static void GetNewConnection(string groupName, bool master, MySqlConnection connection)
		{
			while (true)
			{
				lock (thisLock)
				{
					if (!IsReplicationGroup(groupName))
					{
						break;
					}
					ReplicationServerGroup replicationServerGroup = GetGroup(groupName);
					ReplicationServer server = replicationServerGroup.GetServer(master, connection.Settings);
					if (server == null)
					{
						throw new MySqlException(Resources.Replication_NoAvailableServer);
					}
					try
					{
						bool flag = false;
						if (connection.driver == null || !connection.driver.IsOpen)
						{
							flag = true;
						}
						else
						{
							MySqlConnectionStringBuilder mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder(server.ConnectionString);
							if (!mySqlConnectionStringBuilder.Equals(connection.driver.Settings))
							{
								flag = true;
							}
						}
						if (flag)
						{
							Driver driver = Driver.Create(new MySqlConnectionStringBuilder(server.ConnectionString));
							connection.driver = driver;
						}
						break;
					}
					catch (MySqlException ex)
					{
						connection.driver = null;
						server.IsAvailable = false;
						MySqlTrace.LogError(ex.Number, ex.ToString());
						if (ex.Number == 1042)
						{
							replicationServerGroup.HandleFailover(server, ex);
							continue;
						}
						throw;
					}
				}
			}
		}
	}
	public abstract class ReplicationServerGroup
	{
		protected List<ReplicationServer> servers = new List<ReplicationServer>();

		public string Name { get; protected set; }

		public int RetryTime { get; protected set; }

		protected IList<ReplicationServer> Servers { get; private set; }

		public ReplicationServerGroup(string name, int retryTime)
		{
			Servers = servers;
			Name = name;
			RetryTime = retryTime;
		}

		protected internal ReplicationServer AddServer(string name, bool isMaster, string connectionString)
		{
			ReplicationServer replicationServer = new ReplicationServer(name, isMaster, connectionString);
			servers.Add(replicationServer);
			return replicationServer;
		}

		protected internal void RemoveServer(string name)
		{
			ReplicationServer server = GetServer(name);
			if (server == null)
			{
				throw new MySqlException(string.Format(Resources.ReplicationServerNotFound, name));
			}
			servers.Remove(server);
		}

		protected internal ReplicationServer GetServer(string name)
		{
			foreach (ReplicationServer server in servers)
			{
				if (string.Compare(name, server.Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return server;
				}
			}
			return null;
		}

		protected internal abstract ReplicationServer GetServer(bool isMaster);

		protected internal virtual ReplicationServer GetServer(bool isMaster, MySqlConnectionStringBuilder settings)
		{
			return GetServer(isMaster);
		}

		protected internal virtual void HandleFailover(ReplicationServer server)
		{
			BackgroundWorker backgroundWorker = new BackgroundWorker();
			backgroundWorker.DoWork += delegate(object sender, DoWorkEventArgs e)
			{
				bool isRunning = false;
				ReplicationServer server2 = e.Argument as ReplicationServer;
				System.Timers.Timer timer = new System.Timers.Timer((double)RetryTime * 1000.0);
				ElapsedEventHandler elapsedEventHandler = delegate
				{
					if (isRunning)
					{
						return;
					}
					try
					{
						isRunning = true;
						using MySqlConnection mySqlConnection = new MySqlConnection(server.ConnectionString);
						mySqlConnection.Open();
						server2.IsAvailable = true;
						timer.Stop();
					}
					catch
					{
						MySqlTrace.LogWarning(0, string.Format(Resources.Replication_ConnectionAttemptFailed, server2.Name));
					}
					finally
					{
						isRunning = false;
					}
				};
				timer.Elapsed += elapsedEventHandler;
				timer.Start();
				elapsedEventHandler(sender, null);
			};
			backgroundWorker.RunWorkerAsync(server);
		}

		protected internal virtual void HandleFailover(ReplicationServer server, Exception exception)
		{
			HandleFailover(server);
		}
	}
	public class ReplicationRoundRobinServerGroup : ReplicationServerGroup
	{
		private int nextServer;

		public ReplicationRoundRobinServerGroup(string name, int retryTime)
			: base(name, retryTime)
		{
			nextServer = -1;
		}

		protected internal override ReplicationServer GetServer(bool isMaster)
		{
			for (int i = 0; i < base.Servers.Count; i++)
			{
				nextServer++;
				if (nextServer == base.Servers.Count)
				{
					nextServer = 0;
				}
				ReplicationServer replicationServer = base.Servers[nextServer];
				if (replicationServer.IsAvailable && (!isMaster || replicationServer.IsMaster))
				{
					return replicationServer;
				}
			}
			return null;
		}
	}
	public class ReplicationServer
	{
		public string Name { get; private set; }

		public bool IsMaster { get; private set; }

		public string ConnectionString { get; internal set; }

		public bool IsAvailable { get; set; }

		public ReplicationServer(string name, bool isMaster, string connectionString)
		{
			Name = name;
			IsMaster = isMaster;
			ConnectionString = connectionString;
			IsAvailable = true;
		}
	}
}
namespace MySql.Data.MySqlClient
{
	internal class ResultSet
	{
		private Driver driver;

		private bool hasRows;

		private bool[] uaFieldsUsed;

		private MySqlField[] fields;

		private IMySqlValue[] values;

		private Dictionary<string, int> fieldHashCS;

		private Dictionary<string, int> fieldHashCI;

		private int rowIndex;

		private bool readDone;

		private bool isSequential;

		private int seqIndex;

		private bool isOutputParameters;

		private int affectedRows;

		private long insertedId;

		private int statementId;

		private int totalRows;

		private int skippedRows;

		private bool cached;

		private List<IMySqlValue[]> cachedValues;

		public bool HasRows => hasRows;

		public int Size
		{
			get
			{
				if (fields != null)
				{
					return fields.Length;
				}
				return 0;
			}
		}

		public MySqlField[] Fields => fields;

		public IMySqlValue[] Values => values;

		public bool IsOutputParameters
		{
			get
			{
				return isOutputParameters;
			}
			set
			{
				isOutputParameters = value;
			}
		}

		public int AffectedRows => affectedRows;

		public long InsertedId => insertedId;

		public int TotalRows => totalRows;

		public int SkippedRows => skippedRows;

		public bool Cached
		{
			get
			{
				return cached;
			}
			set
			{
				cached = value;
				if (cached && cachedValues == null)
				{
					cachedValues = new List<IMySqlValue[]>();
				}
			}
		}

		public IMySqlValue this[int index]
		{
			get
			{
				if (rowIndex < 0)
				{
					throw new MySqlException(Resources.AttemptToAccessBeforeRead);
				}
				uaFieldsUsed[index] = true;
				if (isSequential && index != seqIndex)
				{
					if (index < seqIndex)
					{
						throw new MySqlException(Resources.ReadingPriorColumnUsingSeqAccess);
					}
					while (seqIndex < index - 1)
					{
						driver.SkipColumnValue(values[++seqIndex]);
					}
					values[index] = driver.ReadColumnValue(index, fields[index], values[index]);
					seqIndex = index;
				}
				return values[index];
			}
		}

		public ResultSet(int affectedRows, long insertedId)
		{
			this.affectedRows = affectedRows;
			this.insertedId = insertedId;
			readDone = true;
		}

		public ResultSet(Driver d, int statementId, int numCols)
		{
			affectedRows = -1;
			insertedId = -1L;
			driver = d;
			this.statementId = statementId;
			rowIndex = -1;
			LoadColumns(numCols);
			isOutputParameters = IsOutputParameterResultSet();
			hasRows = GetNextRow();
			readDone = !hasRows;
		}

		public int GetOrdinal(string name)
		{
			if (fieldHashCS.TryGetValue(name, out var value))
			{
				return value;
			}
			if (fieldHashCI.TryGetValue(name, out value))
			{
				return value;
			}
			throw new IndexOutOfRangeException(string.Format(Resources.CouldNotFindColumnName, name));
		}

		private bool GetNextRow()
		{
			bool flag = driver.FetchDataRow(statementId, Size);
			if (flag)
			{
				totalRows++;
			}
			return flag;
		}

		public bool NextRow(CommandBehavior behavior)
		{
			if (readDone)
			{
				if (Cached)
				{
					return CachedNextRow(behavior);
				}
				return false;
			}
			if ((behavior & CommandBehavior.SingleRow) != CommandBehavior.Default && rowIndex == 0)
			{
				return false;
			}
			isSequential = (behavior & CommandBehavior.SequentialAccess) != 0;
			seqIndex = -1;
			if (rowIndex >= 0)
			{
				bool flag = false;
				try
				{
					flag = GetNextRow();
				}
				catch (MySqlException ex)
				{
					if (ex.IsQueryAborted)
					{
						readDone = true;
					}
					throw;
				}
				if (!flag)
				{
					readDone = true;
					return false;
				}
			}
			if (!isSequential)
			{
				ReadColumnData(outputParms: false);
			}
			rowIndex++;
			return true;
		}

		private bool CachedNextRow(CommandBehavior behavior)
		{
			if ((behavior & CommandBehavior.SingleRow) != CommandBehavior.Default && rowIndex == 0)
			{
				return false;
			}
			if (rowIndex == totalRows - 1)
			{
				return false;
			}
			rowIndex++;
			values = cachedValues[rowIndex];
			return true;
		}

		public void Close()
		{
			if (!readDone)
			{
				if (HasRows && rowIndex == -1)
				{
					skippedRows++;
				}
				try
				{
					while (driver.IsOpen && driver.SkipDataRow())
					{
						totalRows++;
						skippedRows++;
					}
				}
				catch (IOException)
				{
				}
				readDone = true;
			}
			else if (driver == null)
			{
				CacheClose();
			}
			driver = null;
			if (Cached)
			{
				CacheReset();
			}
		}

		private void CacheClose()
		{
			skippedRows = totalRows - rowIndex - 1;
		}

		private void CacheReset()
		{
			if (Cached)
			{
				rowIndex = -1;
				affectedRows = -1;
				insertedId = -1L;
				skippedRows = 0;
			}
		}

		public bool FieldRead(int index)
		{
			return uaFieldsUsed[index];
		}

		public void SetValueObject(int i, IMySqlValue valueObject)
		{
			values[i] = valueObject;
		}

		private bool IsOutputParameterResultSet()
		{
			if (driver.HasStatus(ServerStatusFlags.OutputParameters))
			{
				return true;
			}
			if (fields.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < fields.Length; i++)
			{
				if (!fields[i].ColumnName.StartsWith("@_cnet_param_", StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			return true;
		}

		private void LoadColumns(int numCols)
		{
			fields = driver.GetColumns(numCols);
			values = new IMySqlValue[numCols];
			uaFieldsUsed = new bool[numCols];
			fieldHashCS = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			fieldHashCI = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i < fields.Length; i++)
			{
				string columnName = fields[i].ColumnName;
				if (!fieldHashCS.ContainsKey(columnName))
				{
					fieldHashCS.Add(columnName, i);
				}
				if (!fieldHashCI.ContainsKey(columnName))
				{
					fieldHashCI.Add(columnName, i);
				}
				values[i] = fields[i].GetValueObject();
			}
		}

		private void ReadColumnData(bool outputParms)
		{
			for (int i = 0; i < Size; i++)
			{
				values[i] = driver.ReadColumnValue(i, fields[i], values[i]);
			}
			if (Cached)
			{
				cachedValues.Add((IMySqlValue[])values.Clone());
			}
			if (outputParms)
			{
				bool flag = driver.FetchDataRow(statementId, fields.Length);
				rowIndex = 0;
				if (flag)
				{
					throw new MySqlException(Resources.MoreThanOneOPRow);
				}
			}
		}
	}
	public class MySqlSchemaCollection
	{
		private List<SchemaColumn> columns = new List<SchemaColumn>();

		private List<MySqlSchemaRow> rows = new List<MySqlSchemaRow>();

		private DataTable _table;

		internal Dictionary<string, int> Mapping;

		internal Dictionary<int, int> LogicalMappings;

		public string Name { get; set; }

		public IList<SchemaColumn> Columns => columns;

		public IList<MySqlSchemaRow> Rows => rows;

		public MySqlSchemaCollection()
		{
			Mapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			LogicalMappings = new Dictionary<int, int>();
		}

		public MySqlSchemaCollection(string name)
			: this()
		{
			Name = name;
		}

		public MySqlSchemaCollection(DataTable dt)
			: this()
		{
			_table = dt;
			int num = 0;
			foreach (DataColumn column in dt.Columns)
			{
				columns.Add(new SchemaColumn
				{
					Name = column.ColumnName,
					Type = column.DataType
				});
				Mapping.Add(column.ColumnName, num++);
				LogicalMappings[columns.Count - 1] = columns.Count - 1;
			}
			foreach (DataRow row in dt.Rows)
			{
				MySqlSchemaRow mySqlSchemaRow = new MySqlSchemaRow(this);
				for (num = 0; num < columns.Count; num++)
				{
					mySqlSchemaRow[num] = row[num];
				}
				rows.Add(mySqlSchemaRow);
			}
		}

		internal SchemaColumn AddColumn(string name, Type t)
		{
			SchemaColumn schemaColumn = new SchemaColumn();
			schemaColumn.Name = name;
			schemaColumn.Type = t;
			columns.Add(schemaColumn);
			Mapping.Add(name, columns.Count - 1);
			LogicalMappings[columns.Count - 1] = columns.Count - 1;
			return schemaColumn;
		}

		internal int ColumnIndex(string name)
		{
			int result = -1;
			for (int i = 0; i < columns.Count; i++)
			{
				SchemaColumn schemaColumn = columns[i];
				if (string.Compare(schemaColumn.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					result = i;
					break;
				}
			}
			return result;
		}

		internal void RemoveColumn(string name)
		{
			int num = ColumnIndex(name);
			if (num == -1)
			{
				throw new InvalidOperationException();
			}
			columns.RemoveAt(num);
			for (int i = num; i < Columns.Count; i++)
			{
				LogicalMappings[i] += 1;
			}
		}

		internal bool ContainsColumn(string name)
		{
			return ColumnIndex(name) >= 0;
		}

		internal MySqlSchemaRow AddRow()
		{
			MySqlSchemaRow mySqlSchemaRow = new MySqlSchemaRow(this);
			rows.Add(mySqlSchemaRow);
			return mySqlSchemaRow;
		}

		internal MySqlSchemaRow NewRow()
		{
			return new MySqlSchemaRow(this);
		}

		internal DataTable AsDataTable()
		{
			if (_table != null)
			{
				return _table;
			}
			DataTable dataTable = new DataTable(Name);
			foreach (SchemaColumn column in Columns)
			{
				dataTable.Columns.Add(column.Name, column.Type);
			}
			foreach (MySqlSchemaRow row in Rows)
			{
				DataRow dataRow = dataTable.NewRow();
				for (int i = 0; i < dataTable.Columns.Count; i++)
				{
					dataRow[i] = ((row[i] == null) ? DBNull.Value : row[i]);
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}
	}
	public class MySqlSchemaRow
	{
		private Dictionary<int, object> data;

		internal MySqlSchemaCollection Collection { get; private set; }

		internal object this[string s]
		{
			get
			{
				return GetValueForName(s);
			}
			set
			{
				SetValueForName(s, value);
			}
		}

		internal object this[int i]
		{
			get
			{
				int key = Collection.LogicalMappings[i];
				if (!data.ContainsKey(key))
				{
					data[key] = null;
				}
				return data[key];
			}
			set
			{
				data[Collection.LogicalMappings[i]] = value;
			}
		}

		public MySqlSchemaRow(MySqlSchemaCollection c)
		{
			Collection = c;
			InitMetadata();
		}

		internal void InitMetadata()
		{
			data = new Dictionary<int, object>();
		}

		private void SetValueForName(string colName, object value)
		{
			int i = Collection.Mapping[colName];
			this[i] = value;
		}

		private object GetValueForName(string colName)
		{
			int num = Collection.Mapping[colName];
			if (!data.ContainsKey(num))
			{
				data[num] = null;
			}
			return this[num];
		}

		internal void CopyRow(MySqlSchemaRow row)
		{
			if (Collection.Columns.Count != row.Collection.Columns.Count)
			{
				throw new InvalidOperationException("column count doesn't match");
			}
			for (int i = 0; i < Collection.Columns.Count; i++)
			{
				row[i] = this[i];
			}
		}
	}
	public class SchemaColumn
	{
		public string Name { get; set; }

		public Type Type { get; set; }
	}
	internal class StoredProcedure : PreparableStatement
	{
		internal const string ParameterPrefix = "_cnet_param_";

		private string outSelect;

		private string resolvedCommandText;

		private bool serverProvidingOutputParameters;

		public bool ServerProvidingOutputParameters => serverProvidingOutputParameters;

		public override string ResolvedCommandText => resolvedCommandText;

		public StoredProcedure(MySqlCommand cmd, string text)
			: base(cmd, text)
		{
		}

		private MySqlParameter GetReturnParameter()
		{
			if (base.Parameters != null)
			{
				foreach (MySqlParameter parameter in base.Parameters)
				{
					if (parameter.Direction == ParameterDirection.ReturnValue)
					{
						return parameter;
					}
				}
			}
			return null;
		}

		internal string GetCacheKey(string spName)
		{
			string text = string.Empty;
			StringBuilder stringBuilder = new StringBuilder(spName);
			stringBuilder.Append("(");
			string text2 = "";
			foreach (MySqlParameter parameter in command.Parameters)
			{
				if (parameter.Direction == ParameterDirection.ReturnValue)
				{
					text = "?=";
					continue;
				}
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}?", new object[1] { text2 });
				text2 = ",";
			}
			stringBuilder.Append(")");
			return text + stringBuilder.ToString();
		}

		private ProcedureCacheEntry GetParameters(string procName)
		{
			string cacheKey = GetCacheKey(procName);
			return base.Connection.ProcedureCache.GetProcedure(base.Connection, procName, cacheKey);
		}

		public static string GetFlags(string dtd)
		{
			int num = dtd.Length - 1;
			while (num > 0 && (char.IsLetterOrDigit(dtd[num]) || dtd[num] == ' '))
			{
				num--;
			}
			string s = dtd.Substring(num);
			return StringUtility.ToUpperInvariant(s);
		}

		private string FixProcedureName(string name)
		{
			string[] array = name.Split(new char[1] { '.' });
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].StartsWith("`", StringComparison.Ordinal))
				{
					array[i] = $"`{array[i]}`";
				}
			}
			if (array.Length == 1)
			{
				return array[0];
			}
			return $"{array[0]}.{array[1]}";
		}

		private MySqlParameter GetAndFixParameter(string spName, MySqlSchemaRow param, bool realAsFloat, MySqlParameter returnParameter)
		{
			_ = (string)param["PARAMETER_MODE"];
			string parameterName = (string)param["PARAMETER_NAME"];
			if (param["ORDINAL_POSITION"].Equals(0))
			{
				if (returnParameter == null)
				{
					throw new InvalidOperationException(string.Format(Resources.RoutineRequiresReturnParameter, spName));
				}
				parameterName = returnParameter.ParameterName;
			}
			MySqlParameter parameterFlexible = command.Parameters.GetParameterFlexible(parameterName, throwOnNotFound: true);
			if (!parameterFlexible.TypeHasBeenSet)
			{
				string typeName = (string)param["DATA_TYPE"];
				bool unsigned = GetFlags(param["DTD_IDENTIFIER"].ToString()).IndexOf("UNSIGNED") != -1;
				parameterFlexible.MySqlDbType = MetaData.NameToType(typeName, unsigned, realAsFloat, base.Connection);
			}
			return parameterFlexible;
		}

		private MySqlParameterCollection CheckParameters(string spName)
		{
			MySqlParameterCollection mySqlParameterCollection = new MySqlParameterCollection(command);
			MySqlParameter returnParameter = GetReturnParameter();
			ProcedureCacheEntry parameters = GetParameters(spName);
			if (parameters.procedure == null || parameters.procedure.Rows.Count == 0)
			{
				throw new InvalidOperationException(string.Format(Resources.RoutineNotFound, spName));
			}
			bool realAsFloat = parameters.procedure.Rows[0]["SQL_MODE"].ToString().IndexOf("REAL_AS_FLOAT") != -1;
			foreach (MySqlSchemaRow row in parameters.parameters.Rows)
			{
				mySqlParameterCollection.Add(GetAndFixParameter(spName, row, realAsFloat, returnParameter));
			}
			return mySqlParameterCollection;
		}

		public override void Resolve(bool preparing)
		{
			if (resolvedCommandText == null)
			{
				serverProvidingOutputParameters = base.Driver.SupportsOutputParameters && preparing;
				string text = commandText;
				if (text.IndexOf(".") == -1 && !string.IsNullOrEmpty(base.Connection.Database))
				{
					text = base.Connection.Database + "." + text;
				}
				text = FixProcedureName(text);
				MySqlParameter returnParameter = GetReturnParameter();
				MySqlParameterCollection parms = (command.Connection.Settings.CheckParameters ? CheckParameters(text) : base.Parameters);
				string arg = SetUserVariables(parms, preparing);
				string arg2 = CreateCallStatement(text, returnParameter, parms);
				string arg3 = CreateOutputSelect(parms, preparing);
				resolvedCommandText = $"{arg}{arg2}{arg3}";
			}
		}

		private string SetUserVariables(MySqlParameterCollection parms, bool preparing)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (serverProvidingOutputParameters)
			{
				return stringBuilder.ToString();
			}
			string text = string.Empty;
			foreach (MySqlParameter parm in parms)
			{
				if (parm.Direction == ParameterDirection.InputOutput)
				{
					string arg = "@" + parm.BaseName;
					string arg2 = "@_cnet_param_" + parm.BaseName;
					string text2 = $"SET {arg2}={arg}";
					if (command.Connection.Settings.AllowBatch && !preparing)
					{
						stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", new object[2] { text, text2 });
						text = "; ";
					}
					else
					{
						MySqlCommand mySqlCommand = new MySqlCommand(text2, command.Connection);
						mySqlCommand.Parameters.Add(parm);
						mySqlCommand.ExecuteNonQuery();
					}
				}
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append("; ");
			}
			return stringBuilder.ToString();
		}

		private string CreateCallStatement(string spName, MySqlParameter returnParameter, MySqlParameterCollection parms)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			foreach (MySqlParameter parm in parms)
			{
				if (parm.Direction != ParameterDirection.ReturnValue)
				{
					string text2 = "@" + parm.BaseName;
					string text3 = "@_cnet_param_" + parm.BaseName;
					bool flag = parm.Direction == ParameterDirection.Input || serverProvidingOutputParameters;
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", new object[2]
					{
						text,
						flag ? text2 : text3
					});
					text = ", ";
				}
			}
			if (returnParameter == null)
			{
				return $"CALL {spName}({stringBuilder.ToString()})";
			}
			return string.Format("SET @{0}{1}={2}({3})", "_cnet_param_", returnParameter.BaseName, spName, stringBuilder.ToString());
		}

		private string CreateOutputSelect(MySqlParameterCollection parms, bool preparing)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			foreach (MySqlParameter parm in parms)
			{
				if (parm.Direction != ParameterDirection.Input && ((parm.Direction != ParameterDirection.InputOutput && parm.Direction != ParameterDirection.Output) || !serverProvidingOutputParameters))
				{
					_ = "@" + parm.BaseName;
					string text2 = "@_cnet_param_" + parm.BaseName;
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", new object[2] { text, text2 });
					text = ", ";
				}
			}
			if (stringBuilder.Length == 0)
			{
				return string.Empty;
			}
			if (command.Connection.Settings.AllowBatch && !preparing)
			{
				return $";SELECT {stringBuilder.ToString()}";
			}
			outSelect = $"SELECT {stringBuilder.ToString()}";
			return string.Empty;
		}

		internal void ProcessOutputParameters(MySqlDataReader reader)
		{
			AdjustOutputTypes(reader);
			if ((reader.CommandBehavior & CommandBehavior.SchemaOnly) != CommandBehavior.Default)
			{
				return;
			}
			reader.Read();
			string text = "@_cnet_param_";
			for (int i = 0; i < reader.FieldCount; i++)
			{
				string text2 = reader.GetName(i);
				if (text2.StartsWith(text, StringComparison.OrdinalIgnoreCase))
				{
					text2 = text2.Remove(0, text.Length);
				}
				MySqlParameter parameterFlexible = command.Parameters.GetParameterFlexible(text2, throwOnNotFound: true);
				parameterFlexible.Value = reader.GetValue(i);
			}
		}

		private void AdjustOutputTypes(MySqlDataReader reader)
		{
			for (int i = 0; i < reader.FieldCount; i++)
			{
				string text = reader.GetName(i);
				if (text.IndexOf("_cnet_param_") != -1)
				{
					text = text.Remove(0, "_cnet_param_".Length + 1);
				}
				MySqlParameter parameterFlexible = command.Parameters.GetParameterFlexible(text, throwOnNotFound: true);
				IMySqlValue iMySqlValue = MySqlField.GetIMySqlValue(parameterFlexible.MySqlDbType);
				if (iMySqlValue is MySqlBit mySqlBit)
				{
					mySqlBit.ReadAsString = true;
					reader.ResultSet.SetValueObject(i, mySqlBit);
				}
				else
				{
					reader.ResultSet.SetValueObject(i, iMySqlValue);
				}
			}
		}

		public override void Close(MySqlDataReader reader)
		{
			base.Close(reader);
			if (string.IsNullOrEmpty(outSelect) || (reader.CommandBehavior & CommandBehavior.SchemaOnly) != CommandBehavior.Default)
			{
				return;
			}
			MySqlCommand mySqlCommand = new MySqlCommand(outSelect, command.Connection);
			using MySqlDataReader reader2 = mySqlCommand.ExecuteReader(reader.CommandBehavior);
			ProcessOutputParameters(reader2);
		}
	}
	internal class SystemPerformanceMonitor : PerformanceMonitor
	{
		private static PerformanceCounter procedureHardQueries;

		private static PerformanceCounter procedureSoftQueries;

		public SystemPerformanceMonitor(MySqlConnection connection)
			: base(connection)
		{
			string perfMonCategoryName = Resources.PerfMonCategoryName;
			if (connection.Settings.UsePerformanceMonitor && procedureHardQueries == null)
			{
				try
				{
					procedureHardQueries = new PerformanceCounter(perfMonCategoryName, "HardProcedureQueries", readOnly: false);
					procedureSoftQueries = new PerformanceCounter(perfMonCategoryName, "SoftProcedureQueries", readOnly: false);
				}
				catch (Exception ex)
				{
					MySqlTrace.LogError(connection.ServerThread, ex.Message);
				}
			}
		}

		public new void AddHardProcedureQuery()
		{
			if (base.Connection.Settings.UsePerformanceMonitor && procedureHardQueries != null)
			{
				procedureHardQueries.Increment();
			}
		}

		public new void AddSoftProcedureQuery()
		{
			if (base.Connection.Settings.UsePerformanceMonitor && procedureSoftQueries != null)
			{
				procedureSoftQueries.Increment();
			}
		}
	}
	internal class TableCache
	{
		private static BaseTableCache cache;

		static TableCache()
		{
			cache = new BaseTableCache(480);
		}

		public static void AddToCache(string commandText, ResultSet resultSet)
		{
			cache.AddToCache(commandText, resultSet);
		}

		public static ResultSet RetrieveFromCache(string commandText, int cacheAge)
		{
			return (ResultSet)cache.RetrieveFromCache(commandText, cacheAge);
		}

		public static void RemoveFromCache(string commandText)
		{
			cache.RemoveFromCache(commandText);
		}

		public static void DumpCache()
		{
			cache.Dump();
		}
	}
	public class BaseTableCache
	{
		private struct CacheEntry
		{
			public DateTime CacheTime;

			public object CacheElement;
		}

		protected int MaxCacheAge;

		private Dictionary<string, CacheEntry> cache = new Dictionary<string, CacheEntry>();

		public BaseTableCache(int maxCacheAge)
		{
			MaxCacheAge = maxCacheAge;
		}

		public virtual void AddToCache(string commandText, object resultSet)
		{
			CleanCache();
			CacheEntry value = new CacheEntry
			{
				CacheTime = DateTime.Now,
				CacheElement = resultSet
			};
			lock (cache)
			{
				if (!cache.ContainsKey(commandText))
				{
					cache.Add(commandText, value);
				}
			}
		}

		public virtual object RetrieveFromCache(string commandText, int cacheAge)
		{
			CleanCache();
			lock (cache)
			{
				if (!cache.ContainsKey(commandText))
				{
					return null;
				}
				CacheEntry cacheEntry = cache[commandText];
				if (DateTime.Now.Subtract(cacheEntry.CacheTime).TotalSeconds > (double)cacheAge)
				{
					return null;
				}
				return cacheEntry.CacheElement;
			}
		}

		public void RemoveFromCache(string commandText)
		{
			lock (cache)
			{
				if (cache.ContainsKey(commandText))
				{
					cache.Remove(commandText);
				}
			}
		}

		public virtual void Dump()
		{
			lock (cache)
			{
				cache.Clear();
			}
		}

		protected virtual void CleanCache()
		{
			DateTime now = DateTime.Now;
			List<string> list = new List<string>();
			lock (cache)
			{
				foreach (string key in cache.Keys)
				{
					if (now.Subtract(cache[key].CacheTime).TotalSeconds > (double)MaxCacheAge)
					{
						list.Add(key);
					}
				}
				foreach (string item in list)
				{
					cache.Remove(item);
				}
			}
		}
	}
	internal class TimedStream : Stream
	{
		private enum IOKind
		{
			Read,
			Write
		}

		private Stream baseStream;

		private int timeout;

		private int lastReadTimeout;

		private int lastWriteTimeout;

		private LowResolutionStopwatch stopwatch;

		private bool isClosed;

		internal bool IsClosed => isClosed;

		public override bool CanRead => baseStream.CanRead;

		public override bool CanSeek => baseStream.CanSeek;

		public override bool CanWrite => baseStream.CanWrite;

		public override long Length => baseStream.Length;

		public override long Position
		{
			get
			{
				return baseStream.Position;
			}
			set
			{
				baseStream.Position = value;
			}
		}

		public override bool CanTimeout => baseStream.CanTimeout;

		public override int ReadTimeout
		{
			get
			{
				return baseStream.ReadTimeout;
			}
			set
			{
				baseStream.ReadTimeout = value;
			}
		}

		public override int WriteTimeout
		{
			get
			{
				return baseStream.WriteTimeout;
			}
			set
			{
				baseStream.WriteTimeout = value;
			}
		}

		public TimedStream(Stream baseStream)
		{
			this.baseStream = baseStream;
			timeout = baseStream.ReadTimeout;
			isClosed = false;
			stopwatch = new LowResolutionStopwatch();
		}

		private bool ShouldResetStreamTimeout(int currentValue, int newValue)
		{
			if (newValue == -1 && currentValue != newValue)
			{
				return true;
			}
			if (newValue > currentValue)
			{
				return true;
			}
			if (currentValue >= newValue + 100)
			{
				return true;
			}
			return false;
		}

		private void StartTimer(IOKind op)
		{
			int num = ((timeout != -1) ? (timeout - (int)stopwatch.ElapsedMilliseconds) : (-1));
			if (op == IOKind.Read)
			{
				if (ShouldResetStreamTimeout(lastReadTimeout, num))
				{
					baseStream.ReadTimeout = num;
					lastReadTimeout = num;
				}
			}
			else if (ShouldResetStreamTimeout(lastWriteTimeout, num))
			{
				baseStream.WriteTimeout = num;
				lastWriteTimeout = num;
			}
			if (timeout != -1)
			{
				stopwatch.Start();
			}
		}

		private void StopTimer()
		{
			if (timeout != -1)
			{
				stopwatch.Stop();
				if (stopwatch.ElapsedMilliseconds > timeout)
				{
					ResetTimeout(-1);
					throw new TimeoutException("Timeout in IO operation");
				}
			}
		}

		public override void Flush()
		{
			try
			{
				StartTimer(IOKind.Write);
				baseStream.Flush();
				StopTimer();
			}
			catch (Exception e)
			{
				HandleException(e);
				throw;
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			try
			{
				StartTimer(IOKind.Read);
				int result = baseStream.Read(buffer, offset, count);
				StopTimer();
				return result;
			}
			catch (Exception e)
			{
				HandleException(e);
				throw;
			}
		}

		public override int ReadByte()
		{
			try
			{
				StartTimer(IOKind.Read);
				int result = baseStream.ReadByte();
				StopTimer();
				return result;
			}
			catch (Exception e)
			{
				HandleException(e);
				throw;
			}
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return baseStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			baseStream.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			try
			{
				StartTimer(IOKind.Write);
				baseStream.Write(buffer, offset, count);
				StopTimer();
			}
			catch (Exception e)
			{
				HandleException(e);
				throw;
			}
		}

		public override void Close()
		{
			if (!isClosed)
			{
				isClosed = true;
				baseStream.Close();
				baseStream.Dispose();
			}
		}

		public void ResetTimeout(int newTimeout)
		{
			if (newTimeout == -1 || newTimeout == 0)
			{
				timeout = -1;
			}
			else
			{
				timeout = newTimeout;
			}
			stopwatch.Reset();
		}

		private void HandleException(Exception e)
		{
			stopwatch.Stop();
			ResetTimeout(-1);
		}
	}
	internal class TracingDriver : Driver
	{
		private static long driverCounter;

		private long driverId;

		private ResultSet activeResult;

		private int rowSizeInBytes;

		public TracingDriver(MySqlConnectionStringBuilder settings)
			: base(settings)
		{
			driverId = Interlocked.Increment(ref driverCounter);
		}

		public override void Open()
		{
			base.Open();
			MySqlTrace.TraceEvent(TraceEventType.Information, MySqlTraceEventType.ConnectionOpened, Resources.TraceOpenConnection, driverId, base.Settings.ConnectionString, base.ThreadID);
		}

		public override void Close()
		{
			base.Close();
			MySqlTrace.TraceEvent(TraceEventType.Information, MySqlTraceEventType.ConnectionClosed, Resources.TraceCloseConnection, driverId);
		}

		public override void SendQuery(MySqlPacket p)
		{
			rowSizeInBytes = 0;
			string text = base.Encoding.GetString(p.Buffer, 5, p.Length - 5);
			string text2 = null;
			if (text.Length > 300)
			{
				QueryNormalizer queryNormalizer = new QueryNormalizer();
				text2 = queryNormalizer.Normalize(text);
				text = text.Substring(0, 300);
			}
			base.SendQuery(p);
			MySqlTrace.TraceEvent(TraceEventType.Information, MySqlTraceEventType.QueryOpened, Resources.TraceQueryOpened, driverId, base.ThreadID, text);
			if (text2 != null)
			{
				MySqlTrace.TraceEvent(TraceEventType.Information, MySqlTraceEventType.QueryNormalized, Resources.TraceQueryNormalized, driverId, base.ThreadID, text2);
			}
		}

		protected override int GetResult(int statementId, ref int affectedRows, ref long insertedId)
		{
			try
			{
				int result = base.GetResult(statementId, ref affectedRows, ref insertedId);
				MySqlTrace.TraceEvent(TraceEventType.Information, MySqlTraceEventType.ResultOpened, Resources.TraceResult, driverId, result, affectedRows, insertedId);
				return result;
			}
			catch (MySqlException ex)
			{
				MySqlTrace.TraceEvent(TraceEventType.Information, MySqlTraceEventType.Error, Resources.TraceOpenResultError, driverId, ex.Number, ex.Message);
				throw ex;
			}
		}

		public override ResultSet NextResult(int statementId, bool force)
		{
			if (activeResult != null)
			{
				if (base.Settings.UseUsageAdvisor)
				{
					ReportUsageAdvisorWarnings(statementId, activeResult);
				}
				MySqlTrace.TraceEvent(TraceEventType.Information, MySqlTraceEventType.ResultClosed, Resources.TraceResultClosed, driverId, activeResult.TotalRows, activeResult.SkippedRows, rowSizeInBytes);
				rowSizeInBytes = 0;
				activeResult = null;
			}
			activeResult = base.NextResult(statementId, force);
			return activeResult;
		}

		public override int PrepareStatement(string sql, ref MySqlField[] parameters)
		{
			int num = base.PrepareStatement(sql, ref parameters);
			MySqlTrace.TraceEvent(TraceEventType.Information, MySqlTraceEventType.StatementPrepared, Resources.TraceStatementPrepared, driverId, sql, num);
			return num;
		}

		public override void CloseStatement(int id)
		{
			base.CloseStatement(id);
			MySqlTrace.TraceEvent(TraceEventType.Information, MySqlTraceEventType.StatementClosed, Resources.TraceStatementClosed, driverId, id);
		}

		public override void SetDatabase(string dbName)
		{
			base.SetDatabase(dbName);
			MySqlTrace.TraceEvent(TraceEventType.Information, MySqlTraceEventType.NonQuery, Resources.TraceSetDatabase, driverId, dbName);
		}

		public override void ExecuteStatement(MySqlPacket packetToExecute)
		{
			base.ExecuteStatement(packetToExecute);
			int position = packetToExecute.Position;
			packetToExecute.Position = 1;
			int num = packetToExecute.ReadInteger(4);
			packetToExecute.Position = position;
			MySqlTrace.TraceEvent(TraceEventType.Information, MySqlTraceEventType.StatementExecuted, Resources.TraceStatementExecuted, driverId, num, base.ThreadID);
		}

		public override bool FetchDataRow(int statementId, int columns)
		{
			try
			{
				bool flag = base.FetchDataRow(statementId, columns);
				if (flag)
				{
					rowSizeInBytes += (handler as NativeDriver).Packet.Length;
				}
				return flag;
			}
			catch (MySqlException ex)
			{
				MySqlTrace.TraceEvent(TraceEventType.Error, MySqlTraceEventType.Error, Resources.TraceFetchError, driverId, ex.Number, ex.Message);
				throw ex;
			}
		}

		public override void CloseQuery(MySqlConnection connection, int statementId)
		{
			base.CloseQuery(connection, statementId);
			MySqlTrace.TraceEvent(TraceEventType.Information, MySqlTraceEventType.QueryClosed, Resources.TraceQueryDone, driverId);
		}

		public override List<MySqlError> ReportWarnings(MySqlConnection connection)
		{
			List<MySqlError> list = base.ReportWarnings(connection);
			foreach (MySqlError item in list)
			{
				MySqlTrace.TraceEvent(TraceEventType.Warning, MySqlTraceEventType.Warning, Resources.TraceWarning, driverId, item.Level, item.Code, item.Message);
			}
			return list;
		}

		private bool AllFieldsAccessed(ResultSet rs)
		{
			if (rs.Fields == null || rs.Fields.Length == 0)
			{
				return true;
			}
			for (int i = 0; i < rs.Fields.Length; i++)
			{
				if (!rs.FieldRead(i))
				{
					return false;
				}
			}
			return true;
		}

		private void ReportUsageAdvisorWarnings(int statementId, ResultSet rs)
		{
			if (!base.Settings.UseUsageAdvisor)
			{
				return;
			}
			if (HasStatus(ServerStatusFlags.NoIndex))
			{
				MySqlTrace.TraceEvent(TraceEventType.Warning, MySqlTraceEventType.UsageAdvisorWarning, Resources.TraceUAWarningNoIndex, driverId, UsageAdvisorWarningFlags.NoIndex);
			}
			else if (HasStatus(ServerStatusFlags.BadIndex))
			{
				MySqlTrace.TraceEvent(TraceEventType.Warning, MySqlTraceEventType.UsageAdvisorWarning, Resources.TraceUAWarningBadIndex, driverId, UsageAdvisorWarningFlags.BadIndex);
			}
			if (rs.SkippedRows > 0)
			{
				MySqlTrace.TraceEvent(TraceEventType.Warning, MySqlTraceEventType.UsageAdvisorWarning, Resources.TraceUAWarningSkippedRows, driverId, UsageAdvisorWarningFlags.SkippedRows, rs.SkippedRows);
			}
			if (!AllFieldsAccessed(rs))
			{
				StringBuilder stringBuilder = new StringBuilder("");
				string arg = "";
				for (int i = 0; i < rs.Size; i++)
				{
					if (!rs.FieldRead(i))
					{
						stringBuilder.AppendFormat("{0}{1}", arg, rs.Fields[i].ColumnName);
						arg = ",";
					}
				}
				MySqlTrace.TraceEvent(TraceEventType.Warning, MySqlTraceEventType.UsageAdvisorWarning, Resources.TraceUAWarningSkippedColumns, driverId, UsageAdvisorWarningFlags.SkippedColumns, stringBuilder.ToString());
			}
			if (rs.Fields == null)
			{
				return;
			}
			MySqlField[] fields = rs.Fields;
			foreach (MySqlField mySqlField in fields)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				string arg2 = "";
				foreach (Type typeConversion in mySqlField.TypeConversions)
				{
					stringBuilder2.AppendFormat("{0}{1}", arg2, typeConversion.Name);
					arg2 = ",";
				}
				if (stringBuilder2.Length > 0)
				{
					MySqlTrace.TraceEvent(TraceEventType.Warning, MySqlTraceEventType.UsageAdvisorWarning, Resources.TraceUAWarningFieldConversion, driverId, UsageAdvisorWarningFlags.FieldConversion, mySqlField.ColumnName, stringBuilder2.ToString());
				}
			}
		}
	}
}
namespace MySql.Data.Types
{
	internal class MetaData
	{
		public static bool IsNumericType(string typename)
		{
			switch (typename.ToLower(CultureInfo.InvariantCulture))
			{
			case "int":
			case "integer":
			case "numeric":
			case "decimal":
			case "dec":
			case "fixed":
			case "tinyint":
			case "mediumint":
			case "bigint":
			case "real":
			case "double":
			case "float":
			case "serial":
			case "smallint":
				return true;
			default:
				return false;
			}
		}

		public static bool IsTextType(string typename)
		{
			switch (typename.ToLower(CultureInfo.InvariantCulture))
			{
			case "varchar":
			case "char":
			case "text":
			case "longtext":
			case "tinytext":
			case "mediumtext":
			case "nchar":
			case "nvarchar":
			case "enum":
			case "set":
				return true;
			default:
				return false;
			}
		}

		public static bool SupportScale(string typename)
		{
			switch (StringUtility.ToLowerInvariant(typename))
			{
			case "numeric":
			case "decimal":
			case "dec":
			case "real":
				return true;
			default:
				return false;
			}
		}

		public static MySqlDbType NameToType(string typeName, bool unsigned, bool realAsFloat, MySqlConnection connection)
		{
			switch (StringUtility.ToUpperInvariant(typeName))
			{
			case "CHAR":
				return MySqlDbType.String;
			case "VARCHAR":
				return MySqlDbType.VarChar;
			case "DATE":
				return MySqlDbType.Date;
			case "DATETIME":
				return MySqlDbType.DateTime;
			case "NUMERIC":
			case "DECIMAL":
			case "DEC":
			case "FIXED":
				if (connection.driver.Version.isAtLeast(5, 0, 3))
				{
					return MySqlDbType.NewDecimal;
				}
				return MySqlDbType.Decimal;
			case "YEAR":
				return MySqlDbType.Year;
			case "TIME":
				return MySqlDbType.Time;
			case "TIMESTAMP":
				return MySqlDbType.Timestamp;
			case "SET":
				return MySqlDbType.Set;
			case "ENUM":
				return MySqlDbType.Enum;
			case "BIT":
				return MySqlDbType.Bit;
			case "TINYINT":
				if (!unsigned)
				{
					return MySqlDbType.Byte;
				}
				return MySqlDbType.UByte;
			case "BOOL":
			case "BOOLEAN":
				return MySqlDbType.Byte;
			case "SMALLINT":
				if (!unsigned)
				{
					return MySqlDbType.Int16;
				}
				return MySqlDbType.UInt16;
			case "MEDIUMINT":
				if (!unsigned)
				{
					return MySqlDbType.Int24;
				}
				return MySqlDbType.UInt24;
			case "INT":
			case "INTEGER":
				if (!unsigned)
				{
					return MySqlDbType.Int32;
				}
				return MySqlDbType.UInt32;
			case "SERIAL":
				return MySqlDbType.UInt64;
			case "BIGINT":
				if (!unsigned)
				{
					return MySqlDbType.Int64;
				}
				return MySqlDbType.UInt64;
			case "FLOAT":
				return MySqlDbType.Float;
			case "DOUBLE":
				return MySqlDbType.Double;
			case "REAL":
				if (!realAsFloat)
				{
					return MySqlDbType.Double;
				}
				return MySqlDbType.Float;
			case "TEXT":
				return MySqlDbType.Text;
			case "BLOB":
				return MySqlDbType.Blob;
			case "LONGBLOB":
				return MySqlDbType.LongBlob;
			case "LONGTEXT":
				return MySqlDbType.LongText;
			case "MEDIUMBLOB":
				return MySqlDbType.MediumBlob;
			case "MEDIUMTEXT":
				return MySqlDbType.MediumText;
			case "TINYBLOB":
				return MySqlDbType.TinyBlob;
			case "TINYTEXT":
				return MySqlDbType.TinyText;
			case "BINARY":
				return MySqlDbType.Binary;
			case "VARBINARY":
				return MySqlDbType.VarBinary;
			default:
				throw new MySqlException("Unhandled type encountered");
			}
		}
	}
	internal struct MySqlBinary : IMySqlValue
	{
		private MySqlDbType type;

		private byte[] mValue;

		private bool isNull;

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => type;

		object IMySqlValue.Value => mValue;

		public byte[] Value => mValue;

		Type IMySqlValue.SystemType => typeof(byte[]);

		string IMySqlValue.MySqlTypeName => type switch
		{
			MySqlDbType.TinyBlob => "TINY_BLOB", 
			MySqlDbType.MediumBlob => "MEDIUM_BLOB", 
			MySqlDbType.LongBlob => "LONG_BLOB", 
			_ => "BLOB", 
		};

		public MySqlBinary(MySqlDbType type, bool isNull)
		{
			this.type = type;
			this.isNull = isNull;
			mValue = null;
		}

		public MySqlBinary(MySqlDbType type, byte[] val)
		{
			this.type = type;
			isNull = false;
			mValue = val;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			byte[] array = val as byte[];
			if (array == null)
			{
				if (val is char[] chars)
				{
					array = packet.Encoding.GetBytes(chars);
				}
				else
				{
					string text = val.ToString();
					if (length == 0)
					{
						length = text.Length;
					}
					else
					{
						text = text.Substring(0, length);
					}
					array = packet.Encoding.GetBytes(text);
				}
			}
			if (length == 0)
			{
				length = array.Length;
			}
			if (array == null)
			{
				throw new MySqlException("Only byte arrays and strings can be serialized by MySqlBinary");
			}
			if (binary)
			{
				packet.WriteLength(length);
				packet.Write(array, 0, length);
				return;
			}
			packet.WriteStringNoNull("_binary ");
			packet.WriteByte(39);
			EscapeByteArray(array, length, packet);
			packet.WriteByte(39);
		}

		private static void EscapeByteArray(byte[] bytes, int length, MySqlPacket packet)
		{
			for (int i = 0; i < length; i++)
			{
				byte b = bytes[i];
				switch (b)
				{
				case 0:
					packet.WriteByte(92);
					packet.WriteByte(48);
					break;
				case 34:
				case 39:
				case 92:
					packet.WriteByte(92);
					packet.WriteByte(b);
					break;
				default:
					packet.WriteByte(b);
					break;
				}
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			MySqlBinary mySqlBinary;
			if (nullVal)
			{
				mySqlBinary = new MySqlBinary(type, isNull: true);
			}
			else
			{
				if (length == -1)
				{
					length = packet.ReadFieldLength();
				}
				byte[] array = new byte[length];
				packet.Read(array, 0, (int)length);
				mySqlBinary = new MySqlBinary(type, array);
			}
			return mySqlBinary;
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			int num = (int)packet.ReadFieldLength();
			packet.Position += num;
		}

		public static void SetDSInfo(MySqlSchemaCollection sc)
		{
			string[] array = new string[6] { "BLOB", "TINYBLOB", "MEDIUMBLOB", "LONGBLOB", "BINARY", "VARBINARY" };
			MySqlDbType[] array2 = new MySqlDbType[6]
			{
				MySqlDbType.Blob,
				MySqlDbType.TinyBlob,
				MySqlDbType.MediumBlob,
				MySqlDbType.LongBlob,
				MySqlDbType.Binary,
				MySqlDbType.VarBinary
			};
			long[] array3 = new long[6] { 65535L, 255L, 16777215L, 4294967295L, 255L, 65535L };
			string[] array4 = new string[6] { null, null, null, null, "binary({0})", "varbinary({0})" };
			string[] array5 = new string[6] { null, null, null, null, "length", "length" };
			for (int i = 0; i < array.Length; i++)
			{
				MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
				mySqlSchemaRow["TypeName"] = array[i];
				mySqlSchemaRow["ProviderDbType"] = array2[i];
				mySqlSchemaRow["ColumnSize"] = array3[i];
				mySqlSchemaRow["CreateFormat"] = array4[i];
				mySqlSchemaRow["CreateParameters"] = array5[i];
				mySqlSchemaRow["DataType"] = "System.Byte[]";
				mySqlSchemaRow["IsAutoincrementable"] = false;
				mySqlSchemaRow["IsBestMatch"] = true;
				mySqlSchemaRow["IsCaseSensitive"] = false;
				mySqlSchemaRow["IsFixedLength"] = i >= 4;
				mySqlSchemaRow["IsFixedPrecisionScale"] = false;
				mySqlSchemaRow["IsLong"] = array3[i] > 255;
				mySqlSchemaRow["IsNullable"] = true;
				mySqlSchemaRow["IsSearchable"] = false;
				mySqlSchemaRow["IsSearchableWithLike"] = false;
				mySqlSchemaRow["IsUnsigned"] = DBNull.Value;
				mySqlSchemaRow["MaximumScale"] = DBNull.Value;
				mySqlSchemaRow["MinimumScale"] = DBNull.Value;
				mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
				mySqlSchemaRow["IsLiteralSupported"] = false;
				mySqlSchemaRow["LiteralPrefix"] = "0x";
				mySqlSchemaRow["LiteralSuffix"] = DBNull.Value;
				mySqlSchemaRow["NativeDataType"] = DBNull.Value;
			}
		}
	}
	internal struct MySqlBit : IMySqlValue
	{
		private ulong mValue;

		private bool isNull;

		private bool readAsString;

		public bool ReadAsString
		{
			get
			{
				return readAsString;
			}
			set
			{
				readAsString = value;
			}
		}

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => MySqlDbType.Bit;

		object IMySqlValue.Value => mValue;

		Type IMySqlValue.SystemType => typeof(ulong);

		string IMySqlValue.MySqlTypeName => "BIT";

		public MySqlBit(bool isnull)
		{
			mValue = 0uL;
			isNull = isnull;
			readAsString = false;
		}

		public void WriteValue(MySqlPacket packet, bool binary, object value, int length)
		{
			ulong v = ((value is ulong) ? ((ulong)value) : Convert.ToUInt64(value));
			if (binary)
			{
				packet.WriteInteger((long)v, 8);
			}
			else
			{
				packet.WriteStringNoNull(v.ToString());
			}
		}

		public IMySqlValue ReadValue(MySqlPacket packet, long length, bool isNull)
		{
			this.isNull = isNull;
			if (isNull)
			{
				return this;
			}
			if (length == -1)
			{
				length = packet.ReadFieldLength();
			}
			if (ReadAsString)
			{
				mValue = ulong.Parse(packet.ReadString(length));
			}
			else
			{
				mValue = packet.ReadBitValue((int)length);
			}
			return this;
		}

		public void SkipValue(MySqlPacket packet)
		{
			int num = (int)packet.ReadFieldLength();
			packet.Position += num;
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
			mySqlSchemaRow["TypeName"] = "BIT";
			mySqlSchemaRow["ProviderDbType"] = MySqlDbType.Bit;
			mySqlSchemaRow["ColumnSize"] = 64;
			mySqlSchemaRow["CreateFormat"] = "BIT";
			mySqlSchemaRow["CreateParameters"] = DBNull.Value;
			mySqlSchemaRow["DataType"] = typeof(ulong).ToString();
			mySqlSchemaRow["IsAutoincrementable"] = false;
			mySqlSchemaRow["IsBestMatch"] = true;
			mySqlSchemaRow["IsCaseSensitive"] = false;
			mySqlSchemaRow["IsFixedLength"] = false;
			mySqlSchemaRow["IsFixedPrecisionScale"] = true;
			mySqlSchemaRow["IsLong"] = false;
			mySqlSchemaRow["IsNullable"] = true;
			mySqlSchemaRow["IsSearchable"] = true;
			mySqlSchemaRow["IsSearchableWithLike"] = false;
			mySqlSchemaRow["IsUnsigned"] = false;
			mySqlSchemaRow["MaximumScale"] = 0;
			mySqlSchemaRow["MinimumScale"] = 0;
			mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
			mySqlSchemaRow["IsLiteralSupported"] = false;
			mySqlSchemaRow["LiteralPrefix"] = DBNull.Value;
			mySqlSchemaRow["LiteralSuffix"] = DBNull.Value;
			mySqlSchemaRow["NativeDataType"] = DBNull.Value;
		}
	}
	internal struct MySqlByte : IMySqlValue
	{
		private sbyte mValue;

		private bool isNull;

		private bool treatAsBool;

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => MySqlDbType.Byte;

		object IMySqlValue.Value
		{
			get
			{
				if (TreatAsBoolean)
				{
					return Convert.ToBoolean(mValue);
				}
				return mValue;
			}
		}

		public sbyte Value
		{
			get
			{
				return mValue;
			}
			set
			{
				mValue = value;
			}
		}

		Type IMySqlValue.SystemType
		{
			get
			{
				if (TreatAsBoolean)
				{
					return typeof(bool);
				}
				return typeof(sbyte);
			}
		}

		string IMySqlValue.MySqlTypeName => "TINYINT";

		internal bool TreatAsBoolean
		{
			get
			{
				return treatAsBool;
			}
			set
			{
				treatAsBool = value;
			}
		}

		public MySqlByte(bool isNull)
		{
			this.isNull = isNull;
			mValue = 0;
			treatAsBool = false;
		}

		public MySqlByte(sbyte val)
		{
			isNull = false;
			mValue = val;
			treatAsBool = false;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			sbyte b = ((val is sbyte) ? ((sbyte)val) : Convert.ToSByte(val));
			if (binary)
			{
				packet.WriteByte((byte)b);
			}
			else
			{
				packet.WriteStringNoNull(b.ToString());
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			if (nullVal)
			{
				return new MySqlByte(isNull: true);
			}
			if (length == -1)
			{
				return new MySqlByte((sbyte)packet.ReadByte());
			}
			string s = packet.ReadString(length);
			MySqlByte mySqlByte = new MySqlByte(sbyte.Parse(s, NumberStyles.Any, CultureInfo.InvariantCulture));
			mySqlByte.TreatAsBoolean = TreatAsBoolean;
			return mySqlByte;
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			packet.ReadByte();
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
			mySqlSchemaRow["TypeName"] = "TINYINT";
			mySqlSchemaRow["ProviderDbType"] = MySqlDbType.Byte;
			mySqlSchemaRow["ColumnSize"] = 0;
			mySqlSchemaRow["CreateFormat"] = "TINYINT";
			mySqlSchemaRow["CreateParameters"] = null;
			mySqlSchemaRow["DataType"] = "System.SByte";
			mySqlSchemaRow["IsAutoincrementable"] = true;
			mySqlSchemaRow["IsBestMatch"] = true;
			mySqlSchemaRow["IsCaseSensitive"] = false;
			mySqlSchemaRow["IsFixedLength"] = true;
			mySqlSchemaRow["IsFixedPrecisionScale"] = true;
			mySqlSchemaRow["IsLong"] = false;
			mySqlSchemaRow["IsNullable"] = true;
			mySqlSchemaRow["IsSearchable"] = true;
			mySqlSchemaRow["IsSearchableWithLike"] = false;
			mySqlSchemaRow["IsUnsigned"] = false;
			mySqlSchemaRow["MaximumScale"] = 0;
			mySqlSchemaRow["MinimumScale"] = 0;
			mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
			mySqlSchemaRow["IsLiteralSupported"] = false;
			mySqlSchemaRow["LiteralPrefix"] = null;
			mySqlSchemaRow["LiteralSuffix"] = null;
			mySqlSchemaRow["NativeDataType"] = null;
		}
	}
	[Serializable]
	public class MySqlConversionException : Exception
	{
		public MySqlConversionException(string msg)
			: base(msg)
		{
		}
	}
	public struct MySqlDecimal : IMySqlValue
	{
		private byte precision;

		private byte scale;

		private string mValue;

		private bool isNull;

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => MySqlDbType.Decimal;

		public byte Precision
		{
			get
			{
				return precision;
			}
			set
			{
				precision = value;
			}
		}

		public byte Scale
		{
			get
			{
				return scale;
			}
			set
			{
				scale = value;
			}
		}

		object IMySqlValue.Value => Value;

		public decimal Value => Convert.ToDecimal(mValue, CultureInfo.InvariantCulture);

		Type IMySqlValue.SystemType => typeof(decimal);

		string IMySqlValue.MySqlTypeName => "DECIMAL";

		internal MySqlDecimal(bool isNull)
		{
			this.isNull = isNull;
			mValue = null;
			precision = (scale = 0);
		}

		internal MySqlDecimal(string val)
		{
			isNull = false;
			precision = (scale = 0);
			mValue = val;
		}

		public double ToDouble()
		{
			return double.Parse(mValue);
		}

		public override string ToString()
		{
			return mValue;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			string text = ((val is decimal) ? ((decimal)val) : Convert.ToDecimal(val)).ToString(CultureInfo.InvariantCulture);
			if (binary)
			{
				packet.WriteLenString(text);
			}
			else
			{
				packet.WriteStringNoNull(text);
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			if (nullVal)
			{
				return new MySqlDecimal(isNull: true);
			}
			string empty = string.Empty;
			empty = ((length != -1) ? packet.ReadString(length) : packet.ReadLenString());
			return new MySqlDecimal(empty);
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			int num = (int)packet.ReadFieldLength();
			packet.Position += num;
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
			mySqlSchemaRow["TypeName"] = "DECIMAL";
			mySqlSchemaRow["ProviderDbType"] = MySqlDbType.NewDecimal;
			mySqlSchemaRow["ColumnSize"] = 0;
			mySqlSchemaRow["CreateFormat"] = "DECIMAL({0},{1})";
			mySqlSchemaRow["CreateParameters"] = "precision,scale";
			mySqlSchemaRow["DataType"] = "System.Decimal";
			mySqlSchemaRow["IsAutoincrementable"] = false;
			mySqlSchemaRow["IsBestMatch"] = true;
			mySqlSchemaRow["IsCaseSensitive"] = false;
			mySqlSchemaRow["IsFixedLength"] = true;
			mySqlSchemaRow["IsFixedPrecisionScale"] = true;
			mySqlSchemaRow["IsLong"] = false;
			mySqlSchemaRow["IsNullable"] = true;
			mySqlSchemaRow["IsSearchable"] = true;
			mySqlSchemaRow["IsSearchableWithLike"] = false;
			mySqlSchemaRow["IsUnsigned"] = false;
			mySqlSchemaRow["MaximumScale"] = 0;
			mySqlSchemaRow["MinimumScale"] = 0;
			mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
			mySqlSchemaRow["IsLiteralSupported"] = false;
			mySqlSchemaRow["LiteralPrefix"] = null;
			mySqlSchemaRow["LiteralSuffix"] = null;
			mySqlSchemaRow["NativeDataType"] = null;
		}
	}
	internal struct MySqlDouble : IMySqlValue
	{
		private double mValue;

		private bool isNull;

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => MySqlDbType.Double;

		object IMySqlValue.Value => mValue;

		public double Value => mValue;

		Type IMySqlValue.SystemType => typeof(double);

		string IMySqlValue.MySqlTypeName => "DOUBLE";

		public MySqlDouble(bool isNull)
		{
			this.isNull = isNull;
			mValue = 0.0;
		}

		public MySqlDouble(double val)
		{
			isNull = false;
			mValue = val;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			double value = ((val is double) ? ((double)val) : Convert.ToDouble(val));
			if (binary)
			{
				packet.Write(BitConverter.GetBytes(value));
			}
			else
			{
				packet.WriteStringNoNull(value.ToString("R", CultureInfo.InvariantCulture));
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			if (nullVal)
			{
				return new MySqlDouble(isNull: true);
			}
			if (length == -1)
			{
				byte[] array = new byte[8];
				packet.Read(array, 0, 8);
				return new MySqlDouble(BitConverter.ToDouble(array, 0));
			}
			string text = packet.ReadString(length);
			double val;
			try
			{
				val = double.Parse(text, CultureInfo.InvariantCulture);
			}
			catch (OverflowException)
			{
				val = ((!text.StartsWith("-", StringComparison.Ordinal)) ? double.MaxValue : double.MinValue);
			}
			return new MySqlDouble(val);
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			packet.Position += 8;
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
			mySqlSchemaRow["TypeName"] = "DOUBLE";
			mySqlSchemaRow["ProviderDbType"] = MySqlDbType.Double;
			mySqlSchemaRow["ColumnSize"] = 0;
			mySqlSchemaRow["CreateFormat"] = "DOUBLE";
			mySqlSchemaRow["CreateParameters"] = null;
			mySqlSchemaRow["DataType"] = "System.Double";
			mySqlSchemaRow["IsAutoincrementable"] = false;
			mySqlSchemaRow["IsBestMatch"] = true;
			mySqlSchemaRow["IsCaseSensitive"] = false;
			mySqlSchemaRow["IsFixedLength"] = true;
			mySqlSchemaRow["IsFixedPrecisionScale"] = true;
			mySqlSchemaRow["IsLong"] = false;
			mySqlSchemaRow["IsNullable"] = true;
			mySqlSchemaRow["IsSearchable"] = true;
			mySqlSchemaRow["IsSearchableWithLike"] = false;
			mySqlSchemaRow["IsUnsigned"] = false;
			mySqlSchemaRow["MaximumScale"] = 0;
			mySqlSchemaRow["MinimumScale"] = 0;
			mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
			mySqlSchemaRow["IsLiteralSupported"] = false;
			mySqlSchemaRow["LiteralPrefix"] = null;
			mySqlSchemaRow["LiteralSuffix"] = null;
			mySqlSchemaRow["NativeDataType"] = null;
		}
	}
	public struct MySqlGeometry : IMySqlValue
	{
		private const int GEOMETRY_LENGTH = 25;

		private MySqlDbType _type;

		private double _xValue;

		private double _yValue;

		private int _srid;

		private byte[] _valBinary;

		private bool _isNull;

		public double? XCoordinate => _xValue;

		public double? YCoordinate => _yValue;

		public int? SRID => _srid;

		MySqlDbType IMySqlValue.MySqlDbType => _type;

		public bool IsNull => _isNull;

		object IMySqlValue.Value => _valBinary;

		public byte[] Value => _valBinary;

		Type IMySqlValue.SystemType => typeof(byte[]);

		string IMySqlValue.MySqlTypeName => "GEOMETRY";

		public MySqlGeometry(bool isNull)
			: this(MySqlDbType.Geometry, isNull)
		{
		}

		public MySqlGeometry(double xValue, double yValue)
			: this(MySqlDbType.Geometry, xValue, yValue, 0)
		{
		}

		public MySqlGeometry(double xValue, double yValue, int srid)
			: this(MySqlDbType.Geometry, xValue, yValue, srid)
		{
		}

		internal MySqlGeometry(MySqlDbType type, bool isNull)
		{
			_type = type;
			isNull = true;
			_xValue = 0.0;
			_yValue = 0.0;
			_srid = 0;
			_valBinary = null;
			_isNull = isNull;
		}

		internal MySqlGeometry(MySqlDbType type, double xValue, double yValue, int srid)
		{
			_type = type;
			_xValue = xValue;
			_yValue = yValue;
			_isNull = false;
			_srid = srid;
			_valBinary = new byte[25];
			byte[] bytes = BitConverter.GetBytes(srid);
			for (int i = 0; i < bytes.Length; i++)
			{
				_valBinary[i] = bytes[i];
			}
			long num = BitConverter.DoubleToInt64Bits(xValue);
			long num2 = BitConverter.DoubleToInt64Bits(yValue);
			_valBinary[4] = 1;
			_valBinary[5] = 1;
			for (int j = 0; j < 8; j++)
			{
				_valBinary[j + 9] = (byte)(num & 0xFF);
				num >>= 8;
			}
			for (int k = 0; k < 8; k++)
			{
				_valBinary[k + 17] = (byte)(num2 & 0xFF);
				num2 >>= 8;
			}
		}

		public MySqlGeometry(MySqlDbType type, byte[] val)
		{
			if (val == null)
			{
				throw new ArgumentNullException("val");
			}
			byte[] array = new byte[val.Length];
			for (int i = 0; i < val.Length; i++)
			{
				array[i] = val[i];
			}
			int startIndex = ((val.Length == 25) ? 9 : 5);
			int startIndex2 = ((val.Length == 25) ? 17 : 13);
			_valBinary = array;
			_xValue = BitConverter.ToDouble(val, startIndex);
			_yValue = BitConverter.ToDouble(val, startIndex2);
			_srid = ((val.Length == 25) ? BitConverter.ToInt32(val, 0) : 0);
			_isNull = false;
			_type = type;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			byte[] array = null;
			try
			{
				array = ((MySqlGeometry)val)._valBinary;
			}
			catch
			{
				array = val as byte[];
			}
			if (array == null)
			{
				MySqlGeometry mySqlGeometryValue = new MySqlGeometry(0.0, 0.0);
				TryParse(val.ToString(), out mySqlGeometryValue);
				array = mySqlGeometryValue._valBinary;
			}
			byte[] array2 = new byte[25];
			for (int i = 0; i < array.Length; i++)
			{
				if (array.Length < 25)
				{
					array2[i + 4] = array[i];
				}
				else
				{
					array2[i] = array[i];
				}
			}
			packet.WriteStringNoNull("_binary ");
			packet.WriteByte(39);
			EscapeByteArray(array2, 25, packet);
			packet.WriteByte(39);
		}

		private static void EscapeByteArray(byte[] bytes, int length, MySqlPacket packet)
		{
			for (int i = 0; i < length; i++)
			{
				byte b = bytes[i];
				switch (b)
				{
				case 0:
					packet.WriteByte(92);
					packet.WriteByte(48);
					break;
				case 34:
				case 39:
				case 92:
					packet.WriteByte(92);
					packet.WriteByte(b);
					break;
				default:
					packet.WriteByte(b);
					break;
				}
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			MySqlGeometry mySqlGeometry;
			if (nullVal)
			{
				mySqlGeometry = new MySqlGeometry(_type, isNull: true);
			}
			else
			{
				if (length == -1)
				{
					length = packet.ReadFieldLength();
				}
				byte[] array = new byte[length];
				packet.Read(array, 0, (int)length);
				mySqlGeometry = new MySqlGeometry(_type, array);
			}
			return mySqlGeometry;
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			int num = (int)packet.ReadFieldLength();
			packet.Position += num;
		}

		public override string ToString()
		{
			if (!_isNull)
			{
				if (_srid == 0)
				{
					return string.Format(CultureInfo.InvariantCulture.NumberFormat, "POINT({0} {1})", new object[2] { _xValue, _yValue });
				}
				return string.Format(CultureInfo.InvariantCulture.NumberFormat, "SRID={2};POINT({0} {1})", new object[3] { _xValue, _yValue, _srid });
			}
			return string.Empty;
		}

		public static MySqlGeometry Parse(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException("value");
			}
			if (!value.Contains("SRID") && !value.Contains("POINT(") && !value.Contains("POINT ("))
			{
				throw new FormatException("String does not contain a valid geometry value");
			}
			MySqlGeometry mySqlGeometryValue = new MySqlGeometry(0.0, 0.0);
			TryParse(value, out mySqlGeometryValue);
			return mySqlGeometryValue;
		}

		public static bool TryParse(string value, out MySqlGeometry mySqlGeometryValue)
		{
			string[] array = new string[0];
			string text = string.Empty;
			bool flag = false;
			bool flag2 = false;
			double result = 0.0;
			double result2 = 0.0;
			int result3 = 0;
			try
			{
				if (value.Contains(";"))
				{
					array = value.Split(new char[1] { ';' });
				}
				else
				{
					text = value;
				}
				if (array.Length > 1 || text != string.Empty)
				{
					string text2 = ((text != string.Empty) ? text : array[1]);
					text2 = text2.Replace("POINT (", "").Replace("POINT(", "").Replace(")", "");
					string[] array2 = text2.Split(new char[1] { ' ' });
					if (array2.Length > 1)
					{
						flag = double.TryParse(array2[0], out result);
						flag2 = double.TryParse(array2[1], out result2);
					}
					if (array.Length >= 1)
					{
						int.TryParse(array[0].Replace("SRID=", ""), out result3);
					}
				}
				if (flag && flag2)
				{
					mySqlGeometryValue = new MySqlGeometry(result, result2, result3);
					return true;
				}
			}
			catch
			{
			}
			mySqlGeometryValue = new MySqlGeometry(isNull: true);
			return false;
		}

		public static void SetDSInfo(MySqlSchemaCollection dsTable)
		{
			MySqlSchemaRow mySqlSchemaRow = dsTable.AddRow();
			mySqlSchemaRow["TypeName"] = "GEOMETRY";
			mySqlSchemaRow["ProviderDbType"] = MySqlDbType.Geometry;
			mySqlSchemaRow["ColumnSize"] = 25;
			mySqlSchemaRow["CreateFormat"] = "GEOMETRY";
			mySqlSchemaRow["CreateParameters"] = DBNull.Value;
			mySqlSchemaRow["DataType"] = "System.Byte[]";
			mySqlSchemaRow["IsAutoincrementable"] = false;
			mySqlSchemaRow["IsBestMatch"] = true;
			mySqlSchemaRow["IsCaseSensitive"] = false;
			mySqlSchemaRow["IsFixedLength"] = false;
			mySqlSchemaRow["IsFixedPrecisionScale"] = true;
			mySqlSchemaRow["IsLong"] = false;
			mySqlSchemaRow["IsNullable"] = true;
			mySqlSchemaRow["IsSearchable"] = true;
			mySqlSchemaRow["IsSearchableWithLike"] = false;
			mySqlSchemaRow["IsUnsigned"] = false;
			mySqlSchemaRow["MaximumScale"] = 0;
			mySqlSchemaRow["MinimumScale"] = 0;
			mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
			mySqlSchemaRow["IsLiteralSupported"] = false;
			mySqlSchemaRow["LiteralPrefix"] = DBNull.Value;
			mySqlSchemaRow["LiteralSuffix"] = DBNull.Value;
			mySqlSchemaRow["NativeDataType"] = DBNull.Value;
		}

		public string GetWKT()
		{
			if (!_isNull)
			{
				return string.Format(CultureInfo.InvariantCulture.NumberFormat, "POINT({0} {1})", new object[2] { _xValue, _yValue });
			}
			return string.Empty;
		}
	}
	internal struct MySqlGuid : IMySqlValue
	{
		private Guid mValue;

		private bool isNull;

		private byte[] bytes;

		private bool oldGuids;

		public byte[] Bytes => bytes;

		public bool OldGuids
		{
			get
			{
				return oldGuids;
			}
			set
			{
				oldGuids = value;
			}
		}

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => MySqlDbType.Guid;

		object IMySqlValue.Value => mValue;

		public Guid Value => mValue;

		Type IMySqlValue.SystemType => typeof(Guid);

		string IMySqlValue.MySqlTypeName
		{
			get
			{
				if (!OldGuids)
				{
					return "CHAR(36)";
				}
				return "BINARY(16)";
			}
		}

		public MySqlGuid(byte[] buff)
		{
			oldGuids = false;
			mValue = new Guid(buff);
			isNull = false;
			bytes = buff;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			Guid guid = Guid.Empty;
			string text = val as string;
			byte[] array = val as byte[];
			if (val is Guid)
			{
				guid = (Guid)val;
			}
			else
			{
				try
				{
					if (text != null)
					{
						guid = new Guid(text);
					}
					else if (array != null)
					{
						guid = new Guid(array);
					}
				}
				catch (Exception ex)
				{
					throw new MySqlException(Resources.DataNotInSupportedFormat, ex);
				}
			}
			if (OldGuids)
			{
				WriteOldGuid(packet, guid, binary);
				return;
			}
			guid.ToString("D");
			if (binary)
			{
				packet.WriteLenString(guid.ToString("D"));
			}
			else
			{
				packet.WriteStringNoNull("'" + MySqlHelper.EscapeString(guid.ToString("D")) + "'");
			}
		}

		private void WriteOldGuid(MySqlPacket packet, Guid guid, bool binary)
		{
			byte[] array = guid.ToByteArray();
			if (binary)
			{
				packet.WriteLength(array.Length);
				packet.Write(array);
				return;
			}
			packet.WriteStringNoNull("_binary ");
			packet.WriteByte(39);
			EscapeByteArray(array, array.Length, packet);
			packet.WriteByte(39);
		}

		private static void EscapeByteArray(byte[] bytes, int length, MySqlPacket packet)
		{
			for (int i = 0; i < length; i++)
			{
				byte b = bytes[i];
				switch (b)
				{
				case 0:
					packet.WriteByte(92);
					packet.WriteByte(48);
					break;
				case 34:
				case 39:
				case 92:
					packet.WriteByte(92);
					packet.WriteByte(b);
					break;
				default:
					packet.WriteByte(b);
					break;
				}
			}
		}

		private MySqlGuid ReadOldGuid(MySqlPacket packet, long length)
		{
			if (length == -1)
			{
				length = packet.ReadFieldLength();
			}
			byte[] array = new byte[length];
			packet.Read(array, 0, (int)length);
			MySqlGuid result = new MySqlGuid(array);
			result.OldGuids = OldGuids;
			return result;
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			MySqlGuid mySqlGuid = new MySqlGuid
			{
				isNull = true,
				OldGuids = OldGuids
			};
			if (!nullVal)
			{
				if (OldGuids)
				{
					return ReadOldGuid(packet, length);
				}
				string empty = string.Empty;
				empty = ((length != -1) ? packet.ReadString(length) : packet.ReadLenString());
				mySqlGuid.mValue = new Guid(empty);
				mySqlGuid.isNull = false;
			}
			return mySqlGuid;
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			int num = (int)packet.ReadFieldLength();
			packet.Position += num;
		}

		public static void SetDSInfo(MySqlSchemaCollection sc)
		{
			MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
			mySqlSchemaRow["TypeName"] = "GUID";
			mySqlSchemaRow["ProviderDbType"] = MySqlDbType.Guid;
			mySqlSchemaRow["ColumnSize"] = 0;
			mySqlSchemaRow["CreateFormat"] = "BINARY(16)";
			mySqlSchemaRow["CreateParameters"] = null;
			mySqlSchemaRow["DataType"] = "System.Guid";
			mySqlSchemaRow["IsAutoincrementable"] = false;
			mySqlSchemaRow["IsBestMatch"] = true;
			mySqlSchemaRow["IsCaseSensitive"] = false;
			mySqlSchemaRow["IsFixedLength"] = true;
			mySqlSchemaRow["IsFixedPrecisionScale"] = true;
			mySqlSchemaRow["IsLong"] = false;
			mySqlSchemaRow["IsNullable"] = true;
			mySqlSchemaRow["IsSearchable"] = false;
			mySqlSchemaRow["IsSearchableWithLike"] = false;
			mySqlSchemaRow["IsUnsigned"] = false;
			mySqlSchemaRow["MaximumScale"] = 0;
			mySqlSchemaRow["MinimumScale"] = 0;
			mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
			mySqlSchemaRow["IsLiteralSupported"] = false;
			mySqlSchemaRow["LiteralPrefix"] = null;
			mySqlSchemaRow["LiteralSuffix"] = null;
			mySqlSchemaRow["NativeDataType"] = null;
		}
	}
	internal struct MySqlInt16 : IMySqlValue
	{
		private short mValue;

		private bool isNull;

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => MySqlDbType.Int16;

		object IMySqlValue.Value => mValue;

		public short Value => mValue;

		Type IMySqlValue.SystemType => typeof(short);

		string IMySqlValue.MySqlTypeName => "SMALLINT";

		public MySqlInt16(bool isNull)
		{
			this.isNull = isNull;
			mValue = 0;
		}

		public MySqlInt16(short val)
		{
			isNull = false;
			mValue = val;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			int num = ((val is int) ? ((int)val) : Convert.ToInt32(val));
			if (binary)
			{
				packet.WriteInteger(num, 2);
			}
			else
			{
				packet.WriteStringNoNull(num.ToString());
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			if (nullVal)
			{
				return new MySqlInt16(isNull: true);
			}
			if (length == -1)
			{
				return new MySqlInt16((short)packet.ReadInteger(2));
			}
			return new MySqlInt16(short.Parse(packet.ReadString(length)));
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			packet.Position += 2;
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
			mySqlSchemaRow["TypeName"] = "SMALLINT";
			mySqlSchemaRow["ProviderDbType"] = MySqlDbType.Int16;
			mySqlSchemaRow["ColumnSize"] = 0;
			mySqlSchemaRow["CreateFormat"] = "SMALLINT";
			mySqlSchemaRow["CreateParameters"] = null;
			mySqlSchemaRow["DataType"] = "System.Int16";
			mySqlSchemaRow["IsAutoincrementable"] = true;
			mySqlSchemaRow["IsBestMatch"] = true;
			mySqlSchemaRow["IsCaseSensitive"] = false;
			mySqlSchemaRow["IsFixedLength"] = true;
			mySqlSchemaRow["IsFixedPrecisionScale"] = true;
			mySqlSchemaRow["IsLong"] = false;
			mySqlSchemaRow["IsNullable"] = true;
			mySqlSchemaRow["IsSearchable"] = true;
			mySqlSchemaRow["IsSearchableWithLike"] = false;
			mySqlSchemaRow["IsUnsigned"] = false;
			mySqlSchemaRow["MaximumScale"] = 0;
			mySqlSchemaRow["MinimumScale"] = 0;
			mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
			mySqlSchemaRow["IsLiteralSupported"] = false;
			mySqlSchemaRow["LiteralPrefix"] = null;
			mySqlSchemaRow["LiteralSuffix"] = null;
			mySqlSchemaRow["NativeDataType"] = null;
		}
	}
	internal struct MySqlInt32 : IMySqlValue
	{
		private int mValue;

		private bool isNull;

		private bool is24Bit;

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => MySqlDbType.Int32;

		object IMySqlValue.Value => mValue;

		public int Value => mValue;

		Type IMySqlValue.SystemType => typeof(int);

		string IMySqlValue.MySqlTypeName
		{
			get
			{
				if (!is24Bit)
				{
					return "INT";
				}
				return "MEDIUMINT";
			}
		}

		private MySqlInt32(MySqlDbType type)
		{
			is24Bit = type == MySqlDbType.Int24;
			isNull = true;
			mValue = 0;
		}

		public MySqlInt32(MySqlDbType type, bool isNull)
			: this(type)
		{
			this.isNull = isNull;
		}

		public MySqlInt32(MySqlDbType type, int val)
			: this(type)
		{
			isNull = false;
			mValue = val;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			int num = ((val is int) ? ((int)val) : Convert.ToInt32(val));
			if (binary)
			{
				packet.WriteInteger(num, is24Bit ? 3 : 4);
			}
			else
			{
				packet.WriteStringNoNull(num.ToString());
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			if (nullVal)
			{
				return new MySqlInt32(((IMySqlValue)this).MySqlDbType, isNull: true);
			}
			if (length == -1)
			{
				return new MySqlInt32(((IMySqlValue)this).MySqlDbType, packet.ReadInteger(4));
			}
			return new MySqlInt32(((IMySqlValue)this).MySqlDbType, int.Parse(packet.ReadString(length), CultureInfo.InvariantCulture));
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			packet.Position += 4;
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			string[] array = new string[3] { "INT", "YEAR", "MEDIUMINT" };
			MySqlDbType[] array2 = new MySqlDbType[3]
			{
				MySqlDbType.Int32,
				MySqlDbType.Year,
				MySqlDbType.Int24
			};
			for (int i = 0; i < array.Length; i++)
			{
				MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
				mySqlSchemaRow["TypeName"] = array[i];
				mySqlSchemaRow["ProviderDbType"] = array2[i];
				mySqlSchemaRow["ColumnSize"] = 0;
				mySqlSchemaRow["CreateFormat"] = array[i];
				mySqlSchemaRow["CreateParameters"] = null;
				mySqlSchemaRow["DataType"] = "System.Int32";
				mySqlSchemaRow["IsAutoincrementable"] = array2[i] != MySqlDbType.Year;
				mySqlSchemaRow["IsBestMatch"] = true;
				mySqlSchemaRow["IsCaseSensitive"] = false;
				mySqlSchemaRow["IsFixedLength"] = true;
				mySqlSchemaRow["IsFixedPrecisionScale"] = true;
				mySqlSchemaRow["IsLong"] = false;
				mySqlSchemaRow["IsNullable"] = true;
				mySqlSchemaRow["IsSearchable"] = true;
				mySqlSchemaRow["IsSearchableWithLike"] = false;
				mySqlSchemaRow["IsUnsigned"] = false;
				mySqlSchemaRow["MaximumScale"] = 0;
				mySqlSchemaRow["MinimumScale"] = 0;
				mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
				mySqlSchemaRow["IsLiteralSupported"] = false;
				mySqlSchemaRow["LiteralPrefix"] = null;
				mySqlSchemaRow["LiteralSuffix"] = null;
				mySqlSchemaRow["NativeDataType"] = null;
			}
		}
	}
	internal struct MySqlInt64 : IMySqlValue
	{
		private long mValue;

		private bool isNull;

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => MySqlDbType.Int64;

		object IMySqlValue.Value => mValue;

		public long Value => mValue;

		Type IMySqlValue.SystemType => typeof(long);

		string IMySqlValue.MySqlTypeName => "BIGINT";

		public MySqlInt64(bool isNull)
		{
			this.isNull = isNull;
			mValue = 0L;
		}

		public MySqlInt64(long val)
		{
			isNull = false;
			mValue = val;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			long v = ((val is long) ? ((long)val) : Convert.ToInt64(val));
			if (binary)
			{
				packet.WriteInteger(v, 8);
			}
			else
			{
				packet.WriteStringNoNull(v.ToString());
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			if (nullVal)
			{
				return new MySqlInt64(isNull: true);
			}
			if (length == -1)
			{
				return new MySqlInt64((long)packet.ReadULong(8));
			}
			return new MySqlInt64(long.Parse(packet.ReadString(length)));
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			packet.Position += 8;
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
			mySqlSchemaRow["TypeName"] = "BIGINT";
			mySqlSchemaRow["ProviderDbType"] = MySqlDbType.Int64;
			mySqlSchemaRow["ColumnSize"] = 0;
			mySqlSchemaRow["CreateFormat"] = "BIGINT";
			mySqlSchemaRow["CreateParameters"] = null;
			mySqlSchemaRow["DataType"] = "System.Int64";
			mySqlSchemaRow["IsAutoincrementable"] = true;
			mySqlSchemaRow["IsBestMatch"] = true;
			mySqlSchemaRow["IsCaseSensitive"] = false;
			mySqlSchemaRow["IsFixedLength"] = true;
			mySqlSchemaRow["IsFixedPrecisionScale"] = true;
			mySqlSchemaRow["IsLong"] = false;
			mySqlSchemaRow["IsNullable"] = true;
			mySqlSchemaRow["IsSearchable"] = true;
			mySqlSchemaRow["IsSearchableWithLike"] = false;
			mySqlSchemaRow["IsUnsigned"] = false;
			mySqlSchemaRow["MaximumScale"] = 0;
			mySqlSchemaRow["MinimumScale"] = 0;
			mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
			mySqlSchemaRow["IsLiteralSupported"] = false;
			mySqlSchemaRow["LiteralPrefix"] = null;
			mySqlSchemaRow["LiteralSuffix"] = null;
			mySqlSchemaRow["NativeDataType"] = null;
		}
	}
	internal struct MySqlSingle : IMySqlValue
	{
		private float mValue;

		private bool isNull;

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => MySqlDbType.Float;

		object IMySqlValue.Value => mValue;

		public float Value => mValue;

		Type IMySqlValue.SystemType => typeof(float);

		string IMySqlValue.MySqlTypeName => "FLOAT";

		public MySqlSingle(bool isNull)
		{
			this.isNull = isNull;
			mValue = 0f;
		}

		public MySqlSingle(float val)
		{
			isNull = false;
			mValue = val;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			float value = ((val is float) ? ((float)val) : Convert.ToSingle(val));
			if (binary)
			{
				packet.Write(BitConverter.GetBytes(value));
			}
			else
			{
				packet.WriteStringNoNull(value.ToString("R", CultureInfo.InvariantCulture));
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			if (nullVal)
			{
				return new MySqlSingle(isNull: true);
			}
			if (length == -1)
			{
				byte[] array = new byte[4];
				packet.Read(array, 0, 4);
				return new MySqlSingle(BitConverter.ToSingle(array, 0));
			}
			return new MySqlSingle(float.Parse(packet.ReadString(length), CultureInfo.InvariantCulture));
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			packet.Position += 4;
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
			mySqlSchemaRow["TypeName"] = "FLOAT";
			mySqlSchemaRow["ProviderDbType"] = MySqlDbType.Float;
			mySqlSchemaRow["ColumnSize"] = 0;
			mySqlSchemaRow["CreateFormat"] = "FLOAT";
			mySqlSchemaRow["CreateParameters"] = null;
			mySqlSchemaRow["DataType"] = "System.Single";
			mySqlSchemaRow["IsAutoincrementable"] = false;
			mySqlSchemaRow["IsBestMatch"] = true;
			mySqlSchemaRow["IsCaseSensitive"] = false;
			mySqlSchemaRow["IsFixedLength"] = true;
			mySqlSchemaRow["IsFixedPrecisionScale"] = true;
			mySqlSchemaRow["IsLong"] = false;
			mySqlSchemaRow["IsNullable"] = true;
			mySqlSchemaRow["IsSearchable"] = true;
			mySqlSchemaRow["IsSearchableWithLike"] = false;
			mySqlSchemaRow["IsUnsigned"] = false;
			mySqlSchemaRow["MaximumScale"] = 0;
			mySqlSchemaRow["MinimumScale"] = 0;
			mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
			mySqlSchemaRow["IsLiteralSupported"] = false;
			mySqlSchemaRow["LiteralPrefix"] = null;
			mySqlSchemaRow["LiteralSuffix"] = null;
			mySqlSchemaRow["NativeDataType"] = null;
		}
	}
	internal struct MySqlString : IMySqlValue
	{
		private string mValue;

		private bool isNull;

		private MySqlDbType type;

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => type;

		object IMySqlValue.Value => mValue;

		public string Value => mValue;

		Type IMySqlValue.SystemType => typeof(string);

		string IMySqlValue.MySqlTypeName
		{
			get
			{
				if (type != MySqlDbType.Set)
				{
					if (type != MySqlDbType.Enum)
					{
						return "VARCHAR";
					}
					return "ENUM";
				}
				return "SET";
			}
		}

		public MySqlString(MySqlDbType type, bool isNull)
		{
			this.type = type;
			this.isNull = isNull;
			mValue = string.Empty;
		}

		public MySqlString(MySqlDbType type, string val)
		{
			this.type = type;
			isNull = false;
			mValue = val;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			string text = val.ToString();
			if (length > 0)
			{
				length = Math.Min(length, text.Length);
				text = text.Substring(0, length);
			}
			if (binary)
			{
				packet.WriteLenString(text);
			}
			else
			{
				packet.WriteStringNoNull("'" + MySqlHelper.EscapeString(text) + "'");
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			if (nullVal)
			{
				return new MySqlString(type, isNull: true);
			}
			string empty = string.Empty;
			MySqlString mySqlString = new MySqlString(val: (length != -1) ? packet.ReadString(length) : packet.ReadLenString(), type: type);
			return mySqlString;
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			int num = (int)packet.ReadFieldLength();
			packet.Position += num;
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			string[] array = new string[10] { "CHAR", "NCHAR", "VARCHAR", "NVARCHAR", "SET", "ENUM", "TINYTEXT", "TEXT", "MEDIUMTEXT", "LONGTEXT" };
			MySqlDbType[] array2 = new MySqlDbType[10]
			{
				MySqlDbType.String,
				MySqlDbType.String,
				MySqlDbType.VarChar,
				MySqlDbType.VarChar,
				MySqlDbType.Set,
				MySqlDbType.Enum,
				MySqlDbType.TinyText,
				MySqlDbType.Text,
				MySqlDbType.MediumText,
				MySqlDbType.LongText
			};
			for (int i = 0; i < array.Length; i++)
			{
				MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
				mySqlSchemaRow["TypeName"] = array[i];
				mySqlSchemaRow["ProviderDbType"] = array2[i];
				mySqlSchemaRow["ColumnSize"] = 0;
				mySqlSchemaRow["CreateFormat"] = ((i < 4) ? (array[i] + "({0})") : array[i]);
				mySqlSchemaRow["CreateParameters"] = ((i < 4) ? "size" : null);
				mySqlSchemaRow["DataType"] = "System.String";
				mySqlSchemaRow["IsAutoincrementable"] = false;
				mySqlSchemaRow["IsBestMatch"] = true;
				mySqlSchemaRow["IsCaseSensitive"] = false;
				mySqlSchemaRow["IsFixedLength"] = false;
				mySqlSchemaRow["IsFixedPrecisionScale"] = true;
				mySqlSchemaRow["IsLong"] = false;
				mySqlSchemaRow["IsNullable"] = true;
				mySqlSchemaRow["IsSearchable"] = true;
				mySqlSchemaRow["IsSearchableWithLike"] = true;
				mySqlSchemaRow["IsUnsigned"] = false;
				mySqlSchemaRow["MaximumScale"] = 0;
				mySqlSchemaRow["MinimumScale"] = 0;
				mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
				mySqlSchemaRow["IsLiteralSupported"] = false;
				mySqlSchemaRow["LiteralPrefix"] = null;
				mySqlSchemaRow["LiteralSuffix"] = null;
				mySqlSchemaRow["NativeDataType"] = null;
			}
		}
	}
	internal struct MySqlTimeSpan : IMySqlValue
	{
		private TimeSpan mValue;

		private bool isNull;

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => MySqlDbType.Time;

		object IMySqlValue.Value => mValue;

		public TimeSpan Value => mValue;

		Type IMySqlValue.SystemType => typeof(TimeSpan);

		string IMySqlValue.MySqlTypeName => "TIME";

		public MySqlTimeSpan(bool isNull)
		{
			this.isNull = isNull;
			mValue = TimeSpan.MinValue;
		}

		public MySqlTimeSpan(TimeSpan val)
		{
			isNull = false;
			mValue = val;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			if (!(val is TimeSpan timeSpan))
			{
				throw new MySqlException("Only TimeSpan objects can be serialized by MySqlTimeSpan");
			}
			bool flag = timeSpan.TotalMilliseconds < 0.0;
			TimeSpan timeSpan2 = timeSpan.Duration();
			if (binary)
			{
				if (timeSpan2.Milliseconds > 0)
				{
					packet.WriteByte(12);
				}
				else
				{
					packet.WriteByte(8);
				}
				packet.WriteByte((byte)(flag ? 1u : 0u));
				packet.WriteInteger(timeSpan2.Days, 4);
				packet.WriteByte((byte)timeSpan2.Hours);
				packet.WriteByte((byte)timeSpan2.Minutes);
				packet.WriteByte((byte)timeSpan2.Seconds);
				if (timeSpan2.Milliseconds > 0)
				{
					long v = timeSpan2.Milliseconds * 1000;
					packet.WriteInteger(v, 4);
				}
			}
			else
			{
				string v2 = string.Format("'{0}{1} {2:00}:{3:00}:{4:00}.{5:000000}'", flag ? "-" : "", timeSpan2.Days, timeSpan2.Hours, timeSpan2.Minutes, timeSpan2.Seconds, timeSpan2.Ticks % 10000000);
				packet.WriteStringNoNull(v2);
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			if (nullVal)
			{
				return new MySqlTimeSpan(isNull: true);
			}
			if (length >= 0)
			{
				string s = packet.ReadString(length);
				ParseMySql(s);
				return this;
			}
			long num = packet.ReadByte();
			int num2 = 0;
			if (num > 0)
			{
				num2 = packet.ReadByte();
			}
			isNull = false;
			switch (num)
			{
			case 0L:
				isNull = true;
				break;
			case 5L:
				mValue = new TimeSpan(packet.ReadInteger(4), 0, 0, 0);
				break;
			case 8L:
				mValue = new TimeSpan(packet.ReadInteger(4), packet.ReadByte(), packet.ReadByte(), packet.ReadByte());
				break;
			default:
				mValue = new TimeSpan(packet.ReadInteger(4), packet.ReadByte(), packet.ReadByte(), packet.ReadByte(), packet.ReadInteger(4) / 1000000);
				break;
			}
			if (num2 == 1)
			{
				mValue = mValue.Negate();
			}
			return this;
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			int num = packet.ReadByte();
			packet.Position += num;
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
			mySqlSchemaRow["TypeName"] = "TIME";
			mySqlSchemaRow["ProviderDbType"] = MySqlDbType.Time;
			mySqlSchemaRow["ColumnSize"] = 0;
			mySqlSchemaRow["CreateFormat"] = "TIME";
			mySqlSchemaRow["CreateParameters"] = null;
			mySqlSchemaRow["DataType"] = "System.TimeSpan";
			mySqlSchemaRow["IsAutoincrementable"] = false;
			mySqlSchemaRow["IsBestMatch"] = true;
			mySqlSchemaRow["IsCaseSensitive"] = false;
			mySqlSchemaRow["IsFixedLength"] = true;
			mySqlSchemaRow["IsFixedPrecisionScale"] = true;
			mySqlSchemaRow["IsLong"] = false;
			mySqlSchemaRow["IsNullable"] = true;
			mySqlSchemaRow["IsSearchable"] = true;
			mySqlSchemaRow["IsSearchableWithLike"] = false;
			mySqlSchemaRow["IsUnsigned"] = false;
			mySqlSchemaRow["MaximumScale"] = 0;
			mySqlSchemaRow["MinimumScale"] = 0;
			mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
			mySqlSchemaRow["IsLiteralSupported"] = false;
			mySqlSchemaRow["LiteralPrefix"] = null;
			mySqlSchemaRow["LiteralSuffix"] = null;
			mySqlSchemaRow["NativeDataType"] = null;
		}

		public override string ToString()
		{
			return $"{mValue.Days} {mValue.Hours:00}:{mValue.Minutes:00}:{mValue.Seconds:00}";
		}

		private void ParseMySql(string s)
		{
			string[] array = s.Split(':', '.');
			int num = int.Parse(array[0]);
			int num2 = int.Parse(array[1]);
			int num3 = int.Parse(array[2]);
			int num4 = 0;
			if (array.Length > 3)
			{
				array[3] = array[3].PadRight(7, '0');
				num4 = int.Parse(array[3]);
			}
			if (num < 0 || array[0].StartsWith("-", StringComparison.Ordinal))
			{
				num2 *= -1;
				num3 *= -1;
				num4 *= -1;
			}
			int num5 = num / 24;
			num -= num5 * 24;
			mValue = new TimeSpan(num5, num, num2, num3).Add(new TimeSpan(num4));
			isNull = false;
		}
	}
	internal struct MySqlUByte : IMySqlValue
	{
		private byte mValue;

		private bool isNull;

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => MySqlDbType.UByte;

		object IMySqlValue.Value => mValue;

		public byte Value => mValue;

		Type IMySqlValue.SystemType => typeof(byte);

		string IMySqlValue.MySqlTypeName => "TINYINT";

		public MySqlUByte(bool isNull)
		{
			this.isNull = isNull;
			mValue = 0;
		}

		public MySqlUByte(byte val)
		{
			isNull = false;
			mValue = val;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			byte b = ((val is byte) ? ((byte)val) : Convert.ToByte(val));
			if (binary)
			{
				packet.WriteByte(b);
			}
			else
			{
				packet.WriteStringNoNull(b.ToString());
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			if (nullVal)
			{
				return new MySqlUByte(isNull: true);
			}
			if (length == -1)
			{
				return new MySqlUByte(packet.ReadByte());
			}
			return new MySqlUByte(byte.Parse(packet.ReadString(length)));
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			packet.ReadByte();
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
			mySqlSchemaRow["TypeName"] = "TINY INT";
			mySqlSchemaRow["ProviderDbType"] = MySqlDbType.UByte;
			mySqlSchemaRow["ColumnSize"] = 0;
			mySqlSchemaRow["CreateFormat"] = "TINYINT UNSIGNED";
			mySqlSchemaRow["CreateParameters"] = null;
			mySqlSchemaRow["DataType"] = "System.Byte";
			mySqlSchemaRow["IsAutoincrementable"] = true;
			mySqlSchemaRow["IsBestMatch"] = true;
			mySqlSchemaRow["IsCaseSensitive"] = false;
			mySqlSchemaRow["IsFixedLength"] = true;
			mySqlSchemaRow["IsFixedPrecisionScale"] = true;
			mySqlSchemaRow["IsLong"] = false;
			mySqlSchemaRow["IsNullable"] = true;
			mySqlSchemaRow["IsSearchable"] = true;
			mySqlSchemaRow["IsSearchableWithLike"] = false;
			mySqlSchemaRow["IsUnsigned"] = true;
			mySqlSchemaRow["MaximumScale"] = 0;
			mySqlSchemaRow["MinimumScale"] = 0;
			mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
			mySqlSchemaRow["IsLiteralSupported"] = false;
			mySqlSchemaRow["LiteralPrefix"] = null;
			mySqlSchemaRow["LiteralSuffix"] = null;
			mySqlSchemaRow["NativeDataType"] = null;
		}
	}
	internal struct MySqlUInt16 : IMySqlValue
	{
		private ushort mValue;

		private bool isNull;

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => MySqlDbType.UInt16;

		object IMySqlValue.Value => mValue;

		public ushort Value => mValue;

		Type IMySqlValue.SystemType => typeof(ushort);

		string IMySqlValue.MySqlTypeName => "SMALLINT";

		public MySqlUInt16(bool isNull)
		{
			this.isNull = isNull;
			mValue = 0;
		}

		public MySqlUInt16(ushort val)
		{
			isNull = false;
			mValue = val;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			int num = ((val is ushort) ? ((ushort)val) : Convert.ToUInt16(val));
			if (binary)
			{
				packet.WriteInteger(num, 2);
			}
			else
			{
				packet.WriteStringNoNull(num.ToString());
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			if (nullVal)
			{
				return new MySqlUInt16(isNull: true);
			}
			if (length == -1)
			{
				return new MySqlUInt16((ushort)packet.ReadInteger(2));
			}
			return new MySqlUInt16(ushort.Parse(packet.ReadString(length)));
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			packet.Position += 2;
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
			mySqlSchemaRow["TypeName"] = "SMALLINT";
			mySqlSchemaRow["ProviderDbType"] = MySqlDbType.UInt16;
			mySqlSchemaRow["ColumnSize"] = 0;
			mySqlSchemaRow["CreateFormat"] = "SMALLINT UNSIGNED";
			mySqlSchemaRow["CreateParameters"] = null;
			mySqlSchemaRow["DataType"] = "System.UInt16";
			mySqlSchemaRow["IsAutoincrementable"] = true;
			mySqlSchemaRow["IsBestMatch"] = true;
			mySqlSchemaRow["IsCaseSensitive"] = false;
			mySqlSchemaRow["IsFixedLength"] = true;
			mySqlSchemaRow["IsFixedPrecisionScale"] = true;
			mySqlSchemaRow["IsLong"] = false;
			mySqlSchemaRow["IsNullable"] = true;
			mySqlSchemaRow["IsSearchable"] = true;
			mySqlSchemaRow["IsSearchableWithLike"] = false;
			mySqlSchemaRow["IsUnsigned"] = true;
			mySqlSchemaRow["MaximumScale"] = 0;
			mySqlSchemaRow["MinimumScale"] = 0;
			mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
			mySqlSchemaRow["IsLiteralSupported"] = false;
			mySqlSchemaRow["LiteralPrefix"] = null;
			mySqlSchemaRow["LiteralSuffix"] = null;
			mySqlSchemaRow["NativeDataType"] = null;
		}
	}
	internal struct MySqlUInt32 : IMySqlValue
	{
		private uint mValue;

		private bool isNull;

		private bool is24Bit;

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => MySqlDbType.UInt32;

		object IMySqlValue.Value => mValue;

		public uint Value => mValue;

		Type IMySqlValue.SystemType => typeof(uint);

		string IMySqlValue.MySqlTypeName
		{
			get
			{
				if (!is24Bit)
				{
					return "INT";
				}
				return "MEDIUMINT";
			}
		}

		private MySqlUInt32(MySqlDbType type)
		{
			is24Bit = type == MySqlDbType.Int24;
			isNull = true;
			mValue = 0u;
		}

		public MySqlUInt32(MySqlDbType type, bool isNull)
			: this(type)
		{
			this.isNull = isNull;
		}

		public MySqlUInt32(MySqlDbType type, uint val)
			: this(type)
		{
			isNull = false;
			mValue = val;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object v, int length)
		{
			uint num = ((v is uint) ? ((uint)v) : Convert.ToUInt32(v));
			if (binary)
			{
				packet.WriteInteger(num, is24Bit ? 3 : 4);
			}
			else
			{
				packet.WriteStringNoNull(num.ToString());
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			if (nullVal)
			{
				return new MySqlUInt32(((IMySqlValue)this).MySqlDbType, isNull: true);
			}
			if (length == -1)
			{
				return new MySqlUInt32(((IMySqlValue)this).MySqlDbType, (uint)packet.ReadInteger(4));
			}
			return new MySqlUInt32(((IMySqlValue)this).MySqlDbType, uint.Parse(packet.ReadString(length), NumberStyles.Any, CultureInfo.InvariantCulture));
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			packet.Position += 4;
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			string[] array = new string[2] { "MEDIUMINT", "INT" };
			MySqlDbType[] array2 = new MySqlDbType[2]
			{
				MySqlDbType.UInt24,
				MySqlDbType.UInt32
			};
			for (int i = 0; i < array.Length; i++)
			{
				MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
				mySqlSchemaRow["TypeName"] = array[i];
				mySqlSchemaRow["ProviderDbType"] = array2[i];
				mySqlSchemaRow["ColumnSize"] = 0;
				mySqlSchemaRow["CreateFormat"] = array[i] + " UNSIGNED";
				mySqlSchemaRow["CreateParameters"] = null;
				mySqlSchemaRow["DataType"] = "System.UInt32";
				mySqlSchemaRow["IsAutoincrementable"] = true;
				mySqlSchemaRow["IsBestMatch"] = true;
				mySqlSchemaRow["IsCaseSensitive"] = false;
				mySqlSchemaRow["IsFixedLength"] = true;
				mySqlSchemaRow["IsFixedPrecisionScale"] = true;
				mySqlSchemaRow["IsLong"] = false;
				mySqlSchemaRow["IsNullable"] = true;
				mySqlSchemaRow["IsSearchable"] = true;
				mySqlSchemaRow["IsSearchableWithLike"] = false;
				mySqlSchemaRow["IsUnsigned"] = true;
				mySqlSchemaRow["MaximumScale"] = 0;
				mySqlSchemaRow["MinimumScale"] = 0;
				mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
				mySqlSchemaRow["IsLiteralSupported"] = false;
				mySqlSchemaRow["LiteralPrefix"] = null;
				mySqlSchemaRow["LiteralSuffix"] = null;
				mySqlSchemaRow["NativeDataType"] = null;
			}
		}
	}
	internal struct MySqlUInt64 : IMySqlValue
	{
		private ulong mValue;

		private bool isNull;

		public bool IsNull => isNull;

		MySqlDbType IMySqlValue.MySqlDbType => MySqlDbType.UInt64;

		object IMySqlValue.Value => mValue;

		public ulong Value => mValue;

		Type IMySqlValue.SystemType => typeof(ulong);

		string IMySqlValue.MySqlTypeName => "BIGINT";

		public MySqlUInt64(bool isNull)
		{
			this.isNull = isNull;
			mValue = 0uL;
		}

		public MySqlUInt64(ulong val)
		{
			isNull = false;
			mValue = val;
		}

		void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
		{
			ulong v = ((val is ulong) ? ((ulong)val) : Convert.ToUInt64(val));
			if (binary)
			{
				packet.WriteInteger((long)v, 8);
			}
			else
			{
				packet.WriteStringNoNull(v.ToString());
			}
		}

		IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
		{
			if (nullVal)
			{
				return new MySqlUInt64(isNull: true);
			}
			if (length == -1)
			{
				return new MySqlUInt64(packet.ReadULong(8));
			}
			return new MySqlUInt64(ulong.Parse(packet.ReadString(length)));
		}

		void IMySqlValue.SkipValue(MySqlPacket packet)
		{
			packet.Position += 8;
		}

		internal static void SetDSInfo(MySqlSchemaCollection sc)
		{
			MySqlSchemaRow mySqlSchemaRow = sc.AddRow();
			mySqlSchemaRow["TypeName"] = "BIGINT";
			mySqlSchemaRow["ProviderDbType"] = MySqlDbType.UInt64;
			mySqlSchemaRow["ColumnSize"] = 0;
			mySqlSchemaRow["CreateFormat"] = "BIGINT UNSIGNED";
			mySqlSchemaRow["CreateParameters"] = null;
			mySqlSchemaRow["DataType"] = "System.UInt64";
			mySqlSchemaRow["IsAutoincrementable"] = true;
			mySqlSchemaRow["IsBestMatch"] = true;
			mySqlSchemaRow["IsCaseSensitive"] = false;
			mySqlSchemaRow["IsFixedLength"] = true;
			mySqlSchemaRow["IsFixedPrecisionScale"] = true;
			mySqlSchemaRow["IsLong"] = false;
			mySqlSchemaRow["IsNullable"] = true;
			mySqlSchemaRow["IsSearchable"] = true;
			mySqlSchemaRow["IsSearchableWithLike"] = false;
			mySqlSchemaRow["IsUnsigned"] = true;
			mySqlSchemaRow["MaximumScale"] = 0;
			mySqlSchemaRow["MinimumScale"] = 0;
			mySqlSchemaRow["IsConcurrencyType"] = DBNull.Value;
			mySqlSchemaRow["IsLiteralSupported"] = false;
			mySqlSchemaRow["LiteralPrefix"] = null;
			mySqlSchemaRow["LiteralSuffix"] = null;
			mySqlSchemaRow["NativeDataType"] = null;
		}
	}
}
namespace zlib
{
	internal sealed class Adler32
	{
		private const int BASE = 65521;

		private const int NMAX = 5552;

		internal long adler32(long adler, byte[] buf, int index, int len)
		{
			if (buf == null)
			{
				return 1L;
			}
			long num = adler & 0xFFFF;
			long num2 = (adler >> 16) & 0xFFFF;
			while (len > 0)
			{
				int num3 = ((len < 5552) ? len : 5552);
				len -= num3;
				while (num3 >= 16)
				{
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num += buf[index++] & 0xFF;
					num2 += num;
					num3 -= 16;
				}
				if (num3 != 0)
				{
					do
					{
						num += buf[index++] & 0xFF;
						num2 += num;
					}
					while (--num3 != 0);
				}
				num %= 65521;
				num2 %= 65521;
			}
			return (num2 << 16) | num;
		}
	}
	internal sealed class Deflate
	{
		internal class Config
		{
			internal int good_length;

			internal int max_lazy;

			internal int nice_length;

			internal int max_chain;

			internal int func;

			internal Config(int good_length, int max_lazy, int nice_length, int max_chain, int func)
			{
				this.good_length = good_length;
				this.max_lazy = max_lazy;
				this.nice_length = nice_length;
				this.max_chain = max_chain;
				this.func = func;
			}
		}

		private const int MAX_MEM_LEVEL = 9;

		private const int Z_DEFAULT_COMPRESSION = -1;

		private const int MAX_WBITS = 15;

		private const int DEF_MEM_LEVEL = 8;

		private const int STORED = 0;

		private const int FAST = 1;

		private const int SLOW = 2;

		private const int NeedMore = 0;

		private const int BlockDone = 1;

		private const int FinishStarted = 2;

		private const int FinishDone = 3;

		private const int PRESET_DICT = 32;

		private const int Z_FILTERED = 1;

		private const int Z_HUFFMAN_ONLY = 2;

		private const int Z_DEFAULT_STRATEGY = 0;

		private const int Z_NO_FLUSH = 0;

		private const int Z_PARTIAL_FLUSH = 1;

		private const int Z_SYNC_FLUSH = 2;

		private const int Z_FULL_FLUSH = 3;

		private const int Z_FINISH = 4;

		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		private const int INIT_STATE = 42;

		private const int BUSY_STATE = 113;

		private const int FINISH_STATE = 666;

		private const int Z_DEFLATED = 8;

		private const int STORED_BLOCK = 0;

		private const int STATIC_TREES = 1;

		private const int DYN_TREES = 2;

		private const int Z_BINARY = 0;

		private const int Z_ASCII = 1;

		private const int Z_UNKNOWN = 2;

		private const int Buf_size = 16;

		private const int REP_3_6 = 16;

		private const int REPZ_3_10 = 17;

		private const int REPZ_11_138 = 18;

		private const int MIN_MATCH = 3;

		private const int MAX_MATCH = 258;

		private const int MAX_BITS = 15;

		private const int D_CODES = 30;

		private const int BL_CODES = 19;

		private const int LENGTH_CODES = 29;

		private const int LITERALS = 256;

		private const int END_BLOCK = 256;

		private static Config[] config_table;

		private static readonly string[] z_errmsg;

		private static readonly int MIN_LOOKAHEAD;

		private static readonly int L_CODES;

		private static readonly int HEAP_SIZE;

		internal ZStream strm;

		internal int status;

		internal byte[] pending_buf;

		internal int pending_buf_size;

		internal int pending_out;

		internal int pending;

		internal int noheader;

		internal byte data_type;

		internal byte method;

		internal int last_flush;

		internal int w_size;

		internal int w_bits;

		internal int w_mask;

		internal byte[] window;

		internal int window_size;

		internal short[] prev;

		internal short[] head;

		internal int ins_h;

		internal int hash_size;

		internal int hash_bits;

		internal int hash_mask;

		internal int hash_shift;

		internal int block_start;

		internal int match_length;

		internal int prev_match;

		internal int match_available;

		internal int strstart;

		internal int match_start;

		internal int lookahead;

		internal int prev_length;

		internal int max_chain_length;

		internal int max_lazy_match;

		internal int level;

		internal int strategy;

		internal int good_match;

		internal int nice_match;

		internal short[] dyn_ltree;

		internal short[] dyn_dtree;

		internal short[] bl_tree;

		internal Tree l_desc = new Tree();

		internal Tree d_desc = new Tree();

		internal Tree bl_desc = new Tree();

		internal short[] bl_count = new short[16];

		internal int[] heap = new int[2 * L_CODES + 1];

		internal int heap_len;

		internal int heap_max;

		internal byte[] depth = new byte[2 * L_CODES + 1];

		internal int l_buf;

		internal int lit_bufsize;

		internal int last_lit;

		internal int d_buf;

		internal int opt_len;

		internal int static_len;

		internal int matches;

		internal int last_eob_len;

		internal short bi_buf;

		internal int bi_valid;

		internal Deflate()
		{
			dyn_ltree = new short[HEAP_SIZE * 2];
			dyn_dtree = new short[122];
			bl_tree = new short[78];
		}

		internal void lm_init()
		{
			window_size = 2 * w_size;
			head[hash_size - 1] = 0;
			for (int i = 0; i < hash_size - 1; i++)
			{
				head[i] = 0;
			}
			max_lazy_match = config_table[level].max_lazy;
			good_match = config_table[level].good_length;
			nice_match = config_table[level].nice_length;
			max_chain_length = config_table[level].max_chain;
			strstart = 0;
			block_start = 0;
			lookahead = 0;
			match_length = (prev_length = 2);
			match_available = 0;
			ins_h = 0;
		}

		internal void tr_init()
		{
			l_desc.dyn_tree = dyn_ltree;
			l_desc.stat_desc = StaticTree.static_l_desc;
			d_desc.dyn_tree = dyn_dtree;
			d_desc.stat_desc = StaticTree.static_d_desc;
			bl_desc.dyn_tree = bl_tree;
			bl_desc.stat_desc = StaticTree.static_bl_desc;
			bi_buf = 0;
			bi_valid = 0;
			last_eob_len = 8;
			init_block();
		}

		internal void init_block()
		{
			for (int i = 0; i < L_CODES; i++)
			{
				dyn_ltree[i * 2] = 0;
			}
			for (int j = 0; j < 30; j++)
			{
				dyn_dtree[j * 2] = 0;
			}
			for (int k = 0; k < 19; k++)
			{
				bl_tree[k * 2] = 0;
			}
			dyn_ltree[512] = 1;
			opt_len = (static_len = 0);
			last_lit = (matches = 0);
		}

		internal void pqdownheap(short[] tree, int k)
		{
			int num = heap[k];
			for (int num2 = k << 1; num2 <= heap_len; num2 <<= 1)
			{
				if (num2 < heap_len && smaller(tree, heap[num2 + 1], heap[num2], depth))
				{
					num2++;
				}
				if (smaller(tree, num, heap[num2], depth))
				{
					break;
				}
				heap[k] = heap[num2];
				k = num2;
			}
			heap[k] = num;
		}

		internal static bool smaller(short[] tree, int n, int m, byte[] depth)
		{
			if (tree[n * 2] >= tree[m * 2])
			{
				if (tree[n * 2] == tree[m * 2])
				{
					return depth[n] <= depth[m];
				}
				return false;
			}
			return true;
		}

		internal void scan_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num2 = tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			tree[(max_code + 1) * 2 + 1] = (short)SupportClass.Identity(65535L);
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = tree[(i + 1) * 2 + 1];
				if (++num3 < num4 && num6 == num2)
				{
					continue;
				}
				if (num3 < num5)
				{
					bl_tree[num6 * 2] = (short)(bl_tree[num6 * 2] + num3);
				}
				else if (num6 != 0)
				{
					if (num6 != num)
					{
						bl_tree[num6 * 2]++;
					}
					bl_tree[32]++;
				}
				else if (num3 <= 10)
				{
					bl_tree[34]++;
				}
				else
				{
					bl_tree[36]++;
				}
				num3 = 0;
				num = num6;
				if (num2 == 0)
				{
					num4 = 138;
					num5 = 3;
				}
				else if (num6 == num2)
				{
					num4 = 6;
					num5 = 3;
				}
				else
				{
					num4 = 7;
					num5 = 4;
				}
			}
		}

		internal int build_bl_tree()
		{
			scan_tree(dyn_ltree, l_desc.max_code);
			scan_tree(dyn_dtree, d_desc.max_code);
			bl_desc.build_tree(this);
			int num = 18;
			while (num >= 3 && bl_tree[Tree.bl_order[num] * 2 + 1] == 0)
			{
				num--;
			}
			opt_len += 3 * (num + 1) + 5 + 5 + 4;
			return num;
		}

		internal void send_all_trees(int lcodes, int dcodes, int blcodes)
		{
			send_bits(lcodes - 257, 5);
			send_bits(dcodes - 1, 5);
			send_bits(blcodes - 4, 4);
			for (int i = 0; i < blcodes; i++)
			{
				send_bits(bl_tree[Tree.bl_order[i] * 2 + 1], 3);
			}
			send_tree(dyn_ltree, lcodes - 1);
			send_tree(dyn_dtree, dcodes - 1);
		}

		internal void send_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num2 = tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = tree[(i + 1) * 2 + 1];
				if (++num3 < num4 && num6 == num2)
				{
					continue;
				}
				if (num3 < num5)
				{
					do
					{
						send_code(num6, bl_tree);
					}
					while (--num3 != 0);
				}
				else if (num6 != 0)
				{
					if (num6 != num)
					{
						send_code(num6, bl_tree);
						num3--;
					}
					send_code(16, bl_tree);
					send_bits(num3 - 3, 2);
				}
				else if (num3 <= 10)
				{
					send_code(17, bl_tree);
					send_bits(num3 - 3, 3);
				}
				else
				{
					send_code(18, bl_tree);
					send_bits(num3 - 11, 7);
				}
				num3 = 0;
				num = num6;
				if (num2 == 0)
				{
					num4 = 138;
					num5 = 3;
				}
				else if (num6 == num2)
				{
					num4 = 6;
					num5 = 3;
				}
				else
				{
					num4 = 7;
					num5 = 4;
				}
			}
		}

		internal void put_byte(byte[] p, int start, int len)
		{
			Array.Copy(p, start, pending_buf, pending, len);
			pending += len;
		}

		internal void put_byte(byte c)
		{
			pending_buf[pending++] = c;
		}

		internal void put_short(int w)
		{
			put_byte((byte)w);
			put_byte((byte)SupportClass.URShift(w, 8));
		}

		internal void putShortMSB(int b)
		{
			put_byte((byte)(b >> 8));
			put_byte((byte)b);
		}

		internal void send_code(int c, short[] tree)
		{
			send_bits(tree[c * 2] & 0xFFFF, tree[c * 2 + 1] & 0xFFFF);
		}

		internal void send_bits(int value_Renamed, int length)
		{
			if (bi_valid > 16 - length)
			{
				bi_buf = (short)((ushort)bi_buf | (ushort)((value_Renamed << bi_valid) & 0xFFFF));
				put_short(bi_buf);
				bi_buf = (short)SupportClass.URShift(value_Renamed, 16 - bi_valid);
				bi_valid += length - 16;
			}
			else
			{
				bi_buf = (short)((ushort)bi_buf | (ushort)((value_Renamed << bi_valid) & 0xFFFF));
				bi_valid += length;
			}
		}

		internal void _tr_align()
		{
			send_bits(2, 3);
			send_code(256, StaticTree.static_ltree);
			bi_flush();
			if (1 + last_eob_len + 10 - bi_valid < 9)
			{
				send_bits(2, 3);
				send_code(256, StaticTree.static_ltree);
				bi_flush();
			}
			last_eob_len = 7;
		}

		internal bool _tr_tally(int dist, int lc)
		{
			pending_buf[d_buf + last_lit * 2] = (byte)SupportClass.URShift(dist, 8);
			pending_buf[d_buf + last_lit * 2 + 1] = (byte)dist;
			pending_buf[l_buf + last_lit] = (byte)lc;
			last_lit++;
			if (dist == 0)
			{
				dyn_ltree[lc * 2]++;
			}
			else
			{
				matches++;
				dist--;
				dyn_ltree[(Tree._length_code[lc] + 256 + 1) * 2]++;
				dyn_dtree[Tree.d_code(dist) * 2]++;
			}
			if ((last_lit & 0x1FFF) == 0 && level > 2)
			{
				int num = last_lit * 8;
				int num2 = strstart - block_start;
				for (int i = 0; i < 30; i++)
				{
					num = (int)(num + dyn_dtree[i * 2] * (5L + (long)Tree.extra_dbits[i]));
				}
				num = SupportClass.URShift(num, 3);
				if (matches < last_lit / 2 && num < num2 / 2)
				{
					return true;
				}
			}
			return last_lit == lit_bufsize - 1;
		}

		internal void compress_block(short[] ltree, short[] dtree)
		{
			int num = 0;
			if (last_lit != 0)
			{
				do
				{
					int num2 = ((pending_buf[d_buf + num * 2] << 8) & 0xFF00) | (pending_buf[d_buf + num * 2 + 1] & 0xFF);
					int num3 = pending_buf[l_buf + num] & 0xFF;
					num++;
					if (num2 == 0)
					{
						send_code(num3, ltree);
						continue;
					}
					int num4 = Tree._length_code[num3];
					send_code(num4 + 256 + 1, ltree);
					int num5 = Tree.extra_lbits[num4];
					if (num5 != 0)
					{
						num3 -= Tree.base_length[num4];
						send_bits(num3, num5);
					}
					num2--;
					num4 = Tree.d_code(num2);
					send_code(num4, dtree);
					num5 = Tree.extra_dbits[num4];
					if (num5 != 0)
					{
						num2 -= Tree.base_dist[num4];
						send_bits(num2, num5);
					}
				}
				while (num < last_lit);
			}
			send_code(256, ltree);
			last_eob_len = ltree[513];
		}

		internal void set_data_type()
		{
			int i = 0;
			int num = 0;
			int num2 = 0;
			for (; i < 7; i++)
			{
				num2 += dyn_ltree[i * 2];
			}
			for (; i < 128; i++)
			{
				num += dyn_ltree[i * 2];
			}
			for (; i < 256; i++)
			{
				num2 += dyn_ltree[i * 2];
			}
			data_type = (byte)((num2 <= SupportClass.URShift(num, 2)) ? 1u : 0u);
		}

		internal void bi_flush()
		{
			if (bi_valid == 16)
			{
				put_short(bi_buf);
				bi_buf = 0;
				bi_valid = 0;
			}
			else if (bi_valid >= 8)
			{
				put_byte((byte)bi_buf);
				bi_buf = (short)SupportClass.URShift(bi_buf, 8);
				bi_valid -= 8;
			}
		}

		internal void bi_windup()
		{
			if (bi_valid > 8)
			{
				put_short(bi_buf);
			}
			else if (bi_valid > 0)
			{
				put_byte((byte)bi_buf);
			}
			bi_buf = 0;
			bi_valid = 0;
		}

		internal void copy_block(int buf, int len, bool header)
		{
			bi_windup();
			last_eob_len = 8;
			if (header)
			{
				put_short((short)len);
				put_short((short)(~len));
			}
			put_byte(window, buf, len);
		}

		internal void flush_block_only(bool eof)
		{
			_tr_flush_block((block_start >= 0) ? block_start : (-1), strstart - block_start, eof);
			block_start = strstart;
			strm.flush_pending();
		}

		internal int deflate_stored(int flush)
		{
			int num = 65535;
			if (num > pending_buf_size - 5)
			{
				num = pending_buf_size - 5;
			}
			while (true)
			{
				if (lookahead <= 1)
				{
					fill_window();
					if (lookahead == 0 && flush == 0)
					{
						return 0;
					}
					if (lookahead == 0)
					{
						break;
					}
				}
				strstart += lookahead;
				lookahead = 0;
				int num2 = block_start + num;
				if (strstart == 0 || strstart >= num2)
				{
					lookahead = strstart - num2;
					strstart = num2;
					flush_block_only(eof: false);
					if (strm.avail_out == 0)
					{
						return 0;
					}
				}
				if (strstart - block_start >= w_size - MIN_LOOKAHEAD)
				{
					flush_block_only(eof: false);
					if (strm.avail_out == 0)
					{
						return 0;
					}
				}
			}
			flush_block_only(flush == 4);
			if (strm.avail_out == 0)
			{
				if (flush != 4)
				{
					return 0;
				}
				return 2;
			}
			if (flush != 4)
			{
				return 1;
			}
			return 3;
		}

		internal void _tr_stored_block(int buf, int stored_len, bool eof)
		{
			send_bits(eof ? 1 : 0, 3);
			copy_block(buf, stored_len, header: true);
		}

		internal void _tr_flush_block(int buf, int stored_len, bool eof)
		{
			int num = 0;
			int num2;
			int num3;
			if (level > 0)
			{
				if (data_type == 2)
				{
					set_data_type();
				}
				l_desc.build_tree(this);
				d_desc.build_tree(this);
				num = build_bl_tree();
				num2 = SupportClass.URShift(opt_len + 3 + 7, 3);
				num3 = SupportClass.URShift(static_len + 3 + 7, 3);
				if (num3 <= num2)
				{
					num2 = num3;
				}
			}
			else
			{
				num2 = (num3 = stored_len + 5);
			}
			if (stored_len + 4 <= num2 && buf != -1)
			{
				_tr_stored_block(buf, stored_len, eof);
			}
			else if (num3 == num2)
			{
				send_bits(2 + (eof ? 1 : 0), 3);
				compress_block(StaticTree.static_ltree, StaticTree.static_dtree);
			}
			else
			{
				send_bits(4 + (eof ? 1 : 0), 3);
				send_all_trees(l_desc.max_code + 1, d_desc.max_code + 1, num + 1);
				compress_block(dyn_ltree, dyn_dtree);
			}
			init_block();
			if (eof)
			{
				bi_windup();
			}
		}

		internal void fill_window()
		{
			do
			{
				int num = window_size - lookahead - strstart;
				int num2;
				if (num == 0 && strstart == 0 && lookahead == 0)
				{
					num = w_size;
				}
				else if (num == -1)
				{
					num--;
				}
				else if (strstart >= w_size + w_size - MIN_LOOKAHEAD)
				{
					Array.Copy(window, w_size, window, 0, w_size);
					match_start -= w_size;
					strstart -= w_size;
					block_start -= w_size;
					num2 = hash_size;
					int num3 = num2;
					do
					{
						int num4 = head[--num3] & 0xFFFF;
						head[num3] = (short)((num4 >= w_size) ? (num4 - w_size) : 0);
					}
					while (--num2 != 0);
					num2 = w_size;
					num3 = num2;
					do
					{
						int num4 = prev[--num3] & 0xFFFF;
						prev[num3] = (short)((num4 >= w_size) ? (num4 - w_size) : 0);
					}
					while (--num2 != 0);
					num += w_size;
				}
				if (strm.avail_in == 0)
				{
					break;
				}
				num2 = strm.read_buf(window, strstart + lookahead, num);
				lookahead += num2;
				if (lookahead >= 3)
				{
					ins_h = window[strstart] & 0xFF;
					ins_h = ((ins_h << hash_shift) ^ (window[strstart + 1] & 0xFF)) & hash_mask;
				}
			}
			while (lookahead < MIN_LOOKAHEAD && strm.avail_in != 0);
		}

		internal int deflate_fast(int flush)
		{
			int num = 0;
			while (true)
			{
				if (lookahead < MIN_LOOKAHEAD)
				{
					fill_window();
					if (lookahead < MIN_LOOKAHEAD && flush == 0)
					{
						return 0;
					}
					if (lookahead == 0)
					{
						break;
					}
				}
				if (lookahead >= 3)
				{
					ins_h = ((ins_h << hash_shift) ^ (window[strstart + 2] & 0xFF)) & hash_mask;
					num = head[ins_h] & 0xFFFF;
					prev[strstart & w_mask] = head[ins_h];
					head[ins_h] = (short)strstart;
				}
				if ((long)num != 0 && ((strstart - num) & 0xFFFF) <= w_size - MIN_LOOKAHEAD && strategy != 2)
				{
					match_length = longest_match(num);
				}
				bool flag;
				if (match_length >= 3)
				{
					flag = _tr_tally(strstart - match_start, match_length - 3);
					lookahead -= match_length;
					if (match_length <= max_lazy_match && lookahead >= 3)
					{
						match_length--;
						do
						{
							strstart++;
							ins_h = ((ins_h << hash_shift) ^ (window[strstart + 2] & 0xFF)) & hash_mask;
							num = head[ins_h] & 0xFFFF;
							prev[strstart & w_mask] = head[ins_h];
							head[ins_h] = (short)strstart;
						}
						while (--match_length != 0);
						strstart++;
					}
					else
					{
						strstart += match_length;
						match_length = 0;
						ins_h = window[strstart] & 0xFF;
						ins_h = ((ins_h << hash_shift) ^ (window[strstart + 1] & 0xFF)) & hash_mask;
					}
				}
				else
				{
					flag = _tr_tally(0, window[strstart] & 0xFF);
					lookahead--;
					strstart++;
				}
				if (flag)
				{
					flush_block_only(eof: false);
					if (strm.avail_out == 0)
					{
						return 0;
					}
				}
			}
			flush_block_only(flush == 4);
			if (strm.avail_out == 0)
			{
				if (flush == 4)
				{
					return 2;
				}
				return 0;
			}
			if (flush != 4)
			{
				return 1;
			}
			return 3;
		}

		internal int deflate_slow(int flush)
		{
			int num = 0;
			while (true)
			{
				if (lookahead < MIN_LOOKAHEAD)
				{
					fill_window();
					if (lookahead < MIN_LOOKAHEAD && flush == 0)
					{
						return 0;
					}
					if (lookahead == 0)
					{
						break;
					}
				}
				if (lookahead >= 3)
				{
					ins_h = ((ins_h << hash_shift) ^ (window[strstart + 2] & 0xFF)) & hash_mask;
					num = head[ins_h] & 0xFFFF;
					prev[strstart & w_mask] = head[ins_h];
					head[ins_h] = (short)strstart;
				}
				prev_length = match_length;
				prev_match = match_start;
				match_length = 2;
				if (num != 0 && prev_length < max_lazy_match && ((strstart - num) & 0xFFFF) <= w_size - MIN_LOOKAHEAD)
				{
					if (strategy != 2)
					{
						match_length = longest_match(num);
					}
					if (match_length <= 5 && (strategy == 1 || (match_length == 3 && strstart - match_start > 4096)))
					{
						match_length = 2;
					}
				}
				if (prev_length >= 3 && match_length <= prev_length)
				{
					int num2 = strstart + lookahead - 3;
					bool flag = _tr_tally(strstart - 1 - prev_match, prev_length - 3);
					lookahead -= prev_length - 1;
					prev_length -= 2;
					do
					{
						if (++strstart <= num2)
						{
							ins_h = ((ins_h << hash_shift) ^ (window[strstart + 2] & 0xFF)) & hash_mask;
							num = head[ins_h] & 0xFFFF;
							prev[strstart & w_mask] = head[ins_h];
							head[ins_h] = (short)strstart;
						}
					}
					while (--prev_length != 0);
					match_available = 0;
					match_length = 2;
					strstart++;
					if (flag)
					{
						flush_block_only(eof: false);
						if (strm.avail_out == 0)
						{
							return 0;
						}
					}
				}
				else if (match_available != 0)
				{
					if (_tr_tally(0, window[strstart - 1] & 0xFF))
					{
						flush_block_only(eof: false);
					}
					strstart++;
					lookahead--;
					if (strm.avail_out == 0)
					{
						return 0;
					}
				}
				else
				{
					match_available = 1;
					strstart++;
					lookahead--;
				}
			}
			if (match_available != 0)
			{
				bool flag = _tr_tally(0, window[strstart - 1] & 0xFF);
				match_available = 0;
			}
			flush_block_only(flush == 4);
			if (strm.avail_out == 0)
			{
				if (flush == 4)
				{
					return 2;
				}
				return 0;
			}
			if (flush != 4)
			{
				return 1;
			}
			return 3;
		}

		internal int longest_match(int cur_match)
		{
			int num = max_chain_length;
			int num2 = strstart;
			int num3 = prev_length;
			int num4 = ((strstart > w_size - MIN_LOOKAHEAD) ? (strstart - (w_size - MIN_LOOKAHEAD)) : 0);
			int num5 = nice_match;
			int num6 = w_mask;
			int num7 = strstart + 258;
			byte b = window[num2 + num3 - 1];
			byte b2 = window[num2 + num3];
			if (prev_length >= good_match)
			{
				num >>= 2;
			}
			if (num5 > lookahead)
			{
				num5 = lookahead;
			}
			do
			{
				int num8 = cur_match;
				if (window[num8 + num3] != b2 || window[num8 + num3 - 1] != b || window[num8] != window[num2] || window[++num8] != window[num2 + 1])
				{
					continue;
				}
				num2 += 2;
				num8++;
				while (window[++num2] == window[++num8] && window[++num2] == window[++num8] && window[++num2] == window[++num8] && window[++num2] == window[++num8] && window[++num2] == window[++num8] && window[++num2] == window[++num8] && window[++num2] == window[++num8] && window[++num2] == window[++num8] && num2 < num7)
				{
				}
				int num9 = 258 - (num7 - num2);
				num2 = num7 - 258;
				if (num9 > num3)
				{
					match_start = cur_match;
					num3 = num9;
					if (num9 >= num5)
					{
						break;
					}
					b = window[num2 + num3 - 1];
					b2 = window[num2 + num3];
				}
			}
			while ((cur_match = prev[cur_match & num6] & 0xFFFF) > num4 && --num != 0);
			if (num3 <= lookahead)
			{
				return num3;
			}
			return lookahead;
		}

		internal int deflateInit(ZStream strm, int level, int bits)
		{
			return deflateInit2(strm, level, 8, bits, 8, 0);
		}

		internal int deflateInit(ZStream strm, int level)
		{
			return deflateInit(strm, level, 15);
		}

		internal int deflateInit2(ZStream strm, int level, int method, int windowBits, int memLevel, int strategy)
		{
			int num = 0;
			strm.msg = null;
			if (level == -1)
			{
				level = 6;
			}
			if (windowBits < 0)
			{
				num = 1;
				windowBits = -windowBits;
			}
			if (memLevel < 1 || memLevel > 9 || method != 8 || windowBits < 9 || windowBits > 15 || level < 0 || level > 9 || strategy < 0 || strategy > 2)
			{
				return -2;
			}
			strm.dstate = this;
			noheader = num;
			w_bits = windowBits;
			w_size = 1 << w_bits;
			w_mask = w_size - 1;
			hash_bits = memLevel + 7;
			hash_size = 1 << hash_bits;
			hash_mask = hash_size - 1;
			hash_shift = (hash_bits + 3 - 1) / 3;
			window = new byte[w_size * 2];
			prev = new short[w_size];
			head = new short[hash_size];
			lit_bufsize = 1 << memLevel + 6;
			pending_buf = new byte[lit_bufsize * 4];
			pending_buf_size = lit_bufsize * 4;
			d_buf = lit_bufsize / 2;
			l_buf = 3 * lit_bufsize;
			this.level = level;
			this.strategy = strategy;
			this.method = (byte)method;
			return deflateReset(strm);
		}

		internal int deflateReset(ZStream strm)
		{
			strm.total_in = (strm.total_out = 0L);
			strm.msg = null;
			strm.data_type = 2;
			pending = 0;
			pending_out = 0;
			if (noheader < 0)
			{
				noheader = 0;
			}
			status = ((noheader != 0) ? 113 : 42);
			strm.adler = strm._adler.adler32(0L, null, 0, 0);
			last_flush = 0;
			tr_init();
			lm_init();
			return 0;
		}

		internal int deflateEnd()
		{
			if (status != 42 && status != 113 && status != 666)
			{
				return -2;
			}
			pending_buf = null;
			head = null;
			prev = null;
			window = null;
			if (status != 113)
			{
				return 0;
			}
			return -3;
		}

		internal int deflateParams(ZStream strm, int _level, int _strategy)
		{
			int result = 0;
			if (_level == -1)
			{
				_level = 6;
			}
			if (_level < 0 || _level > 9 || _strategy < 0 || _strategy > 2)
			{
				return -2;
			}
			if (config_table[level].func != config_table[_level].func && strm.total_in != 0)
			{
				result = strm.deflate(1);
			}
			if (level != _level)
			{
				level = _level;
				max_lazy_match = config_table[level].max_lazy;
				good_match = config_table[level].good_length;
				nice_match = config_table[level].nice_length;
				max_chain_length = config_table[level].max_chain;
			}
			strategy = _strategy;
			return result;
		}

		internal int deflateSetDictionary(ZStream strm, byte[] dictionary, int dictLength)
		{
			int num = dictLength;
			int sourceIndex = 0;
			if (dictionary == null || status != 42)
			{
				return -2;
			}
			strm.adler = strm._adler.adler32(strm.adler, dictionary, 0, dictLength);
			if (num < 3)
			{
				return 0;
			}
			if (num > w_size - MIN_LOOKAHEAD)
			{
				num = w_size - MIN_LOOKAHEAD;
				sourceIndex = dictLength - num;
			}
			Array.Copy(dictionary, sourceIndex, window, 0, num);
			strstart = num;
			block_start = num;
			ins_h = window[0] & 0xFF;
			ins_h = ((ins_h << hash_shift) ^ (window[1] & 0xFF)) & hash_mask;
			for (int i = 0; i <= num - 3; i++)
			{
				ins_h = ((ins_h << hash_shift) ^ (window[i + 2] & 0xFF)) & hash_mask;
				prev[i & w_mask] = head[ins_h];
				head[ins_h] = (short)i;
			}
			return 0;
		}

		internal int deflate(ZStream strm, int flush)
		{
			if (flush > 4 || flush < 0)
			{
				return -2;
			}
			if (strm.next_out == null || (strm.next_in == null && strm.avail_in != 0) || (status == 666 && flush != 4))
			{
				strm.msg = z_errmsg[4];
				return -2;
			}
			if (strm.avail_out == 0)
			{
				strm.msg = z_errmsg[7];
				return -5;
			}
			this.strm = strm;
			int num = last_flush;
			last_flush = flush;
			if (status == 42)
			{
				int num2 = 8 + (w_bits - 8 << 4) << 8;
				int num3 = ((level - 1) & 0xFF) >> 1;
				if (num3 > 3)
				{
					num3 = 3;
				}
				num2 |= num3 << 6;
				if (strstart != 0)
				{
					num2 |= 0x20;
				}
				num2 += 31 - num2 % 31;
				status = 113;
				putShortMSB(num2);
				if (strstart != 0)
				{
					putShortMSB((int)SupportClass.URShift(strm.adler, 16));
					putShortMSB((int)(strm.adler & 0xFFFF));
				}
				strm.adler = strm._adler.adler32(0L, null, 0, 0);
			}
			if (pending != 0)
			{
				strm.flush_pending();
				if (strm.avail_out == 0)
				{
					last_flush = -1;
					return 0;
				}
			}
			else if (strm.avail_in == 0 && flush <= num && flush != 4)
			{
				strm.msg = z_errmsg[7];
				return -5;
			}
			if (status == 666 && strm.avail_in != 0)
			{
				strm.msg = z_errmsg[7];
				return -5;
			}
			if (strm.avail_in != 0 || lookahead != 0 || (flush != 0 && status != 666))
			{
				int num4 = -1;
				switch (config_table[level].func)
				{
				case 0:
					num4 = deflate_stored(flush);
					break;
				case 1:
					num4 = deflate_fast(flush);
					break;
				case 2:
					num4 = deflate_slow(flush);
					break;
				}
				if (num4 == 2 || num4 == 3)
				{
					status = 666;
				}
				switch (num4)
				{
				case 0:
				case 2:
					if (strm.avail_out == 0)
					{
						last_flush = -1;
					}
					return 0;
				case 1:
					if (flush == 1)
					{
						_tr_align();
					}
					else
					{
						_tr_stored_block(0, 0, eof: false);
						if (flush == 3)
						{
							for (int i = 0; i < hash_size; i++)
							{
								head[i] = 0;
							}
						}
					}
					strm.flush_pending();
					if (strm.avail_out == 0)
					{
						last_flush = -1;
						return 0;
					}
					break;
				}
			}
			if (flush != 4)
			{
				return 0;
			}
			if (noheader != 0)
			{
				return 1;
			}
			putShortMSB((int)SupportClass.URShift(strm.adler, 16));
			putShortMSB((int)(strm.adler & 0xFFFF));
			strm.flush_pending();
			noheader = -1;
			if (pending == 0)
			{
				return 1;
			}
			return 0;
		}

		static Deflate()
		{
			z_errmsg = new string[10] { "need dictionary", "stream end", "", "file error", "stream error", "data error", "insufficient memory", "buffer error", "incompatible version", "" };
			MIN_LOOKAHEAD = 262;
			L_CODES = 286;
			HEAP_SIZE = 2 * L_CODES + 1;
			config_table = new Config[10];
			config_table[0] = new Config(0, 0, 0, 0, 0);
			config_table[1] = new Config(4, 4, 8, 4, 1);
			config_table[2] = new Config(4, 5, 16, 8, 1);
			config_table[3] = new Config(4, 6, 32, 32, 1);
			config_table[4] = new Config(4, 4, 16, 16, 2);
			config_table[5] = new Config(8, 16, 32, 32, 2);
			config_table[6] = new Config(8, 16, 128, 128, 2);
			config_table[7] = new Config(8, 32, 128, 256, 2);
			config_table[8] = new Config(32, 128, 258, 1024, 2);
			config_table[9] = new Config(32, 258, 258, 4096, 2);
		}
	}
	internal sealed class InfBlocks
	{
		private const int MANY = 1440;

		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		private const int TYPE = 0;

		private const int LENS = 1;

		private const int STORED = 2;

		private const int TABLE = 3;

		private const int BTREE = 4;

		private const int DTREE = 5;

		private const int CODES = 6;

		private const int DRY = 7;

		private const int DONE = 8;

		private const int BAD = 9;

		private static readonly int[] inflate_mask = new int[17]
		{
			0, 1, 3, 7, 15, 31, 63, 127, 255, 511,
			1023, 2047, 4095, 8191, 16383, 32767, 65535
		};

		internal static readonly int[] border = new int[19]
		{
			16, 17, 18, 0, 8, 7, 9, 6, 10, 5,
			11, 4, 12, 3, 13, 2, 14, 1, 15
		};

		internal int mode;

		internal int left;

		internal int table;

		internal int index;

		internal int[] blens;

		internal int[] bb = new int[1];

		internal int[] tb = new int[1];

		internal InfCodes codes;

		internal int last;

		internal int bitk;

		internal int bitb;

		internal int[] hufts;

		internal byte[] window;

		internal int end;

		internal int read;

		internal int write;

		internal object checkfn;

		internal long check;

		internal InfBlocks(ZStream z, object checkfn, int w)
		{
			hufts = new int[4320];
			window = new byte[w];
			end = w;
			this.checkfn = checkfn;
			mode = 0;
			reset(z, null);
		}

		internal void reset(ZStream z, long[] c)
		{
			if (c != null)
			{
				c[0] = check;
			}
			if (mode == 4 || mode == 5)
			{
				blens = null;
			}
			if (mode == 6)
			{
				codes.free(z);
			}
			mode = 0;
			bitk = 0;
			bitb = 0;
			read = (write = 0);
			if (checkfn != null)
			{
				z.adler = (check = z._adler.adler32(0L, null, 0, 0));
			}
		}

		internal int proc(ZStream z, int r)
		{
			int num = z.next_in_index;
			int num2 = z.avail_in;
			int num3 = bitb;
			int i = bitk;
			int num4 = write;
			int num5 = ((num4 < read) ? (read - num4 - 1) : (end - num4));
			while (true)
			{
				switch (mode)
				{
				case 0:
				{
					for (; i < 3; i += 8)
					{
						if (num2 != 0)
						{
							r = 0;
							num2--;
							num3 |= (z.next_in[num++] & 0xFF) << i;
							continue;
						}
						bitb = num3;
						bitk = i;
						z.avail_in = num2;
						z.total_in += num - z.next_in_index;
						z.next_in_index = num;
						write = num4;
						return inflate_flush(z, r);
					}
					int num6 = num3 & 7;
					last = num6 & 1;
					switch (SupportClass.URShift(num6, 1))
					{
					case 0:
						num3 = SupportClass.URShift(num3, 3);
						i -= 3;
						num6 = i & 7;
						num3 = SupportClass.URShift(num3, num6);
						i -= num6;
						mode = 1;
						break;
					case 1:
					{
						int[] array5 = new int[1];
						int[] array6 = new int[1];
						int[][] array7 = new int[1][];
						int[][] array8 = new int[1][];
						InfTree.inflate_trees_fixed(array5, array6, array7, array8, z);
						codes = new InfCodes(array5[0], array6[0], array7[0], array8[0], z);
						num3 = SupportClass.URShift(num3, 3);
						i -= 3;
						mode = 6;
						break;
					}
					case 2:
						num3 = SupportClass.URShift(num3, 3);
						i -= 3;
						mode = 3;
						break;
					case 3:
						num3 = SupportClass.URShift(num3, 3);
						i -= 3;
						mode = 9;
						z.msg = "invalid block type";
						r = -3;
						bitb = num3;
						bitk = i;
						z.avail_in = num2;
						z.total_in += num - z.next_in_index;
						z.next_in_index = num;
						write = num4;
						return inflate_flush(z, r);
					}
					break;
				}
				case 1:
					for (; i < 32; i += 8)
					{
						if (num2 != 0)
						{
							r = 0;
							num2--;
							num3 |= (z.next_in[num++] & 0xFF) << i;
							continue;
						}
						bitb = num3;
						bitk = i;
						z.avail_in = num2;
						z.total_in += num - z.next_in_index;
						z.next_in_index = num;
						write = num4;
						return inflate_flush(z, r);
					}
					if ((SupportClass.URShift(~num3, 16) & 0xFFFF) != (num3 & 0xFFFF))
					{
						mode = 9;
						z.msg = "invalid stored block lengths";
						r = -3;
						bitb = num3;
						bitk = i;
						z.avail_in = num2;
						z.total_in += num - z.next_in_index;
						z.next_in_index = num;
						write = num4;
						return inflate_flush(z, r);
					}
					left = num3 & 0xFFFF;
					num3 = (i = 0);
					mode = ((left != 0) ? 2 : ((last != 0) ? 7 : 0));
					break;
				case 2:
				{
					if (num2 == 0)
					{
						bitb = num3;
						bitk = i;
						z.avail_in = num2;
						z.total_in += num - z.next_in_index;
						z.next_in_index = num;
						write = num4;
						return inflate_flush(z, r);
					}
					if (num5 == 0)
					{
						if (num4 == end && read != 0)
						{
							num4 = 0;
							num5 = ((num4 < read) ? (read - num4 - 1) : (end - num4));
						}
						if (num5 == 0)
						{
							write = num4;
							r = inflate_flush(z, r);
							num4 = write;
							num5 = ((num4 < read) ? (read - num4 - 1) : (end - num4));
							if (num4 == end && read != 0)
							{
								num4 = 0;
								num5 = ((num4 < read) ? (read - num4 - 1) : (end - num4));
							}
							if (num5 == 0)
							{
								bitb = num3;
								bitk = i;
								z.avail_in = num2;
								z.total_in += num - z.next_in_index;
								z.next_in_index = num;
								write = num4;
								return inflate_flush(z, r);
							}
						}
					}
					r = 0;
					int num6 = left;
					if (num6 > num2)
					{
						num6 = num2;
					}
					if (num6 > num5)
					{
						num6 = num5;
					}
					Array.Copy(z.next_in, num, window, num4, num6);
					num += num6;
					num2 -= num6;
					num4 += num6;
					num5 -= num6;
					if ((left -= num6) == 0)
					{
						mode = ((last != 0) ? 7 : 0);
					}
					break;
				}
				case 3:
				{
					for (; i < 14; i += 8)
					{
						if (num2 != 0)
						{
							r = 0;
							num2--;
							num3 |= (z.next_in[num++] & 0xFF) << i;
							continue;
						}
						bitb = num3;
						bitk = i;
						z.avail_in = num2;
						z.total_in += num - z.next_in_index;
						z.next_in_index = num;
						write = num4;
						return inflate_flush(z, r);
					}
					int num6 = (table = num3 & 0x3FFF);
					if ((num6 & 0x1F) > 29 || ((num6 >> 5) & 0x1F) > 29)
					{
						mode = 9;
						z.msg = "too many length or distance symbols";
						r = -3;
						bitb = num3;
						bitk = i;
						z.avail_in = num2;
						z.total_in += num - z.next_in_index;
						z.next_in_index = num;
						write = num4;
						return inflate_flush(z, r);
					}
					num6 = 258 + (num6 & 0x1F) + ((num6 >> 5) & 0x1F);
					blens = new int[num6];
					num3 = SupportClass.URShift(num3, 14);
					i -= 14;
					index = 0;
					mode = 4;
					goto case 4;
				}
				case 4:
				{
					while (index < 4 + SupportClass.URShift(table, 10))
					{
						for (; i < 3; i += 8)
						{
							if (num2 != 0)
							{
								r = 0;
								num2--;
								num3 |= (z.next_in[num++] & 0xFF) << i;
								continue;
							}
							bitb = num3;
							bitk = i;
							z.avail_in = num2;
							z.total_in += num - z.next_in_index;
							z.next_in_index = num;
							write = num4;
							return inflate_flush(z, r);
						}
						blens[border[index++]] = num3 & 7;
						num3 = SupportClass.URShift(num3, 3);
						i -= 3;
					}
					while (index < 19)
					{
						blens[border[index++]] = 0;
					}
					bb[0] = 7;
					int num6 = InfTree.inflate_trees_bits(blens, bb, tb, hufts, z);
					if (num6 != 0)
					{
						r = num6;
						if (r == -3)
						{
							blens = null;
							mode = 9;
						}
						bitb = num3;
						bitk = i;
						z.avail_in = num2;
						z.total_in += num - z.next_in_index;
						z.next_in_index = num;
						write = num4;
						return inflate_flush(z, r);
					}
					index = 0;
					mode = 5;
					goto case 5;
				}
				case 5:
				{
					int num6;
					while (true)
					{
						num6 = table;
						if (index >= 258 + (num6 & 0x1F) + ((num6 >> 5) & 0x1F))
						{
							break;
						}
						for (num6 = bb[0]; i < num6; i += 8)
						{
							if (num2 != 0)
							{
								r = 0;
								num2--;
								num3 |= (z.next_in[num++] & 0xFF) << i;
								continue;
							}
							bitb = num3;
							bitk = i;
							z.avail_in = num2;
							z.total_in += num - z.next_in_index;
							z.next_in_index = num;
							write = num4;
							return inflate_flush(z, r);
						}
						_ = tb[0];
						_ = -1;
						num6 = hufts[(tb[0] + (num3 & inflate_mask[num6])) * 3 + 1];
						int num7 = hufts[(tb[0] + (num3 & inflate_mask[num6])) * 3 + 2];
						if (num7 < 16)
						{
							num3 = SupportClass.URShift(num3, num6);
							i -= num6;
							blens[index++] = num7;
							continue;
						}
						int num8 = ((num7 == 18) ? 7 : (num7 - 14));
						int num9 = ((num7 == 18) ? 11 : 3);
						for (; i < num6 + num8; i += 8)
						{
							if (num2 != 0)
							{
								r = 0;
								num2--;
								num3 |= (z.next_in[num++] & 0xFF) << i;
								continue;
							}
							bitb = num3;
							bitk = i;
							z.avail_in = num2;
							z.total_in += num - z.next_in_index;
							z.next_in_index = num;
							write = num4;
							return inflate_flush(z, r);
						}
						num3 = SupportClass.URShift(num3, num6);
						i -= num6;
						num9 += num3 & inflate_mask[num8];
						num3 = SupportClass.URShift(num3, num8);
						i -= num8;
						num8 = index;
						num6 = table;
						if (num8 + num9 > 258 + (num6 & 0x1F) + ((num6 >> 5) & 0x1F) || (num7 == 16 && num8 < 1))
						{
							blens = null;
							mode = 9;
							z.msg = "invalid bit length repeat";
							r = -3;
							bitb = num3;
							bitk = i;
							z.avail_in = num2;
							z.total_in += num - z.next_in_index;
							z.next_in_index = num;
							write = num4;
							return inflate_flush(z, r);
						}
						num7 = ((num7 == 16) ? blens[num8 - 1] : 0);
						do
						{
							blens[num8++] = num7;
						}
						while (--num9 != 0);
						index = num8;
					}
					tb[0] = -1;
					int[] array = new int[1];
					int[] array2 = new int[1];
					int[] array3 = new int[1];
					int[] array4 = new int[1];
					array[0] = 9;
					array2[0] = 6;
					num6 = table;
					num6 = InfTree.inflate_trees_dynamic(257 + (num6 & 0x1F), 1 + ((num6 >> 5) & 0x1F), blens, array, array2, array3, array4, hufts, z);
					if (num6 != 0)
					{
						if (num6 == -3)
						{
							blens = null;
							mode = 9;
						}
						r = num6;
						bitb = num3;
						bitk = i;
						z.avail_in = num2;
						z.total_in += num - z.next_in_index;
						z.next_in_index = num;
						write = num4;
						return inflate_flush(z, r);
					}
					codes = new InfCodes(array[0], array2[0], hufts, array3[0], hufts, array4[0], z);
					blens = null;
					mode = 6;
					goto case 6;
				}
				case 6:
					bitb = num3;
					bitk = i;
					z.avail_in = num2;
					z.total_in += num - z.next_in_index;
					z.next_in_index = num;
					write = num4;
					if ((r = codes.proc(this, z, r)) != 1)
					{
						return inflate_flush(z, r);
					}
					r = 0;
					codes.free(z);
					num = z.next_in_index;
					num2 = z.avail_in;
					num3 = bitb;
					i = bitk;
					num4 = write;
					num5 = ((num4 < read) ? (read - num4 - 1) : (end - num4));
					if (last == 0)
					{
						mode = 0;
						break;
					}
					mode = 7;
					goto case 7;
				case 7:
					write = num4;
					r = inflate_flush(z, r);
					num4 = write;
					num5 = ((num4 < read) ? (read - num4 - 1) : (end - num4));
					if (read != write)
					{
						bitb = num3;
						bitk = i;
						z.avail_in = num2;
						z.total_in += num - z.next_in_index;
						z.next_in_index = num;
						write = num4;
						return inflate_flush(z, r);
					}
					mode = 8;
					goto case 8;
				case 8:
					r = 1;
					bitb = num3;
					bitk = i;
					z.avail_in = num2;
					z.total_in += num - z.next_in_index;
					z.next_in_index = num;
					write = num4;
					return inflate_flush(z, r);
				case 9:
					r = -3;
					bitb = num3;
					bitk = i;
					z.avail_in = num2;
					z.total_in += num - z.next_in_index;
					z.next_in_index = num;
					write = num4;
					return inflate_flush(z, r);
				default:
					r = -2;
					bitb = num3;
					bitk = i;
					z.avail_in = num2;
					z.total_in += num - z.next_in_index;
					z.next_in_index = num;
					write = num4;
					return inflate_flush(z, r);
				}
			}
		}

		internal void free(ZStream z)
		{
			reset(z, null);
			window = null;
			hufts = null;
		}

		internal void set_dictionary(byte[] d, int start, int n)
		{
			Array.Copy(d, start, window, 0, n);
			read = (write = n);
		}

		internal int sync_point()
		{
			if (mode != 1)
			{
				return 0;
			}
			return 1;
		}

		internal int inflate_flush(ZStream z, int r)
		{
			int next_out_index = z.next_out_index;
			int num = read;
			int num2 = ((num <= write) ? write : end) - num;
			if (num2 > z.avail_out)
			{
				num2 = z.avail_out;
			}
			if (num2 != 0 && r == -5)
			{
				r = 0;
			}
			z.avail_out -= num2;
			z.total_out += num2;
			if (checkfn != null)
			{
				z.adler = (check = z._adler.adler32(check, window, num, num2));
			}
			Array.Copy(window, num, z.next_out, next_out_index, num2);
			next_out_index += num2;
			num += num2;
			if (num == end)
			{
				num = 0;
				if (write == end)
				{
					write = 0;
				}
				num2 = write - num;
				if (num2 > z.avail_out)
				{
					num2 = z.avail_out;
				}
				if (num2 != 0 && r == -5)
				{
					r = 0;
				}
				z.avail_out -= num2;
				z.total_out += num2;
				if (checkfn != null)
				{
					z.adler = (check = z._adler.adler32(check, window, num, num2));
				}
				Array.Copy(window, num, z.next_out, next_out_index, num2);
				next_out_index += num2;
				num += num2;
			}
			z.next_out_index = next_out_index;
			read = num;
			return r;
		}
	}
	internal sealed class InfCodes
	{
		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		private const int START = 0;

		private const int LEN = 1;

		private const int LENEXT = 2;

		private const int DIST = 3;

		private const int DISTEXT = 4;

		private const int COPY = 5;

		private const int LIT = 6;

		private const int WASH = 7;

		private const int END = 8;

		private const int BADCODE = 9;

		private static readonly int[] inflate_mask = new int[17]
		{
			0, 1, 3, 7, 15, 31, 63, 127, 255, 511,
			1023, 2047, 4095, 8191, 16383, 32767, 65535
		};

		internal int mode;

		internal int len;

		internal int[] tree;

		internal int tree_index;

		internal int need;

		internal int lit;

		internal int get_Renamed;

		internal int dist;

		internal byte lbits;

		internal byte dbits;

		internal int[] ltree;

		internal int ltree_index;

		internal int[] dtree;

		internal int dtree_index;

		internal InfCodes(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index, ZStream z)
		{
			mode = 0;
			lbits = (byte)bl;
			dbits = (byte)bd;
			ltree = tl;
			ltree_index = tl_index;
			dtree = td;
			dtree_index = td_index;
		}

		internal InfCodes(int bl, int bd, int[] tl, int[] td, ZStream z)
		{
			mode = 0;
			lbits = (byte)bl;
			dbits = (byte)bd;
			ltree = tl;
			ltree_index = 0;
			dtree = td;
			dtree_index = 0;
		}

		internal int proc(InfBlocks s, ZStream z, int r)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			num3 = z.next_in_index;
			int num4 = z.avail_in;
			num = s.bitb;
			num2 = s.bitk;
			int num5 = s.write;
			int num6 = ((num5 < s.read) ? (s.read - num5 - 1) : (s.end - num5));
			while (true)
			{
				switch (mode)
				{
				case 0:
					if (num6 >= 258 && num4 >= 10)
					{
						s.bitb = num;
						s.bitk = num2;
						z.avail_in = num4;
						z.total_in += num3 - z.next_in_index;
						z.next_in_index = num3;
						s.write = num5;
						r = inflate_fast(lbits, dbits, ltree, ltree_index, dtree, dtree_index, s, z);
						num3 = z.next_in_index;
						num4 = z.avail_in;
						num = s.bitb;
						num2 = s.bitk;
						num5 = s.write;
						num6 = ((num5 < s.read) ? (s.read - num5 - 1) : (s.end - num5));
						if (r != 0)
						{
							mode = ((r == 1) ? 7 : 9);
							break;
						}
					}
					need = lbits;
					tree = ltree;
					tree_index = ltree_index;
					mode = 1;
					goto case 1;
				case 1:
				{
					int num7;
					for (num7 = need; num2 < num7; num2 += 8)
					{
						if (num4 != 0)
						{
							r = 0;
							num4--;
							num |= (z.next_in[num3++] & 0xFF) << num2;
							continue;
						}
						s.bitb = num;
						s.bitk = num2;
						z.avail_in = num4;
						z.total_in += num3 - z.next_in_index;
						z.next_in_index = num3;
						s.write = num5;
						return s.inflate_flush(z, r);
					}
					int num8 = (tree_index + (num & inflate_mask[num7])) * 3;
					num = SupportClass.URShift(num, tree[num8 + 1]);
					num2 -= tree[num8 + 1];
					int num9 = tree[num8];
					if (num9 == 0)
					{
						lit = tree[num8 + 2];
						mode = 6;
						break;
					}
					if ((num9 & 0x10) != 0)
					{
						get_Renamed = num9 & 0xF;
						len = tree[num8 + 2];
						mode = 2;
						break;
					}
					if ((num9 & 0x40) == 0)
					{
						need = num9;
						tree_index = num8 / 3 + tree[num8 + 2];
						break;
					}
					if ((num9 & 0x20) != 0)
					{
						mode = 7;
						break;
					}
					mode = 9;
					z.msg = "invalid literal/length code";
					r = -3;
					s.bitb = num;
					s.bitk = num2;
					z.avail_in = num4;
					z.total_in += num3 - z.next_in_index;
					z.next_in_index = num3;
					s.write = num5;
					return s.inflate_flush(z, r);
				}
				case 2:
				{
					int num7;
					for (num7 = get_Renamed; num2 < num7; num2 += 8)
					{
						if (num4 != 0)
						{
							r = 0;
							num4--;
							num |= (z.next_in[num3++] & 0xFF) << num2;
							continue;
						}
						s.bitb = num;
						s.bitk = num2;
						z.avail_in = num4;
						z.total_in += num3 - z.next_in_index;
						z.next_in_index = num3;
						s.write = num5;
						return s.inflate_flush(z, r);
					}
					len += num & inflate_mask[num7];
					num >>= num7;
					num2 -= num7;
					need = dbits;
					tree = dtree;
					tree_index = dtree_index;
					mode = 3;
					goto case 3;
				}
				case 3:
				{
					int num7;
					for (num7 = need; num2 < num7; num2 += 8)
					{
						if (num4 != 0)
						{
							r = 0;
							num4--;
							num |= (z.next_in[num3++] & 0xFF) << num2;
							continue;
						}
						s.bitb = num;
						s.bitk = num2;
						z.avail_in = num4;
						z.total_in += num3 - z.next_in_index;
						z.next_in_index = num3;
						s.write = num5;
						return s.inflate_flush(z, r);
					}
					int num8 = (tree_index + (num & inflate_mask[num7])) * 3;
					num >>= tree[num8 + 1];
					num2 -= tree[num8 + 1];
					int num9 = tree[num8];
					if ((num9 & 0x10) != 0)
					{
						get_Renamed = num9 & 0xF;
						dist = tree[num8 + 2];
						mode = 4;
						break;
					}
					if ((num9 & 0x40) == 0)
					{
						need = num9;
						tree_index = num8 / 3 + tree[num8 + 2];
						break;
					}
					mode = 9;
					z.msg = "invalid distance code";
					r = -3;
					s.bitb = num;
					s.bitk = num2;
					z.avail_in = num4;
					z.total_in += num3 - z.next_in_index;
					z.next_in_index = num3;
					s.write = num5;
					return s.inflate_flush(z, r);
				}
				case 4:
				{
					int num7;
					for (num7 = get_Renamed; num2 < num7; num2 += 8)
					{
						if (num4 != 0)
						{
							r = 0;
							num4--;
							num |= (z.next_in[num3++] & 0xFF) << num2;
							continue;
						}
						s.bitb = num;
						s.bitk = num2;
						z.avail_in = num4;
						z.total_in += num3 - z.next_in_index;
						z.next_in_index = num3;
						s.write = num5;
						return s.inflate_flush(z, r);
					}
					dist += num & inflate_mask[num7];
					num >>= num7;
					num2 -= num7;
					mode = 5;
					goto case 5;
				}
				case 5:
				{
					int i;
					for (i = num5 - dist; i < 0; i += s.end)
					{
					}
					while (len != 0)
					{
						if (num6 == 0)
						{
							if (num5 == s.end && s.read != 0)
							{
								num5 = 0;
								num6 = ((num5 < s.read) ? (s.read - num5 - 1) : (s.end - num5));
							}
							if (num6 == 0)
							{
								s.write = num5;
								r = s.inflate_flush(z, r);
								num5 = s.write;
								num6 = ((num5 < s.read) ? (s.read - num5 - 1) : (s.end - num5));
								if (num5 == s.end && s.read != 0)
								{
									num5 = 0;
									num6 = ((num5 < s.read) ? (s.read - num5 - 1) : (s.end - num5));
								}
								if (num6 == 0)
								{
									s.bitb = num;
									s.bitk = num2;
									z.avail_in = num4;
									z.total_in += num3 - z.next_in_index;
									z.next_in_index = num3;
									s.write = num5;
									return s.inflate_flush(z, r);
								}
							}
						}
						s.window[num5++] = s.window[i++];
						num6--;
						if (i == s.end)
						{
							i = 0;
						}
						len--;
					}
					mode = 0;
					break;
				}
				case 6:
					if (num6 == 0)
					{
						if (num5 == s.end && s.read != 0)
						{
							num5 = 0;
							num6 = ((num5 < s.read) ? (s.read - num5 - 1) : (s.end - num5));
						}
						if (num6 == 0)
						{
							s.write = num5;
							r = s.inflate_flush(z, r);
							num5 = s.write;
							num6 = ((num5 < s.read) ? (s.read - num5 - 1) : (s.end - num5));
							if (num5 == s.end && s.read != 0)
							{
								num5 = 0;
								num6 = ((num5 < s.read) ? (s.read - num5 - 1) : (s.end - num5));
							}
							if (num6 == 0)
							{
								s.bitb = num;
								s.bitk = num2;
								z.avail_in = num4;
								z.total_in += num3 - z.next_in_index;
								z.next_in_index = num3;
								s.write = num5;
								return s.inflate_flush(z, r);
							}
						}
					}
					r = 0;
					s.window[num5++] = (byte)lit;
					num6--;
					mode = 0;
					break;
				case 7:
					if (num2 > 7)
					{
						num2 -= 8;
						num4++;
						num3--;
					}
					s.write = num5;
					r = s.inflate_flush(z, r);
					num5 = s.write;
					num6 = ((num5 < s.read) ? (s.read - num5 - 1) : (s.end - num5));
					if (s.read != s.write)
					{
						s.bitb = num;
						s.bitk = num2;
						z.avail_in = num4;
						z.total_in += num3 - z.next_in_index;
						z.next_in_index = num3;
						s.write = num5;
						return s.inflate_flush(z, r);
					}
					mode = 8;
					goto case 8;
				case 8:
					r = 1;
					s.bitb = num;
					s.bitk = num2;
					z.avail_in = num4;
					z.total_in += num3 - z.next_in_index;
					z.next_in_index = num3;
					s.write = num5;
					return s.inflate_flush(z, r);
				case 9:
					r = -3;
					s.bitb = num;
					s.bitk = num2;
					z.avail_in = num4;
					z.total_in += num3 - z.next_in_index;
					z.next_in_index = num3;
					s.write = num5;
					return s.inflate_flush(z, r);
				default:
					r = -2;
					s.bitb = num;
					s.bitk = num2;
					z.avail_in = num4;
					z.total_in += num3 - z.next_in_index;
					z.next_in_index = num3;
					s.write = num5;
					return s.inflate_flush(z, r);
				}
			}
		}

		internal void free(ZStream z)
		{
		}

		internal int inflate_fast(int bl, int bd, int[] tl, int tl_index, int[] td, int td_index, InfBlocks s, ZStream z)
		{
			int next_in_index = z.next_in_index;
			int num = z.avail_in;
			int num2 = s.bitb;
			int num3 = s.bitk;
			int num4 = s.write;
			int num5 = ((num4 < s.read) ? (s.read - num4 - 1) : (s.end - num4));
			int num6 = inflate_mask[bl];
			int num7 = inflate_mask[bd];
			int num11;
			while (true)
			{
				if (num3 < 20)
				{
					num--;
					num2 |= (z.next_in[next_in_index++] & 0xFF) << num3;
					num3 += 8;
					continue;
				}
				int num8 = num2 & num6;
				int[] array = tl;
				int num9 = tl_index;
				int num10;
				if ((num10 = array[(num9 + num8) * 3]) == 0)
				{
					num2 >>= array[(num9 + num8) * 3 + 1];
					num3 -= array[(num9 + num8) * 3 + 1];
					s.window[num4++] = (byte)array[(num9 + num8) * 3 + 2];
					num5--;
				}
				else
				{
					while (true)
					{
						num2 >>= array[(num9 + num8) * 3 + 1];
						num3 -= array[(num9 + num8) * 3 + 1];
						if ((num10 & 0x10) != 0)
						{
							num10 &= 0xF;
							num11 = array[(num9 + num8) * 3 + 2] + (num2 & inflate_mask[num10]);
							num2 >>= num10;
							for (num3 -= num10; num3 < 15; num3 += 8)
							{
								num--;
								num2 |= (z.next_in[next_in_index++] & 0xFF) << num3;
							}
							num8 = num2 & num7;
							array = td;
							num9 = td_index;
							num10 = array[(num9 + num8) * 3];
							while (true)
							{
								num2 >>= array[(num9 + num8) * 3 + 1];
								num3 -= array[(num9 + num8) * 3 + 1];
								if ((num10 & 0x10) != 0)
								{
									break;
								}
								if ((num10 & 0x40) == 0)
								{
									num8 += array[(num9 + num8) * 3 + 2];
									num8 += num2 & inflate_mask[num10];
									num10 = array[(num9 + num8) * 3];
									continue;
								}
								z.msg = "invalid distance code";
								num11 = z.avail_in - num;
								num11 = ((num3 >> 3 < num11) ? (num3 >> 3) : num11);
								num += num11;
								next_in_index -= num11;
								num3 -= num11 << 3;
								s.bitb = num2;
								s.bitk = num3;
								z.avail_in = num;
								z.total_in += next_in_index - z.next_in_index;
								z.next_in_index = next_in_index;
								s.write = num4;
								return -3;
							}
							for (num10 &= 0xF; num3 < num10; num3 += 8)
							{
								num--;
								num2 |= (z.next_in[next_in_index++] & 0xFF) << num3;
							}
							int num12 = array[(num9 + num8) * 3 + 2] + (num2 & inflate_mask[num10]);
							num2 >>= num10;
							num3 -= num10;
							num5 -= num11;
							int num13;
							if (num4 >= num12)
							{
								num13 = num4 - num12;
								if (num4 - num13 > 0 && 2 > num4 - num13)
								{
									s.window[num4++] = s.window[num13++];
									num11--;
									s.window[num4++] = s.window[num13++];
									num11--;
								}
								else
								{
									Array.Copy(s.window, num13, s.window, num4, 2);
									num4 += 2;
									num13 += 2;
									num11 -= 2;
								}
							}
							else
							{
								num13 = num4 - num12;
								do
								{
									num13 += s.end;
								}
								while (num13 < 0);
								num10 = s.end - num13;
								if (num11 > num10)
								{
									num11 -= num10;
									if (num4 - num13 > 0 && num10 > num4 - num13)
									{
										do
										{
											s.window[num4++] = s.window[num13++];
										}
										while (--num10 != 0);
									}
									else
									{
										Array.Copy(s.window, num13, s.window, num4, num10);
										num4 += num10;
										num13 += num10;
										num10 = 0;
									}
									num13 = 0;
								}
							}
							if (num4 - num13 > 0 && num11 > num4 - num13)
							{
								do
								{
									s.window[num4++] = s.window[num13++];
								}
								while (--num11 != 0);
								break;
							}
							Array.Copy(s.window, num13, s.window, num4, num11);
							num4 += num11;
							num13 += num11;
							num11 = 0;
							break;
						}
						if ((num10 & 0x40) == 0)
						{
							num8 += array[(num9 + num8) * 3 + 2];
							num8 += num2 & inflate_mask[num10];
							if ((num10 = array[(num9 + num8) * 3]) == 0)
							{
								num2 >>= array[(num9 + num8) * 3 + 1];
								num3 -= array[(num9 + num8) * 3 + 1];
								s.window[num4++] = (byte)array[(num9 + num8) * 3 + 2];
								num5--;
								break;
							}
							continue;
						}
						if ((num10 & 0x20) != 0)
						{
							num11 = z.avail_in - num;
							num11 = ((num3 >> 3 < num11) ? (num3 >> 3) : num11);
							num += num11;
							next_in_index -= num11;
							num3 -= num11 << 3;
							s.bitb = num2;
							s.bitk = num3;
							z.avail_in = num;
							z.total_in += next_in_index - z.next_in_index;
							z.next_in_index = next_in_index;
							s.write = num4;
							return 1;
						}
						z.msg = "invalid literal/length code";
						num11 = z.avail_in - num;
						num11 = ((num3 >> 3 < num11) ? (num3 >> 3) : num11);
						num += num11;
						next_in_index -= num11;
						num3 -= num11 << 3;
						s.bitb = num2;
						s.bitk = num3;
						z.avail_in = num;
						z.total_in += next_in_index - z.next_in_index;
						z.next_in_index = next_in_index;
						s.write = num4;
						return -3;
					}
				}
				if (num5 < 258 || num < 10)
				{
					break;
				}
			}
			num11 = z.avail_in - num;
			num11 = ((num3 >> 3 < num11) ? (num3 >> 3) : num11);
			num += num11;
			next_in_index -= num11;
			num3 -= num11 << 3;
			s.bitb = num2;
			s.bitk = num3;
			z.avail_in = num;
			z.total_in += next_in_index - z.next_in_index;
			z.next_in_index = next_in_index;
			s.write = num4;
			return 0;
		}
	}
	internal sealed class Inflate
	{
		private const int MAX_WBITS = 15;

		private const int PRESET_DICT = 32;

		internal const int Z_NO_FLUSH = 0;

		internal const int Z_PARTIAL_FLUSH = 1;

		internal const int Z_SYNC_FLUSH = 2;

		internal const int Z_FULL_FLUSH = 3;

		internal const int Z_FINISH = 4;

		private const int Z_DEFLATED = 8;

		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		private const int METHOD = 0;

		private const int FLAG = 1;

		private const int DICT4 = 2;

		private const int DICT3 = 3;

		private const int DICT2 = 4;

		private const int DICT1 = 5;

		private const int DICT0 = 6;

		private const int BLOCKS = 7;

		private const int CHECK4 = 8;

		private const int CHECK3 = 9;

		private const int CHECK2 = 10;

		private const int CHECK1 = 11;

		private const int DONE = 12;

		private const int BAD = 13;

		internal int mode;

		internal int method;

		internal long[] was = new long[1];

		internal long need;

		internal int marker;

		internal int nowrap;

		internal int wbits;

		internal InfBlocks blocks;

		private static byte[] mark = new byte[4]
		{
			0,
			0,
			(byte)SupportClass.Identity(255L),
			(byte)SupportClass.Identity(255L)
		};

		internal int inflateReset(ZStream z)
		{
			if (z == null || z.istate == null)
			{
				return -2;
			}
			z.total_in = (z.total_out = 0L);
			z.msg = null;
			z.istate.mode = ((z.istate.nowrap != 0) ? 7 : 0);
			z.istate.blocks.reset(z, null);
			return 0;
		}

		internal int inflateEnd(ZStream z)
		{
			if (blocks != null)
			{
				blocks.free(z);
			}
			blocks = null;
			return 0;
		}

		internal int inflateInit(ZStream z, int w)
		{
			z.msg = null;
			blocks = null;
			nowrap = 0;
			if (w < 0)
			{
				w = -w;
				nowrap = 1;
			}
			if (w < 8 || w > 15)
			{
				inflateEnd(z);
				return -2;
			}
			wbits = w;
			z.istate.blocks = new InfBlocks(z, (z.istate.nowrap != 0) ? null : this, 1 << w);
			inflateReset(z);
			return 0;
		}

		internal int inflate(ZStream z, int f)
		{
			if (z == null || z.istate == null || z.next_in == null)
			{
				return -2;
			}
			f = ((f == 4) ? (-5) : 0);
			int num = -5;
			while (true)
			{
				switch (z.istate.mode)
				{
				case 0:
					if (z.avail_in == 0)
					{
						return num;
					}
					num = f;
					z.avail_in--;
					z.total_in++;
					if (((z.istate.method = z.next_in[z.next_in_index++]) & 0xF) != 8)
					{
						z.istate.mode = 13;
						z.msg = "unknown compression method";
						z.istate.marker = 5;
						break;
					}
					if ((z.istate.method >> 4) + 8 > z.istate.wbits)
					{
						z.istate.mode = 13;
						z.msg = "invalid window size";
						z.istate.marker = 5;
						break;
					}
					z.istate.mode = 1;
					goto case 1;
				case 1:
				{
					if (z.avail_in == 0)
					{
						return num;
					}
					num = f;
					z.avail_in--;
					z.total_in++;
					int num2 = z.next_in[z.next_in_index++] & 0xFF;
					if (((z.istate.method << 8) + num2) % 31 != 0)
					{
						z.istate.mode = 13;
						z.msg = "incorrect header check";
						z.istate.marker = 5;
						break;
					}
					if ((num2 & 0x20) == 0)
					{
						z.istate.mode = 7;
						break;
					}
					z.istate.mode = 2;
					goto case 2;
				}
				case 2:
					if (z.avail_in == 0)
					{
						return num;
					}
					num = f;
					z.avail_in--;
					z.total_in++;
					z.istate.need = ((z.next_in[z.next_in_index++] & 0xFF) << 24) & -16777216;
					z.istate.mode = 3;
					goto case 3;
				case 3:
					if (z.avail_in == 0)
					{
						return num;
					}
					num = f;
					z.avail_in--;
					z.total_in++;
					z.istate.need += (long)((ulong)((z.next_in[z.next_in_index++] & 0xFF) << 16) & 0xFF0000uL);
					z.istate.mode = 4;
					goto case 4;
				case 4:
					if (z.avail_in == 0)
					{
						return num;
					}
					num = f;
					z.avail_in--;
					z.total_in++;
					z.istate.need += (long)((ulong)((z.next_in[z.next_in_index++] & 0xFF) << 8) & 0xFF00uL);
					z.istate.mode = 5;
					goto case 5;
				case 5:
					if (z.avail_in == 0)
					{
						return num;
					}
					num = f;
					z.avail_in--;
					z.total_in++;
					z.istate.need += (long)((ulong)z.next_in[z.next_in_index++] & 0xFFuL);
					z.adler = z.istate.need;
					z.istate.mode = 6;
					return 2;
				case 6:
					z.istate.mode = 13;
					z.msg = "need dictionary";
					z.istate.marker = 0;
					return -2;
				case 7:
					num = z.istate.blocks.proc(z, num);
					switch (num)
					{
					case -3:
						z.istate.mode = 13;
						z.istate.marker = 0;
						goto end_IL_0031;
					case 0:
						num = f;
						break;
					}
					if (num != 1)
					{
						return num;
					}
					num = f;
					z.istate.blocks.reset(z, z.istate.was);
					if (z.istate.nowrap != 0)
					{
						z.istate.mode = 12;
						break;
					}
					z.istate.mode = 8;
					goto case 8;
				case 8:
					if (z.avail_in == 0)
					{
						return num;
					}
					num = f;
					z.avail_in--;
					z.total_in++;
					z.istate.need = ((z.next_in[z.next_in_index++] & 0xFF) << 24) & -16777216;
					z.istate.mode = 9;
					goto case 9;
				case 9:
					if (z.avail_in == 0)
					{
						return num;
					}
					num = f;
					z.avail_in--;
					z.total_in++;
					z.istate.need += (long)((ulong)((z.next_in[z.next_in_index++] & 0xFF) << 16) & 0xFF0000uL);
					z.istate.mode = 10;
					goto case 10;
				case 10:
					if (z.avail_in == 0)
					{
						return num;
					}
					num = f;
					z.avail_in--;
					z.total_in++;
					z.istate.need += (long)((ulong)((z.next_in[z.next_in_index++] & 0xFF) << 8) & 0xFF00uL);
					z.istate.mode = 11;
					goto case 11;
				case 11:
					if (z.avail_in == 0)
					{
						return num;
					}
					num = f;
					z.avail_in--;
					z.total_in++;
					z.istate.need += (long)((ulong)z.next_in[z.next_in_index++] & 0xFFuL);
					if ((int)z.istate.was[0] != (int)z.istate.need)
					{
						z.istate.mode = 13;
						z.msg = "incorrect data check";
						z.istate.marker = 5;
						break;
					}
					z.istate.mode = 12;
					goto case 12;
				case 12:
					return 1;
				case 13:
					return -3;
				default:
					{
						return -2;
					}
					end_IL_0031:
					break;
				}
			}
		}

		internal int inflateSetDictionary(ZStream z, byte[] dictionary, int dictLength)
		{
			int start = 0;
			int num = dictLength;
			if (z == null || z.istate == null || z.istate.mode != 6)
			{
				return -2;
			}
			if (z._adler.adler32(1L, dictionary, 0, dictLength) != z.adler)
			{
				return -3;
			}
			z.adler = z._adler.adler32(0L, null, 0, 0);
			if (num >= 1 << z.istate.wbits)
			{
				num = (1 << z.istate.wbits) - 1;
				start = dictLength - num;
			}
			z.istate.blocks.set_dictionary(dictionary, start, num);
			z.istate.mode = 7;
			return 0;
		}

		internal int inflateSync(ZStream z)
		{
			if (z == null || z.istate == null)
			{
				return -2;
			}
			if (z.istate.mode != 13)
			{
				z.istate.mode = 13;
				z.istate.marker = 0;
			}
			int num;
			if ((num = z.avail_in) == 0)
			{
				return -5;
			}
			int num2 = z.next_in_index;
			int num3 = z.istate.marker;
			while (num != 0 && num3 < 4)
			{
				num3 = ((z.next_in[num2] != mark[num3]) ? ((z.next_in[num2] == 0) ? (4 - num3) : 0) : (num3 + 1));
				num2++;
				num--;
			}
			z.total_in += num2 - z.next_in_index;
			z.next_in_index = num2;
			z.avail_in = num;
			z.istate.marker = num3;
			if (num3 != 4)
			{
				return -3;
			}
			long total_in = z.total_in;
			long total_out = z.total_out;
			inflateReset(z);
			z.total_in = total_in;
			z.total_out = total_out;
			z.istate.mode = 7;
			return 0;
		}

		internal int inflateSyncPoint(ZStream z)
		{
			if (z == null || z.istate == null || z.istate.blocks == null)
			{
				return -2;
			}
			return z.istate.blocks.sync_point();
		}
	}
	internal sealed class InfTree
	{
		private const int MANY = 1440;

		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		internal const int fixed_bl = 9;

		internal const int fixed_bd = 5;

		internal const int BMAX = 15;

		internal static readonly int[] fixed_tl = new int[1536]
		{
			96, 7, 256, 0, 8, 80, 0, 8, 16, 84,
			8, 115, 82, 7, 31, 0, 8, 112, 0, 8,
			48, 0, 9, 192, 80, 7, 10, 0, 8, 96,
			0, 8, 32, 0, 9, 160, 0, 8, 0, 0,
			8, 128, 0, 8, 64, 0, 9, 224, 80, 7,
			6, 0, 8, 88, 0, 8, 24, 0, 9, 144,
			83, 7, 59, 0, 8, 120, 0, 8, 56, 0,
			9, 208, 81, 7, 17, 0, 8, 104, 0, 8,
			40, 0, 9, 176, 0, 8, 8, 0, 8, 136,
			0, 8, 72, 0, 9, 240, 80, 7, 4, 0,
			8, 84, 0, 8, 20, 85, 8, 227, 83, 7,
			43, 0, 8, 116, 0, 8, 52, 0, 9, 200,
			81, 7, 13, 0, 8, 100, 0, 8, 36, 0,
			9, 168, 0, 8, 4, 0, 8, 132, 0, 8,
			68, 0, 9, 232, 80, 7, 8, 0, 8, 92,
			0, 8, 28, 0, 9, 152, 84, 7, 83, 0,
			8, 124, 0, 8, 60, 0, 9, 216, 82, 7,
			23, 0, 8, 108, 0, 8, 44, 0, 9, 184,
			0, 8, 12, 0, 8, 140, 0, 8, 76, 0,
			9, 248, 80, 7, 3, 0, 8, 82, 0, 8,
			18, 85, 8, 163, 83, 7, 35, 0, 8, 114,
			0, 8, 50, 0, 9, 196, 81, 7, 11, 0,
			8, 98, 0, 8, 34, 0, 9, 164, 0, 8,
			2, 0, 8, 130, 0, 8, 66, 0, 9, 228,
			80, 7, 7, 0, 8, 90, 0, 8, 26, 0,
			9, 148, 84, 7, 67, 0, 8, 122, 0, 8,
			58, 0, 9, 212, 82, 7, 19, 0, 8, 106,
			0, 8, 42, 0, 9, 180, 0, 8, 10, 0,
			8, 138, 0, 8, 74, 0, 9, 244, 80, 7,
			5, 0, 8, 86, 0, 8, 22, 192, 8, 0,
			83, 7, 51, 0, 8, 118, 0, 8, 54, 0,
			9, 204, 81, 7, 15, 0, 8, 102, 0, 8,
			38, 0, 9, 172, 0, 8, 6, 0, 8, 134,
			0, 8, 70, 0, 9, 236, 80, 7, 9, 0,
			8, 94, 0, 8, 30, 0, 9, 156, 84, 7,
			99, 0, 8, 126, 0, 8, 62, 0, 9, 220,
			82, 7, 27, 0, 8, 110, 0, 8, 46, 0,
			9, 188, 0, 8, 14, 0, 8, 142, 0, 8,
			78, 0, 9, 252, 96, 7, 256, 0, 8, 81,
			0, 8, 17, 85, 8, 131, 82, 7, 31, 0,
			8, 113, 0, 8, 49, 0, 9, 194, 80, 7,
			10, 0, 8, 97, 0, 8, 33, 0, 9, 162,
			0, 8, 1, 0, 8, 129, 0, 8, 65, 0,
			9, 226, 80, 7, 6, 0, 8, 89, 0, 8,
			25, 0, 9, 146, 83, 7, 59, 0, 8, 121,
			0, 8, 57, 0, 9, 210, 81, 7, 17, 0,
			8, 105, 0, 8, 41, 0, 9, 178, 0, 8,
			9, 0, 8, 137, 0, 8, 73, 0, 9, 242,
			80, 7, 4, 0, 8, 85, 0, 8, 21, 80,
			8, 258, 83, 7, 43, 0, 8, 117, 0, 8,
			53, 0, 9, 202, 81, 7, 13, 0, 8, 101,
			0, 8, 37, 0, 9, 170, 0, 8, 5, 0,
			8, 133, 0, 8, 69, 0, 9, 234, 80, 7,
			8, 0, 8, 93, 0, 8, 29, 0, 9, 154,
			84, 7, 83, 0, 8, 125, 0, 8, 61, 0,
			9, 218, 82, 7, 23, 0, 8, 109, 0, 8,
			45, 0, 9, 186, 0, 8, 13, 0, 8, 141,
			0, 8, 77, 0, 9, 250, 80, 7, 3, 0,
			8, 83, 0, 8, 19, 85, 8, 195, 83, 7,
			35, 0, 8, 115, 0, 8, 51, 0, 9, 198,
			81, 7, 11, 0, 8, 99, 0, 8, 35, 0,
			9, 166, 0, 8, 3, 0, 8, 131, 0, 8,
			67, 0, 9, 230, 80, 7, 7, 0, 8, 91,
			0, 8, 27, 0, 9, 150, 84, 7, 67, 0,
			8, 123, 0, 8, 59, 0, 9, 214, 82, 7,
			19, 0, 8, 107, 0, 8, 43, 0, 9, 182,
			0, 8, 11, 0, 8, 139, 0, 8, 75, 0,
			9, 246, 80, 7, 5, 0, 8, 87, 0, 8,
			23, 192, 8, 0, 83, 7, 51, 0, 8, 119,
			0, 8, 55, 0, 9, 206, 81, 7, 15, 0,
			8, 103, 0, 8, 39, 0, 9, 174, 0, 8,
			7, 0, 8, 135, 0, 8, 71, 0, 9, 238,
			80, 7, 9, 0, 8, 95, 0, 8, 31, 0,
			9, 158, 84, 7, 99, 0, 8, 127, 0, 8,
			63, 0, 9, 222, 82, 7, 27, 0, 8, 111,
			0, 8, 47, 0, 9, 190, 0, 8, 15, 0,
			8, 143, 0, 8, 79, 0, 9, 254, 96, 7,
			256, 0, 8, 80, 0, 8, 16, 84, 8, 115,
			82, 7, 31, 0, 8, 112, 0, 8, 48, 0,
			9, 193, 80, 7, 10, 0, 8, 96, 0, 8,
			32, 0, 9, 161, 0, 8, 0, 0, 8, 128,
			0, 8, 64, 0, 9, 225, 80, 7, 6, 0,
			8, 88, 0, 8, 24, 0, 9, 145, 83, 7,
			59, 0, 8, 120, 0, 8, 56, 0, 9, 209,
			81, 7, 17, 0, 8, 104, 0, 8, 40, 0,
			9, 177, 0, 8, 8, 0, 8, 136, 0, 8,
			72, 0, 9, 241, 80, 7, 4, 0, 8, 84,
			0, 8, 20, 85, 8, 227, 83, 7, 43, 0,
			8, 116, 0, 8, 52, 0, 9, 201, 81, 7,
			13, 0, 8, 100, 0, 8, 36, 0, 9, 169,
			0, 8, 4, 0, 8, 132, 0, 8, 68, 0,
			9, 233, 80, 7, 8, 0, 8, 92, 0, 8,
			28, 0, 9, 153, 84, 7, 83, 0, 8, 124,
			0, 8, 60, 0, 9, 217, 82, 7, 23, 0,
			8, 108, 0, 8, 44, 0, 9, 185, 0, 8,
			12, 0, 8, 140, 0, 8, 76, 0, 9, 249,
			80, 7, 3, 0, 8, 82, 0, 8, 18, 85,
			8, 163, 83, 7, 35, 0, 8, 114, 0, 8,
			50, 0, 9, 197, 81, 7, 11, 0, 8, 98,
			0, 8, 34, 0, 9, 165, 0, 8, 2, 0,
			8, 130, 0, 8, 66, 0, 9, 229, 80, 7,
			7, 0, 8, 90, 0, 8, 26, 0, 9, 149,
			84, 7, 67, 0, 8, 122, 0, 8, 58, 0,
			9, 213, 82, 7, 19, 0, 8, 106, 0, 8,
			42, 0, 9, 181, 0, 8, 10, 0, 8, 138,
			0, 8, 74, 0, 9, 245, 80, 7, 5, 0,
			8, 86, 0, 8, 22, 192, 8, 0, 83, 7,
			51, 0, 8, 118, 0, 8, 54, 0, 9, 205,
			81, 7, 15, 0, 8, 102, 0, 8, 38, 0,
			9, 173, 0, 8, 6, 0, 8, 134, 0, 8,
			70, 0, 9, 237, 80, 7, 9, 0, 8, 94,
			0, 8, 30, 0, 9, 157, 84, 7, 99, 0,
			8, 126, 0, 8, 62, 0, 9, 221, 82, 7,
			27, 0, 8, 110, 0, 8, 46, 0, 9, 189,
			0, 8, 14, 0, 8, 142, 0, 8, 78, 0,
			9, 253, 96, 7, 256, 0, 8, 81, 0, 8,
			17, 85, 8, 131, 82, 7, 31, 0, 8, 113,
			0, 8, 49, 0, 9, 195, 80, 7, 10, 0,
			8, 97, 0, 8, 33, 0, 9, 163, 0, 8,
			1, 0, 8, 129, 0, 8, 65, 0, 9, 227,
			80, 7, 6, 0, 8, 89, 0, 8, 25, 0,
			9, 147, 83, 7, 59, 0, 8, 121, 0, 8,
			57, 0, 9, 211, 81, 7, 17, 0, 8, 105,
			0, 8, 41, 0, 9, 179, 0, 8, 9, 0,
			8, 137, 0, 8, 73, 0, 9, 243, 80, 7,
			4, 0, 8, 85, 0, 8, 21, 80, 8, 258,
			83, 7, 43, 0, 8, 117, 0, 8, 53, 0,
			9, 203, 81, 7, 13, 0, 8, 101, 0, 8,
			37, 0, 9, 171, 0, 8, 5, 0, 8, 133,
			0, 8, 69, 0, 9, 235, 80, 7, 8, 0,
			8, 93, 0, 8, 29, 0, 9, 155, 84, 7,
			83, 0, 8, 125, 0, 8, 61, 0, 9, 219,
			82, 7, 23, 0, 8, 109, 0, 8, 45, 0,
			9, 187, 0, 8, 13, 0, 8, 141, 0, 8,
			77, 0, 9, 251, 80, 7, 3, 0, 8, 83,
			0, 8, 19, 85, 8, 195, 83, 7, 35, 0,
			8, 115, 0, 8, 51, 0, 9, 199, 81, 7,
			11, 0, 8, 99, 0, 8, 35, 0, 9, 167,
			0, 8, 3, 0, 8, 131, 0, 8, 67, 0,
			9, 231, 80, 7, 7, 0, 8, 91, 0, 8,
			27, 0, 9, 151, 84, 7, 67, 0, 8, 123,
			0, 8, 59, 0, 9, 215, 82, 7, 19, 0,
			8, 107, 0, 8, 43, 0, 9, 183, 0, 8,
			11, 0, 8, 139, 0, 8, 75, 0, 9, 247,
			80, 7, 5, 0, 8, 87, 0, 8, 23, 192,
			8, 0, 83, 7, 51, 0, 8, 119, 0, 8,
			55, 0, 9, 207, 81, 7, 15, 0, 8, 103,
			0, 8, 39, 0, 9, 175, 0, 8, 7, 0,
			8, 135, 0, 8, 71, 0, 9, 239, 80, 7,
			9, 0, 8, 95, 0, 8, 31, 0, 9, 159,
			84, 7, 99, 0, 8, 127, 0, 8, 63, 0,
			9, 223, 82, 7, 27, 0, 8, 111, 0, 8,
			47, 0, 9, 191, 0, 8, 15, 0, 8, 143,
			0, 8, 79, 0, 9, 255
		};

		internal static readonly int[] fixed_td = new int[96]
		{
			80, 5, 1, 87, 5, 257, 83, 5, 17, 91,
			5, 4097, 81, 5, 5, 89, 5, 1025, 85, 5,
			65, 93, 5, 16385, 80, 5, 3, 88, 5, 513,
			84, 5, 33, 92, 5, 8193, 82, 5, 9, 90,
			5, 2049, 86, 5, 129, 192, 5, 24577, 80, 5,
			2, 87, 5, 385, 83, 5, 25, 91, 5, 6145,
			81, 5, 7, 89, 5, 1537, 85, 5, 97, 93,
			5, 24577, 80, 5, 4, 88, 5, 769, 84, 5,
			49, 92, 5, 12289, 82, 5, 13, 90, 5, 3073,
			86, 5, 193, 192, 5, 24577
		};

		internal static readonly int[] cplens = new int[31]
		{
			3, 4, 5, 6, 7, 8, 9, 10, 11, 13,
			15, 17, 19, 23, 27, 31, 35, 43, 51, 59,
			67, 83, 99, 115, 131, 163, 195, 227, 258, 0,
			0
		};

		internal static readonly int[] cplext = new int[31]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 2, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 5, 5, 5, 5, 0, 112,
			112
		};

		internal static readonly int[] cpdist = new int[30]
		{
			1, 2, 3, 4, 5, 7, 9, 13, 17, 25,
			33, 49, 65, 97, 129, 193, 257, 385, 513, 769,
			1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577
		};

		internal static readonly int[] cpdext = new int[30]
		{
			0, 0, 0, 0, 1, 1, 2, 2, 3, 3,
			4, 4, 5, 5, 6, 6, 7, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 13, 13
		};

		internal static int huft_build(int[] b, int bindex, int n, int s, int[] d, int[] e, int[] t, int[] m, int[] hp, int[] hn, int[] v)
		{
			int[] array = new int[16];
			int[] array2 = new int[3];
			int[] array3 = new int[15];
			int[] array4 = new int[16];
			int num = 0;
			int num2 = n;
			do
			{
				array[b[bindex + num]]++;
				num++;
				num2--;
			}
			while (num2 != 0);
			if (array[0] == n)
			{
				t[0] = -1;
				m[0] = 0;
				return 0;
			}
			int num3 = m[0];
			int i;
			for (i = 1; i <= 15 && array[i] == 0; i++)
			{
			}
			int j = i;
			if (num3 < i)
			{
				num3 = i;
			}
			num2 = 15;
			while (num2 != 0 && array[num2] == 0)
			{
				num2--;
			}
			int num4 = num2;
			if (num3 > num2)
			{
				num3 = num2;
			}
			m[0] = num3;
			int num5 = 1 << i;
			while (i < num2)
			{
				if ((num5 -= array[i]) < 0)
				{
					return -3;
				}
				i++;
				num5 <<= 1;
			}
			if ((num5 -= array[num2]) < 0)
			{
				return -3;
			}
			array[num2] += num5;
			i = (array4[1] = 0);
			num = 1;
			int num6 = 2;
			while (--num2 != 0)
			{
				i = (array4[num6] = i + array[num]);
				num6++;
				num++;
			}
			num2 = 0;
			num = 0;
			do
			{
				if ((i = b[bindex + num]) != 0)
				{
					v[array4[i]++] = num2;
				}
				num++;
			}
			while (++num2 < n);
			n = array4[num4];
			num2 = (array4[0] = 0);
			num = 0;
			int num7 = -1;
			int num8 = -num3;
			array3[0] = 0;
			int num9 = 0;
			int num10 = 0;
			for (; j <= num4; j++)
			{
				int num11 = array[j];
				while (num11-- != 0)
				{
					int num12;
					while (j > num8 + num3)
					{
						num7++;
						num8 += num3;
						num10 = num4 - num8;
						num10 = ((num10 > num3) ? num3 : num10);
						if ((num12 = 1 << (i = j - num8)) > num11 + 1)
						{
							num12 -= num11 + 1;
							num6 = j;
							if (i < num10)
							{
								while (++i < num10 && (num12 <<= 1) > array[++num6])
								{
									num12 -= array[num6];
								}
							}
						}
						num10 = 1 << i;
						if (hn[0] + num10 > 1440)
						{
							return -3;
						}
						num9 = (array3[num7] = hn[0]);
						hn[0] += num10;
						if (num7 != 0)
						{
							array4[num7] = num2;
							array2[0] = (byte)i;
							array2[1] = (byte)num3;
							i = SupportClass.URShift(num2, num8 - num3);
							array2[2] = num9 - array3[num7 - 1] - i;
							Array.Copy(array2, 0, hp, (array3[num7 - 1] + i) * 3, 3);
						}
						else
						{
							t[0] = num9;
						}
					}
					array2[1] = (byte)(j - num8);
					if (num >= n)
					{
						array2[0] = 192;
					}
					else if (v[num] < s)
					{
						array2[0] = (byte)((v[num] >= 256) ? 96 : 0);
						array2[2] = v[num++];
					}
					else
					{
						array2[0] = (byte)(e[v[num] - s] + 16 + 64);
						array2[2] = d[v[num++] - s];
					}
					num12 = 1 << j - num8;
					for (i = SupportClass.URShift(num2, num8); i < num10; i += num12)
					{
						Array.Copy(array2, 0, hp, (num9 + i) * 3, 3);
					}
					i = 1 << j - 1;
					while ((num2 & i) != 0)
					{
						num2 ^= i;
						i = SupportClass.URShift(i, 1);
					}
					num2 ^= i;
					int num13 = (1 << num8) - 1;
					while ((num2 & num13) != array4[num7])
					{
						num7--;
						num8 -= num3;
						num13 = (1 << num8) - 1;
					}
				}
			}
			if (num5 == 0 || num4 == 1)
			{
				return 0;
			}
			return -5;
		}

		internal static int inflate_trees_bits(int[] c, int[] bb, int[] tb, int[] hp, ZStream z)
		{
			int[] hn = new int[1];
			int[] v = new int[19];
			int num = huft_build(c, 0, 19, 19, null, null, tb, bb, hp, hn, v);
			if (num == -3)
			{
				z.msg = "oversubscribed dynamic bit lengths tree";
			}
			else if (num == -5 || bb[0] == 0)
			{
				z.msg = "incomplete dynamic bit lengths tree";
				num = -3;
			}
			return num;
		}

		internal static int inflate_trees_dynamic(int nl, int nd, int[] c, int[] bl, int[] bd, int[] tl, int[] td, int[] hp, ZStream z)
		{
			int[] hn = new int[1];
			int[] v = new int[288];
			int num = huft_build(c, 0, nl, 257, cplens, cplext, tl, bl, hp, hn, v);
			if (num != 0 || bl[0] == 0)
			{
				switch (num)
				{
				case -3:
					z.msg = "oversubscribed literal/length tree";
					break;
				default:
					z.msg = "incomplete literal/length tree";
					num = -3;
					break;
				case -4:
					break;
				}
				return num;
			}
			num = huft_build(c, nl, nd, 0, cpdist, cpdext, td, bd, hp, hn, v);
			if (num != 0 || (bd[0] == 0 && nl > 257))
			{
				switch (num)
				{
				case -3:
					z.msg = "oversubscribed distance tree";
					break;
				case -5:
					z.msg = "incomplete distance tree";
					num = -3;
					break;
				default:
					z.msg = "empty distance tree with lengths";
					num = -3;
					break;
				case -4:
					break;
				}
				return num;
			}
			return 0;
		}

		internal static int inflate_trees_fixed(int[] bl, int[] bd, int[][] tl, int[][] td, ZStream z)
		{
			bl[0] = 9;
			bd[0] = 5;
			tl[0] = fixed_tl;
			td[0] = fixed_td;
			return 0;
		}
	}
	internal sealed class StaticTree
	{
		private const int MAX_BITS = 15;

		private const int BL_CODES = 19;

		private const int D_CODES = 30;

		private const int LITERALS = 256;

		private const int LENGTH_CODES = 29;

		internal const int MAX_BL_BITS = 7;

		private static readonly int L_CODES;

		internal static readonly short[] static_ltree;

		internal static readonly short[] static_dtree;

		internal static StaticTree static_l_desc;

		internal static StaticTree static_d_desc;

		internal static StaticTree static_bl_desc;

		internal short[] static_tree;

		internal int[] extra_bits;

		internal int extra_base;

		internal int elems;

		internal int max_length;

		internal StaticTree(short[] static_tree, int[] extra_bits, int extra_base, int elems, int max_length)
		{
			this.static_tree = static_tree;
			this.extra_bits = extra_bits;
			this.extra_base = extra_base;
			this.elems = elems;
			this.max_length = max_length;
		}

		static StaticTree()
		{
			L_CODES = 286;
			static_ltree = new short[576]
			{
				12, 8, 140, 8, 76, 8, 204, 8, 44, 8,
				172, 8, 108, 8, 236, 8, 28, 8, 156, 8,
				92, 8, 220, 8, 60, 8, 188, 8, 124, 8,
				252, 8, 2, 8, 130, 8, 66, 8, 194, 8,
				34, 8, 162, 8, 98, 8, 226, 8, 18, 8,
				146, 8, 82, 8, 210, 8, 50, 8, 178, 8,
				114, 8, 242, 8, 10, 8, 138, 8, 74, 8,
				202, 8, 42, 8, 170, 8, 106, 8, 234, 8,
				26, 8, 154, 8, 90, 8, 218, 8, 58, 8,
				186, 8, 122, 8, 250, 8, 6, 8, 134, 8,
				70, 8, 198, 8, 38, 8, 166, 8, 102, 8,
				230, 8, 22, 8, 150, 8, 86, 8, 214, 8,
				54, 8, 182, 8, 118, 8, 246, 8, 14, 8,
				142, 8, 78, 8, 206, 8, 46, 8, 174, 8,
				110, 8, 238, 8, 30, 8, 158, 8, 94, 8,
				222, 8, 62, 8, 190, 8, 126, 8, 254, 8,
				1, 8, 129, 8, 65, 8, 193, 8, 33, 8,
				161, 8, 97, 8, 225, 8, 17, 8, 145, 8,
				81, 8, 209, 8, 49, 8, 177, 8, 113, 8,
				241, 8, 9, 8, 137, 8, 73, 8, 201, 8,
				41, 8, 169, 8, 105, 8, 233, 8, 25, 8,
				153, 8, 89, 8, 217, 8, 57, 8, 185, 8,
				121, 8, 249, 8, 5, 8, 133, 8, 69, 8,
				197, 8, 37, 8, 165, 8, 101, 8, 229, 8,
				21, 8, 149, 8, 85, 8, 213, 8, 53, 8,
				181, 8, 117, 8, 245, 8, 13, 8, 141, 8,
				77, 8, 205, 8, 45, 8, 173, 8, 109, 8,
				237, 8, 29, 8, 157, 8, 93, 8, 221, 8,
				61, 8, 189, 8, 125, 8, 253, 8, 19, 9,
				275, 9, 147, 9, 403, 9, 83, 9, 339, 9,
				211, 9, 467, 9, 51, 9, 307, 9, 179, 9,
				435, 9, 115, 9, 371, 9, 243, 9, 499, 9,
				11, 9, 267, 9, 139, 9, 395, 9, 75, 9,
				331, 9, 203, 9, 459, 9, 43, 9, 299, 9,
				171, 9, 427, 9, 107, 9, 363, 9, 235, 9,
				491, 9, 27, 9, 283, 9, 155, 9, 411, 9,
				91, 9, 347, 9, 219, 9, 475, 9, 59, 9,
				315, 9, 187, 9, 443, 9, 123, 9, 379, 9,
				251, 9, 507, 9, 7, 9, 263, 9, 135, 9,
				391, 9, 71, 9, 327, 9, 199, 9, 455, 9,
				39, 9, 295, 9, 167, 9, 423, 9, 103, 9,
				359, 9, 231, 9, 487, 9, 23, 9, 279, 9,
				151, 9, 407, 9, 87, 9, 343, 9, 215, 9,
				471, 9, 55, 9, 311, 9, 183, 9, 439, 9,
				119, 9, 375, 9, 247, 9, 503, 9, 15, 9,
				271, 9, 143, 9, 399, 9, 79, 9, 335, 9,
				207, 9, 463, 9, 47, 9, 303, 9, 175, 9,
				431, 9, 111, 9, 367, 9, 239, 9, 495, 9,
				31, 9, 287, 9, 159, 9, 415, 9, 95, 9,
				351, 9, 223, 9, 479, 9, 63, 9, 319, 9,
				191, 9, 447, 9, 127, 9, 383, 9, 255, 9,
				511, 9, 0, 7, 64, 7, 32, 7, 96, 7,
				16, 7, 80, 7, 48, 7, 112, 7, 8, 7,
				72, 7, 40, 7, 104, 7, 24, 7, 88, 7,
				56, 7, 120, 7, 4, 7, 68, 7, 36, 7,
				100, 7, 20, 7, 84, 7, 52, 7, 116, 7,
				3, 8, 131, 8, 67, 8, 195, 8, 35, 8,
				163, 8, 99, 8, 227, 8
			};
			static_dtree = new short[60]
			{
				0, 5, 16, 5, 8, 5, 24, 5, 4, 5,
				20, 5, 12, 5, 28, 5, 2, 5, 18, 5,
				10, 5, 26, 5, 6, 5, 22, 5, 14, 5,
				30, 5, 1, 5, 17, 5, 9, 5, 25, 5,
				5, 5, 21, 5, 13, 5, 29, 5, 3, 5,
				19, 5, 11, 5, 27, 5, 7, 5, 23, 5
			};
			static_l_desc = new StaticTree(static_ltree, Tree.extra_lbits, 257, L_CODES, 15);
			static_d_desc = new StaticTree(static_dtree, Tree.extra_dbits, 0, 30, 15);
			static_bl_desc = new StaticTree(null, Tree.extra_blbits, 0, 19, 7);
		}
	}
	internal class SupportClass
	{
		public static long Identity(long literal)
		{
			return literal;
		}

		public static ulong Identity(ulong literal)
		{
			return literal;
		}

		public static float Identity(float literal)
		{
			return literal;
		}

		public static double Identity(double literal)
		{
			return literal;
		}

		public static int URShift(int number, int bits)
		{
			if (number >= 0)
			{
				return number >> bits;
			}
			return (number >> bits) + (2 << ~bits);
		}

		public static int URShift(int number, long bits)
		{
			return URShift(number, (int)bits);
		}

		public static long URShift(long number, int bits)
		{
			if (number >= 0)
			{
				return number >> bits;
			}
			return (number >> bits) + (2L << ~bits);
		}

		public static long URShift(long number, long bits)
		{
			return URShift(number, (int)bits);
		}

		public static int ReadInput(Stream sourceStream, byte[] target, int start, int count)
		{
			if (target.Length == 0)
			{
				return 0;
			}
			byte[] array = new byte[target.Length];
			int num = sourceStream.Read(array, start, count);
			if (num == 0)
			{
				return -1;
			}
			for (int i = start; i < start + num; i++)
			{
				target[i] = array[i];
			}
			return num;
		}

		public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
		{
			if (target.Length == 0)
			{
				return 0;
			}
			char[] array = new char[target.Length];
			int num = sourceTextReader.Read(array, start, count);
			if (num == 0)
			{
				return -1;
			}
			for (int i = start; i < start + num; i++)
			{
				target[i] = (byte)array[i];
			}
			return num;
		}

		public static byte[] ToByteArray(string sourceString)
		{
			return Encoding.UTF8.GetBytes(sourceString);
		}

		public static char[] ToCharArray(byte[] byteArray)
		{
			return Encoding.UTF8.GetChars(byteArray);
		}

		public static void Serialize(Stream stream, object objectToSend)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, objectToSend);
		}

		public static void Serialize(BinaryWriter binaryWriter, object objectToSend)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(binaryWriter.BaseStream, objectToSend);
		}

		public static object Deserialize(BinaryReader binaryReader)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			return binaryFormatter.Deserialize(binaryReader.BaseStream);
		}

		public static void WriteStackTrace(Exception throwable, TextWriter stream)
		{
			stream.Write(throwable.StackTrace);
			stream.Flush();
		}
	}
	internal sealed class Tree
	{
		private const int MAX_BITS = 15;

		private const int BL_CODES = 19;

		private const int D_CODES = 30;

		private const int LITERALS = 256;

		private const int LENGTH_CODES = 29;

		internal const int MAX_BL_BITS = 7;

		internal const int END_BLOCK = 256;

		internal const int REP_3_6 = 16;

		internal const int REPZ_3_10 = 17;

		internal const int REPZ_11_138 = 18;

		internal const int Buf_size = 16;

		internal const int DIST_CODE_LEN = 512;

		private static readonly int L_CODES = 286;

		private static readonly int HEAP_SIZE = 2 * L_CODES + 1;

		internal static readonly int[] extra_lbits = new int[29]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 2, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 5, 5, 5, 5, 0
		};

		internal static readonly int[] extra_dbits = new int[30]
		{
			0, 0, 0, 0, 1, 1, 2, 2, 3, 3,
			4, 4, 5, 5, 6, 6, 7, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 13, 13
		};

		internal static readonly int[] extra_blbits = new int[19]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 2, 3, 7
		};

		internal static readonly byte[] bl_order = new byte[19]
		{
			16, 17, 18, 0, 8, 7, 9, 6, 10, 5,
			11, 4, 12, 3, 13, 2, 14, 1, 15
		};

		internal static readonly byte[] _dist_code = new byte[512]
		{
			0, 1, 2, 3, 4, 4, 5, 5, 6, 6,
			6, 6, 7, 7, 7, 7, 8, 8, 8, 8,
			8, 8, 8, 8, 9, 9, 9, 9, 9, 9,
			9, 9, 10, 10, 10, 10, 10, 10, 10, 10,
			10, 10, 10, 10, 10, 10, 10, 10, 11, 11,
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 12, 12, 12, 12, 12, 12,
			12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
			12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
			12, 12, 12, 12, 12, 12, 13, 13, 13, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 0, 0, 16, 17,
			18, 18, 19, 19, 20, 20, 20, 20, 21, 21,
			21, 21, 22, 22, 22, 22, 22, 22, 22, 22,
			23, 23, 23, 23, 23, 23, 23, 23, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
			28, 28, 28, 28, 28, 28, 28, 28, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
			29, 29
		};

		internal static readonly byte[] _length_code = new byte[256]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 12, 12,
			13, 13, 13, 13, 14, 14, 14, 14, 15, 15,
			15, 15, 16, 16, 16, 16, 16, 16, 16, 16,
			17, 17, 17, 17, 17, 17, 17, 17, 18, 18,
			18, 18, 18, 18, 18, 18, 19, 19, 19, 19,
			19, 19, 19, 19, 20, 20, 20, 20, 20, 20,
			20, 20, 20, 20, 20, 20, 20, 20, 20, 20,
			21, 21, 21, 21, 21, 21, 21, 21, 21, 21,
			21, 21, 21, 21, 21, 21, 22, 22, 22, 22,
			22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
			22, 22, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
			25, 25, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
			26, 26, 26, 26, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 28
		};

		internal static readonly int[] base_length = new int[29]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 10,
			12, 14, 16, 20, 24, 28, 32, 40, 48, 56,
			64, 80, 96, 112, 128, 160, 192, 224, 0
		};

		internal static readonly int[] base_dist = new int[30]
		{
			0, 1, 2, 3, 4, 6, 8, 12, 16, 24,
			32, 48, 64, 96, 128, 192, 256, 384, 512, 768,
			1024, 1536, 2048, 3072, 4096, 6144, 8192, 12288, 16384, 24576
		};

		internal short[] dyn_tree;

		internal int max_code;

		internal StaticTree stat_desc;

		internal static int d_code(int dist)
		{
			if (dist >= 256)
			{
				return _dist_code[256 + SupportClass.URShift(dist, 7)];
			}
			return _dist_code[dist];
		}

		internal void gen_bitlen(Deflate s)
		{
			short[] array = dyn_tree;
			short[] static_tree = stat_desc.static_tree;
			int[] extra_bits = stat_desc.extra_bits;
			int extra_base = stat_desc.extra_base;
			int max_length = stat_desc.max_length;
			int num = 0;
			for (int i = 0; i <= 15; i++)
			{
				s.bl_count[i] = 0;
			}
			array[s.heap[s.heap_max] * 2 + 1] = 0;
			int j;
			for (j = s.heap_max + 1; j < HEAP_SIZE; j++)
			{
				int num2 = s.heap[j];
				int i = array[array[num2 * 2 + 1] * 2 + 1] + 1;
				if (i > max_length)
				{
					i = max_length;
					num++;
				}
				array[num2 * 2 + 1] = (short)i;
				if (num2 <= max_code)
				{
					s.bl_count[i]++;
					int num3 = 0;
					if (num2 >= extra_base)
					{
						num3 = extra_bits[num2 - extra_base];
					}
					short num4 = array[num2 * 2];
					s.opt_len += num4 * (i + num3);
					if (static_tree != null)
					{
						s.static_len += num4 * (static_tree[num2 * 2 + 1] + num3);
					}
				}
			}
			if (num == 0)
			{
				return;
			}
			do
			{
				int i = max_length - 1;
				while (s.bl_count[i] == 0)
				{
					i--;
				}
				s.bl_count[i]--;
				s.bl_count[i + 1] = (short)(s.bl_count[i + 1] + 2);
				s.bl_count[max_length]--;
				num -= 2;
			}
			while (num > 0);
			for (int i = max_length; i != 0; i--)
			{
				int num2 = s.bl_count[i];
				while (num2 != 0)
				{
					int num5 = s.heap[--j];
					if (num5 <= max_code)
					{
						if (array[num5 * 2 + 1] != i)
						{
							s.opt_len = (int)(s.opt_len + ((long)i - (long)array[num5 * 2 + 1]) * array[num5 * 2]);
							array[num5 * 2 + 1] = (short)i;
						}
						num2--;
					}
				}
			}
		}

		internal void build_tree(Deflate s)
		{
			short[] array = dyn_tree;
			short[] static_tree = stat_desc.static_tree;
			int elems = stat_desc.elems;
			int num = -1;
			s.heap_len = 0;
			s.heap_max = HEAP_SIZE;
			for (int i = 0; i < elems; i++)
			{
				if (array[i * 2] != 0)
				{
					num = (s.heap[++s.heap_len] = i);
					s.depth[i] = 0;
				}
				else
				{
					array[i * 2 + 1] = 0;
				}
			}
			int num2;
			while (s.heap_len < 2)
			{
				num2 = (s.heap[++s.heap_len] = ((num < 2) ? (++num) : 0));
				array[num2 * 2] = 1;
				s.depth[num2] = 0;
				s.opt_len--;
				if (static_tree != null)
				{
					s.static_len -= static_tree[num2 * 2 + 1];
				}
			}
			max_code = num;
			for (int i = s.heap_len / 2; i >= 1; i--)
			{
				s.pqdownheap(array, i);
			}
			num2 = elems;
			do
			{
				int i = s.heap[1];
				s.heap[1] = s.heap[s.heap_len--];
				s.pqdownheap(array, 1);
				int num3 = s.heap[1];
				s.heap[--s.heap_max] = i;
				s.heap[--s.heap_max] = num3;
				array[num2 * 2] = (short)(array[i * 2] + array[num3 * 2]);
				s.depth[num2] = (byte)(Math.Max(s.depth[i], s.depth[num3]) + 1);
				array[i * 2 + 1] = (array[num3 * 2 + 1] = (short)num2);
				s.heap[1] = num2++;
				s.pqdownheap(array, 1);
			}
			while (s.heap_len >= 2);
			s.heap[--s.heap_max] = s.heap[1];
			gen_bitlen(s);
			gen_codes(array, num, s.bl_count);
		}

		internal static void gen_codes(short[] tree, int max_code, short[] bl_count)
		{
			short[] array = new short[16];
			short num = 0;
			for (int i = 1; i <= 15; i++)
			{
				num = (array[i] = (short)(num + bl_count[i - 1] << 1));
			}
			for (int j = 0; j <= max_code; j++)
			{
				int num2 = tree[j * 2 + 1];
				if (num2 != 0)
				{
					tree[j * 2] = (short)bi_reverse(array[num2]++, num2);
				}
			}
		}

		internal static int bi_reverse(int code, int len)
		{
			int num = 0;
			do
			{
				num |= code & 1;
				code = SupportClass.URShift(code, 1);
				num <<= 1;
			}
			while (--len > 0);
			return SupportClass.URShift(num, 1);
		}
	}
	internal class ZInputStream : BinaryReader
	{
		public long maxInput;

		protected internal ZStream z = new ZStream();

		protected internal int bufsize = 512;

		protected internal int flush;

		protected internal byte[] buf;

		protected internal byte[] buf1 = new byte[1];

		protected internal bool compress;

		private Stream in_Renamed;

		private bool nomoreinput;

		public virtual int FlushMode
		{
			get
			{
				return flush;
			}
			set
			{
				flush = value;
			}
		}

		public virtual long TotalIn => z.total_in;

		public virtual long TotalOut => z.total_out;

		private void InitBlock()
		{
			flush = 0;
			buf = new byte[bufsize];
		}

		public ZInputStream(Stream in_Renamed)
			: base(in_Renamed)
		{
			InitBlock();
			this.in_Renamed = in_Renamed;
			z.inflateInit();
			compress = false;
			z.next_in = buf;
			z.next_in_index = 0;
			z.avail_in = 0;
		}

		public ZInputStream(Stream in_Renamed, int level)
			: base(in_Renamed)
		{
			InitBlock();
			this.in_Renamed = in_Renamed;
			z.deflateInit(level);
			compress = true;
			z.next_in = buf;
			z.next_in_index = 0;
			z.avail_in = 0;
		}

		public override int Read()
		{
			if (read(buf1, 0, 1) == -1)
			{
				return -1;
			}
			return buf1[0] & 0xFF;
		}

		public int read(byte[] b, int off, int len)
		{
			if (len == 0)
			{
				return 0;
			}
			z.next_out = b;
			z.next_out_index = off;
			z.avail_out = len;
			int num;
			do
			{
				if (z.avail_in == 0 && !nomoreinput)
				{
					z.next_in_index = 0;
					int count = bufsize;
					if (maxInput > 0)
					{
						if (TotalIn < maxInput)
						{
							count = (int)Math.Min(maxInput - TotalIn, bufsize);
						}
						else
						{
							z.avail_in = -1;
						}
					}
					if (z.avail_in != -1)
					{
						z.avail_in = SupportClass.ReadInput(in_Renamed, buf, 0, count);
					}
					if (z.avail_in == -1)
					{
						z.avail_in = 0;
						nomoreinput = true;
					}
				}
				num = ((!compress) ? z.inflate(flush) : z.deflate(flush));
				if (nomoreinput && num == -5)
				{
					return -1;
				}
				if (num != 0 && num != 1)
				{
					throw new ZStreamException((compress ? "de" : "in") + "flating: " + z.msg);
				}
				if (nomoreinput && z.avail_out == len)
				{
					return -1;
				}
			}
			while (z.avail_out > 0 && num == 0);
			return len - z.avail_out;
		}

		public long skip(long n)
		{
			int num = 512;
			if (n < num)
			{
				num = (int)n;
			}
			byte[] array = new byte[num];
			return SupportClass.ReadInput(BaseStream, array, 0, array.Length);
		}

		public override void Close()
		{
			in_Renamed.Close();
		}
	}
	internal sealed class zlibConst
	{
		private const string version_Renamed_Field = "1.0.2";

		public const int Z_NO_COMPRESSION = 0;

		public const int Z_BEST_SPEED = 1;

		public const int Z_BEST_COMPRESSION = 9;

		public const int Z_DEFAULT_COMPRESSION = -1;

		public const int Z_FILTERED = 1;

		public const int Z_HUFFMAN_ONLY = 2;

		public const int Z_DEFAULT_STRATEGY = 0;

		public const int Z_NO_FLUSH = 0;

		public const int Z_PARTIAL_FLUSH = 1;

		public const int Z_SYNC_FLUSH = 2;

		public const int Z_FULL_FLUSH = 3;

		public const int Z_FINISH = 4;

		public const int Z_OK = 0;

		public const int Z_STREAM_END = 1;

		public const int Z_NEED_DICT = 2;

		public const int Z_ERRNO = -1;

		public const int Z_STREAM_ERROR = -2;

		public const int Z_DATA_ERROR = -3;

		public const int Z_MEM_ERROR = -4;

		public const int Z_BUF_ERROR = -5;

		public const int Z_VERSION_ERROR = -6;

		public static string version()
		{
			return "1.0.2";
		}
	}
	internal class ZOutputStream : Stream
	{
		protected internal ZStream z = new ZStream();

		protected internal int bufsize = 512;

		protected internal int flush_Renamed_Field;

		protected internal byte[] buf;

		protected internal byte[] buf1 = new byte[1];

		protected internal bool compress;

		private Stream out_Renamed;

		public virtual int FlushMode
		{
			get
			{
				return flush_Renamed_Field;
			}
			set
			{
				flush_Renamed_Field = value;
			}
		}

		public virtual long TotalIn => z.total_in;

		public virtual long TotalOut => z.total_out;

		public override bool CanRead => false;

		public override bool CanSeek => false;

		public override bool CanWrite => false;

		public override long Length => 0L;

		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		private void InitBlock()
		{
			flush_Renamed_Field = 0;
			buf = new byte[bufsize];
		}

		public ZOutputStream(Stream out_Renamed)
		{
			InitBlock();
			this.out_Renamed = out_Renamed;
			z.inflateInit();
			compress = false;
		}

		public ZOutputStream(Stream out_Renamed, int level)
		{
			InitBlock();
			this.out_Renamed = out_Renamed;
			z.deflateInit(level);
			compress = true;
		}

		public void WriteByte(int b)
		{
			buf1[0] = (byte)b;
			Write(buf1, 0, 1);
		}

		public override void WriteByte(byte b)
		{
			WriteByte(b);
		}

		public override void Write(byte[] b1, int off, int len)
		{
			if (len == 0)
			{
				return;
			}
			byte[] array = new byte[b1.Length];
			Array.Copy(b1, array, b1.Length);
			z.next_in = array;
			z.next_in_index = off;
			z.avail_in = len;
			do
			{
				z.next_out = buf;
				z.next_out_index = 0;
				z.avail_out = bufsize;
				if (((!compress) ? z.inflate(flush_Renamed_Field) : z.deflate(flush_Renamed_Field)) != 0)
				{
					throw new ZStreamException((compress ? "de" : "in") + "flating: " + z.msg);
				}
				out_Renamed.Write(buf, 0, bufsize - z.avail_out);
			}
			while (z.avail_in > 0 || z.avail_out == 0);
		}

		public virtual void finish()
		{
			do
			{
				z.next_out = buf;
				z.next_out_index = 0;
				z.avail_out = bufsize;
				int num = ((!compress) ? z.inflate(4) : z.deflate(4));
				if (num != 1 && num != 0)
				{
					throw new ZStreamException((compress ? "de" : "in") + "flating: " + z.msg);
				}
				if (bufsize - z.avail_out > 0)
				{
					out_Renamed.Write(buf, 0, bufsize - z.avail_out);
				}
			}
			while (z.avail_in > 0 || z.avail_out == 0);
			try
			{
				Flush();
			}
			catch
			{
			}
		}

		public virtual void end()
		{
			if (compress)
			{
				z.deflateEnd();
			}
			else
			{
				z.inflateEnd();
			}
			z.free();
			z = null;
		}

		public override void Close()
		{
			try
			{
				finish();
			}
			catch
			{
			}
			finally
			{
				end();
				out_Renamed.Close();
				out_Renamed = null;
			}
		}

		public override void Flush()
		{
			out_Renamed.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		public override void SetLength(long value)
		{
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}
	}
	internal sealed class ZStream
	{
		private const int MAX_WBITS = 15;

		private const int Z_NO_FLUSH = 0;

		private const int Z_PARTIAL_FLUSH = 1;

		private const int Z_SYNC_FLUSH = 2;

		private const int Z_FULL_FLUSH = 3;

		private const int Z_FINISH = 4;

		private const int MAX_MEM_LEVEL = 9;

		private const int Z_OK = 0;

		private const int Z_STREAM_END = 1;

		private const int Z_NEED_DICT = 2;

		private const int Z_ERRNO = -1;

		private const int Z_STREAM_ERROR = -2;

		private const int Z_DATA_ERROR = -3;

		private const int Z_MEM_ERROR = -4;

		private const int Z_BUF_ERROR = -5;

		private const int Z_VERSION_ERROR = -6;

		private static readonly int DEF_WBITS = 15;

		public byte[] next_in;

		public int next_in_index;

		public int avail_in;

		public long total_in;

		public byte[] next_out;

		public int next_out_index;

		public int avail_out;

		public long total_out;

		public string msg;

		internal Deflate dstate;

		internal Inflate istate;

		internal int data_type;

		public long adler;

		internal Adler32 _adler = new Adler32();

		public int inflateInit()
		{
			return inflateInit(DEF_WBITS);
		}

		public int inflateInit(int w)
		{
			istate = new Inflate();
			return istate.inflateInit(this, w);
		}

		public int inflate(int f)
		{
			if (istate == null)
			{
				return -2;
			}
			return istate.inflate(this, f);
		}

		public int inflateEnd()
		{
			if (istate == null)
			{
				return -2;
			}
			int result = istate.inflateEnd(this);
			istate = null;
			return result;
		}

		public int inflateSync()
		{
			if (istate == null)
			{
				return -2;
			}
			return istate.inflateSync(this);
		}

		public int inflateSetDictionary(byte[] dictionary, int dictLength)
		{
			if (istate == null)
			{
				return -2;
			}
			return istate.inflateSetDictionary(this, dictionary, dictLength);
		}

		public int deflateInit(int level)
		{
			return deflateInit(level, 15);
		}

		public int deflateInit(int level, int bits)
		{
			dstate = new Deflate();
			return dstate.deflateInit(this, level, bits);
		}

		public int deflate(int flush)
		{
			if (dstate == null)
			{
				return -2;
			}
			return dstate.deflate(this, flush);
		}

		public int deflateEnd()
		{
			if (dstate == null)
			{
				return -2;
			}
			int result = dstate.deflateEnd();
			dstate = null;
			return result;
		}

		public int deflateParams(int level, int strategy)
		{
			if (dstate == null)
			{
				return -2;
			}
			return dstate.deflateParams(this, level, strategy);
		}

		public int deflateSetDictionary(byte[] dictionary, int dictLength)
		{
			if (dstate == null)
			{
				return -2;
			}
			return dstate.deflateSetDictionary(this, dictionary, dictLength);
		}

		internal void flush_pending()
		{
			int pending = dstate.pending;
			if (pending > avail_out)
			{
				pending = avail_out;
			}
			if (pending != 0)
			{
				if (dstate.pending_buf.Length <= dstate.pending_out || next_out.Length <= next_out_index || dstate.pending_buf.Length < dstate.pending_out + pending || next_out.Length < next_out_index + pending)
				{
					Console.Out.WriteLine(dstate.pending_buf.Length + ", " + dstate.pending_out + ", " + next_out.Length + ", " + next_out_index + ", " + pending);
					Console.Out.WriteLine("avail_out=" + avail_out);
				}
				Array.Copy(dstate.pending_buf, dstate.pending_out, next_out, next_out_index, pending);
				next_out_index += pending;
				dstate.pending_out += pending;
				total_out += pending;
				avail_out -= pending;
				dstate.pending -= pending;
				if (dstate.pending == 0)
				{
					dstate.pending_out = 0;
				}
			}
		}

		internal int read_buf(byte[] buf, int start, int size)
		{
			int num = avail_in;
			if (num > size)
			{
				num = size;
			}
			if (num == 0)
			{
				return 0;
			}
			avail_in -= num;
			if (dstate.noheader == 0)
			{
				adler = _adler.adler32(adler, next_in, next_in_index, num);
			}
			Array.Copy(next_in, next_in_index, buf, start, num);
			next_in_index += num;
			total_in += num;
			return num;
		}

		public void free()
		{
			next_in = null;
			next_out = null;
			msg = null;
			_adler = null;
		}
	}
	internal class ZStreamException : IOException
	{
		public ZStreamException()
		{
		}

		public ZStreamException(string s)
			: base(s)
		{
		}
	}
}
namespace MySql.Data.MySqlClient.Properties
{
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[CompilerGenerated]
	public class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("MySql.Data.MySqlClient.Properties.Resources", typeof(Resources).Assembly);
					resourceMan = resourceManager;
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		public static string AdapterIsNull => ResourceManager.GetString("AdapterIsNull", resourceCulture);

		public static string AdapterSelectIsNull => ResourceManager.GetString("AdapterSelectIsNull", resourceCulture);

		public static string AttemptToAccessBeforeRead => ResourceManager.GetString("AttemptToAccessBeforeRead", resourceCulture);

		public static string AuthenticationFailed => ResourceManager.GetString("AuthenticationFailed", resourceCulture);

		public static string AuthenticationMethodNotSupported => ResourceManager.GetString("AuthenticationMethodNotSupported", resourceCulture);

		public static string BadVersionFormat => ResourceManager.GetString("BadVersionFormat", resourceCulture);

		public static string BufferCannotBeNull => ResourceManager.GetString("BufferCannotBeNull", resourceCulture);

		public static string BufferNotLargeEnough => ResourceManager.GetString("BufferNotLargeEnough", resourceCulture);

		public static string CancelNeeds50 => ResourceManager.GetString("CancelNeeds50", resourceCulture);

		public static string CancelNotSupported => ResourceManager.GetString("CancelNotSupported", resourceCulture);

		public static string CanNotDeriveParametersForTextCommands => ResourceManager.GetString("CanNotDeriveParametersForTextCommands", resourceCulture);

		public static string CBMultiTableNotSupported => ResourceManager.GetString("CBMultiTableNotSupported", resourceCulture);

		public static string CBNoKeyColumn => ResourceManager.GetString("CBNoKeyColumn", resourceCulture);

		public static string ChaosNotSupported => ResourceManager.GetString("ChaosNotSupported", resourceCulture);

		public static string CommandTextNotInitialized => ResourceManager.GetString("CommandTextNotInitialized", resourceCulture);

		public static string ConnectionAlreadyOpen => ResourceManager.GetString("ConnectionAlreadyOpen", resourceCulture);

		public static string ConnectionBroken => ResourceManager.GetString("ConnectionBroken", resourceCulture);

		public static string ConnectionMustBeOpen => ResourceManager.GetString("ConnectionMustBeOpen", resourceCulture);

		public static string ConnectionNotOpen => ResourceManager.GetString("ConnectionNotOpen", resourceCulture);

		public static string ConnectionNotSet => ResourceManager.GetString("ConnectionNotSet", resourceCulture);

		public static string CouldNotFindColumnName => ResourceManager.GetString("CouldNotFindColumnName", resourceCulture);

		public static string CountCannotBeNegative => ResourceManager.GetString("CountCannotBeNegative", resourceCulture);

		public static string CSNoSetLength => ResourceManager.GetString("CSNoSetLength", resourceCulture);

		public static string DataNotInSupportedFormat => ResourceManager.GetString("DataNotInSupportedFormat", resourceCulture);

		public static string DataReaderOpen => ResourceManager.GetString("DataReaderOpen", resourceCulture);

		public static string DefaultEncodingNotFound => ResourceManager.GetString("DefaultEncodingNotFound", resourceCulture);

		public static string DistributedTxnNotSupported => ResourceManager.GetString("DistributedTxnNotSupported", resourceCulture);

		public static string ErrorCreatingSocket => ResourceManager.GetString("ErrorCreatingSocket", resourceCulture);

		public static string FatalErrorDuringExecute => ResourceManager.GetString("FatalErrorDuringExecute", resourceCulture);

		public static string FatalErrorDuringRead => ResourceManager.GetString("FatalErrorDuringRead", resourceCulture);

		public static string FatalErrorReadingResult => ResourceManager.GetString("FatalErrorReadingResult", resourceCulture);

		public static string FileBasedCertificateNotSupported => ResourceManager.GetString("FileBasedCertificateNotSupported", resourceCulture);

		public static string FromAndLengthTooBig => ResourceManager.GetString("FromAndLengthTooBig", resourceCulture);

		public static string FromIndexMustBeValid => ResourceManager.GetString("FromIndexMustBeValid", resourceCulture);

		public static string GetHostEntryFailed => ResourceManager.GetString("GetHostEntryFailed", resourceCulture);

		public static string HardProcQuery => ResourceManager.GetString("HardProcQuery", resourceCulture);

		public static string ImproperValueFormat => ResourceManager.GetString("ImproperValueFormat", resourceCulture);

		public static string IncorrectTransmission => ResourceManager.GetString("IncorrectTransmission", resourceCulture);

		public static string IndexAndLengthTooBig => ResourceManager.GetString("IndexAndLengthTooBig", resourceCulture);

		public static string IndexMustBeValid => ResourceManager.GetString("IndexMustBeValid", resourceCulture);

		public static string InvalidColumnOrdinal => ResourceManager.GetString("InvalidColumnOrdinal", resourceCulture);

		public static string InvalidConnectionStringValue => ResourceManager.GetString("InvalidConnectionStringValue", resourceCulture);

		public static string InvalidMicrosecondValue => ResourceManager.GetString("InvalidMicrosecondValue", resourceCulture);

		public static string InvalidMillisecondValue => ResourceManager.GetString("InvalidMillisecondValue", resourceCulture);

		public static string InvalidProcName => ResourceManager.GetString("InvalidProcName", resourceCulture);

		public static string InvalidValueForBoolean => ResourceManager.GetString("InvalidValueForBoolean", resourceCulture);

		public static string KeywordNoNull => ResourceManager.GetString("KeywordNoNull", resourceCulture);

		public static string KeywordNotSupported => ResourceManager.GetString("KeywordNotSupported", resourceCulture);

		public static string keywords => ResourceManager.GetString("keywords", resourceCulture);

		public static string MixedParameterNamingNotAllowed => ResourceManager.GetString("MixedParameterNamingNotAllowed", resourceCulture);

		public static string MoreThanOneOPRow => ResourceManager.GetString("MoreThanOneOPRow", resourceCulture);

		public static string MultipleConnectionsInTransactionNotSupported => ResourceManager.GetString("MultipleConnectionsInTransactionNotSupported", resourceCulture);

		public static string NamedPipeNoSeek => ResourceManager.GetString("NamedPipeNoSeek", resourceCulture);

		public static string NamedPipeNoSetLength => ResourceManager.GetString("NamedPipeNoSetLength", resourceCulture);

		public static string NewValueShouldBeMySqlParameter => ResourceManager.GetString("NewValueShouldBeMySqlParameter", resourceCulture);

		public static string NextResultIsClosed => ResourceManager.GetString("NextResultIsClosed", resourceCulture);

		public static string NoBodiesAndTypeNotSet => ResourceManager.GetString("NoBodiesAndTypeNotSet", resourceCulture);

		public static string NoNestedTransactions => ResourceManager.GetString("NoNestedTransactions", resourceCulture);

		public static string NoServerSSLSupport => ResourceManager.GetString("NoServerSSLSupport", resourceCulture);

		public static string NoUnixSocketsOnWindows => ResourceManager.GetString("NoUnixSocketsOnWindows", resourceCulture);

		public static string NoWindowsIdentity => ResourceManager.GetString("NoWindowsIdentity", resourceCulture);

		public static string ObjectDisposed => ResourceManager.GetString("ObjectDisposed", resourceCulture);

		public static string OffsetCannotBeNegative => ResourceManager.GetString("OffsetCannotBeNegative", resourceCulture);

		public static string OffsetMustBeValid => ResourceManager.GetString("OffsetMustBeValid", resourceCulture);

		public static string OldPasswordsNotSupported => ResourceManager.GetString("OldPasswordsNotSupported", resourceCulture);

		public static string ParameterAlreadyDefined => ResourceManager.GetString("ParameterAlreadyDefined", resourceCulture);

		public static string ParameterCannotBeNegative => ResourceManager.GetString("ParameterCannotBeNegative", resourceCulture);

		public static string ParameterCannotBeNull => ResourceManager.GetString("ParameterCannotBeNull", resourceCulture);

		public static string ParameterIndexNotFound => ResourceManager.GetString("ParameterIndexNotFound", resourceCulture);

		public static string ParameterIsInvalid => ResourceManager.GetString("ParameterIsInvalid", resourceCulture);

		public static string ParameterMustBeDefined => ResourceManager.GetString("ParameterMustBeDefined", resourceCulture);

		public static string ParameterNotFoundDuringPrepare => ResourceManager.GetString("ParameterNotFoundDuringPrepare", resourceCulture);

		public static string PasswordMustHaveLegalChars => ResourceManager.GetString("PasswordMustHaveLegalChars", resourceCulture);

		public static string PerfMonCategoryHelp => ResourceManager.GetString("PerfMonCategoryHelp", resourceCulture);

		public static string PerfMonCategoryName => ResourceManager.GetString("PerfMonCategoryName", resourceCulture);

		public static string PerfMonHardProcHelp => ResourceManager.GetString("PerfMonHardProcHelp", resourceCulture);

		public static string PerfMonHardProcName => ResourceManager.GetString("PerfMonHardProcName", resourceCulture);

		public static string PerfMonSoftProcHelp => ResourceManager.GetString("PerfMonSoftProcHelp", resourceCulture);

		public static string PerfMonSoftProcName => ResourceManager.GetString("PerfMonSoftProcName", resourceCulture);

		public static string ProcAndFuncSameName => ResourceManager.GetString("ProcAndFuncSameName", resourceCulture);

		public static string QueryTooLarge => ResourceManager.GetString("QueryTooLarge", resourceCulture);

		public static string ReadFromStreamFailed => ResourceManager.GetString("ReadFromStreamFailed", resourceCulture);

		public static string ReadingPriorColumnUsingSeqAccess => ResourceManager.GetString("ReadingPriorColumnUsingSeqAccess", resourceCulture);

		public static string ReplicatedConnectionsAllowOnlyReadonlyStatements => ResourceManager.GetString("ReplicatedConnectionsAllowOnlyReadonlyStatements", resourceCulture);

		public static string Replication_ConnectionAttemptFailed => ResourceManager.GetString("Replication_ConnectionAttemptFailed", resourceCulture);

		public static string Replication_NoAvailableServer => ResourceManager.GetString("Replication_NoAvailableServer", resourceCulture);

		public static string ReplicationGroupNotFound => ResourceManager.GetString("ReplicationGroupNotFound", resourceCulture);

		public static string ReplicationServerNotFound => ResourceManager.GetString("ReplicationServerNotFound", resourceCulture);

		public static string RoutineNotFound => ResourceManager.GetString("RoutineNotFound", resourceCulture);

		public static string RoutineRequiresReturnParameter => ResourceManager.GetString("RoutineRequiresReturnParameter", resourceCulture);

		public static string ServerTooOld => ResourceManager.GetString("ServerTooOld", resourceCulture);

		public static string SnapshotNotSupported => ResourceManager.GetString("SnapshotNotSupported", resourceCulture);

		public static string SocketNoSeek => ResourceManager.GetString("SocketNoSeek", resourceCulture);

		public static string SoftProcQuery => ResourceManager.GetString("SoftProcQuery", resourceCulture);

		public static string SPNotSupported => ResourceManager.GetString("SPNotSupported", resourceCulture);

		public static string StreamAlreadyClosed => ResourceManager.GetString("StreamAlreadyClosed", resourceCulture);

		public static string StreamNoRead => ResourceManager.GetString("StreamNoRead", resourceCulture);

		public static string StreamNoWrite => ResourceManager.GetString("StreamNoWrite", resourceCulture);

		public static string Timeout => ResourceManager.GetString("Timeout", resourceCulture);

		public static string TimeoutGettingConnection => ResourceManager.GetString("TimeoutGettingConnection", resourceCulture);

		public static string TraceCloseConnection => ResourceManager.GetString("TraceCloseConnection", resourceCulture);

		public static string TraceErrorMoreThanMaxValueConnections => ResourceManager.GetString("TraceErrorMoreThanMaxValueConnections", resourceCulture);

		public static string TraceFetchError => ResourceManager.GetString("TraceFetchError", resourceCulture);

		public static string TraceOpenConnection => ResourceManager.GetString("TraceOpenConnection", resourceCulture);

		public static string TraceOpenResultError => ResourceManager.GetString("TraceOpenResultError", resourceCulture);

		public static string TraceQueryDone => ResourceManager.GetString("TraceQueryDone", resourceCulture);

		public static string TraceQueryNormalized => ResourceManager.GetString("TraceQueryNormalized", resourceCulture);

		public static string TraceQueryOpened => ResourceManager.GetString("TraceQueryOpened", resourceCulture);

		public static string TraceResult => ResourceManager.GetString("TraceResult", resourceCulture);

		public static string TraceResultClosed => ResourceManager.GetString("TraceResultClosed", resourceCulture);

		public static string TraceSetDatabase => ResourceManager.GetString("TraceSetDatabase", resourceCulture);

		public static string TraceStatementClosed => ResourceManager.GetString("TraceStatementClosed", resourceCulture);

		public static string TraceStatementExecuted => ResourceManager.GetString("TraceStatementExecuted", resourceCulture);

		public static string TraceStatementPrepared => ResourceManager.GetString("TraceStatementPrepared", resourceCulture);

		public static string TraceUAWarningBadIndex => ResourceManager.GetString("TraceUAWarningBadIndex", resourceCulture);

		public static string TraceUAWarningFieldConversion => ResourceManager.GetString("TraceUAWarningFieldConversion", resourceCulture);

		public static string TraceUAWarningNoIndex => ResourceManager.GetString("TraceUAWarningNoIndex", resourceCulture);

		public static string TraceUAWarningSkippedColumns => ResourceManager.GetString("TraceUAWarningSkippedColumns", resourceCulture);

		public static string TraceUAWarningSkippedRows => ResourceManager.GetString("TraceUAWarningSkippedRows", resourceCulture);

		public static string TraceWarning => ResourceManager.GetString("TraceWarning", resourceCulture);

		public static string TypeIsNotCommandInterceptor => ResourceManager.GetString("TypeIsNotCommandInterceptor", resourceCulture);

		public static string TypeIsNotExceptionInterceptor => ResourceManager.GetString("TypeIsNotExceptionInterceptor", resourceCulture);

		public static string UnableToConnectToHost => ResourceManager.GetString("UnableToConnectToHost", resourceCulture);

		public static string UnableToCreateAuthPlugin => ResourceManager.GetString("UnableToCreateAuthPlugin", resourceCulture);

		public static string UnableToDeriveParameters => ResourceManager.GetString("UnableToDeriveParameters", resourceCulture);

		public static string UnableToEnableQueryAnalysis => ResourceManager.GetString("UnableToEnableQueryAnalysis", resourceCulture);

		public static string UnableToEnumerateUDF => ResourceManager.GetString("UnableToEnumerateUDF", resourceCulture);

		public static string UnableToExecuteSP => ResourceManager.GetString("UnableToExecuteSP", resourceCulture);

		public static string UnableToParseFK => ResourceManager.GetString("UnableToParseFK", resourceCulture);

		public static string UnableToRetrieveParameters => ResourceManager.GetString("UnableToRetrieveParameters", resourceCulture);

		public static string UnableToStartSecondAsyncOp => ResourceManager.GetString("UnableToStartSecondAsyncOp", resourceCulture);

		public static string UnixSocketsNotSupported => ResourceManager.GetString("UnixSocketsNotSupported", resourceCulture);

		public static string UnknownAuthenticationMethod => ResourceManager.GetString("UnknownAuthenticationMethod", resourceCulture);

		public static string UnknownConnectionProtocol => ResourceManager.GetString("UnknownConnectionProtocol", resourceCulture);

		public static string ValueNotCorrectType => ResourceManager.GetString("ValueNotCorrectType", resourceCulture);

		public static string ValueNotSupportedForGuid => ResourceManager.GetString("ValueNotSupportedForGuid", resourceCulture);

		public static string WinAuthNotSupportOnPlatform => ResourceManager.GetString("WinAuthNotSupportOnPlatform", resourceCulture);

		public static string WriteToStreamFailed => ResourceManager.GetString("WriteToStreamFailed", resourceCulture);

		public static string WrongParameterName => ResourceManager.GetString("WrongParameterName", resourceCulture);

		internal Resources()
		{
		}
	}
}
