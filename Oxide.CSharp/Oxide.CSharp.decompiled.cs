using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Mono.Collections.Generic;
using Mono.Unix.Native;
using ObjectStream;
using ObjectStream.Data;
using ObjectStream.IO;
using ObjectStream.Threading;
using Oxide.CSharp;
using Oxide.CSharp.Patching;
using Oxide.CSharp.Patching.Validation;
using Oxide.Core;
using Oxide.Core.CSharp;
using Oxide.Core.Extensions;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Logging;
using Oxide.Core.Plugins;
using Oxide.Core.Plugins.Watchers;
using Oxide.Logging;
using Oxide.Plugins;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: TargetFramework(".NETFramework,Version=v4.8", FrameworkDisplayName = ".NET Framework 4.8")]
[assembly: AssemblyCompany("Oxide Team and Contributors")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("(c) 2013-2025 Oxide Team and Contributors")]
[assembly: AssemblyDescription("C#/CSharp (.cs) plugin support for the Oxide modding framework")]
[assembly: AssemblyFileVersion("2.0.4149.0")]
[assembly: AssemblyInformationalVersion("2.0.4149")]
[assembly: AssemblyProduct("Oxide.CSharp")]
[assembly: AssemblyTitle("Oxide.CSharp")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/OxideMod/Oxide.CSharp")]
[assembly: AssemblyMetadata("GitBranch", "master")]
[assembly: AssemblyVersion("2.0.4149.0")]
namespace ObjectStream
{
	public class ObjectStreamClient<TReadWrite> : ObjectStreamClient<TReadWrite, TReadWrite> where TReadWrite : class
	{
		public ObjectStreamClient(Stream inStream, Stream outStream)
			: base(inStream, outStream)
		{
		}
	}
	public delegate void StreamExceptionEventHandler(Exception exception);
	public class ObjectStreamClient<TRead, TWrite> where TRead : class where TWrite : class
	{
		private readonly Stream _inStream;

		private readonly Stream _outStream;

		private ObjectStreamConnection<TRead, TWrite> _connection;

		public event ConnectionMessageEventHandler<TRead, TWrite> Message;

		public event StreamExceptionEventHandler Error;

		public ObjectStreamClient(Stream inStream, Stream outStream)
		{
			_inStream = inStream;
			_outStream = outStream;
		}

		public void Start()
		{
			Worker worker = new Worker();
			worker.Error += OnError;
			worker.DoWork(ListenSync);
		}

		public void PushMessage(TWrite message)
		{
			if (_connection != null)
			{
				_connection.PushMessage(message);
			}
		}

		public void Stop()
		{
			if (_connection != null)
			{
				_connection.Close();
			}
		}

		private void ListenSync()
		{
			_connection = ConnectionFactory.CreateConnection<TRead, TWrite>(_inStream, _outStream);
			_connection.ReceiveMessage += OnReceiveMessage;
			_connection.Error += ConnectionOnError;
			_connection.Open();
		}

		private void OnReceiveMessage(ObjectStreamConnection<TRead, TWrite> connection, TRead message)
		{
			if (this.Message != null)
			{
				this.Message(connection, message);
			}
		}

		private void ConnectionOnError(ObjectStreamConnection<TRead, TWrite> connection, Exception exception)
		{
			OnError(exception);
		}

		private void OnError(Exception exception)
		{
			if (this.Error != null)
			{
				this.Error(exception);
			}
		}
	}
	internal static class ObjectStreamClientFactory
	{
		public static ObjectStreamWrapper<TRead, TWrite> Connect<TRead, TWrite>(Stream inStream, Stream outStream) where TRead : class where TWrite : class
		{
			return new ObjectStreamWrapper<TRead, TWrite>(inStream, outStream);
		}
	}
	public class ObjectStreamConnection<TRead, TWrite> where TRead : class where TWrite : class
	{
		private readonly ObjectStreamWrapper<TRead, TWrite> _streamWrapper;

		private readonly Queue<TWrite> _writeQueue = new Queue<TWrite>();

		private readonly AutoResetEvent _writeSignal = new AutoResetEvent(initialState: false);

		public event ConnectionMessageEventHandler<TRead, TWrite> ReceiveMessage;

		public event ConnectionExceptionEventHandler<TRead, TWrite> Error;

		internal ObjectStreamConnection(Stream inStream, Stream outStream)
		{
			_streamWrapper = new ObjectStreamWrapper<TRead, TWrite>(inStream, outStream);
		}

		public void Open()
		{
			Worker worker = new Worker();
			worker.Error += OnError;
			worker.DoWork(ReadStream);
			Worker worker2 = new Worker();
			worker2.Error += OnError;
			worker2.DoWork(WriteStream);
		}

		public void PushMessage(TWrite message)
		{
			_writeQueue.Enqueue(message);
			_writeSignal.Set();
		}

		public void Close()
		{
			CloseImpl();
		}

		private void CloseImpl()
		{
			this.Error = null;
			_streamWrapper.Close();
			_writeSignal.Set();
		}

		private void OnError(Exception exception)
		{
			if (this.Error != null)
			{
				this.Error(this, exception);
			}
		}

		private void ReadStream()
		{
			while (_streamWrapper.CanRead)
			{
				TRead val = _streamWrapper.ReadObject();
				this.ReceiveMessage?.Invoke(this, val);
				if (val == null)
				{
					CloseImpl();
					break;
				}
			}
		}

		private void WriteStream()
		{
			while (_streamWrapper.CanWrite)
			{
				_writeSignal.WaitOne();
				while (_writeQueue.Count > 0)
				{
					_streamWrapper.WriteObject(_writeQueue.Dequeue());
				}
			}
		}
	}
	internal static class ConnectionFactory
	{
		public static ObjectStreamConnection<TRead, TWrite> CreateConnection<TRead, TWrite>(Stream inStream, Stream outStream) where TRead : class where TWrite : class
		{
			return new ObjectStreamConnection<TRead, TWrite>(inStream, outStream);
		}
	}
	public delegate void ConnectionMessageEventHandler<TRead, TWrite>(ObjectStreamConnection<TRead, TWrite> connection, TRead message) where TRead : class where TWrite : class;
	public delegate void ConnectionExceptionEventHandler<TRead, TWrite>(ObjectStreamConnection<TRead, TWrite> connection, Exception exception) where TRead : class where TWrite : class;
}
namespace ObjectStream.Threading
{
	internal class Worker
	{
		public event WorkerExceptionEventHandler Error;

		public void DoWork(Action action)
		{
			Thread thread = new Thread(DoWorkImpl);
			thread.IsBackground = true;
			thread.Start(action);
		}

		private void DoWorkImpl(object oAction)
		{
			Action action = (Action)oAction;
			try
			{
				action();
			}
			catch (Exception ex)
			{
				Exception e = ex;
				Callback(delegate
				{
					Fail(e);
				});
			}
		}

		private void Fail(Exception exception)
		{
			if (this.Error != null)
			{
				this.Error(exception);
			}
		}

		private void Callback(Action action)
		{
			Thread thread = new Thread(action.Invoke);
			thread.IsBackground = true;
			thread.Start();
		}
	}
	internal delegate void WorkerSucceededEventHandler();
	internal delegate void WorkerExceptionEventHandler(Exception exception);
}
namespace ObjectStream.IO
{
	public class BindChanger : SerializationBinder
	{
		public override Type BindToType(string assemblyName, string typeName)
		{
			return Type.GetType($"{typeName}, {Assembly.GetExecutingAssembly().FullName}");
		}
	}
	public class ObjectStreamWrapper<TReadWrite> : ObjectStreamWrapper<TReadWrite, TReadWrite> where TReadWrite : class
	{
		public ObjectStreamWrapper(Stream inStream, Stream outStream)
			: base(inStream, outStream)
		{
		}
	}
	public class ObjectStreamWrapper<TRead, TWrite> where TRead : class where TWrite : class
	{
		private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter
		{
			Binder = new BindChanger(),
			AssemblyFormat = FormatterAssemblyStyle.Simple
		};

		private readonly Stream _inStream;

		private readonly Stream _outStream;

		private bool _run;

		public bool CanRead
		{
			get
			{
				if (_run)
				{
					return _inStream.CanRead;
				}
				return false;
			}
		}

		public bool CanWrite
		{
			get
			{
				if (_run)
				{
					return _outStream.CanWrite;
				}
				return false;
			}
		}

		public ObjectStreamWrapper(Stream inStream, Stream outStream)
		{
			_inStream = inStream;
			_outStream = outStream;
			_run = true;
		}

		public void Close()
		{
			if (!_run)
			{
				return;
			}
			_run = false;
			try
			{
				_outStream.Close();
			}
			catch (Exception)
			{
			}
			try
			{
				_inStream.Close();
			}
			catch (Exception)
			{
			}
		}

		public TRead ReadObject()
		{
			int num = ReadLength();
			if (num != 0)
			{
				return ReadObject(num);
			}
			return null;
		}

		private int ReadLength()
		{
			byte[] array = new byte[4];
			int num = _inStream.Read(array, 0, 4);
			switch (num)
			{
			case 0:
				return 0;
			default:
				Array.Resize(ref array, Encoding.UTF8.GetPreamble().Length);
				if (Encoding.UTF8.GetPreamble().SequenceEqual(array))
				{
					return ReadLength();
				}
				throw new IOException($"Expected {4} bytes but read {num}");
			case 4:
				return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(array, 0));
			}
		}

		private TRead ReadObject(int len)
		{
			byte[] buffer = new byte[len];
			int num;
			for (int i = 0; len - i > 0; i += num)
			{
				if ((num = _inStream.Read(buffer, i, len - i)) <= 0)
				{
					break;
				}
			}
			using MemoryStream serializationStream = new MemoryStream(buffer);
			return (TRead)_binaryFormatter.Deserialize(serializationStream);
		}

		public void WriteObject(TWrite obj)
		{
			byte[] array = Serialize(obj);
			WriteLength(array.Length);
			WriteObject(array);
			Flush();
		}

		private byte[] Serialize(TWrite obj)
		{
			using MemoryStream memoryStream = new MemoryStream();
			_binaryFormatter.Serialize(memoryStream, obj);
			return memoryStream.ToArray();
		}

		private void WriteLength(int len)
		{
			byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(len));
			_outStream.Write(bytes, 0, bytes.Length);
		}

		private void WriteObject(byte[] data)
		{
			_outStream.Write(data, 0, data.Length);
		}

		private void Flush()
		{
			_outStream.Flush();
		}
	}
}
namespace ObjectStream.Data
{
	[Serializable]
	public class CompilationResult
	{
		public string Name { get; set; }

		public byte[] Data { get; set; }

		public byte[] Symbols { get; set; }

		public CompilationResult()
		{
			Data = new byte[0];
			Symbols = new byte[0];
		}
	}
	[Serializable]
	public class CompilerData
	{
		public bool LoadDefaultReferences { get; set; }

		public string OutputFile { get; set; }

		public CompilerPlatform Platform { get; set; }

		public CompilerFile[] ReferenceFiles { get; set; }

		public string SdkVersion { get; set; }

		public CompilerFile[] SourceFiles { get; set; }

		public bool StdLib { get; set; }

		public CompilerTarget Target { get; set; }

		public CompilerLanguageVersion Version { get; set; }

		public string Encoding { get; set; }

		public bool Debug { get; set; }

		public string[] Preprocessor { get; set; }

		public CompilerData()
		{
			StdLib = false;
			Target = CompilerTarget.Library;
			Platform = CompilerPlatform.AnyCPU;
			Version = CompilerLanguageVersion.Preview;
			LoadDefaultReferences = false;
			SdkVersion = "2";
			Encoding = System.Text.Encoding.UTF8.WebName;
			Debug = false;
		}
	}
	[Serializable]
	public class CompilerFile
	{
		[NonSerialized]
		internal static readonly Dictionary<string, CompilerFile> FileCache = new Dictionary<string, CompilerFile>(StringComparer.InvariantCultureIgnoreCase);

		[NonSerialized]
		internal DateTime LastRead;

		[NonSerialized]
		internal bool KeepCached;

		public string Name { get; set; }

		public byte[] Data { get; set; }

		public static CompilerFile CachedReadFile(string directory, string fileName, byte[] data = null)
		{
			string text = Path.Combine(directory, fileName);
			CompilerFile value;
			lock (FileCache)
			{
				if (FileCache.TryGetValue(text, out value))
				{
					if (data != null)
					{
						value.Data = data;
					}
					value.LastRead = DateTime.Now;
					return value;
				}
			}
			bool patched = false;
			if (data == null && File.Exists(text))
			{
				data = Patcher.Run(File.ReadAllBytes(text), out patched);
			}
			if (data == null)
			{
				return null;
			}
			value = new CompilerFile(fileName, data);
			value.LastRead = DateTime.Now;
			value.KeepCached = patched;
			lock (FileCache)
			{
				FileCache[text] = value;
				return value;
			}
		}

		internal CompilerFile(string name, byte[] data)
		{
			Name = name;
			Data = data;
		}

		internal CompilerFile(string directory, string name)
		{
			Name = name;
			Data = File.ReadAllBytes(Path.Combine(directory, Name));
		}

		internal CompilerFile(string path)
		{
			Name = Path.GetFileName(path);
			Data = File.ReadAllBytes(path);
		}
	}
	[Serializable]
	public enum CompilerLanguageVersion
	{
		Latest = 500,
		V16 = 16,
		V15 = 15,
		V14 = 14,
		V13 = 13,
		V12 = 12,
		V11 = 11,
		V10 = 10,
		V9 = 9,
		V8 = 8,
		V7 = 7,
		V6 = 6,
		V5 = 5,
		V4 = 4,
		V3 = 3,
		V2 = 2,
		V1 = 1,
		Preview = 1000
	}
	[Serializable]
	public class CompilerMessage
	{
		public object Data { get; set; }

		public object ExtraData { get; set; }

		public int Id { get; set; }

		public CompilerMessageType Type { get; set; }
	}
	[Serializable]
	public enum CompilerMessageType
	{
		Assembly,
		Compile,
		Error,
		Exit,
		Ready
	}
	[Serializable]
	public enum CompilerPlatform
	{
		AnyCPU,
		AnyCPU32Preferred,
		Arm,
		X86,
		X64,
		IA64
	}
	[Serializable]
	public enum CompilerTarget
	{
		Library,
		Exe,
		Module,
		WinExe
	}
}
namespace Oxide
{
	public static class ExtensionMethods
	{
		public static void WriteDebug(this Logger logger, LogType level, LogEvent? @event, string source, string message, Exception exception = null)
		{
		}
	}
}
namespace Oxide.Logging
{
	public struct LogEvent
	{
		public int Id { get; }

		public string Name { get; }

		public static LogEvent Compile { get; } = new LogEvent(4, "Compile");

		public static LogEvent HookCall { get; } = new LogEvent(10, "ExecuteHook");

		public static LogEvent Patch { get; } = new LogEvent(23, "Patching");

		internal LogEvent(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}
namespace Oxide.Core.CSharp
{
	public class DirectCallMethod
	{
		public class Node
		{
			public char Char;

			public string Name;

			public Dictionary<char, Node> Edges = new Dictionary<char, Node>();

			public Node Parent;

			public Instruction FirstInstruction;
		}

		private ModuleDefinition module;

		private TypeDefinition type;

		private MethodDefinition method;

		private Mono.Cecil.Cil.MethodBody body;

		private Instruction endInstruction;

		private Dictionary<Instruction, Node> jumpToEdgePlaceholderTargets = new Dictionary<Instruction, Node>();

		private List<Instruction> jumpToEndPlaceholders = new List<Instruction>();

		private Dictionary<string, MethodDefinition> hookMethods = new Dictionary<string, MethodDefinition>();

		private MethodReference getLength;

		private MethodReference getChars;

		private MethodReference isNullOrEmpty;

		private MethodReference stringEquals;

		private string hook_attribute = typeof(HookMethodAttribute).FullName;

		public DirectCallMethod(ModuleDefinition module, TypeDefinition type, ReaderParameters readerParameters)
		{
			DirectCallMethod directCallMethod = this;
			this.module = module;
			this.type = type;
			getLength = module.Import(typeof(string).GetMethod("get_Length", new Type[0]));
			getChars = module.Import(typeof(string).GetMethod("get_Chars", new Type[1] { typeof(int) }));
			isNullOrEmpty = module.Import(typeof(string).GetMethod("IsNullOrEmpty", new Type[1] { typeof(string) }));
			stringEquals = module.Import(typeof(string).GetMethod("Equals", new Type[1] { typeof(string) }));
			AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(Path.Combine(Interface.Oxide.ExtensionDirectory, "Oxide.CSharp.dll"), readerParameters);
			ModuleDefinition mainModule = assemblyDefinition.MainModule;
			TypeDefinition typeDefinition = module.Import(assemblyDefinition.MainModule.GetType("Oxide.Plugins.CSharpPlugin")).Resolve();
			MethodDefinition methodDefinition = module.Import(typeDefinition.Methods.First((MethodDefinition method) => method.Name == "DirectCallHook")).Resolve();
			method = new MethodDefinition(methodDefinition.Name, methodDefinition.Attributes, mainModule.Import(methodDefinition.ReturnType))
			{
				DeclaringType = type
			};
			foreach (ParameterDefinition parameter in methodDefinition.Parameters)
			{
				ParameterDefinition parameterDefinition = new ParameterDefinition(parameter.Name, parameter.Attributes, module.Import(parameter.ParameterType))
				{
					IsOut = parameter.IsOut,
					Constant = parameter.Constant,
					MarshalInfo = parameter.MarshalInfo,
					IsReturnValue = parameter.IsReturnValue
				};
				foreach (CustomAttribute customAttribute in parameter.CustomAttributes)
				{
					parameterDefinition.CustomAttributes.Add(new CustomAttribute(module.Import(customAttribute.Constructor)));
				}
				method.Parameters.Add(parameterDefinition);
			}
			foreach (CustomAttribute customAttribute2 in methodDefinition.CustomAttributes)
			{
				method.CustomAttributes.Add(new CustomAttribute(module.Import(customAttribute2.Constructor)));
			}
			method.ImplAttributes = methodDefinition.ImplAttributes;
			method.SemanticsAttributes = methodDefinition.SemanticsAttributes;
			method.Attributes &= ~Mono.Cecil.MethodAttributes.VtableLayoutMask;
			method.Attributes |= Mono.Cecil.MethodAttributes.CompilerControlled;
			body = new Mono.Cecil.Cil.MethodBody(method);
			body.SimplifyMacros();
			method.Body = body;
			type.Methods.Add(method);
			body.Variables.Add(new VariableDefinition("name_size", module.TypeSystem.Int32));
			body.Variables.Add(new VariableDefinition("i", module.TypeSystem.Int32));
			AddInstruction(OpCodes.Ldarg_2);
			AddInstruction(OpCodes.Ldnull);
			AddInstruction(OpCodes.Stind_Ref);
			AddInstruction(OpCodes.Ldarg_1);
			AddInstruction(OpCodes.Call, isNullOrEmpty);
			Instruction instruction = AddInstruction(OpCodes.Brfalse, body.Instructions[0]);
			Return(value: false);
			instruction.Operand = AddInstruction(OpCodes.Ldarg_1);
			AddInstruction(OpCodes.Callvirt, getLength);
			AddInstruction(OpCodes.Stloc_0);
			AddInstruction(OpCodes.Ldc_I4_0);
			AddInstruction(OpCodes.Stloc_1);
			foreach (MethodDefinition item in type.Methods.Where((MethodDefinition m) => !m.IsStatic && (m.IsPrivate || directCallMethod.IsHookMethod(m)) && !m.HasGenericParameters && !m.ReturnType.IsGenericParameter && m.DeclaringType == type && !m.IsSetter && !m.IsGetter))
			{
				if (item.Name.Contains("<"))
				{
					continue;
				}
				string text = item.Name;
				if (item.Parameters.Count > 0)
				{
					text = text + "(" + string.Join(", ", item.Parameters.Select((ParameterDefinition x) => x.ParameterType.ToString().Replace("/", "+").Replace("<", "[")
						.Replace(">", "]")).ToArray()) + ")";
				}
				if (!hookMethods.ContainsKey(text))
				{
					hookMethods[text] = item;
				}
			}
			Node node = new Node();
			foreach (string key in hookMethods.Keys)
			{
				Node node2 = node;
				for (int num = 1; num <= key.Length; num++)
				{
					char c = key[num - 1];
					if (!node2.Edges.TryGetValue(c, out var value))
					{
						value = new Node
						{
							Parent = node2,
							Char = c
						};
						node2.Edges[c] = value;
					}
					if (num == key.Length)
					{
						value.Name = key;
					}
					node2 = value;
				}
			}
			int num2 = 1;
			foreach (char key2 in node.Edges.Keys)
			{
				BuildNode(node.Edges[key2], num2++);
			}
			endInstruction = Return(value: false);
			foreach (Instruction key3 in jumpToEdgePlaceholderTargets.Keys)
			{
				key3.Operand = jumpToEdgePlaceholderTargets[key3].FirstInstruction;
			}
			foreach (Instruction jumpToEndPlaceholder in jumpToEndPlaceholders)
			{
				jumpToEndPlaceholder.Operand = endInstruction;
			}
			body.OptimizeMacros();
		}

		private bool IsHookMethod(MethodDefinition method)
		{
			foreach (CustomAttribute customAttribute in method.CustomAttributes)
			{
				if (customAttribute.AttributeType.FullName == hook_attribute)
				{
					return true;
				}
			}
			return false;
		}

		private void BuildNode(Node node, int edge_number)
		{
			if (edge_number == 1)
			{
				node.FirstInstruction = AddInstruction(OpCodes.Ldloc_1);
				AddInstruction(OpCodes.Ldloc_0);
				jumpToEndPlaceholders.Add(AddInstruction(OpCodes.Bge, body.Instructions[0]));
			}
			if (edge_number == 1)
			{
				AddInstruction(OpCodes.Ldarg_1);
			}
			else
			{
				node.FirstInstruction = AddInstruction(OpCodes.Ldarg_1);
			}
			AddInstruction(OpCodes.Ldloc_1);
			AddInstruction(OpCodes.Callvirt, getChars);
			AddInstruction(Ldc_I4_n(node.Char));
			if (node.Parent.Edges.Count > edge_number)
			{
				JumpToEdge(node.Parent.Edges.Values.ElementAt(edge_number));
			}
			else
			{
				JumpToEnd();
			}
			if (node.Edges.Count == 1 && node.Name == null)
			{
				Node node2 = node;
				while (node2.Edges.Count == 1 && node2.Name == null)
				{
					node2 = node2.Edges.Values.First();
				}
				if (node2.Edges.Count == 0 && node2.Name != null)
				{
					AddInstruction(OpCodes.Ldarg_1);
					AddInstruction(Instruction.Create(OpCodes.Ldstr, node2.Name));
					AddInstruction(OpCodes.Callvirt, stringEquals);
					jumpToEndPlaceholders.Add(AddInstruction(OpCodes.Brfalse, body.Instructions[0]));
					CallMethod(hookMethods[node2.Name]);
					Return(value: true);
					return;
				}
			}
			AddInstruction(OpCodes.Ldloc_1);
			AddInstruction(OpCodes.Ldc_I4_1);
			AddInstruction(OpCodes.Add);
			AddInstruction(OpCodes.Stloc_1);
			if (node.Name != null)
			{
				AddInstruction(OpCodes.Ldloc_1);
				AddInstruction(OpCodes.Ldloc_0);
				if (node.Edges.Count > 0)
				{
					JumpToEdge(node.Edges.Values.First());
				}
				else
				{
					JumpToEnd();
				}
				CallMethod(hookMethods[node.Name]);
				Return(value: true);
			}
			int num = 1;
			foreach (char key in node.Edges.Keys)
			{
				BuildNode(node.Edges[key], num++);
			}
		}

		private void CallMethod(MethodDefinition method)
		{
			Dictionary<ParameterDefinition, VariableDefinition> dictionary = new Dictionary<ParameterDefinition, VariableDefinition>();
			for (int i = 0; i < method.Parameters.Count; i++)
			{
				ParameterDefinition parameterDefinition = method.Parameters[i];
				if (parameterDefinition.ParameterType is ByReferenceType byReferenceType)
				{
					VariableDefinition value = AddVariable(module.Import(byReferenceType.ElementType));
					AddInstruction(OpCodes.Ldarg_3);
					AddInstruction(Ldc_I4_n(i));
					AddInstruction(OpCodes.Ldelem_Ref);
					AddInstruction(OpCodes.Unbox_Any, module.Import(byReferenceType.ElementType));
					AddInstruction(OpCodes.Stloc_S, value);
					dictionary[parameterDefinition] = value;
				}
			}
			if (method.ReturnType.Name != "Void")
			{
				AddInstruction(OpCodes.Ldarg_2);
			}
			AddInstruction(OpCodes.Ldarg_0);
			for (int j = 0; j < method.Parameters.Count; j++)
			{
				ParameterDefinition parameterDefinition2 = method.Parameters[j];
				if (parameterDefinition2.ParameterType is ByReferenceType)
				{
					AddInstruction(OpCodes.Ldloca, dictionary[parameterDefinition2]);
					continue;
				}
				AddInstruction(OpCodes.Ldarg_3);
				AddInstruction(Ldc_I4_n(j));
				AddInstruction(OpCodes.Ldelem_Ref);
				AddInstruction(OpCodes.Unbox_Any, module.Import(parameterDefinition2.ParameterType));
			}
			AddInstruction(OpCodes.Call, module.Import(method));
			for (int k = 0; k < method.Parameters.Count; k++)
			{
				ParameterDefinition parameterDefinition3 = method.Parameters[k];
				if (parameterDefinition3.ParameterType is ByReferenceType byReferenceType2)
				{
					AddInstruction(OpCodes.Ldarg_3);
					AddInstruction(Ldc_I4_n(k));
					AddInstruction(OpCodes.Ldloc_S, dictionary[parameterDefinition3]);
					AddInstruction(OpCodes.Box, module.Import(byReferenceType2.ElementType));
					AddInstruction(OpCodes.Stelem_Ref);
				}
			}
			if (method.ReturnType.Name != "Void")
			{
				if (method.ReturnType.Name != "Object")
				{
					AddInstruction(OpCodes.Box, module.Import(method.ReturnType));
				}
				AddInstruction(OpCodes.Stind_Ref);
			}
		}

		private Instruction Return(bool value)
		{
			Instruction result = AddInstruction(Ldc_I4_n(value ? 1 : 0));
			AddInstruction(OpCodes.Ret);
			return result;
		}

		private void JumpToEdge(Node node)
		{
			Instruction key = AddInstruction(OpCodes.Bne_Un, body.Instructions[1]);
			jumpToEdgePlaceholderTargets[key] = node;
		}

		private void JumpToEnd()
		{
			jumpToEndPlaceholders.Add(AddInstruction(OpCodes.Bne_Un, body.Instructions[0]));
		}

		private Instruction AddInstruction(OpCode opcode)
		{
			return AddInstruction(Instruction.Create(opcode));
		}

		private Instruction AddInstruction(OpCode opcode, Instruction instruction)
		{
			return AddInstruction(Instruction.Create(opcode, instruction));
		}

		private Instruction AddInstruction(OpCode opcode, MethodReference method_reference)
		{
			return AddInstruction(Instruction.Create(opcode, method_reference));
		}

		private Instruction AddInstruction(OpCode opcode, TypeReference type_reference)
		{
			return AddInstruction(Instruction.Create(opcode, type_reference));
		}

		private Instruction AddInstruction(OpCode opcode, int value)
		{
			return AddInstruction(Instruction.Create(opcode, value));
		}

		private Instruction AddInstruction(OpCode opcode, VariableDefinition value)
		{
			return AddInstruction(Instruction.Create(opcode, value));
		}

		private Instruction AddInstruction(Instruction instruction)
		{
			body.Instructions.Add(instruction);
			return instruction;
		}

		public VariableDefinition AddVariable(TypeReference typeRef, string name = "")
		{
			VariableDefinition variableDefinition = new VariableDefinition(name, typeRef);
			body.Variables.Add(variableDefinition);
			return variableDefinition;
		}

		private Instruction Ldc_I4_n(int n)
		{
			return n switch
			{
				0 => Instruction.Create(OpCodes.Ldc_I4_0), 
				1 => Instruction.Create(OpCodes.Ldc_I4_1), 
				2 => Instruction.Create(OpCodes.Ldc_I4_2), 
				3 => Instruction.Create(OpCodes.Ldc_I4_3), 
				4 => Instruction.Create(OpCodes.Ldc_I4_4), 
				5 => Instruction.Create(OpCodes.Ldc_I4_5), 
				6 => Instruction.Create(OpCodes.Ldc_I4_6), 
				7 => Instruction.Create(OpCodes.Ldc_I4_7), 
				8 => Instruction.Create(OpCodes.Ldc_I4_8), 
				_ => Instruction.Create(OpCodes.Ldc_I4_S, (sbyte)n), 
			};
		}
	}
}
namespace Oxide.Plugins
{
	public class CompilableFile
	{
		private static Oxide.Core.Libraries.Timer timer = Interface.Oxide.GetLibrary<Oxide.Core.Libraries.Timer>();

		private static object compileLock = new object();

		public CSharpExtension Extension;

		public CSharpPluginLoader Loader;

		public string Name;

		public string Directory;

		public string ScriptName;

		public string ScriptPath;

		public string[] ScriptLines;

		public Encoding ScriptEncoding;

		public HashSet<string> Requires = new HashSet<string>();

		public HashSet<string> References = new HashSet<string>();

		public HashSet<string> IncludePaths = new HashSet<string>();

		public string CompilerErrors;

		public CompiledAssembly CompiledAssembly;

		public DateTime LastModifiedAt;

		public DateTime LastCachedScriptAt;

		public DateTime LastCompiledAt;

		public bool IsCompilationNeeded;

		protected Action<CSharpPlugin> LoadCallback;

		protected Action<bool> CompileCallback;

		protected float CompilationQueuedAt;

		private Oxide.Core.Libraries.Timer.TimerInstance timeoutTimer;

		public byte[] ScriptSource => ScriptEncoding.GetBytes(string.Join(Environment.NewLine, ScriptLines));

		public CompilableFile(CSharpExtension extension, CSharpPluginLoader loader, string directory, string name)
		{
			Extension = extension;
			Loader = loader;
			Directory = directory;
			ScriptName = name;
			ScriptPath = Path.Combine(Directory, ScriptName + ".cs");
			Name = Regex.Replace(ScriptName, "_", "");
			CheckLastModificationTime();
		}

		internal void Compile(Action<bool> callback)
		{
			lock (compileLock)
			{
				if (CompilationQueuedAt > 0f)
				{
					float num = Interface.Oxide.Now - CompilationQueuedAt;
					Interface.Oxide.LogDebug($"Plugin compilation is already queued: {ScriptName} ({num:0.000} ago)");
					return;
				}
				OnLoadingStarted();
				if (CompiledAssembly != null && !HasBeenModified() && (CompiledAssembly.IsLoading || !CompiledAssembly.IsBatch || CompiledAssembly.CompilablePlugins.All((CompilablePlugin pl) => pl.IsLoading)))
				{
					callback(obj: true);
					return;
				}
				IsCompilationNeeded = true;
				CompileCallback = callback;
				CompilationQueuedAt = Interface.Oxide.Now;
				OnCompilationRequested();
			}
		}

		internal virtual void OnCompilationStarted()
		{
			LastCompiledAt = LastModifiedAt;
			timeoutTimer?.Destroy();
			timeoutTimer = null;
			Interface.Oxide.NextTick(delegate
			{
				timeoutTimer?.Destroy();
				timeoutTimer = timer.Once(Math.Max(30, Loader.LoadingPlugins.Count * 3), OnCompilationTimeout);
			});
		}

		internal void OnCompilationSucceeded(CompiledAssembly compiledAssembly)
		{
			if (timeoutTimer == null)
			{
				Interface.Oxide.LogWarning("Ignored unexpected plugin compilation: " + Name);
				return;
			}
			timeoutTimer?.Destroy();
			timeoutTimer = null;
			IsCompilationNeeded = false;
			CompilationQueuedAt = 0f;
			CompiledAssembly = compiledAssembly;
			CompileCallback?.Invoke(obj: true);
		}

		internal void OnCompilationFailed()
		{
			if (timeoutTimer == null)
			{
				Interface.Oxide.LogWarning("Ignored unexpected plugin compilation failure: " + Name);
				return;
			}
			timeoutTimer?.Destroy();
			timeoutTimer = null;
			CompilationQueuedAt = 0f;
			LastCompiledAt = default(DateTime);
			CompileCallback?.Invoke(obj: false);
			IsCompilationNeeded = false;
		}

		internal void OnCompilationTimeout()
		{
			Interface.Oxide.LogError("Timed out waiting for plugin to be compiled: " + Name);
			CompilerErrors = "Timed out waiting for compilation";
			OnCompilationFailed();
		}

		internal bool HasBeenModified()
		{
			DateTime lastModifiedAt = LastModifiedAt;
			CheckLastModificationTime();
			return LastModifiedAt != lastModifiedAt;
		}

		internal void CheckLastModificationTime()
		{
			if (!File.Exists(ScriptPath))
			{
				LastModifiedAt = default(DateTime);
				return;
			}
			DateTime lastModificationTime = GetLastModificationTime();
			if (lastModificationTime != default(DateTime))
			{
				LastModifiedAt = lastModificationTime;
			}
		}

		internal DateTime GetLastModificationTime()
		{
			try
			{
				return File.GetLastWriteTime(ScriptPath);
			}
			catch (IOException ex)
			{
				Interface.Oxide.LogError("IOException while checking plugin: {0} ({1})", ScriptName, ex.Message);
				return default(DateTime);
			}
		}

		protected virtual void OnLoadingStarted()
		{
		}

		protected virtual void OnCompilationRequested()
		{
		}

		protected virtual void InitFailed(string message = null)
		{
			if (message != null)
			{
				Interface.Oxide.LogError(message);
			}
			LoadCallback?.Invoke(null);
		}
	}
	public class CompilablePlugin : CompilableFile
	{
		private static object compileLock = new object();

		public CompiledAssembly LastGoodAssembly;

		public bool IsLoading;

		public CompilablePlugin(CSharpExtension extension, CSharpPluginLoader loader, string directory, string name)
			: base(extension, loader, directory, name)
		{
		}

		protected override void OnLoadingStarted()
		{
			Loader.PluginLoadingStarted(this);
		}

		protected override void OnCompilationRequested()
		{
			Loader.CompilationRequested(this);
		}

		internal void LoadPlugin(Action<CSharpPlugin> callback = null)
		{
			if (CompiledAssembly == null)
			{
				Interface.Oxide.LogError("Load called before a compiled assembly exists: {0}", Name);
				return;
			}
			LoadCallback = callback;
			CompiledAssembly.LoadAssembly(delegate(bool loaded)
			{
				if (!loaded)
				{
					callback?.Invoke(null);
				}
				else if (CompilerErrors != null)
				{
					InitFailed("Unable to load " + ScriptName + ". " + CompilerErrors);
				}
				else
				{
					Type type = CompiledAssembly.LoadedAssembly.GetType("Oxide.Plugins." + Name);
					if (type == null)
					{
						InitFailed("Unable to find main plugin class: " + Name);
					}
					else if (!typeof(CSharpPlugin).IsAssignableFrom(type))
					{
						InitFailed("Main plugin class is not assignable to `CSharpPlugin`");
					}
					else
					{
						CSharpPlugin cSharpPlugin;
						try
						{
							cSharpPlugin = Activator.CreateInstance(type) as CSharpPlugin;
						}
						catch (MissingMethodException)
						{
							InitFailed("Main plugin class should not have a constructor defined: " + Name);
							return;
						}
						catch (TargetInvocationException ex2)
						{
							Exception innerException = ex2.InnerException;
							InitFailed($"Unable to load {ScriptName}. {innerException}");
							return;
						}
						catch (Exception arg)
						{
							InitFailed($"Unable to load {ScriptName}. {arg}");
							return;
						}
						if (cSharpPlugin == null)
						{
							InitFailed("Plugin assembly failed to load: " + ScriptName);
						}
						else if (!cSharpPlugin.SetPluginInfo(ScriptName, ScriptPath))
						{
							InitFailed();
						}
						else
						{
							cSharpPlugin.Watcher = Extension.Watcher;
							cSharpPlugin.Loader = Loader;
							if (!Interface.Oxide.PluginLoaded(cSharpPlugin))
							{
								InitFailed();
							}
							else
							{
								if (!CompiledAssembly.IsBatch)
								{
									LastGoodAssembly = CompiledAssembly;
								}
								callback?.Invoke(cSharpPlugin);
							}
						}
					}
				}
			});
		}

		internal override void OnCompilationStarted()
		{
			base.OnCompilationStarted();
			foreach (Plugin plugin in Interface.Oxide.RootPluginManager.GetPlugins())
			{
				if (plugin is CSharpPlugin)
				{
					CompilablePlugin compilablePlugin = CSharpPluginLoader.GetCompilablePlugin(Directory, plugin.Name);
					if (compilablePlugin.Requires.Contains(Name))
					{
						compilablePlugin.CompiledAssembly = null;
						Loader.Load(compilablePlugin);
					}
				}
			}
		}

		protected override void InitFailed(string message = null)
		{
			base.InitFailed(message);
			if (LastGoodAssembly == null)
			{
				Interface.Oxide.LogInfo("No previous version to rollback plugin: {0}", ScriptName);
				return;
			}
			if (CompiledAssembly == LastGoodAssembly)
			{
				Interface.Oxide.LogInfo("Previous version of plugin failed to load: {0}", ScriptName);
				return;
			}
			Interface.Oxide.LogInfo("Rolling back plugin to last good version: {0}", ScriptName);
			CompiledAssembly = LastGoodAssembly;
			CompilerErrors = null;
			LoadPlugin();
		}
	}
	internal class Compilation
	{
		public static Compilation Current;

		internal int id;

		internal string name;

		internal Action<Compilation> callback;

		internal ConcurrentHashSet<CompilablePlugin> queuedPlugins;

		internal HashSet<CompilablePlugin> plugins = new HashSet<CompilablePlugin>();

		internal float startedAt;

		internal float endedAt;

		internal Hash<string, CompilerFile> references = new Hash<string, CompilerFile>();

		internal HashSet<string> referencedPlugins = new HashSet<string>();

		internal CompiledAssembly compiledAssembly;

		private string includePath;

		private string[] extensionNames;

		internal float duration => endedAt - startedAt;

		internal Compilation(int id, Action<Compilation> callback, CompilablePlugin[] plugins)
		{
			this.id = id;
			this.callback = callback;
			queuedPlugins = new ConcurrentHashSet<CompilablePlugin>(plugins);
			if (Current == null)
			{
				Current = this;
			}
			foreach (CompilablePlugin obj in plugins)
			{
				obj.CompilerErrors = null;
				obj.OnCompilationStarted();
			}
			includePath = Path.Combine(Interface.Oxide.PluginDirectory, "include");
			extensionNames = (from ext in Interface.Oxide.GetAllExtensions()
				select ext.Name).ToArray();
		}

		internal void Started()
		{
			name = ((plugins.Count < 2) ? plugins.First().Name : "plugins_") + Math.Round(Interface.Oxide.Now * 10000000f) + ".dll";
		}

		internal void Completed(byte[] rawAssembly = null, byte[] symbols = null)
		{
			endedAt = Interface.Oxide.Now;
			if (plugins.Count > 0 && rawAssembly != null)
			{
				compiledAssembly = new CompiledAssembly(name, plugins.ToArray(), rawAssembly, duration, symbols);
			}
			Interface.Oxide.NextTick(delegate
			{
				callback(this);
			});
		}

		internal void Add(CompilablePlugin plugin)
		{
			if (!queuedPlugins.Add(plugin))
			{
				return;
			}
			plugin.Loader.PluginLoadingStarted(plugin);
			plugin.CompilerErrors = null;
			plugin.OnCompilationStarted();
			foreach (Plugin item in from pl in Interface.Oxide.RootPluginManager.GetPlugins()
				where pl is CSharpPlugin
				select pl)
			{
				CompilablePlugin compilablePlugin = CSharpPluginLoader.GetCompilablePlugin(plugin.Directory, item.Name);
				if (compilablePlugin.Requires.Contains(plugin.Name))
				{
					AddDependency(compilablePlugin);
				}
			}
		}

		internal bool IncludesRequiredPlugin(string name)
		{
			if (referencedPlugins.Contains(name))
			{
				return true;
			}
			CompilablePlugin compilablePlugin = plugins.SingleOrDefault((CompilablePlugin pl) => pl.Name == name);
			if (compilablePlugin != null)
			{
				return compilablePlugin.CompilerErrors == null;
			}
			return false;
		}

		internal void Prepare(Action callback)
		{
			ThreadPool.QueueUserWorkItem(delegate
			{
				try
				{
					referencedPlugins.Clear();
					references.Clear();
					foreach (string pluginReference in CSharpPluginLoader.PluginReferences)
					{
						bool flag = true;
						if (File.Exists(Path.Combine(Interface.Oxide.ExtensionDirectory, pluginReference + ".dll")))
						{
							references[pluginReference + ".dll"] = CompilerFile.CachedReadFile(Interface.Oxide.ExtensionDirectory, pluginReference + ".dll");
						}
						else if (File.Exists(Path.Combine(Interface.Oxide.ExtensionDirectory, pluginReference + ".exe")))
						{
							references[pluginReference + ".exe"] = CompilerFile.CachedReadFile(Interface.Oxide.ExtensionDirectory, pluginReference + ".exe");
						}
						else if (File.Exists(Path.Combine(Interface.Oxide.RootDirectory, pluginReference + ".exe")))
						{
							references[pluginReference + ".exe"] = CompilerFile.CachedReadFile(Interface.Oxide.RootDirectory, pluginReference + ".exe");
						}
						else
						{
							flag = false;
						}
						if (!flag)
						{
							Interface.Oxide.RootLogger.WriteDebug(LogType.Error, LogEvent.Compile, "CSharp", "Failed to add default reference: " + pluginReference + " - Not found!");
						}
					}
					Interface.Oxide.RootLogger.WriteDebug(LogType.Info, LogEvent.Compile, "CSharp", "Preparing compilation");
					List<CompilablePlugin> list = new List<CompilablePlugin>();
					CompilablePlugin value;
					while (queuedPlugins.TryDequeue(out value))
					{
						if (Current == null)
						{
							Current = this;
						}
						if (!CacheScriptLines(value) || value.ScriptLines.Length < 1)
						{
							value.References.Clear();
							value.IncludePaths.Clear();
							value.Requires.Clear();
							Interface.Oxide.RootLogger.WriteDebug(LogType.Error, LogEvent.Compile, "CSharp", "Script file is empty: " + value.Name);
							RemovePlugin(value);
						}
						if (!list.Contains(value))
						{
							list.Add(value);
							PreparseScript(value);
							ResolveReferences(value);
						}
						else
						{
							Interface.Oxide.RootLogger.WriteDebug(LogType.Error, LogEvent.Compile, "CSharp", "Plugin is already part of the compilation: " + value.Name);
						}
						CacheModifiedScripts();
						if (queuedPlugins.Count == 0 && Current == this)
						{
							Current = null;
						}
					}
					list.Sort((CompilablePlugin x, CompilablePlugin y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal));
					foreach (CompilablePlugin item in list)
					{
						if (!plugins.Add(item))
						{
							Interface.Oxide.RootLogger.WriteDebug(LogType.Error, LogEvent.Compile, "CSharp", "Failed to add plugin to compilation: " + item.Name);
						}
						else
						{
							Interface.Oxide.RootLogger.WriteDebug(LogType.Info, LogEvent.Compile, "CSharp", "Added plugin to compilation: " + item.Name);
						}
					}
					Interface.Oxide.RootLogger.WriteDebug(LogType.Info, LogEvent.Compile, "CSharp", "Done preparing compilation: " + plugins.Select((CompilablePlugin p) => p.Name).ToSentence());
					callback();
				}
				catch (Exception ex)
				{
					Interface.Oxide.LogException("Exception while resolving plugin references", ex);
				}
			});
		}

		private void PreparseScript(CompilablePlugin plugin)
		{
			plugin.References.Clear();
			plugin.IncludePaths.Clear();
			plugin.Requires.Clear();
			bool flag = false;
			for (int i = 0; i < plugin.ScriptLines.Length; i++)
			{
				string text = plugin.ScriptLines[i].Trim();
				if (text.IndexOf("namespace uMod.Plugins", StringComparison.InvariantCultureIgnoreCase) >= 0)
				{
					Interface.Oxide.LogError("Plugin " + plugin.ScriptName + ".cs is a uMod plugin, not an Oxide plugin. Please downgrade to the Oxide version if available.");
					plugin.CompilerErrors = "Plugin " + plugin.ScriptName + ".cs is a uMod plugin, not an Oxide plugin. Please downgrade to the Oxide version if available.";
					RemovePlugin(plugin);
					break;
				}
				if (text.Length < 1)
				{
					continue;
				}
				Match match;
				if (flag)
				{
					match = Regex.Match(text, "^\\s*\\{?\\s*$", RegexOptions.IgnoreCase);
					if (match.Success)
					{
						continue;
					}
					match = Regex.Match(text, "^\\s*\\[", RegexOptions.IgnoreCase);
					if (match.Success)
					{
						continue;
					}
					match = Regex.Match(text, "^\\s*(?:public|private|protected|internal)?\\s*class\\s+(\\S+)\\s+\\:\\s+\\S+Plugin\\s*$", RegexOptions.IgnoreCase);
					if (match.Success)
					{
						string value = match.Groups[1].Value;
						if (value != plugin.Name)
						{
							Interface.Oxide.LogError("Plugin filename " + plugin.ScriptName + ".cs must match the main class " + value + " (should be " + value + ".cs)");
							plugin.CompilerErrors = "Plugin filename " + plugin.ScriptName + ".cs must match the main class " + value + " (should be " + value + ".cs)";
							RemovePlugin(plugin);
						}
					}
					break;
				}
				match = Regex.Match(text, "^//\\s*Requires:\\s*(\\S+?)(\\.cs)?\\s*$", RegexOptions.IgnoreCase);
				if (match.Success)
				{
					string value2 = match.Groups[1].Value;
					plugin.Requires.Add(value2);
					if (!File.Exists(Path.Combine(plugin.Directory, value2 + ".cs")))
					{
						Interface.Oxide.LogError(plugin.Name + " plugin requires missing dependency: " + value2);
						plugin.CompilerErrors = "Missing dependency: " + value2;
						RemovePlugin(plugin);
						break;
					}
					CompilablePlugin compilablePlugin = CSharpPluginLoader.GetCompilablePlugin(plugin.Directory, value2);
					AddDependency(compilablePlugin);
					continue;
				}
				match = Regex.Match(text, "^//\\s*Reference:\\s*(\\S+)\\s*$", RegexOptions.IgnoreCase);
				if (match.Success)
				{
					string value3 = match.Groups[1].Value;
					if (!value3.StartsWith("Oxide.") && !value3.Contains("Newtonsoft.Json") && !value3.Contains("protobuf-net"))
					{
						AddReference(plugin, value3);
						Interface.Oxide.LogInfo("Added '// Reference: {0}' in plugin '{1}'", value3, plugin.Name);
					}
					else
					{
						Interface.Oxide.LogWarning("Ignored unnecessary '// Reference: {0}' in plugin '{1}'", value3, plugin.Name);
					}
					continue;
				}
				match = Regex.Match(text, "^\\s*using\\s+(Oxide\\.(?:Core|Ext|Game)\\.(?:[^\\.]+))[^;]*;.*$", RegexOptions.IgnoreCase);
				if (match.Success)
				{
					string value4 = match.Groups[1].Value;
					string text2 = Regex.Replace(value4, "Oxide\\.[\\w]+\\.([\\w]+)", "Oxide.$1");
					if (!string.IsNullOrEmpty(text2) && File.Exists(Path.Combine(Interface.Oxide.ExtensionDirectory, text2 + ".dll")))
					{
						AddReference(plugin, text2);
					}
					else
					{
						AddReference(plugin, value4);
					}
				}
				else
				{
					match = Regex.Match(text, "^\\s*namespace Oxide\\.Plugins\\s*(\\{\\s*)?$", RegexOptions.IgnoreCase);
					if (match.Success)
					{
						flag = true;
					}
				}
			}
		}

		private void ResolveReferences(CompilablePlugin plugin)
		{
			foreach (string reference in plugin.References)
			{
				Match match = Regex.Match(reference, "^(Oxide\\.(?:Ext|Game)\\.(.+))$", RegexOptions.IgnoreCase);
				if (!match.Success)
				{
					continue;
				}
				string value = match.Groups[1].Value;
				string value2 = match.Groups[2].Value;
				if (extensionNames.Contains(value2))
				{
					continue;
				}
				if (Directory.Exists(includePath))
				{
					string text = Path.Combine(includePath, "Ext." + value2 + ".cs");
					if (File.Exists(text))
					{
						plugin.IncludePaths.Add(text);
						continue;
					}
				}
				string text2 = value + " is referenced by " + plugin.Name + " plugin but is not loaded";
				Interface.Oxide.LogError(text2);
				plugin.CompilerErrors = text2;
				RemovePlugin(plugin);
			}
		}

		private void AddDependency(CompilablePlugin plugin)
		{
			if (plugin.IsLoading || plugins.Contains(plugin) || queuedPlugins.Contains(plugin))
			{
				return;
			}
			CompiledAssembly compiledAssembly = plugin.CompiledAssembly;
			if (compiledAssembly != null && !compiledAssembly.IsOutdated())
			{
				referencedPlugins.Add(plugin.Name);
				if (!references.ContainsKey(compiledAssembly.Name))
				{
					references[compiledAssembly.Name] = new CompilerFile(compiledAssembly.Name, compiledAssembly.RawAssembly);
				}
			}
			else
			{
				Add(plugin);
			}
		}

		private void AddReference(CompilablePlugin plugin, string assemblyName)
		{
			if (!File.Exists(Path.Combine(Interface.Oxide.ExtensionDirectory, assemblyName + ".dll")))
			{
				if (assemblyName.StartsWith("Oxide."))
				{
					plugin.References.Add(assemblyName);
					return;
				}
				Interface.Oxide.LogError("Assembly referenced by " + plugin.Name + " plugin does not exist: " + assemblyName + ".dll");
				plugin.CompilerErrors = "Referenced assembly does not exist: " + assemblyName;
				RemovePlugin(plugin);
				return;
			}
			Assembly assembly;
			try
			{
				assembly = Assembly.Load(assemblyName);
			}
			catch (FileNotFoundException)
			{
				Interface.Oxide.LogError("Assembly referenced by " + plugin.Name + " plugin is invalid: " + assemblyName + ".dll");
				plugin.CompilerErrors = "Referenced assembly is invalid: " + assemblyName;
				RemovePlugin(plugin);
				return;
			}
			AddReference(plugin, assembly.GetName());
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			foreach (AssemblyName assemblyName2 in referencedAssemblies)
			{
				if (!assemblyName2.Name.StartsWith("Newtonsoft.Json") && !assemblyName2.Name.StartsWith("Rust.Workshop"))
				{
					if (!File.Exists(Path.Combine(Interface.Oxide.ExtensionDirectory, assemblyName2.Name + ".dll")))
					{
						Interface.Oxide.LogWarning("Reference " + assemblyName2.Name + ".dll from " + assembly.GetName().Name + ".dll not found");
					}
					else
					{
						AddReference(plugin, assemblyName2);
					}
				}
			}
		}

		private void AddReference(CompilablePlugin plugin, AssemblyName reference)
		{
			string text = reference.Name + ".dll";
			if (!references.ContainsKey(text))
			{
				Interface.Oxide.RootLogger.WriteDebug(LogType.Info, LogEvent.Compile, "CSharp", reference.Name + " has been added as a reference");
				references[text] = CompilerFile.CachedReadFile(Interface.Oxide.ExtensionDirectory, text);
			}
			if (!plugin.References.Contains(reference.Name))
			{
				plugin.References.Add(reference.Name);
			}
		}

		private bool CacheScriptLines(CompilablePlugin plugin)
		{
			bool flag = false;
			while (true)
			{
				try
				{
					if (!File.Exists(plugin.ScriptPath))
					{
						Interface.Oxide.LogWarning("Script no longer exists: {0}", plugin.Name);
						plugin.CompilerErrors = "Plugin file was deleted";
						RemovePlugin(plugin);
						return false;
					}
					plugin.CheckLastModificationTime();
					if (plugin.LastCachedScriptAt != plugin.LastModifiedAt)
					{
						using (StreamReader streamReader = File.OpenText(plugin.ScriptPath))
						{
							List<string> list = new List<string>();
							while (!streamReader.EndOfStream)
							{
								list.Add(streamReader.ReadLine());
							}
							plugin.ScriptLines = list.ToArray();
							plugin.ScriptEncoding = streamReader.CurrentEncoding;
						}
						plugin.LastCachedScriptAt = plugin.LastModifiedAt;
						if (plugins.Remove(plugin))
						{
							queuedPlugins.Add(plugin);
						}
					}
					return true;
				}
				catch (IOException)
				{
					if (!flag)
					{
						flag = true;
						Interface.Oxide.LogWarning("Waiting for another application to stop using script: {0}", plugin.Name);
					}
					Thread.Sleep(50);
				}
			}
		}

		private void CacheModifiedScripts()
		{
			CompilablePlugin[] array = plugins.Where((CompilablePlugin pl) => pl.ScriptLines == null || pl.HasBeenModified() || pl.LastCachedScriptAt != pl.LastModifiedAt).ToArray();
			if (array.Length >= 1)
			{
				CompilablePlugin[] array2 = array;
				foreach (CompilablePlugin plugin in array2)
				{
					CacheScriptLines(plugin);
				}
				Thread.Sleep(100);
				CacheModifiedScripts();
			}
		}

		private void RemovePlugin(CompilablePlugin plugin)
		{
			if (plugin.LastCompiledAt == default(DateTime))
			{
				return;
			}
			queuedPlugins.Remove(plugin);
			plugins.Remove(plugin);
			plugin.OnCompilationFailed();
			CompilablePlugin[] array = plugins.Where((CompilablePlugin pl) => !pl.IsCompilationNeeded && plugin.Requires.Contains(pl.Name)).ToArray();
			foreach (CompilablePlugin requiredPlugin in array)
			{
				if (!plugins.Any((CompilablePlugin pl) => pl.Requires.Contains(requiredPlugin.Name)))
				{
					RemovePlugin(requiredPlugin);
				}
			}
		}
	}
	public class CompiledAssembly
	{
		public CompilablePlugin[] CompilablePlugins;

		public string[] PluginNames;

		public string Name;

		public DateTime CompiledAt;

		public byte[] RawAssembly;

		public byte[] Symbols;

		public byte[] PatchedAssembly;

		public float Duration;

		public Assembly LoadedAssembly;

		public bool IsLoading;

		private List<Action<bool>> loadCallbacks = new List<Action<bool>>();

		private bool isPatching;

		private bool isLoaded;

		public bool IsBatch => CompilablePlugins.Length > 1;

		public CompiledAssembly(string name, CompilablePlugin[] plugins, byte[] rawAssembly, float duration, byte[] symbols)
		{
			Name = name;
			CompilablePlugins = plugins;
			RawAssembly = rawAssembly;
			Duration = duration;
			PluginNames = CompilablePlugins.Select((CompilablePlugin pl) => pl.Name).ToArray();
			Symbols = symbols;
		}

		public void LoadAssembly(Action<bool> callback)
		{
			if (isLoaded)
			{
				callback(obj: true);
				return;
			}
			IsLoading = true;
			loadCallbacks.Add(callback);
			if (isPatching)
			{
				return;
			}
			ValidateAssembly(delegate(byte[] rawAssembly)
			{
				if (rawAssembly == null)
				{
					foreach (Action<bool> loadCallback in loadCallbacks)
					{
						loadCallback(obj: true);
					}
					loadCallbacks.Clear();
					IsLoading = false;
				}
				else
				{
					LoadedAssembly = Assembly.Load(rawAssembly);
					isLoaded = true;
					foreach (Action<bool> loadCallback2 in loadCallbacks)
					{
						loadCallback2(obj: true);
					}
					loadCallbacks.Clear();
					IsLoading = false;
				}
			});
		}

		private void ValidateAssembly(Action<byte[]> callback)
		{
			if (isPatching)
			{
				Interface.Oxide.RootLogger.WriteDebug(LogType.Warning, LogEvent.Compile, "CSharp", "Already patching plugin assembly: " + PluginNames.ToSentence() + " (ignoring)");
				return;
			}
			isPatching = true;
			ThreadPool.QueueUserWorkItem(delegate
			{
				try
				{
					AssemblyDefinition assemblyDefinition = null;
					ReaderParameters readerParameters = new ReaderParameters
					{
						AssemblyResolver = new AssemblyResolver()
					};
					using (MemoryStream stream = new MemoryStream(RawAssembly))
					{
						assemblyDefinition = AssemblyDefinition.ReadAssembly(stream, readerParameters);
					}
					int num = 0;
					int num2 = CompilablePlugins.Count((CompilablePlugin p) => p.CompilerErrors == null);
					for (int num3 = 0; num3 < assemblyDefinition.MainModule.Types.Count; num3++)
					{
						if (num == num2)
						{
							Interface.Oxide.RootLogger.WriteDebug(LogType.Info, LogEvent.Compile, "CSharp", $"Patched {num} of {num2} plugins");
							break;
						}
						try
						{
							TypeDefinition type = assemblyDefinition.MainModule.Types[num3];
							if (!(type.Namespace != "Oxide.Plugins") && PluginNames.Contains(type.Name))
							{
								num++;
								Interface.Oxide.RootLogger.WriteDebug(LogType.Info, LogEvent.Compile, "CSharp", "Preparing " + type.Name + " for runtime patching. . .");
								if (type.Methods.FirstOrDefault((MethodDefinition m) => !m.IsStatic && m.IsConstructor && !m.HasParameters && !m.IsPublic) != null)
								{
									Interface.Oxide.RootLogger.WriteDebug(LogType.Error, LogEvent.Compile, "CSharp", "User defined constructors are not supported. Please remove the constructor from " + type.Name + ".cs");
									CompilablePlugin compilablePlugin = CompilablePlugins.SingleOrDefault((CompilablePlugin p) => p.Name == type.Name);
									if (compilablePlugin != null)
									{
										compilablePlugin.CompilerErrors = "Primary constructor in main class must be public";
									}
								}
								else
								{
									Interface.Oxide.RootLogger.WriteDebug(LogType.Info, LogEvent.Compile, "CSharp", "Patching DirectCallMethod on " + type.Name);
									new DirectCallMethod(assemblyDefinition.MainModule, type, readerParameters);
								}
							}
						}
						catch (Exception exception)
						{
							Interface.Oxide.RootLogger.WriteDebug(LogType.Error, LogEvent.Compile, "CSharp", $"Failed to patch type at index {num3}", exception);
						}
					}
					using (MemoryStream memoryStream = new MemoryStream())
					{
						assemblyDefinition.Write(memoryStream, new WriterParameters
						{
							WriteSymbols = false
						});
						PatchedAssembly = memoryStream.ToArray();
					}
					Interface.Oxide.NextTick(delegate
					{
						isPatching = false;
						callback(PatchedAssembly);
					});
				}
				catch (Exception ex)
				{
					Exception ex2 = ex;
					Exception ex3 = ex2;
					Interface.Oxide.NextTick(delegate
					{
						isPatching = false;
						Interface.Oxide.RootLogger.WriteDebug(LogType.Warning, LogEvent.Compile, "CSharp", "Failed to patch DirectCallHook method on plugins " + PluginNames.ToSentence() + ", performance may be degraded.", ex3);
						callback(RawAssembly);
					});
				}
			});
		}

		public bool IsOutdated()
		{
			return CompilablePlugins.Any((CompilablePlugin pl) => pl.GetLastModificationTime() != CompiledAt);
		}
	}
	[AttributeUsage(AttributeTargets.Method)]
	public class CommandAttribute : Attribute
	{
		public string[] Commands { get; }

		public CommandAttribute(params string[] commands)
		{
			Commands = commands;
		}
	}
	[AttributeUsage(AttributeTargets.Method)]
	public class PermissionAttribute : Attribute
	{
		public string[] Permission { get; }

		public PermissionAttribute(string permission)
		{
			Permission = new string[1] { permission };
		}
	}
	public class CovalencePlugin : CSharpPlugin
	{
		private new static readonly Covalence covalence = Interface.Oxide.GetLibrary<Covalence>();

		protected string game = covalence.Game;

		protected IPlayerManager players = covalence.Players;

		protected IServer server = covalence.Server;

		protected void Log(string format, params object[] args)
		{
			Interface.Oxide.LogInfo("[{0}] {1}", base.Title, (args.Length != 0) ? string.Format(format, args) : format);
		}

		protected void LogWarning(string format, params object[] args)
		{
			Interface.Oxide.LogWarning("[{0}] {1}", base.Title, (args.Length != 0) ? string.Format(format, args) : format);
		}

		protected void LogError(string format, params object[] args)
		{
			Interface.Oxide.LogError("[{0}] {1}", base.Title, (args.Length != 0) ? string.Format(format, args) : format);
		}

		public override void HandleAddedToManager(PluginManager manager)
		{
			MethodInfo[] methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
			foreach (MethodInfo method in methods)
			{
				object[] customAttributes = method.GetCustomAttributes(typeof(CommandAttribute), inherit: true);
				object[] customAttributes2 = method.GetCustomAttributes(typeof(PermissionAttribute), inherit: true);
				if (customAttributes.Length == 0)
				{
					continue;
				}
				CommandAttribute commandAttribute = customAttributes[0] as CommandAttribute;
				PermissionAttribute permissionAttribute = ((customAttributes2.Length == 0) ? null : (customAttributes2[0] as PermissionAttribute));
				if (commandAttribute != null)
				{
					AddCovalenceCommand(commandAttribute.Commands, permissionAttribute?.Permission, delegate(IPlayer caller, string command, string[] args)
					{
						CallHook(method.Name, caller, command, args);
						return true;
					});
				}
			}
			base.HandleAddedToManager(manager);
		}
	}
	public class CSharpExtension : Extension
	{
		internal static Assembly Assembly = Assembly.GetExecutingAssembly();

		internal static AssemblyName AssemblyName = Assembly.GetName();

		internal static VersionNumber AssemblyVersion = new VersionNumber(AssemblyName.Version.Major, AssemblyName.Version.Minor, AssemblyName.Version.Build);

		internal static string AssemblyAuthors = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly, typeof(AssemblyCompanyAttribute), inherit: false)).Company;

		private CSharpPluginLoader loader;

		public override bool IsCoreExtension => true;

		public override string Name => "CSharp";

		public override string Author => AssemblyAuthors;

		public override VersionNumber Version => AssemblyVersion;

		public FSWatcher Watcher { get; private set; }

		public CSharpExtension(ExtensionManager manager)
			: base(manager)
		{
			if (Environment.OSVersion.Platform == PlatformID.Unix)
			{
				string extensionDirectory = Interface.Oxide.ExtensionDirectory;
				string path = Path.Combine(extensionDirectory, "Oxide.References.dll.config");
				if (!File.Exists(path) || new string[2] { "target=\"x64", "target=\"./x64" }.Any(File.ReadAllText(path).Contains))
				{
					File.WriteAllText(path, "<configuration>\n<dllmap dll=\"MonoPosixHelper\" target=\"" + extensionDirectory + "/x86/libMonoPosixHelper.so\" os=\"!windows,osx\" wordsize=\"32\" />\n<dllmap dll=\"MonoPosixHelper\" target=\"" + extensionDirectory + "/x64/libMonoPosixHelper.so\" os=\"!windows,osx\" wordsize=\"64\" />\n</configuration>");
				}
			}
		}

		public override void Load()
		{
			loader = new CSharpPluginLoader(this);
			base.Manager.RegisterPluginLoader(loader);
			Interface.Oxide.OnFrame(OnFrame);
		}

		public override void LoadPluginWatchers(string pluginDirectory)
		{
			Watcher = new FSWatcher(pluginDirectory, "*.cs");
			base.Manager.RegisterPluginChangeWatcher(Watcher);
		}

		public override void OnModLoad()
		{
			loader.OnModLoaded();
		}

		public override void OnShutdown()
		{
			base.OnShutdown();
			loader.OnShutdown();
		}

		private void OnFrame(float delta)
		{
			object[] args = new object[1] { delta };
			foreach (KeyValuePair<string, Plugin> loadedPlugin in loader.LoadedPlugins)
			{
				if (loadedPlugin.Value is CSharpPlugin { HookedOnFrame: not false } cSharpPlugin)
				{
					cSharpPlugin.CallHook("OnFrame", args);
				}
			}
		}
	}
	public class PluginLoadFailure : Exception
	{
		public PluginLoadFailure(string reason)
		{
		}
	}
	[AttributeUsage(AttributeTargets.Class)]
	public class InfoAttribute : Attribute
	{
		public string Title { get; }

		public string Author { get; }

		public VersionNumber Version { get; private set; }

		public int ResourceId { get; set; }

		public InfoAttribute(string Title, string Author, string Version)
		{
			this.Title = Title;
			this.Author = Author;
			SetVersion(Version);
		}

		public InfoAttribute(string Title, string Author, double Version)
		{
			this.Title = Title;
			this.Author = Author;
			SetVersion(Version.ToString());
		}

		private void SetVersion(string version)
		{
			ushort result;
			List<ushort> list = (from part in version.Split(new char[1] { '.' })
				select (ushort)(ushort.TryParse(part, out result) ? result : 0)).ToList();
			while (list.Count < 3)
			{
				list.Add(0);
			}
			if (list.Count > 3)
			{
				Interface.Oxide.LogWarning("Version `" + version + "` is invalid for " + Title + ", should be `major.minor.patch`");
			}
			Version = new VersionNumber(list[0], list[1], list[2]);
		}
	}
	[AttributeUsage(AttributeTargets.Class)]
	public class DescriptionAttribute : Attribute
	{
		public string Description { get; }

		public DescriptionAttribute(string description)
		{
			Description = description;
		}
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class PluginReferenceAttribute : Attribute
	{
		public string Name { get; }

		public PluginReferenceAttribute()
		{
		}

		public PluginReferenceAttribute(string name)
		{
			Name = name;
		}
	}
	[AttributeUsage(AttributeTargets.Method)]
	public class ConsoleCommandAttribute : Attribute
	{
		public string Command { get; private set; }

		public ConsoleCommandAttribute(string command)
		{
			Command = (Enumerable.Contains(command, '.') ? command : ("global." + command));
		}
	}
	[AttributeUsage(AttributeTargets.Method)]
	public class ChatCommandAttribute : Attribute
	{
		public string Command { get; private set; }

		public ChatCommandAttribute(string command)
		{
			Command = command;
		}
	}
	[AttributeUsage(AttributeTargets.Field)]
	public class OnlinePlayersAttribute : Attribute
	{
	}
	public abstract class CSharpPlugin : CSPlugin
	{
		public class PluginFieldInfo
		{
			public Plugin Plugin;

			public FieldInfo Field;

			public Type FieldType;

			public Type[] GenericArguments;

			public Dictionary<string, MethodInfo> Methods = new Dictionary<string, MethodInfo>();

			public object Value => Field.GetValue(Plugin);

			public PluginFieldInfo(Plugin plugin, FieldInfo field)
			{
				Plugin = plugin;
				Field = field;
				FieldType = field.FieldType;
				GenericArguments = FieldType.GetGenericArguments();
			}

			public bool HasValidConstructor(params Type[] argument_types)
			{
				Type type = GenericArguments[1];
				if (!(type.GetConstructor(new Type[0]) != null))
				{
					return type.GetConstructor(argument_types) != null;
				}
				return true;
			}

			public bool LookupMethod(string method_name, params Type[] argument_types)
			{
				MethodInfo method = FieldType.GetMethod(method_name, argument_types);
				if (method == null)
				{
					return false;
				}
				Methods[method_name] = method;
				return true;
			}

			public object Call(string method_name, params object[] args)
			{
				if (!Methods.TryGetValue(method_name, out var value))
				{
					value = FieldType.GetMethod(method_name, BindingFlags.Instance | BindingFlags.Public);
					Methods[method_name] = value;
				}
				if (value == null)
				{
					throw new MissingMethodException(FieldType.Name, method_name);
				}
				return value.Invoke(Value, args);
			}
		}

		public FSWatcher Watcher;

		protected Covalence covalence = Interface.Oxide.GetLibrary<Covalence>();

		protected Lang lang = Interface.Oxide.GetLibrary<Lang>();

		protected Oxide.Core.Libraries.Plugins plugins = Interface.Oxide.GetLibrary<Oxide.Core.Libraries.Plugins>();

		protected Permission permission = Interface.Oxide.GetLibrary<Permission>();

		protected WebRequests webrequest = Interface.Oxide.GetLibrary<WebRequests>();

		protected PluginTimers timer;

		protected HashSet<PluginFieldInfo> onlinePlayerFields = new HashSet<PluginFieldInfo>();

		private Dictionary<string, MemberInfo> pluginReferenceMembers = new Dictionary<string, MemberInfo>();

		private bool hookDispatchFallback;

		private static readonly object _logFileLock = new object();

		public bool HookedOnFrame { get; private set; }

		public CSharpPlugin()
		{
			timer = new PluginTimers(this);
			Type type = GetType();
			MemberInfo[] members = type.GetMembers(BindingFlags.Instance | BindingFlags.NonPublic);
			foreach (MemberInfo memberInfo in members)
			{
				if (memberInfo.MemberType == MemberTypes.Property || memberInfo.MemberType == MemberTypes.Field)
				{
					if (memberInfo.MemberType != MemberTypes.Property || (memberInfo as PropertyInfo).CanWrite)
					{
						object[] customAttributes = memberInfo.GetCustomAttributes(typeof(PluginReferenceAttribute), inherit: true);
						if (customAttributes.Length != 0)
						{
							PluginReferenceAttribute pluginReferenceAttribute = customAttributes[0] as PluginReferenceAttribute;
							pluginReferenceMembers[pluginReferenceAttribute.Name ?? memberInfo.Name] = memberInfo;
						}
					}
				}
				else
				{
					if (memberInfo.MemberType != MemberTypes.Method)
					{
						continue;
					}
					MethodInfo methodInfo = memberInfo as MethodInfo;
					if (methodInfo.GetCustomAttributes(typeof(HookMethodAttribute), inherit: true).Length == 0)
					{
						if (methodInfo.Name.Equals("OnFrame"))
						{
							HookedOnFrame = true;
						}
						if (methodInfo.DeclaringType.Name == type.Name)
						{
							AddHookMethod(methodInfo.Name, methodInfo);
						}
					}
				}
			}
		}

		public virtual bool SetPluginInfo(string name, string path)
		{
			base.Name = name;
			base.Filename = path;
			object[] customAttributes = GetType().GetCustomAttributes(typeof(InfoAttribute), inherit: true);
			if (customAttributes.Length != 0)
			{
				InfoAttribute infoAttribute = customAttributes[0] as InfoAttribute;
				base.Title = infoAttribute.Title;
				base.Author = infoAttribute.Author;
				base.Version = infoAttribute.Version;
				base.ResourceId = infoAttribute.ResourceId;
				object[] customAttributes2 = GetType().GetCustomAttributes(typeof(DescriptionAttribute), inherit: true);
				if (customAttributes2.Length != 0)
				{
					DescriptionAttribute descriptionAttribute = customAttributes2[0] as DescriptionAttribute;
					base.Description = descriptionAttribute.Description;
				}
				MethodInfo method = GetType().GetMethod("LoadDefaultConfig", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				base.HasConfig = method.DeclaringType != typeof(Plugin);
				MethodInfo method2 = GetType().GetMethod("LoadDefaultMessages", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				base.HasMessages = method2.DeclaringType != typeof(Plugin);
				return true;
			}
			Interface.Oxide.LogWarning("Failed to load " + name + ": Info attribute missing");
			return false;
		}

		public override void HandleAddedToManager(PluginManager manager)
		{
			base.HandleAddedToManager(manager);
			if (base.Filename != null)
			{
				Watcher.AddMapping(base.Name);
			}
			base.Manager.OnPluginAdded += OnPluginLoaded;
			base.Manager.OnPluginRemoved += OnPluginUnloaded;
			foreach (KeyValuePair<string, MemberInfo> pluginReferenceMember in pluginReferenceMembers)
			{
				if (pluginReferenceMember.Value.MemberType == MemberTypes.Property)
				{
					((PropertyInfo)pluginReferenceMember.Value).SetValue(this, manager.GetPlugin(pluginReferenceMember.Key), null);
				}
				else
				{
					((FieldInfo)pluginReferenceMember.Value).SetValue(this, manager.GetPlugin(pluginReferenceMember.Key));
				}
			}
			try
			{
				OnCallHook("Loaded", null);
			}
			catch (Exception ex)
			{
				Interface.Oxide.LogException($"Failed to initialize plugin '{base.Name} v{base.Version}'", ex);
				base.Loader.PluginErrors[base.Name] = ex.Message;
			}
		}

		public override void HandleRemovedFromManager(PluginManager manager)
		{
			if (base.IsLoaded)
			{
				CallHook("Unload", null);
			}
			Watcher.RemoveMapping(base.Name);
			base.Manager.OnPluginAdded -= OnPluginLoaded;
			base.Manager.OnPluginRemoved -= OnPluginUnloaded;
			foreach (KeyValuePair<string, MemberInfo> pluginReferenceMember in pluginReferenceMembers)
			{
				if (pluginReferenceMember.Value.MemberType == MemberTypes.Property)
				{
					((PropertyInfo)pluginReferenceMember.Value).SetValue(this, null, null);
				}
				else
				{
					((FieldInfo)pluginReferenceMember.Value).SetValue(this, null);
				}
			}
			base.HandleRemovedFromManager(manager);
		}

		public virtual bool DirectCallHook(string name, out object ret, object[] args)
		{
			ret = null;
			return false;
		}

		protected override object InvokeMethod(HookMethod method, object[] args)
		{
			if (!hookDispatchFallback && !method.IsBaseHook)
			{
				if (args != null && args.Length != 0)
				{
					ParameterInfo[] parameters = method.Parameters;
					for (int i = 0; i < args.Length; i++)
					{
						object obj = args[i];
						if (obj == null)
						{
							continue;
						}
						Type parameterType = parameters[i].ParameterType;
						if (parameterType.IsValueType)
						{
							Type type = obj.GetType();
							if (parameterType != typeof(object) && type != parameterType)
							{
								args[i] = Convert.ChangeType(obj, parameterType);
							}
						}
					}
				}
				try
				{
					if (DirectCallHook(method.Name, out var ret, args))
					{
						return ret;
					}
					Interface.Oxide.RootLogger.WriteDebug(LogType.Error, LogEvent.HookCall, base.Name, "DirectCallHook method is not patched, falling back to reflection based dispatch.");
					hookDispatchFallback = true;
				}
				catch (InvalidProgramException ex)
				{
					Interface.Oxide.LogError("Hook dispatch failure detected, falling back to reflection based dispatch. " + ex);
					CompilablePlugin compilablePlugin = CSharpPluginLoader.GetCompilablePlugin(Interface.Oxide.PluginDirectory, base.Name);
					if (compilablePlugin?.CompiledAssembly != null)
					{
						File.WriteAllBytes(Interface.Oxide.PluginDirectory + "\\" + base.Name + ".dump", compilablePlugin.CompiledAssembly.RawAssembly);
						Interface.Oxide.LogWarning("The invalid raw assembly has been dumped to Plugins/" + base.Name + ".dump");
					}
					hookDispatchFallback = true;
				}
			}
			return method.Method.Invoke(this, args);
		}

		public void SetFailState(string reason)
		{
			throw new PluginLoadFailure(reason);
		}

		private void OnPluginLoaded(Plugin plugin)
		{
			if (pluginReferenceMembers.TryGetValue(plugin.Name, out var value))
			{
				if (value.MemberType == MemberTypes.Property)
				{
					((PropertyInfo)value).SetValue(this, plugin, null);
				}
				else
				{
					((FieldInfo)value).SetValue(this, plugin);
				}
			}
		}

		private void OnPluginUnloaded(Plugin plugin)
		{
			if (pluginReferenceMembers.TryGetValue(plugin.Name, out var value))
			{
				if (value.MemberType == MemberTypes.Property)
				{
					((PropertyInfo)value).SetValue(this, null, null);
				}
				else
				{
					((FieldInfo)value).SetValue(this, null);
				}
			}
		}

		protected void Puts(string format, params object[] args)
		{
			Interface.Oxide.LogInfo("[{0}] {1}", base.Title, (args.Length != 0) ? string.Format(format, args) : format);
		}

		protected void PrintWarning(string format, params object[] args)
		{
			Interface.Oxide.LogWarning("[{0}] {1}", base.Title, (args.Length != 0) ? string.Format(format, args) : format);
		}

		protected void PrintError(string format, params object[] args)
		{
			Interface.Oxide.LogError("[{0}] {1}", base.Title, (args.Length != 0) ? string.Format(format, args) : format);
		}

		protected void LogToFile(string filename, string text, Plugin plugin, bool datedFilename = true, bool timestampPrefix = false)
		{
			string text2 = Path.Combine(Interface.Oxide.LogDirectory, plugin.Name);
			if (!Directory.Exists(text2))
			{
				Directory.CreateDirectory(text2);
			}
			filename = plugin.Name.ToLower() + "_" + filename.ToLower() + (datedFilename ? $"-{DateTime.Now:yyyy-MM-dd}" : "") + ".txt";
			lock (_logFileLock)
			{
				using FileStream stream = new FileStream(Path.Combine(text2, Utility.CleanPath(filename)), FileMode.Append, FileAccess.Write, FileShare.Read);
				using StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8);
				streamWriter.WriteLine(timestampPrefix ? $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {text}" : text);
			}
		}

		protected void NextFrame(Action callback)
		{
			Interface.Oxide.NextTick(callback);
		}

		protected void NextTick(Action callback)
		{
			Interface.Oxide.NextTick(callback);
		}

		protected void QueueWorkerThread(Action<object> callback)
		{
			ThreadPool.QueueUserWorkItem(delegate(object context)
			{
				try
				{
					callback(context);
				}
				catch (Exception arg)
				{
					RaiseError($"Exception in '{base.Name} v{base.Version}' plugin worker thread: {arg}");
				}
			});
		}
	}
	public class CSharpPluginLoader : PluginLoader
	{
		public static string[] DefaultReferences = new string[8] { "mscorlib", "Oxide.Core", "Oxide.CSharp", "Oxide.Common", "System", "System.Core", "System.Data", "System.Xml" };

		public static HashSet<string> PluginReferences = new HashSet<string>(DefaultReferences);

		public static CSharpPluginLoader Instance;

		private static CSharpExtension extension;

		private static Dictionary<string, CompilablePlugin> plugins = new Dictionary<string, CompilablePlugin>();

		private static readonly string[] AssemblyBlacklist = new string[3] { "Newtonsoft.Json", "protobuf-net", "websocket-sharp" };

		private List<CompilablePlugin> compilationQueue = new List<CompilablePlugin>();

		private CompilerService compiler;

		private Oxide.Core.Libraries.Timer timer { get; } = Interface.Oxide.GetLibrary<Oxide.Core.Libraries.Timer>();

		public override string FileExtension => ".cs";

		public static CompilablePlugin GetCompilablePlugin(string directory, string name)
		{
			string key = Regex.Replace(name, "_", "");
			if (!plugins.TryGetValue(key, out var value))
			{
				value = new CompilablePlugin(extension, Instance, directory, name);
				plugins[key] = value;
			}
			return value;
		}

		public CSharpPluginLoader(CSharpExtension extension)
		{
			Instance = this;
			CSharpPluginLoader.extension = extension;
			compiler = new CompilerService(extension);
		}

		public void OnModLoaded()
		{
			compiler.Precheck();
			foreach (Extension allExtension in Interface.Oxide.GetAllExtensions())
			{
				if (allExtension == null || (!allExtension.IsCoreExtension && !allExtension.IsGameExtension))
				{
					continue;
				}
				Assembly assembly = allExtension.GetType().Assembly;
				string name = assembly.GetName().Name;
				if (AssemblyBlacklist.Contains(name))
				{
					continue;
				}
				PluginReferences.Add(name);
				AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
				foreach (AssemblyName assemblyName in referencedAssemblies)
				{
					if (assemblyName != null)
					{
						PluginReferences.Add(assemblyName.Name);
					}
				}
			}
		}

		public override IEnumerable<string> ScanDirectory(string directory)
		{
			if (!compiler.Installed)
			{
				yield break;
			}
			IEnumerable<string> enumerable = base.ScanDirectory(directory);
			foreach (string item in enumerable)
			{
				yield return item;
			}
		}

		public override Plugin Load(string directory, string name)
		{
			CompilablePlugin compilablePlugin = GetCompilablePlugin(directory, name);
			if (compilablePlugin.IsLoading)
			{
				Interface.Oxide.RootLogger.WriteDebug(LogType.Warning, LogEvent.Compile, "CSharp", "Load requested for plugin which is already loading: " + compilablePlugin.Name);
				return null;
			}
			if (LoadedPlugins.ContainsKey(compilablePlugin.Name))
			{
				timer.Once(0.5f, delegate
				{
					Load(compilablePlugin);
				});
			}
			else
			{
				Load(compilablePlugin);
			}
			return null;
		}

		public override void Reload(string directory, string name)
		{
			if (Regex.Match(directory, "\\\\include\\b", RegexOptions.IgnoreCase).Success)
			{
				name = "Oxide." + name;
				{
					foreach (CompilablePlugin value in plugins.Values)
					{
						if (value.References.Contains(name))
						{
							Interface.Oxide.LogInfo("Reloading " + value.Name + " because it references updated include file: " + name);
							value.LastModifiedAt = DateTime.Now;
							Load(value);
						}
					}
					return;
				}
			}
			CompilablePlugin compilablePlugin = GetCompilablePlugin(directory, name);
			if (compilablePlugin.IsLoading)
			{
				Interface.Oxide.RootLogger.WriteDebug(LogType.Warning, LogEvent.Compile, "CSharp", "Reload requested for plugin which is already loading: " + compilablePlugin.Name);
			}
			else
			{
				Load(compilablePlugin);
			}
		}

		public override void Unloading(Plugin pluginBase)
		{
			if (!(pluginBase is CSharpPlugin cSharpPlugin))
			{
				return;
			}
			LoadedPlugins.Remove(cSharpPlugin.Name);
			foreach (CompilablePlugin value in plugins.Values)
			{
				if (value.Requires.Contains(cSharpPlugin.Name))
				{
					Interface.Oxide.UnloadPlugin(value.Name);
				}
			}
		}

		public void Load(CompilablePlugin plugin)
		{
			PluginLoadingStarted(plugin);
			plugin.Compile(delegate(bool compiled)
			{
				if (!compiled)
				{
					PluginLoadingCompleted(plugin);
				}
				else
				{
					foreach (string item in plugin.Requires.Where((string r) => LoadedPlugins.ContainsKey(r) && base.LoadingPlugins.Contains(r)))
					{
						Interface.Oxide.UnloadPlugin(item);
					}
					IEnumerable<string> enumerable = plugin.Requires.Where((string r) => !LoadedPlugins.ContainsKey(r));
					if (enumerable.Any())
					{
						IEnumerable<string> enumerable2 = plugin.Requires.Where((string r) => base.LoadingPlugins.Contains(r));
						if (enumerable2.Any())
						{
							Interface.Oxide.RootLogger.WriteDebug(LogType.Info, LogEvent.Compile, "CSharp", plugin.Name + " plugin is waiting for requirements to be loaded: " + enumerable2.ToSentence());
						}
						else
						{
							Interface.Oxide.LogError(plugin.Name + " plugin requires missing dependencies: " + enumerable.ToSentence());
							base.PluginErrors[plugin.Name] = "Missing dependencies: " + enumerable.ToSentence();
							PluginLoadingCompleted(plugin);
						}
					}
					else
					{
						Interface.Oxide.UnloadPlugin(plugin.Name);
						plugin.LoadPlugin(delegate(CSharpPlugin pl)
						{
							if (pl != null)
							{
								LoadedPlugins[pl.Name] = pl;
							}
							PluginLoadingCompleted(plugin);
						});
					}
				}
			});
		}

		public void CompilationRequested(CompilablePlugin plugin)
		{
			if (Compilation.Current != null)
			{
				Compilation.Current.Add(plugin);
				return;
			}
			if (compilationQueue.Count < 1)
			{
				Interface.Oxide.NextTick(delegate
				{
					CompileAssembly(compilationQueue.ToArray());
					compilationQueue.Clear();
				});
			}
			compilationQueue.Add(plugin);
		}

		public void PluginLoadingStarted(CompilablePlugin plugin)
		{
			if (!base.LoadingPlugins.Contains(plugin.Name))
			{
				base.LoadingPlugins.Add(plugin.Name);
			}
			plugin.IsLoading = true;
		}

		private void PluginLoadingCompleted(CompilablePlugin plugin)
		{
			base.LoadingPlugins.Remove(plugin.Name);
			plugin.IsLoading = false;
			string[] array = base.LoadingPlugins.ToArray();
			foreach (string name in array)
			{
				CompilablePlugin compilablePlugin = GetCompilablePlugin(plugin.Directory, name);
				if (compilablePlugin.IsLoading && compilablePlugin.Requires.Contains(plugin.Name))
				{
					Load(compilablePlugin);
				}
			}
		}

		private void CompileAssembly(CompilablePlugin[] plugins)
		{
			compiler.Compile(plugins, delegate(Compilation compilation)
			{
				if (compilation.compiledAssembly == null)
				{
					foreach (CompilablePlugin plugin in compilation.plugins)
					{
						plugin.OnCompilationFailed();
						base.PluginErrors[plugin.Name] = "Failed to compile: " + plugin.CompilerErrors;
						Interface.Oxide.LogError("Error while compiling " + plugin.ScriptName + ": " + plugin.CompilerErrors);
					}
					return;
				}
				if (compilation.plugins.Count > 0)
				{
					string[] array = (from pl in compilation.plugins
						where string.IsNullOrEmpty(pl.CompilerErrors)
						select pl.Name).ToArray();
					string arg = ((array.Length > 1) ? "were" : "was");
					Interface.Oxide.LogInfo($"{array.ToSentence()} {arg} compiled successfully in {Math.Round(compilation.duration * 1000f)}ms");
				}
				foreach (CompilablePlugin plugin2 in compilation.plugins)
				{
					if (plugin2.CompilerErrors == null)
					{
						Interface.Oxide.UnloadPlugin(plugin2.Name);
						plugin2.OnCompilationSucceeded(compilation.compiledAssembly);
					}
					else
					{
						plugin2.OnCompilationFailed();
						base.PluginErrors[plugin2.Name] = "Failed to compile: " + plugin2.CompilerErrors;
						Interface.Oxide.LogError("Error while compiling " + plugin2.ScriptName + ": " + plugin2.CompilerErrors);
					}
				}
			});
		}

		public void OnShutdown()
		{
			compiler.Stop(synchronous: true, "framework shutting down");
		}
	}
	public class Timer
	{
		private Oxide.Core.Libraries.Timer.TimerInstance instance;

		public int Repetitions => instance.Repetitions;

		public float Delay => instance.Delay;

		public Action Callback => instance.Callback;

		public bool Destroyed => instance.Destroyed;

		public Plugin Owner => instance.Owner;

		public Timer(Oxide.Core.Libraries.Timer.TimerInstance instance)
		{
			this.instance = instance;
		}

		public void Reset(float delay = -1f, int repetitions = 1)
		{
			instance.Reset(delay, repetitions);
		}

		public void Destroy()
		{
			instance.Destroy();
		}

		public void DestroyToPool()
		{
			instance.DestroyToPool();
		}
	}
	public class PluginTimers
	{
		private Oxide.Core.Libraries.Timer timer = Interface.Oxide.GetLibrary<Oxide.Core.Libraries.Timer>("Timer");

		private Plugin plugin;

		public PluginTimers(Plugin plugin)
		{
			this.plugin = plugin;
		}

		public Timer Once(float seconds, Action callback)
		{
			return new Timer(timer.Once(seconds, callback, plugin));
		}

		public Timer In(float seconds, Action callback)
		{
			return new Timer(timer.Once(seconds, callback, plugin));
		}

		public Timer Every(float interval, Action callback)
		{
			return new Timer(timer.Repeat(interval, -1, callback, plugin));
		}

		public Timer Repeat(float interval, int repeats, Action callback)
		{
			return new Timer(timer.Repeat(interval, repeats, callback, plugin));
		}

		public void Destroy(ref Timer timer)
		{
			timer?.DestroyToPool();
			timer = null;
		}
	}
	public class Hash<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		private readonly IDictionary<TKey, TValue> dictionary;

		public TValue this[TKey key]
		{
			get
			{
				if (TryGetValue(key, out var value))
				{
					return value;
				}
				if (typeof(TValue).IsValueType)
				{
					return (TValue)Activator.CreateInstance(typeof(TValue));
				}
				return default(TValue);
			}
			set
			{
				if (value == null)
				{
					dictionary.Remove(key);
				}
				else
				{
					dictionary[key] = value;
				}
			}
		}

		public ICollection<TKey> Keys => dictionary.Keys;

		public ICollection<TValue> Values => dictionary.Values;

		public int Count => dictionary.Count;

		public bool IsReadOnly => dictionary.IsReadOnly;

		public Hash()
		{
			dictionary = new Dictionary<TKey, TValue>();
		}

		public Hash(IEqualityComparer<TKey> comparer)
		{
			dictionary = new Dictionary<TKey, TValue>(comparer);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return dictionary.GetEnumerator();
		}

		public bool ContainsKey(TKey key)
		{
			return dictionary.ContainsKey(key);
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return dictionary.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			dictionary.CopyTo(array, index);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return dictionary.TryGetValue(key, out value);
		}

		public void Add(TKey key, TValue value)
		{
			dictionary.Add(key, value);
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			dictionary.Add(item);
		}

		public bool Remove(TKey key)
		{
			return dictionary.Remove(key);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return dictionary.Remove(item);
		}

		public void Clear()
		{
			dictionary.Clear();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
namespace Oxide.CSharp
{
	internal class AssemblyResolver : DefaultAssemblyResolver
	{
		internal readonly AssemblyDefinition mscorlib;

		public AssemblyResolver()
		{
			AddSearchDirectory(Interface.Oxide.ExtensionDirectory);
			mscorlib = AssemblyDefinition.ReadAssembly(Path.Combine(Interface.Oxide.ExtensionDirectory, "mscorlib.dll"));
		}

		public override AssemblyDefinition Resolve(AssemblyNameReference name, ReaderParameters parameters)
		{
			if (name.Name == "System.Private.CoreLib")
			{
				Interface.Oxide.RootLogger.WriteDebug(LogType.Warning, new LogEvent(50, "Resolve"), "Resolver", "Redirecting reference to System.Private.CoreLib to mscorlib");
				return mscorlib;
			}
			return base.Resolve(name, parameters);
		}
	}
	internal class CompilerService
	{
		private static readonly Regex SymbolEscapeRegex = new Regex("[^\\w\\d]", RegexOptions.Compiled);

		private const string baseUrl = "https://downloads.oxidemod.com/artifacts/Oxide.Compiler/{0}/";

		private Hash<int, Compilation> compilations;

		private Queue<CompilerMessage> messageQueue;

		private Process process;

		private volatile int lastId;

		private volatile bool ready;

		private Oxide.Core.Libraries.Timer.TimerInstance idleTimer;

		private ObjectStreamClient<CompilerMessage> client;

		private string filePath;

		private string remoteName;

		private string compilerBasicArguments = "-unsafe true --setting:Force true -ms true";

		private static Regex fileErrorRegex = new Regex("^\\[(?'Severity'\\S+)\\]\\[(?'Code'\\S+)\\]\\[(?'File'\\S+)\\] (?'Message'.+)$", RegexOptions.Compiled);

		private float startTime;

		private string[] preprocessor;

		public bool Installed => File.Exists(filePath);

		public CompilerService(Extension extension)
		{
			compilations = new Hash<int, Compilation>();
			messageQueue = new Queue<CompilerMessage>();
			string text = ((IntPtr.Size == 8) ? "x64" : "x86");
			filePath = Path.Combine(Interface.Oxide.RootDirectory, "Oxide.Compiler");
			string text2 = $"https://downloads.oxidemod.com/artifacts/Oxide.Compiler/{extension.Branch}/";
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.Win32NT:
				filePath += ".exe";
				remoteName = text2 + "win-" + text + ".Compiler.exe";
				break;
			case PlatformID.MacOSX:
				remoteName = text2 + "osx-x64.Compiler";
				break;
			case PlatformID.Unix:
				remoteName = text2 + "linux-x64.Compiler";
				break;
			}
			EnvironmentHelper.SetVariable("Path:Root", Interface.Oxide.RootDirectory);
			EnvironmentHelper.SetVariable("Path:Logging", Interface.Oxide.LogDirectory);
			EnvironmentHelper.SetVariable("Path:Plugins", Interface.Oxide.PluginDirectory);
			EnvironmentHelper.SetVariable("Path:Configuration", Interface.Oxide.ConfigDirectory);
			EnvironmentHelper.SetVariable("Path:Data", Interface.Oxide.DataDirectory);
			EnvironmentHelper.SetVariable("Path:Libraries", Interface.Oxide.ExtensionDirectory);
			if (Interface.Oxide.Config.Compiler.Publicize == true)
			{
				EnvironmentHelper.SetVariable("AllowPublicize", "true", throwOnExisting: false, force: true);
			}
		}

		private void ExpireFileCache()
		{
			lock (CompilerFile.FileCache)
			{
				object[] array = ArrayPool.Get(CompilerFile.FileCache.Count);
				int num = 0;
				foreach (KeyValuePair<string, CompilerFile> item in CompilerFile.FileCache)
				{
					if (!item.Value.KeepCached)
					{
						array[num] = item.Key;
						num++;
					}
				}
				for (int i = 0; i < num; i++)
				{
					string text = (string)array[i];
					Log(LogType.Info, "Removing cached dependency " + Path.GetFileName(text));
					CompilerFile.FileCache.Remove(text);
				}
				ArrayPool.Free(array);
			}
		}

		internal bool Precheck()
		{
			List<string> list = new List<string> { "OXIDE", "OXIDEMOD" };
			Extension extension = Interface.Oxide.GetAllExtensions().SingleOrDefault((Extension e) => e.IsGameExtension);
			if (extension != null)
			{
				string text = extension.Name.ToUpperInvariant();
				string text2 = extension.Branch?.ToUpperInvariant() ?? "PUBLIC";
				list.Add(EscapeSymbolName(text));
				list.Add(EscapeSymbolName(text + "_" + text2));
				if (extension.Version != default(VersionNumber))
				{
					list.Add(EscapeSymbolName(text + "_" + extension.Version.ToString()));
					list.Add(EscapeSymbolName(text + "_" + extension.Version.ToString() + "_" + text2));
				}
			}
			foreach (Extension allExtension in Interface.Oxide.GetAllExtensions())
			{
				try
				{
					string text3 = allExtension.Name.ToUpper() + "_EXT";
					foreach (string preprocessorDirective in allExtension.GetPreprocessorDirectives())
					{
						if (!allExtension.IsGameExtension && !allExtension.IsCoreExtension && !preprocessorDirective.StartsWith(text3))
						{
							Interface.Oxide.LogWarning("Missing extension preprocessor prefix '{0}' for directive '{1}' (by extension '{2}')", text3, preprocessorDirective, allExtension.Name);
						}
						list.Add(EscapeSymbolName(preprocessorDirective));
					}
				}
				catch (Exception ex)
				{
					Interface.Oxide.LogException("An error occurred processing preprocessor directives for extension `" + allExtension.Name + "`", ex);
				}
			}
			if (Interface.Oxide.Config.Compiler.PreprocessorDirectives.Count > 0)
			{
				list.AddRange(Interface.Oxide.Config.Compiler.PreprocessorDirectives);
			}
			if (Interface.Oxide.Config.Compiler.Publicize == true)
			{
				EnvironmentHelper.SetVariable("AllowPublicize", "true", throwOnExisting: false, force: true);
				list.Add("OXIDE_PUBLICIZED");
			}
			preprocessor = list.Distinct().ToArray();
			if (!DownloadFile(remoteName, filePath))
			{
				return false;
			}
			return SetFilePermissions(filePath);
		}

		private bool Start()
		{
			if (filePath == null)
			{
				return false;
			}
			if (process != null && process.Handle != IntPtr.Zero && !process.HasExited)
			{
				return true;
			}
			try
			{
				int num = 0;
				while (!File.Exists(filePath))
				{
					num++;
					if (num > 3)
					{
						throw new IOException("Compiler failed to download after 3 attempts");
					}
					Log(LogType.Error, $"Compiler doesn't exist at {filePath}, attempting to download again | Attempt: {num} of 3");
					Precheck();
					Thread.Sleep(100);
				}
			}
			catch (Exception ex)
			{
				Log(LogType.Error, ex.Message);
				return false;
			}
			Stop(synchronous: false, "starting new process");
			startTime = Interface.Oxide.Now;
			string text = compilerBasicArguments + $" --parent {Process.GetCurrentProcess().Id} -l:file \"{Path.Combine(Interface.Oxide.LogDirectory, $"oxide.compiler_{DateTime.Now:yyyy-MM-dd}.log")}\"";
			Log(LogType.Info, "Starting compiler with parameters: " + text);
			try
			{
				process = new Process
				{
					StartInfo = 
					{
						FileName = filePath,
						CreateNoWindow = true,
						UseShellExecute = false,
						RedirectStandardInput = true,
						RedirectStandardOutput = true,
						Arguments = text
					},
					EnableRaisingEvents = true
				};
				process.Exited += OnProcessExited;
				process.Start();
			}
			catch (Exception ex2)
			{
				process?.Dispose();
				process = null;
				Interface.Oxide.LogException("Exception while starting compiler", ex2);
				if (filePath.Contains("'"))
				{
					Interface.Oxide.LogError("Server directory path contains an apostrophe, compiler will not work until path is renamed");
				}
				else if (Environment.OSVersion.Platform == PlatformID.Unix)
				{
					Interface.Oxide.LogError("Compiler may not be set as executable; chmod +x or 0744/0755 required");
				}
				if (ex2.GetBaseException() != ex2)
				{
					Interface.Oxide.LogException("BaseException: ", ex2.GetBaseException());
				}
				if (ex2 is Win32Exception ex3)
				{
					Interface.Oxide.LogError($"Win32 NativeErrorCode: {ex3.NativeErrorCode} ErrorCode: {ex3.ErrorCode} HelpLink: {ex3.HelpLink}");
				}
			}
			if (process == null)
			{
				return false;
			}
			client = new ObjectStreamClient<CompilerMessage>(process.StandardOutput.BaseStream, process.StandardInput.BaseStream);
			client.Message += OnMessage;
			client.Error += OnError;
			client.Start();
			ResetIdleTimer();
			Interface.Oxide.LogInfo("[CSharp] Started Oxide.Compiler v" + GetCompilerVersion() + " successfully");
			return true;
		}

		internal void Stop(bool synchronous, string reason)
		{
			ready = false;
			Process endedProcess = process;
			ObjectStreamClient<CompilerMessage> stream = client;
			if (endedProcess == null || stream == null)
			{
				return;
			}
			process = null;
			client = null;
			endedProcess.Exited -= OnProcessExited;
			endedProcess.Refresh();
			stream.Message -= OnMessage;
			stream.Error -= OnError;
			if (!string.IsNullOrEmpty(reason))
			{
				Interface.Oxide.LogInfo("Shutting down compiler because " + reason);
			}
			if (!endedProcess.HasExited)
			{
				stream.PushMessage(new CompilerMessage
				{
					Type = CompilerMessageType.Exit
				});
				if (synchronous)
				{
					if (endedProcess.WaitForExit(10000))
					{
						Interface.Oxide.LogInfo("Compiler shutdown completed");
					}
					else
					{
						Interface.Oxide.LogWarning("Compiler failed to gracefully shutdown, killing the process...");
						endedProcess.Kill();
					}
					stream.Stop();
					stream = null;
					endedProcess.Close();
				}
				else
				{
					ThreadPool.QueueUserWorkItem(delegate
					{
						if (endedProcess.WaitForExit(10000))
						{
							Interface.Oxide.LogInfo("Compiler shutdown completed");
						}
						else
						{
							Interface.Oxide.LogWarning("Compiler failed to gracefully shutdown, killing the process...");
							endedProcess.Kill();
						}
						stream.Stop();
						stream = null;
						endedProcess.Close();
					});
				}
			}
			else
			{
				stream.Stop();
				stream = null;
				endedProcess.Close();
				Log(LogType.Info, "Released compiler resources");
			}
			ExpireFileCache();
		}

		private void OnMessage(ObjectStreamConnection<CompilerMessage, CompilerMessage> connection, CompilerMessage message)
		{
			if (message == null)
			{
				return;
			}
			switch (message.Type)
			{
			case CompilerMessageType.Assembly:
			{
				Compilation compilation = compilations[message.Id];
				if (compilation == null)
				{
					Log(LogType.Error, "Compiler compiled an unknown assembly");
					return;
				}
				compilation.endedAt = Interface.Oxide.Now;
				string text2 = (string)message.ExtraData;
				if (text2 != null)
				{
					string[] array = text2.Split(new char[2] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
					foreach (string text3 in array)
					{
						Match match = fileErrorRegex.Match(text3.Trim());
						if (!match.Success || match.Groups["Severity"].Value != "Error")
						{
							continue;
						}
						string value = match.Groups["File"].Value;
						string scriptName = Path.GetFileNameWithoutExtension(value);
						string value2 = match.Groups["Message"].Value;
						CompilablePlugin compilablePlugin = compilation.plugins.SingleOrDefault((CompilablePlugin pl) => pl.ScriptName == scriptName);
						if (compilablePlugin == null)
						{
							Interface.Oxide.LogError("Unable to resolve script error to " + value + ": " + value2);
							continue;
						}
						IEnumerable<string> source = compilablePlugin.Requires.Where((string name) => !compilation.IncludesRequiredPlugin(name));
						if (source.Any())
						{
							compilablePlugin.CompilerErrors = "Missing dependencies: " + string.Join(",", source.ToArray());
							Log(LogType.Error, "[" + match.Groups["Severity"].Value + "][" + scriptName + "] Missing dependencies: " + string.Join(",", source.ToArray()));
						}
						else
						{
							string text4 = value2.Trim();
							string pluginDirectory = Interface.Oxide.PluginDirectory;
							char directorySeparatorChar = Path.DirectorySeparatorChar;
							compilablePlugin.CompilerErrors = text4.Replace(pluginDirectory + directorySeparatorChar, string.Empty);
						}
					}
				}
				CompilationResult compilationResult = (CompilationResult)message.Data;
				if (compilationResult.Data == null || compilationResult.Data.Length == 0)
				{
					compilation.Completed();
				}
				else
				{
					compilation.Completed(compilationResult.Data, compilationResult.Symbols);
				}
				compilations.Remove(message.Id);
				break;
			}
			case CompilerMessageType.Error:
			{
				Exception ex = (Exception)message.Data;
				Compilation compilation2 = compilations[message.Id];
				compilations.Remove(message.Id);
				if (compilation2 == null)
				{
					Interface.Oxide.LogException("Compiler returned a error for a untracked compilation", ex);
					return;
				}
				foreach (CompilablePlugin plugin in compilation2.plugins)
				{
					plugin.CompilerErrors = ex.Message;
				}
				compilation2.Completed();
				break;
			}
			case CompilerMessageType.Ready:
			{
				string text = $"Ready signal received from compiler (Startup took: {Math.Round((Interface.Oxide.Now - startTime) * 1000f)}ms)";
				switch (messageQueue.Count)
				{
				case 0:
					Log(LogType.Info, text);
					break;
				case 1:
					Log(LogType.Info, text + ", sending compilation. . .");
					break;
				default:
					Log(LogType.Info, text + $", sending {messageQueue.Count} compilations. . .");
					break;
				}
				connection.PushMessage(message);
				if (!ready)
				{
					ready = true;
					while (messageQueue.Count > 0)
					{
						CompilerMessage compilerMessage = messageQueue.Dequeue();
						compilations[compilerMessage.Id].startedAt = Interface.Oxide.Now;
						connection.PushMessage(compilerMessage);
					}
				}
				break;
			}
			}
			Interface.Oxide.NextTick(delegate
			{
				ResetIdleTimer();
			});
		}

		private void OnError(Exception exception)
		{
			OnCompilerFailed($"Compiler threw a error: {exception}");
		}

		private void OnProcessExited(object sender, EventArgs eventArgs)
		{
			Interface.Oxide.NextTick(delegate
			{
				OnCompilerFailed("compiler was closed unexpectedly");
				string environmentVariable = Environment.GetEnvironmentVariable("PATH");
				string text = Path.Combine(Interface.Oxide.ExtensionDirectory, ".dotnet");
				if (string.IsNullOrEmpty(environmentVariable) || !environmentVariable.Contains(text))
				{
					Log(LogType.Warning, "PATH does not contain path to compiler dependencies: " + text);
				}
				else
				{
					Log(LogType.Warning, "User running server may not have the proper permissions or install is missing files");
				}
				Stop(synchronous: false, "process exited");
			});
		}

		private void ResetIdleTimer()
		{
			if (idleTimer != null)
			{
				idleTimer.Destroy();
			}
			if (Interface.Oxide.Config.Compiler.IdleShutdown)
			{
				idleTimer = Interface.Oxide.GetLibrary<Oxide.Core.Libraries.Timer>().Once(Interface.Oxide.Config.Compiler.IdleTimeout, delegate
				{
					Stop(synchronous: false, "idle shutdown");
				});
			}
		}

		internal void Compile(CompilablePlugin[] plugins, Action<Compilation> callback)
		{
			int num = lastId++;
			Compilation compilation = new Compilation(num, callback, plugins);
			compilations[num] = compilation;
			compilation.Prepare(delegate
			{
				EnqueueCompilation(compilation);
			});
		}

		internal void OnCompileTimeout()
		{
			Stop(synchronous: false, "compiler timeout");
		}

		private void EnqueueCompilation(Compilation compilation)
		{
			if (compilation.plugins.Count < 1)
			{
				return;
			}
			if ((!Installed && !Precheck()) || !Start())
			{
				OnCompilerFailed("compiler couldn't be started");
				Stop(synchronous: false, "failed to start");
				return;
			}
			compilation.Started();
			HashSet<string> hashSet = new HashSet<string>();
			List<CompilerFile> list = new List<CompilerFile>();
			foreach (CompilablePlugin plugin in compilation.plugins)
			{
				string fileName = Path.GetFileName(plugin.ScriptPath ?? plugin.ScriptName);
				if (plugin.ScriptSource == null || plugin.ScriptSource.Length == 0)
				{
					plugin.CompilerErrors = "No data contained in .cs file";
					Log(LogType.Error, "Ignoring plugin " + fileName + ", file is empty");
					continue;
				}
				foreach (string item in plugin.IncludePaths.Distinct())
				{
					if (hashSet.Contains(item))
					{
						Interface.Oxide.LogWarning("Tried to include " + item + " but it has already been added to the compilation");
						continue;
					}
					CompilerFile compilerFile = new CompilerFile(item);
					if (compilerFile.Data == null || compilerFile.Data.Length == 0)
					{
						Interface.Oxide.LogWarning("Ignoring plugin " + compilerFile.Name + ", file is empty");
						continue;
					}
					Interface.Oxide.LogWarning("Adding " + compilerFile.Name + " to compilation project");
					list.Add(compilerFile);
					hashSet.Add(item);
				}
				Log(LogType.Info, "Adding plugin " + fileName + " to compilation project");
				list.Add(new CompilerFile(plugin.ScriptPath ?? plugin.ScriptName, plugin.ScriptSource));
			}
			if (list.Count == 0)
			{
				Interface.Oxide.LogError("Compilation job contained no valid plugins");
				compilations.Remove(compilation.id);
				compilation.Completed();
				return;
			}
			CompilerData data = new CompilerData
			{
				OutputFile = compilation.name,
				SourceFiles = list.ToArray(),
				ReferenceFiles = compilation.references.Values.ToArray(),
				Preprocessor = preprocessor
			};
			CompilerMessage compilerMessage = new CompilerMessage
			{
				Id = compilation.id,
				Data = data,
				Type = CompilerMessageType.Compile
			};
			if (ready)
			{
				compilation.startedAt = Interface.Oxide.Now;
				client.PushMessage(compilerMessage);
			}
			else
			{
				messageQueue.Enqueue(compilerMessage);
			}
		}

		private void OnCompilerFailed(string reason)
		{
			foreach (Compilation value in compilations.Values)
			{
				foreach (CompilablePlugin plugin in value.plugins)
				{
					plugin.CompilerErrors = reason;
				}
				value.Completed();
			}
			compilations.Clear();
		}

		private static bool SetFilePermissions(string filePath)
		{
			PlatformID platform = Environment.OSVersion.Platform;
			if (platform != PlatformID.Unix && platform != PlatformID.MacOSX)
			{
				return true;
			}
			string fileName = Path.GetFileName(filePath);
			try
			{
				if (Syscall.access(filePath, AccessModes.X_OK) == 0)
				{
					Log(LogType.Info, fileName + " is executable");
				}
			}
			catch (Exception ex)
			{
				Interface.Oxide.LogException("Unable to check " + fileName + " for executable permission", ex);
			}
			try
			{
				Syscall.chmod(filePath, FilePermissions.S_IRWXU);
				Interface.Oxide.LogInfo("File permissions set for " + fileName);
				return true;
			}
			catch (Exception ex2)
			{
				Interface.Oxide.LogException("Could not set " + filePath + " as executable, please set manually", ex2);
			}
			return false;
		}

		private static bool DownloadFile(string url, string path, int retries = 3)
		{
			string fileName = Path.GetFileName(path);
			int current = 0;
			string md = null;
			try
			{
				DateTime? lastModified = null;
				if (File.Exists(path))
				{
					md = GenerateFileHash(path);
					lastModified = File.GetLastWriteTimeUtc(path);
					string text = "[CSharp] Checking for updates for " + fileName + " | Local MD5: " + md;
					if (lastModified.HasValue)
					{
						text += $" | Last modified: {lastModified.Value:yyyy-MM-dd HH:mm:ss}";
					}
					Interface.Oxide.LogInfo(text);
				}
				else
				{
					Interface.Oxide.LogInfo("[CSharp] Downloading " + fileName + ". . .");
				}
				if (!TryDownload(url, retries, ref current, lastModified, out var data, out var code, out var newerFound, ref md))
				{
					string text2 = ((retries == 1) ? "attempt" : "attempts");
					Interface.Oxide.LogError($"[CSharp] Failed to download {fileName} after {current} {text2} with response code '{code}', please manually download it from {url} and save it here {path}");
					return false;
				}
				if (data != null)
				{
					using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
					{
						fileStream.Write(data, 0, data.Length);
					}
					if (newerFound)
					{
						string text3 = ((md != null) ? ("Remote MD5: " + md) : "Newer found");
						Interface.Oxide.LogInfo("[CSharp] Downloaded newer version of " + fileName + " | " + text3);
					}
					else
					{
						Interface.Oxide.LogInfo("[CSharp] Downloaded " + fileName);
					}
				}
				else
				{
					Interface.Oxide.LogInfo("[CSharp] " + fileName + " is up to date");
				}
				return true;
			}
			catch (Exception ex)
			{
				Interface.Oxide.LogException("Unexpected error occurred while trying to download " + fileName + ", please manually download it from " + url + " and save it here " + path, ex);
				return false;
			}
		}

		private static bool TryDownload(string url, int retries, ref int current, DateTime? lastModified, out byte[] data, out int code, out bool newerFound, ref string md5)
		{
			newerFound = true;
			data = null;
			code = -1;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				httpWebRequest.AllowAutoRedirect = true;
				httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
				if (!string.IsNullOrEmpty(md5))
				{
					string text = "Validating checksum with server for " + Path.GetFileName(url) + " | Local: " + md5;
					int current2 = 0;
					string md6 = null;
					if (TryDownload(url + ".md5", retries, ref current2, null, out var data2, out var code2, out var _, ref md6) && code2 == 200)
					{
						md6 = Encoding.UTF8.GetString(data2).Trim();
						if (string.IsNullOrEmpty(md6))
						{
							md6 = "N/A";
						}
						text = text + " | Server: " + md6;
						if (md6.Equals(md5, StringComparison.InvariantCultureIgnoreCase))
						{
							md5 = md6;
							newerFound = false;
							text += " | Match!";
							Log(LogType.Debug, text);
							return true;
						}
						md5 = md6;
						text += " | No Match!";
						Log(LogType.Warning, text);
					}
					else if (lastModified.HasValue)
					{
						md5 = null;
						Log(LogType.Warning, $"Failed to download {url}.md5 after {current2} attempts with response code '{code2}', using last modified date instead");
						httpWebRequest.IfModifiedSince = lastModified.Value;
					}
				}
				else if (lastModified.HasValue)
				{
					httpWebRequest.IfModifiedSince = lastModified.Value;
				}
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				switch (code = (int)httpWebResponse.StatusCode)
				{
				case 304:
					newerFound = false;
					return true;
				default:
					if (current <= retries)
					{
						current++;
						Thread.Sleep(1000);
						return TryDownload(url, retries, ref current, lastModified, out data, out code, out newerFound, ref md5);
					}
					return false;
				case 200:
				{
					MemoryStream memoryStream = new MemoryStream();
					Stream responseStream = httpWebResponse.GetResponseStream();
					int num = 10000;
					byte[] buffer = new byte[num];
					while (true)
					{
						int num2 = responseStream.Read(buffer, 0, num);
						if (num2 == -1 || num2 == 0)
						{
							break;
						}
						memoryStream.Write(buffer, 0, num2);
					}
					data = memoryStream.ToArray();
					memoryStream.Close();
					responseStream.Close();
					httpWebResponse.Close();
					return true;
				}
				}
			}
			catch (WebException ex)
			{
				if (ex.Response != null)
				{
					HttpWebResponse httpWebResponse2 = (HttpWebResponse)ex.Response;
					code = (int)httpWebResponse2.StatusCode;
					if (httpWebResponse2.StatusCode == HttpStatusCode.NotModified)
					{
						newerFound = false;
						return true;
					}
					if (current <= retries)
					{
						current++;
						Thread.Sleep(1000);
						return TryDownload(url, retries, ref current, lastModified, out data, out code, out newerFound, ref md5);
					}
					return false;
				}
			}
			return false;
		}

		private static void Log(LogType type, string message, Exception exception = null)
		{
			Interface.Oxide.RootLogger.WriteDebug(type, LogEvent.Compile, "CSharp", message, exception);
		}

		private string GetCompilerVersion()
		{
			if (!Installed)
			{
				return "0.0.0";
			}
			return FileVersionInfo.GetVersionInfo(filePath).FileVersion;
		}

		private static string GenerateFileHash(string file)
		{
			using MD5 mD = MD5.Create();
			using FileStream inputStream = File.OpenRead(file);
			return BitConverter.ToString(mD.ComputeHash(inputStream)).Replace("-", string.Empty).ToLowerInvariant();
		}

		private string EscapeSymbolName(string name)
		{
			return SymbolEscapeRegex.Replace(name, "_");
		}
	}
}
namespace Oxide.CSharp.Patching
{
	public interface IPatch
	{
		void Patch(PatchContext context);
	}
	public class PatchContext
	{
		public AssemblyDefinition Assembly { get; }

		public List<PatchValidationAttribute> PatchValidators { get; internal set; }

		public int TotalPatches { get; internal set; }

		public int ContextPatches { get; internal set; }

		public PatchContext(AssemblyDefinition assembly)
		{
			Assembly = assembly;
		}

		public void IncrementPatches()
		{
			ContextPatches++;
			TotalPatches++;
		}
	}
	public static class Patcher
	{
		private static Dictionary<Type, List<PatchValidationAttribute>> Patches;

		private static Type PatchType { get; } = typeof(IPatch);

		private static Type PatchValidationType { get; } = typeof(PatchValidationAttribute);

		private static void GetPatches(Assembly module, ref Dictionary<Type, List<PatchValidationAttribute>> patchTypes)
		{
			try
			{
				Type[] types = module.GetTypes();
				foreach (Type type in types)
				{
					if (!type.IsAbstract && PatchType.IsAssignableFrom(type))
					{
						List<PatchValidationAttribute> validationRules = GetValidationRules(type.GetCustomAttributes(PatchValidationType, inherit: true).Concat(type.Assembly.GetCustomAttributes(PatchValidationType, inherit: true)).ToArray());
						patchTypes.Add(type, validationRules);
						Interface.Oxide.RootLogger.WriteDebug(LogType.Info, LogEvent.Patch, "Patcher", $"Found {validationRules.Count} total validators for patch {type.Name}");
					}
				}
			}
			catch (Exception exception)
			{
				Interface.Oxide.RootLogger.WriteDebug(LogType.Error, LogEvent.Patch, "Patcher", "Failed to read " + (module.GetName()?.Name ?? module.FullName) + " for patches", exception);
			}
		}

		private static void GetPatches(Assembly[] modules, ref Dictionary<Type, List<PatchValidationAttribute>> patchTypes)
		{
			for (int i = 0; i < modules.Length; i++)
			{
				GetPatches(modules[i], ref patchTypes);
			}
		}

		public static bool Run(AssemblyDefinition module)
		{
			if (Patches == null)
			{
				Patches = new Dictionary<Type, List<PatchValidationAttribute>>();
				GetPatches(AppDomain.CurrentDomain.GetAssemblies(), ref Patches);
				Interface.Oxide.RootLogger.WriteDebug(LogType.Info, LogEvent.Patch, "Patcher", $"Found {Patches.Count} patches");
			}
			PatchContext patchContext = new PatchContext(module);
			foreach (KeyValuePair<Type, List<PatchValidationAttribute>> patch in Patches)
			{
				Type key = patch.Key;
				List<PatchValidationAttribute> list = (patchContext.PatchValidators = patch.Value);
				bool flag = false;
				for (int i = 0; i < list.Count; i++)
				{
					if (!list[i].Validate(module))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					try
					{
						IPatch obj = (IPatch)Activator.CreateInstance(key, nonPublic: true);
						patchContext.ContextPatches = 0;
						obj.Patch(patchContext);
						Interface.Oxide.RootLogger.WriteDebug(LogType.Info, LogEvent.Patch, "Patcher", $"{key.Name} has applied {patchContext.ContextPatches} patches to {module.Name?.Name ?? module.FullName}");
					}
					catch (Exception exception)
					{
						Interface.Oxide.RootLogger.WriteDebug(LogType.Error, LogEvent.Patch, "Patcher", $"{key.Name} has applied {patchContext.ContextPatches} patches to {module.Name?.Name ?? module.FullName} but threw a error", exception);
					}
				}
			}
			return patchContext.TotalPatches > 0;
		}

		public static byte[] Run(byte[] data, out bool patched)
		{
			try
			{
				using MemoryStream stream = new MemoryStream(data);
				AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(stream);
				if (Run(assemblyDefinition))
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						assemblyDefinition.Write(memoryStream);
						patched = true;
						return memoryStream.ToArray();
					}
				}
			}
			catch (Exception exception)
			{
				Interface.Oxide.RootLogger.WriteDebug(LogType.Error, LogEvent.Patch, "Patcher", "Failed to patch", exception);
			}
			patched = false;
			return data;
		}

		public static List<PatchValidationAttribute> GetValidationRules(object[] attributes)
		{
			List<PatchValidationAttribute> list = new List<PatchValidationAttribute>();
			for (int i = 0; i < attributes.Length; i++)
			{
				if (attributes[i] as Attribute is PatchValidationAttribute item)
				{
					list.Add(item);
				}
			}
			return list;
		}
	}
	[HasName("0Harmony", StringValidationType.StartsWith, StringComparison.InvariantCultureIgnoreCase, InverseCheck = true)]
	[HasName("System", StringValidationType.StartsWith, StringComparison.InvariantCultureIgnoreCase, InverseCheck = true)]
	[HasName("Microsoft", StringValidationType.StartsWith, StringComparison.InvariantCultureIgnoreCase, InverseCheck = true)]
	[HasName("mscorlib", StringValidationType.StartsWith, StringComparison.InvariantCultureIgnoreCase, InverseCheck = true)]
	[HasName("Unity", StringValidationType.StartsWith, StringComparison.InvariantCultureIgnoreCase, InverseCheck = true)]
	[HasName("Mono", StringValidationType.StartsWith, StringComparison.InvariantCultureIgnoreCase, InverseCheck = true)]
	[HasName("netstandard", StringValidationType.StartsWith, StringComparison.InvariantCultureIgnoreCase, InverseCheck = true)]
	[HasName("Oxide", StringValidationType.StartsWith, StringComparison.InvariantCultureIgnoreCase, InverseCheck = true)]
	[HasName("MySql.Data", StringValidationType.StartsWith, StringComparison.InvariantCultureIgnoreCase, InverseCheck = true)]
	[HasConfigNames(InverseCheck = true)]
	[HasEnvironmentalVariable("AllowPublicize")]
	public class Publicizer : TraversePatch
	{
		[HasVisibility(false)]
		[HasAttribute("CompilerGeneratedAttribute", StringValidationType.EndsWith, StringComparison.InvariantCultureIgnoreCase, InverseCheck = true)]
		[HasAttribute("CompilerServices.ExtensionAttribute", StringValidationType.EndsWith, StringComparison.InvariantCultureIgnoreCase, InverseCheck = true)]
		protected override bool OnMemberDefinition(IMemberDefinition member)
		{
			return base.OnMemberDefinition(member);
		}

		protected override bool OnTypeDefinition(TypeDefinition type)
		{
			if (type.IsNested && !type.IsNestedPublic)
			{
				type.IsNestedPublic = true;
				return true;
			}
			if (!type.IsPublic)
			{
				type.IsPublic = true;
				return true;
			}
			return false;
		}

		protected override bool OnFieldDefinition(FieldDefinition field)
		{
			if (field.IsPublic)
			{
				return false;
			}
			field.IsPublic = true;
			return true;
		}

		protected override bool OnPropertyDefinition(PropertyDefinition property)
		{
			bool num = property.GetMethod != null && OnMethodDefinition(property.GetMethod);
			bool flag = property.SetMethod != null && OnMethodDefinition(property.SetMethod);
			return num || flag;
		}

		protected override bool OnMethodDefinition(MethodDefinition method)
		{
			if (method.IsPublic)
			{
				return false;
			}
			method.IsPublic = true;
			return true;
		}

		protected override void OnPatchFinished(PatchContext context)
		{
			string variable = EnvironmentHelper.GetVariable("PublicizerOutput");
			if (string.IsNullOrEmpty(variable))
			{
				return;
			}
			string name = context.Assembly.Name.Name;
			if (!Directory.Exists(variable))
			{
				Log("Failed to write " + name + " because PublicizeOutput " + variable + " doesn't exist", LogType.Error);
				return;
			}
			try
			{
				name = Path.Combine(variable, name + ".dll");
				context.Assembly.Write(name);
				Log("Wrote publicized assembly to " + variable);
			}
			catch (Exception e)
			{
				Log("Failed to write publicized assembly to " + variable, LogType.Error, e);
			}
		}
	}
	public abstract class TraversePatch : IPatch
	{
		protected virtual string Name { get; }

		protected IEnumerable<PatchValidationAttribute> TypeValidators { get; }

		protected IEnumerable<PatchValidationAttribute> PropertyValidators { get; }

		protected IEnumerable<PatchValidationAttribute> FieldValidators { get; }

		protected IEnumerable<PatchValidationAttribute> MethodValidators { get; }

		protected IEnumerable<PatchValidationAttribute> EventValidators { get; }

		protected IEnumerable<PatchValidationAttribute> MemberValidators { get; }

		protected TraversePatch()
		{
			Type type = GetType();
			Name = type.Name;
			TypeValidators = GetValidationRules("OnTypeDefinition", type);
			PropertyValidators = GetValidationRules("OnPropertyDefinition", type);
			FieldValidators = GetValidationRules("OnFieldDefinition", type);
			MethodValidators = GetValidationRules("OnMethodDefinition", type);
			EventValidators = GetValidationRules("OnEventDefinition", type);
			MemberValidators = GetValidationRules("OnMemberDefinition", type);
		}

		public void Patch(PatchContext context)
		{
			List<TypeDefinition> list = context.Assembly.MainModule.GetTypes().ToList();
			for (int i = 0; i < list.Count; i++)
			{
				TypeDefinition type = list[i];
				RecurseType(type, context);
			}
			OnPatchFinished(context);
		}

		private void RecurseType(TypeDefinition type, PatchContext context)
		{
			if (RunValidation(type, MemberValidators) && OnMemberDefinition(type))
			{
				context.IncrementPatches();
			}
			if (type.HasProperties)
			{
				for (int i = 0; i < type.Properties.Count; i++)
				{
					PropertyDefinition member = type.Properties[i];
					if (RunValidation(member, MemberValidators) && OnMemberDefinition(member))
					{
						context.IncrementPatches();
					}
				}
			}
			if (type.HasFields)
			{
				for (int j = 0; j < type.Fields.Count; j++)
				{
					FieldDefinition member2 = type.Fields[j];
					if (RunValidation(member2, MemberValidators) && OnMemberDefinition(member2))
					{
						context.IncrementPatches();
					}
				}
			}
			if (type.HasMethods)
			{
				for (int k = 0; k < type.Methods.Count; k++)
				{
					MethodDefinition member3 = type.Methods[k];
					if (RunValidation(member3, MemberValidators) && OnMemberDefinition(member3))
					{
						context.IncrementPatches();
					}
				}
			}
			if (type.HasEvents)
			{
				for (int l = 0; l < type.Events.Count; l++)
				{
					EventDefinition member4 = type.Events[l];
					if (RunValidation(member4, MemberValidators) && OnMemberDefinition(member4))
					{
						context.IncrementPatches();
					}
				}
			}
			if (type.HasNestedTypes)
			{
				for (int m = 0; m < type.NestedTypes.Count; m++)
				{
					RecurseType(type.NestedTypes[m], context);
				}
			}
		}

		protected virtual bool OnMemberDefinition(IMemberDefinition member)
		{
			if (member is TypeDefinition type)
			{
				if (RunValidation(member, TypeValidators))
				{
					return OnTypeDefinition(type);
				}
				return false;
			}
			if (member is PropertyDefinition property)
			{
				if (RunValidation(member, PropertyValidators))
				{
					return OnPropertyDefinition(property);
				}
				return false;
			}
			if (member is FieldDefinition field)
			{
				if (RunValidation(member, FieldValidators))
				{
					return OnFieldDefinition(field);
				}
				return false;
			}
			if (member is MethodDefinition methodDefinition)
			{
				if (RunValidation(methodDefinition, MethodValidators))
				{
					return OnMethodDefinition(methodDefinition);
				}
				return false;
			}
			if (member is EventDefinition eventDefinition)
			{
				if (RunValidation(eventDefinition, EventValidators))
				{
					return OnEventDefinition(eventDefinition);
				}
				return false;
			}
			return false;
		}

		protected virtual bool OnTypeDefinition(TypeDefinition type)
		{
			return false;
		}

		protected virtual bool OnPropertyDefinition(PropertyDefinition property)
		{
			return false;
		}

		protected virtual bool OnFieldDefinition(FieldDefinition field)
		{
			return false;
		}

		protected virtual bool OnMethodDefinition(MethodDefinition method)
		{
			return false;
		}

		protected virtual bool OnEventDefinition(EventDefinition @event)
		{
			return false;
		}

		protected virtual void OnPatchFinished(PatchContext context)
		{
		}

		protected bool RunValidation(IMemberDefinition member, IEnumerable<PatchValidationAttribute> validations)
		{
			if (member == null)
			{
				return false;
			}
			if (validations == null)
			{
				return true;
			}
			foreach (PatchValidationAttribute validation in validations)
			{
				if (!validation.Validate(member))
				{
					return false;
				}
			}
			return true;
		}

		protected void Log(string message, LogType logType = LogType.Info, Exception e = null)
		{
			Interface.Oxide.RootLogger.WriteDebug(logType, LogEvent.Patch, Name, message, e);
		}

		private static IEnumerable<PatchValidationAttribute> GetValidationRules(string methodName, Type type)
		{
			MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			Type typeFromHandle = typeof(bool);
			foreach (MethodInfo methodInfo in methods)
			{
				if (methodInfo.Name.Equals(methodName) && methodInfo.ReturnType == typeFromHandle && methodInfo.IsVirtual)
				{
					return Patcher.GetValidationRules(methodInfo.GetCustomAttributes(inherit: true));
				}
			}
			return null;
		}
	}
}
namespace Oxide.CSharp.Patching.Validation
{
	public class HasAttributeAttribute : HasNameAttribute
	{
		public HasAttributeAttribute(string rule, StringValidationType type = StringValidationType.StartsWith, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
			: base(rule, type, comparison)
		{
		}

		protected override bool IsValid(object item)
		{
			if (item is CustomAttribute customAttribute)
			{
				return base.IsValid((object)customAttribute.AttributeType.FullName);
			}
			if (item is Collection<CustomAttribute> source)
			{
				return source.Any((CustomAttribute a) => base.IsValid((object)a.AttributeType.FullName));
			}
			if (item is AssemblyDefinition { HasCustomAttributes: not false } assemblyDefinition)
			{
				return assemblyDefinition.CustomAttributes.Any((CustomAttribute a) => base.IsValid((object)a.AttributeType.FullName));
			}
			if (item is ModuleDefinition { HasCustomAttributes: not false } moduleDefinition)
			{
				return moduleDefinition.CustomAttributes.Any((CustomAttribute a) => base.IsValid((object)a.AttributeType.FullName));
			}
			if (item is IMemberDefinition { HasCustomAttributes: not false } memberDefinition)
			{
				return memberDefinition.CustomAttributes.Any((CustomAttribute a) => base.IsValid((object)a.AttributeType.FullName));
			}
			return false;
		}
	}
	public class HasConfigNamesAttribute : HasNameAttribute
	{
		public HasConfigNamesAttribute()
			: base(string.Empty)
		{
		}

		protected override bool IsValid(object item)
		{
			foreach (string ignoredPublicizerReference in Interface.Oxide.Config.Compiler.IgnoredPublicizerReferences)
			{
				base.ValidationRule = ignoredPublicizerReference;
				if (base.IsValid(item))
				{
					return true;
				}
			}
			return false;
		}
	}
	public class HasEnvironmentalVariableAttribute : PatchValidationAttribute
	{
		private string VariableName { get; }

		public HasEnvironmentalVariableAttribute(string rule)
		{
			VariableName = rule ?? throw new ArgumentNullException("rule");
		}

		protected override bool IsValid(object item)
		{
			return !string.IsNullOrEmpty(EnvironmentHelper.GetVariable(VariableName));
		}
	}
	public class HasNameAttribute : PatchValidationAttribute
	{
		public string ValidationRule { get; internal set; }

		public StringValidationType ValidationType { get; }

		public StringComparison ValidationComparison { get; }

		public HasNameAttribute(string rule, StringValidationType type = StringValidationType.StartsWith, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
		{
			ValidationRule = rule;
			ValidationType = type;
			ValidationComparison = comparison;
		}

		protected override bool IsValid(object item)
		{
			string text = null;
			if (item is string text2)
			{
				text = text2;
			}
			else if (item is AssemblyDefinition assemblyDefinition)
			{
				text = assemblyDefinition.FullName;
			}
			else if (item is ModuleDefinition moduleDefinition)
			{
				text = moduleDefinition.Assembly.FullName;
			}
			else if (item is ModuleReference moduleReference)
			{
				text = moduleReference.Name;
			}
			else if (item is AssemblyNameReference assemblyNameReference)
			{
				text = assemblyNameReference.FullName;
			}
			else
			{
				if (!(item is MemberReference memberReference))
				{
					return false;
				}
				text = memberReference.FullName;
			}
			return ValidationType switch
			{
				StringValidationType.Equals => text.Equals(ValidationRule, ValidationComparison), 
				StringValidationType.Contains => text.IndexOf(ValidationRule, ValidationComparison) >= 0, 
				StringValidationType.EndsWith => text.EndsWith(ValidationRule, ValidationComparison), 
				StringValidationType.RegularExpression => Regex.IsMatch(text, ValidationRule, RegexOptions.Compiled), 
				_ => text.StartsWith(ValidationRule, ValidationComparison), 
			};
		}
	}
	public class HasVisibilityAttribute : PatchValidationAttribute
	{
		public bool IsPublic { get; }

		public bool? IsStatic { get; set; }

		public HasVisibilityAttribute(bool isPublic)
		{
			IsPublic = isPublic;
		}

		protected override bool IsValid(object item)
		{
			if (item is TypeDefinition typeDefinition)
			{
				if (typeDefinition.IsNested)
				{
					if (typeDefinition.IsNestedPublic != IsPublic)
					{
						return false;
					}
				}
				else if (typeDefinition.IsPublic != IsPublic)
				{
					return false;
				}
				if (IsStatic.HasValue && (typeDefinition.IsAbstract && typeDefinition.IsSealed) != IsStatic.Value)
				{
					return false;
				}
				return true;
			}
			if (item is PropertyDefinition propertyDefinition)
			{
				if (IsPublic)
				{
					if (propertyDefinition.SetMethod == null)
					{
						return IsValid(propertyDefinition.GetMethod);
					}
					if (IsValid(propertyDefinition.GetMethod))
					{
						return IsValid(propertyDefinition.SetMethod);
					}
					return false;
				}
				if (propertyDefinition.SetMethod == null)
				{
					return IsValid(propertyDefinition.GetMethod);
				}
				if (!IsValid(propertyDefinition.GetMethod))
				{
					return IsValid(propertyDefinition.SetMethod);
				}
				return true;
			}
			if (item is EventDefinition eventDefinition)
			{
				if (eventDefinition.AddMethod != null)
				{
					return IsValid(eventDefinition.AddMethod);
				}
				return false;
			}
			if (item is MethodDefinition methodDefinition)
			{
				if (IsStatic.HasValue && methodDefinition.IsStatic != IsStatic.Value)
				{
					return false;
				}
				return methodDefinition.IsPublic == IsPublic;
			}
			if (item is FieldDefinition fieldDefinition)
			{
				if (IsStatic.HasValue && fieldDefinition.IsStatic != IsStatic.Value)
				{
					return false;
				}
				return fieldDefinition.IsPublic == IsPublic;
			}
			if (item is IMemberDefinition)
			{
				bool? propertyValue = PatchValidationAttribute.GetPropertyValue<bool?>(item, "IsPublic");
				if (!propertyValue.HasValue || propertyValue.Value != IsPublic)
				{
					return false;
				}
				if (IsStatic.HasValue)
				{
					bool? propertyValue2 = PatchValidationAttribute.GetPropertyValue<bool?>(item, "IsStatic");
					if (!propertyValue2.HasValue || propertyValue2.Value != IsStatic.Value)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}
	}
	public class IsHideBySig : PatchValidationAttribute
	{
		protected override bool IsValid(object item)
		{
			if (item is IMemberDefinition)
			{
				return PatchValidationAttribute.GetPropertyValue(item, "IsHideBySig", defaultValue: false);
			}
			return false;
		}
	}
	public class IsSpecialNameAttribute : PatchValidationAttribute
	{
		protected override bool IsValid(object item)
		{
			if (item is IMemberDefinition memberDefinition)
			{
				return memberDefinition.IsSpecialName;
			}
			return false;
		}
	}
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public abstract class PatchValidationAttribute : Attribute
	{
		public bool InverseCheck { get; set; }

		protected abstract bool IsValid(object item);

		public bool Validate(object item)
		{
			if (!InverseCheck)
			{
				return IsValid(item);
			}
			return !IsValid(item);
		}

		protected static T GetPropertyValue<T>(object instance, string name, T defaultValue = default(T), BindingFlags flags = BindingFlags.Instance | BindingFlags.Public)
		{
			if (instance == null || string.IsNullOrEmpty(name))
			{
				return defaultValue;
			}
			PropertyInfo property = instance.GetType().GetProperty(name, flags);
			if (property == null)
			{
				return defaultValue;
			}
			object value = property.GetValue(instance, null);
			if (value is T)
			{
				return (T)value;
			}
			return defaultValue;
		}
	}
	public enum StringValidationType
	{
		Equals = 0,
		Contains = 1,
		StartsWith = 2,
		EndsWith = 4,
		RegularExpression = 8
	}
	[Flags]
	public enum VersionCompareMethod
	{
		Equality = 0,
		GreaterThan = 1,
		LessThan = 2,
		GreaterThanOrEqualTo = 5,
		LessThanOrEqualTo = 0xA
	}
}
