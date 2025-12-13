using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

[assembly: AssemblyTitle("System.Configuration.Install.dll")]
[assembly: AssemblyDescription("System.Configuration.Install.dll")]
[assembly: AssemblyDefaultAlias("System.Configuration.Install.dll")]
[assembly: AssemblyCompany("MONO development team")]
[assembly: AssemblyProduct("MONO Common language infrastructure")]
[assembly: AssemblyCopyright("(c) various MONO Authors")]
[assembly: SatelliteContractVersion("2.0.0.0")]
[assembly: AssemblyInformationalVersion("2.0.50727.1433")]
[assembly: CLSCompliant(true)]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: ComVisible(false)]
[assembly: AssemblyDelaySign(true)]
[assembly: AssemblyKeyFile("../msfinal.pub")]
[assembly: AssemblyFileVersion("2.0.50727.1433")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: CompilationRelaxations(CompilationRelaxations.NoStringInterning)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: ComCompatibleVersion(1, 0, 3300, 0)]
[assembly: PermissionSet(SecurityAction.RequestMinimum, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SkipVerification\"/>\n</PermissionSet>\n")]
[assembly: AssemblyVersion("2.0.0.0")]
internal sealed class Locale
{
	private Locale()
	{
	}

	public static string GetText(string msg)
	{
		return msg;
	}

	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
internal static class Consts
{
	public const string MonoVersion = "2.6.5.0";

	public const string MonoCompany = "MONO development team";

	public const string MonoProduct = "MONO Common language infrastructure";

	public const string MonoCopyright = "(c) various MONO Authors";

	public const string FxVersion = "2.0.0.0";

	public const string VsVersion = "8.0.0.0";

	public const string FxFileVersion = "2.0.50727.1433";

	public const string VsFileVersion = "8.0.50727.1433";

	public const string AssemblyI18N = "I18N, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMicrosoft_VisualStudio = "Microsoft.VisualStudio, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VisualStudio_Web = "Microsoft.VisualStudio.Web, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VSDesigner = "Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMono_Http = "Mono.Http, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Posix = "Mono.Posix, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Security = "Mono.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Messaging_RabbitMQ = "Mono.Messaging.RabbitMQ, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyCorlib = "mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem = "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Data = "System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Design = "System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_DirectoryServices = "System.DirectoryServices, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Drawing = "System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Drawing_Design = "System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Messaging = "System.Messaging, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Security = "System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_ServiceProcess = "System.ServiceProcess, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Web = "System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Windows_Forms = "System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Core = "System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
}
namespace System.Configuration.Install
{
	public class AssemblyInstaller : Installer
	{
		private Assembly _assembly;

		private string[] _commandLine;

		private bool _useNewContext;

		public Assembly Assembly
		{
			get
			{
				return _assembly;
			}
			set
			{
				_assembly = value;
			}
		}

		public string[] CommandLine
		{
			get
			{
				return _commandLine;
			}
			set
			{
				_commandLine = value;
			}
		}

		public override string HelpText => base.HelpText;

		public string Path
		{
			get
			{
				if ((object)_assembly == null)
				{
					return null;
				}
				return _assembly.Location;
			}
			set
			{
				if (value == null)
				{
					_assembly = null;
				}
				_assembly = Assembly.LoadFrom(value);
			}
		}

		public bool UseNewContext
		{
			get
			{
				return _useNewContext;
			}
			set
			{
				_useNewContext = value;
			}
		}

		public AssemblyInstaller()
		{
		}

		public AssemblyInstaller(Assembly assembly, string[] commandLine)
		{
			_assembly = assembly;
			_commandLine = commandLine;
			_useNewContext = true;
		}

		public AssemblyInstaller(string fileName, string[] commandLine)
		{
			Path = System.IO.Path.GetFullPath(fileName);
			_commandLine = commandLine;
			_useNewContext = true;
		}

		[System.MonoTODO]
		public static void CheckIfInstallable(string assemblyName)
		{
			throw new NotImplementedException();
		}

		public override void Commit(IDictionary savedState)
		{
			base.Commit(savedState);
		}

		public override void Install(IDictionary savedState)
		{
			base.Install(savedState);
		}

		public override void Rollback(IDictionary savedState)
		{
			base.Rollback(savedState);
		}

		public override void Uninstall(IDictionary savedState)
		{
			base.Uninstall(savedState);
		}
	}
	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("1E233FE7-C16D-4512-8C3B-2E9988F08D38")]
	public interface IManagedInstaller
	{
		[return: MarshalAs(UnmanagedType.I4)]
		int ManagedInstall([In][MarshalAs(UnmanagedType.BStr)] string commandLine, [In][MarshalAs(UnmanagedType.I4)] int hInstall);
	}
	[DefaultEvent("AfterInstall")]
	public class Installer : Component
	{
		private InstallContext context;

		private string helptext;

		private InstallerCollection installers;

		internal Installer parent;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public InstallContext Context
		{
			get
			{
				return context;
			}
			set
			{
				context = value;
			}
		}

		public virtual string HelpText => helptext;

		public InstallerCollection Installers
		{
			get
			{
				if (installers == null)
				{
					installers = new InstallerCollection(this);
				}
				return installers;
			}
		}

		public Installer Parent
		{
			get
			{
				return parent;
			}
			set
			{
				parent = value;
			}
		}

		public event InstallEventHandler AfterInstall;

		public event InstallEventHandler AfterRollback;

		public event InstallEventHandler AfterUninstall;

		public event InstallEventHandler BeforeInstall;

		public event InstallEventHandler BeforeRollback;

		public event InstallEventHandler BeforeUninstall;

		public event InstallEventHandler Committed;

		public event InstallEventHandler Committing;

		public virtual void Commit(IDictionary savedState)
		{
		}

		public virtual void Install(IDictionary stateSaver)
		{
		}

		protected virtual void OnAfterInstall(IDictionary savedState)
		{
			if (this.AfterInstall != null)
			{
				this.AfterInstall(this, new InstallEventArgs(savedState));
			}
		}

		protected virtual void OnAfterRollback(IDictionary savedState)
		{
			if (this.AfterRollback != null)
			{
				this.AfterRollback(this, new InstallEventArgs(savedState));
			}
		}

		protected virtual void OnAfterUninstall(IDictionary savedState)
		{
			if (this.AfterUninstall != null)
			{
				this.AfterUninstall(this, new InstallEventArgs(savedState));
			}
		}

		protected virtual void OnBeforeInstall(IDictionary savedState)
		{
			if (this.BeforeInstall != null)
			{
				this.BeforeInstall(this, new InstallEventArgs(savedState));
			}
		}

		protected virtual void OnBeforeRollback(IDictionary savedState)
		{
			if (this.BeforeRollback != null)
			{
				this.BeforeRollback(this, new InstallEventArgs(savedState));
			}
		}

		protected virtual void OnBeforeUninstall(IDictionary savedState)
		{
			if (this.BeforeUninstall != null)
			{
				this.BeforeUninstall(this, new InstallEventArgs(savedState));
			}
		}

		protected virtual void OnCommitted(IDictionary savedState)
		{
			if (this.Committed != null)
			{
				this.Committed(this, new InstallEventArgs(savedState));
			}
		}

		protected virtual void OnCommitting(IDictionary savedState)
		{
			if (this.Committing != null)
			{
				this.Committing(this, new InstallEventArgs(savedState));
			}
		}

		public virtual void Rollback(IDictionary savedState)
		{
		}

		public virtual void Uninstall(IDictionary savedState)
		{
		}
	}
	[Serializable]
	public class InstallException : SystemException
	{
		private Exception innerException;

		public InstallException()
		{
		}

		public InstallException(string message)
			: base(message)
		{
		}

		protected InstallException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public InstallException(string message, Exception innerException)
			: base(message)
		{
			this.innerException = innerException;
		}
	}
	public abstract class ComponentInstaller : Installer
	{
		public abstract void CopyFromComponent(IComponent component);

		[System.MonoTODO("Mono always returns false")]
		public virtual bool IsEquivalentInstaller(ComponentInstaller otherInstaller)
		{
			return false;
		}
	}
	public class InstallContext
	{
		private StringDictionary parameters;

		private string log_file;

		private bool log;

		public StringDictionary Parameters => parameters;

		public InstallContext()
		{
			log_file = null;
			log = false;
			parameters = ParseCommandLine(new string[0]);
		}

		public InstallContext(string logFilePath, string[] commandLine)
		{
			log_file = logFilePath;
			parameters = ParseCommandLine(commandLine);
			log = IsParameterTrue("LogtoConsole");
		}

		public bool IsParameterTrue(string paramName)
		{
			return parameters[paramName] == "true";
		}

		public void LogMessage(string message)
		{
			if (log)
			{
				Console.WriteLine(message);
			}
		}

		protected static StringDictionary ParseCommandLine(string[] args)
		{
			StringDictionary stringDictionary = new StringDictionary();
			foreach (string text in args)
			{
				int num = text.IndexOf("=");
				if (num == -1)
				{
					stringDictionary[text] = "true";
					continue;
				}
				string key = text.Substring(0, num);
				string text2 = text.Substring(num + 1).ToLower();
				switch (text2)
				{
				case "yes":
				case "true":
				case "1":
					text2 = "true";
					break;
				}
				stringDictionary[key] = text2;
			}
			return stringDictionary;
		}
	}
	public class InstallEventArgs : EventArgs
	{
		private IDictionary savedstate;

		public IDictionary SavedState => savedstate;

		public InstallEventArgs()
		{
		}

		public InstallEventArgs(IDictionary savedState)
		{
			savedstate = savedState;
		}
	}
	public class InstallerCollection : CollectionBase
	{
		private Installer owner;

		public Installer this[int index]
		{
			get
			{
				return (Installer)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		internal InstallerCollection(Installer owner)
		{
			this.owner = owner;
		}

		public int Add(Installer value)
		{
			return base.List.Add(value);
		}

		public void AddRange(Installer[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				Add(value[i]);
			}
		}

		public void AddRange(InstallerCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				Add(value[i]);
			}
		}

		public bool Contains(Installer value)
		{
			return base.List.Contains(value);
		}

		public void CopyTo(Installer[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		public int IndexOf(Installer value)
		{
			return base.List.IndexOf(value);
		}

		public void Insert(int index, Installer value)
		{
			base.List.Insert(index, value);
		}

		protected override void OnInsert(int index, object value)
		{
			((Installer)value).parent = owner;
		}

		protected override void OnRemove(int index, object value)
		{
			((Installer)value).parent = null;
		}

		protected override void OnSet(int index, object oldValue, object newValue)
		{
			((Installer)oldValue).parent = null;
			((Installer)newValue).parent = owner;
		}

		public void Remove(Installer value)
		{
			base.List.Remove(value);
		}
	}
	[Guid("42EB0342-0393-448f-84AA-D4BEB0283595")]
	[ComVisible(true)]
	public class ManagedInstallerClass : IManagedInstaller
	{
		[System.MonoTODO]
		int IManagedInstaller.ManagedInstall(string argString, int hInstall)
		{
			throw new NotImplementedException();
		}

		[System.MonoTODO]
		public static void InstallHelper(string[] args)
		{
			throw new NotImplementedException();
		}
	}
	public class TransactedInstaller : Installer
	{
		public override void Install(IDictionary savedState)
		{
			base.Install(savedState);
		}

		public override void Uninstall(IDictionary savedState)
		{
			base.Uninstall(savedState);
		}
	}
	public enum UninstallAction
	{
		NoAction = 1,
		Remove = 0
	}
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
namespace System.Diagnostics
{
	public class EventLogInstaller : ComponentInstaller
	{
		private string _log;

		private string _source;

		private UninstallAction _uninstallAction;

		private string _categoryResourceFile;

		private string _messageResourceFile;

		private string _parameterResourceFile;

		[ComVisible(false)]
		[System.MonoTODO]
		public int CategoryCount
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

		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Editor("System.Windows.Forms.Design.FileNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[System.MonoTODO]
		[ComVisible(false)]
		public string CategoryResourceFile
		{
			get
			{
				return _categoryResourceFile;
			}
			set
			{
				_categoryResourceFile = value;
			}
		}

		[System.MonoTODO]
		[Editor("System.Windows.Forms.Design.FileNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ComVisible(false)]
		public string MessageResourceFile
		{
			get
			{
				return _messageResourceFile;
			}
			set
			{
				_messageResourceFile = value;
			}
		}

		[System.MonoTODO]
		[ComVisible(false)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Editor("System.Windows.Forms.Design.FileNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ParameterResourceFile
		{
			get
			{
				return _parameterResourceFile;
			}
			set
			{
				_parameterResourceFile = value;
			}
		}

		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Log
		{
			get
			{
				if (_log == null && _source != null)
				{
					_log = EventLog.LogNameFromSourceName(_source, ".");
				}
				return _log;
			}
			set
			{
				_log = value;
			}
		}

		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Source
		{
			get
			{
				return _source;
			}
			set
			{
				_source = value;
			}
		}

		[DefaultValue(UninstallAction.Remove)]
		public UninstallAction UninstallAction
		{
			get
			{
				return _uninstallAction;
			}
			set
			{
				if (!Enum.IsDefined(typeof(UninstallAction), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(UninstallAction));
				}
				_uninstallAction = value;
			}
		}

		[System.MonoTODO]
		public override void CopyFromComponent(IComponent component)
		{
			throw new NotImplementedException();
		}

		[System.MonoTODO]
		public override void Install(IDictionary stateSaver)
		{
			throw new NotImplementedException();
		}

		[System.MonoTODO]
		public override bool IsEquivalentInstaller(ComponentInstaller otherInstaller)
		{
			throw new NotImplementedException();
		}

		[System.MonoTODO]
		public override void Rollback(IDictionary savedState)
		{
			throw new NotImplementedException();
		}

		[System.MonoTODO]
		public override void Uninstall(IDictionary savedState)
		{
			throw new NotImplementedException();
		}
	}
	public class PerformanceCounterInstaller : ComponentInstaller
	{
		private string _categoryHelp = string.Empty;

		private string _categoryName = string.Empty;

		private CounterCreationDataCollection _counters = new CounterCreationDataCollection();

		private UninstallAction _uninstallAction;

		private PerformanceCounterCategoryType _categoryType;

		[DefaultValue("")]
		public string CategoryHelp
		{
			get
			{
				return _categoryHelp;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				_categoryHelp = value;
			}
		}

		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string CategoryName
		{
			get
			{
				return _categoryName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				_categoryName = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CounterCreationDataCollection Counters => _counters;

		[DefaultValue(UninstallAction.Remove)]
		public UninstallAction UninstallAction
		{
			get
			{
				return _uninstallAction;
			}
			set
			{
				if (!Enum.IsDefined(typeof(UninstallAction), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(UninstallAction));
				}
				_uninstallAction = value;
			}
		}

		[DefaultValue(PerformanceCounterCategoryType.Unknown)]
		[ComVisible(false)]
		public PerformanceCounterCategoryType CategoryType
		{
			get
			{
				return _categoryType;
			}
			set
			{
				if (!Enum.IsDefined(typeof(PerformanceCounterCategoryType), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(PerformanceCounterCategoryType));
				}
				_categoryType = value;
			}
		}

		[System.MonoTODO]
		public override void CopyFromComponent(IComponent component)
		{
			throw new NotImplementedException();
		}

		[System.MonoTODO]
		public override void Install(IDictionary stateSaver)
		{
			throw new NotImplementedException();
		}

		[System.MonoTODO]
		public override void Rollback(IDictionary savedState)
		{
			throw new NotImplementedException();
		}

		[System.MonoTODO]
		public override void Uninstall(IDictionary savedState)
		{
			throw new NotImplementedException();
		}
	}
}
namespace System.Configuration.Install
{
	public delegate void InstallEventHandler(object sender, InstallEventArgs e);
}
