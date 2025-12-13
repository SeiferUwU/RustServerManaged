using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

[assembly: AssemblyFileVersion("4.6.57.0")]
[assembly: AssemblyInformationalVersion("4.6.57.0")]
[assembly: AssemblyCopyright("(c) Various Mono authors")]
[assembly: AssemblyProduct("Mono Common Language Infrastructure")]
[assembly: AssemblyCompany("Mono development team")]
[assembly: AssemblyDefaultAlias("System.IO.Compression.FileSystem.dll")]
[assembly: AssemblyDescription("System.IO.Compression.FileSystem.dll")]
[assembly: AssemblyTitle("System.IO.Compression.FileSystem.dll")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: CompilationRelaxations(8)]
[assembly: AssemblyDelaySign(true)]
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
internal class SR
{
	public const string IO_DirectoryNameWithData = "Zip entry name ends in directory separator character but contains data.";

	public const string IO_ExtractingResultsInOutside = "Extracting Zip entry would have resulted in a file outside the specified destination directory.";
}
namespace System.IO
{
	internal static class PathInternal
	{
		private static readonly bool s_isCaseSensitive = GetIsCaseSensitive();

		internal static StringComparison StringComparison
		{
			get
			{
				if (!s_isCaseSensitive)
				{
					return StringComparison.OrdinalIgnoreCase;
				}
				return StringComparison.Ordinal;
			}
		}

		internal static bool IsCaseSensitive => s_isCaseSensitive;

		private static bool GetIsCaseSensitive()
		{
			try
			{
				string text = Path.Combine(Path.GetTempPath(), "CASESENSITIVETEST" + Guid.NewGuid().ToString("N"));
				using (new FileStream(text, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.DeleteOnClose))
				{
					return !File.Exists(text.ToLowerInvariant());
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
namespace System.IO.Compression
{
	/// <summary>Provides static methods for creating, extracting, and opening zip archives.</summary>
	public static class ZipFile
	{
		private const char PathSeparator = '/';

		/// <summary>Opens a zip archive for reading at the specified path.</summary>
		/// <param name="archiveFileName">The path to the archive to open, specified as a relative or absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <returns>The opened zip archive.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="archiveFileName" /> is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="archiveFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">In <paramref name="archiveFileName" />, the specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="archiveFileName" /> is invalid or does not exist (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="archiveFileName" /> could not be opened.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="archiveFileName" /> specifies a directory.  
		/// -or-  
		/// The caller does not have the required permission to access the file specified in <paramref name="archiveFileName" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="archiveFileName" /> is not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="archiveFileName" /> contains an invalid format.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">
		///   <paramref name="archiveFileName" /> could not be interpreted as a zip archive.</exception>
		public static ZipArchive OpenRead(string archiveFileName)
		{
			return Open(archiveFileName, ZipArchiveMode.Read);
		}

		/// <summary>Opens a zip archive at the specified path and in the specified mode.</summary>
		/// <param name="archiveFileName">The path to the archive to open, specified as a relative or absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <param name="mode">One of the enumeration values that specifies the actions which are allowed on the entries in the opened archive.</param>
		/// <returns>The opened zip archive.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="archiveFileName" /> is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="archiveFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">In <paramref name="archiveFileName" />, the specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="archiveFileName" /> is invalid or does not exist (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="archiveFileName" /> could not be opened.  
		/// -or-  
		/// <paramref name="mode" /> is set to <see cref="F:System.IO.Compression.ZipArchiveMode.Create" />, but the file specified in <paramref name="archiveFileName" /> already exists.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="archiveFileName" /> specifies a directory.  
		/// -or-  
		/// The caller does not have the required permission to access the file specified in <paramref name="archiveFileName" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="mode" /> is set to <see cref="F:System.IO.Compression.ZipArchiveMode.Read" />, but the file specified in <paramref name="archiveFileName" /> is not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="archiveFileName" /> contains an invalid format.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">
		///   <paramref name="archiveFileName" /> could not be interpreted as a zip archive.  
		/// -or-  
		/// <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" />, but an entry is missing or corrupt and cannot be read.  
		/// -or-  
		/// <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" />, but an entry is too large to fit into memory.</exception>
		public static ZipArchive Open(string archiveFileName, ZipArchiveMode mode)
		{
			return Open(archiveFileName, mode, null);
		}

		/// <summary>Opens a zip archive at the specified path, in the specified mode, and by using the specified character encoding for entry names.</summary>
		/// <param name="archiveFileName">The path to the archive to open, specified as a relative or absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <param name="mode">One of the enumeration values that specifies the actions that are allowed on the entries in the opened archive.</param>
		/// <param name="entryNameEncoding">The encoding to use when reading or writing entry names in this archive. Specify a value for this parameter only when an encoding is required for interoperability with zip archive tools and libraries that do not support UTF-8 encoding for entry names.</param>
		/// <returns>The opened zip archive.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="archiveFileName" /> is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.  
		/// -or-  
		/// <paramref name="entryNameEncoding" /> is set to a Unicode encoding other than UTF-8.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="archiveFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">In <paramref name="archiveFileName" />, the specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="archiveFileName" /> is invalid or does not exist (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="archiveFileName" /> could not be opened.  
		/// -or-  
		/// <paramref name="mode" /> is set to <see cref="F:System.IO.Compression.ZipArchiveMode.Create" />, but the file specified in <paramref name="archiveFileName" /> already exists.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="archiveFileName" /> specifies a directory.  
		/// -or-  
		/// The caller does not have the required permission to access the file specified in <paramref name="archiveFileName" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="mode" /> is set to <see cref="F:System.IO.Compression.ZipArchiveMode.Read" />, but the file specified in <paramref name="archiveFileName" /> is not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="archiveFileName" /> contains an invalid format.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">
		///   <paramref name="archiveFileName" /> could not be interpreted as a zip archive.  
		/// -or-  
		/// <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" />, but an entry is missing or corrupt and cannot be read.  
		/// -or-  
		/// <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" />, but an entry is too large to fit into memory.</exception>
		public static ZipArchive Open(string archiveFileName, ZipArchiveMode mode, Encoding entryNameEncoding)
		{
			FileMode mode2;
			FileAccess access;
			FileShare share;
			switch (mode)
			{
			case ZipArchiveMode.Read:
				mode2 = FileMode.Open;
				access = FileAccess.Read;
				share = FileShare.Read;
				break;
			case ZipArchiveMode.Create:
				mode2 = FileMode.CreateNew;
				access = FileAccess.Write;
				share = FileShare.None;
				break;
			case ZipArchiveMode.Update:
				mode2 = FileMode.OpenOrCreate;
				access = FileAccess.ReadWrite;
				share = FileShare.None;
				break;
			default:
				throw new ArgumentOutOfRangeException("mode");
			}
			FileStream fileStream = new FileStream(archiveFileName, mode2, access, share, 4096, useAsync: false);
			try
			{
				return new ZipArchive(fileStream, mode, leaveOpen: false, entryNameEncoding);
			}
			catch
			{
				fileStream.Dispose();
				throw;
			}
		}

		/// <summary>Creates a zip archive that contains the files and directories from the specified directory.</summary>
		/// <param name="sourceDirectoryName">The path to the directory to be archived, specified as a relative or absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <param name="destinationArchiveFileName">The path of the archive to be created, specified as a relative or absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceDirectoryName" /> or <paramref name="destinationArchiveFileName" /> is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceDirectoryName" /> or <paramref name="destinationArchiveFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">In <paramref name="sourceDirectoryName" /> or <paramref name="destinationArchiveFileName" />, the specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceDirectoryName" /> is invalid or does not exist (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="destinationArchiveFileName" /> already exists.  
		/// -or-  
		/// A file in the specified directory could not be opened.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="destinationArchiveFileName" /> specifies a directory.  
		/// -or-  
		/// The caller does not have the required permission to access the directory specified in <paramref name="sourceDirectoryName" /> or the file specified in <paramref name="destinationArchiveFileName" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="sourceDirectoryName" /> or <paramref name="destinationArchiveFileName" /> contains an invalid format.  
		/// -or-  
		/// The zip archive does not support writing.</exception>
		public static void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName)
		{
			DoCreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, null, includeBaseDirectory: false, null);
		}

		/// <summary>Creates a zip archive that contains the files and directories from the specified directory, uses the specified compression level, and optionally includes the base directory.</summary>
		/// <param name="sourceDirectoryName">The path to the directory to be archived, specified as a relative or absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <param name="destinationArchiveFileName">The path of the archive to be created, specified as a relative or absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression effectiveness when creating the entry.</param>
		/// <param name="includeBaseDirectory">
		///   <see langword="true" /> to include the directory name from <paramref name="sourceDirectoryName" /> at the root of the archive; <see langword="false" /> to include only the contents of the directory.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceDirectoryName" /> or <paramref name="destinationArchiveFileName" /> is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceDirectoryName" /> or <paramref name="destinationArchiveFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">In <paramref name="sourceDirectoryName" /> or <paramref name="destinationArchiveFileName" />, the specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceDirectoryName" /> is invalid or does not exist (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="destinationArchiveFileName" /> already exists.  
		/// -or-  
		/// A file in the specified directory could not be opened.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="destinationArchiveFileName" /> specifies a directory.  
		/// -or-  
		/// The caller does not have the required permission to access the directory specified in <paramref name="sourceDirectoryName" /> or the file specified in <paramref name="destinationArchiveFileName" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="sourceDirectoryName" /> or <paramref name="destinationArchiveFileName" /> contains an invalid format.  
		/// -or-  
		/// The zip archive does not support writing.</exception>
		public static void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName, CompressionLevel compressionLevel, bool includeBaseDirectory)
		{
			DoCreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, compressionLevel, includeBaseDirectory, null);
		}

		/// <summary>Creates a zip archive that contains the files and directories from the specified directory, uses the specified compression level and character encoding for entry names, and optionally includes the base directory.</summary>
		/// <param name="sourceDirectoryName">The path to the directory to be archived, specified as a relative or absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <param name="destinationArchiveFileName">The path of the archive to be created, specified as a relative or absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression effectiveness when creating the entry.</param>
		/// <param name="includeBaseDirectory">
		///   <see langword="true" /> to include the directory name from <paramref name="sourceDirectoryName" /> at the root of the archive; <see langword="false" /> to include only the contents of the directory.</param>
		/// <param name="entryNameEncoding">The encoding to use when reading or writing entry names in this archive. Specify a value for this parameter only when an encoding is required for interoperability with zip archive tools and libraries that do not support UTF-8 encoding for entry names.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceDirectoryName" /> or <paramref name="destinationArchiveFileName" /> is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.  
		/// -or-  
		/// <paramref name="entryNameEncoding" /> is set to a Unicode encoding other than UTF-8.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceDirectoryName" /> or <paramref name="destinationArchiveFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">In <paramref name="sourceDirectoryName" /> or <paramref name="destinationArchiveFileName" />, the specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceDirectoryName" /> is invalid or does not exist (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="destinationArchiveFileName" /> already exists.  
		/// -or-  
		/// A file in the specified directory could not be opened.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="destinationArchiveFileName" /> specifies a directory.  
		/// -or-  
		/// The caller does not have the required permission to access the directory specified in <paramref name="sourceDirectoryName" /> or the file specified in <paramref name="destinationArchiveFileName" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="sourceDirectoryName" /> or <paramref name="destinationArchiveFileName" /> contains an invalid format.  
		/// -or-  
		/// The zip archive does not support writing.</exception>
		public static void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName, CompressionLevel compressionLevel, bool includeBaseDirectory, Encoding entryNameEncoding)
		{
			DoCreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, compressionLevel, includeBaseDirectory, entryNameEncoding);
		}

		/// <summary>Extracts all the files in the specified zip archive to a directory on the file system.</summary>
		/// <param name="sourceArchiveFileName">The path to the archive that is to be extracted.</param>
		/// <param name="destinationDirectoryName">The path to the directory in which to place the extracted files, specified as a relative or absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destinationDirectoryName" /> or <paramref name="sourceArchiveFileName" /> is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationDirectoryName" /> or <paramref name="sourceArchiveFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path in <paramref name="destinationDirectoryName" /> or <paramref name="sourceArchiveFileName" /> exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="destinationDirectoryName" /> already exists.  
		///  -or-  
		///  The name of an entry in the archive is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.  
		///  -or-  
		///  Extracting an archive entry would create a file that is outside the directory specified by <paramref name="destinationDirectoryName" />. (For example, this might happen if the entry name contains parent directory accessors.)  
		///  -or-  
		///  An archive entry to extract has the same name as an entry that has already been extracted from the same archive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission to access the archive or the destination directory.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="destinationDirectoryName" /> or <paramref name="sourceArchiveFileName" /> contains an invalid format.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceArchiveFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The archive specified by <paramref name="sourceArchiveFileName" /> is not a valid zip archive.  
		///  -or-  
		///  An archive entry was not found or was corrupt.  
		///  -or-  
		///  An archive entry was compressed by using a compression method that is not supported.</exception>
		public static void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
		{
			ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName, null);
		}

		public static void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName, bool overwrite)
		{
			ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName, null, overwrite);
		}

		/// <summary>Extracts all the files in the specified zip archive to a directory on the file system and uses the specified character encoding for entry names.</summary>
		/// <param name="sourceArchiveFileName">The path to the archive that is to be extracted.</param>
		/// <param name="destinationDirectoryName">The path to the directory in which to place the extracted files, specified as a relative or absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <param name="entryNameEncoding">The encoding to use when reading or writing entry names in this archive. Specify a value for this parameter only when an encoding is required for interoperability with zip archive tools and libraries that do not support UTF-8 encoding for entry names.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destinationDirectoryName" /> or <paramref name="sourceArchiveFileName" /> is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.  
		/// -or-  
		/// <paramref name="entryNameEncoding" /> is set to a Unicode encoding other than UTF-8.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationDirectoryName" /> or <paramref name="sourceArchiveFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path in <paramref name="destinationDirectoryName" /> or <paramref name="sourceArchiveFileName" /> exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="destinationDirectoryName" /> already exists.  
		///  -or-  
		///  The name of an entry in the archive is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.  
		///  -or-  
		///  Extracting an archive entry would create a file that is outside the directory specified by <paramref name="destinationDirectoryName" />. (For example, this might happen if the entry name contains parent directory accessors.)  
		///  -or-  
		///  An archive entry to extract has the same name as an entry that has already been extracted from the same archive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission to access the archive or the destination directory.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="destinationDirectoryName" /> or <paramref name="sourceArchiveFileName" /> contains an invalid format.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceArchiveFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The archive specified by <paramref name="sourceArchiveFileName" /> is not a valid zip archive.  
		///  -or-  
		///  An archive entry was not found or was corrupt.  
		///  -or-  
		///  An archive entry was compressed by using a compression method that is not supported.</exception>
		public static void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName, Encoding entryNameEncoding)
		{
			ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName, entryNameEncoding, overwrite: false);
		}

		public static void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName, Encoding entryNameEncoding, bool overwrite)
		{
			if (sourceArchiveFileName == null)
			{
				throw new ArgumentNullException("sourceArchiveFileName");
			}
			using ZipArchive source = Open(sourceArchiveFileName, ZipArchiveMode.Read, entryNameEncoding);
			source.ExtractToDirectory(destinationDirectoryName, overwrite);
		}

		private static void DoCreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName, CompressionLevel? compressionLevel, bool includeBaseDirectory, Encoding entryNameEncoding)
		{
			sourceDirectoryName = Path.GetFullPath(sourceDirectoryName);
			destinationArchiveFileName = Path.GetFullPath(destinationArchiveFileName);
			using ZipArchive zipArchive = Open(destinationArchiveFileName, ZipArchiveMode.Create, entryNameEncoding);
			bool flag = true;
			DirectoryInfo directoryInfo = new DirectoryInfo(sourceDirectoryName);
			string fullName = directoryInfo.FullName;
			if (includeBaseDirectory && directoryInfo.Parent != null)
			{
				fullName = directoryInfo.Parent.FullName;
			}
			char[] buffer = ArrayPool<char>.Shared.Rent(260);
			try
			{
				foreach (FileSystemInfo item in directoryInfo.EnumerateFileSystemInfos("*", SearchOption.AllDirectories))
				{
					flag = false;
					int length = item.FullName.Length - fullName.Length;
					if (item is FileInfo)
					{
						string entryName = EntryFromPath(item.FullName, fullName.Length, length, ref buffer);
						ZipFileExtensions.DoCreateEntryFromFile(zipArchive, item.FullName, entryName, compressionLevel);
					}
					else if (item is DirectoryInfo possiblyEmptyDir && IsDirEmpty(possiblyEmptyDir))
					{
						string entryName2 = EntryFromPath(item.FullName, fullName.Length, length, ref buffer, appendPathSeparator: true);
						zipArchive.CreateEntry(entryName2);
					}
				}
				if (includeBaseDirectory && flag)
				{
					zipArchive.CreateEntry(EntryFromPath(directoryInfo.Name, 0, directoryInfo.Name.Length, ref buffer, appendPathSeparator: true));
				}
			}
			finally
			{
				ArrayPool<char>.Shared.Return(buffer);
			}
		}

		private static string EntryFromPath(string entry, int offset, int length, ref char[] buffer, bool appendPathSeparator = false)
		{
			while (length > 0 && (entry[offset] == Path.DirectorySeparatorChar || entry[offset] == Path.AltDirectorySeparatorChar))
			{
				offset++;
				length--;
			}
			if (length == 0)
			{
				if (!appendPathSeparator)
				{
					return string.Empty;
				}
				return '/'.ToString();
			}
			int num = (appendPathSeparator ? (length + 1) : length);
			EnsureCapacity(ref buffer, num);
			entry.CopyTo(offset, buffer, 0, length);
			for (int i = 0; i < length; i++)
			{
				char c = buffer[i];
				if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar)
				{
					buffer[i] = '/';
				}
			}
			if (appendPathSeparator)
			{
				buffer[length] = '/';
			}
			return new string(buffer, 0, num);
		}

		private static void EnsureCapacity(ref char[] buffer, int min)
		{
			if (buffer.Length < min)
			{
				int num = buffer.Length * 2;
				if (num < min)
				{
					num = min;
				}
				ArrayPool<char>.Shared.Return(buffer);
				buffer = ArrayPool<char>.Shared.Rent(num);
			}
		}

		private static bool IsDirEmpty(DirectoryInfo possiblyEmptyDir)
		{
			using IEnumerator<string> enumerator = Directory.EnumerateFileSystemEntries(possiblyEmptyDir.FullName).GetEnumerator();
			return !enumerator.MoveNext();
		}
	}
	/// <summary>Provides extension methods for the <see cref="T:System.IO.Compression.ZipArchive" /> and <see cref="T:System.IO.Compression.ZipArchiveEntry" /> classes.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ZipFileExtensions
	{
		/// <summary>Archives a file by compressing it and adding it to the zip archive.</summary>
		/// <param name="destination">The zip archive to add the file to.</param>
		/// <param name="sourceFileName">The path to the file to be archived. You can specify either a relative or an absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <param name="entryName">The name of the entry to create in the zip archive.</param>
		/// <returns>A wrapper for the new entry in the zip archive.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.  
		/// -or-  
		/// <paramref name="entryName" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="entryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">In <paramref name="sourceFileName" />, the specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceFileName" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">The file specified by <paramref name="sourceFileName" /> cannot be opened, or is too large to be updated (current limit is Int32.MaxValue).</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="sourceFileName" /> specifies a directory.  
		/// -or-  
		/// The caller does not have the required permission to access the file specified by <paramref name="sourceFileName" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="sourceFileName" /> is not found.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="sourceFileName" /> parameter is in an invalid format.  
		///  -or-  
		///  The zip archive does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
		public static ZipArchiveEntry CreateEntryFromFile(this ZipArchive destination, string sourceFileName, string entryName)
		{
			return DoCreateEntryFromFile(destination, sourceFileName, entryName, null);
		}

		/// <summary>Archives a file by compressing it using the specified compression level and adding it to the zip archive.</summary>
		/// <param name="destination">The zip archive to add the file to.</param>
		/// <param name="sourceFileName">The path to the file to be archived. You can specify either a relative or an absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <param name="entryName">The name of the entry to create in the zip archive.</param>
		/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression effectiveness when creating the entry.</param>
		/// <returns>A wrapper for the new entry in the zip archive.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.  
		/// -or-  
		/// <paramref name="entryName" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="entryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceFileName" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.PathTooLongException">In <paramref name="sourceFileName" />, the specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.IOException">The file specified by <paramref name="sourceFileName" /> cannot be opened, or is too large to be updated (current limit is Int32.MaxValue).</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="sourceFileName" /> specifies a directory.  
		/// -or-  
		/// The caller does not have the required permission to access the file specified by <paramref name="sourceFileName" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="sourceFileName" /> is not found.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="sourceFileName" /> parameter is in an invalid format.  
		///  -or-  
		///  The zip archive does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
		public static ZipArchiveEntry CreateEntryFromFile(this ZipArchive destination, string sourceFileName, string entryName, CompressionLevel compressionLevel)
		{
			return DoCreateEntryFromFile(destination, sourceFileName, entryName, compressionLevel);
		}

		/// <summary>Extracts all the files in the zip archive to a directory on the file system.</summary>
		/// <param name="source">The zip archive to extract files from.</param>
		/// <param name="destinationDirectoryName">The path to the directory to place the extracted files in. You can specify either a relative or an absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destinationDirectoryName" /> is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationDirectoryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="destinationDirectoryName" /> already exists.  
		///  -or-  
		///  The name of an entry in the archive is <see cref="F:System.String.Empty" />, contains only white space, or contains at least one invalid character.  
		///  -or-  
		///  Extracting an entry from the archive would create a file that is outside the directory specified by <paramref name="destinationDirectoryName" />. (For example, this might happen if the entry name contains parent directory accessors.)  
		///  -or-  
		///  Two or more entries in the archive have the same name.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission to write to the destination directory.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="destinationDirectoryName" /> contains an invalid format.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">An archive entry cannot be found or is corrupt.  
		///  -or-  
		///  An archive entry was compressed by using a compression method that is not supported.</exception>
		public static void ExtractToDirectory(this ZipArchive source, string destinationDirectoryName)
		{
			source.ExtractToDirectory(destinationDirectoryName, overwrite: false);
		}

		public static void ExtractToDirectory(this ZipArchive source, string destinationDirectoryName, bool overwrite)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (destinationDirectoryName == null)
			{
				throw new ArgumentNullException("destinationDirectoryName");
			}
			string text = Directory.CreateDirectory(destinationDirectoryName).FullName;
			if (!text.EndsWith(Path.DirectorySeparatorChar))
			{
				text += Path.DirectorySeparatorChar;
			}
			foreach (ZipArchiveEntry entry in source.Entries)
			{
				string fullPath = Path.GetFullPath(Path.Combine(text, entry.FullName));
				if (!fullPath.StartsWith(text, System.IO.PathInternal.StringComparison))
				{
					throw new IOException("Extracting Zip entry would have resulted in a file outside the specified destination directory.");
				}
				if (Path.GetFileName(fullPath).Length == 0)
				{
					if (entry.Length != 0L)
					{
						throw new IOException("Zip entry name ends in directory separator character but contains data.");
					}
					Directory.CreateDirectory(fullPath);
				}
				else
				{
					Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
					entry.ExtractToFile(fullPath, overwrite);
				}
			}
		}

		internal static ZipArchiveEntry DoCreateEntryFromFile(ZipArchive destination, string sourceFileName, string entryName, CompressionLevel? compressionLevel)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			using Stream stream = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: false);
			ZipArchiveEntry zipArchiveEntry = (compressionLevel.HasValue ? destination.CreateEntry(entryName, compressionLevel.Value) : destination.CreateEntry(entryName));
			DateTime dateTime = File.GetLastWriteTime(sourceFileName);
			if (dateTime.Year < 1980 || dateTime.Year > 2107)
			{
				dateTime = new DateTime(1980, 1, 1, 0, 0, 0);
			}
			zipArchiveEntry.LastWriteTime = dateTime;
			using (Stream destination2 = zipArchiveEntry.Open())
			{
				stream.CopyTo(destination2);
			}
			return zipArchiveEntry;
		}

		/// <summary>Extracts an entry in the zip archive to a file.</summary>
		/// <param name="source">The zip archive entry to extract a file from.</param>
		/// <param name="destinationFileName">The path of the file to create from the contents of the entry. You can  specify either a relative or an absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destinationFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.  
		/// -or-  
		/// <paramref name="destinationFileName" /> specifies a directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="destinationFileName" /> already exists.  
		/// -or-  
		/// An I/O error occurred.  
		/// -or-  
		/// The entry is currently open for writing.  
		/// -or-  
		/// The entry has been deleted from the archive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission to create the new file.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The entry is missing from the archive, or is corrupt and cannot be read.  
		///  -or-  
		///  The entry has been compressed by using a compression method that is not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive that this entry belongs to has been disposed.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="destinationFileName" /> is in an invalid format.  
		/// -or-  
		/// The zip archive for this entry was opened in <see cref="F:System.IO.Compression.ZipArchiveMode.Create" /> mode, which does not permit the retrieval of entries.</exception>
		public static void ExtractToFile(this ZipArchiveEntry source, string destinationFileName)
		{
			source.ExtractToFile(destinationFileName, overwrite: false);
		}

		/// <summary>Extracts an entry in the zip archive to a file, and optionally overwrites an existing file that has the same name.</summary>
		/// <param name="source">The zip archive entry to extract a file from.</param>
		/// <param name="destinationFileName">The path of the file to create from the contents of the entry. You can specify either a relative or an absolute path. A relative path is interpreted as relative to the current working directory.</param>
		/// <param name="overwrite">
		///   <see langword="true" /> to overwrite an existing file that has the same name as the destination file; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destinationFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.  
		/// -or-  
		/// <paramref name="destinationFileName" /> specifies a directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="destinationFileName" /> already exists and <paramref name="overwrite" /> is <see langword="false" />.  
		/// -or-  
		/// An I/O error occurred.  
		/// -or-  
		/// The entry is currently open for writing.  
		/// -or-  
		/// The entry has been deleted from the archive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission to create the new file.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The entry is missing from the archive or is corrupt and cannot be read.  
		///  -or-  
		///  The entry has been compressed by using a compression method that is not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive that this entry belongs to has been disposed.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="destinationFileName" /> is in an invalid format.  
		/// -or-  
		/// The zip archive for this entry was opened in <see cref="F:System.IO.Compression.ZipArchiveMode.Create" /> mode, which does not permit the retrieval of entries.</exception>
		public static void ExtractToFile(this ZipArchiveEntry source, string destinationFileName, bool overwrite)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			FileMode mode = ((!overwrite) ? FileMode.CreateNew : FileMode.Create);
			using (Stream destination = new FileStream(destinationFileName, mode, FileAccess.Write, FileShare.None, 4096, useAsync: false))
			{
				using Stream stream = source.Open();
				stream.CopyTo(destination);
			}
			File.SetLastWriteTime(destinationFileName, source.LastWriteTime.DateTime);
		}
	}
}
