using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

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
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
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
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine
{
	public delegate void AndroidJavaRunnable();
	public sealed class AndroidJavaException : Exception
	{
		private string mJavaStackTrace;

		public override string StackTrace => mJavaStackTrace + base.StackTrace;

		internal AndroidJavaException(string message, string javaStackTrace)
			: base(message)
		{
			mJavaStackTrace = javaStackTrace;
		}
	}
	internal class GlobalJavaObjectRef
	{
		private bool m_disposed = false;

		protected IntPtr m_jobject;

		public GlobalJavaObjectRef(IntPtr jobject)
		{
			m_jobject = ((jobject == IntPtr.Zero) ? IntPtr.Zero : AndroidJNI.NewGlobalRef(jobject));
		}

		~GlobalJavaObjectRef()
		{
			Dispose();
		}

		public static implicit operator IntPtr(GlobalJavaObjectRef obj)
		{
			return obj.m_jobject;
		}

		public void Dispose()
		{
			if (!m_disposed)
			{
				m_disposed = true;
				if (m_jobject != IntPtr.Zero)
				{
					AndroidJNISafe.QueueDeleteGlobalRef(m_jobject);
				}
			}
		}
	}
	internal class AndroidJavaRunnableProxy : AndroidJavaProxy
	{
		private AndroidJavaRunnable mRunnable;

		public AndroidJavaRunnableProxy(AndroidJavaRunnable runnable)
			: base("java/lang/Runnable")
		{
			mRunnable = runnable;
		}

		public void run()
		{
			mRunnable();
		}

		public override IntPtr Invoke(string methodName, IntPtr javaArgs)
		{
			int num = 0;
			if (javaArgs != IntPtr.Zero)
			{
				num = AndroidJNISafe.GetArrayLength(javaArgs);
			}
			if (num == 0 && methodName == "run")
			{
				run();
				return IntPtr.Zero;
			}
			return base.Invoke(methodName, javaArgs);
		}
	}
	public class AndroidJavaProxy
	{
		public readonly AndroidJavaClass javaInterface;

		internal IntPtr proxyObject = IntPtr.Zero;

		private static readonly GlobalJavaObjectRef s_JavaLangSystemClass = new GlobalJavaObjectRef(AndroidJNISafe.FindClass("java/lang/System"));

		private static readonly IntPtr s_HashCodeMethodID = AndroidJNIHelper.GetMethodID(s_JavaLangSystemClass, "identityHashCode", "(Ljava/lang/Object;)I", isStatic: true);

		public AndroidJavaProxy(string javaInterface)
			: this(new AndroidJavaClass(javaInterface))
		{
		}

		public AndroidJavaProxy(AndroidJavaClass javaInterface)
		{
			this.javaInterface = javaInterface;
		}

		~AndroidJavaProxy()
		{
			AndroidJNISafe.DeleteWeakGlobalRef(proxyObject);
		}

		public virtual AndroidJavaObject Invoke(string methodName, object[] args)
		{
			Exception ex = null;
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			int num = 0;
			Type[] array = new Type[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == null)
				{
					array[i] = null;
					num++;
				}
				else
				{
					array[i] = args[i].GetType();
				}
			}
			try
			{
				MethodInfo methodInfo = null;
				if (num > 0)
				{
					MethodInfo[] methods = GetType().GetMethods(bindingAttr);
					int num2 = 0;
					MethodInfo[] array2 = methods;
					foreach (MethodInfo methodInfo2 in array2)
					{
						if (methodName != methodInfo2.Name)
						{
							continue;
						}
						ParameterInfo[] parameters = methodInfo2.GetParameters();
						if (parameters.Length != args.Length)
						{
							continue;
						}
						bool flag = true;
						for (int k = 0; k < parameters.Length; k++)
						{
							if (array[k] == null)
							{
								if (parameters[k].ParameterType.IsValueType)
								{
									flag = false;
									break;
								}
							}
							else if (!parameters[k].ParameterType.IsAssignableFrom(array[k]))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							num2++;
							methodInfo = methodInfo2;
						}
					}
					if (num2 > 1)
					{
						throw new Exception("Ambiguous overloads found for " + methodName + " with given parameters");
					}
				}
				else
				{
					methodInfo = GetType().GetMethod(methodName, bindingAttr, null, array, null);
				}
				if (methodInfo != null)
				{
					return _AndroidJNIHelper.Box(methodInfo.Invoke(this, args));
				}
			}
			catch (TargetInvocationException ex2)
			{
				ex = ex2.InnerException;
			}
			catch (Exception ex3)
			{
				ex = ex3;
			}
			string[] array3 = new string[args.Length];
			for (int l = 0; l < array3.Length; l++)
			{
				if (array[l] == null)
				{
					array3[l] = "null";
				}
				else
				{
					array3[l] = array[l].ToString();
				}
			}
			if (ex != null)
			{
				throw new TargetInvocationException(GetType()?.ToString() + "." + methodName + "(" + string.Join(",", array3) + ")", ex);
			}
			Exception ex4 = new Exception("No such proxy method: " + GetType()?.ToString() + "." + methodName + "(" + string.Join(",", array3) + ")");
			IntPtr intPtr = AndroidReflection.CreateInvocationError(ex4, methodNotFound: true);
			return (intPtr == IntPtr.Zero) ? null : new AndroidJavaObject(intPtr);
		}

		public virtual AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] javaArgs)
		{
			object[] array = new object[javaArgs.Length];
			for (int i = 0; i < javaArgs.Length; i++)
			{
				array[i] = _AndroidJNIHelper.Unbox(javaArgs[i]);
				if (!(array[i] is AndroidJavaObject) && javaArgs[i] != null)
				{
					javaArgs[i].Dispose();
				}
			}
			return Invoke(methodName, array);
		}

		public virtual IntPtr Invoke(string methodName, IntPtr javaArgs)
		{
			int num = 0;
			if (javaArgs != IntPtr.Zero)
			{
				num = AndroidJNISafe.GetArrayLength(javaArgs);
			}
			if (num == 1 && methodName == "equals")
			{
				IntPtr objectArrayElement = AndroidJNISafe.GetObjectArrayElement(javaArgs, 0);
				AndroidJavaObject obj = ((objectArrayElement == IntPtr.Zero) ? null : new AndroidJavaObject(objectArrayElement));
				return AndroidJNIHelper.Box(equals(obj));
			}
			if (num == 0 && methodName == "hashCode")
			{
				return AndroidJNIHelper.Box(hashCode());
			}
			AndroidJavaObject[] array = new AndroidJavaObject[num];
			for (int i = 0; i < num; i++)
			{
				IntPtr objectArrayElement2 = AndroidJNISafe.GetObjectArrayElement(javaArgs, i);
				array[i] = ((objectArrayElement2 != IntPtr.Zero) ? AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(objectArrayElement2) : null);
			}
			using AndroidJavaObject androidJavaObject = Invoke(methodName, array);
			if (androidJavaObject == null)
			{
				return IntPtr.Zero;
			}
			return AndroidJNI.NewLocalRef(androidJavaObject.GetRawObject());
		}

		public virtual bool equals(AndroidJavaObject obj)
		{
			IntPtr obj2 = obj?.GetRawObject() ?? IntPtr.Zero;
			return AndroidJNI.IsSameObject(proxyObject, obj2);
		}

		public virtual int hashCode()
		{
			Span<jvalue> args = stackalloc jvalue[1];
			args[0].l = GetRawProxy();
			return AndroidJNISafe.CallStaticIntMethod(s_JavaLangSystemClass, s_HashCodeMethodID, args);
		}

		public virtual string toString()
		{
			return this?.ToString() + " <c# proxy java object>";
		}

		internal AndroidJavaObject GetProxyObject()
		{
			return AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(GetRawProxy());
		}

		internal IntPtr GetRawProxy()
		{
			IntPtr intPtr = IntPtr.Zero;
			if (proxyObject != IntPtr.Zero)
			{
				intPtr = AndroidJNI.NewLocalRef(proxyObject);
				if (intPtr == IntPtr.Zero)
				{
					AndroidJNI.DeleteWeakGlobalRef(proxyObject);
					proxyObject = IntPtr.Zero;
				}
			}
			if (intPtr == IntPtr.Zero)
			{
				intPtr = AndroidJNIHelper.CreateJavaProxy(this);
				proxyObject = AndroidJNI.NewWeakGlobalRef(intPtr);
			}
			return intPtr;
		}
	}
	public class AndroidJavaObject : IDisposable
	{
		private static bool enableDebugPrints;

		internal GlobalJavaObjectRef m_jobject;

		internal GlobalJavaObjectRef m_jclass;

		public AndroidJavaObject(string className, string[] args)
			: this()
		{
			_AndroidJavaObject(className, new object[1] { args });
		}

		public AndroidJavaObject(string className, AndroidJavaObject[] args)
			: this()
		{
			_AndroidJavaObject(className, new object[1] { args });
		}

		public AndroidJavaObject(string className, AndroidJavaClass[] args)
			: this()
		{
			_AndroidJavaObject(className, new object[1] { args });
		}

		public AndroidJavaObject(string className, AndroidJavaProxy[] args)
			: this()
		{
			_AndroidJavaObject(className, new object[1] { args });
		}

		public AndroidJavaObject(string className, AndroidJavaRunnable[] args)
			: this()
		{
			_AndroidJavaObject(className, new object[1] { args });
		}

		public AndroidJavaObject(string className, params object[] args)
			: this()
		{
			_AndroidJavaObject(className, args);
		}

		public AndroidJavaObject(IntPtr jobject)
			: this()
		{
			if (jobject == IntPtr.Zero)
			{
				throw new Exception("JNI: Init'd AndroidJavaObject with null ptr!");
			}
			IntPtr objectClass = AndroidJNISafe.GetObjectClass(jobject);
			m_jobject = new GlobalJavaObjectRef(jobject);
			m_jclass = new GlobalJavaObjectRef(objectClass);
			AndroidJNISafe.DeleteLocalRef(objectClass);
		}

		public AndroidJavaObject(IntPtr clazz, IntPtr constructorID, params object[] args)
		{
			m_jclass = new GlobalJavaObjectRef(clazz);
			_AndroidJavaObject(constructorID, args);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public void Call<T>(string methodName, T[] args)
		{
			_Call(methodName, args);
		}

		public void Call<T>(IntPtr methodID, T[] args)
		{
			_Call(methodID, args);
		}

		public void Call(string methodName, params object[] args)
		{
			_Call(methodName, args);
		}

		public void Call(IntPtr methodID, params object[] args)
		{
			_Call(methodID, args);
		}

		public void CallStatic<T>(string methodName, T[] args)
		{
			_CallStatic(methodName, args);
		}

		public void CallStatic<T>(IntPtr methodID, T[] args)
		{
			_CallStatic(methodID, args);
		}

		public void CallStatic(string methodName, params object[] args)
		{
			_CallStatic(methodName, args);
		}

		public void CallStatic(IntPtr methodID, params object[] args)
		{
			_CallStatic(methodID, args);
		}

		public FieldType Get<FieldType>(string fieldName)
		{
			return _Get<FieldType>(fieldName);
		}

		public FieldType Get<FieldType>(IntPtr fieldID)
		{
			return _Get<FieldType>(fieldID);
		}

		public void Set<FieldType>(string fieldName, FieldType val)
		{
			_Set(fieldName, val);
		}

		public void Set<FieldType>(IntPtr fieldID, FieldType val)
		{
			_Set(fieldID, val);
		}

		public FieldType GetStatic<FieldType>(string fieldName)
		{
			return _GetStatic<FieldType>(fieldName);
		}

		public FieldType GetStatic<FieldType>(IntPtr fieldID)
		{
			return _GetStatic<FieldType>(fieldID);
		}

		public void SetStatic<FieldType>(string fieldName, FieldType val)
		{
			_SetStatic(fieldName, val);
		}

		public void SetStatic<FieldType>(IntPtr fieldID, FieldType val)
		{
			_SetStatic(fieldID, val);
		}

		public IntPtr GetRawObject()
		{
			return _GetRawObject();
		}

		public IntPtr GetRawClass()
		{
			return _GetRawClass();
		}

		public AndroidJavaObject CloneReference()
		{
			if (m_jclass == null)
			{
				throw new Exception("Cannot clone a disposed reference");
			}
			if (m_jobject != null)
			{
				AndroidJavaObject androidJavaObject = new AndroidJavaObject();
				androidJavaObject.m_jobject = new GlobalJavaObjectRef(m_jobject);
				androidJavaObject.m_jclass = new GlobalJavaObjectRef(m_jclass);
				return androidJavaObject;
			}
			return new AndroidJavaClass(m_jclass);
		}

		public ReturnType Call<ReturnType, T>(string methodName, T[] args)
		{
			return _Call<ReturnType>(methodName, new object[1] { args });
		}

		public ReturnType Call<ReturnType, T>(IntPtr methodID, T[] args)
		{
			return _Call<ReturnType>(methodID, new object[1] { args });
		}

		public ReturnType Call<ReturnType>(string methodName, params object[] args)
		{
			return _Call<ReturnType>(methodName, args);
		}

		public ReturnType Call<ReturnType>(IntPtr methodID, params object[] args)
		{
			return _Call<ReturnType>(methodID, args);
		}

		public ReturnType CallStatic<ReturnType, T>(string methodName, T[] args)
		{
			return _CallStatic<ReturnType>(methodName, new object[1] { args });
		}

		public ReturnType CallStatic<ReturnType, T>(IntPtr methodID, T[] args)
		{
			return _CallStatic<ReturnType>(methodID, new object[1] { args });
		}

		public ReturnType CallStatic<ReturnType>(string methodName, params object[] args)
		{
			return _CallStatic<ReturnType>(methodName, args);
		}

		public ReturnType CallStatic<ReturnType>(IntPtr methodID, params object[] args)
		{
			return _CallStatic<ReturnType>(methodID, args);
		}

		protected void DebugPrint(string msg)
		{
			if (enableDebugPrints)
			{
				Debug.Log(msg);
			}
		}

		protected void DebugPrint(string call, string methodName, string signature, object[] args)
		{
			if (enableDebugPrints)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in args)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append((obj == null) ? "<null>" : obj.GetType().ToString());
				}
				Debug.Log(call + "(\"" + methodName + "\"" + stringBuilder?.ToString() + ") = " + signature);
			}
		}

		private void _AndroidJavaObject(string className, params object[] args)
		{
			DebugPrint("Creating AndroidJavaObject from " + className);
			IntPtr intPtr = AndroidJNISafe.FindClass(className.Replace('.', '/'));
			m_jclass = new GlobalJavaObjectRef(intPtr);
			AndroidJNISafe.DeleteLocalRef(intPtr);
			IntPtr constructorID = AndroidJNIHelper.GetConstructorID(m_jclass, args);
			_AndroidJavaObject(constructorID, args);
		}

		private void _AndroidJavaObject(IntPtr constructorID, params object[] args)
		{
			Span<jvalue> span = ((args != null && args.Length != 0) ? stackalloc jvalue[args.Length] : default(Span<jvalue>));
			Span<jvalue> span2 = span;
			AndroidJNIHelper.CreateJNIArgArray(args, span2);
			try
			{
				IntPtr intPtr = AndroidJNISafe.NewObject(m_jclass, constructorID, span2);
				m_jobject = new GlobalJavaObjectRef(intPtr);
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, span2);
			}
		}

		internal AndroidJavaObject()
		{
		}

		~AndroidJavaObject()
		{
			Dispose(disposing: true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (m_jobject != null)
			{
				m_jobject.Dispose();
				m_jobject = null;
			}
			if (m_jclass != null)
			{
				m_jclass.Dispose();
				m_jclass = null;
			}
		}

		protected void _Call(string methodName, params object[] args)
		{
			IntPtr methodID = AndroidJNIHelper.GetMethodID(m_jclass, methodName, args, isStatic: false);
			_Call(methodID, args);
		}

		protected void _Call(IntPtr methodID, params object[] args)
		{
			Span<jvalue> span = ((args != null && args.Length != 0) ? stackalloc jvalue[args.Length] : default(Span<jvalue>));
			Span<jvalue> span2 = span;
			AndroidJNIHelper.CreateJNIArgArray(args, span2);
			try
			{
				AndroidJNISafe.CallVoidMethod(m_jobject, methodID, span2);
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, span2);
			}
		}

		protected ReturnType _Call<ReturnType>(string methodName, params object[] args)
		{
			IntPtr methodID = AndroidJNIHelper.GetMethodID<ReturnType>(m_jclass, methodName, args, isStatic: false);
			return _Call<ReturnType>(methodID, args);
		}

		protected ReturnType _Call<ReturnType>(IntPtr methodID, params object[] args)
		{
			Span<jvalue> span = ((args != null && args.Length != 0) ? stackalloc jvalue[args.Length] : default(Span<jvalue>));
			Span<jvalue> span2 = span;
			AndroidJNIHelper.CreateJNIArgArray(args, span2);
			try
			{
				if (AndroidReflection.IsPrimitive(typeof(ReturnType)))
				{
					if (typeof(ReturnType) == typeof(int))
					{
						return (ReturnType)(object)AndroidJNISafe.CallIntMethod(m_jobject, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(bool))
					{
						return (ReturnType)(object)AndroidJNISafe.CallBooleanMethod(m_jobject, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(byte))
					{
						Debug.LogWarning("Return type <Byte> for Java method call is obsolete, use return type <SByte> instead");
						return (ReturnType)(object)(byte)AndroidJNISafe.CallSByteMethod(m_jobject, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(sbyte))
					{
						return (ReturnType)(object)AndroidJNISafe.CallSByteMethod(m_jobject, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(short))
					{
						return (ReturnType)(object)AndroidJNISafe.CallShortMethod(m_jobject, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(long))
					{
						return (ReturnType)(object)AndroidJNISafe.CallLongMethod(m_jobject, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(float))
					{
						return (ReturnType)(object)AndroidJNISafe.CallFloatMethod(m_jobject, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(double))
					{
						return (ReturnType)(object)AndroidJNISafe.CallDoubleMethod(m_jobject, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(char))
					{
						return (ReturnType)(object)AndroidJNISafe.CallCharMethod(m_jobject, methodID, span2);
					}
					return default(ReturnType);
				}
				if (typeof(ReturnType) == typeof(string))
				{
					return (ReturnType)(object)AndroidJNISafe.CallStringMethod(m_jobject, methodID, span2);
				}
				if (typeof(ReturnType) == typeof(AndroidJavaClass))
				{
					IntPtr intPtr = AndroidJNISafe.CallObjectMethod(m_jobject, methodID, span2);
					return (intPtr == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)(object)AndroidJavaClassDeleteLocalRef(intPtr));
				}
				if (typeof(ReturnType) == typeof(AndroidJavaObject))
				{
					IntPtr intPtr2 = AndroidJNISafe.CallObjectMethod(m_jobject, methodID, span2);
					return (intPtr2 == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)(object)AndroidJavaObjectDeleteLocalRef(intPtr2));
				}
				if (AndroidReflection.IsAssignableFrom(typeof(Array), typeof(ReturnType)))
				{
					IntPtr jobject = AndroidJNISafe.CallObjectMethod(m_jobject, methodID, span2);
					return FromJavaArrayDeleteLocalRef<ReturnType>(jobject);
				}
				throw new Exception("JNI: Unknown return type '" + typeof(ReturnType)?.ToString() + "'");
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, span2);
			}
		}

		protected FieldType _Get<FieldType>(string fieldName)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(m_jclass, fieldName, isStatic: false);
			return _Get<FieldType>(fieldID);
		}

		protected FieldType _Get<FieldType>(IntPtr fieldID)
		{
			if (AndroidReflection.IsPrimitive(typeof(FieldType)))
			{
				if (typeof(FieldType) == typeof(int))
				{
					return (FieldType)(object)AndroidJNISafe.GetIntField(m_jobject, fieldID);
				}
				if (typeof(FieldType) == typeof(bool))
				{
					return (FieldType)(object)AndroidJNISafe.GetBooleanField(m_jobject, fieldID);
				}
				if (typeof(FieldType) == typeof(byte))
				{
					Debug.LogWarning("Field type <Byte> for Java get field call is obsolete, use field type <SByte> instead");
					return (FieldType)(object)(byte)AndroidJNISafe.GetSByteField(m_jobject, fieldID);
				}
				if (typeof(FieldType) == typeof(sbyte))
				{
					return (FieldType)(object)AndroidJNISafe.GetSByteField(m_jobject, fieldID);
				}
				if (typeof(FieldType) == typeof(short))
				{
					return (FieldType)(object)AndroidJNISafe.GetShortField(m_jobject, fieldID);
				}
				if (typeof(FieldType) == typeof(long))
				{
					return (FieldType)(object)AndroidJNISafe.GetLongField(m_jobject, fieldID);
				}
				if (typeof(FieldType) == typeof(float))
				{
					return (FieldType)(object)AndroidJNISafe.GetFloatField(m_jobject, fieldID);
				}
				if (typeof(FieldType) == typeof(double))
				{
					return (FieldType)(object)AndroidJNISafe.GetDoubleField(m_jobject, fieldID);
				}
				if (typeof(FieldType) == typeof(char))
				{
					return (FieldType)(object)AndroidJNISafe.GetCharField(m_jobject, fieldID);
				}
				return default(FieldType);
			}
			if (typeof(FieldType) == typeof(string))
			{
				return (FieldType)(object)AndroidJNISafe.GetStringField(m_jobject, fieldID);
			}
			if (typeof(FieldType) == typeof(AndroidJavaClass))
			{
				IntPtr objectField = AndroidJNISafe.GetObjectField(m_jobject, fieldID);
				return (objectField == IntPtr.Zero) ? default(FieldType) : ((FieldType)(object)AndroidJavaClassDeleteLocalRef(objectField));
			}
			if (typeof(FieldType) == typeof(AndroidJavaObject))
			{
				IntPtr objectField2 = AndroidJNISafe.GetObjectField(m_jobject, fieldID);
				return (objectField2 == IntPtr.Zero) ? default(FieldType) : ((FieldType)(object)AndroidJavaObjectDeleteLocalRef(objectField2));
			}
			if (AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType)))
			{
				IntPtr objectField3 = AndroidJNISafe.GetObjectField(m_jobject, fieldID);
				return FromJavaArrayDeleteLocalRef<FieldType>(objectField3);
			}
			throw new Exception("JNI: Unknown field type '" + typeof(FieldType)?.ToString() + "'");
		}

		protected void _Set<FieldType>(string fieldName, FieldType val)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(m_jclass, fieldName, isStatic: false);
			_Set(fieldID, val);
		}

		protected void _Set<FieldType>(IntPtr fieldID, FieldType val)
		{
			if (AndroidReflection.IsPrimitive(typeof(FieldType)))
			{
				if (typeof(FieldType) == typeof(int))
				{
					AndroidJNISafe.SetIntField(m_jobject, fieldID, (int)(object)val);
				}
				else if (typeof(FieldType) == typeof(bool))
				{
					AndroidJNISafe.SetBooleanField(m_jobject, fieldID, (bool)(object)val);
				}
				else if (typeof(FieldType) == typeof(byte))
				{
					Debug.LogWarning("Field type <Byte> for Java set field call is obsolete, use field type <SByte> instead");
					AndroidJNISafe.SetSByteField(m_jobject, fieldID, (sbyte)(byte)(object)val);
				}
				else if (typeof(FieldType) == typeof(sbyte))
				{
					AndroidJNISafe.SetSByteField(m_jobject, fieldID, (sbyte)(object)val);
				}
				else if (typeof(FieldType) == typeof(short))
				{
					AndroidJNISafe.SetShortField(m_jobject, fieldID, (short)(object)val);
				}
				else if (typeof(FieldType) == typeof(long))
				{
					AndroidJNISafe.SetLongField(m_jobject, fieldID, (long)(object)val);
				}
				else if (typeof(FieldType) == typeof(float))
				{
					AndroidJNISafe.SetFloatField(m_jobject, fieldID, (float)(object)val);
				}
				else if (typeof(FieldType) == typeof(double))
				{
					AndroidJNISafe.SetDoubleField(m_jobject, fieldID, (double)(object)val);
				}
				else if (typeof(FieldType) == typeof(char))
				{
					AndroidJNISafe.SetCharField(m_jobject, fieldID, (char)(object)val);
				}
			}
			else if (typeof(FieldType) == typeof(string))
			{
				AndroidJNISafe.SetStringField(m_jobject, fieldID, (string)(object)val);
			}
			else if (typeof(FieldType) == typeof(AndroidJavaClass))
			{
				AndroidJNISafe.SetObjectField(m_jobject, fieldID, (val == null) ? IntPtr.Zero : ((IntPtr)((AndroidJavaClass)(object)val).m_jclass));
			}
			else if (typeof(FieldType) == typeof(AndroidJavaObject))
			{
				AndroidJNISafe.SetObjectField(m_jobject, fieldID, (val == null) ? IntPtr.Zero : ((IntPtr)((AndroidJavaObject)(object)val).m_jobject));
			}
			else if (AndroidReflection.IsAssignableFrom(typeof(AndroidJavaProxy), typeof(FieldType)))
			{
				AndroidJNISafe.SetObjectField(m_jobject, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaProxy)(object)val).GetRawProxy());
			}
			else
			{
				if (!AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType)))
				{
					throw new Exception("JNI: Unknown field type '" + typeof(FieldType)?.ToString() + "'");
				}
				IntPtr val2 = AndroidJNIHelper.ConvertToJNIArray((Array)(object)val);
				AndroidJNISafe.SetObjectField(m_jobject, fieldID, val2);
			}
		}

		protected void _CallStatic(string methodName, params object[] args)
		{
			IntPtr methodID = AndroidJNIHelper.GetMethodID(m_jclass, methodName, args, isStatic: true);
			_CallStatic(methodID, args);
		}

		protected void _CallStatic(IntPtr methodID, params object[] args)
		{
			Span<jvalue> span = ((args != null && args.Length != 0) ? stackalloc jvalue[args.Length] : default(Span<jvalue>));
			Span<jvalue> span2 = span;
			AndroidJNIHelper.CreateJNIArgArray(args, span2);
			try
			{
				AndroidJNISafe.CallStaticVoidMethod(m_jclass, methodID, span2);
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, span2);
			}
		}

		protected ReturnType _CallStatic<ReturnType>(string methodName, params object[] args)
		{
			IntPtr methodID = AndroidJNIHelper.GetMethodID<ReturnType>(m_jclass, methodName, args, isStatic: true);
			return _CallStatic<ReturnType>(methodID, args);
		}

		protected ReturnType _CallStatic<ReturnType>(IntPtr methodID, params object[] args)
		{
			Span<jvalue> span = ((args != null && args.Length != 0) ? stackalloc jvalue[args.Length] : default(Span<jvalue>));
			Span<jvalue> span2 = span;
			AndroidJNIHelper.CreateJNIArgArray(args, span2);
			try
			{
				if (AndroidReflection.IsPrimitive(typeof(ReturnType)))
				{
					if (typeof(ReturnType) == typeof(int))
					{
						return (ReturnType)(object)AndroidJNISafe.CallStaticIntMethod(m_jclass, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(bool))
					{
						return (ReturnType)(object)AndroidJNISafe.CallStaticBooleanMethod(m_jclass, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(byte))
					{
						Debug.LogWarning("Return type <Byte> for Java method call is obsolete, use return type <SByte> instead");
						return (ReturnType)(object)(byte)AndroidJNISafe.CallStaticSByteMethod(m_jclass, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(sbyte))
					{
						return (ReturnType)(object)AndroidJNISafe.CallStaticSByteMethod(m_jclass, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(short))
					{
						return (ReturnType)(object)AndroidJNISafe.CallStaticShortMethod(m_jclass, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(long))
					{
						return (ReturnType)(object)AndroidJNISafe.CallStaticLongMethod(m_jclass, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(float))
					{
						return (ReturnType)(object)AndroidJNISafe.CallStaticFloatMethod(m_jclass, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(double))
					{
						return (ReturnType)(object)AndroidJNISafe.CallStaticDoubleMethod(m_jclass, methodID, span2);
					}
					if (typeof(ReturnType) == typeof(char))
					{
						return (ReturnType)(object)AndroidJNISafe.CallStaticCharMethod(m_jclass, methodID, span2);
					}
					return default(ReturnType);
				}
				if (typeof(ReturnType) == typeof(string))
				{
					return (ReturnType)(object)AndroidJNISafe.CallStaticStringMethod(m_jclass, methodID, span2);
				}
				if (typeof(ReturnType) == typeof(AndroidJavaClass))
				{
					IntPtr intPtr = AndroidJNISafe.CallStaticObjectMethod(m_jclass, methodID, span2);
					return (intPtr == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)(object)AndroidJavaClassDeleteLocalRef(intPtr));
				}
				if (typeof(ReturnType) == typeof(AndroidJavaObject))
				{
					IntPtr intPtr2 = AndroidJNISafe.CallStaticObjectMethod(m_jclass, methodID, span2);
					return (intPtr2 == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)(object)AndroidJavaObjectDeleteLocalRef(intPtr2));
				}
				if (AndroidReflection.IsAssignableFrom(typeof(Array), typeof(ReturnType)))
				{
					IntPtr jobject = AndroidJNISafe.CallStaticObjectMethod(m_jclass, methodID, span2);
					return FromJavaArrayDeleteLocalRef<ReturnType>(jobject);
				}
				throw new Exception("JNI: Unknown return type '" + typeof(ReturnType)?.ToString() + "'");
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, span2);
			}
		}

		protected FieldType _GetStatic<FieldType>(string fieldName)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(m_jclass, fieldName, isStatic: true);
			return _GetStatic<FieldType>(fieldID);
		}

		protected FieldType _GetStatic<FieldType>(IntPtr fieldID)
		{
			if (AndroidReflection.IsPrimitive(typeof(FieldType)))
			{
				if (typeof(FieldType) == typeof(int))
				{
					return (FieldType)(object)AndroidJNISafe.GetStaticIntField(m_jclass, fieldID);
				}
				if (typeof(FieldType) == typeof(bool))
				{
					return (FieldType)(object)AndroidJNISafe.GetStaticBooleanField(m_jclass, fieldID);
				}
				if (typeof(FieldType) == typeof(byte))
				{
					Debug.LogWarning("Field type <Byte> for Java get field call is obsolete, use field type <SByte> instead");
					return (FieldType)(object)(byte)AndroidJNISafe.GetStaticSByteField(m_jclass, fieldID);
				}
				if (typeof(FieldType) == typeof(sbyte))
				{
					return (FieldType)(object)AndroidJNISafe.GetStaticSByteField(m_jclass, fieldID);
				}
				if (typeof(FieldType) == typeof(short))
				{
					return (FieldType)(object)AndroidJNISafe.GetStaticShortField(m_jclass, fieldID);
				}
				if (typeof(FieldType) == typeof(long))
				{
					return (FieldType)(object)AndroidJNISafe.GetStaticLongField(m_jclass, fieldID);
				}
				if (typeof(FieldType) == typeof(float))
				{
					return (FieldType)(object)AndroidJNISafe.GetStaticFloatField(m_jclass, fieldID);
				}
				if (typeof(FieldType) == typeof(double))
				{
					return (FieldType)(object)AndroidJNISafe.GetStaticDoubleField(m_jclass, fieldID);
				}
				if (typeof(FieldType) == typeof(char))
				{
					return (FieldType)(object)AndroidJNISafe.GetStaticCharField(m_jclass, fieldID);
				}
				return default(FieldType);
			}
			if (typeof(FieldType) == typeof(string))
			{
				return (FieldType)(object)AndroidJNISafe.GetStaticStringField(m_jclass, fieldID);
			}
			if (typeof(FieldType) == typeof(AndroidJavaClass))
			{
				IntPtr staticObjectField = AndroidJNISafe.GetStaticObjectField(m_jclass, fieldID);
				return (staticObjectField == IntPtr.Zero) ? default(FieldType) : ((FieldType)(object)AndroidJavaClassDeleteLocalRef(staticObjectField));
			}
			if (typeof(FieldType) == typeof(AndroidJavaObject))
			{
				IntPtr staticObjectField2 = AndroidJNISafe.GetStaticObjectField(m_jclass, fieldID);
				return (staticObjectField2 == IntPtr.Zero) ? default(FieldType) : ((FieldType)(object)AndroidJavaObjectDeleteLocalRef(staticObjectField2));
			}
			if (AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType)))
			{
				IntPtr staticObjectField3 = AndroidJNISafe.GetStaticObjectField(m_jclass, fieldID);
				return FromJavaArrayDeleteLocalRef<FieldType>(staticObjectField3);
			}
			throw new Exception("JNI: Unknown field type '" + typeof(FieldType)?.ToString() + "'");
		}

		protected void _SetStatic<FieldType>(string fieldName, FieldType val)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(m_jclass, fieldName, isStatic: true);
			_SetStatic(fieldID, val);
		}

		protected void _SetStatic<FieldType>(IntPtr fieldID, FieldType val)
		{
			if (AndroidReflection.IsPrimitive(typeof(FieldType)))
			{
				if (typeof(FieldType) == typeof(int))
				{
					AndroidJNISafe.SetStaticIntField(m_jclass, fieldID, (int)(object)val);
				}
				else if (typeof(FieldType) == typeof(bool))
				{
					AndroidJNISafe.SetStaticBooleanField(m_jclass, fieldID, (bool)(object)val);
				}
				else if (typeof(FieldType) == typeof(byte))
				{
					Debug.LogWarning("Field type <Byte> for Java set field call is obsolete, use field type <SByte> instead");
					AndroidJNISafe.SetStaticSByteField(m_jclass, fieldID, (sbyte)(byte)(object)val);
				}
				else if (typeof(FieldType) == typeof(sbyte))
				{
					AndroidJNISafe.SetStaticSByteField(m_jclass, fieldID, (sbyte)(object)val);
				}
				else if (typeof(FieldType) == typeof(short))
				{
					AndroidJNISafe.SetStaticShortField(m_jclass, fieldID, (short)(object)val);
				}
				else if (typeof(FieldType) == typeof(long))
				{
					AndroidJNISafe.SetStaticLongField(m_jclass, fieldID, (long)(object)val);
				}
				else if (typeof(FieldType) == typeof(float))
				{
					AndroidJNISafe.SetStaticFloatField(m_jclass, fieldID, (float)(object)val);
				}
				else if (typeof(FieldType) == typeof(double))
				{
					AndroidJNISafe.SetStaticDoubleField(m_jclass, fieldID, (double)(object)val);
				}
				else if (typeof(FieldType) == typeof(char))
				{
					AndroidJNISafe.SetStaticCharField(m_jclass, fieldID, (char)(object)val);
				}
			}
			else if (typeof(FieldType) == typeof(string))
			{
				AndroidJNISafe.SetStaticStringField(m_jclass, fieldID, (string)(object)val);
			}
			else if (typeof(FieldType) == typeof(AndroidJavaClass))
			{
				AndroidJNISafe.SetStaticObjectField(m_jclass, fieldID, (val == null) ? IntPtr.Zero : ((IntPtr)((AndroidJavaClass)(object)val).m_jclass));
			}
			else if (typeof(FieldType) == typeof(AndroidJavaObject))
			{
				AndroidJNISafe.SetStaticObjectField(m_jclass, fieldID, (val == null) ? IntPtr.Zero : ((IntPtr)((AndroidJavaObject)(object)val).m_jobject));
			}
			else if (AndroidReflection.IsAssignableFrom(typeof(AndroidJavaProxy), typeof(FieldType)))
			{
				AndroidJNISafe.SetStaticObjectField(m_jclass, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaProxy)(object)val).GetRawProxy());
			}
			else
			{
				if (!AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType)))
				{
					throw new Exception("JNI: Unknown field type '" + typeof(FieldType)?.ToString() + "'");
				}
				IntPtr val2 = AndroidJNIHelper.ConvertToJNIArray((Array)(object)val);
				AndroidJNISafe.SetStaticObjectField(m_jclass, fieldID, val2);
			}
		}

		internal static AndroidJavaObject AndroidJavaObjectDeleteLocalRef(IntPtr jobject)
		{
			try
			{
				return new AndroidJavaObject(jobject);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jobject);
			}
		}

		internal static AndroidJavaClass AndroidJavaClassDeleteLocalRef(IntPtr jclass)
		{
			try
			{
				return new AndroidJavaClass(jclass);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jclass);
			}
		}

		internal static ReturnType FromJavaArrayDeleteLocalRef<ReturnType>(IntPtr jobject)
		{
			if (jobject == IntPtr.Zero)
			{
				return default(ReturnType);
			}
			try
			{
				return (ReturnType)(object)AndroidJNIHelper.ConvertFromJNIArray<ReturnType>(jobject);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jobject);
			}
		}

		protected IntPtr _GetRawObject()
		{
			return (m_jobject == null) ? IntPtr.Zero : ((IntPtr)m_jobject);
		}

		protected IntPtr _GetRawClass()
		{
			return m_jclass;
		}
	}
	public class AndroidJavaClass : AndroidJavaObject
	{
		public AndroidJavaClass(string className)
		{
			_AndroidJavaClass(className);
		}

		private void _AndroidJavaClass(string className)
		{
			DebugPrint("Creating AndroidJavaClass from " + className);
			IntPtr intPtr = AndroidJNISafe.FindClass(className.Replace('.', '/'));
			m_jclass = new GlobalJavaObjectRef(intPtr);
			m_jobject = null;
			AndroidJNISafe.DeleteLocalRef(intPtr);
		}

		internal AndroidJavaClass(IntPtr jclass)
		{
			if (jclass == IntPtr.Zero)
			{
				throw new Exception("JNI: Init'd AndroidJavaClass with null ptr!");
			}
			m_jclass = new GlobalJavaObjectRef(jclass);
			m_jobject = null;
		}
	}
	internal class AndroidReflection
	{
		private const string RELECTION_HELPER_CLASS_NAME = "com/unity3d/player/ReflectionHelper";

		private static readonly GlobalJavaObjectRef s_ReflectionHelperClass = new GlobalJavaObjectRef(AndroidJNISafe.FindClass("com/unity3d/player/ReflectionHelper"));

		private static readonly IntPtr s_ReflectionHelperGetConstructorID = GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getConstructorID", "(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/reflect/Constructor;");

		private static readonly IntPtr s_ReflectionHelperGetMethodID = GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getMethodID", "(Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;Z)Ljava/lang/reflect/Method;");

		private static readonly IntPtr s_ReflectionHelperGetFieldID = GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getFieldID", "(Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;Z)Ljava/lang/reflect/Field;");

		private static readonly IntPtr s_ReflectionHelperGetFieldSignature = GetStaticMethodID("com/unity3d/player/ReflectionHelper", "getFieldSignature", "(Ljava/lang/reflect/Field;)Ljava/lang/String;");

		private static readonly IntPtr s_ReflectionHelperNewProxyInstance = GetStaticMethodID("com/unity3d/player/ReflectionHelper", "newProxyInstance", "(Lcom/unity3d/player/UnityPlayer;JLjava/lang/Class;)Ljava/lang/Object;");

		private static readonly IntPtr s_ReflectionHelperCeateInvocationError = GetStaticMethodID("com/unity3d/player/ReflectionHelper", "createInvocationError", "(JZ)Ljava/lang/Object;");

		private static readonly IntPtr s_FieldGetDeclaringClass = GetMethodID("java/lang/reflect/Field", "getDeclaringClass", "()Ljava/lang/Class;");

		public static bool IsPrimitive(Type t)
		{
			return t.IsPrimitive;
		}

		public static bool IsAssignableFrom(Type t, Type from)
		{
			return t.IsAssignableFrom(from);
		}

		private static IntPtr GetStaticMethodID(string clazz, string methodName, string signature)
		{
			IntPtr intPtr = AndroidJNISafe.FindClass(clazz);
			try
			{
				return AndroidJNISafe.GetStaticMethodID(intPtr, methodName, signature);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
		}

		private static IntPtr GetMethodID(string clazz, string methodName, string signature)
		{
			IntPtr intPtr = AndroidJNISafe.FindClass(clazz);
			try
			{
				return AndroidJNISafe.GetMethodID(intPtr, methodName, signature);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
		}

		public static IntPtr GetConstructorMember(IntPtr jclass, string signature)
		{
			jvalue[] array = new jvalue[2];
			try
			{
				array[0].l = jclass;
				array[1].l = AndroidJNISafe.NewString(signature);
				return AndroidJNISafe.CallStaticObjectMethod(s_ReflectionHelperClass, s_ReflectionHelperGetConstructorID, array);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(array[1].l);
			}
		}

		public static IntPtr GetMethodMember(IntPtr jclass, string methodName, string signature, bool isStatic)
		{
			jvalue[] array = new jvalue[4];
			try
			{
				array[0].l = jclass;
				array[1].l = AndroidJNISafe.NewString(methodName);
				array[2].l = AndroidJNISafe.NewString(signature);
				array[3].z = isStatic;
				return AndroidJNISafe.CallStaticObjectMethod(s_ReflectionHelperClass, s_ReflectionHelperGetMethodID, array);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(array[1].l);
				AndroidJNISafe.DeleteLocalRef(array[2].l);
			}
		}

		public static IntPtr GetFieldMember(IntPtr jclass, string fieldName, string signature, bool isStatic)
		{
			jvalue[] array = new jvalue[4];
			try
			{
				array[0].l = jclass;
				array[1].l = AndroidJNISafe.NewString(fieldName);
				array[2].l = AndroidJNISafe.NewString(signature);
				array[3].z = isStatic;
				return AndroidJNISafe.CallStaticObjectMethod(s_ReflectionHelperClass, s_ReflectionHelperGetFieldID, array);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(array[1].l);
				AndroidJNISafe.DeleteLocalRef(array[2].l);
			}
		}

		public static IntPtr GetFieldClass(IntPtr field)
		{
			return AndroidJNISafe.CallObjectMethod(field, s_FieldGetDeclaringClass, null);
		}

		public static string GetFieldSignature(IntPtr field)
		{
			jvalue[] array = new jvalue[1];
			array[0].l = field;
			return AndroidJNISafe.CallStaticStringMethod(s_ReflectionHelperClass, s_ReflectionHelperGetFieldSignature, array);
		}

		public static IntPtr NewProxyInstance(IntPtr player, IntPtr delegateHandle, IntPtr interfaze)
		{
			jvalue[] array = new jvalue[3];
			array[0].l = player;
			array[1].j = delegateHandle.ToInt64();
			array[2].l = interfaze;
			return AndroidJNISafe.CallStaticObjectMethod(s_ReflectionHelperClass, s_ReflectionHelperNewProxyInstance, array);
		}

		internal static IntPtr CreateInvocationError(Exception ex, bool methodNotFound)
		{
			jvalue[] array = new jvalue[2];
			array[0].j = GCHandle.ToIntPtr(GCHandle.Alloc(ex)).ToInt64();
			array[1].z = methodNotFound;
			return AndroidJNISafe.CallStaticObjectMethod(s_ReflectionHelperClass, s_ReflectionHelperCeateInvocationError, array);
		}
	}
	[UsedByNativeCode]
	internal sealed class _AndroidJNIHelper
	{
		public static IntPtr CreateJavaProxy(IntPtr player, IntPtr delegateHandle, AndroidJavaProxy proxy)
		{
			return AndroidReflection.NewProxyInstance(player, delegateHandle, proxy.javaInterface.GetRawClass());
		}

		public static IntPtr CreateJavaRunnable(AndroidJavaRunnable jrunnable)
		{
			return AndroidJNIHelper.CreateJavaProxy(new AndroidJavaRunnableProxy(jrunnable));
		}

		[RequiredByNativeCode]
		public static IntPtr InvokeJavaProxyMethod(AndroidJavaProxy proxy, IntPtr jmethodName, IntPtr jargs)
		{
			try
			{
				return proxy.Invoke(AndroidJNI.GetStringChars(jmethodName), jargs);
			}
			catch (Exception ex)
			{
				return AndroidReflection.CreateInvocationError(ex, methodNotFound: false);
			}
		}

		public static void CreateJNIArgArray(object[] args, Span<jvalue> ret)
		{
			int num = 0;
			foreach (object obj in args)
			{
				if (obj == null)
				{
					ret[num].l = IntPtr.Zero;
				}
				else if (AndroidReflection.IsPrimitive(obj.GetType()))
				{
					if (obj is int)
					{
						ret[num].i = (int)obj;
					}
					else if (obj is bool)
					{
						ret[num].z = (bool)obj;
					}
					else if (obj is byte)
					{
						Debug.LogWarning("Passing Byte arguments to Java methods is obsolete, pass SByte parameters instead");
						ret[num].b = (sbyte)(byte)obj;
					}
					else if (obj is sbyte)
					{
						ret[num].b = (sbyte)obj;
					}
					else if (obj is short)
					{
						ret[num].s = (short)obj;
					}
					else if (obj is long)
					{
						ret[num].j = (long)obj;
					}
					else if (obj is float)
					{
						ret[num].f = (float)obj;
					}
					else if (obj is double)
					{
						ret[num].d = (double)obj;
					}
					else if (obj is char)
					{
						ret[num].c = (char)obj;
					}
				}
				else if (obj is string)
				{
					ret[num].l = AndroidJNISafe.NewString((string)obj);
				}
				else if (obj is AndroidJavaClass)
				{
					ret[num].l = ((AndroidJavaClass)obj).GetRawClass();
				}
				else if (obj is AndroidJavaObject)
				{
					ret[num].l = ((AndroidJavaObject)obj).GetRawObject();
				}
				else if (obj is Array)
				{
					ret[num].l = ConvertToJNIArray((Array)obj);
				}
				else if (obj is AndroidJavaProxy)
				{
					ret[num].l = ((AndroidJavaProxy)obj).GetRawProxy();
				}
				else
				{
					if (!(obj is AndroidJavaRunnable))
					{
						throw new Exception("JNI; Unknown argument type '" + obj.GetType()?.ToString() + "'");
					}
					ret[num].l = AndroidJNIHelper.CreateJavaRunnable((AndroidJavaRunnable)obj);
				}
				num++;
			}
		}

		public static object UnboxArray(AndroidJavaObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("java/lang/reflect/Array");
			AndroidJavaObject androidJavaObject = obj.Call<AndroidJavaObject>("getClass", Array.Empty<object>());
			AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getComponentType", Array.Empty<object>());
			string text = androidJavaObject2.Call<string>("getName", Array.Empty<object>());
			int num = androidJavaClass.CallStatic<int>("getLength", new object[1] { obj });
			Array array;
			if (!androidJavaObject2.Call<bool>("isPrimitive", Array.Empty<object>()))
			{
				array = (("java.lang.String" == text) ? ((Array)new string[num]) : ((Array)((!("java.lang.Class" == text)) ? new AndroidJavaObject[num] : new AndroidJavaClass[num])));
			}
			else if ("int" == text)
			{
				array = new int[num];
			}
			else if ("boolean" == text)
			{
				array = new bool[num];
			}
			else if ("byte" == text)
			{
				array = new sbyte[num];
			}
			else if ("short" == text)
			{
				array = new short[num];
			}
			else if ("long" == text)
			{
				array = new long[num];
			}
			else if ("float" == text)
			{
				array = new float[num];
			}
			else if ("double" == text)
			{
				array = new double[num];
			}
			else
			{
				if (!("char" == text))
				{
					throw new Exception("JNI; Unknown argument type '" + text + "'");
				}
				array = new char[num];
			}
			for (int i = 0; i < num; i++)
			{
				array.SetValue(Unbox(androidJavaClass.CallStatic<AndroidJavaObject>("get", new object[2] { obj, i })), i);
			}
			androidJavaClass.Dispose();
			return array;
		}

		public static object Unbox(AndroidJavaObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			using AndroidJavaObject androidJavaObject = obj.Call<AndroidJavaObject>("getClass", Array.Empty<object>());
			string text = androidJavaObject.Call<string>("getName", Array.Empty<object>());
			if ("java.lang.Integer" == text)
			{
				return obj.Call<int>("intValue", Array.Empty<object>());
			}
			if ("java.lang.Boolean" == text)
			{
				return obj.Call<bool>("booleanValue", Array.Empty<object>());
			}
			if ("java.lang.Byte" == text)
			{
				return obj.Call<sbyte>("byteValue", Array.Empty<object>());
			}
			if ("java.lang.Short" == text)
			{
				return obj.Call<short>("shortValue", Array.Empty<object>());
			}
			if ("java.lang.Long" == text)
			{
				return obj.Call<long>("longValue", Array.Empty<object>());
			}
			if ("java.lang.Float" == text)
			{
				return obj.Call<float>("floatValue", Array.Empty<object>());
			}
			if ("java.lang.Double" == text)
			{
				return obj.Call<double>("doubleValue", Array.Empty<object>());
			}
			if ("java.lang.Character" == text)
			{
				return obj.Call<char>("charValue", Array.Empty<object>());
			}
			if ("java.lang.String" == text)
			{
				return obj.Call<string>("toString", Array.Empty<object>());
			}
			if ("java.lang.Class" == text)
			{
				return new AndroidJavaClass(obj.GetRawObject());
			}
			if (androidJavaObject.Call<bool>("isArray", Array.Empty<object>()))
			{
				return UnboxArray(obj);
			}
			return obj;
		}

		public static AndroidJavaObject Box(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (AndroidReflection.IsPrimitive(obj.GetType()))
			{
				if (obj is int)
				{
					return new AndroidJavaObject("java.lang.Integer", (int)obj);
				}
				if (obj is bool)
				{
					return new AndroidJavaObject("java.lang.Boolean", (bool)obj);
				}
				if (obj is byte)
				{
					return new AndroidJavaObject("java.lang.Byte", (sbyte)obj);
				}
				if (obj is sbyte)
				{
					return new AndroidJavaObject("java.lang.Byte", (sbyte)obj);
				}
				if (obj is short)
				{
					return new AndroidJavaObject("java.lang.Short", (short)obj);
				}
				if (obj is long)
				{
					return new AndroidJavaObject("java.lang.Long", (long)obj);
				}
				if (obj is float)
				{
					return new AndroidJavaObject("java.lang.Float", (float)obj);
				}
				if (obj is double)
				{
					return new AndroidJavaObject("java.lang.Double", (double)obj);
				}
				if (obj is char)
				{
					return new AndroidJavaObject("java.lang.Character", (char)obj);
				}
				throw new Exception("JNI; Unknown argument type '" + obj.GetType()?.ToString() + "'");
			}
			if (obj is string)
			{
				return new AndroidJavaObject("java.lang.String", (string)obj);
			}
			if (obj is AndroidJavaClass)
			{
				return new AndroidJavaObject(((AndroidJavaClass)obj).GetRawClass());
			}
			if (obj is AndroidJavaObject)
			{
				return (AndroidJavaObject)obj;
			}
			if (obj is Array)
			{
				return AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(ConvertToJNIArray((Array)obj));
			}
			if (obj is AndroidJavaProxy)
			{
				return ((AndroidJavaProxy)obj).GetProxyObject();
			}
			if (obj is AndroidJavaRunnable)
			{
				return AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(AndroidJNIHelper.CreateJavaRunnable((AndroidJavaRunnable)obj));
			}
			throw new Exception("JNI; Unknown argument type '" + obj.GetType()?.ToString() + "'");
		}

		public static void DeleteJNIArgArray(object[] args, Span<jvalue> jniArgs)
		{
			if (args == null)
			{
				return;
			}
			int num = 0;
			foreach (object obj in args)
			{
				if (obj is string || obj is AndroidJavaRunnable || obj is AndroidJavaProxy || obj is Array)
				{
					AndroidJNISafe.DeleteLocalRef(jniArgs[num].l);
				}
				num++;
			}
		}

		public static IntPtr ConvertToJNIArray(Array array)
		{
			Type elementType = array.GetType().GetElementType();
			if (AndroidReflection.IsPrimitive(elementType))
			{
				if (elementType == typeof(int))
				{
					return AndroidJNISafe.ToIntArray((int[])array);
				}
				if (elementType == typeof(bool))
				{
					return AndroidJNISafe.ToBooleanArray((bool[])array);
				}
				if (elementType == typeof(byte))
				{
					Debug.LogWarning("AndroidJNIHelper: converting Byte array is obsolete, use SByte array instead");
					return AndroidJNISafe.ToByteArray((byte[])array);
				}
				if (elementType == typeof(sbyte))
				{
					return AndroidJNISafe.ToSByteArray((sbyte[])array);
				}
				if (elementType == typeof(short))
				{
					return AndroidJNISafe.ToShortArray((short[])array);
				}
				if (elementType == typeof(long))
				{
					return AndroidJNISafe.ToLongArray((long[])array);
				}
				if (elementType == typeof(float))
				{
					return AndroidJNISafe.ToFloatArray((float[])array);
				}
				if (elementType == typeof(double))
				{
					return AndroidJNISafe.ToDoubleArray((double[])array);
				}
				if (elementType == typeof(char))
				{
					return AndroidJNISafe.ToCharArray((char[])array);
				}
				return IntPtr.Zero;
			}
			if (elementType == typeof(string))
			{
				string[] array2 = (string[])array;
				int length = array.GetLength(0);
				IntPtr intPtr = AndroidJNISafe.FindClass("java/lang/String");
				IntPtr intPtr2 = AndroidJNI.NewObjectArray(length, intPtr, IntPtr.Zero);
				for (int i = 0; i < length; i++)
				{
					IntPtr intPtr3 = AndroidJNISafe.NewString(array2[i]);
					AndroidJNI.SetObjectArrayElement(intPtr2, i, intPtr3);
					AndroidJNISafe.DeleteLocalRef(intPtr3);
				}
				AndroidJNISafe.DeleteLocalRef(intPtr);
				return intPtr2;
			}
			if (elementType == typeof(AndroidJavaObject))
			{
				AndroidJavaObject[] array3 = (AndroidJavaObject[])array;
				int length2 = array.GetLength(0);
				IntPtr[] array4 = new IntPtr[length2];
				IntPtr intPtr4 = AndroidJNISafe.FindClass("java/lang/Object");
				IntPtr intPtr5 = IntPtr.Zero;
				for (int j = 0; j < length2; j++)
				{
					if (array3[j] != null)
					{
						array4[j] = array3[j].GetRawObject();
						IntPtr rawClass = array3[j].GetRawClass();
						if (intPtr5 == IntPtr.Zero)
						{
							intPtr5 = rawClass;
						}
						else if (intPtr5 != intPtr4 && !AndroidJNI.IsSameObject(intPtr5, rawClass))
						{
							intPtr5 = intPtr4;
						}
					}
					else
					{
						array4[j] = IntPtr.Zero;
					}
				}
				IntPtr result = AndroidJNISafe.ToObjectArray(array4, intPtr5);
				AndroidJNISafe.DeleteLocalRef(intPtr4);
				return result;
			}
			if (AndroidReflection.IsAssignableFrom(typeof(AndroidJavaProxy), elementType))
			{
				AndroidJavaProxy[] array5 = (AndroidJavaProxy[])array;
				int length3 = array.GetLength(0);
				IntPtr[] array6 = new IntPtr[length3];
				IntPtr intPtr6 = AndroidJNISafe.FindClass("java/lang/Object");
				IntPtr intPtr7 = IntPtr.Zero;
				for (int k = 0; k < length3; k++)
				{
					if (array5[k] != null)
					{
						array6[k] = array5[k].GetRawProxy();
						IntPtr rawClass2 = array5[k].javaInterface.GetRawClass();
						if (intPtr7 == IntPtr.Zero)
						{
							intPtr7 = rawClass2;
						}
						else if (intPtr7 != intPtr6 && !AndroidJNI.IsSameObject(intPtr7, rawClass2))
						{
							intPtr7 = intPtr6;
						}
					}
					else
					{
						array6[k] = IntPtr.Zero;
					}
				}
				IntPtr result2 = AndroidJNISafe.ToObjectArray(array6, intPtr7);
				AndroidJNISafe.DeleteLocalRef(intPtr6);
				return result2;
			}
			throw new Exception("JNI; Unknown array type '" + elementType?.ToString() + "'");
		}

		public static ArrayType ConvertFromJNIArray<ArrayType>(IntPtr array)
		{
			Type elementType = typeof(ArrayType).GetElementType();
			if (AndroidReflection.IsPrimitive(elementType))
			{
				if (elementType == typeof(int))
				{
					return (ArrayType)(object)AndroidJNISafe.FromIntArray(array);
				}
				if (elementType == typeof(bool))
				{
					return (ArrayType)(object)AndroidJNISafe.FromBooleanArray(array);
				}
				if (elementType == typeof(byte))
				{
					Debug.LogWarning("AndroidJNIHelper: converting from Byte array is obsolete, use SByte array instead");
					return (ArrayType)(object)AndroidJNISafe.FromByteArray(array);
				}
				if (elementType == typeof(sbyte))
				{
					return (ArrayType)(object)AndroidJNISafe.FromSByteArray(array);
				}
				if (elementType == typeof(short))
				{
					return (ArrayType)(object)AndroidJNISafe.FromShortArray(array);
				}
				if (elementType == typeof(long))
				{
					return (ArrayType)(object)AndroidJNISafe.FromLongArray(array);
				}
				if (elementType == typeof(float))
				{
					return (ArrayType)(object)AndroidJNISafe.FromFloatArray(array);
				}
				if (elementType == typeof(double))
				{
					return (ArrayType)(object)AndroidJNISafe.FromDoubleArray(array);
				}
				if (elementType == typeof(char))
				{
					return (ArrayType)(object)AndroidJNISafe.FromCharArray(array);
				}
				return default(ArrayType);
			}
			if (elementType == typeof(string))
			{
				int arrayLength = AndroidJNISafe.GetArrayLength(array);
				string[] array2 = new string[arrayLength];
				for (int i = 0; i < arrayLength; i++)
				{
					IntPtr objectArrayElement = AndroidJNI.GetObjectArrayElement(array, i);
					array2[i] = AndroidJNISafe.GetStringChars(objectArrayElement);
					AndroidJNISafe.DeleteLocalRef(objectArrayElement);
				}
				return (ArrayType)(object)array2;
			}
			if (elementType == typeof(AndroidJavaObject))
			{
				int arrayLength2 = AndroidJNISafe.GetArrayLength(array);
				AndroidJavaObject[] array3 = new AndroidJavaObject[arrayLength2];
				for (int j = 0; j < arrayLength2; j++)
				{
					IntPtr objectArrayElement2 = AndroidJNI.GetObjectArrayElement(array, j);
					array3[j] = new AndroidJavaObject(objectArrayElement2);
					AndroidJNISafe.DeleteLocalRef(objectArrayElement2);
				}
				return (ArrayType)(object)array3;
			}
			throw new Exception("JNI: Unknown generic array type '" + elementType?.ToString() + "'");
		}

		public static IntPtr GetConstructorID(IntPtr jclass, object[] args)
		{
			return AndroidJNIHelper.GetConstructorID(jclass, GetSignature(args));
		}

		public static IntPtr GetMethodID(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return AndroidJNIHelper.GetMethodID(jclass, methodName, GetSignature(args), isStatic);
		}

		public static IntPtr GetMethodID<ReturnType>(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return AndroidJNIHelper.GetMethodID(jclass, methodName, GetSignature<ReturnType>(args), isStatic);
		}

		public static IntPtr GetFieldID<ReturnType>(IntPtr jclass, string fieldName, bool isStatic)
		{
			return AndroidJNIHelper.GetFieldID(jclass, fieldName, GetSignature(typeof(ReturnType)), isStatic);
		}

		public static IntPtr GetConstructorID(IntPtr jclass, string signature)
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = AndroidReflection.GetConstructorMember(jclass, signature);
				return AndroidJNISafe.FromReflectedMethod(intPtr);
			}
			catch (Exception ex)
			{
				IntPtr methodID = AndroidJNISafe.GetMethodID(jclass, "<init>", signature);
				if (methodID != IntPtr.Zero)
				{
					return methodID;
				}
				throw ex;
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
		}

		public static IntPtr GetMethodID(IntPtr jclass, string methodName, string signature, bool isStatic)
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = AndroidReflection.GetMethodMember(jclass, methodName, signature, isStatic);
				return AndroidJNISafe.FromReflectedMethod(intPtr);
			}
			catch (Exception ex)
			{
				IntPtr methodIDFallback = GetMethodIDFallback(jclass, methodName, signature, isStatic);
				if (methodIDFallback != IntPtr.Zero)
				{
					return methodIDFallback;
				}
				throw ex;
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
		}

		private static IntPtr GetMethodIDFallback(IntPtr jclass, string methodName, string signature, bool isStatic)
		{
			try
			{
				return isStatic ? AndroidJNISafe.GetStaticMethodID(jclass, methodName, signature) : AndroidJNISafe.GetMethodID(jclass, methodName, signature);
			}
			catch (Exception)
			{
			}
			return IntPtr.Zero;
		}

		public static IntPtr GetFieldID(IntPtr jclass, string fieldName, string signature, bool isStatic)
		{
			IntPtr zero = IntPtr.Zero;
			Exception ex = null;
			AndroidJNI.PushLocalFrame(10);
			try
			{
				IntPtr fieldMember = AndroidReflection.GetFieldMember(jclass, fieldName, signature, isStatic);
				if (!isStatic)
				{
					jclass = AndroidReflection.GetFieldClass(fieldMember);
				}
				signature = AndroidReflection.GetFieldSignature(fieldMember);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			try
			{
				zero = (isStatic ? AndroidJNISafe.GetStaticFieldID(jclass, fieldName, signature) : AndroidJNISafe.GetFieldID(jclass, fieldName, signature));
				if (zero == IntPtr.Zero)
				{
					if (ex != null)
					{
						throw ex;
					}
					throw new Exception($"Field {fieldName} or type signature {signature} not found");
				}
				return zero;
			}
			finally
			{
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
			}
		}

		public static string GetSignature(object obj)
		{
			if (obj == null)
			{
				return "Ljava/lang/Object;";
			}
			Type type = ((obj is Type) ? ((Type)obj) : obj.GetType());
			if (AndroidReflection.IsPrimitive(type))
			{
				if (type.Equals(typeof(int)))
				{
					return "I";
				}
				if (type.Equals(typeof(bool)))
				{
					return "Z";
				}
				if (type.Equals(typeof(byte)))
				{
					Debug.LogWarning("AndroidJNIHelper.GetSignature: using Byte parameters is obsolete, use SByte parameters instead");
					return "B";
				}
				if (type.Equals(typeof(sbyte)))
				{
					return "B";
				}
				if (type.Equals(typeof(short)))
				{
					return "S";
				}
				if (type.Equals(typeof(long)))
				{
					return "J";
				}
				if (type.Equals(typeof(float)))
				{
					return "F";
				}
				if (type.Equals(typeof(double)))
				{
					return "D";
				}
				if (type.Equals(typeof(char)))
				{
					return "C";
				}
				return "";
			}
			if (type.Equals(typeof(string)))
			{
				return "Ljava/lang/String;";
			}
			if (obj is AndroidJavaProxy)
			{
				using (AndroidJavaObject androidJavaObject = new AndroidJavaObject(((AndroidJavaProxy)obj).javaInterface.GetRawClass()))
				{
					return "L" + androidJavaObject.Call<string>("getName", Array.Empty<object>()) + ";";
				}
			}
			if (obj == type && AndroidReflection.IsAssignableFrom(typeof(AndroidJavaProxy), type))
			{
				return "";
			}
			if (type.Equals(typeof(AndroidJavaRunnable)))
			{
				return "Ljava/lang/Runnable;";
			}
			if (type.Equals(typeof(AndroidJavaClass)))
			{
				return "Ljava/lang/Class;";
			}
			if (type.Equals(typeof(AndroidJavaObject)))
			{
				if (obj == type)
				{
					return "Ljava/lang/Object;";
				}
				AndroidJavaObject androidJavaObject2 = (AndroidJavaObject)obj;
				using AndroidJavaObject androidJavaObject3 = androidJavaObject2.Call<AndroidJavaObject>("getClass", Array.Empty<object>());
				return "L" + androidJavaObject3.Call<string>("getName", Array.Empty<object>()) + ";";
			}
			if (AndroidReflection.IsAssignableFrom(typeof(Array), type))
			{
				if (type.GetArrayRank() != 1)
				{
					throw new Exception("JNI: System.Array in n dimensions is not allowed");
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append('[');
				stringBuilder.Append(GetSignature(type.GetElementType()));
				return (stringBuilder.Length > 1) ? stringBuilder.ToString() : "";
			}
			throw new Exception("JNI: Unknown signature for type '" + type?.ToString() + "' (obj = " + obj?.ToString() + ") " + ((type == obj) ? "equal" : "instance"));
		}

		public static string GetSignature(object[] args)
		{
			if (args == null || args.Length == 0)
			{
				return "()V";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			foreach (object obj in args)
			{
				stringBuilder.Append(GetSignature(obj));
			}
			stringBuilder.Append(")V");
			return stringBuilder.ToString();
		}

		public static string GetSignature<ReturnType>(object[] args)
		{
			if (args == null || args.Length == 0)
			{
				return "()" + GetSignature(typeof(ReturnType));
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			foreach (object obj in args)
			{
				stringBuilder.Append(GetSignature(obj));
			}
			stringBuilder.Append(')');
			stringBuilder.Append(GetSignature(typeof(ReturnType)));
			return stringBuilder.ToString();
		}
	}
	[StructLayout(LayoutKind.Explicit)]
	[NativeType(CodegenOptions.Custom, "ScriptingJvalue")]
	public struct jvalue
	{
		[FieldOffset(0)]
		public bool z;

		[FieldOffset(0)]
		public sbyte b;

		[FieldOffset(0)]
		public char c;

		[FieldOffset(0)]
		public short s;

		[FieldOffset(0)]
		public int i;

		[FieldOffset(0)]
		public long j;

		[FieldOffset(0)]
		public float f;

		[FieldOffset(0)]
		public double d;

		[FieldOffset(0)]
		public IntPtr l;
	}
	[NativeType(CodegenOptions.Custom, "ScriptingJNINativeMethod")]
	public struct JNINativeMethod
	{
		public string name;

		public string signature;

		public IntPtr fnPtr;
	}
	[UsedByNativeCode]
	[NativeHeader("Modules/AndroidJNI/Public/AndroidJNIBindingsHelpers.h")]
	[StaticAccessor("AndroidJNIBindingsHelpers", StaticAccessorType.DoubleColon)]
	[NativeConditional("PLATFORM_ANDROID")]
	public static class AndroidJNIHelper
	{
		public static extern bool debug
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static IntPtr GetConstructorID(IntPtr javaClass)
		{
			return GetConstructorID(javaClass, "");
		}

		public static IntPtr GetConstructorID(IntPtr javaClass, [DefaultValue("")] string signature)
		{
			return _AndroidJNIHelper.GetConstructorID(javaClass, signature);
		}

		public static IntPtr GetMethodID(IntPtr javaClass, string methodName)
		{
			return GetMethodID(javaClass, methodName, "", isStatic: false);
		}

		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("")] string signature)
		{
			return GetMethodID(javaClass, methodName, signature, isStatic: false);
		}

		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, isStatic);
		}

		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName)
		{
			return GetFieldID(javaClass, fieldName, "", isStatic: false);
		}

		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("")] string signature)
		{
			return GetFieldID(javaClass, fieldName, signature, isStatic: false);
		}

		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, isStatic);
		}

		public static IntPtr CreateJavaRunnable(AndroidJavaRunnable jrunnable)
		{
			return _AndroidJNIHelper.CreateJavaRunnable(jrunnable);
		}

		public static IntPtr CreateJavaProxy(AndroidJavaProxy proxy)
		{
			GCHandle value = GCHandle.Alloc(proxy);
			try
			{
				return _AndroidJNIHelper.CreateJavaProxy(AndroidApp.UnityPlayerRaw, GCHandle.ToIntPtr(value), proxy);
			}
			catch
			{
				value.Free();
				throw;
			}
		}

		public static IntPtr ConvertToJNIArray(Array array)
		{
			return _AndroidJNIHelper.ConvertToJNIArray(array);
		}

		public static jvalue[] CreateJNIArgArray(object[] args)
		{
			jvalue[] array = new jvalue[args.Length];
			_AndroidJNIHelper.CreateJNIArgArray(args, array);
			return array;
		}

		public static void CreateJNIArgArray(object[] args, Span<jvalue> jniArgs)
		{
			if (args.Length != jniArgs.Length)
			{
				throw new ArgumentException($"Both arrays must be of the same length, but are {args.Length} and {jniArgs.Length}");
			}
			_AndroidJNIHelper.CreateJNIArgArray(args, jniArgs);
		}

		public static void DeleteJNIArgArray(object[] args, jvalue[] jniArgs)
		{
			_AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
		}

		public static void DeleteJNIArgArray(object[] args, Span<jvalue> jniArgs)
		{
			_AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
		}

		public static IntPtr GetConstructorID(IntPtr jclass, object[] args)
		{
			return _AndroidJNIHelper.GetConstructorID(jclass, args);
		}

		public static IntPtr GetMethodID(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(jclass, methodName, args, isStatic);
		}

		public static string GetSignature(object obj)
		{
			return _AndroidJNIHelper.GetSignature(obj);
		}

		public static string GetSignature(object[] args)
		{
			return _AndroidJNIHelper.GetSignature(args);
		}

		public static ArrayType ConvertFromJNIArray<ArrayType>(IntPtr array)
		{
			return _AndroidJNIHelper.ConvertFromJNIArray<ArrayType>(array);
		}

		public static IntPtr GetMethodID<ReturnType>(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID<ReturnType>(jclass, methodName, args, isStatic);
		}

		public static IntPtr GetFieldID<FieldType>(IntPtr jclass, string fieldName, bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID<FieldType>(jclass, fieldName, isStatic);
		}

		public static string GetSignature<ReturnType>(object[] args)
		{
			return _AndroidJNIHelper.GetSignature<ReturnType>(args);
		}

		private unsafe static IntPtr Box(jvalue val, string boxedClass, string signature)
		{
			IntPtr intPtr = AndroidJNISafe.FindClass(boxedClass);
			try
			{
				IntPtr staticMethodID = AndroidJNISafe.GetStaticMethodID(intPtr, "valueOf", signature);
				Span<jvalue> args = new Span<jvalue>(&val, 1);
				return AndroidJNISafe.CallStaticObjectMethod(intPtr, staticMethodID, args);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
		}

		public static IntPtr Box(sbyte value)
		{
			return Box(new jvalue
			{
				b = value
			}, "java/lang/Byte", "(B)Ljava/lang/Byte;");
		}

		public static IntPtr Box(short value)
		{
			return Box(new jvalue
			{
				s = value
			}, "java/lang/Short", "(S)Ljava/lang/Short;");
		}

		public static IntPtr Box(int value)
		{
			return Box(new jvalue
			{
				i = value
			}, "java/lang/Integer", "(I)Ljava/lang/Integer;");
		}

		public static IntPtr Box(long value)
		{
			return Box(new jvalue
			{
				j = value
			}, "java/lang/Long", "(J)Ljava/lang/Long;");
		}

		public static IntPtr Box(float value)
		{
			return Box(new jvalue
			{
				f = value
			}, "java/lang/Float", "(F)Ljava/lang/Float;");
		}

		public static IntPtr Box(double value)
		{
			return Box(new jvalue
			{
				d = value
			}, "java/lang/Double", "(D)Ljava/lang/Double;");
		}

		public static IntPtr Box(char value)
		{
			return Box(new jvalue
			{
				c = value
			}, "java/lang/Character", "(C)Ljava/lang/Character;");
		}

		public static IntPtr Box(bool value)
		{
			return Box(new jvalue
			{
				z = value
			}, "java/lang/Boolean", "(Z)Ljava/lang/Boolean;");
		}

		private static IntPtr GetUnboxMethod(IntPtr obj, string methodName, string signature)
		{
			IntPtr objectClass = AndroidJNISafe.GetObjectClass(obj);
			try
			{
				return AndroidJNISafe.GetMethodID(objectClass, methodName, signature);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(objectClass);
			}
		}

		public static void Unbox(IntPtr obj, out sbyte value)
		{
			IntPtr unboxMethod = GetUnboxMethod(obj, "byteValue", "()B");
			value = AndroidJNISafe.CallSByteMethod(obj, unboxMethod, default(Span<jvalue>));
		}

		public static void Unbox(IntPtr obj, out short value)
		{
			IntPtr unboxMethod = GetUnboxMethod(obj, "shortValue", "()S");
			value = AndroidJNISafe.CallShortMethod(obj, unboxMethod, default(Span<jvalue>));
		}

		public static void Unbox(IntPtr obj, out int value)
		{
			IntPtr unboxMethod = GetUnboxMethod(obj, "intValue", "()I");
			value = AndroidJNISafe.CallIntMethod(obj, unboxMethod, default(Span<jvalue>));
		}

		public static void Unbox(IntPtr obj, out long value)
		{
			IntPtr unboxMethod = GetUnboxMethod(obj, "longValue", "()J");
			value = AndroidJNISafe.CallLongMethod(obj, unboxMethod, default(Span<jvalue>));
		}

		public static void Unbox(IntPtr obj, out float value)
		{
			IntPtr unboxMethod = GetUnboxMethod(obj, "floatValue", "()F");
			value = AndroidJNISafe.CallFloatMethod(obj, unboxMethod, default(Span<jvalue>));
		}

		public static void Unbox(IntPtr obj, out double value)
		{
			IntPtr unboxMethod = GetUnboxMethod(obj, "doubleValue", "()D");
			value = AndroidJNISafe.CallDoubleMethod(obj, unboxMethod, default(Span<jvalue>));
		}

		public static void Unbox(IntPtr obj, out char value)
		{
			IntPtr unboxMethod = GetUnboxMethod(obj, "charValue", "()C");
			value = AndroidJNISafe.CallCharMethod(obj, unboxMethod, default(Span<jvalue>));
		}

		public static void Unbox(IntPtr obj, out bool value)
		{
			IntPtr unboxMethod = GetUnboxMethod(obj, "booleanValue", "()Z");
			value = AndroidJNISafe.CallBooleanMethod(obj, unboxMethod, default(Span<jvalue>));
		}
	}
	[NativeConditional("PLATFORM_ANDROID")]
	[NativeHeader("Modules/AndroidJNI/Public/AndroidJNIBindingsHelpers.h")]
	[StaticAccessor("AndroidJNIBindingsHelpers", StaticAccessorType.DoubleColon)]
	public static class AndroidJNI
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("jni", StaticAccessorType.DoubleColon)]
		[ThreadSafe]
		public static extern IntPtr GetJavaVM();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int AttachCurrentThread();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int DetachCurrentThread();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int GetVersion();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr FindClass(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr FromReflectedMethod(IntPtr refMethod);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr FromReflectedField(IntPtr refField);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr ToReflectedMethod(IntPtr clazz, IntPtr methodID, bool isStatic);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr ToReflectedField(IntPtr clazz, IntPtr fieldID, bool isStatic);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr GetSuperclass(IntPtr clazz);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern bool IsAssignableFrom(IntPtr clazz1, IntPtr clazz2);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int Throw(IntPtr obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int ThrowNew(IntPtr clazz, string message);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr ExceptionOccurred();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void ExceptionDescribe();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void ExceptionClear();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void FatalError(string message);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int PushLocalFrame(int capacity);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr PopLocalFrame(IntPtr ptr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr NewGlobalRef(IntPtr obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void DeleteGlobalRef(IntPtr obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		internal static extern void QueueDeleteGlobalRef(IntPtr obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		internal static extern uint GetQueueGlobalRefsCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr NewWeakGlobalRef(IntPtr obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void DeleteWeakGlobalRef(IntPtr obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr NewLocalRef(IntPtr obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void DeleteLocalRef(IntPtr obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern bool IsSameObject(IntPtr obj1, IntPtr obj2);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int EnsureLocalCapacity(int capacity);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr AllocObject(IntPtr clazz);

		public static IntPtr NewObject(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return NewObject(clazz, methodID, new Span<jvalue>(args));
		}

		public unsafe static IntPtr NewObject(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return NewObjectA(clazz, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern IntPtr NewObjectA(IntPtr clazz, IntPtr methodID, jvalue* args);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr GetObjectClass(IntPtr obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern bool IsInstanceOf(IntPtr obj, IntPtr clazz);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr GetMethodID(IntPtr clazz, string name, string sig);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr GetFieldID(IntPtr clazz, string name, string sig);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr GetStaticMethodID(IntPtr clazz, string name, string sig);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr GetStaticFieldID(IntPtr clazz, string name, string sig);

		public static IntPtr NewString(string chars)
		{
			return NewStringFromStr(chars);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private static extern IntPtr NewStringFromStr(string chars);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr NewString(char[] chars);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr NewStringUTF(string bytes);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern string GetStringChars(IntPtr str);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int GetStringLength(IntPtr str);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int GetStringUTFLength(IntPtr str);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern string GetStringUTFChars(IntPtr str);

		public static string CallStringMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallStringMethod(obj, methodID, new Span<jvalue>(args));
		}

		public unsafe static string CallStringMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallStringMethodUnsafe(obj, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern string CallStringMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		public static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallObjectMethod(obj, methodID, new Span<jvalue>(args));
		}

		public unsafe static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallObjectMethodUnsafe(obj, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern IntPtr CallObjectMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		public static int CallIntMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallIntMethod(obj, methodID, new Span<jvalue>(args));
		}

		public unsafe static int CallIntMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallIntMethodUnsafe(obj, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern int CallIntMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		public static bool CallBooleanMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallBooleanMethod(obj, methodID, new Span<jvalue>(args));
		}

		public unsafe static bool CallBooleanMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallBooleanMethodUnsafe(obj, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern bool CallBooleanMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		public static short CallShortMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallShortMethod(obj, methodID, new Span<jvalue>(args));
		}

		public unsafe static short CallShortMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallShortMethodUnsafe(obj, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern short CallShortMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		[Obsolete("AndroidJNI.CallByteMethod is obsolete. Use AndroidJNI.CallSByteMethod method instead")]
		public static byte CallByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return (byte)CallSByteMethod(obj, methodID, args);
		}

		public static sbyte CallSByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallSByteMethod(obj, methodID, new Span<jvalue>(args));
		}

		public unsafe static sbyte CallSByteMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallSByteMethodUnsafe(obj, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern sbyte CallSByteMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		public static char CallCharMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallCharMethod(obj, methodID, new Span<jvalue>(args));
		}

		public unsafe static char CallCharMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallCharMethodUnsafe(obj, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern char CallCharMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		public static float CallFloatMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallFloatMethod(obj, methodID, new Span<jvalue>(args));
		}

		public unsafe static float CallFloatMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallFloatMethodUnsafe(obj, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern float CallFloatMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		public static double CallDoubleMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallDoubleMethod(obj, methodID, new Span<jvalue>(args));
		}

		public unsafe static double CallDoubleMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallDoubleMethodUnsafe(obj, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern double CallDoubleMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		public static long CallLongMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallLongMethod(obj, methodID, new Span<jvalue>(args));
		}

		public unsafe static long CallLongMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallLongMethodUnsafe(obj, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern long CallLongMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		public static void CallVoidMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			CallVoidMethod(obj, methodID, new Span<jvalue>(args));
		}

		public unsafe static void CallVoidMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				CallVoidMethodUnsafe(obj, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern void CallVoidMethodUnsafe(IntPtr obj, IntPtr methodID, jvalue* args);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern string GetStringField(IntPtr obj, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr GetObjectField(IntPtr obj, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern bool GetBooleanField(IntPtr obj, IntPtr fieldID);

		[Obsolete("AndroidJNI.GetByteField is obsolete. Use AndroidJNI.GetSByteField method instead")]
		public static byte GetByteField(IntPtr obj, IntPtr fieldID)
		{
			return (byte)GetSByteField(obj, fieldID);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern sbyte GetSByteField(IntPtr obj, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern char GetCharField(IntPtr obj, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern short GetShortField(IntPtr obj, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int GetIntField(IntPtr obj, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern long GetLongField(IntPtr obj, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern float GetFloatField(IntPtr obj, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern double GetDoubleField(IntPtr obj, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetStringField(IntPtr obj, IntPtr fieldID, string val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetObjectField(IntPtr obj, IntPtr fieldID, IntPtr val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetBooleanField(IntPtr obj, IntPtr fieldID, bool val);

		[Obsolete("AndroidJNI.SetByteField is obsolete. Use AndroidJNI.SetSByteField method instead")]
		public static void SetByteField(IntPtr obj, IntPtr fieldID, byte val)
		{
			SetSByteField(obj, fieldID, (sbyte)val);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetSByteField(IntPtr obj, IntPtr fieldID, sbyte val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetCharField(IntPtr obj, IntPtr fieldID, char val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetShortField(IntPtr obj, IntPtr fieldID, short val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetIntField(IntPtr obj, IntPtr fieldID, int val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetLongField(IntPtr obj, IntPtr fieldID, long val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetFloatField(IntPtr obj, IntPtr fieldID, float val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetDoubleField(IntPtr obj, IntPtr fieldID, double val);

		public static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticStringMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public unsafe static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallStaticStringMethodUnsafe(clazz, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern string CallStaticStringMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		public static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticObjectMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public unsafe static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallStaticObjectMethodUnsafe(clazz, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern IntPtr CallStaticObjectMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		public static int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticIntMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public unsafe static int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallStaticIntMethodUnsafe(clazz, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern int CallStaticIntMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		public static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticBooleanMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public unsafe static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallStaticBooleanMethodUnsafe(clazz, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern bool CallStaticBooleanMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		public static short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticShortMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public unsafe static short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallStaticShortMethodUnsafe(clazz, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern short CallStaticShortMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		[Obsolete("AndroidJNI.CallStaticByteMethod is obsolete. Use AndroidJNI.CallStaticSByteMethod method instead")]
		public static byte CallStaticByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return (byte)CallStaticSByteMethod(clazz, methodID, args);
		}

		public static sbyte CallStaticSByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticSByteMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public unsafe static sbyte CallStaticSByteMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallStaticSByteMethodUnsafe(clazz, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern sbyte CallStaticSByteMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		public static char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticCharMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public unsafe static char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallStaticCharMethodUnsafe(clazz, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern char CallStaticCharMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		public static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticFloatMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public unsafe static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallStaticFloatMethodUnsafe(clazz, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern float CallStaticFloatMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		public static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticDoubleMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public unsafe static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallStaticDoubleMethodUnsafe(clazz, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern double CallStaticDoubleMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		public static long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticLongMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public unsafe static long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				return CallStaticLongMethodUnsafe(clazz, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern long CallStaticLongMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		public static void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			CallStaticVoidMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public unsafe static void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			fixed (jvalue* args2 = args)
			{
				CallStaticVoidMethodUnsafe(clazz, methodID, args2);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern void CallStaticVoidMethodUnsafe(IntPtr clazz, IntPtr methodID, jvalue* args);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern string GetStaticStringField(IntPtr clazz, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr GetStaticObjectField(IntPtr clazz, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern bool GetStaticBooleanField(IntPtr clazz, IntPtr fieldID);

		[Obsolete("AndroidJNI.GetStaticByteField is obsolete. Use AndroidJNI.GetStaticSByteField method instead")]
		public static byte GetStaticByteField(IntPtr clazz, IntPtr fieldID)
		{
			return (byte)GetStaticSByteField(clazz, fieldID);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern sbyte GetStaticSByteField(IntPtr clazz, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern char GetStaticCharField(IntPtr clazz, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern short GetStaticShortField(IntPtr clazz, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int GetStaticIntField(IntPtr clazz, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern long GetStaticLongField(IntPtr clazz, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern float GetStaticFloatField(IntPtr clazz, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern double GetStaticDoubleField(IntPtr clazz, IntPtr fieldID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetStaticStringField(IntPtr clazz, IntPtr fieldID, string val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetStaticObjectField(IntPtr clazz, IntPtr fieldID, IntPtr val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetStaticBooleanField(IntPtr clazz, IntPtr fieldID, bool val);

		[Obsolete("AndroidJNI.SetStaticByteField is obsolete. Use AndroidJNI.SetStaticSByteField method instead")]
		public static void SetStaticByteField(IntPtr clazz, IntPtr fieldID, byte val)
		{
			SetStaticSByteField(clazz, fieldID, (sbyte)val);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetStaticSByteField(IntPtr clazz, IntPtr fieldID, sbyte val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetStaticCharField(IntPtr clazz, IntPtr fieldID, char val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetStaticShortField(IntPtr clazz, IntPtr fieldID, short val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetStaticIntField(IntPtr clazz, IntPtr fieldID, int val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetStaticLongField(IntPtr clazz, IntPtr fieldID, long val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetStaticFloatField(IntPtr clazz, IntPtr fieldID, float val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetStaticDoubleField(IntPtr clazz, IntPtr fieldID, double val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr ToBooleanArray(bool[] array);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("AndroidJNI.ToByteArray is obsolete. Use AndroidJNI.ToSByteArray method instead")]
		[ThreadSafe]
		public static extern IntPtr ToByteArray(byte[] array);

		public unsafe static IntPtr ToSByteArray(sbyte[] array)
		{
			if (array == null)
			{
				return IntPtr.Zero;
			}
			fixed (sbyte* array2 = array)
			{
				return ToSByteArray(array2, array.Length);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern IntPtr ToSByteArray(sbyte* array, int length);

		public unsafe static IntPtr ToCharArray(char[] array)
		{
			if (array == null)
			{
				return IntPtr.Zero;
			}
			fixed (char* array2 = array)
			{
				return ToCharArray(array2, array.Length);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern IntPtr ToCharArray(char* array, int length);

		public unsafe static IntPtr ToShortArray(short[] array)
		{
			if (array == null)
			{
				return IntPtr.Zero;
			}
			fixed (short* array2 = array)
			{
				return ToShortArray(array2, array.Length);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern IntPtr ToShortArray(short* array, int length);

		public unsafe static IntPtr ToIntArray(int[] array)
		{
			if (array == null)
			{
				return IntPtr.Zero;
			}
			fixed (int* array2 = array)
			{
				return ToIntArray(array2, array.Length);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern IntPtr ToIntArray(int* array, int length);

		public unsafe static IntPtr ToLongArray(long[] array)
		{
			if (array == null)
			{
				return IntPtr.Zero;
			}
			fixed (long* array2 = array)
			{
				return ToLongArray(array2, array.Length);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern IntPtr ToLongArray(long* array, int length);

		public unsafe static IntPtr ToFloatArray(float[] array)
		{
			if (array == null)
			{
				return IntPtr.Zero;
			}
			fixed (float* array2 = array)
			{
				return ToFloatArray(array2, array.Length);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern IntPtr ToFloatArray(float* array, int length);

		public unsafe static IntPtr ToDoubleArray(double[] array)
		{
			if (array == null)
			{
				return IntPtr.Zero;
			}
			fixed (double* array2 = array)
			{
				return ToDoubleArray(array2, array.Length);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern IntPtr ToDoubleArray(double* array, int length);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern IntPtr ToObjectArray(IntPtr* array, int length, IntPtr arrayClass);

		public unsafe static IntPtr ToObjectArray(IntPtr[] array, IntPtr arrayClass)
		{
			if (array == null)
			{
				return IntPtr.Zero;
			}
			fixed (IntPtr* array2 = array)
			{
				return ToObjectArray(array2, array.Length, arrayClass);
			}
		}

		public static IntPtr ToObjectArray(IntPtr[] array)
		{
			return ToObjectArray(array, IntPtr.Zero);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern bool[] FromBooleanArray(IntPtr array);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		[Obsolete("AndroidJNI.FromByteArray is obsolete. Use AndroidJNI.FromSByteArray method instead")]
		public static extern byte[] FromByteArray(IntPtr array);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern sbyte[] FromSByteArray(IntPtr array);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern char[] FromCharArray(IntPtr array);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern short[] FromShortArray(IntPtr array);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int[] FromIntArray(IntPtr array);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern long[] FromLongArray(IntPtr array);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern float[] FromFloatArray(IntPtr array);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern double[] FromDoubleArray(IntPtr array);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr[] FromObjectArray(IntPtr array);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int GetArrayLength(IntPtr array);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr NewBooleanArray(int size);

		[Obsolete("AndroidJNI.NewByteArray is obsolete. Use AndroidJNI.NewSByteArray method instead")]
		public static IntPtr NewByteArray(int size)
		{
			return NewSByteArray(size);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr NewSByteArray(int size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr NewCharArray(int size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr NewShortArray(int size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr NewIntArray(int size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr NewLongArray(int size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr NewFloatArray(int size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr NewDoubleArray(int size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr NewObjectArray(int size, IntPtr clazz, IntPtr obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern bool GetBooleanArrayElement(IntPtr array, int index);

		[Obsolete("AndroidJNI.GetByteArrayElement is obsolete. Use AndroidJNI.GetSByteArrayElement method instead")]
		public static byte GetByteArrayElement(IntPtr array, int index)
		{
			return (byte)GetSByteArrayElement(array, index);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern sbyte GetSByteArrayElement(IntPtr array, int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern char GetCharArrayElement(IntPtr array, int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern short GetShortArrayElement(IntPtr array, int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int GetIntArrayElement(IntPtr array, int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern long GetLongArrayElement(IntPtr array, int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern float GetFloatArrayElement(IntPtr array, int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern double GetDoubleArrayElement(IntPtr array, int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern IntPtr GetObjectArrayElement(IntPtr array, int index);

		[Obsolete("AndroidJNI.SetBooleanArrayElement(IntPtr, int, byte) is obsolete. Use AndroidJNI.SetBooleanArrayElement(IntPtr, int, bool) method instead")]
		public static void SetBooleanArrayElement(IntPtr array, int index, byte val)
		{
			SetBooleanArrayElement(array, index, val != 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetBooleanArrayElement(IntPtr array, int index, bool val);

		[Obsolete("AndroidJNI.SetByteArrayElement is obsolete. Use AndroidJNI.SetSByteArrayElement method instead")]
		public static void SetByteArrayElement(IntPtr array, int index, sbyte val)
		{
			SetSByteArrayElement(array, index, val);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetSByteArrayElement(IntPtr array, int index, sbyte val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetCharArrayElement(IntPtr array, int index, char val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetShortArrayElement(IntPtr array, int index, short val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetIntArrayElement(IntPtr array, int index, int val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetLongArrayElement(IntPtr array, int index, long val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetFloatArrayElement(IntPtr array, int index, float val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetDoubleArrayElement(IntPtr array, int index, double val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void SetObjectArrayElement(IntPtr array, int index, IntPtr obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern IntPtr NewDirectByteBuffer(byte* buffer, long capacity);

		public static IntPtr NewDirectByteBuffer(NativeArray<byte> buffer)
		{
			return NewDirectByteBufferFromNativeArray(buffer);
		}

		public static IntPtr NewDirectByteBuffer(NativeArray<sbyte> buffer)
		{
			return NewDirectByteBufferFromNativeArray(buffer);
		}

		private unsafe static IntPtr NewDirectByteBufferFromNativeArray<T>(NativeArray<T> buffer) where T : struct
		{
			if (!buffer.IsCreated || buffer.Length <= 0)
			{
				return IntPtr.Zero;
			}
			return NewDirectByteBuffer((byte*)buffer.GetUnsafePtr(), buffer.Length);
		}

		public unsafe static sbyte* GetDirectBufferAddress(IntPtr buffer)
		{
			return null;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern long GetDirectBufferCapacity(IntPtr buffer);

		private unsafe static NativeArray<T> GetDirectBuffer<T>(IntPtr buffer) where T : struct
		{
			if (buffer == IntPtr.Zero)
			{
				return default(NativeArray<T>);
			}
			sbyte* directBufferAddress = GetDirectBufferAddress(buffer);
			if (directBufferAddress == null)
			{
				return default(NativeArray<T>);
			}
			long directBufferCapacity = GetDirectBufferCapacity(buffer);
			if (directBufferCapacity > int.MaxValue)
			{
				throw new Exception($"Direct buffer is too large ({directBufferCapacity}) for NativeArray (max {int.MaxValue})");
			}
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(directBufferAddress, (int)directBufferCapacity, Allocator.None);
		}

		public static NativeArray<byte> GetDirectByteBuffer(IntPtr buffer)
		{
			return GetDirectBuffer<byte>(buffer);
		}

		public static NativeArray<sbyte> GetDirectSByteBuffer(IntPtr buffer)
		{
			return GetDirectBuffer<sbyte>(buffer);
		}

		public static int RegisterNatives(IntPtr clazz, JNINativeMethod[] methods)
		{
			if (methods == null || methods.Length == 0)
			{
				return -1;
			}
			for (int i = 0; i < methods.Length; i++)
			{
				JNINativeMethod jNINativeMethod = methods[i];
				if (string.IsNullOrEmpty(jNINativeMethod.name) || (string.IsNullOrEmpty(jNINativeMethod.signature) ? true : false))
				{
					return -1;
				}
			}
			IntPtr natives = RegisterNativesAllocate(methods.Length);
			for (int j = 0; j < methods.Length; j++)
			{
				RegisterNativesSet(natives, j, methods[j].name, methods[j].signature, methods[j].fnPtr);
			}
			return RegisterNativesAndFree(clazz, natives, methods.Length);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private static extern IntPtr RegisterNativesAllocate(int length);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private static extern void RegisterNativesSet(IntPtr natives, int idx, string name, string signature, IntPtr fnPtr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private static extern int RegisterNativesAndFree(IntPtr clazz, IntPtr natives, int n);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int UnregisterNatives(IntPtr clazz);
	}
	internal class AndroidJNISafe
	{
		public static void CheckException()
		{
			IntPtr intPtr = AndroidJNI.ExceptionOccurred();
			if (intPtr != IntPtr.Zero)
			{
				AndroidJNI.ExceptionClear();
				IntPtr intPtr2 = AndroidJNI.FindClass("java/lang/Throwable");
				IntPtr intPtr3 = AndroidJNI.FindClass("android/util/Log");
				try
				{
					IntPtr methodID = AndroidJNI.GetMethodID(intPtr2, "toString", "()Ljava/lang/String;");
					IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(intPtr3, "getStackTraceString", "(Ljava/lang/Throwable;)Ljava/lang/String;");
					string message = AndroidJNI.CallStringMethod(intPtr, methodID, new jvalue[0]);
					jvalue[] array = new jvalue[1];
					array[0].l = intPtr;
					string javaStackTrace = AndroidJNI.CallStaticStringMethod(intPtr3, staticMethodID, array);
					throw new AndroidJavaException(message, javaStackTrace);
				}
				finally
				{
					DeleteLocalRef(intPtr);
					DeleteLocalRef(intPtr2);
					DeleteLocalRef(intPtr3);
				}
			}
		}

		public static void DeleteGlobalRef(IntPtr globalref)
		{
			if (globalref != IntPtr.Zero)
			{
				AndroidJNI.DeleteGlobalRef(globalref);
			}
		}

		public static void QueueDeleteGlobalRef(IntPtr globalref)
		{
			if (globalref != IntPtr.Zero)
			{
				AndroidJNI.QueueDeleteGlobalRef(globalref);
			}
		}

		public static void DeleteWeakGlobalRef(IntPtr globalref)
		{
			if (globalref != IntPtr.Zero)
			{
				AndroidJNI.DeleteWeakGlobalRef(globalref);
			}
		}

		public static void DeleteLocalRef(IntPtr localref)
		{
			if (localref != IntPtr.Zero)
			{
				AndroidJNI.DeleteLocalRef(localref);
			}
		}

		public static IntPtr NewString(string chars)
		{
			try
			{
				return AndroidJNI.NewString(chars);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr NewStringUTF(string bytes)
		{
			try
			{
				return AndroidJNI.NewStringUTF(bytes);
			}
			finally
			{
				CheckException();
			}
		}

		public static string GetStringChars(IntPtr str)
		{
			try
			{
				return AndroidJNI.GetStringChars(str);
			}
			finally
			{
				CheckException();
			}
		}

		public static string GetStringUTFChars(IntPtr str)
		{
			try
			{
				return AndroidJNI.GetStringUTFChars(str);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr GetObjectClass(IntPtr ptr)
		{
			try
			{
				return AndroidJNI.GetObjectClass(ptr);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr GetStaticMethodID(IntPtr clazz, string name, string sig)
		{
			try
			{
				return AndroidJNI.GetStaticMethodID(clazz, name, sig);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr GetMethodID(IntPtr obj, string name, string sig)
		{
			try
			{
				return AndroidJNI.GetMethodID(obj, name, sig);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr GetFieldID(IntPtr clazz, string name, string sig)
		{
			try
			{
				return AndroidJNI.GetFieldID(clazz, name, sig);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr GetStaticFieldID(IntPtr clazz, string name, string sig)
		{
			try
			{
				return AndroidJNI.GetStaticFieldID(clazz, name, sig);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr FromReflectedMethod(IntPtr refMethod)
		{
			try
			{
				return AndroidJNI.FromReflectedMethod(refMethod);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr FromReflectedField(IntPtr refField)
		{
			try
			{
				return AndroidJNI.FromReflectedField(refField);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr FindClass(string name)
		{
			try
			{
				return AndroidJNI.FindClass(name);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr NewObject(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return NewObject(clazz, methodID, new Span<jvalue>(args));
		}

		public static IntPtr NewObject(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.NewObject(clazz, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetStaticObjectField(IntPtr clazz, IntPtr fieldID, IntPtr val)
		{
			try
			{
				AndroidJNI.SetStaticObjectField(clazz, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetStaticStringField(IntPtr clazz, IntPtr fieldID, string val)
		{
			try
			{
				AndroidJNI.SetStaticStringField(clazz, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetStaticCharField(IntPtr clazz, IntPtr fieldID, char val)
		{
			try
			{
				AndroidJNI.SetStaticCharField(clazz, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetStaticDoubleField(IntPtr clazz, IntPtr fieldID, double val)
		{
			try
			{
				AndroidJNI.SetStaticDoubleField(clazz, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetStaticFloatField(IntPtr clazz, IntPtr fieldID, float val)
		{
			try
			{
				AndroidJNI.SetStaticFloatField(clazz, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetStaticLongField(IntPtr clazz, IntPtr fieldID, long val)
		{
			try
			{
				AndroidJNI.SetStaticLongField(clazz, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetStaticShortField(IntPtr clazz, IntPtr fieldID, short val)
		{
			try
			{
				AndroidJNI.SetStaticShortField(clazz, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetStaticSByteField(IntPtr clazz, IntPtr fieldID, sbyte val)
		{
			try
			{
				AndroidJNI.SetStaticSByteField(clazz, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetStaticBooleanField(IntPtr clazz, IntPtr fieldID, bool val)
		{
			try
			{
				AndroidJNI.SetStaticBooleanField(clazz, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetStaticIntField(IntPtr clazz, IntPtr fieldID, int val)
		{
			try
			{
				AndroidJNI.SetStaticIntField(clazz, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr GetStaticObjectField(IntPtr clazz, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetStaticObjectField(clazz, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static string GetStaticStringField(IntPtr clazz, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetStaticStringField(clazz, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static char GetStaticCharField(IntPtr clazz, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetStaticCharField(clazz, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static double GetStaticDoubleField(IntPtr clazz, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetStaticDoubleField(clazz, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static float GetStaticFloatField(IntPtr clazz, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetStaticFloatField(clazz, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static long GetStaticLongField(IntPtr clazz, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetStaticLongField(clazz, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static short GetStaticShortField(IntPtr clazz, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetStaticShortField(clazz, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static sbyte GetStaticSByteField(IntPtr clazz, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetStaticSByteField(clazz, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static bool GetStaticBooleanField(IntPtr clazz, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetStaticBooleanField(clazz, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static int GetStaticIntField(IntPtr clazz, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetStaticIntField(clazz, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			CallStaticVoidMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public static void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				AndroidJNI.CallStaticVoidMethod(clazz, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticObjectMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallStaticObjectMethod(clazz, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticStringMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallStaticStringMethod(clazz, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticCharMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public static char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallStaticCharMethod(clazz, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticDoubleMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallStaticDoubleMethod(clazz, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticFloatMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallStaticFloatMethod(clazz, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticLongMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public static long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallStaticLongMethod(clazz, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticShortMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public static short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallStaticShortMethod(clazz, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static sbyte CallStaticSByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticSByteMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public static sbyte CallStaticSByteMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallStaticSByteMethod(clazz, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticBooleanMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallStaticBooleanMethod(clazz, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return CallStaticIntMethod(clazz, methodID, new Span<jvalue>(args));
		}

		public static int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallStaticIntMethod(clazz, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetObjectField(IntPtr obj, IntPtr fieldID, IntPtr val)
		{
			try
			{
				AndroidJNI.SetObjectField(obj, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetStringField(IntPtr obj, IntPtr fieldID, string val)
		{
			try
			{
				AndroidJNI.SetStringField(obj, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetCharField(IntPtr obj, IntPtr fieldID, char val)
		{
			try
			{
				AndroidJNI.SetCharField(obj, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetDoubleField(IntPtr obj, IntPtr fieldID, double val)
		{
			try
			{
				AndroidJNI.SetDoubleField(obj, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetFloatField(IntPtr obj, IntPtr fieldID, float val)
		{
			try
			{
				AndroidJNI.SetFloatField(obj, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetLongField(IntPtr obj, IntPtr fieldID, long val)
		{
			try
			{
				AndroidJNI.SetLongField(obj, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetShortField(IntPtr obj, IntPtr fieldID, short val)
		{
			try
			{
				AndroidJNI.SetShortField(obj, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetSByteField(IntPtr obj, IntPtr fieldID, sbyte val)
		{
			try
			{
				AndroidJNI.SetSByteField(obj, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetBooleanField(IntPtr obj, IntPtr fieldID, bool val)
		{
			try
			{
				AndroidJNI.SetBooleanField(obj, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static void SetIntField(IntPtr obj, IntPtr fieldID, int val)
		{
			try
			{
				AndroidJNI.SetIntField(obj, fieldID, val);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr GetObjectField(IntPtr obj, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetObjectField(obj, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static string GetStringField(IntPtr obj, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetStringField(obj, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static char GetCharField(IntPtr obj, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetCharField(obj, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static double GetDoubleField(IntPtr obj, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetDoubleField(obj, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static float GetFloatField(IntPtr obj, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetFloatField(obj, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static long GetLongField(IntPtr obj, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetLongField(obj, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static short GetShortField(IntPtr obj, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetShortField(obj, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static sbyte GetSByteField(IntPtr obj, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetSByteField(obj, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static bool GetBooleanField(IntPtr obj, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetBooleanField(obj, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static int GetIntField(IntPtr obj, IntPtr fieldID)
		{
			try
			{
				return AndroidJNI.GetIntField(obj, fieldID);
			}
			finally
			{
				CheckException();
			}
		}

		public static void CallVoidMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			CallVoidMethod(obj, methodID, new Span<jvalue>(args));
		}

		public static void CallVoidMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				AndroidJNI.CallVoidMethod(obj, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallObjectMethod(obj, methodID, new Span<jvalue>(args));
		}

		public static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallObjectMethod(obj, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static string CallStringMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallStringMethod(obj, methodID, new Span<jvalue>(args));
		}

		public static string CallStringMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallStringMethod(obj, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static char CallCharMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallCharMethod(obj, methodID, new Span<jvalue>(args));
		}

		public static char CallCharMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallCharMethod(obj, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static double CallDoubleMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallDoubleMethod(obj, methodID, new Span<jvalue>(args));
		}

		public static double CallDoubleMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallDoubleMethod(obj, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static float CallFloatMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallFloatMethod(obj, methodID, new Span<jvalue>(args));
		}

		public static float CallFloatMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallFloatMethod(obj, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static long CallLongMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallLongMethod(obj, methodID, new Span<jvalue>(args));
		}

		public static long CallLongMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallLongMethod(obj, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static short CallShortMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallShortMethod(obj, methodID, new Span<jvalue>(args));
		}

		public static short CallShortMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallShortMethod(obj, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static sbyte CallSByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallSByteMethod(obj, methodID, new Span<jvalue>(args));
		}

		public static sbyte CallSByteMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallSByteMethod(obj, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static bool CallBooleanMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallBooleanMethod(obj, methodID, new Span<jvalue>(args));
		}

		public static bool CallBooleanMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallBooleanMethod(obj, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static int CallIntMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return CallIntMethod(obj, methodID, new Span<jvalue>(args));
		}

		public static int CallIntMethod(IntPtr obj, IntPtr methodID, Span<jvalue> args)
		{
			try
			{
				return AndroidJNI.CallIntMethod(obj, methodID, args);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr[] FromObjectArray(IntPtr array)
		{
			try
			{
				return AndroidJNI.FromObjectArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static char[] FromCharArray(IntPtr array)
		{
			try
			{
				return AndroidJNI.FromCharArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static double[] FromDoubleArray(IntPtr array)
		{
			try
			{
				return AndroidJNI.FromDoubleArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static float[] FromFloatArray(IntPtr array)
		{
			try
			{
				return AndroidJNI.FromFloatArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static long[] FromLongArray(IntPtr array)
		{
			try
			{
				return AndroidJNI.FromLongArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static short[] FromShortArray(IntPtr array)
		{
			try
			{
				return AndroidJNI.FromShortArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static byte[] FromByteArray(IntPtr array)
		{
			try
			{
				return AndroidJNI.FromByteArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static sbyte[] FromSByteArray(IntPtr array)
		{
			try
			{
				return AndroidJNI.FromSByteArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static bool[] FromBooleanArray(IntPtr array)
		{
			try
			{
				return AndroidJNI.FromBooleanArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static int[] FromIntArray(IntPtr array)
		{
			try
			{
				return AndroidJNI.FromIntArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr ToObjectArray(IntPtr[] array)
		{
			try
			{
				return AndroidJNI.ToObjectArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr ToObjectArray(IntPtr[] array, IntPtr type)
		{
			try
			{
				return AndroidJNI.ToObjectArray(array, type);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr ToCharArray(char[] array)
		{
			try
			{
				return AndroidJNI.ToCharArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr ToDoubleArray(double[] array)
		{
			try
			{
				return AndroidJNI.ToDoubleArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr ToFloatArray(float[] array)
		{
			try
			{
				return AndroidJNI.ToFloatArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr ToLongArray(long[] array)
		{
			try
			{
				return AndroidJNI.ToLongArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr ToShortArray(short[] array)
		{
			try
			{
				return AndroidJNI.ToShortArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr ToByteArray(byte[] array)
		{
			try
			{
				return AndroidJNI.ToByteArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr ToSByteArray(sbyte[] array)
		{
			try
			{
				return AndroidJNI.ToSByteArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr ToBooleanArray(bool[] array)
		{
			try
			{
				return AndroidJNI.ToBooleanArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr ToIntArray(int[] array)
		{
			try
			{
				return AndroidJNI.ToIntArray(array);
			}
			finally
			{
				CheckException();
			}
		}

		public static IntPtr GetObjectArrayElement(IntPtr array, int index)
		{
			try
			{
				return AndroidJNI.GetObjectArrayElement(array, index);
			}
			finally
			{
				CheckException();
			}
		}

		public static int GetArrayLength(IntPtr array)
		{
			try
			{
				return AndroidJNI.GetArrayLength(array);
			}
			finally
			{
				CheckException();
			}
		}
	}
}
namespace UnityEngine.Android
{
	[NativeConditional("PLATFORM_ANDROID")]
	[StaticAccessor("AndroidApp", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/AndroidJNI/Public/AndroidApp.bindings.h")]
	internal static class AndroidApp
	{
		private static AndroidJavaObject m_Context;

		private static AndroidJavaObject m_Activity;

		private static AndroidJavaObject m_UnityPlayer;

		public static AndroidJavaObject Context
		{
			get
			{
				AcquireContextAndActivity();
				return m_Context;
			}
		}

		public static AndroidJavaObject Activity
		{
			get
			{
				AcquireContextAndActivity();
				return m_Activity;
			}
		}

		public static extern IntPtr UnityPlayerRaw
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[ThreadSafe]
			get;
		}

		public static AndroidJavaObject UnityPlayer
		{
			get
			{
				if (m_UnityPlayer != null)
				{
					return m_UnityPlayer;
				}
				m_UnityPlayer = new AndroidJavaObject(UnityPlayerRaw);
				return m_UnityPlayer;
			}
		}

		private static void AcquireContextAndActivity()
		{
			if (m_Context != null)
			{
				return;
			}
			using AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			m_Context = androidJavaClass.GetStatic<AndroidJavaObject>("currentContext");
			m_Activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		}
	}
	public enum AndroidAssetPackStatus
	{
		Unknown,
		Pending,
		Downloading,
		Transferring,
		Completed,
		Failed,
		Canceled,
		WaitingForWifi,
		NotInstalled
	}
	public enum AndroidAssetPackError
	{
		NoError = 0,
		AppUnavailable = -1,
		PackUnavailable = -2,
		InvalidRequest = -3,
		DownloadNotFound = -4,
		ApiNotAvailable = -5,
		NetworkError = -6,
		AccessDenied = -7,
		InsufficientStorage = -10,
		PlayStoreNotFound = -11,
		NetworkUnrestricted = -12,
		AppNotOwned = -13,
		InternalError = -100
	}
	public class AndroidAssetPackInfo
	{
		public string name { get; }

		public AndroidAssetPackStatus status { get; }

		public ulong size { get; }

		public ulong bytesDownloaded { get; }

		public float transferProgress { get; }

		public AndroidAssetPackError error { get; }

		internal AndroidAssetPackInfo(string name, AndroidAssetPackStatus status, ulong size, ulong bytesDownloaded, float transferProgress, AndroidAssetPackError error)
		{
			this.name = name;
			this.status = status;
			this.size = size;
			this.bytesDownloaded = bytesDownloaded;
			this.transferProgress = transferProgress;
			this.error = error;
		}
	}
	public class AndroidAssetPackState
	{
		public string name { get; }

		public AndroidAssetPackStatus status { get; }

		public AndroidAssetPackError error { get; }

		internal AndroidAssetPackState(string name, AndroidAssetPackStatus status, AndroidAssetPackError error)
		{
			this.name = name;
			this.status = status;
			this.error = error;
		}
	}
	public class AndroidAssetPackUseMobileDataRequestResult
	{
		public bool allowed { get; }

		internal AndroidAssetPackUseMobileDataRequestResult(bool allowed)
		{
			this.allowed = allowed;
		}
	}
	public class DownloadAssetPackAsyncOperation : CustomYieldInstruction
	{
		private Dictionary<string, AndroidAssetPackInfo> m_AssetPackInfos;

		public override bool keepWaiting
		{
			get
			{
				lock (m_AssetPackInfos)
				{
					foreach (AndroidAssetPackInfo value in m_AssetPackInfos.Values)
					{
						if (value == null)
						{
							return true;
						}
						if (value.status != AndroidAssetPackStatus.Canceled && value.status != AndroidAssetPackStatus.Completed && value.status != AndroidAssetPackStatus.Failed && value.status != AndroidAssetPackStatus.Unknown)
						{
							return true;
						}
					}
					return false;
				}
			}
		}

		public bool isDone => !keepWaiting;

		public float progress
		{
			get
			{
				lock (m_AssetPackInfos)
				{
					float num = 0f;
					float num2 = 0f;
					foreach (AndroidAssetPackInfo value in m_AssetPackInfos.Values)
					{
						if (value != null)
						{
							if (value.status == AndroidAssetPackStatus.Canceled || value.status == AndroidAssetPackStatus.Completed || value.status == AndroidAssetPackStatus.Failed || value.status == AndroidAssetPackStatus.Unknown)
							{
								num += 1f;
								num2 += 1f;
							}
							else
							{
								double num3 = (double)value.bytesDownloaded / (double)value.size;
								num += (float)num3;
								num2 += value.transferProgress;
							}
						}
					}
					return Mathf.Clamp((num * 0.8f + num2 * 0.2f) / (float)m_AssetPackInfos.Count, 0f, 1f);
				}
			}
		}

		public string[] downloadedAssetPacks
		{
			get
			{
				lock (m_AssetPackInfos)
				{
					List<string> list = new List<string>();
					foreach (AndroidAssetPackInfo value in m_AssetPackInfos.Values)
					{
						if (value != null && value.status == AndroidAssetPackStatus.Completed)
						{
							list.Add(value.name);
						}
					}
					return list.ToArray();
				}
			}
		}

		public string[] downloadFailedAssetPacks
		{
			get
			{
				lock (m_AssetPackInfos)
				{
					List<string> list = new List<string>();
					foreach (KeyValuePair<string, AndroidAssetPackInfo> assetPackInfo in m_AssetPackInfos)
					{
						AndroidAssetPackInfo value = assetPackInfo.Value;
						if (value == null)
						{
							list.Add(assetPackInfo.Key);
						}
						else if (value.status == AndroidAssetPackStatus.Canceled || value.status == AndroidAssetPackStatus.Failed || value.status == AndroidAssetPackStatus.Unknown)
						{
							list.Add(value.name);
						}
					}
					return list.ToArray();
				}
			}
		}

		internal DownloadAssetPackAsyncOperation(string[] assetPackNames)
		{
			m_AssetPackInfos = assetPackNames.ToDictionary((string name) => name, (string name) => (AndroidAssetPackInfo)null);
		}

		internal void OnUpdate(AndroidAssetPackInfo info)
		{
			lock (m_AssetPackInfos)
			{
				m_AssetPackInfos[info.name] = info;
			}
		}
	}
	public class GetAssetPackStateAsyncOperation : CustomYieldInstruction
	{
		private ulong m_Size;

		private AndroidAssetPackState[] m_States;

		private readonly object m_OperationLock;

		public override bool keepWaiting
		{
			get
			{
				lock (m_OperationLock)
				{
					return m_States == null;
				}
			}
		}

		public bool isDone => !keepWaiting;

		public ulong size
		{
			get
			{
				lock (m_OperationLock)
				{
					return m_Size;
				}
			}
		}

		public AndroidAssetPackState[] states
		{
			get
			{
				lock (m_OperationLock)
				{
					return m_States;
				}
			}
		}

		internal GetAssetPackStateAsyncOperation()
		{
			m_OperationLock = new object();
		}

		internal void OnResult(ulong size, AndroidAssetPackState[] states)
		{
			lock (m_OperationLock)
			{
				m_Size = size;
				m_States = states;
			}
		}
	}
	public class RequestToUseMobileDataAsyncOperation : CustomYieldInstruction
	{
		private AndroidAssetPackUseMobileDataRequestResult m_RequestResult;

		private readonly object m_OperationLock;

		public override bool keepWaiting
		{
			get
			{
				lock (m_OperationLock)
				{
					return m_RequestResult == null;
				}
			}
		}

		public bool isDone => !keepWaiting;

		public AndroidAssetPackUseMobileDataRequestResult result
		{
			get
			{
				lock (m_OperationLock)
				{
					return m_RequestResult;
				}
			}
		}

		internal RequestToUseMobileDataAsyncOperation()
		{
			m_OperationLock = new object();
		}

		internal void OnResult(AndroidAssetPackUseMobileDataRequestResult result)
		{
			lock (m_OperationLock)
			{
				m_RequestResult = result;
			}
		}
	}
	[NativeHeader("Modules/AndroidJNI/Public/AndroidAssetPacksBindingsHelpers.h")]
	[StaticAccessor("AndroidAssetPacksBindingsHelpers", StaticAccessorType.DoubleColon)]
	public static class AndroidAssetPacks
	{
		public static bool coreUnityAssetPacksDownloaded => CoreUnityAssetPacksDownloaded();

		internal static string dataPackName => GetDataPackName();

		internal static string streamingAssetsPackName => GetStreamingAssetsPackName();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("PLATFORM_ANDROID")]
		private static extern bool CoreUnityAssetPacksDownloaded();

		public static string[] GetCoreUnityAssetPackNames()
		{
			return new string[0];
		}

		public static void GetAssetPackStateAsync(string[] assetPackNames, Action<ulong, AndroidAssetPackState[]> callback)
		{
		}

		public static GetAssetPackStateAsyncOperation GetAssetPackStateAsync(string[] assetPackNames)
		{
			return null;
		}

		public static void DownloadAssetPackAsync(string[] assetPackNames, Action<AndroidAssetPackInfo> callback)
		{
		}

		public static DownloadAssetPackAsyncOperation DownloadAssetPackAsync(string[] assetPackNames)
		{
			return null;
		}

		public static void RequestToUseMobileDataAsync(Action<AndroidAssetPackUseMobileDataRequestResult> callback)
		{
		}

		public static RequestToUseMobileDataAsyncOperation RequestToUseMobileDataAsync()
		{
			return null;
		}

		public static string GetAssetPackPath(string assetPackName)
		{
			return "";
		}

		public static void CancelAssetPackDownload(string[] assetPackNames)
		{
		}

		public static void RemoveAssetPack(string assetPackName)
		{
		}

		private static string GetDataPackName()
		{
			return "UnityDataAssetPack";
		}

		private static string GetStreamingAssetsPackName()
		{
			return "UnityStreamingAssetsPack";
		}
	}
	public enum AndroidHardwareType
	{
		Generic,
		ChromeOS
	}
	public class AndroidDevice
	{
		public static AndroidHardwareType hardwareType => AndroidHardwareType.Generic;

		public static void SetSustainedPerformanceMode(bool enabled)
		{
		}
	}
	public static class DiagnosticsReporting
	{
		public static void CallReportFullyDrawn()
		{
		}
	}
	public class PermissionCallbacks : AndroidJavaProxy
	{
		public event Action<string> PermissionGranted;

		public event Action<string> PermissionDenied;

		public event Action<string> PermissionDeniedAndDontAskAgain;

		public PermissionCallbacks()
			: base("com.unity3d.player.IPermissionRequestCallbacks")
		{
		}

		private void onPermissionGranted(string permissionName)
		{
			this.PermissionGranted?.Invoke(permissionName);
		}

		private void onPermissionDenied(string permissionName)
		{
			this.PermissionDenied?.Invoke(permissionName);
		}

		private void onPermissionDeniedAndDontAskAgain(string permissionName)
		{
			if (this.PermissionDeniedAndDontAskAgain != null)
			{
				this.PermissionDeniedAndDontAskAgain(permissionName);
			}
			else
			{
				this.PermissionDenied?.Invoke(permissionName);
			}
		}
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct Permission
	{
		public const string Camera = "android.permission.CAMERA";

		public const string Microphone = "android.permission.RECORD_AUDIO";

		public const string FineLocation = "android.permission.ACCESS_FINE_LOCATION";

		public const string CoarseLocation = "android.permission.ACCESS_COARSE_LOCATION";

		public const string ExternalStorageRead = "android.permission.READ_EXTERNAL_STORAGE";

		public const string ExternalStorageWrite = "android.permission.WRITE_EXTERNAL_STORAGE";

		private static AndroidJavaObject m_UnityPermissions;

		private static AndroidJavaObject GetUnityPermissions()
		{
			if (m_UnityPermissions != null)
			{
				return m_UnityPermissions;
			}
			m_UnityPermissions = new AndroidJavaClass("com.unity3d.player.UnityPermissions");
			return m_UnityPermissions;
		}

		public static bool HasUserAuthorizedPermission(string permission)
		{
			if (permission == null)
			{
				return false;
			}
			return true;
		}

		public static void RequestUserPermission(string permission)
		{
			if (permission != null)
			{
				RequestUserPermissions(new string[1] { permission }, null);
			}
		}

		public static void RequestUserPermissions(string[] permissions)
		{
			if (permissions != null && permissions.Length != 0)
			{
				RequestUserPermissions(permissions, null);
			}
		}

		public static void RequestUserPermission(string permission, PermissionCallbacks callbacks)
		{
			if (permission != null)
			{
				RequestUserPermissions(new string[1] { permission }, callbacks);
			}
		}

		public static void RequestUserPermissions(string[] permissions, PermissionCallbacks callbacks)
		{
			if (permissions != null && permissions.Length != 0)
			{
			}
		}
	}
}
