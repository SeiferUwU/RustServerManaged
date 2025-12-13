#define UNITY_ASSERTIONS
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using LeTai.Effects;
using LeTai.TrueShadow;
using LeTai.TrueShadow.PluginInterfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: InternalsVisibleTo("LeTai.TrueShadow.Editor")]
[assembly: AssemblyVersion("0.0.0.0")]
[DefaultExecutionOrder(-1000)]
public class ShadowUpdateGroup : MonoBehaviour
{
	public static Dictionary<string, ShadowUpdateGroup> Groups = new Dictionary<string, ShadowUpdateGroup>();

	[SerializeField]
	private string groupId;

	[SerializeField]
	private MonoBehaviour visibilityProvider;

	private readonly List<TrueShadow> shadows = new List<TrueShadow>();

	private IShadowGroupVisibility visibility;

	public List<TrueShadow> Shadows => shadows;

	public bool ShouldUpdate
	{
		get
		{
			if (visibility != null)
			{
				return visibility.ShouldUpdateShadows;
			}
			return false;
		}
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void ResetStaticFields()
	{
		Groups = new Dictionary<string, ShadowUpdateGroup>();
	}

	private void Awake()
	{
		visibility = visibilityProvider as IShadowGroupVisibility;
		Groups[groupId] = this;
		TrueShadowManager.Instance.RegisterGroup(this);
	}

	private void OnDestroy()
	{
		Groups.Remove(groupId);
		TrueShadowManager.Instance.UnregisterGroup(this);
	}

	public void Register(TrueShadow shadow)
	{
		shadows.Add(shadow);
	}

	public void Unregister(TrueShadow shadow)
	{
		shadows.Remove(shadow);
	}
}
public interface IShadowGroupVisibility
{
	bool ShouldUpdateShadows { get; }
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
			FilePathsData = new byte[3014]
			{
				0, 0, 0, 1, 0, 0, 0, 71, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 76, 101, 32, 84, 97, 105,
				39, 115, 32, 65, 115, 115, 101, 116, 92, 84,
				114, 117, 101, 83, 104, 97, 100, 111, 119, 92,
				83, 99, 114, 105, 112, 116, 115, 92, 69, 102,
				102, 101, 99, 116, 115, 92, 66, 108, 117, 114,
				67, 111, 110, 102, 105, 103, 46, 99, 115, 0,
				0, 0, 2, 0, 0, 0, 67, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 76, 101, 32, 84, 97, 105, 39,
				115, 32, 65, 115, 115, 101, 116, 92, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 92, 83,
				99, 114, 105, 112, 116, 115, 92, 69, 102, 102,
				101, 99, 116, 115, 92, 66, 108, 117, 114, 72,
				81, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 75, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 76, 101,
				32, 84, 97, 105, 39, 115, 32, 65, 115, 115,
				101, 116, 92, 84, 114, 117, 101, 83, 104, 97,
				100, 111, 119, 92, 83, 99, 114, 105, 112, 116,
				115, 92, 69, 102, 102, 101, 99, 116, 115, 92,
				73, 66, 108, 117, 114, 65, 108, 103, 111, 114,
				105, 116, 104, 109, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 70, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 76, 101, 32, 84, 97, 105, 39, 115, 32,
				65, 115, 115, 101, 116, 92, 84, 114, 117, 101,
				83, 104, 97, 100, 111, 119, 92, 83, 99, 114,
				105, 112, 116, 115, 92, 69, 102, 102, 101, 99,
				116, 115, 92, 82, 101, 115, 111, 117, 114, 99,
				101, 115, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 73, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 76,
				101, 32, 84, 97, 105, 39, 115, 32, 65, 115,
				115, 101, 116, 92, 84, 114, 117, 101, 83, 104,
				97, 100, 111, 119, 92, 83, 99, 114, 105, 112,
				116, 115, 92, 69, 102, 102, 101, 99, 116, 115,
				92, 83, 99, 97, 108, 97, 98, 108, 101, 66,
				108, 117, 114, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 79, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				76, 101, 32, 84, 97, 105, 39, 115, 32, 65,
				115, 115, 101, 116, 92, 84, 114, 117, 101, 83,
				104, 97, 100, 111, 119, 92, 83, 99, 114, 105,
				112, 116, 115, 92, 69, 102, 102, 101, 99, 116,
				115, 92, 83, 99, 97, 108, 97, 98, 108, 101,
				66, 108, 117, 114, 67, 111, 110, 102, 105, 103,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				77, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 76, 101, 32,
				84, 97, 105, 39, 115, 32, 65, 115, 115, 101,
				116, 92, 84, 114, 117, 101, 83, 104, 97, 100,
				111, 119, 92, 83, 99, 114, 105, 112, 116, 115,
				92, 69, 102, 102, 101, 99, 116, 115, 92, 83,
				104, 97, 100, 101, 114, 80, 114, 111, 112, 101,
				114, 116, 105, 101, 115, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 81, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 76, 101, 32, 84, 97, 105, 39, 115,
				32, 65, 115, 115, 101, 116, 92, 84, 114, 117,
				101, 83, 104, 97, 100, 111, 119, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 72, 101, 108, 112,
				101, 114, 92, 65, 110, 105, 109, 97, 116, 101,
				100, 66, 105, 83, 116, 97, 116, 101, 66, 117,
				116, 116, 111, 110, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 84, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 76, 101, 32, 84, 97, 105, 39, 115, 32,
				65, 115, 115, 101, 116, 92, 84, 114, 117, 101,
				83, 104, 97, 100, 111, 119, 92, 83, 99, 114,
				105, 112, 116, 115, 92, 72, 101, 108, 112, 101,
				114, 92, 84, 114, 117, 101, 83, 104, 97, 100,
				111, 119, 67, 117, 115, 116, 111, 109, 77, 97,
				116, 101, 114, 105, 97, 108, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 82, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 76, 101, 32, 84, 97, 105, 39,
				115, 32, 65, 115, 115, 101, 116, 92, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 92, 83,
				99, 114, 105, 112, 116, 115, 92, 72, 101, 108,
				112, 101, 114, 92, 84, 114, 117, 101, 83, 104,
				97, 100, 111, 119, 68, 105, 115, 97, 98, 108,
				101, 67, 97, 99, 104, 101, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 82, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 76, 101, 32, 84, 97, 105, 39,
				115, 32, 65, 115, 115, 101, 116, 92, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 92, 83,
				99, 114, 105, 112, 116, 115, 92, 72, 101, 108,
				112, 101, 114, 92, 84, 114, 117, 101, 83, 104,
				97, 100, 111, 119, 73, 110, 115, 101, 116, 79,
				110, 80, 114, 101, 115, 115, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 90, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 76, 101, 32, 84, 97, 105, 39,
				115, 32, 65, 115, 115, 101, 116, 92, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 92, 83,
				99, 114, 105, 112, 116, 115, 92, 72, 101, 108,
				112, 101, 114, 92, 84, 114, 117, 101, 83, 104,
				97, 100, 111, 119, 73, 110, 116, 101, 114, 97,
				99, 116, 105, 111, 110, 65, 110, 105, 109, 97,
				116, 105, 111, 110, 46, 99, 115, 0, 0, 0,
				11, 0, 0, 0, 69, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 76, 101, 32, 84, 97, 105, 39, 115, 32,
				65, 115, 115, 101, 116, 92, 84, 114, 117, 101,
				83, 104, 97, 100, 111, 119, 92, 83, 99, 114,
				105, 112, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 73, 110, 116, 101, 114, 102, 97, 99, 101,
				115, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 68, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 76, 101,
				32, 84, 97, 105, 39, 115, 32, 65, 115, 115,
				101, 116, 92, 84, 114, 117, 101, 83, 104, 97,
				100, 111, 119, 92, 83, 99, 114, 105, 112, 116,
				115, 92, 80, 114, 111, 106, 101, 99, 116, 83,
				101, 116, 116, 105, 110, 103, 115, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 64, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 76, 101, 32, 84, 97, 105,
				39, 115, 32, 65, 115, 115, 101, 116, 92, 84,
				114, 117, 101, 83, 104, 97, 100, 111, 119, 92,
				83, 99, 114, 105, 112, 116, 115, 92, 81, 117,
				105, 99, 107, 80, 114, 101, 115, 101, 116, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 66,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 76, 101, 32, 84,
				97, 105, 39, 115, 32, 65, 115, 115, 101, 116,
				92, 84, 114, 117, 101, 83, 104, 97, 100, 111,
				119, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				83, 104, 97, 100, 111, 119, 70, 97, 99, 116,
				111, 114, 121, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 67, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				76, 101, 32, 84, 97, 105, 39, 115, 32, 65,
				115, 115, 101, 116, 92, 84, 114, 117, 101, 83,
				104, 97, 100, 111, 119, 92, 83, 99, 114, 105,
				112, 116, 115, 92, 83, 104, 97, 100, 111, 119,
				82, 101, 110, 100, 101, 114, 101, 114, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 80, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 76, 101, 32, 84, 97,
				105, 39, 115, 32, 65, 115, 115, 101, 116, 92,
				84, 114, 117, 101, 83, 104, 97, 100, 111, 119,
				92, 83, 99, 114, 105, 112, 116, 115, 92, 83,
				104, 97, 100, 111, 119, 82, 101, 110, 100, 101,
				114, 101, 114, 46, 77, 97, 115, 107, 72, 97,
				110, 100, 108, 105, 110, 103, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 74, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 76, 101, 32, 84, 97, 105, 39,
				115, 32, 65, 115, 115, 101, 116, 92, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 92, 83,
				99, 114, 105, 112, 116, 115, 92, 83, 104, 97,
				100, 111, 119, 83, 101, 116, 116, 105, 110, 103,
				83, 110, 97, 112, 115, 104, 111, 116, 46, 99,
				115, 0, 0, 0, 3, 0, 0, 0, 65, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 76, 101, 32, 84, 97,
				105, 39, 115, 32, 65, 115, 115, 101, 116, 92,
				84, 114, 117, 101, 83, 104, 97, 100, 111, 119,
				92, 83, 99, 114, 105, 112, 116, 115, 92, 83,
				104, 97, 100, 111, 119, 83, 111, 114, 116, 101,
				114, 46, 99, 115, 0, 0, 0, 2, 0, 0,
				0, 70, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 76, 101,
				32, 84, 97, 105, 39, 115, 32, 65, 115, 115,
				101, 116, 92, 84, 114, 117, 101, 83, 104, 97,
				100, 111, 119, 92, 83, 99, 114, 105, 112, 116,
				115, 92, 83, 104, 97, 100, 111, 119, 85, 112,
				100, 97, 116, 101, 71, 114, 111, 117, 112, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 58,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 76, 101, 32, 84,
				97, 105, 39, 115, 32, 65, 115, 115, 101, 116,
				92, 84, 114, 117, 101, 83, 104, 97, 100, 111,
				119, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				83, 104, 105, 109, 115, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 72, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 76, 101, 32, 84, 97, 105, 39, 115,
				32, 65, 115, 115, 101, 116, 92, 84, 114, 117,
				101, 83, 104, 97, 100, 111, 119, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 83, 116, 114, 117,
				99, 116, 117, 114, 101, 92, 66, 108, 101, 110,
				100, 77, 111, 100, 101, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 78, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 76, 101, 32, 84, 97, 105, 39, 115,
				32, 65, 115, 115, 101, 116, 92, 84, 114, 117,
				101, 83, 104, 97, 100, 111, 119, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 83, 116, 114, 117,
				99, 116, 117, 114, 101, 92, 83, 104, 97, 100,
				111, 119, 67, 111, 110, 116, 97, 105, 110, 101,
				114, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 63, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 76, 101,
				32, 84, 97, 105, 39, 115, 32, 65, 115, 115,
				101, 116, 92, 84, 114, 117, 101, 83, 104, 97,
				100, 111, 119, 92, 83, 99, 114, 105, 112, 116,
				115, 92, 84, 114, 117, 101, 83, 104, 97, 100,
				111, 119, 46, 99, 115, 0, 0, 0, 3, 0,
				0, 0, 75, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 76,
				101, 32, 84, 97, 105, 39, 115, 32, 65, 115,
				115, 101, 116, 92, 84, 114, 117, 101, 83, 104,
				97, 100, 111, 119, 92, 83, 99, 114, 105, 112,
				116, 115, 92, 84, 114, 117, 101, 83, 104, 97,
				100, 111, 119, 46, 73, 110, 118, 97, 108, 105,
				100, 97, 116, 111, 114, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 71, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 76, 101, 32, 84, 97, 105, 39, 115,
				32, 65, 115, 115, 101, 116, 92, 84, 114, 117,
				101, 83, 104, 97, 100, 111, 119, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 84, 114, 117, 101,
				83, 104, 97, 100, 111, 119, 46, 80, 108, 117,
				103, 105, 110, 115, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 70, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 76, 101, 32, 84, 97, 105, 39, 115, 32,
				65, 115, 115, 101, 116, 92, 84, 114, 117, 101,
				83, 104, 97, 100, 111, 119, 92, 83, 99, 114,
				105, 112, 116, 115, 92, 84, 114, 117, 101, 83,
				104, 97, 100, 111, 119, 77, 97, 110, 97, 103,
				101, 114, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 79, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 76,
				101, 32, 84, 97, 105, 39, 115, 32, 65, 115,
				115, 101, 116, 92, 84, 114, 117, 101, 83, 104,
				97, 100, 111, 119, 92, 83, 99, 114, 105, 112,
				116, 115, 92, 85, 116, 105, 108, 105, 116, 105,
				101, 115, 92, 69, 120, 116, 101, 110, 115, 105,
				111, 110, 77, 101, 116, 104, 111, 100, 115, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 71,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 76, 101, 32, 84,
				97, 105, 39, 115, 32, 65, 115, 115, 101, 116,
				92, 84, 114, 117, 101, 83, 104, 97, 100, 111,
				119, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				85, 116, 105, 108, 105, 116, 105, 101, 115, 92,
				72, 97, 115, 104, 67, 111, 100, 101, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 73, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 76, 101, 32, 84, 97,
				105, 39, 115, 32, 65, 115, 115, 101, 116, 92,
				84, 114, 117, 101, 83, 104, 97, 100, 111, 119,
				92, 83, 99, 114, 105, 112, 116, 115, 92, 85,
				116, 105, 108, 105, 116, 105, 101, 115, 92, 73,
				110, 100, 101, 120, 101, 100, 83, 101, 116, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 67,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 76, 101, 32, 84,
				97, 105, 39, 115, 32, 65, 115, 115, 101, 116,
				92, 84, 114, 117, 101, 83, 104, 97, 100, 111,
				119, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				85, 116, 105, 108, 105, 116, 105, 101, 115, 92,
				77, 97, 116, 104, 46, 99, 115, 0, 0, 0,
				2, 0, 0, 0, 75, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 76, 101, 32, 84, 97, 105, 39, 115, 32,
				65, 115, 115, 101, 116, 92, 84, 114, 117, 101,
				83, 104, 97, 100, 111, 119, 92, 83, 99, 114,
				105, 112, 116, 115, 92, 85, 116, 105, 108, 105,
				116, 105, 101, 115, 92, 79, 98, 106, 101, 99,
				116, 72, 97, 110, 100, 108, 101, 46, 99, 115,
				0, 0, 0, 3, 0, 0, 0, 87, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 76, 101, 32, 84, 97, 105,
				39, 115, 32, 65, 115, 115, 101, 116, 92, 84,
				114, 117, 101, 83, 104, 97, 100, 111, 119, 92,
				83, 99, 114, 105, 112, 116, 115, 92, 85, 116,
				105, 108, 105, 116, 105, 101, 115, 92, 80, 114,
				111, 112, 101, 114, 116, 121, 68, 114, 97, 119,
				101, 114, 65, 116, 116, 114, 105, 98, 117, 116,
				101, 115, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 71, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 76,
				101, 32, 84, 97, 105, 39, 115, 32, 65, 115,
				115, 101, 116, 92, 84, 114, 117, 101, 83, 104,
				97, 100, 111, 119, 92, 83, 99, 114, 105, 112,
				116, 115, 92, 85, 116, 105, 108, 105, 116, 105,
				101, 115, 92, 83, 104, 97, 100, 101, 114, 73,
				68, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 84, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 76, 101,
				32, 84, 97, 105, 39, 115, 32, 65, 115, 115,
				101, 116, 92, 84, 114, 117, 101, 83, 104, 97,
				100, 111, 119, 92, 83, 99, 114, 105, 112, 116,
				115, 92, 85, 116, 105, 108, 105, 116, 105, 101,
				115, 92, 83, 112, 114, 101, 97, 100, 83, 108,
				105, 100, 101, 114, 65, 116, 116, 114, 105, 98,
				117, 116, 101, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 70, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				76, 101, 32, 84, 97, 105, 39, 115, 32, 65,
				115, 115, 101, 116, 92, 84, 114, 117, 101, 83,
				104, 97, 100, 111, 119, 92, 83, 99, 114, 105,
				112, 116, 115, 92, 85, 116, 105, 108, 105, 116,
				105, 101, 115, 92, 85, 116, 105, 108, 105, 116,
				121, 46, 99, 115
			},
			TypesData = new byte[2355]
			{
				0, 0, 0, 0, 24, 76, 101, 84, 97, 105,
				46, 69, 102, 102, 101, 99, 116, 115, 124, 66,
				108, 117, 114, 67, 111, 110, 102, 105, 103, 0,
				0, 0, 0, 26, 76, 101, 84, 97, 105, 46,
				69, 102, 102, 101, 99, 116, 115, 124, 66, 108,
				117, 114, 72, 81, 67, 111, 110, 102, 105, 103,
				0, 0, 0, 0, 20, 76, 101, 84, 97, 105,
				46, 69, 102, 102, 101, 99, 116, 115, 124, 66,
				108, 117, 114, 72, 81, 0, 0, 0, 0, 28,
				76, 101, 84, 97, 105, 46, 69, 102, 102, 101,
				99, 116, 115, 124, 73, 66, 108, 117, 114, 65,
				108, 103, 111, 114, 105, 116, 104, 109, 0, 0,
				0, 0, 23, 76, 101, 84, 97, 105, 46, 69,
				102, 102, 101, 99, 116, 115, 124, 82, 101, 115,
				111, 117, 114, 99, 101, 115, 0, 0, 0, 0,
				26, 76, 101, 84, 97, 105, 46, 69, 102, 102,
				101, 99, 116, 115, 124, 83, 99, 97, 108, 97,
				98, 108, 101, 66, 108, 117, 114, 0, 0, 0,
				0, 32, 76, 101, 84, 97, 105, 46, 69, 102,
				102, 101, 99, 116, 115, 124, 83, 99, 97, 108,
				97, 98, 108, 101, 66, 108, 117, 114, 67, 111,
				110, 102, 105, 103, 0, 0, 0, 0, 30, 76,
				101, 84, 97, 105, 46, 69, 102, 102, 101, 99,
				116, 115, 124, 83, 104, 97, 100, 101, 114, 80,
				114, 111, 112, 101, 114, 116, 105, 101, 115, 0,
				0, 0, 0, 38, 76, 101, 84, 97, 105, 46,
				84, 114, 117, 101, 83, 104, 97, 100, 111, 119,
				124, 65, 110, 105, 109, 97, 116, 101, 100, 66,
				105, 83, 116, 97, 116, 101, 66, 117, 116, 116,
				111, 110, 0, 0, 0, 0, 41, 76, 101, 84,
				97, 105, 46, 84, 114, 117, 101, 83, 104, 97,
				100, 111, 119, 124, 84, 114, 117, 101, 83, 104,
				97, 100, 111, 119, 67, 117, 115, 116, 111, 109,
				77, 97, 116, 101, 114, 105, 97, 108, 0, 0,
				0, 0, 39, 76, 101, 84, 97, 105, 46, 84,
				114, 117, 101, 83, 104, 97, 100, 111, 119, 124,
				84, 114, 117, 101, 83, 104, 97, 100, 111, 119,
				68, 105, 115, 97, 98, 108, 101, 67, 97, 99,
				104, 101, 0, 0, 0, 0, 39, 76, 101, 84,
				97, 105, 46, 84, 114, 117, 101, 83, 104, 97,
				100, 111, 119, 124, 84, 114, 117, 101, 83, 104,
				97, 100, 111, 119, 73, 110, 115, 101, 116, 79,
				110, 80, 114, 101, 115, 115, 0, 0, 0, 0,
				47, 76, 101, 84, 97, 105, 46, 84, 114, 117,
				101, 83, 104, 97, 100, 111, 119, 124, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 73, 110,
				116, 101, 114, 97, 99, 116, 105, 111, 110, 65,
				110, 105, 109, 97, 116, 105, 111, 110, 0, 0,
				0, 0, 67, 76, 101, 84, 97, 105, 46, 84,
				114, 117, 101, 83, 104, 97, 100, 111, 119, 46,
				80, 108, 117, 103, 105, 110, 73, 110, 116, 101,
				114, 102, 97, 99, 101, 115, 124, 73, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 67, 97,
				115, 116, 101, 114, 77, 97, 116, 101, 114, 105,
				97, 108, 80, 114, 111, 118, 105, 100, 101, 114,
				0, 0, 0, 0, 74, 76, 101, 84, 97, 105,
				46, 84, 114, 117, 101, 83, 104, 97, 100, 111,
				119, 46, 80, 108, 117, 103, 105, 110, 73, 110,
				116, 101, 114, 102, 97, 99, 101, 115, 124, 73,
				84, 114, 117, 101, 83, 104, 97, 100, 111, 119,
				67, 97, 115, 116, 101, 114, 83, 117, 98, 77,
				101, 115, 104, 77, 97, 116, 101, 114, 105, 97,
				108, 80, 114, 111, 118, 105, 100, 101, 114, 0,
				0, 0, 0, 63, 76, 101, 84, 97, 105, 46,
				84, 114, 117, 101, 83, 104, 97, 100, 111, 119,
				46, 80, 108, 117, 103, 105, 110, 73, 110, 116,
				101, 114, 102, 97, 99, 101, 115, 124, 73, 84,
				114, 117, 101, 83, 104, 97, 100, 111, 119, 67,
				97, 115, 116, 101, 114, 77, 101, 115, 104, 80,
				114, 111, 118, 105, 100, 101, 114, 0, 0, 0,
				0, 63, 76, 101, 84, 97, 105, 46, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 46, 80,
				108, 117, 103, 105, 110, 73, 110, 116, 101, 114,
				102, 97, 99, 101, 115, 124, 73, 84, 114, 117,
				101, 83, 104, 97, 100, 111, 119, 67, 97, 115,
				116, 101, 114, 77, 101, 115, 104, 77, 111, 100,
				105, 102, 105, 101, 114, 0, 0, 0, 0, 77,
				76, 101, 84, 97, 105, 46, 84, 114, 117, 101,
				83, 104, 97, 100, 111, 119, 46, 80, 108, 117,
				103, 105, 110, 73, 110, 116, 101, 114, 102, 97,
				99, 101, 115, 124, 73, 84, 114, 117, 101, 83,
				104, 97, 100, 111, 119, 67, 97, 115, 116, 101,
				114, 77, 97, 116, 101, 114, 105, 97, 108, 80,
				114, 111, 112, 101, 114, 116, 105, 101, 115, 77,
				111, 100, 105, 102, 105, 101, 114, 0, 0, 0,
				0, 69, 76, 101, 84, 97, 105, 46, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 46, 80,
				108, 117, 103, 105, 110, 73, 110, 116, 101, 114,
				102, 97, 99, 101, 115, 124, 73, 84, 114, 117,
				101, 83, 104, 97, 100, 111, 119, 67, 97, 115,
				116, 101, 114, 67, 108, 101, 97, 114, 67, 111,
				108, 111, 114, 80, 114, 111, 118, 105, 100, 101,
				114, 0, 0, 0, 0, 69, 76, 101, 84, 97,
				105, 46, 84, 114, 117, 101, 83, 104, 97, 100,
				111, 119, 46, 80, 108, 117, 103, 105, 110, 73,
				110, 116, 101, 114, 102, 97, 99, 101, 115, 124,
				73, 84, 114, 117, 101, 83, 104, 97, 100, 111,
				119, 82, 101, 110, 100, 101, 114, 101, 114, 77,
				97, 116, 101, 114, 105, 97, 108, 80, 114, 111,
				118, 105, 100, 101, 114, 0, 0, 0, 0, 69,
				76, 101, 84, 97, 105, 46, 84, 114, 117, 101,
				83, 104, 97, 100, 111, 119, 46, 80, 108, 117,
				103, 105, 110, 73, 110, 116, 101, 114, 102, 97,
				99, 101, 115, 124, 73, 84, 114, 117, 101, 83,
				104, 97, 100, 111, 119, 82, 101, 110, 100, 101,
				114, 101, 114, 77, 97, 116, 101, 114, 105, 97,
				108, 77, 111, 100, 105, 102, 105, 101, 114, 0,
				0, 0, 0, 65, 76, 101, 84, 97, 105, 46,
				84, 114, 117, 101, 83, 104, 97, 100, 111, 119,
				46, 80, 108, 117, 103, 105, 110, 73, 110, 116,
				101, 114, 102, 97, 99, 101, 115, 124, 73, 84,
				114, 117, 101, 83, 104, 97, 100, 111, 119, 82,
				101, 110, 100, 101, 114, 101, 114, 77, 101, 115,
				104, 77, 111, 100, 105, 102, 105, 101, 114, 0,
				0, 0, 0, 63, 76, 101, 84, 97, 105, 46,
				84, 114, 117, 101, 83, 104, 97, 100, 111, 119,
				46, 80, 108, 117, 103, 105, 110, 73, 110, 116,
				101, 114, 102, 97, 99, 101, 115, 124, 73, 84,
				114, 117, 101, 83, 104, 97, 100, 111, 119, 67,
				117, 115, 116, 111, 109, 72, 97, 115, 104, 80,
				114, 111, 118, 105, 100, 101, 114, 0, 0, 0,
				0, 65, 76, 101, 84, 97, 105, 46, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 46, 80,
				108, 117, 103, 105, 110, 73, 110, 116, 101, 114,
				102, 97, 99, 101, 115, 124, 73, 84, 114, 117,
				101, 83, 104, 97, 100, 111, 119, 67, 117, 115,
				116, 111, 109, 72, 97, 115, 104, 80, 114, 111,
				118, 105, 100, 101, 114, 86, 50, 0, 0, 0,
				0, 32, 76, 101, 84, 97, 105, 46, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 124, 80,
				114, 111, 106, 101, 99, 116, 83, 101, 116, 116,
				105, 110, 103, 115, 0, 0, 0, 0, 28, 76,
				101, 84, 97, 105, 46, 84, 114, 117, 101, 83,
				104, 97, 100, 111, 119, 124, 81, 117, 105, 99,
				107, 80, 114, 101, 115, 101, 116, 0, 0, 0,
				0, 30, 76, 101, 84, 97, 105, 46, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 124, 83,
				104, 97, 100, 111, 119, 70, 97, 99, 116, 111,
				114, 121, 1, 0, 0, 0, 31, 76, 101, 84,
				97, 105, 46, 84, 114, 117, 101, 83, 104, 97,
				100, 111, 119, 124, 83, 104, 97, 100, 111, 119,
				82, 101, 110, 100, 101, 114, 101, 114, 1, 0,
				0, 0, 31, 76, 101, 84, 97, 105, 46, 84,
				114, 117, 101, 83, 104, 97, 100, 111, 119, 124,
				83, 104, 97, 100, 111, 119, 82, 101, 110, 100,
				101, 114, 101, 114, 0, 0, 0, 0, 38, 76,
				101, 84, 97, 105, 46, 84, 114, 117, 101, 83,
				104, 97, 100, 111, 119, 124, 83, 104, 97, 100,
				111, 119, 83, 101, 116, 116, 105, 110, 103, 83,
				110, 97, 112, 115, 104, 111, 116, 0, 0, 0,
				0, 29, 76, 101, 84, 97, 105, 46, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 124, 83,
				104, 97, 100, 111, 119, 83, 111, 114, 116, 101,
				114, 0, 0, 0, 0, 39, 76, 101, 84, 97,
				105, 46, 84, 114, 117, 101, 83, 104, 97, 100,
				111, 119, 46, 83, 104, 97, 100, 111, 119, 83,
				111, 114, 116, 101, 114, 124, 83, 111, 114, 116,
				69, 110, 116, 114, 121, 0, 0, 0, 0, 39,
				76, 101, 84, 97, 105, 46, 84, 114, 117, 101,
				83, 104, 97, 100, 111, 119, 46, 83, 104, 97,
				100, 111, 119, 83, 111, 114, 116, 101, 114, 124,
				83, 111, 114, 116, 71, 114, 111, 117, 112, 0,
				0, 0, 0, 18, 124, 83, 104, 97, 100, 111,
				119, 85, 112, 100, 97, 116, 101, 71, 114, 111,
				117, 112, 0, 0, 0, 0, 23, 124, 73, 83,
				104, 97, 100, 111, 119, 71, 114, 111, 117, 112,
				86, 105, 115, 105, 98, 105, 108, 105, 116, 121,
				0, 0, 0, 0, 22, 76, 101, 84, 97, 105,
				46, 84, 114, 117, 101, 83, 104, 97, 100, 111,
				119, 124, 83, 104, 105, 109, 115, 0, 0, 0,
				0, 36, 76, 101, 84, 97, 105, 46, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 124, 66,
				108, 101, 110, 100, 77, 111, 100, 101, 69, 120,
				116, 101, 110, 115, 105, 111, 110, 115, 0, 0,
				0, 0, 32, 76, 101, 84, 97, 105, 46, 84,
				114, 117, 101, 83, 104, 97, 100, 111, 119, 124,
				83, 104, 97, 100, 111, 119, 67, 111, 110, 116,
				97, 105, 110, 101, 114, 1, 0, 0, 0, 27,
				76, 101, 84, 97, 105, 46, 84, 114, 117, 101,
				83, 104, 97, 100, 111, 119, 124, 84, 114, 117,
				101, 83, 104, 97, 100, 111, 119, 0, 0, 0,
				0, 31, 76, 101, 84, 97, 105, 46, 84, 114,
				117, 101, 83, 104, 97, 100, 111, 119, 124, 73,
				67, 104, 97, 110, 103, 101, 84, 114, 97, 99,
				107, 101, 114, 0, 0, 0, 0, 30, 76, 101,
				84, 97, 105, 46, 84, 114, 117, 101, 83, 104,
				97, 100, 111, 119, 124, 67, 104, 97, 110, 103,
				101, 84, 114, 97, 99, 107, 101, 114, 1, 0,
				0, 0, 27, 76, 101, 84, 97, 105, 46, 84,
				114, 117, 101, 83, 104, 97, 100, 111, 119, 124,
				84, 114, 117, 101, 83, 104, 97, 100, 111, 119,
				1, 0, 0, 0, 27, 76, 101, 84, 97, 105,
				46, 84, 114, 117, 101, 83, 104, 97, 100, 111,
				119, 124, 84, 114, 117, 101, 83, 104, 97, 100,
				111, 119, 0, 0, 0, 0, 34, 76, 101, 84,
				97, 105, 46, 84, 114, 117, 101, 83, 104, 97,
				100, 111, 119, 124, 84, 114, 117, 101, 83, 104,
				97, 100, 111, 119, 77, 97, 110, 97, 103, 101,
				114, 0, 0, 0, 0, 22, 76, 101, 84, 97,
				105, 124, 69, 120, 116, 101, 110, 115, 105, 111,
				110, 77, 101, 116, 104, 111, 100, 115, 0, 0,
				0, 0, 15, 76, 101, 84, 97, 105, 124, 72,
				97, 115, 104, 85, 116, 105, 108, 115, 0, 0,
				0, 0, 27, 76, 101, 84, 97, 105, 46, 84,
				114, 117, 101, 83, 104, 97, 100, 111, 119, 124,
				73, 110, 100, 101, 120, 101, 100, 83, 101, 116,
				0, 0, 0, 0, 21, 76, 101, 84, 97, 105,
				46, 84, 114, 117, 101, 83, 104, 97, 100, 111,
				119, 124, 77, 97, 116, 104, 1, 0, 0, 0,
				29, 76, 101, 84, 97, 105, 46, 84, 114, 117,
				101, 83, 104, 97, 100, 111, 119, 124, 79, 98,
				106, 101, 99, 116, 72, 97, 110, 100, 108, 101,
				1, 0, 0, 0, 29, 76, 101, 84, 97, 105,
				46, 84, 114, 117, 101, 83, 104, 97, 100, 111,
				119, 124, 79, 98, 106, 101, 99, 116, 72, 97,
				110, 100, 108, 101, 0, 0, 0, 0, 30, 76,
				101, 84, 97, 105, 46, 84, 114, 117, 101, 83,
				104, 97, 100, 111, 119, 124, 75, 110, 111, 98,
				65, 116, 116, 114, 105, 98, 117, 116, 101, 0,
				0, 0, 0, 39, 76, 101, 84, 97, 105, 46,
				84, 114, 117, 101, 83, 104, 97, 100, 111, 119,
				124, 84, 111, 103, 103, 108, 101, 66, 117, 116,
				116, 111, 110, 115, 65, 116, 116, 114, 105, 98,
				117, 116, 101, 0, 0, 0, 0, 37, 76, 101,
				84, 97, 105, 46, 84, 114, 117, 101, 83, 104,
				97, 100, 111, 119, 124, 73, 110, 115, 101, 116,
				84, 111, 103, 103, 108, 101, 65, 116, 116, 114,
				105, 98, 117, 116, 101, 0, 0, 0, 0, 14,
				76, 101, 84, 97, 105, 124, 83, 104, 97, 100,
				101, 114, 73, 100, 0, 0, 0, 0, 38, 76,
				101, 84, 97, 105, 46, 84, 114, 117, 101, 83,
				104, 97, 100, 111, 119, 124, 83, 112, 114, 101,
				97, 100, 83, 108, 105, 100, 101, 114, 65, 116,
				116, 114, 105, 98, 117, 116, 101, 0, 0, 0,
				0, 13, 76, 101, 84, 97, 105, 124, 85, 116,
				105, 108, 105, 116, 121
			},
			TotalFiles = 37,
			TotalTypes = 56,
			IsEditorOnly = false
		};
	}
}
namespace LeTai
{
	public static class ExtensionMethods
	{
		private static Mesh fullscreenTriangle;

		private static Mesh FullscreenTriangle
		{
			get
			{
				if (fullscreenTriangle != null)
				{
					return fullscreenTriangle;
				}
				fullscreenTriangle = new Mesh
				{
					name = "Fullscreen Triangle"
				};
				fullscreenTriangle.SetVertices(new List<Vector3>
				{
					new Vector3(-1f, -1f, 0f),
					new Vector3(-1f, 3f, 0f),
					new Vector3(3f, -1f, 0f)
				});
				fullscreenTriangle.SetIndices(new int[3] { 0, 1, 2 }, MeshTopology.Triangles, 0, calculateBounds: false);
				fullscreenTriangle.UploadMeshData(markNoLongerReadable: false);
				return fullscreenTriangle;
			}
		}

		public static Vector4 ToMinMaxVector(this Rect self)
		{
			return new Vector4(self.xMin, self.yMin, self.xMax, self.yMax);
		}

		public static void BlitFullscreenTriangle(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material, int pass = 0)
		{
			cmd.SetGlobalTexture("_MainTex", source);
			cmd.SetRenderTarget(destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
			cmd.DrawMesh(FullscreenTriangle, Matrix4x4.identity, material, 0, pass);
		}

		internal static bool Approximately(this Rect self, Rect other)
		{
			if (QuickApproximate(self.x, other.x) && QuickApproximate(self.y, other.y) && QuickApproximate(self.width, other.width))
			{
				return QuickApproximate(self.height, other.height);
			}
			return false;
		}

		private static bool QuickApproximate(float a, float b)
		{
			return Mathf.Abs(b - a) < 1.175494E-38f;
		}

		public static Vector3 WithZ(this Vector2 self, float z)
		{
			return new Vector3(self.x, self.y, z);
		}

		public static Color WithA(this Color self, float a)
		{
			return new Color(self.r, self.g, self.b, a);
		}

		public static void NextFrames(this MonoBehaviour behaviour, Action action, int nFrames = 1)
		{
			behaviour.StartCoroutine(NextFrame(action, nFrames));
		}

		private static IEnumerator NextFrame(Action action, int nFrames)
		{
			for (int i = 0; i < nFrames; i++)
			{
				yield return null;
			}
			action();
		}

		public static void SetKeyword(this Material material, string keyword, bool enabled)
		{
			if (enabled)
			{
				material.EnableKeyword(keyword);
			}
			else
			{
				material.DisableKeyword(keyword);
			}
		}

		public static Vector2 Frac(this Vector2 vec)
		{
			return new Vector2(vec.x - Mathf.Floor(vec.x), vec.y - Mathf.Floor(vec.y));
		}

		public static Vector2 LocalToScreenPoint(this RectTransform rt, Vector3 localPoint, Camera referenceCamera = null)
		{
			return RectTransformUtility.WorldToScreenPoint(referenceCamera, rt.TransformPoint(localPoint));
		}

		public static Vector2 ScreenToCanvasSize(this RectTransform rt, Vector2 size, Camera referenceCamera = null)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Vector2.zero, referenceCamera, out var localPoint);
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, size, referenceCamera, out var localPoint2);
			return localPoint2 - localPoint;
		}
	}
	public static class HashUtils
	{
		public static int CombineHashCodes(int h1, int h2)
		{
			return ((h1 << 5) + h1) ^ h2;
		}

		public static int CombineHashCodes(int h1, int h2, int h3)
		{
			return CombineHashCodes(CombineHashCodes(h1, h2), h3);
		}

		public static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return CombineHashCodes(CombineHashCodes(h1, h2), CombineHashCodes(h3, h4));
		}

		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
		{
			return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), h5);
		}

		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), CombineHashCodes(h5, h6));
		}

		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), CombineHashCodes(h5, h6, h7));
		}

		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
		{
			return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), CombineHashCodes(h5, h6, h7, h8));
		}

		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9)
		{
			return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4, h5, h6, h7, h8), h9);
		}

		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10)
		{
			return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4, h5, h6, h7, h8), CombineHashCodes(h9, h10));
		}

		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11)
		{
			return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4, h5, h6, h7, h8), CombineHashCodes(h9, h10, h11));
		}

		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12)
		{
			return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4, h5, h6, h7, h8), CombineHashCodes(h9, h10, h11, h12));
		}

		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13)
		{
			return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4, h5, h6, h7, h8), CombineHashCodes(h9, h10, h11, h12, h13));
		}

		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14)
		{
			return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4, h5, h6, h7, h8), CombineHashCodes(h9, h10, h11, h12, h13, h14));
		}

		public static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8, int h9, int h10, int h11, int h12, int h13, int h14, int h15)
		{
			return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4, h5, h6, h7, h8), CombineHashCodes(h9, h10, h11, h12, h13, h14, h15));
		}
	}
	public static class ShaderId
	{
		public static readonly int MAIN_TEX = Shader.PropertyToID("_MainTex");

		public static readonly int BLUE_NOISE = Shader.PropertyToID("_BlueNoise");

		public static readonly int TARGET_SIZE = Shader.PropertyToID("_TargetSize");

		public static readonly int TMP_TEX = Shader.PropertyToID("_TrueShadow_TmpTex");

		public static readonly int WEIGHTS = Shader.PropertyToID("_Weights");

		public static readonly int OFFSETS = Shader.PropertyToID("_Offsets");

		public static readonly int EXTENT = Shader.PropertyToID("_Extent");

		public static readonly int SHADOW_TEX = Shader.PropertyToID("_ShadowTex");

		public static readonly int CLIP_RECT = Shader.PropertyToID("_ClipRect");

		public static readonly int TEXTURE_SAMPLE_ADD = Shader.PropertyToID("_TextureSampleAdd");

		public static readonly int COLOR_MASK = Shader.PropertyToID("_ColorMask");

		public static readonly int STENCIL_OP = Shader.PropertyToID("_StencilOp");

		public static readonly int STENCIL_ID = Shader.PropertyToID("_Stencil");

		public static readonly int STENCIL_READ_MASK = Shader.PropertyToID("_StencilReadMask");

		public static readonly int OFFSET = Shader.PropertyToID("_Offset");

		public static readonly int OVERFLOW_ALPHA = Shader.PropertyToID("_OverflowAlpha");

		public static readonly int ALPHA_MULTIPLIER = Shader.PropertyToID("_AlphaMultiplier");

		public static readonly int TIME = Shader.PropertyToID("_Time");

		public static readonly int SIN_TIME = Shader.PropertyToID("_SinTime");

		public static readonly int COS_TIME = Shader.PropertyToID("_CosTime");

		public static readonly int UNITY_DELTA_TIME = Shader.PropertyToID("unity_DeltaTime");

		public static readonly int SCREEN_PARAMS = Shader.PropertyToID("_ScreenParams");

		public static readonly int UI_VERTEX_COLOR_ALWAYS_GAMMA_SPACE = Shader.PropertyToID("_UIVertexColorAlwaysGammaSpace");

		public static readonly int SCALE_X = Shader.PropertyToID("_ScaleX");

		public static readonly int SCALE_Y = Shader.PropertyToID("_ScaleY");
	}
	public static class Utility
	{
		public static void LogList<T>(IEnumerable<T> list, Func<T, object> getData)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (T item in list)
			{
				stringBuilder.Append(num + ":    ");
				stringBuilder.Append(getData(item).ToString());
				stringBuilder.Append("\n");
				num++;
			}
			UnityEngine.Debug.Log(stringBuilder.ToString());
		}

		public static int SimplePingPong(int t, int max)
		{
			if (t > max)
			{
				return 2 * max - t;
			}
			return t;
		}

		public static void SafeDestroy(UnityEngine.Object obj)
		{
			if (!(obj != null))
			{
				return;
			}
			if (Application.isPlaying)
			{
				if (obj is GameObject gameObject)
				{
					gameObject.transform.parent = null;
				}
				UnityEngine.Object.Destroy(obj);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
		}
	}
}
namespace LeTai.TrueShadow
{
	public class AnimatedBiStateButton : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
	{
		public enum State
		{
			Up,
			AnimateDown,
			Down,
			AnimateUp
		}

		public float animationDuration = 0.1f;

		public AnimationCurve animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		[Tooltip("Animate if the pointer is hovered over the element while already pressed")]
		public bool useEnterExitEvents = true;

		protected State state;

		protected float pressAmount;

		protected bool IsAnimating
		{
			get
			{
				if (state != State.AnimateDown)
				{
					return state == State.AnimateUp;
				}
				return true;
			}
		}

		public event Action willPress;

		public event Action willRelease;

		private void Update()
		{
			PollPointerUp();
			DoAnimation();
		}

		private void DoAnimation()
		{
			if (IsAnimating)
			{
				if (state == State.AnimateDown)
				{
					pressAmount += Time.deltaTime / animationDuration;
				}
				else if (state == State.AnimateUp)
				{
					pressAmount -= Time.deltaTime / animationDuration;
				}
				pressAmount = Mathf.Clamp01(pressAmount);
				float num = pressAmount;
				if (state == State.AnimateUp)
				{
					num = 1f - num;
				}
				num = animationCurve.Evaluate(num);
				if (state == State.AnimateUp)
				{
					num = 1f - num;
				}
				Animate(num);
				if (state == State.AnimateDown && pressAmount == 1f)
				{
					state = State.Down;
				}
				if (state == State.AnimateUp && pressAmount == 0f)
				{
					state = State.Up;
				}
			}
		}

		protected void Press()
		{
			if (state != State.Down && state != State.AnimateDown)
			{
				OnWillPress();
				state = State.AnimateDown;
			}
		}

		protected void Release()
		{
			if (state != State.Up && state != State.AnimateUp)
			{
				OnWillRelease();
				state = State.AnimateUp;
			}
		}

		private void PollPointerUp()
		{
			if (useEnterExitEvents && (state == State.Down || state == State.AnimateDown) && !Input.GetMouseButton(0))
			{
				Release();
			}
		}

		protected virtual void Animate(float visualPressAmount)
		{
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			Press();
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			Release();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (useEnterExitEvents && Input.GetMouseButton(0))
			{
				Press();
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (useEnterExitEvents)
			{
				Release();
			}
		}

		protected virtual void OnWillPress()
		{
			this.willPress?.Invoke();
		}

		protected virtual void OnWillRelease()
		{
			this.willRelease?.Invoke();
		}
	}
	[AddComponentMenu("UI/True Shadow/True Shadow Custom Material")]
	[ExecuteAlways]
	public class TrueShadowCustomMaterial : MonoBehaviour, ITrueShadowRendererMaterialProvider
	{
		public Material material;

		public event Action materialReplaced;

		public event Action materialModified;

		public Material GetTrueShadowRendererMaterial()
		{
			if (!base.isActiveAndEnabled)
			{
				return null;
			}
			return material;
		}

		private void OnEnable()
		{
			TrueShadow component = GetComponent<TrueShadow>();
			if ((bool)component)
			{
				component.RefreshPlugins();
			}
			this.materialReplaced?.Invoke();
		}

		private void OnDisable()
		{
			TrueShadow component = GetComponent<TrueShadow>();
			if ((bool)component)
			{
				component.RefreshPlugins();
			}
			this.materialReplaced?.Invoke();
		}

		private void OnValidate()
		{
			this.materialReplaced?.Invoke();
		}

		public void OnMaterialModified()
		{
			this.materialModified?.Invoke();
		}
	}
	[AddComponentMenu("UI/True Shadow/True Shadow Disable Cache")]
	[ExecuteAlways]
	[RequireComponent(typeof(TrueShadow))]
	public class TrueShadowDisableCache : MonoBehaviour, ITrueShadowCustomHashProviderV2
	{
		[Tooltip("Force shadow to refresh every frame. Useful for animated effects")]
		public bool everyFrame;

		[Tooltip("Some 3rd party effects doesn't mark the Graphic as dirty when modifying them. Try this if the above doesn't work")]
		public bool markGraphicDirty;

		private Graphic _graphic;

		public event Action<int> trueShadowCustomHashChanged;

		private void OnEnable()
		{
			_graphic = GetComponent<Graphic>();
			Canvas.preWillRenderCanvases += PreWillRenderCanvases;
		}

		private void PreWillRenderCanvases()
		{
			Canvas.preWillRenderCanvases -= PreWillRenderCanvases;
			Dirty();
		}

		private void Update()
		{
			if (everyFrame)
			{
				Dirty();
			}
		}

		private void Dirty()
		{
			if (markGraphicDirty)
			{
				_graphic.SetAllDirty();
			}
			this.trueShadowCustomHashChanged?.Invoke(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
		}

		private void OnDisable()
		{
			this.trueShadowCustomHashChanged?.Invoke(0);
		}
	}
	[AddComponentMenu("UI/True Shadow/True Shadow Inset On Press")]
	[RequireComponent(typeof(TrueShadow))]
	public class TrueShadowInsetOnPress : AnimatedBiStateButton
	{
		private TrueShadow[] _shadows;

		private float[] _normalOpacity;

		private bool _wasInset;

		private void OnEnable()
		{
			_shadows = GetComponents<TrueShadow>();
			_normalOpacity = new float[_shadows.Length];
		}

		protected override void Animate(float visualPressAmount)
		{
			bool flag = visualPressAmount > 0.5f;
			if (flag != _wasInset)
			{
				for (int i = 0; i < _shadows.Length; i++)
				{
					_shadows[i].Inset = flag;
				}
				_wasInset = flag;
			}
			if (flag)
			{
				SetAllOpacity(visualPressAmount * 2f - 1f);
			}
			else
			{
				SetAllOpacity(1f - visualPressAmount * 2f);
			}
			void SetAllOpacity(float lerpProgress)
			{
				for (int j = 0; j < _shadows.Length; j++)
				{
					Color color = _shadows[j].Color;
					color.a = Mathf.Lerp(0f, _normalOpacity[j], lerpProgress);
					_shadows[j].Color = color;
				}
			}
		}

		private void MemorizeOpacity()
		{
			if (!base.IsAnimating)
			{
				for (int i = 0; i < _shadows.Length; i++)
				{
					_normalOpacity[i] = _shadows[i].Color.a;
				}
			}
		}

		protected override void OnWillPress()
		{
			_wasInset = _shadows[0].Inset;
			MemorizeOpacity();
			base.OnWillPress();
		}
	}
	[AddComponentMenu("UI/True Shadow/True Shadow Interaction Animation")]
	[RequireComponent(typeof(TrueShadow))]
	public class TrueShadowInteractionAnimation : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerDownHandler, IPointerUpHandler
	{
		public float smoothTime = 0.05f;

		[Tooltip("Deselect on pointer up")]
		public bool autoDeselect;

		[Header("Size")]
		public float selectedSize = 28f;

		public float hoverSize = 28f;

		public float clickedSize = 24f;

		[Header("Distance")]
		public float selectedDistance = 12f;

		public float hoverDistance = 12f;

		public float clickedDistance = 8f;

		[Header("Color")]
		public Color selectedColor = new Color(0f, 0f, 0f, 0.25f);

		public Color hoverColor = new Color(0f, 0f, 0f, 0.2f);

		public Color clickedColor = new Color(0f, 0f, 0f, 0.25f);

		private float normalSize;

		private float normalDistance;

		private Color normalColor;

		private bool normalStateAcquired;

		private bool isSelected;

		private bool isHovered;

		private bool isClicked;

		private TrueShadow shadow;

		private Selectable selectable;

		private float targetSize;

		private float targetDistance;

		private Color targetColor;

		private static readonly Color FADED_COLOR = new Color(0.5f, 0.5f, 0.5f, 0.5f);

		private readonly List<RaycastResult> raycastResults = new List<RaycastResult>();

		private float currentSizeVelocity;

		private float currentDistanceVelocity;

		private float currentColorRVelocity;

		private float currentColorGVelocity;

		private float currentColorBVelocity;

		private float currentColorAVelocity;

		private void OnEnable()
		{
			shadow = FindTrueShadow();
			selectable = GetComponent<Selectable>();
			isHovered = false;
			if (Input.mousePresent)
			{
				isHovered = IsOverGameObject(Input.mousePosition);
			}
			if (!isHovered)
			{
				for (int i = 0; i < Input.touchCount; i++)
				{
					isHovered = IsOverGameObject(Input.GetTouch(i).position);
					if (isHovered)
					{
						break;
					}
				}
			}
			isSelected = !autoDeselect && EventSystem.current.currentSelectedGameObject == base.gameObject;
			isClicked = false;
			if (!normalStateAcquired)
			{
				targetSize = (normalSize = shadow.Size);
				targetDistance = (normalDistance = shadow.OffsetDistance);
				targetColor = (normalColor = shadow.Color);
				normalStateAcquired = true;
			}
			shadow.Size = (targetSize = normalSize);
			shadow.OffsetDistance = (targetDistance = normalDistance);
		}

		private TrueShadow FindTrueShadow()
		{
			TrueShadow[] components = GetComponents<TrueShadow>();
			if (components.Length == 0)
			{
				return null;
			}
			TrueShadowInteractionAnimation[] components2 = GetComponents<TrueShadowInteractionAnimation>();
			int i;
			for (i = 0; i < components2.Length && !(components2[i] == this); i++)
			{
			}
			return components[i];
		}

		private void OnStateChange()
		{
			if (isClicked)
			{
				targetSize = clickedSize;
				targetDistance = clickedDistance;
				targetColor = clickedColor;
			}
			else if (isSelected)
			{
				targetSize = selectedSize;
				targetDistance = selectedDistance;
				targetColor = selectedColor;
			}
			else if (isHovered)
			{
				targetSize = hoverSize;
				targetDistance = hoverDistance;
				targetColor = hoverColor;
			}
			else
			{
				targetSize = normalSize;
				targetDistance = normalDistance;
				targetColor = normalColor;
			}
		}

		private void Update()
		{
			if (!Mathf.Approximately(targetSize, shadow.Size))
			{
				shadow.Size = Mathf.SmoothDamp(shadow.Size, targetSize, ref currentSizeVelocity, smoothTime);
			}
			if (!Mathf.Approximately(targetDistance, shadow.OffsetDistance))
			{
				shadow.OffsetDistance = Mathf.SmoothDamp(shadow.OffsetDistance, targetDistance, ref currentDistanceVelocity, smoothTime);
			}
			Color color = shadow.Color;
			if (!Mathf.Approximately(targetColor.a, color.a))
			{
				float r = Mathf.SmoothDamp(color.r, targetColor.r, ref currentColorRVelocity, smoothTime);
				float g = Mathf.SmoothDamp(color.g, targetColor.g, ref currentColorGVelocity, smoothTime);
				float b = Mathf.SmoothDamp(color.b, targetColor.b, ref currentColorBVelocity, smoothTime);
				float a = Mathf.SmoothDamp(color.a, targetColor.a, ref currentColorAVelocity, smoothTime);
				shadow.Color = new Color(r, g, b, a);
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			isHovered = true;
			OnStateChange();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			isHovered = false;
			OnStateChange();
		}

		public void OnSelect(BaseEventData eventData)
		{
			isSelected = true;
			OnStateChange();
		}

		public void OnDeselect(BaseEventData eventData)
		{
			isSelected = false;
			OnStateChange();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			isClicked = true;
			OnStateChange();
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (autoDeselect && EventSystem.current.currentSelectedGameObject == base.gameObject)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
			isClicked = false;
			OnStateChange();
		}

		private bool IsOverGameObject(Vector2 position)
		{
			PointerEventData eventData = new PointerEventData(EventSystem.current)
			{
				position = position
			};
			EventSystem.current.RaycastAll(eventData, raycastResults);
			for (int i = 0; i < raycastResults.Count; i++)
			{
				if (raycastResults[i].gameObject == base.gameObject)
				{
					return true;
				}
			}
			return false;
		}
	}
	public class ProjectSettings : ScriptableObject
	{
		public const string DEFAULT_RESOURCE_PATH = "True Shadow Project Settings Default";

		public const string RESOURCE_PATH = "True Shadow Project Settings";

		private static ProjectSettings instance;

		[SerializeField]
		internal bool useGlobalAngleByDefault;

		[Knob]
		[SerializeField]
		internal float globalAngle = 90f;

		[SerializeField]
		internal bool showQuickPresetsButtons = true;

		[SerializeField]
		internal List<QuickPreset> quickPresets = new List<QuickPreset>(8);

		public static ProjectSettings Instance
		{
			get
			{
				if (!instance)
				{
					instance = UnityEngine.Resources.Load<ProjectSettings>("True Shadow Project Settings");
				}
				if (!instance)
				{
					UnityEngine.Debug.LogError("Can't find \"True Shadow Project Settings\" in a Resources folder. Please restore the file or re-install True Shadow.");
				}
				return instance;
			}
		}

		public bool UseGlobalAngleByDefault
		{
			get
			{
				return useGlobalAngleByDefault;
			}
			set
			{
				useGlobalAngleByDefault = value;
			}
		}

		public float GlobalAngle
		{
			get
			{
				return globalAngle;
			}
			set
			{
				globalAngle = value;
				this.globalAngleChanged?.Invoke(globalAngle);
			}
		}

		public bool ShowQuickPresetsButtons
		{
			get
			{
				return showQuickPresetsButtons;
			}
			private set
			{
				showQuickPresetsButtons = value;
			}
		}

		public List<QuickPreset> QuickPresets
		{
			get
			{
				return quickPresets;
			}
			set
			{
				quickPresets = value;
			}
		}

		public event Action<float> globalAngleChanged;
	}
	[Serializable]
	public struct QuickPreset
	{
		public string name;

		[Min(0f)]
		public float size;

		[SpreadSlider]
		public float spread;

		[Min(0f)]
		public float distance;

		[Range(0f, 1f)]
		public float alpha;

		public QuickPreset(string name, float size, float spread, float distance, float alpha)
		{
			this.name = name;
			this.size = size;
			this.spread = spread;
			this.distance = distance;
			this.alpha = alpha;
		}

		public void Apply(TrueShadow target)
		{
			target.Size = size;
			target.Spread = spread;
			target.OffsetDistance = distance;
			Color color = target.Color;
			color.a = alpha;
			target.Color = color;
		}
	}
	public class ShadowFactory
	{
		private static ShadowFactory instance;

		private readonly Dictionary<int, ShadowContainer> shadowCache = new Dictionary<int, ShadowContainer>();

		private readonly CommandBuffer cmd;

		private readonly MaterialPropertyBlock materialProps;

		private readonly ScalableBlur blurProcessorFast;

		private readonly BlurHQ blurProcessorAccurate;

		private readonly BlurConfig blurConfigFast;

		private readonly BlurConfig blurConfigAccurate;

		private Material cutoutMaterial;

		private Material imprintPostProcessMaterial;

		private Material shadowPostProcessMaterial;

		private static readonly Rect UNIT_RECT = new Rect(0f, 0f, 1f, 1f);

		private static readonly Vector4 ALPHA8_TEXTURE_BIAS = new Vector4(1f, 1f, 1f, 0f);

		public static ShadowFactory Instance => instance ?? (instance = new ShadowFactory());

		public int CachedCount => shadowCache.Count;

		private Material CutoutMaterial
		{
			get
			{
				if (!cutoutMaterial)
				{
					return cutoutMaterial = new Material(Shader.Find("Hidden/TrueShadow/Cutout"));
				}
				return cutoutMaterial;
			}
		}

		private Material ImprintPostProcessMaterial
		{
			get
			{
				if (!imprintPostProcessMaterial)
				{
					return imprintPostProcessMaterial = new Material(Shader.Find("Hidden/TrueShadow/ImprintPostProcess"));
				}
				return imprintPostProcessMaterial;
			}
		}

		private Material ShadowPostProcessMaterial
		{
			get
			{
				if (!shadowPostProcessMaterial)
				{
					return shadowPostProcessMaterial = new Material(Shader.Find("Hidden/TrueShadow/PostProcess"));
				}
				return shadowPostProcessMaterial;
			}
		}

		private ShadowFactory()
		{
			cmd = new CommandBuffer
			{
				name = "Shadow Commands"
			};
			materialProps = new MaterialPropertyBlock();
			ShaderProperties.Init(8);
			blurConfigFast = ScriptableObject.CreateInstance<ScalableBlurConfig>();
			blurConfigFast.hideFlags = HideFlags.HideAndDontSave;
			blurProcessorFast = new ScalableBlur();
			blurProcessorFast.Configure(blurConfigFast);
			blurConfigAccurate = ScriptableObject.CreateInstance<BlurHQConfig>();
			blurConfigAccurate.hideFlags = HideFlags.HideAndDontSave;
			blurProcessorAccurate = new BlurHQ();
			blurProcessorAccurate.Configure(blurConfigAccurate);
		}

		internal void Get(ShadowSettingSnapshot snapshot, ref ShadowContainer container)
		{
			if (float.IsNaN(snapshot.dimensions.x) || snapshot.dimensions.x < 1f || float.IsNaN(snapshot.dimensions.y) || snapshot.dimensions.y < 1f || !snapshot.shadow.Graphic.materialForRendering || (snapshot.size == 0f && snapshot.canvasRelativeOffset.sqrMagnitude == 0f))
			{
				ReleaseContainer(ref container);
				return;
			}
			float num = snapshot.size * 2f;
			if (snapshot.dimensions.x + num > 4097f || snapshot.dimensions.y + num > 4097f)
			{
				UnityEngine.Debug.LogWarning("Requested Shadow is too large");
				return;
			}
			int hashCode = snapshot.GetHashCode();
			ShadowContainer obj = container;
			if (obj == null || obj.requestHash != hashCode)
			{
				ReleaseContainer(ref container);
				if (shadowCache.TryGetValue(hashCode, out var value))
				{
					value.RefCount++;
					container = value;
				}
				else
				{
					ShadowContainer shadowContainer = (shadowCache[hashCode] = GenerateShadow(snapshot));
					container = shadowContainer;
				}
			}
		}

		internal void ReleaseContainer(ref ShadowContainer container)
		{
			if (container != null && --container.RefCount <= 0)
			{
				RenderTexture.ReleaseTemporary(container.Texture);
				shadowCache.Remove(container.requestHash);
				container = null;
			}
		}

		private ShadowContainer GenerateShadow(ShadowSettingSnapshot snapshot)
		{
			cmd.Clear();
			cmd.BeginSample("TrueShadow:Capture");
			Mesh obj = snapshot.shadow.SpriteMeshHandle.obj;
			Bounds bounds = obj.bounds;
			BlurConfig blurConfig = ((snapshot.algorithm == BlurAlgorithmSelection.Fast) ? blurConfigFast : blurConfigAccurate);
			IBlurAlgorithm blurAlgorithm2;
			if (snapshot.algorithm != BlurAlgorithmSelection.Fast)
			{
				IBlurAlgorithm blurAlgorithm = blurProcessorAccurate;
				blurAlgorithm2 = blurAlgorithm;
			}
			else
			{
				IBlurAlgorithm blurAlgorithm = blurProcessorFast;
				blurAlgorithm2 = blurAlgorithm;
			}
			IBlurAlgorithm blurAlgorithm3 = blurAlgorithm2;
			int num = (snapshot.shadow.Inset ? System.Math.Max(Mathf.CeilToInt(Mathf.Max(Mathf.Abs(snapshot.canvasRelativeOffset.x), Mathf.Abs(snapshot.canvasRelativeOffset.y))), blurConfig.MinExtent) : Mathf.CeilToInt(snapshot.size));
			if (snapshot.algorithm == BlurAlgorithmSelection.Fast)
			{
				num += ((snapshot.size < 50f) ? 4 : 0);
			}
			Vector2Int vector2Int = Vector2Int.CeilToInt(snapshot.dimensions);
			int x = vector2Int.x;
			int y = vector2Int.y;
			int num2 = num * 2;
			x += num2;
			y += num2;
			RenderTexture temporary = RenderTexture.GetTemporary(x, y, 0, RenderTextureFormat.ARGB32);
			RenderTextureDescriptor descriptor = temporary.descriptor;
			RenderTexture temporary2 = RenderTexture.GetTemporary(descriptor);
			RenderTexture renderTexture = null;
			bool flag = snapshot.shadow.IgnoreCasterColor || snapshot.shadow.Inset;
			if (flag)
			{
				renderTexture = RenderTexture.GetTemporary(descriptor);
			}
			Texture content = snapshot.shadow.Content;
			materialProps.Clear();
			materialProps.SetVector(ShaderId.CLIP_RECT, new Vector4(float.NegativeInfinity, float.NegativeInfinity, float.PositiveInfinity, float.PositiveInfinity));
			materialProps.SetInt(ShaderId.COLOR_MASK, 15);
			if (obj.subMeshCount <= 1)
			{
				if ((bool)content)
				{
					materialProps.SetTexture(ShaderId.MAIN_TEX, content);
					if (content is Texture2D { format: TextureFormat.Alpha8 })
					{
						materialProps.SetVector(ShaderId.TEXTURE_SAMPLE_ADD, ALPHA8_TEXTURE_BIAS);
					}
					else
					{
						materialProps.SetVector(ShaderId.TEXTURE_SAMPLE_ADD, Vector4.zero);
					}
				}
				else
				{
					materialProps.SetTexture(ShaderId.MAIN_TEX, Texture2D.whiteTexture);
				}
			}
			cmd.SetRenderTarget(temporary2);
			cmd.ClearRenderTarget(clearDepth: true, clearColor: true, snapshot.shadow.ClearColor);
			cmd.SetViewport(new Rect(new Vector2(num, num), vector2Int));
			cmd.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.Ortho(bounds.min.x, bounds.max.x, bounds.min.y, bounds.max.y, -1f, 1f));
			float timeSinceLevelLoad = Time.timeSinceLevelLoad;
			materialProps.SetVector(ShaderId.TIME, new Vector4(timeSinceLevelLoad / 20f, timeSinceLevelLoad, timeSinceLevelLoad * 2f, timeSinceLevelLoad * 3f));
			materialProps.SetVector(ShaderId.SIN_TIME, new Vector4(Mathf.Sin(timeSinceLevelLoad / 8f), Mathf.Sin(timeSinceLevelLoad / 4f), Mathf.Sin(timeSinceLevelLoad / 2f), Mathf.Sin(timeSinceLevelLoad)));
			materialProps.SetVector(ShaderId.COS_TIME, new Vector4(Mathf.Cos(timeSinceLevelLoad / 8f), Mathf.Cos(timeSinceLevelLoad / 4f), Mathf.Cos(timeSinceLevelLoad / 2f), Mathf.Cos(timeSinceLevelLoad)));
			materialProps.SetVector(ShaderId.UNITY_DELTA_TIME, new Vector4(Time.deltaTime, 1f / Time.deltaTime, Time.smoothDeltaTime, 1f / Time.smoothDeltaTime));
			materialProps.SetInt(ShaderId.UI_VERTEX_COLOR_ALWAYS_GAMMA_SPACE, 1);
			if (snapshot.shadow.Graphic is TextMeshProUGUI || snapshot.shadow.Graphic is TMP_SubMeshUI)
			{
				Vector3 lossyScale = snapshot.canvas.transform.lossyScale;
				materialProps.SetFloat(ShaderId.SCALE_X, 1f / lossyScale.x);
				materialProps.SetFloat(ShaderId.SCALE_Y, 1f / lossyScale.y);
				materialProps.SetVector(ShaderId.SCREEN_PARAMS, new Vector4(x, y, 1f + 1f / (float)x, 1f + 1f / (float)y));
			}
			snapshot.shadow.ModifyShadowCastingMaterialProperties(materialProps);
			if (obj.subMeshCount == 1)
			{
				Material shadowCastingMaterial = snapshot.shadow.GetShadowCastingMaterial();
				for (int i = 0; i < shadowCastingMaterial.passCount; i++)
				{
					cmd.DrawMesh(obj, Matrix4x4.identity, shadowCastingMaterial, 0, i, materialProps);
				}
			}
			else
			{
				for (int j = 0; j < obj.subMeshCount; j++)
				{
					Material shadowCastingMaterialForSubMesh = snapshot.shadow.GetShadowCastingMaterialForSubMesh(j);
					for (int k = 0; k < shadowCastingMaterialForSubMesh.passCount; k++)
					{
						cmd.DrawMesh(obj, Matrix4x4.identity, shadowCastingMaterialForSubMesh, j, k, materialProps);
					}
				}
			}
			if (flag)
			{
				ImprintPostProcessMaterial.SetKeyword("BLEACH", snapshot.shadow.IgnoreCasterColor);
				ImprintPostProcessMaterial.SetKeyword("INSET", snapshot.shadow.Inset);
				cmd.Blit(temporary2, renderTexture, ImprintPostProcessMaterial);
			}
			cmd.EndSample("TrueShadow:Capture");
			bool num3 = (double)snapshot.shadow.Spread > 0.001;
			cmd.BeginSample("TrueShadow:Cast");
			RenderTexture renderTexture2 = (flag ? renderTexture : temporary2);
			RenderTexture renderTexture3 = ((!num3) ? temporary : RenderTexture.GetTemporary(temporary.descriptor));
			if ((double)snapshot.size < 0.01)
			{
				cmd.Blit(renderTexture2, renderTexture3);
			}
			else
			{
				blurConfig.Strength = snapshot.size;
				blurAlgorithm3.Blur(cmd, renderTexture2, UNIT_RECT, renderTexture3);
			}
			cmd.EndSample("TrueShadow:Cast");
			Vector2 vector = new Vector2(snapshot.canvasRelativeOffset.x / (float)x, snapshot.canvasRelativeOffset.y / (float)y);
			int num4 = (snapshot.shadow.Inset ? 1 : 0);
			if (num3)
			{
				cmd.BeginSample("TrueShadow:PostProcess");
				ShadowPostProcessMaterial.SetTexture(ShaderId.SHADOW_TEX, renderTexture3);
				ShadowPostProcessMaterial.SetVector(ShaderId.OFFSET, vector);
				ShadowPostProcessMaterial.SetFloat(ShaderId.OVERFLOW_ALPHA, num4);
				ShadowPostProcessMaterial.SetFloat(ShaderId.ALPHA_MULTIPLIER, 1f / Mathf.Max(1.5259022E-05f, 1f - snapshot.shadow.Spread));
				cmd.SetViewport(UNIT_RECT);
				cmd.Blit(renderTexture2, temporary, ShadowPostProcessMaterial);
				cmd.EndSample("TrueShadow:PostProcess");
			}
			else if (snapshot.shadow.Cutout)
			{
				cmd.BeginSample("TrueShadow:Cutout");
				CutoutMaterial.SetVector(ShaderId.OFFSET, vector);
				CutoutMaterial.SetFloat(ShaderId.OVERFLOW_ALPHA, num4);
				cmd.SetViewport(UNIT_RECT);
				cmd.Blit(renderTexture2, temporary, CutoutMaterial);
				cmd.EndSample("TrueShadow:Cutout");
			}
			Graphics.ExecuteCommandBuffer(cmd);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(renderTexture2);
			if (num3)
			{
				RenderTexture.ReleaseTemporary(renderTexture3);
			}
			return new ShadowContainer(temporary, snapshot, num, vector2Int);
		}

		private RenderTexture GenColoredTexture(int hash)
		{
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.SetPixels32(new Color32[1]
			{
				new Color32((byte)(hash >> 8), (byte)(hash >> 16), (byte)(hash >> 24), byte.MaxValue)
			});
			texture2D.Apply();
			RenderTexture temporary = RenderTexture.GetTemporary(1, 1);
			Graphics.Blit(texture2D, temporary);
			return temporary;
		}
	}
	[AddComponentMenu("")]
	[ExecuteAlways]
	public class ShadowRenderer : MonoBehaviour, ILayoutIgnorer, IMaterialModifier, IMeshModifier
	{
		private static int reDrawAllFrameIndex = -1;

		private TrueShadow shadow;

		private RectTransform rt;

		private RawImage graphic;

		private Texture shadowTexture;

		private bool willBeDestroyed;

		private static readonly Dictionary<int, Material> MASK_MATERIALS_CACHE = new Dictionary<int, Material>();

		public bool ignoreLayout => true;

		internal CanvasRenderer CanvasRenderer { get; private set; }

		[Conditional("UNITY_EDITOR")]
		internal static void QueueReDrawAll()
		{
			reDrawAllFrameIndex = Time.frameCount + 1;
		}

		public static void Initialize(TrueShadow shadow, ref ShadowRenderer renderer)
		{
			if ((bool)renderer && renderer.shadow == shadow)
			{
				renderer.gameObject.SetActive(value: true);
				return;
			}
			string text = shadow.gameObject.name + "'s Shadow";
			HideFlags hideFlags = HideFlags.HideAndDontSave;
			GameObject gameObject = new GameObject(text)
			{
				hideFlags = hideFlags
			};
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			gameObject.AddComponent<CanvasRenderer>();
			RawImage rawImage = gameObject.AddComponent<RawImage>();
			renderer = gameObject.AddComponent<ShadowRenderer>();
			shadow.SetHierachyDirty();
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.zero;
			rawImage.raycastTarget = false;
			rawImage.color = shadow.Color;
			renderer.shadow = shadow;
			renderer.rt = rectTransform;
			renderer.graphic = rawImage;
			renderer.UpdateMaterial();
			renderer.CanvasRenderer = gameObject.GetComponent<CanvasRenderer>();
			renderer.CanvasRenderer.SetColor(shadow.IgnoreCasterColor ? Color.white : shadow.CanvasRenderer.GetColor());
			renderer.CanvasRenderer.SetAlpha(shadow.CanvasRenderer.GetAlpha());
			renderer.ReLayout();
		}

		public void UpdateMaterial()
		{
			if (!graphic)
			{
				return;
			}
			if (shadow.Graphic is MaskableGraphic maskableGraphic)
			{
				graphic.maskable = maskableGraphic.maskable;
			}
			if (CanvasUpdateRegistry.IsRebuildingGraphics())
			{
				this.NextFrames(delegate
				{
					graphic.material = shadow.GetShadowRenderingMaterial();
				});
			}
			else
			{
				graphic.material = shadow.GetShadowRenderingMaterial();
			}
		}

		internal void ReLayout()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			RectTransform rectTransform = shadow.RectTransform;
			Mesh obj = shadow.SpriteMeshHandle.obj;
			if (!rectTransform || !shadowTexture || !obj)
			{
				CanvasRenderer.SetAlpha(0f);
				return;
			}
			bool flag = !shadow.DisableFitCompensation && !(shadow.Graphic is Text) && !(shadow.Graphic is TextMeshProUGUI) && !(shadow.Graphic is TMP_SubMeshUI);
			ShadowContainer shadowContainer = shadow.ShadowContainer;
			float num = shadowContainer?.Snapshot?.canvasScale ?? graphic.canvas.scaleFactor;
			float num2 = 1f / num;
			Bounds bounds = obj.bounds;
			Vector2 vector = ((shadowContainer == null) ? Vector2.one : ((Vector2)bounds.size * num / shadowContainer.ImprintSize));
			Vector2 vector2 = new Vector2(shadowTexture.width, shadowTexture.height);
			vector2 *= vector;
			vector2 *= num2;
			if (flag)
			{
				if (shadow.Inset)
				{
					vector2 += Vector2.one * num2;
				}
				else
				{
					vector2 -= Vector2.one * num2;
				}
			}
			if (vector2.x < 0.001f || vector2.y < 0.001f)
			{
				CanvasRenderer.SetAlpha(0f);
				return;
			}
			rt.sizeDelta = vector2;
			float num3 = shadowContainer?.Padding ?? Mathf.CeilToInt(shadow.Size * num);
			num3 *= num2;
			if (flag)
			{
				num3 = ((!shadow.Inset) ? (num3 - 0.5f * num2) : (num3 + 0.5f * num2));
			}
			Vector2 vector3 = -(Vector2)bounds.min;
			rt.pivot = (vector3 + num3 * vector) / vector2;
			Vector2 self = ((shadowContainer == null) ? ((Vector2?)null) : (shadowContainer.Snapshot?.canvasRelativeOffset * num2)) ?? shadow.Offset;
			Vector3 vector4 = (shadow.ShadowAsSibling ? shadow.Offset.WithZ(0f) : self.WithZ(0f));
			rt.localPosition = (shadow.ShadowAsSibling ? (rectTransform.localPosition + vector4) : vector4);
			rt.localRotation = (shadow.ShadowAsSibling ? rectTransform.localRotation : Quaternion.identity);
			rt.localScale = (shadow.ShadowAsSibling ? rectTransform.localScale : Vector3.one);
			Color color = shadow.Color;
			if (shadow.UseCasterAlpha)
			{
				color.a *= shadow.Graphic.color.a;
			}
			if (shadow.Size < 1f && shadow.OffsetDistance < 1f)
			{
				float num4 = 1f - Mathf.Min(1f, shadow.Size + shadow.OffsetDistance);
				num4 *= num4;
				color.a *= 1f - num4;
			}
			graphic.color = color;
			CanvasRenderer.SetColor(shadow.IgnoreCasterColor ? Color.white : shadow.CanvasRenderer.GetColor());
			CanvasRenderer.SetAlpha(shadow.CanvasRenderer.GetAlpha());
			graphic.Rebuild(CanvasUpdate.PreRender);
		}

		public void SetTexture(Texture texture)
		{
			shadowTexture = texture;
			CanvasRenderer.SetTexture(texture);
			graphic.texture = texture;
		}

		public void SetMaterialDirty()
		{
			graphic.SetMaterialDirty();
		}

		public void ModifyMesh(VertexHelper vertexHelper)
		{
			if ((bool)shadow)
			{
				shadow.ModifyShadowRendererMesh(vertexHelper);
			}
		}

		public void ModifyMesh(Mesh mesh)
		{
			UnityEngine.Debug.Assert(condition: true, "This should only be called on old unsupported Unity version");
		}

		public void PerformLateUpdate()
		{
			if (shadow == null)
			{
				Dispose();
			}
		}

		protected virtual void OnDestroy()
		{
			willBeDestroyed = true;
		}

		public void Dispose()
		{
			if (!willBeDestroyed)
			{
				if ((bool)shadow && shadow.ShadowAsSibling)
				{
					base.gameObject.SetActive(value: false);
					base.transform.SetParent(null);
				}
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		internal static void ClearMaskMaterialCache()
		{
			foreach (KeyValuePair<int, Material> item in MASK_MATERIALS_CACHE)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(item.Value);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(item.Value);
				}
			}
			MASK_MATERIALS_CACHE.Clear();
		}

		public Material GetModifiedMaterial(Material baseMaterial)
		{
			if (!shadow)
			{
				return baseMaterial;
			}
			shadow.ModifyShadowRendererMaterial(baseMaterial);
			if (!baseMaterial.HasProperty(ShaderId.STENCIL_ID))
			{
				return baseMaterial;
			}
			Mask component = shadow.GetComponent<Mask>();
			bool flag = component != null && component.isActiveAndEnabled;
			int h = (flag ? baseMaterial.ComputeCRC() : baseMaterial.GetHashCode());
			int key = HashUtils.CombineHashCodes(flag.GetHashCode(), h);
			MASK_MATERIALS_CACHE.TryGetValue(key, out var value);
			if (!value)
			{
				value = new Material(baseMaterial);
				if (shadow.ShadowAsSibling)
				{
					value.SetInt(ShaderId.COLOR_MASK, 15);
					value.SetInt(ShaderId.STENCIL_OP, 0);
				}
				else if (flag)
				{
					int num = value.GetInt(ShaderId.STENCIL_ID) + 1;
					int i;
					for (i = 0; i < 8 && ((num >> i) & 1) != 1; i++)
					{
					}
					i = Mathf.Max(0, i - 1);
					int value2 = (1 << i) - 1;
					value.SetInt(ShaderId.STENCIL_ID, value2);
					value.SetInt(ShaderId.STENCIL_READ_MASK, value2);
				}
				MASK_MATERIALS_CACHE[key] = value;
			}
			else
			{
				int value3 = value.GetInt(ShaderId.STENCIL_ID);
				int value4 = value.GetInt(ShaderId.STENCIL_OP);
				int value5 = value.GetInt(ShaderId.COLOR_MASK);
				int value6 = value.GetInt(ShaderId.STENCIL_READ_MASK);
				value.CopyPropertiesFromMaterial(baseMaterial);
				value.SetInt(ShaderId.STENCIL_ID, value3);
				value.SetInt(ShaderId.STENCIL_OP, value4);
				value.SetInt(ShaderId.COLOR_MASK, value5);
				value.SetInt(ShaderId.STENCIL_READ_MASK, value6);
			}
			return value;
		}
	}
	internal class ShadowSettingSnapshot
	{
		public readonly TrueShadow shadow;

		public readonly Canvas canvas;

		public readonly RectTransform canvasRt;

		public readonly float canvasScale;

		public readonly BlurAlgorithmSelection algorithm;

		public readonly float size;

		public readonly Vector2 canvasRelativeOffset;

		public readonly Vector2 dimensions;

		private const int DIMENSIONS_HASH_STEP = 1;

		private int hash;

		internal ShadowSettingSnapshot(TrueShadow shadow)
		{
			this.shadow = shadow;
			canvas = shadow.Graphic.canvas;
			canvasRt = (RectTransform)canvas.transform;
			Mesh obj = shadow.SpriteMeshHandle.obj;
			Bounds bounds;
			if ((bool)obj)
			{
				bounds = obj.bounds;
				if (bounds.extents == Vector3.zero)
				{
					obj.RecalculateBounds();
					bounds = obj.bounds;
				}
			}
			else
			{
				bounds = new Bounds(Vector3.zero, Vector3.zero);
			}
			canvasScale = canvas.scaleFactor;
			Quaternion quaternion = Quaternion.Inverse(canvasRt.rotation) * shadow.RectTransform.rotation;
			canvasRelativeOffset = shadow.Offset.Rotate(0f - quaternion.eulerAngles.z) * canvasScale;
			dimensions = (Vector2)bounds.size * canvasScale;
			size = shadow.Size * canvasScale;
			algorithm = shadow.Algorithm;
			CalcHash();
		}

		private void CalcHash()
		{
			Graphic graphic = shadow.Graphic;
			int h = (int)((double)canvasScale * 10000.0);
			int h2 = (shadow.Inset ? 1 : 0);
			Color clearColor = shadow.ClearColor;
			Color color = graphic.color;
			if (shadow.IgnoreCasterColor)
			{
				color = Color.clear;
			}
			int h3 = HashUtils.CombineHashCodes(shadow.IgnoreCasterColor ? 1 : 0, (int)(color.r * 255f), (int)(color.g * 255f), (int)(color.b * 255f), (int)(color.a * 255f), (int)(clearColor.r * 255f), (int)(clearColor.g * 255f), (int)(clearColor.b * 255f), (int)(clearColor.a * 255f));
			int h4 = HashUtils.CombineHashCodes(shadow.Cutout ? 1 : 0, (int)(canvasRelativeOffset.x * 100f), (int)(canvasRelativeOffset.y * 100f));
			int h5 = ((graphic is Image { type: Image.Type.Tiled }) ? dimensions.GetHashCode() : HashUtils.CombineHashCodes(Mathf.CeilToInt(dimensions.x / 1f), Mathf.CeilToInt(dimensions.y / 1f)));
			int h6 = (int)algorithm;
			int h7 = Mathf.CeilToInt(size * 100f);
			int h8 = Mathf.CeilToInt(shadow.Spread * 100f);
			Material materialForRendering = graphic.materialForRendering;
			int h9 = HashUtils.CombineHashCodes(materialForRendering ? materialForRendering.ComputeCRC() : 0, h, h2, h3, h4, h5, h6, h7, h8, shadow.CustomHash);
			if (!(graphic is Image image2))
			{
				if (!(graphic is RawImage rawImage))
				{
					if (!(graphic is Text text))
					{
						if (!(graphic is TextMeshProUGUI tmp))
						{
							if (graphic is TMP_SubMeshUI tMP_SubMeshUI)
							{
								hash = HashUtils.CombineHashCodes(h9, CalcTMPHash(tMP_SubMeshUI.textComponent));
							}
							else
							{
								hash = h9;
							}
						}
						else
						{
							hash = HashUtils.CombineHashCodes(h9, CalcTMPHash(tmp));
						}
					}
					else
					{
						hash = HashUtils.CombineHashCodes(h9, text.text.GetHashCode(), text.font.GetHashCode(), (int)text.alignment);
					}
				}
				else
				{
					int h10 = 0;
					if ((bool)rawImage.texture)
					{
						h10 = rawImage.texture.GetInstanceID();
					}
					hash = HashUtils.CombineHashCodes(h9, h10);
				}
			}
			else
			{
				int h11 = 0;
				if ((bool)image2.sprite)
				{
					h11 = image2.sprite.GetHashCode();
				}
				int h12 = HashUtils.CombineHashCodes((int)image2.type, (int)(image2.pixelsPerUnitMultiplier * 1000f), (int)(image2.fillAmount * 360f * 20f), (int)image2.fillMethod, image2.fillOrigin, image2.fillClockwise ? 1 : 0);
				hash = HashUtils.CombineHashCodes(h9, h11, h12);
			}
		}

		private int CalcTMPHash(TMP_Text tmp)
		{
			int h = 0;
			if (!shadow.IgnoreCasterColor)
			{
				h = HashUtils.CombineHashCodes(tmp.enableVertexGradient.GetHashCode(), tmp.colorGradient.GetHashCode(), tmp.overrideColorTags.GetHashCode());
			}
			return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(shadow.SkipTextHashing.GetHashCode(), shadow.SkipTextHashing ? tmp.textInfo.characterCount : (tmp.text?.GetHashCode() ?? 0)), tmp.maxVisibleCharacters, tmp.maxVisibleWords, tmp.maxVisibleLines, Mathf.CeilToInt(tmp.transform.lossyScale.y * 100f), tmp.font.GetHashCode(), tmp.fontSize.GetHashCode(), h, tmp.characterSpacing.GetHashCode(), tmp.wordSpacing.GetHashCode(), tmp.lineSpacing.GetHashCode(), tmp.paragraphSpacing.GetHashCode(), (int)tmp.alignment);
		}

		public override int GetHashCode()
		{
			return hash;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			return GetHashCode() == obj.GetHashCode();
		}
	}
	[ExecuteAlways]
	public class ShadowSorter : MonoBehaviour
	{
		private readonly struct SortEntry : IComparable<SortEntry>
		{
			public readonly TrueShadow shadow;

			public readonly Transform shadowTransform;

			public readonly Transform rendererTransform;

			public SortEntry(TrueShadow shadow)
			{
				this.shadow = shadow;
				shadowTransform = shadow.transform;
				rendererTransform = shadow.shadowRenderer.transform;
			}

			public int CompareTo(SortEntry other)
			{
				return other.shadowTransform.GetSiblingIndex().CompareTo(shadowTransform.GetSiblingIndex());
			}
		}

		private readonly struct SortGroup
		{
			public readonly Transform parentTransform;

			public readonly List<SortEntry> sortEntries;

			public SortGroup(SortEntry firstEntry)
			{
				sortEntries = new List<SortEntry> { firstEntry };
				parentTransform = firstEntry.shadowTransform.parent;
			}

			public void Add(SortEntry pair)
			{
				if (!(pair.shadowTransform.parent != parentTransform))
				{
					int num = sortEntries.BinarySearch(pair);
					if (num < 0)
					{
						sortEntries.Insert(~num, pair);
					}
				}
			}

			public override int GetHashCode()
			{
				return parentTransform.GetHashCode();
			}

			public override bool Equals(object obj)
			{
				if (obj is SortGroup sortGroup)
				{
					return sortGroup.parentTransform == parentTransform;
				}
				return false;
			}
		}

		private static ShadowSorter instance;

		private readonly IndexedSet<TrueShadow> shadows = new IndexedSet<TrueShadow>();

		private readonly IndexedSet<SortGroup> sortGroups = new IndexedSet<SortGroup>();

		[Obsolete]
		public static ShadowSorter Instance
		{
			get
			{
				if (!instance)
				{
					ShadowSorter[] array = Shims.FindObjectsOfType<ShadowSorter>();
					for (int num = array.Length - 1; num > 0; num--)
					{
						UnityEngine.Object.Destroy(array[num]);
					}
					instance = ((array.Length != 0) ? array[0] : null);
					if (!instance)
					{
						instance = new GameObject("ShadowSorter")
						{
							hideFlags = HideFlags.HideAndDontSave
						}.AddComponent<ShadowSorter>();
					}
				}
				return instance;
			}
		}

		public void Register(TrueShadow shadow)
		{
			shadows.AddUnique(shadow);
		}

		public void UnRegister(TrueShadow shadow)
		{
			shadows.Remove(shadow);
		}

		private void LateUpdate()
		{
			if (!this)
			{
				return;
			}
			for (int i = 0; i < shadows.Count; i++)
			{
				TrueShadow trueShadow = shadows[i];
				if ((bool)trueShadow && trueShadow.isActiveAndEnabled)
				{
					trueShadow.CheckHierarchyDirtied();
					if (trueShadow.HierarchyDirty)
					{
						AddSortEntry(trueShadow);
					}
				}
			}
			Sort();
		}

		private void AddSortEntry(TrueShadow shadow)
		{
			SortEntry sortEntry = new SortEntry(shadow);
			SortGroup item = new SortGroup(sortEntry);
			int num = sortGroups.IndexOf(item);
			if (num > -1)
			{
				sortGroups[num].Add(sortEntry);
			}
			else
			{
				sortGroups.Add(item);
			}
		}

		public void Sort()
		{
			for (int i = 0; i < sortGroups.Count; i++)
			{
				SortGroup sortGroup = sortGroups[i];
				if (!sortGroup.parentTransform)
				{
					continue;
				}
				foreach (SortEntry sortEntry in sortGroup.sortEntries)
				{
					sortEntry.rendererTransform.SetParent(sortGroup.parentTransform, worldPositionStays: false);
					int siblingIndex = sortEntry.rendererTransform.GetSiblingIndex();
					int siblingIndex2 = sortEntry.shadowTransform.GetSiblingIndex();
					if (siblingIndex > siblingIndex2)
					{
						sortEntry.rendererTransform.SetSiblingIndex(siblingIndex2);
					}
					else
					{
						sortEntry.rendererTransform.SetSiblingIndex(siblingIndex2 - 1);
					}
					sortEntry.shadow.UnSetHierachyDirty();
				}
				foreach (SortEntry sortEntry2 in sortGroup.sortEntries)
				{
					sortEntry2.shadow.ForgetSiblingIndexChanges();
				}
			}
			sortGroups.Clear();
		}

		private void OnApplicationQuit()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
	public static class Shims
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T FindObjectOfType<T>(bool includeInactive = false, bool sorted = true) where T : UnityEngine.Object
		{
			return UnityEngine.Object.FindObjectOfType<T>(includeInactive);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T[] FindObjectsOfType<T>(bool includeInactive = false) where T : UnityEngine.Object
		{
			return UnityEngine.Object.FindObjectsOfType<T>(includeInactive);
		}
	}
	public enum BlendMode
	{
		Normal,
		Additive,
		Screen,
		Multiply
	}
	public static class BlendModeExtensions
	{
		private static Material matNormal;

		private static Material materialAdditive;

		private static Material matScreen;

		private static Material matMultiply;

		public static Material GetMaterial(this BlendMode blendMode)
		{
			switch (blendMode)
			{
			case BlendMode.Normal:
				if (!matNormal)
				{
					matNormal = new Material(Shader.Find("UI/TrueShadow-Normal"));
				}
				return matNormal;
			case BlendMode.Additive:
				if (!materialAdditive)
				{
					materialAdditive = new Material(Shader.Find("UI/TrueShadow-Additive"));
				}
				return materialAdditive;
			case BlendMode.Screen:
				if (!matScreen)
				{
					matScreen = new Material(Shader.Find("UI/TrueShadow-Screen"));
				}
				return matScreen;
			case BlendMode.Multiply:
				if (!matMultiply)
				{
					matMultiply = new Material(Shader.Find("UI/TrueShadow-Multiply"));
				}
				return matMultiply;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
	internal class ShadowContainer
	{
		public readonly int requestHash;

		public RenderTexture Texture { get; }

		public ShadowSettingSnapshot Snapshot { get; }

		public int Padding { get; }

		public Vector2Int ImprintSize { get; }

		public int RefCount { get; internal set; }

		internal ShadowContainer(RenderTexture texture, ShadowSettingSnapshot snapshot, int padding, Vector2Int imprintSize)
		{
			Texture = texture;
			Snapshot = snapshot;
			Padding = padding;
			ImprintSize = imprintSize;
			RefCount = 1;
			requestHash = snapshot.GetHashCode();
		}
	}
	[AddComponentMenu("UI/True Shadow/True Shadow")]
	[RequireComponent(typeof(Graphic))]
	[HelpURL("https://leloctai.com/trueshadow/docs/articles/customize.html")]
	[ExecuteAlways]
	public class TrueShadow : UIBehaviour, IMeshModifier, ICanvasElement
	{
		private static readonly Color DEFAULT_COLOR = new Color(0f, 0f, 0f, 0.6f);

		[SerializeField]
		private string updateGroupId = "";

		private ShadowUpdateGroup currentGroup;

		[Tooltip("Accurate algorithm doesn't miss small features, but can be much slower for large or dynamic shadows. Fast is recommended in most cases")]
		[SerializeField]
		private BlurAlgorithmSelection algorithm;

		[Tooltip("Size of the shadow")]
		[SerializeField]
		private float size = 32f;

		[Tooltip("Make the shadow thicker")]
		[SpreadSlider]
		[SerializeField]
		private float spread;

		[SerializeField]
		private bool useGlobalAngle;

		[Tooltip("Direction to offset the shadow toward")]
		[Knob]
		[SerializeField]
		private float offsetAngle = 90f;

		[Tooltip("How far to offset the shadow")]
		[SerializeField]
		private float offsetDistance = 12f;

		[SerializeField]
		private Vector2 offset = Vector2.zero;

		[Tooltip("Tint the shadow")]
		[SerializeField]
		private Color color = DEFAULT_COLOR;

		[Tooltip("Inset shadow")]
		[InsetToggle]
		[SerializeField]
		private bool inset;

		[Tooltip("Blend mode of the shadow")]
		[SerializeField]
		private BlendMode blendMode;

		[FormerlySerializedAs("multiplyCasterAlpha")]
		[Tooltip("Allow shadow to cross-fade with caster")]
		[SerializeField]
		private bool useCasterAlpha = true;

		[Tooltip("Ignore the shadow caster's color, so you can choose specific color for your shadow")]
		[SerializeField]
		private bool ignoreCasterColor;

		[Tooltip("Skip expensive hashing of long text. Using this is recommended for long animated dialogue. If the text may change without changing length, you must set CustomHash for the change to be detected.")]
		[SerializeField]
		private bool skipTextHashing;

		[Tooltip("Reduce memory use of disabled shadows in exchange for slower enable")]
		[SerializeField]
		private bool deallocateOnDisable;

		[Tooltip("May improve shadow fit on some casters with thin features")]
		[SerializeField]
		private bool disableFitCompensation;

		[Tooltip("Position the shadow GameObject as previous sibling of the UI element")]
		[SerializeField]
		private bool shadowAsSibling;

		[Tooltip("Cut the source image from the shadow to avoid shadow showing behind semi-transparent UI")]
		[SerializeField]
		private bool cutout;

		[Tooltip("Bake the shadow to a sprite to reduce CPU and GPU usage at runtime, at the cost of storage, memory and flexibility")]
		[SerializeField]
		private bool baked;

		[SerializeField]
		private bool modifiedFromInspector;

		[SerializeField]
		private List<Sprite> bakedShadows;

		public ShadowRenderer shadowRenderer;

		private ShadowContainer shadowContainer;

		private int customHash;

		private bool textureDirty;

		private bool layoutDirty;

		private int shadowIndex = -1;

		private Action checkHierarchyDirtiedDelegate;

		private IChangeTracker[] transformTrackers;

		private ChangeTracker<int>[] hierarchyTrackers;

		private ITrueShadowCasterMaterialProvider casterMaterialProvider;

		private ITrueShadowCasterSubMeshMaterialProvider casterSubMeshMaterialProvider;

		private ITrueShadowCasterMeshProvider casterMeshProvider;

		private ITrueShadowCasterMaterialPropertiesModifier casterMaterialPropertiesModifier;

		private ITrueShadowCasterMeshModifier casterMeshModifier;

		private ITrueShadowCasterClearColorProvider casterClearColorProvider;

		private ITrueShadowRendererMaterialProvider rendererMaterialProvider;

		private ITrueShadowRendererMaterialModifier rendererMaterialModifier;

		private ITrueShadowRendererMeshModifier rendererMeshModifier;

		private ITrueShadowCustomHashProviderV2 customHashProviderV2;

		private readonly List<Color32> meshColors = new List<Color32>(4);

		private readonly List<Color32> meshColorsOpaque = new List<Color32>(4);

		public BlurAlgorithmSelection Algorithm
		{
			get
			{
				return algorithm;
			}
			set
			{
				if (modifiedFromInspector || algorithm != value)
				{
					modifiedFromInspector = false;
					SetLayoutDirty();
					SetTextureDirty();
				}
				algorithm = value;
				Size = size;
			}
		}

		public float Size
		{
			get
			{
				return size;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (Algorithm == BlurAlgorithmSelection.Accurate)
				{
					num = Mathf.Min(num, 126f);
				}
				if (modifiedFromInspector || !Mathf.Approximately(size, num))
				{
					modifiedFromInspector = false;
					SetLayoutDirty();
					SetTextureDirty();
				}
				size = num;
				if (Inset && OffsetDistance > Size)
				{
					OffsetDistance = Size;
				}
			}
		}

		public float Spread
		{
			get
			{
				return spread;
			}
			set
			{
				float b = Mathf.Clamp01(value);
				if (modifiedFromInspector || !Mathf.Approximately(spread, b))
				{
					modifiedFromInspector = false;
					SetLayoutDirty();
					SetTextureDirty();
				}
				spread = b;
			}
		}

		public bool UseGlobalAngle
		{
			get
			{
				return useGlobalAngle;
			}
			set
			{
				useGlobalAngle = value;
				ProjectSettings.Instance.globalAngleChanged -= OnGlobalAngleChanged;
				float globalAngle = ProjectSettings.Instance.GlobalAngle;
				if (useGlobalAngle)
				{
					offset = Math.AngleDistanceVector(globalAngle, offset.magnitude, Vector2.right);
					SetLayoutDirty();
					if (Cutout)
					{
						SetTextureDirty();
					}
					ProjectSettings.Instance.globalAngleChanged += OnGlobalAngleChanged;
				}
				else
				{
					float num = offsetAngle;
					OffsetAngle = globalAngle;
					OffsetAngle = num;
				}
			}
		}

		public float OffsetAngle
		{
			get
			{
				return offsetAngle;
			}
			set
			{
				if (UseGlobalAngle)
				{
					return;
				}
				float b = (value + 360f) % 360f;
				if (modifiedFromInspector || !Mathf.Approximately(offsetAngle, b))
				{
					modifiedFromInspector = false;
					SetLayoutDirty();
					if (Cutout)
					{
						SetTextureDirty();
					}
				}
				offsetAngle = b;
				offset = Math.AngleDistanceVector(offsetAngle, offset.magnitude, Vector2.right);
			}
		}

		public float OffsetDistance
		{
			get
			{
				return offsetDistance;
			}
			set
			{
				float num = value;
				num = ((!Inset) ? Mathf.Max(0f, num) : Mathf.Clamp(num, 0f, Size));
				if (modifiedFromInspector || !Mathf.Approximately(offsetDistance, num))
				{
					modifiedFromInspector = false;
					SetLayoutDirty();
					if (Cutout)
					{
						SetTextureDirty();
					}
				}
				offsetDistance = num;
				offset = ((offset.sqrMagnitude < 1E-06f) ? Math.AngleDistanceVector(offsetAngle, offsetDistance, Vector2.right) : (offset.normalized * offsetDistance));
			}
		}

		public Color Color
		{
			get
			{
				return color;
			}
			set
			{
				if (modifiedFromInspector || value != color)
				{
					modifiedFromInspector = false;
					SetLayoutDirty();
				}
				color = value;
			}
		}

		public bool UseCasterAlpha
		{
			get
			{
				return useCasterAlpha;
			}
			set
			{
				if (modifiedFromInspector || value != useCasterAlpha)
				{
					modifiedFromInspector = false;
					SetLayoutDirty();
				}
				useCasterAlpha = value;
			}
		}

		public bool IgnoreCasterColor
		{
			get
			{
				return ignoreCasterColor;
			}
			set
			{
				if (modifiedFromInspector || value != ignoreCasterColor)
				{
					modifiedFromInspector = false;
					SetTextureDirty();
				}
				ignoreCasterColor = value;
			}
		}

		public bool SkipTextHashing
		{
			get
			{
				return skipTextHashing;
			}
			set
			{
				if (modifiedFromInspector || value != skipTextHashing)
				{
					modifiedFromInspector = false;
					SetLayoutTextureDirty();
					SetTextureDirty();
				}
				skipTextHashing = value;
			}
		}

		public bool Inset
		{
			get
			{
				return inset;
			}
			set
			{
				if (modifiedFromInspector || value != inset)
				{
					modifiedFromInspector = false;
					SetLayoutDirty();
					SetTextureDirty();
				}
				inset = value;
				if (Inset && OffsetDistance > Size)
				{
					OffsetDistance = Size;
				}
			}
		}

		public BlendMode BlendMode
		{
			get
			{
				return blendMode;
			}
			set
			{
				if ((bool)Graphic && (bool)CanvasRenderer)
				{
					blendMode = value;
					shadowRenderer.UpdateMaterial();
				}
			}
		}

		public bool DisableFitCompensation
		{
			get
			{
				return disableFitCompensation;
			}
			set
			{
				if (modifiedFromInspector || disableFitCompensation != value)
				{
					modifiedFromInspector = false;
					disableFitCompensation = value;
					SetLayoutDirty();
				}
			}
		}

		public bool DeallocateOnDisable
		{
			get
			{
				return deallocateOnDisable;
			}
			set
			{
				deallocateOnDisable = value;
			}
		}

		public Color ClearColor => casterClearColorProvider?.GetTrueShadowCasterClearColor() ?? Color.clear;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShadowAsSibling
		{
			get
			{
				return shadowAsSibling;
			}
			set
			{
				shadowAsSibling = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Cutout
		{
			get
			{
				if (shadowAsSibling)
				{
					return cutout;
				}
				return true;
			}
			set
			{
				cutout = value;
			}
		}

		public int CustomHash
		{
			get
			{
				return customHash;
			}
			set
			{
				if (customHash != value)
				{
					SetTextureDirty();
				}
				customHash = value;
			}
		}

		public Vector2 Offset => offset;

		internal ObjectHandle<Mesh> SpriteMeshHandle { get; private set; }

		internal Graphic Graphic { get; private set; }

		internal CanvasRenderer CanvasRenderer { get; private set; }

		internal RectTransform RectTransform { get; private set; }

		internal Texture Content
		{
			get
			{
				Graphic graphic = Graphic;
				if (!(graphic is Image { overrideSprite: var overrideSprite }))
				{
					if (!(graphic is RawImage rawImage))
					{
						if (!(graphic is TextMeshProUGUI textMeshProUGUI))
						{
							if (graphic is TMP_SubMeshUI tMP_SubMeshUI)
							{
								return tMP_SubMeshUI.materialForRendering.mainTexture;
							}
							return Graphic.mainTexture;
						}
						return textMeshProUGUI.materialForRendering.mainTexture;
					}
					return rawImage.texture;
				}
				if (!overrideSprite)
				{
					return null;
				}
				return overrideSprite.texture;
			}
		}

		internal ShadowContainer ShadowContainer => shadowContainer;

		internal bool HierarchyDirty { get; private set; }

		public bool UsingRendererMaterialProvider => rendererMaterialProvider != null;

		private void OnGlobalAngleChanged(float angle)
		{
			offset = Math.AngleDistanceVector(angle, offset.magnitude, Vector2.right);
			SetLayoutDirty();
			if (Cutout)
			{
				SetTextureDirty();
			}
		}

		protected override void Awake()
		{
		}

		protected override void OnEnable()
		{
			TrueShadow[] components = GetComponents<TrueShadow>();
			int num = 0;
			for (int i = 0; i < components.Length; i++)
			{
				if (components[i] == this || (bool)components[i].shadowRenderer)
				{
					components[i].shadowIndex = num++;
				}
			}
			UnityEngine.Debug.Assert(shadowIndex >= 0, "shadowIndex < 0. Please make a bug report");
			RectTransform = GetComponent<RectTransform>();
			Graphic = GetComponent<Graphic>();
			CanvasRenderer = GetComponent<CanvasRenderer>();
			if (!SpriteMeshHandle.obj)
			{
				SpriteMeshHandle = ObjectHandle.Take(new Mesh());
			}
			InitializePlugins();
			if (bakedShadows == null)
			{
				bakedShadows = new List<Sprite>(4);
			}
			InitInvalidator();
			ShadowRenderer.Initialize(this, ref shadowRenderer);
			if (UseGlobalAngle)
			{
				ProjectSettings.Instance.globalAngleChanged -= OnGlobalAngleChanged;
				ProjectSettings.Instance.globalAngleChanged += OnGlobalAngleChanged;
				OnGlobalAngleChanged(ProjectSettings.Instance.GlobalAngle);
			}
			if ((bool)Graphic)
			{
				Graphic.SetVerticesDirty();
			}
			RegisterToShadowGroup();
			this.NextFrames(CopyToTMPSubMeshes, 2);
		}

		public void ApplySerializedData()
		{
			Algorithm = algorithm;
			Size = size;
			Spread = spread;
			OffsetAngle = offsetAngle;
			OffsetDistance = offsetDistance;
			BlendMode = blendMode;
			SetHierachyDirty();
			SetLayoutDirty();
			SetTextureDirty();
			if ((bool)shadowRenderer)
			{
				shadowRenderer.SetMaterialDirty();
			}
			CopyToTMPSubMeshes();
		}

		private void RegisterToShadowGroup()
		{
			if (!string.IsNullOrEmpty(updateGroupId) && ShadowUpdateGroup.Groups.TryGetValue(updateGroupId, out var value))
			{
				currentGroup = value;
				value.Register(this);
			}
			else
			{
				currentGroup = null;
				TrueShadowManager.Instance.Register(this);
			}
		}

		private void UnregisterFromShadowGroup()
		{
			if (currentGroup != null)
			{
				currentGroup.Unregister(this);
			}
			else
			{
				TrueShadowManager.Instance.Unregister(this);
			}
		}

		protected override void OnDisable()
		{
			if (Application.isPlaying)
			{
				UnregisterFromShadowGroup();
			}
			ProjectSettings.Instance.globalAngleChanged -= OnGlobalAngleChanged;
			TerminateInvalidator();
			TerminatePlugins();
			if (DeallocateOnDisable)
			{
				ShadowFactory.Instance.ReleaseContainer(ref shadowContainer);
			}
			if ((bool)shadowRenderer)
			{
				shadowRenderer.gameObject.SetActive(value: false);
			}
		}

		protected override void OnDestroy()
		{
			if (Application.isPlaying)
			{
				UnregisterFromShadowGroup();
			}
			SpriteMeshHandle.Dispose();
			if ((bool)shadowRenderer)
			{
				shadowRenderer.Dispose();
			}
			ShadowFactory.Instance.ReleaseContainer(ref shadowContainer);
			StopAllCoroutines();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool ShouldPerformWorks()
		{
			if (!base.isActiveAndEnabled)
			{
				return false;
			}
			CanvasRenderer canvasRenderer = CanvasRenderer;
			if (canvasRenderer == null || canvasRenderer.cull || canvasRenderer.GetInheritedAlpha() <= 0f)
			{
				return false;
			}
			CanvasRenderer canvasRenderer2 = shadowRenderer.CanvasRenderer;
			if (canvasRenderer2 != null)
			{
				return !canvasRenderer2.cull;
			}
			return false;
		}

		public void PerformLateUpdate()
		{
			if ((bool)shadowRenderer)
			{
				shadowRenderer.gameObject.SetActive((bool)Graphic && Graphic.isActiveAndEnabled);
			}
			if (ShouldPerformWorks())
			{
				CheckTransformDirtied();
			}
		}

		public void Rebuild(CanvasUpdate executing)
		{
			if (ShouldPerformWorks() && executing == CanvasUpdate.PostLayout && layoutDirty)
			{
				shadowRenderer.ReLayout();
				layoutDirty = false;
			}
		}

		public void OnWillRenderCanvas()
		{
			if (!ShouldPerformWorks())
			{
				return;
			}
			if (textureDirty && (bool)Graphic && (bool)Graphic.canvas)
			{
				ShadowFactory.Instance.Get(new ShadowSettingSnapshot(this), ref shadowContainer);
				shadowRenderer.SetTexture(shadowContainer?.Texture);
				textureDirty = false;
			}
			if (!shadowAsSibling)
			{
				if (shadowRenderer.transform.parent != RectTransform)
				{
					shadowRenderer.transform.SetParent(RectTransform, worldPositionStays: true);
				}
				if (shadowRenderer.transform.GetSiblingIndex() != shadowIndex)
				{
					shadowRenderer.transform.SetSiblingIndex(shadowIndex);
				}
				UnSetHierachyDirty();
				if (layoutDirty)
				{
					shadowRenderer.ReLayout();
					layoutDirty = false;
				}
			}
		}

		public void LayoutComplete()
		{
		}

		public void GraphicUpdateComplete()
		{
		}

		public void SetTextureDirty()
		{
			textureDirty = true;
		}

		public void SetLayoutDirty()
		{
			layoutDirty = true;
		}

		public void SetHierachyDirty()
		{
			HierarchyDirty = true;
		}

		internal void UnSetHierachyDirty()
		{
			HierarchyDirty = false;
		}

		public void CopyTo(TrueShadow other)
		{
			other.Inset = Inset;
			other.Algorithm = Algorithm;
			other.Size = Size;
			other.Spread = Spread;
			other.UseGlobalAngle = UseGlobalAngle;
			other.OffsetAngle = OffsetAngle;
			other.OffsetDistance = OffsetDistance;
			other.Color = Color;
			other.BlendMode = BlendMode;
			other.UseCasterAlpha = UseCasterAlpha;
			other.IgnoreCasterColor = IgnoreCasterColor;
			other.DisableFitCompensation = DisableFitCompensation;
			other.updateGroupId = updateGroupId;
			other.SetLayoutTextureDirty();
		}

		public void CopyTo(GameObject other)
		{
			TrueShadow component = other.GetComponent<TrueShadow>();
			if ((bool)component)
			{
				CopyTo(component);
				return;
			}
			TrueShadow other2 = other.AddComponent<TrueShadow>();
			CopyTo(other2);
		}

		public void CopyToTMPSubMeshes()
		{
			if (Graphic is TextMeshProUGUI)
			{
				TMP_SubMeshUI[] componentsInChildren = GetComponentsInChildren<TMP_SubMeshUI>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					CopyTo(componentsInChildren[i].gameObject);
				}
			}
		}

		private void InitInvalidator()
		{
			checkHierarchyDirtiedDelegate = CheckHierarchyDirtied;
			hierarchyTrackers = new ChangeTracker<int>[2]
			{
				new ChangeTracker<int>(() => RectTransform.GetSiblingIndex(), delegate(int newValue)
				{
					SetHierachyDirty();
					return newValue;
				}),
				new ChangeTracker<int>(() => shadowRenderer ? shadowRenderer.transform.GetSiblingIndex() : (-1), delegate(int newValue)
				{
					SetHierachyDirty();
					return newValue;
				})
			};
			transformTrackers = new IChangeTracker[3]
			{
				new ChangeTracker<Vector3>(() => RectTransform.position, delegate(Vector3 newValue)
				{
					SetLayoutDirty();
					return newValue;
				}, (Vector3 prev, Vector3 curr) => prev == curr),
				new ChangeTracker<Quaternion>(() => RectTransform.rotation, delegate(Quaternion newValue)
				{
					SetLayoutDirty();
					if (Cutout)
					{
						SetTextureDirty();
					}
					return newValue;
				}, (Quaternion prev, Quaternion curr) => prev == curr),
				new ChangeTracker<Color>(() => CanvasRenderer.GetColor(), delegate(Color newValue)
				{
					SetLayoutDirty();
					return newValue;
				}, (Color prev, Color curr) => prev == curr)
			};
			if (Graphic is TextMeshProUGUI || Graphic is TMP_SubMeshUI)
			{
				IChangeTracker[] array = transformTrackers;
				transformTrackers = new IChangeTracker[array.Length + 1];
				Array.Copy(array, transformTrackers, array.Length);
				transformTrackers[array.Length] = new ChangeTracker<Vector3>(() => RectTransform.lossyScale, delegate(Vector3 newValue)
				{
					SetLayoutTextureDirty();
					return newValue;
				}, delegate(Vector3 prev, Vector3 curr)
				{
					if (prev == curr)
					{
						return true;
					}
					if (prev.x * prev.y * prev.z < 1E-09f && curr.x * curr.y * curr.z > 1E-09f)
					{
						return false;
					}
					Vector3 vector = curr - prev;
					return Mathf.Abs(vector.x / prev.x) < 0.25f && Mathf.Abs(vector.y / prev.y) < 0.25f && Mathf.Abs(vector.z / prev.z) < 0.25f;
				});
			}
			Graphic.RegisterDirtyLayoutCallback(SetLayoutTextureDirty);
			Graphic.RegisterDirtyVerticesCallback(SetLayoutTextureDirty);
			Graphic.RegisterDirtyMaterialCallback(OnGraphicMaterialDirty);
			CheckHierarchyDirtied();
			CheckTransformDirtied();
		}

		private void TerminateInvalidator()
		{
			if ((bool)Graphic)
			{
				Graphic.UnregisterDirtyLayoutCallback(SetLayoutTextureDirty);
				Graphic.UnregisterDirtyVerticesCallback(SetLayoutTextureDirty);
				Graphic.UnregisterDirtyMaterialCallback(OnGraphicMaterialDirty);
			}
		}

		private void OnGraphicMaterialDirty()
		{
			SetLayoutTextureDirty();
			shadowRenderer.UpdateMaterial();
		}

		internal void CheckTransformDirtied()
		{
			if (transformTrackers != null)
			{
				for (int i = 0; i < transformTrackers.Length; i++)
				{
					transformTrackers[i].Check();
				}
			}
		}

		internal void CheckHierarchyDirtied()
		{
			if (ShadowAsSibling && hierarchyTrackers != null)
			{
				for (int i = 0; i < hierarchyTrackers.Length; i++)
				{
					hierarchyTrackers[i].Check();
				}
			}
		}

		internal void ForgetSiblingIndexChanges()
		{
			for (int i = 0; i < hierarchyTrackers.Length; i++)
			{
				hierarchyTrackers[i].Forget();
			}
		}

		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			if (base.isActiveAndEnabled)
			{
				SetHierachyDirty();
				this.NextFrames(checkHierarchyDirtiedDelegate);
			}
		}

		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (base.isActiveAndEnabled)
			{
				SetLayoutTextureDirty();
			}
		}

		protected override void OnDidApplyAnimationProperties()
		{
			if (base.isActiveAndEnabled)
			{
				SetLayoutTextureDirty();
			}
		}

		public void ModifyMesh(Mesh mesh)
		{
			if (casterMeshProvider == null && base.isActiveAndEnabled)
			{
				SpriteMeshHandle.Dispose();
				SpriteMeshHandle = ObjectHandle.Borrow(UnityEngine.Object.Instantiate(mesh));
				ModifyShadowCastingMesh(SpriteMeshHandle.obj);
				SetLayoutTextureDirty();
			}
		}

		public void ModifyMesh(VertexHelper verts)
		{
			if (casterMeshProvider != null || !base.isActiveAndEnabled)
			{
				return;
			}
			if (!(Graphic is TextMeshProUGUI))
			{
				if (!SpriteMeshHandle.obj)
				{
					SpriteMeshHandle = ObjectHandle.Take(new Mesh());
				}
				verts.FillMesh(SpriteMeshHandle.obj);
				ModifyShadowCastingMesh(SpriteMeshHandle.obj);
			}
			SetLayoutTextureDirty();
		}

		private void SetLayoutTextureDirty()
		{
			Mesh mesh = null;
			if (Graphic is TextMeshProUGUI textMeshProUGUI)
			{
				mesh = (string.IsNullOrEmpty(textMeshProUGUI.text) ? null : textMeshProUGUI.mesh);
			}
			else if (Graphic is TMP_SubMeshUI tMP_SubMeshUI)
			{
				mesh = (string.IsNullOrEmpty(tMP_SubMeshUI.textComponent.text) ? null : tMP_SubMeshUI.mesh);
			}
			if (mesh != null)
			{
				SpriteMeshHandle.Dispose();
				SpriteMeshHandle = ObjectHandle.Borrow(mesh);
			}
			SetLayoutDirty();
			SetTextureDirty();
		}

		private void InitializePlugins()
		{
			casterMaterialProvider = GetComponent<ITrueShadowCasterMaterialProvider>();
			casterSubMeshMaterialProvider = GetComponent<ITrueShadowCasterSubMeshMaterialProvider>();
			casterMeshProvider = GetComponent<ITrueShadowCasterMeshProvider>();
			casterMaterialPropertiesModifier = GetComponent<ITrueShadowCasterMaterialPropertiesModifier>();
			casterMeshModifier = GetComponent<ITrueShadowCasterMeshModifier>();
			casterClearColorProvider = GetComponent<ITrueShadowCasterClearColorProvider>();
			rendererMaterialProvider = GetComponent<ITrueShadowRendererMaterialProvider>();
			rendererMaterialModifier = GetComponent<ITrueShadowRendererMaterialModifier>();
			rendererMeshModifier = GetComponent<ITrueShadowRendererMeshModifier>();
			customHashProviderV2 = GetComponent<ITrueShadowCustomHashProviderV2>();
			if (casterMeshProvider != null)
			{
				casterMeshProvider.trueShadowCasterMeshChanged += HandleCasterMeshProviderMeshChanged;
			}
			if (casterMaterialProvider != null)
			{
				casterMaterialProvider.materialReplaced += HandleCasterMaterialReplaced;
				casterMaterialProvider.materialModified += HandleCasterMaterialModified;
			}
			if (rendererMaterialProvider != null)
			{
				rendererMaterialProvider.materialReplaced += HandleRendererMaterialReplaced;
				rendererMaterialProvider.materialModified += HandleRendererMaterialModified;
			}
			if (customHashProviderV2 != null)
			{
				customHashProviderV2.trueShadowCustomHashChanged += HandleTrueShadowCustomHashChanged;
			}
		}

		private void TerminatePlugins()
		{
			if (casterMeshProvider != null)
			{
				casterMeshProvider.trueShadowCasterMeshChanged -= HandleCasterMeshProviderMeshChanged;
			}
			if (casterMaterialProvider != null)
			{
				casterMaterialProvider.materialReplaced -= HandleCasterMaterialReplaced;
				casterMaterialProvider.materialModified -= HandleCasterMaterialModified;
			}
			if (rendererMaterialProvider != null)
			{
				rendererMaterialProvider.materialReplaced -= HandleRendererMaterialReplaced;
				rendererMaterialProvider.materialModified -= HandleRendererMaterialModified;
			}
			if (customHashProviderV2 != null)
			{
				customHashProviderV2.trueShadowCustomHashChanged -= HandleTrueShadowCustomHashChanged;
			}
		}

		public void RefreshPlugins()
		{
			TerminatePlugins();
			InitializePlugins();
		}

		private void HandleCasterMeshProviderMeshChanged(Mesh mesh)
		{
			SpriteMeshHandle = ObjectHandle.Borrow(mesh);
			SetLayoutTextureDirty();
		}

		private void HandleCasterMaterialReplaced()
		{
			SetTextureDirty();
		}

		private void HandleRendererMaterialReplaced()
		{
			if ((bool)shadowRenderer)
			{
				shadowRenderer.UpdateMaterial();
			}
		}

		private void HandleCasterMaterialModified()
		{
			SetTextureDirty();
		}

		private void HandleRendererMaterialModified()
		{
			if ((bool)shadowRenderer)
			{
				shadowRenderer.SetMaterialDirty();
			}
		}

		private void HandleTrueShadowCustomHashChanged(int customHash)
		{
			CustomHash = customHash;
			SetLayoutTextureDirty();
		}

		public virtual Material GetShadowCastingMaterial()
		{
			if (casterSubMeshMaterialProvider != null)
			{
				return casterSubMeshMaterialProvider.GetTrueShadowCasterMaterialForSubMesh(0);
			}
			Material material = null;
			if (casterMaterialProvider != null)
			{
				material = casterMaterialProvider.GetTrueShadowCasterMaterial();
			}
			else if (Graphic is TextMeshProUGUI || Graphic is TMP_SubMeshUI)
			{
				material = Graphic.materialForRendering;
			}
			if (!(material != null))
			{
				return Graphic.material;
			}
			return material;
		}

		public virtual Material GetShadowCastingMaterialForSubMesh(int subMeshIndex)
		{
			if (casterSubMeshMaterialProvider == null)
			{
				UnityEngine.Debug.LogError("Custom UI that use submeshes must implement ITrueShadowCasterSubMeshMaterialProvider");
				return null;
			}
			return casterSubMeshMaterialProvider.GetTrueShadowCasterMaterialForSubMesh(subMeshIndex);
		}

		public virtual void ModifyShadowCastingMaterialProperties(MaterialPropertyBlock propertyBlock)
		{
			casterMaterialPropertiesModifier?.ModifyTrueShadowCasterMaterialProperties(propertyBlock);
		}

		public virtual void ModifyShadowCastingMesh(Mesh mesh)
		{
			casterMeshModifier?.ModifyTrueShadowCasterMesh(mesh);
			MakeOpaque(mesh);
		}

		private void MakeOpaque(Mesh mesh)
		{
			if (shadowAsSibling)
			{
				return;
			}
			mesh.GetColors(meshColors);
			int count = meshColors.Count;
			if (count < 1)
			{
				return;
			}
			if (meshColorsOpaque.Count == count)
			{
				if (meshColors[0].a == meshColorsOpaque[0].a)
				{
					return;
				}
			}
			else
			{
				meshColorsOpaque.Clear();
				meshColorsOpaque.AddRange(Enumerable.Repeat(new Color32(0, 0, 0, 0), count));
			}
			for (int i = 0; i < count; i++)
			{
				Color32 value = meshColors[i];
				value.a = byte.MaxValue;
				meshColorsOpaque[i] = value;
			}
			mesh.SetColors(meshColorsOpaque);
		}

		public virtual Material GetShadowRenderingMaterial()
		{
			Material material = rendererMaterialProvider?.GetTrueShadowRendererMaterial();
			if (!(material != null))
			{
				return BlendMode.GetMaterial();
			}
			return material;
		}

		public virtual void ModifyShadowRendererMaterial(Material baseMaterial)
		{
			rendererMaterialModifier?.ModifyTrueShadowRendererMaterial(baseMaterial);
		}

		public virtual void ModifyShadowRendererMesh(VertexHelper vertexHelper)
		{
			rendererMeshModifier?.ModifyTrueShadowRendererMesh(vertexHelper);
		}

		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}
	}
	internal interface IChangeTracker
	{
		void Check();
	}
	internal class ChangeTracker<T> : IChangeTracker
	{
		private T previousValue;

		private readonly Func<T> getValue;

		private readonly Func<T, T> onChange;

		private readonly Func<T, T, bool> compare;

		public ChangeTracker(Func<T> getValue, Func<T, T> onChange, Func<T, T, bool> compare = null)
		{
			this.getValue = getValue;
			this.onChange = onChange;
			this.compare = compare ?? new Func<T, T, bool>(EqualityComparer<T>.Default.Equals);
			previousValue = this.getValue();
		}

		public void Forget()
		{
			previousValue = getValue();
		}

		public void Check()
		{
			T val = getValue();
			if (!compare(previousValue, val))
			{
				previousValue = onChange(val);
			}
		}
	}
	public class TrueShadowManager : MonoBehaviour
	{
		private static TrueShadowManager _instance;

		private readonly List<TrueShadow> defaultShadows = new List<TrueShadow>(128);

		private readonly List<ShadowUpdateGroup> groups = new List<ShadowUpdateGroup>(4);

		public static TrueShadowManager Instance
		{
			get
			{
				if (!_instance)
				{
					GameObject obj = new GameObject("[TrueShadowManager]");
					UnityEngine.Object.DontDestroyOnLoad(obj);
					_instance = obj.AddComponent<TrueShadowManager>();
				}
				return _instance;
			}
		}

		private void OnEnable()
		{
			Canvas.willRenderCanvases += OnWillRenderCanvas;
		}

		private void OnDisable()
		{
			Canvas.willRenderCanvases -= OnWillRenderCanvas;
		}

		public void Register(TrueShadow shadow)
		{
			defaultShadows.Add(shadow);
		}

		public void Unregister(TrueShadow shadow)
		{
			defaultShadows.Remove(shadow);
		}

		public void RegisterGroup(ShadowUpdateGroup group)
		{
			if (!groups.Contains(group))
			{
				groups.Add(group);
			}
		}

		public void UnregisterGroup(ShadowUpdateGroup group)
		{
			groups.Remove(group);
		}

		private void LateUpdate()
		{
			UpdateShadows(lateUpdate: true);
		}

		private void OnWillRenderCanvas()
		{
			UpdateShadows(lateUpdate: false);
		}

		private void UpdateShadows(bool lateUpdate)
		{
			for (int i = 0; i < groups.Count; i++)
			{
				ShadowUpdateGroup shadowUpdateGroup = groups[i];
				if (!shadowUpdateGroup.ShouldUpdate)
				{
					continue;
				}
				List<TrueShadow> shadows = shadowUpdateGroup.Shadows;
				int count = shadows.Count;
				for (int j = 0; j < count; j++)
				{
					TrueShadow trueShadow = shadows[j];
					if (trueShadow.isActiveAndEnabled)
					{
						if (lateUpdate)
						{
							trueShadow.PerformLateUpdate();
							trueShadow.shadowRenderer.PerformLateUpdate();
						}
						else
						{
							trueShadow.OnWillRenderCanvas();
						}
					}
				}
			}
			int count2 = defaultShadows.Count;
			for (int k = 0; k < count2; k++)
			{
				TrueShadow trueShadow2 = defaultShadows[k];
				if (trueShadow2.isActiveAndEnabled)
				{
					if (lateUpdate)
					{
						trueShadow2.PerformLateUpdate();
						trueShadow2.shadowRenderer.PerformLateUpdate();
					}
					else
					{
						trueShadow2.OnWillRenderCanvas();
					}
				}
			}
		}

		public static List<TrueShadow> GetUngroupedShadows()
		{
			return Instance.defaultShadows;
		}
	}
	internal class IndexedSet<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		private readonly List<T> list = new List<T>();

		private readonly Dictionary<T, int> dict = new Dictionary<T, int>();

		public int Count => list.Count;

		public bool IsReadOnly => false;

		public T this[int index]
		{
			get
			{
				return list[index];
			}
			set
			{
				T key = list[index];
				dict.Remove(key);
				list[index] = value;
				dict.Add(key, index);
			}
		}

		public void Add(T item)
		{
			dict.Add(item, list.Count);
			list.Add(item);
		}

		public bool AddUnique(T item)
		{
			if (dict.ContainsKey(item))
			{
				return false;
			}
			dict.Add(item, list.Count);
			list.Add(item);
			return true;
		}

		public bool Remove(T item)
		{
			if (!dict.TryGetValue(item, out var value))
			{
				return false;
			}
			RemoveAt(value);
			return true;
		}

		public void Remove(Predicate<T> match)
		{
			int num = 0;
			while (num < list.Count)
			{
				T val = list[num];
				if (match(val))
				{
					Remove(val);
				}
				else
				{
					num++;
				}
			}
		}

		public void Clear()
		{
			list.Clear();
			dict.Clear();
		}

		public bool Contains(T item)
		{
			return dict.ContainsKey(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			list.CopyTo(array, arrayIndex);
		}

		public int IndexOf(T item)
		{
			if (dict.TryGetValue(item, out var value))
			{
				return value;
			}
			return -1;
		}

		public void Insert(int index, T item)
		{
			throw new NotSupportedException("Random Insertion is semantically invalid, since this structure does not guarantee ordering.");
		}

		public void RemoveAt(int index)
		{
			T key = list[index];
			dict.Remove(key);
			if (index == list.Count - 1)
			{
				list.RemoveAt(index);
				return;
			}
			int index2 = list.Count - 1;
			T val = list[index2];
			list[index] = val;
			dict[val] = index;
			list.RemoveAt(index2);
		}

		public void Sort(Comparison<T> sortLayoutFunction)
		{
			list.Sort(sortLayoutFunction);
			for (int i = 0; i < list.Count; i++)
			{
				T key = list[i];
				dict[key] = i;
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	public static class Math
	{
		public static float Angle360(Vector2 from, Vector2 to)
		{
			float num = Vector2.SignedAngle(from, to);
			if (!(num < 0f))
			{
				return num;
			}
			return 360f + num;
		}

		public static Vector2 AngleDistanceVector(float angle, float distance, Vector2 zeroVector)
		{
			return Quaternion.Euler(0f, 0f, 0f - angle) * zeroVector * distance;
		}

		public static Vector2 Rotate(this Vector2 v, float angle)
		{
			float f = angle * (MathF.PI / 180f);
			float num = Mathf.Sin(f);
			float num2 = Mathf.Cos(f);
			return new Vector2(num2 * v.x - num * v.y, num * v.x + num2 * v.y);
		}
	}
	public static class ObjectHandle
	{
		public static ObjectHandle<T> Take<T>(T obj) where T : UnityEngine.Object
		{
			return new ObjectHandle<T>(obj, own: true);
		}

		public static ObjectHandle<T> Borrow<T>(T obj) where T : UnityEngine.Object
		{
			return new ObjectHandle<T>(obj, own: false);
		}
	}
	public readonly struct ObjectHandle<T> : IDisposable where T : UnityEngine.Object
	{
		public readonly bool own;

		public readonly T obj;

		internal ObjectHandle(T obj, bool own)
		{
			this.obj = obj;
			this.own = own;
		}

		public void Dispose()
		{
			if (own && obj != null)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(obj);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(obj);
				}
			}
		}

		public static implicit operator T(ObjectHandle<T> handle)
		{
			return handle.obj;
		}
	}
	public class KnobAttribute : PropertyAttribute
	{
	}
	public class ToggleButtonsAttribute : PropertyAttribute
	{
	}
	public class InsetToggleAttribute : ToggleButtonsAttribute
	{
	}
	public class SpreadSliderAttribute : PropertyAttribute
	{
	}
}
namespace LeTai.TrueShadow.PluginInterfaces
{
	public interface ITrueShadowCasterMaterialProvider
	{
		event Action materialReplaced;

		event Action materialModified;

		Material GetTrueShadowCasterMaterial();
	}
	public interface ITrueShadowCasterSubMeshMaterialProvider
	{
		Material GetTrueShadowCasterMaterialForSubMesh(int subMeshIndex);
	}
	public interface ITrueShadowCasterMeshProvider
	{
		event Action<Mesh> trueShadowCasterMeshChanged;
	}
	public interface ITrueShadowCasterMeshModifier
	{
		void ModifyTrueShadowCasterMesh(Mesh mesh);
	}
	public interface ITrueShadowCasterMaterialPropertiesModifier
	{
		void ModifyTrueShadowCasterMaterialProperties(MaterialPropertyBlock propertyBlock);
	}
	public interface ITrueShadowCasterClearColorProvider
	{
		Color GetTrueShadowCasterClearColor();
	}
	public interface ITrueShadowRendererMaterialProvider
	{
		event Action materialReplaced;

		event Action materialModified;

		Material GetTrueShadowRendererMaterial();
	}
	public interface ITrueShadowRendererMaterialModifier
	{
		void ModifyTrueShadowRendererMaterial(Material baseMaterial);
	}
	public interface ITrueShadowRendererMeshModifier
	{
		void ModifyTrueShadowRendererMesh(VertexHelper vertexHelper);
	}
	public interface ITrueShadowCustomHashProvider
	{
	}
	public interface ITrueShadowCustomHashProviderV2
	{
		event Action<int> trueShadowCustomHashChanged;
	}
}
namespace LeTai.Effects
{
	public abstract class BlurConfig : ScriptableObject
	{
		public abstract float Strength { get; set; }

		public abstract int MinExtent { get; }
	}
	public class BlurHQConfig : BlurConfig
	{
		[SerializeField]
		private float strength;

		private static readonly float SQRT05 = Mathf.Sqrt(2f);

		internal readonly float[] rawWeights = new float[128];

		internal readonly float[] weights = new float[64];

		internal readonly float[] offsets = new float[64];

		public int Extent { get; private set; }

		public int NumWeight { get; private set; }

		public override int MinExtent => 1;

		public override float Strength
		{
			get
			{
				return strength;
			}
			set
			{
				float b = Mathf.Min(value, 126f);
				if (Mathf.Approximately(strength, b))
				{
					return;
				}
				strength = b;
				Extent = Mathf.CeilToInt(strength);
				float num = 0.5f * strength;
				float num2 = 1f / num * SQRT05;
				float num3 = (rawWeights[0] = 1f);
				for (int i = 1; i <= Extent; i++)
				{
					float num6;
					if (num < 0.8f)
					{
						float num4 = Erf(((float)i + 0.5f) * num2);
						float num5 = Erf(((float)i - 0.5f) * num2);
						num6 = (num4 - num5) / 2f;
					}
					else
					{
						num6 = Mathf.Exp((float)(-i * i) / num / num);
					}
					rawWeights[i] = num6;
					num3 += num6 * 2f;
				}
				float num7 = 1f / num3;
				for (int j = 0; j <= Extent; j++)
				{
					rawWeights[j] *= num7;
				}
				weights[0] = rawWeights[0];
				NumWeight = Mathf.CeilToInt((float)Extent / 2f) + 1;
				for (int k = 1; k < NumWeight; k++)
				{
					int num8 = k * 2;
					int num9 = num8 - 1;
					float num10 = rawWeights[num9];
					float num11 = ((num8 <= Extent) ? rawWeights[num8] : 0f);
					float num12 = num10 + num11;
					weights[k] = num12;
					offsets[k] = (float)num9 + num11 / num12;
				}
			}
		}

		private static float Erf(float x)
		{
			int num = ((!(x < 0f)) ? 1 : (-1));
			x = Mathf.Abs(x);
			float num2 = 1f / (1f + 0.3275911f * x);
			float num3 = 1f - ((((1.0614054f * num2 + -1.4531521f) * num2 + 1.4214138f) * num2 + -244f / (273f * MathF.PI)) * num2 + 0.2548296f) * num2 * Mathf.Exp((0f - x) * x);
			return (float)num * num3;
		}
	}
	public class BlurHQ : IBlurAlgorithm
	{
		public const float MAX_RADIUS = 126f;

		private BlurHQConfig config;

		private Material _material;

		private Material Material
		{
			get
			{
				if (_material == null)
				{
					_material = new Material(Shader.Find("Hidden/TrueShadow/GenerateHQ"));
				}
				return _material;
			}
		}

		public void Configure(BlurConfig config)
		{
			this.config = (BlurHQConfig)config;
		}

		public void Blur(CommandBuffer cmd, RenderTargetIdentifier src, Rect srcCropRegion, RenderTexture target)
		{
			cmd.GetTemporaryRT(ShaderId.TMP_TEX, target.width, target.height, 0, FilterMode.Bilinear, target.format);
			Material.DisableKeyword("MAX_TAPS_64");
			Material.DisableKeyword("MAX_TAPS_16");
			Material.DisableKeyword("MAX_TAPS_4");
			if (config.NumWeight <= 8)
			{
				Material.EnableKeyword("MAX_TAPS_8");
			}
			else if (config.NumWeight <= 32)
			{
				Material.EnableKeyword("MAX_TAPS_32");
			}
			else
			{
				Material.EnableKeyword("MAX_TAPS_64");
			}
			Material.SetInt(ShaderId.EXTENT, config.Extent);
			Material.SetFloatArray(ShaderId.WEIGHTS, config.weights);
			Material.SetFloatArray(ShaderId.OFFSETS, config.offsets);
			Material.SetTexture(ShaderId.BLUE_NOISE, Resources.BLUE_NOISE);
			Material.SetVector(ShaderId.TARGET_SIZE, new Vector4(target.width, target.height));
			cmd.Blit(src, ShaderId.TMP_TEX, Material, 0);
			cmd.Blit(ShaderId.TMP_TEX, target, Material, 1);
			cmd.ReleaseTemporaryRT(ShaderId.TMP_TEX);
		}
	}
	public interface IBlurAlgorithm
	{
		void Configure(BlurConfig config);

		void Blur(CommandBuffer cmd, RenderTargetIdentifier src, Rect srcCropRegion, RenderTexture target);
	}
	public enum BlurAlgorithmSelection
	{
		Fast,
		Accurate
	}
	public static class Resources
	{
		public static readonly Texture2D BLUE_NOISE = UnityEngine.Resources.Load<Texture2D>("True Shadow Blue Noise");
	}
	public class ScalableBlur : IBlurAlgorithm
	{
		private Material material;

		private ScalableBlurConfig config;

		private const int BLUR_PASS = 0;

		private const int CROP_BLUR_PASS = 1;

		private const int DITHER_BLUR_PASS = 2;

		private Material Material
		{
			get
			{
				if (material == null)
				{
					material = new Material(Shader.Find("Hidden/TrueShadow/Generate"));
				}
				return material;
			}
			set
			{
				material = value;
			}
		}

		public void Configure(BlurConfig config)
		{
			this.config = (ScalableBlurConfig)config;
		}

		public void Blur(CommandBuffer cmd, RenderTargetIdentifier src, Rect srcCropRegion, RenderTexture target)
		{
			float radius = config.Radius;
			Material.SetFloat(ShaderProperties.blurRadius, radius);
			Material.SetVector(ShaderProperties.blurTextureCropRegion, srcCropRegion.ToMinMaxVector());
			int downsampleFactor = ((config.Iteration > 0) ? 1 : 0);
			int num = Mathf.Max(config.Iteration * 2 - 1, 1);
			int num2 = ShaderProperties.intermediateRT[0];
			CreateTempRenderTextureFrom(cmd, num2, target, downsampleFactor);
			cmd.Blit(src, num2, Material, 1);
			for (int i = 1; i < num; i++)
			{
				BlurAtDepth(cmd, i, target);
			}
			Material.SetTexture(ShaderId.BLUE_NOISE, Resources.BLUE_NOISE);
			Material.SetVector(ShaderId.TARGET_SIZE, new Vector4(target.width, target.height));
			cmd.Blit(ShaderProperties.intermediateRT[num - 1], target, Material, 2);
			CleanupIntermediateRT(cmd, num);
		}

		protected virtual void BlurAtDepth(CommandBuffer cmd, int depth, RenderTexture baseTexture)
		{
			int a = Utility.SimplePingPong(depth, config.Iteration - 1) + 1;
			a = Mathf.Min(a, config.MaxDepth);
			CreateTempRenderTextureFrom(cmd, ShaderProperties.intermediateRT[depth], baseTexture, a);
			cmd.Blit(ShaderProperties.intermediateRT[depth - 1], ShaderProperties.intermediateRT[depth], Material, 0);
		}

		private static void CreateTempRenderTextureFrom(CommandBuffer cmd, int nameId, RenderTexture src, int downsampleFactor)
		{
			int width = src.width >> downsampleFactor;
			int height = src.height >> downsampleFactor;
			cmd.GetTemporaryRT(nameId, width, height, 0, FilterMode.Bilinear, src.format);
		}

		private static void CleanupIntermediateRT(CommandBuffer cmd, int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				cmd.ReleaseTemporaryRT(ShaderProperties.intermediateRT[i]);
			}
		}
	}
	public class ScalableBlurConfig : BlurConfig
	{
		[SerializeField]
		private float radius = 4f;

		[SerializeField]
		private int iteration = 4;

		[SerializeField]
		private int maxDepth = 6;

		[SerializeField]
		[Range(0f, 256f)]
		private float strength;

		private static readonly float UNIT_VARIANCE = 1f + Mathf.Sqrt(2f) / 2f;

		public float Radius
		{
			get
			{
				return radius;
			}
			set
			{
				radius = Mathf.Max(0f, value);
			}
		}

		public int Iteration
		{
			get
			{
				return iteration;
			}
			set
			{
				iteration = Mathf.Max(0, value);
			}
		}

		public int MaxDepth
		{
			get
			{
				return maxDepth;
			}
			set
			{
				maxDepth = Mathf.Max(1, value);
			}
		}

		public override int MinExtent => (Iteration + 1) * (Iteration + 1);

		public override float Strength
		{
			get
			{
				return strength = radius * (float)(3 * (1 << iteration) - 2) / UNIT_VARIANCE;
			}
			set
			{
				strength = Mathf.Max(0f, value);
				SetAdvancedFieldFromSimple();
			}
		}

		protected virtual void SetAdvancedFieldFromSimple()
		{
			strength = Mathf.Clamp(strength, 0f, 268435460f);
			float num = strength * 0.66f;
			radius = Mathf.Sqrt(num);
			iteration = 0;
			while ((float)(1 << iteration) < radius)
			{
				iteration++;
			}
			radius = num / (float)(1 << iteration);
		}

		private void OnValidate()
		{
			SetAdvancedFieldFromSimple();
		}
	}
	public static class ShaderProperties
	{
		private static bool isInitialized;

		public static int[] intermediateRT;

		public static int blurRadius;

		public static int blurTextureCropRegion;

		public static void Init()
		{
			if (!isInitialized)
			{
				blurRadius = Shader.PropertyToID("_Radius");
				blurTextureCropRegion = Shader.PropertyToID("_CropRegion");
				isInitialized = true;
			}
		}

		public static void Init(int stackDepth)
		{
			intermediateRT = new int[stackDepth * 2 - 1];
			for (int i = 0; i < intermediateRT.Length; i++)
			{
				intermediateRT[i] = Shader.PropertyToID($"TI_intermediate_rt_{i}");
			}
			Init();
		}
	}
}
