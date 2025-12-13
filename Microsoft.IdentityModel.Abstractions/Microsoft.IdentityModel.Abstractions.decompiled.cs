using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyInformationalVersion("6.35.0.41201040912.c94c7fc235501d478221e979062bd8837a55575b")]
[assembly: AssemblyFileVersion("6.35.0.41201")]
[assembly: AssemblyMetadata("Serviceable", "True")]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = "")]
[assembly: AssemblyMetadata("IsTrimmable", "True")]
[assembly: AssemblyCompany("Microsoft Corporation.")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("© Microsoft Corporation. All rights reserved.")]
[assembly: AssemblyDescription("A package containing thin abstractions for Microsoft.IdentityModel.")]
[assembly: AssemblyProduct("Microsoft IdentityModel")]
[assembly: AssemblyTitle("Microsoft.IdentityModel.Abstractions")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet")]
[assembly: AssemblyVersion("6.35.0.0")]
namespace Microsoft.IdentityModel.Abstractions;

public enum EventLogLevel
{
	LogAlways,
	Critical,
	Error,
	Warning,
	Informational,
	Verbose
}
public interface IIdentityLogger
{
	bool IsEnabled(EventLogLevel eventLogLevel);

	void Log(LogEntry entry);
}
public interface ITelemetryClient
{
	string ClientId { get; set; }

	void Initialize();

	bool IsEnabled();

	bool IsEnabled(string eventName);

	void TrackEvent(TelemetryEventDetails eventDetails);

	void TrackEvent(string eventName, IDictionary<string, string> stringProperties = null, IDictionary<string, long> longProperties = null, IDictionary<string, bool> boolProperties = null, IDictionary<string, DateTime> dateTimeProperties = null, IDictionary<string, double> doubleProperties = null, IDictionary<string, Guid> guidProperties = null);
}
public class LogEntry
{
	public EventLogLevel EventLogLevel { get; set; }

	public string Message { get; set; }

	public string CorrelationId { get; set; }
}
public sealed class NullIdentityModelLogger : IIdentityLogger
{
	public static NullIdentityModelLogger Instance { get; } = new NullIdentityModelLogger();

	private NullIdentityModelLogger()
	{
	}

	public bool IsEnabled(EventLogLevel eventLogLevel)
	{
		return false;
	}

	public void Log(LogEntry entry)
	{
	}
}
public class NullTelemetryClient : ITelemetryClient
{
	public string ClientId { get; set; }

	public static NullTelemetryClient Instance { get; } = new NullTelemetryClient();

	private NullTelemetryClient()
	{
	}

	public bool IsEnabled()
	{
		return false;
	}

	public bool IsEnabled(string eventName)
	{
		return false;
	}

	public void Initialize()
	{
	}

	public void TrackEvent(TelemetryEventDetails eventDetails)
	{
	}

	public void TrackEvent(string eventName, IDictionary<string, string> stringProperties = null, IDictionary<string, long> longProperties = null, IDictionary<string, bool> boolProperties = null, IDictionary<string, DateTime> dateTimeProperties = null, IDictionary<string, double> doubleProperties = null, IDictionary<string, Guid> guidProperties = null)
	{
	}
}
public static class ObservabilityConstants
{
	public const string Succeeded = "Succeeded";

	public const string Duration = "Duration";

	public const string ActivityId = "ActivityId";

	public const string ClientId = "ClientId";
}
public abstract class TelemetryEventDetails
{
	protected internal IDictionary<string, object> PropertyValues { get; } = new Dictionary<string, object>();

	public virtual string Name { get; set; }

	public virtual IReadOnlyDictionary<string, object> Properties => (IReadOnlyDictionary<string, object>)PropertyValues;

	public virtual void SetProperty(string key, string value)
	{
		SetPropertyCore(key, value);
	}

	public virtual void SetProperty(string key, long value)
	{
		SetPropertyCore(key, value);
	}

	public virtual void SetProperty(string key, bool value)
	{
		SetPropertyCore(key, value);
	}

	public virtual void SetProperty(string key, DateTime value)
	{
		SetPropertyCore(key, value);
	}

	public virtual void SetProperty(string key, double value)
	{
		SetPropertyCore(key, value);
	}

	public virtual void SetProperty(string key, Guid value)
	{
		SetPropertyCore(key, value);
	}

	private void SetPropertyCore(string key, object value)
	{
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		PropertyValues[key] = value;
	}
}
