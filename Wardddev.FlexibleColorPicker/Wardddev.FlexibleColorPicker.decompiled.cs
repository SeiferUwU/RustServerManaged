using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Rust.Workshop.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyVersion("0.0.0.0")]
[RequireComponent(typeof(FlexibleColorPicker))]
public class FCP_Persistence : MonoBehaviour
{
	public enum SaveStrategy
	{
		SessionOnly,
		File,
		PlayerPrefs
	}

	public string saveName = GenerateID();

	public SaveStrategy saveStrategy;

	private static string GenerateID()
	{
		return Convert.ToBase64String(BitConverter.GetBytes(DateTime.Now.Ticks));
	}
}
[ExecuteInEditMode]
public class FCP_SpriteMeshEditor : MonoBehaviour
{
	public enum MeshType
	{
		CenterPoint,
		forward,
		backward
	}

	public int x;

	public int y;

	public MeshType meshType;

	public Sprite sprite;
}
public class FlexibleColorPicker : MonoBehaviour, IWorkshopColourPicker
{
	[Serializable]
	private struct Picker
	{
		public Image image;

		public Sprite dynamicSprite;

		public Sprite staticSpriteHor;

		public Sprite staticSpriteVer;

		public Material dynamicMaterial;

		public RectTransform marker;
	}

	private enum PickerType
	{
		Main,
		R,
		G,
		B,
		H,
		S,
		V,
		A,
		Preview,
		PreviewAlpha
	}

	public enum MainPickingMode
	{
		HS,
		HV,
		SH,
		SV,
		VH,
		VS
	}

	[Serializable]
	public class AdvancedSettings
	{
		[Serializable]
		public class PSettings
		{
			[Tooltip("Value can be used to restrict slider range")]
			public Vector2 range = new Vector2(0f, 1f);

			[Tooltip("Make the picker associated with this value act static, even in a dynamic color picker setup")]
			public bool overrideStatic;
		}

		public bool mainStatic = true;

		public PSettings r;

		public PSettings g;

		public PSettings b;

		public PSettings h;

		public PSettings s;

		public PSettings v;

		public PSettings a;

		public PSettings Get(int i)
		{
			if (i <= 0 || i > 7)
			{
				return new PSettings();
			}
			return (new PSettings[7] { r, g, b, h, s, v, a })[i - 1];
		}
	}

	[Tooltip("Connections to the FCP's picker images, this should not be adjusted unless in advanced use cases.")]
	[SerializeField]
	private Picker[] pickers;

	[Tooltip("Connection to the FCP's hexadecimal input field.")]
	[SerializeField]
	private TMP_InputField hexInput;

	[Tooltip("Connection to the FCP's mode dropdown menu.")]
	[SerializeField]
	private Dropdown modeDropdown;

	private Canvas canvas;

	[Tooltip("The (starting) 2D picking mode, i.e. the 2 color values that can be picked with the large square picker.")]
	[SerializeField]
	private MainPickingMode mode;

	[Tooltip("Sprites to be used in static mode on the main picker, one for each 2D mode.")]
	[SerializeField]
	private Sprite[] staticSpriteMain;

	[Tooltip("Color set to the color picker on Start(). If you wish to set a starting color via script please used the standard color parameter of the FCP in stead.")]
	[SerializeField]
	private Color startingColor = Color.white;

	[Tooltip("Use static mode: picker images are replaced by static images in stead of adaptive Unity shaders.")]
	public bool staticMode;

	[Tooltip("Make sure FCP seperates its picker materials so that the dynamic mode works consistently, even when multiple FPCs are active at the same time. Turning this off yields a slight performance boost.")]
	public bool multiInstance = true;

	[Tooltip("More specific settings for color picker. Changes are not applied immediately, but require an FCP update to trigger.")]
	public AdvancedSettings advancedSettings;

	[field: SerializeField]
	public UnityEvent<Color> onColorChange { get; set; }

	[field: SerializeField]
	public UnityEvent<Color> onInitialColorSet { get; set; }
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
			FilePathsData = new byte[231]
			{
				0, 0, 0, 1, 0, 0, 0, 66, 92, 65,
				115, 115, 101, 116, 115, 92, 84, 104, 105, 114,
				100, 32, 80, 97, 114, 116, 121, 92, 70, 108,
				101, 120, 105, 98, 108, 101, 67, 111, 108, 111,
				114, 80, 105, 99, 107, 101, 114, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 70, 67, 80, 95,
				80, 101, 114, 115, 105, 115, 116, 101, 110, 99,
				101, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 71, 92, 65, 115, 115, 101, 116, 115, 92,
				84, 104, 105, 114, 100, 32, 80, 97, 114, 116,
				121, 92, 70, 108, 101, 120, 105, 98, 108, 101,
				67, 111, 108, 111, 114, 80, 105, 99, 107, 101,
				114, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				70, 67, 80, 95, 83, 112, 114, 105, 116, 101,
				77, 101, 115, 104, 69, 100, 105, 116, 111, 114,
				46, 99, 115, 0, 0, 0, 4, 0, 0, 0,
				70, 92, 65, 115, 115, 101, 116, 115, 92, 84,
				104, 105, 114, 100, 32, 80, 97, 114, 116, 121,
				92, 70, 108, 101, 120, 105, 98, 108, 101, 67,
				111, 108, 111, 114, 80, 105, 99, 107, 101, 114,
				92, 83, 99, 114, 105, 112, 116, 115, 92, 70,
				108, 101, 120, 105, 98, 108, 101, 67, 111, 108,
				111, 114, 80, 105, 99, 107, 101, 114, 46, 99,
				115
			},
			TypesData = new byte[195]
			{
				0, 0, 0, 0, 16, 124, 70, 67, 80, 95,
				80, 101, 114, 115, 105, 115, 116, 101, 110, 99,
				101, 0, 0, 0, 0, 21, 124, 70, 67, 80,
				95, 83, 112, 114, 105, 116, 101, 77, 101, 115,
				104, 69, 100, 105, 116, 111, 114, 0, 0, 0,
				0, 20, 124, 70, 108, 101, 120, 105, 98, 108,
				101, 67, 111, 108, 111, 114, 80, 105, 99, 107,
				101, 114, 0, 0, 0, 0, 26, 70, 108, 101,
				120, 105, 98, 108, 101, 67, 111, 108, 111, 114,
				80, 105, 99, 107, 101, 114, 124, 80, 105, 99,
				107, 101, 114, 0, 0, 0, 0, 36, 70, 108,
				101, 120, 105, 98, 108, 101, 67, 111, 108, 111,
				114, 80, 105, 99, 107, 101, 114, 124, 65, 100,
				118, 97, 110, 99, 101, 100, 83, 101, 116, 116,
				105, 110, 103, 115, 0, 0, 0, 0, 46, 70,
				108, 101, 120, 105, 98, 108, 101, 67, 111, 108,
				111, 114, 80, 105, 99, 107, 101, 114, 43, 65,
				100, 118, 97, 110, 99, 101, 100, 83, 101, 116,
				116, 105, 110, 103, 115, 124, 80, 83, 101, 116,
				116, 105, 110, 103, 115
			},
			TotalFiles = 3,
			TotalTypes = 6,
			IsEditorOnly = false
		};
	}
}
