using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.Serialization;
using UnityEngine.TextCore.LowLevel;

[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.FontEngine.Tests")]
[assembly: InternalsVisibleTo("Unity.TextCore.Tests")]
[assembly: InternalsVisibleTo("Unity.TextMeshPro.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.TextCore.Tools")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreModule")]
[assembly: InternalsVisibleTo("Unity.TextCore.FontEngine.Tools")]
[assembly: InternalsVisibleTo("Unity.TextCore.FontEngine")]
[assembly: InternalsVisibleTo("Unity.FontEngine")]
[assembly: InternalsVisibleTo("Unity.TextMeshPro")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.UIElements.Text")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine.TextCore.Text;

[Serializable]
public class Character : TextElement
{
	public Character()
	{
		m_ElementType = TextElementType.Character;
		base.scale = 1f;
	}

	public Character(uint unicode, Glyph glyph)
	{
		m_ElementType = TextElementType.Character;
		base.unicode = unicode;
		base.textAsset = null;
		base.glyph = glyph;
		base.glyphIndex = glyph.index;
		base.scale = 1f;
	}

	public Character(uint unicode, FontAsset fontAsset, Glyph glyph)
	{
		m_ElementType = TextElementType.Character;
		base.unicode = unicode;
		base.textAsset = fontAsset;
		base.glyph = glyph;
		base.glyphIndex = glyph.index;
		base.scale = 1f;
	}

	internal Character(uint unicode, uint glyphIndex)
	{
		m_ElementType = TextElementType.Character;
		base.unicode = unicode;
		base.textAsset = null;
		base.glyph = null;
		base.glyphIndex = glyphIndex;
		base.scale = 1f;
	}
}
internal static class ColorUtilities
{
	internal static bool CompareColors(Color32 a, Color32 b)
	{
		return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
	}

	internal static bool CompareColorsRgb(Color32 a, Color32 b)
	{
		return a.r == b.r && a.g == b.g && a.b == b.b;
	}

	internal static bool CompareColors(Color a, Color b)
	{
		return Mathf.Approximately(a.r, b.r) && Mathf.Approximately(a.g, b.g) && Mathf.Approximately(a.b, b.b) && Mathf.Approximately(a.a, b.a);
	}

	internal static bool CompareColorsRgb(Color a, Color b)
	{
		return Mathf.Approximately(a.r, b.r) && Mathf.Approximately(a.g, b.g) && Mathf.Approximately(a.b, b.b);
	}

	internal static Color32 MultiplyColors(Color32 c1, Color32 c2)
	{
		byte r = (byte)((float)(int)c1.r / 255f * ((float)(int)c2.r / 255f) * 255f);
		byte g = (byte)((float)(int)c1.g / 255f * ((float)(int)c2.g / 255f) * 255f);
		byte b = (byte)((float)(int)c1.b / 255f * ((float)(int)c2.b / 255f) * 255f);
		byte a = (byte)((float)(int)c1.a / 255f * ((float)(int)c2.a / 255f) * 255f);
		return new Color32(r, g, b, a);
	}
}
public class FastAction
{
	private LinkedList<Action> delegates = new LinkedList<Action>();

	private Dictionary<Action, LinkedListNode<Action>> lookup = new Dictionary<Action, LinkedListNode<Action>>();

	public void Add(Action rhs)
	{
		if (!lookup.ContainsKey(rhs))
		{
			lookup[rhs] = delegates.AddLast(rhs);
		}
	}

	public void Remove(Action rhs)
	{
		if (lookup.TryGetValue(rhs, out var value))
		{
			lookup.Remove(rhs);
			delegates.Remove(value);
		}
	}

	public void Call()
	{
		for (LinkedListNode<Action> linkedListNode = delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			linkedListNode.Value();
		}
	}
}
public class FastAction<A>
{
	private LinkedList<Action<A>> delegates = new LinkedList<Action<A>>();

	private Dictionary<Action<A>, LinkedListNode<Action<A>>> lookup = new Dictionary<Action<A>, LinkedListNode<Action<A>>>();

	public void Add(Action<A> rhs)
	{
		if (!lookup.ContainsKey(rhs))
		{
			lookup[rhs] = delegates.AddLast(rhs);
		}
	}

	public void Remove(Action<A> rhs)
	{
		if (lookup.TryGetValue(rhs, out var value))
		{
			lookup.Remove(rhs);
			delegates.Remove(value);
		}
	}

	public void Call(A a)
	{
		for (LinkedListNode<Action<A>> linkedListNode = delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			linkedListNode.Value(a);
		}
	}
}
public class FastAction<A, B>
{
	private LinkedList<Action<A, B>> delegates = new LinkedList<Action<A, B>>();

	private Dictionary<Action<A, B>, LinkedListNode<Action<A, B>>> lookup = new Dictionary<Action<A, B>, LinkedListNode<Action<A, B>>>();

	public void Add(Action<A, B> rhs)
	{
		if (!lookup.ContainsKey(rhs))
		{
			lookup[rhs] = delegates.AddLast(rhs);
		}
	}

	public void Remove(Action<A, B> rhs)
	{
		if (lookup.TryGetValue(rhs, out var value))
		{
			lookup.Remove(rhs);
			delegates.Remove(value);
		}
	}

	public void Call(A a, B b)
	{
		for (LinkedListNode<Action<A, B>> linkedListNode = delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			linkedListNode.Value(a, b);
		}
	}
}
public class FastAction<A, B, C>
{
	private LinkedList<Action<A, B, C>> delegates = new LinkedList<Action<A, B, C>>();

	private Dictionary<Action<A, B, C>, LinkedListNode<Action<A, B, C>>> lookup = new Dictionary<Action<A, B, C>, LinkedListNode<Action<A, B, C>>>();

	public void Add(Action<A, B, C> rhs)
	{
		if (!lookup.ContainsKey(rhs))
		{
			lookup[rhs] = delegates.AddLast(rhs);
		}
	}

	public void Remove(Action<A, B, C> rhs)
	{
		if (lookup.TryGetValue(rhs, out var value))
		{
			lookup.Remove(rhs);
			delegates.Remove(value);
		}
	}

	public void Call(A a, B b, C c)
	{
		for (LinkedListNode<Action<A, B, C>> linkedListNode = delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			linkedListNode.Value(a, b, c);
		}
	}
}
public enum TextFontWeight
{
	Thin = 100,
	ExtraLight = 200,
	Light = 300,
	Regular = 400,
	Medium = 500,
	SemiBold = 600,
	Bold = 700,
	Heavy = 800,
	Black = 900
}
[Serializable]
public struct FontWeightPair
{
	public FontAsset regularTypeface;

	public FontAsset italicTypeface;
}
[Serializable]
[ExcludeFromDocs]
public struct FontAssetCreationEditorSettings
{
	public string sourceFontFileGUID;

	public int faceIndex;

	public int pointSizeSamplingMode;

	public int pointSize;

	public int padding;

	public int paddingMode;

	public int packingMode;

	public int atlasWidth;

	public int atlasHeight;

	public int characterSetSelectionMode;

	public string characterSequence;

	public string referencedFontAssetGUID;

	public string referencedTextAssetGUID;

	public int fontStyle;

	public float fontStyleModifier;

	public int renderMode;

	public bool includeFontFeatures;

	internal FontAssetCreationEditorSettings(string sourceFontFileGUID, int pointSize, int pointSizeSamplingMode, int padding, int packingMode, int atlasWidth, int atlasHeight, int characterSelectionMode, string characterSet, int renderMode)
	{
		this.sourceFontFileGUID = sourceFontFileGUID;
		faceIndex = 0;
		this.pointSize = pointSize;
		this.pointSizeSamplingMode = pointSizeSamplingMode;
		this.padding = padding;
		paddingMode = 2;
		this.packingMode = packingMode;
		this.atlasWidth = atlasWidth;
		this.atlasHeight = atlasHeight;
		characterSequence = characterSet;
		characterSetSelectionMode = characterSelectionMode;
		this.renderMode = renderMode;
		referencedFontAssetGUID = string.Empty;
		referencedTextAssetGUID = string.Empty;
		fontStyle = 0;
		fontStyleModifier = 0f;
		includeFontFeatures = false;
	}
}
public enum AtlasPopulationMode
{
	Static,
	Dynamic,
	DynamicOS
}
[Serializable]
[ExcludeFromPreset]
public class FontAsset : TextAsset
{
	[SerializeField]
	internal string m_SourceFontFileGUID;

	[SerializeField]
	internal FontAssetCreationEditorSettings m_fontAssetCreationEditorSettings;

	[SerializeField]
	private Font m_SourceFontFile;

	[SerializeField]
	private string m_SourceFontFilePath;

	[SerializeField]
	private AtlasPopulationMode m_AtlasPopulationMode;

	[SerializeField]
	internal bool InternalDynamicOS;

	[SerializeField]
	internal FaceInfo m_FaceInfo;

	private int m_FamilyNameHashCode;

	private int m_StyleNameHashCode;

	[SerializeField]
	internal List<Glyph> m_GlyphTable = new List<Glyph>();

	internal Dictionary<uint, Glyph> m_GlyphLookupDictionary;

	[SerializeField]
	internal List<Character> m_CharacterTable = new List<Character>();

	internal Dictionary<uint, Character> m_CharacterLookupDictionary;

	internal Texture2D m_AtlasTexture;

	[SerializeField]
	internal Texture2D[] m_AtlasTextures;

	[SerializeField]
	internal int m_AtlasTextureIndex;

	[SerializeField]
	private bool m_IsMultiAtlasTexturesEnabled;

	[SerializeField]
	private bool m_ClearDynamicDataOnBuild;

	[SerializeField]
	internal int m_AtlasWidth;

	[SerializeField]
	internal int m_AtlasHeight;

	[SerializeField]
	internal int m_AtlasPadding;

	[SerializeField]
	internal GlyphRenderMode m_AtlasRenderMode;

	[SerializeField]
	private List<GlyphRect> m_UsedGlyphRects;

	[SerializeField]
	private List<GlyphRect> m_FreeGlyphRects;

	[SerializeField]
	internal FontFeatureTable m_FontFeatureTable = new FontFeatureTable();

	[SerializeField]
	internal List<FontAsset> m_FallbackFontAssetTable;

	[SerializeField]
	private FontWeightPair[] m_FontWeightTable = new FontWeightPair[10];

	[SerializeField]
	[FormerlySerializedAs("normalStyle")]
	internal float m_RegularStyleWeight = 0f;

	[FormerlySerializedAs("normalSpacingOffset")]
	[SerializeField]
	internal float m_RegularStyleSpacing = 0f;

	[FormerlySerializedAs("boldStyle")]
	[SerializeField]
	internal float m_BoldStyleWeight = 0.75f;

	[FormerlySerializedAs("boldSpacing")]
	[SerializeField]
	internal float m_BoldStyleSpacing = 7f;

	[SerializeField]
	[FormerlySerializedAs("italicStyle")]
	internal byte m_ItalicStyleSlant = 35;

	[SerializeField]
	[FormerlySerializedAs("tabSize")]
	internal byte m_TabMultiple = 10;

	internal bool IsFontAssetLookupTablesDirty;

	private static ProfilerMarker k_ReadFontAssetDefinitionMarker = new ProfilerMarker("FontAsset.ReadFontAssetDefinition");

	private static ProfilerMarker k_AddSynthesizedCharactersMarker = new ProfilerMarker("FontAsset.AddSynthesizedCharacters");

	private static ProfilerMarker k_TryAddCharacterMarker = new ProfilerMarker("FontAsset.TryAddCharacter");

	private static ProfilerMarker k_TryAddCharactersMarker = new ProfilerMarker("FontAsset.TryAddCharacters");

	private static ProfilerMarker k_UpdateGlyphAdjustmentRecordsMarker = new ProfilerMarker("FontAsset.UpdateGlyphAdjustmentRecords");

	private static ProfilerMarker k_UpdateDiacriticalMarkAdjustmentRecordsMarker = new ProfilerMarker("FontAsset.UpdateDiacriticalAdjustmentRecords");

	private static ProfilerMarker k_ClearFontAssetDataMarker = new ProfilerMarker("FontAsset.ClearFontAssetData");

	private static ProfilerMarker k_UpdateFontAssetDataMarker = new ProfilerMarker("FontAsset.UpdateFontAssetData");

	private static ProfilerMarker k_TryAddGlyphMarker = new ProfilerMarker("FontAsset.TryAddGlyphMarker");

	private static string s_DefaultMaterialSuffix = " Atlas Material";

	private static HashSet<int> k_SearchedFontAssetLookup;

	private static List<FontAsset> k_FontAssets_FontFeaturesUpdateQueue = new List<FontAsset>();

	private static HashSet<int> k_FontAssets_FontFeaturesUpdateQueueLookup = new HashSet<int>();

	private static List<Texture2D> k_FontAssets_AtlasTexturesUpdateQueue = new List<Texture2D>();

	private static HashSet<int> k_FontAssets_AtlasTexturesUpdateQueueLookup = new HashSet<int>();

	private List<Glyph> m_GlyphsToRender = new List<Glyph>();

	private List<Glyph> m_GlyphsRendered = new List<Glyph>();

	private List<uint> m_GlyphIndexList = new List<uint>();

	private List<uint> m_GlyphIndexListNewlyAdded = new List<uint>();

	internal List<uint> m_GlyphsToAdd = new List<uint>();

	internal HashSet<uint> m_GlyphsToAddLookup = new HashSet<uint>();

	internal List<Character> m_CharactersToAdd = new List<Character>();

	internal HashSet<uint> m_CharactersToAddLookup = new HashSet<uint>();

	internal List<uint> s_MissingCharacterList = new List<uint>();

	internal HashSet<uint> m_MissingUnicodesFromFontFile = new HashSet<uint>();

	internal static uint[] k_GlyphIndexArray;

	public FontAssetCreationEditorSettings fontAssetCreationEditorSettings
	{
		get
		{
			return m_fontAssetCreationEditorSettings;
		}
		set
		{
			m_fontAssetCreationEditorSettings = value;
		}
	}

	public Font sourceFontFile
	{
		get
		{
			return m_SourceFontFile;
		}
		internal set
		{
			m_SourceFontFile = value;
		}
	}

	public AtlasPopulationMode atlasPopulationMode
	{
		get
		{
			return m_AtlasPopulationMode;
		}
		set
		{
			m_AtlasPopulationMode = value;
		}
	}

	public FaceInfo faceInfo
	{
		get
		{
			return m_FaceInfo;
		}
		set
		{
			m_FaceInfo = value;
		}
	}

	internal int familyNameHashCode
	{
		get
		{
			if (m_FamilyNameHashCode == 0)
			{
				m_FamilyNameHashCode = TextUtilities.GetHashCodeCaseInSensitive(m_FaceInfo.familyName);
			}
			return m_FamilyNameHashCode;
		}
		set
		{
			m_FamilyNameHashCode = value;
		}
	}

	internal int styleNameHashCode
	{
		get
		{
			if (m_StyleNameHashCode == 0)
			{
				m_StyleNameHashCode = TextUtilities.GetHashCodeCaseInSensitive(m_FaceInfo.styleName);
			}
			return m_StyleNameHashCode;
		}
		set
		{
			m_StyleNameHashCode = value;
		}
	}

	public List<Glyph> glyphTable
	{
		get
		{
			return m_GlyphTable;
		}
		internal set
		{
			m_GlyphTable = value;
		}
	}

	public Dictionary<uint, Glyph> glyphLookupTable
	{
		get
		{
			if (m_GlyphLookupDictionary == null)
			{
				ReadFontAssetDefinition();
			}
			return m_GlyphLookupDictionary;
		}
	}

	public List<Character> characterTable
	{
		get
		{
			return m_CharacterTable;
		}
		internal set
		{
			m_CharacterTable = value;
		}
	}

	public Dictionary<uint, Character> characterLookupTable
	{
		get
		{
			if (m_CharacterLookupDictionary == null)
			{
				ReadFontAssetDefinition();
			}
			return m_CharacterLookupDictionary;
		}
	}

	public Texture2D atlasTexture
	{
		get
		{
			if (m_AtlasTexture == null)
			{
				m_AtlasTexture = atlasTextures[0];
			}
			return m_AtlasTexture;
		}
	}

	public Texture2D[] atlasTextures
	{
		get
		{
			if (m_AtlasTextures == null)
			{
			}
			return m_AtlasTextures;
		}
		set
		{
			m_AtlasTextures = value;
		}
	}

	public int atlasTextureCount => m_AtlasTextureIndex + 1;

	public bool isMultiAtlasTexturesEnabled
	{
		get
		{
			return m_IsMultiAtlasTexturesEnabled;
		}
		set
		{
			m_IsMultiAtlasTexturesEnabled = value;
		}
	}

	internal bool clearDynamicDataOnBuild
	{
		get
		{
			return m_ClearDynamicDataOnBuild;
		}
		set
		{
			m_ClearDynamicDataOnBuild = value;
		}
	}

	public int atlasWidth
	{
		get
		{
			return m_AtlasWidth;
		}
		internal set
		{
			m_AtlasWidth = value;
		}
	}

	public int atlasHeight
	{
		get
		{
			return m_AtlasHeight;
		}
		internal set
		{
			m_AtlasHeight = value;
		}
	}

	public int atlasPadding
	{
		get
		{
			return m_AtlasPadding;
		}
		internal set
		{
			m_AtlasPadding = value;
		}
	}

	public GlyphRenderMode atlasRenderMode
	{
		get
		{
			return m_AtlasRenderMode;
		}
		internal set
		{
			m_AtlasRenderMode = value;
		}
	}

	internal List<GlyphRect> usedGlyphRects
	{
		get
		{
			return m_UsedGlyphRects;
		}
		set
		{
			m_UsedGlyphRects = value;
		}
	}

	internal List<GlyphRect> freeGlyphRects
	{
		get
		{
			return m_FreeGlyphRects;
		}
		set
		{
			m_FreeGlyphRects = value;
		}
	}

	public FontFeatureTable fontFeatureTable
	{
		get
		{
			return m_FontFeatureTable;
		}
		internal set
		{
			m_FontFeatureTable = value;
		}
	}

	public List<FontAsset> fallbackFontAssetTable
	{
		get
		{
			return m_FallbackFontAssetTable;
		}
		set
		{
			m_FallbackFontAssetTable = value;
		}
	}

	public FontWeightPair[] fontWeightTable
	{
		get
		{
			return m_FontWeightTable;
		}
		internal set
		{
			m_FontWeightTable = value;
		}
	}

	public float regularStyleWeight
	{
		get
		{
			return m_RegularStyleWeight;
		}
		set
		{
			m_RegularStyleWeight = value;
		}
	}

	public float regularStyleSpacing
	{
		get
		{
			return m_RegularStyleSpacing;
		}
		set
		{
			m_RegularStyleSpacing = value;
		}
	}

	public float boldStyleWeight
	{
		get
		{
			return m_BoldStyleWeight;
		}
		set
		{
			m_BoldStyleWeight = value;
		}
	}

	public float boldStyleSpacing
	{
		get
		{
			return m_BoldStyleSpacing;
		}
		set
		{
			m_BoldStyleSpacing = value;
		}
	}

	public byte italicStyleSlant
	{
		get
		{
			return m_ItalicStyleSlant;
		}
		set
		{
			m_ItalicStyleSlant = value;
		}
	}

	public byte tabMultiple
	{
		get
		{
			return m_TabMultiple;
		}
		set
		{
			m_TabMultiple = value;
		}
	}

	public static FontAsset CreateFontAsset(string familyName, string styleName, int pointSize = 90)
	{
		if (FontEngine.TryGetSystemFontReference(familyName, styleName, out var fontRef))
		{
			return CreateFontAsset(fontRef.filePath, fontRef.faceIndex, pointSize, 9, GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.DynamicOS, enableMultiAtlasSupport: true);
		}
		Debug.Log("Unable to find a font file with the specified Family Name [" + familyName + "] and Style [" + styleName + "].");
		return null;
	}

	public static FontAsset CreateFontAsset(string fontFilePath, int faceIndex, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight)
	{
		return CreateFontAsset(fontFilePath, faceIndex, samplingPointSize, atlasPadding, renderMode, atlasWidth, atlasHeight, AtlasPopulationMode.Dynamic);
	}

	private static FontAsset CreateFontAsset(string fontFilePath, int faceIndex, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.DynamicOS, bool enableMultiAtlasSupport = true)
	{
		if (FontEngine.LoadFontFace(fontFilePath, samplingPointSize, faceIndex) != FontEngineError.Success)
		{
			Debug.Log("Unable to load font face from [" + fontFilePath + "].");
			return null;
		}
		FontAsset fontAsset = CreateFontAssetInstance(null, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
		fontAsset.m_SourceFontFilePath = fontFilePath;
		return fontAsset;
	}

	public static FontAsset CreateFontAsset(Font font)
	{
		return CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024);
	}

	public static FontAsset CreateFontAsset(Font font, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.Dynamic, bool enableMultiAtlasSupport = true)
	{
		return CreateFontAsset(font, 0, samplingPointSize, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
	}

	private static FontAsset CreateFontAsset(Font font, int faceIndex, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.Dynamic, bool enableMultiAtlasSupport = true)
	{
		if (FontEngine.LoadFontFace(font, samplingPointSize, faceIndex) != FontEngineError.Success)
		{
			if (font.name == "LegacyRuntime")
			{
				return CreateFontAsset("Arial", "Regular");
			}
			Debug.LogWarning("Unable to load font face for [" + font.name + "]. Make sure \"Include Font Data\" is enabled in the Font Import Settings.", font);
			return null;
		}
		return CreateFontAssetInstance(font, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
	}

	private static FontAsset CreateFontAssetInstance(Font font, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode, bool enableMultiAtlasSupport)
	{
		FontAsset fontAsset = ScriptableObject.CreateInstance<FontAsset>();
		fontAsset.m_Version = "1.1.0";
		fontAsset.faceInfo = FontEngine.GetFaceInfo();
		if (atlasPopulationMode == AtlasPopulationMode.Dynamic && font != null)
		{
			fontAsset.sourceFontFile = font;
		}
		fontAsset.atlasPopulationMode = atlasPopulationMode;
		fontAsset.atlasWidth = atlasWidth;
		fontAsset.atlasHeight = atlasHeight;
		fontAsset.atlasPadding = atlasPadding;
		fontAsset.atlasRenderMode = renderMode;
		fontAsset.atlasTextures = new Texture2D[1];
		Texture2D texture2D = new Texture2D(1, 1, TextureFormat.Alpha8, mipChain: false);
		fontAsset.atlasTextures[0] = texture2D;
		fontAsset.isMultiAtlasTexturesEnabled = enableMultiAtlasSupport;
		int num;
		if ((renderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16)
		{
			num = 0;
			Material material = new Material(TextShaderUtilities.ShaderRef_MobileBitmap);
			material.SetTexture(TextShaderUtilities.ID_MainTex, texture2D);
			material.SetFloat(TextShaderUtilities.ID_TextureWidth, atlasWidth);
			material.SetFloat(TextShaderUtilities.ID_TextureHeight, atlasHeight);
			fontAsset.material = material;
		}
		else
		{
			num = 1;
			Material material2 = new Material(TextShaderUtilities.ShaderRef_MobileSDF);
			material2.SetTexture(TextShaderUtilities.ID_MainTex, texture2D);
			material2.SetFloat(TextShaderUtilities.ID_TextureWidth, atlasWidth);
			material2.SetFloat(TextShaderUtilities.ID_TextureHeight, atlasHeight);
			material2.SetFloat(TextShaderUtilities.ID_GradientScale, atlasPadding + num);
			material2.SetFloat(TextShaderUtilities.ID_WeightNormal, fontAsset.regularStyleWeight);
			material2.SetFloat(TextShaderUtilities.ID_WeightBold, fontAsset.boldStyleWeight);
			fontAsset.material = material2;
		}
		fontAsset.freeGlyphRects = new List<GlyphRect>(8)
		{
			new GlyphRect(0, 0, atlasWidth - num, atlasHeight - num)
		};
		fontAsset.usedGlyphRects = new List<GlyphRect>(8);
		fontAsset.ReadFontAssetDefinition();
		return fontAsset;
	}

	private void Awake()
	{
	}

	private void OnDestroy()
	{
		DestroyAtlasTextures();
		Object.DestroyImmediate(m_Material);
	}

	public void ReadFontAssetDefinition()
	{
		InitializeDictionaryLookupTables();
		AddSynthesizedCharactersAndFaceMetrics();
		if (m_FaceInfo.capLine == 0f && m_CharacterLookupDictionary.ContainsKey(88u))
		{
			uint glyphIndex = m_CharacterLookupDictionary[88u].glyphIndex;
			m_FaceInfo.capLine = m_GlyphLookupDictionary[glyphIndex].metrics.horizontalBearingY;
		}
		if (m_FaceInfo.meanLine == 0f && m_CharacterLookupDictionary.ContainsKey(120u))
		{
			uint glyphIndex2 = m_CharacterLookupDictionary[120u].glyphIndex;
			m_FaceInfo.meanLine = m_GlyphLookupDictionary[glyphIndex2].metrics.horizontalBearingY;
		}
		if (m_FaceInfo.scale == 0f)
		{
			m_FaceInfo.scale = 1f;
		}
		if (m_FaceInfo.strikethroughOffset == 0f)
		{
			m_FaceInfo.strikethroughOffset = m_FaceInfo.capLine / 2.5f;
		}
		if (m_AtlasPadding == 0 && base.material.HasProperty(TextShaderUtilities.ID_GradientScale))
		{
			m_AtlasPadding = (int)base.material.GetFloat(TextShaderUtilities.ID_GradientScale) - 1;
		}
		base.hashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name);
		familyNameHashCode = TextUtilities.GetHashCodeCaseInSensitive(m_FaceInfo.familyName);
		styleNameHashCode = TextUtilities.GetHashCodeCaseInSensitive(m_FaceInfo.styleName);
		base.materialHashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name + s_DefaultMaterialSuffix);
		TextResourceManager.AddFontAsset(this);
		IsFontAssetLookupTablesDirty = false;
	}

	internal void InitializeDictionaryLookupTables()
	{
		InitializeGlyphLookupDictionary();
		InitializeCharacterLookupDictionary();
		InitializeLigatureSubstitutionLookupDictionary();
		InitializeGlyphPaidAdjustmentRecordsLookupDictionary();
		InitializeMarkToBaseAdjustmentRecordsLookupDictionary();
		InitializeMarkToMarkAdjustmentRecordsLookupDictionary();
	}

	internal void InitializeGlyphLookupDictionary()
	{
		if (m_GlyphLookupDictionary == null)
		{
			m_GlyphLookupDictionary = new Dictionary<uint, Glyph>();
		}
		else
		{
			m_GlyphLookupDictionary.Clear();
		}
		if (m_GlyphIndexList == null)
		{
			m_GlyphIndexList = new List<uint>();
		}
		else
		{
			m_GlyphIndexList.Clear();
		}
		if (m_GlyphIndexListNewlyAdded == null)
		{
			m_GlyphIndexListNewlyAdded = new List<uint>();
		}
		else
		{
			m_GlyphIndexListNewlyAdded.Clear();
		}
		int count = m_GlyphTable.Count;
		for (int i = 0; i < count; i++)
		{
			Glyph glyph = m_GlyphTable[i];
			uint index = glyph.index;
			if (!m_GlyphLookupDictionary.ContainsKey(index))
			{
				m_GlyphLookupDictionary.Add(index, glyph);
				m_GlyphIndexList.Add(index);
			}
		}
	}

	internal void InitializeCharacterLookupDictionary()
	{
		if (m_CharacterLookupDictionary == null)
		{
			m_CharacterLookupDictionary = new Dictionary<uint, Character>();
		}
		else
		{
			m_CharacterLookupDictionary.Clear();
		}
		for (int i = 0; i < m_CharacterTable.Count; i++)
		{
			Character character = m_CharacterTable[i];
			uint unicode = character.unicode;
			uint glyphIndex = character.glyphIndex;
			if (!m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				m_CharacterLookupDictionary.Add(unicode, character);
				character.textAsset = this;
				character.glyph = m_GlyphLookupDictionary[glyphIndex];
			}
		}
	}

	internal void InitializeLigatureSubstitutionLookupDictionary()
	{
		if (m_FontFeatureTable.m_LigatureSubstitutionRecordLookup == null)
		{
			m_FontFeatureTable.m_LigatureSubstitutionRecordLookup = new Dictionary<uint, List<LigatureSubstitutionRecord>>();
		}
		else
		{
			m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.Clear();
		}
		List<LigatureSubstitutionRecord> ligatureSubstitutionRecords = m_FontFeatureTable.m_LigatureSubstitutionRecords;
		if (ligatureSubstitutionRecords == null)
		{
			return;
		}
		for (int i = 0; i < ligatureSubstitutionRecords.Count; i++)
		{
			LigatureSubstitutionRecord item = ligatureSubstitutionRecords[i];
			if (item.componentGlyphIDs != null && item.componentGlyphIDs.Length != 0)
			{
				uint key = item.componentGlyphIDs[0];
				if (!m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.ContainsKey(key))
				{
					m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.Add(key, new List<LigatureSubstitutionRecord> { item });
				}
				else
				{
					m_FontFeatureTable.m_LigatureSubstitutionRecordLookup[key].Add(item);
				}
			}
		}
	}

	internal void InitializeGlyphPaidAdjustmentRecordsLookupDictionary()
	{
		if (m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup == null)
		{
			m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup = new Dictionary<uint, GlyphPairAdjustmentRecord>();
		}
		else
		{
			m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Clear();
		}
		List<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords = m_FontFeatureTable.m_GlyphPairAdjustmentRecords;
		if (glyphPairAdjustmentRecords == null)
		{
			return;
		}
		for (int i = 0; i < glyphPairAdjustmentRecords.Count; i++)
		{
			GlyphPairAdjustmentRecord value = glyphPairAdjustmentRecords[i];
			uint key = (value.secondAdjustmentRecord.glyphIndex << 16) | value.firstAdjustmentRecord.glyphIndex;
			if (!m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(key))
			{
				m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(key, value);
			}
		}
	}

	internal void InitializeMarkToBaseAdjustmentRecordsLookupDictionary()
	{
		if (m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup == null)
		{
			m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup = new Dictionary<uint, MarkToBaseAdjustmentRecord>();
		}
		else
		{
			m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.Clear();
		}
		List<MarkToBaseAdjustmentRecord> markToBaseAdjustmentRecords = m_FontFeatureTable.m_MarkToBaseAdjustmentRecords;
		if (markToBaseAdjustmentRecords == null)
		{
			return;
		}
		for (int i = 0; i < markToBaseAdjustmentRecords.Count; i++)
		{
			MarkToBaseAdjustmentRecord value = markToBaseAdjustmentRecords[i];
			uint key = (value.markGlyphID << 16) | value.baseGlyphID;
			if (!m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.ContainsKey(key))
			{
				m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.Add(key, value);
			}
		}
	}

	internal void InitializeMarkToMarkAdjustmentRecordsLookupDictionary()
	{
		if (m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup == null)
		{
			m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup = new Dictionary<uint, MarkToMarkAdjustmentRecord>();
		}
		else
		{
			m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.Clear();
		}
		List<MarkToMarkAdjustmentRecord> markToMarkAdjustmentRecords = m_FontFeatureTable.m_MarkToMarkAdjustmentRecords;
		if (markToMarkAdjustmentRecords == null)
		{
			return;
		}
		for (int i = 0; i < markToMarkAdjustmentRecords.Count; i++)
		{
			MarkToMarkAdjustmentRecord value = markToMarkAdjustmentRecords[i];
			uint key = (value.combiningMarkGlyphID << 16) | value.baseMarkGlyphID;
			if (!m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.ContainsKey(key))
			{
				m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.Add(key, value);
			}
		}
	}

	internal void AddSynthesizedCharactersAndFaceMetrics()
	{
		bool flag = false;
		if (m_AtlasPopulationMode == AtlasPopulationMode.Dynamic || m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS)
		{
			flag = LoadFontFace() == FontEngineError.Success;
			if (!flag && !InternalDynamicOS)
			{
				Debug.LogWarning("Unable to load font face for [" + base.name + "] font asset.", this);
			}
		}
		AddSynthesizedCharacter(3u, flag, addImmediately: true);
		AddSynthesizedCharacter(9u, flag, addImmediately: true);
		AddSynthesizedCharacter(10u, flag);
		AddSynthesizedCharacter(11u, flag);
		AddSynthesizedCharacter(13u, flag);
		AddSynthesizedCharacter(1564u, flag);
		AddSynthesizedCharacter(8203u, flag);
		AddSynthesizedCharacter(8206u, flag);
		AddSynthesizedCharacter(8207u, flag);
		AddSynthesizedCharacter(8232u, flag);
		AddSynthesizedCharacter(8233u, flag);
		AddSynthesizedCharacter(8288u, flag);
	}

	private void AddSynthesizedCharacter(uint unicode, bool isFontFaceLoaded, bool addImmediately = false)
	{
		if (m_CharacterLookupDictionary.ContainsKey(unicode))
		{
			return;
		}
		if (isFontFaceLoaded && FontEngine.GetGlyphIndex(unicode) != 0)
		{
			if (addImmediately)
			{
				GlyphLoadFlags flags = (((m_AtlasRenderMode & (GlyphRenderMode)4) == (GlyphRenderMode)4) ? (GlyphLoadFlags.LOAD_NO_HINTING | GlyphLoadFlags.LOAD_NO_BITMAP) : GlyphLoadFlags.LOAD_NO_BITMAP);
				if (FontEngine.TryGetGlyphWithUnicodeValue(unicode, flags, out var glyph))
				{
					m_CharacterLookupDictionary.Add(unicode, new Character(unicode, this, glyph));
				}
			}
		}
		else
		{
			Glyph glyph = new Glyph(0u, new GlyphMetrics(0f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
			m_CharacterLookupDictionary.Add(unicode, new Character(unicode, this, glyph));
		}
	}

	internal void AddCharacterToLookupCache(uint unicode, Character character)
	{
		m_CharacterLookupDictionary.Add(unicode, character);
	}

	private FontEngineError LoadFontFace()
	{
		if (m_AtlasPopulationMode == AtlasPopulationMode.Dynamic)
		{
			if (FontEngine.LoadFontFace(m_SourceFontFile, m_FaceInfo.pointSize, m_FaceInfo.faceIndex) == FontEngineError.Success)
			{
				return FontEngineError.Success;
			}
			if (!string.IsNullOrEmpty(m_SourceFontFilePath))
			{
				return FontEngine.LoadFontFace(m_SourceFontFilePath, m_FaceInfo.pointSize, m_FaceInfo.faceIndex);
			}
			return FontEngineError.Invalid_Face;
		}
		return FontEngine.LoadFontFace(m_FaceInfo.familyName, m_FaceInfo.styleName, m_FaceInfo.pointSize);
	}

	internal void SortCharacterTable()
	{
		if (m_CharacterTable != null && m_CharacterTable.Count > 0)
		{
			m_CharacterTable = m_CharacterTable.OrderBy((Character c) => c.unicode).ToList();
		}
	}

	internal void SortGlyphTable()
	{
		if (m_GlyphTable != null && m_GlyphTable.Count > 0)
		{
			m_GlyphTable = m_GlyphTable.OrderBy((Glyph c) => c.index).ToList();
		}
	}

	internal void SortFontFeatureTable()
	{
		m_FontFeatureTable.SortGlyphPairAdjustmentRecords();
		m_FontFeatureTable.SortMarkToBaseAdjustmentRecords();
		m_FontFeatureTable.SortMarkToMarkAdjustmentRecords();
	}

	internal void SortAllTables()
	{
		SortGlyphTable();
		SortCharacterTable();
		SortFontFeatureTable();
	}

	public bool HasCharacter(int character)
	{
		if (m_CharacterLookupDictionary == null)
		{
			return false;
		}
		return m_CharacterLookupDictionary.ContainsKey((uint)character);
	}

	public bool HasCharacter(char character, bool searchFallbacks = false, bool tryAddCharacter = false)
	{
		return HasCharacter((uint)character, searchFallbacks, tryAddCharacter);
	}

	public bool HasCharacter(uint character, bool searchFallbacks = false, bool tryAddCharacter = false)
	{
		if (m_CharacterLookupDictionary == null)
		{
			ReadFontAssetDefinition();
			if (m_CharacterLookupDictionary == null)
			{
				return false;
			}
		}
		if (m_CharacterLookupDictionary.ContainsKey(character))
		{
			return true;
		}
		if (tryAddCharacter && (m_AtlasPopulationMode == AtlasPopulationMode.Dynamic || m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS) && TryAddCharacterInternal(character, out var _))
		{
			return true;
		}
		if (searchFallbacks)
		{
			if (k_SearchedFontAssetLookup == null)
			{
				k_SearchedFontAssetLookup = new HashSet<int>();
			}
			else
			{
				k_SearchedFontAssetLookup.Clear();
			}
			k_SearchedFontAssetLookup.Add(GetInstanceID());
			if (fallbackFontAssetTable != null && fallbackFontAssetTable.Count > 0)
			{
				for (int i = 0; i < fallbackFontAssetTable.Count && fallbackFontAssetTable[i] != null; i++)
				{
					FontAsset fontAsset = fallbackFontAssetTable[i];
					int item = fontAsset.GetInstanceID();
					if (k_SearchedFontAssetLookup.Add(item) && fontAsset.HasCharacter_Internal(character, searchFallbacks: true, tryAddCharacter))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	private bool HasCharacter_Internal(uint character, bool searchFallbacks = false, bool tryAddCharacter = false)
	{
		if (m_CharacterLookupDictionary == null)
		{
			ReadFontAssetDefinition();
			if (m_CharacterLookupDictionary == null)
			{
				return false;
			}
		}
		if (m_CharacterLookupDictionary.ContainsKey(character))
		{
			return true;
		}
		if (tryAddCharacter && (atlasPopulationMode == AtlasPopulationMode.Dynamic || m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS) && TryAddCharacterInternal(character, out var _))
		{
			return true;
		}
		if (searchFallbacks)
		{
			if (fallbackFontAssetTable == null || fallbackFontAssetTable.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < fallbackFontAssetTable.Count && fallbackFontAssetTable[i] != null; i++)
			{
				FontAsset fontAsset = fallbackFontAssetTable[i];
				int item = fontAsset.GetInstanceID();
				if (k_SearchedFontAssetLookup.Add(item) && fontAsset.HasCharacter_Internal(character, searchFallbacks: true, tryAddCharacter))
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool HasCharacters(string text, out List<char> missingCharacters)
	{
		if (m_CharacterLookupDictionary == null)
		{
			missingCharacters = null;
			return false;
		}
		missingCharacters = new List<char>();
		for (int i = 0; i < text.Length; i++)
		{
			if (!m_CharacterLookupDictionary.ContainsKey(text[i]))
			{
				missingCharacters.Add(text[i]);
			}
		}
		if (missingCharacters.Count == 0)
		{
			return true;
		}
		return false;
	}

	public bool HasCharacters(string text, out uint[] missingCharacters, bool searchFallbacks = false, bool tryAddCharacter = false)
	{
		missingCharacters = null;
		if (m_CharacterLookupDictionary == null)
		{
			ReadFontAssetDefinition();
			if (m_CharacterLookupDictionary == null)
			{
				return false;
			}
		}
		s_MissingCharacterList.Clear();
		for (int i = 0; i < text.Length; i++)
		{
			bool flag = true;
			uint num = text[i];
			if (m_CharacterLookupDictionary.ContainsKey(num) || (tryAddCharacter && (atlasPopulationMode == AtlasPopulationMode.Dynamic || m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS) && TryAddCharacterInternal(num, out var _)))
			{
				continue;
			}
			if (searchFallbacks)
			{
				if (k_SearchedFontAssetLookup == null)
				{
					k_SearchedFontAssetLookup = new HashSet<int>();
				}
				else
				{
					k_SearchedFontAssetLookup.Clear();
				}
				k_SearchedFontAssetLookup.Add(GetInstanceID());
				if (fallbackFontAssetTable != null && fallbackFontAssetTable.Count > 0)
				{
					for (int j = 0; j < fallbackFontAssetTable.Count && fallbackFontAssetTable[j] != null; j++)
					{
						FontAsset fontAsset = fallbackFontAssetTable[j];
						int item = fontAsset.GetInstanceID();
						if (k_SearchedFontAssetLookup.Add(item) && fontAsset.HasCharacter_Internal(num, searchFallbacks: true, tryAddCharacter))
						{
							flag = false;
							break;
						}
					}
				}
			}
			if (flag)
			{
				s_MissingCharacterList.Add(num);
			}
		}
		if (s_MissingCharacterList.Count > 0)
		{
			missingCharacters = s_MissingCharacterList.ToArray();
			return false;
		}
		return true;
	}

	public bool HasCharacters(string text)
	{
		if (m_CharacterLookupDictionary == null)
		{
			return false;
		}
		for (int i = 0; i < text.Length; i++)
		{
			if (!m_CharacterLookupDictionary.ContainsKey(text[i]))
			{
				return false;
			}
		}
		return true;
	}

	public static string GetCharacters(FontAsset fontAsset)
	{
		string text = string.Empty;
		for (int i = 0; i < fontAsset.characterTable.Count; i++)
		{
			text += (char)fontAsset.characterTable[i].unicode;
		}
		return text;
	}

	public static int[] GetCharactersArray(FontAsset fontAsset)
	{
		int[] array = new int[fontAsset.characterTable.Count];
		for (int i = 0; i < fontAsset.characterTable.Count; i++)
		{
			array[i] = (int)fontAsset.characterTable[i].unicode;
		}
		return array;
	}

	internal uint GetGlyphIndex(uint unicode)
	{
		if (m_CharacterLookupDictionary.ContainsKey(unicode))
		{
			return m_CharacterLookupDictionary[unicode].glyphIndex;
		}
		return (LoadFontFace() == FontEngineError.Success) ? FontEngine.GetGlyphIndex(unicode) : 0u;
	}

	internal static void RegisterFontAssetForFontFeatureUpdate(FontAsset fontAsset)
	{
		int item = fontAsset.instanceID;
		if (k_FontAssets_FontFeaturesUpdateQueueLookup.Add(item))
		{
			k_FontAssets_FontFeaturesUpdateQueue.Add(fontAsset);
		}
	}

	internal static void UpdateFontFeaturesForFontAssetsInQueue()
	{
		int count = k_FontAssets_FontFeaturesUpdateQueue.Count;
		for (int i = 0; i < count; i++)
		{
			k_FontAssets_FontFeaturesUpdateQueue[i].UpdateAllFontFeatures();
		}
		if (count > 0)
		{
			k_FontAssets_FontFeaturesUpdateQueue.Clear();
			k_FontAssets_FontFeaturesUpdateQueueLookup.Clear();
		}
	}

	internal static void RegisterAtlasTextureForApply(Texture2D texture)
	{
		int item = texture.GetInstanceID();
		if (k_FontAssets_AtlasTexturesUpdateQueueLookup.Add(item))
		{
			k_FontAssets_AtlasTexturesUpdateQueue.Add(texture);
		}
	}

	internal static void UpdateAtlasTexturesInQueue()
	{
		int count = k_FontAssets_AtlasTexturesUpdateQueueLookup.Count;
		for (int i = 0; i < count; i++)
		{
			k_FontAssets_AtlasTexturesUpdateQueue[i].Apply(updateMipmaps: false, makeNoLongerReadable: false);
		}
		if (count > 0)
		{
			k_FontAssets_AtlasTexturesUpdateQueue.Clear();
			k_FontAssets_AtlasTexturesUpdateQueueLookup.Clear();
		}
	}

	internal static void UpdateFontAssetsInUpdateQueue()
	{
		UpdateAtlasTexturesInQueue();
		UpdateFontFeaturesForFontAssetsInQueue();
	}

	public bool TryAddCharacters(uint[] unicodes, bool includeFontFeatures = false)
	{
		uint[] missingUnicodes;
		return TryAddCharacters(unicodes, out missingUnicodes, includeFontFeatures);
	}

	public bool TryAddCharacters(uint[] unicodes, out uint[] missingUnicodes, bool includeFontFeatures = false)
	{
		if (unicodes == null || unicodes.Length == 0 || m_AtlasPopulationMode == AtlasPopulationMode.Static)
		{
			if (m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
			}
			else
			{
				Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided Unicode list is Null or Empty.", this);
			}
			missingUnicodes = null;
			return false;
		}
		if (LoadFontFace() != FontEngineError.Success)
		{
			missingUnicodes = unicodes.ToArray();
			return false;
		}
		if (m_CharacterLookupDictionary == null || m_GlyphLookupDictionary == null)
		{
			ReadFontAssetDefinition();
		}
		m_GlyphsToAdd.Clear();
		m_GlyphsToAddLookup.Clear();
		m_CharactersToAdd.Clear();
		m_CharactersToAddLookup.Clear();
		s_MissingCharacterList.Clear();
		bool flag = false;
		int num = unicodes.Length;
		for (int i = 0; i < num; i++)
		{
			uint num2 = unicodes[i];
			if (m_CharacterLookupDictionary.ContainsKey(num2))
			{
				continue;
			}
			uint glyphIndex = FontEngine.GetGlyphIndex(num2);
			if (glyphIndex == 0)
			{
				switch (num2)
				{
				case 160u:
					glyphIndex = FontEngine.GetGlyphIndex(32u);
					break;
				case 173u:
				case 8209u:
					glyphIndex = FontEngine.GetGlyphIndex(45u);
					break;
				}
				if (glyphIndex == 0)
				{
					s_MissingCharacterList.Add(num2);
					flag = true;
					continue;
				}
			}
			Character character = new Character(num2, glyphIndex);
			if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				character.glyph = m_GlyphLookupDictionary[glyphIndex];
				character.textAsset = this;
				m_CharacterTable.Add(character);
				m_CharacterLookupDictionary.Add(num2, character);
				continue;
			}
			if (m_GlyphsToAddLookup.Add(glyphIndex))
			{
				m_GlyphsToAdd.Add(glyphIndex);
			}
			if (m_CharactersToAddLookup.Add(num2))
			{
				m_CharactersToAdd.Add(character);
			}
		}
		if (m_GlyphsToAdd.Count == 0)
		{
			missingUnicodes = unicodes;
			return false;
		}
		if (m_AtlasTextures[m_AtlasTextureIndex].width != m_AtlasWidth || m_AtlasTextures[m_AtlasTextureIndex].height != m_AtlasHeight)
		{
			m_AtlasTextures[m_AtlasTextureIndex].Reinitialize(m_AtlasWidth, m_AtlasHeight);
			FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
		}
		Glyph[] glyphs;
		bool flag2 = FontEngine.TryAddGlyphsToTexture(m_GlyphsToAdd, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyphs);
		for (int j = 0; j < glyphs.Length && glyphs[j] != null; j++)
		{
			Glyph glyph = glyphs[j];
			uint index = glyph.index;
			glyph.atlasIndex = m_AtlasTextureIndex;
			m_GlyphTable.Add(glyph);
			m_GlyphLookupDictionary.Add(index, glyph);
			m_GlyphIndexListNewlyAdded.Add(index);
			m_GlyphIndexList.Add(index);
		}
		m_GlyphsToAdd.Clear();
		for (int k = 0; k < m_CharactersToAdd.Count; k++)
		{
			Character character2 = m_CharactersToAdd[k];
			if (!m_GlyphLookupDictionary.TryGetValue(character2.glyphIndex, out var value))
			{
				m_GlyphsToAdd.Add(character2.glyphIndex);
				continue;
			}
			character2.glyph = value;
			character2.textAsset = this;
			m_CharacterTable.Add(character2);
			m_CharacterLookupDictionary.Add(character2.unicode, character2);
			m_CharactersToAdd.RemoveAt(k);
			k--;
		}
		if (m_IsMultiAtlasTexturesEnabled && !flag2)
		{
			while (!flag2)
			{
				flag2 = TryAddGlyphsToNewAtlasTexture();
			}
		}
		if (includeFontFeatures)
		{
			UpdateAllFontFeatures();
		}
		for (int l = 0; l < m_CharactersToAdd.Count; l++)
		{
			Character character3 = m_CharactersToAdd[l];
			s_MissingCharacterList.Add(character3.unicode);
		}
		missingUnicodes = null;
		if (s_MissingCharacterList.Count > 0)
		{
			missingUnicodes = s_MissingCharacterList.ToArray();
		}
		return flag2 && !flag;
	}

	public bool TryAddCharacters(string characters, bool includeFontFeatures = false)
	{
		string missingCharacters;
		return TryAddCharacters(characters, out missingCharacters, includeFontFeatures);
	}

	public bool TryAddCharacters(string characters, out string missingCharacters, bool includeFontFeatures = false)
	{
		if (string.IsNullOrEmpty(characters) || m_AtlasPopulationMode == AtlasPopulationMode.Static)
		{
			if (m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
			}
			else
			{
				Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided character list is Null or Empty.", this);
			}
			missingCharacters = characters;
			return false;
		}
		if (LoadFontFace() != FontEngineError.Success)
		{
			missingCharacters = characters;
			return false;
		}
		if (m_CharacterLookupDictionary == null || m_GlyphLookupDictionary == null)
		{
			ReadFontAssetDefinition();
		}
		m_GlyphsToAdd.Clear();
		m_GlyphsToAddLookup.Clear();
		m_CharactersToAdd.Clear();
		m_CharactersToAddLookup.Clear();
		s_MissingCharacterList.Clear();
		bool flag = false;
		int length = characters.Length;
		for (int i = 0; i < length; i++)
		{
			uint num = characters[i];
			if (m_CharacterLookupDictionary.ContainsKey(num))
			{
				continue;
			}
			uint glyphIndex = FontEngine.GetGlyphIndex(num);
			if (glyphIndex == 0)
			{
				switch (num)
				{
				case 160u:
					glyphIndex = FontEngine.GetGlyphIndex(32u);
					break;
				case 173u:
				case 8209u:
					glyphIndex = FontEngine.GetGlyphIndex(45u);
					break;
				}
				if (glyphIndex == 0)
				{
					s_MissingCharacterList.Add(num);
					flag = true;
					continue;
				}
			}
			Character character = new Character(num, glyphIndex);
			if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				character.glyph = m_GlyphLookupDictionary[glyphIndex];
				character.textAsset = this;
				m_CharacterTable.Add(character);
				m_CharacterLookupDictionary.Add(num, character);
				continue;
			}
			if (m_GlyphsToAddLookup.Add(glyphIndex))
			{
				m_GlyphsToAdd.Add(glyphIndex);
			}
			if (m_CharactersToAddLookup.Add(num))
			{
				m_CharactersToAdd.Add(character);
			}
		}
		if (m_GlyphsToAdd.Count == 0)
		{
			missingCharacters = characters;
			return false;
		}
		if (m_AtlasTextures[m_AtlasTextureIndex].width != m_AtlasWidth || m_AtlasTextures[m_AtlasTextureIndex].height != m_AtlasHeight)
		{
			m_AtlasTextures[m_AtlasTextureIndex].Reinitialize(m_AtlasWidth, m_AtlasHeight);
			FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
		}
		Glyph[] glyphs;
		bool flag2 = FontEngine.TryAddGlyphsToTexture(m_GlyphsToAdd, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyphs);
		for (int j = 0; j < glyphs.Length && glyphs[j] != null; j++)
		{
			Glyph glyph = glyphs[j];
			uint index = glyph.index;
			glyph.atlasIndex = m_AtlasTextureIndex;
			m_GlyphTable.Add(glyph);
			m_GlyphLookupDictionary.Add(index, glyph);
			m_GlyphIndexListNewlyAdded.Add(index);
			m_GlyphIndexList.Add(index);
		}
		m_GlyphsToAdd.Clear();
		for (int k = 0; k < m_CharactersToAdd.Count; k++)
		{
			Character character2 = m_CharactersToAdd[k];
			if (!m_GlyphLookupDictionary.TryGetValue(character2.glyphIndex, out var value))
			{
				m_GlyphsToAdd.Add(character2.glyphIndex);
				continue;
			}
			character2.glyph = value;
			character2.textAsset = this;
			m_CharacterTable.Add(character2);
			m_CharacterLookupDictionary.Add(character2.unicode, character2);
			m_CharactersToAdd.RemoveAt(k);
			k--;
		}
		if (m_IsMultiAtlasTexturesEnabled && !flag2)
		{
			while (!flag2)
			{
				flag2 = TryAddGlyphsToNewAtlasTexture();
			}
		}
		if (includeFontFeatures)
		{
			UpdateAllFontFeatures();
		}
		missingCharacters = string.Empty;
		for (int l = 0; l < m_CharactersToAdd.Count; l++)
		{
			Character character3 = m_CharactersToAdd[l];
			s_MissingCharacterList.Add(character3.unicode);
		}
		if (s_MissingCharacterList.Count > 0)
		{
			missingCharacters = s_MissingCharacterList.UintToString();
		}
		return flag2 && !flag;
	}

	internal bool TryAddGlyphInternal(uint glyphIndex, out Glyph glyph)
	{
		using (k_TryAddGlyphMarker.Auto())
		{
			glyph = null;
			if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				glyph = m_GlyphLookupDictionary[glyphIndex];
				return true;
			}
			if (LoadFontFace() != FontEngineError.Success)
			{
				return false;
			}
			if (!m_AtlasTextures[m_AtlasTextureIndex].isReadable)
			{
				Debug.LogWarning("Unable to add the requested glyph to font asset [" + base.name + "]'s atlas texture. Please make the texture [" + m_AtlasTextures[m_AtlasTextureIndex].name + "] readable.", m_AtlasTextures[m_AtlasTextureIndex]);
				return false;
			}
			if (m_AtlasTextures[m_AtlasTextureIndex].width != m_AtlasWidth || m_AtlasTextures[m_AtlasTextureIndex].height != m_AtlasHeight)
			{
				m_AtlasTextures[m_AtlasTextureIndex].Reinitialize(m_AtlasWidth, m_AtlasHeight);
				FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
			}
			FontEngine.SetTextureUploadMode(shouldUploadImmediately: false);
			if (FontEngine.TryAddGlyphToTexture(glyphIndex, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyph))
			{
				glyph.atlasIndex = m_AtlasTextureIndex;
				m_GlyphTable.Add(glyph);
				m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				m_GlyphIndexList.Add(glyphIndex);
				m_GlyphIndexListNewlyAdded.Add(glyphIndex);
				return true;
			}
			if (m_IsMultiAtlasTexturesEnabled)
			{
				SetupNewAtlasTexture();
				if (FontEngine.TryAddGlyphToTexture(glyphIndex, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyph))
				{
					glyph.atlasIndex = m_AtlasTextureIndex;
					m_GlyphTable.Add(glyph);
					m_GlyphLookupDictionary.Add(glyphIndex, glyph);
					m_GlyphIndexList.Add(glyphIndex);
					m_GlyphIndexListNewlyAdded.Add(glyphIndex);
					return true;
				}
			}
		}
		return false;
	}

	internal bool TryAddCharacterInternal(uint unicode, out Character character, bool shouldGetFontFeatures = false)
	{
		character = null;
		if (m_MissingUnicodesFromFontFile.Contains(unicode))
		{
			return false;
		}
		if (LoadFontFace() != FontEngineError.Success)
		{
			return false;
		}
		uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
		if (glyphIndex == 0)
		{
			switch (unicode)
			{
			case 160u:
				glyphIndex = FontEngine.GetGlyphIndex(32u);
				break;
			case 173u:
			case 8209u:
				glyphIndex = FontEngine.GetGlyphIndex(45u);
				break;
			}
			if (glyphIndex == 0)
			{
				m_MissingUnicodesFromFontFile.Add(unicode);
				return false;
			}
		}
		if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
		{
			character = new Character(unicode, this, m_GlyphLookupDictionary[glyphIndex]);
			m_CharacterTable.Add(character);
			m_CharacterLookupDictionary.Add(unicode, character);
			return true;
		}
		Glyph glyph = null;
		if (!m_AtlasTextures[m_AtlasTextureIndex].isReadable)
		{
			Debug.LogWarning("Unable to add the requested character to font asset [" + base.name + "]'s atlas texture. Please make the texture [" + m_AtlasTextures[m_AtlasTextureIndex].name + "] readable.", m_AtlasTextures[m_AtlasTextureIndex]);
			return false;
		}
		if (m_AtlasTextures[m_AtlasTextureIndex].width != m_AtlasWidth || m_AtlasTextures[m_AtlasTextureIndex].height != m_AtlasHeight)
		{
			m_AtlasTextures[m_AtlasTextureIndex].Reinitialize(m_AtlasWidth, m_AtlasHeight);
			FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
		}
		FontEngine.SetTextureUploadMode(shouldUploadImmediately: false);
		if (FontEngine.TryAddGlyphToTexture(glyphIndex, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyph))
		{
			glyph.atlasIndex = m_AtlasTextureIndex;
			m_GlyphTable.Add(glyph);
			m_GlyphLookupDictionary.Add(glyphIndex, glyph);
			character = new Character(unicode, this, glyph);
			m_CharacterTable.Add(character);
			m_CharacterLookupDictionary.Add(unicode, character);
			m_GlyphIndexList.Add(glyphIndex);
			m_GlyphIndexListNewlyAdded.Add(glyphIndex);
			if (shouldGetFontFeatures)
			{
				RegisterFontAssetForFontFeatureUpdate(this);
			}
			RegisterAtlasTextureForApply(m_AtlasTextures[m_AtlasTextureIndex]);
			FontEngine.SetTextureUploadMode(shouldUploadImmediately: true);
			return true;
		}
		if (m_IsMultiAtlasTexturesEnabled)
		{
			SetupNewAtlasTexture();
			if (FontEngine.TryAddGlyphToTexture(glyphIndex, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyph))
			{
				glyph.atlasIndex = m_AtlasTextureIndex;
				m_GlyphTable.Add(glyph);
				m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				character = new Character(unicode, this, glyph);
				m_CharacterTable.Add(character);
				m_CharacterLookupDictionary.Add(unicode, character);
				m_GlyphIndexList.Add(glyphIndex);
				m_GlyphIndexListNewlyAdded.Add(glyphIndex);
				if (shouldGetFontFeatures)
				{
					RegisterFontAssetForFontFeatureUpdate(this);
				}
				RegisterAtlasTextureForApply(m_AtlasTextures[m_AtlasTextureIndex]);
				FontEngine.SetTextureUploadMode(shouldUploadImmediately: true);
				return true;
			}
		}
		return false;
	}

	internal bool TryGetCharacter_and_QueueRenderToTexture(uint unicode, out Character character, bool shouldGetFontFeatures = false)
	{
		character = null;
		if (m_MissingUnicodesFromFontFile.Contains(unicode))
		{
			return false;
		}
		if (LoadFontFace() != FontEngineError.Success)
		{
			return false;
		}
		uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
		if (glyphIndex == 0)
		{
			switch (unicode)
			{
			case 160u:
				glyphIndex = FontEngine.GetGlyphIndex(32u);
				break;
			case 173u:
			case 8209u:
				glyphIndex = FontEngine.GetGlyphIndex(45u);
				break;
			}
			if (glyphIndex == 0)
			{
				m_MissingUnicodesFromFontFile.Add(unicode);
				return false;
			}
		}
		if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
		{
			character = new Character(unicode, this, m_GlyphLookupDictionary[glyphIndex]);
			m_CharacterTable.Add(character);
			m_CharacterLookupDictionary.Add(unicode, character);
			return true;
		}
		GlyphLoadFlags flags = ((((GlyphRenderMode)4 & m_AtlasRenderMode) == (GlyphRenderMode)4) ? (GlyphLoadFlags.LOAD_NO_HINTING | GlyphLoadFlags.LOAD_NO_BITMAP) : GlyphLoadFlags.LOAD_NO_BITMAP);
		Glyph glyph = null;
		if (FontEngine.TryGetGlyphWithIndexValue(glyphIndex, flags, out glyph))
		{
			m_GlyphTable.Add(glyph);
			m_GlyphLookupDictionary.Add(glyphIndex, glyph);
			character = new Character(unicode, this, glyph);
			m_CharacterTable.Add(character);
			m_CharacterLookupDictionary.Add(unicode, character);
			m_GlyphIndexList.Add(glyphIndex);
			m_GlyphIndexListNewlyAdded.Add(glyphIndex);
			if (shouldGetFontFeatures)
			{
				RegisterFontAssetForFontFeatureUpdate(this);
			}
			m_GlyphsToRender.Add(glyph);
			return true;
		}
		return false;
	}

	internal void TryAddGlyphsToAtlasTextures()
	{
	}

	private bool TryAddGlyphsToNewAtlasTexture()
	{
		SetupNewAtlasTexture();
		Glyph[] glyphs;
		bool result = FontEngine.TryAddGlyphsToTexture(m_GlyphsToAdd, m_AtlasPadding, GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyphs);
		for (int i = 0; i < glyphs.Length && glyphs[i] != null; i++)
		{
			Glyph glyph = glyphs[i];
			uint index = glyph.index;
			glyph.atlasIndex = m_AtlasTextureIndex;
			m_GlyphTable.Add(glyph);
			m_GlyphLookupDictionary.Add(index, glyph);
			m_GlyphIndexListNewlyAdded.Add(index);
			m_GlyphIndexList.Add(index);
		}
		m_GlyphsToAdd.Clear();
		for (int j = 0; j < m_CharactersToAdd.Count; j++)
		{
			Character character = m_CharactersToAdd[j];
			if (!m_GlyphLookupDictionary.TryGetValue(character.glyphIndex, out var value))
			{
				m_GlyphsToAdd.Add(character.glyphIndex);
				continue;
			}
			character.glyph = value;
			character.textAsset = this;
			m_CharacterTable.Add(character);
			m_CharacterLookupDictionary.Add(character.unicode, character);
			m_CharactersToAdd.RemoveAt(j);
			j--;
		}
		return result;
	}

	private void SetupNewAtlasTexture()
	{
		m_AtlasTextureIndex++;
		if (m_AtlasTextures.Length == m_AtlasTextureIndex)
		{
			Array.Resize(ref m_AtlasTextures, m_AtlasTextures.Length * 2);
		}
		m_AtlasTextures[m_AtlasTextureIndex] = new Texture2D(m_AtlasWidth, m_AtlasHeight, TextureFormat.Alpha8, mipChain: false);
		FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
		int num = (((m_AtlasRenderMode & (GlyphRenderMode)16) != (GlyphRenderMode)16) ? 1 : 0);
		m_FreeGlyphRects.Clear();
		m_FreeGlyphRects.Add(new GlyphRect(0, 0, m_AtlasWidth - num, m_AtlasHeight - num));
		m_UsedGlyphRects.Clear();
	}

	private void UpdateAllFontFeatures()
	{
		UpdateGlyphAdjustmentRecords();
		m_GlyphIndexListNewlyAdded.Clear();
	}

	internal void UpdateGlyphAdjustmentRecords()
	{
		using (k_UpdateGlyphAdjustmentRecordsMarker.Auto())
		{
			int recordCount;
			GlyphPairAdjustmentRecord[] glyphPairAdjustmentRecords = FontEngine.GetGlyphPairAdjustmentRecords(m_GlyphIndexList, out recordCount);
			m_GlyphIndexListNewlyAdded.Clear();
			if (glyphPairAdjustmentRecords == null || glyphPairAdjustmentRecords.Length == 0)
			{
				return;
			}
			if (m_FontFeatureTable == null)
			{
				m_FontFeatureTable = new FontFeatureTable();
			}
			for (int i = 0; i < glyphPairAdjustmentRecords.Length && glyphPairAdjustmentRecords[i].firstAdjustmentRecord.glyphIndex != 0; i++)
			{
				uint key = (glyphPairAdjustmentRecords[i].secondAdjustmentRecord.glyphIndex << 16) | glyphPairAdjustmentRecords[i].firstAdjustmentRecord.glyphIndex;
				if (!m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(key))
				{
					GlyphPairAdjustmentRecord glyphPairAdjustmentRecord = glyphPairAdjustmentRecords[i];
					m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(glyphPairAdjustmentRecord);
					m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(key, glyphPairAdjustmentRecord);
				}
			}
		}
	}

	internal void UpdateGlyphAdjustmentRecords(uint[] glyphIndexes)
	{
		using (k_UpdateGlyphAdjustmentRecordsMarker.Auto())
		{
			GlyphPairAdjustmentRecord[] glyphPairAdjustmentTable = FontEngine.GetGlyphPairAdjustmentTable(glyphIndexes);
			if (glyphPairAdjustmentTable == null || glyphPairAdjustmentTable.Length == 0)
			{
				return;
			}
			if (m_FontFeatureTable == null)
			{
				m_FontFeatureTable = new FontFeatureTable();
			}
			for (int i = 0; i < glyphPairAdjustmentTable.Length && glyphPairAdjustmentTable[i].firstAdjustmentRecord.glyphIndex != 0; i++)
			{
				uint key = (glyphPairAdjustmentTable[i].secondAdjustmentRecord.glyphIndex << 16) | glyphPairAdjustmentTable[i].firstAdjustmentRecord.glyphIndex;
				if (!m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(key))
				{
					GlyphPairAdjustmentRecord glyphPairAdjustmentRecord = glyphPairAdjustmentTable[i];
					m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(glyphPairAdjustmentRecord);
					m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(key, glyphPairAdjustmentRecord);
				}
			}
		}
	}

	internal void UpdateGlyphAdjustmentRecords(List<uint> glyphIndexes)
	{
	}

	internal void UpdateGlyphAdjustmentRecords(List<uint> newGlyphIndexes, List<uint> allGlyphIndexes)
	{
	}

	private void CopyListDataToArray<T>(List<T> srcList, ref T[] dstArray)
	{
		int count = srcList.Count;
		if (dstArray == null)
		{
			dstArray = new T[count];
		}
		else
		{
			Array.Resize(ref dstArray, count);
		}
		for (int i = 0; i < count; i++)
		{
			dstArray[i] = srcList[i];
		}
	}

	public void ClearFontAssetData(bool setAtlasSizeToZero = false)
	{
		using (k_ClearFontAssetDataMarker.Auto())
		{
			ClearFontAssetTables(clearFontFeatures: true);
			ClearAtlasTextures(setAtlasSizeToZero);
			ReadFontAssetDefinition();
		}
	}

	internal void ClearFontAssetDataInternal(bool clearFontFeatures = false)
	{
		ClearFontAssetTables(clearFontFeatures);
		ClearAtlasTextures(setAtlasSizeToZero: true);
	}

	internal void UpdateFontAssetData()
	{
		using (k_UpdateFontAssetDataMarker.Auto())
		{
			uint[] array = new uint[m_CharacterTable.Count];
			for (int i = 0; i < m_CharacterTable.Count; i++)
			{
				array[i] = m_CharacterTable[i].unicode;
			}
			ClearFontAssetTables(clearFontFeatures: true);
			ClearAtlasTextures(setAtlasSizeToZero: true);
			ReadFontAssetDefinition();
			if (array.Length != 0)
			{
				TryAddCharacters(array, includeFontFeatures: true);
			}
		}
	}

	internal void ClearFontAssetTables(bool clearFontFeatures)
	{
		if (m_GlyphTable != null)
		{
			m_GlyphTable.Clear();
		}
		if (m_CharacterTable != null)
		{
			m_CharacterTable.Clear();
		}
		if (m_UsedGlyphRects != null)
		{
			m_UsedGlyphRects.Clear();
		}
		if (m_FreeGlyphRects != null)
		{
			int num = (((m_AtlasRenderMode & (GlyphRenderMode)16) != (GlyphRenderMode)16) ? 1 : 0);
			m_FreeGlyphRects.Clear();
			m_FreeGlyphRects.Add(new GlyphRect(0, 0, m_AtlasWidth - num, m_AtlasHeight - num));
		}
		if (m_GlyphsToRender != null)
		{
			m_GlyphsToRender.Clear();
		}
		if (m_GlyphsRendered != null)
		{
			m_GlyphsRendered.Clear();
		}
		if (clearFontFeatures)
		{
			if (m_FontFeatureTable != null && m_FontFeatureTable.m_LigatureSubstitutionRecords != null)
			{
				m_FontFeatureTable.m_LigatureSubstitutionRecords.Clear();
			}
			if (m_FontFeatureTable != null && m_FontFeatureTable.m_GlyphPairAdjustmentRecords != null)
			{
				m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Clear();
			}
			if (m_FontFeatureTable != null && m_FontFeatureTable.m_MarkToBaseAdjustmentRecords != null)
			{
				m_FontFeatureTable.m_MarkToBaseAdjustmentRecords.Clear();
			}
			if (m_FontFeatureTable != null && m_FontFeatureTable.m_MarkToMarkAdjustmentRecords != null)
			{
				m_FontFeatureTable.m_MarkToMarkAdjustmentRecords.Clear();
			}
		}
	}

	internal void ClearAtlasTextures(bool setAtlasSizeToZero = false)
	{
		m_AtlasTextureIndex = 0;
		if (m_AtlasTextures == null)
		{
			return;
		}
		Texture2D texture2D = null;
		for (int i = 1; i < m_AtlasTextures.Length; i++)
		{
			texture2D = m_AtlasTextures[i];
			if (!(texture2D == null))
			{
				Object.DestroyImmediate(texture2D, allowDestroyingAssets: true);
			}
		}
		Array.Resize(ref m_AtlasTextures, 1);
		texture2D = (m_AtlasTexture = m_AtlasTextures[0]);
		if (!texture2D.isReadable)
		{
		}
		if (setAtlasSizeToZero)
		{
			texture2D.Reinitialize(1, 1, TextureFormat.Alpha8, hasMipMap: false);
		}
		else if (texture2D.width != m_AtlasWidth || texture2D.height != m_AtlasHeight)
		{
			texture2D.Reinitialize(m_AtlasWidth, m_AtlasHeight, TextureFormat.Alpha8, hasMipMap: false);
		}
		FontEngine.ResetAtlasTexture(texture2D);
		texture2D.Apply();
	}

	private void DestroyAtlasTextures()
	{
		if (m_AtlasTextures == null)
		{
			return;
		}
		for (int i = 0; i < m_AtlasTextures.Length; i++)
		{
			Texture2D texture2D = m_AtlasTextures[i];
			if (texture2D != null)
			{
				Object.DestroyImmediate(texture2D);
			}
		}
	}
}
internal static class FontAssetUtilities
{
	private static HashSet<int> k_SearchedAssets;

	internal static Character GetCharacterFromFontAsset(uint unicode, FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface)
	{
		if (includeFallbacks)
		{
			if (k_SearchedAssets == null)
			{
				k_SearchedAssets = new HashSet<int>();
			}
			else
			{
				k_SearchedAssets.Clear();
			}
		}
		return GetCharacterFromFontAsset_Internal(unicode, sourceFontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface);
	}

	private static Character GetCharacterFromFontAsset_Internal(uint unicode, FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface)
	{
		isAlternativeTypeface = false;
		Character value = null;
		bool flag = (fontStyle & FontStyles.Italic) == FontStyles.Italic;
		if (flag || fontWeight != TextFontWeight.Regular)
		{
			FontWeightPair[] fontWeightTable = sourceFontAsset.fontWeightTable;
			int num = 4;
			switch (fontWeight)
			{
			case TextFontWeight.Thin:
				num = 1;
				break;
			case TextFontWeight.ExtraLight:
				num = 2;
				break;
			case TextFontWeight.Light:
				num = 3;
				break;
			case TextFontWeight.Regular:
				num = 4;
				break;
			case TextFontWeight.Medium:
				num = 5;
				break;
			case TextFontWeight.SemiBold:
				num = 6;
				break;
			case TextFontWeight.Bold:
				num = 7;
				break;
			case TextFontWeight.Heavy:
				num = 8;
				break;
			case TextFontWeight.Black:
				num = 9;
				break;
			}
			FontAsset fontAsset = (flag ? fontWeightTable[num].italicTypeface : fontWeightTable[num].regularTypeface);
			if (fontAsset != null)
			{
				if (fontAsset.characterLookupTable.TryGetValue(unicode, out value))
				{
					if (value.textAsset != null)
					{
						isAlternativeTypeface = true;
						return value;
					}
					fontAsset.characterLookupTable.Remove(unicode);
				}
				if ((fontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic || fontAsset.atlasPopulationMode == AtlasPopulationMode.DynamicOS) && fontAsset.TryAddCharacterInternal(unicode, out value))
				{
					isAlternativeTypeface = true;
					return value;
				}
			}
		}
		if (sourceFontAsset.characterLookupTable.TryGetValue(unicode, out value))
		{
			if (value.textAsset != null)
			{
				return value;
			}
			sourceFontAsset.characterLookupTable.Remove(unicode);
		}
		if ((sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic || sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.DynamicOS) && sourceFontAsset.TryAddCharacterInternal(unicode, out value))
		{
			return value;
		}
		if (value == null && includeFallbacks && sourceFontAsset.fallbackFontAssetTable != null)
		{
			List<FontAsset> fallbackFontAssetTable = sourceFontAsset.fallbackFontAssetTable;
			int count = fallbackFontAssetTable.Count;
			if (count == 0)
			{
				return null;
			}
			for (int i = 0; i < count; i++)
			{
				FontAsset fontAsset2 = fallbackFontAssetTable[i];
				if (fontAsset2 == null)
				{
					continue;
				}
				int instanceID = fontAsset2.instanceID;
				if (k_SearchedAssets.Add(instanceID))
				{
					value = GetCharacterFromFontAsset_Internal(unicode, fontAsset2, includeFallbacks: true, fontStyle, fontWeight, out isAlternativeTypeface);
					if (value != null)
					{
						return value;
					}
				}
			}
		}
		return null;
	}

	public static Character GetCharacterFromFontAssets(uint unicode, FontAsset sourceFontAsset, List<FontAsset> fontAssets, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface)
	{
		isAlternativeTypeface = false;
		if (fontAssets == null || fontAssets.Count == 0)
		{
			return null;
		}
		if (includeFallbacks)
		{
			if (k_SearchedAssets == null)
			{
				k_SearchedAssets = new HashSet<int>();
			}
			else
			{
				k_SearchedAssets.Clear();
			}
		}
		int count = fontAssets.Count;
		for (int i = 0; i < count; i++)
		{
			FontAsset fontAsset = fontAssets[i];
			if (!(fontAsset == null))
			{
				Character characterFromFontAsset_Internal = GetCharacterFromFontAsset_Internal(unicode, fontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface);
				if (characterFromFontAsset_Internal != null)
				{
					return characterFromFontAsset_Internal;
				}
			}
		}
		return null;
	}

	public static SpriteCharacter GetSpriteCharacterFromSpriteAsset(uint unicode, SpriteAsset spriteAsset, bool includeFallbacks)
	{
		if (spriteAsset == null)
		{
			return null;
		}
		if (spriteAsset.spriteCharacterLookupTable.TryGetValue(unicode, out var value))
		{
			return value;
		}
		if (includeFallbacks)
		{
			if (k_SearchedAssets == null)
			{
				k_SearchedAssets = new HashSet<int>();
			}
			else
			{
				k_SearchedAssets.Clear();
			}
			k_SearchedAssets.Add(spriteAsset.instanceID);
			List<SpriteAsset> fallbackSpriteAssets = spriteAsset.fallbackSpriteAssets;
			if (fallbackSpriteAssets != null && fallbackSpriteAssets.Count > 0)
			{
				int count = fallbackSpriteAssets.Count;
				for (int i = 0; i < count; i++)
				{
					SpriteAsset spriteAsset2 = fallbackSpriteAssets[i];
					if (spriteAsset2 == null)
					{
						continue;
					}
					int instanceID = spriteAsset2.instanceID;
					if (k_SearchedAssets.Add(instanceID))
					{
						value = GetSpriteCharacterFromSpriteAsset_Internal(unicode, spriteAsset2, includeFallbacks: true);
						if (value != null)
						{
							return value;
						}
					}
				}
			}
		}
		return null;
	}

	private static SpriteCharacter GetSpriteCharacterFromSpriteAsset_Internal(uint unicode, SpriteAsset spriteAsset, bool includeFallbacks)
	{
		if (spriteAsset.spriteCharacterLookupTable.TryGetValue(unicode, out var value))
		{
			return value;
		}
		if (includeFallbacks)
		{
			List<SpriteAsset> fallbackSpriteAssets = spriteAsset.fallbackSpriteAssets;
			if (fallbackSpriteAssets != null && fallbackSpriteAssets.Count > 0)
			{
				int count = fallbackSpriteAssets.Count;
				for (int i = 0; i < count; i++)
				{
					SpriteAsset spriteAsset2 = fallbackSpriteAssets[i];
					if (spriteAsset2 == null)
					{
						continue;
					}
					int instanceID = spriteAsset2.instanceID;
					if (k_SearchedAssets.Add(instanceID))
					{
						value = GetSpriteCharacterFromSpriteAsset_Internal(unicode, spriteAsset2, includeFallbacks: true);
						if (value != null)
						{
							return value;
						}
					}
				}
			}
		}
		return null;
	}
}
[Serializable]
public class FontFeatureTable
{
	[SerializeField]
	internal List<MultipleSubstitutionRecord> m_MultipleSubstitutionRecords;

	[SerializeField]
	internal List<LigatureSubstitutionRecord> m_LigatureSubstitutionRecords;

	[SerializeField]
	internal List<GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecords;

	[SerializeField]
	internal List<MarkToBaseAdjustmentRecord> m_MarkToBaseAdjustmentRecords;

	[SerializeField]
	internal List<MarkToMarkAdjustmentRecord> m_MarkToMarkAdjustmentRecords;

	internal Dictionary<uint, List<LigatureSubstitutionRecord>> m_LigatureSubstitutionRecordLookup;

	internal Dictionary<uint, GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecordLookup;

	internal Dictionary<uint, MarkToBaseAdjustmentRecord> m_MarkToBaseAdjustmentRecordLookup;

	internal Dictionary<uint, MarkToMarkAdjustmentRecord> m_MarkToMarkAdjustmentRecordLookup;

	internal List<MultipleSubstitutionRecord> multipleSubstitutionRecords
	{
		get
		{
			return m_MultipleSubstitutionRecords;
		}
		set
		{
			m_MultipleSubstitutionRecords = value;
		}
	}

	internal List<LigatureSubstitutionRecord> ligatureRecords
	{
		get
		{
			return m_LigatureSubstitutionRecords;
		}
		set
		{
			m_LigatureSubstitutionRecords = value;
		}
	}

	internal List<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords
	{
		get
		{
			return m_GlyphPairAdjustmentRecords;
		}
		set
		{
			m_GlyphPairAdjustmentRecords = value;
		}
	}

	internal List<MarkToBaseAdjustmentRecord> MarkToBaseAdjustmentRecords
	{
		get
		{
			return m_MarkToBaseAdjustmentRecords;
		}
		set
		{
			m_MarkToBaseAdjustmentRecords = value;
		}
	}

	internal List<MarkToMarkAdjustmentRecord> MarkToMarkAdjustmentRecords
	{
		get
		{
			return m_MarkToMarkAdjustmentRecords;
		}
		set
		{
			m_MarkToMarkAdjustmentRecords = value;
		}
	}

	internal FontFeatureTable()
	{
		m_LigatureSubstitutionRecords = new List<LigatureSubstitutionRecord>();
		m_LigatureSubstitutionRecordLookup = new Dictionary<uint, List<LigatureSubstitutionRecord>>();
		m_GlyphPairAdjustmentRecords = new List<GlyphPairAdjustmentRecord>();
		m_GlyphPairAdjustmentRecordLookup = new Dictionary<uint, GlyphPairAdjustmentRecord>();
		m_MarkToBaseAdjustmentRecords = new List<MarkToBaseAdjustmentRecord>();
		m_MarkToBaseAdjustmentRecordLookup = new Dictionary<uint, MarkToBaseAdjustmentRecord>();
		m_MarkToMarkAdjustmentRecords = new List<MarkToMarkAdjustmentRecord>();
		m_MarkToMarkAdjustmentRecordLookup = new Dictionary<uint, MarkToMarkAdjustmentRecord>();
	}

	public void SortGlyphPairAdjustmentRecords()
	{
		if (m_GlyphPairAdjustmentRecords.Count > 0)
		{
			m_GlyphPairAdjustmentRecords = (from s in m_GlyphPairAdjustmentRecords
				orderby s.firstAdjustmentRecord.glyphIndex, s.secondAdjustmentRecord.glyphIndex
				select s).ToList();
		}
	}

	public void SortMarkToBaseAdjustmentRecords()
	{
		if (m_MarkToBaseAdjustmentRecords.Count > 0)
		{
			m_MarkToBaseAdjustmentRecords = (from s in m_MarkToBaseAdjustmentRecords
				orderby s.baseGlyphID, s.markGlyphID
				select s).ToList();
		}
	}

	public void SortMarkToMarkAdjustmentRecords()
	{
		if (m_MarkToMarkAdjustmentRecords.Count > 0)
		{
			m_MarkToMarkAdjustmentRecords = (from s in m_MarkToMarkAdjustmentRecords
				orderby s.baseMarkGlyphID, s.combiningMarkGlyphID
				select s).ToList();
		}
	}
}
internal struct Extents
{
	public Vector2 min;

	public Vector2 max;

	public Extents(Vector2 min, Vector2 max)
	{
		this.min = min;
		this.max = max;
	}

	public override string ToString()
	{
		return "Min (" + min.x.ToString("f2") + ", " + min.y.ToString("f2") + ")   Max (" + max.x.ToString("f2") + ", " + max.y.ToString("f2") + ")";
	}
}
internal struct LineInfo
{
	internal int controlCharacterCount;

	public int characterCount;

	public int visibleCharacterCount;

	public int spaceCount;

	public int visibleSpaceCount;

	public int wordCount;

	public int firstCharacterIndex;

	public int firstVisibleCharacterIndex;

	public int lastCharacterIndex;

	public int lastVisibleCharacterIndex;

	public float length;

	public float lineHeight;

	public float ascender;

	public float baseline;

	public float descender;

	public float maxAdvance;

	public float width;

	public float marginLeft;

	public float marginRight;

	public TextAlignment alignment;

	public Extents lineExtents;
}
internal struct LinkInfo
{
	public int hashCode;

	public int linkIdFirstCharacterIndex;

	public int linkIdLength;

	public int linkTextfirstCharacterIndex;

	public int linkTextLength;

	internal char[] linkId;

	private string m_LinkIdString;

	private string m_LinkTextString;

	internal void SetLinkId(char[] text, int startIndex, int length)
	{
		if (linkId == null || linkId.Length < length)
		{
			linkId = new char[length];
		}
		for (int i = 0; i < length; i++)
		{
			linkId[i] = text[startIndex + i];
		}
		linkIdLength = length;
		m_LinkIdString = null;
		m_LinkTextString = null;
	}

	public string GetLinkText(TextInfo textInfo)
	{
		if (string.IsNullOrEmpty(m_LinkTextString))
		{
			for (int i = linkTextfirstCharacterIndex; i < linkTextfirstCharacterIndex + linkTextLength; i++)
			{
				m_LinkTextString += textInfo.textElementInfo[i].character;
			}
		}
		return m_LinkTextString;
	}

	public string GetLinkId()
	{
		if (string.IsNullOrEmpty(m_LinkIdString))
		{
			m_LinkIdString = new string(linkId, 0, linkIdLength);
		}
		return m_LinkIdString;
	}
}
internal static class MaterialManager
{
	private static Dictionary<long, Material> s_FallbackMaterials = new Dictionary<long, Material>();

	public static Material GetFallbackMaterial(Material sourceMaterial, Material targetMaterial)
	{
		int instanceID = sourceMaterial.GetInstanceID();
		Texture texture = targetMaterial.GetTexture(TextShaderUtilities.ID_MainTex);
		int instanceID2 = texture.GetInstanceID();
		long key = ((long)instanceID << 32) | (uint)instanceID2;
		if (s_FallbackMaterials.TryGetValue(key, out var value))
		{
			int num = sourceMaterial.ComputeCRC();
			int num2 = value.ComputeCRC();
			if (num == num2)
			{
				return value;
			}
			CopyMaterialPresetProperties(sourceMaterial, value);
			return value;
		}
		if (sourceMaterial.HasProperty(TextShaderUtilities.ID_GradientScale) && targetMaterial.HasProperty(TextShaderUtilities.ID_GradientScale))
		{
			value = new Material(sourceMaterial);
			value.hideFlags = HideFlags.HideAndDontSave;
			value.SetTexture(TextShaderUtilities.ID_MainTex, texture);
			value.SetFloat(TextShaderUtilities.ID_GradientScale, targetMaterial.GetFloat(TextShaderUtilities.ID_GradientScale));
			value.SetFloat(TextShaderUtilities.ID_TextureWidth, targetMaterial.GetFloat(TextShaderUtilities.ID_TextureWidth));
			value.SetFloat(TextShaderUtilities.ID_TextureHeight, targetMaterial.GetFloat(TextShaderUtilities.ID_TextureHeight));
			value.SetFloat(TextShaderUtilities.ID_WeightNormal, targetMaterial.GetFloat(TextShaderUtilities.ID_WeightNormal));
			value.SetFloat(TextShaderUtilities.ID_WeightBold, targetMaterial.GetFloat(TextShaderUtilities.ID_WeightBold));
		}
		else
		{
			value = new Material(targetMaterial);
		}
		s_FallbackMaterials.Add(key, value);
		return value;
	}

	public static Material GetFallbackMaterial(FontAsset fontAsset, Material sourceMaterial, int atlasIndex)
	{
		int instanceID = sourceMaterial.GetInstanceID();
		Texture texture = fontAsset.atlasTextures[atlasIndex];
		int instanceID2 = texture.GetInstanceID();
		long key = ((long)instanceID << 32) | (uint)instanceID2;
		if (s_FallbackMaterials.TryGetValue(key, out var value))
		{
			int num = sourceMaterial.ComputeCRC();
			int num2 = value.ComputeCRC();
			if (num == num2)
			{
				return value;
			}
			CopyMaterialPresetProperties(sourceMaterial, value);
			return value;
		}
		value = new Material(sourceMaterial);
		value.SetTexture(TextShaderUtilities.ID_MainTex, texture);
		value.hideFlags = HideFlags.HideAndDontSave;
		s_FallbackMaterials.Add(key, value);
		return value;
	}

	private static void CopyMaterialPresetProperties(Material source, Material destination)
	{
		if (source.HasProperty(TextShaderUtilities.ID_GradientScale) && destination.HasProperty(TextShaderUtilities.ID_GradientScale))
		{
			Texture texture = destination.GetTexture(TextShaderUtilities.ID_MainTex);
			float value = destination.GetFloat(TextShaderUtilities.ID_GradientScale);
			float value2 = destination.GetFloat(TextShaderUtilities.ID_TextureWidth);
			float value3 = destination.GetFloat(TextShaderUtilities.ID_TextureHeight);
			float value4 = destination.GetFloat(TextShaderUtilities.ID_WeightNormal);
			float value5 = destination.GetFloat(TextShaderUtilities.ID_WeightBold);
			destination.shader = source.shader;
			destination.CopyPropertiesFromMaterial(source);
			destination.shaderKeywords = source.shaderKeywords;
			destination.SetTexture(TextShaderUtilities.ID_MainTex, texture);
			destination.SetFloat(TextShaderUtilities.ID_GradientScale, value);
			destination.SetFloat(TextShaderUtilities.ID_TextureWidth, value2);
			destination.SetFloat(TextShaderUtilities.ID_TextureHeight, value3);
			destination.SetFloat(TextShaderUtilities.ID_WeightNormal, value4);
			destination.SetFloat(TextShaderUtilities.ID_WeightBold, value5);
		}
	}
}
internal struct MaterialReference
{
	public int index;

	public FontAsset fontAsset;

	public SpriteAsset spriteAsset;

	public Material material;

	public bool isDefaultMaterial;

	public bool isFallbackMaterial;

	public Material fallbackMaterial;

	public float padding;

	public int referenceCount;

	public MaterialReference(int index, FontAsset fontAsset, SpriteAsset spriteAsset, Material material, float padding)
	{
		this.index = index;
		this.fontAsset = fontAsset;
		this.spriteAsset = spriteAsset;
		this.material = material;
		isDefaultMaterial = material.GetInstanceID() == fontAsset.material.GetInstanceID();
		isFallbackMaterial = false;
		fallbackMaterial = null;
		this.padding = padding;
		referenceCount = 0;
	}

	public static bool Contains(MaterialReference[] materialReferences, FontAsset fontAsset)
	{
		int instanceID = fontAsset.GetInstanceID();
		for (int i = 0; i < materialReferences.Length && materialReferences[i].fontAsset != null; i++)
		{
			if (materialReferences[i].fontAsset.GetInstanceID() == instanceID)
			{
				return true;
			}
		}
		return false;
	}

	public static int AddMaterialReference(Material material, FontAsset fontAsset, ref MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
	{
		int instanceID = material.GetInstanceID();
		if (materialReferenceIndexLookup.TryGetValue(instanceID, out var value))
		{
			return value;
		}
		value = (materialReferenceIndexLookup[instanceID] = materialReferenceIndexLookup.Count);
		if (value >= materialReferences.Length)
		{
			Array.Resize(ref materialReferences, Mathf.NextPowerOfTwo(value + 1));
		}
		materialReferences[value].index = value;
		materialReferences[value].fontAsset = fontAsset;
		materialReferences[value].spriteAsset = null;
		materialReferences[value].material = material;
		materialReferences[value].isDefaultMaterial = instanceID == fontAsset.material.GetInstanceID();
		materialReferences[value].referenceCount = 0;
		return value;
	}

	public static int AddMaterialReference(Material material, SpriteAsset spriteAsset, ref MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
	{
		int instanceID = material.GetInstanceID();
		if (materialReferenceIndexLookup.TryGetValue(instanceID, out var value))
		{
			return value;
		}
		value = (materialReferenceIndexLookup[instanceID] = materialReferenceIndexLookup.Count);
		if (value >= materialReferences.Length)
		{
			Array.Resize(ref materialReferences, Mathf.NextPowerOfTwo(value + 1));
		}
		materialReferences[value].index = value;
		materialReferences[value].fontAsset = materialReferences[0].fontAsset;
		materialReferences[value].spriteAsset = spriteAsset;
		materialReferences[value].material = material;
		materialReferences[value].isDefaultMaterial = true;
		materialReferences[value].referenceCount = 0;
		return value;
	}
}
internal class MaterialReferenceManager
{
	private static MaterialReferenceManager s_Instance;

	private Dictionary<int, Material> m_FontMaterialReferenceLookup = new Dictionary<int, Material>();

	private Dictionary<int, FontAsset> m_FontAssetReferenceLookup = new Dictionary<int, FontAsset>();

	private Dictionary<int, SpriteAsset> m_SpriteAssetReferenceLookup = new Dictionary<int, SpriteAsset>();

	private Dictionary<int, TextColorGradient> m_ColorGradientReferenceLookup = new Dictionary<int, TextColorGradient>();

	public static MaterialReferenceManager instance
	{
		get
		{
			if (s_Instance == null)
			{
				s_Instance = new MaterialReferenceManager();
			}
			return s_Instance;
		}
	}

	public static void AddFontAsset(FontAsset fontAsset)
	{
		instance.AddFontAssetInternal(fontAsset);
	}

	private void AddFontAssetInternal(FontAsset fontAsset)
	{
		if (!m_FontAssetReferenceLookup.ContainsKey(fontAsset.hashCode))
		{
			m_FontAssetReferenceLookup.Add(fontAsset.hashCode, fontAsset);
			m_FontMaterialReferenceLookup.Add(fontAsset.materialHashCode, fontAsset.material);
		}
	}

	public static void AddSpriteAsset(SpriteAsset spriteAsset)
	{
		instance.AddSpriteAssetInternal(spriteAsset);
	}

	private void AddSpriteAssetInternal(SpriteAsset spriteAsset)
	{
		if (!m_SpriteAssetReferenceLookup.ContainsKey(spriteAsset.hashCode))
		{
			m_SpriteAssetReferenceLookup.Add(spriteAsset.hashCode, spriteAsset);
			m_FontMaterialReferenceLookup.Add(spriteAsset.hashCode, spriteAsset.material);
		}
	}

	public static void AddSpriteAsset(int hashCode, SpriteAsset spriteAsset)
	{
		instance.AddSpriteAssetInternal(hashCode, spriteAsset);
	}

	private void AddSpriteAssetInternal(int hashCode, SpriteAsset spriteAsset)
	{
		if (!m_SpriteAssetReferenceLookup.ContainsKey(hashCode))
		{
			m_SpriteAssetReferenceLookup.Add(hashCode, spriteAsset);
			m_FontMaterialReferenceLookup.Add(hashCode, spriteAsset.material);
		}
	}

	public static void AddFontMaterial(int hashCode, Material material)
	{
		instance.AddFontMaterialInternal(hashCode, material);
	}

	private void AddFontMaterialInternal(int hashCode, Material material)
	{
		m_FontMaterialReferenceLookup.Add(hashCode, material);
	}

	public static void AddColorGradientPreset(int hashCode, TextColorGradient spriteAsset)
	{
		instance.AddColorGradientPreset_Internal(hashCode, spriteAsset);
	}

	private void AddColorGradientPreset_Internal(int hashCode, TextColorGradient spriteAsset)
	{
		if (!m_ColorGradientReferenceLookup.ContainsKey(hashCode))
		{
			m_ColorGradientReferenceLookup.Add(hashCode, spriteAsset);
		}
	}

	public bool Contains(FontAsset font)
	{
		return m_FontAssetReferenceLookup.ContainsKey(font.hashCode);
	}

	public bool Contains(SpriteAsset sprite)
	{
		return m_FontAssetReferenceLookup.ContainsKey(sprite.hashCode);
	}

	public static bool TryGetFontAsset(int hashCode, out FontAsset fontAsset)
	{
		return instance.TryGetFontAssetInternal(hashCode, out fontAsset);
	}

	private bool TryGetFontAssetInternal(int hashCode, out FontAsset fontAsset)
	{
		fontAsset = null;
		return m_FontAssetReferenceLookup.TryGetValue(hashCode, out fontAsset);
	}

	public static bool TryGetSpriteAsset(int hashCode, out SpriteAsset spriteAsset)
	{
		return instance.TryGetSpriteAssetInternal(hashCode, out spriteAsset);
	}

	private bool TryGetSpriteAssetInternal(int hashCode, out SpriteAsset spriteAsset)
	{
		spriteAsset = null;
		return m_SpriteAssetReferenceLookup.TryGetValue(hashCode, out spriteAsset);
	}

	public static bool TryGetColorGradientPreset(int hashCode, out TextColorGradient gradientPreset)
	{
		return instance.TryGetColorGradientPresetInternal(hashCode, out gradientPreset);
	}

	private bool TryGetColorGradientPresetInternal(int hashCode, out TextColorGradient gradientPreset)
	{
		gradientPreset = null;
		return m_ColorGradientReferenceLookup.TryGetValue(hashCode, out gradientPreset);
	}

	public static bool TryGetMaterial(int hashCode, out Material material)
	{
		return instance.TryGetMaterialInternal(hashCode, out material);
	}

	private bool TryGetMaterialInternal(int hashCode, out Material material)
	{
		material = null;
		return m_FontMaterialReferenceLookup.TryGetValue(hashCode, out material);
	}
}
internal enum VertexSortingOrder
{
	Normal,
	Reverse
}
internal struct MeshInfo
{
	private static readonly Color32 k_DefaultColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	private static readonly Vector3 k_DefaultNormal = new Vector3(0f, 0f, -1f);

	private static readonly Vector4 k_DefaultTangent = new Vector4(-1f, 0f, 0f, 1f);

	public int vertexCount;

	public Vector3[] vertices;

	public Vector3[] normals;

	public Vector4[] tangents;

	public Vector4[] uvs0;

	public Vector2[] uvs2;

	public Color32[] colors32;

	public int[] triangles;

	public Material material;

	internal GlyphRenderMode glyphRenderMode;

	public MeshInfo(int size)
	{
		material = null;
		size = Mathf.Min(size, 16383);
		int num = size * 4;
		int num2 = size * 6;
		vertexCount = 0;
		vertices = new Vector3[num];
		uvs0 = new Vector4[num];
		uvs2 = new Vector2[num];
		colors32 = new Color32[num];
		normals = new Vector3[num];
		tangents = new Vector4[num];
		triangles = new int[num2];
		int num3 = 0;
		int num4 = 0;
		while (num4 / 4 < size)
		{
			for (int i = 0; i < 4; i++)
			{
				vertices[num4 + i] = Vector3.zero;
				uvs0[num4 + i] = Vector2.zero;
				uvs2[num4 + i] = Vector2.zero;
				colors32[num4 + i] = k_DefaultColor;
				normals[num4 + i] = k_DefaultNormal;
				tangents[num4 + i] = k_DefaultTangent;
			}
			triangles[num3] = num4;
			triangles[num3 + 1] = num4 + 1;
			triangles[num3 + 2] = num4 + 2;
			triangles[num3 + 3] = num4 + 2;
			triangles[num3 + 4] = num4 + 3;
			triangles[num3 + 5] = num4;
			num4 += 4;
			num3 += 6;
		}
		material = null;
		glyphRenderMode = (GlyphRenderMode)0;
	}

	internal void ResizeMeshInfo(int size)
	{
		size = Mathf.Min(size, 16383);
		int newSize = size * 4;
		int newSize2 = size * 6;
		int num = vertices.Length / 4;
		Array.Resize(ref vertices, newSize);
		Array.Resize(ref uvs0, newSize);
		Array.Resize(ref uvs2, newSize);
		Array.Resize(ref colors32, newSize);
		Array.Resize(ref triangles, newSize2);
		for (int i = num; i < size; i++)
		{
			int num2 = i * 4;
			int num3 = i * 6;
			triangles[num3] = num2;
			triangles[1 + num3] = 1 + num2;
			triangles[2 + num3] = 2 + num2;
			triangles[3 + num3] = 2 + num2;
			triangles[4 + num3] = 3 + num2;
			triangles[5 + num3] = num2;
		}
	}

	internal void Clear(bool uploadChanges)
	{
		if (vertices != null)
		{
			Array.Clear(vertices, 0, vertices.Length);
			vertexCount = 0;
		}
	}

	internal void ClearUnusedVertices()
	{
		int num = vertices.Length - vertexCount;
		if (num > 0)
		{
			Array.Clear(vertices, vertexCount, num);
		}
	}

	public void ClearUnusedVertices(int startIndex, bool updateMesh)
	{
		int num = vertices.Length - startIndex;
		if (num > 0)
		{
			Array.Clear(vertices, startIndex, num);
		}
	}

	internal void ClearUnusedVertices(int startIndex)
	{
		int num = vertices.Length - startIndex;
		if (num > 0)
		{
			Array.Clear(vertices, startIndex, num);
		}
	}

	internal void SortGeometry(VertexSortingOrder order)
	{
		if (order == VertexSortingOrder.Normal || order != VertexSortingOrder.Reverse)
		{
			return;
		}
		int num = vertexCount / 4;
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 4;
			int num3 = (num - i - 1) * 4;
			if (num2 < num3)
			{
				SwapVertexData(num2, num3);
			}
		}
	}

	internal void SwapVertexData(int src, int dst)
	{
		Vector3 vector = vertices[dst];
		vertices[dst] = vertices[src];
		vertices[src] = vector;
		vector = vertices[dst + 1];
		vertices[dst + 1] = vertices[src + 1];
		vertices[src + 1] = vector;
		vector = vertices[dst + 2];
		vertices[dst + 2] = vertices[src + 2];
		vertices[src + 2] = vector;
		vector = vertices[dst + 3];
		vertices[dst + 3] = vertices[src + 3];
		vertices[src + 3] = vector;
		Vector4 vector2 = uvs0[dst];
		uvs0[dst] = uvs0[src];
		uvs0[src] = vector2;
		vector2 = uvs0[dst + 1];
		uvs0[dst + 1] = uvs0[src + 1];
		uvs0[src + 1] = vector2;
		vector2 = uvs0[dst + 2];
		uvs0[dst + 2] = uvs0[src + 2];
		uvs0[src + 2] = vector2;
		vector2 = uvs0[dst + 3];
		uvs0[dst + 3] = uvs0[src + 3];
		uvs0[src + 3] = vector2;
		vector2 = uvs2[dst];
		uvs2[dst] = uvs2[src];
		uvs2[src] = vector2;
		vector2 = uvs2[dst + 1];
		uvs2[dst + 1] = uvs2[src + 1];
		uvs2[src + 1] = vector2;
		vector2 = uvs2[dst + 2];
		uvs2[dst + 2] = uvs2[src + 2];
		uvs2[src + 2] = vector2;
		vector2 = uvs2[dst + 3];
		uvs2[dst + 3] = uvs2[src + 3];
		uvs2[src + 3] = vector2;
		Color32 color = colors32[dst];
		colors32[dst] = colors32[src];
		colors32[src] = color;
		color = colors32[dst + 1];
		colors32[dst + 1] = colors32[src + 1];
		colors32[src + 1] = color;
		color = colors32[dst + 2];
		colors32[dst + 2] = colors32[src + 2];
		colors32[src + 2] = color;
		color = colors32[dst + 3];
		colors32[dst + 3] = colors32[src + 3];
		colors32[src + 3] = color;
	}
}
[HelpURL("https://docs.unity3d.com/2022.3/Documentation/Manual/UIE-sprite.html")]
[ExcludeFromPreset]
public class SpriteAsset : TextAsset
{
	internal Dictionary<int, int> m_NameLookup;

	internal Dictionary<uint, int> m_GlyphIndexLookup;

	[SerializeField]
	internal FaceInfo m_FaceInfo;

	[FormerlySerializedAs("spriteSheet")]
	[SerializeField]
	internal Texture m_SpriteAtlasTexture;

	[SerializeField]
	private List<SpriteCharacter> m_SpriteCharacterTable = new List<SpriteCharacter>();

	internal Dictionary<uint, SpriteCharacter> m_SpriteCharacterLookup;

	[SerializeField]
	private List<SpriteGlyph> m_SpriteGlyphTable = new List<SpriteGlyph>();

	internal Dictionary<uint, SpriteGlyph> m_SpriteGlyphLookup;

	[SerializeField]
	public List<SpriteAsset> fallbackSpriteAssets;

	internal bool m_IsSpriteAssetLookupTablesDirty = false;

	private static HashSet<int> k_searchedSpriteAssets;

	public FaceInfo faceInfo
	{
		get
		{
			return m_FaceInfo;
		}
		internal set
		{
			m_FaceInfo = value;
		}
	}

	public Texture spriteSheet
	{
		get
		{
			return m_SpriteAtlasTexture;
		}
		internal set
		{
			m_SpriteAtlasTexture = value;
		}
	}

	public List<SpriteCharacter> spriteCharacterTable
	{
		get
		{
			if (m_GlyphIndexLookup == null)
			{
				UpdateLookupTables();
			}
			return m_SpriteCharacterTable;
		}
		internal set
		{
			m_SpriteCharacterTable = value;
		}
	}

	public Dictionary<uint, SpriteCharacter> spriteCharacterLookupTable
	{
		get
		{
			if (m_SpriteCharacterLookup == null)
			{
				UpdateLookupTables();
			}
			return m_SpriteCharacterLookup;
		}
		internal set
		{
			m_SpriteCharacterLookup = value;
		}
	}

	public List<SpriteGlyph> spriteGlyphTable
	{
		get
		{
			return m_SpriteGlyphTable;
		}
		internal set
		{
			m_SpriteGlyphTable = value;
		}
	}

	private void Awake()
	{
	}

	public void UpdateLookupTables()
	{
		if (m_GlyphIndexLookup == null)
		{
			m_GlyphIndexLookup = new Dictionary<uint, int>();
		}
		else
		{
			m_GlyphIndexLookup.Clear();
		}
		if (m_SpriteGlyphLookup == null)
		{
			m_SpriteGlyphLookup = new Dictionary<uint, SpriteGlyph>();
		}
		else
		{
			m_SpriteGlyphLookup.Clear();
		}
		for (int i = 0; i < m_SpriteGlyphTable.Count; i++)
		{
			SpriteGlyph spriteGlyph = m_SpriteGlyphTable[i];
			uint index = spriteGlyph.index;
			if (!m_GlyphIndexLookup.ContainsKey(index))
			{
				m_GlyphIndexLookup.Add(index, i);
			}
			if (!m_SpriteGlyphLookup.ContainsKey(index))
			{
				m_SpriteGlyphLookup.Add(index, spriteGlyph);
			}
		}
		if (m_NameLookup == null)
		{
			m_NameLookup = new Dictionary<int, int>();
		}
		else
		{
			m_NameLookup.Clear();
		}
		if (m_SpriteCharacterLookup == null)
		{
			m_SpriteCharacterLookup = new Dictionary<uint, SpriteCharacter>();
		}
		else
		{
			m_SpriteCharacterLookup.Clear();
		}
		for (int j = 0; j < m_SpriteCharacterTable.Count; j++)
		{
			SpriteCharacter spriteCharacter = m_SpriteCharacterTable[j];
			if (spriteCharacter == null)
			{
				continue;
			}
			uint glyphIndex = spriteCharacter.glyphIndex;
			if (m_SpriteGlyphLookup.ContainsKey(glyphIndex))
			{
				spriteCharacter.glyph = m_SpriteGlyphLookup[glyphIndex];
				spriteCharacter.textAsset = this;
				int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive(m_SpriteCharacterTable[j].name);
				if (!m_NameLookup.ContainsKey(hashCodeCaseInSensitive))
				{
					m_NameLookup.Add(hashCodeCaseInSensitive, j);
				}
				uint unicode = m_SpriteCharacterTable[j].unicode;
				if (unicode != 65534 && !m_SpriteCharacterLookup.ContainsKey(unicode))
				{
					m_SpriteCharacterLookup.Add(unicode, spriteCharacter);
				}
			}
		}
		m_IsSpriteAssetLookupTablesDirty = false;
	}

	public int GetSpriteIndexFromHashcode(int hashCode)
	{
		if (m_NameLookup == null)
		{
			UpdateLookupTables();
		}
		if (m_NameLookup.TryGetValue(hashCode, out var value))
		{
			return value;
		}
		return -1;
	}

	public int GetSpriteIndexFromUnicode(uint unicode)
	{
		if (m_SpriteCharacterLookup == null)
		{
			UpdateLookupTables();
		}
		if (m_SpriteCharacterLookup.TryGetValue(unicode, out var value))
		{
			return (int)value.glyphIndex;
		}
		return -1;
	}

	public int GetSpriteIndexFromName(string name)
	{
		if (m_NameLookup == null)
		{
			UpdateLookupTables();
		}
		int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive(name);
		return GetSpriteIndexFromHashcode(hashCodeCaseInSensitive);
	}

	public static SpriteAsset SearchForSpriteByUnicode(SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
	{
		if (spriteAsset == null)
		{
			spriteIndex = -1;
			return null;
		}
		spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
		if (spriteIndex != -1)
		{
			return spriteAsset;
		}
		if (k_searchedSpriteAssets == null)
		{
			k_searchedSpriteAssets = new HashSet<int>();
		}
		else
		{
			k_searchedSpriteAssets.Clear();
		}
		int item = spriteAsset.GetInstanceID();
		k_searchedSpriteAssets.Add(item);
		if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
		{
			return SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, includeFallbacks: true, out spriteIndex);
		}
		spriteIndex = -1;
		return null;
	}

	private static SpriteAsset SearchForSpriteByUnicodeInternal(List<SpriteAsset> spriteAssets, uint unicode, bool includeFallbacks, out int spriteIndex)
	{
		for (int i = 0; i < spriteAssets.Count; i++)
		{
			SpriteAsset spriteAsset = spriteAssets[i];
			if (spriteAsset == null)
			{
				continue;
			}
			int item = spriteAsset.GetInstanceID();
			if (k_searchedSpriteAssets.Add(item))
			{
				spriteAsset = SearchForSpriteByUnicodeInternal(spriteAsset, unicode, includeFallbacks, out spriteIndex);
				if (spriteAsset != null)
				{
					return spriteAsset;
				}
			}
		}
		spriteIndex = -1;
		return null;
	}

	private static SpriteAsset SearchForSpriteByUnicodeInternal(SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
	{
		spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
		if (spriteIndex != -1)
		{
			return spriteAsset;
		}
		if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
		{
			return SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, includeFallbacks: true, out spriteIndex);
		}
		spriteIndex = -1;
		return null;
	}

	public static SpriteAsset SearchForSpriteByHashCode(SpriteAsset spriteAsset, int hashCode, bool includeFallbacks, out int spriteIndex, TextSettings textSettings = null)
	{
		if (spriteAsset == null)
		{
			spriteIndex = -1;
			return null;
		}
		spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
		if (spriteIndex != -1)
		{
			return spriteAsset;
		}
		if (k_searchedSpriteAssets == null)
		{
			k_searchedSpriteAssets = new HashSet<int>();
		}
		else
		{
			k_searchedSpriteAssets.Clear();
		}
		int item = spriteAsset.instanceID;
		k_searchedSpriteAssets.Add(item);
		if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
		{
			SpriteAsset result = SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, searchFallbacks: true, out spriteIndex);
			if (spriteIndex != -1)
			{
				return result;
			}
		}
		if (textSettings == null)
		{
			spriteIndex = -1;
			return null;
		}
		if (includeFallbacks && textSettings.defaultSpriteAsset != null)
		{
			SpriteAsset result = SearchForSpriteByHashCodeInternal(textSettings.defaultSpriteAsset, hashCode, searchFallbacks: true, out spriteIndex);
			if (spriteIndex != -1)
			{
				return result;
			}
		}
		k_searchedSpriteAssets.Clear();
		uint missingSpriteCharacterUnicode = textSettings.missingSpriteCharacterUnicode;
		spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(missingSpriteCharacterUnicode);
		if (spriteIndex != -1)
		{
			return spriteAsset;
		}
		k_searchedSpriteAssets.Add(item);
		if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
		{
			SpriteAsset result = SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, missingSpriteCharacterUnicode, includeFallbacks: true, out spriteIndex);
			if (spriteIndex != -1)
			{
				return result;
			}
		}
		if (includeFallbacks && textSettings.defaultSpriteAsset != null)
		{
			SpriteAsset result = SearchForSpriteByUnicodeInternal(textSettings.defaultSpriteAsset, missingSpriteCharacterUnicode, includeFallbacks: true, out spriteIndex);
			if (spriteIndex != -1)
			{
				return result;
			}
		}
		spriteIndex = -1;
		return null;
	}

	private static SpriteAsset SearchForSpriteByHashCodeInternal(List<SpriteAsset> spriteAssets, int hashCode, bool searchFallbacks, out int spriteIndex)
	{
		for (int i = 0; i < spriteAssets.Count; i++)
		{
			SpriteAsset spriteAsset = spriteAssets[i];
			if (spriteAsset == null)
			{
				continue;
			}
			int item = spriteAsset.instanceID;
			if (k_searchedSpriteAssets.Add(item))
			{
				spriteAsset = SearchForSpriteByHashCodeInternal(spriteAsset, hashCode, searchFallbacks, out spriteIndex);
				if (spriteAsset != null)
				{
					return spriteAsset;
				}
			}
		}
		spriteIndex = -1;
		return null;
	}

	private static SpriteAsset SearchForSpriteByHashCodeInternal(SpriteAsset spriteAsset, int hashCode, bool searchFallbacks, out int spriteIndex)
	{
		spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
		if (spriteIndex != -1)
		{
			return spriteAsset;
		}
		if (searchFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
		{
			return SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, searchFallbacks: true, out spriteIndex);
		}
		spriteIndex = -1;
		return null;
	}

	public void SortGlyphTable()
	{
		if (m_SpriteGlyphTable != null && m_SpriteGlyphTable.Count != 0)
		{
			m_SpriteGlyphTable = m_SpriteGlyphTable.OrderBy((SpriteGlyph item) => item.index).ToList();
		}
	}

	internal void SortCharacterTable()
	{
		if (m_SpriteCharacterTable != null && m_SpriteCharacterTable.Count > 0)
		{
			m_SpriteCharacterTable = m_SpriteCharacterTable.OrderBy((SpriteCharacter c) => c.unicode).ToList();
		}
	}

	internal void SortGlyphAndCharacterTables()
	{
		SortGlyphTable();
		SortCharacterTable();
	}
}
[Serializable]
public class SpriteCharacter : TextElement
{
	[SerializeField]
	private string m_Name;

	public string name
	{
		get
		{
			return m_Name;
		}
		set
		{
			if (!(value == m_Name))
			{
				m_Name = value;
			}
		}
	}

	public SpriteCharacter()
	{
		m_ElementType = TextElementType.Sprite;
	}

	public SpriteCharacter(uint unicode, SpriteGlyph glyph)
	{
		m_ElementType = TextElementType.Sprite;
		base.unicode = unicode;
		base.glyphIndex = glyph.index;
		base.glyph = glyph;
		base.scale = 1f;
	}

	public SpriteCharacter(uint unicode, SpriteAsset spriteAsset, SpriteGlyph glyph)
	{
		m_ElementType = TextElementType.Sprite;
		base.unicode = unicode;
		base.textAsset = spriteAsset;
		base.glyph = glyph;
		base.glyphIndex = glyph.index;
		base.scale = 1f;
	}
}
[Serializable]
public class SpriteGlyph : Glyph
{
	public Sprite sprite;

	public SpriteGlyph()
	{
	}

	public SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
	{
		base.index = index;
		base.metrics = metrics;
		base.glyphRect = glyphRect;
		base.scale = scale;
		base.atlasIndex = atlasIndex;
	}

	public SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex, Sprite sprite)
	{
		base.index = index;
		base.metrics = metrics;
		base.glyphRect = glyphRect;
		base.scale = scale;
		base.atlasIndex = atlasIndex;
		this.sprite = sprite;
	}
}
[Serializable]
[ExcludeFromObjectFactory]
public abstract class TextAsset : ScriptableObject
{
	[SerializeField]
	internal string m_Version;

	internal int m_InstanceID;

	internal int m_HashCode;

	[FormerlySerializedAs("material")]
	[SerializeField]
	internal Material m_Material;

	internal int m_MaterialHashCode;

	public string version
	{
		get
		{
			return m_Version;
		}
		internal set
		{
			m_Version = value;
		}
	}

	public int instanceID
	{
		get
		{
			if (m_InstanceID == 0)
			{
				m_InstanceID = GetInstanceID();
			}
			return m_InstanceID;
		}
	}

	public int hashCode
	{
		get
		{
			if (m_HashCode == 0)
			{
				m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name);
			}
			return m_HashCode;
		}
		set
		{
			m_HashCode = value;
		}
	}

	public Material material
	{
		get
		{
			return m_Material;
		}
		set
		{
			m_Material = value;
		}
	}

	public int materialHashCode
	{
		get
		{
			if (m_MaterialHashCode == 0)
			{
				if (m_Material == null)
				{
					return 0;
				}
				m_MaterialHashCode = TextUtilities.GetHashCodeCaseInSensitive(m_Material.name);
			}
			return m_MaterialHashCode;
		}
		set
		{
			m_MaterialHashCode = value;
		}
	}
}
public enum ColorGradientMode
{
	Single,
	HorizontalGradient,
	VerticalGradient,
	FourCornersGradient
}
[Serializable]
[ExcludeFromPreset]
[ExcludeFromObjectFactory]
public class TextColorGradient : ScriptableObject
{
	public ColorGradientMode colorMode = ColorGradientMode.FourCornersGradient;

	public Color topLeft;

	public Color topRight;

	public Color bottomLeft;

	public Color bottomRight;

	private const ColorGradientMode k_DefaultColorMode = ColorGradientMode.FourCornersGradient;

	private static readonly Color k_DefaultColor = Color.white;

	public TextColorGradient()
	{
		colorMode = ColorGradientMode.FourCornersGradient;
		topLeft = k_DefaultColor;
		topRight = k_DefaultColor;
		bottomLeft = k_DefaultColor;
		bottomRight = k_DefaultColor;
	}

	public TextColorGradient(Color color)
	{
		colorMode = ColorGradientMode.FourCornersGradient;
		topLeft = color;
		topRight = color;
		bottomLeft = color;
		bottomRight = color;
	}

	public TextColorGradient(Color color0, Color color1, Color color2, Color color3)
	{
		colorMode = ColorGradientMode.FourCornersGradient;
		topLeft = color0;
		topRight = color1;
		bottomLeft = color2;
		bottomRight = color3;
	}
}
public enum TextElementType : byte
{
	Character = 1,
	Sprite
}
[Serializable]
public abstract class TextElement
{
	[SerializeField]
	protected TextElementType m_ElementType;

	[SerializeField]
	internal uint m_Unicode;

	internal TextAsset m_TextAsset;

	internal Glyph m_Glyph;

	[SerializeField]
	internal uint m_GlyphIndex;

	[SerializeField]
	internal float m_Scale;

	public TextElementType elementType => m_ElementType;

	public uint unicode
	{
		get
		{
			return m_Unicode;
		}
		set
		{
			m_Unicode = value;
		}
	}

	public TextAsset textAsset
	{
		get
		{
			return m_TextAsset;
		}
		set
		{
			m_TextAsset = value;
		}
	}

	public Glyph glyph
	{
		get
		{
			return m_Glyph;
		}
		set
		{
			m_Glyph = value;
		}
	}

	public uint glyphIndex
	{
		get
		{
			return m_GlyphIndex;
		}
		set
		{
			m_GlyphIndex = value;
		}
	}

	public float scale
	{
		get
		{
			return m_Scale;
		}
		set
		{
			m_Scale = value;
		}
	}
}
internal struct TextVertex
{
	public Vector3 position;

	public Vector4 uv;

	public Vector2 uv2;

	public Color32 color;
}
internal struct TextElementInfo
{
	public char character;

	public int index;

	public TextElementType elementType;

	public int stringLength;

	public TextElement textElement;

	public Glyph alternativeGlyph;

	public FontAsset fontAsset;

	public SpriteAsset spriteAsset;

	public int spriteIndex;

	public Material material;

	public int materialReferenceIndex;

	public bool isUsingAlternateTypeface;

	public float pointSize;

	public int lineNumber;

	public int pageNumber;

	public int vertexIndex;

	public TextVertex vertexTopLeft;

	public TextVertex vertexBottomLeft;

	public TextVertex vertexTopRight;

	public TextVertex vertexBottomRight;

	public Vector3 topLeft;

	public Vector3 bottomLeft;

	public Vector3 topRight;

	public Vector3 bottomRight;

	public float origin;

	public float ascender;

	public float baseLine;

	public float descender;

	internal float adjustedAscender;

	internal float adjustedDescender;

	internal float adjustedHorizontalAdvance;

	public float xAdvance;

	public float aspectRatio;

	public float scale;

	public Color32 color;

	public Color32 underlineColor;

	public int underlineVertexIndex;

	public Color32 strikethroughColor;

	public int strikethroughVertexIndex;

	public Color32 highlightColor;

	public HighlightState highlightState;

	public FontStyles style;

	public bool isVisible;

	public override string ToString()
	{
		return string.Format("{0}: {1}\n{2}: {3}\n{4}: {5}\n{6}: {7}\n{8}: {9}\n{10}: {11}\n{12}: {13}\n{14}: {15}\n{16}: {17}\n{18}: {19}\n{20}: {21}\n{22}: {23}\n{24}: {25}\n{26}: {27}\n{28}: {29}\n{30}: {31}\n{32}: {33}\n{34}: {35}\n{36}: {37}\n{38}: {39}\n{40}: {41}\n{42}: {43}\n{44}: {45}\n{46}: {47}\n{48}: {49}\n{50}: {51}\n{52}: {53}\n{54}: {55}\n{56}: {57}\n{58}: {59}\n{60}: {61}\n{62}: {63}\n{64}: {65}\n{66}: {67}\n{68}: {69}\n{70}: {71}\n{72}: {73}\n{74}: {75}\n{76}: {77}\n{78}: {79}\n{80}: {81}\n{82}: {83}\n{84}: {85}", "character", character, "index", index, "elementType", elementType, "stringLength", stringLength, "textElement", textElement, "alternativeGlyph", alternativeGlyph, "fontAsset", fontAsset, "spriteAsset", spriteAsset, "spriteIndex", spriteIndex, "material", material, "materialReferenceIndex", materialReferenceIndex, "isUsingAlternateTypeface", isUsingAlternateTypeface, "pointSize", pointSize, "lineNumber", lineNumber, "pageNumber", pageNumber, "vertexIndex", vertexIndex, "vertexTopLeft", vertexTopLeft, "vertexBottomLeft", vertexBottomLeft, "vertexTopRight", vertexTopRight, "vertexBottomRight", vertexBottomRight, "topLeft", topLeft, "bottomLeft", bottomLeft, "topRight", topRight, "bottomRight", bottomRight, "origin", origin, "ascender", ascender, "baseLine", baseLine, "descender", descender, "adjustedAscender", adjustedAscender, "adjustedDescender", adjustedDescender, "adjustedHorizontalAdvance", adjustedHorizontalAdvance, "xAdvance", xAdvance, "aspectRatio", aspectRatio, "scale", scale, "color", color, "underlineColor", underlineColor, "underlineVertexIndex", underlineVertexIndex, "strikethroughColor", strikethroughColor, "strikethroughVertexIndex", strikethroughVertexIndex, "highlightColor", highlightColor, "highlightState", highlightState, "style", style, "isVisible", isVisible);
	}

	internal string ToStringTest()
	{
		return "topLeft.x: " + topLeft.x.ToString("F4") + "\n topLeft.y: " + topLeft.y.ToString("F4") + "\n topRight.x: " + topRight.x.ToString("F4") + "\n topRight.y: " + topRight.y.ToString("F4") + "\n  bottomLeft.x: " + bottomLeft.x.ToString("F4") + "\n bottomLeft.y: " + bottomLeft.y.ToString("F4") + "\n  bottomRight.x: " + bottomRight.x.ToString("F4") + "\n bottomRight.y: " + bottomRight.y.ToString("F4") + "\norigin: " + origin.ToString("F4") + "\nxAdvance: " + xAdvance.ToString("F4") + "\n";
	}
}
public static class TextEventManager
{
	public static readonly FastAction<bool, Material> MATERIAL_PROPERTY_EVENT = new FastAction<bool, Material>();

	public static readonly FastAction<bool, Object> FONT_PROPERTY_EVENT = new FastAction<bool, Object>();

	public static readonly FastAction<bool, Object> SPRITE_ASSET_PROPERTY_EVENT = new FastAction<bool, Object>();

	public static readonly FastAction<bool, Object> TEXTMESHPRO_PROPERTY_EVENT = new FastAction<bool, Object>();

	public static readonly FastAction<GameObject, Material, Material> DRAG_AND_DROP_MATERIAL_EVENT = new FastAction<GameObject, Material, Material>();

	public static readonly FastAction<bool> TEXT_STYLE_PROPERTY_EVENT = new FastAction<bool>();

	public static readonly FastAction<Object> COLOR_GRADIENT_PROPERTY_EVENT = new FastAction<Object>();

	public static readonly FastAction TMP_SETTINGS_PROPERTY_EVENT = new FastAction();

	public static readonly FastAction RESOURCE_LOAD_EVENT = new FastAction();

	public static readonly FastAction<bool, Object> TEXTMESHPRO_UGUI_PROPERTY_EVENT = new FastAction<bool, Object>();

	public static readonly FastAction OnPreRenderObject_Event = new FastAction();

	public static readonly FastAction<Object> TEXT_CHANGED_EVENT = new FastAction<Object>();

	public static void ON_PRE_RENDER_OBJECT_CHANGED()
	{
		OnPreRenderObject_Event.Call();
	}

	public static void ON_MATERIAL_PROPERTY_CHANGED(bool isChanged, Material mat)
	{
		MATERIAL_PROPERTY_EVENT.Call(isChanged, mat);
	}

	public static void ON_FONT_PROPERTY_CHANGED(bool isChanged, Object font)
	{
		FONT_PROPERTY_EVENT.Call(isChanged, font);
	}

	public static void ON_SPRITE_ASSET_PROPERTY_CHANGED(bool isChanged, Object obj)
	{
		SPRITE_ASSET_PROPERTY_EVENT.Call(isChanged, obj);
	}

	public static void ON_TEXTMESHPRO_PROPERTY_CHANGED(bool isChanged, Object obj)
	{
		TEXTMESHPRO_PROPERTY_EVENT.Call(isChanged, obj);
	}

	public static void ON_DRAG_AND_DROP_MATERIAL_CHANGED(GameObject sender, Material currentMaterial, Material newMaterial)
	{
		DRAG_AND_DROP_MATERIAL_EVENT.Call(sender, currentMaterial, newMaterial);
	}

	public static void ON_TEXT_STYLE_PROPERTY_CHANGED(bool isChanged)
	{
		TEXT_STYLE_PROPERTY_EVENT.Call(isChanged);
	}

	public static void ON_COLOR_GRADIENT_PROPERTY_CHANGED(Object gradient)
	{
		COLOR_GRADIENT_PROPERTY_EVENT.Call(gradient);
	}

	public static void ON_TEXT_CHANGED(Object obj)
	{
		TEXT_CHANGED_EVENT.Call(obj);
	}

	public static void ON_TMP_SETTINGS_CHANGED()
	{
		TMP_SETTINGS_PROPERTY_EVENT.Call();
	}

	public static void ON_RESOURCES_LOADED()
	{
		RESOURCE_LOAD_EVENT.Call();
	}

	public static void ON_TEXTMESHPRO_UGUI_PROPERTY_CHANGED(bool isChanged, Object obj)
	{
		TEXTMESHPRO_UGUI_PROPERTY_EVENT.Call(isChanged, obj);
	}
}
internal class TextGenerationSettings : IEquatable<TextGenerationSettings>
{
	public string text;

	public Rect screenRect;

	public Vector4 margins;

	public float scale = 1f;

	public FontAsset fontAsset;

	public Material material;

	public SpriteAsset spriteAsset;

	public TextStyleSheet styleSheet;

	public FontStyles fontStyle = FontStyles.Normal;

	public TextSettings textSettings;

	public TextAlignment textAlignment = TextAlignment.TopLeft;

	public TextOverflowMode overflowMode = TextOverflowMode.Overflow;

	public bool wordWrap = false;

	public float wordWrappingRatio;

	public Color color = Color.white;

	public TextColorGradient fontColorGradient;

	public TextColorGradient fontColorGradientPreset;

	public bool tintSprites;

	public bool overrideRichTextColors;

	public bool shouldConvertToLinearSpace = true;

	public float fontSize = 18f;

	public bool autoSize;

	public float fontSizeMin;

	public float fontSizeMax;

	public bool enableKerning = true;

	public bool richText;

	public bool isRightToLeft;

	public float extraPadding = 6f;

	public bool parseControlCharacters = true;

	public bool isOrthographic = true;

	public bool tagNoParsing = false;

	public float characterSpacing;

	public float wordSpacing;

	public float lineSpacing;

	public float paragraphSpacing;

	public float lineSpacingMax;

	public TextWrappingMode textWrappingMode = TextWrappingMode.Normal;

	public int maxVisibleCharacters = 99999;

	public int maxVisibleWords = 99999;

	public int maxVisibleLines = 99999;

	public int firstVisibleCharacter = 0;

	public bool useMaxVisibleDescender;

	public TextFontWeight fontWeight = TextFontWeight.Regular;

	public int pageToDisplay = 1;

	public TextureMapping horizontalMapping = TextureMapping.Character;

	public TextureMapping verticalMapping = TextureMapping.Character;

	public float uvLineOffset;

	public VertexSortingOrder geometrySortingOrder = VertexSortingOrder.Normal;

	public bool inverseYAxis;

	public float charWidthMaxAdj;

	internal TextInputSource inputSource = TextInputSource.TextString;

	public bool Equals(TextGenerationSettings other)
	{
		if ((object)other == null)
		{
			return false;
		}
		if ((object)this == other)
		{
			return true;
		}
		return text == other.text && screenRect.Equals(other.screenRect) && margins.Equals(other.margins) && scale.Equals(other.scale) && object.Equals(fontAsset, other.fontAsset) && object.Equals(material, other.material) && object.Equals(spriteAsset, other.spriteAsset) && object.Equals(styleSheet, other.styleSheet) && fontStyle == other.fontStyle && object.Equals(textSettings, other.textSettings) && textAlignment == other.textAlignment && overflowMode == other.overflowMode && wordWrap == other.wordWrap && wordWrappingRatio.Equals(other.wordWrappingRatio) && color.Equals(other.color) && object.Equals(fontColorGradient, other.fontColorGradient) && object.Equals(fontColorGradientPreset, other.fontColorGradientPreset) && tintSprites == other.tintSprites && overrideRichTextColors == other.overrideRichTextColors && shouldConvertToLinearSpace == other.shouldConvertToLinearSpace && fontSize.Equals(other.fontSize) && autoSize == other.autoSize && fontSizeMin.Equals(other.fontSizeMin) && fontSizeMax.Equals(other.fontSizeMax) && enableKerning == other.enableKerning && richText == other.richText && isRightToLeft == other.isRightToLeft && extraPadding == other.extraPadding && parseControlCharacters == other.parseControlCharacters && isOrthographic == other.isOrthographic && tagNoParsing == other.tagNoParsing && characterSpacing.Equals(other.characterSpacing) && wordSpacing.Equals(other.wordSpacing) && lineSpacing.Equals(other.lineSpacing) && paragraphSpacing.Equals(other.paragraphSpacing) && lineSpacingMax.Equals(other.lineSpacingMax) && textWrappingMode == other.textWrappingMode && maxVisibleCharacters == other.maxVisibleCharacters && maxVisibleWords == other.maxVisibleWords && maxVisibleLines == other.maxVisibleLines && firstVisibleCharacter == other.firstVisibleCharacter && useMaxVisibleDescender == other.useMaxVisibleDescender && fontWeight == other.fontWeight && pageToDisplay == other.pageToDisplay && horizontalMapping == other.horizontalMapping && verticalMapping == other.verticalMapping && uvLineOffset.Equals(other.uvLineOffset) && geometrySortingOrder == other.geometrySortingOrder && inverseYAxis == other.inverseYAxis && charWidthMaxAdj.Equals(other.charWidthMaxAdj) && inputSource == other.inputSource;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (this == obj)
		{
			return true;
		}
		if (obj.GetType() != GetType())
		{
			return false;
		}
		return Equals((TextGenerationSettings)obj);
	}

	public override int GetHashCode()
	{
		HashCode hashCode = default(HashCode);
		hashCode.Add(text);
		hashCode.Add(screenRect);
		hashCode.Add(margins);
		hashCode.Add(scale);
		hashCode.Add(fontAsset);
		hashCode.Add(material);
		hashCode.Add(spriteAsset);
		hashCode.Add(styleSheet);
		hashCode.Add((int)fontStyle);
		hashCode.Add(textSettings);
		hashCode.Add((int)textAlignment);
		hashCode.Add((int)overflowMode);
		hashCode.Add(wordWrap);
		hashCode.Add(wordWrappingRatio);
		hashCode.Add(color);
		hashCode.Add(fontColorGradient);
		hashCode.Add(fontColorGradientPreset);
		hashCode.Add(tintSprites);
		hashCode.Add(overrideRichTextColors);
		hashCode.Add(shouldConvertToLinearSpace);
		hashCode.Add(fontSize);
		hashCode.Add(autoSize);
		hashCode.Add(fontSizeMin);
		hashCode.Add(fontSizeMax);
		hashCode.Add(enableKerning);
		hashCode.Add(richText);
		hashCode.Add(isRightToLeft);
		hashCode.Add(extraPadding);
		hashCode.Add(parseControlCharacters);
		hashCode.Add(isOrthographic);
		hashCode.Add(tagNoParsing);
		hashCode.Add(characterSpacing);
		hashCode.Add(wordSpacing);
		hashCode.Add(lineSpacing);
		hashCode.Add(paragraphSpacing);
		hashCode.Add(lineSpacingMax);
		hashCode.Add((int)textWrappingMode);
		hashCode.Add(maxVisibleCharacters);
		hashCode.Add(maxVisibleWords);
		hashCode.Add(maxVisibleLines);
		hashCode.Add(firstVisibleCharacter);
		hashCode.Add(useMaxVisibleDescender);
		hashCode.Add((int)fontWeight);
		hashCode.Add(pageToDisplay);
		hashCode.Add((int)horizontalMapping);
		hashCode.Add((int)verticalMapping);
		hashCode.Add(uvLineOffset);
		hashCode.Add((int)geometrySortingOrder);
		hashCode.Add(inverseYAxis);
		hashCode.Add(charWidthMaxAdj);
		hashCode.Add((int)inputSource);
		return hashCode.ToHashCode();
	}

	public static bool operator ==(TextGenerationSettings left, TextGenerationSettings right)
	{
		return object.Equals(left, right);
	}

	public static bool operator !=(TextGenerationSettings left, TextGenerationSettings right)
	{
		return !object.Equals(left, right);
	}

	public override string ToString()
	{
		return string.Format("{0}: {1}\n {2}: {3}\n {4}: {5}\n {6}: {7}\n {8}: {9}\n {10}: {11}\n {12}: {13}\n {14}: {15}\n {16}: {17}\n {18}: {19}\n {20}: {21}\n {22}: {23}\n {24}: {25}\n {26}: {27}\n {28}: {29}\n {30}: {31}\n {32}: {33}\n {34}: {35}\n {36}: {37}\n {38}: {39}\n {40}: {41}\n {42}: {43}\n {44}: {45}\n {46}: {47}\n {48}: {49}\n {50}: {51}\n {52}: {53}\n {54}: {55}\n {56}: {57}\n {58}: {59}\n {60}: {61}\n {62}: {63}\n {64}: {65}\n {66}: {67}\n {68}: {69}\n {70}: {71}\n {72}: {73}\n {74}: {75}\n {76}: {77}\n {78}: {79}\n {80}: {81}\n {82}: {83}\n {84}: {85}\n {86}: {87}\n {88}: {89}\n {90}: {91}\n {92}: {93}\n {94}: {95}\n {96}: {97}\n {98}: {99}\n {100}: {101}", "text", text, "screenRect", screenRect, "margins", margins, "scale", scale, "fontAsset", fontAsset, "material", material, "spriteAsset", spriteAsset, "styleSheet", styleSheet, "fontStyle", fontStyle, "textSettings", textSettings, "textAlignment", textAlignment, "overflowMode", overflowMode, "wordWrap", wordWrap, "wordWrappingRatio", wordWrappingRatio, "color", color, "fontColorGradient", fontColorGradient, "fontColorGradientPreset", fontColorGradientPreset, "tintSprites", tintSprites, "overrideRichTextColors", overrideRichTextColors, "shouldConvertToLinearSpace", shouldConvertToLinearSpace, "fontSize", fontSize, "autoSize", autoSize, "fontSizeMin", fontSizeMin, "fontSizeMax", fontSizeMax, "enableKerning", enableKerning, "richText", richText, "isRightToLeft", isRightToLeft, "extraPadding", extraPadding, "parseControlCharacters", parseControlCharacters, "isOrthographic", isOrthographic, "tagNoParsing", tagNoParsing, "characterSpacing", characterSpacing, "wordSpacing", wordSpacing, "lineSpacing", lineSpacing, "paragraphSpacing", paragraphSpacing, "lineSpacingMax", lineSpacingMax, "textWrappingMode", textWrappingMode, "maxVisibleCharacters", maxVisibleCharacters, "maxVisibleWords", maxVisibleWords, "maxVisibleLines", maxVisibleLines, "firstVisibleCharacter", firstVisibleCharacter, "useMaxVisibleDescender", useMaxVisibleDescender, "fontWeight", fontWeight, "pageToDisplay", pageToDisplay, "horizontalMapping", horizontalMapping, "verticalMapping", verticalMapping, "uvLineOffset", uvLineOffset, "geometrySortingOrder", geometrySortingOrder, "inverseYAxis", inverseYAxis, "charWidthMaxAdj", charWidthMaxAdj, "inputSource", inputSource);
	}
}
internal class TextGenerator
{
	public delegate void MissingCharacterEventCallback(uint unicode, int stringIndex, TextInfo text, FontAsset fontAsset);

	protected struct SpecialCharacter
	{
		public Character character;

		public FontAsset fontAsset;

		public Material material;

		public int materialIndex;

		public SpecialCharacter(Character character, int materialIndex)
		{
			this.character = character;
			fontAsset = character.textAsset as FontAsset;
			material = ((fontAsset != null) ? fontAsset.material : null);
			this.materialIndex = materialIndex;
		}
	}

	private const int k_Tab = 9;

	private const int k_LineFeed = 10;

	private const int k_CarriageReturn = 13;

	private const int k_Space = 32;

	private const int k_DoubleQuotes = 34;

	private const int k_NumberSign = 35;

	private const int k_PercentSign = 37;

	private const int k_SingleQuote = 39;

	private const int k_Plus = 43;

	private const int k_Minus = 45;

	private const int k_Period = 46;

	private const int k_LesserThan = 60;

	private const int k_Equal = 61;

	private const int k_GreaterThan = 62;

	private const int k_Underline = 95;

	private const int k_NoBreakSpace = 160;

	private const int k_SoftHyphen = 173;

	private const int k_HyphenMinus = 45;

	private const int k_FigureSpace = 8199;

	private const int k_Hyphen = 8208;

	private const int k_NonBreakingHyphen = 8209;

	private const int k_ZeroWidthSpace = 8203;

	private const int k_NarrowNoBreakSpace = 8239;

	private const int k_WordJoiner = 8288;

	private const int k_HorizontalEllipsis = 8230;

	private const int k_RightSingleQuote = 8217;

	private const int k_Square = 9633;

	private const int k_HangulJamoStart = 4352;

	private const int k_HangulJamoEnd = 4607;

	private const int k_CjkStart = 11904;

	private const int k_CjkEnd = 40959;

	private const int k_HangulJameExtendedStart = 43360;

	private const int k_HangulJameExtendedEnd = 43391;

	private const int k_HangulSyllablesStart = 44032;

	private const int k_HangulSyllablesEnd = 55295;

	private const int k_CjkIdeographsStart = 63744;

	private const int k_CjkIdeographsEnd = 64255;

	private const int k_CjkFormsStart = 65072;

	private const int k_CjkFormsEnd = 65103;

	private const int k_CjkHalfwidthStart = 65280;

	private const int k_CjkHalfwidthEnd = 65519;

	private const int k_EndOfText = 3;

	private const float k_FloatUnset = -32767f;

	private const int k_MaxCharacters = 8;

	private static TextGenerator s_TextGenerator;

	private TextBackingContainer m_TextBackingArray = new TextBackingContainer(4);

	internal TextProcessingElement[] m_TextProcessingArray = new TextProcessingElement[8];

	internal int m_InternalTextProcessingArraySize;

	[SerializeField]
	protected bool m_VertexBufferAutoSizeReduction = false;

	private char[] m_HtmlTag = new char[128];

	internal HighlightState m_HighlightState = new HighlightState(Color.white, Offset.zero);

	protected bool m_IsIgnoringAlignment;

	protected static bool m_IsTextTruncated;

	private Vector3[] m_RectTransformCorners = new Vector3[4];

	private float m_MarginWidth;

	private float m_MarginHeight;

	private float m_PreferredWidth;

	private float m_PreferredHeight;

	private FontAsset m_CurrentFontAsset;

	private Material m_CurrentMaterial;

	private int m_CurrentMaterialIndex;

	private TextProcessingStack<MaterialReference> m_MaterialReferenceStack = new TextProcessingStack<MaterialReference>(new MaterialReference[16]);

	private float m_Padding;

	private SpriteAsset m_CurrentSpriteAsset;

	private int m_TotalCharacterCount;

	private float m_FontSize;

	private float m_FontScaleMultiplier;

	private float m_CurrentFontSize;

	private TextProcessingStack<float> m_SizeStack = new TextProcessingStack<float>(16);

	protected TextProcessingStack<int>[] m_TextStyleStacks = new TextProcessingStack<int>[8];

	protected int m_TextStyleStackDepth = 0;

	private FontStyles m_FontStyleInternal = FontStyles.Normal;

	private FontStyleStack m_FontStyleStack;

	private TextFontWeight m_FontWeightInternal = TextFontWeight.Regular;

	private TextProcessingStack<TextFontWeight> m_FontWeightStack = new TextProcessingStack<TextFontWeight>(8);

	private TextAlignment m_LineJustification;

	private TextProcessingStack<TextAlignment> m_LineJustificationStack = new TextProcessingStack<TextAlignment>(16);

	private float m_BaselineOffset;

	private TextProcessingStack<float> m_BaselineOffsetStack = new TextProcessingStack<float>(new float[16]);

	private Color32 m_FontColor32;

	private Color32 m_HtmlColor;

	private Color32 m_UnderlineColor;

	private Color32 m_StrikethroughColor;

	private TextProcessingStack<Color32> m_ColorStack = new TextProcessingStack<Color32>(new Color32[16]);

	private TextProcessingStack<Color32> m_UnderlineColorStack = new TextProcessingStack<Color32>(new Color32[16]);

	private TextProcessingStack<Color32> m_StrikethroughColorStack = new TextProcessingStack<Color32>(new Color32[16]);

	private TextProcessingStack<Color32> m_HighlightColorStack = new TextProcessingStack<Color32>(new Color32[16]);

	private TextProcessingStack<HighlightState> m_HighlightStateStack = new TextProcessingStack<HighlightState>(new HighlightState[16]);

	private TextProcessingStack<int> m_ItalicAngleStack = new TextProcessingStack<int>(new int[16]);

	private TextColorGradient m_ColorGradientPreset;

	private TextProcessingStack<TextColorGradient> m_ColorGradientStack = new TextProcessingStack<TextColorGradient>(new TextColorGradient[16]);

	private bool m_ColorGradientPresetIsTinted;

	private TextProcessingStack<int> m_ActionStack = new TextProcessingStack<int>(new int[16]);

	private float m_LineOffset;

	private float m_LineHeight;

	private bool m_IsDrivenLineSpacing;

	private float m_CSpacing;

	private float m_MonoSpacing;

	private float m_XAdvance;

	private float m_TagLineIndent;

	private float m_TagIndent;

	private TextProcessingStack<float> m_IndentStack = new TextProcessingStack<float>(new float[16]);

	private bool m_TagNoParsing;

	private int m_CharacterCount;

	private int m_FirstCharacterOfLine;

	private int m_LastCharacterOfLine;

	private int m_FirstVisibleCharacterOfLine;

	private int m_LastVisibleCharacterOfLine;

	private float m_MaxLineAscender;

	private float m_MaxLineDescender;

	private int m_LineNumber;

	private int m_LineVisibleCharacterCount;

	private int m_LineVisibleSpaceCount;

	private int m_FirstOverflowCharacterIndex;

	private int m_PageNumber;

	private float m_MarginLeft;

	private float m_MarginRight;

	private float m_Width;

	private Extents m_MeshExtents;

	private float m_MaxCapHeight;

	private float m_MaxAscender;

	private float m_MaxDescender;

	private bool m_IsNewPage;

	private bool m_IsNonBreakingSpace;

	private WordWrapState m_SavedWordWrapState;

	private WordWrapState m_SavedLineState;

	private WordWrapState m_SavedEllipsisState = default(WordWrapState);

	private WordWrapState m_SavedLastValidState = default(WordWrapState);

	private WordWrapState m_SavedSoftLineBreakState = default(WordWrapState);

	private TextElementType m_TextElementType;

	private bool m_isTextLayoutPhase;

	private int m_SpriteIndex;

	private Color32 m_SpriteColor;

	private TextElement m_CachedTextElement;

	private Color32 m_HighlightColor;

	private float m_CharWidthAdjDelta;

	private float m_MaxFontSize;

	private float m_MinFontSize;

	private int m_AutoSizeIterationCount;

	private int m_AutoSizeMaxIterationCount = 100;

	private bool m_IsAutoSizePointSizeSet;

	private float m_StartOfLineAscender;

	private float m_LineSpacingDelta;

	private MaterialReference[] m_MaterialReferences = new MaterialReference[8];

	private int m_SpriteCount = 0;

	private TextProcessingStack<int> m_StyleStack = new TextProcessingStack<int>(new int[16]);

	private TextProcessingStack<WordWrapState> m_EllipsisInsertionCandidateStack = new TextProcessingStack<WordWrapState>(8, 8);

	private int m_SpriteAnimationId;

	private int m_ItalicAngle;

	private Vector3 m_FXScale;

	private Quaternion m_FXRotation;

	private int m_LastBaseGlyphIndex;

	private float m_PageAscender;

	private RichTextTagAttribute[] m_XmlAttribute = new RichTextTagAttribute[8];

	private float[] m_AttributeParameterValues = new float[16];

	private Dictionary<int, int> m_MaterialReferenceIndexLookup = new Dictionary<int, int>();

	private bool m_IsCalculatingPreferredValues;

	private SpriteAsset m_DefaultSpriteAsset;

	private bool m_TintSprite;

	protected SpecialCharacter m_Ellipsis;

	protected SpecialCharacter m_Underline;

	private TextElementInfo[] m_InternalTextElementInfo;

	private bool vertexBufferAutoSizeReduction
	{
		get
		{
			return m_VertexBufferAutoSizeReduction;
		}
		set
		{
			m_VertexBufferAutoSizeReduction = value;
		}
	}

	public static bool isTextTruncated => m_IsTextTruncated;

	public static event MissingCharacterEventCallback OnMissingCharacter;

	private static TextGenerator GetTextGenerator()
	{
		if (s_TextGenerator == null)
		{
			s_TextGenerator = new TextGenerator();
		}
		return s_TextGenerator;
	}

	public static void GenerateText(TextGenerationSettings settings, TextInfo textInfo)
	{
		if (settings.fontAsset == null || settings.fontAsset.characterLookupTable == null)
		{
			Debug.LogWarning("Can't Generate Mesh, No Font Asset has been assigned.");
			return;
		}
		if (textInfo == null)
		{
			Debug.LogError("Null TextInfo provided to TextGenerator. Cannot update its content.");
			return;
		}
		TextGenerator textGenerator = GetTextGenerator();
		textGenerator.Prepare(settings, textInfo);
		FontAsset.UpdateFontAssetsInUpdateQueue();
		textGenerator.GenerateTextMesh(settings, textInfo);
	}

	public static Vector2 GetCursorPosition(TextGenerationSettings settings, int index)
	{
		if (settings.fontAsset == null || settings.fontAsset.characterLookupTable == null)
		{
			Debug.LogWarning("Can't Generate Mesh, No Font Asset has been assigned.");
			return Vector2.zero;
		}
		TextInfo textInfo = new TextInfo();
		GenerateText(settings, textInfo);
		return GetCursorPosition(textInfo, settings.screenRect, index);
	}

	public static Vector2 GetCursorPosition(TextInfo textInfo, Rect screenRect, int index, bool inverseYAxis = true)
	{
		Vector2 position = screenRect.position;
		if (textInfo.characterCount == 0)
		{
			return position;
		}
		TextElementInfo textElementInfo = textInfo.textElementInfo[textInfo.characterCount - 1];
		LineInfo lineInfo = textInfo.lineInfo[textElementInfo.lineNumber];
		float num = lineInfo.lineHeight - (lineInfo.ascender - lineInfo.descender);
		if (index >= textInfo.characterCount)
		{
			return position + (inverseYAxis ? new Vector2(textElementInfo.xAdvance, screenRect.height - lineInfo.ascender - num) : new Vector2(textElementInfo.xAdvance, lineInfo.descender));
		}
		textElementInfo = textInfo.textElementInfo[index];
		lineInfo = textInfo.lineInfo[textElementInfo.lineNumber];
		num = lineInfo.lineHeight - (lineInfo.ascender - lineInfo.descender);
		return position + (inverseYAxis ? new Vector2(textElementInfo.origin, screenRect.height - lineInfo.ascender - num) : new Vector2(textElementInfo.origin, lineInfo.descender));
	}

	public static float GetPreferredWidth(TextGenerationSettings settings, TextInfo textInfo)
	{
		if (settings.fontAsset == null || settings.fontAsset.characterLookupTable == null)
		{
			Debug.LogWarning("Can't Generate Mesh, No Font Asset has been assigned.");
			return 0f;
		}
		TextGenerator textGenerator = GetTextGenerator();
		textGenerator.Prepare(settings, textInfo);
		return textGenerator.GetPreferredWidthInternal(settings, textInfo);
	}

	public static float GetPreferredHeight(TextGenerationSettings settings, TextInfo textInfo)
	{
		if (settings.fontAsset == null || settings.fontAsset.characterLookupTable == null)
		{
			Debug.LogWarning("Can't Generate Mesh, No Font Asset has been assigned.");
			return 0f;
		}
		TextGenerator textGenerator = GetTextGenerator();
		textGenerator.Prepare(settings, textInfo);
		return textGenerator.GetPreferredHeightInternal(settings, textInfo);
	}

	public static Vector2 GetPreferredValues(TextGenerationSettings settings, TextInfo textInfo)
	{
		if (settings.fontAsset == null || settings.fontAsset.characterLookupTable == null)
		{
			Debug.LogWarning("Can't Generate Mesh, No Font Asset has been assigned.");
			return Vector2.zero;
		}
		TextGenerator textGenerator = GetTextGenerator();
		textGenerator.Prepare(settings, textInfo);
		return textGenerator.GetPreferredValuesInternal(settings, textInfo);
	}

	private void Prepare(TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		m_Padding = generationSettings.extraPadding;
		m_FontStyleInternal = generationSettings.fontStyle;
		m_FontWeightInternal = (((m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? TextFontWeight.Bold : generationSettings.fontWeight);
		GetSpecialCharacters(generationSettings);
		ComputeMarginSize(generationSettings.screenRect, generationSettings.margins);
		PopulateTextBackingArray(generationSettings.text);
		PopulateTextProcessingArray(generationSettings);
		SetArraySizes(m_TextProcessingArray, generationSettings, textInfo);
		if (generationSettings.autoSize)
		{
			m_FontSize = Mathf.Clamp(generationSettings.fontSize, generationSettings.fontSizeMin, generationSettings.fontSizeMax);
		}
		else
		{
			m_FontSize = generationSettings.fontSize;
		}
		m_MaxFontSize = generationSettings.fontSizeMax;
		m_MinFontSize = generationSettings.fontSizeMin;
		m_LineSpacingDelta = 0f;
		m_CharWidthAdjDelta = 0f;
	}

	private void GenerateTextMesh(TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		if (generationSettings.fontAsset == null || generationSettings.fontAsset.characterLookupTable == null)
		{
			Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned.");
			m_IsAutoSizePointSizeSet = true;
			return;
		}
		textInfo?.Clear();
		if (m_TextProcessingArray == null || m_TextProcessingArray.Length == 0 || m_TextProcessingArray[0].unicode == 0)
		{
			ClearMesh(updateMesh: true, textInfo);
			m_PreferredWidth = 0f;
			m_PreferredHeight = 0f;
			m_IsAutoSizePointSizeSet = true;
			return;
		}
		m_CurrentFontAsset = generationSettings.fontAsset;
		m_CurrentMaterial = generationSettings.material;
		m_CurrentMaterialIndex = 0;
		m_MaterialReferenceStack.SetDefault(new MaterialReference(m_CurrentMaterialIndex, m_CurrentFontAsset, null, m_CurrentMaterial, m_Padding));
		m_CurrentSpriteAsset = generationSettings.spriteAsset;
		int totalCharacterCount = m_TotalCharacterCount;
		float num = m_FontSize / (float)generationSettings.fontAsset.m_FaceInfo.pointSize * generationSettings.fontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
		float num2 = num;
		float num3 = m_FontSize * 0.01f * (generationSettings.isOrthographic ? 1f : 0.1f);
		m_FontScaleMultiplier = 1f;
		m_CurrentFontSize = m_FontSize;
		m_SizeStack.SetDefault(m_CurrentFontSize);
		float num4 = 0f;
		uint num5 = 0u;
		m_FontStyleInternal = generationSettings.fontStyle;
		m_FontWeightInternal = (((m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? TextFontWeight.Bold : generationSettings.fontWeight);
		m_FontWeightStack.SetDefault(m_FontWeightInternal);
		m_FontStyleStack.Clear();
		m_LineJustification = generationSettings.textAlignment;
		m_LineJustificationStack.SetDefault(m_LineJustification);
		float num6 = 0f;
		m_BaselineOffset = 0f;
		m_BaselineOffsetStack.Clear();
		bool flag = false;
		Vector3 start = Vector3.zero;
		Vector3 zero = Vector3.zero;
		bool flag2 = false;
		Vector3 start2 = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		bool flag3 = false;
		Vector3 start3 = Vector3.zero;
		Vector3 end = Vector3.zero;
		m_FontColor32 = generationSettings.color;
		m_HtmlColor = m_FontColor32;
		m_UnderlineColor = m_HtmlColor;
		m_StrikethroughColor = m_HtmlColor;
		m_ColorStack.SetDefault(m_HtmlColor);
		m_UnderlineColorStack.SetDefault(m_HtmlColor);
		m_StrikethroughColorStack.SetDefault(m_HtmlColor);
		m_HighlightStateStack.SetDefault(new HighlightState(m_HtmlColor, Offset.zero));
		m_ColorGradientPreset = null;
		m_ColorGradientStack.SetDefault(null);
		m_ItalicAngle = m_CurrentFontAsset.italicStyleSlant;
		m_ItalicAngleStack.SetDefault(m_ItalicAngle);
		m_ActionStack.Clear();
		m_FXScale = Vector3.one;
		m_FXRotation = Quaternion.identity;
		m_LineOffset = 0f;
		m_LineHeight = -32767f;
		float num7 = m_CurrentFontAsset.faceInfo.lineHeight - (m_CurrentFontAsset.m_FaceInfo.ascentLine - m_CurrentFontAsset.m_FaceInfo.descentLine);
		m_CSpacing = 0f;
		m_MonoSpacing = 0f;
		m_XAdvance = 0f;
		m_TagLineIndent = 0f;
		m_TagIndent = 0f;
		m_IndentStack.SetDefault(0f);
		m_TagNoParsing = false;
		m_CharacterCount = 0;
		m_FirstCharacterOfLine = 0;
		m_LastCharacterOfLine = 0;
		m_FirstVisibleCharacterOfLine = 0;
		m_LastVisibleCharacterOfLine = 0;
		m_MaxLineAscender = -32767f;
		m_MaxLineDescender = 32767f;
		m_LineNumber = 0;
		m_StartOfLineAscender = 0f;
		m_LineVisibleCharacterCount = 0;
		m_LineVisibleSpaceCount = 0;
		bool flag4 = true;
		m_IsDrivenLineSpacing = false;
		m_FirstOverflowCharacterIndex = -1;
		m_LastBaseGlyphIndex = int.MinValue;
		m_PageNumber = 0;
		int num8 = Mathf.Clamp(generationSettings.pageToDisplay - 1, 0, textInfo.pageInfo.Length - 1);
		textInfo.ClearPageInfo();
		Vector4 margins = generationSettings.margins;
		float num9 = ((m_MarginWidth > 0f) ? m_MarginWidth : 0f);
		float num10 = ((m_MarginHeight > 0f) ? m_MarginHeight : 0f);
		m_MarginLeft = 0f;
		m_MarginRight = 0f;
		m_Width = -1f;
		float num11 = num9 + 0.0001f - m_MarginLeft - m_MarginRight;
		m_MeshExtents.min = TextGeneratorUtilities.largePositiveVector2;
		m_MeshExtents.max = TextGeneratorUtilities.largeNegativeVector2;
		textInfo.ClearLineInfo();
		m_MaxCapHeight = 0f;
		m_MaxAscender = 0f;
		m_MaxDescender = 0f;
		m_PageAscender = 0f;
		float maxVisibleDescender = 0f;
		bool isMaxVisibleDescenderSet = false;
		m_IsNewPage = false;
		bool flag5 = true;
		m_IsNonBreakingSpace = false;
		bool flag6 = false;
		int num12 = 0;
		CharacterSubstitution characterSubstitution = new CharacterSubstitution(-1, 0u);
		bool flag7 = false;
		TextWrappingMode textWrappingMode = (generationSettings.wordWrap ? TextWrappingMode.Normal : TextWrappingMode.NoWrap);
		SaveWordWrappingState(ref m_SavedWordWrapState, -1, -1, textInfo);
		SaveWordWrappingState(ref m_SavedLineState, -1, -1, textInfo);
		SaveWordWrappingState(ref m_SavedEllipsisState, -1, -1, textInfo);
		SaveWordWrappingState(ref m_SavedLastValidState, -1, -1, textInfo);
		SaveWordWrappingState(ref m_SavedSoftLineBreakState, -1, -1, textInfo);
		m_EllipsisInsertionCandidateStack.Clear();
		m_IsTextTruncated = false;
		TextSettings textSettings = generationSettings.textSettings;
		int num13 = 0;
		Vector3 vector = default(Vector3);
		Vector3 vector2 = default(Vector3);
		Vector3 vector3 = default(Vector3);
		Vector3 vector4 = default(Vector3);
		for (int i = 0; i < m_TextProcessingArray.Length && m_TextProcessingArray[i].unicode != 0; i++)
		{
			num5 = m_TextProcessingArray[i].unicode;
			if (num13 > 5)
			{
				Debug.LogError("Line breaking recursion max threshold hit... Character [" + num5 + "] index: " + i);
				characterSubstitution.index = m_CharacterCount;
				characterSubstitution.unicode = 3u;
			}
			if (num5 == 26)
			{
				continue;
			}
			if (generationSettings.richText && num5 == 60)
			{
				m_isTextLayoutPhase = true;
				m_TextElementType = TextElementType.Character;
				if (ValidateHtmlTag(m_TextProcessingArray, i + 1, out var endIndex, generationSettings, textInfo))
				{
					i = endIndex;
					if (m_TextElementType == TextElementType.Character)
					{
						continue;
					}
				}
			}
			else
			{
				m_TextElementType = textInfo.textElementInfo[m_CharacterCount].elementType;
				m_CurrentMaterialIndex = textInfo.textElementInfo[m_CharacterCount].materialReferenceIndex;
				m_CurrentFontAsset = textInfo.textElementInfo[m_CharacterCount].fontAsset;
			}
			int currentMaterialIndex = m_CurrentMaterialIndex;
			bool isUsingAlternateTypeface = textInfo.textElementInfo[m_CharacterCount].isUsingAlternateTypeface;
			m_isTextLayoutPhase = false;
			bool flag8 = false;
			if (characterSubstitution.index == m_CharacterCount)
			{
				num5 = characterSubstitution.unicode;
				m_TextElementType = TextElementType.Character;
				flag8 = true;
				switch (num5)
				{
				case 3u:
					textInfo.textElementInfo[m_CharacterCount].textElement = m_CurrentFontAsset.characterLookupTable[3u];
					m_IsTextTruncated = true;
					break;
				case 8230u:
					textInfo.textElementInfo[m_CharacterCount].textElement = m_Ellipsis.character;
					textInfo.textElementInfo[m_CharacterCount].elementType = TextElementType.Character;
					textInfo.textElementInfo[m_CharacterCount].fontAsset = m_Ellipsis.fontAsset;
					textInfo.textElementInfo[m_CharacterCount].material = m_Ellipsis.material;
					textInfo.textElementInfo[m_CharacterCount].materialReferenceIndex = m_Ellipsis.materialIndex;
					m_IsTextTruncated = true;
					characterSubstitution.index = m_CharacterCount + 1;
					characterSubstitution.unicode = 3u;
					break;
				}
			}
			if (m_CharacterCount < generationSettings.firstVisibleCharacter && num5 != 3)
			{
				textInfo.textElementInfo[m_CharacterCount].isVisible = false;
				textInfo.textElementInfo[m_CharacterCount].character = '\u200b';
				textInfo.textElementInfo[m_CharacterCount].lineNumber = 0;
				m_CharacterCount++;
				continue;
			}
			float num14 = 1f;
			if (m_TextElementType == TextElementType.Character)
			{
				if ((m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
				{
					if (char.IsLower((char)num5))
					{
						num5 = char.ToUpper((char)num5);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
				{
					if (char.IsUpper((char)num5))
					{
						num5 = char.ToLower((char)num5);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num5))
				{
					num14 = 0.8f;
					num5 = char.ToUpper((char)num5);
				}
			}
			float num15 = 0f;
			float num16 = 0f;
			float num17 = 0f;
			if (m_TextElementType == TextElementType.Sprite)
			{
				SpriteCharacter spriteCharacter = (SpriteCharacter)textInfo.textElementInfo[m_CharacterCount].textElement;
				m_CurrentSpriteAsset = spriteCharacter.textAsset as SpriteAsset;
				m_SpriteIndex = (int)spriteCharacter.glyphIndex;
				if (spriteCharacter == null)
				{
					continue;
				}
				if (num5 == 60)
				{
					num5 = (uint)(57344 + m_SpriteIndex);
				}
				else
				{
					m_SpriteColor = Color.white;
				}
				float num18 = m_CurrentFontSize / (float)m_CurrentFontAsset.faceInfo.pointSize * m_CurrentFontAsset.faceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
				if (m_CurrentSpriteAsset.m_FaceInfo.pointSize > 0)
				{
					float num19 = m_CurrentFontSize / (float)m_CurrentSpriteAsset.m_FaceInfo.pointSize * m_CurrentSpriteAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
					num2 = spriteCharacter.m_Scale * spriteCharacter.m_Glyph.scale * num19;
					num16 = m_CurrentSpriteAsset.m_FaceInfo.ascentLine;
					num15 = m_CurrentSpriteAsset.m_FaceInfo.baseline * num18 * m_FontScaleMultiplier * m_CurrentSpriteAsset.m_FaceInfo.scale;
					num17 = m_CurrentSpriteAsset.m_FaceInfo.descentLine;
				}
				else
				{
					float num20 = m_CurrentFontSize / (float)m_CurrentFontAsset.m_FaceInfo.pointSize * m_CurrentFontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
					num2 = m_CurrentFontAsset.m_FaceInfo.ascentLine / spriteCharacter.m_Glyph.metrics.height * spriteCharacter.m_Scale * spriteCharacter.m_Glyph.scale * num20;
					float num21 = num20 / num2;
					num16 = m_CurrentFontAsset.m_FaceInfo.ascentLine * num21;
					num15 = m_CurrentFontAsset.m_FaceInfo.baseline * num18 * m_FontScaleMultiplier * m_CurrentFontAsset.m_FaceInfo.scale;
					num17 = m_CurrentFontAsset.m_FaceInfo.descentLine * num21;
				}
				m_CachedTextElement = spriteCharacter;
				textInfo.textElementInfo[m_CharacterCount].elementType = TextElementType.Sprite;
				textInfo.textElementInfo[m_CharacterCount].scale = num2;
				textInfo.textElementInfo[m_CharacterCount].spriteAsset = m_CurrentSpriteAsset;
				textInfo.textElementInfo[m_CharacterCount].fontAsset = m_CurrentFontAsset;
				textInfo.textElementInfo[m_CharacterCount].materialReferenceIndex = m_CurrentMaterialIndex;
				m_CurrentMaterialIndex = currentMaterialIndex;
				num6 = 0f;
			}
			else if (m_TextElementType == TextElementType.Character)
			{
				m_CachedTextElement = textInfo.textElementInfo[m_CharacterCount].textElement;
				if (m_CachedTextElement == null)
				{
					continue;
				}
				m_CurrentFontAsset = textInfo.textElementInfo[m_CharacterCount].fontAsset;
				m_CurrentMaterial = textInfo.textElementInfo[m_CharacterCount].material;
				m_CurrentMaterialIndex = textInfo.textElementInfo[m_CharacterCount].materialReferenceIndex;
				float num22 = ((!flag8 || m_TextProcessingArray[i].unicode != 10 || m_CharacterCount == m_FirstCharacterOfLine) ? (m_CurrentFontSize * num14 / (float)m_CurrentFontAsset.m_FaceInfo.pointSize * m_CurrentFontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f)) : (textInfo.textElementInfo[m_CharacterCount - 1].pointSize * num14 / (float)m_CurrentFontAsset.m_FaceInfo.pointSize * m_CurrentFontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f)));
				if (flag8 && num5 == 8230)
				{
					num16 = 0f;
					num17 = 0f;
				}
				else
				{
					num16 = m_CurrentFontAsset.m_FaceInfo.ascentLine;
					num17 = m_CurrentFontAsset.m_FaceInfo.descentLine;
				}
				num2 = num22 * m_FontScaleMultiplier * m_CachedTextElement.m_Scale * m_CachedTextElement.m_Glyph.scale;
				num15 = m_CurrentFontAsset.m_FaceInfo.baseline * num22 * m_FontScaleMultiplier * m_CurrentFontAsset.m_FaceInfo.scale;
				textInfo.textElementInfo[m_CharacterCount].elementType = TextElementType.Character;
				textInfo.textElementInfo[m_CharacterCount].scale = num2;
				num6 = m_Padding;
			}
			float num23 = num2;
			if (num5 == 173 || num5 == 3)
			{
				num2 = 0f;
			}
			textInfo.textElementInfo[m_CharacterCount].character = (char)num5;
			textInfo.textElementInfo[m_CharacterCount].pointSize = m_CurrentFontSize;
			textInfo.textElementInfo[m_CharacterCount].color = m_HtmlColor;
			textInfo.textElementInfo[m_CharacterCount].underlineColor = m_UnderlineColor;
			textInfo.textElementInfo[m_CharacterCount].strikethroughColor = m_StrikethroughColor;
			textInfo.textElementInfo[m_CharacterCount].highlightState = m_HighlightState;
			textInfo.textElementInfo[m_CharacterCount].style = m_FontStyleInternal;
			GlyphMetrics glyphMetrics = textInfo.textElementInfo[m_CharacterCount].alternativeGlyph?.metrics ?? m_CachedTextElement.m_Glyph.metrics;
			bool flag9 = num5 <= 65535 && char.IsWhiteSpace((char)num5);
			GlyphValueRecord glyphValueRecord = default(GlyphValueRecord);
			float num24 = generationSettings.characterSpacing;
			if (generationSettings.enableKerning)
			{
				uint glyphIndex = m_CachedTextElement.m_GlyphIndex;
				GlyphPairAdjustmentRecord value;
				if (m_CharacterCount < totalCharacterCount - 1)
				{
					uint glyphIndex2 = textInfo.textElementInfo[m_CharacterCount + 1].textElement.m_GlyphIndex;
					uint key = (glyphIndex2 << 16) | glyphIndex;
					if (m_CurrentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key, out value))
					{
						glyphValueRecord = value.firstAdjustmentRecord.glyphValueRecord;
						num24 = (((value.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num24);
					}
				}
				if (m_CharacterCount >= 1)
				{
					uint glyphIndex3 = textInfo.textElementInfo[m_CharacterCount - 1].textElement.m_GlyphIndex;
					uint key2 = (glyphIndex << 16) | glyphIndex3;
					if (m_CurrentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key2, out value))
					{
						glyphValueRecord += value.secondAdjustmentRecord.glyphValueRecord;
						num24 = (((value.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num24);
					}
				}
			}
			textInfo.textElementInfo[m_CharacterCount].adjustedHorizontalAdvance = glyphValueRecord.xAdvance;
			bool flag10 = TextGeneratorUtilities.IsBaseGlyph(num5);
			if (flag10)
			{
				m_LastBaseGlyphIndex = m_CharacterCount;
			}
			if (m_CharacterCount > 0 && !flag10)
			{
				if (m_LastBaseGlyphIndex != int.MinValue && m_LastBaseGlyphIndex == m_CharacterCount - 1)
				{
					Glyph glyph = textInfo.textElementInfo[m_LastBaseGlyphIndex].textElement.glyph;
					uint index = glyph.index;
					uint glyphIndex4 = m_CachedTextElement.glyphIndex;
					uint key3 = (glyphIndex4 << 16) | index;
					if (m_CurrentFontAsset.fontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryGetValue(key3, out var value2))
					{
						float num25 = (textInfo.textElementInfo[m_LastBaseGlyphIndex].origin - m_XAdvance) / num2;
						glyphValueRecord.xPlacement = num25 + value2.baseGlyphAnchorPoint.xCoordinate - value2.markPositionAdjustment.xPositionAdjustment;
						glyphValueRecord.yPlacement = value2.baseGlyphAnchorPoint.yCoordinate - value2.markPositionAdjustment.yPositionAdjustment;
						num24 = 0f;
					}
				}
				else
				{
					bool flag11 = false;
					for (int j = m_CharacterCount - 1; j >= 0 && j != m_LastBaseGlyphIndex; j--)
					{
						Glyph glyph2 = textInfo.textElementInfo[j].textElement.glyph;
						uint index2 = glyph2.index;
						uint glyphIndex5 = m_CachedTextElement.glyphIndex;
						uint key4 = (glyphIndex5 << 16) | index2;
						if (m_CurrentFontAsset.fontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.TryGetValue(key4, out var value3))
						{
							float num26 = (textInfo.textElementInfo[j].origin - m_XAdvance) / num2;
							float num27 = num15 - m_LineOffset + m_BaselineOffset;
							float num28 = (textInfo.textElementInfo[j].baseLine - num27) / num2;
							glyphValueRecord.xPlacement = num26 + value3.baseMarkGlyphAnchorPoint.xCoordinate - value3.combiningMarkPositionAdjustment.xPositionAdjustment;
							glyphValueRecord.yPlacement = num28 + value3.baseMarkGlyphAnchorPoint.yCoordinate - value3.combiningMarkPositionAdjustment.yPositionAdjustment;
							num24 = 0f;
							flag11 = true;
							break;
						}
					}
					if (m_LastBaseGlyphIndex != int.MinValue && !flag11)
					{
						Glyph glyph3 = textInfo.textElementInfo[m_LastBaseGlyphIndex].textElement.glyph;
						uint index3 = glyph3.index;
						uint glyphIndex6 = m_CachedTextElement.glyphIndex;
						uint key5 = (glyphIndex6 << 16) | index3;
						if (m_CurrentFontAsset.fontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryGetValue(key5, out var value4))
						{
							float num29 = (textInfo.textElementInfo[m_LastBaseGlyphIndex].origin - m_XAdvance) / num2;
							glyphValueRecord.xPlacement = num29 + value4.baseGlyphAnchorPoint.xCoordinate - value4.markPositionAdjustment.xPositionAdjustment;
							glyphValueRecord.yPlacement = value4.baseGlyphAnchorPoint.yCoordinate - value4.markPositionAdjustment.yPositionAdjustment;
							num24 = 0f;
						}
					}
				}
			}
			num16 += glyphValueRecord.yPlacement;
			num17 += glyphValueRecord.yPlacement;
			if (generationSettings.isRightToLeft)
			{
				m_XAdvance -= glyphMetrics.horizontalAdvance * (1f - m_CharWidthAdjDelta) * num2;
				if (flag9 || num5 == 8203)
				{
					m_XAdvance -= generationSettings.wordSpacing * num3;
				}
			}
			float num30 = 0f;
			if (m_MonoSpacing != 0f)
			{
				num30 = (m_MonoSpacing / 2f - (glyphMetrics.width / 2f + glyphMetrics.horizontalBearingX) * num2) * (1f - m_CharWidthAdjDelta);
				m_XAdvance += num30;
			}
			float num32;
			float num33;
			if (m_TextElementType == TextElementType.Character && !isUsingAlternateTypeface && (m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
			{
				if (m_CurrentMaterial != null && m_CurrentMaterial.HasProperty(TextShaderUtilities.ID_GradientScale))
				{
					float num31 = m_CurrentMaterial.GetFloat(TextShaderUtilities.ID_GradientScale);
					num32 = m_CurrentFontAsset.boldStyleWeight / 4f * num31 * m_CurrentMaterial.GetFloat(TextShaderUtilities.ID_ScaleRatio_A);
					if (num32 + num6 > num31)
					{
						num6 = num31 - num32;
					}
				}
				else
				{
					num32 = 0f;
				}
				num33 = m_CurrentFontAsset.boldStyleSpacing;
			}
			else
			{
				if (m_CurrentMaterial != null && m_CurrentMaterial.HasProperty(TextShaderUtilities.ID_GradientScale) && m_CurrentMaterial.HasProperty(TextShaderUtilities.ID_ScaleRatio_A))
				{
					float num34 = m_CurrentMaterial.GetFloat(TextShaderUtilities.ID_GradientScale);
					num32 = m_CurrentFontAsset.m_RegularStyleWeight / 4f * num34 * m_CurrentMaterial.GetFloat(TextShaderUtilities.ID_ScaleRatio_A);
					if (num32 + num6 > num34)
					{
						num6 = num34 - num32;
					}
				}
				else
				{
					num32 = 0f;
				}
				num33 = 0f;
			}
			vector.x = m_XAdvance + (glyphMetrics.horizontalBearingX * m_FXScale.x - num6 - num32 + glyphValueRecord.xPlacement) * num2 * (1f - m_CharWidthAdjDelta);
			vector.y = num15 + (glyphMetrics.horizontalBearingY + num6 + glyphValueRecord.yPlacement) * num2 - m_LineOffset + m_BaselineOffset;
			vector.z = 0f;
			vector2.x = vector.x;
			vector2.y = vector.y - (glyphMetrics.height + num6 * 2f) * num2;
			vector2.z = 0f;
			vector3.x = vector2.x + (glyphMetrics.width * m_FXScale.x + num6 * 2f + num32 * 2f) * num2 * (1f - m_CharWidthAdjDelta);
			vector3.y = vector.y;
			vector3.z = 0f;
			vector4.x = vector3.x;
			vector4.y = vector2.y;
			vector4.z = 0f;
			if (m_TextElementType == TextElementType.Character && !isUsingAlternateTypeface && (m_FontStyleInternal & FontStyles.Italic) == FontStyles.Italic)
			{
				float num35 = (float)m_ItalicAngle * 0.01f;
				float num36 = (m_CurrentFontAsset.m_FaceInfo.capLine - (m_CurrentFontAsset.m_FaceInfo.baseline + m_BaselineOffset)) / 2f * m_FontScaleMultiplier * m_CurrentFontAsset.m_FaceInfo.scale;
				Vector3 vector5 = new Vector3(num35 * ((glyphMetrics.horizontalBearingY + num6 + num32 - num36) * num2), 0f, 0f);
				Vector3 vector6 = new Vector3(num35 * ((glyphMetrics.horizontalBearingY - glyphMetrics.height - num6 - num32 - num36) * num2), 0f, 0f);
				vector += vector5;
				vector2 += vector6;
				vector3 += vector5;
				vector4 += vector6;
			}
			if (m_FXRotation != Quaternion.identity)
			{
				Matrix4x4 matrix4x = Matrix4x4.Rotate(m_FXRotation);
				Vector3 vector7 = (vector3 + vector2) / 2f;
				vector = matrix4x.MultiplyPoint3x4(vector - vector7) + vector7;
				vector2 = matrix4x.MultiplyPoint3x4(vector2 - vector7) + vector7;
				vector3 = matrix4x.MultiplyPoint3x4(vector3 - vector7) + vector7;
				vector4 = matrix4x.MultiplyPoint3x4(vector4 - vector7) + vector7;
			}
			textInfo.textElementInfo[m_CharacterCount].bottomLeft = vector2;
			textInfo.textElementInfo[m_CharacterCount].topLeft = vector;
			textInfo.textElementInfo[m_CharacterCount].topRight = vector3;
			textInfo.textElementInfo[m_CharacterCount].bottomRight = vector4;
			textInfo.textElementInfo[m_CharacterCount].origin = m_XAdvance + glyphValueRecord.xPlacement * num2;
			textInfo.textElementInfo[m_CharacterCount].baseLine = num15 - m_LineOffset + m_BaselineOffset + glyphValueRecord.yPlacement * num2;
			textInfo.textElementInfo[m_CharacterCount].aspectRatio = (vector3.x - vector2.x) / (vector.y - vector2.y);
			float num37 = ((m_TextElementType == TextElementType.Character) ? (num16 * num2 / num14 + m_BaselineOffset) : (num16 * num2 + m_BaselineOffset));
			float num38 = ((m_TextElementType == TextElementType.Character) ? (num17 * num2 / num14 + m_BaselineOffset) : (num17 * num2 + m_BaselineOffset));
			float num39 = num37;
			float num40 = num38;
			bool flag12 = m_CharacterCount == m_FirstCharacterOfLine;
			if (flag12 || !flag9)
			{
				if (m_BaselineOffset != 0f)
				{
					num39 = Mathf.Max((num37 - m_BaselineOffset) / m_FontScaleMultiplier, num39);
					num40 = Mathf.Min((num38 - m_BaselineOffset) / m_FontScaleMultiplier, num40);
				}
				m_MaxLineAscender = Mathf.Max(num39, m_MaxLineAscender);
				m_MaxLineDescender = Mathf.Min(num40, m_MaxLineDescender);
			}
			if (flag12 || !flag9)
			{
				textInfo.textElementInfo[m_CharacterCount].adjustedAscender = num39;
				textInfo.textElementInfo[m_CharacterCount].adjustedDescender = num40;
				textInfo.textElementInfo[m_CharacterCount].ascender = num37 - m_LineOffset;
				m_MaxDescender = (textInfo.textElementInfo[m_CharacterCount].descender = num38 - m_LineOffset);
			}
			else
			{
				textInfo.textElementInfo[m_CharacterCount].adjustedAscender = m_MaxLineAscender;
				textInfo.textElementInfo[m_CharacterCount].adjustedDescender = m_MaxLineDescender;
				textInfo.textElementInfo[m_CharacterCount].ascender = m_MaxLineAscender - m_LineOffset;
				m_MaxDescender = (textInfo.textElementInfo[m_CharacterCount].descender = m_MaxLineDescender - m_LineOffset);
			}
			if ((m_LineNumber == 0 || m_IsNewPage) && (flag12 || !flag9))
			{
				m_MaxAscender = m_MaxLineAscender;
				m_MaxCapHeight = Mathf.Max(m_MaxCapHeight, m_CurrentFontAsset.m_FaceInfo.capLine * num2 / num14);
			}
			if (m_LineOffset == 0f && (flag12 || !flag9))
			{
				m_PageAscender = ((m_PageAscender > num37) ? m_PageAscender : num37);
			}
			textInfo.textElementInfo[m_CharacterCount].isVisible = false;
			bool flag13 = (m_LineJustification & (TextAlignment)16) == (TextAlignment)16 || (m_LineJustification & (TextAlignment)8) == (TextAlignment)8;
			if (num5 == 9 || ((textWrappingMode == TextWrappingMode.PreserveWhitespace || textWrappingMode == TextWrappingMode.PreserveWhitespaceNoWrap) && (flag9 || num5 == 8203)) || (!flag9 && num5 != 8203 && num5 != 173 && num5 != 3) || (num5 == 173 && !flag7) || m_TextElementType == TextElementType.Sprite)
			{
				textInfo.textElementInfo[m_CharacterCount].isVisible = true;
				float marginLeft = m_MarginLeft;
				float marginRight = m_MarginRight;
				if (flag8)
				{
					marginLeft = textInfo.lineInfo[m_LineNumber].marginLeft;
					marginRight = textInfo.lineInfo[m_LineNumber].marginRight;
				}
				num11 = ((m_Width != -1f) ? Mathf.Min(num9 + 0.0001f - marginLeft - marginRight, m_Width) : (num9 + 0.0001f - marginLeft - marginRight));
				float num41 = Mathf.Abs(m_XAdvance) + ((!generationSettings.isRightToLeft) ? glyphMetrics.horizontalAdvance : 0f) * (1f - m_CharWidthAdjDelta) * ((num5 == 173) ? num23 : num2);
				float num42 = m_MaxAscender - (m_MaxLineDescender - m_LineOffset) + ((m_LineOffset > 0f && !m_IsDrivenLineSpacing) ? (m_MaxLineAscender - m_StartOfLineAscender) : 0f);
				int characterCount = m_CharacterCount;
				if (num42 > num10 + 0.0001f)
				{
					if (m_FirstOverflowCharacterIndex == -1)
					{
						m_FirstOverflowCharacterIndex = m_CharacterCount;
					}
					if (generationSettings.autoSize)
					{
						if (m_LineSpacingDelta > generationSettings.lineSpacingMax && m_LineOffset > 0f && m_AutoSizeIterationCount < m_AutoSizeMaxIterationCount)
						{
							float num43 = (num10 - num42) / (float)m_LineNumber;
							m_LineSpacingDelta = Mathf.Max(m_LineSpacingDelta + num43 / num, generationSettings.lineSpacingMax);
							return;
						}
						if (m_FontSize > generationSettings.fontSizeMin && m_AutoSizeIterationCount < m_AutoSizeMaxIterationCount)
						{
							m_MaxFontSize = m_FontSize;
							float num44 = Mathf.Max((m_FontSize - m_MinFontSize) / 2f, 0.05f);
							m_FontSize -= num44;
							m_FontSize = Mathf.Max((float)(int)(m_FontSize * 20f + 0.5f) / 20f, generationSettings.fontSizeMin);
							return;
						}
					}
					switch (generationSettings.overflowMode)
					{
					case TextOverflowMode.Truncate:
						i = RestoreWordWrappingState(ref m_SavedLastValidState, textInfo);
						characterSubstitution.index = characterCount;
						continue;
					case TextOverflowMode.Ellipsis:
						if (m_LineNumber > 0)
						{
							if (m_EllipsisInsertionCandidateStack.Count == 0)
							{
								i = -1;
								m_CharacterCount = 0;
								characterSubstitution.index = 0;
								characterSubstitution.unicode = 3u;
								m_FirstCharacterOfLine = 0;
							}
							else
							{
								WordWrapState state = m_EllipsisInsertionCandidateStack.Pop();
								i = RestoreWordWrappingState(ref state, textInfo);
								i--;
								m_CharacterCount--;
								characterSubstitution.index = m_CharacterCount;
								characterSubstitution.unicode = 8230u;
								num13++;
							}
							continue;
						}
						break;
					case TextOverflowMode.Linked:
						i = RestoreWordWrappingState(ref m_SavedLastValidState, textInfo);
						characterSubstitution.index = characterCount;
						characterSubstitution.unicode = 3u;
						continue;
					case TextOverflowMode.Page:
						if (i < 0 || characterCount == 0)
						{
							i = -1;
							m_CharacterCount = 0;
							characterSubstitution.index = 0;
							characterSubstitution.unicode = 3u;
							continue;
						}
						if (m_MaxLineAscender - m_MaxLineDescender > num10 + 0.0001f)
						{
							i = RestoreWordWrappingState(ref m_SavedLineState, textInfo);
							characterSubstitution.index = characterCount;
							characterSubstitution.unicode = 3u;
							continue;
						}
						i = RestoreWordWrappingState(ref m_SavedLineState, textInfo);
						m_IsNewPage = true;
						m_FirstCharacterOfLine = m_CharacterCount;
						m_MaxLineAscender = -32767f;
						m_MaxLineDescender = 32767f;
						m_StartOfLineAscender = 0f;
						m_XAdvance = 0f + m_TagIndent;
						m_LineOffset = 0f;
						m_MaxAscender = 0f;
						m_PageAscender = 0f;
						m_LineNumber++;
						m_PageNumber++;
						continue;
					}
				}
				if (flag10 && num41 > num11 * (flag13 ? 1.05f : 1f))
				{
					if (textWrappingMode != TextWrappingMode.NoWrap && textWrappingMode != TextWrappingMode.PreserveWhitespaceNoWrap && m_CharacterCount != m_FirstCharacterOfLine)
					{
						i = RestoreWordWrappingState(ref m_SavedWordWrapState, textInfo);
						float num45 = 0f;
						if (m_LineHeight == -32767f)
						{
							float adjustedAscender = textInfo.textElementInfo[m_CharacterCount].adjustedAscender;
							num45 = ((m_LineOffset > 0f && !m_IsDrivenLineSpacing) ? (m_MaxLineAscender - m_StartOfLineAscender) : 0f) - m_MaxLineDescender + adjustedAscender + (num7 + m_LineSpacingDelta) * num + generationSettings.lineSpacing * num3;
						}
						else
						{
							num45 = m_LineHeight + generationSettings.lineSpacing * num3;
							m_IsDrivenLineSpacing = true;
						}
						float num46 = m_MaxAscender + num45 + m_LineOffset - textInfo.textElementInfo[m_CharacterCount].adjustedDescender;
						if (textInfo.textElementInfo[m_CharacterCount - 1].character == '\u00ad' && !flag7 && (generationSettings.overflowMode == TextOverflowMode.Overflow || num46 < num10 + 0.0001f))
						{
							characterSubstitution.index = m_CharacterCount - 1;
							characterSubstitution.unicode = 45u;
							i--;
							m_CharacterCount--;
							continue;
						}
						flag7 = false;
						if (textInfo.textElementInfo[m_CharacterCount].character == '\u00ad')
						{
							flag7 = true;
							continue;
						}
						if (generationSettings.autoSize && flag5)
						{
							if (m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f && m_AutoSizeIterationCount < m_AutoSizeMaxIterationCount)
							{
								float num47 = num41;
								if (m_CharWidthAdjDelta > 0f)
								{
									num47 /= 1f - m_CharWidthAdjDelta;
								}
								float num48 = num41 - (num11 - 0.0001f) * (flag13 ? 1.05f : 1f);
								m_CharWidthAdjDelta += num48 / num47;
								m_CharWidthAdjDelta = Mathf.Min(m_CharWidthAdjDelta, generationSettings.charWidthMaxAdj / 100f);
								return;
							}
							if (m_FontSize > generationSettings.fontSizeMin && m_AutoSizeIterationCount < m_AutoSizeMaxIterationCount)
							{
								m_MaxFontSize = m_FontSize;
								float num49 = Mathf.Max((m_FontSize - m_MinFontSize) / 2f, 0.05f);
								m_FontSize -= num49;
								m_FontSize = Mathf.Max((float)(int)(m_FontSize * 20f + 0.5f) / 20f, generationSettings.fontSizeMin);
								return;
							}
						}
						int previousWordBreak = m_SavedSoftLineBreakState.previousWordBreak;
						if (flag5 && previousWordBreak != -1 && previousWordBreak != num12)
						{
							i = RestoreWordWrappingState(ref m_SavedSoftLineBreakState, textInfo);
							num12 = previousWordBreak;
							if (textInfo.textElementInfo[m_CharacterCount - 1].character == '\u00ad')
							{
								characterSubstitution.index = m_CharacterCount - 1;
								characterSubstitution.unicode = 45u;
								i--;
								m_CharacterCount--;
								continue;
							}
						}
						if (!(num46 > num10 + 0.0001f))
						{
							InsertNewLine(i, num, num2, num3, num33, num24, num11, num7, ref isMaxVisibleDescenderSet, ref maxVisibleDescender, generationSettings, textInfo);
							flag4 = true;
							flag5 = true;
							continue;
						}
						if (m_FirstOverflowCharacterIndex == -1)
						{
							m_FirstOverflowCharacterIndex = m_CharacterCount;
						}
						if (generationSettings.autoSize)
						{
							if (m_LineSpacingDelta > generationSettings.lineSpacingMax && m_AutoSizeIterationCount < m_AutoSizeMaxIterationCount)
							{
								float num50 = (num10 - num46) / (float)(m_LineNumber + 1);
								m_LineSpacingDelta = Mathf.Max(m_LineSpacingDelta + num50 / num, generationSettings.lineSpacingMax);
								return;
							}
							if (m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f && m_AutoSizeIterationCount < m_AutoSizeMaxIterationCount)
							{
								float num51 = num41;
								if (m_CharWidthAdjDelta > 0f)
								{
									num51 /= 1f - m_CharWidthAdjDelta;
								}
								float num52 = num41 - (num11 - 0.0001f) * (flag13 ? 1.05f : 1f);
								m_CharWidthAdjDelta += num52 / num51;
								m_CharWidthAdjDelta = Mathf.Min(m_CharWidthAdjDelta, generationSettings.charWidthMaxAdj / 100f);
								return;
							}
							if (m_FontSize > generationSettings.fontSizeMin && m_AutoSizeIterationCount < m_AutoSizeMaxIterationCount)
							{
								m_MaxFontSize = m_FontSize;
								float num53 = Mathf.Max((m_FontSize - m_MinFontSize) / 2f, 0.05f);
								m_FontSize -= num53;
								m_FontSize = Mathf.Max((float)(int)(m_FontSize * 20f + 0.5f) / 20f, generationSettings.fontSizeMin);
								return;
							}
						}
						switch (generationSettings.overflowMode)
						{
						case TextOverflowMode.Overflow:
						case TextOverflowMode.Masking:
						case TextOverflowMode.ScrollRect:
							InsertNewLine(i, num, num2, num3, num33, num24, num11, num7, ref isMaxVisibleDescenderSet, ref maxVisibleDescender, generationSettings, textInfo);
							flag4 = true;
							flag5 = true;
							continue;
						case TextOverflowMode.Truncate:
							i = RestoreWordWrappingState(ref m_SavedLastValidState, textInfo);
							characterSubstitution.index = characterCount;
							characterSubstitution.unicode = 3u;
							continue;
						case TextOverflowMode.Ellipsis:
							if (m_EllipsisInsertionCandidateStack.Count == 0)
							{
								i = -1;
								m_CharacterCount = 0;
								characterSubstitution.index = 0;
								characterSubstitution.unicode = 3u;
								m_FirstCharacterOfLine = 0;
							}
							else
							{
								WordWrapState state2 = m_EllipsisInsertionCandidateStack.Pop();
								i = RestoreWordWrappingState(ref state2, textInfo);
								i--;
								m_CharacterCount--;
								characterSubstitution.index = m_CharacterCount;
								characterSubstitution.unicode = 8230u;
								num13++;
							}
							continue;
						case TextOverflowMode.Linked:
							characterSubstitution.index = m_CharacterCount;
							characterSubstitution.unicode = 3u;
							continue;
						case TextOverflowMode.Page:
							m_IsNewPage = true;
							InsertNewLine(i, num, num2, num3, num33, num24, num11, num7, ref isMaxVisibleDescenderSet, ref maxVisibleDescender, generationSettings, textInfo);
							m_StartOfLineAscender = 0f;
							m_LineOffset = 0f;
							m_MaxAscender = 0f;
							m_PageAscender = 0f;
							m_PageNumber++;
							flag4 = true;
							flag5 = true;
							continue;
						}
					}
					else
					{
						if (generationSettings.autoSize && m_AutoSizeIterationCount < m_AutoSizeMaxIterationCount)
						{
							if (m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f)
							{
								float num54 = num41;
								if (m_CharWidthAdjDelta > 0f)
								{
									num54 /= 1f - m_CharWidthAdjDelta;
								}
								float num55 = num41 - (num11 - 0.0001f) * (flag13 ? 1.05f : 1f);
								m_CharWidthAdjDelta += num55 / num54;
								m_CharWidthAdjDelta = Mathf.Min(m_CharWidthAdjDelta, generationSettings.charWidthMaxAdj / 100f);
								return;
							}
							if (m_FontSize > generationSettings.fontSizeMin)
							{
								m_MaxFontSize = m_FontSize;
								float num56 = Mathf.Max((m_FontSize - m_MinFontSize) / 2f, 0.05f);
								m_FontSize -= num56;
								m_FontSize = Mathf.Max((float)(int)(m_FontSize * 20f + 0.5f) / 20f, generationSettings.fontSizeMin);
								return;
							}
						}
						switch (generationSettings.overflowMode)
						{
						case TextOverflowMode.Truncate:
							i = RestoreWordWrappingState(ref m_SavedWordWrapState, textInfo);
							characterSubstitution.index = characterCount;
							characterSubstitution.unicode = 3u;
							continue;
						case TextOverflowMode.Ellipsis:
							if (m_EllipsisInsertionCandidateStack.Count == 0)
							{
								i = -1;
								m_CharacterCount = 0;
								characterSubstitution.index = 0;
								characterSubstitution.unicode = 3u;
								m_FirstCharacterOfLine = 0;
							}
							else
							{
								WordWrapState state3 = m_EllipsisInsertionCandidateStack.Pop();
								i = RestoreWordWrappingState(ref state3, textInfo);
								i--;
								m_CharacterCount--;
								characterSubstitution.index = m_CharacterCount;
								characterSubstitution.unicode = 8230u;
								num13++;
							}
							continue;
						case TextOverflowMode.Linked:
							i = RestoreWordWrappingState(ref m_SavedWordWrapState, textInfo);
							characterSubstitution.index = m_CharacterCount;
							characterSubstitution.unicode = 3u;
							continue;
						}
					}
				}
				if (flag9)
				{
					textInfo.textElementInfo[m_CharacterCount].isVisible = false;
					m_LastVisibleCharacterOfLine = m_CharacterCount;
					m_LineVisibleSpaceCount = ++textInfo.lineInfo[m_LineNumber].spaceCount;
					textInfo.lineInfo[m_LineNumber].marginLeft = marginLeft;
					textInfo.lineInfo[m_LineNumber].marginRight = marginRight;
					TextInfo textInfo2 = textInfo;
					textInfo2.spaceCount++;
				}
				else if (num5 == 173)
				{
					textInfo.textElementInfo[m_CharacterCount].isVisible = false;
				}
				else
				{
					Color32 vertexColor = ((!generationSettings.overrideRichTextColors) ? m_HtmlColor : m_FontColor32);
					if (m_TextElementType == TextElementType.Character)
					{
						SaveGlyphVertexInfo(num6, num32, vertexColor, generationSettings, textInfo);
					}
					else if (m_TextElementType == TextElementType.Sprite)
					{
						SaveSpriteVertexInfo(vertexColor, generationSettings, textInfo);
					}
					if (flag4)
					{
						flag4 = false;
						m_FirstVisibleCharacterOfLine = m_CharacterCount;
					}
					m_LineVisibleCharacterCount++;
					m_LastVisibleCharacterOfLine = m_CharacterCount;
					textInfo.lineInfo[m_LineNumber].marginLeft = marginLeft;
					textInfo.lineInfo[m_LineNumber].marginRight = marginRight;
				}
			}
			else
			{
				if (generationSettings.overflowMode == TextOverflowMode.Linked && (num5 == 10 || num5 == 11))
				{
					float num57 = m_MaxAscender - (m_MaxLineDescender - m_LineOffset) + ((m_LineOffset > 0f && !m_IsDrivenLineSpacing) ? (m_MaxLineAscender - m_StartOfLineAscender) : 0f);
					int characterCount2 = m_CharacterCount;
					if (num57 > num10 + 0.0001f)
					{
						if (m_FirstOverflowCharacterIndex == -1)
						{
							m_FirstOverflowCharacterIndex = m_CharacterCount;
						}
						i = RestoreWordWrappingState(ref m_SavedLastValidState, textInfo);
						characterSubstitution.index = characterCount2;
						characterSubstitution.unicode = 3u;
						continue;
					}
				}
				if ((num5 == 10 || num5 == 11 || num5 == 160 || num5 == 8199 || num5 == 8232 || num5 == 8233 || char.IsSeparator((char)num5)) && num5 != 173 && num5 != 8203 && num5 != 8288)
				{
					textInfo.lineInfo[m_LineNumber].spaceCount++;
					TextInfo textInfo2 = textInfo;
					textInfo2.spaceCount++;
				}
				if (num5 == 160)
				{
					textInfo.lineInfo[m_LineNumber].controlCharacterCount++;
				}
			}
			if (generationSettings.overflowMode == TextOverflowMode.Ellipsis && (!flag8 || num5 == 45))
			{
				float num58 = m_CurrentFontSize / (float)m_Ellipsis.fontAsset.m_FaceInfo.pointSize * m_Ellipsis.fontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
				float num59 = num58 * m_FontScaleMultiplier * m_Ellipsis.character.m_Scale * m_Ellipsis.character.m_Glyph.scale;
				float marginLeft2 = m_MarginLeft;
				float marginRight2 = m_MarginRight;
				if (num5 == 10 && m_CharacterCount != m_FirstCharacterOfLine)
				{
					num58 = textInfo.textElementInfo[m_CharacterCount - 1].pointSize / (float)m_Ellipsis.fontAsset.m_FaceInfo.pointSize * m_Ellipsis.fontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
					num59 = num58 * m_FontScaleMultiplier * m_Ellipsis.character.m_Scale * m_Ellipsis.character.m_Glyph.scale;
					marginLeft2 = textInfo.lineInfo[m_LineNumber].marginLeft;
					marginRight2 = textInfo.lineInfo[m_LineNumber].marginRight;
				}
				float num60 = m_MaxAscender - (m_MaxLineDescender - m_LineOffset) + ((m_LineOffset > 0f && !m_IsDrivenLineSpacing) ? (m_MaxLineAscender - m_StartOfLineAscender) : 0f);
				float num61 = Mathf.Abs(m_XAdvance) + ((!generationSettings.isRightToLeft) ? m_Ellipsis.character.m_Glyph.metrics.horizontalAdvance : 0f) * (1f - m_CharWidthAdjDelta) * num59;
				float num62 = ((m_Width != -1f) ? Mathf.Min(num9 + 0.0001f - marginLeft2 - marginRight2, m_Width) : (num9 + 0.0001f - marginLeft2 - marginRight2));
				if (num61 < num62 * (flag13 ? 1.05f : 1f))
				{
					SaveWordWrappingState(ref m_SavedEllipsisState, i, m_CharacterCount, textInfo);
					m_EllipsisInsertionCandidateStack.Push(m_SavedEllipsisState);
				}
			}
			textInfo.textElementInfo[m_CharacterCount].lineNumber = m_LineNumber;
			textInfo.textElementInfo[m_CharacterCount].pageNumber = m_PageNumber;
			if ((num5 != 10 && num5 != 11 && num5 != 13 && !flag8) || textInfo.lineInfo[m_LineNumber].characterCount == 1)
			{
				textInfo.lineInfo[m_LineNumber].alignment = m_LineJustification;
			}
			switch (num5)
			{
			case 9u:
			{
				float num63 = m_CurrentFontAsset.m_FaceInfo.tabWidth * (float)(int)m_CurrentFontAsset.tabMultiple * num2;
				float num64 = Mathf.Ceil(m_XAdvance / num63) * num63;
				m_XAdvance = ((num64 > m_XAdvance) ? num64 : (m_XAdvance + num63));
				break;
			}
			default:
				if (m_MonoSpacing != 0f)
				{
					m_XAdvance += (m_MonoSpacing - num30 + (m_CurrentFontAsset.regularStyleSpacing + num24) * num3 + m_CSpacing) * (1f - m_CharWidthAdjDelta);
					if (flag9 || num5 == 8203)
					{
						m_XAdvance += generationSettings.wordSpacing * num3;
					}
				}
				else if (generationSettings.isRightToLeft)
				{
					m_XAdvance -= (glyphValueRecord.xAdvance * num2 + (m_CurrentFontAsset.regularStyleSpacing + num24 + num33) * num3 + m_CSpacing) * (1f - m_CharWidthAdjDelta);
					if (flag9 || num5 == 8203)
					{
						m_XAdvance -= generationSettings.wordSpacing * num3;
					}
				}
				else
				{
					m_XAdvance += ((glyphMetrics.horizontalAdvance * m_FXScale.x + glyphValueRecord.xAdvance) * num2 + (m_CurrentFontAsset.regularStyleSpacing + num24 + num33) * num3 + m_CSpacing) * (1f - m_CharWidthAdjDelta);
					if (flag9 || num5 == 8203)
					{
						m_XAdvance += generationSettings.wordSpacing * num3;
					}
				}
				break;
			case 8203u:
				break;
			}
			textInfo.textElementInfo[m_CharacterCount].xAdvance = m_XAdvance;
			if (num5 == 13)
			{
				m_XAdvance = 0f + m_TagIndent;
			}
			if (generationSettings.overflowMode == TextOverflowMode.Page && num5 != 10 && num5 != 11 && num5 != 13 && num5 != 8232 && num5 != 8233)
			{
				if (m_PageNumber + 1 > textInfo.pageInfo.Length)
				{
					TextInfo.Resize(ref textInfo.pageInfo, m_PageNumber + 1, isBlockAllocated: true);
				}
				textInfo.pageInfo[m_PageNumber].ascender = m_PageAscender;
				textInfo.pageInfo[m_PageNumber].descender = ((m_MaxDescender < textInfo.pageInfo[m_PageNumber].descender) ? m_MaxDescender : textInfo.pageInfo[m_PageNumber].descender);
				if (m_IsNewPage)
				{
					m_IsNewPage = false;
					textInfo.pageInfo[m_PageNumber].firstCharacterIndex = m_CharacterCount;
				}
				textInfo.pageInfo[m_PageNumber].lastCharacterIndex = m_CharacterCount;
			}
			if (num5 == 10 || num5 == 11 || num5 == 3 || num5 == 8232 || num5 == 8233 || (num5 == 45 && flag8) || m_CharacterCount == totalCharacterCount - 1)
			{
				float num65 = m_MaxLineAscender - m_StartOfLineAscender;
				if (m_LineOffset > 0f && Math.Abs(num65) > 0.01f && !m_IsDrivenLineSpacing && !m_IsNewPage)
				{
					TextGeneratorUtilities.AdjustLineOffset(m_FirstCharacterOfLine, m_CharacterCount, num65, textInfo);
					m_MaxDescender -= num65;
					m_LineOffset += num65;
					if (m_SavedEllipsisState.lineNumber == m_LineNumber)
					{
						m_SavedEllipsisState = m_EllipsisInsertionCandidateStack.Pop();
						m_SavedEllipsisState.startOfLineAscender += num65;
						m_SavedEllipsisState.lineOffset += num65;
						m_EllipsisInsertionCandidateStack.Push(m_SavedEllipsisState);
					}
				}
				m_IsNewPage = false;
				float num66 = m_MaxLineAscender - m_LineOffset;
				float num67 = m_MaxLineDescender - m_LineOffset;
				m_MaxDescender = ((m_MaxDescender < num67) ? m_MaxDescender : num67);
				if (!isMaxVisibleDescenderSet)
				{
					maxVisibleDescender = m_MaxDescender;
				}
				if (generationSettings.useMaxVisibleDescender && (m_CharacterCount >= generationSettings.maxVisibleCharacters || m_LineNumber >= generationSettings.maxVisibleLines))
				{
					isMaxVisibleDescenderSet = true;
				}
				textInfo.lineInfo[m_LineNumber].firstCharacterIndex = m_FirstCharacterOfLine;
				textInfo.lineInfo[m_LineNumber].firstVisibleCharacterIndex = (m_FirstVisibleCharacterOfLine = ((m_FirstCharacterOfLine > m_FirstVisibleCharacterOfLine) ? m_FirstCharacterOfLine : m_FirstVisibleCharacterOfLine));
				textInfo.lineInfo[m_LineNumber].lastCharacterIndex = (m_LastCharacterOfLine = m_CharacterCount);
				textInfo.lineInfo[m_LineNumber].lastVisibleCharacterIndex = (m_LastVisibleCharacterOfLine = ((m_LastVisibleCharacterOfLine < m_FirstVisibleCharacterOfLine) ? m_FirstVisibleCharacterOfLine : m_LastVisibleCharacterOfLine));
				textInfo.lineInfo[m_LineNumber].characterCount = textInfo.lineInfo[m_LineNumber].lastCharacterIndex - textInfo.lineInfo[m_LineNumber].firstCharacterIndex + 1;
				textInfo.lineInfo[m_LineNumber].visibleCharacterCount = m_LineVisibleCharacterCount;
				textInfo.lineInfo[m_LineNumber].visibleSpaceCount = m_LineVisibleSpaceCount;
				textInfo.lineInfo[m_LineNumber].lineExtents.min = new Vector2(textInfo.textElementInfo[m_FirstVisibleCharacterOfLine].bottomLeft.x, num67);
				textInfo.lineInfo[m_LineNumber].lineExtents.max = new Vector2(textInfo.textElementInfo[m_LastVisibleCharacterOfLine].topRight.x, num66);
				textInfo.lineInfo[m_LineNumber].length = textInfo.lineInfo[m_LineNumber].lineExtents.max.x - num6 * num2;
				textInfo.lineInfo[m_LineNumber].width = num11;
				if (textInfo.lineInfo[m_LineNumber].characterCount == 1)
				{
					textInfo.lineInfo[m_LineNumber].alignment = m_LineJustification;
				}
				float num68 = ((m_CurrentFontAsset.regularStyleSpacing + num24 + num33) * num3 + m_CSpacing) * (1f - m_CharWidthAdjDelta);
				if (textInfo.textElementInfo[m_LastVisibleCharacterOfLine].isVisible)
				{
					textInfo.lineInfo[m_LineNumber].maxAdvance = textInfo.textElementInfo[m_LastVisibleCharacterOfLine].xAdvance + (generationSettings.isRightToLeft ? num68 : (0f - num68));
				}
				else
				{
					textInfo.lineInfo[m_LineNumber].maxAdvance = textInfo.textElementInfo[m_LastCharacterOfLine].xAdvance + (generationSettings.isRightToLeft ? num68 : (0f - num68));
				}
				textInfo.lineInfo[m_LineNumber].baseline = 0f - m_LineOffset;
				textInfo.lineInfo[m_LineNumber].ascender = num66;
				textInfo.lineInfo[m_LineNumber].descender = num67;
				textInfo.lineInfo[m_LineNumber].lineHeight = num66 - num67 + num7 * num;
				if (num5 == 10 || num5 == 11 || num5 == 45 || num5 == 8232 || num5 == 8233)
				{
					SaveWordWrappingState(ref m_SavedLineState, i, m_CharacterCount, textInfo);
					m_LineNumber++;
					flag4 = true;
					flag6 = false;
					flag5 = true;
					m_FirstCharacterOfLine = m_CharacterCount + 1;
					m_LineVisibleCharacterCount = 0;
					m_LineVisibleSpaceCount = 0;
					if (m_LineNumber >= textInfo.lineInfo.Length)
					{
						TextGeneratorUtilities.ResizeLineExtents(m_LineNumber, textInfo);
					}
					float adjustedAscender2 = textInfo.textElementInfo[m_CharacterCount].adjustedAscender;
					if (m_LineHeight == -32767f)
					{
						float num69 = 0f - m_MaxLineDescender + adjustedAscender2 + (num7 + m_LineSpacingDelta) * num + (generationSettings.lineSpacing + ((num5 == 10 || num5 == 8233) ? generationSettings.paragraphSpacing : 0f)) * num3;
						m_LineOffset += num69;
						m_IsDrivenLineSpacing = false;
					}
					else
					{
						m_LineOffset += m_LineHeight + (generationSettings.lineSpacing + ((num5 == 10 || num5 == 8233) ? generationSettings.paragraphSpacing : 0f)) * num3;
						m_IsDrivenLineSpacing = true;
					}
					m_MaxLineAscender = -32767f;
					m_MaxLineDescender = 32767f;
					m_StartOfLineAscender = adjustedAscender2;
					m_XAdvance = 0f + m_TagLineIndent + m_TagIndent;
					SaveWordWrappingState(ref m_SavedWordWrapState, i, m_CharacterCount, textInfo);
					SaveWordWrappingState(ref m_SavedLastValidState, i, m_CharacterCount, textInfo);
					m_CharacterCount++;
					continue;
				}
				if (num5 == 3)
				{
					i = m_TextProcessingArray.Length;
				}
			}
			if (textInfo.textElementInfo[m_CharacterCount].isVisible)
			{
				m_MeshExtents.min.x = Mathf.Min(m_MeshExtents.min.x, textInfo.textElementInfo[m_CharacterCount].bottomLeft.x);
				m_MeshExtents.min.y = Mathf.Min(m_MeshExtents.min.y, textInfo.textElementInfo[m_CharacterCount].bottomLeft.y);
				m_MeshExtents.max.x = Mathf.Max(m_MeshExtents.max.x, textInfo.textElementInfo[m_CharacterCount].topRight.x);
				m_MeshExtents.max.y = Mathf.Max(m_MeshExtents.max.y, textInfo.textElementInfo[m_CharacterCount].topRight.y);
			}
			if ((textWrappingMode != TextWrappingMode.NoWrap && textWrappingMode != TextWrappingMode.PreserveWhitespaceNoWrap) || generationSettings.overflowMode == TextOverflowMode.Truncate || generationSettings.overflowMode == TextOverflowMode.Ellipsis || generationSettings.overflowMode == TextOverflowMode.Linked)
			{
				if ((flag9 || num5 == 8203 || num5 == 45 || num5 == 173) && (!m_IsNonBreakingSpace || flag6) && num5 != 160 && num5 != 8199 && num5 != 8209 && num5 != 8239 && num5 != 8288)
				{
					SaveWordWrappingState(ref m_SavedWordWrapState, i, m_CharacterCount, textInfo);
					flag5 = false;
					m_SavedSoftLineBreakState.previousWordBreak = -1;
				}
				else if (!m_IsNonBreakingSpace && ((TextGeneratorUtilities.IsHangul(num5) && !textSettings.lineBreakingRules.useModernHangulLineBreakingRules) || TextGeneratorUtilities.IsCJK(num5)))
				{
					bool flag14 = textSettings.lineBreakingRules.leadingCharactersLookup.Contains(num5);
					bool flag15 = m_CharacterCount < totalCharacterCount - 1 && textSettings.lineBreakingRules.followingCharactersLookup.Contains(textInfo.textElementInfo[m_CharacterCount + 1].character);
					if (!flag14)
					{
						if (!flag15)
						{
							SaveWordWrappingState(ref m_SavedWordWrapState, i, m_CharacterCount, textInfo);
							flag5 = false;
						}
						if (flag5)
						{
							if (flag9)
							{
								SaveWordWrappingState(ref m_SavedSoftLineBreakState, i, m_CharacterCount, textInfo);
							}
							SaveWordWrappingState(ref m_SavedWordWrapState, i, m_CharacterCount, textInfo);
						}
					}
					else if (flag5 && flag12)
					{
						if (flag9)
						{
							SaveWordWrappingState(ref m_SavedSoftLineBreakState, i, m_CharacterCount, textInfo);
						}
						SaveWordWrappingState(ref m_SavedWordWrapState, i, m_CharacterCount, textInfo);
					}
				}
				else if (flag5)
				{
					if ((flag9 && num5 != 160) || (num5 == 173 && !flag7))
					{
						SaveWordWrappingState(ref m_SavedSoftLineBreakState, i, m_CharacterCount, textInfo);
					}
					SaveWordWrappingState(ref m_SavedWordWrapState, i, m_CharacterCount, textInfo);
				}
			}
			SaveWordWrappingState(ref m_SavedLastValidState, i, m_CharacterCount, textInfo);
			m_CharacterCount++;
		}
		num4 = m_MaxFontSize - m_MinFontSize;
		if (generationSettings.autoSize && num4 > 0.051f && m_FontSize < generationSettings.fontSizeMax && m_AutoSizeIterationCount < m_AutoSizeMaxIterationCount)
		{
			if (m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f)
			{
				m_CharWidthAdjDelta = 0f;
			}
			m_MinFontSize = m_FontSize;
			float num70 = Mathf.Max((m_MaxFontSize - m_FontSize) / 2f, 0.05f);
			m_FontSize += num70;
			m_FontSize = Mathf.Min((float)(int)(m_FontSize * 20f + 0.5f) / 20f, generationSettings.charWidthMaxAdj);
			return;
		}
		m_IsAutoSizePointSizeSet = true;
		if (m_AutoSizeIterationCount >= m_AutoSizeMaxIterationCount)
		{
			Debug.Log("Auto Size Iteration Count: " + m_AutoSizeIterationCount + ". Final Point Size: " + m_FontSize);
		}
		if (m_CharacterCount == 0 || (m_CharacterCount == 1 && num5 == 3))
		{
			ClearMesh(updateMesh: true, textInfo);
			return;
		}
		textInfo.meshInfo[m_CurrentMaterialIndex].Clear(uploadChanges: false);
		Vector3 vector8 = Vector3.zero;
		Vector3[] rectTransformCorners = m_RectTransformCorners;
		switch (generationSettings.textAlignment)
		{
		case TextAlignment.TopLeft:
		case TextAlignment.TopCenter:
		case TextAlignment.TopRight:
		case TextAlignment.TopJustified:
		case TextAlignment.TopFlush:
		case TextAlignment.TopGeoAligned:
			vector8 = ((generationSettings.overflowMode == TextOverflowMode.Page) ? (rectTransformCorners[1] + new Vector3(0f + margins.x, 0f - textInfo.pageInfo[num8].ascender - margins.y, 0f)) : (rectTransformCorners[1] + new Vector3(0f + margins.x, 0f - m_MaxAscender - margins.y, 0f)));
			break;
		case TextAlignment.MiddleLeft:
		case TextAlignment.MiddleCenter:
		case TextAlignment.MiddleRight:
		case TextAlignment.MiddleJustified:
		case TextAlignment.MiddleFlush:
		case TextAlignment.MiddleGeoAligned:
			vector8 = ((generationSettings.overflowMode == TextOverflowMode.Page) ? ((rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margins.x, 0f - (textInfo.pageInfo[num8].ascender + margins.y + textInfo.pageInfo[num8].descender - margins.w) / 2f, 0f)) : ((rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margins.x, 0f - (m_MaxAscender + margins.y + maxVisibleDescender - margins.w) / 2f, 0f)));
			break;
		case TextAlignment.BottomLeft:
		case TextAlignment.BottomCenter:
		case TextAlignment.BottomRight:
		case TextAlignment.BottomJustified:
		case TextAlignment.BottomFlush:
		case TextAlignment.BottomGeoAligned:
			vector8 = ((generationSettings.overflowMode == TextOverflowMode.Page) ? (rectTransformCorners[0] + new Vector3(0f + margins.x, 0f - textInfo.pageInfo[num8].descender + margins.w, 0f)) : (rectTransformCorners[0] + new Vector3(0f + margins.x, 0f - maxVisibleDescender + margins.w, 0f)));
			break;
		case TextAlignment.BaselineLeft:
		case TextAlignment.BaselineCenter:
		case TextAlignment.BaselineRight:
		case TextAlignment.BaselineJustified:
		case TextAlignment.BaselineFlush:
		case TextAlignment.BaselineGeoAligned:
			vector8 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margins.x, 0f, 0f);
			break;
		case TextAlignment.MidlineLeft:
		case TextAlignment.MidlineCenter:
		case TextAlignment.MidlineRight:
		case TextAlignment.MidlineJustified:
		case TextAlignment.MidlineFlush:
		case TextAlignment.MidlineGeoAligned:
			vector8 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margins.x, 0f - (m_MeshExtents.max.y + margins.y + m_MeshExtents.min.y - margins.w) / 2f, 0f);
			break;
		case TextAlignment.CaplineLeft:
		case TextAlignment.CaplineCenter:
		case TextAlignment.CaplineRight:
		case TextAlignment.CaplineJustified:
		case TextAlignment.CaplineFlush:
		case TextAlignment.CaplineGeoAligned:
			vector8 = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(0f + margins.x, 0f - (m_MaxCapHeight - margins.y - margins.w) / 2f, 0f);
			break;
		}
		Vector3 vector9 = Vector3.zero;
		Vector3 zero3 = Vector3.zero;
		int num71 = 0;
		int lineCount = 0;
		int num72 = 0;
		bool flag16 = false;
		bool flag17 = false;
		int num73 = 0;
		int num74 = 0;
		Color32 color = Color.white;
		Color32 underlineColor = Color.white;
		HighlightState highlightState = new HighlightState(new Color32(byte.MaxValue, byte.MaxValue, 0, 64), Offset.zero);
		float num75 = 0f;
		float num76 = 0f;
		float num77 = 0f;
		float num78 = 0f;
		float num79 = 0f;
		float num80 = 32767f;
		int num81 = 0;
		float num82 = 0f;
		float num83 = 0f;
		float b = 0f;
		TextElementInfo[] textElementInfo = textInfo.textElementInfo;
		for (int k = 0; k < m_CharacterCount; k++)
		{
			FontAsset fontAsset = textElementInfo[k].fontAsset;
			char character = textElementInfo[k].character;
			bool flag18 = char.IsWhiteSpace(character);
			int lineNumber = textElementInfo[k].lineNumber;
			LineInfo lineInfo = textInfo.lineInfo[lineNumber];
			lineCount = lineNumber + 1;
			TextAlignment alignment = lineInfo.alignment;
			switch (alignment)
			{
			case TextAlignment.TopLeft:
			case TextAlignment.MiddleLeft:
			case TextAlignment.BottomLeft:
			case TextAlignment.BaselineLeft:
			case TextAlignment.MidlineLeft:
			case TextAlignment.CaplineLeft:
				vector9 = (generationSettings.isRightToLeft ? new Vector3(0f - lineInfo.maxAdvance, 0f, 0f) : new Vector3(0f + lineInfo.marginLeft, 0f, 0f));
				break;
			case TextAlignment.TopCenter:
			case TextAlignment.MiddleCenter:
			case TextAlignment.BottomCenter:
			case TextAlignment.BaselineCenter:
			case TextAlignment.MidlineCenter:
			case TextAlignment.CaplineCenter:
				vector9 = new Vector3(lineInfo.marginLeft + lineInfo.width / 2f - lineInfo.maxAdvance / 2f, 0f, 0f);
				break;
			case TextAlignment.TopGeoAligned:
			case TextAlignment.MiddleGeoAligned:
			case TextAlignment.BottomGeoAligned:
			case TextAlignment.BaselineGeoAligned:
			case TextAlignment.MidlineGeoAligned:
			case TextAlignment.CaplineGeoAligned:
				vector9 = new Vector3(lineInfo.marginLeft + lineInfo.width / 2f - (lineInfo.lineExtents.min.x + lineInfo.lineExtents.max.x) / 2f, 0f, 0f);
				break;
			case TextAlignment.TopRight:
			case TextAlignment.MiddleRight:
			case TextAlignment.BottomRight:
			case TextAlignment.BaselineRight:
			case TextAlignment.MidlineRight:
			case TextAlignment.CaplineRight:
				vector9 = (generationSettings.isRightToLeft ? new Vector3(lineInfo.marginLeft + lineInfo.width, 0f, 0f) : new Vector3(lineInfo.marginLeft + lineInfo.width - lineInfo.maxAdvance, 0f, 0f));
				break;
			case TextAlignment.TopJustified:
			case TextAlignment.TopFlush:
			case TextAlignment.MiddleJustified:
			case TextAlignment.MiddleFlush:
			case TextAlignment.BottomJustified:
			case TextAlignment.BottomFlush:
			case TextAlignment.BaselineJustified:
			case TextAlignment.BaselineFlush:
			case TextAlignment.MidlineJustified:
			case TextAlignment.MidlineFlush:
			case TextAlignment.CaplineJustified:
			case TextAlignment.CaplineFlush:
			{
				if (k > lineInfo.lastVisibleCharacterIndex || character == '\n' || character == '\u00ad' || character == '\u200b' || character == '\u2060' || character == '\u0003')
				{
					break;
				}
				char character2 = textElementInfo[lineInfo.lastCharacterIndex].character;
				bool flag19 = (alignment & (TextAlignment)16) == (TextAlignment)16;
				if ((!char.IsControl(character2) && lineNumber < m_LineNumber) || flag19 || lineInfo.maxAdvance > lineInfo.width)
				{
					if (lineNumber != num72 || k == 0 || k == generationSettings.firstVisibleCharacter)
					{
						vector9 = (generationSettings.isRightToLeft ? new Vector3(lineInfo.marginLeft + lineInfo.width, 0f, 0f) : new Vector3(lineInfo.marginLeft, 0f, 0f));
						flag16 = (char.IsSeparator(character) ? true : false);
						break;
					}
					float num84 = ((!generationSettings.isRightToLeft) ? (lineInfo.width - lineInfo.maxAdvance) : (lineInfo.width + lineInfo.maxAdvance));
					int num85 = lineInfo.visibleCharacterCount - 1 + lineInfo.controlCharacterCount;
					int num86 = lineInfo.spaceCount - lineInfo.controlCharacterCount;
					if (flag16)
					{
						num86--;
						num85++;
					}
					float num87 = ((num86 > 0) ? generationSettings.wordWrappingRatio : 1f);
					if (num86 < 1)
					{
						num86 = 1;
					}
					if (character != '\u00a0' && (character == '\t' || char.IsSeparator(character)))
					{
						if (!generationSettings.isRightToLeft)
						{
							vector9 += new Vector3(num84 * (1f - num87) / (float)num86, 0f, 0f);
						}
						else
						{
							vector9 -= new Vector3(num84 * (1f - num87) / (float)num86, 0f, 0f);
						}
					}
					else if (!generationSettings.isRightToLeft)
					{
						vector9 += new Vector3(num84 * num87 / (float)num85, 0f, 0f);
					}
					else
					{
						vector9 -= new Vector3(num84 * num87 / (float)num85, 0f, 0f);
					}
				}
				else
				{
					vector9 = (generationSettings.isRightToLeft ? new Vector3(lineInfo.marginLeft + lineInfo.width, 0f, 0f) : new Vector3(lineInfo.marginLeft, 0f, 0f));
				}
				break;
			}
			}
			zero3 = vector8 + vector9;
			bool isVisible = textElementInfo[k].isVisible;
			if (isVisible)
			{
				TextElementType elementType = textElementInfo[k].elementType;
				switch (elementType)
				{
				case TextElementType.Character:
				{
					Extents lineExtents = lineInfo.lineExtents;
					float num88 = generationSettings.uvLineOffset * (float)lineNumber % 1f;
					switch (generationSettings.horizontalMapping)
					{
					case TextureMapping.Character:
						textElementInfo[k].vertexBottomLeft.uv2.x = 0f;
						textElementInfo[k].vertexTopLeft.uv2.x = 0f;
						textElementInfo[k].vertexTopRight.uv2.x = 1f;
						textElementInfo[k].vertexBottomRight.uv2.x = 1f;
						break;
					case TextureMapping.Line:
						if (generationSettings.textAlignment != TextAlignment.MiddleJustified)
						{
							textElementInfo[k].vertexBottomLeft.uv2.x = (textElementInfo[k].vertexBottomLeft.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num88;
							textElementInfo[k].vertexTopLeft.uv2.x = (textElementInfo[k].vertexTopLeft.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num88;
							textElementInfo[k].vertexTopRight.uv2.x = (textElementInfo[k].vertexTopRight.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num88;
							textElementInfo[k].vertexBottomRight.uv2.x = (textElementInfo[k].vertexBottomRight.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num88;
						}
						else
						{
							textElementInfo[k].vertexBottomLeft.uv2.x = (textElementInfo[k].vertexBottomLeft.position.x + vector9.x - m_MeshExtents.min.x) / (m_MeshExtents.max.x - m_MeshExtents.min.x) + num88;
							textElementInfo[k].vertexTopLeft.uv2.x = (textElementInfo[k].vertexTopLeft.position.x + vector9.x - m_MeshExtents.min.x) / (m_MeshExtents.max.x - m_MeshExtents.min.x) + num88;
							textElementInfo[k].vertexTopRight.uv2.x = (textElementInfo[k].vertexTopRight.position.x + vector9.x - m_MeshExtents.min.x) / (m_MeshExtents.max.x - m_MeshExtents.min.x) + num88;
							textElementInfo[k].vertexBottomRight.uv2.x = (textElementInfo[k].vertexBottomRight.position.x + vector9.x - m_MeshExtents.min.x) / (m_MeshExtents.max.x - m_MeshExtents.min.x) + num88;
						}
						break;
					case TextureMapping.Paragraph:
						textElementInfo[k].vertexBottomLeft.uv2.x = (textElementInfo[k].vertexBottomLeft.position.x + vector9.x - m_MeshExtents.min.x) / (m_MeshExtents.max.x - m_MeshExtents.min.x) + num88;
						textElementInfo[k].vertexTopLeft.uv2.x = (textElementInfo[k].vertexTopLeft.position.x + vector9.x - m_MeshExtents.min.x) / (m_MeshExtents.max.x - m_MeshExtents.min.x) + num88;
						textElementInfo[k].vertexTopRight.uv2.x = (textElementInfo[k].vertexTopRight.position.x + vector9.x - m_MeshExtents.min.x) / (m_MeshExtents.max.x - m_MeshExtents.min.x) + num88;
						textElementInfo[k].vertexBottomRight.uv2.x = (textElementInfo[k].vertexBottomRight.position.x + vector9.x - m_MeshExtents.min.x) / (m_MeshExtents.max.x - m_MeshExtents.min.x) + num88;
						break;
					case TextureMapping.MatchAspect:
					{
						switch (generationSettings.verticalMapping)
						{
						case TextureMapping.Character:
							textElementInfo[k].vertexBottomLeft.uv2.y = 0f;
							textElementInfo[k].vertexTopLeft.uv2.y = 1f;
							textElementInfo[k].vertexTopRight.uv2.y = 0f;
							textElementInfo[k].vertexBottomRight.uv2.y = 1f;
							break;
						case TextureMapping.Line:
							textElementInfo[k].vertexBottomLeft.uv2.y = (textElementInfo[k].vertexBottomLeft.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num88;
							textElementInfo[k].vertexTopLeft.uv2.y = (textElementInfo[k].vertexTopLeft.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num88;
							textElementInfo[k].vertexTopRight.uv2.y = textElementInfo[k].vertexBottomLeft.uv2.y;
							textElementInfo[k].vertexBottomRight.uv2.y = textElementInfo[k].vertexTopLeft.uv2.y;
							break;
						case TextureMapping.Paragraph:
							textElementInfo[k].vertexBottomLeft.uv2.y = (textElementInfo[k].vertexBottomLeft.position.y - m_MeshExtents.min.y) / (m_MeshExtents.max.y - m_MeshExtents.min.y) + num88;
							textElementInfo[k].vertexTopLeft.uv2.y = (textElementInfo[k].vertexTopLeft.position.y - m_MeshExtents.min.y) / (m_MeshExtents.max.y - m_MeshExtents.min.y) + num88;
							textElementInfo[k].vertexTopRight.uv2.y = textElementInfo[k].vertexBottomLeft.uv2.y;
							textElementInfo[k].vertexBottomRight.uv2.y = textElementInfo[k].vertexTopLeft.uv2.y;
							break;
						case TextureMapping.MatchAspect:
							Debug.Log("ERROR: Cannot Match both Vertical & Horizontal.");
							break;
						}
						float num89 = (1f - (textElementInfo[k].vertexBottomLeft.uv2.y + textElementInfo[k].vertexTopLeft.uv2.y) * textElementInfo[k].aspectRatio) / 2f;
						textElementInfo[k].vertexBottomLeft.uv2.x = textElementInfo[k].vertexBottomLeft.uv2.y * textElementInfo[k].aspectRatio + num89 + num88;
						textElementInfo[k].vertexTopLeft.uv2.x = textElementInfo[k].vertexBottomLeft.uv2.x;
						textElementInfo[k].vertexTopRight.uv2.x = textElementInfo[k].vertexTopLeft.uv2.y * textElementInfo[k].aspectRatio + num89 + num88;
						textElementInfo[k].vertexBottomRight.uv2.x = textElementInfo[k].vertexTopRight.uv2.x;
						break;
					}
					}
					switch (generationSettings.verticalMapping)
					{
					case TextureMapping.Character:
						textElementInfo[k].vertexBottomLeft.uv2.y = 0f;
						textElementInfo[k].vertexTopLeft.uv2.y = 1f;
						textElementInfo[k].vertexTopRight.uv2.y = 1f;
						textElementInfo[k].vertexBottomRight.uv2.y = 0f;
						break;
					case TextureMapping.Line:
						textElementInfo[k].vertexBottomLeft.uv2.y = (textElementInfo[k].vertexBottomLeft.position.y - lineInfo.descender) / (lineInfo.ascender - lineInfo.descender);
						textElementInfo[k].vertexTopLeft.uv2.y = (textElementInfo[k].vertexTopLeft.position.y - lineInfo.descender) / (lineInfo.ascender - lineInfo.descender);
						textElementInfo[k].vertexTopRight.uv2.y = textElementInfo[k].vertexTopLeft.uv2.y;
						textElementInfo[k].vertexBottomRight.uv2.y = textElementInfo[k].vertexBottomLeft.uv2.y;
						break;
					case TextureMapping.Paragraph:
						textElementInfo[k].vertexBottomLeft.uv2.y = (textElementInfo[k].vertexBottomLeft.position.y - m_MeshExtents.min.y) / (m_MeshExtents.max.y - m_MeshExtents.min.y);
						textElementInfo[k].vertexTopLeft.uv2.y = (textElementInfo[k].vertexTopLeft.position.y - m_MeshExtents.min.y) / (m_MeshExtents.max.y - m_MeshExtents.min.y);
						textElementInfo[k].vertexTopRight.uv2.y = textElementInfo[k].vertexTopLeft.uv2.y;
						textElementInfo[k].vertexBottomRight.uv2.y = textElementInfo[k].vertexBottomLeft.uv2.y;
						break;
					case TextureMapping.MatchAspect:
					{
						float num90 = (1f - (textElementInfo[k].vertexBottomLeft.uv2.x + textElementInfo[k].vertexTopRight.uv2.x) / textElementInfo[k].aspectRatio) / 2f;
						textElementInfo[k].vertexBottomLeft.uv2.y = num90 + textElementInfo[k].vertexBottomLeft.uv2.x / textElementInfo[k].aspectRatio;
						textElementInfo[k].vertexTopLeft.uv2.y = num90 + textElementInfo[k].vertexTopRight.uv2.x / textElementInfo[k].aspectRatio;
						textElementInfo[k].vertexBottomRight.uv2.y = textElementInfo[k].vertexBottomLeft.uv2.y;
						textElementInfo[k].vertexTopRight.uv2.y = textElementInfo[k].vertexTopLeft.uv2.y;
						break;
					}
					}
					num75 = textElementInfo[k].scale * (1f - m_CharWidthAdjDelta) * 1f;
					if (!textElementInfo[k].isUsingAlternateTypeface && (textElementInfo[k].style & FontStyles.Bold) == FontStyles.Bold)
					{
						num75 *= -1f;
					}
					textElementInfo[k].vertexBottomLeft.uv.w = num75;
					textElementInfo[k].vertexTopLeft.uv.w = num75;
					textElementInfo[k].vertexTopRight.uv.w = num75;
					textElementInfo[k].vertexBottomRight.uv.w = num75;
					textElementInfo[k].vertexBottomLeft.uv2.x = 1f;
					textElementInfo[k].vertexBottomLeft.uv2.y = num75;
					textElementInfo[k].vertexTopLeft.uv2.x = 1f;
					textElementInfo[k].vertexTopLeft.uv2.y = num75;
					textElementInfo[k].vertexTopRight.uv2.x = 1f;
					textElementInfo[k].vertexTopRight.uv2.y = num75;
					textElementInfo[k].vertexBottomRight.uv2.x = 1f;
					textElementInfo[k].vertexBottomRight.uv2.y = num75;
					break;
				}
				}
				if (k < generationSettings.maxVisibleCharacters && num71 < generationSettings.maxVisibleWords && lineNumber < generationSettings.maxVisibleLines && generationSettings.overflowMode != TextOverflowMode.Page)
				{
					textElementInfo[k].vertexBottomLeft.position += zero3;
					textElementInfo[k].vertexTopLeft.position += zero3;
					textElementInfo[k].vertexTopRight.position += zero3;
					textElementInfo[k].vertexBottomRight.position += zero3;
				}
				else if (k < generationSettings.maxVisibleCharacters && num71 < generationSettings.maxVisibleWords && lineNumber < generationSettings.maxVisibleLines && generationSettings.overflowMode == TextOverflowMode.Page && textElementInfo[k].pageNumber == num8)
				{
					textElementInfo[k].vertexBottomLeft.position += zero3;
					textElementInfo[k].vertexTopLeft.position += zero3;
					textElementInfo[k].vertexTopRight.position += zero3;
					textElementInfo[k].vertexBottomRight.position += zero3;
				}
				else
				{
					textElementInfo[k].vertexBottomLeft.position = Vector3.zero;
					textElementInfo[k].vertexTopLeft.position = Vector3.zero;
					textElementInfo[k].vertexTopRight.position = Vector3.zero;
					textElementInfo[k].vertexBottomRight.position = Vector3.zero;
					textElementInfo[k].isVisible = false;
				}
				bool convertToLinearSpace = QualitySettings.activeColorSpace == ColorSpace.Linear && generationSettings.shouldConvertToLinearSpace;
				switch (elementType)
				{
				case TextElementType.Character:
					TextGeneratorUtilities.FillCharacterVertexBuffers(k, convertToLinearSpace, generationSettings, textInfo);
					break;
				case TextElementType.Sprite:
					TextGeneratorUtilities.FillSpriteVertexBuffers(k, convertToLinearSpace, generationSettings, textInfo);
					break;
				}
			}
			textInfo.textElementInfo[k].bottomLeft += zero3;
			textInfo.textElementInfo[k].topLeft += zero3;
			textInfo.textElementInfo[k].topRight += zero3;
			textInfo.textElementInfo[k].bottomRight += zero3;
			textInfo.textElementInfo[k].origin += zero3.x;
			textInfo.textElementInfo[k].xAdvance += zero3.x;
			textInfo.textElementInfo[k].ascender += zero3.y;
			textInfo.textElementInfo[k].descender += zero3.y;
			textInfo.textElementInfo[k].baseLine += zero3.y;
			if (isVisible)
			{
			}
			if (lineNumber != num72 || k == m_CharacterCount - 1)
			{
				if (lineNumber != num72)
				{
					textInfo.lineInfo[num72].baseline += zero3.y;
					textInfo.lineInfo[num72].ascender += zero3.y;
					textInfo.lineInfo[num72].descender += zero3.y;
					textInfo.lineInfo[num72].maxAdvance += zero3.x;
					textInfo.lineInfo[num72].lineExtents.min = new Vector2(textInfo.textElementInfo[textInfo.lineInfo[num72].firstCharacterIndex].bottomLeft.x, textInfo.lineInfo[num72].descender);
					textInfo.lineInfo[num72].lineExtents.max = new Vector2(textInfo.textElementInfo[textInfo.lineInfo[num72].lastVisibleCharacterIndex].topRight.x, textInfo.lineInfo[num72].ascender);
				}
				if (k == m_CharacterCount - 1)
				{
					textInfo.lineInfo[lineNumber].baseline += zero3.y;
					textInfo.lineInfo[lineNumber].ascender += zero3.y;
					textInfo.lineInfo[lineNumber].descender += zero3.y;
					textInfo.lineInfo[lineNumber].maxAdvance += zero3.x;
					textInfo.lineInfo[lineNumber].lineExtents.min = new Vector2(textInfo.textElementInfo[textInfo.lineInfo[lineNumber].firstCharacterIndex].bottomLeft.x, textInfo.lineInfo[lineNumber].descender);
					textInfo.lineInfo[lineNumber].lineExtents.max = new Vector2(textInfo.textElementInfo[textInfo.lineInfo[lineNumber].lastVisibleCharacterIndex].topRight.x, textInfo.lineInfo[lineNumber].ascender);
				}
			}
			if (char.IsLetterOrDigit(character) || character == '-' || character == '\u00ad' || character == '‐' || character == '‑')
			{
				if (!flag17)
				{
					flag17 = true;
					num73 = k;
				}
				if (flag17 && k == m_CharacterCount - 1)
				{
					int num91 = textInfo.wordInfo.Length;
					int wordCount = textInfo.wordCount;
					if (textInfo.wordCount + 1 > num91)
					{
						TextInfo.Resize(ref textInfo.wordInfo, num91 + 1);
					}
					num74 = k;
					textInfo.wordInfo[wordCount].firstCharacterIndex = num73;
					textInfo.wordInfo[wordCount].lastCharacterIndex = num74;
					textInfo.wordInfo[wordCount].characterCount = num74 - num73 + 1;
					num71++;
					TextInfo textInfo2 = textInfo;
					textInfo2.wordCount++;
					textInfo.lineInfo[lineNumber].wordCount++;
				}
			}
			else if ((flag17 || (k == 0 && (!char.IsPunctuation(character) || flag18 || character == '\u200b' || k == m_CharacterCount - 1))) && (k <= 0 || k >= textElementInfo.Length - 1 || k >= m_CharacterCount || (character != '\'' && character != '’') || !char.IsLetterOrDigit(textElementInfo[k - 1].character) || !char.IsLetterOrDigit(textElementInfo[k + 1].character)))
			{
				num74 = ((k == m_CharacterCount - 1 && char.IsLetterOrDigit(character)) ? k : (k - 1));
				flag17 = false;
				int num92 = textInfo.wordInfo.Length;
				int wordCount2 = textInfo.wordCount;
				if (textInfo.wordCount + 1 > num92)
				{
					TextInfo.Resize(ref textInfo.wordInfo, num92 + 1);
				}
				textInfo.wordInfo[wordCount2].firstCharacterIndex = num73;
				textInfo.wordInfo[wordCount2].lastCharacterIndex = num74;
				textInfo.wordInfo[wordCount2].characterCount = num74 - num73 + 1;
				num71++;
				TextInfo textInfo2 = textInfo;
				textInfo2.wordCount++;
				textInfo.lineInfo[lineNumber].wordCount++;
			}
			if ((textInfo.textElementInfo[k].style & FontStyles.Underline) == FontStyles.Underline)
			{
				bool flag20 = true;
				int pageNumber = textInfo.textElementInfo[k].pageNumber;
				textInfo.textElementInfo[k].underlineVertexIndex = m_MaterialReferences[m_Underline.materialIndex].referenceCount * 4;
				if (k > generationSettings.maxVisibleCharacters || lineNumber > generationSettings.maxVisibleLines || (generationSettings.overflowMode == TextOverflowMode.Page && pageNumber + 1 != generationSettings.pageToDisplay))
				{
					flag20 = false;
				}
				if (!flag18 && character != '\u200b')
				{
					num79 = Mathf.Max(num79, textInfo.textElementInfo[k].scale);
					num76 = Mathf.Max(num76, Mathf.Abs(num75));
					num80 = Mathf.Min((pageNumber == num81) ? num80 : 32767f, textInfo.textElementInfo[k].baseLine + fontAsset.faceInfo.underlineOffset * num79);
					num81 = pageNumber;
				}
				if (!flag && flag20 && k <= lineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\v' && character != '\r' && (k != lineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
				{
					flag = true;
					num77 = textInfo.textElementInfo[k].scale;
					if (num79 == 0f)
					{
						num79 = num77;
						num76 = num75;
					}
					start = new Vector3(textInfo.textElementInfo[k].bottomLeft.x, num80, 0f);
					color = textInfo.textElementInfo[k].underlineColor;
				}
				if (flag && m_CharacterCount == 1)
				{
					flag = false;
					zero = new Vector3(textInfo.textElementInfo[k].topRight.x, num80, 0f);
					num78 = textInfo.textElementInfo[k].scale;
					DrawUnderlineMesh(start, zero, num77, num78, num79, num76, color, generationSettings, textInfo);
					num79 = 0f;
					num76 = 0f;
					num80 = 32767f;
				}
				else if (flag && (k == lineInfo.lastCharacterIndex || k >= lineInfo.lastVisibleCharacterIndex))
				{
					if (flag18 || character == '\u200b')
					{
						int lastVisibleCharacterIndex = lineInfo.lastVisibleCharacterIndex;
						zero = new Vector3(textInfo.textElementInfo[lastVisibleCharacterIndex].topRight.x, num80, 0f);
						num78 = textInfo.textElementInfo[lastVisibleCharacterIndex].scale;
					}
					else
					{
						zero = new Vector3(textInfo.textElementInfo[k].topRight.x, num80, 0f);
						num78 = textInfo.textElementInfo[k].scale;
					}
					flag = false;
					DrawUnderlineMesh(start, zero, num77, num78, num79, num76, color, generationSettings, textInfo);
					num79 = 0f;
					num76 = 0f;
					num80 = 32767f;
				}
				else if (flag && !flag20)
				{
					flag = false;
					zero = new Vector3(textInfo.textElementInfo[k - 1].topRight.x, num80, 0f);
					num78 = textInfo.textElementInfo[k - 1].scale;
					DrawUnderlineMesh(start, zero, num77, num78, num79, num76, color, generationSettings, textInfo);
					num79 = 0f;
					num76 = 0f;
					num80 = 32767f;
				}
				else if (flag && k < m_CharacterCount - 1 && !ColorUtilities.CompareColors(color, textInfo.textElementInfo[k + 1].underlineColor))
				{
					flag = false;
					zero = new Vector3(textInfo.textElementInfo[k].topRight.x, num80, 0f);
					num78 = textInfo.textElementInfo[k].scale;
					DrawUnderlineMesh(start, zero, num77, num78, num79, num76, color, generationSettings, textInfo);
					num79 = 0f;
					num76 = 0f;
					num80 = 32767f;
				}
			}
			else if (flag)
			{
				flag = false;
				zero = new Vector3(textInfo.textElementInfo[k - 1].topRight.x, num80, 0f);
				num78 = textInfo.textElementInfo[k - 1].scale;
				DrawUnderlineMesh(start, zero, num77, num78, num79, num76, color, generationSettings, textInfo);
				num79 = 0f;
				num76 = 0f;
				num80 = 32767f;
			}
			bool flag21 = (textInfo.textElementInfo[k].style & FontStyles.Strikethrough) == FontStyles.Strikethrough;
			float strikethroughOffset = fontAsset.faceInfo.strikethroughOffset;
			if (flag21)
			{
				bool flag22 = true;
				textInfo.textElementInfo[k].strikethroughVertexIndex = m_MaterialReferences[m_Underline.materialIndex].referenceCount * 4;
				if (k > generationSettings.maxVisibleCharacters || lineNumber > generationSettings.maxVisibleLines || (generationSettings.overflowMode == TextOverflowMode.Page && textInfo.textElementInfo[k].pageNumber + 1 != generationSettings.pageToDisplay))
				{
					flag22 = false;
				}
				if (!flag2 && flag22 && k <= lineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\v' && character != '\r' && (k != lineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
				{
					flag2 = true;
					num82 = textInfo.textElementInfo[k].pointSize;
					num83 = textInfo.textElementInfo[k].scale;
					start2 = new Vector3(textInfo.textElementInfo[k].bottomLeft.x, textInfo.textElementInfo[k].baseLine + strikethroughOffset * num83, 0f);
					underlineColor = textInfo.textElementInfo[k].strikethroughColor;
					b = textInfo.textElementInfo[k].baseLine;
				}
				if (flag2 && m_CharacterCount == 1)
				{
					flag2 = false;
					zero2 = new Vector3(textInfo.textElementInfo[k].topRight.x, textInfo.textElementInfo[k].baseLine + strikethroughOffset * num83, 0f);
					DrawUnderlineMesh(start2, zero2, num83, num83, num83, num75, underlineColor, generationSettings, textInfo);
				}
				else if (flag2 && k == lineInfo.lastCharacterIndex)
				{
					if (flag18 || character == '\u200b')
					{
						int lastVisibleCharacterIndex2 = lineInfo.lastVisibleCharacterIndex;
						zero2 = new Vector3(textInfo.textElementInfo[lastVisibleCharacterIndex2].topRight.x, textInfo.textElementInfo[lastVisibleCharacterIndex2].baseLine + strikethroughOffset * num83, 0f);
					}
					else
					{
						zero2 = new Vector3(textInfo.textElementInfo[k].topRight.x, textInfo.textElementInfo[k].baseLine + strikethroughOffset * num83, 0f);
					}
					flag2 = false;
					DrawUnderlineMesh(start2, zero2, num83, num83, num83, num75, underlineColor, generationSettings, textInfo);
				}
				else if (flag2 && k < m_CharacterCount && (textInfo.textElementInfo[k + 1].pointSize != num82 || !TextGeneratorUtilities.Approximately(textInfo.textElementInfo[k + 1].baseLine + zero3.y, b)))
				{
					flag2 = false;
					int lastVisibleCharacterIndex3 = lineInfo.lastVisibleCharacterIndex;
					zero2 = ((k <= lastVisibleCharacterIndex3) ? new Vector3(textInfo.textElementInfo[k].topRight.x, textInfo.textElementInfo[k].baseLine + strikethroughOffset * num83, 0f) : new Vector3(textInfo.textElementInfo[lastVisibleCharacterIndex3].topRight.x, textInfo.textElementInfo[lastVisibleCharacterIndex3].baseLine + strikethroughOffset * num83, 0f));
					DrawUnderlineMesh(start2, zero2, num83, num83, num83, num75, underlineColor, generationSettings, textInfo);
				}
				else if (flag2 && k < m_CharacterCount && fontAsset.GetInstanceID() != textElementInfo[k + 1].fontAsset.GetInstanceID())
				{
					flag2 = false;
					zero2 = new Vector3(textInfo.textElementInfo[k].topRight.x, textInfo.textElementInfo[k].baseLine + strikethroughOffset * num83, 0f);
					DrawUnderlineMesh(start2, zero2, num83, num83, num83, num75, underlineColor, generationSettings, textInfo);
				}
				else if (flag2 && !flag22)
				{
					flag2 = false;
					zero2 = new Vector3(textInfo.textElementInfo[k - 1].topRight.x, textInfo.textElementInfo[k - 1].baseLine + strikethroughOffset * num83, 0f);
					DrawUnderlineMesh(start2, zero2, num83, num83, num83, num75, underlineColor, generationSettings, textInfo);
				}
			}
			else if (flag2)
			{
				flag2 = false;
				zero2 = new Vector3(textInfo.textElementInfo[k - 1].topRight.x, textInfo.textElementInfo[k - 1].baseLine + strikethroughOffset * num83, 0f);
				DrawUnderlineMesh(start2, zero2, num83, num83, num83, num75, underlineColor, generationSettings, textInfo);
			}
			if ((textInfo.textElementInfo[k].style & FontStyles.Highlight) == FontStyles.Highlight)
			{
				bool flag23 = true;
				int pageNumber2 = textInfo.textElementInfo[k].pageNumber;
				if (k > generationSettings.maxVisibleCharacters || lineNumber > generationSettings.maxVisibleLines || (generationSettings.overflowMode == TextOverflowMode.Page && pageNumber2 + 1 != generationSettings.pageToDisplay))
				{
					flag23 = false;
				}
				if (!flag3 && flag23 && k <= lineInfo.lastVisibleCharacterIndex && character != '\n' && character != '\v' && character != '\r' && (k != lineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character)))
				{
					flag3 = true;
					start3 = TextGeneratorUtilities.largePositiveVector2;
					end = TextGeneratorUtilities.largeNegativeVector2;
					highlightState = textInfo.textElementInfo[k].highlightState;
				}
				if (flag3)
				{
					TextElementInfo textElementInfo2 = textInfo.textElementInfo[k];
					HighlightState highlightState2 = textElementInfo2.highlightState;
					bool flag24 = false;
					if (highlightState != highlightState2)
					{
						if (flag18)
						{
							end.x = (end.x - highlightState.padding.right + textElementInfo2.origin) / 2f;
						}
						else
						{
							end.x = (end.x - highlightState.padding.right + textElementInfo2.bottomLeft.x) / 2f;
						}
						start3.y = Mathf.Min(start3.y, textElementInfo2.descender);
						end.y = Mathf.Max(end.y, textElementInfo2.ascender);
						DrawTextHighlight(start3, end, highlightState.color, generationSettings, textInfo);
						flag3 = true;
						start3 = new Vector2(end.x, textElementInfo2.descender - highlightState2.padding.bottom);
						end = ((!flag18) ? ((Vector3)new Vector2(textElementInfo2.topRight.x + highlightState2.padding.right, textElementInfo2.ascender + highlightState2.padding.top)) : ((Vector3)new Vector2(textElementInfo2.xAdvance + highlightState2.padding.right, textElementInfo2.ascender + highlightState2.padding.top)));
						highlightState = highlightState2;
						flag24 = true;
					}
					if (!flag24)
					{
						if (flag18)
						{
							start3.x = Mathf.Min(start3.x, textElementInfo2.origin - highlightState.padding.left);
							end.x = Mathf.Max(end.x, textElementInfo2.xAdvance + highlightState.padding.right);
						}
						else
						{
							start3.x = Mathf.Min(start3.x, textElementInfo2.bottomLeft.x - highlightState.padding.left);
							end.x = Mathf.Max(end.x, textElementInfo2.topRight.x + highlightState.padding.right);
						}
						start3.y = Mathf.Min(start3.y, textElementInfo2.descender - highlightState.padding.bottom);
						end.y = Mathf.Max(end.y, textElementInfo2.ascender + highlightState.padding.top);
					}
				}
				if (flag3 && m_CharacterCount == 1)
				{
					flag3 = false;
					DrawTextHighlight(start3, end, highlightState.color, generationSettings, textInfo);
				}
				else if (flag3 && (k == lineInfo.lastCharacterIndex || k >= lineInfo.lastVisibleCharacterIndex))
				{
					flag3 = false;
					DrawTextHighlight(start3, end, highlightState.color, generationSettings, textInfo);
				}
				else if (flag3 && !flag23)
				{
					flag3 = false;
					DrawTextHighlight(start3, end, highlightState.color, generationSettings, textInfo);
				}
			}
			else if (flag3)
			{
				flag3 = false;
				DrawTextHighlight(start3, end, highlightState.color, generationSettings, textInfo);
			}
			num72 = lineNumber;
		}
		textInfo.characterCount = m_CharacterCount;
		textInfo.spriteCount = m_SpriteCount;
		textInfo.lineCount = lineCount;
		textInfo.wordCount = ((num71 == 0 || m_CharacterCount <= 0) ? 1 : num71);
		textInfo.pageCount = m_PageNumber + 1;
		for (int l = 1; l < textInfo.materialCount; l++)
		{
			textInfo.meshInfo[l].ClearUnusedVertices();
			if (generationSettings.geometrySortingOrder != VertexSortingOrder.Normal)
			{
				textInfo.meshInfo[l].SortGeometry(VertexSortingOrder.Reverse);
			}
		}
	}

	private void SaveWordWrappingState(ref WordWrapState state, int index, int count, TextInfo textInfo)
	{
		state.currentFontAsset = m_CurrentFontAsset;
		state.currentSpriteAsset = m_CurrentSpriteAsset;
		state.currentMaterial = m_CurrentMaterial;
		state.currentMaterialIndex = m_CurrentMaterialIndex;
		state.previousWordBreak = index;
		state.totalCharacterCount = count;
		state.visibleCharacterCount = m_LineVisibleCharacterCount;
		state.visibleSpaceCount = m_LineVisibleSpaceCount;
		state.visibleLinkCount = textInfo.linkCount;
		state.firstCharacterIndex = m_FirstCharacterOfLine;
		state.firstVisibleCharacterIndex = m_FirstVisibleCharacterOfLine;
		state.lastVisibleCharIndex = m_LastVisibleCharacterOfLine;
		state.fontStyle = m_FontStyleInternal;
		state.italicAngle = m_ItalicAngle;
		state.fontScaleMultiplier = m_FontScaleMultiplier;
		state.currentFontSize = m_CurrentFontSize;
		state.xAdvance = m_XAdvance;
		state.maxCapHeight = m_MaxCapHeight;
		state.maxAscender = m_MaxAscender;
		state.maxDescender = m_MaxDescender;
		state.maxLineAscender = m_MaxLineAscender;
		state.maxLineDescender = m_MaxLineDescender;
		state.startOfLineAscender = m_StartOfLineAscender;
		state.preferredWidth = m_PreferredWidth;
		state.preferredHeight = m_PreferredHeight;
		state.meshExtents = m_MeshExtents;
		state.pageAscender = m_PageAscender;
		state.lineNumber = m_LineNumber;
		state.lineOffset = m_LineOffset;
		state.baselineOffset = m_BaselineOffset;
		state.isDrivenLineSpacing = m_IsDrivenLineSpacing;
		state.vertexColor = m_HtmlColor;
		state.underlineColor = m_UnderlineColor;
		state.strikethroughColor = m_StrikethroughColor;
		state.highlightColor = m_HighlightColor;
		state.highlightState = m_HighlightState;
		state.isNonBreakingSpace = m_IsNonBreakingSpace;
		state.tagNoParsing = m_TagNoParsing;
		state.fxScale = m_FXScale;
		state.fxRotation = m_FXRotation;
		state.basicStyleStack = m_FontStyleStack;
		state.italicAngleStack = m_ItalicAngleStack;
		state.colorStack = m_ColorStack;
		state.underlineColorStack = m_UnderlineColorStack;
		state.strikethroughColorStack = m_StrikethroughColorStack;
		state.highlightColorStack = m_HighlightColorStack;
		state.colorGradientStack = m_ColorGradientStack;
		state.highlightStateStack = m_HighlightStateStack;
		state.sizeStack = m_SizeStack;
		state.indentStack = m_IndentStack;
		state.fontWeightStack = m_FontWeightStack;
		state.styleStack = m_StyleStack;
		state.baselineStack = m_BaselineOffsetStack;
		state.actionStack = m_ActionStack;
		state.materialReferenceStack = m_MaterialReferenceStack;
		state.lineJustificationStack = m_LineJustificationStack;
		state.lastBaseGlyphIndex = m_LastBaseGlyphIndex;
		state.spriteAnimationId = m_SpriteAnimationId;
		if (m_LineNumber < textInfo.lineInfo.Length)
		{
			state.lineInfo = textInfo.lineInfo[m_LineNumber];
		}
	}

	protected int RestoreWordWrappingState(ref WordWrapState state, TextInfo textInfo)
	{
		int previousWordBreak = state.previousWordBreak;
		m_CurrentFontAsset = state.currentFontAsset;
		m_CurrentSpriteAsset = state.currentSpriteAsset;
		m_CurrentMaterial = state.currentMaterial;
		m_CurrentMaterialIndex = state.currentMaterialIndex;
		m_CharacterCount = state.totalCharacterCount + 1;
		m_LineVisibleCharacterCount = state.visibleCharacterCount;
		m_LineVisibleSpaceCount = state.visibleSpaceCount;
		textInfo.linkCount = state.visibleLinkCount;
		m_FirstCharacterOfLine = state.firstCharacterIndex;
		m_FirstVisibleCharacterOfLine = state.firstVisibleCharacterIndex;
		m_LastVisibleCharacterOfLine = state.lastVisibleCharIndex;
		m_FontStyleInternal = state.fontStyle;
		m_ItalicAngle = state.italicAngle;
		m_FontScaleMultiplier = state.fontScaleMultiplier;
		m_CurrentFontSize = state.currentFontSize;
		m_XAdvance = state.xAdvance;
		m_MaxCapHeight = state.maxCapHeight;
		m_MaxAscender = state.maxAscender;
		m_MaxDescender = state.maxDescender;
		m_MaxLineAscender = state.maxLineAscender;
		m_MaxLineDescender = state.maxLineDescender;
		m_StartOfLineAscender = state.startOfLineAscender;
		m_PreferredWidth = state.preferredWidth;
		m_PreferredHeight = state.preferredHeight;
		m_MeshExtents = state.meshExtents;
		m_PageAscender = state.pageAscender;
		m_LineNumber = state.lineNumber;
		m_LineOffset = state.lineOffset;
		m_BaselineOffset = state.baselineOffset;
		m_IsDrivenLineSpacing = state.isDrivenLineSpacing;
		m_HtmlColor = state.vertexColor;
		m_UnderlineColor = state.underlineColor;
		m_StrikethroughColor = state.strikethroughColor;
		m_HighlightColor = state.highlightColor;
		m_HighlightState = state.highlightState;
		m_IsNonBreakingSpace = state.isNonBreakingSpace;
		m_TagNoParsing = state.tagNoParsing;
		m_FXScale = state.fxScale;
		m_FXRotation = state.fxRotation;
		m_FontStyleStack = state.basicStyleStack;
		m_ItalicAngleStack = state.italicAngleStack;
		m_ColorStack = state.colorStack;
		m_UnderlineColorStack = state.underlineColorStack;
		m_StrikethroughColorStack = state.strikethroughColorStack;
		m_HighlightColorStack = state.highlightColorStack;
		m_ColorGradientStack = state.colorGradientStack;
		m_HighlightStateStack = state.highlightStateStack;
		m_SizeStack = state.sizeStack;
		m_IndentStack = state.indentStack;
		m_FontWeightStack = state.fontWeightStack;
		m_StyleStack = state.styleStack;
		m_BaselineOffsetStack = state.baselineStack;
		m_ActionStack = state.actionStack;
		m_MaterialReferenceStack = state.materialReferenceStack;
		m_LineJustificationStack = state.lineJustificationStack;
		m_LastBaseGlyphIndex = state.lastBaseGlyphIndex;
		m_SpriteAnimationId = state.spriteAnimationId;
		if (m_LineNumber < textInfo.lineInfo.Length)
		{
			textInfo.lineInfo[m_LineNumber] = state.lineInfo;
		}
		return previousWordBreak;
	}

	protected bool ValidateHtmlTag(TextProcessingElement[] chars, int startIndex, out int endIndex, TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		TextSettings textSettings = generationSettings.textSettings;
		int num = 0;
		byte b = 0;
		int num2 = 0;
		ClearMarkupTagAttributes();
		TagValueType tagValueType = TagValueType.None;
		TagUnitType tagUnitType = TagUnitType.Pixels;
		endIndex = startIndex;
		bool flag = false;
		bool flag2 = false;
		for (int i = startIndex; i < chars.Length && chars[i].unicode != 0; i++)
		{
			if (num >= m_HtmlTag.Length)
			{
				break;
			}
			if (chars[i].unicode == 60)
			{
				break;
			}
			uint unicode = chars[i].unicode;
			if (unicode == 62)
			{
				flag2 = true;
				endIndex = i;
				m_HtmlTag[num] = '\0';
				break;
			}
			m_HtmlTag[num] = (char)unicode;
			num++;
			if (b == 1)
			{
				switch (tagValueType)
				{
				case TagValueType.None:
					if (unicode == 43 || unicode == 45 || unicode == 46 || (unicode >= 48 && unicode <= 57))
					{
						tagUnitType = TagUnitType.Pixels;
						tagValueType = (m_XmlAttribute[num2].valueType = TagValueType.NumericalValue);
						m_XmlAttribute[num2].valueStartIndex = num - 1;
						m_XmlAttribute[num2].valueLength++;
						break;
					}
					switch (unicode)
					{
					case 35u:
						tagUnitType = TagUnitType.Pixels;
						tagValueType = (m_XmlAttribute[num2].valueType = TagValueType.ColorValue);
						m_XmlAttribute[num2].valueStartIndex = num - 1;
						m_XmlAttribute[num2].valueLength++;
						break;
					case 34u:
						tagUnitType = TagUnitType.Pixels;
						tagValueType = (m_XmlAttribute[num2].valueType = TagValueType.StringValue);
						m_XmlAttribute[num2].valueStartIndex = num;
						break;
					default:
						tagUnitType = TagUnitType.Pixels;
						tagValueType = (m_XmlAttribute[num2].valueType = TagValueType.StringValue);
						m_XmlAttribute[num2].valueStartIndex = num - 1;
						m_XmlAttribute[num2].valueHashCode = ((m_XmlAttribute[num2].valueHashCode << 5) + m_XmlAttribute[num2].valueHashCode) ^ TextGeneratorUtilities.ToUpperFast((char)unicode);
						m_XmlAttribute[num2].valueLength++;
						break;
					}
					break;
				case TagValueType.NumericalValue:
					if (unicode == 112 || unicode == 101 || unicode == 37 || unicode == 32)
					{
						b = 2;
						tagValueType = TagValueType.None;
						tagUnitType = unicode switch
						{
							101u => m_XmlAttribute[num2].unitType = TagUnitType.FontUnits, 
							37u => m_XmlAttribute[num2].unitType = TagUnitType.Percentage, 
							_ => m_XmlAttribute[num2].unitType = TagUnitType.Pixels, 
						};
						num2++;
						m_XmlAttribute[num2].nameHashCode = 0;
						m_XmlAttribute[num2].valueHashCode = 0;
						m_XmlAttribute[num2].valueType = TagValueType.None;
						m_XmlAttribute[num2].unitType = TagUnitType.Pixels;
						m_XmlAttribute[num2].valueStartIndex = 0;
						m_XmlAttribute[num2].valueLength = 0;
					}
					else
					{
						m_XmlAttribute[num2].valueLength++;
					}
					break;
				case TagValueType.ColorValue:
					if (unicode != 32)
					{
						m_XmlAttribute[num2].valueLength++;
						break;
					}
					b = 2;
					tagValueType = TagValueType.None;
					tagUnitType = TagUnitType.Pixels;
					num2++;
					m_XmlAttribute[num2].nameHashCode = 0;
					m_XmlAttribute[num2].valueType = TagValueType.None;
					m_XmlAttribute[num2].unitType = TagUnitType.Pixels;
					m_XmlAttribute[num2].valueHashCode = 0;
					m_XmlAttribute[num2].valueStartIndex = 0;
					m_XmlAttribute[num2].valueLength = 0;
					break;
				case TagValueType.StringValue:
					if (unicode != 34)
					{
						m_XmlAttribute[num2].valueHashCode = ((m_XmlAttribute[num2].valueHashCode << 5) + m_XmlAttribute[num2].valueHashCode) ^ TextGeneratorUtilities.ToUpperFast((char)unicode);
						m_XmlAttribute[num2].valueLength++;
						break;
					}
					b = 2;
					tagValueType = TagValueType.None;
					tagUnitType = TagUnitType.Pixels;
					num2++;
					m_XmlAttribute[num2].nameHashCode = 0;
					m_XmlAttribute[num2].valueType = TagValueType.None;
					m_XmlAttribute[num2].unitType = TagUnitType.Pixels;
					m_XmlAttribute[num2].valueHashCode = 0;
					m_XmlAttribute[num2].valueStartIndex = 0;
					m_XmlAttribute[num2].valueLength = 0;
					break;
				}
			}
			if (unicode == 61)
			{
				b = 1;
			}
			if (b == 0 && unicode == 32)
			{
				if (flag)
				{
					return false;
				}
				flag = true;
				b = 2;
				tagValueType = TagValueType.None;
				tagUnitType = TagUnitType.Pixels;
				num2++;
				m_XmlAttribute[num2].nameHashCode = 0;
				m_XmlAttribute[num2].valueType = TagValueType.None;
				m_XmlAttribute[num2].unitType = TagUnitType.Pixels;
				m_XmlAttribute[num2].valueHashCode = 0;
				m_XmlAttribute[num2].valueStartIndex = 0;
				m_XmlAttribute[num2].valueLength = 0;
			}
			if (b == 0)
			{
				m_XmlAttribute[num2].nameHashCode = ((m_XmlAttribute[num2].nameHashCode << 5) + m_XmlAttribute[num2].nameHashCode) ^ TextGeneratorUtilities.ToUpperFast((char)unicode);
			}
			if (b == 2 && unicode == 32)
			{
				b = 0;
			}
		}
		if (!flag2)
		{
			return false;
		}
		if (m_TagNoParsing && m_XmlAttribute[0].nameHashCode != -294095813)
		{
			return false;
		}
		if (m_XmlAttribute[0].nameHashCode == -294095813)
		{
			m_TagNoParsing = false;
			return true;
		}
		if (m_HtmlTag[0] == '#' && num == 4)
		{
			m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(m_HtmlTag, num);
			m_ColorStack.Add(m_HtmlColor);
			return true;
		}
		if (m_HtmlTag[0] == '#' && num == 5)
		{
			m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(m_HtmlTag, num);
			m_ColorStack.Add(m_HtmlColor);
			return true;
		}
		if (m_HtmlTag[0] == '#' && num == 7)
		{
			m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(m_HtmlTag, num);
			m_ColorStack.Add(m_HtmlColor);
			return true;
		}
		if (m_HtmlTag[0] == '#' && num == 9)
		{
			m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(m_HtmlTag, num);
			m_ColorStack.Add(m_HtmlColor);
			return true;
		}
		float num3 = 0f;
		Material material;
		switch ((MarkupTag)m_XmlAttribute[0].nameHashCode)
		{
		case MarkupTag.BOLD:
			m_FontStyleInternal |= FontStyles.Bold;
			m_FontStyleStack.Add(FontStyles.Bold);
			m_FontWeightInternal = TextFontWeight.Bold;
			return true;
		case MarkupTag.SLASH_BOLD:
			if ((generationSettings.fontStyle & FontStyles.Bold) != FontStyles.Bold && m_FontStyleStack.Remove(FontStyles.Bold) == 0)
			{
				m_FontStyleInternal &= ~FontStyles.Bold;
				m_FontWeightInternal = m_FontWeightStack.Peek();
			}
			return true;
		case MarkupTag.ITALIC:
			m_FontStyleInternal |= FontStyles.Italic;
			m_FontStyleStack.Add(FontStyles.Italic);
			if (m_XmlAttribute[1].nameHashCode == 75347905)
			{
				m_ItalicAngle = (int)TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[1].valueStartIndex, m_XmlAttribute[1].valueLength);
				if (m_ItalicAngle < -180 || m_ItalicAngle > 180)
				{
					return false;
				}
			}
			else
			{
				m_ItalicAngle = m_CurrentFontAsset.italicStyleSlant;
			}
			m_ItalicAngleStack.Add(m_ItalicAngle);
			return true;
		case MarkupTag.SLASH_ITALIC:
			if ((generationSettings.fontStyle & FontStyles.Italic) != FontStyles.Italic)
			{
				m_ItalicAngle = m_ItalicAngleStack.Remove();
				if (m_FontStyleStack.Remove(FontStyles.Italic) == 0)
				{
					m_FontStyleInternal &= ~FontStyles.Italic;
				}
			}
			return true;
		case MarkupTag.STRIKETHROUGH:
			m_FontStyleInternal |= FontStyles.Strikethrough;
			m_FontStyleStack.Add(FontStyles.Strikethrough);
			if (m_XmlAttribute[1].nameHashCode == 81999901)
			{
				m_StrikethroughColor = TextGeneratorUtilities.HexCharsToColor(m_HtmlTag, m_XmlAttribute[1].valueStartIndex, m_XmlAttribute[1].valueLength);
				m_StrikethroughColor.a = ((m_HtmlColor.a < m_StrikethroughColor.a) ? m_HtmlColor.a : m_StrikethroughColor.a);
				textInfo.hasMultipleColors = true;
			}
			else
			{
				m_StrikethroughColor = m_HtmlColor;
			}
			m_StrikethroughColorStack.Add(m_StrikethroughColor);
			return true;
		case MarkupTag.SLASH_STRIKETHROUGH:
			if ((generationSettings.fontStyle & FontStyles.Strikethrough) != FontStyles.Strikethrough && m_FontStyleStack.Remove(FontStyles.Strikethrough) == 0)
			{
				m_FontStyleInternal &= ~FontStyles.Strikethrough;
			}
			m_StrikethroughColor = m_StrikethroughColorStack.Remove();
			return true;
		case MarkupTag.UNDERLINE:
			m_FontStyleInternal |= FontStyles.Underline;
			m_FontStyleStack.Add(FontStyles.Underline);
			if (m_XmlAttribute[1].nameHashCode == 81999901)
			{
				m_UnderlineColor = TextGeneratorUtilities.HexCharsToColor(m_HtmlTag, m_XmlAttribute[1].valueStartIndex, m_XmlAttribute[1].valueLength);
				m_UnderlineColor.a = ((m_HtmlColor.a < m_UnderlineColor.a) ? m_HtmlColor.a : m_UnderlineColor.a);
				textInfo.hasMultipleColors = true;
			}
			else
			{
				m_UnderlineColor = m_HtmlColor;
			}
			m_UnderlineColorStack.Add(m_UnderlineColor);
			return true;
		case MarkupTag.SLASH_UNDERLINE:
			if ((generationSettings.fontStyle & FontStyles.Underline) != FontStyles.Underline && m_FontStyleStack.Remove(FontStyles.Underline) == 0)
			{
				m_FontStyleInternal &= ~FontStyles.Underline;
			}
			m_UnderlineColor = m_UnderlineColorStack.Remove();
			return true;
		case MarkupTag.MARK:
		{
			m_FontStyleInternal |= FontStyles.Highlight;
			m_FontStyleStack.Add(FontStyles.Highlight);
			Color32 color = new Color32(byte.MaxValue, byte.MaxValue, 0, 64);
			Offset padding = Offset.zero;
			for (int j = 0; j < m_XmlAttribute.Length && m_XmlAttribute[j].nameHashCode != 0; j++)
			{
				switch ((MarkupTag)m_XmlAttribute[j].nameHashCode)
				{
				case MarkupTag.MARK:
					if (m_XmlAttribute[j].valueType == TagValueType.ColorValue)
					{
						color = TextGeneratorUtilities.HexCharsToColor(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
					}
					break;
				case MarkupTag.COLOR:
					color = TextGeneratorUtilities.HexCharsToColor(m_HtmlTag, m_XmlAttribute[j].valueStartIndex, m_XmlAttribute[j].valueLength);
					break;
				case MarkupTag.PADDING:
				{
					int attributeParameters = TextGeneratorUtilities.GetAttributeParameters(m_HtmlTag, m_XmlAttribute[j].valueStartIndex, m_XmlAttribute[j].valueLength, ref m_AttributeParameterValues);
					if (attributeParameters != 4)
					{
						return false;
					}
					padding = new Offset(m_AttributeParameterValues[0], m_AttributeParameterValues[1], m_AttributeParameterValues[2], m_AttributeParameterValues[3]);
					padding *= m_FontSize * 0.01f * (generationSettings.isOrthographic ? 1f : 0.1f);
					break;
				}
				}
			}
			color.a = ((m_HtmlColor.a < color.a) ? m_HtmlColor.a : color.a);
			m_HighlightState = new HighlightState(color, padding);
			m_HighlightStateStack.Push(m_HighlightState);
			textInfo.hasMultipleColors = true;
			return true;
		}
		case MarkupTag.SLASH_MARK:
			if ((generationSettings.fontStyle & FontStyles.Highlight) != FontStyles.Highlight)
			{
				m_HighlightStateStack.Remove();
				m_HighlightState = m_HighlightStateStack.current;
				if (m_FontStyleStack.Remove(FontStyles.Highlight) == 0)
				{
					m_FontStyleInternal &= ~FontStyles.Highlight;
				}
			}
			return true;
		case MarkupTag.SUBSCRIPT:
		{
			m_FontScaleMultiplier *= ((m_CurrentFontAsset.faceInfo.subscriptSize > 0f) ? m_CurrentFontAsset.faceInfo.subscriptSize : 1f);
			m_BaselineOffsetStack.Push(m_BaselineOffset);
			float num4 = m_CurrentFontSize / (float)m_CurrentFontAsset.faceInfo.pointSize * m_CurrentFontAsset.faceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
			m_BaselineOffset += m_CurrentFontAsset.faceInfo.subscriptOffset * num4 * m_FontScaleMultiplier;
			m_FontStyleStack.Add(FontStyles.Subscript);
			m_FontStyleInternal |= FontStyles.Subscript;
			return true;
		}
		case MarkupTag.SLASH_SUBSCRIPT:
			if ((m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript)
			{
				if (m_FontScaleMultiplier < 1f)
				{
					m_BaselineOffset = m_BaselineOffsetStack.Pop();
					m_FontScaleMultiplier /= ((m_CurrentFontAsset.faceInfo.subscriptSize > 0f) ? m_CurrentFontAsset.faceInfo.subscriptSize : 1f);
				}
				if (m_FontStyleStack.Remove(FontStyles.Subscript) == 0)
				{
					m_FontStyleInternal &= ~FontStyles.Subscript;
				}
			}
			return true;
		case MarkupTag.SUPERSCRIPT:
		{
			m_FontScaleMultiplier *= ((m_CurrentFontAsset.faceInfo.superscriptSize > 0f) ? m_CurrentFontAsset.faceInfo.superscriptSize : 1f);
			m_BaselineOffsetStack.Push(m_BaselineOffset);
			float num4 = m_CurrentFontSize / (float)m_CurrentFontAsset.faceInfo.pointSize * m_CurrentFontAsset.faceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
			m_BaselineOffset += m_CurrentFontAsset.faceInfo.superscriptOffset * num4 * m_FontScaleMultiplier;
			m_FontStyleStack.Add(FontStyles.Superscript);
			m_FontStyleInternal |= FontStyles.Superscript;
			return true;
		}
		case MarkupTag.SLASH_SUPERSCRIPT:
			if ((m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript)
			{
				if (m_FontScaleMultiplier < 1f)
				{
					m_BaselineOffset = m_BaselineOffsetStack.Pop();
					m_FontScaleMultiplier /= ((m_CurrentFontAsset.faceInfo.superscriptSize > 0f) ? m_CurrentFontAsset.faceInfo.superscriptSize : 1f);
				}
				if (m_FontStyleStack.Remove(FontStyles.Superscript) == 0)
				{
					m_FontStyleInternal &= ~FontStyles.Superscript;
				}
			}
			return true;
		case MarkupTag.FONT_WEIGHT:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			switch ((int)num3)
			{
			case 100:
				m_FontWeightInternal = TextFontWeight.Thin;
				break;
			case 200:
				m_FontWeightInternal = TextFontWeight.ExtraLight;
				break;
			case 300:
				m_FontWeightInternal = TextFontWeight.Light;
				break;
			case 400:
				m_FontWeightInternal = TextFontWeight.Regular;
				break;
			case 500:
				m_FontWeightInternal = TextFontWeight.Medium;
				break;
			case 600:
				m_FontWeightInternal = TextFontWeight.SemiBold;
				break;
			case 700:
				m_FontWeightInternal = TextFontWeight.Bold;
				break;
			case 800:
				m_FontWeightInternal = TextFontWeight.Heavy;
				break;
			case 900:
				m_FontWeightInternal = TextFontWeight.Black;
				break;
			}
			m_FontWeightStack.Add(m_FontWeightInternal);
			return true;
		case MarkupTag.SLASH_FONT_WEIGHT:
			m_FontWeightStack.Remove();
			if (m_FontStyleInternal == FontStyles.Bold)
			{
				m_FontWeightInternal = TextFontWeight.Bold;
			}
			else
			{
				m_FontWeightInternal = m_FontWeightStack.Peek();
			}
			return true;
		case MarkupTag.POSITION:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_XAdvance = num3 * (generationSettings.isOrthographic ? 1f : 0.1f);
				return true;
			case TagUnitType.FontUnits:
				m_XAdvance = num3 * m_CurrentFontSize * (generationSettings.isOrthographic ? 1f : 0.1f);
				return true;
			case TagUnitType.Percentage:
				m_XAdvance = m_MarginWidth * num3 / 100f;
				return true;
			default:
				return false;
			}
		case MarkupTag.SLASH_POSITION:
			m_IsIgnoringAlignment = false;
			return true;
		case MarkupTag.VERTICAL_OFFSET:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_BaselineOffset = num3 * (generationSettings.isOrthographic ? 1f : 0.1f);
				return true;
			case TagUnitType.FontUnits:
				m_BaselineOffset = num3 * (generationSettings.isOrthographic ? 1f : 0.1f) * m_CurrentFontSize;
				return true;
			case TagUnitType.Percentage:
				return false;
			default:
				return false;
			}
		case MarkupTag.SLASH_VERTICAL_OFFSET:
			m_BaselineOffset = 0f;
			return true;
		case MarkupTag.PAGE:
			if (generationSettings.overflowMode == TextOverflowMode.Page)
			{
				m_XAdvance = 0f + m_TagLineIndent + m_TagIndent;
				m_LineOffset = 0f;
				m_PageNumber++;
				m_IsNewPage = true;
			}
			return true;
		case MarkupTag.NO_BREAK:
			m_IsNonBreakingSpace = true;
			return true;
		case MarkupTag.SLASH_NO_BREAK:
			m_IsNonBreakingSpace = false;
			return true;
		case MarkupTag.SIZE:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				if (m_HtmlTag[5] == '+')
				{
					m_CurrentFontSize = m_FontSize + num3;
					m_SizeStack.Add(m_CurrentFontSize);
					return true;
				}
				if (m_HtmlTag[5] == '-')
				{
					m_CurrentFontSize = m_FontSize + num3;
					m_SizeStack.Add(m_CurrentFontSize);
					return true;
				}
				m_CurrentFontSize = num3;
				m_SizeStack.Add(m_CurrentFontSize);
				return true;
			case TagUnitType.FontUnits:
				m_CurrentFontSize = m_FontSize * num3;
				m_SizeStack.Add(m_CurrentFontSize);
				return true;
			case TagUnitType.Percentage:
				m_CurrentFontSize = m_FontSize * num3 / 100f;
				m_SizeStack.Add(m_CurrentFontSize);
				return true;
			default:
				return false;
			}
		case MarkupTag.SLASH_SIZE:
			m_CurrentFontSize = m_SizeStack.Remove();
			return true;
		case MarkupTag.FONT:
		{
			int valueHashCode5 = m_XmlAttribute[0].valueHashCode;
			int nameHashCode3 = m_XmlAttribute[1].nameHashCode;
			int valueHashCode2 = m_XmlAttribute[1].valueHashCode;
			if (valueHashCode5 == -620974005)
			{
				m_CurrentFontAsset = m_MaterialReferences[0].fontAsset;
				m_CurrentMaterial = m_MaterialReferences[0].material;
				m_CurrentMaterialIndex = 0;
				m_MaterialReferenceStack.Add(m_MaterialReferences[0]);
				return true;
			}
			MaterialReferenceManager.TryGetFontAsset(valueHashCode5, out var fontAsset);
			if (fontAsset == null)
			{
				if (fontAsset == null)
				{
					fontAsset = Resources.Load<FontAsset>(textSettings.defaultFontAssetPath + new string(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength));
				}
				if (fontAsset == null)
				{
					return false;
				}
				MaterialReferenceManager.AddFontAsset(fontAsset);
			}
			if (nameHashCode3 == 0 && valueHashCode2 == 0)
			{
				m_CurrentMaterial = fontAsset.material;
				m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(m_CurrentMaterial, fontAsset, ref m_MaterialReferences, m_MaterialReferenceIndexLookup);
				m_MaterialReferenceStack.Add(m_MaterialReferences[m_CurrentMaterialIndex]);
			}
			else
			{
				if (nameHashCode3 != 825491659)
				{
					return false;
				}
				if (MaterialReferenceManager.TryGetMaterial(valueHashCode2, out material))
				{
					m_CurrentMaterial = material;
					m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(m_CurrentMaterial, fontAsset, ref m_MaterialReferences, m_MaterialReferenceIndexLookup);
					m_MaterialReferenceStack.Add(m_MaterialReferences[m_CurrentMaterialIndex]);
				}
				else
				{
					material = Resources.Load<Material>(textSettings.defaultFontAssetPath + new string(m_HtmlTag, m_XmlAttribute[1].valueStartIndex, m_XmlAttribute[1].valueLength));
					if (material == null)
					{
						return false;
					}
					MaterialReferenceManager.AddFontMaterial(valueHashCode2, material);
					m_CurrentMaterial = material;
					m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(m_CurrentMaterial, fontAsset, ref m_MaterialReferences, m_MaterialReferenceIndexLookup);
					m_MaterialReferenceStack.Add(m_MaterialReferences[m_CurrentMaterialIndex]);
				}
			}
			m_CurrentFontAsset = fontAsset;
			return true;
		}
		case MarkupTag.SLASH_FONT:
		{
			MaterialReference materialReference2 = m_MaterialReferenceStack.Remove();
			m_CurrentFontAsset = materialReference2.fontAsset;
			m_CurrentMaterial = materialReference2.material;
			m_CurrentMaterialIndex = materialReference2.index;
			return true;
		}
		case MarkupTag.MATERIAL:
		{
			int valueHashCode2 = m_XmlAttribute[0].valueHashCode;
			if (valueHashCode2 == -620974005)
			{
				m_CurrentMaterial = m_MaterialReferences[0].material;
				m_CurrentMaterialIndex = 0;
				m_MaterialReferenceStack.Add(m_MaterialReferences[0]);
				return true;
			}
			if (MaterialReferenceManager.TryGetMaterial(valueHashCode2, out material))
			{
				m_CurrentMaterial = material;
				m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(m_CurrentMaterial, m_CurrentFontAsset, ref m_MaterialReferences, m_MaterialReferenceIndexLookup);
				m_MaterialReferenceStack.Add(m_MaterialReferences[m_CurrentMaterialIndex]);
			}
			else
			{
				material = Resources.Load<Material>(textSettings.defaultFontAssetPath + new string(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength));
				if (material == null)
				{
					return false;
				}
				MaterialReferenceManager.AddFontMaterial(valueHashCode2, material);
				m_CurrentMaterial = material;
				m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(m_CurrentMaterial, m_CurrentFontAsset, ref m_MaterialReferences, m_MaterialReferenceIndexLookup);
				m_MaterialReferenceStack.Add(m_MaterialReferences[m_CurrentMaterialIndex]);
			}
			return true;
		}
		case MarkupTag.SLASH_MATERIAL:
		{
			MaterialReference materialReference = m_MaterialReferenceStack.Remove();
			m_CurrentMaterial = materialReference.material;
			m_CurrentMaterialIndex = materialReference.index;
			return true;
		}
		case MarkupTag.SPACE:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_XAdvance += num3 * (generationSettings.isOrthographic ? 1f : 0.1f);
				return true;
			case TagUnitType.FontUnits:
				m_XAdvance += num3 * (generationSettings.isOrthographic ? 1f : 0.1f) * m_CurrentFontSize;
				return true;
			case TagUnitType.Percentage:
				return false;
			default:
				return false;
			}
		case MarkupTag.ALPHA:
			if (m_XmlAttribute[0].valueLength != 3)
			{
				return false;
			}
			m_HtmlColor.a = (byte)(TextGeneratorUtilities.HexToInt(m_HtmlTag[7]) * 16 + TextGeneratorUtilities.HexToInt(m_HtmlTag[8]));
			return true;
		case MarkupTag.A:
			if (m_isTextLayoutPhase && !m_IsCalculatingPreferredValues)
			{
				if (m_XmlAttribute[1].nameHashCode == 2535353)
				{
					int linkCount2 = textInfo.linkCount;
					if (linkCount2 + 1 > textInfo.linkInfo.Length)
					{
						TextInfo.Resize(ref textInfo.linkInfo, linkCount2 + 1);
					}
					textInfo.linkInfo[linkCount2].hashCode = 2535353;
					textInfo.linkInfo[linkCount2].linkTextfirstCharacterIndex = m_CharacterCount;
					textInfo.linkInfo[linkCount2].linkIdFirstCharacterIndex = startIndex + m_XmlAttribute[1].valueStartIndex;
					textInfo.linkInfo[linkCount2].SetLinkId(m_HtmlTag, m_XmlAttribute[1].valueStartIndex, m_XmlAttribute[1].valueLength);
				}
				textInfo.linkCount++;
			}
			return true;
		case MarkupTag.SLASH_A:
			if (m_isTextLayoutPhase && !m_IsCalculatingPreferredValues)
			{
				if (textInfo.linkInfo.Length == 0 || textInfo.linkCount <= 0)
				{
					if (generationSettings.textSettings.displayWarnings)
					{
						Debug.LogWarning("There seems to be an issue with the formatting of the <a> tag. Possible issues include: missing or misplaced closing '>', missing or incorrect attribute, or unclosed quotes for attribute values. Please review the tag syntax.");
					}
				}
				else
				{
					int num6 = textInfo.linkCount - 1;
					textInfo.linkInfo[num6].linkTextLength = m_CharacterCount - textInfo.linkInfo[num6].linkTextfirstCharacterIndex;
				}
			}
			return true;
		case MarkupTag.LINK:
			if (m_isTextLayoutPhase && !m_IsCalculatingPreferredValues)
			{
				int linkCount = textInfo.linkCount;
				if (linkCount + 1 > textInfo.linkInfo.Length)
				{
					TextInfo.Resize(ref textInfo.linkInfo, linkCount + 1);
				}
				textInfo.linkInfo[linkCount].hashCode = m_XmlAttribute[0].valueHashCode;
				textInfo.linkInfo[linkCount].linkTextfirstCharacterIndex = m_CharacterCount;
				textInfo.linkInfo[linkCount].linkIdFirstCharacterIndex = startIndex + m_XmlAttribute[0].valueStartIndex;
				textInfo.linkInfo[linkCount].SetLinkId(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			}
			return true;
		case MarkupTag.SLASH_LINK:
			if (m_isTextLayoutPhase && !m_IsCalculatingPreferredValues && textInfo.linkCount < textInfo.linkInfo.Length)
			{
				textInfo.linkInfo[textInfo.linkCount].linkTextLength = m_CharacterCount - textInfo.linkInfo[textInfo.linkCount].linkTextfirstCharacterIndex;
				textInfo.linkCount++;
			}
			return true;
		case MarkupTag.ALIGN:
			switch ((MarkupTag)m_XmlAttribute[0].valueHashCode)
			{
			case MarkupTag.LEFT:
				m_LineJustification = TextAlignment.MiddleLeft;
				m_LineJustificationStack.Add(m_LineJustification);
				return true;
			case MarkupTag.RIGHT:
				m_LineJustification = TextAlignment.MiddleRight;
				m_LineJustificationStack.Add(m_LineJustification);
				return true;
			case MarkupTag.CENTER:
				m_LineJustification = TextAlignment.MiddleCenter;
				m_LineJustificationStack.Add(m_LineJustification);
				return true;
			case MarkupTag.JUSTIFIED:
				m_LineJustification = TextAlignment.MiddleJustified;
				m_LineJustificationStack.Add(m_LineJustification);
				return true;
			case MarkupTag.FLUSH:
				m_LineJustification = TextAlignment.MiddleFlush;
				m_LineJustificationStack.Add(m_LineJustification);
				return true;
			default:
				return false;
			}
		case MarkupTag.SLASH_ALIGN:
			m_LineJustification = m_LineJustificationStack.Remove();
			return true;
		case MarkupTag.WIDTH:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_Width = num3 * (generationSettings.isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				return false;
			case TagUnitType.Percentage:
				m_Width = m_MarginWidth * num3 / 100f;
				break;
			}
			return true;
		case MarkupTag.SLASH_WIDTH:
			m_Width = -1f;
			return true;
		case MarkupTag.COLOR:
			textInfo.hasMultipleColors = true;
			if (m_HtmlTag[6] == '#' && num == 10)
			{
				m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(m_HtmlTag, num);
				m_ColorStack.Add(m_HtmlColor);
				return true;
			}
			if (m_HtmlTag[6] == '#' && num == 11)
			{
				m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(m_HtmlTag, num);
				m_ColorStack.Add(m_HtmlColor);
				return true;
			}
			if (m_HtmlTag[6] == '#' && num == 13)
			{
				m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(m_HtmlTag, num);
				m_ColorStack.Add(m_HtmlColor);
				return true;
			}
			if (m_HtmlTag[6] == '#' && num == 15)
			{
				m_HtmlColor = TextGeneratorUtilities.HexCharsToColor(m_HtmlTag, num);
				m_ColorStack.Add(m_HtmlColor);
				return true;
			}
			switch (m_XmlAttribute[0].valueHashCode)
			{
			case 91635:
				m_HtmlColor = Color.red;
				m_ColorStack.Add(m_HtmlColor);
				return true;
			case -992792864:
				m_HtmlColor = new Color32(173, 216, 230, byte.MaxValue);
				m_ColorStack.Add(m_HtmlColor);
				return true;
			case 2457214:
				m_HtmlColor = Color.blue;
				m_ColorStack.Add(m_HtmlColor);
				return true;
			case 3680713:
				m_HtmlColor = new Color32(128, 128, 128, byte.MaxValue);
				m_ColorStack.Add(m_HtmlColor);
				return true;
			case 81074727:
				m_HtmlColor = Color.black;
				m_ColorStack.Add(m_HtmlColor);
				return true;
			case 87065851:
				m_HtmlColor = Color.green;
				m_ColorStack.Add(m_HtmlColor);
				return true;
			case 105680263:
				m_HtmlColor = Color.white;
				m_ColorStack.Add(m_HtmlColor);
				return true;
			case -1108587920:
				m_HtmlColor = new Color32(byte.MaxValue, 128, 0, byte.MaxValue);
				m_ColorStack.Add(m_HtmlColor);
				return true;
			case -1250222130:
				m_HtmlColor = new Color32(160, 32, 240, byte.MaxValue);
				m_ColorStack.Add(m_HtmlColor);
				return true;
			case -882444668:
				m_HtmlColor = Color.yellow;
				m_ColorStack.Add(m_HtmlColor);
				return true;
			default:
				return false;
			}
		case MarkupTag.GRADIENT:
		{
			int valueHashCode4 = m_XmlAttribute[0].valueHashCode;
			if (MaterialReferenceManager.TryGetColorGradientPreset(valueHashCode4, out var gradientPreset))
			{
				m_ColorGradientPreset = gradientPreset;
			}
			else
			{
				if (gradientPreset == null)
				{
					gradientPreset = Resources.Load<TextColorGradient>(textSettings.defaultColorGradientPresetsPath + new string(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength));
				}
				if (gradientPreset == null)
				{
					return false;
				}
				MaterialReferenceManager.AddColorGradientPreset(valueHashCode4, gradientPreset);
				m_ColorGradientPreset = gradientPreset;
			}
			m_ColorGradientPresetIsTinted = false;
			for (int m = 1; m < m_XmlAttribute.Length && m_XmlAttribute[m].nameHashCode != 0; m++)
			{
				int nameHashCode2 = m_XmlAttribute[m].nameHashCode;
				MarkupTag markupTag = (MarkupTag)nameHashCode2;
				MarkupTag markupTag2 = markupTag;
				if (markupTag2 == MarkupTag.TINT)
				{
					m_ColorGradientPresetIsTinted = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[m].valueStartIndex, m_XmlAttribute[m].valueLength) != 0f;
				}
			}
			m_ColorGradientStack.Add(m_ColorGradientPreset);
			return true;
		}
		case MarkupTag.SLASH_GRADIENT:
			m_ColorGradientPreset = m_ColorGradientStack.Remove();
			return true;
		case MarkupTag.CHARACTER_SPACE:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_CSpacing = num3 * (generationSettings.isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				m_CSpacing = num3 * (generationSettings.isOrthographic ? 1f : 0.1f) * m_CurrentFontSize;
				break;
			case TagUnitType.Percentage:
				return false;
			}
			return true;
		case MarkupTag.SLASH_CHARACTER_SPACE:
			if (!m_isTextLayoutPhase)
			{
				return true;
			}
			if (m_CharacterCount > 0)
			{
				m_XAdvance -= m_CSpacing;
				textInfo.textElementInfo[m_CharacterCount - 1].xAdvance = m_XAdvance;
			}
			m_CSpacing = 0f;
			return true;
		case MarkupTag.MONOSPACE:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_MonoSpacing = num3 * (generationSettings.isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				m_MonoSpacing = num3 * (generationSettings.isOrthographic ? 1f : 0.1f) * m_CurrentFontSize;
				break;
			case TagUnitType.Percentage:
				return false;
			}
			return true;
		case MarkupTag.SLASH_MONOSPACE:
			m_MonoSpacing = 0f;
			return true;
		case MarkupTag.CLASS:
			return false;
		case MarkupTag.SLASH_COLOR:
			m_HtmlColor = m_ColorStack.Remove();
			return true;
		case MarkupTag.INDENT:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_TagIndent = num3 * (generationSettings.isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				m_TagIndent = num3 * (generationSettings.isOrthographic ? 1f : 0.1f) * m_CurrentFontSize;
				break;
			case TagUnitType.Percentage:
				m_TagIndent = m_MarginWidth * num3 / 100f;
				break;
			}
			m_IndentStack.Add(m_TagIndent);
			m_XAdvance = m_TagIndent;
			return true;
		case MarkupTag.SLASH_INDENT:
			m_TagIndent = m_IndentStack.Remove();
			return true;
		case MarkupTag.LINE_INDENT:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_TagLineIndent = num3 * (generationSettings.isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				m_TagLineIndent = num3 * (generationSettings.isOrthographic ? 1f : 0.1f) * m_CurrentFontSize;
				break;
			case TagUnitType.Percentage:
				m_TagLineIndent = m_MarginWidth * num3 / 100f;
				break;
			}
			m_XAdvance += m_TagLineIndent;
			return true;
		case MarkupTag.SLASH_LINE_INDENT:
			m_TagLineIndent = 0f;
			return true;
		case MarkupTag.SPRITE:
		{
			int valueHashCode3 = m_XmlAttribute[0].valueHashCode;
			m_SpriteIndex = -1;
			SpriteAsset spriteAsset;
			if (m_XmlAttribute[0].valueType == TagValueType.None || m_XmlAttribute[0].valueType == TagValueType.NumericalValue)
			{
				if (generationSettings.spriteAsset != null)
				{
					m_CurrentSpriteAsset = generationSettings.spriteAsset;
				}
				else if (textSettings.defaultSpriteAsset != null)
				{
					m_CurrentSpriteAsset = textSettings.defaultSpriteAsset;
				}
				else if (m_DefaultSpriteAsset != null)
				{
					m_CurrentSpriteAsset = m_DefaultSpriteAsset;
				}
				else if (m_DefaultSpriteAsset == null)
				{
					m_DefaultSpriteAsset = Resources.Load<SpriteAsset>("Sprite Assets/Default Sprite Asset");
					m_CurrentSpriteAsset = m_DefaultSpriteAsset;
				}
				if (m_CurrentSpriteAsset == null)
				{
					return false;
				}
			}
			else if (MaterialReferenceManager.TryGetSpriteAsset(valueHashCode3, out spriteAsset))
			{
				m_CurrentSpriteAsset = spriteAsset;
			}
			else
			{
				if (spriteAsset == null && spriteAsset == null)
				{
					spriteAsset = Resources.Load<SpriteAsset>(textSettings.defaultSpriteAssetPath + new string(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength));
				}
				if (spriteAsset == null)
				{
					return false;
				}
				MaterialReferenceManager.AddSpriteAsset(valueHashCode3, spriteAsset);
				m_CurrentSpriteAsset = spriteAsset;
			}
			if (m_XmlAttribute[0].valueType == TagValueType.NumericalValue)
			{
				int num5 = (int)TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
				if (num5 == -32768)
				{
					return false;
				}
				if (num5 > m_CurrentSpriteAsset.spriteCharacterTable.Count - 1)
				{
					return false;
				}
				m_SpriteIndex = num5;
			}
			m_SpriteColor = Color.white;
			m_TintSprite = false;
			for (int l = 0; l < m_XmlAttribute.Length && m_XmlAttribute[l].nameHashCode != 0; l++)
			{
				int nameHashCode = m_XmlAttribute[l].nameHashCode;
				int spriteIndex = 0;
				switch ((MarkupTag)nameHashCode)
				{
				case MarkupTag.NAME:
					m_CurrentSpriteAsset = SpriteAsset.SearchForSpriteByHashCode(m_CurrentSpriteAsset, m_XmlAttribute[l].valueHashCode, includeFallbacks: true, out spriteIndex);
					if (spriteIndex == -1)
					{
						return false;
					}
					m_SpriteIndex = spriteIndex;
					break;
				case MarkupTag.INDEX:
					spriteIndex = (int)TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[1].valueStartIndex, m_XmlAttribute[1].valueLength);
					if (spriteIndex == -32768)
					{
						return false;
					}
					if (spriteIndex > m_CurrentSpriteAsset.spriteCharacterTable.Count - 1)
					{
						return false;
					}
					m_SpriteIndex = spriteIndex;
					break;
				case MarkupTag.TINT:
					m_TintSprite = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[l].valueStartIndex, m_XmlAttribute[l].valueLength) != 0f;
					break;
				case MarkupTag.COLOR:
					m_SpriteColor = TextGeneratorUtilities.HexCharsToColor(m_HtmlTag, m_XmlAttribute[l].valueStartIndex, m_XmlAttribute[l].valueLength);
					break;
				case MarkupTag.ANIM:
				{
					int attributeParameters2 = TextGeneratorUtilities.GetAttributeParameters(m_HtmlTag, m_XmlAttribute[l].valueStartIndex, m_XmlAttribute[l].valueLength, ref m_AttributeParameterValues);
					if (attributeParameters2 != 3)
					{
						return false;
					}
					m_SpriteIndex = (int)m_AttributeParameterValues[0];
					if (!m_isTextLayoutPhase)
					{
					}
					break;
				}
				default:
					if (nameHashCode != -991527447)
					{
						return false;
					}
					break;
				}
			}
			if (m_SpriteIndex == -1)
			{
				return false;
			}
			m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(m_CurrentSpriteAsset.material, m_CurrentSpriteAsset, ref m_MaterialReferences, m_MaterialReferenceIndexLookup);
			m_TextElementType = TextElementType.Sprite;
			return true;
		}
		case MarkupTag.LOWERCASE:
			m_FontStyleInternal |= FontStyles.LowerCase;
			m_FontStyleStack.Add(FontStyles.LowerCase);
			return true;
		case MarkupTag.SLASH_LOWERCASE:
			if ((generationSettings.fontStyle & FontStyles.LowerCase) != FontStyles.LowerCase && m_FontStyleStack.Remove(FontStyles.LowerCase) == 0)
			{
				m_FontStyleInternal &= ~FontStyles.LowerCase;
			}
			return true;
		case MarkupTag.UPPERCASE:
		case MarkupTag.ALLCAPS:
			m_FontStyleInternal |= FontStyles.UpperCase;
			m_FontStyleStack.Add(FontStyles.UpperCase);
			return true;
		case MarkupTag.SLASH_ALLCAPS:
		case MarkupTag.SLASH_UPPERCASE:
			if ((generationSettings.fontStyle & FontStyles.UpperCase) != FontStyles.UpperCase && m_FontStyleStack.Remove(FontStyles.UpperCase) == 0)
			{
				m_FontStyleInternal &= ~FontStyles.UpperCase;
			}
			return true;
		case MarkupTag.SMALLCAPS:
			m_FontStyleInternal |= FontStyles.SmallCaps;
			m_FontStyleStack.Add(FontStyles.SmallCaps);
			return true;
		case MarkupTag.SLASH_SMALLCAPS:
			if ((generationSettings.fontStyle & FontStyles.SmallCaps) != FontStyles.SmallCaps && m_FontStyleStack.Remove(FontStyles.SmallCaps) == 0)
			{
				m_FontStyleInternal &= ~FontStyles.SmallCaps;
			}
			return true;
		case MarkupTag.MARGIN:
			switch (m_XmlAttribute[0].valueType)
			{
			case TagValueType.NumericalValue:
				num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
				if (num3 == -32768f)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					m_MarginLeft = num3 * (generationSettings.isOrthographic ? 1f : 0.1f);
					break;
				case TagUnitType.FontUnits:
					m_MarginLeft = num3 * (generationSettings.isOrthographic ? 1f : 0.1f) * m_CurrentFontSize;
					break;
				case TagUnitType.Percentage:
					m_MarginLeft = (m_MarginWidth - ((m_Width != -1f) ? m_Width : 0f)) * num3 / 100f;
					break;
				}
				m_MarginLeft = ((m_MarginLeft >= 0f) ? m_MarginLeft : 0f);
				m_MarginRight = m_MarginLeft;
				return true;
			case TagValueType.None:
			{
				for (int k = 1; k < m_XmlAttribute.Length && m_XmlAttribute[k].nameHashCode != 0; k++)
				{
					switch ((MarkupTag)m_XmlAttribute[k].nameHashCode)
					{
					case MarkupTag.LEFT:
						num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[k].valueStartIndex, m_XmlAttribute[k].valueLength);
						if (num3 == -32768f)
						{
							return false;
						}
						switch (m_XmlAttribute[k].unitType)
						{
						case TagUnitType.Pixels:
							m_MarginLeft = num3 * (generationSettings.isOrthographic ? 1f : 0.1f);
							break;
						case TagUnitType.FontUnits:
							m_MarginLeft = num3 * (generationSettings.isOrthographic ? 1f : 0.1f) * m_CurrentFontSize;
							break;
						case TagUnitType.Percentage:
							m_MarginLeft = (m_MarginWidth - ((m_Width != -1f) ? m_Width : 0f)) * num3 / 100f;
							break;
						}
						m_MarginLeft = ((m_MarginLeft >= 0f) ? m_MarginLeft : 0f);
						break;
					case MarkupTag.RIGHT:
						num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[k].valueStartIndex, m_XmlAttribute[k].valueLength);
						if (num3 == -32768f)
						{
							return false;
						}
						switch (m_XmlAttribute[k].unitType)
						{
						case TagUnitType.Pixels:
							m_MarginRight = num3 * (generationSettings.isOrthographic ? 1f : 0.1f);
							break;
						case TagUnitType.FontUnits:
							m_MarginRight = num3 * (generationSettings.isOrthographic ? 1f : 0.1f) * m_CurrentFontSize;
							break;
						case TagUnitType.Percentage:
							m_MarginRight = (m_MarginWidth - ((m_Width != -1f) ? m_Width : 0f)) * num3 / 100f;
							break;
						}
						m_MarginRight = ((m_MarginRight >= 0f) ? m_MarginRight : 0f);
						break;
					}
				}
				return true;
			}
			default:
				return false;
			}
		case MarkupTag.SLASH_MARGIN:
			m_MarginLeft = 0f;
			m_MarginRight = 0f;
			return true;
		case MarkupTag.MARGIN_LEFT:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_MarginLeft = num3 * (generationSettings.isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				m_MarginLeft = num3 * (generationSettings.isOrthographic ? 1f : 0.1f) * m_CurrentFontSize;
				break;
			case TagUnitType.Percentage:
				m_MarginLeft = (m_MarginWidth - ((m_Width != -1f) ? m_Width : 0f)) * num3 / 100f;
				break;
			}
			m_MarginLeft = ((m_MarginLeft >= 0f) ? m_MarginLeft : 0f);
			return true;
		case MarkupTag.MARGIN_RIGHT:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_MarginRight = num3 * (generationSettings.isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				m_MarginRight = num3 * (generationSettings.isOrthographic ? 1f : 0.1f) * m_CurrentFontSize;
				break;
			case TagUnitType.Percentage:
				m_MarginRight = (m_MarginWidth - ((m_Width != -1f) ? m_Width : 0f)) * num3 / 100f;
				break;
			}
			m_MarginRight = ((m_MarginRight >= 0f) ? m_MarginRight : 0f);
			return true;
		case MarkupTag.LINE_HEIGHT:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			switch (tagUnitType)
			{
			case TagUnitType.Pixels:
				m_LineHeight = num3 * (generationSettings.isOrthographic ? 1f : 0.1f);
				break;
			case TagUnitType.FontUnits:
				m_LineHeight = num3 * (generationSettings.isOrthographic ? 1f : 0.1f) * m_CurrentFontSize;
				break;
			case TagUnitType.Percentage:
			{
				float num4 = m_CurrentFontSize / (float)m_CurrentFontAsset.faceInfo.pointSize * m_CurrentFontAsset.faceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
				m_LineHeight = generationSettings.fontAsset.faceInfo.lineHeight * num3 / 100f * num4;
				break;
			}
			}
			return true;
		case MarkupTag.SLASH_LINE_HEIGHT:
			m_LineHeight = -32767f;
			return true;
		case MarkupTag.NO_PARSE:
			m_TagNoParsing = true;
			return true;
		case MarkupTag.ACTION:
		{
			int valueHashCode = m_XmlAttribute[0].valueHashCode;
			if (m_isTextLayoutPhase)
			{
				m_ActionStack.Add(valueHashCode);
				Debug.Log("Action ID: [" + valueHashCode + "] First character index: " + m_CharacterCount);
			}
			return true;
		}
		case MarkupTag.SLASH_ACTION:
			if (m_isTextLayoutPhase)
			{
				Debug.Log("Action ID: [" + m_ActionStack.CurrentItem() + "] Last character index: " + (m_CharacterCount - 1));
			}
			m_ActionStack.Remove();
			return true;
		case MarkupTag.SCALE:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			m_FXScale = new Vector3(num3, 1f, 1f);
			return true;
		case MarkupTag.SLASH_SCALE:
			m_FXScale = Vector3.one;
			return true;
		case MarkupTag.ROTATE:
			num3 = TextGeneratorUtilities.ConvertToFloat(m_HtmlTag, m_XmlAttribute[0].valueStartIndex, m_XmlAttribute[0].valueLength);
			if (num3 == -32768f)
			{
				return false;
			}
			m_FXRotation = Quaternion.Euler(0f, 0f, num3);
			return true;
		case MarkupTag.SLASH_ROTATE:
			m_FXRotation = Quaternion.identity;
			return true;
		case MarkupTag.TABLE:
			return false;
		case MarkupTag.SLASH_TABLE:
			return false;
		case MarkupTag.TR:
			return false;
		case MarkupTag.SLASH_TR:
			return false;
		case MarkupTag.TH:
			return false;
		case MarkupTag.SLASH_TH:
			return false;
		case MarkupTag.TD:
			return false;
		case MarkupTag.SLASH_TD:
			return false;
		default:
			return false;
		}
	}

	private void SaveGlyphVertexInfo(float padding, float stylePadding, Color32 vertexColor, TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		textInfo.textElementInfo[m_CharacterCount].vertexBottomLeft.position = textInfo.textElementInfo[m_CharacterCount].bottomLeft;
		textInfo.textElementInfo[m_CharacterCount].vertexTopLeft.position = textInfo.textElementInfo[m_CharacterCount].topLeft;
		textInfo.textElementInfo[m_CharacterCount].vertexTopRight.position = textInfo.textElementInfo[m_CharacterCount].topRight;
		textInfo.textElementInfo[m_CharacterCount].vertexBottomRight.position = textInfo.textElementInfo[m_CharacterCount].bottomRight;
		vertexColor.a = ((m_FontColor32.a < vertexColor.a) ? m_FontColor32.a : vertexColor.a);
		bool flag = false;
		if (generationSettings.fontColorGradient == null || flag)
		{
			vertexColor = (flag ? new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, vertexColor.a) : vertexColor);
			textInfo.textElementInfo[m_CharacterCount].vertexBottomLeft.color = vertexColor;
			textInfo.textElementInfo[m_CharacterCount].vertexTopLeft.color = vertexColor;
			textInfo.textElementInfo[m_CharacterCount].vertexTopRight.color = vertexColor;
			textInfo.textElementInfo[m_CharacterCount].vertexBottomRight.color = vertexColor;
		}
		else if (!generationSettings.overrideRichTextColors && m_ColorStack.index > 1)
		{
			textInfo.textElementInfo[m_CharacterCount].vertexBottomLeft.color = vertexColor;
			textInfo.textElementInfo[m_CharacterCount].vertexTopLeft.color = vertexColor;
			textInfo.textElementInfo[m_CharacterCount].vertexTopRight.color = vertexColor;
			textInfo.textElementInfo[m_CharacterCount].vertexBottomRight.color = vertexColor;
		}
		else if (generationSettings.fontColorGradientPreset != null)
		{
			textInfo.textElementInfo[m_CharacterCount].vertexBottomLeft.color = generationSettings.fontColorGradientPreset.bottomLeft * vertexColor;
			textInfo.textElementInfo[m_CharacterCount].vertexTopLeft.color = generationSettings.fontColorGradientPreset.topLeft * vertexColor;
			textInfo.textElementInfo[m_CharacterCount].vertexTopRight.color = generationSettings.fontColorGradientPreset.topRight * vertexColor;
			textInfo.textElementInfo[m_CharacterCount].vertexBottomRight.color = generationSettings.fontColorGradientPreset.bottomRight * vertexColor;
		}
		else
		{
			textInfo.textElementInfo[m_CharacterCount].vertexBottomLeft.color = generationSettings.fontColorGradient.bottomLeft * vertexColor;
			textInfo.textElementInfo[m_CharacterCount].vertexTopLeft.color = generationSettings.fontColorGradient.topLeft * vertexColor;
			textInfo.textElementInfo[m_CharacterCount].vertexTopRight.color = generationSettings.fontColorGradient.topRight * vertexColor;
			textInfo.textElementInfo[m_CharacterCount].vertexBottomRight.color = generationSettings.fontColorGradient.bottomRight * vertexColor;
		}
		if (m_ColorGradientPreset != null && !flag)
		{
			if (m_ColorGradientPresetIsTinted)
			{
				ref Color32 color = ref textInfo.textElementInfo[m_CharacterCount].vertexBottomLeft.color;
				color *= m_ColorGradientPreset.bottomLeft;
				ref Color32 color2 = ref textInfo.textElementInfo[m_CharacterCount].vertexTopLeft.color;
				color2 *= m_ColorGradientPreset.topLeft;
				ref Color32 color3 = ref textInfo.textElementInfo[m_CharacterCount].vertexTopRight.color;
				color3 *= m_ColorGradientPreset.topRight;
				ref Color32 color4 = ref textInfo.textElementInfo[m_CharacterCount].vertexBottomRight.color;
				color4 *= m_ColorGradientPreset.bottomRight;
			}
			else
			{
				textInfo.textElementInfo[m_CharacterCount].vertexBottomLeft.color = m_ColorGradientPreset.bottomLeft.MinAlpha(vertexColor);
				textInfo.textElementInfo[m_CharacterCount].vertexTopLeft.color = m_ColorGradientPreset.topLeft.MinAlpha(vertexColor);
				textInfo.textElementInfo[m_CharacterCount].vertexTopRight.color = m_ColorGradientPreset.topRight.MinAlpha(vertexColor);
				textInfo.textElementInfo[m_CharacterCount].vertexBottomRight.color = m_ColorGradientPreset.bottomRight.MinAlpha(vertexColor);
			}
		}
		stylePadding = 0f;
		GlyphRect glyphRect = textInfo.textElementInfo[m_CharacterCount].alternativeGlyph?.glyphRect ?? m_CachedTextElement.m_Glyph.glyphRect;
		Vector2 vector = default(Vector2);
		vector.x = ((float)glyphRect.x - padding - stylePadding) / (float)m_CurrentFontAsset.atlasWidth;
		vector.y = ((float)glyphRect.y - padding - stylePadding) / (float)m_CurrentFontAsset.atlasHeight;
		Vector2 vector2 = default(Vector2);
		vector2.x = vector.x;
		vector2.y = ((float)glyphRect.y + padding + stylePadding + (float)glyphRect.height) / (float)m_CurrentFontAsset.atlasHeight;
		Vector2 vector3 = default(Vector2);
		vector3.x = ((float)glyphRect.x + padding + stylePadding + (float)glyphRect.width) / (float)m_CurrentFontAsset.atlasWidth;
		vector3.y = vector2.y;
		Vector2 vector4 = default(Vector2);
		vector4.x = vector3.x;
		vector4.y = vector.y;
		textInfo.textElementInfo[m_CharacterCount].vertexBottomLeft.uv = vector;
		textInfo.textElementInfo[m_CharacterCount].vertexTopLeft.uv = vector2;
		textInfo.textElementInfo[m_CharacterCount].vertexTopRight.uv = vector3;
		textInfo.textElementInfo[m_CharacterCount].vertexBottomRight.uv = vector4;
	}

	private void SaveSpriteVertexInfo(Color32 vertexColor, TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		textInfo.textElementInfo[m_CharacterCount].vertexBottomLeft.position = textInfo.textElementInfo[m_CharacterCount].bottomLeft;
		textInfo.textElementInfo[m_CharacterCount].vertexTopLeft.position = textInfo.textElementInfo[m_CharacterCount].topLeft;
		textInfo.textElementInfo[m_CharacterCount].vertexTopRight.position = textInfo.textElementInfo[m_CharacterCount].topRight;
		textInfo.textElementInfo[m_CharacterCount].vertexBottomRight.position = textInfo.textElementInfo[m_CharacterCount].bottomRight;
		if (generationSettings.tintSprites)
		{
			m_TintSprite = true;
		}
		Color32 color = (m_TintSprite ? ColorUtilities.MultiplyColors(m_SpriteColor, vertexColor) : m_SpriteColor);
		color.a = ((color.a >= m_FontColor32.a) ? m_FontColor32.a : ((color.a < vertexColor.a) ? color.a : vertexColor.a));
		Color32 color2 = color;
		Color32 color3 = color;
		Color32 color4 = color;
		Color32 color5 = color;
		if (generationSettings.fontColorGradient != null)
		{
			if (generationSettings.fontColorGradientPreset != null)
			{
				color2 = (m_TintSprite ? ColorUtilities.MultiplyColors(color2, generationSettings.fontColorGradientPreset.bottomLeft) : color2);
				color3 = (m_TintSprite ? ColorUtilities.MultiplyColors(color3, generationSettings.fontColorGradientPreset.topLeft) : color3);
				color4 = (m_TintSprite ? ColorUtilities.MultiplyColors(color4, generationSettings.fontColorGradientPreset.topRight) : color4);
				color5 = (m_TintSprite ? ColorUtilities.MultiplyColors(color5, generationSettings.fontColorGradientPreset.bottomRight) : color5);
			}
			else
			{
				color2 = (m_TintSprite ? ColorUtilities.MultiplyColors(color2, generationSettings.fontColorGradient.bottomLeft) : color2);
				color3 = (m_TintSprite ? ColorUtilities.MultiplyColors(color3, generationSettings.fontColorGradient.topLeft) : color3);
				color4 = (m_TintSprite ? ColorUtilities.MultiplyColors(color4, generationSettings.fontColorGradient.topRight) : color4);
				color5 = (m_TintSprite ? ColorUtilities.MultiplyColors(color5, generationSettings.fontColorGradient.bottomRight) : color5);
			}
		}
		if (m_ColorGradientPreset != null)
		{
			color2 = (m_TintSprite ? ColorUtilities.MultiplyColors(color2, m_ColorGradientPreset.bottomLeft) : color2);
			color3 = (m_TintSprite ? ColorUtilities.MultiplyColors(color3, m_ColorGradientPreset.topLeft) : color3);
			color4 = (m_TintSprite ? ColorUtilities.MultiplyColors(color4, m_ColorGradientPreset.topRight) : color4);
			color5 = (m_TintSprite ? ColorUtilities.MultiplyColors(color5, m_ColorGradientPreset.bottomRight) : color5);
		}
		textInfo.textElementInfo[m_CharacterCount].vertexBottomLeft.color = color2;
		textInfo.textElementInfo[m_CharacterCount].vertexTopLeft.color = color3;
		textInfo.textElementInfo[m_CharacterCount].vertexTopRight.color = color4;
		textInfo.textElementInfo[m_CharacterCount].vertexBottomRight.color = color5;
		Vector2 vector = new Vector2((float)m_CachedTextElement.glyph.glyphRect.x / (float)m_CurrentSpriteAsset.spriteSheet.width, (float)m_CachedTextElement.glyph.glyphRect.y / (float)m_CurrentSpriteAsset.spriteSheet.height);
		Vector2 vector2 = new Vector2(vector.x, (float)(m_CachedTextElement.glyph.glyphRect.y + m_CachedTextElement.glyph.glyphRect.height) / (float)m_CurrentSpriteAsset.spriteSheet.height);
		Vector2 vector3 = new Vector2((float)(m_CachedTextElement.glyph.glyphRect.x + m_CachedTextElement.glyph.glyphRect.width) / (float)m_CurrentSpriteAsset.spriteSheet.width, vector2.y);
		Vector2 vector4 = new Vector2(vector3.x, vector.y);
		textInfo.textElementInfo[m_CharacterCount].vertexBottomLeft.uv = vector;
		textInfo.textElementInfo[m_CharacterCount].vertexTopLeft.uv = vector2;
		textInfo.textElementInfo[m_CharacterCount].vertexTopRight.uv = vector3;
		textInfo.textElementInfo[m_CharacterCount].vertexBottomRight.uv = vector4;
	}

	private void DrawUnderlineMesh(Vector3 start, Vector3 end, float startScale, float endScale, float maxScale, float sdfScale, Color32 underlineColor, TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		GetUnderlineSpecialCharacter(generationSettings);
		if (m_Underline.character == null)
		{
			if (generationSettings.textSettings.displayWarnings)
			{
				Debug.LogWarning("Unable to add underline or strikethrough since the character [0x5F] used by these features is not present in the Font Asset assigned to this text object.");
			}
			return;
		}
		int vertexCount = textInfo.meshInfo[m_CurrentMaterialIndex].vertexCount;
		int num = vertexCount + 12;
		if (num > textInfo.meshInfo[m_CurrentMaterialIndex].vertices.Length)
		{
			textInfo.meshInfo[m_CurrentMaterialIndex].ResizeMeshInfo(num / 4);
		}
		start.y = Mathf.Min(start.y, end.y);
		end.y = Mathf.Min(start.y, end.y);
		GlyphMetrics metrics = m_Underline.character.glyph.metrics;
		GlyphRect glyphRect = m_Underline.character.glyph.glyphRect;
		start.x += (startScale - maxScale) * m_Padding;
		end.x += (maxScale - endScale) * m_Padding;
		float num2 = (metrics.width * 0.5f + m_Padding) * maxScale;
		float num3 = 1f;
		float num4 = 2f * num2;
		float num5 = end.x - start.x;
		if (num5 < num4)
		{
			num3 = num5 / num4;
			num2 *= num3;
		}
		float underlineThickness = m_Underline.fontAsset.faceInfo.underlineThickness;
		float x = start.x;
		float x2 = start.x + num2;
		float x3 = end.x - num2;
		float x4 = end.x;
		float y = start.y - (underlineThickness + m_Padding) * maxScale;
		float y2 = start.y + m_Padding * maxScale;
		Vector3[] vertices = textInfo.meshInfo[m_CurrentMaterialIndex].vertices;
		vertices[vertexCount] = new Vector3(x, y);
		vertices[vertexCount + 1] = new Vector3(x, y2);
		vertices[vertexCount + 2] = new Vector3(x2, y2);
		vertices[vertexCount + 3] = new Vector3(x2, y);
		vertices[vertexCount + 4] = new Vector3(x2, y);
		vertices[vertexCount + 5] = new Vector3(x2, y2);
		vertices[vertexCount + 6] = new Vector3(x3, y2);
		vertices[vertexCount + 7] = new Vector3(x3, y);
		vertices[vertexCount + 8] = new Vector3(x3, y);
		vertices[vertexCount + 9] = new Vector3(x3, y2);
		vertices[vertexCount + 10] = new Vector3(x4, y2);
		vertices[vertexCount + 11] = new Vector3(x4, y);
		if (generationSettings.inverseYAxis)
		{
			Vector3 vector = default(Vector3);
			vector.x = 0f;
			vector.y = generationSettings.screenRect.y + generationSettings.screenRect.height;
			vector.z = 0f;
			for (int i = 0; i < 12; i++)
			{
				vertices[vertexCount + i].y = vertices[vertexCount + i].y * -1f + vector.y;
			}
		}
		float num6 = 1f / (float)m_Underline.fontAsset.atlasWidth;
		float num7 = 1f / (float)m_Underline.fontAsset.atlasHeight;
		float num8 = ((float)glyphRect.width * 0.5f + m_Padding) * num3 * num6;
		float num9 = ((float)glyphRect.x - m_Padding) * num6;
		float x5 = num9 + num8;
		float x6 = ((float)glyphRect.x + (float)glyphRect.width * 0.5f) * num6;
		float num10 = ((float)(glyphRect.x + glyphRect.width) + m_Padding) * num6;
		float x7 = num10 - num8;
		float y3 = ((float)glyphRect.y - m_Padding) * num7;
		float y4 = ((float)(glyphRect.y + glyphRect.height) + m_Padding) * num7;
		float num11 = Mathf.Abs(sdfScale);
		Vector4[] uvs = textInfo.meshInfo[m_CurrentMaterialIndex].uvs0;
		uvs[vertexCount] = new Vector4(num9, y3, 0f, num11);
		uvs[1 + vertexCount] = new Vector4(num9, y4, 0f, num11);
		uvs[2 + vertexCount] = new Vector4(x5, y4, 0f, num11);
		uvs[3 + vertexCount] = new Vector4(x5, y3, 0f, num11);
		uvs[4 + vertexCount] = new Vector4(x6, y3, 0f, num11);
		uvs[5 + vertexCount] = new Vector4(x6, y4, 0f, num11);
		uvs[6 + vertexCount] = new Vector4(x6, y4, 0f, num11);
		uvs[7 + vertexCount] = new Vector4(x6, y3, 0f, num11);
		uvs[8 + vertexCount] = new Vector4(x7, y3, 0f, num11);
		uvs[9 + vertexCount] = new Vector4(x7, y4, 0f, num11);
		uvs[10 + vertexCount] = new Vector4(num10, y4, 0f, num11);
		uvs[11 + vertexCount] = new Vector4(num10, y3, 0f, num11);
		float num12 = 0f;
		float num13 = 1f / num5;
		float x8 = (vertices[vertexCount + 2].x - start.x) * num13;
		Vector2[] uvs2 = textInfo.meshInfo[m_CurrentMaterialIndex].uvs2;
		uvs2[vertexCount] = TextGeneratorUtilities.PackUV(0f, 0f, num11);
		uvs2[1 + vertexCount] = TextGeneratorUtilities.PackUV(0f, 1f, num11);
		uvs2[2 + vertexCount] = TextGeneratorUtilities.PackUV(x8, 1f, num11);
		uvs2[3 + vertexCount] = TextGeneratorUtilities.PackUV(x8, 0f, num11);
		num12 = (vertices[vertexCount + 4].x - start.x) * num13;
		x8 = (vertices[vertexCount + 6].x - start.x) * num13;
		uvs2[4 + vertexCount] = TextGeneratorUtilities.PackUV(num12, 0f, num11);
		uvs2[5 + vertexCount] = TextGeneratorUtilities.PackUV(num12, 1f, num11);
		uvs2[6 + vertexCount] = TextGeneratorUtilities.PackUV(x8, 1f, num11);
		uvs2[7 + vertexCount] = TextGeneratorUtilities.PackUV(x8, 0f, num11);
		num12 = (vertices[vertexCount + 8].x - start.x) * num13;
		uvs2[8 + vertexCount] = TextGeneratorUtilities.PackUV(num12, 0f, num11);
		uvs2[9 + vertexCount] = TextGeneratorUtilities.PackUV(num12, 1f, num11);
		uvs2[10 + vertexCount] = TextGeneratorUtilities.PackUV(1f, 1f, num11);
		uvs2[11 + vertexCount] = TextGeneratorUtilities.PackUV(1f, 0f, num11);
		underlineColor.a = ((m_FontColor32.a < underlineColor.a) ? m_FontColor32.a : underlineColor.a);
		Color32[] colors = textInfo.meshInfo[m_CurrentMaterialIndex].colors32;
		for (int j = 0; j < 12; j++)
		{
			colors[j + vertexCount] = underlineColor;
		}
		textInfo.meshInfo[m_CurrentMaterialIndex].vertexCount += 12;
	}

	private void DrawTextHighlight(Vector3 start, Vector3 end, Color32 highlightColor, TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		GetUnderlineSpecialCharacter(generationSettings);
		if (m_Underline.character == null)
		{
			if (generationSettings.textSettings.displayWarnings)
			{
				Debug.LogWarning("Unable to add highlight since the primary Font Asset doesn't contain the underline character.");
			}
			return;
		}
		int vertexCount = textInfo.meshInfo[m_CurrentMaterialIndex].vertexCount;
		int num = vertexCount + 4;
		if (num > textInfo.meshInfo[m_CurrentMaterialIndex].vertices.Length)
		{
			textInfo.meshInfo[m_CurrentMaterialIndex].ResizeMeshInfo(num / 4);
		}
		Vector3[] vertices = textInfo.meshInfo[m_CurrentMaterialIndex].vertices;
		vertices[vertexCount] = start;
		vertices[vertexCount + 1] = new Vector3(start.x, end.y, 0f);
		vertices[vertexCount + 2] = end;
		vertices[vertexCount + 3] = new Vector3(end.x, start.y, 0f);
		if (generationSettings.inverseYAxis)
		{
			Vector3 vector = default(Vector3);
			vector.x = 0f;
			vector.y = generationSettings.screenRect.y + generationSettings.screenRect.height;
			vector.z = 0f;
			vertices[vertexCount].y = vertices[vertexCount].y * -1f + vector.y;
			vertices[vertexCount + 1].y = vertices[vertexCount + 1].y * -1f + vector.y;
			vertices[vertexCount + 2].y = vertices[vertexCount + 2].y * -1f + vector.y;
			vertices[vertexCount + 3].y = vertices[vertexCount + 3].y * -1f + vector.y;
		}
		Vector4[] uvs = textInfo.meshInfo[m_CurrentMaterialIndex].uvs0;
		int atlasWidth = m_Underline.fontAsset.atlasWidth;
		int atlasHeight = m_Underline.fontAsset.atlasHeight;
		GlyphRect glyphRect = m_Underline.character.glyph.glyphRect;
		Vector2 vector2 = new Vector2(((float)glyphRect.x + (float)glyphRect.width / 2f) / (float)atlasWidth, ((float)glyphRect.y + (float)glyphRect.height / 2f) / (float)atlasHeight);
		Vector2 vector3 = new Vector2(1f / (float)atlasWidth, 1f / (float)atlasHeight);
		uvs[vertexCount] = vector2 - vector3;
		uvs[1 + vertexCount] = vector2 + new Vector2(0f - vector3.x, vector3.y);
		uvs[2 + vertexCount] = vector2 + vector3;
		uvs[3 + vertexCount] = vector2 + new Vector2(vector3.x, 0f - vector3.y);
		Vector2[] uvs2 = textInfo.meshInfo[m_CurrentMaterialIndex].uvs2;
		uvs2[3 + vertexCount] = (uvs2[2 + vertexCount] = (uvs2[1 + vertexCount] = (uvs2[vertexCount] = new Vector2(0f, 1f))));
		highlightColor.a = ((m_FontColor32.a < highlightColor.a) ? m_FontColor32.a : highlightColor.a);
		Color32[] colors = textInfo.meshInfo[m_CurrentMaterialIndex].colors32;
		colors[vertexCount] = highlightColor;
		colors[1 + vertexCount] = highlightColor;
		colors[2 + vertexCount] = highlightColor;
		colors[3 + vertexCount] = highlightColor;
		textInfo.meshInfo[m_CurrentMaterialIndex].vertexCount += 4;
	}

	private static void ClearMesh(bool updateMesh, TextInfo textInfo)
	{
		textInfo.ClearMeshInfo(updateMesh);
	}

	internal int SetArraySizes(TextProcessingElement[] textProcessingArray, TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		TextSettings textSettings = generationSettings.textSettings;
		int num = 0;
		m_TotalCharacterCount = 0;
		m_isTextLayoutPhase = false;
		m_TagNoParsing = false;
		m_FontStyleInternal = generationSettings.fontStyle;
		m_FontStyleStack.Clear();
		m_FontWeightInternal = (((m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? TextFontWeight.Bold : generationSettings.fontWeight);
		m_FontWeightStack.SetDefault(m_FontWeightInternal);
		m_CurrentFontAsset = generationSettings.fontAsset;
		m_CurrentMaterial = generationSettings.material;
		m_CurrentMaterialIndex = 0;
		m_MaterialReferenceStack.SetDefault(new MaterialReference(m_CurrentMaterialIndex, m_CurrentFontAsset, null, m_CurrentMaterial, m_Padding));
		m_MaterialReferenceIndexLookup.Clear();
		MaterialReference.AddMaterialReference(m_CurrentMaterial, m_CurrentFontAsset, ref m_MaterialReferences, m_MaterialReferenceIndexLookup);
		if (textInfo == null)
		{
			textInfo = new TextInfo();
		}
		else if (textInfo.textElementInfo.Length < m_InternalTextProcessingArraySize)
		{
			TextInfo.Resize(ref textInfo.textElementInfo, m_InternalTextProcessingArraySize, isBlockAllocated: false);
		}
		m_TextElementType = TextElementType.Character;
		if (generationSettings.overflowMode == TextOverflowMode.Ellipsis)
		{
			GetEllipsisSpecialCharacter(generationSettings);
			if (m_Ellipsis.character != null)
			{
				if (m_Ellipsis.fontAsset.GetInstanceID() != m_CurrentFontAsset.GetInstanceID())
				{
					if (textSettings.matchMaterialPreset && m_CurrentMaterial.GetInstanceID() != m_Ellipsis.fontAsset.material.GetInstanceID())
					{
						m_Ellipsis.material = MaterialManager.GetFallbackMaterial(m_CurrentMaterial, m_Ellipsis.fontAsset.material);
					}
					else
					{
						m_Ellipsis.material = m_Ellipsis.fontAsset.material;
					}
					m_Ellipsis.materialIndex = MaterialReference.AddMaterialReference(m_Ellipsis.material, m_Ellipsis.fontAsset, ref m_MaterialReferences, m_MaterialReferenceIndexLookup);
					m_MaterialReferences[m_Ellipsis.materialIndex].referenceCount = 0;
				}
			}
			else
			{
				generationSettings.overflowMode = TextOverflowMode.Truncate;
				if (textSettings.displayWarnings)
				{
					Debug.LogWarning("The character used for Ellipsis is not available in font asset [" + m_CurrentFontAsset.name + "] or any potential fallbacks. Switching Text Overflow mode to Truncate.");
				}
			}
		}
		for (int i = 0; i < textProcessingArray.Length && textProcessingArray[i].unicode != 0; i++)
		{
			if (textInfo.textElementInfo == null || m_TotalCharacterCount >= textInfo.textElementInfo.Length)
			{
				TextInfo.Resize(ref textInfo.textElementInfo, m_TotalCharacterCount + 1, isBlockAllocated: true);
			}
			uint num2 = textProcessingArray[i].unicode;
			int currentMaterialIndex = m_CurrentMaterialIndex;
			if (generationSettings.richText && num2 == 60)
			{
				currentMaterialIndex = m_CurrentMaterialIndex;
				if (ValidateHtmlTag(textProcessingArray, i + 1, out var endIndex, generationSettings, textInfo))
				{
					int stringIndex = textProcessingArray[i].stringIndex;
					i = endIndex;
					if (m_TextElementType == TextElementType.Sprite)
					{
						m_MaterialReferences[m_CurrentMaterialIndex].referenceCount++;
						textInfo.textElementInfo[m_TotalCharacterCount].character = (char)(57344 + m_SpriteIndex);
						textInfo.textElementInfo[m_TotalCharacterCount].fontAsset = m_CurrentFontAsset;
						textInfo.textElementInfo[m_TotalCharacterCount].materialReferenceIndex = m_CurrentMaterialIndex;
						textInfo.textElementInfo[m_TotalCharacterCount].textElement = m_CurrentSpriteAsset.spriteCharacterTable[m_SpriteIndex];
						textInfo.textElementInfo[m_TotalCharacterCount].elementType = m_TextElementType;
						textInfo.textElementInfo[m_TotalCharacterCount].index = stringIndex;
						textInfo.textElementInfo[m_TotalCharacterCount].stringLength = textProcessingArray[i].stringIndex - stringIndex + 1;
						m_TextElementType = TextElementType.Character;
						m_CurrentMaterialIndex = currentMaterialIndex;
						num++;
						m_TotalCharacterCount++;
					}
					continue;
				}
			}
			bool flag = false;
			FontAsset currentFontAsset = m_CurrentFontAsset;
			Material currentMaterial = m_CurrentMaterial;
			currentMaterialIndex = m_CurrentMaterialIndex;
			if (m_TextElementType == TextElementType.Character)
			{
				if ((m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
				{
					if (char.IsLower((char)num2))
					{
						num2 = char.ToUpper((char)num2);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
				{
					if (char.IsUpper((char)num2))
					{
						num2 = char.ToLower((char)num2);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num2))
				{
					num2 = char.ToUpper((char)num2);
				}
			}
			bool isUsingAlternativeTypeface;
			TextElement textElement = GetTextElement(generationSettings, num2, m_CurrentFontAsset, m_FontStyleInternal, m_FontWeightInternal, out isUsingAlternativeTypeface);
			if (textElement == null)
			{
				DoMissingGlyphCallback(num2, textProcessingArray[i].stringIndex, m_CurrentFontAsset, textInfo);
				uint num3 = num2;
				num2 = (textProcessingArray[i].unicode = ((textSettings.missingCharacterUnicode == 0) ? 9633u : ((uint)textSettings.missingCharacterUnicode)));
				textElement = FontAssetUtilities.GetCharacterFromFontAsset(num2, m_CurrentFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isUsingAlternativeTypeface);
				if (textElement == null && textSettings.fallbackFontAssets != null && textSettings.fallbackFontAssets.Count > 0)
				{
					textElement = FontAssetUtilities.GetCharacterFromFontAssets(num2, m_CurrentFontAsset, textSettings.fallbackFontAssets, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isUsingAlternativeTypeface);
				}
				if (textElement == null && textSettings.defaultFontAsset != null)
				{
					textElement = FontAssetUtilities.GetCharacterFromFontAsset(num2, textSettings.defaultFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isUsingAlternativeTypeface);
				}
				if (textElement == null)
				{
					num2 = (textProcessingArray[i].unicode = 32u);
					textElement = FontAssetUtilities.GetCharacterFromFontAsset(num2, m_CurrentFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isUsingAlternativeTypeface);
				}
				if (textElement == null)
				{
					num2 = (textProcessingArray[i].unicode = 3u);
					textElement = FontAssetUtilities.GetCharacterFromFontAsset(num2, m_CurrentFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isUsingAlternativeTypeface);
				}
				if (textSettings.displayWarnings)
				{
					string message = ((num3 > 65535) ? $"The character with Unicode value \\U{num3:X8} was not found in the [{generationSettings.fontAsset.name}] font asset or any potential fallbacks. It was replaced by Unicode character \\u{textElement.unicode:X4}." : $"The character with Unicode value \\u{num3:X4} was not found in the [{generationSettings.fontAsset.name}] font asset or any potential fallbacks. It was replaced by Unicode character \\u{textElement.unicode:X4}.");
					Debug.LogWarning(message);
				}
			}
			textInfo.textElementInfo[m_TotalCharacterCount].alternativeGlyph = null;
			if (textElement.elementType == TextElementType.Character)
			{
				if (textElement.textAsset.instanceID != m_CurrentFontAsset.instanceID)
				{
					flag = true;
					m_CurrentFontAsset = textElement.textAsset as FontAsset;
				}
				if (m_CurrentFontAsset.fontFeatureTable.m_LigatureSubstitutionRecordLookup.TryGetValue(textElement.glyphIndex, out var value))
				{
					if (value == null)
					{
						break;
					}
					for (int j = 0; j < value.Count; j++)
					{
						LigatureSubstitutionRecord ligatureSubstitutionRecord = value[j];
						int num4 = ligatureSubstitutionRecord.componentGlyphIDs.Length;
						uint num5 = ligatureSubstitutionRecord.ligatureGlyphID;
						for (int k = 1; k < num4; k++)
						{
							uint glyphIndex = m_CurrentFontAsset.GetGlyphIndex(textProcessingArray[i + k].unicode);
							if (glyphIndex != ligatureSubstitutionRecord.componentGlyphIDs[k])
							{
								num5 = 0u;
								break;
							}
						}
						if (num5 == 0 || !m_CurrentFontAsset.TryAddGlyphInternal(num5, out var glyph))
						{
							continue;
						}
						textInfo.textElementInfo[m_TotalCharacterCount].alternativeGlyph = glyph;
						for (int l = 0; l < num4; l++)
						{
							if (l == 0)
							{
								textProcessingArray[i + l].length = num4;
							}
							else
							{
								textProcessingArray[i + l].unicode = 26u;
							}
						}
						i += num4 - 1;
						break;
					}
				}
			}
			textInfo.textElementInfo[m_TotalCharacterCount].elementType = TextElementType.Character;
			textInfo.textElementInfo[m_TotalCharacterCount].textElement = textElement;
			textInfo.textElementInfo[m_TotalCharacterCount].isUsingAlternateTypeface = isUsingAlternativeTypeface;
			textInfo.textElementInfo[m_TotalCharacterCount].character = (char)num2;
			textInfo.textElementInfo[m_TotalCharacterCount].index = textProcessingArray[i].stringIndex;
			textInfo.textElementInfo[m_TotalCharacterCount].stringLength = textProcessingArray[i].length;
			textInfo.textElementInfo[m_TotalCharacterCount].fontAsset = m_CurrentFontAsset;
			if (textElement.elementType == TextElementType.Sprite)
			{
				SpriteAsset spriteAsset = textElement.textAsset as SpriteAsset;
				m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(spriteAsset.material, spriteAsset, ref m_MaterialReferences, m_MaterialReferenceIndexLookup);
				m_MaterialReferences[m_CurrentMaterialIndex].referenceCount++;
				textInfo.textElementInfo[m_TotalCharacterCount].elementType = TextElementType.Sprite;
				textInfo.textElementInfo[m_TotalCharacterCount].materialReferenceIndex = m_CurrentMaterialIndex;
				m_TextElementType = TextElementType.Character;
				m_CurrentMaterialIndex = currentMaterialIndex;
				num++;
				m_TotalCharacterCount++;
				continue;
			}
			if (flag && m_CurrentFontAsset.instanceID != generationSettings.fontAsset.instanceID)
			{
				if (textSettings.matchMaterialPreset)
				{
					m_CurrentMaterial = MaterialManager.GetFallbackMaterial(m_CurrentMaterial, m_CurrentFontAsset.material);
				}
				else
				{
					m_CurrentMaterial = m_CurrentFontAsset.material;
				}
				m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(m_CurrentMaterial, m_CurrentFontAsset, ref m_MaterialReferences, m_MaterialReferenceIndexLookup);
			}
			if (textElement != null && textElement.glyph.atlasIndex > 0)
			{
				m_CurrentMaterial = MaterialManager.GetFallbackMaterial(m_CurrentFontAsset, m_CurrentMaterial, textElement.glyph.atlasIndex);
				m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(m_CurrentMaterial, m_CurrentFontAsset, ref m_MaterialReferences, m_MaterialReferenceIndexLookup);
				flag = true;
			}
			if (!char.IsWhiteSpace((char)num2) && num2 != 8203)
			{
				if (m_MaterialReferences[m_CurrentMaterialIndex].referenceCount < 16383)
				{
					m_MaterialReferences[m_CurrentMaterialIndex].referenceCount++;
				}
				else
				{
					m_CurrentMaterialIndex = MaterialReference.AddMaterialReference(new Material(m_CurrentMaterial), m_CurrentFontAsset, ref m_MaterialReferences, m_MaterialReferenceIndexLookup);
					m_MaterialReferences[m_CurrentMaterialIndex].referenceCount++;
				}
			}
			textInfo.textElementInfo[m_TotalCharacterCount].material = m_CurrentMaterial;
			textInfo.textElementInfo[m_TotalCharacterCount].materialReferenceIndex = m_CurrentMaterialIndex;
			m_MaterialReferences[m_CurrentMaterialIndex].isFallbackMaterial = flag;
			if (flag)
			{
				m_MaterialReferences[m_CurrentMaterialIndex].fallbackMaterial = currentMaterial;
				m_CurrentFontAsset = currentFontAsset;
				m_CurrentMaterial = currentMaterial;
				m_CurrentMaterialIndex = currentMaterialIndex;
			}
			m_TotalCharacterCount++;
		}
		if (m_IsCalculatingPreferredValues)
		{
			m_IsCalculatingPreferredValues = false;
			return m_TotalCharacterCount;
		}
		textInfo.spriteCount = num;
		int num6 = (textInfo.materialCount = m_MaterialReferenceIndexLookup.Count);
		if (num6 > textInfo.meshInfo.Length)
		{
			TextInfo.Resize(ref textInfo.meshInfo, num6, isBlockAllocated: false);
		}
		if (m_VertexBufferAutoSizeReduction && textInfo.textElementInfo.Length - m_TotalCharacterCount > 256)
		{
			TextInfo.Resize(ref textInfo.textElementInfo, Mathf.Max(m_TotalCharacterCount + 1, 256), isBlockAllocated: true);
		}
		for (int m = 0; m < num6; m++)
		{
			int referenceCount = m_MaterialReferences[m].referenceCount;
			if (textInfo.meshInfo[m].vertices == null || textInfo.meshInfo[m].vertices.Length < referenceCount * 4)
			{
				if (textInfo.meshInfo[m].vertices == null)
				{
					textInfo.meshInfo[m] = new MeshInfo(referenceCount + 1);
				}
				else
				{
					textInfo.meshInfo[m].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount));
				}
			}
			else if (textInfo.meshInfo[m].vertices.Length - referenceCount * 4 > 1024)
			{
				textInfo.meshInfo[m].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.Max(Mathf.NextPowerOfTwo(referenceCount), 256));
			}
			textInfo.meshInfo[m].material = m_MaterialReferences[m].material;
			textInfo.meshInfo[m].glyphRenderMode = m_MaterialReferences[m].fontAsset.atlasRenderMode;
		}
		return m_TotalCharacterCount;
	}

	internal TextElement GetTextElement(TextGenerationSettings generationSettings, uint unicode, FontAsset fontAsset, FontStyles fontStyle, TextFontWeight fontWeight, out bool isUsingAlternativeTypeface)
	{
		TextSettings textSettings = generationSettings.textSettings;
		Character character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, fontAsset, includeFallbacks: false, fontStyle, fontWeight, out isUsingAlternativeTypeface);
		if (character != null)
		{
			return character;
		}
		if (fontAsset.m_FallbackFontAssetTable != null && fontAsset.m_FallbackFontAssetTable.Count > 0)
		{
			character = FontAssetUtilities.GetCharacterFromFontAssets(unicode, fontAsset, fontAsset.m_FallbackFontAssetTable, includeFallbacks: true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
		}
		if (character != null)
		{
			fontAsset.AddCharacterToLookupCache(unicode, character);
			return character;
		}
		if (fontAsset.instanceID != generationSettings.fontAsset.instanceID)
		{
			character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, generationSettings.fontAsset, includeFallbacks: false, fontStyle, fontWeight, out isUsingAlternativeTypeface);
			if (character != null)
			{
				m_CurrentMaterialIndex = 0;
				m_CurrentMaterial = m_MaterialReferences[0].material;
				fontAsset.AddCharacterToLookupCache(unicode, character);
				return character;
			}
			if (generationSettings.fontAsset.m_FallbackFontAssetTable != null && generationSettings.fontAsset.m_FallbackFontAssetTable.Count > 0)
			{
				character = FontAssetUtilities.GetCharacterFromFontAssets(unicode, fontAsset, generationSettings.fontAsset.m_FallbackFontAssetTable, includeFallbacks: true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
			}
			if (character != null)
			{
				fontAsset.AddCharacterToLookupCache(unicode, character);
				return character;
			}
		}
		if (generationSettings.spriteAsset != null)
		{
			SpriteCharacter spriteCharacterFromSpriteAsset = FontAssetUtilities.GetSpriteCharacterFromSpriteAsset(unicode, generationSettings.spriteAsset, includeFallbacks: true);
			if (spriteCharacterFromSpriteAsset != null)
			{
				return spriteCharacterFromSpriteAsset;
			}
		}
		if (textSettings.fallbackFontAssets != null && textSettings.fallbackFontAssets.Count > 0)
		{
			character = FontAssetUtilities.GetCharacterFromFontAssets(unicode, fontAsset, textSettings.fallbackFontAssets, includeFallbacks: true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
		}
		if (character != null)
		{
			fontAsset.AddCharacterToLookupCache(unicode, character);
			return character;
		}
		if (textSettings.defaultFontAsset != null)
		{
			character = FontAssetUtilities.GetCharacterFromFontAsset(unicode, textSettings.defaultFontAsset, includeFallbacks: true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
		}
		if (character != null)
		{
			fontAsset.AddCharacterToLookupCache(unicode, character);
			return character;
		}
		if (textSettings.defaultSpriteAsset != null)
		{
			SpriteCharacter spriteCharacterFromSpriteAsset2 = FontAssetUtilities.GetSpriteCharacterFromSpriteAsset(unicode, textSettings.defaultSpriteAsset, includeFallbacks: true);
			if (spriteCharacterFromSpriteAsset2 != null)
			{
				return spriteCharacterFromSpriteAsset2;
			}
		}
		return null;
	}

	private void ComputeMarginSize(Rect rect, Vector4 margins)
	{
		m_MarginWidth = rect.width - margins.x - margins.z;
		m_MarginHeight = rect.height - margins.y - margins.w;
		m_RectTransformCorners[0].x = 0f;
		m_RectTransformCorners[0].y = 0f;
		m_RectTransformCorners[1].x = 0f;
		m_RectTransformCorners[1].y = rect.height;
		m_RectTransformCorners[2].x = rect.width;
		m_RectTransformCorners[2].y = rect.height;
		m_RectTransformCorners[3].x = rect.width;
		m_RectTransformCorners[3].y = 0f;
	}

	protected void GetSpecialCharacters(TextGenerationSettings generationSettings)
	{
		GetEllipsisSpecialCharacter(generationSettings);
		GetUnderlineSpecialCharacter(generationSettings);
	}

	protected void GetEllipsisSpecialCharacter(TextGenerationSettings generationSettings)
	{
		FontAsset fontAsset = m_CurrentFontAsset ?? generationSettings.fontAsset;
		TextSettings textSettings = generationSettings.textSettings;
		bool isAlternativeTypeface;
		Character character = FontAssetUtilities.GetCharacterFromFontAsset(8230u, fontAsset, includeFallbacks: false, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface);
		if (character == null && fontAsset.m_FallbackFontAssetTable != null && fontAsset.m_FallbackFontAssetTable.Count > 0)
		{
			character = FontAssetUtilities.GetCharacterFromFontAssets(8230u, fontAsset, fontAsset.m_FallbackFontAssetTable, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface);
		}
		if (character == null && textSettings.fallbackFontAssets != null && textSettings.fallbackFontAssets.Count > 0)
		{
			character = FontAssetUtilities.GetCharacterFromFontAssets(8230u, fontAsset, textSettings.fallbackFontAssets, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface);
		}
		if (character == null && textSettings.defaultFontAsset != null)
		{
			character = FontAssetUtilities.GetCharacterFromFontAsset(8230u, textSettings.defaultFontAsset, includeFallbacks: true, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface);
		}
		if (character != null)
		{
			m_Ellipsis = new SpecialCharacter(character, 0);
		}
	}

	protected void GetUnderlineSpecialCharacter(TextGenerationSettings generationSettings)
	{
		FontAsset sourceFontAsset = m_CurrentFontAsset ?? generationSettings.fontAsset;
		TextSettings textSettings = generationSettings.textSettings;
		bool isAlternativeTypeface;
		Character characterFromFontAsset = FontAssetUtilities.GetCharacterFromFontAsset(95u, sourceFontAsset, includeFallbacks: false, m_FontStyleInternal, m_FontWeightInternal, out isAlternativeTypeface);
		if (characterFromFontAsset != null)
		{
			m_Underline = new SpecialCharacter(characterFromFontAsset, m_CurrentMaterialIndex);
		}
	}

	private float GetPreferredWidthInternal(TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		if (generationSettings.textSettings == null)
		{
			return 0f;
		}
		float fontSize = (generationSettings.autoSize ? generationSettings.fontSizeMax : m_FontSize);
		m_MinFontSize = generationSettings.fontSizeMin;
		m_MaxFontSize = generationSettings.fontSizeMax;
		m_CharWidthAdjDelta = 0f;
		Vector2 largePositiveVector = TextGeneratorUtilities.largePositiveVector2;
		TextWrappingMode textWrapMode = ((!generationSettings.wordWrap) ? TextWrappingMode.PreserveWhitespaceNoWrap : TextWrappingMode.NoWrap);
		m_AutoSizeIterationCount = 0;
		return CalculatePreferredValues(ref fontSize, largePositiveVector, isTextAutoSizingEnabled: true, textWrapMode, generationSettings, textInfo).x;
	}

	private float GetPreferredHeightInternal(TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		if (generationSettings.textSettings == null)
		{
			return 0f;
		}
		float fontSize = (generationSettings.autoSize ? generationSettings.fontSizeMax : m_FontSize);
		m_MinFontSize = generationSettings.fontSizeMin;
		m_MaxFontSize = generationSettings.fontSizeMax;
		m_CharWidthAdjDelta = 0f;
		Vector2 marginSize = new Vector2((m_MarginWidth != 0f) ? m_MarginWidth : 32767f, 32767f);
		m_IsAutoSizePointSizeSet = false;
		m_AutoSizeIterationCount = 0;
		float result = 0f;
		TextWrappingMode textWrapMode = (generationSettings.wordWrap ? TextWrappingMode.Normal : TextWrappingMode.NoWrap);
		while (!m_IsAutoSizePointSizeSet)
		{
			result = CalculatePreferredValues(ref fontSize, marginSize, generationSettings.autoSize, textWrapMode, generationSettings, textInfo).y;
			m_AutoSizeIterationCount++;
		}
		return result;
	}

	private Vector2 GetPreferredValuesInternal(TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		if (generationSettings.textSettings == null)
		{
			return Vector2.zero;
		}
		float fontSize = (generationSettings.autoSize ? generationSettings.fontSizeMax : m_FontSize);
		m_MinFontSize = generationSettings.fontSizeMin;
		m_MaxFontSize = generationSettings.fontSizeMax;
		m_CharWidthAdjDelta = 0f;
		Vector2 marginSize = new Vector2((m_MarginWidth != 0f) ? m_MarginWidth : 32767f, (m_MarginHeight != 0f) ? m_MarginHeight : 32767f);
		TextWrappingMode textWrapMode = (generationSettings.wordWrap ? TextWrappingMode.Normal : TextWrappingMode.NoWrap);
		m_AutoSizeIterationCount = 0;
		return CalculatePreferredValues(ref fontSize, marginSize, generationSettings.autoSize, textWrapMode, generationSettings, textInfo);
	}

	protected virtual Vector2 CalculatePreferredValues(ref float fontSize, Vector2 marginSize, bool isTextAutoSizingEnabled, TextWrappingMode textWrapMode, TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		if (generationSettings.fontAsset == null || generationSettings.fontAsset.characterLookupTable == null)
		{
			Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned.");
			m_IsAutoSizePointSizeSet = true;
			return Vector2.zero;
		}
		if (m_TextProcessingArray == null || m_TextProcessingArray.Length == 0 || m_TextProcessingArray[0].unicode == 0)
		{
			m_IsAutoSizePointSizeSet = true;
			return Vector2.zero;
		}
		m_CurrentFontAsset = generationSettings.fontAsset;
		m_CurrentMaterial = generationSettings.material;
		m_CurrentMaterialIndex = 0;
		m_MaterialReferenceStack.SetDefault(new MaterialReference(0, m_CurrentFontAsset, null, m_CurrentMaterial, m_Padding));
		int totalCharacterCount = m_TotalCharacterCount;
		if (m_InternalTextElementInfo == null || totalCharacterCount > m_InternalTextElementInfo.Length)
		{
			m_InternalTextElementInfo = new TextElementInfo[(totalCharacterCount > 1024) ? (totalCharacterCount + 256) : Mathf.NextPowerOfTwo(totalCharacterCount)];
		}
		float num = fontSize / (float)generationSettings.fontAsset.faceInfo.pointSize * generationSettings.fontAsset.faceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
		float num2 = num;
		float num3 = fontSize * 0.01f * (generationSettings.isOrthographic ? 1f : 0.1f);
		m_FontScaleMultiplier = 1f;
		m_CurrentFontSize = fontSize;
		m_SizeStack.SetDefault(m_CurrentFontSize);
		float num4 = 0f;
		m_FontStyleInternal = generationSettings.fontStyle;
		m_LineJustification = generationSettings.textAlignment;
		m_LineJustificationStack.SetDefault(m_LineJustification);
		m_BaselineOffset = 0f;
		m_BaselineOffsetStack.Clear();
		m_FXScale = Vector3.one;
		m_LineOffset = 0f;
		m_LineHeight = -32767f;
		float num5 = m_CurrentFontAsset.faceInfo.lineHeight - (m_CurrentFontAsset.faceInfo.ascentLine - m_CurrentFontAsset.faceInfo.descentLine);
		m_CSpacing = 0f;
		m_MonoSpacing = 0f;
		m_XAdvance = 0f;
		m_TagLineIndent = 0f;
		m_TagIndent = 0f;
		m_IndentStack.SetDefault(0f);
		m_TagNoParsing = false;
		m_CharacterCount = 0;
		m_FirstCharacterOfLine = 0;
		m_MaxLineAscender = -32767f;
		m_MaxLineDescender = 32767f;
		m_LineNumber = 0;
		m_StartOfLineAscender = 0f;
		m_IsDrivenLineSpacing = false;
		m_LastBaseGlyphIndex = int.MinValue;
		TextSettings textSettings = generationSettings.textSettings;
		float x = marginSize.x;
		float y = marginSize.y;
		m_MarginLeft = 0f;
		m_MarginRight = 0f;
		m_Width = -1f;
		float num6 = x + 0.0001f - m_MarginLeft - m_MarginRight;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		m_IsCalculatingPreferredValues = true;
		m_MaxCapHeight = 0f;
		m_MaxAscender = 0f;
		m_MaxDescender = 0f;
		float num10 = 0f;
		bool flag = false;
		bool flag2 = true;
		m_IsNonBreakingSpace = false;
		bool flag3 = false;
		CharacterSubstitution characterSubstitution = new CharacterSubstitution(-1, 0u);
		bool flag4 = false;
		WordWrapState state = default(WordWrapState);
		WordWrapState state2 = default(WordWrapState);
		WordWrapState state3 = default(WordWrapState);
		m_IsTextTruncated = false;
		m_AutoSizeIterationCount++;
		for (int i = 0; i < m_TextProcessingArray.Length && m_TextProcessingArray[i].unicode != 0; i++)
		{
			uint num11 = m_TextProcessingArray[i].unicode;
			if (num11 == 26)
			{
				continue;
			}
			if (generationSettings.richText && num11 == 60)
			{
				m_isTextLayoutPhase = true;
				m_TextElementType = TextElementType.Character;
				if (ValidateHtmlTag(m_TextProcessingArray, i + 1, out var endIndex, generationSettings, textInfo))
				{
					i = endIndex;
					if (m_TextElementType == TextElementType.Character)
					{
						continue;
					}
				}
			}
			else
			{
				m_TextElementType = textInfo.textElementInfo[m_CharacterCount].elementType;
				m_CurrentMaterialIndex = textInfo.textElementInfo[m_CharacterCount].materialReferenceIndex;
				m_CurrentFontAsset = textInfo.textElementInfo[m_CharacterCount].fontAsset;
			}
			int currentMaterialIndex = m_CurrentMaterialIndex;
			bool isUsingAlternateTypeface = textInfo.textElementInfo[m_CharacterCount].isUsingAlternateTypeface;
			m_isTextLayoutPhase = false;
			bool flag5 = false;
			if (characterSubstitution.index == m_CharacterCount)
			{
				num11 = characterSubstitution.unicode;
				m_TextElementType = TextElementType.Character;
				flag5 = true;
				switch (num11)
				{
				case 3u:
					m_InternalTextElementInfo[m_CharacterCount].textElement = m_CurrentFontAsset.characterLookupTable[3u];
					m_IsTextTruncated = true;
					break;
				case 8230u:
					m_InternalTextElementInfo[m_CharacterCount].textElement = m_Ellipsis.character;
					m_InternalTextElementInfo[m_CharacterCount].elementType = TextElementType.Character;
					m_InternalTextElementInfo[m_CharacterCount].fontAsset = m_Ellipsis.fontAsset;
					m_InternalTextElementInfo[m_CharacterCount].material = m_Ellipsis.material;
					m_InternalTextElementInfo[m_CharacterCount].materialReferenceIndex = m_Ellipsis.materialIndex;
					m_IsTextTruncated = true;
					characterSubstitution.index = m_CharacterCount + 1;
					characterSubstitution.unicode = 3u;
					break;
				}
			}
			if (m_CharacterCount < generationSettings.firstVisibleCharacter && num11 != 3)
			{
				m_InternalTextElementInfo[m_CharacterCount].isVisible = false;
				m_InternalTextElementInfo[m_CharacterCount].character = '\u200b';
				m_InternalTextElementInfo[m_CharacterCount].lineNumber = 0;
				m_CharacterCount++;
				continue;
			}
			float num12 = 1f;
			if (m_TextElementType == TextElementType.Character)
			{
				if ((m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
				{
					if (char.IsLower((char)num11))
					{
						num11 = char.ToUpper((char)num11);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
				{
					if (char.IsUpper((char)num11))
					{
						num11 = char.ToLower((char)num11);
					}
				}
				else if ((m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)num11))
				{
					num12 = 0.8f;
					num11 = char.ToUpper((char)num11);
				}
			}
			float num13 = 0f;
			float num14 = 0f;
			float num15 = 0f;
			if (m_TextElementType == TextElementType.Sprite)
			{
				SpriteCharacter spriteCharacter = (SpriteCharacter)textInfo.textElementInfo[m_CharacterCount].textElement;
				m_CurrentSpriteAsset = spriteCharacter.textAsset as SpriteAsset;
				m_SpriteIndex = (int)spriteCharacter.glyphIndex;
				if (spriteCharacter == null)
				{
					continue;
				}
				if (num11 == 60)
				{
					num11 = (uint)(57344 + m_SpriteIndex);
				}
				if (m_CurrentSpriteAsset.faceInfo.pointSize > 0)
				{
					float num16 = m_CurrentFontSize / (float)m_CurrentSpriteAsset.faceInfo.pointSize * m_CurrentSpriteAsset.faceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
					num2 = spriteCharacter.scale * spriteCharacter.glyph.scale * num16;
					num14 = m_CurrentSpriteAsset.faceInfo.ascentLine;
					num15 = m_CurrentSpriteAsset.faceInfo.descentLine;
				}
				else
				{
					float num17 = m_CurrentFontSize / (float)m_CurrentFontAsset.faceInfo.pointSize * m_CurrentFontAsset.faceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f);
					num2 = m_CurrentFontAsset.faceInfo.ascentLine / spriteCharacter.glyph.metrics.height * spriteCharacter.scale * spriteCharacter.glyph.scale * num17;
					float num18 = num17 / num2;
					num14 = m_CurrentFontAsset.faceInfo.ascentLine * num18;
					num15 = m_CurrentFontAsset.faceInfo.descentLine * num18;
				}
				m_CachedTextElement = spriteCharacter;
				m_InternalTextElementInfo[m_CharacterCount].elementType = TextElementType.Sprite;
				m_InternalTextElementInfo[m_CharacterCount].scale = num2;
				m_CurrentMaterialIndex = currentMaterialIndex;
			}
			else if (m_TextElementType == TextElementType.Character)
			{
				m_CachedTextElement = textInfo.textElementInfo[m_CharacterCount].textElement;
				if (m_CachedTextElement == null)
				{
					continue;
				}
				m_CurrentFontAsset = textInfo.textElementInfo[m_CharacterCount].fontAsset;
				m_CurrentMaterial = textInfo.textElementInfo[m_CharacterCount].material;
				m_CurrentMaterialIndex = textInfo.textElementInfo[m_CharacterCount].materialReferenceIndex;
				float num19 = ((!flag5 || m_TextProcessingArray[i].unicode != 10 || m_CharacterCount == m_FirstCharacterOfLine) ? (m_CurrentFontSize * num12 / (float)m_CurrentFontAsset.m_FaceInfo.pointSize * m_CurrentFontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f)) : (textInfo.textElementInfo[m_CharacterCount - 1].pointSize * num12 / (float)m_CurrentFontAsset.m_FaceInfo.pointSize * m_CurrentFontAsset.m_FaceInfo.scale * (generationSettings.isOrthographic ? 1f : 0.1f)));
				if (flag5 && num11 == 8230)
				{
					num14 = 0f;
					num15 = 0f;
				}
				else
				{
					num14 = m_CurrentFontAsset.m_FaceInfo.ascentLine;
					num15 = m_CurrentFontAsset.m_FaceInfo.descentLine;
				}
				num2 = num19 * m_FontScaleMultiplier * m_CachedTextElement.scale;
				m_InternalTextElementInfo[m_CharacterCount].elementType = TextElementType.Character;
			}
			float num20 = num2;
			if (num11 == 173 || num11 == 3)
			{
				num2 = 0f;
			}
			m_InternalTextElementInfo[m_CharacterCount].character = (char)num11;
			GlyphMetrics glyphMetrics = textInfo.textElementInfo[m_CharacterCount].alternativeGlyph?.metrics ?? m_CachedTextElement.m_Glyph.metrics;
			bool flag6 = num11 <= 65535 && char.IsWhiteSpace((char)num11);
			GlyphValueRecord glyphValueRecord = default(GlyphValueRecord);
			float num21 = generationSettings.characterSpacing;
			if (generationSettings.enableKerning)
			{
				uint glyphIndex = m_CachedTextElement.m_GlyphIndex;
				GlyphPairAdjustmentRecord value;
				if (m_CharacterCount < totalCharacterCount - 1)
				{
					uint glyphIndex2 = textInfo.textElementInfo[m_CharacterCount + 1].textElement.m_GlyphIndex;
					uint key = (glyphIndex2 << 16) | glyphIndex;
					if (m_CurrentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key, out value))
					{
						glyphValueRecord = value.firstAdjustmentRecord.glyphValueRecord;
						num21 = (((value.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num21);
					}
				}
				if (m_CharacterCount >= 1)
				{
					uint glyphIndex3 = textInfo.textElementInfo[m_CharacterCount - 1].textElement.m_GlyphIndex;
					uint key2 = (glyphIndex << 16) | glyphIndex3;
					if (m_CurrentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key2, out value))
					{
						glyphValueRecord += value.secondAdjustmentRecord.glyphValueRecord;
						num21 = (((value.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : num21);
					}
				}
				m_InternalTextElementInfo[m_CharacterCount].adjustedHorizontalAdvance = glyphValueRecord.xAdvance;
			}
			bool flag7 = TextGeneratorUtilities.IsBaseGlyph(num11);
			if (flag7)
			{
				m_LastBaseGlyphIndex = m_CharacterCount;
			}
			if (m_CharacterCount > 0 && !flag7)
			{
				if (m_LastBaseGlyphIndex != int.MinValue && m_LastBaseGlyphIndex == m_CharacterCount - 1)
				{
					Glyph glyph = textInfo.textElementInfo[m_LastBaseGlyphIndex].textElement.glyph;
					uint index = glyph.index;
					uint glyphIndex4 = m_CachedTextElement.glyphIndex;
					uint key3 = (glyphIndex4 << 16) | index;
					if (m_CurrentFontAsset.fontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryGetValue(key3, out var value2))
					{
						float num22 = (m_InternalTextElementInfo[m_LastBaseGlyphIndex].origin - m_XAdvance) / num2;
						glyphValueRecord.xPlacement = num22 + value2.baseGlyphAnchorPoint.xCoordinate - value2.markPositionAdjustment.xPositionAdjustment;
						glyphValueRecord.yPlacement = value2.baseGlyphAnchorPoint.yCoordinate - value2.markPositionAdjustment.yPositionAdjustment;
						num21 = 0f;
					}
				}
				else
				{
					bool flag8 = false;
					int num23 = m_CharacterCount - 1;
					while (num23 >= 0 && num23 != m_LastBaseGlyphIndex)
					{
						Glyph glyph2 = textInfo.textElementInfo[num23].textElement.glyph;
						uint index2 = glyph2.index;
						uint glyphIndex5 = m_CachedTextElement.glyphIndex;
						uint key4 = (glyphIndex5 << 16) | index2;
						if (m_CurrentFontAsset.fontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.TryGetValue(key4, out var value3))
						{
							float num24 = (textInfo.textElementInfo[num23].origin - m_XAdvance) / num2;
							float num25 = num13 - m_LineOffset + m_BaselineOffset;
							float num26 = (m_InternalTextElementInfo[num23].baseLine - num25) / num2;
							glyphValueRecord.xPlacement = num24 + value3.baseMarkGlyphAnchorPoint.xCoordinate - value3.combiningMarkPositionAdjustment.xPositionAdjustment;
							glyphValueRecord.yPlacement = num26 + value3.baseMarkGlyphAnchorPoint.yCoordinate - value3.combiningMarkPositionAdjustment.yPositionAdjustment;
							num21 = 0f;
							flag8 = true;
							break;
						}
						num23--;
					}
					if (m_LastBaseGlyphIndex != int.MinValue && !flag8)
					{
						Glyph glyph3 = textInfo.textElementInfo[m_LastBaseGlyphIndex].textElement.glyph;
						uint index3 = glyph3.index;
						uint glyphIndex6 = m_CachedTextElement.glyphIndex;
						uint key5 = (glyphIndex6 << 16) | index3;
						if (m_CurrentFontAsset.fontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryGetValue(key5, out var value4))
						{
							float num27 = (m_InternalTextElementInfo[m_LastBaseGlyphIndex].origin - m_XAdvance) / num2;
							glyphValueRecord.xPlacement = num27 + value4.baseGlyphAnchorPoint.xCoordinate - value4.markPositionAdjustment.xPositionAdjustment;
							glyphValueRecord.yPlacement = value4.baseGlyphAnchorPoint.yCoordinate - value4.markPositionAdjustment.yPositionAdjustment;
							num21 = 0f;
						}
					}
				}
			}
			num14 += glyphValueRecord.yPlacement;
			num15 += glyphValueRecord.yPlacement;
			float num28 = 0f;
			if (m_MonoSpacing != 0f)
			{
				num28 = (m_MonoSpacing / 2f - (m_CachedTextElement.glyph.metrics.width / 2f + m_CachedTextElement.glyph.metrics.horizontalBearingX) * num2) * (1f - m_CharWidthAdjDelta);
				m_XAdvance += num28;
			}
			float num29 = 0f;
			if (m_TextElementType == TextElementType.Character && !isUsingAlternateTypeface && (m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
			{
				num29 = m_CurrentFontAsset.boldStyleSpacing;
			}
			m_InternalTextElementInfo[m_CharacterCount].origin = m_XAdvance + glyphValueRecord.xPlacement * num2;
			m_InternalTextElementInfo[m_CharacterCount].baseLine = num13 - m_LineOffset + m_BaselineOffset + glyphValueRecord.yPlacement * num2;
			float num30 = ((m_TextElementType == TextElementType.Character) ? (num14 * num2 / num12 + m_BaselineOffset) : (num14 * num2 + m_BaselineOffset));
			float num31 = ((m_TextElementType == TextElementType.Character) ? (num15 * num2 / num12 + m_BaselineOffset) : (num15 * num2 + m_BaselineOffset));
			float num32 = num30;
			float num33 = num31;
			bool flag9 = m_CharacterCount == m_FirstCharacterOfLine;
			if (flag9 || !flag6)
			{
				if (m_BaselineOffset != 0f)
				{
					num32 = Mathf.Max((num30 - m_BaselineOffset) / m_FontScaleMultiplier, num32);
					num33 = Mathf.Min((num31 - m_BaselineOffset) / m_FontScaleMultiplier, num33);
				}
				m_MaxLineAscender = Mathf.Max(num32, m_MaxLineAscender);
				m_MaxLineDescender = Mathf.Min(num33, m_MaxLineDescender);
			}
			if (flag9 || !flag6)
			{
				m_InternalTextElementInfo[m_CharacterCount].adjustedAscender = num32;
				m_InternalTextElementInfo[m_CharacterCount].adjustedDescender = num33;
				m_InternalTextElementInfo[m_CharacterCount].ascender = num30 - m_LineOffset;
				m_MaxDescender = (m_InternalTextElementInfo[m_CharacterCount].descender = num31 - m_LineOffset);
			}
			else
			{
				m_InternalTextElementInfo[m_CharacterCount].adjustedAscender = m_MaxLineAscender;
				m_InternalTextElementInfo[m_CharacterCount].adjustedDescender = m_MaxLineDescender;
				m_InternalTextElementInfo[m_CharacterCount].ascender = m_MaxLineAscender - m_LineOffset;
				m_MaxDescender = (m_InternalTextElementInfo[m_CharacterCount].descender = m_MaxLineDescender - m_LineOffset);
			}
			if ((m_LineNumber == 0 || m_IsNewPage) && (flag9 || !flag6))
			{
				m_MaxAscender = m_MaxLineAscender;
				m_MaxCapHeight = Mathf.Max(m_MaxCapHeight, m_CurrentFontAsset.m_FaceInfo.capLine * num2 / num12);
			}
			if (m_LineOffset == 0f && (!flag6 || m_CharacterCount == m_FirstCharacterOfLine))
			{
				m_PageAscender = ((m_PageAscender > num30) ? m_PageAscender : num30);
			}
			bool flag10 = (m_LineJustification & (TextAlignment)16) == (TextAlignment)16 || (m_LineJustification & (TextAlignment)8) == (TextAlignment)8;
			if (num11 == 9 || num11 == 8203 || ((textWrapMode == TextWrappingMode.PreserveWhitespace || textWrapMode == TextWrappingMode.PreserveWhitespaceNoWrap) && (flag6 || num11 == 8203)) || (!flag6 && num11 != 8203 && num11 != 173 && num11 != 3) || (num11 == 173 && !flag4) || m_TextElementType == TextElementType.Sprite)
			{
				num6 = ((m_Width != -1f) ? Mathf.Min(x + 0.0001f - m_MarginLeft - m_MarginRight, m_Width) : (x + 0.0001f - m_MarginLeft - m_MarginRight));
				num9 = Mathf.Abs(m_XAdvance) + glyphMetrics.horizontalAdvance * (1f - m_CharWidthAdjDelta) * ((num11 == 173) ? num20 : num2);
				int characterCount = m_CharacterCount;
				if (flag7 && num9 > num6 * (flag10 ? 1.05f : 1f) && textWrapMode != TextWrappingMode.NoWrap && textWrapMode != TextWrappingMode.PreserveWhitespaceNoWrap && m_CharacterCount != m_FirstCharacterOfLine)
				{
					i = RestoreWordWrappingState(ref state, textInfo);
					if (m_InternalTextElementInfo[m_CharacterCount - 1].character == '\u00ad' && !flag4 && generationSettings.overflowMode == TextOverflowMode.Overflow)
					{
						characterSubstitution.index = m_CharacterCount - 1;
						characterSubstitution.unicode = 45u;
						i--;
						m_CharacterCount--;
						continue;
					}
					flag4 = false;
					if (m_InternalTextElementInfo[m_CharacterCount].character == '\u00ad')
					{
						flag4 = true;
						continue;
					}
					if (isTextAutoSizingEnabled && flag2)
					{
						if (m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f && m_AutoSizeIterationCount < m_AutoSizeMaxIterationCount)
						{
							float num34 = num9;
							if (m_CharWidthAdjDelta > 0f)
							{
								num34 /= 1f - m_CharWidthAdjDelta;
							}
							float num35 = num9 - (num6 - 0.0001f) * (flag10 ? 1.05f : 1f);
							m_CharWidthAdjDelta += num35 / num34;
							m_CharWidthAdjDelta = Mathf.Min(m_CharWidthAdjDelta, generationSettings.charWidthMaxAdj / 100f);
							return Vector2.zero;
						}
						if (fontSize > generationSettings.fontSizeMin && m_AutoSizeIterationCount < m_AutoSizeMaxIterationCount)
						{
							m_MaxFontSize = fontSize;
							float num36 = Mathf.Max((fontSize - m_MinFontSize) / 2f, 0.05f);
							fontSize -= num36;
							fontSize = Mathf.Max((float)(int)(fontSize * 20f + 0.5f) / 20f, generationSettings.fontSizeMin);
						}
					}
					float num37 = m_MaxLineAscender - m_StartOfLineAscender;
					if (m_LineOffset > 0f && Math.Abs(num37) > 0.01f && !m_IsDrivenLineSpacing && !m_IsNewPage)
					{
						m_MaxDescender -= num37;
						m_LineOffset += num37;
					}
					float num38 = m_MaxLineAscender - m_LineOffset;
					float num39 = m_MaxLineDescender - m_LineOffset;
					m_MaxDescender = ((m_MaxDescender < num39) ? m_MaxDescender : num39);
					if (!flag)
					{
						num10 = m_MaxDescender;
					}
					if (generationSettings.useMaxVisibleDescender && (m_CharacterCount >= generationSettings.maxVisibleCharacters || m_LineNumber >= generationSettings.maxVisibleLines))
					{
						flag = true;
					}
					m_FirstCharacterOfLine = m_CharacterCount;
					m_LineVisibleCharacterCount = 0;
					SaveWordWrappingState(ref state2, i, m_CharacterCount - 1, textInfo);
					m_LineNumber++;
					float adjustedAscender = m_InternalTextElementInfo[m_CharacterCount].adjustedAscender;
					if (m_LineHeight == -32767f)
					{
						m_LineOffset += 0f - m_MaxLineDescender + adjustedAscender + (num5 + m_LineSpacingDelta) * num + generationSettings.lineSpacing * num3;
						m_IsDrivenLineSpacing = false;
					}
					else
					{
						m_LineOffset += m_LineHeight + generationSettings.lineSpacing * num3;
						m_IsDrivenLineSpacing = true;
					}
					m_MaxLineAscender = -32767f;
					m_MaxLineDescender = 32767f;
					m_StartOfLineAscender = adjustedAscender;
					m_XAdvance = 0f + m_TagIndent;
					flag2 = true;
					continue;
				}
				num7 = Mathf.Max(num7, num9 + m_MarginLeft + m_MarginRight);
				num8 = Mathf.Max(num8, m_MaxAscender - m_MaxDescender);
			}
			if (m_LineOffset > 0f && !TextGeneratorUtilities.Approximately(m_MaxLineAscender, m_StartOfLineAscender) && !m_IsDrivenLineSpacing && !m_IsNewPage)
			{
				float num40 = m_MaxLineAscender - m_StartOfLineAscender;
				m_MaxDescender -= num40;
				m_LineOffset += num40;
				m_StartOfLineAscender += num40;
				state.lineOffset = m_LineOffset;
				state.startOfLineAscender = m_StartOfLineAscender;
			}
			switch (num11)
			{
			case 9u:
			{
				float num41 = m_CurrentFontAsset.faceInfo.tabWidth * (float)(int)m_CurrentFontAsset.tabMultiple * num2;
				float num42 = Mathf.Ceil(m_XAdvance / num41) * num41;
				m_XAdvance = ((num42 > m_XAdvance) ? num42 : (m_XAdvance + num41));
				break;
			}
			default:
				if (m_MonoSpacing != 0f)
				{
					m_XAdvance += (m_MonoSpacing - num28 + (m_CurrentFontAsset.regularStyleSpacing + num21) * num3 + m_CSpacing) * (1f - m_CharWidthAdjDelta);
					if (flag6 || num11 == 8203)
					{
						m_XAdvance += generationSettings.wordSpacing * num3;
					}
				}
				else
				{
					m_XAdvance += ((glyphMetrics.horizontalAdvance * m_FXScale.x + glyphValueRecord.xAdvance) * num2 + (m_CurrentFontAsset.regularStyleSpacing + num21 + num29) * num3 + m_CSpacing) * (1f - m_CharWidthAdjDelta);
					if (flag6 || num11 == 8203)
					{
						m_XAdvance += generationSettings.wordSpacing * num3;
					}
				}
				break;
			case 8203u:
				break;
			}
			if (num11 == 13)
			{
				m_XAdvance = 0f + m_TagIndent;
			}
			if (num11 == 10 || num11 == 11 || num11 == 3 || num11 == 8232 || num11 == 8233 || m_CharacterCount == totalCharacterCount - 1)
			{
				float num43 = m_MaxLineAscender - m_StartOfLineAscender;
				if (m_LineOffset > 0f && Math.Abs(num43) > 0.01f && !m_IsDrivenLineSpacing && !m_IsNewPage)
				{
					m_MaxDescender -= num43;
					m_LineOffset += num43;
				}
				m_IsNewPage = false;
				float num44 = m_MaxLineDescender - m_LineOffset;
				m_MaxDescender = ((m_MaxDescender < num44) ? m_MaxDescender : num44);
				if (num11 == 10 || num11 == 11 || num11 == 45 || num11 == 8232 || num11 == 8233)
				{
					SaveWordWrappingState(ref state2, i, m_CharacterCount, textInfo);
					SaveWordWrappingState(ref state, i, m_CharacterCount, textInfo);
					m_LineNumber++;
					m_FirstCharacterOfLine = m_CharacterCount + 1;
					float adjustedAscender2 = m_InternalTextElementInfo[m_CharacterCount].adjustedAscender;
					if (m_LineHeight == -32767f)
					{
						float num45 = 0f - m_MaxLineDescender + adjustedAscender2 + (num5 + m_LineSpacingDelta) * num + (generationSettings.lineSpacing + ((num11 == 10 || num11 == 8233) ? generationSettings.paragraphSpacing : 0f)) * num3;
						m_LineOffset += num45;
						m_IsDrivenLineSpacing = false;
					}
					else
					{
						m_LineOffset += m_LineHeight + (generationSettings.lineSpacing + ((num11 == 10 || num11 == 8233) ? generationSettings.paragraphSpacing : 0f)) * num3;
						m_IsDrivenLineSpacing = true;
					}
					m_MaxLineAscender = -32767f;
					m_MaxLineDescender = 32767f;
					m_StartOfLineAscender = adjustedAscender2;
					m_XAdvance = 0f + m_TagLineIndent + m_TagIndent;
					m_CharacterCount++;
					continue;
				}
				if (num11 == 3)
				{
					i = m_TextProcessingArray.Length;
				}
			}
			if ((textWrapMode != TextWrappingMode.NoWrap && textWrapMode != TextWrappingMode.PreserveWhitespaceNoWrap) || generationSettings.overflowMode == TextOverflowMode.Truncate || generationSettings.overflowMode == TextOverflowMode.Ellipsis)
			{
				bool flag11 = false;
				bool flag12 = false;
				if ((flag6 || num11 == 8203 || num11 == 45 || num11 == 173) && (!m_IsNonBreakingSpace || flag3) && num11 != 160 && num11 != 8199 && num11 != 8209 && num11 != 8239 && num11 != 8288)
				{
					if (num11 != 45 || m_CharacterCount <= 0 || !char.IsWhiteSpace(textInfo.textElementInfo[m_CharacterCount - 1].character))
					{
						flag2 = false;
						flag11 = true;
						state3.previousWordBreak = -1;
					}
				}
				else if (!m_IsNonBreakingSpace && ((TextGeneratorUtilities.IsHangul(num11) && !textSettings.useModernHangulLineBreakingRules) || TextGeneratorUtilities.IsCJK(num11)))
				{
					bool flag13 = textSettings.lineBreakingRules.leadingCharactersLookup.Contains(num11);
					bool flag14 = m_CharacterCount < totalCharacterCount - 1 && textSettings.lineBreakingRules.leadingCharactersLookup.Contains(m_InternalTextElementInfo[m_CharacterCount + 1].character);
					if (!flag13)
					{
						if (!flag14)
						{
							flag2 = false;
							flag11 = true;
						}
						if (flag2)
						{
							if (flag6)
							{
								flag12 = true;
							}
							flag11 = true;
						}
					}
					else if (flag2 && flag9)
					{
						if (flag6)
						{
							flag12 = true;
						}
						flag11 = true;
					}
				}
				else if (flag2)
				{
					if ((flag6 && num11 != 160) || (num11 == 173 && !flag4))
					{
						flag12 = true;
					}
					flag11 = true;
				}
				if (flag11)
				{
					SaveWordWrappingState(ref state, i, m_CharacterCount, textInfo);
				}
				if (flag12)
				{
					SaveWordWrappingState(ref state3, i, m_CharacterCount, textInfo);
				}
			}
			m_CharacterCount++;
		}
		num4 = m_MaxFontSize - m_MinFontSize;
		if (isTextAutoSizingEnabled && num4 > 0.051f && fontSize < generationSettings.fontSizeMax && m_AutoSizeIterationCount < m_AutoSizeMaxIterationCount)
		{
			if (m_CharWidthAdjDelta < generationSettings.charWidthMaxAdj / 100f)
			{
				m_CharWidthAdjDelta = 0f;
			}
			m_MinFontSize = fontSize;
			float num46 = Mathf.Max((m_MaxFontSize - fontSize) / 2f, 0.05f);
			fontSize += num46;
			fontSize = Mathf.Min((float)(int)(fontSize * 20f + 0.5f) / 20f, generationSettings.fontSizeMax);
			return Vector2.zero;
		}
		m_IsAutoSizePointSizeSet = true;
		m_IsCalculatingPreferredValues = false;
		num7 += ((generationSettings.margins.x > 0f) ? generationSettings.margins.x : 0f);
		num7 += ((generationSettings.margins.z > 0f) ? generationSettings.margins.z : 0f);
		num8 += ((generationSettings.margins.y > 0f) ? generationSettings.margins.y : 0f);
		num8 += ((generationSettings.margins.w > 0f) ? generationSettings.margins.w : 0f);
		num7 = (float)(int)(num7 * 100f + 1f) / 100f;
		num8 = (float)(int)(num8 * 100f + 1f) / 100f;
		return new Vector2(num7, num8);
	}

	private void PopulateTextBackingArray(string sourceText)
	{
		int length = sourceText?.Length ?? 0;
		PopulateTextBackingArray(sourceText, 0, length);
	}

	private void PopulateTextBackingArray(string sourceText, int start, int length)
	{
		int num = 0;
		int i;
		if (sourceText == null)
		{
			i = 0;
			length = 0;
		}
		else
		{
			i = Mathf.Clamp(start, 0, sourceText.Length);
			length = Mathf.Clamp(length, 0, (start + length < sourceText.Length) ? length : (sourceText.Length - start));
		}
		if (length >= m_TextBackingArray.Capacity)
		{
			m_TextBackingArray.Resize(length);
		}
		for (int num2 = i + length; i < num2; i++)
		{
			m_TextBackingArray[num] = sourceText[i];
			num++;
		}
		m_TextBackingArray[num] = 0u;
		m_TextBackingArray.Count = num;
	}

	private void PopulateTextBackingArray(StringBuilder sourceText, int start, int length)
	{
		int num = 0;
		int i;
		if (sourceText == null)
		{
			i = 0;
			length = 0;
		}
		else
		{
			i = Mathf.Clamp(start, 0, sourceText.Length);
			length = Mathf.Clamp(length, 0, (start + length < sourceText.Length) ? length : (sourceText.Length - start));
		}
		if (length >= m_TextBackingArray.Capacity)
		{
			m_TextBackingArray.Resize(length);
		}
		for (int num2 = i + length; i < num2; i++)
		{
			m_TextBackingArray[num] = sourceText[i];
			num++;
		}
		m_TextBackingArray[num] = 0u;
		m_TextBackingArray.Count = num;
	}

	private void PopulateTextBackingArray(char[] sourceText, int start, int length)
	{
		int num = 0;
		int i;
		if (sourceText == null)
		{
			i = 0;
			length = 0;
		}
		else
		{
			i = Mathf.Clamp(start, 0, sourceText.Length);
			length = Mathf.Clamp(length, 0, (start + length < sourceText.Length) ? length : (sourceText.Length - start));
		}
		if (length >= m_TextBackingArray.Capacity)
		{
			m_TextBackingArray.Resize(length);
		}
		for (int num2 = i + length; i < num2; i++)
		{
			m_TextBackingArray[num] = sourceText[i];
			num++;
		}
		m_TextBackingArray[num] = 0u;
		m_TextBackingArray.Count = num;
	}

	private void PopulateTextProcessingArray(TextGenerationSettings generationSettings)
	{
		int count = m_TextBackingArray.Count;
		if (m_TextProcessingArray.Length < count)
		{
			TextGeneratorUtilities.ResizeInternalArray(ref m_TextProcessingArray, count);
		}
		TextProcessingStack<int>.SetDefault(m_TextStyleStacks, 0);
		m_TextStyleStackDepth = 0;
		int writeIndex = 0;
		int hashCode = m_TextStyleStacks[0].Pop();
		TextStyle style = TextGeneratorUtilities.GetStyle(generationSettings, hashCode);
		if (style != null && style.hashCode != -1183493901)
		{
			TextGeneratorUtilities.InsertOpeningStyleTag(style, ref m_TextProcessingArray, ref writeIndex, ref m_TextStyleStackDepth, ref m_TextStyleStacks, ref generationSettings);
		}
		bool flag = generationSettings.tagNoParsing;
		for (int i = 0; i < count; i++)
		{
			uint num = m_TextBackingArray[i];
			if (num == 0)
			{
				break;
			}
			if (num == 92 && i < count - 1)
			{
				switch (m_TextBackingArray[i + 1])
				{
				case 92u:
					if (generationSettings.parseControlCharacters)
					{
						i++;
					}
					break;
				case 110u:
					if (!generationSettings.parseControlCharacters)
					{
						break;
					}
					m_TextProcessingArray[writeIndex] = new TextProcessingElement
					{
						elementType = TextProcessingElementType.TextCharacterElement,
						stringIndex = i,
						length = 1,
						unicode = 10u
					};
					i++;
					writeIndex++;
					continue;
				case 114u:
					if (!generationSettings.parseControlCharacters)
					{
						break;
					}
					m_TextProcessingArray[writeIndex] = new TextProcessingElement
					{
						elementType = TextProcessingElementType.TextCharacterElement,
						stringIndex = i,
						length = 1,
						unicode = 13u
					};
					i++;
					writeIndex++;
					continue;
				case 116u:
					if (!generationSettings.parseControlCharacters)
					{
						break;
					}
					m_TextProcessingArray[writeIndex] = new TextProcessingElement
					{
						elementType = TextProcessingElementType.TextCharacterElement,
						stringIndex = i,
						length = 1,
						unicode = 9u
					};
					i++;
					writeIndex++;
					continue;
				case 118u:
					if (!generationSettings.parseControlCharacters)
					{
						break;
					}
					m_TextProcessingArray[writeIndex] = new TextProcessingElement
					{
						elementType = TextProcessingElementType.TextCharacterElement,
						stringIndex = i,
						length = 1,
						unicode = 11u
					};
					i++;
					writeIndex++;
					continue;
				case 117u:
					if (count > i + 5 && TextGeneratorUtilities.IsValidUTF16(m_TextBackingArray, i + 2))
					{
						m_TextProcessingArray[writeIndex] = new TextProcessingElement
						{
							elementType = TextProcessingElementType.TextCharacterElement,
							stringIndex = i,
							length = 6,
							unicode = TextGeneratorUtilities.GetUTF16(m_TextBackingArray, i + 2)
						};
						i += 5;
						writeIndex++;
						continue;
					}
					break;
				case 85u:
					if (count > i + 9 && TextGeneratorUtilities.IsValidUTF32(m_TextBackingArray, i + 2))
					{
						m_TextProcessingArray[writeIndex] = new TextProcessingElement
						{
							elementType = TextProcessingElementType.TextCharacterElement,
							stringIndex = i,
							length = 10,
							unicode = TextGeneratorUtilities.GetUTF32(m_TextBackingArray, i + 2)
						};
						i += 9;
						writeIndex++;
						continue;
					}
					break;
				}
			}
			if (num >= 55296 && num <= 56319 && count > i + 1 && m_TextBackingArray[i + 1] >= 56320 && m_TextBackingArray[i + 1] <= 57343)
			{
				m_TextProcessingArray[writeIndex] = new TextProcessingElement
				{
					elementType = TextProcessingElementType.TextCharacterElement,
					stringIndex = i,
					length = 2,
					unicode = TextGeneratorUtilities.ConvertToUTF32(num, m_TextBackingArray[i + 1])
				};
				i++;
				writeIndex++;
				continue;
			}
			if (num == 60 && generationSettings.richText)
			{
				switch ((MarkupTag)TextGeneratorUtilities.GetMarkupTagHashCode(m_TextBackingArray, i + 1))
				{
				case MarkupTag.NO_PARSE:
					flag = true;
					break;
				case MarkupTag.SLASH_NO_PARSE:
					flag = false;
					break;
				case MarkupTag.BR:
					if (flag)
					{
						break;
					}
					if (writeIndex == m_TextProcessingArray.Length)
					{
						TextGeneratorUtilities.ResizeInternalArray(ref m_TextProcessingArray);
					}
					m_TextProcessingArray[writeIndex] = new TextProcessingElement
					{
						elementType = TextProcessingElementType.TextCharacterElement,
						stringIndex = i,
						length = 4,
						unicode = 10u
					};
					writeIndex++;
					i += 3;
					continue;
				case MarkupTag.CR:
					if (flag)
					{
						break;
					}
					if (writeIndex == m_TextProcessingArray.Length)
					{
						TextGeneratorUtilities.ResizeInternalArray(ref m_TextProcessingArray);
					}
					m_TextProcessingArray[writeIndex] = new TextProcessingElement
					{
						elementType = TextProcessingElementType.TextCharacterElement,
						stringIndex = i,
						length = 4,
						unicode = 13u
					};
					writeIndex++;
					i += 3;
					continue;
				case MarkupTag.NBSP:
					if (flag)
					{
						break;
					}
					if (writeIndex == m_TextProcessingArray.Length)
					{
						TextGeneratorUtilities.ResizeInternalArray(ref m_TextProcessingArray);
					}
					m_TextProcessingArray[writeIndex] = new TextProcessingElement
					{
						elementType = TextProcessingElementType.TextCharacterElement,
						stringIndex = i,
						length = 6,
						unicode = 160u
					};
					writeIndex++;
					i += 5;
					continue;
				case MarkupTag.ZWSP:
					if (flag)
					{
						break;
					}
					if (writeIndex == m_TextProcessingArray.Length)
					{
						TextGeneratorUtilities.ResizeInternalArray(ref m_TextProcessingArray);
					}
					m_TextProcessingArray[writeIndex] = new TextProcessingElement
					{
						elementType = TextProcessingElementType.TextCharacterElement,
						stringIndex = i,
						length = 6,
						unicode = 8203u
					};
					writeIndex++;
					i += 5;
					continue;
				case MarkupTag.ZWJ:
					if (flag)
					{
						break;
					}
					if (writeIndex == m_TextProcessingArray.Length)
					{
						TextGeneratorUtilities.ResizeInternalArray(ref m_TextProcessingArray);
					}
					m_TextProcessingArray[writeIndex] = new TextProcessingElement
					{
						elementType = TextProcessingElementType.TextCharacterElement,
						stringIndex = i,
						length = 5,
						unicode = 8205u
					};
					writeIndex++;
					i += 4;
					continue;
				case MarkupTag.SHY:
					if (flag)
					{
						break;
					}
					if (writeIndex == m_TextProcessingArray.Length)
					{
						TextGeneratorUtilities.ResizeInternalArray(ref m_TextProcessingArray);
					}
					m_TextProcessingArray[writeIndex] = new TextProcessingElement
					{
						elementType = TextProcessingElementType.TextCharacterElement,
						stringIndex = i,
						length = 5,
						unicode = 173u
					};
					writeIndex++;
					i += 4;
					continue;
				case MarkupTag.A:
					if (m_TextBackingArray.Count > i + 4 && m_TextBackingArray[i + 3] == 104 && m_TextBackingArray[i + 4] == 114)
					{
						TextGeneratorUtilities.InsertOpeningTextStyle(TextGeneratorUtilities.GetStyle(generationSettings, 65), ref m_TextProcessingArray, ref writeIndex, ref m_TextStyleStackDepth, ref m_TextStyleStacks, ref generationSettings);
					}
					break;
				case MarkupTag.STYLE:
				{
					if (flag)
					{
						break;
					}
					int k = writeIndex;
					if (!TextGeneratorUtilities.ReplaceOpeningStyleTag(ref m_TextBackingArray, i, out var srcOffset, ref m_TextProcessingArray, ref writeIndex, ref m_TextStyleStackDepth, ref m_TextStyleStacks, ref generationSettings))
					{
						break;
					}
					for (; k < writeIndex; k++)
					{
						m_TextProcessingArray[k].stringIndex = i;
						m_TextProcessingArray[k].length = srcOffset - i + 1;
					}
					i = srcOffset;
					continue;
				}
				case MarkupTag.SLASH_A:
					TextGeneratorUtilities.InsertClosingTextStyle(TextGeneratorUtilities.GetStyle(generationSettings, 65), ref m_TextProcessingArray, ref writeIndex, ref m_TextStyleStackDepth, ref m_TextStyleStacks, ref generationSettings);
					break;
				case MarkupTag.SLASH_STYLE:
				{
					if (flag)
					{
						break;
					}
					int j = writeIndex;
					TextGeneratorUtilities.ReplaceClosingStyleTag(ref m_TextProcessingArray, ref writeIndex, ref m_TextStyleStackDepth, ref m_TextStyleStacks, ref generationSettings);
					for (; j < writeIndex; j++)
					{
						m_TextProcessingArray[j].stringIndex = i;
						m_TextProcessingArray[j].length = 8;
					}
					i += 7;
					continue;
				}
				}
			}
			if (writeIndex == m_TextProcessingArray.Length)
			{
				TextGeneratorUtilities.ResizeInternalArray(ref m_TextProcessingArray);
			}
			m_TextProcessingArray[writeIndex] = new TextProcessingElement
			{
				elementType = TextProcessingElementType.TextCharacterElement,
				stringIndex = i,
				length = 1,
				unicode = num
			};
			writeIndex++;
		}
		m_TextStyleStackDepth = 0;
		if (style != null && style.hashCode != -1183493901)
		{
			TextGeneratorUtilities.InsertClosingStyleTag(ref m_TextProcessingArray, ref writeIndex, ref m_TextStyleStackDepth, ref m_TextStyleStacks, ref generationSettings);
		}
		if (writeIndex == m_TextProcessingArray.Length)
		{
			TextGeneratorUtilities.ResizeInternalArray(ref m_TextProcessingArray);
		}
		m_TextProcessingArray[writeIndex].unicode = 0u;
		m_InternalTextProcessingArraySize = writeIndex;
	}

	private void InsertNewLine(int i, float baseScale, float currentElementScale, float currentEmScale, float boldSpacingAdjustment, float characterSpacingAdjustment, float width, float lineGap, ref bool isMaxVisibleDescenderSet, ref float maxVisibleDescender, TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		float num = m_MaxLineAscender - m_StartOfLineAscender;
		if (m_LineOffset > 0f && Math.Abs(num) > 0.01f && !m_IsDrivenLineSpacing && !m_IsNewPage)
		{
			TextGeneratorUtilities.AdjustLineOffset(m_FirstCharacterOfLine, m_CharacterCount, num, textInfo);
			m_MaxDescender -= num;
			m_LineOffset += num;
		}
		float num2 = m_MaxLineAscender - m_LineOffset;
		float num3 = m_MaxLineDescender - m_LineOffset;
		m_MaxDescender = ((m_MaxDescender < num3) ? m_MaxDescender : num3);
		if (!isMaxVisibleDescenderSet)
		{
			maxVisibleDescender = m_MaxDescender;
		}
		if (generationSettings.useMaxVisibleDescender && (m_CharacterCount >= generationSettings.maxVisibleCharacters || m_LineNumber >= generationSettings.maxVisibleLines))
		{
			isMaxVisibleDescenderSet = true;
		}
		textInfo.lineInfo[m_LineNumber].firstCharacterIndex = m_FirstCharacterOfLine;
		textInfo.lineInfo[m_LineNumber].firstVisibleCharacterIndex = (m_FirstVisibleCharacterOfLine = ((m_FirstCharacterOfLine > m_FirstVisibleCharacterOfLine) ? m_FirstCharacterOfLine : m_FirstVisibleCharacterOfLine));
		textInfo.lineInfo[m_LineNumber].lastCharacterIndex = (m_LastCharacterOfLine = ((m_CharacterCount - 1 > 0) ? (m_CharacterCount - 1) : 0));
		textInfo.lineInfo[m_LineNumber].lastVisibleCharacterIndex = (m_LastVisibleCharacterOfLine = ((m_LastVisibleCharacterOfLine < m_FirstVisibleCharacterOfLine) ? m_FirstVisibleCharacterOfLine : m_LastVisibleCharacterOfLine));
		textInfo.lineInfo[m_LineNumber].characterCount = textInfo.lineInfo[m_LineNumber].lastCharacterIndex - textInfo.lineInfo[m_LineNumber].firstCharacterIndex + 1;
		textInfo.lineInfo[m_LineNumber].visibleCharacterCount = m_LineVisibleCharacterCount;
		textInfo.lineInfo[m_LineNumber].visibleSpaceCount = m_LineVisibleSpaceCount;
		textInfo.lineInfo[m_LineNumber].lineExtents.min = new Vector2(textInfo.textElementInfo[m_FirstVisibleCharacterOfLine].bottomLeft.x, num3);
		textInfo.lineInfo[m_LineNumber].lineExtents.max = new Vector2(textInfo.textElementInfo[m_LastVisibleCharacterOfLine].topRight.x, num2);
		textInfo.lineInfo[m_LineNumber].length = textInfo.lineInfo[m_LineNumber].lineExtents.max.x;
		textInfo.lineInfo[m_LineNumber].width = width;
		float adjustedHorizontalAdvance = textInfo.textElementInfo[m_LastVisibleCharacterOfLine].adjustedHorizontalAdvance;
		float num4 = (adjustedHorizontalAdvance * currentElementScale + (m_CurrentFontAsset.regularStyleSpacing + characterSpacingAdjustment + boldSpacingAdjustment) * currentEmScale + m_CSpacing) * (1f - generationSettings.charWidthMaxAdj);
		float xAdvance = (textInfo.lineInfo[m_LineNumber].maxAdvance = textInfo.textElementInfo[m_LastVisibleCharacterOfLine].xAdvance + (generationSettings.isRightToLeft ? num4 : (0f - num4)));
		textInfo.textElementInfo[m_LastVisibleCharacterOfLine].xAdvance = xAdvance;
		textInfo.lineInfo[m_LineNumber].baseline = 0f - m_LineOffset;
		textInfo.lineInfo[m_LineNumber].ascender = num2;
		textInfo.lineInfo[m_LineNumber].descender = num3;
		textInfo.lineInfo[m_LineNumber].lineHeight = num2 - num3 + lineGap * baseScale;
		m_FirstCharacterOfLine = m_CharacterCount;
		m_LineVisibleCharacterCount = 0;
		m_LineVisibleSpaceCount = 0;
		SaveWordWrappingState(ref m_SavedLineState, i, m_CharacterCount - 1, textInfo);
		m_LineNumber++;
		if (m_LineNumber >= textInfo.lineInfo.Length)
		{
			TextGeneratorUtilities.ResizeLineExtents(m_LineNumber, textInfo);
		}
		if (m_LineHeight == -32767f)
		{
			float adjustedAscender = textInfo.textElementInfo[m_CharacterCount].adjustedAscender;
			float num5 = 0f - m_MaxLineDescender + adjustedAscender + (lineGap + m_LineSpacingDelta) * baseScale + generationSettings.lineSpacing * currentEmScale;
			m_LineOffset += num5;
			m_StartOfLineAscender = adjustedAscender;
		}
		else
		{
			m_LineOffset += m_LineHeight + generationSettings.lineSpacing * currentEmScale;
		}
		m_MaxLineAscender = -32767f;
		m_MaxLineDescender = 32767f;
		m_XAdvance = 0f + m_TagIndent;
	}

	protected void DoMissingGlyphCallback(uint unicode, int stringIndex, FontAsset fontAsset, TextInfo textInfo)
	{
		TextGenerator.OnMissingCharacter?.Invoke(unicode, stringIndex, textInfo, fontAsset);
	}

	private void ClearMarkupTagAttributes()
	{
		int num = m_XmlAttribute.Length;
		for (int i = 0; i < num; i++)
		{
			m_XmlAttribute[i] = default(RichTextTagAttribute);
		}
	}
}
[Flags]
internal enum HorizontalAlignment
{
	Left = 1,
	Center = 2,
	Right = 4,
	Justified = 8,
	Flush = 0x10,
	Geometry = 0x20
}
[Flags]
internal enum VerticalAlignment
{
	Top = 0x100,
	Middle = 0x200,
	Bottom = 0x400,
	Baseline = 0x800,
	Midline = 0x1000,
	Capline = 0x2000
}
internal enum TextAlignment
{
	TopLeft = 257,
	TopCenter = 258,
	TopRight = 260,
	TopJustified = 264,
	TopFlush = 272,
	TopGeoAligned = 288,
	MiddleLeft = 513,
	MiddleCenter = 514,
	MiddleRight = 516,
	MiddleJustified = 520,
	MiddleFlush = 528,
	MiddleGeoAligned = 544,
	BottomLeft = 1025,
	BottomCenter = 1026,
	BottomRight = 1028,
	BottomJustified = 1032,
	BottomFlush = 1040,
	BottomGeoAligned = 1056,
	BaselineLeft = 2049,
	BaselineCenter = 2050,
	BaselineRight = 2052,
	BaselineJustified = 2056,
	BaselineFlush = 2064,
	BaselineGeoAligned = 2080,
	MidlineLeft = 4097,
	MidlineCenter = 4098,
	MidlineRight = 4100,
	MidlineJustified = 4104,
	MidlineFlush = 4112,
	MidlineGeoAligned = 4128,
	CaplineLeft = 8193,
	CaplineCenter = 8194,
	CaplineRight = 8196,
	CaplineJustified = 8200,
	CaplineFlush = 8208,
	CaplineGeoAligned = 8224
}
[Flags]
public enum FontStyles
{
	Normal = 0,
	Bold = 1,
	Italic = 2,
	Underline = 4,
	LowerCase = 8,
	UpperCase = 0x10,
	SmallCaps = 0x20,
	Strikethrough = 0x40,
	Superscript = 0x80,
	Subscript = 0x100,
	Highlight = 0x200
}
internal enum TextOverflowMode
{
	Overflow,
	Ellipsis,
	Masking,
	Truncate,
	ScrollRect,
	Page,
	Linked
}
internal enum TextureMapping
{
	Character,
	Line,
	Paragraph,
	MatchAspect
}
internal enum TextWrappingMode
{
	NoWrap,
	Normal,
	PreserveWhitespace,
	PreserveWhitespaceNoWrap
}
internal enum TextInputSource
{
	TextInputBox,
	SetText,
	SetTextArray,
	TextString
}
[Serializable]
internal struct MeshExtents
{
	public Vector2 min;

	public Vector2 max;

	public MeshExtents(Vector2 min, Vector2 max)
	{
		this.min = min;
		this.max = max;
	}

	public override string ToString()
	{
		return "Min (" + min.x.ToString("f2") + ", " + min.y.ToString("f2") + ")   Max (" + max.x.ToString("f2") + ", " + max.y.ToString("f2") + ")";
	}
}
internal struct XmlTagAttribute
{
	public int nameHashCode;

	public TagValueType valueType;

	public int valueStartIndex;

	public int valueLength;

	public int valueHashCode;
}
internal struct RichTextTagAttribute
{
	public int nameHashCode;

	public int valueHashCode;

	public TagValueType valueType;

	public int valueStartIndex;

	public int valueLength;

	public TagUnitType unitType;
}
[DebuggerDisplay("Unicode ({unicode})  '{(char)unicode}'")]
internal struct TextProcessingElement
{
	public TextProcessingElementType elementType;

	public uint unicode;

	public int stringIndex;

	public int length;
}
internal struct TextBackingContainer
{
	private uint[] m_Array;

	private int m_Count;

	public uint[] Text => m_Array;

	public int Capacity => m_Array.Length;

	public int Count
	{
		get
		{
			return m_Count;
		}
		set
		{
			m_Count = value;
		}
	}

	public uint this[int index]
	{
		get
		{
			return m_Array[index];
		}
		set
		{
			if (index >= m_Array.Length)
			{
				Resize(index);
			}
			m_Array[index] = value;
		}
	}

	public TextBackingContainer(int size)
	{
		m_Array = new uint[size];
		m_Count = 0;
	}

	public void Resize(int size)
	{
		size = Mathf.NextPowerOfTwo(size + 1);
		Array.Resize(ref m_Array, size);
	}
}
internal struct CharacterSubstitution
{
	public int index;

	public uint unicode;

	public CharacterSubstitution(int index, uint unicode)
	{
		this.index = index;
		this.unicode = unicode;
	}
}
internal struct Offset
{
	private float m_Left;

	private float m_Right;

	private float m_Top;

	private float m_Bottom;

	private static readonly Offset k_ZeroOffset = new Offset(0f, 0f, 0f, 0f);

	public float left
	{
		get
		{
			return m_Left;
		}
		set
		{
			m_Left = value;
		}
	}

	public float right
	{
		get
		{
			return m_Right;
		}
		set
		{
			m_Right = value;
		}
	}

	public float top
	{
		get
		{
			return m_Top;
		}
		set
		{
			m_Top = value;
		}
	}

	public float bottom
	{
		get
		{
			return m_Bottom;
		}
		set
		{
			m_Bottom = value;
		}
	}

	public float horizontal
	{
		get
		{
			return m_Left;
		}
		set
		{
			m_Left = value;
			m_Right = value;
		}
	}

	public float vertical
	{
		get
		{
			return m_Top;
		}
		set
		{
			m_Top = value;
			m_Bottom = value;
		}
	}

	public static Offset zero => k_ZeroOffset;

	public Offset(float left, float right, float top, float bottom)
	{
		m_Left = left;
		m_Right = right;
		m_Top = top;
		m_Bottom = bottom;
	}

	public Offset(float horizontal, float vertical)
	{
		m_Left = horizontal;
		m_Right = horizontal;
		m_Top = vertical;
		m_Bottom = vertical;
	}

	public static bool operator ==(Offset lhs, Offset rhs)
	{
		return lhs.m_Left == rhs.m_Left && lhs.m_Right == rhs.m_Right && lhs.m_Top == rhs.m_Top && lhs.m_Bottom == rhs.m_Bottom;
	}

	public static bool operator !=(Offset lhs, Offset rhs)
	{
		return !(lhs == rhs);
	}

	public static Offset operator *(Offset a, float b)
	{
		return new Offset(a.m_Left * b, a.m_Right * b, a.m_Top * b, a.m_Bottom * b);
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override bool Equals(object obj)
	{
		return base.Equals(obj);
	}

	public bool Equals(Offset other)
	{
		return base.Equals((object)other);
	}
}
internal struct HighlightState
{
	public Color32 color;

	public Offset padding;

	public HighlightState(Color32 color, Offset padding)
	{
		this.color = color;
		this.padding = padding;
	}

	public static bool operator ==(HighlightState lhs, HighlightState rhs)
	{
		return lhs.color.r == rhs.color.r && lhs.color.g == rhs.color.g && lhs.color.b == rhs.color.b && lhs.color.a == rhs.color.a && lhs.padding == rhs.padding;
	}

	public static bool operator !=(HighlightState lhs, HighlightState rhs)
	{
		return !(lhs == rhs);
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override bool Equals(object obj)
	{
		return base.Equals(obj);
	}

	public bool Equals(HighlightState other)
	{
		return base.Equals((object)other);
	}
}
internal struct WordWrapState
{
	public int previousWordBreak;

	public int totalCharacterCount;

	public int visibleCharacterCount;

	public int visibleSpaceCount;

	public int visibleSpriteCount;

	public int visibleLinkCount;

	public int firstCharacterIndex;

	public int firstVisibleCharacterIndex;

	public int lastCharacterIndex;

	public int lastVisibleCharIndex;

	public int lineNumber;

	public float maxCapHeight;

	public float maxAscender;

	public float maxDescender;

	public float maxLineAscender;

	public float maxLineDescender;

	public float startOfLineAscender;

	public float xAdvance;

	public float preferredWidth;

	public float preferredHeight;

	public float previousLineScale;

	public float pageAscender;

	public int wordCount;

	public FontStyles fontStyle;

	public float fontScale;

	public float fontScaleMultiplier;

	public int italicAngle;

	public float currentFontSize;

	public float baselineOffset;

	public float lineOffset;

	public TextInfo textInfo;

	public LineInfo lineInfo;

	public Color32 vertexColor;

	public Color32 underlineColor;

	public Color32 strikethroughColor;

	public Color32 highlightColor;

	public HighlightState highlightState;

	public FontStyleStack basicStyleStack;

	public TextProcessingStack<int> italicAngleStack;

	public TextProcessingStack<Color32> colorStack;

	public TextProcessingStack<Color32> underlineColorStack;

	public TextProcessingStack<Color32> strikethroughColorStack;

	public TextProcessingStack<Color32> highlightColorStack;

	public TextProcessingStack<HighlightState> highlightStateStack;

	public TextProcessingStack<TextColorGradient> colorGradientStack;

	public TextProcessingStack<float> sizeStack;

	public TextProcessingStack<float> indentStack;

	public TextProcessingStack<TextFontWeight> fontWeightStack;

	public TextProcessingStack<int> styleStack;

	public TextProcessingStack<float> baselineStack;

	public TextProcessingStack<int> actionStack;

	public TextProcessingStack<MaterialReference> materialReferenceStack;

	public TextProcessingStack<TextAlignment> lineJustificationStack;

	public int lastBaseGlyphIndex;

	public int spriteAnimationId;

	public FontAsset currentFontAsset;

	public SpriteAsset currentSpriteAsset;

	public Material currentMaterial;

	public int currentMaterialIndex;

	public Extents meshExtents;

	public bool tagNoParsing;

	public bool isNonBreakingSpace;

	public bool isDrivenLineSpacing;

	public Vector3 fxScale;

	public Quaternion fxRotation;
}
internal static class TextGeneratorUtilities
{
	public static readonly Vector2 largePositiveVector2 = new Vector2(2.1474836E+09f, 2.1474836E+09f);

	public static readonly Vector2 largeNegativeVector2 = new Vector2(-214748370f, -214748370f);

	public const float largePositiveFloat = 32767f;

	public const float largeNegativeFloat = -32767f;

	private const int k_DoubleQuotes = 34;

	private const int k_GreaterThan = 62;

	private const int k_ZeroWidthSpace = 8203;

	private const string k_LookupStringU = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-";

	public static bool Approximately(float a, float b)
	{
		return b - 0.0001f < a && a < b + 0.0001f;
	}

	public static Color32 HexCharsToColor(char[] hexChars, int tagCount)
	{
		switch (tagCount)
		{
		case 4:
		{
			byte r8 = (byte)(HexToInt(hexChars[1]) * 16 + HexToInt(hexChars[1]));
			byte g8 = (byte)(HexToInt(hexChars[2]) * 16 + HexToInt(hexChars[2]));
			byte b8 = (byte)(HexToInt(hexChars[3]) * 16 + HexToInt(hexChars[3]));
			return new Color32(r8, g8, b8, byte.MaxValue);
		}
		case 5:
		{
			byte r7 = (byte)(HexToInt(hexChars[1]) * 16 + HexToInt(hexChars[1]));
			byte g7 = (byte)(HexToInt(hexChars[2]) * 16 + HexToInt(hexChars[2]));
			byte b7 = (byte)(HexToInt(hexChars[3]) * 16 + HexToInt(hexChars[3]));
			byte a4 = (byte)(HexToInt(hexChars[4]) * 16 + HexToInt(hexChars[4]));
			return new Color32(r7, g7, b7, a4);
		}
		case 7:
		{
			byte r6 = (byte)(HexToInt(hexChars[1]) * 16 + HexToInt(hexChars[2]));
			byte g6 = (byte)(HexToInt(hexChars[3]) * 16 + HexToInt(hexChars[4]));
			byte b6 = (byte)(HexToInt(hexChars[5]) * 16 + HexToInt(hexChars[6]));
			return new Color32(r6, g6, b6, byte.MaxValue);
		}
		case 9:
		{
			byte r5 = (byte)(HexToInt(hexChars[1]) * 16 + HexToInt(hexChars[2]));
			byte g5 = (byte)(HexToInt(hexChars[3]) * 16 + HexToInt(hexChars[4]));
			byte b5 = (byte)(HexToInt(hexChars[5]) * 16 + HexToInt(hexChars[6]));
			byte a3 = (byte)(HexToInt(hexChars[7]) * 16 + HexToInt(hexChars[8]));
			return new Color32(r5, g5, b5, a3);
		}
		case 10:
		{
			byte r4 = (byte)(HexToInt(hexChars[7]) * 16 + HexToInt(hexChars[7]));
			byte g4 = (byte)(HexToInt(hexChars[8]) * 16 + HexToInt(hexChars[8]));
			byte b4 = (byte)(HexToInt(hexChars[9]) * 16 + HexToInt(hexChars[9]));
			return new Color32(r4, g4, b4, byte.MaxValue);
		}
		case 11:
		{
			byte r3 = (byte)(HexToInt(hexChars[7]) * 16 + HexToInt(hexChars[7]));
			byte g3 = (byte)(HexToInt(hexChars[8]) * 16 + HexToInt(hexChars[8]));
			byte b3 = (byte)(HexToInt(hexChars[9]) * 16 + HexToInt(hexChars[9]));
			byte a2 = (byte)(HexToInt(hexChars[10]) * 16 + HexToInt(hexChars[10]));
			return new Color32(r3, g3, b3, a2);
		}
		case 13:
		{
			byte r2 = (byte)(HexToInt(hexChars[7]) * 16 + HexToInt(hexChars[8]));
			byte g2 = (byte)(HexToInt(hexChars[9]) * 16 + HexToInt(hexChars[10]));
			byte b2 = (byte)(HexToInt(hexChars[11]) * 16 + HexToInt(hexChars[12]));
			return new Color32(r2, g2, b2, byte.MaxValue);
		}
		case 15:
		{
			byte r = (byte)(HexToInt(hexChars[7]) * 16 + HexToInt(hexChars[8]));
			byte g = (byte)(HexToInt(hexChars[9]) * 16 + HexToInt(hexChars[10]));
			byte b = (byte)(HexToInt(hexChars[11]) * 16 + HexToInt(hexChars[12]));
			byte a = (byte)(HexToInt(hexChars[13]) * 16 + HexToInt(hexChars[14]));
			return new Color32(r, g, b, a);
		}
		default:
			return new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}
	}

	public static Color32 HexCharsToColor(char[] hexChars, int startIndex, int length)
	{
		switch (length)
		{
		case 7:
		{
			byte r2 = (byte)(HexToInt(hexChars[startIndex + 1]) * 16 + HexToInt(hexChars[startIndex + 2]));
			byte g2 = (byte)(HexToInt(hexChars[startIndex + 3]) * 16 + HexToInt(hexChars[startIndex + 4]));
			byte b2 = (byte)(HexToInt(hexChars[startIndex + 5]) * 16 + HexToInt(hexChars[startIndex + 6]));
			return new Color32(r2, g2, b2, byte.MaxValue);
		}
		case 9:
		{
			byte r = (byte)(HexToInt(hexChars[startIndex + 1]) * 16 + HexToInt(hexChars[startIndex + 2]));
			byte g = (byte)(HexToInt(hexChars[startIndex + 3]) * 16 + HexToInt(hexChars[startIndex + 4]));
			byte b = (byte)(HexToInt(hexChars[startIndex + 5]) * 16 + HexToInt(hexChars[startIndex + 6]));
			byte a = (byte)(HexToInt(hexChars[startIndex + 7]) * 16 + HexToInt(hexChars[startIndex + 8]));
			return new Color32(r, g, b, a);
		}
		default:
			return new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}
	}

	public static uint HexToInt(char hex)
	{
		return hex switch
		{
			'0' => 0u, 
			'1' => 1u, 
			'2' => 2u, 
			'3' => 3u, 
			'4' => 4u, 
			'5' => 5u, 
			'6' => 6u, 
			'7' => 7u, 
			'8' => 8u, 
			'9' => 9u, 
			'A' => 10u, 
			'B' => 11u, 
			'C' => 12u, 
			'D' => 13u, 
			'E' => 14u, 
			'F' => 15u, 
			'a' => 10u, 
			'b' => 11u, 
			'c' => 12u, 
			'd' => 13u, 
			'e' => 14u, 
			'f' => 15u, 
			_ => 15u, 
		};
	}

	public static float ConvertToFloat(char[] chars, int startIndex, int length)
	{
		int lastIndex;
		return ConvertToFloat(chars, startIndex, length, out lastIndex);
	}

	public static float ConvertToFloat(char[] chars, int startIndex, int length, out int lastIndex)
	{
		if (startIndex == 0)
		{
			lastIndex = 0;
			return -32767f;
		}
		int num = startIndex + length;
		bool flag = true;
		float num2 = 0f;
		int num3 = 1;
		if (chars[startIndex] == '+')
		{
			num3 = 1;
			startIndex++;
		}
		else if (chars[startIndex] == '-')
		{
			num3 = -1;
			startIndex++;
		}
		float num4 = 0f;
		for (int i = startIndex; i < num; i++)
		{
			uint num5 = chars[i];
			if ((num5 >= 48 && num5 <= 57) || num5 == 46)
			{
				if (num5 == 46)
				{
					flag = false;
					num2 = 0.1f;
				}
				else if (flag)
				{
					num4 = num4 * 10f + (float)((num5 - 48) * num3);
				}
				else
				{
					num4 += (float)(num5 - 48) * num2 * (float)num3;
					num2 *= 0.1f;
				}
			}
			else if (num5 == 44)
			{
				if (i + 1 < num && chars[i + 1] == ' ')
				{
					lastIndex = i + 1;
				}
				else
				{
					lastIndex = i;
				}
				return num4;
			}
		}
		lastIndex = num;
		return num4;
	}

	public static Vector2 PackUV(float x, float y, float scale)
	{
		Vector2 result = default(Vector2);
		result.x = (int)(x * 511f);
		result.y = (int)(y * 511f);
		result.x = result.x * 4096f + result.y;
		result.y = scale;
		return result;
	}

	public static void ResizeInternalArray<T>(ref T[] array)
	{
		int newSize = Mathf.NextPowerOfTwo(array.Length + 1);
		Array.Resize(ref array, newSize);
	}

	public static void ResizeInternalArray<T>(ref T[] array, int size)
	{
		size = Mathf.NextPowerOfTwo(size + 1);
		Array.Resize(ref array, size);
	}

	private static bool IsTagName(ref string text, string tag, int index)
	{
		if (text.Length < index + tag.Length)
		{
			return false;
		}
		for (int i = 0; i < tag.Length; i++)
		{
			if (TextUtilities.ToUpperFast(text[index + i]) != tag[i])
			{
				return false;
			}
		}
		return true;
	}

	private static bool IsTagName(ref int[] text, string tag, int index)
	{
		if (text.Length < index + tag.Length)
		{
			return false;
		}
		for (int i = 0; i < tag.Length; i++)
		{
			if (TextUtilities.ToUpperFast((char)text[index + i]) != tag[i])
			{
				return false;
			}
		}
		return true;
	}

	internal static void InsertOpeningTextStyle(TextStyle style, ref TextProcessingElement[] charBuffer, ref int writeIndex, ref int textStyleStackDepth, ref TextProcessingStack<int>[] textStyleStacks, ref TextGenerationSettings generationSettings)
	{
		if (style != null)
		{
			textStyleStackDepth++;
			textStyleStacks[textStyleStackDepth].Push(style.hashCode);
			uint[] styleOpeningTagArray = style.styleOpeningTagArray;
			InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleOpeningTagArray, ref textStyleStackDepth, ref textStyleStacks, ref generationSettings);
			textStyleStackDepth--;
		}
	}

	internal static void InsertClosingTextStyle(TextStyle style, ref TextProcessingElement[] charBuffer, ref int writeIndex, ref int textStyleStackDepth, ref TextProcessingStack<int>[] textStyleStacks, ref TextGenerationSettings generationSettings)
	{
		if (style != null)
		{
			textStyleStackDepth++;
			textStyleStacks[textStyleStackDepth].Push(style.hashCode);
			uint[] styleClosingTagArray = style.styleClosingTagArray;
			InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleClosingTagArray, ref textStyleStackDepth, ref textStyleStacks, ref generationSettings);
			textStyleStackDepth--;
		}
	}

	public static bool ReplaceOpeningStyleTag(ref TextBackingContainer sourceText, int srcIndex, out int srcOffset, ref TextProcessingElement[] charBuffer, ref int writeIndex, ref int textStyleStackDepth, ref TextProcessingStack<int>[] textStyleStacks, ref TextGenerationSettings generationSettings)
	{
		int styleHashCode = GetStyleHashCode(ref sourceText, srcIndex + 7, out srcOffset);
		TextStyle style = GetStyle(generationSettings, styleHashCode);
		if (style == null || srcOffset == 0)
		{
			return false;
		}
		textStyleStackDepth++;
		textStyleStacks[textStyleStackDepth].Push(style.hashCode);
		uint[] styleOpeningTagArray = style.styleOpeningTagArray;
		InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleOpeningTagArray, ref textStyleStackDepth, ref textStyleStacks, ref generationSettings);
		textStyleStackDepth--;
		return true;
	}

	public static void ReplaceOpeningStyleTag(ref TextProcessingElement[] charBuffer, ref int writeIndex, ref int textStyleStackDepth, ref TextProcessingStack<int>[] textStyleStacks, ref TextGenerationSettings generationSettings)
	{
		int hashCode = textStyleStacks[textStyleStackDepth + 1].Pop();
		TextStyle style = GetStyle(generationSettings, hashCode);
		if (style != null)
		{
			textStyleStackDepth++;
			uint[] styleOpeningTagArray = style.styleOpeningTagArray;
			InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleOpeningTagArray, ref textStyleStackDepth, ref textStyleStacks, ref generationSettings);
			textStyleStackDepth--;
		}
	}

	private static bool ReplaceOpeningStyleTag(ref uint[] sourceText, int srcIndex, out int srcOffset, ref TextProcessingElement[] charBuffer, ref int writeIndex, ref int textStyleStackDepth, ref TextProcessingStack<int>[] textStyleStacks, ref TextGenerationSettings generationSettings)
	{
		int styleHashCode = GetStyleHashCode(ref sourceText, srcIndex + 7, out srcOffset);
		TextStyle style = GetStyle(generationSettings, styleHashCode);
		if (style == null || srcOffset == 0)
		{
			return false;
		}
		textStyleStackDepth++;
		textStyleStacks[textStyleStackDepth].Push(style.hashCode);
		uint[] styleOpeningTagArray = style.styleOpeningTagArray;
		InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleOpeningTagArray, ref textStyleStackDepth, ref textStyleStacks, ref generationSettings);
		textStyleStackDepth--;
		return true;
	}

	public static void ReplaceClosingStyleTag(ref TextProcessingElement[] charBuffer, ref int writeIndex, ref int textStyleStackDepth, ref TextProcessingStack<int>[] textStyleStacks, ref TextGenerationSettings generationSettings)
	{
		int hashCode = textStyleStacks[textStyleStackDepth + 1].Pop();
		TextStyle style = GetStyle(generationSettings, hashCode);
		if (style != null)
		{
			textStyleStackDepth++;
			uint[] styleClosingTagArray = style.styleClosingTagArray;
			InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleClosingTagArray, ref textStyleStackDepth, ref textStyleStacks, ref generationSettings);
			textStyleStackDepth--;
		}
	}

	internal static void InsertOpeningStyleTag(TextStyle style, ref TextProcessingElement[] charBuffer, ref int writeIndex, ref int textStyleStackDepth, ref TextProcessingStack<int>[] textStyleStacks, ref TextGenerationSettings generationSettings)
	{
		if (style != null)
		{
			textStyleStacks[0].Push(style.hashCode);
			uint[] styleOpeningTagArray = style.styleOpeningTagArray;
			InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleOpeningTagArray, ref textStyleStackDepth, ref textStyleStacks, ref generationSettings);
			textStyleStackDepth = 0;
		}
	}

	internal static void InsertClosingStyleTag(ref TextProcessingElement[] charBuffer, ref int writeIndex, ref int textStyleStackDepth, ref TextProcessingStack<int>[] textStyleStacks, ref TextGenerationSettings generationSettings)
	{
		int hashCode = textStyleStacks[0].Pop();
		TextStyle style = GetStyle(generationSettings, hashCode);
		uint[] styleClosingTagArray = style.styleClosingTagArray;
		InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleClosingTagArray, ref textStyleStackDepth, ref textStyleStacks, ref generationSettings);
		textStyleStackDepth = 0;
	}

	private static void InsertTextStyleInTextProcessingArray(ref TextProcessingElement[] charBuffer, ref int writeIndex, uint[] styleDefinition, ref int textStyleStackDepth, ref TextProcessingStack<int>[] textStyleStacks, ref TextGenerationSettings generationSettings)
	{
		bool flag = generationSettings.tagNoParsing;
		int num = styleDefinition.Length;
		if (writeIndex + num >= charBuffer.Length)
		{
			ResizeInternalArray(ref charBuffer, writeIndex + num);
		}
		for (int i = 0; i < num; i++)
		{
			uint num2 = styleDefinition[i];
			if (num2 == 92 && i + 1 < num)
			{
				switch (styleDefinition[i + 1])
				{
				case 92u:
					i++;
					break;
				case 110u:
					num2 = 10u;
					i++;
					break;
				case 117u:
					if (i + 5 < num)
					{
						num2 = GetUTF16(styleDefinition, i + 2);
						i += 5;
					}
					break;
				case 85u:
					if (i + 9 < num)
					{
						num2 = GetUTF32(styleDefinition, i + 2);
						i += 9;
					}
					break;
				}
			}
			if (num2 == 60)
			{
				switch ((MarkupTag)GetMarkupTagHashCode(styleDefinition, i + 1))
				{
				case MarkupTag.NO_PARSE:
					flag = true;
					break;
				case MarkupTag.SLASH_NO_PARSE:
					flag = false;
					break;
				case MarkupTag.BR:
					if (flag)
					{
						break;
					}
					charBuffer[writeIndex].unicode = 10u;
					writeIndex++;
					i += 3;
					continue;
				case MarkupTag.CR:
					if (flag)
					{
						break;
					}
					charBuffer[writeIndex].unicode = 13u;
					writeIndex++;
					i += 3;
					continue;
				case MarkupTag.NBSP:
					if (flag)
					{
						break;
					}
					charBuffer[writeIndex].unicode = 160u;
					writeIndex++;
					i += 5;
					continue;
				case MarkupTag.ZWSP:
					if (flag)
					{
						break;
					}
					charBuffer[writeIndex].unicode = 8203u;
					writeIndex++;
					i += 5;
					continue;
				case MarkupTag.ZWJ:
					if (flag)
					{
						break;
					}
					charBuffer[writeIndex].unicode = 8205u;
					writeIndex++;
					i += 4;
					continue;
				case MarkupTag.SHY:
					if (flag)
					{
						break;
					}
					charBuffer[writeIndex].unicode = 173u;
					writeIndex++;
					i += 4;
					continue;
				case MarkupTag.STYLE:
				{
					if (flag || !ReplaceOpeningStyleTag(ref styleDefinition, i, out var srcOffset, ref charBuffer, ref writeIndex, ref textStyleStackDepth, ref textStyleStacks, ref generationSettings))
					{
						break;
					}
					i = srcOffset;
					continue;
				}
				case MarkupTag.SLASH_STYLE:
					if (flag)
					{
						break;
					}
					ReplaceClosingStyleTag(ref charBuffer, ref writeIndex, ref textStyleStackDepth, ref textStyleStacks, ref generationSettings);
					i += 7;
					continue;
				}
			}
			charBuffer[writeIndex].unicode = num2;
			writeIndex++;
		}
	}

	public static TextStyle GetStyle(TextGenerationSettings generationSetting, int hashCode)
	{
		TextStyle textStyle = null;
		TextStyleSheet styleSheet = generationSetting.styleSheet;
		if (styleSheet != null)
		{
			textStyle = styleSheet.GetStyle(hashCode);
			if (textStyle != null)
			{
				return textStyle;
			}
		}
		styleSheet = generationSetting.textSettings.defaultStyleSheet;
		if (styleSheet != null)
		{
			textStyle = styleSheet.GetStyle(hashCode);
		}
		return textStyle;
	}

	public static int GetStyleHashCode(ref uint[] text, int index, out int closeIndex)
	{
		int num = 0;
		closeIndex = 0;
		for (int i = index; i < text.Length; i++)
		{
			if (text[i] != 34)
			{
				if (text[i] == 62)
				{
					closeIndex = i;
					break;
				}
				num = ((num << 5) + num) ^ ToUpperASCIIFast((char)text[i]);
			}
		}
		return num;
	}

	public static int GetStyleHashCode(ref TextBackingContainer text, int index, out int closeIndex)
	{
		int num = 0;
		closeIndex = 0;
		for (int i = index; i < text.Capacity; i++)
		{
			if (text[i] != 34)
			{
				if (text[i] == 62)
				{
					closeIndex = i;
					break;
				}
				num = ((num << 5) + num) ^ ToUpperASCIIFast((char)text[i]);
			}
		}
		return num;
	}

	public static uint GetUTF16(uint[] text, int i)
	{
		uint num = 0u;
		num += HexToInt((char)text[i]) << 12;
		num += HexToInt((char)text[i + 1]) << 8;
		num += HexToInt((char)text[i + 2]) << 4;
		return num + HexToInt((char)text[i + 3]);
	}

	public static uint GetUTF16(TextBackingContainer text, int i)
	{
		uint num = 0u;
		num += HexToInt((char)text[i]) << 12;
		num += HexToInt((char)text[i + 1]) << 8;
		num += HexToInt((char)text[i + 2]) << 4;
		return num + HexToInt((char)text[i + 3]);
	}

	public static uint GetUTF32(uint[] text, int i)
	{
		uint num = 0u;
		num += HexToInt((char)text[i]) << 28;
		num += HexToInt((char)text[i + 1]) << 24;
		num += HexToInt((char)text[i + 2]) << 20;
		num += HexToInt((char)text[i + 3]) << 16;
		num += HexToInt((char)text[i + 4]) << 12;
		num += HexToInt((char)text[i + 5]) << 8;
		num += HexToInt((char)text[i + 6]) << 4;
		return num + HexToInt((char)text[i + 7]);
	}

	public static uint GetUTF32(TextBackingContainer text, int i)
	{
		uint num = 0u;
		num += HexToInt((char)text[i]) << 28;
		num += HexToInt((char)text[i + 1]) << 24;
		num += HexToInt((char)text[i + 2]) << 20;
		num += HexToInt((char)text[i + 3]) << 16;
		num += HexToInt((char)text[i + 4]) << 12;
		num += HexToInt((char)text[i + 5]) << 8;
		num += HexToInt((char)text[i + 6]) << 4;
		return num + HexToInt((char)text[i + 7]);
	}

	private static int GetTagHashCode(ref int[] text, int index, out int closeIndex)
	{
		int num = 0;
		closeIndex = 0;
		for (int i = index; i < text.Length; i++)
		{
			if (text[i] != 34)
			{
				if (text[i] == 62)
				{
					closeIndex = i;
					break;
				}
				num = ((num << 5) + num) ^ (int)TextUtilities.ToUpperASCIIFast((ushort)text[i]);
			}
		}
		return num;
	}

	private static int GetTagHashCode(ref string text, int index, out int closeIndex)
	{
		int num = 0;
		closeIndex = 0;
		for (int i = index; i < text.Length; i++)
		{
			if (text[i] != '"')
			{
				if (text[i] == '>')
				{
					closeIndex = i;
					break;
				}
				num = ((num << 5) + num) ^ (int)TextUtilities.ToUpperASCIIFast(text[i]);
			}
		}
		return num;
	}

	public static void FillCharacterVertexBuffers(int i, bool convertToLinearSpace, TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		int materialReferenceIndex = textInfo.textElementInfo[i].materialReferenceIndex;
		int vertexCount = textInfo.meshInfo[materialReferenceIndex].vertexCount;
		if (vertexCount >= textInfo.meshInfo[materialReferenceIndex].vertices.Length)
		{
			textInfo.meshInfo[materialReferenceIndex].ResizeMeshInfo(Mathf.NextPowerOfTwo((vertexCount + 4) / 4));
		}
		TextElementInfo[] textElementInfo = textInfo.textElementInfo;
		textInfo.textElementInfo[i].vertexIndex = vertexCount;
		if (generationSettings.inverseYAxis)
		{
			Vector3 vector = default(Vector3);
			vector.x = 0f;
			vector.y = generationSettings.screenRect.y + generationSettings.screenRect.height;
			vector.z = 0f;
			Vector3 position = textElementInfo[i].vertexBottomLeft.position;
			position.y *= -1f;
			textInfo.meshInfo[materialReferenceIndex].vertices[vertexCount] = position + vector;
			position = textElementInfo[i].vertexTopLeft.position;
			position.y *= -1f;
			textInfo.meshInfo[materialReferenceIndex].vertices[1 + vertexCount] = position + vector;
			position = textElementInfo[i].vertexTopRight.position;
			position.y *= -1f;
			textInfo.meshInfo[materialReferenceIndex].vertices[2 + vertexCount] = position + vector;
			position = textElementInfo[i].vertexBottomRight.position;
			position.y *= -1f;
			textInfo.meshInfo[materialReferenceIndex].vertices[3 + vertexCount] = position + vector;
		}
		else
		{
			textInfo.meshInfo[materialReferenceIndex].vertices[vertexCount] = textElementInfo[i].vertexBottomLeft.position;
			textInfo.meshInfo[materialReferenceIndex].vertices[1 + vertexCount] = textElementInfo[i].vertexTopLeft.position;
			textInfo.meshInfo[materialReferenceIndex].vertices[2 + vertexCount] = textElementInfo[i].vertexTopRight.position;
			textInfo.meshInfo[materialReferenceIndex].vertices[3 + vertexCount] = textElementInfo[i].vertexBottomRight.position;
		}
		textInfo.meshInfo[materialReferenceIndex].uvs0[vertexCount] = textElementInfo[i].vertexBottomLeft.uv;
		textInfo.meshInfo[materialReferenceIndex].uvs0[1 + vertexCount] = textElementInfo[i].vertexTopLeft.uv;
		textInfo.meshInfo[materialReferenceIndex].uvs0[2 + vertexCount] = textElementInfo[i].vertexTopRight.uv;
		textInfo.meshInfo[materialReferenceIndex].uvs0[3 + vertexCount] = textElementInfo[i].vertexBottomRight.uv;
		textInfo.meshInfo[materialReferenceIndex].uvs2[vertexCount] = textElementInfo[i].vertexBottomLeft.uv2;
		textInfo.meshInfo[materialReferenceIndex].uvs2[1 + vertexCount] = textElementInfo[i].vertexTopLeft.uv2;
		textInfo.meshInfo[materialReferenceIndex].uvs2[2 + vertexCount] = textElementInfo[i].vertexTopRight.uv2;
		textInfo.meshInfo[materialReferenceIndex].uvs2[3 + vertexCount] = textElementInfo[i].vertexBottomRight.uv2;
		textInfo.meshInfo[materialReferenceIndex].colors32[vertexCount] = (convertToLinearSpace ? GammaToLinear(textElementInfo[i].vertexBottomLeft.color) : textElementInfo[i].vertexBottomLeft.color);
		textInfo.meshInfo[materialReferenceIndex].colors32[1 + vertexCount] = (convertToLinearSpace ? GammaToLinear(textElementInfo[i].vertexTopLeft.color) : textElementInfo[i].vertexTopLeft.color);
		textInfo.meshInfo[materialReferenceIndex].colors32[2 + vertexCount] = (convertToLinearSpace ? GammaToLinear(textElementInfo[i].vertexTopRight.color) : textElementInfo[i].vertexTopRight.color);
		textInfo.meshInfo[materialReferenceIndex].colors32[3 + vertexCount] = (convertToLinearSpace ? GammaToLinear(textElementInfo[i].vertexBottomRight.color) : textElementInfo[i].vertexBottomRight.color);
		textInfo.meshInfo[materialReferenceIndex].vertexCount = vertexCount + 4;
	}

	public static void FillSpriteVertexBuffers(int i, bool convertToLinearSpace, TextGenerationSettings generationSettings, TextInfo textInfo)
	{
		int materialReferenceIndex = textInfo.textElementInfo[i].materialReferenceIndex;
		int vertexCount = textInfo.meshInfo[materialReferenceIndex].vertexCount;
		TextElementInfo[] textElementInfo = textInfo.textElementInfo;
		textInfo.textElementInfo[i].vertexIndex = vertexCount;
		if (generationSettings.inverseYAxis)
		{
			Vector3 vector = default(Vector3);
			vector.x = 0f;
			vector.y = generationSettings.screenRect.y + generationSettings.screenRect.height;
			vector.z = 0f;
			Vector3 position = textElementInfo[i].vertexBottomLeft.position;
			position.y *= -1f;
			textInfo.meshInfo[materialReferenceIndex].vertices[vertexCount] = position + vector;
			position = textElementInfo[i].vertexTopLeft.position;
			position.y *= -1f;
			textInfo.meshInfo[materialReferenceIndex].vertices[1 + vertexCount] = position + vector;
			position = textElementInfo[i].vertexTopRight.position;
			position.y *= -1f;
			textInfo.meshInfo[materialReferenceIndex].vertices[2 + vertexCount] = position + vector;
			position = textElementInfo[i].vertexBottomRight.position;
			position.y *= -1f;
			textInfo.meshInfo[materialReferenceIndex].vertices[3 + vertexCount] = position + vector;
		}
		else
		{
			textInfo.meshInfo[materialReferenceIndex].vertices[vertexCount] = textElementInfo[i].vertexBottomLeft.position;
			textInfo.meshInfo[materialReferenceIndex].vertices[1 + vertexCount] = textElementInfo[i].vertexTopLeft.position;
			textInfo.meshInfo[materialReferenceIndex].vertices[2 + vertexCount] = textElementInfo[i].vertexTopRight.position;
			textInfo.meshInfo[materialReferenceIndex].vertices[3 + vertexCount] = textElementInfo[i].vertexBottomRight.position;
		}
		textInfo.meshInfo[materialReferenceIndex].uvs0[vertexCount] = textElementInfo[i].vertexBottomLeft.uv;
		textInfo.meshInfo[materialReferenceIndex].uvs0[1 + vertexCount] = textElementInfo[i].vertexTopLeft.uv;
		textInfo.meshInfo[materialReferenceIndex].uvs0[2 + vertexCount] = textElementInfo[i].vertexTopRight.uv;
		textInfo.meshInfo[materialReferenceIndex].uvs0[3 + vertexCount] = textElementInfo[i].vertexBottomRight.uv;
		textInfo.meshInfo[materialReferenceIndex].uvs2[vertexCount] = textElementInfo[i].vertexBottomLeft.uv2;
		textInfo.meshInfo[materialReferenceIndex].uvs2[1 + vertexCount] = textElementInfo[i].vertexTopLeft.uv2;
		textInfo.meshInfo[materialReferenceIndex].uvs2[2 + vertexCount] = textElementInfo[i].vertexTopRight.uv2;
		textInfo.meshInfo[materialReferenceIndex].uvs2[3 + vertexCount] = textElementInfo[i].vertexBottomRight.uv2;
		textInfo.meshInfo[materialReferenceIndex].colors32[vertexCount] = (convertToLinearSpace ? GammaToLinear(textElementInfo[i].vertexBottomLeft.color) : textElementInfo[i].vertexBottomLeft.color);
		textInfo.meshInfo[materialReferenceIndex].colors32[1 + vertexCount] = (convertToLinearSpace ? GammaToLinear(textElementInfo[i].vertexTopLeft.color) : textElementInfo[i].vertexTopLeft.color);
		textInfo.meshInfo[materialReferenceIndex].colors32[2 + vertexCount] = (convertToLinearSpace ? GammaToLinear(textElementInfo[i].vertexTopRight.color) : textElementInfo[i].vertexTopRight.color);
		textInfo.meshInfo[materialReferenceIndex].colors32[3 + vertexCount] = (convertToLinearSpace ? GammaToLinear(textElementInfo[i].vertexBottomRight.color) : textElementInfo[i].vertexBottomRight.color);
		textInfo.meshInfo[materialReferenceIndex].vertexCount = vertexCount + 4;
	}

	public static void AdjustLineOffset(int startIndex, int endIndex, float offset, TextInfo textInfo)
	{
		Vector3 vector = new Vector3(0f, offset, 0f);
		for (int i = startIndex; i <= endIndex; i++)
		{
			textInfo.textElementInfo[i].bottomLeft -= vector;
			textInfo.textElementInfo[i].topLeft -= vector;
			textInfo.textElementInfo[i].topRight -= vector;
			textInfo.textElementInfo[i].bottomRight -= vector;
			textInfo.textElementInfo[i].ascender -= vector.y;
			textInfo.textElementInfo[i].baseLine -= vector.y;
			textInfo.textElementInfo[i].descender -= vector.y;
			if (textInfo.textElementInfo[i].isVisible)
			{
				textInfo.textElementInfo[i].vertexBottomLeft.position -= vector;
				textInfo.textElementInfo[i].vertexTopLeft.position -= vector;
				textInfo.textElementInfo[i].vertexTopRight.position -= vector;
				textInfo.textElementInfo[i].vertexBottomRight.position -= vector;
			}
		}
	}

	public static void ResizeLineExtents(int size, TextInfo textInfo)
	{
		size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size + 1));
		LineInfo[] array = new LineInfo[size];
		for (int i = 0; i < size; i++)
		{
			if (i < textInfo.lineInfo.Length)
			{
				array[i] = textInfo.lineInfo[i];
				continue;
			}
			array[i].lineExtents.min = largePositiveVector2;
			array[i].lineExtents.max = largeNegativeVector2;
			array[i].ascender = -32767f;
			array[i].descender = 32767f;
		}
		textInfo.lineInfo = array;
	}

	public static FontStyles LegacyStyleToNewStyle(FontStyle fontStyle)
	{
		return fontStyle switch
		{
			FontStyle.Bold => FontStyles.Bold, 
			FontStyle.Italic => FontStyles.Italic, 
			FontStyle.BoldAndItalic => FontStyles.Bold | FontStyles.Italic, 
			_ => FontStyles.Normal, 
		};
	}

	public static TextAlignment LegacyAlignmentToNewAlignment(TextAnchor anchor)
	{
		return anchor switch
		{
			TextAnchor.UpperLeft => TextAlignment.TopLeft, 
			TextAnchor.UpperCenter => TextAlignment.TopCenter, 
			TextAnchor.UpperRight => TextAlignment.TopRight, 
			TextAnchor.MiddleLeft => TextAlignment.MiddleLeft, 
			TextAnchor.MiddleCenter => TextAlignment.MiddleCenter, 
			TextAnchor.MiddleRight => TextAlignment.MiddleRight, 
			TextAnchor.LowerLeft => TextAlignment.BottomLeft, 
			TextAnchor.LowerCenter => TextAlignment.BottomCenter, 
			TextAnchor.LowerRight => TextAlignment.BottomRight, 
			_ => TextAlignment.TopLeft, 
		};
	}

	public static uint ConvertToUTF32(uint highSurrogate, uint lowSurrogate)
	{
		return (highSurrogate - 55296) * 1024 + (lowSurrogate - 56320 + 65536);
	}

	public static int GetMarkupTagHashCode(TextBackingContainer styleDefinition, int readIndex)
	{
		int num = 0;
		int num2 = readIndex + 16;
		int capacity = styleDefinition.Capacity;
		while (readIndex < num2 && readIndex < capacity)
		{
			uint num3 = styleDefinition[readIndex];
			if (num3 == 62 || num3 == 61 || num3 == 32)
			{
				return num;
			}
			num = ((num << 5) + num) ^ (int)ToUpperASCIIFast(num3);
			readIndex++;
		}
		return num;
	}

	public static int GetMarkupTagHashCode(uint[] styleDefinition, int readIndex)
	{
		int num = 0;
		int num2 = readIndex + 16;
		int num3 = styleDefinition.Length;
		while (readIndex < num2 && readIndex < num3)
		{
			uint num4 = styleDefinition[readIndex];
			if (num4 == 62 || num4 == 61 || num4 == 32)
			{
				return num;
			}
			num = ((num << 5) + num) ^ (int)ToUpperASCIIFast(num4);
			readIndex++;
		}
		return num;
	}

	public static char ToUpperASCIIFast(char c)
	{
		if (c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1)
		{
			return c;
		}
		return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[c];
	}

	public static uint ToUpperASCIIFast(uint c)
	{
		if (c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1)
		{
			return c;
		}
		return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
	}

	public static char ToUpperFast(char c)
	{
		if (c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1)
		{
			return c;
		}
		return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[c];
	}

	public static int GetAttributeParameters(char[] chars, int startIndex, int length, ref float[] parameters)
	{
		int lastIndex = startIndex;
		int num = 0;
		while (lastIndex < startIndex + length)
		{
			parameters[num] = ConvertToFloat(chars, startIndex, length, out lastIndex);
			length -= lastIndex - startIndex + 1;
			startIndex = lastIndex + 1;
			num++;
		}
		return num;
	}

	public static bool IsBitmapRendering(GlyphRenderMode glyphRenderMode)
	{
		return glyphRenderMode == GlyphRenderMode.RASTER || glyphRenderMode == GlyphRenderMode.RASTER_HINTED || glyphRenderMode == GlyphRenderMode.SMOOTH || glyphRenderMode == GlyphRenderMode.SMOOTH_HINTED;
	}

	public static bool IsBaseGlyph(uint c)
	{
		return (c < 768 || c > 879) && (c < 6832 || c > 6911) && (c < 7616 || c > 7679) && (c < 8400 || c > 8447) && (c < 65056 || c > 65071) && c != 3633 && (c < 3636 || c > 3642) && (c < 3655 || c > 3662) && (c < 1425 || c > 1469) && c != 1471 && (c < 1473 || c > 1474) && (c < 1476 || c > 1477) && c != 1479 && (c < 1552 || c > 1562) && (c < 1611 || c > 1631) && c != 1648 && (c < 1750 || c > 1756) && (c < 1759 || c > 1764) && (c < 1767 || c > 1768) && (c < 1770 || c > 1773) && (c < 2259 || c > 2273) && (c < 2275 || c > 2303) && (c < 64434 || c > 64449);
	}

	public static Color MinAlpha(this Color c1, Color c2)
	{
		float a = ((c1.a < c2.a) ? c1.a : c2.a);
		return new Color(c1.r, c1.g, c1.b, a);
	}

	internal static Color32 GammaToLinear(Color32 c)
	{
		return new Color32(GammaToLinear(c.r), GammaToLinear(c.g), GammaToLinear(c.b), c.a);
	}

	private static byte GammaToLinear(byte value)
	{
		float num = (float)(int)value / 255f;
		if (num <= 0.04045f)
		{
			return (byte)(num / 12.92f * 255f);
		}
		if (num < 1f)
		{
			return (byte)(Mathf.Pow((num + 0.055f) / 1.055f, 2.4f) * 255f);
		}
		if (num == 1f)
		{
			return byte.MaxValue;
		}
		return (byte)(Mathf.Pow(num, 2.2f) * 255f);
	}

	public static bool IsValidUTF16(TextBackingContainer text, int index)
	{
		for (int i = 0; i < 4; i++)
		{
			uint num = text[index + i];
			if ((num < 48 || num > 57) && (num < 97 || num > 102) && (num < 65 || num > 70))
			{
				return false;
			}
		}
		return true;
	}

	public static bool IsValidUTF32(TextBackingContainer text, int index)
	{
		for (int i = 0; i < 8; i++)
		{
			uint num = text[index + i];
			if ((num < 48 || num > 57) && (num < 97 || num > 102) && (num < 65 || num > 70))
			{
				return false;
			}
		}
		return true;
	}

	internal static bool IsEmoji(uint c)
	{
		return c == 8205 || c == 8252 || c == 8265 || c == 8419 || c == 8482 || c == 8505 || (c >= 8596 && c <= 8601) || (c >= 8617 && c <= 8618) || (c >= 8986 && c <= 8987) || c == 9000 || c == 9096 || c == 9167 || (c >= 9193 && c <= 9203) || (c >= 9208 && c <= 9210) || c == 9410 || (c >= 9642 && c <= 9643) || c == 9654 || c == 9664 || (c >= 9723 && c <= 9726) || (c >= 9728 && c <= 9733) || (c >= 9735 && c <= 9746) || (c >= 9748 && c <= 9861) || (c >= 9872 && c <= 9989) || (c >= 9992 && c <= 10002) || c == 10004 || c == 10006 || c == 10013 || c == 10017 || c == 10024 || (c >= 10035 && c <= 10036) || c == 10052 || c == 10055 || c == 10060 || c == 10062 || (c >= 10067 && c <= 10069) || c == 10071 || (c >= 10083 && c <= 10087) || (c >= 10133 && c <= 10135) || c == 10145 || c == 10160 || c == 10175 || (c >= 10548 && c <= 10549) || (c >= 11013 && c <= 11015) || (c >= 11035 && c <= 11036) || c == 11088 || c == 11093 || c == 12336 || c == 12349 || c == 12951 || c == 12953 || c == 65039 || (c >= 126976 && c <= 127231) || (c >= 127245 && c <= 127247) || c == 127279 || (c >= 127340 && c <= 127345) || (c >= 127358 && c <= 127359) || c == 127374 || (c >= 127377 && c <= 127386) || (c >= 127405 && c <= 127487) || (c >= 127489 && c <= 127503) || c == 127514 || c == 127535 || (c >= 127538 && c <= 127546) || (c >= 127548 && c <= 127551) || (c >= 127561 && c <= 128317) || (c >= 128326 && c <= 128591) || (c >= 128640 && c <= 128767) || (c >= 128884 && c <= 128895) || (c >= 128981 && c <= 129023) || (c >= 129036 && c <= 129039) || (c >= 129096 && c <= 129103) || (c >= 129114 && c <= 129119) || (c >= 129160 && c <= 129167) || (c >= 129198 && c <= 129279) || (c >= 129292 && c <= 129338) || (c >= 129340 && c <= 129349) || (c >= 129351 && c <= 129791) || (c >= 130048 && c <= 131069) || (c >= 917536 && c <= 917631);
	}

	internal static bool IsHangul(uint c)
	{
		return (c >= 4352 && c <= 4607) || (c >= 43360 && c <= 43391) || (c >= 55216 && c <= 55295) || (c >= 12592 && c <= 12687) || (c >= 65440 && c <= 65500) || (c >= 44032 && c <= 55215);
	}

	internal static bool IsCJK(uint c)
	{
		return (c >= 12288 && c <= 12351) || (c >= 94176 && c <= 5887) || (c >= 12544 && c <= 12591) || (c >= 12704 && c <= 12735) || (c >= 19968 && c <= 40959) || (c >= 13312 && c <= 19903) || (c >= 131072 && c <= 173791) || (c >= 173824 && c <= 177983) || (c >= 177984 && c <= 178207) || (c >= 178208 && c <= 183983) || (c >= 183984 && c <= 191456) || (c >= 196608 && c <= 201546) || (c >= 63744 && c <= 64255) || (c >= 194560 && c <= 195103) || (c >= 12032 && c <= 12255) || (c >= 11904 && c <= 12031) || (c >= 12736 && c <= 12783) || (c >= 12272 && c <= 12287) || (c >= 12352 && c <= 12447) || (c >= 110848 && c <= 110895) || (c >= 110576 && c <= 110591) || (c >= 110592 && c <= 110847) || (c >= 110896 && c <= 110959) || (c >= 12688 && c <= 12703) || (c >= 12448 && c <= 12543) || (c >= 12784 && c <= 12799) || (c >= 65381 && c <= 65439);
	}
}
internal class TextHandle
{
	private Vector2 m_PreferredSize;

	private TextInfo m_TextInfo;

	private static TextInfo m_LayoutTextInfo;

	private int m_PreviousGenerationSettingsHash;

	protected TextGenerationSettings textGenerationSettings;

	protected static TextGenerationSettings s_LayoutSettings = new TextGenerationSettings();

	private bool isDirty;

	internal TextInfo textInfo
	{
		get
		{
			if (m_TextInfo == null)
			{
				m_TextInfo = new TextInfo();
			}
			return m_TextInfo;
		}
	}

	internal static TextInfo layoutTextInfo
	{
		get
		{
			if (m_LayoutTextInfo == null)
			{
				m_LayoutTextInfo = new TextInfo();
			}
			return m_LayoutTextInfo;
		}
	}

	public TextHandle()
	{
		textGenerationSettings = new TextGenerationSettings();
	}

	internal bool IsTextInfoAllocated()
	{
		return m_TextInfo != null;
	}

	public void SetDirty()
	{
		isDirty = true;
	}

	public bool IsDirty()
	{
		int hashCode = textGenerationSettings.GetHashCode();
		if (m_PreviousGenerationSettingsHash == hashCode && !isDirty)
		{
			return false;
		}
		m_PreviousGenerationSettingsHash = hashCode;
		isDirty = false;
		return true;
	}

	public Vector2 GetCursorPositionFromStringIndexUsingCharacterHeight(int index, bool inverseYAxis = true)
	{
		if (textGenerationSettings == null)
		{
			return Vector2.zero;
		}
		Rect screenRect = textGenerationSettings.screenRect;
		Vector2 position = screenRect.position;
		if (textInfo.characterCount == 0)
		{
			return position;
		}
		int num = ((index >= textInfo.characterCount) ? (textInfo.characterCount - 1) : index);
		TextElementInfo textElementInfo = textInfo.textElementInfo[num];
		float descender = textElementInfo.descender;
		float x = ((index >= textInfo.characterCount) ? textElementInfo.xAdvance : textElementInfo.origin);
		return position + (inverseYAxis ? new Vector2(x, screenRect.height - descender) : new Vector2(x, descender));
	}

	public Vector2 GetCursorPositionFromStringIndexUsingLineHeight(int index, bool useXAdvance = false, bool inverseYAxis = true)
	{
		if (textGenerationSettings == null)
		{
			return Vector2.zero;
		}
		Rect screenRect = textGenerationSettings.screenRect;
		Vector2 position = screenRect.position;
		if (textInfo.characterCount == 0)
		{
			return position;
		}
		if (index >= textInfo.characterCount)
		{
			index = textInfo.characterCount - 1;
		}
		TextElementInfo textElementInfo = textInfo.textElementInfo[index];
		LineInfo lineInfo = textInfo.lineInfo[textElementInfo.lineNumber];
		if (index >= textInfo.characterCount - 1 || useXAdvance)
		{
			return position + (inverseYAxis ? new Vector2(textElementInfo.xAdvance, screenRect.height - lineInfo.descender) : new Vector2(textElementInfo.xAdvance, lineInfo.descender));
		}
		return position + (inverseYAxis ? new Vector2(textElementInfo.origin, screenRect.height - lineInfo.descender) : new Vector2(textElementInfo.origin, lineInfo.descender));
	}

	public int GetCursorIndexFromPosition(Vector2 position, bool inverseYAxis = true)
	{
		if (textGenerationSettings == null)
		{
			return 0;
		}
		if (inverseYAxis)
		{
			position.y = textGenerationSettings.screenRect.height - position.y;
		}
		int line = 0;
		if (textInfo.lineCount > 1)
		{
			line = FindNearestLine(position);
		}
		int num = FindNearestCharacterOnLine(position, line, visibleOnly: false);
		TextElementInfo textElementInfo = textInfo.textElementInfo[num];
		Vector3 bottomLeft = textElementInfo.bottomLeft;
		Vector3 topRight = textElementInfo.topRight;
		float num2 = (position.x - bottomLeft.x) / (topRight.x - bottomLeft.x);
		return (num2 < 0.5f || textElementInfo.character == '\n') ? num : (num + 1);
	}

	public int LineDownCharacterPosition(int originalPos)
	{
		if (originalPos >= textInfo.characterCount)
		{
			return textInfo.characterCount - 1;
		}
		TextElementInfo textElementInfo = textInfo.textElementInfo[originalPos];
		int lineNumber = textElementInfo.lineNumber;
		if (lineNumber + 1 >= textInfo.lineCount)
		{
			return textInfo.characterCount - 1;
		}
		int lastCharacterIndex = textInfo.lineInfo[lineNumber + 1].lastCharacterIndex;
		int num = -1;
		float num2 = float.PositiveInfinity;
		float num3 = 0f;
		for (int i = textInfo.lineInfo[lineNumber + 1].firstCharacterIndex; i < lastCharacterIndex; i++)
		{
			TextElementInfo textElementInfo2 = textInfo.textElementInfo[i];
			float num4 = textElementInfo.origin - textElementInfo2.origin;
			float num5 = num4 / (textElementInfo2.xAdvance - textElementInfo2.origin);
			if (num5 >= 0f && num5 <= 1f)
			{
				if (num5 < 0.5f)
				{
					return i;
				}
				return i + 1;
			}
			num4 = Mathf.Abs(num4);
			if (num4 < num2)
			{
				num = i;
				num2 = num4;
				num3 = num5;
			}
		}
		if (num == -1)
		{
			return lastCharacterIndex;
		}
		if (num3 < 0.5f)
		{
			return num;
		}
		return num + 1;
	}

	public int LineUpCharacterPosition(int originalPos)
	{
		if (originalPos >= textInfo.characterCount)
		{
			originalPos--;
		}
		TextElementInfo textElementInfo = textInfo.textElementInfo[originalPos];
		int lineNumber = textElementInfo.lineNumber;
		if (lineNumber - 1 < 0)
		{
			return 0;
		}
		int num = textInfo.lineInfo[lineNumber].firstCharacterIndex - 1;
		int num2 = -1;
		float num3 = float.PositiveInfinity;
		float num4 = 0f;
		for (int i = textInfo.lineInfo[lineNumber - 1].firstCharacterIndex; i < num; i++)
		{
			TextElementInfo textElementInfo2 = textInfo.textElementInfo[i];
			float num5 = textElementInfo.origin - textElementInfo2.origin;
			float num6 = num5 / (textElementInfo2.xAdvance - textElementInfo2.origin);
			if (num6 >= 0f && num6 <= 1f)
			{
				if (num6 < 0.5f)
				{
					return i;
				}
				return i + 1;
			}
			num5 = Mathf.Abs(num5);
			if (num5 < num3)
			{
				num2 = i;
				num3 = num5;
				num4 = num6;
			}
		}
		if (num2 == -1)
		{
			return num;
		}
		if (num4 < 0.5f)
		{
			return num2;
		}
		return num2 + 1;
	}

	public int FindWordIndex(int cursorIndex)
	{
		for (int i = 0; i < textInfo.wordCount; i++)
		{
			WordInfo wordInfo = textInfo.wordInfo[i];
			if (wordInfo.firstCharacterIndex <= cursorIndex && wordInfo.lastCharacterIndex >= cursorIndex)
			{
				return i;
			}
		}
		return -1;
	}

	public int FindNearestLine(Vector2 position)
	{
		float num = float.PositiveInfinity;
		int result = -1;
		for (int i = 0; i < textInfo.lineCount; i++)
		{
			LineInfo lineInfo = textInfo.lineInfo[i];
			float ascender = lineInfo.ascender;
			float descender = lineInfo.descender;
			if (ascender > position.y && descender < position.y)
			{
				return i;
			}
			float a = Mathf.Abs(ascender - position.y);
			float b = Mathf.Abs(descender - position.y);
			float num2 = Mathf.Min(a, b);
			if (num2 < num)
			{
				num = num2;
				result = i;
			}
		}
		return result;
	}

	public int FindNearestCharacterOnLine(Vector2 position, int line, bool visibleOnly)
	{
		int firstCharacterIndex = textInfo.lineInfo[line].firstCharacterIndex;
		int lastCharacterIndex = textInfo.lineInfo[line].lastCharacterIndex;
		float num = float.PositiveInfinity;
		int result = lastCharacterIndex;
		for (int i = firstCharacterIndex; i <= lastCharacterIndex; i++)
		{
			TextElementInfo textElementInfo = textInfo.textElementInfo[i];
			if ((!visibleOnly || textElementInfo.isVisible) && textElementInfo.character != '\r' && textElementInfo.character != '\n')
			{
				Vector3 bottomLeft = textElementInfo.bottomLeft;
				Vector3 vector = new Vector3(textElementInfo.bottomLeft.x, textElementInfo.topRight.y, 0f);
				Vector3 topRight = textElementInfo.topRight;
				Vector3 vector2 = new Vector3(textElementInfo.topRight.x, textElementInfo.bottomLeft.y, 0f);
				if (PointIntersectRectangle(position, bottomLeft, vector, topRight, vector2))
				{
					result = i;
					break;
				}
				float num2 = DistanceToLine(bottomLeft, vector, position);
				float num3 = DistanceToLine(vector, topRight, position);
				float num4 = DistanceToLine(topRight, vector2, position);
				float num5 = DistanceToLine(vector2, bottomLeft, position);
				float num6 = ((num2 < num3) ? num2 : num3);
				num6 = ((num6 < num4) ? num6 : num4);
				num6 = ((num6 < num5) ? num6 : num5);
				if (num > num6)
				{
					num = num6;
					result = i;
				}
			}
		}
		return result;
	}

	public int FindIntersectingLink(Vector3 position, bool inverseYAxis = true)
	{
		if (inverseYAxis)
		{
			position.y = textGenerationSettings.screenRect.height - position.y;
		}
		for (int i = 0; i < textInfo.linkCount; i++)
		{
			LinkInfo linkInfo = textInfo.linkInfo[i];
			bool flag = false;
			Vector3 a = Vector3.zero;
			Vector3 b = Vector3.zero;
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			for (int j = 0; j < linkInfo.linkTextLength; j++)
			{
				int num = linkInfo.linkTextfirstCharacterIndex + j;
				TextElementInfo textElementInfo = textInfo.textElementInfo[num];
				int lineNumber = textElementInfo.lineNumber;
				if (!flag)
				{
					flag = true;
					a = new Vector3(textElementInfo.bottomLeft.x, textElementInfo.descender, 0f);
					b = new Vector3(textElementInfo.bottomLeft.x, textElementInfo.ascender, 0f);
					if (linkInfo.linkTextLength == 1)
					{
						flag = false;
						if (PointIntersectRectangle(d: new Vector3(textElementInfo.topRight.x, textElementInfo.descender, 0f), c: new Vector3(textElementInfo.topRight.x, textElementInfo.ascender, 0f), m: position, a: a, b: b))
						{
							return i;
						}
					}
				}
				if (flag && j == linkInfo.linkTextLength - 1)
				{
					flag = false;
					if (PointIntersectRectangle(d: new Vector3(textElementInfo.topRight.x, textElementInfo.descender, 0f), c: new Vector3(textElementInfo.topRight.x, textElementInfo.ascender, 0f), m: position, a: a, b: b))
					{
						return i;
					}
				}
				else if (flag && lineNumber != textInfo.textElementInfo[num + 1].lineNumber)
				{
					flag = false;
					if (PointIntersectRectangle(d: new Vector3(textElementInfo.topRight.x, textElementInfo.descender, 0f), c: new Vector3(textElementInfo.topRight.x, textElementInfo.ascender, 0f), m: position, a: a, b: b))
					{
						return i;
					}
				}
			}
		}
		return -1;
	}

	private static bool PointIntersectRectangle(Vector3 m, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		Vector3 vector = b - a;
		Vector3 rhs = m - a;
		Vector3 vector2 = c - b;
		Vector3 rhs2 = m - b;
		float num = Vector3.Dot(vector, rhs);
		float num2 = Vector3.Dot(vector2, rhs2);
		return 0f <= num && num <= Vector3.Dot(vector, vector) && 0f <= num2 && num2 <= Vector3.Dot(vector2, vector2);
	}

	private static float DistanceToLine(Vector3 a, Vector3 b, Vector3 point)
	{
		Vector3 vector = b - a;
		Vector3 vector2 = a - point;
		float num = Vector3.Dot(vector, vector2);
		if (num > 0f)
		{
			return Vector3.Dot(vector2, vector2);
		}
		Vector3 vector3 = point - b;
		if (Vector3.Dot(vector, vector3) > 0f)
		{
			return Vector3.Dot(vector3, vector3);
		}
		Vector3 vector4 = vector2 - vector * (num / Vector3.Dot(vector, vector));
		return Vector3.Dot(vector4, vector4);
	}

	public int GetLineNumber(int index)
	{
		if (index <= 0)
		{
			index = 0;
		}
		else if (index >= textInfo.characterCount)
		{
			index = Mathf.Max(0, textInfo.characterCount - 1);
		}
		return textInfo.textElementInfo[index].lineNumber;
	}

	public float GetLineHeight(int lineNumber)
	{
		if (lineNumber <= 0)
		{
			lineNumber = 0;
		}
		else if (lineNumber >= textInfo.lineCount)
		{
			lineNumber = Mathf.Max(0, textInfo.lineCount - 1);
		}
		return textInfo.lineInfo[lineNumber].lineHeight;
	}

	public float GetLineHeightFromCharacterIndex(int index)
	{
		if (index <= 0)
		{
			index = 0;
		}
		else if (index >= textInfo.characterCount)
		{
			index = Mathf.Max(0, textInfo.characterCount - 1);
		}
		return GetLineHeight(textInfo.textElementInfo[index].lineNumber);
	}

	public float GetCharacterHeightFromIndex(int index)
	{
		if (index <= 0)
		{
			index = 0;
		}
		else if (index >= textInfo.characterCount)
		{
			index = Mathf.Max(0, textInfo.characterCount - 1);
		}
		TextElementInfo textElementInfo = textInfo.textElementInfo[index];
		return textElementInfo.ascender - textElementInfo.descender;
	}

	public bool IsElided()
	{
		if (textInfo == null)
		{
			return false;
		}
		if (textInfo.characterCount == 0)
		{
			return true;
		}
		return TextGenerator.isTextTruncated;
	}

	public string Substring(int startIndex, int length)
	{
		if (startIndex < 0 || startIndex + length > textInfo.characterCount)
		{
			throw new ArgumentOutOfRangeException();
		}
		StringBuilder stringBuilder = new StringBuilder(length);
		for (int i = startIndex; i < startIndex + length; i++)
		{
			stringBuilder.Append(textInfo.textElementInfo[i].character);
		}
		return stringBuilder.ToString();
	}

	public int IndexOf(char value, int startIndex)
	{
		if (startIndex < 0 || startIndex >= textInfo.characterCount)
		{
			throw new ArgumentOutOfRangeException();
		}
		for (int i = startIndex; i < textInfo.characterCount; i++)
		{
			if (textInfo.textElementInfo[i].character == value)
			{
				return i;
			}
		}
		return -1;
	}

	public int LastIndexOf(char value, int startIndex)
	{
		if (startIndex < 0 || startIndex >= textInfo.characterCount)
		{
			throw new ArgumentOutOfRangeException();
		}
		for (int num = startIndex; num >= 0; num--)
		{
			if (textInfo.textElementInfo[num].character == value)
			{
				return num;
			}
		}
		return -1;
	}

	protected float ComputeTextWidth(TextGenerationSettings tgs)
	{
		UpdatePreferredValues(tgs);
		return m_PreferredSize.x;
	}

	protected float ComputeTextHeight(TextGenerationSettings tgs)
	{
		UpdatePreferredValues(tgs);
		return m_PreferredSize.y;
	}

	protected void UpdatePreferredValues(TextGenerationSettings tgs)
	{
		m_PreferredSize = TextGenerator.GetPreferredValues(tgs, layoutTextInfo);
	}

	internal TextInfo Update(string newText)
	{
		textGenerationSettings.text = newText;
		return Update(textGenerationSettings);
	}

	protected TextInfo Update(TextGenerationSettings tgs)
	{
		if (!IsDirty())
		{
			return textInfo;
		}
		textInfo.isDirty = true;
		TextGenerator.GenerateText(tgs, textInfo);
		textGenerationSettings = tgs;
		return textInfo;
	}
}
internal struct PageInfo
{
	public int firstCharacterIndex;

	public int lastCharacterIndex;

	public float ascender;

	public float baseLine;

	public float descender;
}
internal struct WordInfo
{
	public int firstCharacterIndex;

	public int lastCharacterIndex;

	public int characterCount;
}
internal class TextInfo
{
	private static Vector2 s_InfinityVectorPositive = new Vector2(32767f, 32767f);

	private static Vector2 s_InfinityVectorNegative = new Vector2(-32767f, -32767f);

	public int characterCount;

	public int spriteCount;

	public int spaceCount;

	public int wordCount;

	public int linkCount;

	public int lineCount;

	public int pageCount;

	public int materialCount;

	public TextElementInfo[] textElementInfo;

	public WordInfo[] wordInfo;

	public LinkInfo[] linkInfo;

	public LineInfo[] lineInfo;

	public PageInfo[] pageInfo;

	public MeshInfo[] meshInfo;

	public bool isDirty;

	public bool hasMultipleColors = false;

	public TextInfo()
	{
		textElementInfo = new TextElementInfo[4];
		wordInfo = new WordInfo[1];
		lineInfo = new LineInfo[1];
		pageInfo = new PageInfo[1];
		linkInfo = new LinkInfo[0];
		meshInfo = new MeshInfo[1];
		materialCount = 0;
		isDirty = true;
	}

	internal void Clear()
	{
		characterCount = 0;
		spaceCount = 0;
		wordCount = 0;
		linkCount = 0;
		lineCount = 0;
		pageCount = 0;
		spriteCount = 0;
		hasMultipleColors = false;
		for (int i = 0; i < meshInfo.Length; i++)
		{
			meshInfo[i].vertexCount = 0;
		}
	}

	internal void ClearMeshInfo(bool updateMesh)
	{
		for (int i = 0; i < meshInfo.Length; i++)
		{
			meshInfo[i].Clear(updateMesh);
		}
	}

	internal void ClearLineInfo()
	{
		if (lineInfo == null)
		{
			lineInfo = new LineInfo[1];
		}
		for (int i = 0; i < lineInfo.Length; i++)
		{
			lineInfo[i].characterCount = 0;
			lineInfo[i].spaceCount = 0;
			lineInfo[i].wordCount = 0;
			lineInfo[i].controlCharacterCount = 0;
			lineInfo[i].ascender = s_InfinityVectorNegative.x;
			lineInfo[i].baseline = 0f;
			lineInfo[i].descender = s_InfinityVectorPositive.x;
			lineInfo[i].maxAdvance = 0f;
			lineInfo[i].marginLeft = 0f;
			lineInfo[i].marginRight = 0f;
			lineInfo[i].lineExtents.min = s_InfinityVectorPositive;
			lineInfo[i].lineExtents.max = s_InfinityVectorNegative;
			lineInfo[i].width = 0f;
		}
	}

	internal void ClearPageInfo()
	{
		if (pageInfo == null)
		{
			pageInfo = new PageInfo[2];
		}
		int num = pageInfo.Length;
		for (int i = 0; i < num; i++)
		{
			pageInfo[i].firstCharacterIndex = 0;
			pageInfo[i].lastCharacterIndex = 0;
			pageInfo[i].ascender = -32767f;
			pageInfo[i].baseLine = 0f;
			pageInfo[i].descender = 32767f;
		}
	}

	internal static void Resize<T>(ref T[] array, int size)
	{
		int newSize = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
		Array.Resize(ref array, newSize);
	}

	internal static void Resize<T>(ref T[] array, int size, bool isBlockAllocated)
	{
		if (isBlockAllocated)
		{
			size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
		}
		if (size != array.Length)
		{
			Array.Resize(ref array, size);
		}
	}
}
internal enum MarkupTag
{
	BOLD = 66,
	SLASH_BOLD = 1613,
	ITALIC = 73,
	SLASH_ITALIC = 1606,
	UNDERLINE = 85,
	SLASH_UNDERLINE = 1626,
	STRIKETHROUGH = 83,
	SLASH_STRIKETHROUGH = 1628,
	MARK = 2699125,
	SLASH_MARK = 57644506,
	SUBSCRIPT = 92132,
	SLASH_SUBSCRIPT = 1770219,
	SUPERSCRIPT = 92150,
	SLASH_SUPERSCRIPT = 1770233,
	COLOR = 81999901,
	SLASH_COLOR = 1909026194,
	ALPHA = 75165780,
	A = 65,
	SLASH_A = 1614,
	SIZE = 3061285,
	SLASH_SIZE = 58429962,
	SPRITE = -991527447,
	NO_BREAK = 2856657,
	SLASH_NO_BREAK = 57477502,
	STYLE = 100252951,
	SLASH_STYLE = 1927738392,
	FONT = 2586451,
	SLASH_FONT = 57747708,
	SLASH_MATERIAL = -1100708252,
	LINK = 2656128,
	SLASH_LINK = 57686191,
	FONT_WEIGHT = -1889896162,
	SLASH_FONT_WEIGHT = -757976431,
	NO_PARSE = -408011596,
	SLASH_NO_PARSE = -294095813,
	POSITION = 85420,
	SLASH_POSITION = 1777699,
	VERTICAL_OFFSET = 1952379995,
	SLASH_VERTICAL_OFFSET = -11107948,
	SPACE = 100083556,
	SLASH_SPACE = 1927873067,
	PAGE = 2808691,
	SLASH_PAGE = 58683868,
	ALIGN = 75138797,
	SLASH_ALIGN = 1916026786,
	WIDTH = 105793766,
	SLASH_WIDTH = 1923459625,
	GRADIENT = -1999759898,
	SLASH_GRADIENT = -1854491959,
	CHARACTER_SPACE = -1584382009,
	SLASH_CHARACTER_SPACE = -1394426712,
	MONOSPACE = -1340221943,
	SLASH_MONOSPACE = -1638865562,
	CLASS = 82115566,
	INDENT = -1514123076,
	SLASH_INDENT = -1496889389,
	LINE_INDENT = -844305121,
	SLASH_LINE_INDENT = 93886352,
	MARGIN = -1355614050,
	SLASH_MARGIN = -1649644303,
	MARGIN_LEFT = -272933656,
	MARGIN_RIGHT = -447416589,
	LINE_HEIGHT = -799081892,
	SLASH_LINE_HEIGHT = 200452819,
	ACTION = -1827519330,
	SLASH_ACTION = -1187217679,
	SCALE = 100553336,
	SLASH_SCALE = 1928413879,
	ROTATE = -1000007783,
	SLASH_ROTATE = -764695562,
	TABLE = 226476955,
	SLASH_TABLE = -979118220,
	TH = 5862489,
	SLASH_TH = 193346070,
	TR = 5862467,
	SLASH_TR = 193346060,
	TD = 5862485,
	SLASH_TD = 193346074,
	LOWERCASE = -1506899689,
	SLASH_LOWERCASE = -1451284584,
	ALLCAPS = 218273952,
	SLASH_ALLCAPS = -797437649,
	UPPERCASE = -305409418,
	SLASH_UPPERCASE = -582368199,
	SMALLCAPS = -766062114,
	SLASH_SMALLCAPS = 199921873,
	LIGA = 2655971,
	SLASH_LIGA = 57686604,
	FRAC = 2598518,
	SLASH_FRAC = 57774681,
	NAME = 2875623,
	INDEX = 84268030,
	TINT = 2960519,
	ANIM = 2283339,
	MATERIAL = 825491659,
	HREF = 2535353,
	ANGLE = 75347905,
	PADDING = -2144568463,
	FAMILYNAME = 704251153,
	STYLENAME = -1207081936,
	RED = 91635,
	GREEN = 87065851,
	BLUE = 2457214,
	YELLOW = -882444668,
	ORANGE = -1108587920,
	BLACK = 81074727,
	WHITE = 105680263,
	PURPLE = -1250222130,
	BR = 2256,
	CR = 2289,
	ZWSP = 3288238,
	ZWJ = 99623,
	NBSP = 2869039,
	SHY = 92674,
	LEFT = 2660507,
	RIGHT = 99937376,
	CENTER = -1591113269,
	JUSTIFIED = 817091359,
	FLUSH = 85552164,
	NONE = 2857034,
	PLUS = 43,
	MINUS = 45,
	PX = 2568,
	PLUS_PX = 49507,
	MINUS_PX = 47461,
	EM = 2216,
	PLUS_EM = 49091,
	MINUS_EM = 46789,
	PCT = 85031,
	PLUS_PCT = 1634348,
	MINUS_PCT = 1567082,
	PERCENTAGE = 37,
	PLUS_PERCENTAGE = 1454,
	MINUS_PERCENTAGE = 1512,
	TRUE = 2932022,
	FALSE = 85422813,
	INVALID = 1585415185,
	NOTDEF = 612146780,
	NORMAL = -1183493901,
	DEFAULT = -620974005,
	REGULAR = 1291372090
}
internal enum TagValueType
{
	None = 0,
	NumericalValue = 1,
	StringValue = 2,
	ColorValue = 4
}
internal enum TagUnitType
{
	Pixels,
	FontUnits,
	Percentage
}
internal static class CodePoint
{
	public const uint SPACE = 32u;

	public const uint DOUBLE_QUOTE = 34u;

	public const uint NUMBER_SIGN = 35u;

	public const uint PERCENTAGE = 37u;

	public const uint PLUS = 43u;

	public const uint MINUS = 45u;

	public const uint PERIOD = 46u;

	public const uint HYPHEN_MINUS = 45u;

	public const uint SOFT_HYPHEN = 173u;

	public const uint HYPHEN = 8208u;

	public const uint NON_BREAKING_HYPHEN = 8209u;

	public const uint ZERO_WIDTH_SPACE = 8203u;

	public const uint RIGHT_SINGLE_QUOTATION = 8217u;

	public const uint APOSTROPHE = 39u;

	public const uint WORD_JOINER = 8288u;

	public const uint HIGH_SURROGATE_START = 55296u;

	public const uint HIGH_SURROGATE_END = 56319u;

	public const uint LOW_SURROGATE_START = 56320u;

	public const uint LOW_SURROGATE_END = 57343u;

	public const uint UNICODE_PLANE01_START = 65536u;
}
internal enum TextProcessingElementType
{
	Undefined,
	TextCharacterElement,
	TextMarkupElement
}
internal struct CharacterElement
{
	private uint m_Unicode;

	private TextElement m_TextElement;

	public uint Unicode
	{
		get
		{
			return m_Unicode;
		}
		set
		{
			m_Unicode = value;
		}
	}

	public CharacterElement(TextElement textElement)
	{
		m_Unicode = textElement.unicode;
		m_TextElement = textElement;
	}
}
internal struct MarkupAttribute
{
	private int m_NameHashCode;

	private int m_ValueHashCode;

	private int m_ValueStartIndex;

	private int m_ValueLength;

	public int NameHashCode
	{
		get
		{
			return m_NameHashCode;
		}
		set
		{
			m_NameHashCode = value;
		}
	}

	public int ValueHashCode
	{
		get
		{
			return m_ValueHashCode;
		}
		set
		{
			m_ValueHashCode = value;
		}
	}

	public int ValueStartIndex
	{
		get
		{
			return m_ValueStartIndex;
		}
		set
		{
			m_ValueStartIndex = value;
		}
	}

	public int ValueLength
	{
		get
		{
			return m_ValueLength;
		}
		set
		{
			m_ValueLength = value;
		}
	}
}
internal struct MarkupElement
{
	private MarkupAttribute[] m_Attributes;

	public int NameHashCode
	{
		get
		{
			return (m_Attributes != null) ? m_Attributes[0].NameHashCode : 0;
		}
		set
		{
			if (m_Attributes == null)
			{
				m_Attributes = new MarkupAttribute[8];
			}
			m_Attributes[0].NameHashCode = value;
		}
	}

	public int ValueHashCode
	{
		get
		{
			return (m_Attributes != null) ? m_Attributes[0].ValueHashCode : 0;
		}
		set
		{
			m_Attributes[0].ValueHashCode = value;
		}
	}

	public int ValueStartIndex
	{
		get
		{
			return (m_Attributes != null) ? m_Attributes[0].ValueStartIndex : 0;
		}
		set
		{
			m_Attributes[0].ValueStartIndex = value;
		}
	}

	public int ValueLength
	{
		get
		{
			return (m_Attributes != null) ? m_Attributes[0].ValueLength : 0;
		}
		set
		{
			m_Attributes[0].ValueLength = value;
		}
	}

	public MarkupAttribute[] Attributes
	{
		get
		{
			return m_Attributes;
		}
		set
		{
			m_Attributes = value;
		}
	}

	public MarkupElement(int nameHashCode, int startIndex, int length)
	{
		m_Attributes = new MarkupAttribute[8];
		m_Attributes[0].NameHashCode = nameHashCode;
		m_Attributes[0].ValueStartIndex = startIndex;
		m_Attributes[0].ValueLength = length;
	}
}
internal struct FontStyleStack
{
	public byte bold;

	public byte italic;

	public byte underline;

	public byte strikethrough;

	public byte highlight;

	public byte superscript;

	public byte subscript;

	public byte uppercase;

	public byte lowercase;

	public byte smallcaps;

	public void Clear()
	{
		bold = 0;
		italic = 0;
		underline = 0;
		strikethrough = 0;
		highlight = 0;
		superscript = 0;
		subscript = 0;
		uppercase = 0;
		lowercase = 0;
		smallcaps = 0;
	}

	public byte Add(FontStyles style)
	{
		switch (style)
		{
		case FontStyles.Bold:
			bold++;
			return bold;
		case FontStyles.Italic:
			italic++;
			return italic;
		case FontStyles.Underline:
			underline++;
			return underline;
		case FontStyles.UpperCase:
			uppercase++;
			return uppercase;
		case FontStyles.LowerCase:
			lowercase++;
			return lowercase;
		case FontStyles.Strikethrough:
			strikethrough++;
			return strikethrough;
		case FontStyles.Superscript:
			superscript++;
			return superscript;
		case FontStyles.Subscript:
			subscript++;
			return subscript;
		case FontStyles.Highlight:
			highlight++;
			return highlight;
		default:
			return 0;
		}
	}

	public byte Remove(FontStyles style)
	{
		switch (style)
		{
		case FontStyles.Bold:
			if (bold > 1)
			{
				bold--;
			}
			else
			{
				bold = 0;
			}
			return bold;
		case FontStyles.Italic:
			if (italic > 1)
			{
				italic--;
			}
			else
			{
				italic = 0;
			}
			return italic;
		case FontStyles.Underline:
			if (underline > 1)
			{
				underline--;
			}
			else
			{
				underline = 0;
			}
			return underline;
		case FontStyles.UpperCase:
			if (uppercase > 1)
			{
				uppercase--;
			}
			else
			{
				uppercase = 0;
			}
			return uppercase;
		case FontStyles.LowerCase:
			if (lowercase > 1)
			{
				lowercase--;
			}
			else
			{
				lowercase = 0;
			}
			return lowercase;
		case FontStyles.Strikethrough:
			if (strikethrough > 1)
			{
				strikethrough--;
			}
			else
			{
				strikethrough = 0;
			}
			return strikethrough;
		case FontStyles.Highlight:
			if (highlight > 1)
			{
				highlight--;
			}
			else
			{
				highlight = 0;
			}
			return highlight;
		case FontStyles.Superscript:
			if (superscript > 1)
			{
				superscript--;
			}
			else
			{
				superscript = 0;
			}
			return superscript;
		case FontStyles.Subscript:
			if (subscript > 1)
			{
				subscript--;
			}
			else
			{
				subscript = 0;
			}
			return subscript;
		default:
			return 0;
		}
	}
}
[DebuggerDisplay("Item count = {m_Count}")]
internal struct TextProcessingStack<T>
{
	public T[] itemStack;

	public int index;

	private T m_DefaultItem;

	private int m_Capacity;

	private int m_RolloverSize;

	private int m_Count;

	private const int k_DefaultCapacity = 4;

	public int Count => m_Count;

	public T current
	{
		get
		{
			if (index > 0)
			{
				return itemStack[index - 1];
			}
			return itemStack[0];
		}
	}

	public int rolloverSize
	{
		get
		{
			return m_RolloverSize;
		}
		set
		{
			m_RolloverSize = value;
		}
	}

	public TextProcessingStack(T[] stack)
	{
		itemStack = stack;
		m_Capacity = stack.Length;
		index = 0;
		m_RolloverSize = 0;
		m_DefaultItem = default(T);
		m_Count = 0;
	}

	public TextProcessingStack(int capacity)
	{
		itemStack = new T[capacity];
		m_Capacity = capacity;
		index = 0;
		m_RolloverSize = 0;
		m_DefaultItem = default(T);
		m_Count = 0;
	}

	public TextProcessingStack(int capacity, int rolloverSize)
	{
		itemStack = new T[capacity];
		m_Capacity = capacity;
		index = 0;
		m_RolloverSize = rolloverSize;
		m_DefaultItem = default(T);
		m_Count = 0;
	}

	internal static void SetDefault(TextProcessingStack<T>[] stack, T item)
	{
		for (int i = 0; i < stack.Length; i++)
		{
			stack[i].SetDefault(item);
		}
	}

	public void Clear()
	{
		index = 0;
		m_Count = 0;
	}

	public void SetDefault(T item)
	{
		if (itemStack == null)
		{
			m_Capacity = 4;
			itemStack = new T[m_Capacity];
			m_DefaultItem = default(T);
		}
		itemStack[0] = item;
		index = 1;
		m_Count = 1;
	}

	public void Add(T item)
	{
		if (index < itemStack.Length)
		{
			itemStack[index] = item;
			index++;
		}
	}

	public T Remove()
	{
		index--;
		m_Count--;
		if (index <= 0)
		{
			m_Count = 0;
			index = 1;
			return itemStack[0];
		}
		return itemStack[index - 1];
	}

	public void Push(T item)
	{
		if (index == m_Capacity)
		{
			m_Capacity *= 2;
			if (m_Capacity == 0)
			{
				m_Capacity = 4;
			}
			Array.Resize(ref itemStack, m_Capacity);
		}
		itemStack[index] = item;
		if (m_RolloverSize == 0)
		{
			index++;
			m_Count++;
		}
		else
		{
			index = (index + 1) % m_RolloverSize;
			m_Count = ((m_Count < m_RolloverSize) ? (m_Count + 1) : m_RolloverSize);
		}
	}

	public T Pop()
	{
		if (index == 0 && m_RolloverSize == 0)
		{
			return default(T);
		}
		if (m_RolloverSize == 0)
		{
			index--;
		}
		else
		{
			index = (index - 1) % m_RolloverSize;
			index = ((index < 0) ? (index + m_RolloverSize) : index);
		}
		T result = itemStack[index];
		itemStack[index] = m_DefaultItem;
		m_Count = ((m_Count > 0) ? (m_Count - 1) : 0);
		return result;
	}

	public T Peek()
	{
		if (index == 0)
		{
			return m_DefaultItem;
		}
		return itemStack[index - 1];
	}

	public T CurrentItem()
	{
		if (index > 0)
		{
			return itemStack[index - 1];
		}
		return itemStack[0];
	}

	public T PreviousItem()
	{
		if (index > 1)
		{
			return itemStack[index - 2];
		}
		return itemStack[0];
	}
}
internal class TextResourceManager
{
	private struct FontAssetRef
	{
		public int nameHashCode;

		public int familyNameHashCode;

		public int styleNameHashCode;

		public long familyNameAndStyleHashCode;

		public readonly FontAsset fontAsset;

		public FontAssetRef(int nameHashCode, int familyNameHashCode, int styleNameHashCode, FontAsset fontAsset)
		{
			this.nameHashCode = ((nameHashCode != 0) ? nameHashCode : familyNameHashCode);
			this.familyNameHashCode = familyNameHashCode;
			this.styleNameHashCode = styleNameHashCode;
			familyNameAndStyleHashCode = ((long)styleNameHashCode << 32) | (uint)familyNameHashCode;
			this.fontAsset = fontAsset;
		}
	}

	private static readonly Dictionary<int, FontAssetRef> s_FontAssetReferences = new Dictionary<int, FontAssetRef>();

	private static readonly Dictionary<int, FontAsset> s_FontAssetNameReferenceLookup = new Dictionary<int, FontAsset>();

	private static readonly Dictionary<long, FontAsset> s_FontAssetFamilyNameAndStyleReferenceLookup = new Dictionary<long, FontAsset>();

	private static readonly List<int> s_FontAssetRemovalList = new List<int>(16);

	private static readonly int k_RegularStyleHashCode = TextUtilities.GetHashCodeCaseInSensitive("Regular");

	internal static void AddFontAsset(FontAsset fontAsset)
	{
		int instanceID = fontAsset.instanceID;
		if (!s_FontAssetReferences.ContainsKey(instanceID))
		{
			FontAssetRef value = new FontAssetRef(fontAsset.hashCode, fontAsset.familyNameHashCode, fontAsset.styleNameHashCode, fontAsset);
			s_FontAssetReferences.Add(instanceID, value);
			if (!s_FontAssetNameReferenceLookup.ContainsKey(value.nameHashCode))
			{
				s_FontAssetNameReferenceLookup.Add(value.nameHashCode, fontAsset);
			}
			if (!s_FontAssetFamilyNameAndStyleReferenceLookup.ContainsKey(value.familyNameAndStyleHashCode))
			{
				s_FontAssetFamilyNameAndStyleReferenceLookup.Add(value.familyNameAndStyleHashCode, fontAsset);
			}
			return;
		}
		FontAssetRef value2 = s_FontAssetReferences[instanceID];
		if (value2.nameHashCode == fontAsset.hashCode && value2.familyNameHashCode == fontAsset.familyNameHashCode && value2.styleNameHashCode == fontAsset.styleNameHashCode)
		{
			return;
		}
		if (value2.nameHashCode != fontAsset.hashCode)
		{
			s_FontAssetNameReferenceLookup.Remove(value2.nameHashCode);
			value2.nameHashCode = fontAsset.hashCode;
			if (!s_FontAssetNameReferenceLookup.ContainsKey(value2.nameHashCode))
			{
				s_FontAssetNameReferenceLookup.Add(value2.nameHashCode, fontAsset);
			}
		}
		if (value2.familyNameHashCode != fontAsset.familyNameHashCode || value2.styleNameHashCode != fontAsset.styleNameHashCode)
		{
			s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(value2.familyNameAndStyleHashCode);
			value2.familyNameHashCode = fontAsset.familyNameHashCode;
			value2.styleNameHashCode = fontAsset.styleNameHashCode;
			value2.familyNameAndStyleHashCode = ((long)fontAsset.styleNameHashCode << 32) | (uint)fontAsset.familyNameHashCode;
			if (!s_FontAssetFamilyNameAndStyleReferenceLookup.ContainsKey(value2.familyNameAndStyleHashCode))
			{
				s_FontAssetFamilyNameAndStyleReferenceLookup.Add(value2.familyNameAndStyleHashCode, fontAsset);
			}
		}
		s_FontAssetReferences[instanceID] = value2;
	}

	public static void RemoveFontAsset(FontAsset fontAsset)
	{
		int instanceID = fontAsset.instanceID;
		if (s_FontAssetReferences.TryGetValue(instanceID, out var value))
		{
			s_FontAssetNameReferenceLookup.Remove(value.nameHashCode);
			s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(value.familyNameAndStyleHashCode);
			s_FontAssetReferences.Remove(instanceID);
		}
	}

	internal static bool TryGetFontAssetByName(int nameHashcode, out FontAsset fontAsset)
	{
		fontAsset = null;
		return s_FontAssetNameReferenceLookup.TryGetValue(nameHashcode, out fontAsset);
	}

	internal static bool TryGetFontAssetByFamilyName(int familyNameHashCode, int styleNameHashCode, out FontAsset fontAsset)
	{
		fontAsset = null;
		if (styleNameHashCode == 0)
		{
			styleNameHashCode = k_RegularStyleHashCode;
		}
		long key = ((long)styleNameHashCode << 32) | (uint)familyNameHashCode;
		return s_FontAssetFamilyNameAndStyleReferenceLookup.TryGetValue(key, out fontAsset);
	}

	internal static void RebuildFontAssetCache()
	{
		foreach (KeyValuePair<int, FontAssetRef> s_FontAssetReference in s_FontAssetReferences)
		{
			FontAssetRef value = s_FontAssetReference.Value;
			FontAsset fontAsset = value.fontAsset;
			if (fontAsset == null)
			{
				s_FontAssetNameReferenceLookup.Remove(value.nameHashCode);
				s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(value.familyNameAndStyleHashCode);
				s_FontAssetRemovalList.Add(s_FontAssetReference.Key);
			}
			else
			{
				fontAsset.InitializeCharacterLookupDictionary();
				fontAsset.AddSynthesizedCharactersAndFaceMetrics();
			}
		}
		for (int i = 0; i < s_FontAssetRemovalList.Count; i++)
		{
			s_FontAssetReferences.Remove(s_FontAssetRemovalList[i]);
		}
		s_FontAssetRemovalList.Clear();
		TextEventManager.ON_FONT_PROPERTY_CHANGED(isChanged: true, null);
	}
}
[Serializable]
[ExcludeFromPreset]
[ExcludeFromObjectFactory]
public class TextSettings : ScriptableObject
{
	[Serializable]
	private struct FontReferenceMap
	{
		public Font font;

		public FontAsset fontAsset;

		public FontReferenceMap(Font font, FontAsset fontAsset)
		{
			this.font = font;
			this.fontAsset = fontAsset;
		}
	}

	[SerializeField]
	protected string m_Version;

	[SerializeField]
	[FormerlySerializedAs("m_defaultFontAsset")]
	protected FontAsset m_DefaultFontAsset;

	[FormerlySerializedAs("m_defaultFontAssetPath")]
	[SerializeField]
	protected string m_DefaultFontAssetPath = "Fonts & Materials/";

	[FormerlySerializedAs("m_fallbackFontAssets")]
	[SerializeField]
	protected List<FontAsset> m_FallbackFontAssets;

	[FormerlySerializedAs("m_matchMaterialPreset")]
	[SerializeField]
	protected bool m_MatchMaterialPreset;

	[FormerlySerializedAs("m_missingGlyphCharacter")]
	[SerializeField]
	protected int m_MissingCharacterUnicode;

	[SerializeField]
	protected bool m_ClearDynamicDataOnBuild = true;

	[FormerlySerializedAs("m_defaultSpriteAsset")]
	[SerializeField]
	protected SpriteAsset m_DefaultSpriteAsset;

	[SerializeField]
	[FormerlySerializedAs("m_defaultSpriteAssetPath")]
	protected string m_DefaultSpriteAssetPath = "Sprite Assets/";

	[SerializeField]
	protected List<SpriteAsset> m_FallbackSpriteAssets;

	[SerializeField]
	protected uint m_MissingSpriteCharacterUnicode;

	[FormerlySerializedAs("m_defaultStyleSheet")]
	[SerializeField]
	protected TextStyleSheet m_DefaultStyleSheet;

	[SerializeField]
	protected string m_StyleSheetsResourcePath = "Text Style Sheets/";

	[FormerlySerializedAs("m_defaultColorGradientPresetsPath")]
	[SerializeField]
	protected string m_DefaultColorGradientPresetsPath = "Text Color Gradients/";

	[SerializeField]
	protected UnicodeLineBreakingRules m_UnicodeLineBreakingRules;

	[SerializeField]
	private bool m_UseModernHangulLineBreakingRules;

	[FormerlySerializedAs("m_warningsDisabled")]
	[SerializeField]
	protected bool m_DisplayWarnings = false;

	internal Dictionary<int, FontAsset> m_FontLookup;

	private List<FontReferenceMap> m_FontReferences = new List<FontReferenceMap>();

	public string version
	{
		get
		{
			return m_Version;
		}
		internal set
		{
			m_Version = value;
		}
	}

	public FontAsset defaultFontAsset
	{
		get
		{
			return m_DefaultFontAsset;
		}
		set
		{
			m_DefaultFontAsset = value;
		}
	}

	public string defaultFontAssetPath
	{
		get
		{
			return m_DefaultFontAssetPath;
		}
		set
		{
			m_DefaultFontAssetPath = value;
		}
	}

	public List<FontAsset> fallbackFontAssets
	{
		get
		{
			return m_FallbackFontAssets;
		}
		set
		{
			m_FallbackFontAssets = value;
		}
	}

	public bool matchMaterialPreset
	{
		get
		{
			return m_MatchMaterialPreset;
		}
		set
		{
			m_MatchMaterialPreset = value;
		}
	}

	public int missingCharacterUnicode
	{
		get
		{
			return m_MissingCharacterUnicode;
		}
		set
		{
			m_MissingCharacterUnicode = value;
		}
	}

	public bool clearDynamicDataOnBuild
	{
		get
		{
			return m_ClearDynamicDataOnBuild;
		}
		set
		{
			m_ClearDynamicDataOnBuild = value;
		}
	}

	public SpriteAsset defaultSpriteAsset
	{
		get
		{
			return m_DefaultSpriteAsset;
		}
		set
		{
			m_DefaultSpriteAsset = value;
		}
	}

	public string defaultSpriteAssetPath
	{
		get
		{
			return m_DefaultSpriteAssetPath;
		}
		set
		{
			m_DefaultSpriteAssetPath = value;
		}
	}

	public List<SpriteAsset> fallbackSpriteAssets
	{
		get
		{
			return m_FallbackSpriteAssets;
		}
		set
		{
			m_FallbackSpriteAssets = value;
		}
	}

	public uint missingSpriteCharacterUnicode
	{
		get
		{
			return m_MissingSpriteCharacterUnicode;
		}
		set
		{
			m_MissingSpriteCharacterUnicode = value;
		}
	}

	public TextStyleSheet defaultStyleSheet
	{
		get
		{
			return m_DefaultStyleSheet;
		}
		set
		{
			m_DefaultStyleSheet = value;
		}
	}

	public string styleSheetsResourcePath
	{
		get
		{
			return m_StyleSheetsResourcePath;
		}
		set
		{
			m_StyleSheetsResourcePath = value;
		}
	}

	public string defaultColorGradientPresetsPath
	{
		get
		{
			return m_DefaultColorGradientPresetsPath;
		}
		set
		{
			m_DefaultColorGradientPresetsPath = value;
		}
	}

	public UnicodeLineBreakingRules lineBreakingRules
	{
		get
		{
			if (m_UnicodeLineBreakingRules == null)
			{
				m_UnicodeLineBreakingRules = new UnicodeLineBreakingRules();
				m_UnicodeLineBreakingRules.LoadLineBreakingRules();
			}
			return m_UnicodeLineBreakingRules;
		}
		set
		{
			m_UnicodeLineBreakingRules = value;
		}
	}

	public bool useModernHangulLineBreakingRules
	{
		get
		{
			return m_UseModernHangulLineBreakingRules;
		}
		set
		{
			m_UseModernHangulLineBreakingRules = value;
		}
	}

	public bool displayWarnings
	{
		get
		{
			return m_DisplayWarnings;
		}
		set
		{
			m_DisplayWarnings = value;
		}
	}

	private void OnEnable()
	{
		lineBreakingRules.LoadLineBreakingRules();
	}

	protected void InitializeFontReferenceLookup()
	{
		if (m_FontReferences == null)
		{
			m_FontReferences = new List<FontReferenceMap>();
		}
		for (int i = 0; i < m_FontReferences.Count; i++)
		{
			FontReferenceMap fontReferenceMap = m_FontReferences[i];
			if (fontReferenceMap.font == null || fontReferenceMap.fontAsset == null)
			{
				Debug.Log("Deleting invalid font reference.");
				m_FontReferences.RemoveAt(i);
				i--;
				continue;
			}
			int instanceID = fontReferenceMap.font.GetInstanceID();
			if (!m_FontLookup.ContainsKey(instanceID))
			{
				m_FontLookup.Add(instanceID, fontReferenceMap.fontAsset);
			}
		}
	}

	protected FontAsset GetCachedFontAssetInternal(Font font)
	{
		if (m_FontLookup == null)
		{
			m_FontLookup = new Dictionary<int, FontAsset>();
			InitializeFontReferenceLookup();
		}
		int instanceID = font.GetInstanceID();
		if (m_FontLookup.ContainsKey(instanceID))
		{
			return m_FontLookup[instanceID];
		}
		FontAsset fontAsset = ((!(font.name == "System Normal")) ? FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024) : FontAsset.CreateFontAsset("Lucida Grande", "Regular"));
		if (fontAsset != null)
		{
			fontAsset.hideFlags = HideFlags.DontSave;
			fontAsset.atlasTextures[0].hideFlags = HideFlags.DontSave;
			fontAsset.material.hideFlags = HideFlags.DontSave;
			fontAsset.isMultiAtlasTexturesEnabled = true;
			m_FontReferences.Add(new FontReferenceMap(font, fontAsset));
			m_FontLookup.Add(instanceID, fontAsset);
		}
		return fontAsset;
	}
}
[ExcludeFromDocs]
public static class TextShaderUtilities
{
	public static int ID_MainTex;

	public static int ID_FaceTex;

	public static int ID_FaceColor;

	public static int ID_FaceDilate;

	public static int ID_Shininess;

	public static int ID_OutlineOffset1;

	public static int ID_OutlineOffset2;

	public static int ID_OutlineOffset3;

	public static int ID_OutlineMode;

	public static int ID_IsoPerimeter;

	public static int ID_Softness;

	public static int ID_UnderlayColor;

	public static int ID_UnderlayOffsetX;

	public static int ID_UnderlayOffsetY;

	public static int ID_UnderlayDilate;

	public static int ID_UnderlaySoftness;

	public static int ID_UnderlayOffset;

	public static int ID_UnderlayIsoPerimeter;

	public static int ID_WeightNormal;

	public static int ID_WeightBold;

	public static int ID_OutlineTex;

	public static int ID_OutlineWidth;

	public static int ID_OutlineSoftness;

	public static int ID_OutlineColor;

	public static int ID_Outline2Color;

	public static int ID_Outline2Width;

	public static int ID_Padding;

	public static int ID_GradientScale;

	public static int ID_ScaleX;

	public static int ID_ScaleY;

	public static int ID_PerspectiveFilter;

	public static int ID_Sharpness;

	public static int ID_TextureWidth;

	public static int ID_TextureHeight;

	public static int ID_BevelAmount;

	public static int ID_GlowColor;

	public static int ID_GlowOffset;

	public static int ID_GlowPower;

	public static int ID_GlowOuter;

	public static int ID_GlowInner;

	public static int ID_LightAngle;

	public static int ID_EnvMap;

	public static int ID_EnvMatrix;

	public static int ID_EnvMatrixRotation;

	public static int ID_MaskCoord;

	public static int ID_ClipRect;

	public static int ID_MaskSoftnessX;

	public static int ID_MaskSoftnessY;

	public static int ID_VertexOffsetX;

	public static int ID_VertexOffsetY;

	public static int ID_UseClipRect;

	public static int ID_StencilID;

	public static int ID_StencilOp;

	public static int ID_StencilComp;

	public static int ID_StencilReadMask;

	public static int ID_StencilWriteMask;

	public static int ID_ShaderFlags;

	public static int ID_ScaleRatio_A;

	public static int ID_ScaleRatio_B;

	public static int ID_ScaleRatio_C;

	public static string Keyword_Bevel;

	public static string Keyword_Glow;

	public static string Keyword_Underlay;

	public static string Keyword_Ratios;

	public static string Keyword_MASK_SOFT;

	public static string Keyword_MASK_HARD;

	public static string Keyword_MASK_TEX;

	public static string Keyword_Outline;

	public static string ShaderTag_ZTestMode;

	public static string ShaderTag_CullMode;

	private static float m_clamp;

	public static bool isInitialized;

	private static Shader k_ShaderRef_MobileSDF;

	private static Shader k_ShaderRef_MobileBitmap;

	private static Shader k_ShaderRef_Sprite;

	internal static Shader ShaderRef_MobileSDF
	{
		get
		{
			if (k_ShaderRef_MobileSDF == null)
			{
				k_ShaderRef_MobileSDF = Shader.Find("TextMeshPro/Mobile/Distance Field SSD");
				if (k_ShaderRef_MobileSDF == null)
				{
					k_ShaderRef_MobileSDF = Shader.Find("Text/Mobile/Distance Field SSD");
				}
				if (k_ShaderRef_MobileSDF == null)
				{
					k_ShaderRef_MobileSDF = Shader.Find("Hidden/TextCore/Distance Field SSD");
				}
			}
			return k_ShaderRef_MobileSDF;
		}
	}

	internal static Shader ShaderRef_MobileBitmap
	{
		get
		{
			if (k_ShaderRef_MobileBitmap == null)
			{
				k_ShaderRef_MobileBitmap = Shader.Find("TextMeshPro/Mobile/Bitmap");
				if (k_ShaderRef_MobileBitmap == null)
				{
					k_ShaderRef_MobileBitmap = Shader.Find("Text/Bitmap");
				}
				if (k_ShaderRef_MobileBitmap == null)
				{
					k_ShaderRef_MobileBitmap = Shader.Find("Hidden/Internal-GUITextureClipText");
				}
			}
			return k_ShaderRef_MobileBitmap;
		}
	}

	internal static Shader ShaderRef_Sprite
	{
		get
		{
			if (k_ShaderRef_Sprite == null)
			{
				k_ShaderRef_Sprite = Shader.Find("TextMeshPro/Sprite");
				if (k_ShaderRef_Sprite == null)
				{
					k_ShaderRef_Sprite = Shader.Find("Text/Sprite");
				}
				if (k_ShaderRef_Sprite == null)
				{
					k_ShaderRef_Sprite = Shader.Find("Hidden/TextCore/Sprite");
				}
			}
			return k_ShaderRef_Sprite;
		}
	}

	static TextShaderUtilities()
	{
		Keyword_Bevel = "BEVEL_ON";
		Keyword_Glow = "GLOW_ON";
		Keyword_Underlay = "UNDERLAY_ON";
		Keyword_Ratios = "RATIOS_OFF";
		Keyword_MASK_SOFT = "MASK_SOFT";
		Keyword_MASK_HARD = "MASK_HARD";
		Keyword_MASK_TEX = "MASK_TEX";
		Keyword_Outline = "OUTLINE_ON";
		ShaderTag_ZTestMode = "unity_GUIZTestMode";
		ShaderTag_CullMode = "_CullMode";
		m_clamp = 1f;
		isInitialized = false;
		GetShaderPropertyIDs();
	}

	internal static void GetShaderPropertyIDs()
	{
		if (!isInitialized)
		{
			isInitialized = true;
			ID_MainTex = Shader.PropertyToID("_MainTex");
			ID_FaceTex = Shader.PropertyToID("_FaceTex");
			ID_FaceColor = Shader.PropertyToID("_FaceColor");
			ID_FaceDilate = Shader.PropertyToID("_FaceDilate");
			ID_Shininess = Shader.PropertyToID("_FaceShininess");
			ID_OutlineOffset1 = Shader.PropertyToID("_OutlineOffset1");
			ID_OutlineOffset2 = Shader.PropertyToID("_OutlineOffset2");
			ID_OutlineOffset3 = Shader.PropertyToID("_OutlineOffset3");
			ID_OutlineMode = Shader.PropertyToID("_OutlineMode");
			ID_IsoPerimeter = Shader.PropertyToID("_IsoPerimeter");
			ID_Softness = Shader.PropertyToID("_Softness");
			ID_UnderlayColor = Shader.PropertyToID("_UnderlayColor");
			ID_UnderlayOffsetX = Shader.PropertyToID("_UnderlayOffsetX");
			ID_UnderlayOffsetY = Shader.PropertyToID("_UnderlayOffsetY");
			ID_UnderlayDilate = Shader.PropertyToID("_UnderlayDilate");
			ID_UnderlaySoftness = Shader.PropertyToID("_UnderlaySoftness");
			ID_UnderlayOffset = Shader.PropertyToID("_UnderlayOffset");
			ID_UnderlayIsoPerimeter = Shader.PropertyToID("_UnderlayIsoPerimeter");
			ID_WeightNormal = Shader.PropertyToID("_WeightNormal");
			ID_WeightBold = Shader.PropertyToID("_WeightBold");
			ID_OutlineTex = Shader.PropertyToID("_OutlineTex");
			ID_OutlineWidth = Shader.PropertyToID("_OutlineWidth");
			ID_OutlineSoftness = Shader.PropertyToID("_OutlineSoftness");
			ID_OutlineColor = Shader.PropertyToID("_OutlineColor");
			ID_Outline2Color = Shader.PropertyToID("_Outline2Color");
			ID_Outline2Width = Shader.PropertyToID("_Outline2Width");
			ID_Padding = Shader.PropertyToID("_Padding");
			ID_GradientScale = Shader.PropertyToID("_GradientScale");
			ID_ScaleX = Shader.PropertyToID("_ScaleX");
			ID_ScaleY = Shader.PropertyToID("_ScaleY");
			ID_PerspectiveFilter = Shader.PropertyToID("_PerspectiveFilter");
			ID_Sharpness = Shader.PropertyToID("_Sharpness");
			ID_TextureWidth = Shader.PropertyToID("_TextureWidth");
			ID_TextureHeight = Shader.PropertyToID("_TextureHeight");
			ID_BevelAmount = Shader.PropertyToID("_Bevel");
			ID_LightAngle = Shader.PropertyToID("_LightAngle");
			ID_EnvMap = Shader.PropertyToID("_Cube");
			ID_EnvMatrix = Shader.PropertyToID("_EnvMatrix");
			ID_EnvMatrixRotation = Shader.PropertyToID("_EnvMatrixRotation");
			ID_GlowColor = Shader.PropertyToID("_GlowColor");
			ID_GlowOffset = Shader.PropertyToID("_GlowOffset");
			ID_GlowPower = Shader.PropertyToID("_GlowPower");
			ID_GlowOuter = Shader.PropertyToID("_GlowOuter");
			ID_GlowInner = Shader.PropertyToID("_GlowInner");
			ID_MaskCoord = Shader.PropertyToID("_MaskCoord");
			ID_ClipRect = Shader.PropertyToID("_ClipRect");
			ID_UseClipRect = Shader.PropertyToID("_UseClipRect");
			ID_MaskSoftnessX = Shader.PropertyToID("_MaskSoftnessX");
			ID_MaskSoftnessY = Shader.PropertyToID("_MaskSoftnessY");
			ID_VertexOffsetX = Shader.PropertyToID("_VertexOffsetX");
			ID_VertexOffsetY = Shader.PropertyToID("_VertexOffsetY");
			ID_StencilID = Shader.PropertyToID("_Stencil");
			ID_StencilOp = Shader.PropertyToID("_StencilOp");
			ID_StencilComp = Shader.PropertyToID("_StencilComp");
			ID_StencilReadMask = Shader.PropertyToID("_StencilReadMask");
			ID_StencilWriteMask = Shader.PropertyToID("_StencilWriteMask");
			ID_ShaderFlags = Shader.PropertyToID("_ShaderFlags");
			ID_ScaleRatio_A = Shader.PropertyToID("_ScaleRatioA");
			ID_ScaleRatio_B = Shader.PropertyToID("_ScaleRatioB");
			ID_ScaleRatio_C = Shader.PropertyToID("_ScaleRatioC");
		}
	}

	private static void UpdateShaderRatios(Material mat)
	{
		float num = 1f;
		float num2 = 1f;
		float num3 = 1f;
		bool flag = !mat.shaderKeywords.Contains(Keyword_Ratios);
		if (mat.HasProperty(ID_GradientScale) && mat.HasProperty(ID_FaceDilate))
		{
			float num4 = mat.GetFloat(ID_GradientScale);
			float num5 = mat.GetFloat(ID_FaceDilate);
			float num6 = mat.GetFloat(ID_OutlineWidth);
			float num7 = mat.GetFloat(ID_OutlineSoftness);
			float num8 = Mathf.Max(mat.GetFloat(ID_WeightNormal), mat.GetFloat(ID_WeightBold)) / 4f;
			float num9 = Mathf.Max(1f, num8 + num5 + num6 + num7);
			num = (flag ? ((num4 - m_clamp) / (num4 * num9)) : 1f);
			mat.SetFloat(ID_ScaleRatio_A, num);
			if (mat.HasProperty(ID_GlowOffset))
			{
				float num10 = mat.GetFloat(ID_GlowOffset);
				float num11 = mat.GetFloat(ID_GlowOuter);
				float num12 = (num8 + num5) * (num4 - m_clamp);
				num9 = Mathf.Max(1f, num10 + num11);
				num2 = (flag ? (Mathf.Max(0f, num4 - m_clamp - num12) / (num4 * num9)) : 1f);
				mat.SetFloat(ID_ScaleRatio_B, num2);
			}
			if (mat.HasProperty(ID_UnderlayOffsetX))
			{
				float f = mat.GetFloat(ID_UnderlayOffsetX);
				float f2 = mat.GetFloat(ID_UnderlayOffsetY);
				float num13 = mat.GetFloat(ID_UnderlayDilate);
				float num14 = mat.GetFloat(ID_UnderlaySoftness);
				float num15 = (num8 + num5) * (num4 - m_clamp);
				num9 = Mathf.Max(1f, Mathf.Max(Mathf.Abs(f), Mathf.Abs(f2)) + num13 + num14);
				num3 = (flag ? (Mathf.Max(0f, num4 - m_clamp - num15) / (num4 * num9)) : 1f);
				mat.SetFloat(ID_ScaleRatio_C, num3);
			}
		}
	}

	internal static Vector4 GetFontExtent(Material material)
	{
		return Vector4.zero;
	}

	internal static bool IsMaskingEnabled(Material material)
	{
		if (material == null || !material.HasProperty(ID_ClipRect))
		{
			return false;
		}
		if (material.shaderKeywords.Contains(Keyword_MASK_SOFT) || material.shaderKeywords.Contains(Keyword_MASK_HARD) || material.shaderKeywords.Contains(Keyword_MASK_TEX))
		{
			return true;
		}
		return false;
	}

	internal static float GetPadding(Material material, bool enableExtraPadding, bool isBold)
	{
		if (!isInitialized)
		{
			GetShaderPropertyIDs();
		}
		if (material == null)
		{
			return 0f;
		}
		int num = (enableExtraPadding ? 4 : 0);
		if (!material.HasProperty(ID_GradientScale))
		{
			if (material.HasProperty(ID_Padding))
			{
				num += (int)material.GetFloat(ID_Padding);
			}
			return (float)num + 1f;
		}
		if (material.HasProperty(ID_IsoPerimeter))
		{
			return ComputePaddingForProperties(material) + 0.25f + (float)num;
		}
		Vector4 zero = Vector4.zero;
		Vector4 zero2 = Vector4.zero;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		float num11 = 0f;
		UpdateShaderRatios(material);
		string[] shaderKeywords = material.shaderKeywords;
		if (material.HasProperty(ID_ScaleRatio_A))
		{
			num5 = material.GetFloat(ID_ScaleRatio_A);
		}
		if (material.HasProperty(ID_FaceDilate))
		{
			num2 = material.GetFloat(ID_FaceDilate) * num5;
		}
		if (material.HasProperty(ID_OutlineSoftness))
		{
			num3 = material.GetFloat(ID_OutlineSoftness) * num5;
		}
		if (material.HasProperty(ID_OutlineWidth))
		{
			num4 = material.GetFloat(ID_OutlineWidth) * num5;
		}
		num11 = num4 + num3 + num2;
		if (material.HasProperty(ID_GlowOffset) && shaderKeywords.Contains(Keyword_Glow))
		{
			if (material.HasProperty(ID_ScaleRatio_B))
			{
				num6 = material.GetFloat(ID_ScaleRatio_B);
			}
			num8 = material.GetFloat(ID_GlowOffset) * num6;
			num9 = material.GetFloat(ID_GlowOuter) * num6;
		}
		num11 = Mathf.Max(num11, num2 + num8 + num9);
		if (material.HasProperty(ID_UnderlaySoftness) && shaderKeywords.Contains(Keyword_Underlay))
		{
			if (material.HasProperty(ID_ScaleRatio_C))
			{
				num7 = material.GetFloat(ID_ScaleRatio_C);
			}
			float num12 = 0f;
			float num13 = 0f;
			float num14 = 0f;
			float num15 = 0f;
			if (material.HasProperty(ID_UnderlayOffset))
			{
				Vector2 vector = material.GetVector(ID_UnderlayOffset);
				num12 = vector.x;
				num13 = vector.y;
				num14 = material.GetFloat(ID_UnderlayDilate);
				num15 = material.GetFloat(ID_UnderlaySoftness);
			}
			else if (material.HasProperty(ID_UnderlayOffsetX))
			{
				num12 = material.GetFloat(ID_UnderlayOffsetX) * num7;
				num13 = material.GetFloat(ID_UnderlayOffsetY) * num7;
				num14 = material.GetFloat(ID_UnderlayDilate) * num7;
				num15 = material.GetFloat(ID_UnderlaySoftness) * num7;
			}
			zero.x = Mathf.Max(zero.x, num2 + num14 + num15 - num12);
			zero.y = Mathf.Max(zero.y, num2 + num14 + num15 - num13);
			zero.z = Mathf.Max(zero.z, num2 + num14 + num15 + num12);
			zero.w = Mathf.Max(zero.w, num2 + num14 + num15 + num13);
		}
		zero.x = Mathf.Max(zero.x, num11);
		zero.y = Mathf.Max(zero.y, num11);
		zero.z = Mathf.Max(zero.z, num11);
		zero.w = Mathf.Max(zero.w, num11);
		zero.x += num;
		zero.y += num;
		zero.z += num;
		zero.w += num;
		zero.x = Mathf.Min(zero.x, 1f);
		zero.y = Mathf.Min(zero.y, 1f);
		zero.z = Mathf.Min(zero.z, 1f);
		zero.w = Mathf.Min(zero.w, 1f);
		zero2.x = ((zero2.x < zero.x) ? zero.x : zero2.x);
		zero2.y = ((zero2.y < zero.y) ? zero.y : zero2.y);
		zero2.z = ((zero2.z < zero.z) ? zero.z : zero2.z);
		zero2.w = ((zero2.w < zero.w) ? zero.w : zero2.w);
		num10 = material.GetFloat(ID_GradientScale);
		zero *= num10;
		num11 = Mathf.Max(zero.x, zero.y);
		num11 = Mathf.Max(zero.z, num11);
		num11 = Mathf.Max(zero.w, num11);
		return num11 + 1.25f;
	}

	private static float ComputePaddingForProperties(Material mat)
	{
		Vector4 vector = mat.GetVector(ID_IsoPerimeter);
		Vector2 vector2 = mat.GetVector(ID_OutlineOffset1);
		Vector2 vector3 = mat.GetVector(ID_OutlineOffset2);
		Vector2 vector4 = mat.GetVector(ID_OutlineOffset3);
		bool flag = mat.GetFloat(ID_OutlineMode) != 0f;
		Vector4 vector5 = mat.GetVector(ID_Softness);
		float num = mat.GetFloat(ID_GradientScale);
		float a = Mathf.Max(0f, vector.x + vector5.x * 0.5f);
		if (!flag)
		{
			a = Mathf.Max(a, vector.y + vector5.y * 0.5f + Mathf.Max(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y)));
			a = Mathf.Max(a, vector.z + vector5.z * 0.5f + Mathf.Max(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y)));
			a = Mathf.Max(a, vector.w + vector5.w * 0.5f + Mathf.Max(Mathf.Abs(vector4.x), Mathf.Abs(vector4.y)));
		}
		else
		{
			float num2 = Mathf.Max(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y));
			float num3 = Mathf.Max(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y));
			a = Mathf.Max(a, vector.y + vector5.y * 0.5f + num2);
			a = Mathf.Max(a, vector.z + vector5.z * 0.5f + num3);
			float num4 = Mathf.Max(num2, num3);
			a += Mathf.Max(0f, vector.w + vector5.w * 0.5f - Mathf.Max(0f, a - num4));
		}
		Vector2 vector6 = mat.GetVector(ID_UnderlayOffset);
		float num5 = mat.GetFloat(ID_UnderlayDilate);
		float num6 = mat.GetFloat(ID_UnderlaySoftness);
		a = Mathf.Max(a, num5 + num6 * 0.5f + Mathf.Max(Mathf.Abs(vector6.x), Mathf.Abs(vector6.y)));
		return a * num;
	}

	internal static float GetPadding(Material[] materials, bool enableExtraPadding, bool isBold)
	{
		if (!isInitialized)
		{
			GetShaderPropertyIDs();
		}
		if (materials == null)
		{
			return 0f;
		}
		int num = (enableExtraPadding ? 4 : 0);
		if (materials[0].HasProperty(ID_Padding))
		{
			return (float)num + materials[0].GetFloat(ID_Padding);
		}
		Vector4 zero = Vector4.zero;
		Vector4 zero2 = Vector4.zero;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		for (int i = 0; i < materials.Length; i++)
		{
			UpdateShaderRatios(materials[i]);
			string[] shaderKeywords = materials[i].shaderKeywords;
			if (materials[i].HasProperty(ID_ScaleRatio_A))
			{
				num5 = materials[i].GetFloat(ID_ScaleRatio_A);
			}
			if (materials[i].HasProperty(ID_FaceDilate))
			{
				num2 = materials[i].GetFloat(ID_FaceDilate) * num5;
			}
			if (materials[i].HasProperty(ID_OutlineSoftness))
			{
				num3 = materials[i].GetFloat(ID_OutlineSoftness) * num5;
			}
			if (materials[i].HasProperty(ID_OutlineWidth))
			{
				num4 = materials[i].GetFloat(ID_OutlineWidth) * num5;
			}
			num10 = num4 + num3 + num2;
			if (materials[i].HasProperty(ID_GlowOffset) && shaderKeywords.Contains(Keyword_Glow))
			{
				if (materials[i].HasProperty(ID_ScaleRatio_B))
				{
					num6 = materials[i].GetFloat(ID_ScaleRatio_B);
				}
				num8 = materials[i].GetFloat(ID_GlowOffset) * num6;
				num9 = materials[i].GetFloat(ID_GlowOuter) * num6;
			}
			num10 = Mathf.Max(num10, num2 + num8 + num9);
			if (materials[i].HasProperty(ID_UnderlaySoftness) && shaderKeywords.Contains(Keyword_Underlay))
			{
				if (materials[i].HasProperty(ID_ScaleRatio_C))
				{
					num7 = materials[i].GetFloat(ID_ScaleRatio_C);
				}
				float num11 = materials[i].GetFloat(ID_UnderlayOffsetX) * num7;
				float num12 = materials[i].GetFloat(ID_UnderlayOffsetY) * num7;
				float num13 = materials[i].GetFloat(ID_UnderlayDilate) * num7;
				float num14 = materials[i].GetFloat(ID_UnderlaySoftness) * num7;
				zero.x = Mathf.Max(zero.x, num2 + num13 + num14 - num11);
				zero.y = Mathf.Max(zero.y, num2 + num13 + num14 - num12);
				zero.z = Mathf.Max(zero.z, num2 + num13 + num14 + num11);
				zero.w = Mathf.Max(zero.w, num2 + num13 + num14 + num12);
			}
			zero.x = Mathf.Max(zero.x, num10);
			zero.y = Mathf.Max(zero.y, num10);
			zero.z = Mathf.Max(zero.z, num10);
			zero.w = Mathf.Max(zero.w, num10);
			zero.x += num;
			zero.y += num;
			zero.z += num;
			zero.w += num;
			zero.x = Mathf.Min(zero.x, 1f);
			zero.y = Mathf.Min(zero.y, 1f);
			zero.z = Mathf.Min(zero.z, 1f);
			zero.w = Mathf.Min(zero.w, 1f);
			zero2.x = ((zero2.x < zero.x) ? zero.x : zero2.x);
			zero2.y = ((zero2.y < zero.y) ? zero.y : zero2.y);
			zero2.z = ((zero2.z < zero.z) ? zero.z : zero2.z);
			zero2.w = ((zero2.w < zero.w) ? zero.w : zero2.w);
		}
		float num15 = materials[0].GetFloat(ID_GradientScale);
		zero *= num15;
		num10 = Mathf.Max(zero.x, zero.y);
		num10 = Mathf.Max(zero.z, num10);
		num10 = Mathf.Max(zero.w, num10);
		return num10 + 0.25f;
	}
}
[Serializable]
public class TextStyle
{
	internal static TextStyle k_NormalStyle;

	[SerializeField]
	private string m_Name;

	[SerializeField]
	private int m_HashCode;

	[SerializeField]
	private string m_OpeningDefinition;

	[SerializeField]
	private string m_ClosingDefinition;

	[SerializeField]
	private uint[] m_OpeningTagArray;

	[SerializeField]
	private uint[] m_ClosingTagArray;

	[SerializeField]
	internal uint[] m_OpeningTagUnicodeArray;

	[SerializeField]
	internal uint[] m_ClosingTagUnicodeArray;

	public static TextStyle NormalStyle
	{
		get
		{
			if (k_NormalStyle == null)
			{
				k_NormalStyle = new TextStyle("Normal", string.Empty, string.Empty);
			}
			return k_NormalStyle;
		}
	}

	public string name
	{
		get
		{
			return m_Name;
		}
		set
		{
			if (value != m_Name)
			{
				m_Name = value;
			}
		}
	}

	public int hashCode
	{
		get
		{
			return m_HashCode;
		}
		set
		{
			if (value != m_HashCode)
			{
				m_HashCode = value;
			}
		}
	}

	public string styleOpeningDefinition => m_OpeningDefinition;

	public string styleClosingDefinition => m_ClosingDefinition;

	public uint[] styleOpeningTagArray => m_OpeningTagArray;

	public uint[] styleClosingTagArray => m_ClosingTagArray;

	internal TextStyle(string styleName, string styleOpeningDefinition, string styleClosingDefinition)
	{
		m_Name = styleName;
		m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(styleName);
		m_OpeningDefinition = styleOpeningDefinition;
		m_ClosingDefinition = styleClosingDefinition;
		RefreshStyle();
	}

	public void RefreshStyle()
	{
		m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(m_Name);
		int length = m_OpeningDefinition.Length;
		m_OpeningTagArray = new uint[length];
		m_OpeningTagUnicodeArray = new uint[length];
		for (int i = 0; i < length; i++)
		{
			m_OpeningTagArray[i] = m_OpeningDefinition[i];
			m_OpeningTagUnicodeArray[i] = m_OpeningDefinition[i];
		}
		int length2 = m_ClosingDefinition.Length;
		m_ClosingTagArray = new uint[length2];
		m_ClosingTagUnicodeArray = new uint[length2];
		for (int j = 0; j < length2; j++)
		{
			m_ClosingTagArray[j] = m_ClosingDefinition[j];
			m_ClosingTagUnicodeArray[j] = m_ClosingDefinition[j];
		}
	}
}
[Serializable]
[ExcludeFromObjectFactory]
[ExcludeFromPreset]
public class TextStyleSheet : ScriptableObject
{
	[SerializeField]
	private List<TextStyle> m_StyleList = new List<TextStyle>(1);

	private Dictionary<int, TextStyle> m_StyleLookupDictionary;

	internal List<TextStyle> styles => m_StyleList;

	private void Reset()
	{
		LoadStyleDictionaryInternal();
	}

	public TextStyle GetStyle(int hashCode)
	{
		if (m_StyleLookupDictionary == null)
		{
			LoadStyleDictionaryInternal();
		}
		if (m_StyleLookupDictionary.TryGetValue(hashCode, out var value))
		{
			return value;
		}
		return null;
	}

	public TextStyle GetStyle(string name)
	{
		if (m_StyleLookupDictionary == null)
		{
			LoadStyleDictionaryInternal();
		}
		int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive(name);
		if (m_StyleLookupDictionary.TryGetValue(hashCodeCaseInSensitive, out var value))
		{
			return value;
		}
		return null;
	}

	public void RefreshStyles()
	{
		LoadStyleDictionaryInternal();
	}

	private void LoadStyleDictionaryInternal()
	{
		if (m_StyleLookupDictionary == null)
		{
			m_StyleLookupDictionary = new Dictionary<int, TextStyle>();
		}
		else
		{
			m_StyleLookupDictionary.Clear();
		}
		for (int i = 0; i < m_StyleList.Count; i++)
		{
			m_StyleList[i].RefreshStyle();
			if (!m_StyleLookupDictionary.ContainsKey(m_StyleList[i].hashCode))
			{
				m_StyleLookupDictionary.Add(m_StyleList[i].hashCode, m_StyleList[i]);
			}
		}
		int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive("Normal");
		if (!m_StyleLookupDictionary.ContainsKey(hashCodeCaseInSensitive))
		{
			TextStyle textStyle = new TextStyle("Normal", string.Empty, string.Empty);
			m_StyleList.Add(textStyle);
			m_StyleLookupDictionary.Add(hashCodeCaseInSensitive, textStyle);
		}
	}
}
internal static class TextUtilities
{
	private const string k_LookupStringL = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-";

	private const string k_LookupStringU = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-";

	internal static void ResizeArray<T>(ref T[] array)
	{
		int newSize = NextPowerOfTwo(array.Length);
		Array.Resize(ref array, newSize);
	}

	internal static void ResizeArray<T>(ref T[] array, int size)
	{
		size = NextPowerOfTwo(size);
		Array.Resize(ref array, size);
	}

	internal static int NextPowerOfTwo(int v)
	{
		v |= v >> 16;
		v |= v >> 8;
		v |= v >> 4;
		v |= v >> 2;
		v |= v >> 1;
		return v + 1;
	}

	internal static char ToLowerFast(char c)
	{
		if (c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1)
		{
			return c;
		}
		return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[c];
	}

	internal static char ToUpperFast(char c)
	{
		if (c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1)
		{
			return c;
		}
		return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[c];
	}

	internal static uint ToUpperASCIIFast(uint c)
	{
		if (c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1)
		{
			return c;
		}
		return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
	}

	internal static uint ToLowerASCIIFast(uint c)
	{
		if (c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1)
		{
			return c;
		}
		return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[(int)c];
	}

	public static int GetHashCodeCaseSensitive(string s)
	{
		int num = 0;
		for (int i = 0; i < s.Length; i++)
		{
			num = ((num << 5) + num) ^ s[i];
		}
		return num;
	}

	public static int GetHashCodeCaseInSensitive(string s)
	{
		int num = 0;
		for (int i = 0; i < s.Length; i++)
		{
			num = ((num << 5) + num) ^ ToUpperFast(s[i]);
		}
		return num;
	}

	public static uint GetSimpleHashCodeLowercase(string s)
	{
		uint num = 0u;
		for (int i = 0; i < s.Length; i++)
		{
			num = ((num << 5) + num) ^ ToLowerFast(s[i]);
		}
		return num;
	}

	internal static uint ConvertToUTF32(uint highSurrogate, uint lowSurrogate)
	{
		return (highSurrogate - 55296) * 1024 + (lowSurrogate - 56320 + 65536);
	}

	internal static uint ReadUTF16(uint[] text, int index)
	{
		uint num = 0u;
		num += HexToInt((char)text[index]) << 12;
		num += HexToInt((char)text[index + 1]) << 8;
		num += HexToInt((char)text[index + 2]) << 4;
		return num + HexToInt((char)text[index + 3]);
	}

	internal static uint ReadUTF32(uint[] text, int index)
	{
		uint num = 0u;
		num += HexToInt((char)text[index]) << 30;
		num += HexToInt((char)text[index + 1]) << 24;
		num += HexToInt((char)text[index + 2]) << 20;
		num += HexToInt((char)text[index + 3]) << 16;
		num += HexToInt((char)text[index + 4]) << 12;
		num += HexToInt((char)text[index + 5]) << 8;
		num += HexToInt((char)text[index + 6]) << 4;
		return num + HexToInt((char)text[index + 7]);
	}

	private static uint HexToInt(char hex)
	{
		return hex switch
		{
			'0' => 0u, 
			'1' => 1u, 
			'2' => 2u, 
			'3' => 3u, 
			'4' => 4u, 
			'5' => 5u, 
			'6' => 6u, 
			'7' => 7u, 
			'8' => 8u, 
			'9' => 9u, 
			'A' => 10u, 
			'B' => 11u, 
			'C' => 12u, 
			'D' => 13u, 
			'E' => 14u, 
			'F' => 15u, 
			'a' => 10u, 
			'b' => 11u, 
			'c' => 12u, 
			'd' => 13u, 
			'e' => 14u, 
			'f' => 15u, 
			_ => 15u, 
		};
	}

	public static uint StringHexToInt(string s)
	{
		uint num = 0u;
		int length = s.Length;
		for (int i = 0; i < length; i++)
		{
			num += HexToInt(s[i]) * (uint)Mathf.Pow(16f, length - 1 - i);
		}
		return num;
	}

	internal static string UintToString(this List<uint> unicodes)
	{
		char[] array = new char[unicodes.Count];
		for (int i = 0; i < unicodes.Count; i++)
		{
			array[i] = (char)unicodes[i];
		}
		return new string(array);
	}
}
[Serializable]
public class UnicodeLineBreakingRules
{
	[SerializeField]
	private UnityEngine.TextAsset m_UnicodeLineBreakingRules;

	[SerializeField]
	private UnityEngine.TextAsset m_LeadingCharacters;

	[SerializeField]
	private UnityEngine.TextAsset m_FollowingCharacters;

	[SerializeField]
	private bool m_UseModernHangulLineBreakingRules;

	private HashSet<uint> m_LeadingCharactersLookup;

	private HashSet<uint> m_FollowingCharactersLookup;

	public UnityEngine.TextAsset lineBreakingRules => m_UnicodeLineBreakingRules;

	public UnityEngine.TextAsset leadingCharacters => m_LeadingCharacters;

	public UnityEngine.TextAsset followingCharacters => m_FollowingCharacters;

	internal HashSet<uint> leadingCharactersLookup
	{
		get
		{
			if (m_LeadingCharactersLookup == null)
			{
				LoadLineBreakingRules(leadingCharacters, followingCharacters);
			}
			return m_LeadingCharactersLookup;
		}
		set
		{
			m_LeadingCharactersLookup = value;
		}
	}

	internal HashSet<uint> followingCharactersLookup
	{
		get
		{
			if (m_LeadingCharactersLookup == null)
			{
				LoadLineBreakingRules(leadingCharacters, followingCharacters);
			}
			return m_FollowingCharactersLookup;
		}
		set
		{
			m_FollowingCharactersLookup = value;
		}
	}

	public bool useModernHangulLineBreakingRules
	{
		get
		{
			return m_UseModernHangulLineBreakingRules;
		}
		set
		{
			m_UseModernHangulLineBreakingRules = value;
		}
	}

	internal void LoadLineBreakingRules()
	{
		if (m_LeadingCharactersLookup == null)
		{
			if (m_LeadingCharacters == null)
			{
				m_LeadingCharacters = Resources.Load<UnityEngine.TextAsset>("LineBreaking Leading Characters");
			}
			m_LeadingCharactersLookup = ((m_LeadingCharacters != null) ? GetCharacters(m_LeadingCharacters) : new HashSet<uint>());
			if (m_FollowingCharacters == null)
			{
				m_FollowingCharacters = Resources.Load<UnityEngine.TextAsset>("LineBreaking Following Characters");
			}
			m_FollowingCharactersLookup = ((m_FollowingCharacters != null) ? GetCharacters(m_FollowingCharacters) : new HashSet<uint>());
		}
	}

	internal void LoadLineBreakingRules(UnityEngine.TextAsset leadingRules, UnityEngine.TextAsset followingRules)
	{
		if (m_LeadingCharactersLookup == null)
		{
			if (leadingRules == null)
			{
				leadingRules = Resources.Load<UnityEngine.TextAsset>("LineBreaking Leading Characters");
			}
			m_LeadingCharactersLookup = ((leadingRules != null) ? GetCharacters(leadingRules) : new HashSet<uint>());
			if (followingRules == null)
			{
				followingRules = Resources.Load<UnityEngine.TextAsset>("LineBreaking Following Characters");
			}
			m_FollowingCharactersLookup = ((followingRules != null) ? GetCharacters(followingRules) : new HashSet<uint>());
		}
	}

	private static HashSet<uint> GetCharacters(UnityEngine.TextAsset file)
	{
		HashSet<uint> hashSet = new HashSet<uint>();
		string text = file.text;
		for (int i = 0; i < text.Length; i++)
		{
			hashSet.Add(text[i]);
		}
		return hashSet;
	}
}
