using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using Unity.Profiling.Memory;
using UnityEngine;
using UnityEngine.Scripting;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: Preserve]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
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
			FilePathsData = new byte[88]
			{
				0, 0, 0, 3, 0, 0, 0, 80, 92, 76,
				105, 98, 114, 97, 114, 121, 92, 80, 97, 99,
				107, 97, 103, 101, 67, 97, 99, 104, 101, 92,
				99, 111, 109, 46, 117, 110, 105, 116, 121, 46,
				109, 101, 109, 111, 114, 121, 112, 114, 111, 102,
				105, 108, 101, 114, 64, 49, 46, 49, 46, 49,
				92, 82, 117, 110, 116, 105, 109, 101, 92, 77,
				101, 116, 97, 100, 97, 116, 97, 73, 110, 106,
				101, 99, 116, 111, 114, 46, 99, 115
			},
			TypesData = new byte[131]
			{
				0, 0, 0, 0, 37, 85, 110, 105, 116, 121,
				46, 77, 101, 109, 111, 114, 121, 80, 114, 111,
				102, 105, 108, 101, 114, 124, 77, 101, 116, 97,
				100, 97, 116, 97, 73, 110, 106, 101, 99, 116,
				111, 114, 0, 0, 0, 0, 36, 85, 110, 105,
				116, 121, 46, 77, 101, 109, 111, 114, 121, 80,
				114, 111, 102, 105, 108, 101, 114, 124, 77, 101,
				116, 97, 100, 97, 116, 97, 67, 111, 108, 108,
				101, 99, 116, 0, 0, 0, 0, 43, 85, 110,
				105, 116, 121, 46, 77, 101, 109, 111, 114, 121,
				80, 114, 111, 102, 105, 108, 101, 114, 124, 68,
				101, 102, 97, 117, 108, 116, 77, 101, 116, 97,
				100, 97, 116, 97, 67, 111, 108, 108, 101, 99,
				116
			},
			TotalFiles = 1,
			TotalTypes = 3,
			IsEditorOnly = false
		};
	}
}
namespace Unity.MemoryProfiler;

internal static class MetadataInjector
{
	public static DefaultMetadataCollect DefaultCollector;

	public static long CollectorCount;

	public static byte DefaultCollectorInjected;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
	private static void PlayerInitMetadata()
	{
		InitializeMetadataCollection();
	}

	private static void InitializeMetadataCollection()
	{
		DefaultCollector = new DefaultMetadataCollect();
	}
}
public abstract class MetadataCollect : IDisposable
{
	private bool disposed;

	protected MetadataCollect()
	{
		if (MetadataInjector.DefaultCollector != null && MetadataInjector.DefaultCollector != this && MetadataInjector.DefaultCollectorInjected != 0)
		{
			Unity.Profiling.Memory.MemoryProfiler.CreatingMetadata -= MetadataInjector.DefaultCollector.CollectMetadata;
			MetadataInjector.CollectorCount--;
			MetadataInjector.DefaultCollectorInjected = 0;
		}
		Unity.Profiling.Memory.MemoryProfiler.CreatingMetadata += CollectMetadata;
		MetadataInjector.CollectorCount++;
	}

	public abstract void CollectMetadata(MemorySnapshotMetadata data);

	public void Dispose()
	{
		if (!disposed)
		{
			disposed = true;
			Unity.Profiling.Memory.MemoryProfiler.CreatingMetadata -= CollectMetadata;
			MetadataInjector.CollectorCount--;
			if (MetadataInjector.DefaultCollector != null && MetadataInjector.CollectorCount < 1 && MetadataInjector.DefaultCollector != this)
			{
				MetadataInjector.DefaultCollectorInjected = 1;
				Unity.Profiling.Memory.MemoryProfiler.CreatingMetadata += MetadataInjector.DefaultCollector.CollectMetadata;
				MetadataInjector.CollectorCount++;
			}
		}
	}
}
internal class DefaultMetadataCollect : MetadataCollect
{
	public DefaultMetadataCollect()
	{
		MetadataInjector.DefaultCollectorInjected = 1;
	}

	public override void CollectMetadata(MemorySnapshotMetadata data)
	{
		data.Description = "Project name: " + Application.productName;
	}
}
