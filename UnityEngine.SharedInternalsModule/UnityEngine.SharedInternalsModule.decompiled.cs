using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Bindings;

[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: UnityEngineModuleAssembly]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine
{
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	internal sealed class AssetFileNameExtensionAttribute : Attribute
	{
		public string preferredExtension { get; }

		public IEnumerable<string> otherExtensions { get; }

		public AssetFileNameExtensionAttribute(string preferredExtension, params string[] otherExtensions)
		{
			this.preferredExtension = preferredExtension;
			this.otherExtensions = otherExtensions;
		}
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Method)]
	internal class ThreadAndSerializationSafeAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Struct)]
	[VisibleToOtherModules]
	internal class IL2CPPStructAlignmentAttribute : Attribute
	{
		public int Align;

		public IL2CPPStructAlignmentAttribute()
		{
			Align = 1;
		}
	}
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
	[VisibleToOtherModules]
	internal class WritableAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Class)]
	[VisibleToOtherModules]
	internal class RejectDragAndDropMaterial : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Assembly)]
	[VisibleToOtherModules]
	internal class UnityEngineModuleAssembly : Attribute
	{
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	internal sealed class NativeClassAttribute : Attribute
	{
		public string QualifiedNativeName { get; private set; }

		public string Declaration { get; private set; }

		public NativeClassAttribute(string qualifiedCppName)
		{
			QualifiedNativeName = qualifiedCppName;
			Declaration = "class " + qualifiedCppName;
		}

		public NativeClassAttribute(string qualifiedCppName, string declaration)
		{
			QualifiedNativeName = qualifiedCppName;
			Declaration = declaration;
		}
	}
	[VisibleToOtherModules]
	internal sealed class UnityString
	{
		public static string Format(string fmt, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture.NumberFormat, fmt, args);
		}
	}
}
namespace UnityEngine.Bindings
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[VisibleToOtherModules]
	internal class VisibleToOtherModulesAttribute : Attribute
	{
		public VisibleToOtherModulesAttribute()
		{
		}

		public VisibleToOtherModulesAttribute(params string[] modules)
		{
		}
	}
	internal interface IBindingsAttribute
	{
	}
	internal interface IBindingsNameProviderAttribute : IBindingsAttribute
	{
		string Name { get; set; }
	}
	internal interface IBindingsHeaderProviderAttribute : IBindingsAttribute
	{
		string Header { get; set; }
	}
	internal interface IBindingsIsThreadSafeProviderAttribute : IBindingsAttribute
	{
		bool IsThreadSafe { get; set; }
	}
	internal interface IBindingsIsFreeFunctionProviderAttribute : IBindingsAttribute
	{
		bool IsFreeFunction { get; set; }

		bool HasExplicitThis { get; set; }
	}
	internal interface IBindingsThrowsProviderAttribute : IBindingsAttribute
	{
		bool ThrowsException { get; set; }
	}
	internal interface IBindingsGenerateMarshallingTypeAttribute : IBindingsAttribute
	{
		CodegenOptions CodegenOptions { get; set; }
	}
	internal interface IBindingsWritableSelfProviderAttribute : IBindingsAttribute
	{
		bool WritableSelf { get; set; }
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property)]
	internal class NativeConditionalAttribute : Attribute, IBindingsAttribute
	{
		public string Condition { get; set; }

		public string StubReturnStatement { get; set; }

		public bool Enabled { get; set; }

		public NativeConditionalAttribute()
		{
		}

		public NativeConditionalAttribute(string condition)
		{
			Condition = condition;
			Enabled = true;
		}

		public NativeConditionalAttribute(bool enabled)
		{
			Enabled = enabled;
		}

		public NativeConditionalAttribute(string condition, bool enabled)
			: this(condition)
		{
			Enabled = enabled;
		}

		public NativeConditionalAttribute(string condition, string stubReturnStatement, bool enabled)
			: this(condition, stubReturnStatement)
		{
			Enabled = enabled;
		}

		public NativeConditionalAttribute(string condition, string stubReturnStatement)
			: this(condition)
		{
			StubReturnStatement = stubReturnStatement;
		}
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	internal class NativeHeaderAttribute : Attribute, IBindingsHeaderProviderAttribute, IBindingsAttribute
	{
		public string Header { get; set; }

		public NativeHeaderAttribute()
		{
		}

		public NativeHeaderAttribute(string header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			if (header == "")
			{
				throw new ArgumentException("header cannot be empty", "header");
			}
			Header = header;
		}
	}
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	[VisibleToOtherModules]
	internal class NativeNameAttribute : Attribute, IBindingsNameProviderAttribute, IBindingsAttribute
	{
		public string Name { get; set; }

		public NativeNameAttribute()
		{
		}

		public NativeNameAttribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == "")
			{
				throw new ArgumentException("name cannot be empty", "name");
			}
			Name = name;
		}
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class NativeWritableSelfAttribute : Attribute, IBindingsWritableSelfProviderAttribute, IBindingsAttribute
	{
		public bool WritableSelf { get; set; }

		public NativeWritableSelfAttribute()
		{
			WritableSelf = true;
		}

		public NativeWritableSelfAttribute(bool writable)
		{
			WritableSelf = writable;
		}
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	internal class NativeMethodAttribute : Attribute, IBindingsNameProviderAttribute, IBindingsAttribute, IBindingsIsThreadSafeProviderAttribute, IBindingsIsFreeFunctionProviderAttribute, IBindingsThrowsProviderAttribute
	{
		public string Name { get; set; }

		public bool IsThreadSafe { get; set; }

		public bool IsFreeFunction { get; set; }

		public bool ThrowsException { get; set; }

		public bool HasExplicitThis { get; set; }

		public bool WritableSelf { get; set; }

		public NativeMethodAttribute()
		{
		}

		public NativeMethodAttribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == "")
			{
				throw new ArgumentException("name cannot be empty", "name");
			}
			Name = name;
		}

		public NativeMethodAttribute(string name, bool isFreeFunction)
			: this(name)
		{
			IsFreeFunction = isFreeFunction;
		}

		public NativeMethodAttribute(string name, bool isFreeFunction, bool isThreadSafe)
			: this(name, isFreeFunction)
		{
			IsThreadSafe = isThreadSafe;
		}

		public NativeMethodAttribute(string name, bool isFreeFunction, bool isThreadSafe, bool throws)
			: this(name, isFreeFunction, isThreadSafe)
		{
			ThrowsException = throws;
		}
	}
	[VisibleToOtherModules]
	internal enum TargetType
	{
		Function,
		Field
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Property)]
	internal class NativePropertyAttribute : NativeMethodAttribute
	{
		public TargetType TargetType { get; set; }

		public NativePropertyAttribute()
		{
		}

		public NativePropertyAttribute(string name)
			: base(name)
		{
		}

		public NativePropertyAttribute(string name, TargetType targetType)
			: base(name)
		{
			TargetType = targetType;
		}

		public NativePropertyAttribute(string name, bool isFree, TargetType targetType)
			: base(name, isFree)
		{
			TargetType = targetType;
		}

		public NativePropertyAttribute(string name, bool isFree, TargetType targetType, bool isThreadSafe)
			: base(name, isFree, isThreadSafe)
		{
			TargetType = targetType;
		}
	}
	[VisibleToOtherModules]
	internal enum CodegenOptions
	{
		Auto,
		Custom,
		Force
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class)]
	internal class NativeAsStructAttribute : Attribute, IBindingsAttribute
	{
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
	internal class NativeTypeAttribute : Attribute, IBindingsHeaderProviderAttribute, IBindingsAttribute, IBindingsGenerateMarshallingTypeAttribute
	{
		public string Header { get; set; }

		public string IntermediateScriptingStructName { get; set; }

		public CodegenOptions CodegenOptions { get; set; }

		public NativeTypeAttribute()
		{
			CodegenOptions = CodegenOptions.Auto;
		}

		public NativeTypeAttribute(CodegenOptions codegenOptions)
		{
			CodegenOptions = codegenOptions;
		}

		public NativeTypeAttribute(string header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			if (header == "")
			{
				throw new ArgumentException("header cannot be empty", "header");
			}
			CodegenOptions = CodegenOptions.Auto;
			Header = header;
		}

		public NativeTypeAttribute(string header, CodegenOptions codegenOptions)
			: this(header)
		{
			CodegenOptions = codegenOptions;
		}

		public NativeTypeAttribute(CodegenOptions codegenOptions, string intermediateStructName)
			: this(codegenOptions)
		{
			IntermediateScriptingStructName = intermediateStructName;
		}
	}
	[AttributeUsage(AttributeTargets.Parameter)]
	[VisibleToOtherModules]
	internal class NotNullAttribute : Attribute, IBindingsAttribute
	{
		public string Exception { get; set; }

		public NotNullAttribute(string exception = "ArgumentNullException")
		{
			Exception = exception;
		}
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Parameter)]
	internal class UnityTypeAttribute : Attribute, IBindingsAttribute
	{
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Parameter)]
	internal class UnmarshalledAttribute : Attribute, IBindingsAttribute
	{
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Method)]
	internal class FreeFunctionAttribute : NativeMethodAttribute
	{
		public FreeFunctionAttribute()
		{
			base.IsFreeFunction = true;
		}

		public FreeFunctionAttribute(string name)
			: base(name, isFreeFunction: true)
		{
		}

		public FreeFunctionAttribute(string name, bool isThreadSafe)
			: base(name, isFreeFunction: true, isThreadSafe)
		{
		}
	}
	[AttributeUsage(AttributeTargets.Method)]
	[VisibleToOtherModules]
	internal class ThreadSafeAttribute : NativeMethodAttribute
	{
		public ThreadSafeAttribute()
		{
			base.IsThreadSafe = true;
		}
	}
	[VisibleToOtherModules]
	internal enum StaticAccessorType
	{
		Dot,
		Arrow,
		DoubleColon,
		ArrowWithDefaultReturnIfNull
	}
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property)]
	[VisibleToOtherModules]
	internal class StaticAccessorAttribute : Attribute, IBindingsAttribute
	{
		public string Name { get; set; }

		public StaticAccessorType Type { get; set; }

		public StaticAccessorAttribute()
		{
		}

		[VisibleToOtherModules]
		internal StaticAccessorAttribute(string name)
		{
			Name = name;
		}

		public StaticAccessorAttribute(StaticAccessorType type)
		{
			Type = type;
		}

		public StaticAccessorAttribute(string name, StaticAccessorType type)
		{
			Name = name;
			Type = type;
		}
	}
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	[VisibleToOtherModules]
	internal class NativeThrowsAttribute : Attribute, IBindingsThrowsProviderAttribute, IBindingsAttribute
	{
		public bool ThrowsException { get; set; }

		public NativeThrowsAttribute()
		{
			ThrowsException = true;
		}

		public NativeThrowsAttribute(bool throwsException)
		{
			ThrowsException = throwsException;
		}
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Field)]
	internal class IgnoreAttribute : Attribute, IBindingsAttribute
	{
		public bool DoesNotContributeToSize { get; set; }
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class)]
	internal class MarshalUnityObjectAs : Attribute, IBindingsAttribute
	{
		public Type MarshalAsType { get; set; }

		public MarshalUnityObjectAs(Type marshalAsType)
		{
			MarshalAsType = marshalAsType;
		}
	}
	[VisibleToOtherModules]
	internal enum PreventExecutionSeverity
	{
		PreventExecution_Error,
		PreventExecution_ManagedException,
		PreventExecution_Warning
	}
	[VisibleToOtherModules]
	internal interface IBindingsPreventExecution
	{
		object singleFlagValue { get; set; }

		PreventExecutionSeverity severity { get; set; }

		string howToFix { get; set; }
	}
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
	[VisibleToOtherModules]
	internal class PreventExecutionInStateAttribute : Attribute, IBindingsPreventExecution
	{
		public object singleFlagValue { get; set; }

		public PreventExecutionSeverity severity { get; set; }

		public string howToFix { get; set; }

		public PreventExecutionInStateAttribute(object systemAndFlags, PreventExecutionSeverity reportSeverity, string howToString = "")
		{
			singleFlagValue = systemAndFlags;
			severity = reportSeverity;
			howToFix = howToString;
		}
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	internal class PreventReadOnlyInstanceModificationAttribute : Attribute
	{
	}
	[VisibleToOtherModules]
	internal interface IBindingsMarshalAsSpan
	{
		bool IsReadOnly { get; }

		string SizeParameter { get; }
	}
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	internal class SpanAttribute : Attribute, IBindingsMarshalAsSpan
	{
		public bool IsReadOnly { get; }

		public string SizeParameter { get; }

		public SpanAttribute(string sizeParameter, bool isReadOnly = false)
		{
			SizeParameter = sizeParameter;
			IsReadOnly = isReadOnly;
		}
	}
}
namespace UnityEngine.Scripting
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, Inherited = false)]
	[VisibleToOtherModules]
	internal class UsedByNativeCodeAttribute : Attribute
	{
		public string Name { get; set; }

		public UsedByNativeCodeAttribute()
		{
		}

		public UsedByNativeCodeAttribute(string name)
		{
			Name = name;
		}
	}
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, Inherited = false)]
	[VisibleToOtherModules]
	internal class RequiredByNativeCodeAttribute : Attribute
	{
		public string Name { get; set; }

		public bool Optional { get; set; }

		public bool GenerateProxy { get; set; }

		public RequiredByNativeCodeAttribute()
		{
		}

		public RequiredByNativeCodeAttribute(string name)
		{
			Name = name;
		}

		public RequiredByNativeCodeAttribute(bool optional)
		{
			Optional = optional;
		}

		public RequiredByNativeCodeAttribute(string name, bool optional)
		{
			Name = name;
			Optional = optional;
		}
	}
}
