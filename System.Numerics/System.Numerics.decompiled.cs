using System;
using System.Buffers;
using System.Diagnostics;
using System.Globalization;
using System.Numerics.Hashing;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("System.Numerics.dll")]
[assembly: AssemblyDescription("System.Numerics.dll")]
[assembly: AssemblyDefaultAlias("System.Numerics.dll")]
[assembly: AssemblyCompany("Mono development team")]
[assembly: AssemblyProduct("Mono Common Language Infrastructure")]
[assembly: AssemblyCopyright("(c) Various Mono authors")]
[assembly: SatelliteContractVersion("4.0.0.0")]
[assembly: AssemblyInformationalVersion("4.6.57.0")]
[assembly: AssemblyFileVersion("4.6.57.0")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: CLSCompliant(true)]
[assembly: AssemblyDelaySign(true)]
[assembly: SecurityCritical]
[assembly: CompilationRelaxations(8)]
[assembly: ComVisible(false)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("4.0.0.0")]
[module: UnverifiableCode]
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
internal static class SR
{
	public const string Argument_BadFormatSpecifier = "Format specifier was invalid.";

	public const string Argument_InvalidNumberStyles = "An undefined NumberStyles value is being used.";

	public const string Argument_InvalidHexStyle = "With the AllowHexSpecifier bit set in the enum bit field, the only other valid bits that can be combined into the enum value must be a subset of those in HexNumber.";

	public const string Argument_MustBeBigInt = "The parameter must be a BigInteger.";

	public const string Format_TooLarge = "The value is too large to be represented by this format specifier.";

	public const string ArgumentOutOfRange_MustBeNonNeg = "The number must be greater than or equal to zero.";

	public const string Overflow_BigIntInfinity = "BigInteger cannot represent infinity.";

	public const string Overflow_NotANumber = "The value is not a number.";

	public const string Overflow_ParseBigInteger = "The value could not be parsed.";

	public const string Overflow_Int32 = "Value was either too large or too small for an Int32.";

	public const string Overflow_Int64 = "Value was either too large or too small for an Int64.";

	public const string Overflow_UInt32 = "Value was either too large or too small for a UInt32.";

	public const string Overflow_UInt64 = "Value was either too large or too small for a UInt64.";

	public const string Overflow_Decimal = "Value was either too large or too small for a Decimal.";

	public const string Arg_ArgumentOutOfRangeException = "Index was out of bounds:";

	public const string Arg_ElementsInSourceIsGreaterThanDestination = "Number of elements in source vector is greater than the destination array";

	public const string Arg_NullArgumentNullRef = "The method was called with a null array argument.";

	public const string Arg_TypeNotSupported = "Specified type is not supported";

	public const string ArgumentException_BufferNotFromPool = "The buffer is not associated with this pool and may not be returned to it.";

	public const string Overflow_Negative_Unsigned = "Negative values do not have an unsigned representation.";

	internal static string GetString(string name, params object[] args)
	{
		return GetString(CultureInfo.InvariantCulture, name, args);
	}

	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	internal static string GetString(string name)
	{
		return name;
	}

	internal static string GetString(CultureInfo culture, string name)
	{
		return name;
	}

	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	internal static string Format(CultureInfo ci, string resourceFormat, object p1, object p2)
	{
		return string.Format(ci, resourceFormat, p1, p2);
	}

	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}

	internal static string GetResourceString(string str)
	{
		return str;
	}
}
namespace System.Numerics
{
	/// <summary>Represents a 3x2 matrix.</summary>
	public struct Matrix3x2 : IEquatable<Matrix3x2>
	{
		/// <summary>The first element of the first row.</summary>
		public float M11;

		/// <summary>The second element of the first row.</summary>
		public float M12;

		/// <summary>The first element of the second row.</summary>
		public float M21;

		/// <summary>The second element of the second row.</summary>
		public float M22;

		/// <summary>The first element of the third row.</summary>
		public float M31;

		/// <summary>The second element of the third row.</summary>
		public float M32;

		private static readonly Matrix3x2 _identity = new Matrix3x2(1f, 0f, 0f, 1f, 0f, 0f);

		/// <summary>Gets the multiplicative identity matrix.</summary>
		/// <returns>The multiplicative identify matrix.</returns>
		public static Matrix3x2 Identity => _identity;

		/// <summary>Indicates whether the current matrix is the identity matrix.</summary>
		/// <returns>
		///   <see langword="true" /> if the current matrix is the identity matrix; otherwise, <see langword="false" />.</returns>
		public bool IsIdentity
		{
			get
			{
				if (M11 == 1f && M22 == 1f && M12 == 0f && M21 == 0f && M31 == 0f)
				{
					return M32 == 0f;
				}
				return false;
			}
		}

		/// <summary>Gets or sets the translation component of this matrix.</summary>
		/// <returns>The translation component of the current instance.</returns>
		public Vector2 Translation
		{
			get
			{
				return new Vector2(M31, M32);
			}
			set
			{
				M31 = value.X;
				M32 = value.Y;
			}
		}

		/// <summary>Creates a 3x2 matrix from the specified components.</summary>
		/// <param name="m11">The value to assign to the first element in the first row.</param>
		/// <param name="m12">The value to assign to the second element in the first row.</param>
		/// <param name="m21">The value to assign to the first element in the second row.</param>
		/// <param name="m22">The value to assign to the second element in the second row.</param>
		/// <param name="m31">The value to assign to the first element in the third row.</param>
		/// <param name="m32">The value to assign to the second element in the third row.</param>
		public Matrix3x2(float m11, float m12, float m21, float m22, float m31, float m32)
		{
			M11 = m11;
			M12 = m12;
			M21 = m21;
			M22 = m22;
			M31 = m31;
			M32 = m32;
		}

		/// <summary>Creates a translation matrix from the specified 2-dimensional vector.</summary>
		/// <param name="position">The translation position.</param>
		/// <returns>The translation matrix.</returns>
		public static Matrix3x2 CreateTranslation(Vector2 position)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = 1f;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M31 = position.X;
			result.M32 = position.Y;
			return result;
		}

		/// <summary>Creates a translation matrix from the specified X and Y components.</summary>
		/// <param name="xPosition">The X position.</param>
		/// <param name="yPosition">The Y position.</param>
		/// <returns>The translation matrix.</returns>
		public static Matrix3x2 CreateTranslation(float xPosition, float yPosition)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = 1f;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M31 = xPosition;
			result.M32 = yPosition;
			return result;
		}

		/// <summary>Creates a scaling matrix from the specified X and Y components.</summary>
		/// <param name="xScale">The value to scale by on the X axis.</param>
		/// <param name="yScale">The value to scale by on the Y axis.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix3x2 CreateScale(float xScale, float yScale)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = xScale;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = yScale;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a scaling matrix that is offset by a given center point.</summary>
		/// <param name="xScale">The value to scale by on the X axis.</param>
		/// <param name="yScale">The value to scale by on the Y axis.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix3x2 CreateScale(float xScale, float yScale, Vector2 centerPoint)
		{
			float m = centerPoint.X * (1f - xScale);
			float m2 = centerPoint.Y * (1f - yScale);
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = xScale;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = yScale;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Creates a scaling matrix from the specified vector scale.</summary>
		/// <param name="scales">The scale to use.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix3x2 CreateScale(Vector2 scales)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = scales.X;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = scales.Y;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a scaling matrix from the specified vector scale with an offset from the specified center point.</summary>
		/// <param name="scales">The scale to use.</param>
		/// <param name="centerPoint">The center offset.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix3x2 CreateScale(Vector2 scales, Vector2 centerPoint)
		{
			float m = centerPoint.X * (1f - scales.X);
			float m2 = centerPoint.Y * (1f - scales.Y);
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = scales.X;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = scales.Y;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Creates a scaling matrix that scales uniformly with the given scale.</summary>
		/// <param name="scale">The uniform scale to use.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix3x2 CreateScale(float scale)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = scale;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = scale;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a scaling matrix that scales uniformly with the specified scale with an offset from the specified center.</summary>
		/// <param name="scale">The uniform scale to use.</param>
		/// <param name="centerPoint">The center offset.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix3x2 CreateScale(float scale, Vector2 centerPoint)
		{
			float m = centerPoint.X * (1f - scale);
			float m2 = centerPoint.Y * (1f - scale);
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = scale;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = scale;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Creates a skew matrix from the specified angles in radians.</summary>
		/// <param name="radiansX">The X angle, in radians.</param>
		/// <param name="radiansY">The Y angle, in radians.</param>
		/// <returns>The skew matrix.</returns>
		public static Matrix3x2 CreateSkew(float radiansX, float radiansY)
		{
			float m = MathF.Tan(radiansX);
			float m2 = MathF.Tan(radiansY);
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = 1f;
			result.M12 = m2;
			result.M21 = m;
			result.M22 = 1f;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a skew matrix from the specified angles in radians and a center point.</summary>
		/// <param name="radiansX">The X angle, in radians.</param>
		/// <param name="radiansY">The Y angle, in radians.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The skew matrix.</returns>
		public static Matrix3x2 CreateSkew(float radiansX, float radiansY, Vector2 centerPoint)
		{
			float num = MathF.Tan(radiansX);
			float num2 = MathF.Tan(radiansY);
			float m = (0f - centerPoint.Y) * num;
			float m2 = (0f - centerPoint.X) * num2;
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = 1f;
			result.M12 = num2;
			result.M21 = num;
			result.M22 = 1f;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Creates a rotation matrix using the given rotation in radians.</summary>
		/// <param name="radians">The amount of rotation, in radians.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix3x2 CreateRotation(float radians)
		{
			radians = MathF.IEEERemainder(radians, MathF.PI * 2f);
			float num;
			float num2;
			if (radians > -1.7453294E-05f && radians < 1.7453294E-05f)
			{
				num = 1f;
				num2 = 0f;
			}
			else if (radians > 1.570779f && radians < 1.5708138f)
			{
				num = 0f;
				num2 = 1f;
			}
			else if (radians < -3.1415753f || radians > 3.1415753f)
			{
				num = -1f;
				num2 = 0f;
			}
			else if (radians > -1.5708138f && radians < -1.570779f)
			{
				num = 0f;
				num2 = -1f;
			}
			else
			{
				num = MathF.Cos(radians);
				num2 = MathF.Sin(radians);
			}
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = num;
			result.M12 = num2;
			result.M21 = 0f - num2;
			result.M22 = num;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a rotation matrix using the specified rotation in radians and a center point.</summary>
		/// <param name="radians">The amount of rotation, in radians.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix3x2 CreateRotation(float radians, Vector2 centerPoint)
		{
			radians = MathF.IEEERemainder(radians, MathF.PI * 2f);
			float num;
			float num2;
			if (radians > -1.7453294E-05f && radians < 1.7453294E-05f)
			{
				num = 1f;
				num2 = 0f;
			}
			else if (radians > 1.570779f && radians < 1.5708138f)
			{
				num = 0f;
				num2 = 1f;
			}
			else if (radians < -3.1415753f || radians > 3.1415753f)
			{
				num = -1f;
				num2 = 0f;
			}
			else if (radians > -1.5708138f && radians < -1.570779f)
			{
				num = 0f;
				num2 = -1f;
			}
			else
			{
				num = MathF.Cos(radians);
				num2 = MathF.Sin(radians);
			}
			float m = centerPoint.X * (1f - num) + centerPoint.Y * num2;
			float m2 = centerPoint.Y * (1f - num) - centerPoint.X * num2;
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = num;
			result.M12 = num2;
			result.M21 = 0f - num2;
			result.M22 = num;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Calculates the determinant for this matrix.</summary>
		/// <returns>The determinant.</returns>
		public float GetDeterminant()
		{
			return M11 * M22 - M21 * M12;
		}

		/// <summary>Inverts the specified matrix. The return value indicates whether the operation succeeded.</summary>
		/// <param name="matrix">The matrix to invert.</param>
		/// <param name="result">When this method returns, contains the inverted matrix if the operation succeeded.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="matrix" /> was converted successfully; otherwise,  <see langword="false" />.</returns>
		public static bool Invert(Matrix3x2 matrix, out Matrix3x2 result)
		{
			float num = matrix.M11 * matrix.M22 - matrix.M21 * matrix.M12;
			if (MathF.Abs(num) < float.Epsilon)
			{
				result = new Matrix3x2(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);
				return false;
			}
			float num2 = 1f / num;
			result.M11 = matrix.M22 * num2;
			result.M12 = (0f - matrix.M12) * num2;
			result.M21 = (0f - matrix.M21) * num2;
			result.M22 = matrix.M11 * num2;
			result.M31 = (matrix.M21 * matrix.M32 - matrix.M31 * matrix.M22) * num2;
			result.M32 = (matrix.M31 * matrix.M12 - matrix.M11 * matrix.M32) * num2;
			return true;
		}

		/// <summary>Performs a linear interpolation from one matrix to a second matrix based on a value that specifies the weighting of the second matrix.</summary>
		/// <param name="matrix1">The first matrix.</param>
		/// <param name="matrix2">The second matrix.</param>
		/// <param name="amount">The relative weighting of <paramref name="matrix2" />.</param>
		/// <returns>The interpolated matrix.</returns>
		public static Matrix3x2 Lerp(Matrix3x2 matrix1, Matrix3x2 matrix2, float amount)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
			result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
			result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
			result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
			result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
			result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
			return result;
		}

		/// <summary>Negates the specified matrix by multiplying all its values by -1.</summary>
		/// <param name="value">The matrix to negate.</param>
		/// <returns>The negated matrix.</returns>
		public static Matrix3x2 Negate(Matrix3x2 value)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = 0f - value.M11;
			result.M12 = 0f - value.M12;
			result.M21 = 0f - value.M21;
			result.M22 = 0f - value.M22;
			result.M31 = 0f - value.M31;
			result.M32 = 0f - value.M32;
			return result;
		}

		/// <summary>Adds each element in one matrix with its corresponding element in a second matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix that contains the summed values of <paramref name="value1" /> and <paramref name="value2" />.</returns>
		public static Matrix3x2 Add(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 + value2.M11;
			result.M12 = value1.M12 + value2.M12;
			result.M21 = value1.M21 + value2.M21;
			result.M22 = value1.M22 + value2.M22;
			result.M31 = value1.M31 + value2.M31;
			result.M32 = value1.M32 + value2.M32;
			return result;
		}

		/// <summary>Subtracts each element in a second matrix from its corresponding element in a first matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix containing the values that result from subtracting each element in <paramref name="value2" /> from its corresponding element in <paramref name="value1" />.</returns>
		public static Matrix3x2 Subtract(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 - value2.M11;
			result.M12 = value1.M12 - value2.M12;
			result.M21 = value1.M21 - value2.M21;
			result.M22 = value1.M22 - value2.M22;
			result.M31 = value1.M31 - value2.M31;
			result.M32 = value1.M32 - value2.M32;
			return result;
		}

		/// <summary>Returns the matrix that results from multiplying two matrices together.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The product matrix.</returns>
		public static Matrix3x2 Multiply(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21;
			result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22;
			result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21;
			result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22;
			result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value2.M31;
			result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value2.M32;
			return result;
		}

		/// <summary>Returns the matrix that results from scaling all the elements of a specified matrix by a scalar factor.</summary>
		/// <param name="value1">The matrix to scale.</param>
		/// <param name="value2">The scaling value to use.</param>
		/// <returns>The scaled matrix.</returns>
		public static Matrix3x2 Multiply(Matrix3x2 value1, float value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 * value2;
			result.M12 = value1.M12 * value2;
			result.M21 = value1.M21 * value2;
			result.M22 = value1.M22 * value2;
			result.M31 = value1.M31 * value2;
			result.M32 = value1.M32 * value2;
			return result;
		}

		/// <summary>Negates the specified matrix by multiplying all its values by -1.</summary>
		/// <param name="value">The matrix to negate.</param>
		/// <returns>The negated matrix.</returns>
		public static Matrix3x2 operator -(Matrix3x2 value)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = 0f - value.M11;
			result.M12 = 0f - value.M12;
			result.M21 = 0f - value.M21;
			result.M22 = 0f - value.M22;
			result.M31 = 0f - value.M31;
			result.M32 = 0f - value.M32;
			return result;
		}

		/// <summary>Adds each element in one matrix with its corresponding element in a second matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix that contains the summed values.</returns>
		public static Matrix3x2 operator +(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 + value2.M11;
			result.M12 = value1.M12 + value2.M12;
			result.M21 = value1.M21 + value2.M21;
			result.M22 = value1.M22 + value2.M22;
			result.M31 = value1.M31 + value2.M31;
			result.M32 = value1.M32 + value2.M32;
			return result;
		}

		/// <summary>Subtracts each element in a second matrix from its corresponding element in a first matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix containing the values that result from subtracting each element in <paramref name="value2" /> from its corresponding element in <paramref name="value1" />.</returns>
		public static Matrix3x2 operator -(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 - value2.M11;
			result.M12 = value1.M12 - value2.M12;
			result.M21 = value1.M21 - value2.M21;
			result.M22 = value1.M22 - value2.M22;
			result.M31 = value1.M31 - value2.M31;
			result.M32 = value1.M32 - value2.M32;
			return result;
		}

		/// <summary>Returns the matrix that results from multiplying two matrices together.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The product matrix.</returns>
		public static Matrix3x2 operator *(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21;
			result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22;
			result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21;
			result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22;
			result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value2.M31;
			result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value2.M32;
			return result;
		}

		/// <summary>Returns the matrix that results from scaling all the elements of a specified matrix by a scalar factor.</summary>
		/// <param name="value1">The matrix to scale.</param>
		/// <param name="value2">The scaling value to use.</param>
		/// <returns>The scaled matrix.</returns>
		public static Matrix3x2 operator *(Matrix3x2 value1, float value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 * value2;
			result.M12 = value1.M12 * value2;
			result.M21 = value1.M21 * value2;
			result.M22 = value1.M22 * value2;
			result.M31 = value1.M31 * value2;
			result.M32 = value1.M32 * value2;
			return result;
		}

		/// <summary>Returns a value that indicates whether the specified matrices are equal.</summary>
		/// <param name="value1">The first matrix to compare.</param>
		/// <param name="value2">The second matrix to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> and <paramref name="value2" /> are equal; otherwise, <see langword="false" />.</returns>
		public static bool operator ==(Matrix3x2 value1, Matrix3x2 value2)
		{
			if (value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M12 == value2.M12 && value1.M21 == value2.M21 && value1.M31 == value2.M31)
			{
				return value1.M32 == value2.M32;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether the specified matrices are not equal.</summary>
		/// <param name="value1">The first matrix to compare.</param>
		/// <param name="value2">The second matrix to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> and <paramref name="value2" /> are not equal; otherwise, <see langword="false" />.</returns>
		public static bool operator !=(Matrix3x2 value1, Matrix3x2 value2)
		{
			if (value1.M11 == value2.M11 && value1.M12 == value2.M12 && value1.M21 == value2.M21 && value1.M22 == value2.M22 && value1.M31 == value2.M31)
			{
				return value1.M32 != value2.M32;
			}
			return true;
		}

		/// <summary>Returns a value that indicates whether this instance and another 3x2 matrix are equal.</summary>
		/// <param name="other">The other matrix.</param>
		/// <returns>
		///   <see langword="true" /> if the two matrices are equal; otherwise, <see langword="false" />.</returns>
		public bool Equals(Matrix3x2 other)
		{
			if (M11 == other.M11 && M22 == other.M22 && M12 == other.M12 && M21 == other.M21 && M31 == other.M31)
			{
				return M32 == other.M32;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance and <paramref name="obj" /> are equal; otherwise, <see langword="false" />. If <paramref name="obj" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix3x2)
			{
				return Equals((Matrix3x2)obj);
			}
			return false;
		}

		/// <summary>Returns a string that represents this matrix.</summary>
		/// <returns>The string representation of this matrix.</returns>
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{ {{M11:{0} M12:{1}}} {{M21:{2} M22:{3}}} {{M31:{4} M32:{5}}} }}", M11.ToString(currentCulture), M12.ToString(currentCulture), M21.ToString(currentCulture), M22.ToString(currentCulture), M31.ToString(currentCulture), M32.ToString(currentCulture));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			return M11.GetHashCode() + M12.GetHashCode() + M21.GetHashCode() + M22.GetHashCode() + M31.GetHashCode() + M32.GetHashCode();
		}
	}
	/// <summary>Represents a 4x4 matrix.</summary>
	public struct Matrix4x4 : IEquatable<Matrix4x4>
	{
		private struct CanonicalBasis
		{
			public Vector3 Row0;

			public Vector3 Row1;

			public Vector3 Row2;
		}

		private struct VectorBasis
		{
			public unsafe Vector3* Element0;

			public unsafe Vector3* Element1;

			public unsafe Vector3* Element2;
		}

		/// <summary>The first element of the first row.</summary>
		public float M11;

		/// <summary>The second element of the first row.</summary>
		public float M12;

		/// <summary>The third element of the first row.</summary>
		public float M13;

		/// <summary>The fourth element of the first row.</summary>
		public float M14;

		/// <summary>The first element of the second row.</summary>
		public float M21;

		/// <summary>The second element of the second row.</summary>
		public float M22;

		/// <summary>The third element of the second row.</summary>
		public float M23;

		/// <summary>The fourth element of the second row.</summary>
		public float M24;

		/// <summary>The first element of the third row.</summary>
		public float M31;

		/// <summary>The second element of the third row.</summary>
		public float M32;

		/// <summary>The third element of the third row.</summary>
		public float M33;

		/// <summary>The fourth element of the third row.</summary>
		public float M34;

		/// <summary>The first element of the fourth row.</summary>
		public float M41;

		/// <summary>The second element of the fourth row.</summary>
		public float M42;

		/// <summary>The third element of the fourth row.</summary>
		public float M43;

		/// <summary>The fourth element of the fourth row.</summary>
		public float M44;

		private static readonly Matrix4x4 _identity = new Matrix4x4(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);

		/// <summary>Gets the multiplicative identity matrix.</summary>
		/// <returns>Gets the multiplicative identity matrix.</returns>
		public static Matrix4x4 Identity => _identity;

		/// <summary>Indicates whether the current matrix is the identity matrix.</summary>
		/// <returns>
		///   <see langword="true" /> if the current matrix is the identity matrix; otherwise, <see langword="false" />.</returns>
		public bool IsIdentity
		{
			get
			{
				if (M11 == 1f && M22 == 1f && M33 == 1f && M44 == 1f && M12 == 0f && M13 == 0f && M14 == 0f && M21 == 0f && M23 == 0f && M24 == 0f && M31 == 0f && M32 == 0f && M34 == 0f && M41 == 0f && M42 == 0f)
				{
					return M43 == 0f;
				}
				return false;
			}
		}

		/// <summary>Gets or sets the translation component of this matrix.</summary>
		/// <returns>The translation component of the current instance.</returns>
		public Vector3 Translation
		{
			get
			{
				return new Vector3(M41, M42, M43);
			}
			set
			{
				M41 = value.X;
				M42 = value.Y;
				M43 = value.Z;
			}
		}

		/// <summary>Creates a 4x4 matrix from the specified components.</summary>
		/// <param name="m11">The value to assign to the first element in the first row.</param>
		/// <param name="m12">The value to assign to the second element in the first row.</param>
		/// <param name="m13">The value to assign to the third element in the first row.</param>
		/// <param name="m14">The value to assign to the fourth element in the first row.</param>
		/// <param name="m21">The value to assign to the first element in the second row.</param>
		/// <param name="m22">The value to assign to the second element in the second row.</param>
		/// <param name="m23">The value to assign to the third element in the second row.</param>
		/// <param name="m24">The value to assign to the third element in the second row.</param>
		/// <param name="m31">The value to assign to the first element in the third row.</param>
		/// <param name="m32">The value to assign to the second element in the third row.</param>
		/// <param name="m33">The value to assign to the third element in the third row.</param>
		/// <param name="m34">The value to assign to the fourth element in the third row.</param>
		/// <param name="m41">The value to assign to the first element in the fourth row.</param>
		/// <param name="m42">The value to assign to the second element in the fourth row.</param>
		/// <param name="m43">The value to assign to the third element in the fourth row.</param>
		/// <param name="m44">The value to assign to the fourth element in the fourth row.</param>
		public Matrix4x4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
		{
			M11 = m11;
			M12 = m12;
			M13 = m13;
			M14 = m14;
			M21 = m21;
			M22 = m22;
			M23 = m23;
			M24 = m24;
			M31 = m31;
			M32 = m32;
			M33 = m33;
			M34 = m34;
			M41 = m41;
			M42 = m42;
			M43 = m43;
			M44 = m44;
		}

		/// <summary>Creates a <see cref="T:System.Numerics.Matrix4x4" /> object from a specified <see cref="T:System.Numerics.Matrix3x2" /> object.</summary>
		/// <param name="value">A 3x2 matrix.</param>
		public Matrix4x4(Matrix3x2 value)
		{
			M11 = value.M11;
			M12 = value.M12;
			M13 = 0f;
			M14 = 0f;
			M21 = value.M21;
			M22 = value.M22;
			M23 = 0f;
			M24 = 0f;
			M31 = 0f;
			M32 = 0f;
			M33 = 1f;
			M34 = 0f;
			M41 = value.M31;
			M42 = value.M32;
			M43 = 0f;
			M44 = 1f;
		}

		/// <summary>Creates a spherical billboard that rotates around a specified object position.</summary>
		/// <param name="objectPosition">The position of the object that the billboard will rotate around.</param>
		/// <param name="cameraPosition">The position of the camera.</param>
		/// <param name="cameraUpVector">The up vector of the camera.</param>
		/// <param name="cameraForwardVector">The forward vector of the camera.</param>
		/// <returns>The created billboard.</returns>
		public static Matrix4x4 CreateBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector, Vector3 cameraForwardVector)
		{
			Vector3 left = new Vector3(objectPosition.X - cameraPosition.X, objectPosition.Y - cameraPosition.Y, objectPosition.Z - cameraPosition.Z);
			float num = left.LengthSquared();
			left = ((!(num < 0.0001f)) ? Vector3.Multiply(left, 1f / MathF.Sqrt(num)) : (-cameraForwardVector));
			Vector3 vector = Vector3.Normalize(Vector3.Cross(cameraUpVector, left));
			Vector3 vector2 = Vector3.Cross(left, vector);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = vector.X;
			result.M12 = vector.Y;
			result.M13 = vector.Z;
			result.M14 = 0f;
			result.M21 = vector2.X;
			result.M22 = vector2.Y;
			result.M23 = vector2.Z;
			result.M24 = 0f;
			result.M31 = left.X;
			result.M32 = left.Y;
			result.M33 = left.Z;
			result.M34 = 0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a cylindrical billboard that rotates around a specified axis.</summary>
		/// <param name="objectPosition">The position of the object that the billboard will rotate around.</param>
		/// <param name="cameraPosition">The position of the camera.</param>
		/// <param name="rotateAxis">The axis to rotate the billboard around.</param>
		/// <param name="cameraForwardVector">The forward vector of the camera.</param>
		/// <param name="objectForwardVector">The forward vector of the object.</param>
		/// <returns>The billboard matrix.</returns>
		public static Matrix4x4 CreateConstrainedBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 rotateAxis, Vector3 cameraForwardVector, Vector3 objectForwardVector)
		{
			Vector3 left = new Vector3(objectPosition.X - cameraPosition.X, objectPosition.Y - cameraPosition.Y, objectPosition.Z - cameraPosition.Z);
			float num = left.LengthSquared();
			left = ((!(num < 0.0001f)) ? Vector3.Multiply(left, 1f / MathF.Sqrt(num)) : (-cameraForwardVector));
			Vector3 vector = rotateAxis;
			Vector3 vector3;
			Vector3 vector2;
			if (MathF.Abs(Vector3.Dot(rotateAxis, left)) > 0.99825466f)
			{
				vector2 = objectForwardVector;
				if (MathF.Abs(Vector3.Dot(rotateAxis, vector2)) > 0.99825466f)
				{
					vector2 = ((MathF.Abs(rotateAxis.Z) > 0.99825466f) ? new Vector3(1f, 0f, 0f) : new Vector3(0f, 0f, -1f));
				}
				vector3 = Vector3.Normalize(Vector3.Cross(rotateAxis, vector2));
				vector2 = Vector3.Normalize(Vector3.Cross(vector3, rotateAxis));
			}
			else
			{
				vector3 = Vector3.Normalize(Vector3.Cross(rotateAxis, left));
				vector2 = Vector3.Normalize(Vector3.Cross(vector3, vector));
			}
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = vector3.X;
			result.M12 = vector3.Y;
			result.M13 = vector3.Z;
			result.M14 = 0f;
			result.M21 = vector.X;
			result.M22 = vector.Y;
			result.M23 = vector.Z;
			result.M24 = 0f;
			result.M31 = vector2.X;
			result.M32 = vector2.Y;
			result.M33 = vector2.Z;
			result.M34 = 0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a translation matrix from the specified 3-dimensional vector.</summary>
		/// <param name="position">The amount to translate in each axis.</param>
		/// <returns>The translation matrix.</returns>
		public static Matrix4x4 CreateTranslation(Vector3 position)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a translation matrix from the specified X, Y, and Z components.</summary>
		/// <param name="xPosition">The amount to translate on the X axis.</param>
		/// <param name="yPosition">The amount to translate on the Y axis.</param>
		/// <param name="zPosition">The amount to translate on the Z axis.</param>
		/// <returns>The translation matrix.</returns>
		public static Matrix4x4 CreateTranslation(float xPosition, float yPosition, float zPosition)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = xPosition;
			result.M42 = yPosition;
			result.M43 = zPosition;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a scaling matrix from the specified X, Y, and Z components.</summary>
		/// <param name="xScale">The value to scale by on the X axis.</param>
		/// <param name="yScale">The value to scale by on the Y axis.</param>
		/// <param name="zScale">The value to scale by on the Z axis.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix4x4 CreateScale(float xScale, float yScale, float zScale)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = xScale;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = yScale;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = zScale;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a scaling matrix that is offset by a given center point.</summary>
		/// <param name="xScale">The value to scale by on the X axis.</param>
		/// <param name="yScale">The value to scale by on the Y axis.</param>
		/// <param name="zScale">The value to scale by on the Z axis.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix4x4 CreateScale(float xScale, float yScale, float zScale, Vector3 centerPoint)
		{
			float m = centerPoint.X * (1f - xScale);
			float m2 = centerPoint.Y * (1f - yScale);
			float m3 = centerPoint.Z * (1f - zScale);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = xScale;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = yScale;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = zScale;
			result.M34 = 0f;
			result.M41 = m;
			result.M42 = m2;
			result.M43 = m3;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a scaling matrix from the specified vector scale.</summary>
		/// <param name="scales">The scale to use.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix4x4 CreateScale(Vector3 scales)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = scales.X;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = scales.Y;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = scales.Z;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a scaling matrix with a center point.</summary>
		/// <param name="scales">The vector that contains the amount to scale on each axis.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix4x4 CreateScale(Vector3 scales, Vector3 centerPoint)
		{
			float m = centerPoint.X * (1f - scales.X);
			float m2 = centerPoint.Y * (1f - scales.Y);
			float m3 = centerPoint.Z * (1f - scales.Z);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = scales.X;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = scales.Y;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = scales.Z;
			result.M34 = 0f;
			result.M41 = m;
			result.M42 = m2;
			result.M43 = m3;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a uniform scaling matrix that scale equally on each axis.</summary>
		/// <param name="scale">The uniform scaling factor.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix4x4 CreateScale(float scale)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = scale;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = scale;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = scale;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a uniform scaling matrix that scales equally on each axis with a center point.</summary>
		/// <param name="scale">The uniform scaling factor.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix4x4 CreateScale(float scale, Vector3 centerPoint)
		{
			float m = centerPoint.X * (1f - scale);
			float m2 = centerPoint.Y * (1f - scale);
			float m3 = centerPoint.Z * (1f - scale);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = scale;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = scale;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = scale;
			result.M34 = 0f;
			result.M41 = m;
			result.M42 = m2;
			result.M43 = m3;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a matrix for rotating points around the X axis.</summary>
		/// <param name="radians">The amount, in radians, by which to rotate around the X axis.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateRotationX(float radians)
		{
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = num;
			result.M23 = num2;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f - num2;
			result.M33 = num;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a matrix for rotating points around the X axis from a center point.</summary>
		/// <param name="radians">The amount, in radians, by which to rotate around the X axis.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateRotationX(float radians, Vector3 centerPoint)
		{
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			float m = centerPoint.Y * (1f - num) + centerPoint.Z * num2;
			float m2 = centerPoint.Z * (1f - num) - centerPoint.Y * num2;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = num;
			result.M23 = num2;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f - num2;
			result.M33 = num;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = m;
			result.M43 = m2;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a matrix for rotating points around the Y axis.</summary>
		/// <param name="radians">The amount, in radians, by which to rotate around the Y-axis.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateRotationY(float radians)
		{
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = num;
			result.M12 = 0f;
			result.M13 = 0f - num2;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = num2;
			result.M32 = 0f;
			result.M33 = num;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>The amount, in radians, by which to rotate around the Y axis from a center point.</summary>
		/// <param name="radians">The amount, in radians, by which to rotate around the Y-axis.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateRotationY(float radians, Vector3 centerPoint)
		{
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			float m = centerPoint.X * (1f - num) - centerPoint.Z * num2;
			float m2 = centerPoint.Z * (1f - num) + centerPoint.X * num2;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = num;
			result.M12 = 0f;
			result.M13 = 0f - num2;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = num2;
			result.M32 = 0f;
			result.M33 = num;
			result.M34 = 0f;
			result.M41 = m;
			result.M42 = 0f;
			result.M43 = m2;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a matrix for rotating points around the Z axis.</summary>
		/// <param name="radians">The amount, in radians, by which to rotate around the Z-axis.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateRotationZ(float radians)
		{
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = num;
			result.M12 = num2;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f - num2;
			result.M22 = num;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a matrix for rotating points around the Z axis from a center point.</summary>
		/// <param name="radians">The amount, in radians, by which to rotate around the Z-axis.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateRotationZ(float radians, Vector3 centerPoint)
		{
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			float m = centerPoint.X * (1f - num) + centerPoint.Y * num2;
			float m2 = centerPoint.Y * (1f - num) - centerPoint.X * num2;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = num;
			result.M12 = num2;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f - num2;
			result.M22 = num;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = m;
			result.M42 = m2;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a matrix that rotates around an arbitrary vector.</summary>
		/// <param name="axis">The axis to rotate around.</param>
		/// <param name="angle">The angle to rotate around <paramref name="axis" />, in radians.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateFromAxisAngle(Vector3 axis, float angle)
		{
			float x = axis.X;
			float y = axis.Y;
			float z = axis.Z;
			float num = MathF.Sin(angle);
			float num2 = MathF.Cos(angle);
			float num3 = x * x;
			float num4 = y * y;
			float num5 = z * z;
			float num6 = x * y;
			float num7 = x * z;
			float num8 = y * z;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = num3 + num2 * (1f - num3);
			result.M12 = num6 - num2 * num6 + num * z;
			result.M13 = num7 - num2 * num7 - num * y;
			result.M14 = 0f;
			result.M21 = num6 - num2 * num6 - num * z;
			result.M22 = num4 + num2 * (1f - num4);
			result.M23 = num8 - num2 * num8 + num * x;
			result.M24 = 0f;
			result.M31 = num7 - num2 * num7 + num * y;
			result.M32 = num8 - num2 * num8 - num * x;
			result.M33 = num5 + num2 * (1f - num5);
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a perspective projection matrix based on a field of view, aspect ratio, and near and far view plane distances.</summary>
		/// <param name="fieldOfView">The field of view in the y direction, in radians.</param>
		/// <param name="aspectRatio">The aspect ratio, defined as view space width divided by height.</param>
		/// <param name="nearPlaneDistance">The distance to the near view plane.</param>
		/// <param name="farPlaneDistance">The distance to the far view plane.</param>
		/// <returns>The perspective projection matrix.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="fieldOfView" /> is less than or equal to zero.  
		/// -or-  
		/// <paramref name="fieldOfView" /> is greater than or equal to <see cref="F:System.Math.PI" />.  
		/// <paramref name="nearPlaneDistance" /> is less than or equal to zero.  
		/// -or-  
		/// <paramref name="farPlaneDistance" /> is less than or equal to zero.  
		/// -or-  
		/// <paramref name="nearPlaneDistance" /> is greater than or equal to <paramref name="farPlaneDistance" />.</exception>
		public static Matrix4x4 CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
		{
			if (fieldOfView <= 0f || fieldOfView >= MathF.PI)
			{
				throw new ArgumentOutOfRangeException("fieldOfView");
			}
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			float num = 1f / MathF.Tan(fieldOfView * 0.5f);
			float m = num / aspectRatio;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = m;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = num;
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M31 = (result.M32 = 0f);
			float num2 = (result.M33 = (float.IsPositiveInfinity(farPlaneDistance) ? (-1f) : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance))));
			result.M34 = -1f;
			result.M41 = (result.M42 = (result.M44 = 0f));
			result.M43 = nearPlaneDistance * num2;
			return result;
		}

		/// <summary>Creates a perspective projection matrix from the given view volume dimensions.</summary>
		/// <param name="width">The width of the view volume at the near view plane.</param>
		/// <param name="height">The height of the view volume at the near view plane.</param>
		/// <param name="nearPlaneDistance">The distance to the near view plane.</param>
		/// <param name="farPlaneDistance">The distance to the far view plane.</param>
		/// <returns>The perspective projection matrix.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="nearPlaneDistance" /> is less than or equal to zero.  
		/// -or-  
		/// <paramref name="farPlaneDistance" /> is less than or equal to zero.  
		/// -or-  
		/// <paramref name="nearPlaneDistance" /> is greater than or equal to <paramref name="farPlaneDistance" />.</exception>
		public static Matrix4x4 CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance)
		{
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 2f * nearPlaneDistance / width;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f * nearPlaneDistance / height;
			result.M21 = (result.M23 = (result.M24 = 0f));
			float num = (result.M33 = (float.IsPositiveInfinity(farPlaneDistance) ? (-1f) : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance))));
			result.M31 = (result.M32 = 0f);
			result.M34 = -1f;
			result.M41 = (result.M42 = (result.M44 = 0f));
			result.M43 = nearPlaneDistance * num;
			return result;
		}

		/// <summary>Creates a customized perspective projection matrix.</summary>
		/// <param name="left">The minimum x-value of the view volume at the near view plane.</param>
		/// <param name="right">The maximum x-value of the view volume at the near view plane.</param>
		/// <param name="bottom">The minimum y-value of the view volume at the near view plane.</param>
		/// <param name="top">The maximum y-value of the view volume at the near view plane.</param>
		/// <param name="nearPlaneDistance">The distance to the near view plane.</param>
		/// <param name="farPlaneDistance">The distance to the far view plane.</param>
		/// <returns>The perspective projection matrix.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="nearPlaneDistance" /> is less than or equal to zero.  
		/// -or-  
		/// <paramref name="farPlaneDistance" /> is less than or equal to zero.  
		/// -or-  
		/// <paramref name="nearPlaneDistance" /> is greater than or equal to <paramref name="farPlaneDistance" />.</exception>
		public static Matrix4x4 CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance)
		{
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 2f * nearPlaneDistance / (right - left);
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f * nearPlaneDistance / (top - bottom);
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M31 = (left + right) / (right - left);
			result.M32 = (top + bottom) / (top - bottom);
			float num = (result.M33 = (float.IsPositiveInfinity(farPlaneDistance) ? (-1f) : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance))));
			result.M34 = -1f;
			result.M43 = nearPlaneDistance * num;
			result.M41 = (result.M42 = (result.M44 = 0f));
			return result;
		}

		/// <summary>Creates an orthographic perspective matrix from the given view volume dimensions.</summary>
		/// <param name="width">The width of the view volume.</param>
		/// <param name="height">The height of the view volume.</param>
		/// <param name="zNearPlane">The minimum Z-value of the view volume.</param>
		/// <param name="zFarPlane">The maximum Z-value of the view volume.</param>
		/// <returns>The orthographic projection matrix.</returns>
		public static Matrix4x4 CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 2f / width;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f / height;
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M33 = 1f / (zNearPlane - zFarPlane);
			result.M31 = (result.M32 = (result.M34 = 0f));
			result.M41 = (result.M42 = 0f);
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a customized orthographic projection matrix.</summary>
		/// <param name="left">The minimum X-value of the view volume.</param>
		/// <param name="right">The maximum X-value of the view volume.</param>
		/// <param name="bottom">The minimum Y-value of the view volume.</param>
		/// <param name="top">The maximum Y-value of the view volume.</param>
		/// <param name="zNearPlane">The minimum Z-value of the view volume.</param>
		/// <param name="zFarPlane">The maximum Z-value of the view volume.</param>
		/// <returns>The orthographic projection matrix.</returns>
		public static Matrix4x4 CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 2f / (right - left);
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f / (top - bottom);
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M33 = 1f / (zNearPlane - zFarPlane);
			result.M31 = (result.M32 = (result.M34 = 0f));
			result.M41 = (left + right) / (left - right);
			result.M42 = (top + bottom) / (bottom - top);
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a view matrix.</summary>
		/// <param name="cameraPosition">The position of the camera.</param>
		/// <param name="cameraTarget">The target towards which the camera is pointing.</param>
		/// <param name="cameraUpVector">The direction that is "up" from the camera's point of view.</param>
		/// <returns>The view matrix.</returns>
		public static Matrix4x4 CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
		{
			Vector3 vector = Vector3.Normalize(cameraPosition - cameraTarget);
			Vector3 vector2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, vector));
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = vector2.X;
			result.M12 = vector3.X;
			result.M13 = vector.X;
			result.M14 = 0f;
			result.M21 = vector2.Y;
			result.M22 = vector3.Y;
			result.M23 = vector.Y;
			result.M24 = 0f;
			result.M31 = vector2.Z;
			result.M32 = vector3.Z;
			result.M33 = vector.Z;
			result.M34 = 0f;
			result.M41 = 0f - Vector3.Dot(vector2, cameraPosition);
			result.M42 = 0f - Vector3.Dot(vector3, cameraPosition);
			result.M43 = 0f - Vector3.Dot(vector, cameraPosition);
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a world matrix with the specified parameters.</summary>
		/// <param name="position">The position of the object.</param>
		/// <param name="forward">The forward direction of the object.</param>
		/// <param name="up">The upward direction of the object. Its value is usually <c>[0, 1, 0]</c>.</param>
		/// <returns>The world matrix.</returns>
		public static Matrix4x4 CreateWorld(Vector3 position, Vector3 forward, Vector3 up)
		{
			Vector3 vector = Vector3.Normalize(-forward);
			Vector3 vector2 = Vector3.Normalize(Vector3.Cross(up, vector));
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = vector2.X;
			result.M12 = vector2.Y;
			result.M13 = vector2.Z;
			result.M14 = 0f;
			result.M21 = vector3.X;
			result.M22 = vector3.Y;
			result.M23 = vector3.Z;
			result.M24 = 0f;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = 0f;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a rotation matrix from the specified Quaternion rotation value.</summary>
		/// <param name="quaternion">The source Quaternion.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateFromQuaternion(Quaternion quaternion)
		{
			float num = quaternion.X * quaternion.X;
			float num2 = quaternion.Y * quaternion.Y;
			float num3 = quaternion.Z * quaternion.Z;
			float num4 = quaternion.X * quaternion.Y;
			float num5 = quaternion.Z * quaternion.W;
			float num6 = quaternion.Z * quaternion.X;
			float num7 = quaternion.Y * quaternion.W;
			float num8 = quaternion.Y * quaternion.Z;
			float num9 = quaternion.X * quaternion.W;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 1f - 2f * (num2 + num3);
			result.M12 = 2f * (num4 + num5);
			result.M13 = 2f * (num6 - num7);
			result.M14 = 0f;
			result.M21 = 2f * (num4 - num5);
			result.M22 = 1f - 2f * (num3 + num);
			result.M23 = 2f * (num8 + num9);
			result.M24 = 0f;
			result.M31 = 2f * (num6 + num7);
			result.M32 = 2f * (num8 - num9);
			result.M33 = 1f - 2f * (num2 + num);
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a rotation matrix from the specified yaw, pitch, and roll.</summary>
		/// <param name="yaw">The angle of rotation, in radians, around the Y axis.</param>
		/// <param name="pitch">The angle of rotation, in radians, around the X axis.</param>
		/// <param name="roll">The angle of rotation, in radians, around the Z axis.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateFromYawPitchRoll(float yaw, float pitch, float roll)
		{
			return CreateFromQuaternion(Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll));
		}

		/// <summary>Creates a matrix that flattens geometry into a specified plane as if casting a shadow from a specified light source.</summary>
		/// <param name="lightDirection">The direction from which the light that will cast the shadow is coming.</param>
		/// <param name="plane">The plane onto which the new matrix should flatten geometry so as to cast a shadow.</param>
		/// <returns>A new matrix that can be used to flatten geometry onto the specified plane from the specified direction.</returns>
		public static Matrix4x4 CreateShadow(Vector3 lightDirection, Plane plane)
		{
			Plane plane2 = Plane.Normalize(plane);
			float num = plane2.Normal.X * lightDirection.X + plane2.Normal.Y * lightDirection.Y + plane2.Normal.Z * lightDirection.Z;
			float num2 = 0f - plane2.Normal.X;
			float num3 = 0f - plane2.Normal.Y;
			float num4 = 0f - plane2.Normal.Z;
			float num5 = 0f - plane2.D;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = num2 * lightDirection.X + num;
			result.M21 = num3 * lightDirection.X;
			result.M31 = num4 * lightDirection.X;
			result.M41 = num5 * lightDirection.X;
			result.M12 = num2 * lightDirection.Y;
			result.M22 = num3 * lightDirection.Y + num;
			result.M32 = num4 * lightDirection.Y;
			result.M42 = num5 * lightDirection.Y;
			result.M13 = num2 * lightDirection.Z;
			result.M23 = num3 * lightDirection.Z;
			result.M33 = num4 * lightDirection.Z + num;
			result.M43 = num5 * lightDirection.Z;
			result.M14 = 0f;
			result.M24 = 0f;
			result.M34 = 0f;
			result.M44 = num;
			return result;
		}

		/// <summary>Creates a matrix that reflects the coordinate system about a specified plane.</summary>
		/// <param name="value">The plane about which to create a reflection.</param>
		/// <returns>A new matrix expressing the reflection.</returns>
		public static Matrix4x4 CreateReflection(Plane value)
		{
			value = Plane.Normalize(value);
			float x = value.Normal.X;
			float y = value.Normal.Y;
			float z = value.Normal.Z;
			float num = -2f * x;
			float num2 = -2f * y;
			float num3 = -2f * z;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = num * x + 1f;
			result.M12 = num2 * x;
			result.M13 = num3 * x;
			result.M14 = 0f;
			result.M21 = num * y;
			result.M22 = num2 * y + 1f;
			result.M23 = num3 * y;
			result.M24 = 0f;
			result.M31 = num * z;
			result.M32 = num2 * z;
			result.M33 = num3 * z + 1f;
			result.M34 = 0f;
			result.M41 = num * value.D;
			result.M42 = num2 * value.D;
			result.M43 = num3 * value.D;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Calculates the determinant of the current 4x4 matrix.</summary>
		/// <returns>The determinant.</returns>
		public float GetDeterminant()
		{
			float m = M11;
			float m2 = M12;
			float m3 = M13;
			float m4 = M14;
			float m5 = M21;
			float m6 = M22;
			float m7 = M23;
			float m8 = M24;
			float m9 = M31;
			float m10 = M32;
			float m11 = M33;
			float m12 = M34;
			float m13 = M41;
			float m14 = M42;
			float m15 = M43;
			float m16 = M44;
			float num = m11 * m16 - m12 * m15;
			float num2 = m10 * m16 - m12 * m14;
			float num3 = m10 * m15 - m11 * m14;
			float num4 = m9 * m16 - m12 * m13;
			float num5 = m9 * m15 - m11 * m13;
			float num6 = m9 * m14 - m10 * m13;
			return m * (m6 * num - m7 * num2 + m8 * num3) - m2 * (m5 * num - m7 * num4 + m8 * num5) + m3 * (m5 * num2 - m6 * num4 + m8 * num6) - m4 * (m5 * num3 - m6 * num5 + m7 * num6);
		}

		/// <summary>Inverts the specified matrix. The return value indicates whether the operation succeeded.</summary>
		/// <param name="matrix">The matrix to invert.</param>
		/// <param name="result">When this method returns, contains the inverted matrix if the operation succeeded.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="matrix" /> was converted successfully; otherwise,  <see langword="false" />.</returns>
		public static bool Invert(Matrix4x4 matrix, out Matrix4x4 result)
		{
			float m = matrix.M11;
			float m2 = matrix.M12;
			float m3 = matrix.M13;
			float m4 = matrix.M14;
			float m5 = matrix.M21;
			float m6 = matrix.M22;
			float m7 = matrix.M23;
			float m8 = matrix.M24;
			float m9 = matrix.M31;
			float m10 = matrix.M32;
			float m11 = matrix.M33;
			float m12 = matrix.M34;
			float m13 = matrix.M41;
			float m14 = matrix.M42;
			float m15 = matrix.M43;
			float m16 = matrix.M44;
			float num = m11 * m16 - m12 * m15;
			float num2 = m10 * m16 - m12 * m14;
			float num3 = m10 * m15 - m11 * m14;
			float num4 = m9 * m16 - m12 * m13;
			float num5 = m9 * m15 - m11 * m13;
			float num6 = m9 * m14 - m10 * m13;
			float num7 = m6 * num - m7 * num2 + m8 * num3;
			float num8 = 0f - (m5 * num - m7 * num4 + m8 * num5);
			float num9 = m5 * num2 - m6 * num4 + m8 * num6;
			float num10 = 0f - (m5 * num3 - m6 * num5 + m7 * num6);
			float num11 = m * num7 + m2 * num8 + m3 * num9 + m4 * num10;
			if (MathF.Abs(num11) < float.Epsilon)
			{
				result = new Matrix4x4(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);
				return false;
			}
			float num12 = 1f / num11;
			result.M11 = num7 * num12;
			result.M21 = num8 * num12;
			result.M31 = num9 * num12;
			result.M41 = num10 * num12;
			result.M12 = (0f - (m2 * num - m3 * num2 + m4 * num3)) * num12;
			result.M22 = (m * num - m3 * num4 + m4 * num5) * num12;
			result.M32 = (0f - (m * num2 - m2 * num4 + m4 * num6)) * num12;
			result.M42 = (m * num3 - m2 * num5 + m3 * num6) * num12;
			float num13 = m7 * m16 - m8 * m15;
			float num14 = m6 * m16 - m8 * m14;
			float num15 = m6 * m15 - m7 * m14;
			float num16 = m5 * m16 - m8 * m13;
			float num17 = m5 * m15 - m7 * m13;
			float num18 = m5 * m14 - m6 * m13;
			result.M13 = (m2 * num13 - m3 * num14 + m4 * num15) * num12;
			result.M23 = (0f - (m * num13 - m3 * num16 + m4 * num17)) * num12;
			result.M33 = (m * num14 - m2 * num16 + m4 * num18) * num12;
			result.M43 = (0f - (m * num15 - m2 * num17 + m3 * num18)) * num12;
			float num19 = m7 * m12 - m8 * m11;
			float num20 = m6 * m12 - m8 * m10;
			float num21 = m6 * m11 - m7 * m10;
			float num22 = m5 * m12 - m8 * m9;
			float num23 = m5 * m11 - m7 * m9;
			float num24 = m5 * m10 - m6 * m9;
			result.M14 = (0f - (m2 * num19 - m3 * num20 + m4 * num21)) * num12;
			result.M24 = (m * num19 - m3 * num22 + m4 * num23) * num12;
			result.M34 = (0f - (m * num20 - m2 * num22 + m4 * num24)) * num12;
			result.M44 = (m * num21 - m2 * num23 + m3 * num24) * num12;
			return true;
		}

		/// <summary>Attempts to extract the scale, translation, and rotation components from the given scale, rotation, or translation matrix. The return value indicates whether the operation succeeded.</summary>
		/// <param name="matrix">The source matrix.</param>
		/// <param name="scale">When this method returns, contains the scaling component of the transformation matrix if the operation succeeded.</param>
		/// <param name="rotation">When this method returns, contains the rotation component of the transformation matrix if the operation succeeded.</param>
		/// <param name="translation">When the method returns, contains the translation component of the transformation matrix if the operation succeeded.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="matrix" /> was decomposed successfully; otherwise,  <see langword="false" />.</returns>
		public unsafe static bool Decompose(Matrix4x4 matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation)
		{
			bool result = true;
			fixed (Vector3* ptr = &scale)
			{
				float* ptr2 = (float*)ptr;
				VectorBasis vectorBasis = default(VectorBasis);
				Vector3** ptr3 = (Vector3**)(&vectorBasis);
				Matrix4x4 identity = Identity;
				CanonicalBasis canonicalBasis = default(CanonicalBasis);
				Vector3* ptr4 = &canonicalBasis.Row0;
				canonicalBasis.Row0 = new Vector3(1f, 0f, 0f);
				canonicalBasis.Row1 = new Vector3(0f, 1f, 0f);
				canonicalBasis.Row2 = new Vector3(0f, 0f, 1f);
				translation = new Vector3(matrix.M41, matrix.M42, matrix.M43);
				*ptr3 = (Vector3*)(&identity.M11);
				ptr3[1] = (Vector3*)(&identity.M21);
				ptr3[2] = (Vector3*)(&identity.M31);
				*(*ptr3) = new Vector3(matrix.M11, matrix.M12, matrix.M13);
				*ptr3[1] = new Vector3(matrix.M21, matrix.M22, matrix.M23);
				*ptr3[2] = new Vector3(matrix.M31, matrix.M32, matrix.M33);
				scale.X = (*ptr3)->Length();
				scale.Y = ptr3[1]->Length();
				scale.Z = ptr3[2]->Length();
				float num = *ptr2;
				float num2 = ptr2[1];
				float num3 = ptr2[2];
				uint num4;
				uint num5;
				uint num6;
				if (num < num2)
				{
					if (num2 < num3)
					{
						num4 = 2u;
						num5 = 1u;
						num6 = 0u;
					}
					else
					{
						num4 = 1u;
						if (num < num3)
						{
							num5 = 2u;
							num6 = 0u;
						}
						else
						{
							num5 = 0u;
							num6 = 2u;
						}
					}
				}
				else if (num < num3)
				{
					num4 = 2u;
					num5 = 0u;
					num6 = 1u;
				}
				else
				{
					num4 = 0u;
					if (num2 < num3)
					{
						num5 = 2u;
						num6 = 1u;
					}
					else
					{
						num5 = 1u;
						num6 = 2u;
					}
				}
				if (ptr2[num4] < 0.0001f)
				{
					*ptr3[num4] = ptr4[num4];
				}
				*ptr3[num4] = Vector3.Normalize(*ptr3[num4]);
				if (ptr2[num5] < 0.0001f)
				{
					float num7 = MathF.Abs(ptr3[num4]->X);
					float num8 = MathF.Abs(ptr3[num4]->Y);
					float num9 = MathF.Abs(ptr3[num4]->Z);
					uint num10 = ((num7 < num8) ? ((!(num8 < num9)) ? ((!(num7 < num9)) ? 2u : 0u) : 0u) : ((num7 < num9) ? 1u : ((num8 < num9) ? 1u : 2u)));
					*ptr3[num5] = Vector3.Cross(*ptr3[num4], ptr4[num10]);
				}
				*ptr3[num5] = Vector3.Normalize(*ptr3[num5]);
				if (ptr2[num6] < 0.0001f)
				{
					*ptr3[num6] = Vector3.Cross(*ptr3[num4], *ptr3[num5]);
				}
				*ptr3[num6] = Vector3.Normalize(*ptr3[num6]);
				float num11 = identity.GetDeterminant();
				if (num11 < 0f)
				{
					ptr2[num4] = 0f - ptr2[num4];
					*ptr3[num4] = -(*ptr3[num4]);
					num11 = 0f - num11;
				}
				num11 -= 1f;
				num11 *= num11;
				if (0.0001f < num11)
				{
					rotation = Quaternion.Identity;
					result = false;
				}
				else
				{
					rotation = Quaternion.CreateFromRotationMatrix(identity);
				}
			}
			return result;
		}

		/// <summary>Transforms the specified matrix by applying the specified Quaternion rotation.</summary>
		/// <param name="value">The matrix to transform.</param>
		/// <param name="rotation">The rotation t apply.</param>
		/// <returns>The transformed matrix.</returns>
		public static Matrix4x4 Transform(Matrix4x4 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			float num13 = 1f - num10 - num12;
			float num14 = num8 - num6;
			float num15 = num9 + num5;
			float num16 = num8 + num6;
			float num17 = 1f - num7 - num12;
			float num18 = num11 - num4;
			float num19 = num9 - num5;
			float num20 = num11 + num4;
			float num21 = 1f - num7 - num10;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value.M11 * num13 + value.M12 * num14 + value.M13 * num15;
			result.M12 = value.M11 * num16 + value.M12 * num17 + value.M13 * num18;
			result.M13 = value.M11 * num19 + value.M12 * num20 + value.M13 * num21;
			result.M14 = value.M14;
			result.M21 = value.M21 * num13 + value.M22 * num14 + value.M23 * num15;
			result.M22 = value.M21 * num16 + value.M22 * num17 + value.M23 * num18;
			result.M23 = value.M21 * num19 + value.M22 * num20 + value.M23 * num21;
			result.M24 = value.M24;
			result.M31 = value.M31 * num13 + value.M32 * num14 + value.M33 * num15;
			result.M32 = value.M31 * num16 + value.M32 * num17 + value.M33 * num18;
			result.M33 = value.M31 * num19 + value.M32 * num20 + value.M33 * num21;
			result.M34 = value.M34;
			result.M41 = value.M41 * num13 + value.M42 * num14 + value.M43 * num15;
			result.M42 = value.M41 * num16 + value.M42 * num17 + value.M43 * num18;
			result.M43 = value.M41 * num19 + value.M42 * num20 + value.M43 * num21;
			result.M44 = value.M44;
			return result;
		}

		/// <summary>Transposes the rows and columns of a matrix.</summary>
		/// <param name="matrix">The matrix to transpose.</param>
		/// <returns>The transposed matrix.</returns>
		public static Matrix4x4 Transpose(Matrix4x4 matrix)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = matrix.M11;
			result.M12 = matrix.M21;
			result.M13 = matrix.M31;
			result.M14 = matrix.M41;
			result.M21 = matrix.M12;
			result.M22 = matrix.M22;
			result.M23 = matrix.M32;
			result.M24 = matrix.M42;
			result.M31 = matrix.M13;
			result.M32 = matrix.M23;
			result.M33 = matrix.M33;
			result.M34 = matrix.M43;
			result.M41 = matrix.M14;
			result.M42 = matrix.M24;
			result.M43 = matrix.M34;
			result.M44 = matrix.M44;
			return result;
		}

		/// <summary>Performs a linear interpolation from one matrix to a second matrix based on a value that specifies the weighting of the second matrix.</summary>
		/// <param name="matrix1">The first matrix.</param>
		/// <param name="matrix2">The second matrix.</param>
		/// <param name="amount">The relative weighting of <paramref name="matrix2" />.</param>
		/// <returns>The interpolated matrix.</returns>
		public static Matrix4x4 Lerp(Matrix4x4 matrix1, Matrix4x4 matrix2, float amount)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
			result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
			result.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13) * amount;
			result.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14) * amount;
			result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
			result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
			result.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23) * amount;
			result.M24 = matrix1.M24 + (matrix2.M24 - matrix1.M24) * amount;
			result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
			result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
			result.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33) * amount;
			result.M34 = matrix1.M34 + (matrix2.M34 - matrix1.M34) * amount;
			result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41) * amount;
			result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42) * amount;
			result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43) * amount;
			result.M44 = matrix1.M44 + (matrix2.M44 - matrix1.M44) * amount;
			return result;
		}

		/// <summary>Negates the specified matrix by multiplying all its values by -1.</summary>
		/// <param name="value">The matrix to negate.</param>
		/// <returns>The negated matrix.</returns>
		public static Matrix4x4 Negate(Matrix4x4 value)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 0f - value.M11;
			result.M12 = 0f - value.M12;
			result.M13 = 0f - value.M13;
			result.M14 = 0f - value.M14;
			result.M21 = 0f - value.M21;
			result.M22 = 0f - value.M22;
			result.M23 = 0f - value.M23;
			result.M24 = 0f - value.M24;
			result.M31 = 0f - value.M31;
			result.M32 = 0f - value.M32;
			result.M33 = 0f - value.M33;
			result.M34 = 0f - value.M34;
			result.M41 = 0f - value.M41;
			result.M42 = 0f - value.M42;
			result.M43 = 0f - value.M43;
			result.M44 = 0f - value.M44;
			return result;
		}

		/// <summary>Adds each element in one matrix with its corresponding element in a second matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix that contains the summed values of <paramref name="value1" /> and <paramref name="value2" />.</returns>
		public static Matrix4x4 Add(Matrix4x4 value1, Matrix4x4 value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 + value2.M11;
			result.M12 = value1.M12 + value2.M12;
			result.M13 = value1.M13 + value2.M13;
			result.M14 = value1.M14 + value2.M14;
			result.M21 = value1.M21 + value2.M21;
			result.M22 = value1.M22 + value2.M22;
			result.M23 = value1.M23 + value2.M23;
			result.M24 = value1.M24 + value2.M24;
			result.M31 = value1.M31 + value2.M31;
			result.M32 = value1.M32 + value2.M32;
			result.M33 = value1.M33 + value2.M33;
			result.M34 = value1.M34 + value2.M34;
			result.M41 = value1.M41 + value2.M41;
			result.M42 = value1.M42 + value2.M42;
			result.M43 = value1.M43 + value2.M43;
			result.M44 = value1.M44 + value2.M44;
			return result;
		}

		/// <summary>Subtracts each element in a second matrix from its corresponding element in a first matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix containing the values that result from subtracting each element in <paramref name="value2" /> from its corresponding element in <paramref name="value1" />.</returns>
		public static Matrix4x4 Subtract(Matrix4x4 value1, Matrix4x4 value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 - value2.M11;
			result.M12 = value1.M12 - value2.M12;
			result.M13 = value1.M13 - value2.M13;
			result.M14 = value1.M14 - value2.M14;
			result.M21 = value1.M21 - value2.M21;
			result.M22 = value1.M22 - value2.M22;
			result.M23 = value1.M23 - value2.M23;
			result.M24 = value1.M24 - value2.M24;
			result.M31 = value1.M31 - value2.M31;
			result.M32 = value1.M32 - value2.M32;
			result.M33 = value1.M33 - value2.M33;
			result.M34 = value1.M34 - value2.M34;
			result.M41 = value1.M41 - value2.M41;
			result.M42 = value1.M42 - value2.M42;
			result.M43 = value1.M43 - value2.M43;
			result.M44 = value1.M44 - value2.M44;
			return result;
		}

		/// <summary>Returns the matrix that results from multiplying two matrices together.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The product matrix.</returns>
		public static Matrix4x4 Multiply(Matrix4x4 value1, Matrix4x4 value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41;
			result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42;
			result.M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43;
			result.M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44;
			result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41;
			result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42;
			result.M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43;
			result.M24 = value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44;
			result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41;
			result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42;
			result.M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43;
			result.M34 = value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44;
			result.M41 = value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41;
			result.M42 = value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42;
			result.M43 = value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43;
			result.M44 = value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44;
			return result;
		}

		/// <summary>Returns the matrix that results from scaling all the elements of a specified matrix by a scalar factor.</summary>
		/// <param name="value1">The matrix to scale.</param>
		/// <param name="value2">The scaling value to use.</param>
		/// <returns>The scaled matrix.</returns>
		public static Matrix4x4 Multiply(Matrix4x4 value1, float value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 * value2;
			result.M12 = value1.M12 * value2;
			result.M13 = value1.M13 * value2;
			result.M14 = value1.M14 * value2;
			result.M21 = value1.M21 * value2;
			result.M22 = value1.M22 * value2;
			result.M23 = value1.M23 * value2;
			result.M24 = value1.M24 * value2;
			result.M31 = value1.M31 * value2;
			result.M32 = value1.M32 * value2;
			result.M33 = value1.M33 * value2;
			result.M34 = value1.M34 * value2;
			result.M41 = value1.M41 * value2;
			result.M42 = value1.M42 * value2;
			result.M43 = value1.M43 * value2;
			result.M44 = value1.M44 * value2;
			return result;
		}

		/// <summary>Negates the specified matrix by multiplying all its values by -1.</summary>
		/// <param name="value">The matrix to negate.</param>
		/// <returns>The negated matrix.</returns>
		public static Matrix4x4 operator -(Matrix4x4 value)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 0f - value.M11;
			result.M12 = 0f - value.M12;
			result.M13 = 0f - value.M13;
			result.M14 = 0f - value.M14;
			result.M21 = 0f - value.M21;
			result.M22 = 0f - value.M22;
			result.M23 = 0f - value.M23;
			result.M24 = 0f - value.M24;
			result.M31 = 0f - value.M31;
			result.M32 = 0f - value.M32;
			result.M33 = 0f - value.M33;
			result.M34 = 0f - value.M34;
			result.M41 = 0f - value.M41;
			result.M42 = 0f - value.M42;
			result.M43 = 0f - value.M43;
			result.M44 = 0f - value.M44;
			return result;
		}

		/// <summary>Adds each element in one matrix with its corresponding element in a second matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix that contains the summed values.</returns>
		public static Matrix4x4 operator +(Matrix4x4 value1, Matrix4x4 value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 + value2.M11;
			result.M12 = value1.M12 + value2.M12;
			result.M13 = value1.M13 + value2.M13;
			result.M14 = value1.M14 + value2.M14;
			result.M21 = value1.M21 + value2.M21;
			result.M22 = value1.M22 + value2.M22;
			result.M23 = value1.M23 + value2.M23;
			result.M24 = value1.M24 + value2.M24;
			result.M31 = value1.M31 + value2.M31;
			result.M32 = value1.M32 + value2.M32;
			result.M33 = value1.M33 + value2.M33;
			result.M34 = value1.M34 + value2.M34;
			result.M41 = value1.M41 + value2.M41;
			result.M42 = value1.M42 + value2.M42;
			result.M43 = value1.M43 + value2.M43;
			result.M44 = value1.M44 + value2.M44;
			return result;
		}

		/// <summary>Subtracts each element in a second matrix from its corresponding element in a first matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix containing the values that result from subtracting each element in <paramref name="value2" /> from its corresponding element in <paramref name="value1" />.</returns>
		public static Matrix4x4 operator -(Matrix4x4 value1, Matrix4x4 value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 - value2.M11;
			result.M12 = value1.M12 - value2.M12;
			result.M13 = value1.M13 - value2.M13;
			result.M14 = value1.M14 - value2.M14;
			result.M21 = value1.M21 - value2.M21;
			result.M22 = value1.M22 - value2.M22;
			result.M23 = value1.M23 - value2.M23;
			result.M24 = value1.M24 - value2.M24;
			result.M31 = value1.M31 - value2.M31;
			result.M32 = value1.M32 - value2.M32;
			result.M33 = value1.M33 - value2.M33;
			result.M34 = value1.M34 - value2.M34;
			result.M41 = value1.M41 - value2.M41;
			result.M42 = value1.M42 - value2.M42;
			result.M43 = value1.M43 - value2.M43;
			result.M44 = value1.M44 - value2.M44;
			return result;
		}

		/// <summary>Returns the matrix that results from multiplying two matrices together.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The product matrix.</returns>
		public static Matrix4x4 operator *(Matrix4x4 value1, Matrix4x4 value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41;
			result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42;
			result.M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43;
			result.M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44;
			result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41;
			result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42;
			result.M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43;
			result.M24 = value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44;
			result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41;
			result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42;
			result.M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43;
			result.M34 = value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44;
			result.M41 = value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41;
			result.M42 = value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42;
			result.M43 = value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43;
			result.M44 = value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44;
			return result;
		}

		/// <summary>Returns the matrix that results from scaling all the elements of a specified matrix by a scalar factor.</summary>
		/// <param name="value1">The matrix to scale.</param>
		/// <param name="value2">The scaling value to use.</param>
		/// <returns>The scaled matrix.</returns>
		public static Matrix4x4 operator *(Matrix4x4 value1, float value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 * value2;
			result.M12 = value1.M12 * value2;
			result.M13 = value1.M13 * value2;
			result.M14 = value1.M14 * value2;
			result.M21 = value1.M21 * value2;
			result.M22 = value1.M22 * value2;
			result.M23 = value1.M23 * value2;
			result.M24 = value1.M24 * value2;
			result.M31 = value1.M31 * value2;
			result.M32 = value1.M32 * value2;
			result.M33 = value1.M33 * value2;
			result.M34 = value1.M34 * value2;
			result.M41 = value1.M41 * value2;
			result.M42 = value1.M42 * value2;
			result.M43 = value1.M43 * value2;
			result.M44 = value1.M44 * value2;
			return result;
		}

		/// <summary>Returns a value that indicates whether the specified matrices are equal.</summary>
		/// <param name="value1">The first matrix to compare.</param>
		/// <param name="value2">The second matrix to care</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> and <paramref name="value2" /> are equal; otherwise, <see langword="false" />.</returns>
		public static bool operator ==(Matrix4x4 value1, Matrix4x4 value2)
		{
			if (value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M33 == value2.M33 && value1.M44 == value2.M44 && value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 && value1.M21 == value2.M21 && value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 && value1.M32 == value2.M32 && value1.M34 == value2.M34 && value1.M41 == value2.M41 && value1.M42 == value2.M42)
			{
				return value1.M43 == value2.M43;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether the specified matrices are not equal.</summary>
		/// <param name="value1">The first matrix to compare.</param>
		/// <param name="value2">The second matrix to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> and <paramref name="value2" /> are not equal; otherwise, <see langword="false" />.</returns>
		public static bool operator !=(Matrix4x4 value1, Matrix4x4 value2)
		{
			if (value1.M11 == value2.M11 && value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 && value1.M21 == value2.M21 && value1.M22 == value2.M22 && value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 && value1.M32 == value2.M32 && value1.M33 == value2.M33 && value1.M34 == value2.M34 && value1.M41 == value2.M41 && value1.M42 == value2.M42 && value1.M43 == value2.M43)
			{
				return value1.M44 != value2.M44;
			}
			return true;
		}

		/// <summary>Returns a value that indicates whether this instance and another 4x4 matrix are equal.</summary>
		/// <param name="other">The other matrix.</param>
		/// <returns>
		///   <see langword="true" /> if the two matrices are equal; otherwise, <see langword="false" />.</returns>
		public bool Equals(Matrix4x4 other)
		{
			if (M11 == other.M11 && M22 == other.M22 && M33 == other.M33 && M44 == other.M44 && M12 == other.M12 && M13 == other.M13 && M14 == other.M14 && M21 == other.M21 && M23 == other.M23 && M24 == other.M24 && M31 == other.M31 && M32 == other.M32 && M34 == other.M34 && M41 == other.M41 && M42 == other.M42)
			{
				return M43 == other.M43;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance and <paramref name="obj" /> are equal; otherwise, <see langword="false" />. If <paramref name="obj" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix4x4)
			{
				return Equals((Matrix4x4)obj);
			}
			return false;
		}

		/// <summary>Returns a string that represents this matrix.</summary>
		/// <returns>The string representation of this matrix.</returns>
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{ {{M11:{0} M12:{1} M13:{2} M14:{3}}} {{M21:{4} M22:{5} M23:{6} M24:{7}}} {{M31:{8} M32:{9} M33:{10} M34:{11}}} {{M41:{12} M42:{13} M43:{14} M44:{15}}} }}", M11.ToString(currentCulture), M12.ToString(currentCulture), M13.ToString(currentCulture), M14.ToString(currentCulture), M21.ToString(currentCulture), M22.ToString(currentCulture), M23.ToString(currentCulture), M24.ToString(currentCulture), M31.ToString(currentCulture), M32.ToString(currentCulture), M33.ToString(currentCulture), M34.ToString(currentCulture), M41.ToString(currentCulture), M42.ToString(currentCulture), M43.ToString(currentCulture), M44.ToString(currentCulture));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			return M11.GetHashCode() + M12.GetHashCode() + M13.GetHashCode() + M14.GetHashCode() + M21.GetHashCode() + M22.GetHashCode() + M23.GetHashCode() + M24.GetHashCode() + M31.GetHashCode() + M32.GetHashCode() + M33.GetHashCode() + M34.GetHashCode() + M41.GetHashCode() + M42.GetHashCode() + M43.GetHashCode() + M44.GetHashCode();
		}
	}
	/// <summary>Represents a plane in three-dimensional space.</summary>
	public struct Plane : IEquatable<Plane>
	{
		/// <summary>The normal vector of the plane.</summary>
		public Vector3 Normal;

		/// <summary>The distance of the plane along its normal from the origin.</summary>
		public float D;

		/// <summary>Creates a <see cref="T:System.Numerics.Plane" /> object from the X, Y, and Z components of its normal, and its distance from the origin on that normal.</summary>
		/// <param name="x">The X component of the normal.</param>
		/// <param name="y">The Y component of the normal.</param>
		/// <param name="z">The Z component of the normal.</param>
		/// <param name="d">The distance of the plane along its normal from the origin.</param>
		public Plane(float x, float y, float z, float d)
		{
			Normal = new Vector3(x, y, z);
			D = d;
		}

		/// <summary>Creates a <see cref="T:System.Numerics.Plane" /> object from a specified normal and the distance along the normal from the origin.</summary>
		/// <param name="normal">The plane's normal vector.</param>
		/// <param name="d">The plane's distance from the origin along its normal vector.</param>
		public Plane(Vector3 normal, float d)
		{
			Normal = normal;
			D = d;
		}

		/// <summary>Creates a <see cref="T:System.Numerics.Plane" /> object from a specified four-dimensional vector.</summary>
		/// <param name="value">A vector whose first three elements describe the normal vector, and whose <see cref="F:System.Numerics.Vector4.W" /> defines the distance along that normal from the origin.</param>
		public Plane(Vector4 value)
		{
			Normal = new Vector3(value.X, value.Y, value.Z);
			D = value.W;
		}

		/// <summary>Creates a <see cref="T:System.Numerics.Plane" /> object that contains three specified points.</summary>
		/// <param name="point1">The first point defining the plane.</param>
		/// <param name="point2">The second point defining the plane.</param>
		/// <param name="point3">The third point defining the plane.</param>
		/// <returns>The plane containing the three points.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane CreateFromVertices(Vector3 point1, Vector3 point2, Vector3 point3)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 vector = point2 - point1;
				Vector3 vector2 = point3 - point1;
				Vector3 vector3 = Vector3.Normalize(Vector3.Cross(vector, vector2));
				float d = 0f - Vector3.Dot(vector3, point1);
				return new Plane(vector3, d);
			}
			float num = point2.X - point1.X;
			float num2 = point2.Y - point1.Y;
			float num3 = point2.Z - point1.Z;
			float num4 = point3.X - point1.X;
			float num5 = point3.Y - point1.Y;
			float num6 = point3.Z - point1.Z;
			float num7 = num2 * num6 - num3 * num5;
			float num8 = num3 * num4 - num * num6;
			float num9 = num * num5 - num2 * num4;
			float x = num7 * num7 + num8 * num8 + num9 * num9;
			float num10 = 1f / MathF.Sqrt(x);
			Vector3 normal = new Vector3(num7 * num10, num8 * num10, num9 * num10);
			return new Plane(normal, 0f - (normal.X * point1.X + normal.Y * point1.Y + normal.Z * point1.Z));
		}

		/// <summary>Creates a new <see cref="T:System.Numerics.Plane" /> object whose normal vector is the source plane's normal vector normalized.</summary>
		/// <param name="value">The source plane.</param>
		/// <returns>The normalized plane.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Normalize(Plane value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float num = value.Normal.LengthSquared();
				if (MathF.Abs(num - 1f) < 1.1920929E-07f)
				{
					return value;
				}
				float num2 = MathF.Sqrt(num);
				return new Plane(value.Normal / num2, value.D / num2);
			}
			float num3 = value.Normal.X * value.Normal.X + value.Normal.Y * value.Normal.Y + value.Normal.Z * value.Normal.Z;
			if (MathF.Abs(num3 - 1f) < 1.1920929E-07f)
			{
				return value;
			}
			float num4 = 1f / MathF.Sqrt(num3);
			return new Plane(value.Normal.X * num4, value.Normal.Y * num4, value.Normal.Z * num4, value.D * num4);
		}

		/// <summary>Transforms a normalized plane by a 4x4 matrix.</summary>
		/// <param name="plane">The normalized plane to transform.</param>
		/// <param name="matrix">The transformation matrix to apply to <paramref name="plane" />.</param>
		/// <returns>The transformed plane.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Transform(Plane plane, Matrix4x4 matrix)
		{
			Matrix4x4.Invert(matrix, out var result);
			float x = plane.Normal.X;
			float y = plane.Normal.Y;
			float z = plane.Normal.Z;
			float d = plane.D;
			return new Plane(x * result.M11 + y * result.M12 + z * result.M13 + d * result.M14, x * result.M21 + y * result.M22 + z * result.M23 + d * result.M24, x * result.M31 + y * result.M32 + z * result.M33 + d * result.M34, x * result.M41 + y * result.M42 + z * result.M43 + d * result.M44);
		}

		/// <summary>Transforms a normalized plane by a Quaternion rotation.</summary>
		/// <param name="plane">The normalized plane to transform.</param>
		/// <param name="rotation">The Quaternion rotation to apply to the plane.</param>
		/// <returns>A new plane that results from applying the Quaternion rotation.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Transform(Plane plane, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			float num13 = 1f - num10 - num12;
			float num14 = num8 - num6;
			float num15 = num9 + num5;
			float num16 = num8 + num6;
			float num17 = 1f - num7 - num12;
			float num18 = num11 - num4;
			float num19 = num9 - num5;
			float num20 = num11 + num4;
			float num21 = 1f - num7 - num10;
			float x = plane.Normal.X;
			float y = plane.Normal.Y;
			float z = plane.Normal.Z;
			return new Plane(x * num13 + y * num14 + z * num15, x * num16 + y * num17 + z * num18, x * num19 + y * num20 + z * num21, plane.D);
		}

		/// <summary>Calculates the dot product of a plane and a 4-dimensional vector.</summary>
		/// <param name="plane">The plane.</param>
		/// <param name="value">The four-dimensional vector.</param>
		/// <returns>The dot product.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Plane plane, Vector4 value)
		{
			return plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z + plane.D * value.W;
		}

		/// <summary>Returns the dot product of a specified three-dimensional vector and the normal vector of this plane plus the distance (<see cref="F:System.Numerics.Plane.D" />) value of the plane.</summary>
		/// <param name="plane">The plane.</param>
		/// <param name="value">The 3-dimensional vector.</param>
		/// <returns>The dot product.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DotCoordinate(Plane plane, Vector3 value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Vector3.Dot(plane.Normal, value) + plane.D;
			}
			return plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z + plane.D;
		}

		/// <summary>Returns the dot product of a specified three-dimensional vector and the <see cref="F:System.Numerics.Plane.Normal" /> vector of this plane.</summary>
		/// <param name="plane">The plane.</param>
		/// <param name="value">The three-dimensional vector.</param>
		/// <returns>The dot product.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DotNormal(Plane plane, Vector3 value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Vector3.Dot(plane.Normal, value);
			}
			return plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z;
		}

		/// <summary>Returns a value that indicates whether two planes are equal.</summary>
		/// <param name="value1">The first plane to compare.</param>
		/// <param name="value2">The second plane to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> and <paramref name="value2" /> are equal; otherwise, <see langword="false" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Plane value1, Plane value2)
		{
			if (value1.Normal.X == value2.Normal.X && value1.Normal.Y == value2.Normal.Y && value1.Normal.Z == value2.Normal.Z)
			{
				return value1.D == value2.D;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether two planes are not equal.</summary>
		/// <param name="value1">The first plane to compare.</param>
		/// <param name="value2">The second plane to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> and <paramref name="value2" /> are not equal; otherwise, <see langword="false" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Plane value1, Plane value2)
		{
			if (value1.Normal.X == value2.Normal.X && value1.Normal.Y == value2.Normal.Y && value1.Normal.Z == value2.Normal.Z)
			{
				return value1.D != value2.D;
			}
			return true;
		}

		/// <summary>Returns a value that indicates whether this instance and another plane object are equal.</summary>
		/// <param name="other">The other plane.</param>
		/// <returns>
		///   <see langword="true" /> if the two planes are equal; otherwise, <see langword="false" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Plane other)
		{
			if (Vector.IsHardwareAccelerated)
			{
				if (Normal.Equals(other.Normal))
				{
					return D == other.D;
				}
				return false;
			}
			if (Normal.X == other.Normal.X && Normal.Y == other.Normal.Y && Normal.Z == other.Normal.Z)
			{
				return D == other.D;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance and <paramref name="obj" /> are equal; otherwise, <see langword="false" />. If <paramref name="obj" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			if (obj is Plane)
			{
				return Equals((Plane)obj);
			}
			return false;
		}

		/// <summary>Returns the string representation of this plane object.</summary>
		/// <returns>A string that represents this <see cref="T:System.Numerics.Plane" /> object.</returns>
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{Normal:{0} D:{1}}}", Normal.ToString(), D.ToString(currentCulture));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			return Normal.GetHashCode() + D.GetHashCode();
		}
	}
	/// <summary>Represents a vector that is used to encode three-dimensional physical rotations.</summary>
	public struct Quaternion : IEquatable<Quaternion>
	{
		/// <summary>The X value of the vector component of the quaternion.</summary>
		public float X;

		/// <summary>The Y value of the vector component of the quaternion.</summary>
		public float Y;

		/// <summary>The Z value of the vector component of the quaternion.</summary>
		public float Z;

		/// <summary>The rotation component of the quaternion.</summary>
		public float W;

		/// <summary>Gets a quaternion that represents no rotation.</summary>
		/// <returns>A quaternion whose values are <c>(0, 0, 0, 1)</c>.</returns>
		public static Quaternion Identity => new Quaternion(0f, 0f, 0f, 1f);

		/// <summary>Gets a value that indicates whether the current instance is the identity quaternion.</summary>
		/// <returns>
		///   <see langword="true" /> if the current instance is the identity quaternion; otherwise, <see langword="false" />.</returns>
		public bool IsIdentity
		{
			get
			{
				if (X == 0f && Y == 0f && Z == 0f)
				{
					return W == 1f;
				}
				return false;
			}
		}

		/// <summary>Constructs a quaternion from the specified components.</summary>
		/// <param name="x">The value to assign to the X component of the quaternion.</param>
		/// <param name="y">The value to assign to the Y component of the quaternion.</param>
		/// <param name="z">The value to assign to the Z component of the quaternion.</param>
		/// <param name="w">The value to assign to the W component of the quaternion.</param>
		public Quaternion(float x, float y, float z, float w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}

		/// <summary>Creates a quaternion from the specified vector and rotation parts.</summary>
		/// <param name="vectorPart">The vector part of the quaternion.</param>
		/// <param name="scalarPart">The rotation part of the quaternion.</param>
		public Quaternion(Vector3 vectorPart, float scalarPart)
		{
			X = vectorPart.X;
			Y = vectorPart.Y;
			Z = vectorPart.Z;
			W = scalarPart;
		}

		/// <summary>Calculates the length of the quaternion.</summary>
		/// <returns>The computed length of the quaternion.</returns>
		public float Length()
		{
			return MathF.Sqrt(X * X + Y * Y + Z * Z + W * W);
		}

		/// <summary>Calculates the squared length of the quaternion.</summary>
		/// <returns>The length squared of the quaternion.</returns>
		public float LengthSquared()
		{
			return X * X + Y * Y + Z * Z + W * W;
		}

		/// <summary>Divides each component of a specified <see cref="T:System.Numerics.Quaternion" /> by its length.</summary>
		/// <param name="value">The quaternion to normalize.</param>
		/// <returns>The normalized quaternion.</returns>
		public static Quaternion Normalize(Quaternion value)
		{
			float x = value.X * value.X + value.Y * value.Y + value.Z * value.Z + value.W * value.W;
			float num = 1f / MathF.Sqrt(x);
			Quaternion result = default(Quaternion);
			result.X = value.X * num;
			result.Y = value.Y * num;
			result.Z = value.Z * num;
			result.W = value.W * num;
			return result;
		}

		/// <summary>Returns the conjugate of a specified quaternion.</summary>
		/// <param name="value">The quaternion.</param>
		/// <returns>A new quaternion that is the conjugate of <see langword="value" />.</returns>
		public static Quaternion Conjugate(Quaternion value)
		{
			Quaternion result = default(Quaternion);
			result.X = 0f - value.X;
			result.Y = 0f - value.Y;
			result.Z = 0f - value.Z;
			result.W = value.W;
			return result;
		}

		/// <summary>Returns the inverse of a quaternion.</summary>
		/// <param name="value">The quaternion.</param>
		/// <returns>The inverted quaternion.</returns>
		public static Quaternion Inverse(Quaternion value)
		{
			float num = value.X * value.X + value.Y * value.Y + value.Z * value.Z + value.W * value.W;
			float num2 = 1f / num;
			Quaternion result = default(Quaternion);
			result.X = (0f - value.X) * num2;
			result.Y = (0f - value.Y) * num2;
			result.Z = (0f - value.Z) * num2;
			result.W = value.W * num2;
			return result;
		}

		/// <summary>Creates a quaternion from a unit vector and an angle to rotate around the vector.</summary>
		/// <param name="axis">The unit vector to rotate around.</param>
		/// <param name="angle">The angle, in radians, to rotate around the vector.</param>
		/// <returns>The newly created quaternion.</returns>
		public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
		{
			float x = angle * 0.5f;
			float num = MathF.Sin(x);
			float w = MathF.Cos(x);
			Quaternion result = default(Quaternion);
			result.X = axis.X * num;
			result.Y = axis.Y * num;
			result.Z = axis.Z * num;
			result.W = w;
			return result;
		}

		/// <summary>Creates a new quaternion from the given yaw, pitch, and roll.</summary>
		/// <param name="yaw">The yaw angle, in radians, around the Y axis.</param>
		/// <param name="pitch">The pitch angle, in radians, around the X axis.</param>
		/// <param name="roll">The roll angle, in radians, around the Z axis.</param>
		/// <returns>The resulting quaternion.</returns>
		public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
		{
			float x = roll * 0.5f;
			float num = MathF.Sin(x);
			float num2 = MathF.Cos(x);
			float x2 = pitch * 0.5f;
			float num3 = MathF.Sin(x2);
			float num4 = MathF.Cos(x2);
			float x3 = yaw * 0.5f;
			float num5 = MathF.Sin(x3);
			float num6 = MathF.Cos(x3);
			Quaternion result = default(Quaternion);
			result.X = num6 * num3 * num2 + num5 * num4 * num;
			result.Y = num5 * num4 * num2 - num6 * num3 * num;
			result.Z = num6 * num4 * num - num5 * num3 * num2;
			result.W = num6 * num4 * num2 + num5 * num3 * num;
			return result;
		}

		/// <summary>Creates a quaternion from the specified rotation matrix.</summary>
		/// <param name="matrix">The rotation matrix.</param>
		/// <returns>The newly created quaternion.</returns>
		public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
		{
			float num = matrix.M11 + matrix.M22 + matrix.M33;
			Quaternion result = default(Quaternion);
			if (num > 0f)
			{
				float num2 = MathF.Sqrt(num + 1f);
				result.W = num2 * 0.5f;
				num2 = 0.5f / num2;
				result.X = (matrix.M23 - matrix.M32) * num2;
				result.Y = (matrix.M31 - matrix.M13) * num2;
				result.Z = (matrix.M12 - matrix.M21) * num2;
			}
			else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
			{
				float num3 = MathF.Sqrt(1f + matrix.M11 - matrix.M22 - matrix.M33);
				float num4 = 0.5f / num3;
				result.X = 0.5f * num3;
				result.Y = (matrix.M12 + matrix.M21) * num4;
				result.Z = (matrix.M13 + matrix.M31) * num4;
				result.W = (matrix.M23 - matrix.M32) * num4;
			}
			else if (matrix.M22 > matrix.M33)
			{
				float num5 = MathF.Sqrt(1f + matrix.M22 - matrix.M11 - matrix.M33);
				float num6 = 0.5f / num5;
				result.X = (matrix.M21 + matrix.M12) * num6;
				result.Y = 0.5f * num5;
				result.Z = (matrix.M32 + matrix.M23) * num6;
				result.W = (matrix.M31 - matrix.M13) * num6;
			}
			else
			{
				float num7 = MathF.Sqrt(1f + matrix.M33 - matrix.M11 - matrix.M22);
				float num8 = 0.5f / num7;
				result.X = (matrix.M31 + matrix.M13) * num8;
				result.Y = (matrix.M32 + matrix.M23) * num8;
				result.Z = 0.5f * num7;
				result.W = (matrix.M12 - matrix.M21) * num8;
			}
			return result;
		}

		/// <summary>Calculates the dot product of two quaternions.</summary>
		/// <param name="quaternion1">The first quaternion.</param>
		/// <param name="quaternion2">The second quaternion.</param>
		/// <returns>The dot product.</returns>
		public static float Dot(Quaternion quaternion1, Quaternion quaternion2)
		{
			return quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
		}

		/// <summary>Interpolates between two quaternions, using spherical linear interpolation.</summary>
		/// <param name="quaternion1">The first quaternion.</param>
		/// <param name="quaternion2">The second quaternion.</param>
		/// <param name="amount">The relative weight of the second quaternion in the interpolation.</param>
		/// <returns>The interpolated quaternion.</returns>
		public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
		{
			float num = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
			bool flag = false;
			if (num < 0f)
			{
				flag = true;
				num = 0f - num;
			}
			float num2;
			float num3;
			if (num > 0.999999f)
			{
				num2 = 1f - amount;
				num3 = (flag ? (0f - amount) : amount);
			}
			else
			{
				float num4 = MathF.Acos(num);
				float num5 = 1f / MathF.Sin(num4);
				num2 = MathF.Sin((1f - amount) * num4) * num5;
				num3 = (flag ? ((0f - MathF.Sin(amount * num4)) * num5) : (MathF.Sin(amount * num4) * num5));
			}
			Quaternion result = default(Quaternion);
			result.X = num2 * quaternion1.X + num3 * quaternion2.X;
			result.Y = num2 * quaternion1.Y + num3 * quaternion2.Y;
			result.Z = num2 * quaternion1.Z + num3 * quaternion2.Z;
			result.W = num2 * quaternion1.W + num3 * quaternion2.W;
			return result;
		}

		/// <summary>Performs a linear interpolation between two quaternions based on a value that specifies the weighting of the second quaternion.</summary>
		/// <param name="quaternion1">The first quaternion.</param>
		/// <param name="quaternion2">The second quaternion.</param>
		/// <param name="amount">The relative weight of <paramref name="quaternion2" /> in the interpolation.</param>
		/// <returns>The interpolated quaternion.</returns>
		public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
		{
			float num = 1f - amount;
			Quaternion result = default(Quaternion);
			if (quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W >= 0f)
			{
				result.X = num * quaternion1.X + amount * quaternion2.X;
				result.Y = num * quaternion1.Y + amount * quaternion2.Y;
				result.Z = num * quaternion1.Z + amount * quaternion2.Z;
				result.W = num * quaternion1.W + amount * quaternion2.W;
			}
			else
			{
				result.X = num * quaternion1.X - amount * quaternion2.X;
				result.Y = num * quaternion1.Y - amount * quaternion2.Y;
				result.Z = num * quaternion1.Z - amount * quaternion2.Z;
				result.W = num * quaternion1.W - amount * quaternion2.W;
			}
			float x = result.X * result.X + result.Y * result.Y + result.Z * result.Z + result.W * result.W;
			float num2 = 1f / MathF.Sqrt(x);
			result.X *= num2;
			result.Y *= num2;
			result.Z *= num2;
			result.W *= num2;
			return result;
		}

		/// <summary>Concatenates two quaternions.</summary>
		/// <param name="value1">The first quaternion rotation in the series.</param>
		/// <param name="value2">The second quaternion rotation in the series.</param>
		/// <returns>A new quaternion representing the concatenation of the <paramref name="value1" /> rotation followed by the <paramref name="value2" /> rotation.</returns>
		public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
		{
			float x = value2.X;
			float y = value2.Y;
			float z = value2.Z;
			float w = value2.W;
			float x2 = value1.X;
			float y2 = value1.Y;
			float z2 = value1.Z;
			float w2 = value1.W;
			float num = y * z2 - z * y2;
			float num2 = z * x2 - x * z2;
			float num3 = x * y2 - y * x2;
			float num4 = x * x2 + y * y2 + z * z2;
			Quaternion result = default(Quaternion);
			result.X = x * w2 + x2 * w + num;
			result.Y = y * w2 + y2 * w + num2;
			result.Z = z * w2 + z2 * w + num3;
			result.W = w * w2 - num4;
			return result;
		}

		/// <summary>Reverses the sign of each component of the quaternion.</summary>
		/// <param name="value">The quaternion to negate.</param>
		/// <returns>The negated quaternion.</returns>
		public static Quaternion Negate(Quaternion value)
		{
			Quaternion result = default(Quaternion);
			result.X = 0f - value.X;
			result.Y = 0f - value.Y;
			result.Z = 0f - value.Z;
			result.W = 0f - value.W;
			return result;
		}

		/// <summary>Adds each element in one quaternion with its corresponding element in a second quaternion.</summary>
		/// <param name="value1">The first quaternion.</param>
		/// <param name="value2">The second quaternion.</param>
		/// <returns>The quaternion that contains the summed values of <paramref name="value1" /> and <paramref name="value2" />.</returns>
		public static Quaternion Add(Quaternion value1, Quaternion value2)
		{
			Quaternion result = default(Quaternion);
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			result.W = value1.W + value2.W;
			return result;
		}

		/// <summary>Subtracts each element in a second quaternion from its corresponding element in a first quaternion.</summary>
		/// <param name="value1">The first quaternion.</param>
		/// <param name="value2">The second quaternion.</param>
		/// <returns>The quaternion containing the values that result from subtracting each element in <paramref name="value2" /> from its corresponding element in <paramref name="value1" />.</returns>
		public static Quaternion Subtract(Quaternion value1, Quaternion value2)
		{
			Quaternion result = default(Quaternion);
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			result.W = value1.W - value2.W;
			return result;
		}

		/// <summary>Returns the quaternion that results from multiplying two quaternions together.</summary>
		/// <param name="value1">The first quaternion.</param>
		/// <param name="value2">The second quaternion.</param>
		/// <returns>The product quaternion.</returns>
		public static Quaternion Multiply(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float x2 = value2.X;
			float y2 = value2.Y;
			float z2 = value2.Z;
			float w2 = value2.W;
			float num = y * z2 - z * y2;
			float num2 = z * x2 - x * z2;
			float num3 = x * y2 - y * x2;
			float num4 = x * x2 + y * y2 + z * z2;
			Quaternion result = default(Quaternion);
			result.X = x * w2 + x2 * w + num;
			result.Y = y * w2 + y2 * w + num2;
			result.Z = z * w2 + z2 * w + num3;
			result.W = w * w2 - num4;
			return result;
		}

		/// <summary>Returns the quaternion that results from scaling all the components of a specified quaternion by a scalar factor.</summary>
		/// <param name="value1">The source quaternion.</param>
		/// <param name="value2">The scalar value.</param>
		/// <returns>The scaled quaternion.</returns>
		public static Quaternion Multiply(Quaternion value1, float value2)
		{
			Quaternion result = default(Quaternion);
			result.X = value1.X * value2;
			result.Y = value1.Y * value2;
			result.Z = value1.Z * value2;
			result.W = value1.W * value2;
			return result;
		}

		/// <summary>Divides one quaternion by a second quaternion.</summary>
		/// <param name="value1">The dividend.</param>
		/// <param name="value2">The divisor.</param>
		/// <returns>The quaternion that results from dividing <paramref name="value1" /> by <paramref name="value2" />.</returns>
		public static Quaternion Divide(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float num = value2.X * value2.X + value2.Y * value2.Y + value2.Z * value2.Z + value2.W * value2.W;
			float num2 = 1f / num;
			float num3 = (0f - value2.X) * num2;
			float num4 = (0f - value2.Y) * num2;
			float num5 = (0f - value2.Z) * num2;
			float num6 = value2.W * num2;
			float num7 = y * num5 - z * num4;
			float num8 = z * num3 - x * num5;
			float num9 = x * num4 - y * num3;
			float num10 = x * num3 + y * num4 + z * num5;
			Quaternion result = default(Quaternion);
			result.X = x * num6 + num3 * w + num7;
			result.Y = y * num6 + num4 * w + num8;
			result.Z = z * num6 + num5 * w + num9;
			result.W = w * num6 - num10;
			return result;
		}

		/// <summary>Reverses the sign of each component of the quaternion.</summary>
		/// <param name="value">The quaternion to negate.</param>
		/// <returns>The negated quaternion.</returns>
		public static Quaternion operator -(Quaternion value)
		{
			Quaternion result = default(Quaternion);
			result.X = 0f - value.X;
			result.Y = 0f - value.Y;
			result.Z = 0f - value.Z;
			result.W = 0f - value.W;
			return result;
		}

		/// <summary>Adds each element in one quaternion with its corresponding element in a second quaternion.</summary>
		/// <param name="value1">The first quaternion.</param>
		/// <param name="value2">The second quaternion.</param>
		/// <returns>The quaternion that contains the summed values of <paramref name="value1" /> and <paramref name="value2" />.</returns>
		public static Quaternion operator +(Quaternion value1, Quaternion value2)
		{
			Quaternion result = default(Quaternion);
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			result.W = value1.W + value2.W;
			return result;
		}

		/// <summary>Subtracts each element in a second quaternion from its corresponding element in a first quaternion.</summary>
		/// <param name="value1">The first quaternion.</param>
		/// <param name="value2">The second quaternion.</param>
		/// <returns>The quaternion containing the values that result from subtracting each element in <paramref name="value2" /> from its corresponding element in <paramref name="value1" />.</returns>
		public static Quaternion operator -(Quaternion value1, Quaternion value2)
		{
			Quaternion result = default(Quaternion);
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			result.W = value1.W - value2.W;
			return result;
		}

		/// <summary>Returns the quaternion that results from multiplying two quaternions together.</summary>
		/// <param name="value1">The first quaternion.</param>
		/// <param name="value2">The second quaternion.</param>
		/// <returns>The product quaternion.</returns>
		public static Quaternion operator *(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float x2 = value2.X;
			float y2 = value2.Y;
			float z2 = value2.Z;
			float w2 = value2.W;
			float num = y * z2 - z * y2;
			float num2 = z * x2 - x * z2;
			float num3 = x * y2 - y * x2;
			float num4 = x * x2 + y * y2 + z * z2;
			Quaternion result = default(Quaternion);
			result.X = x * w2 + x2 * w + num;
			result.Y = y * w2 + y2 * w + num2;
			result.Z = z * w2 + z2 * w + num3;
			result.W = w * w2 - num4;
			return result;
		}

		/// <summary>Returns the quaternion that results from scaling all the components of a specified quaternion by a scalar factor.</summary>
		/// <param name="value1">The source quaternion.</param>
		/// <param name="value2">The scalar value.</param>
		/// <returns>The scaled quaternion.</returns>
		public static Quaternion operator *(Quaternion value1, float value2)
		{
			Quaternion result = default(Quaternion);
			result.X = value1.X * value2;
			result.Y = value1.Y * value2;
			result.Z = value1.Z * value2;
			result.W = value1.W * value2;
			return result;
		}

		/// <summary>Divides one quaternion by a second quaternion.</summary>
		/// <param name="value1">The dividend.</param>
		/// <param name="value2">The divisor.</param>
		/// <returns>The quaternion that results from dividing <paramref name="value1" /> by <paramref name="value2" />.</returns>
		public static Quaternion operator /(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float num = value2.X * value2.X + value2.Y * value2.Y + value2.Z * value2.Z + value2.W * value2.W;
			float num2 = 1f / num;
			float num3 = (0f - value2.X) * num2;
			float num4 = (0f - value2.Y) * num2;
			float num5 = (0f - value2.Z) * num2;
			float num6 = value2.W * num2;
			float num7 = y * num5 - z * num4;
			float num8 = z * num3 - x * num5;
			float num9 = x * num4 - y * num3;
			float num10 = x * num3 + y * num4 + z * num5;
			Quaternion result = default(Quaternion);
			result.X = x * num6 + num3 * w + num7;
			result.Y = y * num6 + num4 * w + num8;
			result.Z = z * num6 + num5 * w + num9;
			result.W = w * num6 - num10;
			return result;
		}

		/// <summary>Returns a value that indicates whether two quaternions are equal.</summary>
		/// <param name="value1">The first quaternion to compare.</param>
		/// <param name="value2">The second quaternion to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the two quaternions are equal; otherwise, <see langword="false" />.</returns>
		public static bool operator ==(Quaternion value1, Quaternion value2)
		{
			if (value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z)
			{
				return value1.W == value2.W;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether two quaternions are not equal.</summary>
		/// <param name="value1">The first quaternion to compare.</param>
		/// <param name="value2">The second quaternion to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value1" /> and <paramref name="value2" /> are not equal; otherwise, <see langword="false" />.</returns>
		public static bool operator !=(Quaternion value1, Quaternion value2)
		{
			if (value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z)
			{
				return value1.W != value2.W;
			}
			return true;
		}

		/// <summary>Returns a value that indicates whether this instance and another quaternion are equal.</summary>
		/// <param name="other">The other quaternion.</param>
		/// <returns>
		///   <see langword="true" /> if the two quaternions are equal; otherwise, <see langword="false" />.</returns>
		public bool Equals(Quaternion other)
		{
			if (X == other.X && Y == other.Y && Z == other.Z)
			{
				return W == other.W;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance and <paramref name="obj" /> are equal; otherwise, <see langword="false" />. If <paramref name="obj" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Quaternion)
			{
				return Equals((Quaternion)obj);
			}
			return false;
		}

		/// <summary>Returns a string that represents this quaternion.</summary>
		/// <returns>The string representation of this quaternion.</returns>
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{X:{0} Y:{1} Z:{2} W:{3}}}", X.ToString(currentCulture), Y.ToString(currentCulture), Z.ToString(currentCulture), W.ToString(currentCulture));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
		}
	}
	/// <summary>Represents a vector with two single-precision floating-point values.</summary>
	public struct Vector2 : IEquatable<Vector2>, IFormattable
	{
		/// <summary>The X component of the vector.</summary>
		public float X;

		/// <summary>The Y component of the vector.</summary>
		public float Y;

		/// <summary>Returns a vector whose 2 elements are equal to zero.</summary>
		/// <returns>A vector whose two elements are equal to zero (that is, it returns the vector <c>(0,0)</c>.</returns>
		public static Vector2 Zero => default(Vector2);

		/// <summary>Gets a vector whose 2 elements are equal to one.</summary>
		/// <returns>A vector whose two elements are equal to one (that is, it returns the vector <c>(1,1)</c>.</returns>
		public static Vector2 One => new Vector2(1f, 1f);

		/// <summary>Gets the vector (1,0).</summary>
		/// <returns>The vector <c>(1,0)</c>.</returns>
		public static Vector2 UnitX => new Vector2(1f, 0f);

		/// <summary>Gets the vector (0,1).</summary>
		/// <returns>The vector <c>(0,1)</c>.</returns>
		public static Vector2 UnitY => new Vector2(0f, 1f);

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			return System.Numerics.Hashing.HashHelpers.Combine(X.GetHashCode(), Y.GetHashCode());
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance and <paramref name="obj" /> are equal; otherwise, <see langword="false" />. If <paramref name="obj" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			if (!(obj is Vector2))
			{
				return false;
			}
			return Equals((Vector2)obj);
		}

		/// <summary>Returns the string representation of the current instance using default formatting.</summary>
		/// <returns>The string representation of the current instance.</returns>
		public override string ToString()
		{
			return ToString("G", CultureInfo.CurrentCulture);
		}

		/// <summary>Returns the string representation of the current instance using the specified format string to format individual elements.</summary>
		/// <param name="format">A standard or custom numeric format string that defines the format of individual elements.</param>
		/// <returns>The string representation of the current instance.</returns>
		public string ToString(string format)
		{
			return ToString(format, CultureInfo.CurrentCulture);
		}

		/// <summary>Returns the string representation of the current instance using the specified format string to format individual elements and the specified format provider to define culture-specific formatting.</summary>
		/// <param name="format">A standard or custom numeric format string that defines the format of individual elements.</param>
		/// <param name="formatProvider">A format provider that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance.</returns>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
			stringBuilder.Append('<');
			stringBuilder.Append(X.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(Y.ToString(format, formatProvider));
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		/// <summary>Returns the length of the vector.</summary>
		/// <returns>The vector's length.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float Length()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return MathF.Sqrt(Dot(this, this));
			}
			return MathF.Sqrt(X * X + Y * Y);
		}

		/// <summary>Returns the length of the vector squared.</summary>
		/// <returns>The vector's length squared.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float LengthSquared()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Dot(this, this);
			}
			return X * X + Y * Y;
		}

		/// <summary>Computes the Euclidean distance between the two given points.</summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The distance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector2 value1, Vector2 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector2 vector = value1 - value2;
				return MathF.Sqrt(Dot(vector, vector));
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			return MathF.Sqrt(num * num + num2 * num2);
		}

		/// <summary>Returns the Euclidean distance squared between two specified points.</summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The distance squared.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquared(Vector2 value1, Vector2 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector2 vector = value1 - value2;
				return Dot(vector, vector);
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			return num * num + num2 * num2;
		}

		/// <summary>Returns a vector with the same direction as the specified vector, but with a length of one.</summary>
		/// <param name="value">The vector to normalize.</param>
		/// <returns>The normalized vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Normalize(Vector2 value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float num = value.Length();
				return value / num;
			}
			float x = value.X * value.X + value.Y * value.Y;
			float num2 = 1f / MathF.Sqrt(x);
			return new Vector2(value.X * num2, value.Y * num2);
		}

		/// <summary>Returns the reflection of a vector off a surface that has the specified normal.</summary>
		/// <param name="vector">The source vector.</param>
		/// <param name="normal">The normal of the surface being reflected off.</param>
		/// <returns>The reflected vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Reflect(Vector2 vector, Vector2 normal)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float num = Dot(vector, normal);
				return vector - 2f * num * normal;
			}
			float num2 = vector.X * normal.X + vector.Y * normal.Y;
			return new Vector2(vector.X - 2f * num2 * normal.X, vector.Y - 2f * num2 * normal.Y);
		}

		/// <summary>Restricts a vector between a minimum and a maximum value.</summary>
		/// <param name="value1">The vector to restrict.</param>
		/// <param name="min">The minimum value.</param>
		/// <param name="max">The maximum value.</param>
		/// <returns>The restricted vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
		{
			float x = value1.X;
			x = ((x > max.X) ? max.X : x);
			x = ((x < min.X) ? min.X : x);
			float y = value1.Y;
			y = ((y > max.Y) ? max.Y : y);
			y = ((y < min.Y) ? min.Y : y);
			return new Vector2(x, y);
		}

		/// <summary>Performs a linear interpolation between two vectors based on the given weighting.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="amount">A value between 0 and 1 that indicates the weight of <paramref name="value2" />.</param>
		/// <returns>The interpolated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
		{
			return new Vector2(value1.X + (value2.X - value1.X) * amount, value1.Y + (value2.Y - value1.Y) * amount);
		}

		/// <summary>Transforms a vector by a specified 3x2 matrix.</summary>
		/// <param name="position">The vector to transform.</param>
		/// <param name="matrix">The transformation matrix.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Transform(Vector2 position, Matrix3x2 matrix)
		{
			return new Vector2(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M31, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M32);
		}

		/// <summary>Transforms a vector by a specified 4x4 matrix.</summary>
		/// <param name="position">The vector to transform.</param>
		/// <param name="matrix">The transformation matrix.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Transform(Vector2 position, Matrix4x4 matrix)
		{
			return new Vector2(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42);
		}

		/// <summary>Transforms a vector normal by the given 3x2 matrix.</summary>
		/// <param name="normal">The source vector.</param>
		/// <param name="matrix">The matrix.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 TransformNormal(Vector2 normal, Matrix3x2 matrix)
		{
			return new Vector2(normal.X * matrix.M11 + normal.Y * matrix.M21, normal.X * matrix.M12 + normal.Y * matrix.M22);
		}

		/// <summary>Transforms a vector normal by the given 4x4 matrix.</summary>
		/// <param name="normal">The source vector.</param>
		/// <param name="matrix">The matrix.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 TransformNormal(Vector2 normal, Matrix4x4 matrix)
		{
			return new Vector2(normal.X * matrix.M11 + normal.Y * matrix.M21, normal.X * matrix.M12 + normal.Y * matrix.M22);
		}

		/// <summary>Transforms a vector by the specified Quaternion rotation value.</summary>
		/// <param name="value">The vector to rotate.</param>
		/// <param name="rotation">The rotation to apply.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Transform(Vector2 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num3;
			float num5 = rotation.X * num;
			float num6 = rotation.X * num2;
			float num7 = rotation.Y * num2;
			float num8 = rotation.Z * num3;
			return new Vector2(value.X * (1f - num7 - num8) + value.Y * (num6 - num4), value.X * (num6 + num4) + value.Y * (1f - num5 - num8));
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector to add.</param>
		/// <param name="right">The second vector to add.</param>
		/// <returns>The summed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Add(Vector2 left, Vector2 right)
		{
			return left + right;
		}

		/// <summary>Subtracts the second vector from the first.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The difference vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Subtract(Vector2 left, Vector2 right)
		{
			return left - right;
		}

		/// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The element-wise product vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Multiply(Vector2 left, Vector2 right)
		{
			return left * right;
		}

		/// <summary>Multiplies a vector by a specified scalar.</summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Multiply(Vector2 left, float right)
		{
			return left * right;
		}

		/// <summary>Multiplies a scalar value by a specified vector.</summary>
		/// <param name="left">The scaled value.</param>
		/// <param name="right">The vector.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Multiply(float left, Vector2 right)
		{
			return left * right;
		}

		/// <summary>Divides the first vector by the second.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector resulting from the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Divide(Vector2 left, Vector2 right)
		{
			return left / right;
		}

		/// <summary>Divides the specified vector by a specified scalar value.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="divisor">The scalar value.</param>
		/// <returns>The vector that results from the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Divide(Vector2 left, float divisor)
		{
			return left / divisor;
		}

		/// <summary>Negates a specified vector.</summary>
		/// <param name="value">The vector to negate.</param>
		/// <returns>The negated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Negate(Vector2 value)
		{
			return -value;
		}

		/// <summary>Creates a new <see cref="T:System.Numerics.Vector2" /> object whose two elements have the same value.</summary>
		/// <param name="value">The value to assign to both elements.</param>
		[System.Runtime.CompilerServices.Intrinsic]
		public Vector2(float value)
			: this(value, value)
		{
		}

		/// <summary>Creates a vector whose elements have the specified values.</summary>
		/// <param name="x">The value to assign to the <see cref="F:System.Numerics.Vector2.X" /> field.</param>
		/// <param name="y">The value to assign to the <see cref="F:System.Numerics.Vector2.Y" /> field.</param>
		[System.Runtime.CompilerServices.Intrinsic]
		public Vector2(float x, float y)
		{
			X = x;
			Y = y;
		}

		/// <summary>Copies the elements of the vector to a specified array.</summary>
		/// <param name="array">The destination array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CopyTo(float[] array)
		{
			CopyTo(array, 0);
		}

		/// <summary>Copies the elements of the vector to a specified array starting at a specified index position.</summary>
		/// <param name="array">The destination array.</param>
		/// <param name="index">The index at which to copy the first element of the vector.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than or equal to the array length.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		public void CopyTo(float[] array, int index)
		{
			if (array == null)
			{
				throw new NullReferenceException("The method was called with a null array argument.");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", global::SR.Format("Index was out of bounds:", index));
			}
			if (array.Length - index < 2)
			{
				throw new ArgumentException(global::SR.Format("Number of elements in source vector is greater than the destination array", index));
			}
			array[index] = X;
			array[index + 1] = Y;
		}

		/// <summary>Returns a value that indicates whether this instance and another vector are equal.</summary>
		/// <param name="other">The other vector.</param>
		/// <returns>
		///   <see langword="true" /> if the two vectors are equal; otherwise, <see langword="false" />.</returns>
		[System.Runtime.CompilerServices.Intrinsic]
		public bool Equals(Vector2 other)
		{
			if (X == other.X)
			{
				return Y == other.Y;
			}
			return false;
		}

		/// <summary>Returns the dot product of two vectors.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The dot product.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static float Dot(Vector2 value1, Vector2 value2)
		{
			return value1.X * value2.X + value1.Y * value2.Y;
		}

		/// <summary>Returns a vector whose elements are the minimum of each of the pairs of elements in two specified vectors.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The minimized vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector2 Min(Vector2 value1, Vector2 value2)
		{
			return new Vector2((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y);
		}

		/// <summary>Returns a vector whose elements are the maximum of each of the pairs of elements in two specified vectors.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The maximized vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector2 Max(Vector2 value1, Vector2 value2)
		{
			return new Vector2((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y);
		}

		/// <summary>Returns a vector whose elements are the absolute values of each of the specified vector's elements.</summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector2 Abs(Vector2 value)
		{
			return new Vector2(MathF.Abs(value.X), MathF.Abs(value.Y));
		}

		/// <summary>Returns a vector whose elements are the square root of each of a specified vector's elements.</summary>
		/// <param name="value">A vector.</param>
		/// <returns>The square root vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector2 SquareRoot(Vector2 value)
		{
			return new Vector2(MathF.Sqrt(value.X), MathF.Sqrt(value.Y));
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector to add.</param>
		/// <param name="right">The second vector to add.</param>
		/// <returns>The summed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector2 operator +(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X + right.X, left.Y + right.Y);
		}

		/// <summary>Subtracts the second vector from the first.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector that results from subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector2 operator -(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X - right.X, left.Y - right.Y);
		}

		/// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The element-wise product vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector2 operator *(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X * right.X, left.Y * right.Y);
		}

		/// <summary>Multiples the scalar value by the specified vector.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector2 operator *(float left, Vector2 right)
		{
			return new Vector2(left, left) * right;
		}

		/// <summary>Multiples the specified vector by the specified scalar value.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector2 operator *(Vector2 left, float right)
		{
			return left * new Vector2(right, right);
		}

		/// <summary>Divides the first vector by the second.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector that results from dividing <paramref name="left" /> by <paramref name="right" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector2 operator /(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X / right.X, left.Y / right.Y);
		}

		/// <summary>Divides the specified vector by a specified scalar value.</summary>
		/// <param name="value1">The vector.</param>
		/// <param name="value2">The scalar value.</param>
		/// <returns>The result of the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator /(Vector2 value1, float value2)
		{
			return value1 / new Vector2(value2);
		}

		/// <summary>Negates the specified vector.</summary>
		/// <param name="value">The vector to negate.</param>
		/// <returns>The negated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator -(Vector2 value)
		{
			return Zero - value;
		}

		/// <summary>Returns a value that indicates whether each pair of elements in two specified vectors is equal.</summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector2 left, Vector2 right)
		{
			return left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether two specified vectors are not equal.</summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector2 left, Vector2 right)
		{
			return !(left == right);
		}
	}
	/// <summary>Represents a vector with three  single-precision floating-point values.</summary>
	public struct Vector3 : IEquatable<Vector3>, IFormattable
	{
		/// <summary>The X component of the vector.</summary>
		public float X;

		/// <summary>The Y component of the vector.</summary>
		public float Y;

		/// <summary>The Z component of the vector.</summary>
		public float Z;

		/// <summary>Gets a vector whose 3 elements are equal to zero.</summary>
		/// <returns>A vector whose three elements are equal to zero (that is, it returns the vector <c>(0,0,0)</c>.</returns>
		public static Vector3 Zero => default(Vector3);

		/// <summary>Gets a vector whose 3 elements are equal to one.</summary>
		/// <returns>A vector whose three elements are equal to one (that is, it returns the vector <c>(1,1,1)</c>.</returns>
		public static Vector3 One => new Vector3(1f, 1f, 1f);

		/// <summary>Gets the vector (1,0,0).</summary>
		/// <returns>The vector <c>(1,0,0)</c>.</returns>
		public static Vector3 UnitX => new Vector3(1f, 0f, 0f);

		/// <summary>Gets the vector (0,1,0).</summary>
		/// <returns>The vector <c>(0,1,0)</c>.</returns>
		public static Vector3 UnitY => new Vector3(0f, 1f, 0f);

		/// <summary>Gets the vector (0,0,1).</summary>
		/// <returns>The vector <c>(0,0,1)</c>.</returns>
		public static Vector3 UnitZ => new Vector3(0f, 0f, 1f);

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			return System.Numerics.Hashing.HashHelpers.Combine(System.Numerics.Hashing.HashHelpers.Combine(X.GetHashCode(), Y.GetHashCode()), Z.GetHashCode());
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance and <paramref name="obj" /> are equal; otherwise, <see langword="false" />. If <paramref name="obj" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			if (!(obj is Vector3))
			{
				return false;
			}
			return Equals((Vector3)obj);
		}

		/// <summary>Returns the string representation of the current instance using default formatting.</summary>
		/// <returns>The string representation of the current instance.</returns>
		public override string ToString()
		{
			return ToString("G", CultureInfo.CurrentCulture);
		}

		/// <summary>Returns the string representation of the current instance using the specified format string to format individual elements.</summary>
		/// <param name="format">A standard or custom numeric format string that defines the format of individual elements.</param>
		/// <returns>The string representation of the current instance.</returns>
		public string ToString(string format)
		{
			return ToString(format, CultureInfo.CurrentCulture);
		}

		/// <summary>Returns the string representation of the current instance using the specified format string to format individual elements and the specified format provider to define culture-specific formatting.</summary>
		/// <param name="format">A standard or custom numeric format string that defines the format of individual elements.</param>
		/// <param name="formatProvider">A format provider that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance.</returns>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
			stringBuilder.Append('<');
			stringBuilder.Append(((IFormattable)X).ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(((IFormattable)Y).ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(((IFormattable)Z).ToString(format, formatProvider));
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		/// <summary>Returns the length of this vector object.</summary>
		/// <returns>The vector's length.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float Length()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return MathF.Sqrt(Dot(this, this));
			}
			return MathF.Sqrt(X * X + Y * Y + Z * Z);
		}

		/// <summary>Returns the length of the vector squared.</summary>
		/// <returns>The vector's length squared.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float LengthSquared()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Dot(this, this);
			}
			return X * X + Y * Y + Z * Z;
		}

		/// <summary>Computes the Euclidean distance between the two given points.</summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The distance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector3 value1, Vector3 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 vector = value1 - value2;
				return MathF.Sqrt(Dot(vector, vector));
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			return MathF.Sqrt(num * num + num2 * num2 + num3 * num3);
		}

		/// <summary>Returns the Euclidean distance squared between two specified points.</summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The distance squared.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquared(Vector3 value1, Vector3 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 vector = value1 - value2;
				return Dot(vector, vector);
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			return num * num + num2 * num2 + num3 * num3;
		}

		/// <summary>Returns a vector with the same direction as the specified vector, but with a length of one.</summary>
		/// <param name="value">The vector to normalize.</param>
		/// <returns>The normalized vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Normalize(Vector3 value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float num = value.Length();
				return value / num;
			}
			float num2 = MathF.Sqrt(value.X * value.X + value.Y * value.Y + value.Z * value.Z);
			return new Vector3(value.X / num2, value.Y / num2, value.Z / num2);
		}

		/// <summary>Computes the cross product of two vectors.</summary>
		/// <param name="vector1">The first vector.</param>
		/// <param name="vector2">The second vector.</param>
		/// <returns>The cross product.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
		{
			return new Vector3(vector1.Y * vector2.Z - vector1.Z * vector2.Y, vector1.Z * vector2.X - vector1.X * vector2.Z, vector1.X * vector2.Y - vector1.Y * vector2.X);
		}

		/// <summary>Returns the reflection of a vector off a surface that has the specified normal.</summary>
		/// <param name="vector">The source vector.</param>
		/// <param name="normal">The normal of the surface being reflected off.</param>
		/// <returns>The reflected vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Reflect(Vector3 vector, Vector3 normal)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float num = Dot(vector, normal);
				Vector3 vector2 = normal * num * 2f;
				return vector - vector2;
			}
			float num2 = vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z;
			float num3 = normal.X * num2 * 2f;
			float num4 = normal.Y * num2 * 2f;
			float num5 = normal.Z * num2 * 2f;
			return new Vector3(vector.X - num3, vector.Y - num4, vector.Z - num5);
		}

		/// <summary>Restricts a vector between a minimum and a maximum value.</summary>
		/// <param name="value1">The vector to restrict.</param>
		/// <param name="min">The minimum value.</param>
		/// <param name="max">The maximum value.</param>
		/// <returns>The restricted vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max)
		{
			float x = value1.X;
			x = ((x > max.X) ? max.X : x);
			x = ((x < min.X) ? min.X : x);
			float y = value1.Y;
			y = ((y > max.Y) ? max.Y : y);
			y = ((y < min.Y) ? min.Y : y);
			float z = value1.Z;
			z = ((z > max.Z) ? max.Z : z);
			z = ((z < min.Z) ? min.Z : z);
			return new Vector3(x, y, z);
		}

		/// <summary>Performs a linear interpolation between two vectors based on the given weighting.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="amount">A value between 0 and 1 that indicates the weight of <paramref name="value2" />.</param>
		/// <returns>The interpolated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Lerp(Vector3 value1, Vector3 value2, float amount)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 vector = value1 * (1f - amount);
				Vector3 vector2 = value2 * amount;
				return vector + vector2;
			}
			return new Vector3(value1.X + (value2.X - value1.X) * amount, value1.Y + (value2.Y - value1.Y) * amount, value1.Z + (value2.Z - value1.Z) * amount);
		}

		/// <summary>Transforms a vector by a specified 4x4 matrix.</summary>
		/// <param name="position">The vector to transform.</param>
		/// <param name="matrix">The transformation matrix.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Transform(Vector3 position, Matrix4x4 matrix)
		{
			return new Vector3(position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43);
		}

		/// <summary>Transforms a vector normal by the given 4x4 matrix.</summary>
		/// <param name="normal">The source vector.</param>
		/// <param name="matrix">The matrix.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 TransformNormal(Vector3 normal, Matrix4x4 matrix)
		{
			return new Vector3(normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31, normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32, normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33);
		}

		/// <summary>Transforms a vector by the specified Quaternion rotation value.</summary>
		/// <param name="value">The vector to rotate.</param>
		/// <param name="rotation">The rotation to apply.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Transform(Vector3 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			return new Vector3(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10));
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector to add.</param>
		/// <param name="right">The second vector to add.</param>
		/// <returns>The summed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Add(Vector3 left, Vector3 right)
		{
			return left + right;
		}

		/// <summary>Subtracts the second vector from the first.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The difference vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Subtract(Vector3 left, Vector3 right)
		{
			return left - right;
		}

		/// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The element-wise product vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(Vector3 left, Vector3 right)
		{
			return left * right;
		}

		/// <summary>Multiplies a vector by a specified scalar.</summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(Vector3 left, float right)
		{
			return left * right;
		}

		/// <summary>Multiplies a scalar value by a specified vector.</summary>
		/// <param name="left">The scaled value.</param>
		/// <param name="right">The vector.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(float left, Vector3 right)
		{
			return left * right;
		}

		/// <summary>Divides the first vector by the second.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector resulting from the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Divide(Vector3 left, Vector3 right)
		{
			return left / right;
		}

		/// <summary>Divides the specified vector by a specified scalar value.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="divisor">The scalar value.</param>
		/// <returns>The vector that results from the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Divide(Vector3 left, float divisor)
		{
			return left / divisor;
		}

		/// <summary>Negates a specified vector.</summary>
		/// <param name="value">The vector to negate.</param>
		/// <returns>The negated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Negate(Vector3 value)
		{
			return -value;
		}

		/// <summary>Creates a new <see cref="T:System.Numerics.Vector3" /> object whose three elements have the same value.</summary>
		/// <param name="value">The value to assign to all three elements.</param>
		[System.Runtime.CompilerServices.Intrinsic]
		public Vector3(float value)
			: this(value, value, value)
		{
		}

		/// <summary>Creates a   new <see cref="T:System.Numerics.Vector3" /> object from the specified <see cref="T:System.Numerics.Vector2" /> object and the specified value.</summary>
		/// <param name="value">The vector with two elements.</param>
		/// <param name="z">The additional value to assign to the <see cref="F:System.Numerics.Vector3.Z" /> field.</param>
		public Vector3(Vector2 value, float z)
			: this(value.X, value.Y, z)
		{
		}

		/// <summary>Creates a vector whose elements have the specified values.</summary>
		/// <param name="x">The value to assign to the <see cref="F:System.Numerics.Vector3.X" /> field.</param>
		/// <param name="y">The value to assign to the <see cref="F:System.Numerics.Vector3.Y" /> field.</param>
		/// <param name="z">The value to assign to the <see cref="F:System.Numerics.Vector3.Z" /> field.</param>
		[System.Runtime.CompilerServices.Intrinsic]
		public Vector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		/// <summary>Copies the elements of the vector to a specified array.</summary>
		/// <param name="array">The destination array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CopyTo(float[] array)
		{
			CopyTo(array, 0);
		}

		/// <summary>Copies the elements of the vector to a specified array starting at a specified index position.</summary>
		/// <param name="array">The destination array.</param>
		/// <param name="index">The index at which to copy the first element of the vector.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than or equal to the array length.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public void CopyTo(float[] array, int index)
		{
			if (array == null)
			{
				throw new NullReferenceException("The method was called with a null array argument.");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", global::SR.Format("Index was out of bounds:", index));
			}
			if (array.Length - index < 3)
			{
				throw new ArgumentException(global::SR.Format("Number of elements in source vector is greater than the destination array", index));
			}
			array[index] = X;
			array[index + 1] = Y;
			array[index + 2] = Z;
		}

		/// <summary>Returns a value that indicates whether this instance and another vector are equal.</summary>
		/// <param name="other">The other vector.</param>
		/// <returns>
		///   <see langword="true" /> if the two vectors are equal; otherwise, <see langword="false" />.</returns>
		[System.Runtime.CompilerServices.Intrinsic]
		public bool Equals(Vector3 other)
		{
			if (X == other.X && Y == other.Y)
			{
				return Z == other.Z;
			}
			return false;
		}

		/// <summary>Returns the dot product of two vectors.</summary>
		/// <param name="vector1">The first vector.</param>
		/// <param name="vector2">The second vector.</param>
		/// <returns>The dot product.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static float Dot(Vector3 vector1, Vector3 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
		}

		/// <summary>Returns a vector whose elements are the minimum of each of the pairs of elements in two specified vectors.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The minimized vector.</returns>
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector3 Min(Vector3 value1, Vector3 value2)
		{
			return new Vector3((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y, (value1.Z < value2.Z) ? value1.Z : value2.Z);
		}

		/// <summary>Returns a vector whose elements are the maximum of each of the pairs of elements in two specified vectors.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The maximized vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector3 Max(Vector3 value1, Vector3 value2)
		{
			return new Vector3((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y, (value1.Z > value2.Z) ? value1.Z : value2.Z);
		}

		/// <summary>Returns a vector whose elements are the absolute values of each of the specified vector's elements.</summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector3 Abs(Vector3 value)
		{
			return new Vector3(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z));
		}

		/// <summary>Returns a vector whose elements are the square root of each of a specified vector's elements.</summary>
		/// <param name="value">A vector.</param>
		/// <returns>The square root vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector3 SquareRoot(Vector3 value)
		{
			return new Vector3(MathF.Sqrt(value.X), MathF.Sqrt(value.Y), MathF.Sqrt(value.Z));
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector to add.</param>
		/// <param name="right">The second vector to add.</param>
		/// <returns>The summed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector3 operator +(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		/// <summary>Subtracts the second vector from the first.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector that results from subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector3 operator -(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		/// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The element-wise product vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector3 operator *(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}

		/// <summary>Multiples the specified vector by the specified scalar value.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector3 operator *(Vector3 left, float right)
		{
			return left * new Vector3(right);
		}

		/// <summary>Multiples the scalar value by the specified vector.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector3 operator *(float left, Vector3 right)
		{
			return new Vector3(left) * right;
		}

		/// <summary>Divides the first vector by the second.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector that results from dividing <paramref name="left" /> by <paramref name="right" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector3 operator /(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
		}

		/// <summary>Divides the specified vector by a specified scalar value.</summary>
		/// <param name="value1">The vector.</param>
		/// <param name="value2">The scalar value.</param>
		/// <returns>The result of the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator /(Vector3 value1, float value2)
		{
			return value1 / new Vector3(value2);
		}

		/// <summary>Negates the specified vector.</summary>
		/// <param name="value">The vector to negate.</param>
		/// <returns>The negated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 value)
		{
			return Zero - value;
		}

		/// <summary>Returns a value that indicates whether each pair of elements in two specified vectors is equal.</summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static bool operator ==(Vector3 left, Vector3 right)
		{
			if (left.X == right.X && left.Y == right.Y)
			{
				return left.Z == right.Z;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether two specified vectors are not equal.</summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector3 left, Vector3 right)
		{
			if (left.X == right.X && left.Y == right.Y)
			{
				return left.Z != right.Z;
			}
			return true;
		}
	}
	/// <summary>Represents a vector with four single-precision floating-point values.</summary>
	public struct Vector4 : IEquatable<Vector4>, IFormattable
	{
		/// <summary>The X component of the vector.</summary>
		public float X;

		/// <summary>The Y component of the vector.</summary>
		public float Y;

		/// <summary>The Z component of the vector.</summary>
		public float Z;

		/// <summary>The W component of the vector.</summary>
		public float W;

		/// <summary>Gets a vector whose 4 elements are equal to zero.</summary>
		/// <returns>A vector whose four elements are equal to zero (that is, it returns the vector <c>(0,0,0,0)</c>.</returns>
		public static Vector4 Zero => default(Vector4);

		/// <summary>Gets a vector whose 4 elements are equal to one.</summary>
		/// <returns>Returns <see cref="T:System.Numerics.Vector4" />.</returns>
		public static Vector4 One => new Vector4(1f, 1f, 1f, 1f);

		/// <summary>Gets the vector (1,0,0,0).</summary>
		/// <returns>The vector <c>(1,0,0,0)</c>.</returns>
		public static Vector4 UnitX => new Vector4(1f, 0f, 0f, 0f);

		/// <summary>Gets the vector (0,1,0,0).</summary>
		/// <returns>The vector <c>(0,1,0,0)</c>.</returns>
		public static Vector4 UnitY => new Vector4(0f, 1f, 0f, 0f);

		/// <summary>Gets the vector (0,0,1,0).</summary>
		/// <returns>The vector <c>(0,0,1,0)</c>.</returns>
		public static Vector4 UnitZ => new Vector4(0f, 0f, 1f, 0f);

		/// <summary>Gets the vector (0,0,0,1).</summary>
		/// <returns>The vector <c>(0,0,0,1)</c>.</returns>
		public static Vector4 UnitW => new Vector4(0f, 0f, 0f, 1f);

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			return System.Numerics.Hashing.HashHelpers.Combine(System.Numerics.Hashing.HashHelpers.Combine(System.Numerics.Hashing.HashHelpers.Combine(X.GetHashCode(), Y.GetHashCode()), Z.GetHashCode()), W.GetHashCode());
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance and <paramref name="obj" /> are equal; otherwise, <see langword="false" />. If <paramref name="obj" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			if (!(obj is Vector4))
			{
				return false;
			}
			return Equals((Vector4)obj);
		}

		/// <summary>Returns the string representation of the current instance using default formatting.</summary>
		/// <returns>The string representation of the current instance.</returns>
		public override string ToString()
		{
			return ToString("G", CultureInfo.CurrentCulture);
		}

		/// <summary>Returns the string representation of the current instance using the specified format string to format individual elements.</summary>
		/// <param name="format">A standard or custom numeric format string that defines the format of individual elements.</param>
		/// <returns>The string representation of the current instance.</returns>
		public string ToString(string format)
		{
			return ToString(format, CultureInfo.CurrentCulture);
		}

		/// <summary>Returns the string representation of the current instance using the specified format string to format individual elements and the specified format provider to define culture-specific formatting.</summary>
		/// <param name="format">A standard or custom numeric format string that defines the format of individual elements.</param>
		/// <param name="formatProvider">A format provider that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance.</returns>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
			stringBuilder.Append('<');
			stringBuilder.Append(X.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(Y.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(Z.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(W.ToString(format, formatProvider));
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		/// <summary>Returns the length of this vector object.</summary>
		/// <returns>The vector's length.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float Length()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return MathF.Sqrt(Dot(this, this));
			}
			return MathF.Sqrt(X * X + Y * Y + Z * Z + W * W);
		}

		/// <summary>Returns the length of the vector squared.</summary>
		/// <returns>The vector's length squared.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float LengthSquared()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Dot(this, this);
			}
			return X * X + Y * Y + Z * Z + W * W;
		}

		/// <summary>Computes the Euclidean distance between the two given points.</summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The distance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector4 value1, Vector4 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector4 vector = value1 - value2;
				return MathF.Sqrt(Dot(vector, vector));
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			float num4 = value1.W - value2.W;
			return MathF.Sqrt(num * num + num2 * num2 + num3 * num3 + num4 * num4);
		}

		/// <summary>Returns the Euclidean distance squared between two specified points.</summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The distance squared.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquared(Vector4 value1, Vector4 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector4 vector = value1 - value2;
				return Dot(vector, vector);
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			float num4 = value1.W - value2.W;
			return num * num + num2 * num2 + num3 * num3 + num4 * num4;
		}

		/// <summary>Returns a vector with the same direction as the specified vector, but with a length of one.</summary>
		/// <param name="vector">The vector to normalize.</param>
		/// <returns>The normalized vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Normalize(Vector4 vector)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float num = vector.Length();
				return vector / num;
			}
			float x = vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z + vector.W * vector.W;
			float num2 = 1f / MathF.Sqrt(x);
			return new Vector4(vector.X * num2, vector.Y * num2, vector.Z * num2, vector.W * num2);
		}

		/// <summary>Restricts a vector between a minimum and a maximum value.</summary>
		/// <param name="value1">The vector to restrict.</param>
		/// <param name="min">The minimum value.</param>
		/// <param name="max">The maximum value.</param>
		/// <returns>The restricted vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Clamp(Vector4 value1, Vector4 min, Vector4 max)
		{
			float x = value1.X;
			x = ((x > max.X) ? max.X : x);
			x = ((x < min.X) ? min.X : x);
			float y = value1.Y;
			y = ((y > max.Y) ? max.Y : y);
			y = ((y < min.Y) ? min.Y : y);
			float z = value1.Z;
			z = ((z > max.Z) ? max.Z : z);
			z = ((z < min.Z) ? min.Z : z);
			float w = value1.W;
			w = ((w > max.W) ? max.W : w);
			w = ((w < min.W) ? min.W : w);
			return new Vector4(x, y, z, w);
		}

		/// <summary>Performs a linear interpolation between two vectors based on the given weighting.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="amount">A value between 0 and 1 that indicates the weight of <paramref name="value2" />.</param>
		/// <returns>The interpolated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Lerp(Vector4 value1, Vector4 value2, float amount)
		{
			return new Vector4(value1.X + (value2.X - value1.X) * amount, value1.Y + (value2.Y - value1.Y) * amount, value1.Z + (value2.Z - value1.Z) * amount, value1.W + (value2.W - value1.W) * amount);
		}

		/// <summary>Transforms a two-dimensional vector by a specified 4x4 matrix.</summary>
		/// <param name="position">The vector to transform.</param>
		/// <param name="matrix">The transformation matrix.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector2 position, Matrix4x4 matrix)
		{
			return new Vector4(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44);
		}

		/// <summary>Transforms a three-dimensional vector by a specified 4x4 matrix.</summary>
		/// <param name="position">The vector to transform.</param>
		/// <param name="matrix">The transformation matrix.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector3 position, Matrix4x4 matrix)
		{
			return new Vector4(position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + position.Z * matrix.M34 + matrix.M44);
		}

		/// <summary>Transforms a four-dimensional vector by a specified 4x4 matrix.</summary>
		/// <param name="vector">The vector to transform.</param>
		/// <param name="matrix">The transformation matrix.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector4 vector, Matrix4x4 matrix)
		{
			return new Vector4(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41, vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42, vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43, vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44);
		}

		/// <summary>Transforms a two-dimensional vector by the specified Quaternion rotation value.</summary>
		/// <param name="value">The vector to rotate.</param>
		/// <param name="rotation">The rotation to apply.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector2 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6), value.X * (num8 + num6) + value.Y * (1f - num7 - num12), value.X * (num9 - num5) + value.Y * (num11 + num4), 1f);
		}

		/// <summary>Transforms a three-dimensional vector by the specified Quaternion rotation value.</summary>
		/// <param name="value">The vector to rotate.</param>
		/// <param name="rotation">The rotation to apply.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector3 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), 1f);
		}

		/// <summary>Transforms a four-dimensional vector by the specified Quaternion rotation value.</summary>
		/// <param name="value">The vector to rotate.</param>
		/// <param name="rotation">The rotation to apply.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector4 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), value.W);
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector to add.</param>
		/// <param name="right">The second vector to add.</param>
		/// <returns>The summed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Add(Vector4 left, Vector4 right)
		{
			return left + right;
		}

		/// <summary>Subtracts the second vector from the first.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The difference vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Subtract(Vector4 left, Vector4 right)
		{
			return left - right;
		}

		/// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The element-wise product vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(Vector4 left, Vector4 right)
		{
			return left * right;
		}

		/// <summary>Multiplies a vector by a specified scalar.</summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(Vector4 left, float right)
		{
			return left * new Vector4(right, right, right, right);
		}

		/// <summary>Multiplies a scalar value by a specified vector.</summary>
		/// <param name="left">The scaled value.</param>
		/// <param name="right">The vector.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(float left, Vector4 right)
		{
			return new Vector4(left, left, left, left) * right;
		}

		/// <summary>Divides the first vector by the second.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector resulting from the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Divide(Vector4 left, Vector4 right)
		{
			return left / right;
		}

		/// <summary>Divides the specified vector by a specified scalar value.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="divisor">The scalar value.</param>
		/// <returns>The vector that results from the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Divide(Vector4 left, float divisor)
		{
			return left / divisor;
		}

		/// <summary>Negates a specified vector.</summary>
		/// <param name="value">The vector to negate.</param>
		/// <returns>The negated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Negate(Vector4 value)
		{
			return -value;
		}

		/// <summary>Creates a new <see cref="T:System.Numerics.Vector4" /> object whose four elements have the same value.</summary>
		/// <param name="value">The value to assign to all four elements.</param>
		[System.Runtime.CompilerServices.Intrinsic]
		public Vector4(float value)
			: this(value, value, value, value)
		{
		}

		/// <summary>Creates a vector whose elements have the specified values.</summary>
		/// <param name="x">The value to assign to the <see cref="F:System.Numerics.Vector4.X" /> field.</param>
		/// <param name="y">The value to assign to the <see cref="F:System.Numerics.Vector4.Y" /> field.</param>
		/// <param name="z">The value to assign to the <see cref="F:System.Numerics.Vector4.Z" /> field.</param>
		/// <param name="w">The value to assign to the <see cref="F:System.Numerics.Vector4.W" /> field.</param>
		[System.Runtime.CompilerServices.Intrinsic]
		public Vector4(float x, float y, float z, float w)
		{
			W = w;
			X = x;
			Y = y;
			Z = z;
		}

		/// <summary>Creates a   new <see cref="T:System.Numerics.Vector4" /> object from the specified <see cref="T:System.Numerics.Vector2" /> object and a Z and a W component.</summary>
		/// <param name="value">The vector to use for the X and Y components.</param>
		/// <param name="z">The Z component.</param>
		/// <param name="w">The W component.</param>
		public Vector4(Vector2 value, float z, float w)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
			W = w;
		}

		/// <summary>Constructs a new <see cref="T:System.Numerics.Vector4" /> object from the specified <see cref="T:System.Numerics.Vector3" /> object and a W component.</summary>
		/// <param name="value">The vector to use for the X, Y, and Z components.</param>
		/// <param name="w">The W component.</param>
		public Vector4(Vector3 value, float w)
		{
			X = value.X;
			Y = value.Y;
			Z = value.Z;
			W = w;
		}

		/// <summary>Copies the elements of the vector to a specified array.</summary>
		/// <param name="array">The destination array.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CopyTo(float[] array)
		{
			CopyTo(array, 0);
		}

		/// <summary>Copies the elements of the vector to a specified array starting at a specified index position.</summary>
		/// <param name="array">The destination array.</param>
		/// <param name="index">The index at which to copy the first element of the vector.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than or equal to the array length.</exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public void CopyTo(float[] array, int index)
		{
			if (array == null)
			{
				throw new NullReferenceException("The method was called with a null array argument.");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", global::SR.Format("Index was out of bounds:", index));
			}
			if (array.Length - index < 4)
			{
				throw new ArgumentException(global::SR.Format("Number of elements in source vector is greater than the destination array", index));
			}
			array[index] = X;
			array[index + 1] = Y;
			array[index + 2] = Z;
			array[index + 3] = W;
		}

		/// <summary>Returns a value that indicates whether this instance and another vector are equal.</summary>
		/// <param name="other">The other vector.</param>
		/// <returns>
		///   <see langword="true" /> if the two vectors are equal; otherwise, <see langword="false" />.</returns>
		[System.Runtime.CompilerServices.Intrinsic]
		public bool Equals(Vector4 other)
		{
			if (X == other.X && Y == other.Y && Z == other.Z)
			{
				return W == other.W;
			}
			return false;
		}

		/// <summary>Returns the dot product of two vectors.</summary>
		/// <param name="vector1">The first vector.</param>
		/// <param name="vector2">The second vector.</param>
		/// <returns>The dot product.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static float Dot(Vector4 vector1, Vector4 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
		}

		/// <summary>Returns a vector whose elements are the minimum of each of the pairs of elements in two specified vectors.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The minimized vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector4 Min(Vector4 value1, Vector4 value2)
		{
			return new Vector4((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y, (value1.Z < value2.Z) ? value1.Z : value2.Z, (value1.W < value2.W) ? value1.W : value2.W);
		}

		/// <summary>Returns a vector whose elements are the maximum of each of the pairs of elements in two specified vectors.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The maximized vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector4 Max(Vector4 value1, Vector4 value2)
		{
			return new Vector4((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y, (value1.Z > value2.Z) ? value1.Z : value2.Z, (value1.W > value2.W) ? value1.W : value2.W);
		}

		/// <summary>Returns a vector whose elements are the absolute values of each of the specified vector's elements.</summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector4 Abs(Vector4 value)
		{
			return new Vector4(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z), MathF.Abs(value.W));
		}

		/// <summary>Returns a vector whose elements are the square root of each of a specified vector's elements.</summary>
		/// <param name="value">A vector.</param>
		/// <returns>The square root vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector4 SquareRoot(Vector4 value)
		{
			return new Vector4(MathF.Sqrt(value.X), MathF.Sqrt(value.Y), MathF.Sqrt(value.Z), MathF.Sqrt(value.W));
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector to add.</param>
		/// <param name="right">The second vector to add.</param>
		/// <returns>The summed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector4 operator +(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}

		/// <summary>Subtracts the second vector from the first.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector that results from subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector4 operator -(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}

		/// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The element-wise product vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector4 operator *(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
		}

		/// <summary>Multiples the specified vector by the specified scalar value.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector4 operator *(Vector4 left, float right)
		{
			return left * new Vector4(right);
		}

		/// <summary>Multiples the scalar value by the specified vector.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector4 operator *(float left, Vector4 right)
		{
			return new Vector4(left) * right;
		}

		/// <summary>Divides the first vector by the second.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector that results from dividing <paramref name="left" /> by <paramref name="right" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static Vector4 operator /(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);
		}

		/// <summary>Divides the specified vector by a specified scalar value.</summary>
		/// <param name="value1">The vector.</param>
		/// <param name="value2">The scalar value.</param>
		/// <returns>The result of the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator /(Vector4 value1, float value2)
		{
			return value1 / new Vector4(value2);
		}

		/// <summary>Negates the specified vector.</summary>
		/// <param name="value">The vector to negate.</param>
		/// <returns>The negated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 value)
		{
			return Zero - value;
		}

		/// <summary>Returns a value that indicates whether each pair of elements in two specified vectors is equal.</summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[System.Runtime.CompilerServices.Intrinsic]
		public static bool operator ==(Vector4 left, Vector4 right)
		{
			return left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether two specified vectors are not equal.</summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector4 left, Vector4 right)
		{
			return !(left == right);
		}
	}
	/// <summary>Represents an arbitrarily large signed integer.</summary>
	[Serializable]
	public readonly struct BigInteger : IFormattable, IComparable, IComparable<BigInteger>, IEquatable<BigInteger>
	{
		private enum GetBytesMode
		{
			AllocateArray,
			Count,
			Span
		}

		private const int knMaskHighBit = int.MinValue;

		private const uint kuMaskHighBit = 2147483648u;

		private const int kcbitUint = 32;

		private const int kcbitUlong = 64;

		private const int DecimalScaleFactorMask = 16711680;

		private const int DecimalSignMask = int.MinValue;

		internal readonly int _sign;

		internal readonly uint[] _bits;

		private static readonly BigInteger s_bnMinInt = new BigInteger(-1, new uint[1] { 2147483648u });

		private static readonly BigInteger s_bnOneInt = new BigInteger(1);

		private static readonly BigInteger s_bnZeroInt = new BigInteger(0);

		private static readonly BigInteger s_bnMinusOneInt = new BigInteger(-1);

		private static readonly byte[] s_success = Array.Empty<byte>();

		/// <summary>Gets a value that represents the number 0 (zero).</summary>
		/// <returns>An integer whose value is 0 (zero).</returns>
		public static BigInteger Zero => s_bnZeroInt;

		/// <summary>Gets a value that represents the number one (1).</summary>
		/// <returns>An object whose value is one (1).</returns>
		public static BigInteger One => s_bnOneInt;

		/// <summary>Gets a value that represents the number negative one (-1).</summary>
		/// <returns>An integer whose value is negative one (-1).</returns>
		public static BigInteger MinusOne => s_bnMinusOneInt;

		/// <summary>Indicates whether the value of the current <see cref="T:System.Numerics.BigInteger" /> object is a power of two.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of the <see cref="T:System.Numerics.BigInteger" /> object is a power of two; otherwise, <see langword="false" />.</returns>
		public bool IsPowerOfTwo
		{
			get
			{
				if (_bits == null)
				{
					if ((_sign & (_sign - 1)) == 0)
					{
						return _sign != 0;
					}
					return false;
				}
				if (_sign != 1)
				{
					return false;
				}
				int num = _bits.Length - 1;
				if ((_bits[num] & (_bits[num] - 1)) != 0)
				{
					return false;
				}
				while (--num >= 0)
				{
					if (_bits[num] != 0)
					{
						return false;
					}
				}
				return true;
			}
		}

		/// <summary>Indicates whether the value of the current <see cref="T:System.Numerics.BigInteger" /> object is <see cref="P:System.Numerics.BigInteger.Zero" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of the <see cref="T:System.Numerics.BigInteger" /> object is <see cref="P:System.Numerics.BigInteger.Zero" />; otherwise, <see langword="false" />.</returns>
		public bool IsZero => _sign == 0;

		/// <summary>Indicates whether the value of the current <see cref="T:System.Numerics.BigInteger" /> object is <see cref="P:System.Numerics.BigInteger.One" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of the <see cref="T:System.Numerics.BigInteger" /> object is <see cref="P:System.Numerics.BigInteger.One" />; otherwise, <see langword="false" />.</returns>
		public bool IsOne
		{
			get
			{
				if (_sign == 1)
				{
					return _bits == null;
				}
				return false;
			}
		}

		/// <summary>Indicates whether the value of the current <see cref="T:System.Numerics.BigInteger" /> object is an even number.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of the <see cref="T:System.Numerics.BigInteger" /> object is an even number; otherwise, <see langword="false" />.</returns>
		public bool IsEven
		{
			get
			{
				if (_bits != null)
				{
					return (_bits[0] & 1) == 0;
				}
				return (_sign & 1) == 0;
			}
		}

		/// <summary>Gets a number that indicates the sign (negative, positive, or zero) of the current <see cref="T:System.Numerics.BigInteger" /> object.</summary>
		/// <returns>A number that indicates the sign of the <see cref="T:System.Numerics.BigInteger" /> object, as shown in the following table.  
		///   Number  
		///
		///   Description  
		///
		///   -1  
		///
		///   The value of this object is negative.  
		///
		///   0  
		///
		///   The value of this object is 0 (zero).  
		///
		///   1  
		///
		///   The value of this object is positive.</returns>
		public int Sign => (_sign >> 31) - (-_sign >> 31);

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure using a 32-bit signed integer value.</summary>
		/// <param name="value">A 32-bit signed integer.</param>
		public BigInteger(int value)
		{
			if (value == int.MinValue)
			{
				this = s_bnMinInt;
				return;
			}
			_sign = value;
			_bits = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure using an unsigned 32-bit integer value.</summary>
		/// <param name="value">An unsigned 32-bit integer value.</param>
		[CLSCompliant(false)]
		public BigInteger(uint value)
		{
			if (value <= int.MaxValue)
			{
				_sign = (int)value;
				_bits = null;
			}
			else
			{
				_sign = 1;
				_bits = new uint[1];
				_bits[0] = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure using a 64-bit signed integer value.</summary>
		/// <param name="value">A 64-bit signed integer.</param>
		public BigInteger(long value)
		{
			if (int.MinValue < value && value <= int.MaxValue)
			{
				_sign = (int)value;
				_bits = null;
				return;
			}
			if (value == int.MinValue)
			{
				this = s_bnMinInt;
				return;
			}
			ulong num = 0uL;
			if (value < 0)
			{
				num = (ulong)(-value);
				_sign = -1;
			}
			else
			{
				num = (ulong)value;
				_sign = 1;
			}
			if (num <= uint.MaxValue)
			{
				_bits = new uint[1];
				_bits[0] = (uint)num;
			}
			else
			{
				_bits = new uint[2];
				_bits[0] = (uint)num;
				_bits[1] = (uint)(num >> 32);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure with an unsigned 64-bit integer value.</summary>
		/// <param name="value">An unsigned 64-bit integer.</param>
		[CLSCompliant(false)]
		public BigInteger(ulong value)
		{
			if (value <= int.MaxValue)
			{
				_sign = (int)value;
				_bits = null;
			}
			else if (value <= uint.MaxValue)
			{
				_sign = 1;
				_bits = new uint[1];
				_bits[0] = (uint)value;
			}
			else
			{
				_sign = 1;
				_bits = new uint[2];
				_bits[0] = (uint)value;
				_bits[1] = (uint)(value >> 32);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure using a single-precision floating-point value.</summary>
		/// <param name="value">A single-precision floating-point value.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.NegativeInfinity" />, or <see cref="F:System.Single.PositiveInfinity" />.</exception>
		public BigInteger(float value)
			: this((double)value)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure using a double-precision floating-point value.</summary>
		/// <param name="value">A double-precision floating-point value.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />.</exception>
		public BigInteger(double value)
		{
			if (!double.IsFinite(value))
			{
				if (double.IsInfinity(value))
				{
					throw new OverflowException("BigInteger cannot represent infinity.");
				}
				throw new OverflowException("The value is not a number.");
			}
			_sign = 0;
			_bits = null;
			NumericsHelpers.GetDoubleParts(value, out var sign, out var exp, out var man, out var _);
			if (man == 0L)
			{
				this = Zero;
				return;
			}
			if (exp <= 0)
			{
				if (exp <= -64)
				{
					this = Zero;
					return;
				}
				this = man >> -exp;
				if (sign < 0)
				{
					_sign = -_sign;
				}
				return;
			}
			if (exp <= 11)
			{
				this = man << exp;
				if (sign < 0)
				{
					_sign = -_sign;
				}
				return;
			}
			man <<= 11;
			exp -= 11;
			int num = (exp - 1) / 32 + 1;
			int num2 = num * 32 - exp;
			_bits = new uint[num + 2];
			_bits[num + 1] = (uint)(man >> num2 + 32);
			_bits[num] = (uint)(man >> num2);
			if (num2 > 0)
			{
				_bits[num - 1] = (uint)((int)man << 32 - num2);
			}
			_sign = sign;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure using a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A decimal number.</param>
		public BigInteger(decimal value)
		{
			int[] bits = decimal.GetBits(decimal.Truncate(value));
			int num = 3;
			while (num > 0 && bits[num - 1] == 0)
			{
				num--;
			}
			switch (num)
			{
			case 0:
				this = s_bnZeroInt;
				return;
			case 1:
				if (bits[0] > 0)
				{
					_sign = bits[0];
					_sign *= (((bits[3] & int.MinValue) == 0) ? 1 : (-1));
					_bits = null;
					return;
				}
				break;
			}
			_bits = new uint[num];
			_bits[0] = (uint)bits[0];
			if (num > 1)
			{
				_bits[1] = (uint)bits[1];
			}
			if (num > 2)
			{
				_bits[2] = (uint)bits[2];
			}
			_sign = (((bits[3] & int.MinValue) == 0) ? 1 : (-1));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.BigInteger" /> structure using the values in a byte array.</summary>
		/// <param name="value">An array of byte values in little-endian order.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		[CLSCompliant(false)]
		public BigInteger(byte[] value)
			: this(new ReadOnlySpan<byte>(value ?? throw new ArgumentNullException("value")))
		{
		}

		public BigInteger(ReadOnlySpan<byte> value, bool isUnsigned = false, bool isBigEndian = false)
		{
			int num = value.Length;
			bool flag;
			if (num > 0)
			{
				byte num2 = (isBigEndian ? value[0] : value[num - 1]);
				flag = (num2 & 0x80) != 0 && !isUnsigned;
				if (num2 == 0)
				{
					if (isBigEndian)
					{
						int i;
						for (i = 1; i < num && value[i] == 0; i++)
						{
						}
						value = value.Slice(i);
						num = value.Length;
					}
					else
					{
						num -= 2;
						while (num >= 0 && value[num] == 0)
						{
							num--;
						}
						num++;
					}
				}
			}
			else
			{
				flag = false;
			}
			if (num == 0)
			{
				_sign = 0;
				_bits = null;
				return;
			}
			if (num <= 4)
			{
				_sign = (flag ? (-1) : 0);
				if (isBigEndian)
				{
					for (int j = 0; j < num; j++)
					{
						_sign = (_sign << 8) | value[j];
					}
				}
				else
				{
					for (int num3 = num - 1; num3 >= 0; num3--)
					{
						_sign = (_sign << 8) | value[num3];
					}
				}
				_bits = null;
				if (_sign < 0 && !flag)
				{
					_bits = new uint[1] { (uint)_sign };
					_sign = 1;
				}
				if (_sign == int.MinValue)
				{
					this = s_bnMinInt;
				}
				return;
			}
			int num4 = num % 4;
			int num5 = num / 4 + ((num4 != 0) ? 1 : 0);
			uint[] array = new uint[num5];
			int num6 = num - 1;
			int k;
			if (isBigEndian)
			{
				int num7 = num - 4;
				for (k = 0; k < num5 - ((num4 != 0) ? 1 : 0); k++)
				{
					for (int l = 0; l < 4; l++)
					{
						byte b = value[num7];
						array[k] = (array[k] << 8) | b;
						num7++;
					}
					num7 -= 8;
				}
			}
			else
			{
				int num7 = 3;
				for (k = 0; k < num5 - ((num4 != 0) ? 1 : 0); k++)
				{
					for (int m = 0; m < 4; m++)
					{
						byte b2 = value[num7];
						array[k] = (array[k] << 8) | b2;
						num7--;
					}
					num7 += 8;
				}
			}
			if (num4 != 0)
			{
				if (flag)
				{
					array[num5 - 1] = uint.MaxValue;
				}
				if (isBigEndian)
				{
					for (int num7 = 0; num7 < num4; num7++)
					{
						byte b3 = value[num7];
						array[k] = (array[k] << 8) | b3;
					}
				}
				else
				{
					for (int num7 = num6; num7 >= num - num4; num7--)
					{
						byte b4 = value[num7];
						array[k] = (array[k] << 8) | b4;
					}
				}
			}
			if (flag)
			{
				NumericsHelpers.DangerousMakeTwosComplement(array);
				int num8 = array.Length - 1;
				while (num8 >= 0 && array[num8] == 0)
				{
					num8--;
				}
				num8++;
				if (num8 == 1)
				{
					switch (array[0])
					{
					case 1u:
						this = s_bnMinusOneInt;
						return;
					case 2147483648u:
						this = s_bnMinInt;
						return;
					}
					if ((int)array[0] > 0)
					{
						_sign = -1 * (int)array[0];
						_bits = null;
						return;
					}
				}
				if (num8 != array.Length)
				{
					_sign = -1;
					_bits = new uint[num8];
					Array.Copy(array, 0, _bits, 0, num8);
				}
				else
				{
					_sign = -1;
					_bits = array;
				}
			}
			else
			{
				_sign = 1;
				_bits = array;
			}
		}

		internal BigInteger(int n, uint[] rgu)
		{
			_sign = n;
			_bits = rgu;
		}

		internal BigInteger(uint[] value, bool negative)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int num = value.Length;
			while (num > 0 && value[num - 1] == 0)
			{
				num--;
			}
			switch (num)
			{
			case 0:
				this = s_bnZeroInt;
				break;
			case 1:
				if (value[0] < 2147483648u)
				{
					_sign = (int)(negative ? (0 - value[0]) : value[0]);
					_bits = null;
					if (_sign == int.MinValue)
					{
						this = s_bnMinInt;
					}
					break;
				}
				goto default;
			default:
				_sign = ((!negative) ? 1 : (-1));
				_bits = new uint[num];
				Array.Copy(value, 0, _bits, 0, num);
				break;
			}
		}

		private BigInteger(uint[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int num = value.Length;
			bool flag = num > 0 && (value[num - 1] & 0x80000000u) == 2147483648u;
			while (num > 0 && value[num - 1] == 0)
			{
				num--;
			}
			switch (num)
			{
			case 0:
				this = s_bnZeroInt;
				return;
			case 1:
				if ((int)value[0] < 0 && !flag)
				{
					_bits = new uint[1];
					_bits[0] = value[0];
					_sign = 1;
				}
				else if (int.MinValue == (int)value[0])
				{
					this = s_bnMinInt;
				}
				else
				{
					_sign = (int)value[0];
					_bits = null;
				}
				return;
			}
			if (!flag)
			{
				if (num != value.Length)
				{
					_sign = 1;
					_bits = new uint[num];
					Array.Copy(value, 0, _bits, 0, num);
				}
				else
				{
					_sign = 1;
					_bits = value;
				}
				return;
			}
			NumericsHelpers.DangerousMakeTwosComplement(value);
			int num2 = value.Length;
			while (num2 > 0 && value[num2 - 1] == 0)
			{
				num2--;
			}
			if (num2 == 1 && (int)value[0] > 0)
			{
				if (value[0] == 1)
				{
					this = s_bnMinusOneInt;
					return;
				}
				if (value[0] == 2147483648u)
				{
					this = s_bnMinInt;
					return;
				}
				_sign = -1 * (int)value[0];
				_bits = null;
			}
			else if (num2 != value.Length)
			{
				_sign = -1;
				_bits = new uint[num2];
				Array.Copy(value, 0, _bits, 0, num2);
			}
			else
			{
				_sign = -1;
				_bits = value;
			}
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Numerics.BigInteger" /> equivalent.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <returns>A value that is equivalent to the number specified in the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in the correct format.</exception>
		public static BigInteger Parse(string value)
		{
			return Parse(value, NumberStyles.Integer);
		}

		/// <summary>Converts the string representation of a number in a specified style to its <see cref="T:System.Numerics.BigInteger" /> equivalent.</summary>
		/// <param name="value">A string that contains a number to convert.</param>
		/// <param name="style">A bitwise combination of the enumeration values that specify the permitted format of <paramref name="value" />.</param>
		/// <returns>A value that is equivalent to the number specified in the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> includes the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> or <see cref="F:System.Globalization.NumberStyles.HexNumber" /> flag along with another value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not comply with the input pattern specified by <see cref="T:System.Globalization.NumberStyles" />.</exception>
		public static BigInteger Parse(string value, NumberStyles style)
		{
			return Parse(value, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its <see cref="T:System.Numerics.BigInteger" /> equivalent.</summary>
		/// <param name="value">A string that contains a number to convert.</param>
		/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="value" />.</param>
		/// <returns>A value that is equivalent to the number specified in the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in the correct format.</exception>
		public static BigInteger Parse(string value, IFormatProvider provider)
		{
			return Parse(value, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="T:System.Numerics.BigInteger" /> equivalent.</summary>
		/// <param name="value">A string that contains a number to convert.</param>
		/// <param name="style">A bitwise combination of the enumeration values that specify the permitted format of <paramref name="value" />.</param>
		/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="value" />.</param>
		/// <returns>A value that is equivalent to the number specified in the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> includes the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> or <see cref="F:System.Globalization.NumberStyles.HexNumber" /> flag along with another value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not comply with the input pattern specified by <paramref name="style" />.</exception>
		public static BigInteger Parse(string value, NumberStyles style, IFormatProvider provider)
		{
			return BigNumber.ParseBigInteger(value, style, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Tries to convert the string representation of a number to its <see cref="T:System.Numerics.BigInteger" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="value">The string representation of a number.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.Numerics.BigInteger" /> equivalent to the number that is contained in <paramref name="value" />, or zero (0) if the conversion fails. The conversion fails if the <paramref name="value" /> parameter is <see langword="null" /> or is not of the correct format. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		public static bool TryParse(string value, out BigInteger result)
		{
			return TryParse(value, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="T:System.Numerics.BigInteger" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="value">The string representation of a number. The string is interpreted using the style specified by <paramref name="style" />.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="value" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="value" />.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.Numerics.BigInteger" /> equivalent to the number that is contained in <paramref name="value" />, or <see cref="P:System.Numerics.BigInteger.Zero" /> if the conversion failed. The conversion fails if the <paramref name="value" /> parameter is <see langword="null" /> or is not in a format that is compliant with <paramref name="style" />. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> includes the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> or <see cref="F:System.Globalization.NumberStyles.HexNumber" /> flag along with another value.</exception>
		public static bool TryParse(string value, NumberStyles style, IFormatProvider provider, out BigInteger result)
		{
			return BigNumber.TryParseBigInteger(value, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		public static BigInteger Parse(ReadOnlySpan<char> value, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
		{
			return BigNumber.ParseBigInteger(value, style, NumberFormatInfo.GetInstance(provider));
		}

		public static bool TryParse(ReadOnlySpan<char> value, out BigInteger result)
		{
			return BigNumber.TryParseBigInteger(value, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		public static bool TryParse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider provider, out BigInteger result)
		{
			return BigNumber.TryParseBigInteger(value, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		/// <summary>Compares two <see cref="T:System.Numerics.BigInteger" /> values and returns an integer that indicates whether the first value is less than, equal to, or greater than the second value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>A signed integer that indicates the relative values of <paramref name="left" /> and <paramref name="right" />, as shown in the following table.  
		///   Value  
		///
		///   Condition  
		///
		///   Less than zero  
		///
		///  <paramref name="left" /> is less than <paramref name="right" />.  
		///
		///   Zero  
		///
		///  <paramref name="left" /> equals <paramref name="right" />.  
		///
		///   Greater than zero  
		///
		///  <paramref name="left" /> is greater than <paramref name="right" />.</returns>
		public static int Compare(BigInteger left, BigInteger right)
		{
			return left.CompareTo(right);
		}

		/// <summary>Gets the absolute value of a <see cref="T:System.Numerics.BigInteger" /> object.</summary>
		/// <param name="value">A number.</param>
		/// <returns>The absolute value of <paramref name="value" />.</returns>
		public static BigInteger Abs(BigInteger value)
		{
			if (!(value >= Zero))
			{
				return -value;
			}
			return value;
		}

		/// <summary>Adds two <see cref="T:System.Numerics.BigInteger" /> values and returns the result.</summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of <paramref name="left" /> and <paramref name="right" />.</returns>
		public static BigInteger Add(BigInteger left, BigInteger right)
		{
			return left + right;
		}

		/// <summary>Subtracts one <see cref="T:System.Numerics.BigInteger" /> value from another and returns the result.</summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
		public static BigInteger Subtract(BigInteger left, BigInteger right)
		{
			return left - right;
		}

		/// <summary>Returns the product of two <see cref="T:System.Numerics.BigInteger" /> values.</summary>
		/// <param name="left">The first number to multiply.</param>
		/// <param name="right">The second number to multiply.</param>
		/// <returns>The product of the <paramref name="left" /> and <paramref name="right" /> parameters.</returns>
		public static BigInteger Multiply(BigInteger left, BigInteger right)
		{
			return left * right;
		}

		/// <summary>Divides one <see cref="T:System.Numerics.BigInteger" /> value by another and returns the result.</summary>
		/// <param name="dividend">The value to be divided.</param>
		/// <param name="divisor">The value to divide by.</param>
		/// <returns>The quotient of the division.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="divisor" /> is 0 (zero).</exception>
		public static BigInteger Divide(BigInteger dividend, BigInteger divisor)
		{
			return dividend / divisor;
		}

		/// <summary>Performs integer division on two <see cref="T:System.Numerics.BigInteger" /> values and returns the remainder.</summary>
		/// <param name="dividend">The value to be divided.</param>
		/// <param name="divisor">The value to divide by.</param>
		/// <returns>The remainder after dividing <paramref name="dividend" /> by <paramref name="divisor" />.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="divisor" /> is 0 (zero).</exception>
		public static BigInteger Remainder(BigInteger dividend, BigInteger divisor)
		{
			return dividend % divisor;
		}

		/// <summary>Divides one <see cref="T:System.Numerics.BigInteger" /> value by another, returns the result, and returns the remainder in an output parameter.</summary>
		/// <param name="dividend">The value to be divided.</param>
		/// <param name="divisor">The value to divide by.</param>
		/// <param name="remainder">When this method returns, contains a <see cref="T:System.Numerics.BigInteger" /> value that represents the remainder from the division. This parameter is passed uninitialized.</param>
		/// <returns>The quotient of the division.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="divisor" /> is 0 (zero).</exception>
		public static BigInteger DivRem(BigInteger dividend, BigInteger divisor, out BigInteger remainder)
		{
			bool flag = dividend._bits == null;
			bool flag2 = divisor._bits == null;
			if (flag && flag2)
			{
				remainder = dividend._sign % divisor._sign;
				return dividend._sign / divisor._sign;
			}
			if (flag)
			{
				remainder = dividend;
				return s_bnZeroInt;
			}
			if (flag2)
			{
				uint remainder2;
				uint[] value = BigIntegerCalculator.Divide(dividend._bits, NumericsHelpers.Abs(divisor._sign), out remainder2);
				remainder = ((dividend._sign < 0) ? (-1 * remainder2) : remainder2);
				return new BigInteger(value, (dividend._sign < 0) ^ (divisor._sign < 0));
			}
			if (dividend._bits.Length < divisor._bits.Length)
			{
				remainder = dividend;
				return s_bnZeroInt;
			}
			uint[] remainder3;
			uint[] value2 = BigIntegerCalculator.Divide(dividend._bits, divisor._bits, out remainder3);
			remainder = new BigInteger(remainder3, dividend._sign < 0);
			return new BigInteger(value2, (dividend._sign < 0) ^ (divisor._sign < 0));
		}

		/// <summary>Negates a specified <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="value">The value to negate.</param>
		/// <returns>The result of the <paramref name="value" /> parameter multiplied by negative one (-1).</returns>
		public static BigInteger Negate(BigInteger value)
		{
			return -value;
		}

		/// <summary>Returns the natural (base <see langword="e" />) logarithm of a specified number.</summary>
		/// <param name="value">The number whose logarithm is to be found.</param>
		/// <returns>The natural (base <see langword="e" />) logarithm of <paramref name="value" />, as shown in the table in the Remarks section.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The natural log of <paramref name="value" /> is out of range of the <see cref="T:System.Double" /> data type.</exception>
		public static double Log(BigInteger value)
		{
			return Log(value, Math.E);
		}

		/// <summary>Returns the logarithm of a specified number in a specified base.</summary>
		/// <param name="value">A number whose logarithm is to be found.</param>
		/// <param name="baseValue">The base of the logarithm.</param>
		/// <returns>The base <paramref name="baseValue" /> logarithm of <paramref name="value" />, as shown in the table in the Remarks section.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The log of <paramref name="value" /> is out of range of the <see cref="T:System.Double" /> data type.</exception>
		public static double Log(BigInteger value, double baseValue)
		{
			if (value._sign < 0 || baseValue == 1.0)
			{
				return double.NaN;
			}
			if (baseValue == double.PositiveInfinity)
			{
				if (!value.IsOne)
				{
					return double.NaN;
				}
				return 0.0;
			}
			if (baseValue == 0.0 && !value.IsOne)
			{
				return double.NaN;
			}
			if (value._bits == null)
			{
				return Math.Log(value._sign, baseValue);
			}
			long num = value._bits[value._bits.Length - 1];
			ulong num2 = ((value._bits.Length > 1) ? value._bits[value._bits.Length - 2] : 0u);
			ulong num3 = ((value._bits.Length > 2) ? value._bits[value._bits.Length - 3] : 0u);
			int num4 = NumericsHelpers.CbitHighZero((uint)num);
			long num5 = (long)value._bits.Length * 32L - num4;
			return Math.Log((ulong)(num << 32 + num4) | (num2 << num4) | (num3 >> 32 - num4), baseValue) + (double)(num5 - 64) / Math.Log(baseValue, 2.0);
		}

		/// <summary>Returns the base 10 logarithm of a specified number.</summary>
		/// <param name="value">A number whose logarithm is to be found.</param>
		/// <returns>The base 10 logarithm of <paramref name="value" />, as shown in the table in the Remarks section.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The base 10 log of <paramref name="value" /> is out of range of the <see cref="T:System.Double" /> data type.</exception>
		public static double Log10(BigInteger value)
		{
			return Log(value, 10.0);
		}

		/// <summary>Finds the greatest common divisor of two <see cref="T:System.Numerics.BigInteger" /> values.</summary>
		/// <param name="left">The first value.</param>
		/// <param name="right">The second value.</param>
		/// <returns>The greatest common divisor of <paramref name="left" /> and <paramref name="right" />.</returns>
		public static BigInteger GreatestCommonDivisor(BigInteger left, BigInteger right)
		{
			bool flag = left._bits == null;
			bool flag2 = right._bits == null;
			if (flag && flag2)
			{
				return BigIntegerCalculator.Gcd(NumericsHelpers.Abs(left._sign), NumericsHelpers.Abs(right._sign));
			}
			if (flag)
			{
				if (left._sign == 0)
				{
					return new BigInteger(right._bits, negative: false);
				}
				return BigIntegerCalculator.Gcd(right._bits, NumericsHelpers.Abs(left._sign));
			}
			if (flag2)
			{
				if (right._sign == 0)
				{
					return new BigInteger(left._bits, negative: false);
				}
				return BigIntegerCalculator.Gcd(left._bits, NumericsHelpers.Abs(right._sign));
			}
			if (BigIntegerCalculator.Compare(left._bits, right._bits) < 0)
			{
				return GreatestCommonDivisor(right._bits, left._bits);
			}
			return GreatestCommonDivisor(left._bits, right._bits);
		}

		private static BigInteger GreatestCommonDivisor(uint[] leftBits, uint[] rightBits)
		{
			if (rightBits.Length == 1)
			{
				uint right = BigIntegerCalculator.Remainder(leftBits, rightBits[0]);
				return BigIntegerCalculator.Gcd(rightBits[0], right);
			}
			if (rightBits.Length == 2)
			{
				uint[] array = BigIntegerCalculator.Remainder(leftBits, rightBits);
				ulong left = ((ulong)rightBits[1] << 32) | rightBits[0];
				ulong right2 = ((ulong)array[1] << 32) | array[0];
				return BigIntegerCalculator.Gcd(left, right2);
			}
			return new BigInteger(BigIntegerCalculator.Gcd(leftBits, rightBits), negative: false);
		}

		/// <summary>Returns the larger of two <see cref="T:System.Numerics.BigInteger" /> values.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>The <paramref name="left" /> or <paramref name="right" /> parameter, whichever is larger.</returns>
		public static BigInteger Max(BigInteger left, BigInteger right)
		{
			if (left.CompareTo(right) < 0)
			{
				return right;
			}
			return left;
		}

		/// <summary>Returns the smaller of two <see cref="T:System.Numerics.BigInteger" /> values.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>The <paramref name="left" /> or <paramref name="right" /> parameter, whichever is smaller.</returns>
		public static BigInteger Min(BigInteger left, BigInteger right)
		{
			if (left.CompareTo(right) <= 0)
			{
				return left;
			}
			return right;
		}

		/// <summary>Performs modulus division on a number raised to the power of another number.</summary>
		/// <param name="value">The number to raise to the <paramref name="exponent" /> power.</param>
		/// <param name="exponent">The exponent to raise <paramref name="value" /> by.</param>
		/// <param name="modulus">The number by which to divide <paramref name="value" /> raised to the <paramref name="exponent" /> power.</param>
		/// <returns>The remainder after dividing <paramref name="value" />exponent by <paramref name="modulus" />.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="modulus" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="exponent" /> is negative.</exception>
		public static BigInteger ModPow(BigInteger value, BigInteger exponent, BigInteger modulus)
		{
			if (exponent.Sign < 0)
			{
				throw new ArgumentOutOfRangeException("exponent", "The number must be greater than or equal to zero.");
			}
			bool flag = value._bits == null;
			bool flag2 = exponent._bits == null;
			if (modulus._bits == null)
			{
				uint num = ((flag && flag2) ? BigIntegerCalculator.Pow(NumericsHelpers.Abs(value._sign), NumericsHelpers.Abs(exponent._sign), NumericsHelpers.Abs(modulus._sign)) : (flag ? BigIntegerCalculator.Pow(NumericsHelpers.Abs(value._sign), exponent._bits, NumericsHelpers.Abs(modulus._sign)) : (flag2 ? BigIntegerCalculator.Pow(value._bits, NumericsHelpers.Abs(exponent._sign), NumericsHelpers.Abs(modulus._sign)) : BigIntegerCalculator.Pow(value._bits, exponent._bits, NumericsHelpers.Abs(modulus._sign)))));
				return (value._sign < 0 && !exponent.IsEven) ? (-1 * num) : num;
			}
			return new BigInteger((flag && flag2) ? BigIntegerCalculator.Pow(NumericsHelpers.Abs(value._sign), NumericsHelpers.Abs(exponent._sign), modulus._bits) : (flag ? BigIntegerCalculator.Pow(NumericsHelpers.Abs(value._sign), exponent._bits, modulus._bits) : (flag2 ? BigIntegerCalculator.Pow(value._bits, NumericsHelpers.Abs(exponent._sign), modulus._bits) : BigIntegerCalculator.Pow(value._bits, exponent._bits, modulus._bits))), value._sign < 0 && !exponent.IsEven);
		}

		/// <summary>Raises a <see cref="T:System.Numerics.BigInteger" /> value to the power of a specified value.</summary>
		/// <param name="value">The number to raise to the <paramref name="exponent" /> power.</param>
		/// <param name="exponent">The exponent to raise <paramref name="value" /> by.</param>
		/// <returns>The result of raising <paramref name="value" /> to the <paramref name="exponent" /> power.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="exponent" /> is negative.</exception>
		public static BigInteger Pow(BigInteger value, int exponent)
		{
			if (exponent < 0)
			{
				throw new ArgumentOutOfRangeException("exponent", "The number must be greater than or equal to zero.");
			}
			switch (exponent)
			{
			case 0:
				return s_bnOneInt;
			case 1:
				return value;
			default:
			{
				bool flag = value._bits == null;
				if (flag)
				{
					if (value._sign == 1)
					{
						return value;
					}
					if (value._sign == -1)
					{
						if ((exponent & 1) == 0)
						{
							return s_bnOneInt;
						}
						return value;
					}
					if (value._sign == 0)
					{
						return value;
					}
				}
				return new BigInteger(flag ? BigIntegerCalculator.Pow(NumericsHelpers.Abs(value._sign), NumericsHelpers.Abs(exponent)) : BigIntegerCalculator.Pow(value._bits, NumericsHelpers.Abs(exponent)), value._sign < 0 && (exponent & 1) != 0);
			}
			}
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Numerics.BigInteger" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			if (_bits == null)
			{
				return _sign;
			}
			int num = _sign;
			int num2 = _bits.Length;
			while (--num2 >= 0)
			{
				num = NumericsHelpers.CombineHash(num, (int)_bits[num2]);
			}
			return num;
		}

		/// <summary>Returns a value that indicates whether the current instance and a specified object have the same value.</summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="obj" /> argument is a <see cref="T:System.Numerics.BigInteger" /> object, and its value is equal to the value of the current <see cref="T:System.Numerics.BigInteger" /> instance; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is BigInteger))
			{
				return false;
			}
			return Equals((BigInteger)obj);
		}

		/// <summary>Returns a value that indicates whether the current instance and a signed 64-bit integer have the same value.</summary>
		/// <param name="other">The signed 64-bit integer value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the signed 64-bit integer and the current instance have the same value; otherwise, <see langword="false" />.</returns>
		public bool Equals(long other)
		{
			if (_bits == null)
			{
				return _sign == other;
			}
			int num;
			if ((_sign ^ other) < 0 || (num = _bits.Length) > 2)
			{
				return false;
			}
			ulong num2 = (ulong)((other < 0) ? (-other) : other);
			if (num == 1)
			{
				return _bits[0] == num2;
			}
			return NumericsHelpers.MakeUlong(_bits[1], _bits[0]) == num2;
		}

		/// <summary>Returns a value that indicates whether the current instance and an unsigned 64-bit integer have the same value.</summary>
		/// <param name="other">The unsigned 64-bit integer to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance and the unsigned 64-bit integer have the same value; otherwise, <see langword="false" />.</returns>
		[CLSCompliant(false)]
		public bool Equals(ulong other)
		{
			if (_sign < 0)
			{
				return false;
			}
			if (_bits == null)
			{
				return (ulong)_sign == other;
			}
			int num = _bits.Length;
			if (num > 2)
			{
				return false;
			}
			if (num == 1)
			{
				return _bits[0] == other;
			}
			return NumericsHelpers.MakeUlong(_bits[1], _bits[0]) == other;
		}

		/// <summary>Returns a value that indicates whether the current instance and a specified <see cref="T:System.Numerics.BigInteger" /> object have the same value.</summary>
		/// <param name="other">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Numerics.BigInteger" /> object and <paramref name="other" /> have the same value; otherwise, <see langword="false" />.</returns>
		public bool Equals(BigInteger other)
		{
			if (_sign != other._sign)
			{
				return false;
			}
			if (_bits == other._bits)
			{
				return true;
			}
			if (_bits == null || other._bits == null)
			{
				return false;
			}
			int num = _bits.Length;
			if (num != other._bits.Length)
			{
				return false;
			}
			return GetDiffLength(_bits, other._bits, num) == 0;
		}

		/// <summary>Compares this instance to a signed 64-bit integer and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the signed 64-bit integer.</summary>
		/// <param name="other">The signed 64-bit integer to compare.</param>
		/// <returns>A signed integer value that indicates the relationship of this instance to <paramref name="other" />, as shown in the following table.  
		///   Return value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   The current instance is less than <paramref name="other" />.  
		///
		///   Zero  
		///
		///   The current instance equals <paramref name="other" />.  
		///
		///   Greater than zero  
		///
		///   The current instance is greater than <paramref name="other" />.</returns>
		public int CompareTo(long other)
		{
			if (_bits == null)
			{
				return ((long)_sign).CompareTo(other);
			}
			int num;
			if ((_sign ^ other) < 0 || (num = _bits.Length) > 2)
			{
				return _sign;
			}
			ulong value = (ulong)((other < 0) ? (-other) : other);
			ulong num2 = ((num == 2) ? NumericsHelpers.MakeUlong(_bits[1], _bits[0]) : _bits[0]);
			return _sign * num2.CompareTo(value);
		}

		/// <summary>Compares this instance to an unsigned 64-bit integer and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the unsigned 64-bit integer.</summary>
		/// <param name="other">The unsigned 64-bit integer to compare.</param>
		/// <returns>A signed integer that indicates the relative value of this instance and <paramref name="other" />, as shown in the following table.  
		///   Return value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   The current instance is less than <paramref name="other" />.  
		///
		///   Zero  
		///
		///   The current instance equals <paramref name="other" />.  
		///
		///   Greater than zero  
		///
		///   The current instance is greater than <paramref name="other" />.</returns>
		[CLSCompliant(false)]
		public int CompareTo(ulong other)
		{
			if (_sign < 0)
			{
				return -1;
			}
			if (_bits == null)
			{
				return ((ulong)_sign).CompareTo(other);
			}
			int num = _bits.Length;
			if (num > 2)
			{
				return 1;
			}
			return ((num == 2) ? NumericsHelpers.MakeUlong(_bits[1], _bits[0]) : _bits[0]).CompareTo(other);
		}

		/// <summary>Compares this instance to a second <see cref="T:System.Numerics.BigInteger" /> and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified object.</summary>
		/// <param name="other">The object to compare.</param>
		/// <returns>A signed integer value that indicates the relationship of this instance to <paramref name="other" />, as shown in the following table.  
		///   Return value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   The current instance is less than <paramref name="other" />.  
		///
		///   Zero  
		///
		///   The current instance equals <paramref name="other" />.  
		///
		///   Greater than zero  
		///
		///   The current instance is greater than <paramref name="other" />.</returns>
		public int CompareTo(BigInteger other)
		{
			if ((_sign ^ other._sign) < 0)
			{
				if (_sign >= 0)
				{
					return 1;
				}
				return -1;
			}
			if (_bits == null)
			{
				if (other._bits == null)
				{
					if (_sign >= other._sign)
					{
						if (_sign <= other._sign)
						{
							return 0;
						}
						return 1;
					}
					return -1;
				}
				return -other._sign;
			}
			int num;
			int num2;
			if (other._bits == null || (num = _bits.Length) > (num2 = other._bits.Length))
			{
				return _sign;
			}
			if (num < num2)
			{
				return -_sign;
			}
			int diffLength = GetDiffLength(_bits, other._bits, num);
			if (diffLength == 0)
			{
				return 0;
			}
			if (_bits[diffLength - 1] >= other._bits[diffLength - 1])
			{
				return _sign;
			}
			return -_sign;
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified object.</summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>A signed integer that indicates the relationship of the current instance to the <paramref name="obj" /> parameter, as shown in the following table.  
		///   Return value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   The current instance is less than <paramref name="obj" />.  
		///
		///   Zero  
		///
		///   The current instance equals <paramref name="obj" />.  
		///
		///   Greater than zero  
		///
		///   The current instance is greater than <paramref name="obj" />, or the <paramref name="obj" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Numerics.BigInteger" />.</exception>
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is BigInteger))
			{
				throw new ArgumentException("The parameter must be a BigInteger.", "obj");
			}
			return CompareTo((BigInteger)obj);
		}

		/// <summary>Converts a <see cref="T:System.Numerics.BigInteger" /> value to a byte array.</summary>
		/// <returns>The value of the current <see cref="T:System.Numerics.BigInteger" /> object converted to an array of bytes.</returns>
		public byte[] ToByteArray()
		{
			return ToByteArray(isUnsigned: false, isBigEndian: false);
		}

		public byte[] ToByteArray(bool isUnsigned = false, bool isBigEndian = false)
		{
			int bytesWritten = 0;
			return TryGetBytes(GetBytesMode.AllocateArray, default(Span<byte>), isUnsigned, isBigEndian, ref bytesWritten);
		}

		public bool TryWriteBytes(Span<byte> destination, out int bytesWritten, bool isUnsigned = false, bool isBigEndian = false)
		{
			bytesWritten = 0;
			if (TryGetBytes(GetBytesMode.Span, destination, isUnsigned, isBigEndian, ref bytesWritten) == null)
			{
				bytesWritten = 0;
				return false;
			}
			return true;
		}

		internal bool TryWriteOrCountBytes(Span<byte> destination, out int bytesWritten, bool isUnsigned = false, bool isBigEndian = false)
		{
			bytesWritten = 0;
			return TryGetBytes(GetBytesMode.Span, destination, isUnsigned, isBigEndian, ref bytesWritten) != null;
		}

		public int GetByteCount(bool isUnsigned = false)
		{
			int bytesWritten = 0;
			TryGetBytes(GetBytesMode.Count, default(Span<byte>), isUnsigned, isBigEndian: false, ref bytesWritten);
			return bytesWritten;
		}

		private byte[] TryGetBytes(GetBytesMode mode, Span<byte> destination, bool isUnsigned, bool isBigEndian, ref int bytesWritten)
		{
			int sign = _sign;
			if (sign == 0)
			{
				switch (mode)
				{
				case GetBytesMode.AllocateArray:
					return new byte[1];
				case GetBytesMode.Count:
					bytesWritten = 1;
					return null;
				default:
					bytesWritten = 1;
					if (destination.Length != 0)
					{
						destination[0] = 0;
						return s_success;
					}
					return null;
				}
			}
			if (isUnsigned && sign < 0)
			{
				throw new OverflowException("Negative values do not have an unsigned representation.");
			}
			int i = 0;
			uint[] bits = _bits;
			byte b;
			uint num;
			if (bits == null)
			{
				b = (byte)((sign < 0) ? 255u : 0u);
				num = (uint)sign;
			}
			else if (sign == -1)
			{
				b = byte.MaxValue;
				for (; bits[i] == 0; i++)
				{
				}
				num = ~bits[^1];
				if (bits.Length - 1 == i)
				{
					num++;
				}
			}
			else
			{
				b = 0;
				num = bits[^1];
			}
			byte b2;
			int num2;
			if ((b2 = (byte)(num >> 24)) != b)
			{
				num2 = 3;
			}
			else if ((b2 = (byte)(num >> 16)) != b)
			{
				num2 = 2;
			}
			else if ((b2 = (byte)(num >> 8)) != b)
			{
				num2 = 1;
			}
			else
			{
				b2 = (byte)num;
				num2 = 0;
			}
			bool flag = (b2 & 0x80) != (b & 0x80) && !isUnsigned;
			int num3 = num2 + 1 + (flag ? 1 : 0);
			if (bits != null)
			{
				num3 = checked(4 * (bits.Length - 1) + num3);
			}
			byte[] result;
			switch (mode)
			{
			case GetBytesMode.AllocateArray:
				destination = (result = new byte[num3]);
				break;
			case GetBytesMode.Count:
				bytesWritten = num3;
				return null;
			default:
				bytesWritten = num3;
				if (destination.Length < num3)
				{
					return null;
				}
				result = s_success;
				break;
			}
			int num4 = (isBigEndian ? (num3 - 1) : 0);
			int num5 = ((!isBigEndian) ? 1 : (-1));
			if (bits != null)
			{
				for (int j = 0; j < bits.Length - 1; j++)
				{
					uint num6 = bits[j];
					if (sign == -1)
					{
						num6 = ~num6;
						if (j <= i)
						{
							num6++;
						}
					}
					destination[num4] = (byte)num6;
					num4 += num5;
					destination[num4] = (byte)(num6 >> 8);
					num4 += num5;
					destination[num4] = (byte)(num6 >> 16);
					num4 += num5;
					destination[num4] = (byte)(num6 >> 24);
					num4 += num5;
				}
			}
			destination[num4] = (byte)num;
			if (num2 != 0)
			{
				num4 += num5;
				destination[num4] = (byte)(num >> 8);
				if (num2 != 1)
				{
					num4 += num5;
					destination[num4] = (byte)(num >> 16);
					if (num2 != 2)
					{
						num4 += num5;
						destination[num4] = (byte)(num >> 24);
					}
				}
			}
			if (flag)
			{
				num4 += num5;
				destination[num4] = b;
			}
			return result;
		}

		private uint[] ToUInt32Array()
		{
			if (_bits == null && _sign == 0)
			{
				return new uint[1];
			}
			uint[] array;
			uint num;
			if (_bits == null)
			{
				array = new uint[1] { (uint)_sign };
				num = ((_sign < 0) ? uint.MaxValue : 0u);
			}
			else if (_sign == -1)
			{
				array = (uint[])_bits.Clone();
				NumericsHelpers.DangerousMakeTwosComplement(array);
				num = uint.MaxValue;
			}
			else
			{
				array = _bits;
				num = 0u;
			}
			int num2 = array.Length - 1;
			while (num2 > 0 && array[num2] == num)
			{
				num2--;
			}
			bool flag = (array[num2] & 0x80000000u) != (num & 0x80000000u);
			uint[] array2 = new uint[num2 + 1 + (flag ? 1 : 0)];
			Array.Copy(array, 0, array2, 0, num2 + 1);
			if (flag)
			{
				array2[^1] = num;
			}
			return array2;
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.Numerics.BigInteger" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the current <see cref="T:System.Numerics.BigInteger" /> value.</returns>
		public override string ToString()
		{
			return BigNumber.FormatBigInteger(this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.Numerics.BigInteger" /> object to its equivalent string representation by using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current <see cref="T:System.Numerics.BigInteger" /> value in the format specified by the <paramref name="provider" /> parameter.</returns>
		public string ToString(IFormatProvider provider)
		{
			return BigNumber.FormatBigInteger(this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.Numerics.BigInteger" /> object to its equivalent string representation by using the specified format.</summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current <see cref="T:System.Numerics.BigInteger" /> value in the format specified by the <paramref name="format" /> parameter.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid format string.</exception>
		public string ToString(string format)
		{
			return BigNumber.FormatBigInteger(this, format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.Numerics.BigInteger" /> object to its equivalent string representation by using the specified format and culture-specific format information.</summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current <see cref="T:System.Numerics.BigInteger" /> value as specified by the <paramref name="format" /> and <paramref name="provider" /> parameters.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid format string.</exception>
		public string ToString(string format, IFormatProvider provider)
		{
			return BigNumber.FormatBigInteger(this, format, NumberFormatInfo.GetInstance(provider));
		}

		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return BigNumber.TryFormatBigInteger(this, format, NumberFormatInfo.GetInstance(provider), destination, out charsWritten);
		}

		private static BigInteger Add(uint[] leftBits, int leftSign, uint[] rightBits, int rightSign)
		{
			bool flag = leftBits == null;
			bool flag2 = rightBits == null;
			if (flag && flag2)
			{
				return (long)leftSign + (long)rightSign;
			}
			if (flag)
			{
				return new BigInteger(BigIntegerCalculator.Add(rightBits, NumericsHelpers.Abs(leftSign)), leftSign < 0);
			}
			if (flag2)
			{
				return new BigInteger(BigIntegerCalculator.Add(leftBits, NumericsHelpers.Abs(rightSign)), leftSign < 0);
			}
			if (leftBits.Length < rightBits.Length)
			{
				return new BigInteger(BigIntegerCalculator.Add(rightBits, leftBits), leftSign < 0);
			}
			return new BigInteger(BigIntegerCalculator.Add(leftBits, rightBits), leftSign < 0);
		}

		/// <summary>Subtracts a <see cref="T:System.Numerics.BigInteger" /> value from another <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
		public static BigInteger operator -(BigInteger left, BigInteger right)
		{
			if (left._sign < 0 != right._sign < 0)
			{
				return Add(left._bits, left._sign, right._bits, -1 * right._sign);
			}
			return Subtract(left._bits, left._sign, right._bits, right._sign);
		}

		private static BigInteger Subtract(uint[] leftBits, int leftSign, uint[] rightBits, int rightSign)
		{
			bool flag = leftBits == null;
			bool flag2 = rightBits == null;
			if (flag && flag2)
			{
				return (long)leftSign - (long)rightSign;
			}
			if (flag)
			{
				return new BigInteger(BigIntegerCalculator.Subtract(rightBits, NumericsHelpers.Abs(leftSign)), leftSign >= 0);
			}
			if (flag2)
			{
				return new BigInteger(BigIntegerCalculator.Subtract(leftBits, NumericsHelpers.Abs(rightSign)), leftSign < 0);
			}
			if (BigIntegerCalculator.Compare(leftBits, rightBits) < 0)
			{
				return new BigInteger(BigIntegerCalculator.Subtract(rightBits, leftBits), leftSign >= 0);
			}
			return new BigInteger(BigIntegerCalculator.Subtract(leftBits, rightBits), leftSign < 0);
		}

		/// <summary>Defines an implicit conversion of an unsigned byte to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		public static implicit operator BigInteger(byte value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an implicit conversion of an 8-bit signed integer to a <see cref="T:System.Numerics.BigInteger" /> value.  
		///  This API is not CLS-compliant. The compliant alternative is <see cref="M:System.Numerics.BigInteger.#ctor(System.Int32)" />.</summary>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		[CLSCompliant(false)]
		public static implicit operator BigInteger(sbyte value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an implicit conversion of a signed 16-bit integer to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		public static implicit operator BigInteger(short value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an implicit conversion of a 16-bit unsigned integer to a <see cref="T:System.Numerics.BigInteger" /> value.  
		///  This API is not CLS-compliant. The compliant alternative is <see cref="M:System.Numerics.BigInteger.op_Implicit(System.Int32)~System.Numerics.BigInteger" />.</summary>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		[CLSCompliant(false)]
		public static implicit operator BigInteger(ushort value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an implicit conversion of a signed 32-bit integer to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		public static implicit operator BigInteger(int value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an implicit conversion of a 32-bit unsigned integer to a <see cref="T:System.Numerics.BigInteger" /> value.  
		///  This API is not CLS-compliant. The compliant alternative is <see cref="M:System.Numerics.BigInteger.op_Implicit(System.Int64)~System.Numerics.BigInteger" />.</summary>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		[CLSCompliant(false)]
		public static implicit operator BigInteger(uint value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an implicit conversion of a signed 64-bit integer to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		public static implicit operator BigInteger(long value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an implicit conversion of a 64-bit unsigned integer to a <see cref="T:System.Numerics.BigInteger" /> value.  
		///  This API is not CLS-compliant. The compliant alternative is <see cref="T:System.Double" />.</summary>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		[CLSCompliant(false)]
		public static implicit operator BigInteger(ulong value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Single" /> value to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />.</exception>
		public static explicit operator BigInteger(float value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Double" /> value to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />.</exception>
		public static explicit operator BigInteger(double value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> object to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="value">The value to convert to a <see cref="T:System.Numerics.BigInteger" />.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		public static explicit operator BigInteger(decimal value)
		{
			return new BigInteger(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to an unsigned byte value.</summary>
		/// <param name="value">The value to convert to a <see cref="T:System.Byte" />.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		public static explicit operator byte(BigInteger value)
		{
			return checked((byte)(int)value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to a signed 8-bit value.  
		///  This API is not CLS-compliant. The compliant alternative is <see cref="T:System.Int16" />.</summary>
		/// <param name="value">The value to convert to a signed 8-bit value.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.SByte.MinValue" /> or is greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator sbyte(BigInteger value)
		{
			return checked((sbyte)(int)value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to a 16-bit signed integer value.</summary>
		/// <param name="value">The value to convert to a 16-bit signed integer.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int16.MinValue" /> or is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		public static explicit operator short(BigInteger value)
		{
			return checked((short)(int)value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to an unsigned 16-bit integer value.  
		///  This API is not CLS-compliant. The compliant alternative is <see cref="T:System.Int32" />.</summary>
		/// <param name="value">The value to convert to an unsigned 16-bit integer.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.UInt16.MinValue" /> or is greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator ushort(BigInteger value)
		{
			return checked((ushort)(int)value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to a 32-bit signed integer value.</summary>
		/// <param name="value">The value to convert to a 32-bit signed integer.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int32.MinValue" /> or is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		public static explicit operator int(BigInteger value)
		{
			if (value._bits == null)
			{
				return value._sign;
			}
			if (value._bits.Length > 1)
			{
				throw new OverflowException("Value was either too large or too small for an Int32.");
			}
			if (value._sign > 0)
			{
				return checked((int)value._bits[0]);
			}
			if (value._bits[0] > 2147483648u)
			{
				throw new OverflowException("Value was either too large or too small for an Int32.");
			}
			return (int)(0 - value._bits[0]);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to an unsigned 32-bit integer value.  
		///  This API is not CLS-compliant. The compliant alternative is <see cref="T:System.Int64" />.</summary>
		/// <param name="value">The value to convert to an unsigned 32-bit integer.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.UInt32.MinValue" /> or is greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator uint(BigInteger value)
		{
			if (value._bits == null)
			{
				return checked((uint)value._sign);
			}
			if (value._bits.Length > 1 || value._sign < 0)
			{
				throw new OverflowException("Value was either too large or too small for a UInt32.");
			}
			return value._bits[0];
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to a 64-bit signed integer value.</summary>
		/// <param name="value">The value to convert to a 64-bit signed integer.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int64.MinValue" /> or is greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		public static explicit operator long(BigInteger value)
		{
			if (value._bits == null)
			{
				return value._sign;
			}
			int num = value._bits.Length;
			if (num > 2)
			{
				throw new OverflowException("Value was either too large or too small for an Int64.");
			}
			ulong num2 = ((num <= 1) ? value._bits[0] : NumericsHelpers.MakeUlong(value._bits[1], value._bits[0]));
			long num3 = (long)((value._sign > 0) ? num2 : (0L - num2));
			if ((num3 > 0 && value._sign > 0) || (num3 < 0 && value._sign < 0))
			{
				return num3;
			}
			throw new OverflowException("Value was either too large or too small for an Int64.");
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to an unsigned 64-bit integer value.  
		///  This API is not CLS-compliant. The compliant alternative is <see cref="T:System.Double" />.</summary>
		/// <param name="value">The value to convert to an unsigned 64-bit integer.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.UInt64.MinValue" /> or is greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		[CLSCompliant(false)]
		public static explicit operator ulong(BigInteger value)
		{
			if (value._bits == null)
			{
				return checked((ulong)value._sign);
			}
			int num = value._bits.Length;
			if (num > 2 || value._sign < 0)
			{
				throw new OverflowException("Value was either too large or too small for a UInt64.");
			}
			if (num > 1)
			{
				return NumericsHelpers.MakeUlong(value._bits[1], value._bits[0]);
			}
			return value._bits[0];
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to a single-precision floating-point value.</summary>
		/// <param name="value">The value to convert to a single-precision floating-point value.</param>
		/// <returns>An object that contains the closest possible representation of the <paramref name="value" /> parameter.</returns>
		public static explicit operator float(BigInteger value)
		{
			return (float)(double)value;
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to a <see cref="T:System.Double" /> value.</summary>
		/// <param name="value">The value to convert to a <see cref="T:System.Double" />.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		public static explicit operator double(BigInteger value)
		{
			int sign = value._sign;
			uint[] bits = value._bits;
			if (bits == null)
			{
				return sign;
			}
			int num = bits.Length;
			if (num > 32)
			{
				if (sign == 1)
				{
					return double.PositiveInfinity;
				}
				return double.NegativeInfinity;
			}
			long num2 = bits[num - 1];
			ulong num3 = ((num > 1) ? bits[num - 2] : 0u);
			ulong num4 = ((num > 2) ? bits[num - 3] : 0u);
			int num5 = NumericsHelpers.CbitHighZero((uint)num2);
			int exp = (num - 2) * 32 - num5;
			ulong man = (ulong)(num2 << 32 + num5) | (num3 << num5) | (num4 >> 32 - num5);
			return NumericsHelpers.GetDoubleFromParts(sign, exp, man);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> object to a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">The value to convert to a <see cref="T:System.Decimal" />.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		public static explicit operator decimal(BigInteger value)
		{
			if (value._bits == null)
			{
				return value._sign;
			}
			int num = value._bits.Length;
			if (num > 3)
			{
				throw new OverflowException("Value was either too large or too small for a Decimal.");
			}
			int lo = 0;
			int mid = 0;
			int hi = 0;
			if (num > 2)
			{
				hi = (int)value._bits[2];
			}
			if (num > 1)
			{
				mid = (int)value._bits[1];
			}
			if (num > 0)
			{
				lo = (int)value._bits[0];
			}
			return new decimal(lo, mid, hi, value._sign < 0, 0);
		}

		/// <summary>Performs a bitwise <see langword="And" /> operation on two <see cref="T:System.Numerics.BigInteger" /> values.</summary>
		/// <param name="left">The first value.</param>
		/// <param name="right">The second value.</param>
		/// <returns>The result of the bitwise <see langword="And" /> operation.</returns>
		public static BigInteger operator &(BigInteger left, BigInteger right)
		{
			if (left.IsZero || right.IsZero)
			{
				return Zero;
			}
			if (left._bits == null && right._bits == null)
			{
				return left._sign & right._sign;
			}
			uint[] array = left.ToUInt32Array();
			uint[] array2 = right.ToUInt32Array();
			uint[] array3 = new uint[Math.Max(array.Length, array2.Length)];
			uint num = ((left._sign < 0) ? uint.MaxValue : 0u);
			uint num2 = ((right._sign < 0) ? uint.MaxValue : 0u);
			for (int i = 0; i < array3.Length; i++)
			{
				uint num3 = ((i < array.Length) ? array[i] : num);
				uint num4 = ((i < array2.Length) ? array2[i] : num2);
				array3[i] = num3 & num4;
			}
			return new BigInteger(array3);
		}

		/// <summary>Performs a bitwise <see langword="Or" /> operation on two <see cref="T:System.Numerics.BigInteger" /> values.</summary>
		/// <param name="left">The first value.</param>
		/// <param name="right">The second value.</param>
		/// <returns>The result of the bitwise <see langword="Or" /> operation.</returns>
		public static BigInteger operator |(BigInteger left, BigInteger right)
		{
			if (left.IsZero)
			{
				return right;
			}
			if (right.IsZero)
			{
				return left;
			}
			if (left._bits == null && right._bits == null)
			{
				return left._sign | right._sign;
			}
			uint[] array = left.ToUInt32Array();
			uint[] array2 = right.ToUInt32Array();
			uint[] array3 = new uint[Math.Max(array.Length, array2.Length)];
			uint num = ((left._sign < 0) ? uint.MaxValue : 0u);
			uint num2 = ((right._sign < 0) ? uint.MaxValue : 0u);
			for (int i = 0; i < array3.Length; i++)
			{
				uint num3 = ((i < array.Length) ? array[i] : num);
				uint num4 = ((i < array2.Length) ? array2[i] : num2);
				array3[i] = num3 | num4;
			}
			return new BigInteger(array3);
		}

		/// <summary>Performs a bitwise exclusive <see langword="Or" /> (<see langword="XOr" />) operation on two <see cref="T:System.Numerics.BigInteger" /> values.</summary>
		/// <param name="left">The first value.</param>
		/// <param name="right">The second value.</param>
		/// <returns>The result of the bitwise <see langword="Or" /> operation.</returns>
		public static BigInteger operator ^(BigInteger left, BigInteger right)
		{
			if (left._bits == null && right._bits == null)
			{
				return left._sign ^ right._sign;
			}
			uint[] array = left.ToUInt32Array();
			uint[] array2 = right.ToUInt32Array();
			uint[] array3 = new uint[Math.Max(array.Length, array2.Length)];
			uint num = ((left._sign < 0) ? uint.MaxValue : 0u);
			uint num2 = ((right._sign < 0) ? uint.MaxValue : 0u);
			for (int i = 0; i < array3.Length; i++)
			{
				uint num3 = ((i < array.Length) ? array[i] : num);
				uint num4 = ((i < array2.Length) ? array2[i] : num2);
				array3[i] = num3 ^ num4;
			}
			return new BigInteger(array3);
		}

		/// <summary>Shifts a <see cref="T:System.Numerics.BigInteger" /> value a specified number of bits to the left.</summary>
		/// <param name="value">The value whose bits are to be shifted.</param>
		/// <param name="shift">The number of bits to shift <paramref name="value" /> to the left.</param>
		/// <returns>A value that has been shifted to the left by the specified number of bits.</returns>
		public static BigInteger operator <<(BigInteger value, int shift)
		{
			if (shift == 0)
			{
				return value;
			}
			if (shift == int.MinValue)
			{
				return value >> int.MaxValue >> 1;
			}
			if (shift < 0)
			{
				return value >> -shift;
			}
			int num = shift / 32;
			int num2 = shift - num * 32;
			uint[] xd;
			int xl;
			bool partsForBitManipulation = GetPartsForBitManipulation(ref value, out xd, out xl);
			uint[] array = new uint[xl + num + 1];
			if (num2 == 0)
			{
				for (int i = 0; i < xl; i++)
				{
					array[i + num] = xd[i];
				}
			}
			else
			{
				int num3 = 32 - num2;
				uint num4 = 0u;
				int j;
				for (j = 0; j < xl; j++)
				{
					uint num5 = xd[j];
					array[j + num] = (num5 << num2) | num4;
					num4 = num5 >> num3;
				}
				array[j + num] = num4;
			}
			return new BigInteger(array, partsForBitManipulation);
		}

		/// <summary>Shifts a <see cref="T:System.Numerics.BigInteger" /> value a specified number of bits to the right.</summary>
		/// <param name="value">The value whose bits are to be shifted.</param>
		/// <param name="shift">The number of bits to shift <paramref name="value" /> to the right.</param>
		/// <returns>A value that has been shifted to the right by the specified number of bits.</returns>
		public static BigInteger operator >>(BigInteger value, int shift)
		{
			if (shift == 0)
			{
				return value;
			}
			if (shift == int.MinValue)
			{
				return value << int.MaxValue << 1;
			}
			if (shift < 0)
			{
				return value << -shift;
			}
			int num = shift / 32;
			int num2 = shift - num * 32;
			uint[] xd;
			int xl;
			bool partsForBitManipulation = GetPartsForBitManipulation(ref value, out xd, out xl);
			if (partsForBitManipulation)
			{
				if (shift >= 32 * xl)
				{
					return MinusOne;
				}
				uint[] array = new uint[xl];
				Array.Copy(xd, 0, array, 0, xl);
				xd = array;
				NumericsHelpers.DangerousMakeTwosComplement(xd);
			}
			int num3 = xl - num;
			if (num3 < 0)
			{
				num3 = 0;
			}
			uint[] array2 = new uint[num3];
			if (num2 == 0)
			{
				for (int num4 = xl - 1; num4 >= num; num4--)
				{
					array2[num4 - num] = xd[num4];
				}
			}
			else
			{
				int num5 = 32 - num2;
				uint num6 = 0u;
				for (int num7 = xl - 1; num7 >= num; num7--)
				{
					uint num8 = xd[num7];
					if (partsForBitManipulation && num7 == xl - 1)
					{
						array2[num7 - num] = (num8 >> num2) | (uint)(-1 << num5);
					}
					else
					{
						array2[num7 - num] = (num8 >> num2) | num6;
					}
					num6 = num8 << num5;
				}
			}
			if (partsForBitManipulation)
			{
				NumericsHelpers.DangerousMakeTwosComplement(array2);
			}
			return new BigInteger(array2, partsForBitManipulation);
		}

		/// <summary>Returns the bitwise one's complement of a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="value">An integer value.</param>
		/// <returns>The bitwise one's complement of <paramref name="value" />.</returns>
		public static BigInteger operator ~(BigInteger value)
		{
			return -(value + One);
		}

		/// <summary>Negates a specified BigInteger value.</summary>
		/// <param name="value">The value to negate.</param>
		/// <returns>The result of the <paramref name="value" /> parameter multiplied by negative one (-1).</returns>
		public static BigInteger operator -(BigInteger value)
		{
			return new BigInteger(-value._sign, value._bits);
		}

		/// <summary>Returns the value of the <see cref="T:System.Numerics.BigInteger" /> operand. (The sign of the operand is unchanged.)</summary>
		/// <param name="value">An integer value.</param>
		/// <returns>The value of the <paramref name="value" /> operand.</returns>
		public static BigInteger operator +(BigInteger value)
		{
			return value;
		}

		/// <summary>Increments a <see cref="T:System.Numerics.BigInteger" /> value by 1.</summary>
		/// <param name="value">The value to increment.</param>
		/// <returns>The value of the <paramref name="value" /> parameter incremented by 1.</returns>
		public static BigInteger operator ++(BigInteger value)
		{
			return value + One;
		}

		/// <summary>Decrements a <see cref="T:System.Numerics.BigInteger" /> value by 1.</summary>
		/// <param name="value">The value to decrement.</param>
		/// <returns>The value of the <paramref name="value" /> parameter decremented by 1.</returns>
		public static BigInteger operator --(BigInteger value)
		{
			return value - One;
		}

		/// <summary>Adds the values of two specified <see cref="T:System.Numerics.BigInteger" /> objects.</summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of <paramref name="left" /> and <paramref name="right" />.</returns>
		public static BigInteger operator +(BigInteger left, BigInteger right)
		{
			if (left._sign < 0 != right._sign < 0)
			{
				return Subtract(left._bits, left._sign, right._bits, -1 * right._sign);
			}
			return Add(left._bits, left._sign, right._bits, right._sign);
		}

		/// <summary>Multiplies two specified <see cref="T:System.Numerics.BigInteger" /> values.</summary>
		/// <param name="left">The first value to multiply.</param>
		/// <param name="right">The second value to multiply.</param>
		/// <returns>The product of <paramref name="left" /> and <paramref name="right" />.</returns>
		public static BigInteger operator *(BigInteger left, BigInteger right)
		{
			bool flag = left._bits == null;
			bool flag2 = right._bits == null;
			if (flag && flag2)
			{
				return (long)left._sign * (long)right._sign;
			}
			if (flag)
			{
				return new BigInteger(BigIntegerCalculator.Multiply(right._bits, NumericsHelpers.Abs(left._sign)), (left._sign < 0) ^ (right._sign < 0));
			}
			if (flag2)
			{
				return new BigInteger(BigIntegerCalculator.Multiply(left._bits, NumericsHelpers.Abs(right._sign)), (left._sign < 0) ^ (right._sign < 0));
			}
			if (left._bits == right._bits)
			{
				return new BigInteger(BigIntegerCalculator.Square(left._bits), (left._sign < 0) ^ (right._sign < 0));
			}
			if (left._bits.Length < right._bits.Length)
			{
				return new BigInteger(BigIntegerCalculator.Multiply(right._bits, left._bits), (left._sign < 0) ^ (right._sign < 0));
			}
			return new BigInteger(BigIntegerCalculator.Multiply(left._bits, right._bits), (left._sign < 0) ^ (right._sign < 0));
		}

		/// <summary>Divides a specified <see cref="T:System.Numerics.BigInteger" /> value by another specified <see cref="T:System.Numerics.BigInteger" /> value by using integer division.</summary>
		/// <param name="dividend">The value to be divided.</param>
		/// <param name="divisor">The value to divide by.</param>
		/// <returns>The integral result of the division.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="divisor" /> is 0 (zero).</exception>
		public static BigInteger operator /(BigInteger dividend, BigInteger divisor)
		{
			bool flag = dividend._bits == null;
			bool flag2 = divisor._bits == null;
			if (flag && flag2)
			{
				return dividend._sign / divisor._sign;
			}
			if (flag)
			{
				return s_bnZeroInt;
			}
			if (flag2)
			{
				return new BigInteger(BigIntegerCalculator.Divide(dividend._bits, NumericsHelpers.Abs(divisor._sign)), (dividend._sign < 0) ^ (divisor._sign < 0));
			}
			if (dividend._bits.Length < divisor._bits.Length)
			{
				return s_bnZeroInt;
			}
			return new BigInteger(BigIntegerCalculator.Divide(dividend._bits, divisor._bits), (dividend._sign < 0) ^ (divisor._sign < 0));
		}

		/// <summary>Returns the remainder that results from division with two specified <see cref="T:System.Numerics.BigInteger" /> values.</summary>
		/// <param name="dividend">The value to be divided.</param>
		/// <param name="divisor">The value to divide by.</param>
		/// <returns>The remainder that results from the division.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="divisor" /> is 0 (zero).</exception>
		public static BigInteger operator %(BigInteger dividend, BigInteger divisor)
		{
			bool flag = dividend._bits == null;
			bool flag2 = divisor._bits == null;
			if (flag && flag2)
			{
				return dividend._sign % divisor._sign;
			}
			if (flag)
			{
				return dividend;
			}
			if (flag2)
			{
				uint num = BigIntegerCalculator.Remainder(dividend._bits, NumericsHelpers.Abs(divisor._sign));
				return (dividend._sign < 0) ? (-1 * num) : num;
			}
			if (dividend._bits.Length < divisor._bits.Length)
			{
				return dividend;
			}
			return new BigInteger(BigIntegerCalculator.Remainder(dividend._bits, divisor._bits), dividend._sign < 0);
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is less than another <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		public static bool operator <(BigInteger left, BigInteger right)
		{
			return left.CompareTo(right) < 0;
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is less than or equal to another <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		public static bool operator <=(BigInteger left, BigInteger right)
		{
			return left.CompareTo(right) <= 0;
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is greater than another <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		public static bool operator >(BigInteger left, BigInteger right)
		{
			return left.CompareTo(right) > 0;
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is greater than or equal to another <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		public static bool operator >=(BigInteger left, BigInteger right)
		{
			return left.CompareTo(right) >= 0;
		}

		/// <summary>Returns a value that indicates whether the values of two <see cref="T:System.Numerics.BigInteger" /> objects are equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, <see langword="false" />.</returns>
		public static bool operator ==(BigInteger left, BigInteger right)
		{
			return left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Numerics.BigInteger" /> objects have different values.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		public static bool operator !=(BigInteger left, BigInteger right)
		{
			return !left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is less than a 64-bit signed integer.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		public static bool operator <(BigInteger left, long right)
		{
			return left.CompareTo(right) < 0;
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is less than or equal to a 64-bit signed integer.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		public static bool operator <=(BigInteger left, long right)
		{
			return left.CompareTo(right) <= 0;
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> is greater than a 64-bit signed integer value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		public static bool operator >(BigInteger left, long right)
		{
			return left.CompareTo(right) > 0;
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is greater than or equal to a 64-bit signed integer value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		public static bool operator >=(BigInteger left, long right)
		{
			return left.CompareTo(right) >= 0;
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value and a signed long integer value are equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, <see langword="false" />.</returns>
		public static bool operator ==(BigInteger left, long right)
		{
			return left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value and a 64-bit signed integer are not equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		public static bool operator !=(BigInteger left, long right)
		{
			return !left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether a 64-bit signed integer is less than a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		public static bool operator <(long left, BigInteger right)
		{
			return right.CompareTo(left) > 0;
		}

		/// <summary>Returns a value that indicates whether a 64-bit signed integer is less than or equal to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		public static bool operator <=(long left, BigInteger right)
		{
			return right.CompareTo(left) >= 0;
		}

		/// <summary>Returns a value that indicates whether a 64-bit signed integer is greater than a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		public static bool operator >(long left, BigInteger right)
		{
			return right.CompareTo(left) < 0;
		}

		/// <summary>Returns a value that indicates whether a 64-bit signed integer is greater than or equal to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		public static bool operator >=(long left, BigInteger right)
		{
			return right.CompareTo(left) <= 0;
		}

		/// <summary>Returns a value that indicates whether a signed long integer value and a <see cref="T:System.Numerics.BigInteger" /> value are equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, <see langword="false" />.</returns>
		public static bool operator ==(long left, BigInteger right)
		{
			return right.Equals(left);
		}

		/// <summary>Returns a value that indicates whether a 64-bit signed integer and a <see cref="T:System.Numerics.BigInteger" /> value are not equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		public static bool operator !=(long left, BigInteger right)
		{
			return !right.Equals(left);
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is less than a 64-bit unsigned integer.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		[CLSCompliant(false)]
		public static bool operator <(BigInteger left, ulong right)
		{
			return left.CompareTo(right) < 0;
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is less than or equal to a 64-bit unsigned integer.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		[CLSCompliant(false)]
		public static bool operator <=(BigInteger left, ulong right)
		{
			return left.CompareTo(right) <= 0;
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is greater than a 64-bit unsigned integer.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		[CLSCompliant(false)]
		public static bool operator >(BigInteger left, ulong right)
		{
			return left.CompareTo(right) > 0;
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is greater than or equal to a 64-bit unsigned integer value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		[CLSCompliant(false)]
		public static bool operator >=(BigInteger left, ulong right)
		{
			return left.CompareTo(right) >= 0;
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value and an unsigned long integer value are equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, <see langword="false" />.</returns>
		[CLSCompliant(false)]
		public static bool operator ==(BigInteger left, ulong right)
		{
			return left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value and a 64-bit unsigned integer are not equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		[CLSCompliant(false)]
		public static bool operator !=(BigInteger left, ulong right)
		{
			return !left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether a 64-bit unsigned integer is less than a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		[CLSCompliant(false)]
		public static bool operator <(ulong left, BigInteger right)
		{
			return right.CompareTo(left) > 0;
		}

		/// <summary>Returns a value that indicates whether a 64-bit unsigned integer is less than or equal to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		[CLSCompliant(false)]
		public static bool operator <=(ulong left, BigInteger right)
		{
			return right.CompareTo(left) >= 0;
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.Numerics.BigInteger" /> value is greater than a 64-bit unsigned integer.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		[CLSCompliant(false)]
		public static bool operator >(ulong left, BigInteger right)
		{
			return right.CompareTo(left) < 0;
		}

		/// <summary>Returns a value that indicates whether a 64-bit unsigned integer is greater than or equal to a <see cref="T:System.Numerics.BigInteger" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		[CLSCompliant(false)]
		public static bool operator >=(ulong left, BigInteger right)
		{
			return right.CompareTo(left) <= 0;
		}

		/// <summary>Returns a value that indicates whether an unsigned long integer value and a <see cref="T:System.Numerics.BigInteger" /> value are equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, <see langword="false" />.</returns>
		[CLSCompliant(false)]
		public static bool operator ==(ulong left, BigInteger right)
		{
			return right.Equals(left);
		}

		/// <summary>Returns a value that indicates whether a 64-bit unsigned integer and a <see cref="T:System.Numerics.BigInteger" /> value are not equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		[CLSCompliant(false)]
		public static bool operator !=(ulong left, BigInteger right)
		{
			return !right.Equals(left);
		}

		private static bool GetPartsForBitManipulation(ref BigInteger x, out uint[] xd, out int xl)
		{
			if (x._bits == null)
			{
				if (x._sign < 0)
				{
					xd = new uint[1] { (uint)(-x._sign) };
				}
				else
				{
					xd = new uint[1] { (uint)x._sign };
				}
			}
			else
			{
				xd = x._bits;
			}
			xl = ((x._bits == null) ? 1 : x._bits.Length);
			return x._sign < 0;
		}

		internal static int GetDiffLength(uint[] rgu1, uint[] rgu2, int cu)
		{
			int num = cu;
			while (--num >= 0)
			{
				if (rgu1[num] != rgu2[num])
				{
					return num + 1;
				}
			}
			return 0;
		}

		[Conditional("DEBUG")]
		private void AssertValid()
		{
			_ = _bits;
		}
	}
	internal static class BigIntegerCalculator
	{
		internal struct BitsBuffer
		{
			private uint[] _bits;

			private int _length;

			public BitsBuffer(int size, uint value)
			{
				_bits = new uint[size];
				_length = ((value != 0) ? 1 : 0);
				_bits[0] = value;
			}

			public BitsBuffer(int size, uint[] value)
			{
				_bits = new uint[size];
				_length = ActualLength(value);
				Array.Copy(value, 0, _bits, 0, _length);
			}

			public unsafe void MultiplySelf(ref BitsBuffer value, ref BitsBuffer temp)
			{
				fixed (uint* bits = _bits)
				{
					fixed (uint* bits2 = value._bits)
					{
						fixed (uint* bits3 = temp._bits)
						{
							if (_length < value._length)
							{
								Multiply(bits2, value._length, bits, _length, bits3, _length + value._length);
							}
							else
							{
								Multiply(bits, _length, bits2, value._length, bits3, _length + value._length);
							}
						}
					}
				}
				Apply(ref temp, _length + value._length);
			}

			public unsafe void SquareSelf(ref BitsBuffer temp)
			{
				fixed (uint* bits = _bits)
				{
					fixed (uint* bits2 = temp._bits)
					{
						Square(bits, _length, bits2, _length + _length);
					}
				}
				Apply(ref temp, _length + _length);
			}

			public void Reduce(ref FastReducer reducer)
			{
				_length = reducer.Reduce(_bits, _length);
			}

			public unsafe void Reduce(uint[] modulus)
			{
				if (_length < modulus.Length)
				{
					return;
				}
				fixed (uint* bits = _bits)
				{
					fixed (uint* right = modulus)
					{
						Divide(bits, _length, right, modulus.Length, null, 0);
					}
				}
				_length = ActualLength(_bits, modulus.Length);
			}

			public unsafe void Reduce(ref BitsBuffer modulus)
			{
				if (_length < modulus._length)
				{
					return;
				}
				fixed (uint* bits = _bits)
				{
					fixed (uint* bits2 = modulus._bits)
					{
						Divide(bits, _length, bits2, modulus._length, null, 0);
					}
				}
				_length = ActualLength(_bits, modulus._length);
			}

			public void Overwrite(ulong value)
			{
				if (_length > 2)
				{
					Array.Clear(_bits, 2, _length - 2);
				}
				uint num = (uint)value;
				uint num2 = (uint)(value >> 32);
				_bits[0] = num;
				_bits[1] = num2;
				_length = ((num2 != 0) ? 2 : ((num != 0) ? 1 : 0));
			}

			public void Overwrite(uint value)
			{
				if (_length > 1)
				{
					Array.Clear(_bits, 1, _length - 1);
				}
				_bits[0] = value;
				_length = ((value != 0) ? 1 : 0);
			}

			public uint[] GetBits()
			{
				return _bits;
			}

			public int GetSize()
			{
				return _bits.Length;
			}

			public int GetLength()
			{
				return _length;
			}

			public void Refresh(int maxLength)
			{
				if (_length > maxLength)
				{
					Array.Clear(_bits, maxLength, _length - maxLength);
				}
				_length = ActualLength(_bits, maxLength);
			}

			private void Apply(ref BitsBuffer temp, int maxLength)
			{
				Array.Clear(_bits, 0, _length);
				uint[] bits = temp._bits;
				temp._bits = _bits;
				_bits = bits;
				_length = ActualLength(_bits, maxLength);
			}
		}

		internal readonly struct FastReducer
		{
			private readonly uint[] _modulus;

			private readonly uint[] _mu;

			private readonly uint[] _q1;

			private readonly uint[] _q2;

			private readonly int _muLength;

			public FastReducer(uint[] modulus)
			{
				uint[] array = new uint[modulus.Length * 2 + 1];
				array[^1] = 1u;
				_mu = Divide(array, modulus);
				_modulus = modulus;
				_q1 = new uint[modulus.Length * 2 + 2];
				_q2 = new uint[modulus.Length * 2 + 1];
				_muLength = ActualLength(_mu);
			}

			public int Reduce(uint[] value, int length)
			{
				if (length < _modulus.Length)
				{
					return length;
				}
				int leftLength = DivMul(value, length, _mu, _muLength, _q1, _modulus.Length - 1);
				int rightLength = DivMul(_q1, leftLength, _modulus, _modulus.Length, _q2, _modulus.Length + 1);
				return SubMod(value, length, _q2, rightLength, _modulus, _modulus.Length + 1);
			}

			private unsafe static int DivMul(uint[] left, int leftLength, uint[] right, int rightLength, uint[] bits, int k)
			{
				Array.Clear(bits, 0, bits.Length);
				if (leftLength > k)
				{
					leftLength -= k;
					fixed (uint* ptr = left)
					{
						fixed (uint* ptr2 = right)
						{
							fixed (uint* bits2 = bits)
							{
								if (leftLength < rightLength)
								{
									Multiply(ptr2, rightLength, ptr + k, leftLength, bits2, leftLength + rightLength);
								}
								else
								{
									Multiply(ptr + k, leftLength, ptr2, rightLength, bits2, leftLength + rightLength);
								}
							}
						}
					}
					return ActualLength(bits, leftLength + rightLength);
				}
				return 0;
			}

			private unsafe static int SubMod(uint[] left, int leftLength, uint[] right, int rightLength, uint[] modulus, int k)
			{
				if (leftLength > k)
				{
					leftLength = k;
				}
				if (rightLength > k)
				{
					rightLength = k;
				}
				fixed (uint* left2 = left)
				{
					fixed (uint* right2 = right)
					{
						fixed (uint* right3 = modulus)
						{
							SubtractSelf(left2, leftLength, right2, rightLength);
							leftLength = ActualLength(left, leftLength);
							while (Compare(left2, leftLength, right3, modulus.Length) >= 0)
							{
								SubtractSelf(left2, leftLength, right3, modulus.Length);
								leftLength = ActualLength(left, leftLength);
							}
						}
					}
				}
				Array.Clear(left, leftLength, left.Length - leftLength);
				return leftLength;
			}
		}

		private static int ReducerThreshold = 32;

		private static int SquareThreshold = 32;

		private static int AllocationThreshold = 256;

		private static int MultiplyThreshold = 32;

		public static uint[] Add(uint[] left, uint right)
		{
			uint[] array = new uint[left.Length + 1];
			long num = (long)left[0] + (long)right;
			array[0] = (uint)num;
			long num2 = num >> 32;
			for (int i = 1; i < left.Length; i++)
			{
				num = left[i] + num2;
				array[i] = (uint)num;
				num2 = num >> 32;
			}
			array[left.Length] = (uint)num2;
			return array;
		}

		public unsafe static uint[] Add(uint[] left, uint[] right)
		{
			uint[] array = new uint[left.Length + 1];
			fixed (uint* left2 = left)
			{
				fixed (uint* right2 = right)
				{
					fixed (uint* bits = &array[0])
					{
						Add(left2, left.Length, right2, right.Length, bits, array.Length);
					}
				}
			}
			return array;
		}

		private unsafe static void Add(uint* left, int leftLength, uint* right, int rightLength, uint* bits, int bitsLength)
		{
			int i = 0;
			long num = 0L;
			for (; i < rightLength; i++)
			{
				long num2 = left[i] + num + right[i];
				bits[i] = (uint)num2;
				num = num2 >> 32;
			}
			for (; i < leftLength; i++)
			{
				long num3 = left[i] + num;
				bits[i] = (uint)num3;
				num = num3 >> 32;
			}
			bits[i] = (uint)num;
		}

		private unsafe static void AddSelf(uint* left, int leftLength, uint* right, int rightLength)
		{
			int i = 0;
			long num = 0L;
			for (; i < rightLength; i++)
			{
				long num2 = left[i] + num + right[i];
				left[i] = (uint)num2;
				num = num2 >> 32;
			}
			while (num != 0L && i < leftLength)
			{
				long num3 = left[i] + num;
				left[i] = (uint)num3;
				num = num3 >> 32;
				i++;
			}
		}

		public static uint[] Subtract(uint[] left, uint right)
		{
			uint[] array = new uint[left.Length];
			long num = (long)left[0] - (long)right;
			array[0] = (uint)num;
			long num2 = num >> 32;
			for (int i = 1; i < left.Length; i++)
			{
				num = left[i] + num2;
				array[i] = (uint)num;
				num2 = num >> 32;
			}
			return array;
		}

		public unsafe static uint[] Subtract(uint[] left, uint[] right)
		{
			uint[] array = new uint[left.Length];
			fixed (uint* left2 = left)
			{
				fixed (uint* right2 = right)
				{
					fixed (uint* bits = array)
					{
						Subtract(left2, left.Length, right2, right.Length, bits, array.Length);
					}
				}
			}
			return array;
		}

		private unsafe static void Subtract(uint* left, int leftLength, uint* right, int rightLength, uint* bits, int bitsLength)
		{
			int i = 0;
			long num = 0L;
			for (; i < rightLength; i++)
			{
				long num2 = left[i] + num - right[i];
				bits[i] = (uint)num2;
				num = num2 >> 32;
			}
			for (; i < leftLength; i++)
			{
				long num3 = left[i] + num;
				bits[i] = (uint)num3;
				num = num3 >> 32;
			}
		}

		private unsafe static void SubtractSelf(uint* left, int leftLength, uint* right, int rightLength)
		{
			int i = 0;
			long num = 0L;
			for (; i < rightLength; i++)
			{
				long num2 = left[i] + num - right[i];
				left[i] = (uint)num2;
				num = num2 >> 32;
			}
			while (num != 0L && i < leftLength)
			{
				long num3 = left[i] + num;
				left[i] = (uint)num3;
				num = num3 >> 32;
				i++;
			}
		}

		public static int Compare(uint[] left, uint[] right)
		{
			if (left.Length < right.Length)
			{
				return -1;
			}
			if (left.Length > right.Length)
			{
				return 1;
			}
			for (int num = left.Length - 1; num >= 0; num--)
			{
				if (left[num] < right[num])
				{
					return -1;
				}
				if (left[num] > right[num])
				{
					return 1;
				}
			}
			return 0;
		}

		private unsafe static int Compare(uint* left, int leftLength, uint* right, int rightLength)
		{
			if (leftLength < rightLength)
			{
				return -1;
			}
			if (leftLength > rightLength)
			{
				return 1;
			}
			for (int num = leftLength - 1; num >= 0; num--)
			{
				if (left[num] < right[num])
				{
					return -1;
				}
				if (left[num] > right[num])
				{
					return 1;
				}
			}
			return 0;
		}

		public static uint[] Divide(uint[] left, uint right, out uint remainder)
		{
			uint[] array = new uint[left.Length];
			ulong num = 0uL;
			for (int num2 = left.Length - 1; num2 >= 0; num2--)
			{
				ulong num3 = (num << 32) | left[num2];
				ulong num4 = num3 / right;
				array[num2] = (uint)num4;
				num = num3 - num4 * right;
			}
			remainder = (uint)num;
			return array;
		}

		public static uint[] Divide(uint[] left, uint right)
		{
			uint[] array = new uint[left.Length];
			ulong num = 0uL;
			for (int num2 = left.Length - 1; num2 >= 0; num2--)
			{
				ulong num3 = (num << 32) | left[num2];
				ulong num4 = num3 / right;
				array[num2] = (uint)num4;
				num = num3 - num4 * right;
			}
			return array;
		}

		public static uint Remainder(uint[] left, uint right)
		{
			ulong num = 0uL;
			for (int num2 = left.Length - 1; num2 >= 0; num2--)
			{
				num = ((num << 32) | left[num2]) % right;
			}
			return (uint)num;
		}

		public unsafe static uint[] Divide(uint[] left, uint[] right, out uint[] remainder)
		{
			uint[] array = CreateCopy(left);
			uint[] array2 = new uint[left.Length - right.Length + 1];
			fixed (uint* left2 = &array[0])
			{
				fixed (uint* right2 = &right[0])
				{
					fixed (uint* bits = &array2[0])
					{
						Divide(left2, array.Length, right2, right.Length, bits, array2.Length);
					}
				}
			}
			remainder = array;
			return array2;
		}

		public unsafe static uint[] Divide(uint[] left, uint[] right)
		{
			uint[] array = CreateCopy(left);
			uint[] array2 = new uint[left.Length - right.Length + 1];
			fixed (uint* left2 = &array[0])
			{
				fixed (uint* right2 = &right[0])
				{
					fixed (uint* bits = &array2[0])
					{
						Divide(left2, array.Length, right2, right.Length, bits, array2.Length);
					}
				}
			}
			return array2;
		}

		public unsafe static uint[] Remainder(uint[] left, uint[] right)
		{
			uint[] array = CreateCopy(left);
			fixed (uint* left2 = &array[0])
			{
				fixed (uint* right2 = &right[0])
				{
					Divide(left2, array.Length, right2, right.Length, null, 0);
				}
			}
			return array;
		}

		private unsafe static void Divide(uint* left, int leftLength, uint* right, int rightLength, uint* bits, int bitsLength)
		{
			uint num = right[rightLength - 1];
			uint num2 = ((rightLength > 1) ? right[rightLength - 2] : 0u);
			int num3 = LeadingZeros(num);
			int num4 = 32 - num3;
			if (num3 > 0)
			{
				uint num5 = ((rightLength > 2) ? right[rightLength - 3] : 0u);
				num = (num << num3) | (num2 >> num4);
				num2 = (num2 << num3) | (num5 >> num4);
			}
			for (int num6 = leftLength; num6 >= rightLength; num6--)
			{
				int num7 = num6 - rightLength;
				uint num8 = ((num6 < leftLength) ? left[num6] : 0u);
				ulong num9 = ((ulong)num8 << 32) | left[num6 - 1];
				uint num10 = ((num6 > 1) ? left[num6 - 2] : 0u);
				if (num3 > 0)
				{
					uint num11 = ((num6 > 2) ? left[num6 - 3] : 0u);
					num9 = (num9 << num3) | (num10 >> num4);
					num10 = (num10 << num3) | (num11 >> num4);
				}
				ulong num12 = num9 / num;
				if (num12 > uint.MaxValue)
				{
					num12 = 4294967295uL;
				}
				while (DivideGuessTooBig(num12, num9, num10, num, num2))
				{
					num12--;
				}
				if (num12 != 0 && SubtractDivisor(left + num7, leftLength - num7, right, rightLength, num12) != num8)
				{
					AddDivisor(left + num7, leftLength - num7, right, rightLength);
					num12--;
				}
				if (bitsLength != 0)
				{
					bits[num7] = (uint)num12;
				}
				if (num6 < leftLength)
				{
					left[num6] = 0u;
				}
			}
		}

		private unsafe static uint AddDivisor(uint* left, int leftLength, uint* right, int rightLength)
		{
			ulong num = 0uL;
			for (int i = 0; i < rightLength; i++)
			{
				ulong num2 = left[i] + num + right[i];
				left[i] = (uint)num2;
				num = num2 >> 32;
			}
			return (uint)num;
		}

		private unsafe static uint SubtractDivisor(uint* left, int leftLength, uint* right, int rightLength, ulong q)
		{
			ulong num = 0uL;
			for (int i = 0; i < rightLength; i++)
			{
				num += right[i] * q;
				uint num2 = (uint)num;
				num >>= 32;
				if (left[i] < num2)
				{
					num++;
				}
				left[i] -= num2;
			}
			return (uint)num;
		}

		private static bool DivideGuessTooBig(ulong q, ulong valHi, uint valLo, uint divHi, uint divLo)
		{
			ulong num = divHi * q;
			ulong num2 = divLo * q;
			num += num2 >> 32;
			num2 &= 0xFFFFFFFFu;
			if (num < valHi)
			{
				return false;
			}
			if (num > valHi)
			{
				return true;
			}
			if (num2 < valLo)
			{
				return false;
			}
			if (num2 > valLo)
			{
				return true;
			}
			return false;
		}

		private static uint[] CreateCopy(uint[] value)
		{
			uint[] array = new uint[value.Length];
			Array.Copy(value, 0, array, 0, array.Length);
			return array;
		}

		private static int LeadingZeros(uint value)
		{
			if (value == 0)
			{
				return 32;
			}
			int num = 0;
			if ((value & 0xFFFF0000u) == 0)
			{
				num += 16;
				value <<= 16;
			}
			if ((value & 0xFF000000u) == 0)
			{
				num += 8;
				value <<= 8;
			}
			if ((value & 0xF0000000u) == 0)
			{
				num += 4;
				value <<= 4;
			}
			if ((value & 0xC0000000u) == 0)
			{
				num += 2;
				value <<= 2;
			}
			if ((value & 0x80000000u) == 0)
			{
				num++;
			}
			return num;
		}

		public static uint Gcd(uint left, uint right)
		{
			while (right != 0)
			{
				uint num = left % right;
				left = right;
				right = num;
			}
			return left;
		}

		public static ulong Gcd(ulong left, ulong right)
		{
			while (right > uint.MaxValue)
			{
				ulong num = left % right;
				left = right;
				right = num;
			}
			if (right != 0L)
			{
				return Gcd((uint)right, (uint)(left % right));
			}
			return left;
		}

		public static uint Gcd(uint[] left, uint right)
		{
			uint right2 = Remainder(left, right);
			return Gcd(right, right2);
		}

		public static uint[] Gcd(uint[] left, uint[] right)
		{
			BitsBuffer left2 = new BitsBuffer(left.Length, left);
			BitsBuffer right2 = new BitsBuffer(right.Length, right);
			Gcd(ref left2, ref right2);
			return left2.GetBits();
		}

		private static void Gcd(ref BitsBuffer left, ref BitsBuffer right)
		{
			while (right.GetLength() > 2)
			{
				ExtractDigits(ref left, ref right, out var x, out var y);
				uint num = 1u;
				uint num2 = 0u;
				uint num3 = 0u;
				uint num4 = 1u;
				int num5 = 0;
				while (y != 0L)
				{
					ulong num6 = x / y;
					if (num6 > uint.MaxValue)
					{
						break;
					}
					ulong num7 = num + num6 * num3;
					ulong num8 = num2 + num6 * num4;
					ulong num9 = x - num6 * y;
					if (num7 > int.MaxValue || num8 > int.MaxValue || num9 < num8 || num9 + num7 > y - num3)
					{
						break;
					}
					num = (uint)num7;
					num2 = (uint)num8;
					x = num9;
					num5++;
					if (x == num2)
					{
						break;
					}
					num6 = y / x;
					if (num6 > uint.MaxValue)
					{
						break;
					}
					num7 = num4 + num6 * num2;
					num8 = num3 + num6 * num;
					num9 = y - num6 * x;
					if (num7 > int.MaxValue || num8 > int.MaxValue || num9 < num8 || num9 + num7 > x - num2)
					{
						break;
					}
					num4 = (uint)num7;
					num3 = (uint)num8;
					y = num9;
					num5++;
					if (y == num3)
					{
						break;
					}
				}
				if (num2 == 0)
				{
					left.Reduce(ref right);
					BitsBuffer bitsBuffer = left;
					left = right;
					right = bitsBuffer;
					continue;
				}
				LehmerCore(ref left, ref right, num, num2, num3, num4);
				if (num5 % 2 == 1)
				{
					BitsBuffer bitsBuffer2 = left;
					left = right;
					right = bitsBuffer2;
				}
			}
			if (right.GetLength() > 0)
			{
				left.Reduce(ref right);
				uint[] bits = right.GetBits();
				uint[] bits2 = left.GetBits();
				ulong left2 = ((ulong)bits[1] << 32) | bits[0];
				ulong right2 = ((ulong)bits2[1] << 32) | bits2[0];
				left.Overwrite(Gcd(left2, right2));
				right.Overwrite(0u);
			}
		}

		private static void ExtractDigits(ref BitsBuffer xBuffer, ref BitsBuffer yBuffer, out ulong x, out ulong y)
		{
			uint[] bits = xBuffer.GetBits();
			int length = xBuffer.GetLength();
			uint[] bits2 = yBuffer.GetBits();
			int length2 = yBuffer.GetLength();
			ulong num = bits[length - 1];
			ulong num2 = bits[length - 2];
			ulong num3 = bits[length - 3];
			ulong num4;
			ulong num5;
			ulong num6;
			switch (length - length2)
			{
			case 0:
				num4 = bits2[length2 - 1];
				num5 = bits2[length2 - 2];
				num6 = bits2[length2 - 3];
				break;
			case 1:
				num4 = 0uL;
				num5 = bits2[length2 - 1];
				num6 = bits2[length2 - 2];
				break;
			case 2:
				num4 = 0uL;
				num5 = 0uL;
				num6 = bits2[length2 - 1];
				break;
			default:
				num4 = 0uL;
				num5 = 0uL;
				num6 = 0uL;
				break;
			}
			int num7 = LeadingZeros((uint)num);
			x = ((num << 32 + num7) | (num2 << num7) | (num3 >> 32 - num7)) >> 1;
			y = ((num4 << 32 + num7) | (num5 << num7) | (num6 >> 32 - num7)) >> 1;
		}

		private static void LehmerCore(ref BitsBuffer xBuffer, ref BitsBuffer yBuffer, long a, long b, long c, long d)
		{
			uint[] bits = xBuffer.GetBits();
			uint[] bits2 = yBuffer.GetBits();
			int length = yBuffer.GetLength();
			long num = 0L;
			long num2 = 0L;
			for (int i = 0; i < length; i++)
			{
				long num3 = a * bits[i] - b * bits2[i] + num;
				long num4 = d * bits2[i] - c * bits[i] + num2;
				num = num3 >> 32;
				num2 = num4 >> 32;
				bits[i] = (uint)num3;
				bits2[i] = (uint)num4;
			}
			xBuffer.Refresh(length);
			yBuffer.Refresh(length);
		}

		public static uint[] Pow(uint value, uint power)
		{
			int size = PowBound(power, 1, 1);
			BitsBuffer value2 = new BitsBuffer(size, value);
			return PowCore(power, ref value2);
		}

		public static uint[] Pow(uint[] value, uint power)
		{
			int size = PowBound(power, value.Length, 1);
			BitsBuffer value2 = new BitsBuffer(size, value);
			return PowCore(power, ref value2);
		}

		private static uint[] PowCore(uint power, ref BitsBuffer value)
		{
			int size = value.GetSize();
			BitsBuffer temp = new BitsBuffer(size, 0u);
			BitsBuffer result = new BitsBuffer(size, 1u);
			PowCore(power, ref value, ref result, ref temp);
			return result.GetBits();
		}

		private static int PowBound(uint power, int valueLength, int resultLength)
		{
			checked
			{
				while (power != 0)
				{
					if ((power & 1) == 1)
					{
						resultLength += valueLength;
					}
					if (power != 1)
					{
						valueLength += valueLength;
					}
					power >>= 1;
				}
				return resultLength;
			}
		}

		private static void PowCore(uint power, ref BitsBuffer value, ref BitsBuffer result, ref BitsBuffer temp)
		{
			while (power != 0)
			{
				if ((power & 1) == 1)
				{
					result.MultiplySelf(ref value, ref temp);
				}
				if (power != 1)
				{
					value.SquareSelf(ref temp);
				}
				power >>= 1;
			}
		}

		public static uint Pow(uint value, uint power, uint modulus)
		{
			return PowCore(power, modulus, value, 1uL);
		}

		public static uint Pow(uint[] value, uint power, uint modulus)
		{
			uint num = Remainder(value, modulus);
			return PowCore(power, modulus, num, 1uL);
		}

		public static uint Pow(uint value, uint[] power, uint modulus)
		{
			return PowCore(power, modulus, value, 1uL);
		}

		public static uint Pow(uint[] value, uint[] power, uint modulus)
		{
			uint num = Remainder(value, modulus);
			return PowCore(power, modulus, num, 1uL);
		}

		private static uint PowCore(uint[] power, uint modulus, ulong value, ulong result)
		{
			for (int i = 0; i < power.Length - 1; i++)
			{
				uint num = power[i];
				for (int j = 0; j < 32; j++)
				{
					if ((num & 1) == 1)
					{
						result = result * value % modulus;
					}
					value = value * value % modulus;
					num >>= 1;
				}
			}
			return PowCore(power[^1], modulus, value, result);
		}

		private static uint PowCore(uint power, uint modulus, ulong value, ulong result)
		{
			while (power != 0)
			{
				if ((power & 1) == 1)
				{
					result = result * value % modulus;
				}
				if (power != 1)
				{
					value = value * value % modulus;
				}
				power >>= 1;
			}
			return (uint)(result % modulus);
		}

		public static uint[] Pow(uint value, uint power, uint[] modulus)
		{
			int size = modulus.Length + modulus.Length;
			BitsBuffer value2 = new BitsBuffer(size, value);
			return PowCore(power, modulus, ref value2);
		}

		public static uint[] Pow(uint[] value, uint power, uint[] modulus)
		{
			if (value.Length > modulus.Length)
			{
				value = Remainder(value, modulus);
			}
			int size = modulus.Length + modulus.Length;
			BitsBuffer value2 = new BitsBuffer(size, value);
			return PowCore(power, modulus, ref value2);
		}

		public static uint[] Pow(uint value, uint[] power, uint[] modulus)
		{
			int size = modulus.Length + modulus.Length;
			BitsBuffer value2 = new BitsBuffer(size, value);
			return PowCore(power, modulus, ref value2);
		}

		public static uint[] Pow(uint[] value, uint[] power, uint[] modulus)
		{
			if (value.Length > modulus.Length)
			{
				value = Remainder(value, modulus);
			}
			int size = modulus.Length + modulus.Length;
			BitsBuffer value2 = new BitsBuffer(size, value);
			return PowCore(power, modulus, ref value2);
		}

		private static uint[] PowCore(uint[] power, uint[] modulus, ref BitsBuffer value)
		{
			int size = value.GetSize();
			BitsBuffer temp = new BitsBuffer(size, 0u);
			BitsBuffer result = new BitsBuffer(size, 1u);
			if (modulus.Length < ReducerThreshold)
			{
				PowCore(power, modulus, ref value, ref result, ref temp);
			}
			else
			{
				FastReducer reducer = new FastReducer(modulus);
				PowCore(power, ref reducer, ref value, ref result, ref temp);
			}
			return result.GetBits();
		}

		private static uint[] PowCore(uint power, uint[] modulus, ref BitsBuffer value)
		{
			int size = value.GetSize();
			BitsBuffer temp = new BitsBuffer(size, 0u);
			BitsBuffer result = new BitsBuffer(size, 1u);
			if (modulus.Length < ReducerThreshold)
			{
				PowCore(power, modulus, ref value, ref result, ref temp);
			}
			else
			{
				FastReducer reducer = new FastReducer(modulus);
				PowCore(power, ref reducer, ref value, ref result, ref temp);
			}
			return result.GetBits();
		}

		private static void PowCore(uint[] power, uint[] modulus, ref BitsBuffer value, ref BitsBuffer result, ref BitsBuffer temp)
		{
			for (int i = 0; i < power.Length - 1; i++)
			{
				uint num = power[i];
				for (int j = 0; j < 32; j++)
				{
					if ((num & 1) == 1)
					{
						result.MultiplySelf(ref value, ref temp);
						result.Reduce(modulus);
					}
					value.SquareSelf(ref temp);
					value.Reduce(modulus);
					num >>= 1;
				}
			}
			PowCore(power[^1], modulus, ref value, ref result, ref temp);
		}

		private static void PowCore(uint power, uint[] modulus, ref BitsBuffer value, ref BitsBuffer result, ref BitsBuffer temp)
		{
			while (power != 0)
			{
				if ((power & 1) == 1)
				{
					result.MultiplySelf(ref value, ref temp);
					result.Reduce(modulus);
				}
				if (power != 1)
				{
					value.SquareSelf(ref temp);
					value.Reduce(modulus);
				}
				power >>= 1;
			}
		}

		private static void PowCore(uint[] power, ref FastReducer reducer, ref BitsBuffer value, ref BitsBuffer result, ref BitsBuffer temp)
		{
			for (int i = 0; i < power.Length - 1; i++)
			{
				uint num = power[i];
				for (int j = 0; j < 32; j++)
				{
					if ((num & 1) == 1)
					{
						result.MultiplySelf(ref value, ref temp);
						result.Reduce(ref reducer);
					}
					value.SquareSelf(ref temp);
					value.Reduce(ref reducer);
					num >>= 1;
				}
			}
			PowCore(power[^1], ref reducer, ref value, ref result, ref temp);
		}

		private static void PowCore(uint power, ref FastReducer reducer, ref BitsBuffer value, ref BitsBuffer result, ref BitsBuffer temp)
		{
			while (power != 0)
			{
				if ((power & 1) == 1)
				{
					result.MultiplySelf(ref value, ref temp);
					result.Reduce(ref reducer);
				}
				if (power != 1)
				{
					value.SquareSelf(ref temp);
					value.Reduce(ref reducer);
				}
				power >>= 1;
			}
		}

		private static int ActualLength(uint[] value)
		{
			return ActualLength(value, value.Length);
		}

		private static int ActualLength(uint[] value, int length)
		{
			while (length > 0 && value[length - 1] == 0)
			{
				length--;
			}
			return length;
		}

		public unsafe static uint[] Square(uint[] value)
		{
			uint[] array = new uint[value.Length + value.Length];
			fixed (uint* value2 = value)
			{
				fixed (uint* bits = array)
				{
					Square(value2, value.Length, bits, array.Length);
				}
			}
			return array;
		}

		private unsafe static void Square(uint* value, int valueLength, uint* bits, int bitsLength)
		{
			if (valueLength < SquareThreshold)
			{
				for (int i = 0; i < valueLength; i++)
				{
					ulong num = 0uL;
					for (int j = 0; j < i; j++)
					{
						ulong num2 = bits[i + j] + num;
						ulong num3 = (ulong)value[j] * (ulong)value[i];
						bits[i + j] = (uint)(num2 + (num3 << 1));
						num = num3 + (num2 >> 1) >> 31;
					}
					ulong num4 = (ulong)((long)value[i] * (long)value[i]) + num;
					bits[i + i] = (uint)num4;
					bits[i + i + 1] = (uint)(num4 >> 32);
				}
				return;
			}
			int num5 = valueLength >> 1;
			int num6 = num5 << 1;
			int num7 = num5;
			uint* ptr = value + num5;
			int num8 = valueLength - num5;
			int num9 = num6;
			uint* ptr2 = bits + num6;
			int num10 = bitsLength - num6;
			Square(value, num7, bits, num9);
			Square(ptr, num8, ptr2, num10);
			int num11 = num8 + 1;
			int num12 = num11 + num11;
			if (num12 < AllocationThreshold)
			{
				uint* ptr3 = stackalloc uint[num11];
				uint* ptr4 = stackalloc uint[num12];
				Add(ptr, num8, value, num7, ptr3, num11);
				Square(ptr3, num11, ptr4, num12);
				SubtractCore(ptr2, num10, bits, num9, ptr4, num12);
				AddSelf(bits + num5, bitsLength - num5, ptr4, num12);
				return;
			}
			fixed (uint* ptr5 = new uint[num11])
			{
				fixed (uint* ptr6 = new uint[num12])
				{
					Add(ptr, num8, value, num7, ptr5, num11);
					Square(ptr5, num11, ptr6, num12);
					SubtractCore(ptr2, num10, bits, num9, ptr6, num12);
					AddSelf(bits + num5, bitsLength - num5, ptr6, num12);
				}
			}
		}

		public static uint[] Multiply(uint[] left, uint right)
		{
			int i = 0;
			ulong num = 0uL;
			uint[] array = new uint[left.Length + 1];
			for (; i < left.Length; i++)
			{
				ulong num2 = (ulong)((long)left[i] * (long)right) + num;
				array[i] = (uint)num2;
				num = num2 >> 32;
			}
			array[i] = (uint)num;
			return array;
		}

		public unsafe static uint[] Multiply(uint[] left, uint[] right)
		{
			uint[] array = new uint[left.Length + right.Length];
			fixed (uint* left2 = left)
			{
				fixed (uint* right2 = right)
				{
					fixed (uint* bits = array)
					{
						Multiply(left2, left.Length, right2, right.Length, bits, array.Length);
					}
				}
			}
			return array;
		}

		private unsafe static void Multiply(uint* left, int leftLength, uint* right, int rightLength, uint* bits, int bitsLength)
		{
			if (rightLength < MultiplyThreshold)
			{
				for (int i = 0; i < rightLength; i++)
				{
					ulong num = 0uL;
					for (int j = 0; j < leftLength; j++)
					{
						ulong num2 = bits[i + j] + num + (ulong)((long)left[j] * (long)right[i]);
						bits[i + j] = (uint)num2;
						num = num2 >> 32;
					}
					bits[i + leftLength] = (uint)num;
				}
				return;
			}
			int num3 = rightLength >> 1;
			int num4 = num3 << 1;
			int num5 = num3;
			uint* left2 = left + num3;
			int num6 = leftLength - num3;
			int rightLength2 = num3;
			uint* ptr = right + num3;
			int num7 = rightLength - num3;
			int num8 = num4;
			uint* ptr2 = bits + num4;
			int num9 = bitsLength - num4;
			Multiply(left, num5, right, rightLength2, bits, num8);
			Multiply(left2, num6, ptr, num7, ptr2, num9);
			int num10 = num6 + 1;
			int num11 = num7 + 1;
			int num12 = num10 + num11;
			if (num12 < AllocationThreshold)
			{
				uint* ptr3 = stackalloc uint[num10];
				uint* ptr4 = stackalloc uint[num11];
				uint* ptr5 = stackalloc uint[num12];
				Add(left2, num6, left, num5, ptr3, num10);
				Add(ptr, num7, right, rightLength2, ptr4, num11);
				Multiply(ptr3, num10, ptr4, num11, ptr5, num12);
				SubtractCore(ptr2, num9, bits, num8, ptr5, num12);
				AddSelf(bits + num3, bitsLength - num3, ptr5, num12);
				return;
			}
			fixed (uint* ptr6 = new uint[num10])
			{
				fixed (uint* ptr7 = new uint[num11])
				{
					fixed (uint* ptr8 = new uint[num12])
					{
						Add(left2, num6, left, num5, ptr6, num10);
						Add(ptr, num7, right, rightLength2, ptr7, num11);
						Multiply(ptr6, num10, ptr7, num11, ptr8, num12);
						SubtractCore(ptr2, num9, bits, num8, ptr8, num12);
						AddSelf(bits + num3, bitsLength - num3, ptr8, num12);
					}
				}
			}
		}

		private unsafe static void SubtractCore(uint* left, int leftLength, uint* right, int rightLength, uint* core, int coreLength)
		{
			int i = 0;
			long num = 0L;
			for (; i < rightLength; i++)
			{
				long num2 = core[i] + num - left[i] - right[i];
				core[i] = (uint)num2;
				num = num2 >> 32;
			}
			for (; i < leftLength; i++)
			{
				long num3 = core[i] + num - left[i];
				core[i] = (uint)num3;
				num = num3 >> 32;
			}
			while (num != 0L && i < coreLength)
			{
				long num4 = core[i] + num;
				core[i] = (uint)num4;
				num = num4 >> 32;
				i++;
			}
		}
	}
	internal static class BigNumber
	{
		private struct BigNumberBuffer
		{
			public StringBuilder digits;

			public int precision;

			public int scale;

			public bool sign;

			public static BigNumberBuffer Create()
			{
				return new BigNumberBuffer
				{
					digits = new StringBuilder()
				};
			}
		}

		private const NumberStyles InvalidNumberStyles = ~(NumberStyles.Any | NumberStyles.AllowHexSpecifier);

		internal static bool TryValidateParseStyleInteger(NumberStyles style, out ArgumentException e)
		{
			if ((style & ~(NumberStyles.Any | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				e = new ArgumentException(global::SR.Format("An undefined NumberStyles value is being used.", "style"));
				return false;
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None && (style & ~NumberStyles.HexNumber) != NumberStyles.None)
			{
				e = new ArgumentException("With the AllowHexSpecifier bit set in the enum bit field, the only other valid bits that can be combined into the enum value must be a subset of those in HexNumber.");
				return false;
			}
			e = null;
			return true;
		}

		internal static bool TryParseBigInteger(string value, NumberStyles style, NumberFormatInfo info, out BigInteger result)
		{
			if (value == null)
			{
				result = default(BigInteger);
				return false;
			}
			return TryParseBigInteger(value.AsSpan(), style, info, out result);
		}

		internal static bool TryParseBigInteger(ReadOnlySpan<char> value, NumberStyles style, NumberFormatInfo info, out BigInteger result)
		{
			result = BigInteger.Zero;
			if (!TryValidateParseStyleInteger(style, out var e))
			{
				throw e;
			}
			BigNumberBuffer number = BigNumberBuffer.Create();
			if (!FormatProvider.TryStringToBigInteger(value, style, info, number.digits, out number.precision, out number.scale, out number.sign))
			{
				return false;
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (!HexNumberToBigInteger(ref number, ref result))
				{
					return false;
				}
			}
			else if (!NumberToBigInteger(ref number, ref result))
			{
				return false;
			}
			return true;
		}

		internal static BigInteger ParseBigInteger(string value, NumberStyles style, NumberFormatInfo info)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return ParseBigInteger(value.AsSpan(), style, info);
		}

		internal static BigInteger ParseBigInteger(ReadOnlySpan<char> value, NumberStyles style, NumberFormatInfo info)
		{
			if (!TryValidateParseStyleInteger(style, out var e))
			{
				throw e;
			}
			BigInteger result = BigInteger.Zero;
			if (!TryParseBigInteger(value, style, info, out result))
			{
				throw new FormatException("The value could not be parsed.");
			}
			return result;
		}

		private static bool HexNumberToBigInteger(ref BigNumberBuffer number, ref BigInteger value)
		{
			if (number.digits == null || number.digits.Length == 0)
			{
				return false;
			}
			int num = number.digits.Length - 1;
			byte[] array = new byte[num / 2 + num % 2];
			bool flag = false;
			bool flag2 = false;
			int num2 = 0;
			for (int num3 = num - 1; num3 > -1; num3--)
			{
				char c = number.digits[num3];
				byte b = ((c >= '0' && c <= '9') ? ((byte)(c - 48)) : ((c < 'A' || c > 'F') ? ((byte)(c - 97 + 10)) : ((byte)(c - 65 + 10))));
				if (num3 == 0 && (b & 8) == 8)
				{
					flag2 = true;
				}
				if (flag)
				{
					array[num2] = (byte)(array[num2] | (b << 4));
					num2++;
				}
				else
				{
					array[num2] = (flag2 ? ((byte)(b | 0xF0)) : b);
				}
				flag = !flag;
			}
			value = new BigInteger(array);
			return true;
		}

		private static bool NumberToBigInteger(ref BigNumberBuffer number, ref BigInteger value)
		{
			int num = number.scale;
			int index = 0;
			BigInteger bigInteger = 10;
			value = 0;
			while (--num >= 0)
			{
				value *= bigInteger;
				if (number.digits[index] != 0)
				{
					value += (BigInteger)(number.digits[index++] - 48);
				}
			}
			while (number.digits[index] != 0)
			{
				if (number.digits[index++] != '0')
				{
					return false;
				}
			}
			if (number.sign)
			{
				value = -value;
			}
			return true;
		}

		internal static char ParseFormatSpecifier(ReadOnlySpan<char> format, out int digits)
		{
			digits = -1;
			if (format.Length == 0)
			{
				return 'R';
			}
			int num = 0;
			char c = format[num];
			if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
			{
				num++;
				int num2 = -1;
				if (num < format.Length && format[num] >= '0' && format[num] <= '9')
				{
					num2 = format[num++] - 48;
					while (num < format.Length && format[num] >= '0' && format[num] <= '9')
					{
						num2 = num2 * 10 + (format[num++] - 48);
						if (num2 >= 10)
						{
							break;
						}
					}
				}
				if (num >= format.Length || format[num] == '\0')
				{
					digits = num2;
					return c;
				}
			}
			return '\0';
		}

		private static string FormatBigIntegerToHex(bool targetSpan, BigInteger value, char format, int digits, NumberFormatInfo info, Span<char> destination, out int charsWritten, out bool spanSuccess)
		{
			byte[] array = null;
			Span<byte> destination2 = stackalloc byte[64];
			if (!value.TryWriteOrCountBytes(destination2, out var bytesWritten))
			{
				destination2 = (array = ArrayPool<byte>.Shared.Rent(bytesWritten));
				value.TryWriteBytes(destination2, out bytesWritten);
			}
			destination2 = destination2.Slice(0, bytesWritten);
			Span<char> initialBuffer = stackalloc char[128];
			System.Text.ValueStringBuilder valueStringBuilder = new System.Text.ValueStringBuilder(initialBuffer);
			int num = destination2.Length - 1;
			if (num > -1)
			{
				bool flag = false;
				byte b = destination2[num];
				if (b > 247)
				{
					b -= 240;
					flag = true;
				}
				if (b < 8 || flag)
				{
					valueStringBuilder.Append((b < 10) ? ((char)(b + 48)) : ((format == 'X') ? ((char)((b & 0xF) - 10 + 65)) : ((char)((b & 0xF) - 10 + 97))));
					num--;
				}
			}
			if (num > -1)
			{
				Span<char> span = valueStringBuilder.AppendSpan((num + 1) * 2);
				int num2 = 0;
				string text = ((format == 'x') ? "0123456789abcdef" : "0123456789ABCDEF");
				while (num > -1)
				{
					byte b2 = destination2[num--];
					span[num2++] = text[b2 >> 4];
					span[num2++] = text[b2 & 0xF];
				}
			}
			if (digits > valueStringBuilder.Length)
			{
				valueStringBuilder.Insert(0, (value._sign >= 0) ? '0' : ((format == 'x') ? 'f' : 'F'), digits - valueStringBuilder.Length);
			}
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array);
			}
			if (targetSpan)
			{
				spanSuccess = valueStringBuilder.TryCopyTo(destination, out charsWritten);
				return null;
			}
			charsWritten = 0;
			spanSuccess = false;
			return valueStringBuilder.ToString();
		}

		internal static string FormatBigInteger(BigInteger value, string format, NumberFormatInfo info)
		{
			int charsWritten;
			bool spanSuccess;
			return FormatBigInteger(targetSpan: false, value, format, format, info, default(Span<char>), out charsWritten, out spanSuccess);
		}

		internal static bool TryFormatBigInteger(BigInteger value, ReadOnlySpan<char> format, NumberFormatInfo info, Span<char> destination, out int charsWritten)
		{
			FormatBigInteger(targetSpan: true, value, null, format, info, destination, out charsWritten, out var spanSuccess);
			return spanSuccess;
		}

		private static string FormatBigInteger(bool targetSpan, BigInteger value, string formatString, ReadOnlySpan<char> formatSpan, NumberFormatInfo info, Span<char> destination, out int charsWritten, out bool spanSuccess)
		{
			int digits = 0;
			char c = ParseFormatSpecifier(formatSpan, out digits);
			if (c == 'x' || c == 'X')
			{
				return FormatBigIntegerToHex(targetSpan, value, c, digits, info, destination, out charsWritten, out spanSuccess);
			}
			if (value._bits == null)
			{
				if (c == 'g' || c == 'G' || c == 'r' || c == 'R')
				{
					formatSpan = (formatString = ((digits > 0) ? $"D{digits}" : "D"));
				}
				if (targetSpan)
				{
					spanSuccess = value._sign.TryFormat(destination, out charsWritten, formatSpan, info);
					return null;
				}
				charsWritten = 0;
				spanSuccess = false;
				return value._sign.ToString(formatString, info);
			}
			int num = value._bits.Length;
			uint[] array;
			int num3;
			int num4;
			checked
			{
				int num2;
				try
				{
					num2 = unchecked(checked(num * 10) / 9) + 2;
				}
				catch (OverflowException innerException)
				{
					throw new FormatException("The value is too large to be represented by this format specifier.", innerException);
				}
				array = new uint[num2];
				num3 = 0;
				num4 = num;
			}
			while (--num4 >= 0)
			{
				uint num5 = value._bits[num4];
				for (int i = 0; i < num3; i++)
				{
					ulong num6 = NumericsHelpers.MakeUlong(array[i], num5);
					array[i] = (uint)(num6 % 1000000000);
					num5 = (uint)(num6 / 1000000000);
				}
				if (num5 != 0)
				{
					array[num3++] = num5 % 1000000000;
					num5 /= 1000000000;
					if (num5 != 0)
					{
						array[num3++] = num5;
					}
				}
			}
			int num7;
			bool flag;
			char[] array2;
			int num9;
			checked
			{
				try
				{
					num7 = num3 * 9;
				}
				catch (OverflowException innerException2)
				{
					throw new FormatException("The value is too large to be represented by this format specifier.", innerException2);
				}
				flag = c == 'g' || c == 'G' || c == 'd' || c == 'D' || c == 'r' || c == 'R';
				if (flag)
				{
					if (digits > 0 && digits > num7)
					{
						num7 = digits;
					}
					if (value._sign < 0)
					{
						try
						{
							num7 += info.NegativeSign.Length;
						}
						catch (OverflowException innerException3)
						{
							throw new FormatException("The value is too large to be represented by this format specifier.", innerException3);
						}
					}
				}
				int num8;
				try
				{
					num8 = num7 + 1;
				}
				catch (OverflowException innerException4)
				{
					throw new FormatException("The value is too large to be represented by this format specifier.", innerException4);
				}
				array2 = new char[num8];
				num9 = num7;
			}
			for (int j = 0; j < num3 - 1; j++)
			{
				uint num10 = array[j];
				int num11 = 9;
				while (--num11 >= 0)
				{
					array2[--num9] = (char)(48 + num10 % 10);
					num10 /= 10;
				}
			}
			for (uint num12 = array[num3 - 1]; num12 != 0; num12 /= 10)
			{
				array2[--num9] = (char)(48 + num12 % 10);
			}
			if (!flag)
			{
				bool sign = value._sign < 0;
				int precision = 29;
				int scale = num7 - num9;
				Span<char> initialBuffer = stackalloc char[128];
				System.Text.ValueStringBuilder sb = new System.Text.ValueStringBuilder(initialBuffer);
				FormatProvider.FormatBigInteger(ref sb, precision, scale, sign, formatSpan, info, array2, num9);
				if (targetSpan)
				{
					spanSuccess = sb.TryCopyTo(destination, out charsWritten);
					return null;
				}
				charsWritten = 0;
				spanSuccess = false;
				return sb.ToString();
			}
			int num13 = num7 - num9;
			while (digits > 0 && digits > num13)
			{
				array2[--num9] = '0';
				digits--;
			}
			if (value._sign < 0)
			{
				_ = info.NegativeSign;
				for (int num14 = info.NegativeSign.Length - 1; num14 > -1; num14--)
				{
					array2[--num9] = info.NegativeSign[num14];
				}
			}
			int num15 = num7 - num9;
			if (!targetSpan)
			{
				charsWritten = 0;
				spanSuccess = false;
				return new string(array2, num9, num7 - num9);
			}
			if (new ReadOnlySpan<char>(array2, num9, num7 - num9).TryCopyTo(destination))
			{
				charsWritten = num15;
				spanSuccess = true;
				return null;
			}
			charsWritten = 0;
			spanSuccess = false;
			return null;
		}
	}
	/// <summary>Represents a complex number.</summary>
	[Serializable]
	public struct Complex : IEquatable<Complex>, IFormattable
	{
		/// <summary>Returns a new <see cref="T:System.Numerics.Complex" /> instance with a real number equal to zero and an imaginary number equal to zero.</summary>
		public static readonly Complex Zero = new Complex(0.0, 0.0);

		/// <summary>Returns a new <see cref="T:System.Numerics.Complex" /> instance with a real number equal to one and an imaginary number equal to zero.</summary>
		public static readonly Complex One = new Complex(1.0, 0.0);

		/// <summary>Returns a new <see cref="T:System.Numerics.Complex" /> instance with a real number equal to zero and an imaginary number equal to one.</summary>
		public static readonly Complex ImaginaryOne = new Complex(0.0, 1.0);

		private const double InverseOfLog10 = 0.43429448190325;

		private static readonly double s_sqrtRescaleThreshold = double.MaxValue / (Math.Sqrt(2.0) + 1.0);

		private static readonly double s_asinOverflowThreshold = Math.Sqrt(double.MaxValue) / 2.0;

		private static readonly double s_log2 = Math.Log(2.0);

		private double m_real;

		private double m_imaginary;

		/// <summary>Gets the real component of the current <see cref="T:System.Numerics.Complex" /> object.</summary>
		/// <returns>The real component of a complex number.</returns>
		public double Real => m_real;

		/// <summary>Gets the imaginary component of the current <see cref="T:System.Numerics.Complex" /> object.</summary>
		/// <returns>The imaginary component of a complex number.</returns>
		public double Imaginary => m_imaginary;

		/// <summary>Gets the magnitude (or absolute value) of a complex number.</summary>
		/// <returns>The magnitude of the current instance.</returns>
		public double Magnitude => Abs(this);

		/// <summary>Gets the phase of a complex number.</summary>
		/// <returns>The phase of a complex number, in radians.</returns>
		public double Phase => Math.Atan2(m_imaginary, m_real);

		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.Complex" /> structure using the specified real and imaginary values.</summary>
		/// <param name="real">The real part of the complex number.</param>
		/// <param name="imaginary">The imaginary part of the complex number.</param>
		public Complex(double real, double imaginary)
		{
			m_real = real;
			m_imaginary = imaginary;
		}

		/// <summary>Creates a complex number from a point's polar coordinates.</summary>
		/// <param name="magnitude">The magnitude, which is the distance from the origin (the intersection of the x-axis and the y-axis) to the number.</param>
		/// <param name="phase">The phase, which is the angle from the line to the horizontal axis, measured in radians.</param>
		/// <returns>A complex number.</returns>
		public static Complex FromPolarCoordinates(double magnitude, double phase)
		{
			return new Complex(magnitude * Math.Cos(phase), magnitude * Math.Sin(phase));
		}

		/// <summary>Returns the additive inverse of a specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The result of the <see cref="P:System.Numerics.Complex.Real" /> and <see cref="P:System.Numerics.Complex.Imaginary" /> components of the <paramref name="value" /> parameter multiplied by -1.</returns>
		public static Complex Negate(Complex value)
		{
			return -value;
		}

		/// <summary>Adds two complex numbers and returns the result.</summary>
		/// <param name="left">The first complex number to add.</param>
		/// <param name="right">The second complex number to add.</param>
		/// <returns>The sum of <paramref name="left" /> and <paramref name="right" />.</returns>
		public static Complex Add(Complex left, Complex right)
		{
			return left + right;
		}

		/// <summary>Subtracts one complex number from another and returns the result.</summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
		public static Complex Subtract(Complex left, Complex right)
		{
			return left - right;
		}

		/// <summary>Returns the product of two complex numbers.</summary>
		/// <param name="left">The first complex number to multiply.</param>
		/// <param name="right">The second complex number to multiply.</param>
		/// <returns>The product of the <paramref name="left" /> and <paramref name="right" /> parameters.</returns>
		public static Complex Multiply(Complex left, Complex right)
		{
			return left * right;
		}

		/// <summary>Divides one complex number by another and returns the result.</summary>
		/// <param name="dividend">The complex number to be divided.</param>
		/// <param name="divisor">The complex number to divide by.</param>
		/// <returns>The quotient of the division.</returns>
		public static Complex Divide(Complex dividend, Complex divisor)
		{
			return dividend / divisor;
		}

		/// <summary>Returns the additive inverse of a specified complex number.</summary>
		/// <param name="value">The value to negate.</param>
		/// <returns>The result of the <see cref="P:System.Numerics.Complex.Real" /> and <see cref="P:System.Numerics.Complex.Imaginary" /> components of the <paramref name="value" /> parameter multiplied by -1.</returns>
		public static Complex operator -(Complex value)
		{
			return new Complex(0.0 - value.m_real, 0.0 - value.m_imaginary);
		}

		/// <summary>Adds two complex numbers.</summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of <paramref name="left" /> and <paramref name="right" />.</returns>
		public static Complex operator +(Complex left, Complex right)
		{
			return new Complex(left.m_real + right.m_real, left.m_imaginary + right.m_imaginary);
		}

		/// <summary>Subtracts a complex number from another complex number.</summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
		public static Complex operator -(Complex left, Complex right)
		{
			return new Complex(left.m_real - right.m_real, left.m_imaginary - right.m_imaginary);
		}

		/// <summary>Multiplies two specified complex numbers.</summary>
		/// <param name="left">The first value to multiply.</param>
		/// <param name="right">The second value to multiply.</param>
		/// <returns>The product of <paramref name="left" /> and <paramref name="right" />.</returns>
		public static Complex operator *(Complex left, Complex right)
		{
			double real = left.m_real * right.m_real - left.m_imaginary * right.m_imaginary;
			double imaginary = left.m_imaginary * right.m_real + left.m_real * right.m_imaginary;
			return new Complex(real, imaginary);
		}

		/// <summary>Divides a specified complex number by another specified complex number.</summary>
		/// <param name="left">The value to be divided.</param>
		/// <param name="right">The value to divide by.</param>
		/// <returns>The result of dividing <paramref name="left" /> by <paramref name="right" />.</returns>
		public static Complex operator /(Complex left, Complex right)
		{
			double real = left.m_real;
			double imaginary = left.m_imaginary;
			double real2 = right.m_real;
			double imaginary2 = right.m_imaginary;
			if (Math.Abs(imaginary2) < Math.Abs(real2))
			{
				double num = imaginary2 / real2;
				return new Complex((real + imaginary * num) / (real2 + imaginary2 * num), (imaginary - real * num) / (real2 + imaginary2 * num));
			}
			double num2 = real2 / imaginary2;
			return new Complex((imaginary + real * num2) / (imaginary2 + real2 * num2), (0.0 - real + imaginary * num2) / (imaginary2 + real2 * num2));
		}

		/// <summary>Gets the absolute value (or magnitude) of a complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The absolute value of <paramref name="value" />.</returns>
		public static double Abs(Complex value)
		{
			return Hypot(value.m_real, value.m_imaginary);
		}

		private static double Hypot(double a, double b)
		{
			a = Math.Abs(a);
			b = Math.Abs(b);
			double num;
			double num2;
			if (a < b)
			{
				num = a;
				num2 = b;
			}
			else
			{
				num = b;
				num2 = a;
			}
			if (num == 0.0)
			{
				return num2;
			}
			if (double.IsPositiveInfinity(num2) && !double.IsNaN(num))
			{
				return double.PositiveInfinity;
			}
			double num3 = num / num2;
			return num2 * Math.Sqrt(1.0 + num3 * num3);
		}

		private static double Log1P(double x)
		{
			double num = 1.0 + x;
			if (num == 1.0)
			{
				return x;
			}
			if (x < 0.75)
			{
				return x * Math.Log(num) / (num - 1.0);
			}
			return Math.Log(num);
		}

		/// <summary>Computes the conjugate of a complex number and returns the result.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The conjugate of <paramref name="value" />.</returns>
		public static Complex Conjugate(Complex value)
		{
			return new Complex(value.m_real, 0.0 - value.m_imaginary);
		}

		/// <summary>Returns the multiplicative inverse of a complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The reciprocal of <paramref name="value" />.</returns>
		public static Complex Reciprocal(Complex value)
		{
			if (value.m_real == 0.0 && value.m_imaginary == 0.0)
			{
				return Zero;
			}
			return One / value;
		}

		/// <summary>Returns a value that indicates whether two complex numbers are equal.</summary>
		/// <param name="left">The first complex number to compare.</param>
		/// <param name="right">The second complex number to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, <see langword="false" />.</returns>
		public static bool operator ==(Complex left, Complex right)
		{
			if (left.m_real == right.m_real)
			{
				return left.m_imaginary == right.m_imaginary;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether two complex numbers are not equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		public static bool operator !=(Complex left, Complex right)
		{
			if (left.m_real == right.m_real)
			{
				return left.m_imaginary != right.m_imaginary;
			}
			return true;
		}

		/// <summary>Returns a value that indicates whether the current instance and a specified object have the same value.</summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="obj" /> parameter is a <see cref="T:System.Numerics.Complex" /> object or a type capable of implicit conversion to a <see cref="T:System.Numerics.Complex" /> object, and its value is equal to the current <see cref="T:System.Numerics.Complex" /> object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is Complex))
			{
				return false;
			}
			return Equals((Complex)obj);
		}

		/// <summary>Returns a value that indicates whether the current instance and a specified complex number have the same value.</summary>
		/// <param name="value">The complex number to compare.</param>
		/// <returns>
		///   <see langword="true" /> if this complex number and <paramref name="value" /> have the same value; otherwise, <see langword="false" />.</returns>
		public bool Equals(Complex value)
		{
			if (m_real.Equals(value.m_real))
			{
				return m_imaginary.Equals(value.m_imaginary);
			}
			return false;
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Numerics.Complex" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			int num = 99999997;
			int num2 = m_real.GetHashCode() % num;
			int hashCode = m_imaginary.GetHashCode();
			return num2 ^ hashCode;
		}

		/// <summary>Converts the value of the current complex number to its equivalent string representation in Cartesian form.</summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "({0}, {1})", m_real, m_imaginary);
		}

		/// <summary>Converts the value of the current complex number to its equivalent string representation in Cartesian form by using the specified format for its real and imaginary parts.</summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid format string.</exception>
		public string ToString(string format)
		{
			return string.Format(CultureInfo.CurrentCulture, "({0}, {1})", m_real.ToString(format, CultureInfo.CurrentCulture), m_imaginary.ToString(format, CultureInfo.CurrentCulture));
		}

		/// <summary>Converts the value of the current complex number to its equivalent string representation in Cartesian form by using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified by <paramref name="provider" />.</returns>
		public string ToString(IFormatProvider provider)
		{
			return string.Format(provider, "({0}, {1})", m_real, m_imaginary);
		}

		/// <summary>Converts the value of the current complex number to its equivalent string representation in Cartesian form by using the specified format and culture-specific format information for its real and imaginary parts.</summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid format string.</exception>
		public string ToString(string format, IFormatProvider provider)
		{
			return string.Format(provider, "({0}, {1})", m_real.ToString(format, provider), m_imaginary.ToString(format, provider));
		}

		/// <summary>Returns the sine of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The sine of <paramref name="value" />.</returns>
		public static Complex Sin(Complex value)
		{
			double num = Math.Exp(value.m_imaginary);
			double num2 = 1.0 / num;
			double num3 = (num - num2) * 0.5;
			double num4 = (num + num2) * 0.5;
			return new Complex(Math.Sin(value.m_real) * num4, Math.Cos(value.m_real) * num3);
		}

		/// <summary>Returns the hyperbolic sine of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The hyperbolic sine of <paramref name="value" />.</returns>
		public static Complex Sinh(Complex value)
		{
			Complex complex = Sin(new Complex(0.0 - value.m_imaginary, value.m_real));
			return new Complex(complex.m_imaginary, 0.0 - complex.m_real);
		}

		/// <summary>Returns the angle that is the arc sine of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The angle which is the arc sine of <paramref name="value" />.</returns>
		public static Complex Asin(Complex value)
		{
			Asin_Internal(Math.Abs(value.Real), Math.Abs(value.Imaginary), out var b, out var bPrime, out var v);
			double num = ((!(bPrime < 0.0)) ? Math.Atan(bPrime) : Math.Asin(b));
			if (value.Real < 0.0)
			{
				num = 0.0 - num;
			}
			if (value.Imaginary < 0.0)
			{
				v = 0.0 - v;
			}
			return new Complex(num, v);
		}

		/// <summary>Returns the cosine of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The cosine of <paramref name="value" />.</returns>
		public static Complex Cos(Complex value)
		{
			double num = Math.Exp(value.m_imaginary);
			double num2 = 1.0 / num;
			double num3 = (num - num2) * 0.5;
			double num4 = (num + num2) * 0.5;
			return new Complex(Math.Cos(value.m_real) * num4, (0.0 - Math.Sin(value.m_real)) * num3);
		}

		/// <summary>Returns the hyperbolic cosine of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The hyperbolic cosine of <paramref name="value" />.</returns>
		public static Complex Cosh(Complex value)
		{
			return Cos(new Complex(0.0 - value.m_imaginary, value.m_real));
		}

		/// <summary>Returns the angle that is the arc cosine of the specified complex number.</summary>
		/// <param name="value">A complex number that represents a cosine.</param>
		/// <returns>The angle, measured in radians, which is the arc cosine of <paramref name="value" />.</returns>
		public static Complex Acos(Complex value)
		{
			Asin_Internal(Math.Abs(value.Real), Math.Abs(value.Imaginary), out var b, out var bPrime, out var v);
			double num = ((!(bPrime < 0.0)) ? Math.Atan(1.0 / bPrime) : Math.Acos(b));
			if (value.Real < 0.0)
			{
				num = Math.PI - num;
			}
			if (value.Imaginary > 0.0)
			{
				v = 0.0 - v;
			}
			return new Complex(num, v);
		}

		/// <summary>Returns the tangent of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The tangent of <paramref name="value" />.</returns>
		public static Complex Tan(Complex value)
		{
			double num = 2.0 * value.m_real;
			double num2 = 2.0 * value.m_imaginary;
			double num3 = Math.Exp(num2);
			double num4 = 1.0 / num3;
			double num5 = (num3 + num4) * 0.5;
			if (Math.Abs(value.m_imaginary) <= 4.0)
			{
				double num6 = (num3 - num4) * 0.5;
				double num7 = Math.Cos(num) + num5;
				return new Complex(Math.Sin(num) / num7, num6 / num7);
			}
			double num8 = 1.0 + Math.Cos(num) / num5;
			return new Complex(Math.Sin(num) / num5 / num8, Math.Tanh(num2) / num8);
		}

		/// <summary>Returns the hyperbolic tangent of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The hyperbolic tangent of <paramref name="value" />.</returns>
		public static Complex Tanh(Complex value)
		{
			Complex complex = Tan(new Complex(0.0 - value.m_imaginary, value.m_real));
			return new Complex(complex.m_imaginary, 0.0 - complex.m_real);
		}

		/// <summary>Returns the angle that is the arc tangent of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The angle that is the arc tangent of <paramref name="value" />.</returns>
		public static Complex Atan(Complex value)
		{
			Complex complex = new Complex(2.0, 0.0);
			return ImaginaryOne / complex * (Log(One - ImaginaryOne * value) - Log(One + ImaginaryOne * value));
		}

		private static void Asin_Internal(double x, double y, out double b, out double bPrime, out double v)
		{
			if (x > s_asinOverflowThreshold || y > s_asinOverflowThreshold)
			{
				b = -1.0;
				bPrime = x / y;
				double num;
				double num2;
				if (x < y)
				{
					num = x;
					num2 = y;
				}
				else
				{
					num = y;
					num2 = x;
				}
				double num3 = num / num2;
				v = s_log2 + Math.Log(num2) + 0.5 * Log1P(num3 * num3);
				return;
			}
			double num4 = Hypot(x + 1.0, y);
			double num5 = Hypot(x - 1.0, y);
			double num6 = (num4 + num5) * 0.5;
			b = x / num6;
			if (b > 0.75)
			{
				if (x <= 1.0)
				{
					double num7 = (y * y / (num4 + (x + 1.0)) + (num5 + (1.0 - x))) * 0.5;
					bPrime = x / Math.Sqrt((num6 + x) * num7);
				}
				else
				{
					double num8 = (1.0 / (num4 + (x + 1.0)) + 1.0 / (num5 + (x - 1.0))) * 0.5;
					bPrime = x / y / Math.Sqrt((num6 + x) * num8);
				}
			}
			else
			{
				bPrime = -1.0;
			}
			if (num6 < 1.5)
			{
				if (x < 1.0)
				{
					double num9 = (1.0 / (num4 + (x + 1.0)) + 1.0 / (num5 + (1.0 - x))) * 0.5;
					double num10 = y * y * num9;
					v = Log1P(num10 + y * Math.Sqrt(num9 * (num6 + 1.0)));
				}
				else
				{
					double num11 = (y * y / (num4 + (x + 1.0)) + (num5 + (x - 1.0))) * 0.5;
					v = Log1P(num11 + Math.Sqrt(num11 * (num6 + 1.0)));
				}
			}
			else
			{
				v = Math.Log(num6 + Math.Sqrt((num6 - 1.0) * (num6 + 1.0)));
			}
		}

		/// <summary>Returns the natural (base <see langword="e" />) logarithm of a specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The natural (base <see langword="e" />) logarithm of <paramref name="value" />.</returns>
		public static Complex Log(Complex value)
		{
			return new Complex(Math.Log(Abs(value)), Math.Atan2(value.m_imaginary, value.m_real));
		}

		/// <summary>Returns the logarithm of a specified complex number in a specified base.</summary>
		/// <param name="value">A complex number.</param>
		/// <param name="baseValue">The base of the logarithm.</param>
		/// <returns>The logarithm of <paramref name="value" /> in base <paramref name="baseValue" />.</returns>
		public static Complex Log(Complex value, double baseValue)
		{
			return Log(value) / Log(baseValue);
		}

		/// <summary>Returns the base-10 logarithm of a specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The base-10 logarithm of <paramref name="value" />.</returns>
		public static Complex Log10(Complex value)
		{
			return Scale(Log(value), 0.43429448190325);
		}

		/// <summary>Returns <see langword="e" /> raised to the power specified by a complex number.</summary>
		/// <param name="value">A complex number that specifies a power.</param>
		/// <returns>The number <see langword="e" /> raised to the power <paramref name="value" />.</returns>
		public static Complex Exp(Complex value)
		{
			double num = Math.Exp(value.m_real);
			double real = num * Math.Cos(value.m_imaginary);
			double imaginary = num * Math.Sin(value.m_imaginary);
			return new Complex(real, imaginary);
		}

		/// <summary>Returns the square root of a specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The square root of <paramref name="value" />.</returns>
		public static Complex Sqrt(Complex value)
		{
			if (value.m_imaginary == 0.0)
			{
				if (value.m_real < 0.0)
				{
					return new Complex(0.0, Math.Sqrt(0.0 - value.m_real));
				}
				return new Complex(Math.Sqrt(value.m_real), 0.0);
			}
			bool flag = false;
			if (Math.Abs(value.m_real) >= s_sqrtRescaleThreshold || Math.Abs(value.m_imaginary) >= s_sqrtRescaleThreshold)
			{
				if (double.IsInfinity(value.m_imaginary) && !double.IsNaN(value.m_real))
				{
					return new Complex(double.PositiveInfinity, value.m_imaginary);
				}
				value.m_real *= 0.25;
				value.m_imaginary *= 0.25;
				flag = true;
			}
			double num;
			double num2;
			if (value.m_real >= 0.0)
			{
				num = Math.Sqrt((Hypot(value.m_real, value.m_imaginary) + value.m_real) * 0.5);
				num2 = value.m_imaginary / (2.0 * num);
			}
			else
			{
				num2 = Math.Sqrt((Hypot(value.m_real, value.m_imaginary) - value.m_real) * 0.5);
				if (value.m_imaginary < 0.0)
				{
					num2 = 0.0 - num2;
				}
				num = value.m_imaginary / (2.0 * num2);
			}
			if (flag)
			{
				num *= 2.0;
				num2 *= 2.0;
			}
			return new Complex(num, num2);
		}

		/// <summary>Returns a specified complex number raised to a power specified by a complex number.</summary>
		/// <param name="value">A complex number to be raised to a power.</param>
		/// <param name="power">A complex number that specifies a power.</param>
		/// <returns>The complex number <paramref name="value" /> raised to the power <paramref name="power" />.</returns>
		public static Complex Pow(Complex value, Complex power)
		{
			if (power == Zero)
			{
				return One;
			}
			if (value == Zero)
			{
				return Zero;
			}
			double real = value.m_real;
			double imaginary = value.m_imaginary;
			double real2 = power.m_real;
			double imaginary2 = power.m_imaginary;
			double num = Abs(value);
			double num2 = Math.Atan2(imaginary, real);
			double num3 = real2 * num2 + imaginary2 * Math.Log(num);
			double num4 = Math.Pow(num, real2) * Math.Pow(Math.E, (0.0 - imaginary2) * num2);
			return new Complex(num4 * Math.Cos(num3), num4 * Math.Sin(num3));
		}

		/// <summary>Returns a specified complex number raised to a power specified by a double-precision floating-point number.</summary>
		/// <param name="value">A complex number to be raised to a power.</param>
		/// <param name="power">A double-precision floating-point number that specifies a power.</param>
		/// <returns>The complex number <paramref name="value" /> raised to the power <paramref name="power" />.</returns>
		public static Complex Pow(Complex value, double power)
		{
			return Pow(value, new Complex(power, 0.0));
		}

		private static Complex Scale(Complex value, double factor)
		{
			double real = factor * value.m_real;
			double imaginary = factor * value.m_imaginary;
			return new Complex(real, imaginary);
		}

		/// <summary>Defines an implicit conversion of a 16-bit signed integer to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		public static implicit operator Complex(short value)
		{
			return new Complex(value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a 32-bit signed integer to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		public static implicit operator Complex(int value)
		{
			return new Complex(value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a 64-bit signed integer to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		public static implicit operator Complex(long value)
		{
			return new Complex(value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a 16-bit unsigned integer to a complex number.   
		/// This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		[CLSCompliant(false)]
		public static implicit operator Complex(ushort value)
		{
			return new Complex((int)value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a 32-bit unsigned integer to a complex number.   
		/// This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		[CLSCompliant(false)]
		public static implicit operator Complex(uint value)
		{
			return new Complex(value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a 64-bit unsigned integer to a complex number.   
		/// This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		[CLSCompliant(false)]
		public static implicit operator Complex(ulong value)
		{
			return new Complex(value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a signed byte to a complex number.   
		/// This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		[CLSCompliant(false)]
		public static implicit operator Complex(sbyte value)
		{
			return new Complex(value, 0.0);
		}

		/// <summary>Defines an implicit conversion of an unsigned byte to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		public static implicit operator Complex(byte value)
		{
			return new Complex((int)value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a single-precision floating-point number to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		public static implicit operator Complex(float value)
		{
			return new Complex(value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a double-precision floating-point number to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		public static implicit operator Complex(double value)
		{
			return new Complex(value, 0.0);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> value to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>A complex number that has a real component equal to <paramref name="value" /> and an imaginary component equal to zero.</returns>
		public static explicit operator Complex(BigInteger value)
		{
			return new Complex((double)value, 0.0);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> value to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>A complex number that has a real component equal to <paramref name="value" /> and an imaginary component equal to zero.</returns>
		public static explicit operator Complex(decimal value)
		{
			return new Complex((double)value, 0.0);
		}
	}
	[StructLayout(LayoutKind.Explicit)]
	internal struct DoubleUlong
	{
		[FieldOffset(0)]
		public double dbl;

		[FieldOffset(0)]
		public ulong uu;
	}
	internal static class NumericsHelpers
	{
		private const int kcbitUint = 32;

		public static void GetDoubleParts(double dbl, out int sign, out int exp, out ulong man, out bool fFinite)
		{
			DoubleUlong doubleUlong = default(DoubleUlong);
			doubleUlong.uu = 0uL;
			doubleUlong.dbl = dbl;
			sign = 1 - ((int)(doubleUlong.uu >> 62) & 2);
			man = doubleUlong.uu & 0xFFFFFFFFFFFFFL;
			exp = (int)(doubleUlong.uu >> 52) & 0x7FF;
			if (exp == 0)
			{
				fFinite = true;
				if (man != 0L)
				{
					exp = -1074;
				}
			}
			else if (exp == 2047)
			{
				fFinite = false;
				exp = int.MaxValue;
			}
			else
			{
				fFinite = true;
				man |= 4503599627370496uL;
				exp -= 1075;
			}
		}

		public static double GetDoubleFromParts(int sign, int exp, ulong man)
		{
			DoubleUlong doubleUlong = default(DoubleUlong);
			doubleUlong.dbl = 0.0;
			if (man == 0L)
			{
				doubleUlong.uu = 0uL;
			}
			else
			{
				int num = CbitHighZero(man) - 11;
				man = ((num >= 0) ? (man << num) : (man >> -num));
				exp -= num;
				exp += 1075;
				if (exp >= 2047)
				{
					doubleUlong.uu = 9218868437227405312uL;
				}
				else if (exp <= 0)
				{
					exp--;
					if (exp < -52)
					{
						doubleUlong.uu = 0uL;
					}
					else
					{
						doubleUlong.uu = man >> -exp;
					}
				}
				else
				{
					doubleUlong.uu = (man & 0xFFFFFFFFFFFFFL) | (ulong)((long)exp << 52);
				}
			}
			if (sign < 0)
			{
				doubleUlong.uu |= 9223372036854775808uL;
			}
			return doubleUlong.dbl;
		}

		public static void DangerousMakeTwosComplement(uint[] d)
		{
			if (d != null && d.Length != 0)
			{
				d[0] = ~d[0] + 1;
				int i;
				for (i = 1; d[i - 1] == 0 && i < d.Length; i++)
				{
					d[i] = ~d[i] + 1;
				}
				for (; i < d.Length; i++)
				{
					d[i] = ~d[i];
				}
			}
		}

		public static ulong MakeUlong(uint uHi, uint uLo)
		{
			return ((ulong)uHi << 32) | uLo;
		}

		public static uint Abs(int a)
		{
			uint num = (uint)(a >> 31);
			return ((uint)a ^ num) - num;
		}

		public static uint CombineHash(uint u1, uint u2)
		{
			return ((u1 << 7) | (u1 >> 25)) ^ u2;
		}

		public static int CombineHash(int n1, int n2)
		{
			return (int)CombineHash((uint)n1, (uint)n2);
		}

		public static int CbitHighZero(uint u)
		{
			if (u == 0)
			{
				return 32;
			}
			int num = 0;
			if ((u & 0xFFFF0000u) == 0)
			{
				num += 16;
				u <<= 16;
			}
			if ((u & 0xFF000000u) == 0)
			{
				num += 8;
				u <<= 8;
			}
			if ((u & 0xF0000000u) == 0)
			{
				num += 4;
				u <<= 4;
			}
			if ((u & 0xC0000000u) == 0)
			{
				num += 2;
				u <<= 2;
			}
			if ((u & 0x80000000u) == 0)
			{
				num++;
			}
			return num;
		}

		public static int CbitHighZero(ulong uu)
		{
			if ((uu & 0xFFFFFFFF00000000uL) == 0L)
			{
				return 32 + CbitHighZero((uint)uu);
			}
			return CbitHighZero((uint)(uu >> 32));
		}
	}
}
namespace System.Numerics.Hashing
{
	internal static class HashHelpers
	{
		public static readonly int RandomSeed = Guid.NewGuid().GetHashCode();

		public static int Combine(int h1, int h2)
		{
			return (((h1 << 5) | (h1 >>> 27)) + h1) ^ h2;
		}
	}
}
namespace System.Globalization
{
	internal class FormatProvider
	{
		private class Number
		{
			internal struct NumberBuffer
			{
				public int precision;

				public int scale;

				public bool sign;

				public unsafe char* overrideDigits;

				public unsafe char* digits => overrideDigits;
			}

			private const int NumberMaxDigits = 32;

			internal const int DECIMAL_PRECISION = 29;

			private const int MIN_SB_BUFFER_SIZE = 105;

			private static string[] s_posCurrencyFormats = new string[4] { "$#", "#$", "$ #", "# $" };

			private static string[] s_negCurrencyFormats = new string[16]
			{
				"($#)", "-$#", "$-#", "$#-", "(#$)", "-#$", "#-$", "#$-", "-# $", "-$ #",
				"# $-", "$ #-", "$ -#", "#- $", "($ #)", "(# $)"
			};

			private static string[] s_posPercentFormats = new string[4] { "# %", "#%", "%#", "% #" };

			private static string[] s_negPercentFormats = new string[12]
			{
				"-# %", "-#%", "-%#", "%-#", "%#-", "#-%", "#%-", "-% #", "# %-", "% #-",
				"% -#", "#- %"
			};

			private static string[] s_negNumberFormats = new string[5] { "(#)", "-#", "- #", "#-", "# -" };

			private static string s_posNumberFormat = "#";

			private Number()
			{
			}

			private static bool IsWhite(char ch)
			{
				if (ch != ' ')
				{
					if (ch >= '\t')
					{
						return ch <= '\r';
					}
					return false;
				}
				return true;
			}

			private unsafe static char* MatchChars(char* p, char* pEnd, string str)
			{
				fixed (char* str2 = str)
				{
					return MatchChars(p, pEnd, str2);
				}
			}

			private unsafe static char* MatchChars(char* p, char* pEnd, char* str)
			{
				if (*str == '\0')
				{
					return null;
				}
				while (true)
				{
					char c = ((p < pEnd) ? (*p) : '\0');
					if (c != *str && (*str != '\u00a0' || c != ' '))
					{
						break;
					}
					p++;
					str++;
					if (*str == '\0')
					{
						return p;
					}
				}
				return null;
			}

			private unsafe static bool ParseNumber(ref char* str, char* strEnd, NumberStyles options, ref NumberBuffer number, StringBuilder sb, NumberFormatInfo numfmt, bool parseDecimal)
			{
				number.scale = 0;
				number.sign = false;
				string text = null;
				bool flag = false;
				string str2;
				string str3;
				if ((options & NumberStyles.AllowCurrencySymbol) != NumberStyles.None)
				{
					text = numfmt.CurrencySymbol;
					str2 = numfmt.CurrencyDecimalSeparator;
					str3 = numfmt.CurrencyGroupSeparator;
					flag = true;
				}
				else
				{
					str2 = numfmt.NumberDecimalSeparator;
					str3 = numfmt.NumberGroupSeparator;
				}
				int num = 0;
				bool flag2 = sb != null;
				int num2 = (flag2 ? int.MaxValue : 32);
				char* ptr = str;
				char c = ((ptr < strEnd) ? (*ptr) : '\0');
				char* digits = number.digits;
				while (true)
				{
					if (!IsWhite(c) || (options & NumberStyles.AllowLeadingWhite) == 0 || ((num & 1) != 0 && (num & 0x20) == 0 && numfmt.NumberNegativePattern != 2))
					{
						char* ptr2;
						if ((options & NumberStyles.AllowLeadingSign) != NumberStyles.None && (num & 1) == 0 && ((ptr2 = MatchChars(ptr, strEnd, numfmt.PositiveSign)) != null || ((ptr2 = MatchChars(ptr, strEnd, numfmt.NegativeSign)) != null && (number.sign = true))))
						{
							num |= 1;
							ptr = ptr2 - 1;
						}
						else if (c == '(' && (options & NumberStyles.AllowParentheses) != NumberStyles.None && (num & 1) == 0)
						{
							num |= 3;
							number.sign = true;
						}
						else
						{
							if (text == null || (ptr2 = MatchChars(ptr, strEnd, text)) == null)
							{
								break;
							}
							num |= 0x20;
							text = null;
							ptr = ptr2 - 1;
						}
					}
					c = ((++ptr < strEnd) ? (*ptr) : '\0');
				}
				int num3 = 0;
				int num4 = 0;
				while (true)
				{
					char* ptr2;
					if ((c >= '0' && c <= '9') || ((options & NumberStyles.AllowHexSpecifier) != NumberStyles.None && ((c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))))
					{
						num |= 4;
						if (c != '0' || (num & 8) != 0 || (flag2 && (options & NumberStyles.AllowHexSpecifier) != NumberStyles.None))
						{
							if (num3 < num2)
							{
								if (flag2)
								{
									sb.Append(c);
								}
								else
								{
									digits[num3++] = c;
								}
								if (c != '0' || parseDecimal)
								{
									num4 = num3;
								}
							}
							if ((num & 0x10) == 0)
							{
								number.scale++;
							}
							num |= 8;
						}
						else if ((num & 0x10) != 0)
						{
							number.scale--;
						}
					}
					else if ((options & NumberStyles.AllowDecimalPoint) != NumberStyles.None && (num & 0x10) == 0 && ((ptr2 = MatchChars(ptr, strEnd, str2)) != null || (flag && (num & 0x20) == 0 && (ptr2 = MatchChars(ptr, strEnd, numfmt.NumberDecimalSeparator)) != null)))
					{
						num |= 0x10;
						ptr = ptr2 - 1;
					}
					else
					{
						if ((options & NumberStyles.AllowThousands) == 0 || (num & 4) == 0 || (num & 0x10) != 0 || ((ptr2 = MatchChars(ptr, strEnd, str3)) == null && (!flag || (num & 0x20) != 0 || (ptr2 = MatchChars(ptr, strEnd, numfmt.NumberGroupSeparator)) == null)))
						{
							break;
						}
						ptr = ptr2 - 1;
					}
					c = ((++ptr < strEnd) ? (*ptr) : '\0');
				}
				bool flag3 = false;
				number.precision = num4;
				if (flag2)
				{
					sb.Append('\0');
				}
				else
				{
					digits[num4] = '\0';
				}
				if ((num & 4) != 0)
				{
					if ((c == 'E' || c == 'e') && (options & NumberStyles.AllowExponent) != NumberStyles.None)
					{
						char* ptr3 = ptr;
						c = ((++ptr < strEnd) ? (*ptr) : '\0');
						char* ptr2;
						if ((ptr2 = MatchChars(ptr, strEnd, numfmt.PositiveSign)) != null)
						{
							c = (((ptr = ptr2) < strEnd) ? (*ptr) : '\0');
						}
						else if ((ptr2 = MatchChars(ptr, strEnd, numfmt.NegativeSign)) != null)
						{
							c = (((ptr = ptr2) < strEnd) ? (*ptr) : '\0');
							flag3 = true;
						}
						if (c >= '0' && c <= '9')
						{
							int num5 = 0;
							do
							{
								num5 = num5 * 10 + (c - 48);
								c = ((++ptr < strEnd) ? (*ptr) : '\0');
								if (num5 > 1000)
								{
									num5 = 9999;
									while (c >= '0' && c <= '9')
									{
										c = ((++ptr < strEnd) ? (*ptr) : '\0');
									}
								}
							}
							while (c >= '0' && c <= '9');
							if (flag3)
							{
								num5 = -num5;
							}
							number.scale += num5;
						}
						else
						{
							ptr = ptr3;
							c = ((ptr < strEnd) ? (*ptr) : '\0');
						}
					}
					while (true)
					{
						if (!IsWhite(c) || (options & NumberStyles.AllowTrailingWhite) == 0)
						{
							char* ptr2;
							if ((options & NumberStyles.AllowTrailingSign) != NumberStyles.None && (num & 1) == 0 && ((ptr2 = MatchChars(ptr, strEnd, numfmt.PositiveSign)) != null || ((ptr2 = MatchChars(ptr, strEnd, numfmt.NegativeSign)) != null && (number.sign = true))))
							{
								num |= 1;
								ptr = ptr2 - 1;
							}
							else if (c == ')' && (num & 2) != 0)
							{
								num &= -3;
							}
							else
							{
								if (text == null || (ptr2 = MatchChars(ptr, strEnd, text)) == null)
								{
									break;
								}
								text = null;
								ptr = ptr2 - 1;
							}
						}
						c = ((++ptr < strEnd) ? (*ptr) : '\0');
					}
					if ((num & 2) == 0)
					{
						if ((num & 8) == 0)
						{
							if (!parseDecimal)
							{
								number.scale = 0;
							}
							if ((num & 0x10) == 0)
							{
								number.sign = false;
							}
						}
						str = ptr;
						return true;
					}
				}
				str = ptr;
				return false;
			}

			private static bool TrailingZeros(ReadOnlySpan<char> s, int index)
			{
				for (int i = index; i < s.Length; i++)
				{
					if (s[i] != 0)
					{
						return false;
					}
				}
				return true;
			}

			internal unsafe static bool TryStringToNumber(ReadOnlySpan<char> str, NumberStyles options, ref NumberBuffer number, StringBuilder sb, NumberFormatInfo numfmt, bool parseDecimal)
			{
				fixed (char* reference = &MemoryMarshal.GetReference(str))
				{
					char* str2 = reference;
					if (!ParseNumber(ref str2, str2 + str.Length, options, ref number, sb, numfmt, parseDecimal) || (str2 - reference < str.Length && !TrailingZeros(str, (int)(str2 - reference))))
					{
						return false;
					}
				}
				return true;
			}

			internal unsafe static void Int32ToDecChars(char* buffer, ref int index, uint value, int digits)
			{
				while (--digits >= 0 || value != 0)
				{
					buffer[--index] = (char)(value % 10 + 48);
					value /= 10;
				}
			}

			internal static char ParseFormatSpecifier(ReadOnlySpan<char> format, out int digits)
			{
				char c = '\0';
				if (format.Length > 0)
				{
					c = format[0];
					if ((uint)(c - 65) <= 25u || (uint)(c - 97) <= 25u)
					{
						if (format.Length == 1)
						{
							digits = -1;
							return c;
						}
						if (format.Length == 2)
						{
							int num = format[1] - 48;
							if ((uint)num < 10u)
							{
								digits = num;
								return c;
							}
						}
						else if (format.Length == 3)
						{
							int num2 = format[1] - 48;
							int num3 = format[2] - 48;
							if ((uint)num2 < 10u && (uint)num3 < 10u)
							{
								digits = num2 * 10 + num3;
								return c;
							}
						}
						int num4 = 0;
						int num5 = 1;
						while (num5 < format.Length && (uint)(format[num5] - 48) < 10u && num4 < 10)
						{
							num4 = num4 * 10 + format[num5++] - 48;
						}
						if (num5 == format.Length || format[num5] == '\0')
						{
							digits = num4;
							return c;
						}
					}
				}
				digits = -1;
				if (format.Length != 0 && c != 0)
				{
					return '\0';
				}
				return 'G';
			}

			internal unsafe static void NumberToString(ref System.Text.ValueStringBuilder sb, ref NumberBuffer number, char format, int nMaxDigits, NumberFormatInfo info, bool isDecimal)
			{
				int num = -1;
				switch (format)
				{
				case 'C':
				case 'c':
					num = ((nMaxDigits >= 0) ? nMaxDigits : info.CurrencyDecimalDigits);
					if (nMaxDigits < 0)
					{
						nMaxDigits = info.CurrencyDecimalDigits;
					}
					RoundNumber(ref number, number.scale + nMaxDigits);
					FormatCurrency(ref sb, ref number, num, nMaxDigits, info);
					break;
				case 'F':
				case 'f':
					if (nMaxDigits < 0)
					{
						nMaxDigits = (num = info.NumberDecimalDigits);
					}
					else
					{
						num = nMaxDigits;
					}
					RoundNumber(ref number, number.scale + nMaxDigits);
					if (number.sign)
					{
						sb.Append(info.NegativeSign);
					}
					FormatFixed(ref sb, ref number, num, nMaxDigits, info, null, info.NumberDecimalSeparator, null);
					break;
				case 'N':
				case 'n':
					if (nMaxDigits < 0)
					{
						nMaxDigits = (num = info.NumberDecimalDigits);
					}
					else
					{
						num = nMaxDigits;
					}
					RoundNumber(ref number, number.scale + nMaxDigits);
					FormatNumber(ref sb, ref number, num, nMaxDigits, info);
					break;
				case 'E':
				case 'e':
					if (nMaxDigits < 0)
					{
						nMaxDigits = (num = 6);
					}
					else
					{
						num = nMaxDigits;
					}
					nMaxDigits++;
					RoundNumber(ref number, nMaxDigits);
					if (number.sign)
					{
						sb.Append(info.NegativeSign);
					}
					FormatScientific(ref sb, ref number, num, nMaxDigits, info, format);
					break;
				case 'G':
				case 'g':
				{
					bool flag = true;
					if (nMaxDigits < 1)
					{
						if (isDecimal && nMaxDigits == -1)
						{
							nMaxDigits = (num = 29);
							flag = false;
						}
						else
						{
							nMaxDigits = (num = number.precision);
						}
					}
					else
					{
						num = nMaxDigits;
					}
					if (flag)
					{
						RoundNumber(ref number, nMaxDigits);
					}
					else if (isDecimal && *number.digits == '\0')
					{
						number.sign = false;
					}
					if (number.sign)
					{
						sb.Append(info.NegativeSign);
					}
					FormatGeneral(ref sb, ref number, num, nMaxDigits, info, (char)(format - 2), !flag);
					break;
				}
				case 'P':
				case 'p':
					if (nMaxDigits < 0)
					{
						nMaxDigits = (num = info.PercentDecimalDigits);
					}
					else
					{
						num = nMaxDigits;
					}
					number.scale += 2;
					RoundNumber(ref number, number.scale + nMaxDigits);
					FormatPercent(ref sb, ref number, num, nMaxDigits, info);
					break;
				default:
					throw new FormatException("Format specifier was invalid.");
				}
			}

			private static void FormatCurrency(ref System.Text.ValueStringBuilder sb, ref NumberBuffer number, int nMinDigits, int nMaxDigits, NumberFormatInfo info)
			{
				string text = (number.sign ? s_negCurrencyFormats[info.CurrencyNegativePattern] : s_posCurrencyFormats[info.CurrencyPositivePattern]);
				foreach (char c in text)
				{
					switch (c)
					{
					case '#':
						FormatFixed(ref sb, ref number, nMinDigits, nMaxDigits, info, info.CurrencyGroupSizes, info.CurrencyDecimalSeparator, info.CurrencyGroupSeparator);
						break;
					case '-':
						sb.Append(info.NegativeSign);
						break;
					case '$':
						sb.Append(info.CurrencySymbol);
						break;
					default:
						sb.Append(c);
						break;
					}
				}
			}

			private unsafe static int wcslen(char* s)
			{
				int num = 0;
				while (*(s++) != 0)
				{
					num++;
				}
				return num;
			}

			private unsafe static void FormatFixed(ref System.Text.ValueStringBuilder sb, ref NumberBuffer number, int nMinDigits, int nMaxDigits, NumberFormatInfo info, int[] groupDigits, string sDecimal, string sGroup)
			{
				int scale = number.scale;
				char* ptr = number.digits;
				int num = wcslen(ptr);
				if (scale > 0)
				{
					if (groupDigits != null)
					{
						int num2 = 0;
						int num3 = groupDigits[num2];
						int num4 = groupDigits.Length;
						int num5 = scale;
						int length = sGroup.Length;
						int num6 = 0;
						if (num4 != 0)
						{
							while (scale > num3 && groupDigits[num2] != 0)
							{
								num5 += length;
								if (num2 < num4 - 1)
								{
									num2++;
								}
								num3 += groupDigits[num2];
								if (num3 < 0 || num5 < 0)
								{
									throw new ArgumentOutOfRangeException();
								}
							}
							num6 = ((num3 != 0) ? groupDigits[0] : 0);
						}
						char* ptr2 = stackalloc char[num5];
						num2 = 0;
						int num7 = 0;
						int num8 = ((scale < num) ? scale : num);
						char* ptr3 = ptr2 + num5 - 1;
						for (int num9 = scale - 1; num9 >= 0; num9--)
						{
							*(ptr3--) = ((num9 < num8) ? ptr[num9] : '0');
							if (num6 > 0)
							{
								num7++;
								if (num7 == num6 && num9 != 0)
								{
									for (int num10 = length - 1; num10 >= 0; num10--)
									{
										*(ptr3--) = sGroup[num10];
									}
									if (num2 < num4 - 1)
									{
										num2++;
										num6 = groupDigits[num2];
									}
									num7 = 0;
								}
							}
						}
						sb.Append(ptr2, num5);
						ptr += num8;
					}
					else
					{
						int num11 = Math.Min(num, scale);
						sb.Append(ptr, num11);
						ptr += num11;
						if (scale > num)
						{
							sb.Append('0', scale - num);
						}
					}
				}
				else
				{
					sb.Append('0');
				}
				if (nMaxDigits > 0)
				{
					sb.Append(sDecimal);
					if (scale < 0 && nMaxDigits > 0)
					{
						int num12 = Math.Min(-scale, nMaxDigits);
						sb.Append('0', num12);
						scale += num12;
						nMaxDigits -= num12;
					}
					while (nMaxDigits > 0)
					{
						sb.Append((*ptr != 0) ? (*(ptr++)) : '0');
						nMaxDigits--;
					}
				}
			}

			private static void FormatNumber(ref System.Text.ValueStringBuilder sb, ref NumberBuffer number, int nMinDigits, int nMaxDigits, NumberFormatInfo info)
			{
				string text = (number.sign ? s_negNumberFormats[info.NumberNegativePattern] : s_posNumberFormat);
				foreach (char c in text)
				{
					switch (c)
					{
					case '#':
						FormatFixed(ref sb, ref number, nMinDigits, nMaxDigits, info, info.NumberGroupSizes, info.NumberDecimalSeparator, info.NumberGroupSeparator);
						break;
					case '-':
						sb.Append(info.NegativeSign);
						break;
					default:
						sb.Append(c);
						break;
					}
				}
			}

			private unsafe static void FormatScientific(ref System.Text.ValueStringBuilder sb, ref NumberBuffer number, int nMinDigits, int nMaxDigits, NumberFormatInfo info, char expChar)
			{
				char* digits = number.digits;
				sb.Append((*digits != 0) ? (*(digits++)) : '0');
				if (nMaxDigits != 1)
				{
					sb.Append(info.NumberDecimalSeparator);
				}
				while (--nMaxDigits > 0)
				{
					sb.Append((*digits != 0) ? (*(digits++)) : '0');
				}
				int value = ((*number.digits != 0) ? (number.scale - 1) : 0);
				FormatExponent(ref sb, info, value, expChar, 3, positiveSign: true);
			}

			private unsafe static void FormatExponent(ref System.Text.ValueStringBuilder sb, NumberFormatInfo info, int value, char expChar, int minDigits, bool positiveSign)
			{
				sb.Append(expChar);
				if (value < 0)
				{
					sb.Append(info.NegativeSign);
					value = -value;
				}
				else if (positiveSign)
				{
					sb.Append(info.PositiveSign);
				}
				char* ptr = stackalloc char[11];
				int index = 10;
				Int32ToDecChars(ptr, ref index, (uint)value, minDigits);
				int num = 10 - index;
				while (--num >= 0)
				{
					sb.Append(ptr[index++]);
				}
			}

			private unsafe static void FormatGeneral(ref System.Text.ValueStringBuilder sb, ref NumberBuffer number, int nMinDigits, int nMaxDigits, NumberFormatInfo info, char expChar, bool bSuppressScientific)
			{
				int i = number.scale;
				bool flag = false;
				if (!bSuppressScientific && (i > nMaxDigits || i < -3))
				{
					i = 1;
					flag = true;
				}
				char* digits = number.digits;
				if (i > 0)
				{
					do
					{
						sb.Append((*digits != 0) ? (*(digits++)) : '0');
					}
					while (--i > 0);
				}
				else
				{
					sb.Append('0');
				}
				if (*digits != 0 || i < 0)
				{
					sb.Append(info.NumberDecimalSeparator);
					for (; i < 0; i++)
					{
						sb.Append('0');
					}
					while (*digits != 0)
					{
						sb.Append(*(digits++));
					}
				}
				if (flag)
				{
					FormatExponent(ref sb, info, number.scale - 1, expChar, 2, positiveSign: true);
				}
			}

			private static void FormatPercent(ref System.Text.ValueStringBuilder sb, ref NumberBuffer number, int nMinDigits, int nMaxDigits, NumberFormatInfo info)
			{
				string text = (number.sign ? s_negPercentFormats[info.PercentNegativePattern] : s_posPercentFormats[info.PercentPositivePattern]);
				foreach (char c in text)
				{
					switch (c)
					{
					case '#':
						FormatFixed(ref sb, ref number, nMinDigits, nMaxDigits, info, info.PercentGroupSizes, info.PercentDecimalSeparator, info.PercentGroupSeparator);
						break;
					case '-':
						sb.Append(info.NegativeSign);
						break;
					case '%':
						sb.Append(info.PercentSymbol);
						break;
					default:
						sb.Append(c);
						break;
					}
				}
			}

			private unsafe static void RoundNumber(ref NumberBuffer number, int pos)
			{
				char* digits = number.digits;
				int i;
				for (i = 0; i < pos && digits[i] != 0; i++)
				{
				}
				if (i == pos && digits[i] >= '5')
				{
					while (i > 0 && digits[i - 1] == '9')
					{
						i--;
					}
					if (i > 0)
					{
						char* num = digits + (i - 1);
						*num = (char)(*num + 1);
					}
					else
					{
						number.scale++;
						*digits = '1';
						i = 1;
					}
				}
				else
				{
					while (i > 0 && digits[i - 1] == '0')
					{
						i--;
					}
				}
				if (i == 0)
				{
					number.scale = 0;
					number.sign = false;
				}
				digits[i] = '\0';
			}

			private unsafe static int FindSection(ReadOnlySpan<char> format, int section)
			{
				if (section == 0)
				{
					return 0;
				}
				fixed (char* reference = &MemoryMarshal.GetReference(format))
				{
					int num = 0;
					while (true)
					{
						if (num >= format.Length)
						{
							return 0;
						}
						char c2;
						char c = (c2 = reference[num++]);
						if ((uint)c <= 34u)
						{
							if (c == '\0')
							{
								break;
							}
							if (c != '"')
							{
								continue;
							}
						}
						else if (c != '\'')
						{
							switch (c)
							{
							default:
								continue;
							case '\\':
								if (num < format.Length && reference[num] != 0)
								{
									num++;
								}
								continue;
							case ';':
								break;
							}
							if (--section == 0)
							{
								if (num >= format.Length || reference[num] == '\0' || reference[num] == ';')
								{
									break;
								}
								return num;
							}
							continue;
						}
						while (num < format.Length && reference[num] != 0 && reference[num++] != c2)
						{
						}
					}
					return 0;
				}
			}

			internal unsafe static void NumberToStringFormat(ref System.Text.ValueStringBuilder sb, ref NumberBuffer number, ReadOnlySpan<char> format, NumberFormatInfo info)
			{
				int num = 0;
				char* digits = number.digits;
				int num2 = FindSection(format, (*digits == '\0') ? 2 : (number.sign ? 1 : 0));
				int num3;
				int num4;
				bool flag;
				bool flag2;
				int num5;
				int num6;
				int num9;
				while (true)
				{
					num3 = 0;
					num4 = -1;
					num5 = int.MaxValue;
					num6 = 0;
					flag = false;
					int num7 = -1;
					flag2 = false;
					int num8 = 0;
					num9 = num2;
					fixed (char* reference = &MemoryMarshal.GetReference(format))
					{
						char c;
						while (num9 < format.Length && (c = reference[num9++]) != 0)
						{
							switch (c)
							{
							case ';':
								break;
							case '#':
								num3++;
								continue;
							case '0':
								if (num5 == int.MaxValue)
								{
									num5 = num3;
								}
								num3++;
								num6 = num3;
								continue;
							case '.':
								if (num4 < 0)
								{
									num4 = num3;
								}
								continue;
							case ',':
								if (num3 <= 0 || num4 >= 0)
								{
									continue;
								}
								if (num7 >= 0)
								{
									if (num7 == num3)
									{
										num++;
										continue;
									}
									flag2 = true;
								}
								num7 = num3;
								num = 1;
								continue;
							case '%':
								num8 += 2;
								continue;
							case '‰':
								num8 += 3;
								continue;
							case '"':
							case '\'':
								while (num9 < format.Length && reference[num9] != 0 && reference[num9++] != c)
								{
								}
								continue;
							case '\\':
								if (num9 < format.Length && reference[num9] != 0)
								{
									num9++;
								}
								continue;
							case 'E':
							case 'e':
								if ((num9 < format.Length && reference[num9] == '0') || (num9 + 1 < format.Length && (reference[num9] == '+' || reference[num9] == '-') && reference[num9 + 1] == '0'))
								{
									while (++num9 < format.Length && reference[num9] == '0')
									{
									}
									flag = true;
								}
								continue;
							default:
								continue;
							}
							break;
						}
					}
					if (num4 < 0)
					{
						num4 = num3;
					}
					if (num7 >= 0)
					{
						if (num7 == num4)
						{
							num8 -= num * 3;
						}
						else
						{
							flag2 = true;
						}
					}
					if (*digits != 0)
					{
						number.scale += num8;
						int pos = (flag ? num3 : (number.scale + num3 - num4));
						RoundNumber(ref number, pos);
						if (*digits != 0)
						{
							break;
						}
						num9 = FindSection(format, 2);
						if (num9 == num2)
						{
							break;
						}
						num2 = num9;
						continue;
					}
					number.sign = false;
					number.scale = 0;
					break;
				}
				num5 = ((num5 < num4) ? (num4 - num5) : 0);
				num6 = ((num6 > num4) ? (num4 - num6) : 0);
				int num10;
				int num11;
				if (flag)
				{
					num10 = num4;
					num11 = 0;
				}
				else
				{
					num10 = ((number.scale > num4) ? number.scale : num4);
					num11 = number.scale - num4;
				}
				num9 = num2;
				Span<int> span = stackalloc int[4];
				int num12 = -1;
				if (flag2 && info.NumberGroupSeparator.Length > 0)
				{
					int[] numberGroupSizes = info.NumberGroupSizes;
					int num13 = 0;
					int i = 0;
					int num14 = numberGroupSizes.Length;
					if (num14 != 0)
					{
						i = numberGroupSizes[num13];
					}
					int num15 = i;
					int num16 = num10 + ((num11 < 0) ? num11 : 0);
					for (int num17 = ((num5 > num16) ? num5 : num16); num17 > i; i += num15)
					{
						if (num15 == 0)
						{
							break;
						}
						num12++;
						if (num12 >= span.Length)
						{
							int[] array = new int[span.Length * 2];
							span.CopyTo(array);
							span = array;
						}
						span[num12] = i;
						if (num13 < num14 - 1)
						{
							num13++;
							num15 = numberGroupSizes[num13];
						}
					}
				}
				if (number.sign && num2 == 0)
				{
					sb.Append(info.NegativeSign);
				}
				bool flag3 = false;
				fixed (char* reference2 = &MemoryMarshal.GetReference(format))
				{
					char* ptr = digits;
					char c;
					while (num9 < format.Length && (c = reference2[num9++]) != 0 && c != ';')
					{
						if (num11 > 0 && (c == '#' || c == '.' || c == '0'))
						{
							while (num11 > 0)
							{
								sb.Append((*ptr != 0) ? (*(ptr++)) : '0');
								if (flag2 && num10 > 1 && num12 >= 0 && num10 == span[num12] + 1)
								{
									sb.Append(info.NumberGroupSeparator);
									num12--;
								}
								num10--;
								num11--;
							}
						}
						switch (c)
						{
						case '#':
						case '0':
							if (num11 < 0)
							{
								num11++;
								c = ((num10 <= num5) ? '0' : '\0');
							}
							else
							{
								c = ((*ptr != 0) ? (*(ptr++)) : ((num10 > num6) ? '0' : '\0'));
							}
							if (c != 0)
							{
								sb.Append(c);
								if (flag2 && num10 > 1 && num12 >= 0 && num10 == span[num12] + 1)
								{
									sb.Append(info.NumberGroupSeparator);
									num12--;
								}
							}
							num10--;
							break;
						case '.':
							if (!(num10 != 0 || flag3) && (num6 < 0 || (num4 < num3 && *ptr != 0)))
							{
								sb.Append(info.NumberDecimalSeparator);
								flag3 = true;
							}
							break;
						case '‰':
							sb.Append(info.PerMilleSymbol);
							break;
						case '%':
							sb.Append(info.PercentSymbol);
							break;
						case '"':
						case '\'':
							while (num9 < format.Length && reference2[num9] != 0 && reference2[num9] != c)
							{
								sb.Append(reference2[num9++]);
							}
							if (num9 < format.Length && reference2[num9] != 0)
							{
								num9++;
							}
							break;
						case '\\':
							if (num9 < format.Length && reference2[num9] != 0)
							{
								sb.Append(reference2[num9++]);
							}
							break;
						case 'E':
						case 'e':
						{
							bool positiveSign = false;
							int num18 = 0;
							if (flag)
							{
								if (num9 < format.Length && reference2[num9] == '0')
								{
									num18++;
								}
								else if (num9 + 1 < format.Length && reference2[num9] == '+' && reference2[num9 + 1] == '0')
								{
									positiveSign = true;
								}
								else if (num9 + 1 >= format.Length || reference2[num9] != '-' || reference2[num9 + 1] != '0')
								{
									sb.Append(c);
									break;
								}
								while (++num9 < format.Length && reference2[num9] == '0')
								{
									num18++;
								}
								if (num18 > 10)
								{
									num18 = 10;
								}
								int value = ((*digits != 0) ? (number.scale - num4) : 0);
								FormatExponent(ref sb, info, value, c, num18, positiveSign);
								flag = false;
								break;
							}
							sb.Append(c);
							if (num9 < format.Length)
							{
								if (reference2[num9] == '+' || reference2[num9] == '-')
								{
									sb.Append(reference2[num9++]);
								}
								while (num9 < format.Length && reference2[num9] == '0')
								{
									sb.Append(reference2[num9++]);
								}
							}
							break;
						}
						default:
							sb.Append(c);
							break;
						case ',':
							break;
						}
					}
				}
			}
		}

		internal unsafe static void FormatBigInteger(ref System.Text.ValueStringBuilder sb, int precision, int scale, bool sign, ReadOnlySpan<char> format, NumberFormatInfo numberFormatInfo, char[] digits, int startIndex)
		{
			fixed (char* ptr = digits)
			{
				Number.NumberBuffer number = new Number.NumberBuffer
				{
					overrideDigits = ptr + startIndex,
					precision = precision,
					scale = scale,
					sign = sign
				};
				int digits2;
				char c = Number.ParseFormatSpecifier(format, out digits2);
				if (c != 0)
				{
					Number.NumberToString(ref sb, ref number, c, digits2, numberFormatInfo, isDecimal: false);
				}
				else
				{
					Number.NumberToStringFormat(ref sb, ref number, format, numberFormatInfo);
				}
			}
		}

		internal unsafe static bool TryStringToBigInteger(ReadOnlySpan<char> s, NumberStyles styles, NumberFormatInfo numberFormatInfo, StringBuilder receiver, out int precision, out int scale, out bool sign)
		{
			Number.NumberBuffer number = new Number.NumberBuffer
			{
				overrideDigits = (char*)1
			};
			if (!Number.TryStringToNumber(s, styles, ref number, receiver, numberFormatInfo, parseDecimal: false))
			{
				precision = 0;
				scale = 0;
				sign = false;
				return false;
			}
			precision = number.precision;
			scale = number.scale;
			sign = number.sign;
			return true;
		}
	}
}
namespace System.Text
{
	internal ref struct ValueStringBuilder
	{
		private char[] _arrayToReturnToPool;

		private Span<char> _chars;

		private int _pos;

		public int Length
		{
			get
			{
				return _pos;
			}
			set
			{
				_pos = value;
			}
		}

		public int Capacity => _chars.Length;

		public ref char this[int index] => ref _chars[index];

		public Span<char> RawChars => _chars;

		public ValueStringBuilder(Span<char> initialBuffer)
		{
			_arrayToReturnToPool = null;
			_chars = initialBuffer;
			_pos = 0;
		}

		public void EnsureCapacity(int capacity)
		{
			if (capacity > _chars.Length)
			{
				Grow(capacity - _chars.Length);
			}
		}

		public ref char GetPinnableReference(bool terminate = false)
		{
			if (terminate)
			{
				EnsureCapacity(Length + 1);
				_chars[Length] = '\0';
			}
			return ref MemoryMarshal.GetReference(_chars);
		}

		public override string ToString()
		{
			string result = new string(_chars.Slice(0, _pos));
			Dispose();
			return result;
		}

		public ReadOnlySpan<char> AsSpan(bool terminate)
		{
			if (terminate)
			{
				EnsureCapacity(Length + 1);
				_chars[Length] = '\0';
			}
			return _chars.Slice(0, _pos);
		}

		public ReadOnlySpan<char> AsSpan()
		{
			return _chars.Slice(0, _pos);
		}

		public ReadOnlySpan<char> AsSpan(int start)
		{
			return _chars.Slice(start, _pos - start);
		}

		public ReadOnlySpan<char> AsSpan(int start, int length)
		{
			return _chars.Slice(start, length);
		}

		public bool TryCopyTo(Span<char> destination, out int charsWritten)
		{
			if (_chars.Slice(0, _pos).TryCopyTo(destination))
			{
				charsWritten = _pos;
				Dispose();
				return true;
			}
			charsWritten = 0;
			Dispose();
			return false;
		}

		public void Insert(int index, char value, int count)
		{
			if (_pos > _chars.Length - count)
			{
				Grow(count);
			}
			int length = _pos - index;
			_chars.Slice(index, length).CopyTo(_chars.Slice(index + count));
			_chars.Slice(index, count).Fill(value);
			_pos += count;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(char c)
		{
			int pos = _pos;
			if (pos < _chars.Length)
			{
				_chars[pos] = c;
				_pos = pos + 1;
			}
			else
			{
				GrowAndAppend(c);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(string s)
		{
			int pos = _pos;
			if (s.Length == 1 && pos < _chars.Length)
			{
				_chars[pos] = s[0];
				_pos = pos + 1;
			}
			else
			{
				AppendSlow(s);
			}
		}

		private void AppendSlow(string s)
		{
			int pos = _pos;
			if (pos > _chars.Length - s.Length)
			{
				Grow(s.Length);
			}
			s.AsSpan().CopyTo(_chars.Slice(pos));
			_pos += s.Length;
		}

		public void Append(char c, int count)
		{
			if (_pos > _chars.Length - count)
			{
				Grow(count);
			}
			Span<char> span = _chars.Slice(_pos, count);
			for (int i = 0; i < span.Length; i++)
			{
				span[i] = c;
			}
			_pos += count;
		}

		public unsafe void Append(char* value, int length)
		{
			if (_pos > _chars.Length - length)
			{
				Grow(length);
			}
			Span<char> span = _chars.Slice(_pos, length);
			for (int i = 0; i < span.Length; i++)
			{
				span[i] = *(value++);
			}
			_pos += length;
		}

		public void Append(ReadOnlySpan<char> value)
		{
			if (_pos > _chars.Length - value.Length)
			{
				Grow(value.Length);
			}
			value.CopyTo(_chars.Slice(_pos));
			_pos += value.Length;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<char> AppendSpan(int length)
		{
			int pos = _pos;
			if (pos > _chars.Length - length)
			{
				Grow(length);
			}
			_pos = pos + length;
			return _chars.Slice(pos, length);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GrowAndAppend(char c)
		{
			Grow(1);
			Append(c);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Grow(int requiredAdditionalCapacity)
		{
			char[] array = ArrayPool<char>.Shared.Rent(Math.Max(_pos + requiredAdditionalCapacity, _chars.Length * 2));
			_chars.CopyTo(array);
			char[] arrayToReturnToPool = _arrayToReturnToPool;
			_chars = (_arrayToReturnToPool = array);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			char[] arrayToReturnToPool = _arrayToReturnToPool;
			this = default(System.Text.ValueStringBuilder);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool);
			}
		}
	}
}
namespace System.Runtime.CompilerServices
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, Inherited = false)]
	internal sealed class IntrinsicAttribute : Attribute
	{
	}
	internal class FriendAccessAllowedAttribute : Attribute
	{
	}
}
