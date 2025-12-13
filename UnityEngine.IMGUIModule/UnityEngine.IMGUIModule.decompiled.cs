using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using UnityEngine.TextCore.Text;
using UnityEngineInternal;

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
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine;

[StructLayout(LayoutKind.Sequential)]
[StaticAccessor("GUIEvent", StaticAccessorType.DoubleColon)]
[NativeHeader("Modules/IMGUI/Event.bindings.h")]
public sealed class Event
{
	[NonSerialized]
	internal IntPtr m_Ptr;

	private static Event s_Current;

	private static Event s_MasterEvent;

	[NativeProperty("type", false, TargetType.Field)]
	public extern EventType rawType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[NativeProperty("mousePosition", false, TargetType.Field)]
	public Vector2 mousePosition
	{
		get
		{
			get_mousePosition_Injected(out var ret);
			return ret;
		}
		set
		{
			set_mousePosition_Injected(ref value);
		}
	}

	[NativeProperty("delta", false, TargetType.Field)]
	public Vector2 delta
	{
		get
		{
			get_delta_Injected(out var ret);
			return ret;
		}
		set
		{
			set_delta_Injected(ref value);
		}
	}

	[NativeProperty("pointerType", false, TargetType.Field)]
	public extern PointerType pointerType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("button", false, TargetType.Field)]
	public extern int button
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("modifiers", false, TargetType.Field)]
	public extern EventModifiers modifiers
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("pressure", false, TargetType.Field)]
	public extern float pressure
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("twist", false, TargetType.Field)]
	public extern float twist
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("tilt", false, TargetType.Field)]
	public Vector2 tilt
	{
		get
		{
			get_tilt_Injected(out var ret);
			return ret;
		}
		set
		{
			set_tilt_Injected(ref value);
		}
	}

	[NativeProperty("penStatus", false, TargetType.Field)]
	public extern PenStatus penStatus
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("clickCount", false, TargetType.Field)]
	public extern int clickCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("character", false, TargetType.Field)]
	public extern char character
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("keycode", false, TargetType.Field)]
	public extern KeyCode keyCode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("displayIndex", false, TargetType.Field)]
	public extern int displayIndex
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern EventType type
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GUIEvent::GetType", HasExplicitThis = true)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GUIEvent::SetType", HasExplicitThis = true)]
		set;
	}

	public extern string commandName
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GUIEvent::GetCommandName", HasExplicitThis = true)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GUIEvent::SetCommandName", HasExplicitThis = true)]
		set;
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);", true)]
	public Ray mouseRay
	{
		get
		{
			return new Ray(Vector3.up, Vector3.up);
		}
		set
		{
		}
	}

	public bool shift
	{
		get
		{
			return (modifiers & EventModifiers.Shift) != 0;
		}
		set
		{
			if (!value)
			{
				modifiers &= ~EventModifiers.Shift;
			}
			else
			{
				modifiers |= EventModifiers.Shift;
			}
		}
	}

	public bool control
	{
		get
		{
			return (modifiers & EventModifiers.Control) != 0;
		}
		set
		{
			if (!value)
			{
				modifiers &= ~EventModifiers.Control;
			}
			else
			{
				modifiers |= EventModifiers.Control;
			}
		}
	}

	public bool alt
	{
		get
		{
			return (modifiers & EventModifiers.Alt) != 0;
		}
		set
		{
			if (!value)
			{
				modifiers &= ~EventModifiers.Alt;
			}
			else
			{
				modifiers |= EventModifiers.Alt;
			}
		}
	}

	public bool command
	{
		get
		{
			return (modifiers & EventModifiers.Command) != 0;
		}
		set
		{
			if (!value)
			{
				modifiers &= ~EventModifiers.Command;
			}
			else
			{
				modifiers |= EventModifiers.Command;
			}
		}
	}

	public bool capsLock
	{
		get
		{
			return (modifiers & EventModifiers.CapsLock) != 0;
		}
		set
		{
			if (!value)
			{
				modifiers &= ~EventModifiers.CapsLock;
			}
			else
			{
				modifiers |= EventModifiers.CapsLock;
			}
		}
	}

	public bool numeric
	{
		get
		{
			return (modifiers & EventModifiers.Numeric) != 0;
		}
		set
		{
			if (!value)
			{
				modifiers &= ~EventModifiers.Numeric;
			}
			else
			{
				modifiers |= EventModifiers.Numeric;
			}
		}
	}

	public bool functionKey => (modifiers & EventModifiers.FunctionKey) != 0;

	public static Event current
	{
		get
		{
			return s_Current;
		}
		set
		{
			s_Current = value ?? s_MasterEvent;
			Internal_SetNativeEvent(s_Current.m_Ptr);
		}
	}

	public bool isKey
	{
		get
		{
			EventType eventType = type;
			return eventType == EventType.KeyDown || eventType == EventType.KeyUp;
		}
	}

	public bool isMouse
	{
		get
		{
			EventType eventType = type;
			return eventType == EventType.MouseMove || eventType == EventType.MouseDown || eventType == EventType.MouseUp || eventType == EventType.MouseDrag || eventType == EventType.ContextClick || eventType == EventType.MouseEnterWindow || eventType == EventType.MouseLeaveWindow;
		}
	}

	public bool isScrollWheel
	{
		get
		{
			EventType eventType = type;
			return eventType == EventType.ScrollWheel;
		}
	}

	internal bool isDirectManipulationDevice => pointerType == PointerType.Pen || pointerType == PointerType.Touch;

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("Use")]
	private extern void Internal_Use();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("GUIEvent::Internal_Create", IsThreadSafe = true)]
	private static extern IntPtr Internal_Create(int displayIndex);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("GUIEvent::Internal_Destroy", IsThreadSafe = true)]
	private static extern void Internal_Destroy(IntPtr ptr);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("GUIEvent::Internal_Copy", IsThreadSafe = true)]
	private static extern IntPtr Internal_Copy(IntPtr otherPtr);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("GUIEvent::GetTypeForControl", HasExplicitThis = true)]
	public extern EventType GetTypeForControl(int controlID);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	[FreeFunction("GUIEvent::CopyFromPtr", IsThreadSafe = true, HasExplicitThis = true)]
	internal extern void CopyFromPtr(IntPtr ptr);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern bool PopEvent([NotNull("ArgumentNullException")] Event outEvent);

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern void QueueEvent([NotNull("ArgumentNullException")] Event outEvent);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern int GetEventCount();

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern void ClearEvents();

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_SetNativeEvent(IntPtr ptr);

	[RequiredByNativeCode]
	internal static void Internal_MakeMasterEventCurrent(int displayIndex)
	{
		if (s_MasterEvent == null)
		{
			s_MasterEvent = new Event(displayIndex);
		}
		s_MasterEvent.displayIndex = displayIndex;
		s_Current = s_MasterEvent;
		Internal_SetNativeEvent(s_MasterEvent.m_Ptr);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static extern int GetDoubleClickTime();

	public Event()
	{
		m_Ptr = Internal_Create(0);
	}

	public Event(int displayIndex)
	{
		m_Ptr = Internal_Create(displayIndex);
	}

	public Event(Event other)
	{
		if (other == null)
		{
			throw new ArgumentException("Event to copy from is null.");
		}
		m_Ptr = Internal_Copy(other.m_Ptr);
	}

	~Event()
	{
		if (m_Ptr != IntPtr.Zero)
		{
			Internal_Destroy(m_Ptr);
			m_Ptr = IntPtr.Zero;
		}
	}

	internal static void CleanupRoots()
	{
		s_Current = null;
		s_MasterEvent = null;
	}

	internal void CopyFrom(Event e)
	{
		if (e.m_Ptr != m_Ptr)
		{
			CopyFromPtr(e.m_Ptr);
		}
	}

	public static Event KeyboardEvent(string key)
	{
		Event obj = new Event(0)
		{
			type = EventType.KeyDown
		};
		if (string.IsNullOrEmpty(key))
		{
			return obj;
		}
		int num = 0;
		bool flag = false;
		do
		{
			flag = true;
			if (num >= key.Length)
			{
				flag = false;
				break;
			}
			switch (key[num])
			{
			case '&':
				obj.modifiers |= EventModifiers.Alt;
				num++;
				break;
			case '^':
				obj.modifiers |= EventModifiers.Control;
				num++;
				break;
			case '%':
				obj.modifiers |= EventModifiers.Command;
				num++;
				break;
			case '#':
				obj.modifiers |= EventModifiers.Shift;
				num++;
				break;
			default:
				flag = false;
				break;
			}
		}
		while (flag);
		string text = key.Substring(num, key.Length - num).ToLowerInvariant();
		switch (text)
		{
		case "[0]":
			obj.character = '0';
			obj.keyCode = KeyCode.Keypad0;
			break;
		case "[1]":
			obj.character = '1';
			obj.keyCode = KeyCode.Keypad1;
			break;
		case "[2]":
			obj.character = '2';
			obj.keyCode = KeyCode.Keypad2;
			break;
		case "[3]":
			obj.character = '3';
			obj.keyCode = KeyCode.Keypad3;
			break;
		case "[4]":
			obj.character = '4';
			obj.keyCode = KeyCode.Keypad4;
			break;
		case "[5]":
			obj.character = '5';
			obj.keyCode = KeyCode.Keypad5;
			break;
		case "[6]":
			obj.character = '6';
			obj.keyCode = KeyCode.Keypad6;
			break;
		case "[7]":
			obj.character = '7';
			obj.keyCode = KeyCode.Keypad7;
			break;
		case "[8]":
			obj.character = '8';
			obj.keyCode = KeyCode.Keypad8;
			break;
		case "[9]":
			obj.character = '9';
			obj.keyCode = KeyCode.Keypad9;
			break;
		case "[.]":
			obj.character = '.';
			obj.keyCode = KeyCode.KeypadPeriod;
			break;
		case "[/]":
			obj.character = '/';
			obj.keyCode = KeyCode.KeypadDivide;
			break;
		case "[-]":
			obj.character = '-';
			obj.keyCode = KeyCode.KeypadMinus;
			break;
		case "[+]":
			obj.character = '+';
			obj.keyCode = KeyCode.KeypadPlus;
			break;
		case "[=]":
			obj.character = '=';
			obj.keyCode = KeyCode.KeypadEquals;
			break;
		case "[equals]":
			obj.character = '=';
			obj.keyCode = KeyCode.KeypadEquals;
			break;
		case "[enter]":
			obj.character = '\n';
			obj.keyCode = KeyCode.KeypadEnter;
			break;
		case "up":
			obj.keyCode = KeyCode.UpArrow;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "down":
			obj.keyCode = KeyCode.DownArrow;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "left":
			obj.keyCode = KeyCode.LeftArrow;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "right":
			obj.keyCode = KeyCode.RightArrow;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "insert":
			obj.keyCode = KeyCode.Insert;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "home":
			obj.keyCode = KeyCode.Home;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "end":
			obj.keyCode = KeyCode.End;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "pgup":
			obj.keyCode = KeyCode.PageDown;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "page up":
			obj.keyCode = KeyCode.PageUp;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "pgdown":
			obj.keyCode = KeyCode.PageUp;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "page down":
			obj.keyCode = KeyCode.PageDown;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "backspace":
			obj.keyCode = KeyCode.Backspace;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "delete":
			obj.keyCode = KeyCode.Delete;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "tab":
			obj.keyCode = KeyCode.Tab;
			break;
		case "f1":
			obj.keyCode = KeyCode.F1;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "f2":
			obj.keyCode = KeyCode.F2;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "f3":
			obj.keyCode = KeyCode.F3;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "f4":
			obj.keyCode = KeyCode.F4;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "f5":
			obj.keyCode = KeyCode.F5;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "f6":
			obj.keyCode = KeyCode.F6;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "f7":
			obj.keyCode = KeyCode.F7;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "f8":
			obj.keyCode = KeyCode.F8;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "f9":
			obj.keyCode = KeyCode.F9;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "f10":
			obj.keyCode = KeyCode.F10;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "f11":
			obj.keyCode = KeyCode.F11;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "f12":
			obj.keyCode = KeyCode.F12;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "f13":
			obj.keyCode = KeyCode.F13;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "f14":
			obj.keyCode = KeyCode.F14;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "f15":
			obj.keyCode = KeyCode.F15;
			obj.modifiers |= EventModifiers.FunctionKey;
			break;
		case "[esc]":
			obj.keyCode = KeyCode.Escape;
			break;
		case "return":
			obj.character = '\n';
			obj.keyCode = KeyCode.Return;
			obj.modifiers &= ~EventModifiers.FunctionKey;
			break;
		case "space":
			obj.keyCode = KeyCode.Space;
			obj.character = ' ';
			obj.modifiers &= ~EventModifiers.FunctionKey;
			break;
		default:
			if (text.Length != 1)
			{
				try
				{
					obj.keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), text, ignoreCase: true);
				}
				catch (ArgumentException)
				{
					Debug.LogError(UnityString.Format("Unable to find key name that matches '{0}'", text));
				}
			}
			else
			{
				obj.character = text.ToLower()[0];
				obj.keyCode = (KeyCode)obj.character;
				if (obj.modifiers != EventModifiers.None)
				{
					obj.character = '\0';
				}
			}
			break;
		}
		return obj;
	}

	public override int GetHashCode()
	{
		int num = 1;
		if (isKey)
		{
			num = (ushort)keyCode;
		}
		if (isMouse)
		{
			num = mousePosition.GetHashCode();
		}
		return (num * 37) | (int)modifiers;
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
		Event obj2 = (Event)obj;
		if (type != obj2.type || (modifiers & ~EventModifiers.CapsLock) != (obj2.modifiers & ~EventModifiers.CapsLock))
		{
			return false;
		}
		if (isKey)
		{
			return keyCode == obj2.keyCode;
		}
		if (isMouse)
		{
			return mousePosition == obj2.mousePosition;
		}
		return false;
	}

	public override string ToString()
	{
		if (isKey)
		{
			if (character == '\0')
			{
				return UnityString.Format("Event:{0}   Character:\\0   Modifiers:{1}   KeyCode:{2}", type, modifiers, keyCode);
			}
			return "Event:" + type.ToString() + "   Character:" + (int)character + "   Modifiers:" + modifiers.ToString() + "   KeyCode:" + keyCode;
		}
		if (isMouse)
		{
			return UnityString.Format("Event: {0}   Position: {1} Modifiers: {2}", type, mousePosition, modifiers);
		}
		if (type == EventType.ExecuteCommand || type == EventType.ValidateCommand)
		{
			return UnityString.Format("Event: {0}  \"{1}\"", type, commandName);
		}
		return type.ToString() ?? "";
	}

	public void Use()
	{
		if (type == EventType.Repaint || type == EventType.Layout)
		{
			Debug.LogWarning(UnityString.Format("Event.Use() should not be called for events of type {0}", type));
		}
		Internal_Use();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_mousePosition_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_mousePosition_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_delta_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_delta_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_tilt_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_tilt_Injected(ref Vector2 value);
}
internal static class EventCommandNames
{
	public const string Cut = "Cut";

	public const string Copy = "Copy";

	public const string Paste = "Paste";

	public const string SelectAll = "SelectAll";

	public const string DeselectAll = "DeselectAll";

	public const string InvertSelection = "InvertSelection";

	public const string Duplicate = "Duplicate";

	public const string Rename = "Rename";

	public const string Delete = "Delete";

	public const string SoftDelete = "SoftDelete";

	public const string Find = "Find";

	public const string SelectChildren = "SelectChildren";

	public const string SelectPrefabRoot = "SelectPrefabRoot";

	public const string UndoRedoPerformed = "UndoRedoPerformed";

	public const string OnLostFocus = "OnLostFocus";

	public const string NewKeyboardFocus = "NewKeyboardFocus";

	public const string ModifierKeysChanged = "ModifierKeysChanged";

	public const string EyeDropperUpdate = "EyeDropperUpdate";

	public const string EyeDropperClicked = "EyeDropperClicked";

	public const string EyeDropperCancelled = "EyeDropperCancelled";

	public const string ColorPickerChanged = "ColorPickerChanged";

	public const string FrameSelected = "FrameSelected";

	public const string FrameSelectedWithLock = "FrameSelectedWithLock";
}
public enum EventType
{
	MouseDown = 0,
	MouseUp = 1,
	MouseMove = 2,
	MouseDrag = 3,
	KeyDown = 4,
	KeyUp = 5,
	ScrollWheel = 6,
	Repaint = 7,
	Layout = 8,
	DragUpdated = 9,
	DragPerform = 10,
	DragExited = 15,
	Ignore = 11,
	Used = 12,
	ValidateCommand = 13,
	ExecuteCommand = 14,
	ContextClick = 16,
	MouseEnterWindow = 20,
	MouseLeaveWindow = 21,
	TouchDown = 30,
	TouchUp = 31,
	TouchMove = 32,
	TouchEnter = 33,
	TouchLeave = 34,
	TouchStationary = 35,
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use MouseDown instead (UnityUpgradable) -> MouseDown", true)]
	mouseDown = 0,
	[Obsolete("Use MouseUp instead (UnityUpgradable) -> MouseUp", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	mouseUp = 1,
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use MouseMove instead (UnityUpgradable) -> MouseMove", true)]
	mouseMove = 2,
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use MouseDrag instead (UnityUpgradable) -> MouseDrag", true)]
	mouseDrag = 3,
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use KeyDown instead (UnityUpgradable) -> KeyDown", true)]
	keyDown = 4,
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use KeyUp instead (UnityUpgradable) -> KeyUp", true)]
	keyUp = 5,
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use ScrollWheel instead (UnityUpgradable) -> ScrollWheel", true)]
	scrollWheel = 6,
	[Obsolete("Use Repaint instead (UnityUpgradable) -> Repaint", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	repaint = 7,
	[Obsolete("Use Layout instead (UnityUpgradable) -> Layout", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	layout = 8,
	[Obsolete("Use DragUpdated instead (UnityUpgradable) -> DragUpdated", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	dragUpdated = 9,
	[Obsolete("Use DragPerform instead (UnityUpgradable) -> DragPerform", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	dragPerform = 10,
	[Obsolete("Use Ignore instead (UnityUpgradable) -> Ignore", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	ignore = 11,
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use Used instead (UnityUpgradable) -> Used", true)]
	used = 12
}
[Flags]
public enum EventModifiers
{
	None = 0,
	Shift = 1,
	Control = 2,
	Alt = 4,
	Command = 8,
	Numeric = 0x10,
	CapsLock = 0x20,
	FunctionKey = 0x40
}
public enum PointerType
{
	Mouse,
	Touch,
	Pen
}
internal struct EventInterests
{
	public bool wantsMouseMove { get; set; }

	public bool wantsMouseEnterLeaveWindow { get; set; }

	public bool wantsLessLayoutEvents { get; set; }

	public bool WantsEvent(EventType type)
	{
		switch (type)
		{
		case EventType.MouseMove:
			return wantsMouseMove;
		case EventType.MouseEnterWindow:
		case EventType.MouseLeaveWindow:
			return wantsMouseEnterLeaveWindow;
		default:
			return true;
		}
	}

	public bool WantsLayoutPass(EventType type)
	{
		if (!wantsLessLayoutEvents)
		{
			return true;
		}
		switch (type)
		{
		case EventType.Repaint:
		case EventType.ExecuteCommand:
			return true;
		case EventType.KeyDown:
		case EventType.KeyUp:
			return GUIUtility.textFieldInput;
		case EventType.MouseDown:
		case EventType.MouseUp:
			return wantsMouseMove;
		case EventType.MouseEnterWindow:
		case EventType.MouseLeaveWindow:
			return wantsMouseEnterLeaveWindow;
		default:
			return false;
		}
	}
}
[NativeHeader("Modules/IMGUI/GUI.bindings.h")]
[NativeHeader("Modules/IMGUI/GUISkin.bindings.h")]
public class GUI
{
	public enum ToolbarButtonSize
	{
		Fixed,
		FitToContents
	}

	internal delegate void CustomSelectionGridItemGUI(int item, Rect rect, GUIStyle style, int controlID);

	public delegate void WindowFunction(int id);

	public abstract class Scope : IDisposable
	{
		private bool m_Disposed;

		internal virtual void Dispose(bool disposing)
		{
			if (!m_Disposed)
			{
				if (disposing && !GUIUtility.guiIsExiting)
				{
					CloseScope();
				}
				m_Disposed = true;
			}
		}

		~Scope()
		{
			if (!m_Disposed && !GUIUtility.guiIsExiting)
			{
				Console.WriteLine(GetType().Name + " was not disposed! You should use the 'using' keyword or manually call Dispose.");
			}
			Dispose(disposing: false);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected abstract void CloseScope();
	}

	public class GroupScope : Scope
	{
		public GroupScope(Rect position)
		{
			BeginGroup(position);
		}

		public GroupScope(Rect position, string text)
		{
			BeginGroup(position, text);
		}

		public GroupScope(Rect position, Texture image)
		{
			BeginGroup(position, image);
		}

		public GroupScope(Rect position, GUIContent content)
		{
			BeginGroup(position, content);
		}

		public GroupScope(Rect position, GUIStyle style)
		{
			BeginGroup(position, style);
		}

		public GroupScope(Rect position, string text, GUIStyle style)
		{
			BeginGroup(position, text, style);
		}

		public GroupScope(Rect position, Texture image, GUIStyle style)
		{
			BeginGroup(position, image, style);
		}

		protected override void CloseScope()
		{
			EndGroup();
		}
	}

	public class ScrollViewScope : Scope
	{
		public Vector2 scrollPosition { get; private set; }

		public bool handleScrollWheel { get; set; }

		public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect)
		{
			handleScrollWheel = true;
			this.scrollPosition = BeginScrollView(position, scrollPosition, viewRect);
		}

		public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
		{
			handleScrollWheel = true;
			this.scrollPosition = BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical);
		}

		public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			handleScrollWheel = true;
			this.scrollPosition = BeginScrollView(position, scrollPosition, viewRect, horizontalScrollbar, verticalScrollbar);
		}

		public ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			handleScrollWheel = true;
			this.scrollPosition = BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar);
		}

		internal ScrollViewScope(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			handleScrollWheel = true;
			this.scrollPosition = BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
		}

		protected override void CloseScope()
		{
			EndScrollView(handleScrollWheel);
		}
	}

	public class ClipScope : Scope
	{
		public ClipScope(Rect position)
		{
			BeginClip(position);
		}

		internal ClipScope(Rect position, Vector2 scrollOffset)
		{
			BeginClip(position, scrollOffset, default(Vector2), resetOffset: false);
		}

		protected override void CloseScope()
		{
			EndClip();
		}
	}

	internal struct ColorScope : IDisposable
	{
		private bool m_Disposed;

		private Color m_PreviousColor;

		public ColorScope(Color newColor)
		{
			m_Disposed = false;
			m_PreviousColor = color;
			color = newColor;
		}

		public ColorScope(float r, float g, float b, float a = 1f)
			: this(new Color(r, g, b, a))
		{
		}

		public void Dispose()
		{
			if (!m_Disposed)
			{
				m_Disposed = true;
				color = m_PreviousColor;
			}
		}
	}

	internal struct BackgroundColorScope : IDisposable
	{
		private bool m_Disposed;

		private Color m_PreviousColor;

		public BackgroundColorScope(Color newColor)
		{
			m_Disposed = false;
			m_PreviousColor = backgroundColor;
			backgroundColor = newColor;
		}

		public BackgroundColorScope(float r, float g, float b, float a = 1f)
			: this(new Color(r, g, b, a))
		{
		}

		public void Dispose()
		{
			if (!m_Disposed)
			{
				m_Disposed = true;
				backgroundColor = m_PreviousColor;
			}
		}
	}

	private const float s_ScrollStepSize = 10f;

	private static int s_ScrollControlId;

	private static int s_HotTextField;

	private static readonly int s_BoxHash;

	private static readonly int s_ButonHash;

	private static readonly int s_RepeatButtonHash;

	private static readonly int s_ToggleHash;

	private static readonly int s_ButtonGridHash;

	private static readonly int s_SliderHash;

	private static readonly int s_BeginGroupHash;

	private static readonly int s_ScrollviewHash;

	private static GUISkin s_Skin;

	internal static Rect s_ToolTipRect;

	public static Color color
	{
		get
		{
			get_color_Injected(out var ret);
			return ret;
		}
		set
		{
			set_color_Injected(ref value);
		}
	}

	public static Color backgroundColor
	{
		get
		{
			get_backgroundColor_Injected(out var ret);
			return ret;
		}
		set
		{
			set_backgroundColor_Injected(ref value);
		}
	}

	public static Color contentColor
	{
		get
		{
			get_contentColor_Injected(out var ret);
			return ret;
		}
		set
		{
			set_contentColor_Injected(ref value);
		}
	}

	public static extern bool changed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern bool enabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern int depth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	internal static extern bool usePageScrollbars
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	internal static extern bool isInsideList
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	internal static extern Material blendMaterial
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetGUIBlendMaterial")]
		get;
	}

	internal static extern Material blitMaterial
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetGUIBlitMaterial")]
		get;
	}

	internal static extern Material roundedRectMaterial
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetGUIRoundedRectMaterial")]
		get;
	}

	internal static extern Material roundedRectWithColorPerBorderMaterial
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetGUIRoundedRectWithColorPerBorderMaterial")]
		get;
	}

	internal static int scrollTroughSide { get; set; }

	internal static DateTime nextScrollStepTime { get; set; }

	public static GUISkin skin
	{
		get
		{
			GUIUtility.CheckOnGUI();
			return s_Skin;
		}
		set
		{
			GUIUtility.CheckOnGUI();
			DoSetSkin(value);
		}
	}

	public static Matrix4x4 matrix
	{
		get
		{
			return GUIClip.GetMatrix();
		}
		set
		{
			GUIClip.SetMatrix(value);
		}
	}

	public static string tooltip
	{
		get
		{
			string text = Internal_GetTooltip();
			if (text != null)
			{
				return text;
			}
			return "";
		}
		set
		{
			Internal_SetTooltip(value);
		}
	}

	protected static string mouseTooltip => Internal_GetMouseTooltip();

	protected static Rect tooltipRect
	{
		get
		{
			return s_ToolTipRect;
		}
		set
		{
			s_ToolTipRect = value;
		}
	}

	internal static GenericStack scrollViewStates { get; set; }

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern void GrabMouseControl(int id);

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern bool HasMouseControl(int id);

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern void ReleaseMouseControl();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("GetGUIState().SetNameOfNextControl")]
	public static extern void SetNextControlName(string name);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("GetGUIState().GetNameOfFocusedControl")]
	public static extern string GetNameOfFocusedControl();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("GetGUIState().FocusKeyboardControl")]
	public static extern void FocusControl(string name);

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern void InternalRepaintEditorWindow();

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern string Internal_GetTooltip();

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_SetTooltip(string value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern string Internal_GetMouseTooltip();

	private static Rect Internal_DoModalWindow(int id, int instanceID, Rect clientRect, WindowFunction func, GUIContent content, GUIStyle style, object skin)
	{
		Internal_DoModalWindow_Injected(id, instanceID, ref clientRect, func, content, style, skin, out var ret);
		return ret;
	}

	private static Rect Internal_DoWindow(int id, int instanceID, Rect clientRect, WindowFunction func, GUIContent title, GUIStyle style, object skin, bool forceRectOnLayout)
	{
		Internal_DoWindow_Injected(id, instanceID, ref clientRect, func, title, style, skin, forceRectOnLayout, out var ret);
		return ret;
	}

	public static void DragWindow(Rect position)
	{
		DragWindow_Injected(ref position);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern void BringWindowToFront(int windowID);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern void BringWindowToBack(int windowID);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern void FocusWindow(int windowID);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern void UnfocusWindow();

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_BeginWindows();

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_EndWindows();

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern string Internal_Concatenate(GUIContent first, GUIContent second);

	static GUI()
	{
		s_HotTextField = -1;
		s_BoxHash = "Box".GetHashCode();
		s_ButonHash = "Button".GetHashCode();
		s_RepeatButtonHash = "repeatButton".GetHashCode();
		s_ToggleHash = "Toggle".GetHashCode();
		s_ButtonGridHash = "ButtonGrid".GetHashCode();
		s_SliderHash = "Slider".GetHashCode();
		s_BeginGroupHash = "BeginGroup".GetHashCode();
		s_ScrollviewHash = "scrollView".GetHashCode();
		scrollViewStates = new GenericStack();
		nextScrollStepTime = DateTime.Now;
	}

	internal static void DoSetSkin(GUISkin newSkin)
	{
		if (!newSkin)
		{
			newSkin = GUIUtility.GetDefaultSkin();
		}
		s_Skin = newSkin;
		newSkin.MakeCurrent();
	}

	internal static void CleanupRoots()
	{
		s_Skin = null;
		GUIUtility.CleanupRoots();
		GUILayoutUtility.CleanupRoots();
		GUISkin.CleanupRoots();
		GUIStyle.CleanupRoots();
	}

	public static void Label(Rect position, string text)
	{
		Label(position, GUIContent.Temp(text), s_Skin.label);
	}

	public static void Label(Rect position, Texture image)
	{
		Label(position, GUIContent.Temp(image), s_Skin.label);
	}

	public static void Label(Rect position, GUIContent content)
	{
		Label(position, content, s_Skin.label);
	}

	public static void Label(Rect position, string text, GUIStyle style)
	{
		Label(position, GUIContent.Temp(text), style);
	}

	public static void Label(Rect position, Texture image, GUIStyle style)
	{
		Label(position, GUIContent.Temp(image), style);
	}

	public static void Label(Rect position, GUIContent content, GUIStyle style)
	{
		GUIUtility.CheckOnGUI();
		DoLabel(position, content, style);
	}

	public static void DrawTexture(Rect position, Texture image)
	{
		DrawTexture(position, image, ScaleMode.StretchToFill);
	}

	public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode)
	{
		DrawTexture(position, image, scaleMode, alphaBlend: true);
	}

	public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend)
	{
		DrawTexture(position, image, scaleMode, alphaBlend, 0f);
	}

	public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect)
	{
		DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, 0f, 0f);
	}

	public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, float borderWidth, float borderRadius)
	{
		Vector4 borderWidths = Vector4.one * borderWidth;
		DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, borderWidths, borderRadius);
	}

	public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, Vector4 borderWidths, float borderRadius)
	{
		Vector4 borderRadiuses = Vector4.one * borderRadius;
		DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, borderWidths, borderRadiuses);
	}

	public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, Vector4 borderWidths, Vector4 borderRadiuses)
	{
		DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, borderWidths, borderRadiuses, drawSmoothCorners: true);
	}

	internal static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color color, Vector4 borderWidths, Vector4 borderRadiuses, bool drawSmoothCorners)
	{
		DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, color, color, color, color, borderWidths, borderRadiuses, drawSmoothCorners);
	}

	internal static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color leftColor, Color topColor, Color rightColor, Color bottomColor, Vector4 borderWidths, Vector4 borderRadiuses)
	{
		DrawTexture(position, image, scaleMode, alphaBlend, imageAspect, leftColor, topColor, rightColor, bottomColor, borderWidths, borderRadiuses, drawSmoothCorners: true);
	}

	internal static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect, Color leftColor, Color topColor, Color rightColor, Color bottomColor, Vector4 borderWidths, Vector4 borderRadiuses, bool drawSmoothCorners)
	{
		GUIUtility.CheckOnGUI();
		if (Event.current.type != EventType.Repaint)
		{
			return;
		}
		if (image == null)
		{
			Debug.LogWarning("null texture passed to GUI.DrawTexture");
			return;
		}
		if (imageAspect == 0f)
		{
			imageAspect = (float)image.width / (float)image.height;
		}
		Material material = null;
		material = ((borderWidths != Vector4.zero) ? ((!(leftColor != topColor) && !(leftColor != rightColor) && !(leftColor != bottomColor)) ? roundedRectMaterial : roundedRectWithColorPerBorderMaterial) : ((!(borderRadiuses != Vector4.zero)) ? (alphaBlend ? blendMaterial : blitMaterial) : roundedRectMaterial));
		Internal_DrawTextureArguments args = new Internal_DrawTextureArguments
		{
			leftBorder = 0,
			rightBorder = 0,
			topBorder = 0,
			bottomBorder = 0,
			color = leftColor,
			leftBorderColor = leftColor,
			topBorderColor = topColor,
			rightBorderColor = rightColor,
			bottomBorderColor = bottomColor,
			borderWidths = borderWidths,
			cornerRadiuses = borderRadiuses,
			texture = image,
			smoothCorners = drawSmoothCorners,
			mat = material
		};
		CalculateScaledTextureRects(position, scaleMode, imageAspect, ref args.screenRect, ref args.sourceRect);
		Graphics.Internal_DrawTexture(ref args);
	}

	internal static bool CalculateScaledTextureRects(Rect position, ScaleMode scaleMode, float imageAspect, ref Rect outScreenRect, ref Rect outSourceRect)
	{
		float num = position.width / position.height;
		bool result = false;
		switch (scaleMode)
		{
		case ScaleMode.StretchToFill:
			outScreenRect = position;
			outSourceRect = new Rect(0f, 0f, 1f, 1f);
			result = true;
			break;
		case ScaleMode.ScaleAndCrop:
			if (num > imageAspect)
			{
				float num4 = imageAspect / num;
				outScreenRect = position;
				outSourceRect = new Rect(0f, (1f - num4) * 0.5f, 1f, num4);
				result = true;
			}
			else
			{
				float num5 = num / imageAspect;
				outScreenRect = position;
				outSourceRect = new Rect(0.5f - num5 * 0.5f, 0f, num5, 1f);
				result = true;
			}
			break;
		case ScaleMode.ScaleToFit:
			if (num > imageAspect)
			{
				float num2 = imageAspect / num;
				outScreenRect = new Rect(position.xMin + position.width * (1f - num2) * 0.5f, position.yMin, num2 * position.width, position.height);
				outSourceRect = new Rect(0f, 0f, 1f, 1f);
				result = true;
			}
			else
			{
				float num3 = num / imageAspect;
				outScreenRect = new Rect(position.xMin, position.yMin + position.height * (1f - num3) * 0.5f, position.width, num3 * position.height);
				outSourceRect = new Rect(0f, 0f, 1f, 1f);
				result = true;
			}
			break;
		}
		return result;
	}

	public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords)
	{
		DrawTextureWithTexCoords(position, image, texCoords, alphaBlend: true);
	}

	public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords, bool alphaBlend)
	{
		GUIUtility.CheckOnGUI();
		if (Event.current.type == EventType.Repaint)
		{
			Material mat = (alphaBlend ? blendMaterial : blitMaterial);
			Internal_DrawTextureArguments args = new Internal_DrawTextureArguments
			{
				texture = image,
				mat = mat,
				leftBorder = 0,
				rightBorder = 0,
				topBorder = 0,
				bottomBorder = 0,
				color = color,
				leftBorderColor = color,
				topBorderColor = color,
				rightBorderColor = color,
				bottomBorderColor = color,
				screenRect = position,
				sourceRect = texCoords
			};
			Graphics.Internal_DrawTexture(ref args);
		}
	}

	public static void Box(Rect position, string text)
	{
		Box(position, GUIContent.Temp(text), s_Skin.box);
	}

	public static void Box(Rect position, Texture image)
	{
		Box(position, GUIContent.Temp(image), s_Skin.box);
	}

	public static void Box(Rect position, GUIContent content)
	{
		Box(position, content, s_Skin.box);
	}

	public static void Box(Rect position, string text, GUIStyle style)
	{
		Box(position, GUIContent.Temp(text), style);
	}

	public static void Box(Rect position, Texture image, GUIStyle style)
	{
		Box(position, GUIContent.Temp(image), style);
	}

	public static void Box(Rect position, GUIContent content, GUIStyle style)
	{
		GUIUtility.CheckOnGUI();
		int controlID = GUIUtility.GetControlID(s_BoxHash, FocusType.Passive);
		if (Event.current.type == EventType.Repaint)
		{
			style.Draw(position, content, controlID, on: false, position.Contains(Event.current.mousePosition));
		}
	}

	public static bool Button(Rect position, string text)
	{
		return Button(position, GUIContent.Temp(text), s_Skin.button);
	}

	public static bool Button(Rect position, Texture image)
	{
		return Button(position, GUIContent.Temp(image), s_Skin.button);
	}

	public static bool Button(Rect position, GUIContent content)
	{
		return Button(position, content, s_Skin.button);
	}

	public static bool Button(Rect position, string text, GUIStyle style)
	{
		return Button(position, GUIContent.Temp(text), style);
	}

	public static bool Button(Rect position, Texture image, GUIStyle style)
	{
		return Button(position, GUIContent.Temp(image), style);
	}

	public static bool Button(Rect position, GUIContent content, GUIStyle style)
	{
		int controlID = GUIUtility.GetControlID(s_ButonHash, FocusType.Passive, position);
		return Button(position, controlID, content, style);
	}

	internal static bool Button(Rect position, int id, GUIContent content, GUIStyle style)
	{
		GUIUtility.CheckOnGUI();
		return DoButton(position, id, content, style);
	}

	public static bool RepeatButton(Rect position, string text)
	{
		return DoRepeatButton(position, GUIContent.Temp(text), s_Skin.button, FocusType.Passive);
	}

	public static bool RepeatButton(Rect position, Texture image)
	{
		return DoRepeatButton(position, GUIContent.Temp(image), s_Skin.button, FocusType.Passive);
	}

	public static bool RepeatButton(Rect position, GUIContent content)
	{
		return DoRepeatButton(position, content, s_Skin.button, FocusType.Passive);
	}

	public static bool RepeatButton(Rect position, string text, GUIStyle style)
	{
		return DoRepeatButton(position, GUIContent.Temp(text), style, FocusType.Passive);
	}

	public static bool RepeatButton(Rect position, Texture image, GUIStyle style)
	{
		return DoRepeatButton(position, GUIContent.Temp(image), style, FocusType.Passive);
	}

	public static bool RepeatButton(Rect position, GUIContent content, GUIStyle style)
	{
		return DoRepeatButton(position, content, style, FocusType.Passive);
	}

	private static bool DoRepeatButton(Rect position, GUIContent content, GUIStyle style, FocusType focusType)
	{
		GUIUtility.CheckOnGUI();
		int controlID = GUIUtility.GetControlID(s_RepeatButtonHash, focusType, position);
		switch (Event.current.GetTypeForControl(controlID))
		{
		case EventType.MouseDown:
			if (position.Contains(Event.current.mousePosition))
			{
				GUIUtility.hotControl = controlID;
				Event.current.Use();
			}
			return false;
		case EventType.MouseUp:
			if (GUIUtility.hotControl == controlID)
			{
				GUIUtility.hotControl = 0;
				Event.current.Use();
				return position.Contains(Event.current.mousePosition);
			}
			return false;
		case EventType.Repaint:
			style.Draw(position, content, controlID, on: false, position.Contains(Event.current.mousePosition));
			return controlID == GUIUtility.hotControl && position.Contains(Event.current.mousePosition);
		default:
			return false;
		}
	}

	public static string TextField(Rect position, string text)
	{
		GUIContent gUIContent = GUIContent.Temp(text);
		DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, multiline: false, -1, skin.textField);
		return gUIContent.text;
	}

	public static string TextField(Rect position, string text, int maxLength)
	{
		GUIContent gUIContent = GUIContent.Temp(text);
		DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, multiline: false, maxLength, skin.textField);
		return gUIContent.text;
	}

	public static string TextField(Rect position, string text, GUIStyle style)
	{
		GUIContent gUIContent = GUIContent.Temp(text);
		DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, multiline: false, -1, style);
		return gUIContent.text;
	}

	public static string TextField(Rect position, string text, int maxLength, GUIStyle style)
	{
		GUIContent gUIContent = GUIContent.Temp(text);
		DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, multiline: false, maxLength, style);
		return gUIContent.text;
	}

	public static string PasswordField(Rect position, string password, char maskChar)
	{
		return PasswordField(position, password, maskChar, -1, skin.textField);
	}

	public static string PasswordField(Rect position, string password, char maskChar, int maxLength)
	{
		return PasswordField(position, password, maskChar, maxLength, skin.textField);
	}

	public static string PasswordField(Rect position, string password, char maskChar, GUIStyle style)
	{
		return PasswordField(position, password, maskChar, -1, style);
	}

	public static string PasswordField(Rect position, string password, char maskChar, int maxLength, GUIStyle style)
	{
		GUIUtility.CheckOnGUI();
		string t = PasswordFieldGetStrToShow(password, maskChar);
		GUIContent gUIContent = GUIContent.Temp(t);
		bool flag = changed;
		changed = false;
		if (TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed)
		{
			DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard), gUIContent, multiline: false, maxLength, style, password, maskChar);
		}
		else
		{
			DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, multiline: false, maxLength, style);
		}
		t = (changed ? gUIContent.text : password);
		changed |= flag;
		return t;
	}

	internal static string PasswordFieldGetStrToShow(string password, char maskChar)
	{
		return (Event.current.type == EventType.Repaint || Event.current.type == EventType.MouseDown) ? "".PadRight(password.Length, maskChar) : password;
	}

	public static string TextArea(Rect position, string text)
	{
		GUIContent gUIContent = GUIContent.Temp(text);
		DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, multiline: true, -1, skin.textArea);
		return gUIContent.text;
	}

	public static string TextArea(Rect position, string text, int maxLength)
	{
		GUIContent gUIContent = GUIContent.Temp(text);
		DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, multiline: true, maxLength, skin.textArea);
		return gUIContent.text;
	}

	public static string TextArea(Rect position, string text, GUIStyle style)
	{
		GUIContent gUIContent = GUIContent.Temp(text);
		DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, multiline: true, -1, style);
		return gUIContent.text;
	}

	public static string TextArea(Rect position, string text, int maxLength, GUIStyle style)
	{
		GUIContent gUIContent = GUIContent.Temp(text);
		DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), gUIContent, multiline: true, maxLength, style);
		return gUIContent.text;
	}

	internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style)
	{
		DoTextField(position, id, content, multiline, maxLength, style, null);
	}

	internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText)
	{
		DoTextField(position, id, content, multiline, maxLength, style, secureText, '\0');
	}

	internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText, char maskChar)
	{
		GUIUtility.CheckOnGUI();
		if (maxLength >= 0 && content.text.Length > maxLength)
		{
			content.text = content.text.Substring(0, maxLength);
		}
		TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), id);
		textEditor.text = content.text;
		textEditor.SaveBackup();
		textEditor.position = position;
		textEditor.style = style;
		textEditor.multiline = multiline;
		textEditor.controlID = id;
		textEditor.DetectFocusChange();
		if (TouchScreenKeyboard.isRequiredToForceOpen)
		{
			HandleTextFieldEventForDesktopWithForcedKeyboard(position, id, content, multiline, maxLength, style, secureText, textEditor);
		}
		else if (TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed)
		{
			HandleTextFieldEventForTouchscreen(position, id, content, multiline, maxLength, style, secureText, maskChar, textEditor);
		}
		else
		{
			HandleTextFieldEventForDesktop(position, id, content, multiline, maxLength, style, textEditor);
		}
		textEditor.UpdateScrollOffsetIfNeeded(Event.current);
	}

	private static void HandleTextFieldEventForTouchscreen(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText, char maskChar, TextEditor editor)
	{
		Event current = Event.current;
		switch (current.type)
		{
		case EventType.MouseDown:
			if (position.Contains(current.mousePosition))
			{
				GUIUtility.hotControl = id;
				if (s_HotTextField != -1 && s_HotTextField != id)
				{
					TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), s_HotTextField);
					textEditor.keyboardOnScreen = null;
				}
				s_HotTextField = id;
				if (GUIUtility.keyboardControl != id)
				{
					GUIUtility.keyboardControl = id;
				}
				editor.keyboardOnScreen = TouchScreenKeyboard.Open(secureText ?? content.text, TouchScreenKeyboardType.Default, autocorrection: true, multiline, secureText != null);
				current.Use();
			}
			break;
		case EventType.Repaint:
		{
			if (editor.keyboardOnScreen != null)
			{
				content.text = editor.keyboardOnScreen.text;
				if (maxLength >= 0 && content.text.Length > maxLength)
				{
					content.text = content.text.Substring(0, maxLength);
				}
				if (editor.keyboardOnScreen.status != TouchScreenKeyboard.Status.Visible)
				{
					editor.keyboardOnScreen = null;
					changed = true;
				}
			}
			string text = content.text;
			if (secureText != null)
			{
				content.text = PasswordFieldGetStrToShow(text, maskChar);
			}
			style.Draw(position, content, id, on: false);
			content.text = text;
			break;
		}
		}
	}

	private static void HandleTextFieldEventForDesktop(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, TextEditor editor)
	{
		Event current = Event.current;
		bool flag = false;
		switch (current.type)
		{
		case EventType.MouseDown:
			if (position.Contains(current.mousePosition))
			{
				GUIUtility.hotControl = id;
				GUIUtility.keyboardControl = id;
				editor.m_HasFocus = true;
				editor.MoveCursorToPosition(Event.current.mousePosition);
				if (Event.current.clickCount == 2 && skin.settings.doubleClickSelectsWord)
				{
					editor.SelectCurrentWord();
					editor.DblClickSnap(TextEditor.DblClickSnapping.WORDS);
					editor.MouseDragSelectsWholeWords(on: true);
				}
				if (Event.current.clickCount == 3 && skin.settings.tripleClickSelectsLine)
				{
					editor.SelectCurrentParagraph();
					editor.MouseDragSelectsWholeWords(on: true);
					editor.DblClickSnap(TextEditor.DblClickSnapping.PARAGRAPHS);
				}
				current.Use();
			}
			break;
		case EventType.MouseDrag:
			if (GUIUtility.hotControl == id)
			{
				if (current.shift)
				{
					editor.MoveCursorToPosition(Event.current.mousePosition);
				}
				else
				{
					editor.SelectToPosition(Event.current.mousePosition);
				}
				current.Use();
			}
			break;
		case EventType.MouseUp:
			if (GUIUtility.hotControl == id)
			{
				editor.MouseDragSelectsWholeWords(on: false);
				GUIUtility.hotControl = 0;
				current.Use();
			}
			break;
		case EventType.KeyDown:
		{
			if (GUIUtility.keyboardControl != id)
			{
				return;
			}
			if (editor.HandleKeyEvent(current))
			{
				current.Use();
				flag = true;
				content.text = editor.text;
				break;
			}
			if (current.keyCode == KeyCode.Tab || current.character == '\t')
			{
				return;
			}
			char character = current.character;
			if (character == '\n' && !multiline && !current.alt)
			{
				return;
			}
			Font font = style.font;
			if (!font)
			{
				font = skin.font;
			}
			if (font.HasCharacter(character) || character == '\n')
			{
				editor.Insert(character);
				flag = true;
			}
			else if (character == '\0')
			{
				if (GUIUtility.compositionString.Length > 0)
				{
					editor.ReplaceSelection("");
					flag = true;
				}
				current.Use();
			}
			break;
		}
		case EventType.Repaint:
			if (GUIUtility.keyboardControl != id)
			{
				style.Draw(position, content, id, on: false);
			}
			else
			{
				editor.DrawCursor(content.text);
			}
			break;
		}
		if (GUIUtility.keyboardControl == id)
		{
			GUIUtility.textFieldInput = true;
		}
		if (flag)
		{
			changed = true;
			content.text = editor.text;
			if (maxLength >= 0 && content.text.Length > maxLength)
			{
				content.text = content.text.Substring(0, maxLength);
			}
			current.Use();
		}
	}

	private static void HandleTextFieldEventForDesktopWithForcedKeyboard(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText, TextEditor editor)
	{
		bool flag = false;
		if (Event.current.type == EventType.Repaint)
		{
			if (s_HotTextField != -1 && s_HotTextField != id)
			{
				TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), s_HotTextField);
				textEditor.keyboardOnScreen.active = false;
				textEditor.keyboardOnScreen = null;
			}
			if (editor.keyboardOnScreen != null)
			{
				if (GUIUtility.keyboardControl != id || !Application.isFocused)
				{
					editor.keyboardOnScreen.active = false;
					editor.keyboardOnScreen = null;
				}
				else if (!editor.keyboardOnScreen.active)
				{
					flag = true;
				}
			}
			else if (GUIUtility.keyboardControl == id && Application.isFocused)
			{
				flag = true;
			}
		}
		if (flag)
		{
			editor.keyboardOnScreen = TouchScreenKeyboard.Open(secureText ?? content.text, TouchScreenKeyboardType.Default, autocorrection: true, multiline, secureText != null);
		}
		HandleTextFieldEventForDesktop(position, id, content, multiline, maxLength, style, editor);
	}

	public static bool Toggle(Rect position, bool value, string text)
	{
		return Toggle(position, value, GUIContent.Temp(text), s_Skin.toggle);
	}

	public static bool Toggle(Rect position, bool value, Texture image)
	{
		return Toggle(position, value, GUIContent.Temp(image), s_Skin.toggle);
	}

	public static bool Toggle(Rect position, bool value, GUIContent content)
	{
		return Toggle(position, value, content, s_Skin.toggle);
	}

	public static bool Toggle(Rect position, bool value, string text, GUIStyle style)
	{
		return Toggle(position, value, GUIContent.Temp(text), style);
	}

	public static bool Toggle(Rect position, bool value, Texture image, GUIStyle style)
	{
		return Toggle(position, value, GUIContent.Temp(image), style);
	}

	public static bool Toggle(Rect position, bool value, GUIContent content, GUIStyle style)
	{
		GUIUtility.CheckOnGUI();
		return DoToggle(position, GUIUtility.GetControlID(s_ToggleHash, FocusType.Passive, position), value, content, style);
	}

	public static bool Toggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)
	{
		GUIUtility.CheckOnGUI();
		return DoToggle(position, id, value, content, style);
	}

	public static int Toolbar(Rect position, int selected, string[] texts)
	{
		return Toolbar(position, selected, GUIContent.Temp(texts), s_Skin.button);
	}

	public static int Toolbar(Rect position, int selected, Texture[] images)
	{
		return Toolbar(position, selected, GUIContent.Temp(images), s_Skin.button);
	}

	public static int Toolbar(Rect position, int selected, GUIContent[] contents)
	{
		return Toolbar(position, selected, contents, s_Skin.button);
	}

	public static int Toolbar(Rect position, int selected, string[] texts, GUIStyle style)
	{
		return Toolbar(position, selected, GUIContent.Temp(texts), style);
	}

	public static int Toolbar(Rect position, int selected, Texture[] images, GUIStyle style)
	{
		return Toolbar(position, selected, GUIContent.Temp(images), style);
	}

	public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style)
	{
		return Toolbar(position, selected, contents, null, style, ToolbarButtonSize.Fixed);
	}

	public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style, ToolbarButtonSize buttonSize)
	{
		return Toolbar(position, selected, contents, null, style, buttonSize);
	}

	internal static int Toolbar(Rect position, int selected, GUIContent[] contents, string[] controlNames, GUIStyle style, ToolbarButtonSize buttonSize, bool[] contentsEnabled = null)
	{
		GUIUtility.CheckOnGUI();
		FindStyles(ref style, out var firstStyle, out var midStyle, out var lastStyle, "left", "mid", "right");
		return DoButtonGrid(position, selected, contents, controlNames, contents.Length, style, firstStyle, midStyle, lastStyle, buttonSize, contentsEnabled);
	}

	public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount)
	{
		return SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, null);
	}

	public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount)
	{
		return SelectionGrid(position, selected, GUIContent.Temp(images), xCount, null);
	}

	public static int SelectionGrid(Rect position, int selected, GUIContent[] content, int xCount)
	{
		return SelectionGrid(position, selected, content, xCount, null);
	}

	public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount, GUIStyle style)
	{
		return SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, style);
	}

	public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount, GUIStyle style)
	{
		return SelectionGrid(position, selected, GUIContent.Temp(images), xCount, style);
	}

	public static int SelectionGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style)
	{
		if (style == null)
		{
			style = s_Skin.button;
		}
		return DoButtonGrid(position, selected, contents, null, xCount, style, style, style, style, ToolbarButtonSize.Fixed);
	}

	internal static void FindStyles(ref GUIStyle style, out GUIStyle firstStyle, out GUIStyle midStyle, out GUIStyle lastStyle, string first, string mid, string last)
	{
		if (style == null)
		{
			style = skin.button;
		}
		string name = style.name;
		midStyle = skin.FindStyle(name + mid) ?? style;
		firstStyle = skin.FindStyle(name + first) ?? midStyle;
		lastStyle = skin.FindStyle(name + last) ?? midStyle;
	}

	internal static int CalcTotalHorizSpacing(int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
	{
		if (xCount < 2)
		{
			return 0;
		}
		if (xCount == 2)
		{
			return Mathf.Max(firstStyle.margin.right, lastStyle.margin.left);
		}
		int num = Mathf.Max(midStyle.margin.left, midStyle.margin.right);
		return Mathf.Max(firstStyle.margin.right, midStyle.margin.left) + Mathf.Max(midStyle.margin.right, lastStyle.margin.left) + num * (xCount - 3);
	}

	internal static bool DoControl(Rect position, int id, bool on, bool hover, GUIContent content, GUIStyle style)
	{
		Event current = Event.current;
		switch (current.type)
		{
		case EventType.Repaint:
			style.Draw(position, content, id, on, hover);
			break;
		case EventType.MouseDown:
			if (GUIUtility.HitTest(position, current))
			{
				GrabMouseControl(id);
				current.Use();
			}
			break;
		case EventType.KeyDown:
		{
			bool flag = current.alt || current.shift || current.command || current.control;
			if ((current.keyCode == KeyCode.Space || current.keyCode == KeyCode.Return || current.keyCode == KeyCode.KeypadEnter) && !flag && GUIUtility.keyboardControl == id)
			{
				current.Use();
				changed = true;
				return !on;
			}
			break;
		}
		case EventType.MouseUp:
			if (HasMouseControl(id))
			{
				ReleaseMouseControl();
				current.Use();
				if (GUIUtility.HitTest(position, current))
				{
					changed = true;
					return !on;
				}
			}
			break;
		case EventType.MouseDrag:
			if (HasMouseControl(id))
			{
				current.Use();
			}
			break;
		}
		return on;
	}

	private static void DoLabel(Rect position, GUIContent content, GUIStyle style)
	{
		Event current = Event.current;
		if (current.type != EventType.Repaint)
		{
			return;
		}
		bool flag = position.Contains(current.mousePosition);
		style.Draw(position, content, flag, isActive: false, on: false, hasKeyboardFocus: false);
		if (!string.IsNullOrEmpty(content.tooltip) && flag && GUIClip.visibleRect.Contains(current.mousePosition))
		{
			if (!GUIStyle.IsTooltipActive(content.tooltip))
			{
				s_ToolTipRect = new Rect(current.mousePosition, Vector2.zero);
			}
			GUIStyle.SetMouseTooltip(content.tooltip, s_ToolTipRect);
		}
	}

	internal static bool DoToggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)
	{
		return DoControl(position, id, value, position.Contains(Event.current.mousePosition), content, style);
	}

	internal static bool DoButton(Rect position, int id, GUIContent content, GUIStyle style)
	{
		return DoControl(position, id, on: false, position.Contains(Event.current.mousePosition), content, style);
	}

	private static Rect[] CalcGridRectsFixedWidthFixedMargin(Rect position, int itemCount, int itemsPerRow, float elemWidth, float elemHeight, float spacingHorizontal, float spacingVertical)
	{
		int num = 0;
		float x = position.xMin;
		float num2 = position.yMin;
		Rect[] array = new Rect[itemCount];
		for (int i = 0; i < itemCount; i++)
		{
			array[i] = new Rect(x, num2, elemWidth, elemHeight);
			array[i] = GUIUtility.AlignRectToDevice(array[i]);
			x = array[i].xMax + spacingHorizontal;
			if (++num >= itemsPerRow)
			{
				num = 0;
				num2 += elemHeight + spacingVertical;
				x = position.xMin;
			}
		}
		return array;
	}

	internal static int DoCustomSelectionGrid(Rect position, int selected, int itemCount, CustomSelectionGridItemGUI itemGUI, int itemsPerRow, GUIStyle style)
	{
		GUIUtility.CheckOnGUI();
		if (itemCount == 0)
		{
			return selected;
		}
		if (itemsPerRow <= 0)
		{
			Debug.LogWarning("You are trying to create a SelectionGrid with zero or less elements to be displayed in the horizontal direction. Set itemsPerRow to a positive value.");
			return selected;
		}
		int num = (itemCount + itemsPerRow - 1) / itemsPerRow;
		float spacingHorizontal = Mathf.Max(style.margin.left, style.margin.right);
		float num2 = Mathf.Max(style.margin.top, style.margin.bottom);
		float elemWidth = ((style.fixedWidth != 0f) ? style.fixedWidth : ((position.width - (float)CalcTotalHorizSpacing(itemsPerRow, style, style, style, style)) / (float)itemsPerRow));
		float elemHeight = ((style.fixedHeight != 0f) ? style.fixedHeight : ((position.height - num2 * (float)(num - 1)) / (float)num));
		Rect[] array = CalcGridRectsFixedWidthFixedMargin(position, itemCount, itemsPerRow, elemWidth, elemHeight, spacingHorizontal, num2);
		int controlID = 0;
		for (int i = 0; i < itemCount; i++)
		{
			Rect rect = array[i];
			int controlID2 = GUIUtility.GetControlID(s_ButtonGridHash, FocusType.Passive, rect);
			if (i == selected)
			{
				controlID = controlID2;
			}
			EventType typeForControl = Event.current.GetTypeForControl(controlID2);
			switch (typeForControl)
			{
			case EventType.MouseDown:
				if (GUIUtility.HitTest(rect, Event.current))
				{
					GUIUtility.hotControl = controlID2;
					Event.current.Use();
				}
				break;
			case EventType.MouseDrag:
				if (GUIUtility.hotControl == controlID2)
				{
					Event.current.Use();
				}
				break;
			case EventType.MouseUp:
				if (GUIUtility.hotControl == controlID2)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
					changed = true;
					return i;
				}
				break;
			case EventType.Repaint:
				if (selected != i)
				{
					itemGUI(i, rect, style, controlID2);
				}
				break;
			}
			if (typeForControl != EventType.Repaint || selected != i)
			{
				itemGUI(i, rect, style, controlID2);
			}
		}
		if (selected >= 0 && selected < itemCount && Event.current.type == EventType.Repaint)
		{
			itemGUI(selected, array[selected], style, controlID);
		}
		return selected;
	}

	private static int DoButtonGrid(Rect position, int selected, GUIContent[] contents, string[] controlNames, int itemsPerRow, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, ToolbarButtonSize buttonSize, bool[] contentsEnabled = null)
	{
		GUIUtility.CheckOnGUI();
		int num = contents.Length;
		if (num == 0)
		{
			return selected;
		}
		if (itemsPerRow <= 0)
		{
			Debug.LogWarning("You are trying to create a SelectionGrid with zero or less elements to be displayed in the horizontal direction. Set itemsPerRow to a positive value.");
			return selected;
		}
		if (contentsEnabled != null && contentsEnabled.Length != num)
		{
			throw new ArgumentException("contentsEnabled");
		}
		int num2 = (num + itemsPerRow - 1) / itemsPerRow;
		float elemWidth = ((style.fixedWidth != 0f) ? style.fixedWidth : ((position.width - (float)CalcTotalHorizSpacing(itemsPerRow, style, firstStyle, midStyle, lastStyle)) / (float)itemsPerRow));
		float elemHeight = ((style.fixedHeight != 0f) ? style.fixedHeight : ((position.height - (float)(Mathf.Max(style.margin.top, style.margin.bottom) * (num2 - 1))) / (float)num2));
		Rect[] array = CalcGridRects(position, contents, itemsPerRow, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, buttonSize);
		GUIStyle gUIStyle = null;
		int num3 = 0;
		for (int i = 0; i < num; i++)
		{
			bool flag = enabled;
			enabled &= contentsEnabled == null || contentsEnabled[i];
			Rect rect = array[i];
			GUIContent gUIContent = contents[i];
			if (controlNames != null)
			{
				SetNextControlName(controlNames[i]);
			}
			int controlID = GUIUtility.GetControlID(s_ButtonGridHash, FocusType.Passive, rect);
			if (i == selected)
			{
				num3 = controlID;
			}
			switch (Event.current.GetTypeForControl(controlID))
			{
			case EventType.MouseDown:
				if (GUIUtility.HitTest(rect, Event.current))
				{
					GUIUtility.hotControl = controlID;
					Event.current.Use();
				}
				break;
			case EventType.MouseDrag:
				if (GUIUtility.hotControl == controlID)
				{
					Event.current.Use();
				}
				break;
			case EventType.MouseUp:
				if (GUIUtility.hotControl == controlID)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
					changed = true;
					return i;
				}
				break;
			case EventType.Repaint:
			{
				GUIStyle gUIStyle2 = ((num == 1) ? style : ((i == 0) ? firstStyle : ((i == num - 1) ? lastStyle : midStyle)));
				bool flag2 = rect.Contains(Event.current.mousePosition);
				bool flag3 = GUIUtility.hotControl == controlID;
				if (selected != i)
				{
					gUIStyle2.Draw(rect, gUIContent, enabled && flag2 && (flag3 || GUIUtility.hotControl == 0), enabled && flag3, on: false, hasKeyboardFocus: false);
				}
				else
				{
					gUIStyle = gUIStyle2;
				}
				if (flag2)
				{
					GUIUtility.mouseUsed = true;
					if (!string.IsNullOrEmpty(gUIContent.tooltip))
					{
						GUIStyle.SetMouseTooltip(gUIContent.tooltip, rect);
					}
				}
				break;
			}
			}
			enabled = flag;
		}
		if (gUIStyle != null)
		{
			Rect position2 = array[selected];
			GUIContent content = contents[selected];
			bool flag4 = position2.Contains(Event.current.mousePosition);
			bool flag5 = GUIUtility.hotControl == num3;
			bool flag6 = enabled;
			enabled &= contentsEnabled == null || contentsEnabled[selected];
			gUIStyle.Draw(position2, content, enabled && flag4 && (flag5 || GUIUtility.hotControl == 0), enabled && flag5, on: true, hasKeyboardFocus: false);
			enabled = flag6;
		}
		return selected;
	}

	private static Rect[] CalcGridRects(Rect position, GUIContent[] contents, int xCount, float elemWidth, float elemHeight, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, ToolbarButtonSize buttonSize)
	{
		int num = contents.Length;
		int num2 = 0;
		float x = position.xMin;
		float num3 = position.yMin;
		GUIStyle gUIStyle = style;
		Rect[] array = new Rect[num];
		if (num > 1)
		{
			gUIStyle = firstStyle;
		}
		for (int i = 0; i < num; i++)
		{
			float width = 0f;
			switch (buttonSize)
			{
			case ToolbarButtonSize.Fixed:
				width = elemWidth;
				break;
			case ToolbarButtonSize.FitToContents:
				width = gUIStyle.CalcSize(contents[i]).x;
				break;
			}
			array[i] = new Rect(x, num3, width, elemHeight);
			array[i] = GUIUtility.AlignRectToDevice(array[i]);
			GUIStyle gUIStyle2 = midStyle;
			if (i == num - 2 || i == xCount - 2)
			{
				gUIStyle2 = lastStyle;
			}
			x = array[i].xMax + (float)Mathf.Max(gUIStyle.margin.right, gUIStyle2.margin.left);
			num2++;
			if (num2 >= xCount)
			{
				num2 = 0;
				num3 += elemHeight + (float)Mathf.Max(style.margin.top, style.margin.bottom);
				x = position.xMin;
				gUIStyle2 = firstStyle;
			}
			gUIStyle = gUIStyle2;
		}
		return array;
	}

	public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue)
	{
		return Slider(position, value, 0f, leftValue, rightValue, skin.horizontalSlider, skin.horizontalSliderThumb, horiz: true, 0, skin.horizontalSliderThumbExtent);
	}

	public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb)
	{
		return Slider(position, value, 0f, leftValue, rightValue, slider, thumb, horiz: true, 0);
	}

	public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUIStyle thumbExtent)
	{
		return Slider(position, value, 0f, leftValue, rightValue, slider, thumb, horiz: true, 0, (thumbExtent == null && thumb == skin.horizontalSliderThumb) ? skin.horizontalSliderThumbExtent : thumbExtent);
	}

	public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue)
	{
		return Slider(position, value, 0f, topValue, bottomValue, skin.verticalSlider, skin.verticalSliderThumb, horiz: false, 0, skin.verticalSliderThumbExtent);
	}

	public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb)
	{
		return Slider(position, value, 0f, topValue, bottomValue, slider, thumb, horiz: false, 0);
	}

	public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb, GUIStyle thumbExtent)
	{
		return Slider(position, value, 0f, topValue, bottomValue, slider, thumb, horiz: false, 0, (thumbExtent == null && thumb == skin.verticalSliderThumb) ? skin.verticalSliderThumbExtent : thumbExtent);
	}

	public static float Slider(Rect position, float value, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id, GUIStyle thumbExtent = null)
	{
		GUIUtility.CheckOnGUI();
		if (id == 0)
		{
			id = GUIUtility.GetControlID(s_SliderHash, FocusType.Passive, position);
		}
		return new SliderHandler(position, value, size, start, end, slider, thumb, horiz, id, thumbExtent).Handle();
	}

	public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue)
	{
		return Scroller(position, value, size, leftValue, rightValue, skin.horizontalScrollbar, skin.horizontalScrollbarThumb, skin.horizontalScrollbarLeftButton, skin.horizontalScrollbarRightButton, horiz: true);
	}

	public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle style)
	{
		return Scroller(position, value, size, leftValue, rightValue, style, skin.GetStyle(style.name + "thumb"), skin.GetStyle(style.name + "leftbutton"), skin.GetStyle(style.name + "rightbutton"), horiz: true);
	}

	internal static bool ScrollerRepeatButton(int scrollerID, Rect rect, GUIStyle style)
	{
		bool result = false;
		if (DoRepeatButton(rect, GUIContent.none, style, FocusType.Passive))
		{
			bool flag = s_ScrollControlId != scrollerID;
			s_ScrollControlId = scrollerID;
			if (flag)
			{
				result = true;
				nextScrollStepTime = DateTime.Now.AddMilliseconds(250.0);
			}
			else if (DateTime.Now >= nextScrollStepTime)
			{
				result = true;
				nextScrollStepTime = DateTime.Now.AddMilliseconds(30.0);
			}
			if (Event.current.type == EventType.Repaint)
			{
				InternalRepaintEditorWindow();
			}
		}
		return result;
	}

	public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue)
	{
		return Scroller(position, value, size, topValue, bottomValue, skin.verticalScrollbar, skin.verticalScrollbarThumb, skin.verticalScrollbarUpButton, skin.verticalScrollbarDownButton, horiz: false);
	}

	public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue, GUIStyle style)
	{
		return Scroller(position, value, size, topValue, bottomValue, style, skin.GetStyle(style.name + "thumb"), skin.GetStyle(style.name + "upbutton"), skin.GetStyle(style.name + "downbutton"), horiz: false);
	}

	internal static float Scroller(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUIStyle leftButton, GUIStyle rightButton, bool horiz)
	{
		GUIUtility.CheckOnGUI();
		int controlID = GUIUtility.GetControlID(s_SliderHash, FocusType.Passive, position);
		Rect position2;
		Rect rect;
		Rect rect2;
		if (horiz)
		{
			position2 = new Rect(position.x + leftButton.fixedWidth, position.y, position.width - leftButton.fixedWidth - rightButton.fixedWidth, position.height);
			rect = new Rect(position.x, position.y, leftButton.fixedWidth, position.height);
			rect2 = new Rect(position.xMax - rightButton.fixedWidth, position.y, rightButton.fixedWidth, position.height);
		}
		else
		{
			position2 = new Rect(position.x, position.y + leftButton.fixedHeight, position.width, position.height - leftButton.fixedHeight - rightButton.fixedHeight);
			rect = new Rect(position.x, position.y, position.width, leftButton.fixedHeight);
			rect2 = new Rect(position.x, position.yMax - rightButton.fixedHeight, position.width, rightButton.fixedHeight);
		}
		value = Slider(position2, value, size, leftValue, rightValue, slider, thumb, horiz, controlID);
		bool flag = Event.current.type == EventType.MouseUp;
		if (ScrollerRepeatButton(controlID, rect, leftButton))
		{
			value -= 10f * ((leftValue < rightValue) ? 1f : (-1f));
		}
		if (ScrollerRepeatButton(controlID, rect2, rightButton))
		{
			value += 10f * ((leftValue < rightValue) ? 1f : (-1f));
		}
		if (flag && Event.current.type == EventType.Used)
		{
			s_ScrollControlId = 0;
		}
		value = ((!(leftValue < rightValue)) ? Mathf.Clamp(value, rightValue, leftValue - size) : Mathf.Clamp(value, leftValue, rightValue - size));
		return value;
	}

	public static void BeginClip(Rect position, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
	{
		GUIUtility.CheckOnGUI();
		GUIClip.Push(position, scrollOffset, renderOffset, resetOffset);
	}

	public static void BeginGroup(Rect position)
	{
		BeginGroup(position, GUIContent.none, GUIStyle.none);
	}

	public static void BeginGroup(Rect position, string text)
	{
		BeginGroup(position, GUIContent.Temp(text), GUIStyle.none);
	}

	public static void BeginGroup(Rect position, Texture image)
	{
		BeginGroup(position, GUIContent.Temp(image), GUIStyle.none);
	}

	public static void BeginGroup(Rect position, GUIContent content)
	{
		BeginGroup(position, content, GUIStyle.none);
	}

	public static void BeginGroup(Rect position, GUIStyle style)
	{
		BeginGroup(position, GUIContent.none, style);
	}

	public static void BeginGroup(Rect position, string text, GUIStyle style)
	{
		BeginGroup(position, GUIContent.Temp(text), style);
	}

	public static void BeginGroup(Rect position, Texture image, GUIStyle style)
	{
		BeginGroup(position, GUIContent.Temp(image), style);
	}

	public static void BeginGroup(Rect position, GUIContent content, GUIStyle style)
	{
		BeginGroup(position, content, style, Vector2.zero);
	}

	internal static void BeginGroup(Rect position, GUIContent content, GUIStyle style, Vector2 scrollOffset)
	{
		GUIUtility.CheckOnGUI();
		int controlID = GUIUtility.GetControlID(s_BeginGroupHash, FocusType.Passive);
		if (content != GUIContent.none || style != GUIStyle.none)
		{
			EventType type = Event.current.type;
			EventType eventType = type;
			if (eventType == EventType.Repaint)
			{
				style.Draw(position, content, controlID);
			}
			else if (position.Contains(Event.current.mousePosition))
			{
				GUIUtility.mouseUsed = true;
			}
		}
		GUIClip.Push(position, scrollOffset, Vector2.zero, resetOffset: false);
	}

	public static void EndGroup()
	{
		GUIUtility.CheckOnGUI();
		GUIClip.Internal_Pop();
	}

	public static void BeginClip(Rect position)
	{
		GUIUtility.CheckOnGUI();
		GUIClip.Push(position, Vector2.zero, Vector2.zero, resetOffset: false);
	}

	public static void EndClip()
	{
		GUIUtility.CheckOnGUI();
		GUIClip.Pop();
	}

	public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect)
	{
		return BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal: false, alwaysShowVertical: false, skin.horizontalScrollbar, skin.verticalScrollbar, skin.scrollView);
	}

	public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
	{
		return BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, skin.horizontalScrollbar, skin.verticalScrollbar, skin.scrollView);
	}

	public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
	{
		return BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal: false, alwaysShowVertical: false, horizontalScrollbar, verticalScrollbar, skin.scrollView);
	}

	public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
	{
		return BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, skin.scrollView);
	}

	protected static Vector2 DoBeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
	{
		return BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
	}

	internal static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
	{
		GUIUtility.CheckOnGUI();
		int controlID = GUIUtility.GetControlID(s_ScrollviewHash, FocusType.Passive);
		ScrollViewState scrollViewState = (ScrollViewState)GUIUtility.GetStateObject(typeof(ScrollViewState), controlID);
		if (scrollViewState.apply)
		{
			scrollPosition = scrollViewState.scrollPosition;
			scrollViewState.apply = false;
		}
		scrollViewState.position = position;
		scrollViewState.scrollPosition = scrollPosition;
		scrollViewState.visibleRect = (scrollViewState.viewRect = viewRect);
		scrollViewState.visibleRect.width = position.width;
		scrollViewState.visibleRect.height = position.height;
		scrollViewStates.Push(scrollViewState);
		Rect screenRect = new Rect(position);
		switch (Event.current.type)
		{
		case EventType.Layout:
			GUIUtility.GetControlID(s_SliderHash, FocusType.Passive);
			GUIUtility.GetControlID(s_RepeatButtonHash, FocusType.Passive);
			GUIUtility.GetControlID(s_RepeatButtonHash, FocusType.Passive);
			GUIUtility.GetControlID(s_SliderHash, FocusType.Passive);
			GUIUtility.GetControlID(s_RepeatButtonHash, FocusType.Passive);
			GUIUtility.GetControlID(s_RepeatButtonHash, FocusType.Passive);
			break;
		default:
		{
			bool flag = alwaysShowVertical;
			bool flag2 = alwaysShowHorizontal;
			if (flag2 || viewRect.width > screenRect.width)
			{
				scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
				screenRect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
				flag2 = true;
			}
			if (flag || viewRect.height > screenRect.height)
			{
				scrollViewState.visibleRect.width = position.width - verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
				screenRect.width -= verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
				flag = true;
				if (!flag2 && viewRect.width > screenRect.width)
				{
					scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
					screenRect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
					flag2 = true;
				}
			}
			if (Event.current.type == EventType.Repaint && background != GUIStyle.none)
			{
				background.Draw(position, position.Contains(Event.current.mousePosition), isActive: false, flag2 && flag, hasKeyboardFocus: false);
			}
			if (flag2 && horizontalScrollbar != GUIStyle.none)
			{
				scrollPosition.x = HorizontalScrollbar(new Rect(position.x, position.yMax - horizontalScrollbar.fixedHeight, screenRect.width, horizontalScrollbar.fixedHeight), scrollPosition.x, Mathf.Min(screenRect.width, viewRect.width), 0f, viewRect.width, horizontalScrollbar);
			}
			else
			{
				GUIUtility.GetControlID(s_SliderHash, FocusType.Passive);
				GUIUtility.GetControlID(s_RepeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(s_RepeatButtonHash, FocusType.Passive);
				scrollPosition.x = ((horizontalScrollbar != GUIStyle.none) ? 0f : Mathf.Clamp(scrollPosition.x, 0f, Mathf.Max(viewRect.width - position.width, 0f)));
			}
			if (flag && verticalScrollbar != GUIStyle.none)
			{
				scrollPosition.y = VerticalScrollbar(new Rect(screenRect.xMax + (float)verticalScrollbar.margin.left, screenRect.y, verticalScrollbar.fixedWidth, screenRect.height), scrollPosition.y, Mathf.Min(screenRect.height, viewRect.height), 0f, viewRect.height, verticalScrollbar);
				break;
			}
			GUIUtility.GetControlID(s_SliderHash, FocusType.Passive);
			GUIUtility.GetControlID(s_RepeatButtonHash, FocusType.Passive);
			GUIUtility.GetControlID(s_RepeatButtonHash, FocusType.Passive);
			scrollPosition.y = ((verticalScrollbar != GUIStyle.none) ? 0f : Mathf.Clamp(scrollPosition.y, 0f, Mathf.Max(viewRect.height - position.height, 0f)));
			break;
		}
		case EventType.Used:
			break;
		}
		GUIClip.Push(screenRect, new Vector2(Mathf.Round(0f - scrollPosition.x - viewRect.x), Mathf.Round(0f - scrollPosition.y - viewRect.y)), Vector2.zero, resetOffset: false);
		return scrollPosition;
	}

	public static void EndScrollView()
	{
		EndScrollView(handleScrollWheel: true);
	}

	public static void EndScrollView(bool handleScrollWheel)
	{
		GUIUtility.CheckOnGUI();
		if (scrollViewStates.Count == 0)
		{
			return;
		}
		ScrollViewState scrollViewState = (ScrollViewState)scrollViewStates.Peek();
		GUIClip.Pop();
		scrollViewStates.Pop();
		bool flag = false;
		float num = Time.realtimeSinceStartup - scrollViewState.previousTimeSinceStartup;
		scrollViewState.previousTimeSinceStartup = Time.realtimeSinceStartup;
		if (Event.current.type == EventType.Repaint && scrollViewState.velocity != Vector2.zero)
		{
			for (int i = 0; i < 2; i++)
			{
				scrollViewState.velocity[i] *= Mathf.Pow(0.1f, num);
				float num2 = 0.1f / num;
				if (Mathf.Abs(scrollViewState.velocity[i]) < num2)
				{
					scrollViewState.velocity[i] = 0f;
					continue;
				}
				scrollViewState.velocity[i] += ((scrollViewState.velocity[i] < 0f) ? num2 : (0f - num2));
				scrollViewState.scrollPosition[i] += scrollViewState.velocity[i] * num;
				flag = true;
				scrollViewState.touchScrollStartMousePosition = Event.current.mousePosition;
				scrollViewState.touchScrollStartPosition = scrollViewState.scrollPosition;
			}
			if (scrollViewState.velocity != Vector2.zero)
			{
				InternalRepaintEditorWindow();
			}
		}
		if (handleScrollWheel && (Event.current.type == EventType.ScrollWheel || Event.current.type == EventType.TouchDown || Event.current.type == EventType.TouchUp || Event.current.type == EventType.TouchMove))
		{
			if (Event.current.type == EventType.ScrollWheel && scrollViewState.position.Contains(Event.current.mousePosition))
			{
				scrollViewState.scrollPosition.x = Mathf.Clamp(scrollViewState.scrollPosition.x + Event.current.delta.x * 20f, 0f, scrollViewState.viewRect.width - scrollViewState.visibleRect.width);
				scrollViewState.scrollPosition.y = Mathf.Clamp(scrollViewState.scrollPosition.y + Event.current.delta.y * 20f, 0f, scrollViewState.viewRect.height - scrollViewState.visibleRect.height);
				Event.current.Use();
				flag = true;
			}
			else if (Event.current.type == EventType.TouchDown && (Event.current.modifiers & EventModifiers.Alt) == EventModifiers.Alt && scrollViewState.position.Contains(Event.current.mousePosition))
			{
				scrollViewState.isDuringTouchScroll = true;
				scrollViewState.touchScrollStartMousePosition = Event.current.mousePosition;
				scrollViewState.touchScrollStartPosition = scrollViewState.scrollPosition;
				GUIUtility.hotControl = GUIUtility.GetControlID(s_ScrollviewHash, FocusType.Passive, scrollViewState.position);
				Event.current.Use();
			}
			else if (scrollViewState.isDuringTouchScroll && Event.current.type == EventType.TouchUp)
			{
				scrollViewState.isDuringTouchScroll = false;
			}
			else if (scrollViewState.isDuringTouchScroll && Event.current.type == EventType.TouchMove)
			{
				Vector2 scrollPosition = scrollViewState.scrollPosition;
				scrollViewState.scrollPosition.x = Mathf.Clamp(scrollViewState.touchScrollStartPosition.x - (Event.current.mousePosition.x - scrollViewState.touchScrollStartMousePosition.x), 0f, scrollViewState.viewRect.width - scrollViewState.visibleRect.width);
				scrollViewState.scrollPosition.y = Mathf.Clamp(scrollViewState.touchScrollStartPosition.y - (Event.current.mousePosition.y - scrollViewState.touchScrollStartMousePosition.y), 0f, scrollViewState.viewRect.height - scrollViewState.visibleRect.height);
				Event.current.Use();
				Vector2 b = (scrollViewState.scrollPosition - scrollPosition) / num;
				scrollViewState.velocity = Vector2.Lerp(scrollViewState.velocity, b, num * 10f);
				flag = true;
			}
		}
		if (flag)
		{
			if (scrollViewState.scrollPosition.x < 0f)
			{
				scrollViewState.scrollPosition.x = 0f;
			}
			if (scrollViewState.scrollPosition.y < 0f)
			{
				scrollViewState.scrollPosition.y = 0f;
			}
			scrollViewState.apply = true;
		}
	}

	internal static ScrollViewState GetTopScrollView()
	{
		if (scrollViewStates.Count != 0)
		{
			return (ScrollViewState)scrollViewStates.Peek();
		}
		return null;
	}

	public static void ScrollTo(Rect position)
	{
		GetTopScrollView()?.ScrollTo(position);
	}

	public static bool ScrollTowards(Rect position, float maxDelta)
	{
		return GetTopScrollView()?.ScrollTowards(position, maxDelta) ?? false;
	}

	public static Rect Window(int id, Rect clientRect, WindowFunction func, string text)
	{
		GUIUtility.CheckOnGUI();
		return DoWindow(id, clientRect, func, GUIContent.Temp(text), skin.window, skin, forceRectOnLayout: true);
	}

	public static Rect Window(int id, Rect clientRect, WindowFunction func, Texture image)
	{
		GUIUtility.CheckOnGUI();
		return DoWindow(id, clientRect, func, GUIContent.Temp(image), skin.window, skin, forceRectOnLayout: true);
	}

	public static Rect Window(int id, Rect clientRect, WindowFunction func, GUIContent content)
	{
		GUIUtility.CheckOnGUI();
		return DoWindow(id, clientRect, func, content, skin.window, skin, forceRectOnLayout: true);
	}

	public static Rect Window(int id, Rect clientRect, WindowFunction func, string text, GUIStyle style)
	{
		GUIUtility.CheckOnGUI();
		return DoWindow(id, clientRect, func, GUIContent.Temp(text), style, skin, forceRectOnLayout: true);
	}

	public static Rect Window(int id, Rect clientRect, WindowFunction func, Texture image, GUIStyle style)
	{
		GUIUtility.CheckOnGUI();
		return DoWindow(id, clientRect, func, GUIContent.Temp(image), style, skin, forceRectOnLayout: true);
	}

	public static Rect Window(int id, Rect clientRect, WindowFunction func, GUIContent title, GUIStyle style)
	{
		GUIUtility.CheckOnGUI();
		return DoWindow(id, clientRect, func, title, style, skin, forceRectOnLayout: true);
	}

	public static Rect ModalWindow(int id, Rect clientRect, WindowFunction func, string text)
	{
		GUIUtility.CheckOnGUI();
		return DoModalWindow(id, clientRect, func, GUIContent.Temp(text), skin.window, skin);
	}

	public static Rect ModalWindow(int id, Rect clientRect, WindowFunction func, Texture image)
	{
		GUIUtility.CheckOnGUI();
		return DoModalWindow(id, clientRect, func, GUIContent.Temp(image), skin.window, skin);
	}

	public static Rect ModalWindow(int id, Rect clientRect, WindowFunction func, GUIContent content)
	{
		GUIUtility.CheckOnGUI();
		return DoModalWindow(id, clientRect, func, content, skin.window, skin);
	}

	public static Rect ModalWindow(int id, Rect clientRect, WindowFunction func, string text, GUIStyle style)
	{
		GUIUtility.CheckOnGUI();
		return DoModalWindow(id, clientRect, func, GUIContent.Temp(text), style, skin);
	}

	public static Rect ModalWindow(int id, Rect clientRect, WindowFunction func, Texture image, GUIStyle style)
	{
		GUIUtility.CheckOnGUI();
		return DoModalWindow(id, clientRect, func, GUIContent.Temp(image), style, skin);
	}

	public static Rect ModalWindow(int id, Rect clientRect, WindowFunction func, GUIContent content, GUIStyle style)
	{
		GUIUtility.CheckOnGUI();
		return DoModalWindow(id, clientRect, func, content, style, skin);
	}

	private static Rect DoWindow(int id, Rect clientRect, WindowFunction func, GUIContent title, GUIStyle style, GUISkin skin, bool forceRectOnLayout)
	{
		return Internal_DoWindow(id, GUIUtility.s_OriginalID, clientRect, func, title, style, skin, forceRectOnLayout);
	}

	private static Rect DoModalWindow(int id, Rect clientRect, WindowFunction func, GUIContent content, GUIStyle style, GUISkin skin)
	{
		return Internal_DoModalWindow(id, GUIUtility.s_OriginalID, clientRect, func, content, style, skin);
	}

	[RequiredByNativeCode]
	internal static void CallWindowDelegate(WindowFunction func, int id, int instanceID, GUISkin _skin, int forceRect, float width, float height, GUIStyle style)
	{
		GUILayoutUtility.SelectIDList(id, isWindow: true);
		GUISkin gUISkin = skin;
		if (Event.current.type == EventType.Layout)
		{
			if (forceRect != 0)
			{
				GUILayoutOption[] options = new GUILayoutOption[2]
				{
					GUILayout.Width(width),
					GUILayout.Height(height)
				};
				GUILayoutUtility.BeginWindow(id, style, options);
			}
			else
			{
				GUILayoutUtility.BeginWindow(id, style, null);
			}
		}
		else
		{
			GUILayoutUtility.BeginWindow(id, GUIStyle.none, null);
		}
		skin = _skin;
		func?.Invoke(id);
		if (Event.current.type == EventType.Layout)
		{
			GUILayoutUtility.Layout();
		}
		skin = gUISkin;
	}

	public static void DragWindow()
	{
		DragWindow(new Rect(0f, 0f, 10000f, 10000f));
	}

	internal static void BeginWindows(int skinMode, int editorWindowInstanceID)
	{
		GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
		GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
		GUILayoutGroup windows = GUILayoutUtility.current.windows;
		Matrix4x4 matrix4x = matrix;
		Internal_BeginWindows();
		matrix = matrix4x;
		GUILayoutUtility.current.topLevel = topLevel;
		GUILayoutUtility.current.layoutGroups = layoutGroups;
		GUILayoutUtility.current.windows = windows;
	}

	internal static void EndWindows()
	{
		GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
		GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
		GUILayoutGroup windows = GUILayoutUtility.current.windows;
		Internal_EndWindows();
		GUILayoutUtility.current.topLevel = topLevel;
		GUILayoutUtility.current.layoutGroups = layoutGroups;
		GUILayoutUtility.current.windows = windows;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_color_Injected(out Color ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_color_Injected(ref Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_backgroundColor_Injected(out Color ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_backgroundColor_Injected(ref Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_contentColor_Injected(out Color ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_contentColor_Injected(ref Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_DoModalWindow_Injected(int id, int instanceID, ref Rect clientRect, WindowFunction func, GUIContent content, GUIStyle style, object skin, out Rect ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_DoWindow_Injected(int id, int instanceID, ref Rect clientRect, WindowFunction func, GUIContent title, GUIStyle style, object skin, bool forceRectOnLayout, out Rect ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void DragWindow_Injected(ref Rect position);
}
[NativeHeader("Modules/IMGUI/GUIState.h")]
[NativeHeader("Modules/IMGUI/GUIClip.h")]
internal sealed class GUIClip
{
	internal struct ParentClipScope : IDisposable
	{
		private bool m_Disposed;

		public ParentClipScope(Matrix4x4 objectTransform, Rect clipRect)
		{
			m_Disposed = false;
			Internal_PushParentClip(objectTransform, clipRect);
		}

		public void Dispose()
		{
			if (!m_Disposed)
			{
				m_Disposed = true;
				Internal_PopParentClip();
			}
		}
	}

	internal static extern bool enabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetEnabled")]
		get;
	}

	internal static Rect visibleRect
	{
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetVisibleRect")]
		get
		{
			get_visibleRect_Injected(out var ret);
			return ret;
		}
	}

	internal static Rect topmostRect
	{
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetTopMostPhysicalRect")]
		get
		{
			get_topmostRect_Injected(out var ret);
			return ret;
		}
	}

	internal static void Internal_Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
	{
		Internal_Push_Injected(ref screenRect, ref scrollOffset, ref renderOffset, resetOffset);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern void Internal_Pop();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetCount")]
	internal static extern int Internal_GetCount();

	[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetTopRect")]
	internal static Rect GetTopRect()
	{
		GetTopRect_Injected(out var ret);
		return ret;
	}

	[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Unclip")]
	private static Vector2 Unclip_Vector2(Vector2 pos)
	{
		Unclip_Vector2_Injected(ref pos, out var ret);
		return ret;
	}

	[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Unclip")]
	private static Rect Unclip_Rect(Rect rect)
	{
		Unclip_Rect_Injected(ref rect, out var ret);
		return ret;
	}

	[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Clip")]
	private static Vector2 Clip_Vector2(Vector2 absolutePos)
	{
		Clip_Vector2_Injected(ref absolutePos, out var ret);
		return ret;
	}

	[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Clip")]
	private static Rect Internal_Clip_Rect(Rect absoluteRect)
	{
		Internal_Clip_Rect_Injected(ref absoluteRect, out var ret);
		return ret;
	}

	[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.UnclipToWindow")]
	private static Vector2 UnclipToWindow_Vector2(Vector2 pos)
	{
		UnclipToWindow_Vector2_Injected(ref pos, out var ret);
		return ret;
	}

	[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.UnclipToWindow")]
	private static Rect UnclipToWindow_Rect(Rect rect)
	{
		UnclipToWindow_Rect_Injected(ref rect, out var ret);
		return ret;
	}

	[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.ClipToWindow")]
	private static Vector2 ClipToWindow_Vector2(Vector2 absolutePos)
	{
		ClipToWindow_Vector2_Injected(ref absolutePos, out var ret);
		return ret;
	}

	[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.ClipToWindow")]
	private static Rect ClipToWindow_Rect(Rect absoluteRect)
	{
		ClipToWindow_Rect_Injected(ref absoluteRect, out var ret);
		return ret;
	}

	[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetAbsoluteMousePosition")]
	private static Vector2 Internal_GetAbsoluteMousePosition()
	{
		Internal_GetAbsoluteMousePosition_Injected(out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern void Reapply();

	[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetUserMatrix")]
	internal static Matrix4x4 GetMatrix()
	{
		GetMatrix_Injected(out var ret);
		return ret;
	}

	internal static void SetMatrix(Matrix4x4 m)
	{
		SetMatrix_Injected(ref m);
	}

	[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetParentTransform")]
	internal static Matrix4x4 GetParentMatrix()
	{
		GetParentMatrix_Injected(out var ret);
		return ret;
	}

	internal static void Internal_PushParentClip(Matrix4x4 objectTransform, Rect clipRect)
	{
		Internal_PushParentClip(objectTransform, objectTransform, clipRect);
	}

	internal static void Internal_PushParentClip(Matrix4x4 renderTransform, Matrix4x4 inputTransform, Rect clipRect)
	{
		Internal_PushParentClip_Injected(ref renderTransform, ref inputTransform, ref clipRect);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern void Internal_PopParentClip();

	internal static void Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
	{
		Internal_Push(screenRect, scrollOffset, renderOffset, resetOffset);
	}

	internal static void Pop()
	{
		Internal_Pop();
	}

	public static Vector2 Unclip(Vector2 pos)
	{
		return Unclip_Vector2(pos);
	}

	public static Rect Unclip(Rect rect)
	{
		return Unclip_Rect(rect);
	}

	public static Vector2 Clip(Vector2 absolutePos)
	{
		return Clip_Vector2(absolutePos);
	}

	public static Rect Clip(Rect absoluteRect)
	{
		return Internal_Clip_Rect(absoluteRect);
	}

	public static Vector2 UnclipToWindow(Vector2 pos)
	{
		return UnclipToWindow_Vector2(pos);
	}

	public static Rect UnclipToWindow(Rect rect)
	{
		return UnclipToWindow_Rect(rect);
	}

	public static Vector2 ClipToWindow(Vector2 absolutePos)
	{
		return ClipToWindow_Vector2(absolutePos);
	}

	public static Rect ClipToWindow(Rect absoluteRect)
	{
		return ClipToWindow_Rect(absoluteRect);
	}

	public static Vector2 GetAbsoluteMousePosition()
	{
		return Internal_GetAbsoluteMousePosition();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_visibleRect_Injected(out Rect ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_topmostRect_Injected(out Rect ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_Push_Injected(ref Rect screenRect, ref Vector2 scrollOffset, ref Vector2 renderOffset, bool resetOffset);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void GetTopRect_Injected(out Rect ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Unclip_Vector2_Injected(ref Vector2 pos, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Unclip_Rect_Injected(ref Rect rect, out Rect ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Clip_Vector2_Injected(ref Vector2 absolutePos, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_Clip_Rect_Injected(ref Rect absoluteRect, out Rect ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void UnclipToWindow_Vector2_Injected(ref Vector2 pos, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void UnclipToWindow_Rect_Injected(ref Rect rect, out Rect ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void ClipToWindow_Vector2_Injected(ref Vector2 absolutePos, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void ClipToWindow_Rect_Injected(ref Rect absoluteRect, out Rect ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_GetAbsoluteMousePosition_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void GetMatrix_Injected(out Matrix4x4 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void SetMatrix_Injected(ref Matrix4x4 m);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void GetParentMatrix_Injected(out Matrix4x4 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_PushParentClip_Injected(ref Matrix4x4 renderTransform, ref Matrix4x4 inputTransform, ref Rect clipRect);
}
[Serializable]
[StructLayout(LayoutKind.Sequential)]
[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
[NativeHeader("Modules/IMGUI/GUIContent.h")]
public class GUIContent
{
	[SerializeField]
	private string m_Text = string.Empty;

	[SerializeField]
	private Texture m_Image;

	[SerializeField]
	private string m_Tooltip = string.Empty;

	private static readonly GUIContent s_Text = new GUIContent();

	private static readonly GUIContent s_Image = new GUIContent();

	private static readonly GUIContent s_TextImage = new GUIContent();

	public static GUIContent none = new GUIContent("");

	public string text
	{
		get
		{
			return m_Text;
		}
		set
		{
			if (!(value == m_Text))
			{
				m_Text = value;
				this.OnTextChanged?.Invoke();
			}
		}
	}

	public Texture image
	{
		get
		{
			return m_Image;
		}
		set
		{
			m_Image = value;
		}
	}

	public string tooltip
	{
		get
		{
			return m_Tooltip;
		}
		set
		{
			m_Tooltip = value;
		}
	}

	internal int hash
	{
		get
		{
			int result = 0;
			if (!string.IsNullOrEmpty(m_Text))
			{
				result = m_Text.GetHashCode() * 37;
			}
			return result;
		}
	}

	internal event Action OnTextChanged;

	public GUIContent()
	{
	}

	public GUIContent(string text)
		: this(text, null, string.Empty)
	{
	}

	public GUIContent(Texture image)
		: this(string.Empty, image, string.Empty)
	{
	}

	public GUIContent(string text, Texture image)
		: this(text, image, string.Empty)
	{
	}

	public GUIContent(string text, string tooltip)
		: this(text, null, tooltip)
	{
	}

	public GUIContent(Texture image, string tooltip)
		: this(string.Empty, image, tooltip)
	{
	}

	public GUIContent(string text, Texture image, string tooltip)
	{
		this.text = text;
		this.image = image;
		this.tooltip = tooltip;
	}

	public GUIContent(GUIContent src)
	{
		text = src.m_Text;
		image = src.m_Image;
		tooltip = src.m_Tooltip;
	}

	internal static GUIContent Temp(string t)
	{
		s_Text.m_Text = t;
		s_Text.m_Tooltip = string.Empty;
		return s_Text;
	}

	internal static GUIContent Temp(string t, string tooltip)
	{
		s_Text.m_Text = t;
		s_Text.m_Tooltip = tooltip;
		return s_Text;
	}

	internal static GUIContent Temp(Texture i)
	{
		s_Image.m_Image = i;
		s_Image.m_Tooltip = string.Empty;
		return s_Image;
	}

	internal static GUIContent Temp(Texture i, string tooltip)
	{
		s_Image.m_Image = i;
		s_Image.m_Tooltip = tooltip;
		return s_Image;
	}

	internal static GUIContent Temp(string t, Texture i)
	{
		s_TextImage.m_Text = t;
		s_TextImage.m_Image = i;
		return s_TextImage;
	}

	internal static void ClearStaticCache()
	{
		s_Text.m_Text = null;
		s_Text.m_Tooltip = string.Empty;
		s_Image.m_Image = null;
		s_Image.m_Tooltip = string.Empty;
		s_TextImage.m_Text = null;
		s_TextImage.m_Image = null;
	}

	internal static GUIContent[] Temp(string[] texts)
	{
		GUIContent[] array = new GUIContent[texts.Length];
		for (int i = 0; i < texts.Length; i++)
		{
			array[i] = new GUIContent(texts[i]);
		}
		return array;
	}

	internal static GUIContent[] Temp(Texture[] images)
	{
		GUIContent[] array = new GUIContent[images.Length];
		for (int i = 0; i < images.Length; i++)
		{
			array[i] = new GUIContent(images[i]);
		}
		return array;
	}

	public override string ToString()
	{
		return text ?? tooltip ?? base.ToString();
	}
}
[NativeHeader("Modules/IMGUI/GUIDebugger.bindings.h")]
internal class GUIDebugger
{
	[NativeConditional("UNITY_EDITOR")]
	public static extern bool active
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[NativeConditional("UNITY_EDITOR")]
	public static void LogLayoutEntry(Rect rect, int left, int right, int top, int bottom, GUIStyle style)
	{
		LogLayoutEntry_Injected(ref rect, left, right, top, bottom, style);
	}

	[NativeConditional("UNITY_EDITOR")]
	public static void LogLayoutGroupEntry(Rect rect, int left, int right, int top, int bottom, GUIStyle style, bool isVertical)
	{
		LogLayoutGroupEntry_Injected(ref rect, left, right, top, bottom, style, isVertical);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeConditional("UNITY_EDITOR")]
	[StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
	[NativeMethod("LogEndGroup")]
	public static extern void LogLayoutEndGroup();

	[StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
	[NativeConditional("UNITY_EDITOR")]
	public static void LogBeginProperty(string targetTypeAssemblyQualifiedName, string path, Rect position)
	{
		LogBeginProperty_Injected(targetTypeAssemblyQualifiedName, path, ref position);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("GetGUIDebuggerManager()", StaticAccessorType.Dot)]
	[NativeConditional("UNITY_EDITOR")]
	public static extern void LogEndProperty();

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void LogLayoutEntry_Injected(ref Rect rect, int left, int right, int top, int bottom, GUIStyle style);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void LogLayoutGroupEntry_Injected(ref Rect rect, int left, int right, int top, int bottom, GUIStyle style, bool isVertical);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void LogBeginProperty_Injected(string targetTypeAssemblyQualifiedName, string path, ref Rect position);
}
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("GUIElement has been removed. Consider using https://docs.unity3d.com/ScriptReference/UIElements.Image.html, https://docs.unity3d.com/ScriptReference/UIElements.TextElement.html or TextMeshPro instead.", true)]
[ExcludeFromObjectFactory]
[ExcludeFromPreset]
public sealed class GUIElement
{
	private static void FeatureRemoved()
	{
		throw new Exception("GUIElement has been removed from Unity. Consider using https://docs.unity3d.com/ScriptReference/UIElements.Image.html, https://docs.unity3d.com/ScriptReference/UIElements.TextElement.html or TextMeshPro instead.");
	}

	[Obsolete("GUIElement has been removed. Consider using https://docs.unity3d.com/ScriptReference/UIElements.Image.html, https://docs.unity3d.com/ScriptReference/UIElements.TextElement.html or TextMeshPro instead.", true)]
	public bool HitTest(Vector3 screenPosition)
	{
		FeatureRemoved();
		return false;
	}

	[Obsolete("GUIElement has been removed. Consider using https://docs.unity3d.com/ScriptReference/UIElements.Image.html, https://docs.unity3d.com/ScriptReference/UIElements.TextElement.html or TextMeshPro instead.", true)]
	public bool HitTest(Vector3 screenPosition, [UnityEngine.Internal.DefaultValue("null")] Camera camera)
	{
		FeatureRemoved();
		return false;
	}

	[Obsolete("GUIElement has been removed. Consider using https://docs.unity3d.com/ScriptReference/UIElements.Image.html, https://docs.unity3d.com/ScriptReference/UIElements.TextElement.html or TextMeshPro instead.", true)]
	public Rect GetScreenRect([UnityEngine.Internal.DefaultValue("null")] Camera camera)
	{
		FeatureRemoved();
		return new Rect(0f, 0f, 0f, 0f);
	}

	[Obsolete("GUIElement has been removed. Consider using https://docs.unity3d.com/ScriptReference/UIElements.Image.html, https://docs.unity3d.com/ScriptReference/UIElements.TextElement.html or TextMeshPro instead.", true)]
	public Rect GetScreenRect()
	{
		FeatureRemoved();
		return new Rect(0f, 0f, 0f, 0f);
	}
}
public enum ScaleMode
{
	StretchToFill,
	ScaleAndCrop,
	ScaleToFit
}
public enum FocusType
{
	[Obsolete("FocusType.Native now behaves the same as FocusType.Passive in all OS cases. (UnityUpgradable) -> Passive", false)]
	Native,
	Keyboard,
	Passive
}
[Obsolete("GUILayer has been removed.", true)]
[ExcludeFromPreset]
[ExcludeFromObjectFactory]
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class GUILayer
{
	[Obsolete("GUILayer has been removed.", true)]
	public GUIElement HitTest(Vector3 screenPosition)
	{
		throw new Exception("GUILayer has been removed from Unity.");
	}
}
public class GUILayout
{
	private sealed class LayoutedWindow
	{
		private readonly GUI.WindowFunction m_Func;

		private readonly Rect m_ScreenRect;

		private readonly GUILayoutOption[] m_Options;

		private readonly GUIStyle m_Style;

		internal LayoutedWindow(GUI.WindowFunction f, Rect screenRect, GUIContent content, GUILayoutOption[] options, GUIStyle style)
		{
			m_Func = f;
			m_ScreenRect = screenRect;
			m_Options = options;
			m_Style = style;
		}

		public void DoWindow(int windowID)
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			EventType type = Event.current.type;
			EventType eventType = type;
			if (eventType == EventType.Layout)
			{
				topLevel.resetCoords = true;
				topLevel.rect = m_ScreenRect;
				if (m_Options != null)
				{
					topLevel.ApplyOptions(m_Options);
				}
				topLevel.isWindow = true;
				topLevel.windowID = windowID;
				topLevel.style = m_Style;
			}
			else
			{
				topLevel.ResetCursor();
			}
			m_Func(windowID);
		}
	}

	public class HorizontalScope : GUI.Scope
	{
		public HorizontalScope(params GUILayoutOption[] options)
		{
			BeginHorizontal(options);
		}

		public HorizontalScope(GUIStyle style, params GUILayoutOption[] options)
		{
			BeginHorizontal(style, options);
		}

		public HorizontalScope(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			BeginHorizontal(text, style, options);
		}

		public HorizontalScope(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			BeginHorizontal(image, style, options);
		}

		public HorizontalScope(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			BeginHorizontal(content, style, options);
		}

		protected override void CloseScope()
		{
			EndHorizontal();
		}
	}

	public class VerticalScope : GUI.Scope
	{
		public VerticalScope(params GUILayoutOption[] options)
		{
			BeginVertical(options);
		}

		public VerticalScope(GUIStyle style, params GUILayoutOption[] options)
		{
			BeginVertical(style, options);
		}

		public VerticalScope(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			BeginVertical(text, style, options);
		}

		public VerticalScope(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			BeginVertical(image, style, options);
		}

		public VerticalScope(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			BeginVertical(content, style, options);
		}

		protected override void CloseScope()
		{
			EndVertical();
		}
	}

	public class AreaScope : GUI.Scope
	{
		public AreaScope(Rect screenRect)
		{
			BeginArea(screenRect);
		}

		public AreaScope(Rect screenRect, string text)
		{
			BeginArea(screenRect, text);
		}

		public AreaScope(Rect screenRect, Texture image)
		{
			BeginArea(screenRect, image);
		}

		public AreaScope(Rect screenRect, GUIContent content)
		{
			BeginArea(screenRect, content);
		}

		public AreaScope(Rect screenRect, string text, GUIStyle style)
		{
			BeginArea(screenRect, text, style);
		}

		public AreaScope(Rect screenRect, Texture image, GUIStyle style)
		{
			BeginArea(screenRect, image, style);
		}

		public AreaScope(Rect screenRect, GUIContent content, GUIStyle style)
		{
			BeginArea(screenRect, content, style);
		}

		protected override void CloseScope()
		{
			EndArea();
		}
	}

	public class ScrollViewScope : GUI.Scope
	{
		public Vector2 scrollPosition { get; private set; }

		public bool handleScrollWheel { get; set; }

		public ScrollViewScope(Vector2 scrollPosition, params GUILayoutOption[] options)
		{
			handleScrollWheel = true;
			this.scrollPosition = BeginScrollView(scrollPosition, options);
		}

		public ScrollViewScope(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, params GUILayoutOption[] options)
		{
			handleScrollWheel = true;
			this.scrollPosition = BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, options);
		}

		public ScrollViewScope(Vector2 scrollPosition, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
		{
			handleScrollWheel = true;
			this.scrollPosition = BeginScrollView(scrollPosition, horizontalScrollbar, verticalScrollbar, options);
		}

		public ScrollViewScope(Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options)
		{
			handleScrollWheel = true;
			this.scrollPosition = BeginScrollView(scrollPosition, style, options);
		}

		public ScrollViewScope(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
		{
			handleScrollWheel = true;
			this.scrollPosition = BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, options);
		}

		public ScrollViewScope(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background, params GUILayoutOption[] options)
		{
			handleScrollWheel = true;
			this.scrollPosition = BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background, options);
		}

		protected override void CloseScope()
		{
			EndScrollView(handleScrollWheel);
		}
	}

	public static void Label(Texture image, params GUILayoutOption[] options)
	{
		DoLabel(GUIContent.Temp(image), GUI.skin.label, options);
	}

	public static void Label(string text, params GUILayoutOption[] options)
	{
		DoLabel(GUIContent.Temp(text), GUI.skin.label, options);
	}

	public static void Label(GUIContent content, params GUILayoutOption[] options)
	{
		DoLabel(content, GUI.skin.label, options);
	}

	public static void Label(Texture image, GUIStyle style, params GUILayoutOption[] options)
	{
		DoLabel(GUIContent.Temp(image), style, options);
	}

	public static void Label(string text, GUIStyle style, params GUILayoutOption[] options)
	{
		DoLabel(GUIContent.Temp(text), style, options);
	}

	public static void Label(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
	{
		DoLabel(content, style, options);
	}

	private static void DoLabel(GUIContent content, GUIStyle style, GUILayoutOption[] options)
	{
		GUI.Label(GUILayoutUtility.GetRect(content, style, options), content, style);
	}

	public static void Box(Texture image, params GUILayoutOption[] options)
	{
		DoBox(GUIContent.Temp(image), GUI.skin.box, options);
	}

	public static void Box(string text, params GUILayoutOption[] options)
	{
		DoBox(GUIContent.Temp(text), GUI.skin.box, options);
	}

	public static void Box(GUIContent content, params GUILayoutOption[] options)
	{
		DoBox(content, GUI.skin.box, options);
	}

	public static void Box(Texture image, GUIStyle style, params GUILayoutOption[] options)
	{
		DoBox(GUIContent.Temp(image), style, options);
	}

	public static void Box(string text, GUIStyle style, params GUILayoutOption[] options)
	{
		DoBox(GUIContent.Temp(text), style, options);
	}

	public static void Box(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
	{
		DoBox(content, style, options);
	}

	private static void DoBox(GUIContent content, GUIStyle style, GUILayoutOption[] options)
	{
		GUI.Box(GUILayoutUtility.GetRect(content, style, options), content, style);
	}

	public static bool Button(Texture image, params GUILayoutOption[] options)
	{
		return DoButton(GUIContent.Temp(image), GUI.skin.button, options);
	}

	public static bool Button(string text, params GUILayoutOption[] options)
	{
		return DoButton(GUIContent.Temp(text), GUI.skin.button, options);
	}

	public static bool Button(GUIContent content, params GUILayoutOption[] options)
	{
		return DoButton(content, GUI.skin.button, options);
	}

	public static bool Button(Texture image, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoButton(GUIContent.Temp(image), style, options);
	}

	public static bool Button(string text, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoButton(GUIContent.Temp(text), style, options);
	}

	public static bool Button(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoButton(content, style, options);
	}

	private static bool DoButton(GUIContent content, GUIStyle style, GUILayoutOption[] options)
	{
		return GUI.Button(GUILayoutUtility.GetRect(content, style, options), content, style);
	}

	public static bool RepeatButton(Texture image, params GUILayoutOption[] options)
	{
		return DoRepeatButton(GUIContent.Temp(image), GUI.skin.button, options);
	}

	public static bool RepeatButton(string text, params GUILayoutOption[] options)
	{
		return DoRepeatButton(GUIContent.Temp(text), GUI.skin.button, options);
	}

	public static bool RepeatButton(GUIContent content, params GUILayoutOption[] options)
	{
		return DoRepeatButton(content, GUI.skin.button, options);
	}

	public static bool RepeatButton(Texture image, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoRepeatButton(GUIContent.Temp(image), style, options);
	}

	public static bool RepeatButton(string text, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoRepeatButton(GUIContent.Temp(text), style, options);
	}

	public static bool RepeatButton(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoRepeatButton(content, style, options);
	}

	private static bool DoRepeatButton(GUIContent content, GUIStyle style, GUILayoutOption[] options)
	{
		return GUI.RepeatButton(GUILayoutUtility.GetRect(content, style, options), content, style);
	}

	public static string TextField(string text, params GUILayoutOption[] options)
	{
		return DoTextField(text, -1, multiline: false, GUI.skin.textField, options);
	}

	public static string TextField(string text, int maxLength, params GUILayoutOption[] options)
	{
		return DoTextField(text, maxLength, multiline: false, GUI.skin.textField, options);
	}

	public static string TextField(string text, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoTextField(text, -1, multiline: false, style, options);
	}

	public static string TextField(string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoTextField(text, maxLength, multiline: false, style, options);
	}

	public static string PasswordField(string password, char maskChar, params GUILayoutOption[] options)
	{
		return PasswordField(password, maskChar, -1, GUI.skin.textField, options);
	}

	public static string PasswordField(string password, char maskChar, int maxLength, params GUILayoutOption[] options)
	{
		return PasswordField(password, maskChar, maxLength, GUI.skin.textField, options);
	}

	public static string PasswordField(string password, char maskChar, GUIStyle style, params GUILayoutOption[] options)
	{
		return PasswordField(password, maskChar, -1, style, options);
	}

	public static string PasswordField(string password, char maskChar, int maxLength, GUIStyle style, params GUILayoutOption[] options)
	{
		GUIContent content = GUIContent.Temp(GUI.PasswordFieldGetStrToShow(password, maskChar));
		return GUI.PasswordField(GUILayoutUtility.GetRect(content, GUI.skin.textField, options), password, maskChar, maxLength, style);
	}

	public static string TextArea(string text, params GUILayoutOption[] options)
	{
		return DoTextField(text, -1, multiline: true, GUI.skin.textArea, options);
	}

	public static string TextArea(string text, int maxLength, params GUILayoutOption[] options)
	{
		return DoTextField(text, maxLength, multiline: true, GUI.skin.textArea, options);
	}

	public static string TextArea(string text, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoTextField(text, -1, multiline: true, style, options);
	}

	public static string TextArea(string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoTextField(text, maxLength, multiline: true, style, options);
	}

	private static string DoTextField(string text, int maxLength, bool multiline, GUIStyle style, GUILayoutOption[] options)
	{
		int controlID = GUIUtility.GetControlID(FocusType.Keyboard);
		GUIContent gUIContent = GUIContent.Temp(text);
		gUIContent = ((GUIUtility.keyboardControl == controlID) ? GUIContent.Temp(text + GUIUtility.compositionString) : GUIContent.Temp(text));
		Rect rect = GUILayoutUtility.GetRect(gUIContent, style, options);
		if (GUIUtility.keyboardControl == controlID)
		{
			gUIContent = GUIContent.Temp(text);
		}
		GUI.DoTextField(rect, controlID, gUIContent, multiline, maxLength, style);
		return gUIContent.text;
	}

	public static bool Toggle(bool value, Texture image, params GUILayoutOption[] options)
	{
		return DoToggle(value, GUIContent.Temp(image), GUI.skin.toggle, options);
	}

	public static bool Toggle(bool value, string text, params GUILayoutOption[] options)
	{
		return DoToggle(value, GUIContent.Temp(text), GUI.skin.toggle, options);
	}

	public static bool Toggle(bool value, GUIContent content, params GUILayoutOption[] options)
	{
		return DoToggle(value, content, GUI.skin.toggle, options);
	}

	public static bool Toggle(bool value, Texture image, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoToggle(value, GUIContent.Temp(image), style, options);
	}

	public static bool Toggle(bool value, string text, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoToggle(value, GUIContent.Temp(text), style, options);
	}

	public static bool Toggle(bool value, GUIContent content, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoToggle(value, content, style, options);
	}

	private static bool DoToggle(bool value, GUIContent content, GUIStyle style, GUILayoutOption[] options)
	{
		return GUI.Toggle(GUILayoutUtility.GetRect(content, style, options), value, content, style);
	}

	public static int Toolbar(int selected, string[] texts, params GUILayoutOption[] options)
	{
		return Toolbar(selected, GUIContent.Temp(texts), GUI.skin.button, options);
	}

	public static int Toolbar(int selected, Texture[] images, params GUILayoutOption[] options)
	{
		return Toolbar(selected, GUIContent.Temp(images), GUI.skin.button, options);
	}

	public static int Toolbar(int selected, GUIContent[] contents, params GUILayoutOption[] options)
	{
		return Toolbar(selected, contents, GUI.skin.button, options);
	}

	public static int Toolbar(int selected, string[] texts, GUIStyle style, params GUILayoutOption[] options)
	{
		return Toolbar(selected, GUIContent.Temp(texts), style, options);
	}

	public static int Toolbar(int selected, Texture[] images, GUIStyle style, params GUILayoutOption[] options)
	{
		return Toolbar(selected, GUIContent.Temp(images), style, options);
	}

	public static int Toolbar(int selected, string[] texts, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
	{
		return Toolbar(selected, GUIContent.Temp(texts), style, buttonSize, options);
	}

	public static int Toolbar(int selected, Texture[] images, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
	{
		return Toolbar(selected, GUIContent.Temp(images), style, buttonSize, options);
	}

	public static int Toolbar(int selected, GUIContent[] contents, GUIStyle style, params GUILayoutOption[] options)
	{
		return Toolbar(selected, contents, style, GUI.ToolbarButtonSize.Fixed, options);
	}

	public static int Toolbar(int selected, GUIContent[] contents, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
	{
		return Toolbar(selected, contents, null, style, buttonSize, options);
	}

	public static int Toolbar(int selected, GUIContent[] contents, bool[] enabled, GUIStyle style, params GUILayoutOption[] options)
	{
		return Toolbar(selected, contents, enabled, style, GUI.ToolbarButtonSize.Fixed, options);
	}

	public static int Toolbar(int selected, GUIContent[] contents, bool[] enabled, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
	{
		GUI.FindStyles(ref style, out var firstStyle, out var midStyle, out var lastStyle, "left", "mid", "right");
		Vector2 vector = default(Vector2);
		int num = contents.Length;
		GUIStyle gUIStyle = ((num > 1) ? firstStyle : style);
		GUIStyle gUIStyle2 = ((num > 1) ? midStyle : style);
		GUIStyle gUIStyle3 = ((num > 1) ? lastStyle : style);
		float num2 = 0f;
		for (int i = 0; i < contents.Length; i++)
		{
			if (i == num - 2)
			{
				gUIStyle2 = gUIStyle3;
			}
			Vector2 vector2 = gUIStyle.CalcSize(contents[i]);
			switch (buttonSize)
			{
			case GUI.ToolbarButtonSize.Fixed:
				if (vector2.x > vector.x)
				{
					vector.x = vector2.x;
				}
				break;
			case GUI.ToolbarButtonSize.FitToContents:
				vector.x += vector2.x;
				break;
			}
			if (vector2.y > vector.y)
			{
				vector.y = vector2.y;
			}
			num2 = ((i != num - 1) ? (num2 + (float)Mathf.Max(gUIStyle.margin.right, gUIStyle2.margin.left)) : (num2 + (float)gUIStyle.margin.right));
			gUIStyle = gUIStyle2;
		}
		switch (buttonSize)
		{
		case GUI.ToolbarButtonSize.Fixed:
			vector.x = vector.x * (float)contents.Length + num2;
			break;
		case GUI.ToolbarButtonSize.FitToContents:
			vector.x += num2;
			break;
		}
		return GUI.Toolbar(GUILayoutUtility.GetRect(vector.x, vector.y, style, options), selected, contents, null, style, buttonSize, enabled);
	}

	public static int SelectionGrid(int selected, string[] texts, int xCount, params GUILayoutOption[] options)
	{
		return SelectionGrid(selected, GUIContent.Temp(texts), xCount, GUI.skin.button, options);
	}

	public static int SelectionGrid(int selected, Texture[] images, int xCount, params GUILayoutOption[] options)
	{
		return SelectionGrid(selected, GUIContent.Temp(images), xCount, GUI.skin.button, options);
	}

	public static int SelectionGrid(int selected, GUIContent[] content, int xCount, params GUILayoutOption[] options)
	{
		return SelectionGrid(selected, content, xCount, GUI.skin.button, options);
	}

	public static int SelectionGrid(int selected, string[] texts, int xCount, GUIStyle style, params GUILayoutOption[] options)
	{
		return SelectionGrid(selected, GUIContent.Temp(texts), xCount, style, options);
	}

	public static int SelectionGrid(int selected, Texture[] images, int xCount, GUIStyle style, params GUILayoutOption[] options)
	{
		return SelectionGrid(selected, GUIContent.Temp(images), xCount, style, options);
	}

	public static int SelectionGrid(int selected, GUIContent[] contents, int xCount, GUIStyle style, params GUILayoutOption[] options)
	{
		return GUI.SelectionGrid(GUIGridSizer.GetRect(contents, xCount, style, options), selected, contents, xCount, style);
	}

	public static float HorizontalSlider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
	{
		return DoHorizontalSlider(value, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, options);
	}

	public static float HorizontalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
	{
		return DoHorizontalSlider(value, leftValue, rightValue, slider, thumb, options);
	}

	private static float DoHorizontalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUILayoutOption[] options)
	{
		return GUI.HorizontalSlider(GUILayoutUtility.GetRect(GUIContent.Temp("mmmm"), slider, options), value, leftValue, rightValue, slider, thumb);
	}

	public static float VerticalSlider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
	{
		return DoVerticalSlider(value, leftValue, rightValue, GUI.skin.verticalSlider, GUI.skin.verticalSliderThumb, options);
	}

	public static float VerticalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
	{
		return DoVerticalSlider(value, leftValue, rightValue, slider, thumb, options);
	}

	private static float DoVerticalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
	{
		return GUI.VerticalSlider(GUILayoutUtility.GetRect(GUIContent.Temp("\n\n\n\n\n"), slider, options), value, leftValue, rightValue, slider, thumb);
	}

	public static float HorizontalScrollbar(float value, float size, float leftValue, float rightValue, params GUILayoutOption[] options)
	{
		return HorizontalScrollbar(value, size, leftValue, rightValue, GUI.skin.horizontalScrollbar, options);
	}

	public static float HorizontalScrollbar(float value, float size, float leftValue, float rightValue, GUIStyle style, params GUILayoutOption[] options)
	{
		return GUI.HorizontalScrollbar(GUILayoutUtility.GetRect(GUIContent.Temp("mmmm"), style, options), value, size, leftValue, rightValue, style);
	}

	public static float VerticalScrollbar(float value, float size, float topValue, float bottomValue, params GUILayoutOption[] options)
	{
		return VerticalScrollbar(value, size, topValue, bottomValue, GUI.skin.verticalScrollbar, options);
	}

	public static float VerticalScrollbar(float value, float size, float topValue, float bottomValue, GUIStyle style, params GUILayoutOption[] options)
	{
		return GUI.VerticalScrollbar(GUILayoutUtility.GetRect(GUIContent.Temp("\n\n\n\n"), style, options), value, size, topValue, bottomValue, style);
	}

	public static void Space(float pixels)
	{
		GUIUtility.CheckOnGUI();
		if (GUILayoutUtility.current.topLevel.isVertical)
		{
			GUILayoutUtility.GetRect(0f, pixels, GUILayoutUtility.spaceStyle, Height(pixels));
		}
		else
		{
			GUILayoutUtility.GetRect(pixels, 0f, GUILayoutUtility.spaceStyle, Width(pixels));
		}
		if (Event.current.type == EventType.Layout)
		{
			GUILayoutUtility.current.topLevel.entries[GUILayoutUtility.current.topLevel.entries.Count - 1].consideredForMargin = false;
		}
	}

	public static void FlexibleSpace()
	{
		GUIUtility.CheckOnGUI();
		GUILayoutOption gUILayoutOption = ((!GUILayoutUtility.current.topLevel.isVertical) ? ExpandWidth(expand: true) : ExpandHeight(expand: true));
		gUILayoutOption = new GUILayoutOption(gUILayoutOption.type, 10000);
		GUILayoutUtility.GetRect(0f, 0f, GUILayoutUtility.spaceStyle, gUILayoutOption);
		if (Event.current.type == EventType.Layout)
		{
			GUILayoutUtility.current.topLevel.entries[GUILayoutUtility.current.topLevel.entries.Count - 1].consideredForMargin = false;
		}
	}

	public static void BeginHorizontal(params GUILayoutOption[] options)
	{
		BeginHorizontal(GUIContent.none, GUIStyle.none, options);
	}

	public static void BeginHorizontal(GUIStyle style, params GUILayoutOption[] options)
	{
		BeginHorizontal(GUIContent.none, style, options);
	}

	public static void BeginHorizontal(string text, GUIStyle style, params GUILayoutOption[] options)
	{
		BeginHorizontal(GUIContent.Temp(text), style, options);
	}

	public static void BeginHorizontal(Texture image, GUIStyle style, params GUILayoutOption[] options)
	{
		BeginHorizontal(GUIContent.Temp(image), style, options);
	}

	public static void BeginHorizontal(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
	{
		GUILayoutGroup gUILayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
		gUILayoutGroup.isVertical = false;
		if (style != GUIStyle.none || content != GUIContent.none)
		{
			GUI.Box(gUILayoutGroup.rect, content, style);
		}
	}

	public static void EndHorizontal()
	{
		GUILayoutUtility.EndLayoutGroup();
	}

	public static void BeginVertical(params GUILayoutOption[] options)
	{
		BeginVertical(GUIContent.none, GUIStyle.none, options);
	}

	public static void BeginVertical(GUIStyle style, params GUILayoutOption[] options)
	{
		BeginVertical(GUIContent.none, style, options);
	}

	public static void BeginVertical(string text, GUIStyle style, params GUILayoutOption[] options)
	{
		BeginVertical(GUIContent.Temp(text), style, options);
	}

	public static void BeginVertical(Texture image, GUIStyle style, params GUILayoutOption[] options)
	{
		BeginVertical(GUIContent.Temp(image), style, options);
	}

	public static void BeginVertical(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
	{
		GUILayoutGroup gUILayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
		gUILayoutGroup.isVertical = true;
		if (style != GUIStyle.none || content != GUIContent.none)
		{
			GUI.Box(gUILayoutGroup.rect, content, style);
		}
	}

	public static void EndVertical()
	{
		GUILayoutUtility.EndLayoutGroup();
	}

	public static void BeginArea(Rect screenRect)
	{
		BeginArea(screenRect, GUIContent.none, GUIStyle.none);
	}

	public static void BeginArea(Rect screenRect, string text)
	{
		BeginArea(screenRect, GUIContent.Temp(text), GUIStyle.none);
	}

	public static void BeginArea(Rect screenRect, Texture image)
	{
		BeginArea(screenRect, GUIContent.Temp(image), GUIStyle.none);
	}

	public static void BeginArea(Rect screenRect, GUIContent content)
	{
		BeginArea(screenRect, content, GUIStyle.none);
	}

	public static void BeginArea(Rect screenRect, GUIStyle style)
	{
		BeginArea(screenRect, GUIContent.none, style);
	}

	public static void BeginArea(Rect screenRect, string text, GUIStyle style)
	{
		BeginArea(screenRect, GUIContent.Temp(text), style);
	}

	public static void BeginArea(Rect screenRect, Texture image, GUIStyle style)
	{
		BeginArea(screenRect, GUIContent.Temp(image), style);
	}

	public static void BeginArea(Rect screenRect, GUIContent content, GUIStyle style)
	{
		GUIUtility.CheckOnGUI();
		GUILayoutGroup gUILayoutGroup = GUILayoutUtility.BeginLayoutArea(style, typeof(GUILayoutGroup));
		if (Event.current.type == EventType.Layout)
		{
			gUILayoutGroup.resetCoords = true;
			gUILayoutGroup.minWidth = (gUILayoutGroup.maxWidth = screenRect.width);
			gUILayoutGroup.minHeight = (gUILayoutGroup.maxHeight = screenRect.height);
			gUILayoutGroup.rect = Rect.MinMaxRect(screenRect.xMin, screenRect.yMin, gUILayoutGroup.rect.xMax, gUILayoutGroup.rect.yMax);
		}
		GUI.BeginGroup(gUILayoutGroup.rect, content, style);
	}

	public static void EndArea()
	{
		GUIUtility.CheckOnGUI();
		GUILayoutUtility.EndLayoutArea();
		if (Event.current.type != EventType.Used)
		{
			GUI.EndGroup();
		}
	}

	public static Vector2 BeginScrollView(Vector2 scrollPosition, params GUILayoutOption[] options)
	{
		return BeginScrollView(scrollPosition, alwaysShowHorizontal: false, alwaysShowVertical: false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView, options);
	}

	public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, params GUILayoutOption[] options)
	{
		return BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView, options);
	}

	public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
	{
		return BeginScrollView(scrollPosition, alwaysShowHorizontal: false, alwaysShowVertical: false, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView, options);
	}

	public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style)
	{
		GUILayoutOption[] options = null;
		return BeginScrollView(scrollPosition, style, options);
	}

	public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options)
	{
		string name = style.name;
		GUIStyle gUIStyle = GUI.skin.FindStyle(name + "VerticalScrollbar");
		if (gUIStyle == null)
		{
			gUIStyle = GUI.skin.verticalScrollbar;
		}
		GUIStyle gUIStyle2 = GUI.skin.FindStyle(name + "HorizontalScrollbar");
		if (gUIStyle2 == null)
		{
			gUIStyle2 = GUI.skin.horizontalScrollbar;
		}
		return BeginScrollView(scrollPosition, alwaysShowHorizontal: false, alwaysShowVertical: false, gUIStyle2, gUIStyle, style, options);
	}

	public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
	{
		return BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView, options);
	}

	public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background, params GUILayoutOption[] options)
	{
		GUIUtility.CheckOnGUI();
		GUIScrollGroup gUIScrollGroup = (GUIScrollGroup)GUILayoutUtility.BeginLayoutGroup(background, null, typeof(GUIScrollGroup));
		EventType type = Event.current.type;
		EventType eventType = type;
		if (eventType == EventType.Layout)
		{
			gUIScrollGroup.resetCoords = true;
			gUIScrollGroup.isVertical = true;
			gUIScrollGroup.stretchWidth = 1;
			gUIScrollGroup.stretchHeight = 1;
			gUIScrollGroup.verticalScrollbar = verticalScrollbar;
			gUIScrollGroup.horizontalScrollbar = horizontalScrollbar;
			gUIScrollGroup.needsVerticalScrollbar = alwaysShowVertical;
			gUIScrollGroup.needsHorizontalScrollbar = alwaysShowHorizontal;
			gUIScrollGroup.ApplyOptions(options);
		}
		return GUI.BeginScrollView(gUIScrollGroup.rect, scrollPosition, new Rect(0f, 0f, gUIScrollGroup.clientWidth, gUIScrollGroup.clientHeight), alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
	}

	public static void EndScrollView()
	{
		EndScrollView(handleScrollWheel: true);
	}

	internal static void EndScrollView(bool handleScrollWheel)
	{
		GUILayoutUtility.EndLayoutGroup();
		GUI.EndScrollView(handleScrollWheel);
	}

	public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, string text, params GUILayoutOption[] options)
	{
		return DoWindow(id, screenRect, func, GUIContent.Temp(text), GUI.skin.window, options);
	}

	public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, Texture image, params GUILayoutOption[] options)
	{
		return DoWindow(id, screenRect, func, GUIContent.Temp(image), GUI.skin.window, options);
	}

	public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, params GUILayoutOption[] options)
	{
		return DoWindow(id, screenRect, func, content, GUI.skin.window, options);
	}

	public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, string text, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoWindow(id, screenRect, func, GUIContent.Temp(text), style, options);
	}

	public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, Texture image, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoWindow(id, screenRect, func, GUIContent.Temp(image), style, options);
	}

	public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoWindow(id, screenRect, func, content, style, options);
	}

	private static Rect DoWindow(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUILayoutOption[] options)
	{
		GUIUtility.CheckOnGUI();
		LayoutedWindow layoutedWindow = new LayoutedWindow(func, screenRect, content, options, style);
		return GUI.Window(id, screenRect, layoutedWindow.DoWindow, content, style);
	}

	public static GUILayoutOption Width(float width)
	{
		return new GUILayoutOption(GUILayoutOption.Type.fixedWidth, width);
	}

	public static GUILayoutOption MinWidth(float minWidth)
	{
		return new GUILayoutOption(GUILayoutOption.Type.minWidth, minWidth);
	}

	public static GUILayoutOption MaxWidth(float maxWidth)
	{
		return new GUILayoutOption(GUILayoutOption.Type.maxWidth, maxWidth);
	}

	public static GUILayoutOption Height(float height)
	{
		return new GUILayoutOption(GUILayoutOption.Type.fixedHeight, height);
	}

	public static GUILayoutOption MinHeight(float minHeight)
	{
		return new GUILayoutOption(GUILayoutOption.Type.minHeight, minHeight);
	}

	public static GUILayoutOption MaxHeight(float maxHeight)
	{
		return new GUILayoutOption(GUILayoutOption.Type.maxHeight, maxHeight);
	}

	public static GUILayoutOption ExpandWidth(bool expand)
	{
		return new GUILayoutOption(GUILayoutOption.Type.stretchWidth, expand ? 1 : 0);
	}

	public static GUILayoutOption ExpandHeight(bool expand)
	{
		return new GUILayoutOption(GUILayoutOption.Type.stretchHeight, expand ? 1 : 0);
	}
}
public sealed class GUILayoutOption
{
	internal enum Type
	{
		fixedWidth,
		fixedHeight,
		minWidth,
		maxWidth,
		minHeight,
		maxHeight,
		stretchWidth,
		stretchHeight,
		alignStart,
		alignMiddle,
		alignEnd,
		alignJustify,
		equalSize,
		spacing
	}

	internal Type type;

	internal object value;

	internal GUILayoutOption(Type type, object value)
	{
		this.type = type;
		this.value = value;
	}
}
[NativeHeader("Modules/IMGUI/GUILayoutUtility.bindings.h")]
public class GUILayoutUtility
{
	internal readonly struct LayoutCacheState
	{
		public readonly int id;

		public readonly GUILayoutGroup topLevel;

		public readonly GenericStack layoutGroups;

		public readonly GUILayoutGroup windows;

		public LayoutCacheState(LayoutCache cache)
		{
			id = cache.id;
			topLevel = cache.topLevel;
			layoutGroups = cache.layoutGroups;
			windows = cache.windows;
		}
	}

	[DebuggerDisplay("id={id}, groups={layoutGroups.Count}")]
	internal sealed class LayoutCache
	{
		internal GUILayoutGroup topLevel = new GUILayoutGroup();

		internal GenericStack layoutGroups = new GenericStack();

		internal GUILayoutGroup windows = new GUILayoutGroup();

		internal int id { get; private set; }

		public LayoutCacheState State => new LayoutCacheState(this);

		internal LayoutCache(int instanceID = -1)
		{
			id = instanceID;
			layoutGroups.Push(topLevel);
		}

		internal void CopyState(LayoutCacheState other)
		{
			id = other.id;
			topLevel = other.topLevel;
			layoutGroups = other.layoutGroups;
			windows = other.windows;
		}

		public void ResetCursor()
		{
			windows.ResetCursor();
			topLevel.ResetCursor();
			foreach (object layoutGroup in layoutGroups)
			{
				((GUILayoutGroup)layoutGroup).ResetCursor();
			}
		}
	}

	private static readonly Dictionary<int, LayoutCache> s_StoredLayouts = new Dictionary<int, LayoutCache>();

	private static readonly Dictionary<int, LayoutCache> s_StoredWindows = new Dictionary<int, LayoutCache>();

	internal static LayoutCache current = new LayoutCache();

	internal static readonly Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

	private static GUIStyle s_SpaceStyle;

	internal static int unbalancedgroupscount { get; set; }

	internal static GUILayoutGroup topLevel => current.topLevel;

	internal static GUIStyle spaceStyle
	{
		get
		{
			if (s_SpaceStyle == null)
			{
				s_SpaceStyle = new GUIStyle();
			}
			s_SpaceStyle.stretchWidth = false;
			return s_SpaceStyle;
		}
	}

	private static Rect Internal_GetWindowRect(int windowID)
	{
		Internal_GetWindowRect_Injected(windowID, out var ret);
		return ret;
	}

	private static void Internal_MoveWindow(int windowID, Rect r)
	{
		Internal_MoveWindow_Injected(windowID, ref r);
	}

	internal static Rect GetWindowsBounds()
	{
		GetWindowsBounds_Injected(out var ret);
		return ret;
	}

	internal static void CleanupRoots()
	{
		s_SpaceStyle = null;
		s_StoredLayouts.Clear();
		s_StoredWindows.Clear();
		current = new LayoutCache();
	}

	internal static LayoutCache GetLayoutCache(int instanceID, bool isWindow)
	{
		Dictionary<int, LayoutCache> dictionary = (isWindow ? s_StoredWindows : s_StoredLayouts);
		dictionary.TryGetValue(instanceID, out var value);
		return value;
	}

	internal static LayoutCache SelectIDList(int instanceID, bool isWindow)
	{
		Dictionary<int, LayoutCache> dictionary = (isWindow ? s_StoredWindows : s_StoredLayouts);
		LayoutCache layoutCache = GetLayoutCache(instanceID, isWindow);
		if (layoutCache == null)
		{
			layoutCache = (dictionary[instanceID] = new LayoutCache(instanceID));
		}
		current.topLevel = layoutCache.topLevel;
		current.layoutGroups = layoutCache.layoutGroups;
		current.windows = layoutCache.windows;
		return layoutCache;
	}

	internal static void RemoveSelectedIdList(int instanceID, bool isWindow)
	{
		Dictionary<int, LayoutCache> dictionary = (isWindow ? s_StoredWindows : s_StoredLayouts);
		if (dictionary.ContainsKey(instanceID))
		{
			dictionary.Remove(instanceID);
		}
	}

	internal static void Begin(int instanceID)
	{
		LayoutCache layoutCache = SelectIDList(instanceID, isWindow: false);
		if (Event.current.type == EventType.Layout)
		{
			current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
			current.layoutGroups.Clear();
			current.layoutGroups.Push(current.topLevel);
			current.windows = (layoutCache.windows = new GUILayoutGroup());
		}
		else
		{
			current.topLevel = layoutCache.topLevel;
			current.layoutGroups = layoutCache.layoutGroups;
			current.windows = layoutCache.windows;
		}
	}

	internal static void BeginContainer(LayoutCache cache)
	{
		if (Event.current.type == EventType.Layout)
		{
			cache.topLevel = new GUILayoutGroup();
			cache.layoutGroups.Clear();
			cache.layoutGroups.Push(cache.topLevel);
			cache.windows = new GUILayoutGroup();
		}
		current.topLevel = cache.topLevel;
		current.layoutGroups = cache.layoutGroups;
		current.windows = cache.windows;
	}

	internal static void BeginWindow(int windowID, GUIStyle style, GUILayoutOption[] options)
	{
		LayoutCache layoutCache = SelectIDList(windowID, isWindow: true);
		if (Event.current.type == EventType.Layout)
		{
			current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
			current.topLevel.style = style;
			current.topLevel.windowID = windowID;
			if (options != null)
			{
				current.topLevel.ApplyOptions(options);
			}
			current.layoutGroups.Clear();
			current.layoutGroups.Push(current.topLevel);
			current.windows = (layoutCache.windows = new GUILayoutGroup());
		}
		else
		{
			current.topLevel = layoutCache.topLevel;
			current.layoutGroups = layoutCache.layoutGroups;
			current.windows = layoutCache.windows;
		}
	}

	[Obsolete("BeginGroup has no effect and will be removed", false)]
	public static void BeginGroup(string GroupName)
	{
	}

	[Obsolete("EndGroup has no effect and will be removed", false)]
	public static void EndGroup(string groupName)
	{
	}

	internal static void Layout()
	{
		if (current.topLevel.windowID == -1)
		{
			current.topLevel.CalcWidth();
			current.topLevel.SetHorizontal(0f, Mathf.Min((float)Screen.width / GUIUtility.pixelsPerPoint, current.topLevel.maxWidth));
			current.topLevel.CalcHeight();
			current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height / GUIUtility.pixelsPerPoint, current.topLevel.maxHeight));
			LayoutFreeGroup(current.windows);
		}
		else
		{
			LayoutSingleGroup(current.topLevel);
			LayoutFreeGroup(current.windows);
		}
	}

	internal static void LayoutFromEditorWindow()
	{
		if (current.topLevel != null)
		{
			current.topLevel.CalcWidth();
			current.topLevel.SetHorizontal(0f, (float)Screen.width / GUIUtility.pixelsPerPoint);
			current.topLevel.CalcHeight();
			current.topLevel.SetVertical(0f, (float)Screen.height / GUIUtility.pixelsPerPoint);
			LayoutFreeGroup(current.windows);
		}
		else
		{
			Debug.LogError("GUILayout state invalid. Verify that all layout begin/end calls match.");
		}
	}

	internal static void LayoutFromContainer(float w, float h)
	{
		if (current.topLevel != null)
		{
			current.topLevel.CalcWidth();
			current.topLevel.SetHorizontal(0f, w);
			current.topLevel.CalcHeight();
			current.topLevel.SetVertical(0f, h);
			LayoutFreeGroup(current.windows);
		}
		else
		{
			Debug.LogError("GUILayout state invalid. Verify that all layout begin/end calls match.");
		}
	}

	internal static float LayoutFromInspector(float width)
	{
		if (current.topLevel != null && current.topLevel.windowID == -1)
		{
			current.topLevel.CalcWidth();
			current.topLevel.SetHorizontal(0f, width);
			current.topLevel.CalcHeight();
			current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height / GUIUtility.pixelsPerPoint, current.topLevel.maxHeight));
			float minHeight = current.topLevel.minHeight;
			LayoutFreeGroup(current.windows);
			return minHeight;
		}
		if (current.topLevel != null)
		{
			LayoutSingleGroup(current.topLevel);
		}
		return 0f;
	}

	internal static void LayoutFreeGroup(GUILayoutGroup toplevel)
	{
		foreach (GUILayoutGroup entry in toplevel.entries)
		{
			LayoutSingleGroup(entry);
		}
		toplevel.ResetCursor();
	}

	private static void LayoutSingleGroup(GUILayoutGroup i)
	{
		if (!i.isWindow)
		{
			float minWidth = i.minWidth;
			float maxWidth = i.maxWidth;
			i.CalcWidth();
			i.SetHorizontal(i.rect.x, Mathf.Clamp(i.maxWidth, minWidth, maxWidth));
			float minHeight = i.minHeight;
			float maxHeight = i.maxHeight;
			i.CalcHeight();
			i.SetVertical(i.rect.y, Mathf.Clamp(i.maxHeight, minHeight, maxHeight));
		}
		else
		{
			i.CalcWidth();
			Rect rect = Internal_GetWindowRect(i.windowID);
			i.SetHorizontal(rect.x, Mathf.Clamp(rect.width, i.minWidth, i.maxWidth));
			i.CalcHeight();
			i.SetVertical(rect.y, Mathf.Clamp(rect.height, i.minHeight, i.maxHeight));
			Internal_MoveWindow(i.windowID, i.rect);
		}
	}

	[SecuritySafeCritical]
	private static GUILayoutGroup CreateGUILayoutGroupInstanceOfType(Type LayoutType)
	{
		if (!typeof(GUILayoutGroup).IsAssignableFrom(LayoutType))
		{
			throw new ArgumentException("LayoutType needs to be of type GUILayoutGroup", "LayoutType");
		}
		return (GUILayoutGroup)Activator.CreateInstance(LayoutType);
	}

	internal static GUILayoutGroup BeginLayoutGroup(GUIStyle style, GUILayoutOption[] options, Type layoutType)
	{
		unbalancedgroupscount++;
		EventType type = Event.current.type;
		EventType eventType = type;
		GUILayoutGroup gUILayoutGroup;
		if (eventType == EventType.Layout || eventType == EventType.Used)
		{
			gUILayoutGroup = CreateGUILayoutGroupInstanceOfType(layoutType);
			gUILayoutGroup.style = style;
			if (options != null)
			{
				gUILayoutGroup.ApplyOptions(options);
			}
			current.topLevel.Add(gUILayoutGroup);
		}
		else
		{
			gUILayoutGroup = current.topLevel.GetNext() as GUILayoutGroup;
			if (gUILayoutGroup == null)
			{
				throw new ExitGUIException("GUILayout: Mismatched LayoutGroup." + Event.current.type);
			}
			gUILayoutGroup.ResetCursor();
		}
		current.layoutGroups.Push(gUILayoutGroup);
		current.topLevel = gUILayoutGroup;
		return gUILayoutGroup;
	}

	internal static void EndLayoutGroup()
	{
		unbalancedgroupscount--;
		if (current.layoutGroups.Count == 0)
		{
			Debug.LogError("EndLayoutGroup: BeginLayoutGroup must be called first.");
			return;
		}
		current.layoutGroups.Pop();
		if (0 < current.layoutGroups.Count)
		{
			current.topLevel = (GUILayoutGroup)current.layoutGroups.Peek();
		}
		else
		{
			current.topLevel = new GUILayoutGroup();
		}
	}

	internal static GUILayoutGroup BeginLayoutArea(GUIStyle style, Type layoutType)
	{
		unbalancedgroupscount++;
		EventType type = Event.current.type;
		EventType eventType = type;
		GUILayoutGroup gUILayoutGroup;
		if (eventType == EventType.Layout || eventType == EventType.Used)
		{
			gUILayoutGroup = CreateGUILayoutGroupInstanceOfType(layoutType);
			gUILayoutGroup.style = style;
			current.windows.Add(gUILayoutGroup);
		}
		else
		{
			gUILayoutGroup = current.windows.GetNext() as GUILayoutGroup;
			if (gUILayoutGroup == null)
			{
				throw new ExitGUIException("GUILayout: Mismatched LayoutGroup." + Event.current.type);
			}
			gUILayoutGroup.ResetCursor();
		}
		current.layoutGroups.Push(gUILayoutGroup);
		current.topLevel = gUILayoutGroup;
		return gUILayoutGroup;
	}

	internal static void EndLayoutArea()
	{
		unbalancedgroupscount--;
		current.layoutGroups.Pop();
		current.topLevel = (GUILayoutGroup)current.layoutGroups.Peek();
	}

	internal static GUILayoutGroup DoBeginLayoutArea(GUIStyle style, Type layoutType)
	{
		return BeginLayoutArea(style, layoutType);
	}

	public static Rect GetRect(GUIContent content, GUIStyle style)
	{
		return DoGetRect(content, style, null);
	}

	public static Rect GetRect(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoGetRect(content, style, options);
	}

	private static Rect DoGetRect(GUIContent content, GUIStyle style, GUILayoutOption[] options)
	{
		GUIUtility.CheckOnGUI();
		switch (Event.current.type)
		{
		case EventType.Layout:
			if (style.isHeightDependantOnWidth)
			{
				current.topLevel.Add(new GUIWordWrapSizer(style, content, options));
			}
			else
			{
				Vector2 constraints = new Vector2(0f, 0f);
				if (options != null)
				{
					foreach (GUILayoutOption gUILayoutOption in options)
					{
						switch (gUILayoutOption.type)
						{
						case GUILayoutOption.Type.maxHeight:
							constraints.y = (float)gUILayoutOption.value;
							break;
						case GUILayoutOption.Type.maxWidth:
							constraints.x = (float)gUILayoutOption.value;
							break;
						}
					}
				}
				Vector2 vector = style.CalcSizeWithConstraints(content, constraints);
				vector.x = Mathf.Ceil(vector.x);
				vector.y = Mathf.Ceil(vector.y);
				current.topLevel.Add(new GUILayoutEntry(vector.x, vector.x, vector.y, vector.y, style, options));
			}
			return kDummyRect;
		case EventType.Used:
			return kDummyRect;
		default:
		{
			GUILayoutEntry next = current.topLevel.GetNext();
			return next.rect;
		}
		}
	}

	public static Rect GetRect(float width, float height)
	{
		return DoGetRect(width, width, height, height, GUIStyle.none, null);
	}

	public static Rect GetRect(float width, float height, GUIStyle style)
	{
		return DoGetRect(width, width, height, height, style, null);
	}

	public static Rect GetRect(float width, float height, params GUILayoutOption[] options)
	{
		return DoGetRect(width, width, height, height, GUIStyle.none, options);
	}

	public static Rect GetRect(float width, float height, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoGetRect(width, width, height, height, style, options);
	}

	public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight)
	{
		return DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, null);
	}

	public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style)
	{
		return DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, null);
	}

	public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, params GUILayoutOption[] options)
	{
		return DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, options);
	}

	public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, options);
	}

	private static Rect DoGetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, GUILayoutOption[] options)
	{
		switch (Event.current.type)
		{
		case EventType.Layout:
			current.topLevel.Add(new GUILayoutEntry(minWidth, maxWidth, minHeight, maxHeight, style, options));
			return kDummyRect;
		case EventType.Used:
			return kDummyRect;
		default:
			return current.topLevel.GetNext().rect;
		}
	}

	public static Rect GetLastRect()
	{
		EventType type = Event.current.type;
		EventType eventType = type;
		if (eventType == EventType.Layout || eventType == EventType.Used)
		{
			return kDummyRect;
		}
		return current.topLevel.GetLast();
	}

	public static Rect GetAspectRect(float aspect)
	{
		return DoGetAspectRect(aspect, null);
	}

	public static Rect GetAspectRect(float aspect, GUIStyle style)
	{
		return DoGetAspectRect(aspect, null);
	}

	public static Rect GetAspectRect(float aspect, params GUILayoutOption[] options)
	{
		return DoGetAspectRect(aspect, options);
	}

	public static Rect GetAspectRect(float aspect, GUIStyle style, params GUILayoutOption[] options)
	{
		return DoGetAspectRect(aspect, options);
	}

	private static Rect DoGetAspectRect(float aspect, GUILayoutOption[] options)
	{
		switch (Event.current.type)
		{
		case EventType.Layout:
			current.topLevel.Add(new GUIAspectSizer(aspect, options));
			return kDummyRect;
		case EventType.Used:
			return kDummyRect;
		default:
			return current.topLevel.GetNext().rect;
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_GetWindowRect_Injected(int windowID, out Rect ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_MoveWindow_Injected(int windowID, ref Rect r);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void GetWindowsBounds_Injected(out Rect ret);
}
[Serializable]
[NativeHeader("Modules/IMGUI/GUISkin.bindings.h")]
public sealed class GUISettings
{
	[SerializeField]
	private bool m_DoubleClickSelectsWord = true;

	[SerializeField]
	private bool m_TripleClickSelectsLine = true;

	[SerializeField]
	private Color m_CursorColor = Color.white;

	[SerializeField]
	private float m_CursorFlashSpeed = -1f;

	[SerializeField]
	private Color m_SelectionColor = new Color(0.5f, 0.5f, 1f);

	public bool doubleClickSelectsWord
	{
		get
		{
			return m_DoubleClickSelectsWord;
		}
		set
		{
			m_DoubleClickSelectsWord = value;
		}
	}

	public bool tripleClickSelectsLine
	{
		get
		{
			return m_TripleClickSelectsLine;
		}
		set
		{
			m_TripleClickSelectsLine = value;
		}
	}

	public Color cursorColor
	{
		get
		{
			return m_CursorColor;
		}
		set
		{
			m_CursorColor = value;
		}
	}

	public float cursorFlashSpeed
	{
		get
		{
			if (m_CursorFlashSpeed >= 0f)
			{
				return m_CursorFlashSpeed;
			}
			return Internal_GetCursorFlashSpeed();
		}
		set
		{
			m_CursorFlashSpeed = value;
		}
	}

	public Color selectionColor
	{
		get
		{
			return m_SelectionColor;
		}
		set
		{
			m_SelectionColor = value;
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern float Internal_GetCursorFlashSpeed();
}
internal enum PlatformSelection
{
	Native,
	Mac,
	Windows
}
[Serializable]
[AssetFileNameExtension("guiskin", new string[] { })]
[RequiredByNativeCode]
[ExecuteInEditMode]
public sealed class GUISkin : ScriptableObject
{
	internal delegate void SkinChangedDelegate();

	[SerializeField]
	private Font m_Font;

	[SerializeField]
	private GUIStyle m_box;

	[SerializeField]
	private GUIStyle m_button;

	[SerializeField]
	private GUIStyle m_toggle;

	[SerializeField]
	private GUIStyle m_label;

	[SerializeField]
	private GUIStyle m_textField;

	[SerializeField]
	private GUIStyle m_textArea;

	[SerializeField]
	private GUIStyle m_window;

	[SerializeField]
	private GUIStyle m_horizontalSlider;

	[SerializeField]
	private GUIStyle m_horizontalSliderThumb;

	[NonSerialized]
	private GUIStyle m_horizontalSliderThumbExtent;

	[SerializeField]
	private GUIStyle m_verticalSlider;

	[SerializeField]
	private GUIStyle m_verticalSliderThumb;

	[NonSerialized]
	private GUIStyle m_verticalSliderThumbExtent;

	[NonSerialized]
	private GUIStyle m_SliderMixed;

	[SerializeField]
	private GUIStyle m_horizontalScrollbar;

	[SerializeField]
	private GUIStyle m_horizontalScrollbarThumb;

	[SerializeField]
	private GUIStyle m_horizontalScrollbarLeftButton;

	[SerializeField]
	private GUIStyle m_horizontalScrollbarRightButton;

	[SerializeField]
	private GUIStyle m_verticalScrollbar;

	[SerializeField]
	private GUIStyle m_verticalScrollbarThumb;

	[SerializeField]
	private GUIStyle m_verticalScrollbarUpButton;

	[SerializeField]
	private GUIStyle m_verticalScrollbarDownButton;

	[SerializeField]
	private GUIStyle m_ScrollView;

	[SerializeField]
	internal GUIStyle[] m_CustomStyles;

	[SerializeField]
	private GUISettings m_Settings = new GUISettings();

	internal static GUIStyle ms_Error;

	private Dictionary<string, GUIStyle> m_Styles = null;

	internal static SkinChangedDelegate m_SkinChanged;

	internal static GUISkin current;

	public Font font
	{
		get
		{
			return m_Font;
		}
		set
		{
			m_Font = value;
			if (current == this)
			{
				GUIStyle.SetDefaultFont(m_Font);
			}
			Apply();
		}
	}

	public GUIStyle box
	{
		get
		{
			return m_box;
		}
		set
		{
			m_box = value;
			Apply();
		}
	}

	public GUIStyle label
	{
		get
		{
			return m_label;
		}
		set
		{
			m_label = value;
			Apply();
		}
	}

	public GUIStyle textField
	{
		get
		{
			return m_textField;
		}
		set
		{
			m_textField = value;
			Apply();
		}
	}

	public GUIStyle textArea
	{
		get
		{
			return m_textArea;
		}
		set
		{
			m_textArea = value;
			Apply();
		}
	}

	public GUIStyle button
	{
		get
		{
			return m_button;
		}
		set
		{
			m_button = value;
			Apply();
		}
	}

	public GUIStyle toggle
	{
		get
		{
			return m_toggle;
		}
		set
		{
			m_toggle = value;
			Apply();
		}
	}

	public GUIStyle window
	{
		get
		{
			return m_window;
		}
		set
		{
			m_window = value;
			Apply();
		}
	}

	public GUIStyle horizontalSlider
	{
		get
		{
			return m_horizontalSlider;
		}
		set
		{
			m_horizontalSlider = value;
			Apply();
		}
	}

	public GUIStyle horizontalSliderThumb
	{
		get
		{
			return m_horizontalSliderThumb;
		}
		set
		{
			m_horizontalSliderThumb = value;
			Apply();
		}
	}

	internal GUIStyle horizontalSliderThumbExtent
	{
		get
		{
			return m_horizontalSliderThumbExtent;
		}
		set
		{
			m_horizontalSliderThumbExtent = value;
			Apply();
		}
	}

	internal GUIStyle sliderMixed
	{
		get
		{
			return m_SliderMixed;
		}
		set
		{
			m_SliderMixed = value;
			Apply();
		}
	}

	public GUIStyle verticalSlider
	{
		get
		{
			return m_verticalSlider;
		}
		set
		{
			m_verticalSlider = value;
			Apply();
		}
	}

	public GUIStyle verticalSliderThumb
	{
		get
		{
			return m_verticalSliderThumb;
		}
		set
		{
			m_verticalSliderThumb = value;
			Apply();
		}
	}

	internal GUIStyle verticalSliderThumbExtent
	{
		get
		{
			return m_verticalSliderThumbExtent;
		}
		set
		{
			m_verticalSliderThumbExtent = value;
			Apply();
		}
	}

	public GUIStyle horizontalScrollbar
	{
		get
		{
			return m_horizontalScrollbar;
		}
		set
		{
			m_horizontalScrollbar = value;
			Apply();
		}
	}

	public GUIStyle horizontalScrollbarThumb
	{
		get
		{
			return m_horizontalScrollbarThumb;
		}
		set
		{
			m_horizontalScrollbarThumb = value;
			Apply();
		}
	}

	public GUIStyle horizontalScrollbarLeftButton
	{
		get
		{
			return m_horizontalScrollbarLeftButton;
		}
		set
		{
			m_horizontalScrollbarLeftButton = value;
			Apply();
		}
	}

	public GUIStyle horizontalScrollbarRightButton
	{
		get
		{
			return m_horizontalScrollbarRightButton;
		}
		set
		{
			m_horizontalScrollbarRightButton = value;
			Apply();
		}
	}

	public GUIStyle verticalScrollbar
	{
		get
		{
			return m_verticalScrollbar;
		}
		set
		{
			m_verticalScrollbar = value;
			Apply();
		}
	}

	public GUIStyle verticalScrollbarThumb
	{
		get
		{
			return m_verticalScrollbarThumb;
		}
		set
		{
			m_verticalScrollbarThumb = value;
			Apply();
		}
	}

	public GUIStyle verticalScrollbarUpButton
	{
		get
		{
			return m_verticalScrollbarUpButton;
		}
		set
		{
			m_verticalScrollbarUpButton = value;
			Apply();
		}
	}

	public GUIStyle verticalScrollbarDownButton
	{
		get
		{
			return m_verticalScrollbarDownButton;
		}
		set
		{
			m_verticalScrollbarDownButton = value;
			Apply();
		}
	}

	public GUIStyle scrollView
	{
		get
		{
			return m_ScrollView;
		}
		set
		{
			m_ScrollView = value;
			Apply();
		}
	}

	public GUIStyle[] customStyles
	{
		get
		{
			return m_CustomStyles;
		}
		set
		{
			m_CustomStyles = value;
			Apply();
		}
	}

	public GUISettings settings => m_Settings;

	internal static GUIStyle error
	{
		get
		{
			if (ms_Error == null)
			{
				ms_Error = new GUIStyle();
				ms_Error.name = "StyleNotFoundError";
			}
			return ms_Error;
		}
	}

	public GUISkin()
	{
		m_CustomStyles = new GUIStyle[1];
	}

	internal void OnEnable()
	{
		Apply();
	}

	internal static void CleanupRoots()
	{
		current = null;
		ms_Error = null;
	}

	internal void Apply()
	{
		if (m_CustomStyles == null)
		{
			Debug.Log("custom styles is null");
		}
		BuildStyleCache();
	}

	private void BuildStyleCache()
	{
		if (m_box == null)
		{
			m_box = new GUIStyle();
		}
		if (m_button == null)
		{
			m_button = new GUIStyle();
		}
		if (m_toggle == null)
		{
			m_toggle = new GUIStyle();
		}
		if (m_label == null)
		{
			m_label = new GUIStyle();
		}
		if (m_window == null)
		{
			m_window = new GUIStyle();
		}
		if (m_textField == null)
		{
			m_textField = new GUIStyle();
		}
		if (m_textArea == null)
		{
			m_textArea = new GUIStyle();
		}
		if (m_horizontalSlider == null)
		{
			m_horizontalSlider = new GUIStyle();
		}
		if (m_horizontalSliderThumb == null)
		{
			m_horizontalSliderThumb = new GUIStyle();
		}
		if (m_verticalSlider == null)
		{
			m_verticalSlider = new GUIStyle();
		}
		if (m_verticalSliderThumb == null)
		{
			m_verticalSliderThumb = new GUIStyle();
		}
		if (m_horizontalScrollbar == null)
		{
			m_horizontalScrollbar = new GUIStyle();
		}
		if (m_horizontalScrollbarThumb == null)
		{
			m_horizontalScrollbarThumb = new GUIStyle();
		}
		if (m_horizontalScrollbarLeftButton == null)
		{
			m_horizontalScrollbarLeftButton = new GUIStyle();
		}
		if (m_horizontalScrollbarRightButton == null)
		{
			m_horizontalScrollbarRightButton = new GUIStyle();
		}
		if (m_verticalScrollbar == null)
		{
			m_verticalScrollbar = new GUIStyle();
		}
		if (m_verticalScrollbarThumb == null)
		{
			m_verticalScrollbarThumb = new GUIStyle();
		}
		if (m_verticalScrollbarUpButton == null)
		{
			m_verticalScrollbarUpButton = new GUIStyle();
		}
		if (m_verticalScrollbarDownButton == null)
		{
			m_verticalScrollbarDownButton = new GUIStyle();
		}
		if (m_ScrollView == null)
		{
			m_ScrollView = new GUIStyle();
		}
		m_Styles = new Dictionary<string, GUIStyle>(StringComparer.OrdinalIgnoreCase);
		m_Styles["box"] = m_box;
		m_box.name = "box";
		m_Styles["button"] = m_button;
		m_button.name = "button";
		m_Styles["toggle"] = m_toggle;
		m_toggle.name = "toggle";
		m_Styles["label"] = m_label;
		m_label.name = "label";
		m_Styles["window"] = m_window;
		m_window.name = "window";
		m_Styles["textfield"] = m_textField;
		m_textField.name = "textfield";
		m_Styles["textarea"] = m_textArea;
		m_textArea.name = "textarea";
		m_Styles["horizontalslider"] = m_horizontalSlider;
		m_horizontalSlider.name = "horizontalslider";
		m_Styles["horizontalsliderthumb"] = m_horizontalSliderThumb;
		m_horizontalSliderThumb.name = "horizontalsliderthumb";
		m_Styles["verticalslider"] = m_verticalSlider;
		m_verticalSlider.name = "verticalslider";
		m_Styles["verticalsliderthumb"] = m_verticalSliderThumb;
		m_verticalSliderThumb.name = "verticalsliderthumb";
		m_Styles["horizontalscrollbar"] = m_horizontalScrollbar;
		m_horizontalScrollbar.name = "horizontalscrollbar";
		m_Styles["horizontalscrollbarthumb"] = m_horizontalScrollbarThumb;
		m_horizontalScrollbarThumb.name = "horizontalscrollbarthumb";
		m_Styles["horizontalscrollbarleftbutton"] = m_horizontalScrollbarLeftButton;
		m_horizontalScrollbarLeftButton.name = "horizontalscrollbarleftbutton";
		m_Styles["horizontalscrollbarrightbutton"] = m_horizontalScrollbarRightButton;
		m_horizontalScrollbarRightButton.name = "horizontalscrollbarrightbutton";
		m_Styles["verticalscrollbar"] = m_verticalScrollbar;
		m_verticalScrollbar.name = "verticalscrollbar";
		m_Styles["verticalscrollbarthumb"] = m_verticalScrollbarThumb;
		m_verticalScrollbarThumb.name = "verticalscrollbarthumb";
		m_Styles["verticalscrollbarupbutton"] = m_verticalScrollbarUpButton;
		m_verticalScrollbarUpButton.name = "verticalscrollbarupbutton";
		m_Styles["verticalscrollbardownbutton"] = m_verticalScrollbarDownButton;
		m_verticalScrollbarDownButton.name = "verticalscrollbardownbutton";
		m_Styles["scrollview"] = m_ScrollView;
		m_ScrollView.name = "scrollview";
		if (m_CustomStyles != null)
		{
			for (int i = 0; i < m_CustomStyles.Length; i++)
			{
				if (m_CustomStyles[i] != null)
				{
					m_Styles[m_CustomStyles[i].name] = m_CustomStyles[i];
				}
			}
		}
		if (!m_Styles.TryGetValue("HorizontalSliderThumbExtent", out m_horizontalSliderThumbExtent))
		{
			m_horizontalSliderThumbExtent = new GUIStyle();
			m_horizontalSliderThumbExtent.name = "horizontalsliderthumbextent";
			m_Styles["HorizontalSliderThumbExtent"] = m_horizontalSliderThumbExtent;
		}
		if (!m_Styles.TryGetValue("SliderMixed", out m_SliderMixed))
		{
			m_SliderMixed = new GUIStyle();
			m_SliderMixed.name = "SliderMixed";
			m_Styles["SliderMixed"] = m_SliderMixed;
		}
		if (!m_Styles.TryGetValue("VerticalSliderThumbExtent", out m_verticalSliderThumbExtent))
		{
			m_verticalSliderThumbExtent = new GUIStyle();
			m_Styles["VerticalSliderThumbExtent"] = m_verticalSliderThumbExtent;
			m_verticalSliderThumbExtent.name = "verticalsliderthumbextent";
		}
		error.stretchHeight = true;
		error.normal.textColor = Color.red;
	}

	public GUIStyle GetStyle(string styleName)
	{
		GUIStyle gUIStyle = FindStyle(styleName);
		if (gUIStyle != null)
		{
			return gUIStyle;
		}
		Debug.LogWarning("Unable to find style '" + styleName + "' in skin '" + base.name + "' " + ((Event.current != null) ? Event.current.type.ToString() : "<called outside OnGUI>"));
		return error;
	}

	public GUIStyle FindStyle(string styleName)
	{
		if (m_Styles == null)
		{
			BuildStyleCache();
		}
		if (m_Styles.TryGetValue(styleName, out var value))
		{
			return value;
		}
		return null;
	}

	internal void MakeCurrent()
	{
		current = this;
		GUIStyle.SetDefaultFont(font);
		if (m_SkinChanged != null)
		{
			m_SkinChanged();
		}
	}

	public IEnumerator GetEnumerator()
	{
		if (m_Styles == null)
		{
			BuildStyleCache();
		}
		return m_Styles.Values.GetEnumerator();
	}
}
internal class GUIStateObjects
{
	private static Dictionary<int, object> s_StateCache = new Dictionary<int, object>();

	[SecuritySafeCritical]
	internal static object GetStateObject(Type t, int controlID)
	{
		if (!s_StateCache.TryGetValue(controlID, out var value) || value.GetType() != t)
		{
			value = Activator.CreateInstance(t);
			s_StateCache[controlID] = value;
		}
		return value;
	}

	internal static object QueryStateObject(Type t, int controlID)
	{
		object obj = s_StateCache[controlID];
		if (t.IsInstanceOfType(obj))
		{
			return obj;
		}
		return null;
	}

	internal static void Tests_ClearObjects()
	{
		s_StateCache.Clear();
	}
}
[Serializable]
[StructLayout(LayoutKind.Sequential)]
[NativeHeader("Modules/IMGUI/GUIStyle.bindings.h")]
public sealed class GUIStyleState
{
	[NonSerialized]
	internal IntPtr m_Ptr;

	private readonly GUIStyle m_SourceStyle;

	[NativeProperty("Background", false, TargetType.Function)]
	public extern Texture2D background
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("textColor", false, TargetType.Field)]
	public Color textColor
	{
		get
		{
			get_textColor_Injected(out var ret);
			return ret;
		}
		set
		{
			set_textColor_Injected(ref value);
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyleState_Bindings::Init", IsThreadSafe = true)]
	private static extern IntPtr Init();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyleState_Bindings::Cleanup", IsThreadSafe = true, HasExplicitThis = true)]
	private extern void Cleanup();

	public GUIStyleState()
	{
		m_Ptr = Init();
	}

	private GUIStyleState(GUIStyle sourceStyle, IntPtr source)
	{
		m_SourceStyle = sourceStyle;
		m_Ptr = source;
	}

	internal static GUIStyleState ProduceGUIStyleStateFromDeserialization(GUIStyle sourceStyle, IntPtr source)
	{
		return new GUIStyleState(sourceStyle, source);
	}

	internal static GUIStyleState GetGUIStyleState(GUIStyle sourceStyle, IntPtr source)
	{
		return new GUIStyleState(sourceStyle, source);
	}

	~GUIStyleState()
	{
		if (m_SourceStyle == null)
		{
			Cleanup();
			m_Ptr = IntPtr.Zero;
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_textColor_Injected(out Color ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_textColor_Injected(ref Color value);
}
[Serializable]
[StructLayout(LayoutKind.Sequential)]
[NativeHeader("IMGUIScriptingClasses.h")]
[RequiredByNativeCode]
[NativeHeader("Modules/IMGUI/GUIStyle.bindings.h")]
public sealed class GUIStyle
{
	[NonSerialized]
	internal IntPtr m_Ptr;

	[NonSerialized]
	private GUIStyleState m_Normal;

	[NonSerialized]
	private GUIStyleState m_Hover;

	[NonSerialized]
	private GUIStyleState m_Active;

	[NonSerialized]
	private GUIStyleState m_Focused;

	[NonSerialized]
	private GUIStyleState m_OnNormal;

	[NonSerialized]
	private GUIStyleState m_OnHover;

	[NonSerialized]
	private GUIStyleState m_OnActive;

	[NonSerialized]
	private GUIStyleState m_OnFocused;

	[NonSerialized]
	private RectOffset m_Border;

	[NonSerialized]
	private RectOffset m_Padding;

	[NonSerialized]
	private RectOffset m_Margin;

	[NonSerialized]
	private RectOffset m_Overflow;

	[NonSerialized]
	private string m_Name;

	internal static bool showKeyboardFocus = true;

	private static GUIStyle s_None;

	[NativeProperty("Name", false, TargetType.Function)]
	internal extern string rawName
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("Font", false, TargetType.Function)]
	public extern Font font
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("m_ImagePosition", false, TargetType.Field)]
	public extern ImagePosition imagePosition
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("m_Alignment", false, TargetType.Field)]
	public extern TextAnchor alignment
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("m_WordWrap", false, TargetType.Field)]
	public extern bool wordWrap
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("m_Clipping", false, TargetType.Field)]
	public extern TextClipping clipping
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("m_ContentOffset", false, TargetType.Field)]
	public Vector2 contentOffset
	{
		get
		{
			get_contentOffset_Injected(out var ret);
			return ret;
		}
		set
		{
			set_contentOffset_Injected(ref value);
		}
	}

	[NativeProperty("m_FixedWidth", false, TargetType.Field)]
	public extern float fixedWidth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("m_FixedHeight", false, TargetType.Field)]
	public extern float fixedHeight
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("m_StretchWidth", false, TargetType.Field)]
	public extern bool stretchWidth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("m_StretchHeight", false, TargetType.Field)]
	public extern bool stretchHeight
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("m_FontSize", false, TargetType.Field)]
	public extern int fontSize
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("m_FontStyle", false, TargetType.Field)]
	public extern FontStyle fontStyle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("m_RichText", false, TargetType.Field)]
	public extern bool richText
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("m_ClipOffset", false, TargetType.Field)]
	[Obsolete("Don't use clipOffset - put things inside BeginGroup instead. This functionality will be removed in a later version.", false)]
	public Vector2 clipOffset
	{
		get
		{
			get_clipOffset_Injected(out var ret);
			return ret;
		}
		set
		{
			set_clipOffset_Injected(ref value);
		}
	}

	[NativeProperty("m_ClipOffset", false, TargetType.Field)]
	internal Vector2 Internal_clipOffset
	{
		get
		{
			get_Internal_clipOffset_Injected(out var ret);
			return ret;
		}
		set
		{
			set_Internal_clipOffset_Injected(ref value);
		}
	}

	public string name
	{
		get
		{
			return m_Name ?? (m_Name = rawName);
		}
		set
		{
			m_Name = value;
			rawName = value;
		}
	}

	public GUIStyleState normal
	{
		get
		{
			return m_Normal ?? (m_Normal = GUIStyleState.GetGUIStyleState(this, GetStyleStatePtr(0)));
		}
		set
		{
			AssignStyleState(0, value.m_Ptr);
		}
	}

	public GUIStyleState hover
	{
		get
		{
			return m_Hover ?? (m_Hover = GUIStyleState.GetGUIStyleState(this, GetStyleStatePtr(1)));
		}
		set
		{
			AssignStyleState(1, value.m_Ptr);
		}
	}

	public GUIStyleState active
	{
		get
		{
			return m_Active ?? (m_Active = GUIStyleState.GetGUIStyleState(this, GetStyleStatePtr(2)));
		}
		set
		{
			AssignStyleState(2, value.m_Ptr);
		}
	}

	public GUIStyleState onNormal
	{
		get
		{
			return m_OnNormal ?? (m_OnNormal = GUIStyleState.GetGUIStyleState(this, GetStyleStatePtr(4)));
		}
		set
		{
			AssignStyleState(4, value.m_Ptr);
		}
	}

	public GUIStyleState onHover
	{
		get
		{
			return m_OnHover ?? (m_OnHover = GUIStyleState.GetGUIStyleState(this, GetStyleStatePtr(5)));
		}
		set
		{
			AssignStyleState(5, value.m_Ptr);
		}
	}

	public GUIStyleState onActive
	{
		get
		{
			return m_OnActive ?? (m_OnActive = GUIStyleState.GetGUIStyleState(this, GetStyleStatePtr(6)));
		}
		set
		{
			AssignStyleState(6, value.m_Ptr);
		}
	}

	public GUIStyleState focused
	{
		get
		{
			return m_Focused ?? (m_Focused = GUIStyleState.GetGUIStyleState(this, GetStyleStatePtr(3)));
		}
		set
		{
			AssignStyleState(3, value.m_Ptr);
		}
	}

	public GUIStyleState onFocused
	{
		get
		{
			return m_OnFocused ?? (m_OnFocused = GUIStyleState.GetGUIStyleState(this, GetStyleStatePtr(7)));
		}
		set
		{
			AssignStyleState(7, value.m_Ptr);
		}
	}

	public RectOffset border
	{
		get
		{
			return m_Border ?? (m_Border = new RectOffset(this, GetRectOffsetPtr(0)));
		}
		set
		{
			AssignRectOffset(0, value.m_Ptr);
		}
	}

	public RectOffset margin
	{
		get
		{
			return m_Margin ?? (m_Margin = new RectOffset(this, GetRectOffsetPtr(1)));
		}
		set
		{
			AssignRectOffset(1, value.m_Ptr);
		}
	}

	public RectOffset padding
	{
		get
		{
			return m_Padding ?? (m_Padding = new RectOffset(this, GetRectOffsetPtr(2)));
		}
		set
		{
			AssignRectOffset(2, value.m_Ptr);
		}
	}

	public RectOffset overflow
	{
		get
		{
			return m_Overflow ?? (m_Overflow = new RectOffset(this, GetRectOffsetPtr(3)));
		}
		set
		{
			AssignRectOffset(3, value.m_Ptr);
		}
	}

	public float lineHeight => Mathf.Round(Internal_GetLineHeight(m_Ptr));

	public static GUIStyle none => s_None ?? (s_None = new GUIStyle());

	public bool isHeightDependantOnWidth => fixedHeight == 0f && wordWrap && imagePosition != ImagePosition.ImageOnly;

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyle_Bindings::Internal_Create", IsThreadSafe = true)]
	private static extern IntPtr Internal_Create(GUIStyle self);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyle_Bindings::Internal_Copy", IsThreadSafe = true)]
	private static extern IntPtr Internal_Copy(GUIStyle self, GUIStyle other);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyle_Bindings::Internal_Destroy", IsThreadSafe = true)]
	private static extern void Internal_Destroy(IntPtr self);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyle_Bindings::GetStyleStatePtr", IsThreadSafe = true, HasExplicitThis = true)]
	private extern IntPtr GetStyleStatePtr(int idx);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyle_Bindings::AssignStyleState", HasExplicitThis = true)]
	private extern void AssignStyleState(int idx, IntPtr srcStyleState);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyle_Bindings::GetRectOffsetPtr", HasExplicitThis = true)]
	private extern IntPtr GetRectOffsetPtr(int idx);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyle_Bindings::AssignRectOffset", HasExplicitThis = true)]
	private extern void AssignRectOffset(int idx, IntPtr srcRectOffset);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetLineHeight")]
	private static extern float Internal_GetLineHeight(IntPtr target);

	[FreeFunction(Name = "GUIStyle_Bindings::Internal_Draw", HasExplicitThis = true)]
	private void Internal_Draw(Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
	{
		Internal_Draw_Injected(ref screenRect, content, isHover, isActive, on, hasKeyboardFocus);
	}

	[FreeFunction(Name = "GUIStyle_Bindings::Internal_Draw2", HasExplicitThis = true)]
	private void Internal_Draw2(Rect position, GUIContent content, int controlID, bool on)
	{
		Internal_Draw2_Injected(ref position, content, controlID, on);
	}

	[FreeFunction(Name = "GUIStyle_Bindings::Internal_DrawCursor", HasExplicitThis = true)]
	private void Internal_DrawCursor(Rect position, GUIContent content, int pos, Color cursorColor)
	{
		Internal_DrawCursor_Injected(ref position, content, pos, ref cursorColor);
	}

	[FreeFunction(Name = "GUIStyle_Bindings::Internal_DrawWithTextSelection", HasExplicitThis = true)]
	private void Internal_DrawWithTextSelection(Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool drawSelectionAsComposition, int cursorFirst, int cursorLast, Color cursorColor, Color selectionColor)
	{
		Internal_DrawWithTextSelection_Injected(ref screenRect, content, isHover, isActive, on, hasKeyboardFocus, drawSelectionAsComposition, cursorFirst, cursorLast, ref cursorColor, ref selectionColor);
	}

	[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorPixelPosition", HasExplicitThis = true)]
	internal Vector2 Internal_GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
	{
		Internal_GetCursorPixelPosition_Injected(ref position, content, cursorStringIndex, out var ret);
		return ret;
	}

	[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorStringIndex", HasExplicitThis = true)]
	internal int Internal_GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
	{
		return Internal_GetCursorStringIndex_Injected(ref position, content, ref cursorPixelPosition);
	}

	[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetSelectedRenderedText", HasExplicitThis = true)]
	internal string Internal_GetSelectedRenderedText(Rect localPosition, GUIContent mContent, int selectIndex, int cursorIndex)
	{
		return Internal_GetSelectedRenderedText_Injected(ref localPosition, mContent, selectIndex, cursorIndex);
	}

	[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetHyperlinksRect", HasExplicitThis = true)]
	internal Rect[] Internal_GetHyperlinksRect(Rect localPosition, GUIContent mContent)
	{
		return Internal_GetHyperlinksRect_Injected(ref localPosition, mContent);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetNumCharactersThatFitWithinWidth", HasExplicitThis = true)]
	internal extern int Internal_GetNumCharactersThatFitWithinWidth(string text, float width);

	[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcSize", HasExplicitThis = true)]
	internal Vector2 Internal_CalcSize(GUIContent content)
	{
		Internal_CalcSize_Injected(content, out var ret);
		return ret;
	}

	[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcSizeWithConstraints", HasExplicitThis = true)]
	internal Vector2 Internal_CalcSizeWithConstraints(GUIContent content, Vector2 maxSize)
	{
		Internal_CalcSizeWithConstraints_Injected(content, ref maxSize, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcHeight", HasExplicitThis = true)]
	private extern float Internal_CalcHeight(GUIContent content, float width);

	[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcMinMaxWidth", HasExplicitThis = true)]
	private Vector2 Internal_CalcMinMaxWidth(GUIContent content)
	{
		Internal_CalcMinMaxWidth_Injected(content, out var ret);
		return ret;
	}

	[FreeFunction(Name = "GUIStyle_Bindings::SetMouseTooltip")]
	internal static void SetMouseTooltip(string tooltip, Rect screenRect)
	{
		SetMouseTooltip_Injected(tooltip, ref screenRect);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyle_Bindings::IsTooltipActive")]
	internal static extern bool IsTooltipActive(string tooltip);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorFlashOffset")]
	private static extern float Internal_GetCursorFlashOffset();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction(Name = "GUIStyle::SetDefaultFont")]
	internal static extern void SetDefaultFont(Font font);

	public GUIStyle()
	{
		m_Ptr = Internal_Create(this);
	}

	public GUIStyle(GUIStyle other)
	{
		if (other == null)
		{
			Debug.LogError("Copied style is null. Using StyleNotFound instead.");
			other = GUISkin.error;
		}
		m_Ptr = Internal_Copy(this, other);
	}

	~GUIStyle()
	{
		if (m_Ptr != IntPtr.Zero)
		{
			Internal_Destroy(m_Ptr);
			m_Ptr = IntPtr.Zero;
		}
	}

	internal static void CleanupRoots()
	{
		s_None = null;
	}

	internal void InternalOnAfterDeserialize()
	{
		m_Normal = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, GetStyleStatePtr(0));
		m_Hover = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, GetStyleStatePtr(1));
		m_Active = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, GetStyleStatePtr(2));
		m_Focused = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, GetStyleStatePtr(3));
		m_OnNormal = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, GetStyleStatePtr(4));
		m_OnHover = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, GetStyleStatePtr(5));
		m_OnActive = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, GetStyleStatePtr(6));
		m_OnFocused = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, GetStyleStatePtr(7));
	}

	public void Draw(Rect position, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
	{
		Draw(position, GUIContent.none, -1, isHover, isActive, on, hasKeyboardFocus);
	}

	public void Draw(Rect position, string text, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
	{
		Draw(position, GUIContent.Temp(text), -1, isHover, isActive, on, hasKeyboardFocus);
	}

	public void Draw(Rect position, Texture image, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
	{
		Draw(position, GUIContent.Temp(image), -1, isHover, isActive, on, hasKeyboardFocus);
	}

	public void Draw(Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
	{
		Draw(position, content, -1, isHover, isActive, on, hasKeyboardFocus);
	}

	public void Draw(Rect position, GUIContent content, int controlID)
	{
		Draw(position, content, controlID, isHover: false, isActive: false, on: false, hasKeyboardFocus: false);
	}

	public void Draw(Rect position, GUIContent content, int controlID, bool on)
	{
		Draw(position, content, controlID, isHover: false, isActive: false, on, hasKeyboardFocus: false);
	}

	public void Draw(Rect position, GUIContent content, int controlID, bool on, bool hover)
	{
		Draw(position, content, controlID, hover, GUIUtility.hotControl == controlID, on, GUIUtility.HasKeyFocus(controlID));
	}

	private void Draw(Rect position, GUIContent content, int controlId, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
	{
		if (controlId == -1)
		{
			Internal_Draw(position, content, isHover, isActive, on, hasKeyboardFocus);
		}
		else
		{
			Internal_Draw2(position, content, controlId, on);
		}
	}

	public void DrawCursor(Rect position, GUIContent content, int controlID, int character)
	{
		Event current = Event.current;
		if (current.type == EventType.Repaint)
		{
			Color cursorColor = new Color(0f, 0f, 0f, 0f);
			float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
			float num = (Time.realtimeSinceStartup - Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
			if (cursorFlashSpeed == 0f || num < 0.5f)
			{
				cursorColor = GUI.skin.settings.cursorColor;
			}
			Internal_DrawCursor(position, content, character, cursorColor);
		}
	}

	internal void DrawWithTextSelection(Rect position, GUIContent content, bool isActive, bool hasKeyboardFocus, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition, Color selectionColor)
	{
		Color cursorColor = new Color(0f, 0f, 0f, 0f);
		float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
		float num = (Time.realtimeSinceStartup - Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
		if (cursorFlashSpeed == 0f || num < 0.5f)
		{
			cursorColor = GUI.skin.settings.cursorColor;
		}
		bool isHover = position.Contains(Event.current.mousePosition);
		Internal_DrawWithTextSelection(position, content, isHover, isActive, on: false, hasKeyboardFocus, drawSelectionAsComposition, firstSelectedCharacter, lastSelectedCharacter, cursorColor, selectionColor);
	}

	internal void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition)
	{
		DrawWithTextSelection(position, content, controlID == GUIUtility.hotControl, controlID == GUIUtility.keyboardControl && showKeyboardFocus, firstSelectedCharacter, lastSelectedCharacter, drawSelectionAsComposition, GUI.skin.settings.selectionColor);
	}

	public void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter)
	{
		DrawWithTextSelection(position, content, controlID, firstSelectedCharacter, lastSelectedCharacter, drawSelectionAsComposition: false);
	}

	public static implicit operator GUIStyle(string str)
	{
		if (GUISkin.current == null)
		{
			Debug.LogError("Unable to use a named GUIStyle without a current skin. Most likely you need to move your GUIStyle initialization code to OnGUI");
			return GUISkin.error;
		}
		return GUISkin.current.GetStyle(str);
	}

	public Vector2 GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
	{
		return Internal_GetCursorPixelPosition(position, content, cursorStringIndex);
	}

	public int GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
	{
		return Internal_GetCursorStringIndex(position, content, cursorPixelPosition);
	}

	internal int GetNumCharactersThatFitWithinWidth(string text, float width)
	{
		return Internal_GetNumCharactersThatFitWithinWidth(text, width);
	}

	public Vector2 CalcSize(GUIContent content)
	{
		return Internal_CalcSize(content);
	}

	internal Vector2 CalcSizeWithConstraints(GUIContent content, Vector2 constraints)
	{
		return Internal_CalcSizeWithConstraints(content, constraints);
	}

	public Vector2 CalcScreenSize(Vector2 contentSize)
	{
		return new Vector2((fixedWidth != 0f) ? fixedWidth : Mathf.Ceil(contentSize.x + (float)padding.left + (float)padding.right), (fixedHeight != 0f) ? fixedHeight : Mathf.Ceil(contentSize.y + (float)padding.top + (float)padding.bottom));
	}

	public float CalcHeight(GUIContent content, float width)
	{
		return Internal_CalcHeight(content, width);
	}

	public void CalcMinMaxWidth(GUIContent content, out float minWidth, out float maxWidth)
	{
		Vector2 vector = Internal_CalcMinMaxWidth(content);
		minWidth = vector.x;
		maxWidth = vector.y;
	}

	public override string ToString()
	{
		return UnityString.Format("GUIStyle '{0}'", name);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_contentOffset_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_contentOffset_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_clipOffset_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_clipOffset_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_Internal_clipOffset_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_Internal_clipOffset_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Internal_Draw_Injected(ref Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Internal_Draw2_Injected(ref Rect position, GUIContent content, int controlID, bool on);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Internal_DrawCursor_Injected(ref Rect position, GUIContent content, int pos, ref Color cursorColor);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Internal_DrawWithTextSelection_Injected(ref Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool drawSelectionAsComposition, int cursorFirst, int cursorLast, ref Color cursorColor, ref Color selectionColor);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Internal_GetCursorPixelPosition_Injected(ref Rect position, GUIContent content, int cursorStringIndex, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int Internal_GetCursorStringIndex_Injected(ref Rect position, GUIContent content, ref Vector2 cursorPixelPosition);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern string Internal_GetSelectedRenderedText_Injected(ref Rect localPosition, GUIContent mContent, int selectIndex, int cursorIndex);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern Rect[] Internal_GetHyperlinksRect_Injected(ref Rect localPosition, GUIContent mContent);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Internal_CalcSize_Injected(GUIContent content, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Internal_CalcSizeWithConstraints_Injected(GUIContent content, ref Vector2 maxSize, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Internal_CalcMinMaxWidth_Injected(GUIContent content, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void SetMouseTooltip_Injected(string tooltip, ref Rect screenRect);
}
public enum ImagePosition
{
	ImageLeft,
	ImageAbove,
	ImageOnly,
	TextOnly
}
public enum TextClipping
{
	Overflow,
	Clip
}
[AttributeUsage(AttributeTargets.Method)]
public class GUITargetAttribute : Attribute
{
	internal int displayMask;

	public GUITargetAttribute()
	{
		displayMask = -1;
	}

	public GUITargetAttribute(int displayIndex)
	{
		displayMask = 1 << displayIndex;
	}

	public GUITargetAttribute(int displayIndex, int displayIndex1)
	{
		displayMask = (1 << displayIndex) | (1 << displayIndex1);
	}

	public GUITargetAttribute(int displayIndex, int displayIndex1, params int[] displayIndexList)
	{
		displayMask = (1 << displayIndex) | (1 << displayIndex1);
		for (int i = 0; i < displayIndexList.Length; i++)
		{
			displayMask |= 1 << displayIndexList[i];
		}
	}

	[RequiredByNativeCode]
	private static int GetGUITargetAttrValue(Type klass, string methodName)
	{
		MethodInfo method = klass.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		if (method != null)
		{
			object[] customAttributes = method.GetCustomAttributes(inherit: true);
			if (customAttributes != null)
			{
				for (int i = 0; i < customAttributes.Length; i++)
				{
					if (!(customAttributes[i].GetType() != typeof(GUITargetAttribute)))
					{
						GUITargetAttribute gUITargetAttribute = customAttributes[i] as GUITargetAttribute;
						return gUITargetAttribute.displayMask;
					}
				}
			}
		}
		return -1;
	}
}
[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
[EditorBrowsable(EditorBrowsableState.Never)]
[ExcludeFromObjectFactory]
[ExcludeFromPreset]
public sealed class GUITexture
{
	[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
	public Color color
	{
		get
		{
			FeatureRemoved();
			return new Color(0f, 0f, 0f);
		}
		set
		{
			FeatureRemoved();
		}
	}

	[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
	public Texture texture
	{
		get
		{
			FeatureRemoved();
			return null;
		}
		set
		{
			FeatureRemoved();
		}
	}

	[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
	public Rect pixelInset
	{
		get
		{
			FeatureRemoved();
			return default(Rect);
		}
		set
		{
			FeatureRemoved();
		}
	}

	[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
	public RectOffset border
	{
		get
		{
			FeatureRemoved();
			return null;
		}
		set
		{
			FeatureRemoved();
		}
	}

	private static void FeatureRemoved()
	{
		throw new Exception("GUITexture has been removed from Unity. Use UI.Image instead.");
	}
}
[NativeHeader("Modules/IMGUI/GUIUtility.h")]
[NativeHeader("Modules/IMGUI/GUIManager.h")]
[NativeHeader("Runtime/Input/InputBindings.h")]
[NativeHeader("Runtime/Utilities/CopyPaste.h")]
[NativeHeader("Runtime/Camera/RenderLayers/GUITexture.h")]
[NativeHeader("Runtime/Input/InputManager.h")]
public class GUIUtility
{
	internal static int s_ControlCount = 0;

	internal static int s_SkinMode;

	internal static int s_OriginalID;

	internal static Action takeCapture;

	internal static Action releaseCapture;

	internal static Func<int, IntPtr, bool> processEvent;

	internal static Action cleanupRoots;

	internal static Func<Exception, bool> endContainerGUIFromException;

	internal static Action guiChanged;

	internal static Action<EventType, KeyCode> beforeEventProcessed;

	private static Event m_Event = new Event();

	internal static Func<bool> s_HasCurrentWindowKeyFocusFunc;

	public static extern bool hasModalWindow
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[NativeProperty("GetGUIState().m_PixelsPerPoint", true, TargetType.Field)]
	internal static extern float pixelsPerPoint
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[NativeProperty("GetGUIState().m_OnGUIDepth", true, TargetType.Field)]
	internal static extern int guiDepth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	internal static Vector2 s_EditorScreenPointOffset
	{
		[NativeMethod("GetGUIState().GetGUIPixelOffset", true)]
		get
		{
			get_s_EditorScreenPointOffset_Injected(out var ret);
			return ret;
		}
		[NativeMethod("GetGUIState().SetGUIPixelOffset", true)]
		set
		{
			set_s_EditorScreenPointOffset_Injected(ref value);
		}
	}

	[NativeProperty("GetGUIState().m_CanvasGUIState.m_IsMouseUsed", true, TargetType.Field)]
	internal static extern bool mouseUsed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetInputManager()", StaticAccessorType.Dot)]
	internal static extern bool textFieldInput
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	internal static extern bool manualTex2SRGBEnabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GUITexture::IsManualTex2SRGBEnabled")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GUITexture::SetManualTex2SRGBEnabled")]
		set;
	}

	public static extern string systemCopyBuffer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetCopyBuffer")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("SetCopyBuffer")]
		set;
	}

	[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
	internal static extern string compositionString
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
	internal static extern IMECompositionMode imeCompositionMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
	internal static Vector2 compositionCursorPos
	{
		get
		{
			get_compositionCursorPos_Injected(out var ret);
			return ret;
		}
		set
		{
			set_compositionCursorPos_Injected(ref value);
		}
	}

	internal static bool guiIsExiting { get; set; }

	public static int hotControl
	{
		get
		{
			return Internal_GetHotControl();
		}
		set
		{
			Internal_SetHotControl(value);
		}
	}

	public static int keyboardControl
	{
		get
		{
			return Internal_GetKeyboardControl();
		}
		set
		{
			Internal_SetKeyboardControl(value);
		}
	}

	[FreeFunction("GetGUIState().GetControlID")]
	private static int Internal_GetControlID(int hint, FocusType focusType, Rect rect)
	{
		return Internal_GetControlID_Injected(hint, focusType, ref rect);
	}

	public static int GetControlID(int hint, FocusType focusType, Rect rect)
	{
		s_ControlCount++;
		return Internal_GetControlID(hint, focusType, rect);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern void BeginContainerFromOwner(ScriptableObject owner);

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern void BeginContainer(ObjectGUIState objectGUIState);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("EndContainer")]
	internal static extern void Internal_EndContainer();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("GetSpecificGUIState(0).m_EternalGUIState->GetNextUniqueID")]
	internal static extern int GetPermanentControlID();

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern int CheckForTabEvent(Event evt);

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern void SetKeyboardControlToFirstControlId();

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern void SetKeyboardControlToLastControlId();

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern bool HasFocusableControls();

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern bool OwnsId(int id);

	public static Rect AlignRectToDevice(Rect rect, out int widthInPixels, out int heightInPixels)
	{
		AlignRectToDevice_Injected(ref rect, out widthInPixels, out heightInPixels, out var ret);
		return ret;
	}

	internal static Vector3 Internal_MultiplyPoint(Vector3 point, Matrix4x4 transform)
	{
		Internal_MultiplyPoint_Injected(ref point, ref transform, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern bool GetChanged();

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern void SetChanged(bool changed);

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal static extern void SetDidGUIWindowsEatLastEvent(bool value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int Internal_GetHotControl();

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int Internal_GetKeyboardControl();

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_SetHotControl(int value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_SetKeyboardControl(int value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern object Internal_GetDefaultSkin(int skinMode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Object Internal_GetBuiltinSkin(int skin);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_ExitGUI();

	private static Vector2 InternalWindowToScreenPoint(Vector2 windowPoint)
	{
		InternalWindowToScreenPoint_Injected(ref windowPoint, out var ret);
		return ret;
	}

	private static Vector2 InternalScreenToWindowPoint(Vector2 screenPoint)
	{
		InternalScreenToWindowPoint_Injected(ref screenPoint, out var ret);
		return ret;
	}

	[RequiredByNativeCode]
	private static void MarkGUIChanged()
	{
		guiChanged?.Invoke();
	}

	public static int GetControlID(FocusType focus)
	{
		return GetControlID(0, focus);
	}

	public static int GetControlID(GUIContent contents, FocusType focus)
	{
		return GetControlID(contents.hash, focus);
	}

	public static int GetControlID(FocusType focus, Rect position)
	{
		return GetControlID(0, focus, position);
	}

	public static int GetControlID(GUIContent contents, FocusType focus, Rect position)
	{
		return GetControlID(contents.hash, focus, position);
	}

	public static int GetControlID(int hint, FocusType focus)
	{
		return GetControlID(hint, focus, Rect.zero);
	}

	public static object GetStateObject(Type t, int controlID)
	{
		return GUIStateObjects.GetStateObject(t, controlID);
	}

	public static object QueryStateObject(Type t, int controlID)
	{
		return GUIStateObjects.QueryStateObject(t, controlID);
	}

	[RequiredByNativeCode]
	internal static void TakeCapture()
	{
		takeCapture?.Invoke();
	}

	[RequiredByNativeCode]
	internal static void RemoveCapture()
	{
		releaseCapture?.Invoke();
	}

	internal static bool HasKeyFocus(int controlID)
	{
		return controlID == keyboardControl && (s_HasCurrentWindowKeyFocusFunc == null || s_HasCurrentWindowKeyFocusFunc());
	}

	public static void ExitGUI()
	{
		throw new ExitGUIException();
	}

	internal static GUISkin GetDefaultSkin(int skinMode)
	{
		return Internal_GetDefaultSkin(skinMode) as GUISkin;
	}

	internal static GUISkin GetDefaultSkin()
	{
		return Internal_GetDefaultSkin(s_SkinMode) as GUISkin;
	}

	internal static GUISkin GetBuiltinSkin(int skin)
	{
		return Internal_GetBuiltinSkin(skin) as GUISkin;
	}

	[RequiredByNativeCode]
	internal static void ProcessEvent(int instanceID, IntPtr nativeEventPtr, out bool result)
	{
		if (beforeEventProcessed != null)
		{
			m_Event.CopyFromPtr(nativeEventPtr);
			beforeEventProcessed(m_Event.type, m_Event.keyCode);
		}
		result = false;
		if (processEvent != null)
		{
			result = processEvent(instanceID, nativeEventPtr);
		}
	}

	internal static void EndContainer()
	{
		Internal_EndContainer();
		Internal_ExitGUI();
	}

	internal static void CleanupRoots()
	{
		cleanupRoots?.Invoke();
	}

	[RequiredByNativeCode]
	internal static void BeginGUI(int skinMode, int instanceID, int useGUILayout)
	{
		s_SkinMode = skinMode;
		s_OriginalID = instanceID;
		ResetGlobalState();
		if (useGUILayout != 0)
		{
			GUILayoutUtility.Begin(instanceID);
		}
	}

	[RequiredByNativeCode]
	internal static void DestroyGUI(int instanceID)
	{
		GUILayoutUtility.RemoveSelectedIdList(instanceID, isWindow: false);
	}

	[RequiredByNativeCode]
	internal static void EndGUI(int layoutType)
	{
		try
		{
			if (Event.current.type == EventType.Layout)
			{
				switch (layoutType)
				{
				case 1:
					GUILayoutUtility.Layout();
					break;
				case 2:
					GUILayoutUtility.LayoutFromEditorWindow();
					break;
				}
			}
			GUILayoutUtility.SelectIDList(s_OriginalID, isWindow: false);
			GUIContent.ClearStaticCache();
		}
		finally
		{
			Internal_ExitGUI();
		}
	}

	[RequiredByNativeCode]
	internal static bool EndGUIFromException(Exception exception)
	{
		Internal_ExitGUI();
		return ShouldRethrowException(exception);
	}

	[RequiredByNativeCode]
	internal static bool EndContainerGUIFromException(Exception exception)
	{
		if (endContainerGUIFromException != null)
		{
			return endContainerGUIFromException(exception);
		}
		return false;
	}

	internal static void ResetGlobalState()
	{
		GUI.skin = null;
		guiIsExiting = false;
		GUI.changed = false;
		GUI.scrollViewStates.Clear();
	}

	internal static bool IsExitGUIException(Exception exception)
	{
		while (exception is TargetInvocationException && exception.InnerException != null)
		{
			exception = exception.InnerException;
		}
		return exception is ExitGUIException;
	}

	internal static bool ShouldRethrowException(Exception exception)
	{
		return IsExitGUIException(exception);
	}

	internal static void CheckOnGUI()
	{
		if (guiDepth <= 0)
		{
			throw new ArgumentException("You can only call GUI functions from inside OnGUI.");
		}
	}

	internal static float RoundToPixelGrid(float v)
	{
		return Mathf.Floor(v * pixelsPerPoint + 0.48f) / pixelsPerPoint;
	}

	internal static float RoundToPixelGrid(float v, float scale)
	{
		return Mathf.Floor(v * scale + 0.48f) / scale;
	}

	public static Vector2 GUIToScreenPoint(Vector2 guiPoint)
	{
		return InternalWindowToScreenPoint(GUIClip.UnclipToWindow(guiPoint));
	}

	public static Rect GUIToScreenRect(Rect guiRect)
	{
		Vector2 vector = GUIToScreenPoint(new Vector2(guiRect.x, guiRect.y));
		guiRect.x = vector.x;
		guiRect.y = vector.y;
		return guiRect;
	}

	public static Vector2 ScreenToGUIPoint(Vector2 screenPoint)
	{
		return GUIClip.ClipToWindow(InternalScreenToWindowPoint(screenPoint));
	}

	public static Rect ScreenToGUIRect(Rect screenRect)
	{
		Vector2 vector = ScreenToGUIPoint(new Vector2(screenRect.x, screenRect.y));
		screenRect.x = vector.x;
		screenRect.y = vector.y;
		return screenRect;
	}

	public static void RotateAroundPivot(float angle, Vector2 pivotPoint)
	{
		Matrix4x4 matrix = GUI.matrix;
		GUI.matrix = Matrix4x4.identity;
		Vector2 vector = GUIClip.Unclip(pivotPoint);
		Matrix4x4 matrix4x = Matrix4x4.TRS(vector, Quaternion.Euler(0f, 0f, angle), Vector3.one) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
		GUI.matrix = matrix4x * matrix;
	}

	public static void ScaleAroundPivot(Vector2 scale, Vector2 pivotPoint)
	{
		Matrix4x4 matrix = GUI.matrix;
		Vector2 vector = GUIClip.Unclip(pivotPoint);
		Matrix4x4 matrix4x = Matrix4x4.TRS(vector, Quaternion.identity, new Vector3(scale.x, scale.y, 1f)) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
		GUI.matrix = matrix4x * matrix;
	}

	public static Rect AlignRectToDevice(Rect rect)
	{
		int widthInPixels;
		int heightInPixels;
		return AlignRectToDevice(rect, out widthInPixels, out heightInPixels);
	}

	internal static bool HitTest(Rect rect, Vector2 point, int offset)
	{
		return point.x >= rect.xMin - (float)offset && point.x < rect.xMax + (float)offset && point.y >= rect.yMin - (float)offset && point.y < rect.yMax + (float)offset;
	}

	internal static bool HitTest(Rect rect, Vector2 point, bool isDirectManipulationDevice)
	{
		int offset = 0;
		return HitTest(rect, point, offset);
	}

	internal static bool HitTest(Rect rect, Event evt)
	{
		return HitTest(rect, evt.mousePosition, evt.isDirectManipulationDevice);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_s_EditorScreenPointOffset_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_s_EditorScreenPointOffset_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int Internal_GetControlID_Injected(int hint, FocusType focusType, ref Rect rect);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void AlignRectToDevice_Injected(ref Rect rect, out int widthInPixels, out int heightInPixels, out Rect ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_compositionCursorPos_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_compositionCursorPos_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_MultiplyPoint_Injected(ref Vector3 point, ref Matrix4x4 transform, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void InternalWindowToScreenPoint_Injected(ref Vector2 windowPoint, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void InternalScreenToWindowPoint_Injected(ref Vector2 screenPoint, out Vector2 ret);
}
public sealed class ExitGUIException : Exception
{
	public ExitGUIException()
	{
		GUIUtility.guiIsExiting = true;
	}

	internal ExitGUIException(string message)
		: base(message)
	{
		GUIUtility.guiIsExiting = true;
	}
}
internal class GUILayoutEntry
{
	public float minWidth;

	public float maxWidth;

	public float minHeight;

	public float maxHeight;

	public Rect rect = new Rect(0f, 0f, 0f, 0f);

	public int stretchWidth;

	public int stretchHeight;

	public bool consideredForMargin = true;

	private GUIStyle m_Style = GUIStyle.none;

	internal static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

	protected static int indent = 0;

	public GUIStyle style
	{
		get
		{
			return m_Style;
		}
		set
		{
			m_Style = value;
			ApplyStyleSettings(value);
		}
	}

	public virtual int marginLeft => style.margin.left;

	public virtual int marginRight => style.margin.right;

	public virtual int marginTop => style.margin.top;

	public virtual int marginBottom => style.margin.bottom;

	public int marginHorizontal => marginLeft + marginRight;

	public int marginVertical => marginBottom + marginTop;

	public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style)
	{
		minWidth = _minWidth;
		maxWidth = _maxWidth;
		minHeight = _minHeight;
		maxHeight = _maxHeight;
		if (_style == null)
		{
			_style = GUIStyle.none;
		}
		style = _style;
	}

	public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style, GUILayoutOption[] options)
	{
		minWidth = _minWidth;
		maxWidth = _maxWidth;
		minHeight = _minHeight;
		maxHeight = _maxHeight;
		style = _style;
		ApplyOptions(options);
	}

	public virtual void CalcWidth()
	{
	}

	public virtual void CalcHeight()
	{
	}

	public virtual void SetHorizontal(float x, float width)
	{
		rect.x = x;
		rect.width = width;
	}

	public virtual void SetVertical(float y, float height)
	{
		rect.y = y;
		rect.height = height;
	}

	protected virtual void ApplyStyleSettings(GUIStyle style)
	{
		stretchWidth = ((style.fixedWidth == 0f && style.stretchWidth) ? 1 : 0);
		stretchHeight = ((style.fixedHeight == 0f && style.stretchHeight) ? 1 : 0);
		m_Style = style;
	}

	public virtual void ApplyOptions(GUILayoutOption[] options)
	{
		if (options == null)
		{
			return;
		}
		foreach (GUILayoutOption gUILayoutOption in options)
		{
			switch (gUILayoutOption.type)
			{
			case GUILayoutOption.Type.fixedWidth:
				minWidth = (maxWidth = (float)gUILayoutOption.value);
				stretchWidth = 0;
				break;
			case GUILayoutOption.Type.fixedHeight:
				minHeight = (maxHeight = (float)gUILayoutOption.value);
				stretchHeight = 0;
				break;
			case GUILayoutOption.Type.minWidth:
				minWidth = (float)gUILayoutOption.value;
				if (maxWidth < minWidth)
				{
					maxWidth = minWidth;
				}
				break;
			case GUILayoutOption.Type.maxWidth:
				maxWidth = (float)gUILayoutOption.value;
				if (minWidth > maxWidth)
				{
					minWidth = maxWidth;
				}
				stretchWidth = 0;
				break;
			case GUILayoutOption.Type.minHeight:
				minHeight = (float)gUILayoutOption.value;
				if (maxHeight < minHeight)
				{
					maxHeight = minHeight;
				}
				break;
			case GUILayoutOption.Type.maxHeight:
				maxHeight = (float)gUILayoutOption.value;
				if (minHeight > maxHeight)
				{
					minHeight = maxHeight;
				}
				stretchHeight = 0;
				break;
			case GUILayoutOption.Type.stretchWidth:
				stretchWidth = (int)gUILayoutOption.value;
				break;
			case GUILayoutOption.Type.stretchHeight:
				stretchHeight = (int)gUILayoutOption.value;
				break;
			}
		}
		if (maxWidth != 0f && maxWidth < minWidth)
		{
			maxWidth = minWidth;
		}
		if (maxHeight != 0f && maxHeight < minHeight)
		{
			maxHeight = minHeight;
		}
	}

	public override string ToString()
	{
		string text = "";
		for (int i = 0; i < indent; i++)
		{
			text += " ";
		}
		return text + UnityString.Format("{1}-{0} (x:{2}-{3}, y:{4}-{5})", (style != null) ? style.name : "NULL", GetType(), rect.x, rect.xMax, rect.y, rect.yMax) + "   -   W: " + minWidth + "-" + maxWidth + ((stretchWidth != 0) ? "+" : "") + ", H: " + minHeight + "-" + maxHeight + ((stretchHeight != 0) ? "+" : "");
	}
}
internal sealed class GUIAspectSizer : GUILayoutEntry
{
	private float aspect;

	public GUIAspectSizer(float aspect, GUILayoutOption[] options)
		: base(0f, 0f, 0f, 0f, GUIStyle.none)
	{
		this.aspect = aspect;
		ApplyOptions(options);
	}

	public override void CalcHeight()
	{
		minHeight = (maxHeight = rect.width / aspect);
	}
}
internal sealed class GUIGridSizer : GUILayoutEntry
{
	private readonly int m_Count;

	private readonly int m_XCount;

	private readonly float m_MinButtonWidth = -1f;

	private readonly float m_MaxButtonWidth = -1f;

	private readonly float m_MinButtonHeight = -1f;

	private readonly float m_MaxButtonHeight = -1f;

	private int rows
	{
		get
		{
			int num = m_Count / m_XCount;
			if (m_Count % m_XCount != 0)
			{
				num++;
			}
			return num;
		}
	}

	public static Rect GetRect(GUIContent[] contents, int xCount, GUIStyle style, GUILayoutOption[] options)
	{
		Rect result = new Rect(0f, 0f, 0f, 0f);
		switch (Event.current.type)
		{
		case EventType.Layout:
			GUILayoutUtility.current.topLevel.Add(new GUIGridSizer(contents, xCount, style, options));
			break;
		case EventType.Used:
			return GUILayoutEntry.kDummyRect;
		default:
			result = GUILayoutUtility.current.topLevel.GetNext().rect;
			break;
		}
		return result;
	}

	private GUIGridSizer(GUIContent[] contents, int xCount, GUIStyle buttonStyle, GUILayoutOption[] options)
		: base(0f, 0f, 0f, 0f, GUIStyle.none)
	{
		m_Count = contents.Length;
		m_XCount = xCount;
		ApplyStyleSettings(buttonStyle);
		ApplyOptions(options);
		if (xCount == 0 || contents.Length == 0)
		{
			return;
		}
		float num = Mathf.Max(buttonStyle.margin.left, buttonStyle.margin.right) * (m_XCount - 1);
		float num2 = Mathf.Max(buttonStyle.margin.top, buttonStyle.margin.bottom) * (rows - 1);
		if (buttonStyle.fixedWidth != 0f)
		{
			m_MinButtonWidth = (m_MaxButtonWidth = buttonStyle.fixedWidth);
		}
		if (buttonStyle.fixedHeight != 0f)
		{
			m_MinButtonHeight = (m_MaxButtonHeight = buttonStyle.fixedHeight);
		}
		if (m_MinButtonWidth == -1f)
		{
			if (minWidth != 0f)
			{
				m_MinButtonWidth = (minWidth - num) / (float)m_XCount;
			}
			if (maxWidth != 0f)
			{
				m_MaxButtonWidth = (maxWidth - num) / (float)m_XCount;
			}
		}
		if (m_MinButtonHeight == -1f)
		{
			if (minHeight != 0f)
			{
				m_MinButtonHeight = (minHeight - num2) / (float)rows;
			}
			if (maxHeight != 0f)
			{
				m_MaxButtonHeight = (maxHeight - num2) / (float)rows;
			}
		}
		if (m_MinButtonHeight == -1f || m_MaxButtonHeight == -1f || m_MinButtonWidth == -1f || m_MaxButtonWidth == -1f)
		{
			float num3 = 0f;
			float num4 = 0f;
			foreach (GUIContent content in contents)
			{
				Vector2 vector = buttonStyle.CalcSize(content);
				num4 = Mathf.Max(num4, vector.x);
				num3 = Mathf.Max(num3, vector.y);
			}
			if (m_MinButtonWidth == -1f)
			{
				if (m_MaxButtonWidth != -1f)
				{
					m_MinButtonWidth = Mathf.Min(num4, m_MaxButtonWidth);
				}
				else
				{
					m_MinButtonWidth = num4;
				}
			}
			if (m_MaxButtonWidth == -1f)
			{
				if (m_MinButtonWidth != -1f)
				{
					m_MaxButtonWidth = Mathf.Max(num4, m_MinButtonWidth);
				}
				else
				{
					m_MaxButtonWidth = num4;
				}
			}
			if (m_MinButtonHeight == -1f)
			{
				if (m_MaxButtonHeight != -1f)
				{
					m_MinButtonHeight = Mathf.Min(num3, m_MaxButtonHeight);
				}
				else
				{
					m_MinButtonHeight = num3;
				}
			}
			if (m_MaxButtonHeight == -1f)
			{
				if (m_MinButtonHeight != -1f)
				{
					maxHeight = Mathf.Max(maxHeight, m_MinButtonHeight);
				}
				m_MaxButtonHeight = maxHeight;
			}
		}
		minWidth = m_MinButtonWidth * (float)m_XCount + num;
		maxWidth = m_MaxButtonWidth * (float)m_XCount + num;
		minHeight = m_MinButtonHeight * (float)rows + num2;
		maxHeight = m_MaxButtonHeight * (float)rows + num2;
	}
}
internal sealed class GUIWordWrapSizer : GUILayoutEntry
{
	private readonly GUIContent m_Content;

	private readonly float m_ForcedMinHeight;

	private readonly float m_ForcedMaxHeight;

	public GUIWordWrapSizer(GUIStyle style, GUIContent content, GUILayoutOption[] options)
		: base(0f, 0f, 0f, 0f, style)
	{
		m_Content = new GUIContent(content);
		ApplyOptions(options);
		m_ForcedMinHeight = minHeight;
		m_ForcedMaxHeight = maxHeight;
	}

	public override void CalcWidth()
	{
		if (minWidth == 0f || maxWidth == 0f)
		{
			base.style.CalcMinMaxWidth(m_Content, out var f, out var f2);
			f = Mathf.Ceil(f);
			f2 = Mathf.Ceil(f2);
			if (minWidth == 0f)
			{
				minWidth = f;
			}
			if (maxWidth == 0f)
			{
				maxWidth = f2;
			}
		}
	}

	public override void CalcHeight()
	{
		if (m_ForcedMinHeight == 0f || m_ForcedMaxHeight == 0f)
		{
			float num = base.style.CalcHeight(m_Content, rect.width);
			if (m_ForcedMinHeight == 0f)
			{
				minHeight = num;
			}
			else
			{
				minHeight = m_ForcedMinHeight;
			}
			if (m_ForcedMaxHeight == 0f)
			{
				maxHeight = num;
			}
			else
			{
				maxHeight = m_ForcedMaxHeight;
			}
		}
	}
}
[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "Unity.UIElements" })]
internal class GUILayoutGroup : GUILayoutEntry
{
	public List<GUILayoutEntry> entries = new List<GUILayoutEntry>();

	public bool isVertical = true;

	public bool resetCoords = false;

	public float spacing = 0f;

	public bool sameSize = true;

	public bool isWindow = false;

	public int windowID = -1;

	private int m_Cursor = 0;

	protected int m_StretchableCountX = 100;

	protected int m_StretchableCountY = 100;

	protected bool m_UserSpecifiedWidth = false;

	protected bool m_UserSpecifiedHeight = false;

	protected float m_ChildMinWidth = 100f;

	protected float m_ChildMaxWidth = 100f;

	protected float m_ChildMinHeight = 100f;

	protected float m_ChildMaxHeight = 100f;

	protected int m_MarginLeft;

	protected int m_MarginRight;

	protected int m_MarginTop;

	protected int m_MarginBottom;

	private static readonly GUILayoutEntry none = new GUILayoutEntry(0f, 1f, 0f, 1f, GUIStyle.none);

	public override int marginLeft => m_MarginLeft;

	public override int marginRight => m_MarginRight;

	public override int marginTop => m_MarginTop;

	public override int marginBottom => m_MarginBottom;

	public GUILayoutGroup()
		: base(0f, 0f, 0f, 0f, GUIStyle.none)
	{
	}

	public GUILayoutGroup(GUIStyle _style, GUILayoutOption[] options)
		: base(0f, 0f, 0f, 0f, _style)
	{
		if (options != null)
		{
			ApplyOptions(options);
		}
		m_MarginLeft = _style.margin.left;
		m_MarginRight = _style.margin.right;
		m_MarginTop = _style.margin.top;
		m_MarginBottom = _style.margin.bottom;
	}

	public override void ApplyOptions(GUILayoutOption[] options)
	{
		if (options == null)
		{
			return;
		}
		base.ApplyOptions(options);
		foreach (GUILayoutOption gUILayoutOption in options)
		{
			switch (gUILayoutOption.type)
			{
			case GUILayoutOption.Type.fixedWidth:
			case GUILayoutOption.Type.minWidth:
			case GUILayoutOption.Type.maxWidth:
				m_UserSpecifiedHeight = true;
				break;
			case GUILayoutOption.Type.fixedHeight:
			case GUILayoutOption.Type.minHeight:
			case GUILayoutOption.Type.maxHeight:
				m_UserSpecifiedWidth = true;
				break;
			case GUILayoutOption.Type.spacing:
				spacing = (int)gUILayoutOption.value;
				break;
			}
		}
	}

	protected override void ApplyStyleSettings(GUIStyle style)
	{
		base.ApplyStyleSettings(style);
		RectOffset margin = style.margin;
		m_MarginLeft = margin.left;
		m_MarginRight = margin.right;
		m_MarginTop = margin.top;
		m_MarginBottom = margin.bottom;
	}

	public void ResetCursor()
	{
		m_Cursor = 0;
	}

	public Rect PeekNext()
	{
		if (m_Cursor < entries.Count)
		{
			GUILayoutEntry gUILayoutEntry = entries[m_Cursor];
			return gUILayoutEntry.rect;
		}
		if (Event.current.type == EventType.Repaint)
		{
			throw new ArgumentException("Getting control " + m_Cursor + "'s position in a group with only " + entries.Count + " controls when doing " + Event.current.rawType.ToString() + "\nAborting");
		}
		return GUILayoutEntry.kDummyRect;
	}

	public GUILayoutEntry GetNext()
	{
		if (m_Cursor < entries.Count)
		{
			GUILayoutEntry result = entries[m_Cursor];
			m_Cursor++;
			return result;
		}
		if (Event.current.type == EventType.Repaint)
		{
			throw new ArgumentException("Getting control " + m_Cursor + "'s position in a group with only " + entries.Count + " controls when doing " + Event.current.rawType.ToString() + "\nAborting");
		}
		return none;
	}

	public Rect GetLast()
	{
		if (m_Cursor == 0)
		{
			if (Event.current.type == EventType.Repaint)
			{
				Debug.LogError("You cannot call GetLast immediately after beginning a group.");
			}
			return GUILayoutEntry.kDummyRect;
		}
		if (m_Cursor <= entries.Count)
		{
			GUILayoutEntry gUILayoutEntry = entries[m_Cursor - 1];
			return gUILayoutEntry.rect;
		}
		if (Event.current.type == EventType.Repaint)
		{
			Debug.LogError("Getting control " + m_Cursor + "'s position in a group with only " + entries.Count + " controls when doing " + Event.current.rawType);
		}
		return GUILayoutEntry.kDummyRect;
	}

	public void Add(GUILayoutEntry e)
	{
		entries.Add(e);
	}

	public override void CalcWidth()
	{
		if (entries.Count == 0)
		{
			maxWidth = (minWidth = base.style.padding.horizontal);
			return;
		}
		int num = 0;
		int num2 = 0;
		m_ChildMinWidth = 0f;
		m_ChildMaxWidth = 0f;
		m_StretchableCountX = 0;
		bool flag = true;
		if (isVertical)
		{
			foreach (GUILayoutEntry entry in entries)
			{
				entry.CalcWidth();
				if (entry.consideredForMargin)
				{
					if (!flag)
					{
						num = Mathf.Min(entry.marginLeft, num);
						num2 = Mathf.Min(entry.marginRight, num2);
					}
					else
					{
						num = entry.marginLeft;
						num2 = entry.marginRight;
						flag = false;
					}
					m_ChildMinWidth = Mathf.Max(entry.minWidth + (float)entry.marginHorizontal, m_ChildMinWidth);
					m_ChildMaxWidth = Mathf.Max(entry.maxWidth + (float)entry.marginHorizontal, m_ChildMaxWidth);
				}
				m_StretchableCountX += entry.stretchWidth;
			}
			m_ChildMinWidth -= num + num2;
			m_ChildMaxWidth -= num + num2;
		}
		else
		{
			int num3 = 0;
			foreach (GUILayoutEntry entry2 in entries)
			{
				entry2.CalcWidth();
				if (entry2.consideredForMargin)
				{
					int num4;
					if (!flag)
					{
						num4 = ((num3 > entry2.marginLeft) ? num3 : entry2.marginLeft);
					}
					else
					{
						num4 = 0;
						flag = false;
					}
					m_ChildMinWidth += entry2.minWidth + spacing + (float)num4;
					m_ChildMaxWidth += entry2.maxWidth + spacing + (float)num4;
					num3 = entry2.marginRight;
					m_StretchableCountX += entry2.stretchWidth;
				}
				else
				{
					m_ChildMinWidth += entry2.minWidth;
					m_ChildMaxWidth += entry2.maxWidth;
					m_StretchableCountX += entry2.stretchWidth;
				}
			}
			m_ChildMinWidth -= spacing;
			m_ChildMaxWidth -= spacing;
			if (entries.Count != 0)
			{
				num = entries[0].marginLeft;
				num2 = num3;
			}
			else
			{
				num = (num2 = 0);
			}
		}
		float num5 = 0f;
		float num6 = 0f;
		if (base.style != GUIStyle.none || m_UserSpecifiedWidth)
		{
			num5 = Mathf.Max(base.style.padding.left, num);
			num6 = Mathf.Max(base.style.padding.right, num2);
		}
		else
		{
			m_MarginLeft = num;
			m_MarginRight = num2;
			num5 = (num6 = 0f);
		}
		minWidth = Mathf.Max(minWidth, m_ChildMinWidth + num5 + num6);
		if (maxWidth == 0f)
		{
			stretchWidth += m_StretchableCountX + (base.style.stretchWidth ? 1 : 0);
			maxWidth = m_ChildMaxWidth + num5 + num6;
		}
		else
		{
			stretchWidth = 0;
		}
		maxWidth = Mathf.Max(maxWidth, minWidth);
		if (base.style.fixedWidth != 0f)
		{
			maxWidth = (minWidth = base.style.fixedWidth);
			stretchWidth = 0;
		}
	}

	public override void SetHorizontal(float x, float width)
	{
		base.SetHorizontal(x, width);
		if (resetCoords)
		{
			x = 0f;
		}
		RectOffset padding = base.style.padding;
		if (isVertical)
		{
			if (base.style != GUIStyle.none)
			{
				foreach (GUILayoutEntry entry in entries)
				{
					float num = Mathf.Max(entry.marginLeft, padding.left);
					float x2 = x + num;
					float num2 = width - (float)Mathf.Max(entry.marginRight, padding.right) - num;
					if (entry.stretchWidth != 0)
					{
						entry.SetHorizontal(x2, num2);
					}
					else
					{
						entry.SetHorizontal(x2, Mathf.Clamp(num2, entry.minWidth, entry.maxWidth));
					}
				}
				return;
			}
			float num3 = x - (float)marginLeft;
			float num4 = width + (float)base.marginHorizontal;
			{
				foreach (GUILayoutEntry entry2 in entries)
				{
					if (entry2.stretchWidth != 0)
					{
						entry2.SetHorizontal(num3 + (float)entry2.marginLeft, num4 - (float)entry2.marginHorizontal);
					}
					else
					{
						entry2.SetHorizontal(num3 + (float)entry2.marginLeft, Mathf.Clamp(num4 - (float)entry2.marginHorizontal, entry2.minWidth, entry2.maxWidth));
					}
				}
				return;
			}
		}
		if (base.style != GUIStyle.none)
		{
			float num5 = padding.left;
			float num6 = padding.right;
			if (entries.Count != 0)
			{
				num5 = Mathf.Max(num5, entries[0].marginLeft);
				num6 = Mathf.Max(num6, entries[entries.Count - 1].marginRight);
			}
			x += num5;
			width -= num6 + num5;
		}
		float num7 = width - spacing * (float)(entries.Count - 1);
		float t = 0f;
		if (m_ChildMinWidth != m_ChildMaxWidth)
		{
			t = Mathf.Clamp((num7 - m_ChildMinWidth) / (m_ChildMaxWidth - m_ChildMinWidth), 0f, 1f);
		}
		float num8 = 0f;
		if (num7 > m_ChildMaxWidth && m_StretchableCountX > 0)
		{
			num8 = (num7 - m_ChildMaxWidth) / (float)m_StretchableCountX;
		}
		int num9 = 0;
		bool flag = true;
		foreach (GUILayoutEntry entry3 in entries)
		{
			float num10 = Mathf.Lerp(entry3.minWidth, entry3.maxWidth, t);
			num10 += num8 * (float)entry3.stretchWidth;
			if (entry3.consideredForMargin)
			{
				int num11 = entry3.marginLeft;
				if (flag)
				{
					num11 = 0;
					flag = false;
				}
				int num12 = ((num9 > num11) ? num9 : num11);
				x += (float)num12;
				num9 = entry3.marginRight;
			}
			entry3.SetHorizontal(Mathf.Round(x), Mathf.Round(num10));
			x += num10 + spacing;
		}
	}

	public override void CalcHeight()
	{
		if (entries.Count == 0)
		{
			maxHeight = (minHeight = base.style.padding.vertical);
			return;
		}
		int b = 0;
		int b2 = 0;
		m_ChildMinHeight = 0f;
		m_ChildMaxHeight = 0f;
		m_StretchableCountY = 0;
		if (isVertical)
		{
			int num = 0;
			bool flag = true;
			foreach (GUILayoutEntry entry in entries)
			{
				entry.CalcHeight();
				if (entry.consideredForMargin)
				{
					int num2;
					if (!flag)
					{
						num2 = Mathf.Max(num, entry.marginTop);
					}
					else
					{
						num2 = 0;
						flag = false;
					}
					m_ChildMinHeight += entry.minHeight + spacing + (float)num2;
					m_ChildMaxHeight += entry.maxHeight + spacing + (float)num2;
					num = entry.marginBottom;
					m_StretchableCountY += entry.stretchHeight;
				}
				else
				{
					m_ChildMinHeight += entry.minHeight;
					m_ChildMaxHeight += entry.maxHeight;
					m_StretchableCountY += entry.stretchHeight;
				}
			}
			m_ChildMinHeight -= spacing;
			m_ChildMaxHeight -= spacing;
			if (entries.Count != 0)
			{
				b = entries[0].marginTop;
				b2 = num;
			}
			else
			{
				b2 = (b = 0);
			}
		}
		else
		{
			bool flag2 = true;
			foreach (GUILayoutEntry entry2 in entries)
			{
				entry2.CalcHeight();
				if (entry2.consideredForMargin)
				{
					if (!flag2)
					{
						b = Mathf.Min(entry2.marginTop, b);
						b2 = Mathf.Min(entry2.marginBottom, b2);
					}
					else
					{
						b = entry2.marginTop;
						b2 = entry2.marginBottom;
						flag2 = false;
					}
					m_ChildMinHeight = Mathf.Max(entry2.minHeight, m_ChildMinHeight);
					m_ChildMaxHeight = Mathf.Max(entry2.maxHeight, m_ChildMaxHeight);
				}
				m_StretchableCountY += entry2.stretchHeight;
			}
		}
		float num3 = 0f;
		float num4 = 0f;
		if (base.style != GUIStyle.none || m_UserSpecifiedHeight)
		{
			num3 = Mathf.Max(base.style.padding.top, b);
			num4 = Mathf.Max(base.style.padding.bottom, b2);
		}
		else
		{
			m_MarginTop = b;
			m_MarginBottom = b2;
			num3 = (num4 = 0f);
		}
		minHeight = Mathf.Max(minHeight, m_ChildMinHeight + num3 + num4);
		if (maxHeight == 0f)
		{
			stretchHeight += m_StretchableCountY + (base.style.stretchHeight ? 1 : 0);
			maxHeight = m_ChildMaxHeight + num3 + num4;
		}
		else
		{
			stretchHeight = 0;
		}
		maxHeight = Mathf.Max(maxHeight, minHeight);
		if (base.style.fixedHeight != 0f)
		{
			maxHeight = (minHeight = base.style.fixedHeight);
			stretchHeight = 0;
		}
	}

	public override void SetVertical(float y, float height)
	{
		base.SetVertical(y, height);
		if (entries.Count == 0)
		{
			return;
		}
		RectOffset padding = base.style.padding;
		if (resetCoords)
		{
			y = 0f;
		}
		if (isVertical)
		{
			if (base.style != GUIStyle.none)
			{
				float num = padding.top;
				float num2 = padding.bottom;
				if (entries.Count != 0)
				{
					num = Mathf.Max(num, entries[0].marginTop);
					num2 = Mathf.Max(num2, entries[entries.Count - 1].marginBottom);
				}
				y += num;
				height -= num2 + num;
			}
			float num3 = height - spacing * (float)(entries.Count - 1);
			float t = 0f;
			if (m_ChildMinHeight != m_ChildMaxHeight)
			{
				t = Mathf.Clamp((num3 - m_ChildMinHeight) / (m_ChildMaxHeight - m_ChildMinHeight), 0f, 1f);
			}
			float num4 = 0f;
			if (num3 > m_ChildMaxHeight && m_StretchableCountY > 0)
			{
				num4 = (num3 - m_ChildMaxHeight) / (float)m_StretchableCountY;
			}
			int num5 = 0;
			bool flag = true;
			{
				foreach (GUILayoutEntry entry in entries)
				{
					float num6 = Mathf.Lerp(entry.minHeight, entry.maxHeight, t);
					num6 += num4 * (float)entry.stretchHeight;
					if (entry.consideredForMargin)
					{
						int num7 = entry.marginTop;
						if (flag)
						{
							num7 = 0;
							flag = false;
						}
						int num8 = ((num5 > num7) ? num5 : num7);
						y += (float)num8;
						num5 = entry.marginBottom;
					}
					entry.SetVertical(Mathf.Round(y), Mathf.Round(num6));
					y += num6 + spacing;
				}
				return;
			}
		}
		if (base.style != GUIStyle.none)
		{
			foreach (GUILayoutEntry entry2 in entries)
			{
				float num9 = Mathf.Max(entry2.marginTop, padding.top);
				float y2 = y + num9;
				float num10 = height - (float)Mathf.Max(entry2.marginBottom, padding.bottom) - num9;
				if (entry2.stretchHeight != 0)
				{
					entry2.SetVertical(y2, num10);
				}
				else
				{
					entry2.SetVertical(y2, Mathf.Clamp(num10, entry2.minHeight, entry2.maxHeight));
				}
			}
			return;
		}
		float num11 = y - (float)marginTop;
		float num12 = height + (float)base.marginVertical;
		foreach (GUILayoutEntry entry3 in entries)
		{
			if (entry3.stretchHeight != 0)
			{
				entry3.SetVertical(num11 + (float)entry3.marginTop, num12 - (float)entry3.marginVertical);
			}
			else
			{
				entry3.SetVertical(num11 + (float)entry3.marginTop, Mathf.Clamp(num12 - (float)entry3.marginVertical, entry3.minHeight, entry3.maxHeight));
			}
		}
	}

	public override string ToString()
	{
		string text = "";
		string text2 = "";
		for (int i = 0; i < GUILayoutEntry.indent; i++)
		{
			text2 += " ";
		}
		text = text + base.ToString() + " Margins: " + m_ChildMinHeight + " {\n";
		GUILayoutEntry.indent += 4;
		foreach (GUILayoutEntry entry in entries)
		{
			text = text + entry?.ToString() + "\n";
		}
		text = text + text2 + "}";
		GUILayoutEntry.indent -= 4;
		return text;
	}
}
internal sealed class GUIScrollGroup : GUILayoutGroup
{
	public float calcMinWidth;

	public float calcMaxWidth;

	public float calcMinHeight;

	public float calcMaxHeight;

	public float clientWidth;

	public float clientHeight;

	public bool allowHorizontalScroll = true;

	public bool allowVerticalScroll = true;

	public bool needsHorizontalScrollbar;

	public bool needsVerticalScrollbar;

	public GUIStyle horizontalScrollbar;

	public GUIStyle verticalScrollbar;

	[RequiredByNativeCode]
	public GUIScrollGroup()
	{
	}

	public override void CalcWidth()
	{
		float num = minWidth;
		float num2 = maxWidth;
		if (allowHorizontalScroll)
		{
			minWidth = 0f;
			maxWidth = 0f;
		}
		base.CalcWidth();
		calcMinWidth = minWidth;
		calcMaxWidth = maxWidth;
		if (allowHorizontalScroll)
		{
			if (minWidth > 32f)
			{
				minWidth = 32f;
			}
			if (num != 0f)
			{
				minWidth = num;
			}
			if (num2 != 0f)
			{
				maxWidth = num2;
				stretchWidth = 0;
			}
		}
	}

	public override void SetHorizontal(float x, float width)
	{
		float num = (needsVerticalScrollbar ? (width - verticalScrollbar.fixedWidth - (float)verticalScrollbar.margin.left) : width);
		if (allowHorizontalScroll && num < calcMinWidth)
		{
			needsHorizontalScrollbar = true;
			minWidth = calcMinWidth;
			maxWidth = calcMaxWidth;
			base.SetHorizontal(x, calcMinWidth);
			rect.width = width;
			clientWidth = calcMinWidth;
			return;
		}
		needsHorizontalScrollbar = false;
		if (allowHorizontalScroll)
		{
			minWidth = calcMinWidth;
			maxWidth = calcMaxWidth;
		}
		base.SetHorizontal(x, num);
		rect.width = width;
		clientWidth = num;
	}

	public override void CalcHeight()
	{
		float num = minHeight;
		float num2 = maxHeight;
		if (allowVerticalScroll)
		{
			minHeight = 0f;
			maxHeight = 0f;
		}
		base.CalcHeight();
		calcMinHeight = minHeight;
		calcMaxHeight = maxHeight;
		if (needsHorizontalScrollbar)
		{
			float num3 = horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
			minHeight += num3;
			maxHeight += num3;
		}
		if (allowVerticalScroll)
		{
			if (minHeight > 32f)
			{
				minHeight = 32f;
			}
			if (num != 0f)
			{
				minHeight = num;
			}
			if (num2 != 0f)
			{
				maxHeight = num2;
				stretchHeight = 0;
			}
		}
	}

	public override void SetVertical(float y, float height)
	{
		float num = height;
		if (needsHorizontalScrollbar)
		{
			num -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
		}
		if (allowVerticalScroll && num < calcMinHeight)
		{
			if (!needsHorizontalScrollbar && !needsVerticalScrollbar)
			{
				clientWidth = rect.width - verticalScrollbar.fixedWidth - (float)verticalScrollbar.margin.left;
				if (clientWidth < calcMinWidth)
				{
					clientWidth = calcMinWidth;
				}
				float width = rect.width;
				SetHorizontal(rect.x, clientWidth);
				CalcHeight();
				rect.width = width;
			}
			float num2 = minHeight;
			float num3 = maxHeight;
			minHeight = calcMinHeight;
			maxHeight = calcMaxHeight;
			base.SetVertical(y, calcMinHeight);
			minHeight = num2;
			maxHeight = num3;
			rect.height = height;
			clientHeight = calcMinHeight;
		}
		else
		{
			if (allowVerticalScroll)
			{
				minHeight = calcMinHeight;
				maxHeight = calcMaxHeight;
			}
			base.SetVertical(y, num);
			rect.height = height;
			clientHeight = num;
		}
	}
}
[NativeHeader("Modules/IMGUI/GUIState.h")]
internal class ObjectGUIState : IDisposable
{
	internal IntPtr m_Ptr;

	public ObjectGUIState()
	{
		m_Ptr = Internal_Create();
	}

	public void Dispose()
	{
		Destroy();
		GC.SuppressFinalize(this);
	}

	~ObjectGUIState()
	{
		Destroy();
	}

	private void Destroy()
	{
		if (m_Ptr != IntPtr.Zero)
		{
			Internal_Destroy(m_Ptr);
			m_Ptr = IntPtr.Zero;
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern IntPtr Internal_Create();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsThreadSafe = true)]
	private static extern void Internal_Destroy(IntPtr ptr);
}
internal class ScrollViewState
{
	public Rect position;

	public Rect visibleRect;

	public Rect viewRect;

	public Vector2 scrollPosition;

	public bool apply;

	public bool isDuringTouchScroll;

	public Vector2 touchScrollStartMousePosition;

	public Vector2 touchScrollStartPosition;

	public Vector2 velocity;

	public float previousTimeSinceStartup;

	[RequiredByNativeCode]
	public ScrollViewState()
	{
	}

	public void ScrollTo(Rect pos)
	{
		ScrollTowards(pos, float.PositiveInfinity);
	}

	public bool ScrollTowards(Rect pos, float maxDelta)
	{
		Vector2 vector = ScrollNeeded(pos);
		if (vector.sqrMagnitude < 0.0001f)
		{
			return false;
		}
		if (maxDelta == 0f)
		{
			return true;
		}
		if (vector.magnitude > maxDelta)
		{
			vector = vector.normalized * maxDelta;
		}
		scrollPosition += vector;
		apply = true;
		return true;
	}

	private Vector2 ScrollNeeded(Rect pos)
	{
		Rect rect = visibleRect;
		rect.x += scrollPosition.x;
		rect.y += scrollPosition.y;
		float num = pos.width - visibleRect.width;
		if (num > 0f)
		{
			pos.width -= num;
			pos.x += num * 0.5f;
		}
		num = pos.height - visibleRect.height;
		if (num > 0f)
		{
			pos.height -= num;
			pos.y += num * 0.5f;
		}
		Vector2 zero = Vector2.zero;
		if (pos.xMax > rect.xMax)
		{
			zero.x += pos.xMax - rect.xMax;
		}
		else if (pos.xMin < rect.xMin)
		{
			zero.x -= rect.xMin - pos.xMin;
		}
		if (pos.yMax > rect.yMax)
		{
			zero.y += pos.yMax - rect.yMax;
		}
		else if (pos.yMin < rect.yMin)
		{
			zero.y -= rect.yMin - pos.yMin;
		}
		Rect rect2 = viewRect;
		rect2.width = Mathf.Max(rect2.width, visibleRect.width);
		rect2.height = Mathf.Max(rect2.height, visibleRect.height);
		zero.x = Mathf.Clamp(zero.x, rect2.xMin - scrollPosition.x, rect2.xMax - visibleRect.width - scrollPosition.x);
		zero.y = Mathf.Clamp(zero.y, rect2.yMin - scrollPosition.y, rect2.yMax - visibleRect.height - scrollPosition.y);
		return zero;
	}
}
internal class SliderState
{
	public float dragStartPos;

	public float dragStartValue;

	public bool isDragging;

	[RequiredByNativeCode]
	public SliderState()
	{
	}
}
internal struct SliderHandler
{
	private readonly Rect position;

	private readonly float currentValue;

	private readonly float size;

	private readonly float start;

	private readonly float end;

	private readonly GUIStyle slider;

	private readonly GUIStyle thumb;

	private readonly GUIStyle thumbExtent;

	private readonly bool horiz;

	private readonly int id;

	public SliderHandler(Rect position, float currentValue, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id, GUIStyle thumbExtent = null)
	{
		this.position = position;
		this.currentValue = currentValue;
		this.size = size;
		this.start = start;
		this.end = end;
		this.slider = slider;
		this.thumb = thumb;
		this.thumbExtent = thumbExtent;
		this.horiz = horiz;
		this.id = id;
	}

	public float Handle()
	{
		if (slider == null || thumb == null)
		{
			return currentValue;
		}
		return CurrentEventType() switch
		{
			EventType.MouseDown => OnMouseDown(), 
			EventType.MouseDrag => OnMouseDrag(), 
			EventType.MouseUp => OnMouseUp(), 
			EventType.Repaint => OnRepaint(), 
			_ => currentValue, 
		};
	}

	private float OnMouseDown()
	{
		Rect rect = ThumbSelectionRect();
		bool flag = GUIUtility.HitTest(rect, CurrentEvent());
		Rect zero = Rect.zero;
		zero.xMin = Math.Min(position.xMin, rect.xMin);
		zero.xMax = Math.Max(position.xMax, rect.xMax);
		zero.yMin = Math.Min(position.yMin, rect.yMin);
		zero.yMax = Math.Max(position.yMax, rect.yMax);
		if (IsEmptySlider() || (!GUIUtility.HitTest(zero, CurrentEvent()) && !flag))
		{
			return currentValue;
		}
		GUI.scrollTroughSide = 0;
		GUIUtility.hotControl = id;
		CurrentEvent().Use();
		if (flag)
		{
			StartDraggingWithValue(ClampedCurrentValue());
			return currentValue;
		}
		GUI.changed = true;
		if (SupportsPageMovements())
		{
			SliderState().isDragging = false;
			GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(250.0);
			GUI.scrollTroughSide = CurrentScrollTroughSide();
			return PageMovementValue();
		}
		float num = ValueForCurrentMousePosition();
		StartDraggingWithValue(num);
		return Clamp(num);
	}

	private float OnMouseDrag()
	{
		if (GUIUtility.hotControl != id)
		{
			return currentValue;
		}
		SliderState sliderState = SliderState();
		if (!sliderState.isDragging)
		{
			return currentValue;
		}
		GUI.changed = true;
		CurrentEvent().Use();
		float num = MousePosition() - sliderState.dragStartPos;
		float value = sliderState.dragStartValue + num / ValuesPerPixel();
		return Clamp(value);
	}

	private float OnMouseUp()
	{
		if (GUIUtility.hotControl == id)
		{
			CurrentEvent().Use();
			GUIUtility.hotControl = 0;
		}
		return currentValue;
	}

	private float OnRepaint()
	{
		bool flag = GUIUtility.HitTest(position, CurrentEvent());
		slider.Draw(position, GUIContent.none, id, on: false, flag);
		if (currentValue >= Mathf.Min(start, end) && currentValue <= Mathf.Max(start, end))
		{
			if (thumbExtent != null)
			{
				thumbExtent.Draw(ThumbExtRect(), GUIContent.none, id, on: false, flag);
			}
			thumb.Draw(ThumbRect(), GUIContent.none, id, on: false, flag);
		}
		if (GUIUtility.hotControl != id || !flag || IsEmptySlider())
		{
			return currentValue;
		}
		Rect rect = ThumbRect();
		if (horiz)
		{
			rect.y = position.y;
			rect.height = position.height;
		}
		else
		{
			rect.x = position.x;
			rect.width = position.width;
		}
		if (GUIUtility.HitTest(rect, CurrentEvent()))
		{
			if (GUI.scrollTroughSide != 0)
			{
				GUIUtility.hotControl = 0;
			}
			return currentValue;
		}
		GUI.InternalRepaintEditorWindow();
		if (SystemClock.now < GUI.nextScrollStepTime)
		{
			return currentValue;
		}
		if (CurrentScrollTroughSide() != GUI.scrollTroughSide)
		{
			return currentValue;
		}
		GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(30.0);
		if (SupportsPageMovements())
		{
			SliderState().isDragging = false;
			GUI.changed = true;
			return PageMovementValue();
		}
		return ClampedCurrentValue();
	}

	private EventType CurrentEventType()
	{
		return CurrentEvent().GetTypeForControl(id);
	}

	private int CurrentScrollTroughSide()
	{
		float num = (horiz ? CurrentEvent().mousePosition.x : CurrentEvent().mousePosition.y);
		float num2 = (horiz ? ThumbRect().x : ThumbRect().y);
		return (num > num2) ? 1 : (-1);
	}

	private bool IsEmptySlider()
	{
		return start == end;
	}

	private bool SupportsPageMovements()
	{
		return size != 0f && GUI.usePageScrollbars;
	}

	private float PageMovementValue()
	{
		float num = currentValue;
		int num2 = ((!(start > end)) ? 1 : (-1));
		num = ((!(MousePosition() > PageUpMovementBound())) ? (num - size * (float)num2 * 0.9f) : (num + size * (float)num2 * 0.9f));
		return Clamp(num);
	}

	private float PageUpMovementBound()
	{
		if (horiz)
		{
			return ThumbRect().xMax - position.x;
		}
		return ThumbRect().yMax - position.y;
	}

	private Event CurrentEvent()
	{
		return Event.current;
	}

	private float ValueForCurrentMousePosition()
	{
		if (horiz)
		{
			return (MousePosition() - ThumbRect().width * 0.5f) / ValuesPerPixel() + start - size * 0.5f;
		}
		return (MousePosition() - ThumbRect().height * 0.5f) / ValuesPerPixel() + start - size * 0.5f;
	}

	private float Clamp(float value)
	{
		return Mathf.Clamp(value, MinValue(), MaxValue());
	}

	private Rect ThumbSelectionRect()
	{
		return ThumbRect();
	}

	private void StartDraggingWithValue(float dragStartValue)
	{
		SliderState sliderState = SliderState();
		sliderState.dragStartPos = MousePosition();
		sliderState.dragStartValue = dragStartValue;
		sliderState.isDragging = true;
	}

	private SliderState SliderState()
	{
		return (SliderState)GUIUtility.GetStateObject(typeof(SliderState), id);
	}

	private Rect ThumbExtRect()
	{
		Rect result = new Rect(0f, 0f, thumbExtent.fixedWidth, thumbExtent.fixedHeight);
		result.center = ThumbRect().center;
		return result;
	}

	private Rect ThumbRect()
	{
		return horiz ? HorizontalThumbRect() : VerticalThumbRect();
	}

	private Rect VerticalThumbRect()
	{
		Rect rect = thumb.margin.Remove(slider.padding.Remove(position));
		float width = ((thumb.fixedWidth != 0f) ? thumb.fixedWidth : rect.width);
		float num = ThumbSize();
		float num2 = ValuesPerPixel();
		if (start < end)
		{
			return new Rect(rect.x, (ClampedCurrentValue() - start) * num2 + rect.y, width, size * num2 + num);
		}
		return new Rect(rect.x, (ClampedCurrentValue() + size - start) * num2 + rect.y, width, size * (0f - num2) + num);
	}

	private Rect HorizontalThumbRect()
	{
		Rect rect = thumb.margin.Remove(slider.padding.Remove(position));
		float height = ((thumb.fixedHeight != 0f) ? thumb.fixedHeight : rect.height);
		float num = ThumbSize();
		float num2 = ValuesPerPixel();
		if (start < end)
		{
			return new Rect((ClampedCurrentValue() - start) * num2 + rect.x, rect.y, size * num2 + num, height);
		}
		return new Rect((ClampedCurrentValue() + size - start) * num2 + rect.x, rect.y, size * (0f - num2) + num, height);
	}

	private float ClampedCurrentValue()
	{
		return Clamp(currentValue);
	}

	private float MousePosition()
	{
		if (horiz)
		{
			return CurrentEvent().mousePosition.x - position.x;
		}
		return CurrentEvent().mousePosition.y - position.y;
	}

	private float ValuesPerPixel()
	{
		float num = ((end == start) ? 1f : (end - start));
		if (horiz)
		{
			return (position.width - (float)slider.padding.horizontal - ThumbSize()) / num;
		}
		return (position.height - (float)slider.padding.vertical - ThumbSize()) / num;
	}

	private float ThumbSize()
	{
		if (horiz)
		{
			return (thumb.fixedWidth != 0f) ? thumb.fixedWidth : ((float)thumb.padding.horizontal);
		}
		return (thumb.fixedHeight != 0f) ? thumb.fixedHeight : ((float)thumb.padding.vertical);
	}

	private float MaxValue()
	{
		return Mathf.Max(start, end) - size;
	}

	private float MinValue()
	{
		return Mathf.Min(start, end);
	}
}
internal enum TextEditOp
{
	MoveLeft,
	MoveRight,
	MoveUp,
	MoveDown,
	MoveLineStart,
	MoveLineEnd,
	MoveTextStart,
	MoveTextEnd,
	MovePageUp,
	MovePageDown,
	MoveGraphicalLineStart,
	MoveGraphicalLineEnd,
	MoveWordLeft,
	MoveWordRight,
	MoveParagraphForward,
	MoveParagraphBackward,
	MoveToStartOfNextWord,
	MoveToEndOfPreviousWord,
	Delete,
	Backspace,
	DeleteWordBack,
	DeleteWordForward,
	DeleteLineBack,
	Cut,
	Paste,
	ScrollStart,
	ScrollEnd,
	ScrollPageUp,
	ScrollPageDown
}
internal enum TextSelectOp
{
	SelectLeft,
	SelectRight,
	SelectUp,
	SelectDown,
	SelectTextStart,
	SelectTextEnd,
	SelectPageUp,
	SelectPageDown,
	ExpandSelectGraphicalLineStart,
	ExpandSelectGraphicalLineEnd,
	SelectGraphicalLineStart,
	SelectGraphicalLineEnd,
	SelectWordLeft,
	SelectWordRight,
	SelectToEndOfPreviousWord,
	SelectToStartOfNextWord,
	SelectParagraphBackward,
	SelectParagraphForward,
	Copy,
	SelectAll,
	SelectNone
}
internal class TextEditingUtilities
{
	private TextSelectingUtilities m_TextSelectingUtility;

	private TextHandle m_TextHandle;

	private int m_CursorIndexSavedState = -1;

	internal bool isCompositionActive;

	private bool m_UpdateImeWindowPosition;

	public bool multiline = false;

	private string m_Text;

	private static Dictionary<Event, TextEditOp> s_KeyEditOps;

	private bool hasSelection => m_TextSelectingUtility.hasSelection;

	private string SelectedText => m_TextSelectingUtility.selectedText;

	private int m_iAltCursorPos => m_TextSelectingUtility.iAltCursorPos;

	internal bool revealCursor
	{
		get
		{
			return m_TextSelectingUtility.revealCursor;
		}
		set
		{
			m_TextSelectingUtility.revealCursor = value;
		}
	}

	private int cursorIndex
	{
		get
		{
			return m_TextSelectingUtility.cursorIndex;
		}
		set
		{
			m_TextSelectingUtility.cursorIndex = value;
		}
	}

	private int selectIndex
	{
		get
		{
			return m_TextSelectingUtility.selectIndex;
		}
		set
		{
			m_TextSelectingUtility.selectIndex = value;
		}
	}

	public string text
	{
		get
		{
			return m_Text;
		}
		set
		{
			if (!(value == m_Text))
			{
				m_Text = value ?? string.Empty;
			}
		}
	}

	public TextEditingUtilities(TextSelectingUtilities selectingUtilities, TextHandle textHandle, string text)
	{
		m_TextSelectingUtility = selectingUtilities;
		m_TextHandle = textHandle;
		m_Text = text;
	}

	public bool UpdateImeState()
	{
		if (GUIUtility.compositionString.Length > 0)
		{
			if (!isCompositionActive)
			{
				m_UpdateImeWindowPosition = true;
				ReplaceSelection(string.Empty);
			}
			isCompositionActive = true;
		}
		else
		{
			isCompositionActive = false;
		}
		return isCompositionActive;
	}

	public bool ShouldUpdateImeWindowPosition()
	{
		return m_UpdateImeWindowPosition;
	}

	public void SetImeWindowPosition(Vector2 worldPosition)
	{
		Vector2 cursorPositionFromStringIndexUsingCharacterHeight = m_TextHandle.GetCursorPositionFromStringIndexUsingCharacterHeight(cursorIndex);
		GUIUtility.compositionCursorPos = worldPosition + cursorPositionFromStringIndexUsingCharacterHeight;
	}

	public string GeneratePreviewString(bool richText)
	{
		RestoreCursorState();
		string compositionString = GUIUtility.compositionString;
		if (isCompositionActive)
		{
			return richText ? text.Insert(cursorIndex, "<u>" + compositionString + "</u>") : text.Insert(cursorIndex, compositionString);
		}
		return text;
	}

	public void EnableCursorPreviewState()
	{
		if (m_CursorIndexSavedState == -1)
		{
			m_CursorIndexSavedState = m_TextSelectingUtility.cursorIndex;
			int num = (selectIndex = m_CursorIndexSavedState + GUIUtility.compositionString.Length);
			cursorIndex = num;
		}
	}

	public void RestoreCursorState()
	{
		if (m_CursorIndexSavedState != -1)
		{
			int num = (selectIndex = m_CursorIndexSavedState);
			cursorIndex = num;
			m_CursorIndexSavedState = -1;
		}
	}

	[VisibleToOtherModules]
	internal bool HandleKeyEvent(Event e)
	{
		RestoreCursorState();
		InitKeyActions();
		EventModifiers modifiers = e.modifiers;
		e.modifiers &= ~EventModifiers.CapsLock;
		if (s_KeyEditOps.ContainsKey(e))
		{
			TextEditOp operation = s_KeyEditOps[e];
			PerformOperation(operation);
			e.modifiers = modifiers;
			return true;
		}
		e.modifiers = modifiers;
		return false;
	}

	private void PerformOperation(TextEditOp operation)
	{
		revealCursor = true;
		switch (operation)
		{
		case TextEditOp.MoveLeft:
			m_TextSelectingUtility.MoveLeft();
			break;
		case TextEditOp.MoveRight:
			m_TextSelectingUtility.MoveRight();
			break;
		case TextEditOp.MoveUp:
			m_TextSelectingUtility.MoveUp();
			break;
		case TextEditOp.MoveDown:
			m_TextSelectingUtility.MoveDown();
			break;
		case TextEditOp.MoveLineStart:
			m_TextSelectingUtility.MoveLineStart();
			break;
		case TextEditOp.MoveLineEnd:
			m_TextSelectingUtility.MoveLineEnd();
			break;
		case TextEditOp.MoveWordRight:
			m_TextSelectingUtility.MoveWordRight();
			break;
		case TextEditOp.MoveToStartOfNextWord:
			m_TextSelectingUtility.MoveToStartOfNextWord();
			break;
		case TextEditOp.MoveToEndOfPreviousWord:
			m_TextSelectingUtility.MoveToEndOfPreviousWord();
			break;
		case TextEditOp.MoveWordLeft:
			m_TextSelectingUtility.MoveWordLeft();
			break;
		case TextEditOp.MoveTextStart:
			m_TextSelectingUtility.MoveTextStart();
			break;
		case TextEditOp.MoveTextEnd:
			m_TextSelectingUtility.MoveTextEnd();
			break;
		case TextEditOp.MoveParagraphForward:
			m_TextSelectingUtility.MoveParagraphForward();
			break;
		case TextEditOp.MoveParagraphBackward:
			m_TextSelectingUtility.MoveParagraphBackward();
			break;
		case TextEditOp.MoveGraphicalLineStart:
			m_TextSelectingUtility.MoveGraphicalLineStart();
			break;
		case TextEditOp.MoveGraphicalLineEnd:
			m_TextSelectingUtility.MoveGraphicalLineEnd();
			break;
		case TextEditOp.Delete:
			Delete();
			break;
		case TextEditOp.Backspace:
			Backspace();
			break;
		case TextEditOp.Cut:
			Cut();
			break;
		case TextEditOp.Paste:
			Paste();
			break;
		case TextEditOp.DeleteWordBack:
			DeleteWordBack();
			break;
		case TextEditOp.DeleteLineBack:
			DeleteLineBack();
			break;
		case TextEditOp.DeleteWordForward:
			DeleteWordForward();
			break;
		default:
			Debug.Log("Unimplemented: " + operation);
			break;
		}
	}

	private static void MapKey(string key, TextEditOp action)
	{
		s_KeyEditOps[Event.KeyboardEvent(key)] = action;
	}

	private void InitKeyActions()
	{
		if (s_KeyEditOps == null)
		{
			s_KeyEditOps = new Dictionary<Event, TextEditOp>();
			MapKey("left", TextEditOp.MoveLeft);
			MapKey("right", TextEditOp.MoveRight);
			MapKey("up", TextEditOp.MoveUp);
			MapKey("down", TextEditOp.MoveDown);
			MapKey("delete", TextEditOp.Delete);
			MapKey("backspace", TextEditOp.Backspace);
			MapKey("#backspace", TextEditOp.Backspace);
			if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX)
			{
				MapKey("^left", TextEditOp.MoveGraphicalLineStart);
				MapKey("^right", TextEditOp.MoveGraphicalLineEnd);
				MapKey("&left", TextEditOp.MoveWordLeft);
				MapKey("&right", TextEditOp.MoveWordRight);
				MapKey("&up", TextEditOp.MoveParagraphBackward);
				MapKey("&down", TextEditOp.MoveParagraphForward);
				MapKey("%left", TextEditOp.MoveGraphicalLineStart);
				MapKey("%right", TextEditOp.MoveGraphicalLineEnd);
				MapKey("%up", TextEditOp.MoveTextStart);
				MapKey("%down", TextEditOp.MoveTextEnd);
				MapKey("%x", TextEditOp.Cut);
				MapKey("%v", TextEditOp.Paste);
				MapKey("^d", TextEditOp.Delete);
				MapKey("^h", TextEditOp.Backspace);
				MapKey("^b", TextEditOp.MoveLeft);
				MapKey("^f", TextEditOp.MoveRight);
				MapKey("^a", TextEditOp.MoveLineStart);
				MapKey("^e", TextEditOp.MoveLineEnd);
				MapKey("&delete", TextEditOp.DeleteWordForward);
				MapKey("&backspace", TextEditOp.DeleteWordBack);
				MapKey("%backspace", TextEditOp.DeleteLineBack);
			}
			else
			{
				MapKey("home", TextEditOp.MoveGraphicalLineStart);
				MapKey("end", TextEditOp.MoveGraphicalLineEnd);
				MapKey("%left", TextEditOp.MoveWordLeft);
				MapKey("%right", TextEditOp.MoveWordRight);
				MapKey("%up", TextEditOp.MoveParagraphBackward);
				MapKey("%down", TextEditOp.MoveParagraphForward);
				MapKey("^left", TextEditOp.MoveToEndOfPreviousWord);
				MapKey("^right", TextEditOp.MoveToStartOfNextWord);
				MapKey("^up", TextEditOp.MoveParagraphBackward);
				MapKey("^down", TextEditOp.MoveParagraphForward);
				MapKey("^delete", TextEditOp.DeleteWordForward);
				MapKey("^backspace", TextEditOp.DeleteWordBack);
				MapKey("%backspace", TextEditOp.DeleteLineBack);
				MapKey("^x", TextEditOp.Cut);
				MapKey("^v", TextEditOp.Paste);
				MapKey("#delete", TextEditOp.Cut);
				MapKey("#insert", TextEditOp.Paste);
			}
		}
	}

	public bool DeleteLineBack()
	{
		RestoreCursorState();
		if (hasSelection)
		{
			DeleteSelection();
			return true;
		}
		int num = cursorIndex;
		int num2 = num;
		while (num2-- != 0)
		{
			if (text[num2] == '\n')
			{
				num = num2 + 1;
				break;
			}
		}
		if (num2 == -1)
		{
			num = 0;
		}
		if (cursorIndex != num)
		{
			text = text.Remove(num, cursorIndex - num);
			TextSelectingUtilities textSelectingUtility = m_TextSelectingUtility;
			int num3 = (cursorIndex = num);
			textSelectingUtility.selectIndex = num3;
			return true;
		}
		return false;
	}

	public bool DeleteWordBack()
	{
		RestoreCursorState();
		if (hasSelection)
		{
			DeleteSelection();
			return true;
		}
		int num = m_TextSelectingUtility.FindEndOfPreviousWord(cursorIndex);
		if (cursorIndex != num)
		{
			text = text.Remove(num, cursorIndex - num);
			int num2 = (cursorIndex = num);
			selectIndex = num2;
			return true;
		}
		return false;
	}

	public bool DeleteWordForward()
	{
		RestoreCursorState();
		if (hasSelection)
		{
			DeleteSelection();
			return true;
		}
		int num = m_TextSelectingUtility.FindStartOfNextWord(cursorIndex);
		if (cursorIndex < text.Length)
		{
			text = text.Remove(cursorIndex, num - cursorIndex);
			return true;
		}
		return false;
	}

	public bool Delete()
	{
		RestoreCursorState();
		if (hasSelection)
		{
			DeleteSelection();
			return true;
		}
		if (cursorIndex < text.Length)
		{
			text = text.Remove(cursorIndex, m_TextSelectingUtility.NextCodePointIndex(cursorIndex) - cursorIndex);
			return true;
		}
		return false;
	}

	public bool Backspace()
	{
		RestoreCursorState();
		if (hasSelection)
		{
			DeleteSelection();
			return true;
		}
		if (cursorIndex > 0)
		{
			int num = m_TextSelectingUtility.PreviousCodePointIndex(cursorIndex);
			text = text.Remove(num, cursorIndex - num);
			m_TextSelectingUtility.SetCursorIndexWithoutNotify(num);
			m_TextSelectingUtility.SetSelectIndexWithoutNotify(num);
			m_TextSelectingUtility.ClearCursorPos();
			return true;
		}
		return false;
	}

	public bool DeleteSelection()
	{
		if (cursorIndex == selectIndex)
		{
			return false;
		}
		if (cursorIndex < selectIndex)
		{
			text = text.Substring(0, cursorIndex) + text.Substring(selectIndex, text.Length - selectIndex);
			m_TextSelectingUtility.SetSelectIndexWithoutNotify(cursorIndex);
		}
		else
		{
			text = text.Substring(0, selectIndex) + text.Substring(cursorIndex, text.Length - cursorIndex);
			m_TextSelectingUtility.SetCursorIndexWithoutNotify(selectIndex);
		}
		m_TextSelectingUtility.ClearCursorPos();
		return true;
	}

	public void ReplaceSelection(string replace)
	{
		RestoreCursorState();
		DeleteSelection();
		text = text.Insert(cursorIndex, replace);
		int num = cursorIndex + replace.Length;
		m_TextSelectingUtility.SetCursorIndexWithoutNotify(num);
		m_TextSelectingUtility.SetSelectIndexWithoutNotify(num);
		m_TextSelectingUtility.ClearCursorPos();
	}

	public void Insert(char c)
	{
		ReplaceSelection(c.ToString());
	}

	public void MoveSelectionToAltCursor()
	{
		RestoreCursorState();
		if (m_iAltCursorPos != -1)
		{
			int iAltCursorPos = m_iAltCursorPos;
			string selectedText = SelectedText;
			text = text.Insert(iAltCursorPos, selectedText);
			if (iAltCursorPos < cursorIndex)
			{
				cursorIndex += selectedText.Length;
				selectIndex += selectedText.Length;
			}
			DeleteSelection();
			int num = (cursorIndex = iAltCursorPos);
			selectIndex = num;
			m_TextSelectingUtility.ClearCursorPos();
		}
	}

	public bool CanPaste()
	{
		return GUIUtility.systemCopyBuffer.Length != 0;
	}

	public bool Cut()
	{
		m_TextSelectingUtility.Copy();
		return DeleteSelection();
	}

	public bool Paste()
	{
		RestoreCursorState();
		string text = GUIUtility.systemCopyBuffer;
		if (text != "")
		{
			if (!multiline)
			{
				text = ReplaceNewlinesWithSpaces(text);
			}
			ReplaceSelection(text);
			return true;
		}
		return false;
	}

	private static string ReplaceNewlinesWithSpaces(string value)
	{
		value = value.Replace("\r\n", " ");
		value = value.Replace('\n', ' ');
		value = value.Replace('\r', ' ');
		return value;
	}

	internal void OnBlur()
	{
		revealCursor = false;
		m_TextSelectingUtility.SelectNone();
	}

	internal bool TouchScreenKeyboardShouldBeUsed()
	{
		RuntimePlatform platform = Application.platform;
		RuntimePlatform runtimePlatform = platform;
		RuntimePlatform runtimePlatform2 = runtimePlatform;
		if (runtimePlatform2 == RuntimePlatform.Android || runtimePlatform2 == RuntimePlatform.WebGLPlayer)
		{
			return !TouchScreenKeyboard.isInPlaceEditingAllowed;
		}
		return TouchScreenKeyboard.isSupported;
	}
}
public class TextEditor
{
	public enum DblClickSnapping : byte
	{
		WORDS,
		PARAGRAPHS
	}

	private enum CharacterType
	{
		LetterLike,
		Symbol,
		Symbol2,
		WhiteSpace
	}

	private enum Direction
	{
		Forward,
		Backward
	}

	private enum TextEditOp
	{
		MoveLeft,
		MoveRight,
		MoveUp,
		MoveDown,
		MoveLineStart,
		MoveLineEnd,
		MoveTextStart,
		MoveTextEnd,
		MovePageUp,
		MovePageDown,
		MoveGraphicalLineStart,
		MoveGraphicalLineEnd,
		MoveWordLeft,
		MoveWordRight,
		MoveParagraphForward,
		MoveParagraphBackward,
		MoveToStartOfNextWord,
		MoveToEndOfPreviousWord,
		SelectLeft,
		SelectRight,
		SelectUp,
		SelectDown,
		SelectTextStart,
		SelectTextEnd,
		SelectPageUp,
		SelectPageDown,
		ExpandSelectGraphicalLineStart,
		ExpandSelectGraphicalLineEnd,
		SelectGraphicalLineStart,
		SelectGraphicalLineEnd,
		SelectWordLeft,
		SelectWordRight,
		SelectToEndOfPreviousWord,
		SelectToStartOfNextWord,
		SelectParagraphBackward,
		SelectParagraphForward,
		Delete,
		Backspace,
		DeleteWordBack,
		DeleteWordForward,
		DeleteLineBack,
		Cut,
		Copy,
		Paste,
		SelectAll,
		SelectNone,
		ScrollStart,
		ScrollEnd,
		ScrollPageUp,
		ScrollPageDown
	}

	public TouchScreenKeyboard keyboardOnScreen = null;

	public int controlID = 0;

	public GUIStyle style = GUIStyle.none;

	public bool multiline = false;

	public bool hasHorizontalCursorPos = false;

	public bool isPasswordField = false;

	internal bool m_HasFocus;

	public Vector2 scrollOffset = Vector2.zero;

	private GUIContent m_Content = new GUIContent();

	private Rect m_Position;

	private int m_CursorIndex = 0;

	private int m_SelectIndex = 0;

	private bool m_RevealCursor = false;

	public Vector2 graphicalCursorPos;

	public Vector2 graphicalSelectCursorPos;

	private bool m_MouseDragSelectsWholeWords = false;

	private int m_DblClickInitPos = 0;

	private DblClickSnapping m_DblClickSnap = DblClickSnapping.WORDS;

	private bool m_bJustSelected = false;

	private int m_iAltCursorPos = -1;

	private string oldText;

	private int oldPos;

	private int oldSelectPos;

	private static Dictionary<Event, TextEditOp> s_Keyactions;

	[Obsolete("Please use 'text' instead of 'content'", false)]
	public GUIContent content
	{
		get
		{
			return m_Content;
		}
		set
		{
			m_Content = value;
		}
	}

	public string text
	{
		get
		{
			return m_Content.text;
		}
		set
		{
			m_Content.text = value ?? string.Empty;
			EnsureValidCodePointIndex(ref m_CursorIndex);
			EnsureValidCodePointIndex(ref m_SelectIndex);
		}
	}

	public Rect position
	{
		get
		{
			return m_Position;
		}
		set
		{
			if (!(m_Position == value))
			{
				scrollOffset = Vector2.zero;
				m_Position = value;
				UpdateScrollOffset();
			}
		}
	}

	internal virtual Rect localPosition => position;

	public int cursorIndex
	{
		get
		{
			return m_CursorIndex;
		}
		set
		{
			int num = m_CursorIndex;
			m_CursorIndex = value;
			EnsureValidCodePointIndex(ref m_CursorIndex);
			if (m_CursorIndex != num)
			{
				m_RevealCursor = true;
				OnCursorIndexChange();
			}
		}
	}

	public int selectIndex
	{
		get
		{
			return m_SelectIndex;
		}
		set
		{
			int num = m_SelectIndex;
			m_SelectIndex = value;
			EnsureValidCodePointIndex(ref m_SelectIndex);
			if (m_SelectIndex != num)
			{
				OnSelectIndexChange();
			}
		}
	}

	public DblClickSnapping doubleClickSnapping
	{
		get
		{
			return m_DblClickSnap;
		}
		set
		{
			m_DblClickSnap = value;
		}
	}

	public int altCursorPosition
	{
		get
		{
			return m_iAltCursorPos;
		}
		set
		{
			m_iAltCursorPos = value;
		}
	}

	public bool hasSelection => cursorIndex != selectIndex;

	public string SelectedText
	{
		get
		{
			if (cursorIndex == selectIndex)
			{
				return "";
			}
			if (cursorIndex < selectIndex)
			{
				return text.Substring(cursorIndex, selectIndex - cursorIndex);
			}
			return text.Substring(selectIndex, cursorIndex - selectIndex);
		}
	}

	private void ClearCursorPos()
	{
		hasHorizontalCursorPos = false;
		m_iAltCursorPos = -1;
	}

	[RequiredByNativeCode]
	public TextEditor()
	{
	}

	public void OnFocus()
	{
		if (multiline)
		{
			int num = (selectIndex = 0);
			cursorIndex = num;
		}
		else
		{
			SelectAll();
		}
		m_HasFocus = true;
	}

	public void OnLostFocus()
	{
		m_HasFocus = false;
		scrollOffset = Vector2.zero;
	}

	private void GrabGraphicalCursorPos()
	{
		if (!hasHorizontalCursorPos)
		{
			graphicalCursorPos = style.GetCursorPixelPosition(localPosition, m_Content, cursorIndex);
			graphicalSelectCursorPos = style.GetCursorPixelPosition(localPosition, m_Content, selectIndex);
			hasHorizontalCursorPos = false;
		}
	}

	public bool HandleKeyEvent(Event e)
	{
		return HandleKeyEvent(e, textIsReadOnly: false);
	}

	[VisibleToOtherModules]
	internal bool HandleKeyEvent(Event e, bool textIsReadOnly)
	{
		InitKeyActions();
		EventModifiers modifiers = e.modifiers;
		e.modifiers &= ~EventModifiers.CapsLock;
		if (s_Keyactions.ContainsKey(e))
		{
			TextEditOp operation = s_Keyactions[e];
			PerformOperation(operation, textIsReadOnly);
			e.modifiers = modifiers;
			return true;
		}
		e.modifiers = modifiers;
		return false;
	}

	public bool DeleteLineBack()
	{
		if (hasSelection)
		{
			DeleteSelection();
			return true;
		}
		int num = cursorIndex;
		int num2 = num;
		while (num2-- != 0)
		{
			if (text[num2] == '\n')
			{
				num = num2 + 1;
				break;
			}
		}
		if (num2 == -1)
		{
			num = 0;
		}
		if (cursorIndex != num)
		{
			m_Content.text = text.Remove(num, cursorIndex - num);
			int num3 = (cursorIndex = num);
			selectIndex = num3;
			return true;
		}
		return false;
	}

	public bool DeleteWordBack()
	{
		if (hasSelection)
		{
			DeleteSelection();
			return true;
		}
		int num = FindEndOfPreviousWord(cursorIndex);
		if (cursorIndex != num)
		{
			m_Content.text = text.Remove(num, cursorIndex - num);
			int num2 = (cursorIndex = num);
			selectIndex = num2;
			return true;
		}
		return false;
	}

	public bool DeleteWordForward()
	{
		if (hasSelection)
		{
			DeleteSelection();
			return true;
		}
		int num = FindStartOfNextWord(cursorIndex);
		if (cursorIndex < text.Length)
		{
			m_Content.text = text.Remove(cursorIndex, num - cursorIndex);
			return true;
		}
		return false;
	}

	public bool Delete()
	{
		if (hasSelection)
		{
			DeleteSelection();
			return true;
		}
		if (cursorIndex < text.Length)
		{
			m_Content.text = text.Remove(cursorIndex, NextCodePointIndex(cursorIndex) - cursorIndex);
			return true;
		}
		return false;
	}

	public bool CanPaste()
	{
		return GUIUtility.systemCopyBuffer.Length != 0;
	}

	public bool Backspace()
	{
		if (hasSelection)
		{
			DeleteSelection();
			return true;
		}
		if (cursorIndex > 0)
		{
			int num = PreviousCodePointIndex(cursorIndex);
			m_Content.text = text.Remove(num, cursorIndex - num);
			int num2 = (cursorIndex = num);
			selectIndex = num2;
			ClearCursorPos();
			return true;
		}
		return false;
	}

	public void SelectAll()
	{
		cursorIndex = 0;
		selectIndex = text.Length;
		ClearCursorPos();
	}

	public void SelectNone()
	{
		selectIndex = cursorIndex;
		ClearCursorPos();
	}

	public bool DeleteSelection()
	{
		if (cursorIndex == selectIndex)
		{
			return false;
		}
		if (cursorIndex < selectIndex)
		{
			m_Content.text = text.Substring(0, cursorIndex) + text.Substring(selectIndex, text.Length - selectIndex);
			selectIndex = cursorIndex;
		}
		else
		{
			m_Content.text = text.Substring(0, selectIndex) + text.Substring(cursorIndex, text.Length - cursorIndex);
			cursorIndex = selectIndex;
		}
		ClearCursorPos();
		return true;
	}

	public void ReplaceSelection(string replace)
	{
		DeleteSelection();
		m_Content.text = text.Insert(cursorIndex, replace);
		selectIndex = (cursorIndex += replace.Length);
		ClearCursorPos();
	}

	public void Insert(char c)
	{
		ReplaceSelection(c.ToString());
	}

	public void MoveSelectionToAltCursor()
	{
		if (m_iAltCursorPos != -1)
		{
			int iAltCursorPos = m_iAltCursorPos;
			string selectedText = SelectedText;
			m_Content.text = text.Insert(iAltCursorPos, selectedText);
			if (iAltCursorPos < cursorIndex)
			{
				cursorIndex += selectedText.Length;
				selectIndex += selectedText.Length;
			}
			DeleteSelection();
			int num = (cursorIndex = iAltCursorPos);
			selectIndex = num;
			ClearCursorPos();
		}
	}

	public void MoveRight()
	{
		ClearCursorPos();
		if (selectIndex == cursorIndex)
		{
			cursorIndex = NextCodePointIndex(cursorIndex);
			DetectFocusChange();
			selectIndex = cursorIndex;
		}
		else if (selectIndex > cursorIndex)
		{
			cursorIndex = selectIndex;
		}
		else
		{
			selectIndex = cursorIndex;
		}
	}

	public void MoveLeft()
	{
		if (selectIndex == cursorIndex)
		{
			cursorIndex = PreviousCodePointIndex(cursorIndex);
			selectIndex = cursorIndex;
		}
		else if (selectIndex > cursorIndex)
		{
			selectIndex = cursorIndex;
		}
		else
		{
			cursorIndex = selectIndex;
		}
		ClearCursorPos();
	}

	public void MoveUp()
	{
		if (selectIndex < cursorIndex)
		{
			selectIndex = cursorIndex;
		}
		else
		{
			cursorIndex = selectIndex;
		}
		GrabGraphicalCursorPos();
		graphicalCursorPos.y -= 1f;
		int num = (selectIndex = style.GetCursorStringIndex(localPosition, m_Content, graphicalCursorPos));
		cursorIndex = num;
		if (cursorIndex <= 0)
		{
			ClearCursorPos();
		}
	}

	public void MoveDown()
	{
		if (selectIndex > cursorIndex)
		{
			selectIndex = cursorIndex;
		}
		else
		{
			cursorIndex = selectIndex;
		}
		GrabGraphicalCursorPos();
		graphicalCursorPos.y += style.lineHeight + 5f;
		int num = (selectIndex = style.GetCursorStringIndex(localPosition, m_Content, graphicalCursorPos));
		cursorIndex = num;
		if (cursorIndex == text.Length)
		{
			ClearCursorPos();
		}
	}

	public void MoveLineStart()
	{
		int num = ((selectIndex < cursorIndex) ? selectIndex : cursorIndex);
		int num2 = num;
		int num3;
		while (num2-- != 0)
		{
			if (text[num2] == '\n')
			{
				num3 = (cursorIndex = num2 + 1);
				selectIndex = num3;
				return;
			}
		}
		num3 = (cursorIndex = 0);
		selectIndex = num3;
	}

	public void MoveLineEnd()
	{
		int num = ((selectIndex > cursorIndex) ? selectIndex : cursorIndex);
		int i = num;
		int length;
		int num2;
		for (length = text.Length; i < length; i++)
		{
			if (text[i] == '\n')
			{
				num2 = (cursorIndex = i);
				selectIndex = num2;
				return;
			}
		}
		num2 = (cursorIndex = length);
		selectIndex = num2;
	}

	public void MoveGraphicalLineStart()
	{
		int num = (selectIndex = GetGraphicalLineStart((cursorIndex < selectIndex) ? cursorIndex : selectIndex));
		cursorIndex = num;
	}

	public void MoveGraphicalLineEnd()
	{
		int num = (selectIndex = GetGraphicalLineEnd((cursorIndex > selectIndex) ? cursorIndex : selectIndex));
		cursorIndex = num;
	}

	public void MoveTextStart()
	{
		int num = (cursorIndex = 0);
		selectIndex = num;
	}

	public void MoveTextEnd()
	{
		int num = (cursorIndex = text.Length);
		selectIndex = num;
	}

	private int IndexOfEndOfLine(int startIndex)
	{
		int num = text.IndexOf('\n', startIndex);
		return (num != -1) ? num : text.Length;
	}

	public void MoveParagraphForward()
	{
		cursorIndex = ((cursorIndex > selectIndex) ? cursorIndex : selectIndex);
		if (cursorIndex < text.Length)
		{
			int num = (cursorIndex = IndexOfEndOfLine(cursorIndex + 1));
			selectIndex = num;
		}
	}

	public void MoveParagraphBackward()
	{
		cursorIndex = ((cursorIndex < selectIndex) ? cursorIndex : selectIndex);
		if (cursorIndex > 1)
		{
			int num = (cursorIndex = text.LastIndexOf('\n', cursorIndex - 2) + 1);
			selectIndex = num;
		}
		else
		{
			int num = (cursorIndex = 0);
			selectIndex = num;
		}
	}

	public void MoveCursorToPosition(Vector2 cursorPosition)
	{
		MoveCursorToPosition_Internal(cursorPosition, Event.current.shift);
	}

	protected internal void MoveCursorToPosition_Internal(Vector2 cursorPosition, bool shift)
	{
		selectIndex = style.GetCursorStringIndex(localPosition, m_Content, cursorPosition + scrollOffset);
		if (!shift)
		{
			cursorIndex = selectIndex;
		}
		DetectFocusChange();
	}

	public void MoveAltCursorToPosition(Vector2 cursorPosition)
	{
		int cursorStringIndex = style.GetCursorStringIndex(localPosition, m_Content, cursorPosition + scrollOffset);
		m_iAltCursorPos = Mathf.Min(text.Length, cursorStringIndex);
		DetectFocusChange();
	}

	public bool IsOverSelection(Vector2 cursorPosition)
	{
		int cursorStringIndex = style.GetCursorStringIndex(localPosition, m_Content, cursorPosition + scrollOffset);
		return cursorStringIndex < Mathf.Max(cursorIndex, selectIndex) && cursorStringIndex > Mathf.Min(cursorIndex, selectIndex);
	}

	public void SelectToPosition(Vector2 cursorPosition)
	{
		if (!m_MouseDragSelectsWholeWords)
		{
			cursorIndex = style.GetCursorStringIndex(localPosition, m_Content, cursorPosition + scrollOffset);
			return;
		}
		int index = style.GetCursorStringIndex(localPosition, m_Content, cursorPosition + scrollOffset);
		EnsureValidCodePointIndex(ref index);
		EnsureValidCodePointIndex(ref m_DblClickInitPos);
		if (m_DblClickSnap == DblClickSnapping.WORDS)
		{
			if (index < m_DblClickInitPos)
			{
				cursorIndex = FindEndOfClassification(index, Direction.Backward);
				selectIndex = FindEndOfClassification(m_DblClickInitPos, Direction.Forward);
			}
			else
			{
				cursorIndex = FindEndOfClassification(index, Direction.Forward);
				selectIndex = FindEndOfClassification(m_DblClickInitPos, Direction.Backward);
			}
		}
		else if (index < m_DblClickInitPos)
		{
			if (index > 0)
			{
				cursorIndex = text.LastIndexOf('\n', Mathf.Max(0, index - 2)) + 1;
			}
			else
			{
				cursorIndex = 0;
			}
			selectIndex = text.LastIndexOf('\n', Mathf.Min(text.Length - 1, m_DblClickInitPos));
		}
		else
		{
			if (index < text.Length)
			{
				cursorIndex = IndexOfEndOfLine(index);
			}
			else
			{
				cursorIndex = text.Length;
			}
			selectIndex = text.LastIndexOf('\n', Mathf.Max(0, m_DblClickInitPos - 2)) + 1;
		}
	}

	public void SelectLeft()
	{
		if (m_bJustSelected && cursorIndex > selectIndex)
		{
			int num = cursorIndex;
			cursorIndex = selectIndex;
			selectIndex = num;
		}
		m_bJustSelected = false;
		cursorIndex = PreviousCodePointIndex(cursorIndex);
	}

	public void SelectRight()
	{
		if (m_bJustSelected && cursorIndex < selectIndex)
		{
			int num = cursorIndex;
			cursorIndex = selectIndex;
			selectIndex = num;
		}
		m_bJustSelected = false;
		cursorIndex = NextCodePointIndex(cursorIndex);
	}

	public void SelectUp()
	{
		GrabGraphicalCursorPos();
		graphicalCursorPos.y -= 1f;
		cursorIndex = style.GetCursorStringIndex(localPosition, m_Content, graphicalCursorPos);
	}

	public void SelectDown()
	{
		GrabGraphicalCursorPos();
		graphicalCursorPos.y += style.lineHeight + 5f;
		cursorIndex = style.GetCursorStringIndex(localPosition, m_Content, graphicalCursorPos);
	}

	public void SelectTextEnd()
	{
		cursorIndex = text.Length;
	}

	public void SelectTextStart()
	{
		cursorIndex = 0;
	}

	public void MouseDragSelectsWholeWords(bool on)
	{
		m_MouseDragSelectsWholeWords = on;
		m_DblClickInitPos = cursorIndex;
	}

	public void DblClickSnap(DblClickSnapping snapping)
	{
		m_DblClickSnap = snapping;
	}

	private int GetGraphicalLineStart(int p)
	{
		Vector2 cursorPixelPosition = style.GetCursorPixelPosition(localPosition, m_Content, p);
		cursorPixelPosition.y += 1f / GUIUtility.pixelsPerPoint;
		cursorPixelPosition.x = 0f;
		return style.GetCursorStringIndex(localPosition, m_Content, cursorPixelPosition);
	}

	private int GetGraphicalLineEnd(int p)
	{
		Vector2 cursorPixelPosition = style.GetCursorPixelPosition(localPosition, m_Content, p);
		cursorPixelPosition.y += 1f / GUIUtility.pixelsPerPoint;
		cursorPixelPosition.x += 5000f;
		return style.GetCursorStringIndex(localPosition, m_Content, cursorPixelPosition);
	}

	private int FindNextSeperator(int startPos)
	{
		int length = text.Length;
		while (startPos < length && ClassifyChar(startPos) != CharacterType.LetterLike)
		{
			startPos = NextCodePointIndex(startPos);
		}
		while (startPos < length && ClassifyChar(startPos) == CharacterType.LetterLike)
		{
			startPos = NextCodePointIndex(startPos);
		}
		return startPos;
	}

	private int FindPrevSeperator(int startPos)
	{
		startPos = PreviousCodePointIndex(startPos);
		while (startPos > 0 && ClassifyChar(startPos) != CharacterType.LetterLike)
		{
			startPos = PreviousCodePointIndex(startPos);
		}
		if (startPos == 0)
		{
			return 0;
		}
		while (startPos > 0 && ClassifyChar(startPos) == CharacterType.LetterLike)
		{
			startPos = PreviousCodePointIndex(startPos);
		}
		if (ClassifyChar(startPos) == CharacterType.LetterLike)
		{
			return startPos;
		}
		return NextCodePointIndex(startPos);
	}

	public void MoveWordRight()
	{
		cursorIndex = ((cursorIndex > selectIndex) ? cursorIndex : selectIndex);
		int num = (selectIndex = FindNextSeperator(cursorIndex));
		cursorIndex = num;
		ClearCursorPos();
	}

	public void MoveToStartOfNextWord()
	{
		ClearCursorPos();
		if (cursorIndex != selectIndex)
		{
			MoveRight();
			return;
		}
		int num = (selectIndex = FindStartOfNextWord(cursorIndex));
		cursorIndex = num;
	}

	public void MoveToEndOfPreviousWord()
	{
		ClearCursorPos();
		if (cursorIndex != selectIndex)
		{
			MoveLeft();
			return;
		}
		int num = (selectIndex = FindEndOfPreviousWord(cursorIndex));
		cursorIndex = num;
	}

	public void SelectToStartOfNextWord()
	{
		ClearCursorPos();
		cursorIndex = FindStartOfNextWord(cursorIndex);
	}

	public void SelectToEndOfPreviousWord()
	{
		ClearCursorPos();
		cursorIndex = FindEndOfPreviousWord(cursorIndex);
	}

	private CharacterType ClassifyChar(int index)
	{
		if (char.IsWhiteSpace(text, index))
		{
			return CharacterType.WhiteSpace;
		}
		if (char.IsLetterOrDigit(text, index) || text[index] == '\'')
		{
			return CharacterType.LetterLike;
		}
		return CharacterType.Symbol;
	}

	public int FindStartOfNextWord(int p)
	{
		int length = text.Length;
		if (p == length)
		{
			return p;
		}
		CharacterType characterType = ClassifyChar(p);
		if (characterType != CharacterType.WhiteSpace)
		{
			p = NextCodePointIndex(p);
			while (p < length && ClassifyChar(p) == characterType)
			{
				p = NextCodePointIndex(p);
			}
		}
		else if (text[p] == '\t' || text[p] == '\n')
		{
			return NextCodePointIndex(p);
		}
		if (p == length)
		{
			return p;
		}
		if (text[p] == ' ')
		{
			while (p < length && ClassifyChar(p) == CharacterType.WhiteSpace)
			{
				p = NextCodePointIndex(p);
			}
		}
		else if (text[p] == '\t' || text[p] == '\n')
		{
			return p;
		}
		return p;
	}

	private int FindEndOfPreviousWord(int p)
	{
		if (p == 0)
		{
			return p;
		}
		p = PreviousCodePointIndex(p);
		while (p > 0 && text[p] == ' ')
		{
			p = PreviousCodePointIndex(p);
		}
		CharacterType characterType = ClassifyChar(p);
		if (characterType != CharacterType.WhiteSpace)
		{
			while (p > 0 && ClassifyChar(PreviousCodePointIndex(p)) == characterType)
			{
				p = PreviousCodePointIndex(p);
			}
		}
		return p;
	}

	public void MoveWordLeft()
	{
		cursorIndex = ((cursorIndex < selectIndex) ? cursorIndex : selectIndex);
		cursorIndex = FindPrevSeperator(cursorIndex);
		selectIndex = cursorIndex;
	}

	public void SelectWordRight()
	{
		ClearCursorPos();
		int num = selectIndex;
		if (cursorIndex < selectIndex)
		{
			selectIndex = cursorIndex;
			MoveWordRight();
			selectIndex = num;
			cursorIndex = ((cursorIndex < selectIndex) ? cursorIndex : selectIndex);
		}
		else
		{
			selectIndex = cursorIndex;
			MoveWordRight();
			selectIndex = num;
		}
	}

	public void SelectWordLeft()
	{
		ClearCursorPos();
		int num = selectIndex;
		if (cursorIndex > selectIndex)
		{
			selectIndex = cursorIndex;
			MoveWordLeft();
			selectIndex = num;
			cursorIndex = ((cursorIndex > selectIndex) ? cursorIndex : selectIndex);
		}
		else
		{
			selectIndex = cursorIndex;
			MoveWordLeft();
			selectIndex = num;
		}
	}

	public void ExpandSelectGraphicalLineStart()
	{
		ClearCursorPos();
		if (cursorIndex < selectIndex)
		{
			cursorIndex = GetGraphicalLineStart(cursorIndex);
			return;
		}
		int num = cursorIndex;
		cursorIndex = GetGraphicalLineStart(selectIndex);
		selectIndex = num;
	}

	public void ExpandSelectGraphicalLineEnd()
	{
		ClearCursorPos();
		if (cursorIndex > selectIndex)
		{
			cursorIndex = GetGraphicalLineEnd(cursorIndex);
			return;
		}
		int num = cursorIndex;
		cursorIndex = GetGraphicalLineEnd(selectIndex);
		selectIndex = num;
	}

	public void SelectGraphicalLineStart()
	{
		ClearCursorPos();
		cursorIndex = GetGraphicalLineStart(cursorIndex);
	}

	public void SelectGraphicalLineEnd()
	{
		ClearCursorPos();
		cursorIndex = GetGraphicalLineEnd(cursorIndex);
	}

	public void SelectParagraphForward()
	{
		ClearCursorPos();
		bool flag = cursorIndex < selectIndex;
		if (cursorIndex < text.Length)
		{
			cursorIndex = IndexOfEndOfLine(cursorIndex + 1);
			if (flag && cursorIndex > selectIndex)
			{
				cursorIndex = selectIndex;
			}
		}
	}

	public void SelectParagraphBackward()
	{
		ClearCursorPos();
		bool flag = cursorIndex > selectIndex;
		if (cursorIndex > 1)
		{
			cursorIndex = text.LastIndexOf('\n', cursorIndex - 2) + 1;
			if (flag && cursorIndex < selectIndex)
			{
				cursorIndex = selectIndex;
			}
		}
		else
		{
			int num = (cursorIndex = 0);
			selectIndex = num;
		}
	}

	public void SelectCurrentWord()
	{
		int p = cursorIndex;
		if (cursorIndex < selectIndex)
		{
			cursorIndex = FindEndOfClassification(p, Direction.Backward);
			selectIndex = FindEndOfClassification(p, Direction.Forward);
		}
		else
		{
			cursorIndex = FindEndOfClassification(p, Direction.Forward);
			selectIndex = FindEndOfClassification(p, Direction.Backward);
		}
		ClearCursorPos();
		m_bJustSelected = true;
	}

	private int FindEndOfClassification(int p, Direction dir)
	{
		if (text.Length == 0)
		{
			return 0;
		}
		if (p == text.Length)
		{
			p = PreviousCodePointIndex(p);
		}
		CharacterType characterType = ClassifyChar(p);
		do
		{
			switch (dir)
			{
			case Direction.Backward:
				p = PreviousCodePointIndex(p);
				if (p == 0)
				{
					return (ClassifyChar(0) != characterType) ? NextCodePointIndex(0) : 0;
				}
				break;
			case Direction.Forward:
				p = NextCodePointIndex(p);
				if (p == text.Length)
				{
					return text.Length;
				}
				break;
			}
		}
		while (ClassifyChar(p) == characterType);
		if (dir == Direction.Forward)
		{
			return p;
		}
		return NextCodePointIndex(p);
	}

	public void SelectCurrentParagraph()
	{
		ClearCursorPos();
		int length = text.Length;
		if (cursorIndex < length)
		{
			cursorIndex = IndexOfEndOfLine(cursorIndex) + 1;
		}
		if (selectIndex != 0)
		{
			selectIndex = text.LastIndexOf('\n', selectIndex - 1) + 1;
		}
	}

	public void UpdateScrollOffsetIfNeeded(Event evt)
	{
		if (evt.type != EventType.Repaint && evt.type != EventType.Layout)
		{
			UpdateScrollOffset();
		}
	}

	[VisibleToOtherModules]
	internal void UpdateScrollOffset()
	{
		int cursorStringIndex = cursorIndex;
		graphicalCursorPos = style.GetCursorPixelPosition(new Rect(0f, 0f, position.width, position.height), m_Content, cursorStringIndex);
		Rect rect = style.padding.Remove(position);
		Vector2 vector = graphicalCursorPos;
		vector.x -= style.padding.left;
		vector.y -= style.padding.top;
		Vector2 vector2 = new Vector2(style.CalcSize(m_Content).x, style.CalcHeight(m_Content, position.width));
		vector2.x -= style.padding.left + style.padding.right;
		vector2.y -= style.padding.top + style.padding.bottom;
		if (vector2.x < rect.width)
		{
			scrollOffset.x = 0f;
		}
		else if (m_RevealCursor)
		{
			if (vector.x + 1f > scrollOffset.x + rect.width)
			{
				scrollOffset.x = vector.x - rect.width + 1f;
			}
			if (vector.x < scrollOffset.x)
			{
				scrollOffset.x = vector.x;
			}
		}
		if (vector2.y < rect.height)
		{
			scrollOffset.y = 0f;
		}
		else if (m_RevealCursor)
		{
			if (vector.y + style.lineHeight > scrollOffset.y + rect.height)
			{
				scrollOffset.y = vector.y - rect.height + style.lineHeight;
			}
			if (vector.y < scrollOffset.y)
			{
				scrollOffset.y = vector.y;
			}
		}
		if (scrollOffset.y > 0f && vector2.y - scrollOffset.y < rect.height)
		{
			scrollOffset.y = vector2.y - rect.height;
		}
		scrollOffset.y = ((scrollOffset.y < 0f) ? 0f : scrollOffset.y);
		m_RevealCursor = false;
	}

	public void DrawCursor(string newText)
	{
		string text = this.text;
		int num = cursorIndex;
		if (GUIUtility.compositionString.Length > 0)
		{
			m_Content.text = newText.Substring(0, cursorIndex) + GUIUtility.compositionString + newText.Substring(selectIndex);
			num += GUIUtility.compositionString.Length;
		}
		else
		{
			m_Content.text = newText;
		}
		graphicalCursorPos = style.GetCursorPixelPosition(new Rect(0f, 0f, position.width, position.height), m_Content, num);
		Vector2 contentOffset = style.contentOffset;
		style.contentOffset -= scrollOffset;
		style.Internal_clipOffset = scrollOffset;
		GUIUtility.compositionCursorPos = GUIClip.UnclipToWindow(graphicalCursorPos + new Vector2(position.x, position.y + style.lineHeight) - scrollOffset);
		if (GUIUtility.compositionString.Length > 0)
		{
			style.DrawWithTextSelection(position, m_Content, controlID, cursorIndex, cursorIndex + GUIUtility.compositionString.Length, drawSelectionAsComposition: true);
		}
		else
		{
			style.DrawWithTextSelection(position, m_Content, controlID, cursorIndex, selectIndex);
		}
		if (m_iAltCursorPos != -1)
		{
			style.DrawCursor(position, m_Content, controlID, m_iAltCursorPos);
		}
		style.contentOffset = contentOffset;
		style.Internal_clipOffset = Vector2.zero;
		m_Content.text = text;
	}

	private bool PerformOperation(TextEditOp operation, bool textIsReadOnly)
	{
		m_RevealCursor = true;
		switch (operation)
		{
		case TextEditOp.MoveLeft:
			MoveLeft();
			break;
		case TextEditOp.MoveRight:
			MoveRight();
			break;
		case TextEditOp.MoveUp:
			MoveUp();
			break;
		case TextEditOp.MoveDown:
			MoveDown();
			break;
		case TextEditOp.MoveLineStart:
			MoveLineStart();
			break;
		case TextEditOp.MoveLineEnd:
			MoveLineEnd();
			break;
		case TextEditOp.MoveWordRight:
			MoveWordRight();
			break;
		case TextEditOp.MoveToStartOfNextWord:
			MoveToStartOfNextWord();
			break;
		case TextEditOp.MoveToEndOfPreviousWord:
			MoveToEndOfPreviousWord();
			break;
		case TextEditOp.MoveWordLeft:
			MoveWordLeft();
			break;
		case TextEditOp.MoveTextStart:
			MoveTextStart();
			break;
		case TextEditOp.MoveTextEnd:
			MoveTextEnd();
			break;
		case TextEditOp.MoveParagraphForward:
			MoveParagraphForward();
			break;
		case TextEditOp.MoveParagraphBackward:
			MoveParagraphBackward();
			break;
		case TextEditOp.MoveGraphicalLineStart:
			MoveGraphicalLineStart();
			break;
		case TextEditOp.MoveGraphicalLineEnd:
			MoveGraphicalLineEnd();
			break;
		case TextEditOp.SelectLeft:
			SelectLeft();
			break;
		case TextEditOp.SelectRight:
			SelectRight();
			break;
		case TextEditOp.SelectUp:
			SelectUp();
			break;
		case TextEditOp.SelectDown:
			SelectDown();
			break;
		case TextEditOp.SelectWordRight:
			SelectWordRight();
			break;
		case TextEditOp.SelectWordLeft:
			SelectWordLeft();
			break;
		case TextEditOp.SelectToEndOfPreviousWord:
			SelectToEndOfPreviousWord();
			break;
		case TextEditOp.SelectToStartOfNextWord:
			SelectToStartOfNextWord();
			break;
		case TextEditOp.SelectTextStart:
			SelectTextStart();
			break;
		case TextEditOp.SelectTextEnd:
			SelectTextEnd();
			break;
		case TextEditOp.ExpandSelectGraphicalLineStart:
			ExpandSelectGraphicalLineStart();
			break;
		case TextEditOp.ExpandSelectGraphicalLineEnd:
			ExpandSelectGraphicalLineEnd();
			break;
		case TextEditOp.SelectParagraphForward:
			SelectParagraphForward();
			break;
		case TextEditOp.SelectParagraphBackward:
			SelectParagraphBackward();
			break;
		case TextEditOp.SelectGraphicalLineStart:
			SelectGraphicalLineStart();
			break;
		case TextEditOp.SelectGraphicalLineEnd:
			SelectGraphicalLineEnd();
			break;
		case TextEditOp.Delete:
			if (textIsReadOnly)
			{
				return false;
			}
			return Delete();
		case TextEditOp.Backspace:
			if (textIsReadOnly)
			{
				return false;
			}
			return Backspace();
		case TextEditOp.Cut:
			if (textIsReadOnly)
			{
				return false;
			}
			return Cut();
		case TextEditOp.Copy:
			Copy();
			break;
		case TextEditOp.Paste:
			if (textIsReadOnly)
			{
				return false;
			}
			return Paste();
		case TextEditOp.SelectAll:
			SelectAll();
			break;
		case TextEditOp.SelectNone:
			SelectNone();
			break;
		case TextEditOp.DeleteWordBack:
			if (textIsReadOnly)
			{
				return false;
			}
			return DeleteWordBack();
		case TextEditOp.DeleteLineBack:
			if (textIsReadOnly)
			{
				return false;
			}
			return DeleteLineBack();
		case TextEditOp.DeleteWordForward:
			if (textIsReadOnly)
			{
				return false;
			}
			return DeleteWordForward();
		default:
			Debug.Log("Unimplemented: " + operation);
			break;
		}
		return false;
	}

	public void SaveBackup()
	{
		oldText = text;
		oldPos = cursorIndex;
		oldSelectPos = selectIndex;
	}

	public void Undo()
	{
		m_Content.text = oldText;
		cursorIndex = oldPos;
		selectIndex = oldSelectPos;
	}

	public bool Cut()
	{
		if (isPasswordField)
		{
			return false;
		}
		Copy();
		return DeleteSelection();
	}

	public void Copy()
	{
		if (selectIndex != cursorIndex && !isPasswordField)
		{
			string systemCopyBuffer = style.Internal_GetSelectedRenderedText(localPosition, m_Content, selectIndex, cursorIndex);
			GUIUtility.systemCopyBuffer = systemCopyBuffer;
		}
	}

	internal Rect[] GetHyperlinksRect()
	{
		return style.Internal_GetHyperlinksRect(localPosition, m_Content);
	}

	private static string ReplaceNewlinesWithSpaces(string value)
	{
		value = value.Replace("\r\n", " ");
		value = value.Replace('\n', ' ');
		value = value.Replace('\r', ' ');
		return value;
	}

	public bool Paste()
	{
		string text = GUIUtility.systemCopyBuffer;
		if (text != "")
		{
			if (!multiline)
			{
				text = ReplaceNewlinesWithSpaces(text);
			}
			ReplaceSelection(text);
			return true;
		}
		return false;
	}

	private static void MapKey(string key, TextEditOp action)
	{
		s_Keyactions[Event.KeyboardEvent(key)] = action;
	}

	private void InitKeyActions()
	{
		if (s_Keyactions == null)
		{
			s_Keyactions = new Dictionary<Event, TextEditOp>();
			MapKey("left", TextEditOp.MoveLeft);
			MapKey("right", TextEditOp.MoveRight);
			MapKey("up", TextEditOp.MoveUp);
			MapKey("down", TextEditOp.MoveDown);
			MapKey("#left", TextEditOp.SelectLeft);
			MapKey("#right", TextEditOp.SelectRight);
			MapKey("#up", TextEditOp.SelectUp);
			MapKey("#down", TextEditOp.SelectDown);
			MapKey("delete", TextEditOp.Delete);
			MapKey("backspace", TextEditOp.Backspace);
			MapKey("#backspace", TextEditOp.Backspace);
			if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX)
			{
				MapKey("^left", TextEditOp.MoveGraphicalLineStart);
				MapKey("^right", TextEditOp.MoveGraphicalLineEnd);
				MapKey("&left", TextEditOp.MoveWordLeft);
				MapKey("&right", TextEditOp.MoveWordRight);
				MapKey("&up", TextEditOp.MoveParagraphBackward);
				MapKey("&down", TextEditOp.MoveParagraphForward);
				MapKey("%left", TextEditOp.MoveGraphicalLineStart);
				MapKey("%right", TextEditOp.MoveGraphicalLineEnd);
				MapKey("%up", TextEditOp.MoveTextStart);
				MapKey("%down", TextEditOp.MoveTextEnd);
				MapKey("#home", TextEditOp.SelectTextStart);
				MapKey("#end", TextEditOp.SelectTextEnd);
				MapKey("#^left", TextEditOp.ExpandSelectGraphicalLineStart);
				MapKey("#^right", TextEditOp.ExpandSelectGraphicalLineEnd);
				MapKey("#^up", TextEditOp.SelectParagraphBackward);
				MapKey("#^down", TextEditOp.SelectParagraphForward);
				MapKey("#&left", TextEditOp.SelectWordLeft);
				MapKey("#&right", TextEditOp.SelectWordRight);
				MapKey("#&up", TextEditOp.SelectParagraphBackward);
				MapKey("#&down", TextEditOp.SelectParagraphForward);
				MapKey("#%left", TextEditOp.ExpandSelectGraphicalLineStart);
				MapKey("#%right", TextEditOp.ExpandSelectGraphicalLineEnd);
				MapKey("#%up", TextEditOp.SelectTextStart);
				MapKey("#%down", TextEditOp.SelectTextEnd);
				MapKey("%a", TextEditOp.SelectAll);
				MapKey("%x", TextEditOp.Cut);
				MapKey("%c", TextEditOp.Copy);
				MapKey("%v", TextEditOp.Paste);
				MapKey("^d", TextEditOp.Delete);
				MapKey("^h", TextEditOp.Backspace);
				MapKey("^b", TextEditOp.MoveLeft);
				MapKey("^f", TextEditOp.MoveRight);
				MapKey("^a", TextEditOp.MoveLineStart);
				MapKey("^e", TextEditOp.MoveLineEnd);
				MapKey("&delete", TextEditOp.DeleteWordForward);
				MapKey("&backspace", TextEditOp.DeleteWordBack);
				MapKey("%backspace", TextEditOp.DeleteLineBack);
			}
			else
			{
				MapKey("home", TextEditOp.MoveGraphicalLineStart);
				MapKey("end", TextEditOp.MoveGraphicalLineEnd);
				MapKey("%left", TextEditOp.MoveWordLeft);
				MapKey("%right", TextEditOp.MoveWordRight);
				MapKey("%up", TextEditOp.MoveParagraphBackward);
				MapKey("%down", TextEditOp.MoveParagraphForward);
				MapKey("^left", TextEditOp.MoveToEndOfPreviousWord);
				MapKey("^right", TextEditOp.MoveToStartOfNextWord);
				MapKey("^up", TextEditOp.MoveParagraphBackward);
				MapKey("^down", TextEditOp.MoveParagraphForward);
				MapKey("#^left", TextEditOp.SelectToEndOfPreviousWord);
				MapKey("#^right", TextEditOp.SelectToStartOfNextWord);
				MapKey("#^up", TextEditOp.SelectParagraphBackward);
				MapKey("#^down", TextEditOp.SelectParagraphForward);
				MapKey("#home", TextEditOp.SelectGraphicalLineStart);
				MapKey("#end", TextEditOp.SelectGraphicalLineEnd);
				MapKey("^delete", TextEditOp.DeleteWordForward);
				MapKey("^backspace", TextEditOp.DeleteWordBack);
				MapKey("%backspace", TextEditOp.DeleteLineBack);
				MapKey("^a", TextEditOp.SelectAll);
				MapKey("^x", TextEditOp.Cut);
				MapKey("^c", TextEditOp.Copy);
				MapKey("^v", TextEditOp.Paste);
				MapKey("#delete", TextEditOp.Cut);
				MapKey("^insert", TextEditOp.Copy);
				MapKey("#insert", TextEditOp.Paste);
			}
		}
	}

	public void DetectFocusChange()
	{
		OnDetectFocusChange();
	}

	internal virtual void OnDetectFocusChange()
	{
		if (m_HasFocus && controlID != GUIUtility.keyboardControl)
		{
			OnLostFocus();
		}
		if (!m_HasFocus && controlID == GUIUtility.keyboardControl)
		{
			OnFocus();
		}
	}

	internal virtual void OnCursorIndexChange()
	{
	}

	internal virtual void OnSelectIndexChange()
	{
	}

	private void ClampTextIndex(ref int index)
	{
		index = Mathf.Clamp(index, 0, text.Length);
	}

	private void EnsureValidCodePointIndex(ref int index)
	{
		ClampTextIndex(ref index);
		if (!IsValidCodePointIndex(index))
		{
			index = NextCodePointIndex(index);
		}
	}

	private bool IsValidCodePointIndex(int index)
	{
		if (index < 0 || index > text.Length)
		{
			return false;
		}
		if (index == 0 || index == text.Length)
		{
			return true;
		}
		return !char.IsLowSurrogate(text[index]);
	}

	private int PreviousCodePointIndex(int index)
	{
		if (index > 0)
		{
			index--;
		}
		while (index > 0 && char.IsLowSurrogate(text[index]))
		{
			index--;
		}
		return index;
	}

	private int NextCodePointIndex(int index)
	{
		if (index < text.Length)
		{
			index++;
		}
		while (index < text.Length && char.IsLowSurrogate(text[index]))
		{
			index++;
		}
		return index;
	}
}
internal class TextSelectingUtilities
{
	private enum CharacterType
	{
		LetterLike,
		Symbol,
		Symbol2,
		WhiteSpace,
		NewLine
	}

	private enum Direction
	{
		Forward,
		Backward
	}

	public TextEditor.DblClickSnapping dblClickSnap = TextEditor.DblClickSnapping.WORDS;

	public int iAltCursorPos = -1;

	public bool hasHorizontalCursorPos = false;

	private bool m_bJustSelected = false;

	private bool m_MouseDragSelectsWholeWords = false;

	private int m_DblClickInitPosStart = 0;

	private int m_DblClickInitPosEnd = 0;

	private TextHandle m_TextHandle;

	private const int kMoveDownHeight = 5;

	private const char kNewLineChar = '\n';

	private bool m_RevealCursor;

	private int m_CursorIndex = 0;

	internal int m_SelectIndex = 0;

	private static Dictionary<Event, TextSelectOp> s_KeySelectOps;

	internal Action OnCursorIndexChange;

	internal Action OnSelectIndexChange;

	internal Action OnRevealCursorChange;

	public bool hasSelection => cursorIndex != selectIndex;

	public bool revealCursor
	{
		get
		{
			return m_RevealCursor;
		}
		set
		{
			if (m_RevealCursor != value)
			{
				m_RevealCursor = value;
				OnRevealCursorChange?.Invoke();
			}
		}
	}

	private int m_CharacterCount => m_TextHandle.textInfo.characterCount;

	private int characterCount => (m_CharacterCount > 0 && m_TextHandle.textInfo.textElementInfo[m_CharacterCount - 1].character == '\u200b') ? (m_CharacterCount - 1) : m_CharacterCount;

	private TextElementInfo[] m_TextElementInfos => m_TextHandle.textInfo.textElementInfo;

	public int cursorIndex
	{
		get
		{
			return EnsureValidCodePointIndex(m_CursorIndex);
		}
		set
		{
			if (m_CursorIndex != value)
			{
				SetCursorIndexWithoutNotify(value);
				OnCursorIndexChange?.Invoke();
			}
		}
	}

	public int selectIndex
	{
		get
		{
			return EnsureValidCodePointIndex(m_SelectIndex);
		}
		set
		{
			if (m_SelectIndex != value)
			{
				SetSelectIndexWithoutNotify(value);
				OnSelectIndexChange?.Invoke();
			}
		}
	}

	public string selectedText
	{
		get
		{
			if (cursorIndex == selectIndex)
			{
				return "";
			}
			if (cursorIndex < selectIndex)
			{
				return m_TextHandle.Substring(cursorIndex, selectIndex - cursorIndex);
			}
			return m_TextHandle.Substring(selectIndex, cursorIndex - selectIndex);
		}
	}

	internal void SetCursorIndexWithoutNotify(int index)
	{
		m_CursorIndex = index;
	}

	internal void SetSelectIndexWithoutNotify(int index)
	{
		m_SelectIndex = index;
	}

	public TextSelectingUtilities(TextHandle textHandle)
	{
		m_TextHandle = textHandle;
	}

	internal bool HandleKeyEvent(Event e)
	{
		InitKeyActions();
		EventModifiers modifiers = e.modifiers;
		e.modifiers &= ~EventModifiers.CapsLock;
		if (s_KeySelectOps.ContainsKey(e))
		{
			TextSelectOp operation = s_KeySelectOps[e];
			PerformOperation(operation);
			e.modifiers = modifiers;
			return true;
		}
		e.modifiers = modifiers;
		return false;
	}

	private bool PerformOperation(TextSelectOp operation)
	{
		switch (operation)
		{
		case TextSelectOp.SelectLeft:
			SelectLeft();
			break;
		case TextSelectOp.SelectRight:
			SelectRight();
			break;
		case TextSelectOp.SelectUp:
			SelectUp();
			break;
		case TextSelectOp.SelectDown:
			SelectDown();
			break;
		case TextSelectOp.SelectWordRight:
			SelectWordRight();
			break;
		case TextSelectOp.SelectWordLeft:
			SelectWordLeft();
			break;
		case TextSelectOp.SelectToEndOfPreviousWord:
			SelectToEndOfPreviousWord();
			break;
		case TextSelectOp.SelectToStartOfNextWord:
			SelectToStartOfNextWord();
			break;
		case TextSelectOp.SelectTextStart:
			SelectTextStart();
			break;
		case TextSelectOp.SelectTextEnd:
			SelectTextEnd();
			break;
		case TextSelectOp.ExpandSelectGraphicalLineStart:
			ExpandSelectGraphicalLineStart();
			break;
		case TextSelectOp.ExpandSelectGraphicalLineEnd:
			ExpandSelectGraphicalLineEnd();
			break;
		case TextSelectOp.SelectParagraphForward:
			SelectParagraphForward();
			break;
		case TextSelectOp.SelectParagraphBackward:
			SelectParagraphBackward();
			break;
		case TextSelectOp.SelectGraphicalLineStart:
			SelectGraphicalLineStart();
			break;
		case TextSelectOp.SelectGraphicalLineEnd:
			SelectGraphicalLineEnd();
			break;
		case TextSelectOp.Copy:
			Copy();
			break;
		case TextSelectOp.SelectAll:
			SelectAll();
			break;
		case TextSelectOp.SelectNone:
			SelectNone();
			break;
		default:
			Debug.Log("Unimplemented: " + operation);
			break;
		}
		return false;
	}

	private static void MapKey(string key, TextSelectOp action)
	{
		s_KeySelectOps[Event.KeyboardEvent(key)] = action;
	}

	private void InitKeyActions()
	{
		if (s_KeySelectOps == null)
		{
			s_KeySelectOps = new Dictionary<Event, TextSelectOp>();
			MapKey("#left", TextSelectOp.SelectLeft);
			MapKey("#right", TextSelectOp.SelectRight);
			MapKey("#up", TextSelectOp.SelectUp);
			MapKey("#down", TextSelectOp.SelectDown);
			if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX)
			{
				MapKey("#home", TextSelectOp.SelectTextStart);
				MapKey("#end", TextSelectOp.SelectTextEnd);
				MapKey("#^left", TextSelectOp.ExpandSelectGraphicalLineStart);
				MapKey("#^right", TextSelectOp.ExpandSelectGraphicalLineEnd);
				MapKey("#^up", TextSelectOp.SelectParagraphBackward);
				MapKey("#^down", TextSelectOp.SelectParagraphForward);
				MapKey("#&left", TextSelectOp.SelectWordLeft);
				MapKey("#&right", TextSelectOp.SelectWordRight);
				MapKey("#&up", TextSelectOp.SelectParagraphBackward);
				MapKey("#&down", TextSelectOp.SelectParagraphForward);
				MapKey("#%left", TextSelectOp.ExpandSelectGraphicalLineStart);
				MapKey("#%right", TextSelectOp.ExpandSelectGraphicalLineEnd);
				MapKey("#%up", TextSelectOp.SelectTextStart);
				MapKey("#%down", TextSelectOp.SelectTextEnd);
				MapKey("%a", TextSelectOp.SelectAll);
				MapKey("%c", TextSelectOp.Copy);
			}
			else
			{
				MapKey("#^left", TextSelectOp.SelectToEndOfPreviousWord);
				MapKey("#^right", TextSelectOp.SelectToStartOfNextWord);
				MapKey("#^up", TextSelectOp.SelectParagraphBackward);
				MapKey("#^down", TextSelectOp.SelectParagraphForward);
				MapKey("#home", TextSelectOp.SelectGraphicalLineStart);
				MapKey("#end", TextSelectOp.SelectGraphicalLineEnd);
				MapKey("^a", TextSelectOp.SelectAll);
				MapKey("^c", TextSelectOp.Copy);
				MapKey("^insert", TextSelectOp.Copy);
			}
		}
	}

	public void ClearCursorPos()
	{
		hasHorizontalCursorPos = false;
		iAltCursorPos = -1;
	}

	public void OnFocus(bool selectAll = true)
	{
		if (selectAll)
		{
			SelectAll();
		}
		revealCursor = true;
	}

	public void SelectAll()
	{
		cursorIndex = 0;
		selectIndex = int.MaxValue;
		ClearCursorPos();
	}

	public void SelectNone()
	{
		selectIndex = cursorIndex;
		ClearCursorPos();
	}

	public void SelectLeft()
	{
		if (m_bJustSelected && cursorIndex > selectIndex)
		{
			int num = cursorIndex;
			cursorIndex = selectIndex;
			selectIndex = num;
		}
		m_bJustSelected = false;
		cursorIndex = PreviousCodePointIndex(cursorIndex);
	}

	public void SelectRight()
	{
		if (m_bJustSelected && cursorIndex < selectIndex)
		{
			int num = cursorIndex;
			cursorIndex = selectIndex;
			selectIndex = num;
		}
		m_bJustSelected = false;
		cursorIndex = NextCodePointIndex(cursorIndex);
	}

	public void SelectUp()
	{
		cursorIndex = m_TextHandle.LineUpCharacterPosition(cursorIndex);
	}

	public void SelectDown()
	{
		cursorIndex = m_TextHandle.LineDownCharacterPosition(cursorIndex);
	}

	public void SelectTextEnd()
	{
		cursorIndex = characterCount;
	}

	public void SelectTextStart()
	{
		cursorIndex = 0;
	}

	public void SelectToStartOfNextWord()
	{
		ClearCursorPos();
		cursorIndex = FindStartOfNextWord(cursorIndex);
	}

	public void SelectToEndOfPreviousWord()
	{
		ClearCursorPos();
		cursorIndex = FindEndOfPreviousWord(cursorIndex);
	}

	public void SelectWordRight()
	{
		ClearCursorPos();
		int num = selectIndex;
		if (cursorIndex < selectIndex)
		{
			selectIndex = cursorIndex;
			MoveWordRight();
			selectIndex = num;
			cursorIndex = ((cursorIndex < selectIndex) ? cursorIndex : selectIndex);
		}
		else
		{
			selectIndex = cursorIndex;
			MoveWordRight();
			selectIndex = num;
		}
	}

	public void SelectWordLeft()
	{
		ClearCursorPos();
		int num = selectIndex;
		if (cursorIndex > selectIndex)
		{
			selectIndex = cursorIndex;
			MoveWordLeft();
			selectIndex = num;
			cursorIndex = ((cursorIndex > selectIndex) ? cursorIndex : selectIndex);
		}
		else
		{
			selectIndex = cursorIndex;
			MoveWordLeft();
			selectIndex = num;
		}
	}

	public void SelectGraphicalLineStart()
	{
		ClearCursorPos();
		cursorIndex = GetGraphicalLineStart(cursorIndex);
	}

	public void SelectGraphicalLineEnd()
	{
		ClearCursorPos();
		cursorIndex = GetGraphicalLineEnd(cursorIndex);
	}

	public void SelectParagraphForward()
	{
		ClearCursorPos();
		bool flag = cursorIndex < selectIndex;
		if (cursorIndex < characterCount)
		{
			cursorIndex = IndexOfEndOfLine(cursorIndex + 1);
			if (flag && cursorIndex > selectIndex)
			{
				cursorIndex = selectIndex;
			}
		}
	}

	public void SelectParagraphBackward()
	{
		ClearCursorPos();
		bool flag = cursorIndex > selectIndex;
		if (cursorIndex > 1)
		{
			cursorIndex = m_TextHandle.LastIndexOf('\n', cursorIndex - 2) + 1;
			if (flag && cursorIndex < selectIndex)
			{
				cursorIndex = selectIndex;
			}
		}
		else
		{
			int num = (cursorIndex = 0);
			selectIndex = num;
		}
	}

	public void SelectCurrentWord()
	{
		int p = cursorIndex;
		if (cursorIndex < selectIndex)
		{
			cursorIndex = FindEndOfClassification(p, Direction.Backward);
			selectIndex = FindEndOfClassification(p, Direction.Forward);
		}
		else
		{
			cursorIndex = FindEndOfClassification(p, Direction.Forward);
			selectIndex = FindEndOfClassification(p, Direction.Backward);
		}
		ClearCursorPos();
		m_bJustSelected = true;
	}

	public void SelectCurrentParagraph()
	{
		ClearCursorPos();
		int num = characterCount;
		if (cursorIndex < num)
		{
			cursorIndex = IndexOfEndOfLine(cursorIndex);
		}
		if (selectIndex != 0)
		{
			selectIndex = m_TextHandle.LastIndexOf('\n', selectIndex - 1) + 1;
		}
	}

	public void MoveRight()
	{
		ClearCursorPos();
		if (selectIndex == cursorIndex)
		{
			cursorIndex = NextCodePointIndex(cursorIndex);
			selectIndex = cursorIndex;
		}
		else if (selectIndex > cursorIndex)
		{
			cursorIndex = selectIndex;
		}
		else
		{
			selectIndex = cursorIndex;
		}
	}

	public void MoveLeft()
	{
		if (selectIndex == cursorIndex)
		{
			cursorIndex = PreviousCodePointIndex(cursorIndex);
			selectIndex = cursorIndex;
		}
		else if (selectIndex > cursorIndex)
		{
			selectIndex = cursorIndex;
		}
		else
		{
			cursorIndex = selectIndex;
		}
		ClearCursorPos();
	}

	public void MoveUp()
	{
		if (selectIndex < cursorIndex)
		{
			selectIndex = cursorIndex;
		}
		else
		{
			cursorIndex = selectIndex;
		}
		int num = (selectIndex = m_TextHandle.LineUpCharacterPosition(cursorIndex));
		cursorIndex = num;
		if (cursorIndex <= 0)
		{
			ClearCursorPos();
		}
	}

	public void MoveDown()
	{
		if (selectIndex > cursorIndex)
		{
			selectIndex = cursorIndex;
		}
		else
		{
			cursorIndex = selectIndex;
		}
		int num = (selectIndex = m_TextHandle.LineDownCharacterPosition(cursorIndex));
		cursorIndex = num;
		if (cursorIndex == characterCount)
		{
			ClearCursorPos();
		}
	}

	public void MoveLineStart()
	{
		int num = ((selectIndex < cursorIndex) ? selectIndex : cursorIndex);
		int num2 = num;
		int num3;
		while (num2-- != 0)
		{
			if (m_TextElementInfos[num2].character == '\n')
			{
				num3 = (cursorIndex = num2 + 1);
				selectIndex = num3;
				return;
			}
		}
		num3 = (cursorIndex = 0);
		selectIndex = num3;
	}

	public void MoveLineEnd()
	{
		int num = ((selectIndex > cursorIndex) ? selectIndex : cursorIndex);
		int i = num;
		int num2;
		int num3;
		for (num2 = characterCount; i < num2; i++)
		{
			if (m_TextElementInfos[i].character == '\n')
			{
				num3 = (cursorIndex = i);
				selectIndex = num3;
				return;
			}
		}
		num3 = (cursorIndex = num2);
		selectIndex = num3;
	}

	public void MoveGraphicalLineStart()
	{
		int num = (selectIndex = GetGraphicalLineStart((cursorIndex < selectIndex) ? cursorIndex : selectIndex));
		cursorIndex = num;
	}

	public void MoveGraphicalLineEnd()
	{
		int num = (selectIndex = GetGraphicalLineEnd((cursorIndex > selectIndex) ? cursorIndex : selectIndex));
		cursorIndex = num;
	}

	public void MoveTextStart()
	{
		int num = (cursorIndex = 0);
		selectIndex = num;
	}

	public void MoveTextEnd()
	{
		int num = (cursorIndex = characterCount);
		selectIndex = num;
	}

	public void MoveParagraphForward()
	{
		cursorIndex = ((cursorIndex > selectIndex) ? cursorIndex : selectIndex);
		if (cursorIndex < characterCount)
		{
			int num = (cursorIndex = IndexOfEndOfLine(cursorIndex + 1));
			selectIndex = num;
		}
	}

	public void MoveParagraphBackward()
	{
		cursorIndex = ((cursorIndex < selectIndex) ? cursorIndex : selectIndex);
		if (cursorIndex > 1)
		{
			int num = (cursorIndex = m_TextHandle.LastIndexOf('\n', cursorIndex - 2) + 1);
			selectIndex = num;
		}
		else
		{
			int num = (cursorIndex = 0);
			selectIndex = num;
		}
	}

	public void MoveWordRight()
	{
		cursorIndex = ((cursorIndex > selectIndex) ? cursorIndex : selectIndex);
		int num = (selectIndex = FindNextSeperator(cursorIndex));
		cursorIndex = num;
		ClearCursorPos();
	}

	public void MoveToStartOfNextWord()
	{
		ClearCursorPos();
		if (cursorIndex != selectIndex)
		{
			MoveRight();
			return;
		}
		int num = (selectIndex = FindStartOfNextWord(cursorIndex));
		cursorIndex = num;
	}

	public void MoveToEndOfPreviousWord()
	{
		ClearCursorPos();
		if (cursorIndex != selectIndex)
		{
			MoveLeft();
			return;
		}
		int num = (selectIndex = FindEndOfPreviousWord(cursorIndex));
		cursorIndex = num;
	}

	public void MoveWordLeft()
	{
		cursorIndex = ((cursorIndex < selectIndex) ? cursorIndex : selectIndex);
		cursorIndex = FindPrevSeperator(cursorIndex);
		selectIndex = cursorIndex;
	}

	public void MouseDragSelectsWholeWords(bool on)
	{
		m_MouseDragSelectsWholeWords = on;
		m_DblClickInitPosStart = ((cursorIndex < selectIndex) ? cursorIndex : selectIndex);
		m_DblClickInitPosEnd = ((cursorIndex < selectIndex) ? selectIndex : cursorIndex);
	}

	public void ExpandSelectGraphicalLineStart()
	{
		ClearCursorPos();
		if (cursorIndex < selectIndex)
		{
			cursorIndex = GetGraphicalLineStart(cursorIndex);
			return;
		}
		int num = cursorIndex;
		cursorIndex = GetGraphicalLineStart(selectIndex);
		selectIndex = num;
	}

	public void ExpandSelectGraphicalLineEnd()
	{
		ClearCursorPos();
		if (cursorIndex > selectIndex)
		{
			cursorIndex = GetGraphicalLineEnd(cursorIndex);
			return;
		}
		int num = cursorIndex;
		cursorIndex = GetGraphicalLineEnd(selectIndex);
		selectIndex = num;
	}

	public void DblClickSnap(TextEditor.DblClickSnapping snapping)
	{
		dblClickSnap = snapping;
	}

	protected internal void MoveCursorToPosition_Internal(Vector2 cursorPosition, bool shift)
	{
		selectIndex = m_TextHandle.GetCursorIndexFromPosition(cursorPosition);
		if (!shift)
		{
			cursorIndex = selectIndex;
		}
	}

	public void SelectToPosition(Vector2 cursorPosition)
	{
		if (!m_MouseDragSelectsWholeWords)
		{
			cursorIndex = m_TextHandle.GetCursorIndexFromPosition(cursorPosition);
			return;
		}
		int cursorIndexFromPosition = m_TextHandle.GetCursorIndexFromPosition(cursorPosition);
		cursorIndexFromPosition = EnsureValidCodePointIndex(cursorIndexFromPosition);
		if (dblClickSnap == TextEditor.DblClickSnapping.WORDS)
		{
			if (cursorIndexFromPosition <= m_DblClickInitPosStart)
			{
				cursorIndex = FindEndOfClassification(cursorIndexFromPosition, Direction.Backward);
				selectIndex = FindEndOfClassification(m_DblClickInitPosEnd - 1, Direction.Forward);
			}
			else if (cursorIndexFromPosition >= m_DblClickInitPosEnd)
			{
				cursorIndex = FindEndOfClassification(cursorIndexFromPosition - 1, Direction.Forward);
				selectIndex = FindEndOfClassification(m_DblClickInitPosStart + 1, Direction.Backward);
			}
			else
			{
				cursorIndex = m_DblClickInitPosStart;
				selectIndex = m_DblClickInitPosEnd;
			}
		}
		else if (cursorIndexFromPosition <= m_DblClickInitPosStart)
		{
			if (cursorIndexFromPosition > 0)
			{
				cursorIndex = m_TextHandle.LastIndexOf('\n', Mathf.Max(0, cursorIndexFromPosition - 1)) + 1;
			}
			else
			{
				cursorIndex = 0;
			}
			selectIndex = m_TextHandle.LastIndexOf('\n', Mathf.Min(characterCount - 1, m_DblClickInitPosEnd + 1));
		}
		else if (cursorIndexFromPosition >= m_DblClickInitPosEnd)
		{
			if (cursorIndexFromPosition < characterCount)
			{
				cursorIndex = IndexOfEndOfLine(cursorIndexFromPosition);
			}
			else
			{
				cursorIndex = characterCount;
			}
			selectIndex = m_TextHandle.LastIndexOf('\n', Mathf.Max(0, m_DblClickInitPosEnd - 2)) + 1;
		}
		else
		{
			cursorIndex = m_DblClickInitPosStart;
			selectIndex = m_DblClickInitPosEnd;
		}
	}

	private int FindNextSeperator(int startPos)
	{
		int num = characterCount;
		while (startPos < num && ClassifyChar(startPos) != CharacterType.LetterLike)
		{
			startPos = NextCodePointIndex(startPos);
		}
		while (startPos < num && ClassifyChar(startPos) == CharacterType.LetterLike)
		{
			startPos = NextCodePointIndex(startPos);
		}
		return startPos;
	}

	private int FindPrevSeperator(int startPos)
	{
		startPos = PreviousCodePointIndex(startPos);
		while (startPos > 0 && ClassifyChar(startPos) != CharacterType.LetterLike)
		{
			startPos = PreviousCodePointIndex(startPos);
		}
		if (startPos == 0)
		{
			return 0;
		}
		while (startPos > 0 && ClassifyChar(startPos) == CharacterType.LetterLike)
		{
			startPos = PreviousCodePointIndex(startPos);
		}
		if (ClassifyChar(startPos) == CharacterType.LetterLike)
		{
			return startPos;
		}
		return NextCodePointIndex(startPos);
	}

	public int FindStartOfNextWord(int p)
	{
		int num = characterCount;
		if (p == num)
		{
			return p;
		}
		CharacterType characterType = ClassifyChar(p);
		if (characterType != CharacterType.WhiteSpace)
		{
			p = NextCodePointIndex(p);
			while (p < num && ClassifyChar(p) == characterType)
			{
				p = NextCodePointIndex(p);
			}
		}
		else if (m_TextElementInfos[p].character == '\t' || m_TextElementInfos[p].character == '\n')
		{
			return NextCodePointIndex(p);
		}
		if (p == num)
		{
			return p;
		}
		if (m_TextElementInfos[p].character == ' ')
		{
			while (p < num && ClassifyChar(p) == CharacterType.WhiteSpace)
			{
				p = NextCodePointIndex(p);
			}
		}
		else if (m_TextElementInfos[p].character == '\t' || m_TextElementInfos[p].character == '\n')
		{
			return p;
		}
		return p;
	}

	public int FindEndOfPreviousWord(int p)
	{
		if (p == 0)
		{
			return p;
		}
		p = PreviousCodePointIndex(p);
		while (p > 0 && m_TextElementInfos[p].character == ' ')
		{
			p = PreviousCodePointIndex(p);
		}
		CharacterType characterType = ClassifyChar(p);
		if (characterType != CharacterType.WhiteSpace)
		{
			while (p > 0 && ClassifyChar(PreviousCodePointIndex(p)) == characterType)
			{
				p = PreviousCodePointIndex(p);
			}
		}
		return p;
	}

	private int FindEndOfClassification(int p, Direction dir)
	{
		if (characterCount == 0)
		{
			return 0;
		}
		if (p == characterCount)
		{
			p = PreviousCodePointIndex(p);
		}
		CharacterType characterType = ClassifyChar(p);
		if (characterType == CharacterType.NewLine)
		{
			return p;
		}
		do
		{
			switch (dir)
			{
			case Direction.Backward:
				p = PreviousCodePointIndex(p);
				if (p == 0)
				{
					return (ClassifyChar(0) != characterType) ? NextCodePointIndex(0) : 0;
				}
				break;
			case Direction.Forward:
				p = NextCodePointIndex(p);
				if (p == characterCount)
				{
					return characterCount;
				}
				break;
			}
		}
		while (ClassifyChar(p) == characterType);
		if (dir == Direction.Forward)
		{
			return p;
		}
		return NextCodePointIndex(p);
	}

	private int ClampTextIndex(int index)
	{
		return Mathf.Clamp(index, 0, characterCount);
	}

	internal int EnsureValidCodePointIndex(int index)
	{
		index = ClampTextIndex(index);
		if (!IsValidCodePointIndex(index))
		{
			index = NextCodePointIndex(index);
		}
		return index;
	}

	private bool IsValidCodePointIndex(int index)
	{
		if (index < 0 || index > characterCount)
		{
			return false;
		}
		if (index == 0 || index == characterCount)
		{
			return true;
		}
		return !char.IsLowSurrogate(m_TextElementInfos[index].character);
	}

	private int IndexOfEndOfLine(int startIndex)
	{
		int num = m_TextHandle.IndexOf('\n', startIndex);
		return (num != -1) ? num : characterCount;
	}

	public int PreviousCodePointIndex(int index)
	{
		if (index > 0)
		{
			index--;
		}
		while (index > 0 && char.IsLowSurrogate(m_TextElementInfos[index].character))
		{
			index--;
		}
		return index;
	}

	public int NextCodePointIndex(int index)
	{
		if (index < characterCount)
		{
			index++;
		}
		while (index < characterCount && char.IsLowSurrogate(m_TextElementInfos[index].character))
		{
			index++;
		}
		return index;
	}

	private int GetGraphicalLineStart(int p)
	{
		Vector2 cursorPositionFromStringIndexUsingLineHeight = m_TextHandle.GetCursorPositionFromStringIndexUsingLineHeight(p);
		cursorPositionFromStringIndexUsingLineHeight.y -= 1f / GUIUtility.pixelsPerPoint;
		cursorPositionFromStringIndexUsingLineHeight.x = 0f;
		return m_TextHandle.GetCursorIndexFromPosition(cursorPositionFromStringIndexUsingLineHeight);
	}

	private int GetGraphicalLineEnd(int p)
	{
		Vector2 cursorPositionFromStringIndexUsingLineHeight = m_TextHandle.GetCursorPositionFromStringIndexUsingLineHeight(p);
		cursorPositionFromStringIndexUsingLineHeight.y -= 1f / GUIUtility.pixelsPerPoint;
		cursorPositionFromStringIndexUsingLineHeight.x += 5000f;
		return m_TextHandle.GetCursorIndexFromPosition(cursorPositionFromStringIndexUsingLineHeight);
	}

	public void Copy()
	{
		if (selectIndex != cursorIndex)
		{
			GUIUtility.systemCopyBuffer = selectedText;
		}
	}

	private CharacterType ClassifyChar(int index)
	{
		char character = m_TextElementInfos[index].character;
		if (character == '\n')
		{
			return CharacterType.NewLine;
		}
		if (char.IsWhiteSpace(character))
		{
			return CharacterType.WhiteSpace;
		}
		if (char.IsLetterOrDigit(character) || m_TextElementInfos[index].character == '\'')
		{
			return CharacterType.LetterLike;
		}
		return CharacterType.Symbol;
	}
}
