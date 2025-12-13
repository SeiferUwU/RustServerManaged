using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("System.IO.FileSystem.AccessControl")]
[assembly: AssemblyDescription("System.IO.FileSystem.AccessControl")]
[assembly: AssemblyDefaultAlias("System.IO.FileSystem.AccessControl")]
[assembly: AssemblyCompany("Mono development team")]
[assembly: AssemblyProduct("Mono Common Language Infrastructure")]
[assembly: AssemblyCopyright("(c) Various Mono authors")]
[assembly: AssemblyInformationalVersion("4.0.0.0")]
[assembly: AssemblyFileVersion("4.0.0.0")]
[assembly: AssemblyVersion("4.0.2.0")]
[assembly: TypeForwardedTo(typeof(DirectoryObjectSecurity))]
[assembly: TypeForwardedTo(typeof(DirectorySecurity))]
[assembly: TypeForwardedTo(typeof(FileSecurity))]
[assembly: TypeForwardedTo(typeof(FileSystemAccessRule))]
[assembly: TypeForwardedTo(typeof(FileSystemAuditRule))]
[assembly: TypeForwardedTo(typeof(FileSystemRights))]
[assembly: TypeForwardedTo(typeof(FileSystemSecurity))]
namespace System.IO;

public static class FileSystemAclExtensions
{
	public static DirectorySecurity GetAccessControl(this DirectoryInfo directoryInfo)
	{
		return directoryInfo.GetAccessControl();
	}

	public static DirectorySecurity GetAccessControl(this DirectoryInfo directoryInfo, AccessControlSections includeSections)
	{
		return directoryInfo.GetAccessControl(includeSections);
	}

	public static void SetAccessControl(this DirectoryInfo directoryInfo, DirectorySecurity directorySecurity)
	{
		directoryInfo.SetAccessControl(directorySecurity);
	}

	public static FileSecurity GetAccessControl(this FileInfo fileInfo)
	{
		return fileInfo.GetAccessControl();
	}

	public static FileSecurity GetAccessControl(this FileInfo fileInfo, AccessControlSections includeSections)
	{
		return fileInfo.GetAccessControl(includeSections);
	}

	public static void SetAccessControl(this FileInfo fileInfo, FileSecurity fileSecurity)
	{
		fileInfo.SetAccessControl(fileSecurity);
	}

	public static FileSecurity GetAccessControl(this FileStream fileStream)
	{
		return fileStream.GetAccessControl();
	}

	public static void SetAccessControl(this FileStream fileStream, FileSecurity fileSecurity)
	{
		fileStream.SetAccessControl(fileSecurity);
	}
}
