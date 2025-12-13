using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngine.TextCore.LowLevel;

[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.TextCore.FontEngine.Tests")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.TextCore.Tests")]
[assembly: InternalsVisibleTo("Unity.FontEngine.Tests")]
[assembly: InternalsVisibleTo("Unity.TextCore.FontEngine.Tools")]
[assembly: InternalsVisibleTo("Unity.TextCore.FontEngine")]
[assembly: InternalsVisibleTo("Unity.TextMeshPro")]
[assembly: InternalsVisibleTo("Unity.TextCore")]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine.TextCore
{
	[Serializable]
	[UsedByNativeCode]
	public struct FaceInfo
	{
		[SerializeField]
		[NativeName("faceIndex")]
		private int m_FaceIndex;

		[SerializeField]
		[NativeName("familyName")]
		private string m_FamilyName;

		[SerializeField]
		[NativeName("styleName")]
		private string m_StyleName;

		[SerializeField]
		[NativeName("pointSize")]
		private int m_PointSize;

		[NativeName("scale")]
		[SerializeField]
		private float m_Scale;

		[SerializeField]
		[NativeName("unitsPerEM")]
		private int m_UnitsPerEM;

		[NativeName("lineHeight")]
		[SerializeField]
		private float m_LineHeight;

		[NativeName("ascentLine")]
		[SerializeField]
		private float m_AscentLine;

		[NativeName("capLine")]
		[SerializeField]
		private float m_CapLine;

		[SerializeField]
		[NativeName("meanLine")]
		private float m_MeanLine;

		[SerializeField]
		[NativeName("baseline")]
		private float m_Baseline;

		[NativeName("descentLine")]
		[SerializeField]
		private float m_DescentLine;

		[NativeName("superscriptOffset")]
		[SerializeField]
		private float m_SuperscriptOffset;

		[SerializeField]
		[NativeName("superscriptSize")]
		private float m_SuperscriptSize;

		[SerializeField]
		[NativeName("subscriptOffset")]
		private float m_SubscriptOffset;

		[SerializeField]
		[NativeName("subscriptSize")]
		private float m_SubscriptSize;

		[NativeName("underlineOffset")]
		[SerializeField]
		private float m_UnderlineOffset;

		[SerializeField]
		[NativeName("underlineThickness")]
		private float m_UnderlineThickness;

		[SerializeField]
		[NativeName("strikethroughOffset")]
		private float m_StrikethroughOffset;

		[SerializeField]
		[NativeName("strikethroughThickness")]
		private float m_StrikethroughThickness;

		[SerializeField]
		[NativeName("tabWidth")]
		private float m_TabWidth;

		internal int faceIndex
		{
			get
			{
				return m_FaceIndex;
			}
			set
			{
				m_FaceIndex = value;
			}
		}

		public string familyName
		{
			get
			{
				return m_FamilyName;
			}
			set
			{
				m_FamilyName = value;
			}
		}

		public string styleName
		{
			get
			{
				return m_StyleName;
			}
			set
			{
				m_StyleName = value;
			}
		}

		public int pointSize
		{
			get
			{
				return m_PointSize;
			}
			set
			{
				m_PointSize = value;
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

		internal int unitsPerEM
		{
			get
			{
				return m_UnitsPerEM;
			}
			set
			{
				m_UnitsPerEM = value;
			}
		}

		public float lineHeight
		{
			get
			{
				return m_LineHeight;
			}
			set
			{
				m_LineHeight = value;
			}
		}

		public float ascentLine
		{
			get
			{
				return m_AscentLine;
			}
			set
			{
				m_AscentLine = value;
			}
		}

		public float capLine
		{
			get
			{
				return m_CapLine;
			}
			set
			{
				m_CapLine = value;
			}
		}

		public float meanLine
		{
			get
			{
				return m_MeanLine;
			}
			set
			{
				m_MeanLine = value;
			}
		}

		public float baseline
		{
			get
			{
				return m_Baseline;
			}
			set
			{
				m_Baseline = value;
			}
		}

		public float descentLine
		{
			get
			{
				return m_DescentLine;
			}
			set
			{
				m_DescentLine = value;
			}
		}

		public float superscriptOffset
		{
			get
			{
				return m_SuperscriptOffset;
			}
			set
			{
				m_SuperscriptOffset = value;
			}
		}

		public float superscriptSize
		{
			get
			{
				return m_SuperscriptSize;
			}
			set
			{
				m_SuperscriptSize = value;
			}
		}

		public float subscriptOffset
		{
			get
			{
				return m_SubscriptOffset;
			}
			set
			{
				m_SubscriptOffset = value;
			}
		}

		public float subscriptSize
		{
			get
			{
				return m_SubscriptSize;
			}
			set
			{
				m_SubscriptSize = value;
			}
		}

		public float underlineOffset
		{
			get
			{
				return m_UnderlineOffset;
			}
			set
			{
				m_UnderlineOffset = value;
			}
		}

		public float underlineThickness
		{
			get
			{
				return m_UnderlineThickness;
			}
			set
			{
				m_UnderlineThickness = value;
			}
		}

		public float strikethroughOffset
		{
			get
			{
				return m_StrikethroughOffset;
			}
			set
			{
				m_StrikethroughOffset = value;
			}
		}

		public float strikethroughThickness
		{
			get
			{
				return m_StrikethroughThickness;
			}
			set
			{
				m_StrikethroughThickness = value;
			}
		}

		public float tabWidth
		{
			get
			{
				return m_TabWidth;
			}
			set
			{
				m_TabWidth = value;
			}
		}

		internal FaceInfo(string familyName, string styleName, int pointSize, float scale, int unitsPerEM, float lineHeight, float ascentLine, float capLine, float meanLine, float baseline, float descentLine, float superscriptOffset, float superscriptSize, float subscriptOffset, float subscriptSize, float underlineOffset, float underlineThickness, float strikethroughOffset, float strikethroughThickness, float tabWidth)
		{
			m_FaceIndex = 0;
			m_FamilyName = familyName;
			m_StyleName = styleName;
			m_PointSize = pointSize;
			m_Scale = scale;
			m_UnitsPerEM = unitsPerEM;
			m_LineHeight = lineHeight;
			m_AscentLine = ascentLine;
			m_CapLine = capLine;
			m_MeanLine = meanLine;
			m_Baseline = baseline;
			m_DescentLine = descentLine;
			m_SuperscriptOffset = superscriptOffset;
			m_SuperscriptSize = superscriptSize;
			m_SubscriptOffset = subscriptOffset;
			m_SubscriptSize = subscriptSize;
			m_UnderlineOffset = underlineOffset;
			m_UnderlineThickness = underlineThickness;
			m_StrikethroughOffset = strikethroughOffset;
			m_StrikethroughThickness = strikethroughThickness;
			m_TabWidth = tabWidth;
		}

		public bool Compare(FaceInfo other)
		{
			return familyName == other.familyName && styleName == other.styleName && faceIndex == other.faceIndex && pointSize == other.pointSize && FontEngineUtilities.Approximately(scale, other.scale) && FontEngineUtilities.Approximately(unitsPerEM, other.unitsPerEM) && FontEngineUtilities.Approximately(lineHeight, other.lineHeight) && FontEngineUtilities.Approximately(ascentLine, other.ascentLine) && FontEngineUtilities.Approximately(capLine, other.capLine) && FontEngineUtilities.Approximately(meanLine, other.meanLine) && FontEngineUtilities.Approximately(baseline, other.baseline) && FontEngineUtilities.Approximately(descentLine, other.descentLine) && FontEngineUtilities.Approximately(superscriptOffset, other.superscriptOffset) && FontEngineUtilities.Approximately(superscriptSize, other.superscriptSize) && FontEngineUtilities.Approximately(subscriptOffset, other.subscriptOffset) && FontEngineUtilities.Approximately(subscriptSize, other.subscriptSize) && FontEngineUtilities.Approximately(underlineOffset, other.underlineOffset) && FontEngineUtilities.Approximately(underlineThickness, other.underlineThickness) && FontEngineUtilities.Approximately(strikethroughOffset, other.strikethroughOffset) && FontEngineUtilities.Approximately(strikethroughThickness, other.strikethroughThickness) && FontEngineUtilities.Approximately(tabWidth, other.tabWidth);
		}
	}
	public enum GlyphClassDefinitionType
	{
		Undefined,
		Base,
		Ligature,
		Mark,
		Component
	}
	[Serializable]
	[UsedByNativeCode]
	public struct GlyphRect : IEquatable<GlyphRect>
	{
		[NativeName("x")]
		[SerializeField]
		private int m_X;

		[NativeName("y")]
		[SerializeField]
		private int m_Y;

		[SerializeField]
		[NativeName("width")]
		private int m_Width;

		[SerializeField]
		[NativeName("height")]
		private int m_Height;

		private static readonly GlyphRect s_ZeroGlyphRect = new GlyphRect(0, 0, 0, 0);

		public int x
		{
			get
			{
				return m_X;
			}
			set
			{
				m_X = value;
			}
		}

		public int y
		{
			get
			{
				return m_Y;
			}
			set
			{
				m_Y = value;
			}
		}

		public int width
		{
			get
			{
				return m_Width;
			}
			set
			{
				m_Width = value;
			}
		}

		public int height
		{
			get
			{
				return m_Height;
			}
			set
			{
				m_Height = value;
			}
		}

		public static GlyphRect zero => s_ZeroGlyphRect;

		public GlyphRect(int x, int y, int width, int height)
		{
			m_X = x;
			m_Y = y;
			m_Width = width;
			m_Height = height;
		}

		public GlyphRect(Rect rect)
		{
			m_X = (int)rect.x;
			m_Y = (int)rect.y;
			m_Width = (int)rect.width;
			m_Height = (int)rect.height;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool Equals(GlyphRect other)
		{
			return base.Equals((object)other);
		}

		public static bool operator ==(GlyphRect lhs, GlyphRect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		public static bool operator !=(GlyphRect lhs, GlyphRect rhs)
		{
			return !(lhs == rhs);
		}
	}
	[Serializable]
	[UsedByNativeCode]
	public struct GlyphMetrics : IEquatable<GlyphMetrics>
	{
		[NativeName("width")]
		[SerializeField]
		private float m_Width;

		[SerializeField]
		[NativeName("height")]
		private float m_Height;

		[SerializeField]
		[NativeName("horizontalBearingX")]
		private float m_HorizontalBearingX;

		[NativeName("horizontalBearingY")]
		[SerializeField]
		private float m_HorizontalBearingY;

		[NativeName("horizontalAdvance")]
		[SerializeField]
		private float m_HorizontalAdvance;

		public float width
		{
			get
			{
				return m_Width;
			}
			set
			{
				m_Width = value;
			}
		}

		public float height
		{
			get
			{
				return m_Height;
			}
			set
			{
				m_Height = value;
			}
		}

		public float horizontalBearingX
		{
			get
			{
				return m_HorizontalBearingX;
			}
			set
			{
				m_HorizontalBearingX = value;
			}
		}

		public float horizontalBearingY
		{
			get
			{
				return m_HorizontalBearingY;
			}
			set
			{
				m_HorizontalBearingY = value;
			}
		}

		public float horizontalAdvance
		{
			get
			{
				return m_HorizontalAdvance;
			}
			set
			{
				m_HorizontalAdvance = value;
			}
		}

		public GlyphMetrics(float width, float height, float bearingX, float bearingY, float advance)
		{
			m_Width = width;
			m_Height = height;
			m_HorizontalBearingX = bearingX;
			m_HorizontalBearingY = bearingY;
			m_HorizontalAdvance = advance;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool Equals(GlyphMetrics other)
		{
			return base.Equals((object)other);
		}

		public static bool operator ==(GlyphMetrics lhs, GlyphMetrics rhs)
		{
			return lhs.width == rhs.width && lhs.height == rhs.height && lhs.horizontalBearingX == rhs.horizontalBearingX && lhs.horizontalBearingY == rhs.horizontalBearingY && lhs.horizontalAdvance == rhs.horizontalAdvance;
		}

		public static bool operator !=(GlyphMetrics lhs, GlyphMetrics rhs)
		{
			return !(lhs == rhs);
		}
	}
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	[UsedByNativeCode]
	public class Glyph
	{
		[NativeName("index")]
		[SerializeField]
		private uint m_Index;

		[NativeName("metrics")]
		[SerializeField]
		private GlyphMetrics m_Metrics;

		[NativeName("glyphRect")]
		[SerializeField]
		private GlyphRect m_GlyphRect;

		[SerializeField]
		[NativeName("scale")]
		private float m_Scale;

		[SerializeField]
		[NativeName("atlasIndex")]
		private int m_AtlasIndex;

		[NativeName("type")]
		[SerializeField]
		private GlyphClassDefinitionType m_ClassDefinitionType;

		public uint index
		{
			get
			{
				return m_Index;
			}
			set
			{
				m_Index = value;
			}
		}

		public GlyphMetrics metrics
		{
			get
			{
				return m_Metrics;
			}
			set
			{
				m_Metrics = value;
			}
		}

		public GlyphRect glyphRect
		{
			get
			{
				return m_GlyphRect;
			}
			set
			{
				m_GlyphRect = value;
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

		public int atlasIndex
		{
			get
			{
				return m_AtlasIndex;
			}
			set
			{
				m_AtlasIndex = value;
			}
		}

		public GlyphClassDefinitionType classDefinitionType
		{
			get
			{
				return m_ClassDefinitionType;
			}
			set
			{
				m_ClassDefinitionType = value;
			}
		}

		public Glyph()
		{
			m_Index = 0u;
			m_Metrics = default(GlyphMetrics);
			m_GlyphRect = default(GlyphRect);
			m_Scale = 1f;
			m_AtlasIndex = 0;
		}

		public Glyph(Glyph glyph)
		{
			m_Index = glyph.index;
			m_Metrics = glyph.metrics;
			m_GlyphRect = glyph.glyphRect;
			m_Scale = glyph.scale;
			m_AtlasIndex = glyph.atlasIndex;
		}

		internal Glyph(GlyphMarshallingStruct glyphStruct)
		{
			m_Index = glyphStruct.index;
			m_Metrics = glyphStruct.metrics;
			m_GlyphRect = glyphStruct.glyphRect;
			m_Scale = glyphStruct.scale;
			m_AtlasIndex = glyphStruct.atlasIndex;
		}

		public Glyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect)
		{
			m_Index = index;
			m_Metrics = metrics;
			m_GlyphRect = glyphRect;
			m_Scale = 1f;
			m_AtlasIndex = 0;
		}

		public Glyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			m_Index = index;
			m_Metrics = metrics;
			m_GlyphRect = glyphRect;
			m_Scale = scale;
			m_AtlasIndex = atlasIndex;
		}

		public bool Compare(Glyph other)
		{
			return index == other.index && metrics == other.metrics && glyphRect == other.glyphRect && scale == other.scale && atlasIndex == other.atlasIndex;
		}
	}
}
namespace UnityEngine.TextCore.LowLevel
{
	[UsedByNativeCode]
	[Flags]
	public enum GlyphLoadFlags
	{
		LOAD_DEFAULT = 0,
		LOAD_NO_SCALE = 1,
		LOAD_NO_HINTING = 2,
		LOAD_RENDER = 4,
		LOAD_NO_BITMAP = 8,
		LOAD_FORCE_AUTOHINT = 0x20,
		LOAD_MONOCHROME = 0x1000,
		LOAD_NO_AUTOHINT = 0x8000,
		LOAD_COLOR = 0x100000,
		LOAD_COMPUTE_METRICS = 0x200000,
		LOAD_BITMAP_METRICS_ONLY = 0x400000
	}
	[Flags]
	internal enum GlyphRasterModes
	{
		RASTER_MODE_8BIT = 1,
		RASTER_MODE_MONO = 2,
		RASTER_MODE_NO_HINTING = 4,
		RASTER_MODE_HINTED = 8,
		RASTER_MODE_BITMAP = 0x10,
		RASTER_MODE_SDF = 0x20,
		RASTER_MODE_SDFAA = 0x40,
		RASTER_MODE_MSDF = 0x100,
		RASTER_MODE_MSDFA = 0x200,
		RASTER_MODE_1X = 0x1000,
		RASTER_MODE_8X = 0x2000,
		RASTER_MODE_16X = 0x4000,
		RASTER_MODE_32X = 0x8000,
		RASTER_MODE_COLOR = 0x10000
	}
	public enum FontEngineError
	{
		Success = 0,
		Invalid_File_Path = 1,
		Invalid_File_Format = 2,
		Invalid_File_Structure = 3,
		Invalid_File = 4,
		Invalid_Table = 8,
		Invalid_Glyph_Index = 16,
		Invalid_Character_Code = 17,
		Invalid_Pixel_Size = 23,
		Invalid_Library = 33,
		Invalid_Face = 35,
		Invalid_Library_or_Face = 41,
		Atlas_Generation_Cancelled = 100,
		Invalid_SharedTextureData = 101,
		OpenTypeLayoutLookup_Mismatch = 116
	}
	[UsedByNativeCode]
	public enum GlyphRenderMode
	{
		SMOOTH_HINTED = 4121,
		SMOOTH = 4117,
		COLOR_HINTED = 69656,
		COLOR = 69652,
		RASTER_HINTED = 4122,
		RASTER = 4118,
		SDF = 4134,
		SDF8 = 8230,
		SDF16 = 16422,
		SDF32 = 32806,
		SDFAA_HINTED = 4169,
		SDFAA = 4165
	}
	[UsedByNativeCode]
	public enum GlyphPackingMode
	{
		BestShortSideFit,
		BestLongSideFit,
		BestAreaFit,
		BottomLeftRule,
		ContactPointRule
	}
	[DebuggerDisplay("{familyName} - {styleName}")]
	[UsedByNativeCode]
	internal struct FontReference
	{
		public string familyName;

		public string styleName;

		public int faceIndex;

		public string filePath;
	}
	[NativeHeader("Modules/TextCoreFontEngine/Native/FontEngine.h")]
	public sealed class FontEngine
	{
		private static Glyph[] s_Glyphs = new Glyph[16];

		private static uint[] s_GlyphIndexes_MarshallingArray_A;

		private static uint[] s_GlyphIndexes_MarshallingArray_B;

		private static GlyphMarshallingStruct[] s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[16];

		private static GlyphMarshallingStruct[] s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[16];

		private static GlyphRect[] s_FreeGlyphRects = new GlyphRect[16];

		private static GlyphRect[] s_UsedGlyphRects = new GlyphRect[16];

		private static GlyphAdjustmentRecord[] s_SingleAdjustmentRecords_MarshallingArray;

		private static SingleSubstitutionRecord[] s_SingleSubstitutionRecords_MarshallingArray;

		private static MultipleSubstitutionRecord[] s_MultipleSubstitutionRecords_MarshallingArray;

		private static AlternateSubstitutionRecord[] s_AlternateSubstitutionRecords_MarshallingArray;

		private static LigatureSubstitutionRecord[] s_LigatureSubstitutionRecords_MarshallingArray;

		private static ContextualSubstitutionRecord[] s_ContextualSubstitutionRecords_MarshallingArray;

		private static ChainingContextualSubstitutionRecord[] s_ChainingContextualSubstitutionRecords_MarshallingArray;

		private static GlyphPairAdjustmentRecord[] s_PairAdjustmentRecords_MarshallingArray;

		private static MarkToBaseAdjustmentRecord[] s_MarkToBaseAdjustmentRecords_MarshallingArray;

		private static MarkToMarkAdjustmentRecord[] s_MarkToMarkAdjustmentRecords_MarshallingArray;

		private static Dictionary<uint, Glyph> s_GlyphLookupDictionary = new Dictionary<uint, Glyph>();

		internal static extern bool isProcessingDone
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "TextCore::FontEngine::GetIsProcessingDone", IsFreeFunction = true)]
			get;
		}

		internal static extern float generationProgress
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "TextCore::FontEngine::GetGenerationProgress", IsFreeFunction = true)]
			get;
		}

		internal FontEngine()
		{
		}

		public static FontEngineError InitializeFontEngine()
		{
			return (FontEngineError)InitializeFontEngine_Internal();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::InitFontEngine", IsFreeFunction = true)]
		private static extern int InitializeFontEngine_Internal();

		public static FontEngineError DestroyFontEngine()
		{
			return (FontEngineError)DestroyFontEngine_Internal();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::DestroyFontEngine", IsFreeFunction = true)]
		private static extern int DestroyFontEngine_Internal();

		internal static void SendCancellationRequest()
		{
			SendCancellationRequest_Internal();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::SendCancellationRequest", IsFreeFunction = true)]
		private static extern void SendCancellationRequest_Internal();

		public static FontEngineError LoadFontFace(string filePath)
		{
			return (FontEngineError)LoadFontFace_Internal(filePath);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static extern int LoadFontFace_Internal(string filePath);

		public static FontEngineError LoadFontFace(string filePath, int pointSize)
		{
			return (FontEngineError)LoadFontFace_With_Size_Internal(filePath, pointSize);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static extern int LoadFontFace_With_Size_Internal(string filePath, int pointSize);

		public static FontEngineError LoadFontFace(string filePath, int pointSize, int faceIndex)
		{
			return (FontEngineError)LoadFontFace_With_Size_And_FaceIndex_Internal(filePath, pointSize, faceIndex);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static extern int LoadFontFace_With_Size_And_FaceIndex_Internal(string filePath, int pointSize, int faceIndex);

		public static FontEngineError LoadFontFace(byte[] sourceFontFile)
		{
			if (sourceFontFile.Length == 0)
			{
				return FontEngineError.Invalid_File;
			}
			return (FontEngineError)LoadFontFace_FromSourceFontFile_Internal(sourceFontFile);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static extern int LoadFontFace_FromSourceFontFile_Internal(byte[] sourceFontFile);

		public static FontEngineError LoadFontFace(byte[] sourceFontFile, int pointSize)
		{
			if (sourceFontFile.Length == 0)
			{
				return FontEngineError.Invalid_File;
			}
			return (FontEngineError)LoadFontFace_With_Size_FromSourceFontFile_Internal(sourceFontFile, pointSize);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static extern int LoadFontFace_With_Size_FromSourceFontFile_Internal(byte[] sourceFontFile, int pointSize);

		public static FontEngineError LoadFontFace(byte[] sourceFontFile, int pointSize, int faceIndex)
		{
			if (sourceFontFile.Length == 0)
			{
				return FontEngineError.Invalid_File;
			}
			return (FontEngineError)LoadFontFace_With_Size_And_FaceIndex_FromSourceFontFile_Internal(sourceFontFile, pointSize, faceIndex);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static extern int LoadFontFace_With_Size_And_FaceIndex_FromSourceFontFile_Internal(byte[] sourceFontFile, int pointSize, int faceIndex);

		public static FontEngineError LoadFontFace(Font font)
		{
			return (FontEngineError)LoadFontFace_FromFont_Internal(font);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static extern int LoadFontFace_FromFont_Internal(Font font);

		public static FontEngineError LoadFontFace(Font font, int pointSize)
		{
			return (FontEngineError)LoadFontFace_With_Size_FromFont_Internal(font, pointSize);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static extern int LoadFontFace_With_Size_FromFont_Internal(Font font, int pointSize);

		public static FontEngineError LoadFontFace(Font font, int pointSize, int faceIndex)
		{
			return (FontEngineError)LoadFontFace_With_Size_and_FaceIndex_FromFont_Internal(font, pointSize, faceIndex);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static extern int LoadFontFace_With_Size_and_FaceIndex_FromFont_Internal(Font font, int pointSize, int faceIndex);

		public static FontEngineError LoadFontFace(string familyName, string styleName)
		{
			return (FontEngineError)LoadFontFace_by_FamilyName_and_StyleName_Internal(familyName, styleName);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static extern int LoadFontFace_by_FamilyName_and_StyleName_Internal(string familyName, string styleName);

		public static FontEngineError LoadFontFace(string familyName, string styleName, int pointSize)
		{
			return (FontEngineError)LoadFontFace_With_Size_by_FamilyName_and_StyleName_Internal(familyName, styleName, pointSize);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static extern int LoadFontFace_With_Size_by_FamilyName_and_StyleName_Internal(string familyName, string styleName, int pointSize);

		public static FontEngineError UnloadFontFace()
		{
			return (FontEngineError)UnloadFontFace_Internal();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::UnloadFontFace", IsFreeFunction = true)]
		private static extern int UnloadFontFace_Internal();

		public static FontEngineError UnloadAllFontFaces()
		{
			return (FontEngineError)UnloadAllFontFaces_Internal();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::UnloadAllFontFaces", IsFreeFunction = true)]
		private static extern int UnloadAllFontFaces_Internal();

		public static string[] GetSystemFontNames()
		{
			string[] systemFontNames_Internal = GetSystemFontNames_Internal();
			if (systemFontNames_Internal != null && systemFontNames_Internal.Length == 0)
			{
				return null;
			}
			return systemFontNames_Internal;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetSystemFontNames", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern string[] GetSystemFontNames_Internal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetSystemFontReferences", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern FontReference[] GetSystemFontReferences();

		internal static bool TryGetSystemFontReference(string familyName, string styleName, out FontReference fontRef)
		{
			return TryGetSystemFontReference_Internal(familyName, styleName, out fontRef);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::TryGetSystemFontReference", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern bool TryGetSystemFontReference_Internal(string familyName, string styleName, out FontReference fontRef);

		public static FontEngineError SetFaceSize(int pointSize)
		{
			return (FontEngineError)SetFaceSize_Internal(pointSize);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::SetFaceSize", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern int SetFaceSize_Internal(int pointSize);

		public static FaceInfo GetFaceInfo()
		{
			FaceInfo faceInfo = default(FaceInfo);
			GetFaceInfo_Internal(ref faceInfo);
			return faceInfo;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetFaceInfo", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern int GetFaceInfo_Internal(ref FaceInfo faceInfo);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetFaceCount", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern int GetFaceCount();

		public static string[] GetFontFaces()
		{
			string[] fontFaces_Internal = GetFontFaces_Internal();
			if (fontFaces_Internal != null && fontFaces_Internal.Length == 0)
			{
				return null;
			}
			return fontFaces_Internal;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetFontFaces", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern string[] GetFontFaces_Internal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetVariantGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern uint GetVariantGlyphIndex(uint unicode, uint variantSelectorUnicode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern uint GetGlyphIndex(uint unicode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		public static extern bool TryGetGlyphIndex(uint unicode, out uint glyphIndex);

		internal static FontEngineError LoadGlyph(uint unicode, GlyphLoadFlags flags)
		{
			return (FontEngineError)LoadGlyph_Internal(unicode, flags);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::LoadGlyph", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern int LoadGlyph_Internal(uint unicode, GlyphLoadFlags loadFlags);

		public static bool TryGetGlyphWithUnicodeValue(uint unicode, GlyphLoadFlags flags, out Glyph glyph)
		{
			GlyphMarshallingStruct glyphStruct = default(GlyphMarshallingStruct);
			if (TryGetGlyphWithUnicodeValue_Internal(unicode, flags, ref glyphStruct))
			{
				glyph = new Glyph(glyphStruct);
				return true;
			}
			glyph = null;
			return false;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphWithUnicodeValue", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern bool TryGetGlyphWithUnicodeValue_Internal(uint unicode, GlyphLoadFlags loadFlags, ref GlyphMarshallingStruct glyphStruct);

		public static bool TryGetGlyphWithIndexValue(uint glyphIndex, GlyphLoadFlags flags, out Glyph glyph)
		{
			GlyphMarshallingStruct glyphStruct = default(GlyphMarshallingStruct);
			if (TryGetGlyphWithIndexValue_Internal(glyphIndex, flags, ref glyphStruct))
			{
				glyph = new Glyph(glyphStruct);
				return true;
			}
			glyph = null;
			return false;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphWithIndexValue", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern bool TryGetGlyphWithIndexValue_Internal(uint glyphIndex, GlyphLoadFlags loadFlags, ref GlyphMarshallingStruct glyphStruct);

		internal static bool TryPackGlyphInAtlas(Glyph glyph, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects)
		{
			GlyphMarshallingStruct glyph2 = new GlyphMarshallingStruct(glyph);
			int freeGlyphRectCount = freeGlyphRects.Count;
			int usedGlyphRectCount = usedGlyphRects.Count;
			int num = freeGlyphRectCount + usedGlyphRectCount;
			if (s_FreeGlyphRects.Length < num || s_UsedGlyphRects.Length < num)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				s_FreeGlyphRects = new GlyphRect[num2];
				s_UsedGlyphRects = new GlyphRect[num2];
			}
			int num3 = Mathf.Max(freeGlyphRectCount, usedGlyphRectCount);
			for (int i = 0; i < num3; i++)
			{
				if (i < freeGlyphRectCount)
				{
					s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				if (i < usedGlyphRectCount)
				{
					s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			if (TryPackGlyphInAtlas_Internal(ref glyph2, padding, packingMode, renderMode, width, height, s_FreeGlyphRects, ref freeGlyphRectCount, s_UsedGlyphRects, ref usedGlyphRectCount))
			{
				glyph.glyphRect = glyph2.glyphRect;
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				num3 = Mathf.Max(freeGlyphRectCount, usedGlyphRectCount);
				for (int j = 0; j < num3; j++)
				{
					if (j < freeGlyphRectCount)
					{
						freeGlyphRects.Add(s_FreeGlyphRects[j]);
					}
					if (j < usedGlyphRectCount)
					{
						usedGlyphRects.Add(s_UsedGlyphRects[j]);
					}
				}
				return true;
			}
			return false;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::TryPackGlyph", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern bool TryPackGlyphInAtlas_Internal(ref GlyphMarshallingStruct glyph, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount);

		internal static bool TryPackGlyphsInAtlas(List<Glyph> glyphsToAdd, List<Glyph> glyphsAdded, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects)
		{
			int glyphsToAddCount = glyphsToAdd.Count;
			int glyphsAddedCount = glyphsAdded.Count;
			int freeGlyphRectCount = freeGlyphRects.Count;
			int usedGlyphRectCount = usedGlyphRects.Count;
			int num = glyphsToAddCount + glyphsAddedCount + freeGlyphRectCount + usedGlyphRectCount;
			if (s_GlyphMarshallingStruct_IN.Length < num || s_GlyphMarshallingStruct_OUT.Length < num || s_FreeGlyphRects.Length < num || s_UsedGlyphRects.Length < num)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num2];
				s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[num2];
				s_FreeGlyphRects = new GlyphRect[num2];
				s_UsedGlyphRects = new GlyphRect[num2];
			}
			s_GlyphLookupDictionary.Clear();
			for (int i = 0; i < num; i++)
			{
				if (i < glyphsToAddCount)
				{
					GlyphMarshallingStruct glyphMarshallingStruct = new GlyphMarshallingStruct(glyphsToAdd[i]);
					s_GlyphMarshallingStruct_IN[i] = glyphMarshallingStruct;
					if (!s_GlyphLookupDictionary.ContainsKey(glyphMarshallingStruct.index))
					{
						s_GlyphLookupDictionary.Add(glyphMarshallingStruct.index, glyphsToAdd[i]);
					}
				}
				if (i < glyphsAddedCount)
				{
					GlyphMarshallingStruct glyphMarshallingStruct2 = new GlyphMarshallingStruct(glyphsAdded[i]);
					s_GlyphMarshallingStruct_OUT[i] = glyphMarshallingStruct2;
					if (!s_GlyphLookupDictionary.ContainsKey(glyphMarshallingStruct2.index))
					{
						s_GlyphLookupDictionary.Add(glyphMarshallingStruct2.index, glyphsAdded[i]);
					}
				}
				if (i < freeGlyphRectCount)
				{
					s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				if (i < usedGlyphRectCount)
				{
					s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			bool result = TryPackGlyphsInAtlas_Internal(s_GlyphMarshallingStruct_IN, ref glyphsToAddCount, s_GlyphMarshallingStruct_OUT, ref glyphsAddedCount, padding, packingMode, renderMode, width, height, s_FreeGlyphRects, ref freeGlyphRectCount, s_UsedGlyphRects, ref usedGlyphRectCount);
			glyphsToAdd.Clear();
			glyphsAdded.Clear();
			freeGlyphRects.Clear();
			usedGlyphRects.Clear();
			for (int j = 0; j < num; j++)
			{
				if (j < glyphsToAddCount)
				{
					GlyphMarshallingStruct glyphMarshallingStruct3 = s_GlyphMarshallingStruct_IN[j];
					Glyph glyph = s_GlyphLookupDictionary[glyphMarshallingStruct3.index];
					glyph.metrics = glyphMarshallingStruct3.metrics;
					glyph.glyphRect = glyphMarshallingStruct3.glyphRect;
					glyph.scale = glyphMarshallingStruct3.scale;
					glyph.atlasIndex = glyphMarshallingStruct3.atlasIndex;
					glyphsToAdd.Add(glyph);
				}
				if (j < glyphsAddedCount)
				{
					GlyphMarshallingStruct glyphMarshallingStruct4 = s_GlyphMarshallingStruct_OUT[j];
					Glyph glyph2 = s_GlyphLookupDictionary[glyphMarshallingStruct4.index];
					glyph2.metrics = glyphMarshallingStruct4.metrics;
					glyph2.glyphRect = glyphMarshallingStruct4.glyphRect;
					glyph2.scale = glyphMarshallingStruct4.scale;
					glyph2.atlasIndex = glyphMarshallingStruct4.atlasIndex;
					glyphsAdded.Add(glyph2);
				}
				if (j < freeGlyphRectCount)
				{
					freeGlyphRects.Add(s_FreeGlyphRects[j]);
				}
				if (j < usedGlyphRectCount)
				{
					usedGlyphRects.Add(s_UsedGlyphRects[j]);
				}
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::TryPackGlyphs", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern bool TryPackGlyphsInAtlas_Internal([Out] GlyphMarshallingStruct[] glyphsToAdd, ref int glyphsToAddCount, [Out] GlyphMarshallingStruct[] glyphsAdded, ref int glyphsAddedCount, int padding, GlyphPackingMode packingMode, GlyphRenderMode renderMode, int width, int height, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount);

		internal static FontEngineError RenderGlyphToTexture(Glyph glyph, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			GlyphMarshallingStruct glyphStruct = new GlyphMarshallingStruct(glyph);
			return (FontEngineError)RenderGlyphToTexture_Internal(glyphStruct, padding, renderMode, texture);
		}

		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphToTexture", IsFreeFunction = true)]
		private static int RenderGlyphToTexture_Internal(GlyphMarshallingStruct glyphStruct, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			return RenderGlyphToTexture_Internal_Injected(ref glyphStruct, padding, renderMode, texture);
		}

		internal static FontEngineError RenderGlyphsToTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode, Texture2D texture)
		{
			int count = glyphs.Count;
			if (s_GlyphMarshallingStruct_IN.Length < count)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)RenderGlyphsToTexture_Internal(s_GlyphMarshallingStruct_IN, count, padding, renderMode, texture);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToTexture", IsFreeFunction = true)]
		private static extern int RenderGlyphsToTexture_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode, Texture2D texture);

		internal static FontEngineError RenderGlyphsToTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode, byte[] texBuffer, int texWidth, int texHeight)
		{
			int count = glyphs.Count;
			if (s_GlyphMarshallingStruct_IN.Length < count)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)RenderGlyphsToTextureBuffer_Internal(s_GlyphMarshallingStruct_IN, count, padding, renderMode, texBuffer, texWidth, texHeight);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToTextureBuffer", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern int RenderGlyphsToTextureBuffer_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode, [Out] byte[] texBuffer, int texWidth, int texHeight);

		internal static FontEngineError RenderGlyphsToSharedTexture(List<Glyph> glyphs, int padding, GlyphRenderMode renderMode)
		{
			int count = glyphs.Count;
			if (s_GlyphMarshallingStruct_IN.Length < count)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[num];
			}
			for (int i = 0; i < count; i++)
			{
				s_GlyphMarshallingStruct_IN[i] = new GlyphMarshallingStruct(glyphs[i]);
			}
			return (FontEngineError)RenderGlyphsToSharedTexture_Internal(s_GlyphMarshallingStruct_IN, count, padding, renderMode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::RenderGlyphsToSharedTexture", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern int RenderGlyphsToSharedTexture_Internal(GlyphMarshallingStruct[] glyphs, int glyphCount, int padding, GlyphRenderMode renderMode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::SetSharedTextureData", IsFreeFunction = true)]
		internal static extern void SetSharedTexture(Texture2D texture);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::ReleaseSharedTextureData", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern void ReleaseSharedTexture();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::SetTextureUploadMode", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern void SetTextureUploadMode(bool shouldUploadImmediately);

		internal static bool TryAddGlyphToTexture(uint glyphIndex, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture, out Glyph glyph)
		{
			int freeGlyphRectCount = freeGlyphRects.Count;
			int usedGlyphRectCount = usedGlyphRects.Count;
			int num = freeGlyphRectCount + usedGlyphRectCount;
			if (s_FreeGlyphRects.Length < num || s_UsedGlyphRects.Length < num)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				s_FreeGlyphRects = new GlyphRect[num2];
				s_UsedGlyphRects = new GlyphRect[num2];
			}
			int num3 = Mathf.Max(freeGlyphRectCount, usedGlyphRectCount);
			for (int i = 0; i < num3; i++)
			{
				if (i < freeGlyphRectCount)
				{
					s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				if (i < usedGlyphRectCount)
				{
					s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			if (TryAddGlyphToTexture_Internal(glyphIndex, padding, packingMode, s_FreeGlyphRects, ref freeGlyphRectCount, s_UsedGlyphRects, ref usedGlyphRectCount, renderMode, texture, out var glyph2))
			{
				glyph = new Glyph(glyph2);
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				num3 = Mathf.Max(freeGlyphRectCount, usedGlyphRectCount);
				for (int j = 0; j < num3; j++)
				{
					if (j < freeGlyphRectCount)
					{
						freeGlyphRects.Add(s_FreeGlyphRects[j]);
					}
					if (j < usedGlyphRectCount)
					{
						usedGlyphRects.Add(s_UsedGlyphRects[j]);
					}
				}
				return true;
			}
			glyph = null;
			return false;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern bool TryAddGlyphToTexture_Internal(uint glyphIndex, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture, out GlyphMarshallingStruct glyph);

		internal static bool TryAddGlyphsToTexture(List<Glyph> glyphsToAdd, List<Glyph> glyphsAdded, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture)
		{
			int num = 0;
			int glyphsToAddCount = glyphsToAdd.Count;
			int glyphsAddedCount = 0;
			if (s_GlyphMarshallingStruct_IN.Length < glyphsToAddCount || s_GlyphMarshallingStruct_OUT.Length < glyphsToAddCount)
			{
				int newSize = Mathf.NextPowerOfTwo(glyphsToAddCount + 1);
				if (s_GlyphMarshallingStruct_IN.Length < glyphsToAddCount)
				{
					Array.Resize(ref s_GlyphMarshallingStruct_IN, newSize);
				}
				if (s_GlyphMarshallingStruct_OUT.Length < glyphsToAddCount)
				{
					Array.Resize(ref s_GlyphMarshallingStruct_OUT, newSize);
				}
			}
			int freeGlyphRectCount = freeGlyphRects.Count;
			int usedGlyphRectCount = usedGlyphRects.Count;
			int num2 = freeGlyphRectCount + usedGlyphRectCount + glyphsToAddCount;
			if (s_FreeGlyphRects.Length < num2 || s_UsedGlyphRects.Length < num2)
			{
				int newSize2 = Mathf.NextPowerOfTwo(num2 + 1);
				if (s_FreeGlyphRects.Length < num2)
				{
					Array.Resize(ref s_FreeGlyphRects, newSize2);
				}
				if (s_UsedGlyphRects.Length < num2)
				{
					Array.Resize(ref s_UsedGlyphRects, newSize2);
				}
			}
			s_GlyphLookupDictionary.Clear();
			num = 0;
			bool flag = true;
			while (flag)
			{
				flag = false;
				if (num < glyphsToAddCount)
				{
					Glyph glyph = glyphsToAdd[num];
					s_GlyphMarshallingStruct_IN[num] = new GlyphMarshallingStruct(glyph);
					s_GlyphLookupDictionary.Add(glyph.index, glyph);
					flag = true;
				}
				if (num < freeGlyphRectCount)
				{
					s_FreeGlyphRects[num] = freeGlyphRects[num];
					flag = true;
				}
				if (num < usedGlyphRectCount)
				{
					s_UsedGlyphRects[num] = usedGlyphRects[num];
					flag = true;
				}
				num++;
			}
			bool result = TryAddGlyphsToTexture_Internal_MultiThread(s_GlyphMarshallingStruct_IN, ref glyphsToAddCount, s_GlyphMarshallingStruct_OUT, ref glyphsAddedCount, padding, packingMode, s_FreeGlyphRects, ref freeGlyphRectCount, s_UsedGlyphRects, ref usedGlyphRectCount, renderMode, texture);
			glyphsToAdd.Clear();
			glyphsAdded.Clear();
			freeGlyphRects.Clear();
			usedGlyphRects.Clear();
			num = 0;
			flag = true;
			while (flag)
			{
				flag = false;
				if (num < glyphsToAddCount)
				{
					uint index = s_GlyphMarshallingStruct_IN[num].index;
					glyphsToAdd.Add(s_GlyphLookupDictionary[index]);
					flag = true;
				}
				if (num < glyphsAddedCount)
				{
					uint index2 = s_GlyphMarshallingStruct_OUT[num].index;
					Glyph glyph2 = s_GlyphLookupDictionary[index2];
					glyph2.atlasIndex = s_GlyphMarshallingStruct_OUT[num].atlasIndex;
					glyph2.scale = s_GlyphMarshallingStruct_OUT[num].scale;
					glyph2.glyphRect = s_GlyphMarshallingStruct_OUT[num].glyphRect;
					glyph2.metrics = s_GlyphMarshallingStruct_OUT[num].metrics;
					glyphsAdded.Add(glyph2);
					flag = true;
				}
				if (num < freeGlyphRectCount)
				{
					freeGlyphRects.Add(s_FreeGlyphRects[num]);
					flag = true;
				}
				if (num < usedGlyphRectCount)
				{
					usedGlyphRects.Add(s_UsedGlyphRects[num]);
					flag = true;
				}
				num++;
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphsToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern bool TryAddGlyphsToTexture_Internal_MultiThread([Out] GlyphMarshallingStruct[] glyphsToAdd, ref int glyphsToAddCount, [Out] GlyphMarshallingStruct[] glyphsAdded, ref int glyphsAddedCount, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture);

		internal static bool TryAddGlyphsToTexture(List<uint> glyphIndexes, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture, out Glyph[] glyphs)
		{
			glyphs = null;
			if (glyphIndexes == null || glyphIndexes.Count == 0)
			{
				return false;
			}
			int glyphCount = glyphIndexes.Count;
			if (s_GlyphIndexes_MarshallingArray_A == null || s_GlyphIndexes_MarshallingArray_A.Length < glyphCount)
			{
				s_GlyphIndexes_MarshallingArray_A = new uint[Mathf.NextPowerOfTwo(glyphCount + 1)];
			}
			int freeGlyphRectCount = freeGlyphRects.Count;
			int usedGlyphRectCount = usedGlyphRects.Count;
			int num = freeGlyphRectCount + usedGlyphRectCount + glyphCount;
			if (s_FreeGlyphRects.Length < num || s_UsedGlyphRects.Length < num)
			{
				int num2 = Mathf.NextPowerOfTwo(num + 1);
				s_FreeGlyphRects = new GlyphRect[num2];
				s_UsedGlyphRects = new GlyphRect[num2];
			}
			if (s_GlyphMarshallingStruct_OUT.Length < glyphCount)
			{
				int num3 = Mathf.NextPowerOfTwo(glyphCount + 1);
				s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[num3];
			}
			int num4 = FontEngineUtilities.MaxValue(freeGlyphRectCount, usedGlyphRectCount, glyphCount);
			for (int i = 0; i < num4; i++)
			{
				if (i < glyphCount)
				{
					s_GlyphIndexes_MarshallingArray_A[i] = glyphIndexes[i];
				}
				if (i < freeGlyphRectCount)
				{
					s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				if (i < usedGlyphRectCount)
				{
					s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			bool result = TryAddGlyphsToTexture_Internal(s_GlyphIndexes_MarshallingArray_A, padding, packingMode, s_FreeGlyphRects, ref freeGlyphRectCount, s_UsedGlyphRects, ref usedGlyphRectCount, renderMode, texture, s_GlyphMarshallingStruct_OUT, ref glyphCount);
			if (s_Glyphs == null || s_Glyphs.Length <= glyphCount)
			{
				s_Glyphs = new Glyph[Mathf.NextPowerOfTwo(glyphCount + 1)];
			}
			s_Glyphs[glyphCount] = null;
			freeGlyphRects.Clear();
			usedGlyphRects.Clear();
			num4 = FontEngineUtilities.MaxValue(freeGlyphRectCount, usedGlyphRectCount, glyphCount);
			for (int j = 0; j < num4; j++)
			{
				if (j < glyphCount)
				{
					s_Glyphs[j] = new Glyph(s_GlyphMarshallingStruct_OUT[j]);
				}
				if (j < freeGlyphRectCount)
				{
					freeGlyphRects.Add(s_FreeGlyphRects[j]);
				}
				if (j < usedGlyphRectCount)
				{
					usedGlyphRects.Add(s_UsedGlyphRects[j]);
				}
			}
			glyphs = s_Glyphs;
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphsToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		private static extern bool TryAddGlyphsToTexture_Internal(uint[] glyphIndex, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture, [Out] GlyphMarshallingStruct[] glyphs, ref int glyphCount);

		[NativeMethod(Name = "TextCore::FontEngine::GetOpenTypeLayoutTable", IsFreeFunction = true)]
		internal static OTL_Table GetOpenTypeLayoutTable(OTL_TableType type)
		{
			GetOpenTypeLayoutTable_Injected(type, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetOpenTypeLayoutScripts", IsFreeFunction = true)]
		internal static extern OTL_Script[] GetOpenTypeLayoutScripts();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetOpenTypeLayoutFeatures", IsFreeFunction = true)]
		internal static extern OTL_Feature[] GetOpenTypeLayoutFeatures();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetOpenTypeLayoutLookups", IsFreeFunction = true)]
		internal static extern OTL_Lookup[] GetOpenTypeLayoutLookups();

		internal static OpenTypeFeature[] GetOpenTypeFontFeatureList()
		{
			throw new NotImplementedException();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetAllSingleSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern SingleSubstitutionRecord[] GetAllSingleSubstitutionRecords();

		internal static SingleSubstitutionRecord[] GetSingleSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			GlyphIndexToMarshallingArray(glyphIndex, ref s_GlyphIndexes_MarshallingArray_A);
			return GetSingleSubstitutionRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		internal static SingleSubstitutionRecord[] GetSingleSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			return GetSingleSubstitutionRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		private static SingleSubstitutionRecord[] GetSingleSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			PopulateSingleSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_SingleSubstitutionRecords_MarshallingArray, recordCount);
			GetSingleSubstitutionRecordsFromMarshallingArray(s_SingleSubstitutionRecords_MarshallingArray);
			s_SingleSubstitutionRecords_MarshallingArray[recordCount] = default(SingleSubstitutionRecord);
			return s_SingleSubstitutionRecords_MarshallingArray;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulateSingleSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private static extern int PopulateSingleSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetSingleSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		private static extern int GetSingleSubstitutionRecordsFromMarshallingArray([Out] SingleSubstitutionRecord[] singleSubstitutionRecords);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetAllMultipleSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern MultipleSubstitutionRecord[] GetAllMultipleSubstitutionRecords();

		internal static MultipleSubstitutionRecord[] GetMultipleSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			GlyphIndexToMarshallingArray(glyphIndex, ref s_GlyphIndexes_MarshallingArray_A);
			return GetMultipleSubstitutionRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		internal static MultipleSubstitutionRecord[] GetMultipleSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			return GetMultipleSubstitutionRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		private static MultipleSubstitutionRecord[] GetMultipleSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			PopulateMultipleSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_MultipleSubstitutionRecords_MarshallingArray, recordCount);
			GetMultipleSubstitutionRecordsFromMarshallingArray(s_MultipleSubstitutionRecords_MarshallingArray);
			s_MultipleSubstitutionRecords_MarshallingArray[recordCount] = default(MultipleSubstitutionRecord);
			return s_MultipleSubstitutionRecords_MarshallingArray;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMultipleSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private static extern int PopulateMultipleSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetMultipleSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		private static extern int GetMultipleSubstitutionRecordsFromMarshallingArray([Out] MultipleSubstitutionRecord[] substitutionRecords);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetAllAlternateSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern AlternateSubstitutionRecord[] GetAllAlternateSubstitutionRecords();

		internal static AlternateSubstitutionRecord[] GetAlternateSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			GlyphIndexToMarshallingArray(glyphIndex, ref s_GlyphIndexes_MarshallingArray_A);
			return GetAlternateSubstitutionRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		internal static AlternateSubstitutionRecord[] GetAlternateSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			return GetAlternateSubstitutionRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		private static AlternateSubstitutionRecord[] GetAlternateSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			PopulateAlternateSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_AlternateSubstitutionRecords_MarshallingArray, recordCount);
			GetAlternateSubstitutionRecordsFromMarshallingArray(s_AlternateSubstitutionRecords_MarshallingArray);
			s_AlternateSubstitutionRecords_MarshallingArray[recordCount] = default(AlternateSubstitutionRecord);
			return s_AlternateSubstitutionRecords_MarshallingArray;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulateAlternateSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private static extern int PopulateAlternateSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetAlternateSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		private static extern int GetAlternateSubstitutionRecordsFromMarshallingArray([Out] AlternateSubstitutionRecord[] singleSubstitutionRecords);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetAllLigatureSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern LigatureSubstitutionRecord[] GetAllLigatureSubstitutionRecords();

		internal static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(uint glyphIndex)
		{
			GlyphIndexToMarshallingArray(glyphIndex, ref s_GlyphIndexes_MarshallingArray_A);
			return GetLigatureSubstitutionRecords(s_GlyphIndexes_MarshallingArray_A);
		}

		internal static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(List<uint> glyphIndexes)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			return GetLigatureSubstitutionRecords(s_GlyphIndexes_MarshallingArray_A);
		}

		internal static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			GlyphIndexToMarshallingArray(glyphIndex, ref s_GlyphIndexes_MarshallingArray_A);
			return GetLigatureSubstitutionRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		internal static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			return GetLigatureSubstitutionRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		private static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(uint[] glyphIndexes)
		{
			PopulateLigatureSubstitutionRecordMarshallingArray(glyphIndexes, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_LigatureSubstitutionRecords_MarshallingArray, recordCount);
			GetLigatureSubstitutionRecordsFromMarshallingArray(s_LigatureSubstitutionRecords_MarshallingArray);
			s_LigatureSubstitutionRecords_MarshallingArray[recordCount] = default(LigatureSubstitutionRecord);
			return s_LigatureSubstitutionRecords_MarshallingArray;
		}

		private static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			PopulateLigatureSubstitutionRecordMarshallingArray_for_LookupIndex(glyphIndexes, lookupIndex, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_LigatureSubstitutionRecords_MarshallingArray, recordCount);
			GetLigatureSubstitutionRecordsFromMarshallingArray(s_LigatureSubstitutionRecords_MarshallingArray);
			s_LigatureSubstitutionRecords_MarshallingArray[recordCount] = default(LigatureSubstitutionRecord);
			return s_LigatureSubstitutionRecords_MarshallingArray;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulateLigatureSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private static extern int PopulateLigatureSubstitutionRecordMarshallingArray(uint[] glyphIndexes, out int recordCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulateLigatureSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private static extern int PopulateLigatureSubstitutionRecordMarshallingArray_for_LookupIndex(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetLigatureSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		private static extern int GetLigatureSubstitutionRecordsFromMarshallingArray([Out] LigatureSubstitutionRecord[] ligatureSubstitutionRecords);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetAllContextualSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern ContextualSubstitutionRecord[] GetAllContextualSubstitutionRecords();

		internal static ContextualSubstitutionRecord[] GetContextualSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			GlyphIndexToMarshallingArray(glyphIndex, ref s_GlyphIndexes_MarshallingArray_A);
			return GetContextualSubstitutionRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		internal static ContextualSubstitutionRecord[] GetContextualSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			return GetContextualSubstitutionRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		private static ContextualSubstitutionRecord[] GetContextualSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			PopulateContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_ContextualSubstitutionRecords_MarshallingArray, recordCount);
			GetContextualSubstitutionRecordsFromMarshallingArray(s_ContextualSubstitutionRecords_MarshallingArray);
			s_ContextualSubstitutionRecords_MarshallingArray[recordCount] = default(ContextualSubstitutionRecord);
			return s_ContextualSubstitutionRecords_MarshallingArray;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulateContextualSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private static extern int PopulateContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetContextualSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		private static extern int GetContextualSubstitutionRecordsFromMarshallingArray([Out] ContextualSubstitutionRecord[] substitutionRecords);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetAllChainingContextualSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern ChainingContextualSubstitutionRecord[] GetAllChainingContextualSubstitutionRecords();

		internal static ChainingContextualSubstitutionRecord[] GetChainingContextualSubstitutionRecords(int lookupIndex, uint glyphIndex)
		{
			GlyphIndexToMarshallingArray(glyphIndex, ref s_GlyphIndexes_MarshallingArray_A);
			return GetChainingContextualSubstitutionRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		internal static ChainingContextualSubstitutionRecord[] GetChainingContextualSubstitutionRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			return GetChainingContextualSubstitutionRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		private static ChainingContextualSubstitutionRecord[] GetChainingContextualSubstitutionRecords(int lookupIndex, uint[] glyphIndexes)
		{
			PopulateChainingContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_ChainingContextualSubstitutionRecords_MarshallingArray, recordCount);
			GetChainingContextualSubstitutionRecordsFromMarshallingArray(s_ChainingContextualSubstitutionRecords_MarshallingArray);
			s_ChainingContextualSubstitutionRecords_MarshallingArray[recordCount] = default(ChainingContextualSubstitutionRecord);
			return s_ChainingContextualSubstitutionRecords_MarshallingArray;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulateChainingContextualSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private static extern int PopulateChainingContextualSubstitutionRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetChainingContextualSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		private static extern int GetChainingContextualSubstitutionRecordsFromMarshallingArray([Out] ChainingContextualSubstitutionRecord[] substitutionRecords);

		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentTable(uint[] glyphIndexes)
		{
			PopulatePairAdjustmentRecordMarshallingArray_from_KernTable(glyphIndexes, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_PairAdjustmentRecords_MarshallingArray, recordCount);
			GetPairAdjustmentRecordsFromMarshallingArray(s_PairAdjustmentRecords_MarshallingArray);
			s_PairAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphPairAdjustmentRecord);
			return s_PairAdjustmentRecords_MarshallingArray;
		}

		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(List<uint> glyphIndexes, out int recordCount)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			PopulatePairAdjustmentRecordMarshallingArray_from_KernTable(s_GlyphIndexes_MarshallingArray_A, out recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_PairAdjustmentRecords_MarshallingArray, recordCount);
			GetPairAdjustmentRecordsFromMarshallingArray(s_PairAdjustmentRecords_MarshallingArray);
			s_PairAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphPairAdjustmentRecord);
			return s_PairAdjustmentRecords_MarshallingArray;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArrayFromKernTable", IsFreeFunction = true)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_from_KernTable(uint[] glyphIndexes, out int recordCount);

		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(uint glyphIndex, out int recordCount)
		{
			PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndex(glyphIndex, out recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_PairAdjustmentRecords_MarshallingArray, recordCount);
			GetPairAdjustmentRecordsFromMarshallingArray(s_PairAdjustmentRecords_MarshallingArray);
			s_PairAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphPairAdjustmentRecord);
			return s_PairAdjustmentRecords_MarshallingArray;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArrayFromKernTable", IsFreeFunction = true)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_from_GlyphIndex(uint glyphIndex, out int recordCount);

		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(List<uint> newGlyphIndexes, List<uint> allGlyphIndexes)
		{
			GenericListToMarshallingArray(ref newGlyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			GenericListToMarshallingArray(ref allGlyphIndexes, ref s_GlyphIndexes_MarshallingArray_B);
			PopulatePairAdjustmentRecordMarshallingArray_for_NewlyAddedGlyphIndexes(s_GlyphIndexes_MarshallingArray_A, s_GlyphIndexes_MarshallingArray_B, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_PairAdjustmentRecords_MarshallingArray, recordCount);
			GetPairAdjustmentRecordsFromMarshallingArray(s_PairAdjustmentRecords_MarshallingArray);
			s_PairAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphPairAdjustmentRecord);
			return s_PairAdjustmentRecords_MarshallingArray;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArrayFromKernTable", IsFreeFunction = true)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_for_NewlyAddedGlyphIndexes(uint[] newGlyphIndexes, uint[] allGlyphIndexes, out int recordCount);

		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphPairAdjustmentRecord", IsFreeFunction = true)]
		internal static GlyphPairAdjustmentRecord GetGlyphPairAdjustmentRecord(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			GetGlyphPairAdjustmentRecord_Injected(firstGlyphIndex, secondGlyphIndex, out var ret);
			return ret;
		}

		internal static GlyphAdjustmentRecord[] GetSingleAdjustmentRecords(int lookupIndex, uint glyphIndex)
		{
			if (s_GlyphIndexes_MarshallingArray_A == null)
			{
				s_GlyphIndexes_MarshallingArray_A = new uint[8];
			}
			s_GlyphIndexes_MarshallingArray_A[0] = glyphIndex;
			s_GlyphIndexes_MarshallingArray_A[1] = 0u;
			return GetSingleAdjustmentRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		internal static GlyphAdjustmentRecord[] GetSingleAdjustmentRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			return GetSingleAdjustmentRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		private static GlyphAdjustmentRecord[] GetSingleAdjustmentRecords(int lookupIndex, uint[] glyphIndexes)
		{
			PopulateSingleAdjustmentRecordMarshallingArray_from_GlyphIndexes(glyphIndexes, lookupIndex, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_SingleAdjustmentRecords_MarshallingArray, recordCount);
			GetSingleAdjustmentRecordsFromMarshallingArray(s_SingleAdjustmentRecords_MarshallingArray);
			s_SingleAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphAdjustmentRecord);
			return s_SingleAdjustmentRecords_MarshallingArray;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulateSingleAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private static extern int PopulateSingleAdjustmentRecordMarshallingArray_from_GlyphIndexes(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetSingleAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		private static extern int GetSingleAdjustmentRecordsFromMarshallingArray([Out] GlyphAdjustmentRecord[] singleSubstitutionRecords);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetPairAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(uint glyphIndex);

		[NativeMethod(Name = "TextCore::FontEngine::GetPairAdjustmentRecord", IsThreadSafe = true, IsFreeFunction = true)]
		internal static GlyphPairAdjustmentRecord GetPairAdjustmentRecord(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			GetPairAdjustmentRecord_Injected(firstGlyphIndex, secondGlyphIndex, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetAllPairAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern GlyphPairAdjustmentRecord[] GetAllPairAdjustmentRecords();

		internal static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(List<uint> glyphIndexes)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			return GetPairAdjustmentRecords(s_GlyphIndexes_MarshallingArray_A);
		}

		internal static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(int lookupIndex, uint glyphIndex)
		{
			GlyphIndexToMarshallingArray(glyphIndex, ref s_GlyphIndexes_MarshallingArray_A);
			return GetPairAdjustmentRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		internal static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			return GetPairAdjustmentRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		private static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(uint[] glyphIndexes)
		{
			PopulatePairAdjustmentRecordMarshallingArray(glyphIndexes, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_PairAdjustmentRecords_MarshallingArray, recordCount);
			GetPairAdjustmentRecordsFromMarshallingArray(s_PairAdjustmentRecords_MarshallingArray);
			s_PairAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphPairAdjustmentRecord);
			return s_PairAdjustmentRecords_MarshallingArray;
		}

		private static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(int lookupIndex, uint[] glyphIndexes)
		{
			PopulatePairAdjustmentRecordMarshallingArray_for_LookupIndex(glyphIndexes, lookupIndex, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_PairAdjustmentRecords_MarshallingArray, recordCount);
			GetPairAdjustmentRecordsFromMarshallingArray(s_PairAdjustmentRecords_MarshallingArray);
			s_PairAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphPairAdjustmentRecord);
			return s_PairAdjustmentRecords_MarshallingArray;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray(uint[] glyphIndexes, out int recordCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_for_LookupIndex(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphPairAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		private static extern int GetPairAdjustmentRecordsFromMarshallingArray([Out] GlyphPairAdjustmentRecord[] glyphPairAdjustmentRecords);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetAllMarkToBaseAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern MarkToBaseAdjustmentRecord[] GetAllMarkToBaseAdjustmentRecords();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToBaseAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(uint baseGlyphIndex);

		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToBaseAdjustmentRecord", IsFreeFunction = true)]
		internal static MarkToBaseAdjustmentRecord GetMarkToBaseAdjustmentRecord(uint baseGlyphIndex, uint markGlyphIndex)
		{
			GetMarkToBaseAdjustmentRecord_Injected(baseGlyphIndex, markGlyphIndex, out var ret);
			return ret;
		}

		internal static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(List<uint> glyphIndexes)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			return GetMarkToBaseAdjustmentRecords(s_GlyphIndexes_MarshallingArray_A);
		}

		internal static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			return GetMarkToBaseAdjustmentRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		private static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(uint[] glyphIndexes)
		{
			PopulateMarkToBaseAdjustmentRecordMarshallingArray(glyphIndexes, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_MarkToBaseAdjustmentRecords_MarshallingArray, recordCount);
			GetMarkToBaseAdjustmentRecordsFromMarshallingArray(s_MarkToBaseAdjustmentRecords_MarshallingArray);
			s_MarkToBaseAdjustmentRecords_MarshallingArray[recordCount] = default(MarkToBaseAdjustmentRecord);
			return s_MarkToBaseAdjustmentRecords_MarshallingArray;
		}

		private static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(int lookupIndex, uint[] glyphIndexes)
		{
			PopulateMarkToBaseAdjustmentRecordMarshallingArray_for_LookupIndex(glyphIndexes, lookupIndex, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_MarkToBaseAdjustmentRecords_MarshallingArray, recordCount);
			GetMarkToBaseAdjustmentRecordsFromMarshallingArray(s_MarkToBaseAdjustmentRecords_MarshallingArray);
			s_MarkToBaseAdjustmentRecords_MarshallingArray[recordCount] = default(MarkToBaseAdjustmentRecord);
			return s_MarkToBaseAdjustmentRecords_MarshallingArray;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMarkToBaseAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private static extern int PopulateMarkToBaseAdjustmentRecordMarshallingArray(uint[] glyphIndexes, out int recordCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMarkToBaseAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private static extern int PopulateMarkToBaseAdjustmentRecordMarshallingArray_for_LookupIndex(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToBaseAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		private static extern int GetMarkToBaseAdjustmentRecordsFromMarshallingArray([Out] MarkToBaseAdjustmentRecord[] adjustmentRecords);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetAllMarkToMarkAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern MarkToMarkAdjustmentRecord[] GetAllMarkToMarkAdjustmentRecords();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToMarkAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static extern MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(uint baseMarkGlyphIndex);

		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToMarkAdjustmentRecord", IsFreeFunction = true)]
		internal static MarkToMarkAdjustmentRecord GetMarkToMarkAdjustmentRecord(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			GetMarkToMarkAdjustmentRecord_Injected(firstGlyphIndex, secondGlyphIndex, out var ret);
			return ret;
		}

		internal static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(List<uint> glyphIndexes)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			return GetMarkToMarkAdjustmentRecords(s_GlyphIndexes_MarshallingArray_A);
		}

		internal static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(int lookupIndex, List<uint> glyphIndexes)
		{
			GenericListToMarshallingArray(ref glyphIndexes, ref s_GlyphIndexes_MarshallingArray_A);
			return GetMarkToMarkAdjustmentRecords(lookupIndex, s_GlyphIndexes_MarshallingArray_A);
		}

		private static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(uint[] glyphIndexes)
		{
			PopulateMarkToMarkAdjustmentRecordMarshallingArray(s_GlyphIndexes_MarshallingArray_A, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_MarkToMarkAdjustmentRecords_MarshallingArray, recordCount);
			GetMarkToMarkAdjustmentRecordsFromMarshallingArray(s_MarkToMarkAdjustmentRecords_MarshallingArray);
			s_MarkToMarkAdjustmentRecords_MarshallingArray[recordCount] = default(MarkToMarkAdjustmentRecord);
			return s_MarkToMarkAdjustmentRecords_MarshallingArray;
		}

		private static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(int lookupIndex, uint[] glyphIndexes)
		{
			PopulateMarkToMarkAdjustmentRecordMarshallingArray_for_LookupIndex(s_GlyphIndexes_MarshallingArray_A, lookupIndex, out var recordCount);
			if (recordCount == 0)
			{
				return null;
			}
			SetMarshallingArraySize(ref s_MarkToMarkAdjustmentRecords_MarshallingArray, recordCount);
			GetMarkToMarkAdjustmentRecordsFromMarshallingArray(s_MarkToMarkAdjustmentRecords_MarshallingArray);
			s_MarkToMarkAdjustmentRecords_MarshallingArray[recordCount] = default(MarkToMarkAdjustmentRecord);
			return s_MarkToMarkAdjustmentRecords_MarshallingArray;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMarkToMarkAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private static extern int PopulateMarkToMarkAdjustmentRecordMarshallingArray(uint[] glyphIndexes, out int recordCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMarkToMarkAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private static extern int PopulateMarkToMarkAdjustmentRecordMarshallingArray_for_LookupIndex(uint[] glyphIndexes, int lookupIndex, out int recordCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToMarkAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		private static extern int GetMarkToMarkAdjustmentRecordsFromMarshallingArray([Out] MarkToMarkAdjustmentRecord[] adjustmentRecords);

		private static void GlyphIndexToMarshallingArray(uint glyphIndex, ref uint[] dstArray)
		{
			if (dstArray == null || dstArray.Length == 1)
			{
				dstArray = new uint[8];
			}
			dstArray[0] = glyphIndex;
			dstArray[1] = 0u;
		}

		private static void GenericListToMarshallingArray<T>(ref List<T> srcList, ref T[] dstArray)
		{
			int count = srcList.Count;
			if (dstArray == null || dstArray.Length <= count)
			{
				int num = Mathf.NextPowerOfTwo(count + 1);
				if (dstArray == null)
				{
					dstArray = new T[num];
				}
				else
				{
					Array.Resize(ref dstArray, num);
				}
			}
			for (int i = 0; i < count; i++)
			{
				dstArray[i] = srcList[i];
			}
			dstArray[count] = default(T);
		}

		private static void SetMarshallingArraySize<T>(ref T[] marshallingArray, int recordCount)
		{
			if (marshallingArray == null || marshallingArray.Length <= recordCount)
			{
				int num = Mathf.NextPowerOfTwo(recordCount + 1);
				if (marshallingArray == null)
				{
					marshallingArray = new T[num];
				}
				else
				{
					Array.Resize(ref marshallingArray, num);
				}
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::ResetAtlasTexture", IsFreeFunction = true)]
		internal static extern void ResetAtlasTexture(Texture2D texture);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "TextCore::FontEngine::RenderToTexture", IsFreeFunction = true)]
		internal static extern void RenderBufferToTexture(Texture2D srcTexture, int padding, GlyphRenderMode renderMode, Texture2D dstTexture);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RenderGlyphToTexture_Internal_Injected(ref GlyphMarshallingStruct glyphStruct, int padding, GlyphRenderMode renderMode, Texture2D texture);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOpenTypeLayoutTable_Injected(OTL_TableType type, out OTL_Table ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlyphPairAdjustmentRecord_Injected(uint firstGlyphIndex, uint secondGlyphIndex, out GlyphPairAdjustmentRecord ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPairAdjustmentRecord_Injected(uint firstGlyphIndex, uint secondGlyphIndex, out GlyphPairAdjustmentRecord ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMarkToBaseAdjustmentRecord_Injected(uint baseGlyphIndex, uint markGlyphIndex, out MarkToBaseAdjustmentRecord ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMarkToMarkAdjustmentRecord_Injected(uint firstGlyphIndex, uint secondGlyphIndex, out MarkToMarkAdjustmentRecord ret);
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct FontEngineUtilities
	{
		internal static bool Approximately(float a, float b)
		{
			return Mathf.Abs(a - b) < 0.001f;
		}

		internal static int MaxValue(int a, int b, int c)
		{
			return (a >= b) ? ((a < c) ? c : a) : ((b < c) ? c : b);
		}
	}
	[UsedByNativeCode]
	internal struct OTL_Table
	{
		public OTL_Script[] scripts;

		public OTL_Feature[] features;

		public OTL_Lookup[] lookups;
	}
	[DebuggerDisplay("Script = {tag},  Language Count = {languages.Length}")]
	[UsedByNativeCode]
	internal struct OTL_Script
	{
		public string tag;

		public OTL_Language[] languages;
	}
	[DebuggerDisplay("Language = {tag},  Feature Count = {featureIndexes.Length}")]
	[UsedByNativeCode]
	internal struct OTL_Language
	{
		public string tag;

		public uint[] featureIndexes;
	}
	[DebuggerDisplay("Feature = {tag},  Lookup Count = {lookupIndexes.Length}")]
	[UsedByNativeCode]
	internal struct OTL_Feature
	{
		public string tag;

		public uint[] lookupIndexes;
	}
	[UsedByNativeCode]
	[DebuggerDisplay("{(OTL_LookupType)lookupType}")]
	internal struct OTL_Lookup
	{
		public uint lookupType;

		public uint lookupFlag;

		public uint markFilteringSet;
	}
	[UsedByNativeCode]
	internal struct GlyphMarshallingStruct
	{
		public uint index;

		public GlyphMetrics metrics;

		public GlyphRect glyphRect;

		public float scale;

		public int atlasIndex;

		public GlyphClassDefinitionType classDefinitionType;

		public GlyphMarshallingStruct(Glyph glyph)
		{
			index = glyph.index;
			metrics = glyph.metrics;
			glyphRect = glyph.glyphRect;
			scale = glyph.scale;
			atlasIndex = glyph.atlasIndex;
			classDefinitionType = glyph.classDefinitionType;
		}

		public GlyphMarshallingStruct(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			this.index = index;
			this.metrics = metrics;
			this.glyphRect = glyphRect;
			this.scale = scale;
			this.atlasIndex = atlasIndex;
			classDefinitionType = GlyphClassDefinitionType.Undefined;
		}

		public GlyphMarshallingStruct(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex, GlyphClassDefinitionType classDefinitionType)
		{
			this.index = index;
			this.metrics = metrics;
			this.glyphRect = glyphRect;
			this.scale = scale;
			this.atlasIndex = atlasIndex;
			this.classDefinitionType = classDefinitionType;
		}
	}
	internal enum OTL_TableType
	{
		BASE = 0x1000,
		GDEF = 0x2000,
		GPOS = 0x4000,
		GSUB = 0x8000,
		JSTF = 0x10000,
		MATH = 0x20000
	}
	internal enum OTL_LookupType
	{
		Single_Substitution = 32769,
		Multiple_Substitution = 32770,
		Alternate_Substitution = 32771,
		Ligature_Substitution = 32772,
		Contextual_Substitution = 32773,
		Chaining_Contextual_Substitution = 32774,
		Extension_Substitution = 32775,
		Reverse_Chaining_Contextual_Single_Substitution = 32776,
		Single_Adjustment = 16385,
		Pair_Adjustment = 16386,
		Cursive_Attachment = 16387,
		Mark_to_Base_Attachment = 16388,
		Mark_to_Ligature_Attachment = 16389,
		Mark_to_Mark_Attachment = 16390,
		Contextual_Positioning = 16391,
		Chaining_Contextual_Positioning = 16392,
		Extension_Positioning = 16393
	}
	[Flags]
	public enum FontFeatureLookupFlags
	{
		None = 0,
		IgnoreLigatures = 4,
		IgnoreSpacingAdjustments = 0x100
	}
	[Serializable]
	internal struct OpenTypeLayoutTable
	{
		public List<OpenTypeLayoutScript> scripts;

		public List<OpenTypeLayoutFeature> features;

		[SerializeReference]
		public List<OpenTypeLayoutLookup> lookups;
	}
	[Serializable]
	[DebuggerDisplay("Script = {tag},  Language Count = {languages.Count}")]
	internal struct OpenTypeLayoutScript
	{
		public string tag;

		public List<OpenTypeLayoutLanguage> languages;
	}
	[Serializable]
	[DebuggerDisplay("Language = {tag},  Feature Count = {featureIndexes.Length}")]
	internal struct OpenTypeLayoutLanguage
	{
		public string tag;

		public uint[] featureIndexes;
	}
	[Serializable]
	[DebuggerDisplay("Feature = {tag},  Lookup Count = {lookupIndexes.Length}")]
	internal struct OpenTypeLayoutFeature
	{
		public string tag;

		public uint[] lookupIndexes;
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct OpenTypeFeature
	{
	}
	[Serializable]
	internal abstract class OpenTypeLayoutLookup
	{
		public uint lookupType;

		public uint lookupFlag;

		public uint markFilteringSet;

		public abstract void InitializeLookupDictionary();

		public virtual void UpdateRecords(int lookupIndex, uint glyphIndex)
		{
		}

		public virtual void UpdateRecords(int lookupIndex, uint glyphIndex, float emScale)
		{
		}

		public virtual void UpdateRecords(int lookupIndex, List<uint> glyphIndexes)
		{
		}

		public virtual void UpdateRecords(int lookupIndex, List<uint> glyphIndexes, float emScale)
		{
		}

		public abstract void ClearRecords();
	}
	[Serializable]
	[UsedByNativeCode]
	public struct GlyphValueRecord : IEquatable<GlyphValueRecord>
	{
		[NativeName("xPlacement")]
		[SerializeField]
		private float m_XPlacement;

		[SerializeField]
		[NativeName("yPlacement")]
		private float m_YPlacement;

		[SerializeField]
		[NativeName("xAdvance")]
		private float m_XAdvance;

		[SerializeField]
		[NativeName("yAdvance")]
		private float m_YAdvance;

		public float xPlacement
		{
			get
			{
				return m_XPlacement;
			}
			set
			{
				m_XPlacement = value;
			}
		}

		public float yPlacement
		{
			get
			{
				return m_YPlacement;
			}
			set
			{
				m_YPlacement = value;
			}
		}

		public float xAdvance
		{
			get
			{
				return m_XAdvance;
			}
			set
			{
				m_XAdvance = value;
			}
		}

		public float yAdvance
		{
			get
			{
				return m_YAdvance;
			}
			set
			{
				m_YAdvance = value;
			}
		}

		public GlyphValueRecord(float xPlacement, float yPlacement, float xAdvance, float yAdvance)
		{
			m_XPlacement = xPlacement;
			m_YPlacement = yPlacement;
			m_XAdvance = xAdvance;
			m_YAdvance = yAdvance;
		}

		public static GlyphValueRecord operator +(GlyphValueRecord a, GlyphValueRecord b)
		{
			GlyphValueRecord result = default(GlyphValueRecord);
			result.m_XPlacement = a.xPlacement + b.xPlacement;
			result.m_YPlacement = a.yPlacement + b.yPlacement;
			result.m_XAdvance = a.xAdvance + b.xAdvance;
			result.m_YAdvance = a.yAdvance + b.yAdvance;
			return result;
		}

		[ExcludeFromDocs]
		public static GlyphValueRecord operator *(GlyphValueRecord a, float emScale)
		{
			a.m_XPlacement = a.xPlacement * emScale;
			a.m_YPlacement = a.yPlacement * emScale;
			a.m_XAdvance = a.xAdvance * emScale;
			a.m_YAdvance = a.yAdvance * emScale;
			return a;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool Equals(GlyphValueRecord other)
		{
			return base.Equals((object)other);
		}

		public static bool operator ==(GlyphValueRecord lhs, GlyphValueRecord rhs)
		{
			return lhs.m_XPlacement == rhs.m_XPlacement && lhs.m_YPlacement == rhs.m_YPlacement && lhs.m_XAdvance == rhs.m_XAdvance && lhs.m_YAdvance == rhs.m_YAdvance;
		}

		public static bool operator !=(GlyphValueRecord lhs, GlyphValueRecord rhs)
		{
			return !(lhs == rhs);
		}
	}
	[Serializable]
	[UsedByNativeCode]
	public struct GlyphAdjustmentRecord : IEquatable<GlyphAdjustmentRecord>
	{
		[SerializeField]
		[NativeName("glyphIndex")]
		private uint m_GlyphIndex;

		[NativeName("glyphValueRecord")]
		[SerializeField]
		private GlyphValueRecord m_GlyphValueRecord;

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

		public GlyphValueRecord glyphValueRecord
		{
			get
			{
				return m_GlyphValueRecord;
			}
			set
			{
				m_GlyphValueRecord = value;
			}
		}

		public GlyphAdjustmentRecord(uint glyphIndex, GlyphValueRecord glyphValueRecord)
		{
			m_GlyphIndex = glyphIndex;
			m_GlyphValueRecord = glyphValueRecord;
		}

		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		[ExcludeFromDocs]
		public bool Equals(GlyphAdjustmentRecord other)
		{
			return base.Equals((object)other);
		}

		[ExcludeFromDocs]
		public static bool operator ==(GlyphAdjustmentRecord lhs, GlyphAdjustmentRecord rhs)
		{
			return lhs.m_GlyphIndex == rhs.m_GlyphIndex && lhs.m_GlyphValueRecord == rhs.m_GlyphValueRecord;
		}

		[ExcludeFromDocs]
		public static bool operator !=(GlyphAdjustmentRecord lhs, GlyphAdjustmentRecord rhs)
		{
			return !(lhs == rhs);
		}
	}
	[Serializable]
	[UsedByNativeCode]
	[DebuggerDisplay("First glyphIndex = {m_FirstAdjustmentRecord.m_GlyphIndex},  Second glyphIndex = {m_SecondAdjustmentRecord.m_GlyphIndex}")]
	public struct GlyphPairAdjustmentRecord : IEquatable<GlyphPairAdjustmentRecord>
	{
		[NativeName("firstAdjustmentRecord")]
		[SerializeField]
		private GlyphAdjustmentRecord m_FirstAdjustmentRecord;

		[NativeName("secondAdjustmentRecord")]
		[SerializeField]
		private GlyphAdjustmentRecord m_SecondAdjustmentRecord;

		[SerializeField]
		private FontFeatureLookupFlags m_FeatureLookupFlags;

		public GlyphAdjustmentRecord firstAdjustmentRecord
		{
			get
			{
				return m_FirstAdjustmentRecord;
			}
			set
			{
				m_FirstAdjustmentRecord = value;
			}
		}

		public GlyphAdjustmentRecord secondAdjustmentRecord
		{
			get
			{
				return m_SecondAdjustmentRecord;
			}
			set
			{
				m_SecondAdjustmentRecord = value;
			}
		}

		public FontFeatureLookupFlags featureLookupFlags
		{
			get
			{
				return m_FeatureLookupFlags;
			}
			set
			{
				m_FeatureLookupFlags = value;
			}
		}

		public GlyphPairAdjustmentRecord(GlyphAdjustmentRecord firstAdjustmentRecord, GlyphAdjustmentRecord secondAdjustmentRecord)
		{
			m_FirstAdjustmentRecord = firstAdjustmentRecord;
			m_SecondAdjustmentRecord = secondAdjustmentRecord;
			m_FeatureLookupFlags = FontFeatureLookupFlags.None;
		}

		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		[ExcludeFromDocs]
		public bool Equals(GlyphPairAdjustmentRecord other)
		{
			return base.Equals((object)other);
		}

		[ExcludeFromDocs]
		public static bool operator ==(GlyphPairAdjustmentRecord lhs, GlyphPairAdjustmentRecord rhs)
		{
			return lhs.m_FirstAdjustmentRecord == rhs.m_FirstAdjustmentRecord && lhs.m_SecondAdjustmentRecord == rhs.m_SecondAdjustmentRecord;
		}

		[ExcludeFromDocs]
		public static bool operator !=(GlyphPairAdjustmentRecord lhs, GlyphPairAdjustmentRecord rhs)
		{
			return !(lhs == rhs);
		}
	}
	[Serializable]
	[UsedByNativeCode]
	internal struct GlyphAnchorPoint
	{
		[SerializeField]
		[NativeName("xPositionAdjustment")]
		private float m_XCoordinate;

		[NativeName("yPositionAdjustment")]
		[SerializeField]
		private float m_YCoordinate;

		public float xCoordinate
		{
			get
			{
				return m_XCoordinate;
			}
			set
			{
				m_XCoordinate = value;
			}
		}

		public float yCoordinate
		{
			get
			{
				return m_YCoordinate;
			}
			set
			{
				m_YCoordinate = value;
			}
		}
	}
	[Serializable]
	[UsedByNativeCode]
	internal struct MarkPositionAdjustment
	{
		[NativeName("xCoordinate")]
		[SerializeField]
		private float m_XPositionAdjustment;

		[NativeName("yCoordinate")]
		[SerializeField]
		private float m_YPositionAdjustment;

		public float xPositionAdjustment
		{
			get
			{
				return m_XPositionAdjustment;
			}
			set
			{
				m_XPositionAdjustment = value;
			}
		}

		public float yPositionAdjustment
		{
			get
			{
				return m_YPositionAdjustment;
			}
			set
			{
				m_YPositionAdjustment = value;
			}
		}

		public MarkPositionAdjustment(float x, float y)
		{
			m_XPositionAdjustment = x;
			m_YPositionAdjustment = y;
		}
	}
	[Serializable]
	[UsedByNativeCode]
	internal struct MarkToBaseAdjustmentRecord
	{
		[SerializeField]
		[NativeName("baseGlyphID")]
		private uint m_BaseGlyphID;

		[NativeName("baseAnchor")]
		[SerializeField]
		private GlyphAnchorPoint m_BaseGlyphAnchorPoint;

		[SerializeField]
		[NativeName("markGlyphID")]
		private uint m_MarkGlyphID;

		[NativeName("markPositionAdjustment")]
		[SerializeField]
		private MarkPositionAdjustment m_MarkPositionAdjustment;

		public uint baseGlyphID
		{
			get
			{
				return m_BaseGlyphID;
			}
			set
			{
				m_BaseGlyphID = value;
			}
		}

		public GlyphAnchorPoint baseGlyphAnchorPoint
		{
			get
			{
				return m_BaseGlyphAnchorPoint;
			}
			set
			{
				m_BaseGlyphAnchorPoint = value;
			}
		}

		public uint markGlyphID
		{
			get
			{
				return m_MarkGlyphID;
			}
			set
			{
				m_MarkGlyphID = value;
			}
		}

		public MarkPositionAdjustment markPositionAdjustment
		{
			get
			{
				return m_MarkPositionAdjustment;
			}
			set
			{
				m_MarkPositionAdjustment = value;
			}
		}
	}
	[Serializable]
	[UsedByNativeCode]
	internal struct MarkToMarkAdjustmentRecord
	{
		[SerializeField]
		[NativeName("baseMarkGlyphID")]
		private uint m_BaseMarkGlyphID;

		[SerializeField]
		[NativeName("baseMarkAnchor")]
		private GlyphAnchorPoint m_BaseMarkGlyphAnchorPoint;

		[NativeName("combiningMarkGlyphID")]
		[SerializeField]
		private uint m_CombiningMarkGlyphID;

		[NativeName("combiningMarkPositionAdjustment")]
		[SerializeField]
		private MarkPositionAdjustment m_CombiningMarkPositionAdjustment;

		public uint baseMarkGlyphID
		{
			get
			{
				return m_BaseMarkGlyphID;
			}
			set
			{
				m_BaseMarkGlyphID = value;
			}
		}

		public GlyphAnchorPoint baseMarkGlyphAnchorPoint
		{
			get
			{
				return m_BaseMarkGlyphAnchorPoint;
			}
			set
			{
				m_BaseMarkGlyphAnchorPoint = value;
			}
		}

		public uint combiningMarkGlyphID
		{
			get
			{
				return m_CombiningMarkGlyphID;
			}
			set
			{
				m_CombiningMarkGlyphID = value;
			}
		}

		public MarkPositionAdjustment combiningMarkPositionAdjustment
		{
			get
			{
				return m_CombiningMarkPositionAdjustment;
			}
			set
			{
				m_CombiningMarkPositionAdjustment = value;
			}
		}
	}
	[Serializable]
	[UsedByNativeCode]
	internal struct SingleSubstitutionRecord
	{
		[NativeName("targetGlyphID")]
		[SerializeField]
		private uint m_TargetGlyphID;

		[SerializeField]
		[NativeName("substituteGlyphID")]
		private uint m_SubstituteGlyphID;

		public uint targetGlyphID
		{
			get
			{
				return m_TargetGlyphID;
			}
			set
			{
				m_TargetGlyphID = value;
			}
		}

		public uint substituteGlyphID
		{
			get
			{
				return m_SubstituteGlyphID;
			}
			set
			{
				m_SubstituteGlyphID = value;
			}
		}
	}
	[Serializable]
	[UsedByNativeCode]
	internal struct MultipleSubstitutionRecord
	{
		[NativeName("targetGlyphID")]
		[SerializeField]
		private uint m_TargetGlyphID;

		[NativeName("substituteGlyphIDs")]
		[SerializeField]
		private uint[] m_SubstituteGlyphIDs;

		public uint targetGlyphID
		{
			get
			{
				return m_TargetGlyphID;
			}
			set
			{
				m_TargetGlyphID = value;
			}
		}

		public uint[] substituteGlyphIDs
		{
			get
			{
				return m_SubstituteGlyphIDs;
			}
			set
			{
				m_SubstituteGlyphIDs = value;
			}
		}
	}
	[Serializable]
	[UsedByNativeCode]
	internal struct AlternateSubstitutionRecord
	{
		[SerializeField]
		[NativeName("targetGlyphID")]
		private uint m_TargetGlyphID;

		[SerializeField]
		[NativeName("substituteGlyphIDs")]
		private uint[] m_SubstituteGlyphIDs;

		public uint targetGlyphID
		{
			get
			{
				return m_TargetGlyphID;
			}
			set
			{
				m_TargetGlyphID = value;
			}
		}

		public uint[] substituteGlyphIDs
		{
			get
			{
				return m_SubstituteGlyphIDs;
			}
			set
			{
				m_SubstituteGlyphIDs = value;
			}
		}
	}
	[Serializable]
	[UsedByNativeCode]
	internal struct LigatureSubstitutionRecord
	{
		[SerializeField]
		[NativeName("componentGlyphs")]
		private uint[] m_ComponentGlyphIDs;

		[SerializeField]
		[NativeName("ligatureGlyph")]
		private uint m_LigatureGlyphID;

		public uint[] componentGlyphIDs
		{
			get
			{
				return m_ComponentGlyphIDs;
			}
			set
			{
				m_ComponentGlyphIDs = value;
			}
		}

		public uint ligatureGlyphID
		{
			get
			{
				return m_LigatureGlyphID;
			}
			set
			{
				m_LigatureGlyphID = value;
			}
		}
	}
	[Serializable]
	[UsedByNativeCode]
	internal struct GlyphIDSequence
	{
		[SerializeField]
		[NativeName("glyphIDs")]
		private uint[] m_GlyphIDs;

		public uint[] glyphIDs
		{
			get
			{
				return m_GlyphIDs;
			}
			set
			{
				m_GlyphIDs = value;
			}
		}
	}
	[Serializable]
	[UsedByNativeCode]
	internal struct SequenceLookupRecord
	{
		[NativeName("glyphSequenceIndex")]
		[SerializeField]
		private uint m_GlyphSequenceIndex;

		[SerializeField]
		[NativeName("lookupListIndex")]
		private uint m_LookupListIndex;

		public uint glyphSequenceIndex
		{
			get
			{
				return m_GlyphSequenceIndex;
			}
			set
			{
				m_GlyphSequenceIndex = value;
			}
		}

		public uint lookupListIndex
		{
			get
			{
				return m_LookupListIndex;
			}
			set
			{
				m_LookupListIndex = value;
			}
		}
	}
	[Serializable]
	[UsedByNativeCode]
	internal struct ContextualSubstitutionRecord
	{
		[SerializeField]
		[NativeName("inputGlyphSequences")]
		private GlyphIDSequence[] m_InputGlyphSequences;

		[SerializeField]
		[NativeName("sequenceLookupRecords")]
		private SequenceLookupRecord[] m_SequenceLookupRecords;

		public GlyphIDSequence[] inputSequences
		{
			get
			{
				return m_InputGlyphSequences;
			}
			set
			{
				m_InputGlyphSequences = value;
			}
		}

		public SequenceLookupRecord[] sequenceLookupRecords
		{
			get
			{
				return m_SequenceLookupRecords;
			}
			set
			{
				m_SequenceLookupRecords = value;
			}
		}
	}
	[Serializable]
	[UsedByNativeCode]
	internal struct ChainingContextualSubstitutionRecord
	{
		[SerializeField]
		[NativeName("backtrackGlyphSequences")]
		private GlyphIDSequence[] m_BacktrackGlyphSequences;

		[SerializeField]
		[NativeName("inputGlyphSequences")]
		private GlyphIDSequence[] m_InputGlyphSequences;

		[NativeName("lookaheadGlyphSequences")]
		[SerializeField]
		private GlyphIDSequence[] m_LookaheadGlyphSequences;

		[SerializeField]
		[NativeName("sequenceLookupRecords")]
		private SequenceLookupRecord[] m_SequenceLookupRecords;

		public GlyphIDSequence[] backtrackGlyphSequences
		{
			get
			{
				return m_BacktrackGlyphSequences;
			}
			set
			{
				m_BacktrackGlyphSequences = value;
			}
		}

		public GlyphIDSequence[] inputGlyphSequences
		{
			get
			{
				return m_InputGlyphSequences;
			}
			set
			{
				m_InputGlyphSequences = value;
			}
		}

		public GlyphIDSequence[] lookaheadGlyphSequences
		{
			get
			{
				return m_LookaheadGlyphSequences;
			}
			set
			{
				m_LookaheadGlyphSequences = value;
			}
		}

		public SequenceLookupRecord[] sequenceLookupRecords
		{
			get
			{
				return m_SequenceLookupRecords;
			}
			set
			{
				m_SequenceLookupRecords = value;
			}
		}
	}
}
