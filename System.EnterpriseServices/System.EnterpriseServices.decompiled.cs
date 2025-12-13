using System;
using System.Collections;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Transactions;
using Unity;

[assembly: ComVisible(true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("System.EnterpriseServices.dll")]
[assembly: AssemblyDescription("System.EnterpriseServices.dll")]
[assembly: AssemblyDefaultAlias("System.EnterpriseServices.dll")]
[assembly: AssemblyCompany("Mono development team")]
[assembly: AssemblyProduct("Mono Common Language Infrastructure")]
[assembly: AssemblyCopyright("(c) Various Mono authors")]
[assembly: SatelliteContractVersion("4.0.0.0")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: ComCompatibleVersion(1, 0, 3300, 0)]
[assembly: ApplicationID("1e246775-2281-484f-8ad4-044c15b86eb7")]
[assembly: ApplicationName(".NET Utilities")]
[assembly: Guid("4fb2d46f-efc8-4643-bcd0-6e5bfa6a174c")]
[assembly: CLSCompliant(true)]
[assembly: AssemblyDelaySign(true)]
[assembly: AssemblyInformationalVersion("4.6.57.0")]
[assembly: AssemblyFileVersion("4.6.57.0")]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: CompilationRelaxations(CompilationRelaxations.NoStringInterning)]
[assembly: AssemblyVersion("4.0.0.0")]
internal static class Consts
{
	public const string MonoCorlibVersion = "1A5E0066-58DC-428A-B21C-0AD6CDAE2789";

	public const string MonoVersion = "6.13.0.0";

	public const string MonoCompany = "Mono development team";

	public const string MonoProduct = "Mono Common Language Infrastructure";

	public const string MonoCopyright = "(c) Various Mono authors";

	public const string FxVersion = "4.0.0.0";

	public const string FxFileVersion = "4.6.57.0";

	public const string EnvironmentVersion = "4.0.30319.42000";

	public const string VsVersion = "0.0.0.0";

	public const string VsFileVersion = "11.0.0.0";

	private const string PublicKeyToken = "b77a5c561934e089";

	public const string AssemblyI18N = "I18N, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMicrosoft_JScript = "Microsoft.JScript, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VisualStudio = "Microsoft.VisualStudio, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VisualStudio_Web = "Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VSDesigner = "Microsoft.VSDesigner, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMono_Http = "Mono.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Posix = "Mono.Posix, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Security = "Mono.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Messaging_RabbitMQ = "Mono.Messaging.RabbitMQ, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyCorlib = "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem = "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Data = "System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Design = "System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_DirectoryServices = "System.DirectoryServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Drawing = "System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Drawing_Design = "System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Messaging = "System.Messaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Security = "System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_ServiceProcess = "System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Web = "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Windows_Forms = "System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_2_0 = "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystemCore_3_5 = "System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Core = "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string WindowsBase_3_0 = "WindowsBase, Version=3.0.0.0, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyWindowsBase = "WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationCore_3_5 = "PresentationCore, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationCore_4_0 = "PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationFramework_3_5 = "PresentationFramework, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblySystemServiceModel_3_0 = "System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
}
namespace System
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		private string comment;

		public string Comment => comment;

		public MonoTODOAttribute()
		{
		}

		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : System.MonoTODOAttribute
	{
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : System.MonoTODOAttribute
	{
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : System.MonoTODOAttribute
	{
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : System.MonoTODOAttribute
	{
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : System.MonoTODOAttribute
	{
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
namespace System.EnterpriseServices
{
	/// <summary>Specifies the level of access checking for an application, either at the process level only or at all levels, including component, interface, and method levels.</summary>
	[Serializable]
	public enum AccessChecksLevelOption
	{
		/// <summary>Enables access checks only at the process level. No access checks are made at the component, interface, or method level.</summary>
		Application,
		/// <summary>Enables access checks at every level on calls into the application.</summary>
		ApplicationComponent
	}
	/// <summary>Specifies the manner in which serviced components are activated in the application.</summary>
	[Serializable]
	public enum ActivationOption
	{
		/// <summary>Specifies that serviced components in the marked application are activated in the creator's process.</summary>
		Library,
		/// <summary>Specifies that serviced components in the marked application are activated in a system-provided process.</summary>
		Server
	}
	/// <summary>Creates an activity to do synchronous or asynchronous batch work that can use COM+ services without needing to create a COM+ component. This class cannot be inherited.</summary>
	[ComVisible(false)]
	public sealed class Activity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Activity" /> class.</summary>
		/// <param name="cfg">A <see cref="T:System.EnterpriseServices.ServiceConfig" /> that contains the configuration information for the services to be used.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///   <see cref="T:System.EnterpriseServices.Activity" /> is not supported on the current platform.</exception>
		[System.MonoTODO]
		public Activity(ServiceConfig cfg)
		{
			throw new NotImplementedException();
		}

		/// <summary>Runs the specified user-defined batch work asynchronously.</summary>
		/// <param name="serviceCall">A <see cref="T:System.EnterpriseServices.IServiceCall" /> object that is used to implement the batch work.</param>
		[System.MonoTODO]
		public void AsynchronousCall(IServiceCall serviceCall)
		{
			throw new NotImplementedException();
		}

		/// <summary>Binds the user-defined work to the current thread.</summary>
		[System.MonoTODO]
		public void BindToCurrentThread()
		{
			throw new NotImplementedException();
		}

		/// <summary>Runs the specified user-defined batch work synchronously.</summary>
		/// <param name="serviceCall">A <see cref="T:System.EnterpriseServices.IServiceCall" /> object that is used to implement the batch work.</param>
		[System.MonoTODO]
		public void SynchronousCall(IServiceCall serviceCall)
		{
			throw new NotImplementedException();
		}

		/// <summary>Unbinds the batch work that is submitted by the <see cref="M:System.EnterpriseServices.Activity.SynchronousCall(System.EnterpriseServices.IServiceCall)" /> or <see cref="M:System.EnterpriseServices.Activity.AsynchronousCall(System.EnterpriseServices.IServiceCall)" /> methods from the thread on which the batch work is running.</summary>
		[System.MonoTODO]
		public void UnbindFromThread()
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Specifies access controls to an assembly containing <see cref="T:System.EnterpriseServices.ServicedComponent" /> classes.</summary>
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Assembly)]
	public sealed class ApplicationAccessControlAttribute : Attribute, IConfigurationAttribute
	{
		private AccessChecksLevelOption accessChecksLevel;

		private AuthenticationOption authentication;

		private ImpersonationLevelOption impersonation;

		private bool val;

		/// <summary>Gets or sets the access checking level to process level or to component level.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.AccessChecksLevelOption" /> values.</returns>
		public AccessChecksLevelOption AccessChecksLevel
		{
			get
			{
				return accessChecksLevel;
			}
			set
			{
				accessChecksLevel = value;
			}
		}

		/// <summary>Gets or sets the remote procedure call (RPC) authentication level.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.AuthenticationOption" /> values.</returns>
		public AuthenticationOption Authentication
		{
			get
			{
				return authentication;
			}
			set
			{
				authentication = value;
			}
		}

		/// <summary>Gets or sets the impersonation level that is allowed for calling targets of this application.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.ImpersonationLevelOption" /> values.</returns>
		public ImpersonationLevelOption ImpersonationLevel
		{
			get
			{
				return impersonation;
			}
			set
			{
				impersonation = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to enable COM+ security configuration.</summary>
		/// <returns>
		///   <see langword="true" /> if COM+ security configuration is enabled; otherwise, <see langword="false" />.</returns>
		public bool Value
		{
			get
			{
				return val;
			}
			set
			{
				val = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationAccessControlAttribute" /> class, enabling the COM+ security configuration.</summary>
		public ApplicationAccessControlAttribute()
		{
			val = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationAccessControlAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ApplicationAccessControlAttribute.Value" /> property indicating whether to enable COM security configuration.</summary>
		/// <param name="val">
		///   <see langword="true" /> to allow configuration of security; otherwise, <see langword="false" />.</param>
		public ApplicationAccessControlAttribute(bool val)
		{
			this.val = val;
		}

		bool IConfigurationAttribute.AfterSaveChanges(Hashtable info)
		{
			return false;
		}

		[System.MonoTODO]
		bool IConfigurationAttribute.Apply(Hashtable cache)
		{
			throw new NotImplementedException();
		}

		bool IConfigurationAttribute.IsValidTarget(string s)
		{
			return s == "Application";
		}
	}
	/// <summary>Specifies whether components in the assembly run in the creator's process or in a system process.</summary>
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(false)]
	public sealed class ApplicationActivationAttribute : Attribute, IConfigurationAttribute
	{
		private ActivationOption opt;

		private string soapMailbox;

		private string soapVRoot;

		/// <summary>This property is not supported in the current version.</summary>
		/// <returns>This property is not supported in the current version.</returns>
		public string SoapMailbox
		{
			get
			{
				return soapMailbox;
			}
			set
			{
				soapMailbox = value;
			}
		}

		/// <summary>Gets or sets a value representing a virtual root on the Web for the COM+ application.</summary>
		/// <returns>The virtual root on the Web for the COM+ application.</returns>
		public string SoapVRoot
		{
			get
			{
				return soapVRoot;
			}
			set
			{
				soapVRoot = value;
			}
		}

		/// <summary>Gets the specified <see cref="T:System.EnterpriseServices.ActivationOption" /> value.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.ActivationOption" /> values, either <see langword="Library" /> or <see langword="Server" />.</returns>
		public ActivationOption Value => opt;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationActivationAttribute" /> class, setting the specified <see cref="T:System.EnterpriseServices.ActivationOption" /> value.</summary>
		/// <param name="opt">One of the <see cref="T:System.EnterpriseServices.ActivationOption" /> values.</param>
		public ApplicationActivationAttribute(ActivationOption opt)
		{
			this.opt = opt;
		}

		[System.MonoTODO]
		bool IConfigurationAttribute.AfterSaveChanges(Hashtable info)
		{
			throw new NotImplementedException();
		}

		[System.MonoTODO]
		bool IConfigurationAttribute.Apply(Hashtable cache)
		{
			throw new NotImplementedException();
		}

		bool IConfigurationAttribute.IsValidTarget(string s)
		{
			return s == "Application";
		}
	}
	/// <summary>Specifies the application ID (as a GUID) for this assembly. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(false)]
	public sealed class ApplicationIDAttribute : Attribute, IConfigurationAttribute
	{
		private Guid guid;

		/// <summary>Gets the GUID of the COM+ application.</summary>
		/// <returns>The GUID representing the COM+ application.</returns>
		public Guid Value => guid;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationIDAttribute" /> class specifying the GUID representing the application ID for the COM+ application.</summary>
		/// <param name="guid">The GUID associated with the COM+ application.</param>
		public ApplicationIDAttribute(string guid)
		{
			this.guid = new Guid(guid);
		}

		bool IConfigurationAttribute.AfterSaveChanges(Hashtable info)
		{
			return false;
		}

		bool IConfigurationAttribute.Apply(Hashtable cache)
		{
			return false;
		}

		bool IConfigurationAttribute.IsValidTarget(string s)
		{
			return s == "Application";
		}
	}
	/// <summary>Specifies the name of the COM+ application to be used for the install of the components in the assembly. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(false)]
	public sealed class ApplicationNameAttribute : Attribute, IConfigurationAttribute
	{
		private string name;

		/// <summary>Gets a value indicating the name of the COM+ application that contains the components in the assembly.</summary>
		/// <returns>The name of the COM+ application.</returns>
		public string Value => name;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationNameAttribute" /> class, specifying the name of the COM+ application to be used for the install of the components.</summary>
		/// <param name="name">The name of the COM+ application.</param>
		public ApplicationNameAttribute(string name)
		{
			this.name = name;
		}

		bool IConfigurationAttribute.AfterSaveChanges(Hashtable info)
		{
			return false;
		}

		[System.MonoTODO]
		bool IConfigurationAttribute.Apply(Hashtable cache)
		{
			throw new NotImplementedException();
		}

		bool IConfigurationAttribute.IsValidTarget(string s)
		{
			return s == "Application";
		}
	}
	/// <summary>Enables queuing support for the marked assembly and enables the application to read method calls from Message Queuing queues. This class cannot be inherited.</summary>
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Assembly)]
	public sealed class ApplicationQueuingAttribute : Attribute
	{
		private bool enabled;

		private int maxListenerThreads;

		private bool queueListenerEnabled;

		/// <summary>Gets or sets a value indicating whether queuing support is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if queuing support is enabled; otherwise, <see langword="false" />. The default value set by the constructor is <see langword="true" />.</returns>
		public bool Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				enabled = value;
			}
		}

		/// <summary>Gets or sets the number of threads used to extract messages from the queue and activate the corresponding component.</summary>
		/// <returns>The maximum number of threads to use for processing messages arriving in the queue. The default is zero.</returns>
		public int MaxListenerThreads
		{
			get
			{
				return maxListenerThreads;
			}
			set
			{
				maxListenerThreads = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the application will accept queued component calls from clients.</summary>
		/// <returns>
		///   <see langword="true" /> if the application accepts queued component calls; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public bool QueueListenerEnabled
		{
			get
			{
				return queueListenerEnabled;
			}
			set
			{
				queueListenerEnabled = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationQueuingAttribute" /> class, enabling queuing support for the assembly and initializing <see cref="P:System.EnterpriseServices.ApplicationQueuingAttribute.Enabled" />, <see cref="P:System.EnterpriseServices.ApplicationQueuingAttribute.QueueListenerEnabled" />, and <see cref="P:System.EnterpriseServices.ApplicationQueuingAttribute.MaxListenerThreads" />.</summary>
		public ApplicationQueuingAttribute()
		{
			enabled = true;
			queueListenerEnabled = false;
			maxListenerThreads = 0;
		}
	}
	/// <summary>Specifies the remote procedure call (RPC) authentication mechanism. Applicable only when the <see cref="T:System.EnterpriseServices.ActivationOption" /> is set to <see langword="Server" />.</summary>
	[Serializable]
	public enum AuthenticationOption
	{
		/// <summary>Authenticates credentials at the beginning of every call.</summary>
		Call = 3,
		/// <summary>Authenticates credentials only when the connection is made.</summary>
		Connect = 2,
		/// <summary>Uses the default authentication level for the specified authentication service. In COM+, this setting is provided by the <see langword="DefaultAuthenticationLevel" /> property in the <see langword="LocalComputer" /> collection.</summary>
		Default = 0,
		/// <summary>Authenticates credentials and verifies that no call data has been modified in transit.</summary>
		Integrity = 5,
		/// <summary>Authentication does not occur.</summary>
		None = 1,
		/// <summary>Authenticates credentials and verifies that all call data is received.</summary>
		Packet = 4,
		/// <summary>Authenticates credentials and encrypts the packet, including the data and the sender's identity and signature.</summary>
		Privacy = 6
	}
	/// <summary>Marks the attributed method as an <see langword="AutoComplete" /> object. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(false)]
	public sealed class AutoCompleteAttribute : Attribute
	{
		private bool val;

		/// <summary>Gets a value indicating the setting of the <see langword="AutoComplete" /> option in COM+.</summary>
		/// <returns>
		///   <see langword="true" /> if <see langword="AutoComplete" /> is enabled in COM+; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		public bool Value => val;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.AutoCompleteAttribute" /> class, specifying that the application should automatically call <see cref="M:System.EnterpriseServices.ContextUtil.SetComplete" /> if the transaction completes successfully.</summary>
		public AutoCompleteAttribute()
		{
			val = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.AutoCompleteAttribute" /> class, specifying whether COM+ <see langword="AutoComplete" /> is enabled.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable <see langword="AutoComplete" /> in the COM+ object; otherwise, <see langword="false" />.</param>
		public AutoCompleteAttribute(bool val)
		{
			this.val = val;
		}
	}
	/// <summary>Represents the unit of work associated with a transaction. This structure is used in <see cref="T:System.EnterpriseServices.XACTTRANSINFO" />.</summary>
	[ComVisible(false)]
	public struct BOID
	{
		/// <summary>Represents an array that contains the data.</summary>
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] rgb;
	}
	/// <summary>Wraps the COM+ <see langword="ByotServerEx" /> class and the COM+ DTC interfaces <see langword="ICreateWithTransactionEx" /> and <see langword="ICreateWithTipTransactionEx" />. This class cannot be inherited.</summary>
	public sealed class BYOT
	{
		private BYOT()
		{
		}

		/// <summary>Creates an object that is enlisted within a manual transaction using the Transaction Internet Protocol (TIP).</summary>
		/// <param name="url">A TIP URL that specifies a transaction.</param>
		/// <param name="t">The type.</param>
		/// <returns>The requested transaction.</returns>
		[System.MonoTODO]
		public static object CreateWithTipTransaction(string url, Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an object that is enlisted within a manual transaction.</summary>
		/// <param name="transaction">The <see cref="T:System.EnterpriseServices.ITransaction" /> or <see cref="T:System.Transactions.Transaction" /> object that specifies a transaction.</param>
		/// <param name="t">The specified type.</param>
		/// <returns>The requested transaction.</returns>
		[System.MonoTODO]
		public static object CreateWithTransaction(object transaction, Type t)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Indicates whether all work submitted by <see cref="T:System.EnterpriseServices.Activity" /> should be bound to only one single-threaded apartment (STA). This enumeration has no impact on the multithreaded apartment (MTA).</summary>
	[Serializable]
	[ComVisible(false)]
	public enum BindingOption
	{
		/// <summary>The work submitted by the activity is not bound to a single STA.</summary>
		NoBinding,
		/// <summary>The work submitted by the activity is bound to a single STA.</summary>
		BindingToPoolThread
	}
	/// <summary>Enables you to pass context properties from the COM Transaction Integrator (COMTI) into the COM+ context.</summary>
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class COMTIIntrinsicsAttribute : Attribute
	{
		private bool val;

		/// <summary>Gets a value indicating whether the COM Transaction Integrator (COMTI) context properties are passed into the COM+ context.</summary>
		/// <returns>
		///   <see langword="true" /> if the COMTI context properties are passed into the COM+ context; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		public bool Value => val;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.COMTIIntrinsicsAttribute" /> class, setting the <see cref="P:System.EnterpriseServices.COMTIIntrinsicsAttribute.Value" /> property to <see langword="true" />.</summary>
		public COMTIIntrinsicsAttribute()
		{
			val = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.COMTIIntrinsicsAttribute" /> class, enabling the setting of the <see cref="P:System.EnterpriseServices.COMTIIntrinsicsAttribute.Value" /> property.</summary>
		/// <param name="val">
		///   <see langword="true" /> if the COMTI context properties are passed into the COM+ context; otherwise, <see langword="false" />.</param>
		public COMTIIntrinsicsAttribute(bool val)
		{
			this.val = val;
		}
	}
	/// <summary>Enables security checking on calls to a component. This class cannot be inherited.</summary>
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ComponentAccessControlAttribute : Attribute
	{
		private bool val;

		/// <summary>Gets a value indicating whether to enable security checking on calls to a component.</summary>
		/// <returns>
		///   <see langword="true" /> if security checking is to be enabled; otherwise, <see langword="false" />.</returns>
		public bool Value => val;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ComponentAccessControlAttribute" /> class.</summary>
		public ComponentAccessControlAttribute()
		{
			val = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ComponentAccessControlAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ComponentAccessControlAttribute.Value" /> property to indicate whether to enable COM+ security configuration.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable security checking on calls to a component; otherwise, <see langword="false" />.</param>
		public ComponentAccessControlAttribute(bool val)
		{
			this.val = val;
		}
	}
	/// <summary>Enables COM+ object construction support. This class cannot be inherited.</summary>
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ConstructionEnabledAttribute : Attribute
	{
		private string def;

		private bool enabled;

		/// <summary>Gets or sets a default value for the constructor string.</summary>
		/// <returns>The value to be used for the default constructor string. The default is an empty string ("").</returns>
		public string Default
		{
			get
			{
				return def;
			}
			set
			{
				def = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether COM+ object construction support is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if COM+ object construction support is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		public bool Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				enabled = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ConstructionEnabledAttribute" /> class and initializes the default settings for <see cref="P:System.EnterpriseServices.ConstructionEnabledAttribute.Enabled" /> and <see cref="P:System.EnterpriseServices.ConstructionEnabledAttribute.Default" />.</summary>
		public ConstructionEnabledAttribute()
		{
			def = string.Empty;
			enabled = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ConstructionEnabledAttribute" /> class, setting <see cref="P:System.EnterpriseServices.ConstructionEnabledAttribute.Enabled" /> to the specified value.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable COM+ object construction support; otherwise, <see langword="false" />.</param>
		public ConstructionEnabledAttribute(bool val)
		{
			def = string.Empty;
			enabled = val;
		}
	}
	/// <summary>Obtains information about the COM+ object context. This class cannot be inherited.</summary>
	public sealed class ContextUtil
	{
		private static bool deactivateOnReturn;

		private static TransactionVote myTransactionVote;

		/// <summary>Gets a GUID representing the activity containing the component.</summary>
		/// <returns>The GUID for an activity if the current context is part of an activity; otherwise, <see langword="GUID_NULL" />.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		public static Guid ActivityId
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a GUID for the current application.</summary>
		/// <returns>The GUID for the current application.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows XP or later.</exception>
		public static Guid ApplicationId
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a GUID for the current application instance.</summary>
		/// <returns>The GUID for the current application instance.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows XP or later.</exception>
		public static Guid ApplicationInstanceId
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a GUID for the current context.</summary>
		/// <returns>The GUID for the current context.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		public static Guid ContextId
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the <see langword="done" /> bit in the COM+ context.</summary>
		/// <returns>
		///   <see langword="true" /> if the object is to be deactivated when the method returns; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		public static bool DeactivateOnReturn
		{
			get
			{
				return deactivateOnReturn;
			}
			set
			{
				deactivateOnReturn = value;
			}
		}

		/// <summary>Gets a value that indicates whether the current context is transactional.</summary>
		/// <returns>
		///   <see langword="true" /> if the current context is transactional; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		public static bool IsInTransaction
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether role-based security is active in the current context.</summary>
		/// <returns>
		///   <see langword="true" /> if the current context has security enabled; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		public static bool IsSecurityEnabled
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the <see langword="consistent" /> bit in the COM+ context.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.TransactionVote" /> values, either <see langword="Commit" /> or <see langword="Abort" />.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		[System.MonoTODO]
		public static TransactionVote MyTransactionVote
		{
			get
			{
				return myTransactionVote;
			}
			set
			{
				myTransactionVote = value;
			}
		}

		/// <summary>Gets a GUID for the current partition.</summary>
		/// <returns>The GUID for the current partition.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows XP or later.</exception>
		public static Guid PartitionId
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an object describing the current COM+ DTC transaction.</summary>
		/// <returns>An object that represents the current transaction.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		public static object Transaction
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the current transaction context.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> that represents the current transaction context.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		public static Transaction SystemTransaction
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the GUID of the current COM+ DTC transaction.</summary>
		/// <returns>A GUID representing the current COM+ DTC transaction, if one exists.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		public static Guid TransactionId
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		internal ContextUtil()
		{
		}

		/// <summary>Sets both the <see langword="consistent" /> bit and the <see langword="done" /> bit to <see langword="false" /> in the COM+ context.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">No COM+ context is available.</exception>
		[System.MonoTODO]
		public static void DisableCommit()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the <see langword="consistent" /> bit to <see langword="true" /> and the <see langword="done" /> bit to <see langword="false" /> in the COM+ context.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">No COM+ context is available.</exception>
		[System.MonoTODO]
		public static void EnableCommit()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a named property from the COM+ context.</summary>
		/// <param name="name">The name of the requested property.</param>
		/// <returns>The named property for the context.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		[System.MonoTODO]
		public static object GetNamedProperty(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the caller is in the specified role.</summary>
		/// <param name="role">The name of the role to check.</param>
		/// <returns>
		///   <see langword="true" /> if the caller is in the specified role; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		[System.MonoTODO]
		public static bool IsCallerInRole(string role)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the serviced component is activated in the default context. Serviced components that do not have COM+ catalog information are activated in the default context.</summary>
		/// <returns>
		///   <see langword="true" /> if the serviced component is activated in the default context; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public static bool IsDefaultContext()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the <see langword="consistent" /> bit to <see langword="false" /> and the <see langword="done" /> bit to <see langword="true" /> in the COM+ context.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		[System.MonoTODO]
		public static void SetAbort()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the <see langword="consistent" /> bit and the <see langword="done" /> bit to <see langword="true" /> in the COM+ context.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		[System.MonoTODO]
		public static void SetComplete()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the named property for the COM+ context.</summary>
		/// <param name="name">The name of the property to set.</param>
		/// <param name="value">Object that represents the property value to set.</param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		[System.MonoTODO]
		public static void SetNamedProperty(string name, object value)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Sets the description on an assembly (application), component, method, or interface. This class cannot be inherited.</summary>
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface)]
	public sealed class DescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.DescriptionAttribute" /> class.</summary>
		/// <param name="desc">The description of the assembly (application), component, method, or interface.</param>
		public DescriptionAttribute(string desc)
		{
		}
	}
	/// <summary>Marks the attributed class as an event class. This class cannot be inherited.</summary>
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class EventClassAttribute : Attribute
	{
		private bool allowInProcSubscribers;

		private bool fireInParallel;

		private string publisherFilter;

		/// <summary>Gets or sets a value that indicates whether subscribers can be activated in the publisher's process.</summary>
		/// <returns>
		///   <see langword="true" /> if subscribers can be activated in the publisher's process; otherwise, <see langword="false" />.</returns>
		public bool AllowInprocSubscribers
		{
			get
			{
				return allowInProcSubscribers;
			}
			set
			{
				allowInProcSubscribers = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether events are to be delivered to subscribers in parallel.</summary>
		/// <returns>
		///   <see langword="true" /> if events are to be delivered to subscribers in parallel; otherwise, <see langword="false" />.</returns>
		public bool FireInParallel
		{
			get
			{
				return fireInParallel;
			}
			set
			{
				fireInParallel = value;
			}
		}

		/// <summary>Gets or sets a publisher filter for an event method.</summary>
		/// <returns>The publisher filter.</returns>
		public string PublisherFilter
		{
			get
			{
				return publisherFilter;
			}
			set
			{
				publisherFilter = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.EventClassAttribute" /> class.</summary>
		public EventClassAttribute()
		{
			allowInProcSubscribers = true;
			fireInParallel = false;
			publisherFilter = null;
		}
	}
	/// <summary>Enables event tracking for a component. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class EventTrackingEnabledAttribute : Attribute
	{
		private bool val;

		/// <summary>Gets the value of the <see cref="P:System.EnterpriseServices.EventTrackingEnabledAttribute.Value" /> property, which indicates whether tracking is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if tracking is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		public bool Value => val;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.EventTrackingEnabledAttribute" /> class, enabling event tracking.</summary>
		public EventTrackingEnabledAttribute()
		{
			val = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.EventTrackingEnabledAttribute" /> class, optionally disabling event tracking.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable event tracking; otherwise, <see langword="false" />.</param>
		public EventTrackingEnabledAttribute(bool val)
		{
			this.val = val;
		}
	}
	/// <summary>Sets the queuing exception class for the queued class. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class ExceptionClassAttribute : Attribute
	{
		private string name;

		/// <summary>Gets the name of the exception class for the player to activate and play back before the message is routed to the dead letter queue.</summary>
		/// <returns>The name of the exception class for the player to activate and play back before the message is routed to the dead letter queue.</returns>
		public string Value => name;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ExceptionClassAttribute" /> class.</summary>
		/// <param name="name">The name of the exception class for the player to activate and play back before the message is routed to the dead letter queue.</param>
		public ExceptionClassAttribute(string name)
		{
			this.name = name;
		}
	}
	/// <summary>Implements error trapping on the asynchronous batch work that is submitted by the <see cref="T:System.EnterpriseServices.Activity" /> object.</summary>
	[ComImport]
	[Guid("FE6777FB-A674-4177-8F32-6D707E113484")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IAsyncErrorNotify
	{
		/// <summary>Handles errors for asynchronous batch work.</summary>
		/// <param name="hresult">The HRESULT of the error that occurred while the batch work was running asynchronously.</param>
		void OnError(int hresult);
	}
	internal interface IConfigurationAttribute
	{
		bool AfterSaveChanges(Hashtable info);

		bool Apply(Hashtable info);

		bool IsValidTarget(string s);
	}
	/// <summary>Enables access to ASP intrinsic values from <see cref="M:System.EnterpriseServices.ContextUtil.GetNamedProperty(System.String)" />. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class IISIntrinsicsAttribute : Attribute
	{
		private bool val;

		/// <summary>Gets a value that indicates whether access to the ASP intrinsic values is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if access is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		public bool Value => val;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.IISIntrinsicsAttribute" /> class, enabling access to the ASP intrinsic values.</summary>
		public IISIntrinsicsAttribute()
		{
			val = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.IISIntrinsicsAttribute" /> class, optionally disabling access to the ASP intrinsic values.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable access to the ASP intrinsic values; otherwise, <see langword="false" />.</param>
		public IISIntrinsicsAttribute(bool val)
		{
			this.val = val;
		}
	}
	/// <summary>Functions in Queued Components in the abnormal handling of server-side playback errors and client-side failures of the Message Queuing delivery mechanism.</summary>
	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("51372AFD-CAE7-11CF-BE81-00AA00A2FA25")]
	public interface IPlaybackControl
	{
		/// <summary>Informs the client-side exception-handling component that all Message Queuing attempts to deliver the message to the server were rejected, and the message ended up on the client-side Xact Dead Letter queue.</summary>
		void FinalClientRetry();

		/// <summary>Informs the server-side exception class implementation that all attempts to play back the deferred activation to the server have failed, and the message is about to be moved to its final resting queue.</summary>
		void FinalServerRetry();
	}
	/// <summary>Supports setting the time-out for the <see cref="M:System.EnterpriseServices.IProcessInitializer.Startup(System.Object)" /> method.</summary>
	[ComImport]
	[Guid("72380d55-8d2b-43a3-8513-2b6ef31434e9")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IProcessInitControl
	{
		/// <summary>Sets the number of seconds remaining before the <see cref="M:System.EnterpriseServices.IProcessInitializer.Startup(System.Object)" /> method times out.</summary>
		/// <param name="dwSecondsRemaining">The number of seconds that remain before the time taken to execute the start up method times out.</param>
		void ResetInitializerTimeout(int dwSecondsRemaining);
	}
	/// <summary>Supports methods that can be called when a COM component starts up or shuts down.</summary>
	[ComImport]
	[Guid("1113f52d-dc7f-4943-aed6-88d04027e32a")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IProcessInitializer
	{
		/// <summary>Performs shutdown actions. Called when Dllhost.exe is shut down.</summary>
		void Shutdown();

		/// <summary>Performs initialization at startup. Called when Dllhost.exe is started.</summary>
		/// <param name="punkProcessControl">In Microsoft Windows XP, a pointer to the <see langword="IUnknown" /> interface of the COM component starting up. In Microsoft Windows 2000, this argument is always <see langword="null" />.</param>
		void Startup([In][MarshalAs(UnmanagedType.IUnknown)] object punkProcessControl);
	}
	/// <summary>Installs and configures assemblies in the COM+ catalog.</summary>
	[Guid("55e3ea25-55cb-4650-8887-18e8d30bb4bc")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IRegistrationHelper
	{
		/// <summary>Installs the assembly into the COM+ catalog.</summary>
		/// <param name="assembly">The assembly name as a file or the strong name of an assembly in the global assembly cache (GAC).</param>
		/// <param name="application">The application parameter can be <see langword="null" />. If it is, the name of the application is automatically generated based on the name of the assembly or the <see langword="ApplicationName" /> attribute. If the application contains an <see langword="ApplicationID" /> attribute, the attribute takes precedence.</param>
		/// <param name="tlb">The name of the output type library (TLB) file, or a string containing <see langword="null" /> if the registration helper is expected to generate the name. On call completion, the actual name used is placed in the parameter.</param>
		/// <param name="installFlags">The installation options specified in the enumeration.</param>
		void InstallAssembly([In][MarshalAs(UnmanagedType.BStr)] string assembly, [In][Out][MarshalAs(UnmanagedType.BStr)] ref string application, [In][Out][MarshalAs(UnmanagedType.BStr)] ref string tlb, [In] InstallationFlags installFlags);

		/// <summary>Uninstalls the assembly from the COM+ catalog.</summary>
		/// <param name="assembly">The assembly name as a file or the strong name of an assembly in the global assembly cache (GAC).</param>
		/// <param name="application">The name of the COM+ application.</param>
		void UninstallAssembly([In][MarshalAs(UnmanagedType.BStr)] string assembly, [In][MarshalAs(UnmanagedType.BStr)] string application);
	}
	/// <summary>Implemented by the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class to determine if the <see cref="T:System.EnterpriseServices.AutoCompleteAttribute" /> class attribute is set to <see langword="true" /> or <see langword="false" /> for a remote method invocation.</summary>
	[Guid("6619a740-8154-43be-a186-0319578e02db")]
	public interface IRemoteDispatch
	{
		/// <summary>Ensures that, in the COM+ context, the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class object's done bit is set to <see langword="true" /> after a remote method invocation.</summary>
		/// <param name="s">A string to be converted into a request object that implements the <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface.</param>
		/// <returns>A string converted from a response object that implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface.</returns>
		[AutoComplete]
		string RemoteDispatchAutoDone(string s);

		/// <summary>Does not ensure that, in the COM+ context, the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class object's done bit is set to <see langword="true" /> after a remote method invocation.</summary>
		/// <param name="s">A string to be converted into a request object implementing the <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface.</param>
		/// <returns>A string converted from a response object implementing the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface.</returns>
		[AutoComplete(false)]
		string RemoteDispatchNotAutoDone(string s);
	}
	internal interface ISecurityCallContext
	{
		int Count { get; }

		void GetEnumerator(ref IEnumerator enumerator);

		object GetItem(string user);

		bool IsCallerInRole(string role);

		bool IsSecurityEnabled();

		bool IsUserInRole(ref object user, string role);
	}
	internal interface ISecurityCallersColl
	{
		int Count { get; }

		void GetEnumerator(out IEnumerator enumerator);

		ISecurityIdentityColl GetItem(int idx);
	}
	internal interface ISecurityIdentityColl
	{
		int Count { get; }

		void GetEnumerator(out IEnumerator enumerator);

		SecurityIdentity GetItem(int idx);
	}
	/// <summary>Implements the batch work that is submitted through the activity created by <see cref="T:System.EnterpriseServices.Activity" />.</summary>
	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("BD3E2E12-42DD-40f4-A09A-95A50C58304B")]
	public interface IServiceCall
	{
		/// <summary>Starts the execution of the batch work implemented in this method.</summary>
		void OnCall();
	}
	/// <summary>Implemented by the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class to obtain information about the component via the <see cref="M:System.EnterpriseServices.IServicedComponentInfo.GetComponentInfo(System.Int32@,System.String[]@)" /> method.</summary>
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("8165B19E-8D3A-4d0b-80C8-97DE310DB583")]
	public interface IServicedComponentInfo
	{
		/// <summary>Obtains certain information about the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class instance.</summary>
		/// <param name="infoMask">A bitmask where 0x00000001 is a key for the serviced component's process ID, 0x00000002 is a key for the application domain ID, and 0x00000004 is a key for the serviced component's remote URI.</param>
		/// <param name="infoArray">A string array that may contain any or all of the following, in order: the serviced component's process ID, the application domain ID, and the serviced component's remote URI.</param>
		void GetComponentInfo(ref int infoMask, out string[] infoArray);
	}
	internal interface ISharedProperty
	{
		object Value { get; set; }
	}
	internal interface ISharedPropertyGroup
	{
		ISharedProperty CreateProperty(string name, out bool fExists);

		ISharedProperty CreatePropertyByPosition(int position, out bool fExists);

		ISharedProperty Property(string name);

		ISharedProperty PropertyByPosition(int position);
	}
	/// <summary>Corresponds to the Distributed Transaction Coordinator (DTC) <see langword="ITransaction" /> interface and is supported by objects obtained through <see cref="P:System.EnterpriseServices.ContextUtil.Transaction" />.</summary>
	[ComImport]
	[Guid("0FB15084-AF41-11CE-BD2B-204C4F4F5020")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface ITransaction
	{
		/// <summary>Aborts the transaction.</summary>
		/// <param name="pboidReason">An optional <see cref="T:System.EnterpriseServices.BOID" /> that indicates why the transaction is being aborted. This parameter can be <see langword="null" />, indicating that no reason for the abort is provided.</param>
		/// <param name="fRetaining">Must be <see langword="false" />.</param>
		/// <param name="fAsync">When <paramref name="fAsync" /> is <see langword="true" />, an asynchronous abort is performed and the caller must use <see langword="ITransactionOutcomeEvents" /> to learn the outcome of the transaction.</param>
		void Abort(ref BOID pboidReason, int fRetaining, int fAsync);

		/// <summary>Commits the transaction.</summary>
		/// <param name="fRetaining">Must be <see langword="false" />.</param>
		/// <param name="grfTC">A value taken from the OLE DB enumeration <see langword="XACTTC" />.</param>
		/// <param name="grfRM">Must be zero.</param>
		void Commit(int fRetaining, int grfTC, int grfRM);

		/// <summary>Returns information about a transaction object.</summary>
		/// <param name="pinfo">Pointer to the caller-allocated <see cref="T:System.EnterpriseServices.XACTTRANSINFO" /> structure that will receive information about the transaction. Must not be <see langword="null" />.</param>
		void GetTransactionInfo(out XACTTRANSINFO pinfo);
	}
	/// <summary>Specifies the level of impersonation allowed when calling targets of a server application.</summary>
	[Serializable]
	public enum ImpersonationLevelOption
	{
		/// <summary>The client is anonymous to the server. The server process can impersonate the client, but the impersonation token does not contain any information about the client.</summary>
		Anonymous = 1,
		/// <summary>Uses the default impersonation level for the specified authentication service. In COM+, this setting is provided by the <see langword="DefaultImpersonationLevel" /> property in the <see langword="LocalComputer" /> collection.</summary>
		Default = 0,
		/// <summary>The most powerful impersonation level. When this level is selected, the server (whether local or remote) can impersonate the client's security context while acting on behalf of the client</summary>
		Delegate = 4,
		/// <summary>The system default level. The server can obtain the client's identity, and the server can impersonate the client to do ACL checks.</summary>
		Identify = 2,
		/// <summary>The server can impersonate the client's security context while acting on behalf of the client. The server can access local resources as the client.</summary>
		Impersonate = 3
	}
	/// <summary>Indicates whether to create a new context based on the current context or on the information in <see cref="T:System.EnterpriseServices.ServiceConfig" />.</summary>
	[Serializable]
	[ComVisible(false)]
	public enum InheritanceOption
	{
		/// <summary>The new context is created from the existing context. <see cref="F:System.EnterpriseServices.InheritanceOption.Inherit" /> is the default value for <see cref="P:System.EnterpriseServices.ServiceConfig.Inheritance" />.</summary>
		Inherit,
		/// <summary>The new context is created from the default context.</summary>
		Ignore
	}
	/// <summary>Flags used with the <see cref="T:System.EnterpriseServices.RegistrationHelper" /> class.</summary>
	[Serializable]
	[Flags]
	public enum InstallationFlags
	{
		/// <summary>Should not be used.</summary>
		Configure = 0x400,
		/// <summary>Configures components only, do not configure methods or interfaces.</summary>
		ConfigureComponentsOnly = 0x10,
		/// <summary>Creates the target application. An error occurs if the target already exists.</summary>
		CreateTargetApplication = 2,
		/// <summary>Do the default installation, which configures, installs, and registers, and assumes that the application already exists.</summary>
		Default = 0,
		/// <summary>Do not export the type library; one can be found either by the generated or supplied type library name.</summary>
		ExpectExistingTypeLib = 1,
		/// <summary>Creates the application if it does not exist; otherwise use the existing application.</summary>
		FindOrCreateTargetApplication = 4,
		/// <summary>Should not be used.</summary>
		Install = 0x200,
		/// <summary>If using an existing application, ensures that the properties on this application match those in the assembly.</summary>
		ReconfigureExistingApplication = 8,
		/// <summary>Should not be used.</summary>
		Register = 0x100,
		/// <summary>When alert text is encountered, writes it to the Console.</summary>
		ReportWarningsToConsole = 0x20
	}
	/// <summary>Enables queuing support for the marked interface. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
	[ComVisible(false)]
	public sealed class InterfaceQueuingAttribute : Attribute
	{
		private bool enabled;

		private string interfaceName;

		/// <summary>Gets or sets a value indicating whether queuing support is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if queuing support is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		public bool Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				enabled = value;
			}
		}

		/// <summary>Gets or sets the name of the interface on which queuing is enabled.</summary>
		/// <returns>The name of the interface on which queuing is enabled.</returns>
		public string Interface
		{
			get
			{
				return interfaceName;
			}
			set
			{
				interfaceName = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.InterfaceQueuingAttribute" /> class setting the <see cref="P:System.EnterpriseServices.InterfaceQueuingAttribute.Enabled" /> and <see cref="P:System.EnterpriseServices.InterfaceQueuingAttribute.Interface" /> properties to their default values.</summary>
		public InterfaceQueuingAttribute()
			: this(enabled: true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.InterfaceQueuingAttribute" /> class, optionally disabling queuing support.</summary>
		/// <param name="enabled">
		///   <see langword="true" /> to enable queuing support; otherwise, <see langword="false" />.</param>
		public InterfaceQueuingAttribute(bool enabled)
		{
			this.enabled = enabled;
			interfaceName = null;
		}
	}
	/// <summary>Turns just-in-time (JIT) activation on or off. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class JustInTimeActivationAttribute : Attribute
	{
		private bool val;

		/// <summary>Gets the value of the <see cref="T:System.EnterpriseServices.JustInTimeActivationAttribute" /> setting.</summary>
		/// <returns>
		///   <see langword="true" /> if JIT activation is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		public bool Value => val;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.JustInTimeActivationAttribute" /> class. The default constructor enables just-in-time (JIT) activation.</summary>
		public JustInTimeActivationAttribute()
			: this(val: true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.JustInTimeActivationAttribute" /> class, optionally allowing the disabling of just-in-time (JIT) activation by passing <see langword="false" /> as the parameter.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable JIT activation; otherwise, <see langword="false" />.</param>
		public JustInTimeActivationAttribute(bool val)
		{
			this.val = val;
		}
	}
	/// <summary>Determines whether the component participates in load balancing, if the component load balancing service is installed and enabled on the server.</summary>
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class LoadBalancingSupportedAttribute : Attribute
	{
		private bool val;

		/// <summary>Gets a value that indicates whether load balancing support is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if load balancing support is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		public bool Value => val;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.LoadBalancingSupportedAttribute" /> class, specifying load balancing support.</summary>
		public LoadBalancingSupportedAttribute()
			: this(val: true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.LoadBalancingSupportedAttribute" /> class, optionally disabling load balancing support.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable load balancing support; otherwise, <see langword="false" />.</param>
		public LoadBalancingSupportedAttribute(bool val)
		{
			this.val = val;
		}
	}
	/// <summary>Forces the attributed object to be created in the context of the creator, if possible. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class MustRunInClientContextAttribute : Attribute
	{
		private bool val;

		/// <summary>Gets a value that indicates whether the attributed object is to be created in the context of the creator.</summary>
		/// <returns>
		///   <see langword="true" /> if the object is to be created in the context of the creator; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		public bool Value => val;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.MustRunInClientContextAttribute" /> class, requiring creation of the object in the context of the creator.</summary>
		public MustRunInClientContextAttribute()
			: this(val: true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.MustRunInClientContextAttribute" /> class, optionally not creating the object in the context of the creator.</summary>
		/// <param name="val">
		///   <see langword="true" /> to create the object in the context of the creator; otherwise, <see langword="false" />.</param>
		public MustRunInClientContextAttribute(bool val)
		{
			this.val = val;
		}
	}
	/// <summary>Enables and configures object pooling for a component. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class ObjectPoolingAttribute : Attribute, IConfigurationAttribute
	{
		private int creationTimeout;

		private bool enabled;

		private int minPoolSize;

		private int maxPoolSize;

		/// <summary>Gets or sets the length of time to wait for an object to become available in the pool before throwing an exception. This value is in milliseconds.</summary>
		/// <returns>The time-out value in milliseconds.</returns>
		public int CreationTimeout
		{
			get
			{
				return creationTimeout;
			}
			set
			{
				creationTimeout = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether object pooling is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if object pooling is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		public bool Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				enabled = value;
			}
		}

		/// <summary>Gets or sets the value for the maximum size of the pool.</summary>
		/// <returns>The maximum number of objects in the pool.</returns>
		public int MaxPoolSize
		{
			get
			{
				return maxPoolSize;
			}
			set
			{
				maxPoolSize = value;
			}
		}

		/// <summary>Gets or sets the value for the minimum size of the pool.</summary>
		/// <returns>The minimum number of objects in the pool.</returns>
		public int MinPoolSize
		{
			get
			{
				return minPoolSize;
			}
			set
			{
				minPoolSize = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.Enabled" />, <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MaxPoolSize" />, <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MinPoolSize" />, and <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.CreationTimeout" /> properties to their default values.</summary>
		public ObjectPoolingAttribute()
			: this(enable: true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.Enabled" /> property.</summary>
		/// <param name="enable">
		///   <see langword="true" /> to enable object pooling; otherwise, <see langword="false" />.</param>
		public ObjectPoolingAttribute(bool enable)
		{
			enabled = enable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MaxPoolSize" /> and <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MinPoolSize" /> properties.</summary>
		/// <param name="minPoolSize">The minimum pool size.</param>
		/// <param name="maxPoolSize">The maximum pool size.</param>
		public ObjectPoolingAttribute(int minPoolSize, int maxPoolSize)
			: this(enable: true, minPoolSize, maxPoolSize)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.Enabled" />, <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MaxPoolSize" />, and <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MinPoolSize" /> properties.</summary>
		/// <param name="enable">
		///   <see langword="true" /> to enable object pooling; otherwise, <see langword="false" />.</param>
		/// <param name="minPoolSize">The minimum pool size.</param>
		/// <param name="maxPoolSize">The maximum pool size.</param>
		public ObjectPoolingAttribute(bool enable, int minPoolSize, int maxPoolSize)
		{
			enabled = enable;
			this.minPoolSize = minPoolSize;
			this.maxPoolSize = maxPoolSize;
		}

		/// <summary>Called internally by the .NET Framework infrastructure while installing and configuring assemblies in the COM+ catalog.</summary>
		/// <param name="info">A hash table that contains internal objects referenced by internal keys.</param>
		/// <returns>
		///   <see langword="true" /> if the method has made changes.</returns>
		[System.MonoTODO]
		public bool AfterSaveChanges(Hashtable info)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called internally by the .NET Framework infrastructure while applying the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class attribute to a serviced component.</summary>
		/// <param name="info">A hash table that contains an internal object to which object pooling properties are applied, referenced by an internal key.</param>
		/// <returns>
		///   <see langword="true" /> if the method has made changes; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool Apply(Hashtable info)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called internally by the .NET Framework infrastructure while installing and configuring assemblies in the COM+ catalog.</summary>
		/// <param name="s">A string generated by the .NET Framework infrastructure that is checked for a special value that indicates a serviced component.</param>
		/// <returns>
		///   <see langword="true" /> if the attribute is applied to a serviced component class.</returns>
		[System.MonoTODO]
		public bool IsValidTarget(string s)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Indicates the context in which to run the COM+ partition.</summary>
	[Serializable]
	[ComVisible(false)]
	public enum PartitionOption
	{
		/// <summary>The enclosed context runs in the Global Partition. <see cref="F:System.EnterpriseServices.PartitionOption.Ignore" /> is the default setting for <see cref="P:System.EnterpriseServices.ServiceConfig.PartitionOption" /> when <see cref="P:System.EnterpriseServices.ServiceConfig.Inheritance" /> is set to <see cref="F:System.EnterpriseServices.InheritanceOption.Ignore" />.</summary>
		Ignore,
		/// <summary>The enclosed context runs in the current containing COM+ partition. This is the default setting for <see cref="P:System.EnterpriseServices.ServiceConfig.PartitionOption" /> when <see cref="P:System.EnterpriseServices.ServiceConfig.Inheritance" /> is set to <see cref="F:System.EnterpriseServices.InheritanceOption.Inherit" />.</summary>
		Inherit,
		/// <summary>The enclosed context runs in a COM+ partition that is different from the current containing partition.</summary>
		New
	}
	/// <summary>Identifies a component as a private component that is only seen and activated by components in the same application. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class PrivateComponentAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.PrivateComponentAttribute" /> class.</summary>
		public PrivateComponentAttribute()
		{
		}
	}
	/// <summary>Specifies the mode for accessing shared properties in the shared property group manager.</summary>
	[Serializable]
	[ComVisible(false)]
	public enum PropertyLockMode
	{
		/// <summary>Locks all the properties in the shared property group for exclusive use by the caller, as long as the caller's current method is executing.</summary>
		Method = 1,
		/// <summary>Locks a property during a get or set, assuring that every get or set operation on a shared property is atomic.</summary>
		SetGet = 0
	}
	/// <summary>Specifies the release mode for the properties in the new shared property group.</summary>
	[Serializable]
	[ComVisible(false)]
	public enum PropertyReleaseMode
	{
		/// <summary>The property group is not destroyed until the process in which it was created has terminated.</summary>
		Process = 1,
		/// <summary>When all clients have released their references on the property group, the property group is automatically destroyed. This is the default COM mode.</summary>
		Standard = 0
	}
	/// <summary>Provides configuration information for installing assemblies into the COM+ catalog.</summary>
	[Serializable]
	[Guid("36dcda30-dc3b-4d93-be42-90b2d74c64e7")]
	public class RegistrationConfig
	{
		/// <summary>Gets or sets the name of the COM+ application in which the assembly is to be installed.</summary>
		/// <returns>The name of the COM+ application in which the assembly is to be installed.</returns>
		[System.MonoTODO]
		public string Application
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the name of the root directory of the application.</summary>
		/// <returns>The name of the root directory of the application.</returns>
		[System.MonoTODO]
		public string ApplicationRootDirectory
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the file name of the assembly to install.</summary>
		/// <returns>The file name of the assembly to install.</returns>
		[System.MonoTODO]
		public string AssemblyFile
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a flag that indicates how to install the assembly.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.InstallationFlags" /> values.</returns>
		[System.MonoTODO]
		public InstallationFlags InstallationFlags
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the name of the COM+ partition.</summary>
		/// <returns>The name of the COM+ partition.</returns>
		[System.MonoTODO]
		public string Partition
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the name of the output Type Library Exporter (Tlbexp.exe) file.</summary>
		/// <returns>The name of the output Type Library Exporter (Tlbexp.exe) file.</returns>
		[System.MonoTODO]
		public string TypeLibrary
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.RegistrationConfig" /> class.</summary>
		[System.MonoTODO]
		public RegistrationConfig()
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Retrieves extended error information about methods related to multiple COM+ objects. This also includes methods that install, import, and export COM+ applications and components. This class cannot be inherited.</summary>
	[Serializable]
	public sealed class RegistrationErrorInfo
	{
		private int errorCode;

		private string errorString;

		private string majorRef;

		private string minorRef;

		private string name;

		/// <summary>Gets the error code for the object or file.</summary>
		/// <returns>The error code for the object or file.</returns>
		public int ErrorCode => errorCode;

		/// <summary>Gets the description of the <see cref="P:System.EnterpriseServices.RegistrationErrorInfo.ErrorCode" />.</summary>
		/// <returns>The description of the <see cref="P:System.EnterpriseServices.RegistrationErrorInfo.ErrorCode" />.</returns>
		public string ErrorString => errorString;

		/// <summary>Gets the key value for the object that caused the error, if applicable.</summary>
		/// <returns>The key value for the object that caused the error, if applicable.</returns>
		public string MajorRef => majorRef;

		/// <summary>Gets a precise specification of the item that caused the error, such as a property name.</summary>
		/// <returns>A precise specification of the item, such as a property name, that caused the error. If multiple errors occurred, or this does not apply, <see cref="P:System.EnterpriseServices.RegistrationErrorInfo.MinorRef" /> returns the string "&lt;Invalid&gt;".</returns>
		public string MinorRef => minorRef;

		/// <summary>Gets the name of the object or file that caused the error.</summary>
		/// <returns>The name of the object or file that caused the error.</returns>
		public string Name => name;

		[System.MonoTODO]
		internal RegistrationErrorInfo(string name, string majorRef, string minorRef, int errorCode)
		{
			this.name = name;
			this.majorRef = majorRef;
			this.minorRef = minorRef;
			this.errorCode = errorCode;
		}

		internal RegistrationErrorInfo()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>The exception that is thrown when a registration error is detected.</summary>
	[Serializable]
	public sealed class RegistrationException : SystemException
	{
		private RegistrationErrorInfo[] errorInfo;

		/// <summary>Gets an array of <see cref="T:System.EnterpriseServices.RegistrationErrorInfo" /> objects that describe registration errors.</summary>
		/// <returns>The array of <see cref="T:System.EnterpriseServices.RegistrationErrorInfo" /> objects.</returns>
		public RegistrationErrorInfo[] ErrorInfo => errorInfo;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.RegistrationException" /> class with a specified error message.</summary>
		/// <param name="msg">The message displayed to the client when the exception is thrown.</param>
		[System.MonoTODO]
		public RegistrationException(string msg)
			: base(msg)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.RegistrationException" /> class.</summary>
		public RegistrationException()
			: this("Registration error")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.RegistrationException" /> class with a specified error message and nested exception.</summary>
		/// <param name="msg">The message displayed to the client when the exception is thrown.</param>
		/// <param name="inner">The nested exception.</param>
		public RegistrationException(string msg, Exception inner)
			: base(msg, inner)
		{
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the error information in <see cref="T:System.EnterpriseServices.RegistrationErrorInfo" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains serialized object data.</param>
		/// <param name="ctx">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="info" /> parameter is <see langword="null" />.</exception>
		[System.MonoTODO]
		public override void GetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Installs and configures assemblies in the COM+ catalog. This class cannot be inherited.</summary>
	[Guid("89a86e7b-c229-4008-9baa-2f5c8411d7e0")]
	public sealed class RegistrationHelper : MarshalByRefObject, IRegistrationHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.RegistrationHelper" /> class.</summary>
		public RegistrationHelper()
		{
		}

		/// <summary>Installs the named assembly in a COM+ application.</summary>
		/// <param name="assembly">The file name of the assembly to install.</param>
		/// <param name="application">The name of the COM+ application to install into. This parameter can be <see langword="null" />. If the parameter is <see langword="null" /> and the assembly contains a <see cref="T:System.EnterpriseServices.ApplicationNameAttribute" />, then the attribute is used. Otherwise, the name of the application is generated based on the name of the assembly, then is returned.</param>
		/// <param name="tlb">The name of the output Type Library Exporter (Tlbexp.exe) file, or a string that contains <see langword="null" /> if the registration helper is expected to generate the name. The actual name used is placed in the parameter on call completion.</param>
		/// <param name="installFlags">A bitwise combination of the <see cref="T:System.EnterpriseServices.InstallationFlags" /> values.</param>
		/// <exception cref="T:System.EnterpriseServices.RegistrationException">The input assembly does not have a strong name.</exception>
		public void InstallAssembly(string assembly, ref string application, ref string tlb, InstallationFlags installFlags)
		{
			application = string.Empty;
			tlb = string.Empty;
			InstallAssembly(assembly, ref application, null, ref tlb, installFlags);
		}

		/// <summary>Installs the named assembly in a COM+ application.</summary>
		/// <param name="assembly">The file name of the assembly to install.</param>
		/// <param name="application">The name of the COM+ application to install into. This parameter can be <see langword="null" />. If the parameter is <see langword="null" /> and the assembly contains a <see cref="T:System.EnterpriseServices.ApplicationNameAttribute" />, then the attribute is used. Otherwise, the name of the application is generated based on the name of the assembly, then is returned.</param>
		/// <param name="partition">The name of the partition. This parameter can be <see langword="null" />.</param>
		/// <param name="tlb">The name of the output Type Library Exporter (Tlbexp.exe) file, or a string that contains <see langword="null" /> if the registration helper is expected to generate the name. The actual name used is placed in the parameter on call completion.</param>
		/// <param name="installFlags">A bitwise combination of the <see cref="T:System.EnterpriseServices.InstallationFlags" /> values.</param>
		/// <exception cref="T:System.EnterpriseServices.RegistrationException">The input assembly does not have a strong name.</exception>
		[System.MonoTODO]
		public void InstallAssembly(string assembly, ref string application, string partition, ref string tlb, InstallationFlags installFlags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Installs the named assembly in a COM+ application.</summary>
		/// <param name="regConfig">A <see cref="T:System.EnterpriseServices.RegistrationConfig" /> identifying the assembly to install.</param>
		[System.MonoTODO]
		public void InstallAssemblyFromConfig([MarshalAs(UnmanagedType.IUnknown)] ref RegistrationConfig regConfig)
		{
			throw new NotImplementedException();
		}

		/// <summary>Uninstalls the assembly from the given application.</summary>
		/// <param name="assembly">The file name of the assembly to uninstall.</param>
		/// <param name="application">If this name is not <see langword="null" />, it is the name of the application that contains the components in the assembly.</param>
		/// <exception cref="T:System.EnterpriseServices.RegistrationException">The input assembly does not have a strong name.</exception>
		public void UninstallAssembly(string assembly, string application)
		{
			UninstallAssembly(assembly, application, null);
		}

		/// <summary>Uninstalls the assembly from the given application.</summary>
		/// <param name="assembly">The file name of the assembly to uninstall.</param>
		/// <param name="application">If this name is not <see langword="null" />, it is the name of the application that contains the components in the assembly.</param>
		/// <param name="partition">The name of the partition. This parameter can be <see langword="null" />.</param>
		/// <exception cref="T:System.EnterpriseServices.RegistrationException">The input assembly does not have a strong name.</exception>
		[System.MonoTODO]
		public void UninstallAssembly(string assembly, string application, string partition)
		{
			throw new NotImplementedException();
		}

		/// <summary>Uninstalls the assembly from the given application.</summary>
		/// <param name="regConfig">A <see cref="T:System.EnterpriseServices.RegistrationConfig" /> identifying the assembly to uninstall.</param>
		[System.MonoTODO]
		public void UninstallAssemblyFromConfig([MarshalAs(UnmanagedType.IUnknown)] ref RegistrationConfig regConfig)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Used by the .NET Framework infrastructure to install and configure assemblies in the COM+ catalog while maintaining a newly established transaction.</summary>
	[Transaction(TransactionOption.RequiresNew)]
	[Guid("C89AC250-E18A-4FC7-ABD5-B8897B6A78A5")]
	public sealed class RegistrationHelperTx : ServicedComponent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.RegistrationHelperTx" /> class.</summary>
		[System.MonoTODO]
		public RegistrationHelperTx()
		{
		}

		[System.MonoTODO]
		protected internal override void Activate()
		{
			throw new NotImplementedException();
		}

		[System.MonoTODO]
		protected internal override void Deactivate()
		{
			throw new NotImplementedException();
		}

		/// <summary>Installs the named assembly in the COM+ catalog using transactional semantics.</summary>
		/// <param name="assembly">The file name of the assembly to install.</param>
		/// <param name="application">Either the name of the COM+ application to install into or <see langword="null" />.</param>
		/// <param name="tlb">Either the name of the output Type Library Exporter (Tlbexp.exe) file or <see langword="null" />.</param>
		/// <param name="installFlags">A bitwise combination of the installation flags values.</param>
		/// <param name="sync">A synchronization object generated by the infrastructure that can wait until the specified assembly has been configured in the COM+ catalog.</param>
		public void InstallAssembly(string assembly, ref string application, ref string tlb, InstallationFlags installFlags, object sync)
		{
			InstallAssembly(assembly, ref application, null, ref tlb, installFlags, sync);
		}

		/// <summary>Installs the named assembly in the COM+ catalog using transactional semantics.</summary>
		/// <param name="assembly">The file name of the assembly to install.</param>
		/// <param name="application">Either the name of the COM+ application to install into or a string that contains <see langword="null" />.</param>
		/// <param name="partition">Either the name of the partition or <see langword="null" />.</param>
		/// <param name="tlb">Either the name of the output Type Library Exporter (Tlbexp.exe) file or <see langword="null" />.</param>
		/// <param name="installFlags">A bitwise combination of the installation flags values.</param>
		/// <param name="sync">A synchronization object generated by the infrastructure that can wait until the specified assembly has been configured in the COM+ catalog.</param>
		[System.MonoTODO]
		public void InstallAssembly(string assembly, ref string application, string partition, ref string tlb, InstallationFlags installFlags, object sync)
		{
			throw new NotImplementedException();
		}

		/// <summary>Installs a specified assembly in the COM+ catalog using transactional semantics.</summary>
		/// <param name="regConfig">Configuration information for installing an assembly into the COM+ catalog.</param>
		/// <param name="sync">A synchronization object generated by the infrastructure that waits until the specified assembly has been configured in the COM+ catalog.</param>
		[System.MonoTODO]
		public void InstallAssemblyFromConfig([MarshalAs(UnmanagedType.IUnknown)] ref RegistrationConfig regConfig, object sync)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the current context for the <see cref="T:System.EnterpriseServices.RegistrationHelperTx" /> class instance is transactional.</summary>
		/// <returns>
		///   <see langword="true" /> if the current context for the <see cref="T:System.EnterpriseServices.RegistrationHelperTx" /> class instance is transactional; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool IsInTransaction()
		{
			throw new NotImplementedException();
		}

		/// <summary>Uninstalls an assembly from a COM+ application using transactional semantics.</summary>
		/// <param name="assembly">The file name of the assembly to uninstall.</param>
		/// <param name="application">Either the name of the COM+ application that contains the components in the assembly or <see langword="null" />.</param>
		/// <param name="sync">A synchronization object generated by the infrastructure that can wait until the specified assembly has been uninstalled.</param>
		public void UninstallAssembly(string assembly, string application, object sync)
		{
			UninstallAssembly(assembly, application, null, sync);
		}

		/// <summary>Uninstalls an assembly from a COM+ application using transactional semantics.</summary>
		/// <param name="assembly">The file name of the assembly to uninstall.</param>
		/// <param name="application">Either the name of the COM+ application that contains the components in the assembly or <see langword="null" />.</param>
		/// <param name="partition">Either the name of the partition or <see langword="null" />.</param>
		/// <param name="sync">A synchronization object generated by the infrastructure that can wait until the specified assembly has been uninstalled.</param>
		[System.MonoTODO]
		public void UninstallAssembly(string assembly, string application, string partition, object sync)
		{
			throw new NotImplementedException();
		}

		/// <summary>Uninstalls a specified assembly from a COM+ application using transactional semantics.</summary>
		/// <param name="regConfig">Configuration information that specifies an assembly to uninstall from an application.</param>
		/// <param name="sync">A synchronization object generated by the infrastructure that waits until the specified assembly has been uninstalled.</param>
		[System.MonoTODO]
		public void UninstallAssemblyFromConfig([MarshalAs(UnmanagedType.IUnknown)] ref RegistrationConfig regConfig, object sync)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Stores objects in the current transaction. This class cannot be inherited.</summary>
	public sealed class ResourcePool
	{
		/// <summary>Represents the method that handles the ending of a transaction.</summary>
		/// <param name="resource">The object that is passed back to the delegate.</param>
		public delegate void TransactionEndDelegate(object resource);

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ResourcePool" /> class.</summary>
		/// <param name="cb">A <see cref="T:System.EnterpriseServices.ResourcePool.TransactionEndDelegate" />, that is called when a transaction is finished. All items currently stored in the transaction are handed back to the user through the delegate.</param>
		[System.MonoTODO]
		public ResourcePool(TransactionEndDelegate cb)
		{
		}

		/// <summary>Gets a resource from the current transaction.</summary>
		/// <returns>The resource object.</returns>
		[System.MonoTODO]
		public object GetResource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds a resource to the current transaction.</summary>
		/// <param name="resource">The resource to add.</param>
		/// <returns>
		///   <see langword="true" /> if the resource object was added to the pool; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool PutResource(object resource)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Ensures that the infrastructure calls through an interface for a method or for each method in a class when using the security service. Classes need to use interfaces to use security services. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	[ComVisible(false)]
	public sealed class SecureMethodAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SecureMethodAttribute" /> class.</summary>
		public SecureMethodAttribute()
		{
		}
	}
	/// <summary>Describes the chain of callers leading up to the current method call.</summary>
	public sealed class SecurityCallContext
	{
		/// <summary>Gets a <see cref="T:System.EnterpriseServices.SecurityCallers" /> object that describes the caller.</summary>
		/// <returns>The <see cref="T:System.EnterpriseServices.SecurityCallers" /> object that describes the caller.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no security context.</exception>
		public SecurityCallers Callers
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.EnterpriseServices.SecurityCallContext" /> object that describes the security call context.</summary>
		/// <returns>The <see cref="T:System.EnterpriseServices.SecurityCallContext" /> object that describes the security call context.</returns>
		public static SecurityCallContext CurrentCall
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.EnterpriseServices.SecurityIdentity" /> object that describes the direct caller of this method.</summary>
		/// <returns>A <see cref="T:System.EnterpriseServices.SecurityIdentity" /> value.</returns>
		public SecurityIdentity DirectCaller
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines whether security checks are enabled in the current context.</summary>
		/// <returns>
		///   <see langword="true" /> if security checks are enabled in the current context; otherwise, <see langword="false" />.</returns>
		public bool IsSecurityEnabled
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see langword="MinAuthenticationLevel" /> value from the <see langword="ISecurityCallContext" /> collection in COM+.</summary>
		/// <returns>The <see langword="MinAuthenticationLevel" /> value from the <see langword="ISecurityCallContext" /> collection in COM+.</returns>
		public int MinAuthenticationLevel
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see langword="NumCallers" /> value from the <see langword="ISecurityCallContext" /> collection in COM+.</summary>
		/// <returns>The <see langword="NumCallers" /> value from the <see langword="ISecurityCallContext" /> collection in COM+.</returns>
		public int NumCallers
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.EnterpriseServices.SecurityIdentity" /> that describes the original caller.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.SecurityIdentity" /> values.</returns>
		public SecurityIdentity OriginalCaller
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		internal SecurityCallContext()
		{
		}

		internal SecurityCallContext(ISecurityCallContext context)
		{
		}

		/// <summary>Verifies that the direct caller is a member of the specified role.</summary>
		/// <param name="role">The specified role.</param>
		/// <returns>
		///   <see langword="true" /> if the direct caller is a member of the specified role; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool IsCallerInRole(string role)
		{
			throw new NotImplementedException();
		}

		/// <summary>Verifies that the specified user is in the specified role.</summary>
		/// <param name="user">The specified user.</param>
		/// <param name="role">The specified role.</param>
		/// <returns>
		///   <see langword="true" /> if the specified user is a member of the specified role; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool IsUserInRole(string user, string role)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Provides an ordered collection of identities in the current call chain.</summary>
	public sealed class SecurityCallers : IEnumerable
	{
		/// <summary>Gets the number of callers in the chain.</summary>
		/// <returns>The number of callers in the chain.</returns>
		public int Count
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the specified <see cref="T:System.EnterpriseServices.SecurityIdentity" /> item.</summary>
		/// <param name="idx">The item to access using an index number.</param>
		/// <returns>A <see cref="T:System.EnterpriseServices.SecurityIdentity" /> object.</returns>
		public SecurityIdentity this[int idx]
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		internal SecurityCallers()
		{
		}

		internal SecurityCallers(ISecurityCallersColl collection)
		{
		}

		/// <summary>Retrieves the enumeration interface for the object.</summary>
		/// <returns>The enumerator interface for the <see langword="ISecurityCallersColl" /> collection.</returns>
		[System.MonoTODO]
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Contains information that regards an identity in a COM+ call chain.</summary>
	public sealed class SecurityIdentity
	{
		/// <summary>Gets the name of the user described by this identity.</summary>
		/// <returns>The name of the user described by this identity.</returns>
		public string AccountName
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the authentication level of the user described by this identity.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.AuthenticationOption" /> values.</returns>
		public AuthenticationOption AuthenticationLevel
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the authentication service described by this identity.</summary>
		/// <returns>The authentication service described by this identity.</returns>
		public int AuthenticationService
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the impersonation level of the user described by this identity.</summary>
		/// <returns>A <see cref="T:System.EnterpriseServices.ImpersonationLevelOption" /> value.</returns>
		public ImpersonationLevelOption ImpersonationLevel
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		[System.MonoTODO]
		internal SecurityIdentity()
		{
		}

		[System.MonoTODO]
		internal SecurityIdentity(ISecurityIdentityColl collection)
		{
		}
	}
	/// <summary>Configures a role for an application or component. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	[ComVisible(false)]
	public sealed class SecurityRoleAttribute : Attribute
	{
		private string description;

		private bool everyone;

		private string role;

		/// <summary>Gets or sets the role description.</summary>
		/// <returns>The description for the role.</returns>
		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
			}
		}

		/// <summary>Gets or sets the security role.</summary>
		/// <returns>The security role applied to an application, component, interface, or method.</returns>
		public string Role
		{
			get
			{
				return role;
			}
			set
			{
				role = value;
			}
		}

		/// <summary>Sets a value indicating whether to add the Everyone user group as a user.</summary>
		/// <returns>
		///   <see langword="true" /> to require that a newly created role have the Everyone user group added as a user (roles that already exist on the application are not modified); otherwise, <see langword="false" /> to suppress adding the Everyone user group as a user.</returns>
		public bool SetEveryoneAccess
		{
			get
			{
				return everyone;
			}
			set
			{
				everyone = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SecurityRoleAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.SecurityRoleAttribute.Role" /> property.</summary>
		/// <param name="role">A security role for the application, component, interface, or method.</param>
		public SecurityRoleAttribute(string role)
			: this(role, everyone: false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SecurityRoleAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.SecurityRoleAttribute.Role" /> and <see cref="P:System.EnterpriseServices.SecurityRoleAttribute.SetEveryoneAccess" /> properties.</summary>
		/// <param name="role">A security role for the application, component, interface, or method.</param>
		/// <param name="everyone">
		///   <see langword="true" /> to require that the newly created role have the Everyone user group added as a user; otherwise, <see langword="false" />.</param>
		public SecurityRoleAttribute(string role, bool everyone)
		{
			description = string.Empty;
			this.everyone = everyone;
			this.role = role;
		}
	}
	/// <summary>Specifies and configures the services that are to be active in the domain which is entered when calling <see cref="M:System.EnterpriseServices.ServiceDomain.Enter(System.EnterpriseServices.ServiceConfig)" /> or creating an <see cref="T:System.EnterpriseServices.Activity" />. This class cannot be inherited.</summary>
	[System.MonoTODO]
	[ComVisible(false)]
	public sealed class ServiceConfig
	{
		/// <summary>Gets or sets the binding option, which indicates whether all work submitted by the activity is to be bound to only one single-threaded apartment (STA).</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.BindingOption" /> values. The default is <see cref="F:System.EnterpriseServices.BindingOption.NoBinding" />.</returns>
		[System.MonoTODO]
		public BindingOption Binding
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Transactions.Transaction" /> that represents an existing transaction that supplies the settings used to run the transaction identified by <see cref="T:System.EnterpriseServices.ServiceConfig" />.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" />. The default is <see langword="null" />.</returns>
		[System.MonoTODO]
		public Transaction BringYourOwnSystemTransaction
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.EnterpriseServices.ITransaction" /> that represents an existing transaction that supplies the settings used to run the transaction identified by <see cref="T:System.EnterpriseServices.ServiceConfig" />.</summary>
		/// <returns>An <see cref="T:System.EnterpriseServices.ITransaction" />. The default is <see langword="null" />.</returns>
		[System.MonoTODO]
		public ITransaction BringYourOwnTransaction
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether COM Transaction Integrator (COMTI) intrinsics are enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if COMTI intrinsics are enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool COMTIIntrinsicsEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether Internet Information Services (IIS) intrinsics are enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if IIS intrinsics are enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool IISIntrinsicsEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether to construct a new context based on the current context or to create a new context based solely on the information in <see cref="T:System.EnterpriseServices.ServiceConfig" />.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.InheritanceOption" /> values. The default is <see cref="F:System.EnterpriseServices.InheritanceOption.Inherit" />.</returns>
		[System.MonoTODO]
		public InheritanceOption Inheritance
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the isolation level of the transaction.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.TransactionIsolationLevel" /> values. The default is <see cref="F:System.EnterpriseServices.TransactionIsolationLevel.Any" />.</returns>
		[System.MonoTODO]
		public TransactionIsolationLevel IsolationLevel
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the GUID for the COM+ partition that is to be used.</summary>
		/// <returns>The GUID for the partition to be used. The default is a zero GUID.</returns>
		[System.MonoTODO]
		public Guid PartitionId
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates how partitions are used for the enclosed work.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.PartitionOption" /> values. The default is <see cref="F:System.EnterpriseServices.PartitionOption.Ignore" />.</returns>
		[System.MonoTODO]
		public PartitionOption PartitionOption
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the directory for the side-by-side assembly for the enclosed work.</summary>
		/// <returns>The name of the directory to be used for the side-by-side assembly. The default value is <see langword="null" />.</returns>
		[System.MonoTODO]
		public string SxsDirectory
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the file name of the side-by-side assembly for the enclosed work.</summary>
		/// <returns>The file name of the side-by-side assembly. The default value is <see langword="null" />.</returns>
		[System.MonoTODO]
		public string SxsName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates how to configure the side-by-side assembly.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.SxsOption" /> values. The default is <see cref="F:System.EnterpriseServices.SxsOption.Ignore" />.</returns>
		[System.MonoTODO]
		public SxsOption SxsOption
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value in that indicates the type of automatic synchronization requested by the component.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.SynchronizationOption" /> values. The default is <see cref="F:System.EnterpriseServices.SynchronizationOption.Disabled" />.</returns>
		[System.MonoTODO]
		public SynchronizationOption Synchronization
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates the thread pool which runs the work submitted by the activity.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.ThreadPoolOption" /> values. The default is <see cref="F:System.EnterpriseServices.ThreadPoolOption.None" />.</returns>
		[System.MonoTODO]
		public ThreadPoolOption ThreadPool
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the Transaction Internet Protocol (TIP) URL that allows the enclosed code to run in an existing transaction.</summary>
		/// <returns>A TIP URL. The default value is <see langword="null" />.</returns>
		[System.MonoTODO]
		public string TipUrl
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a text string that corresponds to the application ID under which tracker information is reported.</summary>
		/// <returns>The application ID under which tracker information is reported. The default value is <see langword="null" />.</returns>
		[System.MonoTODO]
		public string TrackingAppName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a text string that corresponds to the context name under which tracker information is reported.</summary>
		/// <returns>The context name under which tracker information is reported. The default value is <see langword="null" />.</returns>
		[System.MonoTODO]
		public string TrackingComponentName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether tracking is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if tracking is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool TrackingEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates how transactions are used in the enclosed work.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.TransactionOption" /> values. The default is <see cref="F:System.EnterpriseServices.TransactionOption.Disabled" />.</returns>
		[System.MonoTODO]
		public TransactionOption Transaction
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the name that is used when transaction statistics are displayed.</summary>
		/// <returns>The name used when transaction statistics are displayed. The default value is <see langword="null" />.</returns>
		[System.MonoTODO]
		public string TransactionDescription
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the transaction time-out for a new transaction.</summary>
		/// <returns>The transaction time-out, in seconds.</returns>
		[System.MonoTODO]
		public int TransactionTimeout
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ServiceConfig" /> class, setting the properties to configure the desired services.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///   <see cref="T:System.EnterpriseServices.ServiceConfig" /> is not supported on the current platform.</exception>
		[System.MonoTODO]
		public ServiceConfig()
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Allows a code segment identified by <see cref="M:System.EnterpriseServices.ServiceDomain.Enter(System.EnterpriseServices.ServiceConfig)" /> and <see cref="M:System.EnterpriseServices.ServiceDomain.Leave" /> to run in its own context and behave as if it were a method that is called on an object created within the context. This class cannot be inherited.</summary>
	[ComVisible(false)]
	public sealed class ServiceDomain
	{
		private ServiceDomain()
		{
		}

		/// <summary>Creates the context specified by the <see cref="T:System.EnterpriseServices.ServiceConfig" /> object and pushes it onto the context stack to become the current context.</summary>
		/// <param name="cfg">A <see cref="T:System.EnterpriseServices.ServiceConfig" /> that contains the configuration information for the services to be used within the enclosed code.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///   <see cref="T:System.EnterpriseServices.ServiceConfig" /> is not supported on the current platform.</exception>
		[System.MonoTODO]
		public static void Enter(ServiceConfig cfg)
		{
			throw new NotImplementedException();
		}

		/// <summary>Triggers the server and then the client side policies as if a method call were returning. The current context is then popped from the context stack, and the context that was running when <see cref="M:System.EnterpriseServices.ServiceDomain.Enter(System.EnterpriseServices.ServiceConfig)" /> was called becomes the current context.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.TransactionStatus" /> values.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///   <see cref="T:System.EnterpriseServices.ServiceConfig" /> is not supported on the current platform.</exception>
		[System.MonoTODO]
		public static TransactionStatus Leave()
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Represents the base class of all classes using COM+ services.</summary>
	[Serializable]
	public abstract class ServicedComponent : ContextBoundObject, IDisposable, IRemoteDispatch, IServicedComponentInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class.</summary>
		public ServicedComponent()
		{
		}

		/// <summary>Called by the infrastructure when the object is created or allocated from a pool. Override this method to add custom initialization code to objects.</summary>
		[System.MonoTODO]
		protected internal virtual void Activate()
		{
			throw new NotImplementedException();
		}

		/// <summary>This method is called by the infrastructure before the object is put back into the pool. Override this method to vote on whether the object is put back into the pool.</summary>
		/// <returns>
		///   <see langword="true" /> if the serviced component can be pooled; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		protected internal virtual bool CanBePooled()
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by the infrastructure just after the constructor is called, passing in the constructor string. Override this method to make use of the construction string value.</summary>
		/// <param name="s">The construction string.</param>
		[System.MonoTODO]
		protected internal virtual void Construct(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by the infrastructure when the object is about to be deactivated. Override this method to add custom finalization code to objects when just-in-time (JIT) compiled code or object pooling is used.</summary>
		[System.MonoTODO]
		protected internal virtual void Deactivate()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.EnterpriseServices.ServicedComponent" />.</summary>
		[System.MonoTODO]
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.EnterpriseServices.ServicedComponent" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; otherwise, <see langword="false" /> to release only unmanaged resources.</param>
		[System.MonoTODO]
		protected virtual void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Finalizes the object and removes the associated COM+ reference.</summary>
		/// <param name="sc">The object to dispose.</param>
		[System.MonoTODO]
		public static void DisposeObject(ServicedComponent sc)
		{
			throw new NotImplementedException();
		}

		/// <summary>Ensures that, in the COM+ context, the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class object's <see langword="done" /> bit is set to <see langword="true" /> after a remote method invocation.</summary>
		/// <param name="s">A string to be converted into a request object that implements the <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface.</param>
		/// <returns>A string converted from a response object that implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface.</returns>
		[System.MonoTODO]
		string IRemoteDispatch.RemoteDispatchAutoDone(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>Does not ensure that, in the COM+ context, the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class object's <see langword="done" /> bit is set to <see langword="true" /> after a remote method invocation.</summary>
		/// <param name="s">A string to be converted into a request object that implements the <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface.</param>
		/// <returns>A string converted from a response object that implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface.</returns>
		[System.MonoTODO]
		string IRemoteDispatch.RemoteDispatchNotAutoDone(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>Obtains certain information about the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class instance.</summary>
		/// <param name="infoMask">A bitmask where 0x00000001 is a key for the serviced component's process ID, 0x00000002 is a key for the application domain ID, and 0x00000004 is a key for the serviced component's remote URI.</param>
		/// <param name="infoArray">A string array that may contain any or all of the following, in order: the serviced component's process ID, the application domain ID, and the serviced component's remote URI.</param>
		[System.MonoTODO]
		void IServicedComponentInfo.GetComponentInfo(ref int infoMask, out string[] infoArray)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>The exception that is thrown when an error is detected in a serviced component.</summary>
	[Serializable]
	[ComVisible(false)]
	public sealed class ServicedComponentException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ServicedComponentException" /> class.</summary>
		public ServicedComponentException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ServicedComponentException" /> class with a specified error message.</summary>
		/// <param name="message">The message displayed to the client when the exception is thrown.</param>
		public ServicedComponentException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ServicedComponentException" /> class.</summary>
		/// <param name="message">The message displayed to the client when the exception is thrown.</param>
		/// <param name="innerException">The <see cref="P:System.Exception.InnerException" />, if any, that threw the current exception.</param>
		public ServicedComponentException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
	/// <summary>Accesses a shared property. This class cannot be inherited.</summary>
	[ComVisible(false)]
	public sealed class SharedProperty
	{
		private ISharedProperty property;

		/// <summary>Gets or sets the value of the shared property.</summary>
		/// <returns>The value of the shared property.</returns>
		public object Value
		{
			get
			{
				return property.Value;
			}
			set
			{
				property.Value = value;
			}
		}

		internal SharedProperty(ISharedProperty property)
		{
			this.property = property;
		}

		internal SharedProperty()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Represents a collection of shared properties. This class cannot be inherited.</summary>
	[ComVisible(false)]
	public sealed class SharedPropertyGroup
	{
		private ISharedPropertyGroup propertyGroup;

		internal SharedPropertyGroup(ISharedPropertyGroup propertyGroup)
		{
			this.propertyGroup = propertyGroup;
		}

		/// <summary>Creates a property with the given name.</summary>
		/// <param name="name">The name of the new property.</param>
		/// <param name="fExists">Determines whether the property exists. Set to <see langword="true" /> on return if the property exists.</param>
		/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedProperty" />.</returns>
		public SharedProperty CreateProperty(string name, out bool fExists)
		{
			return new SharedProperty(propertyGroup.CreateProperty(name, out fExists));
		}

		/// <summary>Creates a property at the given position.</summary>
		/// <param name="position">The index of the new property</param>
		/// <param name="fExists">Determines whether the property exists. Set to <see langword="true" /> on return if the property exists.</param>
		/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedProperty" />.</returns>
		public SharedProperty CreatePropertyByPosition(int position, out bool fExists)
		{
			return new SharedProperty(propertyGroup.CreatePropertyByPosition(position, out fExists));
		}

		/// <summary>Returns the property with the given name.</summary>
		/// <param name="name">The name of requested property.</param>
		/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedProperty" />.</returns>
		public SharedProperty Property(string name)
		{
			return new SharedProperty(propertyGroup.Property(name));
		}

		/// <summary>Returns the property at the given position.</summary>
		/// <param name="position">The index of the property.</param>
		/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedProperty" />.</returns>
		public SharedProperty PropertyByPosition(int position)
		{
			return new SharedProperty(propertyGroup.PropertyByPosition(position));
		}

		internal SharedPropertyGroup()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Controls access to shared property groups. This class cannot be inherited.</summary>
	[ComVisible(false)]
	public sealed class SharedPropertyGroupManager : IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SharedPropertyGroupManager" /> class.</summary>
		public SharedPropertyGroupManager()
		{
		}

		/// <summary>Finds or creates a property group with the given information.</summary>
		/// <param name="name">The name of requested property.</param>
		/// <param name="dwIsoMode">One of the <see cref="T:System.EnterpriseServices.PropertyLockMode" /> values. See the Remarks section for more information.</param>
		/// <param name="dwRelMode">One of the <see cref="T:System.EnterpriseServices.PropertyReleaseMode" /> values. See the Remarks section for more information.</param>
		/// <param name="fExist">When this method returns, contains <see langword="true" /> if the property already existed; <see langword="false" /> if the call created the property.</param>
		/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedPropertyGroup" />.</returns>
		[System.MonoTODO]
		public SharedPropertyGroup CreatePropertyGroup(string name, ref PropertyLockMode dwIsoMode, ref PropertyReleaseMode dwRelMode, out bool fExist)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the enumeration interface for the collection.</summary>
		/// <returns>The enumerator interface for the collection.</returns>
		[System.MonoTODO]
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds the property group with the given name.</summary>
		/// <param name="name">The name of requested property.</param>
		/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedPropertyGroup" />.</returns>
		[System.MonoTODO]
		public SharedPropertyGroup Group(string name)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Indicates how side-by-side assemblies are configured for <see cref="T:System.EnterpriseServices.ServiceConfig" />.</summary>
	[Serializable]
	[ComVisible(false)]
	public enum SxsOption
	{
		/// <summary>Side-by-side assemblies are not used within the enclosed context. <see cref="F:System.EnterpriseServices.SxsOption.Ignore" /> is the default setting for <see cref="P:System.EnterpriseServices.ServiceConfig.SxsOption" /> when <see cref="P:System.EnterpriseServices.ServiceConfig.Inheritance" /> is set to <see cref="F:System.EnterpriseServices.InheritanceOption.Ignore" />.</summary>
		Ignore,
		/// <summary>The current side-by-side assembly of the enclosed context is used. <see cref="F:System.EnterpriseServices.SxsOption.Inherit" /> is the default setting for <see cref="P:System.EnterpriseServices.ServiceConfig.SxsOption" /> when <see cref="P:System.EnterpriseServices.ServiceConfig.Inheritance" /> is set to <see cref="F:System.EnterpriseServices.InheritanceOption.Inherit" />.</summary>
		Inherit,
		/// <summary>A new side-by-side assembly is created for the enclosed context.</summary>
		New
	}
	/// <summary>Sets the synchronization value of the component. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class SynchronizationAttribute : Attribute
	{
		private SynchronizationOption val;

		/// <summary>Gets the current setting of the <see cref="P:System.EnterpriseServices.SynchronizationAttribute.Value" /> property.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.SynchronizationOption" /> values. The default is <see langword="Required" />.</returns>
		public SynchronizationOption Value => val;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SynchronizationAttribute" /> class with the default <see cref="T:System.EnterpriseServices.SynchronizationOption" />.</summary>
		public SynchronizationAttribute()
			: this(SynchronizationOption.Required)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SynchronizationAttribute" /> class with the specified <see cref="T:System.EnterpriseServices.SynchronizationOption" />.</summary>
		/// <param name="val">One of the <see cref="T:System.EnterpriseServices.SynchronizationOption" /> values.</param>
		public SynchronizationAttribute(SynchronizationOption val)
		{
			this.val = val;
		}
	}
	/// <summary>Specifies the type of automatic synchronization requested by the component.</summary>
	[Serializable]
	public enum SynchronizationOption
	{
		/// <summary>COM+ ignores the synchronization requirements of the component when determining context for the object.</summary>
		Disabled = 0,
		/// <summary>An object with this value never participates in synchronization, regardless of the status of its caller. This setting is only available for components that are non-transactional and do not use just-in-time (JIT) activation.</summary>
		NotSupported = 1,
		/// <summary>Ensures that all objects created from the component are synchronized.</summary>
		Required = 3,
		/// <summary>An object with this value must participate in a new synchronization where COM+ manages contexts and apartments on behalf of all components involved in the call.</summary>
		RequiresNew = 4,
		/// <summary>An object with this value participates in synchronization, if it exists.</summary>
		Supported = 2
	}
	/// <summary>Indicates the thread pool in which the work, submitted by <see cref="T:System.EnterpriseServices.Activity" />, runs.</summary>
	[Serializable]
	[ComVisible(false)]
	public enum ThreadPoolOption
	{
		/// <summary>No thread pool is used. If this value is used to configure a <see cref="T:System.EnterpriseServices.ServiceConfig" /> that is passed to an <see cref="T:System.EnterpriseServices.Activity" />, an exception is thrown.</summary>
		None,
		/// <summary>The same type of thread pool apartment as the caller's thread apartment is used.</summary>
		Inherit,
		/// <summary>A single-threaded apartment (STA) is used.</summary>
		STA,
		/// <summary>A multithreaded apartment (MTA) is used.</summary>
		MTA
	}
	/// <summary>Specifies the type of transaction that is available to the attributed object. Permissible values are members of the <see cref="T:System.EnterpriseServices.TransactionOption" /> enumeration.</summary>
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class TransactionAttribute : Attribute
	{
		private TransactionIsolationLevel isolation;

		private int timeout;

		private TransactionOption val;

		/// <summary>Gets or sets the transaction isolation level.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.TransactionIsolationLevel" /> values.</returns>
		public TransactionIsolationLevel Isolation
		{
			get
			{
				return isolation;
			}
			set
			{
				isolation = value;
			}
		}

		/// <summary>Gets or sets the time-out for this transaction.</summary>
		/// <returns>The transaction time-out in seconds.</returns>
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

		/// <summary>Gets the <see cref="T:System.EnterpriseServices.TransactionOption" /> value for the transaction, optionally disabling the transaction service.</summary>
		/// <returns>The specified transaction type, a <see cref="T:System.EnterpriseServices.TransactionOption" /> value.</returns>
		public TransactionOption Value => val;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.TransactionAttribute" /> class, setting the component's requested transaction type to <see cref="F:System.EnterpriseServices.TransactionOption.Required" />.</summary>
		public TransactionAttribute()
			: this(TransactionOption.Required)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.TransactionAttribute" /> class, specifying the transaction type.</summary>
		/// <param name="val">The specified transaction type, a <see cref="T:System.EnterpriseServices.TransactionOption" /> value.</param>
		public TransactionAttribute(TransactionOption val)
		{
			isolation = TransactionIsolationLevel.Serializable;
			timeout = -1;
			this.val = val;
		}
	}
	/// <summary>Specifies the value of the <see cref="T:System.EnterpriseServices.TransactionAttribute" />.</summary>
	[Serializable]
	public enum TransactionIsolationLevel
	{
		/// <summary>The isolation level for the component is obtained from the calling component's isolation level. If this is the root component, the isolation level used is <see cref="F:System.EnterpriseServices.TransactionIsolationLevel.Serializable" />.</summary>
		Any = 0,
		/// <summary>Shared locks are held while the data is being read to avoid reading modified data, but the data can be changed before the end of the transaction, resulting in non-repeatable reads or phantom data.</summary>
		ReadCommitted = 2,
		/// <summary>Shared locks are issued and no exclusive locks are honored.</summary>
		ReadUncommitted = 1,
		/// <summary>Locks are placed on all data that is used in a query, preventing other users from updating the data. Prevents non-repeatable reads, but phantom rows are still possible.</summary>
		RepeatableRead = 3,
		/// <summary>Prevents updating or inserting until the transaction is complete.</summary>
		Serializable = 4
	}
	/// <summary>Specifies the automatic transaction type requested by the component.</summary>
	[Serializable]
	public enum TransactionOption
	{
		/// <summary>Ignores any transaction in the current context.</summary>
		Disabled,
		/// <summary>Creates the component in a context with no governing transaction.</summary>
		NotSupported,
		/// <summary>Shares a transaction, if one exists.</summary>
		Supported,
		/// <summary>Shares a transaction, if one exists, and creates a new transaction if necessary.</summary>
		Required,
		/// <summary>Creates the component with a new transaction, regardless of the state of the current context.</summary>
		RequiresNew
	}
	/// <summary>Indicates the transaction status.</summary>
	[Serializable]
	[ComVisible(false)]
	public enum TransactionStatus
	{
		/// <summary>The transaction has committed.</summary>
		Commited,
		/// <summary>The transaction has neither committed nor aborted.</summary>
		LocallyOk,
		/// <summary>No transactions are being used through <see cref="M:System.EnterpriseServices.ServiceDomain.Enter(System.EnterpriseServices.ServiceConfig)" />.</summary>
		NoTransaction,
		/// <summary>The transaction is in the process of aborting.</summary>
		Aborting,
		/// <summary>The transaction is aborted.</summary>
		Aborted
	}
	/// <summary>Specifies the values allowed for transaction outcome voting.</summary>
	[Serializable]
	[ComVisible(false)]
	public enum TransactionVote
	{
		/// <summary>Aborts the current transaction.</summary>
		Abort = 1,
		/// <summary>Commits the current transaction.</summary>
		Commit = 0
	}
	/// <summary>Represents a structure used in the <see cref="T:System.EnterpriseServices.ITransaction" /> interface.</summary>
	[ComVisible(false)]
	public struct XACTTRANSINFO
	{
		/// <summary>Specifies zero. This field is reserved.</summary>
		public int grfRMSupported;

		/// <summary>Specifies zero. This field is reserved.</summary>
		public int grfRMSupportedRetaining;

		/// <summary>Represents a bitmask that indicates which <see langword="grfTC" /> flags this transaction implementation supports.</summary>
		public int grfTCSupported;

		/// <summary>Specifies zero. This field is reserved.</summary>
		public int grfTCSupportedRetaining;

		/// <summary>Specifies zero. This field is reserved.</summary>
		public int isoFlags;

		/// <summary>Represents the isolation level associated with this transaction object. ISOLATIONLEVEL_UNSPECIFIED indicates that no isolation level was specified.</summary>
		public int isoLevel;

		/// <summary>Represents the unit of work associated with this transaction.</summary>
		public BOID uow;
	}
}
namespace System.EnterpriseServices.Internal
{
	/// <summary>Switches into the given application domain, which the object should be bound to, and does a callback on the given function.</summary>
	[Guid("ef24f689-14f8-4d92-b4af-d7b1f0e70fd4")]
	public class AppDomainHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.AppDomainHelper" /> class.</summary>
		[System.MonoTODO]
		public AppDomainHelper()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases unmanaged resources and performs other cleanup operations before the <see cref="T:System.ComponentModel.Component" /> is reclaimed by garbage collection.</summary>
		[System.MonoTODO]
		~AppDomainHelper()
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Locates an assembly and returns information about its modules.</summary>
	[Guid("458aa3b5-265a-4b75-bc05-9bea4630cf18")]
	public class AssemblyLocator : MarshalByRefObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.AssemblyLocator" /> class.</summary>
		[System.MonoTODO]
		public AssemblyLocator()
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Defines a static <see cref="M:System.EnterpriseServices.Internal.ClientRemotingConfig.Write(System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String)" /> method that creates a client remoting configuration file for a client type library.</summary>
	public class ClientRemotingConfig
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.ClientRemotingConfig" /> class.</summary>
		[System.MonoTODO]
		public ClientRemotingConfig()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a client remoting configuration file for a client type library in a SOAP-enabled COM+ application.</summary>
		/// <param name="DestinationDirectory">The folder in which to create the configuration file.</param>
		/// <param name="VRoot">The name of the virtual root.</param>
		/// <param name="BaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="AssemblyName">The display name of the assembly that contains common language runtime (CLR) metadata corresponding to the type library.</param>
		/// <param name="TypeName">The fully qualified name of the assembly that contains CLR metadata corresponding to the type library.</param>
		/// <param name="ProgId">The programmatic identifier of the class.</param>
		/// <param name="Mode">The activation mode.</param>
		/// <param name="Transport">Not used. Specify <see langword="null" /> for this parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the client remoting configuration file was successfully created; otherwise <see langword="false" />.</returns>
		[System.MonoTODO]
		public static bool Write(string DestinationDirectory, string VRoot, string BaseUrl, string AssemblyName, string TypeName, string ProgId, string Mode, string Transport)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Activates SOAP-enabled COM+ application proxies from a client.</summary>
	[Guid("ecabafd1-7f19-11d2-978e-0000f8757e2a")]
	public class ClrObjectFactory : IClrObjectFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.ClrObjectFactory" /> class.</summary>
		[System.MonoTODO]
		public ClrObjectFactory()
		{
			throw new NotImplementedException();
		}

		/// <summary>Activates a remote assembly through .NET remoting, using the assembly's configuration file.</summary>
		/// <param name="AssemblyName">The name of the assembly to activate.</param>
		/// <param name="TypeName">The name of the type to activate.</param>
		/// <param name="Mode">Not used.</param>
		/// <returns>An instance of the <see cref="T:System.Object" /> that represents the type, with culture, arguments, and binding and activation attributes set to <see langword="null" />, or <see langword="null" /> if the <paramref name="TypeName" /> parameter is not found.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The class is not registered.</exception>
		[System.MonoTODO]
		public object CreateFromAssembly(string AssemblyName, string TypeName, string Mode)
		{
			throw new NotImplementedException();
		}

		/// <summary>Activates a remote assembly through .NET remoting, using the remote assembly's mailbox. Currently not implemented; throws a <see cref="T:System.Runtime.InteropServices.COMException" /> if called.</summary>
		/// <param name="Mailbox">A mailbox on the Web service.</param>
		/// <param name="Mode">Not used.</param>
		/// <returns>This method throws an exception if called.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">Simple Mail Transfer Protocol (SMTP) is not implemented.</exception>
		[System.MonoTODO]
		public object CreateFromMailbox(string Mailbox, string Mode)
		{
			throw new NotImplementedException();
		}

		/// <summary>Activates a remote assembly through .NET remoting, using the virtual root URL of the remote assembly.</summary>
		/// <param name="VrootUrl">The virtual root URL of the object to be activated.</param>
		/// <param name="Mode">Not used.</param>
		/// <returns>An instance of the <see cref="T:System.Object" /> representing the type, with culture, arguments, and binding and activation attributes set to <see langword="null" />, or <see langword="null" /> if the assembly identified by the <paramref name="VrootUrl" /> parameter is not found.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The thread token could not be opened.</exception>
		[System.MonoTODO]
		public object CreateFromVroot(string VrootUrl, string Mode)
		{
			throw new NotImplementedException();
		}

		/// <summary>Activates a remote assembly through .NET remoting, using the Web Services Description Language (WSDL) of the XML Web service.</summary>
		/// <param name="WsdlUrl">The WSDL URL of the Web service.</param>
		/// <param name="Mode">Not used.</param>
		/// <returns>An instance of the <see cref="T:System.Object" /> representing the type, with culture, arguments, and binding and activation attributes set to <see langword="null" />, or <see langword="null" /> if the assembly identified by the <paramref name="WsdlUrl" /> parameter is not found.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The thread token could not be opened.</exception>
		[System.MonoTODO]
		public object CreateFromWsdl(string WsdlUrl, string Mode)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Identifies and installs components in the COM+ catalog.</summary>
	[Guid("3b0398c9-7812-4007-85cb-18c771f2206f")]
	public class ComManagedImportUtil : IComManagedImportUtil
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.ComManagedImportUtil" /> class.</summary>
		[System.MonoTODO]
		public ComManagedImportUtil()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the component information from the assembly.</summary>
		/// <param name="assemblyPath">The path to the assembly.</param>
		/// <param name="numComponents">When this method returns, this parameter contains the number of components in the assembly.</param>
		/// <param name="componentInfo">When this method returns, this parameter contains the information about the components.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyPath" /> is an empty string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.  
		/// -or-  
		/// The system could not retrieve the absolute path.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="assemblyPath" /> contains a colon (":").</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		[System.MonoTODO]
		public void GetComponentInfo(string assemblyPath, out string numComponents, out string componentInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Installs an assembly into a COM+ application.</summary>
		/// <param name="asmpath">The path for the assembly.</param>
		/// <param name="parname">The COM+ partition name.</param>
		/// <param name="appname">The COM+ application name.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.EnterpriseServices.RegistrationException">The input assembly does not have a strong name.</exception>
		[System.MonoTODO]
		public void InstallAssembly(string asmpath, string parname, string appname)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Error handler for publishing SOAP-enabled services in COM+ applications.</summary>
	public class ComSoapPublishError
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.ComSoapPublishError" /> class.</summary>
		[System.MonoTODO]
		public ComSoapPublishError()
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes to an event log an error encountered while publishing SOAP-enabled COM interfaces in COM+ applications.</summary>
		/// <param name="s">An error message to be written to the event log.</param>
		[System.MonoTODO]
		public static void Report(string s)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Generates common language runtime (CLR) metadata for a COM+ component.</summary>
	[Guid("d8013ff1-730b-45e2-ba24-874b7242c425")]
	public class GenerateMetadata : IComSoapMetadata
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.GenerateMetadata" /> class.</summary>
		[System.MonoTODO]
		public GenerateMetadata()
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates, or locates, an assembly that contains common language runtime (CLR) metadata for a COM+ component represented by the specified type library.</summary>
		/// <param name="strSrcTypeLib">The name of the type library for which to generate an assembly.</param>
		/// <param name="outPath">The folder in which to generate an assembly or to locate an already existing assembly.</param>
		/// <returns>The generated assembly name; otherwise, an empty string if the inputs are invalid.</returns>
		[System.MonoTODO]
		public string Generate(string strSrcTypeLib, string outPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates, or locates, an assembly that contains common language runtime (CLR) metadata for a COM+ component represented by the specified type library, signs the assembly with a strong-named key pair, and installs it in the global assembly cache.</summary>
		/// <param name="strSrcTypeLib">The name of the type library for which to generate an assembly.</param>
		/// <param name="outPath">The folder in which to generate an assembly or to locate an already existing assembly.</param>
		/// <param name="PublicKey">A public key used to import type library information into an assembly.</param>
		/// <param name="KeyPair">A strong-named key pair used to sign the generated assembly.</param>
		/// <returns>The generated assembly name; otherwise, an empty string if the inputs are invalid.</returns>
		[System.MonoTODO]
		public string GenerateMetaData(string strSrcTypeLib, string outPath, byte[] PublicKey, StrongNameKeyPair KeyPair)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates, or locates, an assembly that contains common language runtime (CLR) metadata for a COM+ component represented by the specified type library, signs the assembly with a strong-named key pair, and installs it in the global assembly cache.</summary>
		/// <param name="strSrcTypeLib">The name of the type library for which to generate an assembly.</param>
		/// <param name="outPath">The folder in which to generate an assembly or to locate an already existing assembly.</param>
		/// <param name="InstallGac">Ignored.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		/// <returns>The generated assembly name; otherwise, an empty string if the inputs are invalid.</returns>
		[System.MonoTODO]
		public string GenerateSigned(string strSrcTypeLib, string outPath, bool InstallGac, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for a specified file in a specified path.</summary>
		/// <param name="path">The path to be searched for the file.</param>
		/// <param name="fileName">The name of the file for which to search.</param>
		/// <param name="extension">An extension to be added to the file name when searching for the file.</param>
		/// <param name="numBufferChars">The size of the buffer that receives the valid path and file name.</param>
		/// <param name="buffer">The buffer that receives the path and file name of the file found.</param>
		/// <param name="filePart">The variable that receives the address of the last component of the valid path and file name.</param>
		/// <returns>If the search succeeds, the return value is the length of the string copied to <paramref name="buffer" />. If the search fails, the return value is 0.</returns>
		[System.MonoTODO]
		public static int SearchPath(string path, string fileName, string extension, int numBufferChars, string buffer, int[] filePart)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Activates SOAP-enabled COM+ application proxies from a client.</summary>
	[Guid("ecabafd2-7f19-11d2-978e-0000f8757e2a")]
	public interface IClrObjectFactory
	{
		/// <summary>Activates a remote assembly through .NET remoting, using the assembly's configuration file.</summary>
		/// <param name="assembly">The name of the assembly to activate.</param>
		/// <param name="type">The name of the type to activate.</param>
		/// <param name="mode">Not used.</param>
		/// <returns>An instance of the <see cref="T:System.Object" /> representing the type, with culture, arguments, and binding and activation attributes set to <see langword="null" />, or <see langword="null" /> if the <paramref name="type" /> parameter is not found.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The class is not registered.</exception>
		[DispId(1)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object CreateFromAssembly(string assembly, string type, string mode);

		/// <summary>Activates a remote assembly through .NET remoting, using the remote assembly's mailbox. Currently not implemented; throws a <see cref="T:System.Runtime.InteropServices.COMException" /> if called.</summary>
		/// <param name="Mailbox">A mailbox on the Web service.</param>
		/// <param name="Mode">Not used.</param>
		/// <returns>This method throws an exception if called.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">Simple Mail Transfer Protocol (SMTP) is not implemented.</exception>
		[DispId(4)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object CreateFromMailbox(string Mailbox, string Mode);

		/// <summary>Activates a remote assembly through .NET remoting, using the virtual root URL of the remote assembly.</summary>
		/// <param name="VrootUrl">The virtual root URL of the remote object.</param>
		/// <param name="Mode">Not used.</param>
		/// <returns>An instance of the <see cref="T:System.Object" /> representing the type, with culture, arguments, and binding and activation attributes set to <see langword="null" />, or <see langword="null" /> if the assembly identified by the <paramref name="VrootUrl" /> parameter is not found.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The thread token could not be opened.</exception>
		[DispId(2)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object CreateFromVroot(string VrootUrl, string Mode);

		/// <summary>Activates a remote assembly through .NET remoting, using the Web Services Description Language (WSDL) of the XML Web service.</summary>
		/// <param name="WsdlUrl">The WSDL URL of the Web service.</param>
		/// <param name="Mode">Not used.</param>
		/// <returns>An instance of the <see cref="T:System.Object" /> representing the type, with culture, arguments, and binding and activation attributes set to <see langword="null" />, or <see langword="null" /> if the assembly identified by the <paramref name="WsdlUrl" /> parameter is not found.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The thread token could not be opened.</exception>
		[DispId(3)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object CreateFromWsdl(string WsdlUrl, string Mode);
	}
	/// <summary>Identifies and installs components in the COM+ catalog.</summary>
	[Guid("c3f8f66b-91be-4c99-a94f-ce3b0a951039")]
	public interface IComManagedImportUtil
	{
		/// <summary>Gets the component information from the assembly.</summary>
		/// <param name="assemblyPath">The path to the assembly.</param>
		/// <param name="numComponents">When this method returns, this parameter contains the number of components in the assembly.</param>
		/// <param name="componentInfo">When this method returns, this parameter contains the information about the components.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyPath" /> is an empty string (""), contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.  
		/// -or-  
		/// The system could not retrieve the absolute path.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="assemblyPath" /> contains a colon (":").</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		[DispId(4)]
		void GetComponentInfo([MarshalAs(UnmanagedType.BStr)] string assemblyPath, [MarshalAs(UnmanagedType.BStr)] out string numComponents, [MarshalAs(UnmanagedType.BStr)] out string componentInfo);

		/// <summary>Installs an assembly into a COM+ application.</summary>
		/// <param name="filename">The path for the assembly.</param>
		/// <param name="parname">The COM+ partition name.</param>
		/// <param name="appname">The COM+ application name.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.EnterpriseServices.RegistrationException">The input assembly does not have a strong name.</exception>
		[DispId(5)]
		void InstallAssembly([MarshalAs(UnmanagedType.BStr)] string filename, [MarshalAs(UnmanagedType.BStr)] string parname, [MarshalAs(UnmanagedType.BStr)] string appname);
	}
	/// <summary>Interface definition for creating and deleting Internet Information Services (IIS) 6.0 virtual roots.</summary>
	[Guid("d8013ef0-730b-45e2-ba24-874b7242c425")]
	public interface IComSoapIISVRoot
	{
		/// <summary>Creates an Internet Information Services (IIS) virtual root.</summary>
		/// <param name="RootWeb">The root Web server.</param>
		/// <param name="PhysicalDirectory">The physical path of the virtual root, which corresponds to <paramref name="PhysicalPath" /> from the <see cref="M:System.EnterpriseServices.Internal.IComSoapPublisher.CreateVirtualRoot(System.String,System.String,System.String@,System.String@,System.String@,System.String@)" /> method.</param>
		/// <param name="VirtualDirectory">The name of the virtual root, which corresponds to <paramref name="VirtualRoot" /> from the <see cref="M:System.EnterpriseServices.Internal.IComSoapPublisher.CreateVirtualRoot(System.String,System.String,System.String@,System.String@,System.String@,System.String@)" /> method.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		[DispId(1)]
		void Create([MarshalAs(UnmanagedType.BStr)] string RootWeb, [MarshalAs(UnmanagedType.BStr)] string PhysicalDirectory, [MarshalAs(UnmanagedType.BStr)] string VirtualDirectory, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Deletes an Internet Information Services (IIS) virtual root.</summary>
		/// <param name="RootWeb">The root Web server.</param>
		/// <param name="PhysicalDirectory">The physical path of the virtual root.</param>
		/// <param name="VirtualDirectory">The name of the virtual root.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		[DispId(2)]
		void Delete([MarshalAs(UnmanagedType.BStr)] string RootWeb, [MarshalAs(UnmanagedType.BStr)] string PhysicalDirectory, [MarshalAs(UnmanagedType.BStr)] string VirtualDirectory, [MarshalAs(UnmanagedType.BStr)] out string Error);
	}
	/// <summary>Specifies methods for generating common language runtime (CLR) metadata for a COM+ component.</summary>
	[Guid("d8013ff0-730b-45e2-ba24-874b7242c425")]
	public interface IComSoapMetadata
	{
		/// <summary>Generates an assembly that contains common language runtime (CLR) metadata for a COM+ component represented by the specified type library.</summary>
		/// <param name="SrcTypeLibFileName">The name of the type library for which to generate an assembly.</param>
		/// <param name="OutPath">The folder in which to generate an assembly.</param>
		/// <returns>The generated assembly name.</returns>
		[DispId(1)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string Generate([MarshalAs(UnmanagedType.BStr)] string SrcTypeLibFileName, [MarshalAs(UnmanagedType.BStr)] string OutPath);

		/// <summary>Generates an assembly that contains common language runtime (CLR) metadata for a COM+ component represented by the specified type library, signs the assembly with a strong-named key pair, and installs it in the global assembly cache.</summary>
		/// <param name="SrcTypeLibFileName">The name of the type library for which to generate an assembly.</param>
		/// <param name="OutPath">The folder in which to generate an assembly.</param>
		/// <param name="InstallGac">A flag that indicates whether to install the assembly in the global assembly cache.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		/// <returns>The generated assembly name.</returns>
		[DispId(2)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GenerateSigned([MarshalAs(UnmanagedType.BStr)] string SrcTypeLibFileName, [MarshalAs(UnmanagedType.BStr)] string OutPath, [MarshalAs(UnmanagedType.Bool)] bool InstallGac, [MarshalAs(UnmanagedType.BStr)] out string Error);
	}
	/// <summary>Publishes COM interfaces for SOAP-enabled COM+ applications.</summary>
	[Guid("d8013eee-730b-45e2-ba24-874b7242c425")]
	public interface IComSoapPublisher
	{
		/// <summary>Creates a SOAP-enabled COM+ application mailbox at a specified URL. Not fully implemented.</summary>
		/// <param name="RootMailServer">The URL for the root mail server.</param>
		/// <param name="MailBox">The mailbox to create.</param>
		/// <param name="SmtpName">When this method returns, this parameter contains the name of the Simple Mail Transfer Protocol (SMTP) server containing the mailbox.</param>
		/// <param name="Domain">When this method returns, this parameter contains the domain of the SMTP server.</param>
		/// <param name="PhysicalPath">When this method returns, this parameter contains the file system path for the mailbox.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		[DispId(6)]
		void CreateMailBox([MarshalAs(UnmanagedType.BStr)] string RootMailServer, [MarshalAs(UnmanagedType.BStr)] string MailBox, [MarshalAs(UnmanagedType.BStr)] out string SmtpName, [MarshalAs(UnmanagedType.BStr)] out string Domain, [MarshalAs(UnmanagedType.BStr)] out string PhysicalPath, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Creates a SOAP-enabled COM+ application virtual root.</summary>
		/// <param name="Operation">The operation to perform.</param>
		/// <param name="FullUrl">The complete URL address for the virtual root.</param>
		/// <param name="BaseUrl">When this method returns, this parameter contains the base URL address.</param>
		/// <param name="VirtualRoot">When this method returns, this parameter contains the name of the virtual root.</param>
		/// <param name="PhysicalPath">When this method returns, this parameter contains the file path for the virtual root.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  The caller does not have permission to access Domain Name System (DNS) information.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="FullUrl" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving the local host name.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="FullUrl" /> is empty.  
		/// -or-  
		/// The scheme specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// <paramref name="FullUrl" /> contains more than two consecutive slashes.  
		/// -or-  
		/// The password specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// The host name specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// The file name specified in <paramref name="FullUrl" /> is invalid.</exception>
		[DispId(4)]
		void CreateVirtualRoot([MarshalAs(UnmanagedType.BStr)] string Operation, [MarshalAs(UnmanagedType.BStr)] string FullUrl, [MarshalAs(UnmanagedType.BStr)] out string BaseUrl, [MarshalAs(UnmanagedType.BStr)] out string VirtualRoot, [MarshalAs(UnmanagedType.BStr)] out string PhysicalPath, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Deletes a SOAP-enabled COM+ application mailbox at a specified URL. Not fully implemented.</summary>
		/// <param name="RootMailServer">The URL for the root mail server.</param>
		/// <param name="MailBox">The mailbox to delete.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		[DispId(7)]
		void DeleteMailBox([MarshalAs(UnmanagedType.BStr)] string RootMailServer, [MarshalAs(UnmanagedType.BStr)] string MailBox, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Deletes a SOAP-enabled COM+ application virtual root. Not fully implemented.</summary>
		/// <param name="RootWebServer">The root Web server.</param>
		/// <param name="FullUrl">The complete URL address for the virtual root.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		[DispId(5)]
		void DeleteVirtualRoot([MarshalAs(UnmanagedType.BStr)] string RootWebServer, [MarshalAs(UnmanagedType.BStr)] string FullUrl, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Installs an assembly in the global assembly cache.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		[DispId(13)]
		void GacInstall([MarshalAs(UnmanagedType.BStr)] string AssemblyPath);

		/// <summary>Removes an assembly from the global assembly cache.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="AssemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="AssemblyPath" /> is empty.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="AssemblyPath" /> is not found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="AssemblyPath" /> is not a valid assembly.</exception>
		[DispId(14)]
		void GacRemove([MarshalAs(UnmanagedType.BStr)] string AssemblyPath);

		/// <summary>Returns the full path for a strong-named signed generated assembly in the SoapCache directory.</summary>
		/// <param name="TypeLibPath">The path for the file that contains the typelib.</param>
		/// <param name="CachePath">When this method returns, this parameter contains the full path of the proxy assembly in the SoapCache directory.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="TypeLibPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">The file name is empty, contains only white spaces, or contains invalid characters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to <paramref name="TypeLibPath" /> is denied.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="TypeLibPath" /> contains a colon (:) in the middle of the string.</exception>
		[DispId(15)]
		void GetAssemblyNameForCache([MarshalAs(UnmanagedType.BStr)] string TypeLibPath, [MarshalAs(UnmanagedType.BStr)] out string CachePath);

		/// <summary>Reflects over an assembly and returns the type name that matches the ProgID.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <param name="ProgId">The programmatic identifier of the class.</param>
		/// <returns>The type name that matches the ProgID.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		[DispId(10)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetTypeNameFromProgId([MarshalAs(UnmanagedType.BStr)] string AssemblyPath, [MarshalAs(UnmanagedType.BStr)] string ProgId);

		/// <summary>Processes a client type library, creating a configuration file on the client.</summary>
		/// <param name="ProgId">The programmatic identifier of the class.</param>
		/// <param name="SrcTlbPath">The path for the file that contains the typelib.</param>
		/// <param name="PhysicalPath">The Web application directory.</param>
		/// <param name="VRoot">The name of the virtual root.</param>
		/// <param name="BaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="Mode">The activation mode.</param>
		/// <param name="Transport">Not used. Specify <see langword="null" /> for this parameter.</param>
		/// <param name="AssemblyName">When this method returns, this parameter contains the display name of the assembly.</param>
		/// <param name="TypeName">When this method returns, this parameter contains the fully-qualified type name of the assembly.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		[DispId(9)]
		void ProcessClientTlb([MarshalAs(UnmanagedType.BStr)] string ProgId, [MarshalAs(UnmanagedType.BStr)] string SrcTlbPath, [MarshalAs(UnmanagedType.BStr)] string PhysicalPath, [MarshalAs(UnmanagedType.BStr)] string VRoot, [MarshalAs(UnmanagedType.BStr)] string BaseUrl, [MarshalAs(UnmanagedType.BStr)] string Mode, [MarshalAs(UnmanagedType.BStr)] string Transport, [MarshalAs(UnmanagedType.BStr)] out string AssemblyName, [MarshalAs(UnmanagedType.BStr)] out string TypeName, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Processes a server type library, either adding or deleting component entries to the Web.config and Default.disco files. Generates a proxy if necessary.</summary>
		/// <param name="ProgId">The programmatic identifier of the class.</param>
		/// <param name="SrcTlbPath">The path for the file that contains the type library.</param>
		/// <param name="PhysicalPath">The Web application directory.</param>
		/// <param name="Operation">The operation to perform.</param>
		/// <param name="AssemblyName">When this method returns, this parameter contains the display name of the assembly.</param>
		/// <param name="TypeName">When this method returns, this parameter contains the fully-qualified type name of the assembly.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The <paramref name="SrcTlbPath" /> parameter referenced scrobj.dll; therefore, SOAP publication of script components is not supported.</exception>
		[DispId(8)]
		void ProcessServerTlb([MarshalAs(UnmanagedType.BStr)] string ProgId, [MarshalAs(UnmanagedType.BStr)] string SrcTlbPath, [MarshalAs(UnmanagedType.BStr)] string PhysicalPath, [MarshalAs(UnmanagedType.BStr)] string Operation, [MarshalAs(UnmanagedType.BStr)] out string AssemblyName, [MarshalAs(UnmanagedType.BStr)] out string TypeName, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Registers an assembly for COM interop.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.EnterpriseServices.RegistrationException">The input assembly does not have a strong name.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="AssemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="AssemblyPath" /> is not found, or a file name extension is not specified.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="AssemblyPath" /> is not a valid assembly.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences, or the assembly name exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not <see langword="static" />.  
		///  -or-  
		///  There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> at a given level of the hierarchy.  
		///  -or-  
		///  The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not valid.</exception>
		[DispId(11)]
		void RegisterAssembly([MarshalAs(UnmanagedType.BStr)] string AssemblyPath);

		/// <summary>Unregisters a COM interop assembly.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="AssemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="AssemblyPath" /> is not found, or a file name extension is not specified.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="AssemblyPath" /> is not a valid assembly.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences, or the assembly name exceeds the syste-defined maximum length.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not <see langword="static" />.  
		///  -or-  
		///  There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> at a given level of the hierarchy.  
		///  -or-  
		///  The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not valid.</exception>
		[DispId(12)]
		void UnRegisterAssembly([MarshalAs(UnmanagedType.BStr)] string AssemblyPath);
	}
	/// <summary>Creates and deletes Internet Information Services (IIS) 6.0 virtual roots.</summary>
	[Guid("d8013ef1-730b-45e2-ba24-874b7242c425")]
	public class IISVirtualRoot : IComSoapIISVRoot
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.IISVirtualRoot" /> class.</summary>
		[System.MonoTODO]
		public IISVirtualRoot()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an Internet Information Services (IIS) virtual root.</summary>
		/// <param name="RootWeb">A string with the value <c>"IIS://localhost/W3SVC/1/ROOT"</c> representing the root Web server.</param>
		/// <param name="inPhysicalDirectory">The physical path of the virtual root, which corresponds to <paramref name="PhysicalPath" /> from the <see cref="M:System.EnterpriseServices.Internal.Publish.CreateVirtualRoot(System.String,System.String,System.String@,System.String@,System.String@,System.String@)" /> method.</param>
		/// <param name="VirtualDirectory">The name of the virtual root, which corresponds to <paramref name="VirtualRoot" /> from <see cref="M:System.EnterpriseServices.Internal.Publish.CreateVirtualRoot(System.String,System.String,System.String@,System.String@,System.String@,System.String@)" />.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		[System.MonoTODO]
		public void Create(string RootWeb, string inPhysicalDirectory, string VirtualDirectory, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes an Internet Information Services (IIS) virtual root.</summary>
		/// <param name="RootWeb">The root Web server, as specified by <paramref name="RootWebServer" /> from the <see cref="M:System.EnterpriseServices.Internal.IComSoapPublisher.DeleteVirtualRoot(System.String,System.String,System.String@)" /> method.</param>
		/// <param name="PhysicalDirectory">The physical path of the virtual root.</param>
		/// <param name="VirtualDirectory">The name of the virtual root.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		[System.MonoTODO]
		public void Delete(string RootWeb, string PhysicalDirectory, string VirtualDirectory, out string Error)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Creates a Web.config file for a SOAP-enabled COM+ application and adds component entries to the file for COM interfaces being published in the application.</summary>
	[Guid("6261e4b5-572a-4142-a2f9-1fe1a0c97097")]
	public interface IServerWebConfig
	{
		/// <summary>Adds XML elements to a Web.config file for a COM interface being published in a SOAP-enabled COM+ application.</summary>
		/// <param name="FilePath">The path for the existing Web.config file.</param>
		/// <param name="AssemblyName">The name of the assembly that contains the type being added.</param>
		/// <param name="TypeName">The name of the type being added.</param>
		/// <param name="ProgId">The programmatic identifier for the type being added.</param>
		/// <param name="Mode">A string constant that corresponds to the name of a member from the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> enumeration, which indicates how a well-known object is activated.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		[DispId(1)]
		void AddElement([MarshalAs(UnmanagedType.BStr)] string FilePath, [MarshalAs(UnmanagedType.BStr)] string AssemblyName, [MarshalAs(UnmanagedType.BStr)] string TypeName, [MarshalAs(UnmanagedType.BStr)] string ProgId, [MarshalAs(UnmanagedType.BStr)] string Mode, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Creates a Web.config file for a SOAP-enabled COM+ application so that the file is ready to have XML elements added for COM interfaces being published.</summary>
		/// <param name="FilePath">The folder in which to create the configuration file.</param>
		/// <param name="FileRootName">The string value to which a config extension can be added (for example, Web for Web.config).</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		[DispId(2)]
		void Create([MarshalAs(UnmanagedType.BStr)] string FilePath, [MarshalAs(UnmanagedType.BStr)] string FileRootName, [MarshalAs(UnmanagedType.BStr)] out string Error);
	}
	/// <summary>Imports authenticated, encrypted SOAP client proxies.</summary>
	[Guid("E7F0F021-9201-47e4-94DA-1D1416DEC27A")]
	public interface ISoapClientImport
	{
		/// <summary>Creates a .NET remoting client configuration file that includes security and authentication options.</summary>
		/// <param name="progId">The programmatic identifier of the class. If an empty string (""), this method returns without doing anything.</param>
		/// <param name="virtualRoot">The name of the virtual root.</param>
		/// <param name="baseUrl">The base URL that contains the virtual root.</param>
		/// <param name="authentication">The type of ASP.NET authentication to use.</param>
		/// <param name="assemblyName">The name of the assembly.</param>
		/// <param name="typeName">The name of the type.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		[DispId(1)]
		void ProcessClientTlbEx([MarshalAs(UnmanagedType.BStr)] string progId, [MarshalAs(UnmanagedType.BStr)] string virtualRoot, [MarshalAs(UnmanagedType.BStr)] string baseUrl, [MarshalAs(UnmanagedType.BStr)] string authentication, [MarshalAs(UnmanagedType.BStr)] string assemblyName, [MarshalAs(UnmanagedType.BStr)] string typeName);
	}
	/// <summary>Processes authenticated, encrypted SOAP components on servers.</summary>
	[Guid("1E7BA9F7-21DB-4482-929E-21BDE2DFE51C")]
	public interface ISoapServerTlb
	{
		/// <summary>Adds the entries for a server type library to the Web.config and Default.disco files, depending on security options, and generates a proxy if necessary.</summary>
		/// <param name="progId">The programmatic identifier of the class.</param>
		/// <param name="classId">The class identifier (CLSID) for the type library.</param>
		/// <param name="interfaceId">The IID for the type library.</param>
		/// <param name="srcTlbPath">The path for the file containing the type library.</param>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="baseUrl">The base URL that contains the virtual root.</param>
		/// <param name="virtualRoot">The name of the virtual root.</param>
		/// <param name="clientActivated">
		///   <see langword="true" /> if client activated; otherwise, <see langword="false" />.</param>
		/// <param name="wellKnown">
		///   <see langword="true" /> if well-known; otherwise, <see langword="false" />.</param>
		/// <param name="discoFile">
		///   <see langword="true" /> if a discovery file; otherwise, <see langword="false" />.</param>
		/// <param name="operation">The operation to perform. Specify either "delete" or an empty string.</param>
		/// <param name="assemblyName">When this method returns, contains the name of the assembly.</param>
		/// <param name="typeName">When this method returns, contains the type of the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to get the system directory failed.</exception>
		[DispId(1)]
		void AddServerTlb([MarshalAs(UnmanagedType.BStr)] string progId, [MarshalAs(UnmanagedType.BStr)] string classId, [MarshalAs(UnmanagedType.BStr)] string interfaceId, [MarshalAs(UnmanagedType.BStr)] string srcTlbPath, [MarshalAs(UnmanagedType.BStr)] string rootWebServer, [MarshalAs(UnmanagedType.BStr)] string baseUrl, [MarshalAs(UnmanagedType.BStr)] string virtualRoot, [MarshalAs(UnmanagedType.BStr)] string clientActivated, [MarshalAs(UnmanagedType.BStr)] string wellKnown, [MarshalAs(UnmanagedType.BStr)] string discoFile, [MarshalAs(UnmanagedType.BStr)] string operation, [MarshalAs(UnmanagedType.BStr)] out string assemblyName, [MarshalAs(UnmanagedType.BStr)] out string typeName);

		/// <summary>Removes entries for a server type library from the Web.config and Default.disco files, depending on security options.</summary>
		/// <param name="progId">The programmatic identifier of the class.</param>
		/// <param name="classId">The class identifier (CLSID) for the type library.</param>
		/// <param name="interfaceId">The IID for the type library.</param>
		/// <param name="srcTlbPath">The path for the file containing the type library.</param>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="baseUrl">The base URL that contains the virtual root.</param>
		/// <param name="virtualRoot">The name of the virtual root.</param>
		/// <param name="operation">Not used. Specify <see langword="null" /> for this parameter.</param>
		/// <param name="assemblyName">The name of the assembly.</param>
		/// <param name="typeName">The type of the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		[DispId(2)]
		void DeleteServerTlb([MarshalAs(UnmanagedType.BStr)] string progId, [MarshalAs(UnmanagedType.BStr)] string classId, [MarshalAs(UnmanagedType.BStr)] string interfaceId, [MarshalAs(UnmanagedType.BStr)] string srcTlbPath, [MarshalAs(UnmanagedType.BStr)] string rootWebServer, [MarshalAs(UnmanagedType.BStr)] string baseUrl, [MarshalAs(UnmanagedType.BStr)] string virtualRoot, [MarshalAs(UnmanagedType.BStr)] string operation, [MarshalAs(UnmanagedType.BStr)] string assemblyName, [MarshalAs(UnmanagedType.BStr)] string typeName);
	}
	/// <summary>Publishes authenticated, encrypted SOAP virtual roots on servers.</summary>
	[Guid("A31B6577-71D2-4344-AEDF-ADC1B0DC5347")]
	public interface ISoapServerVRoot
	{
		/// <summary>Creates a SOAP virtual root with security options.</summary>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="inBaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="homePage">
		///   <see langword="true" /> if the <see langword="EnableDefaultDoc" /> property is to be set; otherwise, <see langword="false" />.</param>
		/// <param name="discoFile">
		///   <see langword="true" /> if a default discovery file is to be created; <see langword="false" /> if there is to be no discovery file. If <see langword="false" /> and a Default.disco file exists, the file is deleted.</param>
		/// <param name="secureSockets">
		///   <see langword="true" /> if SSL encryption is required; otherwise, <see langword="false" />.</param>
		/// <param name="authentication">Specify "anonymous" if no authentication is to be used (anonymous user). Otherwise, specify an empty string.</param>
		/// <param name="operation">Not used. Specify <see langword="null" /> for this parameter.</param>
		/// <param name="baseUrl">When this method returns, this parameter contains the base URL.</param>
		/// <param name="virtualRoot">When this method returns, this parameter contains the name of the virtual root.</param>
		/// <param name="physicalPath">When this method returns, this parameter contains the disk address of the Virtual Root directory.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		[DispId(1)]
		void CreateVirtualRootEx([MarshalAs(UnmanagedType.BStr)] string rootWebServer, [MarshalAs(UnmanagedType.BStr)] string inBaseUrl, [MarshalAs(UnmanagedType.BStr)] string inVirtualRoot, [MarshalAs(UnmanagedType.BStr)] string homePage, [MarshalAs(UnmanagedType.BStr)] string discoFile, [MarshalAs(UnmanagedType.BStr)] string secureSockets, [MarshalAs(UnmanagedType.BStr)] string authentication, [MarshalAs(UnmanagedType.BStr)] string operation, [MarshalAs(UnmanagedType.BStr)] out string baseUrl, [MarshalAs(UnmanagedType.BStr)] out string virtualRoot, [MarshalAs(UnmanagedType.BStr)] out string physicalPath);

		/// <summary>Deletes a virtual root. Not fully implemented.</summary>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="baseUrl">The base URL that contains the virtual root.</param>
		/// <param name="virtualRoot">The name of the virtual root.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to identify the system directory failed.</exception>
		[DispId(2)]
		void DeleteVirtualRootEx([MarshalAs(UnmanagedType.BStr)] string rootWebServer, [MarshalAs(UnmanagedType.BStr)] string baseUrl, [MarshalAs(UnmanagedType.BStr)] string virtualRoot);

		/// <summary>Returns the security status of an existing SOAP virtual root.</summary>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="inBaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="exists">When this method returns, this parameter contains a <see langword="true" /> if the virtual directory exists; otherwise, <see langword="false" />.</param>
		/// <param name="secureSockets">When this method returns, this parameter contains a <see langword="true" /> if SSL encryption is required; otherwise, <see langword="false" />.</param>
		/// <param name="windowsAuth">When this method returns, this parameter contains <see langword="true" /> if Windows authentication is set, otherwise, <see langword="false" />.</param>
		/// <param name="anonymous">When this method returns, this parameter contains <see langword="true" /> if no authentication is set (anonymous user); otherwise, <see langword="false" />.</param>
		/// <param name="homePage">When this method returns, this parameter contains a <see langword="true" /> if the Virtual Root directory's <see langword="EnableDefaultDoc" /> property is set; otherwise, <see langword="false" />.</param>
		/// <param name="discoFile">When this method returns, this parameter contains a <see langword="true" /> if a Default.disco file exists; otherwise, <see langword="false" />.</param>
		/// <param name="physicalPath">When this method returns, this parameter contains the disk address of the Virtual Root directory.</param>
		/// <param name="baseUrl">When this method returns, this parameter contains the base URL.</param>
		/// <param name="virtualRoot">When this method returns, this parameter contains the name of the virtual root.</param>
		[DispId(3)]
		void GetVirtualRootStatus([MarshalAs(UnmanagedType.BStr)] string rootWebServer, [MarshalAs(UnmanagedType.BStr)] string inBaseUrl, [MarshalAs(UnmanagedType.BStr)] string inVirtualRoot, [MarshalAs(UnmanagedType.BStr)] out string exists, [MarshalAs(UnmanagedType.BStr)] out string secureSockets, [MarshalAs(UnmanagedType.BStr)] out string windowsAuth, [MarshalAs(UnmanagedType.BStr)] out string anonymous, [MarshalAs(UnmanagedType.BStr)] out string homePage, [MarshalAs(UnmanagedType.BStr)] out string discoFile, [MarshalAs(UnmanagedType.BStr)] out string physicalPath, [MarshalAs(UnmanagedType.BStr)] out string baseUrl, [MarshalAs(UnmanagedType.BStr)] out string virtualRoot);
	}
	/// <summary>Provides utilities to support the exporting of COM+ SOAP-enabled application proxies by the server and the importing of the proxies by the client.</summary>
	[Guid("5AC4CB7E-F89F-429b-926B-C7F940936BF4")]
	public interface ISoapUtility
	{
		/// <summary>Returns the path for the SOAP virtual root bin directory.</summary>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="inBaseUrl">The base URL address.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="binPath">When this method returns, this parameter contains the file path for the SOAP virtual root bin directory.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to get the system directory failed.</exception>
		[DispId(2)]
		void GetServerBinPath([MarshalAs(UnmanagedType.BStr)] string rootWebServer, [MarshalAs(UnmanagedType.BStr)] string inBaseUrl, [MarshalAs(UnmanagedType.BStr)] string inVirtualRoot, [MarshalAs(UnmanagedType.BStr)] out string binPath);

		/// <summary>Returns the path for the SOAP virtual root.</summary>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="inBaseUrl">The base URL address.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="physicalPath">When this method returns, this parameter contains the file path for the SOAP virtual root.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to get the system directory failed.</exception>
		[DispId(1)]
		void GetServerPhysicalPath([MarshalAs(UnmanagedType.BStr)] string rootWebServer, [MarshalAs(UnmanagedType.BStr)] string inBaseUrl, [MarshalAs(UnmanagedType.BStr)] string inVirtualRoot, [MarshalAs(UnmanagedType.BStr)] out string physicalPath);

		/// <summary>Determines whether authenticated, encrypted SOAP interfaces are present.</summary>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		[DispId(3)]
		void Present();
	}
	/// <summary>Publishes COM interfaces for SOAP-enabled COM+ applications.</summary>
	[Guid("d8013eef-730b-45e2-ba24-874b7242c425")]
	public class Publish : IComSoapPublisher
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.Publish" /> class.</summary>
		[System.MonoTODO]
		public Publish()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a SOAP-enabled COM+ application mailbox at a specified URL. Not fully implemented.</summary>
		/// <param name="RootMailServer">The URL for the root mail server.</param>
		/// <param name="MailBox">The mailbox to create.</param>
		/// <param name="SmtpName">When this method returns, this parameter contains the name of the Simple Mail Transfer Protocol (SMTP) server containing the mailbox.</param>
		/// <param name="Domain">When this method returns, this parameter contains the domain of the SMTP server.</param>
		/// <param name="PhysicalPath">When this method returns, this parameter contains the file system path for the mailbox.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		[System.MonoTODO]
		public void CreateMailBox(string RootMailServer, string MailBox, out string SmtpName, out string Domain, out string PhysicalPath, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a SOAP-enabled COM+ application virtual root.</summary>
		/// <param name="Operation">The operation to perform.</param>
		/// <param name="FullUrl">The complete URL address for the virtual root.</param>
		/// <param name="BaseUrl">When this method returns, this parameter contains the base URL address.</param>
		/// <param name="VirtualRoot">When this method returns, this parameter contains the name of the virtual root.</param>
		/// <param name="PhysicalPath">When this method returns, this parameter contains the file path for the virtual root.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  The caller does not have permission to access DNS information.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="FullUrl" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving the local host name.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="FullUrl" /> is empty.  
		/// -or-  
		/// The scheme specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// <paramref name="FullUrl" /> contains more than two consecutive slashes.  
		/// -or-  
		/// The password specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// The host name specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// The file name specified in <paramref name="FullUrl" /> is invalid.</exception>
		[System.MonoTODO]
		public void CreateVirtualRoot(string Operation, string FullUrl, out string BaseUrl, out string VirtualRoot, out string PhysicalPath, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes a SOAP-enabled COM+ application mailbox at a specified URL. Not fully implemented.</summary>
		/// <param name="RootMailServer">The URL for the root mail server.</param>
		/// <param name="MailBox">The mailbox to delete.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		[System.MonoTODO]
		public void DeleteMailBox(string RootMailServer, string MailBox, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes a SOAP-enabled COM+ application virtual root. Not fully implemented.</summary>
		/// <param name="RootWebServer">The root Web server.</param>
		/// <param name="FullUrl">The complete URL address for the virtual root.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		[System.MonoTODO]
		public void DeleteVirtualRoot(string RootWebServer, string FullUrl, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Installs an assembly in the global assembly cache.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		[System.MonoTODO]
		public void GacInstall(string AssemblyPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes an assembly from the global assembly cache.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  The caller does not have path discovery permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="AssemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="AssemblyPath" /> is empty.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="AssemblyPath" /> is not found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="AssemblyPath" /> is not a valid assembly.</exception>
		[System.MonoTODO]
		public void GacRemove(string AssemblyPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the full path for a strong-named signed generated assembly in the SoapCache directory.</summary>
		/// <param name="TypeLibPath">The path for the file that contains the typelib.</param>
		/// <param name="CachePath">When this method returns, this parameter contains the name of the SoapCache directory.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="TypeLibPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">The file name is empty, contains only white spaces, or contains invalid characters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to <paramref name="TypeLibPath" /> is denied.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="TypeLibPath" /> contains a colon (:) in the middle of the string.</exception>
		[System.MonoTODO]
		public void GetAssemblyNameForCache(string TypeLibPath, out string CachePath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the path for the directory for storing client configuration files.</summary>
		/// <param name="CreateDir">Set to <see langword="true" /> to create the directory, or <see langword="false" /> to return the path but not create the directory.</param>
		/// <returns>The path for the directory to contain the configuration files.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		[System.MonoTODO]
		public static string GetClientPhysicalPath(bool CreateDir)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reflects over an assembly and returns the type name that matches the ProgID.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <param name="ProgId">The programmatic identifier of the class.</param>
		/// <returns>The type name that matches the ProgID.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		[System.MonoTODO]
		public string GetTypeNameFromProgId(string AssemblyPath, string ProgId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Parses a URL and returns the base URL and virtual root portions.</summary>
		/// <param name="FullUrl">The complete URL address for the virtual root.</param>
		/// <param name="BaseUrl">When this method returns, this parameter contains the base URL address.</param>
		/// <param name="VirtualRoot">When this method returns, this parameter contains the name of the virtual root.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="FullUrl" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error is encountered when resolving the local host name.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission to access DNS information.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="FullUrl" /> is empty.  
		/// -or-  
		/// The scheme specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// <paramref name="FullUrl" /> contains too many slashes.  
		/// -or-  
		/// The password specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// The host name specified in <paramref name="FullUrl" /> is invalid.  
		/// -or-  
		/// The file name specified in <paramref name="FullUrl" /> is invalid.</exception>
		[System.MonoTODO]
		public static void ParseUrl(string FullUrl, out string BaseUrl, out string VirtualRoot)
		{
			throw new NotImplementedException();
		}

		/// <summary>Processes a client type library, creating a configuration file on the client.</summary>
		/// <param name="ProgId">The programmatic identifier of the class.</param>
		/// <param name="SrcTlbPath">The path for the file that contains the typelib.</param>
		/// <param name="PhysicalPath">The Web application directory.</param>
		/// <param name="VRoot">The name of the virtual root.</param>
		/// <param name="BaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="Mode">The activation mode.</param>
		/// <param name="Transport">Not used. Specify <see langword="null" /> for this parameter.</param>
		/// <param name="AssemblyName">When this method returns, this parameter contains the display name of the assembly.</param>
		/// <param name="TypeName">When this method returns, this parameter contains the fully-qualified type name of the assembly.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		[System.MonoTODO]
		public void ProcessClientTlb(string ProgId, string SrcTlbPath, string PhysicalPath, string VRoot, string BaseUrl, string Mode, string Transport, out string AssemblyName, out string TypeName, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Processes a server type library, either adding or deleting component entries to the Web.config and Default.disco files. Generates a proxy if necessary.</summary>
		/// <param name="ProgId">The programmatic identifier of the class.</param>
		/// <param name="SrcTlbPath">The path for the file that contains the type library.</param>
		/// <param name="PhysicalPath">The Web application directory.</param>
		/// <param name="Operation">The operation to perform.</param>
		/// <param name="strAssemblyName">When this method returns, this parameter contains the display name of the assembly.</param>
		/// <param name="TypeName">When this method returns, this parameter contains the fully-qualified type name of the assembly.</param>
		/// <param name="Error">When this method returns, this parameter contains an error message if a problem was encountered.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The <paramref name="SrcTlbPath" /> parameter referenced scrobj.dll; therefore, SOAP publication of script components is not supported.</exception>
		[System.MonoTODO]
		public void ProcessServerTlb(string ProgId, string SrcTlbPath, string PhysicalPath, string Operation, out string strAssemblyName, out string TypeName, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Registers an assembly for COM interop.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.EnterpriseServices.RegistrationException">The input assembly does not have a strong name.</exception>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="AssemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="AssemblyPath" /> is not found, or a filename extension is not specified.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="AssemblyPath" /> is not a valid assembly.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences, or the assembly name exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not <see langword="static" />.  
		///  -or-  
		///  There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> at a given level of the hierarchy.  
		///  -or-  
		///  The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not valid.</exception>
		[System.MonoTODO]
		public void RegisterAssembly(string AssemblyPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Unregisters a COM interop assembly.</summary>
		/// <param name="AssemblyPath">The file system path for the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="AssemblyPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="AssemblyPath" /> is not found, or a file name extension is not specified.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="AssemblyPath" /> is not a valid assembly.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences, or the assembly name exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not <see langword="static" />.  
		///  -or-  
		///  There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> at a given level of the hierarchy.  
		///  -or-  
		///  The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not valid.</exception>
		[System.MonoTODO]
		public void UnRegisterAssembly(string AssemblyPath)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Creates a Web.config file for a SOAP-enabled COM+ application. Can also add component entries to the file for COM interfaces being published in the application.</summary>
	public class ServerWebConfig : IServerWebConfig
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.ServerWebConfig" /> class.</summary>
		[System.MonoTODO]
		public ServerWebConfig()
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds XML elements to a Web.config file for a COM interface being published in a SOAP-enabled COM+ application.</summary>
		/// <param name="FilePath">The path of the existing Web.config file.</param>
		/// <param name="AssemblyName">The name of the assembly that contains the type being added.</param>
		/// <param name="TypeName">The name of the type being added.</param>
		/// <param name="ProgId">The programmatic identifier for the type being added.</param>
		/// <param name="WkoMode">A string constant that corresponds to the name of a member from the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> enumeration, which indicates how a well-known object is activated.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		[System.MonoTODO]
		public void AddElement(string FilePath, string AssemblyName, string TypeName, string ProgId, string WkoMode, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a Web.config file for a SOAP-enabled COM+ application so that the file is ready to have XML elements added for COM interfaces being published.</summary>
		/// <param name="FilePath">The folder in which the configuration file should be created.</param>
		/// <param name="FilePrefix">The string value "Web", to which a config extension is added.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		[System.MonoTODO]
		public void Create(string FilePath, string FilePrefix, out string Error)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Imports authenticated, encrypted SOAP client proxies. This class cannot be inherited.</summary>
	[Guid("346D5B9F-45E1-45c0-AADF-1B7D221E9063")]
	public sealed class SoapClientImport : ISoapClientImport
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.SoapClientImport" /> class.</summary>
		[System.MonoTODO]
		public SoapClientImport()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a .NET remoting client configuration file that includes security and authentication options.</summary>
		/// <param name="progId">The programmatic identifier of the class. If an empty string (""), this method returns without doing anything.</param>
		/// <param name="virtualRoot">The name of the virtual root.</param>
		/// <param name="baseUrl">The base URL that contains the virtual root.</param>
		/// <param name="authentication">The type of ASP.NET authentication to use.</param>
		/// <param name="assemblyName">The name of the assembly.</param>
		/// <param name="typeName">The name of the type.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		[System.MonoTODO]
		public void ProcessClientTlbEx(string progId, string virtualRoot, string baseUrl, string authentication, string assemblyName, string typeName)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Processes authenticated, encrypted SOAP components on servers. This class cannot be inherited.</summary>
	[Guid("F6B6768F-F99E-4152-8ED2-0412F78517FB")]
	public sealed class SoapServerTlb : ISoapServerTlb
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.SoapServerTlb" /> class.</summary>
		[System.MonoTODO]
		public SoapServerTlb()
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds the entries for a server type library to the Web.config and Default.disco files, depending on security options, and generates a proxy if necessary.</summary>
		/// <param name="progId">The programmatic identifier of the class.</param>
		/// <param name="classId">The class identifier (CLSID) for the type library.</param>
		/// <param name="interfaceId">The IID for the type library.</param>
		/// <param name="srcTlbPath">The path for the file containing the type library.</param>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="inBaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="clientActivated">
		///   <see langword="true" /> if client activated; otherwise, <see langword="false" />.</param>
		/// <param name="wellKnown">
		///   <see langword="true" /> if well-known; otherwise, <see langword="false" />.</param>
		/// <param name="discoFile">
		///   <see langword="true" /> if a discovery file; otherwise, <see langword="false" />.</param>
		/// <param name="operation">The operation to perform. Specify either "delete" or an empty string.</param>
		/// <param name="strAssemblyName">When this method returns, contains the name of the assembly.</param>
		/// <param name="typeName">When this method returns, contains the type of the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to get the system directory failed.</exception>
		[System.MonoTODO]
		public void AddServerTlb(string progId, string classId, string interfaceId, string srcTlbPath, string rootWebServer, string inBaseUrl, string inVirtualRoot, string clientActivated, string wellKnown, string discoFile, string operation, out string strAssemblyName, out string typeName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes entries for a server type library from the Web.config and Default.disco files, depending on security options.</summary>
		/// <param name="progId">The programmatic identifier of the class.</param>
		/// <param name="classId">The class identifier (CLSID) for the type library.</param>
		/// <param name="interfaceId">The IID for the type library.</param>
		/// <param name="srcTlbPath">The path for the file containing the type library.</param>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="baseUrl">The base URL that contains the virtual root.</param>
		/// <param name="virtualRoot">The name of the virtual root.</param>
		/// <param name="operation">Not used. Specify <see langword="null" /> for this parameter.</param>
		/// <param name="assemblyName">The name of the assembly.</param>
		/// <param name="typeName">The type of the assembly.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		[System.MonoTODO]
		public void DeleteServerTlb(string progId, string classId, string interfaceId, string srcTlbPath, string rootWebServer, string baseUrl, string virtualRoot, string operation, string assemblyName, string typeName)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Publishes authenticated, encrypted SOAP virtual roots on servers. This class cannot be inherited.</summary>
	[Guid("CAA817CC-0C04-4d22-A05C-2B7E162F4E8F")]
	public sealed class SoapServerVRoot : ISoapServerVRoot
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.SoapServerVRoot" /> class.</summary>
		[System.MonoTODO]
		public SoapServerVRoot()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a SOAP virtual root with security options.</summary>
		/// <param name="rootWebServer">The root Web server. The default is "IIS://localhost/W3SVC/1/ROOT".</param>
		/// <param name="inBaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="homePage">The URL of the home page.</param>
		/// <param name="discoFile">
		///   <see langword="true" /> if a default discovery file is to be created; <see langword="false" /> if there is to be no discovery file. If <see langword="false" /> and a Default.disco file exists, the file is deleted.</param>
		/// <param name="secureSockets">
		///   <see langword="true" /> if SSL encryption is required; otherwise, <see langword="false" />.</param>
		/// <param name="authentication">Specify "anonymous" if no authentication is to be used (anonymous user). Otherwise, specify an empty string.</param>
		/// <param name="operation">Not used. Specify <see langword="null" /> for this parameter.</param>
		/// <param name="baseUrl">When this method returns, this parameter contains the base URL.</param>
		/// <param name="virtualRoot">When this method returns, this parameter contains the name of the virtual root.</param>
		/// <param name="physicalPath">When this method returns, this parameter contains the disk address of the Virtual Root directory.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		[System.MonoTODO]
		public void CreateVirtualRootEx(string rootWebServer, string inBaseUrl, string inVirtualRoot, string homePage, string discoFile, string secureSockets, string authentication, string operation, out string baseUrl, out string virtualRoot, out string physicalPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes a virtual root. Not fully implemented.</summary>
		/// <param name="rootWebServer">The root Web server. The default is "IIS://localhost/W3SVC/1/ROOT".</param>
		/// <param name="inBaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to get the system directory failed.</exception>
		[System.MonoTODO]
		public void DeleteVirtualRootEx(string rootWebServer, string inBaseUrl, string inVirtualRoot)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the security status of an existing SOAP virtual root.</summary>
		/// <param name="RootWebServer">The root Web server. The default is "IIS://localhost/W3SVC/1/ROOT".</param>
		/// <param name="inBaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="Exists">When this method returns, this parameter contains a <see langword="true" /> if the virtual directory exists; otherwise, <see langword="false" />.</param>
		/// <param name="SSL">When this method returns, this parameter contains a <see langword="true" /> if SSL encryption is required; otherwise, <see langword="false" />.</param>
		/// <param name="WindowsAuth">When this method returns, this parameter contains <see langword="true" /> if Windows authentication is set, otherwise, <see langword="false" />.</param>
		/// <param name="Anonymous">When this method returns, this parameter contains <see langword="true" /> if no authentication is set (anonymous user); otherwise, <see langword="false" />.</param>
		/// <param name="HomePage">When this method returns, this parameter contains a <see langword="true" /> if the Virtual Root's <see langword="EnableDefaultDoc" /> property is set; otherwise, <see langword="false" />.</param>
		/// <param name="DiscoFile">When this method returns, this parameter contains a <see langword="true" /> if a Default.disco file exists; otherwise, <see langword="false" />.</param>
		/// <param name="PhysicalPath">When this method returns, this parameter contains the disk address of the virtual root directory.</param>
		/// <param name="BaseUrl">When this method returns, this parameter contains the base URL.</param>
		/// <param name="VirtualRoot">When this method returns, this parameter contains the name of the virtual root.</param>
		[System.MonoTODO]
		public void GetVirtualRootStatus(string RootWebServer, string inBaseUrl, string inVirtualRoot, out string Exists, out string SSL, out string WindowsAuth, out string Anonymous, out string HomePage, out string DiscoFile, out string PhysicalPath, out string BaseUrl, out string VirtualRoot)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Provides utilities to support the exporting of COM+ SOAP-enabled application proxies by the server and the importing of the proxies by the client. This class cannot be inherited.</summary>
	[Guid("5F9A955F-AA55-4127-A32B-33496AA8A44E")]
	public sealed class SoapUtility : ISoapUtility
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.SoapUtility" /> class.</summary>
		[System.MonoTODO]
		public SoapUtility()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the path for the SOAP bin directory.</summary>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="inBaseUrl">The base URL address.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="binPath">When this method returns, this parameter contains the file path for the SOAP virtual root bin directory.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to get the system directory failed.</exception>
		[System.MonoTODO]
		public void GetServerBinPath(string rootWebServer, string inBaseUrl, string inVirtualRoot, out string binPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the path for the SOAP virtual root.</summary>
		/// <param name="rootWebServer">The root Web server.</param>
		/// <param name="inBaseUrl">The base URL address.</param>
		/// <param name="inVirtualRoot">The name of the virtual root.</param>
		/// <param name="physicalPath">When this method returns, this parameter contains the file path for the SOAP virtual root.</param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		/// <exception cref="T:System.EnterpriseServices.ServicedComponentException">The call to get the system directory failed.</exception>
		[System.MonoTODO]
		public void GetServerPhysicalPath(string rootWebServer, string inBaseUrl, string inVirtualRoot, out string physicalPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether authenticated, encrypted SOAP interfaces are present.</summary>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The SOAP utility is not available.</exception>
		[System.MonoTODO]
		public void Present()
		{
			throw new NotImplementedException();
		}
	}
}
namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Enables Compensating Resource Manger (CRM) on the tagged application.</summary>
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(false)]
	[ProgId("System.EnterpriseServices.Crm.ApplicationCrmEnabledAttribute")]
	public sealed class ApplicationCrmEnabledAttribute : Attribute
	{
		private bool val;

		/// <summary>Enables or disables Compensating Resource Manager (CRM) on the tagged application.</summary>
		/// <returns>
		///   <see langword="true" /> if CRM is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		public bool Value => val;

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ApplicationCrmEnabledAttribute" /> class, setting the <see cref="P:System.EnterpriseServices.CompensatingResourceManager.ApplicationCrmEnabledAttribute.Value" /> property to <see langword="true" />.</summary>
		public ApplicationCrmEnabledAttribute()
		{
			val = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ApplicationCrmEnabledAttribute" /> class, optionally setting the <see cref="P:System.EnterpriseServices.CompensatingResourceManager.ApplicationCrmEnabledAttribute.Value" /> property to <see langword="false" />.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable Compensating Resource Manager (CRM); otherwise, <see langword="false" />.</param>
		public ApplicationCrmEnabledAttribute(bool val)
		{
			this.val = val;
		}
	}
	/// <summary>Writes records of transactional actions to a log.</summary>
	public sealed class Clerk
	{
		/// <summary>Gets the number of log records.</summary>
		/// <returns>The number of log records.</returns>
		public int LogRecordCount
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value representing the transaction unit of work (UOW).</summary>
		/// <returns>A GUID representing the UOW.</returns>
		public string TransactionUOW
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Clerk" /> class.</summary>
		/// <param name="compensator">The name of the compensator.</param>
		/// <param name="description">The description of the compensator.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.CompensatorOptions" /> values.</param>
		[System.MonoTODO]
		public Clerk(string compensator, string description, CompensatorOptions flags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Clerk" /> class.</summary>
		/// <param name="compensator">A type that represents the compensator.</param>
		/// <param name="description">The description of the compensator.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.CompensatorOptions" /> values.</param>
		[System.MonoTODO]
		public Clerk(Type compensator, string description, CompensatorOptions flags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Frees the resources of the current Clerk before it is reclaimed by the garbage collector.</summary>
		[System.MonoTODO]
		~Clerk()
		{
			throw new NotImplementedException();
		}

		/// <summary>Forces all log records to disk.</summary>
		[System.MonoTODO]
		public void ForceLog()
		{
			throw new NotImplementedException();
		}

		/// <summary>Performs an immediate abort call on the transaction.</summary>
		[System.MonoTODO]
		public void ForceTransactionToAbort()
		{
			throw new NotImplementedException();
		}

		/// <summary>Does not deliver the last log record that was written by this instance of this interface.</summary>
		[System.MonoTODO]
		public void ForgetLogRecord()
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes unstructured log records to the log.</summary>
		/// <param name="record">The log record to write to the log.</param>
		[System.MonoTODO]
		public void WriteLogRecord(object record)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Contains information describing an active Compensating Resource Manager (CRM) Clerk object.</summary>
	public sealed class ClerkInfo
	{
		/// <summary>Gets the activity ID of the current Compensating Resource Manager (CRM) Worker.</summary>
		/// <returns>Gets the activity ID of the current Compensating Resource Manager (CRM) Worker.</returns>
		[System.MonoTODO]
		public string ActivityId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets <see cref="F:System.Runtime.InteropServices.UnmanagedType.IUnknown" /> for the current Clerk.</summary>
		/// <returns>
		///   <see cref="F:System.Runtime.InteropServices.UnmanagedType.IUnknown" /> for the current Clerk.</returns>
		[System.MonoTODO]
		public Clerk Clerk
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the ProgId of the Compensating Resource Manager (CRM) Compensator for the current CRM Clerk.</summary>
		/// <returns>The ProgId of the CRM Compensator for the current CRM Clerk.</returns>
		[System.MonoTODO]
		public string Compensator
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the description of the Compensating Resource Manager (CRM) Compensator for the current CRM Clerk. The description string is the string that was provided by the <see langword="ICrmLogControl::RegisterCompensator" /> method.</summary>
		/// <returns>The description of the CRM Compensator for the current CRM Clerk.</returns>
		[System.MonoTODO]
		public string Description
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the instance class ID (CLSID) of the current Compensating Resource Manager (CRM) Clerk.</summary>
		/// <returns>The instance CLSID of the current CRM Clerk.</returns>
		[System.MonoTODO]
		public string InstanceId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the unit of work (UOW) of the transaction for the current Compensating Resource Manager (CRM) Clerk.</summary>
		/// <returns>The UOW of the transaction for the current CRM Clerk.</returns>
		[System.MonoTODO]
		public string TransactionUOW
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Frees the resources of the current <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkInfo" /> before it is reclaimed by the garbage collector.</summary>
		[System.MonoTODO]
		~ClerkInfo()
		{
			throw new NotImplementedException();
		}

		internal ClerkInfo()
		{
		}
	}
	/// <summary>Contains a snapshot of all Clerks active in the process.</summary>
	public sealed class ClerkMonitor : IEnumerable
	{
		/// <summary>Gets the count of the Clerk monitors in the Compensating Resource Manager (CRM) monitor collection.</summary>
		/// <returns>The number of Clerk monitors in the CRM monitor collection.</returns>
		[System.MonoTODO]
		public int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkInfo" /> object for this <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" />.</summary>
		/// <param name="index">The numeric index that identifies the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" />.</param>
		/// <returns>The <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkInfo" /> object for this <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" />.</returns>
		[System.MonoTODO]
		public ClerkInfo this[string index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkInfo" /> object for this <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" />.</summary>
		/// <param name="index">The integer index that identifies the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" />.</param>
		/// <returns>The <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkInfo" /> object for this <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" />.</returns>
		[System.MonoTODO]
		public ClerkInfo this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Frees the resources of the current <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" /> before it is reclaimed by the garbage collector.</summary>
		[System.MonoTODO]
		~ClerkMonitor()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ClerkMonitor" /> class.</summary>
		[System.MonoTODO]
		public ClerkMonitor()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the enumeration of the clerks in the Compensating Resource Manager (CRM) monitor collection.</summary>
		/// <returns>An enumerator describing the clerks in the collection.</returns>
		[System.MonoTODO]
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the Clerks collection object, which is a snapshot of the current state of the Clerks.</summary>
		[System.MonoTODO]
		public void Populate()
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Represents the base class for all Compensating Resource Manager (CRM) Compensators.</summary>
	public class Compensator : ServicedComponent
	{
		/// <summary>Gets a value representing the Compensating Resource Manager (CRM) <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Clerk" /> object.</summary>
		/// <returns>The <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Clerk" /> object.</returns>
		public Clerk Clerk
		{
			[System.MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Compensator" /> class.</summary>
		[System.MonoTODO]
		public Compensator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Delivers a log record to the Compensating Resource Manager (CRM) Compensator during the abort phase.</summary>
		/// <param name="rec">The log record to be delivered.</param>
		/// <returns>
		///   <see langword="true" /> if the delivered record should be forgotten; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public virtual bool AbortRecord(LogRecord rec)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator of the abort phase of the transaction completion, and the upcoming delivery of records.</summary>
		/// <param name="fRecovery">
		///   <see langword="true" /> to begin abort phase; otherwise, <see langword="false" />.</param>
		[System.MonoTODO]
		public virtual void BeginAbort(bool fRecovery)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator of the commit phase of the transaction completion and the upcoming delivery of records.</summary>
		/// <param name="fRecovery">
		///   <see langword="true" /> to begin commit phase; otherwise, <see langword="false" />.</param>
		[System.MonoTODO]
		public virtual void BeginCommit(bool fRecovery)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator of the prepare phase of the transaction completion and the upcoming delivery of records.</summary>
		[System.MonoTODO]
		public virtual void BeginPrepare()
		{
			throw new NotImplementedException();
		}

		/// <summary>Delivers a log record in forward order during the commit phase.</summary>
		/// <param name="rec">The log record to forward.</param>
		/// <returns>
		///   <see langword="true" /> if the delivered record should be forgotten; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public virtual bool CommitRecord(LogRecord rec)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator that it has received all the log records available during the abort phase.</summary>
		[System.MonoTODO]
		public virtual void EndAbort()
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator that it has delivered all the log records available during the commit phase.</summary>
		[System.MonoTODO]
		public virtual void EndCommit()
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator that it has had all the log records available during the prepare phase.</summary>
		/// <returns>
		///   <see langword="true" /> if successful; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public virtual bool EndPrepare()
		{
			throw new NotImplementedException();
		}

		/// <summary>Delivers a log record in forward order during the prepare phase.</summary>
		/// <param name="rec">The log record to forward.</param>
		/// <returns>
		///   <see langword="true" /> if the delivered record should be forgotten; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public virtual bool PrepareRecord(LogRecord rec)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Specifies flags that control which phases of transaction completion should be received by the Compensating Resource Manager (CRM) Compensator, and whether recovery should fail if questionable transactions remain after recovery has been attempted.</summary>
	[Serializable]
	[Flags]
	public enum CompensatorOptions
	{
		/// <summary>Represents the prepare phase.</summary>
		PreparePhase = 1,
		/// <summary>Represents the commit phase.</summary>
		CommitPhase = 2,
		/// <summary>Represents the abort phase.</summary>
		AbortPhase = 4,
		/// <summary>Represents all phases.</summary>
		AllPhases = 7,
		/// <summary>Fails if in-doubt transactions remain after recovery has been attempted.</summary>
		FailIfInDoubtsRemain = 0x10
	}
	/// <summary>Represents an unstructured log record delivered as a COM+ <see langword="CrmLogRecordRead" /> structure. This class cannot be inherited.</summary>
	public sealed class LogRecord
	{
		private LogRecordFlags flags;

		private object record;

		private int sequence;

		/// <summary>Gets a value that indicates when the log record was written.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.LogRecordFlags" /> values which provides information about when this record was written.</returns>
		public LogRecordFlags Flags => flags;

		/// <summary>Gets the log record user data.</summary>
		/// <returns>A single BLOB that contains the user data.</returns>
		public object Record => record;

		/// <summary>The sequence number of the log record.</summary>
		/// <returns>An integer value that specifies the sequence number of the log record.</returns>
		public int Sequence => sequence;

		[System.MonoTODO]
		internal LogRecord()
		{
		}

		[System.MonoTODO]
		internal LogRecord(_LogRecord logRecord)
		{
			flags = (LogRecordFlags)logRecord.dwCrmFlags;
			sequence = logRecord.dwSequenceNumber;
			record = logRecord.blobUserData;
		}
	}
	internal struct _LogRecord
	{
		public int dwCrmFlags;

		public int dwSequenceNumber;

		public object blobUserData;
	}
	/// <summary>Describes the origin of a Compensating Resource Manager (CRM) log record.</summary>
	[Serializable]
	[Flags]
	public enum LogRecordFlags
	{
		/// <summary>Indicates the delivered record should be forgotten.</summary>
		ForgetTarget = 1,
		/// <summary>Log record was written during prepare.</summary>
		WrittenDuringPrepare = 2,
		/// <summary>Log record was written during commit.</summary>
		WrittenDuringCommit = 4,
		/// <summary>Log record was written during abort.</summary>
		WrittenDuringAbort = 8,
		/// <summary>Log record was written during recovery.</summary>
		WrittenDurringRecovery = 0x10,
		/// <summary>Log record was written during replay.</summary>
		WrittenDuringReplay = 0x20,
		/// <summary>Log record was written when replay was in progress.</summary>
		ReplayInProgress = 0x40
	}
	/// <summary>Specifies the state of the current Compensating Resource Manager (CRM) transaction.</summary>
	[Serializable]
	public enum TransactionState
	{
		/// <summary>The transaction is active.</summary>
		Active,
		/// <summary>The transaction is commited.</summary>
		Committed,
		/// <summary>The transaction is aborted.</summary>
		Aborted,
		/// <summary>The transaction is in-doubt.</summary>
		Indoubt
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
