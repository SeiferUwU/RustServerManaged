using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: InternalsVisibleTo("Unity.Formats.Fbx.Editor")]
[assembly: InternalsVisibleTo("Unity.Formats.Fbx.Editor.Tests")]
[assembly: InternalsVisibleTo("Unity.ProBuilder.AddOns.Editor")]
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
			FilePathsData = new byte[78]
			{
				0, 0, 0, 2, 0, 0, 0, 70, 92, 76,
				105, 98, 114, 97, 114, 121, 92, 80, 97, 99,
				107, 97, 103, 101, 67, 97, 99, 104, 101, 92,
				99, 111, 109, 46, 117, 110, 105, 116, 121, 46,
				102, 111, 114, 109, 97, 116, 115, 46, 102, 98,
				120, 64, 53, 46, 49, 46, 51, 92, 82, 117,
				110, 116, 105, 109, 101, 92, 70, 98, 120, 80,
				114, 101, 102, 97, 98, 46, 99, 115
			},
			TypesData = new byte[95]
			{
				0, 0, 0, 0, 43, 85, 110, 105, 116, 121,
				69, 110, 103, 105, 110, 101, 46, 70, 111, 114,
				109, 97, 116, 115, 46, 70, 98, 120, 46, 69,
				120, 112, 111, 114, 116, 101, 114, 124, 83, 116,
				114, 105, 110, 103, 80, 97, 105, 114, 0, 0,
				0, 0, 42, 85, 110, 105, 116, 121, 69, 110,
				103, 105, 110, 101, 46, 70, 111, 114, 109, 97,
				116, 115, 46, 70, 98, 120, 46, 69, 120, 112,
				111, 114, 116, 101, 114, 124, 70, 98, 120, 80,
				114, 101, 102, 97, 98
			},
			TotalFiles = 1,
			TotalTypes = 2,
			IsEditorOnly = false
		};
	}
}
namespace UnityEngine.Formats.Fbx.Exporter;

[Serializable]
internal struct StringPair
{
	private string m_fbxObjectName;

	private string m_unityObjectName;

	public string FBXObjectName
	{
		get
		{
			return m_fbxObjectName;
		}
		set
		{
			m_fbxObjectName = value;
		}
	}

	public string UnityObjectName
	{
		get
		{
			return m_unityObjectName;
		}
		set
		{
			m_unityObjectName = value;
		}
	}
}
internal delegate void HandleUpdate(FbxPrefab updatedInstance, IEnumerable<GameObject> updatedObjects);
internal class FbxPrefab : MonoBehaviour
{
	[SerializeField]
	private string m_fbxHistory;

	[SerializeField]
	private List<StringPair> m_nameMapping = new List<StringPair>();

	[SerializeField]
	[Tooltip("Which FBX file does this refer to?")]
	private GameObject m_fbxModel;

	[Tooltip("Should we auto-update this prefab when the FBX file is updated?")]
	[SerializeField]
	private bool m_autoUpdate = true;

	public string FbxHistory
	{
		get
		{
			return m_fbxHistory;
		}
		set
		{
			m_fbxHistory = value;
		}
	}

	public List<StringPair> NameMapping => m_nameMapping;

	public GameObject FbxModel
	{
		get
		{
			return m_fbxModel;
		}
		set
		{
			m_fbxModel = value;
		}
	}

	public bool AutoUpdate
	{
		get
		{
			return m_autoUpdate;
		}
		set
		{
			m_autoUpdate = value;
		}
	}

	public static event HandleUpdate OnUpdate;

	public static void CallOnUpdate(FbxPrefab instance, IEnumerable<GameObject> updatedObjects)
	{
		if (FbxPrefab.OnUpdate != null)
		{
			FbxPrefab.OnUpdate(instance, updatedObjects);
		}
	}
}
