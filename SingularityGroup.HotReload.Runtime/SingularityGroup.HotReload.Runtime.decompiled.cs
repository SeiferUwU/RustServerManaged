using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using UnityEngine;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
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
			FilePathsData = new byte[83]
			{
				0, 0, 0, 1, 0, 0, 0, 75, 92, 80,
				97, 99, 107, 97, 103, 101, 115, 92, 99, 111,
				109, 46, 115, 105, 110, 103, 117, 108, 97, 114,
				105, 116, 121, 103, 114, 111, 117, 112, 46, 104,
				111, 116, 114, 101, 108, 111, 97, 100, 92, 82,
				117, 110, 116, 105, 109, 101, 92, 72, 111, 116,
				82, 101, 108, 111, 97, 100, 83, 101, 116, 116,
				105, 110, 103, 115, 79, 98, 106, 101, 99, 116,
				46, 99, 115
			},
			TypesData = new byte[55]
			{
				0, 0, 0, 0, 50, 83, 105, 110, 103, 117,
				108, 97, 114, 105, 116, 121, 71, 114, 111, 117,
				112, 46, 72, 111, 116, 82, 101, 108, 111, 97,
				100, 124, 72, 111, 116, 82, 101, 108, 111, 97,
				100, 83, 101, 116, 116, 105, 110, 103, 115, 79,
				98, 106, 101, 99, 116
			},
			TotalFiles = 1,
			TotalTypes = 1,
			IsEditorOnly = false
		};
	}
}
namespace SingularityGroup.HotReload;

internal class HotReloadSettingsObject : ScriptableObject
{
}
