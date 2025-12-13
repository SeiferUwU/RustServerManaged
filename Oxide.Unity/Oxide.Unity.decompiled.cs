using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using Oxide.Core.Extensions;
using Oxide.Core.Logging;
using Oxide.Core.Plugins;
using Oxide.Core.Unity.Logging;
using Oxide.Core.Unity.Plugins;
using UnityEngine;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyCompany("Oxide Team and Contributors")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("(c) 2013-2022 Oxide Team and Contributors")]
[assembly: AssemblyDescription("Unity Engine extension for the Oxide modding framework")]
[assembly: AssemblyFileVersion("2.0.3777.0")]
[assembly: AssemblyInformationalVersion("2.0.3777")]
[assembly: AssemblyProduct("Oxide.Unity")]
[assembly: AssemblyTitle("Oxide.Unity")]
[assembly: AssemblyVersion("2.0.3777.0")]
namespace Oxide.Core.Unity
{
	public static class ExtensionMethods
	{
		public static Vector3 ToVector3(this string vector3)
		{
			float[] array = vector3.Split(new char[1] { ',' }).Select(Convert.ToSingle).ToArray();
			if (array.Length != 3)
			{
				return Vector3.zero;
			}
			return new Vector3(array[0], array[1], array[2]);
		}

		public static Oxide.Core.Logging.LogType ToLogType(this UnityEngine.LogType logType)
		{
			switch (logType)
			{
			case UnityEngine.LogType.Error:
			case UnityEngine.LogType.Assert:
			case UnityEngine.LogType.Exception:
				return Oxide.Core.Logging.LogType.Error;
			case UnityEngine.LogType.Warning:
				return Oxide.Core.Logging.LogType.Warning;
			default:
				return Oxide.Core.Logging.LogType.Info;
			}
		}
	}
	public class UnityExtension : Extension
	{
		internal static Assembly Assembly = Assembly.GetExecutingAssembly();

		internal static AssemblyName AssemblyName = Assembly.GetName();

		internal static VersionNumber AssemblyVersion = new VersionNumber(AssemblyName.Version.Major, AssemblyName.Version.Minor, AssemblyName.Version.Build);

		internal static string AssemblyAuthors = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly, typeof(AssemblyCompanyAttribute), inherit: false)).Company;

		public override bool IsCoreExtension => true;

		public override string Name => "Unity";

		public override string Author => AssemblyAuthors;

		public override VersionNumber Version => AssemblyVersion;

		public UnityExtension(ExtensionManager manager)
			: base(manager)
		{
		}

		public override void Load()
		{
			Interface.Oxide.LogInfo("Unity version: " + Application.unityVersion);
			base.Manager.RegisterPluginLoader(new UnityPluginLoader());
			Interface.Oxide.RegisterEngineClock(() => UnityScript.RealtimeSinceStartup);
			UnityScript.Create();
		}

		public override void LoadPluginWatchers(string pluginDirectory)
		{
		}

		public override void OnModLoad()
		{
		}
	}
	public class UnityScript : MonoBehaviour
	{
		private OxideMod oxideMod;

		public static GameObject Instance { get; private set; }

		public static float RealtimeSinceStartup { get; private set; }

		public static void Create()
		{
			Instance = new GameObject("Oxide.Core.Unity");
			UnityEngine.Object.DontDestroyOnLoad(Instance);
			Instance.AddComponent<UnityScript>();
		}

		private void Awake()
		{
			RealtimeSinceStartup = Time.realtimeSinceStartup;
			oxideMod = Interface.Oxide;
			EventInfo eventInfo = typeof(Application).GetEvent("logMessageReceived");
			if ((object)eventInfo == null)
			{
				Application.LogCallback logCallback = typeof(Application).GetField("s_LogCallback", BindingFlags.Static | BindingFlags.NonPublic)?.GetValue(null) as Application.LogCallback;
				if (logCallback == null)
				{
					Interface.Oxide.LogWarning("No Unity application log callback is registered");
				}
				Application.RegisterLogCallback(delegate(string message, string stack_trace, UnityEngine.LogType type)
				{
					logCallback?.Invoke(message, stack_trace, type);
					LogMessageReceived(message, stack_trace, type);
				});
			}
			else
			{
				Delegate obj = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, "LogMessageReceived");
				eventInfo.GetAddMethod().Invoke(null, new object[1] { obj });
			}
		}

		private void Update()
		{
			RealtimeSinceStartup = Time.realtimeSinceStartup;
			oxideMod.OnFrame(Time.deltaTime);
		}

		private void OnDestroy()
		{
			if (!oxideMod.IsShuttingDown)
			{
				oxideMod.LogWarning("The Oxide Unity Script was destroyed (creating a new instance)");
				oxideMod.NextTick(Create);
			}
		}

		private void OnApplicationQuit()
		{
			if (!oxideMod.IsShuttingDown)
			{
				Interface.Call("OnServerShutdown");
				Interface.Oxide.OnShutdown();
			}
		}

		private void LogMessageReceived(string message, string stackTrace, UnityEngine.LogType type)
		{
			if (type == UnityEngine.LogType.Exception)
			{
				RemoteLogger.Exception(message, stackTrace);
			}
		}
	}
}
namespace Oxide.Core.Unity.Plugins
{
	public class UnityCore : CSPlugin
	{
		private UnityLogger logger;

		public UnityCore()
		{
			base.Title = "Unity";
			base.Author = UnityExtension.AssemblyAuthors;
			base.Version = UnityExtension.AssemblyVersion;
		}

		[HookMethod("InitLogging")]
		private void InitLogging()
		{
			Interface.Oxide.NextTick(delegate
			{
				logger = new UnityLogger();
				Interface.Oxide.RootLogger.AddLogger(logger);
				Interface.Oxide.RootLogger.DisableCache();
			});
		}

		public void Print(string message)
		{
			Debug.Log(message);
		}

		public void PrintWarning(string message)
		{
			Debug.LogWarning(message);
		}

		public void PrintError(string message)
		{
			Debug.LogError(message);
		}
	}
	public class UnityPluginLoader : PluginLoader
	{
		public override Type[] CorePlugins => new Type[1] { typeof(UnityCore) };
	}
}
namespace Oxide.Core.Unity.Logging
{
	public sealed class UnityLogger : Oxide.Core.Logging.Logger
	{
		private readonly Thread mainThread = Thread.CurrentThread;

		public UnityLogger()
			: base(processImmediately: true)
		{
		}

		protected override void ProcessMessage(LogMessage message)
		{
			if (Thread.CurrentThread != mainThread)
			{
				Interface.Oxide.NextTick(delegate
				{
					ProcessMessage(message);
				});
				return;
			}
			switch (message.Type)
			{
			case Oxide.Core.Logging.LogType.Error:
				Debug.LogError(message.ConsoleMessage);
				break;
			case Oxide.Core.Logging.LogType.Warning:
				Debug.LogWarning(message.ConsoleMessage);
				break;
			default:
				Debug.Log(message.ConsoleMessage);
				break;
			}
		}
	}
}
