using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Properties.Internal;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Scripting;

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
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.Properties.Reflection.Tests")]
[assembly: InternalsVisibleTo("PropertyBags.GenerationTests")]
[assembly: InternalsVisibleTo("Unity.Properties.CodeGen.IntegrationTests")]
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
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.Properties.Tests")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace Unity.Properties
{
	[Flags]
	public enum VisitExceptionKind
	{
		None = 0,
		Internal = 1,
		Visitor = 2,
		All = 3
	}
	public struct VisitParameters
	{
		public VisitExceptionKind IgnoreExceptions { get; set; }
	}
	public static class PropertyContainer
	{
		private class GetPropertyVisitor : PathVisitor
		{
			public static readonly ObjectPool<GetPropertyVisitor> Pool = new ObjectPool<GetPropertyVisitor>(() => new GetPropertyVisitor(), null, delegate(GetPropertyVisitor v)
			{
				v.Reset();
			});

			public IProperty Property;

			public override void Reset()
			{
				base.Reset();
				Property = null;
				base.ReadonlyVisit = true;
			}

			protected override void VisitPath<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
			{
				Property = property;
			}
		}

		private class GetValueVisitor<TSrcValue> : PathVisitor
		{
			public static readonly ObjectPool<GetValueVisitor<TSrcValue>> Pool = new ObjectPool<GetValueVisitor<TSrcValue>>(() => new GetValueVisitor<TSrcValue>(), null, delegate(GetValueVisitor<TSrcValue> v)
			{
				v.Reset();
			});

			public TSrcValue Value;

			public override void Reset()
			{
				base.Reset();
				Value = default(TSrcValue);
				base.ReadonlyVisit = true;
			}

			protected override void VisitPath<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
			{
				if (!TypeConversion.TryConvert<TValue, TSrcValue>(ref value, out Value))
				{
					base.ReturnCode = VisitReturnCode.InvalidCast;
				}
			}
		}

		private class ValueAtPathVisitor : PathVisitor
		{
			public static readonly ObjectPool<ValueAtPathVisitor> Pool = new ObjectPool<ValueAtPathVisitor>(() => new ValueAtPathVisitor(), null, delegate(ValueAtPathVisitor v)
			{
				v.Reset();
			});

			public IPropertyVisitor Visitor;

			public override void Reset()
			{
				base.Reset();
				Visitor = null;
				base.ReadonlyVisit = true;
			}

			protected override void VisitPath<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
			{
				((IPropertyAccept<TContainer>)property).Accept(Visitor, ref container);
			}
		}

		private class ExistsAtPathVisitor : PathVisitor
		{
			public static readonly ObjectPool<ExistsAtPathVisitor> Pool = new ObjectPool<ExistsAtPathVisitor>(() => new ExistsAtPathVisitor(), null, delegate(ExistsAtPathVisitor v)
			{
				v.Reset();
			});

			public bool Exists;

			public override void Reset()
			{
				base.Reset();
				Exists = false;
				base.ReadonlyVisit = true;
			}

			protected override void VisitPath<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
			{
				Exists = true;
			}
		}

		internal class SetValueVisitor<TSrcValue> : PathVisitor
		{
			public static readonly ObjectPool<SetValueVisitor<TSrcValue>> Pool = new ObjectPool<SetValueVisitor<TSrcValue>>(() => new SetValueVisitor<TSrcValue>(), null, delegate(SetValueVisitor<TSrcValue> v)
			{
				v.Reset();
			});

			public TSrcValue Value;

			public override void Reset()
			{
				base.Reset();
				Value = default(TSrcValue);
			}

			protected override void VisitPath<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
			{
				TValue destination;
				if (property.IsReadOnly)
				{
					base.ReturnCode = VisitReturnCode.AccessViolation;
				}
				else if (TypeConversion.TryConvert<TSrcValue, TValue>(ref Value, out destination))
				{
					property.SetValue(ref container, destination);
				}
				else
				{
					base.ReturnCode = VisitReturnCode.InvalidCast;
				}
			}
		}

		public static void Accept<TContainer>(IPropertyBagVisitor visitor, TContainer container, VisitParameters parameters = default(VisitParameters))
		{
			VisitReturnCode returnCode = VisitReturnCode.Ok;
			try
			{
				if (TryAccept(visitor, ref container, out returnCode, parameters))
				{
					return;
				}
			}
			catch (Exception)
			{
				if ((parameters.IgnoreExceptions & VisitExceptionKind.Visitor) == 0)
				{
					throw;
				}
			}
			if ((parameters.IgnoreExceptions & VisitExceptionKind.Internal) == 0)
			{
				switch (returnCode)
				{
				case VisitReturnCode.Ok:
				case VisitReturnCode.InvalidContainerType:
					break;
				case VisitReturnCode.NullContainer:
					throw new ArgumentException("The given container was null. Visitation only works for valid non-null containers.");
				case VisitReturnCode.MissingPropertyBag:
					throw new MissingPropertyBagException(container.GetType());
				default:
					throw new Exception(string.Format("Unexpected {0}=[{1}]", "VisitReturnCode", returnCode));
				}
			}
		}

		public static void Accept<TContainer>(IPropertyBagVisitor visitor, ref TContainer container, VisitParameters parameters = default(VisitParameters))
		{
			VisitReturnCode returnCode = VisitReturnCode.Ok;
			try
			{
				if (TryAccept(visitor, ref container, out returnCode, parameters))
				{
					return;
				}
			}
			catch (Exception)
			{
				if ((parameters.IgnoreExceptions & VisitExceptionKind.Visitor) == 0)
				{
					throw;
				}
			}
			if ((parameters.IgnoreExceptions & VisitExceptionKind.Internal) == 0)
			{
				switch (returnCode)
				{
				case VisitReturnCode.Ok:
				case VisitReturnCode.InvalidContainerType:
					break;
				case VisitReturnCode.NullContainer:
					throw new ArgumentException("The given container was null. Visitation only works for valid non-null containers.");
				case VisitReturnCode.MissingPropertyBag:
					throw new MissingPropertyBagException(container.GetType());
				default:
					throw new Exception(string.Format("Unexpected {0}=[{1}]", "VisitReturnCode", returnCode));
				}
			}
		}

		public static bool TryAccept<TContainer>(IPropertyBagVisitor visitor, ref TContainer container, VisitParameters parameters = default(VisitParameters))
		{
			VisitReturnCode returnCode;
			return TryAccept(visitor, ref container, out returnCode, parameters);
		}

		public static bool TryAccept<TContainer>(IPropertyBagVisitor visitor, ref TContainer container, out VisitReturnCode returnCode, VisitParameters parameters = default(VisitParameters))
		{
			if (!TypeTraits<TContainer>.IsContainer)
			{
				returnCode = VisitReturnCode.InvalidContainerType;
				return false;
			}
			if (TypeTraits<TContainer>.CanBeNull && EqualityComparer<TContainer>.Default.Equals(container, default(TContainer)))
			{
				returnCode = VisitReturnCode.NullContainer;
				return false;
			}
			if (!TypeTraits<TContainer>.IsValueType && typeof(TContainer) != container.GetType())
			{
				if (!TypeTraits.IsContainer(container.GetType()))
				{
					returnCode = VisitReturnCode.InvalidContainerType;
					return false;
				}
				IPropertyBag propertyBag = PropertyBagStore.GetPropertyBag(container.GetType());
				if (propertyBag == null)
				{
					returnCode = VisitReturnCode.MissingPropertyBag;
					return false;
				}
				object container2 = container;
				propertyBag.Accept(visitor, ref container2);
				container = (TContainer)container2;
			}
			else
			{
				IPropertyBag<TContainer> propertyBag2 = PropertyBagStore.GetPropertyBag<TContainer>();
				if (propertyBag2 == null)
				{
					returnCode = VisitReturnCode.MissingPropertyBag;
					return false;
				}
				PropertyBag.AcceptWithSpecializedVisitor(propertyBag2, visitor, ref container);
			}
			returnCode = VisitReturnCode.Ok;
			return true;
		}

		public static void Accept<TContainer>(IPropertyVisitor visitor, ref TContainer container, in PropertyPath path, VisitParameters parameters = default(VisitParameters))
		{
			ValueAtPathVisitor valueAtPathVisitor = ValueAtPathVisitor.Pool.Get();
			try
			{
				valueAtPathVisitor.Path = path;
				valueAtPathVisitor.Visitor = visitor;
				Accept(valueAtPathVisitor, ref container, parameters);
				if ((parameters.IgnoreExceptions & VisitExceptionKind.Internal) == 0)
				{
					switch (valueAtPathVisitor.ReturnCode)
					{
					case VisitReturnCode.Ok:
						break;
					case VisitReturnCode.InvalidPath:
						throw new InvalidPathException($"Failed to Visit at Path=[{path}]");
					default:
						throw new Exception(string.Format("Unexpected {0}=[{1}]", "VisitReturnCode", valueAtPathVisitor.ReturnCode));
					}
				}
			}
			finally
			{
				ValueAtPathVisitor.Pool.Release(valueAtPathVisitor);
			}
		}

		public static bool TryAccept<TContainer>(IPropertyVisitor visitor, ref TContainer container, in PropertyPath path, out VisitReturnCode returnCode, VisitParameters parameters = default(VisitParameters))
		{
			ValueAtPathVisitor valueAtPathVisitor = ValueAtPathVisitor.Pool.Get();
			try
			{
				valueAtPathVisitor.Path = path;
				valueAtPathVisitor.Visitor = visitor;
				return TryAccept(valueAtPathVisitor, ref container, out returnCode, parameters);
			}
			finally
			{
				ValueAtPathVisitor.Pool.Release(valueAtPathVisitor);
			}
		}

		public static IProperty GetProperty<TContainer>(TContainer container, in PropertyPath path)
		{
			return GetProperty(ref container, in path);
		}

		public static IProperty GetProperty<TContainer>(ref TContainer container, in PropertyPath path)
		{
			if (TryGetProperty(ref container, in path, out var property, out var returnCode))
			{
				return property;
			}
			switch (returnCode)
			{
			case VisitReturnCode.NullContainer:
				throw new ArgumentNullException("container");
			case VisitReturnCode.InvalidContainerType:
				throw new InvalidContainerTypeException(container.GetType());
			case VisitReturnCode.MissingPropertyBag:
				throw new MissingPropertyBagException(container.GetType());
			case VisitReturnCode.InvalidPath:
				throw new ArgumentException($"Failed to get property for path=[{path}]");
			default:
				throw new Exception(string.Format("Unexpected {0}=[{1}]", "VisitReturnCode", returnCode));
			}
		}

		public static bool TryGetProperty<TContainer>(TContainer container, in PropertyPath path, out IProperty property)
		{
			VisitReturnCode returnCode;
			return TryGetProperty(ref container, in path, out property, out returnCode);
		}

		public static bool TryGetProperty<TContainer>(ref TContainer container, in PropertyPath path, out IProperty property)
		{
			VisitReturnCode returnCode;
			return TryGetProperty(ref container, in path, out property, out returnCode);
		}

		public static bool TryGetProperty<TContainer>(ref TContainer container, in PropertyPath path, out IProperty property, out VisitReturnCode returnCode)
		{
			GetPropertyVisitor getPropertyVisitor = GetPropertyVisitor.Pool.Get();
			try
			{
				getPropertyVisitor.Path = path;
				if (!TryAccept(getPropertyVisitor, ref container, out returnCode))
				{
					property = null;
					return false;
				}
				returnCode = getPropertyVisitor.ReturnCode;
				property = getPropertyVisitor.Property;
				return returnCode == VisitReturnCode.Ok;
			}
			finally
			{
				GetPropertyVisitor.Pool.Release(getPropertyVisitor);
			}
		}

		public static TValue GetValue<TContainer, TValue>(TContainer container, string name)
		{
			return GetValue<TContainer, TValue>(ref container, name);
		}

		public static TValue GetValue<TContainer, TValue>(ref TContainer container, string name)
		{
			return GetValue<TContainer, TValue>(ref container, new PropertyPath(name));
		}

		public static TValue GetValue<TContainer, TValue>(TContainer container, in PropertyPath path)
		{
			return GetValue<TContainer, TValue>(ref container, in path);
		}

		public static TValue GetValue<TContainer, TValue>(ref TContainer container, in PropertyPath path)
		{
			if (path.IsEmpty)
			{
				throw new InvalidPathException("The specified PropertyPath is empty.");
			}
			if (TryGetValue<TContainer, TValue>(ref container, in path, out var value, out var returnCode))
			{
				return value;
			}
			switch (returnCode)
			{
			case VisitReturnCode.NullContainer:
				throw new ArgumentNullException("container");
			case VisitReturnCode.InvalidContainerType:
				throw new InvalidContainerTypeException(container.GetType());
			case VisitReturnCode.MissingPropertyBag:
				throw new MissingPropertyBagException(container.GetType());
			case VisitReturnCode.InvalidCast:
				throw new InvalidCastException($"Failed to GetValue of Type=[{typeof(TValue).Name}] for property with path=[{path}]");
			case VisitReturnCode.InvalidPath:
				throw new InvalidPathException($"Failed to GetValue for property with Path=[{path}]");
			default:
				throw new Exception(string.Format("Unexpected {0}=[{1}]", "VisitReturnCode", returnCode));
			}
		}

		public static bool TryGetValue<TContainer, TValue>(TContainer container, string name, out TValue value)
		{
			return TryGetValue<TContainer, TValue>(ref container, name, out value);
		}

		public static bool TryGetValue<TContainer, TValue>(ref TContainer container, string name, out TValue value)
		{
			VisitReturnCode returnCode;
			return TryGetValue<TContainer, TValue>(ref container, new PropertyPath(name), out value, out returnCode);
		}

		public static bool TryGetValue<TContainer, TValue>(TContainer container, in PropertyPath path, out TValue value)
		{
			VisitReturnCode returnCode;
			return TryGetValue<TContainer, TValue>(ref container, in path, out value, out returnCode);
		}

		public static bool TryGetValue<TContainer, TValue>(ref TContainer container, in PropertyPath path, out TValue value)
		{
			VisitReturnCode returnCode;
			return TryGetValue<TContainer, TValue>(ref container, in path, out value, out returnCode);
		}

		public static bool TryGetValue<TContainer, TValue>(ref TContainer container, in PropertyPath path, out TValue value, out VisitReturnCode returnCode)
		{
			if (path.IsEmpty)
			{
				returnCode = VisitReturnCode.InvalidPath;
				value = default(TValue);
				return false;
			}
			GetValueVisitor<TValue> getValueVisitor = GetValueVisitor<TValue>.Pool.Get();
			getValueVisitor.Path = path;
			getValueVisitor.ReadonlyVisit = true;
			try
			{
				if (!TryAccept(getValueVisitor, ref container, out returnCode))
				{
					value = default(TValue);
					return false;
				}
				value = getValueVisitor.Value;
				returnCode = getValueVisitor.ReturnCode;
			}
			finally
			{
				GetValueVisitor<TValue>.Pool.Release(getValueVisitor);
			}
			return returnCode == VisitReturnCode.Ok;
		}

		public static bool IsPathValid<TContainer>(TContainer container, string path)
		{
			return IsPathValid(ref container, new PropertyPath(path));
		}

		public static bool IsPathValid<TContainer>(TContainer container, in PropertyPath path)
		{
			return IsPathValid(ref container, in path);
		}

		public static bool IsPathValid<TContainer>(ref TContainer container, string path)
		{
			ExistsAtPathVisitor existsAtPathVisitor = ExistsAtPathVisitor.Pool.Get();
			try
			{
				existsAtPathVisitor.Path = new PropertyPath(path);
				TryAccept(existsAtPathVisitor, ref container);
				return existsAtPathVisitor.Exists;
			}
			finally
			{
				ExistsAtPathVisitor.Pool.Release(existsAtPathVisitor);
			}
		}

		public static bool IsPathValid<TContainer>(ref TContainer container, in PropertyPath path)
		{
			ExistsAtPathVisitor existsAtPathVisitor = ExistsAtPathVisitor.Pool.Get();
			try
			{
				existsAtPathVisitor.Path = path;
				TryAccept(existsAtPathVisitor, ref container);
				return existsAtPathVisitor.Exists;
			}
			finally
			{
				ExistsAtPathVisitor.Pool.Release(existsAtPathVisitor);
			}
		}

		public static void SetValue<TContainer, TValue>(TContainer container, string name, TValue value)
		{
			SetValue(ref container, name, value);
		}

		public static void SetValue<TContainer, TValue>(ref TContainer container, string name, TValue value)
		{
			SetValue(ref container, new PropertyPath(name), value);
		}

		public static void SetValue<TContainer, TValue>(TContainer container, in PropertyPath path, TValue value)
		{
			SetValue(ref container, in path, value);
		}

		public static void SetValue<TContainer, TValue>(ref TContainer container, in PropertyPath path, TValue value)
		{
			if (path.Length == 0)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length <= 0)
			{
				throw new InvalidPathException("The specified PropertyPath is empty.");
			}
			if (TrySetValue(ref container, in path, value, out var returnCode))
			{
				return;
			}
			switch (returnCode)
			{
			case VisitReturnCode.NullContainer:
				throw new ArgumentNullException("container");
			case VisitReturnCode.InvalidContainerType:
				throw new InvalidContainerTypeException(container.GetType());
			case VisitReturnCode.MissingPropertyBag:
				throw new MissingPropertyBagException(container.GetType());
			case VisitReturnCode.InvalidCast:
				throw new InvalidCastException($"Failed to SetValue of Type=[{typeof(TValue).Name}] for property with path=[{path}]");
			case VisitReturnCode.InvalidPath:
				throw new InvalidPathException($"Failed to SetValue for property with Path=[{path}]");
			case VisitReturnCode.AccessViolation:
				throw new AccessViolationException($"Failed to SetValue for read-only property with Path=[{path}]");
			default:
				throw new Exception(string.Format("Unexpected {0}=[{1}]", "VisitReturnCode", returnCode));
			}
		}

		public static bool TrySetValue<TContainer, TValue>(TContainer container, string name, TValue value)
		{
			return TrySetValue(ref container, name, value);
		}

		public static bool TrySetValue<TContainer, TValue>(ref TContainer container, string name, TValue value)
		{
			return TrySetValue(ref container, new PropertyPath(name), value);
		}

		public static bool TrySetValue<TContainer, TValue>(TContainer container, in PropertyPath path, TValue value)
		{
			return TrySetValue(ref container, in path, value);
		}

		public static bool TrySetValue<TContainer, TValue>(ref TContainer container, in PropertyPath path, TValue value)
		{
			VisitReturnCode returnCode;
			return TrySetValue(ref container, in path, value, out returnCode);
		}

		public static bool TrySetValue<TContainer, TValue>(ref TContainer container, in PropertyPath path, TValue value, out VisitReturnCode returnCode)
		{
			if (path.IsEmpty)
			{
				returnCode = VisitReturnCode.InvalidPath;
				return false;
			}
			SetValueVisitor<TValue> setValueVisitor = SetValueVisitor<TValue>.Pool.Get();
			setValueVisitor.Path = path;
			setValueVisitor.Value = value;
			try
			{
				if (!TryAccept(setValueVisitor, ref container, out returnCode))
				{
					return false;
				}
				returnCode = setValueVisitor.ReturnCode;
			}
			finally
			{
				SetValueVisitor<TValue>.Pool.Release(setValueVisitor);
			}
			return returnCode == VisitReturnCode.Ok;
		}
	}
	public enum VisitReturnCode
	{
		Ok,
		NullContainer,
		InvalidContainerType,
		MissingPropertyBag,
		InvalidPath,
		InvalidCast,
		AccessViolation
	}
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public class GeneratePropertyBagsForAssemblyAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class CreatePropertyAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Field)]
	public class DontCreatePropertyAttribute : Attribute
	{
	}
	[Flags]
	public enum TypeGenerationOptions
	{
		None = 0,
		ValueType = 2,
		ReferenceType = 4,
		Default = 6
	}
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class GeneratePropertyBagsForTypesQualifiedWithAttribute : Attribute
	{
		public Type Type { get; }

		public TypeGenerationOptions Options { get; }

		public GeneratePropertyBagsForTypesQualifiedWithAttribute(Type type, TypeGenerationOptions options = TypeGenerationOptions.Default)
		{
			if (type == null)
			{
				throw new ArgumentException("type is null.");
			}
			if (!type.IsInterface)
			{
				throw new ArgumentException("GeneratePropertyBagsForTypesQualifiedWithAttribute Type must be an interface type.");
			}
			Type = type;
			Options = options;
		}
	}
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class GeneratePropertyBagsForTypeAttribute : Attribute
	{
		public Type Type { get; }

		public GeneratePropertyBagsForTypeAttribute(Type type)
		{
			if (!TypeTraits.IsContainer(type))
			{
				throw new ArgumentException(type.Name + " is not a valid container type.");
			}
			Type = type;
		}
	}
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	public class GeneratePropertyBagAttribute : Attribute
	{
	}
	[Serializable]
	public class MissingPropertyBagException : Exception
	{
		public Type Type { get; }

		public MissingPropertyBagException(Type type)
			: base(GetMessageForType(type))
		{
			Type = type;
		}

		public MissingPropertyBagException(Type type, Exception inner)
			: base(GetMessageForType(type), inner)
		{
			Type = type;
		}

		private static string GetMessageForType(Type type)
		{
			return "No PropertyBag was found for Type=[" + type.FullName + "]. Please make sure all types are declared ahead of time using [GeneratePropertyBagAttribute], [GeneratePropertyBagsForTypeAttribute] or [GeneratePropertyBagsForTypesQualifiedWithAttribute]";
		}
	}
	[Serializable]
	public class InvalidContainerTypeException : Exception
	{
		public Type Type { get; }

		public InvalidContainerTypeException(Type type)
			: base(GetMessageForType(type))
		{
			Type = type;
		}

		public InvalidContainerTypeException(Type type, Exception inner)
			: base(GetMessageForType(type), inner)
		{
			Type = type;
		}

		private static string GetMessageForType(Type type)
		{
			return "Invalid container Type=[" + type.Name + "." + type.Name + "]";
		}
	}
	[Serializable]
	public class InvalidPathException : Exception
	{
		public InvalidPathException(string message)
			: base(message)
		{
		}

		public InvalidPathException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
	public readonly struct AttributesScope : IDisposable
	{
		private readonly IAttributes m_Target;

		private readonly List<Attribute> m_Previous;

		public AttributesScope(IProperty target, IProperty source)
		{
			m_Target = target as IAttributes;
			m_Previous = (target as IAttributes)?.Attributes;
			if (m_Target != null)
			{
				m_Target.Attributes = (source as IAttributes)?.Attributes;
			}
		}

		internal AttributesScope(IAttributes target, List<Attribute> attributes)
		{
			m_Target = target;
			m_Previous = target.Attributes;
			target.Attributes = attributes;
		}

		public void Dispose()
		{
			if (m_Target != null)
			{
				m_Target.Attributes = m_Previous;
			}
		}
	}
	public delegate TValue PropertyGetter<TContainer, out TValue>(ref TContainer container);
	public delegate void PropertySetter<TContainer, in TValue>(ref TContainer container, TValue value);
	public class DelegateProperty<TContainer, TValue> : Property<TContainer, TValue>
	{
		private readonly PropertyGetter<TContainer, TValue> m_Getter;

		private readonly PropertySetter<TContainer, TValue> m_Setter;

		public override string Name { get; }

		public override bool IsReadOnly => m_Setter == null;

		public DelegateProperty(string name, PropertyGetter<TContainer, TValue> getter, PropertySetter<TContainer, TValue> setter = null)
		{
			Name = name;
			m_Getter = getter ?? throw new ArgumentException("getter");
			m_Setter = setter;
		}

		public override TValue GetValue(ref TContainer container)
		{
			return m_Getter(ref container);
		}

		public override void SetValue(ref TContainer container, TValue value)
		{
			if (IsReadOnly)
			{
				throw new InvalidOperationException("Property is ReadOnly.");
			}
			m_Setter(ref container, value);
		}
	}
	public interface ICollectionElementProperty
	{
	}
	public interface IListElementProperty : ICollectionElementProperty
	{
		int Index { get; }
	}
	public interface ISetElementProperty : ICollectionElementProperty
	{
		object ObjectKey { get; }
	}
	public interface ISetElementProperty<out TKey> : ISetElementProperty, ICollectionElementProperty
	{
		TKey Key { get; }
	}
	public interface IDictionaryElementProperty : ICollectionElementProperty
	{
		object ObjectKey { get; }
	}
	public interface IDictionaryElementProperty<out TKey> : IDictionaryElementProperty, ICollectionElementProperty
	{
		TKey Key { get; }
	}
	public interface IProperty
	{
		string Name { get; }

		bool IsReadOnly { get; }

		Type DeclaredValueType();

		bool HasAttribute<TAttribute>() where TAttribute : Attribute;

		TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute;

		IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute;

		IEnumerable<Attribute> GetAttributes();
	}
	public interface IProperty<TContainer> : IProperty, IPropertyAccept<TContainer>
	{
		object GetValue(ref TContainer container);

		void SetValue(ref TContainer container, object value);
	}
	public abstract class Property<TContainer, TValue> : IProperty<TContainer>, IProperty, IPropertyAccept<TContainer>, IAttributes
	{
		private List<Attribute> m_Attributes;

		List<Attribute> IAttributes.Attributes
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

		public abstract string Name { get; }

		public abstract bool IsReadOnly { get; }

		public Type DeclaredValueType()
		{
			return typeof(TValue);
		}

		public void Accept(IPropertyVisitor visitor, ref TContainer container)
		{
			visitor.Visit(this, ref container);
		}

		object IProperty<TContainer>.GetValue(ref TContainer container)
		{
			return GetValue(ref container);
		}

		void IProperty<TContainer>.SetValue(ref TContainer container, object value)
		{
			SetValue(ref container, TypeConversion.Convert<object, TValue>(ref value));
		}

		public abstract TValue GetValue(ref TContainer container);

		public abstract void SetValue(ref TContainer container, TValue value);

		protected void AddAttribute(Attribute attribute)
		{
			((IAttributes)this).AddAttribute(attribute);
		}

		protected void AddAttributes(IEnumerable<Attribute> attributes)
		{
			((IAttributes)this).AddAttributes(attributes);
		}

		void IAttributes.AddAttribute(Attribute attribute)
		{
			if (attribute != null && !(attribute.GetType() == typeof(CreatePropertyAttribute)))
			{
				if (m_Attributes == null)
				{
					m_Attributes = new List<Attribute>();
				}
				m_Attributes.Add(attribute);
			}
		}

		void IAttributes.AddAttributes(IEnumerable<Attribute> attributes)
		{
			if (m_Attributes == null)
			{
				m_Attributes = new List<Attribute>();
			}
			foreach (Attribute attribute in attributes)
			{
				if (attribute != null && !(attribute.GetType() == typeof(CreatePropertyAttribute)))
				{
					m_Attributes.Add(attribute);
				}
			}
		}

		public bool HasAttribute<TAttribute>() where TAttribute : Attribute
		{
			for (int i = 0; i < m_Attributes?.Count; i++)
			{
				if (m_Attributes[i] is TAttribute)
				{
					return true;
				}
			}
			return false;
		}

		public TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute
		{
			for (int i = 0; i < m_Attributes?.Count; i++)
			{
				if (m_Attributes[i] is TAttribute result)
				{
					return result;
				}
			}
			return null;
		}

		public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute
		{
			for (int i = 0; i < m_Attributes?.Count; i++)
			{
				Attribute attribute = m_Attributes[i];
				if (attribute is TAttribute typed)
				{
					yield return typed;
				}
			}
		}

		public IEnumerable<Attribute> GetAttributes()
		{
			for (int i = 0; i < m_Attributes?.Count; i++)
			{
				yield return m_Attributes[i];
			}
		}

		AttributesScope IAttributes.CreateAttributesScope(IAttributes attributes)
		{
			return new AttributesScope(this, attributes?.Attributes);
		}
	}
	public enum PropertyPathPartKind
	{
		Name,
		Index,
		Key
	}
	public readonly struct PropertyPathPart : IEquatable<PropertyPathPart>
	{
		private readonly PropertyPathPartKind m_Kind;

		private readonly string m_Name;

		private readonly int m_Index;

		private readonly object m_Key;

		public bool IsName => Kind == PropertyPathPartKind.Name;

		public bool IsIndex => Kind == PropertyPathPartKind.Index;

		public bool IsKey => Kind == PropertyPathPartKind.Key;

		public PropertyPathPartKind Kind => m_Kind;

		public string Name
		{
			get
			{
				CheckKind(PropertyPathPartKind.Name);
				return m_Name;
			}
		}

		public int Index
		{
			get
			{
				CheckKind(PropertyPathPartKind.Index);
				return m_Index;
			}
		}

		public object Key
		{
			get
			{
				CheckKind(PropertyPathPartKind.Key);
				return m_Key;
			}
		}

		public PropertyPathPart(string name)
		{
			m_Kind = PropertyPathPartKind.Name;
			m_Name = name;
			m_Index = -1;
			m_Key = null;
		}

		public PropertyPathPart(int index)
		{
			m_Kind = PropertyPathPartKind.Index;
			m_Name = string.Empty;
			m_Index = index;
			m_Key = null;
		}

		public PropertyPathPart(object key)
		{
			m_Kind = PropertyPathPartKind.Key;
			m_Name = string.Empty;
			m_Index = -1;
			m_Key = key;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckKind(PropertyPathPartKind type)
		{
			if (type != Kind)
			{
				throw new InvalidOperationException();
			}
		}

		public override string ToString()
		{
			PropertyPathPartKind kind = Kind;
			if (1 == 0)
			{
			}
			string result = kind switch
			{
				PropertyPathPartKind.Name => m_Name, 
				PropertyPathPartKind.Index => "[" + m_Index + "]", 
				PropertyPathPartKind.Key => "[\"" + m_Key?.ToString() + "\"]", 
				_ => throw new ArgumentOutOfRangeException(), 
			};
			if (1 == 0)
			{
			}
			return result;
		}

		public bool Equals(PropertyPathPart other)
		{
			return m_Kind == other.m_Kind && m_Name == other.m_Name && m_Index == other.m_Index && object.Equals(m_Key, other.m_Key);
		}

		public override bool Equals(object obj)
		{
			return obj is PropertyPathPart other && Equals(other);
		}

		public override int GetHashCode()
		{
			int kind = (int)m_Kind;
			PropertyPathPartKind kind2 = m_Kind;
			if (1 == 0)
			{
			}
			int result = kind2 switch
			{
				PropertyPathPartKind.Name => (kind * 397) ^ ((m_Name != null) ? m_Name.GetHashCode() : 0), 
				PropertyPathPartKind.Index => (kind * 397) ^ m_Index, 
				PropertyPathPartKind.Key => (kind * 397) ^ ((m_Key != null) ? m_Key.GetHashCode() : 0), 
				_ => throw new ArgumentOutOfRangeException(), 
			};
			if (1 == 0)
			{
			}
			return result;
		}
	}
	public readonly struct PropertyPath : IEquatable<PropertyPath>
	{
		internal const int k_InlineCount = 4;

		private readonly PropertyPathPart m_Part0;

		private readonly PropertyPathPart m_Part1;

		private readonly PropertyPathPart m_Part2;

		private readonly PropertyPathPart m_Part3;

		private readonly int m_InlinePartsCount;

		private readonly PropertyPathPart[] m_AdditionalParts;

		public int Length { get; }

		public bool IsEmpty => Length == 0;

		public PropertyPathPart this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					if (Length < 1)
					{
						throw new IndexOutOfRangeException();
					}
					return m_Part0;
				case 1:
					if (Length < 2)
					{
						throw new IndexOutOfRangeException();
					}
					return m_Part1;
				case 2:
					if (Length < 3)
					{
						throw new IndexOutOfRangeException();
					}
					return m_Part2;
				case 3:
					if (Length < 4)
					{
						throw new IndexOutOfRangeException();
					}
					return m_Part3;
				default:
					return m_AdditionalParts[index - 4];
				}
			}
		}

		public PropertyPath(string path)
		{
			PropertyPath propertyPath = ConstructFromPath(path);
			m_Part0 = propertyPath.m_Part0;
			m_Part1 = propertyPath.m_Part1;
			m_Part2 = propertyPath.m_Part2;
			m_Part3 = propertyPath.m_Part3;
			m_AdditionalParts = propertyPath.m_AdditionalParts;
			m_InlinePartsCount = propertyPath.m_InlinePartsCount;
			int inlinePartsCount = m_InlinePartsCount;
			PropertyPathPart[] additionalParts = m_AdditionalParts;
			Length = inlinePartsCount + ((additionalParts != null) ? additionalParts.Length : 0);
		}

		private PropertyPath(in PropertyPathPart part)
		{
			m_Part0 = part;
			m_Part1 = default(PropertyPathPart);
			m_Part2 = default(PropertyPathPart);
			m_Part3 = default(PropertyPathPart);
			m_AdditionalParts = null;
			m_InlinePartsCount = 1;
			Length = 1;
		}

		private PropertyPath(in PropertyPathPart part0, in PropertyPathPart part1)
		{
			m_Part0 = part0;
			m_Part1 = part1;
			m_Part2 = default(PropertyPathPart);
			m_Part3 = default(PropertyPathPart);
			m_AdditionalParts = null;
			m_InlinePartsCount = 2;
			Length = 2;
		}

		private PropertyPath(in PropertyPathPart part0, in PropertyPathPart part1, in PropertyPathPart part2)
		{
			m_Part0 = part0;
			m_Part1 = part1;
			m_Part2 = part2;
			m_Part3 = default(PropertyPathPart);
			m_AdditionalParts = null;
			m_InlinePartsCount = 3;
			Length = 3;
		}

		private PropertyPath(in PropertyPathPart part0, in PropertyPathPart part1, in PropertyPathPart part2, in PropertyPathPart part3)
		{
			m_Part0 = part0;
			m_Part1 = part1;
			m_Part2 = part2;
			m_Part3 = part3;
			m_AdditionalParts = null;
			m_InlinePartsCount = 4;
			Length = 4;
		}

		internal PropertyPath(List<PropertyPathPart> parts)
		{
			m_Part0 = default(PropertyPathPart);
			m_Part1 = default(PropertyPathPart);
			m_Part2 = default(PropertyPathPart);
			m_Part3 = default(PropertyPathPart);
			m_InlinePartsCount = 0;
			m_AdditionalParts = ((parts.Count > 4) ? new PropertyPathPart[parts.Count - 4] : null);
			for (int i = 0; i < parts.Count; i++)
			{
				switch (i)
				{
				case 0:
					m_Part0 = parts[i];
					m_InlinePartsCount++;
					break;
				case 1:
					m_Part1 = parts[i];
					m_InlinePartsCount++;
					break;
				case 2:
					m_Part2 = parts[i];
					m_InlinePartsCount++;
					break;
				case 3:
					m_Part3 = parts[i];
					m_InlinePartsCount++;
					break;
				default:
					m_AdditionalParts[i - 4] = parts[i];
					break;
				}
			}
			Length = parts.Count;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath FromPart(in PropertyPathPart part)
		{
			return new PropertyPath(in part);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath FromName(string name)
		{
			return new PropertyPath(new PropertyPathPart(name));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath FromIndex(int index)
		{
			return new PropertyPath(new PropertyPathPart(index));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath FromKey(object key)
		{
			return new PropertyPath(new PropertyPathPart(key));
		}

		public static PropertyPath Combine(in PropertyPath path, in PropertyPath pathToAppend)
		{
			if (path.IsEmpty)
			{
				return pathToAppend;
			}
			if (pathToAppend.IsEmpty)
			{
				return path;
			}
			int length = path.Length;
			int length2 = pathToAppend.Length;
			int num = length + length2;
			if (num <= 4)
			{
				int index = 0;
				PropertyPathPart part = path[0];
				PropertyPathPart part2 = ((length > 1) ? path[1] : pathToAppend[index++]);
				PropertyPathPart part3 = ((num <= 2) ? default(PropertyPathPart) : ((length > 2) ? path[2] : pathToAppend[index++]));
				PropertyPathPart part4 = ((num <= 3) ? default(PropertyPathPart) : ((length > 3) ? path[3] : pathToAppend[index]));
				switch (num)
				{
				case 2:
					return new PropertyPath(in part, in part2);
				case 3:
					return new PropertyPath(in part, in part2, in part3);
				case 4:
					return new PropertyPath(in part, in part2, in part3, in part4);
				}
			}
			List<PropertyPathPart> list = CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Get();
			try
			{
				GetParts(in path, list);
				GetParts(in pathToAppend, list);
				return new PropertyPath(list);
			}
			finally
			{
				CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Release(list);
			}
		}

		public static PropertyPath Combine(in PropertyPath path, string pathToAppend)
		{
			if (string.IsNullOrEmpty(pathToAppend))
			{
				return path;
			}
			return Combine(in path, new PropertyPath(pathToAppend));
		}

		public static PropertyPath AppendPart(in PropertyPath path, in PropertyPathPart part)
		{
			if (path.IsEmpty)
			{
				return new PropertyPath(in part);
			}
			switch (path.Length)
			{
			case 1:
				return new PropertyPath(path[0], in part);
			case 2:
				return new PropertyPath(path[0], path[1], in part);
			case 3:
				return new PropertyPath(path[0], path[1], path[2], in part);
			default:
			{
				List<PropertyPathPart> list = CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Get();
				try
				{
					GetParts(in path, list);
					list.Add(part);
					return new PropertyPath(list);
				}
				finally
				{
					CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Release(list);
				}
			}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath AppendName(in PropertyPath path, string name)
		{
			return AppendPart(in path, new PropertyPathPart(name));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath AppendIndex(in PropertyPath path, int index)
		{
			return AppendPart(in path, new PropertyPathPart(index));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath AppendKey(in PropertyPath path, object key)
		{
			return AppendPart(in path, new PropertyPathPart(key));
		}

		public static PropertyPath AppendProperty(in PropertyPath path, IProperty property)
		{
			if (1 == 0)
			{
			}
			PropertyPath result = ((property is IListElementProperty listElementProperty) ? AppendPart(in path, new PropertyPathPart(listElementProperty.Index)) : ((property is ISetElementProperty setElementProperty) ? AppendPart(in path, new PropertyPathPart(setElementProperty.ObjectKey)) : ((!(property is IDictionaryElementProperty dictionaryElementProperty)) ? AppendPart(in path, new PropertyPathPart(property.Name)) : AppendPart(in path, new PropertyPathPart(dictionaryElementProperty.ObjectKey)))));
			if (1 == 0)
			{
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath Pop(in PropertyPath path)
		{
			return SubPath(in path, 0, path.Length - 1);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath SubPath(in PropertyPath path, int startIndex)
		{
			return SubPath(in path, startIndex, path.Length - startIndex);
		}

		public static PropertyPath SubPath(in PropertyPath path, int startIndex, int length)
		{
			int length2 = path.Length;
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (startIndex > length2)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (startIndex > length2 - length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (length == 0)
			{
				return default(PropertyPath);
			}
			if (startIndex == 0 && length == length2)
			{
				return path;
			}
			switch (length)
			{
			case 1:
				return new PropertyPath(path[startIndex]);
			case 2:
				return new PropertyPath(path[startIndex], path[startIndex + 1]);
			case 3:
				return new PropertyPath(path[startIndex], path[startIndex + 1], path[startIndex + 2]);
			case 4:
				return new PropertyPath(path[startIndex], path[startIndex + 1], path[startIndex + 2], path[startIndex + 3]);
			default:
			{
				List<PropertyPathPart> list = CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Get();
				try
				{
					for (int i = startIndex; i < startIndex + length; i++)
					{
						list.Add(path[i]);
					}
					return new PropertyPath(list);
				}
				finally
				{
					CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Release(list);
				}
			}
			}
		}

		public override string ToString()
		{
			if (Length == 0)
			{
				return string.Empty;
			}
			if (Length == 1 && m_Part0.IsName)
			{
				return m_Part0.Name;
			}
			StringBuilder stringBuilder = new StringBuilder(32);
			if (Length > 0)
			{
				AppendToBuilder(in m_Part0, stringBuilder);
			}
			if (Length > 1)
			{
				AppendToBuilder(in m_Part1, stringBuilder);
			}
			if (Length > 2)
			{
				AppendToBuilder(in m_Part2, stringBuilder);
			}
			if (Length > 3)
			{
				AppendToBuilder(in m_Part3, stringBuilder);
			}
			if (Length > 4)
			{
				PropertyPathPart[] additionalParts = m_AdditionalParts;
				for (int i = 0; i < additionalParts.Length; i++)
				{
					PropertyPathPart part = additionalParts[i];
					AppendToBuilder(in part, stringBuilder);
				}
			}
			return stringBuilder.ToString();
		}

		private static void AppendToBuilder(in PropertyPathPart part, StringBuilder builder)
		{
			switch (part.Kind)
			{
			case PropertyPathPartKind.Name:
				if (builder.Length > 0)
				{
					builder.Append('.');
				}
				builder.Append(part.ToString());
				break;
			case PropertyPathPartKind.Index:
			case PropertyPathPartKind.Key:
				builder.Append(part.ToString());
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		private static void GetParts(in PropertyPath path, List<PropertyPathPart> parts)
		{
			int length = path.Length;
			for (int i = 0; i < length; i++)
			{
				parts.Add(path[i]);
			}
		}

		private static PropertyPath ConstructFromPath(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return default(PropertyPath);
			}
			int index = 0;
			int length = path.Length;
			int state = 0;
			List<PropertyPathPart> list = CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Get();
			try
			{
				list.Clear();
				while (index < length)
				{
					switch (state)
					{
					case 0:
						TrimStart();
						if (index == length)
						{
							break;
						}
						if (path[index] == '.')
						{
							throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", path[index], index));
						}
						if (path[index] == '[')
						{
							state = 2;
							break;
						}
						if (path[index] == '"')
						{
							throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", path[index], index));
						}
						state = 1;
						break;
					case 1:
					{
						int num3 = index;
						while (index < length && path[index] != '.' && path[index] != '[')
						{
							int num = index + 1;
							index = num;
						}
						if (num3 == index)
						{
							throw new ArgumentException("Invalid PropertyPath: Name is empty.");
						}
						if (index == length)
						{
							list.Add(new PropertyPathPart(path.Substring(num3)));
							state = 0;
						}
						else
						{
							list.Add(new PropertyPathPart(path.Substring(num3, index - num3)));
							ReadNext();
						}
						break;
					}
					case 2:
						if (path[index] != '[')
						{
							throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", path[index], index));
						}
						if (index + 1 < length && path[index + 1] == '"')
						{
							state = 4;
						}
						else
						{
							state = 3;
						}
						break;
					case 3:
					{
						if (path[index] != '[')
						{
							throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", path[index], index));
						}
						int num = index + 1;
						index = num;
						int num4 = index;
						while (index < length)
						{
							char c2 = path[index];
							if (c2 == ']')
							{
								break;
							}
							num = index + 1;
							index = num;
						}
						if (path[index] != ']')
						{
							throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", path[index], index));
						}
						string s = path.Substring(num4, index - num4);
						if (!int.TryParse(s, out var result))
						{
							throw new ArgumentException("Indices in PropertyPath must be a numeric value.");
						}
						if (result < 0)
						{
							throw new ArgumentException("Invalid PropertyPath: Negative indices are not supported.");
						}
						list.Add(new PropertyPathPart(result));
						num = index + 1;
						index = num;
						if (index != length)
						{
							ReadNext();
						}
						break;
					}
					case 4:
					{
						if (path[index] != '[')
						{
							throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", path[index], index));
						}
						int num = index + 1;
						index = num;
						if (path[index] != '"')
						{
							throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", path[index], index));
						}
						num = index + 1;
						index = num;
						int num2 = index;
						while (index < length)
						{
							char c = path[index];
							if (c == '"')
							{
								break;
							}
							num = index + 1;
							index = num;
						}
						if (path[index] != '"')
						{
							throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", path[index], index));
						}
						if (index + 1 < length && path[index + 1] == ']')
						{
							string key = path.Substring(num2, index - num2);
							list.Add(new PropertyPathPart((object)key));
							index += 2;
							ReadNext();
							break;
						}
						throw new ArgumentException("Invalid PropertyPath: No matching end quote for key.");
					}
					}
				}
				return new PropertyPath(list);
			}
			finally
			{
				CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Release(list);
			}
			void ReadNext()
			{
				if (index == length)
				{
					state = 0;
				}
				else
				{
					switch (path[index])
					{
					case '.':
					{
						int num5 = index + 1;
						index = num5;
						state = 0;
						break;
					}
					case '[':
						state = 2;
						break;
					default:
						throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", path[index], index));
					}
				}
			}
			void TrimStart()
			{
				while (index < length && path[index] == ' ')
				{
					int num5 = index + 1;
					index = num5;
				}
			}
		}

		public bool Equals(PropertyPath other)
		{
			if (Length != other.Length)
			{
				return false;
			}
			for (int i = 0; i < Length; i++)
			{
				if (!this[i].Equals(other[i]))
				{
					return false;
				}
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			if (obj is PropertyPath other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			int num = 19;
			int length = Length;
			if (length == 0)
			{
				return num;
			}
			if (length > 0)
			{
				num = num * 31 + m_Part0.GetHashCode();
			}
			if (length > 1)
			{
				num = num * 31 + m_Part1.GetHashCode();
			}
			if (length > 2)
			{
				num = num * 31 + m_Part2.GetHashCode();
			}
			if (length > 3)
			{
				num = num * 31 + m_Part3.GetHashCode();
			}
			if (length <= 4)
			{
				return num;
			}
			PropertyPathPart[] additionalParts = m_AdditionalParts;
			for (int i = 0; i < additionalParts.Length; i++)
			{
				PropertyPathPart propertyPathPart = additionalParts[i];
				num = num * 31 + propertyPathPart.GetHashCode();
			}
			return num;
		}
	}
	internal interface IMemberInfo
	{
		string Name { get; }

		bool IsReadOnly { get; }

		Type ValueType { get; }

		object GetValue(object obj);

		void SetValue(object obj, object value);

		IEnumerable<Attribute> GetCustomAttributes();
	}
	internal readonly struct FieldMember : IMemberInfo
	{
		internal readonly FieldInfo m_FieldInfo;

		public string Name { get; }

		public bool IsReadOnly => m_FieldInfo.IsInitOnly;

		public Type ValueType => m_FieldInfo.FieldType;

		public FieldMember(FieldInfo fieldInfo)
		{
			m_FieldInfo = fieldInfo;
			Name = ReflectionUtilities.SanitizeMemberName(m_FieldInfo);
		}

		public object GetValue(object obj)
		{
			return m_FieldInfo.GetValue(obj);
		}

		public void SetValue(object obj, object value)
		{
			m_FieldInfo.SetValue(obj, value);
		}

		public IEnumerable<Attribute> GetCustomAttributes()
		{
			return m_FieldInfo.GetCustomAttributes();
		}
	}
	internal readonly struct PropertyMember : IMemberInfo
	{
		internal readonly PropertyInfo m_PropertyInfo;

		public string Name { get; }

		public bool IsReadOnly => !m_PropertyInfo.CanWrite;

		public Type ValueType => m_PropertyInfo.PropertyType;

		public PropertyMember(PropertyInfo propertyInfo)
		{
			m_PropertyInfo = propertyInfo;
			Name = ReflectionUtilities.SanitizeMemberName(m_PropertyInfo);
		}

		public object GetValue(object obj)
		{
			return m_PropertyInfo.GetValue(obj);
		}

		public void SetValue(object obj, object value)
		{
			m_PropertyInfo.SetValue(obj, value);
		}

		public IEnumerable<Attribute> GetCustomAttributes()
		{
			return m_PropertyInfo.GetCustomAttributes();
		}
	}
	public class ReflectedMemberProperty<TContainer, TValue> : Property<TContainer, TValue>
	{
		private delegate TValue GetStructValueAction(ref TContainer container);

		private delegate void SetStructValueAction(ref TContainer container, TValue value);

		private delegate TValue GetClassValueAction(TContainer container);

		private delegate void SetClassValueAction(TContainer container, TValue value);

		private readonly IMemberInfo m_Info;

		private readonly bool m_IsStructContainerType;

		private GetStructValueAction m_GetStructValueAction;

		private SetStructValueAction m_SetStructValueAction;

		private GetClassValueAction m_GetClassValueAction;

		private SetClassValueAction m_SetClassValueAction;

		public override string Name { get; }

		public override bool IsReadOnly { get; }

		public ReflectedMemberProperty(FieldInfo info, string name)
			: this((IMemberInfo)new FieldMember(info), name)
		{
		}

		public ReflectedMemberProperty(PropertyInfo info, string name)
			: this((IMemberInfo)new PropertyMember(info), name)
		{
		}

		internal ReflectedMemberProperty(IMemberInfo info, string name)
		{
			Name = name;
			m_Info = info;
			m_IsStructContainerType = TypeTraits<TContainer>.IsValueType;
			AddAttributes(info.GetCustomAttributes());
			bool flag = (IsReadOnly = m_Info.IsReadOnly || HasAttribute<ReadOnlyAttribute>());
			if (m_Info is FieldMember fieldMember)
			{
				FieldInfo fieldInfo = fieldMember.m_FieldInfo;
				DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, fieldInfo.FieldType, new Type[1] { m_IsStructContainerType ? fieldInfo.ReflectedType.MakeByRefType() : fieldInfo.ReflectedType }, restrictedSkipVisibility: true);
				ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
				iLGenerator.Emit(OpCodes.Ldarg_0);
				iLGenerator.Emit(OpCodes.Ldfld, fieldInfo);
				iLGenerator.Emit(OpCodes.Ret);
				if (m_IsStructContainerType)
				{
					m_GetStructValueAction = (GetStructValueAction)dynamicMethod.CreateDelegate(typeof(GetStructValueAction));
				}
				else
				{
					m_GetClassValueAction = (GetClassValueAction)dynamicMethod.CreateDelegate(typeof(GetClassValueAction));
				}
				if (!flag)
				{
					dynamicMethod = new DynamicMethod(string.Empty, typeof(void), new Type[2]
					{
						m_IsStructContainerType ? fieldInfo.ReflectedType.MakeByRefType() : fieldInfo.ReflectedType,
						fieldInfo.FieldType
					}, restrictedSkipVisibility: true);
					iLGenerator = dynamicMethod.GetILGenerator();
					iLGenerator.Emit(OpCodes.Ldarg_0);
					iLGenerator.Emit(OpCodes.Ldarg_1);
					iLGenerator.Emit(OpCodes.Stfld, fieldInfo);
					iLGenerator.Emit(OpCodes.Ret);
					if (m_IsStructContainerType)
					{
						m_SetStructValueAction = (SetStructValueAction)dynamicMethod.CreateDelegate(typeof(SetStructValueAction));
					}
					else
					{
						m_SetClassValueAction = (SetClassValueAction)dynamicMethod.CreateDelegate(typeof(SetClassValueAction));
					}
				}
			}
			else
			{
				if (!(m_Info is PropertyMember propertyMember))
				{
					return;
				}
				if (m_IsStructContainerType)
				{
					MethodInfo getMethod = propertyMember.m_PropertyInfo.GetGetMethod(nonPublic: true);
					m_GetStructValueAction = (GetStructValueAction)Delegate.CreateDelegate(typeof(GetStructValueAction), getMethod);
					if (!flag)
					{
						MethodInfo setMethod = propertyMember.m_PropertyInfo.GetSetMethod(nonPublic: true);
						m_SetStructValueAction = (SetStructValueAction)Delegate.CreateDelegate(typeof(SetStructValueAction), setMethod);
					}
				}
				else
				{
					MethodInfo getMethod2 = propertyMember.m_PropertyInfo.GetGetMethod(nonPublic: true);
					m_GetClassValueAction = (GetClassValueAction)Delegate.CreateDelegate(typeof(GetClassValueAction), getMethod2);
					if (!flag)
					{
						MethodInfo setMethod2 = propertyMember.m_PropertyInfo.GetSetMethod(nonPublic: true);
						m_SetClassValueAction = (SetClassValueAction)Delegate.CreateDelegate(typeof(SetClassValueAction), setMethod2);
					}
				}
			}
		}

		public override TValue GetValue(ref TContainer container)
		{
			if (m_IsStructContainerType)
			{
				return (m_GetStructValueAction == null) ? ((TValue)m_Info.GetValue(container)) : m_GetStructValueAction(ref container);
			}
			return (m_GetClassValueAction == null) ? ((TValue)m_Info.GetValue(container)) : m_GetClassValueAction(container);
		}

		public override void SetValue(ref TContainer container, TValue value)
		{
			if (IsReadOnly)
			{
				throw new InvalidOperationException("Property is ReadOnly.");
			}
			if (m_IsStructContainerType)
			{
				if (m_SetStructValueAction == null)
				{
					object obj = container;
					m_Info.SetValue(obj, value);
					container = (TContainer)obj;
				}
				else
				{
					m_SetStructValueAction(ref container, value);
				}
			}
			else if (m_SetClassValueAction == null)
			{
				m_Info.SetValue(container, value);
			}
			else
			{
				m_SetClassValueAction(container, value);
			}
		}
	}
	public sealed class ArrayPropertyBag<TElement> : IndexedCollectionPropertyBag<TElement[], TElement>
	{
		protected override InstantiationKind InstantiationKind => InstantiationKind.PropertyBagOverride;

		protected override TElement[] InstantiateWithCount(int count)
		{
			return new TElement[count];
		}

		protected override TElement[] Instantiate()
		{
			return Array.Empty<TElement>();
		}
	}
	public abstract class ContainerPropertyBag<TContainer> : PropertyBag<TContainer>, INamedProperties<TContainer>
	{
		private readonly List<IProperty<TContainer>> m_PropertiesList = new List<IProperty<TContainer>>();

		private readonly Dictionary<string, IProperty<TContainer>> m_PropertiesHash = new Dictionary<string, IProperty<TContainer>>();

		static ContainerPropertyBag()
		{
			if (!TypeTraits.IsContainer(typeof(TContainer)))
			{
				throw new InvalidOperationException($"Failed to create a property bag for Type=[{typeof(TContainer)}]. The type is not a valid container type.");
			}
		}

		protected void AddProperty<TValue>(Property<TContainer, TValue> property)
		{
			m_PropertiesList.Add(property);
			m_PropertiesHash.Add(property.Name, property);
		}

		public override PropertyCollection<TContainer> GetProperties()
		{
			return new PropertyCollection<TContainer>(m_PropertiesList);
		}

		public override PropertyCollection<TContainer> GetProperties(ref TContainer container)
		{
			return new PropertyCollection<TContainer>(m_PropertiesList);
		}

		public bool TryGetProperty(ref TContainer container, string name, out IProperty<TContainer> property)
		{
			return m_PropertiesHash.TryGetValue(name, out property);
		}
	}
	public class DictionaryPropertyBag<TKey, TValue> : KeyValueCollectionPropertyBag<Dictionary<TKey, TValue>, TKey, TValue>
	{
		protected override InstantiationKind InstantiationKind => InstantiationKind.PropertyBagOverride;

		protected override Dictionary<TKey, TValue> Instantiate()
		{
			return new Dictionary<TKey, TValue>();
		}
	}
	public class HashSetPropertyBag<TElement> : SetPropertyBagBase<HashSet<TElement>, TElement>
	{
		protected override InstantiationKind InstantiationKind => InstantiationKind.PropertyBagOverride;

		protected override HashSet<TElement> Instantiate()
		{
			return new HashSet<TElement>();
		}
	}
	internal readonly struct IndexedCollectionPropertyBagEnumerable<TContainer>
	{
		private readonly IIndexedCollectionPropertyBagEnumerator<TContainer> m_Impl;

		private readonly TContainer m_Container;

		public IndexedCollectionPropertyBagEnumerable(IIndexedCollectionPropertyBagEnumerator<TContainer> impl, TContainer container)
		{
			m_Impl = impl;
			m_Container = container;
		}

		public IndexedCollectionPropertyBagEnumerator<TContainer> GetEnumerator()
		{
			return new IndexedCollectionPropertyBagEnumerator<TContainer>(m_Impl, m_Container);
		}
	}
	internal struct IndexedCollectionPropertyBagEnumerator<TContainer> : IEnumerator<IProperty<TContainer>>, IEnumerator, IDisposable
	{
		private readonly IIndexedCollectionPropertyBagEnumerator<TContainer> m_Impl;

		private readonly IndexedCollectionSharedPropertyState m_Previous;

		private TContainer m_Container;

		private int m_Position;

		public IProperty<TContainer> Current => m_Impl.GetSharedProperty();

		object IEnumerator.Current => Current;

		internal IndexedCollectionPropertyBagEnumerator(IIndexedCollectionPropertyBagEnumerator<TContainer> impl, TContainer container)
		{
			m_Impl = impl;
			m_Container = container;
			m_Previous = impl.GetSharedPropertyState();
			m_Position = -1;
		}

		public bool MoveNext()
		{
			m_Position++;
			if (m_Position < m_Impl.GetCount(ref m_Container))
			{
				m_Impl.SetSharedPropertyState(new IndexedCollectionSharedPropertyState
				{
					Index = m_Position,
					IsReadOnly = false
				});
				return true;
			}
			m_Impl.SetSharedPropertyState(m_Previous);
			return false;
		}

		public void Reset()
		{
			m_Position = -1;
			m_Impl.SetSharedPropertyState(m_Previous);
		}

		public void Dispose()
		{
		}
	}
	internal interface IIndexedCollectionPropertyBagEnumerator<TContainer>
	{
		int GetCount(ref TContainer container);

		IProperty<TContainer> GetSharedProperty();

		IndexedCollectionSharedPropertyState GetSharedPropertyState();

		void SetSharedPropertyState(IndexedCollectionSharedPropertyState state);
	}
	internal struct IndexedCollectionSharedPropertyState
	{
		public int Index;

		public bool IsReadOnly;
	}
	public class IndexedCollectionPropertyBag<TList, TElement> : PropertyBag<TList>, IListPropertyBag<TList, TElement>, ICollectionPropertyBag<TList, TElement>, IPropertyBag<TList>, IPropertyBag, ICollectionPropertyBagAccept<TList>, IListPropertyBagAccept<TList>, IListPropertyAccept<TList>, IIndexedProperties<TList>, IConstructorWithCount<TList>, IConstructor, IIndexedCollectionPropertyBagEnumerator<TList> where TList : IList<TElement>
	{
		private class ListElementProperty : Property<TList, TElement>, IListElementProperty, ICollectionElementProperty
		{
			internal int m_Index;

			internal bool m_IsReadOnly;

			public int Index => m_Index;

			public override string Name => Index.ToString();

			public override bool IsReadOnly => m_IsReadOnly;

			public override TElement GetValue(ref TList container)
			{
				int index = m_Index;
				return container[index];
			}

			public override void SetValue(ref TList container, TElement value)
			{
				int index = m_Index;
				container[index] = value;
			}
		}

		private readonly ListElementProperty m_Property = new ListElementProperty();

		public override PropertyCollection<TList> GetProperties()
		{
			return PropertyCollection<TList>.Empty;
		}

		public override PropertyCollection<TList> GetProperties(ref TList container)
		{
			return new PropertyCollection<TList>(new IndexedCollectionPropertyBagEnumerable<TList>(this, container));
		}

		public bool TryGetProperty(ref TList container, int index, out IProperty<TList> property)
		{
			if ((uint)index >= (uint)container.Count)
			{
				property = null;
				return false;
			}
			property = new ListElementProperty
			{
				m_Index = index,
				m_IsReadOnly = false
			};
			return true;
		}

		void ICollectionPropertyBagAccept<TList>.Accept(ICollectionPropertyBagVisitor visitor, ref TList container)
		{
			visitor.Visit(this, ref container);
		}

		void IListPropertyBagAccept<TList>.Accept(IListPropertyBagVisitor visitor, ref TList list)
		{
			visitor.Visit(this, ref list);
		}

		void IListPropertyAccept<TList>.Accept<TContainer>(IListPropertyVisitor visitor, Property<TContainer, TList> property, ref TContainer container, ref TList list)
		{
			using (new AttributesScope(m_Property, property))
			{
				visitor.Visit<TContainer, TList, TElement>(property, ref container, ref list);
			}
		}

		TList IConstructorWithCount<TList>.InstantiateWithCount(int count)
		{
			return InstantiateWithCount(count);
		}

		protected virtual TList InstantiateWithCount(int count)
		{
			return default(TList);
		}

		int IIndexedCollectionPropertyBagEnumerator<TList>.GetCount(ref TList container)
		{
			return container.Count;
		}

		IProperty<TList> IIndexedCollectionPropertyBagEnumerator<TList>.GetSharedProperty()
		{
			return m_Property;
		}

		IndexedCollectionSharedPropertyState IIndexedCollectionPropertyBagEnumerator<TList>.GetSharedPropertyState()
		{
			return new IndexedCollectionSharedPropertyState
			{
				Index = m_Property.m_Index,
				IsReadOnly = m_Property.IsReadOnly
			};
		}

		void IIndexedCollectionPropertyBagEnumerator<TList>.SetSharedPropertyState(IndexedCollectionSharedPropertyState state)
		{
			m_Property.m_Index = state.Index;
			m_Property.m_IsReadOnly = state.IsReadOnly;
		}
	}
	public interface IIndexedProperties<TContainer>
	{
		bool TryGetProperty(ref TContainer container, int index, out IProperty<TContainer> property);
	}
	public interface INamedProperties<TContainer>
	{
		bool TryGetProperty(ref TContainer container, string name, out IProperty<TContainer> property);
	}
	public interface IKeyedProperties<TContainer, TKey>
	{
		bool TryGetProperty(ref TContainer container, TKey key, out IProperty<TContainer> property);
	}
	public interface IPropertyBag
	{
		void Accept(ITypeVisitor visitor);

		void Accept(IPropertyBagVisitor visitor, ref object container);
	}
	public interface IPropertyBag<TContainer> : IPropertyBag
	{
		PropertyCollection<TContainer> GetProperties();

		PropertyCollection<TContainer> GetProperties(ref TContainer container);

		TContainer CreateInstance();

		bool TryCreateInstance(out TContainer instance);

		void Accept(IPropertyBagVisitor visitor, ref TContainer container);
	}
	public interface ICollectionPropertyBag<TCollection, TElement> : IPropertyBag<TCollection>, IPropertyBag, ICollectionPropertyBagAccept<TCollection> where TCollection : ICollection<TElement>
	{
	}
	public interface IListPropertyBag<TList, TElement> : ICollectionPropertyBag<TList, TElement>, IPropertyBag<TList>, IPropertyBag, ICollectionPropertyBagAccept<TList>, IListPropertyBagAccept<TList>, IListPropertyAccept<TList>, IIndexedProperties<TList> where TList : IList<TElement>
	{
	}
	public interface ISetPropertyBag<TSet, TElement> : ICollectionPropertyBag<TSet, TElement>, IPropertyBag<TSet>, IPropertyBag, ICollectionPropertyBagAccept<TSet>, ISetPropertyBagAccept<TSet>, ISetPropertyAccept<TSet>, IKeyedProperties<TSet, object> where TSet : ISet<TElement>
	{
	}
	public interface IDictionaryPropertyBag<TDictionary, TKey, TValue> : ICollectionPropertyBag<TDictionary, KeyValuePair<TKey, TValue>>, IPropertyBag<TDictionary>, IPropertyBag, ICollectionPropertyBagAccept<TDictionary>, IDictionaryPropertyBagAccept<TDictionary>, IDictionaryPropertyAccept<TDictionary>, IKeyedProperties<TDictionary, object> where TDictionary : IDictionary<TKey, TValue>
	{
	}
	public class KeyValueCollectionPropertyBag<TDictionary, TKey, TValue> : PropertyBag<TDictionary>, IDictionaryPropertyBag<TDictionary, TKey, TValue>, ICollectionPropertyBag<TDictionary, KeyValuePair<TKey, TValue>>, IPropertyBag<TDictionary>, IPropertyBag, ICollectionPropertyBagAccept<TDictionary>, IDictionaryPropertyBagAccept<TDictionary>, IDictionaryPropertyAccept<TDictionary>, IKeyedProperties<TDictionary, object> where TDictionary : IDictionary<TKey, TValue>
	{
		private class KeyValuePairProperty : Property<TDictionary, KeyValuePair<TKey, TValue>>, IDictionaryElementProperty<TKey>, IDictionaryElementProperty, ICollectionElementProperty
		{
			public override string Name => Key.ToString();

			public override bool IsReadOnly => false;

			public TKey Key { get; internal set; }

			public object ObjectKey => Key;

			public override KeyValuePair<TKey, TValue> GetValue(ref TDictionary container)
			{
				TKey key = Key;
				TKey key2 = Key;
				return new KeyValuePair<TKey, TValue>(key, container[key2]);
			}

			public override void SetValue(ref TDictionary container, KeyValuePair<TKey, TValue> value)
			{
				TKey key = value.Key;
				TValue value2 = value.Value;
				container[key] = value2;
			}
		}

		private readonly struct Enumerable : IEnumerable<IProperty<TDictionary>>, IEnumerable
		{
			private class Enumerator : IEnumerator<IProperty<TDictionary>>, IEnumerator, IDisposable
			{
				private readonly TDictionary m_Dictionary;

				private readonly KeyValuePairProperty m_Property;

				private readonly TKey m_Previous;

				private readonly List<TKey> m_Keys;

				private int m_Position;

				public IProperty<TDictionary> Current => m_Property;

				object IEnumerator.Current => Current;

				public Enumerator(TDictionary dictionary, KeyValuePairProperty property)
				{
					m_Dictionary = dictionary;
					m_Property = property;
					m_Previous = property.Key;
					m_Position = -1;
					m_Keys = CollectionPool<List<TKey>, TKey>.Get();
					m_Keys.AddRange(m_Dictionary.Keys);
				}

				public bool MoveNext()
				{
					m_Position++;
					if (m_Position < m_Dictionary.Count)
					{
						m_Property.Key = m_Keys[m_Position];
						return true;
					}
					m_Property.Key = m_Previous;
					return false;
				}

				public void Reset()
				{
					m_Position = -1;
					m_Property.Key = m_Previous;
				}

				public void Dispose()
				{
					CollectionPool<List<TKey>, TKey>.Release(m_Keys);
				}
			}

			private readonly TDictionary m_Dictionary;

			private readonly KeyValuePairProperty m_Property;

			public Enumerable(TDictionary dictionary, KeyValuePairProperty property)
			{
				m_Dictionary = dictionary;
				m_Property = property;
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Enumerator(m_Dictionary, m_Property);
			}

			IEnumerator<IProperty<TDictionary>> IEnumerable<IProperty<TDictionary>>.GetEnumerator()
			{
				return new Enumerator(m_Dictionary, m_Property);
			}
		}

		private readonly KeyValuePairProperty m_KeyValuePairProperty = new KeyValuePairProperty();

		public override PropertyCollection<TDictionary> GetProperties()
		{
			return PropertyCollection<TDictionary>.Empty;
		}

		public override PropertyCollection<TDictionary> GetProperties(ref TDictionary container)
		{
			return new PropertyCollection<TDictionary>(new Enumerable(container, m_KeyValuePairProperty));
		}

		void ICollectionPropertyBagAccept<TDictionary>.Accept(ICollectionPropertyBagVisitor visitor, ref TDictionary container)
		{
			visitor.Visit(this, ref container);
		}

		void IDictionaryPropertyBagAccept<TDictionary>.Accept(IDictionaryPropertyBagVisitor visitor, ref TDictionary container)
		{
			visitor.Visit(this, ref container);
		}

		void IDictionaryPropertyAccept<TDictionary>.Accept<TContainer>(IDictionaryPropertyVisitor visitor, Property<TContainer, TDictionary> property, ref TContainer container, ref TDictionary dictionary)
		{
			using (new AttributesScope(m_KeyValuePairProperty, property))
			{
				visitor.Visit<TContainer, TDictionary, TKey, TValue>(property, ref container, ref dictionary);
			}
		}

		bool IKeyedProperties<TDictionary, object>.TryGetProperty(ref TDictionary container, object key, out IProperty<TDictionary> property)
		{
			TKey key2 = (TKey)key;
			if (container.ContainsKey(key2))
			{
				property = new KeyValuePairProperty
				{
					Key = (TKey)key
				};
				return true;
			}
			property = null;
			return false;
		}
	}
	public class KeyValuePairPropertyBag<TKey, TValue> : PropertyBag<KeyValuePair<TKey, TValue>>, INamedProperties<KeyValuePair<TKey, TValue>>
	{
		private static readonly DelegateProperty<KeyValuePair<TKey, TValue>, TKey> s_KeyProperty = new DelegateProperty<KeyValuePair<TKey, TValue>, TKey>("Key", delegate(ref KeyValuePair<TKey, TValue> container)
		{
			return container.Key;
		});

		private static readonly DelegateProperty<KeyValuePair<TKey, TValue>, TValue> s_ValueProperty = new DelegateProperty<KeyValuePair<TKey, TValue>, TValue>("Value", delegate(ref KeyValuePair<TKey, TValue> container)
		{
			return container.Value;
		});

		public override PropertyCollection<KeyValuePair<TKey, TValue>> GetProperties()
		{
			return new PropertyCollection<KeyValuePair<TKey, TValue>>(GetPropertiesEnumerable());
		}

		public override PropertyCollection<KeyValuePair<TKey, TValue>> GetProperties(ref KeyValuePair<TKey, TValue> container)
		{
			return new PropertyCollection<KeyValuePair<TKey, TValue>>(GetPropertiesEnumerable());
		}

		private static IEnumerable<IProperty<KeyValuePair<TKey, TValue>>> GetPropertiesEnumerable()
		{
			yield return s_KeyProperty;
			yield return s_ValueProperty;
		}

		public bool TryGetProperty(ref KeyValuePair<TKey, TValue> container, string name, out IProperty<KeyValuePair<TKey, TValue>> property)
		{
			if (name == "Key")
			{
				property = s_KeyProperty;
				return true;
			}
			if (name == "Value")
			{
				property = s_ValueProperty;
				return true;
			}
			property = null;
			return false;
		}
	}
	public class ListPropertyBag<TElement> : IndexedCollectionPropertyBag<List<TElement>, TElement>
	{
		protected override InstantiationKind InstantiationKind => InstantiationKind.PropertyBagOverride;

		protected override List<TElement> InstantiateWithCount(int count)
		{
			return new List<TElement>(count);
		}

		protected override List<TElement> Instantiate()
		{
			return new List<TElement>();
		}
	}
	public static class PropertyBag
	{
		public static void AcceptWithSpecializedVisitor<TContainer>(IPropertyBag<TContainer> properties, IPropertyBagVisitor visitor, ref TContainer container)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			if (!(properties is IDictionaryPropertyBagAccept<TContainer> dictionaryPropertyBagAccept) || !(visitor is IDictionaryPropertyBagVisitor visitor2))
			{
				if (!(properties is IListPropertyBagAccept<TContainer> listPropertyBagAccept) || !(visitor is IListPropertyBagVisitor visitor3))
				{
					if (!(properties is ISetPropertyBagAccept<TContainer> setPropertyBagAccept) || !(visitor is ISetPropertyBagVisitor visitor4))
					{
						if (properties is ICollectionPropertyBagAccept<TContainer> collectionPropertyBagAccept && visitor is ICollectionPropertyBagVisitor visitor5)
						{
							collectionPropertyBagAccept.Accept(visitor5, ref container);
						}
						else
						{
							properties.Accept(visitor, ref container);
						}
					}
					else
					{
						setPropertyBagAccept.Accept(visitor4, ref container);
					}
				}
				else
				{
					listPropertyBagAccept.Accept(visitor3, ref container);
				}
			}
			else
			{
				dictionaryPropertyBagAccept.Accept(visitor2, ref container);
			}
		}

		public static void Register<TContainer>(PropertyBag<TContainer> propertyBag)
		{
			PropertyBagStore.AddPropertyBag(propertyBag);
		}

		public static void RegisterArray<TElement>()
		{
			if (PropertyBagStore.TypedStore<IPropertyBag<TElement[]>>.PropertyBag == null)
			{
				PropertyBagStore.AddPropertyBag(new ArrayPropertyBag<TElement>());
			}
		}

		public static void RegisterArray<TContainer, TElement>()
		{
			RegisterArray<TElement>();
		}

		public static void RegisterList<TElement>()
		{
			if (PropertyBagStore.TypedStore<IPropertyBag<TElement[]>>.PropertyBag == null)
			{
				PropertyBagStore.AddPropertyBag(new ListPropertyBag<TElement>());
			}
		}

		public static void RegisterList<TContainer, TElement>()
		{
			RegisterList<TElement>();
		}

		public static void RegisterHashSet<TElement>()
		{
			if (PropertyBagStore.TypedStore<IPropertyBag<HashSet<TElement>>>.PropertyBag == null)
			{
				PropertyBagStore.AddPropertyBag(new HashSetPropertyBag<TElement>());
			}
		}

		public static void RegisterHashSet<TContainer, TElement>()
		{
			RegisterHashSet<TElement>();
		}

		public static void RegisterDictionary<TKey, TValue>()
		{
			if (PropertyBagStore.TypedStore<IPropertyBag<Dictionary<TKey, TValue>>>.PropertyBag == null)
			{
				PropertyBagStore.AddPropertyBag(new DictionaryPropertyBag<TKey, TValue>());
			}
		}

		public static void RegisterDictionary<TContainer, TKey, TValue>()
		{
			RegisterDictionary<TKey, TValue>();
		}

		public static void RegisterIList<TList, TElement>() where TList : IList<TElement>
		{
			if (PropertyBagStore.TypedStore<IPropertyBag<TList>>.PropertyBag == null)
			{
				PropertyBagStore.AddPropertyBag(new IndexedCollectionPropertyBag<TList, TElement>());
			}
		}

		public static void RegisterIList<TContainer, TList, TElement>() where TList : IList<TElement>
		{
			RegisterIList<TList, TElement>();
		}

		public static void RegisterISet<TSet, TElement>() where TSet : ISet<TElement>
		{
			if (PropertyBagStore.TypedStore<IPropertyBag<TSet>>.PropertyBag == null)
			{
				PropertyBagStore.AddPropertyBag(new SetPropertyBagBase<TSet, TElement>());
			}
		}

		public static void RegisterISet<TContainer, TSet, TElement>() where TSet : ISet<TElement>
		{
			RegisterISet<TSet, TElement>();
		}

		public static void RegisterIDictionary<TDictionary, TKey, TValue>() where TDictionary : IDictionary<TKey, TValue>
		{
			if (PropertyBagStore.TypedStore<IPropertyBag<TDictionary>>.PropertyBag == null)
			{
				PropertyBagStore.AddPropertyBag(new KeyValueCollectionPropertyBag<TDictionary, TKey, TValue>());
				PropertyBagStore.AddPropertyBag(new KeyValuePairPropertyBag<TKey, TValue>());
			}
		}

		public static void RegisterIDictionary<TContainer, TDictionary, TKey, TValue>() where TDictionary : IDictionary<TKey, TValue>
		{
			RegisterIDictionary<TDictionary, TKey, TValue>();
		}

		public static TContainer CreateInstance<TContainer>()
		{
			IPropertyBag<TContainer> propertyBag = PropertyBagStore.GetPropertyBag<TContainer>();
			if (propertyBag == null)
			{
				throw new MissingPropertyBagException(typeof(TContainer));
			}
			return propertyBag.CreateInstance();
		}

		public static IPropertyBag GetPropertyBag(Type type)
		{
			return PropertyBagStore.GetPropertyBag(type);
		}

		public static IPropertyBag<TContainer> GetPropertyBag<TContainer>()
		{
			return PropertyBagStore.GetPropertyBag<TContainer>();
		}

		public static bool TryGetPropertyBagForValue<TValue>(ref TValue value, out IPropertyBag propertyBag)
		{
			return PropertyBagStore.TryGetPropertyBagForValue(ref value, out propertyBag);
		}

		public static bool Exists<TContainer>()
		{
			return PropertyBagStore.Exists<TContainer>();
		}

		public static bool Exists(Type type)
		{
			return PropertyBagStore.Exists(type);
		}

		public static IEnumerable<Type> GetAllTypesWithAPropertyBag()
		{
			return PropertyBagStore.AllTypes;
		}
	}
	public abstract class PropertyBag<TContainer> : IPropertyBag<TContainer>, IPropertyBag, IPropertyBagRegister, IConstructor<TContainer>, IConstructor
	{
		InstantiationKind IConstructor.InstantiationKind => InstantiationKind;

		protected virtual InstantiationKind InstantiationKind { get; } = InstantiationKind.Activator;

		static PropertyBag()
		{
			if (!TypeTraits.IsContainer(typeof(TContainer)))
			{
				throw new InvalidOperationException($"Failed to create a property bag for Type=[{typeof(TContainer)}]. The type is not a valid container type.");
			}
		}

		void IPropertyBagRegister.Register()
		{
			PropertyBagStore.AddPropertyBag(this);
		}

		public void Accept(ITypeVisitor visitor)
		{
			if (visitor == null)
			{
				throw new ArgumentNullException("visitor");
			}
			visitor.Visit<TContainer>();
		}

		void IPropertyBag.Accept(IPropertyBagVisitor visitor, ref object container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			if (!(container is TContainer container2) || 1 == 0)
			{
				throw new ArgumentException($"The given ContainerType=[{container.GetType()}] does not match the PropertyBagType=[{typeof(TContainer)}]");
			}
			PropertyBag.AcceptWithSpecializedVisitor(this, visitor, ref container2);
			container = container2;
		}

		void IPropertyBag<TContainer>.Accept(IPropertyBagVisitor visitor, ref TContainer container)
		{
			visitor.Visit(this, ref container);
		}

		PropertyCollection<TContainer> IPropertyBag<TContainer>.GetProperties()
		{
			return GetProperties();
		}

		PropertyCollection<TContainer> IPropertyBag<TContainer>.GetProperties(ref TContainer container)
		{
			return GetProperties(ref container);
		}

		TContainer IConstructor<TContainer>.Instantiate()
		{
			return Instantiate();
		}

		public abstract PropertyCollection<TContainer> GetProperties();

		public abstract PropertyCollection<TContainer> GetProperties(ref TContainer container);

		protected virtual TContainer Instantiate()
		{
			return default(TContainer);
		}

		public TContainer CreateInstance()
		{
			return TypeUtility.Instantiate<TContainer>();
		}

		public bool TryCreateInstance(out TContainer instance)
		{
			return TypeUtility.TryInstantiate<TContainer>(out instance);
		}
	}
	public readonly struct PropertyCollection<TContainer> : IEnumerable<IProperty<TContainer>>, IEnumerable
	{
		private enum EnumeratorType
		{
			Empty,
			Enumerable,
			List,
			IndexedCollectionPropertyBag
		}

		public struct Enumerator : IEnumerator<IProperty<TContainer>>, IEnumerator, IDisposable
		{
			private readonly EnumeratorType m_Type;

			private IEnumerator<IProperty<TContainer>> m_Enumerator;

			private List<IProperty<TContainer>>.Enumerator m_Properties;

			private IndexedCollectionPropertyBagEnumerator<TContainer> m_IndexedCollectionPropertyBag;

			public IProperty<TContainer> Current { get; private set; }

			object IEnumerator.Current => Current;

			internal Enumerator(IEnumerator<IProperty<TContainer>> enumerator)
			{
				m_Type = EnumeratorType.Enumerable;
				m_Enumerator = enumerator;
				m_Properties = default(List<IProperty<TContainer>>.Enumerator);
				m_IndexedCollectionPropertyBag = default(IndexedCollectionPropertyBagEnumerator<TContainer>);
				Current = null;
			}

			internal Enumerator(List<IProperty<TContainer>>.Enumerator properties)
			{
				m_Type = EnumeratorType.List;
				m_Enumerator = null;
				m_Properties = properties;
				m_IndexedCollectionPropertyBag = default(IndexedCollectionPropertyBagEnumerator<TContainer>);
				Current = null;
			}

			internal Enumerator(IndexedCollectionPropertyBagEnumerator<TContainer> enumerator)
			{
				m_Type = EnumeratorType.IndexedCollectionPropertyBag;
				m_Enumerator = null;
				m_Properties = default(List<IProperty<TContainer>>.Enumerator);
				m_IndexedCollectionPropertyBag = enumerator;
				Current = null;
			}

			public bool MoveNext()
			{
				bool result;
				switch (m_Type)
				{
				case EnumeratorType.Empty:
					return false;
				case EnumeratorType.Enumerable:
					result = m_Enumerator.MoveNext();
					Current = m_Enumerator.Current;
					break;
				case EnumeratorType.List:
					result = m_Properties.MoveNext();
					Current = m_Properties.Current;
					break;
				case EnumeratorType.IndexedCollectionPropertyBag:
					result = m_IndexedCollectionPropertyBag.MoveNext();
					Current = m_IndexedCollectionPropertyBag.Current;
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				return result;
			}

			public void Reset()
			{
				switch (m_Type)
				{
				case EnumeratorType.Empty:
					break;
				case EnumeratorType.Enumerable:
					m_Enumerator.Reset();
					break;
				case EnumeratorType.List:
					((IEnumerator)m_Properties).Reset();
					break;
				case EnumeratorType.IndexedCollectionPropertyBag:
					m_IndexedCollectionPropertyBag.Reset();
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}

			public void Dispose()
			{
				switch (m_Type)
				{
				case EnumeratorType.Empty:
					break;
				case EnumeratorType.Enumerable:
					m_Enumerator.Dispose();
					break;
				case EnumeratorType.List:
					break;
				case EnumeratorType.IndexedCollectionPropertyBag:
					m_IndexedCollectionPropertyBag.Dispose();
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		private readonly EnumeratorType m_Type;

		private readonly IEnumerable<IProperty<TContainer>> m_Enumerable;

		private readonly List<IProperty<TContainer>> m_Properties;

		private readonly IndexedCollectionPropertyBagEnumerable<TContainer> m_IndexedCollectionPropertyBag;

		public static PropertyCollection<TContainer> Empty { get; } = default(PropertyCollection<TContainer>);

		public PropertyCollection(IEnumerable<IProperty<TContainer>> enumerable)
		{
			m_Type = EnumeratorType.Enumerable;
			m_Enumerable = enumerable;
			m_Properties = null;
			m_IndexedCollectionPropertyBag = default(IndexedCollectionPropertyBagEnumerable<TContainer>);
		}

		public PropertyCollection(List<IProperty<TContainer>> properties)
		{
			m_Type = EnumeratorType.List;
			m_Enumerable = null;
			m_Properties = properties;
			m_IndexedCollectionPropertyBag = default(IndexedCollectionPropertyBagEnumerable<TContainer>);
		}

		internal PropertyCollection(IndexedCollectionPropertyBagEnumerable<TContainer> enumerable)
		{
			m_Type = EnumeratorType.IndexedCollectionPropertyBag;
			m_Enumerable = null;
			m_Properties = null;
			m_IndexedCollectionPropertyBag = enumerable;
		}

		public Enumerator GetEnumerator()
		{
			return m_Type switch
			{
				EnumeratorType.Empty => default(Enumerator), 
				EnumeratorType.Enumerable => new Enumerator(m_Enumerable.GetEnumerator()), 
				EnumeratorType.List => new Enumerator(m_Properties.GetEnumerator()), 
				EnumeratorType.IndexedCollectionPropertyBag => new Enumerator(m_IndexedCollectionPropertyBag.GetEnumerator()), 
				_ => throw new ArgumentOutOfRangeException(), 
			};
		}

		IEnumerator<IProperty<TContainer>> IEnumerable<IProperty<TContainer>>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	public class SetPropertyBagBase<TSet, TElement> : PropertyBag<TSet>, ISetPropertyBag<TSet, TElement>, ICollectionPropertyBag<TSet, TElement>, IPropertyBag<TSet>, IPropertyBag, ICollectionPropertyBagAccept<TSet>, ISetPropertyBagAccept<TSet>, ISetPropertyAccept<TSet>, IKeyedProperties<TSet, object> where TSet : ISet<TElement>
	{
		private class SetElementProperty : Property<TSet, TElement>, ISetElementProperty<TElement>, ISetElementProperty, ICollectionElementProperty
		{
			internal TElement m_Value;

			public override string Name => m_Value.ToString();

			public override bool IsReadOnly => true;

			public TElement Key => m_Value;

			public object ObjectKey => m_Value;

			public override TElement GetValue(ref TSet container)
			{
				return m_Value;
			}

			public override void SetValue(ref TSet container, TElement value)
			{
				throw new InvalidOperationException("Property is ReadOnly.");
			}
		}

		private readonly SetElementProperty m_Property = new SetElementProperty();

		public override PropertyCollection<TSet> GetProperties()
		{
			return PropertyCollection<TSet>.Empty;
		}

		public override PropertyCollection<TSet> GetProperties(ref TSet container)
		{
			return new PropertyCollection<TSet>(GetPropertiesEnumerable(container));
		}

		private IEnumerable<IProperty<TSet>> GetPropertiesEnumerable(TSet container)
		{
			foreach (TElement element in container)
			{
				m_Property.m_Value = element;
				yield return m_Property;
			}
		}

		void ICollectionPropertyBagAccept<TSet>.Accept(ICollectionPropertyBagVisitor visitor, ref TSet container)
		{
			visitor.Visit(this, ref container);
		}

		void ISetPropertyBagAccept<TSet>.Accept(ISetPropertyBagVisitor visitor, ref TSet container)
		{
			visitor.Visit(this, ref container);
		}

		void ISetPropertyAccept<TSet>.Accept<TContainer>(ISetPropertyVisitor visitor, Property<TContainer, TSet> property, ref TContainer container, ref TSet dictionary)
		{
			using (new AttributesScope(m_Property, property))
			{
				visitor.Visit<TContainer, TSet, TElement>(property, ref container, ref dictionary);
			}
		}

		public bool TryGetProperty(ref TSet container, object key, out IProperty<TSet> property)
		{
			TElement item = (TElement)key;
			if (container.Contains(item))
			{
				property = new SetElementProperty
				{
					m_Value = (TElement)key
				};
				return true;
			}
			property = null;
			return false;
		}
	}
	public interface IExcludePropertyAdapter<TContainer, TValue> : IPropertyVisitorAdapter
	{
		bool IsExcluded(in ExcludeContext<TContainer, TValue> context, ref TContainer container, ref TValue value);
	}
	public interface IExcludePropertyAdapter<TValue> : IPropertyVisitorAdapter
	{
		bool IsExcluded<TContainer>(in ExcludeContext<TContainer, TValue> context, ref TContainer container, ref TValue value);
	}
	public interface IExcludePropertyAdapter : IPropertyVisitorAdapter
	{
		bool IsExcluded<TContainer, TValue>(in ExcludeContext<TContainer, TValue> context, ref TContainer container, ref TValue value);
	}
	public interface IExcludeContravariantPropertyAdapter<TContainer, in TValue> : IPropertyVisitorAdapter
	{
		bool IsExcluded(in ExcludeContext<TContainer> context, ref TContainer container, TValue value);
	}
	public interface IExcludeContravariantPropertyAdapter<in TValue> : IPropertyVisitorAdapter
	{
		bool IsExcluded<TContainer>(in ExcludeContext<TContainer> context, ref TContainer container, TValue value);
	}
	public interface IVisitPrimitivesPropertyAdapter : IVisitPropertyAdapter<sbyte>, IPropertyVisitorAdapter, IVisitPropertyAdapter<short>, IVisitPropertyAdapter<int>, IVisitPropertyAdapter<long>, IVisitPropertyAdapter<byte>, IVisitPropertyAdapter<ushort>, IVisitPropertyAdapter<uint>, IVisitPropertyAdapter<ulong>, IVisitPropertyAdapter<float>, IVisitPropertyAdapter<double>, IVisitPropertyAdapter<bool>, IVisitPropertyAdapter<char>
	{
	}
	public interface IVisitPropertyAdapter<TContainer, TValue> : IPropertyVisitorAdapter
	{
		void Visit(in VisitContext<TContainer, TValue> context, ref TContainer container, ref TValue value);
	}
	public interface IVisitPropertyAdapter<TValue> : IPropertyVisitorAdapter
	{
		void Visit<TContainer>(in VisitContext<TContainer, TValue> context, ref TContainer container, ref TValue value);
	}
	public interface IVisitPropertyAdapter : IPropertyVisitorAdapter
	{
		void Visit<TContainer, TValue>(in VisitContext<TContainer, TValue> context, ref TContainer container, ref TValue value);
	}
	public interface IVisitContravariantPropertyAdapter<TContainer, in TValue> : IPropertyVisitorAdapter
	{
		void Visit(in VisitContext<TContainer> context, ref TContainer container, TValue value);
	}
	public interface IVisitContravariantPropertyAdapter<in TValue> : IPropertyVisitorAdapter
	{
		void Visit<TContainer>(in VisitContext<TContainer> context, ref TContainer container, TValue value);
	}
	public abstract class ConcreteTypeVisitor : IPropertyBagVisitor
	{
		protected abstract void VisitContainer<TContainer>(ref TContainer container);

		void IPropertyBagVisitor.Visit<TContainer>(IPropertyBag<TContainer> properties, ref TContainer container)
		{
			VisitContainer(ref container);
		}
	}
	public readonly struct ExcludeContext<TContainer, TValue>
	{
		private readonly PropertyVisitor m_Visitor;

		public Property<TContainer, TValue> Property { get; }

		internal static ExcludeContext<TContainer, TValue> FromProperty(PropertyVisitor visitor, Property<TContainer, TValue> property)
		{
			return new ExcludeContext<TContainer, TValue>(visitor, property);
		}

		private ExcludeContext(PropertyVisitor visitor, Property<TContainer, TValue> property)
		{
			m_Visitor = visitor;
			Property = property;
		}
	}
	public readonly struct ExcludeContext<TContainer>
	{
		private readonly PropertyVisitor m_Visitor;

		public IProperty<TContainer> Property { get; }

		internal static ExcludeContext<TContainer> FromProperty<TValue>(PropertyVisitor visitor, Property<TContainer, TValue> property)
		{
			return new ExcludeContext<TContainer>(visitor, property);
		}

		private ExcludeContext(PropertyVisitor visitor, IProperty<TContainer> property)
		{
			m_Visitor = visitor;
			Property = property;
		}
	}
	public interface ICollectionPropertyBagAccept<TContainer>
	{
		void Accept(ICollectionPropertyBagVisitor visitor, ref TContainer container);
	}
	public interface IListPropertyBagAccept<TContainer>
	{
		void Accept(IListPropertyBagVisitor visitor, ref TContainer container);
	}
	public interface ISetPropertyBagAccept<TContainer>
	{
		void Accept(ISetPropertyBagVisitor visitor, ref TContainer container);
	}
	public interface IDictionaryPropertyBagAccept<TContainer>
	{
		void Accept(IDictionaryPropertyBagVisitor visitor, ref TContainer container);
	}
	public interface IPropertyAccept<TContainer>
	{
		void Accept(IPropertyVisitor visitor, ref TContainer container);
	}
	public interface ICollectionPropertyAccept<TCollection>
	{
		void Accept<TContainer>(ICollectionPropertyVisitor visitor, Property<TContainer, TCollection> property, ref TContainer container, ref TCollection collection);
	}
	public interface IListPropertyAccept<TList>
	{
		void Accept<TContainer>(IListPropertyVisitor visitor, Property<TContainer, TList> property, ref TContainer container, ref TList list);
	}
	public interface ISetPropertyAccept<TSet>
	{
		void Accept<TContainer>(ISetPropertyVisitor visitor, Property<TContainer, TSet> property, ref TContainer container, ref TSet set);
	}
	public interface IDictionaryPropertyAccept<TDictionary>
	{
		void Accept<TContainer>(IDictionaryPropertyVisitor visitor, Property<TContainer, TDictionary> property, ref TContainer container, ref TDictionary dictionary);
	}
	public interface ITypeVisitor
	{
		void Visit<TContainer>();
	}
	public interface IPropertyBagVisitor
	{
		void Visit<TContainer>(IPropertyBag<TContainer> properties, ref TContainer container);
	}
	public interface ICollectionPropertyBagVisitor
	{
		void Visit<TCollection, TElement>(ICollectionPropertyBag<TCollection, TElement> properties, ref TCollection container) where TCollection : ICollection<TElement>;
	}
	public interface IListPropertyBagVisitor
	{
		void Visit<TList, TElement>(IListPropertyBag<TList, TElement> properties, ref TList container) where TList : IList<TElement>;
	}
	public interface ISetPropertyBagVisitor
	{
		void Visit<TSet, TValue>(ISetPropertyBag<TSet, TValue> properties, ref TSet container) where TSet : ISet<TValue>;
	}
	public interface IDictionaryPropertyBagVisitor
	{
		void Visit<TDictionary, TKey, TValue>(IDictionaryPropertyBag<TDictionary, TKey, TValue> properties, ref TDictionary container) where TDictionary : IDictionary<TKey, TValue>;
	}
	public interface IPropertyVisitor
	{
		void Visit<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container);
	}
	public interface ICollectionPropertyVisitor
	{
		void Visit<TContainer, TCollection, TElement>(Property<TContainer, TCollection> property, ref TContainer container, ref TCollection collection) where TCollection : ICollection<TElement>;
	}
	public interface IListPropertyVisitor
	{
		void Visit<TContainer, TList, TElement>(Property<TContainer, TList> property, ref TContainer container, ref TList list) where TList : IList<TElement>;
	}
	public interface ISetPropertyVisitor
	{
		void Visit<TContainer, TSet, TValue>(Property<TContainer, TSet> property, ref TContainer container, ref TSet set) where TSet : ISet<TValue>;
	}
	public interface IDictionaryPropertyVisitor
	{
		void Visit<TContainer, TDictionary, TKey, TValue>(Property<TContainer, TDictionary> property, ref TContainer container, ref TDictionary dictionary) where TDictionary : IDictionary<TKey, TValue>;
	}
	public abstract class PathVisitor : IPropertyBagVisitor, IPropertyVisitor
	{
		private readonly struct PropertyScope : IDisposable
		{
			private readonly PathVisitor m_Visitor;

			private readonly IProperty m_Property;

			public PropertyScope(PathVisitor visitor, IProperty property)
			{
				m_Visitor = visitor;
				m_Property = m_Visitor.Property;
				m_Visitor.Property = property;
			}

			public void Dispose()
			{
				m_Visitor.Property = m_Property;
			}
		}

		private int m_PathIndex;

		public PropertyPath Path { get; set; }

		private IProperty Property { get; set; }

		public bool ReadonlyVisit { get; set; }

		public VisitReturnCode ReturnCode { get; protected set; }

		public virtual void Reset()
		{
			m_PathIndex = 0;
			Path = default(PropertyPath);
			ReturnCode = VisitReturnCode.Ok;
			ReadonlyVisit = false;
		}

		void IPropertyBagVisitor.Visit<TContainer>(IPropertyBag<TContainer> properties, ref TContainer container)
		{
			PropertyPathPart propertyPathPart = Path[m_PathIndex++];
			IProperty<TContainer> property;
			switch (propertyPathPart.Kind)
			{
			case PropertyPathPartKind.Name:
				if (properties is INamedProperties<TContainer> namedProperties && namedProperties.TryGetProperty(ref container, propertyPathPart.Name, out property))
				{
					property.Accept(this, ref container);
				}
				else
				{
					ReturnCode = VisitReturnCode.InvalidPath;
				}
				break;
			case PropertyPathPartKind.Index:
				if (properties is IIndexedProperties<TContainer> indexedProperties && indexedProperties.TryGetProperty(ref container, propertyPathPart.Index, out property))
				{
					using ((property as IAttributes).CreateAttributesScope(Property as IAttributes))
					{
						property.Accept(this, ref container);
						break;
					}
				}
				ReturnCode = VisitReturnCode.InvalidPath;
				break;
			case PropertyPathPartKind.Key:
				if (properties is IKeyedProperties<TContainer, object> keyedProperties && keyedProperties.TryGetProperty(ref container, propertyPathPart.Key, out property))
				{
					using ((property as IAttributes).CreateAttributesScope(Property as IAttributes))
					{
						property.Accept(this, ref container);
						break;
					}
				}
				ReturnCode = VisitReturnCode.InvalidPath;
				break;
			default:
				ReturnCode = VisitReturnCode.InvalidPath;
				break;
			}
		}

		void IPropertyVisitor.Visit<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container)
		{
			TValue value = property.GetValue(ref container);
			IPropertyBag propertyBag;
			if (m_PathIndex >= Path.Length)
			{
				VisitPath(property, ref container, ref value);
			}
			else if (PropertyBag.TryGetPropertyBagForValue(ref value, out propertyBag))
			{
				if (TypeTraits<TValue>.CanBeNull && EqualityComparer<TValue>.Default.Equals(value, default(TValue)))
				{
					ReturnCode = VisitReturnCode.InvalidPath;
					return;
				}
				using (new PropertyScope(this, property))
				{
					PropertyContainer.Accept(this, ref value);
				}
				if (!property.IsReadOnly && !ReadonlyVisit)
				{
					property.SetValue(ref container, value);
				}
			}
			else
			{
				ReturnCode = VisitReturnCode.InvalidPath;
			}
		}

		protected virtual void VisitPath<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
		{
		}
	}
	public interface IPropertyVisitorAdapter
	{
	}
	public abstract class PropertyVisitor : IPropertyBagVisitor, IListPropertyBagVisitor, IDictionaryPropertyBagVisitor, IPropertyVisitor, ICollectionPropertyVisitor, IListPropertyVisitor, ISetPropertyVisitor, IDictionaryPropertyVisitor
	{
		private readonly List<IPropertyVisitorAdapter> m_Adapters = new List<IPropertyVisitorAdapter>();

		public void AddAdapter(IPropertyVisitorAdapter adapter)
		{
			m_Adapters.Add(adapter);
		}

		public void RemoveAdapter(IPropertyVisitorAdapter adapter)
		{
			m_Adapters.Remove(adapter);
		}

		void IPropertyBagVisitor.Visit<TContainer>(IPropertyBag<TContainer> properties, ref TContainer container)
		{
			foreach (IProperty<TContainer> property in properties.GetProperties(ref container))
			{
				property.Accept(this, ref container);
			}
		}

		void IListPropertyBagVisitor.Visit<TList, TElement>(IListPropertyBag<TList, TElement> properties, ref TList container)
		{
			foreach (IProperty<TList> property in properties.GetProperties(ref container))
			{
				property.Accept(this, ref container);
			}
		}

		void IDictionaryPropertyBagVisitor.Visit<TDictionary, TKey, TValue>(IDictionaryPropertyBag<TDictionary, TKey, TValue> properties, ref TDictionary container)
		{
			foreach (IProperty<TDictionary> property in properties.GetProperties(ref container))
			{
				property.Accept(this, ref container);
			}
		}

		void IPropertyVisitor.Visit<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container)
		{
			TValue value = property.GetValue(ref container);
			if (!IsExcluded(property, new ReadOnlyAdapterCollection(m_Adapters).GetEnumerator(), ref container, ref value) && !IsExcluded(property, ref container, ref value))
			{
				ContinueVisitation(property, new ReadOnlyAdapterCollection(m_Adapters).GetEnumerator(), ref container, ref value);
				if (!property.IsReadOnly)
				{
					property.SetValue(ref container, value);
				}
			}
		}

		internal void ContinueVisitation<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
		{
			if (PropertyBagStore.TryGetPropertyBagForValue(ref value, out var propertyBag))
			{
				IPropertyBag propertyBag2 = propertyBag;
				IPropertyBag propertyBag3 = propertyBag2;
				if (propertyBag3 is IDictionaryPropertyAccept<TValue> dictionaryPropertyAccept)
				{
					dictionaryPropertyAccept.Accept(this, property, ref container, ref value);
					return;
				}
				if (propertyBag3 is IListPropertyAccept<TValue> listPropertyAccept)
				{
					listPropertyAccept.Accept(this, property, ref container, ref value);
					return;
				}
				if (propertyBag3 is ISetPropertyAccept<TValue> setPropertyAccept)
				{
					setPropertyAccept.Accept(this, property, ref container, ref value);
					return;
				}
				if (propertyBag3 is ICollectionPropertyAccept<TValue> collectionPropertyAccept)
				{
					collectionPropertyAccept.Accept(this, property, ref container, ref value);
					return;
				}
			}
			VisitProperty(property, ref container, ref value);
		}

		void ICollectionPropertyVisitor.Visit<TContainer, TCollection, TElement>(Property<TContainer, TCollection> property, ref TContainer container, ref TCollection collection)
		{
			VisitCollection<TContainer, TCollection, TElement>(property, ref container, ref collection);
		}

		void IListPropertyVisitor.Visit<TContainer, TList, TElement>(Property<TContainer, TList> property, ref TContainer container, ref TList list)
		{
			VisitList<TContainer, TList, TElement>(property, ref container, ref list);
		}

		void ISetPropertyVisitor.Visit<TContainer, TSet, TElement>(Property<TContainer, TSet> property, ref TContainer container, ref TSet set)
		{
			VisitSet<TContainer, TSet, TElement>(property, ref container, ref set);
		}

		void IDictionaryPropertyVisitor.Visit<TContainer, TDictionary, TKey, TValue>(Property<TContainer, TDictionary> property, ref TContainer container, ref TDictionary dictionary)
		{
			VisitDictionary<TContainer, TDictionary, TKey, TValue>(property, ref container, ref dictionary);
		}

		protected virtual bool IsExcluded<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
		{
			return false;
		}

		protected virtual void VisitProperty<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
		{
			PropertyContainer.TryAccept(this, ref value);
		}

		protected virtual void VisitCollection<TContainer, TCollection, TElement>(Property<TContainer, TCollection> property, ref TContainer container, ref TCollection value) where TCollection : ICollection<TElement>
		{
			VisitProperty(property, ref container, ref value);
		}

		protected virtual void VisitList<TContainer, TList, TElement>(Property<TContainer, TList> property, ref TContainer container, ref TList value) where TList : IList<TElement>
		{
			VisitCollection<TContainer, TList, TElement>(property, ref container, ref value);
		}

		protected virtual void VisitSet<TContainer, TSet, TValue>(Property<TContainer, TSet> property, ref TContainer container, ref TSet value) where TSet : ISet<TValue>
		{
			VisitCollection<TContainer, TSet, TValue>(property, ref container, ref value);
		}

		protected virtual void VisitDictionary<TContainer, TDictionary, TKey, TValue>(Property<TContainer, TDictionary> property, ref TContainer container, ref TDictionary value) where TDictionary : IDictionary<TKey, TValue>
		{
			VisitCollection<TContainer, TDictionary, KeyValuePair<TKey, TValue>>(property, ref container, ref value);
		}

		private bool IsExcluded<TContainer, TValue>(Property<TContainer, TValue> property, ReadOnlyAdapterCollection.Enumerator enumerator, ref TContainer container, ref TValue value)
		{
			while (enumerator.MoveNext())
			{
				IPropertyVisitorAdapter current = enumerator.Current;
				IPropertyVisitorAdapter propertyVisitorAdapter = current;
				IPropertyVisitorAdapter propertyVisitorAdapter2 = propertyVisitorAdapter;
				if (!(propertyVisitorAdapter2 is IExcludePropertyAdapter<TContainer, TValue> excludePropertyAdapter))
				{
					if (!(propertyVisitorAdapter2 is IExcludeContravariantPropertyAdapter<TContainer, TValue> excludeContravariantPropertyAdapter))
					{
						if (!(propertyVisitorAdapter2 is IExcludePropertyAdapter<TValue> excludePropertyAdapter2))
						{
							if (!(propertyVisitorAdapter2 is IExcludeContravariantPropertyAdapter<TValue> excludeContravariantPropertyAdapter2))
							{
								if (!(propertyVisitorAdapter2 is IExcludePropertyAdapter excludePropertyAdapter3) || !excludePropertyAdapter3.IsExcluded(ExcludeContext<TContainer, TValue>.FromProperty(this, property), ref container, ref value))
								{
									continue;
								}
								return true;
							}
							bool flag = excludeContravariantPropertyAdapter2.IsExcluded(ExcludeContext<TContainer>.FromProperty(this, property), ref container, value);
							value = property.GetValue(ref container);
							if (flag)
							{
								return true;
							}
						}
						else if (excludePropertyAdapter2.IsExcluded(ExcludeContext<TContainer, TValue>.FromProperty(this, property), ref container, ref value))
						{
							return true;
						}
					}
					else
					{
						bool flag2 = excludeContravariantPropertyAdapter.IsExcluded(ExcludeContext<TContainer>.FromProperty(this, property), ref container, value);
						value = property.GetValue(ref container);
						if (flag2)
						{
							return true;
						}
					}
				}
				else if (excludePropertyAdapter.IsExcluded(ExcludeContext<TContainer, TValue>.FromProperty(this, property), ref container, ref value))
				{
					return true;
				}
			}
			return false;
		}

		internal void ContinueVisitation<TContainer, TValue>(Property<TContainer, TValue> property, ReadOnlyAdapterCollection.Enumerator enumerator, ref TContainer container, ref TValue value)
		{
			while (enumerator.MoveNext())
			{
				IPropertyVisitorAdapter current = enumerator.Current;
				IPropertyVisitorAdapter propertyVisitorAdapter = current;
				IPropertyVisitorAdapter propertyVisitorAdapter2 = propertyVisitorAdapter;
				if (!(propertyVisitorAdapter2 is IVisitPropertyAdapter<TContainer, TValue> visitPropertyAdapter))
				{
					if (!(propertyVisitorAdapter2 is IVisitContravariantPropertyAdapter<TContainer, TValue> visitContravariantPropertyAdapter))
					{
						if (!(propertyVisitorAdapter2 is IVisitPropertyAdapter<TValue> visitPropertyAdapter2))
						{
							if (!(propertyVisitorAdapter2 is IVisitContravariantPropertyAdapter<TValue> visitContravariantPropertyAdapter2))
							{
								if (!(propertyVisitorAdapter2 is IVisitPropertyAdapter visitPropertyAdapter3))
								{
									continue;
								}
								visitPropertyAdapter3.Visit(VisitContext<TContainer, TValue>.FromProperty(this, enumerator, property), ref container, ref value);
								return;
							}
							visitContravariantPropertyAdapter2.Visit(VisitContext<TContainer>.FromProperty(this, enumerator, property), ref container, value);
							value = property.GetValue(ref container);
							return;
						}
						visitPropertyAdapter2.Visit(VisitContext<TContainer, TValue>.FromProperty(this, enumerator, property), ref container, ref value);
						return;
					}
					visitContravariantPropertyAdapter.Visit(VisitContext<TContainer>.FromProperty(this, enumerator, property), ref container, value);
					value = property.GetValue(ref container);
					return;
				}
				visitPropertyAdapter.Visit(VisitContext<TContainer, TValue>.FromProperty(this, enumerator, property), ref container, ref value);
				return;
			}
			ContinueVisitationWithoutAdapters(property, enumerator, ref container, ref value);
		}

		internal void ContinueVisitationWithoutAdapters<TContainer, TValue>(Property<TContainer, TValue> property, ReadOnlyAdapterCollection.Enumerator enumerator, ref TContainer container, ref TValue value)
		{
			ContinueVisitation(property, ref container, ref value);
		}
	}
	public readonly struct VisitContext<TContainer, TValue>
	{
		private readonly ReadOnlyAdapterCollection.Enumerator m_Enumerator;

		private readonly PropertyVisitor m_Visitor;

		public Property<TContainer, TValue> Property { get; }

		internal static VisitContext<TContainer, TValue> FromProperty(PropertyVisitor visitor, ReadOnlyAdapterCollection.Enumerator enumerator, Property<TContainer, TValue> property)
		{
			return new VisitContext<TContainer, TValue>(visitor, enumerator, property);
		}

		private VisitContext(PropertyVisitor visitor, ReadOnlyAdapterCollection.Enumerator enumerator, Property<TContainer, TValue> property)
		{
			m_Visitor = visitor;
			m_Enumerator = enumerator;
			Property = property;
		}

		public void ContinueVisitation(ref TContainer container, ref TValue value)
		{
			m_Visitor.ContinueVisitation(Property, m_Enumerator, ref container, ref value);
		}

		public void ContinueVisitationWithoutAdapters(ref TContainer container, ref TValue value)
		{
			m_Visitor.ContinueVisitationWithoutAdapters(Property, m_Enumerator, ref container, ref value);
		}
	}
	public readonly struct VisitContext<TContainer>
	{
		private delegate void VisitDelegate(PropertyVisitor visitor, ReadOnlyAdapterCollection.Enumerator enumerator, IProperty<TContainer> property, ref TContainer container);

		private delegate void VisitWithoutAdaptersDelegate(PropertyVisitor visitor, IProperty<TContainer> property, ref TContainer container);

		private readonly ReadOnlyAdapterCollection.Enumerator m_Enumerator;

		private readonly PropertyVisitor m_Visitor;

		private readonly VisitDelegate m_Continue;

		private readonly VisitWithoutAdaptersDelegate m_ContinueWithoutAdapters;

		public IProperty<TContainer> Property { get; }

		internal static VisitContext<TContainer> FromProperty<TValue>(PropertyVisitor visitor, ReadOnlyAdapterCollection.Enumerator enumerator, Property<TContainer, TValue> property)
		{
			return new VisitContext<TContainer>(visitor, enumerator, property, delegate(PropertyVisitor v, ReadOnlyAdapterCollection.Enumerator e, IProperty<TContainer> p, ref TContainer c)
			{
				Property<TContainer, TValue> property2 = (Property<TContainer, TValue>)p;
				TValue value = property2.GetValue(ref c);
				v.ContinueVisitation(property2, e, ref c, ref value);
			}, delegate(PropertyVisitor v, IProperty<TContainer> p, ref TContainer c)
			{
				Property<TContainer, TValue> property2 = (Property<TContainer, TValue>)p;
				TValue value = property2.GetValue(ref c);
				v.ContinueVisitation(property2, ref c, ref value);
			});
		}

		private VisitContext(PropertyVisitor visitor, ReadOnlyAdapterCollection.Enumerator enumerator, IProperty<TContainer> property, VisitDelegate continueVisitation, VisitWithoutAdaptersDelegate continueVisitationWithoutAdapters)
		{
			m_Visitor = visitor;
			m_Enumerator = enumerator;
			Property = property;
			m_Continue = continueVisitation;
			m_ContinueWithoutAdapters = continueVisitationWithoutAdapters;
		}

		public void ContinueVisitation(ref TContainer container)
		{
			m_Continue(m_Visitor, m_Enumerator, Property, ref container);
		}

		public void ContinueVisitationWithoutAdapters(ref TContainer container)
		{
			m_ContinueWithoutAdapters(m_Visitor, Property, ref container);
		}
	}
	internal readonly struct ConversionRegistry : IEqualityComparer<ConversionRegistry>
	{
		private class ConverterKeyComparer : IEqualityComparer<ConverterKey>
		{
			public bool Equals(ConverterKey x, ConverterKey y)
			{
				return x.SourceType == y.SourceType && x.DestinationType == y.DestinationType;
			}

			public int GetHashCode(ConverterKey obj)
			{
				return (((obj.SourceType != null) ? obj.SourceType.GetHashCode() : 0) * 397) ^ ((obj.DestinationType != null) ? obj.DestinationType.GetHashCode() : 0);
			}
		}

		private readonly struct ConverterKey
		{
			public readonly Type SourceType;

			public readonly Type DestinationType;

			public ConverterKey(Type source, Type destination)
			{
				SourceType = source;
				DestinationType = destination;
			}
		}

		private static readonly ConverterKeyComparer Comparer = new ConverterKeyComparer();

		private readonly Dictionary<ConverterKey, Delegate> m_Converters;

		public int ConverterCount => m_Converters?.Count ?? 0;

		private ConversionRegistry(Dictionary<ConverterKey, Delegate> storage)
		{
			m_Converters = storage;
		}

		public static ConversionRegistry Create()
		{
			return new ConversionRegistry(new Dictionary<ConverterKey, Delegate>(Comparer));
		}

		public void Register(Type source, Type destination, Delegate converter)
		{
			m_Converters[new ConverterKey(source, destination)] = converter ?? throw new ArgumentException("converter");
		}

		public void Unregister(Type source, Type destination)
		{
			m_Converters.Remove(new ConverterKey(source, destination));
		}

		public Delegate GetConverter(Type source, Type destination)
		{
			ConverterKey key = new ConverterKey(source, destination);
			Delegate value;
			return m_Converters.TryGetValue(key, out value) ? value : null;
		}

		public bool TryGetConverter(Type source, Type destination, out Delegate converter)
		{
			converter = GetConverter(source, destination);
			return (object)converter != null;
		}

		public void GetAllTypesConvertingToType(Type type, List<Type> result)
		{
			foreach (ConverterKey key in m_Converters.Keys)
			{
				if (key.DestinationType == type)
				{
					result.Add(key.SourceType);
				}
			}
		}

		public bool Equals(ConversionRegistry x, ConversionRegistry y)
		{
			return x.m_Converters == y.m_Converters;
		}

		public int GetHashCode(ConversionRegistry obj)
		{
			return (obj.m_Converters != null) ? obj.m_Converters.GetHashCode() : 0;
		}
	}
	public delegate TDestination TypeConverter<TSource, out TDestination>(ref TSource value);
	public static class TypeConversion
	{
		private static class PrimitiveConverters
		{
			public static void Register()
			{
				RegisterInt8Converters();
				RegisterInt16Converters();
				RegisterInt32Converters();
				RegisterInt64Converters();
				RegisterUInt8Converters();
				RegisterUInt16Converters();
				RegisterUInt32Converters();
				RegisterUInt64Converters();
				RegisterFloat32Converters();
				RegisterFloat64Converters();
				RegisterBooleanConverters();
				RegisterCharConverters();
				RegisterStringConverters();
				RegisterObjectConverters();
				s_GlobalConverters.Register(typeof(string), typeof(Guid), (TypeConverter<string, Guid>)delegate(ref string g)
				{
					return new Guid(g);
				});
			}

			private static void RegisterInt8Converters()
			{
				s_GlobalConverters.Register(typeof(sbyte), typeof(char), (TypeConverter<sbyte, char>)delegate(ref sbyte v)
				{
					return (char)v;
				});
				s_GlobalConverters.Register(typeof(sbyte), typeof(bool), (TypeConverter<sbyte, bool>)delegate(ref sbyte v)
				{
					return v != 0;
				});
				s_GlobalConverters.Register(typeof(sbyte), typeof(short), (TypeConverter<sbyte, short>)delegate(ref sbyte v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(sbyte), typeof(int), (TypeConverter<sbyte, int>)delegate(ref sbyte v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(sbyte), typeof(long), (TypeConverter<sbyte, long>)delegate(ref sbyte v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(sbyte), typeof(byte), (TypeConverter<sbyte, byte>)delegate(ref sbyte v)
				{
					return (byte)v;
				});
				s_GlobalConverters.Register(typeof(sbyte), typeof(ushort), (TypeConverter<sbyte, ushort>)delegate(ref sbyte v)
				{
					return (ushort)v;
				});
				s_GlobalConverters.Register(typeof(sbyte), typeof(uint), (TypeConverter<sbyte, uint>)delegate(ref sbyte v)
				{
					return (uint)v;
				});
				s_GlobalConverters.Register(typeof(sbyte), typeof(ulong), (TypeConverter<sbyte, ulong>)delegate(ref sbyte v)
				{
					return (ulong)v;
				});
				s_GlobalConverters.Register(typeof(sbyte), typeof(float), (TypeConverter<sbyte, float>)delegate(ref sbyte v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(sbyte), typeof(double), (TypeConverter<sbyte, double>)delegate(ref sbyte v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(sbyte), typeof(object), (TypeConverter<sbyte, object>)delegate(ref sbyte v)
				{
					return v;
				});
			}

			private static void RegisterInt16Converters()
			{
				s_GlobalConverters.Register(typeof(short), typeof(sbyte), (TypeConverter<short, sbyte>)delegate(ref short v)
				{
					return (sbyte)v;
				});
				s_GlobalConverters.Register(typeof(short), typeof(char), (TypeConverter<short, char>)delegate(ref short v)
				{
					return (char)v;
				});
				s_GlobalConverters.Register(typeof(short), typeof(bool), (TypeConverter<short, bool>)delegate(ref short v)
				{
					return v != 0;
				});
				s_GlobalConverters.Register(typeof(short), typeof(int), (TypeConverter<short, int>)delegate(ref short v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(short), typeof(long), (TypeConverter<short, long>)delegate(ref short v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(short), typeof(byte), (TypeConverter<short, byte>)delegate(ref short v)
				{
					return (byte)v;
				});
				s_GlobalConverters.Register(typeof(short), typeof(ushort), (TypeConverter<short, ushort>)delegate(ref short v)
				{
					return (ushort)v;
				});
				s_GlobalConverters.Register(typeof(short), typeof(uint), (TypeConverter<short, uint>)delegate(ref short v)
				{
					return (uint)v;
				});
				s_GlobalConverters.Register(typeof(short), typeof(ulong), (TypeConverter<short, ulong>)delegate(ref short v)
				{
					return (ulong)v;
				});
				s_GlobalConverters.Register(typeof(short), typeof(float), (TypeConverter<short, float>)delegate(ref short v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(short), typeof(double), (TypeConverter<short, double>)delegate(ref short v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(short), typeof(object), (TypeConverter<short, object>)delegate(ref short v)
				{
					return v;
				});
			}

			private static void RegisterInt32Converters()
			{
				s_GlobalConverters.Register(typeof(int), typeof(sbyte), (TypeConverter<int, sbyte>)delegate(ref int v)
				{
					return (sbyte)v;
				});
				s_GlobalConverters.Register(typeof(int), typeof(char), (TypeConverter<int, char>)delegate(ref int v)
				{
					return (char)v;
				});
				s_GlobalConverters.Register(typeof(int), typeof(bool), (TypeConverter<int, bool>)delegate(ref int v)
				{
					return v != 0;
				});
				s_GlobalConverters.Register(typeof(int), typeof(short), (TypeConverter<int, short>)delegate(ref int v)
				{
					return (short)v;
				});
				s_GlobalConverters.Register(typeof(int), typeof(long), (TypeConverter<int, long>)delegate(ref int v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(int), typeof(byte), (TypeConverter<int, byte>)delegate(ref int v)
				{
					return (byte)v;
				});
				s_GlobalConverters.Register(typeof(int), typeof(ushort), (TypeConverter<int, ushort>)delegate(ref int v)
				{
					return (ushort)v;
				});
				s_GlobalConverters.Register(typeof(int), typeof(uint), (TypeConverter<int, uint>)delegate(ref int v)
				{
					return (uint)v;
				});
				s_GlobalConverters.Register(typeof(int), typeof(ulong), (TypeConverter<int, ulong>)delegate(ref int v)
				{
					return (ulong)v;
				});
				s_GlobalConverters.Register(typeof(int), typeof(float), (TypeConverter<int, float>)delegate(ref int v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(int), typeof(double), (TypeConverter<int, double>)delegate(ref int v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(int), typeof(object), (TypeConverter<int, object>)delegate(ref int v)
				{
					return v;
				});
			}

			private static void RegisterInt64Converters()
			{
				s_GlobalConverters.Register(typeof(long), typeof(sbyte), (TypeConverter<long, sbyte>)delegate(ref long v)
				{
					return (sbyte)v;
				});
				s_GlobalConverters.Register(typeof(long), typeof(char), (TypeConverter<long, char>)delegate(ref long v)
				{
					return (char)v;
				});
				s_GlobalConverters.Register(typeof(long), typeof(bool), (TypeConverter<long, bool>)delegate(ref long v)
				{
					return v != 0;
				});
				s_GlobalConverters.Register(typeof(long), typeof(short), (TypeConverter<long, short>)delegate(ref long v)
				{
					return (short)v;
				});
				s_GlobalConverters.Register(typeof(long), typeof(int), (TypeConverter<long, int>)delegate(ref long v)
				{
					return (int)v;
				});
				s_GlobalConverters.Register(typeof(long), typeof(byte), (TypeConverter<long, byte>)delegate(ref long v)
				{
					return (byte)v;
				});
				s_GlobalConverters.Register(typeof(long), typeof(ushort), (TypeConverter<long, ushort>)delegate(ref long v)
				{
					return (ushort)v;
				});
				s_GlobalConverters.Register(typeof(long), typeof(uint), (TypeConverter<long, uint>)delegate(ref long v)
				{
					return (uint)v;
				});
				s_GlobalConverters.Register(typeof(long), typeof(ulong), (TypeConverter<long, ulong>)delegate(ref long v)
				{
					return (ulong)v;
				});
				s_GlobalConverters.Register(typeof(long), typeof(float), (TypeConverter<long, float>)delegate(ref long v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(long), typeof(double), (TypeConverter<long, double>)delegate(ref long v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(long), typeof(object), (TypeConverter<long, object>)delegate(ref long v)
				{
					return v;
				});
			}

			private static void RegisterUInt8Converters()
			{
				s_GlobalConverters.Register(typeof(byte), typeof(sbyte), (TypeConverter<byte, sbyte>)delegate(ref byte v)
				{
					return (sbyte)v;
				});
				s_GlobalConverters.Register(typeof(byte), typeof(char), (TypeConverter<byte, char>)delegate(ref byte v)
				{
					return (char)v;
				});
				s_GlobalConverters.Register(typeof(byte), typeof(bool), (TypeConverter<byte, bool>)delegate(ref byte v)
				{
					return v != 0;
				});
				s_GlobalConverters.Register(typeof(byte), typeof(short), (TypeConverter<byte, short>)delegate(ref byte v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(byte), typeof(int), (TypeConverter<byte, int>)delegate(ref byte v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(byte), typeof(long), (TypeConverter<byte, long>)delegate(ref byte v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(byte), typeof(ushort), (TypeConverter<byte, ushort>)delegate(ref byte v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(byte), typeof(uint), (TypeConverter<byte, uint>)delegate(ref byte v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(byte), typeof(ulong), (TypeConverter<byte, ulong>)delegate(ref byte v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(byte), typeof(float), (TypeConverter<byte, float>)delegate(ref byte v)
				{
					return (int)v;
				});
				s_GlobalConverters.Register(typeof(byte), typeof(double), (TypeConverter<byte, double>)delegate(ref byte v)
				{
					return (int)v;
				});
				s_GlobalConverters.Register(typeof(byte), typeof(object), (TypeConverter<byte, object>)delegate(ref byte v)
				{
					return v;
				});
			}

			private static void RegisterUInt16Converters()
			{
				s_GlobalConverters.Register(typeof(ushort), typeof(sbyte), (TypeConverter<ushort, sbyte>)delegate(ref ushort v)
				{
					return (sbyte)v;
				});
				s_GlobalConverters.Register(typeof(ushort), typeof(char), (TypeConverter<ushort, char>)delegate(ref ushort v)
				{
					return (char)v;
				});
				s_GlobalConverters.Register(typeof(ushort), typeof(bool), (TypeConverter<ushort, bool>)delegate(ref ushort v)
				{
					return v != 0;
				});
				s_GlobalConverters.Register(typeof(ushort), typeof(short), (TypeConverter<ushort, short>)delegate(ref ushort v)
				{
					return (short)v;
				});
				s_GlobalConverters.Register(typeof(ushort), typeof(int), (TypeConverter<ushort, int>)delegate(ref ushort v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(ushort), typeof(long), (TypeConverter<ushort, long>)delegate(ref ushort v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(ushort), typeof(byte), (TypeConverter<ushort, byte>)delegate(ref ushort v)
				{
					return (byte)v;
				});
				s_GlobalConverters.Register(typeof(ushort), typeof(uint), (TypeConverter<ushort, uint>)delegate(ref ushort v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(ushort), typeof(ulong), (TypeConverter<ushort, ulong>)delegate(ref ushort v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(ushort), typeof(float), (TypeConverter<ushort, float>)delegate(ref ushort v)
				{
					return (int)v;
				});
				s_GlobalConverters.Register(typeof(ushort), typeof(double), (TypeConverter<ushort, double>)delegate(ref ushort v)
				{
					return (int)v;
				});
				s_GlobalConverters.Register(typeof(ushort), typeof(object), (TypeConverter<ushort, object>)delegate(ref ushort v)
				{
					return v;
				});
			}

			private static void RegisterUInt32Converters()
			{
				s_GlobalConverters.Register(typeof(uint), typeof(sbyte), (TypeConverter<uint, sbyte>)delegate(ref uint v)
				{
					return (sbyte)v;
				});
				s_GlobalConverters.Register(typeof(uint), typeof(char), (TypeConverter<uint, char>)delegate(ref uint v)
				{
					return (char)v;
				});
				s_GlobalConverters.Register(typeof(uint), typeof(bool), (TypeConverter<uint, bool>)delegate(ref uint v)
				{
					return v != 0;
				});
				s_GlobalConverters.Register(typeof(uint), typeof(short), (TypeConverter<uint, short>)delegate(ref uint v)
				{
					return (short)v;
				});
				s_GlobalConverters.Register(typeof(uint), typeof(int), (TypeConverter<uint, int>)delegate(ref uint v)
				{
					return (int)v;
				});
				s_GlobalConverters.Register(typeof(uint), typeof(long), (TypeConverter<uint, long>)delegate(ref uint v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(uint), typeof(byte), (TypeConverter<uint, byte>)delegate(ref uint v)
				{
					return (byte)v;
				});
				s_GlobalConverters.Register(typeof(uint), typeof(ushort), (TypeConverter<uint, ushort>)delegate(ref uint v)
				{
					return (ushort)v;
				});
				s_GlobalConverters.Register(typeof(uint), typeof(ulong), (TypeConverter<uint, ulong>)delegate(ref uint v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(uint), typeof(float), (TypeConverter<uint, float>)delegate(ref uint v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(uint), typeof(double), (TypeConverter<uint, double>)delegate(ref uint v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(uint), typeof(object), (TypeConverter<uint, object>)delegate(ref uint v)
				{
					return v;
				});
			}

			private static void RegisterUInt64Converters()
			{
				s_GlobalConverters.Register(typeof(ulong), typeof(sbyte), (TypeConverter<ulong, sbyte>)delegate(ref ulong v)
				{
					return (sbyte)v;
				});
				s_GlobalConverters.Register(typeof(ulong), typeof(char), (TypeConverter<ulong, char>)delegate(ref ulong v)
				{
					return (char)v;
				});
				s_GlobalConverters.Register(typeof(ulong), typeof(bool), (TypeConverter<ulong, bool>)delegate(ref ulong v)
				{
					return v != 0;
				});
				s_GlobalConverters.Register(typeof(ulong), typeof(short), (TypeConverter<ulong, short>)delegate(ref ulong v)
				{
					return (short)v;
				});
				s_GlobalConverters.Register(typeof(ulong), typeof(int), (TypeConverter<ulong, int>)delegate(ref ulong v)
				{
					return (int)v;
				});
				s_GlobalConverters.Register(typeof(ulong), typeof(long), (TypeConverter<ulong, long>)delegate(ref ulong v)
				{
					return (long)v;
				});
				s_GlobalConverters.Register(typeof(ulong), typeof(byte), (TypeConverter<ulong, byte>)delegate(ref ulong v)
				{
					return (byte)v;
				});
				s_GlobalConverters.Register(typeof(ulong), typeof(ushort), (TypeConverter<ulong, ushort>)delegate(ref ulong v)
				{
					return (ushort)v;
				});
				s_GlobalConverters.Register(typeof(ulong), typeof(uint), (TypeConverter<ulong, uint>)delegate(ref ulong v)
				{
					return (uint)v;
				});
				s_GlobalConverters.Register(typeof(ulong), typeof(float), (TypeConverter<ulong, float>)delegate(ref ulong v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(ulong), typeof(double), (TypeConverter<ulong, double>)delegate(ref ulong v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(ulong), typeof(object), (TypeConverter<ulong, object>)delegate(ref ulong v)
				{
					return v;
				});
			}

			private static void RegisterFloat32Converters()
			{
				s_GlobalConverters.Register(typeof(float), typeof(sbyte), (TypeConverter<float, sbyte>)delegate(ref float v)
				{
					return (sbyte)v;
				});
				s_GlobalConverters.Register(typeof(float), typeof(char), (TypeConverter<float, char>)delegate(ref float v)
				{
					return (char)v;
				});
				s_GlobalConverters.Register(typeof(float), typeof(bool), (TypeConverter<float, bool>)delegate(ref float v)
				{
					return Math.Abs(v) > float.Epsilon;
				});
				s_GlobalConverters.Register(typeof(float), typeof(short), (TypeConverter<float, short>)delegate(ref float v)
				{
					return (short)v;
				});
				s_GlobalConverters.Register(typeof(float), typeof(int), (TypeConverter<float, int>)delegate(ref float v)
				{
					return (int)v;
				});
				s_GlobalConverters.Register(typeof(float), typeof(long), (TypeConverter<float, long>)delegate(ref float v)
				{
					return (long)v;
				});
				s_GlobalConverters.Register(typeof(float), typeof(byte), (TypeConverter<float, byte>)delegate(ref float v)
				{
					return (byte)v;
				});
				s_GlobalConverters.Register(typeof(float), typeof(ushort), (TypeConverter<float, ushort>)delegate(ref float v)
				{
					return (ushort)v;
				});
				s_GlobalConverters.Register(typeof(float), typeof(uint), (TypeConverter<float, uint>)delegate(ref float v)
				{
					return (uint)v;
				});
				s_GlobalConverters.Register(typeof(float), typeof(ulong), (TypeConverter<float, ulong>)delegate(ref float v)
				{
					return (ulong)v;
				});
				s_GlobalConverters.Register(typeof(float), typeof(double), (TypeConverter<float, double>)delegate(ref float v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(float), typeof(object), (TypeConverter<float, object>)delegate(ref float v)
				{
					return v;
				});
			}

			private static void RegisterFloat64Converters()
			{
				s_GlobalConverters.Register(typeof(double), typeof(sbyte), (TypeConverter<double, sbyte>)delegate(ref double v)
				{
					return (sbyte)v;
				});
				s_GlobalConverters.Register(typeof(double), typeof(char), (TypeConverter<double, char>)delegate(ref double v)
				{
					return (char)v;
				});
				s_GlobalConverters.Register(typeof(double), typeof(bool), (TypeConverter<double, bool>)delegate(ref double v)
				{
					return Math.Abs(v) > double.Epsilon;
				});
				s_GlobalConverters.Register(typeof(double), typeof(short), (TypeConverter<double, short>)delegate(ref double v)
				{
					return (short)v;
				});
				s_GlobalConverters.Register(typeof(double), typeof(int), (TypeConverter<double, int>)delegate(ref double v)
				{
					return (int)v;
				});
				s_GlobalConverters.Register(typeof(double), typeof(long), (TypeConverter<double, long>)delegate(ref double v)
				{
					return (long)v;
				});
				s_GlobalConverters.Register(typeof(double), typeof(byte), (TypeConverter<double, byte>)delegate(ref double v)
				{
					return (byte)v;
				});
				s_GlobalConverters.Register(typeof(double), typeof(ushort), (TypeConverter<double, ushort>)delegate(ref double v)
				{
					return (ushort)v;
				});
				s_GlobalConverters.Register(typeof(double), typeof(uint), (TypeConverter<double, uint>)delegate(ref double v)
				{
					return (uint)v;
				});
				s_GlobalConverters.Register(typeof(double), typeof(ulong), (TypeConverter<double, ulong>)delegate(ref double v)
				{
					return (ulong)v;
				});
				s_GlobalConverters.Register(typeof(double), typeof(float), (TypeConverter<double, float>)delegate(ref double v)
				{
					return (float)v;
				});
				s_GlobalConverters.Register(typeof(double), typeof(object), (TypeConverter<double, object>)delegate(ref double v)
				{
					return v;
				});
			}

			private static void RegisterBooleanConverters()
			{
				s_GlobalConverters.Register(typeof(bool), typeof(char), (TypeConverter<bool, char>)delegate(ref bool v)
				{
					return v ? '\u0001' : '\0';
				});
				s_GlobalConverters.Register(typeof(bool), typeof(sbyte), (TypeConverter<bool, sbyte>)delegate(ref bool v)
				{
					return (sbyte)(v ? 1 : 0);
				});
				s_GlobalConverters.Register(typeof(bool), typeof(short), (TypeConverter<bool, short>)delegate(ref bool v)
				{
					return (short)(v ? 1 : 0);
				});
				s_GlobalConverters.Register(typeof(bool), typeof(int), (TypeConverter<bool, int>)delegate(ref bool v)
				{
					return v ? 1 : 0;
				});
				s_GlobalConverters.Register(typeof(bool), typeof(long), (TypeConverter<bool, long>)delegate(ref bool v)
				{
					return v ? 1 : 0;
				});
				s_GlobalConverters.Register(typeof(bool), typeof(byte), (TypeConverter<bool, byte>)delegate(ref bool v)
				{
					return (byte)(v ? 1 : 0);
				});
				s_GlobalConverters.Register(typeof(bool), typeof(ushort), (TypeConverter<bool, ushort>)delegate(ref bool v)
				{
					return (ushort)(v ? 1 : 0);
				});
				s_GlobalConverters.Register(typeof(bool), typeof(uint), (TypeConverter<bool, uint>)delegate(ref bool v)
				{
					return v ? 1u : 0u;
				});
				s_GlobalConverters.Register(typeof(bool), typeof(ulong), (TypeConverter<bool, ulong>)delegate(ref bool v)
				{
					return (ulong)(v ? 1 : 0);
				});
				s_GlobalConverters.Register(typeof(bool), typeof(float), (TypeConverter<bool, float>)delegate(ref bool v)
				{
					return v ? 1f : 0f;
				});
				s_GlobalConverters.Register(typeof(bool), typeof(double), (TypeConverter<bool, double>)delegate(ref bool v)
				{
					return v ? 1.0 : 0.0;
				});
				s_GlobalConverters.Register(typeof(bool), typeof(object), (TypeConverter<bool, object>)delegate(ref bool v)
				{
					return v;
				});
			}

			private static void RegisterCharConverters()
			{
				s_GlobalConverters.Register(typeof(string), typeof(char), (TypeConverter<string, char>)delegate(ref string v)
				{
					if (v.Length != 1)
					{
						throw new Exception("Not a valid char");
					}
					return v[0];
				});
				s_GlobalConverters.Register(typeof(char), typeof(bool), (TypeConverter<char, bool>)delegate(ref char v)
				{
					return v != '\0';
				});
				s_GlobalConverters.Register(typeof(char), typeof(sbyte), (TypeConverter<char, sbyte>)delegate(ref char v)
				{
					return (sbyte)v;
				});
				s_GlobalConverters.Register(typeof(char), typeof(short), (TypeConverter<char, short>)delegate(ref char v)
				{
					return (short)v;
				});
				s_GlobalConverters.Register(typeof(char), typeof(int), (TypeConverter<char, int>)delegate(ref char v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(char), typeof(long), (TypeConverter<char, long>)delegate(ref char v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(char), typeof(byte), (TypeConverter<char, byte>)delegate(ref char v)
				{
					return (byte)v;
				});
				s_GlobalConverters.Register(typeof(char), typeof(ushort), (TypeConverter<char, ushort>)delegate(ref char v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(char), typeof(uint), (TypeConverter<char, uint>)delegate(ref char v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(char), typeof(ulong), (TypeConverter<char, ulong>)delegate(ref char v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(char), typeof(float), (TypeConverter<char, float>)delegate(ref char v)
				{
					return (int)v;
				});
				s_GlobalConverters.Register(typeof(char), typeof(double), (TypeConverter<char, double>)delegate(ref char v)
				{
					return (int)v;
				});
				s_GlobalConverters.Register(typeof(char), typeof(object), (TypeConverter<char, object>)delegate(ref char v)
				{
					return v;
				});
				s_GlobalConverters.Register(typeof(char), typeof(string), (TypeConverter<char, string>)delegate(ref char v)
				{
					return v.ToString();
				});
			}

			private static void RegisterStringConverters()
			{
				s_GlobalConverters.Register(typeof(string), typeof(char), (TypeConverter<string, char>)delegate(ref string v)
				{
					return (!string.IsNullOrEmpty(v)) ? v[0] : '\0';
				});
				s_GlobalConverters.Register(typeof(char), typeof(string), (TypeConverter<char, string>)delegate(ref char v)
				{
					return v.ToString();
				});
				s_GlobalConverters.Register(typeof(string), typeof(bool), (TypeConverter<string, bool>)delegate(ref string v)
				{
					bool result;
					double result2;
					return bool.TryParse(v, out result) ? result : (double.TryParse(v, out result2) && Convert<double, bool>(ref result2));
				});
				s_GlobalConverters.Register(typeof(bool), typeof(string), (TypeConverter<bool, string>)delegate(ref bool v)
				{
					return v.ToString();
				});
				s_GlobalConverters.Register(typeof(string), typeof(sbyte), (TypeConverter<string, sbyte>)delegate(ref string v)
				{
					sbyte result;
					double result2;
					return (sbyte)(sbyte.TryParse(v, out result) ? result : (double.TryParse(v, out result2) ? Convert<double, sbyte>(ref result2) : 0));
				});
				s_GlobalConverters.Register(typeof(sbyte), typeof(string), (TypeConverter<sbyte, string>)delegate(ref sbyte v)
				{
					return v.ToString();
				});
				s_GlobalConverters.Register(typeof(string), typeof(short), (TypeConverter<string, short>)delegate(ref string v)
				{
					short result;
					double result2;
					return (short)(short.TryParse(v, out result) ? result : (double.TryParse(v, out result2) ? Convert<double, short>(ref result2) : 0));
				});
				s_GlobalConverters.Register(typeof(short), typeof(string), (TypeConverter<short, string>)delegate(ref short v)
				{
					return v.ToString();
				});
				s_GlobalConverters.Register(typeof(string), typeof(int), (TypeConverter<string, int>)delegate(ref string v)
				{
					int result;
					double result2;
					return int.TryParse(v, out result) ? result : (double.TryParse(v, out result2) ? Convert<double, int>(ref result2) : 0);
				});
				s_GlobalConverters.Register(typeof(int), typeof(string), (TypeConverter<int, string>)delegate(ref int v)
				{
					return v.ToString();
				});
				s_GlobalConverters.Register(typeof(string), typeof(long), (TypeConverter<string, long>)delegate(ref string v)
				{
					long result;
					double result2;
					return long.TryParse(v, out result) ? result : (double.TryParse(v, out result2) ? Convert<double, long>(ref result2) : 0);
				});
				s_GlobalConverters.Register(typeof(long), typeof(string), (TypeConverter<long, string>)delegate(ref long v)
				{
					return v.ToString();
				});
				s_GlobalConverters.Register(typeof(string), typeof(byte), (TypeConverter<string, byte>)delegate(ref string v)
				{
					byte result;
					double result2;
					return (byte)(byte.TryParse(v, out result) ? result : (double.TryParse(v, out result2) ? Convert<double, byte>(ref result2) : 0));
				});
				s_GlobalConverters.Register(typeof(byte), typeof(string), (TypeConverter<byte, string>)delegate(ref byte v)
				{
					return v.ToString();
				});
				s_GlobalConverters.Register(typeof(string), typeof(ushort), (TypeConverter<string, ushort>)delegate(ref string v)
				{
					ushort result;
					double result2;
					return (ushort)(ushort.TryParse(v, out result) ? result : (double.TryParse(v, out result2) ? Convert<double, ushort>(ref result2) : 0));
				});
				s_GlobalConverters.Register(typeof(ushort), typeof(string), (TypeConverter<ushort, string>)delegate(ref ushort v)
				{
					return v.ToString();
				});
				s_GlobalConverters.Register(typeof(string), typeof(uint), (TypeConverter<string, uint>)delegate(ref string v)
				{
					uint result;
					double result2;
					return uint.TryParse(v, out result) ? result : (double.TryParse(v, out result2) ? Convert<double, uint>(ref result2) : 0u);
				});
				s_GlobalConverters.Register(typeof(uint), typeof(string), (TypeConverter<uint, string>)delegate(ref uint v)
				{
					return v.ToString();
				});
				s_GlobalConverters.Register(typeof(string), typeof(ulong), (TypeConverter<string, ulong>)delegate(ref string v)
				{
					ulong result;
					double result2;
					return ulong.TryParse(v, out result) ? result : (double.TryParse(v, out result2) ? Convert<double, ulong>(ref result2) : 0);
				});
				s_GlobalConverters.Register(typeof(ulong), typeof(string), (TypeConverter<ulong, string>)delegate(ref ulong v)
				{
					return v.ToString();
				});
				s_GlobalConverters.Register(typeof(string), typeof(float), (TypeConverter<string, float>)delegate(ref string v)
				{
					float result;
					double result2;
					return float.TryParse(v, out result) ? result : (double.TryParse(v, out result2) ? Convert<double, float>(ref result2) : 0f);
				});
				s_GlobalConverters.Register(typeof(float), typeof(string), (TypeConverter<float, string>)delegate(ref float v)
				{
					return v.ToString(CultureInfo.InvariantCulture);
				});
				s_GlobalConverters.Register(typeof(string), typeof(double), (TypeConverter<string, double>)delegate(ref string v)
				{
					double.TryParse(v, out var result);
					return result;
				});
				s_GlobalConverters.Register(typeof(double), typeof(string), (TypeConverter<double, string>)delegate(ref double v)
				{
					return v.ToString(CultureInfo.InvariantCulture);
				});
			}

			private static void RegisterObjectConverters()
			{
				s_GlobalConverters.Register(typeof(object), typeof(char), (TypeConverter<object, char>)delegate(ref object v)
				{
					return (v is char c) ? c : '\0';
				});
				s_GlobalConverters.Register(typeof(object), typeof(bool), (TypeConverter<object, bool>)delegate(ref object v)
				{
					return v is bool flag && flag;
				});
				s_GlobalConverters.Register(typeof(object), typeof(sbyte), (TypeConverter<object, sbyte>)delegate(ref object v)
				{
					return (sbyte)((v is sbyte b) ? b : 0);
				});
				s_GlobalConverters.Register(typeof(object), typeof(short), (TypeConverter<object, short>)delegate(ref object v)
				{
					return (short)((v is short num) ? num : 0);
				});
				s_GlobalConverters.Register(typeof(object), typeof(int), (TypeConverter<object, int>)delegate(ref object v)
				{
					return (v is int num) ? num : 0;
				});
				s_GlobalConverters.Register(typeof(object), typeof(long), (TypeConverter<object, long>)delegate(ref object v)
				{
					return (v is long num) ? num : 0;
				});
				s_GlobalConverters.Register(typeof(object), typeof(byte), (TypeConverter<object, byte>)delegate(ref object v)
				{
					return (byte)((v is byte b) ? b : 0);
				});
				s_GlobalConverters.Register(typeof(object), typeof(ushort), (TypeConverter<object, ushort>)delegate(ref object v)
				{
					return (ushort)((v is ushort num) ? num : 0);
				});
				s_GlobalConverters.Register(typeof(object), typeof(uint), (TypeConverter<object, uint>)delegate(ref object v)
				{
					return (v is uint num) ? num : 0u;
				});
				s_GlobalConverters.Register(typeof(object), typeof(ulong), (TypeConverter<object, ulong>)delegate(ref object v)
				{
					return (v is ulong num) ? num : 0;
				});
				s_GlobalConverters.Register(typeof(object), typeof(float), (TypeConverter<object, float>)delegate(ref object v)
				{
					return (v is float num) ? num : 0f;
				});
				s_GlobalConverters.Register(typeof(object), typeof(double), (TypeConverter<object, double>)delegate(ref object v)
				{
					return (v is double num) ? num : 0.0;
				});
			}
		}

		private static readonly ConversionRegistry s_GlobalConverters;

		static TypeConversion()
		{
			s_GlobalConverters = ConversionRegistry.Create();
			PrimitiveConverters.Register();
		}

		public static void Register<TSource, TDestination>(TypeConverter<TSource, TDestination> converter)
		{
			s_GlobalConverters.Register(typeof(TSource), typeof(TDestination), converter);
		}

		public static TDestination Convert<TSource, TDestination>(ref TSource value)
		{
			if (!TryConvert<TSource, TDestination>(ref value, out var destination))
			{
				throw new InvalidOperationException($"TypeConversion no converter has been registered for SrcType=[{typeof(TSource)}] to DstType=[{typeof(TDestination)}]");
			}
			return destination;
		}

		public static bool TryConvert<TSource, TDestination>(ref TSource source, out TDestination destination)
		{
			if (s_GlobalConverters.TryGetConverter(typeof(TSource), typeof(TDestination), out var converter))
			{
				destination = ((TypeConverter<TSource, TDestination>)converter)(ref source);
				return true;
			}
			if (typeof(TSource).IsValueType && typeof(TSource) == typeof(TDestination))
			{
				destination = UnsafeUtility.As<TSource, TDestination>(ref source);
				return true;
			}
			if (TypeTraits<TDestination>.IsNullable)
			{
				if (TypeTraits<TSource>.IsNullable && Nullable.GetUnderlyingType(typeof(TDestination)) != Nullable.GetUnderlyingType(typeof(TSource)))
				{
					destination = default(TDestination);
					return false;
				}
				Type underlyingType = Nullable.GetUnderlyingType(typeof(TDestination));
				if (underlyingType.IsEnum)
				{
					Type underlyingType2 = Enum.GetUnderlyingType(underlyingType);
					object value = System.Convert.ChangeType(source, underlyingType2);
					destination = (TDestination)Enum.ToObject(underlyingType, value);
					return true;
				}
				if (source == null)
				{
					destination = default(TDestination);
					return true;
				}
				destination = (TDestination)System.Convert.ChangeType(source, underlyingType);
				return true;
			}
			if (TypeTraits<TSource>.IsNullable && typeof(TDestination) == Nullable.GetUnderlyingType(typeof(TSource)))
			{
				if (source == null)
				{
					destination = default(TDestination);
					return false;
				}
				destination = (TDestination)(object)source;
				return true;
			}
			if (TypeTraits<TDestination>.IsUnityObject && TryConvertToUnityEngineObject<TSource, TDestination>(source, out destination))
			{
				return true;
			}
			if (TypeTraits<TDestination>.IsEnum)
			{
				if (typeof(TSource) == typeof(string))
				{
					try
					{
						destination = (TDestination)Enum.Parse(typeof(TDestination), (string)(object)source);
					}
					catch (ArgumentException)
					{
						destination = default(TDestination);
						return false;
					}
					return true;
				}
				if (IsNumericType(typeof(TSource)))
				{
					destination = UnsafeUtility.As<TSource, TDestination>(ref source);
					return true;
				}
			}
			TSource val = source;
			if (val is TDestination val2)
			{
				destination = val2;
				return true;
			}
			if (typeof(TDestination).IsAssignableFrom(typeof(TSource)))
			{
				destination = (TDestination)(object)source;
				return true;
			}
			destination = default(TDestination);
			return false;
		}

		private static bool TryConvertToUnityEngineObject<TSource, TDestination>(TSource source, out TDestination destination)
		{
			if (!typeof(UnityEngine.Object).IsAssignableFrom(typeof(TDestination)))
			{
				destination = default(TDestination);
				return false;
			}
			if (typeof(UnityEngine.Object).IsAssignableFrom(typeof(TSource)) || source is UnityEngine.Object)
			{
				if (source == null)
				{
					destination = default(TDestination);
					return true;
				}
				if (typeof(TDestination) == typeof(UnityEngine.Object))
				{
					destination = (TDestination)(object)source;
					return true;
				}
			}
			if (s_GlobalConverters.TryGetConverter(typeof(TSource), typeof(UnityEngine.Object), out var converter))
			{
				UnityEngine.Object obj = ((TypeConverter<TSource, UnityEngine.Object>)converter)(ref source);
				destination = (TDestination)(object)obj;
				return obj;
			}
			destination = default(TDestination);
			return false;
		}

		private static bool IsNumericType(Type t)
		{
			TypeCode typeCode = Type.GetTypeCode(t);
			TypeCode typeCode2 = typeCode;
			if ((uint)(typeCode2 - 5) <= 10u)
			{
				return true;
			}
			return false;
		}
	}
	public static class TypeTraits
	{
		public static bool IsContainer(Type type)
		{
			if (null == type)
			{
				throw new ArgumentNullException("type");
			}
			return !type.IsPrimitive && !type.IsPointer && !type.IsEnum && !(type == typeof(string));
		}
	}
	public static class TypeTraits<T>
	{
		public static bool IsValueType { get; }

		public static bool IsPrimitive { get; }

		public static bool IsInterface { get; }

		public static bool IsAbstract { get; }

		public static bool IsArray { get; }

		public static bool IsMultidimensionalArray { get; }

		public static bool IsEnum { get; }

		public static bool IsEnumFlags { get; }

		public static bool IsNullable { get; }

		public static bool IsObject { get; }

		public static bool IsString { get; }

		public static bool IsContainer { get; }

		public static bool CanBeNull { get; }

		public static bool IsPrimitiveOrString { get; }

		public static bool IsAbstractOrInterface { get; }

		public static bool IsUnityObject { get; }

		public static bool IsLazyLoadReference { get; }

		static TypeTraits()
		{
			Type typeFromHandle = typeof(T);
			IsValueType = typeFromHandle.IsValueType;
			IsPrimitive = typeFromHandle.IsPrimitive;
			IsInterface = typeFromHandle.IsInterface;
			IsAbstract = typeFromHandle.IsAbstract;
			IsArray = typeFromHandle.IsArray;
			IsEnum = typeFromHandle.IsEnum;
			IsEnumFlags = IsEnum && typeFromHandle.GetCustomAttribute<FlagsAttribute>() != null;
			IsNullable = Nullable.GetUnderlyingType(typeof(T)) != null;
			IsMultidimensionalArray = IsArray && typeof(T).GetArrayRank() != 1;
			IsObject = typeFromHandle == typeof(object);
			IsString = typeFromHandle == typeof(string);
			IsContainer = TypeTraits.IsContainer(typeFromHandle);
			CanBeNull = !IsValueType;
			IsPrimitiveOrString = IsPrimitive || IsString;
			IsAbstractOrInterface = IsAbstract || IsInterface;
			CanBeNull |= IsNullable;
			IsLazyLoadReference = typeFromHandle.IsGenericType && typeFromHandle.GetGenericTypeDefinition() == typeof(LazyLoadReference<>);
			IsUnityObject = typeof(UnityEngine.Object).IsAssignableFrom(typeFromHandle);
		}
	}
	public enum InstantiationKind
	{
		Activator,
		PropertyBagOverride,
		NotInstantiatable
	}
	internal interface IConstructor
	{
		InstantiationKind InstantiationKind { get; }
	}
	internal interface IConstructor<out T> : IConstructor
	{
		T Instantiate();
	}
	internal interface IConstructorWithCount<out T> : IConstructor
	{
		T InstantiateWithCount(int count);
	}
	public static class TypeUtility
	{
		private interface ITypeConstructor
		{
			bool CanBeInstantiated { get; }

			object Instantiate();
		}

		private interface ITypeConstructor<T> : ITypeConstructor
		{
			new T Instantiate();

			void SetExplicitConstructor(Func<T> constructor);
		}

		private class TypeConstructor<T> : ITypeConstructor<T>, ITypeConstructor
		{
			private Func<T> m_ExplicitConstructor;

			private Func<T> m_ImplicitConstructor;

			private IConstructor<T> m_OverrideConstructor;

			bool ITypeConstructor.CanBeInstantiated
			{
				get
				{
					if (m_ExplicitConstructor != null)
					{
						return true;
					}
					if (m_OverrideConstructor != null)
					{
						if (m_OverrideConstructor.InstantiationKind == InstantiationKind.NotInstantiatable)
						{
							return false;
						}
						if (m_OverrideConstructor.InstantiationKind == InstantiationKind.PropertyBagOverride)
						{
							return true;
						}
					}
					return m_ImplicitConstructor != null;
				}
			}

			public TypeConstructor()
			{
				m_OverrideConstructor = PropertyBagStore.GetPropertyBag<T>() as IConstructor<T>;
				SetImplicitConstructor();
			}

			private void SetImplicitConstructor()
			{
				Type typeFromHandle = typeof(T);
				if (typeFromHandle.IsValueType)
				{
					m_ImplicitConstructor = CreateValueTypeInstance;
				}
				else if (!typeFromHandle.IsAbstract)
				{
					if (typeof(ScriptableObject).IsAssignableFrom(typeFromHandle))
					{
						m_ImplicitConstructor = CreateScriptableObjectInstance;
					}
					else if (null != typeFromHandle.GetConstructor(Array.Empty<Type>()))
					{
						m_ImplicitConstructor = CreateClassInstance;
					}
				}
			}

			private static T CreateValueTypeInstance()
			{
				return default(T);
			}

			private static T CreateScriptableObjectInstance()
			{
				return (T)(object)ScriptableObject.CreateInstance(typeof(T));
			}

			private static T CreateClassInstance()
			{
				return Activator.CreateInstance<T>();
			}

			public void SetExplicitConstructor(Func<T> constructor)
			{
				m_ExplicitConstructor = constructor;
			}

			T ITypeConstructor<T>.Instantiate()
			{
				if (m_ExplicitConstructor != null)
				{
					return m_ExplicitConstructor();
				}
				if (m_OverrideConstructor != null)
				{
					if (m_OverrideConstructor.InstantiationKind == InstantiationKind.NotInstantiatable)
					{
						throw new InvalidOperationException("The type '" + typeof(T).Name + "' is not constructable.");
					}
					if (m_OverrideConstructor.InstantiationKind == InstantiationKind.PropertyBagOverride)
					{
						return m_OverrideConstructor.Instantiate();
					}
				}
				if (m_ImplicitConstructor != null)
				{
					return m_ImplicitConstructor();
				}
				throw new InvalidOperationException("The type '" + typeof(T).Name + "' is not constructable.");
			}

			object ITypeConstructor.Instantiate()
			{
				return ((ITypeConstructor<T>)this).Instantiate();
			}
		}

		private class NonConstructable : ITypeConstructor
		{
			bool ITypeConstructor.CanBeInstantiated => false;

			public object Instantiate()
			{
				throw new InvalidOperationException("The type is not instantiatable.");
			}
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		private struct Cache<T>
		{
			public static ITypeConstructor<T> TypeConstructor;
		}

		private class TypeConstructorVisitor : ITypeVisitor
		{
			public ITypeConstructor TypeConstructor;

			public void Visit<TContainer>()
			{
				TypeConstructor = CreateTypeConstructor<TContainer>();
			}
		}

		private static readonly ConcurrentDictionary<Type, ITypeConstructor> s_TypeConstructors;

		private static readonly MethodInfo s_CreateTypeConstructor;

		private static readonly ConcurrentDictionary<Type, string> s_CachedResolvedName;

		private static readonly ObjectPool<StringBuilder> s_Builders;

		private static readonly object syncedPoolObject;

		static TypeUtility()
		{
			s_TypeConstructors = new ConcurrentDictionary<Type, ITypeConstructor>();
			syncedPoolObject = new object();
			s_CachedResolvedName = new ConcurrentDictionary<Type, string>();
			s_Builders = new ObjectPool<StringBuilder>(() => new StringBuilder(), null, delegate(StringBuilder sb)
			{
				sb.Clear();
			});
			SetExplicitInstantiationMethod(() => string.Empty);
			MethodInfo[] methods = typeof(TypeUtility).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
			foreach (MethodInfo methodInfo in methods)
			{
				if (!(methodInfo.Name != "CreateTypeConstructor") && methodInfo.IsGenericMethod)
				{
					s_CreateTypeConstructor = methodInfo;
					break;
				}
			}
			if (null == s_CreateTypeConstructor)
			{
				throw new InvalidProgramException();
			}
		}

		public static string GetTypeDisplayName(Type type)
		{
			if (s_CachedResolvedName.TryGetValue(type, out var value))
			{
				return value;
			}
			int argIndex = 0;
			value = GetTypeDisplayName(type, type.GetGenericArguments(), ref argIndex);
			s_CachedResolvedName[type] = value;
			return value;
		}

		private static string GetTypeDisplayName(Type type, IReadOnlyList<Type> args, ref int argIndex)
		{
			if (type == typeof(int))
			{
				return "int";
			}
			if (type == typeof(uint))
			{
				return "uint";
			}
			if (type == typeof(short))
			{
				return "short";
			}
			if (type == typeof(ushort))
			{
				return "ushort";
			}
			if (type == typeof(byte))
			{
				return "byte";
			}
			if (type == typeof(char))
			{
				return "char";
			}
			if (type == typeof(bool))
			{
				return "bool";
			}
			if (type == typeof(long))
			{
				return "long";
			}
			if (type == typeof(ulong))
			{
				return "ulong";
			}
			if (type == typeof(float))
			{
				return "float";
			}
			if (type == typeof(double))
			{
				return "double";
			}
			if (type == typeof(string))
			{
				return "string";
			}
			string text = type.Name;
			if (type.IsGenericParameter)
			{
				return text;
			}
			if (type.IsNested)
			{
				text = GetTypeDisplayName(type.DeclaringType, args, ref argIndex) + "." + text;
			}
			if (!type.IsGenericType)
			{
				return text;
			}
			int num = text.IndexOf('`');
			int num2 = type.GetGenericArguments().Length;
			if (num > -1)
			{
				num2 = int.Parse(text.Substring(num + 1));
				text = text.Remove(num);
			}
			StringBuilder stringBuilder = null;
			lock (syncedPoolObject)
			{
				stringBuilder = s_Builders.Get();
			}
			try
			{
				int num3 = 0;
				while (num3 < num2 && argIndex < args.Count)
				{
					if (num3 != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(GetTypeDisplayName(args[argIndex]));
					num3++;
					argIndex++;
				}
				if (stringBuilder.Length > 0)
				{
					text = $"{text}<{stringBuilder}>";
				}
			}
			finally
			{
				lock (syncedPoolObject)
				{
					s_Builders.Release(stringBuilder);
				}
			}
			return text;
		}

		public static Type GetRootType(this Type type)
		{
			if (type.IsInterface)
			{
				return null;
			}
			Type type2 = (type.IsValueType ? typeof(ValueType) : typeof(object));
			while (type2 != type.BaseType)
			{
				type = type.BaseType;
			}
			return type;
		}

		[Preserve]
		private static ITypeConstructor CreateTypeConstructor(Type type)
		{
			IPropertyBag propertyBag = PropertyBagStore.GetPropertyBag(type);
			if (propertyBag != null)
			{
				TypeConstructorVisitor typeConstructorVisitor = new TypeConstructorVisitor();
				propertyBag.Accept(typeConstructorVisitor);
				return typeConstructorVisitor.TypeConstructor;
			}
			if (type.ContainsGenericParameters)
			{
				NonConstructable nonConstructable = new NonConstructable();
				s_TypeConstructors[type] = nonConstructable;
				return nonConstructable;
			}
			return s_CreateTypeConstructor.MakeGenericMethod(type).Invoke(null, null) as ITypeConstructor;
		}

		private static ITypeConstructor<T> CreateTypeConstructor<T>()
		{
			TypeConstructor<T> typeConstructor = (TypeConstructor<T>)(Cache<T>.TypeConstructor = new TypeConstructor<T>());
			s_TypeConstructors[typeof(T)] = typeConstructor;
			return typeConstructor;
		}

		private static ITypeConstructor GetTypeConstructor(Type type)
		{
			ITypeConstructor value;
			return s_TypeConstructors.TryGetValue(type, out value) ? value : CreateTypeConstructor(type);
		}

		private static ITypeConstructor<T> GetTypeConstructor<T>()
		{
			return (Cache<T>.TypeConstructor != null) ? Cache<T>.TypeConstructor : CreateTypeConstructor<T>();
		}

		public static bool CanBeInstantiated(Type type)
		{
			return GetTypeConstructor(type).CanBeInstantiated;
		}

		public static bool CanBeInstantiated<T>()
		{
			return GetTypeConstructor<T>().CanBeInstantiated;
		}

		public static void SetExplicitInstantiationMethod<T>(Func<T> constructor)
		{
			GetTypeConstructor<T>().SetExplicitConstructor(constructor);
		}

		public static T Instantiate<T>()
		{
			ITypeConstructor<T> typeConstructor = GetTypeConstructor<T>();
			CheckCanBeInstantiated(typeConstructor);
			return typeConstructor.Instantiate();
		}

		public static bool TryInstantiate<T>(out T instance)
		{
			ITypeConstructor<T> typeConstructor = GetTypeConstructor<T>();
			if (typeConstructor.CanBeInstantiated)
			{
				instance = typeConstructor.Instantiate();
				return true;
			}
			instance = default(T);
			return false;
		}

		public static T Instantiate<T>(Type derivedType)
		{
			ITypeConstructor typeConstructor = GetTypeConstructor(derivedType);
			CheckIsAssignableFrom(typeof(T), derivedType);
			CheckCanBeInstantiated(typeConstructor, derivedType);
			return (T)typeConstructor.Instantiate();
		}

		public static bool TryInstantiate<T>(Type derivedType, out T value)
		{
			if (!typeof(T).IsAssignableFrom(derivedType))
			{
				value = default(T);
				value = default(T);
				return false;
			}
			ITypeConstructor typeConstructor = GetTypeConstructor(derivedType);
			if (!typeConstructor.CanBeInstantiated)
			{
				value = default(T);
				return false;
			}
			value = (T)typeConstructor.Instantiate();
			return true;
		}

		public static TArray InstantiateArray<TArray>(int count = 0)
		{
			if (count < 0)
			{
				throw new ArgumentException(string.Format("{0}: Cannot construct an array with {1}={2}", "TypeUtility", "count", count));
			}
			IPropertyBag<TArray> propertyBag = PropertyBagStore.GetPropertyBag<TArray>();
			if (propertyBag is IConstructorWithCount<TArray> constructorWithCount)
			{
				return constructorWithCount.InstantiateWithCount(count);
			}
			Type typeFromHandle = typeof(TArray);
			if (!typeFromHandle.IsArray)
			{
				throw new ArgumentException("TypeUtility: Cannot construct an array, since " + typeof(TArray).Name + " is not an array type.");
			}
			Type elementType = typeFromHandle.GetElementType();
			if (null == elementType)
			{
				throw new ArgumentException("TypeUtility: Cannot construct an array, since " + typeof(TArray).Name + ".GetElementType() returned null.");
			}
			return (TArray)(object)Array.CreateInstance(elementType, count);
		}

		public static bool TryInstantiateArray<TArray>(int count, out TArray instance)
		{
			if (count < 0)
			{
				instance = default(TArray);
				return false;
			}
			IPropertyBag<TArray> propertyBag = PropertyBagStore.GetPropertyBag<TArray>();
			if (propertyBag is IConstructorWithCount<TArray> constructorWithCount)
			{
				try
				{
					instance = constructorWithCount.InstantiateWithCount(count);
					return true;
				}
				catch
				{
				}
			}
			Type typeFromHandle = typeof(TArray);
			if (!typeFromHandle.IsArray)
			{
				instance = default(TArray);
				return false;
			}
			Type elementType = typeFromHandle.GetElementType();
			if (null == elementType)
			{
				instance = default(TArray);
				return false;
			}
			instance = (TArray)(object)Array.CreateInstance(elementType, count);
			return true;
		}

		public static TArray InstantiateArray<TArray>(Type derivedType, int count = 0)
		{
			if (count < 0)
			{
				throw new ArgumentException(string.Format("{0}: Cannot instantiate an array with {1}={2}", "TypeUtility", "count", count));
			}
			IPropertyBag propertyBag = PropertyBagStore.GetPropertyBag(derivedType);
			if (propertyBag is IConstructorWithCount<TArray> constructorWithCount)
			{
				return constructorWithCount.InstantiateWithCount(count);
			}
			Type typeFromHandle = typeof(TArray);
			if (!typeFromHandle.IsArray)
			{
				throw new ArgumentException("TypeUtility: Cannot instantiate an array, since " + typeof(TArray).Name + " is not an array type.");
			}
			Type elementType = typeFromHandle.GetElementType();
			if (null == elementType)
			{
				throw new ArgumentException("TypeUtility: Cannot instantiate an array, since " + typeof(TArray).Name + ".GetElementType() returned null.");
			}
			return (TArray)(object)Array.CreateInstance(elementType, count);
		}

		private static void CheckIsAssignableFrom(Type type, Type derivedType)
		{
			if (!type.IsAssignableFrom(derivedType))
			{
				throw new ArgumentException("Could not create instance of type `" + derivedType.Name + "` and convert to `" + type.Name + "`: The given type is not assignable to target type.");
			}
		}

		private static void CheckCanBeInstantiated<T>(ITypeConstructor<T> constructor)
		{
			if (!constructor.CanBeInstantiated)
			{
				throw new InvalidOperationException("Type `" + typeof(T).Name + "` could not be instantiated. A parameter-less constructor or an explicit construction method is required.");
			}
		}

		private static void CheckCanBeInstantiated(ITypeConstructor constructor, Type type)
		{
			if (!constructor.CanBeInstantiated)
			{
				throw new InvalidOperationException("Type `" + type.Name + "` could not be instantiated. A parameter-less constructor or an explicit construction method is required.");
			}
		}
	}
}
namespace Unity.Properties.Internal
{
	internal interface IAttributes
	{
		List<Attribute> Attributes { get; set; }

		void AddAttribute(Attribute attribute);

		void AddAttributes(IEnumerable<Attribute> attributes);

		AttributesScope CreateAttributesScope(IAttributes attributes);
	}
	internal static class DefaultPropertyBagInitializer
	{
		internal static void Initialize()
		{
			PropertyBag.Register(new ColorPropertyBag());
			PropertyBag.Register(new Vector2PropertyBag());
			PropertyBag.Register(new Vector3PropertyBag());
			PropertyBag.Register(new Vector4PropertyBag());
			PropertyBag.Register(new Vector2IntPropertyBag());
			PropertyBag.Register(new Vector3IntPropertyBag());
			PropertyBag.Register(new RectPropertyBag());
			PropertyBag.Register(new RectIntPropertyBag());
			PropertyBag.Register(new BoundsPropertyBag());
			PropertyBag.Register(new BoundsIntPropertyBag());
			PropertyBag.Register(new SystemVersionPropertyBag());
		}
	}
	internal class ColorPropertyBag : ContainerPropertyBag<Color>
	{
		private class RProperty : Property<Color, float>
		{
			public override string Name => "r";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Color container)
			{
				return container.r;
			}

			public override void SetValue(ref Color container, float value)
			{
				container.r = value;
			}
		}

		private class GProperty : Property<Color, float>
		{
			public override string Name => "g";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Color container)
			{
				return container.g;
			}

			public override void SetValue(ref Color container, float value)
			{
				container.g = value;
			}
		}

		private class BProperty : Property<Color, float>
		{
			public override string Name => "b";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Color container)
			{
				return container.b;
			}

			public override void SetValue(ref Color container, float value)
			{
				container.b = value;
			}
		}

		private class AProperty : Property<Color, float>
		{
			public override string Name => "a";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Color container)
			{
				return container.a;
			}

			public override void SetValue(ref Color container, float value)
			{
				container.a = value;
			}
		}

		public ColorPropertyBag()
		{
			AddProperty(new RProperty());
			AddProperty(new GProperty());
			AddProperty(new BProperty());
			AddProperty(new AProperty());
		}
	}
	internal class Vector2PropertyBag : ContainerPropertyBag<Vector2>
	{
		private class XProperty : Property<Vector2, float>
		{
			public override string Name => "x";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Vector2 container)
			{
				return container.x;
			}

			public override void SetValue(ref Vector2 container, float value)
			{
				container.x = value;
			}
		}

		private class YProperty : Property<Vector2, float>
		{
			public override string Name => "y";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Vector2 container)
			{
				return container.y;
			}

			public override void SetValue(ref Vector2 container, float value)
			{
				container.y = value;
			}
		}

		public Vector2PropertyBag()
		{
			AddProperty(new XProperty());
			AddProperty(new YProperty());
		}
	}
	internal class Vector3PropertyBag : ContainerPropertyBag<Vector3>
	{
		private class XProperty : Property<Vector3, float>
		{
			public override string Name => "x";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Vector3 container)
			{
				return container.x;
			}

			public override void SetValue(ref Vector3 container, float value)
			{
				container.x = value;
			}
		}

		private class YProperty : Property<Vector3, float>
		{
			public override string Name => "y";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Vector3 container)
			{
				return container.y;
			}

			public override void SetValue(ref Vector3 container, float value)
			{
				container.y = value;
			}
		}

		private class ZProperty : Property<Vector3, float>
		{
			public override string Name => "z";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Vector3 container)
			{
				return container.z;
			}

			public override void SetValue(ref Vector3 container, float value)
			{
				container.z = value;
			}
		}

		public Vector3PropertyBag()
		{
			AddProperty(new XProperty());
			AddProperty(new YProperty());
			AddProperty(new ZProperty());
		}
	}
	internal class Vector4PropertyBag : ContainerPropertyBag<Vector4>
	{
		private class XProperty : Property<Vector4, float>
		{
			public override string Name => "x";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Vector4 container)
			{
				return container.x;
			}

			public override void SetValue(ref Vector4 container, float value)
			{
				container.x = value;
			}
		}

		private class YProperty : Property<Vector4, float>
		{
			public override string Name => "y";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Vector4 container)
			{
				return container.y;
			}

			public override void SetValue(ref Vector4 container, float value)
			{
				container.y = value;
			}
		}

		private class ZProperty : Property<Vector4, float>
		{
			public override string Name => "z";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Vector4 container)
			{
				return container.z;
			}

			public override void SetValue(ref Vector4 container, float value)
			{
				container.z = value;
			}
		}

		private class WProperty : Property<Vector4, float>
		{
			public override string Name => "w";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Vector4 container)
			{
				return container.w;
			}

			public override void SetValue(ref Vector4 container, float value)
			{
				container.w = value;
			}
		}

		public Vector4PropertyBag()
		{
			AddProperty(new XProperty());
			AddProperty(new YProperty());
			AddProperty(new ZProperty());
			AddProperty(new WProperty());
		}
	}
	internal class Vector2IntPropertyBag : ContainerPropertyBag<Vector2Int>
	{
		private class XProperty : Property<Vector2Int, int>
		{
			public override string Name => "x";

			public override bool IsReadOnly => false;

			public override int GetValue(ref Vector2Int container)
			{
				return container.x;
			}

			public override void SetValue(ref Vector2Int container, int value)
			{
				container.x = value;
			}
		}

		private class YProperty : Property<Vector2Int, int>
		{
			public override string Name => "y";

			public override bool IsReadOnly => false;

			public override int GetValue(ref Vector2Int container)
			{
				return container.y;
			}

			public override void SetValue(ref Vector2Int container, int value)
			{
				container.y = value;
			}
		}

		public Vector2IntPropertyBag()
		{
			AddProperty(new XProperty());
			AddProperty(new YProperty());
		}
	}
	internal class Vector3IntPropertyBag : ContainerPropertyBag<Vector3Int>
	{
		private class XProperty : Property<Vector3Int, int>
		{
			public override string Name => "x";

			public override bool IsReadOnly => false;

			public override int GetValue(ref Vector3Int container)
			{
				return container.x;
			}

			public override void SetValue(ref Vector3Int container, int value)
			{
				container.x = value;
			}
		}

		private class YProperty : Property<Vector3Int, int>
		{
			public override string Name => "y";

			public override bool IsReadOnly => false;

			public override int GetValue(ref Vector3Int container)
			{
				return container.y;
			}

			public override void SetValue(ref Vector3Int container, int value)
			{
				container.y = value;
			}
		}

		private class ZProperty : Property<Vector3Int, int>
		{
			public override string Name => "z";

			public override bool IsReadOnly => false;

			public override int GetValue(ref Vector3Int container)
			{
				return container.z;
			}

			public override void SetValue(ref Vector3Int container, int value)
			{
				container.z = value;
			}
		}

		public Vector3IntPropertyBag()
		{
			AddProperty(new XProperty());
			AddProperty(new YProperty());
			AddProperty(new ZProperty());
		}
	}
	internal class RectPropertyBag : ContainerPropertyBag<Rect>
	{
		private class XProperty : Property<Rect, float>
		{
			public override string Name => "x";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Rect container)
			{
				return container.x;
			}

			public override void SetValue(ref Rect container, float value)
			{
				container.x = value;
			}
		}

		private class YProperty : Property<Rect, float>
		{
			public override string Name => "y";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Rect container)
			{
				return container.y;
			}

			public override void SetValue(ref Rect container, float value)
			{
				container.y = value;
			}
		}

		private class WidthProperty : Property<Rect, float>
		{
			public override string Name => "width";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Rect container)
			{
				return container.width;
			}

			public override void SetValue(ref Rect container, float value)
			{
				container.width = value;
			}
		}

		private class HeightProperty : Property<Rect, float>
		{
			public override string Name => "height";

			public override bool IsReadOnly => false;

			public override float GetValue(ref Rect container)
			{
				return container.height;
			}

			public override void SetValue(ref Rect container, float value)
			{
				container.height = value;
			}
		}

		public RectPropertyBag()
		{
			AddProperty(new XProperty());
			AddProperty(new YProperty());
			AddProperty(new WidthProperty());
			AddProperty(new HeightProperty());
		}
	}
	internal class RectIntPropertyBag : ContainerPropertyBag<RectInt>
	{
		private class XProperty : Property<RectInt, int>
		{
			public override string Name => "x";

			public override bool IsReadOnly => false;

			public override int GetValue(ref RectInt container)
			{
				return container.x;
			}

			public override void SetValue(ref RectInt container, int value)
			{
				container.x = value;
			}
		}

		private class YProperty : Property<RectInt, int>
		{
			public override string Name => "y";

			public override bool IsReadOnly => false;

			public override int GetValue(ref RectInt container)
			{
				return container.y;
			}

			public override void SetValue(ref RectInt container, int value)
			{
				container.y = value;
			}
		}

		private class WidthProperty : Property<RectInt, int>
		{
			public override string Name => "width";

			public override bool IsReadOnly => false;

			public override int GetValue(ref RectInt container)
			{
				return container.width;
			}

			public override void SetValue(ref RectInt container, int value)
			{
				container.width = value;
			}
		}

		private class HeightProperty : Property<RectInt, int>
		{
			public override string Name => "height";

			public override bool IsReadOnly => false;

			public override int GetValue(ref RectInt container)
			{
				return container.height;
			}

			public override void SetValue(ref RectInt container, int value)
			{
				container.height = value;
			}
		}

		public RectIntPropertyBag()
		{
			AddProperty(new XProperty());
			AddProperty(new YProperty());
			AddProperty(new WidthProperty());
			AddProperty(new HeightProperty());
		}
	}
	internal class BoundsPropertyBag : ContainerPropertyBag<Bounds>
	{
		private class CenterProperty : Property<Bounds, Vector3>
		{
			public override string Name => "center";

			public override bool IsReadOnly => false;

			public override Vector3 GetValue(ref Bounds container)
			{
				return container.center;
			}

			public override void SetValue(ref Bounds container, Vector3 value)
			{
				container.center = value;
			}
		}

		private class ExtentsProperty : Property<Bounds, Vector3>
		{
			public override string Name => "extents";

			public override bool IsReadOnly => false;

			public override Vector3 GetValue(ref Bounds container)
			{
				return container.extents;
			}

			public override void SetValue(ref Bounds container, Vector3 value)
			{
				container.extents = value;
			}
		}

		public BoundsPropertyBag()
		{
			AddProperty(new CenterProperty());
			AddProperty(new ExtentsProperty());
		}
	}
	internal class BoundsIntPropertyBag : ContainerPropertyBag<BoundsInt>
	{
		private class PositionProperty : Property<BoundsInt, Vector3Int>
		{
			public override string Name => "position";

			public override bool IsReadOnly => false;

			public override Vector3Int GetValue(ref BoundsInt container)
			{
				return container.position;
			}

			public override void SetValue(ref BoundsInt container, Vector3Int value)
			{
				container.position = value;
			}
		}

		private class SizeProperty : Property<BoundsInt, Vector3Int>
		{
			public override string Name => "size";

			public override bool IsReadOnly => false;

			public override Vector3Int GetValue(ref BoundsInt container)
			{
				return container.size;
			}

			public override void SetValue(ref BoundsInt container, Vector3Int value)
			{
				container.size = value;
			}
		}

		public BoundsIntPropertyBag()
		{
			AddProperty(new PositionProperty());
			AddProperty(new SizeProperty());
		}
	}
	internal class SystemVersionPropertyBag : ContainerPropertyBag<Version>
	{
		private class MajorProperty : Property<Version, int>
		{
			public override string Name => "Major";

			public override bool IsReadOnly => true;

			public MajorProperty()
			{
				AddAttribute(new MinAttribute(0f));
			}

			public override int GetValue(ref Version container)
			{
				return container.Major;
			}

			public override void SetValue(ref Version container, int value)
			{
			}
		}

		private class MinorProperty : Property<Version, int>
		{
			public override string Name => "Minor";

			public override bool IsReadOnly => true;

			public MinorProperty()
			{
				AddAttribute(new MinAttribute(0f));
			}

			public override int GetValue(ref Version container)
			{
				return container.Minor;
			}

			public override void SetValue(ref Version container, int value)
			{
			}
		}

		private class BuildProperty : Property<Version, int>
		{
			public override string Name => "Build";

			public override bool IsReadOnly => true;

			public BuildProperty()
			{
				AddAttribute(new MinAttribute(0f));
			}

			public override int GetValue(ref Version container)
			{
				return container.Build;
			}

			public override void SetValue(ref Version container, int value)
			{
			}
		}

		private class RevisionProperty : Property<Version, int>
		{
			public override string Name => "Revision";

			public override bool IsReadOnly => true;

			public RevisionProperty()
			{
				AddAttribute(new MinAttribute(0f));
			}

			public override int GetValue(ref Version container)
			{
				return container.Revision;
			}

			public override void SetValue(ref Version container, int value)
			{
			}
		}

		public SystemVersionPropertyBag()
		{
			AddProperty(new MajorProperty());
			AddProperty(new MinorProperty());
			AddProperty(new BuildProperty());
			AddProperty(new RevisionProperty());
		}
	}
	internal interface IPropertyBagRegister
	{
		void Register();
	}
	internal static class PropertyBagStore
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		internal struct TypedStore<TContainer>
		{
			public static IPropertyBag<TContainer> PropertyBag;
		}

		private static readonly ConcurrentDictionary<Type, IPropertyBag> s_PropertyBags;

		private static readonly List<Type> s_RegisteredTypes;

		private static ReflectedPropertyBagProvider s_PropertyBagProvider;

		internal static bool HasProvider => s_PropertyBagProvider != null;

		internal static List<Type> AllTypes => s_RegisteredTypes;

		internal static event Action<Type, IPropertyBag> NewTypeRegistered;

		static PropertyBagStore()
		{
			s_PropertyBags = new ConcurrentDictionary<Type, IPropertyBag>();
			s_RegisteredTypes = new List<Type>();
			s_PropertyBagProvider = null;
			s_PropertyBagProvider = new ReflectedPropertyBagProvider();
			DefaultPropertyBagInitializer.Initialize();
		}

		internal static void AddPropertyBag<TContainer>(IPropertyBag<TContainer> propertyBag)
		{
			if (!TypeTraits<TContainer>.IsContainer)
			{
				throw new Exception($"PropertyBagStore Type=[{typeof(TContainer)}] is not a valid container type. Type can not be primitive, enum or string.");
			}
			if (TypeTraits<TContainer>.IsAbstractOrInterface)
			{
				throw new Exception($"PropertyBagStore Type=[{typeof(TContainer)}] is not a valid container type. Type can not be abstract or interface.");
			}
			if (TypedStore<TContainer>.PropertyBag != null)
			{
				IPropertyBag<TContainer> propertyBag2 = TypedStore<TContainer>.PropertyBag;
				if (propertyBag2.GetType().Assembly == typeof(TContainer).Assembly || (propertyBag.GetType().GetCustomAttributes<CompilerGeneratedAttribute>().Any() && propertyBag.GetType().Assembly != typeof(TContainer).Assembly))
				{
					return;
				}
			}
			TypedStore<TContainer>.PropertyBag = propertyBag;
			if (!s_PropertyBags.ContainsKey(typeof(TContainer)))
			{
				s_RegisteredTypes.Add(typeof(TContainer));
			}
			s_PropertyBags[typeof(TContainer)] = propertyBag;
			PropertyBagStore.NewTypeRegistered?.Invoke(typeof(TContainer), propertyBag);
		}

		internal static IPropertyBag<TContainer> GetPropertyBag<TContainer>()
		{
			if (TypedStore<TContainer>.PropertyBag != null)
			{
				return TypedStore<TContainer>.PropertyBag;
			}
			IPropertyBag propertyBag = GetPropertyBag(typeof(TContainer));
			if (propertyBag == null)
			{
				return null;
			}
			if (!(propertyBag is IPropertyBag<TContainer> result))
			{
				throw new InvalidOperationException("PropertyBag type container type mismatch.");
			}
			return result;
		}

		internal static IPropertyBag GetPropertyBag(Type type)
		{
			if (s_PropertyBags.TryGetValue(type, out var value))
			{
				return value;
			}
			if (!TypeTraits.IsContainer(type))
			{
				return null;
			}
			if (type.IsArray && type.GetArrayRank() != 1)
			{
				return null;
			}
			if (type.IsInterface || type.IsAbstract)
			{
				return null;
			}
			if (type == typeof(object))
			{
				return null;
			}
			if (s_PropertyBagProvider != null)
			{
				value = s_PropertyBagProvider.CreatePropertyBag(type);
				if (value != null)
				{
					(value as IPropertyBagRegister)?.Register();
					return value;
				}
				s_PropertyBags.TryAdd(type, null);
			}
			return null;
		}

		internal static bool Exists<TContainer>()
		{
			return TypedStore<TContainer>.PropertyBag != null;
		}

		internal static bool Exists(Type type)
		{
			return s_PropertyBags.ContainsKey(type);
		}

		internal static bool Exists<TContainer>(ref TContainer value)
		{
			if (!TypeTraits<TContainer>.CanBeNull)
			{
				return GetPropertyBag<TContainer>() != null;
			}
			if (EqualityComparer<TContainer>.Default.Equals(value, default(TContainer)))
			{
				return false;
			}
			return GetPropertyBag(value.GetType()) != null;
		}

		internal static bool TryGetPropertyBagForValue<TValue>(ref TValue value, out IPropertyBag propertyBag)
		{
			if (!TypeTraits<TValue>.IsContainer)
			{
				propertyBag = null;
				return false;
			}
			if (TypeTraits<TValue>.CanBeNull && EqualityComparer<TValue>.Default.Equals(value, default(TValue)))
			{
				propertyBag = GetPropertyBag<TValue>();
				return propertyBag != null;
			}
			if (TypeTraits<TValue>.IsValueType)
			{
				propertyBag = GetPropertyBag<TValue>();
				return propertyBag != null;
			}
			propertyBag = GetPropertyBag(value.GetType());
			return propertyBag != null;
		}
	}
	internal readonly struct ReadOnlyAdapterCollection
	{
		public struct Enumerator
		{
			private List<IPropertyVisitorAdapter> m_Adapters;

			private int m_Index;

			public IPropertyVisitorAdapter Current { get; private set; }

			public Enumerator(ReadOnlyAdapterCollection collection)
			{
				m_Adapters = collection.m_Adapters;
				m_Index = 0;
				Current = null;
			}

			public bool MoveNext()
			{
				if (m_Adapters == null)
				{
					return false;
				}
				if (m_Index >= m_Adapters.Count)
				{
					return false;
				}
				Current = m_Adapters[m_Index];
				m_Index++;
				return true;
			}

			private void Reset()
			{
				m_Index = 0;
				Current = null;
			}
		}

		private readonly List<IPropertyVisitorAdapter> m_Adapters;

		public ReadOnlyAdapterCollection(List<IPropertyVisitorAdapter> adapters)
		{
			m_Adapters = adapters;
		}

		public Enumerator GetEnumerator()
		{
			return new Enumerator(this);
		}
	}
	internal class ReflectedPropertyBagAttribute : Attribute
	{
	}
	[ReflectedPropertyBag]
	internal class ReflectedPropertyBag<TContainer> : ContainerPropertyBag<TContainer>
	{
		internal new void AddProperty<TValue>(Property<TContainer, TValue> property)
		{
			TContainer container = default(TContainer);
			if (TryGetProperty(ref container, property.Name, out var property2))
			{
				if (!(property2.DeclaredValueType() == typeof(TValue)))
				{
					UnityEngine.Debug.LogWarning("Detected multiple return types for PropertyBag=[" + TypeUtility.GetTypeDisplayName(typeof(TContainer)) + "] Property=[" + property.Name + "]. The property will use the most derived Type=[" + TypeUtility.GetTypeDisplayName(property2.DeclaredValueType()) + "] and IgnoreType=[" + TypeUtility.GetTypeDisplayName(property.DeclaredValueType()) + "].");
				}
			}
			else
			{
				base.AddProperty(property);
			}
		}
	}
	internal class ReflectedPropertyBagProvider
	{
		private readonly MethodInfo m_CreatePropertyMethod;

		private readonly MethodInfo m_CreatePropertyBagMethod;

		private readonly MethodInfo m_CreateIndexedCollectionPropertyBagMethod;

		private readonly MethodInfo m_CreateSetPropertyBagMethod;

		private readonly MethodInfo m_CreateKeyValueCollectionPropertyBagMethod;

		private readonly MethodInfo m_CreateKeyValuePairPropertyBagMethod;

		private readonly MethodInfo m_CreateArrayPropertyBagMethod;

		private readonly MethodInfo m_CreateListPropertyBagMethod;

		private readonly MethodInfo m_CreateHashSetPropertyBagMethod;

		private readonly MethodInfo m_CreateDictionaryPropertyBagMethod;

		public ReflectedPropertyBagProvider()
		{
			m_CreatePropertyMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateProperty", BindingFlags.Instance | BindingFlags.NonPublic);
			m_CreatePropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethods(BindingFlags.Instance | BindingFlags.Public).First((MethodInfo x) => x.Name == "CreatePropertyBag" && x.IsGenericMethod);
			m_CreateIndexedCollectionPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateIndexedCollectionPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
			m_CreateSetPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateSetPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
			m_CreateKeyValueCollectionPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateKeyValueCollectionPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
			m_CreateKeyValuePairPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateKeyValuePairPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
			m_CreateArrayPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateArrayPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
			m_CreateListPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateListPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
			m_CreateHashSetPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateHashSetPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
			m_CreateDictionaryPropertyBagMethod = typeof(ReflectedPropertyBagProvider).GetMethod("CreateDictionaryPropertyBag", BindingFlags.Instance | BindingFlags.NonPublic);
		}

		public IPropertyBag CreatePropertyBag(Type type)
		{
			if (type.IsGenericTypeDefinition)
			{
				return null;
			}
			return (IPropertyBag)m_CreatePropertyBagMethod.MakeGenericMethod(type).Invoke(this, null);
		}

		public IPropertyBag<TContainer> CreatePropertyBag<TContainer>()
		{
			if (!TypeTraits<TContainer>.IsContainer || TypeTraits<TContainer>.IsObject)
			{
				throw new InvalidOperationException("Invalid container type.");
			}
			if (typeof(TContainer).IsArray)
			{
				if (typeof(TContainer).GetArrayRank() != 1)
				{
					throw new InvalidOperationException("Properties does not support multidimensional arrays.");
				}
				return (IPropertyBag<TContainer>)m_CreateArrayPropertyBagMethod.MakeGenericMethod(typeof(TContainer).GetElementType()).Invoke(this, new object[0]);
			}
			if (typeof(TContainer).IsGenericType && typeof(TContainer).GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)))
			{
				return (IPropertyBag<TContainer>)m_CreateListPropertyBagMethod.MakeGenericMethod(typeof(TContainer).GetGenericArguments().First()).Invoke(this, new object[0]);
			}
			if (typeof(TContainer).IsGenericType && typeof(TContainer).GetGenericTypeDefinition().IsAssignableFrom(typeof(HashSet<>)))
			{
				return (IPropertyBag<TContainer>)m_CreateHashSetPropertyBagMethod.MakeGenericMethod(typeof(TContainer).GetGenericArguments().First()).Invoke(this, new object[0]);
			}
			if (typeof(TContainer).IsGenericType && typeof(TContainer).GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<, >)))
			{
				return (IPropertyBag<TContainer>)m_CreateDictionaryPropertyBagMethod.MakeGenericMethod(typeof(TContainer).GetGenericArguments().First(), typeof(TContainer).GetGenericArguments().ElementAt(1)).Invoke(this, new object[0]);
			}
			if (typeof(TContainer).IsGenericType && typeof(TContainer).GetGenericTypeDefinition().IsAssignableFrom(typeof(IList<>)))
			{
				return (IPropertyBag<TContainer>)m_CreateIndexedCollectionPropertyBagMethod.MakeGenericMethod(typeof(TContainer), typeof(TContainer).GetGenericArguments().First()).Invoke(this, new object[0]);
			}
			if (typeof(TContainer).IsGenericType && typeof(TContainer).GetGenericTypeDefinition().IsAssignableFrom(typeof(ISet<>)))
			{
				return (IPropertyBag<TContainer>)m_CreateSetPropertyBagMethod.MakeGenericMethod(typeof(TContainer), typeof(TContainer).GetGenericArguments().First()).Invoke(this, new object[0]);
			}
			if (typeof(TContainer).IsGenericType && typeof(TContainer).GetGenericTypeDefinition().IsAssignableFrom(typeof(IDictionary<, >)))
			{
				return (IPropertyBag<TContainer>)m_CreateKeyValueCollectionPropertyBagMethod.MakeGenericMethod(typeof(TContainer), typeof(TContainer).GetGenericArguments().First(), typeof(TContainer).GetGenericArguments().ElementAt(1)).Invoke(this, new object[0]);
			}
			if (typeof(TContainer).IsGenericType && typeof(TContainer).GetGenericTypeDefinition().IsAssignableFrom(typeof(KeyValuePair<, >)))
			{
				Type[] array = typeof(TContainer).GetGenericArguments().ToArray();
				return (IPropertyBag<TContainer>)m_CreateKeyValuePairPropertyBagMethod.MakeGenericMethod(array[0], array[1]).Invoke(this, new object[0]);
			}
			ReflectedPropertyBag<TContainer> reflectedPropertyBag = new ReflectedPropertyBag<TContainer>();
			foreach (MemberInfo propertyMember in GetPropertyMembers(typeof(TContainer)))
			{
				MemberInfo memberInfo = propertyMember;
				MemberInfo memberInfo2 = memberInfo;
				IMemberInfo memberInfo3;
				if (!(memberInfo2 is FieldInfo fieldInfo))
				{
					if (!(memberInfo2 is PropertyInfo propertyInfo))
					{
						throw new InvalidOperationException();
					}
					memberInfo3 = new PropertyMember(propertyInfo);
				}
				else
				{
					memberInfo3 = new FieldMember(fieldInfo);
				}
				m_CreatePropertyMethod.MakeGenericMethod(typeof(TContainer), memberInfo3.ValueType).Invoke(this, new object[2] { memberInfo3, reflectedPropertyBag });
			}
			return reflectedPropertyBag;
		}

		[Preserve]
		private void CreateProperty<TContainer, TValue>(IMemberInfo member, ReflectedPropertyBag<TContainer> propertyBag)
		{
			if (!typeof(TValue).IsPointer)
			{
				propertyBag.AddProperty(new ReflectedMemberProperty<TContainer, TValue>(member, member.Name));
			}
		}

		[Preserve]
		private IPropertyBag<TList> CreateIndexedCollectionPropertyBag<TList, TElement>() where TList : IList<TElement>
		{
			return new IndexedCollectionPropertyBag<TList, TElement>();
		}

		[Preserve]
		private IPropertyBag<TSet> CreateSetPropertyBag<TSet, TValue>() where TSet : ISet<TValue>
		{
			return new SetPropertyBagBase<TSet, TValue>();
		}

		[Preserve]
		private IPropertyBag<TDictionary> CreateKeyValueCollectionPropertyBag<TDictionary, TKey, TValue>() where TDictionary : IDictionary<TKey, TValue>
		{
			return new KeyValueCollectionPropertyBag<TDictionary, TKey, TValue>();
		}

		[Preserve]
		private IPropertyBag<KeyValuePair<TKey, TValue>> CreateKeyValuePairPropertyBag<TKey, TValue>()
		{
			return new KeyValuePairPropertyBag<TKey, TValue>();
		}

		[Preserve]
		private IPropertyBag<TElement[]> CreateArrayPropertyBag<TElement>()
		{
			return new ArrayPropertyBag<TElement>();
		}

		[Preserve]
		private IPropertyBag<List<TElement>> CreateListPropertyBag<TElement>()
		{
			return new ListPropertyBag<TElement>();
		}

		[Preserve]
		private IPropertyBag<HashSet<TElement>> CreateHashSetPropertyBag<TElement>()
		{
			return new HashSetPropertyBag<TElement>();
		}

		[Preserve]
		private IPropertyBag<Dictionary<TKey, TValue>> CreateDictionaryPropertyBag<TKey, TValue>()
		{
			return new DictionaryPropertyBag<TKey, TValue>();
		}

		private static IEnumerable<MemberInfo> GetPropertyMembers(Type type)
		{
			do
			{
				IOrderedEnumerable<MemberInfo> members = from x in type.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
					orderby x.MetadataToken
					select x;
				foreach (MemberInfo member in members)
				{
					if ((member.MemberType != MemberTypes.Field && member.MemberType != MemberTypes.Property) || member.DeclaringType != type || !IsValidMember(member))
					{
						continue;
					}
					bool hasDontCreatePropertyAttribute = member.GetCustomAttribute<DontCreatePropertyAttribute>() != null;
					bool hasCreatePropertyAttribute = member.GetCustomAttribute<CreatePropertyAttribute>() != null;
					bool hasNonSerializedAttribute = member.GetCustomAttribute<NonSerializedAttribute>() != null;
					bool hasSerializedFieldAttribute = member.GetCustomAttribute<SerializeField>() != null;
					if (hasDontCreatePropertyAttribute)
					{
						continue;
					}
					if (hasCreatePropertyAttribute)
					{
						yield return member;
					}
					else if (!hasNonSerializedAttribute)
					{
						if (hasSerializedFieldAttribute)
						{
							yield return member;
						}
						else if (member is FieldInfo field && field.IsPublic)
						{
							yield return member;
						}
					}
				}
				type = type.BaseType;
			}
			while (type != null && type != typeof(object));
		}

		private static bool IsValidMember(MemberInfo memberInfo)
		{
			if (!(memberInfo is FieldInfo fieldInfo))
			{
				if (memberInfo is PropertyInfo propertyInfo)
				{
					return null != propertyInfo.GetMethod && !propertyInfo.GetMethod.IsStatic && IsValidPropertyType(propertyInfo.PropertyType);
				}
				return false;
			}
			return !fieldInfo.IsStatic && IsValidPropertyType(fieldInfo.FieldType);
		}

		private static bool IsValidPropertyType(Type type)
		{
			if (type.IsPointer)
			{
				return false;
			}
			return !type.IsGenericType || type.GetGenericArguments().All(IsValidPropertyType);
		}
	}
	internal static class ReflectionUtilities
	{
		public static string SanitizeMemberName(MemberInfo info)
		{
			return info.Name.Replace(".", "_").Replace("<", "_").Replace(">", "")
				.Replace("+", "_");
		}
	}
}
