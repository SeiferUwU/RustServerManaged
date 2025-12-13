using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;
using Unity;

[assembly: ComCompatibleVersion(1, 0, 3300, 0)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("System.Data.DataSetExtensions.dll")]
[assembly: AssemblyDescription("System.Data.DataSetExtensions.dll")]
[assembly: AssemblyDefaultAlias("System.Data.DataSetExtensions.dll")]
[assembly: AssemblyCompany("Mono development team")]
[assembly: AssemblyProduct("Mono Common Language Infrastructure")]
[assembly: SatelliteContractVersion("4.0.0.0")]
[assembly: AssemblyCopyright("(c) Various Mono authors")]
[assembly: AssemblyInformationalVersion("4.6.57.0")]
[assembly: AssemblyFileVersion("4.6.57.0")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: CLSCompliant(true)]
[assembly: AssemblyDelaySign(true)]
[assembly: ComVisible(false)]
[assembly: AllowPartiallyTrustedCallers]
[assembly: CompilationRelaxations(CompilationRelaxations.NoStringInterning)]
[assembly: SecurityCritical]
[assembly: AssemblyVersion("4.0.0.0")]
internal static class DataSetUtil
{
	private static readonly Type s_stackOverflowType = typeof(StackOverflowException);

	private static readonly Type s_outOfMemoryType = typeof(OutOfMemoryException);

	private static readonly Type s_threadAbortType = typeof(ThreadAbortException);

	private static readonly Type s_nullReferenceType = typeof(NullReferenceException);

	private static readonly Type s_accessViolationType = typeof(AccessViolationException);

	private static readonly Type s_securityType = typeof(SecurityException);

	internal static void CheckArgumentNull<T>(T argumentValue, string argumentName) where T : class
	{
		if (argumentValue == null)
		{
			throw ArgumentNull(argumentName);
		}
	}

	private static T TraceException<T>(string trace, T e)
	{
		return e;
	}

	private static T TraceExceptionAsReturnValue<T>(T e)
	{
		return TraceException("<comm.ADP.TraceException|ERR|THROW> '%ls'\n", e);
	}

	internal static ArgumentException Argument(string message)
	{
		return TraceExceptionAsReturnValue(new ArgumentException(message));
	}

	internal static ArgumentNullException ArgumentNull(string message)
	{
		return TraceExceptionAsReturnValue(new ArgumentNullException(message));
	}

	internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName)
	{
		return TraceExceptionAsReturnValue(new ArgumentOutOfRangeException(parameterName, message));
	}

	internal static InvalidCastException InvalidCast(string message)
	{
		return TraceExceptionAsReturnValue(new InvalidCastException(message));
	}

	internal static InvalidOperationException InvalidOperation(string message)
	{
		return TraceExceptionAsReturnValue(new InvalidOperationException(message));
	}

	internal static NotSupportedException NotSupported(string message)
	{
		return TraceExceptionAsReturnValue(new NotSupportedException(message));
	}

	internal static ArgumentOutOfRangeException InvalidEnumerationValue(Type type, int value)
	{
		return ArgumentOutOfRange($"The {type.Name} enumeration value, {value.ToString(CultureInfo.InvariantCulture)}, is not valid.", type.Name);
	}

	internal static ArgumentOutOfRangeException InvalidDataRowState(DataRowState value)
	{
		return InvalidEnumerationValue(typeof(DataRowState), (int)value);
	}

	internal static ArgumentOutOfRangeException InvalidLoadOption(LoadOption value)
	{
		return InvalidEnumerationValue(typeof(LoadOption), (int)value);
	}

	internal static bool IsCatchableExceptionType(Exception e)
	{
		Type type = e.GetType();
		if (type != s_stackOverflowType && type != s_outOfMemoryType && type != s_threadAbortType && type != s_nullReferenceType && type != s_accessViolationType)
		{
			return !s_securityType.IsAssignableFrom(type);
		}
		return false;
	}
}
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
internal class SR
{
	public const string DataSetLinq_InvalidEnumerationValue = "The {0} enumeration value, {1}, is not valid.";

	public const string DataSetLinq_EmptyDataRowSource = "The source contains no DataRows.";

	public const string DataSetLinq_NullDataRow = "The source contains a DataRow reference that is null.";

	public const string DataSetLinq_CannotLoadDetachedRow = "The source contains a detached DataRow that cannot be copied to the DataTable.";

	public const string DataSetLinq_CannotCompareDeletedRow = "The DataRowComparer does not work with DataRows that have been deleted since it only compares current values.";

	public const string DataSetLinq_CannotLoadDeletedRow = "The source contains a deleted DataRow that cannot be copied to the DataTable.";

	public const string DataSetLinq_NonNullableCast = "Cannot cast DBNull. Value to type '{0}'. Please use a nullable type.";
}
namespace System.Data
{
	/// <summary>Returns a singleton instance of the <see cref="T:System.Data.DataRowComparer`1" /> class.</summary>
	public static class DataRowComparer
	{
		/// <summary>Gets a singleton instance of <see cref="T:System.Data.DataRowComparer`1" />. This property is read-only.</summary>
		/// <returns>An instance of a <see cref="T:System.Data.DataRowComparer`1" />.</returns>
		public static DataRowComparer<DataRow> Default => DataRowComparer<DataRow>.Default;

		internal static bool AreEqual(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || a == DBNull.Value || b == null || b == DBNull.Value)
			{
				return false;
			}
			if (!a.Equals(b))
			{
				if (a.GetType().IsArray)
				{
					return CompareArray((Array)a, b as Array);
				}
				return false;
			}
			return true;
		}

		private static bool AreElementEqual(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || a == DBNull.Value || b == null || b == DBNull.Value)
			{
				return false;
			}
			return a.Equals(b);
		}

		private static bool CompareArray(Array a, Array b)
		{
			if (b == null || 1 != a.Rank || 1 != b.Rank || a.Length != b.Length)
			{
				return false;
			}
			int num = a.GetLowerBound(0);
			int num2 = b.GetLowerBound(0);
			if (a.GetType() == b.GetType() && num == 0 && num2 == 0)
			{
				switch (Type.GetTypeCode(a.GetType().GetElementType()))
				{
				case TypeCode.Byte:
					return CompareEquatableArray((byte[])a, (byte[])b);
				case TypeCode.Int16:
					return CompareEquatableArray((short[])a, (short[])b);
				case TypeCode.Int32:
					return CompareEquatableArray((int[])a, (int[])b);
				case TypeCode.Int64:
					return CompareEquatableArray((long[])a, (long[])b);
				case TypeCode.String:
					return CompareEquatableArray((string[])a, (string[])b);
				}
			}
			int num3 = num + a.Length;
			while (num < num3)
			{
				if (!AreElementEqual(a.GetValue(num), b.GetValue(num2)))
				{
					return false;
				}
				num++;
				num2++;
			}
			return true;
		}

		private static bool CompareEquatableArray<TElem>(TElem[] a, TElem[] b) where TElem : IEquatable<TElem>
		{
			for (int i = 0; i < a.Length; i++)
			{
				ref readonly TElem reference = ref a[i];
				TElem other = b[i];
				if (!reference.Equals(other))
				{
					return false;
				}
			}
			return true;
		}
	}
	/// <summary>Compares two <see cref="T:System.Data.DataRow" /> objects for equivalence by using value-based comparison.</summary>
	/// <typeparam name="TRow">The type of objects to be compared, typically <see cref="T:System.Data.DataRow" />.</typeparam>
	public sealed class DataRowComparer<TRow> : IEqualityComparer<TRow> where TRow : DataRow
	{
		private static DataRowComparer<TRow> s_instance = new DataRowComparer<TRow>();

		/// <summary>Gets a singleton instance of <see cref="T:System.Data.DataRowComparer`1" />. This property is read-only.</summary>
		/// <returns>An instance of a <see cref="T:System.Data.DataRowComparer`1" />.</returns>
		public static DataRowComparer<TRow> Default => s_instance;

		private DataRowComparer()
		{
		}

		/// <summary>Compares two <see cref="T:System.Data.DataRow" /> objects by using a column-by-column, value-based comparison.</summary>
		/// <param name="leftRow">The first <see cref="T:System.Data.DataRow" /> object to compare.</param>
		/// <param name="rightRow">The second <see cref="T:System.Data.DataRow" /> object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Data.DataRow" /> objects have ordered sets of column values that are equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">One or both of the source <see cref="T:System.Data.DataRow" /> objects are <see langword="null" />.</exception>
		public bool Equals(TRow leftRow, TRow rightRow)
		{
			if (leftRow == rightRow)
			{
				return true;
			}
			if (leftRow == null || rightRow == null)
			{
				return false;
			}
			if (leftRow.RowState == DataRowState.Deleted || rightRow.RowState == DataRowState.Deleted)
			{
				throw DataSetUtil.InvalidOperation("The DataRowComparer does not work with DataRows that have been deleted since it only compares current values.");
			}
			int count = leftRow.Table.Columns.Count;
			if (count != rightRow.Table.Columns.Count)
			{
				return false;
			}
			for (int i = 0; i < count; i++)
			{
				if (!DataRowComparer.AreEqual(leftRow[i], rightRow[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns a hash code for the specified <see cref="T:System.Data.DataRow" /> object.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to compute the hash code from.</param>
		/// <returns>An <see cref="T:System.Int32" /> value representing the hash code of the row.</returns>
		/// <exception cref="T:System.ArgumentException">The source <see cref="T:System.Data.DataRow" /> objects does not belong to a <see cref="T:System.Data.DataTable" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The source <see cref="T:System.Data.DataRow" /> objects is <see langword="null" />.</exception>
		public int GetHashCode(TRow row)
		{
			DataSetUtil.CheckArgumentNull(row, "row");
			if (row.RowState == DataRowState.Deleted)
			{
				throw DataSetUtil.InvalidOperation("The DataRowComparer does not work with DataRows that have been deleted since it only compares current values.");
			}
			int result = 0;
			if (row.Table.Columns.Count > 0)
			{
				object obj = row[0];
				if (!obj.GetType().IsArray)
				{
					result = ((!(obj is ValueType valueType)) ? obj.GetHashCode() : valueType.GetHashCode());
				}
				else
				{
					Array array = obj as Array;
					if (array.Rank > 1)
					{
						result = obj.GetHashCode();
					}
					else if (array.Length > 0)
					{
						result = array.GetValue(array.GetLowerBound(0)).GetHashCode();
					}
				}
			}
			return result;
		}
	}
	/// <summary>Defines the extension methods to the <see cref="T:System.Data.DataRow" /> class. This is a static class.</summary>
	public static class DataRowExtensions
	{
		private static class UnboxT<T>
		{
			internal static readonly Converter<object, T> s_unbox = Create();

			private static Converter<object, T> Create()
			{
				if (default(T) == null)
				{
					return ReferenceOrNullableField;
				}
				return ValueField;
			}

			private static T ReferenceOrNullableField(object value)
			{
				if (DBNull.Value != value)
				{
					return (T)value;
				}
				return default(T);
			}

			private static T ValueField(object value)
			{
				if (DBNull.Value == value)
				{
					throw DataSetUtil.InvalidCast($"Cannot cast DBNull. Value to type '{typeof(T).ToString()}'. Please use a nullable type.");
				}
				return (T)value;
			}
		}

		/// <summary>Provides strongly-typed access to each of the column values in the specified row. The <see cref="M:System.Data.DataRowExtensions.Field``1(System.Data.DataRow,System.String)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="columnName">The name of the column to return the value of.</param>
		/// <typeparam name="T">A generic parameter that specifies the return type of the column.</typeparam>
		/// <returns>The value, of type <paramref name="T" />, of the <see cref="T:System.Data.DataColumn" /> specified by <paramref name="columnName" />.</returns>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The column specified by <paramref name="columnName" /> does not occur in the <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataRow" /> is a part of.</exception>
		/// <exception cref="T:System.NullReferenceException">A <see langword="null" /> value was assigned to a non-nullable type.</exception>
		public static T Field<T>(this DataRow row, string columnName)
		{
			DataSetUtil.CheckArgumentNull(row, "row");
			return UnboxT<T>.s_unbox(row[columnName]);
		}

		/// <summary>Provides strongly-typed access to each of the column values in the specified row. The <see cref="M:System.Data.DataRowExtensions.Field``1(System.Data.DataRow,System.Data.DataColumn)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="column">The input <see cref="T:System.Data.DataColumn" /> object that specifies the column to return the value of.</param>
		/// <typeparam name="T">A generic parameter that specifies the return type of the column.</typeparam>
		/// <returns>The value, of type <paramref name="T" />, of the <see cref="T:System.Data.DataColumn" /> specified by <paramref name="column" />.</returns>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The column specified by <paramref name="column" /> does not occur in the <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataRow" /> is a part of.</exception>
		/// <exception cref="T:System.NullReferenceException">A null value was assigned to a non-nullable type.</exception>
		public static T Field<T>(this DataRow row, DataColumn column)
		{
			DataSetUtil.CheckArgumentNull(row, "row");
			return UnboxT<T>.s_unbox(row[column]);
		}

		/// <summary>Provides strongly-typed access to each of the column values in the specified row. The <see cref="M:System.Data.DataRowExtensions.Field``1(System.Data.DataRow,System.Int32)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="columnIndex">The column index.</param>
		/// <typeparam name="T">A generic parameter that specifies the return type of the column.</typeparam>
		/// <returns>The value, of type <paramref name="T" />, of the <see cref="T:System.Data.DataColumn" /> specified by <paramref name="columnIndex" />.</returns>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The column specified by <paramref name="ordinal" /> does not exist in the <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataRow" /> is a part of.</exception>
		/// <exception cref="T:System.NullReferenceException">A null value was assigned to a non-nullable type.</exception>
		public static T Field<T>(this DataRow row, int columnIndex)
		{
			DataSetUtil.CheckArgumentNull(row, "row");
			return UnboxT<T>.s_unbox(row[columnIndex]);
		}

		/// <summary>Provides strongly-typed access to each of the column values in the specified row. The <see cref="M:System.Data.DataRowExtensions.Field``1(System.Data.DataRow,System.Int32,System.Data.DataRowVersion)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="columnIndex">The zero-based ordinal of the column to return the value of.</param>
		/// <param name="version">A <see cref="T:System.Data.DataRowVersion" /> enumeration that specifies the version of the column value to return, such as <see langword="Current" /> or <see langword="Original" /> version.</param>
		/// <typeparam name="T">A generic parameter that specifies the return type of the column.</typeparam>
		/// <returns>The value, of type <paramref name="T" />, of the <see cref="T:System.Data.DataColumn" /> specified by <paramref name="ordinal" /> and <paramref name="version" />.</returns>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The column specified by <paramref name="ordinal" /> does not exist in the <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataRow" /> is a part of.</exception>
		/// <exception cref="T:System.NullReferenceException">A null value was assigned to a non-nullable type.</exception>
		public static T Field<T>(this DataRow row, int columnIndex, DataRowVersion version)
		{
			DataSetUtil.CheckArgumentNull(row, "row");
			return UnboxT<T>.s_unbox(row[columnIndex, version]);
		}

		/// <summary>Provides strongly-typed access to each of the column values in the specified row. The <see cref="M:System.Data.DataRowExtensions.Field``1(System.Data.DataRow,System.String,System.Data.DataRowVersion)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="columnName">The name of the column to return the value of.</param>
		/// <param name="version">A <see cref="T:System.Data.DataRowVersion" /> enumeration that specifies the version of the column value to return, such as <see langword="Current" /> or <see langword="Original" /> version.</param>
		/// <typeparam name="T">A generic parameter that specifies the return type of the column.</typeparam>
		/// <returns>The value, of type <paramref name="T" />, of the <see cref="T:System.Data.DataColumn" /> specified by <paramref name="columnName" /> and <paramref name="version" />.</returns>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The column specified by <paramref name="columnName" /> does not exist in the <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataRow" /> is a part of.</exception>
		/// <exception cref="T:System.NullReferenceException">A null value was assigned to a non-nullable type.</exception>
		public static T Field<T>(this DataRow row, string columnName, DataRowVersion version)
		{
			DataSetUtil.CheckArgumentNull(row, "row");
			return UnboxT<T>.s_unbox(row[columnName, version]);
		}

		/// <summary>Provides strongly-typed access to each of the column values in the specified row. The <see cref="M:System.Data.DataRowExtensions.Field``1(System.Data.DataRow,System.Data.DataColumn,System.Data.DataRowVersion)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="column">The input <see cref="T:System.Data.DataColumn" /> object that specifies the column to return the value of.</param>
		/// <param name="version">A <see cref="T:System.Data.DataRowVersion" /> enumeration that specifies the version of the column value to return, such as <see langword="Current" /> or <see langword="Original" /> version.</param>
		/// <typeparam name="T">A generic parameter that specifies the return type of the column.</typeparam>
		/// <returns>The value, of type <paramref name="T" />, of the <see cref="T:System.Data.DataColumn" /> specified by <paramref name="column" /> and <paramref name="version" />.</returns>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The column specified by <paramref name="column" /> does not exist in the <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataRow" /> is a part of.</exception>
		/// <exception cref="T:System.NullReferenceException">A null value was assigned to a non-nullable type.</exception>
		public static T Field<T>(this DataRow row, DataColumn column, DataRowVersion version)
		{
			DataSetUtil.CheckArgumentNull(row, "row");
			return UnboxT<T>.s_unbox(row[column, version]);
		}

		/// <summary>Sets a new value for the specified column in the <see cref="T:System.Data.DataRow" /> the method is called on. The <see cref="M:System.Data.DataRowExtensions.SetField``1(System.Data.DataRow,System.Int32,``0)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="columnIndex">The zero-based ordinal of the column to set the value of.</param>
		/// <param name="value">The new row value for the specified column, of type <paramref name="T" />.</param>
		/// <typeparam name="T">A generic parameter that specifies the value type of the column.</typeparam>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">Occurs when attempting to set a value on a deleted row.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="ordinal" /> argument is out of range.</exception>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could be not cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		public static void SetField<T>(this DataRow row, int columnIndex, T value)
		{
			DataSetUtil.CheckArgumentNull(row, "row");
			row[columnIndex] = ((object)value) ?? DBNull.Value;
		}

		/// <summary>Sets a new value for the specified column in the <see cref="T:System.Data.DataRow" />. The <see cref="M:System.Data.DataRowExtensions.SetField``1(System.Data.DataRow,System.String,``0)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="columnName">The name of the column to set the value of.</param>
		/// <param name="value">The new row value for the specified column, of type <paramref name="T" />.</param>
		/// <typeparam name="T">A generic parameter that specifies the value type of the column.</typeparam>
		/// <exception cref="T:System.ArgumentException">The column specified by <paramref name="columnName" /> cannot be found.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">Occurs when attempting to set a value on a deleted row.</exception>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		public static void SetField<T>(this DataRow row, string columnName, T value)
		{
			DataSetUtil.CheckArgumentNull(row, "row");
			row[columnName] = ((object)value) ?? DBNull.Value;
		}

		/// <summary>Sets a new value for the specified column in the <see cref="T:System.Data.DataRow" />. The <see cref="M:System.Data.DataRowExtensions.SetField``1(System.Data.DataRow,System.Data.DataColumn,``0)" /> method also supports nullable types.</summary>
		/// <param name="row">The input <see cref="T:System.Data.DataRow" />, which acts as the <see langword="this" /> instance for the extension method.</param>
		/// <param name="column">The input <see cref="T:System.Data.DataColumn" /> specifies which row value to retrieve.</param>
		/// <param name="value">The new row value for the specified column, of type <paramref name="T" />.</param>
		/// <typeparam name="T">A generic parameter that specifies the value type of the column.</typeparam>
		/// <exception cref="T:System.ArgumentException">The column specified by <paramref name="column" /> cannot be found.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> is null.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">Occurs when attempting to set a value on a deleted row.</exception>
		/// <exception cref="T:System.InvalidCastException">The value type of the underlying column could not be cast to the type specified by the generic parameter, <paramref name="T" />.</exception>
		public static void SetField<T>(this DataRow row, DataColumn column, T value)
		{
			DataSetUtil.CheckArgumentNull(row, "row");
			row[column] = ((object)value) ?? DBNull.Value;
		}
	}
	/// <summary>Defines the extension methods to the <see cref="T:System.Data.DataTable" /> class. <see cref="T:System.Data.DataTableExtensions" /> is a static class.</summary>
	public static class DataTableExtensions
	{
		/// <summary>Returns an <see cref="T:System.Collections.Generic.IEnumerable`1" /> object, where the generic parameter <paramref name="T" /> is <see cref="T:System.Data.DataRow" />. This object can be used in a LINQ expression or method query.</summary>
		/// <param name="source">The source <see cref="T:System.Data.DataTable" /> to make enumerable.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> object, where the generic parameter <paramref name="T" /> is <see cref="T:System.Data.DataRow" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The source <see cref="T:System.Data.DataTable" /> is <see langword="null" />.</exception>
		public static EnumerableRowCollection<DataRow> AsEnumerable(this DataTable source)
		{
			DataSetUtil.CheckArgumentNull(source, "source");
			return new EnumerableRowCollection<DataRow>(source);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that contains copies of the <see cref="T:System.Data.DataRow" /> objects, given an input <see cref="T:System.Collections.Generic.IEnumerable`1" /> object where the generic parameter <paramref name="T" /> is <see cref="T:System.Data.DataRow" />.</summary>
		/// <param name="source">The source <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence.</param>
		/// <typeparam name="T">The type of objects in the source sequence, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains the input sequence as the type of <see cref="T:System.Data.DataRow" /> objects.</returns>
		/// <exception cref="T:System.ArgumentNullException">The source <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence is <see langword="null" /> and a new table cannot be created.</exception>
		/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Data.DataRow" /> in the source sequence has a state of <see cref="F:System.Data.DataRowState.Deleted" />.  
		///  The source sequence does not contain any <see cref="T:System.Data.DataRow" /> objects.  
		///  A <see cref="T:System.Data.DataRow" /> in the source sequence is <see langword="null" />.</exception>
		public static DataTable CopyToDataTable<T>(this IEnumerable<T> source) where T : DataRow
		{
			DataSetUtil.CheckArgumentNull(source, "source");
			return LoadTableFromEnumerable(source, null, null, null);
		}

		/// <summary>Copies <see cref="T:System.Data.DataRow" /> objects to the specified <see cref="T:System.Data.DataTable" />, given an input <see cref="T:System.Collections.Generic.IEnumerable`1" /> object where the generic parameter <paramref name="T" /> is <see cref="T:System.Data.DataRow" />.</summary>
		/// <param name="source">The source <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence.</param>
		/// <param name="table">The destination <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="options">A <see cref="T:System.Data.LoadOption" /> enumeration that specifies the <see cref="T:System.Data.DataTable" /> load options.</param>
		/// <typeparam name="T">The type of objects in the source sequence, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <exception cref="T:System.ArgumentException">The copied <see cref="T:System.Data.DataRow" /> objects do not fit the schema of the destination <see cref="T:System.Data.DataTable" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The source <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence is <see langword="null" /> or the destination <see cref="T:System.Data.DataTable" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Data.DataRow" /> in the source sequence has a state of <see cref="F:System.Data.DataRowState.Deleted" />.  
		///  The source sequence does not contain any <see cref="T:System.Data.DataRow" /> objects.  
		///  A <see cref="T:System.Data.DataRow" /> in the source sequence is <see langword="null" />.</exception>
		public static void CopyToDataTable<T>(this IEnumerable<T> source, DataTable table, LoadOption options) where T : DataRow
		{
			DataSetUtil.CheckArgumentNull(source, "source");
			DataSetUtil.CheckArgumentNull(table, "table");
			LoadTableFromEnumerable(source, table, options, null);
		}

		/// <summary>Copies <see cref="T:System.Data.DataRow" /> objects to the specified <see cref="T:System.Data.DataTable" />, given an input <see cref="T:System.Collections.Generic.IEnumerable`1" /> object where the generic parameter <paramref name="T" /> is <see cref="T:System.Data.DataRow" />.</summary>
		/// <param name="source">The source <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence.</param>
		/// <param name="table">The destination <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="options">A <see cref="T:System.Data.LoadOption" /> enumeration that specifies the <see cref="T:System.Data.DataTable" /> load options.</param>
		/// <param name="errorHandler">A <see cref="T:System.Data.FillErrorEventHandler" /> delegate that represents the method that will handle an error.</param>
		/// <typeparam name="T">The type of objects in the source sequence, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <exception cref="T:System.ArgumentException">The copied <see cref="T:System.Data.DataRow" /> objects do not fit the schema of the destination <see cref="T:System.Data.DataTable" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The source <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence is <see langword="null" /> or the destination <see cref="T:System.Data.DataTable" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Data.DataRow" /> in the source sequence has a state of <see cref="F:System.Data.DataRowState.Deleted" />.  
		///  -or-  
		///  The source sequence does not contain any <see cref="T:System.Data.DataRow" /> objects.  
		///  -or-  
		///  A <see cref="T:System.Data.DataRow" /> in the source sequence is <see langword="null" />.</exception>
		public static void CopyToDataTable<T>(this IEnumerable<T> source, DataTable table, LoadOption options, FillErrorEventHandler errorHandler) where T : DataRow
		{
			DataSetUtil.CheckArgumentNull(source, "source");
			DataSetUtil.CheckArgumentNull(table, "table");
			LoadTableFromEnumerable(source, table, options, errorHandler);
		}

		private static DataTable LoadTableFromEnumerable<T>(IEnumerable<T> source, DataTable table, LoadOption? options, FillErrorEventHandler errorHandler) where T : DataRow
		{
			if (options.HasValue)
			{
				LoadOption value = options.Value;
				if ((uint)(value - 1) > 2u)
				{
					throw DataSetUtil.InvalidLoadOption(options.Value);
				}
			}
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					return table ?? throw DataSetUtil.InvalidOperation("The source contains no DataRows.");
				}
				if (table == null)
				{
					DataRow current = enumerator.Current;
					if (current == null)
					{
						throw DataSetUtil.InvalidOperation("The source contains a DataRow reference that is null.");
					}
					table = new DataTable
					{
						Locale = CultureInfo.CurrentCulture
					};
					foreach (DataColumn column in current.Table.Columns)
					{
						table.Columns.Add(column.ColumnName, column.DataType);
					}
				}
				table.BeginLoadData();
				try
				{
					do
					{
						DataRow current = enumerator.Current;
						if (current == null)
						{
							continue;
						}
						object[] values = null;
						try
						{
							switch (current.RowState)
							{
							case DataRowState.Detached:
								if (!current.HasVersion(DataRowVersion.Proposed))
								{
									throw DataSetUtil.InvalidOperation("The source contains a detached DataRow that cannot be copied to the DataTable.");
								}
								goto case DataRowState.Unchanged;
							case DataRowState.Unchanged:
							case DataRowState.Added:
							case DataRowState.Modified:
								values = current.ItemArray;
								if (options.HasValue)
								{
									table.LoadDataRow(values, options.Value);
								}
								else
								{
									table.LoadDataRow(values, fAcceptChanges: true);
								}
								break;
							case DataRowState.Deleted:
								throw DataSetUtil.InvalidOperation("The source contains a deleted DataRow that cannot be copied to the DataTable.");
							default:
								throw DataSetUtil.InvalidDataRowState(current.RowState);
							}
						}
						catch (Exception ex)
						{
							if (!DataSetUtil.IsCatchableExceptionType(ex))
							{
								throw;
							}
							FillErrorEventArgs e = null;
							if (errorHandler != null)
							{
								e = new FillErrorEventArgs(table, values)
								{
									Errors = ex
								};
								errorHandler(enumerator, e);
							}
							if (e == null)
							{
								throw;
							}
							if (!e.Continue)
							{
								if ((e.Errors ?? ex) == ex)
								{
									throw;
								}
								throw e.Errors;
							}
						}
					}
					while (enumerator.MoveNext());
				}
				finally
				{
					table.EndLoadData();
				}
			}
			return table;
		}

		/// <summary>Creates and returns a LINQ-enabled <see cref="T:System.Data.DataView" /> object.</summary>
		/// <param name="table">The source <see cref="T:System.Data.DataTable" /> from which the LINQ-enabled <see cref="T:System.Data.DataView" /> is created.</param>
		/// <returns>A LINQ-enabled <see cref="T:System.Data.DataView" /> object.</returns>
		public static DataView AsDataView(this DataTable table)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Creates and returns a LINQ-enabled <see cref="T:System.Data.DataView" /> object representing the LINQ to DataSet query.</summary>
		/// <param name="source">The source LINQ to DataSet query from which the LINQ-enabled <see cref="T:System.Data.DataView" /> is created.</param>
		/// <typeparam name="T">The type of objects in the source sequence, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <returns>A LINQ-enabled <see cref="T:System.Data.DataView" /> object.</returns>
		public static DataView AsDataView<T>(this EnumerableRowCollection<T> source) where T : DataRow
		{
			throw new PlatformNotSupportedException();
		}
	}
	/// <summary>Represents a collection of <see cref="T:System.Data.DataRow" /> objects returned from a LINQ to DataSet query. This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	public abstract class EnumerableRowCollection : IEnumerable
	{
		internal abstract Type ElementType { get; }

		internal abstract DataTable Table { get; }

		internal EnumerableRowCollection()
		{
		}

		/// <summary>Returns an enumerator for the collection of <see cref="T:System.Data.DataRow" /> objects. This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to traverse the collection of <see cref="T:System.Data.DataRow" /> objects.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}
	}
	/// <summary>Represents a collection of <see cref="T:System.Data.DataRow" /> objects returned from a query.</summary>
	/// <typeparam name="TRow">The type of objects in the source sequence, typically <see cref="T:System.Data.DataRow" />.</typeparam>
	public class EnumerableRowCollection<TRow> : EnumerableRowCollection, IEnumerable<TRow>, IEnumerable
	{
		private readonly DataTable _table;

		private readonly IEnumerable<TRow> _enumerableRows;

		private readonly List<Func<TRow, bool>> _listOfPredicates;

		private readonly SortExpressionBuilder<TRow> _sortExpression;

		private readonly Func<TRow, TRow> _selector;

		internal override Type ElementType => typeof(TRow);

		internal IEnumerable<TRow> EnumerableRows => _enumerableRows;

		internal override DataTable Table => _table;

		internal EnumerableRowCollection(IEnumerable<TRow> enumerableRows, bool isDataViewable, DataTable table)
		{
			_enumerableRows = enumerableRows;
			if (isDataViewable)
			{
				_table = table;
			}
			_listOfPredicates = new List<Func<TRow, bool>>();
			_sortExpression = new SortExpressionBuilder<TRow>();
		}

		internal EnumerableRowCollection(DataTable table)
		{
			_table = table;
			_enumerableRows = table.Rows.Cast<TRow>();
			_listOfPredicates = new List<Func<TRow, bool>>();
			_sortExpression = new SortExpressionBuilder<TRow>();
		}

		internal EnumerableRowCollection(EnumerableRowCollection<TRow> source, IEnumerable<TRow> enumerableRows, Func<TRow, TRow> selector)
		{
			_enumerableRows = enumerableRows;
			_selector = selector;
			if (source != null)
			{
				if (source._selector == null)
				{
					_table = source._table;
				}
				_listOfPredicates = new List<Func<TRow, bool>>(source._listOfPredicates);
				_sortExpression = source._sortExpression.Clone();
			}
			else
			{
				_listOfPredicates = new List<Func<TRow, bool>>();
				_sortExpression = new SortExpressionBuilder<TRow>();
			}
		}

		/// <summary>Returns an enumerator for the collection of <see cref="T:System.Data.DataRow" /> objects.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to traverse the collection of <see cref="T:System.Data.DataRow" /> objects.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>Returns an enumerator for the collection of contained row objects.</summary>
		/// <returns>A strongly-typed <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to traverse the collection of <paramref name="TRow" /> objects.</returns>
		public IEnumerator<TRow> GetEnumerator()
		{
			return _enumerableRows.GetEnumerator();
		}

		internal void AddPredicate(Func<TRow, bool> pred)
		{
			_listOfPredicates.Add(pred);
		}

		internal void AddSortExpression<TKey>(Func<TRow, TKey> keySelector, bool isDescending, bool isOrderBy)
		{
			AddSortExpression(keySelector, Comparer<TKey>.Default, isDescending, isOrderBy);
		}

		internal void AddSortExpression<TKey>(Func<TRow, TKey> keySelector, IComparer<TKey> comparer, bool isDescending, bool isOrderBy)
		{
			DataSetUtil.CheckArgumentNull(keySelector, "keySelector");
			DataSetUtil.CheckArgumentNull(comparer, "comparer");
			_sortExpression.Add((TRow input) => keySelector(input), (object val1, object val2) => ((!isDescending) ? 1 : (-1)) * comparer.Compare((TKey)val1, (TKey)val2), isOrderBy);
		}

		internal EnumerableRowCollection()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Contains the extension methods for the data row collection classes.</summary>
	public static class EnumerableRowCollectionExtensions
	{
		/// <summary>Filters a sequence of rows based on the specified predicate.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to filter.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> that contains rows from the input sequence that satisfy the condition.</returns>
		public static EnumerableRowCollection<TRow> Where<TRow>(this EnumerableRowCollection<TRow> source, Func<TRow, bool> predicate)
		{
			EnumerableRowCollection<TRow> enumerableRowCollection = new EnumerableRowCollection<TRow>(source, Enumerable.Where(source, predicate), null);
			enumerableRowCollection.AddPredicate(predicate);
			return enumerableRowCollection;
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in ascending order according to the specified key.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key.</returns>
		public static OrderedEnumerableRowCollection<TRow> OrderBy<TRow, TKey>(this EnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector)
		{
			IEnumerable<TRow> enumerableRows = Enumerable.OrderBy(source, keySelector);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression(keySelector, isDescending: false, isOrderBy: true);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in ascending order according to the specified key and comparer.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key and comparer.</returns>
		public static OrderedEnumerableRowCollection<TRow> OrderBy<TRow, TKey>(this EnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector, IComparer<TKey> comparer)
		{
			IEnumerable<TRow> enumerableRows = Enumerable.OrderBy(source, keySelector, comparer);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression(keySelector, comparer, isDescending: false, isOrderBy: true);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in descending order according to the specified key.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key.</returns>
		public static OrderedEnumerableRowCollection<TRow> OrderByDescending<TRow, TKey>(this EnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector)
		{
			IEnumerable<TRow> enumerableRows = Enumerable.OrderByDescending(source, keySelector);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression(keySelector, isDescending: true, isOrderBy: true);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in descending order according to the specified key and comparer.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key and comparer.</returns>
		public static OrderedEnumerableRowCollection<TRow> OrderByDescending<TRow, TKey>(this EnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector, IComparer<TKey> comparer)
		{
			IEnumerable<TRow> enumerableRows = Enumerable.OrderByDescending(source, keySelector, comparer);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression(keySelector, comparer, isDescending: true, isOrderBy: true);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Performs a secondary ordering of the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in ascending order according to the specified key.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key.</returns>
		public static OrderedEnumerableRowCollection<TRow> ThenBy<TRow, TKey>(this OrderedEnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector)
		{
			IEnumerable<TRow> enumerableRows = ((IOrderedEnumerable<TRow>)source.EnumerableRows).ThenBy(keySelector);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression(keySelector, isDescending: false, isOrderBy: false);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Performs a secondary ordering of the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in ascending order according to the specified key and comparer.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key and comparer.</returns>
		public static OrderedEnumerableRowCollection<TRow> ThenBy<TRow, TKey>(this OrderedEnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector, IComparer<TKey> comparer)
		{
			IEnumerable<TRow> enumerableRows = ((IOrderedEnumerable<TRow>)source.EnumerableRows).ThenBy(keySelector, comparer);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression(keySelector, comparer, isDescending: false, isOrderBy: false);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Performs a secondary ordering of the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in descending order according to the specified key.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key.</returns>
		public static OrderedEnumerableRowCollection<TRow> ThenByDescending<TRow, TKey>(this OrderedEnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector)
		{
			IEnumerable<TRow> enumerableRows = ((IOrderedEnumerable<TRow>)source.EnumerableRows).ThenByDescending(keySelector);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression(keySelector, isDescending: true, isOrderBy: false);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Performs a secondary ordering of the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in descending order according to the specified key and comparer.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key and comparer.</returns>
		public static OrderedEnumerableRowCollection<TRow> ThenByDescending<TRow, TKey>(this OrderedEnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector, IComparer<TKey> comparer)
		{
			IEnumerable<TRow> enumerableRows = ((IOrderedEnumerable<TRow>)source.EnumerableRows).ThenByDescending(keySelector, comparer);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression(keySelector, comparer, isDescending: true, isOrderBy: false);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Projects each element of an <see cref="T:System.Data.EnumerableRowCollection`1" /> into a new form.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection`1" /> containing the <see cref="T:System.Data.DataRow" /> elements to invoke a transform function upon.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="S">The type that <paramref name="TRow" /> will be transformed into.</typeparam>
		/// <returns>An <see cref="T:System.Data.EnumerableRowCollection`1" /> whose elements are the result of invoking the transform function on each element of <paramref name="source" />.</returns>
		public static EnumerableRowCollection<S> Select<TRow, S>(this EnumerableRowCollection<TRow> source, Func<TRow, S> selector)
		{
			IEnumerable<S> enumerableRows = Enumerable.Select(source, selector);
			return new EnumerableRowCollection<S>(source as EnumerableRowCollection<S>, enumerableRows, selector as Func<S, S>);
		}

		/// <summary>Converts the elements of an <see cref="T:System.Data.EnumerableRowCollection" /> to the specified type.</summary>
		/// <param name="source">The <see cref="T:System.Data.EnumerableRowCollection" /> that contains the elements to be converted.</param>
		/// <typeparam name="TResult">The type to convert the elements of source to.</typeparam>
		/// <returns>An <see cref="T:System.Data.EnumerableRowCollection" /> that contains each element of the source sequence converted to the specified type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">An element in the sequence cannot be cast to type <paramref name="TResult" />.</exception>
		public static EnumerableRowCollection<TResult> Cast<TResult>(this EnumerableRowCollection source)
		{
			if (source != null && source.ElementType.Equals(typeof(TResult)))
			{
				return (EnumerableRowCollection<TResult>)source;
			}
			return new EnumerableRowCollection<TResult>(Enumerable.Cast<TResult>(source), typeof(TResult).IsAssignableFrom(source.ElementType) && typeof(DataRow).IsAssignableFrom(typeof(TResult)), source.Table);
		}
	}
	/// <summary>Represents a collection of ordered <see cref="T:System.Data.DataRow" /> objects returned from a query.</summary>
	/// <typeparam name="TRow">The type of objects in the source sequence, typically <see cref="T:System.Data.DataRow" />.</typeparam>
	public sealed class OrderedEnumerableRowCollection<TRow> : EnumerableRowCollection<TRow>
	{
		internal OrderedEnumerableRowCollection(EnumerableRowCollection<TRow> enumerableTable, IEnumerable<TRow> enumerableRows)
			: base(enumerableTable, enumerableRows, (Func<TRow, TRow>)null)
		{
		}

		internal OrderedEnumerableRowCollection()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	internal class SortExpressionBuilder<T> : IComparer<List<object>>
	{
		private LinkedList<Func<T, object>> _selectors = new LinkedList<Func<T, object>>();

		private LinkedList<Comparison<object>> _comparers = new LinkedList<Comparison<object>>();

		private LinkedListNode<Func<T, object>> _currentSelector;

		private LinkedListNode<Comparison<object>> _currentComparer;

		internal int Count => _selectors.Count;

		internal void Add(Func<T, object> keySelector, Comparison<object> compare, bool isOrderBy)
		{
			if (isOrderBy)
			{
				_currentSelector = _selectors.AddFirst(keySelector);
				_currentComparer = _comparers.AddFirst(compare);
			}
			else
			{
				_currentSelector = _selectors.AddAfter(_currentSelector, keySelector);
				_currentComparer = _comparers.AddAfter(_currentComparer, compare);
			}
		}

		public List<object> Select(T row)
		{
			List<object> list = new List<object>();
			foreach (Func<T, object> selector in _selectors)
			{
				list.Add(selector(row));
			}
			return list;
		}

		public int Compare(List<object> a, List<object> b)
		{
			int num = 0;
			foreach (Comparison<object> comparer in _comparers)
			{
				int num2 = comparer(a[num], b[num]);
				if (num2 != 0)
				{
					return num2;
				}
				num++;
			}
			return 0;
		}

		internal SortExpressionBuilder<T> Clone()
		{
			SortExpressionBuilder<T> sortExpressionBuilder = new SortExpressionBuilder<T>();
			foreach (Func<T, object> selector in _selectors)
			{
				if (selector == _currentSelector.Value)
				{
					sortExpressionBuilder._currentSelector = sortExpressionBuilder._selectors.AddLast(selector);
				}
				else
				{
					sortExpressionBuilder._selectors.AddLast(selector);
				}
			}
			foreach (Comparison<object> comparer in _comparers)
			{
				if (comparer == _currentComparer.Value)
				{
					sortExpressionBuilder._currentComparer = sortExpressionBuilder._comparers.AddLast(comparer);
				}
				else
				{
					sortExpressionBuilder._comparers.AddLast(comparer);
				}
			}
			return sortExpressionBuilder;
		}

		internal SortExpressionBuilder<TResult> CloneCast<TResult>()
		{
			SortExpressionBuilder<TResult> sortExpressionBuilder = new SortExpressionBuilder<TResult>();
			foreach (Func<T, object> selector in _selectors)
			{
				if (selector == _currentSelector.Value)
				{
					sortExpressionBuilder._currentSelector = sortExpressionBuilder._selectors.AddLast((TResult r) => selector((T)(object)r));
				}
				else
				{
					sortExpressionBuilder._selectors.AddLast((TResult r) => selector((T)(object)r));
				}
			}
			foreach (Comparison<object> comparer in _comparers)
			{
				if (comparer == _currentComparer.Value)
				{
					sortExpressionBuilder._currentComparer = sortExpressionBuilder._comparers.AddLast(comparer);
				}
				else
				{
					sortExpressionBuilder._comparers.AddLast(comparer);
				}
			}
			return sortExpressionBuilder;
		}
	}
	/// <summary>This type is used as a base class for typed-<see cref="T:System.Data.DataTable" /> object generation by Visual Studio and the XSD.exe .NET Framework tool, and is not intended to be used directly from your code.</summary>
	/// <typeparam name="T">The type of objects in the source sequence represented by the table, typically <see cref="T:System.Data.DataRow" />.</typeparam>
	[Serializable]
	public abstract class TypedTableBase<T> : DataTable, IEnumerable<T>, IEnumerable where T : DataRow
	{
		/// <summary>Initializes a new <see cref="T:System.Data.TypedTableBase`1" />. This method supports typed-<see cref="T:System.Data.DataTable" /> object generation by Visual Studio and the XSD.exe .NET Framework tool. This type is not intended to be used directly from your code.</summary>
		protected TypedTableBase()
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Data.TypedTableBase`1" />. This method supports typed-<see cref="T:System.Data.DataTable" /> object generation by Visual Studio and the XSD.exe .NET Framework tool. This method is not intended to be used directly from your code.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains data to construct the object.</param>
		/// <param name="context">The streaming context for the object being deserializad.</param>
		protected TypedTableBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Returns an enumerator for the typed-<see cref="T:System.Data.DataRow" />. This method supports typed-<see cref="T:System.Data.DataTable" /> object generation by Visual Studio and the XSD.exe .NET Framework tool. This method is not intended to be used directly from your code.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.Generic.IEnumerator`1" /> interface.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			return base.Rows.Cast<T>().GetEnumerator();
		}

		/// <summary>Returns an enumerator for the typed-<see cref="T:System.Data.DataRow" />. This method supports typed-<see cref="T:System.Data.DataTable" /> object generation by Visual Studio and the XSD.exe .NET Framework tool. This method is not intended to be used directly from your code.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.Generic.IEnumerator`1" /> interface.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>Converts the elements of an <see cref="T:System.Data.TypedTableBase`1" /> to the specified type. This method supports typed <see cref="T:System.Data.DataTable" /> object generation by Visual Studio and the XSD.exe .NET Framework tool. This method is not intended to be used directly from your code.</summary>
		/// <typeparam name="TResult" />
		/// <returns>An <see cref="T:System.Data.EnumerableRowCollection" /> that contains each element of the source sequence converted to the specified type.</returns>
		public EnumerableRowCollection<TResult> Cast<TResult>()
		{
			return new EnumerableRowCollection<T>(this).Cast<TResult>();
		}
	}
	/// <summary>Contains the extension methods for the <see cref="T:System.Data.TypedTableBase`1" /> class.</summary>
	public static class TypedTableBaseExtensions
	{
		/// <summary>Filters a sequence of rows based on the specified predicate.</summary>
		/// <param name="source">A <see cref="T:System.Data.TypedTableBase`1" /> that contains the <see cref="T:System.Data.DataRow" /> elements to filter.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> that contains rows from the input sequence that satisfy the condition.</returns>
		public static EnumerableRowCollection<TRow> Where<TRow>(this TypedTableBase<TRow> source, Func<TRow, bool> predicate) where TRow : DataRow
		{
			DataSetUtil.CheckArgumentNull(source, "source");
			return new EnumerableRowCollection<TRow>(source).Where(predicate);
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.TypedTableBase`1" /> in ascending order according to the specified key.</summary>
		/// <param name="source">A <see cref="T:System.Data.TypedTableBase`1" /> that contains the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key.</returns>
		public static OrderedEnumerableRowCollection<TRow> OrderBy<TRow, TKey>(this TypedTableBase<TRow> source, Func<TRow, TKey> keySelector) where TRow : DataRow
		{
			DataSetUtil.CheckArgumentNull(source, "source");
			return new EnumerableRowCollection<TRow>(source).OrderBy(keySelector);
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.TypedTableBase`1" /> in ascending order according to the specified key and comparer.</summary>
		/// <param name="source">A <see cref="T:System.Data.TypedTableBase`1" /> that contains the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key and comparer.</returns>
		public static OrderedEnumerableRowCollection<TRow> OrderBy<TRow, TKey>(this TypedTableBase<TRow> source, Func<TRow, TKey> keySelector, IComparer<TKey> comparer) where TRow : DataRow
		{
			DataSetUtil.CheckArgumentNull(source, "source");
			return new EnumerableRowCollection<TRow>(source).OrderBy(keySelector, comparer);
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.TypedTableBase`1" /> in descending order according to the specified key.</summary>
		/// <param name="source">A <see cref="T:System.Data.TypedTableBase`1" /> that contains the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key.</returns>
		public static OrderedEnumerableRowCollection<TRow> OrderByDescending<TRow, TKey>(this TypedTableBase<TRow> source, Func<TRow, TKey> keySelector) where TRow : DataRow
		{
			DataSetUtil.CheckArgumentNull(source, "source");
			return new EnumerableRowCollection<TRow>(source).OrderByDescending(keySelector);
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.TypedTableBase`1" /> in descending order according to the specified key and comparer.</summary>
		/// <param name="source">A <see cref="T:System.Data.TypedTableBase`1" /> that contains the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key and comparer.</returns>
		public static OrderedEnumerableRowCollection<TRow> OrderByDescending<TRow, TKey>(this TypedTableBase<TRow> source, Func<TRow, TKey> keySelector, IComparer<TKey> comparer) where TRow : DataRow
		{
			DataSetUtil.CheckArgumentNull(source, "source");
			return new EnumerableRowCollection<TRow>(source).OrderByDescending(keySelector, comparer);
		}

		/// <summary>Projects each element of a <see cref="T:System.Data.TypedTableBase`1" /> into a new form.</summary>
		/// <param name="source">A <see cref="T:System.Data.TypedTableBase`1" /> that contains the <see cref="T:System.Data.DataRow" /> elements to invoke a transformation function upon.</param>
		/// <param name="selector">A transformation function to apply to each element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="S" />
		/// <returns>An <see cref="T:System.Data.EnumerableRowCollection`1" /> whose elements are the result of invoking the transformation function on each element of <paramref name="source" />.</returns>
		public static EnumerableRowCollection<S> Select<TRow, S>(this TypedTableBase<TRow> source, Func<TRow, S> selector) where TRow : DataRow
		{
			DataSetUtil.CheckArgumentNull(source, "source");
			return new EnumerableRowCollection<TRow>(source).Select(selector);
		}

		/// <summary>Enumerates the data row elements of the <see cref="T:System.Data.TypedTableBase`1" /> and returns an <see cref="T:System.Data.EnumerableRowCollection`1" /> object, where the generic parameter <paramref name="T" /> is <see cref="T:System.Data.DataRow" />. This object can be used in a LINQ expression or method query.</summary>
		/// <param name="source">The source <see cref="T:System.Data.TypedTableBase`1" /> to make enumerable.</param>
		/// <typeparam name="TRow">The type to convert the elements of the source to.</typeparam>
		/// <returns>An <see cref="T:System.Data.EnumerableRowCollection`1" /> object, where the generic parameter <paramref name="T" /> is <see cref="T:System.Data.DataRow" />.</returns>
		public static EnumerableRowCollection<TRow> AsEnumerable<TRow>(this TypedTableBase<TRow> source) where TRow : DataRow
		{
			DataSetUtil.CheckArgumentNull(source, "source");
			return new EnumerableRowCollection<TRow>(source);
		}

		/// <summary>Returns the element at a specified row in a sequence or a default value if the row is out of range.</summary>
		/// <param name="source">An enumerable object to return an element from.</param>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <typeparam name="TRow">The type of the elements or the row.</typeparam>
		/// <returns>The element at a specified row in a sequence.</returns>
		public static TRow ElementAtOrDefault<TRow>(this TypedTableBase<TRow> source, int index) where TRow : DataRow
		{
			if (index >= 0 && index < source.Rows.Count)
			{
				return (TRow)source.Rows[index];
			}
			return null;
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
