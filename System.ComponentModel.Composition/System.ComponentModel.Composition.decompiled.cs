using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.AttributedModel;
using System.ComponentModel.Composition.Diagnostics;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading;
using Microsoft.Internal;
using Microsoft.Internal.Collections;
using Microsoft.Internal.Runtime.Serialization;
using Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("System.ComponentModel.Composition.dll")]
[assembly: AssemblyDescription("System.ComponentModel.Composition.dll")]
[assembly: AssemblyDefaultAlias("System.ComponentModel.Composition.dll")]
[assembly: AssemblyCompany("Mono development team")]
[assembly: AssemblyProduct("Mono Common Language Infrastructure")]
[assembly: AssemblyCopyright("(c) Microsoft Corporation. All rights reserved.")]
[assembly: SatelliteContractVersion("4.0.0.0")]
[assembly: AssemblyInformationalVersion("4.6.57.0")]
[assembly: AssemblyFileVersion("4.6.57.0")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: CLSCompliant(true)]
[assembly: AssemblyDelaySign(true)]
[assembly: InternalsVisibleTo("net_4_x_System.ComponentModel.Composition_xunit-test, PublicKey=002400000480000094000000060200000024000052534131000400000100010079159977d2d03a8e6bea7a2e74e8d1afcc93e8851974952bb480a12c9134474d04062447c37e0e68c080536fcf3c3fbe2ff9c979ce998475e506e8ce82dd5b0f350dc10e93bf2eeecf874b24770c5081dbea7447fddafa277b22de47d6ffea449674a4f9fccf84d15069089380284dbdd35f46cdff12a1bd78e4ef0065d016df")]
[assembly: SecurityCritical]
[assembly: AllowPartiallyTrustedCallers]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("4.0.0.0")]
internal static class Consts
{
	public const string MonoCorlibVersion = "1A5E0066-58DC-428A-B21C-0AD6CDAE2789";

	public const string MonoVersion = "6.13.0.0";

	public const string MonoCompany = "Mono development team";

	public const string MonoProduct = "Mono Common Language Infrastructure";

	public const string MonoCopyright = "(c) Various Mono authors";

	public const string FxVersion = "4.0.0.0";

	public const string FxFileVersion = "4.6.57.0";

	public const string EnvironmentVersion = "4.0.30319.42000";

	public const string VsVersion = "0.0.0.0";

	public const string VsFileVersion = "11.0.0.0";

	private const string PublicKeyToken = "b77a5c561934e089";

	public const string AssemblyI18N = "I18N, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMicrosoft_JScript = "Microsoft.JScript, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VisualStudio = "Microsoft.VisualStudio, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VisualStudio_Web = "Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VSDesigner = "Microsoft.VSDesigner, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMono_Http = "Mono.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Posix = "Mono.Posix, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Security = "Mono.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Messaging_RabbitMQ = "Mono.Messaging.RabbitMQ, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyCorlib = "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem = "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Data = "System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Design = "System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_DirectoryServices = "System.DirectoryServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Drawing = "System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Drawing_Design = "System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Messaging = "System.Messaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Security = "System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_ServiceProcess = "System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Web = "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Windows_Forms = "System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_2_0 = "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystemCore_3_5 = "System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Core = "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string WindowsBase_3_0 = "WindowsBase, Version=3.0.0.0, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyWindowsBase = "WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationCore_3_5 = "PresentationCore, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationCore_4_0 = "PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationFramework_3_5 = "PresentationFramework, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblySystemServiceModel_3_0 = "System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
}
namespace Microsoft.Internal
{
	internal static class Assumes
	{
		[Serializable]
		private class InternalErrorException : Exception
		{
			public InternalErrorException(string message)
				: base(string.Format(CultureInfo.CurrentCulture, Strings.InternalExceptionMessage, message))
			{
			}

			[SecuritySafeCritical]
			protected InternalErrorException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}
		}

		[DebuggerStepThrough]
		internal static void NotNull<T>(T value) where T : class
		{
			IsTrue(value != null);
		}

		[DebuggerStepThrough]
		internal static void NotNull<T1, T2>(T1 value1, T2 value2) where T1 : class where T2 : class
		{
			NotNull(value1);
			NotNull(value2);
		}

		[DebuggerStepThrough]
		internal static void NotNull<T1, T2, T3>(T1 value1, T2 value2, T3 value3) where T1 : class where T2 : class where T3 : class
		{
			NotNull(value1);
			NotNull(value2);
			NotNull(value3);
		}

		[DebuggerStepThrough]
		internal static void NotNullOrEmpty(string value)
		{
			NotNull(value);
			IsTrue(value.Length > 0);
		}

		[DebuggerStepThrough]
		internal static void IsTrue(bool condition)
		{
			if (!condition)
			{
				throw UncatchableException(null);
			}
		}

		[DebuggerStepThrough]
		internal static void IsTrue(bool condition, string message)
		{
			if (!condition)
			{
				throw UncatchableException(message);
			}
		}

		[DebuggerStepThrough]
		internal static T NotReachable<T>()
		{
			throw UncatchableException("Code path should never be reached!");
		}

		[DebuggerStepThrough]
		private static Exception UncatchableException(string message)
		{
			return new InternalErrorException(message);
		}
	}
	internal static class AttributeServices
	{
		public static T[] GetAttributes<T>(this ICustomAttributeProvider attributeProvider) where T : class
		{
			return (T[])attributeProvider.GetCustomAttributes(typeof(T), inherit: false);
		}

		public static T[] GetAttributes<T>(this ICustomAttributeProvider attributeProvider, bool inherit) where T : class
		{
			return (T[])attributeProvider.GetCustomAttributes(typeof(T), inherit);
		}

		public static T GetFirstAttribute<T>(this ICustomAttributeProvider attributeProvider) where T : class
		{
			return attributeProvider.GetAttributes<T>().FirstOrDefault();
		}

		public static T GetFirstAttribute<T>(this ICustomAttributeProvider attributeProvider, bool inherit) where T : class
		{
			return attributeProvider.GetAttributes<T>(inherit).FirstOrDefault();
		}

		public static bool IsAttributeDefined<T>(this ICustomAttributeProvider attributeProvider) where T : class
		{
			return attributeProvider.IsDefined(typeof(T), inherit: false);
		}

		public static bool IsAttributeDefined<T>(this ICustomAttributeProvider attributeProvider, bool inherit) where T : class
		{
			return attributeProvider.IsDefined(typeof(T), inherit);
		}
	}
	internal static class ContractServices
	{
		public static bool TryCast(Type contractType, object value, out object result)
		{
			if (value == null)
			{
				result = null;
				return true;
			}
			if (contractType.IsInstanceOfType(value))
			{
				result = value;
				return true;
			}
			if (typeof(Delegate).IsAssignableFrom(contractType) && value is ExportedDelegate exportedDelegate)
			{
				result = exportedDelegate.CreateDelegate(contractType.UnderlyingSystemType);
				return result != null;
			}
			result = null;
			return false;
		}
	}
	internal static class GenerationServices
	{
		private static readonly MethodInfo _typeGetTypeFromHandleMethod = typeof(Type).GetMethod("GetTypeFromHandle");

		private static readonly Type TypeType = typeof(Type);

		private static readonly Type StringType = typeof(string);

		private static readonly Type CharType = typeof(char);

		private static readonly Type BooleanType = typeof(bool);

		private static readonly Type ByteType = typeof(byte);

		private static readonly Type SByteType = typeof(sbyte);

		private static readonly Type Int16Type = typeof(short);

		private static readonly Type UInt16Type = typeof(ushort);

		private static readonly Type Int32Type = typeof(int);

		private static readonly Type UInt32Type = typeof(uint);

		private static readonly Type Int64Type = typeof(long);

		private static readonly Type UInt64Type = typeof(ulong);

		private static readonly Type DoubleType = typeof(double);

		private static readonly Type SingleType = typeof(float);

		private static readonly Type IEnumerableTypeofT = typeof(IEnumerable<>);

		private static readonly Type IEnumerableType = typeof(IEnumerable);

		private static readonly MethodInfo ExceptionGetData = typeof(Exception).GetProperty("Data").GetGetMethod();

		private static readonly MethodInfo DictionaryAdd = typeof(IDictionary).GetMethod("Add");

		private static readonly ConstructorInfo ObjectCtor = typeof(object).GetConstructor(Type.EmptyTypes);

		public static ILGenerator CreateGeneratorForPublicConstructor(this TypeBuilder typeBuilder, Type[] ctrArgumentTypes)
		{
			ILGenerator iLGenerator = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, ctrArgumentTypes).GetILGenerator();
			iLGenerator.Emit(OpCodes.Ldarg_0);
			iLGenerator.Emit(OpCodes.Call, ObjectCtor);
			return iLGenerator;
		}

		public static void LoadValue(this ILGenerator ilGenerator, object value)
		{
			Assumes.NotNull(ilGenerator);
			if (value == null)
			{
				ilGenerator.LoadNull();
				return;
			}
			Type type = value.GetType();
			object obj = value;
			if (type.IsEnum)
			{
				obj = Convert.ChangeType(value, Enum.GetUnderlyingType(type), null);
				type = obj.GetType();
			}
			if (type == StringType)
			{
				ilGenerator.LoadString((string)obj);
				return;
			}
			if (TypeType.IsAssignableFrom(type))
			{
				ilGenerator.LoadTypeOf((Type)obj);
				return;
			}
			if (IEnumerableType.IsAssignableFrom(type))
			{
				ilGenerator.LoadEnumerable((IEnumerable)obj);
				return;
			}
			if (type == CharType || type == BooleanType || type == ByteType || type == SByteType || type == Int16Type || type == UInt16Type || type == Int32Type)
			{
				ilGenerator.LoadInt((int)Convert.ChangeType(obj, typeof(int), CultureInfo.InvariantCulture));
				return;
			}
			if (type == UInt32Type)
			{
				ilGenerator.LoadInt((int)(uint)obj);
				return;
			}
			if (type == Int64Type)
			{
				ilGenerator.LoadLong((long)obj);
				return;
			}
			if (type == UInt64Type)
			{
				ilGenerator.LoadLong((long)(ulong)obj);
				return;
			}
			if (type == SingleType)
			{
				ilGenerator.LoadFloat((float)obj);
				return;
			}
			if (type == DoubleType)
			{
				ilGenerator.LoadDouble((double)obj);
				return;
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.InvalidMetadataValue, value.GetType().FullName));
		}

		public static void AddItemToLocalDictionary(this ILGenerator ilGenerator, LocalBuilder dictionary, object key, object value)
		{
			Assumes.NotNull(ilGenerator);
			Assumes.NotNull(dictionary);
			Assumes.NotNull(key);
			Assumes.NotNull(value);
			ilGenerator.Emit(OpCodes.Ldloc, dictionary);
			ilGenerator.LoadValue(key);
			ilGenerator.LoadValue(value);
			ilGenerator.Emit(OpCodes.Callvirt, DictionaryAdd);
		}

		public static void AddLocalToLocalDictionary(this ILGenerator ilGenerator, LocalBuilder dictionary, object key, LocalBuilder value)
		{
			Assumes.NotNull(ilGenerator);
			Assumes.NotNull(dictionary);
			Assumes.NotNull(key);
			Assumes.NotNull(value);
			ilGenerator.Emit(OpCodes.Ldloc, dictionary);
			ilGenerator.LoadValue(key);
			ilGenerator.Emit(OpCodes.Ldloc, value);
			ilGenerator.Emit(OpCodes.Callvirt, DictionaryAdd);
		}

		public static void GetExceptionDataAndStoreInLocal(this ILGenerator ilGenerator, LocalBuilder exception, LocalBuilder dataStore)
		{
			Assumes.NotNull(ilGenerator);
			Assumes.NotNull(exception);
			Assumes.NotNull(dataStore);
			ilGenerator.Emit(OpCodes.Ldloc, exception);
			ilGenerator.Emit(OpCodes.Callvirt, ExceptionGetData);
			ilGenerator.Emit(OpCodes.Stloc, dataStore);
		}

		private static void LoadEnumerable(this ILGenerator ilGenerator, IEnumerable enumerable)
		{
			Assumes.NotNull(ilGenerator);
			Assumes.NotNull(enumerable);
			Type type = null;
			Type targetClosedInterfaceType = null;
			type = ((!ReflectionServices.TryGetGenericInterfaceType(enumerable.GetType(), IEnumerableTypeofT, out targetClosedInterfaceType)) ? typeof(object) : targetClosedInterfaceType.GetGenericArguments()[0]);
			Type localType = type.MakeArrayType();
			LocalBuilder local = ilGenerator.DeclareLocal(localType);
			ilGenerator.LoadInt(enumerable.Cast<object>().Count());
			ilGenerator.Emit(OpCodes.Newarr, type);
			ilGenerator.Emit(OpCodes.Stloc, local);
			int num = 0;
			foreach (object item in enumerable)
			{
				ilGenerator.Emit(OpCodes.Ldloc, local);
				ilGenerator.LoadInt(num);
				ilGenerator.LoadValue(item);
				if (IsBoxingRequiredForValue(item) && !type.IsValueType)
				{
					ilGenerator.Emit(OpCodes.Box, item.GetType());
				}
				ilGenerator.Emit(OpCodes.Stelem, type);
				num++;
			}
			ilGenerator.Emit(OpCodes.Ldloc, local);
		}

		private static bool IsBoxingRequiredForValue(object value)
		{
			return value?.GetType().IsValueType ?? false;
		}

		private static void LoadNull(this ILGenerator ilGenerator)
		{
			ilGenerator.Emit(OpCodes.Ldnull);
		}

		private static void LoadString(this ILGenerator ilGenerator, string s)
		{
			Assumes.NotNull(ilGenerator);
			if (s == null)
			{
				ilGenerator.LoadNull();
			}
			else
			{
				ilGenerator.Emit(OpCodes.Ldstr, s);
			}
		}

		private static void LoadInt(this ILGenerator ilGenerator, int value)
		{
			Assumes.NotNull(ilGenerator);
			ilGenerator.Emit(OpCodes.Ldc_I4, value);
		}

		private static void LoadLong(this ILGenerator ilGenerator, long value)
		{
			Assumes.NotNull(ilGenerator);
			ilGenerator.Emit(OpCodes.Ldc_I8, value);
		}

		private static void LoadFloat(this ILGenerator ilGenerator, float value)
		{
			Assumes.NotNull(ilGenerator);
			ilGenerator.Emit(OpCodes.Ldc_R4, value);
		}

		private static void LoadDouble(this ILGenerator ilGenerator, double value)
		{
			Assumes.NotNull(ilGenerator);
			ilGenerator.Emit(OpCodes.Ldc_R8, value);
		}

		private static void LoadTypeOf(this ILGenerator ilGenerator, Type type)
		{
			Assumes.NotNull(ilGenerator);
			ilGenerator.Emit(OpCodes.Ldtoken, type);
			ilGenerator.EmitCall(OpCodes.Call, _typeGetTypeFromHandleMethod, null);
		}
	}
	internal static class LazyServices
	{
		public static T GetNotNullValue<T>(this Lazy<T> lazy, string argument) where T : class
		{
			Assumes.NotNull(lazy);
			return lazy.Value ?? throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.LazyServices_LazyResolvesToNull, typeof(T), argument));
		}
	}
	internal struct ReadLock : IDisposable
	{
		private readonly Lock _lock;

		private int _isDisposed;

		public ReadLock(Lock @lock)
		{
			_isDisposed = 0;
			_lock = @lock;
			_lock.EnterReadLock();
		}

		public void Dispose()
		{
			if (Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 0)
			{
				_lock.ExitReadLock();
			}
		}
	}
	internal struct WriteLock : IDisposable
	{
		private readonly Lock _lock;

		private int _isDisposed;

		public WriteLock(Lock @lock)
		{
			_isDisposed = 0;
			_lock = @lock;
			_lock.EnterWriteLock();
		}

		public void Dispose()
		{
			if (Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 0)
			{
				_lock.ExitWriteLock();
			}
		}
	}
	internal sealed class Lock : IDisposable
	{
		private ReaderWriterLockSlim _thisLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

		private int _isDisposed;

		public void EnterReadLock()
		{
			_thisLock.EnterReadLock();
		}

		public void EnterWriteLock()
		{
			_thisLock.EnterWriteLock();
		}

		public void ExitReadLock()
		{
			_thisLock.ExitReadLock();
		}

		public void ExitWriteLock()
		{
			_thisLock.ExitWriteLock();
		}

		public void Dispose()
		{
			if (Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 0)
			{
				_thisLock.Dispose();
			}
		}
	}
	internal static class ReflectionInvoke
	{
		public static object SafeCreateInstance(this Type type, params object[] arguments)
		{
			DemandMemberAccessIfNeeded(type);
			return Activator.CreateInstance(type, arguments);
		}

		public static object SafeInvoke(this ConstructorInfo constructor, params object[] arguments)
		{
			DemandMemberAccessIfNeeded(constructor);
			return constructor.Invoke(arguments);
		}

		public static object SafeInvoke(this MethodInfo method, object instance, params object[] arguments)
		{
			DemandMemberAccessIfNeeded(method);
			return method.Invoke(instance, arguments);
		}

		public static object SafeGetValue(this FieldInfo field, object instance)
		{
			DemandMemberAccessIfNeeded(field);
			return field.GetValue(instance);
		}

		public static void SafeSetValue(this FieldInfo field, object instance, object value)
		{
			DemandMemberAccessIfNeeded(field);
			field.SetValue(instance, value);
		}

		public static void DemandMemberAccessIfNeeded(MethodInfo method)
		{
		}

		private static void DemandMemberAccessIfNeeded(ConstructorInfo constructor)
		{
		}

		private static void DemandMemberAccessIfNeeded(FieldInfo field)
		{
		}

		public static void DemandMemberAccessIfNeeded(Type type)
		{
		}
	}
	internal static class ReflectionServices
	{
		public static Assembly Assembly(this MemberInfo member)
		{
			Type type = member as Type;
			if (type != null)
			{
				return type.Assembly;
			}
			return member.DeclaringType.Assembly;
		}

		public static bool IsVisible(this ConstructorInfo constructor)
		{
			if (constructor.DeclaringType.IsVisible)
			{
				return constructor.IsPublic;
			}
			return false;
		}

		public static bool IsVisible(this FieldInfo field)
		{
			if (field.DeclaringType.IsVisible)
			{
				return field.IsPublic;
			}
			return false;
		}

		public static bool IsVisible(this MethodInfo method)
		{
			if (!method.DeclaringType.IsVisible)
			{
				return false;
			}
			if (!method.IsPublic)
			{
				return false;
			}
			if (method.IsGenericMethod)
			{
				Type[] genericArguments = method.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (!genericArguments[i].IsVisible)
					{
						return false;
					}
				}
			}
			return true;
		}

		public static string GetDisplayName(Type declaringType, string name)
		{
			Assumes.NotNull(declaringType);
			return declaringType.GetDisplayName() + "." + name;
		}

		public static string GetDisplayName(this MemberInfo member)
		{
			Assumes.NotNull(member);
			MemberTypes memberType = member.MemberType;
			if (memberType == MemberTypes.TypeInfo || memberType == MemberTypes.NestedType)
			{
				return AttributedModelServices.GetTypeIdentity((Type)member);
			}
			return GetDisplayName(member.DeclaringType, member.Name);
		}

		internal static bool TryGetGenericInterfaceType(Type instanceType, Type targetOpenInterfaceType, out Type targetClosedInterfaceType)
		{
			Assumes.IsTrue(targetOpenInterfaceType.IsInterface);
			Assumes.IsTrue(targetOpenInterfaceType.IsGenericTypeDefinition);
			Assumes.IsTrue(!instanceType.IsGenericTypeDefinition);
			if (instanceType.IsInterface && instanceType.IsGenericType && instanceType.UnderlyingSystemType.GetGenericTypeDefinition() == targetOpenInterfaceType.UnderlyingSystemType)
			{
				targetClosedInterfaceType = instanceType;
				return true;
			}
			try
			{
				Type type = instanceType.GetInterface(targetOpenInterfaceType.Name, ignoreCase: false);
				if (type != null && type.UnderlyingSystemType.GetGenericTypeDefinition() == targetOpenInterfaceType.UnderlyingSystemType)
				{
					targetClosedInterfaceType = type;
					return true;
				}
			}
			catch (AmbiguousMatchException)
			{
			}
			targetClosedInterfaceType = null;
			return false;
		}

		internal static IEnumerable<PropertyInfo> GetAllProperties(this Type type)
		{
			return type.GetInterfaces().Concat(new Type[1] { type }).SelectMany((Type itf) => itf.GetProperties());
		}

		internal static IEnumerable<MethodInfo> GetAllMethods(this Type type)
		{
			IEnumerable<MethodInfo> declaredMethods = type.GetDeclaredMethods();
			Type baseType = type.BaseType;
			if (baseType.UnderlyingSystemType != typeof(object))
			{
				return declaredMethods.Concat(baseType.GetAllMethods());
			}
			return declaredMethods;
		}

		private static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type)
		{
			MethodInfo[] methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < methods.Length; i++)
			{
				yield return methods[i];
			}
		}

		public static IEnumerable<FieldInfo> GetAllFields(this Type type)
		{
			IEnumerable<FieldInfo> declaredFields = type.GetDeclaredFields();
			Type baseType = type.BaseType;
			if (baseType.UnderlyingSystemType != typeof(object))
			{
				return declaredFields.Concat(baseType.GetAllFields());
			}
			return declaredFields;
		}

		private static IEnumerable<FieldInfo> GetDeclaredFields(this Type type)
		{
			FieldInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < fields.Length; i++)
			{
				yield return fields[i];
			}
		}
	}
	internal static class Requires
	{
		[DebuggerStepThrough]
		public static void NotNull<T>(T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
		}

		[DebuggerStepThrough]
		public static void NotNullOrEmpty(string value, string parameterName)
		{
			NotNull(value, parameterName);
			if (value.Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ArgumentException_EmptyString, parameterName), parameterName);
			}
		}

		[DebuggerStepThrough]
		public static void NotNullOrNullElements<T>(IEnumerable<T> values, string parameterName) where T : class
		{
			NotNull(values, parameterName);
			NotNullElements(values, parameterName);
		}

		[DebuggerStepThrough]
		public static void NullOrNotNullElements<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> values, string parameterName) where TKey : class where TValue : class
		{
			NotNullElements(values, parameterName);
		}

		[DebuggerStepThrough]
		public static void NullOrNotNullElements<T>(IEnumerable<T> values, string parameterName) where T : class
		{
			NotNullElements(values, parameterName);
		}

		private static void NotNullElements<T>(IEnumerable<T> values, string parameterName) where T : class
		{
			if (values != null && !Contract.ForAll(values, (T value) => value != null))
			{
				throw ExceptionBuilder.CreateContainsNullElement(parameterName);
			}
		}

		private static void NotNullElements<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> values, string parameterName) where TKey : class where TValue : class
		{
			if (values != null && !Contract.ForAll(values, (KeyValuePair<TKey, TValue> keyValue) => keyValue.Key != null && keyValue.Value != null))
			{
				throw ExceptionBuilder.CreateContainsNullElement(parameterName);
			}
		}

		[DebuggerStepThrough]
		public static void IsInMembertypeSet(MemberTypes value, string parameterName, MemberTypes enumFlagSet)
		{
			if ((value & enumFlagSet) != value || (value & (value - 1)) != 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ArgumentOutOfRange_InvalidEnumInSet, parameterName, value, enumFlagSet.ToString()), parameterName);
			}
		}
	}
	internal static class StringComparers
	{
		public static StringComparer ContractName => StringComparer.Ordinal;

		public static StringComparer MetadataKeyNames => StringComparer.Ordinal;
	}
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Strings
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (resourceMan == null)
				{
					resourceMan = new ResourceManager("Microsoft.Internal.Strings", typeof(Strings).Assembly);
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal static string Argument_AssemblyReflectionOnly => ResourceManager.GetString("Argument_AssemblyReflectionOnly", resourceCulture);

		internal static string Argument_ElementReflectionOnlyType => ResourceManager.GetString("Argument_ElementReflectionOnlyType", resourceCulture);

		internal static string Argument_ExportsEmpty => ResourceManager.GetString("Argument_ExportsEmpty", resourceCulture);

		internal static string Argument_ExportsTooMany => ResourceManager.GetString("Argument_ExportsTooMany", resourceCulture);

		internal static string Argument_NullElement => ResourceManager.GetString("Argument_NullElement", resourceCulture);

		internal static string Argument_ReflectionContextReturnsReflectionOnlyType => ResourceManager.GetString("Argument_ReflectionContextReturnsReflectionOnlyType", resourceCulture);

		internal static string ArgumentException_EmptyString => ResourceManager.GetString("ArgumentException_EmptyString", resourceCulture);

		internal static string ArgumentOutOfRange_InvalidEnum => ResourceManager.GetString("ArgumentOutOfRange_InvalidEnum", resourceCulture);

		internal static string ArgumentOutOfRange_InvalidEnumInSet => ResourceManager.GetString("ArgumentOutOfRange_InvalidEnumInSet", resourceCulture);

		internal static string ArgumentValueType => ResourceManager.GetString("ArgumentValueType", resourceCulture);

		internal static string AssemblyFileNotFoundOrWrongType => ResourceManager.GetString("AssemblyFileNotFoundOrWrongType", resourceCulture);

		internal static string AtomicComposition_AlreadyCompleted => ResourceManager.GetString("AtomicComposition_AlreadyCompleted", resourceCulture);

		internal static string AtomicComposition_AlreadyNested => ResourceManager.GetString("AtomicComposition_AlreadyNested", resourceCulture);

		internal static string AtomicComposition_PartOfAnotherAtomicComposition => ResourceManager.GetString("AtomicComposition_PartOfAnotherAtomicComposition", resourceCulture);

		internal static string CardinalityMismatch_NoExports => ResourceManager.GetString("CardinalityMismatch_NoExports", resourceCulture);

		internal static string CardinalityMismatch_TooManyExports => ResourceManager.GetString("CardinalityMismatch_TooManyExports", resourceCulture);

		internal static string CatalogMutation_Invalid => ResourceManager.GetString("CatalogMutation_Invalid", resourceCulture);

		internal static string CompositionElement_UnknownOrigin => ResourceManager.GetString("CompositionElement_UnknownOrigin", resourceCulture);

		internal static string CompositionException_ChangesRejected => ResourceManager.GetString("CompositionException_ChangesRejected", resourceCulture);

		internal static string CompositionException_ElementPrefix => ResourceManager.GetString("CompositionException_ElementPrefix", resourceCulture);

		internal static string CompositionException_ErrorPrefix => ResourceManager.GetString("CompositionException_ErrorPrefix", resourceCulture);

		internal static string CompositionException_MetadataViewInvalidConstructor => ResourceManager.GetString("CompositionException_MetadataViewInvalidConstructor", resourceCulture);

		internal static string CompositionException_MultipleErrorsWithMultiplePaths => ResourceManager.GetString("CompositionException_MultipleErrorsWithMultiplePaths", resourceCulture);

		internal static string CompositionException_OriginFormat => ResourceManager.GetString("CompositionException_OriginFormat", resourceCulture);

		internal static string CompositionException_OriginSeparator => ResourceManager.GetString("CompositionException_OriginSeparator", resourceCulture);

		internal static string CompositionException_PathsCountSeparator => ResourceManager.GetString("CompositionException_PathsCountSeparator", resourceCulture);

		internal static string CompositionException_ReviewErrorProperty => ResourceManager.GetString("CompositionException_ReviewErrorProperty", resourceCulture);

		internal static string CompositionException_SingleErrorWithMultiplePaths => ResourceManager.GetString("CompositionException_SingleErrorWithMultiplePaths", resourceCulture);

		internal static string CompositionException_SingleErrorWithSinglePath => ResourceManager.GetString("CompositionException_SingleErrorWithSinglePath", resourceCulture);

		internal static string CompositionTrace_Discovery_AssemblyLoadFailed => ResourceManager.GetString("CompositionTrace_Discovery_AssemblyLoadFailed", resourceCulture);

		internal static string CompositionTrace_Discovery_DefinitionContainsNoExports => ResourceManager.GetString("CompositionTrace_Discovery_DefinitionContainsNoExports", resourceCulture);

		internal static string CompositionTrace_Discovery_DefinitionMarkedWithPartNotDiscoverableAttribute => ResourceManager.GetString("CompositionTrace_Discovery_DefinitionMarkedWithPartNotDiscoverableAttribute", resourceCulture);

		internal static string CompositionTrace_Discovery_DefinitionMismatchedExportArity => ResourceManager.GetString("CompositionTrace_Discovery_DefinitionMismatchedExportArity", resourceCulture);

		internal static string CompositionTrace_Discovery_MemberMarkedWithMultipleImportAndImportMany => ResourceManager.GetString("CompositionTrace_Discovery_MemberMarkedWithMultipleImportAndImportMany", resourceCulture);

		internal static string CompositionTrace_Rejection_DefinitionRejected => ResourceManager.GetString("CompositionTrace_Rejection_DefinitionRejected", resourceCulture);

		internal static string CompositionTrace_Rejection_DefinitionResurrected => ResourceManager.GetString("CompositionTrace_Rejection_DefinitionResurrected", resourceCulture);

		internal static string ContractMismatch_ExportedValueCannotBeCastToT => ResourceManager.GetString("ContractMismatch_ExportedValueCannotBeCastToT", resourceCulture);

		internal static string ContractMismatch_InvalidCastOnMetadataField => ResourceManager.GetString("ContractMismatch_InvalidCastOnMetadataField", resourceCulture);

		internal static string ContractMismatch_MetadataViewImplementationCanNotBeNull => ResourceManager.GetString("ContractMismatch_MetadataViewImplementationCanNotBeNull", resourceCulture);

		internal static string ContractMismatch_MetadataViewImplementationDoesNotImplementViewInterface => ResourceManager.GetString("ContractMismatch_MetadataViewImplementationDoesNotImplementViewInterface", resourceCulture);

		internal static string ContractMismatch_NullReferenceOnMetadataField => ResourceManager.GetString("ContractMismatch_NullReferenceOnMetadataField", resourceCulture);

		internal static string DirectoryNotFound => ResourceManager.GetString("DirectoryNotFound", resourceCulture);

		internal static string Discovery_DuplicateMetadataNameValues => ResourceManager.GetString("Discovery_DuplicateMetadataNameValues", resourceCulture);

		internal static string Discovery_MetadataContainsValueWithInvalidType => ResourceManager.GetString("Discovery_MetadataContainsValueWithInvalidType", resourceCulture);

		internal static string Discovery_ReservedMetadataNameUsed => ResourceManager.GetString("Discovery_ReservedMetadataNameUsed", resourceCulture);

		internal static string ExportDefinitionNotOnThisComposablePart => ResourceManager.GetString("ExportDefinitionNotOnThisComposablePart", resourceCulture);

		internal static string ExportFactory_TooManyGenericParameters => ResourceManager.GetString("ExportFactory_TooManyGenericParameters", resourceCulture);

		internal static string ExportNotValidOnIndexers => ResourceManager.GetString("ExportNotValidOnIndexers", resourceCulture);

		internal static string ImportDefinitionNotOnThisComposablePart => ResourceManager.GetString("ImportDefinitionNotOnThisComposablePart", resourceCulture);

		internal static string ImportEngine_ComposeTookTooManyIterations => ResourceManager.GetString("ImportEngine_ComposeTookTooManyIterations", resourceCulture);

		internal static string ImportEngine_InvalidStateForRecomposition => ResourceManager.GetString("ImportEngine_InvalidStateForRecomposition", resourceCulture);

		internal static string ImportEngine_PartCannotActivate => ResourceManager.GetString("ImportEngine_PartCannotActivate", resourceCulture);

		internal static string ImportEngine_PartCannotGetExportedValue => ResourceManager.GetString("ImportEngine_PartCannotGetExportedValue", resourceCulture);

		internal static string ImportEngine_PartCannotSetImport => ResourceManager.GetString("ImportEngine_PartCannotSetImport", resourceCulture);

		internal static string ImportEngine_PartCycle => ResourceManager.GetString("ImportEngine_PartCycle", resourceCulture);

		internal static string ImportEngine_PreventedByExistingImport => ResourceManager.GetString("ImportEngine_PreventedByExistingImport", resourceCulture);

		internal static string ImportNotSetOnPart => ResourceManager.GetString("ImportNotSetOnPart", resourceCulture);

		internal static string ImportNotValidOnIndexers => ResourceManager.GetString("ImportNotValidOnIndexers", resourceCulture);

		internal static string InternalExceptionMessage => ResourceManager.GetString("InternalExceptionMessage", resourceCulture);

		internal static string InvalidArgument_ReflectionContext => ResourceManager.GetString("InvalidArgument_ReflectionContext", resourceCulture);

		internal static string InvalidMetadataValue => ResourceManager.GetString("InvalidMetadataValue", resourceCulture);

		internal static string InvalidMetadataView => ResourceManager.GetString("InvalidMetadataView", resourceCulture);

		internal static string InvalidOperation_DefinitionCannotBeRecomposed => ResourceManager.GetString("InvalidOperation_DefinitionCannotBeRecomposed", resourceCulture);

		internal static string InvalidOperation_GetExportedValueBeforePrereqImportSet => ResourceManager.GetString("InvalidOperation_GetExportedValueBeforePrereqImportSet", resourceCulture);

		internal static string InvalidOperationReentrantCompose => ResourceManager.GetString("InvalidOperationReentrantCompose", resourceCulture);

		internal static string InvalidPartCreationPolicyOnImport => ResourceManager.GetString("InvalidPartCreationPolicyOnImport", resourceCulture);

		internal static string InvalidPartCreationPolicyOnPart => ResourceManager.GetString("InvalidPartCreationPolicyOnPart", resourceCulture);

		internal static string InvalidSetterOnMetadataField => ResourceManager.GetString("InvalidSetterOnMetadataField", resourceCulture);

		internal static string LazyMemberInfo_AccessorsNull => ResourceManager.GetString("LazyMemberInfo_AccessorsNull", resourceCulture);

		internal static string LazyMemberInfo_InvalidAccessorOnSimpleMember => ResourceManager.GetString("LazyMemberInfo_InvalidAccessorOnSimpleMember", resourceCulture);

		internal static string LazyMemberinfo_InvalidEventAccessors_AccessorType => ResourceManager.GetString("LazyMemberinfo_InvalidEventAccessors_AccessorType", resourceCulture);

		internal static string LazyMemberInfo_InvalidEventAccessors_Cardinality => ResourceManager.GetString("LazyMemberInfo_InvalidEventAccessors_Cardinality", resourceCulture);

		internal static string LazyMemberinfo_InvalidPropertyAccessors_AccessorType => ResourceManager.GetString("LazyMemberinfo_InvalidPropertyAccessors_AccessorType", resourceCulture);

		internal static string LazyMemberInfo_InvalidPropertyAccessors_Cardinality => ResourceManager.GetString("LazyMemberInfo_InvalidPropertyAccessors_Cardinality", resourceCulture);

		internal static string LazyMemberInfo_NoAccessors => ResourceManager.GetString("LazyMemberInfo_NoAccessors", resourceCulture);

		internal static string LazyServices_LazyResolvesToNull => ResourceManager.GetString("LazyServices_LazyResolvesToNull", resourceCulture);

		internal static string MetadataItemNotSupported => ResourceManager.GetString("MetadataItemNotSupported", resourceCulture);

		internal static string NotImplemented_NotOverriddenByDerived => ResourceManager.GetString("NotImplemented_NotOverriddenByDerived", resourceCulture);

		internal static string NotSupportedCatalogChanges => ResourceManager.GetString("NotSupportedCatalogChanges", resourceCulture);

		internal static string NotSupportedInterfaceMetadataView => ResourceManager.GetString("NotSupportedInterfaceMetadataView", resourceCulture);

		internal static string NotSupportedReadOnlyDictionary => ResourceManager.GetString("NotSupportedReadOnlyDictionary", resourceCulture);

		internal static string ObjectAlreadyInitialized => ResourceManager.GetString("ObjectAlreadyInitialized", resourceCulture);

		internal static string ObjectMustBeInitialized => ResourceManager.GetString("ObjectMustBeInitialized", resourceCulture);

		internal static string ReentrantCompose => ResourceManager.GetString("ReentrantCompose", resourceCulture);

		internal static string ReflectionContext_Requires_DefaultConstructor => ResourceManager.GetString("ReflectionContext_Requires_DefaultConstructor", resourceCulture);

		internal static string ReflectionContext_Type_Required => ResourceManager.GetString("ReflectionContext_Type_Required", resourceCulture);

		internal static string ReflectionModel_ExportNotReadable => ResourceManager.GetString("ReflectionModel_ExportNotReadable", resourceCulture);

		internal static string ReflectionModel_ExportThrewException => ResourceManager.GetString("ReflectionModel_ExportThrewException", resourceCulture);

		internal static string ReflectionModel_ImportCollectionAddThrewException => ResourceManager.GetString("ReflectionModel_ImportCollectionAddThrewException", resourceCulture);

		internal static string ReflectionModel_ImportCollectionClearThrewException => ResourceManager.GetString("ReflectionModel_ImportCollectionClearThrewException", resourceCulture);

		internal static string ReflectionModel_ImportCollectionConstructionThrewException => ResourceManager.GetString("ReflectionModel_ImportCollectionConstructionThrewException", resourceCulture);

		internal static string ReflectionModel_ImportCollectionGetThrewException => ResourceManager.GetString("ReflectionModel_ImportCollectionGetThrewException", resourceCulture);

		internal static string ReflectionModel_ImportCollectionIsReadOnlyThrewException => ResourceManager.GetString("ReflectionModel_ImportCollectionIsReadOnlyThrewException", resourceCulture);

		internal static string ReflectionModel_ImportCollectionNotWritable => ResourceManager.GetString("ReflectionModel_ImportCollectionNotWritable", resourceCulture);

		internal static string ReflectionModel_ImportCollectionNull => ResourceManager.GetString("ReflectionModel_ImportCollectionNull", resourceCulture);

		internal static string ReflectionModel_ImportManyOnParameterCanOnlyBeAssigned => ResourceManager.GetString("ReflectionModel_ImportManyOnParameterCanOnlyBeAssigned", resourceCulture);

		internal static string ReflectionModel_ImportNotAssignableFromExport => ResourceManager.GetString("ReflectionModel_ImportNotAssignableFromExport", resourceCulture);

		internal static string ReflectionModel_ImportNotWritable => ResourceManager.GetString("ReflectionModel_ImportNotWritable", resourceCulture);

		internal static string ReflectionModel_ImportThrewException => ResourceManager.GetString("ReflectionModel_ImportThrewException", resourceCulture);

		internal static string ReflectionModel_InvalidExportDefinition => ResourceManager.GetString("ReflectionModel_InvalidExportDefinition", resourceCulture);

		internal static string ReflectionModel_InvalidImportDefinition => ResourceManager.GetString("ReflectionModel_InvalidImportDefinition", resourceCulture);

		internal static string ReflectionModel_InvalidMemberImportDefinition => ResourceManager.GetString("ReflectionModel_InvalidMemberImportDefinition", resourceCulture);

		internal static string ReflectionModel_InvalidParameterImportDefinition => ResourceManager.GetString("ReflectionModel_InvalidParameterImportDefinition", resourceCulture);

		internal static string ReflectionModel_InvalidPartDefinition => ResourceManager.GetString("ReflectionModel_InvalidPartDefinition", resourceCulture);

		internal static string ReflectionModel_PartConstructorMissing => ResourceManager.GetString("ReflectionModel_PartConstructorMissing", resourceCulture);

		internal static string ReflectionModel_PartConstructorThrewException => ResourceManager.GetString("ReflectionModel_PartConstructorThrewException", resourceCulture);

		internal static string ReflectionModel_PartOnImportsSatisfiedThrewException => ResourceManager.GetString("ReflectionModel_PartOnImportsSatisfiedThrewException", resourceCulture);

		internal static string TypeCatalog_DisplayNameFormat => ResourceManager.GetString("TypeCatalog_DisplayNameFormat", resourceCulture);

		internal static string TypeCatalog_Empty => ResourceManager.GetString("TypeCatalog_Empty", resourceCulture);

		internal Strings()
		{
		}
	}
}
namespace Microsoft.Internal.Runtime.Serialization
{
	internal static class SerializationServices
	{
		public static T GetValue<T>(this SerializationInfo info, string name)
		{
			Assumes.NotNull(info, name);
			return (T)info.GetValue(name, typeof(T));
		}
	}
}
namespace Microsoft.Internal.Collections
{
	internal static class CollectionServices
	{
		private class CollectionOfObjectList : ICollection<object>, IEnumerable<object>, IEnumerable
		{
			private readonly IList _list;

			public int Count => Assumes.NotReachable<int>();

			public bool IsReadOnly => _list.IsReadOnly;

			public CollectionOfObjectList(IList list)
			{
				_list = list;
			}

			public void Add(object item)
			{
				_list.Add(item);
			}

			public void Clear()
			{
				_list.Clear();
			}

			public bool Contains(object item)
			{
				return Assumes.NotReachable<bool>();
			}

			public void CopyTo(object[] array, int arrayIndex)
			{
				Assumes.NotReachable<object>();
			}

			public bool Remove(object item)
			{
				return Assumes.NotReachable<bool>();
			}

			public IEnumerator<object> GetEnumerator()
			{
				return Assumes.NotReachable<IEnumerator<object>>();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return Assumes.NotReachable<IEnumerator>();
			}
		}

		private class CollectionOfObject<T> : ICollection<object>, IEnumerable<object>, IEnumerable
		{
			private readonly ICollection<T> _collectionOfT;

			public int Count => Assumes.NotReachable<int>();

			public bool IsReadOnly => _collectionOfT.IsReadOnly;

			public CollectionOfObject(object collectionOfT)
			{
				_collectionOfT = (ICollection<T>)collectionOfT;
			}

			public void Add(object item)
			{
				_collectionOfT.Add((T)item);
			}

			public void Clear()
			{
				_collectionOfT.Clear();
			}

			public bool Contains(object item)
			{
				return Assumes.NotReachable<bool>();
			}

			public void CopyTo(object[] array, int arrayIndex)
			{
				Assumes.NotReachable<object>();
			}

			public bool Remove(object item)
			{
				return Assumes.NotReachable<bool>();
			}

			public IEnumerator<object> GetEnumerator()
			{
				return Assumes.NotReachable<IEnumerator<object>>();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return Assumes.NotReachable<IEnumerator>();
			}
		}

		private static readonly Type StringType = typeof(string);

		private static readonly Type IEnumerableType = typeof(IEnumerable);

		private static readonly Type IEnumerableOfTType = typeof(IEnumerable<>);

		private static readonly Type ICollectionOfTType = typeof(ICollection<>);

		public static ICollection<object> GetCollectionWrapper(Type itemType, object collectionObject)
		{
			Assumes.NotNull(itemType, collectionObject);
			Type underlyingSystemType = itemType.UnderlyingSystemType;
			if (underlyingSystemType == typeof(object))
			{
				return (ICollection<object>)collectionObject;
			}
			if (typeof(IList).IsAssignableFrom(collectionObject.GetType()))
			{
				return new CollectionOfObjectList((IList)collectionObject);
			}
			return (ICollection<object>)Activator.CreateInstance(typeof(CollectionOfObject<>).MakeGenericType(underlyingSystemType), collectionObject);
		}

		public static bool IsEnumerableOfT(Type type)
		{
			if (type.IsGenericType && type.GetGenericTypeDefinition().UnderlyingSystemType == IEnumerableOfTType)
			{
				return true;
			}
			return false;
		}

		public static Type GetEnumerableElementType(Type type)
		{
			if (type.UnderlyingSystemType == StringType || !IEnumerableType.IsAssignableFrom(type))
			{
				return null;
			}
			if (ReflectionServices.TryGetGenericInterfaceType(type, IEnumerableOfTType, out var targetClosedInterfaceType))
			{
				return targetClosedInterfaceType.GetGenericArguments()[0];
			}
			return null;
		}

		public static Type GetCollectionElementType(Type type)
		{
			if (ReflectionServices.TryGetGenericInterfaceType(type, ICollectionOfTType, out var targetClosedInterfaceType))
			{
				return targetClosedInterfaceType.GetGenericArguments()[0];
			}
			return null;
		}

		public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> source)
		{
			Assumes.NotNull(source);
			return new ReadOnlyCollection<T>(source.AsArray());
		}

		public static IEnumerable<T> ConcatAllowingNull<T>(this IEnumerable<T> source, IEnumerable<T> second)
		{
			if (second == null || !second.FastAny())
			{
				return source;
			}
			if (source == null || !source.FastAny())
			{
				return second;
			}
			return source.Concat(second);
		}

		public static ICollection<T> ConcatAllowingNull<T>(this ICollection<T> source, ICollection<T> second)
		{
			if (second == null || second.Count == 0)
			{
				return source;
			}
			if (source == null || source.Count == 0)
			{
				return second;
			}
			List<T> list = new List<T>(source);
			list.AddRange(second);
			return list;
		}

		public static List<T> FastAppendToListAllowNulls<T>(this List<T> source, IEnumerable<T> second)
		{
			if (second == null)
			{
				return source;
			}
			if (source == null || source.Count == 0)
			{
				return second.AsList();
			}
			if (second is List<T> list)
			{
				if (list.Count == 0)
				{
					return source;
				}
				if (list.Count == 1)
				{
					source.Add(list[0]);
					return source;
				}
			}
			source.AddRange(second);
			return source;
		}

		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (T item in source)
			{
				action(item);
			}
		}

		public static EnumerableCardinality GetCardinality<T>(this IEnumerable<T> source)
		{
			Assumes.NotNull(source);
			if (source is ICollection { Count: var count })
			{
				return count switch
				{
					0 => EnumerableCardinality.Zero, 
					1 => EnumerableCardinality.One, 
					_ => EnumerableCardinality.TwoOrMore, 
				};
			}
			using IEnumerator<T> enumerator = source.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return EnumerableCardinality.Zero;
			}
			if (!enumerator.MoveNext())
			{
				return EnumerableCardinality.One;
			}
			return EnumerableCardinality.TwoOrMore;
		}

		public static bool FastAny<T>(this IEnumerable<T> source)
		{
			if (source is ICollection collection)
			{
				return collection.Count > 0;
			}
			return source.Any();
		}

		public static Stack<T> Copy<T>(this Stack<T> stack)
		{
			Assumes.NotNull(stack);
			return new Stack<T>(stack.Reverse());
		}

		public static T[] AsArray<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable is T[] result)
			{
				return result;
			}
			return enumerable.ToArray();
		}

		public static List<T> AsList<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable is List<T> result)
			{
				return result;
			}
			return enumerable.ToList();
		}

		public static bool IsArrayEqual<T>(this T[] thisArray, T[] thatArray)
		{
			if (thisArray.Length != thatArray.Length)
			{
				return false;
			}
			for (int i = 0; i < thisArray.Length; i++)
			{
				ref readonly T reference = ref thisArray[i];
				object obj = thatArray[i];
				if (!reference.Equals(obj))
				{
					return false;
				}
			}
			return true;
		}

		public static bool IsCollectionEqual<T>(this IList<T> thisList, IList<T> thatList)
		{
			if (thisList.Count != thatList.Count)
			{
				return false;
			}
			for (int i = 0; i < thisList.Count; i++)
			{
				if (!thisList[i].Equals(thatList[i]))
				{
					return false;
				}
			}
			return true;
		}
	}
	internal enum EnumerableCardinality
	{
		Zero,
		One,
		TwoOrMore
	}
	internal class WeakReferenceCollection<T> where T : class
	{
		private readonly List<WeakReference> _items = new List<WeakReference>();

		public void Add(T item)
		{
			if (_items.Capacity == _items.Count)
			{
				CleanupDeadReferences();
			}
			_items.Add(new WeakReference(item));
		}

		public void Remove(T item)
		{
			int num = IndexOf(item);
			if (num != -1)
			{
				_items.RemoveAt(num);
			}
		}

		public bool Contains(T item)
		{
			return IndexOf(item) >= 0;
		}

		public void Clear()
		{
			_items.Clear();
		}

		private int IndexOf(T item)
		{
			int count = _items.Count;
			for (int i = 0; i < count; i++)
			{
				if (_items[i].Target == item)
				{
					return i;
				}
			}
			return -1;
		}

		private void CleanupDeadReferences()
		{
			_items.RemoveAll((WeakReference w) => !w.IsAlive);
		}

		public List<T> AliveItemsToList()
		{
			List<T> list = new List<T>();
			foreach (WeakReference item2 in _items)
			{
				if (item2.Target is T item)
				{
					list.Add(item);
				}
			}
			return list;
		}
	}
}
namespace System
{
	/// <summary>Provides a lazy indirect reference to an object and its associated metadata for use by the Managed Extensibility Framework.</summary>
	/// <typeparam name="T">The type of the object referenced.</typeparam>
	/// <typeparam name="TMetadata">The type of the metadata.</typeparam>
	[Serializable]
	public class Lazy<T, TMetadata> : Lazy<T>
	{
		private TMetadata _metadata;

		/// <summary>Gets the metadata associated with the referenced object.</summary>
		/// <returns>The metadata associated with the referenced object.</returns>
		public TMetadata Metadata => _metadata;

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`2" /> class with the specified metadata that uses the specified function to get the referenced object.</summary>
		/// <param name="valueFactory">A function that returns the referenced object.</param>
		/// <param name="metadata">The metadata associated with the referenced object.</param>
		public Lazy(Func<T> valueFactory, TMetadata metadata)
			: base(valueFactory)
		{
			_metadata = metadata;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`2" /> class with the specified metadata.</summary>
		/// <param name="metadata">The metadata associated with the referenced object.</param>
		public Lazy(TMetadata metadata)
		{
			_metadata = metadata;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`2" /> class with the specified metadata and thread safety value.</summary>
		/// <param name="metadata">The metadata associated with the referenced object.</param>
		/// <param name="isThreadSafe">Indicates whether the <see cref="T:System.Lazy`2" /> object that is created will be thread-safe.</param>
		public Lazy(TMetadata metadata, bool isThreadSafe)
			: base(isThreadSafe)
		{
			_metadata = metadata;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`2" /> class with the specified metadata and thread safety value that uses the specified function to get the referenced object.</summary>
		/// <param name="valueFactory">A function that returns the referenced object.</param>
		/// <param name="metadata">The metadata associated with the referenced object.</param>
		/// <param name="isThreadSafe">Indicates whether the <see cref="T:System.Lazy`2" /> object that is created will be thread-safe.</param>
		public Lazy(Func<T> valueFactory, TMetadata metadata, bool isThreadSafe)
			: base(valueFactory, isThreadSafe)
		{
			_metadata = metadata;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`2" /> class with the specified metadata and thread synchronization mode.</summary>
		/// <param name="metadata">The metadata associated with the referenced object.</param>
		/// <param name="mode">The thread synchronization mode.</param>
		public Lazy(TMetadata metadata, LazyThreadSafetyMode mode)
			: base(mode)
		{
			_metadata = metadata;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`2" /> class with the specified metadata and thread synchronization mode that uses the specified function to get the referenced object.</summary>
		/// <param name="valueFactory">A function that returns the referenced object</param>
		/// <param name="metadata">The metadata associated with the referenced object.</param>
		/// <param name="mode">The thread synchronization mode</param>
		public Lazy(Func<T> valueFactory, TMetadata metadata, LazyThreadSafetyMode mode)
			: base(valueFactory, mode)
		{
			_metadata = metadata;
		}
	}
}
namespace System.ComponentModel
{
	[Conditional("NOT_FEATURE_LEGACYCOMPONENTMODEL")]
	internal sealed class LocalizableAttribute : Attribute
	{
		public LocalizableAttribute(bool isLocalizable)
		{
		}
	}
}
namespace System.ComponentModel.Composition
{
	/// <summary>Contains helper methods for using the MEF attributed programming model with composition.</summary>
	public static class AttributedModelServices
	{
		/// <summary>Gets a metadata view object from a dictionary of loose metadata.</summary>
		/// <param name="metadata">A collection of loose metadata.</param>
		/// <typeparam name="TMetadataView">The type of the metadata view object to get.</typeparam>
		/// <returns>A metadata view containing the specified metadata.</returns>
		public static TMetadataView GetMetadataView<TMetadataView>(IDictionary<string, object> metadata)
		{
			Requires.NotNull(metadata, "metadata");
			return MetadataViewProvider.GetMetadataView<TMetadataView>(metadata);
		}

		/// <summary>Creates a composable part from the specified attributed object.</summary>
		/// <param name="attributedPart">The attributed object.</param>
		/// <returns>The created part.</returns>
		public static ComposablePart CreatePart(object attributedPart)
		{
			Requires.NotNull(attributedPart, "attributedPart");
			return AttributedModelDiscovery.CreatePart(attributedPart);
		}

		/// <summary>Creates a composable part from the specified attributed object, using the specified reflection context.</summary>
		/// <param name="attributedPart">The attributed object.</param>
		/// <param name="reflectionContext">The reflection context for the part.</param>
		/// <returns>The created part.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reflectionContext" /> is <see langword="null" />.</exception>
		public static ComposablePart CreatePart(object attributedPart, ReflectionContext reflectionContext)
		{
			Requires.NotNull(attributedPart, "attributedPart");
			Requires.NotNull(reflectionContext, "reflectionContext");
			return AttributedModelDiscovery.CreatePart(attributedPart, reflectionContext);
		}

		/// <summary>Creates a composable part from the specified attributed object, using the specified part definition.</summary>
		/// <param name="partDefinition">The definition of the new part.</param>
		/// <param name="attributedPart">The attributed object.</param>
		/// <returns>The created part.</returns>
		public static ComposablePart CreatePart(ComposablePartDefinition partDefinition, object attributedPart)
		{
			Requires.NotNull(partDefinition, "partDefinition");
			Requires.NotNull(attributedPart, "attributedPart");
			return AttributedModelDiscovery.CreatePart((partDefinition as ReflectionComposablePartDefinition) ?? throw ExceptionBuilder.CreateReflectionModelInvalidPartDefinition("partDefinition", partDefinition.GetType()), attributedPart);
		}

		/// <summary>Creates a part definition with the specified type and origin.</summary>
		/// <param name="type">The type of the definition.</param>
		/// <param name="origin">The origin of the definition.</param>
		/// <returns>The new part definition.</returns>
		public static ComposablePartDefinition CreatePartDefinition(Type type, ICompositionElement origin)
		{
			Requires.NotNull(type, "type");
			return CreatePartDefinition(type, origin, ensureIsDiscoverable: false);
		}

		/// <summary>Creates a part definition with the specified type and origin.</summary>
		/// <param name="type">The type of the definition.</param>
		/// <param name="origin">The origin of the definition.</param>
		/// <param name="ensureIsDiscoverable">A value indicating whether or not the new definition should be discoverable.</param>
		/// <returns>The new part definition.</returns>
		public static ComposablePartDefinition CreatePartDefinition(Type type, ICompositionElement origin, bool ensureIsDiscoverable)
		{
			Requires.NotNull(type, "type");
			if (ensureIsDiscoverable)
			{
				return AttributedModelDiscovery.CreatePartDefinitionIfDiscoverable(type, origin);
			}
			return AttributedModelDiscovery.CreatePartDefinition(type, null, ignoreConstructorImports: false, origin);
		}

		/// <summary>Gets the unique identifier for the specified type.</summary>
		/// <param name="type">The type to examine.</param>
		/// <returns>The unique identifier for the type.</returns>
		public static string GetTypeIdentity(Type type)
		{
			Requires.NotNull(type, "type");
			return ContractNameServices.GetTypeIdentity(type);
		}

		/// <summary>Gets the unique identifier for the specified method.</summary>
		/// <param name="method">The method to examine.</param>
		/// <returns>The unique identifier for the method.</returns>
		public static string GetTypeIdentity(MethodInfo method)
		{
			Requires.NotNull(method, "method");
			return ContractNameServices.GetTypeIdentityFromMethod(method);
		}

		/// <summary>Gets a canonical contract name for the specified type.</summary>
		/// <param name="type">The type to use.</param>
		/// <returns>A contract name created from the specified type.</returns>
		public static string GetContractName(Type type)
		{
			Requires.NotNull(type, "type");
			return GetTypeIdentity(type);
		}

		/// <summary>Creates a part from the specified value and adds it to the specified batch.</summary>
		/// <param name="batch">The batch to add to.</param>
		/// <param name="exportedValue">The value to add.</param>
		/// <typeparam name="T">The type of the new part.</typeparam>
		/// <returns>The new part.</returns>
		public static ComposablePart AddExportedValue<T>(this CompositionBatch batch, T exportedValue)
		{
			Requires.NotNull(batch, "batch");
			string contractName = GetContractName(typeof(T));
			return batch.AddExportedValue(contractName, exportedValue);
		}

		/// <summary>Creates a part from the specified value and composes it in the specified composition container.</summary>
		/// <param name="container">The composition container to perform composition in.</param>
		/// <param name="exportedValue">The value to compose.</param>
		/// <typeparam name="T">The type of the new part.</typeparam>
		public static void ComposeExportedValue<T>(this CompositionContainer container, T exportedValue)
		{
			Requires.NotNull(container, "container");
			CompositionBatch batch = new CompositionBatch();
			batch.AddExportedValue(exportedValue);
			container.Compose(batch);
		}

		/// <summary>Creates a part from the specified value and adds it to the specified batch with the specified contract name.</summary>
		/// <param name="batch">The batch to add to.</param>
		/// <param name="contractName">The contract name of the export.</param>
		/// <param name="exportedValue">The value to add.</param>
		/// <typeparam name="T">The type of the new part.</typeparam>
		/// <returns>The new part.</returns>
		public static ComposablePart AddExportedValue<T>(this CompositionBatch batch, string contractName, T exportedValue)
		{
			Requires.NotNull(batch, "batch");
			string typeIdentity = GetTypeIdentity(typeof(T));
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("ExportTypeIdentity", typeIdentity);
			return batch.AddExport(new Export(contractName, dictionary, () => exportedValue));
		}

		/// <summary>Creates a part from the specified object under the specified contract name and composes it in the specified composition container.</summary>
		/// <param name="container">The composition container to perform composition in.</param>
		/// <param name="contractName">The contract name to export the part under.</param>
		/// <param name="exportedValue">The value to compose.</param>
		/// <typeparam name="T">The type of the new part.</typeparam>
		public static void ComposeExportedValue<T>(this CompositionContainer container, string contractName, T exportedValue)
		{
			Requires.NotNull(container, "container");
			CompositionBatch batch = new CompositionBatch();
			batch.AddExportedValue(contractName, exportedValue);
			container.Compose(batch);
		}

		/// <summary>Creates a composable part from the specified attributed object, and adds it to the specified composition batch.</summary>
		/// <param name="batch">The batch to add to.</param>
		/// <param name="attributedPart">The object to add.</param>
		/// <returns>The new part.</returns>
		public static ComposablePart AddPart(this CompositionBatch batch, object attributedPart)
		{
			Requires.NotNull(batch, "batch");
			Requires.NotNull(attributedPart, "attributedPart");
			ComposablePart composablePart = CreatePart(attributedPart);
			batch.AddPart(composablePart);
			return composablePart;
		}

		/// <summary>Creates composable parts from an array of attributed objects and composes them in the specified composition container.</summary>
		/// <param name="container">The composition container to perform composition in.</param>
		/// <param name="attributedParts">An array of attributed objects to compose.</param>
		public static void ComposeParts(this CompositionContainer container, params object[] attributedParts)
		{
			Requires.NotNull(container, "container");
			Requires.NotNullOrNullElements(attributedParts, "attributedParts");
			CompositionBatch batch = new CompositionBatch(attributedParts.Select((object attributedPart) => CreatePart(attributedPart)).ToArray(), Enumerable.Empty<ComposablePart>());
			container.Compose(batch);
		}

		/// <summary>Composes the specified part by using the specified composition service, with recomposition disabled.</summary>
		/// <param name="compositionService">The composition service to use.</param>
		/// <param name="attributedPart">The part to compose.</param>
		/// <returns>The composed part.</returns>
		public static ComposablePart SatisfyImportsOnce(this ICompositionService compositionService, object attributedPart)
		{
			Requires.NotNull(compositionService, "compositionService");
			Requires.NotNull(attributedPart, "attributedPart");
			ComposablePart composablePart = CreatePart(attributedPart);
			compositionService.SatisfyImportsOnce(composablePart);
			return composablePart;
		}

		/// <summary>Composes the specified part by using the specified composition service, with recomposition disabled and using the specified reflection context.</summary>
		/// <param name="compositionService">The composition service to use.</param>
		/// <param name="attributedPart">The part to compose.</param>
		/// <param name="reflectionContext">The reflection context for the part.</param>
		/// <returns>The composed part.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reflectionContext" /> is <see langword="null" />.</exception>
		public static ComposablePart SatisfyImportsOnce(this ICompositionService compositionService, object attributedPart, ReflectionContext reflectionContext)
		{
			Requires.NotNull(compositionService, "compositionService");
			Requires.NotNull(attributedPart, "attributedPart");
			Requires.NotNull(reflectionContext, "reflectionContext");
			ComposablePart composablePart = CreatePart(attributedPart, reflectionContext);
			compositionService.SatisfyImportsOnce(composablePart);
			return composablePart;
		}

		/// <summary>Returns a value that indicates whether the specified part contains an export that matches the specified contract type.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="contractType">The contract type.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains an export definition that matches <paramref name="contractType" />; otherwise, <see langword="false" />.</returns>
		public static bool Exports(this ComposablePartDefinition part, Type contractType)
		{
			Requires.NotNull(part, "part");
			Requires.NotNull(contractType, "contractType");
			return part.Exports(GetContractName(contractType));
		}

		/// <summary>Returns a value that indicates whether the specified part contains an export that matches the specified contract type.</summary>
		/// <param name="part">The part to search.</param>
		/// <typeparam name="T">The contract type.</typeparam>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains an export definition of type <paramref name="T" />; otherwise, <see langword="false" />.</returns>
		public static bool Exports<T>(this ComposablePartDefinition part)
		{
			Requires.NotNull(part, "part");
			return part.Exports(typeof(T));
		}

		/// <summary>Returns a value that indicates whether the specified part contains an import that matches the specified contract type.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="contractType">The contract type.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains an import definition that matches <paramref name="contractType" />; otherwise, <see langword="false" />.</returns>
		public static bool Imports(this ComposablePartDefinition part, Type contractType)
		{
			Requires.NotNull(part, "part");
			Requires.NotNull(contractType, "contractType");
			return part.Imports(GetContractName(contractType));
		}

		/// <summary>Returns a value that indicates whether the specified part contains an import that matches the specified contract type.</summary>
		/// <param name="part">The part to search.</param>
		/// <typeparam name="T">The contract type.</typeparam>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains an import definition of type <paramref name="T" />; otherwise, <see langword="false" />.</returns>
		public static bool Imports<T>(this ComposablePartDefinition part)
		{
			Requires.NotNull(part, "part");
			return part.Imports(typeof(T));
		}

		/// <summary>Returns a value that indicates whether the specified part contains an import that matches the specified contract type and import cardinality.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="contractType">The contract type.</param>
		/// <param name="importCardinality">The import cardinality.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains an import definition that matches <paramref name="contractType" /> and <paramref name="importCardinality" />; otherwise, <see langword="false" />.</returns>
		public static bool Imports(this ComposablePartDefinition part, Type contractType, ImportCardinality importCardinality)
		{
			Requires.NotNull(part, "part");
			Requires.NotNull(contractType, "contractType");
			return part.Imports(GetContractName(contractType), importCardinality);
		}

		/// <summary>Returns a value that indicates whether the specified part contains an import that matches the specified contract type and import cardinality.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="importCardinality">The import cardinality.</param>
		/// <typeparam name="T">The contract type.</typeparam>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains an import definition of type <paramref name="T" /> that has the specified import cardinality; otherwise, <see langword="false" />.</returns>
		public static bool Imports<T>(this ComposablePartDefinition part, ImportCardinality importCardinality)
		{
			Requires.NotNull(part, "part");
			return part.Imports(typeof(T), importCardinality);
		}
	}
	/// <summary>When applied to a <see cref="T:System.Reflection.Assembly" /> object, enables an <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object to discover custom <see cref="T:System.Reflection.ReflectionContext" /> objects.</summary>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = true)]
	public class CatalogReflectionContextAttribute : Attribute
	{
		private Type _reflectionContextType;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified <see cref="T:System.Reflection.ReflectionContext" /> type.</summary>
		/// <param name="reflectionContextType">The type of the reflection context.</param>
		public CatalogReflectionContextAttribute(Type reflectionContextType)
		{
			Requires.NotNull(reflectionContextType, "reflectionContextType");
			_reflectionContextType = reflectionContextType;
		}

		/// <summary>Creates an instance of the custom <see cref="T:System.Reflection.ReflectionContext" /> object.</summary>
		/// <returns>An instance of the custom reflection context.</returns>
		public ReflectionContext CreateReflectionContext()
		{
			Assumes.NotNull(_reflectionContextType);
			ReflectionContext reflectionContext = null;
			try
			{
				return (ReflectionContext)Activator.CreateInstance(_reflectionContextType);
			}
			catch (InvalidCastException innerException)
			{
				throw new InvalidOperationException(Strings.ReflectionContext_Type_Required, innerException);
			}
			catch (MissingMethodException inner)
			{
				throw new MissingMethodException(Strings.ReflectionContext_Requires_DefaultConstructor, inner);
			}
		}
	}
	/// <summary>An exception that indicates whether a part has been rejected during composition.</summary>
	[Serializable]
	public class ChangeRejectedException : CompositionException
	{
		/// <summary>Gets or sets the message associated with the component rejection.</summary>
		/// <returns>The message associated with the component rejection.</returns>
		public override string Message => string.Format(CultureInfo.CurrentCulture, Strings.CompositionException_ChangesRejected, base.Message);

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ChangeRejectedException" /> class with a system-supplied message that describes the error.</summary>
		public ChangeRejectedException()
			: this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ChangeRejectedException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		public ChangeRejectedException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ChangeRejectedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		public ChangeRejectedException(string message, Exception innerException)
			: base(message, innerException, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ChangeRejectedException" /> class with a list of composition errors.</summary>
		/// <param name="errors">A collection of errors that occurred during composition.</param>
		public ChangeRejectedException(IEnumerable<CompositionError> errors)
			: base(null, null, errors)
		{
		}
	}
	/// <summary>The exception that is thrown when the underlying exported value or metadata of a <see cref="T:System.Lazy`1" /> or <see cref="T:System.Lazy`2" /> object cannot be cast to T or TMetadataView, respectively.</summary>
	[Serializable]
	public class CompositionContractMismatchException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionContractMismatchException" /> class with a system-supplied message that describes the error.</summary>
		public CompositionContractMismatchException()
			: this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionContractMismatchException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		public CompositionContractMismatchException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionContractMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		public CompositionContractMismatchException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionContractMismatchException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		[SecuritySafeCritical]
		protected CompositionContractMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	/// <summary>Represents an error that occurred during composition.</summary>
	[Serializable]
	[DebuggerTypeProxy(typeof(CompositionErrorDebuggerProxy))]
	public class CompositionError
	{
		private readonly CompositionErrorId _id;

		private readonly string _description;

		private readonly Exception _exception;

		private readonly ICompositionElement _element;

		/// <summary>Gets the composition element that is the cause of the error.</summary>
		/// <returns>The composition element that is the cause of the <see cref="T:System.ComponentModel.Composition.CompositionError" />. The default is <see langword="null" />.</returns>
		public ICompositionElement Element => _element;

		/// <summary>Gets a description of the composition error.</summary>
		/// <returns>A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionError" />.</returns>
		public string Description => _description;

		/// <summary>Gets the exception that is the underlying cause of the composition error.</summary>
		/// <returns>The exception that is the underlying cause of the <see cref="T:System.ComponentModel.Composition.CompositionError" />. The default is <see langword="null" />.</returns>
		public Exception Exception => _exception;

		internal CompositionErrorId Id => _id;

		internal Exception InnerException => Exception;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> class with the specified error message.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Description" /> property to an empty string ("").</param>
		public CompositionError(string message)
			: this(CompositionErrorId.Unknown, message, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> class with the specified error message and the composition element that is the cause of the composition error.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Description" /> property to an empty string ("").</param>
		/// <param name="element">The composition element that is the cause of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Element" /> property to <see langword="null" />.</param>
		public CompositionError(string message, ICompositionElement element)
			: this(CompositionErrorId.Unknown, message, element, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> class with the specified error message and the exception that is the cause of the composition error.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Description" /> property to an empty string ("").</param>
		/// <param name="exception">The <see cref="P:System.ComponentModel.Composition.CompositionError.Exception" /> that is the underlying cause of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Exception" /> property to <see langword="null" />.</param>
		public CompositionError(string message, Exception exception)
			: this(CompositionErrorId.Unknown, message, null, exception)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> class with the specified error message, and the composition element and exception that are the cause of the composition error.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Description" /> property to an empty string ("").</param>
		/// <param name="element">The composition element that is the cause of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Element" /> property to <see langword="null" />.</param>
		/// <param name="exception">The <see cref="P:System.ComponentModel.Composition.CompositionError.Exception" /> that is the underlying cause of the <see cref="T:System.ComponentModel.Composition.CompositionError" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.CompositionError.Exception" /> property to <see langword="null" />.</param>
		public CompositionError(string message, ICompositionElement element, Exception exception)
			: this(CompositionErrorId.Unknown, message, element, exception)
		{
		}

		internal CompositionError(CompositionErrorId id, string description, ICompositionElement element, Exception exception)
		{
			_id = id;
			_description = description ?? string.Empty;
			_element = element;
			_exception = exception;
		}

		/// <summary>Returns a string representation of the composition error.</summary>
		/// <returns>A string that contains the <see cref="P:System.ComponentModel.Composition.CompositionError.Description" /> property.</returns>
		public override string ToString()
		{
			return Description;
		}

		internal static CompositionError Create(CompositionErrorId id, string format, params object[] parameters)
		{
			return Create(id, null, null, format, parameters);
		}

		internal static CompositionError Create(CompositionErrorId id, ICompositionElement element, string format, params object[] parameters)
		{
			return Create(id, element, null, format, parameters);
		}

		internal static CompositionError Create(CompositionErrorId id, ICompositionElement element, Exception exception, string format, params object[] parameters)
		{
			return new CompositionError(id, string.Format(CultureInfo.CurrentCulture, format, parameters), element, exception);
		}
	}
	internal class CompositionErrorDebuggerProxy
	{
		private readonly CompositionError _error;

		public string Description => _error.Description;

		public Exception Exception => _error.Exception;

		public ICompositionElement Element => _error.Element;

		public CompositionErrorDebuggerProxy(CompositionError error)
		{
			Requires.NotNull(error, "error");
			_error = error;
		}
	}
	internal enum CompositionErrorId
	{
		Unknown,
		InvalidExportMetadata,
		ImportNotSetOnPart,
		ImportEngine_ComposeTookTooManyIterations,
		ImportEngine_ImportCardinalityMismatch,
		ImportEngine_PartCycle,
		ImportEngine_PartCannotSetImport,
		ImportEngine_PartCannotGetExportedValue,
		ImportEngine_PartCannotActivate,
		ImportEngine_PreventedByExistingImport,
		ImportEngine_InvalidStateForRecomposition,
		ReflectionModel_ImportThrewException,
		ReflectionModel_ImportNotAssignableFromExport,
		ReflectionModel_ImportCollectionNull,
		ReflectionModel_ImportCollectionNotWritable,
		ReflectionModel_ImportCollectionConstructionThrewException,
		ReflectionModel_ImportCollectionGetThrewException,
		ReflectionModel_ImportCollectionIsReadOnlyThrewException,
		ReflectionModel_ImportCollectionClearThrewException,
		ReflectionModel_ImportCollectionAddThrewException,
		ReflectionModel_ImportManyOnParameterCanOnlyBeAssigned
	}
	/// <summary>Represents the exception that is thrown when one or more errors occur during composition in a <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object.</summary>
	[Serializable]
	[DebuggerDisplay("{Message}")]
	[DebuggerTypeProxy(typeof(CompositionExceptionDebuggerProxy))]
	public class CompositionException : Exception
	{
		[Serializable]
		private struct CompositionExceptionData : ISafeSerializationData
		{
			public CompositionError[] _errors;

			void ISafeSerializationData.CompleteDeserialization(object obj)
			{
				(obj as CompositionException)._errors = new ReadOnlyCollection<CompositionError>(_errors);
			}
		}

		private struct VisitContext
		{
			public Stack<CompositionError> Path;

			public Action<Stack<CompositionError>> LeafVisitor;
		}

		private const string ErrorsKey = "Errors";

		private ReadOnlyCollection<CompositionError> _errors;

		/// <summary>Gets or sets a collection of <see cref="T:System.ComponentModel.Composition.CompositionError" /> objects that describe the errors associated with the <see cref="T:System.ComponentModel.Composition.CompositionException" />.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Composition.CompositionError" /> objects that describe the errors associated with the <see cref="T:System.ComponentModel.Composition.CompositionException" />.</returns>
		public ReadOnlyCollection<CompositionError> Errors => _errors;

		/// <summary>Gets a message that describes the exception.</summary>
		/// <returns>A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionException" />.</returns>
		public override string Message
		{
			get
			{
				if (Errors.Count == 0)
				{
					return base.Message;
				}
				return BuildDefaultMessage();
			}
		}

		/// <summary>Gets a collection that contains the initial sources of this exception.</summary>
		/// <returns>A collection that contains the initial sources of this exception.</returns>
		public ReadOnlyCollection<Exception> RootCauses
		{
			get
			{
				//IL_0007: Expected O, but got I4
				Unity.ThrowStub.ThrowNotSupportedException();
				return (ReadOnlyCollection<Exception>)0;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionException" /> class.</summary>
		public CompositionException()
			: this(null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionException" /> class with the specified error message.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionException" /> or <see langword="null" /> to set the <see cref="P:System.Exception.Message" /> property to its default value.</param>
		public CompositionException(string message)
			: this(message, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionException" /> class with the specified error message and the exception that is the cause of this exception.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.CompositionException" /> or <see langword="null" /> to set the <see cref="P:System.Exception.Message" /> property to its default value.</param>
		/// <param name="innerException">The exception that is the underlying cause of the <see cref="T:System.ComponentModel.Composition.CompositionException" /> or <see langword="null" /> to set the <see cref="P:System.Exception.InnerException" /> property to <see langword="null" />.</param>
		public CompositionException(string message, Exception innerException)
			: this(message, innerException, null)
		{
		}

		internal CompositionException(CompositionError error)
			: this(new CompositionError[1] { error })
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionException" /> class with the specified collection of composition errors.</summary>
		/// <param name="errors">A collection of <see cref="T:System.ComponentModel.Composition.CompositionError" /> objects that represent problems during composition.</param>
		public CompositionException(IEnumerable<CompositionError> errors)
			: this(null, null, errors)
		{
		}

		internal CompositionException(string message, Exception innerException, IEnumerable<CompositionError> errors)
			: base(message, innerException)
		{
			Requires.NullOrNotNullElements(errors, "errors");
			base.SerializeObjectState += delegate(object exception, SafeSerializationEventArgs eventArgs)
			{
				CompositionExceptionData compositionExceptionData = default(CompositionExceptionData);
				if (_errors != null)
				{
					compositionExceptionData._errors = _errors.Select((CompositionError error) => new CompositionError(error.Id, error.Description, error.Element.ToSerializableElement(), error.Exception)).ToArray();
				}
				else
				{
					compositionExceptionData._errors = new CompositionError[0];
				}
				eventArgs.AddSerializedState(compositionExceptionData);
			};
			_errors = new ReadOnlyCollection<CompositionError>((errors == null) ? new CompositionError[0] : errors.ToArray());
		}

		private string BuildDefaultMessage()
		{
			IEnumerable<IEnumerable<CompositionError>> enumerable = CalculatePaths(this);
			StringBuilder stringBuilder = new StringBuilder();
			WriteHeader(stringBuilder, Errors.Count, enumerable.Count());
			WritePaths(stringBuilder, enumerable);
			return stringBuilder.ToString();
		}

		private static void WriteHeader(StringBuilder writer, int errorsCount, int pathCount)
		{
			if (errorsCount > 1 && pathCount > 1)
			{
				writer.AppendFormat(CultureInfo.CurrentCulture, Strings.CompositionException_MultipleErrorsWithMultiplePaths, pathCount);
			}
			else if (errorsCount == 1 && pathCount > 1)
			{
				writer.AppendFormat(CultureInfo.CurrentCulture, Strings.CompositionException_SingleErrorWithMultiplePaths, pathCount);
			}
			else
			{
				Assumes.IsTrue(errorsCount == 1);
				Assumes.IsTrue(pathCount == 1);
				writer.AppendFormat(CultureInfo.CurrentCulture, Strings.CompositionException_SingleErrorWithSinglePath, pathCount);
			}
			writer.Append(' ');
			writer.AppendLine(Strings.CompositionException_ReviewErrorProperty);
		}

		private static void WritePaths(StringBuilder writer, IEnumerable<IEnumerable<CompositionError>> paths)
		{
			int num = 0;
			foreach (IEnumerable<CompositionError> path in paths)
			{
				num++;
				WritePath(writer, path, num);
			}
		}

		private static void WritePath(StringBuilder writer, IEnumerable<CompositionError> path, int ordinal)
		{
			writer.AppendLine();
			writer.Append(ordinal.ToString(CultureInfo.CurrentCulture));
			writer.Append(Strings.CompositionException_PathsCountSeparator);
			writer.Append(' ');
			WriteError(writer, path.First());
			foreach (CompositionError item in path.Skip(1))
			{
				writer.AppendLine();
				writer.Append(Strings.CompositionException_ErrorPrefix);
				writer.Append(' ');
				WriteError(writer, item);
			}
		}

		private static void WriteError(StringBuilder writer, CompositionError error)
		{
			writer.AppendLine(error.Description);
			if (error.Element != null)
			{
				WriteElementGraph(writer, error.Element);
			}
		}

		private static void WriteElementGraph(StringBuilder writer, ICompositionElement element)
		{
			writer.AppendFormat(CultureInfo.CurrentCulture, Strings.CompositionException_ElementPrefix, element.DisplayName);
			while ((element = element.Origin) != null)
			{
				writer.AppendFormat(CultureInfo.CurrentCulture, Strings.CompositionException_OriginFormat, Strings.CompositionException_OriginSeparator, element.DisplayName);
			}
			writer.AppendLine();
		}

		private static IEnumerable<IEnumerable<CompositionError>> CalculatePaths(CompositionException exception)
		{
			List<IEnumerable<CompositionError>> paths = new List<IEnumerable<CompositionError>>();
			VisitCompositionException(exception, new VisitContext
			{
				Path = new Stack<CompositionError>(),
				LeafVisitor = delegate(Stack<CompositionError> path)
				{
					paths.Add(path.Copy());
				}
			});
			return paths;
		}

		private static void VisitCompositionException(CompositionException exception, VisitContext context)
		{
			foreach (CompositionError error in exception.Errors)
			{
				VisitError(error, context);
			}
			if (exception.InnerException != null)
			{
				VisitException(exception.InnerException, context);
			}
		}

		private static void VisitError(CompositionError error, VisitContext context)
		{
			context.Path.Push(error);
			if (error.Exception == null)
			{
				context.LeafVisitor(context.Path);
			}
			else
			{
				VisitException(error.Exception, context);
			}
			context.Path.Pop();
		}

		private static void VisitException(Exception exception, VisitContext context)
		{
			if (exception is CompositionException exception2)
			{
				VisitCompositionException(exception2, context);
			}
			else
			{
				VisitError(new CompositionError(exception.Message, exception.InnerException), context);
			}
		}
	}
	internal class CompositionExceptionDebuggerProxy
	{
		private readonly CompositionException _exception;

		public ReadOnlyCollection<Exception> Exceptions
		{
			get
			{
				List<Exception> list = new List<Exception>();
				foreach (CompositionError error in _exception.Errors)
				{
					if (error.Exception != null)
					{
						list.Add(error.Exception);
					}
				}
				return list.ToReadOnlyCollection();
			}
		}

		public string Message => _exception.Message;

		public ReadOnlyCollection<Exception> RootCauses
		{
			get
			{
				List<Exception> list = new List<Exception>();
				foreach (CompositionError error in _exception.Errors)
				{
					if (error.Exception == null)
					{
						continue;
					}
					if (error.Exception is CompositionException exception)
					{
						CompositionExceptionDebuggerProxy compositionExceptionDebuggerProxy = new CompositionExceptionDebuggerProxy(exception);
						if (compositionExceptionDebuggerProxy.RootCauses.Count > 0)
						{
							list.AddRange(compositionExceptionDebuggerProxy.RootCauses);
							continue;
						}
					}
					list.Add(error.Exception);
				}
				return list.ToReadOnlyCollection();
			}
		}

		public CompositionExceptionDebuggerProxy(CompositionException exception)
		{
			Requires.NotNull(exception, "exception");
			_exception = exception;
		}
	}
	internal struct CompositionResult
	{
		public static readonly CompositionResult SucceededResult;

		private readonly IEnumerable<CompositionError> _errors;

		public bool Succeeded
		{
			get
			{
				if (_errors != null)
				{
					return !_errors.FastAny();
				}
				return true;
			}
		}

		public IEnumerable<CompositionError> Errors => _errors ?? Enumerable.Empty<CompositionError>();

		public CompositionResult(params CompositionError[] errors)
			: this((IEnumerable<CompositionError>)errors)
		{
		}

		public CompositionResult(IEnumerable<CompositionError> errors)
		{
			_errors = errors;
		}

		public CompositionResult MergeResult(CompositionResult result)
		{
			if (Succeeded)
			{
				return result;
			}
			if (result.Succeeded)
			{
				return this;
			}
			return MergeErrors(result._errors);
		}

		public CompositionResult MergeError(CompositionError error)
		{
			return MergeErrors(new CompositionError[1] { error });
		}

		public CompositionResult MergeErrors(IEnumerable<CompositionError> errors)
		{
			return new CompositionResult(_errors.ConcatAllowingNull(errors));
		}

		public CompositionResult<T> ToResult<T>(T value)
		{
			return new CompositionResult<T>(value, _errors);
		}

		public void ThrowOnErrors()
		{
			ThrowOnErrors(null);
		}

		public void ThrowOnErrors(AtomicComposition atomicComposition)
		{
			if (!Succeeded)
			{
				if (atomicComposition == null)
				{
					throw new CompositionException(_errors);
				}
				throw new ChangeRejectedException(_errors);
			}
		}
	}
	internal struct CompositionResult<T>
	{
		private readonly IEnumerable<CompositionError> _errors;

		private readonly T _value;

		public bool Succeeded
		{
			get
			{
				if (_errors != null)
				{
					return !_errors.FastAny();
				}
				return true;
			}
		}

		public IEnumerable<CompositionError> Errors => _errors ?? Enumerable.Empty<CompositionError>();

		public T Value
		{
			get
			{
				ThrowOnErrors();
				return _value;
			}
		}

		public CompositionResult(T value)
			: this(value, null)
		{
		}

		public CompositionResult(params CompositionError[] errors)
			: this(default(T), errors)
		{
		}

		public CompositionResult(IEnumerable<CompositionError> errors)
			: this(default(T), errors)
		{
		}

		internal CompositionResult(T value, IEnumerable<CompositionError> errors)
		{
			_errors = errors;
			_value = value;
		}

		internal CompositionResult<TValue> ToResult<TValue>()
		{
			return new CompositionResult<TValue>(_errors);
		}

		internal CompositionResult ToResult()
		{
			return new CompositionResult(_errors);
		}

		private void ThrowOnErrors()
		{
			if (!Succeeded)
			{
				throw new CompositionException(_errors);
			}
		}
	}
	internal static class ConstraintServices
	{
		private static readonly PropertyInfo _exportDefinitionContractNameProperty = typeof(ExportDefinition).GetProperty("ContractName");

		private static readonly PropertyInfo _exportDefinitionMetadataProperty = typeof(ExportDefinition).GetProperty("Metadata");

		private static readonly MethodInfo _metadataContainsKeyMethod = typeof(IDictionary<string, object>).GetMethod("ContainsKey");

		private static readonly MethodInfo _metadataItemMethod = typeof(IDictionary<string, object>).GetMethod("get_Item");

		private static readonly MethodInfo _metadataEqualsMethod = typeof(object).GetMethod("Equals", new Type[1] { typeof(object) });

		private static readonly MethodInfo _typeIsInstanceOfTypeMethod = typeof(Type).GetMethod("IsInstanceOfType");

		public static Expression<Func<ExportDefinition, bool>> CreateConstraint(string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, CreationPolicy requiredCreationPolicy)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(ExportDefinition), "exportDefinition");
			Expression expression = CreateContractConstraintBody(contractName, parameterExpression);
			if (!string.IsNullOrEmpty(requiredTypeIdentity))
			{
				Expression right = CreateTypeIdentityContraint(requiredTypeIdentity, parameterExpression);
				expression = Expression.AndAlso(expression, right);
			}
			if (requiredMetadata != null)
			{
				Expression expression2 = CreateMetadataConstraintBody(requiredMetadata, parameterExpression);
				if (expression2 != null)
				{
					expression = Expression.AndAlso(expression, expression2);
				}
			}
			if (requiredCreationPolicy != CreationPolicy.Any)
			{
				Expression right2 = CreateCreationPolicyContraint(requiredCreationPolicy, parameterExpression);
				expression = Expression.AndAlso(expression, right2);
			}
			return Expression.Lambda<Func<ExportDefinition, bool>>(expression, new ParameterExpression[1] { parameterExpression });
		}

		private static Expression CreateContractConstraintBody(string contractName, ParameterExpression parameter)
		{
			Assumes.NotNull(parameter);
			return Expression.Equal(Expression.Property(parameter, _exportDefinitionContractNameProperty), Expression.Constant(contractName ?? string.Empty, typeof(string)));
		}

		private static Expression CreateMetadataConstraintBody(IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ParameterExpression parameter)
		{
			Assumes.NotNull(requiredMetadata);
			Assumes.NotNull(parameter);
			Expression expression = null;
			foreach (KeyValuePair<string, Type> requiredMetadatum in requiredMetadata)
			{
				Expression expression2 = CreateMetadataContainsKeyExpression(parameter, requiredMetadatum.Key);
				expression = ((expression != null) ? Expression.AndAlso(expression, expression2) : expression2);
				expression = Expression.AndAlso(expression, CreateMetadataOfTypeExpression(parameter, requiredMetadatum.Key, requiredMetadatum.Value));
			}
			return expression;
		}

		private static Expression CreateCreationPolicyContraint(CreationPolicy policy, ParameterExpression parameter)
		{
			Assumes.IsTrue(policy != CreationPolicy.Any);
			Assumes.NotNull(parameter);
			return Expression.MakeBinary(ExpressionType.OrElse, Expression.MakeBinary(ExpressionType.OrElse, Expression.Not(CreateMetadataContainsKeyExpression(parameter, "System.ComponentModel.Composition.CreationPolicy")), CreateMetadataValueEqualsExpression(parameter, CreationPolicy.Any, "System.ComponentModel.Composition.CreationPolicy")), CreateMetadataValueEqualsExpression(parameter, policy, "System.ComponentModel.Composition.CreationPolicy"));
		}

		private static Expression CreateTypeIdentityContraint(string requiredTypeIdentity, ParameterExpression parameter)
		{
			Assumes.NotNull(requiredTypeIdentity);
			Assumes.NotNull(parameter);
			return Expression.MakeBinary(ExpressionType.AndAlso, CreateMetadataContainsKeyExpression(parameter, "ExportTypeIdentity"), CreateMetadataValueEqualsExpression(parameter, requiredTypeIdentity, "ExportTypeIdentity"));
		}

		private static Expression CreateMetadataContainsKeyExpression(ParameterExpression parameter, string constantKey)
		{
			Assumes.NotNull(parameter, constantKey);
			return Expression.Call(Expression.Property(parameter, _exportDefinitionMetadataProperty), _metadataContainsKeyMethod, Expression.Constant(constantKey));
		}

		private static Expression CreateMetadataOfTypeExpression(ParameterExpression parameter, string constantKey, Type constantType)
		{
			Assumes.NotNull(parameter, constantKey);
			Assumes.NotNull(parameter, constantType);
			return Expression.Call(Expression.Constant(constantType, typeof(Type)), _typeIsInstanceOfTypeMethod, Expression.Call(Expression.Property(parameter, _exportDefinitionMetadataProperty), _metadataItemMethod, Expression.Constant(constantKey)));
		}

		private static Expression CreateMetadataValueEqualsExpression(ParameterExpression parameter, object constantValue, string metadataName)
		{
			Assumes.NotNull(parameter, constantValue);
			return Expression.Call(Expression.Constant(constantValue), _metadataEqualsMethod, Expression.Call(Expression.Property(parameter, _exportDefinitionMetadataProperty), _metadataItemMethod, Expression.Constant(metadataName)));
		}

		public static Expression<Func<ExportDefinition, bool>> CreatePartCreatorConstraint(Expression<Func<ExportDefinition, bool>> baseConstraint, ImportDefinition productImportDefinition)
		{
			ParameterExpression parameterExpression = baseConstraint.Parameters[0];
			Expression instance = Expression.Property(parameterExpression, _exportDefinitionMetadataProperty);
			Expression left = Expression.Call(instance, _metadataContainsKeyMethod, Expression.Constant("ProductDefinition"));
			Expression expression = Expression.Call(instance, _metadataItemMethod, Expression.Constant("ProductDefinition"));
			Expression right = Expression.Invoke(productImportDefinition.Constraint, Expression.Convert(expression, typeof(ExportDefinition)));
			return Expression.Lambda<Func<ExportDefinition, bool>>(Expression.AndAlso(baseConstraint.Body, Expression.AndAlso(left, right)), new ParameterExpression[1] { parameterExpression });
		}
	}
	internal static class ContractNameServices
	{
		private const char NamespaceSeparator = '.';

		private const char ArrayOpeningBracket = '[';

		private const char ArrayClosingBracket = ']';

		private const char ArraySeparator = ',';

		private const char PointerSymbol = '*';

		private const char ReferenceSymbol = '&';

		private const char GenericArityBackQuote = '`';

		private const char NestedClassSeparator = '+';

		private const char ContractNameGenericOpeningBracket = '(';

		private const char ContractNameGenericClosingBracket = ')';

		private const char ContractNameGenericArgumentSeparator = ',';

		private const char CustomModifiersSeparator = ' ';

		private const char GenericFormatOpeningBracket = '{';

		private const char GenericFormatClosingBracket = '}';

		[ThreadStatic]
		private static Dictionary<Type, string> typeIdentityCache;

		private static Dictionary<Type, string> TypeIdentityCache
		{
			get
			{
				Dictionary<Type, string> result = typeIdentityCache ?? new Dictionary<Type, string>();
				typeIdentityCache = result;
				return result;
			}
		}

		internal static string GetTypeIdentity(Type type)
		{
			return GetTypeIdentity(type, formatGenericName: true);
		}

		internal static string GetTypeIdentity(Type type, bool formatGenericName)
		{
			Assumes.NotNull(type);
			string value = null;
			if (!TypeIdentityCache.TryGetValue(type, out value))
			{
				if (!type.IsAbstract && type.IsSubclassOf(typeof(Delegate)))
				{
					value = GetTypeIdentityFromMethod(type.GetMethod("Invoke"));
				}
				else if (type.IsGenericParameter)
				{
					StringBuilder stringBuilder = new StringBuilder();
					WriteTypeArgument(stringBuilder, isDefinition: false, type, formatGenericName);
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
					value = stringBuilder.ToString();
				}
				else
				{
					StringBuilder stringBuilder2 = new StringBuilder();
					WriteTypeWithNamespace(stringBuilder2, type, formatGenericName);
					value = stringBuilder2.ToString();
				}
				Assumes.IsTrue(!string.IsNullOrEmpty(value));
				TypeIdentityCache.Add(type, value);
			}
			return value;
		}

		internal static string GetTypeIdentityFromMethod(MethodInfo method)
		{
			return GetTypeIdentityFromMethod(method, formatGenericName: true);
		}

		internal static string GetTypeIdentityFromMethod(MethodInfo method, bool formatGenericName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			WriteTypeWithNamespace(stringBuilder, method.ReturnType, formatGenericName);
			stringBuilder.Append("(");
			ParameterInfo[] parameters = method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(",");
				}
				WriteTypeWithNamespace(stringBuilder, parameters[i].ParameterType, formatGenericName);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		private static void WriteTypeWithNamespace(StringBuilder typeName, Type type, bool formatGenericName)
		{
			if (!string.IsNullOrEmpty(type.Namespace))
			{
				typeName.Append(type.Namespace);
				typeName.Append('.');
			}
			WriteType(typeName, type, formatGenericName);
		}

		private static void WriteType(StringBuilder typeName, Type type, bool formatGenericName)
		{
			if (type.IsGenericType)
			{
				Queue<Type> queue = new Queue<Type>(type.GetGenericArguments());
				WriteGenericType(typeName, type, type.IsGenericTypeDefinition, queue, formatGenericName);
				Assumes.IsTrue(queue.Count == 0, "Expecting genericTypeArguments queue to be empty.");
			}
			else
			{
				WriteNonGenericType(typeName, type, formatGenericName);
			}
		}

		private static void WriteNonGenericType(StringBuilder typeName, Type type, bool formatGenericName)
		{
			if (type.DeclaringType != null)
			{
				WriteType(typeName, type.DeclaringType, formatGenericName);
				typeName.Append('+');
			}
			if (type.IsArray)
			{
				WriteArrayType(typeName, type, formatGenericName);
			}
			else if (type.IsPointer)
			{
				WritePointerType(typeName, type, formatGenericName);
			}
			else if (type.IsByRef)
			{
				WriteByRefType(typeName, type, formatGenericName);
			}
			else
			{
				typeName.Append(type.Name);
			}
		}

		private static void WriteArrayType(StringBuilder typeName, Type type, bool formatGenericName)
		{
			Type type2 = FindArrayElementType(type);
			WriteType(typeName, type2, formatGenericName);
			Type type3 = type;
			do
			{
				WriteArrayTypeDimensions(typeName, type3);
			}
			while ((type3 = type3.GetElementType()) != null && type3.IsArray);
		}

		private static void WritePointerType(StringBuilder typeName, Type type, bool formatGenericName)
		{
			WriteType(typeName, type.GetElementType(), formatGenericName);
			typeName.Append('*');
		}

		private static void WriteByRefType(StringBuilder typeName, Type type, bool formatGenericName)
		{
			WriteType(typeName, type.GetElementType(), formatGenericName);
			typeName.Append('&');
		}

		private static void WriteArrayTypeDimensions(StringBuilder typeName, Type type)
		{
			typeName.Append('[');
			int arrayRank = type.GetArrayRank();
			for (int i = 1; i < arrayRank; i++)
			{
				typeName.Append(',');
			}
			typeName.Append(']');
		}

		private static void WriteGenericType(StringBuilder typeName, Type type, bool isDefinition, Queue<Type> genericTypeArguments, bool formatGenericName)
		{
			if (type.DeclaringType != null)
			{
				if (type.DeclaringType.IsGenericType)
				{
					WriteGenericType(typeName, type.DeclaringType, isDefinition, genericTypeArguments, formatGenericName);
				}
				else
				{
					WriteNonGenericType(typeName, type.DeclaringType, formatGenericName);
				}
				typeName.Append('+');
			}
			WriteGenericTypeName(typeName, type, isDefinition, genericTypeArguments, formatGenericName);
		}

		private static void WriteGenericTypeName(StringBuilder typeName, Type type, bool isDefinition, Queue<Type> genericTypeArguments, bool formatGenericName)
		{
			Assumes.IsTrue(type.IsGenericType, "Expecting type to be a generic type");
			int genericArity = GetGenericArity(type);
			string value = FindGenericTypeName(type.GetGenericTypeDefinition().Name);
			typeName.Append(value);
			WriteTypeArgumentsString(typeName, genericArity, isDefinition, genericTypeArguments, formatGenericName);
		}

		private static void WriteTypeArgumentsString(StringBuilder typeName, int argumentsCount, bool isDefinition, Queue<Type> genericTypeArguments, bool formatGenericName)
		{
			if (argumentsCount != 0)
			{
				typeName.Append('(');
				for (int i = 0; i < argumentsCount; i++)
				{
					Assumes.IsTrue(genericTypeArguments.Count > 0, "Expecting genericTypeArguments to contain at least one Type");
					Type genericTypeArgument = genericTypeArguments.Dequeue();
					WriteTypeArgument(typeName, isDefinition, genericTypeArgument, formatGenericName);
				}
				typeName.Remove(typeName.Length - 1, 1);
				typeName.Append(')');
			}
		}

		private static void WriteTypeArgument(StringBuilder typeName, bool isDefinition, Type genericTypeArgument, bool formatGenericName)
		{
			if (!isDefinition && !genericTypeArgument.IsGenericParameter)
			{
				WriteTypeWithNamespace(typeName, genericTypeArgument, formatGenericName);
			}
			if (formatGenericName && genericTypeArgument.IsGenericParameter)
			{
				typeName.Append('{');
				typeName.Append(genericTypeArgument.GenericParameterPosition);
				typeName.Append('}');
			}
			typeName.Append(',');
		}

		internal static void WriteCustomModifiers(StringBuilder typeName, string customKeyword, Type[] types, bool formatGenericName)
		{
			typeName.Append(' ');
			typeName.Append(customKeyword);
			Queue<Type> queue = new Queue<Type>(types);
			WriteTypeArgumentsString(typeName, types.Length, isDefinition: false, queue, formatGenericName);
			Assumes.IsTrue(queue.Count == 0, "Expecting genericTypeArguments queue to be empty.");
		}

		private static Type FindArrayElementType(Type type)
		{
			Type type2 = type;
			while ((type2 = type2.GetElementType()) != null && type2.IsArray)
			{
			}
			return type2;
		}

		private static string FindGenericTypeName(string genericName)
		{
			int num = genericName.IndexOf('`');
			if (num > -1)
			{
				genericName = genericName.Substring(0, num);
			}
			return genericName;
		}

		private static int GetGenericArity(Type type)
		{
			if (type.DeclaringType == null)
			{
				return type.GetGenericArguments().Length;
			}
			int num = type.DeclaringType.GetGenericArguments().Length;
			int num2 = type.GetGenericArguments().Length;
			Assumes.IsTrue(num2 >= num);
			return num2 - num;
		}
	}
	/// <summary>Specifies when and how a part will be instantiated.</summary>
	public enum CreationPolicy
	{
		/// <summary>Specifies that the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> will use the most appropriate <see cref="T:System.ComponentModel.Composition.CreationPolicy" /> for the part given the current context. This is the default <see cref="T:System.ComponentModel.Composition.CreationPolicy" />. By default, <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> will use <see cref="F:System.ComponentModel.Composition.CreationPolicy.Shared" />, unless the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> or importer requests <see cref="F:System.ComponentModel.Composition.CreationPolicy.NonShared" />.</summary>
		Any,
		/// <summary>Specifies that a single shared instance of the associated <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> will be created by the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> and shared by all requestors.</summary>
		Shared,
		/// <summary>Specifies that a new non-shared instance of the associated <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> will be created by the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> for every requestor.</summary>
		NonShared,
		NewScope
	}
	internal static class ErrorBuilder
	{
		public static CompositionError PreventedByExistingImport(ComposablePart part, ImportDefinition import)
		{
			return CompositionError.Create(CompositionErrorId.ImportEngine_PreventedByExistingImport, Strings.ImportEngine_PreventedByExistingImport, import.ToElement().DisplayName, part.ToElement().DisplayName);
		}

		public static CompositionError InvalidStateForRecompposition(ComposablePart part)
		{
			return CompositionError.Create(CompositionErrorId.ImportEngine_InvalidStateForRecomposition, Strings.ImportEngine_InvalidStateForRecomposition, part.ToElement().DisplayName);
		}

		public static CompositionError ComposeTookTooManyIterations(int maximumNumberOfCompositionIterations)
		{
			return CompositionError.Create(CompositionErrorId.ImportEngine_ComposeTookTooManyIterations, Strings.ImportEngine_ComposeTookTooManyIterations, maximumNumberOfCompositionIterations);
		}

		public static CompositionError CreateImportCardinalityMismatch(ImportCardinalityMismatchException exception, ImportDefinition definition)
		{
			Assumes.NotNull(exception, definition);
			return CompositionError.Create(CompositionErrorId.ImportEngine_ImportCardinalityMismatch, exception.Message, definition.ToElement(), null);
		}

		public static CompositionError CreatePartCannotActivate(ComposablePart part, Exception innerException)
		{
			Assumes.NotNull(part, innerException);
			ICompositionElement compositionElement = part.ToElement();
			return CompositionError.Create(CompositionErrorId.ImportEngine_PartCannotActivate, compositionElement, innerException, Strings.ImportEngine_PartCannotActivate, compositionElement.DisplayName);
		}

		public static CompositionError CreatePartCannotSetImport(ComposablePart part, ImportDefinition definition, Exception innerException)
		{
			Assumes.NotNull(part, definition, innerException);
			ICompositionElement compositionElement = definition.ToElement();
			return CompositionError.Create(CompositionErrorId.ImportEngine_PartCannotSetImport, compositionElement, innerException, Strings.ImportEngine_PartCannotSetImport, compositionElement.DisplayName, part.ToElement().DisplayName);
		}

		public static CompositionError CreateCannotGetExportedValue(ComposablePart part, ExportDefinition definition, Exception innerException)
		{
			Assumes.NotNull(part, definition, innerException);
			ICompositionElement compositionElement = definition.ToElement();
			return CompositionError.Create(CompositionErrorId.ImportEngine_PartCannotGetExportedValue, compositionElement, innerException, Strings.ImportEngine_PartCannotGetExportedValue, compositionElement.DisplayName, part.ToElement().DisplayName);
		}

		public static CompositionError CreatePartCycle(ComposablePart part)
		{
			Assumes.NotNull(part);
			ICompositionElement compositionElement = part.ToElement();
			return CompositionError.Create(CompositionErrorId.ImportEngine_PartCycle, compositionElement, Strings.ImportEngine_PartCycle, compositionElement.DisplayName);
		}
	}
	internal static class ExceptionBuilder
	{
		public static Exception CreateDiscoveryException(string messageFormat, params string[] arguments)
		{
			return new InvalidOperationException(Format(messageFormat, arguments));
		}

		public static ArgumentException CreateContainsNullElement(string parameterName)
		{
			Assumes.NotNull(parameterName);
			return new ArgumentException(Format(Strings.Argument_NullElement, parameterName), parameterName);
		}

		public static ObjectDisposedException CreateObjectDisposed(object instance)
		{
			Assumes.NotNull(instance);
			return new ObjectDisposedException(instance.GetType().ToString());
		}

		public static NotImplementedException CreateNotOverriddenByDerived(string memberName)
		{
			Assumes.NotNullOrEmpty(memberName);
			return new NotImplementedException(Format(Strings.NotImplemented_NotOverriddenByDerived, memberName));
		}

		public static ArgumentException CreateExportDefinitionNotOnThisComposablePart(string parameterName)
		{
			Assumes.NotNullOrEmpty(parameterName);
			return new ArgumentException(Format(Strings.ExportDefinitionNotOnThisComposablePart, parameterName), parameterName);
		}

		public static ArgumentException CreateImportDefinitionNotOnThisComposablePart(string parameterName)
		{
			Assumes.NotNullOrEmpty(parameterName);
			return new ArgumentException(Format(Strings.ImportDefinitionNotOnThisComposablePart, parameterName), parameterName);
		}

		public static CompositionException CreateCannotGetExportedValue(ComposablePart part, ExportDefinition definition, Exception innerException)
		{
			Assumes.NotNull(part, definition, innerException);
			return new CompositionException(ErrorBuilder.CreateCannotGetExportedValue(part, definition, innerException));
		}

		public static ArgumentException CreateReflectionModelInvalidPartDefinition(string parameterName, Type partDefinitionType)
		{
			Assumes.NotNullOrEmpty(parameterName);
			Assumes.NotNull(partDefinitionType);
			return new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidPartDefinition, partDefinitionType), parameterName);
		}

		public static ArgumentException ExportFactory_TooManyGenericParameters(string typeName)
		{
			Assumes.NotNullOrEmpty(typeName);
			return new ArgumentException(Format(Strings.ExportFactory_TooManyGenericParameters, typeName), typeName);
		}

		private static string Format(string format, params string[] arguments)
		{
			return string.Format(CultureInfo.CurrentCulture, format, arguments);
		}
	}
	/// <summary>Specifies that a type, property, field, or method provides a particular export.</summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	public class ExportAttribute : Attribute
	{
		/// <summary>Gets the contract name that is used to export the type or member marked with this attribute.</summary>
		/// <returns>The contract name that is used to export the type or member marked with this attribute. The default value is an empty string ("").</returns>
		public string ContractName { get; private set; }

		/// <summary>Gets the contract type that is exported by the member that this attribute is attached to.</summary>
		/// <returns>The type of export that is be provided. The default value is <see langword="null" />, which means that the type will be obtained by looking at the type on the member that this export is attached to.</returns>
		public Type ContractType { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportAttribute" /> class, exporting the type or member marked with this attribute under the default contract name.</summary>
		public ExportAttribute()
			: this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportAttribute" /> class, exporting the type or member marked with this attribute under a contract name derived from the specified type.</summary>
		/// <param name="contractType">A type from which to derive the contract name that is used to export the type or member marked with this attribute, or <see langword="null" /> to use the default contract name.</param>
		public ExportAttribute(Type contractType)
			: this(null, contractType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportAttribute" /> class, exporting the type or member marked with this attribute under the specified contract name.</summary>
		/// <param name="contractName">The contract name that is used to export the type or member marked with this attribute, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		public ExportAttribute(string contractName)
			: this(contractName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportAttribute" /> class, exporting the specified type under the specified contract name.</summary>
		/// <param name="contractName">The contract name that is used to export the type or member marked with this attribute, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		/// <param name="contractType">The type to export.</param>
		public ExportAttribute(string contractName, Type contractType)
		{
			ContractName = contractName;
			ContractType = contractType;
		}
	}
	internal enum ExportCardinalityCheckResult
	{
		Match,
		NoExports,
		TooManyExports
	}
	/// <summary>A factory that creates new instances of a part that provides the specified export.</summary>
	/// <typeparam name="T">The type of the export.</typeparam>
	public class ExportFactory<T>
	{
		private Func<Tuple<T, Action>> _exportLifetimeContextCreator;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportFactory`1" /> class.</summary>
		/// <param name="exportLifetimeContextCreator">A function that returns the exported value and an <see cref="T:System.Action" /> that releases it.</param>
		public ExportFactory(Func<Tuple<T, Action>> exportLifetimeContextCreator)
		{
			if (exportLifetimeContextCreator == null)
			{
				throw new ArgumentNullException("exportLifetimeContextCreator");
			}
			_exportLifetimeContextCreator = exportLifetimeContextCreator;
		}

		/// <summary>Creates an instance of the factory's export type.</summary>
		/// <returns>A valid instance of the factory's exported type.</returns>
		public ExportLifetimeContext<T> CreateExport()
		{
			Tuple<T, Action> tuple = _exportLifetimeContextCreator();
			return new ExportLifetimeContext<T>(tuple.Item1, tuple.Item2);
		}

		internal bool IncludeInScopedCatalog(ComposablePartDefinition composablePartDefinition)
		{
			return OnFilterScopedCatalog(composablePartDefinition);
		}

		protected virtual bool OnFilterScopedCatalog(ComposablePartDefinition composablePartDefinition)
		{
			return true;
		}
	}
	/// <summary>A factory that creates new instances of a part that provides the specified export, with attached metadata.</summary>
	/// <typeparam name="T">The type of the created part.</typeparam>
	/// <typeparam name="TMetadata">The type of the created part's metadata.</typeparam>
	public class ExportFactory<T, TMetadata> : ExportFactory<T>
	{
		private readonly TMetadata _metadata;

		/// <summary>Gets the metadata to be attached to the created parts.</summary>
		/// <returns>A metadata object that will be attached to the created parts.</returns>
		public TMetadata Metadata => _metadata;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportFactory`2" /> class.</summary>
		/// <param name="exportLifetimeContextCreator">A function that returns the exported value and an <see cref="T:System.Action" /> that releases it.</param>
		/// <param name="metadata">The metadata to attach to the created parts.</param>
		public ExportFactory(Func<Tuple<T, Action>> exportLifetimeContextCreator, TMetadata metadata)
			: base(exportLifetimeContextCreator)
		{
			_metadata = metadata;
		}
	}
	/// <summary>Holds an exported value created by an <see cref="T:System.ComponentModel.Composition.ExportFactory`1" /> object and a reference to a method to release that object.</summary>
	/// <typeparam name="T">The type of the exported value.</typeparam>
	public sealed class ExportLifetimeContext<T> : IDisposable
	{
		private readonly T _value;

		private readonly Action _disposeAction;

		/// <summary>Gets the exported value of a <see cref="T:System.ComponentModel.Composition.ExportFactory`1" /> object.</summary>
		/// <returns>The exported value.</returns>
		public T Value => _value;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportLifetimeContext`1" /> class.</summary>
		/// <param name="value">The exported value.</param>
		/// <param name="disposeAction">A reference to a method to release the object.</param>
		public ExportLifetimeContext(T value, Action disposeAction)
		{
			_value = value;
			_disposeAction = disposeAction;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.ExportLifetimeContext`1" /> class, including its associated export.</summary>
		public void Dispose()
		{
			if (_disposeAction != null)
			{
				_disposeAction();
			}
		}
	}
	/// <summary>Specifies metadata for a type, property, field, or method marked with the <see cref="T:System.ComponentModel.Composition.ExportAttribute" />.</summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	public sealed class ExportMetadataAttribute : Attribute
	{
		/// <summary>Gets the name of the metadata value.</summary>
		/// <returns>A string that contains the name of the metadata value.</returns>
		public string Name { get; private set; }

		/// <summary>Gets the metadata value.</summary>
		/// <returns>An object that contains the metadata value.</returns>
		public object Value { get; private set; }

		/// <summary>Gets or sets a value that indicates whether this item is marked with this attribute more than once.</summary>
		/// <returns>
		///   <see langword="true" /> if the item is marked more than once; otherwise, <see langword="false" />.</returns>
		public bool IsMultiple { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportMetadataAttribute" /> with the specified name and metadata value.</summary>
		/// <param name="name">A string that contains the name of the metadata value, or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.ExportMetadataAttribute.Name" /> property to an empty string ("").</param>
		/// <param name="value">An object that contains the metadata value. This can be <see langword="null" />.</param>
		public ExportMetadataAttribute(string name, object value)
		{
			Name = name ?? string.Empty;
			Value = value;
		}
	}
	internal static class ExportServices
	{
		private sealed class DisposableLazy<T, TMetadataView> : Lazy<T, TMetadataView>, IDisposable
		{
			private IDisposable _disposable;

			public DisposableLazy(Func<T> valueFactory, TMetadataView metadataView, IDisposable disposable, LazyThreadSafetyMode mode)
				: base(valueFactory, metadataView, mode)
			{
				Assumes.NotNull(disposable);
				_disposable = disposable;
			}

			void IDisposable.Dispose()
			{
				_disposable.Dispose();
			}
		}

		private sealed class DisposableLazy<T> : Lazy<T>, IDisposable
		{
			private IDisposable _disposable;

			public DisposableLazy(Func<T> valueFactory, IDisposable disposable, LazyThreadSafetyMode mode)
				: base(valueFactory, mode)
			{
				Assumes.NotNull(disposable);
				_disposable = disposable;
			}

			void IDisposable.Dispose()
			{
				_disposable.Dispose();
			}
		}

		private static readonly MethodInfo _createStronglyTypedLazyOfTM = typeof(ExportServices).GetMethod("CreateStronglyTypedLazyOfTM", BindingFlags.Static | BindingFlags.NonPublic);

		private static readonly MethodInfo _createStronglyTypedLazyOfT = typeof(ExportServices).GetMethod("CreateStronglyTypedLazyOfT", BindingFlags.Static | BindingFlags.NonPublic);

		private static readonly MethodInfo _createSemiStronglyTypedLazy = typeof(ExportServices).GetMethod("CreateSemiStronglyTypedLazy", BindingFlags.Static | BindingFlags.NonPublic);

		internal static readonly Type DefaultMetadataViewType = typeof(IDictionary<string, object>);

		internal static readonly Type DefaultExportedValueType = typeof(object);

		internal static bool IsDefaultMetadataViewType(Type metadataViewType)
		{
			Assumes.NotNull(metadataViewType);
			return metadataViewType.IsAssignableFrom(DefaultMetadataViewType);
		}

		internal static bool IsDictionaryConstructorViewType(Type metadataViewType)
		{
			Assumes.NotNull(metadataViewType);
			return metadataViewType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, Type.DefaultBinder, new Type[1] { typeof(IDictionary<string, object>) }, new ParameterModifier[0]) != null;
		}

		internal static Func<Export, object> CreateStronglyTypedLazyFactory(Type exportType, Type metadataViewType)
		{
			MethodInfo methodInfo = null;
			methodInfo = ((!(metadataViewType != null)) ? _createStronglyTypedLazyOfT.MakeGenericMethod(exportType ?? DefaultExportedValueType) : _createStronglyTypedLazyOfTM.MakeGenericMethod(exportType ?? DefaultExportedValueType, metadataViewType));
			Assumes.NotNull(methodInfo);
			return (Func<Export, object>)Delegate.CreateDelegate(typeof(Func<Export, object>), methodInfo);
		}

		internal static Func<Export, Lazy<object, object>> CreateSemiStronglyTypedLazyFactory(Type exportType, Type metadataViewType)
		{
			MethodInfo methodInfo = _createSemiStronglyTypedLazy.MakeGenericMethod(exportType ?? DefaultExportedValueType, metadataViewType ?? DefaultMetadataViewType);
			Assumes.NotNull(methodInfo);
			return (Func<Export, Lazy<object, object>>)Delegate.CreateDelegate(typeof(Func<Export, Lazy<object, object>>), methodInfo);
		}

		internal static Lazy<T, M> CreateStronglyTypedLazyOfTM<T, M>(Export export)
		{
			if (export is IDisposable disposable)
			{
				return new DisposableLazy<T, M>(() => GetCastedExportedValue<T>(export), AttributedModelServices.GetMetadataView<M>(export.Metadata), disposable, LazyThreadSafetyMode.PublicationOnly);
			}
			return new Lazy<T, M>(() => GetCastedExportedValue<T>(export), AttributedModelServices.GetMetadataView<M>(export.Metadata), LazyThreadSafetyMode.PublicationOnly);
		}

		internal static Lazy<T> CreateStronglyTypedLazyOfT<T>(Export export)
		{
			if (export is IDisposable disposable)
			{
				return new DisposableLazy<T>(() => GetCastedExportedValue<T>(export), disposable, LazyThreadSafetyMode.PublicationOnly);
			}
			return new Lazy<T>(() => GetCastedExportedValue<T>(export), LazyThreadSafetyMode.PublicationOnly);
		}

		internal static Lazy<object, object> CreateSemiStronglyTypedLazy<T, M>(Export export)
		{
			if (export is IDisposable disposable)
			{
				return new DisposableLazy<object, object>(() => GetCastedExportedValue<T>(export), AttributedModelServices.GetMetadataView<M>(export.Metadata), disposable, LazyThreadSafetyMode.PublicationOnly);
			}
			return new Lazy<object, object>(() => GetCastedExportedValue<T>(export), AttributedModelServices.GetMetadataView<M>(export.Metadata), LazyThreadSafetyMode.PublicationOnly);
		}

		internal static T GetCastedExportedValue<T>(Export export)
		{
			return CastExportedValue<T>(export.ToElement(), export.Value);
		}

		internal static T CastExportedValue<T>(ICompositionElement element, object exportedValue)
		{
			object result = null;
			if (!ContractServices.TryCast(typeof(T), exportedValue, out result))
			{
				throw new CompositionContractMismatchException(string.Format(CultureInfo.CurrentCulture, Strings.ContractMismatch_ExportedValueCannotBeCastToT, element.DisplayName, typeof(T)));
			}
			return (T)result;
		}

		internal static ExportCardinalityCheckResult CheckCardinality<T>(ImportDefinition definition, IEnumerable<T> enumerable)
		{
			return MatchCardinality(enumerable?.GetCardinality() ?? EnumerableCardinality.Zero, definition.Cardinality);
		}

		private static ExportCardinalityCheckResult MatchCardinality(EnumerableCardinality actualCardinality, ImportCardinality importCardinality)
		{
			switch (actualCardinality)
			{
			case EnumerableCardinality.Zero:
				if (importCardinality == ImportCardinality.ExactlyOne)
				{
					return ExportCardinalityCheckResult.NoExports;
				}
				break;
			case EnumerableCardinality.TwoOrMore:
				if (importCardinality.IsAtMostOne())
				{
					return ExportCardinalityCheckResult.TooManyExports;
				}
				break;
			default:
				Assumes.IsTrue(actualCardinality == EnumerableCardinality.One);
				break;
			}
			return ExportCardinalityCheckResult.Match;
		}
	}
	internal interface IAttributedImport
	{
		string ContractName { get; }

		Type ContractType { get; }

		bool AllowRecomposition { get; }

		CreationPolicy RequiredCreationPolicy { get; }

		ImportCardinality Cardinality { get; }

		ImportSource Source { get; }
	}
	/// <summary>Provides methods to satisfy imports on an existing part instance.</summary>
	public interface ICompositionService
	{
		/// <summary>Composes the specified part, with recomposition and validation disabled.</summary>
		/// <param name="part">The part to compose.</param>
		void SatisfyImportsOnce(ComposablePart part);
	}
	/// <summary>Notifies a part when its imports have been satisfied.</summary>
	public interface IPartImportsSatisfiedNotification
	{
		/// <summary>Called when a part's imports have been satisfied and it is safe to use.</summary>
		void OnImportsSatisfied();
	}
	/// <summary>Specifies that a property, field, or parameter value should be provided by the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.object</summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class ImportAttribute : Attribute, IAttributedImport
	{
		/// <summary>Gets the contract name of the export to import.</summary>
		/// <returns>The contract name of the export to import. The default is an empty string ("").</returns>
		public string ContractName { get; private set; }

		/// <summary>Gets the type of the export to import.</summary>
		/// <returns>The type of the export to import.</returns>
		public Type ContractType { get; private set; }

		/// <summary>Gets or sets a value that indicates whether the property, field, or parameter will be set to its type's default value when an export with the contract name is not present in the container.</summary>
		/// <returns>
		///   <see langword="true" /> if the property, field, or parameter will be set to its type's default value when there is no export with the <see cref="P:System.ComponentModel.Composition.ImportAttribute.ContractName" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public bool AllowDefault { get; set; }

		/// <summary>Gets or sets a value that indicates whether the property or field will be recomposed when exports with a matching contract have changed in the container.</summary>
		/// <returns>
		///   <see langword="true" /> if the property or field allows recomposition when exports with a matching <see cref="P:System.ComponentModel.Composition.ImportAttribute.ContractName" /> are added or removed from the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public bool AllowRecomposition { get; set; }

		/// <summary>Gets or sets a value that indicates that the importer requires a specific <see cref="T:System.ComponentModel.Composition.CreationPolicy" /> for the exports used to satisfy this import.</summary>
		/// <returns>One of the following values:  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.Any" />, if the importer does not require a specific <see cref="T:System.ComponentModel.Composition.CreationPolicy" />. This is the default.  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.Shared" /> to require that all used exports be shared by all parts in the container.  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.NonShared" /> to require that all used exports be non-shared in a container. In this case, each part receives their own instance.</returns>
		public CreationPolicy RequiredCreationPolicy { get; set; }

		/// <summary>Gets or sets a value that specifies the scopes from which this import may be satisfied.</summary>
		/// <returns>A value that specifies the scopes from which this import may be satisfied.</returns>
		public ImportSource Source { get; set; }

		ImportCardinality IAttributedImport.Cardinality
		{
			get
			{
				if (AllowDefault)
				{
					return ImportCardinality.ZeroOrOne;
				}
				return ImportCardinality.ExactlyOne;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportAttribute" /> class, importing the export with the default contract name.</summary>
		public ImportAttribute()
			: this((string)null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportAttribute" /> class, importing the export with the contract name derived from the specified type.</summary>
		/// <param name="contractType">The type to derive the contract name of the export from, or <see langword="null" /> to use the default contract name.</param>
		public ImportAttribute(Type contractType)
			: this(null, contractType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportAttribute" /> class, importing the export with the specified contract name.</summary>
		/// <param name="contractName">The contract name of the export to import, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		public ImportAttribute(string contractName)
			: this(contractName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportAttribute" /> class, importing the export with the specified contract name and type.</summary>
		/// <param name="contractName">The contract name of the export to import, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		/// <param name="contractType">The type of the export to import.</param>
		public ImportAttribute(string contractName, Type contractType)
		{
			ContractName = contractName;
			ContractType = contractType;
		}
	}
	/// <summary>The exception that is thrown when the cardinality of an import is not compatible with the cardinality of the matching exports.</summary>
	[Serializable]
	[DebuggerTypeProxy(typeof(ImportCardinalityMismatchExceptionDebuggerProxy))]
	[DebuggerDisplay("{Message}")]
	public class ImportCardinalityMismatchException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException" /> class with a system-supplied message that describes the error.</summary>
		public ImportCardinalityMismatchException()
			: this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException" />, or <see langword="null" /> to set the <see cref="P:System.Exception.Message" /> property to its default value.</param>
		public ImportCardinalityMismatchException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		public ImportCardinalityMismatchException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException" /> class with serialized data.</summary>
		/// <param name="info">An object that holds the serialized object data about the <see cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException" />.</param>
		/// <param name="context">An object that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <paramref name="info" /> is missing a required value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="info" /> contains a value that cannot be cast to the correct type.</exception>
		[SecuritySafeCritical]
		protected ImportCardinalityMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	internal class ImportCardinalityMismatchExceptionDebuggerProxy
	{
		private readonly ImportCardinalityMismatchException _exception;

		public Exception InnerException => _exception.InnerException;

		public string Message => _exception.Message;

		public ImportCardinalityMismatchExceptionDebuggerProxy(ImportCardinalityMismatchException exception)
		{
			Requires.NotNull(exception, "exception");
			_exception = exception;
		}
	}
	/// <summary>Specifies that a property, field, or parameter should be populated with all matching exports by the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object.</summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class ImportManyAttribute : Attribute, IAttributedImport
	{
		/// <summary>Gets the contract name of the exports to import.</summary>
		/// <returns>The contract name of the exports to import. The default value is an empty string ("").</returns>
		public string ContractName { get; private set; }

		/// <summary>Gets the contract type of the export to import.</summary>
		/// <returns>The type of the export that this import is expecting. The default value is <see langword="null" />, which means that the type will be obtained by looking at the type on the member that this import is attached to. If the type is <see cref="T:System.Object" />, the import will match any exported type.</returns>
		public Type ContractType { get; private set; }

		/// <summary>Gets or sets a value indicating whether the decorated property or field will be recomposed when exports that provide the matching contract change.</summary>
		/// <returns>
		///   <see langword="true" /> if the property or field allows for recomposition when exports that provide the same <see cref="P:System.ComponentModel.Composition.ImportManyAttribute.ContractName" /> are added or removed from the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />; otherwise, <see langword="false" />.  
		/// The default value is <see langword="false" />.</returns>
		public bool AllowRecomposition { get; set; }

		/// <summary>Gets or sets a value that indicates that the importer requires a specific <see cref="T:System.ComponentModel.Composition.CreationPolicy" /> for the exports used to satisfy this import.</summary>
		/// <returns>One of the following values:  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.Any" />, if the importer does not require a specific <see cref="T:System.ComponentModel.Composition.CreationPolicy" />. This is the default.  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.Shared" /> to require that all used exports be shared by all parts in the container.  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.NonShared" /> to require that all used exports be non-shared in a container. In this case, each part receives their own instance.</returns>
		public CreationPolicy RequiredCreationPolicy { get; set; }

		/// <summary>Gets or sets a value that specifies the scopes from which this import may be satisfied.</summary>
		/// <returns>A value that specifies the scopes from which this import may be satisfied.</returns>
		public ImportSource Source { get; set; }

		ImportCardinality IAttributedImport.Cardinality => ImportCardinality.ZeroOrMore;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportManyAttribute" /> class, importing the set of exports with the default contract name.</summary>
		public ImportManyAttribute()
			: this((string)null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportManyAttribute" /> class, importing the set of exports with the contract name derived from the specified type.</summary>
		/// <param name="contractType">The type to derive the contract name of the exports to import, or <see langword="null" /> to use the default contract name.</param>
		public ImportManyAttribute(Type contractType)
			: this(null, contractType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportManyAttribute" /> class, importing the set of exports with the specified contract name.</summary>
		/// <param name="contractName">The contract name of the exports to import, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		public ImportManyAttribute(string contractName)
			: this(contractName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportManyAttribute" /> class, importing the set of exports with the specified contract name and contract type.</summary>
		/// <param name="contractName">The contract name of the exports to import, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		/// <param name="contractType">The type of the export to import.</param>
		public ImportManyAttribute(string contractName, Type contractType)
		{
			ContractName = contractName;
			ContractType = contractType;
		}
	}
	/// <summary>Specifies values that indicate how the MEF composition engine searches for imports.</summary>
	public enum ImportSource
	{
		/// <summary>Imports may be satisfied from the current scope or any ancestor scope.</summary>
		Any,
		/// <summary>Imports may be satisfied only from the current scope.</summary>
		Local,
		/// <summary>Imports may be satisfied only from an ancestor scope.</summary>
		NonLocal
	}
	/// <summary>Specifies which constructor should be used when creating a part.</summary>
	[AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
	public class ImportingConstructorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportingConstructorAttribute" /> class.</summary>
		public ImportingConstructorAttribute()
		{
		}
	}
	/// <summary>Specifies that a type provides a particular export, and that subclasses of that type will also provide that export.</summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public class InheritedExportAttribute : ExportAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.InheritedExportAttribute" /> class.</summary>
		public InheritedExportAttribute()
			: this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.InheritedExportAttribute" /> class with the specified contract type.</summary>
		/// <param name="contractType">The type of the contract.</param>
		public InheritedExportAttribute(Type contractType)
			: this(null, contractType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.InheritedExportAttribute" /> class with the specified contract name.</summary>
		/// <param name="contractName">The name of the contract.</param>
		public InheritedExportAttribute(string contractName)
			: this(contractName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.InheritedExportAttribute" /> class with the specified contract name and type.</summary>
		/// <param name="contractName">The name of the contract.</param>
		/// <param name="contractType">The type of the contract.</param>
		public InheritedExportAttribute(string contractName, Type contractType)
			: base(contractName, contractType)
		{
		}
	}
	/// <summary>Specifies that a custom attribute's properties provide metadata for exports applied to the same type, property, field, or method.</summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class MetadataAttributeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.MetadataAttributeAttribute" /> class.</summary>
		public MetadataAttributeAttribute()
		{
		}
	}
	internal static class MetadataServices
	{
		public static readonly IDictionary<string, object> EmptyMetadata = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>(0));

		public static IDictionary<string, object> AsReadOnly(this IDictionary<string, object> metadata)
		{
			if (metadata == null)
			{
				return EmptyMetadata;
			}
			if (metadata is ReadOnlyDictionary<string, object>)
			{
				return metadata;
			}
			return new ReadOnlyDictionary<string, object>(metadata);
		}

		public static T GetValue<T>(this IDictionary<string, object> metadata, string key)
		{
			Assumes.NotNull(metadata, "metadata");
			object value = true;
			if (!metadata.TryGetValue(key, out value))
			{
				return default(T);
			}
			if (value is T)
			{
				return (T)value;
			}
			return default(T);
		}
	}
	internal static class MetadataViewGenerator
	{
		public const string MetadataViewType = "MetadataViewType";

		public const string MetadataItemKey = "MetadataItemKey";

		public const string MetadataItemTargetType = "MetadataItemTargetType";

		public const string MetadataItemSourceType = "MetadataItemSourceType";

		public const string MetadataItemValue = "MetadataItemValue";

		private static Microsoft.Internal.Lock _lock = new Microsoft.Internal.Lock();

		private static Dictionary<Type, Type> _proxies = new Dictionary<Type, Type>();

		private static AssemblyName ProxyAssemblyName = new AssemblyName(string.Format(CultureInfo.InvariantCulture, "MetadataViewProxies_{0}", Guid.NewGuid()));

		private static ModuleBuilder transparentProxyModuleBuilder;

		private static Type[] CtorArgumentTypes = new Type[1] { typeof(IDictionary<string, object>) };

		private static MethodInfo _mdvDictionaryTryGet = CtorArgumentTypes[0].GetMethod("TryGetValue");

		private static readonly MethodInfo ObjectGetType = typeof(object).GetMethod("GetType", Type.EmptyTypes);

		private static AssemblyBuilder CreateProxyAssemblyBuilder(ConstructorInfo constructorInfo)
		{
			return AppDomain.CurrentDomain.DefineDynamicAssembly(ProxyAssemblyName, AssemblyBuilderAccess.Run);
		}

		private static ModuleBuilder GetProxyModuleBuilder(bool requiresCritical)
		{
			if (transparentProxyModuleBuilder == null)
			{
				transparentProxyModuleBuilder = CreateProxyAssemblyBuilder(typeof(SecurityTransparentAttribute).GetConstructor(Type.EmptyTypes)).DefineDynamicModule("MetadataViewProxiesModule");
			}
			return transparentProxyModuleBuilder;
		}

		public static Type GenerateView(Type viewType)
		{
			Assumes.NotNull(viewType);
			Assumes.IsTrue(viewType.IsInterface);
			bool flag;
			Type value;
			using (new ReadLock(_lock))
			{
				flag = _proxies.TryGetValue(viewType, out value);
			}
			if (!flag)
			{
				Type type = GenerateInterfaceViewProxyType(viewType);
				Assumes.NotNull(type);
				using (new WriteLock(_lock))
				{
					if (!_proxies.TryGetValue(viewType, out value))
					{
						value = type;
						_proxies.Add(viewType, value);
					}
				}
			}
			return value;
		}

		private static void GenerateLocalAssignmentFromDefaultAttribute(this ILGenerator IL, DefaultValueAttribute[] attrs, LocalBuilder local)
		{
			if (attrs.Length != 0)
			{
				DefaultValueAttribute defaultValueAttribute = attrs[0];
				IL.LoadValue(defaultValueAttribute.Value);
				if (defaultValueAttribute.Value != null && defaultValueAttribute.Value.GetType().IsValueType)
				{
					IL.Emit(OpCodes.Box, defaultValueAttribute.Value.GetType());
				}
				IL.Emit(OpCodes.Stloc, local);
			}
		}

		private static void GenerateFieldAssignmentFromLocalValue(this ILGenerator IL, LocalBuilder local, FieldBuilder field)
		{
			IL.Emit(OpCodes.Ldarg_0);
			IL.Emit(OpCodes.Ldloc, local);
			IL.Emit(field.FieldType.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, field.FieldType);
			IL.Emit(OpCodes.Stfld, field);
		}

		private static void GenerateLocalAssignmentFromFlag(this ILGenerator IL, LocalBuilder local, bool flag)
		{
			IL.Emit(flag ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
			IL.Emit(OpCodes.Stloc, local);
		}

		private static Type GenerateInterfaceViewProxyType(Type viewType)
		{
			Type[] interfaces = new Type[1] { viewType };
			TypeBuilder typeBuilder = GetProxyModuleBuilder(requiresCritical: false).DefineType(string.Format(CultureInfo.InvariantCulture, "_proxy_{0}_{1}", viewType.FullName, Guid.NewGuid()), TypeAttributes.Public, typeof(object), interfaces);
			ILGenerator iLGenerator = typeBuilder.CreateGeneratorForPublicConstructor(CtorArgumentTypes);
			LocalBuilder localBuilder = iLGenerator.DeclareLocal(typeof(Exception));
			LocalBuilder localBuilder2 = iLGenerator.DeclareLocal(typeof(IDictionary));
			LocalBuilder localBuilder3 = iLGenerator.DeclareLocal(typeof(Type));
			LocalBuilder localBuilder4 = iLGenerator.DeclareLocal(typeof(object));
			LocalBuilder local = iLGenerator.DeclareLocal(typeof(bool));
			Label label = iLGenerator.BeginExceptionBlock();
			foreach (PropertyInfo allProperty in viewType.GetAllProperties())
			{
				string fieldName = string.Format(CultureInfo.InvariantCulture, "_{0}_{1}", allProperty.Name, Guid.NewGuid());
				string text = string.Format(CultureInfo.InvariantCulture, "{0}", allProperty.Name);
				Type[] parameterTypes = new Type[1] { allProperty.PropertyType };
				Type[] returnTypeOptionalCustomModifiers = null;
				Type[] returnTypeRequiredCustomModifiers = null;
				FieldBuilder field = typeBuilder.DefineField(fieldName, allProperty.PropertyType, FieldAttributes.Private);
				PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(text, PropertyAttributes.None, allProperty.PropertyType, parameterTypes);
				Label label2 = iLGenerator.BeginExceptionBlock();
				DefaultValueAttribute[] attributes = allProperty.GetAttributes<DefaultValueAttribute>(inherit: false);
				if (attributes.Length != 0)
				{
					iLGenerator.BeginExceptionBlock();
				}
				Label label3 = iLGenerator.DefineLabel();
				iLGenerator.GenerateLocalAssignmentFromFlag(local, flag: true);
				iLGenerator.Emit(OpCodes.Ldarg_1);
				iLGenerator.Emit(OpCodes.Ldstr, allProperty.Name);
				iLGenerator.Emit(OpCodes.Ldloca, localBuilder4);
				iLGenerator.Emit(OpCodes.Callvirt, _mdvDictionaryTryGet);
				iLGenerator.Emit(OpCodes.Brtrue, label3);
				iLGenerator.GenerateLocalAssignmentFromFlag(local, flag: false);
				iLGenerator.GenerateLocalAssignmentFromDefaultAttribute(attributes, localBuilder4);
				iLGenerator.MarkLabel(label3);
				iLGenerator.GenerateFieldAssignmentFromLocalValue(localBuilder4, field);
				iLGenerator.Emit(OpCodes.Leave, label2);
				if (attributes.Length != 0)
				{
					iLGenerator.BeginCatchBlock(typeof(InvalidCastException));
					Label label4 = iLGenerator.DefineLabel();
					iLGenerator.Emit(OpCodes.Ldloc, local);
					iLGenerator.Emit(OpCodes.Brtrue, label4);
					iLGenerator.Emit(OpCodes.Rethrow);
					iLGenerator.MarkLabel(label4);
					iLGenerator.GenerateLocalAssignmentFromDefaultAttribute(attributes, localBuilder4);
					iLGenerator.GenerateFieldAssignmentFromLocalValue(localBuilder4, field);
					iLGenerator.EndExceptionBlock();
				}
				iLGenerator.BeginCatchBlock(typeof(NullReferenceException));
				iLGenerator.Emit(OpCodes.Stloc, localBuilder);
				iLGenerator.GetExceptionDataAndStoreInLocal(localBuilder, localBuilder2);
				iLGenerator.AddItemToLocalDictionary(localBuilder2, "MetadataItemKey", text);
				iLGenerator.AddItemToLocalDictionary(localBuilder2, "MetadataItemTargetType", allProperty.PropertyType);
				iLGenerator.Emit(OpCodes.Rethrow);
				iLGenerator.BeginCatchBlock(typeof(InvalidCastException));
				iLGenerator.Emit(OpCodes.Stloc, localBuilder);
				iLGenerator.GetExceptionDataAndStoreInLocal(localBuilder, localBuilder2);
				iLGenerator.AddItemToLocalDictionary(localBuilder2, "MetadataItemKey", text);
				iLGenerator.AddItemToLocalDictionary(localBuilder2, "MetadataItemTargetType", allProperty.PropertyType);
				iLGenerator.Emit(OpCodes.Rethrow);
				iLGenerator.EndExceptionBlock();
				if (allProperty.CanWrite)
				{
					throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Strings.InvalidSetterOnMetadataField, viewType, text));
				}
				if (allProperty.CanRead)
				{
					MethodBuilder methodBuilder = typeBuilder.DefineMethod(string.Format(CultureInfo.InvariantCulture, "get_{0}", text), MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask | MethodAttributes.SpecialName, CallingConventions.HasThis, allProperty.PropertyType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, Type.EmptyTypes, null, null);
					typeBuilder.DefineMethodOverride(methodBuilder, allProperty.GetGetMethod());
					ILGenerator iLGenerator2 = methodBuilder.GetILGenerator();
					iLGenerator2.Emit(OpCodes.Ldarg_0);
					iLGenerator2.Emit(OpCodes.Ldfld, field);
					iLGenerator2.Emit(OpCodes.Ret);
					propertyBuilder.SetGetMethod(methodBuilder);
				}
			}
			iLGenerator.Emit(OpCodes.Leave, label);
			iLGenerator.BeginCatchBlock(typeof(NullReferenceException));
			iLGenerator.Emit(OpCodes.Stloc, localBuilder);
			iLGenerator.GetExceptionDataAndStoreInLocal(localBuilder, localBuilder2);
			iLGenerator.AddItemToLocalDictionary(localBuilder2, "MetadataViewType", viewType);
			iLGenerator.Emit(OpCodes.Rethrow);
			iLGenerator.BeginCatchBlock(typeof(InvalidCastException));
			iLGenerator.Emit(OpCodes.Stloc, localBuilder);
			iLGenerator.GetExceptionDataAndStoreInLocal(localBuilder, localBuilder2);
			iLGenerator.Emit(OpCodes.Ldloc, localBuilder4);
			iLGenerator.Emit(OpCodes.Call, ObjectGetType);
			iLGenerator.Emit(OpCodes.Stloc, localBuilder3);
			iLGenerator.AddItemToLocalDictionary(localBuilder2, "MetadataViewType", viewType);
			iLGenerator.AddLocalToLocalDictionary(localBuilder2, "MetadataItemSourceType", localBuilder3);
			iLGenerator.AddLocalToLocalDictionary(localBuilder2, "MetadataItemValue", localBuilder4);
			iLGenerator.Emit(OpCodes.Rethrow);
			iLGenerator.EndExceptionBlock();
			iLGenerator.Emit(OpCodes.Ret);
			return typeBuilder.CreateType();
		}
	}
	/// <summary>Specifies the type used to implement a metadata view.</summary>
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class MetadataViewImplementationAttribute : Attribute
	{
		/// <summary>Gets the type of the metadata view.</summary>
		/// <returns>The type of the metadata view.</returns>
		public Type ImplementationType { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.MetadataViewImplementationAttribute" /> class.</summary>
		/// <param name="implementationType">The type of the metadata view.</param>
		public MetadataViewImplementationAttribute(Type implementationType)
		{
			ImplementationType = implementationType;
		}
	}
	internal static class MetadataViewProvider
	{
		public static TMetadataView GetMetadataView<TMetadataView>(IDictionary<string, object> metadata)
		{
			Assumes.NotNull(metadata);
			Type typeFromHandle = typeof(TMetadataView);
			if (typeFromHandle.IsAssignableFrom(typeof(IDictionary<string, object>)))
			{
				return (TMetadataView)metadata;
			}
			Type type;
			if (typeFromHandle.IsInterface)
			{
				if (!typeFromHandle.IsAttributeDefined<MetadataViewImplementationAttribute>())
				{
					try
					{
						type = MetadataViewGenerator.GenerateView(typeFromHandle);
					}
					catch (TypeLoadException innerException)
					{
						throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Strings.NotSupportedInterfaceMetadataView, typeFromHandle.FullName), innerException);
					}
				}
				else
				{
					type = typeFromHandle.GetFirstAttribute<MetadataViewImplementationAttribute>().ImplementationType;
					if (type == null)
					{
						throw new CompositionContractMismatchException(string.Format(CultureInfo.CurrentCulture, Strings.ContractMismatch_MetadataViewImplementationCanNotBeNull, typeFromHandle.FullName, type.FullName));
					}
					if (!typeFromHandle.IsAssignableFrom(type))
					{
						throw new CompositionContractMismatchException(string.Format(CultureInfo.CurrentCulture, Strings.ContractMismatch_MetadataViewImplementationDoesNotImplementViewInterface, typeFromHandle.FullName, type.FullName));
					}
				}
			}
			else
			{
				type = typeFromHandle;
			}
			try
			{
				return (TMetadataView)type.SafeCreateInstance(metadata);
			}
			catch (MissingMethodException innerException2)
			{
				throw new CompositionContractMismatchException(string.Format(CultureInfo.CurrentCulture, Strings.CompositionException_MetadataViewInvalidConstructor, type.AssemblyQualifiedName), innerException2);
			}
			catch (TargetInvocationException ex)
			{
				if (typeFromHandle.IsInterface)
				{
					if (ex.InnerException.GetType() == typeof(InvalidCastException))
					{
						throw new CompositionContractMismatchException(string.Format(CultureInfo.CurrentCulture, Strings.ContractMismatch_InvalidCastOnMetadataField, ex.InnerException.Data["MetadataViewType"], ex.InnerException.Data["MetadataItemKey"], ex.InnerException.Data["MetadataItemValue"], ex.InnerException.Data["MetadataItemSourceType"], ex.InnerException.Data["MetadataItemTargetType"]), ex);
					}
					if (ex.InnerException.GetType() == typeof(NullReferenceException))
					{
						throw new CompositionContractMismatchException(string.Format(CultureInfo.CurrentCulture, Strings.ContractMismatch_NullReferenceOnMetadataField, ex.InnerException.Data["MetadataViewType"], ex.InnerException.Data["MetadataItemKey"], ex.InnerException.Data["MetadataItemTargetType"]), ex);
					}
				}
				throw;
			}
		}

		public static bool IsViewTypeValid(Type metadataViewType)
		{
			Assumes.NotNull(metadataViewType);
			if (ExportServices.IsDefaultMetadataViewType(metadataViewType) || metadataViewType.IsInterface || ExportServices.IsDictionaryConstructorViewType(metadataViewType))
			{
				return true;
			}
			return false;
		}
	}
	/// <summary>Specifies the <see cref="P:System.ComponentModel.Composition.PartCreationPolicyAttribute.CreationPolicy" /> for a part.</summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class PartCreationPolicyAttribute : Attribute
	{
		internal static PartCreationPolicyAttribute Default = new PartCreationPolicyAttribute(CreationPolicy.Any);

		internal static PartCreationPolicyAttribute Shared = new PartCreationPolicyAttribute(CreationPolicy.Shared);

		/// <summary>Gets or sets a value that indicates the creation policy of the attributed part.</summary>
		/// <returns>One of the <see cref="P:System.ComponentModel.Composition.PartCreationPolicyAttribute.CreationPolicy" /> values that indicates the creation policy of the attributed part. The default is <see cref="F:System.ComponentModel.Composition.CreationPolicy.Any" />.</returns>
		public CreationPolicy CreationPolicy { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.PartCreationPolicyAttribute" /> class with the specified creation policy.</summary>
		/// <param name="creationPolicy">The creation policy to use.</param>
		public PartCreationPolicyAttribute(CreationPolicy creationPolicy)
		{
			CreationPolicy = creationPolicy;
		}
	}
	/// <summary>Specifies metadata for a part.</summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class PartMetadataAttribute : Attribute
	{
		/// <summary>Gets the name of the metadata value.</summary>
		/// <returns>A string that contains the name of the metadata value.</returns>
		public string Name { get; private set; }

		/// <summary>Gets the metadata value.</summary>
		/// <returns>An object that contains the metadata value.</returns>
		public object Value { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.PartMetadataAttribute" /> class with the specified name and metadata value.</summary>
		/// <param name="name">A string that contains the name of the metadata value or <see langword="null" /> to use an empty string ("").</param>
		/// <param name="value">An object that contains the metadata value. This can be <see langword="null" />.</param>
		public PartMetadataAttribute(string name, object value)
		{
			Name = name ?? string.Empty;
			Value = value;
		}
	}
	/// <summary>Specifies that this type's exports won't be included in a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" />.</summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class PartNotDiscoverableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.PartNotDiscoverableAttribute" /> class.</summary>
		public PartNotDiscoverableAttribute()
		{
		}
	}
}
namespace System.ComponentModel.Composition.ReflectionModel
{
	internal sealed class DisposableReflectionComposablePart : ReflectionComposablePart, IDisposable
	{
		private volatile int _isDisposed;

		public DisposableReflectionComposablePart(ReflectionComposablePartDefinition definition)
			: base(definition)
		{
		}

		protected override void ReleaseInstanceIfNecessary(object instance)
		{
			if (instance is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}

		protected override void EnsureRunning()
		{
			base.EnsureRunning();
			if (_isDisposed == 1)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}

		void IDisposable.Dispose()
		{
			if (Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 0)
			{
				ReleaseInstanceIfNecessary(base.CachedInstance);
			}
		}
	}
	internal sealed class ExportFactoryCreator
	{
		private class LifetimeContext
		{
			private static Type[] types = new Type[1] { typeof(ComposablePartDefinition) };

			public Func<ComposablePartDefinition, bool> CatalogFilter { get; private set; }

			public void SetInstance(object instance)
			{
				Assumes.NotNull(instance);
				MethodInfo method = instance.GetType().GetMethod("IncludeInScopedCatalog", BindingFlags.Instance | BindingFlags.NonPublic, null, types, null);
				CatalogFilter = (Func<ComposablePartDefinition, bool>)Delegate.CreateDelegate(typeof(Func<ComposablePartDefinition, bool>), instance, method);
			}

			public Tuple<T, Action> GetExportLifetimeContextFromExport<T>(Export export)
			{
				IDisposable disposable = null;
				T item;
				if (export is CatalogExportProvider.ScopeFactoryExport scopeFactoryExport)
				{
					Export export2 = scopeFactoryExport.CreateExportProduct(CatalogFilter);
					item = ExportServices.GetCastedExportedValue<T>(export2);
					disposable = export2 as IDisposable;
				}
				else if (export is CatalogExportProvider.FactoryExport factoryExport)
				{
					Export export3 = factoryExport.CreateExportProduct();
					item = ExportServices.GetCastedExportedValue<T>(export3);
					disposable = export3 as IDisposable;
				}
				else
				{
					ComposablePartDefinition castedExportedValue = ExportServices.GetCastedExportedValue<ComposablePartDefinition>(export);
					ComposablePart composablePart = castedExportedValue.CreatePart();
					ExportDefinition definition = castedExportedValue.ExportDefinitions.Single();
					item = ExportServices.CastExportedValue<T>(composablePart.ToElement(), composablePart.GetExportedValue(definition));
					disposable = composablePart as IDisposable;
				}
				Action item2 = ((disposable == null) ? ((Action)delegate
				{
				}) : ((Action)delegate
				{
					disposable.Dispose();
				}));
				return new Tuple<T, Action>(item, item2);
			}
		}

		private static readonly MethodInfo _createStronglyTypedExportFactoryOfT = typeof(ExportFactoryCreator).GetMethod("CreateStronglyTypedExportFactoryOfT", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		private static readonly MethodInfo _createStronglyTypedExportFactoryOfTM = typeof(ExportFactoryCreator).GetMethod("CreateStronglyTypedExportFactoryOfTM", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		private Type _exportFactoryType;

		public ExportFactoryCreator(Type exportFactoryType)
		{
			Assumes.NotNull(exportFactoryType);
			_exportFactoryType = exportFactoryType;
		}

		public Func<Export, object> CreateStronglyTypedExportFactoryFactory(Type exportType, Type metadataViewType)
		{
			MethodInfo methodInfo = null;
			methodInfo = ((!(metadataViewType == null)) ? _createStronglyTypedExportFactoryOfTM.MakeGenericMethod(exportType, metadataViewType) : _createStronglyTypedExportFactoryOfT.MakeGenericMethod(exportType));
			Assumes.NotNull(methodInfo);
			Func<Export, object> exportFactoryFactory = (Func<Export, object>)Delegate.CreateDelegate(typeof(Func<Export, object>), this, methodInfo);
			return (Export e) => exportFactoryFactory(e);
		}

		private object CreateStronglyTypedExportFactoryOfT<T>(Export export)
		{
			Type[] typeArguments = new Type[1] { typeof(T) };
			Type type = _exportFactoryType.MakeGenericType(typeArguments);
			LifetimeContext lifetimeContext = new LifetimeContext();
			Func<Tuple<T, Action>> func = () => lifetimeContext.GetExportLifetimeContextFromExport<T>(export);
			object[] args = new object[1] { func };
			object obj = Activator.CreateInstance(type, args);
			lifetimeContext.SetInstance(obj);
			return obj;
		}

		private object CreateStronglyTypedExportFactoryOfTM<T, M>(Export export)
		{
			Type[] typeArguments = new Type[2]
			{
				typeof(T),
				typeof(M)
			};
			Type type = _exportFactoryType.MakeGenericType(typeArguments);
			LifetimeContext lifetimeContext = new LifetimeContext();
			Func<Tuple<T, Action>> func = () => lifetimeContext.GetExportLifetimeContextFromExport<T>(export);
			M metadataView = AttributedModelServices.GetMetadataView<M>(export.Metadata);
			object[] args = new object[2] { func, metadataView };
			object obj = Activator.CreateInstance(type, args);
			lifetimeContext.SetInstance(obj);
			return obj;
		}
	}
	internal class ExportingMember
	{
		private readonly ExportDefinition _definition;

		private readonly ReflectionMember _member;

		private object _cachedValue;

		private volatile bool _isValueCached;

		public bool RequiresInstance => _member.RequiresInstance;

		public ExportDefinition Definition => _definition;

		public ExportingMember(ExportDefinition definition, ReflectionMember member)
		{
			Assumes.NotNull(definition, member);
			_definition = definition;
			_member = member;
		}

		public object GetExportedValue(object instance, object @lock)
		{
			EnsureReadable();
			if (!_isValueCached)
			{
				object value;
				try
				{
					value = _member.GetValue(instance);
				}
				catch (TargetInvocationException ex)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ExportThrewException, _member.GetDisplayName()), Definition.ToElement(), ex.InnerException);
				}
				catch (TargetParameterCountException ex2)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ExportNotValidOnIndexers, _member.GetDisplayName()), Definition.ToElement(), ex2.InnerException);
				}
				lock (@lock)
				{
					if (!_isValueCached)
					{
						_cachedValue = value;
						Thread.MemoryBarrier();
						_isValueCached = true;
					}
				}
			}
			return _cachedValue;
		}

		private void EnsureReadable()
		{
			if (!_member.CanRead)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ExportNotReadable, _member.GetDisplayName()), Definition.ToElement());
			}
		}
	}
	internal static class GenericServices
	{
		internal static IList<Type> GetPureGenericParameters(this Type type)
		{
			Assumes.NotNull(type);
			if (type.IsGenericType && type.ContainsGenericParameters)
			{
				List<Type> pureGenericParameters = new List<Type>();
				TraverseGenericType(type, delegate(Type t)
				{
					if (t.IsGenericParameter)
					{
						pureGenericParameters.Add(t);
					}
				});
				return pureGenericParameters;
			}
			return Type.EmptyTypes;
		}

		internal static int GetPureGenericArity(this Type type)
		{
			Assumes.NotNull(type);
			int genericArity = 0;
			if (type.IsGenericType && type.ContainsGenericParameters)
			{
				new List<Type>();
				TraverseGenericType(type, delegate(Type t)
				{
					if (t.IsGenericParameter)
					{
						genericArity++;
					}
				});
			}
			return genericArity;
		}

		private static void TraverseGenericType(Type type, Action<Type> onType)
		{
			if (type.IsGenericType)
			{
				Type[] genericArguments = type.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					TraverseGenericType(genericArguments[i], onType);
				}
			}
			onType(type);
		}

		public static int[] GetGenericParametersOrder(Type type)
		{
			return (from parameter in type.GetPureGenericParameters()
				select parameter.GenericParameterPosition).ToArray();
		}

		public static string GetGenericName(string originalGenericName, int[] genericParametersOrder, int genericArity)
		{
			string[] array = new string[genericArity];
			for (int i = 0; i < genericParametersOrder.Length; i++)
			{
				array[genericParametersOrder[i]] = string.Format(CultureInfo.InvariantCulture, "{{{0}}}", i);
			}
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			object[] args = array;
			return string.Format(invariantCulture, originalGenericName, args);
		}

		public static T[] Reorder<T>(T[] original, int[] genericParametersOrder)
		{
			T[] array = new T[genericParametersOrder.Length];
			for (int i = 0; i < genericParametersOrder.Length; i++)
			{
				array[i] = original[genericParametersOrder[i]];
			}
			return array;
		}

		public static IEnumerable<Type> CreateTypeSpecializations(this Type[] types, Type[] specializationTypes)
		{
			return types?.Select((Type type) => type.CreateTypeSpecialization(specializationTypes));
		}

		public static Type CreateTypeSpecialization(this Type type, Type[] specializationTypes)
		{
			if (!type.ContainsGenericParameters)
			{
				return type;
			}
			if (type.IsGenericParameter)
			{
				return specializationTypes[type.GenericParameterPosition];
			}
			Type[] genericArguments = type.GetGenericArguments();
			Type[] array = new Type[genericArguments.Length];
			for (int i = 0; i < genericArguments.Length; i++)
			{
				Type type2 = genericArguments[i];
				array[i] = (type2.IsGenericParameter ? specializationTypes[type2.GenericParameterPosition] : type2);
			}
			return type.GetGenericTypeDefinition().MakeGenericType(array);
		}

		public static bool CanSpecialize(Type type, IEnumerable<Type> constraints, GenericParameterAttributes attributes)
		{
			if (CanSpecialize(type, constraints))
			{
				return CanSpecialize(type, attributes);
			}
			return false;
		}

		public static bool CanSpecialize(Type type, IEnumerable<Type> constraintTypes)
		{
			if (constraintTypes == null)
			{
				return true;
			}
			foreach (Type constraintType in constraintTypes)
			{
				if (constraintType != null && !constraintType.IsAssignableFrom(type))
				{
					return false;
				}
			}
			return true;
		}

		public static bool CanSpecialize(Type type, GenericParameterAttributes attributes)
		{
			if (attributes == GenericParameterAttributes.None)
			{
				return true;
			}
			if ((attributes & GenericParameterAttributes.ReferenceTypeConstraint) != GenericParameterAttributes.None && type.IsValueType)
			{
				return false;
			}
			if ((attributes & GenericParameterAttributes.DefaultConstructorConstraint) != GenericParameterAttributes.None && !type.IsValueType && type.GetConstructor(Type.EmptyTypes) == null)
			{
				return false;
			}
			if ((attributes & GenericParameterAttributes.NotNullableValueTypeConstraint) != GenericParameterAttributes.None)
			{
				if (!type.IsValueType)
				{
					return false;
				}
				if (Nullable.GetUnderlyingType(type) != null)
				{
					return false;
				}
			}
			return true;
		}
	}
	internal class GenericSpecializationPartCreationInfo : IReflectionPartCreationInfo, ICompositionElement
	{
		private readonly IReflectionPartCreationInfo _originalPartCreationInfo;

		private readonly ReflectionComposablePartDefinition _originalPart;

		private readonly Type[] _specialization;

		private readonly string[] _specializationIdentities;

		private IEnumerable<ExportDefinition> _exports;

		private IEnumerable<ImportDefinition> _imports;

		private readonly Lazy<Type> _lazyPartType;

		private List<LazyMemberInfo> _members;

		private List<Lazy<ParameterInfo>> _parameters;

		private Dictionary<LazyMemberInfo, MemberInfo[]> _membersTable;

		private Dictionary<Lazy<ParameterInfo>, ParameterInfo> _parametersTable;

		private ConstructorInfo _constructor;

		private object _lock = new object();

		public ReflectionComposablePartDefinition OriginalPart => _originalPart;

		public bool IsDisposalRequired => _originalPartCreationInfo.IsDisposalRequired;

		public string DisplayName => Translate(_originalPartCreationInfo.DisplayName);

		public ICompositionElement Origin => _originalPartCreationInfo.Origin;

		public GenericSpecializationPartCreationInfo(IReflectionPartCreationInfo originalPartCreationInfo, ReflectionComposablePartDefinition originalPart, Type[] specialization)
		{
			GenericSpecializationPartCreationInfo genericSpecializationPartCreationInfo = this;
			Assumes.NotNull(originalPartCreationInfo);
			Assumes.NotNull(specialization);
			Assumes.NotNull(originalPart);
			_originalPartCreationInfo = originalPartCreationInfo;
			_originalPart = originalPart;
			_specialization = specialization;
			_specializationIdentities = new string[_specialization.Length];
			for (int i = 0; i < _specialization.Length; i++)
			{
				_specializationIdentities[i] = AttributedModelServices.GetTypeIdentity(_specialization[i]);
			}
			_lazyPartType = new Lazy<Type>(() => genericSpecializationPartCreationInfo._originalPartCreationInfo.GetPartType().MakeGenericType(specialization), LazyThreadSafetyMode.PublicationOnly);
		}

		public Type GetPartType()
		{
			return _lazyPartType.Value;
		}

		public Lazy<Type> GetLazyPartType()
		{
			return _lazyPartType;
		}

		public ConstructorInfo GetConstructor()
		{
			if (_constructor == null)
			{
				ConstructorInfo constructor = _originalPartCreationInfo.GetConstructor();
				ConstructorInfo constructor2 = null;
				if (constructor != null)
				{
					ConstructorInfo[] constructors = GetPartType().GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					foreach (ConstructorInfo constructorInfo in constructors)
					{
						if (constructorInfo.MetadataToken == constructor.MetadataToken)
						{
							constructor2 = constructorInfo;
							break;
						}
					}
				}
				Thread.MemoryBarrier();
				lock (_lock)
				{
					if (_constructor == null)
					{
						_constructor = constructor2;
					}
				}
			}
			return _constructor;
		}

		public IDictionary<string, object> GetMetadata()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(_originalPartCreationInfo.GetMetadata(), StringComparers.MetadataKeyNames);
			dictionary.Remove("System.ComponentModel.Composition.IsGenericPart");
			dictionary.Remove("System.ComponentModel.Composition.GenericPartArity");
			dictionary.Remove("System.ComponentModel.Composition.GenericParameterConstraints");
			dictionary.Remove("System.ComponentModel.Composition.GenericParameterAttributes");
			return dictionary;
		}

		private MemberInfo[] GetAccessors(LazyMemberInfo originalLazyMember)
		{
			BuildTables();
			Assumes.NotNull(_membersTable);
			return _membersTable[originalLazyMember];
		}

		private ParameterInfo GetParameter(Lazy<ParameterInfo> originalParameter)
		{
			BuildTables();
			Assumes.NotNull(_parametersTable);
			return _parametersTable[originalParameter];
		}

		private void BuildTables()
		{
			if (_membersTable != null)
			{
				return;
			}
			PopulateImportsAndExports();
			List<LazyMemberInfo> list = null;
			List<Lazy<ParameterInfo>> parameters = null;
			lock (_lock)
			{
				if (_membersTable == null)
				{
					list = _members;
					parameters = _parameters;
					Assumes.NotNull(list);
				}
			}
			Dictionary<LazyMemberInfo, MemberInfo[]> membersTable = BuildMembersTable(list);
			Dictionary<Lazy<ParameterInfo>, ParameterInfo> parametersTable = BuildParametersTable(parameters);
			lock (_lock)
			{
				if (_membersTable == null)
				{
					_membersTable = membersTable;
					_parametersTable = parametersTable;
					Thread.MemoryBarrier();
					_parameters = null;
					_members = null;
				}
			}
		}

		private Dictionary<LazyMemberInfo, MemberInfo[]> BuildMembersTable(List<LazyMemberInfo> members)
		{
			Assumes.NotNull(members);
			Dictionary<LazyMemberInfo, MemberInfo[]> dictionary = new Dictionary<LazyMemberInfo, MemberInfo[]>();
			Dictionary<int, MemberInfo> dictionary2 = new Dictionary<int, MemberInfo>();
			Type partType = GetPartType();
			dictionary2[partType.MetadataToken] = partType;
			foreach (MethodInfo allMethod in partType.GetAllMethods())
			{
				dictionary2[allMethod.MetadataToken] = allMethod;
			}
			foreach (FieldInfo allField in partType.GetAllFields())
			{
				dictionary2[allField.MetadataToken] = allField;
			}
			foreach (LazyMemberInfo member in members)
			{
				MemberInfo[] accessors = member.GetAccessors();
				MemberInfo[] array = new MemberInfo[accessors.Length];
				for (int i = 0; i < accessors.Length; i++)
				{
					array[i] = ((accessors[i] != null) ? dictionary2[accessors[i].MetadataToken] : null);
				}
				dictionary[member] = array;
			}
			return dictionary;
		}

		private Dictionary<Lazy<ParameterInfo>, ParameterInfo> BuildParametersTable(List<Lazy<ParameterInfo>> parameters)
		{
			if (parameters != null)
			{
				Dictionary<Lazy<ParameterInfo>, ParameterInfo> dictionary = new Dictionary<Lazy<ParameterInfo>, ParameterInfo>();
				ParameterInfo[] parameters2 = GetConstructor().GetParameters();
				{
					foreach (Lazy<ParameterInfo> parameter in parameters)
					{
						dictionary[parameter] = parameters2[parameter.Value.Position];
					}
					return dictionary;
				}
			}
			return null;
		}

		private List<ImportDefinition> PopulateImports(List<LazyMemberInfo> members, List<Lazy<ParameterInfo>> parameters)
		{
			List<ImportDefinition> list = new List<ImportDefinition>();
			foreach (ImportDefinition import in _originalPartCreationInfo.GetImports())
			{
				if (import is ReflectionImportDefinition reflectionImport)
				{
					list.Add(TranslateImport(reflectionImport, members, parameters));
				}
			}
			return list;
		}

		private ImportDefinition TranslateImport(ReflectionImportDefinition reflectionImport, List<LazyMemberInfo> members, List<Lazy<ParameterInfo>> parameters)
		{
			bool flag = false;
			ContractBasedImportDefinition contractBasedImportDefinition = reflectionImport;
			if (reflectionImport is IPartCreatorImportDefinition partCreatorImportDefinition)
			{
				contractBasedImportDefinition = partCreatorImportDefinition.ProductImportDefinition;
				flag = true;
			}
			string contractName = Translate(contractBasedImportDefinition.ContractName);
			string requiredTypeIdentity = Translate(contractBasedImportDefinition.RequiredTypeIdentity);
			IDictionary<string, object> metadata = TranslateImportMetadata(contractBasedImportDefinition);
			ReflectionMemberImportDefinition reflectionMemberImportDefinition = reflectionImport as ReflectionMemberImportDefinition;
			ImportDefinition importDefinition = null;
			if (reflectionMemberImportDefinition != null)
			{
				LazyMemberInfo lazyMember = reflectionMemberImportDefinition.ImportingLazyMember;
				LazyMemberInfo importingLazyMember = new LazyMemberInfo(lazyMember.MemberType, () => GetAccessors(lazyMember));
				importDefinition = ((!flag) ? new ReflectionMemberImportDefinition(importingLazyMember, contractName, requiredTypeIdentity, contractBasedImportDefinition.RequiredMetadata, contractBasedImportDefinition.Cardinality, contractBasedImportDefinition.IsRecomposable, isPrerequisite: false, contractBasedImportDefinition.RequiredCreationPolicy, metadata, ((ICompositionElement)reflectionMemberImportDefinition).Origin) : new PartCreatorMemberImportDefinition(importingLazyMember, ((ICompositionElement)reflectionMemberImportDefinition).Origin, new ContractBasedImportDefinition(contractName, requiredTypeIdentity, contractBasedImportDefinition.RequiredMetadata, contractBasedImportDefinition.Cardinality, contractBasedImportDefinition.IsRecomposable, isPrerequisite: false, CreationPolicy.NonShared, metadata)));
				members.Add(lazyMember);
			}
			else
			{
				ReflectionParameterImportDefinition reflectionParameterImportDefinition = reflectionImport as ReflectionParameterImportDefinition;
				Assumes.NotNull(reflectionParameterImportDefinition);
				Lazy<ParameterInfo> lazyParameter = reflectionParameterImportDefinition.ImportingLazyParameter;
				Lazy<ParameterInfo> importingLazyParameter = new Lazy<ParameterInfo>(() => GetParameter(lazyParameter));
				importDefinition = ((!flag) ? new ReflectionParameterImportDefinition(importingLazyParameter, contractName, requiredTypeIdentity, contractBasedImportDefinition.RequiredMetadata, contractBasedImportDefinition.Cardinality, contractBasedImportDefinition.RequiredCreationPolicy, metadata, ((ICompositionElement)reflectionParameterImportDefinition).Origin) : new PartCreatorParameterImportDefinition(importingLazyParameter, ((ICompositionElement)reflectionParameterImportDefinition).Origin, new ContractBasedImportDefinition(contractName, requiredTypeIdentity, contractBasedImportDefinition.RequiredMetadata, contractBasedImportDefinition.Cardinality, isRecomposable: false, isPrerequisite: true, CreationPolicy.NonShared, metadata)));
				parameters.Add(lazyParameter);
			}
			return importDefinition;
		}

		private List<ExportDefinition> PopulateExports(List<LazyMemberInfo> members)
		{
			List<ExportDefinition> list = new List<ExportDefinition>();
			foreach (ExportDefinition export in _originalPartCreationInfo.GetExports())
			{
				if (export is ReflectionMemberExportDefinition reflectionExport)
				{
					list.Add(TranslateExpot(reflectionExport, members));
				}
			}
			return list;
		}

		public ExportDefinition TranslateExpot(ReflectionMemberExportDefinition reflectionExport, List<LazyMemberInfo> members)
		{
			LazyMemberInfo exportingLazyMember = reflectionExport.ExportingLazyMember;
			LazyMemberInfo capturedLazyMember = exportingLazyMember;
			ReflectionMemberExportDefinition capturedReflectionExport = reflectionExport;
			string contractName = Translate(reflectionExport.ContractName, reflectionExport.Metadata.GetValue<int[]>("System.ComponentModel.Composition.GenericExportParametersOrderMetadataName"));
			LazyMemberInfo member = new LazyMemberInfo(capturedLazyMember.MemberType, () => GetAccessors(capturedLazyMember));
			Lazy<IDictionary<string, object>> metadata = new Lazy<IDictionary<string, object>>(() => TranslateExportMetadata(capturedReflectionExport));
			ReflectionMemberExportDefinition result = new ReflectionMemberExportDefinition(member, new LazyExportDefinition(contractName, metadata), ((ICompositionElement)reflectionExport).Origin);
			members.Add(capturedLazyMember);
			return result;
		}

		private string Translate(string originalValue, int[] genericParametersOrder)
		{
			if (genericParametersOrder != null)
			{
				string[] array = GenericServices.Reorder(_specializationIdentities, genericParametersOrder);
				CultureInfo invariantCulture = CultureInfo.InvariantCulture;
				object[] args = array;
				return string.Format(invariantCulture, originalValue, args);
			}
			return Translate(originalValue);
		}

		private string Translate(string originalValue)
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			object[] specializationIdentities = _specializationIdentities;
			return string.Format(invariantCulture, originalValue, specializationIdentities);
		}

		private IDictionary<string, object> TranslateImportMetadata(ContractBasedImportDefinition originalImport)
		{
			int[] value = originalImport.Metadata.GetValue<int[]>("System.ComponentModel.Composition.GenericImportParametersOrderMetadataName");
			if (value != null)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>(originalImport.Metadata, StringComparers.MetadataKeyNames);
				dictionary["System.ComponentModel.Composition.GenericContractName"] = GenericServices.GetGenericName(originalImport.ContractName, value, _specialization.Length);
				dictionary["System.ComponentModel.Composition.GenericParameters"] = GenericServices.Reorder(_specialization, value);
				dictionary.Remove("System.ComponentModel.Composition.GenericImportParametersOrderMetadataName");
				return dictionary.AsReadOnly();
			}
			return originalImport.Metadata;
		}

		private IDictionary<string, object> TranslateExportMetadata(ReflectionMemberExportDefinition originalExport)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(originalExport.Metadata, StringComparers.MetadataKeyNames);
			string value = originalExport.Metadata.GetValue<string>("ExportTypeIdentity");
			if (!string.IsNullOrEmpty(value))
			{
				dictionary["ExportTypeIdentity"] = Translate(value, originalExport.Metadata.GetValue<int[]>("System.ComponentModel.Composition.GenericExportParametersOrderMetadataName"));
			}
			dictionary.Remove("System.ComponentModel.Composition.GenericExportParametersOrderMetadataName");
			return dictionary;
		}

		private void PopulateImportsAndExports()
		{
			if (_exports != null && _imports != null)
			{
				return;
			}
			List<LazyMemberInfo> members = new List<LazyMemberInfo>();
			List<Lazy<ParameterInfo>> list = new List<Lazy<ParameterInfo>>();
			List<ExportDefinition> exports = PopulateExports(members);
			List<ImportDefinition> imports = PopulateImports(members, list);
			Thread.MemoryBarrier();
			lock (_lock)
			{
				if (_exports == null || _imports == null)
				{
					_members = members;
					if (list.Count > 0)
					{
						_parameters = list;
					}
					_exports = exports;
					_imports = imports;
				}
			}
		}

		public IEnumerable<ExportDefinition> GetExports()
		{
			PopulateImportsAndExports();
			return _exports;
		}

		public IEnumerable<ImportDefinition> GetImports()
		{
			PopulateImportsAndExports();
			return _imports;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is GenericSpecializationPartCreationInfo genericSpecializationPartCreationInfo))
			{
				return false;
			}
			if (_originalPartCreationInfo.Equals(genericSpecializationPartCreationInfo._originalPartCreationInfo))
			{
				return _specialization.IsArrayEqual(genericSpecializationPartCreationInfo._specialization);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return _originalPartCreationInfo.GetHashCode();
		}

		public static bool CanSpecialize(IDictionary<string, object> partMetadata, Type[] specialization)
		{
			int value = partMetadata.GetValue<int>("System.ComponentModel.Composition.GenericPartArity");
			if (value != specialization.Length)
			{
				return false;
			}
			object[] value2 = partMetadata.GetValue<object[]>("System.ComponentModel.Composition.GenericParameterConstraints");
			GenericParameterAttributes[] value3 = partMetadata.GetValue<GenericParameterAttributes[]>("System.ComponentModel.Composition.GenericParameterAttributes");
			if (value2 == null && value3 == null)
			{
				return true;
			}
			if (value2 != null && value2.Length != value)
			{
				return false;
			}
			if (value3 != null && value3.Length != value)
			{
				return false;
			}
			for (int i = 0; i < value; i++)
			{
				if (!GenericServices.CanSpecialize(specialization[i], (value2[i] as Type[]).CreateTypeSpecializations(specialization), value3[i]))
				{
					return false;
				}
			}
			return true;
		}
	}
	internal interface IReflectionPartCreationInfo : ICompositionElement
	{
		bool IsDisposalRequired { get; }

		Type GetPartType();

		Lazy<Type> GetLazyPartType();

		ConstructorInfo GetConstructor();

		IDictionary<string, object> GetMetadata();

		IEnumerable<ExportDefinition> GetExports();

		IEnumerable<ImportDefinition> GetImports();
	}
	internal class ImportType
	{
		private static readonly Type LazyOfTType = typeof(Lazy<>);

		private static readonly Type LazyOfTMType = typeof(Lazy<, >);

		private static readonly Type ExportFactoryOfTType = typeof(ExportFactory<>);

		private static readonly Type ExportFactoryOfTMType = typeof(ExportFactory<, >);

		private readonly Type _type;

		private readonly bool _isAssignableCollectionType;

		private readonly Type _contractType;

		private Func<Export, object> _castSingleValue;

		private bool _isOpenGeneric;

		public bool IsAssignableCollectionType => _isAssignableCollectionType;

		public Type ElementType { get; private set; }

		public Type ActualType => _type;

		public bool IsPartCreator { get; private set; }

		public Type ContractType => _contractType;

		public Func<Export, object> CastExport
		{
			get
			{
				Assumes.IsTrue(!_isOpenGeneric);
				return _castSingleValue;
			}
		}

		public Type MetadataViewType { get; private set; }

		public ImportType(Type type, ImportCardinality cardinality)
		{
			Assumes.NotNull(type);
			_type = type;
			_contractType = type;
			if (cardinality == ImportCardinality.ZeroOrMore)
			{
				_isAssignableCollectionType = IsTypeAssignableCollectionType(type);
				_contractType = CheckForCollection(type);
			}
			_isOpenGeneric = type.ContainsGenericParameters;
			_contractType = CheckForLazyAndPartCreator(_contractType);
		}

		private Type CheckForCollection(Type type)
		{
			ElementType = CollectionServices.GetEnumerableElementType(type);
			if (ElementType != null)
			{
				return ElementType;
			}
			return type;
		}

		private static bool IsGenericDescendentOf(Type type, Type baseGenericTypeDefinition)
		{
			if (type == typeof(object) || type == null)
			{
				return false;
			}
			if (type.IsGenericType && type.GetGenericTypeDefinition() == baseGenericTypeDefinition)
			{
				return true;
			}
			return IsGenericDescendentOf(type.BaseType, baseGenericTypeDefinition);
		}

		public static bool IsDescendentOf(Type type, Type baseType)
		{
			Assumes.NotNull(type);
			Assumes.NotNull(baseType);
			if (!baseType.IsGenericTypeDefinition)
			{
				return baseType.IsAssignableFrom(type);
			}
			return IsGenericDescendentOf(type, baseType.GetGenericTypeDefinition());
		}

		private Type CheckForLazyAndPartCreator(Type type)
		{
			if (type.IsGenericType)
			{
				Type underlyingSystemType = type.GetGenericTypeDefinition().UnderlyingSystemType;
				Type[] genericArguments = type.GetGenericArguments();
				if (underlyingSystemType == LazyOfTType)
				{
					if (!_isOpenGeneric)
					{
						_castSingleValue = ExportServices.CreateStronglyTypedLazyFactory(genericArguments[0].UnderlyingSystemType, null);
					}
					return genericArguments[0];
				}
				if (underlyingSystemType == LazyOfTMType)
				{
					MetadataViewType = genericArguments[1];
					if (!_isOpenGeneric)
					{
						_castSingleValue = ExportServices.CreateStronglyTypedLazyFactory(genericArguments[0].UnderlyingSystemType, genericArguments[1].UnderlyingSystemType);
					}
					return genericArguments[0];
				}
				if (underlyingSystemType != null && IsDescendentOf(underlyingSystemType, ExportFactoryOfTType))
				{
					IsPartCreator = true;
					if (genericArguments.Length == 1)
					{
						if (!_isOpenGeneric)
						{
							_castSingleValue = new ExportFactoryCreator(underlyingSystemType).CreateStronglyTypedExportFactoryFactory(genericArguments[0].UnderlyingSystemType, null);
						}
					}
					else
					{
						if (genericArguments.Length != 2)
						{
							throw ExceptionBuilder.ExportFactory_TooManyGenericParameters(underlyingSystemType.FullName);
						}
						if (!_isOpenGeneric)
						{
							_castSingleValue = new ExportFactoryCreator(underlyingSystemType).CreateStronglyTypedExportFactoryFactory(genericArguments[0].UnderlyingSystemType, genericArguments[1].UnderlyingSystemType);
						}
						MetadataViewType = genericArguments[1];
					}
					return genericArguments[0];
				}
			}
			return type;
		}

		private static bool IsTypeAssignableCollectionType(Type type)
		{
			if (type.IsArray || CollectionServices.IsEnumerableOfT(type))
			{
				return true;
			}
			return false;
		}
	}
	internal abstract class ImportingItem
	{
		private readonly ContractBasedImportDefinition _definition;

		private readonly ImportType _importType;

		public ContractBasedImportDefinition Definition => _definition;

		public ImportType ImportType => _importType;

		protected ImportingItem(ContractBasedImportDefinition definition, ImportType importType)
		{
			Assumes.NotNull(definition);
			_definition = definition;
			_importType = importType;
		}

		public object CastExportsToImportType(Export[] exports)
		{
			if (Definition.Cardinality == ImportCardinality.ZeroOrMore)
			{
				return CastExportsToCollectionImportType(exports);
			}
			return CastExportsToSingleImportType(exports);
		}

		private object CastExportsToCollectionImportType(Export[] exports)
		{
			Assumes.NotNull(exports);
			Type type = ImportType.ElementType ?? typeof(object);
			Array array = Array.CreateInstance(type, exports.Length);
			for (int i = 0; i < array.Length; i++)
			{
				object value = CastSingleExportToImportType(type, exports[i]);
				array.SetValue(value, i);
			}
			return array;
		}

		private object CastExportsToSingleImportType(Export[] exports)
		{
			Assumes.NotNull(exports);
			Assumes.IsTrue(exports.Length < 2);
			if (exports.Length == 0)
			{
				return null;
			}
			return CastSingleExportToImportType(ImportType.ActualType, exports[0]);
		}

		private object CastSingleExportToImportType(Type type, Export export)
		{
			if (ImportType.CastExport != null)
			{
				return ImportType.CastExport(export);
			}
			return Cast(type, export);
		}

		private object Cast(Type type, Export export)
		{
			object value = export.Value;
			if (!ContractServices.TryCast(type, value, out var result))
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportNotAssignableFromExport, export.ToElement().DisplayName, type.FullName), Definition.ToElement());
			}
			return result;
		}
	}
	internal class ImportingMember : ImportingItem
	{
		private readonly ReflectionWritableMember _member;

		public ImportingMember(ContractBasedImportDefinition definition, ReflectionWritableMember member, ImportType importType)
			: base(definition, importType)
		{
			Assumes.NotNull(definition, member);
			_member = member;
		}

		public void SetExportedValue(object instance, object value)
		{
			if (RequiresCollectionNormalization())
			{
				SetCollectionMemberValue(instance, (IEnumerable)value);
			}
			else
			{
				SetSingleMemberValue(instance, value);
			}
		}

		private bool RequiresCollectionNormalization()
		{
			if (base.Definition.Cardinality != ImportCardinality.ZeroOrMore)
			{
				return false;
			}
			if (_member.CanWrite && base.ImportType.IsAssignableCollectionType)
			{
				return false;
			}
			return true;
		}

		private void SetSingleMemberValue(object instance, object value)
		{
			EnsureWritable();
			try
			{
				_member.SetValue(instance, value);
			}
			catch (TargetInvocationException ex)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportThrewException, _member.GetDisplayName()), base.Definition.ToElement(), ex.InnerException);
			}
			catch (TargetParameterCountException ex2)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ImportNotValidOnIndexers, _member.GetDisplayName()), base.Definition.ToElement(), ex2.InnerException);
			}
		}

		private void EnsureWritable()
		{
			if (!_member.CanWrite)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportNotWritable, _member.GetDisplayName()), base.Definition.ToElement());
			}
		}

		private void SetCollectionMemberValue(object instance, IEnumerable values)
		{
			Assumes.NotNull(values);
			ICollection<object> collection = null;
			Type collectionElementType = CollectionServices.GetCollectionElementType(base.ImportType.ActualType);
			if (collectionElementType != null)
			{
				collection = GetNormalizedCollection(collectionElementType, instance);
			}
			EnsureCollectionIsWritable(collection);
			PopulateCollection(collection, values);
		}

		private ICollection<object> GetNormalizedCollection(Type itemType, object instance)
		{
			Assumes.NotNull(itemType);
			object obj = null;
			if (_member.CanRead)
			{
				try
				{
					obj = _member.GetValue(instance);
				}
				catch (TargetInvocationException ex)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportCollectionGetThrewException, _member.GetDisplayName()), base.Definition.ToElement(), ex.InnerException);
				}
			}
			if (obj == null)
			{
				ConstructorInfo constructor = base.ImportType.ActualType.GetConstructor(Type.EmptyTypes);
				if (constructor != null)
				{
					try
					{
						obj = constructor.SafeInvoke();
					}
					catch (TargetInvocationException ex2)
					{
						throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportCollectionConstructionThrewException, _member.GetDisplayName(), base.ImportType.ActualType.FullName), base.Definition.ToElement(), ex2.InnerException);
					}
					SetSingleMemberValue(instance, obj);
				}
			}
			if (obj == null)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportCollectionNull, _member.GetDisplayName()), base.Definition.ToElement());
			}
			return CollectionServices.GetCollectionWrapper(itemType, obj);
		}

		private void EnsureCollectionIsWritable(ICollection<object> collection)
		{
			bool flag = true;
			try
			{
				if (collection != null)
				{
					flag = collection.IsReadOnly;
				}
			}
			catch (Exception innerException)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportCollectionIsReadOnlyThrewException, _member.GetDisplayName(), collection.GetType().FullName), base.Definition.ToElement(), innerException);
			}
			if (flag)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportCollectionNotWritable, _member.GetDisplayName()), base.Definition.ToElement());
			}
		}

		private void PopulateCollection(ICollection<object> collection, IEnumerable values)
		{
			Assumes.NotNull(collection, values);
			try
			{
				collection.Clear();
			}
			catch (Exception innerException)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportCollectionClearThrewException, _member.GetDisplayName(), collection.GetType().FullName), base.Definition.ToElement(), innerException);
			}
			foreach (object value in values)
			{
				try
				{
					collection.Add(value);
				}
				catch (Exception innerException2)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportCollectionAddThrewException, _member.GetDisplayName(), collection.GetType().FullName), base.Definition.ToElement(), innerException2);
				}
			}
		}
	}
	internal class ImportingParameter : ImportingItem
	{
		public ImportingParameter(ContractBasedImportDefinition definition, ImportType importType)
			: base(definition, importType)
		{
		}
	}
	/// <summary>Represents a <see cref="T:System.Reflection.MemberInfo" /> object that does not load assemblies or create objects until requested.</summary>
	public struct LazyMemberInfo
	{
		private readonly MemberTypes _memberType;

		private MemberInfo[] _accessors;

		private readonly Func<MemberInfo[]> _accessorsCreator;

		/// <summary>Gets the type of the represented member.</summary>
		/// <returns>The type of the represented member.</returns>
		public MemberTypes MemberType => _memberType;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ReflectionModel.LazyMemberInfo" /> class, representing the specified member.</summary>
		/// <param name="member">The member to represent.</param>
		public LazyMemberInfo(MemberInfo member)
		{
			Requires.NotNull(member, "member");
			EnsureSupportedMemberType(member.MemberType, "member");
			_accessorsCreator = null;
			_memberType = member.MemberType;
			switch (_memberType)
			{
			case MemberTypes.Property:
			{
				PropertyInfo propertyInfo = (PropertyInfo)member;
				Assumes.NotNull(propertyInfo);
				_accessors = new MemberInfo[2]
				{
					propertyInfo.GetGetMethod(nonPublic: true),
					propertyInfo.GetSetMethod(nonPublic: true)
				};
				break;
			}
			case MemberTypes.Event:
			{
				EventInfo eventInfo = (EventInfo)member;
				_accessors = new MemberInfo[3]
				{
					eventInfo.GetRaiseMethod(nonPublic: true),
					eventInfo.GetAddMethod(nonPublic: true),
					eventInfo.GetRemoveMethod(nonPublic: true)
				};
				break;
			}
			default:
				_accessors = new MemberInfo[1] { member };
				break;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ReflectionModel.LazyMemberInfo" /> class for a member of the specified type with the specified accessors.</summary>
		/// <param name="memberType">The type of the represented member.</param>
		/// <param name="accessors">An array of the accessors for the represented member.</param>
		/// <exception cref="T:System.ArgumentException">One or more of the objects in <paramref name="accessors" /> are not valid accessors for this member.</exception>
		public LazyMemberInfo(MemberTypes memberType, params MemberInfo[] accessors)
		{
			EnsureSupportedMemberType(memberType, "memberType");
			Requires.NotNull(accessors, "accessors");
			if (!AreAccessorsValid(memberType, accessors, out var errorMessage))
			{
				throw new ArgumentException(errorMessage, "accessors");
			}
			_memberType = memberType;
			_accessors = accessors;
			_accessorsCreator = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ReflectionModel.LazyMemberInfo" /> class for a member of the specified type with the specified accessors.</summary>
		/// <param name="memberType">The type of the represented member.</param>
		/// <param name="accessorsCreator">A function whose return value is a collection of the accessors for the represented member.</param>
		public LazyMemberInfo(MemberTypes memberType, Func<MemberInfo[]> accessorsCreator)
		{
			EnsureSupportedMemberType(memberType, "memberType");
			Requires.NotNull(accessorsCreator, "accessorsCreator");
			_memberType = memberType;
			_accessors = null;
			_accessorsCreator = accessorsCreator;
		}

		/// <summary>Gets an array of the accessors for the represented member.</summary>
		/// <returns>An array of the accessors for the represented member.</returns>
		/// <exception cref="T:System.ArgumentException">One or more of the accessors in this object are invalid.</exception>
		public MemberInfo[] GetAccessors()
		{
			if (_accessors == null && _accessorsCreator != null)
			{
				MemberInfo[] accessors = _accessorsCreator();
				if (!AreAccessorsValid(MemberType, accessors, out var errorMessage))
				{
					throw new InvalidOperationException(errorMessage);
				}
				_accessors = accessors;
			}
			return _accessors;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		public override int GetHashCode()
		{
			if (_accessorsCreator != null)
			{
				return MemberType.GetHashCode() ^ _accessorsCreator.GetHashCode();
			}
			Assumes.NotNull(_accessors);
			Assumes.NotNull(_accessors[0]);
			return MemberType.GetHashCode() ^ _accessors[0].GetHashCode();
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			LazyMemberInfo lazyMemberInfo = (LazyMemberInfo)obj;
			if (_memberType != lazyMemberInfo._memberType)
			{
				return false;
			}
			if (_accessorsCreator != null || lazyMemberInfo._accessorsCreator != null)
			{
				return object.Equals(_accessorsCreator, lazyMemberInfo._accessorsCreator);
			}
			Assumes.NotNull(_accessors);
			Assumes.NotNull(lazyMemberInfo._accessors);
			return _accessors.SequenceEqual(lazyMemberInfo._accessors);
		}

		/// <summary>Determines whether the two specified <see cref="T:System.ComponentModel.Composition.ReflectionModel.LazyMemberInfo" /> objects are equal.</summary>
		/// <param name="left">The first object to test.</param>
		/// <param name="right">The second object to test.</param>
		/// <returns>
		///   <see langword="true" /> if the objects are equal; otherwise, <see langword="false" />.</returns>
		public static bool operator ==(LazyMemberInfo left, LazyMemberInfo right)
		{
			return left.Equals(right);
		}

		/// <summary>Determines whether the two specified <see cref="T:System.ComponentModel.Composition.ReflectionModel.LazyMemberInfo" /> objects are not equal.</summary>
		/// <param name="left">The first object to test.</param>
		/// <param name="right">The second object to test.</param>
		/// <returns>
		///   <see langword="true" /> if the objects are equal; otherwise, <see langword="false" />.</returns>
		public static bool operator !=(LazyMemberInfo left, LazyMemberInfo right)
		{
			return !left.Equals(right);
		}

		private static void EnsureSupportedMemberType(MemberTypes memberType, string argument)
		{
			MemberTypes enumFlagSet = MemberTypes.All;
			Requires.IsInMembertypeSet(memberType, argument, enumFlagSet);
		}

		private static bool AreAccessorsValid(MemberTypes memberType, MemberInfo[] accessors, out string errorMessage)
		{
			errorMessage = string.Empty;
			if (accessors == null)
			{
				errorMessage = Strings.LazyMemberInfo_AccessorsNull;
				return false;
			}
			if (accessors.All((MemberInfo accessor) => accessor == null))
			{
				errorMessage = Strings.LazyMemberInfo_NoAccessors;
				return false;
			}
			switch (memberType)
			{
			case MemberTypes.Property:
				if (accessors.Length != 2)
				{
					errorMessage = Strings.LazyMemberInfo_InvalidPropertyAccessors_Cardinality;
					return false;
				}
				if (accessors.Where((MemberInfo accessor) => accessor != null && accessor.MemberType != MemberTypes.Method).Any())
				{
					errorMessage = Strings.LazyMemberinfo_InvalidPropertyAccessors_AccessorType;
					return false;
				}
				break;
			case MemberTypes.Event:
				if (accessors.Length != 3)
				{
					errorMessage = Strings.LazyMemberInfo_InvalidEventAccessors_Cardinality;
					return false;
				}
				if (accessors.Where((MemberInfo accessor) => accessor != null && accessor.MemberType != MemberTypes.Method).Any())
				{
					errorMessage = Strings.LazyMemberinfo_InvalidEventAccessors_AccessorType;
					return false;
				}
				break;
			default:
				if (accessors.Length != 1 || (accessors.Length == 1 && accessors[0].MemberType != memberType))
				{
					errorMessage = string.Format(CultureInfo.CurrentCulture, Strings.LazyMemberInfo_InvalidAccessorOnSimpleMember, memberType);
					return false;
				}
				break;
			}
			return true;
		}
	}
	internal class PartCreatorExportDefinition : ExportDefinition
	{
		private readonly ExportDefinition _productDefinition;

		private IDictionary<string, object> _metadata;

		public override string ContractName => "System.ComponentModel.Composition.Contracts.ExportFactory";

		public override IDictionary<string, object> Metadata
		{
			get
			{
				if (_metadata == null)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>(_productDefinition.Metadata);
					dictionary["ExportTypeIdentity"] = CompositionConstants.PartCreatorTypeIdentity;
					dictionary["ProductDefinition"] = _productDefinition;
					_metadata = dictionary.AsReadOnly();
				}
				return _metadata;
			}
		}

		public PartCreatorExportDefinition(ExportDefinition productDefinition)
		{
			_productDefinition = productDefinition;
		}

		internal static bool IsProductConstraintSatisfiedBy(ImportDefinition productImportDefinition, ExportDefinition exportDefinition)
		{
			object value = null;
			if (exportDefinition.Metadata.TryGetValue("ProductDefinition", out value) && value is ExportDefinition exportDefinition2)
			{
				return productImportDefinition.IsConstraintSatisfiedBy(exportDefinition2);
			}
			return false;
		}
	}
	internal class PartCreatorMemberImportDefinition : ReflectionMemberImportDefinition, IPartCreatorImportDefinition
	{
		private readonly ContractBasedImportDefinition _productImportDefinition;

		public ContractBasedImportDefinition ProductImportDefinition => _productImportDefinition;

		public override Expression<Func<ExportDefinition, bool>> Constraint => ConstraintServices.CreatePartCreatorConstraint(base.Constraint, _productImportDefinition);

		public PartCreatorMemberImportDefinition(LazyMemberInfo importingLazyMember, ICompositionElement origin, ContractBasedImportDefinition productImportDefinition)
			: base(importingLazyMember, "System.ComponentModel.Composition.Contracts.ExportFactory", CompositionConstants.PartCreatorTypeIdentity, productImportDefinition.RequiredMetadata, productImportDefinition.Cardinality, productImportDefinition.IsRecomposable, isPrerequisite: false, productImportDefinition.RequiredCreationPolicy, MetadataServices.EmptyMetadata, origin)
		{
			Assumes.NotNull(productImportDefinition);
			_productImportDefinition = productImportDefinition;
		}

		public override bool IsConstraintSatisfiedBy(ExportDefinition exportDefinition)
		{
			if (!base.IsConstraintSatisfiedBy(exportDefinition))
			{
				return false;
			}
			return PartCreatorExportDefinition.IsProductConstraintSatisfiedBy(_productImportDefinition, exportDefinition);
		}
	}
	internal class PartCreatorParameterImportDefinition : ReflectionParameterImportDefinition, IPartCreatorImportDefinition
	{
		private readonly ContractBasedImportDefinition _productImportDefinition;

		public ContractBasedImportDefinition ProductImportDefinition => _productImportDefinition;

		public override Expression<Func<ExportDefinition, bool>> Constraint => ConstraintServices.CreatePartCreatorConstraint(base.Constraint, _productImportDefinition);

		public PartCreatorParameterImportDefinition(Lazy<ParameterInfo> importingLazyParameter, ICompositionElement origin, ContractBasedImportDefinition productImportDefinition)
			: base(importingLazyParameter, "System.ComponentModel.Composition.Contracts.ExportFactory", CompositionConstants.PartCreatorTypeIdentity, productImportDefinition.RequiredMetadata, productImportDefinition.Cardinality, CreationPolicy.Any, MetadataServices.EmptyMetadata, origin)
		{
			Assumes.NotNull(productImportDefinition);
			_productImportDefinition = productImportDefinition;
		}

		public override bool IsConstraintSatisfiedBy(ExportDefinition exportDefinition)
		{
			if (!base.IsConstraintSatisfiedBy(exportDefinition))
			{
				return false;
			}
			return PartCreatorExportDefinition.IsProductConstraintSatisfiedBy(_productImportDefinition, exportDefinition);
		}
	}
	internal class ReflectionComposablePart : ComposablePart, ICompositionElement
	{
		private readonly ReflectionComposablePartDefinition _definition;

		private readonly Dictionary<ImportDefinition, object> _importValues = new Dictionary<ImportDefinition, object>();

		private readonly Dictionary<ImportDefinition, ImportingItem> _importsCache = new Dictionary<ImportDefinition, ImportingItem>();

		private readonly Dictionary<int, ExportingMember> _exportsCache = new Dictionary<int, ExportingMember>();

		private bool _invokeImportsSatisfied = true;

		private bool _invokingImportsSatisfied;

		private bool _initialCompositionComplete;

		private volatile object _cachedInstance;

		private object _lock = new object();

		protected object CachedInstance
		{
			get
			{
				lock (_lock)
				{
					return _cachedInstance;
				}
			}
		}

		public ReflectionComposablePartDefinition Definition
		{
			get
			{
				RequiresRunning();
				return _definition;
			}
		}

		public override IDictionary<string, object> Metadata
		{
			get
			{
				RequiresRunning();
				return Definition.Metadata;
			}
		}

		public sealed override IEnumerable<ImportDefinition> ImportDefinitions
		{
			get
			{
				RequiresRunning();
				return Definition.ImportDefinitions;
			}
		}

		public sealed override IEnumerable<ExportDefinition> ExportDefinitions
		{
			get
			{
				RequiresRunning();
				return Definition.ExportDefinitions;
			}
		}

		string ICompositionElement.DisplayName => GetDisplayName();

		ICompositionElement ICompositionElement.Origin => Definition;

		public ReflectionComposablePart(ReflectionComposablePartDefinition definition)
		{
			Requires.NotNull(definition, "definition");
			_definition = definition;
		}

		public ReflectionComposablePart(ReflectionComposablePartDefinition definition, object attributedPart)
		{
			Requires.NotNull(definition, "definition");
			Requires.NotNull(attributedPart, "attributedPart");
			_definition = definition;
			if (attributedPart is ValueType)
			{
				throw new ArgumentException(Strings.ArgumentValueType, "attributedPart");
			}
			_cachedInstance = attributedPart;
		}

		protected virtual void EnsureRunning()
		{
		}

		protected void RequiresRunning()
		{
			EnsureRunning();
		}

		protected virtual void ReleaseInstanceIfNecessary(object instance)
		{
		}

		public override object GetExportedValue(ExportDefinition definition)
		{
			RequiresRunning();
			Requires.NotNull(definition, "definition");
			ExportingMember exportingMember = null;
			lock (_lock)
			{
				exportingMember = GetExportingMemberFromDefinition(definition);
				if (exportingMember == null)
				{
					throw ExceptionBuilder.CreateExportDefinitionNotOnThisComposablePart("definition");
				}
				EnsureGettable();
			}
			return GetExportedValue(exportingMember);
		}

		public override void SetImport(ImportDefinition definition, IEnumerable<Export> exports)
		{
			RequiresRunning();
			Requires.NotNull(definition, "definition");
			Requires.NotNull(exports, "exports");
			ImportingItem importingItemFromDefinition = GetImportingItemFromDefinition(definition);
			if (importingItemFromDefinition == null)
			{
				throw ExceptionBuilder.CreateImportDefinitionNotOnThisComposablePart("definition");
			}
			EnsureSettable(definition);
			Export[] exports2 = exports.AsArray();
			EnsureCardinality(definition, exports2);
			SetImport(importingItemFromDefinition, exports2);
		}

		public override void Activate()
		{
			RequiresRunning();
			SetNonPrerequisiteImports();
			NotifyImportSatisfied();
			lock (_lock)
			{
				_initialCompositionComplete = true;
			}
		}

		public override string ToString()
		{
			return GetDisplayName();
		}

		private object GetExportedValue(ExportingMember member)
		{
			object instance = null;
			if (member.RequiresInstance)
			{
				instance = GetInstanceActivatingIfNeeded();
			}
			return member.GetExportedValue(instance, _lock);
		}

		private void SetImport(ImportingItem item, Export[] exports)
		{
			object value = item.CastExportsToImportType(exports);
			lock (_lock)
			{
				_invokeImportsSatisfied = true;
				_importValues[item.Definition] = value;
			}
		}

		private object GetInstanceActivatingIfNeeded()
		{
			if (_cachedInstance != null)
			{
				return _cachedInstance;
			}
			ConstructorInfo constructorInfo = null;
			object[] arguments = null;
			lock (_lock)
			{
				if (!RequiresActivation())
				{
					return null;
				}
				constructorInfo = Definition.GetConstructor();
				if (constructorInfo == null)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_PartConstructorMissing, Definition.GetPartType().FullName), Definition.ToElement());
				}
				arguments = GetConstructorArguments();
			}
			object obj = CreateInstance(constructorInfo, arguments);
			SetPrerequisiteImports();
			lock (_lock)
			{
				if (_cachedInstance == null)
				{
					_cachedInstance = obj;
					obj = null;
				}
			}
			if (obj == null)
			{
				ReleaseInstanceIfNecessary(obj);
			}
			return _cachedInstance;
		}

		private object[] GetConstructorArguments()
		{
			ReflectionParameterImportDefinition[] array = ImportDefinitions.OfType<ReflectionParameterImportDefinition>().ToArray();
			object[] arguments = new object[array.Length];
			UseImportedValues(array, delegate(ImportingItem import, ReflectionParameterImportDefinition definition, object value)
			{
				if (definition.Cardinality == ImportCardinality.ZeroOrMore && !import.ImportType.IsAssignableCollectionType)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_ImportManyOnParameterCanOnlyBeAssigned, Definition.GetPartType().FullName, definition.ImportingLazyParameter.Value.Name), Definition.ToElement());
				}
				arguments[definition.ImportingLazyParameter.Value.Position] = value;
			}, errorIfMissing: true);
			return arguments;
		}

		private bool RequiresActivation()
		{
			if (ImportDefinitions.Any())
			{
				return true;
			}
			return ExportDefinitions.Any((ExportDefinition definition) => GetExportingMemberFromDefinition(definition).RequiresInstance);
		}

		private void EnsureGettable()
		{
			if (_initialCompositionComplete)
			{
				return;
			}
			foreach (ImportDefinition item in ImportDefinitions.Where((ImportDefinition definition) => definition.IsPrerequisite))
			{
				if (!_importValues.ContainsKey(item))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.InvalidOperation_GetExportedValueBeforePrereqImportSet, item.ToElement().DisplayName));
				}
			}
		}

		private void EnsureSettable(ImportDefinition definition)
		{
			lock (_lock)
			{
				if (_initialCompositionComplete && !definition.IsRecomposable)
				{
					throw new InvalidOperationException(Strings.InvalidOperation_DefinitionCannotBeRecomposed);
				}
			}
		}

		private static void EnsureCardinality(ImportDefinition definition, Export[] exports)
		{
			Requires.NullOrNotNullElements(exports, "exports");
			ExportCardinalityCheckResult exportCardinalityCheckResult = ExportServices.CheckCardinality(definition, exports);
			switch (exportCardinalityCheckResult)
			{
			case ExportCardinalityCheckResult.NoExports:
				throw new ArgumentException(Strings.Argument_ExportsEmpty, "exports");
			case ExportCardinalityCheckResult.TooManyExports:
				throw new ArgumentException(Strings.Argument_ExportsTooMany, "exports");
			}
			Assumes.IsTrue(exportCardinalityCheckResult == ExportCardinalityCheckResult.Match);
		}

		private object CreateInstance(ConstructorInfo constructor, object[] arguments)
		{
			Exception ex = null;
			object result = null;
			try
			{
				result = constructor.SafeInvoke(arguments);
			}
			catch (TypeInitializationException ex2)
			{
				ex = ex2;
			}
			catch (TargetInvocationException ex3)
			{
				ex = ex3.InnerException;
			}
			if (ex != null)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_PartConstructorThrewException, Definition.GetPartType().FullName), Definition.ToElement(), ex);
			}
			return result;
		}

		private void SetNonPrerequisiteImports()
		{
			IEnumerable<ImportDefinition> definitions = ImportDefinitions.Where((ImportDefinition import) => !import.IsPrerequisite);
			UseImportedValues(definitions, SetExportedValueForImport, errorIfMissing: false);
		}

		private void SetPrerequisiteImports()
		{
			IEnumerable<ImportDefinition> definitions = ImportDefinitions.Where((ImportDefinition import) => import.IsPrerequisite);
			UseImportedValues(definitions, SetExportedValueForImport, errorIfMissing: false);
		}

		private void SetExportedValueForImport(ImportingItem import, ImportDefinition definition, object value)
		{
			ImportingMember obj = (ImportingMember)import;
			object instanceActivatingIfNeeded = GetInstanceActivatingIfNeeded();
			obj.SetExportedValue(instanceActivatingIfNeeded, value);
		}

		private void UseImportedValues<TImportDefinition>(IEnumerable<TImportDefinition> definitions, Action<ImportingItem, TImportDefinition, object> useImportValue, bool errorIfMissing) where TImportDefinition : ImportDefinition
		{
			CompositionResult compositionResult = CompositionResult.SucceededResult;
			foreach (TImportDefinition definition in definitions)
			{
				ImportingItem importingItemFromDefinition = GetImportingItemFromDefinition(definition);
				if (!TryGetImportValue(definition, out var value))
				{
					if (!errorIfMissing)
					{
						continue;
					}
					if (definition.Cardinality == ImportCardinality.ExactlyOne)
					{
						CompositionError error = CompositionError.Create(CompositionErrorId.ImportNotSetOnPart, Strings.ImportNotSetOnPart, Definition.GetPartType().FullName, definition.ToString());
						compositionResult = compositionResult.MergeError(error);
						continue;
					}
					value = importingItemFromDefinition.CastExportsToImportType(new Export[0]);
				}
				useImportValue(importingItemFromDefinition, definition, value);
			}
			compositionResult.ThrowOnErrors();
		}

		private bool TryGetImportValue(ImportDefinition definition, out object value)
		{
			lock (_lock)
			{
				if (_importValues.TryGetValue(definition, out value))
				{
					_importValues.Remove(definition);
					return true;
				}
			}
			value = null;
			return false;
		}

		private void NotifyImportSatisfied()
		{
			if (!_invokeImportsSatisfied || _invokingImportsSatisfied || !(GetInstanceActivatingIfNeeded() is IPartImportsSatisfiedNotification partImportsSatisfiedNotification))
			{
				return;
			}
			try
			{
				_invokingImportsSatisfied = true;
				partImportsSatisfiedNotification.OnImportsSatisfied();
			}
			catch (Exception innerException)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_PartOnImportsSatisfiedThrewException, Definition.GetPartType().FullName), Definition.ToElement(), innerException);
			}
			finally
			{
				_invokingImportsSatisfied = false;
			}
			_invokeImportsSatisfied = false;
		}

		private ExportingMember GetExportingMemberFromDefinition(ExportDefinition definition)
		{
			if (!(definition is ReflectionMemberExportDefinition reflectionMemberExportDefinition))
			{
				return null;
			}
			int index = reflectionMemberExportDefinition.GetIndex();
			if (!_exportsCache.TryGetValue(index, out var value))
			{
				value = GetExportingMember(definition);
				if (value != null)
				{
					_exportsCache[index] = value;
				}
			}
			return value;
		}

		private ImportingItem GetImportingItemFromDefinition(ImportDefinition definition)
		{
			if (!_importsCache.TryGetValue(definition, out var value))
			{
				value = GetImportingItem(definition);
				if (value != null)
				{
					_importsCache[definition] = value;
				}
			}
			return value;
		}

		private static ImportingItem GetImportingItem(ImportDefinition definition)
		{
			if (definition is ReflectionImportDefinition reflectionImportDefinition)
			{
				return reflectionImportDefinition.ToImportingItem();
			}
			return null;
		}

		private static ExportingMember GetExportingMember(ExportDefinition definition)
		{
			if (definition is ReflectionMemberExportDefinition reflectionMemberExportDefinition)
			{
				return reflectionMemberExportDefinition.ToExportingMember();
			}
			return null;
		}

		private string GetDisplayName()
		{
			return _definition.GetPartType().GetDisplayName();
		}
	}
	internal class ReflectionComposablePartDefinition : ComposablePartDefinition, ICompositionElement
	{
		private readonly IReflectionPartCreationInfo _creationInfo;

		private volatile IEnumerable<ImportDefinition> _imports;

		private volatile IEnumerable<ExportDefinition> _exports;

		private volatile IDictionary<string, object> _metadata;

		private volatile ConstructorInfo _constructor;

		private object _lock = new object();

		public override IEnumerable<ExportDefinition> ExportDefinitions
		{
			get
			{
				if (_exports == null)
				{
					ExportDefinition[] exports = _creationInfo.GetExports().ToArray();
					lock (_lock)
					{
						if (_exports == null)
						{
							_exports = exports;
						}
					}
				}
				return _exports;
			}
		}

		public override IEnumerable<ImportDefinition> ImportDefinitions
		{
			get
			{
				if (_imports == null)
				{
					ImportDefinition[] imports = _creationInfo.GetImports().ToArray();
					lock (_lock)
					{
						if (_imports == null)
						{
							_imports = imports;
						}
					}
				}
				return _imports;
			}
		}

		public override IDictionary<string, object> Metadata
		{
			get
			{
				if (_metadata == null)
				{
					IDictionary<string, object> metadata = _creationInfo.GetMetadata().AsReadOnly();
					lock (_lock)
					{
						if (_metadata == null)
						{
							_metadata = metadata;
						}
					}
				}
				return _metadata;
			}
		}

		internal bool IsDisposalRequired => _creationInfo.IsDisposalRequired;

		string ICompositionElement.DisplayName => _creationInfo.DisplayName;

		ICompositionElement ICompositionElement.Origin => _creationInfo.Origin;

		public ReflectionComposablePartDefinition(IReflectionPartCreationInfo creationInfo)
		{
			Assumes.NotNull(creationInfo);
			_creationInfo = creationInfo;
		}

		public Type GetPartType()
		{
			return _creationInfo.GetPartType();
		}

		public Lazy<Type> GetLazyPartType()
		{
			return _creationInfo.GetLazyPartType();
		}

		public ConstructorInfo GetConstructor()
		{
			if (_constructor == null)
			{
				ConstructorInfo constructor = _creationInfo.GetConstructor();
				lock (_lock)
				{
					if (_constructor == null)
					{
						_constructor = constructor;
					}
				}
			}
			return _constructor;
		}

		public override ComposablePart CreatePart()
		{
			if (IsDisposalRequired)
			{
				return new DisposableReflectionComposablePart(this);
			}
			return new ReflectionComposablePart(this);
		}

		internal override ComposablePartDefinition GetGenericPartDefinition()
		{
			if (_creationInfo is GenericSpecializationPartCreationInfo genericSpecializationPartCreationInfo)
			{
				return genericSpecializationPartCreationInfo.OriginalPart;
			}
			return null;
		}

		internal override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			if (this.IsGeneric())
			{
				List<Tuple<ComposablePartDefinition, ExportDefinition>> list = null;
				IEnumerable<object> enumerable = ((definition.Metadata.Count > 0) ? definition.Metadata.GetValue<IEnumerable<object>>("System.ComponentModel.Composition.GenericParameters") : null);
				if (enumerable != null)
				{
					Type[] genericTypeParameters = null;
					if (TryGetGenericTypeParameters(enumerable, out genericTypeParameters))
					{
						foreach (Type[] candidateParameter in GetCandidateParameters(genericTypeParameters))
						{
							ComposablePartDefinition genericPartDefinition = null;
							if (TryMakeGenericPartDefinition(candidateParameter, out genericPartDefinition))
							{
								IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> exports = genericPartDefinition.GetExports(definition);
								if (exports != ComposablePartDefinition._EmptyExports)
								{
									list = list.FastAppendToListAllowNulls(exports);
								}
							}
						}
					}
				}
				IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> enumerable2 = list;
				return enumerable2 ?? ComposablePartDefinition._EmptyExports;
			}
			return base.GetExports(definition);
		}

		private IEnumerable<Type[]> GetCandidateParameters(Type[] genericParameters)
		{
			foreach (ExportDefinition exportDefinition in ExportDefinitions)
			{
				int[] value = exportDefinition.Metadata.GetValue<int[]>("System.ComponentModel.Composition.GenericExportParametersOrderMetadataName");
				if (value != null && value.Length == genericParameters.Length)
				{
					yield return GenericServices.Reorder(genericParameters, value);
				}
			}
		}

		private static bool TryGetGenericTypeParameters(IEnumerable<object> genericParameters, out Type[] genericTypeParameters)
		{
			genericTypeParameters = genericParameters as Type[];
			if (genericTypeParameters == null)
			{
				object[] array = genericParameters.AsArray();
				genericTypeParameters = new Type[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					genericTypeParameters[i] = array[i] as Type;
					if (genericTypeParameters[i] == null)
					{
						return false;
					}
				}
			}
			return true;
		}

		internal bool TryMakeGenericPartDefinition(Type[] genericTypeParameters, out ComposablePartDefinition genericPartDefinition)
		{
			genericPartDefinition = null;
			if (!GenericSpecializationPartCreationInfo.CanSpecialize(Metadata, genericTypeParameters))
			{
				return false;
			}
			genericPartDefinition = new ReflectionComposablePartDefinition(new GenericSpecializationPartCreationInfo(_creationInfo, this, genericTypeParameters));
			return true;
		}

		public override string ToString()
		{
			return _creationInfo.DisplayName;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ReflectionComposablePartDefinition reflectionComposablePartDefinition))
			{
				return false;
			}
			return _creationInfo.Equals(reflectionComposablePartDefinition._creationInfo);
		}

		public override int GetHashCode()
		{
			return _creationInfo.GetHashCode();
		}
	}
	internal static class ReflectionExtensions
	{
		public static ReflectionMember ToReflectionMember(this LazyMemberInfo lazyMember)
		{
			MemberInfo[] accessors = lazyMember.GetAccessors();
			MemberTypes memberType = lazyMember.MemberType;
			switch (memberType)
			{
			case MemberTypes.Field:
				Assumes.IsTrue(accessors.Length == 1);
				return ((FieldInfo)accessors[0]).ToReflectionField();
			case MemberTypes.Property:
				Assumes.IsTrue(accessors.Length == 2);
				return CreateReflectionProperty((MethodInfo)accessors[0], (MethodInfo)accessors[1]);
			case MemberTypes.TypeInfo:
			case MemberTypes.NestedType:
				return ((Type)accessors[0]).ToReflectionType();
			default:
				Assumes.IsTrue(memberType == MemberTypes.Method);
				return ((MethodInfo)accessors[0]).ToReflectionMethod();
			}
		}

		public static LazyMemberInfo ToLazyMember(this MemberInfo member)
		{
			Assumes.NotNull(member);
			if (member.MemberType == MemberTypes.Property)
			{
				PropertyInfo propertyInfo = member as PropertyInfo;
				Assumes.NotNull(propertyInfo);
				MemberInfo[] accessors = new MemberInfo[2]
				{
					propertyInfo.GetGetMethod(nonPublic: true),
					propertyInfo.GetSetMethod(nonPublic: true)
				};
				return new LazyMemberInfo(MemberTypes.Property, accessors);
			}
			return new LazyMemberInfo(member);
		}

		public static ReflectionWritableMember ToReflectionWriteableMember(this LazyMemberInfo lazyMember)
		{
			Assumes.IsTrue(lazyMember.MemberType == MemberTypes.Field || lazyMember.MemberType == MemberTypes.Property);
			ReflectionWritableMember obj = lazyMember.ToReflectionMember() as ReflectionWritableMember;
			Assumes.NotNull(obj);
			return obj;
		}

		public static ReflectionProperty ToReflectionProperty(this PropertyInfo property)
		{
			Assumes.NotNull(property);
			return CreateReflectionProperty(property.GetGetMethod(nonPublic: true), property.GetSetMethod(nonPublic: true));
		}

		public static ReflectionProperty CreateReflectionProperty(MethodInfo getMethod, MethodInfo setMethod)
		{
			Assumes.IsTrue(getMethod != null || setMethod != null);
			return new ReflectionProperty(getMethod, setMethod);
		}

		public static ReflectionParameter ToReflectionParameter(this ParameterInfo parameter)
		{
			Assumes.NotNull(parameter);
			return new ReflectionParameter(parameter);
		}

		public static ReflectionMethod ToReflectionMethod(this MethodInfo method)
		{
			Assumes.NotNull(method);
			return new ReflectionMethod(method);
		}

		public static ReflectionField ToReflectionField(this FieldInfo field)
		{
			Assumes.NotNull(field);
			return new ReflectionField(field);
		}

		public static ReflectionType ToReflectionType(this Type type)
		{
			Assumes.NotNull(type);
			return new ReflectionType(type);
		}

		public static ReflectionWritableMember ToReflectionWritableMember(this MemberInfo member)
		{
			Assumes.NotNull(member);
			if (member.MemberType == MemberTypes.Property)
			{
				return ((PropertyInfo)member).ToReflectionProperty();
			}
			return ((FieldInfo)member).ToReflectionField();
		}
	}
	internal class ReflectionField : ReflectionWritableMember
	{
		private readonly FieldInfo _field;

		public FieldInfo UndelyingField => _field;

		public override MemberInfo UnderlyingMember => UndelyingField;

		public override bool CanRead => true;

		public override bool CanWrite => !UndelyingField.IsInitOnly;

		public override bool RequiresInstance => !UndelyingField.IsStatic;

		public override Type ReturnType => UndelyingField.FieldType;

		public override ReflectionItemType ItemType => ReflectionItemType.Field;

		public ReflectionField(FieldInfo field)
		{
			Assumes.NotNull(field);
			_field = field;
		}

		public override object GetValue(object instance)
		{
			return UndelyingField.SafeGetValue(instance);
		}

		public override void SetValue(object instance, object value)
		{
			UndelyingField.SafeSetValue(instance, value);
		}
	}
	internal abstract class ReflectionImportDefinition : ContractBasedImportDefinition, ICompositionElement
	{
		private readonly ICompositionElement _origin;

		string ICompositionElement.DisplayName => GetDisplayName();

		ICompositionElement ICompositionElement.Origin => _origin;

		public ReflectionImportDefinition(string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, bool isRecomposable, bool isPrerequisite, CreationPolicy requiredCreationPolicy, IDictionary<string, object> metadata, ICompositionElement origin)
			: base(contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, isPrerequisite, requiredCreationPolicy, metadata)
		{
			_origin = origin;
		}

		public abstract ImportingItem ToImportingItem();

		protected abstract string GetDisplayName();
	}
	internal abstract class ReflectionItem
	{
		public abstract string Name { get; }

		public abstract Type ReturnType { get; }

		public abstract ReflectionItemType ItemType { get; }

		public abstract string GetDisplayName();
	}
	internal enum ReflectionItemType
	{
		Parameter,
		Field,
		Property,
		Method,
		Type
	}
	internal abstract class ReflectionMember : ReflectionItem
	{
		public abstract bool CanRead { get; }

		public Type DeclaringType => UnderlyingMember.DeclaringType;

		public override string Name => UnderlyingMember.Name;

		public abstract bool RequiresInstance { get; }

		public abstract MemberInfo UnderlyingMember { get; }

		public override string GetDisplayName()
		{
			return UnderlyingMember.GetDisplayName();
		}

		public abstract object GetValue(object instance);
	}
	internal class ReflectionMemberExportDefinition : ExportDefinition, ICompositionElement
	{
		private readonly LazyMemberInfo _member;

		private readonly ExportDefinition _exportDefinition;

		private readonly ICompositionElement _origin;

		private IDictionary<string, object> _metadata;

		public override string ContractName => _exportDefinition.ContractName;

		public LazyMemberInfo ExportingLazyMember => _member;

		public override IDictionary<string, object> Metadata
		{
			get
			{
				if (_metadata == null)
				{
					_metadata = _exportDefinition.Metadata.AsReadOnly();
				}
				return _metadata;
			}
		}

		string ICompositionElement.DisplayName => GetDisplayName();

		ICompositionElement ICompositionElement.Origin => _origin;

		public ReflectionMemberExportDefinition(LazyMemberInfo member, ExportDefinition exportDefinition, ICompositionElement origin)
		{
			Assumes.NotNull(exportDefinition);
			_member = member;
			_exportDefinition = exportDefinition;
			_origin = origin;
		}

		public override string ToString()
		{
			return GetDisplayName();
		}

		public int GetIndex()
		{
			return ExportingLazyMember.ToReflectionMember().UnderlyingMember.MetadataToken;
		}

		public ExportingMember ToExportingMember()
		{
			return new ExportingMember(this, ToReflectionMember());
		}

		private ReflectionMember ToReflectionMember()
		{
			return ExportingLazyMember.ToReflectionMember();
		}

		private string GetDisplayName()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} (ContractName=\"{1}\")", ToReflectionMember().GetDisplayName(), ContractName);
		}
	}
	internal class ReflectionMemberImportDefinition : ReflectionImportDefinition
	{
		private LazyMemberInfo _importingLazyMember;

		public LazyMemberInfo ImportingLazyMember => _importingLazyMember;

		public ReflectionMemberImportDefinition(LazyMemberInfo importingLazyMember, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, bool isRecomposable, bool isPrerequisite, CreationPolicy requiredCreationPolicy, IDictionary<string, object> metadata, ICompositionElement origin)
			: base(contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, isPrerequisite, requiredCreationPolicy, metadata, origin)
		{
			Assumes.NotNull(contractName);
			_importingLazyMember = importingLazyMember;
		}

		public override ImportingItem ToImportingItem()
		{
			ReflectionWritableMember reflectionWritableMember = ImportingLazyMember.ToReflectionWriteableMember();
			return new ImportingMember(this, reflectionWritableMember, new ImportType(reflectionWritableMember.ReturnType, Cardinality));
		}

		protected override string GetDisplayName()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} (ContractName=\"{1}\")", ImportingLazyMember.ToReflectionMember().GetDisplayName(), ContractName);
		}
	}
	internal class ReflectionMethod : ReflectionMember
	{
		private readonly MethodInfo _method;

		public MethodInfo UnderlyingMethod => _method;

		public override MemberInfo UnderlyingMember => UnderlyingMethod;

		public override bool CanRead => true;

		public override bool RequiresInstance => !UnderlyingMethod.IsStatic;

		public override Type ReturnType => UnderlyingMethod.ReturnType;

		public override ReflectionItemType ItemType => ReflectionItemType.Method;

		public ReflectionMethod(MethodInfo method)
		{
			Assumes.NotNull(method);
			_method = method;
		}

		public override object GetValue(object instance)
		{
			return SafeCreateExportedDelegate(instance, _method);
		}

		private static ExportedDelegate SafeCreateExportedDelegate(object instance, MethodInfo method)
		{
			ReflectionInvoke.DemandMemberAccessIfNeeded(method);
			return new ExportedDelegate(instance, method);
		}
	}
	/// <summary>Provides extension methods to create and retrieve reflection-based parts.</summary>
	public static class ReflectionModelServices
	{
		/// <summary>Gets the type of a part from a specified part definition.</summary>
		/// <param name="partDefinition">The part definition to examine.</param>
		/// <returns>The type of the defined part.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="partDefinition" /> is <see langword="null" />.</exception>
		public static Lazy<Type> GetPartType(ComposablePartDefinition partDefinition)
		{
			Requires.NotNull(partDefinition, "partDefinition");
			return ((partDefinition as ReflectionComposablePartDefinition) ?? throw ExceptionBuilder.CreateReflectionModelInvalidPartDefinition("partDefinition", partDefinition.GetType())).GetLazyPartType();
		}

		/// <summary>Determines whether the specified part requires disposal.</summary>
		/// <param name="partDefinition">The part to examine.</param>
		/// <returns>
		///   <see langword="true" /> if the part requires disposal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="partDefinition" /> is <see langword="null" />.</exception>
		public static bool IsDisposalRequired(ComposablePartDefinition partDefinition)
		{
			Requires.NotNull(partDefinition, "partDefinition");
			return ((partDefinition as ReflectionComposablePartDefinition) ?? throw ExceptionBuilder.CreateReflectionModelInvalidPartDefinition("partDefinition", partDefinition.GetType())).IsDisposalRequired;
		}

		/// <summary>Gets the exporting member from a specified export definition.</summary>
		/// <param name="exportDefinition">The export definition to examine.</param>
		/// <returns>The member specified in the export definition.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="exportDefinition" /> is <see langword="null" />.</exception>
		public static LazyMemberInfo GetExportingMember(ExportDefinition exportDefinition)
		{
			Requires.NotNull(exportDefinition, "exportDefinition");
			return ((exportDefinition as ReflectionMemberExportDefinition) ?? throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidExportDefinition, exportDefinition.GetType()), "exportDefinition")).ExportingLazyMember;
		}

		/// <summary>Gets the importing member from a specified import definition.</summary>
		/// <param name="importDefinition">The import definition to examine.</param>
		/// <returns>The member specified in the import definition.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="importDefinition" /> is <see langword="null" />.</exception>
		public static LazyMemberInfo GetImportingMember(ImportDefinition importDefinition)
		{
			Requires.NotNull(importDefinition, "importDefinition");
			return ((importDefinition as ReflectionMemberImportDefinition) ?? throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidMemberImportDefinition, importDefinition.GetType()), "importDefinition")).ImportingLazyMember;
		}

		/// <summary>Gets the importing parameter from a specified import definition.</summary>
		/// <param name="importDefinition">The import definition to examine.</param>
		/// <returns>The parameter specified in the import definition.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="importDefinition" /> is <see langword="null" />.</exception>
		public static Lazy<ParameterInfo> GetImportingParameter(ImportDefinition importDefinition)
		{
			Requires.NotNull(importDefinition, "importDefinition");
			return ((importDefinition as ReflectionParameterImportDefinition) ?? throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidParameterImportDefinition, importDefinition.GetType()), "importDefinition")).ImportingLazyParameter;
		}

		/// <summary>Determines whether an import definition represents a member or a parameter.</summary>
		/// <param name="importDefinition">The import definition to examine.</param>
		/// <returns>
		///   <see langword="true" /> if the import definition represents a parameter; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="importDefinition" /> is <see langword="null" />.</exception>
		public static bool IsImportingParameter(ImportDefinition importDefinition)
		{
			Requires.NotNull(importDefinition, "importDefinition");
			if (!(importDefinition is ReflectionImportDefinition))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidImportDefinition, importDefinition.GetType()), "importDefinition");
			}
			return importDefinition is ReflectionParameterImportDefinition;
		}

		/// <summary>Indicates whether a specified import definition represents an export factory (<see cref="T:System.ComponentModel.Composition.ExportFactory`1" /> or <see cref="T:System.ComponentModel.Composition.ExportFactory`2" /> object).</summary>
		/// <param name="importDefinition">The import definition to check.</param>
		/// <returns>
		///   <see langword="true" /> if the specified import definition represents an export factory; otherwise, <see langword="false" />.</returns>
		public static bool IsExportFactoryImportDefinition(ImportDefinition importDefinition)
		{
			Requires.NotNull(importDefinition, "importDefinition");
			return importDefinition is IPartCreatorImportDefinition;
		}

		/// <summary>Returns a representation of an import definition as an export factory product.</summary>
		/// <param name="importDefinition">The import definition to represent.</param>
		/// <returns>The representation of the import definition.</returns>
		public static ContractBasedImportDefinition GetExportFactoryProductImportDefinition(ImportDefinition importDefinition)
		{
			Requires.NotNull(importDefinition, "importDefinition");
			return ((importDefinition as IPartCreatorImportDefinition) ?? throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidImportDefinition, importDefinition.GetType()), "importDefinition")).ProductImportDefinition;
		}

		/// <summary>Creates a part definition with the specified part type, imports, exports, metadata, and origin.</summary>
		/// <param name="partType">The type of the part.</param>
		/// <param name="isDisposalRequired">
		///   <see langword="true" /> if the part requires disposal; otherwise, <see langword="false" />.</param>
		/// <param name="imports">A collection of the part's imports.</param>
		/// <param name="exports">A collection of the part's exports.</param>
		/// <param name="metadata">The part's metadata.</param>
		/// <param name="origin">The part's origin.</param>
		/// <returns>A part definition created from the specified parameters.</returns>
		public static ComposablePartDefinition CreatePartDefinition(Lazy<Type> partType, bool isDisposalRequired, Lazy<IEnumerable<ImportDefinition>> imports, Lazy<IEnumerable<ExportDefinition>> exports, Lazy<IDictionary<string, object>> metadata, ICompositionElement origin)
		{
			Requires.NotNull(partType, "partType");
			return new ReflectionComposablePartDefinition(new ReflectionPartCreationInfo(partType, isDisposalRequired, imports, exports, metadata, origin));
		}

		/// <summary>Creates an export definition from the specified member, with the specified contract name, metadata, and origin.</summary>
		/// <param name="exportingMember">The member to export.</param>
		/// <param name="contractName">The contract name to use for the export.</param>
		/// <param name="metadata">The metadata for the export.</param>
		/// <param name="origin">The object that the export originates from.</param>
		/// <returns>An export definition created from the specified parameters.</returns>
		public static ExportDefinition CreateExportDefinition(LazyMemberInfo exportingMember, string contractName, Lazy<IDictionary<string, object>> metadata, ICompositionElement origin)
		{
			Requires.NotNullOrEmpty(contractName, "contractName");
			Requires.IsInMembertypeSet(exportingMember.MemberType, "exportingMember", MemberTypes.Field | MemberTypes.Method | MemberTypes.Property | MemberTypes.TypeInfo | MemberTypes.NestedType);
			return new ReflectionMemberExportDefinition(exportingMember, new LazyExportDefinition(contractName, metadata), origin);
		}

		/// <summary>Creates an import definition for the specified member by using the specified contract name, type identity, import metadata, cardinality, recomposition policy, and creation policy.</summary>
		/// <param name="importingMember">The member to import into.</param>
		/// <param name="contractName">The contract name to use for the import.</param>
		/// <param name="requiredTypeIdentity">The required type identity for the import.</param>
		/// <param name="requiredMetadata">The required metadata for the import.</param>
		/// <param name="cardinality">The cardinality of the import.</param>
		/// <param name="isRecomposable">
		///   <see langword="true" /> to indicate that the import is recomposable; otherwise, <see langword="false" />.</param>
		/// <param name="requiredCreationPolicy">One of the enumeration values that specifies the import's creation policy.</param>
		/// <param name="origin">The object to import into.</param>
		/// <returns>An import definition created from the specified parameters.</returns>
		public static ContractBasedImportDefinition CreateImportDefinition(LazyMemberInfo importingMember, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, bool isRecomposable, CreationPolicy requiredCreationPolicy, ICompositionElement origin)
		{
			return CreateImportDefinition(importingMember, contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, requiredCreationPolicy, MetadataServices.EmptyMetadata, isExportFactory: false, origin);
		}

		/// <summary>Creates an import definition for the specified member by using the specified contract name, type identity, import and contract metadata, cardinality, recomposition policy, and creation policy.</summary>
		/// <param name="importingMember">The member to import into.</param>
		/// <param name="contractName">The contract name to use for the import.</param>
		/// <param name="requiredTypeIdentity">The required type identity for the import.</param>
		/// <param name="requiredMetadata">The required metadata for the import.</param>
		/// <param name="cardinality">The cardinality of the import.</param>
		/// <param name="isRecomposable">
		///   <see langword="true" /> to indicate that the import is recomposable; otherwise, <see langword="false" />.</param>
		/// <param name="requiredCreationPolicy">One of the enumeration values that specifies the import's creation policy.</param>
		/// <param name="metadata">The contract metadata.</param>
		/// <param name="isExportFactory">
		///   <see langword="true" /> to indicate that the import represents an <see cref="T:System.ComponentModel.Composition.ExportFactory`1" />; otherwise, <see langword="false" />.</param>
		/// <param name="origin">The object to import into.</param>
		/// <returns>An import definition created from the specified parameters.</returns>
		public static ContractBasedImportDefinition CreateImportDefinition(LazyMemberInfo importingMember, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, bool isRecomposable, CreationPolicy requiredCreationPolicy, IDictionary<string, object> metadata, bool isExportFactory, ICompositionElement origin)
		{
			return CreateImportDefinition(importingMember, contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, isPreRequisite: false, requiredCreationPolicy, metadata, isExportFactory, origin);
		}

		/// <summary>Creates an import definition for the specified member by using the specified contract name, type identity, import and contract metadata, cardinality, recomposition policy, and creation policy.</summary>
		/// <param name="importingMember">The member to import into.</param>
		/// <param name="contractName">The contract name to use for the import.</param>
		/// <param name="requiredTypeIdentity">The required type identity for the import.</param>
		/// <param name="requiredMetadata">The required metadata for the import.</param>
		/// <param name="cardinality">The cardinality of the import.</param>
		/// <param name="isRecomposable">
		///   <see langword="true" /> to indicate that the import is recomposable; otherwise, <see langword="false" />.</param>
		/// <param name="isPreRequisite">
		///   <see langword="true" /> to indicate that the import is a prerequisite; otherwise, <see langword="false" />.</param>
		/// <param name="requiredCreationPolicy">One of the enumeration values that specifies the import's creation policy.</param>
		/// <param name="metadata">The contract metadata.</param>
		/// <param name="isExportFactory">
		///   <see langword="true" /> to indicate that the import represents an <see cref="T:System.ComponentModel.Composition.ExportFactory`1" />; otherwise, <see langword="false" />.</param>
		/// <param name="origin">The object to import into.</param>
		/// <returns>An import definition created from the specified parameters.</returns>
		public static ContractBasedImportDefinition CreateImportDefinition(LazyMemberInfo importingMember, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, bool isRecomposable, bool isPreRequisite, CreationPolicy requiredCreationPolicy, IDictionary<string, object> metadata, bool isExportFactory, ICompositionElement origin)
		{
			Requires.NotNullOrEmpty(contractName, "contractName");
			Requires.IsInMembertypeSet(importingMember.MemberType, "importingMember", MemberTypes.Field | MemberTypes.Property);
			if (isExportFactory)
			{
				return new PartCreatorMemberImportDefinition(importingMember, origin, new ContractBasedImportDefinition(contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, isPreRequisite, CreationPolicy.NonShared, metadata));
			}
			return new ReflectionMemberImportDefinition(importingMember, contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, isPreRequisite, requiredCreationPolicy, metadata, origin);
		}

		/// <summary>Creates an import definition for the specified parameter by using the specified contract name, type identity, import metadata, cardinality, and creation policy.</summary>
		/// <param name="parameter">The parameter to import.</param>
		/// <param name="contractName">The contract name to use for the import.</param>
		/// <param name="requiredTypeIdentity">The required type identity for the import.</param>
		/// <param name="requiredMetadata">The required metadata for the import.</param>
		/// <param name="cardinality">The cardinality of the import.</param>
		/// <param name="requiredCreationPolicy">One of the enumeration values that specifies the import's creation policy.</param>
		/// <param name="origin">The object to import into.</param>
		/// <returns>An import definition created from the specified parameters.</returns>
		public static ContractBasedImportDefinition CreateImportDefinition(Lazy<ParameterInfo> parameter, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, CreationPolicy requiredCreationPolicy, ICompositionElement origin)
		{
			return CreateImportDefinition(parameter, contractName, requiredTypeIdentity, requiredMetadata, cardinality, requiredCreationPolicy, MetadataServices.EmptyMetadata, isExportFactory: false, origin);
		}

		/// <summary>Creates an import definition for the specified parameter by using the specified contract name, type identity, import and contract metadata, cardinality, and creation policy.</summary>
		/// <param name="parameter">The parameter to import.</param>
		/// <param name="contractName">The contract name to use for the import.</param>
		/// <param name="requiredTypeIdentity">The required type identity for the import.</param>
		/// <param name="requiredMetadata">The required metadata for the import.</param>
		/// <param name="cardinality">The cardinality of the import.</param>
		/// <param name="requiredCreationPolicy">One of the enumeration values that specifies the import's creation policy.</param>
		/// <param name="metadata">The contract metadata</param>
		/// <param name="isExportFactory">
		///   <see langword="true" /> to indicate that the import represents an <see cref="T:System.ComponentModel.Composition.ExportFactory`1" />; otherwise, <see langword="false" />.</param>
		/// <param name="origin">The object to import into.</param>
		/// <returns>An import definition created from the specified parameters.</returns>
		public static ContractBasedImportDefinition CreateImportDefinition(Lazy<ParameterInfo> parameter, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, CreationPolicy requiredCreationPolicy, IDictionary<string, object> metadata, bool isExportFactory, ICompositionElement origin)
		{
			Requires.NotNull(parameter, "parameter");
			Requires.NotNullOrEmpty(contractName, "contractName");
			if (isExportFactory)
			{
				return new PartCreatorParameterImportDefinition(parameter, origin, new ContractBasedImportDefinition(contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable: false, isPrerequisite: true, CreationPolicy.NonShared, metadata));
			}
			return new ReflectionParameterImportDefinition(parameter, contractName, requiredTypeIdentity, requiredMetadata, cardinality, requiredCreationPolicy, metadata, origin);
		}

		/// <summary>Indicates whether a generic part definition can be specialized with the provided parameters.</summary>
		/// <param name="partDefinition">The part definition.</param>
		/// <param name="genericParameters">A collection of types to specify the generic parameters.</param>
		/// <param name="specialization">When this method returns, contains the specialized part definition. This parameter is treated as uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the specialization succeeds; otherwise, <see langword="false" />.</returns>
		public static bool TryMakeGenericPartDefinition(ComposablePartDefinition partDefinition, IEnumerable<Type> genericParameters, out ComposablePartDefinition specialization)
		{
			Requires.NotNull(partDefinition, "partDefinition");
			specialization = null;
			return ((partDefinition as ReflectionComposablePartDefinition) ?? throw ExceptionBuilder.CreateReflectionModelInvalidPartDefinition("partDefinition", partDefinition.GetType())).TryMakeGenericPartDefinition(genericParameters.ToArray(), out specialization);
		}
	}
	internal class ReflectionPartCreationInfo : IReflectionPartCreationInfo, ICompositionElement
	{
		private readonly Lazy<Type> _partType;

		private readonly Lazy<IEnumerable<ImportDefinition>> _imports;

		private readonly Lazy<IEnumerable<ExportDefinition>> _exports;

		private readonly Lazy<IDictionary<string, object>> _metadata;

		private readonly ICompositionElement _origin;

		private ConstructorInfo _constructor;

		private bool _isDisposalRequired;

		public bool IsDisposalRequired => _isDisposalRequired;

		public string DisplayName => GetPartType().GetDisplayName();

		public ICompositionElement Origin => _origin;

		public ReflectionPartCreationInfo(Lazy<Type> partType, bool isDisposalRequired, Lazy<IEnumerable<ImportDefinition>> imports, Lazy<IEnumerable<ExportDefinition>> exports, Lazy<IDictionary<string, object>> metadata, ICompositionElement origin)
		{
			Assumes.NotNull(partType);
			_partType = partType;
			_isDisposalRequired = isDisposalRequired;
			_imports = imports;
			_exports = exports;
			_metadata = metadata;
			_origin = origin;
		}

		public Type GetPartType()
		{
			return _partType.GetNotNullValue("type");
		}

		public Lazy<Type> GetLazyPartType()
		{
			return _partType;
		}

		public ConstructorInfo GetConstructor()
		{
			if (_constructor == null)
			{
				ConstructorInfo[] array = null;
				array = (from parameterImport in GetImports().OfType<ReflectionParameterImportDefinition>()
					select parameterImport.ImportingLazyParameter.Value.Member).OfType<ConstructorInfo>().Distinct().ToArray();
				if (array.Length == 1)
				{
					_constructor = array[0];
				}
				else if (array.Length == 0)
				{
					_constructor = GetPartType().GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
				}
			}
			return _constructor;
		}

		public IDictionary<string, object> GetMetadata()
		{
			if (_metadata == null)
			{
				return null;
			}
			return _metadata.Value;
		}

		public IEnumerable<ExportDefinition> GetExports()
		{
			if (_exports == null)
			{
				yield break;
			}
			IEnumerable<ExportDefinition> value = _exports.Value;
			if (value == null)
			{
				yield break;
			}
			foreach (ExportDefinition item in value)
			{
				if (!(item is ReflectionMemberExportDefinition reflectionMemberExportDefinition))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidExportDefinition, item.GetType()));
				}
				yield return reflectionMemberExportDefinition;
			}
		}

		public IEnumerable<ImportDefinition> GetImports()
		{
			if (_imports == null)
			{
				yield break;
			}
			IEnumerable<ImportDefinition> value = _imports.Value;
			if (value == null)
			{
				yield break;
			}
			foreach (ImportDefinition item in value)
			{
				if (!(item is ReflectionImportDefinition reflectionImportDefinition))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidMemberImportDefinition, item.GetType()));
				}
				yield return reflectionImportDefinition;
			}
		}
	}
	internal class LazyExportDefinition : ExportDefinition
	{
		private readonly Lazy<IDictionary<string, object>> _metadata;

		public override IDictionary<string, object> Metadata => _metadata.Value ?? MetadataServices.EmptyMetadata;

		public LazyExportDefinition(string contractName, Lazy<IDictionary<string, object>> metadata)
			: base(contractName, null)
		{
			_metadata = metadata;
		}
	}
	internal class ReflectionParameter : ReflectionItem
	{
		private readonly ParameterInfo _parameter;

		public ParameterInfo UnderlyingParameter => _parameter;

		public override string Name => UnderlyingParameter.Name;

		public override Type ReturnType => UnderlyingParameter.ParameterType;

		public override ReflectionItemType ItemType => ReflectionItemType.Parameter;

		public ReflectionParameter(ParameterInfo parameter)
		{
			Assumes.NotNull(parameter);
			_parameter = parameter;
		}

		public override string GetDisplayName()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} (Parameter=\"{1}\")", UnderlyingParameter.Member.GetDisplayName(), UnderlyingParameter.Name);
		}
	}
	internal class ReflectionParameterImportDefinition : ReflectionImportDefinition
	{
		private Lazy<ParameterInfo> _importingLazyParameter;

		public Lazy<ParameterInfo> ImportingLazyParameter => _importingLazyParameter;

		public ReflectionParameterImportDefinition(Lazy<ParameterInfo> importingLazyParameter, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, CreationPolicy requiredCreationPolicy, IDictionary<string, object> metadata, ICompositionElement origin)
			: base(contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable: false, isPrerequisite: true, requiredCreationPolicy, metadata, origin)
		{
			Assumes.NotNull(importingLazyParameter);
			_importingLazyParameter = importingLazyParameter;
		}

		public override ImportingItem ToImportingItem()
		{
			return new ImportingParameter(this, new ImportType(ImportingLazyParameter.GetNotNullValue("parameter").ParameterType, Cardinality));
		}

		protected override string GetDisplayName()
		{
			ParameterInfo notNullValue = ImportingLazyParameter.GetNotNullValue("parameter");
			return string.Format(CultureInfo.CurrentCulture, "{0} (Parameter=\"{1}\", ContractName=\"{2}\")", notNullValue.Member.GetDisplayName(), notNullValue.Name, ContractName);
		}
	}
	internal class ReflectionProperty : ReflectionWritableMember
	{
		private readonly MethodInfo _getMethod;

		private readonly MethodInfo _setMethod;

		public override MemberInfo UnderlyingMember => UnderlyingGetMethod ?? UnderlyingSetMethod;

		public override bool CanRead => UnderlyingGetMethod != null;

		public override bool CanWrite => UnderlyingSetMethod != null;

		public MethodInfo UnderlyingGetMethod => _getMethod;

		public MethodInfo UnderlyingSetMethod => _setMethod;

		public override string Name
		{
			get
			{
				string name = (UnderlyingGetMethod ?? UnderlyingSetMethod).Name;
				Assumes.IsTrue(name.Length > 4);
				return name.Substring(4);
			}
		}

		public override bool RequiresInstance => !(UnderlyingGetMethod ?? UnderlyingSetMethod).IsStatic;

		public override Type ReturnType
		{
			get
			{
				if (UnderlyingGetMethod != null)
				{
					return UnderlyingGetMethod.ReturnType;
				}
				ParameterInfo[] parameters = UnderlyingSetMethod.GetParameters();
				Assumes.IsTrue(parameters.Length != 0);
				return parameters[^1].ParameterType;
			}
		}

		public override ReflectionItemType ItemType => ReflectionItemType.Property;

		public ReflectionProperty(MethodInfo getMethod, MethodInfo setMethod)
		{
			Assumes.IsTrue(getMethod != null || setMethod != null);
			_getMethod = getMethod;
			_setMethod = setMethod;
		}

		public override string GetDisplayName()
		{
			return ReflectionServices.GetDisplayName(base.DeclaringType, Name);
		}

		public override object GetValue(object instance)
		{
			Assumes.NotNull(_getMethod);
			return UnderlyingGetMethod.SafeInvoke(instance);
		}

		public override void SetValue(object instance, object value)
		{
			Assumes.NotNull(_setMethod);
			UnderlyingSetMethod.SafeInvoke(instance, value);
		}
	}
	internal class ReflectionType : ReflectionMember
	{
		private Type _type;

		public override MemberInfo UnderlyingMember => _type;

		public override bool CanRead => true;

		public override bool RequiresInstance => true;

		public override Type ReturnType => _type;

		public override ReflectionItemType ItemType => ReflectionItemType.Type;

		public ReflectionType(Type type)
		{
			Assumes.NotNull(type);
			_type = type;
		}

		public override object GetValue(object instance)
		{
			return instance;
		}
	}
	internal abstract class ReflectionWritableMember : ReflectionMember
	{
		public abstract bool CanWrite { get; }

		public abstract void SetValue(object instance, object value);
	}
}
namespace System.ComponentModel.Composition.Primitives
{
	/// <summary>Defines the abstract base class for composable parts, which import objects and produce exported objects.</summary>
	public abstract class ComposablePart
	{
		/// <summary>Gets a collection of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects that describe the exported objects provided by the part.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects that describe the exported objects provided by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object has been disposed of.</exception>
		public abstract IEnumerable<ExportDefinition> ExportDefinitions { get; }

		/// <summary>Gets a collection of the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> objects that describe the imported objects required by the part.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> objects that describe the imported objects required by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object has been disposed of.</exception>
		public abstract IEnumerable<ImportDefinition> ImportDefinitions { get; }

		/// <summary>Gets the metadata of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object.</summary>
		/// <returns>The metadata of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object. The default is an empty, read-only <see cref="T:System.Collections.Generic.IDictionary`2" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object has been disposed of.</exception>
		public virtual IDictionary<string, object> Metadata => MetadataServices.EmptyMetadata;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> class.</summary>
		protected ComposablePart()
		{
		}

		/// <summary>Called when all the imports of the part have been set, and exports can be retrieved.</summary>
		public virtual void Activate()
		{
		}

		/// <summary>Gets the exported object described by the specified <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> object.</summary>
		/// <param name="definition">One of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects from the <see cref="P:System.ComponentModel.Composition.Primitives.ComposablePart.ExportDefinitions" /> property that describes the exported object to return.</param>
		/// <returns>The exported object described by <paramref name="definition" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException">An error occurred getting the exported object described by the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="definition" /> did not originate from the <see cref="P:System.ComponentModel.Composition.Primitives.ComposablePart.ExportDefinitions" /> property on the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more prerequisite imports, indicated by <see cref="P:System.ComponentModel.Composition.Primitives.ImportDefinition.IsPrerequisite" />, have not been set.</exception>
		public abstract object GetExportedValue(ExportDefinition definition);

		/// <summary>Sets the import described by the specified <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> object to be satisfied by the specified exports.</summary>
		/// <param name="definition">One of the objects from the <see cref="P:System.ComponentModel.Composition.Primitives.ComposablePart.ImportDefinitions" /> property that specifies the import to be set.</param>
		/// <param name="exports">A collection of <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects of which to set the import described by <paramref name="definition" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="exports" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException">An error occurred setting the import described by the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> object.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="definition" /> did not originate from the <see cref="P:System.ComponentModel.Composition.Primitives.ComposablePart.ImportDefinitions" /> property on the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" />.  
		/// -or-  
		/// <paramref name="exports" /> contains an element that is <see langword="null" />.  
		/// -or-  
		/// <paramref name="exports" /> is empty and <see cref="P:System.ComponentModel.Composition.Primitives.ImportDefinition.Cardinality" /> is <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ExactlyOne" />.  
		/// -or-  
		/// <paramref name="exports" /> contains more than one element and <see cref="P:System.ComponentModel.Composition.Primitives.ImportDefinition.Cardinality" /> is <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ZeroOrOne" /> or <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ExactlyOne" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.Composition.Primitives.ComposablePart.SetImport(System.ComponentModel.Composition.Primitives.ImportDefinition,System.Collections.Generic.IEnumerable{System.ComponentModel.Composition.Primitives.Export})" /> has been previously called and <see cref="P:System.ComponentModel.Composition.Primitives.ImportDefinition.IsRecomposable" /> is <see langword="false" />.</exception>
		public abstract void SetImport(ImportDefinition definition, IEnumerable<Export> exports);
	}
	/// <summary>Represents the abstract base class for composable part catalogs, which collect and return <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects.</summary>
	[DebuggerTypeProxy(typeof(ComposablePartCatalogDebuggerProxy))]
	public abstract class ComposablePartCatalog : IEnumerable<ComposablePartDefinition>, IEnumerable, IDisposable
	{
		private bool _isDisposed;

		private volatile IQueryable<ComposablePartDefinition> _queryableParts;

		private static readonly List<Tuple<ComposablePartDefinition, ExportDefinition>> _EmptyExportsList = new List<Tuple<ComposablePartDefinition, ExportDefinition>>();

		/// <summary>Gets the part definitions that are contained in the catalog.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> contained in the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> object has been disposed of.</exception>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual IQueryable<ComposablePartDefinition> Parts
		{
			get
			{
				ThrowIfDisposed();
				if (_queryableParts == null)
				{
					IQueryable<ComposablePartDefinition> value = this.AsQueryable();
					Interlocked.CompareExchange(ref _queryableParts, value, null);
					Assumes.NotNull(_queryableParts);
				}
				return _queryableParts;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> class.</summary>
		protected ComposablePartCatalog()
		{
		}

		/// <summary>Gets a list of export definitions that match the constraint defined by the specified <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> object.</summary>
		/// <param name="definition">The conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects to be returned.</param>
		/// <returns>A collection of <see cref="T:System.Tuple`2" /> containing the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects and their associated <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects for objects that match the constraint specified by <paramref name="definition" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.</exception>
		public virtual IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			ThrowIfDisposed();
			Requires.NotNull(definition, "definition");
			List<Tuple<ComposablePartDefinition, ExportDefinition>> list = null;
			IEnumerable<ComposablePartDefinition> candidateParts = GetCandidateParts(definition);
			if (candidateParts != null)
			{
				foreach (ComposablePartDefinition item in candidateParts)
				{
					IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> exports = item.GetExports(definition);
					if (exports != ComposablePartDefinition._EmptyExports)
					{
						list = list.FastAppendToListAllowNulls(exports);
					}
				}
			}
			return list ?? _EmptyExportsList;
		}

		internal virtual IEnumerable<ComposablePartDefinition> GetCandidateParts(ImportDefinition definition)
		{
			return this;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" />.</summary>
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			_isDisposed = true;
		}

		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}

		/// <summary>Returns an enumerator that iterates through the catalog.</summary>
		/// <returns>An enumerator that can be used to iterate through the catalog.</returns>
		public virtual IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			IQueryable<ComposablePartDefinition> parts = Parts;
			if (parts == _queryableParts)
			{
				return Enumerable.Empty<ComposablePartDefinition>().GetEnumerator();
			}
			return parts.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through the catalog.</summary>
		/// <returns>An enumerator that can be used to iterate through the catalog.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	internal class ComposablePartCatalogDebuggerProxy
	{
		private readonly ComposablePartCatalog _catalog;

		public ReadOnlyCollection<ComposablePartDefinition> Parts => _catalog.Parts.ToReadOnlyCollection();

		public ComposablePartCatalogDebuggerProxy(ComposablePartCatalog catalog)
		{
			Requires.NotNull(catalog, "catalog");
			_catalog = catalog;
		}
	}
	/// <summary>Defines an abstract base class for composable part definitions, which describe and enable the creation of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> objects.</summary>
	public abstract class ComposablePartDefinition
	{
		internal static readonly IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> _EmptyExports = Enumerable.Empty<Tuple<ComposablePartDefinition, ExportDefinition>>();

		/// <summary>Gets a collection of <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects that describe the objects exported by the part defined by this <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> object.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects that describe the exported objects provided by <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> objects created by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" />.</returns>
		public abstract IEnumerable<ExportDefinition> ExportDefinitions { get; }

		/// <summary>Gets a collection of <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> objects that describe the imports required by the part defined by this <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> object.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> objects that describe the imports required by <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> objects created by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" />.</returns>
		public abstract IEnumerable<ImportDefinition> ImportDefinitions { get; }

		/// <summary>Gets a collection of the metadata for this <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> object.</summary>
		/// <returns>A collection that contains the metadata for the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" />. The default is an empty, read-only <see cref="T:System.Collections.Generic.IDictionary`2" /> object.</returns>
		public virtual IDictionary<string, object> Metadata => MetadataServices.EmptyMetadata;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> class.</summary>
		protected ComposablePartDefinition()
		{
		}

		/// <summary>Creates a new instance of a part that the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> describes.</summary>
		/// <returns>The created part.</returns>
		public abstract ComposablePart CreatePart();

		internal virtual IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			List<Tuple<ComposablePartDefinition, ExportDefinition>> list = null;
			foreach (ExportDefinition exportDefinition in ExportDefinitions)
			{
				if (definition.IsConstraintSatisfiedBy(exportDefinition))
				{
					if (list == null)
					{
						list = new List<Tuple<ComposablePartDefinition, ExportDefinition>>();
					}
					list.Add(new Tuple<ComposablePartDefinition, ExportDefinition>(this, exportDefinition));
				}
			}
			IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> enumerable = list;
			return enumerable ?? _EmptyExports;
		}

		internal virtual ComposablePartDefinition GetGenericPartDefinition()
		{
			return null;
		}
	}
	/// <summary>The exception that is thrown when an error occurs when calling methods on a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object.</summary>
	[Serializable]
	[DebuggerTypeProxy(typeof(ComposablePartExceptionDebuggerProxy))]
	[DebuggerDisplay("{Message}")]
	public class ComposablePartException : Exception
	{
		private readonly ICompositionElement _element;

		/// <summary>Gets the composition element that is the cause of the exception.</summary>
		/// <returns>The compositional element that is the cause of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />. The default is <see langword="null" />.</returns>
		public ICompositionElement Element => _element;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" /> class.</summary>
		public ComposablePartException()
			: this(null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" /> class with the specified error message.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.Exception.Message" /> property to its default value.</param>
		public ComposablePartException(string message)
			: this(message, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" /> class with the specified error message and the composition element that is the cause of the exception.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.Exception.Message" /> property to its default value.</param>
		/// <param name="element">The composition element that is the cause of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.Primitives.ComposablePartException.Element" /> property to <see langword="null" />.</param>
		public ComposablePartException(string message, ICompositionElement element)
			: this(message, element, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" /> class with the specified error message and the exception that is the cause of this exception.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.Exception.Message" /> property to its default value.</param>
		/// <param name="innerException">The exception that is the underlying cause of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.Exception.InnerException" /> property to <see langword="null" />.</param>
		public ComposablePartException(string message, Exception innerException)
			: this(message, null, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" /> class with the specified error message, and the composition element and exception that are the cause of this exception.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.Exception.Message" /> property to its default value.</param>
		/// <param name="element">The composition element that is the cause of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.Primitives.ComposablePartException.Element" /> property to <see langword="null" />.</param>
		/// <param name="innerException">The exception that is the underlying cause of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />, or <see langword="null" /> to set the <see cref="P:System.Exception.InnerException" /> property to <see langword="null" />.</param>
		public ComposablePartException(string message, ICompositionElement element, Exception innerException)
			: base(message, innerException)
		{
			_element = element;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" /> class with the specified serialization data.</summary>
		/// <param name="info">An object that holds the serialized object data for the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />.</param>
		/// <param name="context">An object that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <paramref name="info" /> is missing a required value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="info" /> contains a value that cannot be cast to the correct type.</exception>
		[SecuritySafeCritical]
		protected ComposablePartException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			_element = info.GetValue<ICompositionElement>("Element");
		}

		/// <summary>Gets the serialization data for the exception.</summary>
		/// <param name="info">After calling the method, contains serialized object data about the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException" />.</param>
		/// <param name="context">After calling the method, contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Element", _element.ToSerializableElement());
		}
	}
	internal class ComposablePartExceptionDebuggerProxy
	{
		private readonly ComposablePartException _exception;

		public ICompositionElement Element => _exception.Element;

		public Exception InnerException => _exception.InnerException;

		public string Message => _exception.Message;

		public ComposablePartExceptionDebuggerProxy(ComposablePartException exception)
		{
			Requires.NotNull(exception, "exception");
			_exception = exception;
		}
	}
	[Serializable]
	[DebuggerTypeProxy(typeof(CompositionElementDebuggerProxy))]
	internal class CompositionElement : SerializableCompositionElement
	{
		private static readonly ICompositionElement UnknownOrigin = new SerializableCompositionElement(Strings.CompositionElement_UnknownOrigin, null);

		private readonly object _underlyingObject;

		public object UnderlyingObject => _underlyingObject;

		public CompositionElement(object underlyingObject)
			: base(underlyingObject.ToString(), UnknownOrigin)
		{
			_underlyingObject = underlyingObject;
		}
	}
	internal class CompositionElementDebuggerProxy
	{
		private readonly CompositionElement _element;

		public string DisplayName => _element.DisplayName;

		public ICompositionElement Origin => _element.Origin;

		public object UnderlyingObject => _element.UnderlyingObject;

		public CompositionElementDebuggerProxy(CompositionElement element)
		{
			Requires.NotNull(element, "element");
			_element = element;
		}
	}
	internal static class CompositionElementExtensions
	{
		public static ICompositionElement ToSerializableElement(this ICompositionElement element)
		{
			return SerializableCompositionElement.FromICompositionElement(element);
		}

		public static ICompositionElement ToElement(this Export export)
		{
			if (export is ICompositionElement result)
			{
				return result;
			}
			return export.Definition.ToElement();
		}

		public static ICompositionElement ToElement(this ExportDefinition definition)
		{
			return ToElementCore(definition);
		}

		public static ICompositionElement ToElement(this ImportDefinition definition)
		{
			return ToElementCore(definition);
		}

		public static ICompositionElement ToElement(this ComposablePart part)
		{
			return ToElementCore(part);
		}

		public static ICompositionElement ToElement(this ComposablePartDefinition definition)
		{
			return ToElementCore(definition);
		}

		public static string GetDisplayName(this ComposablePartDefinition definition)
		{
			return GetDisplayNameCore(definition);
		}

		public static string GetDisplayName(this ComposablePartCatalog catalog)
		{
			return GetDisplayNameCore(catalog);
		}

		private static string GetDisplayNameCore(object value)
		{
			if (value is ICompositionElement compositionElement)
			{
				return compositionElement.DisplayName;
			}
			return value.ToString();
		}

		private static ICompositionElement ToElementCore(object value)
		{
			if (value is ICompositionElement result)
			{
				return result;
			}
			return new CompositionElement(value);
		}
	}
	/// <summary>Represents an import that is required by a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object and that can specify both a contract name and metadata.</summary>
	public class ContractBasedImportDefinition : ImportDefinition
	{
		private readonly IEnumerable<KeyValuePair<string, Type>> _requiredMetadata = Enumerable.Empty<KeyValuePair<string, Type>>();

		private Expression<Func<ExportDefinition, bool>> _constraint;

		private readonly CreationPolicy _requiredCreationPolicy;

		private readonly string _requiredTypeIdentity;

		private bool _isRequiredMetadataValidated;

		/// <summary>Gets the expected type of the export that matches this <see cref="T:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition" />.</summary>
		/// <returns>A string that is generated by calling the <see cref="M:System.ComponentModel.Composition.AttributedModelServices.GetTypeIdentity(System.Type)" /> method on the type that this import expects. If the value is <see langword="null" />, this import does not expect a particular type.</returns>
		public virtual string RequiredTypeIdentity => _requiredTypeIdentity;

		/// <summary>Gets the metadata names of the export required by the import definition.</summary>
		/// <returns>A collection of <see cref="T:System.String" /> objects that contain the metadata names of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects required by the <see cref="T:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition" />. The default is an empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection.</returns>
		public virtual IEnumerable<KeyValuePair<string, Type>> RequiredMetadata
		{
			get
			{
				ValidateRequiredMetadata();
				return _requiredMetadata;
			}
		}

		/// <summary>Gets or sets a value that indicates that the importer requires a specific <see cref="T:System.ComponentModel.Composition.CreationPolicy" /> for the exports used to satisfy this import.</summary>
		/// <returns>One of the following values:  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.Any" />, if the importer does not require a specific <see cref="T:System.ComponentModel.Composition.CreationPolicy" />.  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.Shared" /> to require that all exports used should be shared by all importers in the container.  
		///  <see cref="F:System.ComponentModel.Composition.CreationPolicy.NonShared" /> to require that all exports used should be non-shared in the container. In this case, each importer receives a separate instance.</returns>
		public virtual CreationPolicy RequiredCreationPolicy => _requiredCreationPolicy;

		/// <summary>Gets an expression that defines conditions that must be matched to satisfy the import described by this import definition.</summary>
		/// <returns>An expression that contains a <see cref="T:System.Func`2" /> object that defines the conditions that must be matched for the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> to be satisfied by an <see cref="T:System.ComponentModel.Composition.Primitives.Export" />.</returns>
		public override Expression<Func<ExportDefinition, bool>> Constraint
		{
			get
			{
				if (_constraint == null)
				{
					_constraint = ConstraintServices.CreateConstraint(ContractName, RequiredTypeIdentity, RequiredMetadata, RequiredCreationPolicy);
				}
				return _constraint;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition" /> class.</summary>
		protected ContractBasedImportDefinition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition" /> class with the specified contract name, required type identity, required metadata, cardinality, and creation policy, and indicates whether the import definition is recomposable or a prerequisite.</summary>
		/// <param name="contractName">The contract name of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object required by the import definition.</param>
		/// <param name="requiredTypeIdentity">The type identity of the export type expected. Use the <see cref="M:System.ComponentModel.Composition.AttributedModelServices.GetTypeIdentity(System.Type)" /> method to generate a type identity for a given type. If no specific type is required, use <see langword="null" />.</param>
		/// <param name="requiredMetadata">A collection of key/value pairs that contain the metadata names and types required by the import definition; or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition.RequiredMetadata" /> property to an empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection.</param>
		/// <param name="cardinality">One of the enumeration values that indicates the cardinality of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects required by the import definition.</param>
		/// <param name="isRecomposable">
		///   <see langword="true" /> to specify that the import definition can be satisfied multiple times throughout the lifetime of a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" />; otherwise, <see langword="false" />.</param>
		/// <param name="isPrerequisite">
		///   <see langword="true" /> to specify that the import definition is required to be satisfied before a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> can start producing exported objects; otherwise, <see langword="false" />.</param>
		/// <param name="requiredCreationPolicy">A value that indicates that the importer requires a specific creation policy for the exports used to satisfy this import. If no specific creation policy is needed, the default is <see cref="F:System.ComponentModel.Composition.CreationPolicy.Any" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contractName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contractName" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="requiredMetadata" /> contains an element that is <see langword="null" />.  
		/// -or-  
		/// <paramref name="cardinality" /> is not one of the <see cref="T:System.ComponentModel.Composition.Primitives.ImportCardinality" /> values.</exception>
		public ContractBasedImportDefinition(string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, bool isRecomposable, bool isPrerequisite, CreationPolicy requiredCreationPolicy)
			: this(contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, isPrerequisite, requiredCreationPolicy, MetadataServices.EmptyMetadata)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition" /> class with the specified contract name, required type identity, required and optional metadata, cardinality, and creation policy, and indicates whether the import definition is recomposable or a prerequisite.</summary>
		/// <param name="contractName">The contract name of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object required by the import definition.</param>
		/// <param name="requiredTypeIdentity">The type identity of the export type expected. Use the <see cref="M:System.ComponentModel.Composition.AttributedModelServices.GetTypeIdentity(System.Type)" /> method to generate a type identity for a given type. If no specific type is required, use <see langword="null" />.</param>
		/// <param name="requiredMetadata">A collection of key/value pairs that contain the metadata names and types required by the import definition; or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition.RequiredMetadata" /> property to an empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection.</param>
		/// <param name="cardinality">One of the enumeration values that indicates the cardinality of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects required by the import definition.</param>
		/// <param name="isRecomposable">
		///   <see langword="true" /> to specify that the import definition can be satisfied multiple times throughout the lifetime of a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" />; otherwise, <see langword="false" />.</param>
		/// <param name="isPrerequisite">
		///   <see langword="true" /> to specify that the import definition is required to be satisfied before a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> can start producing exported objects; otherwise, <see langword="false" />.</param>
		/// <param name="requiredCreationPolicy">A value that indicates that the importer requires a specific creation policy for the exports used to satisfy this import. If no specific creation policy is needed, the default is <see cref="F:System.ComponentModel.Composition.CreationPolicy.Any" />.</param>
		/// <param name="metadata">The metadata associated with this import.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contractName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contractName" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="requiredMetadata" /> contains an element that is <see langword="null" />.  
		/// -or-  
		/// <paramref name="cardinality" /> is not one of the <see cref="T:System.ComponentModel.Composition.Primitives.ImportCardinality" /> values.</exception>
		public ContractBasedImportDefinition(string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, bool isRecomposable, bool isPrerequisite, CreationPolicy requiredCreationPolicy, IDictionary<string, object> metadata)
			: base(contractName, cardinality, isRecomposable, isPrerequisite, metadata)
		{
			Requires.NotNullOrEmpty(contractName, "contractName");
			_requiredTypeIdentity = requiredTypeIdentity;
			if (requiredMetadata != null)
			{
				_requiredMetadata = requiredMetadata;
			}
			_requiredCreationPolicy = requiredCreationPolicy;
		}

		private void ValidateRequiredMetadata()
		{
			if (_isRequiredMetadataValidated)
			{
				return;
			}
			foreach (KeyValuePair<string, Type> requiredMetadatum in _requiredMetadata)
			{
				if (requiredMetadatum.Key == null || requiredMetadatum.Value == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.Argument_NullElement, "requiredMetadata"));
				}
			}
			_isRequiredMetadataValidated = true;
		}

		/// <summary>Returns a value indicating whether the constraint represented by this object is satisfied by the export represented by the given export definition.</summary>
		/// <param name="exportDefinition">The export definition to test.</param>
		/// <returns>
		///   <see langword="true" /> if the constraint is satisfied; otherwise, <see langword="false" />.</returns>
		public override bool IsConstraintSatisfiedBy(ExportDefinition exportDefinition)
		{
			Requires.NotNull(exportDefinition, "exportDefinition");
			if (!StringComparers.ContractName.Equals(ContractName, exportDefinition.ContractName))
			{
				return false;
			}
			return MatchRequiredMatadata(exportDefinition);
		}

		private bool MatchRequiredMatadata(ExportDefinition definition)
		{
			if (!string.IsNullOrEmpty(RequiredTypeIdentity))
			{
				string value = definition.Metadata.GetValue<string>("ExportTypeIdentity");
				if (!StringComparers.ContractName.Equals(RequiredTypeIdentity, value))
				{
					return false;
				}
			}
			foreach (KeyValuePair<string, Type> requiredMetadatum in RequiredMetadata)
			{
				string key = requiredMetadatum.Key;
				Type value2 = requiredMetadatum.Value;
				object value3 = null;
				if (!definition.Metadata.TryGetValue(key, out value3))
				{
					return false;
				}
				if (value3 != null)
				{
					if (!value2.IsInstanceOfType(value3))
					{
						return false;
					}
				}
				else if (value2.IsValueType)
				{
					return false;
				}
			}
			if (RequiredCreationPolicy == CreationPolicy.Any)
			{
				return true;
			}
			CreationPolicy value4 = definition.Metadata.GetValue<CreationPolicy>("System.ComponentModel.Composition.CreationPolicy");
			if (value4 != CreationPolicy.Any)
			{
				return value4 == RequiredCreationPolicy;
			}
			return true;
		}

		/// <summary>Returns the string representation of this <see cref="T:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition" /> object.</summary>
		/// <returns>The string representation of this <see cref="T:System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition" /> object.</returns>
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($"\n\tContractName\t{ContractName}");
			stringBuilder.Append($"\n\tRequiredTypeIdentity\t{RequiredTypeIdentity}");
			if (_requiredCreationPolicy != CreationPolicy.Any)
			{
				stringBuilder.Append($"\n\tRequiredCreationPolicy\t{RequiredCreationPolicy}");
			}
			if (_requiredMetadata.Count() > 0)
			{
				stringBuilder.Append($"\n\tRequiredMetadata");
				foreach (KeyValuePair<string, Type> requiredMetadatum in _requiredMetadata)
				{
					stringBuilder.Append($"\n\t\t{requiredMetadatum.Key}\t({requiredMetadatum.Value})");
				}
			}
			return stringBuilder.ToString();
		}
	}
	/// <summary>Represents an export, which is a type that consists of a delay-created exported object and the metadata that describes that object.</summary>
	public class Export
	{
		private readonly ExportDefinition _definition;

		private readonly Func<object> _exportedValueGetter;

		private static readonly object _EmptyValue = new object();

		private volatile object _exportedValue = _EmptyValue;

		/// <summary>Gets the definition that describes the contract that the export satisfies.</summary>
		/// <returns>A definition that describes the contract that the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object satisfies.</returns>
		/// <exception cref="T:System.NotImplementedException">This property was not overridden by a derived class.</exception>
		public virtual ExportDefinition Definition
		{
			get
			{
				if (_definition != null)
				{
					return _definition;
				}
				throw ExceptionBuilder.CreateNotOverriddenByDerived("Definition");
			}
		}

		/// <summary>Gets the metadata for the export.</summary>
		/// <returns>The metadata of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" />.</returns>
		/// <exception cref="T:System.NotImplementedException">The <see cref="P:System.ComponentModel.Composition.Primitives.Export.Definition" /> property was not overridden by a derived class.</exception>
		public IDictionary<string, object> Metadata => Definition.Metadata;

		/// <summary>Provides the object this export represents.</summary>
		/// <returns>The object this export represents.</returns>
		public object Value
		{
			get
			{
				if (_exportedValue == _EmptyValue)
				{
					object exportedValueCore = GetExportedValueCore();
					Interlocked.CompareExchange(ref _exportedValue, exportedValueCore, _EmptyValue);
				}
				return _exportedValue;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> class.</summary>
		protected Export()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> class with the specified contract name and exported value getter.</summary>
		/// <param name="contractName">The contract name of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object.</param>
		/// <param name="exportedValueGetter">A method that is called to create the exported object of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" />. This delays the creation of the object until the <see cref="P:System.ComponentModel.Composition.Primitives.Export.Value" /> method is called.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contractName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="exportedObjectGetter" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contractName" /> is an empty string ("").</exception>
		public Export(string contractName, Func<object> exportedValueGetter)
			: this(new ExportDefinition(contractName, null), exportedValueGetter)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> class with the specified contract name, metadata, and exported value getter.</summary>
		/// <param name="contractName">The contract name of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object.</param>
		/// <param name="metadata">The metadata of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.Primitives.Export.Metadata" /> property to an empty, read-only <see cref="T:System.Collections.Generic.IDictionary`2" /> object.</param>
		/// <param name="exportedValueGetter">A method that is called to create the exported object of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" />. This delays the creation of the object until the <see cref="P:System.ComponentModel.Composition.Primitives.Export.Value" /> method is called.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contractName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="exportedObjectGetter" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contractName" /> is an empty string ("").</exception>
		public Export(string contractName, IDictionary<string, object> metadata, Func<object> exportedValueGetter)
			: this(new ExportDefinition(contractName, metadata), exportedValueGetter)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> class with the specified export definition and exported object getter.</summary>
		/// <param name="definition">An object that describes the contract that the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object satisfies.</param>
		/// <param name="exportedValueGetter">A method that is called to create the exported object of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" />. This delays the creation of the object until the <see cref="P:System.ComponentModel.Composition.Primitives.Export.Value" /> property is called.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="exportedObjectGetter" /> is <see langword="null" />.</exception>
		public Export(ExportDefinition definition, Func<object> exportedValueGetter)
		{
			Requires.NotNull(definition, "definition");
			Requires.NotNull(exportedValueGetter, "exportedValueGetter");
			_definition = definition;
			_exportedValueGetter = exportedValueGetter;
		}

		/// <summary>Returns the exported object the export provides.</summary>
		/// <returns>The exported object the export provides.</returns>
		/// <exception cref="T:System.NotImplementedException">The <see cref="M:System.ComponentModel.Composition.Primitives.Export.GetExportedValueCore" /> method was not overridden by a derived class.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionException">An error occurred during composition. <see cref="P:System.ComponentModel.Composition.CompositionException.Errors" /> will contain a collection of errors that occurred.</exception>
		protected virtual object GetExportedValueCore()
		{
			if (_exportedValueGetter != null)
			{
				return _exportedValueGetter();
			}
			throw ExceptionBuilder.CreateNotOverriddenByDerived("GetExportedValueCore");
		}
	}
	/// <summary>Describes the contract that a particular <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object satisfies.</summary>
	public class ExportDefinition
	{
		private readonly IDictionary<string, object> _metadata = MetadataServices.EmptyMetadata;

		private readonly string _contractName;

		/// <summary>Gets the contract name.</summary>
		/// <returns>The contract name of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> object.</returns>
		/// <exception cref="T:System.NotImplementedException">The property was not overridden by a derived class.</exception>
		public virtual string ContractName
		{
			get
			{
				if (_contractName != null)
				{
					return _contractName;
				}
				throw ExceptionBuilder.CreateNotOverriddenByDerived("ContractName");
			}
		}

		/// <summary>Gets the contract metadata.</summary>
		/// <returns>The metadata of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" />. The default is an empty, read-only <see cref="T:System.Collections.Generic.IDictionary`2" /> object.</returns>
		public virtual IDictionary<string, object> Metadata => _metadata;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> class.</summary>
		protected ExportDefinition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> class with the specified contract name and metadata.</summary>
		/// <param name="contractName">The contract name of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> object.</param>
		/// <param name="metadata">The metadata of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.Primitives.ExportDefinition.Metadata" /> property to an empty, read-only <see cref="T:System.Collections.Generic.IDictionary`2" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contractName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contractName" /> is an empty string ("").</exception>
		public ExportDefinition(string contractName, IDictionary<string, object> metadata)
		{
			Requires.NotNullOrEmpty(contractName, "contractName");
			_contractName = contractName;
			if (metadata != null)
			{
				_metadata = metadata.AsReadOnly();
			}
		}

		/// <summary>Returns a string representation of the export definition.</summary>
		/// <returns>A string representation of the export definition.</returns>
		public override string ToString()
		{
			return ContractName;
		}
	}
	/// <summary>Represents a function exported by a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" />.</summary>
	public class ExportedDelegate
	{
		private object _instance;

		private MethodInfo _method;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportedDelegate" /> class.</summary>
		protected ExportedDelegate()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportedDelegate" /> class for the specified part and method.</summary>
		/// <param name="instance">The part exporting the method.</param>
		/// <param name="method">The method to be exported.</param>
		public ExportedDelegate(object instance, MethodInfo method)
		{
			Requires.NotNull(method, "method");
			_instance = instance;
			_method = method;
		}

		/// <summary>Gets a delegate of the specified type.</summary>
		/// <param name="delegateType">The type of the delegate to return.</param>
		/// <returns>A delegate of the specified type, or <see langword="null" /> if no such delegate can be created.</returns>
		public virtual Delegate CreateDelegate(Type delegateType)
		{
			Requires.NotNull(delegateType, "delegateType");
			if (delegateType == typeof(Delegate) || delegateType == typeof(MulticastDelegate))
			{
				delegateType = CreateStandardDelegateType();
			}
			return Delegate.CreateDelegate(delegateType, _instance, _method, throwOnBindFailure: false);
		}

		private Type CreateStandardDelegateType()
		{
			ParameterInfo[] parameters = _method.GetParameters();
			Type[] array = new Type[parameters.Length + 1];
			array[parameters.Length] = _method.ReturnType;
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			return Expression.GetDelegateType(array);
		}
	}
	/// <summary>Represents an element that participates in composition.</summary>
	public interface ICompositionElement
	{
		/// <summary>Gets the display name of the composition element.</summary>
		/// <returns>The human-readable display name of the <see cref="T:System.ComponentModel.Composition.Primitives.ICompositionElement" />.</returns>
		string DisplayName { get; }

		/// <summary>Gets the composition element from which the current composition element originated.</summary>
		/// <returns>The composition element from which the current <see cref="T:System.ComponentModel.Composition.Primitives.ICompositionElement" /> originated, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Composition.Primitives.ICompositionElement" /> is the root composition element.</returns>
		ICompositionElement Origin { get; }
	}
	internal interface IPartCreatorImportDefinition
	{
		ContractBasedImportDefinition ProductImportDefinition { get; }
	}
	/// <summary>Indicates the cardinality of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects required by an <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />.</summary>
	public enum ImportCardinality
	{
		/// <summary>Zero or one <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects are required by the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />.</summary>
		ZeroOrOne,
		/// <summary>Exactly one <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object is required by the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />.</summary>
		ExactlyOne,
		/// <summary>Zero or more <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects are required by the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />.</summary>
		ZeroOrMore
	}
	/// <summary>Represents an import that is required by a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object.</summary>
	public class ImportDefinition
	{
		internal static readonly string EmptyContractName = string.Empty;

		private readonly Expression<Func<ExportDefinition, bool>> _constraint;

		private readonly ImportCardinality _cardinality = ImportCardinality.ExactlyOne;

		private readonly string _contractName = EmptyContractName;

		private readonly bool _isRecomposable;

		private readonly bool _isPrerequisite = true;

		private Func<ExportDefinition, bool> _compiledConstraint;

		private readonly IDictionary<string, object> _metadata = MetadataServices.EmptyMetadata;

		/// <summary>Gets the name of the contract.</summary>
		/// <returns>The contract name.</returns>
		public virtual string ContractName => _contractName;

		/// <summary>Gets the metadata associated with this import.</summary>
		/// <returns>A collection that contains the metadata associated with this import.</returns>
		public virtual IDictionary<string, object> Metadata => _metadata;

		/// <summary>Gets the cardinality of the exports required by the import definition.</summary>
		/// <returns>One of the enumeration values that indicates the cardinality of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects required by the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />. The default is <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ExactlyOne" />.</returns>
		public virtual ImportCardinality Cardinality => _cardinality;

		/// <summary>Gets an expression that defines conditions that the import must satisfy to match the import definition.</summary>
		/// <returns>An expression that contains a <see cref="T:System.Func`2" /> object that defines the conditions an <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> must satisfy to match the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />.</returns>
		/// <exception cref="T:System.NotImplementedException">The property was not overridden by a derived class.</exception>
		public virtual Expression<Func<ExportDefinition, bool>> Constraint
		{
			get
			{
				if (_constraint != null)
				{
					return _constraint;
				}
				throw ExceptionBuilder.CreateNotOverriddenByDerived("Constraint");
			}
		}

		/// <summary>Gets a value that indicates whether the import definition must be satisfied before a part can start producing exported objects.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> must be satisfied before a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object can start producing exported objects; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		public virtual bool IsPrerequisite => _isPrerequisite;

		/// <summary>Gets a value that indicates whether the import definition can be satisfied multiple times.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> can be satisfied multiple times throughout the lifetime of a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		public virtual bool IsRecomposable => _isRecomposable;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> class.</summary>
		protected ImportDefinition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> class with the specified constraint, contract name, and cardinality, and indicates whether the import definition is recomposable or a prerequisite.</summary>
		/// <param name="constraint">An expression that contains a <see cref="T:System.Func`2" /> object that defines the conditions an <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> must match to satisfy the import definition.</param>
		/// <param name="contractName">The contract name.</param>
		/// <param name="cardinality">One of the enumeration values that indicates the cardinality of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects required by the import definition.</param>
		/// <param name="isRecomposable">
		///   <see langword="true" /> to specify that the import definition can be satisfied multiple times throughout the lifetime of a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object; otherwise, <see langword="false" />.</param>
		/// <param name="isPrerequisite">
		///   <see langword="true" /> to specify that the import definition must be satisfied before a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> can start producing exported objects; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="constraint" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="cardinality" /> is not one of the values of <see cref="T:System.ComponentModel.Composition.Primitives.ImportCardinality" />.</exception>
		public ImportDefinition(Expression<Func<ExportDefinition, bool>> constraint, string contractName, ImportCardinality cardinality, bool isRecomposable, bool isPrerequisite)
			: this(contractName, cardinality, isRecomposable, isPrerequisite, MetadataServices.EmptyMetadata)
		{
			Requires.NotNull(constraint, "constraint");
			_constraint = constraint;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> class with the specified constraint, contract name, cardinality, and metadata, and indicates whether the import definition is recomposable or a prerequisite.</summary>
		/// <param name="constraint">An expression that contains a <see cref="T:System.Func`2" /> object that defines the conditions an <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> must match to satisfy the import definition.</param>
		/// <param name="contractName">The contract name.</param>
		/// <param name="cardinality">One of the enumeration values that indicates the cardinality of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects required by the import definition.</param>
		/// <param name="isRecomposable">
		///   <see langword="true" /> to specify that the import definition can be satisfied multiple times throughout the lifetime of a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object; otherwise, <see langword="false" />.</param>
		/// <param name="isPrerequisite">
		///   <see langword="true" /> to specify that the import definition must be satisfied before a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> can start producing exported objects; otherwise, <see langword="false" />.</param>
		/// <param name="metadata">The metadata associated with the import.</param>
		public ImportDefinition(Expression<Func<ExportDefinition, bool>> constraint, string contractName, ImportCardinality cardinality, bool isRecomposable, bool isPrerequisite, IDictionary<string, object> metadata)
			: this(contractName, cardinality, isRecomposable, isPrerequisite, metadata)
		{
			Requires.NotNull(constraint, "constraint");
			_constraint = constraint;
		}

		internal ImportDefinition(string contractName, ImportCardinality cardinality, bool isRecomposable, bool isPrerequisite, IDictionary<string, object> metadata)
		{
			if (cardinality != ImportCardinality.ExactlyOne && cardinality != ImportCardinality.ZeroOrMore && cardinality != ImportCardinality.ZeroOrOne)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ArgumentOutOfRange_InvalidEnum, "cardinality", cardinality, typeof(ImportCardinality).Name), "cardinality");
			}
			_contractName = contractName ?? EmptyContractName;
			_cardinality = cardinality;
			_isRecomposable = isRecomposable;
			_isPrerequisite = isPrerequisite;
			if (metadata != null)
			{
				_metadata = metadata;
			}
		}

		/// <summary>Gets a value that indicates whether the export represented by the specified definition satisfies the constraints of this import definition.</summary>
		/// <param name="exportDefinition">The export definition to test.</param>
		/// <returns>
		///   <see langword="true" /> if the constraints are satisfied; otherwise, <see langword="false" />.</returns>
		public virtual bool IsConstraintSatisfiedBy(ExportDefinition exportDefinition)
		{
			Requires.NotNull(exportDefinition, "exportDefinition");
			if (_compiledConstraint == null)
			{
				_compiledConstraint = Constraint.Compile();
			}
			return _compiledConstraint(exportDefinition);
		}

		/// <summary>Returns a string representation of the import definition.</summary>
		/// <returns>A string representation of the import definition.</returns>
		public override string ToString()
		{
			return Constraint.Body.ToString();
		}
	}
	internal static class PrimitivesServices
	{
		public static bool IsGeneric(this ComposablePartDefinition part)
		{
			return part.Metadata.GetValue<bool>("System.ComponentModel.Composition.IsGenericPart");
		}

		public static ImportDefinition GetProductImportDefinition(this ImportDefinition import)
		{
			if (import is IPartCreatorImportDefinition partCreatorImportDefinition)
			{
				return partCreatorImportDefinition.ProductImportDefinition;
			}
			return import;
		}

		internal static IEnumerable<string> GetCandidateContractNames(this ImportDefinition import, ComposablePartDefinition part)
		{
			import = import.GetProductImportDefinition();
			string text = import.ContractName;
			string genericContractName = import.Metadata.GetValue<string>("System.ComponentModel.Composition.GenericContractName");
			int[] value = import.Metadata.GetValue<int[]>("System.ComponentModel.Composition.GenericImportParametersOrderMetadataName");
			if (value != null)
			{
				int value2 = part.Metadata.GetValue<int>("System.ComponentModel.Composition.GenericPartArity");
				if (value2 > 0)
				{
					text = GenericServices.GetGenericName(text, value, value2);
				}
			}
			yield return text;
			if (!string.IsNullOrEmpty(genericContractName))
			{
				yield return genericContractName;
			}
		}

		internal static bool IsImportDependentOnPart(this ImportDefinition import, ComposablePartDefinition part, ExportDefinition export, bool expandGenerics)
		{
			import = import.GetProductImportDefinition();
			if (expandGenerics)
			{
				return part.GetExports(import).Any();
			}
			return TranslateImport(import, part).IsConstraintSatisfiedBy(export);
		}

		private static ImportDefinition TranslateImport(ImportDefinition import, ComposablePartDefinition part)
		{
			if (!(import is ContractBasedImportDefinition contractBasedImportDefinition))
			{
				return import;
			}
			int[] value = contractBasedImportDefinition.Metadata.GetValue<int[]>("System.ComponentModel.Composition.GenericImportParametersOrderMetadataName");
			if (value == null)
			{
				return import;
			}
			int value2 = part.Metadata.GetValue<int>("System.ComponentModel.Composition.GenericPartArity");
			if (value2 == 0)
			{
				return import;
			}
			string genericName = GenericServices.GetGenericName(contractBasedImportDefinition.ContractName, value, value2);
			string genericName2 = GenericServices.GetGenericName(contractBasedImportDefinition.RequiredTypeIdentity, value, value2);
			return new ContractBasedImportDefinition(genericName, genericName2, contractBasedImportDefinition.RequiredMetadata, contractBasedImportDefinition.Cardinality, contractBasedImportDefinition.IsRecomposable, isPrerequisite: false, contractBasedImportDefinition.RequiredCreationPolicy, contractBasedImportDefinition.Metadata);
		}
	}
	[Serializable]
	internal class SerializableCompositionElement : ICompositionElement
	{
		private readonly string _displayName;

		private readonly ICompositionElement _origin;

		public string DisplayName => _displayName;

		public ICompositionElement Origin => _origin;

		public SerializableCompositionElement(string displayName, ICompositionElement origin)
		{
			Assumes.IsTrue(origin?.GetType().IsSerializable ?? true);
			_displayName = displayName ?? string.Empty;
			_origin = origin;
		}

		public override string ToString()
		{
			return DisplayName;
		}

		public static ICompositionElement FromICompositionElement(ICompositionElement element)
		{
			if (element == null)
			{
				return null;
			}
			ICompositionElement origin = FromICompositionElement(element.Origin);
			return new SerializableCompositionElement(element.DisplayName, origin);
		}
	}
}
namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>A catalog that combines the elements of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> objects.</summary>
	public class AggregateCatalog : ComposablePartCatalog, INotifyComposablePartCatalogChanged
	{
		private ComposablePartCatalogCollection _catalogs;

		private volatile int _isDisposed;

		/// <summary>Gets the underlying catalogs of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> object.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> objects that underlie the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> object has been disposed of.</exception>
		public ICollection<ComposablePartCatalog> Catalogs
		{
			get
			{
				ThrowIfDisposed();
				return _catalogs;
			}
		}

		/// <summary>Occurs when the contents of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> object have changed.</summary>
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed
		{
			add
			{
				_catalogs.Changed += value;
			}
			remove
			{
				_catalogs.Changed -= value;
			}
		}

		/// <summary>Occurs when the contents of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> object are changing.</summary>
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing
		{
			add
			{
				_catalogs.Changing += value;
			}
			remove
			{
				_catalogs.Changing -= value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> class.</summary>
		public AggregateCatalog()
			: this((IEnumerable<ComposablePartCatalog>)null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> class with the specified catalogs.</summary>
		/// <param name="catalogs">A array of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="catalogs" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="catalogs" /> contains an element that is <see langword="null" />.</exception>
		public AggregateCatalog(params ComposablePartCatalog[] catalogs)
			: this((IEnumerable<ComposablePartCatalog>)catalogs)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> class with the specified catalogs.</summary>
		/// <param name="catalogs">A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> or <see langword="null" /> to create an empty <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="catalogs" /> contains an element that is <see langword="null" />.</exception>
		public AggregateCatalog(IEnumerable<ComposablePartCatalog> catalogs)
		{
			Requires.NullOrNotNullElements(catalogs, "catalogs");
			_catalogs = new ComposablePartCatalogCollection(catalogs, OnChanged, OnChanging);
		}

		/// <summary>Gets the export definitions that match the constraint expressed by the specified definition.</summary>
		/// <param name="definition">The conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects to be returned.</param>
		/// <returns>A collection of <see cref="T:System.Tuple`2" /> containing the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects and their associated <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects for objects that match the constraint specified by <paramref name="definition" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.</exception>
		public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			ThrowIfDisposed();
			Requires.NotNull(definition, "definition");
			List<Tuple<ComposablePartDefinition, ExportDefinition>> list = new List<Tuple<ComposablePartDefinition, ExportDefinition>>();
			foreach (ComposablePartCatalog catalog in _catalogs)
			{
				foreach (Tuple<ComposablePartDefinition, ExportDefinition> export in catalog.GetExports(definition))
				{
					list.Add(export);
				}
			}
			return list;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 0)
				{
					_catalogs.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Returns an enumerator that iterates through the catalog.</summary>
		/// <returns>An enumerator that can be used to iterate through the catalog.</returns>
		public override IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			return _catalogs.SelectMany((ComposablePartCatalog catalog) => catalog).GetEnumerator();
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.AggregateCatalog.Changed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartCatalogChangeEventArgs" /> object that contains the event data.</param>
		protected virtual void OnChanged(ComposablePartCatalogChangeEventArgs e)
		{
			_catalogs.OnChanged(this, e);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.AggregateCatalog.Changing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartCatalogChangeEventArgs" /> object that contains the event data.</param>
		protected virtual void OnChanging(ComposablePartCatalogChangeEventArgs e)
		{
			_catalogs.OnChanging(this, e);
		}

		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (_isDisposed == 1)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}
	}
	/// <summary>Retrieves exports provided by a collection of <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> objects.</summary>
	public class AggregateExportProvider : ExportProvider, IDisposable
	{
		private readonly ReadOnlyCollection<ExportProvider> _readOnlyProviders;

		private readonly ExportProvider[] _providers;

		private volatile int _isDisposed;

		/// <summary>Gets a collection that contains the providers that the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateExportProvider" /> object aggregates.</summary>
		/// <returns>A collection of the <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> objects that the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateExportProvider" /> aggregates.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.AggregateExportProvider" /> object has been disposed of.</exception>
		public ReadOnlyCollection<ExportProvider> Providers
		{
			get
			{
				ThrowIfDisposed();
				return _readOnlyProviders;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateExportProvider" /> class.</summary>
		/// <param name="providers">The prioritized list of export providers.</param>
		public AggregateExportProvider(params ExportProvider[] providers)
		{
			ExportProvider[] array = null;
			if (providers != null)
			{
				array = new ExportProvider[providers.Length];
				for (int i = 0; i < providers.Length; i++)
				{
					ExportProvider exportProvider = providers[i];
					if (exportProvider == null)
					{
						throw ExceptionBuilder.CreateContainsNullElement("providers");
					}
					array[i] = exportProvider;
					exportProvider.ExportsChanged += OnExportChangedInternal;
					exportProvider.ExportsChanging += OnExportChangingInternal;
				}
			}
			else
			{
				array = new ExportProvider[0];
			}
			_providers = array;
			_readOnlyProviders = new ReadOnlyCollection<ExportProvider>(_providers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateExportProvider" /> class.</summary>
		/// <param name="providers">The prioritized list of export providers. The providers are consulted in the order in which they are supplied.</param>
		/// <exception cref="T:System.ArgumentException">One or more elements of <paramref name="providers" /> are <see langword="null" />.</exception>
		public AggregateExportProvider(IEnumerable<ExportProvider> providers)
			: this(providers?.AsArray())
		{
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateExportProvider" /> class.</summary>
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateExportProvider" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 0)
			{
				ExportProvider[] providers = _providers;
				foreach (ExportProvider obj in providers)
				{
					obj.ExportsChanged -= OnExportChangedInternal;
					obj.ExportsChanging -= OnExportChangingInternal;
				}
			}
		}

		/// <summary>Gets all the exports that match the conditions of the specified import.</summary>
		/// <param name="definition">The conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects to be returned.</param>
		/// <param name="atomicComposition">The transactional container for the composition.</param>
		/// <returns>A collection that contains all the exports that match the specified condition.</returns>
		protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
		{
			ThrowIfDisposed();
			ExportProvider[] providers;
			if (definition.Cardinality == ImportCardinality.ZeroOrMore)
			{
				List<Export> list = new List<Export>();
				providers = _providers;
				for (int i = 0; i < providers.Length; i++)
				{
					foreach (Export export in providers[i].GetExports(definition, atomicComposition))
					{
						list.Add(export);
					}
				}
				return list;
			}
			IEnumerable<Export> enumerable = null;
			providers = _providers;
			for (int i = 0; i < providers.Length; i++)
			{
				IEnumerable<Export> exports;
				bool num = providers[i].TryGetExports(definition, atomicComposition, out exports);
				bool flag = exports.FastAny();
				if (num && flag)
				{
					return exports;
				}
				if (flag)
				{
					enumerable = ((enumerable != null) ? enumerable.Concat(exports) : exports);
				}
			}
			return enumerable;
		}

		private void OnExportChangedInternal(object sender, ExportsChangeEventArgs e)
		{
			OnExportsChanged(e);
		}

		private void OnExportChangingInternal(object sender, ExportsChangeEventArgs e)
		{
			OnExportsChanging(e);
		}

		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (_isDisposed == 1)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}
	}
	/// <summary>Discovers attributed parts in the dynamic link library (DLL) and EXE files in an application's directory and path.</summary>
	public class ApplicationCatalog : ComposablePartCatalog, ICompositionElement
	{
		private bool _isDisposed;

		private volatile AggregateCatalog _innerCatalog;

		private readonly object _thisLock = new object();

		private ICompositionElement _definitionOrigin;

		private ReflectionContext _reflectionContext;

		private AggregateCatalog InnerCatalog
		{
			get
			{
				if (_innerCatalog == null)
				{
					lock (_thisLock)
					{
						if (_innerCatalog == null)
						{
							string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
							Assumes.NotNull(baseDirectory);
							List<ComposablePartCatalog> list = new List<ComposablePartCatalog>();
							list.Add(CreateCatalog(baseDirectory, "*.exe"));
							list.Add(CreateCatalog(baseDirectory, "*.dll"));
							string relativeSearchPath = AppDomain.CurrentDomain.RelativeSearchPath;
							if (!string.IsNullOrEmpty(relativeSearchPath))
							{
								string[] array = relativeSearchPath.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
								foreach (string path in array)
								{
									string text = Path.Combine(baseDirectory, path);
									if (Directory.Exists(text))
									{
										list.Add(CreateCatalog(text, "*.dll"));
									}
								}
							}
							AggregateCatalog innerCatalog = new AggregateCatalog(list);
							_innerCatalog = innerCatalog;
						}
					}
				}
				return _innerCatalog;
			}
		}

		/// <summary>Gets the display name of the application catalog.</summary>
		/// <returns>A string that contains a human-readable display name of the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> object.</returns>
		string ICompositionElement.DisplayName => GetDisplayName();

		/// <summary>Gets the composition element from which the application catalog originated.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		ICompositionElement ICompositionElement.Origin => null;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ApplicationCatalog" /> class.</summary>
		public ApplicationCatalog()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ApplicationCatalog" /> class by using the specified source for parts.</summary>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the source for parts.</param>
		public ApplicationCatalog(ICompositionElement definitionOrigin)
		{
			Requires.NotNull(definitionOrigin, "definitionOrigin");
			_definitionOrigin = definitionOrigin;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ApplicationCatalog" /> class by using the specified reflection context.</summary>
		/// <param name="reflectionContext">The reflection context.</param>
		public ApplicationCatalog(ReflectionContext reflectionContext)
		{
			Requires.NotNull(reflectionContext, "reflectionContext");
			_reflectionContext = reflectionContext;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ApplicationCatalog" /> class by using the specified reflection context and source for parts.</summary>
		/// <param name="reflectionContext">The reflection context.</param>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the source for parts.</param>
		public ApplicationCatalog(ReflectionContext reflectionContext, ICompositionElement definitionOrigin)
		{
			Requires.NotNull(reflectionContext, "reflectionContext");
			Requires.NotNull(definitionOrigin, "definitionOrigin");
			_reflectionContext = reflectionContext;
			_definitionOrigin = definitionOrigin;
		}

		internal ComposablePartCatalog CreateCatalog(string location, string pattern)
		{
			if (_reflectionContext != null)
			{
				if (_definitionOrigin == null)
				{
					return new DirectoryCatalog(location, pattern, _reflectionContext);
				}
				return new DirectoryCatalog(location, pattern, _reflectionContext, _definitionOrigin);
			}
			if (_definitionOrigin == null)
			{
				return new DirectoryCatalog(location, pattern);
			}
			return new DirectoryCatalog(location, pattern, _definitionOrigin);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!_isDisposed)
				{
					IDisposable disposable = null;
					lock (_thisLock)
					{
						disposable = _innerCatalog;
						_innerCatalog = null;
						_isDisposed = true;
					}
					disposable?.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public override IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			ThrowIfDisposed();
			return InnerCatalog.GetEnumerator();
		}

		/// <summary>Gets the export definitions that match the constraint expressed by the specified import definition.</summary>
		/// <param name="definition">The conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects to be returned.</param>
		/// <returns>A collection of objects that contain the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects and their associated <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects that match the specified constraint.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.</exception>
		public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			ThrowIfDisposed();
			Requires.NotNull(definition, "definition");
			return InnerCatalog.GetExports(definition);
		}

		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}

		private string GetDisplayName()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} (Path=\"{1}\") (PrivateProbingPath=\"{2}\")", GetType().Name, AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath);
		}

		/// <summary>Retrieves a string representation of the application catalog.</summary>
		/// <returns>A string representation of the catalog.</returns>
		public override string ToString()
		{
			return GetDisplayName();
		}
	}
	/// <summary>Discovers attributed parts in a managed code assembly.</summary>
	[DebuggerTypeProxy(typeof(AssemblyCatalogDebuggerProxy))]
	public class AssemblyCatalog : ComposablePartCatalog, ICompositionElement
	{
		private readonly object _thisLock = new object();

		private readonly ICompositionElement _definitionOrigin;

		private volatile Assembly _assembly;

		private volatile ComposablePartCatalog _innerCatalog;

		private int _isDisposed;

		private ReflectionContext _reflectionContext;

		private ComposablePartCatalog InnerCatalog
		{
			get
			{
				ThrowIfDisposed();
				if (_innerCatalog == null)
				{
					CatalogReflectionContextAttribute firstAttribute = _assembly.GetFirstAttribute<CatalogReflectionContextAttribute>();
					Assembly assembly = ((firstAttribute != null) ? firstAttribute.CreateReflectionContext().MapAssembly(_assembly) : _assembly);
					lock (_thisLock)
					{
						if (_innerCatalog == null)
						{
							TypeCatalog innerCatalog = ((_reflectionContext != null) ? new TypeCatalog(assembly.GetTypes(), _reflectionContext, _definitionOrigin) : new TypeCatalog(assembly.GetTypes(), _definitionOrigin));
							Thread.MemoryBarrier();
							_innerCatalog = innerCatalog;
						}
					}
				}
				return _innerCatalog;
			}
		}

		/// <summary>Gets the assembly whose attributed types are contained in the assembly catalog.</summary>
		/// <returns>The assembly whose attributed <see cref="T:System.Type" /> objects are contained in the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" />.</returns>
		public Assembly Assembly => _assembly;

		/// <summary>Gets the display name of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</summary>
		/// <returns>A string that represents the type and assembly of this <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</returns>
		string ICompositionElement.DisplayName => GetDisplayName();

		/// <summary>Gets the composition element that this element originated from.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		ICompositionElement ICompositionElement.Origin => null;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified code base.</summary>
		/// <param name="codeBase">A string that specifies the code base of the assembly (that is, the path to the assembly file) that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="codeBase" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or earlier of the common language runtime is currently loaded and <paramref name="codeBase" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have path discovery permission.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="codeBase" /> could not be loaded.  
		/// -or-  
		/// <paramref name="codeBase" /> specified a directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="codeBase" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="codeBase" /> is not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codeBase" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		public AssemblyCatalog(string codeBase)
		{
			Requires.NotNullOrEmpty(codeBase, "codeBase");
			InitializeAssemblyCatalog(LoadAssembly(codeBase));
			_definitionOrigin = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified code base and reflection context.</summary>
		/// <param name="codeBase">A string that specifies the code base of the assembly (that is, the path to the assembly file) that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <param name="reflectionContext">The context used by the catalog to interpret types.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="codeBase" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="codeBase" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have path discovery permission.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="codeBase" /> could not be loaded.  
		/// -or-  
		/// <paramref name="codeBase" /> specified a directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="codebase" /> or <paramref name="reflectionContext" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="codeBase" /> is not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codeBase" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		public AssemblyCatalog(string codeBase, ReflectionContext reflectionContext)
		{
			Requires.NotNullOrEmpty(codeBase, "codeBase");
			Requires.NotNull(reflectionContext, "reflectionContext");
			InitializeAssemblyCatalog(LoadAssembly(codeBase));
			_reflectionContext = reflectionContext;
			_definitionOrigin = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified code base.</summary>
		/// <param name="codeBase">A string that specifies the code base of the assembly (that is, the path to the assembly file) that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the sources of parts.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="codeBase" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="codeBase" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have path discovery permission.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="codeBase" /> could not be loaded.  
		/// -or-  
		/// <paramref name="codeBase" /> specified a directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="codebase" /> or <paramref name="definitionOrigin" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="codeBase" /> is not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codeBase" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		public AssemblyCatalog(string codeBase, ICompositionElement definitionOrigin)
		{
			Requires.NotNullOrEmpty(codeBase, "codeBase");
			Requires.NotNull(definitionOrigin, "definitionOrigin");
			InitializeAssemblyCatalog(LoadAssembly(codeBase));
			_definitionOrigin = definitionOrigin;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified code base and reflection context.</summary>
		/// <param name="codeBase">A string that specifies the code base of the assembly (that is, the path to the assembly file) that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <param name="reflectionContext">The context used by the catalog to interpret types.</param>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the sources of parts.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="codeBase" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="codeBase" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have path discovery permission.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="codeBase" /> could not be loaded.  
		/// -or-  
		/// <paramref name="codeBase" /> specified a directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="codebase" />, <paramref name="definitionOrigin" /> or <paramref name="reflectionContext" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="codeBase" /> is not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codeBase" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		public AssemblyCatalog(string codeBase, ReflectionContext reflectionContext, ICompositionElement definitionOrigin)
		{
			Requires.NotNullOrEmpty(codeBase, "codeBase");
			Requires.NotNull(reflectionContext, "reflectionContext");
			Requires.NotNull(definitionOrigin, "definitionOrigin");
			InitializeAssemblyCatalog(LoadAssembly(codeBase));
			_reflectionContext = reflectionContext;
			_definitionOrigin = definitionOrigin;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified assembly and reflection context.</summary>
		/// <param name="assembly">The assembly that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <param name="reflectionContext">The context used by the catalog to interpret types.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" /> or <paramref name="reflectionContext" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="assembly" /> was loaded in the reflection-only context.</exception>
		public AssemblyCatalog(Assembly assembly, ReflectionContext reflectionContext)
		{
			Requires.NotNull(assembly, "assembly");
			Requires.NotNull(reflectionContext, "reflectionContext");
			InitializeAssemblyCatalog(assembly);
			_reflectionContext = reflectionContext;
			_definitionOrigin = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified assembly and reflection context.</summary>
		/// <param name="assembly">The assembly that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <param name="reflectionContext">The context used by the catalog to interpret types.</param>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the sources of parts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" />, <paramref name="definitionOrigin" />, or <paramref name="reflectionContext" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="assembly" /> was loaded in the reflection-only context.</exception>
		public AssemblyCatalog(Assembly assembly, ReflectionContext reflectionContext, ICompositionElement definitionOrigin)
		{
			Requires.NotNull(assembly, "assembly");
			Requires.NotNull(reflectionContext, "reflectionContext");
			Requires.NotNull(definitionOrigin, "definitionOrigin");
			InitializeAssemblyCatalog(assembly);
			_reflectionContext = reflectionContext;
			_definitionOrigin = definitionOrigin;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified assembly.</summary>
		/// <param name="assembly">The assembly that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="assembly" /> was loaded in the reflection-only context.</exception>
		public AssemblyCatalog(Assembly assembly)
		{
			Requires.NotNull(assembly, "assembly");
			InitializeAssemblyCatalog(assembly);
			_definitionOrigin = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified assembly.</summary>
		/// <param name="assembly">The assembly that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the sources of parts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" /> or <paramref name="definitionOrigin" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="assembly" /> was loaded in the reflection-only context.</exception>
		public AssemblyCatalog(Assembly assembly, ICompositionElement definitionOrigin)
		{
			Requires.NotNull(assembly, "assembly");
			Requires.NotNull(definitionOrigin, "definitionOrigin");
			InitializeAssemblyCatalog(assembly);
			_definitionOrigin = definitionOrigin;
		}

		private void InitializeAssemblyCatalog(Assembly assembly)
		{
			_assembly = assembly;
		}

		/// <summary>Gets a collection of exports that match the conditions specified by the import definition.</summary>
		/// <param name="definition">Conditions that specify which exports to match.</param>
		/// <returns>A collection of exports that match the conditions specified by <paramref name="definition" />.</returns>
		public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			return InnerCatalog.GetExports(definition);
		}

		/// <summary>Gets a string representation of the assembly catalog.</summary>
		/// <returns>A representation of the assembly catalog.</returns>
		public override string ToString()
		{
			return GetDisplayName();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 0 && disposing && _innerCatalog != null)
				{
					_innerCatalog.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Returns an enumerator that iterates through the catalog.</summary>
		/// <returns>An enumerator that can be used to iterate through the catalog.</returns>
		public override IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			return InnerCatalog.GetEnumerator();
		}

		private void ThrowIfDisposed()
		{
			if (_isDisposed == 1)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}

		private string GetDisplayName()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} (Assembly=\"{1}\")", GetType().Name, Assembly.FullName);
		}

		private static Assembly LoadAssembly(string codeBase)
		{
			Requires.NotNullOrEmpty(codeBase, "codeBase");
			AssemblyName assemblyName;
			try
			{
				assemblyName = AssemblyName.GetAssemblyName(codeBase);
			}
			catch (ArgumentException)
			{
				assemblyName = new AssemblyName();
				assemblyName.CodeBase = codeBase;
			}
			return Assembly.Load(assemblyName);
		}
	}
	internal class AssemblyCatalogDebuggerProxy
	{
		private readonly AssemblyCatalog _catalog;

		public Assembly Assembly => _catalog.Assembly;

		public ReadOnlyCollection<ComposablePartDefinition> Parts => _catalog.Parts.ToReadOnlyCollection();

		public AssemblyCatalogDebuggerProxy(AssemblyCatalog catalog)
		{
			Requires.NotNull(catalog, "catalog");
			_catalog = catalog;
		}
	}
	/// <summary>Represents a single composition operation for transactional composition.</summary>
	public class AtomicComposition : IDisposable
	{
		private readonly AtomicComposition _outerAtomicComposition;

		private KeyValuePair<object, object>[] _values;

		private int _valueCount;

		private List<Action> _completeActionList;

		private List<Action> _revertActionList;

		private bool _isDisposed;

		private bool _isCompleted;

		private bool _containsInnerAtomicComposition;

		private bool ContainsInnerAtomicComposition
		{
			set
			{
				if (value && _containsInnerAtomicComposition)
				{
					throw new InvalidOperationException(Strings.AtomicComposition_AlreadyNested);
				}
				_containsInnerAtomicComposition = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AtomicComposition" /> class.</summary>
		public AtomicComposition()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AtomicComposition" /> class with the specified parent <see cref="T:System.ComponentModel.Composition.Hosting.AtomicComposition" />.</summary>
		/// <param name="outerAtomicComposition">The parent of this composition operation.</param>
		public AtomicComposition(AtomicComposition outerAtomicComposition)
		{
			if (outerAtomicComposition != null)
			{
				_outerAtomicComposition = outerAtomicComposition;
				_outerAtomicComposition.ContainsInnerAtomicComposition = true;
			}
		}

		/// <summary>Saves a key-value pair in the transaction to track tentative state.</summary>
		/// <param name="key">The key to save.</param>
		/// <param name="value">The value to save.</param>
		public void SetValue(object key, object value)
		{
			ThrowIfDisposed();
			ThrowIfCompleted();
			ThrowIfContainsInnerAtomicComposition();
			Requires.NotNull(key, "key");
			SetValueInternal(key, value);
		}

		/// <summary>Gets a value saved by the <see cref="M:System.ComponentModel.Composition.Hosting.AtomicComposition.SetValue(System.Object,System.Object)" /> method.</summary>
		/// <param name="key">The key to retrieve from.</param>
		/// <param name="value">The retrieved value.</param>
		/// <typeparam name="T">The type of the value to be retrieved.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the value was successfully retrieved; otherwise, <see langword="false" />.</returns>
		public bool TryGetValue<T>(object key, out T value)
		{
			return TryGetValue<T>(key, localAtomicCompositionOnly: false, out value);
		}

		/// <summary>Gets a value saved by the <see cref="M:System.ComponentModel.Composition.Hosting.AtomicComposition.SetValue(System.Object,System.Object)" /> method, with the option of not searching parent transactions.</summary>
		/// <param name="key">The key to retrieve from.</param>
		/// <param name="localAtomicCompositionOnly">
		///   <see langword="true" /> to exclude parent transactions; otherwise, <see langword="false" />.</param>
		/// <param name="value">The retrieved value.</param>
		/// <typeparam name="T">The type of the value to be retrieved.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the value was successfully retrieved; otherwise, <see langword="false" />.</returns>
		public bool TryGetValue<T>(object key, bool localAtomicCompositionOnly, out T value)
		{
			ThrowIfDisposed();
			ThrowIfCompleted();
			Requires.NotNull(key, "key");
			return TryGetValueInternal<T>(key, localAtomicCompositionOnly, out value);
		}

		/// <summary>Adds an action to be executed when the overall composition operation completes successfully.</summary>
		/// <param name="completeAction">The action to be executed.</param>
		public void AddCompleteAction(Action completeAction)
		{
			ThrowIfDisposed();
			ThrowIfCompleted();
			ThrowIfContainsInnerAtomicComposition();
			Requires.NotNull(completeAction, "completeAction");
			if (_completeActionList == null)
			{
				_completeActionList = new List<Action>();
			}
			_completeActionList.Add(completeAction);
		}

		/// <summary>Adds an action to be executed if the overall composition operation fails.</summary>
		/// <param name="revertAction">The action to be executed.</param>
		public void AddRevertAction(Action revertAction)
		{
			ThrowIfDisposed();
			ThrowIfCompleted();
			ThrowIfContainsInnerAtomicComposition();
			Requires.NotNull(revertAction, "revertAction");
			if (_revertActionList == null)
			{
				_revertActionList = new List<Action>();
			}
			_revertActionList.Add(revertAction);
		}

		/// <summary>Marks this composition operation as complete.</summary>
		public void Complete()
		{
			ThrowIfDisposed();
			ThrowIfCompleted();
			if (_outerAtomicComposition == null)
			{
				FinalComplete();
			}
			else
			{
				CopyComplete();
			}
			_isCompleted = true;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AtomicComposition" /> class, and mark this composition operation as failed.</summary>
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.AtomicComposition" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			ThrowIfDisposed();
			_isDisposed = true;
			if (_outerAtomicComposition != null)
			{
				_outerAtomicComposition.ContainsInnerAtomicComposition = false;
			}
			if (!_isCompleted && _revertActionList != null)
			{
				for (int num = _revertActionList.Count - 1; num >= 0; num--)
				{
					_revertActionList[num]();
				}
				_revertActionList = null;
			}
		}

		private void FinalComplete()
		{
			if (_completeActionList == null)
			{
				return;
			}
			foreach (Action completeAction in _completeActionList)
			{
				completeAction();
			}
			_completeActionList = null;
		}

		private void CopyComplete()
		{
			Assumes.NotNull(_outerAtomicComposition);
			_outerAtomicComposition.ContainsInnerAtomicComposition = false;
			if (_completeActionList != null)
			{
				foreach (Action completeAction in _completeActionList)
				{
					_outerAtomicComposition.AddCompleteAction(completeAction);
				}
			}
			if (_revertActionList != null)
			{
				foreach (Action revertAction in _revertActionList)
				{
					_outerAtomicComposition.AddRevertAction(revertAction);
				}
			}
			for (int i = 0; i < _valueCount; i++)
			{
				_outerAtomicComposition.SetValueInternal(_values[i].Key, _values[i].Value);
			}
		}

		private bool TryGetValueInternal<T>(object key, bool localAtomicCompositionOnly, out T value)
		{
			for (int i = 0; i < _valueCount; i++)
			{
				if (_values[i].Key == key)
				{
					value = (T)_values[i].Value;
					return true;
				}
			}
			if (!localAtomicCompositionOnly && _outerAtomicComposition != null)
			{
				return _outerAtomicComposition.TryGetValueInternal<T>(key, localAtomicCompositionOnly, out value);
			}
			value = default(T);
			return false;
		}

		private void SetValueInternal(object key, object value)
		{
			for (int i = 0; i < _valueCount; i++)
			{
				if (_values[i].Key == key)
				{
					_values[i] = new KeyValuePair<object, object>(key, value);
					return;
				}
			}
			if (_values == null || _valueCount == _values.Length)
			{
				KeyValuePair<object, object>[] array = new KeyValuePair<object, object>[(_valueCount == 0) ? 5 : (_valueCount * 2)];
				if (_values != null)
				{
					Array.Copy(_values, array, _valueCount);
				}
				_values = array;
			}
			_values[_valueCount] = new KeyValuePair<object, object>(key, value);
			_valueCount++;
		}

		[DebuggerStepThrough]
		private void ThrowIfContainsInnerAtomicComposition()
		{
			if (_containsInnerAtomicComposition)
			{
				throw new InvalidOperationException(Strings.AtomicComposition_PartOfAnotherAtomicComposition);
			}
		}

		[DebuggerStepThrough]
		private void ThrowIfCompleted()
		{
			if (_isCompleted)
			{
				throw new InvalidOperationException(Strings.AtomicComposition_AlreadyCompleted);
			}
		}

		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}
	}
	internal static class AtomicCompositionExtensions
	{
		internal static T GetValueAllowNull<T>(this AtomicComposition atomicComposition, T defaultResultAndKey) where T : class
		{
			Assumes.NotNull(defaultResultAndKey);
			return atomicComposition.GetValueAllowNull(defaultResultAndKey, defaultResultAndKey);
		}

		internal static T GetValueAllowNull<T>(this AtomicComposition atomicComposition, object key, T defaultResult)
		{
			if (atomicComposition != null && atomicComposition.TryGetValue<T>(key, out var value))
			{
				return value;
			}
			return defaultResult;
		}

		internal static void AddRevertActionAllowNull(this AtomicComposition atomicComposition, Action action)
		{
			Assumes.NotNull(action);
			if (atomicComposition == null)
			{
				action();
			}
			else
			{
				atomicComposition.AddRevertAction(action);
			}
		}

		internal static void AddCompleteActionAllowNull(this AtomicComposition atomicComposition, Action action)
		{
			Assumes.NotNull(action);
			if (atomicComposition == null)
			{
				action();
			}
			else
			{
				atomicComposition.AddCompleteAction(action);
			}
		}
	}
	/// <summary>Retrieves exports from a catalog.</summary>
	public class CatalogExportProvider : ExportProvider, IDisposable
	{
		private class CatalogChangeProxy : ComposablePartCatalog
		{
			private ComposablePartCatalog _originalCatalog;

			private List<ComposablePartDefinition> _addedParts;

			private HashSet<ComposablePartDefinition> _removedParts;

			public CatalogChangeProxy(ComposablePartCatalog originalCatalog, IEnumerable<ComposablePartDefinition> addedParts, IEnumerable<ComposablePartDefinition> removedParts)
			{
				_originalCatalog = originalCatalog;
				_addedParts = new List<ComposablePartDefinition>(addedParts);
				_removedParts = new HashSet<ComposablePartDefinition>(removedParts);
			}

			public override IEnumerator<ComposablePartDefinition> GetEnumerator()
			{
				return _originalCatalog.Concat(_addedParts).Except(_removedParts).GetEnumerator();
			}

			public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
			{
				Requires.NotNull(definition, "definition");
				IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> first = from partAndExport in _originalCatalog.GetExports(definition)
					where !_removedParts.Contains(partAndExport.Item1)
					select partAndExport;
				List<Tuple<ComposablePartDefinition, ExportDefinition>> list = new List<Tuple<ComposablePartDefinition, ExportDefinition>>();
				foreach (ComposablePartDefinition addedPart in _addedParts)
				{
					foreach (ExportDefinition exportDefinition in addedPart.ExportDefinitions)
					{
						if (definition.IsConstraintSatisfiedBy(exportDefinition))
						{
							list.Add(new Tuple<ComposablePartDefinition, ExportDefinition>(addedPart, exportDefinition));
						}
					}
				}
				return first.Concat(list);
			}
		}

		private class CatalogExport : Export
		{
			protected readonly CatalogExportProvider _catalogExportProvider;

			protected readonly ComposablePartDefinition _partDefinition;

			protected readonly ExportDefinition _definition;

			public override ExportDefinition Definition => _definition;

			protected virtual bool IsSharedPart => true;

			public CatalogExport(CatalogExportProvider catalogExportProvider, ComposablePartDefinition partDefinition, ExportDefinition definition)
			{
				_catalogExportProvider = catalogExportProvider;
				_partDefinition = partDefinition;
				_definition = definition;
			}

			protected CatalogPart GetPartCore()
			{
				return _catalogExportProvider.GetComposablePart(_partDefinition, IsSharedPart);
			}

			protected void ReleasePartCore(CatalogPart part, object value)
			{
				_catalogExportProvider.ReleasePart(value, part, null);
			}

			protected virtual CatalogPart GetPart()
			{
				return GetPartCore();
			}

			protected override object GetExportedValueCore()
			{
				return _catalogExportProvider.GetExportedValue(GetPart(), _definition, IsSharedPart);
			}

			public static CatalogExport CreateExport(CatalogExportProvider catalogExportProvider, ComposablePartDefinition partDefinition, ExportDefinition definition, CreationPolicy importCreationPolicy)
			{
				if (ShouldUseSharedPart(partDefinition.Metadata.GetValue<CreationPolicy>("System.ComponentModel.Composition.CreationPolicy"), importCreationPolicy))
				{
					return new CatalogExport(catalogExportProvider, partDefinition, definition);
				}
				return new NonSharedCatalogExport(catalogExportProvider, partDefinition, definition);
			}

			private static bool ShouldUseSharedPart(CreationPolicy partPolicy, CreationPolicy importPolicy)
			{
				switch (partPolicy)
				{
				case CreationPolicy.Any:
					if (importPolicy == CreationPolicy.Any || importPolicy == CreationPolicy.NewScope || importPolicy == CreationPolicy.Shared)
					{
						return true;
					}
					return false;
				case CreationPolicy.NonShared:
					Assumes.IsTrue(importPolicy != CreationPolicy.Shared);
					return false;
				default:
					Assumes.IsTrue(partPolicy == CreationPolicy.Shared);
					Assumes.IsTrue(importPolicy != CreationPolicy.NonShared && importPolicy != CreationPolicy.NewScope);
					return true;
				}
			}
		}

		private sealed class NonSharedCatalogExport : CatalogExport, IDisposable
		{
			private CatalogPart _part;

			private readonly object _lock = new object();

			protected override bool IsSharedPart => false;

			public NonSharedCatalogExport(CatalogExportProvider catalogExportProvider, ComposablePartDefinition partDefinition, ExportDefinition definition)
				: base(catalogExportProvider, partDefinition, definition)
			{
			}

			protected override CatalogPart GetPart()
			{
				if (_part == null)
				{
					CatalogPart catalogPart = GetPartCore();
					lock (_lock)
					{
						if (_part == null)
						{
							Thread.MemoryBarrier();
							_part = catalogPart;
							catalogPart = null;
						}
					}
					if (catalogPart != null)
					{
						ReleasePartCore(catalogPart, null);
					}
				}
				return _part;
			}

			void IDisposable.Dispose()
			{
				if (_part != null)
				{
					ReleasePartCore(_part, base.Value);
					_part = null;
				}
			}
		}

		internal abstract class FactoryExport : Export
		{
			private class FactoryExportPartDefinition : ComposablePartDefinition
			{
				private readonly FactoryExport _FactoryExport;

				public override IEnumerable<ExportDefinition> ExportDefinitions => new ExportDefinition[1] { _FactoryExport.Definition };

				public override IEnumerable<ImportDefinition> ImportDefinitions => Enumerable.Empty<ImportDefinition>();

				public ExportDefinition FactoryExportDefinition => _FactoryExport.Definition;

				public FactoryExportPartDefinition(FactoryExport FactoryExport)
				{
					_FactoryExport = FactoryExport;
				}

				public Export CreateProductExport()
				{
					return _FactoryExport.CreateExportProduct();
				}

				public override ComposablePart CreatePart()
				{
					return new FactoryExportPart(this);
				}
			}

			private sealed class FactoryExportPart : ComposablePart, IDisposable
			{
				private readonly FactoryExportPartDefinition _definition;

				private readonly Export _export;

				public override IEnumerable<ExportDefinition> ExportDefinitions => _definition.ExportDefinitions;

				public override IEnumerable<ImportDefinition> ImportDefinitions => _definition.ImportDefinitions;

				public FactoryExportPart(FactoryExportPartDefinition definition)
				{
					_definition = definition;
					_export = definition.CreateProductExport();
				}

				public override object GetExportedValue(ExportDefinition definition)
				{
					if (definition != _definition.FactoryExportDefinition)
					{
						throw ExceptionBuilder.CreateExportDefinitionNotOnThisComposablePart("definition");
					}
					return _export.Value;
				}

				public override void SetImport(ImportDefinition definition, IEnumerable<Export> exports)
				{
					throw ExceptionBuilder.CreateImportDefinitionNotOnThisComposablePart("definition");
				}

				public void Dispose()
				{
					if (_export is IDisposable disposable)
					{
						disposable.Dispose();
					}
				}
			}

			private readonly ComposablePartDefinition _partDefinition;

			private readonly ExportDefinition _exportDefinition;

			private ExportDefinition _factoryExportDefinition;

			private FactoryExportPartDefinition _factoryExportPartDefinition;

			public override ExportDefinition Definition => _factoryExportDefinition;

			protected ComposablePartDefinition UnderlyingPartDefinition => _partDefinition;

			protected ExportDefinition UnderlyingExportDefinition => _exportDefinition;

			public FactoryExport(ComposablePartDefinition partDefinition, ExportDefinition exportDefinition)
			{
				_partDefinition = partDefinition;
				_exportDefinition = exportDefinition;
				_factoryExportDefinition = new PartCreatorExportDefinition(_exportDefinition);
			}

			protected override object GetExportedValueCore()
			{
				if (_factoryExportPartDefinition == null)
				{
					_factoryExportPartDefinition = new FactoryExportPartDefinition(this);
				}
				return _factoryExportPartDefinition;
			}

			public abstract Export CreateExportProduct();
		}

		internal class PartCreatorExport : FactoryExport
		{
			private readonly CatalogExportProvider _catalogExportProvider;

			public PartCreatorExport(CatalogExportProvider catalogExportProvider, ComposablePartDefinition partDefinition, ExportDefinition exportDefinition)
				: base(partDefinition, exportDefinition)
			{
				_catalogExportProvider = catalogExportProvider;
			}

			public override Export CreateExportProduct()
			{
				return new NonSharedCatalogExport(_catalogExportProvider, base.UnderlyingPartDefinition, base.UnderlyingExportDefinition);
			}
		}

		internal class ScopeFactoryExport : FactoryExport
		{
			private sealed class ScopeCatalogExport : Export, IDisposable
			{
				private readonly ScopeFactoryExport _scopeFactoryExport;

				private Func<ComposablePartDefinition, bool> _catalogFilter;

				private CompositionContainer _childContainer;

				private Export _export;

				private readonly object _lock = new object();

				public override ExportDefinition Definition => _scopeFactoryExport.UnderlyingExportDefinition;

				public ScopeCatalogExport(ScopeFactoryExport scopeFactoryExport, Func<ComposablePartDefinition, bool> catalogFilter)
				{
					_scopeFactoryExport = scopeFactoryExport;
					_catalogFilter = catalogFilter;
				}

				protected override object GetExportedValueCore()
				{
					if (_export == null)
					{
						CompositionScopeDefinition childCatalog = new CompositionScopeDefinition(new FilteredCatalog(_scopeFactoryExport._catalog, _catalogFilter), _scopeFactoryExport._catalog.Children);
						CompositionContainer compositionContainer = _scopeFactoryExport._scopeManager.CreateChildContainer(childCatalog);
						Export export = compositionContainer.CatalogExportProvider.CreateExport(_scopeFactoryExport.UnderlyingPartDefinition, _scopeFactoryExport.UnderlyingExportDefinition, isExportFactory: false, CreationPolicy.Any);
						lock (_lock)
						{
							if (_export == null)
							{
								_childContainer = compositionContainer;
								Thread.MemoryBarrier();
								_export = export;
								compositionContainer = null;
								export = null;
							}
						}
						compositionContainer?.Dispose();
					}
					return _export.Value;
				}

				public void Dispose()
				{
					CompositionContainer compositionContainer = null;
					if (_export != null)
					{
						lock (_lock)
						{
							_ = _export;
							compositionContainer = _childContainer;
							_childContainer = null;
							Thread.MemoryBarrier();
							_export = null;
						}
					}
					compositionContainer?.Dispose();
				}
			}

			private readonly ScopeManager _scopeManager;

			private readonly CompositionScopeDefinition _catalog;

			internal ScopeFactoryExport(ScopeManager scopeManager, CompositionScopeDefinition catalog, ComposablePartDefinition partDefinition, ExportDefinition exportDefinition)
				: base(partDefinition, exportDefinition)
			{
				_scopeManager = scopeManager;
				_catalog = catalog;
			}

			public virtual Export CreateExportProduct(Func<ComposablePartDefinition, bool> filter)
			{
				return new ScopeCatalogExport(this, filter);
			}

			public override Export CreateExportProduct()
			{
				return new ScopeCatalogExport(this, null);
			}
		}

		internal class ScopeManager : ExportProvider
		{
			private CompositionScopeDefinition _scopeDefinition;

			private CatalogExportProvider _catalogExportProvider;

			public ScopeManager(CatalogExportProvider catalogExportProvider, CompositionScopeDefinition scopeDefinition)
			{
				Assumes.NotNull(catalogExportProvider);
				Assumes.NotNull(scopeDefinition);
				_scopeDefinition = scopeDefinition;
				_catalogExportProvider = catalogExportProvider;
			}

			protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
			{
				List<Export> list = new List<Export>();
				ImportDefinition importDefinition = TranslateImport(definition);
				if (importDefinition == null)
				{
					return list;
				}
				foreach (CompositionScopeDefinition child in _scopeDefinition.Children)
				{
					foreach (Tuple<ComposablePartDefinition, ExportDefinition> item in child.GetExportsFromPublicSurface(importDefinition))
					{
						using CompositionContainer compositionContainer = CreateChildContainer(child);
						using AtomicComposition parentAtomicComposition = new AtomicComposition(atomicComposition);
						if (!compositionContainer.CatalogExportProvider.DetermineRejection(item.Item1, parentAtomicComposition))
						{
							list.Add(CreateScopeExport(child, item.Item1, item.Item2));
						}
					}
				}
				return list;
			}

			private Export CreateScopeExport(CompositionScopeDefinition childCatalog, ComposablePartDefinition partDefinition, ExportDefinition exportDefinition)
			{
				return new ScopeFactoryExport(this, childCatalog, partDefinition, exportDefinition);
			}

			internal CompositionContainer CreateChildContainer(ComposablePartCatalog childCatalog)
			{
				return new CompositionContainer(childCatalog, _catalogExportProvider._compositionOptions, _catalogExportProvider._sourceProvider);
			}

			private static ImportDefinition TranslateImport(ImportDefinition definition)
			{
				if (!(definition is IPartCreatorImportDefinition { ProductImportDefinition: var productImportDefinition }))
				{
					return null;
				}
				ImportDefinition result = null;
				switch (productImportDefinition.RequiredCreationPolicy)
				{
				case CreationPolicy.NonShared:
				case CreationPolicy.NewScope:
					result = new ContractBasedImportDefinition(productImportDefinition.ContractName, productImportDefinition.RequiredTypeIdentity, productImportDefinition.RequiredMetadata, productImportDefinition.Cardinality, productImportDefinition.IsRecomposable, productImportDefinition.IsPrerequisite, CreationPolicy.Any, productImportDefinition.Metadata);
					break;
				case CreationPolicy.Any:
					result = productImportDefinition;
					break;
				}
				return result;
			}
		}

		private class InnerCatalogExportProvider : ExportProvider
		{
			private Func<ImportDefinition, AtomicComposition, IEnumerable<Export>> _getExportsCore;

			public InnerCatalogExportProvider(Func<ImportDefinition, AtomicComposition, IEnumerable<Export>> getExportsCore)
			{
				_getExportsCore = getExportsCore;
			}

			protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
			{
				Assumes.NotNull(_getExportsCore);
				return _getExportsCore(definition, atomicComposition);
			}
		}

		private enum AtomicCompositionQueryState
		{
			Unknown,
			TreatAsRejected,
			TreatAsValidated,
			NeedsTesting
		}

		private class CatalogPart
		{
			private volatile bool _importsSatisfied;

			public ComposablePart Part { get; private set; }

			public bool ImportsSatisfied
			{
				get
				{
					return _importsSatisfied;
				}
				set
				{
					_importsSatisfied = value;
				}
			}

			public CatalogPart(ComposablePart part)
			{
				Part = part;
			}
		}

		private readonly CompositionLock _lock;

		private Dictionary<ComposablePartDefinition, CatalogPart> _activatedParts = new Dictionary<ComposablePartDefinition, CatalogPart>();

		private HashSet<ComposablePartDefinition> _rejectedParts = new HashSet<ComposablePartDefinition>();

		private ConditionalWeakTable<object, List<ComposablePart>> _gcRoots;

		private HashSet<IDisposable> _partsToDispose = new HashSet<IDisposable>();

		private ComposablePartCatalog _catalog;

		private volatile bool _isDisposed;

		private volatile bool _isRunning;

		private ExportProvider _sourceProvider;

		private ImportEngine _importEngine;

		private CompositionOptions _compositionOptions;

		private ExportProvider _innerExportProvider;

		/// <summary>Gets the catalog that is used to provide exports.</summary>
		/// <returns>The catalog that the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> uses to produce <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> has been disposed of.</exception>
		public ComposablePartCatalog Catalog
		{
			get
			{
				ThrowIfDisposed();
				return _catalog;
			}
		}

		/// <summary>Gets or sets the export provider that provides access to additional exports.</summary>
		/// <returns>The export provider that provides the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> access to additional <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This property has already been set.  
		///  -or-  
		///  The methods on the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> object have already been accessed.</exception>
		public ExportProvider SourceProvider
		{
			get
			{
				ThrowIfDisposed();
				using (_lock.LockStateForRead())
				{
					return _sourceProvider;
				}
			}
			set
			{
				ThrowIfDisposed();
				Requires.NotNull(value, "value");
				ImportEngine importEngine = null;
				AggregateExportProvider aggregateExportProvider = null;
				bool flag = true;
				try
				{
					importEngine = new ImportEngine(value, _compositionOptions);
					value.ExportsChanging += OnExportsChangingInternal;
					using (_lock.LockStateForWrite())
					{
						EnsureCanSet(_sourceProvider);
						_sourceProvider = value;
						_importEngine = importEngine;
						flag = false;
					}
				}
				finally
				{
					if (flag)
					{
						value.ExportsChanging -= OnExportsChangingInternal;
						importEngine.Dispose();
						aggregateExportProvider?.Dispose();
					}
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> class with the specified catalog.</summary>
		/// <param name="catalog">The catalog that the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> uses to produce <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="catalog" /> is <see langword="null" />.</exception>
		public CatalogExportProvider(ComposablePartCatalog catalog)
			: this(catalog, CompositionOptions.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> class with the specified catalog and optional thread-safe mode.</summary>
		/// <param name="catalog">The catalog that the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> uses to produce <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</param>
		/// <param name="isThreadSafe">
		///   <see langword="true" /> if this object must be thread-safe; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="catalog" /> is <see langword="null" />.</exception>
		public CatalogExportProvider(ComposablePartCatalog catalog, bool isThreadSafe)
			: this(catalog, isThreadSafe ? CompositionOptions.IsThreadSafe : CompositionOptions.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> class with the specified catalog and composition options.</summary>
		/// <param name="catalog">The catalog that the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> uses to produce <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</param>
		/// <param name="compositionOptions">Options that determine the behavior of this provider.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="catalog" /> is <see langword="null" />.</exception>
		public CatalogExportProvider(ComposablePartCatalog catalog, CompositionOptions compositionOptions)
		{
			Requires.NotNull(catalog, "catalog");
			if (compositionOptions > (CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe | CompositionOptions.ExportCompositionService))
			{
				throw new ArgumentOutOfRangeException("compositionOptions");
			}
			_catalog = catalog;
			_compositionOptions = compositionOptions;
			if (_catalog is INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged)
			{
				notifyComposablePartCatalogChanged.Changing += OnCatalogChanging;
			}
			if (_catalog is CompositionScopeDefinition scopeDefinition)
			{
				_innerExportProvider = new AggregateExportProvider(new ScopeManager(this, scopeDefinition), new InnerCatalogExportProvider(InternalGetExportsCore));
			}
			else
			{
				_innerExportProvider = new InnerCatalogExportProvider(InternalGetExportsCore);
			}
			_lock = new CompositionLock(compositionOptions.HasFlag(CompositionOptions.IsThreadSafe));
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> class.</summary>
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing || _isDisposed)
			{
				return;
			}
			bool flag = false;
			INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged = null;
			HashSet<IDisposable> hashSet = null;
			ImportEngine importEngine = null;
			ExportProvider exportProvider = null;
			AggregateExportProvider aggregateExportProvider = null;
			try
			{
				using (_lock.LockStateForWrite())
				{
					if (!_isDisposed)
					{
						notifyComposablePartCatalogChanged = _catalog as INotifyComposablePartCatalogChanged;
						_catalog = null;
						aggregateExportProvider = _innerExportProvider as AggregateExportProvider;
						_innerExportProvider = null;
						exportProvider = _sourceProvider;
						_sourceProvider = null;
						importEngine = _importEngine;
						_importEngine = null;
						hashSet = _partsToDispose;
						_gcRoots = null;
						flag = true;
						_isDisposed = true;
					}
				}
			}
			finally
			{
				if (notifyComposablePartCatalogChanged != null)
				{
					notifyComposablePartCatalogChanged.Changing -= OnCatalogChanging;
				}
				aggregateExportProvider?.Dispose();
				if (exportProvider != null)
				{
					exportProvider.ExportsChanging -= OnExportsChangingInternal;
				}
				importEngine?.Dispose();
				if (hashSet != null)
				{
					foreach (IDisposable item in hashSet)
					{
						item.Dispose();
					}
				}
				if (flag)
				{
					_lock.Dispose();
				}
			}
		}

		/// <summary>Returns all exports that match the conditions of the specified import.</summary>
		/// <param name="definition">The conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects to be returned.</param>
		/// <param name="atomicComposition">The composition transaction to use, or <see langword="null" /> to disable transactional composition.</param>
		/// <returns>A collection that contains all the exports that match the specified condition.</returns>
		protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
		{
			ThrowIfDisposed();
			EnsureRunning();
			Assumes.NotNull(_innerExportProvider);
			_innerExportProvider.TryGetExports(definition, atomicComposition, out var exports);
			return exports;
		}

		private IEnumerable<Export> InternalGetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
		{
			ThrowIfDisposed();
			EnsureRunning();
			ComposablePartCatalog valueAllowNull = atomicComposition.GetValueAllowNull(_catalog);
			IPartCreatorImportDefinition partCreatorImportDefinition = definition as IPartCreatorImportDefinition;
			bool isExportFactory = false;
			if (partCreatorImportDefinition != null)
			{
				definition = partCreatorImportDefinition.ProductImportDefinition;
				isExportFactory = true;
			}
			CreationPolicy requiredCreationPolicy = definition.GetRequiredCreationPolicy();
			List<Export> list = new List<Export>();
			foreach (Tuple<ComposablePartDefinition, ExportDefinition> export in valueAllowNull.GetExports(definition))
			{
				if (!IsRejected(export.Item1, atomicComposition))
				{
					list.Add(CreateExport(export.Item1, export.Item2, isExportFactory, requiredCreationPolicy));
				}
			}
			return list;
		}

		private Export CreateExport(ComposablePartDefinition partDefinition, ExportDefinition exportDefinition, bool isExportFactory, CreationPolicy importPolicy)
		{
			if (isExportFactory)
			{
				return new PartCreatorExport(this, partDefinition, exportDefinition);
			}
			return CatalogExport.CreateExport(this, partDefinition, exportDefinition, importPolicy);
		}

		private void OnExportsChangingInternal(object sender, ExportsChangeEventArgs e)
		{
			UpdateRejections(e.AddedExports.Concat(e.RemovedExports), e.AtomicComposition);
		}

		private static ExportDefinition[] GetExportsFromPartDefinitions(IEnumerable<ComposablePartDefinition> partDefinitions)
		{
			List<ExportDefinition> list = new List<ExportDefinition>();
			foreach (ComposablePartDefinition partDefinition in partDefinitions)
			{
				foreach (ExportDefinition exportDefinition in partDefinition.ExportDefinitions)
				{
					list.Add(exportDefinition);
					list.Add(new PartCreatorExportDefinition(exportDefinition));
				}
			}
			return list.ToArray();
		}

		private void OnCatalogChanging(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			using AtomicComposition atomicComposition = new AtomicComposition(e.AtomicComposition);
			atomicComposition.SetValue(_catalog, new CatalogChangeProxy(_catalog, e.AddedDefinitions, e.RemovedDefinitions));
			IEnumerable<ExportDefinition> addedExports = GetExportsFromPartDefinitions(e.AddedDefinitions);
			IEnumerable<ExportDefinition> removedExports = GetExportsFromPartDefinitions(e.RemovedDefinitions);
			foreach (ComposablePartDefinition removedDefinition in e.RemovedDefinitions)
			{
				CatalogPart value = null;
				bool flag = false;
				using (_lock.LockStateForRead())
				{
					flag = _activatedParts.TryGetValue(removedDefinition, out value);
				}
				if (!flag)
				{
					continue;
				}
				ComposablePartDefinition capturedDefinition = removedDefinition;
				ReleasePart(null, value, atomicComposition);
				atomicComposition.AddCompleteActionAllowNull(delegate
				{
					using (_lock.LockStateForWrite())
					{
						_activatedParts.Remove(capturedDefinition);
					}
				});
			}
			UpdateRejections(addedExports.ConcatAllowingNull(removedExports), atomicComposition);
			OnExportsChanging(new ExportsChangeEventArgs(addedExports, removedExports, atomicComposition));
			atomicComposition.AddCompleteAction(delegate
			{
				OnExportsChanged(new ExportsChangeEventArgs(addedExports, removedExports, null));
			});
			atomicComposition.Complete();
		}

		private CatalogPart GetComposablePart(ComposablePartDefinition partDefinition, bool isSharedPart)
		{
			ThrowIfDisposed();
			EnsureRunning();
			CatalogPart catalogPart = null;
			if (isSharedPart)
			{
				catalogPart = GetSharedPart(partDefinition);
			}
			else
			{
				ComposablePart composablePart = partDefinition.CreatePart();
				catalogPart = new CatalogPart(composablePart);
				if (composablePart is IDisposable item)
				{
					using (_lock.LockStateForWrite())
					{
						_partsToDispose.Add(item);
					}
				}
			}
			return catalogPart;
		}

		private CatalogPart GetSharedPart(ComposablePartDefinition partDefinition)
		{
			CatalogPart value = null;
			using (_lock.LockStateForRead())
			{
				if (_activatedParts.TryGetValue(partDefinition, out value))
				{
					return value;
				}
			}
			ComposablePart composablePart = partDefinition.CreatePart();
			IDisposable disposable = composablePart as IDisposable;
			using (_lock.LockStateForWrite())
			{
				if (!_activatedParts.TryGetValue(partDefinition, out value))
				{
					value = new CatalogPart(composablePart);
					_activatedParts.Add(partDefinition, value);
					if (disposable != null)
					{
						_partsToDispose.Add(disposable);
					}
					composablePart = null;
					disposable = null;
				}
			}
			disposable?.Dispose();
			return value;
		}

		private object GetExportedValue(CatalogPart part, ExportDefinition export, bool isSharedPart)
		{
			ThrowIfDisposed();
			EnsureRunning();
			Assumes.NotNull(part, export);
			bool importsSatisfied = part.ImportsSatisfied;
			object exportedValueFromComposedPart = CompositionServices.GetExportedValueFromComposedPart(importsSatisfied ? null : _importEngine, part.Part, export);
			if (!importsSatisfied)
			{
				part.ImportsSatisfied = true;
			}
			if (exportedValueFromComposedPart != null && !isSharedPart && part.Part.IsRecomposable())
			{
				PreventPartCollection(exportedValueFromComposedPart, part.Part);
			}
			return exportedValueFromComposedPart;
		}

		private void ReleasePart(object exportedValue, CatalogPart catalogPart, AtomicComposition atomicComposition)
		{
			ThrowIfDisposed();
			EnsureRunning();
			Assumes.NotNull(catalogPart);
			_importEngine.ReleaseImports(catalogPart.Part, atomicComposition);
			if (exportedValue != null)
			{
				atomicComposition.AddCompleteActionAllowNull(delegate
				{
					AllowPartCollection(exportedValue);
				});
			}
			IDisposable diposablePart = catalogPart.Part as IDisposable;
			if (diposablePart == null)
			{
				return;
			}
			atomicComposition.AddCompleteActionAllowNull(delegate
			{
				bool flag = false;
				using (_lock.LockStateForWrite())
				{
					flag = _partsToDispose.Remove(diposablePart);
				}
				if (flag)
				{
					diposablePart.Dispose();
				}
			});
		}

		private void PreventPartCollection(object exportedValue, ComposablePart part)
		{
			Assumes.NotNull(exportedValue, part);
			using (_lock.LockStateForWrite())
			{
				ConditionalWeakTable<object, List<ComposablePart>> conditionalWeakTable = _gcRoots;
				if (conditionalWeakTable == null)
				{
					conditionalWeakTable = new ConditionalWeakTable<object, List<ComposablePart>>();
				}
				if (!conditionalWeakTable.TryGetValue(exportedValue, out var value))
				{
					value = new List<ComposablePart>();
					conditionalWeakTable.Add(exportedValue, value);
				}
				value.Add(part);
				if (_gcRoots == null)
				{
					Thread.MemoryBarrier();
					_gcRoots = conditionalWeakTable;
				}
			}
		}

		private void AllowPartCollection(object gcRoot)
		{
			if (_gcRoots != null)
			{
				using (_lock.LockStateForWrite())
				{
					_gcRoots.Remove(gcRoot);
				}
			}
		}

		private bool IsRejected(ComposablePartDefinition definition, AtomicComposition atomicComposition)
		{
			bool flag = false;
			if (atomicComposition != null)
			{
				AtomicCompositionQueryState atomicCompositionQueryState = GetAtomicCompositionQuery(atomicComposition)(definition);
				switch (atomicCompositionQueryState)
				{
				case AtomicCompositionQueryState.TreatAsRejected:
					return true;
				case AtomicCompositionQueryState.TreatAsValidated:
					return false;
				case AtomicCompositionQueryState.NeedsTesting:
					flag = true;
					break;
				default:
					Assumes.IsTrue(atomicCompositionQueryState == AtomicCompositionQueryState.Unknown);
					break;
				}
			}
			if (!flag)
			{
				using (_lock.LockStateForRead())
				{
					if (_activatedParts.ContainsKey(definition))
					{
						return false;
					}
					if (_rejectedParts.Contains(definition))
					{
						return true;
					}
				}
			}
			return DetermineRejection(definition, atomicComposition);
		}

		private bool DetermineRejection(ComposablePartDefinition definition, AtomicComposition parentAtomicComposition)
		{
			ChangeRejectedException exception = null;
			using (AtomicComposition atomicComposition = new AtomicComposition(parentAtomicComposition))
			{
				UpdateAtomicCompositionQuery(atomicComposition, (ComposablePartDefinition def) => definition.Equals(def), AtomicCompositionQueryState.TreatAsValidated);
				ComposablePart newPart = definition.CreatePart();
				try
				{
					_importEngine.PreviewImports(newPart, atomicComposition);
					atomicComposition.AddCompleteActionAllowNull(delegate
					{
						using (_lock.LockStateForWrite())
						{
							if (!_activatedParts.ContainsKey(definition))
							{
								_activatedParts.Add(definition, new CatalogPart(newPart));
								if (newPart is IDisposable item)
								{
									_partsToDispose.Add(item);
								}
							}
						}
					});
					atomicComposition.Complete();
					return false;
				}
				catch (ChangeRejectedException ex)
				{
					exception = ex;
				}
			}
			parentAtomicComposition.AddCompleteActionAllowNull(delegate
			{
				using (_lock.LockStateForWrite())
				{
					_rejectedParts.Add(definition);
				}
				CompositionTrace.PartDefinitionRejected(definition, exception);
			});
			if (parentAtomicComposition != null)
			{
				UpdateAtomicCompositionQuery(parentAtomicComposition, (ComposablePartDefinition def) => definition.Equals(def), AtomicCompositionQueryState.TreatAsRejected);
			}
			return true;
		}

		private void UpdateRejections(IEnumerable<ExportDefinition> changedExports, AtomicComposition atomicComposition)
		{
			using AtomicComposition atomicComposition2 = new AtomicComposition(atomicComposition);
			HashSet<ComposablePartDefinition> affectedRejections = new HashSet<ComposablePartDefinition>();
			Func<ComposablePartDefinition, AtomicCompositionQueryState> atomicCompositionQuery = GetAtomicCompositionQuery(atomicComposition2);
			ComposablePartDefinition[] array;
			using (_lock.LockStateForRead())
			{
				array = _rejectedParts.ToArray();
			}
			ComposablePartDefinition[] array2 = array;
			foreach (ComposablePartDefinition composablePartDefinition in array2)
			{
				if (atomicCompositionQuery(composablePartDefinition) == AtomicCompositionQueryState.TreatAsValidated)
				{
					continue;
				}
				foreach (ImportDefinition import in composablePartDefinition.ImportDefinitions.Where(ImportEngine.IsRequiredImportForPreview))
				{
					if (changedExports.Any((ExportDefinition export) => import.IsConstraintSatisfiedBy(export)))
					{
						affectedRejections.Add(composablePartDefinition);
						break;
					}
				}
			}
			UpdateAtomicCompositionQuery(atomicComposition2, (ComposablePartDefinition def) => affectedRejections.Contains(def), AtomicCompositionQueryState.NeedsTesting);
			List<ExportDefinition> resurrectedExports = new List<ExportDefinition>();
			foreach (ComposablePartDefinition item in affectedRejections)
			{
				if (IsRejected(item, atomicComposition2))
				{
					continue;
				}
				resurrectedExports.AddRange(item.ExportDefinitions);
				ComposablePartDefinition capturedPartDefinition = item;
				atomicComposition2.AddCompleteAction(delegate
				{
					using (_lock.LockStateForWrite())
					{
						_rejectedParts.Remove(capturedPartDefinition);
					}
					CompositionTrace.PartDefinitionResurrected(capturedPartDefinition);
				});
			}
			if (resurrectedExports.Any())
			{
				OnExportsChanging(new ExportsChangeEventArgs(resurrectedExports, new ExportDefinition[0], atomicComposition2));
				atomicComposition2.AddCompleteAction(delegate
				{
					OnExportsChanged(new ExportsChangeEventArgs(resurrectedExports, new ExportDefinition[0], null));
				});
			}
			atomicComposition2.Complete();
		}

		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}

		[DebuggerStepThrough]
		private void EnsureCanRun()
		{
			if (_sourceProvider == null || _importEngine == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ObjectMustBeInitialized, "SourceProvider"));
			}
		}

		[DebuggerStepThrough]
		private void EnsureRunning()
		{
			if (_isRunning)
			{
				return;
			}
			using (_lock.LockStateForWrite())
			{
				if (!_isRunning)
				{
					EnsureCanRun();
					_isRunning = true;
				}
			}
		}

		[DebuggerStepThrough]
		private void EnsureCanSet<T>(T currentValue) where T : class
		{
			if (_isRunning || currentValue != null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ObjectAlreadyInitialized));
			}
		}

		private Func<ComposablePartDefinition, AtomicCompositionQueryState> GetAtomicCompositionQuery(AtomicComposition atomicComposition)
		{
			atomicComposition.TryGetValue<Func<ComposablePartDefinition, AtomicCompositionQueryState>>(this, out var value);
			if (value == null)
			{
				return (ComposablePartDefinition definition) => AtomicCompositionQueryState.Unknown;
			}
			return value;
		}

		private void UpdateAtomicCompositionQuery(AtomicComposition atomicComposition, Func<ComposablePartDefinition, bool> query, AtomicCompositionQueryState state)
		{
			Func<ComposablePartDefinition, AtomicCompositionQueryState> parentQuery = GetAtomicCompositionQuery(atomicComposition);
			Func<ComposablePartDefinition, AtomicCompositionQueryState> value = (ComposablePartDefinition definition) => query(definition) ? state : parentQuery(definition);
			atomicComposition.SetValue(this, value);
		}
	}
	/// <summary>Provides extension methods for constructing composition services.</summary>
	public static class CatalogExtensions
	{
		/// <summary>Creates a new composition service by using the specified catalog as a source for exports.</summary>
		/// <param name="composablePartCatalog">The catalog that will provide exports.</param>
		/// <returns>A new composition service.</returns>
		public static CompositionService CreateCompositionService(this ComposablePartCatalog composablePartCatalog)
		{
			Requires.NotNull(composablePartCatalog, "composablePartCatalog");
			return new CompositionService(composablePartCatalog);
		}
	}
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Composition.Hosting.INotifyComposablePartCatalogChanged.Changed" /> event.</summary>
	public class ComposablePartCatalogChangeEventArgs : EventArgs
	{
		private readonly IEnumerable<ComposablePartDefinition> _addedDefinitions;

		private readonly IEnumerable<ComposablePartDefinition> _removedDefinitions;

		/// <summary>Gets a collection of definitions added to the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> in this change.</summary>
		/// <returns>A collection of definitions added to the catalog.</returns>
		public IEnumerable<ComposablePartDefinition> AddedDefinitions => _addedDefinitions;

		/// <summary>Gets a collection of definitions removed from the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> in this change.</summary>
		/// <returns>A collection of definitions removed from the catalog in this change.</returns>
		public IEnumerable<ComposablePartDefinition> RemovedDefinitions => _removedDefinitions;

		/// <summary>Gets the composition transaction for this change.</summary>
		/// <returns>The composition transaction for this change.</returns>
		public AtomicComposition AtomicComposition { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartCatalogChangeEventArgs" /> class with the specified changes.</summary>
		/// <param name="addedDefinitions">The part definitions that were added to the catalog.</param>
		/// <param name="removedDefinitions">The part definitions that were removed from the catalog.</param>
		/// <param name="atomicComposition">The composition transaction to use, or <see langword="null" /> to disable transactional composition.</param>
		public ComposablePartCatalogChangeEventArgs(IEnumerable<ComposablePartDefinition> addedDefinitions, IEnumerable<ComposablePartDefinition> removedDefinitions, AtomicComposition atomicComposition)
		{
			Requires.NotNull(addedDefinitions, "addedDefinitions");
			Requires.NotNull(removedDefinitions, "removedDefinitions");
			_addedDefinitions = addedDefinitions.AsArray();
			_removedDefinitions = removedDefinitions.AsArray();
			AtomicComposition = atomicComposition;
		}
	}
	internal class ComposablePartCatalogCollection : ICollection<ComposablePartCatalog>, IEnumerable<ComposablePartCatalog>, IEnumerable, INotifyComposablePartCatalogChanged, IDisposable
	{
		private readonly Microsoft.Internal.Lock _lock = new Microsoft.Internal.Lock();

		private Action<ComposablePartCatalogChangeEventArgs> _onChanged;

		private Action<ComposablePartCatalogChangeEventArgs> _onChanging;

		private List<ComposablePartCatalog> _catalogs = new List<ComposablePartCatalog>();

		private volatile bool _isCopyNeeded;

		private volatile bool _isDisposed;

		private bool _hasChanged;

		public int Count
		{
			get
			{
				ThrowIfDisposed();
				using (new ReadLock(_lock))
				{
					return _catalogs.Count;
				}
			}
		}

		public bool IsReadOnly
		{
			get
			{
				ThrowIfDisposed();
				return false;
			}
		}

		internal bool HasChanged
		{
			get
			{
				ThrowIfDisposed();
				using (new ReadLock(_lock))
				{
					return _hasChanged;
				}
			}
		}

		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed;

		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing;

		public ComposablePartCatalogCollection(IEnumerable<ComposablePartCatalog> catalogs, Action<ComposablePartCatalogChangeEventArgs> onChanged, Action<ComposablePartCatalogChangeEventArgs> onChanging)
		{
			catalogs = catalogs ?? Enumerable.Empty<ComposablePartCatalog>();
			_catalogs = new List<ComposablePartCatalog>(catalogs);
			_onChanged = onChanged;
			_onChanging = onChanging;
			SubscribeToCatalogNotifications(catalogs);
		}

		public void Add(ComposablePartCatalog item)
		{
			Requires.NotNull(item, "item");
			ThrowIfDisposed();
			Lazy<IEnumerable<ComposablePartDefinition>> addedDefinitions = new Lazy<IEnumerable<ComposablePartDefinition>>(() => item.ToArray(), LazyThreadSafetyMode.PublicationOnly);
			using (AtomicComposition atomicComposition = new AtomicComposition())
			{
				RaiseChangingEvent(addedDefinitions, null, atomicComposition);
				using (new WriteLock(_lock))
				{
					if (_isCopyNeeded)
					{
						_catalogs = new List<ComposablePartCatalog>(_catalogs);
						_isCopyNeeded = false;
					}
					_hasChanged = true;
					_catalogs.Add(item);
				}
				SubscribeToCatalogNotifications(item);
				atomicComposition.Complete();
			}
			RaiseChangedEvent(addedDefinitions, null);
		}

		public void Clear()
		{
			ThrowIfDisposed();
			ComposablePartCatalog[] catalogs = null;
			using (new ReadLock(_lock))
			{
				if (_catalogs.Count == 0)
				{
					return;
				}
				catalogs = _catalogs.ToArray();
			}
			Lazy<IEnumerable<ComposablePartDefinition>> removedDefinitions = new Lazy<IEnumerable<ComposablePartDefinition>>(() => catalogs.SelectMany((ComposablePartCatalog catalog) => catalog).ToArray(), LazyThreadSafetyMode.PublicationOnly);
			using (AtomicComposition atomicComposition = new AtomicComposition())
			{
				RaiseChangingEvent(null, removedDefinitions, atomicComposition);
				UnsubscribeFromCatalogNotifications(catalogs);
				using (new WriteLock(_lock))
				{
					_catalogs = new List<ComposablePartCatalog>();
					_isCopyNeeded = false;
					_hasChanged = true;
				}
				atomicComposition.Complete();
			}
			RaiseChangedEvent(null, removedDefinitions);
		}

		public bool Contains(ComposablePartCatalog item)
		{
			Requires.NotNull(item, "item");
			ThrowIfDisposed();
			using (new ReadLock(_lock))
			{
				return _catalogs.Contains(item);
			}
		}

		public void CopyTo(ComposablePartCatalog[] array, int arrayIndex)
		{
			ThrowIfDisposed();
			using (new ReadLock(_lock))
			{
				_catalogs.CopyTo(array, arrayIndex);
			}
		}

		public bool Remove(ComposablePartCatalog item)
		{
			Requires.NotNull(item, "item");
			ThrowIfDisposed();
			using (new ReadLock(_lock))
			{
				if (!_catalogs.Contains(item))
				{
					return false;
				}
			}
			bool flag = false;
			Lazy<IEnumerable<ComposablePartDefinition>> removedDefinitions = new Lazy<IEnumerable<ComposablePartDefinition>>(() => item.ToArray(), LazyThreadSafetyMode.PublicationOnly);
			using (AtomicComposition atomicComposition = new AtomicComposition())
			{
				RaiseChangingEvent(null, removedDefinitions, atomicComposition);
				using (new WriteLock(_lock))
				{
					if (_isCopyNeeded)
					{
						_catalogs = new List<ComposablePartCatalog>(_catalogs);
						_isCopyNeeded = false;
					}
					flag = _catalogs.Remove(item);
					if (flag)
					{
						_hasChanged = true;
					}
				}
				UnsubscribeFromCatalogNotifications(item);
				atomicComposition.Complete();
			}
			RaiseChangedEvent(null, removedDefinitions);
			return flag;
		}

		public IEnumerator<ComposablePartCatalog> GetEnumerator()
		{
			ThrowIfDisposed();
			using (new WriteLock(_lock))
			{
				object result = _catalogs.GetEnumerator();
				_isCopyNeeded = true;
				return (IEnumerator<ComposablePartCatalog>)result;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing || _isDisposed)
			{
				return;
			}
			bool flag = false;
			IEnumerable<ComposablePartCatalog> enumerable = null;
			try
			{
				using (new WriteLock(_lock))
				{
					if (!_isDisposed)
					{
						flag = true;
						enumerable = _catalogs;
						_catalogs = null;
						_isDisposed = true;
					}
				}
			}
			finally
			{
				if (enumerable != null)
				{
					UnsubscribeFromCatalogNotifications(enumerable);
					enumerable.ForEach(delegate(ComposablePartCatalog catalog)
					{
						catalog.Dispose();
					});
				}
				if (flag)
				{
					_lock.Dispose();
				}
			}
		}

		private void RaiseChangedEvent(Lazy<IEnumerable<ComposablePartDefinition>> addedDefinitions, Lazy<IEnumerable<ComposablePartDefinition>> removedDefinitions)
		{
			if (_onChanged != null && this.Changed != null)
			{
				IEnumerable<ComposablePartDefinition> addedDefinitions2 = ((addedDefinitions == null) ? Enumerable.Empty<ComposablePartDefinition>() : addedDefinitions.Value);
				IEnumerable<ComposablePartDefinition> removedDefinitions2 = ((removedDefinitions == null) ? Enumerable.Empty<ComposablePartDefinition>() : removedDefinitions.Value);
				_onChanged(new ComposablePartCatalogChangeEventArgs(addedDefinitions2, removedDefinitions2, null));
			}
		}

		public void OnChanged(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			this.Changed?.Invoke(sender, e);
		}

		private void RaiseChangingEvent(Lazy<IEnumerable<ComposablePartDefinition>> addedDefinitions, Lazy<IEnumerable<ComposablePartDefinition>> removedDefinitions, AtomicComposition atomicComposition)
		{
			if (_onChanging != null && this.Changing != null)
			{
				IEnumerable<ComposablePartDefinition> addedDefinitions2 = ((addedDefinitions == null) ? Enumerable.Empty<ComposablePartDefinition>() : addedDefinitions.Value);
				IEnumerable<ComposablePartDefinition> removedDefinitions2 = ((removedDefinitions == null) ? Enumerable.Empty<ComposablePartDefinition>() : removedDefinitions.Value);
				_onChanging(new ComposablePartCatalogChangeEventArgs(addedDefinitions2, removedDefinitions2, atomicComposition));
			}
		}

		public void OnChanging(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			this.Changing?.Invoke(sender, e);
		}

		private void OnContainedCatalogChanged(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			if (_onChanged != null && this.Changed != null)
			{
				_onChanged(e);
			}
		}

		private void OnContainedCatalogChanging(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			if (_onChanging != null && this.Changing != null)
			{
				_onChanging(e);
			}
		}

		private void SubscribeToCatalogNotifications(ComposablePartCatalog catalog)
		{
			if (catalog is INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged)
			{
				notifyComposablePartCatalogChanged.Changed += OnContainedCatalogChanged;
				notifyComposablePartCatalogChanged.Changing += OnContainedCatalogChanging;
			}
		}

		private void SubscribeToCatalogNotifications(IEnumerable<ComposablePartCatalog> catalogs)
		{
			foreach (ComposablePartCatalog catalog in catalogs)
			{
				SubscribeToCatalogNotifications(catalog);
			}
		}

		private void UnsubscribeFromCatalogNotifications(ComposablePartCatalog catalog)
		{
			if (catalog is INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged)
			{
				notifyComposablePartCatalogChanged.Changed -= OnContainedCatalogChanged;
				notifyComposablePartCatalogChanged.Changing -= OnContainedCatalogChanging;
			}
		}

		private void UnsubscribeFromCatalogNotifications(IEnumerable<ComposablePartCatalog> catalogs)
		{
			foreach (ComposablePartCatalog catalog in catalogs)
			{
				UnsubscribeFromCatalogNotifications(catalog);
			}
		}

		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}
	}
	/// <summary>Retrieves exports from a part.</summary>
	public class ComposablePartExportProvider : ExportProvider, IDisposable
	{
		private List<ComposablePart> _parts = new List<ComposablePart>();

		private volatile bool _isDisposed;

		private volatile bool _isRunning;

		private CompositionLock _lock;

		private ExportProvider _sourceProvider;

		private ImportEngine _importEngine;

		private volatile bool _currentlyComposing;

		private CompositionOptions _compositionOptions;

		/// <summary>Gets or sets the export provider that provides access to additional <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</summary>
		/// <returns>A provider that provides the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> access to <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.  
		///  The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This property has already been set.  
		///  -or-  
		///  The methods on the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> have already been accessed.</exception>
		public ExportProvider SourceProvider
		{
			get
			{
				ThrowIfDisposed();
				return _sourceProvider;
			}
			set
			{
				ThrowIfDisposed();
				Requires.NotNull(value, "value");
				using (_lock.LockStateForWrite())
				{
					EnsureCanSet(_sourceProvider);
					_sourceProvider = value;
				}
			}
		}

		private ImportEngine ImportEngine
		{
			get
			{
				if (_importEngine == null)
				{
					Assumes.NotNull(_sourceProvider);
					ImportEngine importEngine = new ImportEngine(_sourceProvider, _compositionOptions);
					using (_lock.LockStateForWrite())
					{
						if (_importEngine == null)
						{
							Thread.MemoryBarrier();
							_importEngine = importEngine;
							importEngine = null;
						}
					}
					importEngine?.Dispose();
				}
				return _importEngine;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> class.</summary>
		public ComposablePartExportProvider()
			: this(isThreadSafe: false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> class, optionally in thread-safe mode.</summary>
		/// <param name="isThreadSafe">
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> object must be thread-safe; otherwise, <see langword="false" />.</param>
		public ComposablePartExportProvider(bool isThreadSafe)
			: this(isThreadSafe ? CompositionOptions.IsThreadSafe : CompositionOptions.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> class with the specified composition options.</summary>
		/// <param name="compositionOptions">Options that specify the behavior of this provider.</param>
		public ComposablePartExportProvider(CompositionOptions compositionOptions)
		{
			if (compositionOptions > (CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe | CompositionOptions.ExportCompositionService))
			{
				throw new ArgumentOutOfRangeException("compositionOptions");
			}
			_compositionOptions = compositionOptions;
			_lock = new CompositionLock(compositionOptions.HasFlag(CompositionOptions.IsThreadSafe));
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> class.</summary>
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing || _isDisposed)
			{
				return;
			}
			bool flag = false;
			ImportEngine importEngine = null;
			try
			{
				using (_lock.LockStateForWrite())
				{
					if (!_isDisposed)
					{
						importEngine = _importEngine;
						_importEngine = null;
						_sourceProvider = null;
						_isDisposed = true;
						flag = true;
					}
				}
			}
			finally
			{
				importEngine?.Dispose();
				if (flag)
				{
					_lock.Dispose();
				}
			}
		}

		/// <summary>Gets a collection of all exports in this provider that match the conditions of the specified import.</summary>
		/// <param name="definition">The <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> that defines the conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> to get.</param>
		/// <param name="atomicComposition">The composition transaction to use, or <see langword="null" /> to disable transactional composition.</param>
		/// <returns>A collection of all exports in this provider that match the specified conditions.</returns>
		protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
		{
			ThrowIfDisposed();
			EnsureRunning();
			List<ComposablePart> list = null;
			using (_lock.LockStateForRead())
			{
				list = atomicComposition.GetValueAllowNull(this, _parts);
			}
			if (list.Count == 0)
			{
				return null;
			}
			List<Export> list2 = new List<Export>();
			foreach (ComposablePart item in list)
			{
				foreach (ExportDefinition exportDefinition in item.ExportDefinitions)
				{
					if (definition.IsConstraintSatisfiedBy(exportDefinition))
					{
						list2.Add(CreateExport(item, exportDefinition));
					}
				}
			}
			return list2;
		}

		/// <summary>Executes composition on the specified batch.</summary>
		/// <param name="batch">The batch to execute composition on.</param>
		/// <exception cref="T:System.InvalidOperationException">The container is already in the process of composing.</exception>
		public void Compose(CompositionBatch batch)
		{
			ThrowIfDisposed();
			EnsureRunning();
			Requires.NotNull(batch, "batch");
			if (batch.PartsToAdd.Count == 0 && batch.PartsToRemove.Count == 0)
			{
				return;
			}
			CompositionResult compositionResult = CompositionResult.SucceededResult;
			List<ComposablePart> updatedPartsList = GetUpdatedPartsList(ref batch);
			using (AtomicComposition atomicComposition = new AtomicComposition())
			{
				if (_currentlyComposing)
				{
					throw new InvalidOperationException(Strings.ReentrantCompose);
				}
				_currentlyComposing = true;
				try
				{
					atomicComposition.SetValue(this, updatedPartsList);
					Recompose(batch, atomicComposition);
					foreach (ComposablePart item in batch.PartsToAdd)
					{
						try
						{
							ImportEngine.PreviewImports(item, atomicComposition);
						}
						catch (ChangeRejectedException ex)
						{
							compositionResult = compositionResult.MergeResult(new CompositionResult(ex.Errors));
						}
					}
					compositionResult.ThrowOnErrors(atomicComposition);
					using (_lock.LockStateForWrite())
					{
						_parts = updatedPartsList;
					}
					atomicComposition.Complete();
				}
				finally
				{
					_currentlyComposing = false;
				}
			}
			foreach (ComposablePart part in batch.PartsToAdd)
			{
				compositionResult = compositionResult.MergeResult(CompositionServices.TryInvoke(delegate
				{
					ImportEngine.SatisfyImports(part);
				}));
			}
			compositionResult.ThrowOnErrors();
		}

		private List<ComposablePart> GetUpdatedPartsList(ref CompositionBatch batch)
		{
			Assumes.NotNull(batch);
			List<ComposablePart> list = null;
			using (_lock.LockStateForRead())
			{
				list = _parts.ToList();
			}
			foreach (ComposablePart item in batch.PartsToAdd)
			{
				list.Add(item);
			}
			List<ComposablePart> list2 = null;
			foreach (ComposablePart item2 in batch.PartsToRemove)
			{
				if (list.Remove(item2))
				{
					if (list2 == null)
					{
						list2 = new List<ComposablePart>();
					}
					list2.Add(item2);
				}
			}
			batch = new CompositionBatch(batch.PartsToAdd, list2);
			return list;
		}

		private void Recompose(CompositionBatch batch, AtomicComposition atomicComposition)
		{
			Assumes.NotNull(batch);
			foreach (ComposablePart item in batch.PartsToRemove)
			{
				ImportEngine.ReleaseImports(item, atomicComposition);
			}
			IEnumerable<ExportDefinition> addedExports = ((batch.PartsToAdd.Count != 0) ? batch.PartsToAdd.SelectMany((ComposablePart part) => part.ExportDefinitions).ToArray() : new ExportDefinition[0]);
			IEnumerable<ExportDefinition> removedExports = ((batch.PartsToRemove.Count != 0) ? batch.PartsToRemove.SelectMany((ComposablePart part) => part.ExportDefinitions).ToArray() : new ExportDefinition[0]);
			OnExportsChanging(new ExportsChangeEventArgs(addedExports, removedExports, atomicComposition));
			atomicComposition.AddCompleteAction(delegate
			{
				OnExportsChanged(new ExportsChangeEventArgs(addedExports, removedExports, null));
			});
		}

		private Export CreateExport(ComposablePart part, ExportDefinition export)
		{
			return new Export(export, () => GetExportedValue(part, export));
		}

		private object GetExportedValue(ComposablePart part, ExportDefinition export)
		{
			ThrowIfDisposed();
			EnsureRunning();
			return CompositionServices.GetExportedValueFromComposedPart(ImportEngine, part, export);
		}

		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw new ObjectDisposedException(GetType().Name);
			}
		}

		[DebuggerStepThrough]
		private void EnsureCanRun()
		{
			if (_sourceProvider == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ObjectMustBeInitialized, "SourceProvider"));
			}
		}

		[DebuggerStepThrough]
		private void EnsureRunning()
		{
			if (_isRunning)
			{
				return;
			}
			using (_lock.LockStateForWrite())
			{
				if (!_isRunning)
				{
					EnsureCanRun();
					_isRunning = true;
				}
			}
		}

		[DebuggerStepThrough]
		private void EnsureCanSet<T>(T currentValue) where T : class
		{
			if (_isRunning || currentValue != null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ObjectAlreadyInitialized));
			}
		}
	}
	/// <summary>Represents a set of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> objects which will be added or removed from the container in a single transactional composition.</summary>
	public class CompositionBatch
	{
		private class SingleExportComposablePart : ComposablePart
		{
			private readonly Export _export;

			public override IDictionary<string, object> Metadata => MetadataServices.EmptyMetadata;

			public override IEnumerable<ExportDefinition> ExportDefinitions => new ExportDefinition[1] { _export.Definition };

			public override IEnumerable<ImportDefinition> ImportDefinitions => Enumerable.Empty<ImportDefinition>();

			public SingleExportComposablePart(Export export)
			{
				Assumes.NotNull(export);
				_export = export;
			}

			public override object GetExportedValue(ExportDefinition definition)
			{
				Requires.NotNull(definition, "definition");
				if (definition != _export.Definition)
				{
					throw ExceptionBuilder.CreateExportDefinitionNotOnThisComposablePart("definition");
				}
				return _export.Value;
			}

			public override void SetImport(ImportDefinition definition, IEnumerable<Export> exports)
			{
				Requires.NotNull(definition, "definition");
				Requires.NotNullOrNullElements(exports, "exports");
				throw ExceptionBuilder.CreateImportDefinitionNotOnThisComposablePart("definition");
			}
		}

		private object _lock = new object();

		private bool _copyNeededForAdd;

		private bool _copyNeededForRemove;

		private List<ComposablePart> _partsToAdd;

		private ReadOnlyCollection<ComposablePart> _readOnlyPartsToAdd;

		private List<ComposablePart> _partsToRemove;

		private ReadOnlyCollection<ComposablePart> _readOnlyPartsToRemove;

		/// <summary>Gets the collection of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> objects to be added.</summary>
		/// <returns>A collection of parts to be added.</returns>
		public ReadOnlyCollection<ComposablePart> PartsToAdd
		{
			get
			{
				lock (_lock)
				{
					_copyNeededForAdd = true;
					return _readOnlyPartsToAdd;
				}
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> objects to be removed.</summary>
		/// <returns>A collection of parts to be removed.</returns>
		public ReadOnlyCollection<ComposablePart> PartsToRemove
		{
			get
			{
				lock (_lock)
				{
					_copyNeededForRemove = true;
					return _readOnlyPartsToRemove;
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionBatch" /> class.</summary>
		public CompositionBatch()
			: this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionBatch" /> class with the specified parts for addition and removal.</summary>
		/// <param name="partsToAdd">A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> objects to add.</param>
		/// <param name="partsToRemove">A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> objects to remove.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="partsToAdd" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="partsToRemove" /> is <see langword="null" />.</exception>
		public CompositionBatch(IEnumerable<ComposablePart> partsToAdd, IEnumerable<ComposablePart> partsToRemove)
		{
			_partsToAdd = new List<ComposablePart>();
			if (partsToAdd != null)
			{
				foreach (ComposablePart item in partsToAdd)
				{
					if (item == null)
					{
						throw ExceptionBuilder.CreateContainsNullElement("partsToAdd");
					}
					_partsToAdd.Add(item);
				}
			}
			_readOnlyPartsToAdd = _partsToAdd.AsReadOnly();
			_partsToRemove = new List<ComposablePart>();
			if (partsToRemove != null)
			{
				foreach (ComposablePart item2 in partsToRemove)
				{
					if (item2 == null)
					{
						throw ExceptionBuilder.CreateContainsNullElement("partsToRemove");
					}
					_partsToRemove.Add(item2);
				}
			}
			_readOnlyPartsToRemove = _partsToRemove.AsReadOnly();
		}

		/// <summary>Adds the specified part to the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionBatch" /> object.</summary>
		/// <param name="part">The part to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="part" /> is <see langword="null" />.</exception>
		public void AddPart(ComposablePart part)
		{
			Requires.NotNull(part, "part");
			lock (_lock)
			{
				if (_copyNeededForAdd)
				{
					_partsToAdd = new List<ComposablePart>(_partsToAdd);
					_readOnlyPartsToAdd = _partsToAdd.AsReadOnly();
					_copyNeededForAdd = false;
				}
				_partsToAdd.Add(part);
			}
		}

		/// <summary>Puts the specified part on the list of parts to remove.</summary>
		/// <param name="part">The part to be removed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="part" /> is <see langword="null" />.</exception>
		public void RemovePart(ComposablePart part)
		{
			Requires.NotNull(part, "part");
			lock (_lock)
			{
				if (_copyNeededForRemove)
				{
					_partsToRemove = new List<ComposablePart>(_partsToRemove);
					_readOnlyPartsToRemove = _partsToRemove.AsReadOnly();
					_copyNeededForRemove = false;
				}
				_partsToRemove.Add(part);
			}
		}

		/// <summary>Adds the specified export to the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionBatch" /> object.</summary>
		/// <param name="export">The export to add to the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionBatch" /> object.</param>
		/// <returns>The part added.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="export" /> is <see langword="null" />.</exception>
		public ComposablePart AddExport(Export export)
		{
			Requires.NotNull(export, "export");
			ComposablePart composablePart = new SingleExportComposablePart(export);
			AddPart(composablePart);
			return composablePart;
		}
	}
	/// <summary>Contains static metadata keys used by the composition system.</summary>
	public static class CompositionConstants
	{
		private const string CompositionNamespace = "System.ComponentModel.Composition";

		/// <summary>Specifies the metadata key created by the composition system to mark a part with a creation policy.</summary>
		public const string PartCreationPolicyMetadataName = "System.ComponentModel.Composition.CreationPolicy";

		/// <summary>Specifies the metadata key created by the composition system to mark an import source.</summary>
		public const string ImportSourceMetadataName = "System.ComponentModel.Composition.ImportSource";

		/// <summary>Specifies the metadata key created by the composition system to mark an <see langword="IsGenericPart" /> method.</summary>
		public const string IsGenericPartMetadataName = "System.ComponentModel.Composition.IsGenericPart";

		/// <summary>Specifies the metadata key created by the composition system to mark a generic contract.</summary>
		public const string GenericContractMetadataName = "System.ComponentModel.Composition.GenericContractName";

		/// <summary>Specifies the metadata key created by the composition system to mark generic parameters.</summary>
		public const string GenericParametersMetadataName = "System.ComponentModel.Composition.GenericParameters";

		/// <summary>Specifies the metadata key created by the composition system to mark a part with a unique identifier.</summary>
		public const string ExportTypeIdentityMetadataName = "ExportTypeIdentity";

		internal const string GenericImportParametersOrderMetadataName = "System.ComponentModel.Composition.GenericImportParametersOrderMetadataName";

		internal const string GenericExportParametersOrderMetadataName = "System.ComponentModel.Composition.GenericExportParametersOrderMetadataName";

		internal const string GenericPartArityMetadataName = "System.ComponentModel.Composition.GenericPartArity";

		internal const string GenericParameterConstraintsMetadataName = "System.ComponentModel.Composition.GenericParameterConstraints";

		internal const string GenericParameterAttributesMetadataName = "System.ComponentModel.Composition.GenericParameterAttributes";

		internal const string ProductDefinitionMetadataName = "ProductDefinition";

		internal const string PartCreatorContractName = "System.ComponentModel.Composition.Contracts.ExportFactory";

		internal static readonly string PartCreatorTypeIdentity = AttributedModelServices.GetTypeIdentity(typeof(ComposablePartDefinition));
	}
	/// <summary>Manages the composition of parts.</summary>
	public class CompositionContainer : ExportProvider, ICompositionService, IDisposable
	{
		private class CompositionServiceShim : ICompositionService
		{
			private CompositionContainer _innerContainer;

			public CompositionServiceShim(CompositionContainer innerContainer)
			{
				Assumes.NotNull(innerContainer);
				_innerContainer = innerContainer;
			}

			void ICompositionService.SatisfyImportsOnce(ComposablePart part)
			{
				_innerContainer.SatisfyImportsOnce(part);
			}
		}

		private CompositionOptions _compositionOptions;

		private ImportEngine _importEngine;

		private ComposablePartExportProvider _partExportProvider;

		private ExportProvider _rootProvider;

		private CatalogExportProvider _catalogExportProvider;

		private AggregateExportProvider _localExportProvider;

		private AggregateExportProvider _ancestorExportProvider;

		private readonly ReadOnlyCollection<ExportProvider> _providers;

		private volatile bool _isDisposed;

		private object _lock = new object();

		private static ReadOnlyCollection<ExportProvider> EmptyProviders = new ReadOnlyCollection<ExportProvider>(new ExportProvider[0]);

		internal CompositionOptions CompositionOptions
		{
			get
			{
				ThrowIfDisposed();
				return _compositionOptions;
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> that provides the container access to <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</summary>
		/// <returns>The catalog that provides the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> access to exports produced from <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> objects. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		public ComposablePartCatalog Catalog
		{
			get
			{
				ThrowIfDisposed();
				if (_catalogExportProvider == null)
				{
					return null;
				}
				return _catalogExportProvider.Catalog;
			}
		}

		internal CatalogExportProvider CatalogExportProvider
		{
			get
			{
				ThrowIfDisposed();
				return _catalogExportProvider;
			}
		}

		/// <summary>Gets the export providers that provide the container access to additional <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> objects.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> objects that provide the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> access to additional <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects. The default is an empty <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> has been disposed of.</exception>
		public ReadOnlyCollection<ExportProvider> Providers
		{
			get
			{
				ThrowIfDisposed();
				return _providers;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> class.</summary>
		public CompositionContainer()
			: this(null, Array.Empty<ExportProvider>())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> class with the specified export providers.</summary>
		/// <param name="providers">An array of <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> objects that provide the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> access to <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects, or <see langword="null" /> to set <see cref="P:System.ComponentModel.Composition.Hosting.CompositionContainer.Providers" /> to an empty <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="providers" /> contains an element that is <see langword="null" />.</exception>
		public CompositionContainer(params ExportProvider[] providers)
			: this(null, providers)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> class with the specified export providers and options.</summary>
		/// <param name="compositionOptions">An object that specifies the behavior of this container.</param>
		/// <param name="providers">An array of <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> objects that provide the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> access to <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects, or <see langword="null" /> to set <see cref="P:System.ComponentModel.Composition.Hosting.CompositionContainer.Providers" /> to an empty <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="providers" /> contains an element that is <see langword="null" />.</exception>
		public CompositionContainer(CompositionOptions compositionOptions, params ExportProvider[] providers)
			: this(null, compositionOptions, providers)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> class with the specified catalog and export providers.</summary>
		/// <param name="catalog">A catalog that provides <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects to the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.</param>
		/// <param name="providers">An array of <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> objects that provide the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> access to <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects, or <see langword="null" /> to set <see cref="P:System.ComponentModel.Composition.Hosting.CompositionContainer.Providers" /> to an empty <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="providers" /> contains an element that is <see langword="null" />.</exception>
		public CompositionContainer(ComposablePartCatalog catalog, params ExportProvider[] providers)
			: this(catalog, isThreadSafe: false, providers)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> class with the specified catalog, thread-safe mode, and export providers.</summary>
		/// <param name="catalog">A catalog that provides <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects to the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.</param>
		/// <param name="isThreadSafe">
		///   <see langword="true" /> if this <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object must be thread-safe; otherwise, <see langword="false" />.</param>
		/// <param name="providers">An array of <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> objects that provide the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> access to <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects, or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.Hosting.CompositionContainer.Providers" /> property to an empty <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.</param>
		/// <exception cref="T:System.ArgumentException">One or more elements of <paramref name="providers" /> are <see langword="null" />.</exception>
		public CompositionContainer(ComposablePartCatalog catalog, bool isThreadSafe, params ExportProvider[] providers)
			: this(catalog, isThreadSafe ? CompositionOptions.IsThreadSafe : CompositionOptions.Default, providers)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> class with the specified catalog, options, and export providers.</summary>
		/// <param name="catalog">A catalog that provides <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects to the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.</param>
		/// <param name="compositionOptions">An object that specifies options that affect the behavior of the container.</param>
		/// <param name="providers">An array of <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> objects that provide the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> access to <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects, or <see langword="null" /> to set <see cref="P:System.ComponentModel.Composition.Hosting.CompositionContainer.Providers" /> to an empty <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="providers" /> contains an element that is <see langword="null" />.</exception>
		public CompositionContainer(ComposablePartCatalog catalog, CompositionOptions compositionOptions, params ExportProvider[] providers)
		{
			if (compositionOptions > (CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe | CompositionOptions.ExportCompositionService))
			{
				throw new ArgumentOutOfRangeException("compositionOptions");
			}
			_compositionOptions = compositionOptions;
			_partExportProvider = new ComposablePartExportProvider(compositionOptions);
			_partExportProvider.SourceProvider = this;
			if (catalog != null || providers.Length != 0)
			{
				if (catalog != null)
				{
					_catalogExportProvider = new CatalogExportProvider(catalog, compositionOptions);
					_catalogExportProvider.SourceProvider = this;
					_localExportProvider = new AggregateExportProvider(_partExportProvider, _catalogExportProvider);
				}
				else
				{
					_localExportProvider = new AggregateExportProvider(_partExportProvider);
				}
				if (providers != null && providers.Length != 0)
				{
					_ancestorExportProvider = new AggregateExportProvider(providers);
					_rootProvider = new AggregateExportProvider(_localExportProvider, _ancestorExportProvider);
				}
				else
				{
					_rootProvider = _localExportProvider;
				}
			}
			else
			{
				_rootProvider = _partExportProvider;
			}
			if (compositionOptions.HasFlag(CompositionOptions.ExportCompositionService))
			{
				this.ComposeExportedValue((ICompositionService)new CompositionServiceShim(this));
			}
			_rootProvider.ExportsChanged += OnExportsChangedInternal;
			_rootProvider.ExportsChanging += OnExportsChangingInternal;
			_providers = ((providers != null) ? new ReadOnlyCollection<ExportProvider>((ExportProvider[])providers.Clone()) : EmptyProviders);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> class.</summary>
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing || _isDisposed)
			{
				return;
			}
			ExportProvider exportProvider = null;
			AggregateExportProvider aggregateExportProvider = null;
			AggregateExportProvider aggregateExportProvider2 = null;
			ComposablePartExportProvider composablePartExportProvider = null;
			CatalogExportProvider catalogExportProvider = null;
			ImportEngine importEngine = null;
			lock (_lock)
			{
				if (!_isDisposed)
				{
					exportProvider = _rootProvider;
					_rootProvider = null;
					aggregateExportProvider2 = _localExportProvider;
					_localExportProvider = null;
					aggregateExportProvider = _ancestorExportProvider;
					_ancestorExportProvider = null;
					composablePartExportProvider = _partExportProvider;
					_partExportProvider = null;
					catalogExportProvider = _catalogExportProvider;
					_catalogExportProvider = null;
					importEngine = _importEngine;
					_importEngine = null;
					_isDisposed = true;
				}
			}
			if (exportProvider != null)
			{
				exportProvider.ExportsChanged -= OnExportsChangedInternal;
				exportProvider.ExportsChanging -= OnExportsChangingInternal;
			}
			aggregateExportProvider?.Dispose();
			aggregateExportProvider2?.Dispose();
			catalogExportProvider?.Dispose();
			composablePartExportProvider?.Dispose();
			importEngine?.Dispose();
		}

		/// <summary>Adds or removes the parts in the specified <see cref="T:System.ComponentModel.Composition.Hosting.CompositionBatch" /> from the container and executes composition.</summary>
		/// <param name="batch">Changes to the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> to include during the composition.</param>
		public void Compose(CompositionBatch batch)
		{
			Requires.NotNull(batch, "batch");
			ThrowIfDisposed();
			_partExportProvider.Compose(batch);
		}

		/// <summary>Releases the specified <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object from the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.</summary>
		/// <param name="export">The <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> that needs to be released.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="export" /> is <see langword="null" />.</exception>
		public void ReleaseExport(Export export)
		{
			Requires.NotNull(export, "export");
			if (export is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}

		/// <summary>Removes the specified export from composition and releases its resources if possible.</summary>
		/// <param name="export">An indirect reference to the export to remove.</param>
		/// <typeparam name="T">The type of the export.</typeparam>
		public void ReleaseExport<T>(Lazy<T> export)
		{
			Requires.NotNull(export, "export");
			if (export is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}

		/// <summary>Releases a set of <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects from the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.</summary>
		/// <param name="exports">A collection of <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects to be released.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="exports" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="exports" /> contains an element that is <see langword="null" />.</exception>
		public void ReleaseExports(IEnumerable<Export> exports)
		{
			Requires.NotNullOrNullElements(exports, "exports");
			foreach (Export export in exports)
			{
				ReleaseExport(export);
			}
		}

		/// <summary>Removes a collection of exports from composition and releases their resources if possible.</summary>
		/// <param name="exports">A collection of indirect references to the exports to be removed.</param>
		/// <typeparam name="T">The type of the exports.</typeparam>
		public void ReleaseExports<T>(IEnumerable<Lazy<T>> exports)
		{
			Requires.NotNullOrNullElements(exports, "exports");
			foreach (Lazy<T> export in exports)
			{
				ReleaseExport(export);
			}
		}

		/// <summary>Removes a collection of exports from composition and releases their resources if possible.</summary>
		/// <param name="exports">A collection of indirect references to the exports to be removed and their metadata.</param>
		/// <typeparam name="T">The type of the exports.</typeparam>
		/// <typeparam name="TMetadataView">The type of the exports' metadata view.</typeparam>
		public void ReleaseExports<T, TMetadataView>(IEnumerable<Lazy<T, TMetadataView>> exports)
		{
			Requires.NotNullOrNullElements(exports, "exports");
			foreach (Lazy<T, TMetadataView> export in exports)
			{
				ReleaseExport(export);
			}
		}

		/// <summary>Satisfies the imports of the specified <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object without registering it for recomposition.</summary>
		/// <param name="part">The part to satisfy the imports of.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="part" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionException">An error occurred during composition. <see cref="P:System.ComponentModel.Composition.CompositionException.Errors" /> will contain a collection of the errors that occurred.</exception>
		public void SatisfyImportsOnce(ComposablePart part)
		{
			ThrowIfDisposed();
			if (_importEngine == null)
			{
				ImportEngine importEngine = new ImportEngine(this, _compositionOptions);
				lock (_lock)
				{
					if (_importEngine == null)
					{
						Thread.MemoryBarrier();
						_importEngine = importEngine;
						importEngine = null;
					}
				}
				importEngine?.Dispose();
			}
			_importEngine.SatisfyImportsOnce(part);
		}

		internal void OnExportsChangedInternal(object sender, ExportsChangeEventArgs e)
		{
			OnExportsChanged(e);
		}

		internal void OnExportsChangingInternal(object sender, ExportsChangeEventArgs e)
		{
			OnExportsChanging(e);
		}

		/// <summary>Returns a collection of all exports that match the conditions in the specified <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> object.</summary>
		/// <param name="definition">The object that defines the conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects to get.</param>
		/// <param name="atomicComposition">The composition transaction to use, or <see langword="null" /> to disable transactional composition.</param>
		/// <returns>A collection of all the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects in this <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object that match the conditions specified by <paramref name="definition" />.</returns>
		protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
		{
			ThrowIfDisposed();
			IEnumerable<Export> exports = null;
			if (!definition.Metadata.TryGetValue("System.ComponentModel.Composition.ImportSource", out var value))
			{
				value = ImportSource.Any;
			}
			switch ((ImportSource)value)
			{
			case ImportSource.Any:
				Assumes.NotNull(_rootProvider);
				_rootProvider.TryGetExports(definition, atomicComposition, out exports);
				break;
			case ImportSource.Local:
				Assumes.NotNull(_localExportProvider);
				_localExportProvider.TryGetExports(definition.RemoveImportSource(), atomicComposition, out exports);
				break;
			case ImportSource.NonLocal:
				if (_ancestorExportProvider != null)
				{
					_ancestorExportProvider.TryGetExports(definition.RemoveImportSource(), atomicComposition, out exports);
				}
				break;
			}
			return exports;
		}

		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}
	}
	internal sealed class CompositionLock : IDisposable
	{
		public sealed class CompositionLockHolder : IDisposable
		{
			private CompositionLock _lock;

			private int _isDisposed;

			public CompositionLockHolder(CompositionLock @lock)
			{
				_lock = @lock;
				_isDisposed = 0;
				_lock.EnterCompositionLock();
			}

			public void Dispose()
			{
				if (Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 0)
				{
					_lock.ExitCompositionLock();
				}
			}
		}

		private sealed class EmptyLockHolder : IDisposable
		{
			public void Dispose()
			{
			}
		}

		private readonly Microsoft.Internal.Lock _stateLock;

		private static object _compositionLock = new object();

		private int _isDisposed;

		private bool _isThreadSafe;

		private static readonly EmptyLockHolder _EmptyLockHolder = new EmptyLockHolder();

		public bool IsThreadSafe => _isThreadSafe;

		public CompositionLock(bool isThreadSafe)
		{
			_isThreadSafe = isThreadSafe;
			if (isThreadSafe)
			{
				_stateLock = new Microsoft.Internal.Lock();
			}
		}

		public void Dispose()
		{
			if (_isThreadSafe && Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 0)
			{
				_stateLock.Dispose();
			}
		}

		private void EnterCompositionLock()
		{
			if (_isThreadSafe)
			{
				Monitor.Enter(_compositionLock);
			}
		}

		private void ExitCompositionLock()
		{
			if (_isThreadSafe)
			{
				Monitor.Exit(_compositionLock);
			}
		}

		public IDisposable LockComposition()
		{
			if (_isThreadSafe)
			{
				return new CompositionLockHolder(this);
			}
			return _EmptyLockHolder;
		}

		public IDisposable LockStateForRead()
		{
			if (_isThreadSafe)
			{
				return new ReadLock(_stateLock);
			}
			return _EmptyLockHolder;
		}

		public IDisposable LockStateForWrite()
		{
			if (_isThreadSafe)
			{
				return new WriteLock(_stateLock);
			}
			return _EmptyLockHolder;
		}
	}
	/// <summary>Defines options for export providers.</summary>
	[Flags]
	public enum CompositionOptions
	{
		/// <summary>No options are defined.</summary>
		Default = 0,
		/// <summary>Silent rejection is disabled, so all rejections will result in errors.</summary>
		DisableSilentRejection = 1,
		/// <summary>This provider should be thread-safe.</summary>
		IsThreadSafe = 2,
		/// <summary>This provider is an export composition service.</summary>
		ExportCompositionService = 4
	}
	/// <summary>Represents a node in a tree of scoped catalogs, reflecting an underlying catalog and its child scopes.</summary>
	[DebuggerTypeProxy(typeof(CompositionScopeDefinitionDebuggerProxy))]
	public class CompositionScopeDefinition : ComposablePartCatalog, INotifyComposablePartCatalogChanged
	{
		private ComposablePartCatalog _catalog;

		private IEnumerable<ExportDefinition> _publicSurface;

		private IEnumerable<CompositionScopeDefinition> _children = Enumerable.Empty<CompositionScopeDefinition>();

		private volatile int _isDisposed;

		/// <summary>Gets the child scopes of this catalog.</summary>
		/// <returns>A collection of the child scopes of this catalog.</returns>
		public virtual IEnumerable<CompositionScopeDefinition> Children
		{
			get
			{
				ThrowIfDisposed();
				return _children;
			}
		}

		/// <summary>Gets a collection of parts visible to the parent scope of this catalog.</summary>
		/// <returns>A collection of parts visible to the parent scope of this catalog.</returns>
		public virtual IEnumerable<ExportDefinition> PublicSurface
		{
			get
			{
				ThrowIfDisposed();
				if (_publicSurface == null)
				{
					return this.SelectMany((ComposablePartDefinition p) => p.ExportDefinitions);
				}
				return _publicSurface;
			}
		}

		/// <summary>Occurs when the underlying catalog has changed, if that catalog supports notifications.</summary>
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed;

		/// <summary>Occurs when the underlying catalog is changing, if that catalog supports notifications.</summary>
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition" /> class.</summary>
		protected CompositionScopeDefinition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition" /> class with the specified underlying catalog and children.</summary>
		/// <param name="catalog">The underlying catalog for this catalog.</param>
		/// <param name="children">A collection of the child scopes of this catalog.</param>
		public CompositionScopeDefinition(ComposablePartCatalog catalog, IEnumerable<CompositionScopeDefinition> children)
		{
			Requires.NotNull(catalog, "catalog");
			Requires.NullOrNotNullElements(children, "children");
			InitializeCompositionScopeDefinition(catalog, children, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition" /> class with the specified underlying catalog, children, and public surface.</summary>
		/// <param name="catalog">The underlying catalog for this catalog.</param>
		/// <param name="children">A collection of the child scopes of this catalog.</param>
		/// <param name="publicSurface">The public surface for this catalog.</param>
		public CompositionScopeDefinition(ComposablePartCatalog catalog, IEnumerable<CompositionScopeDefinition> children, IEnumerable<ExportDefinition> publicSurface)
		{
			Requires.NotNull(catalog, "catalog");
			Requires.NullOrNotNullElements(children, "children");
			Requires.NullOrNotNullElements(publicSurface, "publicSurface");
			InitializeCompositionScopeDefinition(catalog, children, publicSurface);
		}

		private void InitializeCompositionScopeDefinition(ComposablePartCatalog catalog, IEnumerable<CompositionScopeDefinition> children, IEnumerable<ExportDefinition> publicSurface)
		{
			_catalog = catalog;
			if (children != null)
			{
				_children = children.ToArray();
			}
			if (publicSurface != null)
			{
				_publicSurface = publicSurface;
			}
			if (_catalog is INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged)
			{
				notifyComposablePartCatalogChanged.Changed += OnChangedInternal;
				notifyComposablePartCatalogChanged.Changing += OnChangingInternal;
			}
		}

		/// <summary>Called by the <see langword="Dispose()" /> and <see langword="Finalize()" /> methods to release the managed and unmanaged resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition" /> class.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && Interlocked.CompareExchange(ref _isDisposed, 1, 0) == 0 && _catalog is INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged)
				{
					notifyComposablePartCatalogChanged.Changed -= OnChangedInternal;
					notifyComposablePartCatalogChanged.Changing -= OnChangingInternal;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Returns an enumerator that iterates through the catalog.</summary>
		/// <returns>An enumerator that can be used to iterate through the catalog.</returns>
		public override IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			return _catalog.GetEnumerator();
		}

		/// <summary>Gets a collection of exports that match the conditions specified by the import definition.</summary>
		/// <param name="definition">Conditions that specify which exports to match.</param>
		/// <returns>A collection of exports that match the specified conditions.</returns>
		public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			ThrowIfDisposed();
			return _catalog.GetExports(definition);
		}

		internal IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExportsFromPublicSurface(ImportDefinition definition)
		{
			Assumes.NotNull(definition, "definition");
			List<Tuple<ComposablePartDefinition, ExportDefinition>> list = new List<Tuple<ComposablePartDefinition, ExportDefinition>>();
			foreach (ExportDefinition item in PublicSurface)
			{
				if (!definition.IsConstraintSatisfiedBy(item))
				{
					continue;
				}
				foreach (Tuple<ComposablePartDefinition, ExportDefinition> export in GetExports(definition))
				{
					if (export.Item2 == item)
					{
						list.Add(export);
						break;
					}
				}
			}
			return list;
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition.Changed" /> event.</summary>
		/// <param name="e">Contains data for the <see cref="E:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition.Changed" /> event.</param>
		protected virtual void OnChanged(ComposablePartCatalogChangeEventArgs e)
		{
			this.Changed?.Invoke(this, e);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition.Changing" /> event.</summary>
		/// <param name="e">Contains data for the <see cref="E:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition.Changing" /> event.</param>
		protected virtual void OnChanging(ComposablePartCatalogChangeEventArgs e)
		{
			this.Changing?.Invoke(this, e);
		}

		private void OnChangedInternal(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			OnChanged(e);
		}

		private void OnChangingInternal(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			OnChanging(e);
		}

		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (_isDisposed == 1)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}
	}
	internal class CompositionScopeDefinitionDebuggerProxy
	{
		private readonly CompositionScopeDefinition _compositionScopeDefinition;

		public ReadOnlyCollection<ComposablePartDefinition> Parts => _compositionScopeDefinition.Parts.ToReadOnlyCollection();

		public IEnumerable<ExportDefinition> PublicSurface => _compositionScopeDefinition.PublicSurface.ToReadOnlyCollection();

		public virtual IEnumerable<CompositionScopeDefinition> Children => _compositionScopeDefinition.Children.ToReadOnlyCollection();

		public CompositionScopeDefinitionDebuggerProxy(CompositionScopeDefinition compositionScopeDefinition)
		{
			Requires.NotNull(compositionScopeDefinition, "compositionScopeDefinition");
			_compositionScopeDefinition = compositionScopeDefinition;
		}
	}
	/// <summary>Provides methods to satisfy imports on an existing part instance.</summary>
	public class CompositionService : ICompositionService, IDisposable
	{
		private CompositionContainer _compositionContainer;

		private INotifyComposablePartCatalogChanged _notifyCatalog;

		internal CompositionService(ComposablePartCatalog composablePartCatalog)
		{
			Assumes.NotNull(composablePartCatalog);
			_notifyCatalog = composablePartCatalog as INotifyComposablePartCatalogChanged;
			try
			{
				if (_notifyCatalog != null)
				{
					_notifyCatalog.Changing += OnCatalogChanging;
				}
				CompositionOptions compositionOptions = CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe | CompositionOptions.ExportCompositionService;
				CompositionContainer compositionContainer = new CompositionContainer(composablePartCatalog, compositionOptions);
				_compositionContainer = compositionContainer;
			}
			catch
			{
				if (_notifyCatalog != null)
				{
					_notifyCatalog.Changing -= OnCatalogChanging;
				}
				throw;
			}
		}

		/// <summary>Composes the specified part, with recomposition and validation disabled.</summary>
		/// <param name="part">The part to compose.</param>
		public void SatisfyImportsOnce(ComposablePart part)
		{
			Requires.NotNull(part, "part");
			Assumes.NotNull(_compositionContainer);
			_compositionContainer.SatisfyImportsOnce(part);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> class.</summary>
		public void Dispose()
		{
			Assumes.NotNull(_compositionContainer);
			if (_notifyCatalog != null)
			{
				_notifyCatalog.Changing -= OnCatalogChanging;
			}
			_compositionContainer.Dispose();
		}

		private void OnCatalogChanging(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			throw new ChangeRejectedException(Strings.NotSupportedCatalogChanges);
		}

		internal CompositionService()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	internal static class CompositionServices
	{
		private class MetadataList
		{
			private Type _arrayType;

			private bool _containsNulls;

			private static readonly Type ObjectType = typeof(object);

			private static readonly Type TypeType = typeof(Type);

			private Collection<object> _innerList = new Collection<object>();

			public void Add(object item, Type itemType)
			{
				_containsNulls |= item == null;
				if (itemType == ObjectType)
				{
					itemType = null;
				}
				if (itemType == null && item != null)
				{
					itemType = item.GetType();
				}
				if (item is Type)
				{
					itemType = TypeType;
				}
				if (itemType != null)
				{
					InferArrayType(itemType);
				}
				_innerList.Add(item);
			}

			private void InferArrayType(Type itemType)
			{
				Assumes.NotNull(itemType);
				if (_arrayType == null)
				{
					_arrayType = itemType;
				}
				else if (_arrayType != itemType)
				{
					_arrayType = ObjectType;
				}
			}

			public Array ToArray()
			{
				if (_arrayType == null)
				{
					_arrayType = ObjectType;
				}
				else if (_containsNulls && _arrayType.IsValueType)
				{
					_arrayType = ObjectType;
				}
				Array array = Array.CreateInstance(_arrayType, _innerList.Count);
				for (int i = 0; i < array.Length; i++)
				{
					array.SetValue(_innerList[i], i);
				}
				return array;
			}
		}

		internal static readonly Type InheritedExportAttributeType = typeof(InheritedExportAttribute);

		internal static readonly Type ExportAttributeType = typeof(ExportAttribute);

		internal static readonly Type AttributeType = typeof(Attribute);

		internal static readonly Type ObjectType = typeof(object);

		private static readonly string[] reservedMetadataNames = new string[1] { "System.ComponentModel.Composition.CreationPolicy" };

		internal static Type GetDefaultTypeFromMember(this MemberInfo member)
		{
			Assumes.NotNull(member);
			switch (member.MemberType)
			{
			case MemberTypes.Property:
				return ((PropertyInfo)member).PropertyType;
			case MemberTypes.TypeInfo:
			case MemberTypes.NestedType:
				return (Type)member;
			default:
				Assumes.IsTrue(member.MemberType == MemberTypes.Field);
				return ((FieldInfo)member).FieldType;
			}
		}

		internal static Type AdjustSpecifiedTypeIdentityType(this Type specifiedContractType, MemberInfo member)
		{
			if (member.MemberType == MemberTypes.Method)
			{
				return specifiedContractType;
			}
			return specifiedContractType.AdjustSpecifiedTypeIdentityType(member.GetDefaultTypeFromMember());
		}

		internal static Type AdjustSpecifiedTypeIdentityType(this Type specifiedContractType, Type memberType)
		{
			Assumes.NotNull(specifiedContractType);
			if (memberType != null && memberType.IsGenericType && specifiedContractType.IsGenericType)
			{
				if (specifiedContractType.ContainsGenericParameters && !memberType.ContainsGenericParameters)
				{
					Type[] genericArguments = memberType.GetGenericArguments();
					Type[] genericArguments2 = specifiedContractType.GetGenericArguments();
					if (genericArguments.Length == genericArguments2.Length)
					{
						return specifiedContractType.MakeGenericType(genericArguments);
					}
				}
				else if (specifiedContractType.ContainsGenericParameters && memberType.ContainsGenericParameters)
				{
					IList<Type> pureGenericParameters = memberType.GetPureGenericParameters();
					if (specifiedContractType.GetPureGenericArity() == pureGenericParameters.Count)
					{
						return specifiedContractType.GetGenericTypeDefinition().MakeGenericType(pureGenericParameters.ToArray());
					}
				}
			}
			return specifiedContractType;
		}

		private static string AdjustTypeIdentity(string originalTypeIdentity, Type typeIdentityType)
		{
			return GenericServices.GetGenericName(originalTypeIdentity, GenericServices.GetGenericParametersOrder(typeIdentityType), typeIdentityType.GetPureGenericArity());
		}

		internal static void GetContractInfoFromExport(this MemberInfo member, ExportAttribute export, out Type typeIdentityType, out string contractName)
		{
			typeIdentityType = member.GetTypeIdentityTypeFromExport(export);
			if (!string.IsNullOrEmpty(export.ContractName))
			{
				contractName = export.ContractName;
			}
			else
			{
				contractName = member.GetTypeIdentityFromExport(typeIdentityType);
			}
		}

		internal static string GetTypeIdentityFromExport(this MemberInfo member, Type typeIdentityType)
		{
			if (typeIdentityType != null)
			{
				string text = AttributedModelServices.GetTypeIdentity(typeIdentityType);
				if (typeIdentityType.ContainsGenericParameters)
				{
					text = AdjustTypeIdentity(text, typeIdentityType);
				}
				return text;
			}
			MethodInfo obj = member as MethodInfo;
			Assumes.NotNull(obj);
			return AttributedModelServices.GetTypeIdentity(obj);
		}

		private static Type GetTypeIdentityTypeFromExport(this MemberInfo member, ExportAttribute export)
		{
			if (export.ContractType != null)
			{
				return export.ContractType.AdjustSpecifiedTypeIdentityType(member);
			}
			if (member.MemberType == MemberTypes.Method)
			{
				return null;
			}
			return member.GetDefaultTypeFromMember();
		}

		internal static bool IsContractNameSameAsTypeIdentity(this ExportAttribute export)
		{
			return string.IsNullOrEmpty(export.ContractName);
		}

		internal static Type GetContractTypeFromImport(this IAttributedImport import, ImportType importType)
		{
			if (import.ContractType != null)
			{
				return import.ContractType.AdjustSpecifiedTypeIdentityType(importType.ContractType);
			}
			return importType.ContractType;
		}

		internal static string GetContractNameFromImport(this IAttributedImport import, ImportType importType)
		{
			if (!string.IsNullOrEmpty(import.ContractName))
			{
				return import.ContractName;
			}
			return AttributedModelServices.GetContractName(import.GetContractTypeFromImport(importType));
		}

		internal static string GetTypeIdentityFromImport(this IAttributedImport import, ImportType importType)
		{
			Type contractTypeFromImport = import.GetContractTypeFromImport(importType);
			if (contractTypeFromImport == ObjectType)
			{
				return null;
			}
			return AttributedModelServices.GetTypeIdentity(contractTypeFromImport);
		}

		internal static IDictionary<string, object> GetPartMetadataForType(this Type type, CreationPolicy creationPolicy)
		{
			IDictionary<string, object> dictionary = new Dictionary<string, object>(StringComparers.MetadataKeyNames);
			if (creationPolicy != CreationPolicy.Any)
			{
				dictionary.Add("System.ComponentModel.Composition.CreationPolicy", creationPolicy);
			}
			PartMetadataAttribute[] attributes = type.GetAttributes<PartMetadataAttribute>();
			foreach (PartMetadataAttribute partMetadataAttribute in attributes)
			{
				if (!reservedMetadataNames.Contains(partMetadataAttribute.Name, StringComparers.MetadataKeyNames) && !dictionary.ContainsKey(partMetadataAttribute.Name))
				{
					dictionary.Add(partMetadataAttribute.Name, partMetadataAttribute.Value);
				}
			}
			if (type.ContainsGenericParameters)
			{
				dictionary.Add("System.ComponentModel.Composition.IsGenericPart", true);
				Type[] genericArguments = type.GetGenericArguments();
				dictionary.Add("System.ComponentModel.Composition.GenericPartArity", genericArguments.Length);
				bool flag = false;
				object[] array = new object[genericArguments.Length];
				GenericParameterAttributes[] array2 = new GenericParameterAttributes[genericArguments.Length];
				for (int j = 0; j < genericArguments.Length; j++)
				{
					Type obj = genericArguments[j];
					Type[] array3 = obj.GetGenericParameterConstraints();
					if (array3.Length == 0)
					{
						array3 = null;
					}
					GenericParameterAttributes genericParameterAttributes = obj.GenericParameterAttributes;
					if (array3 != null || genericParameterAttributes != GenericParameterAttributes.None)
					{
						array[j] = array3;
						array2[j] = genericParameterAttributes;
						flag = true;
					}
				}
				if (flag)
				{
					dictionary.Add("System.ComponentModel.Composition.GenericParameterConstraints", array);
					dictionary.Add("System.ComponentModel.Composition.GenericParameterAttributes", array2);
				}
			}
			if (dictionary.Count == 0)
			{
				return MetadataServices.EmptyMetadata;
			}
			return dictionary;
		}

		internal static void TryExportMetadataForMember(this MemberInfo member, out IDictionary<string, object> dictionary)
		{
			dictionary = new Dictionary<string, object>();
			Attribute[] attributes = member.GetAttributes<Attribute>();
			foreach (Attribute attribute in attributes)
			{
				ExportMetadataAttribute exportMetadataAttribute = attribute as ExportMetadataAttribute;
				if (exportMetadataAttribute != null)
				{
					if (reservedMetadataNames.Contains(exportMetadataAttribute.Name, StringComparers.MetadataKeyNames))
					{
						throw ExceptionBuilder.CreateDiscoveryException(Strings.Discovery_ReservedMetadataNameUsed, member.GetDisplayName(), exportMetadataAttribute.Name);
					}
					if (!dictionary.TryContributeMetadataValue(exportMetadataAttribute.Name, exportMetadataAttribute.Value, null, exportMetadataAttribute.IsMultiple))
					{
						throw ExceptionBuilder.CreateDiscoveryException(Strings.Discovery_DuplicateMetadataNameValues, member.GetDisplayName(), exportMetadataAttribute.Name);
					}
					continue;
				}
				Type type = attribute.GetType();
				if (!(type != ExportAttributeType) || !type.IsAttributeDefined<MetadataAttributeAttribute>(inherit: true))
				{
					continue;
				}
				bool allowsMultiple = false;
				AttributeUsageAttribute firstAttribute = type.GetFirstAttribute<AttributeUsageAttribute>(inherit: true);
				if (firstAttribute != null)
				{
					allowsMultiple = firstAttribute.AllowMultiple;
				}
				PropertyInfo[] properties = type.GetProperties();
				foreach (PropertyInfo propertyInfo in properties)
				{
					if (!(propertyInfo.DeclaringType == ExportAttributeType) && !(propertyInfo.DeclaringType == AttributeType))
					{
						if (reservedMetadataNames.Contains(propertyInfo.Name, StringComparers.MetadataKeyNames))
						{
							throw ExceptionBuilder.CreateDiscoveryException(Strings.Discovery_ReservedMetadataNameUsed, member.GetDisplayName(), exportMetadataAttribute.Name);
						}
						object value = propertyInfo.GetValue(attribute, null);
						if (value != null && !IsValidAttributeType(value.GetType()))
						{
							throw ExceptionBuilder.CreateDiscoveryException(Strings.Discovery_MetadataContainsValueWithInvalidType, propertyInfo.GetDisplayName(), value.GetType().GetDisplayName());
						}
						if (!dictionary.TryContributeMetadataValue(propertyInfo.Name, value, propertyInfo.PropertyType, allowsMultiple))
						{
							throw ExceptionBuilder.CreateDiscoveryException(Strings.Discovery_DuplicateMetadataNameValues, member.GetDisplayName(), propertyInfo.Name);
						}
					}
				}
			}
			string[] array = dictionary.Keys.ToArray();
			foreach (string key in array)
			{
				if (dictionary[key] is MetadataList metadataList)
				{
					dictionary[key] = metadataList.ToArray();
				}
			}
		}

		private static bool TryContributeMetadataValue(this IDictionary<string, object> dictionary, string name, object value, Type valueType, bool allowsMultiple)
		{
			if (!dictionary.TryGetValue(name, out var value2))
			{
				if (allowsMultiple)
				{
					MetadataList metadataList = new MetadataList();
					metadataList.Add(value, valueType);
					value = metadataList;
				}
				dictionary.Add(name, value);
			}
			else
			{
				MetadataList metadataList2 = value2 as MetadataList;
				if (!allowsMultiple || metadataList2 == null)
				{
					dictionary.Remove(name);
					return false;
				}
				metadataList2.Add(value, valueType);
			}
			return true;
		}

		internal static IEnumerable<KeyValuePair<string, Type>> GetRequiredMetadata(Type metadataViewType)
		{
			if (metadataViewType == null || ExportServices.IsDefaultMetadataViewType(metadataViewType) || ExportServices.IsDictionaryConstructorViewType(metadataViewType) || !metadataViewType.IsInterface)
			{
				return Enumerable.Empty<KeyValuePair<string, Type>>();
			}
			return from property in (from property in metadataViewType.GetAllProperties()
					where property.GetFirstAttribute<DefaultValueAttribute>() == null
					select property).ToList()
				select new KeyValuePair<string, Type>(property.Name, property.PropertyType);
		}

		internal static IDictionary<string, object> GetImportMetadata(ImportType importType, IAttributedImport attributedImport)
		{
			return GetImportMetadata(importType.ContractType, attributedImport);
		}

		internal static IDictionary<string, object> GetImportMetadata(Type type, IAttributedImport attributedImport)
		{
			Dictionary<string, object> dictionary = null;
			if (type.IsGenericType)
			{
				dictionary = new Dictionary<string, object>();
				if (type.ContainsGenericParameters)
				{
					dictionary["System.ComponentModel.Composition.GenericImportParametersOrderMetadataName"] = GenericServices.GetGenericParametersOrder(type);
				}
				else
				{
					dictionary["System.ComponentModel.Composition.GenericContractName"] = ContractNameServices.GetTypeIdentity(type.GetGenericTypeDefinition());
					dictionary["System.ComponentModel.Composition.GenericParameters"] = type.GetGenericArguments();
				}
			}
			if (attributedImport != null && attributedImport.Source != ImportSource.Any)
			{
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
				}
				dictionary["System.ComponentModel.Composition.ImportSource"] = attributedImport.Source;
			}
			if (dictionary != null)
			{
				return dictionary.AsReadOnly();
			}
			return MetadataServices.EmptyMetadata;
		}

		internal static object GetExportedValueFromComposedPart(ImportEngine engine, ComposablePart part, ExportDefinition definition)
		{
			if (engine != null)
			{
				try
				{
					engine.SatisfyImports(part);
				}
				catch (CompositionException innerException)
				{
					throw ExceptionBuilder.CreateCannotGetExportedValue(part, definition, innerException);
				}
			}
			try
			{
				return part.GetExportedValue(definition);
			}
			catch (ComposablePartException innerException2)
			{
				throw ExceptionBuilder.CreateCannotGetExportedValue(part, definition, innerException2);
			}
		}

		internal static bool IsRecomposable(this ComposablePart part)
		{
			return part.ImportDefinitions.Any((ImportDefinition import) => import.IsRecomposable);
		}

		internal static CompositionResult TryInvoke(Action action)
		{
			try
			{
				action();
				return CompositionResult.SucceededResult;
			}
			catch (CompositionException ex)
			{
				return new CompositionResult(ex.Errors);
			}
		}

		internal static CompositionResult TryFire<TEventArgs>(EventHandler<TEventArgs> _delegate, object sender, TEventArgs e) where TEventArgs : EventArgs
		{
			CompositionResult result = CompositionResult.SucceededResult;
			Delegate[] invocationList = _delegate.GetInvocationList();
			for (int i = 0; i < invocationList.Length; i++)
			{
				EventHandler<TEventArgs> eventHandler = (EventHandler<TEventArgs>)invocationList[i];
				try
				{
					eventHandler(sender, e);
				}
				catch (CompositionException ex)
				{
					result = result.MergeErrors(ex.Errors);
				}
			}
			return result;
		}

		internal static CreationPolicy GetRequiredCreationPolicy(this ImportDefinition definition)
		{
			if (definition is ContractBasedImportDefinition contractBasedImportDefinition)
			{
				return contractBasedImportDefinition.RequiredCreationPolicy;
			}
			return CreationPolicy.Any;
		}

		internal static bool IsAtMostOne(this ImportCardinality cardinality)
		{
			if (cardinality != ImportCardinality.ZeroOrOne)
			{
				return cardinality == ImportCardinality.ExactlyOne;
			}
			return true;
		}

		private static bool IsValidAttributeType(Type type)
		{
			return IsValidAttributeType(type, arrayAllowed: true);
		}

		private static bool IsValidAttributeType(Type type, bool arrayAllowed)
		{
			Assumes.NotNull(type);
			if (type.IsPrimitive)
			{
				return true;
			}
			if (type == typeof(string))
			{
				return true;
			}
			if (type.IsEnum && type.IsVisible)
			{
				return true;
			}
			if (typeof(Type).IsAssignableFrom(type))
			{
				return true;
			}
			if (arrayAllowed && type.IsArray && type.GetArrayRank() == 1 && IsValidAttributeType(type.GetElementType(), arrayAllowed: false))
			{
				return true;
			}
			return false;
		}
	}
	/// <summary>Discovers attributed parts in the assemblies in a specified directory.</summary>
	[DebuggerTypeProxy(typeof(DirectoryCatalogDebuggerProxy))]
	public class DirectoryCatalog : ComposablePartCatalog, INotifyComposablePartCatalogChanged, ICompositionElement
	{
		internal class DirectoryCatalogDebuggerProxy
		{
			private readonly DirectoryCatalog _catalog;

			public ReadOnlyCollection<Assembly> Assemblies => _catalog._assemblyCatalogs.Values.Select((AssemblyCatalog catalog) => catalog.Assembly).ToReadOnlyCollection();

			public ReflectionContext ReflectionContext => _catalog._reflectionContext;

			public string SearchPattern => _catalog.SearchPattern;

			public string Path => _catalog._path;

			public string FullPath => _catalog._fullPath;

			public ReadOnlyCollection<string> LoadedFiles => _catalog._loadedFiles;

			public ReadOnlyCollection<ComposablePartDefinition> Parts => _catalog.Parts.ToReadOnlyCollection();

			public DirectoryCatalogDebuggerProxy(DirectoryCatalog catalog)
			{
				Requires.NotNull(catalog, "catalog");
				_catalog = catalog;
			}
		}

		private readonly Microsoft.Internal.Lock _thisLock = new Microsoft.Internal.Lock();

		private readonly ICompositionElement _definitionOrigin;

		private ComposablePartCatalogCollection _catalogCollection;

		private Dictionary<string, AssemblyCatalog> _assemblyCatalogs;

		private volatile bool _isDisposed;

		private string _path;

		private string _fullPath;

		private string _searchPattern;

		private ReadOnlyCollection<string> _loadedFiles;

		private readonly ReflectionContext _reflectionContext;

		/// <summary>Gets the translated absolute path observed by the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> object.</summary>
		/// <returns>The translated absolute path observed by the catalog.</returns>
		public string FullPath => _fullPath;

		/// <summary>Gets the collection of files currently loaded in the catalog.</summary>
		/// <returns>A collection of files currently loaded in the catalog.</returns>
		public ReadOnlyCollection<string> LoadedFiles
		{
			get
			{
				using (new ReadLock(_thisLock))
				{
					return _loadedFiles;
				}
			}
		}

		/// <summary>Gets the path observed by the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> object.</summary>
		/// <returns>The path observed by the catalog.</returns>
		public string Path => _path;

		/// <summary>Gets the search pattern that is passed into the constructor of the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> object.</summary>
		/// <returns>The search pattern the catalog uses to find files. The default is *.dll, which returns all DLL files.</returns>
		public string SearchPattern => _searchPattern;

		/// <summary>Gets the display name of the directory catalog.</summary>
		/// <returns>A string that contains a human-readable display name of the directory catalog.</returns>
		string ICompositionElement.DisplayName => GetDisplayName();

		/// <summary>Gets the composition element from which the directory catalog originated.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		ICompositionElement ICompositionElement.Origin => null;

		/// <summary>Occurs when the contents of the catalog has changed.</summary>
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed;

		/// <summary>Occurs when the catalog is changing.</summary>
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> class by using <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects based on all the DLL files in the specified directory path.</summary>
		/// <param name="path">The path to the directory to scan for assemblies to add to the catalog.  
		///  The path must be absolute or relative to <see cref="P:System.AppDomain.BaseDirectory" />.</param>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified <paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more implementation-specific invalid characters.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified <paramref name="path" />, file name, or both exceed the system-defined maximum length.</exception>
		public DirectoryCatalog(string path)
			: this(path, "*.dll")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> class by using <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects based on all the DLL files in the specified directory path, in the specified reflection context.</summary>
		/// <param name="path">The path to the directory to scan for assemblies to add to the catalog.  
		///  The path must be absolute or relative to <see cref="P:System.AppDomain.BaseDirectory" />.</param>
		/// <param name="reflectionContext">The context used to create parts.</param>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified <paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more implementation-specific invalid characters.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified <paramref name="path" />, file name, or both exceed the system-defined maximum length.</exception>
		public DirectoryCatalog(string path, ReflectionContext reflectionContext)
			: this(path, "*.dll", reflectionContext)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> class by using <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects based on all the DLL files in the specified directory path with the specified source for parts.</summary>
		/// <param name="path">The path to the directory to scan for assemblies to add to the catalog.  
		///  The path must be absolute or relative to <see cref="P:System.AppDomain.BaseDirectory" />.</param>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the source for parts.</param>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified <paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more implementation-specific invalid characters.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified <paramref name="path" />, file name, or both exceed the system-defined maximum length.</exception>
		public DirectoryCatalog(string path, ICompositionElement definitionOrigin)
			: this(path, "*.dll", definitionOrigin)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> class by  using <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects based on all the DLL files in the specified directory path, in the specified reflection context.</summary>
		/// <param name="path">The path to the directory to scan for assemblies to add to the catalog.  
		///  The path must be absolute or relative to <see cref="P:System.AppDomain.BaseDirectory" />.</param>
		/// <param name="reflectionContext">The context used to create parts.</param>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the source for parts.</param>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified <paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more implementation-specific invalid characters.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified <paramref name="path" />, file name, or both exceed the system-defined maximum length.</exception>
		public DirectoryCatalog(string path, ReflectionContext reflectionContext, ICompositionElement definitionOrigin)
			: this(path, "*.dll", reflectionContext, definitionOrigin)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> class by using <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects that match a specified search pattern in the specified directory path.</summary>
		/// <param name="path">The path to the directory to scan for assemblies to add to the catalog.  
		///  The path must be absolute or relative to <see cref="P:System.AppDomain.BaseDirectory" />.</param>
		/// <param name="searchPattern">The search string. The format of the string should be the same as specified for the <see cref="M:System.IO.Directory.GetFiles(System.String,System.String)" /> method.</param>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified <paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more implementation-specific invalid characters.  
		/// -or-  
		/// <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified <paramref name="path" />, file name, or both exceed the system-defined maximum length.</exception>
		public DirectoryCatalog(string path, string searchPattern)
		{
			Requires.NotNullOrEmpty(path, "path");
			Requires.NotNullOrEmpty(searchPattern, "searchPattern");
			_definitionOrigin = this;
			Initialize(path, searchPattern);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> class by using <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects based on the specified search pattern in the specified directory path with the specified source for parts.</summary>
		/// <param name="path">The path to the directory to scan for assemblies to add to the catalog.  
		///  The path must be absolute or relative to <see cref="P:System.AppDomain.BaseDirectory" />.</param>
		/// <param name="searchPattern">The search string. The format of the string should be the same as specified for the <see cref="M:System.IO.Directory.GetFiles(System.String,System.String)" /> method.</param>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the source for parts.</param>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified <paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more implementation-specific invalid characters.  
		/// -or-  
		/// <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified <paramref name="path" />, file name, or both exceed the system-defined maximum length.</exception>
		public DirectoryCatalog(string path, string searchPattern, ICompositionElement definitionOrigin)
		{
			Requires.NotNullOrEmpty(path, "path");
			Requires.NotNullOrEmpty(searchPattern, "searchPattern");
			Requires.NotNull(definitionOrigin, "definitionOrigin");
			_definitionOrigin = definitionOrigin;
			Initialize(path, searchPattern);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> class by using <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects based on the specified search pattern in the specified directory path, using the specified reflection context.</summary>
		/// <param name="path">The path to the directory to scan for assemblies to add to the catalog.  
		///  The path must be absolute or relative to <see cref="P:System.AppDomain.BaseDirectory" />.</param>
		/// <param name="searchPattern">The search string. The format of the string should be the same as specified for the <see cref="M:System.IO.Directory.GetFiles(System.String,System.String)" /> method.</param>
		/// <param name="reflectionContext">The context used to create parts.</param>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified <paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more implementation-specific invalid characters.  
		/// -or-  
		/// <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified <paramref name="path" />, file name, or both exceed the system-defined maximum length.</exception>
		public DirectoryCatalog(string path, string searchPattern, ReflectionContext reflectionContext)
		{
			Requires.NotNullOrEmpty(path, "path");
			Requires.NotNullOrEmpty(searchPattern, "searchPattern");
			Requires.NotNull(reflectionContext, "reflectionContext");
			_reflectionContext = reflectionContext;
			_definitionOrigin = this;
			Initialize(path, searchPattern);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> class by using <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects based on the specified search pattern in the specified directory path, using the specified reflection context.</summary>
		/// <param name="path">The path to the directory to scan for assemblies to add to the catalog.  
		///  The path must be absolute or relative to <see cref="P:System.AppDomain.BaseDirectory" />.</param>
		/// <param name="searchPattern">The search string. The format of the string should be the same as specified for the <see cref="M:System.IO.Directory.GetFiles(System.String,System.String)" /> method.</param>
		/// <param name="reflectionContext">The context used to create parts.</param>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the source for parts.</param>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified <paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more implementation-specific invalid characters.  
		/// -or-  
		/// <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified <paramref name="path" />, file name, or both exceed the system-defined maximum length.</exception>
		public DirectoryCatalog(string path, string searchPattern, ReflectionContext reflectionContext, ICompositionElement definitionOrigin)
		{
			Requires.NotNullOrEmpty(path, "path");
			Requires.NotNullOrEmpty(searchPattern, "searchPattern");
			Requires.NotNull(reflectionContext, "reflectionContext");
			Requires.NotNull(definitionOrigin, "definitionOrigin");
			_reflectionContext = reflectionContext;
			_definitionOrigin = definitionOrigin;
			Initialize(path, searchPattern);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!disposing || _isDisposed)
				{
					return;
				}
				bool flag = false;
				ComposablePartCatalogCollection composablePartCatalogCollection = null;
				try
				{
					using (new WriteLock(_thisLock))
					{
						if (!_isDisposed)
						{
							flag = true;
							composablePartCatalogCollection = _catalogCollection;
							_catalogCollection = null;
							_assemblyCatalogs = null;
							_isDisposed = true;
						}
					}
				}
				finally
				{
					composablePartCatalogCollection?.Dispose();
					if (flag)
					{
						_thisLock.Dispose();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Returns an enumerator that iterates through the catalog.</summary>
		/// <returns>An enumerator that can be used to iterate through the catalog.</returns>
		public override IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			return _catalogCollection.SelectMany((ComposablePartCatalog catalog) => catalog).GetEnumerator();
		}

		/// <summary>Gets the export definitions that match the constraint expressed by the specified import definition.</summary>
		/// <param name="definition">The conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects to be returned.</param>
		/// <returns>A collection of objects that contain the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects and their associated <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects that match the constraint specified by <paramref name="definition" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.</exception>
		public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			ThrowIfDisposed();
			Requires.NotNull(definition, "definition");
			return _catalogCollection.SelectMany((ComposablePartCatalog catalog) => catalog.GetExports(definition));
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.DirectoryCatalog.Changed" /> event.</summary>
		/// <param name="e">An object  that contains the event data.</param>
		protected virtual void OnChanged(ComposablePartCatalogChangeEventArgs e)
		{
			this.Changed?.Invoke(this, e);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.DirectoryCatalog.Changing" /> event.</summary>
		/// <param name="e">An object  that contains the event data.</param>
		protected virtual void OnChanging(ComposablePartCatalogChangeEventArgs e)
		{
			this.Changing?.Invoke(this, e);
		}

		/// <summary>Refreshes the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects with the latest files in the directory that match the search pattern.</summary>
		public void Refresh()
		{
			ThrowIfDisposed();
			Assumes.NotNull(_loadedFiles);
			ComposablePartDefinition[] addedDefinitions;
			ComposablePartDefinition[] removedDefinitions;
			while (true)
			{
				string[] files = GetFiles();
				object loadedFiles;
				string[] beforeFiles;
				using (new ReadLock(_thisLock))
				{
					loadedFiles = _loadedFiles;
					beforeFiles = _loadedFiles.ToArray();
				}
				DiffChanges(beforeFiles, files, out var catalogsToAdd, out var catalogsToRemove);
				if (catalogsToAdd.Count == 0 && catalogsToRemove.Count == 0)
				{
					return;
				}
				addedDefinitions = catalogsToAdd.SelectMany((Tuple<string, AssemblyCatalog> cat) => cat.Item2).ToArray();
				removedDefinitions = catalogsToRemove.SelectMany((Tuple<string, AssemblyCatalog> cat) => cat.Item2).ToArray();
				using AtomicComposition atomicComposition = new AtomicComposition();
				ComposablePartCatalogChangeEventArgs e = new ComposablePartCatalogChangeEventArgs(addedDefinitions, removedDefinitions, atomicComposition);
				OnChanging(e);
				using (new WriteLock(_thisLock))
				{
					if (loadedFiles != _loadedFiles)
					{
						continue;
					}
					foreach (Tuple<string, AssemblyCatalog> item in catalogsToAdd)
					{
						_assemblyCatalogs.Add(item.Item1, item.Item2);
						_catalogCollection.Add(item.Item2);
					}
					foreach (Tuple<string, AssemblyCatalog> item2 in catalogsToRemove)
					{
						_assemblyCatalogs.Remove(item2.Item1);
						_catalogCollection.Remove(item2.Item2);
					}
					_loadedFiles = files.ToReadOnlyCollection();
					atomicComposition.Complete();
					break;
				}
			}
			ComposablePartCatalogChangeEventArgs e2 = new ComposablePartCatalogChangeEventArgs(addedDefinitions, removedDefinitions, null);
			OnChanged(e2);
		}

		/// <summary>Gets a string representation of the directory catalog.</summary>
		/// <returns>A string representation of the catalog.</returns>
		public override string ToString()
		{
			return GetDisplayName();
		}

		private AssemblyCatalog CreateAssemblyCatalogGuarded(string assemblyFilePath)
		{
			Exception ex = null;
			try
			{
				return (_reflectionContext != null) ? new AssemblyCatalog(assemblyFilePath, _reflectionContext, this) : new AssemblyCatalog(assemblyFilePath, this);
			}
			catch (FileNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (FileLoadException ex3)
			{
				ex = ex3;
			}
			catch (BadImageFormatException ex4)
			{
				ex = ex4;
			}
			catch (ReflectionTypeLoadException ex5)
			{
				ex = ex5;
			}
			CompositionTrace.AssemblyLoadFailed(this, assemblyFilePath, ex);
			return null;
		}

		private void DiffChanges(string[] beforeFiles, string[] afterFiles, out List<Tuple<string, AssemblyCatalog>> catalogsToAdd, out List<Tuple<string, AssemblyCatalog>> catalogsToRemove)
		{
			catalogsToAdd = new List<Tuple<string, AssemblyCatalog>>();
			catalogsToRemove = new List<Tuple<string, AssemblyCatalog>>();
			foreach (string item in afterFiles.Except(beforeFiles))
			{
				AssemblyCatalog assemblyCatalog = CreateAssemblyCatalogGuarded(item);
				if (assemblyCatalog != null)
				{
					catalogsToAdd.Add(new Tuple<string, AssemblyCatalog>(item, assemblyCatalog));
				}
			}
			IEnumerable<string> enumerable = beforeFiles.Except(afterFiles);
			using (new ReadLock(_thisLock))
			{
				foreach (string item2 in enumerable)
				{
					if (_assemblyCatalogs.TryGetValue(item2, out var value))
					{
						catalogsToRemove.Add(new Tuple<string, AssemblyCatalog>(item2, value));
					}
				}
			}
		}

		private string GetDisplayName()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} (Path=\"{1}\")", GetType().Name, _path);
		}

		private string[] GetFiles()
		{
			return Directory.GetFiles(_fullPath, _searchPattern);
		}

		private static string GetFullPath(string path)
		{
			if (!System.IO.Path.IsPathRooted(path) && AppDomain.CurrentDomain.BaseDirectory != null)
			{
				path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
			}
			return System.IO.Path.GetFullPath(path);
		}

		private void Initialize(string path, string searchPattern)
		{
			_path = path;
			_fullPath = GetFullPath(path);
			_searchPattern = searchPattern;
			_assemblyCatalogs = new Dictionary<string, AssemblyCatalog>();
			_catalogCollection = new ComposablePartCatalogCollection(null, null, null);
			_loadedFiles = GetFiles().ToReadOnlyCollection();
			foreach (string loadedFile in _loadedFiles)
			{
				AssemblyCatalog assemblyCatalog = null;
				assemblyCatalog = CreateAssemblyCatalogGuarded(loadedFile);
				if (assemblyCatalog != null)
				{
					_assemblyCatalogs.Add(loadedFile, assemblyCatalog);
					_catalogCollection.Add(assemblyCatalog);
				}
			}
		}

		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}
	}
	/// <summary>Retrieves exports which match a specified <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> object.</summary>
	public abstract class ExportProvider
	{
		private static readonly Export[] EmptyExports = new Export[0];

		/// <summary>Occurs when the exports in the <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> change.</summary>
		public event EventHandler<ExportsChangeEventArgs> ExportsChanged;

		/// <summary>Occurs when the provided exports are changing.</summary>
		public event EventHandler<ExportsChangeEventArgs> ExportsChanging;

		/// <summary>Returns the export with the contract name derived from the specified type parameter. If there is not exactly one matching export, an exception is thrown.</summary>
		/// <typeparam name="T">The type parameter of the <see cref="T:System.Lazy`1" /> object to return. The contract name is also derived from this type parameter.</typeparam>
		/// <returns>The export with the contract name derived from the specified type parameter.</returns>
		/// <exception cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException">There are zero <see cref="T:System.Lazy`1" /> objects with the contract name derived from <paramref name="T" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object.  
		///  -or-  
		///  There is more than one <see cref="T:System.Lazy`1" /> object with the contract name derived from <paramref name="T" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		public Lazy<T> GetExport<T>()
		{
			return GetExport<T>(null);
		}

		/// <summary>Returns the export with the specified contract name. If there is not exactly one matching export, an exception is thrown.</summary>
		/// <param name="contractName">The contract name of the <see cref="T:System.Lazy`1" /> object to return, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		/// <typeparam name="T">The type parameter of the <see cref="T:System.Lazy`1" /> object to return.</typeparam>
		/// <returns>The export with the specified contract name.</returns>
		/// <exception cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException">There are zero <see cref="T:System.Lazy`1" /> objects with the contract name derived from <paramref name="T" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object.  
		///  -or-  
		///  There is more than one <see cref="T:System.Lazy`1" /> object with the contract name derived from <paramref name="T" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		public Lazy<T> GetExport<T>(string contractName)
		{
			return GetExportCore<T>(contractName);
		}

		/// <summary>Returns the export with the contract name derived from the specified type parameter. If there is not exactly one matching export, an exception is thrown.</summary>
		/// <typeparam name="T">The type parameter of the <see cref="T:System.Lazy`2" /> object to return. The contract name is also derived from this type parameter.</typeparam>
		/// <typeparam name="TMetadataView">The type of the metadata view of the <see cref="T:System.Lazy`2" /> object to return.</typeparam>
		/// <returns>System.Lazy`2</returns>
		/// <exception cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException">There are zero <see cref="T:System.Lazy`2" /> objects with the contract name derived from <paramref name="T" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object.  
		///  -or-  
		///  There is more than one <see cref="T:System.Lazy`2" /> object with the contract name derived from <paramref name="T" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="TMetadataView" /> is not a valid metadata view type.</exception>
		public Lazy<T, TMetadataView> GetExport<T, TMetadataView>()
		{
			return GetExport<T, TMetadataView>(null);
		}

		/// <summary>Returns the export with the specified contract name. If there is not exactly one matching export, an exception is thrown.</summary>
		/// <param name="contractName">The contract name of the <see cref="T:System.Lazy`2" /> object to return, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		/// <typeparam name="T">The type parameter of the <see cref="T:System.Lazy`2" /> object to return.</typeparam>
		/// <typeparam name="TMetadataView">The type of the metadata view of the <see cref="T:System.Lazy`2" /> object to return.</typeparam>
		/// <returns>The export with the specified contract name.</returns>
		/// <exception cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException">There are zero <see cref="T:System.Lazy`2" /> objects with the contract name derived from <paramref name="T" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object.  
		///  -or-  
		///  There is more than one <see cref="T:System.Lazy`2" /> object with the contract name derived from <paramref name="T" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="TMetadataView" /> is not a valid metadata view type.</exception>
		public Lazy<T, TMetadataView> GetExport<T, TMetadataView>(string contractName)
		{
			return GetExportCore<T, TMetadataView>(contractName);
		}

		/// <summary>Gets all the exports with the specified contract name.</summary>
		/// <param name="type">The type parameter of the <see cref="T:System.Lazy`2" /> objects to return.</param>
		/// <param name="metadataViewType">The type of the metadata view of the <see cref="T:System.Lazy`2" /> objects to return.</param>
		/// <param name="contractName">The contract name of the <see cref="T:System.Lazy`2" /> object to return, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		/// <returns>A collection of all the <see cref="T:System.Lazy`2" /> objects for the contract matching <paramref name="contractName" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="metadataViewType" /> is not a valid metadata view type.</exception>
		public IEnumerable<Lazy<object, object>> GetExports(Type type, Type metadataViewType, string contractName)
		{
			IEnumerable<Export> exportsCore = GetExportsCore(type, metadataViewType, contractName, ImportCardinality.ZeroOrMore);
			Collection<Lazy<object, object>> collection = new Collection<Lazy<object, object>>();
			Func<Export, Lazy<object, object>> func = ExportServices.CreateSemiStronglyTypedLazyFactory(type, metadataViewType);
			foreach (Export item in exportsCore)
			{
				collection.Add(func(item));
			}
			return collection;
		}

		/// <summary>Gets all the exports with the contract name derived from the specified type parameter.</summary>
		/// <typeparam name="T">The type parameter of the <see cref="T:System.Lazy`1" /> objects to return. The contract name is also derived from this type parameter.</typeparam>
		/// <returns>The <see cref="T:System.Lazy`1" /> objects with the contract name derived from <paramref name="T" />, if found; otherwise, an empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		public IEnumerable<Lazy<T>> GetExports<T>()
		{
			return GetExports<T>(null);
		}

		/// <summary>Gets all the exports with the specified contract name.</summary>
		/// <param name="contractName">The contract name of the <see cref="T:System.Lazy`1" /> objects to return, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		/// <typeparam name="T">The type parameter of the <see cref="T:System.Lazy`1" /> objects to return.</typeparam>
		/// <returns>The <see cref="T:System.Lazy`1" /> objects with the specified contract name, if found; otherwise, an empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		public IEnumerable<Lazy<T>> GetExports<T>(string contractName)
		{
			return GetExportsCore<T>(contractName);
		}

		/// <summary>Gets all the exports with the contract name derived from the specified type parameter.</summary>
		/// <typeparam name="T">The type parameter of the <see cref="T:System.Lazy`2" /> objects to return. The contract name is also derived from this type parameter.</typeparam>
		/// <typeparam name="TMetadataView">The type of the metadata view of the <see cref="T:System.Lazy`2" /> objects to return.</typeparam>
		/// <returns>The <see cref="T:System.Lazy`2" /> objects with the contract name derived from <paramref name="T" />, if found; otherwise, an empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="TMetadataView" /> is not a valid metadata view type.</exception>
		public IEnumerable<Lazy<T, TMetadataView>> GetExports<T, TMetadataView>()
		{
			return GetExports<T, TMetadataView>(null);
		}

		/// <summary>Gets all the exports with the specified contract name.</summary>
		/// <param name="contractName">The contract name of the <see cref="T:System.Lazy`2" /> objects to return, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		/// <typeparam name="T">The type parameter of the <see cref="T:System.Lazy`2" /> objects to return. The contract name is also derived from this type parameter.</typeparam>
		/// <typeparam name="TMetadataView">The type of the metadata view of the <see cref="T:System.Lazy`2" /> objects to return.</typeparam>
		/// <returns>The <see cref="T:System.Lazy`2" /> objects with the specified contract name if found; otherwise, an empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="TMetadataView" /> is not a valid metadata view type.</exception>
		public IEnumerable<Lazy<T, TMetadataView>> GetExports<T, TMetadataView>(string contractName)
		{
			return GetExportsCore<T, TMetadataView>(contractName);
		}

		/// <summary>Returns the exported object with the contract name derived from the specified type parameter. If there is not exactly one matching exported object, an exception is thrown.</summary>
		/// <typeparam name="T">The type of the exported object to return. The contract name is also derived from this type parameter.</typeparam>
		/// <returns>The exported object with the contract name derived from the specified type parameter.</returns>
		/// <exception cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException">There are zero exported objects with the contract name derived from <paramref name="T" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.  
		///  -or-  
		///  There is more than one exported object with the contract name derived from <paramref name="T" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionContractMismatchException">The underlying exported object cannot be cast to <paramref name="T" />.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionException">An error occurred during composition. <see cref="P:System.ComponentModel.Composition.CompositionException.Errors" /> will contain a collection of errors that occurred.</exception>
		public T GetExportedValue<T>()
		{
			return GetExportedValue<T>(null);
		}

		/// <summary>Returns the exported object with the specified contract name. If there is not exactly one matching exported object, an exception is thrown.</summary>
		/// <param name="contractName">The contract name of the exported object to return, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		/// <typeparam name="T">The type of the exported object to return.</typeparam>
		/// <returns>The exported object with the specified contract name.</returns>
		/// <exception cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException">There are zero exported objects with the contract name derived from <paramref name="T" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.  
		///  -or-  
		///  There is more than one exported object with the contract name derived from <paramref name="T" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionContractMismatchException">The underlying exported object cannot be cast to <paramref name="T" />.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionException">An error occurred during composition. <see cref="P:System.ComponentModel.Composition.CompositionException.Errors" /> will contain a collection of errors that occurred.</exception>
		public T GetExportedValue<T>(string contractName)
		{
			return GetExportedValueCore<T>(contractName, ImportCardinality.ExactlyOne);
		}

		/// <summary>Gets the exported object with the contract name derived from the specified type parameter or the default value for the specified type, or throws an exception if there is more than one matching exported object.</summary>
		/// <typeparam name="T">The type of the exported object to return. The contract name is also derived from this type parameter.</typeparam>
		/// <returns>The exported object with the contract name derived from <paramref name="T" />, if found; otherwise, the default value for <paramref name="T" />.</returns>
		/// <exception cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException">There is more than one exported object with the contract name derived from <paramref name="T" /> in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionContractMismatchException">The underlying exported object cannot be cast to <paramref name="T" />.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionException">An error occurred during composition. <see cref="P:System.ComponentModel.Composition.CompositionException.Errors" /> will contain a collection of errors that occurred.</exception>
		public T GetExportedValueOrDefault<T>()
		{
			return GetExportedValueOrDefault<T>(null);
		}

		/// <summary>Gets the exported object with the specified contract name or the default value for the specified type, or throws an exception if there is more than one matching exported object.</summary>
		/// <param name="contractName">The contract name of the exported object to return, or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		/// <typeparam name="T">The type of the exported object to return.</typeparam>
		/// <returns>The exported object with the specified contract name, if found; otherwise, the default value for <paramref name="T" />.</returns>
		/// <exception cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException">There is more than one exported object with the specified contract name in the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionContractMismatchException">The underlying exported object cannot be cast to <paramref name="T" />.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionException">An error occurred during composition. <see cref="P:System.ComponentModel.Composition.CompositionException.Errors" /> will contain a collection of errors that occurred.</exception>
		public T GetExportedValueOrDefault<T>(string contractName)
		{
			return GetExportedValueCore<T>(contractName, ImportCardinality.ZeroOrOne);
		}

		/// <summary>Gets all the exported objects with the contract name derived from the specified type parameter.</summary>
		/// <typeparam name="T">The type of the exported object to return. The contract name is also derived from this type parameter.</typeparam>
		/// <returns>The exported objects with the contract name derived from the specified type parameter, if found; otherwise, an empty <see cref="T:System.Collections.ObjectModel.Collection`1" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionContractMismatchException">One or more of the underlying exported objects cannot be cast to <paramref name="T" />.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionException">An error occurred during composition. <see cref="P:System.ComponentModel.Composition.CompositionException.Errors" /> will contain a collection of errors that occurred.</exception>
		public IEnumerable<T> GetExportedValues<T>()
		{
			return GetExportedValues<T>(null);
		}

		/// <summary>Gets all the exported objects with the specified contract name.</summary>
		/// <param name="contractName">The contract name of the exported objects to return; or <see langword="null" /> or an empty string ("") to use the default contract name.</param>
		/// <typeparam name="T">The type of the exported object to return.</typeparam>
		/// <returns>The exported objects with the specified contract name, if found; otherwise, an empty <see cref="T:System.Collections.ObjectModel.Collection`1" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionContractMismatchException">One or more of the underlying exported values cannot be cast to <paramref name="T" />.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionException">An error occurred during composition. <see cref="P:System.ComponentModel.Composition.CompositionException.Errors" /> will contain a collection of errors that occurred.</exception>
		public IEnumerable<T> GetExportedValues<T>(string contractName)
		{
			return GetExportedValuesCore<T>(contractName);
		}

		private IEnumerable<T> GetExportedValuesCore<T>(string contractName)
		{
			IEnumerable<Export> exportsCore = GetExportsCore(typeof(T), null, contractName, ImportCardinality.ZeroOrMore);
			Collection<T> collection = new Collection<T>();
			foreach (Export item in exportsCore)
			{
				collection.Add(ExportServices.GetCastedExportedValue<T>(item));
			}
			return collection;
		}

		private T GetExportedValueCore<T>(string contractName, ImportCardinality cardinality)
		{
			Assumes.IsTrue(cardinality.IsAtMostOne());
			Export export = GetExportsCore(typeof(T), null, contractName, cardinality).SingleOrDefault();
			if (export == null)
			{
				return default(T);
			}
			return ExportServices.GetCastedExportedValue<T>(export);
		}

		private IEnumerable<Lazy<T>> GetExportsCore<T>(string contractName)
		{
			IEnumerable<Export> exportsCore = GetExportsCore(typeof(T), null, contractName, ImportCardinality.ZeroOrMore);
			Collection<Lazy<T>> collection = new Collection<Lazy<T>>();
			foreach (Export item in exportsCore)
			{
				collection.Add(ExportServices.CreateStronglyTypedLazyOfT<T>(item));
			}
			return collection;
		}

		private IEnumerable<Lazy<T, TMetadataView>> GetExportsCore<T, TMetadataView>(string contractName)
		{
			IEnumerable<Export> exportsCore = GetExportsCore(typeof(T), typeof(TMetadataView), contractName, ImportCardinality.ZeroOrMore);
			Collection<Lazy<T, TMetadataView>> collection = new Collection<Lazy<T, TMetadataView>>();
			foreach (Export item in exportsCore)
			{
				collection.Add(ExportServices.CreateStronglyTypedLazyOfTM<T, TMetadataView>(item));
			}
			return collection;
		}

		private Lazy<T, TMetadataView> GetExportCore<T, TMetadataView>(string contractName)
		{
			Export export = GetExportsCore(typeof(T), typeof(TMetadataView), contractName, ImportCardinality.ExactlyOne).SingleOrDefault();
			if (export == null)
			{
				return null;
			}
			return ExportServices.CreateStronglyTypedLazyOfTM<T, TMetadataView>(export);
		}

		private Lazy<T> GetExportCore<T>(string contractName)
		{
			Export export = GetExportsCore(typeof(T), null, contractName, ImportCardinality.ExactlyOne).SingleOrDefault();
			if (export == null)
			{
				return null;
			}
			return ExportServices.CreateStronglyTypedLazyOfT<T>(export);
		}

		private IEnumerable<Export> GetExportsCore(Type type, Type metadataViewType, string contractName, ImportCardinality cardinality)
		{
			Requires.NotNull(type, "type");
			if (string.IsNullOrEmpty(contractName))
			{
				contractName = AttributedModelServices.GetContractName(type);
			}
			if (metadataViewType == null)
			{
				metadataViewType = ExportServices.DefaultMetadataViewType;
			}
			if (!MetadataViewProvider.IsViewTypeValid(metadataViewType))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.InvalidMetadataView, metadataViewType.Name));
			}
			ImportDefinition definition = BuildImportDefinition(type, metadataViewType, contractName, cardinality);
			return GetExports(definition, null);
		}

		private static ImportDefinition BuildImportDefinition(Type type, Type metadataViewType, string contractName, ImportCardinality cardinality)
		{
			Assumes.NotNull(type, metadataViewType, contractName);
			IEnumerable<KeyValuePair<string, Type>> requiredMetadata = CompositionServices.GetRequiredMetadata(metadataViewType);
			IDictionary<string, object> importMetadata = CompositionServices.GetImportMetadata(type, null);
			string requiredTypeIdentity = null;
			if (type != typeof(object))
			{
				requiredTypeIdentity = AttributedModelServices.GetTypeIdentity(type);
			}
			return new ContractBasedImportDefinition(contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable: false, isPrerequisite: true, CreationPolicy.Any, importMetadata);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> class.</summary>
		protected ExportProvider()
		{
		}

		/// <summary>Gets all exports that match the conditions of the specified import definition.</summary>
		/// <param name="definition">The object that defines the conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects to get.</param>
		/// <returns>A collection of all the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects matching the condition specified by <paramref name="definition" />.</returns>
		/// <exception cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException">
		///   <see cref="P:System.ComponentModel.Composition.Primitives.ImportDefinition.Cardinality" /> is <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ExactlyOne" /> and there are zero <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects that match the conditions of the specified <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />.  
		/// -or-  
		/// <see cref="P:System.ComponentModel.Composition.Primitives.ImportDefinition.Cardinality" /> is <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ZeroOrOne" /> or <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ExactlyOne" /> and there is more than one <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object that matches the conditions of the specified <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.</exception>
		public IEnumerable<Export> GetExports(ImportDefinition definition)
		{
			return GetExports(definition, null);
		}

		/// <summary>Gets all exports that match the conditions of the specified import definition and composition.</summary>
		/// <param name="definition">The object that defines the conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects to get.</param>
		/// <param name="atomicComposition">The transactional container for the composition.</param>
		/// <returns>A collection of all the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects matching the condition specified by <paramref name="definition" /> and <paramref name="atomicComposition" />.</returns>
		/// <exception cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException">
		///   <see cref="P:System.ComponentModel.Composition.Primitives.ImportDefinition.Cardinality" /> is <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ExactlyOne" /> and there are zero <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects that match the conditions of the specified <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />.  
		/// -or-  
		/// <see cref="P:System.ComponentModel.Composition.Primitives.ImportDefinition.Cardinality" /> is <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ZeroOrOne" /> or <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ExactlyOne" /> and there is more than one <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object that matches the conditions of the specified <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="atomicComposition" /> is <see langword="null" />.</exception>
		public IEnumerable<Export> GetExports(ImportDefinition definition, AtomicComposition atomicComposition)
		{
			Requires.NotNull(definition, "definition");
			IEnumerable<Export> exports;
			ExportCardinalityCheckResult exportCardinalityCheckResult = TryGetExportsCore(definition, atomicComposition, out exports);
			switch (exportCardinalityCheckResult)
			{
			case ExportCardinalityCheckResult.Match:
				return exports;
			case ExportCardinalityCheckResult.NoExports:
				throw new ImportCardinalityMismatchException(string.Format(CultureInfo.CurrentCulture, Strings.CardinalityMismatch_NoExports, definition.ToString()));
			default:
				Assumes.IsTrue(exportCardinalityCheckResult == ExportCardinalityCheckResult.TooManyExports);
				throw new ImportCardinalityMismatchException(string.Format(CultureInfo.CurrentCulture, Strings.CardinalityMismatch_TooManyExports, definition.ToString()));
			}
		}

		/// <summary>Gets all the exports that match the conditions of the specified import.</summary>
		/// <param name="definition">The object that defines the conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects to get.</param>
		/// <param name="atomicComposition">The transactional container for the composition.</param>
		/// <param name="exports">When this method returns, contains a collection of <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects that match the conditions defined by <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />, if found; otherwise, an empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> object. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <see cref="P:System.ComponentModel.Composition.Primitives.ImportDefinition.Cardinality" /> is <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ZeroOrOne" /> or <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ZeroOrMore" /> and there are zero <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects that match the conditions of the specified <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />; <see langword="true" /> if <see cref="P:System.ComponentModel.Composition.Primitives.ImportDefinition.Cardinality" /> is <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ZeroOrOne" /> or <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ExactlyOne" /> and there is exactly one <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> that matches the conditions of the specified <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.</exception>
		public bool TryGetExports(ImportDefinition definition, AtomicComposition atomicComposition, out IEnumerable<Export> exports)
		{
			Requires.NotNull(definition, "definition");
			exports = null;
			return TryGetExportsCore(definition, atomicComposition, out exports) == ExportCardinalityCheckResult.Match;
		}

		/// <summary>Gets all the exports that match the constraint defined by the specified definition.</summary>
		/// <param name="definition">The object that defines the conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects to return.</param>
		/// <param name="atomicComposition">The transactional container for the composition.</param>
		/// <returns>A collection that contains all the exports that match the specified condition.</returns>
		protected abstract IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition);

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.ExportProvider.ExportsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.Composition.Hosting.ExportsChangeEventArgs" /> that contains the event data.</param>
		protected virtual void OnExportsChanged(ExportsChangeEventArgs e)
		{
			EventHandler<ExportsChangeEventArgs> eventHandler = this.ExportsChanged;
			if (eventHandler != null)
			{
				CompositionServices.TryFire(eventHandler, this, e).ThrowOnErrors(e.AtomicComposition);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.ExportProvider.ExportsChanging" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.Composition.Hosting.ExportsChangeEventArgs" /> that contains the event data.</param>
		protected virtual void OnExportsChanging(ExportsChangeEventArgs e)
		{
			EventHandler<ExportsChangeEventArgs> eventHandler = this.ExportsChanging;
			if (eventHandler != null)
			{
				CompositionServices.TryFire(eventHandler, this, e).ThrowOnErrors(e.AtomicComposition);
			}
		}

		private ExportCardinalityCheckResult TryGetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition, out IEnumerable<Export> exports)
		{
			Assumes.NotNull(definition);
			exports = GetExportsCore(definition, atomicComposition);
			ExportCardinalityCheckResult exportCardinalityCheckResult = ExportServices.CheckCardinality(definition, exports);
			if (exportCardinalityCheckResult == ExportCardinalityCheckResult.TooManyExports && definition.Cardinality == ImportCardinality.ZeroOrOne)
			{
				exportCardinalityCheckResult = ExportCardinalityCheckResult.Match;
				exports = null;
			}
			if (exports == null)
			{
				exports = EmptyExports;
			}
			return exportCardinalityCheckResult;
		}
	}
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Composition.Hosting.ExportProvider.ExportsChanging" /> and <see cref="E:System.ComponentModel.Composition.Hosting.ExportProvider.ExportsChanged" /> event.</summary>
	public class ExportsChangeEventArgs : EventArgs
	{
		private readonly IEnumerable<ExportDefinition> _addedExports;

		private readonly IEnumerable<ExportDefinition> _removedExports;

		private IEnumerable<string> _changedContractNames;

		/// <summary>Gets the exports that were added in this change.</summary>
		/// <returns>A collection of the exports that were added.</returns>
		public IEnumerable<ExportDefinition> AddedExports => _addedExports;

		/// <summary>Gets the exports that were removed in the change.</summary>
		/// <returns>A collection of the removed exports.</returns>
		public IEnumerable<ExportDefinition> RemovedExports => _removedExports;

		/// <summary>Gets the contract names that were altered in the change.</summary>
		/// <returns>A collection of the altered contract names.</returns>
		public IEnumerable<string> ChangedContractNames
		{
			get
			{
				if (_changedContractNames == null)
				{
					_changedContractNames = (from export in AddedExports.Concat(RemovedExports)
						select export.ContractName).Distinct().ToArray();
				}
				return _changedContractNames;
			}
		}

		/// <summary>Gets the composition transaction of the change, if any.</summary>
		/// <returns>A reference to the composition transaction associated with the change, or <see langword="null" /> if no transaction is being used.</returns>
		public AtomicComposition AtomicComposition { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ExportsChangeEventArgs" /> class.</summary>
		/// <param name="addedExports">The events that were added.</param>
		/// <param name="removedExports">The events that were removed.</param>
		/// <param name="atomicComposition">The composition transaction that contains the change.</param>
		public ExportsChangeEventArgs(IEnumerable<ExportDefinition> addedExports, IEnumerable<ExportDefinition> removedExports, AtomicComposition atomicComposition)
		{
			Requires.NotNull(addedExports, "addedExports");
			Requires.NotNull(removedExports, "removedExports");
			_addedExports = addedExports.AsArray();
			_removedExports = removedExports.AsArray();
			AtomicComposition = atomicComposition;
		}
	}
	/// <summary>Represents a catalog after a filter function is applied to it.</summary>
	public class FilteredCatalog : ComposablePartCatalog, INotifyComposablePartCatalogChanged
	{
		internal class DependenciesTraversal : IComposablePartCatalogTraversal
		{
			private IEnumerable<ComposablePartDefinition> _parts;

			private Func<ImportDefinition, bool> _importFilter;

			private Dictionary<string, List<ComposablePartDefinition>> _exportersIndex;

			public DependenciesTraversal(FilteredCatalog catalog, Func<ImportDefinition, bool> importFilter)
			{
				Assumes.NotNull(catalog);
				Assumes.NotNull(importFilter);
				_parts = catalog._innerCatalog;
				_importFilter = importFilter;
			}

			public void Initialize()
			{
				BuildExportersIndex();
			}

			private void BuildExportersIndex()
			{
				_exportersIndex = new Dictionary<string, List<ComposablePartDefinition>>();
				foreach (ComposablePartDefinition part in _parts)
				{
					foreach (ExportDefinition exportDefinition in part.ExportDefinitions)
					{
						AddToExportersIndex(exportDefinition.ContractName, part);
					}
				}
			}

			private void AddToExportersIndex(string contractName, ComposablePartDefinition part)
			{
				List<ComposablePartDefinition> value = null;
				if (!_exportersIndex.TryGetValue(contractName, out value))
				{
					value = new List<ComposablePartDefinition>();
					_exportersIndex.Add(contractName, value);
				}
				value.Add(part);
			}

			public bool TryTraverse(ComposablePartDefinition part, out IEnumerable<ComposablePartDefinition> reachableParts)
			{
				reachableParts = null;
				List<ComposablePartDefinition> list = null;
				foreach (ImportDefinition item in part.ImportDefinitions.Where(_importFilter))
				{
					List<ComposablePartDefinition> value = null;
					foreach (string candidateContractName in item.GetCandidateContractNames(part))
					{
						if (!_exportersIndex.TryGetValue(candidateContractName, out value))
						{
							continue;
						}
						foreach (ComposablePartDefinition item2 in value)
						{
							foreach (ExportDefinition exportDefinition in item2.ExportDefinitions)
							{
								if (item.IsImportDependentOnPart(item2, exportDefinition, part.IsGeneric() != item2.IsGeneric()))
								{
									if (list == null)
									{
										list = new List<ComposablePartDefinition>();
									}
									list.Add(item2);
								}
							}
						}
					}
				}
				reachableParts = list;
				return reachableParts != null;
			}
		}

		internal class DependentsTraversal : IComposablePartCatalogTraversal
		{
			private IEnumerable<ComposablePartDefinition> _parts;

			private Func<ImportDefinition, bool> _importFilter;

			private Dictionary<string, List<ComposablePartDefinition>> _importersIndex;

			public DependentsTraversal(FilteredCatalog catalog, Func<ImportDefinition, bool> importFilter)
			{
				Assumes.NotNull(catalog);
				Assumes.NotNull(importFilter);
				_parts = catalog._innerCatalog;
				_importFilter = importFilter;
			}

			public void Initialize()
			{
				BuildImportersIndex();
			}

			private void BuildImportersIndex()
			{
				_importersIndex = new Dictionary<string, List<ComposablePartDefinition>>();
				foreach (ComposablePartDefinition part in _parts)
				{
					foreach (ImportDefinition importDefinition in part.ImportDefinitions)
					{
						foreach (string candidateContractName in importDefinition.GetCandidateContractNames(part))
						{
							AddToImportersIndex(candidateContractName, part);
						}
					}
				}
			}

			private void AddToImportersIndex(string contractName, ComposablePartDefinition part)
			{
				List<ComposablePartDefinition> value = null;
				if (!_importersIndex.TryGetValue(contractName, out value))
				{
					value = new List<ComposablePartDefinition>();
					_importersIndex.Add(contractName, value);
				}
				value.Add(part);
			}

			public bool TryTraverse(ComposablePartDefinition part, out IEnumerable<ComposablePartDefinition> reachableParts)
			{
				reachableParts = null;
				List<ComposablePartDefinition> list = null;
				foreach (ExportDefinition exportDefinition in part.ExportDefinitions)
				{
					List<ComposablePartDefinition> value = null;
					if (!_importersIndex.TryGetValue(exportDefinition.ContractName, out value))
					{
						continue;
					}
					foreach (ComposablePartDefinition item in value)
					{
						foreach (ImportDefinition item2 in item.ImportDefinitions.Where(_importFilter))
						{
							if (item2.IsImportDependentOnPart(part, exportDefinition, part.IsGeneric() != item.IsGeneric()))
							{
								if (list == null)
								{
									list = new List<ComposablePartDefinition>();
								}
								list.Add(item);
							}
						}
					}
				}
				reachableParts = list;
				return reachableParts != null;
			}
		}

		internal interface IComposablePartCatalogTraversal
		{
			void Initialize();

			bool TryTraverse(ComposablePartDefinition part, out IEnumerable<ComposablePartDefinition> reachableParts);
		}

		private Func<ComposablePartDefinition, bool> _filter;

		private ComposablePartCatalog _innerCatalog;

		private FilteredCatalog _complement;

		private object _lock = new object();

		private volatile bool _isDisposed;

		/// <summary>Gets a catalog that contains parts that are present in the underlying catalog but that were filtered out by the filter function.</summary>
		/// <returns>A catalog that contains the complement of this catalog.</returns>
		public FilteredCatalog Complement
		{
			get
			{
				ThrowIfDisposed();
				if (_complement == null)
				{
					FilteredCatalog filteredCatalog = new FilteredCatalog(_innerCatalog, (ComposablePartDefinition p) => !_filter(p), this);
					lock (_lock)
					{
						if (_complement == null)
						{
							Thread.MemoryBarrier();
							_complement = filteredCatalog;
							filteredCatalog = null;
						}
					}
					filteredCatalog?.Dispose();
				}
				return _complement;
			}
		}

		/// <summary>Occurs when the underlying catalog has changed.</summary>
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed;

		/// <summary>Occurs when the underlying catalog is changing.</summary>
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing;

		/// <summary>Gets a new <see cref="T:System.ComponentModel.Composition.Hosting.FilteredCatalog" /> object that contains all the parts from this catalog and all their dependencies.</summary>
		/// <returns>The new catalog.</returns>
		public FilteredCatalog IncludeDependencies()
		{
			return IncludeDependencies((ImportDefinition i) => i.Cardinality == ImportCardinality.ExactlyOne);
		}

		/// <summary>Gets a new <see cref="T:System.ComponentModel.Composition.Hosting.FilteredCatalog" /> object that contains all the parts from this catalog and all dependencies that can be reached through imports that match the specified filter.</summary>
		/// <param name="importFilter">The filter for imports.</param>
		/// <returns>The new catalog.</returns>
		public FilteredCatalog IncludeDependencies(Func<ImportDefinition, bool> importFilter)
		{
			Requires.NotNull(importFilter, "importFilter");
			ThrowIfDisposed();
			return Traverse(new DependenciesTraversal(this, importFilter));
		}

		/// <summary>Gets a new <see cref="T:System.ComponentModel.Composition.Hosting.FilteredCatalog" /> object that contains all the parts from this catalog and all their dependents.</summary>
		/// <returns>The new catalog.</returns>
		public FilteredCatalog IncludeDependents()
		{
			return IncludeDependents((ImportDefinition i) => i.Cardinality == ImportCardinality.ExactlyOne);
		}

		/// <summary>Gets a new <see cref="T:System.ComponentModel.Composition.Hosting.FilteredCatalog" /> object that contains all the parts from this catalog and all dependents that can be reached through imports that match the specified filter.</summary>
		/// <param name="importFilter">The filter for imports.</param>
		/// <returns>The new catalog.</returns>
		public FilteredCatalog IncludeDependents(Func<ImportDefinition, bool> importFilter)
		{
			Requires.NotNull(importFilter, "importFilter");
			ThrowIfDisposed();
			return Traverse(new DependentsTraversal(this, importFilter));
		}

		private FilteredCatalog Traverse(IComposablePartCatalogTraversal traversal)
		{
			Assumes.NotNull(traversal);
			FreezeInnerCatalog();
			try
			{
				traversal.Initialize();
				HashSet<ComposablePartDefinition> traversalClosure = GetTraversalClosure(_innerCatalog.Where(_filter), traversal);
				return new FilteredCatalog(_innerCatalog, (ComposablePartDefinition p) => traversalClosure.Contains(p));
			}
			finally
			{
				UnfreezeInnerCatalog();
			}
		}

		private static HashSet<ComposablePartDefinition> GetTraversalClosure(IEnumerable<ComposablePartDefinition> parts, IComposablePartCatalogTraversal traversal)
		{
			Assumes.NotNull(traversal);
			HashSet<ComposablePartDefinition> hashSet = new HashSet<ComposablePartDefinition>();
			GetTraversalClosure(parts, hashSet, traversal);
			return hashSet;
		}

		private static void GetTraversalClosure(IEnumerable<ComposablePartDefinition> parts, HashSet<ComposablePartDefinition> traversedParts, IComposablePartCatalogTraversal traversal)
		{
			foreach (ComposablePartDefinition part in parts)
			{
				if (traversedParts.Add(part))
				{
					IEnumerable<ComposablePartDefinition> reachableParts = null;
					if (traversal.TryTraverse(part, out reachableParts))
					{
						GetTraversalClosure(reachableParts, traversedParts, traversal);
					}
				}
			}
		}

		private void FreezeInnerCatalog()
		{
			if (_innerCatalog is INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged)
			{
				notifyComposablePartCatalogChanged.Changing += ThrowOnRecomposition;
			}
		}

		private void UnfreezeInnerCatalog()
		{
			if (_innerCatalog is INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged)
			{
				notifyComposablePartCatalogChanged.Changing -= ThrowOnRecomposition;
			}
		}

		private static void ThrowOnRecomposition(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			throw new ChangeRejectedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.FilteredCatalog" /> class with the specified underlying catalog and filter.</summary>
		/// <param name="catalog">The underlying catalog.</param>
		/// <param name="filter">The function to filter parts.</param>
		public FilteredCatalog(ComposablePartCatalog catalog, Func<ComposablePartDefinition, bool> filter)
			: this(catalog, filter, null)
		{
		}

		internal FilteredCatalog(ComposablePartCatalog catalog, Func<ComposablePartDefinition, bool> filter, FilteredCatalog complement)
		{
			Requires.NotNull(catalog, "catalog");
			Requires.NotNull(filter, "filter");
			_innerCatalog = catalog;
			_filter = (ComposablePartDefinition p) => filter(p.GetGenericPartDefinition() ?? p);
			_complement = complement;
			if (_innerCatalog is INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged)
			{
				notifyComposablePartCatalogChanged.Changed += OnChangedInternal;
				notifyComposablePartCatalogChanged.Changing += OnChangingInternal;
			}
		}

		/// <summary>Called by the <see langword="Dispose()" /> and <see langword="Finalize()" /> methods to release the managed and unmanaged resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.FilteredCatalog" /> class.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!disposing || _isDisposed)
				{
					return;
				}
				INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged = null;
				try
				{
					lock (_lock)
					{
						if (!_isDisposed)
						{
							_isDisposed = true;
							notifyComposablePartCatalogChanged = _innerCatalog as INotifyComposablePartCatalogChanged;
							_innerCatalog = null;
						}
					}
				}
				finally
				{
					if (notifyComposablePartCatalogChanged != null)
					{
						notifyComposablePartCatalogChanged.Changed -= OnChangedInternal;
						notifyComposablePartCatalogChanged.Changing -= OnChangingInternal;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Returns an enumerator that iterates through the catalog.</summary>
		/// <returns>An enumerator that can be used to iterate through the catalog.</returns>
		public override IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			return _innerCatalog.Where(_filter).GetEnumerator();
		}

		/// <summary>Gets the exported parts from this catalog that match the specified import.</summary>
		/// <param name="definition">The import to match.</param>
		/// <returns>A collection of matching parts.</returns>
		public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			ThrowIfDisposed();
			Requires.NotNull(definition, "definition");
			List<Tuple<ComposablePartDefinition, ExportDefinition>> list = new List<Tuple<ComposablePartDefinition, ExportDefinition>>();
			foreach (Tuple<ComposablePartDefinition, ExportDefinition> export in _innerCatalog.GetExports(definition))
			{
				if (_filter(export.Item1))
				{
					list.Add(export);
				}
			}
			return list;
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.FilteredCatalog.Changed" /> event.</summary>
		/// <param name="e">Provides data for the event.</param>
		protected virtual void OnChanged(ComposablePartCatalogChangeEventArgs e)
		{
			this.Changed?.Invoke(this, e);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.FilteredCatalog.Changing" /> event.</summary>
		/// <param name="e">Provides data for the event.</param>
		protected virtual void OnChanging(ComposablePartCatalogChangeEventArgs e)
		{
			this.Changing?.Invoke(this, e);
		}

		private void OnChangedInternal(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			ComposablePartCatalogChangeEventArgs e2 = ProcessEventArgs(e);
			if (e2 != null)
			{
				OnChanged(ProcessEventArgs(e2));
			}
		}

		private void OnChangingInternal(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			ComposablePartCatalogChangeEventArgs e2 = ProcessEventArgs(e);
			if (e2 != null)
			{
				OnChanging(ProcessEventArgs(e2));
			}
		}

		private ComposablePartCatalogChangeEventArgs ProcessEventArgs(ComposablePartCatalogChangeEventArgs e)
		{
			ComposablePartCatalogChangeEventArgs e2 = new ComposablePartCatalogChangeEventArgs(e.AddedDefinitions.Where(_filter), e.RemovedDefinitions.Where(_filter), e.AtomicComposition);
			if (e2.AddedDefinitions.FastAny() || e2.RemovedDefinitions.FastAny())
			{
				return e2;
			}
			return null;
		}

		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}
	}
	/// <summary>Provides notifications when a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> changes.</summary>
	public interface INotifyComposablePartCatalogChanged
	{
		/// <summary>Occurs when a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> has changed.</summary>
		event EventHandler<ComposablePartCatalogChangeEventArgs> Changed;

		/// <summary>Occurs when a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> is changing.</summary>
		event EventHandler<ComposablePartCatalogChangeEventArgs> Changing;
	}
	/// <summary>Performs composition for containers.</summary>
	public class ImportEngine : ICompositionService, IDisposable
	{
		private class EngineContext
		{
			private ImportEngine _importEngine;

			private List<PartManager> _addedPartManagers = new List<PartManager>();

			private List<PartManager> _removedPartManagers = new List<PartManager>();

			private EngineContext _parentEngineContext;

			public EngineContext(ImportEngine importEngine, EngineContext parentEngineContext)
			{
				_importEngine = importEngine;
				_parentEngineContext = parentEngineContext;
			}

			public void AddPartManager(PartManager part)
			{
				Assumes.NotNull(part);
				if (!_removedPartManagers.Remove(part))
				{
					_addedPartManagers.Add(part);
				}
			}

			public void RemovePartManager(PartManager part)
			{
				Assumes.NotNull(part);
				if (!_addedPartManagers.Remove(part))
				{
					_removedPartManagers.Add(part);
				}
			}

			public IEnumerable<PartManager> GetAddedPartManagers()
			{
				if (_parentEngineContext != null)
				{
					return _addedPartManagers.ConcatAllowingNull(_parentEngineContext.GetAddedPartManagers());
				}
				return _addedPartManagers;
			}

			public IEnumerable<PartManager> GetRemovedPartManagers()
			{
				if (_parentEngineContext != null)
				{
					return _removedPartManagers.ConcatAllowingNull(_parentEngineContext.GetRemovedPartManagers());
				}
				return _removedPartManagers;
			}

			public void Complete()
			{
				foreach (PartManager addedPartManager in _addedPartManagers)
				{
					_importEngine.StartSatisfyingImports(addedPartManager, null);
				}
				foreach (PartManager removedPartManager in _removedPartManagers)
				{
					_importEngine.StopSatisfyingImports(removedPartManager, null);
				}
			}
		}

		private class PartManager
		{
			private Dictionary<ImportDefinition, List<IDisposable>> _importedDisposableExports;

			private Dictionary<ImportDefinition, Export[]> _importCache;

			private string[] _importedContractNames;

			private ComposablePart _part;

			private ImportState _state;

			private readonly ImportEngine _importEngine;

			public ComposablePart Part => _part;

			public ImportState State
			{
				get
				{
					using (_importEngine._lock.LockStateForRead())
					{
						return _state;
					}
				}
				set
				{
					using (_importEngine._lock.LockStateForWrite())
					{
						_state = value;
					}
				}
			}

			public bool TrackingImports { get; set; }

			public PartManager(ImportEngine importEngine, ComposablePart part)
			{
				_importEngine = importEngine;
				_part = part;
			}

			public IEnumerable<string> GetImportedContractNames()
			{
				if (Part == null)
				{
					return Enumerable.Empty<string>();
				}
				if (_importedContractNames == null)
				{
					_importedContractNames = Part.ImportDefinitions.Select((ImportDefinition import) => import.ContractName ?? ImportDefinition.EmptyContractName).Distinct().ToArray();
				}
				return _importedContractNames;
			}

			public CompositionResult TrySetImport(ImportDefinition import, IEnumerable<Export> exports)
			{
				try
				{
					Part.SetImport(import, exports);
					UpdateDisposableDependencies(import, exports);
					return CompositionResult.SucceededResult;
				}
				catch (CompositionException innerException)
				{
					return new CompositionResult(ErrorBuilder.CreatePartCannotSetImport(Part, import, innerException));
				}
				catch (ComposablePartException innerException2)
				{
					return new CompositionResult(ErrorBuilder.CreatePartCannotSetImport(Part, import, innerException2));
				}
			}

			public void SetSavedImport(ImportDefinition import, Export[] exports, AtomicComposition atomicComposition)
			{
				if (atomicComposition != null)
				{
					Export[] savedExports = GetSavedImport(import);
					atomicComposition.AddRevertAction(delegate
					{
						SetSavedImport(import, savedExports, null);
					});
				}
				if (_importCache == null)
				{
					_importCache = new Dictionary<ImportDefinition, Export[]>();
				}
				_importCache[import] = exports;
			}

			public Export[] GetSavedImport(ImportDefinition import)
			{
				Export[] value = null;
				if (_importCache != null)
				{
					_importCache.TryGetValue(import, out value);
				}
				return value;
			}

			public void ClearSavedImports()
			{
				_importCache = null;
			}

			public CompositionResult TryOnComposed()
			{
				try
				{
					Part.Activate();
					return CompositionResult.SucceededResult;
				}
				catch (ComposablePartException innerException)
				{
					return new CompositionResult(ErrorBuilder.CreatePartCannotActivate(Part, innerException));
				}
			}

			public void UpdateDisposableDependencies(ImportDefinition import, IEnumerable<Export> exports)
			{
				List<IDisposable> list = null;
				foreach (IDisposable item in exports.OfType<IDisposable>())
				{
					if (list == null)
					{
						list = new List<IDisposable>();
					}
					list.Add(item);
				}
				List<IDisposable> value = null;
				if (_importedDisposableExports != null && _importedDisposableExports.TryGetValue(import, out value))
				{
					value.ForEach(delegate(IDisposable disposable)
					{
						disposable.Dispose();
					});
					if (list == null)
					{
						_importedDisposableExports.Remove(import);
						if (!_importedDisposableExports.FastAny())
						{
							_importedDisposableExports = null;
						}
						return;
					}
				}
				if (list != null)
				{
					if (_importedDisposableExports == null)
					{
						_importedDisposableExports = new Dictionary<ImportDefinition, List<IDisposable>>();
					}
					_importedDisposableExports[import] = list;
				}
			}

			public void DisposeAllDependencies()
			{
				if (_importedDisposableExports != null)
				{
					IEnumerable<IDisposable> source = _importedDisposableExports.Values.SelectMany((List<IDisposable> exports) => exports);
					_importedDisposableExports = null;
					source.ForEach(delegate(IDisposable disposableExport)
					{
						disposableExport.Dispose();
					});
				}
			}
		}

		private class RecompositionManager
		{
			private WeakReferenceCollection<PartManager> _partsToIndex = new WeakReferenceCollection<PartManager>();

			private WeakReferenceCollection<PartManager> _partsToUnindex = new WeakReferenceCollection<PartManager>();

			private Dictionary<string, WeakReferenceCollection<PartManager>> _partManagerIndex = new Dictionary<string, WeakReferenceCollection<PartManager>>();

			public void AddPartToIndex(PartManager partManager)
			{
				_partsToIndex.Add(partManager);
			}

			public void AddPartToUnindex(PartManager partManager)
			{
				_partsToUnindex.Add(partManager);
			}

			public IEnumerable<PartManager> GetAffectedParts(IEnumerable<string> changedContractNames)
			{
				UpdateImportIndex();
				List<PartManager> list = new List<PartManager>();
				list.AddRange(GetPartsImporting(ImportDefinition.EmptyContractName));
				foreach (string changedContractName in changedContractNames)
				{
					list.AddRange(GetPartsImporting(changedContractName));
				}
				return list;
			}

			public static IEnumerable<ImportDefinition> GetAffectedImports(ComposablePart part, IEnumerable<ExportDefinition> changedExports)
			{
				return part.ImportDefinitions.Where((ImportDefinition import) => IsAffectedImport(import, changedExports));
			}

			private static bool IsAffectedImport(ImportDefinition import, IEnumerable<ExportDefinition> changedExports)
			{
				foreach (ExportDefinition changedExport in changedExports)
				{
					if (import.IsConstraintSatisfiedBy(changedExport))
					{
						return true;
					}
				}
				return false;
			}

			public IEnumerable<PartManager> GetPartsImporting(string contractName)
			{
				if (!_partManagerIndex.TryGetValue(contractName, out var value))
				{
					return Enumerable.Empty<PartManager>();
				}
				return value.AliveItemsToList();
			}

			private void AddIndexEntries(PartManager partManager)
			{
				foreach (string importedContractName in partManager.GetImportedContractNames())
				{
					if (!_partManagerIndex.TryGetValue(importedContractName, out var value))
					{
						value = new WeakReferenceCollection<PartManager>();
						_partManagerIndex.Add(importedContractName, value);
					}
					if (!value.Contains(partManager))
					{
						value.Add(partManager);
					}
				}
			}

			private void RemoveIndexEntries(PartManager partManager)
			{
				foreach (string importedContractName in partManager.GetImportedContractNames())
				{
					if (_partManagerIndex.TryGetValue(importedContractName, out var value))
					{
						value.Remove(partManager);
						if (value.AliveItemsToList().Count == 0)
						{
							_partManagerIndex.Remove(importedContractName);
						}
					}
				}
			}

			private void UpdateImportIndex()
			{
				List<PartManager> list = _partsToIndex.AliveItemsToList();
				_partsToIndex.Clear();
				List<PartManager> list2 = _partsToUnindex.AliveItemsToList();
				_partsToUnindex.Clear();
				if (list.Count == 0 && list2.Count == 0)
				{
					return;
				}
				foreach (PartManager item in list)
				{
					int num = list2.IndexOf(item);
					if (num >= 0)
					{
						list2[num] = null;
					}
					else
					{
						AddIndexEntries(item);
					}
				}
				foreach (PartManager item2 in list2)
				{
					if (item2 != null)
					{
						RemoveIndexEntries(item2);
					}
				}
			}
		}

		private enum ImportState
		{
			NoImportsSatisfied,
			ImportsPreviewing,
			ImportsPreviewed,
			PreExportImportsSatisfying,
			PreExportImportsSatisfied,
			PostExportImportsSatisfying,
			PostExportImportsSatisfied,
			ComposedNotifying,
			Composed
		}

		private const int MaximumNumberOfCompositionIterations = 100;

		private volatile bool _isDisposed;

		private ExportProvider _sourceProvider;

		private Stack<PartManager> _recursionStateStack = new Stack<PartManager>();

		private ConditionalWeakTable<ComposablePart, PartManager> _partManagers = new ConditionalWeakTable<ComposablePart, PartManager>();

		private RecompositionManager _recompositionManager = new RecompositionManager();

		private readonly CompositionLock _lock;

		private readonly CompositionOptions _compositionOptions;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> class.</summary>
		/// <param name="sourceProvider">The <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> that provides the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> access to <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</param>
		public ImportEngine(ExportProvider sourceProvider)
			: this(sourceProvider, CompositionOptions.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> class, optionally in thread-safe mode.</summary>
		/// <param name="sourceProvider">The <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> that provides the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> access to <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</param>
		/// <param name="isThreadSafe">
		///   <see langword="true" /> if thread safety is required; otherwise, <see langword="false" />.</param>
		public ImportEngine(ExportProvider sourceProvider, bool isThreadSafe)
			: this(sourceProvider, isThreadSafe ? CompositionOptions.IsThreadSafe : CompositionOptions.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> class with the specified options.</summary>
		/// <param name="sourceProvider">The <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> that provides the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> access to <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</param>
		/// <param name="compositionOptions">An object that specifies options that affect the behavior of the engine.</param>
		public ImportEngine(ExportProvider sourceProvider, CompositionOptions compositionOptions)
		{
			Requires.NotNull(sourceProvider, "sourceProvider");
			_compositionOptions = compositionOptions;
			_sourceProvider = sourceProvider;
			_sourceProvider.ExportsChanging += OnExportsChanging;
			_lock = new CompositionLock(compositionOptions.HasFlag(CompositionOptions.IsThreadSafe));
		}

		/// <summary>Previews all the required imports for the specified part to make sure that they can be satisfied, without actually setting them.</summary>
		/// <param name="part">The part to preview the imports of.</param>
		/// <param name="atomicComposition">The composition transaction to use, or <see langword="null" /> for no composition transaction.</param>
		public void PreviewImports(ComposablePart part, AtomicComposition atomicComposition)
		{
			ThrowIfDisposed();
			Requires.NotNull(part, "part");
			if (_compositionOptions.HasFlag(CompositionOptions.DisableSilentRejection))
			{
				return;
			}
			IDisposable compositionLockHolder = (_lock.IsThreadSafe ? _lock.LockComposition() : null);
			bool flag = compositionLockHolder != null;
			try
			{
				if (flag)
				{
					atomicComposition?.AddRevertAction(delegate
					{
						compositionLockHolder.Dispose();
					});
				}
				PartManager partManager = GetPartManager(part, createIfNotpresent: true);
				TryPreviewImportsStateMachine(partManager, part, atomicComposition).ThrowOnErrors(atomicComposition);
				StartSatisfyingImports(partManager, atomicComposition);
				if (flag)
				{
					atomicComposition?.AddCompleteAction(delegate
					{
						compositionLockHolder.Dispose();
					});
				}
			}
			finally
			{
				if (flag && atomicComposition == null)
				{
					compositionLockHolder.Dispose();
				}
			}
		}

		/// <summary>Satisfies the imports of the specified part.</summary>
		/// <param name="part">The part to satisfy the imports of.</param>
		public void SatisfyImports(ComposablePart part)
		{
			ThrowIfDisposed();
			Requires.NotNull(part, "part");
			PartManager partManager = GetPartManager(part, createIfNotpresent: true);
			if (partManager.State == ImportState.Composed)
			{
				return;
			}
			using (_lock.LockComposition())
			{
				TrySatisfyImports(partManager, part, shouldTrackImports: true).ThrowOnErrors();
			}
		}

		/// <summary>Satisfies the imports of the specified part without registering them for recomposition.</summary>
		/// <param name="part">The part to satisfy the imports of.</param>
		public void SatisfyImportsOnce(ComposablePart part)
		{
			ThrowIfDisposed();
			Requires.NotNull(part, "part");
			PartManager partManager = GetPartManager(part, createIfNotpresent: true);
			if (partManager.State == ImportState.Composed)
			{
				return;
			}
			using (_lock.LockComposition())
			{
				TrySatisfyImports(partManager, part, shouldTrackImports: false).ThrowOnErrors();
			}
		}

		/// <summary>Releases all the exports used to satisfy the imports of the specified part.</summary>
		/// <param name="part">The part to release the imports of.</param>
		/// <param name="atomicComposition">The composition transaction to use, or <see langword="null" /> for no composition transaction.</param>
		public void ReleaseImports(ComposablePart part, AtomicComposition atomicComposition)
		{
			ThrowIfDisposed();
			Requires.NotNull(part, "part");
			using (_lock.LockComposition())
			{
				PartManager partManager = GetPartManager(part, createIfNotpresent: false);
				if (partManager != null)
				{
					StopSatisfyingImports(partManager, atomicComposition);
				}
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> class.</summary>
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing || _isDisposed)
			{
				return;
			}
			bool flag = false;
			ExportProvider exportProvider = null;
			using (_lock.LockStateForWrite())
			{
				if (!_isDisposed)
				{
					exportProvider = _sourceProvider;
					_sourceProvider = null;
					_recompositionManager = null;
					_partManagers = null;
					_isDisposed = true;
					flag = true;
				}
			}
			if (exportProvider != null)
			{
				exportProvider.ExportsChanging -= OnExportsChanging;
			}
			if (flag)
			{
				_lock.Dispose();
			}
		}

		private CompositionResult TryPreviewImportsStateMachine(PartManager partManager, ComposablePart part, AtomicComposition atomicComposition)
		{
			CompositionResult result = CompositionResult.SucceededResult;
			if (partManager.State == ImportState.ImportsPreviewing)
			{
				return new CompositionResult(ErrorBuilder.CreatePartCycle(part));
			}
			if (partManager.State == ImportState.NoImportsSatisfied)
			{
				partManager.State = ImportState.ImportsPreviewing;
				IEnumerable<ImportDefinition> imports = part.ImportDefinitions.Where(IsRequiredImportForPreview);
				atomicComposition.AddRevertActionAllowNull(delegate
				{
					partManager.State = ImportState.NoImportsSatisfied;
				});
				result = result.MergeResult(TrySatisfyImportSubset(partManager, imports, atomicComposition));
				if (!result.Succeeded)
				{
					partManager.State = ImportState.NoImportsSatisfied;
					return result;
				}
				partManager.State = ImportState.ImportsPreviewed;
			}
			return result;
		}

		private CompositionResult TrySatisfyImportsStateMachine(PartManager partManager, ComposablePart part)
		{
			CompositionResult result = CompositionResult.SucceededResult;
			while (partManager.State < ImportState.Composed)
			{
				ImportState state = partManager.State;
				switch (partManager.State)
				{
				case ImportState.NoImportsSatisfied:
				case ImportState.ImportsPreviewed:
				{
					partManager.State = ImportState.PreExportImportsSatisfying;
					IEnumerable<ImportDefinition> imports2 = part.ImportDefinitions.Where((ImportDefinition import) => import.IsPrerequisite);
					result = result.MergeResult(TrySatisfyImportSubset(partManager, imports2, null));
					partManager.State = ImportState.PreExportImportsSatisfied;
					break;
				}
				case ImportState.PreExportImportsSatisfied:
				{
					partManager.State = ImportState.PostExportImportsSatisfying;
					IEnumerable<ImportDefinition> imports = part.ImportDefinitions.Where((ImportDefinition import) => !import.IsPrerequisite);
					result = result.MergeResult(TrySatisfyImportSubset(partManager, imports, null));
					partManager.State = ImportState.PostExportImportsSatisfied;
					break;
				}
				case ImportState.PostExportImportsSatisfied:
					partManager.State = ImportState.ComposedNotifying;
					partManager.ClearSavedImports();
					result = result.MergeResult(partManager.TryOnComposed());
					partManager.State = ImportState.Composed;
					break;
				case ImportState.ImportsPreviewing:
					return new CompositionResult(ErrorBuilder.CreatePartCycle(part));
				case ImportState.PreExportImportsSatisfying:
				case ImportState.PostExportImportsSatisfying:
					if (InPrerequisiteLoop())
					{
						return result.MergeError(ErrorBuilder.CreatePartCycle(part));
					}
					return result;
				case ImportState.ComposedNotifying:
					return result;
				}
				if (!result.Succeeded)
				{
					partManager.State = state;
					return result;
				}
			}
			return result;
		}

		private CompositionResult TrySatisfyImports(PartManager partManager, ComposablePart part, bool shouldTrackImports)
		{
			Assumes.NotNull(part);
			CompositionResult result = CompositionResult.SucceededResult;
			if (partManager.State == ImportState.Composed)
			{
				return result;
			}
			if (_recursionStateStack.Count >= 100)
			{
				return result.MergeError(ErrorBuilder.ComposeTookTooManyIterations(100));
			}
			_recursionStateStack.Push(partManager);
			try
			{
				result = result.MergeResult(TrySatisfyImportsStateMachine(partManager, part));
			}
			finally
			{
				_recursionStateStack.Pop();
			}
			if (shouldTrackImports)
			{
				StartSatisfyingImports(partManager, null);
			}
			return result;
		}

		private CompositionResult TrySatisfyImportSubset(PartManager partManager, IEnumerable<ImportDefinition> imports, AtomicComposition atomicComposition)
		{
			CompositionResult result = CompositionResult.SucceededResult;
			ComposablePart part = partManager.Part;
			foreach (ImportDefinition import in imports)
			{
				Export[] array = partManager.GetSavedImport(import);
				if (array == null)
				{
					CompositionResult<IEnumerable<Export>> compositionResult = TryGetExports(_sourceProvider, part, import, atomicComposition);
					if (!compositionResult.Succeeded)
					{
						result = result.MergeResult(compositionResult.ToResult());
						continue;
					}
					array = compositionResult.Value.AsArray();
				}
				if (atomicComposition == null)
				{
					result = result.MergeResult(partManager.TrySetImport(import, array));
				}
				else
				{
					partManager.SetSavedImport(import, array, atomicComposition);
				}
			}
			return result;
		}

		private void OnExportsChanging(object sender, ExportsChangeEventArgs e)
		{
			CompositionResult compositionResult = CompositionResult.SucceededResult;
			AtomicComposition atomicComposition = e.AtomicComposition;
			IEnumerable<PartManager> enumerable = _recompositionManager.GetAffectedParts(e.ChangedContractNames);
			if (atomicComposition != null && atomicComposition.TryGetValue<EngineContext>(this, out var value))
			{
				enumerable = enumerable.ConcatAllowingNull(value.GetAddedPartManagers()).Except(value.GetRemovedPartManagers());
			}
			IEnumerable<ExportDefinition> changedExports = e.AddedExports.ConcatAllowingNull(e.RemovedExports);
			foreach (PartManager item in enumerable)
			{
				compositionResult = compositionResult.MergeResult(TryRecomposeImports(item, changedExports, atomicComposition));
			}
			compositionResult.ThrowOnErrors(atomicComposition);
		}

		private CompositionResult TryRecomposeImports(PartManager partManager, IEnumerable<ExportDefinition> changedExports, AtomicComposition atomicComposition)
		{
			CompositionResult result = CompositionResult.SucceededResult;
			ImportState state = partManager.State;
			if (state != ImportState.ImportsPreviewed && state != ImportState.Composed)
			{
				return new CompositionResult(ErrorBuilder.InvalidStateForRecompposition(partManager.Part));
			}
			IEnumerable<ImportDefinition> affectedImports = RecompositionManager.GetAffectedImports(partManager.Part, changedExports);
			bool flag = partManager.State == ImportState.Composed;
			bool flag2 = false;
			foreach (ImportDefinition item in affectedImports)
			{
				result = result.MergeResult(TryRecomposeImport(partManager, flag, item, atomicComposition));
				flag2 = true;
			}
			if (result.Succeeded && flag2 && flag)
			{
				if (atomicComposition == null)
				{
					result = result.MergeResult(partManager.TryOnComposed());
				}
				else
				{
					atomicComposition.AddCompleteAction(delegate
					{
						partManager.TryOnComposed().ThrowOnErrors();
					});
				}
			}
			return result;
		}

		private CompositionResult TryRecomposeImport(PartManager partManager, bool partComposed, ImportDefinition import, AtomicComposition atomicComposition)
		{
			if (partComposed && !import.IsRecomposable)
			{
				return new CompositionResult(ErrorBuilder.PreventedByExistingImport(partManager.Part, import));
			}
			CompositionResult<IEnumerable<Export>> compositionResult = TryGetExports(_sourceProvider, partManager.Part, import, atomicComposition);
			if (!compositionResult.Succeeded)
			{
				return compositionResult.ToResult();
			}
			Export[] exports = compositionResult.Value.AsArray();
			if (partComposed)
			{
				if (atomicComposition == null)
				{
					return partManager.TrySetImport(import, exports);
				}
				atomicComposition.AddCompleteAction(delegate
				{
					partManager.TrySetImport(import, exports).ThrowOnErrors();
				});
			}
			else
			{
				partManager.SetSavedImport(import, exports, atomicComposition);
			}
			return CompositionResult.SucceededResult;
		}

		private void StartSatisfyingImports(PartManager partManager, AtomicComposition atomicComposition)
		{
			if (atomicComposition == null)
			{
				if (!partManager.TrackingImports)
				{
					partManager.TrackingImports = true;
					_recompositionManager.AddPartToIndex(partManager);
				}
			}
			else
			{
				GetEngineContext(atomicComposition).AddPartManager(partManager);
			}
		}

		private void StopSatisfyingImports(PartManager partManager, AtomicComposition atomicComposition)
		{
			if (atomicComposition == null)
			{
				_partManagers.Remove(partManager.Part);
				partManager.DisposeAllDependencies();
				if (partManager.TrackingImports)
				{
					partManager.TrackingImports = false;
					_recompositionManager.AddPartToUnindex(partManager);
				}
			}
			else
			{
				GetEngineContext(atomicComposition).RemovePartManager(partManager);
			}
		}

		private PartManager GetPartManager(ComposablePart part, bool createIfNotpresent)
		{
			PartManager value = null;
			using (_lock.LockStateForRead())
			{
				if (_partManagers.TryGetValue(part, out value))
				{
					return value;
				}
			}
			if (createIfNotpresent)
			{
				using (_lock.LockStateForWrite())
				{
					if (!_partManagers.TryGetValue(part, out value))
					{
						value = new PartManager(this, part);
						_partManagers.Add(part, value);
					}
				}
			}
			return value;
		}

		private EngineContext GetEngineContext(AtomicComposition atomicComposition)
		{
			Assumes.NotNull(atomicComposition);
			if (!atomicComposition.TryGetValue<EngineContext>(this, localAtomicCompositionOnly: true, out var value))
			{
				atomicComposition.TryGetValue<EngineContext>(this, localAtomicCompositionOnly: false, out var value2);
				value = new EngineContext(this, value2);
				atomicComposition.SetValue(this, value);
				atomicComposition.AddCompleteAction(value.Complete);
			}
			return value;
		}

		private bool InPrerequisiteLoop()
		{
			PartManager partManager = _recursionStateStack.First();
			PartManager partManager2 = null;
			foreach (PartManager item in _recursionStateStack.Skip(1))
			{
				if (item.State == ImportState.PreExportImportsSatisfying)
				{
					return true;
				}
				if (item == partManager)
				{
					partManager2 = item;
					break;
				}
			}
			Assumes.IsTrue(partManager2 == partManager);
			return false;
		}

		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}

		private static CompositionResult<IEnumerable<Export>> TryGetExports(ExportProvider provider, ComposablePart part, ImportDefinition definition, AtomicComposition atomicComposition)
		{
			try
			{
				return new CompositionResult<IEnumerable<Export>>(provider.GetExports(definition, atomicComposition).AsArray());
			}
			catch (ImportCardinalityMismatchException exception)
			{
				CompositionException innerException = new CompositionException(ErrorBuilder.CreateImportCardinalityMismatch(exception, definition));
				return new CompositionResult<IEnumerable<Export>>(ErrorBuilder.CreatePartCannotSetImport(part, definition, innerException));
			}
		}

		internal static bool IsRequiredImportForPreview(ImportDefinition import)
		{
			return import.Cardinality == ImportCardinality.ExactlyOne;
		}
	}
	internal static class ImportSourceImportDefinitionHelpers
	{
		internal class NonImportSourceImportDefinition : ContractBasedImportDefinition
		{
			private ContractBasedImportDefinition _sourceDefinition;

			private IDictionary<string, object> _metadata;

			public override string ContractName => _sourceDefinition.ContractName;

			public override IDictionary<string, object> Metadata
			{
				get
				{
					IDictionary<string, object> dictionary = _metadata;
					if (dictionary == null)
					{
						dictionary = new Dictionary<string, object>(_sourceDefinition.Metadata);
						dictionary.Remove("System.ComponentModel.Composition.ImportSource");
						_metadata = dictionary;
					}
					return dictionary;
				}
			}

			public override ImportCardinality Cardinality => _sourceDefinition.Cardinality;

			public override Expression<Func<ExportDefinition, bool>> Constraint => _sourceDefinition.Constraint;

			public override bool IsPrerequisite => _sourceDefinition.IsPrerequisite;

			public override bool IsRecomposable => _sourceDefinition.IsRecomposable;

			public override string RequiredTypeIdentity => _sourceDefinition.RequiredTypeIdentity;

			public override IEnumerable<KeyValuePair<string, Type>> RequiredMetadata => _sourceDefinition.RequiredMetadata;

			public override CreationPolicy RequiredCreationPolicy => _sourceDefinition.RequiredCreationPolicy;

			public NonImportSourceImportDefinition(ContractBasedImportDefinition sourceDefinition)
			{
				Assumes.NotNull(sourceDefinition);
				_sourceDefinition = sourceDefinition;
				_metadata = null;
			}

			public override bool IsConstraintSatisfiedBy(ExportDefinition exportDefinition)
			{
				Requires.NotNull(exportDefinition, "exportDefinition");
				return _sourceDefinition.IsConstraintSatisfiedBy(exportDefinition);
			}

			public override string ToString()
			{
				return _sourceDefinition.ToString();
			}
		}

		public static ImportDefinition RemoveImportSource(this ImportDefinition definition)
		{
			if (!(definition is ContractBasedImportDefinition sourceDefinition))
			{
				return definition;
			}
			return new NonImportSourceImportDefinition(sourceDefinition);
		}
	}
	/// <summary>Defines static convenience methods for scoping.</summary>
	public static class ScopingExtensions
	{
		/// <summary>Gets a value that indicates whether the specified part exports the specified contract.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="contractName">The name of the contract.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> exports the specified contract; otherwise, <see langword="false" />.</returns>
		public static bool Exports(this ComposablePartDefinition part, string contractName)
		{
			Requires.NotNull(part, "part");
			Requires.NotNull(contractName, "contractName");
			foreach (ExportDefinition exportDefinition in part.ExportDefinitions)
			{
				if (StringComparers.ContractName.Equals(contractName, exportDefinition.ContractName))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the specified part imports the specified contract.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="contractName">The name of the contract.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> imports the specified contract; otherwise, <see langword="false" />.</returns>
		public static bool Imports(this ComposablePartDefinition part, string contractName)
		{
			Requires.NotNull(part, "part");
			Requires.NotNull(contractName, "contractName");
			foreach (ImportDefinition importDefinition in part.ImportDefinitions)
			{
				if (StringComparers.ContractName.Equals(contractName, importDefinition.ContractName))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the specified part imports the specified contract with the specified cardinality.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="contractName">The name of the contract.</param>
		/// <param name="importCardinality">The cardinality of the contract.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> imports a contract that has the specified name and cardinality; otherwise, <see langword="false" />.</returns>
		public static bool Imports(this ComposablePartDefinition part, string contractName, ImportCardinality importCardinality)
		{
			Requires.NotNull(part, "part");
			Requires.NotNull(contractName, "contractName");
			foreach (ImportDefinition importDefinition in part.ImportDefinitions)
			{
				if (StringComparers.ContractName.Equals(contractName, importDefinition.ContractName) && importDefinition.Cardinality == importCardinality)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets a value that indicates whether the specified part contains metadata that has the specified key.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="key">The metadata key.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains metadata that has the specified key; otherwise, <see langword="false" />.</returns>
		public static bool ContainsPartMetadataWithKey(this ComposablePartDefinition part, string key)
		{
			Requires.NotNull(part, "part");
			Requires.NotNull(key, "key");
			return part.Metadata.ContainsKey(key);
		}

		/// <summary>Gets a value that indicates whether the specified part contains metadata that has the specified key and value.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="key">The metadata key.</param>
		/// <param name="value">The metadata value.</param>
		/// <typeparam name="T">The type of the metadata value.</typeparam>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains metadata that has the specified key, value type, and value; otherwise, <see langword="false" />.</returns>
		public static bool ContainsPartMetadata<T>(this ComposablePartDefinition part, string key, T value)
		{
			Requires.NotNull(part, "part");
			Requires.NotNull(key, "key");
			object value2 = null;
			if (part.Metadata.TryGetValue(key, out value2))
			{
				return value?.Equals(value2) ?? (value2 == null);
			}
			return false;
		}

		/// <summary>Filters the specified catalog with the specified filter function.</summary>
		/// <param name="catalog">The catalog to filter.</param>
		/// <param name="filter">The filter function.</param>
		/// <returns>A new catalog filtered by using the specified filter.</returns>
		public static FilteredCatalog Filter(this ComposablePartCatalog catalog, Func<ComposablePartDefinition, bool> filter)
		{
			Requires.NotNull(catalog, "catalog");
			Requires.NotNull(filter, "filter");
			return new FilteredCatalog(catalog, filter);
		}
	}
	/// <summary>Discovers attributed parts from a collection of types.</summary>
	[DebuggerTypeProxy(typeof(ComposablePartCatalogDebuggerProxy))]
	public class TypeCatalog : ComposablePartCatalog, ICompositionElement
	{
		private readonly object _thisLock = new object();

		private Type[] _types;

		private volatile List<ComposablePartDefinition> _parts;

		private volatile bool _isDisposed;

		private readonly ICompositionElement _definitionOrigin;

		private readonly Lazy<IDictionary<string, List<ComposablePartDefinition>>> _contractPartIndex;

		/// <summary>Gets the display name of the type catalog.</summary>
		/// <returns>A string containing a human-readable display name of the <see cref="T:System.ComponentModel.Composition.Hosting.TypeCatalog" />.</returns>
		string ICompositionElement.DisplayName => GetDisplayName();

		/// <summary>Gets the composition element from which the type catalog originated.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		ICompositionElement ICompositionElement.Origin => null;

		private IEnumerable<ComposablePartDefinition> PartsInternal
		{
			get
			{
				if (_parts == null)
				{
					lock (_thisLock)
					{
						if (_parts == null)
						{
							Assumes.NotNull(_types);
							List<ComposablePartDefinition> list = new List<ComposablePartDefinition>();
							Type[] types = _types;
							for (int i = 0; i < types.Length; i++)
							{
								ComposablePartDefinition composablePartDefinition = AttributedModelDiscovery.CreatePartDefinitionIfDiscoverable(types[i], _definitionOrigin);
								if (composablePartDefinition != null)
								{
									list.Add(composablePartDefinition);
								}
							}
							Thread.MemoryBarrier();
							_types = null;
							_parts = list;
						}
					}
				}
				return _parts;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.TypeCatalog" /> class with the specified types.</summary>
		/// <param name="types">An array of attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.TypeCatalog" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> contains an element that is <see langword="null" />.  
		/// -or-  
		/// <paramref name="types" /> contains an element that was loaded in the reflection-only context.</exception>
		public TypeCatalog(params Type[] types)
			: this((IEnumerable<Type>)types)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.TypeCatalog" /> class with the specified types.</summary>
		/// <param name="types">A collection of attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.TypeCatalog" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> contains an element that is <see langword="null" />.  
		/// -or-  
		/// <paramref name="types" /> contains an element that was loaded in the reflection-only context.</exception>
		public TypeCatalog(IEnumerable<Type> types)
		{
			Requires.NotNull(types, "types");
			InitializeTypeCatalog(types);
			_definitionOrigin = this;
			_contractPartIndex = new Lazy<IDictionary<string, List<ComposablePartDefinition>>>(CreateIndex, isThreadSafe: true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.TypeCatalog" /> class with the specified types and source for parts.</summary>
		/// <param name="types">A collection of attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.TypeCatalog" /> object.</param>
		/// <param name="definitionOrigin">An element used by diagnostics to identify the source for parts.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> contains an element that is <see langword="null" />.  
		/// -or-  
		/// <paramref name="types" /> contains an element that was loaded in the reflection-only context.</exception>
		public TypeCatalog(IEnumerable<Type> types, ICompositionElement definitionOrigin)
		{
			Requires.NotNull(types, "types");
			Requires.NotNull(definitionOrigin, "definitionOrigin");
			InitializeTypeCatalog(types);
			_definitionOrigin = definitionOrigin;
			_contractPartIndex = new Lazy<IDictionary<string, List<ComposablePartDefinition>>>(CreateIndex, isThreadSafe: true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.TypeCatalog" /> class with the specified types in the specified reflection context.</summary>
		/// <param name="types">A collection of attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.TypeCatalog" /> object.</param>
		/// <param name="reflectionContext">The context used to interpret the types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> contains an element that is <see langword="null" />.  
		/// -or-  
		/// <paramref name="types" /> contains an element that was loaded in the reflection-only context.</exception>
		public TypeCatalog(IEnumerable<Type> types, ReflectionContext reflectionContext)
		{
			Requires.NotNull(types, "types");
			Requires.NotNull(reflectionContext, "reflectionContext");
			InitializeTypeCatalog(types, reflectionContext);
			_definitionOrigin = this;
			_contractPartIndex = new Lazy<IDictionary<string, List<ComposablePartDefinition>>>(CreateIndex, isThreadSafe: true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.TypeCatalog" /> class with the specified types in the specified reflection context and source for parts.</summary>
		/// <param name="types">A collection of attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.TypeCatalog" /> object.</param>
		/// <param name="reflectionContext">The context used to interpret the types.</param>
		/// <param name="definitionOrigin">An element used by diagnostics to identify the source for parts.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> contains an element that is <see langword="null" />.  
		/// -or-  
		/// <paramref name="types" /> contains an element that was loaded in the reflection-only context.</exception>
		public TypeCatalog(IEnumerable<Type> types, ReflectionContext reflectionContext, ICompositionElement definitionOrigin)
		{
			Requires.NotNull(types, "types");
			Requires.NotNull(reflectionContext, "reflectionContext");
			Requires.NotNull(definitionOrigin, "definitionOrigin");
			InitializeTypeCatalog(types, reflectionContext);
			_definitionOrigin = definitionOrigin;
			_contractPartIndex = new Lazy<IDictionary<string, List<ComposablePartDefinition>>>(CreateIndex, isThreadSafe: true);
		}

		private void InitializeTypeCatalog(IEnumerable<Type> types, ReflectionContext reflectionContext)
		{
			List<Type> list = new List<Type>();
			foreach (Type type in types)
			{
				if (type == null)
				{
					throw ExceptionBuilder.CreateContainsNullElement("types");
				}
				if (type.Assembly.ReflectionOnly)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.Argument_ElementReflectionOnlyType, "types"), "types");
				}
				TypeInfo typeInfo = type.GetTypeInfo();
				TypeInfo typeInfo2 = ((reflectionContext != null) ? reflectionContext.MapType(typeInfo) : typeInfo);
				if (typeInfo2 != null)
				{
					if (typeInfo2.Assembly.ReflectionOnly)
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.Argument_ReflectionContextReturnsReflectionOnlyType, "reflectionContext"), "reflectionContext");
					}
					list.Add(typeInfo2);
				}
			}
			_types = list.ToArray();
		}

		private void InitializeTypeCatalog(IEnumerable<Type> types)
		{
			foreach (Type type in types)
			{
				if (type == null)
				{
					throw ExceptionBuilder.CreateContainsNullElement("types");
				}
			}
			_types = types.ToArray();
		}

		/// <summary>Returns an enumerator that iterates through the catalog.</summary>
		/// <returns>An enumerator that can be used to iterate through the catalog.</returns>
		public override IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			ThrowIfDisposed();
			return PartsInternal.GetEnumerator();
		}

		internal override IEnumerable<ComposablePartDefinition> GetCandidateParts(ImportDefinition definition)
		{
			Assumes.NotNull(definition);
			string contractName = definition.ContractName;
			if (string.IsNullOrEmpty(contractName))
			{
				return PartsInternal;
			}
			string value = definition.Metadata.GetValue<string>("System.ComponentModel.Composition.GenericContractName");
			List<ComposablePartDefinition> candidateParts = GetCandidateParts(contractName);
			List<ComposablePartDefinition> candidateParts2 = GetCandidateParts(value);
			return candidateParts.ConcatAllowingNull(candidateParts2);
		}

		private List<ComposablePartDefinition> GetCandidateParts(string contractName)
		{
			if (contractName == null)
			{
				return null;
			}
			List<ComposablePartDefinition> value = null;
			_contractPartIndex.Value.TryGetValue(contractName, out value);
			return value;
		}

		private IDictionary<string, List<ComposablePartDefinition>> CreateIndex()
		{
			Dictionary<string, List<ComposablePartDefinition>> dictionary = new Dictionary<string, List<ComposablePartDefinition>>(StringComparers.ContractName);
			foreach (ComposablePartDefinition item in PartsInternal)
			{
				foreach (string item2 in item.ExportDefinitions.Select((ExportDefinition export) => export.ContractName).Distinct())
				{
					List<ComposablePartDefinition> value = null;
					if (!dictionary.TryGetValue(item2, out value))
					{
						value = new List<ComposablePartDefinition>();
						dictionary.Add(item2, value);
					}
					value.Add(item);
				}
			}
			return dictionary;
		}

		/// <summary>Returns a string representation of the type catalog.</summary>
		/// <returns>A string representation of the type catalog.</returns>
		public override string ToString()
		{
			return GetDisplayName();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.TypeCatalog" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_isDisposed = true;
			}
			base.Dispose(disposing);
		}

		private string GetDisplayName()
		{
			return string.Format(CultureInfo.CurrentCulture, Strings.TypeCatalog_DisplayNameFormat, GetType().Name, GetTypesDisplay());
		}

		private string GetTypesDisplay()
		{
			int num = PartsInternal.Count();
			if (num == 0)
			{
				return Strings.TypeCatalog_Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ReflectionComposablePartDefinition item in PartsInternal.Take(2))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(CultureInfo.CurrentCulture.TextInfo.ListSeparator);
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(item.GetPartType().GetDisplayName());
			}
			if (num > 2)
			{
				stringBuilder.Append(CultureInfo.CurrentCulture.TextInfo.ListSeparator);
				stringBuilder.Append(" ...");
			}
			return stringBuilder.ToString();
		}

		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (_isDisposed)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}
	}
}
namespace System.ComponentModel.Composition.Diagnostics
{
	internal static class CompositionTrace
	{
		internal static void PartDefinitionResurrected(ComposablePartDefinition definition)
		{
			Assumes.NotNull(definition);
			if (CompositionTraceSource.CanWriteInformation)
			{
				CompositionTraceSource.WriteInformation(CompositionTraceId.Rejection_DefinitionResurrected, Strings.CompositionTrace_Rejection_DefinitionResurrected, definition.GetDisplayName());
			}
		}

		internal static void PartDefinitionRejected(ComposablePartDefinition definition, ChangeRejectedException exception)
		{
			Assumes.NotNull(definition, exception);
			if (CompositionTraceSource.CanWriteWarning)
			{
				CompositionTraceSource.WriteWarning(CompositionTraceId.Rejection_DefinitionRejected, Strings.CompositionTrace_Rejection_DefinitionRejected, definition.GetDisplayName(), exception.Message);
			}
		}

		internal static void AssemblyLoadFailed(DirectoryCatalog catalog, string fileName, Exception exception)
		{
			Assumes.NotNull(catalog, exception);
			Assumes.NotNullOrEmpty(fileName);
			if (CompositionTraceSource.CanWriteWarning)
			{
				CompositionTraceSource.WriteWarning(CompositionTraceId.Discovery_AssemblyLoadFailed, Strings.CompositionTrace_Discovery_AssemblyLoadFailed, catalog.GetDisplayName(), fileName, exception.Message);
			}
		}

		internal static void DefinitionMarkedWithPartNotDiscoverableAttribute(Type type)
		{
			Assumes.NotNull(type);
			if (CompositionTraceSource.CanWriteInformation)
			{
				CompositionTraceSource.WriteInformation(CompositionTraceId.Discovery_DefinitionMarkedWithPartNotDiscoverableAttribute, Strings.CompositionTrace_Discovery_DefinitionMarkedWithPartNotDiscoverableAttribute, type.GetDisplayName());
			}
		}

		internal static void DefinitionMismatchedExportArity(Type type, MemberInfo member)
		{
			Assumes.NotNull(type);
			Assumes.NotNull(member);
			if (CompositionTraceSource.CanWriteInformation)
			{
				CompositionTraceSource.WriteInformation(CompositionTraceId.Discovery_DefinitionMismatchedExportArity, Strings.CompositionTrace_Discovery_DefinitionMismatchedExportArity, type.GetDisplayName(), member.GetDisplayName());
			}
		}

		internal static void DefinitionContainsNoExports(Type type)
		{
			Assumes.NotNull(type);
			if (CompositionTraceSource.CanWriteInformation)
			{
				CompositionTraceSource.WriteInformation(CompositionTraceId.Discovery_DefinitionContainsNoExports, Strings.CompositionTrace_Discovery_DefinitionContainsNoExports, type.GetDisplayName());
			}
		}

		internal static void MemberMarkedWithMultipleImportAndImportMany(ReflectionItem item)
		{
			Assumes.NotNull(item);
			if (CompositionTraceSource.CanWriteError)
			{
				CompositionTraceSource.WriteError(CompositionTraceId.Discovery_MemberMarkedWithMultipleImportAndImportMany, Strings.CompositionTrace_Discovery_MemberMarkedWithMultipleImportAndImportMany, item.GetDisplayName());
			}
		}
	}
	internal enum CompositionTraceId : ushort
	{
		Rejection_DefinitionRejected = 1,
		Rejection_DefinitionResurrected,
		Discovery_AssemblyLoadFailed,
		Discovery_DefinitionMarkedWithPartNotDiscoverableAttribute,
		Discovery_DefinitionMismatchedExportArity,
		Discovery_DefinitionContainsNoExports,
		Discovery_MemberMarkedWithMultipleImportAndImportMany
	}
	internal static class CompositionTraceSource
	{
		private static readonly DebuggerTraceWriter Source = new DebuggerTraceWriter();

		public static bool CanWriteInformation => Source.CanWriteInformation;

		public static bool CanWriteWarning => Source.CanWriteWarning;

		public static bool CanWriteError => Source.CanWriteError;

		public static void WriteInformation(CompositionTraceId traceId, string format, params object[] arguments)
		{
			EnsureEnabled(CanWriteInformation);
			Source.WriteInformation(traceId, format, arguments);
		}

		public static void WriteWarning(CompositionTraceId traceId, string format, params object[] arguments)
		{
			EnsureEnabled(CanWriteWarning);
			Source.WriteWarning(traceId, format, arguments);
		}

		public static void WriteError(CompositionTraceId traceId, string format, params object[] arguments)
		{
			EnsureEnabled(CanWriteError);
			Source.WriteError(traceId, format, arguments);
		}

		private static void EnsureEnabled(bool condition)
		{
			Assumes.IsTrue(condition, "To avoid unnecessary work when a trace level has not been enabled, check CanWriteXXX before calling this method.");
		}
	}
	internal sealed class DebuggerTraceWriter : TraceWriter
	{
		internal enum TraceEventType
		{
			Error = 2,
			Warning = 4,
			Information = 8
		}

		private static readonly string SourceName = "System.ComponentModel.Composition";

		public override bool CanWriteInformation => false;

		public override bool CanWriteWarning => Debugger.IsLogging();

		public override bool CanWriteError => Debugger.IsLogging();

		public override void WriteInformation(CompositionTraceId traceId, string format, params object[] arguments)
		{
			WriteEvent(TraceEventType.Information, traceId, format, arguments);
		}

		public override void WriteWarning(CompositionTraceId traceId, string format, params object[] arguments)
		{
			WriteEvent(TraceEventType.Warning, traceId, format, arguments);
		}

		public override void WriteError(CompositionTraceId traceId, string format, params object[] arguments)
		{
			WriteEvent(TraceEventType.Error, traceId, format, arguments);
		}

		private static void WriteEvent(TraceEventType eventType, CompositionTraceId traceId, string format, params object[] arguments)
		{
			if (Debugger.IsLogging())
			{
				string message = CreateLogMessage(eventType, traceId, format, arguments);
				Debugger.Log(0, null, message);
			}
		}

		internal static string CreateLogMessage(TraceEventType eventType, CompositionTraceId traceId, string format, params object[] arguments)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} {1}: {2} : ", SourceName, eventType.ToString(), (int)traceId);
			if (arguments == null)
			{
				stringBuilder.Append(format);
			}
			else
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, format, arguments);
			}
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}
	}
	internal abstract class TraceWriter
	{
		public abstract bool CanWriteInformation { get; }

		public abstract bool CanWriteWarning { get; }

		public abstract bool CanWriteError { get; }

		public abstract void WriteInformation(CompositionTraceId traceId, string format, params object[] arguments);

		public abstract void WriteWarning(CompositionTraceId traceId, string format, params object[] arguments);

		public abstract void WriteError(CompositionTraceId traceId, string format, params object[] arguments);
	}
}
namespace System.ComponentModel.Composition.AttributedModel
{
	internal class AttributedExportDefinition : ExportDefinition
	{
		private readonly AttributedPartCreationInfo _partCreationInfo;

		private readonly MemberInfo _member;

		private readonly ExportAttribute _exportAttribute;

		private readonly Type _typeIdentityType;

		private IDictionary<string, object> _metadata;

		public override IDictionary<string, object> Metadata
		{
			get
			{
				if (_metadata == null)
				{
					_member.TryExportMetadataForMember(out var dictionary);
					string value = (_exportAttribute.IsContractNameSameAsTypeIdentity() ? ContractName : _member.GetTypeIdentityFromExport(_typeIdentityType));
					dictionary.Add("ExportTypeIdentity", value);
					IDictionary<string, object> metadata = _partCreationInfo.GetMetadata();
					if (metadata != null && metadata.ContainsKey("System.ComponentModel.Composition.CreationPolicy"))
					{
						dictionary.Add("System.ComponentModel.Composition.CreationPolicy", metadata["System.ComponentModel.Composition.CreationPolicy"]);
					}
					if (_typeIdentityType != null && _member.MemberType != MemberTypes.Method && _typeIdentityType.ContainsGenericParameters)
					{
						dictionary.Add("System.ComponentModel.Composition.GenericExportParametersOrderMetadataName", GenericServices.GetGenericParametersOrder(_typeIdentityType));
					}
					_metadata = dictionary;
				}
				return _metadata;
			}
		}

		public AttributedExportDefinition(AttributedPartCreationInfo partCreationInfo, MemberInfo member, ExportAttribute exportAttribute, Type typeIdentityType, string contractName)
			: base(contractName, null)
		{
			Assumes.NotNull(partCreationInfo);
			Assumes.NotNull(member);
			Assumes.NotNull(exportAttribute);
			_partCreationInfo = partCreationInfo;
			_member = member;
			_exportAttribute = exportAttribute;
			_typeIdentityType = typeIdentityType;
		}
	}
	internal static class AttributedModelDiscovery
	{
		public static ComposablePartDefinition CreatePartDefinitionIfDiscoverable(Type type, ICompositionElement origin)
		{
			AttributedPartCreationInfo attributedPartCreationInfo = new AttributedPartCreationInfo(type, null, ignoreConstructorImports: false, origin);
			if (!attributedPartCreationInfo.IsPartDiscoverable())
			{
				return null;
			}
			return new ReflectionComposablePartDefinition(attributedPartCreationInfo);
		}

		public static ReflectionComposablePartDefinition CreatePartDefinition(Type type, PartCreationPolicyAttribute partCreationPolicy, bool ignoreConstructorImports, ICompositionElement origin)
		{
			Assumes.NotNull(type);
			return new ReflectionComposablePartDefinition(new AttributedPartCreationInfo(type, partCreationPolicy, ignoreConstructorImports, origin));
		}

		public static ReflectionComposablePart CreatePart(object attributedPart)
		{
			Assumes.NotNull(attributedPart);
			return new ReflectionComposablePart(CreatePartDefinition(attributedPart.GetType(), PartCreationPolicyAttribute.Shared, ignoreConstructorImports: true, null), attributedPart);
		}

		public static ReflectionComposablePart CreatePart(object attributedPart, ReflectionContext reflectionContext)
		{
			Assumes.NotNull(attributedPart);
			Assumes.NotNull(reflectionContext);
			TypeInfo typeInfo = reflectionContext.MapType(attributedPart.GetType().GetTypeInfo());
			if (typeInfo.Assembly.ReflectionOnly)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.Argument_ReflectionContextReturnsReflectionOnlyType, "reflectionContext"), "reflectionContext");
			}
			return CreatePart(CreatePartDefinition(typeInfo, PartCreationPolicyAttribute.Shared, ignoreConstructorImports: true, null), attributedPart);
		}

		public static ReflectionComposablePart CreatePart(ComposablePartDefinition partDefinition, object attributedPart)
		{
			Assumes.NotNull(partDefinition);
			Assumes.NotNull(attributedPart);
			return new ReflectionComposablePart((ReflectionComposablePartDefinition)partDefinition, attributedPart);
		}

		public static ReflectionParameterImportDefinition CreateParameterImportDefinition(ParameterInfo parameter, ICompositionElement origin)
		{
			Requires.NotNull(parameter, "parameter");
			ReflectionParameter reflectionParameter = parameter.ToReflectionParameter();
			IAttributedImport attributedImport = GetAttributedImport(reflectionParameter, parameter);
			ImportType importType = new ImportType(reflectionParameter.ReturnType, attributedImport.Cardinality);
			if (importType.IsPartCreator)
			{
				return new PartCreatorParameterImportDefinition(new Lazy<ParameterInfo>(() => parameter), origin, new ContractBasedImportDefinition(attributedImport.GetContractNameFromImport(importType), attributedImport.GetTypeIdentityFromImport(importType), CompositionServices.GetRequiredMetadata(importType.MetadataViewType), attributedImport.Cardinality, isRecomposable: false, isPrerequisite: true, (attributedImport.RequiredCreationPolicy != CreationPolicy.NewScope) ? CreationPolicy.NonShared : CreationPolicy.NewScope, CompositionServices.GetImportMetadata(importType, attributedImport)));
			}
			if (attributedImport.RequiredCreationPolicy == CreationPolicy.NewScope)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.InvalidPartCreationPolicyOnImport, attributedImport.RequiredCreationPolicy), origin);
			}
			return new ReflectionParameterImportDefinition(new Lazy<ParameterInfo>(() => parameter), attributedImport.GetContractNameFromImport(importType), attributedImport.GetTypeIdentityFromImport(importType), CompositionServices.GetRequiredMetadata(importType.MetadataViewType), attributedImport.Cardinality, attributedImport.RequiredCreationPolicy, CompositionServices.GetImportMetadata(importType, attributedImport), origin);
		}

		public static ReflectionMemberImportDefinition CreateMemberImportDefinition(MemberInfo member, ICompositionElement origin)
		{
			Requires.NotNull(member, "member");
			ReflectionWritableMember reflectionWritableMember = member.ToReflectionWritableMember();
			IAttributedImport attributedImport = GetAttributedImport(reflectionWritableMember, member);
			ImportType importType = new ImportType(reflectionWritableMember.ReturnType, attributedImport.Cardinality);
			if (importType.IsPartCreator)
			{
				return new PartCreatorMemberImportDefinition(new LazyMemberInfo(member), origin, new ContractBasedImportDefinition(attributedImport.GetContractNameFromImport(importType), attributedImport.GetTypeIdentityFromImport(importType), CompositionServices.GetRequiredMetadata(importType.MetadataViewType), attributedImport.Cardinality, attributedImport.AllowRecomposition, isPrerequisite: false, (attributedImport.RequiredCreationPolicy != CreationPolicy.NewScope) ? CreationPolicy.NonShared : CreationPolicy.NewScope, CompositionServices.GetImportMetadata(importType, attributedImport)));
			}
			if (attributedImport.RequiredCreationPolicy == CreationPolicy.NewScope)
			{
				throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.InvalidPartCreationPolicyOnImport, attributedImport.RequiredCreationPolicy), origin);
			}
			bool isPrerequisite = member.GetAttributes<ExportAttribute>().Length != 0;
			return new ReflectionMemberImportDefinition(new LazyMemberInfo(member), attributedImport.GetContractNameFromImport(importType), attributedImport.GetTypeIdentityFromImport(importType), CompositionServices.GetRequiredMetadata(importType.MetadataViewType), attributedImport.Cardinality, attributedImport.AllowRecomposition, isPrerequisite, attributedImport.RequiredCreationPolicy, CompositionServices.GetImportMetadata(importType, attributedImport), origin);
		}

		private static IAttributedImport GetAttributedImport(ReflectionItem item, ICustomAttributeProvider attributeProvider)
		{
			IAttributedImport[] attributes = attributeProvider.GetAttributes<IAttributedImport>(inherit: false);
			if (attributes.Length == 0)
			{
				return new ImportAttribute();
			}
			if (attributes.Length > 1)
			{
				CompositionTrace.MemberMarkedWithMultipleImportAndImportMany(item);
			}
			return attributes[0];
		}
	}
	internal class AttributedPartCreationInfo : IReflectionPartCreationInfo, ICompositionElement
	{
		private readonly Type _type;

		private readonly bool _ignoreConstructorImports;

		private readonly ICompositionElement _origin;

		private PartCreationPolicyAttribute _partCreationPolicy;

		private ConstructorInfo _constructor;

		private IEnumerable<ExportDefinition> _exports;

		private IEnumerable<ImportDefinition> _imports;

		private HashSet<string> _contractNamesOnNonInterfaces;

		public bool IsDisposalRequired => typeof(IDisposable).IsAssignableFrom(GetPartType());

		string ICompositionElement.DisplayName => GetDisplayName();

		ICompositionElement ICompositionElement.Origin => _origin;

		private CreationPolicy CreationPolicy
		{
			get
			{
				if (_partCreationPolicy == null)
				{
					_partCreationPolicy = _type.GetFirstAttribute<PartCreationPolicyAttribute>() ?? PartCreationPolicyAttribute.Default;
				}
				if (_partCreationPolicy.CreationPolicy == CreationPolicy.NewScope)
				{
					throw new ComposablePartException(string.Format(CultureInfo.CurrentCulture, Strings.InvalidPartCreationPolicyOnPart, _partCreationPolicy.CreationPolicy), _origin);
				}
				return _partCreationPolicy.CreationPolicy;
			}
		}

		public AttributedPartCreationInfo(Type type, PartCreationPolicyAttribute partCreationPolicy, bool ignoreConstructorImports, ICompositionElement origin)
		{
			Assumes.NotNull(type);
			_type = type;
			_ignoreConstructorImports = ignoreConstructorImports;
			_partCreationPolicy = partCreationPolicy;
			_origin = origin;
		}

		public Type GetPartType()
		{
			return _type;
		}

		public Lazy<Type> GetLazyPartType()
		{
			return new Lazy<Type>(GetPartType, LazyThreadSafetyMode.PublicationOnly);
		}

		public ConstructorInfo GetConstructor()
		{
			if (_constructor == null && !_ignoreConstructorImports)
			{
				_constructor = SelectPartConstructor(_type);
			}
			return _constructor;
		}

		public IDictionary<string, object> GetMetadata()
		{
			return _type.GetPartMetadataForType(CreationPolicy);
		}

		public IEnumerable<ExportDefinition> GetExports()
		{
			DiscoverExportsAndImports();
			return _exports;
		}

		public IEnumerable<ImportDefinition> GetImports()
		{
			DiscoverExportsAndImports();
			return _imports;
		}

		public bool IsPartDiscoverable()
		{
			if (_type.IsAttributeDefined<PartNotDiscoverableAttribute>())
			{
				CompositionTrace.DefinitionMarkedWithPartNotDiscoverableAttribute(_type);
				return false;
			}
			if (!HasExports())
			{
				CompositionTrace.DefinitionContainsNoExports(_type);
				return false;
			}
			if (!AllExportsHaveMatchingArity())
			{
				return false;
			}
			return true;
		}

		private bool HasExports()
		{
			if (!GetExportMembers(_type).Any())
			{
				return GetInheritedExports(_type).Any();
			}
			return true;
		}

		private bool AllExportsHaveMatchingArity()
		{
			bool result = true;
			if (_type.ContainsGenericParameters)
			{
				int pureGenericArity = _type.GetPureGenericArity();
				foreach (MemberInfo item in GetExportMembers(_type).Concat(GetInheritedExports(_type)))
				{
					if (item.MemberType == MemberTypes.Method && ((MethodInfo)item).ContainsGenericParameters)
					{
						result = false;
						CompositionTrace.DefinitionMismatchedExportArity(_type, item);
					}
					else if (item.GetDefaultTypeFromMember().GetPureGenericArity() != pureGenericArity)
					{
						result = false;
						CompositionTrace.DefinitionMismatchedExportArity(_type, item);
					}
				}
			}
			return result;
		}

		public override string ToString()
		{
			return GetDisplayName();
		}

		private string GetDisplayName()
		{
			return GetPartType().GetDisplayName();
		}

		private static ConstructorInfo SelectPartConstructor(Type type)
		{
			Assumes.NotNull(type);
			if (type.IsAbstract)
			{
				return null;
			}
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			ConstructorInfo[] constructors = type.GetConstructors(bindingAttr);
			if (constructors.Length == 0)
			{
				return null;
			}
			if (constructors.Length == 1 && constructors[0].GetParameters().Length == 0)
			{
				return constructors[0];
			}
			ConstructorInfo constructorInfo = null;
			ConstructorInfo constructorInfo2 = null;
			ConstructorInfo[] array = constructors;
			foreach (ConstructorInfo constructorInfo3 in array)
			{
				if (constructorInfo3.IsAttributeDefined<ImportingConstructorAttribute>())
				{
					if (constructorInfo != null)
					{
						return null;
					}
					constructorInfo = constructorInfo3;
				}
				else if (constructorInfo2 == null && constructorInfo3.GetParameters().Length == 0)
				{
					constructorInfo2 = constructorInfo3;
				}
			}
			return constructorInfo ?? constructorInfo2;
		}

		private void DiscoverExportsAndImports()
		{
			if (_exports == null || _imports == null)
			{
				_exports = GetExportDefinitions();
				_imports = GetImportDefinitions();
			}
		}

		private IEnumerable<ExportDefinition> GetExportDefinitions()
		{
			List<ExportDefinition> list = new List<ExportDefinition>();
			_contractNamesOnNonInterfaces = new HashSet<string>();
			foreach (MemberInfo exportMember in GetExportMembers(_type))
			{
				ExportAttribute[] attributes = exportMember.GetAttributes<ExportAttribute>();
				foreach (ExportAttribute exportAttribute in attributes)
				{
					AttributedExportDefinition attributedExportDefinition = CreateExportDefinition(exportMember, exportAttribute);
					if (exportAttribute.GetType() == CompositionServices.InheritedExportAttributeType)
					{
						if (!_contractNamesOnNonInterfaces.Contains(attributedExportDefinition.ContractName))
						{
							list.Add(new ReflectionMemberExportDefinition(exportMember.ToLazyMember(), attributedExportDefinition, this));
							_contractNamesOnNonInterfaces.Add(attributedExportDefinition.ContractName);
						}
					}
					else
					{
						list.Add(new ReflectionMemberExportDefinition(exportMember.ToLazyMember(), attributedExportDefinition, this));
					}
				}
			}
			foreach (Type inheritedExport in GetInheritedExports(_type))
			{
				InheritedExportAttribute[] attributes2 = inheritedExport.GetAttributes<InheritedExportAttribute>();
				foreach (InheritedExportAttribute exportAttribute2 in attributes2)
				{
					AttributedExportDefinition attributedExportDefinition2 = CreateExportDefinition(inheritedExport, exportAttribute2);
					if (!_contractNamesOnNonInterfaces.Contains(attributedExportDefinition2.ContractName))
					{
						list.Add(new ReflectionMemberExportDefinition(inheritedExport.ToLazyMember(), attributedExportDefinition2, this));
						if (!inheritedExport.IsInterface)
						{
							_contractNamesOnNonInterfaces.Add(attributedExportDefinition2.ContractName);
						}
					}
				}
			}
			_contractNamesOnNonInterfaces = null;
			return list;
		}

		private AttributedExportDefinition CreateExportDefinition(MemberInfo member, ExportAttribute exportAttribute)
		{
			string contractName = null;
			Type typeIdentityType = null;
			member.GetContractInfoFromExport(exportAttribute, out typeIdentityType, out contractName);
			return new AttributedExportDefinition(this, member, exportAttribute, typeIdentityType, contractName);
		}

		private IEnumerable<MemberInfo> GetExportMembers(Type type)
		{
			BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			if (type.IsAbstract)
			{
				flags &= ~BindingFlags.Instance;
			}
			else if (IsExport(type))
			{
				yield return type;
			}
			FieldInfo[] fields = type.GetFields(flags);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (IsExport(fieldInfo))
				{
					yield return fieldInfo;
				}
			}
			PropertyInfo[] properties = type.GetProperties(flags);
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (IsExport(propertyInfo))
				{
					yield return propertyInfo;
				}
			}
			MethodInfo[] methods = type.GetMethods(flags);
			foreach (MethodInfo methodInfo in methods)
			{
				if (IsExport(methodInfo))
				{
					yield return methodInfo;
				}
			}
		}

		private IEnumerable<Type> GetInheritedExports(Type type)
		{
			if (type.IsAbstract)
			{
				yield break;
			}
			Type currentType = type.BaseType;
			if (currentType == null)
			{
				yield break;
			}
			while (currentType != null && currentType.UnderlyingSystemType != CompositionServices.ObjectType)
			{
				if (IsInheritedExport(currentType))
				{
					yield return currentType;
				}
				currentType = currentType.BaseType;
			}
			Type[] interfaces = type.GetInterfaces();
			foreach (Type type2 in interfaces)
			{
				if (IsInheritedExport(type2))
				{
					yield return type2;
				}
			}
		}

		private static bool IsExport(ICustomAttributeProvider attributeProvider)
		{
			return attributeProvider.IsAttributeDefined<ExportAttribute>(inherit: false);
		}

		private static bool IsInheritedExport(ICustomAttributeProvider attributedProvider)
		{
			return attributedProvider.IsAttributeDefined<InheritedExportAttribute>(inherit: false);
		}

		private IEnumerable<ImportDefinition> GetImportDefinitions()
		{
			List<ImportDefinition> list = new List<ImportDefinition>();
			foreach (MemberInfo importMember in GetImportMembers(_type))
			{
				ReflectionMemberImportDefinition item = AttributedModelDiscovery.CreateMemberImportDefinition(importMember, this);
				list.Add(item);
			}
			ConstructorInfo constructor = GetConstructor();
			if (constructor != null)
			{
				ParameterInfo[] parameters = constructor.GetParameters();
				for (int i = 0; i < parameters.Length; i++)
				{
					ReflectionParameterImportDefinition item2 = AttributedModelDiscovery.CreateParameterImportDefinition(parameters[i], this);
					list.Add(item2);
				}
			}
			return list;
		}

		private IEnumerable<MemberInfo> GetImportMembers(Type type)
		{
			if (type.IsAbstract)
			{
				yield break;
			}
			foreach (MemberInfo declaredOnlyImportMember in GetDeclaredOnlyImportMembers(type))
			{
				yield return declaredOnlyImportMember;
			}
			if (!(type.BaseType != null))
			{
				yield break;
			}
			Type baseType = type.BaseType;
			while (baseType != null && baseType.UnderlyingSystemType != CompositionServices.ObjectType)
			{
				foreach (MemberInfo declaredOnlyImportMember2 in GetDeclaredOnlyImportMembers(baseType))
				{
					yield return declaredOnlyImportMember2;
				}
				baseType = baseType.BaseType;
			}
		}

		private IEnumerable<MemberInfo> GetDeclaredOnlyImportMembers(Type type)
		{
			BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			FieldInfo[] fields = type.GetFields(flags);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (IsImport(fieldInfo))
				{
					yield return fieldInfo;
				}
			}
			PropertyInfo[] properties = type.GetProperties(flags);
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (IsImport(propertyInfo))
				{
					yield return propertyInfo;
				}
			}
		}

		private static bool IsImport(ICustomAttributeProvider attributeProvider)
		{
			return attributeProvider.IsAttributeDefined<IAttributedImport>(inherit: false);
		}
	}
}
namespace Unity
{
	internal sealed class ThrowStub : ObjectDisposedException
	{
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
