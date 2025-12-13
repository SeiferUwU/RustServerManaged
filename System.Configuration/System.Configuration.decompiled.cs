using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration.Internal;
using System.Configuration.Provider;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Unity;

[assembly: ComCompatibleVersion(1, 0, 3300, 0)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("System.Configuration.dll")]
[assembly: AssemblyDescription("System.Configuration.dll")]
[assembly: AssemblyDefaultAlias("System.Configuration.dll")]
[assembly: AssemblyCompany("Mono development team")]
[assembly: AssemblyProduct("Mono Common Language Infrastructure")]
[assembly: CompilationRelaxations(8)]
[assembly: AssemblyCopyright("(c) Various Mono authors")]
[assembly: AssemblyInformationalVersion("4.6.57.0")]
[assembly: CLSCompliant(true)]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: ComVisible(false)]
[assembly: AllowPartiallyTrustedCallers]
[assembly: AssemblyDelaySign(true)]
[assembly: InternalsVisibleTo("System.Web, PublicKey=002400000480000094000000060200000024000052534131000400000100010007d1fa57c4aed9f0a32e84aa0faefd0de9e8fd6aec8f87fb03766c834c99921eb23be79ad9d5dcc1dd9ad236132102900b723cf980957fc4e177108fc607774f29e8320e92ea05ece4e821c0a5efe8f1645c4c0c93c1ab99285d622caa652c1dfad63d745d6f2de5f17e5eaf0fc4963d261c8a12436518206dc093344d5ad293")]
[assembly: SatelliteContractVersion("4.0.0.0")]
[assembly: AssemblyFileVersion("4.6.57.0")]
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
internal class ConfigXmlTextReader : XmlTextReader, IConfigErrorInfo
{
	private readonly string fileName;

	public string Filename => fileName;

	public ConfigXmlTextReader(Stream s, string fileName)
		: base(s)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		this.fileName = fileName;
	}

	public ConfigXmlTextReader(TextReader input, string fileName)
		: base(input)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		this.fileName = fileName;
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
namespace System.Configuration
{
	/// <summary>Provides configuration system support for the <see langword="appSettings" /> configuration section. This class cannot be inherited.</summary>
	public sealed class AppSettingsSection : ConfigurationSection
	{
		private static ConfigurationPropertyCollection _properties;

		private static readonly ConfigurationProperty _propFile;

		private static readonly ConfigurationProperty _propSettings;

		/// <summary>Gets or sets a configuration file that provides additional settings or overrides the settings specified in the <see langword="appSettings" /> element.</summary>
		/// <returns>A configuration file that provides additional settings or overrides the settings specified in the <see langword="appSettings" /> element.</returns>
		[ConfigurationProperty("file", DefaultValue = "")]
		public string File
		{
			get
			{
				return (string)base[_propFile];
			}
			set
			{
				base[_propFile] = value;
			}
		}

		/// <summary>Gets a collection of key/value pairs that contains application settings.</summary>
		/// <returns>A collection of key/value pairs that contains the application settings from the configuration file.</returns>
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public KeyValueConfigurationCollection Settings => (KeyValueConfigurationCollection)base[_propSettings];

		protected internal override ConfigurationPropertyCollection Properties => _properties;

		static AppSettingsSection()
		{
			_propFile = new ConfigurationProperty("file", typeof(string), "", new StringConverter(), null, ConfigurationPropertyOptions.None);
			_propSettings = new ConfigurationProperty("", typeof(KeyValueConfigurationCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);
			_properties = new ConfigurationPropertyCollection();
			_properties.Add(_propFile);
			_properties.Add(_propSettings);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.AppSettingsSection" /> class.</summary>
		public AppSettingsSection()
		{
		}

		protected internal override bool IsModified()
		{
			return Settings.IsModified();
		}

		[System.MonoInternalNote("file path?  do we use a System.Configuration api for opening it?  do we keep it open?  do we open it writable?")]
		protected internal override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
			if (!(File != ""))
			{
				return;
			}
			try
			{
				string text = File;
				if (!Path.IsPathRooted(text))
				{
					text = Path.Combine(Path.GetDirectoryName(base.Configuration.FilePath), text);
				}
				FileStream fileStream = System.IO.File.OpenRead(text);
				XmlReader reader2 = new ConfigXmlTextReader(fileStream, text);
				base.DeserializeElement(reader2, serializeCollectionKey);
				fileStream.Close();
			}
			catch
			{
			}
		}

		protected internal override void Reset(ConfigurationElement parentSection)
		{
			if (parentSection is AppSettingsSection appSettingsSection)
			{
				Settings.Reset(appSettingsSection.Settings);
			}
		}

		[System.MonoTODO]
		protected internal override string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
		{
			if (File == "")
			{
				return base.SerializeSection(parentElement, name, saveMode);
			}
			throw new NotImplementedException();
		}

		protected internal override object GetRuntimeObject()
		{
			KeyValueInternalCollection keyValueInternalCollection = new KeyValueInternalCollection();
			string[] allKeys = Settings.AllKeys;
			foreach (string key in allKeys)
			{
				KeyValueConfigurationElement keyValueConfigurationElement = Settings[key];
				keyValueInternalCollection.Add(keyValueConfigurationElement.Key, keyValueConfigurationElement.Value);
			}
			if (!ConfigurationManager.ConfigurationSystem.SupportsUserConfig)
			{
				keyValueInternalCollection.SetReadOnly();
			}
			return keyValueInternalCollection;
		}
	}
	/// <summary>Provides dynamic validation of an object.</summary>
	public sealed class CallbackValidator : ConfigurationValidatorBase
	{
		private Type type;

		private ValidatorCallback callback;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.CallbackValidator" /> class.</summary>
		/// <param name="type">The type of object that will be validated.</param>
		/// <param name="callback">The <see cref="T:System.Configuration.ValidatorCallback" /> used as the delegate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		public CallbackValidator(Type type, ValidatorCallback callback)
		{
			this.type = type;
			this.callback = callback;
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <param name="type">The type of object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see langword="type" /> parameter matches the type used as the first parameter when creating an instance of <see cref="T:System.Configuration.CallbackValidator" />; otherwise, <see langword="false" />.</returns>
		public override bool CanValidate(Type type)
		{
			return type == this.type;
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		public override void Validate(object value)
		{
			callback(value);
		}
	}
	/// <summary>Specifies a <see cref="T:System.Configuration.CallbackValidator" /> object to use for code validation. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class CallbackValidatorAttribute : ConfigurationValidatorAttribute
	{
		private string callbackMethodName = "";

		private Type type;

		private ConfigurationValidatorBase instance;

		/// <summary>Gets or sets the name of the callback method.</summary>
		/// <returns>The name of the method to call.</returns>
		public string CallbackMethodName
		{
			get
			{
				return callbackMethodName;
			}
			set
			{
				callbackMethodName = value;
				instance = null;
			}
		}

		/// <summary>Gets or sets the type of the validator.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the current validator attribute instance.</returns>
		public Type Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
				instance = null;
			}
		}

		/// <summary>Gets the validator instance.</summary>
		/// <returns>The current <see cref="T:System.Configuration.ConfigurationValidatorBase" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <see cref="P:System.Configuration.CallbackValidatorAttribute.Type" /> property is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Configuration.CallbackValidatorAttribute.CallbackMethodName" /> property is not set to a public static void method with one object parameter.</exception>
		public override ConfigurationValidatorBase ValidatorInstance => instance;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.CallbackValidatorAttribute" /> class.</summary>
		public CallbackValidatorAttribute()
		{
		}
	}
	internal class ClientConfigurationSystem : IInternalConfigSystem
	{
		private Configuration cfg;

		private Configuration Configuration
		{
			get
			{
				if (cfg == null)
				{
					Assembly entryAssembly = Assembly.GetEntryAssembly();
					try
					{
						cfg = ConfigurationManager.OpenExeConfigurationInternal(ConfigurationUserLevel.None, entryAssembly, null);
					}
					catch (Exception inner)
					{
						throw new ConfigurationErrorsException("Error Initializing the configuration system.", inner);
					}
				}
				return cfg;
			}
		}

		bool IInternalConfigSystem.SupportsUserConfig => false;

		object IInternalConfigSystem.GetSection(string configKey)
		{
			return Configuration.GetSection(configKey)?.GetRuntimeObject();
		}

		void IInternalConfigSystem.RefreshConfig(string sectionName)
		{
		}
	}
	/// <summary>Represents a collection of string elements separated by commas. This class cannot be inherited.</summary>
	public sealed class CommaDelimitedStringCollection : StringCollection
	{
		private bool modified;

		private bool readOnly;

		private int originalStringHash;

		/// <summary>Gets a value that specifies whether the collection has been modified.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> has been modified; otherwise, <see langword="false" />.</returns>
		public bool IsModified
		{
			get
			{
				if (modified)
				{
					return true;
				}
				string text = ToString();
				if (text == null)
				{
					return false;
				}
				return text.GetHashCode() != originalStringHash;
			}
		}

		/// <summary>Gets a value indicating whether the collection object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the specified string element in the <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> is read-only; otherwise, <see langword="false" />.</returns>
		public new bool IsReadOnly => readOnly;

		/// <summary>Gets or sets a string element in the collection based on the index.</summary>
		/// <param name="index">The index of the string element in the collection.</param>
		/// <returns>A string element in the collection.</returns>
		public new string this[int index]
		{
			get
			{
				return base[index];
			}
			set
			{
				if (readOnly)
				{
					throw new ConfigurationErrorsException("The configuration is read only");
				}
				base[index] = value;
				modified = true;
			}
		}

		/// <summary>Adds a string to the comma-delimited collection.</summary>
		/// <param name="value">A string value.</param>
		public new void Add(string value)
		{
			if (readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.Add(value);
			modified = true;
		}

		/// <summary>Adds all the strings in a string array to the collection.</summary>
		/// <param name="range">An array of strings to add to the collection.</param>
		public new void AddRange(string[] range)
		{
			if (readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.AddRange(range);
			modified = true;
		}

		/// <summary>Clears the collection.</summary>
		public new void Clear()
		{
			if (readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.Clear();
			modified = true;
		}

		/// <summary>Creates a copy of the collection.</summary>
		/// <returns>A copy of the <see cref="T:System.Configuration.CommaDelimitedStringCollection" />.</returns>
		public CommaDelimitedStringCollection Clone()
		{
			CommaDelimitedStringCollection commaDelimitedStringCollection = new CommaDelimitedStringCollection();
			string[] array = new string[base.Count];
			CopyTo(array, 0);
			commaDelimitedStringCollection.AddRange(array);
			commaDelimitedStringCollection.originalStringHash = originalStringHash;
			return commaDelimitedStringCollection;
		}

		/// <summary>Adds a string element to the collection at the specified index.</summary>
		/// <param name="index">The index in the collection at which the new element will be added.</param>
		/// <param name="value">The value of the new element to add to the collection.</param>
		public new void Insert(int index, string value)
		{
			if (readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.Insert(index, value);
			modified = true;
		}

		/// <summary>Removes a string element from the collection.</summary>
		/// <param name="value">The string to remove.</param>
		public new void Remove(string value)
		{
			if (readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.Remove(value);
			modified = true;
		}

		/// <summary>Sets the collection object to read-only.</summary>
		public void SetReadOnly()
		{
			readOnly = true;
		}

		/// <summary>Returns a string representation of the object.</summary>
		/// <returns>A string representation of the object.</returns>
		public override string ToString()
		{
			if (base.Count == 0)
			{
				return null;
			}
			string[] array = new string[base.Count];
			CopyTo(array, 0);
			return string.Join(",", array);
		}

		internal void UpdateStringHash()
		{
			string text = ToString();
			if (text == null)
			{
				originalStringHash = 0;
			}
			else
			{
				originalStringHash = text.GetHashCode();
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> class.</summary>
		public CommaDelimitedStringCollection()
		{
		}
	}
	/// <summary>Converts a comma-delimited string value to and from a <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> object. This class cannot be inherited.</summary>
	public sealed class CommaDelimitedStringCollectionConverter : ConfigurationConverterBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.CommaDelimitedStringCollectionConverter" /> class.</summary>
		public CommaDelimitedStringCollectionConverter()
		{
		}

		/// <summary>Converts a <see cref="T:System.String" /> object to a <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> object.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> used during conversion.</param>
		/// <param name="data">The comma-separated <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> containing the converted value.</returns>
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			CommaDelimitedStringCollection commaDelimitedStringCollection = new CommaDelimitedStringCollection();
			string[] array = ((string)data).Split(',');
			foreach (string text in array)
			{
				commaDelimitedStringCollection.Add(text.Trim());
			}
			commaDelimitedStringCollection.UpdateStringHash();
			return commaDelimitedStringCollection;
		}

		/// <summary>Converts a <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> object to a <see cref="T:System.String" /> object.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> used during conversion.</param>
		/// <param name="value">The value to convert.</param>
		/// <param name="type">The conversion type.</param>
		/// <returns>The <see cref="T:System.String" /> representing the converted <paramref name="value" /> parameter, which is a <see cref="T:System.Configuration.CommaDelimitedStringCollection" />.</returns>
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value == null)
			{
				return null;
			}
			if (!typeof(StringCollection).IsAssignableFrom(value.GetType()))
			{
				throw new ArgumentException();
			}
			return value.ToString();
		}
	}
	internal class ConfigNameValueCollection : NameValueCollection
	{
		private bool modified;

		public bool IsModified => modified;

		public ConfigNameValueCollection()
		{
		}

		public ConfigNameValueCollection(System.Configuration.ConfigNameValueCollection col)
			: base(col.Count, col)
		{
		}

		public void ResetModified()
		{
			modified = false;
		}

		public override void Set(string name, string value)
		{
			base.Set(name, value);
			modified = true;
		}
	}
	internal abstract class ConfigInfo
	{
		public string Name;

		public string TypeName;

		protected Type Type;

		private string streamName;

		public ConfigInfo Parent;

		public IInternalConfigHost ConfigHost;

		public string XPath
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(Name);
				for (ConfigInfo parent = Parent; parent != null; parent = parent.Parent)
				{
					stringBuilder.Insert(0, parent.Name + "/");
				}
				return stringBuilder.ToString();
			}
		}

		public string StreamName
		{
			get
			{
				return streamName;
			}
			set
			{
				streamName = value;
			}
		}

		public virtual object CreateInstance()
		{
			if (Type == null)
			{
				Type = ConfigHost.GetConfigType(TypeName, throwOnError: true);
			}
			return Activator.CreateInstance(Type, nonPublic: true);
		}

		public abstract bool HasConfigContent(Configuration cfg);

		public abstract bool HasDataContent(Configuration cfg);

		protected void ThrowException(string text, XmlReader reader)
		{
			throw new ConfigurationErrorsException(text, reader);
		}

		public abstract void ReadConfig(Configuration cfg, string streamName, XmlReader reader);

		public abstract void WriteConfig(Configuration cfg, XmlWriter writer, ConfigurationSaveMode mode);

		public abstract void ReadData(Configuration config, XmlReader reader, bool overrideAllowed);

		public abstract void WriteData(Configuration config, XmlWriter writer, ConfigurationSaveMode mode);

		internal abstract void Merge(ConfigInfo data);

		internal abstract bool HasValues(Configuration config, ConfigurationSaveMode mode);

		internal abstract void ResetModified(Configuration config);
	}
	internal class ConfigurationXmlDocument : XmlDocument
	{
		public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			if (namespaceURI == "http://schemas.microsoft.com/.NetConfiguration/v2.0")
			{
				return base.CreateElement(string.Empty, localName, string.Empty);
			}
			return base.CreateElement(prefix, localName, namespaceURI);
		}
	}
	/// <summary>Represents a configuration file that is applicable to a particular computer, application, or resource. This class cannot be inherited.</summary>
	public sealed class Configuration
	{
		private Configuration parent;

		private Hashtable elementData;

		private string streamName;

		private ConfigurationSectionGroup rootSectionGroup;

		private ConfigurationLocationCollection locations;

		private SectionGroupInfo rootGroup;

		private IConfigSystem system;

		private bool hasFile;

		private string rootNamespace;

		private string configPath;

		private string locationConfigPath;

		private string locationSubPath;

		private ContextInformation evaluationContext;

		internal Configuration Parent
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

		internal string FileName => streamName;

		internal IInternalConfigHost ConfigHost => system.Host;

		internal string LocationConfigPath => locationConfigPath;

		internal string ConfigPath => configPath;

		/// <summary>Gets the <see cref="T:System.Configuration.AppSettingsSection" /> object configuration section that applies to this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>An <see cref="T:System.Configuration.AppSettingsSection" /> object representing the <see langword="appSettings" /> configuration section that applies to this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		public AppSettingsSection AppSettings => (AppSettingsSection)GetSection("appSettings");

		/// <summary>Gets a <see cref="T:System.Configuration.ConnectionStringsSection" /> configuration-section object that applies to this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConnectionStringsSection" /> configuration-section object representing the <see langword="connectionStrings" /> configuration section that applies to this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		public ConnectionStringsSection ConnectionStrings => (ConnectionStringsSection)GetSection("connectionStrings");

		/// <summary>Gets the physical path to the configuration file represented by this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>The physical path to the configuration file represented by this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		public string FilePath
		{
			get
			{
				if (streamName == null && parent != null)
				{
					return parent.FilePath;
				}
				return streamName;
			}
		}

		/// <summary>Gets a value that indicates whether a file exists for the resource represented by this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> if there is a configuration file; otherwise, <see langword="false" />.</returns>
		public bool HasFile => hasFile;

		/// <summary>Gets the <see cref="T:System.Configuration.ContextInformation" /> object for the <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ContextInformation" /> object for the <see cref="T:System.Configuration.Configuration" /> object.</returns>
		public ContextInformation EvaluationContext
		{
			get
			{
				if (evaluationContext == null)
				{
					object ctx = system.Host.CreateConfigurationContext(configPath, GetLocationSubPath());
					evaluationContext = new ContextInformation(this, ctx);
				}
				return evaluationContext;
			}
		}

		/// <summary>Gets the locations defined within this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationLocationCollection" /> containing the locations defined within this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		public ConfigurationLocationCollection Locations
		{
			get
			{
				if (locations == null)
				{
					locations = new ConfigurationLocationCollection();
				}
				return locations;
			}
		}

		/// <summary>Gets or sets a value indicating whether the configuration file has an XML namespace.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration file has an XML namespace; otherwise, <see langword="false" />.</returns>
		public bool NamespaceDeclared
		{
			get
			{
				return rootNamespace != null;
			}
			set
			{
				rootNamespace = (value ? "http://schemas.microsoft.com/.NetConfiguration/v2.0" : null);
			}
		}

		/// <summary>Gets the root <see cref="T:System.Configuration.ConfigurationSectionGroup" /> for this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>The root section group for this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		public ConfigurationSectionGroup RootSectionGroup
		{
			get
			{
				if (rootSectionGroup == null)
				{
					rootSectionGroup = new ConfigurationSectionGroup();
					rootSectionGroup.Initialize(this, rootGroup);
				}
				return rootSectionGroup;
			}
		}

		/// <summary>Gets a collection of the section groups defined by this configuration.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> collection representing the collection of section groups for this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		public ConfigurationSectionGroupCollection SectionGroups => RootSectionGroup.SectionGroups;

		/// <summary>Gets a collection of the sections defined by this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A collection of the sections defined by this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		public ConfigurationSectionCollection Sections => RootSectionGroup.Sections;

		/// <summary>Specifies a function delegate that is used to transform assembly strings in configuration files.</summary>
		/// <returns>A delegate that transforms type strings. The default value is <see langword="null" />.</returns>
		public Func<string, string> AssemblyStringTransformer
		{
			get
			{
				//IL_0007: Expected O, but got I4
				Unity.ThrowStub.ThrowNotSupportedException();
				return (Func<string, string>)0;
			}
			[ConfigurationPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Specifies the targeted version of the .NET Framework when a version earlier than the current one is targeted.</summary>
		/// <returns>The name of the targeted version of the .NET Framework. The default value is <see langword="null" />, which indicates that the current version is targeted.</returns>
		public FrameworkName TargetFramework
		{
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[ConfigurationPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Specifies a function delegate that is used to transform type strings in configuration files.</summary>
		/// <returns>A delegate that transforms type strings. The default value is <see langword="null" />.</returns>
		public Func<string, string> TypeStringTransformer
		{
			get
			{
				//IL_0007: Expected O, but got I4
				Unity.ThrowStub.ThrowNotSupportedException();
				return (Func<string, string>)0;
			}
			[ConfigurationPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		internal static event ConfigurationSaveEventHandler SaveStart;

		internal static event ConfigurationSaveEventHandler SaveEnd;

		internal Configuration(Configuration parent, string locationSubPath)
		{
			elementData = new Hashtable();
			base..ctor();
			this.parent = parent;
			system = parent.system;
			rootGroup = parent.rootGroup;
			this.locationSubPath = locationSubPath;
			configPath = parent.ConfigPath;
		}

		internal Configuration(InternalConfigurationSystem system, string locationSubPath)
		{
			elementData = new Hashtable();
			base..ctor();
			hasFile = true;
			this.system = system;
			system.InitForConfiguration(ref locationSubPath, out configPath, out locationConfigPath);
			Configuration configuration = null;
			if (locationSubPath != null)
			{
				configuration = new Configuration(system, locationSubPath);
				if (locationConfigPath != null)
				{
					configuration = configuration.FindLocationConfiguration(locationConfigPath, configuration);
				}
			}
			Init(system, configPath, configuration);
		}

		internal Configuration FindLocationConfiguration(string relativePath, Configuration defaultConfiguration)
		{
			Configuration configuration = defaultConfiguration;
			if (!string.IsNullOrEmpty(LocationConfigPath))
			{
				Configuration parentWithFile = GetParentWithFile();
				if (parentWithFile != null)
				{
					string configPathFromLocationSubPath = system.Host.GetConfigPathFromLocationSubPath(configPath, relativePath);
					configuration = parentWithFile.FindLocationConfiguration(configPathFromLocationSubPath, defaultConfiguration);
				}
			}
			string text = configPath.Substring(1) + "/";
			if (relativePath.StartsWith(text, StringComparison.Ordinal))
			{
				relativePath = relativePath.Substring(text.Length);
			}
			ConfigurationLocation configurationLocation = Locations.FindBest(relativePath);
			if (configurationLocation == null)
			{
				return configuration;
			}
			configurationLocation.SetParentConfiguration(configuration);
			return configurationLocation.OpenConfiguration();
		}

		internal void Init(IConfigSystem system, string configPath, Configuration parent)
		{
			this.system = system;
			this.configPath = configPath;
			streamName = system.Host.GetStreamName(configPath);
			this.parent = parent;
			if (parent != null)
			{
				rootGroup = parent.rootGroup;
			}
			else
			{
				rootGroup = new SectionGroupInfo();
				rootGroup.StreamName = streamName;
			}
			try
			{
				if (streamName != null)
				{
					Load();
				}
			}
			catch (XmlException ex)
			{
				throw new ConfigurationErrorsException(ex.Message, ex, streamName, 0);
			}
		}

		internal Configuration GetParentWithFile()
		{
			Configuration configuration = Parent;
			while (configuration != null && !configuration.HasFile)
			{
				configuration = configuration.Parent;
			}
			return configuration;
		}

		internal string GetLocationSubPath()
		{
			Configuration configuration = parent;
			string text = null;
			while (configuration != null)
			{
				text = configuration.locationSubPath;
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				configuration = configuration.parent;
			}
			return text;
		}

		/// <summary>Returns the specified <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
		/// <param name="sectionName">The path to the section to be returned.</param>
		/// <returns>The specified <see cref="T:System.Configuration.ConfigurationSection" /> object.</returns>
		public ConfigurationSection GetSection(string sectionName)
		{
			string[] array = sectionName.Split('/');
			if (array.Length == 1)
			{
				return Sections[array[0]];
			}
			ConfigurationSectionGroup configurationSectionGroup = SectionGroups[array[0]];
			int num = 1;
			while (configurationSectionGroup != null && num < array.Length - 1)
			{
				configurationSectionGroup = configurationSectionGroup.SectionGroups[array[num]];
				num++;
			}
			return configurationSectionGroup?.Sections[array[^1]];
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <param name="sectionGroupName">The path name of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> to return.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> specified.</returns>
		public ConfigurationSectionGroup GetSectionGroup(string sectionGroupName)
		{
			string[] array = sectionGroupName.Split('/');
			ConfigurationSectionGroup configurationSectionGroup = SectionGroups[array[0]];
			int num = 1;
			while (configurationSectionGroup != null && num < array.Length)
			{
				configurationSectionGroup = configurationSectionGroup.SectionGroups[array[num]];
				num++;
			}
			return configurationSectionGroup;
		}

		internal ConfigurationSection GetSectionInstance(SectionInfo config, bool createDefaultInstance)
		{
			object obj = elementData[config];
			ConfigurationSection configurationSection = obj as ConfigurationSection;
			if (configurationSection != null || !createDefaultInstance)
			{
				return configurationSection;
			}
			object obj2 = config.CreateInstance();
			configurationSection = obj2 as ConfigurationSection;
			if (configurationSection == null)
			{
				configurationSection = new DefaultSection
				{
					SectionHandler = (obj2 as IConfigurationSectionHandler)
				};
			}
			configurationSection.Configuration = this;
			ConfigurationSection configurationSection2 = null;
			if (parent != null)
			{
				configurationSection2 = parent.GetSectionInstance(config, createDefaultInstance: true);
				configurationSection.SectionInformation.SetParentSection(configurationSection2);
			}
			configurationSection.SectionInformation.ConfigFilePath = FilePath;
			configurationSection.ConfigContext = system.Host.CreateDeprecatedConfigContext(configPath);
			string text = (configurationSection.RawXml = obj as string);
			configurationSection.Reset(configurationSection2);
			if (text != null)
			{
				XmlTextReader xmlTextReader = new ConfigXmlTextReader(new StringReader(text), FilePath);
				configurationSection.DeserializeSection(xmlTextReader);
				xmlTextReader.Close();
				if (!string.IsNullOrEmpty(configurationSection.SectionInformation.ConfigSource) && !string.IsNullOrEmpty(FilePath))
				{
					configurationSection.DeserializeConfigSource(Path.GetDirectoryName(FilePath));
				}
			}
			elementData[config] = configurationSection;
			return configurationSection;
		}

		internal ConfigurationSectionGroup GetSectionGroupInstance(SectionGroupInfo group)
		{
			ConfigurationSectionGroup configurationSectionGroup = group.CreateInstance() as ConfigurationSectionGroup;
			configurationSectionGroup?.Initialize(this, group);
			return configurationSectionGroup;
		}

		internal void SetConfigurationSection(SectionInfo config, ConfigurationSection sec)
		{
			elementData[config] = sec;
		}

		internal void SetSectionXml(SectionInfo config, string data)
		{
			elementData[config] = data;
		}

		internal string GetSectionXml(SectionInfo config)
		{
			return elementData[config] as string;
		}

		internal void CreateSection(SectionGroupInfo group, string name, ConfigurationSection sec)
		{
			if (group.HasChild(name))
			{
				throw new ConfigurationErrorsException("Cannot add a ConfigurationSection. A section or section group already exists with the name '" + name + "'");
			}
			if (!HasFile && !sec.SectionInformation.AllowLocation)
			{
				throw new ConfigurationErrorsException("The configuration section <" + name + "> cannot be defined inside a <location> element.");
			}
			if (!system.Host.IsDefinitionAllowed(configPath, sec.SectionInformation.AllowDefinition, sec.SectionInformation.AllowExeDefinition))
			{
				object obj = ((sec.SectionInformation.AllowExeDefinition != ConfigurationAllowExeDefinition.MachineToApplication) ? ((object)sec.SectionInformation.AllowExeDefinition) : ((object)sec.SectionInformation.AllowDefinition));
				throw new ConfigurationErrorsException("The section <" + name + "> can't be defined in this configuration file (the allowed definition context is '" + obj?.ToString() + "').");
			}
			if (sec.SectionInformation.Type == null)
			{
				sec.SectionInformation.Type = system.Host.GetConfigTypeName(sec.GetType());
			}
			SectionInfo sectionInfo = new SectionInfo(name, sec.SectionInformation);
			sectionInfo.StreamName = streamName;
			sectionInfo.ConfigHost = system.Host;
			group.AddChild(sectionInfo);
			elementData[sectionInfo] = sec;
			sec.Configuration = this;
		}

		internal void CreateSectionGroup(SectionGroupInfo parentGroup, string name, ConfigurationSectionGroup sec)
		{
			if (parentGroup.HasChild(name))
			{
				throw new ConfigurationErrorsException("Cannot add a ConfigurationSectionGroup. A section or section group already exists with the name '" + name + "'");
			}
			if (sec.Type == null)
			{
				sec.Type = system.Host.GetConfigTypeName(sec.GetType());
			}
			sec.SetName(name);
			SectionGroupInfo sectionGroupInfo = new SectionGroupInfo(name, sec.Type);
			sectionGroupInfo.StreamName = streamName;
			sectionGroupInfo.ConfigHost = system.Host;
			parentGroup.AddChild(sectionGroupInfo);
			elementData[sectionGroupInfo] = sec;
			sec.Initialize(this, sectionGroupInfo);
		}

		internal void RemoveConfigInfo(ConfigInfo config)
		{
			elementData.Remove(config);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the current XML configuration file.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.  
		/// -or-
		///  The configuration file has changed.</exception>
		public void Save()
		{
			Save(ConfigurationSaveMode.Modified, forceSaveAll: false);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the current XML configuration file.</summary>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> value that determines which property values to save.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.  
		/// -or-
		///  The configuration file has changed.</exception>
		public void Save(ConfigurationSaveMode saveMode)
		{
			Save(saveMode, forceSaveAll: false);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the current XML configuration file.</summary>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> value that determines which property values to save.</param>
		/// <param name="forceSaveAll">
		///   <see langword="true" /> to save even if the configuration was not modified; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.  
		/// -or-
		///  The configuration file has changed.</exception>
		public void Save(ConfigurationSaveMode saveMode, bool forceSaveAll)
		{
			if (!forceSaveAll && saveMode != ConfigurationSaveMode.Full && !HasValues(saveMode))
			{
				ResetModified();
				return;
			}
			ConfigurationSaveEventHandler saveStart = Configuration.SaveStart;
			ConfigurationSaveEventHandler saveEnd = Configuration.SaveEnd;
			object writeContext = null;
			Exception ex = null;
			Stream stream = system.Host.OpenStreamForWrite(streamName, null, ref writeContext);
			try
			{
				saveStart?.Invoke(this, new ConfigurationSaveEventArgs(streamName, start: true, null, writeContext));
				Save(stream, saveMode, forceSaveAll);
				system.Host.WriteCompleted(streamName, success: true, writeContext);
			}
			catch (Exception ex2)
			{
				ex = ex2;
				system.Host.WriteCompleted(streamName, success: false, writeContext);
				throw;
			}
			finally
			{
				stream.Close();
				saveEnd?.Invoke(this, new ConfigurationSaveEventArgs(streamName, start: false, ex, writeContext));
			}
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the specified XML configuration file.</summary>
		/// <param name="filename">The path and file name to save the configuration file to.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.  
		/// -or-
		///  The configuration file has changed.</exception>
		public void SaveAs(string filename)
		{
			SaveAs(filename, ConfigurationSaveMode.Modified, forceSaveAll: false);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the specified XML configuration file.</summary>
		/// <param name="filename">The path and file name to save the configuration file to.</param>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> value that determines which property values to save.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.  
		/// -or-
		///  The configuration file has changed.</exception>
		public void SaveAs(string filename, ConfigurationSaveMode saveMode)
		{
			SaveAs(filename, saveMode, forceSaveAll: false);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the specified XML configuration file.</summary>
		/// <param name="filename">The path and file name to save the configuration file to.</param>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> value that determines which property values to save.</param>
		/// <param name="forceSaveAll">
		///   <see langword="true" /> to save even if the configuration was not modified; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="filename" /> is null or an empty string ("").</exception>
		[System.MonoInternalNote("Detect if file has changed")]
		public void SaveAs(string filename, ConfigurationSaveMode saveMode, bool forceSaveAll)
		{
			if (!forceSaveAll && saveMode != ConfigurationSaveMode.Full && !HasValues(saveMode))
			{
				ResetModified();
				return;
			}
			string directoryName = Path.GetDirectoryName(Path.GetFullPath(filename));
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			Save(new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write), saveMode, forceSaveAll);
		}

		private void Save(Stream stream, ConfigurationSaveMode mode, bool forceUpdateAll)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(new StreamWriter(stream));
			xmlTextWriter.Formatting = Formatting.Indented;
			try
			{
				xmlTextWriter.WriteStartDocument();
				if (rootNamespace != null)
				{
					xmlTextWriter.WriteStartElement("configuration", rootNamespace);
				}
				else
				{
					xmlTextWriter.WriteStartElement("configuration");
				}
				if (rootGroup.HasConfigContent(this))
				{
					rootGroup.WriteConfig(this, xmlTextWriter, mode);
				}
				foreach (ConfigurationLocation location in Locations)
				{
					if (location.OpenedConfiguration == null)
					{
						xmlTextWriter.WriteRaw("\n");
						xmlTextWriter.WriteRaw(location.XmlContent);
						continue;
					}
					xmlTextWriter.WriteStartElement("location");
					xmlTextWriter.WriteAttributeString("path", location.Path);
					if (!location.AllowOverride)
					{
						xmlTextWriter.WriteAttributeString("allowOverride", "false");
					}
					location.OpenedConfiguration.SaveData(xmlTextWriter, mode, forceUpdateAll);
					xmlTextWriter.WriteEndElement();
				}
				SaveData(xmlTextWriter, mode, forceUpdateAll);
				xmlTextWriter.WriteEndElement();
				ResetModified();
			}
			finally
			{
				xmlTextWriter.Flush();
				xmlTextWriter.Close();
			}
		}

		private void SaveData(XmlTextWriter tw, ConfigurationSaveMode mode, bool forceUpdateAll)
		{
			rootGroup.WriteRootData(tw, this, mode);
		}

		private bool HasValues(ConfigurationSaveMode mode)
		{
			foreach (ConfigurationLocation location in Locations)
			{
				if (location.OpenedConfiguration != null && location.OpenedConfiguration.HasValues(mode))
				{
					return true;
				}
			}
			return rootGroup.HasValues(this, mode);
		}

		private void ResetModified()
		{
			foreach (ConfigurationLocation location in Locations)
			{
				if (location.OpenedConfiguration != null)
				{
					location.OpenedConfiguration.ResetModified();
				}
			}
			rootGroup.ResetModified(this);
		}

		private bool Load()
		{
			if (string.IsNullOrEmpty(streamName))
			{
				return true;
			}
			Stream stream = null;
			try
			{
				stream = system.Host.OpenStreamForRead(streamName);
				if (stream == null)
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
			using (XmlTextReader reader = new ConfigXmlTextReader(stream, streamName))
			{
				ReadConfigFile(reader, streamName);
			}
			ResetModified();
			return true;
		}

		private void ReadConfigFile(XmlReader reader, string fileName)
		{
			reader.MoveToContent();
			if (reader.NodeType != XmlNodeType.Element || reader.Name != "configuration")
			{
				ThrowException("Configuration file does not have a valid root element", reader);
			}
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.LocalName == "xmlns")
					{
						rootNamespace = reader.Value;
					}
					else
					{
						ThrowException($"Unrecognized attribute '{reader.LocalName}' in root element", reader);
					}
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return;
			}
			reader.ReadStartElement();
			reader.MoveToContent();
			if (reader.LocalName == "configSections")
			{
				if (reader.HasAttributes)
				{
					ThrowException("Unrecognized attribute in <configSections>.", reader);
				}
				rootGroup.ReadConfig(this, fileName, reader);
			}
			rootGroup.ReadRootData(reader, this, overrideAllowed: true);
		}

		internal void ReadData(XmlReader reader, bool allowOverride)
		{
			rootGroup.ReadData(this, reader, allowOverride);
		}

		private void ThrowException(string text, XmlReader reader)
		{
			throw new ConfigurationErrorsException(text, streamName, (reader as IXmlLineInfo)?.LineNumber ?? 0);
		}

		internal Configuration()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Specifies the locations within the configuration-file hierarchy that can set or override the properties contained within a <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
	public enum ConfigurationAllowDefinition
	{
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined only in the Machine.config file.</summary>
		MachineOnly = 0,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined in either the Machine.config file or the machine-level Web.config file found in the same directory as Machine.config, but not in application Web.config files.</summary>
		MachineToWebRoot = 100,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined in either the Machine.config file, the machine-level Web.config file found in the same directory as Machine.config, or the top-level application Web.config file found in the virtual-directory root, but not in subdirectories of a virtual root.</summary>
		MachineToApplication = 200,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined anywhere.</summary>
		Everywhere = 300
	}
	/// <summary>Specifies the locations within the configuration-file hierarchy that can set or override the properties contained within a <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
	public enum ConfigurationAllowExeDefinition
	{
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined only in the Machine.config file.</summary>
		MachineOnly = 0,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined either in the Machine.config file or in the Exe.config file in the client application directory. This is the default value.</summary>
		MachineToApplication = 100,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined in the Machine.config file, in the Exe.config file in the client application directory, in the User.config file in the roaming user directory, or in the User.config file in the local user directory.</summary>
		MachineToLocalUser = 300,
		/// <summary>The <see cref="T:System.Configuration.ConfigurationSection" /> can be defined in the Machine.config file, in the Exe.config file in the client application directory, or in the User.config file in the roaming user directory.</summary>
		MachineToRoamingUser = 200
	}
	/// <summary>Declaratively instructs the .NET Framework to create an instance of a configuration element collection. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class ConfigurationCollectionAttribute : Attribute
	{
		private string addItemName = "add";

		private string clearItemsName = "clear";

		private string removeItemName = "remove";

		private ConfigurationElementCollectionType collectionType;

		private Type itemType;

		/// <summary>Gets or sets the name of the <see langword="&lt;add&gt;" /> configuration element.</summary>
		/// <returns>The name that substitutes the standard name "add" for the configuration item.</returns>
		public string AddItemName
		{
			get
			{
				return addItemName;
			}
			set
			{
				addItemName = value;
			}
		}

		/// <summary>Gets or sets the name for the <see langword="&lt;clear&gt;" /> configuration element.</summary>
		/// <returns>The name that replaces the standard name "clear" for the configuration item.</returns>
		public string ClearItemsName
		{
			get
			{
				return clearItemsName;
			}
			set
			{
				clearItemsName = value;
			}
		}

		/// <summary>Gets or sets the name for the <see langword="&lt;remove&gt;" /> configuration element.</summary>
		/// <returns>The name that replaces the standard name "remove" for the configuration element.</returns>
		public string RemoveItemName
		{
			get
			{
				return removeItemName;
			}
			set
			{
				removeItemName = value;
			}
		}

		/// <summary>Gets or sets the type of the <see cref="T:System.Configuration.ConfigurationCollectionAttribute" /> attribute.</summary>
		/// <returns>The type of the <see cref="T:System.Configuration.ConfigurationCollectionAttribute" />.</returns>
		public ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return collectionType;
			}
			set
			{
				collectionType = value;
			}
		}

		/// <summary>Gets the type of the collection element.</summary>
		/// <returns>The type of the collection element.</returns>
		[System.MonoInternalNote("Do something with this in ConfigurationElementCollection")]
		public Type ItemType => itemType;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationCollectionAttribute" /> class.</summary>
		/// <param name="itemType">The type of the property collection to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="itemType" /> is <see langword="null" />.</exception>
		public ConfigurationCollectionAttribute(Type itemType)
		{
			this.itemType = itemType;
		}
	}
	/// <summary>The base class for the configuration converter types.</summary>
	public abstract class ConfigurationConverterBase : TypeConverter
	{
		/// <summary>Determines whether the conversion is allowed.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="type">The <see cref="T:System.Type" /> to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if the conversion is allowed; otherwise, <see langword="false" />.</returns>
		public override bool CanConvertFrom(ITypeDescriptorContext ctx, Type type)
		{
			if (type == typeof(string))
			{
				return true;
			}
			return base.CanConvertFrom(ctx, type);
		}

		/// <summary>Determines whether the conversion is allowed.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversion.</param>
		/// <param name="type">The type to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if the conversion is allowed; otherwise, <see langword="false" />.</returns>
		public override bool CanConvertTo(ITypeDescriptorContext ctx, Type type)
		{
			if (type == typeof(string))
			{
				return true;
			}
			return base.CanConvertTo(ctx, type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationConverterBase" /> class.</summary>
		protected ConfigurationConverterBase()
		{
		}
	}
	/// <summary>Represents a configuration element within a configuration file.</summary>
	public abstract class ConfigurationElement
	{
		private class SaveContext
		{
			public readonly ConfigurationElement Element;

			public readonly ConfigurationElement Parent;

			public readonly ConfigurationSaveMode Mode;

			public SaveContext(ConfigurationElement element, ConfigurationElement parent, ConfigurationSaveMode mode)
			{
				Element = element;
				Parent = parent;
				Mode = mode;
			}

			public bool HasValues()
			{
				if (Mode == ConfigurationSaveMode.Full)
				{
					return true;
				}
				return Element.HasValues(Parent, Mode);
			}

			public bool HasValue(PropertyInformation prop)
			{
				if (Mode == ConfigurationSaveMode.Full)
				{
					return true;
				}
				return Element.HasValue(Parent, prop, Mode);
			}
		}

		private string rawXml;

		private bool modified;

		private ElementMap map;

		private ConfigurationPropertyCollection keyProps;

		private ConfigurationElementCollection defaultCollection;

		private bool readOnly;

		private ElementInformation elementInfo;

		private ConfigurationElementProperty elementProperty;

		private Configuration _configuration;

		private bool elementPresent;

		private ConfigurationLockCollection lockAllAttributesExcept;

		private ConfigurationLockCollection lockAllElementsExcept;

		private ConfigurationLockCollection lockAttributes;

		private ConfigurationLockCollection lockElements;

		private bool lockItem;

		private SaveContext saveContext;

		internal Configuration Configuration
		{
			get
			{
				return _configuration;
			}
			set
			{
				_configuration = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Configuration.ElementInformation" /> object that contains the non-customizable information and functionality of the <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>An <see cref="T:System.Configuration.ElementInformation" /> that contains the non-customizable information and functionality of the <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		public ElementInformation ElementInformation
		{
			get
			{
				if (elementInfo == null)
				{
					elementInfo = new ElementInformation(this, null);
				}
				return elementInfo;
			}
		}

		internal string RawXml
		{
			get
			{
				return rawXml;
			}
			set
			{
				if (rawXml == null || value != null)
				{
					rawXml = value;
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationElementProperty" /> object that represents the <see cref="T:System.Configuration.ConfigurationElement" /> object itself.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementProperty" /> that represents the <see cref="T:System.Configuration.ConfigurationElement" /> itself.</returns>
		protected internal virtual ConfigurationElementProperty ElementProperty
		{
			get
			{
				if (elementProperty == null)
				{
					elementProperty = new ConfigurationElementProperty(ElementInformation.Validator);
				}
				return elementProperty;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ContextInformation" /> object for the <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ContextInformation" /> for the <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The current element is not associated with a context.</exception>
		protected ContextInformation EvaluationContext
		{
			get
			{
				if (Configuration != null)
				{
					return Configuration.EvaluationContext;
				}
				throw new ConfigurationErrorsException("This element is not currently associated with any context.");
			}
		}

		/// <summary>Gets the collection of locked attributes.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked attributes (properties) for the element.</returns>
		public ConfigurationLockCollection LockAllAttributesExcept
		{
			get
			{
				if (lockAllAttributesExcept == null)
				{
					lockAllAttributesExcept = new ConfigurationLockCollection(this, ConfigurationLockType.Attribute | ConfigurationLockType.Exclude);
				}
				return lockAllAttributesExcept;
			}
		}

		/// <summary>Gets the collection of locked elements.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked elements.</returns>
		public ConfigurationLockCollection LockAllElementsExcept
		{
			get
			{
				if (lockAllElementsExcept == null)
				{
					lockAllElementsExcept = new ConfigurationLockCollection(this, ConfigurationLockType.Element | ConfigurationLockType.Exclude);
				}
				return lockAllElementsExcept;
			}
		}

		/// <summary>Gets the collection of locked attributes</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked attributes (properties) for the element.</returns>
		public ConfigurationLockCollection LockAttributes
		{
			get
			{
				if (lockAttributes == null)
				{
					lockAttributes = new ConfigurationLockCollection(this, ConfigurationLockType.Attribute);
				}
				return lockAttributes;
			}
		}

		/// <summary>Gets the collection of locked elements.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked elements.</returns>
		public ConfigurationLockCollection LockElements
		{
			get
			{
				if (lockElements == null)
				{
					lockElements = new ConfigurationLockCollection(this, ConfigurationLockType.Element);
				}
				return lockElements;
			}
		}

		/// <summary>Gets or sets a value indicating whether the element is locked.</summary>
		/// <returns>
		///   <see langword="true" /> if the element is locked; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element has already been locked at a higher configuration level.</exception>
		public bool LockItem
		{
			get
			{
				return lockItem;
			}
			set
			{
				lockItem = value;
			}
		}

		/// <summary>Gets or sets a property or attribute of this configuration element.</summary>
		/// <param name="prop">The property to access.</param>
		/// <returns>The specified property, attribute, or child element.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationException">
		///   <paramref name="prop" /> is <see langword="null" /> or does not exist within the element.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="prop" /> is read only or locked.</exception>
		protected internal object this[ConfigurationProperty prop]
		{
			get
			{
				return this[prop.Name];
			}
			set
			{
				this[prop.Name] = value;
			}
		}

		/// <summary>Gets or sets a property, attribute, or child element of this configuration element.</summary>
		/// <param name="propertyName">The name of the <see cref="T:System.Configuration.ConfigurationProperty" /> to access.</param>
		/// <returns>The specified property, attribute, or child element</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="prop" /> is read-only or locked.</exception>
		protected internal object this[string propertyName]
		{
			get
			{
				return (ElementInformation.Properties[propertyName] ?? throw new InvalidOperationException("Property '" + propertyName + "' not found in configuration element")).Value;
			}
			set
			{
				PropertyInformation propertyInformation = ElementInformation.Properties[propertyName];
				if (propertyInformation == null)
				{
					throw new InvalidOperationException("Property '" + propertyName + "' not found in configuration element");
				}
				SetPropertyValue(propertyInformation.Property, value, ignoreLocks: false);
				propertyInformation.Value = value;
				modified = true;
			}
		}

		/// <summary>Gets the collection of properties.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> of properties for the element.</returns>
		protected internal virtual ConfigurationPropertyCollection Properties
		{
			get
			{
				if (map == null)
				{
					map = ElementMap.GetMap(GetType());
				}
				return map.Properties;
			}
		}

		internal bool IsElementPresent => elementPresent;

		/// <summary>Gets a reference to the top-level <see cref="T:System.Configuration.Configuration" /> instance that represents the configuration hierarchy that the current <see cref="T:System.Configuration.ConfigurationElement" /> instance belongs to.</summary>
		/// <returns>The top-level <see cref="T:System.Configuration.Configuration" /> instance that the current <see cref="T:System.Configuration.ConfigurationElement" /> instance belongs to.</returns>
		public Configuration CurrentConfiguration
		{
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="P:System.Configuration.ConfigurationElement.CurrentConfiguration" /> property is <see langword="null" />.</summary>
		/// <returns>false if the <see cref="P:System.Configuration.ConfigurationElement.CurrentConfiguration" /> property is <see langword="null" />; otherwise, <see langword="true" />.</returns>
		protected bool HasContext
		{
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationElement" /> class.</summary>
		protected ConfigurationElement()
		{
		}

		internal virtual void InitFromProperty(PropertyInformation propertyInfo)
		{
			elementInfo = new ElementInformation(this, propertyInfo);
			Init();
		}

		/// <summary>Sets the <see cref="T:System.Configuration.ConfigurationElement" /> object to its initial state.</summary>
		protected internal virtual void Init()
		{
		}

		/// <summary>Adds the invalid-property errors in this <see cref="T:System.Configuration.ConfigurationElement" /> object, and in all subelements, to the passed list.</summary>
		/// <param name="errorList">An object that implements the <see cref="T:System.Collections.IList" /> interface.</param>
		[System.MonoTODO]
		protected virtual void ListErrors(IList errorList)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets a property to the specified value.</summary>
		/// <param name="prop">The element property to set.</param>
		/// <param name="value">The value to assign to the property.</param>
		/// <param name="ignoreLocks">
		///   <see langword="true" /> if the locks on the property should be ignored; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs if the element is read-only or <paramref name="ignoreLocks" /> is <see langword="true" /> but the locks cannot be ignored.</exception>
		[System.MonoTODO]
		protected void SetPropertyValue(ConfigurationProperty prop, object value, bool ignoreLocks)
		{
			try
			{
				if (value != null)
				{
					prop.Validate(value);
				}
			}
			catch (Exception inner)
			{
				throw new ConfigurationErrorsException($"The value for the property '{prop.Name}' on type {ElementInformation.Type} is not valid.", inner);
			}
		}

		internal ConfigurationPropertyCollection GetKeyProperties()
		{
			if (keyProps != null)
			{
				return keyProps;
			}
			ConfigurationPropertyCollection configurationPropertyCollection = new ConfigurationPropertyCollection();
			foreach (ConfigurationProperty property in Properties)
			{
				if (property.IsKey)
				{
					configurationPropertyCollection.Add(property);
				}
			}
			return keyProps = configurationPropertyCollection;
		}

		internal ConfigurationElementCollection GetDefaultCollection()
		{
			if (defaultCollection != null)
			{
				return defaultCollection;
			}
			ConfigurationProperty configurationProperty = null;
			foreach (ConfigurationProperty property in Properties)
			{
				if (property.IsDefaultCollection)
				{
					configurationProperty = property;
					break;
				}
			}
			if (configurationProperty != null)
			{
				defaultCollection = this[configurationProperty] as ConfigurationElementCollection;
			}
			return defaultCollection;
		}

		/// <summary>Compares the current <see cref="T:System.Configuration.ConfigurationElement" /> instance to the specified object.</summary>
		/// <param name="compareTo">The object to compare with.</param>
		/// <returns>
		///   <see langword="true" /> if the object to compare with is equal to the current <see cref="T:System.Configuration.ConfigurationElement" /> instance; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public override bool Equals(object compareTo)
		{
			if (!(compareTo is ConfigurationElement configurationElement))
			{
				return false;
			}
			if (GetType() != configurationElement.GetType())
			{
				return false;
			}
			foreach (ConfigurationProperty property in Properties)
			{
				if (!object.Equals(this[property], configurationElement[property]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a unique value representing the current <see cref="T:System.Configuration.ConfigurationElement" /> instance.</summary>
		/// <returns>A unique value representing the current <see cref="T:System.Configuration.ConfigurationElement" /> instance.</returns>
		public override int GetHashCode()
		{
			int num = 0;
			foreach (ConfigurationProperty property in Properties)
			{
				object obj = this[property];
				if (obj != null)
				{
					num += obj.GetHashCode();
				}
			}
			return num;
		}

		internal virtual bool HasLocalModifications()
		{
			foreach (PropertyInformation property in ElementInformation.Properties)
			{
				if (property.ValueOrigin == PropertyValueOrigin.SetHere && property.IsModified)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Reads XML from the configuration file.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> that reads from the configuration file.</param>
		/// <param name="serializeCollectionKey">
		///   <see langword="true" /> to serialize only the collection key properties; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element to read is locked.  
		/// -or-
		///  An attribute of the current node is not recognized.  
		/// -or-
		///  The lock status of the current node cannot be determined.</exception>
		protected internal virtual void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			Hashtable hashtable = new Hashtable();
			reader.MoveToContent();
			elementPresent = true;
			while (reader.MoveToNextAttribute())
			{
				PropertyInformation propertyInformation = ElementInformation.Properties[reader.LocalName];
				if (propertyInformation == null || (serializeCollectionKey && !propertyInformation.IsKey))
				{
					if (reader.LocalName == "lockAllAttributesExcept")
					{
						LockAllAttributesExcept.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockAllElementsExcept")
					{
						LockAllElementsExcept.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockAttributes")
					{
						LockAttributes.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockElements")
					{
						LockElements.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockItem")
					{
						LockItem = reader.Value.ToLowerInvariant() == "true";
					}
					else if (!(reader.LocalName == "xmlns") && (!(this is ConfigurationSection) || !(reader.LocalName == "configSource")) && !OnDeserializeUnrecognizedAttribute(reader.LocalName, reader.Value))
					{
						throw new ConfigurationErrorsException("Unrecognized attribute '" + reader.LocalName + "'.", reader);
					}
					continue;
				}
				if (hashtable.ContainsKey(propertyInformation))
				{
					throw new ConfigurationErrorsException("The attribute '" + propertyInformation.Name + "' may only appear once in this element.", reader);
				}
				string text = null;
				try
				{
					text = reader.Value;
					ValidateValue(propertyInformation.Property, text);
					propertyInformation.SetStringValue(text);
				}
				catch (ConfigurationErrorsException)
				{
					throw;
				}
				catch (ConfigurationException)
				{
					throw;
				}
				catch (Exception ex3)
				{
					throw new ConfigurationErrorsException($"The value for the property '{propertyInformation.Name}' is not valid. The error is: {ex3.Message}", reader);
				}
				hashtable[propertyInformation] = propertyInformation.Name;
				if (reader is ConfigXmlTextReader configXmlTextReader)
				{
					propertyInformation.Source = configXmlTextReader.Filename;
					propertyInformation.LineNumber = configXmlTextReader.LineNumber;
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				reader.Skip();
			}
			else
			{
				int depth = reader.Depth;
				reader.ReadStartElement();
				reader.MoveToContent();
				do
				{
					if (reader.NodeType != XmlNodeType.Element)
					{
						reader.Skip();
						continue;
					}
					PropertyInformation propertyInformation2 = ElementInformation.Properties[reader.LocalName];
					if (propertyInformation2 == null || (serializeCollectionKey && !propertyInformation2.IsKey))
					{
						if (OnDeserializeUnrecognizedElement(reader.LocalName, reader))
						{
							continue;
						}
						if (propertyInformation2 == null)
						{
							ConfigurationElementCollection configurationElementCollection = GetDefaultCollection();
							if (configurationElementCollection != null && configurationElementCollection.OnDeserializeUnrecognizedElement(reader.LocalName, reader))
							{
								continue;
							}
						}
						throw new ConfigurationErrorsException("Unrecognized element '" + reader.LocalName + "'.", reader);
					}
					if (!propertyInformation2.IsElement)
					{
						throw new ConfigurationErrorsException("Property '" + propertyInformation2.Name + "' is not a ConfigurationElement.");
					}
					if (hashtable.Contains(propertyInformation2))
					{
						throw new ConfigurationErrorsException("The element <" + propertyInformation2.Name + "> may only appear once in this section.", reader);
					}
					((ConfigurationElement)propertyInformation2.Value).DeserializeElement(reader, serializeCollectionKey);
					hashtable[propertyInformation2] = propertyInformation2.Name;
					if (depth == reader.Depth)
					{
						reader.Read();
					}
				}
				while (depth < reader.Depth);
			}
			modified = false;
			foreach (PropertyInformation property in ElementInformation.Properties)
			{
				if (!string.IsNullOrEmpty(property.Name) && property.IsRequired && !hashtable.ContainsKey(property) && ElementInformation.Properties[property.Name] == null)
				{
					object obj = OnRequiredPropertyNotFound(property.Name);
					if (!object.Equals(obj, property.DefaultValue))
					{
						property.Value = obj;
						property.IsModified = false;
					}
				}
			}
			PostDeserialize();
		}

		/// <summary>Gets a value indicating whether an unknown attribute is encountered during deserialization.</summary>
		/// <param name="name">The name of the unrecognized attribute.</param>
		/// <param name="value">The value of the unrecognized attribute.</param>
		/// <returns>
		///   <see langword="true" /> when an unknown attribute is encountered while deserializing; otherwise, <see langword="false" />.</returns>
		protected virtual bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			return false;
		}

		/// <summary>Gets a value indicating whether an unknown element is encountered during deserialization.</summary>
		/// <param name="elementName">The name of the unknown subelement.</param>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> being used for deserialization.</param>
		/// <returns>
		///   <see langword="true" /> when an unknown element is encountered while deserializing; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element identified by <paramref name="elementName" /> is locked.  
		/// -or-
		///  One or more of the element's attributes is locked.  
		/// -or-
		///  <paramref name="elementName" /> is unrecognized, or the element has an unrecognized attribute.  
		/// -or-
		///  The element has a Boolean attribute with an invalid value.  
		/// -or-
		///  An attempt was made to deserialize a property more than once.  
		/// -or-
		///  An attempt was made to deserialize a property that is not a valid member of the element.  
		/// -or-
		///  The element cannot contain a CDATA or text element.</exception>
		protected virtual bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return false;
		}

		/// <summary>Throws an exception when a required property is not found.</summary>
		/// <param name="name">The name of the required attribute that was not found.</param>
		/// <returns>None.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">In all cases.</exception>
		protected virtual object OnRequiredPropertyNotFound(string name)
		{
			throw new ConfigurationErrorsException("Required attribute '" + name + "' not found.");
		}

		/// <summary>Called before serialization.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that will be used to serialize the <see cref="T:System.Configuration.ConfigurationElement" />.</param>
		protected virtual void PreSerialize(XmlWriter writer)
		{
		}

		/// <summary>Called after deserialization.</summary>
		protected virtual void PostDeserialize()
		{
		}

		/// <summary>Used to initialize a default set of values for the <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		protected internal virtual void InitializeDefault()
		{
		}

		/// <summary>Indicates whether this configuration element has been modified since it was last saved or loaded, when implemented in a derived class.</summary>
		/// <returns>
		///   <see langword="true" /> if the element has been modified; otherwise, <see langword="false" />.</returns>
		protected internal virtual bool IsModified()
		{
			if (modified)
			{
				return true;
			}
			foreach (PropertyInformation property in ElementInformation.Properties)
			{
				if (property.IsElement && property.Value is ConfigurationElement configurationElement && configurationElement.IsModified())
				{
					modified = true;
					break;
				}
			}
			return modified;
		}

		/// <summary>Sets the <see cref="M:System.Configuration.ConfigurationElement.IsReadOnly" /> property for the <see cref="T:System.Configuration.ConfigurationElement" /> object and all subelements.</summary>
		protected internal virtual void SetReadOnly()
		{
			readOnly = true;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only; otherwise, <see langword="false" />.</returns>
		public virtual bool IsReadOnly()
		{
			return readOnly;
		}

		/// <summary>Resets the internal state of the <see cref="T:System.Configuration.ConfigurationElement" /> object, including the locks and the properties collections.</summary>
		/// <param name="parentElement">The parent node of the configuration element.</param>
		protected internal virtual void Reset(ConfigurationElement parentElement)
		{
			elementPresent = false;
			if (parentElement != null)
			{
				ElementInformation.Reset(parentElement.ElementInformation);
			}
			else
			{
				InitializeDefault();
			}
		}

		/// <summary>Resets the value of the <see cref="M:System.Configuration.ConfigurationElement.IsModified" /> method to <see langword="false" /> when implemented in a derived class.</summary>
		protected internal virtual void ResetModified()
		{
			modified = false;
			foreach (PropertyInformation property in ElementInformation.Properties)
			{
				property.IsModified = false;
				if (property.Value is ConfigurationElement configurationElement)
				{
					configurationElement.ResetModified();
				}
			}
		}

		/// <summary>Writes the contents of this configuration element to the configuration file when implemented in a derived class.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that writes to the configuration file.</param>
		/// <param name="serializeCollectionKey">
		///   <see langword="true" /> to serialize only the collection key properties; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if any data was actually serialized; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The current attribute is locked at a higher configuration level.</exception>
		protected internal virtual bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			PreSerialize(writer);
			if (serializeCollectionKey)
			{
				ConfigurationPropertyCollection keyProperties = GetKeyProperties();
				foreach (ConfigurationProperty item in keyProperties)
				{
					writer.WriteAttributeString(item.Name, item.ConvertToString(this[item.Name]));
				}
				return keyProperties.Count > 0;
			}
			bool flag = false;
			foreach (PropertyInformation property in ElementInformation.Properties)
			{
				if (!property.IsElement)
				{
					if (saveContext == null)
					{
						throw new InvalidOperationException();
					}
					if (saveContext.HasValue(property))
					{
						writer.WriteAttributeString(property.Name, property.GetStringValue());
						flag = true;
					}
				}
			}
			foreach (PropertyInformation property2 in ElementInformation.Properties)
			{
				if (property2.IsElement)
				{
					ConfigurationElement configurationElement = (ConfigurationElement)property2.Value;
					if (configurationElement != null)
					{
						flag = configurationElement.SerializeToXmlElement(writer, property2.Name) || flag;
					}
				}
			}
			return flag;
		}

		/// <summary>Writes the outer tags of this configuration element to the configuration file when implemented in a derived class.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that writes to the configuration file.</param>
		/// <param name="elementName">The name of the <see cref="T:System.Configuration.ConfigurationElement" /> to be written.</param>
		/// <returns>
		///   <see langword="true" /> if writing was successful; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Exception">The element has multiple child elements.</exception>
		protected internal virtual bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			if (saveContext == null)
			{
				throw new InvalidOperationException();
			}
			if (!saveContext.HasValues())
			{
				return false;
			}
			if (elementName != null && elementName != "")
			{
				writer.WriteStartElement(elementName);
			}
			bool result = SerializeElement(writer, serializeCollectionKey: false);
			if (elementName != null && elementName != "")
			{
				writer.WriteEndElement();
			}
			return result;
		}

		/// <summary>Modifies the <see cref="T:System.Configuration.ConfigurationElement" /> object to remove all values that should not be saved.</summary>
		/// <param name="sourceElement">A <see cref="T:System.Configuration.ConfigurationElement" /> at the current level containing a merged view of the properties.</param>
		/// <param name="parentElement">The parent <see cref="T:System.Configuration.ConfigurationElement" />, or <see langword="null" /> if this is the top level.</param>
		/// <param name="saveMode">One of the enumeration values that determines which property values to include.</param>
		protected internal virtual void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			if (parentElement != null && sourceElement.GetType() != parentElement.GetType())
			{
				throw new ConfigurationErrorsException("Can't unmerge two elements of different type");
			}
			bool flag = saveMode == ConfigurationSaveMode.Minimal || saveMode == ConfigurationSaveMode.Modified;
			foreach (PropertyInformation property in sourceElement.ElementInformation.Properties)
			{
				if (property.ValueOrigin == PropertyValueOrigin.Default)
				{
					continue;
				}
				PropertyInformation propertyInformation2 = ElementInformation.Properties[property.Name];
				object value = property.Value;
				if (parentElement == null || !parentElement.HasValue(property.Name))
				{
					propertyInformation2.Value = value;
				}
				else
				{
					if (value == null)
					{
						continue;
					}
					object obj = parentElement[property.Name];
					if (!property.IsElement)
					{
						if (!object.Equals(value, obj) || saveMode == ConfigurationSaveMode.Full || (saveMode == ConfigurationSaveMode.Modified && property.ValueOrigin == PropertyValueOrigin.SetHere))
						{
							propertyInformation2.Value = value;
						}
						continue;
					}
					ConfigurationElement configurationElement = (ConfigurationElement)value;
					if (!flag || configurationElement.IsModified())
					{
						if (obj == null)
						{
							propertyInformation2.Value = value;
							continue;
						}
						ConfigurationElement parentElement2 = (ConfigurationElement)obj;
						((ConfigurationElement)propertyInformation2.Value).Unmerge(configurationElement, parentElement2, saveMode);
					}
				}
			}
		}

		internal bool HasValue(string propName)
		{
			PropertyInformation propertyInformation = ElementInformation.Properties[propName];
			if (propertyInformation != null)
			{
				return propertyInformation.ValueOrigin != PropertyValueOrigin.Default;
			}
			return false;
		}

		internal bool IsReadFromConfig(string propName)
		{
			PropertyInformation propertyInformation = ElementInformation.Properties[propName];
			if (propertyInformation != null)
			{
				return propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere;
			}
			return false;
		}

		private void ValidateValue(ConfigurationProperty p, string value)
		{
			ConfigurationValidatorBase validator;
			if (p != null && (validator = p.Validator) != null)
			{
				if (!validator.CanValidate(p.Type))
				{
					throw new ConfigurationErrorsException($"Validator does not support type {p.Type}");
				}
				validator.Validate(p.ConvertFromString(value));
			}
		}

		internal bool HasValue(ConfigurationElement parent, PropertyInformation prop, ConfigurationSaveMode mode)
		{
			if (prop.ValueOrigin == PropertyValueOrigin.Default)
			{
				return false;
			}
			if (mode == ConfigurationSaveMode.Modified && prop.ValueOrigin == PropertyValueOrigin.SetHere && prop.IsModified)
			{
				return true;
			}
			object obj = ((parent != null && parent.HasValue(prop.Name)) ? parent[prop.Name] : prop.DefaultValue);
			if (!prop.IsElement)
			{
				return !object.Equals(prop.Value, obj);
			}
			ConfigurationElement obj2 = (ConfigurationElement)prop.Value;
			ConfigurationElement parent2 = (ConfigurationElement)obj;
			return obj2.HasValues(parent2, mode);
		}

		internal virtual bool HasValues(ConfigurationElement parent, ConfigurationSaveMode mode)
		{
			if (mode == ConfigurationSaveMode.Full)
			{
				return true;
			}
			if (modified && mode == ConfigurationSaveMode.Modified)
			{
				return true;
			}
			foreach (PropertyInformation property in ElementInformation.Properties)
			{
				if (HasValue(parent, property, mode))
				{
					return true;
				}
			}
			return false;
		}

		internal virtual void PrepareSave(ConfigurationElement parent, ConfigurationSaveMode mode)
		{
			saveContext = new SaveContext(this, parent, mode);
			foreach (PropertyInformation property in ElementInformation.Properties)
			{
				if (property.IsElement)
				{
					ConfigurationElement configurationElement = (ConfigurationElement)property.Value;
					if (parent == null || !parent.HasValue(property.Name))
					{
						configurationElement.PrepareSave(null, mode);
						continue;
					}
					ConfigurationElement parent2 = (ConfigurationElement)parent[property.Name];
					configurationElement.PrepareSave(parent2, mode);
				}
			}
		}

		/// <summary>Returns the transformed version of the specified assembly name.</summary>
		/// <param name="assemblyName">The name of the assembly.</param>
		/// <returns>The transformed version of the assembly name. If no transformer is available, the <paramref name="assemblyName" /> parameter value is returned unchanged. The <see cref="P:System.Configuration.Configuration.TypeStringTransformer" /> property is <see langword="null" /> if no transformer is available.</returns>
		protected virtual string GetTransformedAssemblyString(string assemblyName)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the transformed version of the specified type name.</summary>
		/// <param name="typeName">The name of the type.</param>
		/// <returns>The transformed version of the specified type name. If no transformer is available, the <paramref name="typeName" /> parameter value is returned unchanged. The <see cref="P:System.Configuration.Configuration.TypeStringTransformer" /> property is <see langword="null" /> if no transformer is available.</returns>
		protected virtual string GetTransformedTypeString(string typeName)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
	internal class ElementMap
	{
		private static readonly Hashtable elementMaps = Hashtable.Synchronized(new Hashtable());

		private readonly ConfigurationPropertyCollection properties;

		private readonly ConfigurationCollectionAttribute collectionAttribute;

		public ConfigurationCollectionAttribute CollectionAttribute => collectionAttribute;

		public bool HasProperties => properties.Count > 0;

		public ConfigurationPropertyCollection Properties => properties;

		public static ElementMap GetMap(Type t)
		{
			if (elementMaps[t] is ElementMap result)
			{
				return result;
			}
			ElementMap elementMap = new ElementMap(t);
			elementMaps[t] = elementMap;
			return elementMap;
		}

		public ElementMap(Type t)
		{
			properties = new ConfigurationPropertyCollection();
			collectionAttribute = Attribute.GetCustomAttribute(t, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute;
			PropertyInfo[] array = t.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (PropertyInfo propertyInfo in array)
			{
				if (Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationPropertyAttribute)) is ConfigurationPropertyAttribute configurationPropertyAttribute)
				{
					string name = ((configurationPropertyAttribute.Name != null) ? configurationPropertyAttribute.Name : propertyInfo.Name);
					ConfigurationValidatorBase validator = ((Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationValidatorAttribute)) is ConfigurationValidatorAttribute configurationValidatorAttribute) ? configurationValidatorAttribute.ValidatorInstance : null);
					TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(TypeConverterAttribute));
					ConfigurationProperty property = new ConfigurationProperty(typeConverter: (typeConverterAttribute != null) ? ((TypeConverter)Activator.CreateInstance(Type.GetType(typeConverterAttribute.ConverterTypeName), nonPublic: true)) : null, name: name, type: propertyInfo.PropertyType, defaultValue: configurationPropertyAttribute.DefaultValue, validator: validator, options: configurationPropertyAttribute.Options)
					{
						CollectionAttribute = (Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute)
					};
					properties.Add(property);
				}
			}
		}
	}
	/// <summary>Represents a configuration element containing a collection of child elements.</summary>
	[DebuggerDisplay("Count = {Count}")]
	public abstract class ConfigurationElementCollection : ConfigurationElement, ICollection, IEnumerable
	{
		private sealed class ConfigurationRemoveElement : ConfigurationElement
		{
			private readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

			private readonly ConfigurationElement _origElement;

			private readonly ConfigurationElementCollection _origCollection;

			internal object KeyValue
			{
				get
				{
					foreach (ConfigurationProperty property in Properties)
					{
						_origElement[property] = base[property];
					}
					return _origCollection.GetElementKey(_origElement);
				}
			}

			protected internal override ConfigurationPropertyCollection Properties => properties;

			internal ConfigurationRemoveElement(ConfigurationElement origElement, ConfigurationElementCollection origCollection)
			{
				_origElement = origElement;
				_origCollection = origCollection;
				foreach (ConfigurationProperty property in origElement.Properties)
				{
					if (property.IsKey)
					{
						properties.Add(property);
					}
				}
			}
		}

		private ArrayList list = new ArrayList();

		private ArrayList removed;

		private ArrayList inherited;

		private bool emitClear;

		private bool modified;

		private IComparer comparer;

		private int inheritedLimitIndex;

		private string addElementName = "add";

		private string clearElementName = "clear";

		private string removeElementName = "remove";

		/// <summary>Gets the type of the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> of this collection.</returns>
		public virtual ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.AddRemoveClearMap;

		private bool IsBasic
		{
			get
			{
				if (CollectionType != ConfigurationElementCollectionType.BasicMap)
				{
					return CollectionType == ConfigurationElementCollectionType.BasicMapAlternate;
				}
				return true;
			}
		}

		private bool IsAlternate
		{
			get
			{
				if (CollectionType != ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
				{
					return CollectionType == ConfigurationElementCollectionType.BasicMapAlternate;
				}
				return true;
			}
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		public int Count => list.Count;

		/// <summary>Gets the name used to identify this collection of elements in the configuration file when overridden in a derived class.</summary>
		/// <returns>The name of the collection; otherwise, an empty string. The default is an empty string.</returns>
		protected virtual string ElementName => string.Empty;

		/// <summary>Gets or sets a value that specifies whether the collection has been cleared.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection has been cleared; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration is read-only.</exception>
		public bool EmitClear
		{
			get
			{
				return emitClear;
			}
			set
			{
				emitClear = value;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized.</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Configuration.ConfigurationElementCollection" /> is synchronized; otherwise, <see langword="false" />.</returns>
		public bool IsSynchronized => false;

		/// <summary>Gets an object used to synchronize access to the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>An object used to synchronize access to the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</returns>
		public object SyncRoot => this;

		/// <summary>Gets a value indicating whether an attempt to add a duplicate <see cref="T:System.Configuration.ConfigurationElement" /> to the <see cref="T:System.Configuration.ConfigurationElementCollection" /> will cause an exception to be thrown.</summary>
		/// <returns>
		///   <see langword="true" /> if an attempt to add a duplicate <see cref="T:System.Configuration.ConfigurationElement" /> to this <see cref="T:System.Configuration.ConfigurationElementCollection" /> will cause an exception to be thrown; otherwise, <see langword="false" />.</returns>
		protected virtual bool ThrowOnDuplicate
		{
			get
			{
				if (CollectionType != ConfigurationElementCollectionType.AddRemoveClearMap && CollectionType != ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
				{
					return false;
				}
				return true;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the add operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a derived class.</summary>
		/// <returns>The name of the element.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value starts with the reserved prefix "config" or "lock".</exception>
		protected internal string AddElementName
		{
			get
			{
				return addElementName;
			}
			set
			{
				addElementName = value;
			}
		}

		/// <summary>Gets or sets the name for the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the clear operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a derived class.</summary>
		/// <returns>The name of the element.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value starts with the reserved prefix "config" or "lock".</exception>
		protected internal string ClearElementName
		{
			get
			{
				return clearElementName;
			}
			set
			{
				clearElementName = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the remove operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a derived class.</summary>
		/// <returns>The name of the element.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value starts with the reserved prefix "config" or "lock".</exception>
		protected internal string RemoveElementName
		{
			get
			{
				return removeElementName;
			}
			set
			{
				removeElementName = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationElementCollection" /> class.</summary>
		protected ConfigurationElementCollection()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.ConfigurationElementCollection" /> class.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> comparer to use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparer" /> is <see langword="null" />.</exception>
		protected ConfigurationElementCollection(IComparer comparer)
		{
			this.comparer = comparer;
		}

		internal override void InitFromProperty(PropertyInformation propertyInfo)
		{
			ConfigurationCollectionAttribute configurationCollectionAttribute = propertyInfo.Property.CollectionAttribute;
			if (configurationCollectionAttribute == null)
			{
				configurationCollectionAttribute = Attribute.GetCustomAttribute(propertyInfo.Type, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute;
			}
			if (configurationCollectionAttribute != null)
			{
				addElementName = configurationCollectionAttribute.AddItemName;
				clearElementName = configurationCollectionAttribute.ClearItemsName;
				removeElementName = configurationCollectionAttribute.RemoveItemName;
			}
			base.InitFromProperty(propertyInfo);
		}

		/// <summary>Adds a configuration element to the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to add.</param>
		protected virtual void BaseAdd(ConfigurationElement element)
		{
			BaseAdd(element, ThrowOnDuplicate);
		}

		/// <summary>Adds a configuration element to the configuration element collection.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to add.</param>
		/// <param name="throwIfExists">
		///   <see langword="true" /> to throw an exception if the <see cref="T:System.Configuration.ConfigurationElement" /> specified is already contained in the <see cref="T:System.Configuration.ConfigurationElementCollection" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.Exception">The <see cref="T:System.Configuration.ConfigurationElement" /> to add already exists in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> and the <paramref name="throwIfExists" /> parameter is <see langword="true" />.</exception>
		protected void BaseAdd(ConfigurationElement element, bool throwIfExists)
		{
			if (IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			if (IsAlternate)
			{
				list.Insert(inheritedLimitIndex, element);
				inheritedLimitIndex++;
			}
			else
			{
				int num = IndexOfKey(GetElementKey(element));
				if (num >= 0)
				{
					if (element.Equals(list[num]))
					{
						return;
					}
					if (throwIfExists)
					{
						throw new ConfigurationErrorsException("Duplicate element in collection");
					}
					list.RemoveAt(num);
				}
				list.Add(element);
			}
			modified = true;
		}

		/// <summary>Adds a configuration element to the configuration element collection.</summary>
		/// <param name="index">The index location at which to add the specified <see cref="T:System.Configuration.ConfigurationElement" />.</param>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to add.</param>
		protected virtual void BaseAdd(int index, ConfigurationElement element)
		{
			if (ThrowOnDuplicate && BaseIndexOf(element) != -1)
			{
				throw new ConfigurationErrorsException("Duplicate element in collection");
			}
			if (IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			if (IsAlternate && index > inheritedLimitIndex)
			{
				throw new ConfigurationErrorsException("Can't insert new elements below the inherited elements.");
			}
			if (!IsAlternate && index <= inheritedLimitIndex)
			{
				throw new ConfigurationErrorsException("Can't insert new elements above the inherited elements.");
			}
			list.Insert(index, element);
			modified = true;
		}

		/// <summary>Removes all configuration element objects from the collection.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration is read-only.  
		/// -or-
		///  A collection item has been locked in a higher-level configuration.</exception>
		protected internal void BaseClear()
		{
			if (IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			list.Clear();
			modified = true;
		}

		/// <summary>Gets the configuration element at the specified index location.</summary>
		/// <param name="index">The index location of the <see cref="T:System.Configuration.ConfigurationElement" /> to return.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElement" /> at the specified index.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="index" /> is less than <see langword="0" />.  
		/// -or-
		///  There is no <see cref="T:System.Configuration.ConfigurationElement" /> at the specified <paramref name="index" />.</exception>
		protected internal ConfigurationElement BaseGet(int index)
		{
			return (ConfigurationElement)list[index];
		}

		/// <summary>Returns the configuration element with the specified key.</summary>
		/// <param name="key">The key of the element to return.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElement" /> with the specified key; otherwise, <see langword="null" />.</returns>
		protected internal ConfigurationElement BaseGet(object key)
		{
			int num = IndexOfKey(key);
			if (num != -1)
			{
				return (ConfigurationElement)list[num];
			}
			return null;
		}

		/// <summary>Returns an array of the keys for all of the configuration elements contained in the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>An array that contains the keys for all of the <see cref="T:System.Configuration.ConfigurationElement" /> objects contained in the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</returns>
		protected internal object[] BaseGetAllKeys()
		{
			object[] array = new object[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				array[i] = BaseGetKey(i);
			}
			return array;
		}

		/// <summary>Gets the key for the <see cref="T:System.Configuration.ConfigurationElement" /> at the specified index location.</summary>
		/// <param name="index">The index location for the <see cref="T:System.Configuration.ConfigurationElement" />.</param>
		/// <returns>The key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="index" /> is less than <see langword="0" />.  
		/// -or-
		///  There is no <see cref="T:System.Configuration.ConfigurationElement" /> at the specified <paramref name="index" />.</exception>
		protected internal object BaseGetKey(int index)
		{
			if (index < 0 || index >= list.Count)
			{
				throw new ConfigurationErrorsException($"Index {index} is out of range");
			}
			return GetElementKey((ConfigurationElement)list[index]).ToString();
		}

		/// <summary>Indicates the index of the specified <see cref="T:System.Configuration.ConfigurationElement" />.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> for the specified index location.</param>
		/// <returns>The index of the specified <see cref="T:System.Configuration.ConfigurationElement" />; otherwise, -1.</returns>
		protected int BaseIndexOf(ConfigurationElement element)
		{
			return list.IndexOf(element);
		}

		private int IndexOfKey(object key)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (CompareKeys(GetElementKey((ConfigurationElement)list[i]), key))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Indicates whether the <see cref="T:System.Configuration.ConfigurationElement" /> with the specified key has been removed from the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <param name="key">The key of the element to check.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationElement" /> with the specified key has been removed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		protected internal bool BaseIsRemoved(object key)
		{
			if (removed == null)
			{
				return false;
			}
			foreach (ConfigurationElement item in removed)
			{
				if (CompareKeys(GetElementKey(item), key))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Removes a <see cref="T:System.Configuration.ConfigurationElement" /> from the collection.</summary>
		/// <param name="key">The key of the <see cref="T:System.Configuration.ConfigurationElement" /> to remove.</param>
		/// <exception cref="T:System.Exception">No <see cref="T:System.Configuration.ConfigurationElement" /> with the specified key exists in the collection, the element has already been removed, or the element cannot be removed because the value of its <see cref="P:System.Configuration.ConfigurationProperty.Type" /> is not <see cref="F:System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMap" />.</exception>
		protected internal void BaseRemove(object key)
		{
			if (IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			int num = IndexOfKey(key);
			if (num != -1)
			{
				BaseRemoveAt(num);
				modified = true;
			}
		}

		/// <summary>Removes the <see cref="T:System.Configuration.ConfigurationElement" /> at the specified index location.</summary>
		/// <param name="index">The index location of the <see cref="T:System.Configuration.ConfigurationElement" /> to remove.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration is read-only.  
		/// -or-
		///  <paramref name="index" /> is less than <see langword="0" /> or greater than the number of <see cref="T:System.Configuration.ConfigurationElement" /> objects in the collection.  
		/// -or-
		///  The <see cref="T:System.Configuration.ConfigurationElement" /> object has already been removed.  
		/// -or-
		///  The value of the <see cref="T:System.Configuration.ConfigurationElement" /> object has been locked at a higher level.  
		/// -or-
		///  The <see cref="T:System.Configuration.ConfigurationElement" /> object was inherited.  
		/// -or-
		///  The value of the <see cref="T:System.Configuration.ConfigurationElement" /> object's <see cref="P:System.Configuration.ConfigurationProperty.Type" /> is not <see cref="F:System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMap" /> or <see cref="F:System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMapAlternate" />.</exception>
		protected internal void BaseRemoveAt(int index)
		{
			if (IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			ConfigurationElement configurationElement = (ConfigurationElement)list[index];
			if (!IsElementRemovable(configurationElement))
			{
				throw new ConfigurationErrorsException("Element can't be removed from element collection.");
			}
			if (inherited != null && inherited.Contains(configurationElement))
			{
				throw new ConfigurationErrorsException("Inherited items can't be removed.");
			}
			list.RemoveAt(index);
			if (IsAlternate && inheritedLimitIndex > 0)
			{
				inheritedLimitIndex--;
			}
			modified = true;
		}

		private bool CompareKeys(object key1, object key2)
		{
			if (comparer != null)
			{
				return comparer.Compare(key1, key2) == 0;
			}
			return object.Equals(key1, key2);
		}

		/// <summary>Copies the contents of the <see cref="T:System.Configuration.ConfigurationElementCollection" /> to an array.</summary>
		/// <param name="array">Array to which to copy the contents of the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</param>
		/// <param name="index">Index location at which to begin copying.</param>
		public void CopyTo(ConfigurationElement[] array, int index)
		{
			list.CopyTo(array, index);
		}

		/// <summary>When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.</summary>
		/// <returns>A newly created <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		protected abstract ConfigurationElement CreateNewElement();

		/// <summary>Creates a new <see cref="T:System.Configuration.ConfigurationElement" /> when overridden in a derived class.</summary>
		/// <param name="elementName">The name of the <see cref="T:System.Configuration.ConfigurationElement" /> to create.</param>
		/// <returns>A new <see cref="T:System.Configuration.ConfigurationElement" /> with a specified name.</returns>
		protected virtual ConfigurationElement CreateNewElement(string elementName)
		{
			return CreateNewElement();
		}

		private ConfigurationElement CreateNewElementInternal(string elementName)
		{
			ConfigurationElement configurationElement = ((elementName != null) ? CreateNewElement(elementName) : CreateNewElement());
			configurationElement.Init();
			return configurationElement;
		}

		/// <summary>Compares the <see cref="T:System.Configuration.ConfigurationElementCollection" /> to the specified object.</summary>
		/// <param name="compareTo">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the object to compare with is equal to the current <see cref="T:System.Configuration.ConfigurationElementCollection" /> instance; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public override bool Equals(object compareTo)
		{
			if (!(compareTo is ConfigurationElementCollection configurationElementCollection))
			{
				return false;
			}
			if (GetType() != configurationElementCollection.GetType())
			{
				return false;
			}
			if (Count != configurationElementCollection.Count)
			{
				return false;
			}
			for (int i = 0; i < Count; i++)
			{
				if (!BaseGet(i).Equals(configurationElementCollection.BaseGet(i)))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets the element key for a specified configuration element when overridden in a derived class.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
		/// <returns>An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		protected abstract object GetElementKey(ConfigurationElement element);

		/// <summary>Gets a unique value representing the <see cref="T:System.Configuration.ConfigurationElementCollection" /> instance.</summary>
		/// <returns>A unique value representing the <see cref="T:System.Configuration.ConfigurationElementCollection" /> current instance.</returns>
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < Count; i++)
			{
				num += BaseGet(i).GetHashCode();
			}
			return num;
		}

		/// <summary>Copies the <see cref="T:System.Configuration.ConfigurationElementCollection" /> to an array.</summary>
		/// <param name="arr">Array to which to copy this <see cref="T:System.Configuration.ConfigurationElementCollection" />.</param>
		/// <param name="index">Index location at which to begin copying.</param>
		void ICollection.CopyTo(Array arr, int index)
		{
			list.CopyTo(arr, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> which is used to iterate through the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> which is used to iterate through the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</returns>
		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Configuration.ConfigurationElement" /> exists in the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <param name="elementName">The name of the element to verify.</param>
		/// <returns>
		///   <see langword="true" /> if the element exists in the collection; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		protected virtual bool IsElementName(string elementName)
		{
			return false;
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Configuration.ConfigurationElement" /> can be removed from the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <param name="element">The element to check.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Configuration.ConfigurationElement" /> can be removed from this <see cref="T:System.Configuration.ConfigurationElementCollection" />; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		protected virtual bool IsElementRemovable(ConfigurationElement element)
		{
			return !IsReadOnly();
		}

		/// <summary>Indicates whether this <see cref="T:System.Configuration.ConfigurationElementCollection" /> has been modified since it was last saved or loaded when overridden in a derived class.</summary>
		/// <returns>
		///   <see langword="true" /> if any contained element has been modified; otherwise, <see langword="false" /></returns>
		protected internal override bool IsModified()
		{
			if (modified)
			{
				return true;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (((ConfigurationElement)list[i]).IsModified())
				{
					modified = true;
					break;
				}
			}
			return modified;
		}

		/// <summary>Indicates whether the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object is read only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object is read only; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public override bool IsReadOnly()
		{
			return base.IsReadOnly();
		}

		internal override void PrepareSave(ConfigurationElement parentElement, ConfigurationSaveMode mode)
		{
			ConfigurationElementCollection configurationElementCollection = (ConfigurationElementCollection)parentElement;
			base.PrepareSave(parentElement, mode);
			for (int i = 0; i < list.Count; i++)
			{
				ConfigurationElement configurationElement = (ConfigurationElement)list[i];
				object elementKey = GetElementKey(configurationElement);
				ConfigurationElement parent = configurationElementCollection?.BaseGet(elementKey);
				configurationElement.PrepareSave(parent, mode);
			}
		}

		internal override bool HasValues(ConfigurationElement parentElement, ConfigurationSaveMode mode)
		{
			ConfigurationElementCollection configurationElementCollection = (ConfigurationElementCollection)parentElement;
			if (mode == ConfigurationSaveMode.Full)
			{
				return list.Count > 0;
			}
			for (int i = 0; i < list.Count; i++)
			{
				ConfigurationElement configurationElement = (ConfigurationElement)list[i];
				object elementKey = GetElementKey(configurationElement);
				ConfigurationElement parent = configurationElementCollection?.BaseGet(elementKey);
				if (configurationElement.HasValues(parent, mode))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Resets the <see cref="T:System.Configuration.ConfigurationElementCollection" /> to its unmodified state when overridden in a derived class.</summary>
		/// <param name="parentElement">The <see cref="T:System.Configuration.ConfigurationElement" /> representing the collection parent element, if any; otherwise, <see langword="null" />.</param>
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			bool isBasic = IsBasic;
			ConfigurationElementCollection configurationElementCollection = (ConfigurationElementCollection)parentElement;
			for (int i = 0; i < configurationElementCollection.Count; i++)
			{
				ConfigurationElement parentElement2 = configurationElementCollection.BaseGet(i);
				ConfigurationElement configurationElement = CreateNewElementInternal(null);
				configurationElement.Reset(parentElement2);
				BaseAdd(configurationElement);
				if (isBasic)
				{
					if (inherited == null)
					{
						inherited = new ArrayList();
					}
					inherited.Add(configurationElement);
				}
			}
			if (IsAlternate)
			{
				inheritedLimitIndex = 0;
			}
			else
			{
				inheritedLimitIndex = Count - 1;
			}
			modified = false;
		}

		/// <summary>Resets the value of the <see cref="M:System.Configuration.ConfigurationElementCollection.IsModified" /> property to <see langword="false" /> when overridden in a derived class.</summary>
		protected internal override void ResetModified()
		{
			modified = false;
			for (int i = 0; i < list.Count; i++)
			{
				((ConfigurationElement)list[i]).ResetModified();
			}
		}

		/// <summary>Sets the <see cref="M:System.Configuration.ConfigurationElementCollection.IsReadOnly" /> property for the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object and for all sub-elements.</summary>
		[System.MonoTODO]
		protected internal override void SetReadOnly()
		{
			base.SetReadOnly();
		}

		/// <summary>Writes the configuration data to an XML element in the configuration file when overridden in a derived class.</summary>
		/// <param name="writer">Output stream that writes XML to the configuration file.</param>
		/// <param name="serializeCollectionKey">
		///   <see langword="true" /> to serialize the collection key; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationElementCollection" /> was written to the configuration file successfully.</returns>
		/// <exception cref="T:System.ArgumentException">One of the elements in the collection was added or replaced and starts with the reserved prefix "config" or "lock".</exception>
		protected internal override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			if (serializeCollectionKey)
			{
				return base.SerializeElement(writer, serializeCollectionKey);
			}
			bool flag = false;
			if (IsBasic)
			{
				for (int i = 0; i < list.Count; i++)
				{
					ConfigurationElement configurationElement = (ConfigurationElement)list[i];
					flag = ((!(ElementName != string.Empty)) ? (configurationElement.SerializeElement(writer, serializeCollectionKey: false) || flag) : (configurationElement.SerializeToXmlElement(writer, ElementName) || flag));
				}
			}
			else
			{
				if (emitClear)
				{
					writer.WriteElementString(clearElementName, "");
					flag = true;
				}
				if (removed != null)
				{
					for (int j = 0; j < removed.Count; j++)
					{
						writer.WriteStartElement(removeElementName);
						((ConfigurationElement)removed[j]).SerializeElement(writer, serializeCollectionKey: true);
						writer.WriteEndElement();
					}
					flag = flag || removed.Count > 0;
				}
				for (int k = 0; k < list.Count; k++)
				{
					((ConfigurationElement)list[k]).SerializeToXmlElement(writer, addElementName);
				}
				flag = flag || list.Count > 0;
			}
			return flag;
		}

		/// <summary>Causes the configuration system to throw an exception.</summary>
		/// <param name="elementName">The name of the unrecognized element.</param>
		/// <param name="reader">An input stream that reads XML from the configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the unrecognized element was deserialized successfully; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element specified in <paramref name="elementName" /> is the <see langword="&lt;clear&gt;" /> element.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementName" /> starts with the reserved prefix "config" or "lock".</exception>
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			if (IsBasic)
			{
				ConfigurationElement configurationElement = null;
				if (elementName == ElementName)
				{
					configurationElement = CreateNewElementInternal(null);
				}
				if (IsElementName(elementName))
				{
					configurationElement = CreateNewElementInternal(elementName);
				}
				if (configurationElement != null)
				{
					configurationElement.DeserializeElement(reader, serializeCollectionKey: false);
					BaseAdd(configurationElement);
					modified = false;
					return true;
				}
			}
			else
			{
				if (elementName == clearElementName)
				{
					reader.MoveToContent();
					if (reader.MoveToNextAttribute())
					{
						throw new ConfigurationErrorsException("Unrecognized attribute '" + reader.LocalName + "'.");
					}
					reader.MoveToElement();
					reader.Skip();
					BaseClear();
					emitClear = true;
					modified = false;
					return true;
				}
				if (elementName == removeElementName)
				{
					ConfigurationRemoveElement configurationRemoveElement = new ConfigurationRemoveElement(CreateNewElementInternal(null), this);
					configurationRemoveElement.DeserializeElement(reader, serializeCollectionKey: true);
					BaseRemove(configurationRemoveElement.KeyValue);
					modified = false;
					return true;
				}
				if (elementName == addElementName)
				{
					ConfigurationElement configurationElement2 = CreateNewElementInternal(null);
					configurationElement2.DeserializeElement(reader, serializeCollectionKey: false);
					BaseAdd(configurationElement2);
					modified = false;
					return true;
				}
			}
			return false;
		}

		/// <summary>Reverses the effect of merging configuration information from different levels of the configuration hierarchy.</summary>
		/// <param name="sourceElement">A <see cref="T:System.Configuration.ConfigurationElement" /> object at the current level containing a merged view of the properties.</param>
		/// <param name="parentElement">The parent <see cref="T:System.Configuration.ConfigurationElement" /> object of the current element, or <see langword="null" /> if this is the top level.</param>
		/// <param name="saveMode">One of the enumeration value that determines which property values to include.</param>
		protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			ConfigurationElementCollection configurationElementCollection = (ConfigurationElementCollection)sourceElement;
			ConfigurationElementCollection configurationElementCollection2 = (ConfigurationElementCollection)parentElement;
			for (int i = 0; i < configurationElementCollection.Count; i++)
			{
				ConfigurationElement configurationElement = configurationElementCollection.BaseGet(i);
				object elementKey = configurationElementCollection.GetElementKey(configurationElement);
				ConfigurationElement configurationElement2 = configurationElementCollection2?.BaseGet(elementKey);
				ConfigurationElement configurationElement3 = CreateNewElementInternal(null);
				if (configurationElement2 != null && saveMode != ConfigurationSaveMode.Full)
				{
					configurationElement3.Unmerge(configurationElement, configurationElement2, saveMode);
					if (configurationElement3.HasValues(configurationElement2, saveMode))
					{
						BaseAdd(configurationElement3);
					}
				}
				else
				{
					configurationElement3.Unmerge(configurationElement, null, ConfigurationSaveMode.Full);
					BaseAdd(configurationElement3);
				}
			}
			if (saveMode == ConfigurationSaveMode.Full)
			{
				EmitClear = true;
			}
			else
			{
				if (configurationElementCollection2 == null)
				{
					return;
				}
				for (int j = 0; j < configurationElementCollection2.Count; j++)
				{
					ConfigurationElement configurationElement4 = configurationElementCollection2.BaseGet(j);
					object elementKey2 = configurationElementCollection2.GetElementKey(configurationElement4);
					if (configurationElementCollection.IndexOfKey(elementKey2) == -1)
					{
						if (removed == null)
						{
							removed = new ArrayList();
						}
						removed.Add(configurationElement4);
					}
				}
			}
		}
	}
	/// <summary>Specifies the type of a <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object.</summary>
	public enum ConfigurationElementCollectionType
	{
		/// <summary>Collections of this type contain elements that apply to the level at which they are specified, and to all child levels. A child level cannot modify the properties specified by a parent element of this type.</summary>
		BasicMap,
		/// <summary>The default type of <see cref="T:System.Configuration.ConfigurationElementCollection" />. Collections of this type contain elements that can be merged across a hierarchy of configuration files. At any particular level within such a hierarchy, <see langword="add" />, <see langword="remove" />, and <see langword="clear" /> directives are used to modify any inherited properties and specify new ones.</summary>
		AddRemoveClearMap,
		/// <summary>Same as <see cref="F:System.Configuration.ConfigurationElementCollectionType.BasicMap" />, except that this type causes the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object to sort its contents such that inherited elements are listed last.</summary>
		BasicMapAlternate,
		/// <summary>Same as <see cref="F:System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMap" />, except that this type causes the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object to sort its contents such that inherited elements are listed last.</summary>
		AddRemoveClearMapAlternate
	}
	/// <summary>Specifies the property of a configuration element. This class cannot be inherited.</summary>
	public sealed class ConfigurationElementProperty
	{
		private ConfigurationValidatorBase validator;

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object used to validate the <see cref="T:System.Configuration.ConfigurationElementProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object.</returns>
		public ConfigurationValidatorBase Validator => validator;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationElementProperty" /> class, based on a supplied parameter.</summary>
		/// <param name="validator">A <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="validator" /> is <see langword="null" />.</exception>
		public ConfigurationElementProperty(ConfigurationValidatorBase validator)
		{
			this.validator = validator;
		}
	}
	/// <summary>The exception that is thrown when a configuration error has occurred.</summary>
	[Serializable]
	public class ConfigurationErrorsException : ConfigurationException
	{
		private readonly string filename;

		private readonly int line;

		/// <summary>Gets a description of why this configuration exception was thrown.</summary>
		/// <returns>A description of why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> was thrown.</returns>
		public override string BareMessage => base.BareMessage;

		/// <summary>Gets a collection of errors that detail the reasons this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object that contains errors that identify the reasons this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</returns>
		public ICollection Errors
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the path to the configuration file that caused this configuration exception to be thrown.</summary>
		/// <returns>The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> to be thrown.</returns>
		public override string Filename => filename;

		/// <summary>Gets the line number within the configuration file at which this configuration exception was thrown.</summary>
		/// <returns>The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</returns>
		public override int Line => line;

		/// <summary>Gets an extended description of why this configuration exception was thrown.</summary>
		/// <returns>An extended description of why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</returns>
		public override string Message
		{
			get
			{
				if (!string.IsNullOrEmpty(filename))
				{
					if (line != 0)
					{
						return BareMessage + " (" + filename + " line " + line + ")";
					}
					return BareMessage + " (" + filename + ")";
				}
				if (line != 0)
				{
					return BareMessage + " (line " + line + ")";
				}
				return BareMessage;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		public ConfigurationErrorsException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		public ConfigurationErrorsException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="info">The object that holds the information to deserialize.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		/// <exception cref="T:System.InvalidOperationException">The current type is not a <see cref="T:System.Configuration.ConfigurationException" /> or a <see cref="T:System.Configuration.ConfigurationErrorsException" />.</exception>
		protected ConfigurationErrorsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			filename = info.GetString("ConfigurationErrors_Filename");
			line = info.GetInt32("ConfigurationErrors_Line");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="inner">The exception that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		public ConfigurationErrorsException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		public ConfigurationErrorsException(string message, XmlNode node)
			: this(message, null, GetFilename(node), GetLineNumber(node))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		public ConfigurationErrorsException(string message, Exception inner, XmlNode node)
			: this(message, inner, GetFilename(node), GetLineNumber(node))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		public ConfigurationErrorsException(string message, XmlReader reader)
			: this(message, null, GetFilename(reader), GetLineNumber(reader))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		public ConfigurationErrorsException(string message, Exception inner, XmlReader reader)
			: this(message, inner, GetFilename(reader), GetLineNumber(reader))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="filename">The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="line">The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		public ConfigurationErrorsException(string message, string filename, int line)
			: this(message, null, filename, line)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="filename">The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="line">The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		public ConfigurationErrorsException(string message, Exception inner, string filename, int line)
			: base(message, inner)
		{
			this.filename = filename;
			this.line = line;
		}

		/// <summary>Gets the path to the configuration file that the internal <see cref="T:System.Xml.XmlReader" /> was reading when this configuration exception was thrown.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <returns>The path of the configuration file the internal <see cref="T:System.Xml.XmlReader" /> object was accessing when the exception occurred.</returns>
		public static string GetFilename(XmlReader reader)
		{
			if (reader is IConfigErrorInfo)
			{
				return ((IConfigErrorInfo)reader).Filename;
			}
			return reader?.BaseURI;
		}

		/// <summary>Gets the line number within the configuration file that the internal <see cref="T:System.Xml.XmlReader" /> object was processing when this configuration exception was thrown.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <returns>The line number within the configuration file that the <see cref="T:System.Xml.XmlReader" /> object was accessing when the exception occurred.</returns>
		public static int GetLineNumber(XmlReader reader)
		{
			if (reader is IConfigErrorInfo)
			{
				return ((IConfigErrorInfo)reader).LineNumber;
			}
			if (!(reader is IXmlLineInfo xmlLineInfo))
			{
				return 0;
			}
			return xmlLineInfo.LineNumber;
		}

		/// <summary>Gets the path to the configuration file from which the internal <see cref="T:System.Xml.XmlNode" /> object was loaded when this configuration exception was thrown.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <returns>The path to the configuration file from which the internal <see cref="T:System.Xml.XmlNode" /> object was loaded when this configuration exception was thrown.</returns>
		public static string GetFilename(XmlNode node)
		{
			if (!(node is IConfigErrorInfo))
			{
				return null;
			}
			return ((IConfigErrorInfo)node).Filename;
		}

		/// <summary>Gets the line number within the configuration file that the internal <see cref="T:System.Xml.XmlNode" /> object represented when this configuration exception was thrown.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <returns>The line number within the configuration file that contains the <see cref="T:System.Xml.XmlNode" /> object being parsed when this configuration exception was thrown.</returns>
		public static int GetLineNumber(XmlNode node)
		{
			if (!(node is IConfigErrorInfo))
			{
				return 0;
			}
			return ((IConfigErrorInfo)node).LineNumber;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and line number at which this configuration exception occurred.</summary>
		/// <param name="info">The object that holds the information to be serialized.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ConfigurationErrors_Filename", filename);
			info.AddValue("ConfigurationErrors_Line", line);
		}
	}
	/// <summary>Defines the configuration file mapping for the machine configuration file.</summary>
	public class ConfigurationFileMap : ICloneable
	{
		private string machineConfigFilename;

		/// <summary>Gets or sets the name of the machine configuration file name.</summary>
		/// <returns>The machine configuration file name.</returns>
		public string MachineConfigFilename
		{
			get
			{
				return machineConfigFilename;
			}
			set
			{
				machineConfigFilename = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationFileMap" /> class.</summary>
		public ConfigurationFileMap()
		{
			machineConfigFilename = RuntimeEnvironment.SystemConfigurationFile;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationFileMap" /> class based on the supplied parameter.</summary>
		/// <param name="machineConfigFilename">The name of the machine configuration file.</param>
		public ConfigurationFileMap(string machineConfigFilename)
		{
			this.machineConfigFilename = machineConfigFilename;
		}

		/// <summary>Creates a copy of the existing <see cref="T:System.Configuration.ConfigurationFileMap" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationFileMap" /> object.</returns>
		public virtual object Clone()
		{
			return new ConfigurationFileMap(machineConfigFilename);
		}
	}
	/// <summary>Represents a <see langword="location" /> element within a configuration file.</summary>
	public class ConfigurationLocation
	{
		private static readonly char[] pathTrimChars = new char[1] { '/' };

		private string path;

		private Configuration configuration;

		private Configuration parent;

		private string xmlContent;

		private bool parentResolved;

		private bool allowOverride;

		/// <summary>Gets the relative path to the resource whose configuration settings are represented by this <see cref="T:System.Configuration.ConfigurationLocation" /> object.</summary>
		/// <returns>The relative path to the resource whose configuration settings are represented by this <see cref="T:System.Configuration.ConfigurationLocation" />.</returns>
		public string Path => path;

		internal bool AllowOverride => allowOverride;

		internal string XmlContent => xmlContent;

		internal Configuration OpenedConfiguration => configuration;

		internal ConfigurationLocation()
		{
		}

		internal ConfigurationLocation(string path, string xmlContent, Configuration parent, bool allowOverride)
		{
			if (!string.IsNullOrEmpty(path))
			{
				switch (path[0])
				{
				case ' ':
				case '.':
				case '/':
				case '\\':
					throw new ConfigurationErrorsException("<location> path attribute must be a relative virtual path.  It cannot start with any of ' ' '.' '/' or '\\'.");
				}
				path = path.TrimEnd(pathTrimChars);
			}
			this.path = path;
			this.xmlContent = xmlContent;
			this.parent = parent;
			this.allowOverride = allowOverride;
		}

		/// <summary>Creates an instance of a Configuration object.</summary>
		/// <returns>A Configuration object.</returns>
		public Configuration OpenConfiguration()
		{
			if (configuration == null)
			{
				if (!parentResolved)
				{
					Configuration parentWithFile = parent.GetParentWithFile();
					if (parentWithFile != null)
					{
						string configPathFromLocationSubPath = parent.ConfigHost.GetConfigPathFromLocationSubPath(parent.LocationConfigPath, path);
						parent = parentWithFile.FindLocationConfiguration(configPathFromLocationSubPath, parent);
					}
				}
				configuration = new Configuration(parent, path);
				using (XmlTextReader reader = new ConfigXmlTextReader(new StringReader(xmlContent), path))
				{
					configuration.ReadData(reader, allowOverride);
				}
				xmlContent = null;
			}
			return configuration;
		}

		internal void SetParentConfiguration(Configuration parent)
		{
			if (!parentResolved)
			{
				parentResolved = true;
				this.parent = parent;
				if (configuration != null)
				{
					configuration.Parent = parent;
				}
			}
		}
	}
	/// <summary>Contains a collection of <see cref="T:System.Configuration.ConfigurationLocationCollection" /> objects.</summary>
	public class ConfigurationLocationCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationLocationCollection" /> object at the specified index.</summary>
		/// <param name="index">The index location of the <see cref="T:System.Configuration.ConfigurationLocationCollection" /> to return.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLocationCollection" /> at the specified index.</returns>
		public ConfigurationLocation this[int index] => base.InnerList[index] as ConfigurationLocation;

		internal ConfigurationLocationCollection()
		{
		}

		internal void Add(ConfigurationLocation loc)
		{
			base.InnerList.Add(loc);
		}

		internal ConfigurationLocation Find(string location)
		{
			foreach (ConfigurationLocation inner in base.InnerList)
			{
				if (string.Compare(inner.Path, location, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return inner;
				}
			}
			return null;
		}

		internal ConfigurationLocation FindBest(string location)
		{
			if (string.IsNullOrEmpty(location))
			{
				return null;
			}
			ConfigurationLocation configurationLocation = null;
			int length = location.Length;
			int num = 0;
			foreach (ConfigurationLocation inner in base.InnerList)
			{
				string path = inner.Path;
				if (string.IsNullOrEmpty(path))
				{
					continue;
				}
				int length2 = path.Length;
				if (!location.StartsWith(path, StringComparison.OrdinalIgnoreCase))
				{
					continue;
				}
				if (length == length2)
				{
					return inner;
				}
				if (length <= length2 || location[length2] == '/')
				{
					if (configurationLocation == null)
					{
						configurationLocation = inner;
					}
					else if (num < length2)
					{
						configurationLocation = inner;
						num = length2;
					}
				}
			}
			return configurationLocation;
		}
	}
	[Flags]
	internal enum ConfigurationLockType
	{
		Attribute = 1,
		Element = 2,
		Exclude = 0x10
	}
	/// <summary>Contains a collection of locked configuration objects. This class cannot be inherited.</summary>
	public sealed class ConfigurationLockCollection : ICollection, IEnumerable
	{
		private ArrayList names;

		private ConfigurationElement element;

		private ConfigurationLockType lockType;

		private bool is_modified;

		private Hashtable valid_name_hash;

		private string valid_names;

		/// <summary>Gets a list of configuration objects contained in the collection.</summary>
		/// <returns>A comma-delimited string that lists the lock configuration objects in the collection.</returns>
		public string AttributeList
		{
			get
			{
				string[] array = new string[names.Count];
				names.CopyTo(array, 0);
				return string.Join(",", array);
			}
		}

		/// <summary>Gets the number of locked configuration objects contained in the collection.</summary>
		/// <returns>The number of locked configuration objects contained in the collection.</returns>
		public int Count => names.Count;

		/// <summary>Gets a value specifying whether the collection of locked objects has parent elements.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection has parent elements; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool HasParentElements => false;

		/// <summary>Gets a value specifying whether the collection has been modified.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection has been modified; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool IsModified
		{
			get
			{
				return is_modified;
			}
			internal set
			{
				is_modified = value;
			}
		}

		/// <summary>Gets a value specifying whether the collection is synchronized.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection is synchronized; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool IsSynchronized => false;

		/// <summary>Gets an object used to synchronize access to this <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection.</summary>
		/// <returns>An object used to synchronize access to this <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection.</returns>
		[System.MonoTODO]
		public object SyncRoot => this;

		internal ConfigurationLockCollection(ConfigurationElement element, ConfigurationLockType lockType)
		{
			names = new ArrayList();
			this.element = element;
			this.lockType = lockType;
		}

		private void CheckName(string name)
		{
			bool flag = (lockType & ConfigurationLockType.Attribute) == ConfigurationLockType.Attribute;
			if (valid_name_hash == null)
			{
				valid_name_hash = new Hashtable();
				foreach (ConfigurationProperty property in element.Properties)
				{
					if (flag != property.IsElement)
					{
						valid_name_hash.Add(property.Name, true);
					}
				}
				if (!flag)
				{
					ConfigurationElementCollection defaultCollection = element.GetDefaultCollection();
					valid_name_hash.Add(defaultCollection.AddElementName, true);
					valid_name_hash.Add(defaultCollection.ClearElementName, true);
					valid_name_hash.Add(defaultCollection.RemoveElementName, true);
				}
				string[] array = new string[valid_name_hash.Keys.Count];
				valid_name_hash.Keys.CopyTo(array, 0);
				valid_names = string.Join(",", array);
			}
			if (valid_name_hash[name] == null)
			{
				throw new ConfigurationErrorsException(string.Format("The {2} '{0}' is not valid in the locked list for this section.  The following {3} can be locked: '{1}'", name, valid_names, flag ? "attribute" : "element", flag ? "attributes" : "elements"));
			}
		}

		/// <summary>Locks a configuration object by adding it to the collection.</summary>
		/// <param name="name">The name of the configuration object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs when the <paramref name="name" /> does not match an existing configuration object within the collection.</exception>
		public void Add(string name)
		{
			CheckName(name);
			if (!names.Contains(name))
			{
				names.Add(name);
				is_modified = true;
			}
		}

		/// <summary>Clears all configuration objects from the collection.</summary>
		public void Clear()
		{
			names.Clear();
			is_modified = true;
		}

		/// <summary>Verifies whether a specific configuration object is locked.</summary>
		/// <param name="name">The name of the configuration object to verify.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationLockCollection" /> contains the specified configuration object; otherwise, <see langword="false" />.</returns>
		public bool Contains(string name)
		{
			return names.Contains(name);
		}

		/// <summary>Copies the entire <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Configuration.ConfigurationLockCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		public void CopyTo(string[] array, int index)
		{
			names.CopyTo(array, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> object, which is used to iterate through this <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object.</returns>
		public IEnumerator GetEnumerator()
		{
			return names.GetEnumerator();
		}

		/// <summary>Verifies whether a specific configuration object is read-only.</summary>
		/// <param name="name">The name of the configuration object to verify.</param>
		/// <returns>
		///   <see langword="true" /> if the specified configuration object in the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection is read-only; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The specified configuration object is not in the collection.</exception>
		[System.MonoInternalNote("we can't possibly *always* return false here...")]
		public bool IsReadOnly(string name)
		{
			for (int i = 0; i < names.Count; i++)
			{
				if ((string)names[i] == name)
				{
					return false;
				}
			}
			throw new ConfigurationErrorsException($"The entry '{name}' is not in the collection.");
		}

		/// <summary>Removes a configuration object from the collection.</summary>
		/// <param name="name">The name of the configuration object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs when the <paramref name="name" /> does not match an existing configuration object within the collection.</exception>
		public void Remove(string name)
		{
			names.Remove(name);
			is_modified = true;
		}

		/// <summary>Locks a set of configuration objects based on the supplied list.</summary>
		/// <param name="attributeList">A comma-delimited string.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs when an item in the <paramref name="attributeList" /> parameter is not a valid lockable configuration attribute.</exception>
		public void SetFromList(string attributeList)
		{
			Clear();
			char[] separator = new char[1] { ',' };
			string[] array = attributeList.Split(separator);
			foreach (string text in array)
			{
				Add(text.Trim());
			}
		}

		/// <summary>Copies the entire <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		void ICollection.CopyTo(Array array, int index)
		{
			names.CopyTo(array, index);
		}

		internal ConfigurationLockCollection()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Provides access to configuration files for client applications. This class cannot be inherited.</summary>
	public static class ConfigurationManager
	{
		private static InternalConfigurationFactory configFactory = new InternalConfigurationFactory();

		private static IInternalConfigSystem configSystem = new ClientConfigurationSystem();

		private static object lockobj = new object();

		internal static IInternalConfigConfigurationFactory ConfigurationFactory => configFactory;

		internal static IInternalConfigSystem ConfigurationSystem => configSystem;

		/// <summary>Gets the <see cref="T:System.Configuration.AppSettingsSection" /> data for the current application's default configuration.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains the contents of the <see cref="T:System.Configuration.AppSettingsSection" /> object for the current application's default configuration.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Could not retrieve a <see cref="T:System.Collections.Specialized.NameValueCollection" /> object with the application settings data.</exception>
		public static NameValueCollection AppSettings => (NameValueCollection)GetSection("appSettings");

		/// <summary>Gets the <see cref="T:System.Configuration.ConnectionStringsSection" /> data for the current application's default configuration.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> object that contains the contents of the <see cref="T:System.Configuration.ConnectionStringsSection" /> object for the current application's default configuration.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Could not retrieve a <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> object.</exception>
		public static ConnectionStringSettingsCollection ConnectionStrings => ((ConnectionStringsSection)GetSection("connectionStrings")).ConnectionStrings;

		[System.MonoTODO("Evidence and version still needs work")]
		private static string GetAssemblyInfo(Assembly a)
		{
			object[] customAttributes = a.GetCustomAttributes(typeof(AssemblyProductAttribute), inherit: false);
			string arg = ((customAttributes == null || customAttributes.Length == 0) ? AppDomain.CurrentDomain.FriendlyName : ((AssemblyProductAttribute)customAttributes[0]).Product);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("evidencehere");
			string arg2 = stringBuilder.ToString();
			customAttributes = a.GetCustomAttributes(typeof(AssemblyVersionAttribute), inherit: false);
			return Path.Combine(path2: (customAttributes == null || customAttributes.Length == 0) ? "1.0.0.0" : ((AssemblyVersionAttribute)customAttributes[0]).Version, path1: $"{arg}_{arg2}");
		}

		internal static Configuration OpenExeConfigurationInternal(ConfigurationUserLevel userLevel, Assembly calling_assembly, string exePath)
		{
			ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap();
			if (userLevel != ConfigurationUserLevel.None)
			{
				if (userLevel != ConfigurationUserLevel.PerUserRoaming)
				{
					if (userLevel != ConfigurationUserLevel.PerUserRoamingAndLocal)
					{
						goto IL_00ea;
					}
					exeConfigurationFileMap.LocalUserConfigFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), GetAssemblyInfo(calling_assembly));
					exeConfigurationFileMap.LocalUserConfigFilename = Path.Combine(exeConfigurationFileMap.LocalUserConfigFilename, "user.config");
				}
				exeConfigurationFileMap.RoamingUserConfigFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GetAssemblyInfo(calling_assembly));
				exeConfigurationFileMap.RoamingUserConfigFilename = Path.Combine(exeConfigurationFileMap.RoamingUserConfigFilename, "user.config");
			}
			if (exePath == null || exePath.Length == 0)
			{
				exeConfigurationFileMap.ExeConfigFilename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			}
			else
			{
				if (!Path.IsPathRooted(exePath))
				{
					exePath = Path.GetFullPath(exePath);
				}
				if (!File.Exists(exePath))
				{
					Exception inner = new ArgumentException("The specified path does not exist.", "exePath");
					throw new ConfigurationErrorsException("Error Initializing the configuration system:", inner);
				}
				exeConfigurationFileMap.ExeConfigFilename = exePath + ".config";
			}
			goto IL_00ea;
			IL_00ea:
			return ConfigurationFactory.Create(typeof(ExeConfigurationHost), exeConfigurationFileMap, userLevel);
		}

		/// <summary>Opens the configuration file for the current application as a <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <param name="userLevel">The <see cref="T:System.Configuration.ConfigurationUserLevel" /> for which you are opening the configuration.</param>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		public static Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel)
		{
			return OpenExeConfigurationInternal(userLevel, Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly(), null);
		}

		/// <summary>Opens the specified client configuration file as a <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <param name="exePath">The path of the executable (exe) file.</param>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		public static Configuration OpenExeConfiguration(string exePath)
		{
			return OpenExeConfigurationInternal(ConfigurationUserLevel.None, Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly(), exePath);
		}

		/// <summary>Opens the specified client configuration file as a <see cref="T:System.Configuration.Configuration" /> object that uses the specified file mapping and user level.</summary>
		/// <param name="fileMap">An <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object that references configuration file to use instead of the application default configuration file.</param>
		/// <param name="userLevel">The <see cref="T:System.Configuration.ConfigurationUserLevel" /> object for which you are opening the configuration.</param>
		/// <returns>The configuration object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		[System.MonoLimitation("ConfigurationUserLevel parameter is not supported.")]
		public static Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel)
		{
			return ConfigurationFactory.Create(typeof(ExeConfigurationHost), fileMap, userLevel);
		}

		/// <summary>Opens the machine configuration file on the current computer as a <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		public static Configuration OpenMachineConfiguration()
		{
			ConfigurationFileMap configurationFileMap = new ConfigurationFileMap();
			return ConfigurationFactory.Create(typeof(MachineConfigurationHost), configurationFileMap);
		}

		/// <summary>Opens the machine configuration file as a <see cref="T:System.Configuration.Configuration" /> object that uses the specified file mapping.</summary>
		/// <param name="fileMap">An <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object that references configuration file to use instead of the application default configuration file.</param>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap)
		{
			return ConfigurationFactory.Create(typeof(MachineConfigurationHost), fileMap);
		}

		/// <summary>Retrieves a specified configuration section for the current application's default configuration.</summary>
		/// <param name="sectionName">The configuration section path and name. Node names are separated by forward slashes, for example "system.net/mailSettings/smtp".</param>
		/// <returns>The specified <see cref="T:System.Configuration.ConfigurationSection" /> object, or <see langword="null" /> if the section does not exist.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		public static object GetSection(string sectionName)
		{
			object section = ConfigurationSystem.GetSection(sectionName);
			if (section is ConfigurationSection)
			{
				return ((ConfigurationSection)section).GetRuntimeObject();
			}
			return section;
		}

		/// <summary>Refreshes the named section so the next time that it is retrieved it will be re-read from disk.</summary>
		/// <param name="sectionName">The configuration section name or the configuration path and section name of the section to refresh.</param>
		public static void RefreshSection(string sectionName)
		{
			ConfigurationSystem.RefreshConfig(sectionName);
		}

		internal static IInternalConfigSystem ChangeConfigurationSystem(IInternalConfigSystem newSystem)
		{
			if (newSystem == null)
			{
				throw new ArgumentNullException("newSystem");
			}
			lock (lockobj)
			{
				IInternalConfigSystem result = configSystem;
				configSystem = newSystem;
				return result;
			}
		}

		/// <summary>Opens the specified client configuration file as a <see cref="T:System.Configuration.Configuration" /> object that uses the specified file mapping, user level, and preload option.</summary>
		/// <param name="fileMap">An <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object that references the configuration file to use instead of the default application configuration file.</param>
		/// <param name="userLevel">The <see cref="T:System.Configuration.ConfigurationUserLevel" /> object for which you are opening the configuration.</param>
		/// <param name="preLoad">
		///   <see langword="true" /> to preload all section groups and sections; otherwise, <see langword="false" />.</param>
		/// <returns>The configuration object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		public static Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel, bool preLoad)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
	/// <summary>Provides a permission structure that allows methods or classes to access configuration files.</summary>
	[Serializable]
	public sealed class ConfigurationPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		private bool unrestricted;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationPermission" /> class.</summary>
		/// <param name="state">The permission level to grant.</param>
		/// <exception cref="T:System.ArgumentException">The value of <paramref name="state" /> is neither <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /> nor <see cref="F:System.Security.Permissions.PermissionState.None" />.</exception>
		public ConfigurationPermission(PermissionState state)
		{
			unrestricted = state == PermissionState.Unrestricted;
		}

		/// <summary>Returns a new <see cref="T:System.Configuration.ConfigurationPermission" /> object with the same permission level.</summary>
		/// <returns>A new <see cref="T:System.Configuration.ConfigurationPermission" /> with the same permission level.</returns>
		public override IPermission Copy()
		{
			return new ConfigurationPermission(unrestricted ? PermissionState.Unrestricted : PermissionState.None);
		}

		/// <summary>Reads the value of the permission state from XML.</summary>
		/// <param name="securityElement">The configuration element from which the permission state is read.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see langword="unrestricted" /> attribute for the given <paramref name="securityElement" /> is neither <see langword="true" /> nor <see langword="false" />.
		/// -or-
		/// The <see cref="P:System.Security.SecurityElement.Tag" /> for the given <paramref name="securityElement" /> does not equal "IPermission".
		/// -or-
		/// The <see langword="class" /> attribute of the given <paramref name="securityElement " /> is <see langword="null" /> or is not the type name for <see cref="T:System.Configuration.ConfigurationPermission" />.
		/// -or-
		/// The <see langword="version" /> attribute for the given <paramref name="securityElement" /> does not equal 1.</exception>
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (securityElement.Tag != "IPermission")
			{
				throw new ArgumentException("securityElement");
			}
			string text = securityElement.Attribute("Unrestricted");
			if (text != null)
			{
				unrestricted = string.Compare(text, "true", StringComparison.InvariantCultureIgnoreCase) == 0;
			}
		}

		/// <summary>Returns the logical intersection between the <see cref="T:System.Configuration.ConfigurationPermission" /> object and a given object that implements the <see cref="T:System.Security.IPermission" /> interface.</summary>
		/// <param name="target">The object containing the permissions to perform the intersection with.</param>
		/// <returns>The logical intersection between the <see cref="T:System.Configuration.ConfigurationPermission" /> and a given object that implements <see cref="T:System.Security.IPermission" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not typed as <see cref="T:System.Configuration.ConfigurationPermission" />.</exception>
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (!(target is ConfigurationPermission configurationPermission))
			{
				throw new ArgumentException("target");
			}
			return new ConfigurationPermission((unrestricted && configurationPermission.IsUnrestricted()) ? PermissionState.Unrestricted : PermissionState.None);
		}

		/// <summary>Returns the logical union of the <see cref="T:System.Configuration.ConfigurationPermission" /> object and an object that implements the <see cref="T:System.Security.IPermission" /> interface.</summary>
		/// <param name="target">The object to perform the union with.</param>
		/// <returns>The logical union of the <see cref="T:System.Configuration.ConfigurationPermission" /> and an object that implements <see cref="T:System.Security.IPermission" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not typed as <see cref="T:System.Configuration.ConfigurationPermission" />.</exception>
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return Copy();
			}
			if (!(target is ConfigurationPermission configurationPermission))
			{
				throw new ArgumentException("target");
			}
			return new ConfigurationPermission((unrestricted || configurationPermission.IsUnrestricted()) ? PermissionState.Unrestricted : PermissionState.None);
		}

		/// <summary>Compares the <see cref="T:System.Configuration.ConfigurationPermission" /> object with an object implementing the <see cref="T:System.Security.IPermission" /> interface.</summary>
		/// <param name="target">The object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if the permission state is equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not typed as <see cref="T:System.Configuration.ConfigurationPermission" />.</exception>
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !unrestricted;
			}
			if (!(target is ConfigurationPermission configurationPermission))
			{
				throw new ArgumentException("target");
			}
			if (unrestricted)
			{
				return configurationPermission.IsUnrestricted();
			}
			return true;
		}

		/// <summary>Indicates whether the permission state for the <see cref="T:System.Configuration.ConfigurationPermission" /> object is the <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /> value of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration.</summary>
		/// <returns>
		///   <see langword="true" /> if the permission state for the <see cref="T:System.Configuration.ConfigurationPermission" /> is the <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /> value of <see cref="T:System.Security.Permissions.PermissionState" />; otherwise, <see langword="false" />.</returns>
		public bool IsUnrestricted()
		{
			return unrestricted;
		}

		/// <summary>Returns a <see cref="T:System.Security.SecurityElement" /> object with attribute values based on the current <see cref="T:System.Configuration.ConfigurationPermission" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> with attribute values based on the current <see cref="T:System.Configuration.ConfigurationPermission" />.</returns>
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", GetType().AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			if (unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}
	}
	/// <summary>Creates a <see cref="T:System.Configuration.ConfigurationPermission" /> object that either grants or denies the marked target permission to access sections of configuration files.</summary>
	[Serializable]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	public sealed class ConfigurationPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationPermissionAttribute" /> class.</summary>
		/// <param name="action">The security action represented by an enumeration member of <see cref="T:System.Security.Permissions.SecurityAction" />. Determines the permission state of the attribute.</param>
		public ConfigurationPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Creates and returns an object that implements the <see cref="T:System.Security.IPermission" /> interface.</summary>
		/// <returns>An object that implements <see cref="T:System.Security.IPermission" />.</returns>
		public override IPermission CreatePermission()
		{
			return new ConfigurationPermission(base.Unrestricted ? PermissionState.Unrestricted : PermissionState.None);
		}
	}
	/// <summary>Represents an attribute or a child of a configuration element. This class cannot be inherited.</summary>
	public sealed class ConfigurationProperty
	{
		internal static readonly object NoDefaultValue = new object();

		private string name;

		private Type type;

		private object default_value;

		private TypeConverter converter;

		private ConfigurationValidatorBase validation;

		private ConfigurationPropertyOptions flags;

		private string description;

		private ConfigurationCollectionAttribute collectionAttribute;

		/// <summary>Gets the <see cref="T:System.ComponentModel.TypeConverter" /> used to convert this <see cref="T:System.Configuration.ConfigurationProperty" /> into an XML representation for writing to the configuration file.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> used to convert this <see cref="T:System.Configuration.ConfigurationProperty" /> into an XML representation for writing to the configuration file.</returns>
		/// <exception cref="T:System.Exception">This <see cref="T:System.Configuration.ConfigurationProperty" /> cannot be converted.</exception>
		public TypeConverter Converter => converter;

		/// <summary>Gets the default value for this <see cref="T:System.Configuration.ConfigurationProperty" /> property.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be cast to the type specified by the <see cref="P:System.Configuration.ConfigurationProperty.Type" /> property.</returns>
		public object DefaultValue => default_value;

		/// <summary>Gets a value indicating whether this <see cref="T:System.Configuration.ConfigurationProperty" /> is the key for the containing <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Configuration.ConfigurationProperty" /> object is the key for the containing element; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public bool IsKey => (flags & ConfigurationPropertyOptions.IsKey) != 0;

		/// <summary>Gets a value indicating whether this <see cref="T:System.Configuration.ConfigurationProperty" /> is required.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationProperty" /> is required; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public bool IsRequired => (flags & ConfigurationPropertyOptions.IsRequired) != 0;

		/// <summary>Gets a value that indicates whether the property is the default collection of an element.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is the default collection of an element; otherwise, <see langword="false" />.</returns>
		public bool IsDefaultCollection => (flags & ConfigurationPropertyOptions.IsDefaultCollection) != 0;

		/// <summary>Gets the name of this <see cref="T:System.Configuration.ConfigurationProperty" />.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.ConfigurationProperty" />.</returns>
		public string Name => name;

		/// <summary>Gets the description associated with the <see cref="T:System.Configuration.ConfigurationProperty" />.</summary>
		/// <returns>A <see langword="string" /> value that describes the property.</returns>
		public string Description => description;

		/// <summary>Gets the type of this <see cref="T:System.Configuration.ConfigurationProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the type of this <see cref="T:System.Configuration.ConfigurationProperty" /> object.</returns>
		public Type Type => type;

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationValidatorAttribute" />, which is used to validate this <see cref="T:System.Configuration.ConfigurationProperty" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator, which is used to validate this <see cref="T:System.Configuration.ConfigurationProperty" />.</returns>
		public ConfigurationValidatorBase Validator => validation;

		internal bool IsElement => typeof(ConfigurationElement).IsAssignableFrom(type);

		internal ConfigurationCollectionAttribute CollectionAttribute
		{
			get
			{
				return collectionAttribute;
			}
			set
			{
				collectionAttribute = value;
			}
		}

		/// <summary>Indicates whether the assembly name for the configuration property requires transformation when it is serialized for an earlier version of the .NET Framework.</summary>
		/// <returns>
		///   <see langword="true" /> if the property requires assembly name transformation; otherwise, <see langword="false" />.</returns>
		public bool IsAssemblyStringTransformationRequired
		{
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Indicates whether the type name for the configuration property requires transformation when it is serialized for an earlier version of the .NET Framework.</summary>
		/// <returns>
		///   <see langword="true" /> if the property requires type-name transformation; otherwise, <see langword="false" />.</returns>
		public bool IsTypeStringTransformationRequired
		{
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Indicates whether the configuration property's parent configuration section is queried at serialization time to determine whether the configuration property should be serialized into XML.</summary>
		/// <returns>
		///   <see langword="true" /> if the parent configuration section should be queried; otherwise, <see langword="false" />.</returns>
		public bool IsVersionCheckRequired
		{
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class.</summary>
		/// <param name="name">The name of the configuration entity.</param>
		/// <param name="type">The type of the configuration entity.</param>
		public ConfigurationProperty(string name, Type type)
			: this(name, type, NoDefaultValue, TypeDescriptor.GetConverter(type), new DefaultValidator(), ConfigurationPropertyOptions.None, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class.</summary>
		/// <param name="name">The name of the configuration entity.</param>
		/// <param name="type">The type of the configuration entity.</param>
		/// <param name="defaultValue">The default value of the configuration entity.</param>
		public ConfigurationProperty(string name, Type type, object defaultValue)
			: this(name, type, defaultValue, TypeDescriptor.GetConverter(type), new DefaultValidator(), ConfigurationPropertyOptions.None, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class.</summary>
		/// <param name="name">The name of the configuration entity.</param>
		/// <param name="type">The type of the configuration entity.</param>
		/// <param name="defaultValue">The default value of the configuration entity.</param>
		/// <param name="options">One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values.</param>
		public ConfigurationProperty(string name, Type type, object defaultValue, ConfigurationPropertyOptions options)
			: this(name, type, defaultValue, TypeDescriptor.GetConverter(type), new DefaultValidator(), options, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class.</summary>
		/// <param name="name">The name of the configuration entity.</param>
		/// <param name="type">The type of the configuration entity.</param>
		/// <param name="defaultValue">The default value of the configuration entity.</param>
		/// <param name="typeConverter">The type of the converter to apply.</param>
		/// <param name="validator">The validator to use.</param>
		/// <param name="options">One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values.</param>
		public ConfigurationProperty(string name, Type type, object defaultValue, TypeConverter typeConverter, ConfigurationValidatorBase validator, ConfigurationPropertyOptions options)
			: this(name, type, defaultValue, typeConverter, validator, options, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class.</summary>
		/// <param name="name">The name of the configuration entity.</param>
		/// <param name="type">The type of the configuration entity.</param>
		/// <param name="defaultValue">The default value of the configuration entity.</param>
		/// <param name="typeConverter">The type of the converter to apply.</param>
		/// <param name="validator">The validator to use.</param>
		/// <param name="options">One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values.</param>
		/// <param name="description">The description of the configuration entity.</param>
		public ConfigurationProperty(string name, Type type, object defaultValue, TypeConverter typeConverter, ConfigurationValidatorBase validator, ConfigurationPropertyOptions options, string description)
		{
			this.name = name;
			converter = ((typeConverter != null) ? typeConverter : TypeDescriptor.GetConverter(type));
			if (defaultValue != null)
			{
				if (defaultValue == NoDefaultValue)
				{
					defaultValue = Type.GetTypeCode(type) switch
					{
						TypeCode.Object => null, 
						TypeCode.String => string.Empty, 
						_ => Activator.CreateInstance(type), 
					};
				}
				else if (!type.IsAssignableFrom(defaultValue.GetType()))
				{
					if (!converter.CanConvertFrom(defaultValue.GetType()))
					{
						throw new ConfigurationErrorsException($"The default value for property '{name}' has a different type than the one of the property itself: expected {type} but was {defaultValue.GetType()}");
					}
					defaultValue = converter.ConvertFrom(defaultValue);
				}
			}
			default_value = defaultValue;
			flags = options;
			this.type = type;
			validation = ((validator != null) ? validator : new DefaultValidator());
			this.description = description;
		}

		internal object ConvertFromString(string value)
		{
			if (converter != null)
			{
				return converter.ConvertFromInvariantString(value);
			}
			throw new NotImplementedException();
		}

		internal string ConvertToString(object value)
		{
			if (converter != null)
			{
				return converter.ConvertToInvariantString(value);
			}
			throw new NotImplementedException();
		}

		internal void Validate(object value)
		{
			if (validation != null)
			{
				validation.Validate(value);
			}
		}
	}
	/// <summary>Declaratively instructs the .NET Framework to instantiate a configuration property. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ConfigurationPropertyAttribute : Attribute
	{
		private string name;

		private object default_value = ConfigurationProperty.NoDefaultValue;

		private ConfigurationPropertyOptions flags;

		/// <summary>Gets or sets a value indicating whether this is a key property for the decorated element property.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is a key property for an element of the collection; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public bool IsKey
		{
			get
			{
				return (flags & ConfigurationPropertyOptions.IsKey) != 0;
			}
			set
			{
				if (value)
				{
					flags |= ConfigurationPropertyOptions.IsKey;
				}
				else
				{
					flags &= ~ConfigurationPropertyOptions.IsKey;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether this is the default property collection for the decorated configuration property.</summary>
		/// <returns>
		///   <see langword="true" /> if the property represents the default collection of an element; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public bool IsDefaultCollection
		{
			get
			{
				return (flags & ConfigurationPropertyOptions.IsDefaultCollection) != 0;
			}
			set
			{
				if (value)
				{
					flags |= ConfigurationPropertyOptions.IsDefaultCollection;
				}
				else
				{
					flags &= ~ConfigurationPropertyOptions.IsDefaultCollection;
				}
			}
		}

		/// <summary>Gets or sets the default value for the decorated property.</summary>
		/// <returns>The object representing the default value of the decorated configuration-element property.</returns>
		public object DefaultValue
		{
			get
			{
				return default_value;
			}
			set
			{
				default_value = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> for the decorated configuration-element property.</summary>
		/// <returns>One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values associated with the property.</returns>
		public ConfigurationPropertyOptions Options
		{
			get
			{
				return flags;
			}
			set
			{
				flags = value;
			}
		}

		/// <summary>Gets the name of the decorated configuration-element property.</summary>
		/// <returns>The name of the decorated configuration-element property.</returns>
		public string Name => name;

		/// <summary>Gets or sets a value indicating whether the decorated element property is required.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is required; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public bool IsRequired
		{
			get
			{
				return (flags & ConfigurationPropertyOptions.IsRequired) != 0;
			}
			set
			{
				if (value)
				{
					flags |= ConfigurationPropertyOptions.IsRequired;
				}
				else
				{
					flags &= ~ConfigurationPropertyOptions.IsRequired;
				}
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Configuration.ConfigurationPropertyAttribute" /> class.</summary>
		/// <param name="name">Name of the <see cref="T:System.Configuration.ConfigurationProperty" /> object defined.</param>
		public ConfigurationPropertyAttribute(string name)
		{
			this.name = name;
		}
	}
	/// <summary>Represents a collection of configuration-element properties.</summary>
	public class ConfigurationPropertyCollection : ICollection, IEnumerable
	{
		private List<ConfigurationProperty> collection;

		/// <summary>Gets the number of properties in the collection.</summary>
		/// <returns>The number of properties in the collection.</returns>
		public int Count => collection.Count;

		/// <summary>Gets the collection item with the specified name.</summary>
		/// <param name="name">The <see cref="T:System.Configuration.ConfigurationProperty" /> to return.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationProperty" /> with the specified <paramref name="name" />.</returns>
		public ConfigurationProperty this[string name]
		{
			get
			{
				foreach (ConfigurationProperty item in collection)
				{
					if (item.Name == name)
					{
						return item;
					}
				}
				return null;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> is synchronized; otherwise, <see langword="false" />.</returns>
		public bool IsSynchronized => false;

		/// <summary>Gets the object to synchronize access to the collection.</summary>
		/// <returns>The object to synchronize access to the collection.</returns>
		public object SyncRoot => collection;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> class.</summary>
		public ConfigurationPropertyCollection()
		{
			collection = new List<ConfigurationProperty>();
		}

		/// <summary>Adds a configuration property to the collection.</summary>
		/// <param name="property">The <see cref="T:System.Configuration.ConfigurationProperty" /> to add.</param>
		public void Add(ConfigurationProperty property)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			collection.Add(property);
		}

		/// <summary>Specifies whether the configuration property is contained in this collection.</summary>
		/// <param name="name">An identifier for the <see cref="T:System.Configuration.ConfigurationProperty" /> to verify.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Configuration.ConfigurationProperty" /> is contained in the collection; otherwise, <see langword="false" />.</returns>
		public bool Contains(string name)
		{
			ConfigurationProperty configurationProperty = this[name];
			if (configurationProperty == null)
			{
				return false;
			}
			return collection.Contains(configurationProperty);
		}

		/// <summary>Copies this ConfigurationPropertyCollection to an array.</summary>
		/// <param name="array">Array to which to copy.</param>
		/// <param name="index">Index at which to begin copying.</param>
		public void CopyTo(ConfigurationProperty[] array, int index)
		{
			collection.CopyTo(array, index);
		}

		/// <summary>Copies this collection to an array.</summary>
		/// <param name="array">The array to which to copy.</param>
		/// <param name="index">The index location at which to begin copying.</param>
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)collection).CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection</returns>
		public IEnumerator GetEnumerator()
		{
			return collection.GetEnumerator();
		}

		/// <summary>Removes a configuration property from the collection.</summary>
		/// <param name="name">The <see cref="T:System.Configuration.ConfigurationProperty" /> to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Configuration.ConfigurationProperty" /> was removed; otherwise, <see langword="false" />.</returns>
		public bool Remove(string name)
		{
			return collection.Remove(this[name]);
		}

		/// <summary>Removes all configuration property objects from the collection.</summary>
		public void Clear()
		{
			collection.Clear();
		}
	}
	/// <summary>Specifies the options to apply to a property.</summary>
	[Flags]
	public enum ConfigurationPropertyOptions
	{
		/// <summary>Indicates that no option applies to the property.</summary>
		None = 0,
		/// <summary>Indicates that the property is a default collection.</summary>
		IsDefaultCollection = 1,
		/// <summary>Indicates that the property is required.</summary>
		IsRequired = 2,
		/// <summary>Indicates that the property is a collection key.</summary>
		IsKey = 4,
		/// <summary>Indicates whether the type name for the configuration property requires transformation when it is serialized for an earlier version of the .NET Framework.</summary>
		IsTypeStringTransformationRequired = 8,
		/// <summary>Indicates whether the assembly name for the configuration property requires transformation when it is serialized for an earlier version of the .NET Framework.</summary>
		IsAssemblyStringTransformationRequired = 0x10,
		/// <summary>Indicates whether the configuration property's parent configuration section should be queried at serialization time to determine whether the configuration property should be serialized into XML.</summary>
		IsVersionCheckRequired = 0x20
	}
	internal class ConfigurationSaveEventArgs : EventArgs
	{
		public string StreamPath { get; private set; }

		public bool Start { get; private set; }

		public object Context { get; private set; }

		public bool Failed { get; private set; }

		public Exception Exception { get; private set; }

		public ConfigurationSaveEventArgs(string streamPath, bool start, Exception ex, object context)
		{
			StreamPath = streamPath;
			Start = start;
			Failed = ex != null;
			Exception = ex;
			Context = context;
		}
	}
	internal delegate void ConfigurationSaveEventHandler(Configuration sender, ConfigurationSaveEventArgs args);
	/// <summary>Determines which properties are written out to a configuration file.</summary>
	public enum ConfigurationSaveMode
	{
		/// <summary>Causes only properties that differ from inherited values to be written to the configuration file.</summary>
		Minimal = 1,
		/// <summary>Causes all properties to be written to the configuration file. This is useful mostly for creating information configuration files or moving configuration values from one machine to another.</summary>
		Full = 2,
		/// <summary>Causes only modified properties to be written to the configuration file, even when the value is the same as the inherited value.</summary>
		Modified = 0
	}
	/// <summary>Represents a section within a configuration file.</summary>
	public abstract class ConfigurationSection : ConfigurationElement
	{
		private SectionInformation sectionInformation;

		private IConfigurationSectionHandler section_handler;

		private string externalDataXml;

		private object _configContext;

		internal string ExternalDataXml => externalDataXml;

		internal IConfigurationSectionHandler SectionHandler
		{
			get
			{
				return section_handler;
			}
			set
			{
				section_handler = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SectionInformation" /> object that contains the non-customizable information and functionality of the <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SectionInformation" /> that contains the non-customizable information and functionality of the <see cref="T:System.Configuration.ConfigurationSection" />.</returns>
		[System.MonoTODO]
		public SectionInformation SectionInformation
		{
			get
			{
				if (sectionInformation == null)
				{
					sectionInformation = new SectionInformation();
				}
				return sectionInformation;
			}
		}

		internal object ConfigContext
		{
			get
			{
				return _configContext;
			}
			set
			{
				_configContext = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationSection" /> class.</summary>
		protected ConfigurationSection()
		{
		}

		/// <summary>Returns a custom object when overridden in a derived class.</summary>
		/// <returns>The object representing the section.</returns>
		[System.MonoTODO("Provide ConfigContext. Likely the culprit of bug #322493")]
		protected internal virtual object GetRuntimeObject()
		{
			if (SectionHandler != null)
			{
				object obj = ((sectionInformation != null) ? sectionInformation.GetParentSection() : null)?.GetRuntimeObject();
				if (base.RawXml == null)
				{
					return obj;
				}
				try
				{
					XmlReader reader = new ConfigXmlTextReader(new StringReader(base.RawXml), base.Configuration.FilePath);
					DoDeserializeSection(reader);
					if (!string.IsNullOrEmpty(SectionInformation.ConfigSource))
					{
						string configFilePath = SectionInformation.ConfigFilePath;
						configFilePath = (string.IsNullOrEmpty(configFilePath) ? string.Empty : Path.GetDirectoryName(configFilePath));
						string path = Path.Combine(configFilePath, SectionInformation.ConfigSource);
						if (File.Exists(path))
						{
							base.RawXml = File.ReadAllText(path);
							SectionInformation.SetRawXml(base.RawXml);
						}
					}
				}
				catch
				{
				}
				XmlDocument xmlDocument = new ConfigurationXmlDocument();
				xmlDocument.LoadXml(base.RawXml);
				return SectionHandler.Create(obj, ConfigContext, xmlDocument.DocumentElement);
			}
			return this;
		}

		/// <summary>Indicates whether this configuration element has been modified since it was last saved or loaded when implemented in a derived class.</summary>
		/// <returns>
		///   <see langword="true" /> if the element has been modified; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		protected internal override bool IsModified()
		{
			return base.IsModified();
		}

		/// <summary>Resets the value of the <see cref="M:System.Configuration.ConfigurationElement.IsModified" /> method to <see langword="false" /> when implemented in a derived class.</summary>
		[System.MonoTODO]
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		private ConfigurationElement CreateElement(Type t)
		{
			ConfigurationElement configurationElement = (ConfigurationElement)Activator.CreateInstance(t);
			configurationElement.Init();
			configurationElement.Configuration = base.Configuration;
			if (IsReadOnly())
			{
				configurationElement.SetReadOnly();
			}
			return configurationElement;
		}

		private void DoDeserializeSection(XmlReader reader)
		{
			reader.MoveToContent();
			string text = null;
			string text2 = null;
			while (reader.MoveToNextAttribute())
			{
				string localName = reader.LocalName;
				if (localName == "configProtectionProvider")
				{
					text = reader.Value;
				}
				else if (localName == "configSource")
				{
					text2 = reader.Value;
				}
			}
			if (text != null)
			{
				ProtectedConfigurationProvider provider = ProtectedConfiguration.GetProvider(text, throwOnError: true);
				XmlDocument xmlDocument = new ConfigurationXmlDocument();
				reader.MoveToElement();
				xmlDocument.Load(new StringReader(reader.ReadInnerXml()));
				reader = new XmlNodeReader(provider.Decrypt(xmlDocument));
				SectionInformation.ProtectSection(text);
				reader.MoveToContent();
			}
			if (text2 != null)
			{
				SectionInformation.ConfigSource = text2;
			}
			SectionInformation.SetRawXml(base.RawXml);
			if (SectionHandler == null)
			{
				DeserializeElement(reader, serializeCollectionKey: false);
			}
		}

		/// <summary>Reads XML from the configuration file.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object, which reads from the configuration file.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="reader" /> found no elements in the configuration file.</exception>
		[System.MonoInternalNote("find the proper location for the decryption stuff")]
		protected internal virtual void DeserializeSection(XmlReader reader)
		{
			try
			{
				DoDeserializeSection(reader);
			}
			catch (ConfigurationErrorsException ex)
			{
				throw new ConfigurationErrorsException($"Error deserializing configuration section {SectionInformation.Name}: {ex.Message}");
			}
		}

		internal void DeserializeConfigSource(string basePath)
		{
			string configSource = SectionInformation.ConfigSource;
			if (!string.IsNullOrEmpty(configSource))
			{
				if (Path.IsPathRooted(configSource))
				{
					throw new ConfigurationErrorsException("The configSource attribute must be a relative physical path.");
				}
				if (HasLocalModifications())
				{
					throw new ConfigurationErrorsException("A section using 'configSource' may contain no other attributes or elements.");
				}
				string text = Path.Combine(basePath, configSource);
				if (!File.Exists(text))
				{
					base.RawXml = null;
					SectionInformation.SetRawXml(null);
					throw new ConfigurationErrorsException($"Unable to open configSource file '{text}'.");
				}
				base.RawXml = File.ReadAllText(text);
				SectionInformation.SetRawXml(base.RawXml);
				DeserializeElement(new ConfigXmlTextReader(new StringReader(base.RawXml), text), serializeCollectionKey: false);
			}
		}

		/// <summary>Creates an XML string containing an unmerged view of the <see cref="T:System.Configuration.ConfigurationSection" /> object as a single section to write to a file.</summary>
		/// <param name="parentElement">The <see cref="T:System.Configuration.ConfigurationElement" /> instance to use as the parent when performing the un-merge.</param>
		/// <param name="name">The name of the section to create.</param>
		/// <param name="saveMode">The <see cref="T:System.Configuration.ConfigurationSaveMode" /> instance to use when writing to a string.</param>
		/// <returns>An XML string containing an unmerged view of the <see cref="T:System.Configuration.ConfigurationSection" /> object.</returns>
		protected internal virtual string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
		{
			externalDataXml = null;
			ConfigurationElement configurationElement;
			if (parentElement != null)
			{
				configurationElement = CreateElement(GetType());
				configurationElement.Unmerge(this, parentElement, saveMode);
			}
			else
			{
				configurationElement = this;
			}
			configurationElement.PrepareSave(parentElement, saveMode);
			bool flag = configurationElement.HasValues(parentElement, saveMode);
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					xmlTextWriter.Formatting = Formatting.Indented;
					if (flag)
					{
						configurationElement.SerializeToXmlElement(xmlTextWriter, name);
					}
					else if (saveMode == ConfigurationSaveMode.Modified && configurationElement.IsModified())
					{
						xmlTextWriter.WriteStartElement(name);
						xmlTextWriter.WriteEndElement();
					}
					xmlTextWriter.Close();
				}
				result = stringWriter.ToString();
			}
			string configSource = SectionInformation.ConfigSource;
			if (string.IsNullOrEmpty(configSource))
			{
				return result;
			}
			externalDataXml = result;
			using StringWriter stringWriter2 = new StringWriter();
			bool flag2 = !string.IsNullOrEmpty(name);
			using (XmlTextWriter xmlTextWriter2 = new XmlTextWriter(stringWriter2))
			{
				if (flag2)
				{
					xmlTextWriter2.WriteStartElement(name);
				}
				xmlTextWriter2.WriteAttributeString("configSource", configSource);
				if (flag2)
				{
					xmlTextWriter2.WriteEndElement();
				}
			}
			return stringWriter2.ToString();
		}

		/// <summary>Indicates whether the specified element should be serialized when the configuration object hierarchy is serialized for the specified target version of the .NET Framework.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> object that is a candidate for serialization.</param>
		/// <param name="elementName">The name of the <see cref="T:System.Configuration.ConfigurationElement" /> object as it occurs in XML.</param>
		/// <param name="targetFramework">The target version of the .NET Framework.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="element" /> should be serialized; otherwise, <see langword="false" />.</returns>
		protected internal virtual bool ShouldSerializeElementInTargetVersion(ConfigurationElement element, string elementName, FrameworkName targetFramework)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Indicates whether the specified property should be serialized when the configuration object hierarchy is serialized for the specified target version of the .NET Framework.</summary>
		/// <param name="property">The <see cref="T:System.Configuration.ConfigurationProperty" /> object that is a candidate for serialization.</param>
		/// <param name="propertyName">The name of the <see cref="T:System.Configuration.ConfigurationProperty" /> object as it occurs in XML.</param>
		/// <param name="targetFramework">The target version of the .NET Framework.</param>
		/// <param name="parentConfigurationElement">The parent element of the property.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="property" /> should be serialized; otherwise, <see langword="false" />.</returns>
		protected internal virtual bool ShouldSerializePropertyInTargetVersion(ConfigurationProperty property, string propertyName, FrameworkName targetFramework, ConfigurationElement parentConfigurationElement)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Indicates whether the current <see cref="T:System.Configuration.ConfigurationSection" /> instance should be serialized when the configuration object hierarchy is serialized for the specified target version of the .NET Framework.</summary>
		/// <param name="targetFramework">The target version of the .NET Framework.</param>
		/// <returns>
		///   <see langword="true" /> if the current section should be serialized; otherwise, <see langword="false" />.</returns>
		protected internal virtual bool ShouldSerializeSectionInTargetVersion(FrameworkName targetFramework)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
	/// <summary>Represents a collection of related sections within a configuration file.</summary>
	[Serializable]
	public sealed class ConfigurationSectionCollection : NameObjectCollectionBase
	{
		private SectionGroupInfo group;

		private Configuration config;

		private static readonly object lockObject = new object();

		/// <summary>Gets the keys to all <see cref="T:System.Configuration.ConfigurationSection" /> objects contained in this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> object that contains the keys of all sections in this collection.</returns>
		public override KeysCollection Keys => group.Sections.Keys;

		/// <summary>Gets the number of sections in this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <returns>An integer that represents the number of sections in the collection.</returns>
		public override int Count => group.Sections.Count;

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSection" /> object to be returned.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object with the specified name.</returns>
		public ConfigurationSection this[string name]
		{
			get
			{
				ConfigurationSection configurationSection = BaseGet(name) as ConfigurationSection;
				if (configurationSection == null)
				{
					if (!(group.Sections[name] is SectionInfo sectionInfo))
					{
						return null;
					}
					configurationSection = config.GetSectionInstance(sectionInfo, createDefaultInstance: true);
					if (configurationSection == null)
					{
						return null;
					}
					lock (lockObject)
					{
						BaseSet(name, configurationSection);
					}
				}
				return configurationSection;
			}
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ConfigurationSection" /> object to be returned.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object at the specified index.</returns>
		public ConfigurationSection this[int index] => this[GetKey(index)];

		internal ConfigurationSectionCollection(Configuration config, SectionGroupInfo group)
			: base(StringComparer.Ordinal)
		{
			this.config = config;
			this.group = group;
		}

		/// <summary>Adds a <see cref="T:System.Configuration.ConfigurationSection" /> object to the <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <param name="name">The name of the section to be added.</param>
		/// <param name="section">The section to be added.</param>
		public void Add(string name, ConfigurationSection section)
		{
			config.CreateSection(group, name, section);
		}

		/// <summary>Clears this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		public void Clear()
		{
			if (group.Sections == null)
			{
				return;
			}
			foreach (ConfigInfo section in group.Sections)
			{
				config.RemoveConfigInfo(section);
			}
		}

		/// <summary>Copies this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object to an array.</summary>
		/// <param name="array">The array to copy the <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object to.</param>
		/// <param name="index">The index location at which to begin copying.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="array" /> is less than the value of <see cref="P:System.Configuration.ConfigurationSectionCollection.Count" /> plus <paramref name="index" />.</exception>
		public void CopyTo(ConfigurationSection[] array, int index)
		{
			for (int i = 0; i < group.Sections.Count; i++)
			{
				array[i + index] = this[i];
			}
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSection" /> object contained in this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ConfigurationSection" /> object to be returned.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object at the specified index.</returns>
		public ConfigurationSection Get(int index)
		{
			return this[index];
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSection" /> object contained in this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSection" /> object to be returned.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object with the specified name.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null or an empty string ("").</exception>
		public ConfigurationSection Get(string name)
		{
			return this[name];
		}

		/// <summary>Gets an enumerator that can iterate through this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</returns>
		public override IEnumerator GetEnumerator()
		{
			foreach (string allKey in group.Sections.AllKeys)
			{
				yield return this[allKey];
			}
		}

		/// <summary>Gets the key of the specified <see cref="T:System.Configuration.ConfigurationSection" /> object contained in this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ConfigurationSection" /> object whose key is to be returned.</param>
		/// <returns>The key of the <see cref="T:System.Configuration.ConfigurationSection" /> object at the specified index.</returns>
		public string GetKey(int index)
		{
			return group.Sections.GetKey(index);
		}

		/// <summary>Removes the specified <see cref="T:System.Configuration.ConfigurationSection" /> object from this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <param name="name">The name of the section to be removed.</param>
		public void Remove(string name)
		{
			if (group.Sections[name] is SectionInfo sectionInfo)
			{
				config.RemoveConfigInfo(sectionInfo);
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Configuration.ConfigurationSection" /> object from this <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object.</summary>
		/// <param name="index">The index of the section to be removed.</param>
		public void RemoveAt(int index)
		{
			SectionInfo sectionInfo = group.Sections[index] as SectionInfo;
			config.RemoveConfigInfo(sectionInfo);
		}

		/// <summary>Used by the system during serialization.</summary>
		/// <param name="info">The applicable <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The applicable <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		[System.MonoTODO]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		internal ConfigurationSectionCollection()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Represents a group of related sections within a configuration file.</summary>
	public class ConfigurationSectionGroup
	{
		private bool require_declaration;

		private string name;

		private string type_name;

		private ConfigurationSectionCollection sections;

		private ConfigurationSectionGroupCollection groups;

		private Configuration config;

		private SectionGroupInfo group;

		private bool initialized;

		private Configuration Config
		{
			get
			{
				if (config == null)
				{
					throw new InvalidOperationException("ConfigurationSectionGroup cannot be edited until it is added to a Configuration instance as its descendant");
				}
				return config;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> is declared; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool IsDeclared => false;

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object declaration is required.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> declaration is required; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool IsDeclarationRequired => require_declaration;

		/// <summary>Gets the name property of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>The name property of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		public string Name => name;

		/// <summary>Gets the section group name associated with this <see cref="T:System.Configuration.ConfigurationSectionGroup" />.</summary>
		/// <returns>The section group name of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		[System.MonoInternalNote("Check if this is correct")]
		public string SectionGroupName => group.XPath;

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object that contains all the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects that are children of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object that contains all the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects that are children of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		public ConfigurationSectionGroupCollection SectionGroups
		{
			get
			{
				if (groups == null)
				{
					groups = new ConfigurationSectionGroupCollection(Config, group);
				}
				return groups;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object that contains all of <see cref="T:System.Configuration.ConfigurationSection" /> objects within this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object that contains all the <see cref="T:System.Configuration.ConfigurationSection" /> objects within this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		public ConfigurationSectionCollection Sections
		{
			get
			{
				if (sections == null)
				{
					sections = new ConfigurationSectionCollection(Config, group);
				}
				return sections;
			}
		}

		/// <summary>Gets or sets the type for this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>The type of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object is the root section group.  
		/// -or-
		///  The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object has a location.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The section or group is already defined at another level.</exception>
		public string Type
		{
			get
			{
				return type_name;
			}
			set
			{
				type_name = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> class.</summary>
		public ConfigurationSectionGroup()
		{
		}

		internal void Initialize(Configuration config, SectionGroupInfo group)
		{
			if (initialized)
			{
				throw new SystemException("INTERNAL ERROR: this configuration section is being initialized twice: " + GetType());
			}
			initialized = true;
			this.config = config;
			this.group = group;
		}

		internal void SetName(string name)
		{
			this.name = name;
		}

		/// <summary>Forces the declaration for this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <param name="force">
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object must be written to the file; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object is the root section group.  
		/// -or-
		///  The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object has a location.</exception>
		[System.MonoTODO]
		public void ForceDeclaration(bool force)
		{
			require_declaration = force;
		}

		/// <summary>Forces the declaration for this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		public void ForceDeclaration()
		{
			ForceDeclaration(force: true);
		}

		/// <summary>Indicates whether the current <see cref="T:System.Configuration.ConfigurationSectionGroup" /> instance should be serialized when the configuration object hierarchy is serialized for the specified target version of the .NET Framework.</summary>
		/// <param name="targetFramework">The target version of the .NET Framework.</param>
		/// <returns>
		///   <see langword="true" /> if the current section group should be serialized; otherwise, <see langword="false" />.</returns>
		protected internal virtual bool ShouldSerializeSectionGroupInTargetVersion(FrameworkName targetFramework)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
	/// <summary>Represents a collection of <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects.</summary>
	[Serializable]
	public sealed class ConfigurationSectionGroupCollection : NameObjectCollectionBase
	{
		private SectionGroupInfo group;

		private Configuration config;

		/// <summary>Gets the keys to all <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects contained in this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> object that contains the names of all section groups in this collection.</returns>
		public override KeysCollection Keys => group.Groups.Keys;

		/// <summary>Gets the number of section groups in the collection.</summary>
		/// <returns>An integer that represents the number of section groups in the collection.</returns>
		public override int Count => group.Groups.Count;

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object whose name is specified from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be returned.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object with the specified name.  
		///  In C#, this property is the indexer for the <see cref="T:System.Configuration.ConfigurationSectionCollection" /> class.</returns>
		public ConfigurationSectionGroup this[string name]
		{
			get
			{
				ConfigurationSectionGroup configurationSectionGroup = BaseGet(name) as ConfigurationSectionGroup;
				if (configurationSectionGroup == null)
				{
					if (!(group.Groups[name] is SectionGroupInfo sectionGroupInfo))
					{
						return null;
					}
					configurationSectionGroup = config.GetSectionGroupInstance(sectionGroupInfo);
					BaseSet(name, configurationSectionGroup);
				}
				return configurationSectionGroup;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object whose index is specified from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be returned.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object at the specified index.  
		///  In C#, this property is the indexer for the <see cref="T:System.Configuration.ConfigurationSectionCollection" /> class.</returns>
		public ConfigurationSectionGroup this[int index] => this[GetKey(index)];

		internal ConfigurationSectionGroupCollection(Configuration config, SectionGroupInfo group)
			: base(StringComparer.Ordinal)
		{
			this.config = config;
			this.group = group;
		}

		/// <summary>Adds a <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be added.</param>
		/// <param name="sectionGroup">The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be added.</param>
		public void Add(string name, ConfigurationSectionGroup sectionGroup)
		{
			config.CreateSectionGroup(group, name, sectionGroup);
		}

		/// <summary>Clears the collection.</summary>
		public void Clear()
		{
			if (group.Groups == null)
			{
				return;
			}
			foreach (ConfigInfo group in group.Groups)
			{
				config.RemoveConfigInfo(group);
			}
		}

		/// <summary>Copies this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object to an array.</summary>
		/// <param name="array">The array to copy the object to.</param>
		/// <param name="index">The index location at which to begin copying.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="array" /> is less than the value of <see cref="P:System.Configuration.ConfigurationSectionGroupCollection.Count" /> plus <paramref name="index" />.</exception>
		public void CopyTo(ConfigurationSectionGroup[] array, int index)
		{
			for (int i = 0; i < group.Groups.Count; i++)
			{
				array[i + index] = this[i];
			}
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object contained in the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be returned.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object at the specified index.</returns>
		public ConfigurationSectionGroup Get(int index)
		{
			return this[index];
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be returned.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object with the specified name.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null or an empty string ("").</exception>
		public ConfigurationSectionGroup Get(string name)
		{
			return this[name];
		}

		/// <summary>Gets an enumerator that can iterate through the <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</returns>
		public override IEnumerator GetEnumerator()
		{
			return group.Groups.AllKeys.GetEnumerator();
		}

		/// <summary>Gets the key of the specified <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object contained in this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <param name="index">The index of the section group whose key is to be returned.</param>
		/// <returns>The key of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object at the specified index.</returns>
		public string GetKey(int index)
		{
			return group.Groups.GetKey(index);
		}

		/// <summary>Removes the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object whose name is specified from this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <param name="name">The name of the section group to be removed.</param>
		public void Remove(string name)
		{
			if (group.Groups[name] is SectionGroupInfo sectionGroupInfo)
			{
				config.RemoveConfigInfo(sectionGroupInfo);
			}
		}

		/// <summary>Removes the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object whose index is specified from this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <param name="index">The index of the section group to be removed.</param>
		public void RemoveAt(int index)
		{
			SectionGroupInfo sectionGroupInfo = group.Groups[index] as SectionGroupInfo;
			config.RemoveConfigInfo(sectionGroupInfo);
		}

		/// <summary>Used by the system during serialization.</summary>
		/// <param name="info">The applicable <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The applicable <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		[System.MonoTODO]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		internal ConfigurationSectionGroupCollection()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Used to specify which configuration file is to be represented by the Configuration object.</summary>
	public enum ConfigurationUserLevel
	{
		/// <summary>Gets the <see cref="T:System.Configuration.Configuration" /> that applies to all users.</summary>
		None = 0,
		/// <summary>Gets the roaming <see cref="T:System.Configuration.Configuration" /> that applies to the current user.</summary>
		PerUserRoaming = 10,
		/// <summary>Gets the local <see cref="T:System.Configuration.Configuration" /> that applies to the current user.</summary>
		PerUserRoamingAndLocal = 20
	}
	/// <summary>Serves as the base class for the <see cref="N:System.Configuration" /> validator attribute types.</summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ConfigurationValidatorAttribute : Attribute
	{
		private Type validatorType;

		private ConfigurationValidatorBase instance;

		/// <summary>Gets the validator attribute instance.</summary>
		/// <returns>The current <see cref="T:System.Configuration.ConfigurationValidatorBase" />.</returns>
		public virtual ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (instance == null)
				{
					instance = (ConfigurationValidatorBase)Activator.CreateInstance(validatorType);
				}
				return instance;
			}
		}

		/// <summary>Gets the type of the validator attribute.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the current validator attribute instance.</returns>
		public Type ValidatorType => validatorType;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationValidatorAttribute" /> class.</summary>
		protected ConfigurationValidatorAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationValidatorAttribute" /> class using the specified validator type.</summary>
		/// <param name="validator">The validator type to use when creating an instance of <see cref="T:System.Configuration.ConfigurationValidatorAttribute" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="validator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="validator" /> is not derived from <see cref="T:System.Configuration.ConfigurationValidatorBase" />.</exception>
		public ConfigurationValidatorAttribute(Type validator)
		{
			validatorType = validator;
		}
	}
	/// <summary>Acts as a base class for deriving a validation class so that a value of an object can be verified.</summary>
	public abstract class ConfigurationValidatorBase
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ConfigurationValidatorBase" /> class.</summary>
		protected ConfigurationValidatorBase()
		{
		}

		/// <summary>Determines whether an object can be validated based on type.</summary>
		/// <param name="type">The object type.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="type" /> parameter value matches the expected <see langword="type" />; otherwise, <see langword="false" />.</returns>
		public virtual bool CanValidate(Type type)
		{
			return false;
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The object value.</param>
		public abstract void Validate(object value);
	}
	/// <summary>Represents a single, named connection string in the connection strings configuration file section.</summary>
	public sealed class ConnectionStringSettings : ConfigurationElement
	{
		private static ConfigurationPropertyCollection _properties;

		private static readonly ConfigurationProperty _propConnectionString;

		private static readonly ConfigurationProperty _propName;

		private static readonly ConfigurationProperty _propProviderName;

		protected internal override ConfigurationPropertyCollection Properties => _properties;

		/// <summary>Gets or sets the <see cref="T:System.Configuration.ConnectionStringSettings" /> name.</summary>
		/// <returns>The string value assigned to the <see cref="P:System.Configuration.ConnectionStringSettings.Name" /> property.</returns>
		[ConfigurationProperty("name", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Name
		{
			get
			{
				return (string)base[_propName];
			}
			set
			{
				base[_propName] = value;
			}
		}

		/// <summary>Gets or sets the provider name property.</summary>
		/// <returns>The <see cref="P:System.Configuration.ConnectionStringSettings.ProviderName" /> property.</returns>
		[ConfigurationProperty("providerName", DefaultValue = "System.Data.SqlClient")]
		public string ProviderName
		{
			get
			{
				return (string)base[_propProviderName];
			}
			set
			{
				base[_propProviderName] = value;
			}
		}

		/// <summary>Gets or sets the connection string.</summary>
		/// <returns>The string value assigned to the <see cref="P:System.Configuration.ConnectionStringSettings.ConnectionString" /> property.</returns>
		[ConfigurationProperty("connectionString", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string ConnectionString
		{
			get
			{
				return (string)base[_propConnectionString];
			}
			set
			{
				base[_propConnectionString] = value;
			}
		}

		static ConnectionStringSettings()
		{
			_properties = new ConfigurationPropertyCollection();
			_propName = new ConfigurationProperty("name", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
			_propProviderName = new ConfigurationProperty("providerName", typeof(string), "", ConfigurationPropertyOptions.None);
			_propConnectionString = new ConfigurationProperty("connectionString", typeof(string), "", ConfigurationPropertyOptions.IsRequired);
			_properties.Add(_propName);
			_properties.Add(_propProviderName);
			_properties.Add(_propConnectionString);
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Configuration.ConnectionStringSettings" /> class.</summary>
		public ConnectionStringSettings()
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Configuration.ConnectionStringSettings" /> class.</summary>
		/// <param name="name">The name of the connection string.</param>
		/// <param name="connectionString">The connection string.</param>
		public ConnectionStringSettings(string name, string connectionString)
			: this(name, connectionString, "")
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Configuration.ConnectionStringSettings" /> object.</summary>
		/// <param name="name">The name of the connection string.</param>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="providerName">The name of the provider to use with the connection string.</param>
		public ConnectionStringSettings(string name, string connectionString, string providerName)
		{
			Name = name;
			ConnectionString = connectionString;
			ProviderName = providerName;
		}

		/// <summary>Returns a string representation of the object.</summary>
		/// <returns>A string representation of the object.</returns>
		public override string ToString()
		{
			return ConnectionString;
		}
	}
	/// <summary>Contains a collection of <see cref="T:System.Configuration.ConnectionStringSettings" /> objects.</summary>
	[ConfigurationCollection(typeof(ConnectionStringSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ConnectionStringSettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the <see cref="T:System.Configuration.ConnectionStringSettings" /> object with the specified name in the collection.</summary>
		/// <param name="name">The name of a <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		/// <returns>The <see cref="T:System.Configuration.ConnectionStringSettings" /> object with the specified name; otherwise, <see langword="null" />.</returns>
		public new ConnectionStringSettings this[string name]
		{
			get
			{
				IEnumerator enumerator = GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						ConfigurationElement configurationElement = (ConfigurationElement)enumerator.Current;
						if (configurationElement is ConnectionStringSettings && string.Compare(((ConnectionStringSettings)configurationElement).Name, name, ignoreCase: true, CultureInfo.InvariantCulture) == 0)
						{
							return configurationElement as ConnectionStringSettings;
						}
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				return null;
			}
		}

		/// <summary>Gets or sets the connection string at the specified index in the collection.</summary>
		/// <param name="index">The index of a <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		/// <returns>The <see cref="T:System.Configuration.ConnectionStringSettings" /> object at the specified index.</returns>
		public ConnectionStringSettings this[int index]
		{
			get
			{
				return (ConnectionStringSettings)BaseGet(index);
			}
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		[System.MonoTODO]
		protected internal override ConfigurationPropertyCollection Properties => base.Properties;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> class.</summary>
		public ConnectionStringSettingsCollection()
		{
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionStringSettings();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ConnectionStringSettings)element).Name;
		}

		/// <summary>Adds a <see cref="T:System.Configuration.ConnectionStringSettings" /> object to the collection.</summary>
		/// <param name="settings">A <see cref="T:System.Configuration.ConnectionStringSettings" /> object to add to the collection.</param>
		public void Add(ConnectionStringSettings settings)
		{
			BaseAdd(settings);
		}

		/// <summary>Removes all the <see cref="T:System.Configuration.ConnectionStringSettings" /> objects from the collection.</summary>
		public void Clear()
		{
			BaseClear();
		}

		/// <summary>Returns the collection index of the passed <see cref="T:System.Configuration.ConnectionStringSettings" /> object.</summary>
		/// <param name="settings">A <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		/// <returns>The collection index of the specified <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> object.</returns>
		public int IndexOf(ConnectionStringSettings settings)
		{
			return BaseIndexOf(settings);
		}

		/// <summary>Removes the specified <see cref="T:System.Configuration.ConnectionStringSettings" /> object from the collection.</summary>
		/// <param name="settings">A <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		public void Remove(ConnectionStringSettings settings)
		{
			BaseRemove(settings.Name);
		}

		/// <summary>Removes the specified <see cref="T:System.Configuration.ConnectionStringSettings" /> object from the collection.</summary>
		/// <param name="name">The name of a <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		public void Remove(string name)
		{
			BaseRemove(name);
		}

		/// <summary>Removes the <see cref="T:System.Configuration.ConnectionStringSettings" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index of a <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}

		protected override void BaseAdd(int index, ConfigurationElement element)
		{
			if (!(element is ConnectionStringSettings))
			{
				base.BaseAdd(element);
			}
			if (IndexOf((ConnectionStringSettings)element) >= 0)
			{
				throw new ConfigurationErrorsException($"The element {((ConnectionStringSettings)element).Name} already exist!");
			}
			this[index] = (ConnectionStringSettings)element;
		}
	}
	/// <summary>Provides programmatic access to the connection strings configuration-file section.</summary>
	public sealed class ConnectionStringsSection : ConfigurationSection
	{
		private static readonly ConfigurationProperty _propConnectionStrings;

		private static ConfigurationPropertyCollection _properties;

		/// <summary>Gets a <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> collection of <see cref="T:System.Configuration.ConnectionStringSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> collection of <see cref="T:System.Configuration.ConnectionStringSettings" /> objects.</returns>
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ConnectionStringSettingsCollection ConnectionStrings => (ConnectionStringSettingsCollection)base[_propConnectionStrings];

		protected internal override ConfigurationPropertyCollection Properties => _properties;

		static ConnectionStringsSection()
		{
			_propConnectionStrings = new ConfigurationProperty("", typeof(ConnectionStringSettingsCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
			_properties = new ConfigurationPropertyCollection();
			_properties.Add(_propConnectionStrings);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConnectionStringsSection" /> class.</summary>
		public ConnectionStringsSection()
		{
		}

		protected internal override object GetRuntimeObject()
		{
			return base.GetRuntimeObject();
		}
	}
	/// <summary>Encapsulates the context information that is associated with a <see cref="T:System.Configuration.ConfigurationElement" /> object. This class cannot be inherited.</summary>
	public sealed class ContextInformation
	{
		private object ctx;

		private Configuration config;

		/// <summary>Gets the context of the environment where the configuration property is being evaluated.</summary>
		/// <returns>An object specifying the environment where the configuration property is being evaluated.</returns>
		public object HostingContext => ctx;

		/// <summary>Gets a value specifying whether the configuration property is being evaluated at the machine configuration level.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration property is being evaluated at the machine configuration level; otherwise, <see langword="false" />.</returns>
		[System.MonoInternalNote("should this use HostingContext instead?")]
		public bool IsMachineLevel => config.ConfigPath == "machine";

		internal ContextInformation(Configuration config, object ctx)
		{
			this.ctx = ctx;
			this.config = config;
		}

		/// <summary>Provides an object containing configuration-section information based on the specified section name.</summary>
		/// <param name="sectionName">The name of the configuration section.</param>
		/// <returns>An object containing the specified section within the configuration.</returns>
		public object GetSection(string sectionName)
		{
			return config.GetSection(sectionName);
		}

		internal ContextInformation()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Represents a basic configuration-section handler that exposes the configuration section's XML for both read and write access.</summary>
	public sealed class DefaultSection : ConfigurationSection
	{
		private static ConfigurationPropertyCollection properties;

		protected internal override ConfigurationPropertyCollection Properties => properties;

		static DefaultSection()
		{
			properties = new ConfigurationPropertyCollection();
		}

		protected internal override void DeserializeSection(XmlReader xmlReader)
		{
			if (base.RawXml == null)
			{
				base.RawXml = xmlReader.ReadOuterXml();
			}
			else
			{
				xmlReader.Skip();
			}
		}

		[System.MonoTODO]
		protected internal override bool IsModified()
		{
			return base.IsModified();
		}

		[System.MonoTODO]
		protected internal override void Reset(ConfigurationElement parentSection)
		{
			base.Reset(parentSection);
		}

		[System.MonoTODO]
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		[System.MonoTODO]
		protected internal override string SerializeSection(ConfigurationElement parentSection, string name, ConfigurationSaveMode saveMode)
		{
			return base.SerializeSection(parentSection, name, saveMode);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.DefaultSection" /> class.</summary>
		public DefaultSection()
		{
		}
	}
	/// <summary>Provides validation of an object. This class cannot be inherited.</summary>
	public sealed class DefaultValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.DefaultValidator" /> class.</summary>
		public DefaultValidator()
		{
		}

		/// <summary>Determines whether an object can be validated, based on type.</summary>
		/// <param name="type">The object type.</param>
		/// <returns>
		///   <see langword="true" /> for all types being validated.</returns>
		public override bool CanValidate(Type type)
		{
			return true;
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The object value.</param>
		public override void Validate(object value)
		{
		}
	}
	/// <summary>Provides a <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object that uses the Windows data protection API (DPAPI) to encrypt and decrypt configuration data.</summary>
	public sealed class DpapiProtectedConfigurationProvider : ProtectedConfigurationProvider
	{
		private bool useMachineProtection;

		private const string NotSupportedReason = "DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.";

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Configuration.DpapiProtectedConfigurationProvider" /> object is using machine-specific or user-account-specific protection.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.DpapiProtectedConfigurationProvider" /> is using machine-specific protection; <see langword="false" /> if it is using user-account-specific protection.</returns>
		public bool UseMachineProtection => useMachineProtection;

		/// <summary>Decrypts the passed <see cref="T:System.Xml.XmlNode" /> object.</summary>
		/// <param name="encryptedNode">The <see cref="T:System.Xml.XmlNode" /> object to decrypt.</param>
		/// <returns>A decrypted <see cref="T:System.Xml.XmlNode" /> object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="encryptedNode" /> does not have <see cref="P:System.Xml.XmlNode.Name" /> set to "EncryptedData" and <see cref="T:System.Xml.XmlNodeType" /> set to <see cref="F:System.Xml.XmlNodeType.Element" />.  
		/// -or-
		///  <paramref name="encryptedNode" /> does not have a child node named "CipherData" with a child node named "CipherValue".  
		/// -or-
		///  The child node named "CipherData" is an empty node.</exception>
		[System.MonoNotSupported("DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.")]
		public override XmlNode Decrypt(XmlNode encryptedNode)
		{
			throw new NotSupportedException("DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.");
		}

		/// <summary>Encrypts the passed <see cref="T:System.Xml.XmlNode" /> object.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object to encrypt.</param>
		/// <returns>An encrypted <see cref="T:System.Xml.XmlNode" /> object.</returns>
		[System.MonoNotSupported("DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.")]
		public override XmlNode Encrypt(XmlNode node)
		{
			throw new NotSupportedException("DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.");
		}

		/// <summary>Initializes the provider with default settings.</summary>
		/// <param name="name">The provider name to use for the object.</param>
		/// <param name="configurationValues">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> collection of values to use when initializing the object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="configurationValues" /> contains an unrecognized configuration setting.</exception>
		[System.MonoTODO]
		public override void Initialize(string name, NameValueCollection configurationValues)
		{
			base.Initialize(name, configurationValues);
			string text = configurationValues["useMachineProtection"];
			if (text != null && text.ToLowerInvariant() == "true")
			{
				useMachineProtection = true;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.DpapiProtectedConfigurationProvider" /> class using default settings.</summary>
		public DpapiProtectedConfigurationProvider()
		{
		}
	}
	/// <summary>Contains meta-information about an individual element within the configuration. This class cannot be inherited.</summary>
	public sealed class ElementInformation
	{
		private readonly PropertyInformation propertyInfo;

		private readonly ConfigurationElement owner;

		private readonly PropertyInformationCollection properties;

		/// <summary>Gets the errors for the associated element and subelements</summary>
		/// <returns>The collection containing the errors for the associated element and subelements</returns>
		[System.MonoTODO]
		public ICollection Errors
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is a <see cref="T:System.Configuration.ConfigurationElementCollection" /> collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is a <see cref="T:System.Configuration.ConfigurationElementCollection" /> collection; otherwise, <see langword="false" />.</returns>
		public bool IsCollection => owner is ConfigurationElementCollection;

		/// <summary>Gets a value that indicates whether the associated <see cref="T:System.Configuration.ConfigurationElement" /> object cannot be modified.</summary>
		/// <returns>
		///   <see langword="true" /> if the associated <see cref="T:System.Configuration.ConfigurationElement" /> object cannot be modified; otherwise, <see langword="false" />.</returns>
		public bool IsLocked
		{
			get
			{
				if (propertyInfo == null)
				{
					return false;
				}
				return propertyInfo.IsLocked;
			}
		}

		/// <summary>Gets a value indicating whether the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is in the configuration file.</summary>
		/// <returns>
		///   <see langword="true" /> if the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is in the configuration file; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO("Support multiple levels of inheritance")]
		public bool IsPresent => owner.IsElementPresent;

		/// <summary>Gets the line number in the configuration file where the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is defined.</summary>
		/// <returns>The line number in the configuration file where the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is defined.</returns>
		public int LineNumber
		{
			get
			{
				if (propertyInfo == null)
				{
					return 0;
				}
				return propertyInfo.LineNumber;
			}
		}

		/// <summary>Gets the source file where the associated <see cref="T:System.Configuration.ConfigurationElement" /> object originated.</summary>
		/// <returns>The source file where the associated <see cref="T:System.Configuration.ConfigurationElement" /> object originated.</returns>
		public string Source
		{
			get
			{
				if (propertyInfo == null)
				{
					return null;
				}
				return propertyInfo.Source;
			}
		}

		/// <summary>Gets the type of the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>The type of the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</returns>
		public Type Type
		{
			get
			{
				if (propertyInfo == null)
				{
					return owner.GetType();
				}
				return propertyInfo.Type;
			}
		}

		/// <summary>Gets the object used to validate the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>The object used to validate the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</returns>
		public ConfigurationValidatorBase Validator
		{
			get
			{
				if (propertyInfo == null)
				{
					return new DefaultValidator();
				}
				return propertyInfo.Validator;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.PropertyInformationCollection" /> collection of the properties in the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.PropertyInformationCollection" /> collection of the properties in the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</returns>
		public PropertyInformationCollection Properties => properties;

		internal ElementInformation(ConfigurationElement owner, PropertyInformation propertyInfo)
		{
			this.propertyInfo = propertyInfo;
			this.owner = owner;
			properties = new PropertyInformationCollection();
			foreach (ConfigurationProperty property in owner.Properties)
			{
				properties.Add(new PropertyInformation(owner, property));
			}
		}

		internal void Reset(ElementInformation parentInfo)
		{
			foreach (PropertyInformation property in Properties)
			{
				PropertyInformation parentProperty = parentInfo.Properties[property.Name];
				property.Reset(parentProperty);
			}
		}

		internal ElementInformation()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Defines the configuration file mapping for an .exe application. This class cannot be inherited.</summary>
	public sealed class ExeConfigurationFileMap : ConfigurationFileMap
	{
		private string exeConfigFilename;

		private string localUserConfigFilename;

		private string roamingUserConfigFilename;

		/// <summary>Gets or sets the name of the configuration file.</summary>
		/// <returns>The configuration file name.</returns>
		public string ExeConfigFilename
		{
			get
			{
				return exeConfigFilename;
			}
			set
			{
				exeConfigFilename = value;
			}
		}

		/// <summary>Gets or sets the name of the configuration file for the local user.</summary>
		/// <returns>The configuration file name.</returns>
		public string LocalUserConfigFilename
		{
			get
			{
				return localUserConfigFilename;
			}
			set
			{
				localUserConfigFilename = value;
			}
		}

		/// <summary>Gets or sets the name of the configuration file for the roaming user.</summary>
		/// <returns>The configuration file name.</returns>
		public string RoamingUserConfigFilename
		{
			get
			{
				return roamingUserConfigFilename;
			}
			set
			{
				roamingUserConfigFilename = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ExeConfigurationFileMap" /> class.</summary>
		public ExeConfigurationFileMap()
		{
			exeConfigFilename = "";
			localUserConfigFilename = "";
			roamingUserConfigFilename = "";
		}

		/// <summary>Creates a copy of the existing <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object.</summary>
		/// <returns>An <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object.</returns>
		public override object Clone()
		{
			return new ExeConfigurationFileMap
			{
				exeConfigFilename = exeConfigFilename,
				localUserConfigFilename = localUserConfigFilename,
				roamingUserConfigFilename = roamingUserConfigFilename,
				MachineConfigFilename = base.MachineConfigFilename
			};
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ExeConfigurationFileMap" /> class by using the specified machine configuration file name.</summary>
		/// <param name="machineConfigFileName">The name of the machine configuration file that includes the complete physical path (for example, <c>c:\Windows\Microsoft.NET\Framework\v2.0.50727\CONFIG\machine.config</c>).</param>
		public ExeConfigurationFileMap(string machineConfigFileName)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Manages the path context for the current application. This class cannot be inherited.</summary>
	public sealed class ExeContext
	{
		private string path;

		private ConfigurationUserLevel level;

		/// <summary>Gets the current path for the application.</summary>
		/// <returns>A string value containing the current path.</returns>
		public string ExePath => path;

		/// <summary>Gets an object representing the path level of the current application.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationUserLevel" /> object representing the path level of the current application.</returns>
		public ConfigurationUserLevel UserLevel => level;

		internal ExeContext(string path, ConfigurationUserLevel level)
		{
			this.path = path;
			this.level = level;
		}

		internal ExeContext()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Converts between a string and an enumeration type.</summary>
	public sealed class GenericEnumConverter : ConfigurationConverterBase
	{
		private Type typeEnum;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.GenericEnumConverter" /> class.</summary>
		/// <param name="typeEnum">The enumeration type to convert.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeEnum" /> is <see langword="null" />.</exception>
		public GenericEnumConverter(Type typeEnum)
		{
			if (typeEnum == null)
			{
				throw new ArgumentNullException("typeEnum");
			}
			this.typeEnum = typeEnum;
		}

		/// <summary>Converts a <see cref="T:System.String" /> to an <see cref="T:System.Enum" /> type.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <returns>The <see cref="T:System.Enum" /> type that represents the <paramref name="data" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="data" /> is null or an empty string ("").  
		/// -or-
		///  <paramref name="data" /> starts with a numeric character.  
		/// -or-
		///  <paramref name="data" /> includes white space.</exception>
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			if (data == null)
			{
				throw new ArgumentException();
			}
			return Enum.Parse(typeEnum, (string)data);
		}

		/// <summary>Converts an <see cref="T:System.Enum" /> type to a <see cref="T:System.String" /> value.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert to.</param>
		/// <param name="type">The type to convert to.</param>
		/// <returns>The <see cref="T:System.String" /> that represents the <paramref name="value" /> parameter.</returns>
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			return value.ToString();
		}
	}
	internal interface IConfigXmlNode
	{
		string Filename { get; }

		int LineNumber { get; }
	}
	/// <summary>Provides a wrapper type definition for configuration sections that are not handled by the <see cref="N:System.Configuration" /> types.</summary>
	public sealed class IgnoreSection : ConfigurationSection
	{
		private string xml;

		private static ConfigurationPropertyCollection properties;

		protected internal override ConfigurationPropertyCollection Properties => properties;

		static IgnoreSection()
		{
			properties = new ConfigurationPropertyCollection();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IgnoreSection" /> class.</summary>
		public IgnoreSection()
		{
		}

		protected internal override bool IsModified()
		{
			return false;
		}

		protected internal override void DeserializeSection(XmlReader xmlReader)
		{
			xml = xmlReader.ReadOuterXml();
		}

		[System.MonoTODO]
		protected internal override void Reset(ConfigurationElement parentSection)
		{
			base.Reset(parentSection);
		}

		[System.MonoTODO]
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		protected internal override string SerializeSection(ConfigurationElement parentSection, string name, ConfigurationSaveMode saveMode)
		{
			return xml;
		}
	}
	/// <summary>Converts between a string and the standard infinite or integer value.</summary>
	public sealed class InfiniteIntConverter : ConfigurationConverterBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.InfiniteIntConverter" /> class.</summary>
		public InfiniteIntConverter()
		{
		}

		/// <summary>Converts a <see cref="T:System.String" /> to an <see cref="T:System.Int32" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <returns>The <see cref="F:System.Int32.MaxValue" />, if the <paramref name="data" /> parameter is the <see cref="T:System.String" /> "infinite"; otherwise, the <see cref="T:System.Int32" /> representing the <paramref name="data" /> parameter integer value.</returns>
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			if ((string)data == "Infinite")
			{
				return int.MaxValue;
			}
			return Convert.ToInt32((string)data, 10);
		}

		/// <summary>Converts an <see cref="T:System.Int32" />.to a <see cref="T:System.String" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert to.</param>
		/// <param name="type">The type to convert to.</param>
		/// <returns>The <see cref="T:System.String" /> "infinite" if the <paramref name="value" /> is <see cref="F:System.Int32.MaxValue" />; otherwise, the <see cref="T:System.String" /> representing the <paramref name="value" /> parameter.</returns>
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value.GetType() != typeof(int))
			{
				throw new ArgumentException();
			}
			if ((int)value == int.MaxValue)
			{
				return "Infinite";
			}
			return value.ToString();
		}
	}
	/// <summary>Converts between a string and the standard infinite <see cref="T:System.TimeSpan" /> value.</summary>
	public sealed class InfiniteTimeSpanConverter : ConfigurationConverterBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.InfiniteTimeSpanConverter" /> class.</summary>
		public InfiniteTimeSpanConverter()
		{
		}

		/// <summary>Converts a <see cref="T:System.String" /> to a <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <returns>The <see cref="F:System.TimeSpan.MaxValue" />, if the <paramref name="data" /> parameter is the <see cref="T:System.String" /> infinite; otherwise, the <see cref="T:System.TimeSpan" /> representing the <paramref name="data" /> parameter in minutes.</returns>
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			if ((string)data == "Infinite")
			{
				return TimeSpan.MaxValue;
			}
			return TimeSpan.Parse((string)data);
		}

		/// <summary>Converts a <see cref="T:System.TimeSpan" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> used during object conversion.</param>
		/// <param name="value">The value to convert.</param>
		/// <param name="type">The conversion type.</param>
		/// <returns>The <see cref="T:System.String" /> "infinite", if the <paramref name="value" /> parameter is <see cref="F:System.TimeSpan.MaxValue" />; otherwise, the <see cref="T:System.String" /> representing the <paramref name="value" /> parameter in minutes.</returns>
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value.GetType() != typeof(TimeSpan))
			{
				throw new ArgumentException();
			}
			if ((TimeSpan)value == TimeSpan.MaxValue)
			{
				return "Infinite";
			}
			return value.ToString();
		}
	}
	/// <summary>Provides validation of an <see cref="T:System.Int32" /> value.</summary>
	public class IntegerValidator : ConfigurationValidatorBase
	{
		private bool rangeIsExclusive;

		private int minValue;

		private int maxValue = int.MaxValue;

		private int resolution;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IntegerValidator" /> class.</summary>
		/// <param name="minValue">An <see cref="T:System.Int32" /> object that specifies the minimum length of the integer value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int32" /> object that specifies the maximum length of the integer value.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		/// <param name="resolution">An <see cref="T:System.Int32" /> object that specifies a value that must be matched.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="resolution" /> is less than <see langword="0" />.  
		/// -or-
		///  <paramref name="minValue" /> is greater than <paramref name="maxValue" />.</exception>
		public IntegerValidator(int minValue, int maxValue, bool rangeIsExclusive, int resolution)
		{
			if (minValue != 0)
			{
				this.minValue = minValue;
			}
			if (maxValue != 0)
			{
				this.maxValue = maxValue;
			}
			this.rangeIsExclusive = rangeIsExclusive;
			this.resolution = resolution;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IntegerValidator" /> class.</summary>
		/// <param name="minValue">An <see cref="T:System.Int32" /> object that specifies the minimum value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int32" /> object that specifies the maximum value.</param>
		/// <param name="rangeIsExclusive">
		///   <see langword="true" /> to specify that the validation range is exclusive. Inclusive means the value to be validated must be within the specified range; exclusive means that it must be below the minimum or above the maximum.</param>
		public IntegerValidator(int minValue, int maxValue, bool rangeIsExclusive)
			: this(minValue, maxValue, rangeIsExclusive, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IntegerValidator" /> class.</summary>
		/// <param name="minValue">An <see cref="T:System.Int32" /> object that specifies the minimum value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int32" /> object that specifies the maximum value.</param>
		public IntegerValidator(int minValue, int maxValue)
			: this(minValue, maxValue, rangeIsExclusive: false, 0)
		{
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <param name="type">The type of the object.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="type" /> parameter matches an <see cref="T:System.Int32" /> value; otherwise, <see langword="false" />.</returns>
		public override bool CanValidate(Type type)
		{
			return type == typeof(int);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value to be validated.</param>
		public override void Validate(object value)
		{
			int num = (int)value;
			if (!rangeIsExclusive)
			{
				if (num < minValue || num > maxValue)
				{
					throw new ArgumentException("The value must be in the range " + minValue + " - " + maxValue);
				}
			}
			else if (num >= minValue && num <= maxValue)
			{
				throw new ArgumentException("The value must not be in the range " + minValue + " - " + maxValue);
			}
			if (resolution != 0 && num % resolution != 0)
			{
				throw new ArgumentException("The value must have a resolution of " + resolution);
			}
		}
	}
	/// <summary>Declaratively instructs the .NET Framework to perform integer validation on a configuration property. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class IntegerValidatorAttribute : ConfigurationValidatorAttribute
	{
		private bool excludeRange;

		private int maxValue;

		private int minValue;

		private ConfigurationValidatorBase instance;

		/// <summary>Gets or sets a value that indicates whether to include or exclude the integers in the range defined by the <see cref="P:System.Configuration.IntegerValidatorAttribute.MinValue" /> and <see cref="P:System.Configuration.IntegerValidatorAttribute.MaxValue" /> property values.</summary>
		/// <returns>
		///   <see langword="true" /> if the value must be excluded; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public bool ExcludeRange
		{
			get
			{
				return excludeRange;
			}
			set
			{
				excludeRange = value;
				instance = null;
			}
		}

		/// <summary>Gets or sets the maximum value allowed for the property.</summary>
		/// <returns>An integer that indicates the allowed maximum value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see cref="P:System.Configuration.IntegerValidatorAttribute.MinValue" />.</exception>
		public int MaxValue
		{
			get
			{
				return maxValue;
			}
			set
			{
				maxValue = value;
				instance = null;
			}
		}

		/// <summary>Gets or sets the minimum value allowed for the property.</summary>
		/// <returns>An integer that indicates the allowed minimum value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is greater than <see cref="P:System.Configuration.IntegerValidatorAttribute.MaxValue" />.</exception>
		public int MinValue
		{
			get
			{
				return minValue;
			}
			set
			{
				minValue = value;
				instance = null;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.IntegerValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (instance == null)
				{
					instance = new IntegerValidator(minValue, maxValue, excludeRange);
				}
				return instance;
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.IntegerValidatorAttribute" /> class.</summary>
		public IntegerValidatorAttribute()
		{
		}
	}
	internal class InternalConfigurationFactory : IInternalConfigConfigurationFactory
	{
		public Configuration Create(Type typeConfigHost, params object[] hostInitConfigurationParams)
		{
			InternalConfigurationSystem internalConfigurationSystem = new InternalConfigurationSystem();
			internalConfigurationSystem.Init(typeConfigHost, hostInitConfigurationParams);
			return new Configuration(internalConfigurationSystem, null);
		}

		public string NormalizeLocationSubPath(string subPath, IConfigErrorInfo errorInfo)
		{
			return subPath;
		}
	}
	internal class InternalConfigurationSystem : IConfigSystem
	{
		private IInternalConfigHost host;

		private IInternalConfigRoot root;

		private object[] hostInitParams;

		public IInternalConfigHost Host => host;

		public IInternalConfigRoot Root => root;

		public void Init(Type typeConfigHost, params object[] hostInitParams)
		{
			this.hostInitParams = hostInitParams;
			host = (IInternalConfigHost)Activator.CreateInstance(typeConfigHost);
			root = new InternalConfigurationRoot();
			root.Init(host, isDesignTime: false);
		}

		public void InitForConfiguration(ref string locationConfigPath, out string parentConfigPath, out string parentLocationConfigPath)
		{
			host.InitForConfiguration(ref locationConfigPath, out parentConfigPath, out parentLocationConfigPath, root, hostInitParams);
		}
	}
	internal abstract class InternalConfigurationHost : IInternalConfigHost
	{
		public virtual bool IsRemote
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public virtual bool SupportsChangeNotifications => false;

		public virtual bool SupportsLocation => false;

		public virtual bool SupportsPath => false;

		public virtual bool SupportsRefresh => false;

		public virtual object CreateConfigurationContext(string configPath, string locationSubPath)
		{
			return null;
		}

		public virtual object CreateDeprecatedConfigContext(string configPath)
		{
			return null;
		}

		public virtual void DeleteStream(string streamName)
		{
			File.Delete(streamName);
		}

		string IInternalConfigHost.DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedSection)
		{
			return protectedSection.DecryptSection(encryptedXml, protectionProvider);
		}

		string IInternalConfigHost.EncryptSection(string clearXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedSection)
		{
			return protectedSection.EncryptSection(clearXml, protectionProvider);
		}

		public virtual string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath)
		{
			return configPath;
		}

		public virtual Type GetConfigType(string typeName, bool throwOnError)
		{
			Type type = Type.GetType(typeName);
			if (type == null)
			{
				type = Type.GetType(typeName + ",System");
			}
			if (type == null && throwOnError)
			{
				throw new ConfigurationErrorsException("Type '" + typeName + "' not found.");
			}
			return type;
		}

		public virtual string GetConfigTypeName(Type t)
		{
			return t.AssemblyQualifiedName;
		}

		public virtual void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			throw new NotImplementedException();
		}

		public abstract string GetStreamName(string configPath);

		public abstract void Init(IInternalConfigRoot root, params object[] hostInitParams);

		public abstract void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams);

		[System.MonoNotSupported("mono does not support remote configuration")]
		public virtual string GetStreamNameForConfigSource(string streamName, string configSource)
		{
			throw new NotSupportedException("mono does not support remote configuration");
		}

		public virtual object GetStreamVersion(string streamName)
		{
			throw new NotImplementedException();
		}

		public virtual IDisposable Impersonate()
		{
			throw new NotImplementedException();
		}

		public virtual bool IsAboveApplication(string configPath)
		{
			throw new NotImplementedException();
		}

		public virtual bool IsConfigRecordRequired(string configPath)
		{
			throw new NotImplementedException();
		}

		public virtual bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			switch (allowDefinition)
			{
			case ConfigurationAllowDefinition.MachineOnly:
				return configPath == "machine";
			case ConfigurationAllowDefinition.MachineToApplication:
				if (!(configPath == "machine"))
				{
					return configPath == "exe";
				}
				return true;
			default:
				return true;
			}
		}

		public virtual bool IsFile(string streamName)
		{
			throw new NotImplementedException();
		}

		public virtual bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		public virtual bool IsInitDelayed(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		public virtual bool IsLocationApplicable(string configPath)
		{
			throw new NotImplementedException();
		}

		public virtual bool IsSecondaryRoot(string configPath)
		{
			throw new NotImplementedException();
		}

		public virtual bool IsTrustedConfigPath(string configPath)
		{
			throw new NotImplementedException();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_bundled_machine_config();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_bundled_app_config();

		public virtual Stream OpenStreamForRead(string streamName)
		{
			if (string.CompareOrdinal(streamName, RuntimeEnvironment.SystemConfigurationFile) == 0)
			{
				string bundled_machine_config = get_bundled_machine_config();
				if (bundled_machine_config != null)
				{
					return new MemoryStream(Encoding.UTF8.GetBytes(bundled_machine_config));
				}
			}
			if (string.CompareOrdinal(streamName, AppDomain.CurrentDomain.SetupInformation.ConfigurationFile) == 0)
			{
				string bundled_app_config = get_bundled_app_config();
				if (bundled_app_config != null)
				{
					return new MemoryStream(Encoding.UTF8.GetBytes(bundled_app_config));
				}
			}
			if (!File.Exists(streamName))
			{
				return null;
			}
			return new FileStream(streamName, FileMode.Open, FileAccess.Read);
		}

		public virtual Stream OpenStreamForRead(string streamName, bool assertPermissions)
		{
			throw new NotImplementedException();
		}

		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
		{
			string directoryName = Path.GetDirectoryName(streamName);
			if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			return new FileStream(streamName, FileMode.Create, FileAccess.Write);
		}

		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions)
		{
			throw new NotImplementedException();
		}

		public virtual bool PrefetchAll(string configPath, string streamName)
		{
			throw new NotImplementedException();
		}

		public virtual bool PrefetchSection(string sectionGroupName, string sectionName)
		{
			throw new NotImplementedException();
		}

		public virtual void RequireCompleteInit(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		public virtual object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			throw new NotImplementedException();
		}

		public virtual void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			throw new NotImplementedException();
		}

		public virtual void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo)
		{
			if (!IsDefinitionAllowed(configPath, allowDefinition, allowExeDefinition))
			{
				throw new ConfigurationErrorsException("The section can't be defined in this file (the allowed definition context is '" + allowDefinition.ToString() + "').", errorInfo.Filename, errorInfo.LineNumber);
			}
		}

		public virtual void WriteCompleted(string streamName, bool success, object writeContext)
		{
		}

		public virtual void WriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions)
		{
		}
	}
	internal class ExeConfigurationHost : InternalConfigurationHost
	{
		private ExeConfigurationFileMap map;

		private ConfigurationUserLevel level;

		public override void Init(IInternalConfigRoot root, params object[] hostInitParams)
		{
			map = (ExeConfigurationFileMap)hostInitParams[0];
			level = (ConfigurationUserLevel)hostInitParams[1];
			CheckFileMap(level, map);
		}

		private static void CheckFileMap(ConfigurationUserLevel level, ExeConfigurationFileMap map)
		{
			if (level != ConfigurationUserLevel.None)
			{
				switch (level)
				{
				default:
					return;
				case ConfigurationUserLevel.PerUserRoamingAndLocal:
					if (string.IsNullOrEmpty(map.LocalUserConfigFilename))
					{
						throw new ArgumentException("The 'LocalUserConfigFilename' argument cannot be null.");
					}
					break;
				case ConfigurationUserLevel.PerUserRoaming:
					break;
				}
				if (string.IsNullOrEmpty(map.RoamingUserConfigFilename))
				{
					throw new ArgumentException("The 'RoamingUserConfigFilename' argument cannot be null.");
				}
			}
			if (string.IsNullOrEmpty(map.ExeConfigFilename))
			{
				throw new ArgumentException("The 'ExeConfigFilename' argument cannot be null.");
			}
		}

		public override string GetStreamName(string configPath)
		{
			return configPath switch
			{
				"exe" => map.ExeConfigFilename, 
				"local" => map.LocalUserConfigFilename, 
				"roaming" => map.RoamingUserConfigFilename, 
				"machine" => map.MachineConfigFilename, 
				_ => level switch
				{
					ConfigurationUserLevel.None => map.ExeConfigFilename, 
					ConfigurationUserLevel.PerUserRoaming => map.RoamingUserConfigFilename, 
					ConfigurationUserLevel.PerUserRoamingAndLocal => map.LocalUserConfigFilename, 
					_ => map.MachineConfigFilename, 
				}, 
			};
		}

		public override void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams)
		{
			map = (ExeConfigurationFileMap)hostInitConfigurationParams[0];
			if (hostInitConfigurationParams.Length > 1 && hostInitConfigurationParams[1] is ConfigurationUserLevel)
			{
				level = (ConfigurationUserLevel)hostInitConfigurationParams[1];
			}
			CheckFileMap(level, map);
			if (locationSubPath == null)
			{
				switch (level)
				{
				case ConfigurationUserLevel.PerUserRoaming:
					if (map.RoamingUserConfigFilename == null)
					{
						throw new ArgumentException("RoamingUserConfigFilename must be set correctly");
					}
					locationSubPath = "roaming";
					break;
				case ConfigurationUserLevel.PerUserRoamingAndLocal:
					if (map.LocalUserConfigFilename == null)
					{
						throw new ArgumentException("LocalUserConfigFilename must be set correctly");
					}
					locationSubPath = "local";
					break;
				}
			}
			if (locationSubPath == "exe" || (locationSubPath == null && map.ExeConfigFilename != null))
			{
				configPath = "exe";
				locationSubPath = "machine";
				locationConfigPath = map.ExeConfigFilename;
				return;
			}
			if (locationSubPath == "local" && map.LocalUserConfigFilename != null)
			{
				configPath = "local";
				locationSubPath = "roaming";
				locationConfigPath = map.LocalUserConfigFilename;
				return;
			}
			if (locationSubPath == "roaming" && map.RoamingUserConfigFilename != null)
			{
				configPath = "roaming";
				locationSubPath = "exe";
				locationConfigPath = map.RoamingUserConfigFilename;
				return;
			}
			if (locationSubPath == "machine" && map.MachineConfigFilename != null)
			{
				configPath = "machine";
				locationSubPath = null;
				locationConfigPath = null;
				return;
			}
			throw new NotImplementedException();
		}
	}
	internal class MachineConfigurationHost : InternalConfigurationHost
	{
		private ConfigurationFileMap map;

		public override void Init(IInternalConfigRoot root, params object[] hostInitParams)
		{
			map = (ConfigurationFileMap)hostInitParams[0];
		}

		public override string GetStreamName(string configPath)
		{
			return map.MachineConfigFilename;
		}

		public override void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams)
		{
			map = (ConfigurationFileMap)hostInitConfigurationParams[0];
			locationSubPath = null;
			configPath = null;
			locationConfigPath = null;
		}

		public override bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			return true;
		}
	}
	internal class InternalConfigurationRoot : IInternalConfigRoot
	{
		private IInternalConfigHost host;

		private bool isDesignTime;

		public bool IsDesignTime => isDesignTime;

		public event InternalConfigEventHandler ConfigChanged;

		public event InternalConfigEventHandler ConfigRemoved;

		[System.MonoTODO]
		public IInternalConfigRecord GetConfigRecord(string configPath)
		{
			throw new NotImplementedException();
		}

		public object GetSection(string section, string configPath)
		{
			return GetConfigRecord(configPath).GetSection(section);
		}

		[System.MonoTODO]
		public string GetUniqueConfigPath(string configPath)
		{
			return configPath;
		}

		[System.MonoTODO]
		public IInternalConfigRecord GetUniqueConfigRecord(string configPath)
		{
			return GetConfigRecord(GetUniqueConfigPath(configPath));
		}

		public void Init(IInternalConfigHost host, bool isDesignTime)
		{
			this.host = host;
			this.isDesignTime = isDesignTime;
		}

		[System.MonoTODO]
		public void RemoveConfig(string configPath)
		{
			host.DeleteStream(configPath);
			if (this.ConfigRemoved != null)
			{
				this.ConfigRemoved(this, new InternalConfigEventArgs(configPath));
			}
		}
	}
	/// <summary>Contains a collection of <see cref="T:System.Configuration.KeyValueConfigurationElement" /> objects.</summary>
	[ConfigurationCollection(typeof(KeyValueConfigurationElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public class KeyValueConfigurationCollection : ConfigurationElementCollection
	{
		private ConfigurationPropertyCollection properties;

		/// <summary>Gets the keys to all items contained in the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> collection.</summary>
		/// <returns>A string array.</returns>
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				int num = 0;
				IEnumerator enumerator = GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						KeyValueConfigurationElement keyValueConfigurationElement = (KeyValueConfigurationElement)enumerator.Current;
						array[num++] = keyValueConfigurationElement.Key;
					}
					return array;
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object based on the supplied parameter.</summary>
		/// <param name="key">The key of the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> contained in the collection.</param>
		/// <returns>A configuration element, or <see langword="null" /> if the key does not exist in the collection.</returns>
		public new KeyValueConfigurationElement this[string key] => (KeyValueConfigurationElement)BaseGet(key);

		/// <summary>Gets a collection of configuration properties.</summary>
		/// <returns>A collection of configuration properties.</returns>
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (properties == null)
				{
					properties = new ConfigurationPropertyCollection();
				}
				return properties;
			}
		}

		/// <summary>Gets a value indicating whether an attempt to add a duplicate <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> collection will cause an exception to be thrown.</summary>
		/// <returns>
		///   <see langword="true" /> if an attempt to add a duplicate <see cref="T:System.Configuration.KeyValueConfigurationElement" /> to the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> will cause an exception to be thrown; otherwise, <see langword="false" />.</returns>
		protected override bool ThrowOnDuplicate => false;

		/// <summary>Adds a <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to the collection based on the supplied parameters.</summary>
		/// <param name="keyValue">A <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</param>
		public void Add(KeyValueConfigurationElement keyValue)
		{
			keyValue.Init();
			BaseAdd(keyValue);
		}

		/// <summary>Adds a <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to the collection based on the supplied parameters.</summary>
		/// <param name="key">A string specifying the key.</param>
		/// <param name="value">A string specifying the value.</param>
		public void Add(string key, string value)
		{
			Add(new KeyValueConfigurationElement(key, value));
		}

		/// <summary>Clears the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> collection.</summary>
		public void Clear()
		{
			BaseClear();
		}

		/// <summary>Removes a <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object from the collection.</summary>
		/// <param name="key">A string specifying the <paramref name="key" />.</param>
		public void Remove(string key)
		{
			BaseRemove(key);
		}

		/// <summary>When overridden in a derived class, the <see cref="M:System.Configuration.KeyValueConfigurationCollection.CreateNewElement" /> method creates a new <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object.</summary>
		/// <returns>A newly created <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new KeyValueConfigurationElement();
		}

		/// <summary>Gets the element key for a specified configuration element when overridden in a derived class.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.KeyValueConfigurationElement" /> to which the key should be returned.</param>
		/// <returns>An object that acts as the key for the specified <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((KeyValueConfigurationElement)element).Key;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> class.</summary>
		public KeyValueConfigurationCollection()
		{
		}
	}
	/// <summary>Represents a configuration element that contains a key/value pair.</summary>
	public class KeyValueConfigurationElement : ConfigurationElement
	{
		private static ConfigurationProperty keyProp;

		private static ConfigurationProperty valueProp;

		private static ConfigurationPropertyCollection properties;

		/// <summary>Gets the key of the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object.</summary>
		/// <returns>The key of the <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</returns>
		[ConfigurationProperty("key", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
		public string Key => (string)base[keyProp];

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object.</summary>
		/// <returns>The value of the <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</returns>
		[ConfigurationProperty("value", DefaultValue = "")]
		public string Value
		{
			get
			{
				return (string)base[valueProp];
			}
			set
			{
				base[valueProp] = value;
			}
		}

		/// <summary>Gets the collection of properties.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> of properties for the element.</returns>
		protected internal override ConfigurationPropertyCollection Properties => properties;

		static KeyValueConfigurationElement()
		{
			keyProp = new ConfigurationProperty("key", typeof(string), "", ConfigurationPropertyOptions.IsKey);
			valueProp = new ConfigurationProperty("value", typeof(string), "");
			properties = new ConfigurationPropertyCollection();
			properties.Add(keyProp);
			properties.Add(valueProp);
		}

		internal KeyValueConfigurationElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> class based on the supplied parameters.</summary>
		/// <param name="key">The key of the <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</param>
		/// <param name="value">The value of the <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</param>
		public KeyValueConfigurationElement(string key, string value)
		{
			base[keyProp] = key;
			base[valueProp] = value;
		}

		/// <summary>Sets the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to its initial state.</summary>
		[System.MonoTODO]
		protected internal override void Init()
		{
		}
	}
	internal class KeyValueInternalCollection : NameValueCollection
	{
		public void SetReadOnly()
		{
			base.IsReadOnly = true;
		}

		public override void Add(string name, string val)
		{
			Remove(name);
			base.Add(name, val);
		}
	}
	/// <summary>Provides validation of an <see cref="T:System.Int64" /> value.</summary>
	public class LongValidator : ConfigurationValidatorBase
	{
		private bool rangeIsExclusive;

		private long minValue;

		private long maxValue;

		private long resolution;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.LongValidator" /> class.</summary>
		/// <param name="minValue">An <see cref="T:System.Int64" /> value that specifies the minimum length of the <see langword="long" /> value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int64" /> value that specifies the maximum length of the <see langword="long" /> value.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		/// <param name="resolution">An <see cref="T:System.Int64" /> value that specifies a specific value that must be matched.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="resolution" /> is equal to or less than <see langword="0" />.  
		/// -or-
		///  <paramref name="maxValue" /> is less than <paramref name="minValue" />.</exception>
		public LongValidator(long minValue, long maxValue, bool rangeIsExclusive, long resolution)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.rangeIsExclusive = rangeIsExclusive;
			this.resolution = resolution;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.LongValidator" /> class.</summary>
		/// <param name="minValue">An <see cref="T:System.Int64" /> value that specifies the minimum length of the <see langword="long" /> value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int64" /> value that specifies the maximum length of the <see langword="long" /> value.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		public LongValidator(long minValue, long maxValue, bool rangeIsExclusive)
			: this(minValue, maxValue, rangeIsExclusive, 0L)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.LongValidator" /> class.</summary>
		/// <param name="minValue">An <see cref="T:System.Int64" /> value that specifies the minimum length of the <see langword="long" /> value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int64" /> value that specifies the maximum length of the <see langword="long" /> value.</param>
		public LongValidator(long minValue, long maxValue)
			: this(minValue, maxValue, rangeIsExclusive: false, 0L)
		{
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <param name="type">The type of object.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="type" /> parameter matches an <see cref="T:System.Int64" /> value; otherwise, <see langword="false" />.</returns>
		public override bool CanValidate(Type type)
		{
			return type == typeof(long);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		public override void Validate(object value)
		{
			long num = (long)value;
			if (!rangeIsExclusive)
			{
				if (num < minValue || num > maxValue)
				{
					throw new ArgumentException("The value must be in the range " + minValue + " - " + maxValue);
				}
			}
			else if (num >= minValue && num <= maxValue)
			{
				throw new ArgumentException("The value must not be in the range " + minValue + " - " + maxValue);
			}
			if (resolution != 0L && num % resolution != 0L)
			{
				throw new ArgumentException("The value must have a resolution of " + resolution);
			}
		}
	}
	/// <summary>Declaratively instructs the .NET Framework to perform long-integer validation on a configuration property. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class LongValidatorAttribute : ConfigurationValidatorAttribute
	{
		private bool excludeRange;

		private long maxValue;

		private long minValue;

		private ConfigurationValidatorBase instance;

		/// <summary>Gets or sets a value that indicates whether to include or exclude the integers in the range defined by the <see cref="P:System.Configuration.LongValidatorAttribute.MinValue" /> and <see cref="P:System.Configuration.LongValidatorAttribute.MaxValue" /> property values.</summary>
		/// <returns>
		///   <see langword="true" /> if the value must be excluded; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public bool ExcludeRange
		{
			get
			{
				return excludeRange;
			}
			set
			{
				excludeRange = value;
				instance = null;
			}
		}

		/// <summary>Gets or sets the maximum value allowed for the property.</summary>
		/// <returns>A long integer that indicates the allowed maximum value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see cref="P:System.Configuration.LongValidatorAttribute.MinValue" />.</exception>
		public long MaxValue
		{
			get
			{
				return maxValue;
			}
			set
			{
				maxValue = value;
				instance = null;
			}
		}

		/// <summary>Gets or sets the minimum value allowed for the property.</summary>
		/// <returns>An integer that indicates the allowed minimum value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is greater than <see cref="P:System.Configuration.LongValidatorAttribute.MaxValue" />.</exception>
		public long MinValue
		{
			get
			{
				return minValue;
			}
			set
			{
				minValue = value;
				instance = null;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.LongValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (instance == null)
				{
					instance = new LongValidator(minValue, maxValue, excludeRange);
				}
				return instance;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.LongValidatorAttribute" /> class.</summary>
		public LongValidatorAttribute()
		{
		}
	}
	/// <summary>Contains a collection of <see cref="T:System.Configuration.NameValueConfigurationElement" /> objects. This class cannot be inherited.</summary>
	[ConfigurationCollection(typeof(NameValueConfigurationElement), AddItemName = "add", RemoveItemName = "remove", ClearItemsName = "clear", CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class NameValueConfigurationCollection : ConfigurationElementCollection
	{
		private static ConfigurationPropertyCollection properties;

		/// <summary>Gets the keys to all items contained in the <see cref="T:System.Configuration.NameValueConfigurationCollection" />.</summary>
		/// <returns>A string array.</returns>
		public string[] AllKeys => (string[])BaseGetAllKeys();

		/// <summary>Gets or sets the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object based on the supplied parameter.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> contained in the collection.</param>
		/// <returns>A <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</returns>
		public new NameValueConfigurationElement this[string name]
		{
			get
			{
				return (NameValueConfigurationElement)BaseGet(name);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		protected internal override ConfigurationPropertyCollection Properties => properties;

		static NameValueConfigurationCollection()
		{
			properties = new ConfigurationPropertyCollection();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.NameValueConfigurationCollection" /> class.</summary>
		public NameValueConfigurationCollection()
		{
		}

		/// <summary>Adds a <see cref="T:System.Configuration.NameValueConfigurationElement" /> object to the collection.</summary>
		/// <param name="nameValue">A  <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</param>
		public void Add(NameValueConfigurationElement nameValue)
		{
			BaseAdd(nameValue, throwIfExists: false);
		}

		/// <summary>Clears the <see cref="T:System.Configuration.NameValueConfigurationCollection" />.</summary>
		public void Clear()
		{
			BaseClear();
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new NameValueConfigurationElement("", "");
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((NameValueConfigurationElement)element).Name;
		}

		/// <summary>Removes a <see cref="T:System.Configuration.NameValueConfigurationElement" /> object from the collection based on the provided parameter.</summary>
		/// <param name="nameValue">A <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</param>
		public void Remove(NameValueConfigurationElement nameValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes a <see cref="T:System.Configuration.NameValueConfigurationElement" /> object from the collection based on the provided parameter.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</param>
		public void Remove(string name)
		{
			BaseRemove(name);
		}
	}
	/// <summary>A configuration element that contains a <see cref="T:System.String" /> name and <see cref="T:System.String" /> value. This class cannot be inherited.</summary>
	public sealed class NameValueConfigurationElement : ConfigurationElement
	{
		private static ConfigurationPropertyCollection _properties;

		private static readonly ConfigurationProperty _propName;

		private static readonly ConfigurationProperty _propValue;

		/// <summary>Gets the name of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</returns>
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
		public string Name => (string)base[_propName];

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</summary>
		/// <returns>The value of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</returns>
		[ConfigurationProperty("value", DefaultValue = "", Options = ConfigurationPropertyOptions.None)]
		public string Value
		{
			get
			{
				return (string)base[_propValue];
			}
			set
			{
				base[_propValue] = value;
			}
		}

		protected internal override ConfigurationPropertyCollection Properties => _properties;

		static NameValueConfigurationElement()
		{
			_properties = new ConfigurationPropertyCollection();
			_propName = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsKey);
			_propValue = new ConfigurationProperty("value", typeof(string), "");
			_properties.Add(_propName);
			_properties.Add(_propValue);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> class based on supplied parameters.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</param>
		/// <param name="value">The value of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</param>
		public NameValueConfigurationElement(string name, string value)
		{
			base[_propName] = name;
			base[_propValue] = value;
		}
	}
	/// <summary>Provides validation of a <see cref="T:System.TimeSpan" /> object. This class cannot be inherited.</summary>
	public class PositiveTimeSpanValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.PositiveTimeSpanValidator" /> class.</summary>
		public PositiveTimeSpanValidator()
		{
		}

		/// <summary>Determines whether the object type can be validated.</summary>
		/// <param name="type">The object type.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="type" /> parameter matches a <see cref="T:System.TimeSpan" /> object; otherwise, <see langword="false" />.</returns>
		public override bool CanValidate(Type type)
		{
			return type == typeof(TimeSpan);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> cannot be resolved as a positive <see cref="T:System.TimeSpan" /> value.</exception>
		public override void Validate(object value)
		{
			if ((TimeSpan)value <= new TimeSpan(0L))
			{
				throw new ArgumentException("The time span value must be positive");
			}
		}
	}
	/// <summary>Declaratively instructs the .NET Framework to perform time validation on a configuration property. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class PositiveTimeSpanValidatorAttribute : ConfigurationValidatorAttribute
	{
		private ConfigurationValidatorBase instance;

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.PositiveTimeSpanValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (instance == null)
				{
					instance = new PositiveTimeSpanValidator();
				}
				return instance;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.PositiveTimeSpanValidatorAttribute" /> class.</summary>
		public PositiveTimeSpanValidatorAttribute()
		{
		}
	}
	/// <summary>Contains meta-information on an individual property within the configuration. This type cannot be inherited.</summary>
	public sealed class PropertyInformation
	{
		private bool isLocked;

		private bool isModified;

		private int lineNumber;

		private string source;

		private object val;

		private PropertyValueOrigin origin;

		private readonly ConfigurationElement owner;

		private readonly ConfigurationProperty property;

		/// <summary>Gets the <see cref="T:System.ComponentModel.TypeConverter" /> object related to the configuration attribute.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> object.</returns>
		public TypeConverter Converter => property.Converter;

		/// <summary>Gets an object containing the default value related to a configuration attribute.</summary>
		/// <returns>An object containing the default value of the configuration attribute.</returns>
		public object DefaultValue => property.DefaultValue;

		/// <summary>Gets the description of the object that corresponds to a configuration attribute.</summary>
		/// <returns>The description of the configuration attribute.</returns>
		public string Description => property.Description;

		/// <summary>Gets a value specifying whether the configuration attribute is a key.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration attribute is a key; otherwise, <see langword="false" />.</returns>
		public bool IsKey => property.IsKey;

		/// <summary>Gets a value specifying whether the configuration attribute is locked.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.PropertyInformation" /> object is locked; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool IsLocked
		{
			get
			{
				return isLocked;
			}
			internal set
			{
				isLocked = value;
			}
		}

		/// <summary>Gets a value specifying whether the configuration attribute has been modified.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.PropertyInformation" /> object has been modified; otherwise, <see langword="false" />.</returns>
		public bool IsModified
		{
			get
			{
				return isModified;
			}
			internal set
			{
				isModified = value;
			}
		}

		/// <summary>Gets a value specifying whether the configuration attribute is required.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.PropertyInformation" /> object is required; otherwise, <see langword="false" />.</returns>
		public bool IsRequired => property.IsRequired;

		/// <summary>Gets the line number in the configuration file related to the configuration attribute.</summary>
		/// <returns>A line number of the configuration file.</returns>
		public int LineNumber
		{
			get
			{
				return lineNumber;
			}
			internal set
			{
				lineNumber = value;
			}
		}

		/// <summary>Gets the name of the object that corresponds to a configuration attribute.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		public string Name => property.Name;

		/// <summary>Gets the source file that corresponds to a configuration attribute.</summary>
		/// <returns>The source file of the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		public string Source
		{
			get
			{
				return source;
			}
			internal set
			{
				source = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the object that corresponds to a configuration attribute.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		public Type Type => property.Type;

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object related to the configuration attribute.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object.</returns>
		public ConfigurationValidatorBase Validator => property.Validator;

		/// <summary>Gets or sets an object containing the value related to a configuration attribute.</summary>
		/// <returns>An object containing the value for the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		public object Value
		{
			get
			{
				if (origin == PropertyValueOrigin.Default)
				{
					if (!property.IsElement)
					{
						return DefaultValue;
					}
					ConfigurationElement configurationElement = (ConfigurationElement)Activator.CreateInstance(Type, nonPublic: true);
					configurationElement.InitFromProperty(this);
					if (owner != null && owner.IsReadOnly())
					{
						configurationElement.SetReadOnly();
					}
					val = configurationElement;
					origin = PropertyValueOrigin.Inherited;
				}
				return val;
			}
			set
			{
				val = value;
				isModified = true;
				origin = PropertyValueOrigin.SetHere;
			}
		}

		internal bool IsElement => property.IsElement;

		/// <summary>Gets a <see cref="T:System.Configuration.PropertyValueOrigin" /> object related to the configuration attribute.</summary>
		/// <returns>A <see cref="T:System.Configuration.PropertyValueOrigin" /> object.</returns>
		public PropertyValueOrigin ValueOrigin => origin;

		internal ConfigurationProperty Property => property;

		internal PropertyInformation(ConfigurationElement owner, ConfigurationProperty property)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			this.owner = owner;
			this.property = property;
		}

		internal void Reset(PropertyInformation parentProperty)
		{
			if (parentProperty != null)
			{
				if (property.IsElement)
				{
					((ConfigurationElement)Value).Reset((ConfigurationElement)parentProperty.Value);
					return;
				}
				val = parentProperty.Value;
				origin = PropertyValueOrigin.Inherited;
			}
			else
			{
				origin = PropertyValueOrigin.Default;
			}
		}

		internal string GetStringValue()
		{
			return property.ConvertToString(Value);
		}

		internal void SetStringValue(string value)
		{
			val = property.ConvertFromString(value);
			if (!object.Equals(val, DefaultValue))
			{
				origin = PropertyValueOrigin.SetHere;
			}
		}

		internal PropertyInformation()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Contains a collection of <see cref="T:System.Configuration.PropertyInformation" /> objects. This class cannot be inherited.</summary>
	[Serializable]
	public sealed class PropertyInformationCollection : NameObjectCollectionBase
	{
		private class PropertyInformationEnumerator : IEnumerator
		{
			private PropertyInformationCollection collection;

			private int position;

			public object Current
			{
				get
				{
					if (position < collection.Count && position >= 0)
					{
						return collection.BaseGet(position);
					}
					throw new InvalidOperationException();
				}
			}

			public PropertyInformationEnumerator(PropertyInformationCollection collection)
			{
				this.collection = collection;
				position = -1;
			}

			public bool MoveNext()
			{
				if (++position >= collection.Count)
				{
					return false;
				}
				return true;
			}

			public void Reset()
			{
				position = -1;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.PropertyInformation" /> object in the collection, based on the specified property name.</summary>
		/// <param name="propertyName">The name of the configuration attribute contained in the <see cref="T:System.Configuration.PropertyInformationCollection" /> object.</param>
		/// <returns>A <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		public PropertyInformation this[string propertyName] => (PropertyInformation)BaseGet(propertyName);

		internal PropertyInformationCollection()
			: base(StringComparer.Ordinal)
		{
		}

		/// <summary>Copies the entire <see cref="T:System.Configuration.PropertyInformationCollection" /> collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Configuration.PropertyInformationCollection" /> collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Array.Length" /> property of <paramref name="array" /> is less than <see cref="P:System.Collections.Specialized.NameObjectCollectionBase.Count" /> + <paramref name="index" />.</exception>
		public void CopyTo(PropertyInformation[] array, int index)
		{
			((ICollection)this).CopyTo((Array)array, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> object, which is used to iterate through this <see cref="T:System.Configuration.PropertyInformationCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object, which is used to iterate through this <see cref="T:System.Configuration.PropertyInformationCollection" />.</returns>
		public override IEnumerator GetEnumerator()
		{
			return new PropertyInformationEnumerator(this);
		}

		internal void Add(PropertyInformation pi)
		{
			BaseAdd(pi.Name, pi);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the <see cref="T:System.Configuration.PropertyInformationCollection" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Configuration.PropertyInformationCollection" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Configuration.PropertyInformationCollection" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		[System.MonoTODO]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Specifies the level in the configuration hierarchy where a configuration property value originated.</summary>
	public enum PropertyValueOrigin
	{
		/// <summary>The configuration property value originates from the <see cref="P:System.Configuration.ConfigurationProperty.DefaultValue" /> property.</summary>
		Default,
		/// <summary>The configuration property value is inherited from a parent level in the configuration.</summary>
		Inherited,
		/// <summary>The configuration property value is defined at the current level of the hierarchy.</summary>
		SetHere
	}
	/// <summary>Provides access to the protected-configuration providers for the current application's configuration file.</summary>
	public static class ProtectedConfiguration
	{
		/// <summary>The name of the data protection provider.</summary>
		public const string DataProtectionProviderName = "DataProtectionConfigurationProvider";

		/// <summary>The name of the protected data section.</summary>
		public const string ProtectedDataSectionName = "configProtectedData";

		/// <summary>The name of the RSA provider.</summary>
		public const string RsaProviderName = "RsaProtectedConfigurationProvider";

		/// <summary>Gets the name of the default protected-configuration provider.</summary>
		/// <returns>The name of the default protected-configuration provider.</returns>
		public static string DefaultProvider => Section.DefaultProvider;

		/// <summary>Gets a collection of the installed protected-configuration providers.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProtectedConfigurationProviderCollection" /> collection of installed <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> objects.</returns>
		public static ProtectedConfigurationProviderCollection Providers => Section.GetAllProviders();

		internal static ProtectedConfigurationSection Section => (ProtectedConfigurationSection)ConfigurationManager.GetSection("configProtectedData");

		internal static ProtectedConfigurationProvider GetProvider(string name, bool throwOnError)
		{
			ProtectedConfigurationProvider protectedConfigurationProvider = Providers[name];
			if (protectedConfigurationProvider == null && throwOnError)
			{
				throw new Exception($"The protection provider '{name}' was not found.");
			}
			return protectedConfigurationProvider;
		}
	}
	/// <summary>Is the base class to create providers for encrypting and decrypting protected-configuration data.</summary>
	public abstract class ProtectedConfigurationProvider : ProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> class using default settings.</summary>
		protected ProtectedConfigurationProvider()
		{
		}

		/// <summary>Decrypts the passed <see cref="T:System.Xml.XmlNode" /> object from a configuration file.</summary>
		/// <param name="encryptedNode">The <see cref="T:System.Xml.XmlNode" /> object to decrypt.</param>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> object containing decrypted data.</returns>
		public abstract XmlNode Decrypt(XmlNode encryptedNode);

		/// <summary>Encrypts the passed <see cref="T:System.Xml.XmlNode" /> object from a configuration file.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object to encrypt.</param>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> object containing encrypted data.</returns>
		public abstract XmlNode Encrypt(XmlNode node);
	}
	/// <summary>Provides a collection of <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> objects.</summary>
	public class ProtectedConfigurationProviderCollection : ProviderCollection
	{
		/// <summary>Gets a <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object in the collection with the specified name.</summary>
		/// <param name="name">The name of a <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object in the collection.</param>
		/// <returns>The <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object with the specified name, or <see langword="null" /> if there is no object with that name.</returns>
		[System.MonoTODO]
		public new ProtectedConfigurationProvider this[string name] => (ProtectedConfigurationProvider)base[name];

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProtectedConfigurationProviderCollection" /> class using default settings.</summary>
		public ProtectedConfigurationProviderCollection()
		{
		}

		/// <summary>Adds a <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object to the collection.</summary>
		/// <param name="provider">A <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="provider" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="provider" /> is not a <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object to add already exists in the collection.  
		/// -or-
		///  The collection is read-only.</exception>
		[System.MonoTODO]
		public override void Add(ProviderBase provider)
		{
			base.Add(provider);
		}
	}
	/// <summary>Provides programmatic access to the <see langword="configProtectedData" /> configuration section. This class cannot be inherited.</summary>
	public sealed class ProtectedConfigurationSection : ConfigurationSection
	{
		private static ConfigurationProperty defaultProviderProp;

		private static ConfigurationProperty providersProp;

		private static ConfigurationPropertyCollection properties;

		private ProtectedConfigurationProviderCollection providers;

		/// <summary>Gets or sets the name of the default <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object in the <see cref="P:System.Configuration.ProtectedConfigurationSection.Providers" /> collection property.</summary>
		/// <returns>The name of the default <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object in the <see cref="P:System.Configuration.ProtectedConfigurationSection.Providers" /> collection property.</returns>
		[ConfigurationProperty("defaultProvider", DefaultValue = "RsaProtectedConfigurationProvider")]
		public string DefaultProvider
		{
			get
			{
				return (string)base[defaultProviderProp];
			}
			set
			{
				base[defaultProviderProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection of all the <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> objects in all participating configuration files.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection of all the <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> objects in all participating configuration files.</returns>
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers => (ProviderSettingsCollection)base[providersProp];

		protected internal override ConfigurationPropertyCollection Properties => properties;

		static ProtectedConfigurationSection()
		{
			defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "RsaProtectedConfigurationProvider");
			providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null);
			properties = new ConfigurationPropertyCollection();
			properties.Add(defaultProviderProp);
			properties.Add(providersProp);
		}

		internal string EncryptSection(string clearXml, ProtectedConfigurationProvider protectionProvider)
		{
			XmlDocument xmlDocument = new ConfigurationXmlDocument();
			xmlDocument.LoadXml(clearXml);
			return protectionProvider.Encrypt(xmlDocument.DocumentElement).OuterXml;
		}

		internal string DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider)
		{
			XmlDocument xmlDocument = new ConfigurationXmlDocument();
			xmlDocument.InnerXml = encryptedXml;
			return protectionProvider.Decrypt(xmlDocument.DocumentElement).OuterXml;
		}

		internal ProtectedConfigurationProviderCollection GetAllProviders()
		{
			if (providers == null)
			{
				providers = new ProtectedConfigurationProviderCollection();
				foreach (ProviderSettings provider in Providers)
				{
					providers.Add(InstantiateProvider(provider));
				}
			}
			return providers;
		}

		private ProtectedConfigurationProvider InstantiateProvider(ProviderSettings ps)
		{
			ProtectedConfigurationProvider obj = (Activator.CreateInstance(Type.GetType(ps.Type, throwOnError: true)) as ProtectedConfigurationProvider) ?? throw new Exception("The type specified does not extend ProtectedConfigurationProvider class.");
			obj.Initialize(ps.Name, ps.Parameters);
			return obj;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProtectedConfigurationSection" /> class using default settings.</summary>
		public ProtectedConfigurationSection()
		{
		}
	}
	/// <summary>Represents a group of configuration elements that configure the providers for the <see langword="&lt;configProtectedData&gt;" /> configuration section.</summary>
	public class ProtectedProviderSettings : ConfigurationElement
	{
		private static ConfigurationProperty providersProp;

		private static ConfigurationPropertyCollection properties;

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> collection that represents the properties of the providers for the protected configuration data.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> that represents the properties of the providers for the protected configuration data.</returns>
		protected internal override ConfigurationPropertyCollection Properties => properties;

		/// <summary>Gets a collection of <see cref="T:System.Configuration.ProviderSettings" /> objects that represent the properties of the providers for the protected configuration data.</summary>
		/// <returns>A collection of <see cref="T:System.Configuration.ProviderSettings" /> objects that represent the properties of the providers for the protected configuration data.</returns>
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ProviderSettingsCollection Providers => (ProviderSettingsCollection)base[providersProp];

		static ProtectedProviderSettings()
		{
			providersProp = new ConfigurationProperty("", typeof(ProviderSettingsCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);
			properties = new ConfigurationPropertyCollection();
			properties.Add(providersProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProtectedProviderSettings" /> class.</summary>
		public ProtectedProviderSettings()
		{
		}
	}
	/// <summary>Represents the configuration elements associated with a provider.</summary>
	public sealed class ProviderSettings : ConfigurationElement
	{
		private System.Configuration.ConfigNameValueCollection parameters;

		private static ConfigurationProperty nameProp;

		private static ConfigurationProperty typeProp;

		private static ConfigurationPropertyCollection properties;

		/// <summary>Gets or sets the name of the provider configured by this class.</summary>
		/// <returns>The name of the provider.</returns>
		[ConfigurationProperty("name", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Name
		{
			get
			{
				return (string)base[nameProp];
			}
			set
			{
				base[nameProp] = value;
			}
		}

		/// <summary>Gets or sets the type of the provider configured by this class.</summary>
		/// <returns>The fully qualified namespace and class name for the type of provider configured by this <see cref="T:System.Configuration.ProviderSettings" /> instance.</returns>
		[ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[typeProp];
			}
			set
			{
				base[typeProp] = value;
			}
		}

		protected internal override ConfigurationPropertyCollection Properties => properties;

		/// <summary>Gets a collection of user-defined parameters for the provider.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of parameters for the provider.</returns>
		public NameValueCollection Parameters
		{
			get
			{
				if (parameters == null)
				{
					parameters = new System.Configuration.ConfigNameValueCollection();
				}
				return parameters;
			}
		}

		static ProviderSettings()
		{
			nameProp = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
			typeProp = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.IsRequired);
			properties = new ConfigurationPropertyCollection();
			properties.Add(nameProp);
			properties.Add(typeProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProviderSettings" /> class.</summary>
		public ProviderSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProviderSettings" /> class.</summary>
		/// <param name="name">The name of the provider to configure settings for.</param>
		/// <param name="type">The type of the provider to configure settings for.</param>
		public ProviderSettings(string name, string type)
		{
			Name = name;
			Type = type;
		}

		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			if (parameters == null)
			{
				parameters = new System.Configuration.ConfigNameValueCollection();
			}
			parameters[name] = value;
			parameters.ResetModified();
			return true;
		}

		protected internal override bool IsModified()
		{
			if (parameters == null || !parameters.IsModified)
			{
				return base.IsModified();
			}
			return true;
		}

		protected internal override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
			if (parentElement is ProviderSettings { parameters: not null } providerSettings)
			{
				parameters = new System.Configuration.ConfigNameValueCollection(providerSettings.parameters);
			}
			else
			{
				parameters = null;
			}
		}

		[System.MonoTODO]
		protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
		}
	}
	/// <summary>Represents a collection of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
	[ConfigurationCollection(typeof(ProviderSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ProviderSettingsCollection : ConfigurationElementCollection
	{
		private static ConfigurationPropertyCollection props = new ConfigurationPropertyCollection();

		/// <summary>Gets or sets a value at the specified index in the <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ProviderSettings" /> to return.</param>
		/// <returns>The specified <see cref="T:System.Configuration.ProviderSettings" />.</returns>
		public ProviderSettings this[int index]
		{
			get
			{
				return (ProviderSettings)BaseGet(index);
			}
			set
			{
				BaseAdd(index, value);
			}
		}

		/// <summary>Gets an item from the collection.</summary>
		/// <param name="key">A string reference to the <see cref="T:System.Configuration.ProviderSettings" /> object within the collection.</param>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettings" /> object contained in the collection.</returns>
		public new ProviderSettings this[string key] => (ProviderSettings)BaseGet(key);

		protected internal override ConfigurationPropertyCollection Properties => props;

		/// <summary>Adds a <see cref="T:System.Configuration.ProviderSettings" /> object to the collection.</summary>
		/// <param name="provider">The <see cref="T:System.Configuration.ProviderSettings" /> object to add.</param>
		public void Add(ProviderSettings provider)
		{
			BaseAdd(provider);
		}

		/// <summary>Clears the collection.</summary>
		public void Clear()
		{
			BaseClear();
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new ProviderSettings();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProviderSettings)element).Name;
		}

		/// <summary>Removes an element from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ProviderSettings" /> object to remove.</param>
		public void Remove(string name)
		{
			BaseRemove(name);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProviderSettingsCollection" /> class.</summary>
		public ProviderSettingsCollection()
		{
		}
	}
	/// <summary>Provides validation of a string based on the rules provided by a regular expression.</summary>
	public class RegexStringValidator : ConfigurationValidatorBase
	{
		private string regex;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.RegexStringValidator" /> class.</summary>
		/// <param name="regex">A string that specifies a regular expression.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="regex" /> is null or an empty string ("").</exception>
		public RegexStringValidator(string regex)
		{
			this.regex = regex;
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <param name="type">The type of object.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="type" /> parameter matches a string; otherwise, <see langword="false" />.</returns>
		public override bool CanValidate(Type type)
		{
			return type == typeof(string);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> does not conform to the parameters of the <see cref="T:System.Text.RegularExpressions.Regex" /> class.</exception>
		public override void Validate(object value)
		{
			if (!Regex.IsMatch((string)value, regex))
			{
				throw new ArgumentException("The string must match the regexp `{0}'", regex);
			}
		}
	}
	/// <summary>Declaratively instructs the .NET Framework to perform string validation on a configuration property using a regular expression. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class RegexStringValidatorAttribute : ConfigurationValidatorAttribute
	{
		private string regex;

		private ConfigurationValidatorBase instance;

		/// <summary>Gets the string used to perform regular-expression validation.</summary>
		/// <returns>The string containing the regular expression used to filter the string assigned to the decorated configuration-element property.</returns>
		public string Regex => regex;

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.RegexStringValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (instance == null)
				{
					instance = new RegexStringValidator(regex);
				}
				return instance;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.RegexStringValidatorAttribute" /> object.</summary>
		/// <param name="regex">The string to use for regular expression validation.</param>
		public RegexStringValidatorAttribute(string regex)
		{
			this.regex = regex;
		}
	}
	/// <summary>Provides a <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> instance that uses RSA encryption to encrypt and decrypt configuration data.</summary>
	public sealed class RsaProtectedConfigurationProvider : ProtectedConfigurationProvider
	{
		private string cspProviderName;

		private string keyContainerName;

		private bool useMachineContainer;

		private bool useOAEP;

		private RSACryptoServiceProvider rsa;

		/// <summary>Gets the name of the Windows cryptography API (crypto API) cryptographic service provider (CSP).</summary>
		/// <returns>The name of the CryptoAPI cryptographic service provider.</returns>
		public string CspProviderName => cspProviderName;

		/// <summary>Gets the name of the key container.</summary>
		/// <returns>The name of the key container.</returns>
		public string KeyContainerName => keyContainerName;

		/// <summary>Gets the public key used by the provider.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.RSAParameters" /> object that contains the public key used by the provider.</returns>
		public RSAParameters RsaPublicKey => GetProvider().ExportParameters(includePrivateParameters: false);

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Configuration.RsaProtectedConfigurationProvider" /> object is using the machine key container.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.RsaProtectedConfigurationProvider" /> object is using the machine key container; otherwise, <see langword="false" />.</returns>
		public bool UseMachineContainer => useMachineContainer;

		/// <summary>Gets a value that indicates whether the provider is using Optimal Asymmetric Encryption Padding (OAEP) key exchange data.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.RsaProtectedConfigurationProvider" /> object is using Optimal Asymmetric Encryption Padding (OAEP) key exchange data; otherwise, <see langword="false" />.</returns>
		public bool UseOAEP => useOAEP;

		/// <summary>Gets a value indicating whether the provider uses FIPS.</summary>
		/// <returns>
		///   <see langword="true" /> if the provider uses FIPS; otherwise, <see langword="false" />.</returns>
		public bool UseFIPS
		{
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		private RSACryptoServiceProvider GetProvider()
		{
			if (rsa == null)
			{
				CspParameters cspParameters = new CspParameters();
				cspParameters.ProviderName = cspProviderName;
				cspParameters.KeyContainerName = keyContainerName;
				if (useMachineContainer)
				{
					cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
				}
				rsa = new RSACryptoServiceProvider(cspParameters);
			}
			return rsa;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.RsaProtectedConfigurationProvider" /> class.</summary>
		public RsaProtectedConfigurationProvider()
		{
		}

		/// <summary>Decrypts the XML node passed to it.</summary>
		/// <param name="encryptedNode">The <see cref="T:System.Xml.XmlNode" /> to decrypt.</param>
		/// <returns>The decrypted XML node.</returns>
		[System.MonoTODO]
		public override XmlNode Decrypt(XmlNode encryptedNode)
		{
			ConfigurationXmlDocument configurationXmlDocument = new ConfigurationXmlDocument();
			configurationXmlDocument.Load(new StringReader(encryptedNode.OuterXml));
			EncryptedXml encryptedXml = new EncryptedXml(configurationXmlDocument);
			encryptedXml.AddKeyNameMapping("Rsa Key", GetProvider());
			encryptedXml.DecryptDocument();
			return configurationXmlDocument.DocumentElement;
		}

		/// <summary>Encrypts the XML node passed to it.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> to encrypt.</param>
		/// <returns>An encrypted <see cref="T:System.Xml.XmlNode" /> object.</returns>
		[System.MonoTODO]
		public override XmlNode Encrypt(XmlNode node)
		{
			XmlDocument xmlDocument = new ConfigurationXmlDocument();
			xmlDocument.Load(new StringReader(node.OuterXml));
			EncryptedXml encryptedXml = new EncryptedXml(xmlDocument);
			encryptedXml.AddKeyNameMapping("Rsa Key", GetProvider());
			return encryptedXml.Encrypt(xmlDocument.DocumentElement, "Rsa Key").GetXml();
		}

		/// <summary>Initializes the provider with default settings.</summary>
		/// <param name="name">The provider name to use for the object.</param>
		/// <param name="configurationValues">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> collection of values to use when initializing the object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="configurationValues" /> includes one or more unrecognized values.</exception>
		[System.MonoTODO]
		public override void Initialize(string name, NameValueCollection configurationValues)
		{
			base.Initialize(name, configurationValues);
			keyContainerName = configurationValues["keyContainerName"];
			cspProviderName = configurationValues["cspProviderName"];
			string text = configurationValues["useMachineContainer"];
			if (text != null && text.ToLower() == "true")
			{
				useMachineContainer = true;
			}
			text = configurationValues["useOAEP"];
			if (text != null && text.ToLower() == "true")
			{
				useOAEP = true;
			}
		}

		/// <summary>Adds a key to the RSA key container.</summary>
		/// <param name="keySize">The size of the key to add.</param>
		/// <param name="exportable">
		///   <see langword="true" /> to indicate that the key is exportable; otherwise, <see langword="false" />.</param>
		[System.MonoTODO]
		public void AddKey(int keySize, bool exportable)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes a key from the RSA key container.</summary>
		[System.MonoTODO]
		public void DeleteKey()
		{
			throw new NotImplementedException();
		}

		/// <summary>Exports an RSA key from the key container.</summary>
		/// <param name="xmlFileName">The file name and path to export the key to.</param>
		/// <param name="includePrivateParameters">
		///   <see langword="true" /> to indicate that private parameters are exported; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		[System.MonoTODO]
		public void ExportKey(string xmlFileName, bool includePrivateParameters)
		{
			string value = GetProvider().ToXmlString(includePrivateParameters);
			StreamWriter streamWriter = new StreamWriter(new FileStream(xmlFileName, FileMode.OpenOrCreate, FileAccess.Write));
			streamWriter.Write(value);
			streamWriter.Close();
		}

		/// <summary>Imports an RSA key into the key container.</summary>
		/// <param name="xmlFileName">The file name and path to import the key from.</param>
		/// <param name="exportable">
		///   <see langword="true" /> to indicate that the key is exportable; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is write-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		[System.MonoTODO]
		public void ImportKey(string xmlFileName, bool exportable)
		{
			throw new NotImplementedException();
		}
	}
	internal class SectionGroupInfo : ConfigInfo
	{
		private bool modified;

		private ConfigInfoCollection sections;

		private ConfigInfoCollection groups;

		private static ConfigInfoCollection emptyList = new ConfigInfoCollection();

		public ConfigInfoCollection Sections
		{
			get
			{
				if (sections == null)
				{
					return emptyList;
				}
				return sections;
			}
		}

		public ConfigInfoCollection Groups
		{
			get
			{
				if (groups == null)
				{
					return emptyList;
				}
				return groups;
			}
		}

		public SectionGroupInfo()
		{
			Type = typeof(ConfigurationSectionGroup);
		}

		public SectionGroupInfo(string groupName, string typeName)
		{
			Name = groupName;
			TypeName = typeName;
		}

		public void AddChild(ConfigInfo data)
		{
			modified = true;
			data.Parent = this;
			if (data is SectionInfo)
			{
				if (sections == null)
				{
					sections = new ConfigInfoCollection();
				}
				sections[data.Name] = data;
			}
			else
			{
				if (groups == null)
				{
					groups = new ConfigInfoCollection();
				}
				groups[data.Name] = data;
			}
		}

		public void Clear()
		{
			modified = true;
			if (sections != null)
			{
				sections.Clear();
			}
			if (groups != null)
			{
				groups.Clear();
			}
		}

		public bool HasChild(string name)
		{
			if (sections != null && sections[name] != null)
			{
				return true;
			}
			if (groups != null)
			{
				return groups[name] != null;
			}
			return false;
		}

		public void RemoveChild(string name)
		{
			modified = true;
			if (sections != null)
			{
				sections.Remove(name);
			}
			if (groups != null)
			{
				groups.Remove(name);
			}
		}

		public SectionInfo GetChildSection(string name)
		{
			if (sections != null)
			{
				return sections[name] as SectionInfo;
			}
			return null;
		}

		public SectionGroupInfo GetChildGroup(string name)
		{
			if (groups != null)
			{
				return groups[name] as SectionGroupInfo;
			}
			return null;
		}

		public override bool HasDataContent(Configuration config)
		{
			object[] array = new object[2] { Sections, Groups };
			for (int i = 0; i < array.Length; i++)
			{
				ConfigInfoCollection configInfoCollection = (ConfigInfoCollection)array[i];
				foreach (string item in configInfoCollection)
				{
					if (configInfoCollection[item].HasDataContent(config))
					{
						return true;
					}
				}
			}
			return false;
		}

		public override bool HasConfigContent(Configuration cfg)
		{
			if (base.StreamName == cfg.FileName)
			{
				return true;
			}
			object[] array = new object[2] { Sections, Groups };
			for (int i = 0; i < array.Length; i++)
			{
				ConfigInfoCollection configInfoCollection = (ConfigInfoCollection)array[i];
				foreach (string item in configInfoCollection)
				{
					if (configInfoCollection[item].HasConfigContent(cfg))
					{
						return true;
					}
				}
			}
			return false;
		}

		public override void ReadConfig(Configuration cfg, string streamName, XmlReader reader)
		{
			base.StreamName = streamName;
			ConfigHost = cfg.ConfigHost;
			if (reader.LocalName != "configSections")
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.Name == "name")
					{
						Name = reader.Value;
					}
					else if (reader.Name == "type")
					{
						TypeName = reader.Value;
						Type = null;
					}
					else
					{
						ThrowException("Unrecognized attribute", reader);
					}
				}
				if (Name == null)
				{
					ThrowException("sectionGroup must have a 'name' attribute", reader);
				}
				if (Name == "location")
				{
					ThrowException("location is a reserved section name", reader);
				}
			}
			if (TypeName == null)
			{
				TypeName = "System.Configuration.ConfigurationSectionGroup";
			}
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return;
			}
			reader.ReadStartElement();
			reader.MoveToContent();
			while (reader.NodeType != XmlNodeType.EndElement)
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					reader.Skip();
					continue;
				}
				string localName = reader.LocalName;
				ConfigInfo configInfo = null;
				switch (localName)
				{
				case "remove":
					ReadRemoveSection(reader);
					continue;
				case "clear":
					if (reader.HasAttributes)
					{
						ThrowException("Unrecognized attribute.", reader);
					}
					Clear();
					reader.Skip();
					continue;
				case "section":
					configInfo = new SectionInfo();
					break;
				case "sectionGroup":
					configInfo = new SectionGroupInfo();
					break;
				default:
					ThrowException("Unrecognized element: " + reader.Name, reader);
					break;
				}
				configInfo.ReadConfig(cfg, streamName, reader);
				ConfigInfo configInfo2 = Groups[configInfo.Name];
				if (configInfo2 == null)
				{
					configInfo2 = Sections[configInfo.Name];
				}
				if (configInfo2 != null)
				{
					if (configInfo2.GetType() != configInfo.GetType())
					{
						ThrowException("A section or section group named '" + configInfo.Name + "' already exists", reader);
					}
					configInfo2.Merge(configInfo);
					configInfo2.StreamName = streamName;
				}
				else
				{
					AddChild(configInfo);
				}
			}
			reader.ReadEndElement();
		}

		public override void WriteConfig(Configuration cfg, XmlWriter writer, ConfigurationSaveMode mode)
		{
			if (Name != null)
			{
				writer.WriteStartElement("sectionGroup");
				writer.WriteAttributeString("name", Name);
				if (TypeName != null && TypeName != "" && TypeName != "System.Configuration.ConfigurationSectionGroup")
				{
					writer.WriteAttributeString("type", TypeName);
				}
			}
			else
			{
				writer.WriteStartElement("configSections");
			}
			object[] array = new object[2] { Sections, Groups };
			for (int i = 0; i < array.Length; i++)
			{
				ConfigInfoCollection configInfoCollection = (ConfigInfoCollection)array[i];
				foreach (string item in configInfoCollection)
				{
					ConfigInfo configInfo = configInfoCollection[item];
					if (configInfo.HasConfigContent(cfg))
					{
						configInfo.WriteConfig(cfg, writer, mode);
					}
				}
			}
			writer.WriteEndElement();
		}

		private void ReadRemoveSection(XmlReader reader)
		{
			if (!reader.MoveToNextAttribute() || reader.Name != "name")
			{
				ThrowException("Unrecognized attribute.", reader);
			}
			string value = reader.Value;
			if (string.IsNullOrEmpty(value))
			{
				ThrowException("Empty name to remove", reader);
			}
			reader.MoveToElement();
			if (!HasChild(value))
			{
				ThrowException("No factory for " + value, reader);
			}
			RemoveChild(value);
			reader.Skip();
		}

		public void ReadRootData(XmlReader reader, Configuration config, bool overrideAllowed)
		{
			reader.MoveToContent();
			ReadContent(reader, config, overrideAllowed, root: true);
		}

		public override void ReadData(Configuration config, XmlReader reader, bool overrideAllowed)
		{
			reader.MoveToContent();
			if (!reader.IsEmptyElement)
			{
				reader.ReadStartElement();
				ReadContent(reader, config, overrideAllowed, root: false);
				reader.MoveToContent();
				reader.ReadEndElement();
			}
			else
			{
				reader.Read();
			}
		}

		private void ReadContent(XmlReader reader, Configuration config, bool overrideAllowed, bool root)
		{
			while (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.None)
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					reader.Skip();
				}
				else if (reader.LocalName == "dllmap")
				{
					reader.Skip();
				}
				else if (reader.LocalName == "location")
				{
					if (!root)
					{
						ThrowException("<location> elements are only allowed in <configuration> elements.", reader);
					}
					string attribute = reader.GetAttribute("allowOverride");
					bool flag = attribute == null || attribute.Length == 0 || bool.Parse(attribute);
					string attribute2 = reader.GetAttribute("path");
					if (attribute2 != null && attribute2.Length > 0)
					{
						string xmlContent = reader.ReadOuterXml();
						string[] array = attribute2.Split(',');
						for (int i = 0; i < array.Length; i++)
						{
							string text = array[i].Trim();
							if (config.Locations.Find(text) != null)
							{
								ThrowException("Sections must only appear once per config file.", reader);
							}
							ConfigurationLocation loc = new ConfigurationLocation(text, xmlContent, config, flag);
							config.Locations.Add(loc);
						}
					}
					else
					{
						ReadData(config, reader, flag);
					}
				}
				else
				{
					ConfigInfo configInfo = GetConfigInfo(reader, this);
					if (configInfo != null)
					{
						configInfo.ReadData(config, reader, overrideAllowed);
					}
					else
					{
						ThrowException("Unrecognized configuration section <" + reader.LocalName + ">", reader);
					}
				}
			}
		}

		private ConfigInfo GetConfigInfo(XmlReader reader, SectionGroupInfo current)
		{
			ConfigInfo configInfo = null;
			if (current.sections != null)
			{
				configInfo = current.sections[reader.LocalName];
			}
			if (configInfo != null)
			{
				return configInfo;
			}
			if (current.groups != null)
			{
				configInfo = current.groups[reader.LocalName];
			}
			if (configInfo != null)
			{
				return configInfo;
			}
			if (current.groups == null)
			{
				return null;
			}
			foreach (string allKey in current.groups.AllKeys)
			{
				configInfo = GetConfigInfo(reader, (SectionGroupInfo)current.groups[allKey]);
				if (configInfo != null)
				{
					return configInfo;
				}
			}
			return null;
		}

		internal override void Merge(ConfigInfo newData)
		{
			if (!(newData is SectionGroupInfo sectionGroupInfo))
			{
				return;
			}
			if (sectionGroupInfo.sections != null && sectionGroupInfo.sections.Count > 0)
			{
				foreach (string allKey in sectionGroupInfo.sections.AllKeys)
				{
					if (sections[allKey] == null)
					{
						sections.Add(allKey, sectionGroupInfo.sections[allKey]);
					}
				}
			}
			if (sectionGroupInfo.groups == null || sectionGroupInfo.sections == null || sectionGroupInfo.sections.Count <= 0)
			{
				return;
			}
			foreach (string allKey2 in sectionGroupInfo.groups.AllKeys)
			{
				if (groups[allKey2] == null)
				{
					groups.Add(allKey2, sectionGroupInfo.groups[allKey2]);
				}
			}
		}

		public void WriteRootData(XmlWriter writer, Configuration config, ConfigurationSaveMode mode)
		{
			WriteContent(writer, config, mode, writeElem: false);
		}

		public override void WriteData(Configuration config, XmlWriter writer, ConfigurationSaveMode mode)
		{
			writer.WriteStartElement(Name);
			WriteContent(writer, config, mode, writeElem: true);
			writer.WriteEndElement();
		}

		public void WriteContent(XmlWriter writer, Configuration config, ConfigurationSaveMode mode, bool writeElem)
		{
			object[] array = new object[2] { Sections, Groups };
			for (int i = 0; i < array.Length; i++)
			{
				ConfigInfoCollection configInfoCollection = (ConfigInfoCollection)array[i];
				foreach (string item in configInfoCollection)
				{
					ConfigInfo configInfo = configInfoCollection[item];
					if (configInfo.HasDataContent(config))
					{
						configInfo.WriteData(config, writer, mode);
					}
				}
			}
		}

		internal override bool HasValues(Configuration config, ConfigurationSaveMode mode)
		{
			if (modified && mode == ConfigurationSaveMode.Modified)
			{
				return true;
			}
			object[] array = new object[2] { Sections, Groups };
			for (int i = 0; i < array.Length; i++)
			{
				ConfigInfoCollection configInfoCollection = (ConfigInfoCollection)array[i];
				foreach (string item in configInfoCollection)
				{
					if (configInfoCollection[item].HasValues(config, mode))
					{
						return true;
					}
				}
			}
			return false;
		}

		internal override void ResetModified(Configuration config)
		{
			modified = false;
			object[] array = new object[2] { Sections, Groups };
			for (int i = 0; i < array.Length; i++)
			{
				ConfigInfoCollection configInfoCollection = (ConfigInfoCollection)array[i];
				foreach (string item in configInfoCollection)
				{
					configInfoCollection[item].ResetModified(config);
				}
			}
		}
	}
	internal class ConfigInfoCollection : NameObjectCollectionBase
	{
		public ICollection AllKeys => Keys;

		public ConfigInfo this[string name]
		{
			get
			{
				return (ConfigInfo)BaseGet(name);
			}
			set
			{
				BaseSet(name, value);
			}
		}

		public ConfigInfo this[int index]
		{
			get
			{
				return (ConfigInfo)BaseGet(index);
			}
			set
			{
				BaseSet(index, value);
			}
		}

		public ConfigInfoCollection()
			: base(StringComparer.Ordinal)
		{
		}

		public void Add(string name, ConfigInfo config)
		{
			BaseAdd(name, config);
		}

		public void Clear()
		{
			BaseClear();
		}

		public string GetKey(int index)
		{
			return BaseGetKey(index);
		}

		public void Remove(string name)
		{
			BaseRemove(name);
		}

		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}
	}
	internal class SectionInfo : ConfigInfo
	{
		private bool allowLocation = true;

		private bool requirePermission = true;

		private bool restartOnExternalChanges;

		private ConfigurationAllowDefinition allowDefinition = ConfigurationAllowDefinition.Everywhere;

		private ConfigurationAllowExeDefinition allowExeDefinition = ConfigurationAllowExeDefinition.MachineToApplication;

		public SectionInfo()
		{
		}

		public SectionInfo(string sectionName, SectionInformation info)
		{
			Name = sectionName;
			TypeName = info.Type;
			allowLocation = info.AllowLocation;
			allowDefinition = info.AllowDefinition;
			allowExeDefinition = info.AllowExeDefinition;
			requirePermission = info.RequirePermission;
			restartOnExternalChanges = info.RestartOnExternalChanges;
		}

		public override object CreateInstance()
		{
			object obj = base.CreateInstance();
			if (obj is ConfigurationSection configurationSection)
			{
				configurationSection.SectionInformation.AllowLocation = allowLocation;
				configurationSection.SectionInformation.AllowDefinition = allowDefinition;
				configurationSection.SectionInformation.AllowExeDefinition = allowExeDefinition;
				configurationSection.SectionInformation.RequirePermission = requirePermission;
				configurationSection.SectionInformation.RestartOnExternalChanges = restartOnExternalChanges;
				configurationSection.SectionInformation.SetName(Name);
			}
			return obj;
		}

		public override bool HasDataContent(Configuration config)
		{
			if (config.GetSectionInstance(this, createDefaultInstance: false) == null)
			{
				return config.GetSectionXml(this) != null;
			}
			return true;
		}

		public override bool HasConfigContent(Configuration cfg)
		{
			return base.StreamName == cfg.FileName;
		}

		public override void ReadConfig(Configuration cfg, string streamName, XmlReader reader)
		{
			base.StreamName = streamName;
			ConfigHost = cfg.ConfigHost;
			while (reader.MoveToNextAttribute())
			{
				switch (reader.Name)
				{
				case "allowLocation":
				{
					string value2 = reader.Value;
					allowLocation = value2 == "true";
					if (!allowLocation && value2 != "false")
					{
						ThrowException("Invalid attribute value", reader);
					}
					break;
				}
				case "allowDefinition":
				{
					string value5 = reader.Value;
					try
					{
						allowDefinition = (ConfigurationAllowDefinition)Enum.Parse(typeof(ConfigurationAllowDefinition), value5);
					}
					catch
					{
						ThrowException("Invalid attribute value", reader);
					}
					break;
				}
				case "allowExeDefinition":
				{
					string value4 = reader.Value;
					try
					{
						allowExeDefinition = (ConfigurationAllowExeDefinition)Enum.Parse(typeof(ConfigurationAllowExeDefinition), value4);
					}
					catch
					{
						ThrowException("Invalid attribute value", reader);
					}
					break;
				}
				case "type":
					TypeName = reader.Value;
					break;
				case "name":
					Name = reader.Value;
					if (Name == "location")
					{
						ThrowException("location is a reserved section name", reader);
					}
					break;
				case "requirePermission":
				{
					string value3 = reader.Value;
					bool flag2 = value3 == "true";
					if (!flag2 && value3 != "false")
					{
						ThrowException("Invalid attribute value", reader);
					}
					requirePermission = flag2;
					break;
				}
				case "restartOnExternalChanges":
				{
					string value = reader.Value;
					bool flag = value == "true";
					if (!flag && value != "false")
					{
						ThrowException("Invalid attribute value", reader);
					}
					restartOnExternalChanges = flag;
					break;
				}
				default:
					ThrowException($"Unrecognized attribute: {reader.Name}", reader);
					break;
				}
			}
			if (Name == null || TypeName == null)
			{
				ThrowException("Required attribute missing", reader);
			}
			reader.MoveToElement();
			reader.Skip();
		}

		public override void WriteConfig(Configuration cfg, XmlWriter writer, ConfigurationSaveMode mode)
		{
			writer.WriteStartElement("section");
			writer.WriteAttributeString("name", Name);
			writer.WriteAttributeString("type", TypeName);
			if (!allowLocation)
			{
				writer.WriteAttributeString("allowLocation", "false");
			}
			if (allowDefinition != ConfigurationAllowDefinition.Everywhere)
			{
				writer.WriteAttributeString("allowDefinition", allowDefinition.ToString());
			}
			if (allowExeDefinition != ConfigurationAllowExeDefinition.MachineToApplication)
			{
				writer.WriteAttributeString("allowExeDefinition", allowExeDefinition.ToString());
			}
			if (!requirePermission)
			{
				writer.WriteAttributeString("requirePermission", "false");
			}
			writer.WriteEndElement();
		}

		public override void ReadData(Configuration config, XmlReader reader, bool overrideAllowed)
		{
			if (!config.HasFile && !allowLocation)
			{
				throw new ConfigurationErrorsException("The configuration section <" + Name + "> cannot be defined inside a <location> element.", reader);
			}
			if (!config.ConfigHost.IsDefinitionAllowed(config.ConfigPath, allowDefinition, allowExeDefinition))
			{
				object obj = ((allowExeDefinition != ConfigurationAllowExeDefinition.MachineToApplication) ? ((object)allowExeDefinition) : ((object)allowDefinition));
				throw new ConfigurationErrorsException("The section <" + Name + "> can't be defined in this configuration file (the allowed definition context is '" + obj?.ToString() + "').", reader);
			}
			if (config.GetSectionXml(this) != null)
			{
				ThrowException("The section <" + Name + "> is defined more than once in the same configuration file.", reader);
			}
			config.SetSectionXml(this, reader.ReadOuterXml());
		}

		public override void WriteData(Configuration config, XmlWriter writer, ConfigurationSaveMode mode)
		{
			ConfigurationSection sectionInstance = config.GetSectionInstance(this, createDefaultInstance: false);
			string text;
			if (sectionInstance != null)
			{
				ConfigurationSection parentElement = ((config.Parent != null) ? config.Parent.GetSectionInstance(this, createDefaultInstance: false) : null);
				text = sectionInstance.SerializeSection(parentElement, Name, mode);
				string externalDataXml = sectionInstance.ExternalDataXml;
				string filePath = config.FilePath;
				if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(externalDataXml))
				{
					using StreamWriter streamWriter = new StreamWriter(Path.Combine(Path.GetDirectoryName(filePath), sectionInstance.SectionInformation.ConfigSource));
					streamWriter.Write(externalDataXml);
				}
				if (sectionInstance.SectionInformation.IsProtected)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat("<{0} configProtectionProvider=\"{1}\">\n", Name, sectionInstance.SectionInformation.ProtectionProvider.Name);
					stringBuilder.Append(config.ConfigHost.EncryptSection(text, sectionInstance.SectionInformation.ProtectionProvider, ProtectedConfiguration.Section));
					stringBuilder.AppendFormat("</{0}>", Name);
					text = stringBuilder.ToString();
				}
			}
			else
			{
				text = config.GetSectionXml(this);
			}
			if (!string.IsNullOrEmpty(text))
			{
				writer.WriteRaw(text);
			}
		}

		internal override void Merge(ConfigInfo data)
		{
		}

		internal override bool HasValues(Configuration config, ConfigurationSaveMode mode)
		{
			ConfigurationSection sectionInstance = config.GetSectionInstance(this, createDefaultInstance: false);
			if (sectionInstance == null)
			{
				return false;
			}
			ConfigurationSection parent = ((config.Parent != null) ? config.Parent.GetSectionInstance(this, createDefaultInstance: false) : null);
			return sectionInstance.HasValues(parent, mode);
		}

		internal override void ResetModified(Configuration config)
		{
			config.GetSectionInstance(this, createDefaultInstance: false)?.ResetModified();
		}
	}
	/// <summary>Contains metadata about an individual section within the configuration hierarchy. This class cannot be inherited.</summary>
	public sealed class SectionInformation
	{
		private ConfigurationSection parent;

		private ConfigurationAllowDefinition allow_definition = ConfigurationAllowDefinition.Everywhere;

		private ConfigurationAllowExeDefinition allow_exe_definition = ConfigurationAllowExeDefinition.MachineToApplication;

		private bool allow_location;

		private bool allow_override;

		private bool inherit_on_child_apps;

		private bool restart_on_external_changes;

		private bool require_permission;

		private string config_source = string.Empty;

		private bool force_update;

		private string name;

		private string type_name;

		private string raw_xml;

		private ProtectedConfigurationProvider protection_provider;

		internal string ConfigFilePath { get; set; }

		/// <summary>Gets or sets a value that indicates where in the configuration file hierarchy the associated configuration section can be defined.</summary>
		/// <returns>A value that indicates where in the configuration file hierarchy the associated <see cref="T:System.Configuration.ConfigurationSection" /> object can be declared.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		public ConfigurationAllowDefinition AllowDefinition
		{
			get
			{
				return allow_definition;
			}
			set
			{
				allow_definition = value;
			}
		}

		/// <summary>Gets or sets a value that indicates where in the configuration file hierarchy the associated configuration section can be declared.</summary>
		/// <returns>A value that indicates where in the configuration file hierarchy the associated <see cref="T:System.Configuration.ConfigurationSection" /> object can be declared for .exe files.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		public ConfigurationAllowExeDefinition AllowExeDefinition
		{
			get
			{
				return allow_exe_definition;
			}
			set
			{
				allow_exe_definition = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the configuration section allows the <see langword="location" /> attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="location" /> attribute is allowed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		public bool AllowLocation
		{
			get
			{
				return allow_location;
			}
			set
			{
				allow_location = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the associated configuration section can be overridden by lower-level configuration files.</summary>
		/// <returns>
		///   <see langword="true" /> if the section can be overridden; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public bool AllowOverride
		{
			get
			{
				return allow_override;
			}
			set
			{
				allow_override = value;
			}
		}

		/// <summary>Gets or sets the name of the include file in which the associated configuration section is defined, if such a file exists.</summary>
		/// <returns>The name of the include file in which the associated <see cref="T:System.Configuration.ConfigurationSection" /> is defined, if such a file exists; otherwise, an empty string ("").</returns>
		public string ConfigSource
		{
			get
			{
				return config_source;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				config_source = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the associated configuration section will be saved even if it has not been modified.</summary>
		/// <returns>
		///   <see langword="true" /> if the associated <see cref="T:System.Configuration.ConfigurationSection" /> object will be saved even if it has not been modified; otherwise, <see langword="false" />. The default is <see langword="false" />.  
		///
		///  If the configuration file is saved (even if there are no modifications), ASP.NET restarts the application.</returns>
		public bool ForceSave
		{
			get
			{
				return force_update;
			}
			set
			{
				force_update = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the settings that are specified in the associated configuration section are inherited by applications that reside in a subdirectory of the relevant application.</summary>
		/// <returns>
		///   <see langword="true" /> if the settings specified in this <see cref="T:System.Configuration.ConfigurationSection" /> object are inherited by child applications; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		public bool InheritInChildApplications
		{
			get
			{
				return inherit_on_child_apps;
			}
			set
			{
				inherit_on_child_apps = value;
			}
		}

		/// <summary>Gets a value that indicates whether the configuration section must be declared in the configuration file.</summary>
		/// <returns>
		///   <see langword="true" /> if the associated <see cref="T:System.Configuration.ConfigurationSection" /> object must be declared in the configuration file; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool IsDeclarationRequired
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the associated configuration section is declared in the configuration file.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Configuration.ConfigurationSection" /> is declared in the configuration file; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		[System.MonoTODO]
		public bool IsDeclared => false;

		/// <summary>Gets a value that indicates whether the associated configuration section is locked.</summary>
		/// <returns>
		///   <see langword="true" /> if the section is locked; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool IsLocked => false;

		/// <summary>Gets a value that indicates whether the associated configuration section is protected.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Configuration.ConfigurationSection" /> is protected; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public bool IsProtected => protection_provider != null;

		/// <summary>Gets the name of the associated configuration section.</summary>
		/// <returns>The complete name of the configuration section.</returns>
		public string Name => name;

		/// <summary>Gets the protected configuration provider for the associated configuration section.</summary>
		/// <returns>The protected configuration provider for this <see cref="T:System.Configuration.ConfigurationSection" /> object.</returns>
		public ProtectedConfigurationProvider ProtectionProvider => protection_provider;

		/// <summary>Gets a value that indicates whether the associated configuration section requires access permissions.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="requirePermission" /> attribute is set to <see langword="true" />; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		[System.MonoTODO]
		public bool RequirePermission
		{
			get
			{
				return require_permission;
			}
			set
			{
				require_permission = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether a change in an external configuration include file requires an application restart.</summary>
		/// <returns>
		///   <see langword="true" /> if a change in an external configuration include file requires an application restart; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		[System.MonoTODO]
		public bool RestartOnExternalChanges
		{
			get
			{
				return restart_on_external_changes;
			}
			set
			{
				restart_on_external_changes = value;
			}
		}

		/// <summary>Gets the name of the associated configuration section.</summary>
		/// <returns>The name of the associated <see cref="T:System.Configuration.ConfigurationSection" /> object.</returns>
		[System.MonoTODO]
		public string SectionName => name;

		/// <summary>Gets or sets the section class name.</summary>
		/// <returns>The name of the class that is associated with this <see cref="T:System.Configuration.ConfigurationSection" /> section.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value is <see langword="null" /> or an empty string ("").</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		public string Type
		{
			get
			{
				return type_name;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException("Value cannot be null or empty.");
				}
				type_name = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationBuilder" /> object for this configuration section.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationBuilder" /> object for this configuration section.</returns>
		public ConfigurationBuilder ConfigurationBuilder
		{
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Configuration.OverrideMode" /> enumeration value that specifies whether the associated configuration section can be overridden by child configuration files.</summary>
		/// <returns>One of the <see cref="T:System.Configuration.OverrideMode" /> enumeration values.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">An attempt was made to change both the <see cref="P:System.Configuration.SectionInformation.AllowOverride" /> and <see cref="P:System.Configuration.SectionInformation.OverrideMode" /> properties, which is not supported for compatibility reasons.</exception>
		public OverrideMode OverrideMode
		{
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return default(OverrideMode);
			}
			set
			{
				Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies the default override behavior of a configuration section by child configuration files.</summary>
		/// <returns>One of the <see cref="T:System.Configuration.OverrideMode" /> enumeration values.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The override behavior is specified in a parent configuration section.</exception>
		public OverrideMode OverrideModeDefault
		{
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return default(OverrideMode);
			}
			set
			{
				Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the override behavior of a configuration section that is in turn based on whether child configuration files can lock the configuration section.</summary>
		/// <returns>One of the <see cref="T:System.Configuration.OverrideMode" /> enumeration values.</returns>
		public OverrideMode OverrideModeEffective
		{
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return default(OverrideMode);
			}
		}

		[System.MonoTODO("default value for require_permission")]
		internal SectionInformation()
		{
			allow_definition = ConfigurationAllowDefinition.Everywhere;
			allow_location = true;
			allow_override = true;
			inherit_on_child_apps = true;
			restart_on_external_changes = true;
		}

		/// <summary>Gets the configuration section that contains the configuration section associated with this object.</summary>
		/// <returns>The configuration section that contains the <see cref="T:System.Configuration.ConfigurationSection" /> that is associated with this <see cref="T:System.Configuration.SectionInformation" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is invoked from a parent section.</exception>
		public ConfigurationSection GetParentSection()
		{
			return parent;
		}

		internal void SetParentSection(ConfigurationSection parent)
		{
			this.parent = parent;
		}

		/// <summary>Returns an XML node object that represents the associated configuration-section object.</summary>
		/// <returns>The XML representation for this configuration section.</returns>
		/// <exception cref="T:System.InvalidOperationException">This configuration object is locked and cannot be edited.</exception>
		public string GetRawXml()
		{
			return raw_xml;
		}

		/// <summary>Marks a configuration section for protection.</summary>
		/// <param name="protectionProvider">The name of the protection provider to use.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Configuration.SectionInformation.AllowLocation" /> property is set to <see langword="false" />.  
		/// -or-
		///  The target section is already a protected data section.</exception>
		public void ProtectSection(string protectionProvider)
		{
			protection_provider = ProtectedConfiguration.GetProvider(protectionProvider, throwOnError: true);
		}

		/// <summary>Forces the associated configuration section to appear in the configuration file, or removes an existing section from the configuration file.</summary>
		/// <param name="force">
		///   <see langword="true" /> if the associated section should be written in the configuration file; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="force" /> is <see langword="true" /> and the associated section cannot be exported to the child configuration file, or it is undeclared.</exception>
		[System.MonoTODO]
		public void ForceDeclaration(bool force)
		{
		}

		/// <summary>Forces the associated configuration section to appear in the configuration file.</summary>
		public void ForceDeclaration()
		{
			ForceDeclaration(force: true);
		}

		/// <summary>Causes the associated configuration section to inherit all its values from the parent section.</summary>
		/// <exception cref="T:System.InvalidOperationException">This method cannot be called outside editing mode.</exception>
		[System.MonoTODO]
		public void RevertToParent()
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the protected configuration encryption from the associated configuration section.</summary>
		public void UnprotectSection()
		{
			protection_provider = null;
		}

		/// <summary>Sets the object to an XML representation of the associated configuration section within the configuration file.</summary>
		/// <param name="rawXml">The XML to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rawXml" /> is <see langword="null" />.</exception>
		public void SetRawXml(string rawXml)
		{
			raw_xml = rawXml;
		}

		[System.MonoTODO]
		internal void SetName(string name)
		{
			this.name = name;
		}
	}
	/// <summary>Provides validation of a string.</summary>
	public class StringValidator : ConfigurationValidatorBase
	{
		private char[] invalidCharacters;

		private int maxLength;

		private int minLength;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.StringValidator" /> class, based on a supplied parameter.</summary>
		/// <param name="minLength">An integer that specifies the minimum length of the string value.</param>
		public StringValidator(int minLength)
		{
			this.minLength = minLength;
			maxLength = int.MaxValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.StringValidator" /> class, based on supplied parameters.</summary>
		/// <param name="minLength">An integer that specifies the minimum length of the string value.</param>
		/// <param name="maxLength">An integer that specifies the maximum length of the string value.</param>
		public StringValidator(int minLength, int maxLength)
		{
			this.minLength = minLength;
			this.maxLength = maxLength;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.StringValidator" /> class, based on supplied parameters.</summary>
		/// <param name="minLength">An integer that specifies the minimum length of the string value.</param>
		/// <param name="maxLength">An integer that specifies the maximum length of the string value.</param>
		/// <param name="invalidCharacters">A string that represents invalid characters.</param>
		public StringValidator(int minLength, int maxLength, string invalidCharacters)
		{
			this.minLength = minLength;
			this.maxLength = maxLength;
			if (invalidCharacters != null)
			{
				this.invalidCharacters = invalidCharacters.ToCharArray();
			}
		}

		/// <summary>Determines whether an object can be validated based on type.</summary>
		/// <param name="type">The object type.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="type" /> parameter matches a string; otherwise, <see langword="false" />.</returns>
		public override bool CanValidate(Type type)
		{
			return type == typeof(string);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The object value.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is less than <paramref name="minValue" /> or greater than <paramref name="maxValue" /> as defined in the constructor.  
		/// -or-
		///  <paramref name="value" /> contains invalid characters.</exception>
		public override void Validate(object value)
		{
			if (value != null || minLength > 0)
			{
				string text = (string)value;
				if (text == null || text.Length < minLength)
				{
					throw new ArgumentException("The string must be at least " + minLength + " characters long.");
				}
				if (text.Length > maxLength)
				{
					throw new ArgumentException("The string must be no more than " + maxLength + " characters long.");
				}
				if (invalidCharacters != null && text.IndexOfAny(invalidCharacters) != -1)
				{
					throw new ArgumentException($"The string cannot contain any of the following characters: '{invalidCharacters}'.");
				}
			}
		}
	}
	/// <summary>Declaratively instructs the .NET Framework to perform string validation on a configuration property. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class StringValidatorAttribute : ConfigurationValidatorAttribute
	{
		private string invalidCharacters;

		private int maxLength = int.MaxValue;

		private int minLength;

		private ConfigurationValidatorBase instance;

		/// <summary>Gets or sets the invalid characters for the property.</summary>
		/// <returns>The string that contains the set of characters that are not allowed for the property.</returns>
		public string InvalidCharacters
		{
			get
			{
				return invalidCharacters;
			}
			set
			{
				invalidCharacters = value;
				instance = null;
			}
		}

		/// <summary>Gets or sets the maximum length allowed for the string to assign to the property.</summary>
		/// <returns>An integer that indicates the maximum allowed length for the string to assign to the property.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see cref="P:System.Configuration.StringValidatorAttribute.MinLength" />.</exception>
		public int MaxLength
		{
			get
			{
				return maxLength;
			}
			set
			{
				maxLength = value;
				instance = null;
			}
		}

		/// <summary>Gets or sets the minimum allowed value for the string to assign to the property.</summary>
		/// <returns>An integer that indicates the allowed minimum length for the string to assign to the property.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is greater than <see cref="P:System.Configuration.StringValidatorAttribute.MaxLength" />.</exception>
		public int MinLength
		{
			get
			{
				return minLength;
			}
			set
			{
				minLength = value;
				instance = null;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.StringValidator" /> class.</summary>
		/// <returns>A current <see cref="T:System.Configuration.StringValidator" /> settings in a <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (instance == null)
				{
					instance = new StringValidator(minLength, maxLength, invalidCharacters);
				}
				return instance;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.StringValidatorAttribute" /> class.</summary>
		public StringValidatorAttribute()
		{
		}
	}
	/// <summary>Validates that an object is a derived class of a specified type.</summary>
	public sealed class SubclassTypeValidator : ConfigurationValidatorBase
	{
		private Type baseClass;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SubclassTypeValidator" /> class.</summary>
		/// <param name="baseClass">The base class to validate against.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="baseClass" /> is <see langword="null" />.</exception>
		public SubclassTypeValidator(Type baseClass)
		{
			this.baseClass = baseClass;
		}

		/// <summary>Determines whether an object can be validated based on type.</summary>
		/// <param name="type">The object type.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="type" /> parameter matches a type that can be validated; otherwise, <see langword="false" />.</returns>
		public override bool CanValidate(Type type)
		{
			return type == typeof(Type);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The object value.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not of a <see cref="T:System.Type" /> that can be derived from <paramref name="baseClass" /> as defined in the constructor.</exception>
		public override void Validate(object value)
		{
			Type c = (Type)value;
			if (!baseClass.IsAssignableFrom(c))
			{
				throw new ArgumentException("The value must be a subclass");
			}
		}
	}
	/// <summary>Declaratively instructs the .NET Framework to perform validation on a configuration property. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SubclassTypeValidatorAttribute : ConfigurationValidatorAttribute
	{
		private Type baseClass;

		private ConfigurationValidatorBase instance;

		/// <summary>Gets the base type of the object being validated.</summary>
		/// <returns>The base type of the object being validated.</returns>
		public Type BaseClass => baseClass;

		/// <summary>Gets the validator attribute instance.</summary>
		/// <returns>The current <see cref="T:System.Configuration.ConfigurationValidatorBase" /> instance.</returns>
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (instance == null)
				{
					instance = new SubclassTypeValidator(baseClass);
				}
				return instance;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SubclassTypeValidatorAttribute" /> class.</summary>
		/// <param name="baseClass">The base class type.</param>
		public SubclassTypeValidatorAttribute(Type baseClass)
		{
			this.baseClass = baseClass;
		}
	}
	/// <summary>Converts a time span expressed in minutes.</summary>
	public class TimeSpanMinutesConverter : ConfigurationConverterBase
	{
		/// <summary>Converts a <see cref="T:System.String" /> to a <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <returns>The <see cref="T:System.TimeSpan" /> representing the <paramref name="data" /> parameter in minutes.</returns>
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			return TimeSpan.FromMinutes(long.Parse((string)data));
		}

		/// <summary>Converts a <see cref="T:System.TimeSpan" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert to.</param>
		/// <param name="type">The type to convert to.</param>
		/// <returns>The <see cref="T:System.String" /> representing the <paramref name="value" /> parameter in minutes.</returns>
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value.GetType() != typeof(TimeSpan))
			{
				throw new ArgumentException();
			}
			return ((long)((TimeSpan)value).TotalMinutes).ToString();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanMinutesConverter" /> class.</summary>
		public TimeSpanMinutesConverter()
		{
		}
	}
	/// <summary>Converts a <see cref="T:System.TimeSpan" /> expressed in minutes or as a standard infinite time span.</summary>
	public sealed class TimeSpanMinutesOrInfiniteConverter : TimeSpanMinutesConverter
	{
		/// <summary>Converts a <see cref="T:System.String" /> to a <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <returns>The <see cref="F:System.TimeSpan.MaxValue" />, if the <paramref name="data" /> parameter is the <see cref="T:System.String" /> "infinite"; otherwise, the <see cref="T:System.TimeSpan" /> representing the <paramref name="data" /> parameter in minutes.</returns>
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			if ((string)data == "Infinite")
			{
				return TimeSpan.MaxValue;
			}
			return base.ConvertFrom(ctx, ci, data);
		}

		/// <summary>Converts a <see cref="T:System.TimeSpan" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert.</param>
		/// <param name="type">The conversion type.</param>
		/// <returns>The <see cref="T:System.String" /> "infinite", if the <paramref name="value" /> parameter is <see cref="F:System.TimeSpan.MaxValue" />; otherwise, the <see cref="T:System.String" /> representing the <paramref name="value" /> parameter in minutes.</returns>
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value.GetType() != typeof(TimeSpan))
			{
				throw new ArgumentException();
			}
			if ((TimeSpan)value == TimeSpan.MaxValue)
			{
				return "Infinite";
			}
			return base.ConvertTo(ctx, ci, value, type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanMinutesOrInfiniteConverter" /> class.</summary>
		public TimeSpanMinutesOrInfiniteConverter()
		{
		}
	}
	/// <summary>Converts a time span expressed in seconds.</summary>
	public class TimeSpanSecondsConverter : ConfigurationConverterBase
	{
		/// <summary>Converts a <see cref="T:System.String" /> to a <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <returns>The <see cref="T:System.TimeSpan" /> representing the <paramref name="data" /> parameter in seconds.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="data" /> cannot be parsed as an integer value.</exception>
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			if (!(data is string))
			{
				throw new ArgumentException("data");
			}
			if (!long.TryParse((string)data, out var result))
			{
				throw new ArgumentException("data");
			}
			return TimeSpan.FromSeconds(result);
		}

		/// <summary>Converts a <see cref="T:System.TimeSpan" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert to.</param>
		/// <param name="type">The type to convert to.</param>
		/// <returns>The <see cref="T:System.String" /> that represents the <paramref name="value" /> parameter in minutes.</returns>
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value.GetType() != typeof(TimeSpan))
			{
				throw new ArgumentException();
			}
			return ((long)((TimeSpan)value).TotalSeconds).ToString();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanSecondsConverter" /> class.</summary>
		public TimeSpanSecondsConverter()
		{
		}
	}
	/// <summary>Converts a <see cref="T:System.TimeSpan" /> expressed in seconds or as a standard infinite time span.</summary>
	public sealed class TimeSpanSecondsOrInfiniteConverter : TimeSpanSecondsConverter
	{
		/// <summary>Converts a <see cref="T:System.String" /> to a <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <returns>The <see cref="F:System.TimeSpan.MaxValue" />, if the <paramref name="data" /> parameter is the <see cref="T:System.String" /> "infinite"; otherwise, the <see cref="T:System.TimeSpan" /> representing the <paramref name="data" /> parameter in seconds.</returns>
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			if ((string)data == "Infinite")
			{
				return TimeSpan.MaxValue;
			}
			return base.ConvertFrom(ctx, ci, data);
		}

		/// <summary>Converts a <see cref="T:System.TimeSpan" /> to a. <see cref="T:System.String" />.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert.</param>
		/// <param name="type">The conversion type.</param>
		/// <returns>The <see cref="T:System.String" /> "infinite", if the <paramref name="value" /> parameter is <see cref="F:System.TimeSpan.MaxValue" />; otherwise, the <see cref="T:System.String" /> representing the <paramref name="value" /> parameter in seconds.</returns>
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value.GetType() != typeof(TimeSpan))
			{
				throw new ArgumentException();
			}
			if ((TimeSpan)value == TimeSpan.MaxValue)
			{
				return "Infinite";
			}
			return base.ConvertTo(ctx, ci, value, type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanSecondsOrInfiniteConverter" /> class.</summary>
		public TimeSpanSecondsOrInfiniteConverter()
		{
		}
	}
	/// <summary>Provides validation of a <see cref="T:System.TimeSpan" /> object.</summary>
	public class TimeSpanValidator : ConfigurationValidatorBase
	{
		private bool rangeIsExclusive;

		private TimeSpan minValue;

		private TimeSpan maxValue;

		private long resolutionInSeconds;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanValidator" /> class, based on supplied parameters.</summary>
		/// <param name="minValue">A <see cref="T:System.TimeSpan" /> object that specifies the minimum time allowed to pass validation.</param>
		/// <param name="maxValue">A <see cref="T:System.TimeSpan" /> object that specifies the maximum time allowed to pass validation.</param>
		public TimeSpanValidator(TimeSpan minValue, TimeSpan maxValue)
			: this(minValue, maxValue, rangeIsExclusive: false, 0L)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanValidator" /> class, based on supplied parameters.</summary>
		/// <param name="minValue">A <see cref="T:System.TimeSpan" /> object that specifies the minimum time allowed to pass validation.</param>
		/// <param name="maxValue">A <see cref="T:System.TimeSpan" /> object that specifies the maximum time allowed to pass validation.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		public TimeSpanValidator(TimeSpan minValue, TimeSpan maxValue, bool rangeIsExclusive)
			: this(minValue, maxValue, rangeIsExclusive, 0L)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanValidator" /> class, based on supplied parameters.</summary>
		/// <param name="minValue">A <see cref="T:System.TimeSpan" /> object that specifies the minimum time allowed to pass validation.</param>
		/// <param name="maxValue">A <see cref="T:System.TimeSpan" /> object that specifies the maximum time allowed to pass validation.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		/// <param name="resolutionInSeconds">An <see cref="T:System.Int64" /> value that specifies a number of seconds.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="resolutionInSeconds" /> is less than <see langword="0" />.  
		/// -or-
		///  <paramref name="minValue" /> is greater than <paramref name="maxValue" />.</exception>
		public TimeSpanValidator(TimeSpan minValue, TimeSpan maxValue, bool rangeIsExclusive, long resolutionInSeconds)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.rangeIsExclusive = rangeIsExclusive;
			this.resolutionInSeconds = resolutionInSeconds;
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <param name="type">The type of the object.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="type" /> parameter matches a <see cref="T:System.TimeSpan" /> value; otherwise, <see langword="false" />.</returns>
		public override bool CanValidate(Type type)
		{
			return type == typeof(TimeSpan);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		public override void Validate(object value)
		{
			TimeSpan timeSpan = (TimeSpan)value;
			if (!rangeIsExclusive)
			{
				if (timeSpan < minValue || timeSpan > maxValue)
				{
					throw new ArgumentException("The value must be in the range " + minValue.ToString() + " - " + maxValue);
				}
			}
			else if (timeSpan >= minValue && timeSpan <= maxValue)
			{
				throw new ArgumentException("The value must not be in the range " + minValue.ToString() + " - " + maxValue);
			}
			if (resolutionInSeconds != 0L && timeSpan.Ticks % (10000000 * resolutionInSeconds) != 0L)
			{
				throw new ArgumentException("The value must have a resolution of " + TimeSpan.FromTicks(10000000 * resolutionInSeconds));
			}
		}
	}
	/// <summary>Declaratively instructs the .NET Framework to perform time validation on a configuration property. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class TimeSpanValidatorAttribute : ConfigurationValidatorAttribute
	{
		private bool excludeRange;

		private string maxValueString = "10675199.02:48:05.4775807";

		private string minValueString = "-10675199.02:48:05.4775808";

		/// <summary>Gets the absolute maximum value allowed.</summary>
		public const string TimeSpanMaxValue = "10675199.02:48:05.4775807";

		/// <summary>Gets the absolute minimum value allowed.</summary>
		public const string TimeSpanMinValue = "-10675199.02:48:05.4775808";

		private ConfigurationValidatorBase instance;

		/// <summary>Gets or sets the relative maximum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The allowed maximum <see cref="T:System.TimeSpan" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value represents less than <see cref="P:System.Configuration.TimeSpanValidatorAttribute.MinValue" />.</exception>
		public string MaxValueString
		{
			get
			{
				return maxValueString;
			}
			set
			{
				maxValueString = value;
				instance = null;
			}
		}

		/// <summary>Gets or sets the relative minimum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The minimum allowed <see cref="T:System.TimeSpan" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value represents more than <see cref="P:System.Configuration.TimeSpanValidatorAttribute.MaxValue" />.</exception>
		public string MinValueString
		{
			get
			{
				return minValueString;
			}
			set
			{
				minValueString = value;
				instance = null;
			}
		}

		/// <summary>Gets the absolute maximum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The allowed maximum <see cref="T:System.TimeSpan" /> value.</returns>
		public TimeSpan MaxValue => TimeSpan.Parse(maxValueString);

		/// <summary>Gets the absolute minimum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The allowed minimum <see cref="T:System.TimeSpan" /> value.</returns>
		public TimeSpan MinValue => TimeSpan.Parse(minValueString);

		/// <summary>Gets or sets a value that indicates whether to include or exclude the integers in the range as defined by <see cref="P:System.Configuration.TimeSpanValidatorAttribute.MinValueString" /> and <see cref="P:System.Configuration.TimeSpanValidatorAttribute.MaxValueString" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the value must be excluded; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public bool ExcludeRange
		{
			get
			{
				return excludeRange;
			}
			set
			{
				excludeRange = value;
				instance = null;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.TimeSpanValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (instance == null)
				{
					instance = new TimeSpanValidator(MinValue, MaxValue, excludeRange);
				}
				return instance;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanValidatorAttribute" /> class.</summary>
		public TimeSpanValidatorAttribute()
		{
		}
	}
	/// <summary>Converts between type and string values. This class cannot be inherited.</summary>
	public sealed class TypeNameConverter : ConfigurationConverterBase
	{
		/// <summary>Converts a <see cref="T:System.String" /> object to a <see cref="T:System.Type" /> object.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <returns>The <see cref="T:System.Type" /> that represents the <paramref name="data" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Type" /> value cannot be resolved.</exception>
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			return Type.GetType((string)data);
		}

		/// <summary>Converts a <see cref="T:System.Type" /> object to a <see cref="T:System.String" /> object.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert to.</param>
		/// <param name="type">The type to convert to.</param>
		/// <returns>The <see cref="T:System.String" /> that represents the <paramref name="value" /> parameter.</returns>
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value == null)
			{
				return null;
			}
			if (!(value is Type))
			{
				throw new ArgumentException("value");
			}
			return ((Type)value).AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TypeNameConverter" /> class.</summary>
		public TypeNameConverter()
		{
		}
	}
	/// <summary>Represents a method to be called after the validation of an object.</summary>
	/// <param name="value">The callback method.</param>
	/// <param name="o">The callback method.</param>
	public delegate void ValidatorCallback(object value);
	/// <summary>Converts a string to its canonical format.</summary>
	public sealed class WhiteSpaceTrimStringConverter : ConfigurationConverterBase
	{
		/// <summary>Converts a <see cref="T:System.String" /> to canonical form.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		/// <returns>An object representing the converted value.</returns>
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			return ((string)data).Trim();
		}

		/// <summary>Converts a <see cref="T:System.String" /> to canonical form.</summary>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert to.</param>
		/// <param name="type">The type to convert to.</param>
		/// <returns>An object representing the converted value.</returns>
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value == null)
			{
				return "";
			}
			if (!(value is string))
			{
				throw new ArgumentException("value");
			}
			return ((string)value).Trim();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.WhiteSpaceTrimStringConverter" /> class.</summary>
		public WhiteSpaceTrimStringConverter()
		{
		}
	}
}
namespace System.Configuration.Provider
{
	/// <summary>Provides a base implementation for the extensible provider model.</summary>
	public abstract class ProviderBase
	{
		private bool alreadyInitialized;

		private string _description;

		private string _name;

		/// <summary>Gets the friendly name used to refer to the provider during configuration.</summary>
		/// <returns>The friendly name used to refer to the provider during configuration.</returns>
		public virtual string Name => _name;

		/// <summary>Gets a brief, friendly description suitable for display in administrative tools or other user interfaces (UIs).</summary>
		/// <returns>A brief, friendly description suitable for display in administrative tools or other UIs.</returns>
		public virtual string Description => _description;

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Provider.ProviderBase" /> class.</summary>
		protected ProviderBase()
		{
		}

		/// <summary>Initializes the configuration builder.</summary>
		/// <param name="name">The friendly name of the provider.</param>
		/// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
		/// <exception cref="T:System.ArgumentNullException">The name of the provider is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The name of the provider has a length of zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)" /> on a provider after the provider has already been initialized.</exception>
		public virtual void Initialize(string name, NameValueCollection config)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Provider name cannot be null or empty.", "name");
			}
			if (alreadyInitialized)
			{
				throw new InvalidOperationException("This provider instance has already been initialized.");
			}
			alreadyInitialized = true;
			_name = name;
			if (config != null)
			{
				_description = config["description"];
				config.Remove("description");
			}
			if (string.IsNullOrEmpty(_description))
			{
				_description = _name;
			}
		}
	}
	/// <summary>Represents a collection of provider objects that inherit from <see cref="T:System.Configuration.Provider.ProviderBase" />.</summary>
	public class ProviderCollection : ICollection, IEnumerable
	{
		private Hashtable lookup;

		private bool readOnly;

		private ArrayList values;

		/// <summary>Gets the number of providers in the collection.</summary>
		/// <returns>The number of providers in the collection.</returns>
		public int Count => values.Count;

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		public bool IsSynchronized => false;

		/// <summary>Gets the current object.</summary>
		/// <returns>The current object.</returns>
		public object SyncRoot => this;

		/// <summary>Gets the provider with the specified name.</summary>
		/// <param name="name">The key by which the provider is identified.</param>
		/// <returns>The provider with the specified name.</returns>
		public ProviderBase this[string name]
		{
			get
			{
				object obj = lookup[name];
				if (obj == null)
				{
					return null;
				}
				return values[(int)obj] as ProviderBase;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Provider.ProviderCollection" /> class.</summary>
		public ProviderCollection()
		{
			lookup = new Hashtable(10, StringComparer.InvariantCultureIgnoreCase);
			values = new ArrayList();
		}

		/// <summary>Adds a provider to the collection.</summary>
		/// <param name="provider">The provider to be added.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="provider" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> of <paramref name="provider" /> is <see langword="null" />.  
		/// -or-
		///  The length of the <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> of <paramref name="provider" /> is less than 1.</exception>
		public virtual void Add(ProviderBase provider)
		{
			if (readOnly)
			{
				throw new NotSupportedException();
			}
			if (provider == null || provider.Name == null)
			{
				throw new ArgumentNullException();
			}
			int num = values.Add(provider);
			try
			{
				lookup.Add(provider.Name, num);
			}
			catch
			{
				values.RemoveAt(num);
				throw;
			}
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is set to read-only.</exception>
		public void Clear()
		{
			if (readOnly)
			{
				throw new NotSupportedException();
			}
			values.Clear();
			lookup.Clear();
		}

		/// <summary>Copies the contents of the collection to the given array starting at the specified index.</summary>
		/// <param name="array">The array to copy the elements of the collection to.</param>
		/// <param name="index">The index of the collection item at which to start the copying process.</param>
		public void CopyTo(ProviderBase[] array, int index)
		{
			values.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Configuration.Provider.ProviderCollection" /> to an array, starting at a particular array index.</summary>
		/// <param name="array">The array to copy the elements of the collection to.</param>
		/// <param name="index">The index of the array at which to start copying provider instances from the collection.</param>
		void ICollection.CopyTo(Array array, int index)
		{
			values.CopyTo(array, index);
		}

		/// <summary>Returns an object that implements the <see cref="T:System.Collections.IEnumerator" /> interface to iterate through the collection.</summary>
		/// <returns>An object that implements <see cref="T:System.Collections.IEnumerator" /> to iterate through the collection.</returns>
		public IEnumerator GetEnumerator()
		{
			return values.GetEnumerator();
		}

		/// <summary>Removes a provider from the collection.</summary>
		/// <param name="name">The name of the provider to be removed.</param>
		/// <exception cref="T:System.NotSupportedException">The collection has been set to read-only.</exception>
		public void Remove(string name)
		{
			if (readOnly)
			{
				throw new NotSupportedException();
			}
			object obj = lookup[name];
			if (obj == null || !(obj is int num))
			{
				throw new ArgumentException();
			}
			if (num >= values.Count)
			{
				throw new ArgumentException();
			}
			values.RemoveAt(num);
			lookup.Remove(name);
			ArrayList arrayList = new ArrayList();
			foreach (DictionaryEntry item in lookup)
			{
				if ((int)item.Value > num)
				{
					arrayList.Add(item.Key);
				}
			}
			foreach (string item2 in arrayList)
			{
				lookup[item2] = (int)lookup[item2] - 1;
			}
		}

		/// <summary>Sets the collection to be read-only.</summary>
		public void SetReadOnly()
		{
			readOnly = true;
		}
	}
	/// <summary>The exception that is thrown when a configuration provider error has occurred. This exception class is also used by providers to throw exceptions when internal errors occur within the provider that do not map to other pre-existing exception classes.</summary>
	[Serializable]
	public class ProviderException : Exception
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.Provider.ProviderException" /> class.</summary>
		public ProviderException()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.Provider.ProviderException" /> class.</summary>
		/// <param name="info">The object that holds the information to deserialize.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		protected ProviderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.Provider.ProviderException" /> class.</summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.Provider.ProviderException" /> was thrown.</param>
		public ProviderException(string message)
			: base(message)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.Provider.ProviderException" /> class.</summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.Provider.ProviderException" /> was thrown.</param>
		/// <param name="innerException">The exception that caused this <see cref="T:System.Configuration.Provider.ProviderException" /> to be thrown.</param>
		public ProviderException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
namespace System.Configuration.Internal
{
	/// <summary>Delegates all members of the <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> interface to another instance of a host.</summary>
	public class DelegatingConfigHost : IInternalConfigHost, IInternalConfigurationBuilderHost
	{
		private IInternalConfigHost host;

		/// <summary>Gets or sets the <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object.</returns>
		protected IInternalConfigHost Host
		{
			get
			{
				return host;
			}
			set
			{
				host = value;
			}
		}

		/// <summary>Gets a value indicating whether the configuration is remote.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration is remote; otherwise, <see langword="false" />.</returns>
		public virtual bool IsRemote => host.IsRemote;

		/// <summary>Gets a value indicating whether the host configuration supports change notifications.</summary>
		/// <returns>
		///   <see langword="true" /> if the host supports change notifications; otherwise, <see langword="false" />.</returns>
		public virtual bool SupportsChangeNotifications => host.SupportsChangeNotifications;

		/// <summary>Gets a value indicating whether the host configuration supports location tags.</summary>
		/// <returns>
		///   <see langword="true" /> if the host supports location tags; otherwise, <see langword="false" />.</returns>
		public virtual bool SupportsLocation => host.SupportsLocation;

		/// <summary>Gets a value indicating whether the host configuration has path support.</summary>
		/// <returns>
		///   <see langword="true" /> if the host configuration has path support; otherwise, <see langword="false" />.</returns>
		public virtual bool SupportsPath => host.SupportsPath;

		/// <summary>Gets a value indicating whether the host configuration supports refresh.</summary>
		/// <returns>
		///   <see langword="true" /> if the host configuration supports refresh; otherwise, <see langword="false" />.</returns>
		public virtual bool SupportsRefresh => host.SupportsRefresh;

		/// <summary>Gets the <see cref="T:System.Configuration.Internal.IInternalConfigurationBuilderHost" /> object if the delegated host provides the functionality required by that interface.</summary>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigurationBuilderHost" /> object.</returns>
		protected IInternalConfigurationBuilderHost ConfigBuilderHost
		{
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Internal.DelegatingConfigHost" /> class.</summary>
		protected DelegatingConfigHost()
		{
		}

		/// <summary>Creates a new configuration context.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="locationSubPath">A string representing a location subpath.</param>
		/// <returns>A <see cref="T:System.Object" /> representing a new configuration context.</returns>
		public virtual object CreateConfigurationContext(string configPath, string locationSubPath)
		{
			return host.CreateConfigurationContext(configPath, locationSubPath);
		}

		/// <summary>Creates a deprecated configuration context.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>A <see cref="T:System.Object" /> representing a deprecated configuration context.</returns>
		public virtual object CreateDeprecatedConfigContext(string configPath)
		{
			return host.CreateDeprecatedConfigContext(configPath);
		}

		/// <summary>Decrypts an encrypted configuration section.</summary>
		/// <param name="encryptedXml">An encrypted section of a configuration file.</param>
		/// <param name="protectionProvider">A <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object.</param>
		/// <param name="protectedConfigSection">A <see cref="T:System.Configuration.ProtectedConfigurationSection" /> object.</param>
		/// <returns>A string representing a decrypted configuration section.</returns>
		public virtual string DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection)
		{
			return host.DecryptSection(encryptedXml, protectionProvider, protectedConfigSection);
		}

		/// <summary>Deletes the <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		public virtual void DeleteStream(string streamName)
		{
			host.DeleteStream(streamName);
		}

		/// <summary>Encrypts a section of a configuration object.</summary>
		/// <param name="clearTextXml">A section of the configuration that is not encrypted.</param>
		/// <param name="protectionProvider">A <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object.</param>
		/// <param name="protectedConfigSection">A <see cref="T:System.Configuration.ProtectedConfigurationSection" /> object.</param>
		/// <returns>A string representing an encrypted section of the configuration object.</returns>
		public virtual string EncryptSection(string clearTextXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection)
		{
			return host.EncryptSection(clearTextXml, protectionProvider, protectedConfigSection);
		}

		/// <summary>Returns a configuration path based on a location subpath.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="locationSubPath">A string representing a location subpath.</param>
		/// <returns>A string representing a configuration path.</returns>
		public virtual string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath)
		{
			return host.GetConfigPathFromLocationSubPath(configPath, locationSubPath);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> representing the type of the configuration.</summary>
		/// <param name="typeName">A string representing the configuration type.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> if an exception should be thrown if an error is encountered; <see langword="false" /> if an exception should not be thrown if an error is encountered.</param>
		/// <returns>A <see cref="T:System.Type" /> representing the type of the configuration.</returns>
		public virtual Type GetConfigType(string typeName, bool throwOnError)
		{
			return host.GetConfigType(typeName, throwOnError);
		}

		/// <summary>Returns a string representing the type name of the configuration object.</summary>
		/// <param name="t">A <see cref="T:System.Type" /> object.</param>
		/// <returns>A string representing the type name of the configuration object.</returns>
		public virtual string GetConfigTypeName(Type t)
		{
			return host.GetConfigTypeName(t);
		}

		/// <summary>Sets the specified permission set if available within the host object.</summary>
		/// <param name="configRecord">An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		/// <param name="permissionSet">A <see cref="T:System.Security.PermissionSet" /> object.</param>
		/// <param name="isHostReady">
		///   <see langword="true" /> if the host has finished initialization; otherwise, <see langword="false" />.</param>
		public virtual void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			host.GetRestrictedPermissions(configRecord, out permissionSet, out isHostReady);
		}

		/// <summary>Returns the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>A string representing the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</returns>
		public virtual string GetStreamName(string configPath)
		{
			return host.GetStreamName(configPath);
		}

		/// <summary>Returns the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration source.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="configSource">A string representing the configuration source.</param>
		/// <returns>A string representing the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration source.</returns>
		public virtual string GetStreamNameForConfigSource(string streamName, string configSource)
		{
			return host.GetStreamNameForConfigSource(streamName, configSource);
		}

		/// <summary>Returns a <see cref="P:System.Diagnostics.FileVersionInfo.FileVersion" /> object representing the version of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <returns>A <see cref="P:System.Diagnostics.FileVersionInfo.FileVersion" /> object representing the version of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</returns>
		public virtual object GetStreamVersion(string streamName)
		{
			return host.GetStreamVersion(streamName);
		}

		/// <summary>Instructs the host to impersonate and returns an <see cref="T:System.IDisposable" /> object required internally by the .NET Framework.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> value.</returns>
		public virtual IDisposable Impersonate()
		{
			return host.Impersonate();
		}

		/// <summary>Initializes the configuration host.</summary>
		/// <param name="configRoot">An <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</param>
		/// <param name="hostInitParams">A parameter object containing the values used for initializing the configuration host.</param>
		public virtual void Init(IInternalConfigRoot configRoot, params object[] hostInitParams)
		{
			host.Init(configRoot, hostInitParams);
		}

		/// <summary>Initializes the host for configuration.</summary>
		/// <param name="locationSubPath">A string representing a location subpath (passed by reference).</param>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="locationConfigPath">The location configuration path.</param>
		/// <param name="configRoot">The configuration root element.</param>
		/// <param name="hostInitConfigurationParams">A parameter object representing the parameters used to initialize the host.</param>
		public virtual void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot configRoot, params object[] hostInitConfigurationParams)
		{
			host.InitForConfiguration(ref locationSubPath, out configPath, out locationConfigPath, configRoot, hostInitConfigurationParams);
		}

		/// <summary>Returns a value indicating whether the configuration is above the application configuration in the configuration hierarchy.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration is above the application configuration in the configuration hierarchy; otherwise, <see langword="false" />.</returns>
		public virtual bool IsAboveApplication(string configPath)
		{
			return host.IsAboveApplication(configPath);
		}

		/// <summary>Returns a value indicating whether a configuration record is required for the host configuration initialization.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if a configuration record is required for the host configuration initialization; otherwise, <see langword="false" />.</returns>
		public virtual bool IsConfigRecordRequired(string configPath)
		{
			return host.IsConfigRecordRequired(configPath);
		}

		/// <summary>Restricts or allows definitions in the host configuration.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="allowDefinition">The <see cref="T:System.Configuration.ConfigurationAllowDefinition" /> object.</param>
		/// <param name="allowExeDefinition">The <see cref="T:System.Configuration.ConfigurationAllowExeDefinition" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the grant or restriction of definitions in the host configuration was successful; otherwise, <see langword="false" />.</returns>
		public virtual bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			return host.IsDefinitionAllowed(configPath, allowDefinition, allowExeDefinition);
		}

		/// <summary>Returns a value indicating whether the initialization of a configuration object is considered delayed.</summary>
		/// <param name="configRecord">The <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the initialization of a configuration object is considered delayed; otherwise, <see langword="false" />.</returns>
		public virtual bool IsInitDelayed(IInternalConfigRecord configRecord)
		{
			return host.IsInitDelayed(configRecord);
		}

		/// <summary>Returns a value indicating whether the file path used by a <see cref="T:System.IO.Stream" /> object to read a configuration file is a valid path.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the path used by a <see cref="T:System.IO.Stream" /> object to read a configuration file is a valid path; otherwise, <see langword="false" />.</returns>
		public virtual bool IsFile(string streamName)
		{
			return host.IsFile(streamName);
		}

		/// <summary>Returns a value indicating whether a configuration section requires a fully trusted code access security level and does not allow the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> attribute to disable implicit link demands.</summary>
		/// <param name="configRecord">The <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration section requires a fully trusted code access security level and does not allow the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> attribute to disable implicit link demands; otherwise, <see langword="false" />.</returns>
		public virtual bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord)
		{
			return host.IsFullTrustSectionWithoutAptcaAllowed(configRecord);
		}

		/// <summary>Returns a value indicating whether the configuration object supports a location tag.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration object supports a location tag; otherwise, <see langword="false" />.</returns>
		public virtual bool IsLocationApplicable(string configPath)
		{
			return host.IsLocationApplicable(configPath);
		}

		/// <summary>Returns a value indicating whether a configuration path is to a configuration node whose contents should be treated as a root.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration path is to a configuration node whose contents should be treated as a root; otherwise, <see langword="false" />.</returns>
		public virtual bool IsSecondaryRoot(string configPath)
		{
			return host.IsSecondaryRoot(configPath);
		}

		/// <summary>Returns a value indicating whether the configuration path is trusted.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration path is trusted; otherwise, <see langword="false" />.</returns>
		public virtual bool IsTrustedConfigPath(string configPath)
		{
			return host.IsTrustedConfigPath(configPath);
		}

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object to read a configuration file.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <returns>The object specified by <paramref name="streamName" />.</returns>
		public virtual Stream OpenStreamForRead(string streamName)
		{
			return host.OpenStreamForRead(streamName);
		}

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object to read a configuration file.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="assertPermissions">
		///   <see langword="true" /> to assert permissions; otherwise, <see langword="false" />.</param>
		/// <returns>The object specified by <paramref name="streamName" />.</returns>
		public virtual Stream OpenStreamForRead(string streamName, bool assertPermissions)
		{
			return host.OpenStreamForRead(streamName, assertPermissions);
		}

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object for writing to a configuration file or for writing to a temporary file used to build a configuration file. Allows a <see cref="T:System.IO.Stream" /> object to be designated as a template for copying file attributes.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="templateStreamName">The name of a <see cref="T:System.IO.Stream" /> object from which file attributes are to be copied as a template.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object (passed by reference).</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
		{
			return host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext);
		}

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object for writing to a configuration file. Allows a <see cref="T:System.IO.Stream" /> object to be designated as a template for copying file attributes.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="templateStreamName">The name of a <see cref="T:System.IO.Stream" /> object from which file attributes are to be copied as a template.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file (passed by reference).</param>
		/// <param name="assertPermissions">
		///   <see langword="true" /> to assert permissions; otherwise, <see langword="false" />.</param>
		/// <returns>The object specified by the <paramref name="streamName" /> parameter.</returns>
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions)
		{
			return host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext, assertPermissions);
		}

		/// <summary>Returns a value indicating whether the entire configuration file could be read by a designated <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the entire configuration file could be read by the <see cref="T:System.IO.Stream" /> object designated by <paramref name="streamName" />; otherwise, <see langword="false" />.</returns>
		public virtual bool PrefetchAll(string configPath, string streamName)
		{
			return host.PrefetchAll(configPath, streamName);
		}

		/// <summary>Instructs the <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object to read a designated section of its associated configuration file.</summary>
		/// <param name="sectionGroupName">A string representing the name of a section group in the configuration file.</param>
		/// <param name="sectionName">A string representing the name of a section in the configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if a section of the configuration file designated by the <paramref name="sectionGroupName" /> and <paramref name="sectionName" /> parameters can be read by a <see cref="T:System.IO.Stream" /> object; otherwise, <see langword="false" />.</returns>
		public virtual bool PrefetchSection(string sectionGroupName, string sectionName)
		{
			return host.PrefetchSection(sectionGroupName, sectionName);
		}

		/// <summary>Indicates that a new configuration record requires a complete initialization.</summary>
		/// <param name="configRecord">An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		public virtual void RequireCompleteInit(IInternalConfigRecord configRecord)
		{
			host.RequireCompleteInit(configRecord);
		}

		/// <summary>Instructs the host to monitor an associated <see cref="T:System.IO.Stream" /> object for changes in a configuration file.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="callback">A <see cref="T:System.Configuration.Internal.StreamChangeCallback" /> object to receive the returned data representing the changes in the configuration file.</param>
		/// <returns>An <see cref="T:System.Object" /> instance containing changed configuration settings.</returns>
		public virtual object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			return host.StartMonitoringStreamForChanges(streamName, callback);
		}

		/// <summary>Instructs the host object to stop monitoring an associated <see cref="T:System.IO.Stream" /> object for changes in a configuration file.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="callback">A <see cref="T:System.Configuration.Internal.StreamChangeCallback" /> object.</param>
		public virtual void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			host.StopMonitoringStreamForChanges(streamName, callback);
		}

		/// <summary>Verifies that a configuration definition is allowed for a configuration record.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="allowDefinition">An <see cref="P:System.Configuration.SectionInformation.AllowDefinition" /> object.</param>
		/// <param name="allowExeDefinition">A <see cref="T:System.Configuration.ConfigurationAllowExeDefinition" /> object</param>
		/// <param name="errorInfo">An <see cref="T:System.Configuration.Internal.IConfigErrorInfo" /> object.</param>
		public virtual void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo)
		{
			host.VerifyDefinitionAllowed(configPath, allowDefinition, allowExeDefinition, errorInfo);
		}

		/// <summary>Indicates that all writing to the configuration file has completed.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="success">
		///   <see langword="true" /> if writing to the configuration file completed successfully; otherwise, <see langword="false" />.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		public virtual void WriteCompleted(string streamName, bool success, object writeContext)
		{
			host.WriteCompleted(streamName, success, writeContext);
		}

		/// <summary>Indicates that all writing to the configuration file has completed and specifies whether permissions should be asserted.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="success">
		///   <see langword="true" /> to indicate that writing was completed successfully; otherwise, <see langword="false" />.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="assertPermissions">
		///   <see langword="true" /> to assert permissions; otherwise, <see langword="false" />.</param>
		public virtual void WriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions)
		{
			host.WriteCompleted(streamName, success, writeContext, assertPermissions);
		}

		/// <summary>Processes a <see cref="T:System.Configuration.ConfigurationSection" /> object using the provided <see cref="T:System.Configuration.ConfigurationBuilder" />.</summary>
		/// <param name="configSection">The <see cref="T:System.Configuration.ConfigurationSection" /> to process.</param>
		/// <param name="builder">
		///   <see cref="T:System.Configuration.ConfigurationBuilder" /> to use to process the <paramref name="configSection" />.</param>
		/// <returns>The processed <see cref="T:System.Configuration.ConfigurationSection" />.</returns>
		public virtual ConfigurationSection ProcessConfigurationSection(ConfigurationSection configSection, ConfigurationBuilder builder)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Processes the markup of a configuration section using the provided <see cref="T:System.Configuration.ConfigurationBuilder" />.</summary>
		/// <param name="rawXml">The <see cref="T:System.Xml.XmlNode" /> to process.</param>
		/// <param name="builder">
		///   <see cref="T:System.Configuration.ConfigurationBuilder" /> to use to process the <paramref name="rawXml" />.</param>
		/// <returns>The processed <see cref="T:System.Xml.XmlNode" />.</returns>
		public virtual XmlNode ProcessRawXml(XmlNode rawXml, ConfigurationBuilder builder)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
	/// <summary>Defines an interface used by the .NET Framework to support creating error configuration records.</summary>
	public interface IConfigErrorInfo
	{
		/// <summary>Gets a string specifying the file name related to the configuration details.</summary>
		/// <returns>A string specifying a filename.</returns>
		string Filename { get; }

		/// <summary>Gets an integer specifying the line number related to the configuration details.</summary>
		/// <returns>An integer specifying a line number.</returns>
		int LineNumber { get; }
	}
	/// <summary>Defines an interface used by the .NET Framework to support the initialization of configuration properties.</summary>
	public interface IConfigSystem
	{
		/// <summary>Gets the configuration host.</summary>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object that is used by the .NET Framework to initialize application configuration properties.</returns>
		IInternalConfigHost Host { get; }

		/// <summary>Gets the root of the configuration hierarchy.</summary>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</returns>
		IInternalConfigRoot Root { get; }

		/// <summary>Initializes a configuration object.</summary>
		/// <param name="typeConfigHost">The type of configuration host.</param>
		/// <param name="hostInitParams">An array of configuration host parameters.</param>
		void Init(Type typeConfigHost, params object[] hostInitParams);
	}
	/// <summary>Defines an interface used by the .NET Framework to support configuration management.</summary>
	[ComVisible(false)]
	public interface IConfigurationManagerHelper
	{
		/// <summary>Ensures that the networking configuration is loaded.</summary>
		void EnsureNetConfigLoaded();
	}
	/// <summary>Defines an interface used by the .NET Framework to initialize configuration properties.</summary>
	[ComVisible(false)]
	public interface IConfigurationManagerInternal
	{
		/// <summary>Gets the configuration file name related to the application path.</summary>
		/// <returns>A string value representing a configuration file name.</returns>
		string ApplicationConfigUri { get; }

		/// <summary>Gets the local configuration directory of the application based on the entry assembly.</summary>
		/// <returns>A string representing the local configuration directory.</returns>
		string ExeLocalConfigDirectory { get; }

		/// <summary>Gets the local configuration path of the application based on the entry assembly.</summary>
		/// <returns>A string value representing the local configuration path of the application.</returns>
		string ExeLocalConfigPath { get; }

		/// <summary>Gets the product name of the application based on the entry assembly.</summary>
		/// <returns>A string value representing the product name of the application.</returns>
		string ExeProductName { get; }

		/// <summary>Gets the product version of the application based on the entry assembly.</summary>
		/// <returns>A string value representing the product version of the application.</returns>
		string ExeProductVersion { get; }

		/// <summary>Gets the roaming configuration directory of the application based on the entry assembly.</summary>
		/// <returns>A string value representing the roaming configuration directory of the application.</returns>
		string ExeRoamingConfigDirectory { get; }

		/// <summary>Gets the roaming user's configuration path based on the application's entry assembly.</summary>
		/// <returns>A string value representing the roaming user's configuration path.</returns>
		string ExeRoamingConfigPath { get; }

		/// <summary>Gets the configuration path for the Machine.config file.</summary>
		/// <returns>A string value representing the path of the Machine.config file.</returns>
		string MachineConfigPath { get; }

		/// <summary>Gets a value representing the configuration system's status.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration system is in the process of being initialized; otherwise, <see langword="false" />.</returns>
		bool SetConfigurationSystemInProgress { get; }

		/// <summary>Gets a value that specifies whether user configuration settings are supported.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration system supports user configuration settings; otherwise, <see langword="false" />.</returns>
		bool SupportsUserConfig { get; }

		/// <summary>Gets the name of the file used to store user configuration settings.</summary>
		/// <returns>A string specifying the name of the file used to store user configuration.</returns>
		string UserConfigFilename { get; }
	}
	/// <summary>Defines interfaces that allow the internal .NET Framework infrastructure to customize configuration.</summary>
	[ComVisible(false)]
	public interface IInternalConfigClientHost
	{
		/// <summary>Returns the path to the application configuration file.</summary>
		/// <returns>A string representing the path to the application configuration file.</returns>
		string GetExeConfigPath();

		/// <summary>Returns a string representing the path to the known local user configuration file.</summary>
		/// <returns>A string representing the path to the known local user configuration file.</returns>
		string GetLocalUserConfigPath();

		/// <summary>Returns a string representing the path to the known roaming user configuration file.</summary>
		/// <returns>A string representing the path to the known roaming user configuration file.</returns>
		string GetRoamingUserConfigPath();

		/// <summary>Returns a value indicating whether a configuration file path is the same as a currently known application configuration file path.</summary>
		/// <param name="configPath">A string representing the path to the application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if a string representing a configuration path is the same as a path to the application configuration file; <see langword="false" /> if a string representing a configuration path is not the same as a path to the application configuration file.</returns>
		bool IsExeConfig(string configPath);

		/// <summary>Returns a value indicating whether a configuration file path is the same as the configuration file path for the currently known local user.</summary>
		/// <param name="configPath">A string representing the path to the application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if a string representing a configuration path is the same as a path to a known local user configuration file; otherwise, <see langword="false" />.</returns>
		bool IsLocalUserConfig(string configPath);

		/// <summary>Returns a value indicating whether a configuration file path is the same as the configuration file path for the currently known roaming user.</summary>
		/// <param name="configPath">A string representing the path to an application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if a string representing a configuration path is the same as a path to a known roaming user configuration file; otherwise, <see langword="false" />.</returns>
		bool IsRoamingUserConfig(string configPath);
	}
	/// <summary>Defines the interfaces used by the internal design time API to create a <see cref="T:System.Configuration.Configuration" /> object.</summary>
	[ComVisible(false)]
	public interface IInternalConfigConfigurationFactory
	{
		/// <summary>Creates and initializes a <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <param name="typeConfigHost">The <see cref="T:System.Type" /> of the <see cref="T:System.Configuration.Configuration" /> object to be created.</param>
		/// <param name="hostInitConfigurationParams">A parameter array of <see cref="T:System.Object" /> that contains the parameters to be applied to the created <see cref="T:System.Configuration.Configuration" /> object.</param>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		Configuration Create(Type typeConfigHost, params object[] hostInitConfigurationParams);

		/// <summary>Normalizes a location subpath of a path to a configuration file.</summary>
		/// <param name="subPath">A string representing the path to the configuration file.</param>
		/// <param name="errorInfo">An instance of <see cref="T:System.Configuration.Internal.IConfigErrorInfo" /> or <see langword="null" />.</param>
		/// <returns>A normalized subpath string.</returns>
		string NormalizeLocationSubPath(string subPath, IConfigErrorInfo errorInfo);
	}
	/// <summary>Defines interfaces used by internal .NET structures to initialize application configuration properties.</summary>
	[ComVisible(false)]
	public interface IInternalConfigHost
	{
		/// <summary>Returns a value indicating whether the configuration is remote.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration is remote; otherwise, <see langword="false" />.</returns>
		bool IsRemote { get; }

		/// <summary>Returns a value indicating whether the host configuration supports change notification.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration supports change notification; otherwise, <see langword="false" />.</returns>
		bool SupportsChangeNotifications { get; }

		/// <summary>Returns a value indicating whether the host configuration supports location tags.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration supports location tags; otherwise, <see langword="false" />.</returns>
		bool SupportsLocation { get; }

		/// <summary>Returns a value indicating whether the host configuration supports path tags.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration supports path tags; otherwise, <see langword="false" />.</returns>
		bool SupportsPath { get; }

		/// <summary>Returns a value indicating whether the host configuration supports configuration refresh.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration supports configuration refresh; otherwise, <see langword="false" />.</returns>
		bool SupportsRefresh { get; }

		/// <summary>Creates and returns a context object for a <see cref="T:System.Configuration.ConfigurationElement" /> of an application configuration.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="locationSubPath">A string representing a subpath location of the configuration element.</param>
		/// <returns>A context object for a <see cref="T:System.Configuration.ConfigurationElement" /> object of an application configuration.</returns>
		object CreateConfigurationContext(string configPath, string locationSubPath);

		/// <summary>Creates and returns a deprecated context object of the application configuration.</summary>
		/// <param name="configPath">A string representing a path to an application configuration file.</param>
		/// <returns>A deprecated context object of the application configuration.</returns>
		object CreateDeprecatedConfigContext(string configPath);

		/// <summary>Decrypts an encrypted configuration section and returns it as a string.</summary>
		/// <param name="encryptedXml">An encrypted XML string representing a configuration section.</param>
		/// <param name="protectionProvider">The <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object.</param>
		/// <param name="protectedConfigSection">The <see cref="T:System.Configuration.ProtectedConfigurationSection" /> object.</param>
		/// <returns>A decrypted configuration section as a string.</returns>
		string DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection);

		/// <summary>Deletes the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the application configuration file.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		void DeleteStream(string streamName);

		/// <summary>Encrypts a configuration section and returns it as a string.</summary>
		/// <param name="clearTextXml">An XML string representing a configuration section to encrypt.</param>
		/// <param name="protectionProvider">The <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object.</param>
		/// <param name="protectedConfigSection">The <see cref="T:System.Configuration.ProtectedConfigurationSection" /> object.</param>
		/// <returns>An encrypted configuration section represented as a string.</returns>
		string EncryptSection(string clearTextXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection);

		/// <summary>Returns the complete path to an application configuration file based on the location subpath.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="locationSubPath">The subpath location of the configuration file.</param>
		/// <returns>A string representing the complete path to an application configuration file.</returns>
		string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath);

		/// <summary>Returns a <see cref="T:System.Type" /> object representing the type of the configuration object.</summary>
		/// <param name="typeName">The type name</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw an exception if an error occurs; otherwise, <see langword="false" /></param>
		/// <returns>A <see cref="T:System.Type" /> object representing the type of the configuration object.</returns>
		Type GetConfigType(string typeName, bool throwOnError);

		/// <summary>Returns a string representing a type name from the <see cref="T:System.Type" /> object representing the type of the configuration.</summary>
		/// <param name="t">A <see cref="T:System.Type" /> object.</param>
		/// <returns>A string representing the type name from a <see cref="T:System.Type" /> object representing the type of the configuration.</returns>
		string GetConfigTypeName(Type t);

		/// <summary>Associates the configuration with a <see cref="T:System.Security.PermissionSet" /> object.</summary>
		/// <param name="configRecord">An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		/// <param name="permissionSet">The <see cref="T:System.Security.PermissionSet" /> object to associate with the configuration.</param>
		/// <param name="isHostReady">
		///   <see langword="true" /> to indicate the configuration host is has completed building associated permissions; otherwise, <see langword="false" />.</param>
		void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady);

		/// <summary>Returns a string representing the configuration file name associated with the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <returns>A string representing the configuration file name associated with the <see cref="T:System.IO.Stream" /> I/O tasks on the configuration file.</returns>
		string GetStreamName(string configPath);

		/// <summary>Returns a string representing the configuration file name associated with the <see cref="T:System.IO.Stream" /> object performing I/O tasks on a remote configuration file.</summary>
		/// <param name="streamName">A string representing the configuration file name associated with the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="configSource">A string representing a path to a remote configuration file.</param>
		/// <returns>A string representing the configuration file name associated with the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</returns>
		string GetStreamNameForConfigSource(string streamName, string configSource);

		/// <summary>Returns the version of the <see cref="T:System.IO.Stream" /> object associated with configuration file.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <returns>The version of the <see cref="T:System.IO.Stream" /> object associated with configuration file.</returns>
		object GetStreamVersion(string streamName);

		/// <summary>Instructs the host to impersonate and returns an <see cref="T:System.IDisposable" /> object required by the internal .NET structure.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> value.</returns>
		IDisposable Impersonate();

		/// <summary>Initializes a configuration host.</summary>
		/// <param name="configRoot">The configuration root object.</param>
		/// <param name="hostInitParams">The parameter object containing the values used for initializing the configuration host.</param>
		void Init(IInternalConfigRoot configRoot, params object[] hostInitParams);

		/// <summary>Initializes a configuration object.</summary>
		/// <param name="locationSubPath">The subpath location of the configuration file.</param>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="locationConfigPath">A string representing the location of a configuration path.</param>
		/// <param name="configRoot">The <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</param>
		/// <param name="hostInitConfigurationParams">The parameter object containing the values used for initializing the configuration host.</param>
		void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot configRoot, params object[] hostInitConfigurationParams);

		/// <summary>Returns a value indicating whether the configuration file is located at a higher level in the configuration hierarchy than the application configuration.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> the configuration file is located at a higher level in the configuration hierarchy than the application configuration; otherwise, <see langword="false" />.</returns>
		bool IsAboveApplication(string configPath);

		/// <summary>Returns a value indicating whether a child record is required for a child configuration path.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if child record is required for a child configuration path; otherwise, <see langword="false" />.</returns>
		bool IsConfigRecordRequired(string configPath);

		/// <summary>Determines if a different <see cref="T:System.Type" /> definition is allowable for an application configuration object.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="allowDefinition">A <see cref="T:System.Configuration.ConfigurationAllowDefinition" /> object.</param>
		/// <param name="allowExeDefinition">A <see cref="T:System.Configuration.ConfigurationAllowExeDefinition" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if a different <see cref="T:System.Type" /> definition is allowable for an application configuration object; otherwise, <see langword="false" />.</returns>
		bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition);

		/// <summary>Returns a value indicating whether the file path used by a <see cref="T:System.IO.Stream" /> object to read a configuration file is a valid path.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the path used by a <see cref="T:System.IO.Stream" /> object to read a configuration file is a valid path; otherwise, <see langword="false" />.</returns>
		bool IsFile(string streamName);

		/// <summary>Returns a value indicating whether a configuration section requires a fully trusted code access security level and does not allow the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> attribute to disable implicit link demands.</summary>
		/// <param name="configRecord">The <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration section requires a fully trusted code access security level and does not allow the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> attribute to disable implicit link demands; otherwise, <see langword="false" />.</returns>
		bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord);

		/// <summary>Returns a value indicating whether the initialization of a configuration object is considered delayed.</summary>
		/// <param name="configRecord">The <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the initialization of a configuration object is considered delayed; otherwise, <see langword="false" />.</returns>
		bool IsInitDelayed(IInternalConfigRecord configRecord);

		/// <summary>Returns a value indicating whether the configuration object supports a location tag.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration object supports a location tag; otherwise, <see langword="false" />.</returns>
		bool IsLocationApplicable(string configPath);

		/// <summary>Returns a value indicating whether a configuration path is to a configuration node whose contents should be treated as a root.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration path is to a configuration node whose contents should be treated as a root; otherwise, <see langword="false" />.</returns>
		bool IsSecondaryRoot(string configPath);

		/// <summary>Returns a value indicating whether the configuration path is trusted.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration path is trusted; otherwise, <see langword="false" />.</returns>
		bool IsTrustedConfigPath(string configPath);

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> to read a configuration file.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		Stream OpenStreamForRead(string streamName);

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object to read a configuration file.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="assertPermissions">
		///   <see langword="true" /> to assert permissions; otherwise, <see langword="false" />.</param>
		/// <returns>The object specified by <paramref name="streamName" />.</returns>
		Stream OpenStreamForRead(string streamName, bool assertPermissions);

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object for writing to a configuration file or for writing to a temporary file used to build a configuration file. Allows a <see cref="T:System.IO.Stream" /> object to be designated as a template for copying file attributes.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="templateStreamName">The name of a <see cref="T:System.IO.Stream" /> object from which file attributes are to be copied as a template.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext);

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object for writing to a configuration file. Allows a <see cref="T:System.IO.Stream" /> object to be designated as a template for copying file attributes.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="templateStreamName">The name of a <see cref="T:System.IO.Stream" /> from which file attributes are to be copied as a template.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="assertPermissions">
		///   <see langword="true" /> to assert permissions; otherwise, <see langword="false" />.</param>
		/// <returns>The object specified by <paramref name="streamName" />.</returns>
		Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions);

		/// <summary>Returns a value that indicates whether the entire configuration file could be read by a designated <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the entire configuration file could be read by the <see cref="T:System.IO.Stream" /> object designated by <paramref name="streamName" />; otherwise, <see langword="false" />.</returns>
		bool PrefetchAll(string configPath, string streamName);

		/// <summary>Instructs the <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object to read a designated section of its associated configuration file.</summary>
		/// <param name="sectionGroupName">A string representing the identifying name of a configuration file section group.</param>
		/// <param name="sectionName">A string representing the identifying name of a configuration file section.</param>
		/// <returns>
		///   <see langword="true" /> if a section of the configuration file designated by <paramref name="sectionGroupName" /> and <paramref name="sectionName" /> could be read by a <see cref="T:System.IO.Stream" /> object; otherwise, <see langword="false" />.</returns>
		bool PrefetchSection(string sectionGroupName, string sectionName);

		/// <summary>Indicates a new configuration record requires a complete initialization.</summary>
		/// <param name="configRecord">An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		void RequireCompleteInit(IInternalConfigRecord configRecord);

		/// <summary>Instructs the <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object to monitor an associated <see cref="T:System.IO.Stream" /> object for changes in a configuration file.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="callback">A <see cref="T:System.Configuration.Internal.StreamChangeCallback" /> object to receive the returned data representing the changes in the configuration file.</param>
		/// <returns>An <see cref="T:System.Object" /> containing changed configuration settings.</returns>
		object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback);

		/// <summary>Instructs the  <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object to stop monitoring an associated <see cref="T:System.IO.Stream" /> object for changes in a configuration file.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="callback">A <see cref="T:System.Configuration.Internal.StreamChangeCallback" /> object.</param>
		void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback);

		/// <summary>Verifies that a configuration definition is allowed for a configuration record.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="allowDefinition">A <see cref="P:System.Configuration.SectionInformation.AllowDefinition" /> object.</param>
		/// <param name="allowExeDefinition">A <see cref="T:System.Configuration.ConfigurationAllowExeDefinition" /> object</param>
		/// <param name="errorInfo">An <see cref="T:System.Configuration.Internal.IConfigErrorInfo" /> object.</param>
		void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo);

		/// <summary>Indicates that all writing to the configuration file has completed.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="success">
		///   <see langword="true" /> if the write to the configuration file was completed successfully; otherwise, <see langword="false" />.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		void WriteCompleted(string streamName, bool success, object writeContext);

		/// <summary>Indicates that all writing to the configuration file has completed and specifies whether permissions should be asserted.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="success">
		///   <see langword="true" /> to indicate the write was completed successfully; otherwise, <see langword="false" />.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="assertPermissions">
		///   <see langword="true" /> to assert permissions; otherwise, <see langword="false" />.</param>
		void WriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions);
	}
	/// <summary>Defines interfaces used by internal .NET structures to support creation of new configuration records.</summary>
	[ComVisible(false)]
	public interface IInternalConfigRecord
	{
		/// <summary>Gets a string representing a configuration file path.</summary>
		/// <returns>A string representing a configuration file path.</returns>
		string ConfigPath { get; }

		/// <summary>Returns a value indicating whether an error occurred during initialization of a configuration object.</summary>
		/// <returns>
		///   <see langword="true" /> if an error occurred during initialization of a configuration object; otherwise, <see langword="false" />.</returns>
		bool HasInitErrors { get; }

		/// <summary>Returns the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</summary>
		/// <returns>A string representing the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</returns>
		string StreamName { get; }

		/// <summary>Returns an object representing a section of a configuration from the last-known-good (LKG) configuration.</summary>
		/// <param name="configKey">A string representing a key to a configuration section.</param>
		/// <returns>An <see cref="T:System.Object" /> instance representing the section of the last-known-good configuration specified by <paramref name="configKey" />.</returns>
		object GetLkgSection(string configKey);

		/// <summary>Returns an <see cref="T:System.Object" /> instance representing a section of a configuration file.</summary>
		/// <param name="configKey">A string representing a key to a configuration section.</param>
		/// <returns>An <see cref="T:System.Object" /> instance representing a section of a configuration file.</returns>
		object GetSection(string configKey);

		/// <summary>Causes a specified section of the configuration object to be reinitialized.</summary>
		/// <param name="configKey">A string representing a key to a configuration section that is to be refreshed.</param>
		void RefreshSection(string configKey);

		/// <summary>Removes a configuration record.</summary>
		void Remove();

		/// <summary>Grants the configuration object the permission to throw an exception if an error occurs during initialization.</summary>
		void ThrowIfInitErrors();
	}
	/// <summary>Defines interfaces used by internal .NET structures to support a configuration root object.</summary>
	[ComVisible(false)]
	public interface IInternalConfigRoot
	{
		/// <summary>Returns a value indicating whether the configuration is a design-time configuration.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration is a design-time configuration; <see langword="false" /> if the configuration is not a design-time configuration.</returns>
		bool IsDesignTime { get; }

		/// <summary>Represents the method that handles the <see cref="E:System.Configuration.Internal.IInternalConfigRoot.ConfigChanged" /> event of an <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</summary>
		event InternalConfigEventHandler ConfigChanged;

		/// <summary>Represents the method that handles the <see cref="E:System.Configuration.Internal.IInternalConfigRoot.ConfigRemoved" /> event of a <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</summary>
		event InternalConfigEventHandler ConfigRemoved;

		/// <summary>Returns an <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object representing a configuration specified by a configuration path.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object representing a configuration specified by <paramref name="configPath" />.</returns>
		IInternalConfigRecord GetConfigRecord(string configPath);

		/// <summary>Returns an <see cref="T:System.Object" /> representing the data in a section of a configuration file.</summary>
		/// <param name="section">A string representing a section of a configuration file.</param>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the data in a section of a configuration file.</returns>
		object GetSection(string section, string configPath);

		/// <summary>Returns a value representing the file path of the nearest configuration ancestor that has configuration data.</summary>
		/// <param name="configPath">The path of configuration file.</param>
		/// <returns>A string representing the file path of the nearest configuration ancestor that has configuration data.</returns>
		string GetUniqueConfigPath(string configPath);

		/// <summary>Returns an <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object representing a unique configuration record for given configuration path.</summary>
		/// <param name="configPath">The path of the configuration file.</param>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object representing a unique configuration record for a given configuration path.</returns>
		IInternalConfigRecord GetUniqueConfigRecord(string configPath);

		/// <summary>Initializes a configuration object.</summary>
		/// <param name="host">An <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object.</param>
		/// <param name="isDesignTime">
		///   <see langword="true" /> if design time; <see langword="false" /> if run time.</param>
		void Init(IInternalConfigHost host, bool isDesignTime);

		/// <summary>Finds and removes a configuration record and all its children for a given configuration path.</summary>
		/// <param name="configPath">The path of the configuration file.</param>
		void RemoveConfig(string configPath);
	}
	/// <summary>Defines an interface used by the configuration system to set the <see cref="T:System.Configuration.ConfigurationSettings" /> class.</summary>
	[ComVisible(false)]
	public interface IInternalConfigSettingsFactory
	{
		/// <summary>Indicates that initialization of the configuration system has completed.</summary>
		void CompleteInit();

		/// <summary>Provides hierarchical configuration settings and extensions specific to ASP.NET to the configuration system.</summary>
		/// <param name="internalConfigSystem">An <see cref="T:System.Configuration.Internal.IInternalConfigSystem" /> object used by the <see cref="T:System.Configuration.ConfigurationSettings" /> class.</param>
		/// <param name="initComplete">
		///   <see langword="true" /> if the initialization process of the configuration system is complete; otherwise, <see langword="false" />.</param>
		void SetConfigurationSystem(IInternalConfigSystem internalConfigSystem, bool initComplete);
	}
	/// <summary>Defines an interface used by the .NET Framework to initialize application configuration properties.</summary>
	[ComVisible(false)]
	public interface IInternalConfigSystem
	{
		/// <summary>Gets a value indicating whether the user configuration is supported.</summary>
		/// <returns>
		///   <see langword="true" /> if the user configuration is supported; otherwise, <see langword="false" />.</returns>
		bool SupportsUserConfig { get; }

		/// <summary>Returns the configuration object based on the specified key.</summary>
		/// <param name="configKey">The configuration key value.</param>
		/// <returns>A configuration object.</returns>
		object GetSection(string configKey);

		/// <summary>Refreshes the configuration system based on the specified section name.</summary>
		/// <param name="sectionName">The name of the configuration section.</param>
		void RefreshConfig(string sectionName);
	}
	/// <summary>Defines a class that allows the .NET Framework infrastructure to specify event arguments for configuration events.</summary>
	public sealed class InternalConfigEventArgs : EventArgs
	{
		private string configPath;

		/// <summary>Gets or sets the configuration path related to the <see cref="T:System.Configuration.Internal.InternalConfigEventArgs" /> object.</summary>
		/// <returns>A string value specifying the configuration path.</returns>
		public string ConfigPath
		{
			get
			{
				return configPath;
			}
			set
			{
				configPath = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Internal.InternalConfigEventArgs" /> class.</summary>
		/// <param name="configPath">A configuration path.</param>
		public InternalConfigEventArgs(string configPath)
		{
			this.configPath = configPath;
		}
	}
	/// <summary>Defines a class used by the .NET Framework infrastructure to support configuration events.</summary>
	/// <param name="sender">The source object of the event.</param>
	/// <param name="e">A configuration event argument.</param>
	public delegate void InternalConfigEventHandler(object sender, InternalConfigEventArgs e);
	/// <summary>Represents a method for hosts to call when a monitored stream has changed.</summary>
	/// <param name="streamName">The name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
	public delegate void StreamChangeCallback(string streamName);
}
namespace System.Configuration
{
	/// <summary>Represents the base class to be extended by custom configuration builder implementations.</summary>
	public abstract class ConfigurationBuilder : ProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationBuilder" /> class.</summary>
		protected ConfigurationBuilder()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Accepts a <see cref="T:System.Configuration.ConfigurationSection" /> object from the configuration system and returns a modified or new <see cref="T:System.Configuration.ConfigurationSection" /> object for further use.</summary>
		/// <param name="configSection">The <see cref="T:System.Configuration.ConfigurationSection" /> to process.</param>
		/// <returns>The processed <see cref="T:System.Configuration.ConfigurationSection" />.</returns>
		public virtual ConfigurationSection ProcessConfigurationSection(ConfigurationSection configSection)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Accepts an <see cref="T:System.Xml.XmlNode" /> representing the raw configuration section from a config file and returns a modified or new <see cref="T:System.Xml.XmlNode" /> for further use.</summary>
		/// <param name="rawXml">The <see cref="T:System.Xml.XmlNode" /> to process.</param>
		/// <returns>The processed <see cref="T:System.Xml.XmlNode" />.</returns>
		public virtual XmlNode ProcessRawXml(XmlNode rawXml)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
	/// <summary>Specifies the override behavior of a configuration element for configuration elements in child directories.</summary>
	public enum OverrideMode
	{
		/// <summary>The configuration setting of the element or group can be overridden by configuration settings that are in child directories.</summary>
		Allow = 1,
		/// <summary>The configuration setting of the element or group cannot be overridden by configuration settings that are in child directories.</summary>
		Deny = 2,
		/// <summary>The configuration setting of the element or group will be overridden by configuration settings that are in child directories if explicitly allowed by a parent element of the current configuration element or group. Permission to override is specified by using the <see langword="OverrideMode" /> attribute.</summary>
		Inherit = 0
	}
}
namespace System.Configuration.Internal
{
	/// <summary>Defines the supplemental interface to <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> for configuration hosts that wish to support the application of <see cref="T:System.Configuration.ConfigurationBuilder" /> objects.</summary>
	[ComVisible(false)]
	public interface IInternalConfigurationBuilderHost
	{
		/// <summary>Processes a <see cref="T:System.Configuration.ConfigurationSection" /> object using the provided <see cref="T:System.Configuration.ConfigurationBuilder" />.</summary>
		/// <param name="configSection">The <see cref="T:System.Configuration.ConfigurationSection" /> to process.</param>
		/// <param name="builder">
		///   <see cref="T:System.Configuration.ConfigurationBuilder" /> to use to process the <paramref name="configSection" />.</param>
		/// <returns>The processed <see cref="T:System.Configuration.ConfigurationSection" />.</returns>
		ConfigurationSection ProcessConfigurationSection(ConfigurationSection configSection, ConfigurationBuilder builder);

		/// <summary>Processes the markup of a configuration section using the provided <see cref="T:System.Configuration.ConfigurationBuilder" />.</summary>
		/// <param name="rawXml">The <see cref="T:System.Xml.XmlNode" /> to process.</param>
		/// <param name="builder">
		///   <see cref="T:System.Configuration.ConfigurationBuilder" /> to use to process the <paramref name="rawXml" />.</param>
		/// <returns>The processed <see cref="T:System.Xml.XmlNode" />.</returns>
		XmlNode ProcessRawXml(XmlNode rawXml, ConfigurationBuilder builder);
	}
}
namespace System.Configuration
{
	/// <summary>Maintains a collection of <see cref="T:System.Configuration.ConfigurationBuilder" /> objects by name.</summary>
	[DefaultMember("Item")]
	public class ConfigurationBuilderCollection : ProviderCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationBuilderCollection" /> class.</summary>
		public ConfigurationBuilderCollection()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Represents a group of configuration elements that configure the providers for the <see langword="&lt;configBuilders&gt;" /> configuration section.</summary>
	public class ConfigurationBuilderSettings : ConfigurationElement
	{
		/// <summary>Gets a collection of <see cref="T:System.Configuration.ConfigurationBuilderSettings" /> objects that represent the properties of configuration builders.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationBuilder" /> objects.</returns>
		public ProviderSettingsCollection Builders
		{
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationBuilderSettings" /> class.</summary>
		public ConfigurationBuilderSettings()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Provides programmatic access to the <see langword="&lt;configBuilders&gt;" /> section. This class can't be inherited.</summary>
	public sealed class ConfigurationBuildersSection : ConfigurationSection
	{
		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationBuilderCollection" /> of all <see cref="T:System.Configuration.ConfigurationBuilder" /> objects in all participating configuration files.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationBuilder" /> objects in all participating configuration files.</returns>
		public ProviderSettingsCollection Builders
		{
			get
			{
				Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationBuildersSection" /> class.</summary>
		public ConfigurationBuildersSection()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a <see cref="T:System.Configuration.ConfigurationBuilder" /> object that has the provided configuration builder name.</summary>
		/// <param name="builderName">A configuration builder name or a comma-separated list of names. If <paramref name="builderName" /> is a comma-separated list of <see cref="T:System.Configuration.ConfigurationBuilder" /> names, a special aggregate <see cref="T:System.Configuration.ConfigurationBuilder" /> object that references and applies all named configuration builders is returned.</param>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationBuilder" /> object that has the provided configuration <paramref name="builderName" />.</returns>
		/// <exception cref="T:System.Exception">A configuration provider type can't be instantiated under a partially trusted security policy (<see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> is not present on the target assembly).</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">ConfigurationBuilders.IgnoreLoadFailure is disabled by default. If a bin-deployed configuration builder can't be found or instantiated for one of the sections read from the configuration file, a <see cref="T:System.IO.FileNotFoundException" /> is trapped and reported. If you wish to ignore load failures, enable ConfigurationBuilders.IgnoreLoadFailure.</exception>
		/// <exception cref="T:System.TypeLoadException">ConfigurationBuilders.IgnoreLoadFailure is disabled by default. While loading a configuration builder if a <see cref="T:System.TypeLoadException" /> occurs for one of the sections read from the configuration file, a <see cref="T:System.TypeLoadException" /> is trapped and reported. If you wish to ignore load failures, enable ConfigurationBuilders.IgnoreLoadFailure.</exception>
		public ConfigurationBuilder GetBuilderFromName(string builderName)
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
