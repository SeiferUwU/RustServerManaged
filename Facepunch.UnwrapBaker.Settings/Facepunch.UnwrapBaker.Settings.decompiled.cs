using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyVersion("0.0.0.0")]
[Serializable]
public struct MeshTrimSettings
{
	public bool TrimBasedOnVisibility;

	[Range(1f, 100f)]
	public int IterationsPerLoop;

	public float StartHeight;

	public float EndHeight;

	[Range(1f, 100f)]
	public int HeightIterations;

	public float Radius;

	public float MinimumTriangleArea;

	public float MinimumTriangleEdgeLength;

	public Vector3 OriginOffset;

	public static MeshTrimSettings Default = new MeshTrimSettings
	{
		TrimBasedOnVisibility = true,
		IterationsPerLoop = 16,
		StartHeight = 1f,
		EndHeight = 16f,
		HeightIterations = 16,
		Radius = 50f,
		MinimumTriangleArea = 0.1f,
		MinimumTriangleEdgeLength = 0.02f,
		OriginOffset = Vector3.zero
	};
}
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
			FilePathsData = new byte[99]
			{
				0, 0, 0, 1, 0, 0, 0, 91, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 85, 110, 119, 114, 97, 112,
				66, 97, 107, 101, 114, 92, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 85, 110, 119, 114,
				97, 112, 66, 97, 107, 101, 114, 46, 83, 101,
				116, 116, 105, 110, 103, 115, 92, 77, 101, 115,
				104, 84, 114, 105, 109, 109, 101, 114, 83, 101,
				116, 116, 105, 110, 103, 115, 46, 99, 115
			},
			TypesData = new byte[22]
			{
				0, 0, 0, 0, 17, 124, 77, 101, 115, 104,
				84, 114, 105, 109, 83, 101, 116, 116, 105, 110,
				103, 115
			},
			TotalFiles = 1,
			TotalTypes = 1,
			IsEditorOnly = false
		};
	}
}
