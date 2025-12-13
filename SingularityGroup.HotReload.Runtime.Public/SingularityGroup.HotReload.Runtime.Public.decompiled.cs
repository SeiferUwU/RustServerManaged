using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyVersion("0.0.0.0")]
[CompilerGenerated]
[EditorBrowsable(EditorBrowsableState.Never)]
[GeneratedCode("Unity.MonoScriptGenerator.MonoScriptInfoGenerator", null)]
internal class UnitySourceGeneratedAssemblyMonoScriptTypes_v1
{
	private struct MonoScriptData
	{
		public byte[] FilePathsData;

		public byte[] TypesData;

		public int TotalTypes;

		public int TotalFiles;

		public bool IsEditorOnly;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static MonoScriptData Get()
	{
		return new MonoScriptData
		{
			FilePathsData = new byte[326]
			{
				0, 0, 0, 1, 0, 0, 0, 75, 92, 80,
				97, 99, 107, 97, 103, 101, 115, 92, 99, 111,
				109, 46, 115, 105, 110, 103, 117, 108, 97, 114,
				105, 116, 121, 103, 114, 111, 117, 112, 46, 104,
				111, 116, 114, 101, 108, 111, 97, 100, 92, 82,
				117, 110, 116, 105, 109, 101, 92, 80, 117, 98,
				108, 105, 99, 92, 72, 111, 116, 82, 101, 108,
				111, 97, 100, 76, 111, 103, 103, 105, 110, 103,
				46, 99, 115, 0, 0, 0, 2, 0, 0, 0,
				76, 92, 80, 97, 99, 107, 97, 103, 101, 115,
				92, 99, 111, 109, 46, 115, 105, 110, 103, 117,
				108, 97, 114, 105, 116, 121, 103, 114, 111, 117,
				112, 46, 104, 111, 116, 114, 101, 108, 111, 97,
				100, 92, 82, 117, 110, 116, 105, 109, 101, 92,
				80, 117, 98, 108, 105, 99, 92, 73, 110, 118,
				111, 107, 101, 79, 110, 72, 111, 116, 82, 101,
				108, 111, 97, 100, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 81, 92, 80, 97, 99, 107,
				97, 103, 101, 115, 92, 99, 111, 109, 46, 115,
				105, 110, 103, 117, 108, 97, 114, 105, 116, 121,
				103, 114, 111, 117, 112, 46, 104, 111, 116, 114,
				101, 108, 111, 97, 100, 92, 82, 117, 110, 116,
				105, 109, 101, 92, 80, 117, 98, 108, 105, 99,
				92, 73, 110, 118, 111, 107, 101, 79, 110, 72,
				111, 116, 82, 101, 108, 111, 97, 100, 76, 111,
				99, 97, 108, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 62, 92, 80, 97, 99, 107, 97,
				103, 101, 115, 92, 99, 111, 109, 46, 115, 105,
				110, 103, 117, 108, 97, 114, 105, 116, 121, 103,
				114, 111, 117, 112, 46, 104, 111, 116, 114, 101,
				108, 111, 97, 100, 92, 82, 117, 110, 116, 105,
				109, 101, 92, 80, 117, 98, 108, 105, 99, 92,
				76, 111, 103, 46, 99, 115
			},
			TypesData = new byte[229]
			{
				0, 0, 0, 0, 43, 83, 105, 110, 103, 117,
				108, 97, 114, 105, 116, 121, 71, 114, 111, 117,
				112, 46, 72, 111, 116, 82, 101, 108, 111, 97,
				100, 124, 72, 111, 116, 82, 101, 108, 111, 97,
				100, 76, 111, 103, 103, 105, 110, 103, 0, 0,
				0, 0, 44, 83, 105, 110, 103, 117, 108, 97,
				114, 105, 116, 121, 71, 114, 111, 117, 112, 46,
				72, 111, 116, 82, 101, 108, 111, 97, 100, 124,
				73, 110, 118, 111, 107, 101, 79, 110, 72, 111,
				116, 82, 101, 108, 111, 97, 100, 0, 0, 0,
				0, 38, 83, 105, 110, 103, 117, 108, 97, 114,
				105, 116, 121, 71, 114, 111, 117, 112, 46, 72,
				111, 116, 82, 101, 108, 111, 97, 100, 124, 77,
				101, 116, 104, 111, 100, 80, 97, 116, 99, 104,
				0, 0, 0, 0, 49, 83, 105, 110, 103, 117,
				108, 97, 114, 105, 116, 121, 71, 114, 111, 117,
				112, 46, 72, 111, 116, 82, 101, 108, 111, 97,
				100, 124, 73, 110, 118, 111, 107, 101, 79, 110,
				72, 111, 116, 82, 101, 108, 111, 97, 100, 76,
				111, 99, 97, 108, 0, 0, 0, 0, 30, 83,
				105, 110, 103, 117, 108, 97, 114, 105, 116, 121,
				71, 114, 111, 117, 112, 46, 72, 111, 116, 82,
				101, 108, 111, 97, 100, 124, 76, 111, 103
			},
			TotalFiles = 4,
			TotalTypes = 5,
			IsEditorOnly = false
		};
	}
}
namespace SingularityGroup.HotReload;

public static class HotReloadLogging
{
	public static void SetLogLevel(LogLevel level)
	{
		Log.minLevel = level;
	}
}
[AttributeUsage(AttributeTargets.Method)]
public class InvokeOnHotReload : Attribute
{
}
public class MethodPatch
{
	public MethodBase originalMethod;

	public MethodBase previousMethod;

	public MethodBase newMethod;

	public MethodPatch(MethodBase originalMethod, MethodBase previousMethod, MethodBase newMethod)
	{
		this.originalMethod = originalMethod;
		this.previousMethod = previousMethod;
		this.newMethod = newMethod;
	}
}
[AttributeUsage(AttributeTargets.Method)]
public class InvokeOnHotReloadLocal : Attribute
{
	public readonly string methodToInvoke;

	public InvokeOnHotReloadLocal(string methodToInvoke = null)
	{
		this.methodToInvoke = methodToInvoke;
	}
}
public static class Log
{
	public static LogLevel minLevel = LogLevel.Info;

	private const string TAG = "[HotReload] ";

	public static void Debug(string message)
	{
		if (minLevel <= LogLevel.Debug)
		{
			UnityEngine.Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "{0}{1}", "[HotReload] ", message);
		}
	}

	[StringFormatMethod("message")]
	public static void Debug(string message, params object[] args)
	{
		if (minLevel <= LogLevel.Debug)
		{
			UnityEngine.Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "[HotReload] " + message, args);
		}
	}

	public static void Info(string message)
	{
		if (minLevel <= LogLevel.Info)
		{
			UnityEngine.Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "{0}{1}", "[HotReload] ", message);
		}
	}

	[StringFormatMethod("message")]
	public static void Info(string message, params object[] args)
	{
		if (minLevel <= LogLevel.Info)
		{
			UnityEngine.Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, "[HotReload] " + message, args);
		}
	}

	public static void Warning(string message)
	{
		if (minLevel <= LogLevel.Warning)
		{
			UnityEngine.Debug.LogFormat(LogType.Warning, LogOption.NoStacktrace, null, "{0}{1}", "[HotReload] ", message);
		}
	}

	[StringFormatMethod("message")]
	public static void Warning(string message, params object[] args)
	{
		if (minLevel <= LogLevel.Warning)
		{
			UnityEngine.Debug.LogFormat(LogType.Warning, LogOption.NoStacktrace, null, "[HotReload] " + message, args);
		}
	}

	public static void Error(string message)
	{
		if (minLevel <= LogLevel.Error)
		{
			UnityEngine.Debug.LogFormat(LogType.Error, LogOption.NoStacktrace, null, "{0}{1}", "[HotReload] ", message);
		}
	}

	[StringFormatMethod("message")]
	public static void Error(string message, params object[] args)
	{
		if (minLevel <= LogLevel.Error)
		{
			UnityEngine.Debug.LogFormat(LogType.Error, LogOption.NoStacktrace, null, "[HotReload] " + message, args);
		}
	}

	public static void Exception(Exception exception)
	{
		if (minLevel <= LogLevel.Exception)
		{
			UnityEngine.Debug.LogException(exception);
		}
	}
}
public enum LogLevel
{
	Debug = 1,
	Info,
	Warning,
	Error,
	Exception,
	Disabled
}
